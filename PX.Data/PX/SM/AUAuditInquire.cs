// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditInquire
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data;
using PX.Data.Process;
using PX.Data.SQLTree;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

public class AUAuditInquire : PXGraph<AUAuditInquire>
{
  public const string VIRTUAL_FIELD_SUFFIX = "VIRTUAL_FIELD";
  public static readonly string[] FORBIDDEN_COLUMNS = new string[2]
  {
    "CombinedKey",
    "Num"
  };
  private AUAuditInquire.AuditState _state;
  public PXFilter<AUAuditFilter> Filter;
  public PXCancel<AUAuditFilter> Cancel;
  public PXSelect<AuditHistory> Audit;
  [PXFilterable(new System.Type[] {})]
  [PXVirtualDAC]
  public PXSelect<AUAuditKeys> Keys;
  [PXFilterable(new System.Type[] {})]
  [PXVirtualDAC]
  public PXSelect<AUAuditValues> Changes;
  [PXHidden]
  public PXSelect<AUAuditSetup> Setup;
  public PXAction<AUAuditFilter> Manage;

  private AUAuditInquire.AuditState State
  {
    get
    {
      AUAuditFilter current = this.Filter.Current;
      if (current == null || string.IsNullOrWhiteSpace(current.ScreenID))
        return (AUAuditInquire.AuditState) null;
      string tableName = this._state != null ? this._state.TableName : (string) null;
      if (this._state == null || this._state.ScreenID != current.ScreenID || this._state.TableName != current.TableName)
      {
        this._state = (AUAuditInquire.AuditState) null;
        AUAuditInquire.AuditState auditState = new AUAuditInquire.AuditState()
        {
          ScreenID = current.ScreenID
        };
        string graphType = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(current.ScreenID)?.GraphType;
        if (string.IsNullOrWhiteSpace(graphType))
          return (AUAuditInquire.AuditState) null;
        auditState.GraphType = PXBuildManager.GetType(graphType, false);
        auditState.Graph = PXGraph.CreateInstance(auditState.GraphType);
        for (int index = 0; index < auditState.Graph.Views.Caches.Count; ++index)
        {
          PXCache cach = auditState.Graph.Caches[auditState.Graph.Views.Caches[index]];
        }
        foreach ((System.Type type, string _, string _) in AuditMaintDataLoader.GetTablesFromScreen(current.ScreenID, out PXGraph _))
        {
          PXCache cach = auditState.Graph.Caches[type];
        }
        auditState.TableName = current.TableName;
        auditState.PrevTableName = tableName;
        auditState.CacheTableName = this.GetTableName(current.TableName, auditState.Graph);
        this._state = auditState;
        this.Initialise();
      }
      return this._state;
    }
  }

  public AUAuditInquire()
  {
    this.Keys.Cache.AllowInsert = false;
    this.Keys.Cache.AllowUpdate = false;
    this.Keys.Cache.AllowDelete = false;
    this.Changes.Cache.AllowInsert = false;
    this.Changes.Cache.AllowUpdate = false;
    this.Changes.Cache.AllowDelete = false;
    this.Setup.Cache.AllowInsert = false;
    this.Setup.Cache.AllowUpdate = false;
    this.Setup.Cache.AllowDelete = false;
  }

