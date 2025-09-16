// Decompiled with JetBrains decompiler
// Type: PX.SM.PXThemeLoader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance.SM.DAC;
using PX.DbServices.QueryObjectModel;
using PX.SP;
using PX.SP.Alias;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

#nullable enable
namespace PX.SM;

public class PXThemeLoader
{
  public const 
  #nullable disable
  string THEME_DEFAULT = "Default";
  public const string THEME_MODERN = "Modern";
  public const string THEME_FOLDER = "App_Themes";
  private const string SlotName = "ThemeLoader";
  private const string BranchSlotName = "ThemeLoaderPerBranch";
  private const string CssThemeConfigStart = "CSS THEME CONFIG DO NOT REMOVE START";
  private const string CssThemeConfigEnd = "CSS THEME CONFIG DO NOT REMOVE END";
  private static Lazy<Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>>> _cssVariables = new Lazy<Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>>>(new Func<Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>>>(PXThemeLoader.InitializeThemeInfo));
  private static Regex color = new Regex("^#([0-9a-fA-F]{6})$", RegexOptions.Compiled);

  static PXThemeLoader()
  {
    try
    {
      new FileSystemWatcher()
      {
        Path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Themes"),
        NotifyFilter = NotifyFilters.LastWrite,
        Filter = "*.css",
        EnableRaisingEvents = true,
        IncludeSubdirectories = true
      }.Changed += (FileSystemEventHandler) ((s, e) => PXThemeLoader._cssVariables = new Lazy<Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>>>(new Func<Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>>>(PXThemeLoader.InitializeThemeInfo)));
    }
    catch
    {
      PXTrace.WriteError("PXThemeLoader failed to watch App_Themes directory");
    }
  }

  public static string ThemeName
  {
    get
    {
      PXThemeLoader.Definition globalDefinition = PXThemeLoader.GlobalDefinition;
      if (!PortalHelper.IsPortalContext(PortalContexts.Modern))
        return globalDefinition.CompanyTheme;
      PortalInfo portal = PortalHelper.GetPortal();
      return !PXThemeLoader.AvailableThemes.Contains<string>(portal?.Theme ?? string.Empty) ? globalDefinition.CompanyTheme : portal.Theme;
    }
  }

  private static PXThemeLoader.Definition GlobalDefinition
  {
    get
    {
      try
      {
        return PXDatabase.GetSlot<PXThemeLoader.Definition>("ThemeLoader", typeof (PreferencesGeneral), typeof (PXThemeLoader.Organization), typeof (PXThemeLoader.Branch), typeof (ThemeVariables), typeof (SPPortal)) ?? PXThemeLoader.Definition.Default;
      }
      catch
      {
        return PXThemeLoader.Definition.Default;
      }
    }
  }

