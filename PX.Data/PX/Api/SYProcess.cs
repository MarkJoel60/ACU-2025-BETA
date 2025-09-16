// Decompiled with JetBrains decompiler
// Type: PX.Api.SYProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Compilation;

#nullable disable
namespace PX.Api;

public abstract class SYProcess : PXGraph<SYProcess>
{
  public const string ContextKeyForImportErrorHandling = "_ProcessScheduled";
  public const string ContextKeyForImportErrorMessage = "_ProcessScheduledMessage";
  public PXSave<SYImportOperation> Save;
  public PXCancel<SYImportOperation> Cancel;
  public PXFilter<SYImportOperation> Operation;
  public PXFilteredProcessing<SYMappingActive, SYImportOperation> Mappings;
  public PXSelect<SYMappingActive, Where<SYMapping.mappingID, Equal<Current<SYMapping.mappingID>>>> CurrentMapping;
  public PXSelect<SYData, Where<SYData.mappingID, Equal<Current<SYMapping.mappingID>>>> Data;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<SYData, Where<SYData.mappingID, Equal<Current<SYMapping.mappingID>>>> PreparedData;
  public PXSelect<SYHistory, Where<SYHistory.mappingID, Equal<Current<SYMapping.mappingID>>>, OrderBy<Desc<SYHistory.statusDate>>> History;
  public PXSelect<SYMappingFieldSimple, Where<SYMappingField.mappingID, Equal<Current<SYMappingSimpleProperty.mappingID>>>, OrderBy<Asc<SYMappingField.orderNumber>>> MappingsSimple;
  public PXFilter<SYReplace> ReplacementProperties;
  public PXRecords<SYSubstitutionInfo> SubstitutionInfo;
  public PXAction<SYImportOperation> ViewHistory;
  public PXAction<SYImportOperation> ViewPreparedData;
  public PXAction<SYImportOperation> SavePreparedData;
  public PXAction<SYImportOperation> ViewReplacement;
  public PXAction<SYImportOperation> ReplaceOneValue;
  public PXAction<SYImportOperation> ReplaceAllValues;
  public PXAction<SYImportOperation> AddSubstitutions;
  public PXAction<SYImportOperation> SaveSubstitutions;
  public PXAction<SYImportOperation> CloseSubstitutions;
  private readonly Dictionary<int?, Dictionary<string, SyImportRowResult.FieldError>> splittedFieldErrors = new Dictionary<int?, Dictionary<string, SyImportRowResult.FieldError>>();
  private readonly Dictionary<int?, string[]> splittedFieldValues = new Dictionary<int?, string[]>();
  private readonly Dictionary<string, string> dynamicFieldValues = new Dictionary<string, string>();
  private bool _Flag;
  private readonly IList<string> _dynamicColumns = (IList<string>) new List<string>();
  private readonly IDictionary<string, PXFieldSelecting> _dynamicColumnsFieldSelectingHandlers = (IDictionary<string, PXFieldSelecting>) new Dictionary<string, PXFieldSelecting>();
  private readonly IDictionary<string, PXFieldUpdating> _dynamicColumnsFieldUpdating = (IDictionary<string, PXFieldUpdating>) new Dictionary<string, PXFieldUpdating>();
  private readonly IList<PXRowUpdated> _dynamicColumnsEvents = (IList<PXRowUpdated>) new List<PXRowUpdated>();
  public PXSYParameter[] Parameters = new PXSYParameter[0];

