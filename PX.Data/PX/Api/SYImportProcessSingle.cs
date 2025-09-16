// Decompiled with JetBrains decompiler
// Type: PX.Api.SYImportProcessSingle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.ImportSimple;
using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.Api.Export.MappingFieldNameTree;
using PX.Data.Process.Automation;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Api;

[PXDisableWorkflow]
public class SYImportProcessSingle : 
  SYImportProcess,
  IFormulaEditorFields,
  IFormulaEditorInternalFields,
  IFormulaEditorExternalFields
{
  private (string ScreenId, MappingFieldTreeNodeFactory Factory, MappingFieldNodeKeyParser Parser) _treeNodeInfo = ((string) null, (MappingFieldTreeNodeFactory) null, (MappingFieldNodeKeyParser) null);
  public PXSave<SYMappingActive> Save;
  public PXInsert<SYMappingActive> Insert;
  public PXCancel<SYMappingActive> Cancel;
  public PXFirst<SYMappingActive> First;
  public PXPrevious<SYMappingActive> Prev;
  public PXNext<SYMappingActive> Next;
  public PXLast<SYMappingActive> Last;
  public PXAction<SYMappingActive> Prepare;
  public PXAction<SYMappingActive> Import;
  public PXAction<SYMappingActive> PrepareImport;
  public PXAction<SYMappingActive> Rollback;
  public PXAction<SYMappingActive> SwitchActivation;
  public PXAction<SYMappingActive> SwitchActivationUntilError;
  public PXAction<SYMappingActive> SwitchProcessing;
  public PXAction<SYMappingActive> ShowUploadPanel;
  public PXAction<SYMappingActive> GetLatestFile;
  public PXAction<SYMappingActive> ClearErrors;
  public PXAction<SYMappingActive> RefreshFromFile;
  public PXAction<SYMappingActive> Replace;
  public PXAction<SYMappingActive> AddSubstitution;
  public PXFilter<SYUploadPanel> UploadPanel;
  public PXFilter<SYMappingActiveFilter> CreateFromFilePanel;
  public PXSelect<SYMappingActive, Where<SYMappingActive.mappingType, Equal<SYMapping.mappingType.typeImport>, And<SYMapping.isActive, Equal<PX.Data.True>, And<SYMappingActive.providerType, NotEqual<BPEventProviderType>>>>> MappingsSingle;
  public PXSelect<SYMappingActive, Where<SYMappingActive.name, Equal<Current<SYMappingActive.name>>>> MappingsSingleDetails;
  public PXFilter<SYMappingSimpleProperty> NewScenarioProperties;
  public PXSelectOrderBy<MappingFieldTreeNode, OrderBy<Asc<MappingFieldTreeNode.orderNumber>>> MappingFieldTree;
  private ProcessSingleActivator activator;
  private PXScreenToSiteMapViewHelper screenToSiteMapViewHelper;
  public PXAction<SYMappingActive> ConvertSimpleMappingToManual;
  public PXAction<SYMappingActive> ViewScreen;

  protected override bool HideSave => false;

  protected override bool RestrictPreparedDataUpdate => false;

  public SYImportProcessSingle()
  {
    if (this.activator != null)
      this.activator.EnableFields();
    this.screenToSiteMapViewHelper = new PXScreenToSiteMapViewHelper("IB", this.Caches[typeof (SYMappingActive)], new PXAction[4]
    {
      (PXAction) this.First,
      (PXAction) this.Prev,
      (PXAction) this.Next,
      (PXAction) this.Last
    }, new System.Type[2]
    {
      typeof (SYMappingActive.name),
      typeof (SYMapping.sitemapTitle)
    });
  }

  protected IEnumerable mappingsSingle()
  {
    SYImportProcessSingle graph = this;
    if (graph.NewScenarioProperties.Current != null && graph.NewScenarioProperties.Current.Mapping != null && graph.NewScenarioProperties.Current.Mapping != graph.MappingsSingle.Current)
    {
      yield return graph.MappingsSingle.Cache.Extend<SYMapping>((SYMapping) graph.NewScenarioProperties.Current.Mapping);
      graph.MappingsSingle.Cache.Remove(graph.MappingsSingle.Cache.Current);
    }
    foreach (object mapping in PXSYMappingSelector.GetMappings<SYMappingActive>(new PXView((PXGraph) graph, false, graph.MappingsSingle.View.BqlSelect)))
      yield return mapping;
  }

  protected IEnumerable mappingFieldTree(string parentNodeKey)
  {
    MappingFieldTreeNodeFactory nodeFactory = this.GetFieldNodeFactory();
    MappingFieldNodeKeyParser nodeKeyParser = this.GetFieldNodeKeyParser();
    List<SYImportSimple.NameDisplayNameModel> objectNameList = this.NewScenarioProperties.Current?.ObjectNameList;
    Dictionary<string, SYImportSimple.NameDisplayNameModel[]> objectFieldDictionary = this.NewScenarioProperties.Current?.ObjectFieldDictionary;
    if (nodeFactory != null && nodeKeyParser != null && objectNameList != null && objectFieldDictionary != null)
    {
      IEnumerable<MappingFieldTreeNode> childNodes = nodeFactory.CreateChildNodes(parentNodeKey);
      List<SYImportSimple.NameDisplayNameModel> usedObjectNames = new List<SYImportSimple.NameDisplayNameModel>();
      List<SYImportSimple.NameDisplayNameModel> usedFields = new List<SYImportSimple.NameDisplayNameModel>();
      string lastViewName = string.Empty;
      int lastOrderNumber = 0;
      foreach (MappingFieldTreeNode mappingFieldTreeNode in childNodes)
      {
        string v;
        if (MappingFieldNodeKeyParser.IsViewNodeKey(mappingFieldTreeNode.Key, out v))
        {
          SYImportSimple.NameDisplayNameModel displayNameModel = objectNameList.FirstOrDefault<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (o => v.Equals(o.Name, StringComparison.Ordinal)));
          if (displayNameModel != null)
          {
            usedObjectNames.Add(displayNameModel);
            lastOrderNumber = mappingFieldTreeNode.OrderNumber.Value;
            yield return (object) mappingFieldTreeNode;
          }
        }
        else
        {
          (string str, string CommandName) = nodeKeyParser.ParseNodeKey(mappingFieldTreeNode.Key, false);
          SYImportSimple.NameDisplayNameModel[] source;
          if (objectFieldDictionary.TryGetValue(str, out source))
          {
            SYImportSimple.NameDisplayNameModel displayNameModel = ((IEnumerable<SYImportSimple.NameDisplayNameModel>) source).FirstOrDefault<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (f => CommandName.Equals(f.Name, StringComparison.Ordinal)));
            if (displayNameModel != null)
            {
              usedFields.Add(displayNameModel);
              lastOrderNumber = mappingFieldTreeNode.OrderNumber.Value;
              yield return (object) mappingFieldTreeNode;
            }
            lastViewName = str;
            str = (string) null;
          }
        }
      }
      int nextOrderNumber = lastOrderNumber + 1;
      if (usedObjectNames.Count > 0)
      {
        foreach (SYImportSimple.NameDisplayNameModel displayNameModel in objectNameList.Except<SYImportSimple.NameDisplayNameModel>((IEnumerable<SYImportSimple.NameDisplayNameModel>) usedObjectNames).Where<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (o => !string.IsNullOrEmpty(o.Name))))
          yield return (object) nodeFactory.CreateViewNode(displayNameModel.Name, displayNameModel.DisplayName, ref nextOrderNumber);
      }
      else if (usedFields.Count > 0)
      {
        foreach (SYImportSimple.NameDisplayNameModel displayNameModel in ((IEnumerable<SYImportSimple.NameDisplayNameModel>) objectFieldDictionary[lastViewName]).Except<SYImportSimple.NameDisplayNameModel>((IEnumerable<SYImportSimple.NameDisplayNameModel>) usedFields).Where<SYImportSimple.NameDisplayNameModel>((Func<SYImportSimple.NameDisplayNameModel, bool>) (f => !string.IsNullOrEmpty(f.Name))))
          yield return (object) nodeFactory.CreateFieldNode(lastViewName, displayNameModel.Name, displayNameModel.DisplayName, ref nextOrderNumber);
      }
    }
  }

  [PXButton(Category = "Other")]
  [PXUIField(DisplayName = "Convert To Manual Scenario")]
  protected IEnumerable convertSimpleMappingToManual(PXAdapter adapter)
  {
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => SYImportProcessSingle.PerformConvertionFromSimpleToManualMapping((SYMapping) this.MappingsSingle.Current)));
    return adapter.Get();
  }

  [PXButton(Category = "Other")]
  [PXUIField(DisplayName = "View Screen", Enabled = false)]
  protected IEnumerable viewScreen(PXAdapter adapter)
  {
    ImportGraphNavigator.NavigateToGraph(this.UID);
    return adapter.Get();
  }

  public static void PerformConvertionFromSimpleToManualMapping(SYMapping mapping)
  {
    if (mapping != null)
      mapping.IsSimpleMapping = new bool?(false);
    SYImportMaint instance = PXGraph.CreateInstance<SYImportMaint>();
    instance.Mappings.Current = mapping;
    instance.Mappings.Update(mapping);
    instance.Mappings.Cache.PersistUpdated((object) mapping);
  }

  protected virtual void SYMappingActive_ProviderID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void SYMappingActive_ProviderObject_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected override bool GenerateDynamicInConstructor
  {
    get
    {
      if (this.activator == null)
        this.activator = new ProcessSingleActivator((SYProcess) this, this.PrepareImport, this.Prepare, this.Rollback, this.Import, this.ShowUploadPanel, this.GetLatestFile);
      return this.activator.GenerateDynamicColumns;
    }
  }

  private bool IsNewUI()
  {
    return PXSiteMap.Provider.FindSiteMapNodeByGraphType(typeof (SYImportProcessSingle).FullName)?.SelectedUI == "T";
  }

  private void InitTreeInfo()
  {
    string screenId = this.NewScenarioProperties.Current?.ScreenID;
    if (string.IsNullOrEmpty(screenId))
    {
      this._treeNodeInfo = ((string) null, (MappingFieldTreeNodeFactory) null, (MappingFieldNodeKeyParser) null);
    }
    else
    {
      if (this._treeNodeInfo.ScreenId == screenId && this._treeNodeInfo.Factory != null && this._treeNodeInfo.Parser != null)
        return;
      PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(screenId);
      if (screenInfo == null)
      {
        this._treeNodeInfo = ((string) null, (MappingFieldTreeNodeFactory) null, (MappingFieldNodeKeyParser) null);
      }
      else
      {
        PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId);
        if (mapNodeByScreenId == null || mapNodeByScreenId.GraphType == null)
        {
          this._treeNodeInfo = ((string) null, (MappingFieldTreeNodeFactory) null, (MappingFieldNodeKeyParser) null);
        }
        else
        {
          PXGraph instance = PXGraph.CreateInstance(GraphHelper.GetType(mapNodeByScreenId.GraphType));
          this._treeNodeInfo.ScreenId = screenId;
          this._treeNodeInfo.Factory = new MappingFieldTreeNodeFactory(screenInfo, instance, screenId, viewsAndFieldsMode: true);
          this._treeNodeInfo.Parser = new MappingFieldNodeKeyParser((IEnumerable<string>) instance.Views.Keys);
        }
      }
    }
  }

  private MappingFieldTreeNodeFactory GetFieldNodeFactory()
  {
    this.InitTreeInfo();
    return this._treeNodeInfo.Factory;
  }

  private MappingFieldNodeKeyParser GetFieldNodeKeyParser()
  {
    this.InitTreeInfo();
    return this._treeNodeInfo.Parser;
  }

  [PXButton]
  [PXUIField(DisplayName = "Refresh from File")]
  public IEnumerable refreshFromFile(PXAdapter adapter)
  {
    SYMappingSimpleProperty current = this.NewScenarioProperties.Current;
    if (current != null)
    {
      current.RefreshExistingMapping = new bool?(true);
      current.CreationProcessIsOn = new bool?(false);
      current.MappingAlreadyLoaded = new bool?(false);
      if (this.CreateFromFilePanel.AskExt() == WebDialogResult.OK)
      {
        SYImportSimple.LoadFileFromSession(current);
        if (current.File != null)
          SYImportSimple.CreateMappingInMemory(current, (PXSelectBase<SYMappingFieldSimple>) this.MappingsSimple, this.MappingsSingle.Current);
        else
          current.RefreshExistingMapping = new bool?(false);
      }
      else
        current.RefreshExistingMapping = new bool?(false);
    }
    return adapter.Get();
  }

  [PXInsertButton]
  [PXUIField]
  public IEnumerable insert(PXAdapter adapter)
  {
    SYMappingSimpleProperty current = this.NewScenarioProperties.Current;
    if (current != null)
    {
      current.CreationProcessIsOn = new bool?(true);
      current.RefreshExistingMapping = new bool?(false);
      if (this.CreateFromFilePanel.AskExt() == WebDialogResult.OK)
      {
        SYImportSimple.LoadFileFromSession(current);
        bool? refreshImportSimple = current.RefreshImportSimple;
        if (refreshImportSimple.HasValue)
        {
          refreshImportSimple = current.RefreshImportSimple;
          if (refreshImportSimple.Value)
          {
            current.RefreshImportSimple = new bool?(false);
            ImportSimpleDefaulter.FillDefaultsToScenarioProperties(current);
          }
        }
        if (current.File != null && this.NewScenarioProperties.AskExt() == WebDialogResult.OK)
        {
          current.RefreshImportSimple = new bool?(true);
          PXException exception;
          if (!SYImportSimple.ScenarioPropertiesAreValid(current, out exception))
            throw exception;
          SYImportSimple.CreateMappingInMemory(current, (PXSelectBase<SYMappingFieldSimple>) this.MappingsSimple, (SYMappingActive) null);
          if (adapter.SortColumns.Length != 0 && string.Equals(adapter.SortColumns[0], "Name", StringComparison.OrdinalIgnoreCase) && adapter.Searches.Length != 0)
            adapter.Searches[0] = (object) current.Name;
        }
        else
          current.CreationProcessIsOn = new bool?(false);
      }
      else
        current.CreationProcessIsOn = new bool?(false);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Prepare & Import", Category = "Processing")]
  [PXUIField(DisplayName = "Prepare & Import", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable prepareImport(PXAdapter adapter)
  {
    this.PerformOperation("C");
    return adapter.Get();
  }

  [PXButton(Tooltip = "Prepare", Category = "Processing", IsLockedOnToolbar = true, DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Prepare", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable prepare(PXAdapter adapter)
  {
    this.PerformOperation("P");
    return adapter.Get();
  }

  [PXButton(Tooltip = "On the Prepared Data tab, delete all records, and on the History tab, delete all records up to the currently selected one on the History tab.", Category = "Processing")]
  [PXUIField(DisplayName = "Clear Data", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable rollback(PXAdapter adapter)
  {
    this.activator.PerformRollback();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Import", Category = "Processing", IsLockedOnToolbar = true, DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Import", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  public IEnumerable import(PXAdapter adapter)
  {
    this.PerformOperation("I");
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Replace")]
  public IEnumerable replace(PXAdapter adapter)
  {
    this.viewReplacement();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Add Substitution")]
  public IEnumerable addSubstitution(PXAdapter adapter)
  {
    this.addSubstitutions();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Change the activation status for all rows.")]
  [PXUIField(DisplayName = "Toggle Activation")]
  public IEnumerable switchActivation(PXAdapter adapter)
  {
    this.activator.SwitchActivation();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Clear the activation status for all rows before the row with an error.")]
  [PXUIField(DisplayName = "Clear Activation Until Error")]
  public IEnumerable switchActivationUntilError(PXAdapter adapter)
  {
    this.activator.SwitchActivationUntilError();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Change processing status for all rows.")]
  [PXUIField(DisplayName = "Toggle Processing")]
  public IEnumerable switchProcessing(PXAdapter adapter)
  {
    this.activator.SwitchProcessing();
    return adapter.Get();
  }

  [PXButton(Tooltip = "Upload a new version of the file attached to the provider.", Category = "Data")]
  [PXUIField(DisplayName = "Upload File Version")]
  protected void showUploadPanel()
  {
    if (this.UploadPanel.AskExt() != WebDialogResult.OK)
      return;
    FileInfo file = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["sessionKey_fileImportProcess"];
    if (file == null)
      return;
    this.SaveNewFileVersion(file);
  }

  public void SaveNewFileVersion(FileInfo file) => this.activator.SaveNewFileVersion(file);

  [PXButton(Tooltip = "Get the latest version of the file that is used by the provider for this mapping.", Category = "Data")]
  [PXUIField(DisplayName = "Get File", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected void getLatestFile() => this.activator.getLatestFile();

  [PXButton(Tooltip = "Delete error messages for all rows in the prepared data.")]
  [PXUIField(DisplayName = "Clear Errors", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected void clearErrors() => this.activator.ClearErrors();

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXSYMappingActiveSelector))]
  [PXSYMappingActiveWithoutBPMSelector]
  protected void SYMappingActive_Name_CacheAttached(PXCache sender)
  {
  }

  internal override PXSYTable QueryPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    CancellationToken token)
  {
    throw new NotImplementedException();
  }

  internal override int ImportPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData,
    CancellationToken token)
  {
    throw new NotImplementedException();
  }

  protected virtual void SYMappingFieldSimple_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SYMappingFieldSimple row = e.Row as SYMappingFieldSimple;
    SYMappingFieldSimple oldRow = e.OldRow as SYMappingFieldSimple;
    if (row == null || string.IsNullOrEmpty(row.FieldName) || oldRow == null || !string.IsNullOrEmpty(oldRow.FieldName))
      return;
    sender.SetValue<SYMappingField.isActive>((object) row, (object) true);
  }

  protected virtual void _(
    Events.FieldUpdating<SYMappingFieldSimple.fieldName> e)
  {
    if (!(e.Row is SYMappingFieldSimple row) || !(e.NewValue is string newValue) || !this.IsNewUI())
      return;
    MappingFieldNodeKeyParser fieldNodeKeyParser = this.GetFieldNodeKeyParser();
    (string str1, string str2) = fieldNodeKeyParser != null ? fieldNodeKeyParser.ParseNodeKey(newValue, false) : ((string) null, (string) null);
    if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
      return;
    row.ObjectName = str1;
    e.NewValue = (object) str2;
  }

  protected virtual void SYMappingFieldSimple_FieldName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SYMappingFieldSimple row) || string.IsNullOrEmpty(row.FieldName) || !SYImportSimple.IsKeyField(this.NewScenarioProperties.Current, sender, row))
      return;
    row.IsKey = new bool?(true);
  }

  protected virtual void SYMappingFieldSimple_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SYMappingFieldSimple row))
      return;
    SYImportSimple.PopulateValueFieldList(this.NewScenarioProperties.Current, sender, row);
    SYImportSimple.PopulateObjectNameList(this.NewScenarioProperties.Current, sender, row);
    SYImportSimple.SetMappingKeyEnabling(this.NewScenarioProperties.Current, sender, row);
    MappingFieldTreeNodeFactory fieldNodeFactory = this.GetFieldNodeFactory();
    MappingFieldNodeKeyParser fieldNodeKeyParser = this.GetFieldNodeKeyParser();
    SYImportSimple.PopulateFieldNameList(this.NewScenarioProperties.Current, sender, row, this.IsNewUI(), fieldNodeFactory, fieldNodeKeyParser);
  }

  protected virtual void SYMappingFieldSimple_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is SYMappingFieldSimple row) || this.MappingsSingle.Current == null || row.MappingID.HasValue)
      return;
    row.MappingID = this.MappingsSingle.Current.MappingID;
  }

  protected virtual void SYMappingFieldSimple_Value_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SYMappingFieldSimple row))
      return;
    SYImportSimple.PopulateObjectNameList(this.NewScenarioProperties.Current, sender, row);
  }

  protected virtual void SYMappingFieldSimple_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void SYMappingFieldSimple_ObjectName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SYMappingFieldSimple row))
      return;
    row.FieldName = (string) null;
  }

  protected virtual void SYMappingSimpleProperty_ScreenID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SYMappingSimpleProperty row) || string.IsNullOrEmpty(row.ScreenID))
      return;
    if (SYImportSimple.ScreenIDIsValid(row.ScreenID))
      row.Name = ImportSimpleDefaulter.GetDefaultScenarioName(row.ScreenID);
    else
      PXUIFieldAttribute.SetWarning<SYMappingSimpleProperty.screenID>(this.NewScenarioProperties.Cache, (object) row, "An import scenario for this form cannot be created. The form doesn't have the Copy and Paste actions.");
  }

  protected virtual void _(Events.FieldUpdated<SYMapping.processInParallel> e)
  {
    if (!(e.Row is SYMappingActive row))
      return;
    bool valueOrDefault = row.ProcessInParallel.GetValueOrDefault();
    if (valueOrDefault)
    {
      e.Cache.SetValue<SYMapping.breakOnError>((object) row, (object) false);
      e.Cache.SetValue<SYMapping.breakOnTarget>((object) row, (object) false);
    }
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnError>(e.Cache, e.Row, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<SYMapping.breakOnTarget>(e.Cache, e.Row, !valueOrDefault);
  }

  protected virtual void _(Events.RowUpdated<SYMappingActive> e)
  {
    if (e.OldRow == null)
      return;
    bool? isSimpleMapping = e.Row.IsSimpleMapping;
    bool flag = true;
    if (isSimpleMapping.GetValueOrDefault() == flag & isSimpleMapping.HasValue)
      return;
    e.Cache.SetStatus((object) e.Row, PXEntryStatus.Notchanged);
    e.Cache.IsDirty = false;
    int num = e.Cache.Graph.IsDirty ? 1 : 0;
  }

  protected override void SYMappingActive_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SYMapping row1))
      return;
    if (row1.TStamp == null)
    {
      PXUIFieldAttribute.SetEnabled<SYMapping.processInParallel>(cache, (object) row1, false);
    }
    else
    {
      SYMappingActive row2 = e.Row as SYMappingActive;
      SYMappingSimpleProperty current = this.NewScenarioProperties.Current;
      bool? nullable;
      if (row2 != null && current != null)
      {
        Guid? mappingId1 = current.MappingID;
        Guid? mappingId2 = row2.MappingID;
        bool flag1 = mappingId1.HasValue != mappingId2.HasValue || mappingId1.HasValue && mappingId1.GetValueOrDefault() != mappingId2.GetValueOrDefault();
        nullable = current.CreationProcessIsOn;
        bool flag2 = true;
        int num1;
        if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
        {
          nullable = current.RefreshExistingMapping;
          bool flag3 = true;
          if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
          {
            nullable = current.MappingAlreadyLoaded;
            bool flag4 = true;
            num1 = !(nullable.GetValueOrDefault() == flag4 & nullable.HasValue) ? 1 : 0;
            goto label_8;
          }
        }
        num1 = 0;
label_8:
        int num2 = flag1 ? 1 : 0;
        if ((num1 | num2) != 0)
        {
          nullable = row2.IsSimpleMapping;
          bool flag5 = true;
          if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
          {
            current.MappingAlreadyLoaded = new bool?(true);
            SYImportSimple.SetMappingSimpleProperty(current, row2);
            SYImportSimple.LoadSavedMapping(current, this.MappingsSimple.Cache);
          }
          else if (!flag1)
            SYImportSimple.ClearMappingSimpleInfo(current, this.MappingsSimple.Cache);
        }
        PXAction<SYMappingActive> simpleMappingToManual1 = this.ConvertSimpleMappingToManual;
        nullable = row2.IsSimpleMapping;
        bool flag6 = true;
        int num3 = nullable.GetValueOrDefault() == flag6 & nullable.HasValue ? 1 : 0;
        simpleMappingToManual1.SetVisible(num3 != 0);
        PXAction<SYMappingActive> simpleMappingToManual2 = this.ConvertSimpleMappingToManual;
        nullable = row2.IsSimpleMapping;
        bool flag7 = true;
        int num4 = nullable.GetValueOrDefault() == flag7 & nullable.HasValue ? 1 : 0;
        simpleMappingToManual2.SetEnabled(num4 != 0);
      }
      PXGraph pxGraph = ImportGraphNavigator.LoadGraph(this.UID);
      nullable = this.Operation.Current.ProcessInParallel;
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) && pxGraph != null && pxGraph.GetType().FullName.Equals(row2.GraphName))
      {
        this.ViewScreen.SetEnabled(true);
      }
      else
      {
        this.ViewScreen.SetEnabled(false);
        ImportGraphNavigator.ClearImportInfo();
      }
      base.SYMappingActive_RowSelected(cache, e);
      if (HttpContext.Current != null)
      {
        this.activator.SYMapping_RowSelected(cache, e.Row as SYMapping);
        PXUIFieldAttribute.SetEnabled<SYMapping.processInParallel>(cache, (object) row2, false);
        PXCache cache1 = cache;
        SYMappingActive data1 = row2;
        nullable = row2.ProcessInParallel;
        int num5 = !nullable.GetValueOrDefault() ? 1 : 0;
        PXUIFieldAttribute.SetEnabled<SYMapping.breakOnError>(cache1, (object) data1, num5 != 0);
        PXCache cache2 = cache;
        SYMappingActive data2 = row2;
        nullable = row2.ProcessInParallel;
        int num6 = !nullable.GetValueOrDefault() ? 1 : 0;
        PXUIFieldAttribute.SetEnabled<SYMapping.breakOnTarget>(cache2, (object) data2, num6 != 0);
      }
      this.Save?.SetEnabled(!PXLongOperation.Exists(this.UID));
    }
  }

  protected override void SYImportOperation_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }

  public string[] GetInternalFields()
  {
    if (this.MappingsSingle.Current == null || string.IsNullOrEmpty(this.MappingsSingle.Current.ScreenID))
      return Array.Empty<string>();
    PXSiteMap.ScreenInfo info = ScreenUtils.ScreenInfo.TryGet(this.MappingsSingle.Current.ScreenID);
    Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
    List<string> stringList = new List<string>();
    foreach (string key in info.Containers.Keys)
    {
      int length = key.IndexOf(": ");
      if (length == -1 || !dictionary.ContainsKey(key.Substring(0, length)))
      {
        dictionary.Add(key, true);
        foreach (PX.Data.Description.FieldInfo field in info.Containers[key].Fields)
          stringList.Add($"[{key}.{field.FieldName}]");
        if (key == info.PrimaryView)
        {
          Tuple<PXFieldState, short, short, string>[] attributeFields = KeyValueHelper.GetAttributeFields(this.MappingsSingle.Current.ScreenID);
          stringList.AddRange(((IEnumerable<Tuple<PXFieldState, short, short, string>>) attributeFields).Select<Tuple<PXFieldState, short, short, string>, string>((Func<Tuple<PXFieldState, short, short, string>, string>) (x => $"[{info.PrimaryView}.{x.Item1.Name}]")));
        }
      }
    }
    return stringList.ToArray();
  }

  public string[] GetExternalFields()
  {
    return PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderField.objectName, Equal<Current<SYMapping.providerObject>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>>, OrderBy<Asc<SYProviderField.displayName>>>.Config>.Select((PXGraph) this).Select<PXResult<SYProviderField>, string>((Expression<Func<PXResult<SYProviderField>, string>>) (field => "[" + ((SYProviderField) field).Name + "]")).ToArray<string>();
  }

  public override void Persist()
  {
    bool flag1 = false;
    SYMappingFieldSimple[] array = this.MappingsSimple.Select().Select<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>((Expression<Func<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>>) (field => (SYMappingFieldSimple) field)).ToArray<SYMappingFieldSimple>();
    if (this.MappingsSimple.Cache.IsDirty && SYImportSimple.GetSaveWarnings(this.NewScenarioProperties.Current, array).Any<string>((Func<string, bool>) (warning => this.MappingsSimple.Ask("Warning", warning, MessageButtons.YesNo) == WebDialogResult.Yes)))
      flag1 = true;
    if (flag1)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      SYMappingSimpleProperty current = this.NewScenarioProperties.Current;
      if (current != null && current.Mapping != null)
      {
        bool flag2 = this.MappingsSimple.Cache.Inserted.Cast<SYMappingFieldSimple>().Any<SYMappingFieldSimple>() || this.MappingsSimple.Cache.Updated.Cast<SYMappingFieldSimple>().Any<SYMappingFieldSimple>() || this.MappingsSimple.Cache.Deleted.Cast<SYMappingFieldSimple>().Any<SYMappingFieldSimple>();
        bool? nullable = current.CreationProcessIsOn;
        bool flag3 = true;
        int num1;
        if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
        {
          nullable = current.RefreshExistingMapping;
          bool flag4 = true;
          num1 = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 1 : 0;
        }
        else
          num1 = 1;
        int num2 = flag2 ? 1 : 0;
        if ((num1 | num2) != 0)
        {
          nullable = current.RefreshExistingMapping;
          bool flag5 = true;
          int num3;
          if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue | flag2)
          {
            nullable = current.CreationProcessIsOn;
            bool flag6 = true;
            num3 = !(nullable.GetValueOrDefault() == flag6 & nullable.HasValue) ? 1 : 0;
          }
          else
            num3 = 0;
          bool recreateMappingFields = num3 != 0;
          nullable = current.CreationProcessIsOn;
          bool flag7 = true;
          int num4;
          if (!(nullable.GetValueOrDefault() == flag7 & nullable.HasValue))
          {
            nullable = current.RefreshExistingMapping;
            bool flag8 = true;
            num4 = nullable.GetValueOrDefault() == flag8 & nullable.HasValue ? 1 : 0;
          }
          else
            num4 = 1;
          bool attachFileToProvider = num4 != 0;
          SYImportSimple.Save(this.NewScenarioProperties.Current, this.MappingsSimple.Select().Select<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>((Expression<Func<PXResult<SYMappingFieldSimple>, SYMappingFieldSimple>>) (field => (SYMappingFieldSimple) field)).ToArray<SYMappingFieldSimple>(), attachFileToProvider, recreateMappingFields);
          this.MappingsSingle.Current = this.NewScenarioProperties.Current.Mapping;
          this.MappingsSingle.Cache.Remove((object) this.MappingsSingle.Current);
          this.MappingsSingle.Cache.PlaceNotChanged((object) this.MappingsSingle.Current);
          current.CreationProcessIsOn = new bool?(false);
          current.RefreshExistingMapping = new bool?(false);
          this.MappingsSimple.Cache.Clear();
        }
      }
      base.Persist();
      transactionScope.Complete();
    }
    this.NewScenarioProperties.Current.MappingAlreadyLoaded = new bool?(false);
    this.Mappings.Current.TStamp = this.Mappings.Current.TStamp ?? this.TimeStamp;
    this.Mappings.Cache.RaiseRowSelected((object) this.Mappings.Current);
  }

  public override void Unload()
  {
    if (this.NewScenarioProperties.Current != null && this.NewScenarioProperties.Current.ProviderGraph != null)
      this.NewScenarioProperties.Current.ProviderGraph.Unload();
    base.Unload();
  }

  public override void Load()
  {
    if (this.NewScenarioProperties.Current != null && this.NewScenarioProperties.Current.ProviderGraph != null)
    {
      this.NewScenarioProperties.Current.ProviderGraph.UnattendedMode = false;
      this.NewScenarioProperties.Current.ProviderGraph.Load();
    }
    base.Load();
  }

  private void PerformOperation(string opType)
  {
    SYMappingActive row = this.MappingsSingle.Current;
    if (this.MappingsSingle.Cache.GetStatus((object) row) == PXEntryStatus.Updated)
      this.MappingsSingle.Cache.SetStatus((object) row, PXEntryStatus.Notchanged);
    if (this.PreparedData.Cache.IsDirty && this.Operation.Ask("Unsaved Prepared Data", "Prepared data was not saved. Do you want to save it?", MessageButtons.YesNo) == WebDialogResult.Yes)
      this.Save.Press();
    SYImportProcess graph = PXGraph.CreateInstance<SYImportProcess>();
    SYImportOperation operation = this.Operation.Current;
    operation.Operation = opType;
    operation.BatchSize = row.BatchSize;
    operation.BreakOnError = row.BreakOnError;
    operation.BreakOnTarget = row.BreakOnTarget;
    operation.ProcessInParallel = row.ProcessInParallel;
    operation.SkipHeaders = row.SkipHeaders;
    ImportGraphNavigator.ClearImportInfo();
    this.LongOperationManager.StartOperation(this.UID, (System.Action<CancellationToken>) (cancellationToken =>
    {
      SYProcess.ProcessMapping((SYProcess) graph, row, operation, cancellationToken, true);
      this.activator.EnableButtons(row.Status);
      this.Operation.View.RequestRefresh();
    }));
  }
}
