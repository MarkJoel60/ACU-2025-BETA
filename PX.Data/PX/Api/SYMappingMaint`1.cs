// Decompiled with JetBrains decompiler
// Type: PX.Api.SYMappingMaint`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.CS;
using PX.Data;
using PX.Data.Api.Export.MappingFieldNameTree;
using PX.Data.Description;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

#nullable disable
namespace PX.Api;

public abstract class SYMappingMaint<T> : 
  PXGraph<SYMappingMaint<T>, SYMapping>,
  PXImportAttribute.IPXPrepareItems,
  IFormulaEditorFields,
  IFormulaEditorInternalFields,
  IFormulaEditorExternalFields,
  ICanAlterSiteMap
  where T : SYMapping.mappingType.IMappingConst, new()
{
  public PXSelect<SYMapping, Where<SYMapping.mappingType, Equal<T>>> Mappings;
  public PXSelect<SYImportCondition, Where<SYImportCondition.mappingID, Equal<Current<SYMapping.mappingID>>>> Conditions;
  public PXSelect<SYMappingCondition, Where<SYMappingCondition.mappingID, Equal<Current<SYMapping.mappingID>>>> MatchingConditions;
  [PXImport(typeof (SYMapping))]
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<SYMappingField, OrderBy<Asc<SYMappingField.orderNumber>>> FieldMappings;
  public PXSelect<SYData, Where<SYData.mappingID, Equal<Current<SYMapping.mappingID>>>> Data;
  public PXFilter<SYWhatToShow> WhatToShowFilter;
  public PXFilter<SYInsertFrom> InsertFromFilter;
  public PXSelectOrderBy<MappingFieldTreeNode, OrderBy<Asc<MappingFieldTreeNode.orderNumber>>> MappingFieldTree;
  private PXGraph _Graph;
  private bool isImporting;
  private List<SYProviderField> providerFields;
  private Dictionary<string, PXView> allViewFields = new Dictionary<string, PXView>();
  public PXAction<SYMapping> FillSource;
  public PXAction<SYMapping> FillDestination;
  public PXAction<SYMapping> RowUp;
  public PXAction<SYMapping> RowDown;
  public PXAction<SYMapping> RowInsert;
  public PXAction<SYMapping> InsertFrom;
  public PXAction<SYMapping> ViewScreen;
  public PXAction<SYMapping> ViewSubstitutions;
  private const string ViewNumberSeparator = ": ";
  private bool _Recalculating;
  private Dictionary<string, string> objectNames;
  private const string Available_Mappings_Key = "AvailableMappingsKey";

  [InjectDependency]
  protected IScreenInfoProvider ScreenInfoProvider { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  public bool IsSiteMapAltered { get; internal set; }

  public PXFieldScreenToSiteMapAddHelper<SYMapping> ScreenToSiteMapAddHelper { get; protected set; }

  protected abstract string ScreenId { get; }

  protected bool IsNewUI()
  {
    return PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(this.ScreenId)?.SelectedUI == nameof (T);
  }

  private MappingFieldHandler.Mode MappingFieldHandlerMode
  {
    get
    {
      return this.Mappings.Current == null || !(this.Mappings.Current.MappingType == "I") ? MappingFieldHandler.Mode.Export : MappingFieldHandler.Mode.Import;
    }
  }

  protected IEnumerable mappingFieldTree(string parentNodeKey)
  {
    return (IEnumerable) this.GetFieldNodeFactory()?.CreateChildNodes(parentNodeKey) ?? (IEnumerable) Enumerable.Empty<MappingFieldTreeNode>();
  }

  protected IEnumerable mappings()
  {
    return (IEnumerable) PXSYMappingSelector.GetMappings<SYMapping>(new PXView((PXGraph) this, false, this.Mappings.View.BqlSelect));
  }

  protected IEnumerable fieldMappings()
  {
    SYMappingMaint<T> graph = this;
    graph.CorrectNeedToRefreshOption();
    PXSelectBase pxSelectBase = (PXSelectBase) new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) graph);
    bool? showHidden = graph.WhatToShowFilter.Current.ShowHidden;
    bool flag1 = true;
    if (!(showHidden.GetValueOrDefault() == flag1 & showHidden.HasValue))
      pxSelectBase.View.WhereAnd<Where<SYMappingField.isVisible, Equal<PX.Data.True>>>();
    PXSiteMap.ScreenInfo screenInfo = graph.GetScreenInfo();
    bool screenInfoInNewUI = screenInfo != null && screenInfo.IsNewUI;
    foreach (SYMappingField row in pxSelectBase.View.SelectMulti())
    {
      if (screenInfoInNewUI && graph.Mappings.Current?.MappingType == "I")
        row.ObjectName = ScreenUtils.NormalizeViewName(row.ObjectName);
      bool? isVisible = row.IsVisible;
      bool flag2 = true;
      if (!(isVisible.GetValueOrDefault() == flag2 & isVisible.HasValue))
        graph.DisableRow((object) row);
      yield return (object) row;
    }
  }

  [PXButton(Tooltip = "View Screen", IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "View Screen")]
  protected IEnumerable viewScreen(PXAdapter adapter)
  {
    if (this.Mappings.Current != null && !string.IsNullOrEmpty(this.Mappings.Current.ScreenID))
    {
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.Mappings.Current.ScreenID);
      if (mapNodeByScreenId != null && !string.IsNullOrEmpty(mapNodeByScreenId.Url))
      {
        PXRedirectToUrlException redirectToUrlException = new PXRedirectToUrlException(mapNodeByScreenId.Url, "ViewScreen");
        redirectToUrlException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
        throw redirectToUrlException;
      }
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Add all source fields to the list.")]
  [PXUIField(DisplayName = "Fill Source", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  protected IEnumerable fillSource(PXAdapter adapter)
  {
    if (this.Mappings.Current == null || string.IsNullOrEmpty(this.Mappings.Current.ProviderObject))
      throw new PXException("An external object is not selected.");
    Dictionary<string, SYMappingField> dictionary = new Dictionary<string, SYMappingField>();
    foreach (PXResult<SYMappingField> pxResult in this.FieldMappings.Select())
    {
      SYMappingField syMappingField = (SYMappingField) pxResult;
      if (!string.IsNullOrEmpty(syMappingField.Value))
        dictionary[syMappingField.Value] = syMappingField;
    }
    foreach (PXResult<SYProviderField> pxResult in PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderField.objectName, Equal<Current<SYMapping.providerObject>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>>, OrderBy<Asc<SYProviderField.providerID, Asc<SYProviderField.objectName, Asc<SYProviderField.lineNbr>>>>>.Config>.Select((PXGraph) this))
    {
      SYProviderField syProviderField = (SYProviderField) pxResult;
      if (!dictionary.ContainsKey(syProviderField.Name))
        this.FieldMappings.Cache.Insert((object) new SYMappingField()
        {
          MappingID = this.Mappings.Current.MappingID,
          Value = syProviderField.Name,
          IsActive = new bool?(true)
        });
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Add all target objects and fields to the list.")]
  [PXUIField(DisplayName = "Fill Destination", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  protected IEnumerable fillDestination(PXAdapter adapter)
  {
    Dictionary<string, SYMappingField> dictionary = new Dictionary<string, SYMappingField>();
    foreach (PXResult<SYMappingField> pxResult in this.FieldMappings.Select())
    {
      SYMappingField syMappingField = (SYMappingField) pxResult;
      if (!string.IsNullOrEmpty(syMappingField.ObjectName) && !string.IsNullOrEmpty(syMappingField.FieldName))
        dictionary[$"{syMappingField.ObjectName}#{syMappingField.FieldName}"] = syMappingField;
    }
    IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
    PXSiteMap.ScreenInfo screenInfo = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
    if (screenInfo == null)
      return adapter.Get();
    foreach (string key in screenInfo.Containers.Keys)
    {
      foreach (PX.Data.Description.FieldInfo field in screenInfo.Containers[key].Fields)
      {
        if (!dictionary.ContainsKey($"{key}#{field.FieldName}"))
          this.FieldMappings.Cache.Insert((object) new SYMappingField()
          {
            MappingID = this.Mappings.Current.MappingID,
            IsActive = new bool?(true),
            ObjectName = screenInfo.Containers[key].DisplayName,
            FieldName = field.FieldName,
            NeedCommit = new bool?(field.Callback != null)
          });
      }
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected IEnumerable rowUp(PXAdapter adapter)
  {
    this.MoveUp(this.FieldMappings.Current);
    if (this.FieldMappings.Current != null && this.FieldMappings.Current.MappingID.HasValue)
      this.RecalcHidden(this.FieldMappings.Current);
    return adapter.Get();
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected IEnumerable rowDown(PXAdapter adapter)
  {
    if ("<Set: Branch>".Equals(this.FieldMappings.Current?.FieldName))
      return adapter.Get();
    this.MoveDown(this.FieldMappings.Current);
    if (this.FieldMappings.Current != null && this.FieldMappings.Current.MappingID.HasValue)
      this.RecalcHidden(this.FieldMappings.Current);
    return adapter.Get();
  }

  [PXButton(ImageKey = "RecordAdd", Tooltip = "Insert a new record.")]
  [PXUIField(DisplayName = "Insert", MapEnableRights = PXCacheRights.Update)]
  protected IEnumerable rowInsert(PXAdapter adapter)
  {
    PXSelect<SYMappingField, Where2<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, Or<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.lineNbr, Equal<Required<SYMappingField.lineNbr>>>>>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect = new PXSelect<SYMappingField, Where2<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, Or<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.lineNbr, Equal<Required<SYMappingField.lineNbr>>>>>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    if (this.FieldMappings.Current == null)
      return adapter.Get();
    SYMappingField main = this.FindMain(this.FieldMappings.Current);
    if (main == null)
      return adapter.Get();
    SYMappingField syMappingField = (SYMappingField) pxSelect.Select((object) main.MappingID, (object) main.LineNbr, (object) main.MappingID, (object) main.LineNbr);
    SYMappingField instance = (SYMappingField) this.FieldMappings.Cache.CreateInstance();
    instance.MappingID = main.MappingID;
    int valueOrDefault = syMappingField.OrderNumber.GetValueOrDefault();
    this.InsertFieldOnPosition(instance, ref valueOrDefault);
    return adapter.Get();
  }

  private void MoveUp(SYMappingField mf)
  {
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.orderNumber, Less<Required<SYMappingField.orderNumber>>>>, OrderBy<Desc<SYMappingField.orderNumber>>> pxSelect1 = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.orderNumber, Less<Required<SYMappingField.orderNumber>>>>, OrderBy<Desc<SYMappingField.orderNumber>>>((PXGraph) this);
    PXSelect<SYMappingField, Where2<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, Or<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.lineNbr, Equal<Required<SYMappingField.lineNbr>>>>>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect2 = new PXSelect<SYMappingField, Where2<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, Or<Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.lineNbr, Equal<Required<SYMappingField.lineNbr>>>>>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    if (mf == null)
      return;
    SYMappingField main1 = this.FindMain(mf);
    if (main1 == null)
      return;
    SYMappingField syMappingField = (SYMappingField) pxSelect2.Select((object) main1.MappingID, (object) main1.LineNbr, (object) main1.MappingID, (object) main1.LineNbr)[0];
    SYMappingField mf1 = (SYMappingField) pxSelect1.Select((object) syMappingField.MappingID, (object) syMappingField.OrderNumber);
    if (mf1 == null)
      return;
    SYMappingField main2 = this.FindMain(mf1);
    SYMappingField copy1 = (SYMappingField) this.FieldMappings.Cache.CreateCopy((object) main1);
    SYMappingField copy2 = (SYMappingField) this.FieldMappings.Cache.CreateCopy((object) main2);
    this.ChangeNumber(main1, copy2);
    this.FieldMappings.Cache.MarkUpdated((object) main1);
    this.FieldMappings.Cache.Update((object) copy2);
  }

  private void MoveDown(SYMappingField mf)
  {
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.orderNumber, Greater<Required<SYMappingField.orderNumber>>>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.orderNumber, Greater<Required<SYMappingField.orderNumber>>>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    if (mf == null)
      return;
    SYMappingField main1 = this.FindMain(mf);
    if (main1 == null)
      return;
    SYMappingField mf1 = (SYMappingField) pxSelect.Select((object) main1.MappingID, (object) main1.OrderNumber);
    if (mf1 == null)
      return;
    SYMappingField main2 = this.FindMain(mf1);
    SYMappingField copy1 = (SYMappingField) this.FieldMappings.Cache.CreateCopy((object) main1);
    SYMappingField copy2 = (SYMappingField) this.FieldMappings.Cache.CreateCopy((object) main2);
    this.ChangeNumber(copy1, main2);
    this.FieldMappings.Cache.MarkUpdated((object) main2);
    this.FieldMappings.Cache.Update((object) copy1);
  }

  private void InsertFieldOnPosition(SYMappingField mf, ref int start)
  {
    foreach (PXResult pxResult in new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.orderNumber, GreaterEqual<Required<SYMappingField.orderNumber>>>>, OrderBy<Desc<SYMappingField.orderNumber>>>((PXGraph) this).Select((object) mf.MappingID, (object) start))
    {
      SYMappingField syMappingField1 = pxResult[typeof (SYMappingField)] as SYMappingField;
      SYMappingField syMappingField2 = syMappingField1;
      int? orderNumber = syMappingField2.OrderNumber;
      syMappingField2.OrderNumber = orderNumber.HasValue ? new int?(orderNumber.GetValueOrDefault() + 1) : new int?();
      if (this.FieldMappings.Cache.Locate((object) syMappingField1) is SYMappingField row)
      {
        row.OrderNumber = syMappingField1.OrderNumber;
        this.FieldMappings.Cache.MarkUpdated((object) row);
      }
    }
    mf.OrderNumber = new int?(start);
    SYMappingField syMappingField = this.FieldMappings.Cache.Insert((object) mf) as SYMappingField;
    start = (int?) syMappingField?.OrderNumber ?? start;
  }

  private void ChangeNumber(SYMappingField mf1, SYMappingField mf2)
  {
    int? orderNumber = mf1.OrderNumber;
    mf1.OrderNumber = mf2.OrderNumber;
    mf2.OrderNumber = orderNumber;
  }

  private SYMappingField FindMain(SYMappingField mf)
  {
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.lineNbr, Equal<Required<SYMappingField.lineNbr>>>>> pxSelect = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.lineNbr, Equal<Required<SYMappingField.lineNbr>>>>>((PXGraph) this);
    SYMappingField main;
    if (!mf.ParentLineNbr.HasValue)
      main = (SYMappingField) pxSelect.Select((object) mf.MappingID, (object) mf.LineNbr);
    else
      main = (SYMappingField) pxSelect.Select((object) mf.MappingID, (object) mf.ParentLineNbr);
    return main;
  }

  [PXButton(ImageKey = "Paste", Tooltip = "Insert steps from another scenario.")]
  [PXUIField(DisplayName = "Insert From...", MapEnableRights = PXCacheRights.Insert)]
  protected IEnumerable insertFrom(PXAdapter adapter)
  {
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>>, OrderBy<Desc<SYMappingField.orderNumber>>> pxSelect = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>>, OrderBy<Desc<SYMappingField.orderNumber>>>((PXGraph) this);
    Guid? mappingId = this.InsertFromFilter.Current.MappingID;
    if (this.InsertFromFilter.AskExt() == WebDialogResult.OK && mappingId.HasValue)
    {
      SYMapping syMapping = (SYMapping) PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.mappingID, Equal<Required<SYMapping.mappingID>>>>.Config>.Select((PXGraph) this, (object) mappingId);
      Guid? nullable = syMapping != null ? syMapping.ProviderID : throw new PXException("The scenario cannot be found.");
      Guid? providerId1 = this.Mappings.Current.ProviderID;
      if ((nullable.HasValue == providerId1.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != providerId1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && this.Mappings.Ask("Warning", "The provider in the source scenario is different from the provider in the current scenario. Do you want to continue?", MessageButtons.YesNo, MessageIcon.Warning) == WebDialogResult.No)
        return adapter.Get();
      providerId1 = syMapping.ProviderID;
      Guid? providerId2 = this.Mappings.Current.ProviderID;
      if ((providerId1.HasValue == providerId2.HasValue ? (providerId1.HasValue ? (providerId1.GetValueOrDefault() == providerId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && syMapping.ProviderObject != this.Mappings.Current.ProviderObject && this.Mappings.Ask("Warning", "The object in the source scenario is different from the object in the current scenario. Do you want to continue?", MessageButtons.YesNo, MessageIcon.Warning) == WebDialogResult.No)
        return adapter.Get();
      foreach (PXResult<SYMappingField> pxResult in PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMappingField.mappingID>>>, OrderBy<Asc<SYMappingField.orderNumber>>>.Config>.Select((PXGraph) this, (object) mappingId))
      {
        SYMappingField syMappingField1 = (SYMappingField) pxResult;
        if (!syMappingField1.ParentLineNbr.HasValue)
        {
          SYMappingField syMappingField2 = (SYMappingField) pxSelect.Select();
          SYMappingField mf = new SYMappingField();
          mf.MappingID = this.Mappings.Current.MappingID;
          mf.IsActive = syMappingField1.IsActive;
          mf.IsVisible = syMappingField1.IsVisible;
          mf.ObjectName = syMappingField1.ObjectName;
          mf.FieldName = syMappingField1.FieldName;
          mf.Value = syMappingField1.Value;
          mf.NeedCommit = syMappingField1.NeedCommit;
          mf.IgnoreError = syMappingField1.IgnoreError;
          mf.ParentLineNbr = syMappingField1.ParentLineNbr;
          int start = syMappingField2 != null ? syMappingField2.OrderNumber.GetValueOrDefault() + 1 : 0;
          this.InsertFieldOnPosition(mf, ref start);
        }
      }
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Substitution Lists")]
  protected void viewSubstitutions()
  {
    throw new PXPopupRedirectException((PXGraph) PXGraph.CreateInstance<SYSubstitutionMaint>(), "Substitution Lists", true);
  }

  protected void SYMapping_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    bool flag1 = this.Mappings.Current != null && this.Mappings.Current != e.Row;
    SYMapping row1 = (SYMapping) e.Row;
    if (!flag1 && e.Operation == PXDBOperation.Insert)
    {
      if ((SYMapping) PXSelectBase<SYMapping, PXSelectReadonly<SYMapping, Where<SYMapping.name, Equal<Required<SYMapping.name>>>>.Config>.Select((PXGraph) this, (object) ((SYMapping) e.Row).Name) != null)
      {
        e.Cancel = true;
        throw new PXException("An import or export scenario with the same name already exists. Please enter another name.");
      }
    }
    if (!flag1 && e.Operation == PXDBOperation.Delete)
    {
      bool? isActive = ((SYMapping) e.Row).IsActive;
      bool flag2 = true;
      if (isActive.GetValueOrDefault() == flag2 & isActive.HasValue)
      {
        e.Cancel = true;
        row1.NoteID = new Guid?();
        throw new PXException("Activated scenarios cannot be deleted. Deactivate the scenario and try again.");
      }
    }
    Guid? noteId = row1.NoteID;
    if (noteId.HasValue || flag1)
      return;
    Guid empty = Guid.Empty;
    foreach (Note row2 in this.Caches[typeof (Note)].Inserted)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.Caches[typeof (Note)].PersistInserted((object) row2);
        noteId = row2.NoteID;
        empty = noteId.Value;
        transactionScope.Complete();
      }
    }
    row1.NoteID = new Guid?();
    SYProvider syProvider = (SYProvider) PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) row1.ProviderID);
    syProvider.NoteID = new Guid?(empty);
    this.Caches[typeof (SYProvider)].Update((object) syProvider);
    this.Caches[typeof (SYProvider)].Persist(PXDBOperation.Update);
    this.Caches[typeof (SYProvider)].IsDirty = false;
    foreach (NoteDoc noteDoc in this.Caches[typeof (NoteDoc)].Inserted)
      noteDoc.NoteID = new Guid?(empty);
    this.Caches[typeof (NoteDoc)].Persist(PXDBOperation.Insert);
    this.Caches[typeof (NoteDoc)].IsDirty = false;
  }

  protected virtual void _(Events.FieldUpdated<SYMapping.isActive> e)
  {
    if (!(e.NewValue is bool newValue) || newValue)
      return;
    e.Cache.SetValueExt<SYMapping.sitemapScreenId>(e.Row, (object) null);
    e.Cache.SetValueExt<SYMapping.sitemapTitle>(e.Row, (object) null);
  }

  protected virtual void SYMapping_MappingType_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    SYMapping.mappingType.IMappingConst mappingConst = (SYMapping.mappingType.IMappingConst) new T();
    e.NewValue = (object) mappingConst.Value;
  }

  protected virtual void SYMapping_MappingID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void SYMapping_ScreenID_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || !(e.NewValue is string))
      return;
    e.NewValue = (object) ((string) e.NewValue).Replace(".", "");
  }

  protected void SYMapping_NoteFiles_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
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

  protected void SYMapping_NoteText_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
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

  protected void SYMapping_NoteFiles_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    this.NoteUpdating(e.Row as SYMapping);
  }

  protected void SYMapping_NoteText_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    this.NoteUpdating(e.Row as SYMapping);
  }

  private void NoteUpdating(SYMapping row)
  {
    if (row == null || row.NoteID.HasValue)
      return;
    PXResult<SYProvider, Note> pxResult = (PXResult<SYProvider, Note>) (PXResult<SYProvider>) PXSelectBase<SYProvider, PXSelectJoin<SYProvider, InnerJoin<Note, On<Note.noteID, Equal<SYProvider.noteID>>>, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) row.ProviderID);
    if (pxResult != null)
    {
      Note note = (Note) pxResult;
      row.NoteID = note.NoteID;
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

  private void NoteSelecting(PXCache sender, SYMapping row, bool throwError)
  {
    if (row == null || !row.ProviderID.HasValue)
      return;
    SYProvider data = (SYProvider) PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.ProviderID);
    if (data == null)
    {
      PXSetPropertyException<SYMapping.providerID> propertyException = new PXSetPropertyException<SYMapping.providerID>("The provider does not exist in the system.");
      if (throwError)
        throw propertyException;
      sender.RaiseExceptionHandling<SYMapping.providerID>((object) row, (object) row.ProviderID, (Exception) propertyException);
    }
    else
    {
      sender.RaiseExceptionHandling<SYMapping.providerID>((object) row, (object) data.Name, (Exception) null);
      if (!data.NoteID.HasValue)
        PXNoteAttribute.GetNoteID(this.Caches[typeof (SYProvider)], (object) data, (string) null);
      row.NoteID = data.NoteID;
    }
  }

  protected virtual void SYMapping_ProviderObject_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    int count = this.FieldMappings.Select().Count;
    if (e.Row is SYMapping && this.Mappings.View.Answer == WebDialogResult.None && count > 0)
      ((SYMapping) e.Row).ProviderObject = (string) e.OldValue;
    if (count <= 0 || this.Mappings.View.Ask("Do you want to preserve the existing field mapping?", MessageButtons.YesNo) != WebDialogResult.No)
      return;
    foreach (PXResult<SYMappingField> pxResult in this.FieldMappings.Select())
      this.FieldMappings.Cache.Delete((object) (SYMappingField) pxResult);
  }

  protected virtual void SYMapping_ProviderID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SYMapping row))
      return;
    row.ProviderObject = this.GetInitialProviderObject(row.ProviderID);
    if (row.ProviderID.HasValue)
    {
      using (IEnumerator<PXResult<SYProvider>> enumerator = PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) row.ProviderID).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<SYProvider> current = enumerator.Current;
          row.NoteID = ((SYProvider) current).NoteID;
        }
      }
    }
    this.NoteSelecting(cache, row, true);
  }

  private string GetInitialProviderObject(Guid? providerId)
  {
    string initialProviderObject = (string) null;
    List<SYProviderObject> list = PXSelectBase<SYProviderObject, PXSelect<SYProviderObject, Where<SYProviderObject.providerID, Equal<Required<SYMapping.providerID>>, And<SYProviderObject.isActive, Equal<PX.Data.True>>>>.Config>.Select((PXGraph) this, (object) providerId).Select<PXResult<SYProviderObject>, SYProviderObject>((Expression<Func<PXResult<SYProviderObject>, SYProviderObject>>) (r => (SYProviderObject) r)).ToList<SYProviderObject>();
    if (list.Count == 1)
      initialProviderObject = list[0].Name;
    return initialProviderObject;
  }

  protected virtual void SYMapping_ScreenID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<SYMappingField> pxResult in this.FieldMappings.Select())
      this.FieldMappings.Cache.Delete((object) (SYMappingField) pxResult);
  }

  protected virtual void SYMapping_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SYMapping row) || string.IsNullOrEmpty(row.ScreenID))
    {
      this.ViewScreen.SetEnabled(false);
    }
    else
    {
      this.ViewScreen.SetEnabled(true);
      string errorMessage;
      if (this.GetScreenInfoGeneric(row.ScreenID, out errorMessage) == null)
      {
        string error = string.IsNullOrEmpty(errorMessage) ? "This form cannot be automated." : $"{"This form cannot be automated."} {errorMessage}";
        if (PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(row.ScreenID) == null)
          error = $"A site map node with the screen ID {row.ScreenID} does not exist. Maybe it was moved or removed.";
        PXUIFieldAttribute.SetWarning<SYMapping.screenID>(cache, (object) row, error);
      }
      else
      {
        PXUIFieldAttribute.SetWarning<SYMapping.screenID>(cache, (object) row, (string) null);
        Guid? providerId;
        if (this.Mappings.Current != null)
        {
          providerId = this.Mappings.Current.ProviderID;
          if (providerId.HasValue)
          {
            PXUIFieldAttribute.SetEnabled<SYMapping.providerObject>(cache, (object) null, true);
            goto label_10;
          }
        }
        PXUIFieldAttribute.SetEnabled<SYMapping.providerObject>(cache, (object) null, false);
label_10:
        if (!string.IsNullOrEmpty(row.GraphName))
        {
          if (this._Graph == null || this._Graph.GetType().FullName != row.GraphName)
            this._Graph = this.CreateGraph(row.GraphName);
        }
        else
          this._Graph = (PXGraph) null;
        providerId = row.ProviderID;
        if (providerId.HasValue)
        {
          using (IEnumerator<PXResult<SYProvider>> enumerator = PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select((PXGraph) this, (object) row.ProviderID).GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              PXResult<SYProvider> current = enumerator.Current;
              row.NoteID = ((SYProvider) current).NoteID;
            }
          }
        }
        this.NoteSelecting(cache, row, false);
      }
    }
  }

  private PXSiteMap.ScreenInfo GetScreenInfoGeneric(string screenID, out string errorMessage)
  {
    Exception error = (Exception) null;
    PXSiteMap.ScreenInfo screenInfoGeneric;
    if (!this.IsImport && !this.IsExport || HttpContext.Current != null)
    {
      IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
      screenInfoGeneric = screenInfoProvider != null ? screenInfoProvider.TryGet(screenID, out error) : (PXSiteMap.ScreenInfo) null;
    }
    else
    {
      IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
      screenInfoGeneric = screenInfoProvider != null ? screenInfoProvider.Get(screenID) : (PXSiteMap.ScreenInfo) null;
    }
    errorMessage = error?.Message;
    return screenInfoGeneric;
  }

  protected virtual void SYMapping_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    SYMapping row = (SYMapping) e.Row;
    SYMapping oldRow = (SYMapping) e.OldRow;
    PXSelect<SYMapping, Where<SYMapping.mappingID, Equal<Current<SYMapping.inverseMappingID>>>> pxSelect = new PXSelect<SYMapping, Where<SYMapping.mappingID, Equal<Current<SYMapping.inverseMappingID>>>>((PXGraph) this);
    if (row == null || string.IsNullOrEmpty(row.ScreenID) || oldRow != null && !(row.ScreenID != oldRow.ScreenID))
      return;
    PXSiteMap.ScreenInfo screenInfoGeneric = this.GetScreenInfoGeneric(row.ScreenID, out string _);
    if (screenInfoGeneric == null)
    {
      PXUIFieldAttribute.SetWarning<SYMapping.screenID>(cache, (object) row, "This form cannot be automated.");
    }
    else
    {
      PXUIFieldAttribute.SetWarning<SYMapping.screenID>(cache, (object) row, (string) null);
      row.GraphName = screenInfoGeneric.GraphName;
      row.ViewName = screenInfoGeneric.PrimaryView;
      row.GridViewName = screenInfoGeneric.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (c => c.Value.IsGrid)).Select<KeyValuePair<string, PXViewDescription>, string>((Func<KeyValuePair<string, PXViewDescription>, string>) (c => c.Value.ViewName)).FirstOrDefault<string>();
      SYMapping syMapping = (SYMapping) pxSelect.Select();
      if (syMapping == null)
        return;
      Guid? inverseMappingId = syMapping.InverseMappingID;
      Guid? mappingId = this.Mappings.Current.MappingID;
      if ((inverseMappingId.HasValue == mappingId.HasValue ? (inverseMappingId.HasValue ? (inverseMappingId.GetValueOrDefault() != mappingId.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        return;
      syMapping.InverseMappingID = this.Mappings.Current.MappingID;
      this.Caches[typeof (SYMapping)].Update((object) syMapping);
    }
  }

  protected virtual void SYMapping_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (e.Operation != PXDBOperation.Delete || e.TranStatus != PXTranStatus.Completed)
      return;
    SYMapping row = (SYMapping) e.Row;
    if (row == null)
      return;
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        SyMappingUtils.DeleteHistoryEntries(row.MappingID);
        SyMappingUtils.DeleteDataEntries(row.MappingID);
        transactionScope.Complete();
      }
    }
  }

  [PXMergeAttributes]
  [PXFormulaEditor_AddSubstitutions]
  protected virtual void _(Events.CacheAttached<SYMappingField.value> e)
  {
  }

  protected virtual void SYMappingField_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (((SYMappingField) e.Row).ParentLineNbr.HasValue && e.ExternalCall)
      throw new PXException("System rows cannot be deleted.");
  }

  protected virtual void SYMappingField_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    SYMappingField row = (SYMappingField) e.Row;
    if (!row.ParentLineNbr.HasValue)
      return;
    this.DisableRow((object) row);
  }

  protected virtual void SYMappingField_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (this.IsNewUI() && this.Mappings.Current?.MappingType == "I")
      return;
    SYMappingField row = (SYMappingField) e.Row;
    SYMappingField syMappingField = (SYMappingField) PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>>, OrderBy<Desc<SYMappingField.orderNumber>>>.Config>.Select((PXGraph) this);
    if (syMappingField == null || row.ObjectName != null)
      return;
    row.ObjectName = syMappingField.ObjectName;
  }

  protected virtual void SYMappingField_MappingID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Mappings.Current == null)
      return;
    e.NewValue = (object) this.Mappings.Current.MappingID;
  }

  protected virtual void SYMappingField_OrderNumber_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    SYMappingField syMappingField = (SYMappingField) PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>>, OrderBy<Desc<SYMappingField.orderNumber>>>.Config>.Select((PXGraph) this);
    if (syMappingField != null)
    {
      PXFieldDefaultingEventArgs defaultingEventArgs = e;
      int? orderNumber = syMappingField.OrderNumber;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local = (ValueType) (orderNumber.HasValue ? new int?(orderNumber.GetValueOrDefault() + 1) : new int?());
      defaultingEventArgs.NewValue = (object) local;
    }
    else
      e.NewValue = (object) 0;
  }

  protected virtual void SYMappingField_IsActive_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    PXBoolAttribute.ConvertValue(e);
    SYMappingField row = (SYMappingField) e.Row;
    bool flag1 = false;
    bool? isVisible = row.IsVisible;
    bool flag2 = true;
    if (!(isVisible.GetValueOrDefault() == flag2 & isVisible.HasValue))
      return;
    foreach (PXResult<SYMappingField> pxResult in PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.parentLineNbr>>, And<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>>>>.Config>.Select((PXGraph) this, (object) row.LineNbr))
    {
      SYMappingField syMappingField = (SYMappingField) pxResult;
      flag1 = true;
      syMappingField.IsActive = (bool?) e.NewValue;
      this.FieldMappings.Cache.Update((object) syMappingField);
    }
    if (!flag1)
      return;
    bool? showHidden = this.WhatToShowFilter.Current.ShowHidden;
    bool flag3 = true;
    if (!(showHidden.GetValueOrDefault() == flag3 & showHidden.HasValue))
      return;
    this.FieldMappings.View.RequestRefresh();
  }

  protected virtual void SYMappingField_ObjectName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    this.ObjectName_OnFieldSelecting<SYMappingField.objectName>((ISYObjectField) (e.Row as SYMappingField), cache);
  }

  protected virtual void SYMappingField_FieldName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    bool defaultObjectName = !this.IsNewUI() || this.Mappings.Current?.MappingType == "E";
    this.FieldName_OnFieldSelecting<SYMappingField.fieldName>((ISYObjectField) (e.Row as SYMappingField), cache, false, defaultObjectName: defaultObjectName);
  }

  protected virtual void SYMappingField_Value_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is SYMappingField row))
      return;
    this.SetValueList(row);
  }

  private PXSiteMap.ScreenInfo GetScreenInfo()
  {
    return this.Mappings.Current != null ? this.GetScreenInfoGeneric(this.Mappings.Current.ScreenID, out string _) : (PXSiteMap.ScreenInfo) null;
  }

  protected void ObjectName_OnFieldSelecting<TFieldType>(
    ISYObjectField row,
    PXCache cache,
    bool isSummaryAndDetailObjectsOnly = false)
    where TFieldType : IBqlField
  {
    PXSiteMap.ScreenInfo screenInfo = this.GetScreenInfo();
    if (screenInfo == null)
      return;
    this.SetObjectNameList<TFieldType>(row, screenInfo, cache, isSummaryAndDetailObjectsOnly);
  }

  private void SetObjectNameList<TFieldType>(
    ISYObjectField row,
    PXSiteMap.ScreenInfo info,
    PXCache cache,
    bool isSummaryAndDetailObjectsOnly = false)
    where TFieldType : IBqlField
  {
    List<string> resvalues = new List<string>();
    List<string> reslabels = new List<string>();
    this.CreateObjectNameList(resvalues, reslabels, info, isSummaryAndDetailObjectsOnly);
    PXStringListAttribute.SetList<TFieldType>(cache, (object) row, resvalues.ToArray(), reslabels.ToArray());
    string viewNameForNewUi = this.GetOriginalViewNameForNewUI(info, row?.ObjectName, out int? _);
    if (row == null || string.IsNullOrEmpty(viewNameForNewUi) || resvalues.IndexOf(viewNameForNewUi) > -1)
      PXUIFieldAttribute.SetWarning<TFieldType>(cache, (object) row, (string) null);
    else
      PXUIFieldAttribute.SetWarning<TFieldType>(cache, (object) row, "This object doesn't exist.");
  }

  protected string GetOriginalViewNameForNewUI(
    PXSiteMap.ScreenInfo info,
    string viewName,
    out int? viewNumber)
  {
    viewNumber = new int?();
    if (!info.IsNewUI || string.IsNullOrEmpty(viewName))
      return viewName;
    string viewNameForNewUi = viewName;
    string[] strArray = viewName.Split(new string[1]{ ": " }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray.Length == 2)
    {
      viewNameForNewUi = strArray[0];
      int result;
      if (int.TryParse(strArray[1], out result))
        viewNumber = new int?(result);
    }
    return viewNameForNewUi;
  }

  private Dictionary<string, string> CreateObjectNameList(
    List<string> resvalues,
    List<string> reslabels,
    PXSiteMap.ScreenInfo info,
    bool isSummaryAndDetailObjectsOnly = false)
  {
    Dictionary<string, string> objectNameList = new Dictionary<string, string>();
    List<Pair> pairList1 = new List<Pair>();
    List<Pair> pairList2 = new List<Pair>();
    foreach (string key1 in info.Containers.Keys)
    {
      if (!(typeof (T) != typeof (SYMapping.mappingType.typeImport)) || !string.Equals(key1, "FilterPreview", StringComparison.OrdinalIgnoreCase))
      {
        Pair pair = new Pair((object) key1, (object) info.Containers[key1].DisplayName);
        if (key1 == info.PrimaryView)
          pairList1.Add(pair);
        else if (isSummaryAndDetailObjectsOnly)
        {
          string key2 = ScreenUtils.NormalizeViewName(key1);
          PXView pxView;
          if (key2 == info.PrimaryView || info.Containers[key1].IsGrid || this._Graph != null && this._Graph.Views.TryGetValue(key2, out pxView) && pxView.CacheType?.FullName == info.PrimaryViewTypeName)
            pairList2.Add(pair);
        }
        else
          pairList2.Add(pair);
      }
    }
    pairList1.Sort(new Comparison<Pair>(this.CompareObjectContainers));
    pairList2.Sort(new Comparison<Pair>(this.CompareObjectContainers));
    foreach (Pair pair in pairList1)
    {
      resvalues.Add(pair.First as string);
      reslabels.Add(pair.Second as string);
    }
    foreach (Pair pair in pairList2)
    {
      resvalues.Add(pair.First as string);
      reslabels.Add(pair.Second as string);
    }
    SYMappingMaint<T>.MakeObjectNameLabelsDistinct((IList<string>) resvalues, (IList<string>) reslabels);
    for (int index = 0; index < resvalues.Count; ++index)
    {
      string lower = reslabels[index].ToLower();
      string resvalue = resvalues[index];
      objectNameList[lower] = resvalue;
    }
    return objectNameList;
  }

  internal static void MakeObjectNameLabelsDistinct(
    IList<string> nameKeysList,
    IList<string> nameLabelsList)
  {
    if (nameKeysList == null || nameLabelsList == null || nameKeysList.Count != nameLabelsList.Count)
      return;
    SYMappingMaint<T>.NormalizeNameLabels(nameKeysList, nameLabelsList);
    bool flag = false;
    for (int index1 = 0; index1 < nameKeysList.Count - 1; ++index1)
    {
      string nameLabels = nameLabelsList[index1];
      for (int index2 = index1 + 1; index2 < nameKeysList.Count; ++index2)
      {
        if (nameLabelsList[index2] == nameLabels)
        {
          nameLabelsList[index2] = SYMappingMaint<T>.FormDistinctNameLabel(nameKeysList[index2], nameLabelsList[index2]);
          flag = true;
        }
      }
      if (flag)
      {
        flag = false;
        nameLabelsList[index1] = SYMappingMaint<T>.FormDistinctNameLabel(nameKeysList[index1], nameLabelsList[index1]);
      }
    }
  }

  private static void NormalizeNameLabels(IList<string> nameKeysList, IList<string> nameLabelsList)
  {
    for (int index = 0; index < nameLabelsList.Count; ++index)
    {
      if (string.IsNullOrEmpty(nameLabelsList[index]))
        nameLabelsList[index] = nameKeysList[index];
    }
  }

  private static string FormDistinctNameLabel(string nameKey, string nameLabel)
  {
    return !string.IsNullOrEmpty(nameKey) ? $"{nameLabel} ({nameKey})" : (string) null;
  }

  /// <summary>
  /// Generic FieldSelecting event handler for ISYObjectField.FieldName.
  /// </summary>
  /// <typeparam name="TFieldType">Field type for filling PXStringListAttribute on it.</typeparam>
  /// <param name="fieldsOnly">If false, adds actions, "Add All Fields", "Dialog Answer" items.</param>
  protected void FieldName_OnFieldSelecting<TFieldType>(
    ISYObjectField row,
    PXCache cache,
    bool fieldsOnly,
    bool addSelectorFields = true,
    bool containerFieldsOnly = false,
    bool defaultObjectName = false)
    where TFieldType : IBqlField
  {
    PXSiteMap.ScreenInfo screenInfo = this.GetScreenInfo();
    if (row == null || screenInfo == null)
      return;
    if (string.IsNullOrEmpty(row.ObjectName) & defaultObjectName)
      row.ObjectName = screenInfo.PrimaryView;
    this.SetFieldNameList<TFieldType>(row, screenInfo, cache, fieldsOnly, addSelectorFields, containerFieldsOnly);
  }

  private void SetFieldNameList<TFieldType>(
    ISYObjectField row,
    PXSiteMap.ScreenInfo info,
    PXCache cache,
    bool fieldsOnly,
    bool addSelectorFields,
    bool containerFieldsOnly = false)
    where TFieldType : IBqlField
  {
    List<string> values = new List<string>();
    List<string> labels = new List<string>();
    if (this.IsNewUI() && this.Mappings.Current?.MappingType == "I" && !containerFieldsOnly)
      this.CreateFieldNameListForNewUI(row.ObjectName, values, labels);
    else
      this.CreateFieldNameList(row.ObjectName, values, labels, info, fieldsOnly, addSelectorFields, containerFieldsOnly, out Dictionary<string, string> _);
    PXStringListAttribute.SetList<TFieldType>(cache, (object) row, values.ToArray(), labels.ToArray());
    if (string.IsNullOrEmpty(row.FieldName) || values.IndexOf(row.FieldName) > -1 || row.FieldName.IndexOf("##") > -1 || row.FieldName.IndexOf("@") > -1 || this.Mappings.Current.MappingType == "E")
      return;
    PXUIFieldAttribute.SetWarning<TFieldType>(cache, (object) row, "This field or action doesn't exist.");
  }

  private void AddActionsToFieldNameList(
    PXSiteMap.ScreenInfo.Action[] screenActions,
    List<string> values,
    List<string> labels,
    List<string> labelsObsolete)
  {
    labelsObsolete.AddRange((IEnumerable<string>) labels);
    int count = labels.Count;
    foreach (PXSiteMap.ScreenInfo.Action screenAction in screenActions)
    {
      string actionLabel = MappingFieldNamer.CreateActionLabel(string.IsNullOrEmpty(screenAction.DisplayName) ? screenAction.Name : screenAction.DisplayName);
      int index1 = labels.IndexOf(actionLabel);
      if (index1 > -1)
      {
        int index2 = index1 - count;
        string name = screenActions[index2].Name;
        string label = labels[index1];
        labels[index1] = MappingFieldNamer.CorrectActionLabel(label, name);
        actionLabel = MappingFieldNamer.CorrectActionLabel(actionLabel, screenAction.Name);
      }
      labels.Add(actionLabel);
      labelsObsolete.Add(MappingFieldNamer.CreateActionLabel(screenAction.Name));
      values.Add(MappingFieldNamer.CreateActionValue(screenAction.Name));
    }
  }

  internal MappingFieldTreeNodeFactory GetFieldNodeFactory()
  {
    string screenId = this.Mappings.Current?.ScreenID;
    if (string.IsNullOrEmpty(screenId))
      return (MappingFieldTreeNodeFactory) null;
    PXSiteMap.ScreenInfo screenInfo = this.ScreenInfoProvider.TryGet(screenId);
    if (screenInfo == null)
      return (MappingFieldTreeNodeFactory) null;
    PXGraph graph = this._Graph;
    return graph == null ? (MappingFieldTreeNodeFactory) null : new MappingFieldTreeNodeFactory(screenInfo, graph, screenId, true);
  }

  internal MappingFieldNodeKeyParser GetFieldNodeKeyParser()
  {
    return this._Graph == null ? (MappingFieldNodeKeyParser) null : new MappingFieldNodeKeyParser((IEnumerable<string>) this._Graph.Views.Keys);
  }

  private void CreateFieldNameListForNewUI(
    string objectName,
    List<string> values,
    List<string> labels)
  {
    if (string.IsNullOrEmpty(objectName))
      return;
    MappingFieldTreeNodeFactory fieldNodeFactory = this.GetFieldNodeFactory();
    if (fieldNodeFactory == null)
      return;
    MappingFieldNodeKeyParser fieldNodeKeyParser = this.GetFieldNodeKeyParser();
    if (fieldNodeKeyParser == null)
      return;
    IEnumerable<MappingFieldTreeNode> childNodes1 = fieldNodeFactory.CreateChildNodes("<ActionsNode>");
    string viewNodeKey = MappingFieldNodeTextGenerator.GetViewNodeKey(objectName);
    IEnumerable<MappingFieldTreeNode> childNodes2 = fieldNodeFactory.CreateChildNodes(viewNodeKey);
    foreach (MappingFieldTreeNode mappingFieldTreeNode in childNodes1.Concat<MappingFieldTreeNode>(childNodes2).Where<MappingFieldTreeNode>((Func<MappingFieldTreeNode, bool>) (n => !string.IsNullOrEmpty(n.Value))))
    {
      string commandName = fieldNodeKeyParser.ParseNodeKey(mappingFieldTreeNode.Value).CommandName;
      values.Add(commandName);
      labels.Add(mappingFieldTreeNode.Text);
    }
  }

  /// <param name="fieldsOnly">If false, adds actions, "Add All Fields", "Dialog Answer" items.</param>
  private Dictionary<string, string> CreateFieldNameList(
    string objectName,
    List<string> values,
    List<string> labels,
    PXSiteMap.ScreenInfo info,
    bool fieldsOnly,
    bool addSelectorFields,
    bool containerFieldsOnly,
    out Dictionary<string, string> valuesByObsoleteLabels)
  {
    Dictionary<string, string> fieldNameList = new Dictionary<string, string>();
    valuesByObsoleteLabels = new Dictionary<string, string>();
    objectName = this.GetOriginalViewNameForNewUI(info, objectName, out int? _);
    if (string.IsNullOrEmpty(objectName) || !info.Containers.ContainsKey(objectName))
      return fieldNameList;
    if (info.Containers[objectName].CreatedFromOtherViewDescription || string.Equals(objectName, "FilterPreview", StringComparison.OrdinalIgnoreCase))
      fieldsOnly = true;
    List<PX.Data.Description.FieldInfo> fieldInfoList = new List<PX.Data.Description.FieldInfo>((IEnumerable<PX.Data.Description.FieldInfo>) info.Containers[objectName].Fields);
    if (!fieldsOnly)
    {
      PX.Data.Description.FieldInfo fieldInfo = new PX.Data.Description.FieldInfo("<Add All Fields>", "<Add All Fields>", (CallbackDescr) null, false, typeof (string), true, false, false, false, (string) null, "", (string) null, (object) null, (object) null, 10, 0, (string[]) null, false);
      fieldInfoList.Add(fieldInfo);
    }
    if (objectName == info.PrimaryView)
    {
      Tuple<PXFieldState, short, short, string>[] attributeFields = KeyValueHelper.GetAttributeFields(this.Mappings.Current?.ScreenID);
      if (attributeFields.Length != 0)
        fieldInfoList.AddRange(((IEnumerable<Tuple<PXFieldState, short, short, string>>) attributeFields).Select<Tuple<PXFieldState, short, short, string>, PX.Data.Description.FieldInfo>((Func<Tuple<PXFieldState, short, short, string>, PX.Data.Description.FieldInfo>) (_ => new PX.Data.Description.FieldInfo(_.Item1.Name, _.Item1.DisplayName, (CallbackDescr) null, false, _.Item1.DataType, true, false, true, false, (string) null, (string) null, (string) null, (object) null, (object) null, -1, -1, (string[]) null, false))));
    }
    fieldInfoList.Sort(new Comparison<PX.Data.Description.FieldInfo>(this.CompareFieldInfos));
    values.Add(string.Empty);
    labels.Add(string.Empty);
    foreach (PX.Data.Description.FieldInfo field in fieldInfoList)
    {
      string fieldName = field.FieldName;
      string displayName = field.DisplayName;
      if (fieldName.IndexOf("!") <= -1)
      {
        values.Add(fieldName);
        labels.Add(displayName);
        if (addSelectorFields)
        {
          foreach (KeyValuePair<string, string> keyValuePair in !string.Equals(objectName, "FilterPreview", StringComparison.OrdinalIgnoreCase) ? this.CreateAssignedFieldNameList(objectName, field) : this.CreateAssignedFieldNameListForWorkflowForm(objectName, info, field))
          {
            values.Add(keyValuePair.Value);
            labels.Add(keyValuePair.Key);
          }
        }
      }
    }
    if (!containerFieldsOnly && info.Containers[objectName].Parameters != null)
    {
      foreach (ParsInfo parameter in info.Containers[objectName].Parameters)
      {
        if (parameter.Type != ParType.Filters)
        {
          if (parameter.Type == ParType.Parameters)
          {
            labels.Add(MappingFieldNamer.CreateParameterLabel(parameter.Name));
            values.Add(MappingFieldNamer.CreateParameterValue(parameter.Name));
          }
          else
          {
            labels.Add(MappingFieldNamer.CreateKeyLabel(parameter.Name));
            values.Add(MappingFieldNamer.CreateKeyValue(parameter.Field, parameter.Name));
          }
        }
      }
    }
    if (!containerFieldsOnly && info.Containers[objectName].HasNoteID)
    {
      values.Add(MappingFieldNamer.CreateExternalKeyValue());
      labels.Add(MappingFieldNamer.CreateExternalKeyLabel());
    }
    List<string> stringList = new List<string>();
    if (!fieldsOnly && info.PrimaryView == info.Containers[objectName].ViewName && info.Actions != null)
    {
      PXSiteMap.ScreenInfo.Action[] screenActions;
      if (info.HasWorkflow && !(typeof (T) != typeof (SYMapping.mappingType.typeImport)))
        screenActions = ((IEnumerable<PXSiteMap.ScreenInfo.Action>) info.Actions).Union<PXSiteMap.ScreenInfo.Action>((IEnumerable<PXSiteMap.ScreenInfo.Action>) new PXSiteMap.ScreenInfo.Action[1]
        {
          new PXSiteMap.ScreenInfo.Action("WorkflowTransition", PXLocalizer.Localize("Transition"), true, PXSpecialButtonType.ActionsFolder, false, true)
        }).ToArray<PXSiteMap.ScreenInfo.Action>();
      else
        screenActions = info.Actions;
      List<string> values1 = values;
      List<string> labels1 = labels;
      List<string> labelsObsolete = stringList;
      this.AddActionsToFieldNameList(screenActions, values1, labels1, labelsObsolete);
    }
    if (info.Containers[objectName].HasLineNumber)
    {
      values.Add(MappingFieldNamer.CreateLineNumberValue());
      labels.Add(MappingFieldNamer.CreateLineNumberLabel());
    }
    if (!fieldsOnly)
    {
      values.Add(MappingFieldNamer.CreateDialogAnswerValue());
      labels.Add(MappingFieldNamer.CreateDialogAnswerLabel());
    }
    for (int index = 0; index < values.Count; ++index)
    {
      string lower1 = labels[index].ToLower();
      string str = values[index];
      fieldNameList[lower1] = str;
      if (index < stringList.Count)
      {
        string lower2 = stringList[index].ToLower();
        valuesByObsoleteLabels[lower2] = str;
      }
    }
    if (objectName == info.PrimaryView && typeof (T) == typeof (SYMapping.mappingType.typeImport))
    {
      values.Add("<Set: Branch>");
      labels.Add("<Set: Branch>");
    }
    return fieldNameList;
  }

  private Dictionary<string, string> CreateAssignedFieldNameListForWorkflowForm(
    string objectName,
    PXSiteMap.ScreenInfo info,
    PX.Data.Description.FieldInfo field)
  {
    string fieldName = field.FieldName;
    PXViewDescription pxViewDescription;
    Dictionary<string, string> listForWorkflowForm;
    if (!string.IsNullOrEmpty(field.SelectorViewDescription?.ViewName) && !string.IsNullOrEmpty(info.PrimaryView) && info.Containers.TryGetValue(info.PrimaryView, out pxViewDescription))
    {
      PX.Data.Description.FieldInfo[] allFields = pxViewDescription.AllFields;
      PX.Data.Description.FieldInfo field1 = allFields != null ? ((IEnumerable<PX.Data.Description.FieldInfo>) allFields).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (it => it.SelectorViewDescription?.ViewName == field.SelectorViewDescription?.ViewName)) : (PX.Data.Description.FieldInfo) null;
      if (field1 == null)
      {
        System.Type primaryViewType = this._Graph.Views[info.PrimaryView].Cache.GetItemType();
        field1 = info.Containers.Values.Where<PXViewDescription>((Func<PXViewDescription, bool>) (c => this._Graph.Views[SyMappingUtils.CleanViewName(c.ViewName)].Cache.GetItemType() == primaryViewType)).SelectMany<PXViewDescription, PX.Data.Description.FieldInfo>((Func<PXViewDescription, IEnumerable<PX.Data.Description.FieldInfo>>) (c => (IEnumerable<PX.Data.Description.FieldInfo>) c.AllFields)).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (it => it.SelectorViewDescription?.ViewName == field.SelectorViewDescription?.ViewName));
      }
      listForWorkflowForm = field1 != null ? this.CreateAssignedFieldNameList(info.PrimaryView, field1) : (Dictionary<string, string>) null;
      if (listForWorkflowForm != null)
      {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (string key in listForWorkflowForm.Keys)
        {
          string str = listForWorkflowForm[key];
          string[] strArray1;
          if (str == null)
            strArray1 = (string[]) null;
          else
            strArray1 = str.Split('!');
          string[] strArray2 = strArray1;
          if (strArray2 != null && strArray2.Length == 2)
            dictionary[key] = $"{fieldName}!{strArray2[1]}";
        }
        listForWorkflowForm = dictionary;
      }
    }
    else
      listForWorkflowForm = this.CreateAssignedFieldNameList(objectName, field);
    return listForWorkflowForm;
  }

  private void SetValueList(SYMappingField row)
  {
    List<string> stringList = new List<string>();
    List<string> labels = new List<string>();
    this.CreateValueList(row, stringList, labels);
    PXStringListAttribute.SetList<SYMappingField.value>(this.FieldMappings.Cache, (object) row, stringList.ToArray(), labels.ToArray());
    if (!this.FieldValueIsObsolete(row.Value, stringList))
      return;
    PXUIFieldAttribute.SetWarning<SYMappingField.value>(this.FieldMappings.Cache, (object) row, "This field doesn't exist.");
  }

  private bool FieldValueIsObsolete(string fieldValue, List<string> allowedValues)
  {
    bool flag = false;
    if (!string.IsNullOrEmpty(fieldValue) && !fieldValue.StartsWith("="))
      flag = allowedValues == null || !allowedValues.Contains(fieldValue);
    return flag;
  }

  private Dictionary<string, string> CreateValueList(
    SYMappingField row,
    List<string> values,
    List<string> labels)
  {
    Dictionary<string, string> valueList = new Dictionary<string, string>();
    values.Add(string.Empty);
    labels.Add(string.Empty);
    if (row != null && row.FieldName == "##" && this.AllowLineNumberNew)
    {
      values.Add("=-1");
      labels.Add(PXMessages.LocalizeNoPrefix("=New"));
    }
    if (row != null && this._Graph != null && this.Mappings.Current != null && this.Mappings.Current.MappingType == "E" && !string.IsNullOrEmpty(this.Mappings.Current.ViewName) && !string.IsNullOrEmpty(this.Mappings.Current.ScreenID) && !string.IsNullOrEmpty(row.FieldName))
    {
      IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
      PXSiteMap.ScreenInfo info = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
      if (info == null)
        return valueList;
      string objectName = row.ObjectName;
      string viewNameForNewUi = this.GetOriginalViewNameForNewUI(info, objectName, out int? _);
      if (!string.IsNullOrEmpty(viewNameForNewUi) && info.Containers.ContainsKey(viewNameForNewUi))
      {
        foreach (PX.Data.Description.FieldInfo field in info.Containers[viewNameForNewUi].Fields)
        {
          if (field.FieldName == row.FieldName)
          {
            if (field.AllowedLabels != null && !info.Containers[viewNameForNewUi].HasLineNumber)
            {
              values.Add("=Every");
              labels.Add(PXMessages.LocalizeNoPrefix("=Every"));
              break;
            }
            break;
          }
        }
      }
    }
    if (row != null && row.FieldName == "??")
    {
      foreach (string name in Enum.GetNames(typeof (WebDialogResult)))
      {
        values.Add($"='{name}'");
        labels.Add($"='{name}'");
      }
    }
    if (this.providerFields == null)
    {
      this.providerFields = new List<SYProviderField>();
      foreach (PXResult<SYProviderField> pxResult in PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderField.objectName, Equal<Current<SYMapping.providerObject>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>>, OrderBy<Asc<SYProviderField.displayName>>>.Config>.Select((PXGraph) this))
        this.providerFields.Add((SYProviderField) pxResult);
    }
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal);
    foreach (SYProviderField providerField in this.providerFields)
    {
      values.Add(providerField.Name);
      string str = string.IsNullOrEmpty(providerField.DisplayName) ? providerField.Name : providerField.DisplayName;
      if (!stringSet.Contains(str))
      {
        stringSet.Add(str);
        labels.Add(str);
      }
      else
        labels.Add(providerField.Name);
    }
    for (int index = 0; index < values.Count; ++index)
    {
      string lower = labels[index].ToLower();
      string str = values[index];
      valueList.Add(lower, str);
    }
    return valueList;
  }

  protected virtual void SYMappingField_NeedCommit_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.RecalcHidden((SYMappingField) e.Row);
  }

  protected virtual void SYMappingField_FieldName_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is SYMappingField row) || !(e.NewValue is string str1))
      return;
    if (this.IsNewUI() && this.Mappings.Current?.MappingType == "I")
    {
      MappingFieldNodeKeyParser fieldNodeKeyParser = this.GetFieldNodeKeyParser();
      (string str2, string str3) = fieldNodeKeyParser != null ? fieldNodeKeyParser.ParseNodeKey(str1, false) : ((string) null, (string) null);
      if (!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3))
      {
        row.ObjectName = str2;
        e.NewValue = (object) str3;
        str1 = str3;
      }
    }
    IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
    PXSiteMap.ScreenInfo info = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
    if (info == null || string.IsNullOrEmpty(row.ObjectName) || !info.Containers.ContainsKey(this.GetOriginalViewNameForNewUI(info, row.ObjectName, out int? _)))
      return;
    this.ChangeFieldNameAndCommit(row, str1, info);
  }

  private void ChangeFieldNameAndCommit(SYMappingField row, string val, PXSiteMap.ScreenInfo info)
  {
    string viewNameForNewUi = this.GetOriginalViewNameForNewUI(info, row.ObjectName, out int? _);
    if (val.StartsWith("@"))
    {
      ParType parType = val.StartsWith("@@") ? ParType.Searches : ParType.Parameters;
      string str = parType == ParType.Searches ? "@@" : "@";
      foreach (ParsInfo parameter in info.Containers[viewNameForNewUi].Parameters)
      {
        if (parameter.Type == parType && ((parType == ParType.Parameters || string.IsNullOrEmpty(parameter.Field)) && str + parameter.Name == val || parType == ParType.Searches && str + parameter.Field == val))
        {
          if (string.IsNullOrEmpty(parameter.Field))
            break;
          if (!parameter.Field.Contains<char>('.'))
          {
            row.Value = $"=[{info.Containers[viewNameForNewUi].ViewName}.{parameter.Field}]";
            break;
          }
          row.Value = $"=[{parameter.Field}]";
          break;
        }
      }
    }
    else
    {
      foreach (PX.Data.Description.FieldInfo field in info.Containers[viewNameForNewUi].Fields)
      {
        if (field.FieldName == val && field.Callback != null)
        {
          bool? needCommit = row.NeedCommit;
          bool flag = true;
          if (!(needCommit.GetValueOrDefault() == flag & needCommit.HasValue) && this.CallbacksAutoCommit)
          {
            row.NeedCommit = new bool?(true);
            break;
          }
        }
      }
    }
  }

  protected virtual void SYMappingField_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is SYMappingField row))
      return;
    this.AddAllFiedsIfNeeded(sender, row, (SYMappingField) null);
    if (this.isImporting || row.ParentLineNbr.HasValue)
      return;
    bool? needCommit = row.NeedCommit;
    bool flag = false;
    if (needCommit.GetValueOrDefault() == flag & needCommit.HasValue && !this.IsRowKey(row))
      return;
    if (row.MappingID.HasValue)
      this.RecalcHidden(row);
    if (this.Mappings.Current == null || PXLongOperation.GetStatus(this.UID) == PXLongRunStatus.InProcess)
      return;
    MappingFieldHandler.AutoSetCommit((PXGraph) this, this.FieldMappings.Cache, row, this.Mappings.Current.ScreenID, this.MappingFieldHandlerMode);
  }

  private void ProcessSetBranchCommand(PXCache cache, SYMappingField row)
  {
    SYMappingField[] array = this.FieldMappings.Select().FirstTableItems.ToArray<SYMappingField>();
    if (((IEnumerable<SYMappingField>) array).Where<SYMappingField>((Func<SYMappingField, bool>) (f =>
    {
      if (!"<Set: Branch>".Equals(f.FieldName, StringComparison.OrdinalIgnoreCase))
        return false;
      short? lineNbr = f.LineNbr;
      int? nullable1 = lineNbr.HasValue ? new int?((int) lineNbr.GetValueOrDefault()) : new int?();
      lineNbr = row.LineNbr;
      int? nullable2 = lineNbr.HasValue ? new int?((int) lineNbr.GetValueOrDefault()) : new int?();
      return !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
    })).Any<SYMappingField>())
      throw new PXSetPropertyException("The <Set: Branch> command has been already added.");
    SYMappingField syMappingField1 = array[0];
    if ("<Set: Branch>".Equals(syMappingField1.FieldName, StringComparison.OrdinalIgnoreCase))
      return;
    int? orderNumber1 = syMappingField1.OrderNumber;
    int? orderNumber2 = row.OrderNumber;
    foreach (SYMappingField row1 in array)
    {
      if ("<Set: Branch>".Equals(row1.FieldName, StringComparison.OrdinalIgnoreCase))
      {
        row1.OrderNumber = orderNumber1;
      }
      else
      {
        int? orderNumber3 = row1.OrderNumber;
        int? nullable = orderNumber2;
        if (orderNumber3.GetValueOrDefault() <= nullable.GetValueOrDefault() & orderNumber3.HasValue & nullable.HasValue)
        {
          SYMappingField syMappingField2 = row1;
          orderNumber3 = syMappingField2.OrderNumber;
          syMappingField2.OrderNumber = orderNumber3.HasValue ? new int?(orderNumber3.GetValueOrDefault() + 1) : new int?();
        }
        else
        {
          SYMappingField syMappingField3 = row1;
          orderNumber3 = syMappingField3.OrderNumber;
          syMappingField3.OrderNumber = orderNumber3.HasValue ? new int?(orderNumber3.GetValueOrDefault() - 1) : new int?();
        }
      }
      cache.MarkUpdated((object) row1);
    }
  }

  protected virtual void SYMappingField_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    SYMappingField row = (SYMappingField) e.Row;
    SYMappingField oldRow = (SYMappingField) e.OldRow;
    this.AddAllFiedsIfNeeded(cache, row, (SYMappingField) null);
    if (this.Mappings.Current != null && row != null && oldRow != null)
    {
      bool? needCommit1 = row.NeedCommit;
      bool? needCommit2 = oldRow.NeedCommit;
      if (needCommit1.GetValueOrDefault() == needCommit2.GetValueOrDefault() & needCommit1.HasValue == needCommit2.HasValue && PXLongOperation.GetStatus(this.UID) != PXLongRunStatus.InProcess)
        MappingFieldHandler.AutoSetCommit((PXGraph) this, this.FieldMappings.Cache, row, this.Mappings.Current.ScreenID, this.MappingFieldHandlerMode);
    }
    bool? nullable = this.WhatToShowFilter.Current.NeedToRefresh;
    bool flag = true;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    if (!row.ParentLineNbr.HasValue)
    {
      nullable = row.NeedCommit;
      bool? needCommit = oldRow.NeedCommit;
      if (!(nullable.GetValueOrDefault() == needCommit.GetValueOrDefault() & nullable.HasValue == needCommit.HasValue))
        return;
      int? orderNumber1 = row.OrderNumber;
      int? orderNumber2 = oldRow.OrderNumber;
      if (!(orderNumber1.GetValueOrDefault() == orderNumber2.GetValueOrDefault() & orderNumber1.HasValue == orderNumber2.HasValue) || !(row.ObjectName == oldRow.ObjectName) || !(row.FieldName == oldRow.FieldName))
        return;
    }
    this.FieldMappings.View.RequestRefresh();
  }

  protected virtual void SYMappingField_FieldName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SYMappingField row) || !row.MappingID.HasValue)
      return;
    if ("<Set: Branch>".Equals(row.FieldName, StringComparison.OrdinalIgnoreCase))
      this.ProcessSetBranchCommand(sender, row);
    if (this.Mappings.Current != null && this.Mappings.Current.MappingType == "E")
    {
      IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
      PXSiteMap.ScreenInfo info = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
      int? viewNumber;
      if (info != null && !string.IsNullOrEmpty(row.ObjectName) && info.Containers.ContainsKey(this.GetOriginalViewNameForNewUI(info, row.ObjectName, out viewNumber)) && info.Containers[this.GetOriginalViewNameForNewUI(info, row.ObjectName, out viewNumber)].IsGrid)
        return;
    }
    this.RecalcHidden(row);
  }

  protected virtual void SYMappingField_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    SYMappingField row = (SYMappingField) e.Row;
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    if (row.ParentLineNbr.HasValue || !row.MappingID.HasValue)
      return;
    foreach (PXResult pxResult in pxSelect.Select((object) row.MappingID, (object) row.LineNbr))
      this.FieldMappings.Cache.Delete((object) (pxResult[typeof (SYMappingField)] as SYMappingField));
    this.RecalcHidden(row);
  }

  protected void RecalcHidden(SYMappingField row)
  {
    if (this._Recalculating)
      return;
    bool? isSimpleMapping = this.Mappings.Current.IsSimpleMapping;
    bool flag = true;
    if (isSimpleMapping.GetValueOrDefault() == flag & isSimpleMapping.HasValue)
      return;
    this._Recalculating = true;
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, IsNull>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect1 = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, IsNull>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, IsNotNull>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect2 = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, IsNotNull>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect3 = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, Equal<Required<SYMappingField.lineNbr>>>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    pxSelect1.View.Clear();
    pxSelect2.View.Clear();
    int num = 1;
    Dictionary<string, bool> have_key = new Dictionary<string, bool>();
    Dictionary<string, bool> have_linenbr = new Dictionary<string, bool>();
    foreach (PXResult pxResult1 in pxSelect1.Select())
    {
      SYMappingField row1 = pxResult1[typeof (SYMappingField)] as SYMappingField;
      if (!string.IsNullOrEmpty(row1.ObjectName) && !string.IsNullOrEmpty(row1.FieldName))
      {
        if (!have_key.ContainsKey(row1.ObjectName))
          have_key.Add(row1.ObjectName, false);
        if (!have_linenbr.ContainsKey(row1.ObjectName))
          have_linenbr.Add(row1.ObjectName, false);
        List<SYMappingField> newhiddenlist = new List<SYMappingField>();
        if (!row1.FieldName.StartsWith("@", StringComparison.Ordinal) && !row1.FieldName.StartsWith("##", StringComparison.Ordinal) && !row1.FieldName.StartsWith("<", StringComparison.Ordinal))
          newhiddenlist = this.CreateFieldHiddenRows(row1, have_key, have_linenbr);
        List<SYMappingField> oldhiddenlist = new List<SYMappingField>();
        foreach (PXResult pxResult2 in pxSelect3.Select((object) row1.LineNbr))
        {
          SYMappingField syMappingField = pxResult2[typeof (SYMappingField)] as SYMappingField;
          oldhiddenlist.Add(syMappingField);
        }
        if (this.CompareLists(newhiddenlist, oldhiddenlist))
        {
          foreach (object obj in pxSelect3.Select((object) row1.LineNbr))
            this.FieldMappings.Cache.Delete(obj);
          foreach (SYMappingField syMappingField in newhiddenlist)
          {
            syMappingField.OrderNumber = new int?(num++);
            this.FieldMappings.Cache.Insert((object) syMappingField);
          }
        }
        else
        {
          foreach (object obj in oldhiddenlist)
          {
            if (this.FieldMappings.Cache.Locate(obj) is SYMappingField row2)
            {
              row2.OrderNumber = new int?(num++);
              this.FieldMappings.Cache.MarkUpdated((object) row2);
            }
          }
        }
      }
      if (this.FieldMappings.Cache.Locate((object) row1) is SYMappingField row3)
      {
        row3.OrderNumber = new int?(num++);
        this.FieldMappings.Cache.MarkUpdated((object) row3);
      }
      this.FindHiddeninMain(row1);
    }
    this.FieldMappings.View.RequestRefresh();
    this._Recalculating = false;
  }

  private void FindHiddeninMain(SYMappingField row)
  {
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.orderNumber, Less<Required<SYMappingField.orderNumber>>, And<SYMappingField.parentLineNbr, IsNull>>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect1 = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.orderNumber, Less<Required<SYMappingField.orderNumber>>, And<SYMappingField.parentLineNbr, IsNull>>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, IsNotNull>>, OrderBy<Asc<SYMappingField.orderNumber>>> pxSelect2 = new PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Current<SYMapping.mappingID>>, And<SYMappingField.parentLineNbr, IsNotNull>>, OrderBy<Asc<SYMappingField.orderNumber>>>((PXGraph) this);
    List<SYMappingField> syMappingFieldList = new List<SYMappingField>();
    List<SYMappingField> source = new List<SYMappingField>();
    if (row == null || !row.MappingID.HasValue || !row.OrderNumber.HasValue)
      return;
    foreach (PXResult pxResult in pxSelect1.Select((object) row.OrderNumber))
    {
      SYMappingField syMappingField = pxResult[typeof (SYMappingField)] as SYMappingField;
      source.Add(syMappingField);
    }
    foreach (PXResult pxResult in pxSelect2.Select((object) row.LineNbr))
    {
      SYMappingField syMappingField = pxResult[typeof (SYMappingField)] as SYMappingField;
      syMappingFieldList.Add(syMappingField);
    }
    foreach (SYMappingField syMappingField1 in syMappingFieldList)
    {
      SYMappingField hiddenField = syMappingField1;
      SYMappingField syMappingField2 = source.Where<SYMappingField>((Func<SYMappingField, bool>) (m => m.FieldName != null && m.FieldName.Equals(hiddenField.FieldName, StringComparison.Ordinal) && m.ObjectName.Equals(hiddenField.ObjectName, StringComparison.Ordinal))).FirstOrDefault<SYMappingField>();
      if (syMappingField2 != null && syMappingField2.FieldName != "##")
      {
        hiddenField.ObjectName = syMappingField2.ObjectName;
        hiddenField.IsActive = syMappingField2.IsActive;
        hiddenField.Value = syMappingField2.Value;
        hiddenField.IgnoreError = syMappingField2.IgnoreError;
        this.FieldMappings.Cache.Update((object) hiddenField);
        syMappingField2.ParentLineNbr = hiddenField.ParentLineNbr;
        this.FieldMappings.Cache.Delete((object) syMappingField2);
      }
    }
  }

  private bool CompareLists(List<SYMappingField> newhiddenlist, List<SYMappingField> oldhiddenlist)
  {
    if (newhiddenlist.Count != oldhiddenlist.Count)
      return true;
    for (int index = 0; index < newhiddenlist.Count; ++index)
    {
      if (newhiddenlist[index].FieldName != oldhiddenlist[index].FieldName)
        return true;
    }
    return false;
  }

  private List<SYMappingField> CreateFieldHiddenRows(
    SYMappingField row,
    Dictionary<string, bool> have_key,
    Dictionary<string, bool> have_linenbr)
  {
    List<SYMappingField> fieldHiddenRows = new List<SYMappingField>();
    IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
    PXSiteMap.ScreenInfo info = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
    if (info == null)
      return fieldHiddenRows;
    int startIndex = row.FieldName.IndexOf("!");
    string fieldName = row.FieldName;
    if (startIndex > -1)
      fieldName = row.FieldName.Remove(startIndex, row.FieldName.Length - startIndex);
    string viewNameForNewUi = this.GetOriginalViewNameForNewUI(info, row.ObjectName, out int? _);
    if (!info.Containers.ContainsKey(viewNameForNewUi) || string.IsNullOrEmpty(viewNameForNewUi) || !info.Containers.ContainsKey(viewNameForNewUi))
      return fieldHiddenRows;
    PX.Data.Description.FieldInfo fieldInfo = info.Containers[viewNameForNewUi][fieldName];
    bool? nullable;
    if (fieldInfo != null)
    {
      string viewName = ScreenUtils.NormalizeViewName(info.Containers[viewNameForNewUi].ViewName);
      if (info.Containers[viewNameForNewUi].Parameters != null && fieldInfo.IsKey && !have_key[viewNameForNewUi])
      {
        have_key[viewNameForNewUi] = true;
        foreach (ParsInfo parameter in info.Containers[viewNameForNewUi].Parameters)
        {
          if (parameter.Type != ParType.Filters)
          {
            SYMappingField instance = (SYMappingField) this.FieldMappings.Cache.CreateInstance();
            instance.ParentLineNbr = row.LineNbr;
            instance.ObjectName = viewNameForNewUi;
            instance.FieldName = parameter.Type == ParType.Parameters ? MappingFieldNamer.CreateParameterValue(parameter.Name) : MappingFieldNamer.CreateKeyValue(parameter.Field, parameter.Name);
            instance.IsVisible = new bool?(false);
            instance.IsActive = new bool?(true);
            instance.MappingID = row.MappingID;
            instance.NeedCommit = new bool?(false);
            instance.IgnoreError = new bool?(false);
            instance.CreatedByID = row.CreatedByID;
            instance.CreatedByScreenID = row.CreatedByScreenID;
            instance.CreatedDateTime = row.CreatedDateTime;
            if (!string.IsNullOrEmpty(parameter.Field))
              instance.Value = parameter.Field.Contains<char>('.') ? MappingFieldNamer.CreateKeyOrParameterFormula(parameter.Field) : MappingFieldNamer.CreateKeyOrParameterFormula(parameter.Field, viewName);
            fieldHiddenRows.Add(instance);
          }
        }
      }
      else if (!fieldInfo.IsKey)
      {
        nullable = row.NeedSearch;
        bool flag = true;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          SYMappingField searchCommand = this.CreateSearchCommand(viewName, viewNameForNewUi, row);
          fieldHiddenRows.Add(searchCommand);
        }
      }
    }
    else
    {
      nullable = row.NeedSearch;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        SYMappingField searchCommand = this.CreateSearchCommand(ScreenUtils.NormalizeViewName(viewNameForNewUi), viewNameForNewUi, row);
        fieldHiddenRows.Add(searchCommand);
      }
    }
    nullable = row.NeedCommit;
    if (nullable.Value && fieldInfo != null && fieldInfo.Callback != null && !string.IsNullOrEmpty(fieldInfo.Callback.dsCommandName))
    {
      SYMappingField instance = (SYMappingField) this.FieldMappings.Cache.CreateInstance();
      instance.ParentLineNbr = row.LineNbr;
      instance.ObjectName = info.PrimaryView;
      instance.FieldName = $"<{fieldInfo.Callback.dsCommandName}>";
      instance.IsVisible = new bool?(false);
      instance.MappingID = row.MappingID;
      instance.NeedCommit = new bool?(false);
      instance.IsActive = new bool?(true);
      instance.IgnoreError = new bool?(false);
      instance.CreatedByID = row.CreatedByID;
      instance.CreatedByScreenID = row.CreatedByScreenID;
      instance.CreatedDateTime = row.CreatedDateTime;
      fieldHiddenRows.Add(instance);
    }
    if (info.Containers[viewNameForNewUi].HasLineNumber && !info.Containers[viewNameForNewUi].HasSearchesByKey && !have_linenbr[viewNameForNewUi])
    {
      if (this.AllowLineNumberNew)
      {
        have_linenbr[viewNameForNewUi] = true;
        SYMappingField instance = (SYMappingField) this.FieldMappings.Cache.CreateInstance();
        instance.ParentLineNbr = row.LineNbr;
        instance.ObjectName = viewNameForNewUi;
        instance.FieldName = "##";
        instance.IsActive = new bool?(true);
        instance.IsVisible = new bool?(false);
        instance.MappingID = row.MappingID;
        instance.NeedCommit = new bool?(false);
        instance.Value = "=-1";
        instance.IgnoreError = new bool?(false);
        instance.CreatedByID = row.CreatedByID;
        instance.CreatedByScreenID = row.CreatedByScreenID;
        instance.CreatedDateTime = row.CreatedDateTime;
        fieldHiddenRows.Add(instance);
      }
      else
      {
        have_linenbr[viewNameForNewUi] = true;
        SYMappingField instance = (SYMappingField) this.FieldMappings.Cache.CreateInstance();
        instance.ParentLineNbr = row.LineNbr;
        instance.ObjectName = viewNameForNewUi;
        instance.IsActive = new bool?(true);
        instance.IsVisible = new bool?(false);
        instance.MappingID = row.MappingID;
        instance.NeedCommit = new bool?(false);
        instance.FieldName = "##";
        instance.IgnoreError = new bool?(false);
        instance.CreatedByID = row.CreatedByID;
        instance.CreatedByScreenID = row.CreatedByScreenID;
        instance.CreatedDateTime = row.CreatedDateTime;
        fieldHiddenRows.Add(instance);
      }
    }
    return fieldHiddenRows;
  }

  private SYMappingField CreateSearchCommand(
    string viewName,
    string objectName,
    SYMappingField row)
  {
    SYMappingField instance = (SYMappingField) this.FieldMappings.View.Cache.CreateInstance();
    instance.ParentLineNbr = row.LineNbr;
    instance.ObjectName = objectName;
    instance.FieldName = MappingFieldNamer.CreateKeyValue(row.FieldName, (string) null);
    instance.Value = MappingFieldNamer.CreateKeyOrParameterFormula(row.FieldName, viewName);
    instance.IsVisible = new bool?(false);
    instance.IsActive = new bool?(true);
    instance.MappingID = row.MappingID;
    instance.NeedCommit = new bool?(false);
    instance.IgnoreError = new bool?(false);
    instance.CreatedByID = row.CreatedByID;
    instance.CreatedByScreenID = row.CreatedByScreenID;
    instance.CreatedDateTime = row.CreatedDateTime;
    return instance;
  }

  private void CorrectNeedToRefreshOption()
  {
    bool flag1 = PXView.Filters.OfType<PXFilterRow>().Any<PXFilterRow>((Func<PXFilterRow, bool>) (f => "IsVisible".Equals(f.DataField, StringComparison.OrdinalIgnoreCase) && f.Condition == PXCondition.EQ && f.Value is bool flag2 && flag2));
    this.WhatToShowFilter.Current.ShowHidden = new bool?(!flag1);
    this.WhatToShowFilter.Current.NeedToRefresh = new bool?(!flag1);
  }

  private void AddAllFiedsIfNeeded(PXCache cache, SYMappingField row, SYMappingField oldRow)
  {
    if (!(row.FieldName == "<Add All Fields>"))
      return;
    IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
    PXSiteMap.ScreenInfo info = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
    if (info == null || string.IsNullOrEmpty(row.ObjectName) || !info.Containers.ContainsKey(this.GetOriginalViewNameForNewUI(info, row.ObjectName, out int? _)))
      return;
    List<SYMappingField> fieldAllRows = this.GetFieldAllRows(row, info, false);
    int valueOrDefault;
    if (oldRow != null)
    {
      valueOrDefault = oldRow.OrderNumber.GetValueOrDefault();
      cache.Delete((object) oldRow);
    }
    else
      valueOrDefault = row.OrderNumber.GetValueOrDefault();
    cache.Delete((object) row);
    foreach (SYMappingField mf in fieldAllRows)
    {
      this.InsertFieldOnPosition(mf, ref valueOrDefault);
      ++valueOrDefault;
    }
    this.FieldMappings.View.RequestRefresh();
  }

  private List<SYMappingField> GetFieldAllRows(
    SYMappingField row,
    PXSiteMap.ScreenInfo info,
    bool needassigned)
  {
    List<SYMappingField> fieldAllRows = new List<SYMappingField>();
    string viewNameForNewUi = this.GetOriginalViewNameForNewUI(info, row.ObjectName, out int? _);
    if (string.IsNullOrEmpty(viewNameForNewUi) || !info.Containers.ContainsKey(viewNameForNewUi))
      return fieldAllRows;
    foreach (PX.Data.Description.FieldInfo field in info.Containers[viewNameForNewUi].Fields)
    {
      SYMappingField syMappingField1 = new SYMappingField()
      {
        ObjectName = viewNameForNewUi,
        FieldName = field.FieldName,
        IsVisible = new bool?(true),
        MappingID = row.MappingID,
        NeedCommit = new bool?(false)
      };
      fieldAllRows.Add(syMappingField1);
      if (needassigned)
      {
        foreach (KeyValuePair<string, string> assignedFieldName in this.CreateAssignedFieldNameList(viewNameForNewUi, field))
        {
          SYMappingField syMappingField2 = new SYMappingField()
          {
            ObjectName = viewNameForNewUi,
            FieldName = assignedFieldName.Key,
            IsVisible = new bool?(true),
            MappingID = row.MappingID,
            NeedCommit = new bool?(false)
          };
          fieldAllRows.Add(syMappingField2);
        }
      }
    }
    return fieldAllRows;
  }

  private Dictionary<string, string> CreateAssignedFieldNameList(string objectName, PX.Data.Description.FieldInfo field)
  {
    Dictionary<string, string> assignedFieldNameList = new Dictionary<string, string>();
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    PXView pxView = (PXView) null;
    int startIndex = objectName.IndexOf(":");
    if (startIndex > -1)
      objectName = objectName.Remove(startIndex, objectName.Length - startIndex);
    if (this.allViewFields.ContainsKey(objectName))
      pxView = this.allViewFields[objectName];
    else if (this._Graph != null && this._Graph.Views.ContainsKey(objectName))
    {
      pxView = this._Graph.Views[objectName];
      this.allViewFields[objectName] = pxView;
    }
    if (pxView != null)
    {
      PXFieldState stateExt = (PXFieldState) pxView.Cache.GetStateExt((object) null, field.FieldName);
      if (stateExt != null && stateExt.FieldList != null && stateExt.HeaderList != null)
      {
        foreach (string field1 in stateExt.FieldList)
        {
          string str = $"{field.FieldName}!{field1}";
          stringList1.Add(str);
        }
        foreach (string header in stateExt.HeaderList)
        {
          string str = $"{field.DisplayName} -> {header}";
          stringList2.Add(str);
        }
      }
      for (int index = 0; index < stringList1.Count; ++index)
        assignedFieldNameList[stringList2[index]] = stringList1[index];
    }
    for (int index = 0; index < stringList1.Count; ++index)
      assignedFieldNameList[stringList2[index]] = stringList1[index];
    return assignedFieldNameList;
  }

  protected bool IsRowKey(SYMappingField row)
  {
    if (string.IsNullOrEmpty(row.ObjectName) || string.IsNullOrEmpty(row.FieldName))
      return false;
    int startIndex = row.FieldName.IndexOf("!");
    string fieldName = row.FieldName;
    if (startIndex > -1)
      fieldName = row.FieldName.Remove(startIndex, row.FieldName.Length - startIndex);
    IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
    PXSiteMap.ScreenInfo info = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
    if (info == null)
      return false;
    string viewNameForNewUi = this.GetOriginalViewNameForNewUI(info, row.ObjectName, out int? _);
    if (string.IsNullOrEmpty(viewNameForNewUi) || !info.Containers.ContainsKey(viewNameForNewUi))
      return false;
    PX.Data.Description.FieldInfo fieldInfo = info.Containers[viewNameForNewUi][fieldName];
    return fieldInfo != null && fieldInfo.IsKey;
  }

  protected virtual bool CallbacksAutoCommit => true;

  protected virtual bool AllowLineNumberNew => true;

  public override int Persist(System.Type cacheType, PXDBOperation operation)
  {
    this.IsSiteMapAltered = this.IsSiteMapAltered | this.ScreenToSiteMapAddHelper.IsSiteMapAltered | this.Caches[typeof (PX.SM.SiteMap)].IsDirty;
    if (cacheType == typeof (SYMappingField) && operation == PXDBOperation.Insert)
      base.Persist(cacheType, PXDBOperation.Delete);
    int num = base.Persist(cacheType, operation);
    this.SelectTimeStamp();
    return num;
  }

  private void DisableRow(object row)
  {
    PXUIFieldAttribute.SetEnabled<SYMappingField.objectName>(this.FieldMappings.Cache, row, false);
    PXUIFieldAttribute.SetEnabled<SYMappingField.fieldName>(this.FieldMappings.Cache, row, false);
    PXUIFieldAttribute.SetEnabled<SYMappingField.needCommit>(this.FieldMappings.Cache, row, false);
    PXUIFieldAttribute.SetEnabled<SYMappingField.needSearch>(this.FieldMappings.Cache, row, false);
  }

  public override void Clear()
  {
    base.Clear();
    this.providerFields = (List<SYProviderField>) null;
  }

  private int CompareObjectContainers(Pair i1, Pair i2)
  {
    if (i1 == null && i2 == null)
      return 0;
    if (i1 == null)
      return -1;
    return i2 == null ? 1 : string.Compare(i1.Second as string, i2.Second as string);
  }

  private int CompareFieldInfos(PX.Data.Description.FieldInfo f1, PX.Data.Description.FieldInfo f2)
  {
    if (f1 == null && f2 == null)
      return 0;
    if (f1 == null)
      return -1;
    return f2 == null ? 1 : string.Compare(f1.DisplayName, f2.DisplayName);
  }

  protected virtual void SYImportCondition_MappingID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Mappings.Current == null)
      return;
    e.NewValue = (object) this.Mappings.Current.MappingID;
  }

  protected virtual void SYImportCondition_FieldName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (this.Mappings.Current == null || !this.Mappings.Current.ProviderID.HasValue || string.IsNullOrEmpty(this.Mappings.Current.ProviderObject))
      return;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (PXResult<SYProviderField> pxResult in PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderField.objectName, Equal<Current<SYMapping.providerObject>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>>, OrderBy<Asc<SYProviderField.providerID, Asc<SYProviderField.objectName, Asc<SYProviderField.lineNbr>>>>>.Config>.Select((PXGraph) this))
    {
      SYProviderField syProviderField = (SYProviderField) pxResult;
      stringList1.Add(syProviderField.Name);
      stringList2.Add(string.IsNullOrEmpty(syProviderField.DisplayName) ? syProviderField.Name : syProviderField.DisplayName);
    }
    PXStringListAttribute.SetList<SYImportCondition.fieldName>(this.Conditions.Cache, e.Row, stringList1.ToArray(), stringList2.ToArray());
  }

  protected virtual void SYImportCondition_Value_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    this.ImportConditionValueFieldSelecting<SYImportCondition.value>(cache, e);
  }

  protected virtual void SYImportCondition_Value2_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    this.ImportConditionValueFieldSelecting<SYImportCondition.value2>(cache, e);
  }

  private void ImportConditionValueFieldSelecting<TField>(
    PXCache cache,
    PXFieldSelectingEventArgs e)
    where TField : IBqlField
  {
    if (!(e.Row is SYImportCondition row) || row.FieldName == null)
      return;
    string dataType1 = PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderField.objectName, Equal<Current<SYMapping.providerObject>>, And<SYProviderField.name, Equal<Required<SYProviderField.name>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>>>>.Config>.Select((PXGraph) this, (object) row.FieldName).FirstTableItems.FirstOrDefault<SYProviderField>()?.DataType;
    if (string.IsNullOrEmpty(dataType1))
      return;
    if (dataType1.OrdinalEquals(typeof (System.DateTime).FullName))
    {
      string name = typeof (TField).Name;
      object obj = cache.GetValue((object) row, name);
      System.DateTime result;
      if (obj != null && System.DateTime.TryParse(obj.ToString(), out result))
        obj = (object) result;
      e.ReturnState = (object) PXDateState.CreateInstance(obj, name, new bool?(), new int?(), (string) null, (string) null, new System.DateTime?(), new System.DateTime?());
    }
    else
    {
      if (!dataType1.OrdinalEquals(typeof (bool).FullName))
        return;
      bool result;
      bool.TryParse(cache.GetValue((object) row, typeof (TField).Name)?.ToString(), out result);
      PXFieldSelectingEventArgs selectingEventArgs = e;
      // ISSUE: variable of a boxed type
      __Boxed<bool> local = (ValueType) result;
      System.Type dataType2 = typeof (bool);
      bool? nullable1 = new bool?(false);
      bool? isKey = new bool?();
      bool? nullable2 = nullable1;
      int? required = new int?();
      int? precision = new int?();
      int? length = new int?();
      bool? enabled = new bool?();
      bool? visible = new bool?();
      bool? readOnly = new bool?();
      PXFieldState instance = PXFieldState.CreateInstance((object) local, dataType2, isKey, nullable2, required, precision, length, enabled: enabled, visible: visible, readOnly: readOnly);
      selectingEventArgs.ReturnState = (object) instance;
    }
  }

  protected virtual void SYMappingCondition_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SYMappingCondition row = (SYMappingCondition) e.Row;
    if (row == null || this.Mappings.Current == null || string.IsNullOrEmpty(this.Mappings.Current.ViewName) || this._Graph == null || string.IsNullOrEmpty(row.FieldName) || row.ObjectName != null)
      return;
    PXSiteMap.ScreenInfo screenInfo = this.GetScreenInfo();
    if (screenInfo == null)
      return;
    row.ObjectName = screenInfo.PrimaryView;
  }

  protected virtual void SYMappingCondition_FieldName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    this.FieldName_OnFieldSelecting<SYMappingCondition.fieldName>((ISYObjectField) (e.Row as SYMappingCondition), cache, true, false, true, true);
  }

  protected virtual void SYMappingCondition_FieldName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SYMappingCondition row = (SYMappingCondition) e.Row;
    if (row == null)
      return;
    row.Value = (string) null;
    row.Value2 = (string) null;
  }

  protected virtual void SYMappingCondition_ObjectName_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    this.ObjectName_OnFieldSelecting<SYMappingCondition.objectName>((ISYObjectField) (e.Row as SYMappingCondition), cache, true);
  }

  protected virtual void MatchingFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    bool secondValue)
  {
    SYMappingCondition row = (SYMappingCondition) e.Row;
    if (row == null || this._Graph == null || this.Mappings.Current == null || string.IsNullOrEmpty(this.Mappings.Current.ViewName) || string.IsNullOrEmpty(row.FieldName) || !(this._Graph.GetStateExt(this.Mappings.Current.ViewName, (object) null, row.FieldName) is PXFieldState stateExt))
      return;
    if (!string.IsNullOrEmpty(stateExt.ViewName))
    {
      PXView view = this._Graph.Views[stateExt.ViewName];
      stateExt.ViewName = "$Outer$" + stateExt.ViewName;
      this.Views[stateExt.ViewName] = view;
      if (stateExt.DataType == typeof (string))
        stateExt.DescriptionName = (string) null;
    }
    stateExt.Value = e.ReturnValue;
    stateExt.Enabled = true;
    if (stateExt.DataType == typeof (bool) && stateExt.Value == null)
    {
      if (secondValue)
        row.Value2 = "False";
      else
        row.Value = "False";
    }
    if (stateExt is PXStringState pxStringState && pxStringState.AllowedLabels != null && pxStringState.AllowedLabels.Length != 0)
    {
      for (int index1 = pxStringState.AllowedLabels.Length - 1; index1 >= 0; --index1)
      {
        for (int index2 = index1 - 1; index2 >= 0; --index2)
        {
          if (pxStringState.AllowedLabels[index1] == pxStringState.AllowedLabels[index2])
          {
            pxStringState.AllowedLabels[index1] = $"{PXMessages.Localize("Explicit")} - {pxStringState.AllowedLabels[index1]}";
            break;
          }
        }
      }
    }
    e.ReturnState = (object) stateExt;
    e.Cancel = true;
  }

  protected virtual void SYMappingCondition_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    this.MatchingFieldSelecting(sender, e, false);
  }

  protected virtual void SYMappingCondition_Value2_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    this.MatchingFieldSelecting(sender, e, true);
  }

  protected virtual PXGraph CreateGraph(string graphName)
  {
    System.Type type1 = PXBuildManager.GetType(graphName, false);
    if (type1 == (System.Type) null)
      type1 = System.Type.GetType(graphName);
    if (!(type1 != (System.Type) null))
      return (PXGraph) null;
    System.Type type2 = PXBuildManager.GetType(CustomizedTypeManager.GetCustomizedTypeFullName(type1), false);
    if ((object) type2 == null)
      type2 = type1;
    System.Type graphType = type2;
    using (new PXPreserveScope())
    {
      try
      {
        return PXGraph.CreateInstance(graphType);
      }
      catch (TargetInvocationException ex)
      {
        throw PXException.ExtractInner((Exception) ex);
      }
    }
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    IScreenInfoProvider screenInfoProvider = this.ScreenInfoProvider;
    PXSiteMap.ScreenInfo info = screenInfoProvider != null ? screenInfoProvider.TryGet(this.Mappings.Current.ScreenID) : (PXSiteMap.ScreenInfo) null;
    if (info == null)
      return false;
    this.isImporting = true;
    if (this.objectNames == null)
      this.objectNames = this.CreateObjectNameList(new List<string>(), new List<string>(), info);
    string matchingValue1 = this.FindMatchingValue(this.objectNames, (Dictionary<string, string>) null, (string) values[(object) "ObjectName"]);
    Dictionary<string, string> valuesByObsoleteLabels;
    Dictionary<string, string> fieldNameList = this.CreateFieldNameList(matchingValue1, new List<string>(), new List<string>(), info, false, true, false, out valuesByObsoleteLabels);
    string label = (string) values[(object) "FieldName"];
    string matchingValue2 = this.FindMatchingValue(fieldNameList, valuesByObsoleteLabels, label);
    string matchingValue3 = this.FindMatchingValue(this.CreateValueList(new SYMappingField()
    {
      ObjectName = matchingValue1,
      FieldName = matchingValue2
    }, new List<string>(), new List<string>()), (Dictionary<string, string>) null, (string) values[(object) "Value"]);
    string str = string.IsNullOrEmpty(matchingValue3) ? (string) null : matchingValue3;
    values[(object) "ObjectName"] = (object) matchingValue1;
    values[(object) "FieldName"] = (object) matchingValue2;
    values[(object) "Value"] = (object) str;
    this.isImporting = false;
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items) => this.isImporting = false;

  private string FindMatchingValue(
    Dictionary<string, string> dict,
    Dictionary<string, string> valuesByObsoleteLabels,
    string label)
  {
    string str;
    return !string.IsNullOrEmpty(label) && (dict.TryGetValue(label.ToLower(), out str) || valuesByObsoleteLabels != null && valuesByObsoleteLabels.TryGetValue(label.ToLower(), out str)) ? str : label;
  }

  public static string[] GetAvailableMappings(string screenID)
  {
    Dictionary<string, string[]> slot = PXDatabase.GetSlot<Dictionary<string, string[]>>("AvailableMappingsKey", typeof (SYMapping));
    string[] availableMappings = new string[0];
    bool flag = false;
    lock (((ICollection) slot).SyncRoot)
      flag = screenID != null && (!slot.TryGetValue(screenID, out availableMappings) || availableMappings == null);
    if (flag)
    {
      availableMappings = SYMappingMaint<T>.InternalGetAvailableMappings(screenID);
      lock (((ICollection) slot).SyncRoot)
        slot[screenID] = availableMappings;
    }
    return availableMappings;
  }

  private static string[] InternalGetAvailableMappings(string screenID)
  {
    List<string> stringList = new List<string>();
    if (string.IsNullOrEmpty(screenID) || typeof (T) == typeof (SYMapping.mappingType.typeImport) && !PXAccess.VerifyRights(typeof (SYImportProcessSingle)) || typeof (T) == typeof (SYMapping.mappingType.typeExport) && !PXAccess.VerifyRights(typeof (SYExportProcessSingle)))
      return stringList.ToArray();
    screenID = screenID.Replace(".", "");
    foreach (SYMapping mapping in PXSYMappingSelector.GetMappings<SYMapping>(new PXView(new PXGraph(), true, PXSelectBase<SYMapping, PXSelect<SYMapping, Where<SYMapping.mappingType, Equal<T>, And<SYMapping.isActive, Equal<PX.Data.True>, And<SYMapping.screenID, Equal<Required<SYMapping.screenID>>>>>>.Config>.GetCommand()), (object) screenID))
      stringList.Add(mapping.Name);
    return stringList.ToArray();
  }

  public string[] GetInternalFields()
  {
    SYMappingMaint<T> syMappingMaint = this;
    if (syMappingMaint.Mappings.Current == null || string.IsNullOrEmpty(syMappingMaint.Mappings.Current.ScreenID))
      return Array.Empty<string>();
    PXSiteMap.ScreenInfo info = ScreenUtils.ScreenInfo.TryGet(syMappingMaint.Mappings.Current.ScreenID);
    return info == null ? Array.Empty<string>() : info.Containers.Select(c => new
    {
      container = c,
      viewName = c.Key.Split(new string[1]{ ": " }, StringSplitOptions.None)[0]
    }).SelectMany(t => this.GetFieldNames(t.container.Key, info), (t, field) => $"[{t.viewName}.{field}]").Distinct<string>().ToArray<string>();
  }

  private IEnumerable<string> GetFieldNames(string key, PXSiteMap.ScreenInfo info)
  {
    PX.Data.Description.FieldInfo[] fieldInfoArray = info.Containers[key].Fields;
    int index;
    for (index = 0; index < fieldInfoArray.Length; ++index)
      yield return fieldInfoArray[index].FieldName;
    fieldInfoArray = (PX.Data.Description.FieldInfo[]) null;
    if (key == info.PrimaryView)
    {
      Tuple<PXFieldState, short, short, string>[] tupleArray = KeyValueHelper.GetAttributeFields(this.Mappings.Current.ScreenID);
      for (index = 0; index < tupleArray.Length; ++index)
        yield return tupleArray[index].Item1.Name;
      tupleArray = (Tuple<PXFieldState, short, short, string>[]) null;
    }
  }

  public string[] GetExternalFields()
  {
    return PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderField.objectName, Equal<Current<SYMapping.providerObject>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>>, OrderBy<Asc<SYProviderField.displayName>>>.Config>.Select((PXGraph) this).Select<PXResult<SYProviderField>, string>((Expression<Func<PXResult<SYProviderField>, string>>) (field => "[" + ((SYProviderField) field).Name + "]")).ToArray<string>();
  }
}
