// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBFeatureAccessProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Common.Context;
using PX.Data.Services;
using PX.Data.Update;
using PX.DbServices;
using PX.DbServices.Model.Entities;
using PX.Licensing;
using PX.Security;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Web.Compilation;
using System.Xml;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDBFeatureAccessProvider : PXDatabaseAccessProvider
{
  public override void Clear()
  {
    PXDatabase.ResetSlot<PXDBFeatureAccessProvider.FeatureDefinition>("FeatureDefinition", PXDBFeatureAccessProvider.FeatureDefinition.Tables);
    PXContext.SetSlot<PXDBFeatureAccessProvider.FeatureDefinition>((PXDBFeatureAccessProvider.FeatureDefinition) null);
  }

  internal override void ResetContextDefinitions()
  {
    PXContext.SetSlot<PXDBFeatureAccessProvider.FeatureDefinition>((PXDBFeatureAccessProvider.FeatureDefinition) null);
  }

  public override byte[] InstallationID
  {
    get
    {
      return PXCriptoHelper.CalculateSHA(PXLicenseHelper.InstallationIdBase + PXDatabase.Provider.SchemaCache.DatabaseName);
    }
  }

  internal override bool IsScreenHiddenByFeature(string screenID)
  {
    return this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions && definitions.ScreensHiddenByFeatures.Contains(screenID);
  }

  protected override PXDatabaseAccessProvider.Definition Definitions
  {
    get
    {
      return (PXDatabaseAccessProvider.Definition) PXContext.GetSlot<PXDBFeatureAccessProvider.FeatureDefinition>() ?? (PXDatabaseAccessProvider.Definition) PXDBFeatureAccessProvider.StringCollectionFeatures.Get(SlotStore.Instance) ?? (PXDatabaseAccessProvider.Definition) PXContext.SetSlot<PXDBFeatureAccessProvider.FeatureDefinition>(PXDatabase.GetSlot<PXDBFeatureAccessProvider.FeatureDefinition, string>("FeatureDefinition", this.pApplicationName, PXDBFeatureAccessProvider.FeatureDefinition.Tables));
    }
  }

  internal Func<ISlotStore, IDisposable> CreateStringCollectionContextInitializer(ISlotStore slots)
  {
    PXDBFeatureAccessProvider.FeatureDefinition fullAccessFeatures = PXDBFeatureAccessProvider.StringCollectionFeatures.Get(slots) ?? this._adjustForFullAccess(PXDatabase.GetSlot<PXDBFeatureAccessProvider.FeatureDefinition, string>("FeatureDefinition", this.pApplicationName, PXDBFeatureAccessProvider.FeatureDefinition.Tables), ServiceLocator.Current.GetInstance<IRoleManagementService>().GetAllRoles());
    return (Func<ISlotStore, IDisposable>) (newSlots => PXDBFeatureAccessProvider.InitializeContextForStringCollection(newSlots, fullAccessFeatures));
  }

  private static IDisposable InitializeContextForStringCollection(
    ISlotStore slots,
    PXDBFeatureAccessProvider.FeatureDefinition fullAccessFeatures)
  {
    ProviderKeySuffixSlot.Set(slots, PXControlPropertiesCollector.COLLECTION_PROVIDER_VALUE);
    PXDBFeatureAccessProvider.StringCollectionFeatures.Set(slots, fullAccessFeatures);
    TypeKeyedOperationExtensions.Set<PXDBFeatureAccessProvider.FeatureDefinition>(slots, (PXDBFeatureAccessProvider.FeatureDefinition) null);
    PXExtensionManager.ResetFeatures(slots);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return Disposable.Create<ISlotStore>(slots, PXDBFeatureAccessProvider.\u003C\u003EO.\u003C0\u003E__CleanContextAfterStringCollection ?? (PXDBFeatureAccessProvider.\u003C\u003EO.\u003C0\u003E__CleanContextAfterStringCollection = new System.Action<ISlotStore>(PXDBFeatureAccessProvider.CleanContextAfterStringCollection)));
  }

  private static void CleanContextAfterStringCollection(ISlotStore slots)
  {
    ProviderKeySuffixSlot.Clear(slots);
    PXDBFeatureAccessProvider.StringCollectionFeatures.Clear(slots);
    TypeKeyedOperationExtensions.Set<PXDBFeatureAccessProvider.FeatureDefinition>(slots, (PXDBFeatureAccessProvider.FeatureDefinition) null);
    PXExtensionManager.ResetFeatures(slots);
  }

  protected virtual PXDBFeatureAccessProvider.FeatureDefinition _adjustForFullAccess(
    PXDBFeatureAccessProvider.FeatureDefinition source,
    string[] roles)
  {
    PXDBFeatureAccessProvider.FeatureDefinition ret = this._createFeatureDefinitionInstance();
    EnumerableExtensions.AddRange<string>((ISet<string>) ret._features, (IEnumerable<string>) source._allFeatures);
    EnumerableExtensions.AddRange<string>((ISet<string>) ret._allFeatures, (IEnumerable<string>) source._allFeatures);
    EnumerableExtensions.AddRange<string>((ISet<string>) ret._featuresSet, (IEnumerable<string>) source._featuresSet);
    ret._Screens = new Dictionary<string, PXAccessProvider.GraphAccess>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    ret._ScreenRoles = new Dictionary<string, Tuple<List<string>, List<string>, List<string>>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    ret._Graphs = new Dictionary<string, PXAccessProvider.GraphAccess>();
    ret._GraphRoles = new Dictionary<string, List<string>>();
    EnumerableExtensions.ForEach<KeyValuePair<string, PXAccessProvider.GraphAccess>>((IEnumerable<KeyValuePair<string, PXAccessProvider.GraphAccess>>) source._Screens, (System.Action<KeyValuePair<string, PXAccessProvider.GraphAccess>>) (_ =>
    {
      PXAccessProvider.GraphAccess ga;
      ret._Screens[_.Key] = ga = new PXAccessProvider.GraphAccess();
      EnumerableExtensions.ForEach<string>((IEnumerable<string>) roles, (System.Action<string>) (__ => ga.Rights[__] = new PXCacheRightsPrioritized(false, PXCacheRights.Delete)));
    }));
    EnumerableExtensions.ForEach<KeyValuePair<string, Tuple<List<string>, List<string>, List<string>>>>((IEnumerable<KeyValuePair<string, Tuple<List<string>, List<string>, List<string>>>>) source._ScreenRoles, (System.Action<KeyValuePair<string, Tuple<List<string>, List<string>, List<string>>>>) (_ => ret._ScreenRoles[_.Key] = new Tuple<List<string>, List<string>, List<string>>(new List<string>((IEnumerable<string>) roles), new List<string>((IEnumerable<string>) roles), new List<string>())));
    return ret;
  }

  protected virtual PXDBFeatureAccessProvider.FeatureDefinition _createFeatureDefinitionInstance()
  {
    return new PXDBFeatureAccessProvider.FeatureDefinition();
  }

  public override string[] FieldClassRestricted
  {
    get
    {
      return !(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) ? (string[]) null : definitions._fieldClass.ToArray<string>();
    }
  }

  public override PXCacheRights GetRights(string fieldClass)
  {
    return !(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) || !definitions._fieldClass.Contains(fieldClass) ? PXCacheRights.Select : PXCacheRights.Denied;
  }

  public override bool IsRoleEnabled(string role)
  {
    if (!(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) || string.IsNullOrWhiteSpace(role))
      return true;
    Dictionary<string, bool> rolesEnabled = definitions.RolesEnabled;
    if (!rolesEnabled.ContainsKey(role))
      return true;
    return rolesEnabled.ContainsKey(role) && rolesEnabled[role];
  }

  public override bool IsStringListValueDisabled(string cacheName, string fieldName, string value)
  {
    if (this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions)
    {
      string stringListValueKey = PXDBFeatureAccessProvider.FeatureDefinition.GetStringListValueKey(cacheName, fieldName, value);
      bool flag;
      if (definitions.StringListValuesEnabled.TryGetValue(stringListValueKey, out flag))
        return !flag;
    }
    return false;
  }

  internal override PXCache.FieldDefaultingDelegate GetDefaultingDelegate(System.Type type)
  {
    PXCache.FieldDefaultingDelegate defaultingDelegate;
    return this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions && definitions.DefaultingDelegates.TryGetValue(type, out defaultingDelegate) ? defaultingDelegate : (PXCache.FieldDefaultingDelegate) null;
  }

  public override bool FeatureInstalled(string feature)
  {
    return this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions && definitions._features.Contains<string>(feature, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
  }

  public override bool FeatureSetInstalled(string featureSet)
  {
    return this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions && definitions._featuresSet.Contains(featureSet);
  }

  public override bool FeatureReadOnly(string feature)
  {
    return this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions && definitions.IsReadOnly.ContainsKey(feature.ToLowerInvariant()) && definitions.IsReadOnly[feature.ToLowerInvariant()];
  }

  public override string GetLocalizationCodeForFeature(string feature)
  {
    return this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions && definitions.Localization.ContainsKey(feature.ToLowerInvariant()) ? definitions.Localization[feature.ToLowerInvariant()] : (string) null;
  }

  public override IEnumerable<string> GetEnabledLocalizations()
  {
    return !(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) ? (IEnumerable<string>) null : definitions.Localization.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (kvp => this.FeatureInstalled(kvp.Key))).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (pair => pair.Value));
  }

  public override bool IsSchedulesEnabled()
  {
    return !(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) || definitions.IsSchedulesEnabled;
  }

  public override bool IsScreenApiEnabled(string screenId)
  {
    return !(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) || !definitions.ApiEnabled.ContainsKey(screenId) || definitions.ApiEnabled[screenId];
  }

  public override bool IsScreenMobileEnabled(string screenId)
  {
    return !(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) || !definitions.MobileEnabled.ContainsKey(screenId) || definitions.MobileEnabled[screenId];
  }

  public override HashSet<string> AllFeatures
  {
    get
    {
      return !(this.Definitions is PXDBFeatureAccessProvider.FeatureDefinition definitions) ? (HashSet<string>) null : definitions._allFeatures;
    }
  }

  private class StringCollectionFeatures
  {
    private StringCollectionFeatures(PXDBFeatureAccessProvider.FeatureDefinition value)
    {
      this.Value = value;
    }

    private PXDBFeatureAccessProvider.FeatureDefinition Value { get; }

    internal static PXDBFeatureAccessProvider.FeatureDefinition Get(ISlotStore slots)
    {
      return TypeKeyedOperationExtensions.Get<PXDBFeatureAccessProvider.StringCollectionFeatures>(slots)?.Value;
    }

    internal static void Set(
      ISlotStore slots,
      PXDBFeatureAccessProvider.FeatureDefinition features)
    {
      TypeKeyedOperationExtensions.Set<PXDBFeatureAccessProvider.StringCollectionFeatures>(slots, new PXDBFeatureAccessProvider.StringCollectionFeatures(features));
    }

    internal static void Clear(ISlotStore slots)
    {
      TypeKeyedOperationExtensions.Remove<PXDBFeatureAccessProvider.StringCollectionFeatures>(slots);
    }
  }

  /// <exclude />
  protected class FeatureDefinition : PXDatabaseAccessProvider.Definition
  {
    protected Dictionary<string, List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>> _definition = new Dictionary<string, List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>>();
    private IFeatureService _featureService;
    private HashSet<string> _existingScreens;
    private new static readonly System.Type[] _tables;
    protected bool PXDBFeatureAccessProviderBypass;
    public HashSet<string> ScreensHiddenByFeatures;
    public HashSet<string> _featuresSet = new HashSet<string>();
    public HashSet<string> _features = new HashSet<string>();
    public HashSet<string> _allFeatures = new HashSet<string>();
    public HashSet<string> _fieldClass = new HashSet<string>();
    internal Dictionary<string, bool> RolesEnabled = new Dictionary<string, bool>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    internal Dictionary<string, bool> IsReadOnly = new Dictionary<string, bool>();
    internal Dictionary<string, bool> StringListValuesEnabled = new Dictionary<string, bool>();
    internal Dictionary<System.Type, PXCache.FieldDefaultingDelegate> DefaultingDelegates = new Dictionary<System.Type, PXCache.FieldDefaultingDelegate>();
    internal Dictionary<string, bool> ApiEnabled = new Dictionary<string, bool>();
    internal Dictionary<string, bool> MobileEnabled = new Dictionary<string, bool>();
    internal Dictionary<string, string> Localization = new Dictionary<string, string>();
    internal bool IsSchedulesEnabled = true;
    protected const string ScheduleScreenId = "SM205020";

    public FeatureDefinition()
    {
      try
      {
        this._featureService = ServiceLocator.Current.GetInstance<IFeatureService>();
      }
      catch
      {
      }
    }

    static FeatureDefinition()
    {
      List<System.Type> typeList = new List<System.Type>((IEnumerable<System.Type>) PXDatabaseAccessProvider.Definition.Tables);
      try
      {
        Stream manifestResourceStream = typeof (PXDBFeatureAccessProvider).Assembly.GetManifestResourceStream("PX.Data.Access.Features.xml");
        if (manifestResourceStream != null)
        {
          XmlReader xmlReader = XmlReader.Create(manifestResourceStream);
          while (xmlReader.Read())
          {
            if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "Features")
            {
              xmlReader.Skip();
            }
            else
            {
              string attribute = xmlReader.GetAttribute("Type");
              if (attribute != null)
              {
                System.Type type = PXBuildManager.GetType(attribute, false);
                if (type != (System.Type) null)
                  typeList.Add(type);
              }
              xmlReader.Skip();
            }
          }
        }
      }
      catch
      {
      }
      PXDBFeatureAccessProvider.FeatureDefinition._tables = typeList.ToArray();
    }

    protected internal new static System.Type[] Tables
    {
      get => PXDBFeatureAccessProvider.FeatureDefinition._tables;
    }

    public override void Prefetch(string pApplicationName)
    {
      base.Prefetch(pApplicationName);
      this._existingScreens = new HashSet<string>();
      bool flag = false;
      try
      {
        flag = PXSiteMap.IsPortal;
      }
      catch
      {
      }
      IEnumerable<PXDataRecord> pxDataRecords;
      if (!flag)
        pxDataRecords = PXDatabase.SelectMulti<PX.SM.SiteMap>(new PXDataField("ScreenID"));
      else
        pxDataRecords = PXDatabase.SelectMulti<PX.SM.PortalMap>(new PXDataField("ScreenID"));
      foreach (PXDataRecord pxDataRecord in pxDataRecords)
      {
        string str = pxDataRecord.GetString(0);
        if (!string.IsNullOrEmpty(str))
          this._existingScreens.Add(str);
      }
      this.PrefetchFrom(typeof (PXDBFeatureAccessProvider).Assembly.GetManifestResourceStream("PX.Data.Access.Features.xml"));
      if (!this._featuresSet.Any<string>())
        return;
      try
      {
        System.Type type = PXBuildManager.GetType(this._featuresSet.FirstOrDefault<string>(), false);
        if (type == (System.Type) null)
          return;
        foreach (System.Type extension in PXDBFeatureAccessProvider.FeatureDefinition.GetExtensions(type))
          this.PrefetchFrom(extension.Assembly.GetManifestResourceStream(extension.Namespace + ".Features.xml"));
      }
      catch (Exception ex)
      {
        PXTrace.WithSourceLocation(nameof (Prefetch), "C:\\build\\code_repo\\NetTools\\PX.Data\\Access\\Access.cs", 1312).Warning<string>("Feature extensions can't be loaded with exception: {Message}", ex.Message);
      }
    }

    protected void PrefetchFrom(Stream source)
    {
      if (this.PXDBFeatureAccessProviderBypass)
        return;
      this.ScreensHiddenByFeatures = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      PXLicense license = LicensingManager.Instance.License;
      HashSet<string> stringSet1 = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
      HashSet<string> stringSet2 = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
      HashSet<string> fieldClass = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
      HashSet<string> stringSet3 = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
      List<string> roles = new List<string>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXDatabaseAccessProvider.Roles>(new PXDataField("Rolename")))
      {
        roles.Add(pxDataRecord.GetString(0));
        this._PrioritizedRoles.Remove(roles[roles.Count - 1]);
      }
      roles.AddRange((IEnumerable<string>) this._PrioritizedRoles);
      Stream stream = source;
      if (stream != null)
      {
        XmlReader reader = this._featureService == null ? XmlReader.Create(stream) : this._featureService.CreateReader(stream);
        if (reader.ReadToDescendant("Features"))
        {
          do
          {
            string typeName = StringExtensions.Intern(reader.GetAttribute("Type"));
            string str = StringExtensions.Intern(reader.GetAttribute("Key"));
            string fieldName1 = StringExtensions.Intern(reader.GetAttribute("CheckValidation"));
            string fieldName2 = StringExtensions.Intern(reader.GetAttribute("Date"));
            bool flag1 = true;
            bool flag2 = true;
            System.Type type1 = PXBuildManager.GetType(typeName, false);
            if (reader.GetAttribute("ApiEnabled") != null)
              flag1 = string.Equals(reader.GetAttribute("ApiEnabled"), "true", StringComparison.OrdinalIgnoreCase);
            if (reader.GetAttribute("MobileEnabled") != null)
              flag2 = string.Equals(reader.GetAttribute("MobileEnabled"), "true", StringComparison.OrdinalIgnoreCase);
            Dictionary<string, List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>> dictionary1 = new Dictionary<string, List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
            if (typeName != null)
            {
              if (reader.ReadToDescendant("Feature"))
              {
                do
                {
                  string key = StringExtensions.Intern(reader.GetAttribute("Name"));
                  bool flag3 = string.Equals(reader.GetAttribute("ReadOnly"), "true", StringComparison.OrdinalIgnoreCase);
                  bool flag4 = flag1;
                  bool flag5 = flag2;
                  string attribute = reader.GetAttribute("Localization");
                  if (reader.GetAttribute("ApiEnabled") != null)
                    flag4 = string.Equals(reader.GetAttribute("ApiEnabled"), "true", StringComparison.OrdinalIgnoreCase);
                  if (reader.GetAttribute("MobileEnabled") != null)
                    flag5 = string.Equals(reader.GetAttribute("MobileEnabled"), "true", StringComparison.OrdinalIgnoreCase);
                  if (key != null)
                  {
                    dictionary1[key] = new List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>();
                    this.AddIsReadOnly(key.ToLowerInvariant(), flag3);
                    if (attribute != null)
                      this.AddLocalization($"{type1.FullName}+{key}".ToLowerInvariant(), attribute);
                  }
                  if (key != null && !reader.IsEmptyElement && reader.ReadToDescendant("Access"))
                  {
                    do
                    {
                      PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict = new PXDBFeatureAccessProvider.FeatureDefinition.Restrict(reader);
                      if (!string.IsNullOrWhiteSpace(restrict.ScreenID) && string.IsNullOrWhiteSpace(restrict.ApiEnabled))
                        restrict.ApiEnabled = flag4.ToString();
                      if (!string.IsNullOrWhiteSpace(restrict.ScreenID) && string.IsNullOrWhiteSpace(restrict.MobileEnabled))
                        restrict.MobileEnabled = flag5.ToString();
                      if (string.IsNullOrWhiteSpace(restrict.Rights) & flag3)
                        restrict.Rights = "readonly";
                      dictionary1[key].Add(restrict);
                    }
                    while (reader.ReadToNextSibling("Access"));
                  }
                }
                while (reader.ReadToNextSibling("Feature"));
              }
              if (!(type1 == (System.Type) null))
              {
                List<PXDataField> pxDataFieldList1 = new List<PXDataField>();
                if (str != null)
                  pxDataFieldList1.Add((PXDataField) new PXDataFieldValue("Status", PXDbType.Int, (object) str));
                if (fieldName1 != null)
                  pxDataFieldList1.Add(new PXDataField(fieldName1));
                if (fieldName2 != null)
                  pxDataFieldList1.Add((PXDataField) new PXDataFieldValue(fieldName2, PXDbType.DateTime, new int?(4), (object) System.DateTime.Today, PXComp.GE));
                HashSet<PXDBFeatureAccessProvider.FeatureDefinition.Restrict> restrictSet1 = new HashSet<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>();
                HashSet<PXDBFeatureAccessProvider.FeatureDefinition.Restrict> restrictSet2 = new HashSet<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>();
                HashSet<PXDBFeatureAccessProvider.FeatureDefinition.Restrict> defaults = new HashSet<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>();
                List<System.Type> extensions = PXDBFeatureAccessProvider.FeatureDefinition.GetExtensions(type1);
                List<string> list = PXDBFeatureAccessProvider.FeatureDefinition.GetTables(type1, extensions).ToList<string>();
                System.Type type2 = type1;
                if (list.Count == 0 && this._featuresSet.Count > 0)
                {
                  type2 = this.GetType(this._featuresSet.First<string>());
                  list.AddRange(PXDBFeatureAccessProvider.FeatureDefinition.GetTables(type2, new List<System.Type>()));
                }
                Dictionary<string, IEnumerable<string>> fieldsByTable = PXDBFeatureAccessProvider.FeatureDefinition.GetFieldsByTable((IEnumerable<string>) list, dictionary1);
                Dictionary<string, bool> dictionary2 = new Dictionary<string, bool>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
                bool flag6 = true;
                foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in fieldsByTable)
                {
                  KeyValuePair<string, IEnumerable<string>> pair = keyValuePair;
                  List<PXDataField> pxDataFieldList2 = new List<PXDataField>();
                  if (str != null)
                    pxDataFieldList2.Add((PXDataField) new PXDataFieldValue("Status", PXDbType.Int, (object) str));
                  pxDataFieldList2.AddRange(pair.Value.Select<string, PXDataField>((Func<string, PXDataField>) (f => new PXDataField(f))));
                  PXDataRecord pxDataRecord = PXDatabase.Provider.SelectSingle(string.Equals(pair.Key, type2.Name, StringComparison.OrdinalIgnoreCase) ? type2 : extensions.FirstOrDefault<System.Type>((Func<System.Type, bool>) (t => string.Equals(t.Name, pair.Key, StringComparison.OrdinalIgnoreCase))), pxDataFieldList2.ToArray());
                  if (pxDataRecord == null)
                  {
                    if (string.Equals(pair.Key, type1.Name, StringComparison.OrdinalIgnoreCase))
                      flag6 = false;
                  }
                  else
                  {
                    using (pxDataRecord)
                    {
                      int i = 0;
                      foreach (string key1 in pair.Value)
                      {
                        if (dictionary1.Keys.Contains<string>(key1, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
                        {
                          Dictionary<string, bool> dictionary3 = !dictionary2.ContainsKey(key1) ? dictionary2 : throw new PXException("Multiple features with the {0} name exist in the system. You should rename the custom features so that their names differ from the name of the in-built feature.", new object[1]
                          {
                            (object) key1
                          });
                          string key2 = key1;
                          bool? boolean = pxDataRecord.GetBoolean(i);
                          bool flag7 = true;
                          int num = boolean.GetValueOrDefault() == flag7 & boolean.HasValue ? 1 : 0;
                          dictionary3.Add(key2, num != 0);
                        }
                        ++i;
                      }
                    }
                  }
                }
                using (PXDataRecord pxDataRecord = PXDatabase.Provider.SelectSingle(type2, pxDataFieldList1.ToArray()))
                {
                  if (flag6)
                    stringSet2.Add(typeName);
                  if (pxDataRecord != null && fieldName1 != null && pxDataRecord.GetString(0) != "1234")
                    stringSet2.Remove(typeName);
                  foreach (KeyValuePair<string, List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>> keyValuePair in dictionary1)
                  {
                    string feature = $"{type1.FullName}+{keyValuePair.Key}";
                    stringSet3.Add(feature);
                    bool active = stringSet2.Contains(typeName) && dictionary2.ContainsKey(keyValuePair.Key) && dictionary2[keyValuePair.Key] && license != null && license.ValidFeature(feature);
                    if (active)
                      stringSet1.Add(feature);
                    foreach (PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict in keyValuePair.Value)
                    {
                      if ((string.IsNullOrEmpty(restrict.ScreenID) || PXPageIndexingService.GetGraphTypeByScreenID(restrict.ScreenID) == null || this._existingScreens.Contains(restrict.ScreenID)) && this.CheckDefinitionAlreadyExists(restrict))
                      {
                        if (!string.IsNullOrWhiteSpace(restrict.Role))
                          this.SetRolesEnabled(restrict, active);
                        else if (!string.IsNullOrWhiteSpace(restrict.StringListValue))
                          this.SetStringListValuesEnabled(restrict, active);
                        else if (!string.IsNullOrWhiteSpace(restrict.DefaultValue))
                        {
                          if (active)
                            defaults.Add(restrict);
                        }
                        else if (!string.IsNullOrWhiteSpace(restrict.ScreenID) || !string.IsNullOrWhiteSpace(restrict.FieldClass))
                        {
                          if (active ^ string.Equals(restrict.Rights, "hide", StringComparison.OrdinalIgnoreCase))
                            restrictSet2.Add(restrict);
                          else
                            restrictSet1.Add(restrict);
                        }
                        if (!string.IsNullOrWhiteSpace(restrict.ScreenID))
                          this.SetApiEnabled(restrict);
                        if (!string.IsNullOrWhiteSpace(restrict.ScreenID))
                          this.SetMobileEnabled(restrict);
                      }
                    }
                  }
                }
                this._definition.AddRange<string, List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>>(dictionary1);
                this.BuildDefaultingDelegates(defaults);
                this.SetRights(roles, fieldClass, (IEnumerable<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>) restrictSet1, (IEnumerable<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>) restrictSet2, PXMemberRights.Denied, PXCacheRights.Denied);
                this.SetRights(roles, fieldClass, restrictSet2.Where<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>((Func<PXDBFeatureAccessProvider.FeatureDefinition.Restrict, bool>) (r => string.Equals(r.Rights, "readonly", StringComparison.OrdinalIgnoreCase))), (IEnumerable<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>) new PXDBFeatureAccessProvider.FeatureDefinition.Restrict[0], PXMemberRights.Visible, PXCacheRights.Select);
              }
            }
          }
          while (reader.ReadToNextSibling("Features"));
        }
      }
      EnumerableExtensions.AddRange<string>((ISet<string>) this._featuresSet, (IEnumerable<string>) stringSet2);
      EnumerableExtensions.AddRange<string>((ISet<string>) this._features, (IEnumerable<string>) stringSet1);
      EnumerableExtensions.AddRange<string>((ISet<string>) this._fieldClass, (IEnumerable<string>) fieldClass);
      EnumerableExtensions.AddRange<string>((ISet<string>) this._allFeatures, (IEnumerable<string>) stringSet3);
    }

    private bool CheckDefinitionAlreadyExists(
      PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict)
    {
      if (this._definition == null)
        return true;
      foreach (List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict> restrictList in this._definition.Values)
      {
        if (restrictList.Exists((Predicate<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>) (s => !string.IsNullOrEmpty(s.Role) && s.Role == restrict.Role)) || restrictList.Exists((Predicate<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>) (s => !string.IsNullOrEmpty(s.CacheName) && !string.IsNullOrEmpty(s.FieldName) && !string.IsNullOrEmpty(s.StringListValue) && s.CacheName == restrict.CacheName && s.FieldName == restrict.FieldName && s.StringListValue == restrict.StringListValue)) || restrictList.Exists((Predicate<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>) (s => !string.IsNullOrEmpty(s.ScreenID) && s.ScreenID == restrict.ScreenID && string.IsNullOrEmpty(s.CacheName) && string.IsNullOrEmpty(restrict.CacheName) && string.IsNullOrEmpty(s.FieldName) && string.IsNullOrEmpty(restrict.FieldName))))
          return false;
      }
      return true;
    }

    protected void SetMobileEnabled(
      PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict)
    {
      bool flag = !string.Equals(restrict.MobileEnabled, "false", StringComparison.OrdinalIgnoreCase) && !string.Equals(restrict.MobileEnabled, "0", StringComparison.OrdinalIgnoreCase);
      if (restrict.ScreenID != null && restrict.ScreenID.Length == 2)
      {
        this.UpdateMobileEnabled(restrict.ScreenID + "000000", flag);
        foreach (string screenId in PXPageIndexingService.GetScreensByModule(restrict.ScreenID))
          this.UpdateMobileEnabled(screenId, flag);
      }
      else
        this.UpdateMobileEnabled(restrict.ScreenID, flag);
    }

    protected void BuildDefaultingDelegates(
      HashSet<PXDBFeatureAccessProvider.FeatureDefinition.Restrict> defaults)
    {
      foreach (IGrouping<string, PXDBFeatureAccessProvider.FeatureDefinition.Restrict> source in defaults.GroupBy<PXDBFeatureAccessProvider.FeatureDefinition.Restrict, string>((Func<PXDBFeatureAccessProvider.FeatureDefinition.Restrict, string>) (d => d.CacheName)))
      {
        System.Type type = PXBuildManager.GetType(source.First<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>().CacheName, false);
        Dictionary<string, string> defaultValues = new Dictionary<string, string>();
        foreach (PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict in (IEnumerable<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>) source)
          defaultValues.Add(restrict.FieldName.ToLower(), restrict.DefaultValue);
        this.DefaultingDelegates.Add(type, (PXCache.FieldDefaultingDelegate) ((PXCache sender, string field, ref object value, bool rowSpecific) =>
        {
          string val;
          if (!defaultValues.TryGetValue(field.ToLower(), out val))
            return false;
          value = sender.ValueFromString(field, val);
          return true;
        }));
      }
    }

    protected void SetStringListValuesEnabled(
      PXDBFeatureAccessProvider.FeatureDefinition.Restrict r,
      bool active)
    {
      string stringListValueKey = PXDBFeatureAccessProvider.FeatureDefinition.GetStringListValueKey(r.CacheName, r.FieldName, r.StringListValue);
      if (!this.StringListValuesEnabled.ContainsKey(stringListValueKey))
        this.StringListValuesEnabled.Add(stringListValueKey, false);
      this.StringListValuesEnabled[stringListValueKey] = this.StringListValuesEnabled[stringListValueKey] || active ^ string.Equals(r.Rights, "hide", StringComparison.OrdinalIgnoreCase);
    }

    protected void SetRolesEnabled(
      PXDBFeatureAccessProvider.FeatureDefinition.Restrict r,
      bool active)
    {
      if (!this.RolesEnabled.ContainsKey(r.Role))
        this.RolesEnabled.Add(r.Role, false);
      this.RolesEnabled[r.Role] = this.RolesEnabled[r.Role] || active ^ string.Equals(r.Rights, "hide", StringComparison.OrdinalIgnoreCase);
    }

    protected static Dictionary<string, IEnumerable<string>> GetFieldsByTable(
      IEnumerable<string> tables,
      Dictionary<string, List<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>> definition)
    {
      return tables.ToDictionary<string, string, IEnumerable<string>>((Func<string, string>) (table => table), (Func<string, IEnumerable<string>>) (table => PXDatabase.Provider.GetTableStructure(table).Columns.Select<TableColumn, string>((Func<TableColumn, string>) (c => ((TableEntityBase) c).Name)).Where<string>((Func<string, bool>) (c => definition.Keys.Contains<string>(c, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)))));
    }

    protected static IEnumerable<string> GetTables(System.Type featureType, List<System.Type> extensions)
    {
      return PXDatabase.Provider.GetTables().Where<string>((Func<string, bool>) (table => string.Equals(featureType.Name, table, StringComparison.OrdinalIgnoreCase) || extensions.Any<System.Type>((Func<System.Type, bool>) (t => string.Equals(t.Name, table, StringComparison.OrdinalIgnoreCase))))).Select<string, string>((Func<string, string>) (table => table.ToLowerInvariant()));
    }

    protected static List<System.Type> GetExtensions(System.Type featureType)
    {
      return PXCache._GetExtensions(featureType, false);
    }

    protected void SetApiEnabled(
      PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict)
    {
      bool flag = !string.Equals(restrict.ApiEnabled, "false", StringComparison.OrdinalIgnoreCase) && !string.Equals(restrict.ApiEnabled, "0", StringComparison.OrdinalIgnoreCase);
      if (restrict.ScreenID != null && restrict.ScreenID.Length == 2)
      {
        this.UpdateApiEnabled(restrict.ScreenID + "000000", flag);
        foreach (string screenId in PXPageIndexingService.GetScreensByModule(restrict.ScreenID))
          this.UpdateApiEnabled(screenId, flag);
      }
      else
        this.UpdateApiEnabled(restrict.ScreenID, flag);
    }

    protected void UpdateMobileEnabled(string screenId, bool value)
    {
      if (!this.MobileEnabled.ContainsKey(screenId))
        this.MobileEnabled.Add(screenId, value);
      this.MobileEnabled[screenId] = this.MobileEnabled[screenId] & value;
    }

    protected void UpdateApiEnabled(string screenId, bool value)
    {
      if (!this.ApiEnabled.ContainsKey(screenId))
        this.ApiEnabled.Add(screenId, value);
      this.ApiEnabled[screenId] = this.ApiEnabled[screenId] & value;
    }

    protected void AddIsReadOnly(string key, bool value)
    {
      if (this.IsReadOnly.ContainsKey(key))
        return;
      this.IsReadOnly.Add(key, value);
    }

    protected void AddLocalization(string key, string value)
    {
      if (this.Localization.ContainsKey(key))
        return;
      this.Localization.Add(key, value);
    }

    public static string GetStringListValueKey(string cacheName, string fieldName, string value)
    {
      return string.Join(".", cacheName, fieldName, value).ToLowerInvariant();
    }

    protected void SetRights(
      List<string> roles,
      HashSet<string> fieldClass,
      IEnumerable<PXDBFeatureAccessProvider.FeatureDefinition.Restrict> restricts,
      IEnumerable<PXDBFeatureAccessProvider.FeatureDefinition.Restrict> forceAllowed,
      PXMemberRights memberRights,
      PXCacheRights cacheRights)
    {
      foreach (PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict1 in restricts.Where<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>((Func<PXDBFeatureAccessProvider.FeatureDefinition.Restrict, bool>) (r =>
      {
        if (!forceAllowed.Contains<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>(r))
          return true;
        return forceAllowed.Contains<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>(r) && r.Operator == "And";
      })))
      {
        if (restrict1.ScreenID != null && restrict1.ScreenID.Length == 2)
        {
          PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict2 = new PXDBFeatureAccessProvider.FeatureDefinition.Restrict(restrict1.ScreenID + "000000", restrict1);
          this.AddRestriction(restrict2, roles, memberRights, cacheRights);
          foreach (string screenID in this.GetScreensByModule(restrict1.ScreenID))
          {
            PXDBFeatureAccessProvider.FeatureDefinition.Restrict def = new PXDBFeatureAccessProvider.FeatureDefinition.Restrict(screenID, restrict2);
            if (!forceAllowed.Contains<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>(def) || forceAllowed.Contains<PXDBFeatureAccessProvider.FeatureDefinition.Restrict>(restrict1) && restrict1.Operator == "And")
              this.AddRestriction(def, roles, memberRights, cacheRights);
          }
        }
        else
          this.AddRestriction(restrict1, roles, memberRights, cacheRights);
        if (!string.IsNullOrEmpty(restrict1.FieldClass))
          fieldClass.Add(restrict1.FieldClass);
      }
    }

    private IEnumerable<string> GetScreensByModule(string screenID)
    {
      return this._existingScreens.Where<string>((Func<string, bool>) (s => s.StartsWith(screenID)));
    }

    protected void AddRestriction(
      PXDBFeatureAccessProvider.FeatureDefinition.Restrict def,
      List<string> roles,
      PXMemberRights memberRights,
      PXCacheRights cacheRights)
    {
      string screenId = def.ScreenID;
      if (screenId == null)
        return;
      if (def.FieldName != null)
        this.SetMemberRights(screenId, def.CacheName, def.FieldName, (IEnumerable<string>) roles, memberRights, true);
      else if (def.ActionName != null)
      {
        string graphTypeByScreenId = PXPageIndexingService.GetGraphTypeByScreenID(screenId);
        if (graphTypeByScreenId == null)
          return;
        PXCacheInfo primaryCache = GraphHelper.GetPrimaryCache(graphTypeByScreenId);
        PXPageIndexingService.GetPrimaryView(graphTypeByScreenId);
        if (primaryCache == null)
          return;
        this.SetMemberRights(screenId, primaryCache.Name, def.ActionName, (IEnumerable<string>) roles, memberRights, true);
      }
      else if (def.CacheName != null)
      {
        this.SetCacheRights(screenId, def.CacheName, (IEnumerable<string>) roles, cacheRights, true);
      }
      else
      {
        if (string.Equals(screenId, "SM205020", StringComparison.OrdinalIgnoreCase))
          this.IsSchedulesEnabled = memberRights != 0;
        this.SetGraphRights(screenId, (IEnumerable<string>) roles, cacheRights, true);
        if (cacheRights != PXCacheRights.Denied)
          return;
        this.ScreensHiddenByFeatures.Add(screenId);
      }
    }

    /// <exclude />
    protected class Restrict
    {
      public const string ReadOnly = "readonly";
      public const string Hide = "hide";
      public const string Show = "show";
      public readonly string FieldClass;
      public readonly string ScreenID;
      public readonly string CacheName;
      public readonly string ActionName;
      public readonly string FieldName;
      public readonly string Role;
      public readonly string StringListValue;
      public readonly string DefaultValue;
      public readonly string Operator;
      public string ApiEnabled;
      public string MobileEnabled;
      public string Rights;

      public Restrict(XmlReader reader)
      {
        this.FieldClass = reader.GetAttribute(nameof (FieldClass));
        this.ScreenID = reader.GetAttribute(nameof (ScreenID));
        this.ActionName = reader.GetAttribute(nameof (ActionName));
        this.CacheName = reader.GetAttribute(nameof (CacheName));
        this.FieldName = reader.GetAttribute(nameof (FieldName));
        this.Rights = reader.GetAttribute(nameof (Rights));
        this.StringListValue = reader.GetAttribute(nameof (StringListValue));
        this.DefaultValue = reader.GetAttribute(nameof (DefaultValue));
        this.Role = reader.GetAttribute(nameof (Role));
        this.ApiEnabled = reader.GetAttribute(nameof (ApiEnabled));
        this.MobileEnabled = reader.GetAttribute(nameof (MobileEnabled));
        this.Operator = reader.GetAttribute(nameof (Operator));
      }

      public Restrict(
        string screenID,
        PXDBFeatureAccessProvider.FeatureDefinition.Restrict template)
      {
        this.FieldClass = template.FieldClass;
        this.ScreenID = screenID;
        this.ActionName = template.ActionName;
        this.CacheName = template.CacheName;
        this.FieldName = template.FieldName;
        this.Role = template.Role;
        this.Rights = template.Rights;
        this.StringListValue = template.StringListValue;
        this.DefaultValue = template.DefaultValue;
        this.ApiEnabled = template.ApiEnabled;
        this.Operator = template.Operator;
      }

      public override int GetHashCode()
      {
        return (this.ScreenID ?? string.Empty).ToLowerInvariant().GetHashCode() + (this.FieldClass ?? string.Empty).ToLowerInvariant().GetHashCode() + (this.ActionName ?? string.Empty).ToLowerInvariant().GetHashCode() + (this.CacheName ?? string.Empty).ToLowerInvariant().GetHashCode() + (this.FieldName ?? string.Empty).ToLowerInvariant().GetHashCode() + (this.StringListValue ?? string.Empty).ToLowerInvariant().GetHashCode() + (this.DefaultValue ?? string.Empty).ToLowerInvariant().GetHashCode() + (this.Role ?? string.Empty).ToLowerInvariant().GetHashCode();
      }

      public override bool Equals(object obj)
      {
        if (!(obj is PXDBFeatureAccessProvider.FeatureDefinition.Restrict restrict))
          return false;
        if (this == restrict)
          return true;
        return PXLocalesProvider.CollationComparer.Compare(this.ScreenID ?? string.Empty, restrict.ScreenID ?? string.Empty) == 0 && PXLocalesProvider.CollationComparer.Compare(this.FieldClass ?? string.Empty, restrict.FieldClass ?? string.Empty) == 0 && PXLocalesProvider.CollationComparer.Compare(this.ActionName ?? string.Empty, restrict.ActionName ?? string.Empty) == 0 && PXLocalesProvider.CollationComparer.Compare(this.CacheName ?? string.Empty, restrict.CacheName ?? string.Empty) == 0 && PXLocalesProvider.CollationComparer.Compare(this.FieldName ?? string.Empty, restrict.FieldName ?? string.Empty) == 0 && PXLocalesProvider.CollationComparer.Compare(this.StringListValue ?? string.Empty, restrict.StringListValue ?? string.Empty) == 0 && PXLocalesProvider.CollationComparer.Compare(this.DefaultValue ?? string.Empty, restrict.DefaultValue ?? string.Empty) == 0 && PXLocalesProvider.CollationComparer.Compare(this.Role ?? string.Empty, restrict.Role ?? string.Empty) == 0;
      }
    }
  }
}