  public IEnumerable keys()
  {
    AUAuditInquire graph = this;
    AUAuditInquire.AuditState state = graph.State;
    AUAuditFilter current = graph.Filter.Current;
    if (state != null && !string.IsNullOrEmpty(state.TableName) && current != null)
    {
      PXCache cache = state.Graph.Caches[state.CacheTableName];
      string name = PXCache.GetBqlTable(cache.GetItemType()).Name;
      PXSelectBase<AuditHistory> pxSelectBase = (PXSelectBase<AuditHistory>) new PXSelectGroupBy<AuditHistory, Where<AuditHistory.tableName, Equal<Required<AuditHistory.tableName>>>, Aggregate<GroupBy<AuditHistory.combinedKey>>, OrderBy<Asc<AuditHistory.batchID>>>((PXGraph) graph);
      if (current.UserID.HasValue)
        pxSelectBase.WhereAnd<Where<AuditHistory.userID, Equal<Current<AUAuditFilter.userID>>>>();
      if (current.StartDate.HasValue)
        pxSelectBase.WhereAnd<Where<AuditHistory.changeDate, GreaterEqual<Current<AUAuditFilter.startDate>>>>();
      if (current.EndDate.HasValue)
        pxSelectBase.WhereAnd<Where<AuditHistory.changeDate, LessEqual<Current<AUAuditFilter.endDate>>>>();
      int num1 = 0;
      int maximumRows1 = PXView.SortColumns.Length > 1 | PXView.Filters.Length > 0 ? 0 : PXView.MaximumRows;
      int num2 = PXView.StartRow;
      if (maximumRows1 == 1)
      {
        if (PXView.Searches.Length != 0 && PXView.Searches[0] != null)
          num2 = (int) PXView.Searches[0] - 1;
      }
      else if (num2 != 0)
        PXView.StartRow = 0;
      int counter = num2;
      pxSelectBase.View.Clear();
      PXView view = pxSelectBase.View;
      object[] currents = PXView.Currents;
      object[] parameters = new object[1]{ (object) name };
      PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
      ref int local1 = ref num2;
      int maximumRows2 = maximumRows1;
      ref int local2 = ref num1;
      foreach (AuditHistory auditHistory in view.Select(currents, parameters, (object[]) null, (string[]) null, (bool[]) null, filters, ref local1, maximumRows2, ref local2))
      {
        ++counter;
        if (!string.IsNullOrEmpty(auditHistory.CombinedKey))
        {
          string[] keys = PXAuditHelper.GetKeys(cache, state.ScreenID);
          string[] strArray = auditHistory.CombinedKey.Split(PXAuditHelper.SEPARATOR);
          if (strArray.Length == keys.Length)
          {
            object instance = cache.CreateInstance();
            for (int index = 0; index < strArray.Length; ++index)
            {
              string fieldName = keys[index];
              string val = strArray[index];
              object obj = string.IsNullOrWhiteSpace(val) ? (object) null : cache.ValueFromString(fieldName, val);
              cache.SetValue(instance, fieldName, obj);
            }
            yield return (object) new AUAuditKeys()
            {
              Num = new int?(counter),
              Row = instance,
              CombinedKey = auditHistory.CombinedKey
            };
          }
        }
      }
      cache.IsDirty = false;
    }
  }