  protected IEnumerable mappingsSimple()
  {
    SYProcess syProcess = this;
    SYMappingSimpleProperty current = syProcess.Caches[typeof (SYMappingSimpleProperty)]?.Current as SYMappingSimpleProperty;
    if (current != null && current.MappingID.HasValue)
    {
      foreach (SYMappingFieldSimple mappingFieldSimple in syProcess.MappingsSimple.Cache.Cached)
      {
        Guid? mappingId1 = mappingFieldSimple.MappingID;
        Guid? mappingId2 = current.MappingID;
        if ((mappingId1.HasValue == mappingId2.HasValue ? (mappingId1.HasValue ? (mappingId1.GetValueOrDefault() == mappingId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && EnumerableExtensions.IsNotIn<PXEntryStatus>(syProcess.MappingsSimple.Cache.GetStatus((object) mappingFieldSimple), PXEntryStatus.Notchanged, PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
          yield return (object) mappingFieldSimple;
      }
    }
  }

  public override bool IsProcessing
  {
    get => false;
    set
    {
    }
  }

  protected SYProcess()
  {
    if (HttpContext.Current == null)
      return;
    PXUIFieldAttribute.SetEnabled<SYData.errorMessage>(this.PreparedData.Cache, (object) null, false);
    List<string> displayNames = this.GetDisplayNames();
    string[] array = displayNames.ToArray();
    PXIntListAttribute.SetList<SYReplace.columnIndex>(this.ReplacementProperties.Cache, (object) null, this.getIndexesArray(array.Length), array);
    if (!this.GenerateDynamicInConstructor)
      return;
    this.GenerateDynamicColumns(displayNames);
  }

  protected internal IEnumerable mappings()
  {
    return (IEnumerable) PXSYMappingSelector.GetMappings<SYMappingActive>(new PXView((PXGraph) this, false, this.Mappings.View.BqlSelect));
  }

  protected virtual bool GenerateDynamicInConstructor => true;

  [PXButton(ImageKey = "History", Tooltip = "Show the history for the selected item.")]
  [PXUIField(DisplayName = "History", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable viewHistory(PXAdapter adapter)
  {
    int num = (int) this.History.AskExt(true);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Show the prepared data for the selected item.")]
  [PXUIField(DisplayName = "Prepared Data", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable viewPreparedData(PXAdapter adapter)
  {
    this.Operation.Current.MappingID = (Guid?) this.Mappings.Current?.MappingID;
    int index = this.PreparedData.Cache.BqlFields.Count + 3;
    if (this.PreparedData.Cache.Fields.Count > index)
      this.PreparedData.Cache.Fields.RemoveRange(index, this.PreparedData.Cache.Fields.Count - index);
    int num = (int) this.PreparedData.AskExt(true);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Save (Ctrl+S).")]
  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable savePreparedData(PXAdapter adapter)
  {
    this.PreparedData.Cache.Persist(PXDBOperation.Update);
    this.PreparedData.Cache.IsDirty = false;
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Replace")]
  protected void viewReplacement()
  {
    int num = (int) this.ReplacementProperties.AskExt((PXView.InitializePanel) ((graph, viewname) =>
    {
      this.ReplacementProperties.Cache.Clear();
      this.ReplacementProperties.Cache.Insert();
      this.ReplacementProperties.Cache.IsDirty = false;
    }), true);
  }

  [PXButton]
  [PXUIField(DisplayName = "Replace")]
  protected void replaceOneValue() => this.replacePreparedDataValues(true);

  [PXButton]
  [PXUIField(DisplayName = "Replace All")]
  protected void replaceAllValues() => this.replacePreparedDataValues(false);

  protected void replacePreparedDataValues(bool replaceFirstOnly)
  {
    int num = 0;
    SYReplace current = this.ReplacementProperties.Current;
    string str1;
    PXResultset<SYData> pxResultset;
    if (Str.IsNullOrEmpty(current.SearchValue))
    {
      str1 = string.Empty;
      pxResultset = PXSelectBase<SYData, PXSelect<SYData, Where<SYData.mappingID, Equal<Current<SYMapping.mappingID>>>>.Config>.Select((PXGraph) this);
    }
    else
    {
      str1 = current.SearchValue;
      pxResultset = PXSelectBase<SYData, PXSelect<SYData, Where<SYData.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYData.fieldValues, Like<Required<SYReplace.searchValue>>>>>.Config>.Select((PXGraph) this, (object) $"%{str1}%");
    }
    foreach (PXResult<SYData> pxResult in pxResultset)
    {
      SYData data = (SYData) pxResult;
      string[] fieldValues = this.GetFieldValues(data.LineNbr, data.FieldValues, this.splittedFieldValues);
      int? columnIndex = current.ColumnIndex;
      if (columnIndex.Value < fieldValues.Length)
      {
        string[] strArray1 = fieldValues;
        columnIndex = current.ColumnIndex;
        int index1 = columnIndex.Value;
        string str2 = strArray1[index1];
        bool? matchCase = current.MatchCase;
        bool flag = true;
        StringComparison stringComparison = matchCase.GetValueOrDefault() == flag & matchCase.HasValue ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        string str3 = str1;
        int comparisonType = (int) stringComparison;
        if (str2.Equals(str3, (StringComparison) comparisonType))
        {
          string[] strArray2 = fieldValues;
          columnIndex = current.ColumnIndex;
          int index2 = columnIndex.Value;
          string replaceValue = current.ReplaceValue;
          strArray2[index2] = replaceValue;
          this.PreparedData.Cache.SetValueExt<SYData.fieldValues>((object) data, (object) SYData.JoinFields((IEnumerable<string>) fieldValues));
          this.PreparedData.Cache.SetStatus((object) data, PXEntryStatus.Updated);
          this.PreparedData.Cache.IsDirty = true;
          ++num;
          if (replaceFirstOnly)
            break;
        }
      }
      else
        break;
    }
    string str4 = PXMessages.LocalizeFormat("Values replaced: {0}", (object) num);
    this.ReplacementProperties.Cache.SetValueExt<SYReplace.replaceResult>((object) current, (object) str4);
    this.ReplacementProperties.Cache.SetStatus((object) current, PXEntryStatus.Updated);
  }

  [PXButton]
  [PXUIField(DisplayName = "Add Substitution")]
  protected void addSubstitutions()
  {
    if (this.PreparedData.Cache.Current == null)
      throw new PXException("The current row is not selected.");
    int num = (int) this.SubstitutionInfo.AskExt((PXView.InitializePanel) ((g, viewname) =>
    {
      PXCache cache = this.SubstitutionInfo.Cache;
      string[] strArray = SYData.SplitErrors(((SYData) this.PreparedData.Cache.Current).FieldExceptions);
      cache.Clear();
      foreach (string str in strArray)
      {
        if (str.StartsWith("PXSubstitutionException", StringComparison.Ordinal))
        {
          SYSubstitutionInfo substitutionInfo = SYSubstitutionInfo.Deserialize(StringExtensions.RestSegment(str, SYData.PARAM_SEPARATOR, (ushort) 1));
          SYSubstitutionMaint instance = PXGraph.CreateInstance<SYSubstitutionMaint>();
          SYSubstitution sySubstitution = instance.SearchSubstitution(substitutionInfo.SubstitutionID);
          if (sySubstitution != null)
          {
            substitutionInfo.TableName = sySubstitution.TableName;
            substitutionInfo.FieldName = sySubstitution.FieldName;
            instance.Substitution.Current = sySubstitution;
            SYSubstitutionValues substitutionValues = instance.SearchSubstitutionValue(substitutionInfo.OriginalValue);
            if (substitutionValues != null)
              substitutionInfo.SubstitutedValue = substitutionValues.SubstitutedValue;
          }
          cache.Insert((object) substitutionInfo);
        }
      }
      cache.IsDirty = false;
    }), true);
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "Save")]
  protected void saveSubstitutions()
  {
    foreach (SYSubstitutionInfo substitutionInfo in this.SubstitutionInfo.Cache.Dirty)
    {
      SYSubstitutionMaint instance = PXGraph.CreateInstance<SYSubstitutionMaint>();
      SYSubstitution sySubstitution1 = instance.SearchSubstitution(substitutionInfo.SubstitutionID);
      if (sySubstitution1 == null)
      {
        SYSubstitution sySubstitution2 = new SYSubstitution()
        {
          SubstitutionID = substitutionInfo.SubstitutionID,
          TableName = substitutionInfo.TableName,
          FieldName = substitutionInfo.FieldName
        };
        sySubstitution1 = (SYSubstitution) instance.Substitution.Cache.Insert((object) sySubstitution2);
      }
      else if (!Str.IsNullOrEmpty(substitutionInfo.TableName) && !Str.IsNullOrEmpty(substitutionInfo.FieldName))
      {
        sySubstitution1.TableName = substitutionInfo.TableName;
        sySubstitution1.FieldName = substitutionInfo.FieldName;
        sySubstitution1 = (SYSubstitution) instance.Substitution.Cache.Update((object) sySubstitution1);
      }
      instance.Substitution.Current = sySubstitution1;
      SYSubstitutionValues substitutionValues1 = instance.SearchSubstitutionValue(substitutionInfo.OriginalValue);
      if (substitutionValues1 == null)
      {
        SYSubstitutionValues substitutionValues2 = new SYSubstitutionValues()
        {
          SubstitutionID = substitutionInfo.SubstitutionID,
          OriginalValue = substitutionInfo.OriginalValue,
          SubstitutedValue = substitutionInfo.SubstitutedValue
        };
        instance.SubstitutionValues.Cache.Insert((object) substitutionValues2);
      }
      else
      {
        substitutionValues1.SubstitutedValue = substitutionInfo.SubstitutedValue;
        instance.SubstitutionValues.Cache.Update((object) substitutionValues1);
      }
      instance.Actions.PressSave();
    }
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "Close")]
  protected void closeSubstitutions() => this.SubstitutionInfo.Cache.IsDirty = false;

  protected virtual void SYData_IsActive_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    SYData row = (SYData) e.Row;
    if (row == null)
      return;
    Guid? mappingId1 = this.Operation.Current.MappingID;
    Guid? mappingId2 = row.MappingID;
    if ((mappingId1.HasValue == mappingId2.HasValue ? (mappingId1.HasValue ? (mappingId1.GetValueOrDefault() == mappingId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || string.IsNullOrEmpty(row.ErrorMessage))
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(false), new bool?(false), length: new int?(4), fieldName: "IsActive", displayName: "Is Active", error: row.ErrorMessage, errorLevel: PXErrorLevel.RowError, enabled: new bool?(true), visible: new bool?(true), readOnly: new bool?(false));
  }

  protected virtual void SYData_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is SYData row) || string.IsNullOrEmpty(row.ErrorMessage))
      return;
    SYProcess.AppendErrorMessage(row.ErrorMessage);
  }

  protected void SYMappingActive_NoteFiles_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    SYMapping row = (SYMapping) e.Row;
    List<string> stringList = new List<string>();
    foreach (PXResult<SYProvider, NoteDoc, UploadFile> pxResult in PXSelectBase<SYProvider, PXSelectJoin<SYProvider, InnerJoin<NoteDoc, On<NoteDoc.noteID, Equal<SYProvider.noteID>>, InnerJoin<UploadFile, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>>, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) row.ProviderID))
    {
      UploadFile uploadFile = (UploadFile) pxResult;
      stringList.Add(uploadFile.Name);
    }
    e.ReturnValue = stringList.Count == 0 ? (object) (string[]) null : (object) stringList.ToArray();
  }

  protected void SYMappingActive_NoteText_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    using (IEnumerator<PXResult<SYProvider>> enumerator = PXSelectBase<SYProvider, PXSelectJoin<SYProvider, InnerJoin<Note, On<Note.noteID, Equal<SYProvider.noteID>>>, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) ((SYMapping) e.Row).ProviderID).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXResult<SYProvider, Note> current = (PXResult<SYProvider, Note>) enumerator.Current;
      e.ReturnValue = (object) ((Note) current).NoteText;
    }
  }

  protected void SYMappingActive_NoteFiles_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    this.NoteUpdating(e.Row as SYMapping);
  }

  protected void SYMappingActive_NoteText_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    this.NoteUpdating(e.Row as SYMapping);
  }

