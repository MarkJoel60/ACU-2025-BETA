// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGenericInqGrph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PX.Api;
using PX.Common;
using PX.Data.Automation.Services;
using PX.Data.BQL;
using PX.Data.BQL.AggregateCalculators;
using PX.Data.Database.ResultSet;
using PX.Data.Description.GI;
using PX.Data.GenericInquiry;
using PX.Data.GenericInquiry.Services;
using PX.Data.Localization;
using PX.Data.Maintenance.GI;
using PX.Data.MassProcess;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using PX.Data.SyncAsyncEnumerableWrapper;
using PX.DbServices.Model.Entities;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

/// <exclude />
/// <exclude />
/// <exclude />
[DashboardType(new int[] {8, 9})]
[PXInternalUseOnly]
public class PXGenericInqGrph : PXGraph<
#nullable disable
PXGenericInqGrph>, ISuppressAggregateValidation
{
  [Obsolete("This constant is obsolete and will be removed in the future versions.")]
  public const int DASHBOARD_TYPE = 8;
  [Obsolete("This constant is obsolete and will be removed in the future versions.")]
  public const int DASHBOARD_CHART_TYPE = 9;
  [PXInternalUseOnly]
  public const string INQUIRY_URL = "~/GenericInquiry/GenericInquiry.aspx";
  [PXInternalUseOnly]
  public const string FilterViewName = "Filter";
  [PXInternalUseOnly]
  public const string FilterContainerName = "Filter_";
  [PXInternalUseOnly]
  public const string PrimaryViewName = "Results";
  [PXInternalUseOnly]
  public const string PrimaryContainerName = "Result";
  internal const string FormulaFieldPrefix = "Formula";
  internal const string CountFieldPrefix = "Count";
  internal const string StringAggFieldPrefix = "StringAgg";
  internal const string AggregationFieldPrefix = "Aggr";
  private const string _DefSlotKey = "GenericInquiry$Descriptions";
  private readonly List<string> _noteFields = new List<string>();
  private readonly List<GIFilter> _filterDefinitions = new List<GIFilter>();
  private readonly List<GIResult> _resultColumns = new List<GIResult>();
  private readonly Dictionary<string, PXGenericInqGrph.GIDescriptionField> _descriptionFields = new Dictionary<string, PXGenericInqGrph.GIDescriptionField>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private readonly Dictionary<string, PXGenericInqGrph.GIDescriptionField> _descriptionFieldsBySourceField = new Dictionary<string, PXGenericInqGrph.GIDescriptionField>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private readonly HashSet<string> _virtualColumns = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  protected readonly OrderedHashSet<string> _initializedColumns = new OrderedHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private readonly HashSet<string> _auxiliaryColumns = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private readonly PXGIFormulaProcessor _formulaProcessor = new PXGIFormulaProcessor();
  private bool _isFilterUpdated;
  private bool? _shouldCachesBeMarkedAsAggregateSelecting;
  private string _noteTableAlias;
  private PXQueryDescription _baseQueryDescription;
  private IDictionary<System.Type, int> _PositionSlotParameters = (IDictionary<System.Type, int>) new Dictionary<System.Type, int>();
  private Dictionary<System.Type, PXGenericInqGrph.DynamicField> _dynamicFields = new Dictionary<System.Type, PXGenericInqGrph.DynamicField>();
  private int _dfsCount;
  private PXGenericInqGrph.DynamicFieldsScope _dynamicFieldsScope;
  private static readonly Regex _CommentSearch = new Regex("(?<begin>(^|,)\\s*(?<name>\\w+)\\s*)(|/\\*(?<value>.*?)\\s*\\*/)((?=,|$|\\)))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
  internal const string NavigateToActionNamePrefix = "NavigateTo$";
  private readonly Dictionary<string, List<PXDBLocalizableStringAttribute>> _formulaFieldsWithLocalizableStringAttributes = new Dictionary<string, List<PXDBLocalizableStringAttribute>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  public PXFilter<GenericFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public GIFilteredProcessing Results;
  public PXCancel<GenericFilter> Cancel;
  public PXSelect<PXGenericInqGrph.GIKeyDefault> AddNewKeys;
  public PXAction<GenericFilter> Insert;
  [PXInternalUseOnly]
  public const string EDITDETAIL_ACTION_NAME = "EditDetail";
  public PXAction<GenericFilter> EditDetail;
  public PXFilter<PXGenericInqGrph.GIUpdateValue> Fields;
  private readonly IDictionary<string, System.Type> _graphTypeByViewNames = (IDictionary<string, System.Type>) new Dictionary<string, System.Type>();

  [InjectDependency]
  private IScreenInfoProvider ScreenInfoProvider { get; set; }

  [InjectDependency]
  private IPXPageIndexingService PageIndexingService { get; set; }

  [InjectDependency]
  internal IGenericInquiryDescriptionProvider DescriptionProvider { get; set; }

  [InjectDependency]
  internal IGenericInquiryReferenceInfoProvider ReferenceInfoProvider { get; set; }

  [InjectDependency]
  private IOptions<GIOptions> Options { get; set; }

  [InjectDependency]
  internal IGIResultViewProcessor GIResultViewProcessor { get; set; }

  [InjectDependency]
  public ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  [PXInternalUseOnly]
  public static PXGenericInqGrph.Definition Def
  {
    get
    {
      return PXContext.GetSlot<PXGenericInqGrph.Definition>("GenericInquiry$Descriptions") ?? PXContext.SetSlot<PXGenericInqGrph.Definition>("GenericInquiry$Descriptions", PXDatabase.GetLocalizableSlot<PXGenericInqGrph.Definition>("GenericInquiry$Descriptions", PXGenericInqGrph.Definition.UsedTables));
    }
  }

  [PXInternalUseOnly]
  public static void ResetDefinitions()
  {
    PXGenericInqGrph.ResetContextDefinitions();
    PXDatabase.ResetLocalizableSlot<PXGenericInqGrph.Definition>("GenericInquiry$Descriptions", PXGenericInqGrph.Definition.UsedTables);
  }

  internal static void ResetContextDefinitions()
  {
    PXContext.SetSlot<PXGenericInqGrph.Definition>("GenericInquiry$Descriptions", (PXGenericInqGrph.Definition) null);
  }

  [PXInternalUseOnly]
  public GIDescription Description { get; private set; }

  private void InitializeScreenInfo()
  {
    if (this.Design == null || this.Design.PrimaryScreenID == null || HttpContext.Current == null || string.Equals(PXContext.GetScreenID()?.Replace(".", ""), this.Design.PrimaryScreenID, StringComparison.OrdinalIgnoreCase))
      return;
    PXSiteMapNode siteMapNode = GIScreenHelper.TryGetSiteMapNode(this.Design.PrimaryScreenID);
    if (siteMapNode == null || siteMapNode.ScreenID == null)
      return;
    using (new PXScreenIDScope(siteMapNode.ScreenID))
    {
      try
      {
        this.ScreenInfoProvider.Get(siteMapNode.ScreenID);
      }
      catch (PXSetupNotEnteredException ex)
      {
        throw new PXRedirectToUrlException(siteMapNode.Url, "PXSetupNotEnteredException");
      }
      catch (Exception ex)
      {
        string str = ex is PXException pxException ? pxException.MessageNoPrefix : ex.Message;
        throw new PXException(ex, "The entry form (ID: {0}, title: {1}) cannot be automated. {2}", new object[3]
        {
          (object) siteMapNode.ScreenID,
          (object) siteMapNode.Title,
          (object) str
        });
      }
    }
  }

  internal string NoteTableAlias => this._noteTableAlias;

  private Dictionary<System.Type, object> ParameterCurrents
  {
    get
    {
      string key = $"{this.GetType().FullName}$ParametersCurrents${this.Design.DesignID.ToString()}";
      return PXContext.Session.IsSessionEnabled ? (Dictionary<System.Type, object>) (PXContext.Session[key] ?? (PXContext.Session[key] = (object) new Dictionary<System.Type, object>())) : PXContext.GetSlot<Dictionary<System.Type, object>>(key) ?? PXContext.SetSlot<Dictionary<System.Type, object>>(key, new Dictionary<System.Type, object>());
    }
  }

  public PXGenericInqGrph()
  {
    this.Results.Cache.AllowInsert = false;
    this.Results.Cache.AllowUpdate = true;
    this.Results.Cache.AllowDelete = false;
    typeof (FieldValue).GetCustomAttributes(typeof (PXVirtualAttribute), false);
    this.Columns = this._initializedColumns.AsReadOnly();
    PXNamedAction.AddHiddenAction((PXGraph) this, typeof (GenericFilter), "SyncGridPosition", (PXButtonDelegate) (a =>
    {
      this.EditCurrent = (GenericResult) null;
      return a.Get();
    }));
  }

  public override bool IsProcessing
  {
    get => false;
    set
    {
    }
  }

  public GIDesign Design => this.Description != null ? this.Description.Design : (GIDesign) null;

  protected override PXCacheCollection CreateCacheCollection()
  {
    return (PXCacheCollection) new GICacheCollection(this);
  }

  public List<string> NoteFields => this._noteFields;

  public List<GIFilter> FilterDefinitions => this._filterDefinitions;

  public List<GIResult> ResultColumns => this._resultColumns;

  public HashSet<string> VisibleColumns { get; set; }

  internal BqlGenericCommand LastCommand { get; set; }

  internal bool IsSubQueryInstance { get; private set; }

  internal bool HasSubQueries
  {
    get
    {
      GIDescription description = this.Description;
      if (description == null)
        return false;
      IEnumerable<GITable> tables = description.Tables;
      bool? nullable = tables != null ? new bool?(tables.Any<GITable>((Func<GITable, bool>) (x =>
      {
        int? type = x.Type;
        int num = 1;
        return type.GetValueOrDefault() == num & type.HasValue;
      }))) : new bool?();
      bool flag = true;
      return nullable.GetValueOrDefault() == flag & nullable.HasValue;
    }
  }

  public PXQueryDescription BaseQueryDescription
  {
    get
    {
      if (this._baseQueryDescription == null)
      {
        if (this.Design == null)
          throw new PXException("A query description cannot be created: A design ID or a name is not specified. Call the PrepareCaches method and pass to it a valid ID or name.");
        this._baseQueryDescription = this.CreateQueryDescription();
      }
      return this._baseQueryDescription;
    }
  }

  public IReadOnlySet<string> Columns { get; }

  protected virtual PXQueryDescription CreateQueryDescription()
  {
    return PXQueryDescription.Create((IGenericQueryProvider) new PXDesignedQueryProvider((PXGraph) this, this.Description));
  }

  public void PrepareCaches(string id, string name, bool fetchSchema = true)
  {
    this.PrepareCaches(id, name, (Dictionary<string, string>) null, fetchSchema);
  }

  public void PrepareCaches(
    string id,
    string name,
    Dictionary<string, string> parameters,
    bool fetchSchema = true)
  {
    Guid? nullable = new Guid?();
    if (!string.IsNullOrEmpty(id))
      nullable = GUID.CreateGuid(id);
    if (!nullable.HasValue && string.IsNullOrEmpty(name))
      return;
    if (fetchSchema)
      this.SelectTimeStamp();
    GIDescription def = (GIDescription) ((nullable.HasValue ? this.DescriptionProvider.Get(nullable.Value) : this.DescriptionProvider.GetByName(name)) ?? throw new PXException("This generic inquiry does not exist anymore.")).Clone();
    def.Design = (GIDesign) this.Caches[typeof (GIDesign)].CreateCopy((object) def.Design);
    this.PrepareCaches(def, parameters);
  }

  public void PrepareCaches(GIDescription def, Dictionary<string, string> parameters = null)
  {
    string screenID = PXContext.GetScreenID()?.Replace(".", "");
    PXSiteMapNode siteMapNode;
    if (screenID != "00000000" && (siteMapNode = GIScreenHelper.TryGetSiteMapNode(screenID)) != null)
      def.Design.SitemapTitle = siteMapNode.Title;
    this.Description = def;
    this.GenerateFilterFields(this, string.Empty);
    bool? nullable = def.Design.ShowDeletedRecords;
    if (nullable.GetValueOrDefault())
    {
      this.GenerateDeletedDatabaseRecordFields();
      this.DynamicFieldsCacheAttached();
    }
    nullable = def.Design.ShowArchivedRecords;
    if (nullable.GetValueOrDefault())
      this.GenerateDatabaseRecordStatusFields();
    using (new PXGenericInqGrph.DynamicFieldsScope(this))
    {
      this.CollectDescriptionFields();
      if (parameters != null && parameters.Count > 0)
      {
        foreach (KeyValuePair<string, string> parameter in parameters)
        {
          if (this.Filter.Cache.Fields.Contains(parameter.Key))
            this.Filter.Cache.SetValueExt((object) this.Filter.Current, parameter.Key, (object) parameter.Value);
        }
      }
      this.GenerateResultsFields();
      this.InitializePrimaryScreenActions();
      this.AddAdditionalCacheFields();
      EnumerableExtensions.AddRange<string>((ISet<string>) this._virtualColumns, (IEnumerable<string>) this.Results.Cache.PlainDacFields);
      this.InitializeLayeredNavigationActions();
    }
  }

  public override void Load()
  {
    GraphSessionStatePrefix sessionStatePrefix = GraphSessionStatePrefix.For((PXGraph) this);
    if (this.Design == null)
    {
      Guid? nullable = PXContext.Session.GenericInquiryDesign[sessionStatePrefix.GetSubKey("DesignID")];
      if (nullable.HasValue)
        this.PrepareCaches(nullable.ToString(), (string) null);
    }
    base.Load();
    if (HttpContext.Current == null || !PXContext.Session.IsSessionEnabled)
      return;
    string subKey = sessionStatePrefix.GetSubKey("isFilterUpdated");
    this._isFilterUpdated = PXContext.Session.isFilterUpdated[subKey].GetValueOrDefault();
  }

  public override void Unload()
  {
    base.Unload();
    if (HttpContext.Current == null || !PXContext.Session.IsSessionEnabled)
      return;
    GraphSessionStatePrefix sessionStatePrefix = GraphSessionStatePrefix.For((PXGraph) this);
    string subKey = sessionStatePrefix.GetSubKey("isFilterUpdated");
    PXContext.Session.isFilterUpdated[subKey] = new bool?(this._isFilterUpdated);
    if (this.Design == null || !this.Design.DesignID.HasValue)
      return;
    PXContext.Session.GenericInquiryDesign[sessionStatePrefix.GetSubKey("DesignID")] = this.Design.DesignID;
  }

  public override void Clear(PXClearOption option)
  {
    base.Clear(option);
    foreach (KeyValuePair<GIResult, Tuple<string, PXFieldSelecting>> cleanableField in (IEnumerable<KeyValuePair<GIResult, Tuple<string, PXFieldSelecting>>>) this.CleanableFields)
    {
      string field = cleanableField.Value.Item1;
      this.FieldSelecting.RemoveHandler(this.Results.Name, field, cleanableField.Value.Item2);
      this._resultColumns.Remove(cleanableField.Key);
      this._initializedColumns.Remove(field);
      this.Results.Cache.Fields.Remove(field);
      this.Results.View.RestrictedFields.Remove(new RestrictedField(typeof (GenericResult), field));
    }
    this.CleanableFields.Clear();
    this._isFilterUpdated = false;
    this.ClearInstantiatedSubQueryInstances(option);
  }

  private void ClearInstantiatedSubQueryInstances(PXClearOption option)
  {
    PXQueryDescription queryDescription = this._baseQueryDescription;
    object obj;
    if (queryDescription == null)
    {
      obj = (object) null;
    }
    else
    {
      Dictionary<string, PXTable> tables = queryDescription.Tables;
      obj = tables != null ? (object) tables.Values.AsEnumerable<PXTable>() : (object) null;
    }
    if (obj == null)
      obj = (object) Array<PXTable>.Empty;
    foreach (PXTable pxTable in (IEnumerable<PXTable>) obj)
      pxTable.Graph?.Clear(option);
  }

  public override bool IsDirty => false;

  private static void SwapItems(Array array, int i, int j)
  {
    if (array == null || i >= array.Length || j >= array.Length)
      return;
    object obj = array.GetValue(j);
    array.SetValue(array.GetValue(i), j);
    array.SetValue(obj, i);
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    using (new PXGenericInqGrph.DynamicFieldsScope(this))
    {
      filters = filters != null ? ((IEnumerable<PXFilterRow>) filters).Select<PXFilterRow, PXFilterRow>((Func<PXFilterRow, PXFilterRow>) (f => (PXFilterRow) f.Clone())).ToArray<PXFilterRow>() : (PXFilterRow[]) null;
      if (!viewName.StartsWith("Parameters$"))
        return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
      using (new ReplaceCurrentScope(this.ParameterCurrents.Select<KeyValuePair<System.Type, object>, KeyValuePair<PXCache, object>>((Func<KeyValuePair<System.Type, object>, KeyValuePair<PXCache, object>>) (i => new KeyValuePair<PXCache, object>(this.Caches[i.Key], i.Value)))))
        return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    }
  }

  public override int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (!(viewName == this.Results.Name))
      return base.ExecuteUpdate(viewName, keys, values, parameters);
    GenericResult data = this.Search(keys);
    if (this.Results.Cache.Locate(data) == null)
      this.Results.Cache.PlaceNotChanged((object) data);
    return this.Results.Cache.Update(keys, values);
  }

  public override string[] GetKeyNames(string viewName)
  {
    return viewName == this.Results.Name ? this.Caches[typeof (GenericResult)].Keys.ToArray() : base.GetKeyNames(viewName);
  }

  public override string PrimaryView => this.Filter?.Name ?? "Filter";

  internal GenericResult Search(IDictionary keys)
  {
    List<string> stringList = new List<string>();
    List<object> objectList = new List<object>();
    foreach (string key in (IEnumerable<string>) this.Results.Cache.Keys)
    {
      if (keys.Contains((object) key))
      {
        stringList.Add(key);
        objectList.Add(keys[(object) key]);
      }
    }
    bool[] descendings = new bool[stringList.Count];
    int startRow = 0;
    int totalRows = 0;
    return (GenericResult) this.Results.View.Select((object[]) null, (object[]) null, objectList.ToArray(), stringList.ToArray(), descendings, (PXFilterRow[]) null, ref startRow, 1, ref totalRows).FirstOrDefault<object>();
  }

  internal GenericResult Search(GenericResult item)
  {
    return this.Search((IDictionary) this.Results.Cache.Keys.ToDictionary<string, string, object>((Func<string, string>) (k => k), (Func<string, object>) (k => PXFieldState.UnwrapValue(this.Results.Cache.GetValueExt((object) item, k)))));
  }

  private void GenerateFilterFields(PXGenericInqGrph graphToAddFilters, string prefix)
  {
    Dictionary<string, GITable> tables = new Dictionary<string, GITable>();
    Dictionary<string, PXTable> dictionary = new Dictionary<string, PXTable>();
    foreach (GITable table in this.Description.Tables)
    {
      int? type1 = table.Type;
      int num = 1;
      if (type1.GetValueOrDefault() == num & type1.HasValue)
      {
        PXTable pxTable;
        if (this.BaseQueryDescription.Tables.TryGetValue(table.Alias, out pxTable) && pxTable.Graph != null)
          dictionary[table.Alias] = pxTable;
      }
      else
      {
        type1 = table.Type;
        if (!type1.HasValue || type1.GetValueOrDefault() == 0)
        {
          System.Type type2 = PXBuildManager.GetType(table.Name, true);
          this.ActivateDynamicFields(graphToAddFilters.Caches[type2]);
        }
      }
      tables[table.Alias] = table;
    }
    PXCache cach = graphToAddFilters.Caches[typeof (CheckboxCombobox)];
    foreach (GIFilter filter in this.Description.Filters)
    {
      GIFilter giFilter = filter;
      System.Type fieldFromName = PXGenericInqGrph.GetFieldFromName(this, giFilter.FieldName, (IDictionary<string, GITable>) tables);
      string str = (string) null;
      System.Type cacheType = (System.Type) null;
      if (fieldFromName == (System.Type) null)
      {
        if (giFilter.FieldName != null)
        {
          int length = giFilter.FieldName.IndexOf('.');
          if (length > 0 && length < giFilter.FieldName.Length - 1)
          {
            string key = giFilter.FieldName.Substring(0, length);
            GITable giTable;
            bool flag = tables.TryGetValue(key, out giTable);
            if (flag)
            {
              int? type = giTable.Type;
              flag = !type.HasValue || type.GetValueOrDefault() == 0;
            }
            if (flag)
            {
              cacheType = PXBuildManager.GetType(giTable.Name, true);
              str = giFilter.FieldName.Substring(length + 1);
            }
          }
        }
        if (string.IsNullOrEmpty(str))
          throw new PXException("Invalid field: '{0}'", new object[1]
          {
            (object) giFilter.FieldName
          });
      }
      else
      {
        str = fieldFromName.Name;
        cacheType = BqlCommand.GetItemType(fieldFromName);
        if (fieldFromName.GetInterface(typeof (IBqlField).Name) == (System.Type) null || cacheType == (System.Type) null || cacheType.GetInterface(typeof (IBqlTable).Name) == (System.Type) null)
          throw new PXException("The field or table '{0}' in the inquiry design is invalid.", new object[1]
          {
            (object) giFilter.FieldName
          });
      }
      if (!string.IsNullOrEmpty(prefix))
      {
        giFilter = Clone<GIFilter>(giFilter);
        giFilter.Name = prefix + giFilter.Name;
      }
      string parName = giFilter.Name;
      string availableValues = giFilter.AvailableValues;
      PXCache cache = graphToAddFilters.Caches[cacheType];
      PXFieldState state = cache.GetStateExt((object) null, str) as PXFieldState;
      if (state == null)
        throw new PXException("The field or table '{0}' in the inquiry design is invalid.", new object[1]
        {
          (object) cacheType.Name
        });
      state.Enabled = true;
      state.IsReadOnly = false;
      state.DisplayName = this.ResolveFieldDisplayName(str, state.DisplayName, cache, giFilter.DisplayName);
      if (!string.IsNullOrEmpty(state.ViewName) && !state.ViewName.StartsWith("Parameters$"))
      {
        string key = "Parameters$" + state.ViewName;
        graphToAddFilters.Views[key] = graphToAddFilters.Views[state.ViewName];
        state.ViewName = key;
      }
      if (!graphToAddFilters.Filter.Cache.Fields.Contains(parName))
      {
        bool? required = giFilter.Required;
        int num1;
        if (graphToAddFilters == this)
        {
          bool? hidden1 = giFilter.Hidden;
          bool flag = true;
          num1 = hidden1.GetValueOrDefault() == flag & hidden1.HasValue ? 1 : 0;
        }
        else
          num1 = 1;
        bool? hidden = new bool?(num1 != 0);
        string defaultValue = giFilter.DefaultValue;
        if (string.IsNullOrEmpty(defaultValue))
          defaultValue = (string) null;
        graphToAddFilters.Filter.Cache.Fields.Add(parName);
        graphToAddFilters._filterDefinitions.Add(giFilter);
        this.EnsureCacheInitialised((PXGraph) graphToAddFilters, cacheType);
        this._PositionSlotParameters[cacheType] = cache.SetupSlot<GenericFilter>((Func<GenericFilter>) (() => new GenericFilter()), (Func<GenericFilter, GenericFilter, GenericFilter>) ((copyTo, copyFrom) =>
        {
          foreach (string key in (IEnumerable<string>) copyFrom.Values.Keys)
            copyTo.Values[key] = copyFrom.Values[key];
          return copyTo;
        }), (Func<GenericFilter, GenericFilter>) (item => new GenericFilter()
        {
          Values = (IDictionary<string, object>) item.Values.ToDictionary<KeyValuePair<string, object>, string, object>((Func<KeyValuePair<string, object>, string>) (param => param.Key), (Func<KeyValuePair<string, object>, object>) (param => param.Value))
        }));
        object defaultValueObject = (object) defaultValue;
        cache.RaiseFieldUpdating(str, (object) null, ref defaultValueObject);
        bool forceSetDefault = false;
        if (defaultValueObject == null && cache.RaiseFieldDefaulting(str, (object) null, out defaultValueObject))
          cache.RaiseFieldUpdating(str, (object) null, ref defaultValueObject);
        if (defaultValueObject != null && cache.Current != null && cache.GetValue(cache.Current, str) == null)
          cache.SetValue(cache.Current, str, defaultValueObject);
        else if (defaultValue != null)
          forceSetDefault = true;
        graphToAddFilters.FieldSelecting.AddHandler("Filter", parName, (PXFieldSelecting) ((sender, selE) =>
        {
          this.EnsureCacheInitialised((PXGraph) this, cacheType);
          PXFieldState pxFieldState = (PXFieldState) ((ICloneable) state).Clone();
          object obj1;
          if (!this.ParameterCurrents.TryGetValue(cacheType, out obj1))
            return;
          GenericFilter genericFilter = selE.Row as GenericFilter;
          int num2 = genericFilter == null ? 1 : 0;
          if (num2 != 0)
            genericFilter = graphToAddFilters.Filter.Current;
          bool flag = true;
          object returnValue1 = selE.ReturnValue;
          pxFieldState.Visible = !hidden.GetValueOrDefault();
          pxFieldState.Required = required;
          pxFieldState.PrimaryKey = false;
          selE.ReturnState = (object) pxFieldState;
          PXGenericInqGrph.TrySetAvailableValues(selE.ReturnState, availableValues);
          int positionSlotParameter = this._PositionSlotParameters[cacheType];
          GenericFilter slot = cache.GetSlot<GenericFilter>(obj1, positionSlotParameter);
          if (slot == null)
          {
            slot = new GenericFilter();
            slot.Values = (IDictionary<string, object>) new Dictionary<string, object>();
            cache.SetSlot<GenericFilter>(obj1, positionSlotParameter, slot);
          }
          IDictionary<string, object> values = slot.Values;
          if (!genericFilter.Values.ContainsKey(parName))
          {
            object obj2;
            if (values.TryGetValue(parName, out obj2) && obj2 != null)
            {
              genericFilter.Values[parName] = obj2;
            }
            else
            {
              genericFilter.Values[parName] = defaultValueObject;
              values[parName] = genericFilter.Values[parName];
              cache.SetSlot<GenericFilter>(obj1, positionSlotParameter, slot);
            }
            flag = false;
          }
          else
          {
            values[parName] = genericFilter.Values[parName];
            cache.SetSlot<GenericFilter>(obj1, positionSlotParameter, slot);
          }
          object data = (object) null;
          if (num2 != 0)
            data = cache.CreateCopy(obj1);
          object returnValue2 = returnValue1 ?? genericFilter.Values[parName];
          cache.RaiseFieldSelecting(pxFieldState.Name, (object) null, ref returnValue2, false);
          object obj3 = PXFieldState.UnwrapValue(returnValue2);
          ((PXFieldState) selE.ReturnState).Value = obj3;
          if (graphToAddFilters._isFilterUpdated || !(obj3 == null | forceSetDefault) || defaultValue == null || flag || returnValue1 != null)
            return;
          string a = defaultValue;
          if (!string.IsNullOrEmpty(a) && a.Trim().StartsWith("=") && a.Trim().Length > 1)
          {
            a = a.Trim().Substring(1).Trim();
            if (string.Equals(a, "null", StringComparison.OrdinalIgnoreCase))
              pxFieldState.DefaultValue = (object) (a = (string) null);
          }
          object stateOrValue = (object) a;
          if (!RelativeDatesManager.IsRelativeDatesString(stateOrValue as string))
          {
            cache.RaiseFieldUpdating(pxFieldState.Name, (object) null, ref stateOrValue);
            genericFilter.Values[parName] = stateOrValue ?? pxFieldState.DefaultValue;
            cache.SetValue(data, pxFieldState.Name, stateOrValue);
            cache.RaiseFieldSelecting(pxFieldState.Name, (object) null, ref stateOrValue, false);
            stateOrValue = PXFieldState.UnwrapValue(stateOrValue) ?? pxFieldState.DefaultValue;
          }
          else
          {
            object asString = (object) RelativeDatesManager.EvaluateAsString(stateOrValue as string);
            cache.RaiseFieldUpdating(pxFieldState.Name, (object) null, ref asString);
            genericFilter.Values[parName] = stateOrValue;
            cache.SetValue(data, pxFieldState.Name, asString);
            cache.RaiseFieldSelecting(pxFieldState.Name, (object) null, ref asString, false);
          }
          ((PXFieldState) selE.ReturnState).Value = stateOrValue;
        }));
        graphToAddFilters.FieldUpdating.AddHandler("Filter", parName, (PXFieldUpdating) ((sender, updE) =>
        {
          this.EnsureCacheInitialised((PXGraph) this, cacheType);
          object copy;
          if (!this.ParameterCurrents.TryGetValue(cacheType, out copy))
            return;
          if (updE.NewValue is "")
            updE.NewValue = (object) null;
          GenericFilter genericFilter = (GenericFilter) updE.Row ?? this.Filter.Current;
          if (updE.Row != this.Filter.Current)
            copy = cache.CreateCopy(copy);
          object newValue = updE.NewValue;
          cache.RaiseFieldUpdating(state.Name, copy, ref newValue);
          cache.SetValue(copy, state.Name, newValue);
          object obj = newValue ?? updE.NewValue;
          genericFilter.Values[parName] = obj;
          updE.NewValue = obj;
          cache.IsDirty = false;
          graphToAddFilters._isFilterUpdated = true;
        }));
        graphToAddFilters.FieldDefaulting.AddHandler("Filter", parName, (PXFieldDefaulting) ((sender, defE) =>
        {
          GenericFilter row = (GenericFilter) defE.Row;
          object newValue;
          if (defaultValue != null)
          {
            string a = defaultValue;
            if (!string.IsNullOrEmpty(a) && a.Trim().StartsWith("=") && a.Trim().Length > 1)
            {
              a = a.Trim().Substring(1).Trim();
              if (string.Equals(a, "null", StringComparison.OrdinalIgnoreCase))
                a = (string) null;
            }
            newValue = (object) a;
          }
          else
            cache.RaiseFieldDefaulting(state.Name, (object) null, out newValue);
          defE.NewValue = newValue;
          if (row?.Values == null)
            return;
          cache.RaiseFieldUpdating(state.Name, (object) null, ref newValue);
          row.Values[parName] = newValue;
        }));
        if (state.DescriptionName != null)
          graphToAddFilters.FieldSelecting.AddHandler("Filter", parName + "_description", (PXFieldSelecting) ((sender, selE) =>
          {
            this.EnsureCacheInitialised((PXGraph) this, cacheType);
            object copy;
            if (!this.ParameterCurrents.TryGetValue(cacheType, out copy))
              return;
            GenericFilter genericFilter = selE.Row as GenericFilter;
            int num3 = genericFilter == null ? 1 : 0;
            if (num3 != 0)
              genericFilter = graphToAddFilters.Filter.Current;
            object returnValue3 = selE.ReturnValue;
            if (!genericFilter.Values.ContainsKey(parName))
              genericFilter.Values[parName] = cache.GetValue(copy, state.Name);
            if (num3 != 0)
            {
              copy = cache.CreateCopy(copy);
              cache.SetValue(copy, state.Name, returnValue3);
            }
            object returnValue4 = returnValue3 ?? genericFilter.Values[parName];
            cache.RaiseFieldSelecting(state.Name + "_description", copy, ref returnValue4, true);
            selE.ReturnState = returnValue4;
          }));
      }
    }
    foreach (KeyValuePair<string, PXTable> keyValuePair in dictionary)
    {
      string str;
      PXTable pxTable;
      EnumerableExtensions.Deconstruct<string, PXTable>(keyValuePair, ref str, ref pxTable);
      string alias = str;
      pxTable.Graph.GenerateFilterFields(graphToAddFilters, GetSubGIPrefix(alias));
    }

    static T Clone<T>(T value) => ((JToken) JObject.FromObject((object) value)).ToObject<T>();

    string GetSubGIPrefix(string alias) => $"{prefix}{alias}_";
  }

  private void GenerateResultsFields()
  {
    this.CollectVirtualFieldsInFormulas();
    this.CollectLocalizeStringAttributesInFormulas();
    this.GenerateNoteFields(this.Design.NotesAndFilesTable);
    bool groupingActivated = this.Description.GroupBys.Any<GIGroupBy>();
    Dictionary<string, (System.Type, PXCache)> cachesByTypeName = new Dictionary<string, (System.Type, PXCache)>();
    foreach ((GITable, GIResult) tuple in this.Description.Results.Join<GIResult, GITable, string, (GITable, GIResult)>(this.Description.Tables, (Func<GIResult, string>) (result => result.ObjectName), (Func<GITable, string>) (table => table.Alias), (Func<GIResult, GITable, (GITable, GIResult)>) ((result, table) => (table, result))))
    {
      GITable giTable = tuple.Item1;
      (System.Type cacheType, PXCache cache) = this.GetCache(giTable, cachesByTypeName);
      this.GenerateResultField(tuple.Item2, groupingActivated, giTable, cacheType, cache);
      if (string.IsNullOrEmpty(this.Design.NotesAndFilesTable))
        this.GenerateNoteFields(cache, giTable.Alias);
    }
    foreach ((GITable, GIResult) tuple in this.Description.Results.Join(this.Description.Tables, (Func<GIResult, string>) (result => result.ObjectName), (Func<GITable, string>) (table => table.Alias), (result, table) => new
    {
      result = result,
      table = table
    }).Where(_param1 => _param1.result.TotalAggregateFunction == "COUNT" || _param1.result.AggregateFunction == "COUNT").Select(_param1 => (_param1.table, _param1.result)))
    {
      if (tuple.Item2.TotalAggregateFunction == "COUNT" && (!groupingActivated || !(tuple.Item2.AggregateFunction == "COUNT")))
      {
        GITable giTable = tuple.Item1;
        (System.Type cacheType, PXCache cache) = this.GetCache(giTable, cachesByTypeName);
        this.GenerateResultField(tuple.Item2, groupingActivated, giTable, cacheType, cache, forceCount: true);
      }
    }
  }

  private (System.Type cacheType, PXCache cache) GetCache(
    GITable giTable,
    Dictionary<string, (System.Type, PXCache)> cachesByTypeName)
  {
    string name = giTable.Name;
    (System.Type, PXCache) cache;
    if (!cachesByTypeName.TryGetValue(name, out cache))
    {
      int? type1 = giTable.Type;
      int num1 = 1;
      System.Type key = type1.GetValueOrDefault() == num1 & type1.HasValue ? typeof (GenericResult) : PXBuildManager.GetType(name, true);
      int? type2 = giTable.Type;
      int num2 = 1;
      PXCache cach = (type2.GetValueOrDefault() == num2 & type2.HasValue ? (PXGraph) this.BaseQueryDescription.Tables[giTable.Alias].Graph : (PXGraph) this).Caches[key];
      PXGenericInqGrph.DisableUIAttributes(cach);
      cache = (key, cach);
      cachesByTypeName[name] = cache;
    }
    return cache;
  }

  [PXInternalUseOnly]
  public void GenerateResultField(
    GIResult gir,
    bool groupingActivated,
    GITable git,
    System.Type cacheType,
    PXCache cache,
    bool cleanable = false,
    bool forceCount = false)
  {
    if (string.IsNullOrEmpty(gir.Field))
      throw new PXException("The field or table '{0}' in the inquiry design is invalid.");
    bool isCountField = gir.Field == "$<Count>";
    bool isCountAggregate = !isCountField && ((!groupingActivated ? 0 : (gir.AggregateFunction == "COUNT" ? 1 : 0)) | (forceCount ? 1 : 0)) != 0;
    bool isStringAgg = groupingActivated && gir.AggregateFunction == "STRINGAGG";
    bool isFormula = gir.Field.StartsWith("=");
    int? type = git.Type;
    int num = 1;
    bool flag1 = type.GetValueOrDefault() == num & type.HasValue;
    string field = isCountField | isCountAggregate ? "Count" + PXGenericInqGrph.GetExtFieldId(gir) : (isFormula ? "Formula" + PXGenericInqGrph.GetExtFieldId(gir) : (isStringAgg ? "StringAgg" + PXGenericInqGrph.GetExtFieldId(gir) : gir.Field));
    string fieldName = $"{(git.Alias == cacheType.FullName ? cacheType.Name : git.Alias)}_{field}";
    if (!isFormula && !isCountField && !flag1 && !cache.Fields.Contains(gir.Field) && !this._descriptionFields.ContainsKey(fieldName) && !cleanable || isFormula && !isCountAggregate && !isStringAgg && !this.BaseQueryDescription.FormulaFields.Any<PXFormulaField>((Func<PXFormulaField, bool>) (f => string.Equals(f.Name, field, StringComparison.OrdinalIgnoreCase))))
      return;
    if (!string.Equals(fieldName, gir.FieldName) && this._initializedColumns.Contains(gir.FieldName))
      gir = (GIResult) this.Caches[typeof (GIResult)].CreateCopy((object) gir);
    gir.FieldName = fieldName;
    bool isvisible = gir.IsVisible.GetValueOrDefault();
    if (this._initializedColumns.Contains(fieldName))
      return;
    this._initializedColumns.Add(fieldName);
    if (forceCount)
      this._auxiliaryColumns.Add(fieldName);
    if (cache.Fields.Contains(field))
    {
      try
      {
        if (PXGenericInqGrph.IsVirtualField(cache, field, cacheType))
          this._virtualColumns.Add(fieldName);
      }
      catch
      {
      }
      PXSelectorAttribute[] array = cache.GetAttributesReadonly(field, true).OfType<PXSelectorAttribute>().ToArray<PXSelectorAttribute>();
      if ((array.Length == 0 ? 0 : (((IEnumerable<PXSelectorAttribute>) array).All<PXSelectorAttribute>((Func<PXSelectorAttribute, bool>) (s => s.SubstituteKey == (System.Type) null)) ? 1 : 0)) != 0)
      {
        string str = fieldName + "_description";
        if (!this.Results.Cache.Fields.Contains(str))
          this.Results.Cache.Fields.Add(str);
      }
    }
    if (!this.Results.Cache.Fields.Contains(fieldName))
      this.Results.Cache.Fields.Add(fieldName);
    this._resultColumns.Add(gir);
    PXGenericInqGrph.GIDescriptionField giDescrField;
    if (this._descriptionFields.TryGetValue(fieldName, out giDescrField))
      EmitDescriptionField(fieldName, giDescrField);
    PXGenericInqGrph.GIDescriptionField descrFieldSchema;
    if (this._descriptionFieldsBySourceField.TryGetValue(fieldName, out descrFieldSchema) && descrFieldSchema.IsSelfReferencing)
      EmitDescriptionField(descrFieldSchema.GenericDescriptionFieldAlias, descrFieldSchema);
    Dictionary<string, string> captionTranslations = this.GetCaptionTranslations(gir, giDescrField, cache);
    PXFieldSelecting handler = (PXFieldSelecting) ((sender, selArgs) =>
    {
      using (new PXGenericInqGrph.DynamicFieldsScope(this))
      {
        if (isCountField | isCountAggregate)
          this.CreateResultFromCount(selArgs, fieldName, this.GetResultColumnState(cache, selArgs.Row as GenericResult, git.Alias, field, gir.SchemaField), gir.Caption, isvisible);
        else if (isStringAgg)
          this.CreateResultFromStringAgg(selArgs, fieldName, this.GetResultColumnState(cache, selArgs.Row as GenericResult, git.Alias, field, gir.SchemaField), gir.Caption, isvisible);
        else if (isFormula)
        {
          this.CreateResultFromFormula(selArgs, fieldName, gir.SchemaField, gir.Caption, isvisible);
        }
        else
        {
          PXGenericInqGrph.GIFieldState resultColumnState = this.GetResultColumnState(cache, selArgs.Row as GenericResult, git.Alias, field, gir.SchemaField, selArgs.ReturnValue, giDescrField);
          PXFieldState state = resultColumnState.State;
          if (state != null)
          {
            this.Results.Cache.AdjustState(state);
            state.SetFieldName(fieldName);
            state.PrimaryKey = this.Results.Cache.Keys.Contains(fieldName);
            if (state.Visibility != PXUIVisibility.HiddenByAccessRights)
            {
              state.Visibility = PXUIVisibility.Visible;
              state.Visible = isvisible;
            }
            string translatedCaption = this.GetTranslatedCaption(gir, captionTranslations);
            state.DisplayName = this.ResolveFieldDisplayName(field, state.DisplayName, sender.Graph.Caches[cacheType], translatedCaption);
          }
          selArgs.ReturnState = (object) state;
          if (!resultColumnState.HasValue)
            return;
          selArgs.ReturnValue = resultColumnState.Value;
        }
      }
    });
    this.FieldSelecting.AddHandler(this.Results.Name, fieldName, handler);
    if (cleanable)
      this.CleanableFields.Add(gir, Tuple.Create<string, PXFieldSelecting>(fieldName, handler));
    bool flag2 = this.IsSubQueryInstance && isCountField | isCountAggregate;
    bool flag3 = this.IsSubQueryInstance & isStringAgg;
    if (isFormula | flag2 | flag3)
    {
      this.CommandPreparing.AddHandler(this.Results.Name, fieldName, (PXCommandPreparing) ((sender, cpArgs) =>
      {
        using (new PXGenericInqGrph.DynamicFieldsScope(this))
        {
          if ((cpArgs.Operation & PXDBOperation.External) != PXDBOperation.External)
            return;
          cpArgs.Expr = (SQLExpression) new Column(fieldName, cpArgs.Table);
          if (cpArgs.DataValue != null)
            return;
          cpArgs.DataValue = cpArgs.Value;
        }
      }));
      if (!string.IsNullOrEmpty(gir.SchemaField))
        this.FieldUpdating.AddHandler(this.Results.Name, fieldName, (PXFieldUpdating) ((sender, updArgs) =>
        {
          using (new PXGenericInqGrph.DynamicFieldsScope(this))
          {
            string[] strArray = gir.SchemaField.Split(new char[1]
            {
              '.'
            }, 2);
            if (strArray.Length != 2)
              return;
            string key = strArray[0];
            string name = strArray[1];
            GenericResult row1 = updArgs.Row as GenericResult;
            object row2 = (object) null;
            object newValue = updArgs.NewValue;
            if (row1?.Values != null && row1.Values.ContainsKey(key))
              row2 = row1.Values[key];
            PXTable pxTable;
            if (!this.BaseQueryDescription.Tables.TryGetValue(key, out pxTable))
              return;
            sender.Graph.Caches[pxTable.CacheType].RaiseFieldUpdating(name, row2, ref newValue);
            updArgs.NewValue = newValue;
          }
        }));
    }
    if (isCountField)
    {
      PXIntAttribute instance = PXEventSubscriberAttribute.CreateInstance<PXIntAttribute>();
      instance.FieldName = fieldName;
      instance.FieldOrdinal = this.Results.Cache.GetFieldOrdinal(fieldName);
      instance.BqlTable = this.Results.Cache.GetItemType();
      this.FieldUpdating.AddHandler(this.Results.Name, fieldName, new PXFieldUpdating(instance.FieldUpdating));
    }
    this.ConfigureNavigationAction(this, gir, fieldName);
    if (!(isFormula | isCountField | isCountAggregate | isStringAgg))
      return;
    this.SubscribeFormulaCountStringAggAndAggregateFieldsOnRowSelecting(cache.Graph, gir, fieldName, isFormula);

    void EmitDescriptionField(
      string fname,
      PXGenericInqGrph.GIDescriptionField descrFieldSchema)
    {
      if (string.IsNullOrEmpty(descrFieldSchema.DescriptionFieldAlias) || cache.Fields.Contains(descrFieldSchema.DescriptionFieldAlias) || !string.Equals(fname, descrFieldSchema.GenericDescriptionFieldAlias, StringComparison.OrdinalIgnoreCase))
        return;
      cache.Fields.Add(descrFieldSchema.DescriptionFieldAlias);
      cache.FieldSelectingEvents.Add(descrFieldSchema.DescriptionFieldAlias, (PXFieldSelecting) ((s, e) =>
      {
        if (descrFieldSchema.SourceStateInitializing)
          return;
        if (e.ReturnValue == null && e.Row != null)
          e.ReturnValue = s.GetValue(e.Row, descrFieldSchema.DescriptionFieldName);
        object returnState = e.ReturnState;
        s.RaiseFieldSelecting(descrFieldSchema.DescriptionFieldName, e.Row, ref returnState, e.IsAltered);
        this.AdjustStateForDescriptionField(returnState as PXFieldState, descrFieldSchema);
        e.ReturnState = returnState;
      }));
      cache.CommandPreparingEvents.Add(descrFieldSchema.DescriptionFieldAlias, (PXCommandPreparing) ((s, e) =>
      {
        if ((e.Operation & PXDBOperation.Option) != PXDBOperation.External)
          return;
        PXCommandPreparingEventArgs.FieldDescription description;
        s.RaiseCommandPreparing(descrFieldSchema.DescriptionFieldName, e.Row, e.Value, e.Operation, e.Table, out description);
        e.FillFromFieldDescription(description);
      }));
    }
  }

  private void ConfigureNavigationAction(PXGenericInqGrph graph, GIResult gir, string fieldName)
  {
    bool? defaultNav1 = gir.DefaultNav;
    bool flag1 = false;
    if (defaultNav1.GetValueOrDefault() == flag1 & defaultNav1.HasValue && gir.NavigationNbr.HasValue)
    {
      GINavigationScreen navScreen = graph.Description.NavigationScreens.FirstOrDefault<GINavigationScreen>((Func<GINavigationScreen, bool>) (s =>
      {
        int? lineNbr = s.LineNbr;
        int? navigationNbr = gir.NavigationNbr;
        return lineNbr.GetValueOrDefault() == navigationNbr.GetValueOrDefault() & lineNbr.HasValue == navigationNbr.HasValue;
      }));
      if (navScreen == null)
        return;
      PXButtonDelegate handler = (PXButtonDelegate) (adapter =>
      {
        this.NavigateTo(graph, navScreen.Link, PXWindowModeAttribute.Convert(navScreen.WindowMode), navScreen.LineNbr);
        return adapter.Get();
      });
      PXNamedAction.AddHiddenAction((PXGraph) this, typeof (GenericFilter), PXGenericInqGrph.GetNavigateToActionName(fieldName), handler);
    }
    else
    {
      bool? defaultNav2 = gir.DefaultNav;
      bool flag2 = true;
      PXTable pxTable;
      if (!(defaultNav2.GetValueOrDefault() == flag2 & defaultNav2.HasValue) || !this.BaseQueryDescription.Tables.TryGetValue(gir.ObjectName, out pxTable) || pxTable.Graph == null)
        return;
      string[] split = gir.Field.Split('_', 2);
      if (split.Length != 2)
        return;
      GIResult gir1 = pxTable.Graph.Description.Results.FirstOrDefault<GIResult>((Func<GIResult, bool>) (x => x.ObjectName.Equals(split[0], StringComparison.OrdinalIgnoreCase) && split[1].Equals(x.Field.StartsWith("=") ? "Formula" + PXGenericInqGrph.GetExtFieldId(x) : x.Field, StringComparison.OrdinalIgnoreCase)));
      if (gir1 == null)
        return;
      this.ConfigureNavigationAction(pxTable.Graph, gir1, fieldName);
    }
  }

  private Dictionary<string, string> GetCaptionTranslations(
    GIResult giResult,
    PXGenericInqGrph.GIDescriptionField giDescrField,
    PXCache dacCache)
  {
    if (!PXDBLocalizableStringAttribute.HasMultipleLocales)
      return new Dictionary<string, string>();
    PXCache cach = this.Caches[typeof (GIResult)];
    string fieldName = "CaptionTranslations";
    string[] valueExt1 = (string[]) cach.GetValueExt((object) giResult, fieldName);
    string field = giResult.Field;
    bool flag = field.StartsWith("=");
    if ((valueExt1 == null || ((IEnumerable<string>) valueExt1).All<string>((Func<string, bool>) (tr => tr == null))) && !flag)
    {
      if (!string.IsNullOrEmpty(giResult.Caption))
        return this.GetAllCaptionTranslations(giResult.Caption);
      string schemaField = giResult.SchemaField;
      string[] strArray = schemaField != null ? schemaField.Split('.', 2) : (string[]) null;
      if (strArray != null && strArray.Length == 2)
        return this.GetSchemaFieldCaption(strArray[0], strArray[1]);
      return giDescrField == null ? this.GetAllCaptionTranslations(dacCache, field) : new Dictionary<string, string>();
    }
    string[] valueExt2 = (string[]) cach.GetValueExt((object) null, fieldName);
    return (valueExt2 != null ? EnumerableExtensions.Zip<string, string>((IEnumerable<string>) valueExt2, (IEnumerable<string>) valueExt1).ToDictionary<Tuple<string, string>, string, string>((Func<Tuple<string, string>, string>) (x => x.Item1), (Func<Tuple<string, string>, string>) (x => x.Item2), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) : (Dictionary<string, string>) null) ?? new Dictionary<string, string>();
  }

  private Dictionary<string, string> GetAllCaptionTranslations(PXCache cache, string fieldName)
  {
    PXUIFieldAttribute pxuiFieldAttribute = cache != null ? cache.GetAttributesOfType<PXUIFieldAttribute>((object) null, fieldName).FirstOrDefault<PXUIFieldAttribute>() : (PXUIFieldAttribute) null;
    if (pxuiFieldAttribute == null)
      return new Dictionary<string, string>();
    (string LocaleName, string Language)[] withUniqueLanguage = PXLocalesHelper.GetLocalesWithUniqueLanguage(this.CurrentUserInformationProvider.GetUserName());
    if (withUniqueLanguage == null || withUniqueLanguage.Length == 0)
      return new Dictionary<string, string>();
    Dictionary<string, string> captionTranslations = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach ((string LocaleName, string Language) tuple in withUniqueLanguage)
    {
      using (new PXLocaleScope(tuple.LocaleName))
      {
        pxuiFieldAttribute.CacheAttached(cache);
        captionTranslations[tuple.Language] = pxuiFieldAttribute.DisplayName;
      }
    }
    return captionTranslations;
  }

  private Dictionary<string, string> GetAllCaptionTranslations(string caption)
  {
    Dictionary<string, string> captionTranslations = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    (string LocaleName, string Language)[] withUniqueLanguage = PXLocalesHelper.GetLocalesWithUniqueLanguage(this.CurrentUserInformationProvider.GetUserName());
    if (withUniqueLanguage == null || withUniqueLanguage.Length == 0)
      return new Dictionary<string, string>();
    foreach ((string LocaleName, string Language) tuple in withUniqueLanguage)
    {
      using (new PXLocaleScope(tuple.LocaleName))
      {
        string str = PXLocalizer.Localize(caption);
        captionTranslations[tuple.Language] = str;
      }
    }
    return captionTranslations;
  }

  private string GetTranslatedCaption(GIResult giResult, Dictionary<string, string> translations)
  {
    if (!PXDBLocalizableStringAttribute.HasMultipleLocales || translations.Count == 0)
      return giResult.Caption;
    string currentLanguage = PXLocalesHelper.GetCurrentLanguage();
    string translatedCaption;
    if (translations.TryGetValue(currentLanguage, out translatedCaption) && !string.IsNullOrEmpty(translatedCaption))
      return translatedCaption;
    string defaultLanguage = PXLocalesHelper.GetDefaultLanguage();
    return !translations.TryGetValue(defaultLanguage, out translatedCaption) || string.IsNullOrEmpty(translatedCaption) ? Localizer.Localize(giResult.Caption, currentLanguage) : Localizer.Localize(translatedCaption, currentLanguage);
  }

  private Dictionary<string, string> GetSchemaFieldCaption(string tableName, string fieldName)
  {
    PXTable table = this.BaseQueryDescription.Tables[tableName];
    PXGenericInqGrph pxGenericInqGrph = table.Graph ?? this;
    Dictionary<string, string> captionTranslations = this.GetAllCaptionTranslations(pxGenericInqGrph.Caches[table.BqlTable], fieldName);
    if (captionTranslations != null && captionTranslations.Any<KeyValuePair<string, string>>())
      return captionTranslations;
    if (!GenericInquiryHelpers.IsDescriptionField(fieldName))
    {
      string[] strArray = fieldName.Split('_', 2);
      if (strArray != null && strArray.Length == 2)
        return this.GetAllCaptionTranslations(pxGenericInqGrph.Caches[strArray[0]], strArray[1]) ?? new Dictionary<string, string>();
    }
    return new Dictionary<string, string>();
  }

  private void SubscribeFormulaCountStringAggAndAggregateFieldsOnRowSelecting(
    PXGraph graph,
    GIResult giResult,
    string fieldName,
    bool isFormula)
  {
    PXFieldState schemaFieldState = string.IsNullOrEmpty(giResult.SchemaField) ? (PXFieldState) null : this.GetStateFromSchemaField(giResult.SchemaField);
    bool needToLocalize = isFormula && PXDBLocalizableStringAttribute.IsEnabled && (schemaFieldState?.DataType == (System.Type) null || schemaFieldState.DataType == typeof (string));
    List<PXDBLocalizableStringAttribute> localizableStringAttributeInfos = needToLocalize ? this.GetFormulaLocalizableStringAttributes(fieldName) : (List<PXDBLocalizableStringAttribute>) null;
    this.Results.Cache.RowSelectingWhileReading += new PXRowSelecting(RowSelectingHandler);

    void RowSelectingHandler(PXCache cache, PXRowSelectingEventArgs selArgs)
    {
      using (new PXGenericInqGrph.DynamicFieldsScope(this))
      {
        if (selArgs.Row is GenericResult row)
        {
          object obj = selArgs.Record.GetValue(selArgs.Position);
          if (schemaFieldState?.DataType == typeof (bool))
          {
            int result;
            if (obj is string s && int.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
              obj = (object) result;
            obj = (object) Convert.ToBoolean(obj);
          }
          if (needToLocalize)
          {
            List<PXDBLocalizableStringAttribute> localizableStringAttributeList = localizableStringAttributeInfos;
            // ISSUE: explicit non-virtual call
            if ((localizableStringAttributeList != null ? (__nonvirtual (localizableStringAttributeList.Count) > 0 ? 1 : 0) : 0) != 0)
            {
              string nonLocalizedString = obj as string;
              obj = (object) this.LocalizeFormulaField(cache, nonLocalizedString, giResult.Field, localizableStringAttributeInfos);
            }
          }
          this.Results.Cache.SetValue((object) row, fieldName, obj);
        }
        ++selArgs.Position;
      }
    }
  }

  [PXInternalUseOnly]
  public IDictionary<GIResult, Tuple<string, PXFieldSelecting>> CleanableFields { get; } = (IDictionary<GIResult, Tuple<string, PXFieldSelecting>>) new Dictionary<GIResult, Tuple<string, PXFieldSelecting>>();

  private void GenerateNoteFields(string tableAlias)
  {
    if (string.IsNullOrEmpty(tableAlias) || this._noteFields.Count > 0)
      return;
    GITable giTable = this.Description.Tables.FirstOrDefault<GITable>((Func<GITable, bool>) (t => t.Alias.Equals(tableAlias, StringComparison.OrdinalIgnoreCase)));
    if (giTable == null)
      return;
    this.GenerateNoteFields(this.Caches[PXBuildManager.GetType(giTable.Name, true)], giTable.Alias);
  }

  private void GenerateNoteFields(PXCache cache, string alias)
  {
    if (this._noteFields.Count > 0 || !cache.Fields.Contains("NoteID") || this.Description.GroupBys.Any<GIGroupBy>() && this.Description.GroupBys.Select<GIGroupBy, string[]>((Func<GIGroupBy, string[]>) (gb => gb.DataFieldName.Split('.'))).Count<string[]>((Func<string[], bool>) (s => s.Length == 2 && string.Equals(s[0], alias, StringComparison.OrdinalIgnoreCase) && cache.Keys.Contains(s[1]))) != cache.Keys.Count)
      return;
    PXNoteAttribute.ForcePassThrow(cache, (string) null);
    string[] strArray = new string[5]
    {
      "NoteID",
      "NoteText",
      "NoteFiles",
      "NoteTextExists",
      "NoteFilesCount"
    };
    foreach (string str in strArray)
    {
      string f = str;
      if (cache.Fields.Contains(f) && !this._initializedColumns.Contains(f))
      {
        this._initializedColumns.Add(f);
        this.Results.Cache.Fields.Add(f);
        this.FieldSelecting.AddHandler(this.Results.Name, f, (PXFieldSelecting) ((sender, selArgs) =>
        {
          PXGenericInqGrph.GIFieldState resultColumnState = this.GetResultColumnState(cache, selArgs.Row as GenericResult, alias, f, (string) null);
          selArgs.ReturnState = (object) resultColumnState.State;
          selArgs.ReturnValue = resultColumnState.Value ?? (resultColumnState.State == null ? (object) null : resultColumnState.State.Value);
        }));
        this.FieldUpdating.AddHandler(this.Results.Name, f, (PXFieldUpdating) ((sender, updArgs) =>
        {
          object row2;
          if (!(updArgs.Row is GenericResult row3) || !row3.Values.TryGetValue(alias, out row2) || row2 == null)
            return;
          object newValue = updArgs.NewValue;
          cache.RaiseFieldUpdating(f, row2, ref newValue);
          updArgs.NewValue = newValue;
        }));
        this._noteFields.Add(f);
      }
    }
    this.GetRowWithNote = (Func<GenericResult, object>) (genericRow =>
    {
      if (genericRow == null)
        return (object) null;
      object obj;
      return !genericRow.Values.TryGetValue(alias, out obj) ? (object) null : obj;
    });
    this._noteTableAlias = alias;
  }

  private void DynamicFieldsCacheAttached()
  {
    EnumerableExtensions.ForEach(this._dynamicFields.Values.SelectMany((Func<PXGenericInqGrph.DynamicField, IEnumerable<System.Action<PXCache>>>) (f => (IEnumerable<System.Action<PXCache>>) f.cacheAttached), (f, ca) => new
    {
      table = f.bqlTable,
      ca = ca
    }), o => o.ca(this.Caches[o.table]));
  }

  internal void UseDynamicFields()
  {
    this._dynamicFieldsScope = new PXGenericInqGrph.DynamicFieldsScope(this);
  }

  private void AddDynamicFields()
  {
    foreach (PXGenericInqGrph.DynamicField dynamicField in this._dynamicFields.Values)
    {
      PXCache cach = this.Caches[dynamicField.bqlTable];
      if (!cach.Fields.Contains(dynamicField.name))
      {
        cach.Fields.Add(dynamicField.name);
        foreach (PXCommandPreparing commandPreparing in dynamicField.commandPreparing)
          cach.CommandPreparingEvents[dynamicField.name] += commandPreparing;
        foreach (PXFieldSelecting pxFieldSelecting in dynamicField.fieldSelecting)
          cach.FieldSelectingEvents[dynamicField.name] += pxFieldSelecting;
        foreach (PXFieldUpdating pxFieldUpdating in dynamicField.fieldUpdating)
          cach.FieldUpdatingEvents[dynamicField.name] += pxFieldUpdating;
        foreach (PXRowSelecting pxRowSelecting in dynamicField.rowSelecting)
          cach.RowSelectingWhileReading += pxRowSelecting;
      }
    }
  }

  private void RemoveDynamicFields()
  {
    foreach (PXGenericInqGrph.DynamicField dynamicField in this._dynamicFields.Values)
    {
      PXCache cach = this.Caches[dynamicField.bqlTable];
      if (cach.Fields.Contains(dynamicField.name))
      {
        cach.Fields.Remove(dynamicField.name);
        foreach (PXCommandPreparing commandPreparing in dynamicField.commandPreparing)
          cach.CommandPreparingEvents[dynamicField.name] -= commandPreparing;
        foreach (PXFieldSelecting pxFieldSelecting in dynamicField.fieldSelecting)
          cach.FieldSelectingEvents[dynamicField.name] -= pxFieldSelecting;
        foreach (PXFieldUpdating pxFieldUpdating in dynamicField.fieldUpdating)
          cach.FieldUpdatingEvents[dynamicField.name] -= pxFieldUpdating;
        foreach (PXRowSelecting pxRowSelecting in dynamicField.rowSelecting)
          cach.RowSelectingWhileReading -= pxRowSelecting;
      }
    }
  }

  private void GenerateDeletedDatabaseRecordFields()
  {
    foreach (GITable table in this.Description.Tables)
    {
      PXGenericInqGrph.DynamicField databaseRecordField = this.GenerateDeletedDatabaseRecordField(this.Caches[PXBuildManager.GetType(table.Name, true)]);
      if (databaseRecordField != null)
        this._dynamicFields[databaseRecordField.bqlTable] = databaseRecordField;
    }
  }

  private PXGenericInqGrph.DynamicField GenerateDeletedDatabaseRecordField(PXCache cache)
  {
    (string Field, System.Type type) = PXGenericInqGrph.GetDeletedDatabaseRecord(cache);
    if (Field == null || cache.Fields.Contains(Field))
      return (PXGenericInqGrph.DynamicField) null;
    PXGenericInqGrph.DynamicField databaseRecordField = new PXGenericInqGrph.DynamicField();
    databaseRecordField.name = Field;
    databaseRecordField.bqlTable = type;
    int? slotId = new int?();
    databaseRecordField.cacheAttached.Add((System.Action<PXCache>) (c => slotId = new int?(c.SetupSlot<bool?>((Func<bool?>) (() => new bool?()), (Func<bool?, bool?, bool?>) ((item, copy) => copy), (Func<bool?, bool?>) (item => item)))));
    PXDBBoolAttribute dbAttr = PXEventSubscriberAttribute.CreateInstance<PXDBBoolAttribute>();
    dbAttr.FieldName = Field;
    dbAttr.SetBqlTable(type);
    databaseRecordField.cacheAttached.Add((System.Action<PXCache>) (c => dbAttr.CacheAttached(c)));
    databaseRecordField.rowSelecting.Add((PXRowSelecting) ((s, e) =>
    {
      if (slotId.HasValue && e.Row != null)
      {
        bool? boolean = e.Record.GetBoolean(e.Position);
        s.SetSlot<bool?>(e.Row, slotId.Value, boolean);
      }
      ++e.Position;
    }));
    databaseRecordField.fieldSelecting.Add((PXFieldSelecting) ((s, e) =>
    {
      if (slotId.HasValue && e.Row != null)
        e.ReturnValue = (object) s.GetSlot<bool?>(e.Row, slotId.Value);
      dbAttr.FieldSelecting(s, e);
    }));
    databaseRecordField.commandPreparing.Add((PXCommandPreparing) ((s, e) =>
    {
      if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Select)
      {
        if (e.Value != null)
          return;
        dbAttr.CommandPreparing(s, e);
      }
      else
      {
        if (!slotId.HasValue)
          return;
        bool? slot = s.GetSlot<bool?>(e.Row, slotId.Value);
        if (!slot.HasValue)
          return;
        e.Value = (object) slot;
      }
    }));
    databaseRecordField.fieldUpdating.Add(new PXFieldUpdating(dbAttr.FieldUpdating));
    PXUIFieldAttribute uiAttr = PXEventSubscriberAttribute.CreateInstance<PXUIFieldAttribute>();
    uiAttr.FieldName = Field;
    uiAttr.DisplayName = "Is Deleted";
    uiAttr.SetBqlTable(type);
    databaseRecordField.cacheAttached.Add((System.Action<PXCache>) (c => uiAttr.CacheAttached(c)));
    databaseRecordField.fieldSelecting.Add(new PXFieldSelecting(uiAttr.FieldSelecting));
    return databaseRecordField;
  }

  private void GenerateDatabaseRecordStatusFields()
  {
    foreach (GITable table in this.Description.Tables)
    {
      PXGenericInqGrph.DynamicField recordStatusField = this.GenerateDatabaseRecordStatusField(this.Caches[PXBuildManager.GetType(table.Name, true)]);
      if (recordStatusField != null)
        this._dynamicFields[recordStatusField.bqlTable] = recordStatusField;
    }
  }

  private PXGenericInqGrph.DynamicField GenerateDatabaseRecordStatusField(PXCache cache)
  {
    (string Field, System.Type type) = PXGenericInqGrph.GetDatabaseRecordStatus(cache);
    if (Field == null || cache.Fields.Contains(Field))
      return (PXGenericInqGrph.DynamicField) null;
    PXGenericInqGrph.DynamicField recordStatusField = new PXGenericInqGrph.DynamicField();
    recordStatusField.name = Field;
    recordStatusField.bqlTable = type;
    int? slotId = new int?();
    recordStatusField.cacheAttached.Add((System.Action<PXCache>) (c => slotId = new int?(c.SetupSlot<int?>((Func<int?>) (() => new int?()), (Func<int?, int?, int?>) ((item, copy) => copy), (Func<int?, int?>) (item => item)))));
    PXDBIntAttribute dbAttr = PXEventSubscriberAttribute.CreateInstance<PXDBIntAttribute>();
    dbAttr.FieldName = Field;
    dbAttr.SetBqlTable(type);
    recordStatusField.cacheAttached.Add((System.Action<PXCache>) (c => dbAttr.CacheAttached(c)));
    recordStatusField.rowSelecting.Add((PXRowSelecting) ((s, e) =>
    {
      if (slotId.HasValue && e.Row != null)
      {
        int? int32 = e.Record.GetInt32(e.Position);
        s.SetSlot<int?>(e.Row, slotId.Value, int32);
      }
      ++e.Position;
    }));
    recordStatusField.fieldSelecting.Add((PXFieldSelecting) ((s, e) =>
    {
      if (slotId.HasValue && e.Row != null)
        e.ReturnValue = (object) s.GetSlot<int?>(e.Row, slotId.Value);
      dbAttr.FieldSelecting(s, e);
    }));
    recordStatusField.commandPreparing.Add((PXCommandPreparing) ((s, e) =>
    {
      if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Select)
      {
        if (e.Value != null)
          return;
        dbAttr.CommandPreparing(s, e);
      }
      else
      {
        if (!slotId.HasValue)
          return;
        int? slot = s.GetSlot<int?>(e.Row, slotId.Value);
        if (!slot.HasValue)
          return;
        e.Value = (object) slot;
      }
    }));
    recordStatusField.fieldUpdating.Add(new PXFieldUpdating(dbAttr.FieldUpdating));
    PXUIFieldAttribute uiAttr = PXEventSubscriberAttribute.CreateInstance<PXUIFieldAttribute>();
    uiAttr.FieldName = Field;
    uiAttr.DisplayName = "Status";
    uiAttr.SetBqlTable(type);
    recordStatusField.cacheAttached.Add((System.Action<PXCache>) (c => uiAttr.CacheAttached(c)));
    recordStatusField.fieldSelecting.Add(new PXFieldSelecting(uiAttr.FieldSelecting));
    return recordStatusField;
  }

  private void CollectVirtualFieldsInFormulas()
  {
    foreach (PXFormulaField formulaField in this.BaseQueryDescription.FormulaFields)
    {
      PXFormulaField formula = formulaField;
      formula.Value.GetExpression((Func<string, SQLExpression>) (fn =>
      {
        string[] strArray = fn.Split(new char[1]{ '.' }, 2);
        PXTable pxTable;
        if (strArray.Length == 2 && this.BaseQueryDescription.Tables.TryGetValue(strArray[0], out pxTable))
        {
          System.Type cacheType = pxTable.CacheType;
          if (PXGenericInqGrph.IsVirtualField((pxTable.Graph ?? this).Caches[cacheType], strArray[1], cacheType))
            this._virtualColumns.Add($"{formula.Table.Alias}_{formula.Name}");
        }
        return (SQLExpression) new Column(fn);
      }));
    }
  }

  internal IEnumerable<string> GetDataFieldsFromFormula(string formula)
  {
    if (string.IsNullOrEmpty(formula) || !formula.StartsWith("="))
      return Enumerable.Empty<string>();
    List<string> fields = new List<string>();
    new PXCalcedValue(formula, this._formulaProcessor).GetExpression((Func<string, SQLExpression>) (fn =>
    {
      string[] strArray = fn.Split(new char[1]{ '.' }, 2);
      if (strArray.Length == 2 && this.BaseQueryDescription.Tables.ContainsKey(strArray[0]))
        fields.Add($"{strArray[0]}_{strArray[1]}");
      return (SQLExpression) new Column(fn);
    }), false);
    return (IEnumerable<string>) fields;
  }

  protected bool ActiveVirtualField(PXQueryDescription descr)
  {
    using (new PXGenericInqGrph.DynamicFieldsScope(this))
    {
      BqlGenericCommand genericCommand = new BqlGenericCommand((PXGraph) this, (PXCache) this.Results.Cache, descr);
      try
      {
        foreach (PXWhereCond filterWhere in descr.FilterWheres)
        {
          PXWhereCond where = filterWhere;
          int parNum = 0;
          where.DataField.GetExpression((Func<string, SQLExpression>) (_ => genericCommand.ParameterHandlerExpression(_, ref parNum, PXDBOperation.WhereClause, where.UseExt)));
        }
      }
      catch (PXVirtualFieldException ex)
      {
        return true;
      }
      return false;
    }
  }

  internal PXQueryDescription GetCurrentQueryDescription()
  {
    PXQueryDescription queryDescription;
    using (new PXGenericInqGrph.DynamicFieldsScope(this))
    {
      if (this.Filter.Current.Values.Count == 0)
      {
        for (int index = 0; index < this.Filter.Cache.Fields.Count; ++index)
          this.Filter.Cache.GetStateExt((object) null, this.Filter.Cache.Fields[index]);
      }
      queryDescription = this.CreateQueryDescription();
      this.DescribeSortsAndSearches(queryDescription);
      this.DescribeFilters(queryDescription);
      this.PrepareWhereParameters(queryDescription);
      if (PXView.RetrieveTotalRowCount && !queryDescription.ResetTopCount)
      {
        queryDescription.RetrieveTotalRowCount = true;
        queryDescription.RetrieveTotals = true;
      }
      else if (queryDescription.TotalFields.Count > 0)
      {
        if (queryDescription.RetrieveTotals)
        {
          if (!this.ActiveVirtualField(queryDescription))
            PXView.Filters.Clear();
        }
      }
    }
    return queryDescription;
  }

  private void DescribeFilters(PXQueryDescription description)
  {
    PXView.PXFilterRowCollection filters = PXView.Filters;
    this.DescribeFilters(description, filters);
    PXView.Filters.Clear();
    PXView.Filters.Add((PXFilterRow[]) filters);
  }

  /// <summary>
  /// Returns true, if outer query searches for Generic Inquiry record by it's artificial key.
  /// </summary>
  private bool SearchByID
  {
    get
    {
      return PXView.MaximumRows == 1 && PXView.Searches.Length >= this.Results.Cache.Keys.Count && ((IEnumerable<object>) PXView.Searches).Any<object>((Func<object, bool>) (s => s != null)) && new HashSet<string>((IEnumerable<string>) PXView.SortColumns, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).IsSubsetOf((IEnumerable<string>) this.Results.Cache.Keys);
    }
  }

  private void AddSortByFormula(
    PXSort sortField,
    PXQueryDescription description,
    HashSet<string> addedSorts,
    List<PXView.PXSearchColumn> sortColumns)
  {
    PXAggregateField pxAggregateField1 = description.AggregateFields.FirstOrDefault<PXAggregateField>((Func<PXAggregateField, bool>) (f =>
    {
      if (!string.Equals(f.Name, PXGenericInqGrph.GetFieldName(sortField), StringComparison.OrdinalIgnoreCase) || !sortField.Table.Equals(f.Table))
        return false;
      AggregateFunction? function = f.Function;
      AggregateFunction aggregateFunction = AggregateFunction.Count;
      return function.GetValueOrDefault() == aggregateFunction & function.HasValue;
    }));
    PXAggregateField pxAggregateField2 = description.AggregateFields.FirstOrDefault<PXAggregateField>((Func<PXAggregateField, bool>) (f =>
    {
      if (!string.Equals(f.Name, PXGenericInqGrph.GetFieldName(sortField), StringComparison.OrdinalIgnoreCase) || !sortField.Table.Equals(f.Table))
        return false;
      AggregateFunction? function = f.Function;
      AggregateFunction aggregateFunction = AggregateFunction.StringAgg;
      return function.GetValueOrDefault() == aggregateFunction & function.HasValue;
    }));
    if (description.ExtFields.Any<PXExtField>((Func<PXExtField, bool>) (f => f is PXFormulaField pxFormulaField && pxFormulaField.Value.ToString() == sortField.DataField.ToString() && description.Sorts.Any<PXSort>((Func<PXSort, bool>) (s => (s.DataField is PXFieldValue dataField ? dataField.FieldName : (string) null) == f.Name)))))
      return;
    if (!(sortField.DataField is PXFieldValue))
    {
      description.Sorts.Add(sortField);
    }
    else
    {
      bool flag1 = pxAggregateField1 != null;
      bool flag2 = pxAggregateField2 != null;
      string column = $"{sortField.Table.Alias}_{(flag1 ? pxAggregateField1.Alias : (flag2 ? pxAggregateField2.Alias : PXGenericInqGrph.GetFieldName(sortField)))}";
      if (addedSorts.Contains(column))
        return;
      PXView.PXSearchColumn pxSearchColumn1 = new PXView.PXSearchColumn(column, sortField.Order == SortOrder.Desc, (object) null);
      pxSearchColumn1.UseExt = true;
      PXView.PXSearchColumn pxSearchColumn2 = pxSearchColumn1;
      if (PXView.ReverseOrder)
        this.InverseSortOrder(sortField);
      sortColumns.Add(pxSearchColumn2);
      if (flag1 | flag2)
      {
        PXAggregateField pxAggregateField3 = flag1 ? pxAggregateField1 : pxAggregateField2;
        PXSort pxSort = (PXSort) sortField.Clone();
        pxSort.Table = pxAggregateField3.Table;
        pxSort.DataField = (IPXValue) new PXFieldValue(pxAggregateField3.Table.Alias, pxAggregateField3.Alias);
        description.Sorts.Add(pxSort);
      }
      else
        description.Sorts.Add(sortField);
      addedSorts.Add(column);
    }
  }

  private void DescribeSortsAndSearches(PXQueryDescription description)
  {
    PXSort[] array = description.Sorts.ToArray();
    HashSet<string> addedSorts = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    description.Sorts.Clear();
    description.Searches.Clear();
    List<PXView.PXSearchColumn> pxSearchColumnList = new List<PXView.PXSearchColumn>();
    List<PXSort> list = ((IEnumerable<PXSort>) array).Where<PXSort>((Func<PXSort, bool>) (s => s.DataField.ToString().StartsWith("="))).ToList<PXSort>();
    int num1 = 0;
    bool flag1 = false;
    int num2 = PXView.SortColumns.Length;
    if (PXView.SortColumns.Length == this.Results.Cache.Keys.Count)
    {
      flag1 = true;
      for (int index = 0; flag1 && index < PXView.SortColumns.Length; ++index)
        flag1 &= PXView.SortColumns[index].OrdinalEquals(this.Results.Cache.Keys[index]);
    }
    else if (((IEnumerable<PXSort>) array).Count<PXSort>() > 0)
      num2 = PXView.SortColumns.Length - this.Results.Cache.Keys.Count;
    for (int index = 0; PXView.SortColumns != null && index < PXView.SortColumns.Length; ++index)
    {
      string sortColumn = PXView.SortColumns[index];
      object searchValue = index < PXView.Sorts.Length ? PXView.Sorts[index].SearchValue : (object) null;
      bool flag2 = PXView.Descendings != null && index < PXView.Descendings.Length && PXView.Descendings[index];
      if (searchValue != null || (!flag1 || flag2) && index < num2)
      {
        if (int.TryParse(sortColumn, out int _))
        {
          PXSort sortField = list[num1++];
          MatchCollection matchCollection = PXGenericInqGrph._CommentSearch.Matches(sortField.DataField.ToString());
          if (matchCollection.Count > 0)
            index += matchCollection.Count - 1;
          this.AddSortByFormula(sortField, description, addedSorts, pxSearchColumnList);
        }
        else if (this.Results.Cache.PlainDacFields.Contains(sortColumn))
        {
          pxSearchColumnList.Add(PXView.Sorts[index]);
        }
        else
        {
          string[] strArray = sortColumn.Split('_', 2);
          if (strArray.Length == 2 && description.Tables.ContainsKey(strArray[0]))
          {
            string str = strArray[0];
            string fieldName = strArray[1];
            PXSort sort = new PXSort()
            {
              Table = description.Tables[str],
              DataField = (IPXValue) new PXFieldValue(str, fieldName),
              Order = flag2 ? SortOrder.Desc : SortOrder.Asc
            };
            description.Sorts.Add(sort);
            addedSorts.Add(sortColumn);
            if (searchValue != null)
            {
              PXWhereCond search = this.CreateSearch(sort, searchValue, PXView.Sorts[index].UseExt);
              if (description.GroupBys.Count == 0)
                description.Searches.Add(search);
              else
                description.FilterWheres.Add(search);
            }
            pxSearchColumnList.Add(PXView.Sorts[index]);
            if (PXView.ReverseOrder)
              this.InverseSortOrder(sort);
          }
        }
      }
    }
    foreach (PXSort sortField in array)
      this.AddSortByFormula(sortField, description, addedSorts, pxSearchColumnList);
    if (PXView.MaximumRows != 0 && array.Length == 0)
    {
      foreach (PXTable table in description.UsedTables.Values)
      {
        foreach (string tableKeyName in GetTableKeyNames(table))
        {
          string column = $"{table.Alias}_{tableKeyName}";
          if (!addedSorts.Contains(column))
          {
            PXSort sort = new PXSort()
            {
              Table = table,
              DataField = (IPXValue) new PXFieldValue(table.Alias, tableKeyName),
              Order = SortOrder.Asc
            };
            PXView.PXSearchColumn pxSearchColumn1 = new PXView.PXSearchColumn(column, sort.Order == SortOrder.Desc, (object) null);
            pxSearchColumn1.UseExt = true;
            PXView.PXSearchColumn pxSearchColumn2 = pxSearchColumn1;
            if (PXView.ReverseOrder)
              this.InverseSortOrder(sort);
            description.Sorts.Add(sort);
            pxSearchColumnList.Add(pxSearchColumn2);
            addedSorts.Add(sort.Table.BqlTable.FullName + sort.DataField?.ToString());
          }
        }
      }
    }
    if (PXView.Searches.Length == 0 || ((IEnumerable<object>) PXView.Searches).All<object>((Func<object, bool>) (s => s == null)))
    {
      PXView.Sorts.Clear();
      foreach (PXView.PXSearchColumn pxSearchColumn in pxSearchColumnList)
      {
        if (this._descriptionFields.ContainsKey(pxSearchColumn.Column))
          pxSearchColumn.UseExt = true;
      }
      PXView.Sorts.Add(pxSearchColumnList.ToArray());
    }
    description.ResetTopCount |= pxSearchColumnList.Any<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (c => this.IsVirtualField(c.Column)));

    IEnumerable<string> GetTableKeyNames(PXTable table)
    {
      if (table.Graph != null)
        return (IEnumerable<string>) table.Graph.Caches[table.CacheType].Keys;
      return !table.IsInDbExist ? (IEnumerable<string>) Array<string>.Empty : this.Caches[table.BqlTable].BqlKeys.Select<System.Type, string>((Func<System.Type, string>) (x => x.Name));
    }
  }

  internal void PrepareWhereParameters(PXQueryDescription descr)
  {
    foreach (PXWhereCond pxWhereCond1 in new List<PXWhereCond>((IEnumerable<PXWhereCond>) descr.Wheres))
    {
      PXWhereCond w = pxWhereCond1;
      if (w.Table != null)
      {
        PXCache cach = this.Caches[w.Table.BqlTable];
        PXFieldValue dataField = w.DataField as PXFieldValue;
        if (cach != null && dataField != null && cach.GetStateExt((object) null, dataField.FieldName) is PXStringState stateExt)
        {
          if (stateExt.MultiSelect)
          {
            object parameter = w.Value1.GetParameters((Func<string, IPXValue>) null)[0];
            if (parameter != null)
            {
              string[] strArray = parameter.ToString().Split(new char[1]
              {
                ','
              }, StringSplitOptions.RemoveEmptyEntries);
              if (w.Cond == Condition.Contains || w.Cond == Condition.NotContains)
              {
                int index1;
                int index2 = index1 = descr.Wheres.IndexOf(w);
                descr.Wheres.Remove(w);
                w.UseExt = true;
                foreach (string str1 in strArray)
                {
                  if (w.Cond == Condition.Contains)
                  {
                    PXWhereCond subCondition1 = this.CreateSubCondition(w, Condition.Equals, str1);
                    List<PXWhereCond> wheres1 = descr.Wheres;
                    int index3 = index1;
                    int num1 = index3 + 1;
                    PXWhereCond pxWhereCond2 = subCondition1;
                    wheres1.Insert(index3, pxWhereCond2);
                    string str2 = $",{str1},";
                    PXWhereCond subCondition2 = this.CreateSubCondition(w, Condition.Contains, str2);
                    List<PXWhereCond> wheres2 = descr.Wheres;
                    int index4 = num1;
                    int num2 = index4 + 1;
                    PXWhereCond pxWhereCond3 = subCondition2;
                    wheres2.Insert(index4, pxWhereCond3);
                    string str3 = str1 + ",";
                    PXWhereCond subCondition3 = this.CreateSubCondition(w, Condition.StartsWith, str3);
                    List<PXWhereCond> wheres3 = descr.Wheres;
                    int index5 = num2;
                    int num3 = index5 + 1;
                    PXWhereCond pxWhereCond4 = subCondition3;
                    wheres3.Insert(index5, pxWhereCond4);
                    string str4 = "," + str1;
                    PXWhereCond subCondition4 = this.CreateSubCondition(w, Condition.EndsWith, str4);
                    List<PXWhereCond> wheres4 = descr.Wheres;
                    int index6 = num3;
                    index1 = index6 + 1;
                    PXWhereCond pxWhereCond5 = subCondition4;
                    wheres4.Insert(index6, pxWhereCond5);
                  }
                  else
                  {
                    PXWhereCond pxWhereCond6 = w.Clone() as PXWhereCond;
                    pxWhereCond6.Value1 = (IPXValue) new PXSimpleValue((object) str1);
                    pxWhereCond6.Op = PX.Data.Description.GI.Operation.And;
                    descr.Wheres.Insert(index1, pxWhereCond6);
                    ++index1;
                  }
                }
                PXWhereCond pxWhereCond7 = descr.Wheres.ElementAt<PXWhereCond>(index1 - 1);
                PXWhereCond pxWhereCond8 = descr.Wheres.ElementAt<PXWhereCond>(index2);
                pxWhereCond7.Op = w.Op;
                pxWhereCond7.CloseBrackets = w.CloseBrackets + 1;
                pxWhereCond8.OpenBrackets = w.OpenBrackets + 1;
              }
              else
                w.Value1 = (IPXValue) new PXSimpleValue((object) string.Join(",", strArray));
            }
          }
          else if (w.Cond == Condition.Contains || w.Cond == Condition.StartsWith || w.Cond == Condition.EndsWith || w.Cond == Condition.NotContains)
          {
            string wildcard = this.SqlDialect.WildcardAnything;
            Func<object, object> paramValueHandler = (Func<object, object>) (v =>
            {
              if (v is string str7)
              {
                string str6 = str7.TrimEnd();
                switch (w.Cond)
                {
                  case Condition.Contains:
                  case Condition.NotContains:
                    return (object) (wildcard + str6 + wildcard);
                  case Condition.EndsWith:
                    return (object) (wildcard + str6);
                  case Condition.StartsWith:
                    return (object) (str6 + wildcard);
                }
              }
              return v;
            });
            if (w.Value1 is PXParameterValue pxParameterValue1)
              w.Value1 = (IPXValue) new PXParameterValue(pxParameterValue1.Parameter, pxParameterValue1.Graph, paramValueHandler);
            if (w.Value2 is PXParameterValue pxParameterValue2)
              w.Value2 = (IPXValue) new PXParameterValue(pxParameterValue2.Parameter, pxParameterValue2.Graph, paramValueHandler);
          }
        }
      }
    }
  }

  private PXWhereCond CreateSubCondition(
    PXWhereCond oridinalCondition,
    Condition condition,
    string value)
  {
    PXWhereCond subCondition = (PXWhereCond) oridinalCondition.Clone();
    subCondition.Value1 = (IPXValue) new PXSimpleValue((object) value);
    subCondition.Op = PX.Data.Description.GI.Operation.Or;
    subCondition.Cond = condition;
    return subCondition;
  }

  private List<PXSort> CloneSorts(List<PXSort> sorts)
  {
    return sorts != null ? sorts.Select<PXSort, PXSort>((Func<PXSort, PXSort>) (s => s != null ? (PXSort) s.Clone() : (PXSort) null)).ToList<PXSort>() : (List<PXSort>) null;
  }

  private void InverseSortsOrder(IEnumerable<PXSort> sorts)
  {
    foreach (PXSort sort in sorts)
      this.InverseSortOrder(sort);
  }

  private void InverseSortOrder(PXSort sort)
  {
    if (sort == null)
      return;
    if (sort.Order == SortOrder.Asc)
    {
      sort.Order = SortOrder.Desc;
    }
    else
    {
      if (sort.Order != SortOrder.Desc)
        return;
      sort.Order = SortOrder.Asc;
    }
  }

  private IEnumerable<PXWhereCond> GenerateORGroup(
    PXTable table,
    IPXValue dataField,
    ICollection values)
  {
    if (values == null || values.Count == 0)
      return Enumerable.Empty<PXWhereCond>();
    List<PXWhereCond> orGroup = new List<PXWhereCond>();
    foreach (object obj in (IEnumerable) values)
      orGroup.Add(new PXWhereCond()
      {
        Table = table,
        DataField = dataField,
        Cond = Condition.Equals,
        Op = PX.Data.Description.GI.Operation.Or,
        Value1 = (IPXValue) new PXSimpleValue(obj),
        Value2 = (IPXValue) new PXSimpleValue(obj)
      });
    if (orGroup.Count > 1)
    {
      orGroup[0].OpenBrackets = 1;
      orGroup[orGroup.Count - 1].CloseBrackets = 1;
    }
    else
      orGroup[0].Op = PX.Data.Description.GI.Operation.And;
    return (IEnumerable<PXWhereCond>) orGroup;
  }

  private static void VoidFilter(PXFilterRow filter, PXWhereCond where)
  {
    filter.Value = (object) null;
    filter.Value2 = (object) null;
    where.Value1 = (IPXValue) null;
    where.Value2 = (IPXValue) null;
  }

  private void ProcessRelativeFilters(
    PXFilterRow filter,
    PXWhereCond where,
    PXQueryDescription description)
  {
    if (!filter.Variable.HasValue)
      return;
    where.UseExt = false;
    if (filter.Value is string && FilterVariable.GetConditionViolationMessage(filter.Value as string, filter.Condition) != null)
    {
      PXGenericInqGrph.VoidFilter(filter, where);
    }
    else
    {
      filter.DataField = filter.DataField.RemoveFromEnd("_description", StringComparison.OrdinalIgnoreCase);
      PXFieldValue dataField = (PXFieldValue) where.DataField;
      where.DataField = (IPXValue) new PXFieldValue(dataField.TableName, dataField.FieldName.RemoveFromEnd("_description", StringComparison.OrdinalIgnoreCase));
      PXCommandPreparingEventArgs.FieldDescription description1;
      this.Results.Cache.RaiseCommandPreparing(filter.DataField, (object) null, (object) null, PXDBOperation.Select, typeof (GenericResult), out description1);
      if (description1?.Expr == null)
        this.Results.Cache.RaiseCommandPreparing(filter.DataField, (object) null, (object) null, PXDBOperation.External, typeof (GenericResult), out description1);
      if (description1?.Expr == null)
        PXGenericInqGrph.VoidFilter(filter, where);
      else if (filter.Variable.Value == FilterVariableType.CurrentUser)
      {
        if (description1.DataType != PXDbType.Int && description1.DataType != PXDbType.UniqueIdentifier && description1.DataType != PXDbType.VarChar && description1.DataType != PXDbType.NVarChar)
        {
          PXGenericInqGrph.VoidFilter(filter, where);
        }
        else
        {
          object obj1;
          switch (description1.DataType)
          {
            case PXDbType.Int:
              obj1 = (object) PXAccess.GetContactID();
              break;
            case PXDbType.NVarChar:
              obj1 = (object) PXAccess.GetUserName();
              break;
            case PXDbType.UniqueIdentifier:
              obj1 = (object) PXAccess.GetUserID();
              break;
            case PXDbType.VarChar:
              obj1 = (object) PXAccess.GetUserName();
              break;
            default:
              obj1 = filter.Value;
              break;
          }
          object obj2 = obj1;
          where.Value1 = (IPXValue) new PXSimpleValue(obj2);
          where.Value2 = (IPXValue) new PXSimpleValue(obj2);
        }
      }
      else if ((filter.Variable.Value == FilterVariableType.CurrentUserGroups || filter.Variable.Value == FilterVariableType.CurrentUserGroupsTree || filter.Variable.Value == FilterVariableType.CurrentOrganization) && (filter.Condition == PXCondition.IN || filter.Condition == PXCondition.NI))
      {
        if (description1.DataType != PXDbType.Int)
        {
          PXGenericInqGrph.VoidFilter(filter, where);
        }
        else
        {
          where.Value1 = (IPXValue) null;
          where.Cond = filter.Condition == PXCondition.IN ? Condition.In : Condition.NotIn;
          UserGroupLazyCache current = UserGroupLazyCache.Current;
          switch (filter.Variable.Value)
          {
            case FilterVariableType.CurrentUserGroups:
              where.Value1 = (IPXValue) new PXInValue<int>((IEnumerable<int>) current.GetUserGroupIds(PXAccess.GetUserID()));
              break;
            case FilterVariableType.CurrentUserGroupsTree:
              where.Value1 = (IPXValue) new PXInValue<int>((IEnumerable<int>) current.GetUserWorkTreeIds(PXAccess.GetUserID()));
              break;
            case FilterVariableType.CurrentOrganization:
              where.Value1 = (IPXValue) new PXInValue<int>((IEnumerable<int>) PXAccess.GetBranchIDsForCurrentOrganization());
              break;
          }
        }
      }
      else
      {
        if (filter.Variable.Value != FilterVariableType.CurrentBranch)
          return;
        if (description1.DataType != PXDbType.Int)
        {
          PXGenericInqGrph.VoidFilter(filter, where);
        }
        else
        {
          object branchId = (object) PXAccess.GetBranchID();
          if (branchId == null)
          {
            where.Cond = Condition.IsNull;
          }
          else
          {
            where.Value1 = (IPXValue) new PXSimpleValue(branchId);
            where.Value2 = (IPXValue) new PXSimpleValue(branchId);
          }
        }
      }
    }
  }

  internal void DescribeFilters(
    PXQueryDescription description,
    PXView.PXFilterRowCollection filters)
  {
    bool skipped;
    bool flag = filters.PrepareFilters(out skipped);
    if (skipped)
    {
      PXFilterRow[] filters1 = (PXFilterRow[]) filters;
      BqlGenericCommand bqlGenericCommand = new BqlGenericCommand((PXGraph) this, (PXCache) this.Results.Cache, description);
      flag |= PXView.prepareFilters((PXGraph) this, (PXCache) this.Results.Cache, (BqlCommand) bqlGenericCommand, ref filters1);
    }
    description.ResetTopCount |= flag;
    int num = 0;
    foreach (PXFilterRow filter in filters)
    {
      string[] strArray = filter.DataField.Split('_', 2);
      if (strArray.Length < 2 || !description.Tables.ContainsKey(strArray[0]))
      {
        num += filter.OpenBrackets - filter.CloseBrackets;
      }
      else
      {
        string str = strArray[0];
        string field = strArray[1];
        PXFormulaField pxFormulaField = description.FormulaFields.FirstOrDefault<PXFormulaField>((Func<PXFormulaField, bool>) (f => f.Name == field));
        PXWhereCond where = new PXWhereCond();
        where.Table = description.Tables[str];
        where.DataField = pxFormulaField == null ? (IPXValue) new PXFieldValue(str, field) : pxFormulaField.Value;
        ValFromStr.ApplyFilter((PXGraph) this, where, filter);
        this.ProcessRelativeFilters(filter, where, description);
        if (where.Value1 != null)
          description.FilterWheres.Add(where);
        if (!where.UseExt && (where.Cond == Condition.StartsWith || where.Cond == Condition.EndsWith || where.Cond == Condition.Contains || where.Cond == Condition.NotContains))
        {
          PXFieldState stateExt = this.Caches[where.Table.CacheType].GetStateExt((object) null, field) as PXFieldState;
          if (stateExt is PXStringState pxStringState)
          {
            string[] allowedValues = pxStringState.AllowedValues;
            if ((allowedValues != null ? (allowedValues.Length != 0 ? 1 : 0) : 0) != 0)
              goto label_13;
          }
          if (stateExt is PXIntState pxIntState)
          {
            int[] allowedValues = pxIntState.AllowedValues;
            if ((allowedValues != null ? (allowedValues.Length != 0 ? 1 : 0) : 0) == 0)
              goto label_14;
          }
          else
            goto label_14;
label_13:
          where.UseExt = true;
        }
label_14:
        if (num > 0)
          where.OpenBrackets += num;
        if (num < 0)
          where.CloseBrackets -= num;
        num = 0;
      }
    }
  }

  /// <summary>
  /// Creates search based on sort configuration and search value.
  /// </summary>
  private PXWhereCond CreateSearch(PXSort sort, object value, bool useExt)
  {
    int maximumRows = PXView.MaximumRows;
    bool reverseOrder = PXView.ReverseOrder;
    if (value == null)
      return (PXWhereCond) null;
    Condition condition = maximumRows != 1 ? (sort.Order != SortOrder.Desc ? (reverseOrder ? Condition.Greater : Condition.GreaterOrEqual) : (reverseOrder ? Condition.Less : Condition.LessOrEqual)) : Condition.Equals;
    return new PXWhereCond()
    {
      DataField = sort.DataField,
      Cond = condition,
      Table = sort.Table,
      Op = PX.Data.Description.GI.Operation.And,
      UseExt = useExt,
      Value1 = (IPXValue) new PXSimpleValue(value),
      Value2 = (IPXValue) new PXSimpleValue(value)
    };
  }

  private void CreateResultFromFormula(
    PXFieldSelectingEventArgs e,
    string fieldname,
    string schemaField,
    string caption,
    bool isvisible)
  {
    object obj = e.Row is GenericResult row || e.ReturnValue == null ? this.Results.Cache.GetValue((object) row, fieldname) : e.ReturnValue;
    PXFieldState state = this.GetStateFromSchemaField(schemaField);
    if (state == null)
    {
      state = PXStringState.CreateInstance(obj, new int?(), new bool?(true), fieldname, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
      obj = (object) obj?.ToString();
    }
    this.Results.Cache.AdjustState(state);
    state.SetFieldName(fieldname);
    state.Visible = isvisible;
    state.Visibility = PXUIVisibility.Visible;
    state.PrimaryKey = false;
    state.Value = e.ReturnValue = obj;
    string str = caption;
    if (string.IsNullOrEmpty(str) && e.ReturnState != null && e.ReturnState is PXFieldState)
      str = (e.ReturnState as PXFieldState).DisplayName;
    if (string.IsNullOrEmpty(str))
      str = state.DisplayName;
    state.DisplayName = str;
    e.ReturnState = (object) state;
  }

  private void CreateResultFromCount(
    PXFieldSelectingEventArgs e,
    string fieldname,
    PXGenericInqGrph.GIFieldState schema,
    string caption,
    bool isvisible)
  {
    PXFieldState instance = PXIntState.CreateInstance(e.Row is GenericResult row || !(e.ReturnValue is string returnValue) ? this.Results.Cache.GetValue((object) row, fieldname) : (object) int.Parse(returnValue), fieldname, new bool?(false), new int?(), new int?(), new int?(), (int[]) null, (string[]) null, (System.Type) null, new int?());
    this.AdjustState(instance, e.ReturnState, fieldname, schema, caption, isvisible);
    e.ReturnState = (object) instance;
  }

  private void CreateResultFromStringAgg(
    PXFieldSelectingEventArgs e,
    string fieldName,
    PXGenericInqGrph.GIFieldState schema,
    string caption,
    bool isVisible)
  {
    object obj;
    if (e.Row is GenericResult row)
    {
      obj = this.Results.Cache.GetValue((object) row, fieldName);
    }
    else
    {
      object returnValue = e.ReturnValue;
      obj = returnValue != null ? (object) returnValue.ToString() : (object) null;
    }
    int? length = new int?();
    bool? isUnicode = new bool?(true);
    string fieldName1 = fieldName;
    bool? isKey = new bool?(false);
    int? required = new int?(0);
    bool? exclusiveValues = new bool?();
    PXFieldState instance = PXStringState.CreateInstance(obj, length, isUnicode, fieldName1, isKey, required, (string) null, (string[]) null, (string[]) null, exclusiveValues, (string) null);
    this.AdjustState(instance, e.ReturnState, fieldName, schema, caption, isVisible);
    e.ReturnState = (object) instance;
  }

  private void AdjustState(
    PXFieldState state,
    object returnState,
    string fieldName,
    PXGenericInqGrph.GIFieldState schema,
    string caption,
    bool isVisible)
  {
    this.Results.Cache.AdjustState(state);
    state.SetFieldName(fieldName);
    state.Visible = isVisible;
    state.Visibility = PXUIVisibility.Visible;
    string str = caption;
    if (string.IsNullOrEmpty(str) && returnState is PXFieldState pxFieldState)
      str = pxFieldState.DisplayName;
    if (string.IsNullOrEmpty(str) && schema != null && schema.State != null)
      str = schema.State.DisplayName;
    state.DisplayName = str;
  }

  private PXGenericInqGrph.GIFieldState GetResultColumnState(
    PXCache cache,
    GenericResult row,
    string alias,
    string field,
    string schema,
    object value = null,
    PXGenericInqGrph.GIDescriptionField descrField = null)
  {
    try
    {
      cache.NeverCloneAttributes = true;
      if (row == null && value != null)
      {
        cache.RaiseFieldSelecting(field, (object) null, ref value, true);
        return new PXGenericInqGrph.GIFieldState(value);
      }
      object obj = row == null || !row.Values.ContainsKey(alias) ? (object) null : row.Values[alias];
      PXFieldState state = this.GetStateFromSchemaField(schema) ?? this.GetStateForDescriptionField(cache, obj, descrField);
      if (state == null)
      {
        value = cache.GetStateExt(obj, field);
        return new PXGenericInqGrph.GIFieldState(value);
      }
      state.Value = obj != null ? cache.GetValueExt(obj, field) : (object) null;
      if (state.Value is PXFieldState)
        state.Value = ((PXFieldState) state.Value).Value;
      return new PXGenericInqGrph.GIFieldState(state, state.Value);
    }
    finally
    {
      cache.NeverCloneAttributes = false;
    }
  }

  private PXFieldState GetStateFromSchemaField(string schemaField)
  {
    if (this.BaseQueryDescription == null || string.IsNullOrEmpty(schemaField))
      return (PXFieldState) null;
    int length = schemaField.LastIndexOf('.');
    return length == -1 || length == schemaField.Length - 1 ? (PXFieldState) null : this.Caches[this.BaseQueryDescription.Tables[schemaField.Substring(0, length)].BqlTable].GetStateExt((object) null, schemaField.Substring(length + 1)) as PXFieldState;
  }

  private PXFieldState GetStateForDescriptionField(
    PXCache cache,
    object row,
    PXGenericInqGrph.GIDescriptionField descrField)
  {
    if (descrField == null)
      return (PXFieldState) null;
    PXFieldState stateExt = cache.GetStateExt(row, descrField.DescriptionFieldName) as PXFieldState;
    this.AdjustStateForDescriptionField(stateExt, descrField);
    return stateExt;
  }

  private void AdjustStateForDescriptionField(
    PXFieldState state,
    PXGenericInqGrph.GIDescriptionField descrField)
  {
    if (state == null)
      return;
    PXFieldState pxFieldState = descrField.SourceState.Value;
    state.ValueField = pxFieldState.DescriptionName;
    state.FieldList = pxFieldState.FieldList;
    state.HeaderList = pxFieldState.HeaderList;
    state.SelectorMode = pxFieldState.SelectorMode;
    state.ViewName = pxFieldState.ViewName;
    if (!state.Visibility.HasFlag((Enum) PXUIVisibility.SelectorVisible) && !state.Visibility.HasFlag((Enum) PXUIVisibility.Dynamic))
      return;
    state.Visibility = PXUIVisibility.Visible;
  }

  private string ResolveFieldDisplayName(
    string fieldName,
    string displayName,
    PXCache cache,
    string def)
  {
    if (!string.IsNullOrEmpty(def))
      return def;
    int num = displayName.IndexOf("-");
    return cache.GetBqlField(fieldName) == (System.Type) null && cache.Fields.Contains(fieldName) && num > 0 && num < displayName.Length ? displayName.Substring(num + 1) : displayName;
  }

  private void CollectDescriptionFields()
  {
    foreach (PXTable pxTable in this.BaseQueryDescription.UsedTables.Values)
    {
      PXCache cache = (pxTable.Graph ?? this).Caches[pxTable.CacheType];
      foreach (string field1 in (List<string>) cache.Fields)
      {
        string field = field1;
        PXSelectorAttribute selectorAttribute = cache.GetAttributesReadonly(field, true).OfType<PXSelectorAttribute>().FirstOrDefault<PXSelectorAttribute>((Func<PXSelectorAttribute, bool>) (s => s.DescriptionField != (System.Type) null));
        if (selectorAttribute != null)
        {
          string str = field + "_description";
          PXGenericInqGrph.GIDescriptionField descriptionField;
          if (cache.Fields.Contains(str))
          {
            descriptionField = new PXGenericInqGrph.GIDescriptionField(pxTable.Alias, field, str);
          }
          else
          {
            descriptionField = new PXGenericInqGrph.GIDescriptionField(pxTable.Alias, field, selectorAttribute.DescriptionField.Name, str);
            this._descriptionFields[descriptionField.GenericDescriptionFieldAlias] = descriptionField;
          }
          descriptionField.SetSourceStateFactory((Func<PXFieldState>) (() => (PXFieldState) cache.GetStateExt((object) null, field)));
          this._descriptionFields[descriptionField.GenericDescriptionFieldName] = descriptionField;
          this._descriptionFieldsBySourceField[descriptionField.GenericSourceFieldName] = descriptionField;
        }
      }
    }
  }

  [PXInternalUseOnly]
  public string GetTableName(string designId, string objectName)
  {
    Guid guid;
    if (string.IsNullOrWhiteSpace(objectName) || !GUID.TryParse(designId, ref guid))
      return (string) null;
    return ((GITable) PXSelectBase<GITable, PXSelect<GITable, Where<GITable.alias, Equal<Required<GITable.alias>>, And<GITable.designID, Equal<Required<GITable.designID>>>>>.Config>.Select((PXGraph) this, (object) objectName, (object) guid)).With<GITable, string>((Func<GITable, string>) (_ => _.Name));
  }

  [PXInternalUseOnly]
  public string GetTableName(string alias)
  {
    if (string.IsNullOrEmpty(alias) || this.Design == null)
      return (string) null;
    return ((GITable) PXSelectBase<GITable, PXSelect<GITable, Where<GITable.alias, Equal<Required<GITable.alias>>, And<GITable.designID, Equal<Required<GITable.designID>>>>>.Config>.Select((PXGraph) this, (object) alias, (object) this.Design.DesignID))?.Name;
  }

  [PXInternalUseOnly]
  public string GetTableAlias(string name)
  {
    if (string.IsNullOrEmpty(name) || this.Design == null)
      return (string) null;
    return ((GITable) PXSelectBase<GITable, PXSelect<GITable, Where<GITable.name, Equal<Required<GITable.name>>, And<GITable.designID, Equal<Required<GITable.designID>>>>>.Config>.Select((PXGraph) this, (object) name, (object) this.Design.DesignID))?.Alias;
  }

  /// <summary>Selects full row from primary view.</summary>
  internal object SelectPrimaryRow(
    PXGraph primaryGraph,
    PXCache primaryCache,
    GenericResult genericRow)
  {
    if (genericRow == null)
      return (object) null;
    List<GINavigationScreen> list = this.Description.NavigationScreens.Where<GINavigationScreen>((Func<GINavigationScreen, bool>) (ns => string.Equals(ns.Link, this.Design.PrimaryScreenID, StringComparison.OrdinalIgnoreCase))).ToList<GINavigationScreen>();
    GINavigationScreen navigationScreen1 = list.FirstOrDefault<GINavigationScreen>((Func<GINavigationScreen, bool>) (ns => string.Equals(ns.WindowMode, "I", StringComparison.OrdinalIgnoreCase)));
    if (navigationScreen1 != null)
    {
      list.Remove(navigationScreen1);
      list.Insert(0, navigationScreen1);
    }
    GINavigationParameter[] source = Array.Empty<GINavigationParameter>();
    foreach (GINavigationScreen navigationScreen2 in list)
    {
      GINavigationScreen navigation = navigationScreen2;
      source = this.Description.NavigationParameters.Where<GINavigationParameter>((Func<GINavigationParameter, bool>) (n =>
      {
        int? navigationScreenLineNbr = n.NavigationScreenLineNbr;
        int? lineNbr = navigation.LineNbr;
        return navigationScreenLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & navigationScreenLineNbr.HasValue == lineNbr.HasValue;
      })).ToArray<GINavigationParameter>();
      if (source.Length != 0)
        break;
    }
    if (source.Length == 0)
    {
      System.Type primaryType = primaryCache.GetItemType();
      GITable primaryTable = this.Description.Tables.First<GITable>((Func<GITable, bool>) (t => t.Name == primaryType.FullName));
      source = primaryCache.Keys.Select<string, GINavigationParameter>((Func<string, GINavigationParameter>) (k => new GINavigationParameter()
      {
        FieldName = k,
        ParameterName = $"{primaryTable.Alias}.{k}",
        IsExpression = new bool?(false)
      })).ToArray<GINavigationParameter>();
    }
    Dictionary<string, object> dictionary = ((IEnumerable<GINavigationParameter>) source).ToDictionary<GINavigationParameter, string, object>((Func<GINavigationParameter, string>) (n => n.FieldName), (Func<GINavigationParameter, object>) (n => this.EvaluateNavParamValue(genericRow, n)), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    object primaryRow = this._navigationService.GetPrimaryRow(primaryGraph.Views[primaryGraph.PrimaryView], (IReadOnlyDictionary<string, object>) dictionary);
    primaryGraph.Views[primaryGraph.PrimaryView].Cache.Current = primaryRow;
    primaryGraph.EnsureIfArchived();
    return primaryRow;
  }

  /// <summary>
  /// Selects fields that has combo box values in automation.
  /// </summary>
  /// <returns></returns>
  internal ISet<string> SelectAUComboFields()
  {
    PXSiteMapNode siteMapNode = GIScreenHelper.GetSiteMapNode(this.Design.With<GIDesign, string>((Func<GIDesign, string>) (_ => _.PrimaryScreenID)));
    string primaryView = this.PageIndexingService.GetPrimaryView(siteMapNode.GraphType);
    PXCacheInfo cache = GraphHelper.GetGraphView(siteMapNode.GraphType, primaryView).Cache;
    PXResultset<AUStepCombo> pxResultset = PXSelectBase<AUStepCombo, PXSelectGroupBy<AUStepCombo, Where<AUStepCombo.isActive, Equal<True>, And<AUStepCombo.screenID, Equal<Required<AUStepCombo.screenID>>, And<AUStepCombo.tableName, Equal<Required<AUStepCombo.tableName>>>>>, Aggregate<GroupBy<AUStepCombo.fieldName>>>.Config>.Select((PXGraph) this, (object) siteMapNode.ScreenID, (object) cache.CacheType.FullName);
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<AUStepCombo> pxResult in pxResultset)
    {
      AUStepCombo auStepCombo = (AUStepCombo) pxResult;
      stringSet.Add(auStepCombo.FieldName);
    }
    return (ISet<string>) stringSet;
  }

  [PXInternalUseOnly]
  public Func<GenericResult, object> GetRowWithNote { get; private set; } = (Func<GenericResult, object>) (gr => (object) null);

  [PXInternalUseOnly]
  public bool IsVirtualField(string fieldName) => this._virtualColumns.Contains(fieldName);

  [PXInternalUseOnly]
  public bool IsAuxiliaryField(string fieldName) => this._auxiliaryColumns.Contains(fieldName);

  private void ActivateDynamicFields(PXCache cache)
  {
    PXDBAttributeAttribute.Activate(cache);
    PXDBFieldAttribute.ActivateDynamicFields(cache);
    if (cache.Interceptor == null)
      return;
    foreach (System.Type table in cache.Interceptor.GetTables())
    {
      PXCache cach = this.Caches[table];
      if (cache != cach)
        this.ActivateDynamicFields(cach);
    }
  }

  /// <summary>
  /// Returns all suitable (GI-supported) fields for a DAC type.
  /// </summary>
  /// <param name="graph">Any <see cref="T:PX.Data.PXGraph" /> instance to retrieve the instance of <see cref="T:PX.Data.PXCache" /> for the particular DAC type.</param>
  /// <param name="dacType">DAC type.</param>
  /// <param name="predicate">A predicate used to additionally filter suitable fields. Takes <see cref="T:PX.Data.PXCache" /> instance for the particular DAC type and the field name as arguments.</param>
  /// <returns></returns>
  internal static IEnumerable<(bool IsKey, bool IsIdentity, bool IsDeletedDatabaseRecord, int Ordinal, string Field)> GetSuitableInquiryFieldsFromDacType(
    PXGraph graph,
    System.Type dacType,
    Func<PXCache, string, bool> predicate = null)
  {
    ExceptionExtensions.ThrowOnNull<PXGraph>(graph, nameof (graph), (string) null);
    ExceptionExtensions.ThrowOnNull<System.Type>(dacType, nameof (dacType), (string) null);
    HashSet<string> existingNames = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    PXCache cache = graph.Caches[dacType];
    int ordinal = 0;
    foreach (string field1 in (List<string>) cache.Fields)
    {
      string field = field1;
      System.Type bqlField = cache.GetBqlField(field);
      if ((!(bqlField != (System.Type) null) || BqlCommand.GetItemType(bqlField).IsAssignableFrom(dacType)) && (!(bqlField == (System.Type) null) || field.EndsWith("_Attributes") || field.EndsWith("Signed") || cache.IsKvExtAttribute(field)) && !cache.GetAttributes(field).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBTimestampAttribute)) && existingNames.Add(field))
      {
        PXFieldState state = (PXFieldState) cache.GetStateExt((object) null, field);
        if (state != null && !string.IsNullOrEmpty(state.DescriptionName) && (predicate == null || predicate(cache, field + "_description")))
          yield return (false, false, false, ordinal++, field + "_description");
        if (predicate == null || predicate(cache, field))
        {
          PXFieldState pxFieldState = state;
          yield return (pxFieldState != null ? pxFieldState.PrimaryKey : cache.Keys.Contains(field), string.Equals(cache.Identity, field, StringComparison.OrdinalIgnoreCase), false, ordinal++, field);
        }
        state = (PXFieldState) null;
        field = (string) null;
      }
    }
    string field2 = PXGenericInqGrph.GetDeletedDatabaseRecord(cache).Field;
    if (field2 != null && !cache.Fields.Contains(field2))
      yield return (false, false, true, ordinal, field2);
    string field3 = PXGenericInqGrph.GetDatabaseRecordStatus(cache).Field;
    if (field3 != null && !cache.Fields.Contains(field3))
      yield return (false, false, true, ordinal, field3);
  }

  private static void DisableUIAttributes(PXCache cache)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.Fields.SelectMany<string, PXEventSubscriberAttribute>(new Func<string, IEnumerable<PXEventSubscriberAttribute>>(cache.GetAttributes)).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a.AttributeLevel != 0)))
    {
      switch (subscriberAttribute)
      {
        case PXUIEnabledAttribute _:
        case PXUIRequiredAttribute _:
        case PXUIVerifyAttribute _:
          ((PXBaseConditionAttribute) subscriberAttribute).Condition = (System.Type) null;
          continue;
        default:
          continue;
      }
    }
  }

  private static bool ShouldUseExt(PXCache cache, string field, object value)
  {
    object newValue = value;
    try
    {
      cache.RaiseFieldUpdating(field, (object) null, ref newValue);
    }
    catch
    {
    }
    return !object.Equals(newValue, value);
  }

  internal static string GetExtFieldId(GIResult field)
  {
    if (field == null)
      throw new ArgumentNullException(nameof (field));
    if (!field.RowID.HasValue)
      throw new ArgumentException("RowID cannot be null.", nameof (field));
    return field.RowID.Value.ToString("N", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  internal static void TrySetAvailableValues(object s, string availableValues)
  {
    if (!(s is PXStringState pxStringState) || string.IsNullOrEmpty(availableValues))
      return;
    List<string> values = new List<string>();
    List<string> labels = new List<string>();
    PXGenericInqGrph.ParseAvailableValues(availableValues, values, labels);
    pxStringState.AllowedLabels = labels.ToArray();
    pxStringState.AllowedValues = values.ToArray();
  }

  internal static void ParseAvailableValues(
    string availableValues,
    List<string> values,
    List<string> labels)
  {
    if (values == null || labels == null || string.IsNullOrEmpty(availableValues))
      return;
    values.Clear();
    labels.Clear();
    string[] strArray1 = availableValues.Split(',');
    if (strArray1.Length == 0)
      return;
    foreach (string str in strArray1)
    {
      char[] chArray = new char[1]{ ';' };
      string[] strArray2 = str.Split(chArray);
      if (strArray2.Length == 2)
      {
        values.Add(strArray2[0]);
        labels.Add(strArray2[1]);
      }
    }
  }

  internal static System.Type GetFieldFromName(
    PXGenericInqGrph graph,
    string fieldName,
    IDictionary<string, GITable> tables)
  {
    if (string.IsNullOrEmpty(fieldName))
      return (System.Type) null;
    if (fieldName == typeof (CheckboxCombobox.checkbox).FullName || fieldName == $"{typeof (CheckboxCombobox.checkbox).DeclaringType.Name}.{typeof (CheckboxCombobox.checkbox).Name}")
      return PXBuildManager.GetType(typeof (CheckboxCombobox.checkbox).FullName, true);
    if (fieldName == typeof (CheckboxCombobox.combobox).FullName || fieldName == $"{typeof (CheckboxCombobox.combobox).DeclaringType.Name}.{typeof (CheckboxCombobox.combobox).Name}")
      return PXBuildManager.GetType(typeof (CheckboxCombobox.combobox).FullName, true);
    int length = fieldName.LastIndexOf('.');
    if (length == -1 || length == fieldName.Length - 1)
      return (System.Type) null;
    string key = fieldName.Substring(0, length).Trim();
    string field = fieldName.Substring(length + 1).Trim();
    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(field) || !tables.ContainsKey(key))
      return (System.Type) null;
    PXTable pxTable;
    return !graph.BaseQueryDescription.Tables.TryGetValue(key, out pxTable) ? (System.Type) null : (pxTable.Graph ?? graph).Caches[pxTable.CacheType].GetBqlField(field);
  }

  internal void EnsureCacheInitialised(PXGraph graph, System.Type cacheType)
  {
    if (this.ParameterCurrents.ContainsKey(cacheType))
      return;
    PXCache cach = graph.Caches[cacheType];
    using (new ReadOnlyScope(new PXCache[1]{ cach }))
    {
      bool flag;
      try
      {
        object obj = cach.Insert();
        flag = obj != null;
        cach.Current = obj;
      }
      catch
      {
        flag = cach.Insert((IDictionary) new Dictionary<string, object>()) > 0;
      }
      if (!flag)
        return;
      cach.SetStatus(cach.Current, PXEntryStatus.Notchanged);
      this.ParameterCurrents[cacheType] = cach.Current;
    }
  }

  [PXInternalUseOnly]
  internal static PXGenericInqGrph CreateSubQueryInstance(Guid designID)
  {
    try
    {
      return PXGenericInqGrph.CreateInstanceWithPrefix(designID.ToString(), (string) null, fetchSchema: false, isSubQueryInstance: true);
    }
    catch (PXException ex)
    {
      throw new PXException("This inquiry cannot be displayed because it refers to a non-existent generic inquiry, table, or field. For details, see the Trace log.");
    }
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstance(Guid designID, bool fetchSchema = true)
  {
    return PXGenericInqGrph.CreateInstance(designID.ToString(), (string) null, fetchSchema: fetchSchema);
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstance(
    string designID,
    string name,
    Dictionary<string, string> parameters = null,
    bool fetchSchema = true)
  {
    return PXGenericInqGrph.CreateInstanceWithPrefix(designID, name, parameters, fetchSchema);
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstanceWithPrefix(
    string designID,
    string name,
    Dictionary<string, string> parameters = null,
    bool fetchSchema = true,
    string prefix = null,
    bool isSubQueryInstance = false)
  {
    PXGenericInqGrph instance = PXGraph.CreateInstance<PXGenericInqGrph>(string.IsNullOrEmpty(prefix) ? PXContext.GetScreenID() : prefix);
    instance.IsSubQueryInstance = isSubQueryInstance;
    instance.PrepareCaches(designID, name, parameters, fetchSchema);
    instance.Results.Cache.NormalizeUpdated();
    return instance;
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstance(string screenID)
  {
    return PXGenericInqGrph.CreateInstance(screenID, (string) null);
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstance(string screenID, string prefix)
  {
    return PXGenericInqGrph.CreateInstance((!string.IsNullOrEmpty(screenID) ? PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID) : throw new ArgumentNullException(nameof (screenID))) ?? throw new PXException("A site map node with the screen ID {0} cannot be found.", new object[1]
    {
      (object) screenID
    }), prefix: prefix);
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstance(
    PXSiteMapNode node,
    bool fetchSchema = true,
    string prefix = null)
  {
    string designID = node != null ? PXUrl.GetParameter(node.Url, "id") : throw new ArgumentNullException(nameof (node));
    string parameter = PXUrl.GetParameter(node.Url, "name");
    if (string.IsNullOrEmpty(designID) && string.IsNullOrEmpty(parameter))
      throw new PXException("A generic inquiry with this ID or name cannot be found. Please check the URL of the site map node for this inquiry.");
    return PXGenericInqGrph.CreateInstanceWithPrefix(designID, parameter, prefix: prefix);
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstance(GIDescription schema)
  {
    if (schema == null)
      throw new ArgumentNullException(nameof (schema));
    PXGenericInqGrph instance = PXGraph.CreateInstance<PXGenericInqGrph>();
    instance.PrepareCaches(schema);
    return instance;
  }

  /// <summary>
  /// Create an instance of the Generic Inquiry graph with an automatically generated schema that contains all supported fields for a particular DAC.
  /// </summary>
  /// <param name="dacType">DAC type.</param>
  /// <param name="showDeletedRecords">Whether or not to show the 'Deleted Database Record' field, if any.</param>
  /// <returns>The graph instance.</returns>
  [PXInternalUseOnly]
  public static PXGenericInqGrph CreateInstance(
    System.Type dacType,
    bool showDeletedRecords = true,
    bool showArchivedRecords = false)
  {
    ExceptionExtensions.ThrowOnNull<System.Type>(dacType, nameof (dacType), (string) null);
    PXGenericInqGrph instance = PXGraph.CreateInstance<PXGenericInqGrph>();
    IOrderedEnumerable<(bool, bool, bool, int, string)> source = PXGenericInqGrph.GetSuitableInquiryFieldsFromDacType((PXGraph) instance, dacType).OrderByDescending<(bool, bool, bool, int, string), bool>((Func<(bool, bool, bool, int, string), bool>) (f => f.IsDeletedDatabaseRecord)).ThenByDescending<(bool, bool, bool, int, string), bool>((Func<(bool, bool, bool, int, string), bool>) (f => f.IsIdentity)).ThenByDescending<(bool, bool, bool, int, string), bool>((Func<(bool, bool, bool, int, string), bool>) (f => f.IsKey)).ThenBy<(bool, bool, bool, int, string), string>((Func<(bool, bool, bool, int, string), string>) (f => f.Field));
    Guid designId = Guid.Empty;
    string tableAlias = dacType.Name;
    string name = PXCacheNameAttribute.GetName(dacType);
    GIDescription def = new GIDescription(designId)
    {
      Design = new GIDesign()
      {
        DesignID = new Guid?(designId),
        Name = string.IsNullOrEmpty(name) ? string.Format(PXLocalizer.Localize("Preview - {0}"), (object) dacType.Name) : string.Format(PXLocalizer.Localize("Preview - {0} ({1})"), (object) dacType.Name, (object) name),
        ShowDeletedRecords = new bool?(showDeletedRecords),
        ShowArchivedRecords = new bool?(showArchivedRecords)
      },
      Tables = EnumerableExtensions.AsSingleEnumerable<GITable>(new GITable()
      {
        DesignID = new Guid?(designId),
        Alias = tableAlias,
        Name = dacType.FullName
      }),
      Results = (IEnumerable<GIResult>) source.Select<(bool, bool, bool, int, string), GIResult>((Func<(bool, bool, bool, int, string), GIResult>) (f => new GIResult()
      {
        DesignID = new Guid?(designId),
        LineNbr = new int?(f.Ordinal),
        SortOrder = new int?(f.Ordinal),
        ObjectName = tableAlias,
        Field = f.Field,
        NoteID = new Guid?(Guid.NewGuid()),
        RowID = new Guid?(Guid.NewGuid()),
        FastFilter = new bool?(f.IsKey),
        IsActive = new bool?(true),
        IsVisible = new bool?(true),
        DefaultNav = new bool?(true)
      })).ToArray<GIResult>()
    };
    instance.PrepareCaches(def);
    foreach (string field1 in (List<string>) instance.Results.Cache.Fields)
    {
      string field = field1;
      instance.Results.Cache.FieldSelectingEvents[field] += (PXFieldSelecting) ((s, e) =>
      {
        if (!(e.ReturnState is PXFieldState returnState2))
          return;
        string[] strArray = field.Split('_', 2);
        if (strArray.Length != 2)
          return;
        returnState2.DisplayName = string.Equals(returnState2.DisplayName, field) ? strArray[1] : string.Format(PXLocalizer.Localize("{0} ({1})"), (object) returnState2.DisplayName, (object) strArray[1]);
      });
    }
    return instance;
  }

  [PXInternalUseOnly]
  public static GIDescription GetDescription(string screenID)
  {
    if (screenID == null)
      return (GIDescription) null;
    PXSiteMapNode pxSiteMapNode = PXSiteMap.Provider.FindSiteMapNodesByScreenIDUnsecure(screenID).FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (n => PXSiteMap.IsGenericInquiry(n.Url)));
    if (pxSiteMapNode == null)
      return (GIDescription) null;
    Guid result;
    return Guid.TryParse(PXUrl.GetParameter(pxSiteMapNode.Url, "id"), out result) && PXGenericInqGrph.Def[result] != null ? PXGenericInqGrph.Def[result] : PXGenericInqGrph.Def[PXUrl.GetParameter(pxSiteMapNode.Url, "name")];
  }

  [PXInternalUseOnly]
  public static string GetFieldName(PXSort sort)
  {
    if (sort == null || sort.DataField == null)
      throw new ArgumentNullException("sort || sort.DataField");
    if (sort.DataField is PXFieldValue)
      return (sort.DataField as PXFieldValue).FieldName;
    return sort.DataField is PXCalcedValue ? (sort.DataField as PXCalcedValue).ToString() : "";
  }

  internal static bool IsVirtualField(PXCache cache, string field, System.Type bqlTable = null)
  {
    if (string.IsNullOrEmpty(field))
      throw new ArgumentNullException(nameof (field));
    if (bqlTable == (System.Type) null)
      bqlTable = cache.GetItemType();
    Dictionary<(System.Type, System.Type, string), bool> dictionary1 = PXContext.GetSlot<Dictionary<(System.Type, System.Type, string), bool>>("GIVirtualFieldsCache") ?? PXContext.SetSlot<Dictionary<(System.Type, System.Type, string), bool>>("GIVirtualFieldsCache", new Dictionary<(System.Type, System.Type, string), bool>());
    (System.Type, System.Type, string) key1 = (cache.GetItemType(), bqlTable, field.ToLowerInvariant());
    bool flag1;
    if (dictionary1.TryGetValue(key1, out flag1))
      return flag1;
    PXEntityAttribute[] array = cache.GetAttributesReadonly(field).OfType<PXEntityAttribute>().ToArray<PXEntityAttribute>();
    if (array.Length != 0 && ((IEnumerable<PXEntityAttribute>) array).All<PXEntityAttribute>((Func<PXEntityAttribute, bool>) (attr => !attr.IsDBField)))
    {
      dictionary1[key1] = true;
      return true;
    }
    PXCommandPreparingEventArgs.FieldDescription description;
    cache.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, bqlTable, out description);
    if (description?.Expr == null)
      cache.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.External, bqlTable, out description);
    Dictionary<(System.Type, System.Type, string), bool> dictionary2 = dictionary1;
    (System.Type, System.Type, string) key2 = key1;
    SQLExpression expr = description?.Expr;
    int num;
    bool flag2 = (num = expr == null ? 1 : 0) != 0;
    dictionary2[key2] = num != 0;
    return flag2;
  }

  internal static (string Field, System.Type BqlTable) GetDeletedDatabaseRecord(PXCache cache)
  {
    System.Type type1 = cache.BqlTable;
    if (cache.BqlSelect != null)
    {
      System.Type type2 = PXCache.GetBqlTable(((IEnumerable<System.Type>) cache.BqlSelect.GetTables()).FirstOrDefault<System.Type>());
      if ((object) type2 == null)
        type2 = type1;
      type1 = type2;
    }
    TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(type1.Name);
    if (tableStructure != null && tableStructure.HasAnyDatabaseDeletedRecord())
    {
      if (tableStructure.HasDatabaseDeletedRecord())
        return ("DeletedDatabaseRecord", type1);
      if (tableStructure.HasUsrDatabaseDeletedRecord())
        return ("UsrDeletedDatabaseRecord", type1);
    }
    return ((string) null, (System.Type) null);
  }

  internal static (string Field, System.Type BqlTable) GetDatabaseRecordStatus(PXCache cache)
  {
    System.Type type1 = cache.BqlTable;
    if (cache.BqlSelect != null)
    {
      System.Type type2 = PXCache.GetBqlTable(((IEnumerable<System.Type>) cache.BqlSelect.GetTables()).FirstOrDefault<System.Type>());
      if ((object) type2 == null)
        type2 = type1;
      type1 = type2;
    }
    TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(type1.Name);
    return tableStructure != null && tableStructure.HasDatabaseRecordStatus() ? ("DatabaseRecordStatus", type1) : ((string) null, (System.Type) null);
  }

  [PXInternalUseOnly]
  public static PXGenericInqGrph.ParametersChangedIndexer ParametersChanged { get; } = new PXGenericInqGrph.ParametersChangedIndexer();

  [PXInternalUseOnly]
  public static string GetNavigateToActionName(string fieldName) => "NavigateTo$" + fieldName;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Selected")]
  protected void GenericResult_Selected_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Row Number", Visible = false)]
  protected void GenericResult_Row_CacheAttached(PXCache cache)
  {
  }

  protected void GenericResult_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    this.UpdateSidePanelVisible();
  }

  /// <summary>Collect localizable string attributes in formulas.</summary>
  private void CollectLocalizeStringAttributesInFormulas()
  {
    if (!PXDBLocalizableStringAttribute.IsEnabled)
      return;
    foreach (PXFormulaField formulaField in this.BaseQueryDescription.FormulaFields)
    {
      string key = $"{formulaField.Table.Alias}_{formulaField.Name}";
      if (!this._formulaFieldsWithLocalizableStringAttributes.ContainsKey(key))
      {
        List<PXDBLocalizableStringAttribute> localizableStringAttributeList = this.CollectLocalizableAttributesFromFormula((PXGraph) this, formulaField);
        if (localizableStringAttributeList.Count > 0)
          this._formulaFieldsWithLocalizableStringAttributes[key] = localizableStringAttributeList;
      }
    }
  }

  private List<PXDBLocalizableStringAttribute> CollectLocalizableAttributesFromFormula(
    PXGraph graph,
    PXFormulaField formulaField)
  {
    List<PXDBLocalizableStringAttribute> formulaLocalizableStringAttributes = new List<PXDBLocalizableStringAttribute>();
    formulaField.Value.GetExpression((Func<string, SQLExpression>) (fieldFullName =>
    {
      (string DacName, string FieldName)? andFieldNamePair = this.GetDacAndFieldNamePair(fieldFullName);
      PXTable pxTable;
      if (andFieldNamePair.HasValue && this.BaseQueryDescription.Tables.TryGetValue(andFieldNamePair.Value.DacName, out pxTable))
      {
        PXCache cach = ((PXGraph) pxTable.Graph ?? graph).Caches[pxTable.CacheType];
        if (cach != null)
        {
          PXDBLocalizableStringAttribute localizableStringAttribute = cach.GetAttributesReadonly(andFieldNamePair.Value.FieldName).OfType<PXDBLocalizableStringAttribute>().FirstOrDefault<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (l => l.MultiLingual));
          if (localizableStringAttribute != null)
            formulaLocalizableStringAttributes.Add(localizableStringAttribute);
          else if (pxTable.Graph != null)
          {
            pxTable.Graph.CollectLocalizeStringAttributesInFormulas();
            List<PXDBLocalizableStringAttribute> collection;
            if (pxTable.Graph._formulaFieldsWithLocalizableStringAttributes.TryGetValue(andFieldNamePair.Value.FieldName, out collection))
              formulaLocalizableStringAttributes.AddRange((IEnumerable<PXDBLocalizableStringAttribute>) collection);
          }
        }
      }
      return (SQLExpression) new Column(fieldFullName);
    }));
    return formulaLocalizableStringAttributes;
  }

  private (string DacName, string FieldName)? GetDacAndFieldNamePair(string fullDacFieldName)
  {
    string[] strArray1;
    if (fullDacFieldName == null)
      strArray1 = (string[]) null;
    else
      strArray1 = fullDacFieldName.Split(new string[1]
      {
        "."
      }, StringSplitOptions.RemoveEmptyEntries);
    string[] strArray2 = strArray1;
    return strArray2 == null || strArray2.Length != 2 ? new (string, string)?() : new (string, string)?((strArray2[0], strArray2[1]));
  }

  private List<PXDBLocalizableStringAttribute> GetFormulaLocalizableStringAttributes(
    string fullFormulaName)
  {
    List<PXDBLocalizableStringAttribute> localizableStringAttributeList;
    return !this._formulaFieldsWithLocalizableStringAttributes.TryGetValue(fullFormulaName, out localizableStringAttributeList) ? (List<PXDBLocalizableStringAttribute>) null : localizableStringAttributeList;
  }

  private string LocalizeFormulaField(
    PXCache cache,
    string nonLocalizedString,
    string formulaString,
    List<PXDBLocalizableStringAttribute> localizableStringAttributes)
  {
    if (string.IsNullOrWhiteSpace(nonLocalizedString))
      return nonLocalizedString;
    PXGraph graph = cache.Graph;
    string str = (string) null;
    foreach (PXDBLocalizableStringAttribute localizableStringAttribute in localizableStringAttributes)
    {
      string localizedString;
      if (localizableStringAttribute.TryTranslateValue(graph, nonLocalizedString, out localizedString) && localizedString != null)
      {
        if (str == null)
          str = localizedString;
        else if (localizedString != str)
        {
          PXTrace.WriteWarning("Could not localize GI formula {Formula} because there are multiple possible translations", (object) formulaString);
          return nonLocalizedString;
        }
      }
    }
    if (string.IsNullOrWhiteSpace(str))
      str = nonLocalizedString;
    return str;
  }

  protected IEnumerable results()
  {
    this.InitializeScreenInfo();
    PXQueryDescription queryDescription = this.GetCurrentQueryDescription();
    if (queryDescription.Parameters.Values.Any<PXParameter>((Func<PXParameter, bool>) (v => v.Required && v.Value == null)))
      return (IEnumerable) Enumerable.Empty<GenericResult>();
    bool flag = this.ActiveVirtualField(queryDescription);
    IEnumerable enumerable;
    if ((queryDescription.RetrieveTotalRowCount || queryDescription.RetrieveTotals) && !flag)
    {
      PXResultset<GenericResult> pxResultset = this.DoSelectAggregate(queryDescription, true);
      this.TotalCurrent = (GenericResult) pxResultset[0];
      enumerable = (IEnumerable) pxResultset;
    }
    else
    {
      IEnumerable<GenericResult> source = this.DoSelect(queryDescription);
      IEnumerable<GenericResult> ReturnedResults = source;
      if (((queryDescription.TotalFields.Count <= 0 ? 0 : (this.TotalCurrent == null ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      {
        List<GenericResult> list = source.ToList<GenericResult>();
        List<object> resultsFilteredInMemory = this.FilterResultsFromDBInMemory(list, queryDescription);
        (this.TotalCurrent, ReturnedResults) = this.CalcTotals(list, resultsFilteredInMemory, queryDescription);
      }
      enumerable = (IEnumerable) ReturnedResults;
    }
    if (!queryDescription.ResetTopCount)
      PXView.StartRow = 0;
    foreach (KeyValuePair<System.Type, PXCache> cach in (Dictionary<System.Type, PXCache>) this.Caches)
      cach.Value.IsDirty = false;
    return enumerable;
  }

  private List<object> FilterResultsFromDBInMemory(
    List<GenericResult> resultsFromDB,
    PXQueryDescription descr)
  {
    if (resultsFromDB.Count == 0)
      return new List<object>();
    GIFilteredProcessing.GIParametrizedView view = this.Results.View as GIFilteredProcessing.GIParametrizedView;
    List<object> list = EnumerableExtensions.ToList<object>(resultsFromDB.Cast<object>(), resultsFromDB.Count);
    PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
    view.prepareFilters(ref filters);
    view.FilterResult(list, filters, true);
    return list;
  }

  private (GenericResult Total, IEnumerable<GenericResult> ReturnedResults) CalcTotals(
    List<GenericResult> resultsFromDB,
    List<object> resultsFilteredInMemory,
    PXQueryDescription descr)
  {
    GenericResult rowFromPrototype = this.CreateAggregateRowFromPrototype(resultsFromDB.FirstOrDefault<GenericResult>());
    PXView.Filters.Clear();
    List<GenericResult> list = EnumerableExtensions.ToList<GenericResult>(resultsFilteredInMemory.Cast<GenericResult>(), resultsFilteredInMemory.Count);
    IEnumerable<(PXAggregateField Field, object Value)> aggregatedValues = this.CalculateAggregatedValues((IEnumerable<PXAggregateField>) descr.TotalFields, (IEnumerable<GenericResult>) list);
    int num = 0;
    foreach ((PXAggregateField Field, object Value) in aggregatedValues)
    {
      AggregateFunction? function = Field.Function;
      AggregateFunction aggregateFunction = AggregateFunction.Count;
      string str = function.GetValueOrDefault() == aggregateFunction & function.HasValue ? Field.Alias : (PXView.RetrieveTotalRowCount ? Field.Name : "Aggr" + num++.ToString("0000"));
      string fieldName = $"{Field.Table.Alias}_{str}";
      this.Results.Cache.SetValue((object) rowFromPrototype, fieldName, Value);
    }
    return descr.RetrieveTotalRowCount || descr.RetrieveTotals ? (rowFromPrototype, EnumerableExtensions.AsSingleEnumerable<GenericResult>(rowFromPrototype)) : (rowFromPrototype, (IEnumerable<GenericResult>) list);
  }

  private GenericResult CreateAggregateRowFromPrototype(GenericResult rowPrototype)
  {
    if (rowPrototype == null)
      return this.Results.Cache.CreateInstance() as GenericResult;
    GenericResult copy = PXCache<GenericResult>.CreateCopy(rowPrototype);
    foreach (string field in (List<string>) this.Results.Cache.Fields)
      this.Results.Cache.SetValue((object) copy, field, (object) null);
    return copy;
  }

  private IEnumerable<(PXAggregateField Field, object Value)> CalculateAggregatedValues(
    IEnumerable<PXAggregateField> totalFields,
    IEnumerable<GenericResult> resultsToAggregate)
  {
    PXCache resultsCache = (PXCache) this.Results.Cache;
    foreach (PXAggregateField totalField in totalFields)
    {
      string field = $"{totalField.Table.Alias}_{totalField.Name}";
      IEnumerable<object> objects = resultsToAggregate.Select<GenericResult, object>((Func<GenericResult, object>) (r => resultsCache.GetValue((object) r, field)));
      AggregateFunction? function = totalField.Function;
      AggregateFunction aggregateFunction = AggregateFunction.Count;
      if (function.GetValueOrDefault() == aggregateFunction & function.HasValue)
      {
        int count = objects.ToHashSet<object>().Count;
        yield return (totalField, (object) count);
      }
      else
      {
        object obj1 = (object) null;
        object obj2 = objects.FirstOrDefault<object>((Func<object, bool>) (v => v != null));
        if (obj2 == null)
        {
          obj1 = (object) null;
        }
        else
        {
          TypeCode typeCode = System.Type.GetTypeCode(obj2.GetType());
          function = totalField.Function;
          if (function.HasValue)
          {
            switch (function.GetValueOrDefault())
            {
              case AggregateFunction.Avg:
                obj1 = this.GetAverage(objects, typeCode);
                break;
              case AggregateFunction.Max:
                obj1 = this.GetMax(objects, typeCode);
                break;
              case AggregateFunction.Min:
                obj1 = this.GetMin(objects, typeCode);
                break;
              case AggregateFunction.Sum:
                obj1 = this.GetSum(objects, typeCode);
                break;
            }
          }
        }
        yield return (totalField, obj1);
      }
    }
  }

  private object GetAverage(IEnumerable<object> values, TypeCode type)
  {
    switch (type)
    {
      case TypeCode.Int16:
        return (object) values.Average<object>((Func<object, int>) (s => (int) (short) s));
      case TypeCode.Int32:
        return (object) values.Average<object>((Func<object, int>) (s => (int) s));
      case TypeCode.Int64:
        return (object) values.Average<object>((Func<object, long>) (s => (long) s));
      case TypeCode.Double:
        return (object) values.Average<object>((Func<object, double>) (s => (double) s));
      case TypeCode.Decimal:
        return (object) values.Average<object>((Func<object, Decimal>) (s => (Decimal) s));
      default:
        return (object) null;
    }
  }

  private object GetMax(IEnumerable<object> values, TypeCode type)
  {
    switch (type)
    {
      case TypeCode.Int16:
        return (object) values.Max<object, short>((Func<object, short>) (s => (short) s));
      case TypeCode.Int32:
        return (object) values.Max<object>((Func<object, int>) (s => (int) s));
      case TypeCode.Int64:
        return (object) values.Max<object>((Func<object, long>) (s => (long) s));
      case TypeCode.Double:
        return (object) values.Max<object>((Func<object, double>) (s => (double) s));
      case TypeCode.Decimal:
        return (object) values.Max<object>((Func<object, Decimal>) (s => (Decimal) s));
      case TypeCode.DateTime:
        return (object) values.Max<object, System.DateTime>((Func<object, System.DateTime>) (s => (System.DateTime) s));
      case TypeCode.String:
        return (object) values.Max<object, string>((Func<object, string>) (s => s?.ToString()));
      default:
        return (object) null;
    }
  }

  private object GetMin(IEnumerable<object> values, TypeCode type)
  {
    switch (type)
    {
      case TypeCode.Int16:
        return (object) values.Min<object, short>((Func<object, short>) (s => (short) s));
      case TypeCode.Int32:
        return (object) values.Min<object>((Func<object, int>) (s => (int) s));
      case TypeCode.Int64:
        return (object) values.Min<object>((Func<object, long>) (s => (long) s));
      case TypeCode.Double:
        return (object) values.Min<object>((Func<object, double>) (s => (double) s));
      case TypeCode.Decimal:
        return (object) values.Min<object>((Func<object, Decimal>) (s => (Decimal) s));
      case TypeCode.DateTime:
        return (object) values.Min<object, System.DateTime>((Func<object, System.DateTime>) (s => (System.DateTime) s));
      case TypeCode.String:
        return (object) values.Min<object, string>((Func<object, string>) (s => s?.ToString()));
      default:
        return (object) null;
    }
  }

  private object GetSum(IEnumerable<object> values, TypeCode type)
  {
    switch (type)
    {
      case TypeCode.Int16:
        return (object) values.Sum<object>((Func<object, int>) (s => (int) (short) s));
      case TypeCode.Int32:
        return (object) values.Sum<object>((Func<object, int>) (s => (int) s));
      case TypeCode.Int64:
        return (object) values.Sum<object>((Func<object, long>) (s => (long) s));
      case TypeCode.Double:
        return (object) values.Sum<object>((Func<object, double>) (s => (double) s));
      case TypeCode.Decimal:
        return (object) values.Sum<object>((Func<object, Decimal>) (s => (Decimal) s));
      default:
        return (object) null;
    }
  }

  private ISet<string> GetRestrictedFields(PXQueryDescription descr)
  {
    HashSet<string> hashSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (GIResult giResult in this._resultColumns.Where<GIResult>((Func<GIResult, bool>) (c =>
    {
      if (this.VisibleColumns != null && !this.VisibleColumns.Contains(c.FieldName))
        return false;
      return !this.Results.View.RestrictedFields.Any() || this.Results.View.RestrictedFields.Contains(new RestrictedField(typeof (GenericResult), c.FieldName));
    })))
    {
      PXGenericInqGrph.GIDescriptionField descriptionField;
      if (this._descriptionFields.TryGetValue(giResult.FieldName, out descriptionField) && descriptionField.DescriptionFieldAlias != null)
        hashSet.Add($"{descriptionField.TableAlias}_{descriptionField.DescriptionFieldName}");
      else
        hashSet.Add(giResult.FieldName);
    }
    this.GetSpecialRestrictedItems(hashSet, descr, true);
    foreach (PXGenericInqGrph.GIDescriptionField descriptionField in this._descriptionFields.Values)
    {
      if (hashSet.Contains($"{descriptionField.TableAlias}_{descriptionField.SourceFieldName}"))
        hashSet.Add($"{descriptionField.TableAlias}_{descriptionField.DescriptionFieldName}");
    }
    return (ISet<string>) hashSet;
  }

  private IEnumerable<GenericResult> DoSelect(PXQueryDescription descr)
  {
    return this.DoSelect(descr, PXView.StartRow, PXView.MaximumRows);
  }

  [PXInternalUseOnly]
  public IEnumerable<GenericResult> DoSelect(PXQueryDescription descr, int startRow, int selectTop)
  {
    if (!descr.ShowDeletedRecords && !descr.ShowArchivedRecords)
      return GetScopedRecords();
    using (new CompositeDisposable(new IDisposable[2]
    {
      descr.ShowDeletedRecords ? (IDisposable) new PXReadDeletedScope() : Disposable.Empty,
      descr.ShowArchivedRecords ? (IDisposable) new PXReadThroughArchivedScope() : Disposable.Empty
    }))
      return (IEnumerable<GenericResult>) new PreEnumeratedEnumerable<GenericResult>(GetScopedRecords());

    IEnumerable<GenericResult> GetScopedRecords()
    {
      return EnumerableHelper.Using<GenericResult, CompositeDisposable>((Func<CompositeDisposable>) (() => new CompositeDisposable(new IDisposable[2]
      {
        (IDisposable) new PXGenericInqGrph.DynamicFieldsScope(this),
        (IDisposable) new PXStreamingQueryScope()
      })), (Func<CompositeDisposable, IEnumerable<GenericResult>>) (scope =>
      {
        ISet<string> restrictedFields = this.GetRestrictedFields(descr);
        scope.Add((IDisposable) new PXFieldScope(this.Results.View, (IEnumerable<string>) restrictedFields, false));
        if (this.Options.Value.OptimizeRowSelecting)
          scope.Add((IDisposable) new PXGenericInqGrph.PXRowSelectingFieldScope(this, descr, (IEnumerable<string>) restrictedFields));
        return this.GetRecords(descr, startRow, selectTop, scope, (Func<Query, PXDataValue[], IEnumerableLinqWrapper<PXDataRecord>>) ((query, parameters) => (IEnumerableLinqWrapper<PXDataRecord>) new EnumerableWrapper<PXDataRecord>(PXDatabase.Provider.Select(query, (IEnumerable<PXDataValue>) parameters, (System.Action<PXDatabaseProvider.ExecutionParameters>) (c =>
        {
          c.CommandTimeout *= 10;
          c.TraceQuery = true;
        }))))).AsEnumerable();
      }));
    }
  }

  [PXInternalUseOnly]
  public IAsyncEnumerable<GenericResult> DoSelectAsync(
    CancellationToken token,
    PXQueryDescription descr,
    int startRow,
    int selectTop)
  {
    return descr.ShowDeletedRecords || descr.ShowArchivedRecords ? (IAsyncEnumerable<GenericResult>) new PreEnumeratedAsyncEnumerable<GenericResult>((Func<IDisposable>) (() => (IDisposable) new CompositeDisposable(new IDisposable[2]
    {
      descr.ShowDeletedRecords ? (IDisposable) new PXReadDeletedScope() : Disposable.Empty,
      descr.ShowArchivedRecords ? (IDisposable) new PXReadThroughArchivedScope() : Disposable.Empty
    })), GetScopedRecords()) : GetScopedRecords();

    IAsyncEnumerable<GenericResult> GetScopedRecords()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IAsyncEnumerable<GenericResult>) new PXGenericInqGrph.\u003C\u003Ec__DisplayClass243_0.\u003C\u003CDoSelectAsync\u003Eg__GetScopedRecords\u007C1\u003Ed(-2)
      {
        \u003C\u003E4__this = this
      };
    }
  }

  private IEnumerableLinqWrapper<GenericResult> GetRecords(
    PXQueryDescription descr,
    int startRow,
    int selectTop,
    CompositeDisposable scope,
    Func<Query, PXDataValue[], IEnumerableLinqWrapper<PXDataRecord>> selector)
  {
    BqlGenericCommand bqlGenericCommand = new BqlGenericCommand((PXGraph) this, (PXCache) this.Results.Cache, descr);
    BqlGenericCommand command = bqlGenericCommand;
    int topCount;
    int skip;
    if (descr.ResetTopCount)
    {
      topCount = descr.SelectTop;
      skip = 0;
    }
    else
    {
      topCount = descr.SelectTop <= 0 || selectTop <= descr.SelectTop && selectTop != 0 ? selectTop : descr.SelectTop;
      skip = PXView.ReverseOrder ? System.Math.Abs(startRow + selectTop) : startRow;
      if (!PXView.ReverseOrder && topCount + skip > descr.SelectTop && startRow < descr.SelectTop)
        topCount = descr.SelectTop - skip;
    }
    int row = 1;
    bool anySelected = NonGenericIEnumerableExtensions.Any_(this.Results.Cache.Updated);
    Query query = command.GetQuery((PXGraph) this, this.Results.View, (long) topCount, (long) skip);
    PXDataValue[] parameters = bqlGenericCommand.Parameters;
    IEnumerableLinqWrapper<PXDataRecord> enumerableLinqWrapper = selector(query, parameters);
    PXDataRecordMap map = command.RecordMapEntries != null ? (PXDataRecordMap) scope.AddToDisposableScope<GIDataRecordMap>(new GIDataRecordMap(command.RecordMapEntries, this.GetRecordRestrictedFields(descr, this.Results.View.RestrictedFields))) : (PXDataRecordMap) null;
    PXTable[] tables = bqlGenericCommand.GetTables();
    bool isTableChanging = TableChangingScope.IsScoped;
    Func<PXDataRecord, GenericResult> selector1 = (Func<PXDataRecord, GenericResult>) (r =>
    {
      GenericResult row1 = this.ReadGenericRow(r, map, (IEnumerable<PXTable>) tables).Row;
      r._isTableChanging = isTableChanging;
      row1.Row = new int?(row++);
      if (anySelected)
      {
        GenericResult genericResult = this.Results.Cache.Locate(row1);
        if (genericResult != null)
          row1.Selected = genericResult.Selected;
      }
      return row1;
    });
    return enumerableLinqWrapper.Select<GenericResult>(selector1).Where((Func<GenericResult, bool>) (gr =>
    {
      if (!GISelectedOnlyScope.IsDefined)
        return true;
      bool? selected = gr.Selected;
      bool flag = true;
      return selected.GetValueOrDefault() == flag & selected.HasValue;
    }));
  }

  internal PXResultset<GenericResult> DoSelectAggregate(
    PXQueryDescription descr,
    bool decreaseTimeout)
  {
    PXResult<GenericResult> pxResult = EnumerableHelper.Using<PXResult<GenericResult>, CompositeDisposable>((Func<CompositeDisposable>) (() => new CompositeDisposable(new IDisposable[4]
    {
      descr.ShowDeletedRecords ? (IDisposable) new PXReadDeletedScope() : Disposable.Empty,
      descr.ShowArchivedRecords ? (IDisposable) new PXReadThroughArchivedScope() : Disposable.Empty,
      (IDisposable) new PXGenericInqGrph.DynamicFieldsScope(this),
      (IDisposable) new PXStreamingQueryScope()
    })), (Func<CompositeDisposable, IEnumerable<PXResult<GenericResult>>>) (scope => this.GetAggregatedRecords(descr, scope, (Func<Query, PXDataValue[], IEnumerableLinqWrapper<PXDataRecord>>) ((query, parameters) => (IEnumerableLinqWrapper<PXDataRecord>) new EnumerableWrapper<PXDataRecord>(PXDatabase.Provider.Select(query, (IEnumerable<PXDataValue>) parameters, (System.Action<PXDatabaseProvider.ExecutionParameters>) (c =>
    {
      if (descr.RetrieveTotalRowCount & decreaseTimeout)
        c.CommandTimeout /= 5;
      else
        c.CommandTimeout *= 5;
      c.TraceQuery = true;
    }))))).AsEnumerable().Take<PXResult<GenericResult>>(1))).FirstOrDefault<PXResult<GenericResult>>();
    if (pxResult == null)
      return new PXResultset<GenericResult>();
    return new PXResultset<GenericResult>() { pxResult };
  }

  internal PXAsyncResultset<GenericResult> DoSelectAggregateAsync(
    CancellationToken token,
    PXQueryDescription descr,
    bool decreaseTimeout)
  {
    return new PXAsyncResultset<GenericResult>(AsyncEnumerableEx.Using<PXResult<GenericResult>, CompositeDisposable>((Func<CompositeDisposable>) (() => new CompositeDisposable(new IDisposable[4]
    {
      descr.ShowDeletedRecords ? (IDisposable) new PXReadDeletedScope() : Disposable.Empty,
      descr.ShowArchivedRecords ? (IDisposable) new PXReadThroughArchivedScope() : Disposable.Empty,
      (IDisposable) new PXGenericInqGrph.DynamicFieldsScope(this),
      (IDisposable) new PXStreamingQueryScope()
    })), (Func<CompositeDisposable, IAsyncEnumerable<PXResult<GenericResult>>>) (scope => AsyncEnumerable.Take<PXResult<GenericResult>>(this.GetAggregatedRecords(descr, scope, (Func<Query, PXDataValue[], IEnumerableLinqWrapper<PXDataRecord>>) ((query, parameters) => (IEnumerableLinqWrapper<PXDataRecord>) new AsyncEnumerableWrapper<PXDataRecord>(PXDatabase.Provider.SelectAsync(query, (IEnumerable<PXDataValue>) parameters, (System.Action<PXDatabaseProvider.ExecutionParameters>) (c =>
    {
      if (descr.RetrieveTotalRowCount & decreaseTimeout)
        c.CommandTimeout /= 5;
      else
        c.CommandTimeout *= 5;
      c.TraceQuery = true;
    }), token)))).AsAsyncEnumerable(), 1))), token);
  }

  private IEnumerableLinqWrapper<PXResult<GenericResult>> GetAggregatedRecords(
    PXQueryDescription descr,
    CompositeDisposable scope,
    Func<Query, PXDataValue[], IEnumerableLinqWrapper<PXDataRecord>> selector)
  {
    BqlGenericCommand command = new BqlGenericCommand((PXGraph) this, (PXCache) this.Results.Cache, descr);
    IEnumerable<string> fields = descr.TotalFields.Any<PXAggregateField>() ? descr.TotalFields.Select<PXAggregateField, string>((Func<PXAggregateField, string>) (f => $"{f.Table.Alias}_{f.Name}")) : (IEnumerable<string>) this.GetRestrictedFields(descr);
    scope.Add((IDisposable) new PXFieldScope(this.Results.View, fields, false));
    Query query = command.GetQuery((PXGraph) this, this.Results.View);
    IEnumerableLinqWrapper<PXDataRecord> enumerableLinqWrapper = selector(query, command.Parameters);
    GIDataRecordMap map = command.RecordMapEntries != null ? scope.AddToDisposableScope<GIDataRecordMap>(new GIDataRecordMap(command.RecordMapEntries, this.GetRecordRestrictedFields(descr, this.Results.View.RestrictedFields))) : (GIDataRecordMap) null;
    Func<PXDataRecord, PXResult<GenericResult>> selector1 = (Func<PXDataRecord, PXResult<GenericResult>>) (r => this.ProcessRow(descr, r, (PXDataRecordMap) map, command));
    return enumerableLinqWrapper.Select<PXResult<GenericResult>>(selector1);
  }

  private PXResult<GenericResult> ProcessRow(
    PXQueryDescription descr,
    PXDataRecord r,
    PXDataRecordMap map,
    BqlGenericCommand command)
  {
    int? nullable1 = new int?();
    (GenericResult genericResult, int num) = this.ReadGenericRow(r, map, (IEnumerable<PXTable>) command.GetTables());
    if (descr.RetrieveTotalRowCount)
    {
      PXDataRecord pxDataRecord = r;
      if (map != null)
      {
        map.SetRow(r);
        pxDataRecord = (PXDataRecord) map;
      }
      nullable1 = pxDataRecord.GetInt32(num);
      if (descr.SelectTop > 0)
      {
        int? nullable2 = nullable1;
        int selectTop = descr.SelectTop;
        if (nullable2.GetValueOrDefault() > selectTop & nullable2.HasValue)
          nullable1 = new int?(descr.SelectTop);
      }
    }
    PXResult<GenericResult> pxResult = new PXResult<GenericResult>(genericResult);
    pxResult.RowCount = nullable1;
    return pxResult;
  }

  internal (GenericResult Row, int DataReaderPos) ReadGenericRow(
    PXDataRecord record,
    PXDataRecordMap map,
    IEnumerable<PXTable> tables)
  {
    int position = 0;
    PXDataRecord record1 = record;
    if (map != null)
    {
      map.SetRow(record);
      record1 = (PXDataRecord) map;
    }
    Dictionary<string, object> values = new Dictionary<string, object>();
    foreach (PXTable table in tables)
    {
      PXCache cach = (table.Graph ?? this).Caches[table.CacheType];
      cach.NeverCloneAttributes = true;
      object instance = cach.CreateInstance();
      TableChangingScope.SetCurrentLevelTable(table.Alias);
      instance.GetType();
      using (PXGenericInqGrph.PXRowSelectingFieldScope.ApplyFor(cach, this))
        cach.RaiseRowSelecting(instance, record1, ref position, true);
      TableChangingScope.RemoveCurrentLevelTable(table.Alias);
      cach.NeverCloneAttributes = false;
      values[table.Alias] = instance;
    }
    GenericResult genericRow = this.CreateGenericRow((IDictionary<string, object>) values);
    this.Results.Cache.RaiseRowSelecting((object) genericRow, record1, ref position, true);
    return (genericRow, position);
  }

  /// <summary>
  /// Converts restricted fields set from GI format (GenericResult | Alias_Field) to common (DAC) format (DacTableName | Field).
  /// </summary>
  internal RestrictedFieldsSet GetRecordRestrictedFields(
    PXQueryDescription descr,
    RestrictedFieldsSet genericRestrictedFields)
  {
    RestrictedFieldsSet restrictedFields = new RestrictedFieldsSet();
    foreach (RestrictedField genericRestrictedField in genericRestrictedFields)
    {
      RestrictedField field = genericRestrictedField;
      if (genericRestrictedField.Table == typeof (GenericResult))
      {
        Tuple<System.Type, string> fieldNameAndTable = this.GetFieldNameAndTable(descr, genericRestrictedField.Field);
        if (fieldNameAndTable != null)
          field = new RestrictedField(fieldNameAndTable.Item1, fieldNameAndTable.Item2);
      }
      restrictedFields.Add(field);
    }
    return restrictedFields;
  }

  /// <summary>
  /// Returns table type and field name from GI field name (in Alias_Field format)
  /// </summary>
  private Tuple<System.Type, string> GetFieldNameAndTable(
    PXQueryDescription descr,
    string fieldName)
  {
    if (!string.IsNullOrEmpty(fieldName))
    {
      string[] strArray = fieldName.Split('_', 2);
      PXTable pxTable;
      if (strArray.Length == 2 && descr.Tables.TryGetValue(strArray[0], out pxTable))
        return Tuple.Create<System.Type, string>(pxTable.BqlTable, strArray[1]);
    }
    return (Tuple<System.Type, string>) null;
  }

  private GenericResult CreateGenericRow(IDictionary<string, object> values)
  {
    return new GenericResult()
    {
      Values = values,
      Selected = new bool?(false)
    };
  }

  [PXInternalUseOnly]
  public virtual void GetSpecialRestrictedItems(
    HashSet<string> hashSet,
    PXQueryDescription description,
    bool mainQuery)
  {
    if (mainQuery)
    {
      foreach (PXSort sort in description.Sorts)
      {
        if (sort.Table != null && sort.DataField is PXFieldValue dataField)
          hashSet.Add($"{dataField.TableName}_{dataField.FieldName}");
      }
    }
    foreach (GINavigationParameter navigationParameter in this.Description.NavigationParameters)
    {
      if (!string.IsNullOrEmpty(navigationParameter.ParameterName) && navigationParameter.ParameterName.StartsWith("="))
      {
        foreach (string str in this.GetDataFieldsFromFormula(navigationParameter.ParameterName))
          hashSet.Add(str);
      }
      else if (!string.IsNullOrEmpty(navigationParameter.TableAlias) && !string.IsNullOrEmpty(navigationParameter.ParameterFieldName))
        hashSet.Add($"{navigationParameter.TableAlias}_{navigationParameter.ParameterFieldName}");
    }
    foreach (GINavigationCondition navigationCondition in this.Description.NavigationConditions)
    {
      hashSet.Add(navigationCondition.DataField.Replace('.', '_'));
      foreach (string str in this.GetDataFieldsFromFormula(navigationCondition.ValueSt))
        hashSet.Add(str);
      foreach (string str in this.GetDataFieldsFromFormula(navigationCondition.ValueSt2))
        hashSet.Add(str);
    }
    foreach (string str in this.GetDataFieldsFromFormula(this.Description.Design.RowStyleFormula))
      hashSet.Add(str);
    foreach (GIResult giResult in this._resultColumns.Where<GIResult>((Func<GIResult, bool>) (c =>
    {
      if (string.IsNullOrEmpty(c.StyleFormula))
        return false;
      return this.VisibleColumns == null || this.VisibleColumns.Contains(c.FieldName);
    })))
    {
      foreach (string str in this.GetDataFieldsFromFormula(giResult.StyleFormula))
        hashSet.Add(str);
    }
    if (mainQuery)
    {
      foreach (PXFieldValue pxFieldValue in description.FilterWheres.Concat<PXWhereCond>((IEnumerable<PXWhereCond>) description.Searches).Select<PXWhereCond, IPXValue>((Func<PXWhereCond, IPXValue>) (c => c.DataField)).OfType<PXFieldValue>())
        hashSet.Add($"{pxFieldValue.TableName}_{pxFieldValue.FieldName}");
    }
    foreach (PXTable pxTable in description.Tables.Values)
    {
      PXTable table = pxTable;
      PXCache cach = (table.Graph ?? this).Caches[table.CacheType];
      foreach (string key in (IEnumerable<string>) cach.Keys)
        hashSet.Add($"{table.Alias}_{key}");
      string rowId;
      if ((rowId = cach.RowId) != null)
        hashSet.Add($"{table.Alias}_{rowId}");
      Dictionary<string, PropertyInfo> dictionary = ((IEnumerable<PropertyInfo>) table.CacheType.GetProperties()).ToDictionary<PropertyInfo, string, PropertyInfo>((Func<PropertyInfo, string>) (o => o.Name), (Func<PropertyInfo, PropertyInfo>) (o => o));
      foreach (System.Type extensionType in cach.GetExtensionTypes())
      {
        foreach (PropertyInfo property in extensionType.GetProperties())
        {
          if (!dictionary.ContainsKey(property.Name))
            dictionary.Add(property.Name, property);
        }
      }
      string alias = table.Alias;
      foreach (PropertyInfo propertyInfo in dictionary.Values)
      {
        if (propertyInfo.IsDefined(typeof (IPXReportRequiredField), true))
        {
          hashSet.Add($"{table.Alias}_{propertyInfo.Name}");
          if (table.Alias == this._noteTableAlias && PXView.MaximumRows != 0 && string.Equals(propertyInfo.Name, "NoteID", StringComparison.OrdinalIgnoreCase))
          {
            hashSet.Add(table.Alias + "_NoteText");
            hashSet.Add(table.Alias + "_NoteFiles");
            hashSet.Add(table.Alias + "_NoteTextExists");
            hashSet.Add(table.Alias + "_NoteFilesCount");
          }
        }
        if (hashSet.Contains($"{alias}_{propertyInfo.Name}"))
        {
          HashSet<string> source = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          if (!propertyInfo.IsDefined(typeof (PXDependsOnFieldsAttribute), true))
          {
            MethodInfo getMethod = propertyInfo.GetGetMethod(true);
            if (((object) getMethod != null ? (getMethod.IsDefined(typeof (PXDependsOnFieldsAttribute), true) ? 1 : 0) : 0) == 0)
              goto label_99;
          }
          EnumerableExtensions.AddRange<string>((ISet<string>) source, (IEnumerable<string>) PXDependsOnFieldsAttribute.GetDependsRecursive(cach, propertyInfo.Name));
label_99:
          foreach (PXEventSubscriberAttribute subscriberAttribute in cach.GetAttributesReadonly(propertyInfo.Name, true))
          {
            if (subscriberAttribute is IPXDependsOnFields)
              EnumerableExtensions.AddRange<string>((ISet<string>) source, (IEnumerable<string>) PXDependsOnFieldsAttribute.GetDependsRecursive(cach, propertyInfo.Name));
          }
          hashSet.UnionWith(source.Select<string, string>((Func<string, string>) (a => $"{table.Alias}_{a}")));
        }
      }
      if (table.Graph != null)
      {
        string aliasPrefix = table.Alias + "_";
        HashSet<string> hashSet1 = hashSet.Where<string>((Func<string, bool>) (x => x.StartsWith(aliasPrefix, StringComparison.OrdinalIgnoreCase))).Select<string, string>((Func<string, string>) (x => x.Substring(aliasPrefix.Length))).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        table.Graph.GetSpecialRestrictedItems(hashSet1, table.Graph.BaseQueryDescription, false);
        foreach (string str in hashSet1)
        {
          if (hashSet.Add(aliasPrefix + str) && !cach.Fields.Contains<string>(str, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            cach.Fields.Add(str);
        }
      }
    }
  }

  protected IEnumerable addNewKeys() => this.Caches[typeof (PXGenericInqGrph.GIKeyDefault)].Cached;

  /// <summary>
  /// Inserts GIKeyDefault records in cache (AskExt delegate).
  /// </summary>
  private void FillAddNewKeys(PXGraph graph, string viewName)
  {
    PXCache cach = this.Caches[typeof (PXGenericInqGrph.GIKeyDefault)];
    cach.Clear();
    if (this.Design.PrimaryScreenID != null)
    {
      PXCache cache = this.Caches[GIScreenHelper.GetCacheType(this.Design.PrimaryScreenID)];
      Dictionary<string, GIRecordDefault> defaults = this.Description.RecordDefaults.ToDictionary<GIRecordDefault, string>((Func<GIRecordDefault, string>) (gd => gd.FieldName));
      foreach (PXGenericInqGrph.GIKeyDefault giKeyDefault in cache.Keys.Select(key => new
      {
        key = key,
        state = cache.GetStateExt((object) null, key) as PXFieldState
      }).Where(_param1 => _param1.state != null).Select(_param1 => new PXGenericInqGrph.GIKeyDefault()
      {
        FieldName = _param1.key,
        DisplayName = _param1.state.DisplayName,
        Value = defaults.ContainsKey(_param1.key) ? defaults[_param1.key].Value : (string) null
      }))
        cach.Insert((object) giKeyDefault);
    }
    cach.IsDirty = false;
  }

  [PXUIField(DisplayName = "Insert", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXButton(SpecialType = PXSpecialButtonType.Insert, ImageKey = "AddNew", CommitChanges = true, Tooltip = "New Record")]
  protected void insert()
  {
    PXView primaryView;
    PXGraph graph = GIScreenHelper.InstantiateGraph(this.Design.With<GIDesign, string>((Func<GIDesign, string>) (_ => _.PrimaryScreenID)), out primaryView);
    PXCache cache = primaryView.Cache;
    GIRecordDefault[] array = this.Description.RecordDefaults.ToArray<GIRecordDefault>();
    bool flag1 = false;
    if (((IEnumerable<GIRecordDefault>) array).Any<GIRecordDefault>())
    {
      this.Caches[typeof (GIDesign)].Current = (object) this.Design;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      object instance = cache.CreateInstance();
      bool flag2 = true;
      foreach (string key1 in (IEnumerable<string>) cache.Keys)
      {
        string key = key1;
        GIRecordDefault giRecordDefault = ((IEnumerable<GIRecordDefault>) array).FirstOrDefault<GIRecordDefault>((Func<GIRecordDefault, bool>) (d => string.Equals(d.FieldName, key, StringComparison.OrdinalIgnoreCase)));
        object newValue;
        if (giRecordDefault == null)
        {
          cache.RaiseFieldDefaulting(key, instance, out newValue);
          if (newValue == null)
            flag2 = false;
          else
            dictionary[key] = newValue;
        }
        else
          newValue = dictionary[key] = (object) giRecordDefault.Value;
        cache.RaiseFieldUpdating(key, instance, ref newValue);
        cache.SetValue(instance, key, newValue);
      }
      if (!flag2)
      {
        if (this.AddNewKeys.AskExt(new PXView.InitializePanel(this.FillAddNewKeys)) != WebDialogResult.OK)
          return;
        foreach (PXGenericInqGrph.GIKeyDefault giKeyDefault in this.Caches[typeof (PXGenericInqGrph.GIKeyDefault)].Cached.Cast<PXGenericInqGrph.GIKeyDefault>())
          dictionary[giKeyDefault.FieldName] = (object) giKeyDefault.Value;
      }
      Dictionary<string, object> values = new Dictionary<string, object>();
      foreach (GIRecordDefault giRecordDefault in array)
      {
        if (!cache.Keys.Contains(giRecordDefault.FieldName))
        {
          values[giRecordDefault.FieldName] = (object) giRecordDefault.Value;
          flag1 = true;
        }
      }
      cache.Insert((IDictionary) dictionary);
      cache.Update((IDictionary) dictionary, (IDictionary) values);
    }
    else
      cache.Insert();
    cache.IsDirty = flag1;
    GINavigationScreen navigationScreen = this.GetPrimaryNavigationScreen();
    if (navigationScreen != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException(GIScreenHelper.GetSiteMapNode(navigationScreen.Link).Url, graph, string.Empty);
      requiredException.Mode = PXWindowModeAttribute.Convert(navigationScreen.WindowMode);
      throw requiredException;
    }
  }

  private GINavigationScreen GetPrimaryNavigationScreen()
  {
    return this.Description.NavigationScreens.FirstOrDefault<GINavigationScreen>((Func<GINavigationScreen, bool>) (s =>
    {
      bool? isActive = s.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue && string.Equals(s.Link, this.Design.PrimaryScreenID, StringComparison.OrdinalIgnoreCase);
    }));
  }

  /// <summary>
  /// Keeps current record when navigating back and forth to the entry screen.
  /// </summary>
  internal GenericResult EditCurrent
  {
    get
    {
      GIDesign design = this.Design;
      return (design != null ? (!design.DesignID.HasValue ? 1 : 0) : 1) != 0 ? (GenericResult) null : PXContext.SessionTyped<PXSessionStatePXData>().GIEditCurrent[this.Design.DesignID.Value.ToString()];
    }
    set
    {
      GIDesign design = this.Design;
      if ((design != null ? (!design.DesignID.HasValue ? 1 : 0) : 1) != 0)
        return;
      PXContext.SessionTyped<PXSessionStatePXData>().GIEditCurrent[this.Design.DesignID.Value.ToString()] = value;
    }
  }

  /// <summary>Keeps current record with total values, if any.</summary>
  internal GenericResult TotalCurrent
  {
    get
    {
      GIDesign design = this.Design;
      return (design != null ? (!design.DesignID.HasValue ? 1 : 0) : 1) != 0 ? (GenericResult) null : PXContext.SessionTyped<PXSessionStatePXData>().GITotalCurrent[this.Design.DesignID.Value.ToString()];
    }
    set
    {
      GIDesign design = this.Design;
      if ((design != null ? (!design.DesignID.HasValue ? 1 : 0) : 1) != 0)
        return;
      PXContext.SessionTyped<PXSessionStatePXData>().GITotalCurrent[this.Design.DesignID.Value.ToString()] = value;
    }
  }

  /// <summary>
  /// Gets primary screen node url, selects navigation parameters,
  /// builds url string with this params and throws PXRedirectToUrlException.
  /// </summary>
  [PXUIField(DisplayName = "Edit", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXEditDetailButton]
  protected void editDetail()
  {
    if (this.Design == null || this.Design.PrimaryScreenID == null)
      return;
    GINavigationScreen navigationScreen = this.GetPrimaryNavigationScreen();
    if (navigationScreen == null)
      return;
    this.EditCurrent = this.Results.Current;
    this.NavigateTo(this, this.Design.PrimaryScreenID, PXWindowModeAttribute.Convert(navigationScreen.WindowMode), navigationScreen.LineNbr);
  }

  /// <summary>
  /// Applies access rights from the Primary Screen to the action in the List
  /// (only if action with the same name exists in the Primary Screen).
  /// </summary>
  /// <param name="action"></param>
  internal void SecureAction(PXAction action)
  {
    if (this.Design.PrimaryScreenID == null)
      return;
    PXUIFieldAttribute attribute = action.Attributes.OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
    if (attribute == null)
      return;
    PXAccess.Secure(this.Caches[GIScreenHelper.GetCacheType(this.Design.PrimaryScreenID)], (PXEventSubscriberAttribute) attribute);
  }

  /// <summary>
  /// Initializes and adds actions from the Primary Screen to the inquiry graph.
  /// </summary>
  private void InitializePrimaryScreenActions()
  {
    bool isVisible1 = this.Design.PrimaryScreenID != null;
    int num;
    if (isVisible1)
    {
      bool? recordCreationEnabled = this.Design.NewRecordCreationEnabled;
      bool flag = true;
      num = recordCreationEnabled.GetValueOrDefault() == flag & recordCreationEnabled.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool isVisible2 = num != 0;
    if (isVisible1)
    {
      PXSiteMap.ScreenInfo screenInfo = this.ScreenInfoProvider.TryGet(this.Design.PrimaryScreenID);
      if (screenInfo != null)
      {
        PXSiteMap.ScreenInfo.Action action = ((IEnumerable<PXSiteMap.ScreenInfo.Action>) screenInfo.Actions).FirstOrDefault<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (a => a.ButtonType == PXSpecialButtonType.Insert));
        if (action != null && !action.Visible)
          isVisible2 = false;
      }
    }
    this.Insert.SetVisible(isVisible2);
    this.SecureAction((PXAction) this.Insert);
    this.EditDetail.SetVisible(isVisible1);
    this.Results.InitializeActions();
  }

  public IEnumerable fields(PXAdapter adapter)
  {
    return (IEnumerable) this.Caches[typeof (PXGenericInqGrph.GIUpdateValue)].Cached.Cast<PXGenericInqGrph.GIUpdateValue>();
  }

  private IEnumerable<PXGenericInqGrph.GIUpdateValue> GetProcessingProperties()
  {
    PXCache cache = this.Caches[GIScreenHelper.GetCacheType(this.Design.With<GIDesign, string>((Func<GIDesign, string>) (_ => _.PrimaryScreenID)))];
    HashSet<string> cacheFields = new HashSet<string>((IEnumerable<string>) cache.Fields);
    return this.Description.MassUpdateFields.Select(muField => new
    {
      muField = muField,
      t = new
      {
        Name = muField.FieldName,
        State = cache.GetStateExt((object) null, muField.FieldName) as PXFieldState
      }
    }).Where(_param1 => _param1.t.State != null && cacheFields.Contains(_param1.muField.FieldName)).Select(_param1 => new PXGenericInqGrph.GIUpdateValue()
    {
      Selected = new bool?(false),
      FieldName = _param1.t.Name,
      DisplayName = _param1.t.State.DisplayName,
      Value = (string) null
    });
  }

  private void FillPropertyValue(PXGraph graph, string viewName)
  {
    PXCache cach = this.Caches[typeof (PXGenericInqGrph.GIUpdateValue)];
    cach.Clear();
    foreach (PXGenericInqGrph.GIUpdateValue processingProperty in this.GetProcessingProperties())
      cach.Insert((object) processingProperty);
    cach.IsDirty = false;
    this.Caches[typeof (GIDesign)].Current = (object) this.Design;
  }

  internal bool AskParameters()
  {
    return this.Fields.AskExt(new PXView.InitializePanel(this.FillPropertyValue)) == WebDialogResult.OK && this.FieldsToUpdate.Any<PXGenericInqGrph.GIUpdateValue>();
  }

  internal IEnumerable<PXGenericInqGrph.GIUpdateValue> FieldsToUpdate
  {
    get
    {
      return this.Caches[typeof (PXGenericInqGrph.GIUpdateValue)].Cached.Cast<PXGenericInqGrph.GIUpdateValue>().Where<PXGenericInqGrph.GIUpdateValue>((Func<PXGenericInqGrph.GIUpdateValue, bool>) (uv =>
      {
        bool? selected = uv.Selected;
        bool flag = true;
        return selected.GetValueOrDefault() == flag & selected.HasValue;
      }));
    }
  }

  protected virtual void GIUpdateValue_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    PXGenericInqGrph.GIUpdateValue newRow = (PXGenericInqGrph.GIUpdateValue) e.NewRow;
    PXGenericInqGrph.GIUpdateValue row = (PXGenericInqGrph.GIUpdateValue) e.Row;
    PXGenericInqGrph.GIUpdateValue giUpdateValue = newRow;
    bool? selected = newRow.Selected;
    bool flag = true;
    bool? nullable = new bool?(selected.GetValueOrDefault() == flag & selected.HasValue || row.Value != newRow.Value);
    giUpdateValue.Selected = nullable;
  }

  [InjectDependency]
  private INavigationService _navigationService { get; set; }

  [InjectDependency]
  private IConditionEvaluator _conditionEvaluator { get; set; }

  [InjectDependency]
  private INavigationExpressionEvaluator _navigationParamsEvaluator { get; set; }

  private void NavigateTo(
    PXGenericInqGrph graph,
    string urlOrScreenID,
    PXBaseRedirectException.WindowMode windowMode,
    int? lineNbr)
  {
    GenericResult navigatingGraphCurrent = graph.Results.Current;
    if (navigatingGraphCurrent == null)
      return;
    if (this.Design != null && string.Equals(this.Design.PrimaryScreenID, urlOrScreenID, StringComparison.OrdinalIgnoreCase))
      this.EditCurrent = this.Results.Current;
    if (!NavigationTemplateHelper.IsExternalUrlOrTemplate(urlOrScreenID))
      GIScreenHelper.GetSiteMapNode(urlOrScreenID);
    GINavigationParameter[] array = graph.Description.NavigationParameters.Where<GINavigationParameter>((Func<GINavigationParameter, bool>) (n =>
    {
      int? navigationScreenLineNbr = n.NavigationScreenLineNbr;
      int? nullable = lineNbr;
      return navigationScreenLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & navigationScreenLineNbr.HasValue == nullable.HasValue;
    })).ToArray<GINavigationParameter>();
    Dictionary<string, object> dictionary = ((IEnumerable<GINavigationParameter>) array).ToDictionary<GINavigationParameter, string, object>((Func<GINavigationParameter, string>) (n => n.FieldName), (Func<GINavigationParameter, object>) (n => this.EvaluateNavParamValue(graph, navigatingGraphCurrent, n)));
    this._navigationService.NavigateTo(urlOrScreenID, (IReadOnlyDictionary<string, object>) dictionary, windowMode, this.GenerateRedirectMessage(navigatingGraphCurrent, (IReadOnlyCollection<GINavigationParameter>) array));
  }

  private bool EvaluateNavConditions(
    GenericResult row,
    IEnumerable<GINavigationCondition> conditions)
  {
    if (conditions == null || !conditions.Any<GINavigationCondition>())
      return true;
    List<PXFilterRow> filters = new List<PXFilterRow>();
    foreach (GINavigationCondition condition in conditions)
    {
      object navConditionValue1 = this.EvaluateNavConditionValue(row, condition.ValueSt, condition.IsExpression);
      object navConditionValue2 = this.EvaluateNavConditionValue(row, condition.ValueSt2, condition.IsExpression);
      PXFilterRow pxFilterRow = new PXFilterRow(condition.DataField.Replace(".", "_"), ValFromStr.GetPXCondition(condition.Condition), navConditionValue1, navConditionValue2)
      {
        OpenBrackets = condition.OpenBrackets.GetValueOrDefault(),
        CloseBrackets = condition.CloseBrackets.GetValueOrDefault(),
        OrOperator = ValFromStr.GetOperation(condition.Operator) == PX.Data.Description.GI.Operation.Or
      };
      filters.Add(pxFilterRow);
    }
    return this._conditionEvaluator.Evaluate((PXGraph) this, this.Results.View, (object) row, (IEnumerable<PXFilterRow>) filters);
  }

  private object EvaluateNavConditionValue(GenericResult row, string value, bool? isExpression)
  {
    if (string.IsNullOrEmpty(value))
      return (object) value;
    bool? nullable = isExpression;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue && !RelativeDatesManager.IsRelativeDatesString(value))
      return (object) value;
    INavigationExpressionEvaluator navigationParamsEvaluator = this._navigationParamsEvaluator;
    GenericResult row1 = row;
    string expression = value;
    nullable = new bool?();
    bool? isFromSchema = nullable;
    return navigationParamsEvaluator.Evaluate((PXGraph) this, (object) row1, expression, isFromSchema);
  }

  private object EvaluateNavParamValue(
    GenericResult row,
    GINavigationParameter param,
    bool applyMask = false)
  {
    return this.EvaluateNavParamValue(this, row, param, applyMask);
  }

  private object EvaluateNavParamValue(
    PXGenericInqGrph graph,
    GenericResult row,
    GINavigationParameter param,
    bool applyMask = false)
  {
    return this._navigationParamsEvaluator.Evaluate((PXGraph) graph, (object) row, param.ParameterName, param.IsExpression, applyMask: applyMask);
  }

  private object GetNavParamValue(GenericResult row, GINavigationParameter param, bool applyMask = false)
  {
    bool? isExpression = param.IsExpression;
    bool flag1 = true;
    if (isExpression.GetValueOrDefault() == flag1 & isExpression.HasValue || param.ParameterName.StartsWith("="))
      return this.EvaluateNavParamValue(row, param, true);
    object stateExt = this.Caches[row.Values[param.TableAlias].GetType()].GetStateExt((object) null, param.ParameterFieldName);
    string descriptionName = stateExt is PXFieldState pxFieldState1 ? pxFieldState1.DescriptionName : (string) null;
    string str;
    if (string.IsNullOrEmpty(descriptionName))
    {
      if (!(stateExt is PXFieldState pxFieldState2) || !(pxFieldState2.DataType == typeof (string)))
        return (object) string.Empty;
      str = param.ParameterName;
    }
    else
      str = $"{param.TableAlias}.{descriptionName}";
    INavigationExpressionEvaluator navigationParamsEvaluator = this._navigationParamsEvaluator;
    GenericResult row1 = row;
    string expression = str;
    bool flag2 = applyMask;
    bool? isFromSchema = new bool?();
    int num = flag2 ? 1 : 0;
    return navigationParamsEvaluator.Evaluate((PXGraph) this, (object) row1, expression, isFromSchema, applyMask: num != 0);
  }

  private string GenerateRedirectMessage(
    GenericResult row,
    IReadOnlyCollection<GINavigationParameter> navParams)
  {
    return string.Join<object>(", ", navParams.Select<GINavigationParameter, object>((Func<GINavigationParameter, object>) (p => this.GetNavParamValue(row, p, true))).Where<object>((Func<object, bool>) (parameter => !string.IsNullOrEmpty(parameter?.ToString()))));
  }

  internal bool DefaultNavigationExists(string fieldName)
  {
    if (string.IsNullOrEmpty(fieldName))
      throw new ArgumentNullException(nameof (fieldName));
    string viewName = (this.Results.Cache.GetStateExt((object) null, fieldName) as PXFieldState)?.ViewName;
    if (string.IsNullOrEmpty(viewName))
      return false;
    GIResult giResult = this.Description.Results.FirstOrDefault<GIResult>((Func<GIResult, bool>) (res => string.Equals(res.FieldName, fieldName, StringComparison.OrdinalIgnoreCase)));
    if (giResult != null)
    {
      bool? defaultNav = giResult.DefaultNav;
      if (defaultNav.HasValue && !defaultNav.GetValueOrDefault() && !giResult.NavigationNbr.HasValue)
        return false;
    }
    PXTable pxTable;
    if (giResult != null && this.BaseQueryDescription.Tables.TryGetValue(giResult.ObjectName, out pxTable) && pxTable.Graph != null)
      return pxTable.Graph.DefaultNavigationExists(giResult.Field);
    HashSet<string> keys = new HashSet<string>((IEnumerable<string>) this.GetKeyNames(viewName), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (!this.Results.Cache.GetAttributesReadonly(fieldName, true).OfType<PXSelectorAttribute>().Any<PXSelectorAttribute>((Func<PXSelectorAttribute, bool>) (sel =>
    {
      if (keys.Contains(sel.Field.Name))
        return true;
      return sel.SubstituteKey != (System.Type) null && keys.Contains(sel.SubstituteKey.Name) && !string.IsNullOrEmpty(viewName);
    })))
      return false;
    if (!string.IsNullOrEmpty(this.Design.PrimaryScreenID))
    {
      System.Type graphType = GetPrimaryGraphType(viewName);
      if (graphType != (System.Type) null)
        return PXSiteMap.Provider.FindSiteMapNodesByScreenID(this.Design.PrimaryScreenID).Any<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (node => node.GraphType != graphType.FullName));
    }
    return true;

    System.Type GetPrimaryGraphType(string viewName)
    {
      System.Type graphType;
      if (!this._graphTypeByViewNames.TryGetValue(viewName, out graphType))
      {
        PXPrimaryGraphAttribute.FindPrimaryGraph(this.Caches[this.GetItemType(viewName)], out graphType);
        this._graphTypeByViewNames[viewName] = graphType;
      }
      return graphType;
    }
  }

  /// <summary>Adds actions for side-panel navigation layer.</summary>
  private void InitializeLayeredNavigationActions()
  {
    List<PXGraph.SidePanelAction> collection = new List<PXGraph.SidePanelAction>();
    List<string> source = new List<string>();
    GINavigationScreen[] sidePanels = this.Description.NavigationScreens.Where<GINavigationScreen>((Func<GINavigationScreen, bool>) (s =>
    {
      if (string.IsNullOrEmpty(s.Link) || string.IsNullOrEmpty(s.WindowMode) || PXWindowModeAttribute.Convert(s.WindowMode) != PXBaseRedirectException.WindowMode.Layer)
        return false;
      bool? isActive = s.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue;
    })).ToArray<GINavigationScreen>();
    foreach (GINavigationScreen navigationScreen in sidePanels)
    {
      GINavigationScreen navScreen = navigationScreen;
      if (NavigationTemplateHelper.IsExternalUrlOrTemplate(navScreen.Link) || PXSiteMap.Provider.FindSiteMapNodeByScreenID(navScreen.Link) != null)
      {
        string screenId = navScreen.Link;
        PXButtonDelegate handler = (PXButtonDelegate) (adapter =>
        {
          this.NavigateTo(this, screenId, PXWindowModeAttribute.Convert(navScreen.WindowMode), navScreen.LineNbr);
          return adapter.Get();
        });
        string str = "NavigateToLayer$" + screenId;
        int num = source.Count<string>((Func<string, bool>) (x => x.Equals(screenId, StringComparison.OrdinalIgnoreCase)));
        if (num > 0)
          str += $"_{num + 1}";
        source.Add(screenId);
        PXNamedAction.AddHiddenAction((PXGraph) this, typeof (GenericFilter), str, handler);
        bool navConditions = this.EvaluateNavConditions(this.Results.Current, this.Description.NavigationConditions.Where<GINavigationCondition>((Func<GINavigationCondition, bool>) (c =>
        {
          int? navigationScreenLineNbr = c.NavigationScreenLineNbr;
          int? lineNbr = navScreen.LineNbr;
          return navigationScreenLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & navigationScreenLineNbr.HasValue == lineNbr.HasValue;
        })));
        string toolTip = navScreen.CustomTitle != null ? PXDBLocalizableStringAttribute.GetTranslation<GINavigationScreen.customTitle>((PXCache) this.Caches<GINavigationScreen>(), (object) navScreen, PXLocalesProvider.GetCurrentLocale()) : navScreen.Title;
        List<PXGraph.SidePanelAction> sidePanelActionList = collection;
        PXGenericInqGrph.GiNavigationSidePanelAction navigationSidePanelAction = new PXGenericInqGrph.GiNavigationSidePanelAction(str, navScreen.Icon, toolTip, navScreen.LineNbr.Value);
        navigationSidePanelAction.Visible = navConditions;
        sidePanelActionList.Add((PXGraph.SidePanelAction) navigationSidePanelAction);
      }
    }
    this.HasSidePanelConditions = this.Description.NavigationConditions.Any<GINavigationCondition>((Func<GINavigationCondition, bool>) (c => ((IEnumerable<GINavigationScreen>) sidePanels).Any<GINavigationScreen>((Func<GINavigationScreen, bool>) (s =>
    {
      int? lineNbr = s.LineNbr;
      int? navigationScreenLineNbr = c.NavigationScreenLineNbr;
      return lineNbr.GetValueOrDefault() == navigationScreenLineNbr.GetValueOrDefault() & lineNbr.HasValue == navigationScreenLineNbr.HasValue;
    }))));
    this.SidePanelActions.Clear();
    this.SidePanelActions.AddRange((IEnumerable<PXGraph.SidePanelAction>) collection);
  }

  private void UpdateSidePanelVisible()
  {
    if (!this.HasSidePanelConditions)
      return;
    foreach (PXGraph.SidePanelAction sidePanelAction in this.SidePanelActions)
    {
      int? screenNbr = sidePanelAction is PXGenericInqGrph.GiNavigationSidePanelAction navigationSidePanelAction ? new int?(navigationSidePanelAction.ScreenLineNbr) : new int?();
      bool navConditions = this.EvaluateNavConditions(this.Results.Current, this.Description.NavigationConditions.Where<GINavigationCondition>((Func<GINavigationCondition, bool>) (c =>
      {
        int? navigationScreenLineNbr = c.NavigationScreenLineNbr;
        int? nullable = screenNbr;
        return navigationScreenLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & navigationScreenLineNbr.HasValue == nullable.HasValue;
      })));
      sidePanelAction.Visible = navConditions;
    }
  }

  private void AddAdditionalCacheFields()
  {
    foreach (GINavigationCondition navigationCondition in this.Description.NavigationConditions)
    {
      string str = navigationCondition.DataField.Replace('.', '_');
      if (!this.Results.Cache.Fields.Contains(str))
        this.Results.Cache.Fields.Add(str);
    }
  }

  /// <exclude />
  private class GIFieldState
  {
    public bool HasValue { get; private set; }

    public PXFieldState State { get; private set; }

    public object Value { get; private set; }

    public GIFieldState(object value)
    {
      if (value is PXFieldState)
      {
        this.State = value as PXFieldState;
      }
      else
      {
        this.HasValue = true;
        this.Value = value;
      }
    }

    public GIFieldState(PXFieldState state) => this.State = state;

    public GIFieldState(PXFieldState state, object value)
    {
      this.HasValue = true;
      this.Value = value;
      this.State = state;
    }
  }

  /// <summary>
  /// Keeps information about description field that is needed to show it properly.
  /// </summary>
  /// <exclude />
  [PXInternalUseOnly]
  public class GIDescriptionField
  {
    public string TableAlias { get; }

    public string SourceFieldName { get; }

    /// <summary>Real description field name.</summary>
    public string DescriptionFieldName { get; }

    /// <summary>
    /// If DAC does not contain SourceField_description field, it will be generated by GI, and it's name will be stored in this field.
    /// </summary>
    public string DescriptionFieldAlias { get; }

    public string GenericSourceFieldName => $"{this.TableAlias}_{this.SourceFieldName}";

    public string GenericDescriptionFieldName => $"{this.TableAlias}_{this.DescriptionFieldName}";

    public string GenericDescriptionFieldAlias => $"{this.TableAlias}_{this.DescriptionFieldAlias}";

    /// <summary>
    /// Determines if a selector references a description field from the DAC where it is declared.<br />
    /// See <see cref="P:PX.Data.PXSelectorAttribute.IsSelfReferencing" />.
    /// </summary>
    public bool IsSelfReferencing
    {
      get
      {
        return !string.IsNullOrEmpty(this.DescriptionFieldAlias) && !string.Equals(this.DescriptionFieldName, this.DescriptionFieldAlias, StringComparison.OrdinalIgnoreCase);
      }
    }

    /// <summary>
    /// State from the source field (i.e. a field that contains PXSelectorAttribute with this DescriptionField)
    /// </summary>
    public Lazy<PXFieldState> SourceState { get; private set; }

    public void SetSourceStateFactory(Func<PXFieldState> factory)
    {
      this.SourceState = new Lazy<PXFieldState>((Func<PXFieldState>) (() =>
      {
        using (new PXGenericInqGrph.GIDescriptionField.SourceStateInitializationScope())
          return factory();
      }));
    }

    public bool SourceStateInitializing
    {
      get => PXGenericInqGrph.GIDescriptionField.SourceStateInitializationScope.IsScoped;
    }

    public GIDescriptionField(
      string alias,
      string sourceFieldName,
      string descriptionFieldName,
      string descriptionFieldAlias = null)
    {
      this.TableAlias = alias;
      this.SourceFieldName = sourceFieldName;
      this.DescriptionFieldName = descriptionFieldName;
      this.DescriptionFieldAlias = descriptionFieldAlias;
    }

    private class SourceStateInitializationScope : IDisposable
    {
      private static string SlotKey
      {
        get => typeof (PXGenericInqGrph.GIDescriptionField.SourceStateInitializationScope).FullName;
      }

      public static bool IsScoped
      {
        get
        {
          return PXContext.GetSlot<bool>(PXGenericInqGrph.GIDescriptionField.SourceStateInitializationScope.SlotKey);
        }
      }

      public SourceStateInitializationScope()
      {
        PXContext.SetSlot<bool>(PXGenericInqGrph.GIDescriptionField.SourceStateInitializationScope.SlotKey, true);
      }

      public void Dispose()
      {
        PXContext.SetSlot(PXGenericInqGrph.GIDescriptionField.SourceStateInitializationScope.SlotKey, (object) null);
      }
    }
  }

  /// <summary>
  /// Omits PXRowSelecting event handlers for fields that are not included into the selection.
  /// </summary>
  private class PXRowSelectingFieldScope : IDisposable
  {
    private readonly Dictionary<PXCache, IPXRowSelectingSubscriber[]> _optimizedHandlersByCache = new Dictionary<PXCache, IPXRowSelectingSubscriber[]>();
    private readonly PXGenericInqGrph.PXRowSelectingFieldScope _previous;

    public PXRowSelectingFieldScope(
      PXGenericInqGrph graph,
      PXQueryDescription descr,
      IEnumerable<string> usedFields)
    {
      IDictionary<PXCache, ISet<string>> restrictedFieldsMap = PXGenericInqGrph.PXRowSelectingFieldScope.CreatePerCacheRestrictedFieldsMap(graph, descr, usedFields);
      IPXRowSelectingSubscriber selectingSubscriber1 = (IPXRowSelectingSubscriber) new PXGenericInqGrph.PXRowSelectingFieldScope.PassThroughSubscriber.Incrementing();
      IPXRowSelectingSubscriber selectingSubscriber2 = (IPXRowSelectingSubscriber) new PXGenericInqGrph.PXRowSelectingFieldScope.PassThroughSubscriber.NonIncrementing();
      foreach (KeyValuePair<PXCache, ISet<string>> keyValuePair in (IEnumerable<KeyValuePair<PXCache, ISet<string>>>) restrictedFieldsMap)
      {
        PXCache pxCache1;
        ISet<string> stringSet1;
        EnumerableExtensions.Deconstruct<PXCache, ISet<string>>(keyValuePair, ref pxCache1, ref stringSet1);
        PXCache pxCache2 = pxCache1;
        ISet<string> stringSet2 = stringSet1;
        IPXRowSelectingSubscriber[] rowSelecting = pxCache2._EventsRowAttr.RowSelecting;
        int length = rowSelecting != null ? rowSelecting.Length : 0;
        IPXRowSelectingSubscriber[] selectingSubscriberArray = new IPXRowSelectingSubscriber[length];
        IPXRowSelectingSubscriber selectingSubscriber3 = (IPXRowSelectingSubscriber) null;
        for (int index = 0; index < length; ++index)
        {
          IPXRowSelectingSubscriber selectingSubscriber4;
          IPXRowSelectingSubscriber selectingSubscriber5 = selectingSubscriber4 = rowSelecting[index];
          if (selectingSubscriber4 is PXEventSubscriberAttribute subscriberAttribute2 && !string.IsNullOrEmpty(subscriberAttribute2.FieldName) && !stringSet2.Contains(subscriberAttribute2.FieldName))
            selectingSubscriber5 = (PXGenericInqGrph.IsVirtualField(pxCache2, subscriberAttribute2.FieldName) ? 1 : (!(selectingSubscriber3 is PXEventSubscriberAttribute subscriberAttribute1) ? 0 : (string.Equals(subscriberAttribute1.FieldName, subscriberAttribute2.FieldName, StringComparison.OrdinalIgnoreCase) ? 1 : 0))) != 0 ? selectingSubscriber2 : selectingSubscriber1;
          selectingSubscriberArray[index] = selectingSubscriber5;
          selectingSubscriber3 = selectingSubscriber4;
        }
        this._optimizedHandlersByCache[pxCache2] = selectingSubscriberArray;
      }
      this._previous = PXContext.GetSlot<PXGenericInqGrph.PXRowSelectingFieldScope>();
      PXContext.SetSlot<PXGenericInqGrph.PXRowSelectingFieldScope>(this);
    }

    public static IDisposable ApplyFor(PXCache cache, PXGenericInqGrph graph)
    {
      bool originalAggregateSelectingValue = cache._AggregateSelecting;
      PXGenericInqGrph.PXRowSelectingFieldScope slot = PXContext.GetSlot<PXGenericInqGrph.PXRowSelectingFieldScope>();
      cache._AggregateSelecting = PXGenericInqGrph.PXRowSelectingFieldScope.ShouldCachesBeMarkedAsAggregateSelecting(graph);
      if (slot == null)
        return Disposable.Create<PXCache>(cache, (System.Action<PXCache>) (c => c._AggregateSelecting = originalAggregateSelectingValue));
      IPXRowSelectingSubscriber[] originalHandlers = cache._EventsRowAttr.RowSelecting;
      IPXRowSelectingSubscriber[] selectingSubscriberArray;
      if (slot._optimizedHandlersByCache.TryGetValue(cache, out selectingSubscriberArray))
        cache._EventsRowAttr.RowSelecting = selectingSubscriberArray;
      return Disposable.Create<PXCache>(cache, (System.Action<PXCache>) (c =>
      {
        c._EventsRowAttr.RowSelecting = originalHandlers;
        c._AggregateSelecting = originalAggregateSelectingValue;
      }));
    }

    private static bool ShouldCachesBeMarkedAsAggregateSelecting(PXGenericInqGrph graph)
    {
      if (graph == null)
        return false;
      if (graph._shouldCachesBeMarkedAsAggregateSelecting.HasValue)
        return graph._shouldCachesBeMarkedAsAggregateSelecting.Value;
      PXQueryDescription queryDescription = graph.BaseQueryDescription;
      graph._shouldCachesBeMarkedAsAggregateSelecting = new bool?(queryDescription.GroupBys.Any<PXGroupBy>() || queryDescription.UsedTables.Values.Any<PXTable>((Func<PXTable, bool>) (x => PXGenericInqGrph.PXRowSelectingFieldScope.ShouldCachesBeMarkedAsAggregateSelecting(x.Graph))));
      return graph._shouldCachesBeMarkedAsAggregateSelecting.Value;
    }

    public void Dispose()
    {
      PXContext.SetSlot<PXGenericInqGrph.PXRowSelectingFieldScope>(this._previous);
    }

    private static IDictionary<PXCache, ISet<string>> CreatePerCacheRestrictedFieldsMap(
      PXGenericInqGrph graph,
      PXQueryDescription descr,
      IEnumerable<string> usedFields)
    {
      Dictionary<string, PXCache> dictionary = new Dictionary<string, PXCache>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      Dictionary<PXCache, ISet<string>> restrictedFieldsMap = new Dictionary<PXCache, ISet<string>>();
      foreach (PXTable pxTable in descr.UsedTables.Values)
      {
        PXCache cach = graph.Caches[pxTable.CacheType];
        dictionary[pxTable.Alias] = cach;
        restrictedFieldsMap[cach] = (ISet<string>) new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      }
      foreach (string usedField in usedFields)
      {
        string[] strArray = usedField.Split('_');
        if (strArray.Length >= 2)
        {
          string str1;
          string str2;
          ArrayDeconstruct.Deconstruct<string>(strArray, ref str1, ref str2);
          string key = str1;
          string str3 = str2;
          ArrayDeconstruct.Deconstruct<string>(usedField.Split('_', 2), ref str2, ref str1);
          string field = str1;
          PXCache pxCache;
          if (dictionary.TryGetValue(key, out pxCache))
          {
            ISet<string> restrictedFields = restrictedFieldsMap[pxCache];
            if (!TryProcessAsSpecialField(pxCache, field, restrictedFields))
              restrictedFields.Add(str3);
          }
        }
      }
      return (IDictionary<PXCache, ISet<string>>) restrictedFieldsMap;

      static bool TryProcessAsSpecialField(
        PXCache cache,
        string field,
        ISet<string> restrictedFields)
      {
        return TryProcessAsAttributeField(cache, field, restrictedFields) || TryProcessAsNoteField(field, restrictedFields);
      }

      static bool TryProcessAsAttributeField(
        PXCache cache,
        string field,
        ISet<string> restrictedFields)
      {
        if (!cache.IsAttributesField(field))
          return false;
        string str1 = "Attributes";
        if (cache.Fields.Contains(str1))
        {
          restrictedFields.Add(str1);
        }
        else
        {
          string str2 = cache.Fields.FirstOrDefault<string>((Func<string, bool>) (f => cache.GetAttributesReadonly(f).OfType<PXDBAttributeAttribute>().Any<PXDBAttributeAttribute>()));
          if (!string.IsNullOrEmpty(str2))
            restrictedFields.Add(str2);
        }
        return true;
      }

      static bool TryProcessAsNoteField(string field, ISet<string> restrictedFields)
      {
        if (!PXNoteAttribute.IsNoteRelatedField(field))
          return false;
        restrictedFields.Add(field);
        restrictedFields.Add("NoteID");
        return true;
      }
    }

    private abstract class PassThroughSubscriber : PXEventSubscriberAttribute
    {
      private PassThroughSubscriber() => this._AttributeLevel = PXAttributeLevel.Cache;

      public class Incrementing : 
        PXGenericInqGrph.PXRowSelectingFieldScope.PassThroughSubscriber,
        IPXRowSelectingSubscriber
      {
        public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e) => ++e.Position;
      }

      public class NonIncrementing : 
        PXGenericInqGrph.PXRowSelectingFieldScope.PassThroughSubscriber,
        IPXRowSelectingSubscriber
      {
        public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
        {
        }
      }
    }
  }

  /// <exclude />
  [PXInternalUseOnly]
  public class Definition : 
    IPrefetchable,
    IPXCompanyDependent,
    IEnumerable<GIDescription>,
    IEnumerable
  {
    private IDictionary<Guid, GIDescription> _descriptionsByID;
    private IDictionary<string, GIDescription> _descriptionsByName;
    private static readonly System.Type[] _usedTables = new System.Type[16 /*0x10*/]
    {
      typeof (GIDesign),
      typeof (GIFilter),
      typeof (GIGroupBy),
      typeof (GIMassAction),
      typeof (GIMassUpdateField),
      typeof (GINavigationScreen),
      typeof (GINavigationParameter),
      typeof (GINavigationCondition),
      typeof (GIRecordDefault),
      typeof (GIRelation),
      typeof (GIOn),
      typeof (GIResult),
      typeof (GISort),
      typeof (GITable),
      typeof (GIWhere),
      typeof (PXGraph.FeaturesSet)
    };

    private IDictionary<Guid, List<T>> SelectTable<T>(
      Func<T, Guid> keyFunc,
      params PXDataField[] restrictions)
      where T : class, IBqlTable, new()
    {
      IDictionary<Guid, List<T>> dictionary = (IDictionary<Guid, List<T>>) new Dictionary<Guid, List<T>>();
      foreach (T selectRecord in PXDatabase.SelectRecords<T>(restrictions))
      {
        Guid key = keyFunc(selectRecord);
        if (!dictionary.ContainsKey(key))
          dictionary[key] = new List<T>();
        dictionary[key].Add(selectRecord);
      }
      return dictionary;
    }

    private IEnumerable<T> GetValue<T>(IDictionary<Guid, List<T>> dict, Guid key)
    {
      List<T> objList;
      return !dict.TryGetValue(key, out objList) ? Enumerable.Empty<T>() : (IEnumerable<T>) objList;
    }

    public static System.Type[] UsedTables => PXGenericInqGrph.Definition._usedTables;

    public void Prefetch()
    {
      this._descriptionsByID = (IDictionary<Guid, GIDescription>) new Dictionary<Guid, GIDescription>();
      this._descriptionsByName = (IDictionary<string, GIDescription>) new Dictionary<string, GIDescription>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
      IDictionary<Guid, List<GIFilter>> dict1 = this.SelectTable<GIFilter>((Func<GIFilter, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GIFilter.isActive>((object) true), (PXDataField) new PXDataFieldOrder<GIFilter.lineNbr>());
      IDictionary<Guid, List<GIGroupBy>> dict2 = this.SelectTable<GIGroupBy>((Func<GIGroupBy, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GIGroupBy.isActive>((object) true), (PXDataField) new PXDataFieldOrder<GIGroupBy.lineNbr>());
      IDictionary<Guid, List<GIMassAction>> dict3 = this.SelectTable<GIMassAction>((Func<GIMassAction, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GIMassAction.isActive>((object) true));
      IDictionary<Guid, List<GIMassUpdateField>> dict4 = this.SelectTable<GIMassUpdateField>((Func<GIMassUpdateField, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GIMassUpdateField.isActive>((object) true));
      IDictionary<Guid, List<GINavigationScreen>> dict5 = this.SelectTable<GINavigationScreen>((Func<GINavigationScreen, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldOrder<GINavigationScreen.sortOrder>(), (PXDataField) new PXDataFieldOrder<GINavigationScreen.lineNbr>());
      IDictionary<Guid, List<GINavigationParameter>> dict6 = this.SelectTable<GINavigationParameter>((Func<GINavigationParameter, Guid>) (i => i.DesignID.Value));
      IDictionary<Guid, List<GINavigationCondition>> dict7 = this.SelectTable<GINavigationCondition>((Func<GINavigationCondition, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GINavigationCondition.isActive>((object) true));
      IDictionary<Guid, List<GIRecordDefault>> dict8 = this.SelectTable<GIRecordDefault>((Func<GIRecordDefault, Guid>) (i => i.DesignID.Value));
      IDictionary<Guid, List<GIRelation>> dict9 = this.SelectTable<GIRelation>((Func<GIRelation, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GIRelation.isActive>((object) true), (PXDataField) new PXDataFieldOrder<GIRelation.lineNbr>());
      IDictionary<Guid, List<GIOn>> dict10 = this.SelectTable<GIOn>((Func<GIOn, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldOrder<GIOn.lineNbr>());
      IDictionary<Guid, List<GIResult>> dict11 = this.SelectTable<GIResult>((Func<GIResult, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GIResult.isActive>((object) true), (PXDataField) new PXDataFieldOrder<GIResult.designID>(), (PXDataField) new PXDataFieldOrder<GIResult.sortOrder>(), (PXDataField) new PXDataFieldOrder<GIResult.lineNbr>());
      IDictionary<Guid, List<GISort>> dict12 = this.SelectTable<GISort>((Func<GISort, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GISort.isActive>((object) true), (PXDataField) new PXDataFieldOrder<GISort.lineNbr>());
      IDictionary<Guid, List<GITable>> dict13 = this.SelectTable<GITable>((Func<GITable, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldOrder<GITable.name>());
      IDictionary<Guid, List<GIWhere>> dict14 = this.SelectTable<GIWhere>((Func<GIWhere, Guid>) (i => i.DesignID.Value), (PXDataField) new PXDataFieldValue<GIWhere.isActive>((object) true), (PXDataField) new PXDataFieldOrder<GIWhere.lineNbr>());
      foreach (GIDescription giDescription in PXDatabase.SelectRecords<GIDesign>().Select<GIDesign, GIDescription>((Func<GIDesign, GIDescription>) (design => new GIDescription(design.DesignID.Value)
      {
        Design = design
      })))
      {
        this._descriptionsByID.Add(giDescription.DesignID, giDescription);
        this._descriptionsByName.Add(giDescription.Design.Name, giDescription);
        giDescription.Filters = this.GetValue<GIFilter>(dict1, giDescription.DesignID);
        giDescription.GroupBys = this.GetValue<GIGroupBy>(dict2, giDescription.DesignID);
        giDescription.MassActions = this.GetValue<GIMassAction>(dict3, giDescription.DesignID);
        giDescription.MassUpdateFields = this.GetValue<GIMassUpdateField>(dict4, giDescription.DesignID);
        giDescription.NavigationScreens = this.GetValue<GINavigationScreen>(dict5, giDescription.DesignID);
        giDescription.NavigationParameters = this.GetValue<GINavigationParameter>(dict6, giDescription.DesignID);
        giDescription.NavigationConditions = this.GetValue<GINavigationCondition>(dict7, giDescription.DesignID);
        giDescription.RecordDefaults = this.GetValue<GIRecordDefault>(dict8, giDescription.DesignID);
        giDescription.Relations = this.GetValue<GIRelation>(dict9, giDescription.DesignID);
        giDescription.Ons = this.GetValue<GIOn>(dict10, giDescription.DesignID);
        giDescription.Results = this.GetValue<GIResult>(dict11, giDescription.DesignID);
        giDescription.Sorts = this.GetValue<GISort>(dict12, giDescription.DesignID);
        giDescription.Tables = this.GetValue<GITable>(dict13, giDescription.DesignID);
        giDescription.Wheres = this.GetValue<GIWhere>(dict14, giDescription.DesignID);
      }
    }

    public GIDescription this[Guid designID]
    {
      get
      {
        GIDescription giDescription;
        return !this._descriptionsByID.TryGetValue(designID, out giDescription) ? (GIDescription) null : giDescription;
      }
    }

    public GIDescription this[string name]
    {
      get
      {
        if (name == null)
          return (GIDescription) null;
        GIDescription giDescription;
        return !this._descriptionsByName.TryGetValue(name, out giDescription) ? (GIDescription) null : giDescription;
      }
    }

    public IEnumerator<GIDescription> GetEnumerator()
    {
      return this._descriptionsByID.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }

  [PXInternalUseOnly]
  public class DynamicFieldsScope : IDisposable
  {
    private PXGenericInqGrph _graph;

    public DynamicFieldsScope(PXGenericInqGrph graph)
    {
      this._graph = graph;
      GIDesign design = graph.Design;
      int num;
      if (design == null)
      {
        num = 0;
      }
      else
      {
        bool? showDeletedRecords = design.ShowDeletedRecords;
        bool flag = true;
        num = showDeletedRecords.GetValueOrDefault() == flag & showDeletedRecords.HasValue ? 1 : 0;
      }
      if (num == 0 || Interlocked.Increment(ref graph._dfsCount) != 1)
        return;
      graph.AddDynamicFields();
    }

    void IDisposable.Dispose()
    {
      GIDesign design = this._graph.Design;
      int num;
      if (design == null)
      {
        num = 0;
      }
      else
      {
        bool? showDeletedRecords = design.ShowDeletedRecords;
        bool flag = true;
        num = showDeletedRecords.GetValueOrDefault() == flag & showDeletedRecords.HasValue ? 1 : 0;
      }
      if (num == 0 || Interlocked.Decrement(ref this._graph._dfsCount) != 0)
        return;
      this._graph.RemoveDynamicFields();
    }
  }

  private class DynamicField
  {
    public System.Type bqlTable;
    public string name;
    public List<System.Action<PXCache>> cacheAttached = new List<System.Action<PXCache>>();
    public List<PXCommandPreparing> commandPreparing = new List<PXCommandPreparing>();
    public List<PXFieldSelecting> fieldSelecting = new List<PXFieldSelecting>();
    public List<PXFieldUpdating> fieldUpdating = new List<PXFieldUpdating>();
    public List<PXRowSelecting> rowSelecting = new List<PXRowSelecting>();
  }

  [PXInternalUseOnly]
  public class ParametersChangedIndexer
  {
    private string GetKey(Guid designID)
    {
      return "GenericInquiryParametersChanged$" + designID.ToString();
    }

    public bool this[Guid designID]
    {
      get
      {
        bool? nullable = PXContext.Session.GenericInquiryParametersChanged[this.GetKey(designID)];
        bool flag = true;
        return nullable.GetValueOrDefault() == flag & nullable.HasValue;
      }
      set
      {
        if (value)
          PXContext.Session.GenericInquiryParametersChanged[this.GetKey(designID)] = new bool?(true);
        else
          PXContext.Session.GenericInquiryParametersChanged[this.GetKey(designID)] = new bool?();
      }
    }
  }

  /// <exclude />
  [PXVirtual]
  [PXHidden]
  [Serializable]
  public class GIKeyDefault : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(256 /*0x0100*/, IsKey = true, InputMask = "", IsUnicode = true)]
    [PXUIField(Visible = false)]
    public string FieldName { get; set; }

    [PXString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Key", Enabled = false)]
    public string DisplayName { get; set; }

    [PrimaryViewValueList(512 /*0x0200*/, typeof (GIDesign.primaryScreenID), typeof (PXGenericInqGrph.GIKeyDefault.fieldName))]
    [PXUIField(DisplayName = "Value")]
    public string Value { get; set; }

    /// <exclude />
    public abstract class fieldName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInqGrph.GIKeyDefault.fieldName>
    {
    }

    /// <exclude />
    public abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInqGrph.GIKeyDefault.displayName>
    {
    }

    /// <exclude />
    public abstract class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInqGrph.GIKeyDefault.value>
    {
    }
  }

  /// <exclude />
  [PXVirtual]
  [PXHidden]
  [Serializable]
  public class GIUpdateValue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private bool? _Selected;

    [PXBool]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXString(256 /*0x0100*/, IsKey = true, InputMask = "", IsUnicode = true)]
    [PXUIField(Visible = false)]
    public string FieldName { get; set; }

    [PXString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Name", Enabled = false)]
    public string DisplayName { get; set; }

    [PrimaryViewValueList(512 /*0x0200*/, typeof (GIDesign.primaryScreenID), typeof (PXGenericInqGrph.GIUpdateValue.fieldName))]
    [PXUIField(DisplayName = "Value")]
    public string Value { get; set; }

    /// <exclude />
    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXGenericInqGrph.GIUpdateValue.selected>
    {
    }

    /// <exclude />
    public abstract class fieldName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInqGrph.GIUpdateValue.fieldName>
    {
    }

    /// <exclude />
    public abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInqGrph.GIUpdateValue.displayName>
    {
    }

    /// <exclude />
    public abstract class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInqGrph.GIUpdateValue.value>
    {
    }
  }

  internal class GiNavigationSidePanelAction : PXGraph.SidePanelAction
  {
    public int ScreenLineNbr { get; }

    public GiNavigationSidePanelAction(
      string actionName,
      string icon,
      string toolTip,
      int lineNbr)
      : base(actionName, icon, toolTip)
    {
      this.ScreenLineNbr = lineNbr;
    }
  }
}