  public IEnumerable changes()
  {
    AUAuditInquire.AuditState state = this.State;
    AUAuditKeys current = this.Keys.Current;
    if (state != null && !string.IsNullOrEmpty(state.TableName) && (state.Graph.Caches[state.CacheTableName].Keys.Count <= 0 || current != null && !string.IsNullOrEmpty(current.CombinedKey)))
    {
      PXCache cache = state.Graph.Caches[state.CacheTableName];
      Dictionary<string, object> lastValues = new Dictionary<string, object>();
      PXView auditTablesView = this.GetAuditTablesView(cache);
      if (auditTablesView != null)
      {
        int totalRows = 0;
        int startRow = PXView.StartRow;
        string keyParam = current == null ? string.Empty : current.CombinedKey;
        int maximumRows = PXView.MaximumRows * 5;
        List<object> objectList = auditTablesView.Select(PXView.Currents, this.GetAuditTablesParams(state.TableName, cache, keyParam), (object[]) null, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, maximumRows, ref totalRows);
        AUAuditValues auAuditValues = (AUAuditValues) null;
        int recCount = 0;
        foreach (PXResult<AuditHistory, Users> pxResult in objectList)
        {
          if (recCount != PXView.MaximumRows || PXView.MaximumRows == 0)
          {
            AuditHistory auditHistory = pxResult[typeof (AuditHistory)] as AuditHistory;
            Users users = pxResult[typeof (Users)] as Users;
            long? batchId1 = (long?) auAuditValues?.BatchID;
            long? batchId2 = auditHistory.BatchID;
            if ((!(batchId1.GetValueOrDefault() == batchId2.GetValueOrDefault() & batchId1.HasValue == batchId2.HasValue) ? 1 : (auAuditValues.Operation != auditHistory.Operation ? 1 : 0)) != 0)
            {
              if (auAuditValues != null)
              {
                ++recCount;
                yield return (object) auAuditValues;
              }
              auAuditValues = new AUAuditValues();
              auAuditValues.Row = cache.CreateInstance();
              if (cache._KeyValueAttributeNames != null)
                cache.SetSlot<object[]>(auAuditValues.Row, cache._KeyValueAttributeSlotPosition, new object[cache._KeyValueAttributeNames.Count], true);
              auAuditValues.BatchID = auditHistory.BatchID;
              auAuditValues.ChangeDate = auditHistory.ChangeDate;
              auAuditValues.ChangeID = auditHistory.ChangeID;
              auAuditValues.Operation = auditHistory.Operation;
              auAuditValues.UserName = users?.Username;
              foreach (KeyValuePair<string, object> keyValuePair in lastValues)
              {
                if (!cache.IsKvExtAttribute(keyValuePair.Key))
                  cache.SetValue(auAuditValues.Row, keyValuePair.Key, keyValuePair.Value);
                else
                  cache.SetValueExt(auAuditValues.Row, keyValuePair.Key, keyValuePair.Value);
              }
              this.FillKeys(cache, auAuditValues.Row, auditHistory.CombinedKey, lastValues, auditHistory.ScreenID);
            }
            if (!string.IsNullOrEmpty(auditHistory.ModifiedFields))
              this.FillFields(cache, auAuditValues.Row, auditHistory.ModifiedFields, lastValues);
            auditHistory = (AuditHistory) null;
            users = (Users) null;
          }
          else
            break;
        }
        if (auAuditValues != null && recCount < PXView.MaximumRows || PXView.MaximumRows == 0)
          yield return (object) auAuditValues;
        PXView.StartRow = 0;
      }
      cache.IsDirty = false;
    }
  }

  private object[] GetAuditTablesParams(string tableName, PXCache tableCache, string keyParam)
  {
    object[] auditTablesParams = (object[]) null;
    if (!string.IsNullOrEmpty(tableName) && tableCache != null)
    {
      List<object> objectList = new List<object>()
      {
        (object) keyParam,
        (object) tableName
      };
      List<System.Type> extensionTables = tableCache.GetExtensionTables();
      if (extensionTables != null)
        objectList.AddRange((IEnumerable<object>) extensionTables.Select<System.Type, string>((Func<System.Type, string>) (extTable => extTable.Name)));
      auditTablesParams = objectList.ToArray();
    }
    return auditTablesParams;
  }

  private PXView GetAuditTablesView(PXCache tableCache)
  {
    PXView auditTablesView = (PXView) null;
    if (tableCache != null)
    {
      BqlCommand bqlCommand = (BqlCommand) new Select2<AuditHistory, InnerJoin<Users, On<Users.pKID, Equal<AuditHistory.userID>>>, Where<AuditHistory.combinedKey, Equal<Required<AuditHistory.combinedKey>>>, OrderBy<Asc<AuditHistory.batchID, Asc<AuditHistory.changeID>>>>();
      List<System.Type> extensionTables = tableCache.GetExtensionTables();
      int count = extensionTables == null ? 1 : extensionTables.Count + 1;
      auditTablesView = new PXView((PXGraph) this, true, this.AppendCurrentFilter(bqlCommand.WhereAnd(InHelper<AuditHistory.tableName>.Create(count))));
      auditTablesView.Clear();
    }
    return auditTablesView;
  }