  protected virtual void SYSubstitutionInfo_FieldName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is SYSubstitutionInfo row) || row.TableName == null)
      return;
    List<string> internalNames;
    List<string> displayNames;
    SYSubstitutionMaint.GetTableFields(row.TableName, (PXGraph) this, out internalNames, out displayNames);
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), "fieldName", new bool?(), new int?(), (string) null, internalNames.ToArray(), displayNames.ToArray(), new bool?(true), (string) null);
  }

  protected virtual void SYSubstitutionInfo_TableName_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is SYSubstitutionInfo row) || row.TableName.OrdinalEquals((string) e.NewValue))
      return;
    row.FieldName = (string) null;
  }

  protected virtual void SYSubstitutionInfo_SubstitutedValue_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    SYSubstitutionInfo current = this.SubstitutionInfo.Current;
    if (current == null || current.TableName == null || current.FieldName == null || !(this.Caches[PXBuildManager.GetType(current.TableName, true)].GetStateExt((object) null, current.FieldName) is PXFieldState stateExt))
      return;
    stateExt.Value = e.ReturnValue;
    e.ReturnState = (object) stateExt;
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
    if (!this._Flag)
    {
      this._Flag = true;
      this.PreparedData.Cache.AllowUpdate = true;
      this.ViewHistory.SetEnabled(true);
      if (PXLongOperation.GetStatus(this.UID) == PXLongRunStatus.InProcess)
      {
        this.PreparedData.Cache.AllowUpdate = false;
        this.ViewHistory.SetEnabled(false);
        PXUIFieldAttribute.SetEnabled<SYImportOperation.operation>(this.Operation.Cache, (object) this.Operation.Current, false);
        PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnError>(this.Operation.Cache, (object) this.Operation.Current, false);
        PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnTarget>(this.Operation.Cache, (object) this.Operation.Current, false);
        PXUIFieldAttribute.SetEnabled<SYImportOperation.processInParallel>(this.Operation.Cache, (object) this.Operation.Current, false);
        PXUIFieldAttribute.SetEnabled<SYImportOperation.validate>(this.Operation.Cache, (object) this.Operation.Current, false);
        PXUIFieldAttribute.SetEnabled<SYImportOperation.validateAndSave>(this.Operation.Cache, (object) this.Operation.Current, false);
      }
    }
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  protected virtual void SYMappingActive_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    SYMapping row1 = (SYMapping) e.Row;
    if (row1.NoteID.HasValue)
    {
      Guid? noteId = row1.NoteID;
      Guid empty = Guid.Empty;
      if ((noteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) == 0)
        return;
    }
    Guid? nullable = new Guid?();
    PXCache cach1 = this.Caches[typeof (Note)];
    foreach (Note row2 in cach1.Inserted)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        cach1.PersistInserted((object) row2);
        nullable = row2.NoteID;
        transactionScope.Complete();
      }
    }
    row1.NoteID = new Guid?();
    SYProvider syProvider = (SYProvider) PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) row1.ProviderID);
    PXCache cach2 = this.Caches[typeof (SYProvider)];
    syProvider.NoteID = nullable;
    cach2.Update((object) syProvider);
    cach2.Persist(PXDBOperation.Update);
    cach2.IsDirty = false;
    PXCache cach3 = this.Caches[typeof (NoteDoc)];
    foreach (NoteDoc noteDoc in cach3.Inserted)
      noteDoc.NoteID = nullable;
    cach3.Persist(PXDBOperation.Insert);
    cach3.IsDirty = false;
  }

  protected virtual void SYMappingActive_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SYMapping row) || !row.ProviderID.HasValue)
      return;
    this.NoteSelecting(cache, row);
    SYProcess.OverrideAccessRights(row);
  }

  protected internal void ClearGenerateDynamicColumns()
  {
    foreach (string dynamicColumn in (IEnumerable<string>) this._dynamicColumns)
    {
      this.PreparedData.Cache.Fields.Remove(dynamicColumn);
      this.FieldSelecting.RemoveHandler("PreparedData", dynamicColumn, this._dynamicColumnsFieldSelectingHandlers[dynamicColumn]);
      this.FieldUpdating.RemoveHandler("PreparedData", dynamicColumn, this._dynamicColumnsFieldUpdating[dynamicColumn]);
    }
    this._dynamicColumnsFieldSelectingHandlers.Clear();
    this._dynamicColumnsFieldUpdating.Clear();
    this._dynamicColumns.Clear();
    foreach (PXRowUpdated dynamicColumnsEvent in (IEnumerable<PXRowUpdated>) this._dynamicColumnsEvents)
      this.RowUpdated.RemoveHandler("PreparedData", dynamicColumnsEvent);
    this._dynamicColumnsEvents.Clear();
  }

  protected internal void GenerateDynamicColumns(List<string> displayNames)
  {
    if (this.Operation.Current == null)
      return;
    for (int index = 0; index < displayNames.Count; ++index)
    {
      int fieldIndex = index;
      string fieldName = "Value_" + fieldIndex.ToString();
      string displayName = displayNames[index];
      if (!this.PreparedData.Cache.Fields.Contains(fieldName))
      {
        this.PreparedData.Cache.Fields.Add(fieldName);
        this._dynamicColumns.Add(fieldName);
        this.FieldSelecting.AddHandler("PreparedData", fieldName, new PXFieldSelecting(PreparedDataFieldSelectingHandler));
        this._dynamicColumnsFieldSelectingHandlers.Add(fieldName, new PXFieldSelecting(PreparedDataFieldSelectingHandler));
      }
      if (!this._dynamicColumnsFieldUpdating.ContainsKey(fieldName))
      {
        this.FieldUpdating.AddHandler("PreparedData", fieldName, new PXFieldUpdating(PreparedDataFieldUpdating));
        this._dynamicColumnsFieldUpdating.Add(fieldName, new PXFieldUpdating(PreparedDataFieldUpdating));
      }

      void PreparedDataFieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs selE)
      {
        PXFieldSelectingEventArgs selectingEventArgs = selE;
        object returnState = selE.ReturnState;
        System.Type dataType = typeof (string);
        bool? isKey = new bool?();
        bool? nullable = new bool?();
        int? required = new int?();
        int? precision = new int?();
        int? length = new int?();
        string fieldName = fieldName;
        string displayName = displayName;
        bool? enabled = new bool?();
        bool? visible = new bool?();
        bool? readOnly = new bool?();
        PXFieldState instance;
        PXFieldState pxFieldState = instance = PXFieldState.CreateInstance(returnState, dataType, isKey, nullable, required, precision, length, fieldName: fieldName, displayName: displayName, enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Dynamic);
        selectingEventArgs.ReturnState = (object) instance;
        SYData row = (SYData) selE.Row;
        if (row == null || !row.LineNbr.HasValue)
          return;
        SyImportRowResult.FieldError fieldError;
        if (this.GetFieldErrors(row.LineNbr, row.FieldErrors, this.splittedFieldErrors).TryGetValue(displayName, out fieldError))
        {
          pxFieldState.Error = fieldError.ErrorText;
          if (!string.IsNullOrEmpty(fieldError.ErrorText))
            pxFieldState.ErrorLevel = fieldError.IsError ? PXErrorLevel.Error : PXErrorLevel.Warning;
        }
        string[] fieldValues = this.GetFieldValues(row.LineNbr, row.FieldValues, this.splittedFieldValues);
        if (fieldIndex >= fieldValues.Length)
          return;
        selE.ReturnValue = (object) fieldValues[fieldIndex];
      }

      void PreparedDataFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs updE)
      {
        this.dynamicFieldValues[fieldName] = updE.NewValue as string;
      }
    }
    // ISSUE: method pointer
    this.RowUpdated.AddHandler("PreparedData", new PXRowUpdated((object) this, __methodptr(\u003CGenerateDynamicColumns\u003Eg__PreparedDataRowUpdated\u007C61_0)));
    // ISSUE: method pointer
    this._dynamicColumnsEvents.Add(new PXRowUpdated((object) this, __methodptr(\u003CGenerateDynamicColumns\u003Eg__PreparedDataRowUpdated\u007C61_0)));
  }

  internal List<string> GetDisplayNames()
  {
    List<string> displayNames = new List<string>();
    foreach (PXResult<SYMapping, SYProviderField> pxResult in PXSelectBase<SYMapping, PXSelectJoin<SYMapping, InnerJoin<SYProviderField, On<SYMapping.providerID, Equal<SYProviderField.providerID>, And<SYMapping.providerObject, Equal<SYProviderField.objectName>>>>, Where<SYMapping.mappingID, Equal<Current<SYImportOperation.mappingID>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>, OrderBy<Asc<SYProviderField.lineNbr>>>.Config>.Select((PXGraph) this))
      displayNames.Add(PXLocalizer.Localize(((SYProviderField) pxResult).DisplayName ?? ((SYProviderField) pxResult).Name, typeof (SYProviderField).FullName));
    return displayNames;
  }

  internal Dictionary<string, SyImportRowResult.FieldError> GetFieldErrors(
    int? lineNumber,
    string lineErrors,
    Dictionary<int?, Dictionary<string, SyImportRowResult.FieldError>> cachedErrors)
  {
    Dictionary<string, SyImportRowResult.FieldError> fieldErrors;
    if (cachedErrors.ContainsKey(lineNumber))
    {
      fieldErrors = cachedErrors[lineNumber];
    }
    else
    {
      fieldErrors = ((IEnumerable<string>) SYData.SplitErrors(lineErrors)).Select<string, SyImportRowResult.FieldError>((Func<string, SyImportRowResult.FieldError>) (str => SyImportRowResult.FieldError.Deserialize(str))).ToDictionary<SyImportRowResult.FieldError, string>((Func<SyImportRowResult.FieldError, string>) (error => error.FieldName));
      cachedErrors.Add(lineNumber, fieldErrors);
    }
    return fieldErrors;
  }

  protected string[] GetFieldValues(
    int? lineNumber,
    string lineValues,
    Dictionary<int?, string[]> cachedValues)
  {
    string[] fieldValues;
    if (cachedValues.ContainsKey(lineNumber))
    {
      fieldValues = cachedValues[lineNumber];
    }
    else
    {
      fieldValues = SYData.SplitFields(lineValues);
      cachedValues.Add(lineNumber, fieldValues);
    }
    return fieldValues;
  }

  private void NoteUpdating(SYMapping row)
  {
    if (row == null || row.NoteID.HasValue)
      return;
    PXResult<SYProvider, Note> pxResult = (PXResult<SYProvider, Note>) (PXResult<SYProvider>) PXSelectBase<SYProvider, PXSelectJoin<SYProvider, InnerJoin<Note, On<Note.noteID, Equal<SYProvider.noteID>>>, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) row.ProviderID);
    if (pxResult != null)
    {
      row.NoteID = ((Note) pxResult).NoteID;
    }
    else
    {
      if (!(this.Caches[typeof (Note)].Insert() is Note note))
        return;
      note.NoteText = string.Empty;
      note.EntityType = typeof (SYProvider).FullName;
      note.GraphType = typeof (SYProviderMaint).FullName;
      row.NoteID = note.NoteID;
    }
  }

  private void NoteSelecting(PXCache cache, SYMapping row)
  {
    SYProvider data = (SYProvider) PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.ProviderID);
    if (data == null)
    {
      cache.RaiseExceptionHandling<SYMapping.providerID>((object) row, (object) row.ProviderID, (Exception) new PXSetPropertyException("The provider does not exist in the system."));
    }
    else
    {
      if (!data.NoteID.HasValue)
        PXNoteAttribute.GetNoteID(this.Caches[typeof (SYProvider)], (object) data, (string) null);
      row.NoteID = data.NoteID;
    }
  }

  internal abstract PXSYTable QueryPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    CancellationToken token);

  internal abstract int ImportPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData,
    CancellationToken token);

  protected virtual void SYImportOperation_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }

  internal virtual void RaiseOnImportCompleted(SYMapping mapping, PXSYTableEx data)
  {
  }

  protected internal static void ProcessMapping(
    SYProcess graph,
    SYMappingActive mapping,
    SYImportOperation operation,
    CancellationToken token,
    bool needOverrideAccessRights = false)
  {
    if (needOverrideAccessRights)
      SYProcess.OverrideAccessRights((SYMapping) mapping);
    SyMappingUtils.ProcessMapping(graph, mapping, operation, token);
  }

  private static void OverrideAccessRights(SYMapping mapping)
  {
    Note note = (Note) PXSelectBase<Note, PXSelectJoin<Note, InnerJoin<SYProvider, On<Note.noteID, Equal<SYProvider.noteID>>, InnerJoin<SYMapping, On<SYProvider.providerID, Equal<SYMapping.providerID>>>>, Where<SYMapping.providerID, Equal<Required<SYMapping.providerID>>>>.Config>.SelectSingleBound(new PXGraph(), (object[]) null, (object) mapping.ProviderID);
    if (note == null)
      return;
    PXContext.SetSlot<string>($"{typeof (Note).FullName}+{note.NoteID}", note.NoteID.ToString());
  }

  private static void AppendErrorMessage(string errorMessage)
  {
    if (!PXContext.GetSlot<bool>("_ProcessScheduled"))
      return;
    string slot = PXContext.GetSlot<string>("_ProcessScheduledMessage");
    PXContext.SetSlot<string>("_ProcessScheduledMessage", string.IsNullOrEmpty(slot) ? errorMessage : $"{slot} {errorMessage}");
  }

  private int[] getIndexesArray(int length)
  {
    int[] indexesArray = new int[length];
    for (int index = 0; index < length; ++index)
      indexesArray[index] = index;
    return indexesArray;
  }
}