  [PXInternalUseOnly]
  public static string GetThemeVariables(string branchCd)
  {
    return string.Join("", PXThemeLoader.GetThemeVariablesDictionary(branchCd).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (kv => $"{kv.Key}:{kv.Value};")).ToArray<string>());
  }

  [PXInternalUseOnly]
  public static Dictionary<string, string> GetThemeVariablesDictionary(string branchCd)
  {
    branchCd = branchCd?.Trim();
    PXThemeLoader.Definition globalDefinition = PXThemeLoader.GlobalDefinition;
    Guid? branchNoteId = new Guid?();
    Guid guid;
    branchNoteId = string.IsNullOrEmpty(branchCd) || !globalDefinition.BranchesNoteIds.TryGetValue(branchCd, out guid) ? new Guid?(globalDefinition.GeneralNoteId) : new Guid?(guid);
    IReadOnlyList<PXThemeLoader.CssVariable> cssVariables = PXThemeLoader.GetCssVariables(globalDefinition.CompanyTheme);
    return PXThemeLoader.CalculateDependantVariables(cssVariables != null ? (IReadOnlyDictionary<string, string>) cssVariables.ToDictionary<PXThemeLoader.CssVariable, string, string>((Func<PXThemeLoader.CssVariable, string>) (v => v.VariableName), (Func<PXThemeLoader.CssVariable, string>) (v => !v.Enabled ? v.DefaultValue : PXThemeLoader.GetVariableValue(branchNoteId, v.VariableName))) : (IReadOnlyDictionary<string, string>) null);
  }

  internal static string GetParentVariableValue(Guid? noteId, string variableName)
  {
    Guid guid;
    return PXThemeLoader.GetVariableValue(!noteId.HasValue || noteId.Value == PXThemeLoader.GlobalDefinition.GeneralNoteId ? new Guid?() : (!PXThemeLoader.GlobalDefinition.BranchesParents.TryGetValue(noteId.Value, out guid) ? new Guid?(PXThemeLoader.GlobalDefinition.GeneralNoteId) : new Guid?(guid)), variableName);
  }

  internal static string GetVariableValue(Guid? noteId, string variableName)
  {
    PXThemeLoader.Definition globalDefinition = PXThemeLoader.GlobalDefinition;
    int? portalId = PortalHelper.GetPortalID();
    Dictionary<int, string> dictionary1;
    string variableValue1;
    if (PortalHelper.IsPortalContext(PortalContexts.Modern) && portalId.HasValue && globalDefinition.PortalThemeVariables.TryGetValue(variableName, out dictionary1) && dictionary1.TryGetValue(portalId.Value, out variableValue1))
      return variableValue1;
    Dictionary<Guid, string> dictionary2;
    if (globalDefinition.ThemeVariables.TryGetValue(variableName, out dictionary2) && noteId.HasValue)
    {
      string variableValue2;
      if (dictionary2.TryGetValue(noteId.Value, out variableValue2))
        return variableValue2;
      Guid key;
      string variableValue3;
      if (globalDefinition.BranchesParents.TryGetValue(noteId.Value, out key) && dictionary2.TryGetValue(key, out variableValue3))
        return variableValue3;
      string variableValue4;
      if (dictionary2.TryGetValue(globalDefinition.GeneralNoteId, out variableValue4))
        return variableValue4;
    }
    return PXThemeLoader.GetCssVariables(globalDefinition.CompanyTheme).FirstOrDefault<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (o => string.Equals(o.VariableName, variableName, StringComparison.OrdinalIgnoreCase)))?.DefaultValue;
  }

  internal static IEnumerable<Guid> GetNotUsedDependantsNoteIds(Guid noteId)
  {
    PXThemeLoader.Definition globalDefinition = PXThemeLoader.GlobalDefinition;
    return (noteId == globalDefinition.GeneralNoteId ? globalDefinition.BranchesParents.SelectMany<KeyValuePair<Guid, Guid>, Guid>((Func<KeyValuePair<Guid, Guid>, IEnumerable<Guid>>) (o => (IEnumerable<Guid>) new Guid[2]
    {
      o.Key,
      o.Value
    })) : globalDefinition.BranchesParents.Where<KeyValuePair<Guid, Guid>>((Func<KeyValuePair<Guid, Guid>, bool>) (o => o.Value == noteId)).Select<KeyValuePair<Guid, Guid>, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (o => o.Key))).Where<Guid>((Func<Guid, bool>) (d => !globalDefinition.ThemeVariables.Values.Any<Dictionary<Guid, string>>((Func<Dictionary<Guid, string>, bool>) (v => v.ContainsKey(d)))));
  }

  private static Dictionary<string, string> CalculateDependantVariables(
    IReadOnlyDictionary<string, string> topVariables)
  {
    if (topVariables == null)
      return new Dictionary<string, string>();
    IReadOnlyList<PXThemeLoader.CssVariable> cssVariables = PXThemeLoader.GetCssVariables(PXThemeLoader.ThemeName);
    Dictionary<string, string> dictionary = EnumerableExtensions.ToDictionary<string, string>(topVariables.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (kv => !string.IsNullOrEmpty(kv.Value))));
    EnumerableExtensions.AddRange<string, string>((IDictionary<string, string>) dictionary, cssVariables.Where<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (x => topVariables.Keys.Contains<string>(x.VariableName))).SelectMany<PXThemeLoader.CssVariable, KeyValuePair<string, string>>((Func<PXThemeLoader.CssVariable, IEnumerable<KeyValuePair<string, string>>>) (x => PXThemeLoader.ComputeVariables((IEnumerable<PXThemeLoader.CssVariable>) x.Dependants, topVariables[x.VariableName]))));
    return dictionary;
  }

  private static IEnumerable<KeyValuePair<string, string>> ComputeVariables(
    IEnumerable<PXThemeLoader.CssVariable> source,
    string parentValue)
  {
    Match match = PXThemeLoader.color.Match(parentValue);
    if (match.Success && source != null)
    {
      string str = match.Groups[1].Value;
      byte[] parts = new byte[3]
      {
        byte.Parse(str.Substring(0, 2), NumberStyles.HexNumber),
        byte.Parse(str.Substring(2, 2), NumberStyles.HexNumber),
        byte.Parse(str.Substring(4, 2), NumberStyles.HexNumber)
      };
      foreach (PXThemeLoader.CssVariable cssVariable in source.Where<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (x => x.Enabled)))
      {
        PXThemeLoader.CssVariable x = cssVariable;
        string computedValue = "#" + string.Join("", ((IEnumerable<byte>) parts).Select<byte, string>((Func<byte, string>) (p => $"{(ValueType) (byte) System.Math.Min(System.Math.Ceiling((double) x.Weight >= 0.0 ? (double) ((int) byte.MaxValue - (int) p) * (double) x.Weight + (double) p : (double) p * (1.0 + (double) x.Weight)), (double) byte.MaxValue):X2}")));
        yield return new KeyValuePair<string, string>(x.VariableName, computedValue);
        PXThemeLoader.CssVariable[] dependants = x.Dependants;
        if ((dependants != null ? (dependants.Length != 0 ? 1 : 0) : 0) != 0)
        {
          foreach (KeyValuePair<string, string> variable in PXThemeLoader.ComputeVariables((IEnumerable<PXThemeLoader.CssVariable>) x.Dependants, computedValue))
            yield return variable;
        }
        computedValue = (string) null;
      }
      parts = (byte[]) null;
    }
  }

  [PXInternalUseOnly]
  public static IEnumerable<string> AvailableThemes
  {
    get => (IEnumerable<string>) PXThemeLoader._cssVariables.Value.Keys;
  }

  internal static IReadOnlyList<PXThemeLoader.CssVariable> GetCssVariables(string theme)
  {
    return !PXThemeLoader._cssVariables.Value.ContainsKey(theme) ? (IReadOnlyList<PXThemeLoader.CssVariable>) Array.Empty<PXThemeLoader.CssVariable>() : PXThemeLoader._cssVariables.Value[theme];
  }

  private static Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>> InitializeThemeInfo()
  {
    Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>> dictionary = new Dictionary<string, IReadOnlyList<PXThemeLoader.CssVariable>>();
    try
    {
      string str = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Themes");
      foreach (DirectoryInfo directory in new DirectoryInfo(str).GetDirectories())
        dictionary[directory.Name] = PXThemeLoader.GetCssVariables(str, directory.Name);
    }
    catch
    {
      dictionary["Default"] = (IReadOnlyList<PXThemeLoader.CssVariable>) Array.Empty<PXThemeLoader.CssVariable>();
    }
    return dictionary;
  }

  private static IReadOnlyList<PXThemeLoader.CssVariable> GetCssVariables(
    string basePath,
    string theme)
  {
    foreach (System.IO.FileInfo file in new DirectoryInfo(Path.Combine(basePath, theme)).GetFiles("*.css"))
    {
      StringBuilder stringBuilder = new StringBuilder();
      using (StreamReader streamReader = new StreamReader(file.FullName))
      {
        bool flag1 = false;
        bool flag2 = false;
        while (!streamReader.EndOfStream)
        {
          string str = streamReader.ReadLine();
          if (!string.IsNullOrEmpty(str) && str.Trim().Equals("CSS THEME CONFIG DO NOT REMOVE START", StringComparison.OrdinalIgnoreCase))
          {
            flag1 = true;
            break;
          }
        }
        if (flag1)
        {
          while (!streamReader.EndOfStream)
          {
            string str = streamReader.ReadLine();
            if (!string.IsNullOrEmpty(str) && str.Trim().Equals("CSS THEME CONFIG DO NOT REMOVE END", StringComparison.OrdinalIgnoreCase))
            {
              flag2 = true;
              break;
            }
            stringBuilder.Append(str);
          }
          if (flag2)
          {
            try
            {
              return (IReadOnlyList<PXThemeLoader.CssVariable>) ((IEnumerable<PXThemeLoader.CssVariable>) JsonConvert.DeserializeObject<PXThemeLoader.CssVariable[]>(stringBuilder.ToString())).Where<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (v => !string.IsNullOrEmpty(v.VariableName))).ToArray<PXThemeLoader.CssVariable>();
            }
            catch
            {
              return (IReadOnlyList<PXThemeLoader.CssVariable>) Array.Empty<PXThemeLoader.CssVariable>();
            }
          }
        }
      }
    }
    return (IReadOnlyList<PXThemeLoader.CssVariable>) Array.Empty<PXThemeLoader.CssVariable>();
  }

  [PXInternalUseOnly]
  public class CssVariable
  {
    [JsonConstructor]
    public CssVariable(
      string variableName,
      string displayName,
      bool enabled,
      string defaultValue,
      float weight,
      PXThemeLoader.CssVariable[] dependants)
    {
      this.VariableName = variableName;
      this.DisplayName = displayName;
      this.Enabled = enabled;
      this.DefaultValue = defaultValue;
      this.Weight = weight;
      this.Dependants = (dependants != null ? ((IEnumerable<PXThemeLoader.CssVariable>) dependants).Where<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (d => d != null)).ToArray<PXThemeLoader.CssVariable>() : (PXThemeLoader.CssVariable[]) null) ?? Array.Empty<PXThemeLoader.CssVariable>();
    }

    public string VariableName { get; }

    public string DisplayName { get; }

    public bool Enabled { get; }

    public string DefaultValue { get; }

    public float Weight { get; }

    public PXThemeLoader.CssVariable[] Dependants { get; }
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public static readonly PXThemeLoader.Definition Default = new PXThemeLoader.Definition();
    private string _companyTheme = nameof (Default);
    private readonly Dictionary<string, Guid> _branchesNoteIds = new Dictionary<string, Guid>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<Guid, Guid> _branchesParents = new Dictionary<Guid, Guid>();
    private readonly Dictionary<string, Dictionary<Guid, string>> _overriddenThemeVariables = new Dictionary<string, Dictionary<Guid, string>>();
    private readonly Dictionary<string, Dictionary<int, string>> _overriddenPortalThemeVariables = new Dictionary<string, Dictionary<int, string>>();

    public string CompanyTheme
    {
      get
      {
        string str = PortalHelper.GetPortalTheme() ?? string.Empty;
        return !PXThemeLoader.AvailableThemes.Contains<string>(str) ? this._companyTheme : str;
      }
      private set => this._companyTheme = value;
    }

    public Guid GeneralNoteId { get; private set; }

    public IReadOnlyDictionary<Guid, Guid> BranchesParents
    {
      get => (IReadOnlyDictionary<Guid, Guid>) this._branchesParents;
    }

    public IReadOnlyDictionary<string, Dictionary<Guid, string>> ThemeVariables
    {
      get => (IReadOnlyDictionary<string, Dictionary<Guid, string>>) this._overriddenThemeVariables;
    }

    public IReadOnlyDictionary<string, Dictionary<int, string>> PortalThemeVariables
    {
      get
      {
        return (IReadOnlyDictionary<string, Dictionary<int, string>>) this._overriddenPortalThemeVariables;
      }
    }

    public IReadOnlyDictionary<string, Guid> BranchesNoteIds
    {
      get => (IReadOnlyDictionary<string, Guid>) this._branchesNoteIds;
    }

    public void Prefetch()
    {
      using (new PXConnectionScope())
      {
        this.PrefetchGeneralSettings();
        this.PrefetchOrganizations();
        this.PrefetchBranches();
        this.PrefetchDependants();
        this.PrefetchPortals();
      }
    }

    private void PrefetchGeneralSettings()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PreferencesGeneral>((PXDataField) new PXDataField<PreferencesGeneral.theme>(), (PXDataField) new PXDataField<PreferencesGeneral.noteID>()))
      {
        if (pxDataRecord != null)
        {
          string path3 = pxDataRecord.GetString(0);
          if (!string.IsNullOrEmpty(this.CompanyTheme))
          {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
              string path = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Themes", path3);
              if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0)
                path3 = "Default";
            }
            this.CompanyTheme = path3;
          }
          this.GeneralNoteId = pxDataRecord.GetGuid(1).Value;
        }
      }
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<ThemeVariables>((PXDataField) new PXDataField<ThemeVariables.variableName>(), (PXDataField) new PXDataField<ThemeVariables.value>(), (PXDataField) new PXDataFieldValue<ThemeVariables.entityNoteID>((object) this.GeneralNoteId), (PXDataField) new PXDataFieldValue<ThemeVariables.theme>((object) this.CompanyTheme)))
      {
        string key = pxDataRecord.GetString(0);
        if (!this._overriddenThemeVariables.ContainsKey(key))
          this._overriddenThemeVariables[key] = new Dictionary<Guid, string>();
        this._overriddenThemeVariables[key][this.GeneralNoteId] = pxDataRecord.GetString(1);
      }
    }

    private void PrefetchOrganizations()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXThemeLoader.Organization>(Yaql.join<ThemeVariables>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXThemeLoader.Organization.noteID>((string) null), Yaql.column<ThemeVariables.entityNoteID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXAliasedDataField<PXThemeLoader.Organization.noteID>(), (PXDataField) new PXAliasedDataField<ThemeVariables.variableName>(), (PXDataField) new PXAliasedDataField<ThemeVariables.value>(), (PXDataField) new PXAliasedDataFieldValue<ThemeVariables.theme>((object) this.CompanyTheme), (PXDataField) new PXAliasedDataFieldValue<PXThemeLoader.Organization.overrideThemeVariables>((object) true)))
      {
        Guid key1 = pxDataRecord.GetGuid(0).Value;
        string key2 = pxDataRecord.GetString(1);
        if (!this._overriddenThemeVariables.ContainsKey(key2))
          this._overriddenThemeVariables[key2] = new Dictionary<Guid, string>();
        this._overriddenThemeVariables[key2][key1] = pxDataRecord.GetString(2);
      }
    }

    private void PrefetchDependants()
    {
      YaqlJoin join1 = Yaql.join<PXThemeLoader.Branch>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXThemeLoader.Branch.bAccountID>((string) null), Yaql.column<PXThemeLoader.BAccount.bAccountID>((string) null)), (YaqlJoinType) 0);
      YaqlJoin join2 = Yaql.join<PXThemeLoader.Organization>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXThemeLoader.Branch.organizationID>((string) null), Yaql.column<PXThemeLoader.Organization.organizationID>((string) null)), (YaqlJoinType) 0);
      PXDataField[] pxDataFieldArray = new PXDataField[5]
      {
        (PXDataField) new PXAliasedDataField<PXThemeLoader.BAccount.noteID>(),
        (PXDataField) new PXAliasedDataField<PXThemeLoader.Branch.branchCD>(),
        (PXDataField) new PXAliasedDataField<PXThemeLoader.Organization.noteID>(),
        null,
        null
      };
      PXAliasedDataFieldValue<PXThemeLoader.Branch.overrideThemeVariables> aliasedDataFieldValue1 = new PXAliasedDataFieldValue<PXThemeLoader.Branch.overrideThemeVariables>((object) true);
      aliasedDataFieldValue1.OpenBrackets = 1;
      pxDataFieldArray[3] = (PXDataField) aliasedDataFieldValue1;
      PXAliasedDataFieldValue<PXThemeLoader.Organization.overrideThemeVariables> aliasedDataFieldValue2 = new PXAliasedDataFieldValue<PXThemeLoader.Organization.overrideThemeVariables>((object) true);
      aliasedDataFieldValue2.OrOperator = true;
      aliasedDataFieldValue2.CloseBrackets = 1;
      pxDataFieldArray[4] = (PXDataField) aliasedDataFieldValue2;
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXThemeLoader.BAccount>(join1, join2, pxDataFieldArray))
      {
        Guid? guid1 = pxDataRecord.GetGuid(0);
        Guid key1 = guid1.Value;
        string key2 = pxDataRecord.GetString(1).Trim();
        guid1 = pxDataRecord.GetGuid(2);
        Guid guid2 = guid1.Value;
        if (!this._branchesParents.ContainsKey(key1) && guid2 != key1)
          this._branchesParents[key1] = guid2;
        if (!this._branchesNoteIds.ContainsKey(key2))
          this._branchesNoteIds[key2] = key1;
      }
    }

    private void PrefetchBranches()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXThemeLoader.BAccount>(Yaql.join<PXThemeLoader.Branch>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXThemeLoader.Branch.bAccountID>((string) null), Yaql.column<PXThemeLoader.BAccount.bAccountID>((string) null)), (YaqlJoinType) 0), Yaql.join<ThemeVariables>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<ThemeVariables.entityNoteID>((string) null), Yaql.column<PXThemeLoader.BAccount.noteID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXAliasedDataField<PXThemeLoader.BAccount.noteID>(), (PXDataField) new PXAliasedDataField<ThemeVariables.variableName>(), (PXDataField) new PXAliasedDataField<ThemeVariables.value>(), (PXDataField) new PXAliasedDataFieldValue<PXThemeLoader.Branch.overrideThemeVariables>((object) true), (PXDataField) new PXAliasedDataFieldValue<ThemeVariables.theme>((object) this.CompanyTheme)))
      {
        Guid key1 = pxDataRecord.GetGuid(0).Value;
        string key2 = pxDataRecord.GetString(1);
        if (!this._overriddenThemeVariables.ContainsKey(key2))
          this._overriddenThemeVariables[key2] = new Dictionary<Guid, string>();
        this._overriddenThemeVariables[key2][key1] = pxDataRecord.GetString(2);
      }
    }

    private void PrefetchPortals()
    {
      List<PortalInfo> portals = PortalHelper.GetPortals();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<SPPortal>(Yaql.join<ThemeVariables>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<ThemeVariables.entityNoteID>((string) null), Yaql.column<SPPortal.noteID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXAliasedDataField<SPPortal.portalID>(), (PXDataField) new PXAliasedDataField<ThemeVariables.variableName>(), (PXDataField) new PXAliasedDataField<ThemeVariables.value>(), (PXDataField) new PXAliasedDataField<ThemeVariables.theme>()))
      {
        int portalID = pxDataRecord.GetInt32(0).Value;
        PortalInfo portalInfo = portals.Where<PortalInfo>((Func<PortalInfo, bool>) (pi =>
        {
          int? id = pi.ID;
          int num = portalID;
          return id.GetValueOrDefault() == num & id.HasValue;
        })).FirstOrDefault<PortalInfo>();
        string key = pxDataRecord.GetString(1);
        string str = pxDataRecord.GetString(2);
        if (!((pxDataRecord.GetString(3) ?? string.Empty) != portalInfo.Theme))
        {
          if (!this._overriddenPortalThemeVariables.ContainsKey(key))
            this._overriddenPortalThemeVariables[key] = new Dictionary<int, string>();
          this._overriddenPortalThemeVariables[key][portalID] = str;
        }
      }
    }
  }

  [PXHidden]
  public class Branch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity]
    public virtual int? BranchID { get; set; }

    [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
    public virtual string BranchCD { get; set; }

    [PXDBBool]
    public bool? OverrideThemeVariables { get; set; }

    [PXDBInt]
    public virtual int? OrganizationID { get; set; }

    [PXDBInt]
    public virtual int? BAccountID { get; set; }

    public abstract class branchID : IBqlField, IBqlOperand
    {
    }

    public abstract class branchCD : IBqlField, IBqlOperand
    {
    }

    public abstract class overrideThemeVariables : IBqlField, IBqlOperand
    {
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXThemeLoader.Branch.organizationID>
    {
    }

    public abstract class bAccountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXThemeLoader.Branch.bAccountID>
    {
    }
  }

  [PXHidden]
  public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    public abstract class bAccountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXThemeLoader.BAccount.bAccountID>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PXThemeLoader.BAccount.noteID>
    {
    }
  }

  [PXHidden]
  public class Organization : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity(IsKey = true)]
    public virtual int? OrganizationID { get; set; }

    [PXDBBool]
    public bool? OverrideThemeVariables { get; set; }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXThemeLoader.Organization.organizationID>
    {
    }

    public abstract class overrideThemeVariables : IBqlField, IBqlOperand
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PXThemeLoader.Organization.noteID>
    {
    }
  }
}