  private BqlCommand AppendCurrentFilter(BqlCommand select)
  {
    AUAuditFilter current = this.Filter?.Current;
    if (current == null || select == null)
      return select;
    if (current.UserID.HasValue)
      select = select.WhereAnd<Where<AuditHistory.userID, Equal<Current<AUAuditFilter.userID>>>>();
    if (current.StartDate.HasValue)
      select = select.WhereAnd<Where<AuditHistory.changeDate, GreaterEqual<Current<AUAuditFilter.startDate>>>>();
    if (current.EndDate.HasValue)
      select = select.WhereAnd<Where<AuditHistory.changeDate, LessEqual<Current<AUAuditFilter.endDate>>>>();
    return select;
  }

  /// <summary>
  /// Fills modified fields with new values and updates "lastValues" dictionary.
  /// </summary>
  protected virtual void FillFields(
    PXCache cache,
    object row,
    string fields,
    Dictionary<string, object> lastValues)
  {
    string[] strArray = fields.Split(PXAuditHelper.SEPARATOR);
    if (strArray.Length % 2 != 0)
      return;
    for (int index = 0; index < strArray.Length; index += 2)
    {
      string str = strArray[index];
      string val = strArray[index + 1];
      object obj = string.IsNullOrWhiteSpace(val) ? (object) null : cache.ValueFromString(str, val);
      if (!cache.IsKvExtAttribute(str))
        cache.SetValue(row, str, obj);
      else
        cache.SetValueExt(row, str, obj);
      lastValues[str] = obj;
    }
  }

  protected virtual void FillKeys(
    PXCache cache,
    object row,
    string fields,
    Dictionary<string, object> lastValues,
    string screenId)
  {
    if (string.IsNullOrEmpty(fields))
      return;
    string[] strArray = fields.Split(PXAuditHelper.SEPARATOR);
    string[] keys = PXAuditHelper.GetKeys(cache, screenId);
    for (int index = 0; index < strArray.Length; ++index)
    {
      string str = keys[index];
      string val = strArray[index];
      object obj = string.IsNullOrWhiteSpace(val) ? (object) null : cache.ValueFromString(str, val);
      cache.SetValue(row, str, obj);
      lastValues[str] = obj;
    }
  }

  public override void Clear()
  {
    base.Clear();
    this._state = (AUAuditInquire.AuditState) null;
  }

  public void Initialise()
  {
    this.InitialiseKeys();
    this.InitialiseChanges();
    System.Action onInitialised = this.OnInitialised;
    if (onInitialised == null)
      return;
    onInitialised();
  }

  public void InitialiseKeys()
  {
    AUAuditInquire.AuditState state = this.State;
    if (state == null || string.IsNullOrEmpty(state.TableName))
      return;
    PXCache cache = state.Graph.Caches[state.CacheTableName];
    if (state.TableName != state.PrevTableName)
    {
      this.Keys.Cache.FieldSelectingEvents.Clear();
      this.Keys.Cache.FieldUpdatedEvents.Clear();
      for (int index = this.Keys.Cache.Fields.Count - 1; index >= 0; --index)
      {
        if (this.Keys.Cache.Fields[index].EndsWith("VIRTUAL_FIELD", true, (CultureInfo) null))
          this.Keys.Cache.Fields.Remove(this.Keys.Cache.Fields[index]);
      }
    }
    foreach (string key in PXAuditHelper.GetKeys(cache, state.ScreenID))
    {
      string field = key;
      string encoded = key + "VIRTUAL_FIELD";
      if (!this.Keys.Cache.Fields.Contains(encoded))
      {
        this.Keys.Cache.Fields.Add(encoded);
        this.FieldSelecting.AddHandler("Keys", encoded, (PXFieldSelecting) ((sender, args) =>
        {
          object row = args.Row != null ? (args.Row as AUAuditKeys).Row : (object) null;
          try
          {
            if (args.Row == null || row == null)
              args.ReturnState = cache.GetStateExt((object) null, field);
            else
              args.ReturnValue = cache.GetValueExt(row, field);
          }
          catch (Exception ex)
          {
            args.ReturnState = (object) AUAuditInquire.CreateErrorState(ex.Message);
          }
          if (args.ReturnState == null || !(args.ReturnState is PXFieldState))
            return;
          PXFieldState returnState = args.ReturnState as PXFieldState;
          returnState.SetFieldName(encoded);
          returnState.Visible = true;
          returnState.Visibility = PXUIVisibility.Visible;
          returnState.ViewName = (string) null;
          if (!returnState.DisplayName.EndsWith("VIRTUAL_FIELD", true, (CultureInfo) null))
            return;
          returnState.DisplayName = field;
        }));
        this.FieldUpdating.AddHandler("Keys", encoded, (PXFieldUpdating) ((sender, args) =>
        {
          object row = args.Row != null ? (args.Row as AUAuditKeys).Row : (object) null;
          object newValue = args.NewValue;
          cache.RaiseFieldUpdating(field, row, ref newValue);
          args.NewValue = newValue;
        }));
      }
    }
  }

  public void InitialiseChanges()
  {
    AUAuditInquire.AuditState state = this.State;
    if (state == null || string.IsNullOrEmpty(state.TableName))
      return;
    PXCache cache = state.Graph.Caches[state.CacheTableName];
    cache._SelectingForAuditExplore = true;
    if (state.TableName != state.PrevTableName)
    {
      foreach (string key in this.Changes.Cache.FieldSelectingEvents.Keys.ToArray<string>())
      {
        if (key.EndsWith("VIRTUAL_FIELD", true, (CultureInfo) null))
          this.Changes.Cache.FieldSelectingEvents.Remove(key);
      }
      foreach (string key in this.Changes.Cache.FieldUpdatedEvents.Keys.ToArray<string>())
      {
        if (key.EndsWith("VIRTUAL_FIELD", true, (CultureInfo) null))
          this.Changes.Cache.FieldUpdatedEvents.Remove(key);
      }
      for (int index = this.Changes.Cache.Fields.Count - 1; index >= 0; --index)
      {
        if (this.Changes.Cache.Fields[index].EndsWith("VIRTUAL_FIELD", true, (CultureInfo) null))
          this.Changes.Cache.Fields.Remove(this.Changes.Cache.Fields[index]);
      }
    }
    foreach (string field1 in this.GetFields(cache))
    {
      if ((cache.Identity != null || !cache.Keys.Contains(field1)) && (cache.Identity == null || !string.Equals(field1, cache.Identity, StringComparison.OrdinalIgnoreCase)))
      {
        string field = field1;
        string encoded = field1 + "VIRTUAL_FIELD";
        if (!this.Changes.Cache.Fields.Contains(encoded))
        {
          this.Changes.Cache.Fields.Add(encoded);
          this.FieldSelecting.AddHandler("Changes", encoded, (PXFieldSelecting) ((sender, args) =>
          {
            object row = args.Row != null ? (args.Row as AUAuditValues).Row : (object) null;
            try
            {
              if (args.Row == null || row == null)
                args.ReturnState = cache.GetStateExt((object) null, field);
              else
                args.ReturnValue = cache.GetValueExt(row, field);
            }
            catch (Exception ex)
            {
              args.ReturnState = (object) AUAuditInquire.CreateErrorState(ex.Message);
            }
            if (args.ReturnState == null || !(args.ReturnState is PXFieldState))
              return;
            PXFieldState returnState = args.ReturnState as PXFieldState;
            returnState.SetFieldName(encoded);
            returnState.Visible = true;
            returnState.Visibility = PXUIVisibility.Visible;
            returnState.ViewName = (string) null;
            if (!returnState.DisplayName.EndsWith("VIRTUAL_FIELD", true, (CultureInfo) null))
              return;
            returnState.DisplayName = field;
          }));
          this.FieldUpdating.AddHandler("Changes", encoded, (PXFieldUpdating) ((sender, args) =>
          {
            object row = args.Row != null ? (args.Row as AUAuditValues).Row : (object) null;
            object newValue = args.NewValue;
            cache.RaiseFieldUpdating(field, row, ref newValue);
            args.NewValue = newValue;
          }));
        }
      }
    }
  }

  private static PXFieldState CreateErrorState(string errorMessage)
  {
    PXFieldState instance = PXStringState.CreateInstance((object) null, new int?(), new bool?(true), (string) null, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
    instance.Error = PXLocalizer.LocalizeFormat("This value cannot be obtained because of the following error: {0}", (object) errorMessage);
    instance.ErrorLevel = PXErrorLevel.Error;
    return instance;
  }

  private string GetTableName(string stateTableName, PXGraph stateGraph)
  {
    string tableName = (string) null;
    if (!string.IsNullOrEmpty(stateTableName) && stateGraph != null)
    {
      if (stateGraph.Caches[stateTableName] != null)
      {
        tableName = stateTableName;
      }
      else
      {
        System.Type type1 = stateGraph.Caches.Keys.FirstOrDefault<System.Type>((Func<System.Type, bool>) (type => string.CompareOrdinal(TablesAndFieldsInfoExtractor.GetTableName(stateGraph.Caches[type]), stateTableName) == 0));
        if (type1 != (System.Type) null)
          tableName = type1.Name;
      }
    }
    return tableName;
  }

  public event System.Action OnInitialised;

  [PXButton]
  [PXUIField(DisplayName = "Manage")]
  public IEnumerable manage(PXAdapter adapter)
  {
    if (this.Filter.Current == null || string.IsNullOrEmpty(this.Filter.Current.ScreenID))
      return adapter.Get();
    AUAuditMaintenance instance = PXGraph.CreateInstance<AUAuditMaintenance>();
    instance.Audit.Current = (AUAuditSetup) PXSelectBase<AUAuditSetup, PXSelectReadonly<AUAuditSetup, Where<AUAuditSetup.screenID, Equal<Required<AUAuditSetup.screenID>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, (object) this.Filter.Current.ScreenID);
    throw new PXPopupRedirectException((PXGraph) instance, "Redirect", true);
  }

  private IEnumerable<string> GetFields(PXCache cache)
  {
    AUAuditInquire.AuditState state = this.State;
    if (state == null || string.IsNullOrEmpty(state.TableName))
      return (IEnumerable<string>) AuditMaintDataLoader.GetTableFields(cache, (string) null);
    List<string> second = new List<string>();
    foreach (PXResult<AUAuditField> pxResult in PXSelectBase<AUAuditField, PXSelect<AUAuditField, Where<AUAuditField.screenID, Equal<Required<AUAuditField.screenID>>, And<AUAuditField.tableName, Equal<Required<AUAuditField.tableName>>, And<AUAuditField.isActive, Equal<PX.Data.True>>>>>.Config>.Select((PXGraph) this, (object) state.ScreenID, (object) state.TableName))
    {
      AUAuditField auAuditField = (AUAuditField) pxResult;
      second.Add(auAuditField.FieldName);
    }
    return AuditMaintDataLoader.GetTableFields(cache, state.TableName).Except<string>((IEnumerable<string>) second);
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Audited Screen ID")]
  protected virtual void AUAuditSetup_VirtualScreenID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void AUAuditFilter_StartDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUAuditFilter row = (AUAuditFilter) e.Row;
    if (row == null)
      return;
    System.DateTime? startDate = row.StartDate;
    if (!startDate.HasValue)
      return;
    AUAuditFilter auAuditFilter = row;
    startDate = row.StartDate;
    int year = startDate.Value.Year;
    startDate = row.StartDate;
    System.DateTime dateTime = startDate.Value;
    int month = dateTime.Month;
    startDate = row.StartDate;
    dateTime = startDate.Value;
    int day = dateTime.Day;
    System.DateTime? nullable = new System.DateTime?(new System.DateTime(year, month, day, 0, 0, 0));
    auAuditFilter.StartDate = nullable;
  }

  protected virtual void AUAuditFilter_EndDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUAuditFilter row = (AUAuditFilter) e.Row;
    if (row == null)
      return;
    System.DateTime? endDate = row.EndDate;
    if (!endDate.HasValue)
      return;
    AUAuditFilter auAuditFilter = row;
    endDate = row.EndDate;
    int year = endDate.Value.Year;
    endDate = row.EndDate;
    System.DateTime dateTime = endDate.Value;
    int month = dateTime.Month;
    endDate = row.EndDate;
    dateTime = endDate.Value;
    int day = dateTime.Day;
    System.DateTime? nullable = new System.DateTime?(new System.DateTime(year, month, day, 23, 59, 59));
    auAuditFilter.EndDate = nullable;
  }

  protected virtual void AUAuditFilter_ScreenID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUAuditFilter row = (AUAuditFilter) e.Row;
    if (string.IsNullOrEmpty(row.ScreenID))
      return;
    string graphType = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(row.ScreenID)?.GraphType;
    if (string.IsNullOrEmpty(graphType))
      return;
    System.Type type = PXBuildManager.GetType(graphType, false);
    if (!(type != (System.Type) null))
      return;
    PXGraph instance = PXGraph.CreateInstance(type);
    if (instance is IPXAuditSource pxAuditSource)
    {
      string mainView = pxAuditSource.GetMainView();
      if (string.IsNullOrEmpty(mainView) || !instance.Views.ContainsKey(mainView))
        return;
      row.TableName = instance.Views[mainView].Cache.BqlTable.Name;
    }
    else
    {
      PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.Get(row.ScreenID);
      if (screenInfo == null)
        return;
      PXView view = instance.Views[screenInfo.PrimaryView];
      row.TableName = view.Cache.BqlTable.Name;
    }
  }

  private static void AggregaeFunctionCanceling(PXCommandPreparingEventArgs e)
  {
    if (e.Operation != PXDBOperation.GroupBy)
      return;
    e.Expr = SQLExpression.Null();
    e.Cancel = true;
  }

  protected virtual void AuditHistory_BatchID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
  }

  protected virtual void AuditHistory_ChangeID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
  }

  protected virtual void AuditHistory_ScreenID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    AUAuditInquire.AggregaeFunctionCanceling(e);
  }

  protected virtual void AuditHistory_UserID_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    AUAuditInquire.AggregaeFunctionCanceling(e);
  }

  protected virtual void AuditHistory_ChangeDate_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    AUAuditInquire.AggregaeFunctionCanceling(e);
  }

  protected virtual void AuditHistory_Operation_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    AUAuditInquire.AggregaeFunctionCanceling(e);
  }

  protected virtual void AuditHistory_TableName_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    AUAuditInquire.AggregaeFunctionCanceling(e);
  }

  protected virtual void AuditHistory_ModifiedFields_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    AUAuditInquire.AggregaeFunctionCanceling(e);
  }

  protected virtual void AUAuditFilter_EndDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    AUAuditFilter row = (AUAuditFilter) e.Row;
    if (row == null)
      return;
    System.DateTime now = System.DateTime.Now;
    row.EndDate = new System.DateTime?(new System.DateTime(now.Year, now.Month, now.Day, 23, 59, 59));
  }

  protected virtual void AUAuditFilter_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    AUAuditFilter row = (AUAuditFilter) e.Row;
    if (row == null)
      return;
    System.DateTime dateTime = System.DateTime.Now.AddMonths(-1);
    row.StartDate = new System.DateTime?(new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0));
  }

  private class AuditState
  {
    public string ScreenID;
    public System.Type GraphType;
    public PXGraph Graph;
    public string TableName;
    public string PrevTableName;
    public string CacheTableName;
  }
}
