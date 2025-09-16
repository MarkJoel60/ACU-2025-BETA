// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GenericInquiryDesigner
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Models;
using PX.Caching;
using PX.Common;
using PX.Data.Automation.Services;
using PX.Data.BQL;
using PX.Data.DependencyInjection;
using PX.Data.Description.GI;
using PX.Data.GenericInquiry;
using PX.Data.GenericInquiry.Description;
using PX.Data.GenericInquiry.Services.ValidationServices.Interface;
using PX.Data.ReferentialIntegrity.DacRelations;
using PX.Data.SQLTree;
using PX.Data.WorkflowAPI;
using PX.DbServices.QueryObjectModel;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Compilation;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
/// <exclude />
public class GenericInquiryDesigner : 
  PXGraph<
  #nullable disable
  GenericInquiryDesigner, GIDesign>,
  IGraphWithInitialization,
  ICanAlterSiteMap
{
  [PXCopyPasteHiddenView]
  public PXFilter<GenericInquiryDesigner.CustomMessageDialogParams> OverwriteExistingOnImportDialog;
  public PXSelect<GITable, Where<GITable.designID, Equal<Current<GIDesign.designID>>>, OrderBy<Asc<GITable.name>>> Tables;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (GIDesign.replacePrimaryScreen), typeof (GIDesign.designID)})]
  public PXSelect<GIDesign> Designs;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (GIDesign.replacePrimaryScreen), typeof (GIDesign.designID)})]
  public PXSelect<GIDesign, Where<GIDesign.designID, Equal<Current<GIDesign.designID>>>> CurrentDesign;
  public PXSelect<GIRelation, Where<GIRelation.designID, Equal<Current<GIDesign.designID>>>> Relations;
  public PXSelect<GIOn, Where<GIOn.designID, Equal<Current<GIDesign.designID>>, And<GIOn.relationNbr, Equal<Current<GIRelation.lineNbr>>>>> JoinConditions;
  public PXSelect<GIOn, Where<GIOn.designID, Equal<Current<GIDesign.designID>>, And<GIOn.relationNbr, Equal<Required<GIOn.lineNbr>>>>> RequiredJoinConditions;
  public PXSelect<GIGroupBy, Where<GIGroupBy.designID, Equal<Current<GIDesign.designID>>>> GroupBy;
  public PXSelect<GIWhere, Where<GIWhere.designID, Equal<Current<GIDesign.designID>>>> Wheres;
  public PXSelect<GISort, Where<GISort.designID, Equal<Current<GIDesign.designID>>>> Sortings;
  public PXSelect<GIFilter, Where<GIFilter.designID, Equal<Current<GIDesign.designID>>>> Parameters;
  public PXOrderedSelect<GIDesign, GIResult, Where<GIResult.designID, Equal<Current<GIDesign.designID>>>, OrderBy<Asc<GIResult.sortOrder, Asc<GIResult.lineNbr>>>> Results;
  [PXCopyPasteHiddenView]
  public PXSelect<ComboValues> ValuesLabels;
  [PXCopyPasteHiddenView]
  public PXSelect<GenericInquiryDesigner.NestedFilterHeader, Where<FilterHeader.screenID, Equal<Required<FilterHeader.screenID>>>> FilterHeaders;
  [PXCopyPasteHiddenView]
  public PXSelect<GenericInquiryDesigner.NestedFilterRow, Where<FilterRow.filterID, Equal<Current<FilterRow.filterID>>>> FilterRows;
  [PXCopyPasteHiddenView]
  public PXFilter<RelatedTable> RelatedTables;
  [PXCopyPasteHiddenView]
  public PXSelect<LinkedTable> TablesInformation;
  public PXFilter<NewFilter> NewParPanel;
  public PXAction<GIDesign> ViewInquiry;
  public PXAction<GIDesign> PreviewInquiry;
  public PXAction<GIDesign> MoveUpFilter;
  public PXAction<GIDesign> MoveDownFilter;
  public PXAction<GIDesign> MoveUpSortings;
  public PXAction<GIDesign> MoveDownSortings;
  public PXAction<GIDesign> MoveUpCondition;
  public PXAction<GIDesign> MoveDownCondition;
  public PXAction<GIDesign> MoveUpRelations;
  public PXAction<GIDesign> MoveDownRelations;
  public PXAction<GIDesign> MoveUpGroupBy;
  public PXAction<GIDesign> MoveDownGroupBy;
  public PXAction<GIDesign> MoveUpOn;
  public PXAction<GIDesign> MoveDownOn;
  public PXAction<GIDesign> ShowAvailableValues;
  public PXAction<GIDesign> AddRelatedTable;
  public PXAction<GIDesign> AddRelatedTableRelations;
  public PXAction<GIDesign> BrowseForRelation;
  public PXAction<GIDesign> SelectParentTable;
  public PXAction<GIDesign> SelectChildTable;
  public PXAction<GIDesign> NavigateToDataSource;
  public PXChangeID<GIDesign, GIDesign.name> ChangeName;
  private bool BypassInsertedHandler;
  private Dictionary<string, GITable> _allTables;
  private readonly PXGIFormulaProcessor _formulaProcessor = new PXGIFormulaProcessor();
  private const char SubGIFieldDelimiter = '_';
  private Dictionary<Guid, HashSet<string>> primaryViewsDict = new Dictionary<Guid, HashSet<string>>();
  public PXAction<GIDesign> replace;
  public PXSelect<GIDesign> UploadDialogPanel;
  private List<string> views = new List<string>();
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Data.ListEntryPoint, Where<PX.Data.ListEntryPoint.entryScreenID, Equal<Required<PX.Data.ListEntryPoint.entryScreenID>>>> ListEntryPointSameEntry;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Data.ListEntryPoint, Where<PX.Data.ListEntryPoint.listScreenID, Equal<Required<PX.Data.ListEntryPoint.listScreenID>>>> ListEntryPointSameList;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Data.ListEntryPoint, Where<PX.Data.ListEntryPoint.entryScreenID, Equal<Required<PX.Data.ListEntryPoint.entryScreenID>>, PX.Data.And<Where<PX.Data.ListEntryPoint.listScreenID, Equal<Required<PX.Data.ListEntryPoint.listScreenID>>, Or<PX.Data.ListEntryPoint.listScreenID, PX.Data.IsNull>>>>> ListEntryPoint;
  private const char ParameterNameAliasLabelDelimiter = '.';
  public PXSelect<GITable, Where<GITable.designID, Equal<Current<GIDesign.designID>>, And<GITable.name, Equal<Required<GITable.name>>>>> PrimaryScreenInqTable;
  public PXSelect<GIRecordDefault, Where<GIRecordDefault.designID, Equal<Current<GIDesign.designID>>>> RecordDefaults;
  public PXSelect<GIMassUpdateField, Where<GIMassUpdateField.designID, Equal<Current<GIDesign.designID>>>> MassUpdateFields;
  public PXSelect<GIMassAction, Where<GIMassAction.designID, Equal<Current<GIDesign.designID>>>> MassActions;
  public NavigationScreensSelect NavigationScreens;
  public PXSelect<GINavigationScreen, Where<GINavigationScreen.designID, Equal<Current<GIDesign.designID>>, And<GINavigationScreen.lineNbr, Equal<Current<GINavigationScreen.lineNbr>>>>> CurrentNavigationScreen;
  public PXSelect<GINavigationScreen, Where<GINavigationScreen.designID, Equal<Current<GIDesign.designID>>, And<GINavigationScreen.lineNbr, Equal<Current<GINavigationScreen.lineNbr>>>>> SidePanelSettings;
  public PXSelect<GINavigationScreen, Where<GINavigationScreen.designID, Equal<Current<GIDesign.designID>>, And<GINavigationScreen.windowMode, NotEqual<PXWindowModeAttribute.layer>>>, OrderBy<Asc<GINavigationScreen.sortOrder, Asc<GINavigationScreen.lineNbr>>>> NavigationScreensForColumns;
  public PXSelect<GINavigationScreen, Where<GINavigationScreen.designID, Equal<Current<GIDesign.designID>>, And<GINavigationScreen.link, Equal<Required<GINavigationScreen.link>>>>> SpecificNavigationScreen;
  public PXSelect<GINavigationParameter, Where<GINavigationParameter.designID, Equal<Current<GIDesign.designID>>, And<GINavigationParameter.navigationScreenLineNbr, Equal<Current<GINavigationScreen.lineNbr>>>>> NavigationParameters;
  public PXSelect<GIResult, Where<GIResult.designID, Equal<Current<GIDesign.designID>>, And<GIResult.navigationNbr, Equal<Required<GIResult.navigationNbr>>>>> ResultsByNavigationScreen;
  public PXSelect<GINavigationParameter, Where<GINavigationParameter.designID, Equal<Current<GIDesign.designID>>, And<GINavigationParameter.navigationScreenLineNbr, Equal<Required<GINavigationParameter.navigationScreenLineNbr>>>>> ScreensNavigationParameters;
  public PXSelect<GINavigationCondition, Where<GINavigationCondition.designID, Equal<Current<GIDesign.designID>>, And<GINavigationCondition.navigationScreenLineNbr, Equal<Current<GINavigationScreen.lineNbr>>>>> NavigationConditions;
  public PXSelect<GINavigationCondition, Where<GINavigationCondition.designID, Equal<Current<GIDesign.designID>>, And<GINavigationCondition.navigationScreenLineNbr, Equal<Required<GINavigationCondition.navigationScreenLineNbr>>>>> ScreensNavigationConditions;
  [PXCopyPasteHiddenView]
  public PXFilter<GenericInquiryDesigner.CustomMessageDialogParams> CreateCopyOrOverwriteGIDialog;
  [PXCopyPasteHiddenView]
  public PXFilter<GenericInquiryDesigner.CustomMessageDialogParams> DeleteUsedGIDialog;
  [PXCopyPasteHiddenView]
  public PXFilter<GenericInquiryDesigner.CustomMessageDialogParams> DeleteUsedFieldDialog;
  private bool _additionalResultsRefreshRequired;
  private static readonly Regex EndsWithDigit = new Regex(".+-\\d+", RegexOptions.Compiled);

  private void InitializeCopyPaste()
  {
    this.CopyPaste.AddScreenIDAndUrlUniquenessImportValidation<GIDesign>("A site map node with the Screen ID = '{0}' already exists. Please change its screen ID before the importing of this Generic Inquiry.").ImportXMLPostProcessor = (System.Action) (() =>
    {
      this.IsSiteMapAltered = true;
      this.PageCacheControl.InvalidateCache();
      PXGenericInqGrph.ResetDefinitions();
    });
  }

  public override void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    base.CopyPasteGetScript(isImportSimple, script, containers);
    GenericInquiryDesigner.CommitJoinBrackets(script);
  }

  /// <summary>Sets open bracket for join conditions to be committed</summary>
  private static void CommitJoinBrackets(List<Command> script)
  {
    EnumerableExtensions.ForEach<Command>(script.Where<Command>((Func<Command, bool>) (x => x.ObjectName == "JoinConditions" && x.FieldName == "OpenBrackets")), (System.Action<Command>) (x => x.Commit = true));
  }

  internal override YaqlCondition GetFilterConditionForAdditionalRecordsForExport(
    Dictionary<string, object> keys)
  {
    Guid? designId = (Guid?) this.CurrentDesign?.Current?.DesignID;
    object obj;
    if (designId.HasValue && keys.Count == 1 && keys.TryGetValue("Name", out obj) && obj is string a)
    {
      string name = this.CurrentDesign?.Current?.Name;
      if (string.Equals(a, name, StringComparison.OrdinalIgnoreCase))
      {
        List<(string, Guid)> list = this.GenericInquiryReferenceInfoProvider.GetReferencesFrom(designId.Value).ToList<(string, Guid)>();
        return !list.Any<(string, Guid)>() ? (YaqlCondition) null : Yaql.or(list.Select<(string, Guid), YaqlCondition>((Func<(string, Guid), YaqlCondition>) (x => Yaql.eq<string>((YaqlScalar) Yaql.column("Name", (string) null), x.Name))));
      }
    }
    return (YaqlCondition) null;
  }

  public override object GetValueExt(string viewName, object data, string fieldName)
  {
    return this.ClearListsWhenCopyPasteContext(base.GetValueExt(viewName, data, fieldName));
  }

  public override object GetStateExt(string viewName, object data, string fieldName)
  {
    return this.ClearListsWhenCopyPasteContext(base.GetStateExt(viewName, data, fieldName));
  }

  private object ClearListsWhenCopyPasteContext(object state)
  {
    return !this.IsCopyPasteContext || !(state is PXStringState state1) || state.GetType() != typeof (PXStringState) || state1.Value == null || !state1.HasFixedValuesList() ? state : (object) PXStringState.CreateInstance(state1.Value, new int?(state1.Length), new bool?(state1.IsUnicode), state1.Name, new bool?(state1._IsKey), GetRequiredValue(state1.Required), state1.InputMask, (string[]) null, (string[]) null, new bool?(state1.ExclusiveValues), state1.DefaultValue?.ToString());

    static int? GetRequiredValue(bool? boolValue)
    {
      return !boolValue.HasValue ? new int?() : (boolValue.GetValueOrDefault() ? new int?(1) : new int?(-1));
    }
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(Events.CacheAttached<GIDesign.rowStyleFormula> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(Events.CacheAttached<GIGroupBy.dataFieldName> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(
    Events.CacheAttached<GINavigationCondition.valueSt> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(
    Events.CacheAttached<GINavigationCondition.valueSt2> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(Events.CacheAttached<GIWhere.value1> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(Events.CacheAttached<GIWhere.value2> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(Events.CacheAttached<GIResult.field> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(Events.CacheAttached<GIResult.styleFormula> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddParameters]
  protected virtual void _(Events.CacheAttached<GISort.dataFieldName> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddRelationFields]
  protected virtual void _(Events.CacheAttached<GIOn.parentField> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddRelationFields]
  protected virtual void _(Events.CacheAttached<GIOn.childField> e)
  {
  }

  [PXMergeAttributes]
  [GenericInquiryDesigner.PXFormulaEditor_AddNavigationParameterFields]
  protected virtual void _(
    Events.CacheAttached<GINavigationParameter.parameterName> e)
  {
  }

  public bool IsSiteMapAltered { get; internal set; }

  [PXInternalUseOnly]
  public PXFieldScreenToSiteMapAddHelper<GIDesign> ScreenToSiteMapAddHelper { get; private set; }

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  protected IScreenInfoProvider ScreenInfoProvider { get; set; }

  [InjectDependency]
  protected IDacRegistry DacRegistry { get; set; }

  [InjectDependency]
  protected IPXPageIndexingService PageIndexingService { get; set; }

  [InjectDependency]
  private IGenericInquiryDescriptionProvider GenericInquiryDescriptionProvider { get; set; }

  [InjectDependency]
  private IGenericInquiryReferenceInfoProvider GenericInquiryReferenceInfoProvider { get; set; }

  [InjectDependency]
  private IGenericInquiryValidationService GenericInquiryValidationService { get; set; }

  public GenericInquiryDesigner()
  {
    PXUIFieldAttribute.SetVisible<GIFilter.availableValues>(this.Caches[typeof (GIFilter)], (object) null, PXGraph.ProxyIsActive);
    PXUIFieldAttribute.SetVisibility<GIFilter.availableValues>(this.Caches[typeof (GIFilter)], (object) null, PXGraph.ProxyIsActive ? PXUIVisibility.Dynamic : PXUIVisibility.Invisible);
    this.JoinConditions.Cache.AllowInsert = false;
    this.InitializeCopyPaste();
    PXSiteMapNodeSelectorAttribute.SetRestriction<GIDesign.primaryScreenID>(this.CurrentDesign.Cache, (object) null, (Func<PX.SM.SiteMap, bool>) (s => !this.IsEntryScreen(s) && !PXSiteMap.IsPortal));
    ChildTableFilterAttribute.SetFunction<RelatedTable.childTable>(this.RelatedTables.Cache, (object) null, (Func<IEnumerable>) (() => this.FindRelatedTables()));
    ParentTableFilterAttribute.SetFunction<RelatedTable.parentTable>(this.RelatedTables.Cache, (object) null, (Func<IEnumerable>) (() => this.FindParentTables()));
    this.SidePanelActions.Add(new PXGraph.SidePanelAction(nameof (PreviewInquiry), "visibility", PXLocalizer.Localize("Preview the currently selected inquiry.")));
    this.ChangeName.SetCaption("Change Inquiry Title");
    this.ChangeName.SetDuplicateKeyMessage("The '{0}' title is already used for another generic inquiry.");
    this.ChangeName.SetCategory("Actions");
    this.NavigationConditions.AllowSelect = false;
  }

  public void Initialize()
  {
    IEnumerable<string> urlPrefixes = GenericInquiryDesigner.GetUrlPrefixes();
    this.ScreenToSiteMapAddHelper = new PXFieldScreenToSiteMapAddHelper<GIDesign>((PXGraph) this, this.ScreenInfoCacheControl, PXSiteMap.IsPortal ? "GIP" : "GI", urlPrefixes, typeof (GIDesign.name), typeof (GIDesign.sitemapTitle), typeof (GIDesign.sitemapScreenID), new PXFieldScreenToSiteMapAddHelper<GIDesign>.Field[2][]
    {
      (PXFieldScreenToSiteMapAddHelper<GIDesign>.Field[]) new PXFieldScreenToSiteMapAddHelper<GIDesign>.Field<GIDesign.designID>[1]
      {
        new PXFieldScreenToSiteMapAddHelper<GIDesign>.Field<GIDesign.designID>("id")
      },
      (PXFieldScreenToSiteMapAddHelper<GIDesign>.Field[]) new PXFieldScreenToSiteMapAddHelper<GIDesign>.Field<GIDesign.name>[1]
      {
        new PXFieldScreenToSiteMapAddHelper<GIDesign>.Field<GIDesign.name>()
      }
    });
  }

  private static IEnumerable<string> GetUrlPrefixes()
  {
    return !(SitePolicy.DefaultUI == "T") ? (IEnumerable<string>) new \u003C\u003Ez__ReadOnlyArray<string>(new string[2]
    {
      "~/Scripts/Screens/GenericInquiry.html",
      "~/GenericInquiry/GenericInquiry.aspx"
    }) : (IEnumerable<string>) new \u003C\u003Ez__ReadOnlyArray<string>(new string[2]
    {
      "~/GenericInquiry/GenericInquiry.aspx",
      "~/Scripts/Screens/GenericInquiry.html"
    });
  }

  protected override PXCacheCollection CreateCacheCollection()
  {
    return (PXCacheCollection) new PXCacheUniqueForTypeCollection((PXGraph) this);
  }

  private bool IsEntryScreen(PX.SM.SiteMap sm)
  {
    GIDesign current = this.CurrentDesign.Current;
    if (PXSiteMap.IsDashboard(sm.Url))
      return false;
    if (current != null)
    {
      Guid? nodeId = sm.NodeID;
      Guid? designId = current.DesignID;
      if ((nodeId.HasValue == designId.HasValue ? (nodeId.HasValue ? (nodeId.GetValueOrDefault() == designId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 || string.Equals(sm.ScreenID, current.SitemapScreenID, StringComparison.OrdinalIgnoreCase))
        return false;
    }
    if (PXSiteMap.IsGenericInquiry(sm.Url))
      return true;
    if (string.IsNullOrEmpty(sm.Graphtype))
      return false;
    string primaryView = this.PageIndexingService.GetPrimaryView(sm.Graphtype);
    if (string.IsNullOrEmpty(primaryView))
      return false;
    PXViewInfo graphView = GraphHelper.GetGraphView(sm.Graphtype, primaryView);
    if (graphView == null)
      return false;
    HashSet<string> stringSet;
    if (current != null)
    {
      Guid? nullable1 = current.DesignID;
      if (nullable1.HasValue)
      {
        Guid? nullable2;
        if (current == null)
        {
          nullable1 = new Guid?();
          nullable2 = nullable1;
        }
        else
          nullable2 = current.DesignID;
        nullable1 = nullable2;
        Guid key = nullable1.Value;
        if (!this.primaryViewsDict.TryGetValue(key, out stringSet))
        {
          stringSet = new HashSet<string>(this.Tables.Select().AsEnumerable<PXResult<GITable>>().Select<PXResult<GITable>, string>((Func<PXResult<GITable>, string>) (t => ((GITable) t).Name)));
          this.primaryViewsDict[key] = stringSet;
          goto label_21;
        }
        goto label_21;
      }
    }
    stringSet = new HashSet<string>(this.Tables.Select().AsEnumerable<PXResult<GITable>>().Select<PXResult<GITable>, string>((Func<PXResult<GITable>, string>) (t => ((GITable) t).Name)));
label_21:
    string name = graphView.Cache.Name;
    return stringSet.Contains(name);
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected IEnumerable Replace(PXAdapter adapter)
  {
    this.Clear();
    foreach (object obj in adapter.Get())
      adapter.View.Cache.Delete(obj);
    this.Actions.PressSave();
    return this.Cancel.Press(adapter);
  }

  [PXButton(Category = "Actions", Tooltip = "Open the currently selected inquiry (Ctrl+M).", DisplayOnMainToolbar = true, ConfirmationType = PXConfirmationType.IfDirty, ConfirmationMessage = "Any unsaved changes will be discarded. Do you want to proceed?", ShortcutCtrl = true, ShortcutChar = 'M')]
  [PXUIField(DisplayName = "View Inquiry")]
  protected void viewInquiry()
  {
    if (this.Designs.Current != null && this.Designs.Current.Name != null)
      throw new PXRedirectToUrlException(this.ScreenToSiteMapAddHelper.BuildUrl(this.Designs.Current), PXBaseRedirectException.WindowMode.New, "ViewInquiry");
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected void previewInquiry()
  {
    if (this.Designs.Current?.Name != null)
    {
      PXRedirectToUrlException redirectToUrlException = new PXRedirectToUrlException(this.ScreenToSiteMapAddHelper.BuildUrl(this.Designs.Current), PXBaseRedirectException.WindowMode.Layer, string.Empty);
      redirectToUrlException.Filters.Add(new PXBaseRedirectException.Filter("Results"));
      throw redirectToUrlException;
    }
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveUpFilter()
  {
    if (this.Parameters.Current == null)
      return;
    GIFilter first = (GIFilter) PXSelectBase<GIFilter, PXSelect<GIFilter, Where<GIFilter.designID, Equal<Current<GIDesign.designID>>, And<GIFilter.lineNbr, Less<Current<GIFilter.lineNbr>>>>, OrderBy<Desc<GIFilter.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Parameters.Cache, (object) first, (object) this.Parameters.Current);
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveDownFilter()
  {
    if (this.Parameters.Current == null)
      return;
    GIFilter first = (GIFilter) PXSelectBase<GIFilter, PXSelect<GIFilter, Where<GIFilter.designID, Equal<Current<GIDesign.designID>>, And<GIFilter.lineNbr, Greater<Current<GIFilter.lineNbr>>>>, OrderBy<Asc<GIFilter.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Parameters.Cache, (object) first, (object) this.Parameters.Current);
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveUpSortings()
  {
    if (this.Sortings.Current == null)
      return;
    GISort first = (GISort) PXSelectBase<GISort, PXSelect<GISort, Where<GISort.designID, Equal<Current<GIDesign.designID>>, And<GISort.lineNbr, Less<Current<GISort.lineNbr>>>>, OrderBy<Desc<GISort.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Sortings.Cache, (object) first, (object) this.Sortings.Current);
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveDownSortings()
  {
    if (this.Sortings.Current == null)
      return;
    GISort first = (GISort) PXSelectBase<GISort, PXSelect<GISort, Where<GISort.designID, Equal<Current<GIDesign.designID>>, And<GISort.lineNbr, Greater<Current<GISort.lineNbr>>>>, OrderBy<Asc<GISort.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Sortings.Cache, (object) first, (object) this.Sortings.Current);
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveUpCondition()
  {
    if (this.Wheres.Current == null)
      return;
    GIWhere first = (GIWhere) PXSelectBase<GIWhere, PXSelect<GIWhere, Where<GIWhere.designID, Equal<Current<GIDesign.designID>>, And<GIWhere.lineNbr, Less<Current<GIWhere.lineNbr>>>>, OrderBy<Desc<GIWhere.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Wheres.Cache, (object) first, (object) this.Wheres.Current);
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveDownCondition()
  {
    if (this.Wheres.Current == null)
      return;
    GIWhere first = (GIWhere) PXSelectBase<GIWhere, PXSelect<GIWhere, Where<GIWhere.designID, Equal<Current<GIDesign.designID>>, And<GIWhere.lineNbr, Greater<Current<GIWhere.lineNbr>>>>, OrderBy<Asc<GIWhere.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Wheres.Cache, (object) first, (object) this.Wheres.Current);
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveUpRelations()
  {
    if (this.Relations.Current == null)
      return;
    GIRelation first = (GIRelation) PXSelectBase<GIRelation, PXSelect<GIRelation, Where<GIRelation.designID, Equal<Current<GIDesign.designID>>, And<GIRelation.lineNbr, Less<Current<GIRelation.lineNbr>>>>, OrderBy<Desc<GIRelation.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Relations.Cache, (object) first, (object) this.Relations.Current);
    this.SwapJoinConditions(first.LineNbr.Value);
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveDownRelations()
  {
    if (this.Relations.Current == null)
      return;
    GIRelation first = (GIRelation) PXSelectBase<GIRelation, PXSelect<GIRelation, Where<GIRelation.designID, Equal<Current<GIDesign.designID>>, And<GIRelation.lineNbr, Greater<Current<GIRelation.lineNbr>>>>, OrderBy<Asc<GIRelation.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.Relations.Cache, (object) first, (object) this.Relations.Current);
    this.SwapJoinConditions(first.LineNbr.Value);
  }

  private void SwapJoinConditions(int lineNbr)
  {
    GIOn[] array1 = this.RequiredJoinConditions.Select((object) lineNbr).Select<PXResult<GIOn>, GIOn>((Expression<Func<PXResult<GIOn>, GIOn>>) (r => (GIOn) this.JoinConditions.Cache.CreateCopy((GIOn) r))).ToArray<GIOn>();
    GIOn[] array2 = this.RequiredJoinConditions.Select((object) this.Relations.Current.LineNbr).Select<PXResult<GIOn>, GIOn>((Expression<Func<PXResult<GIOn>, GIOn>>) (r => (GIOn) this.JoinConditions.Cache.CreateCopy((GIOn) r))).ToArray<GIOn>();
    foreach (GIOn giOn in ((IEnumerable<GIOn>) array1).Concat<GIOn>((IEnumerable<GIOn>) array2).ToArray<GIOn>())
    {
      this.JoinConditions.Delete(giOn);
      giOn.LineNbr = new int?();
      giOn.NoteID = new Guid?();
      giOn.RelationNbr = new int?();
    }
    foreach (GIOn giOn in array1)
    {
      giOn.RelationNbr = this.Relations.Current.LineNbr;
      this.JoinConditions.Insert(giOn);
    }
    foreach (GIOn giOn in array2)
    {
      giOn.RelationNbr = new int?(lineNbr);
      this.JoinConditions.Insert(giOn);
    }
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveUpGroupBy()
  {
    if (this.GroupBy.Current == null)
      return;
    GIGroupBy first = (GIGroupBy) PXSelectBase<GIGroupBy, PXSelect<GIGroupBy, Where<GIGroupBy.designID, Equal<Current<GIDesign.designID>>, And<GIGroupBy.lineNbr, Less<Current<GIGroupBy.lineNbr>>>>, OrderBy<Desc<GIGroupBy.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.GroupBy.Cache, (object) first, (object) this.GroupBy.Current);
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveDownGroupBy()
  {
    if (this.GroupBy.Current == null)
      return;
    GIGroupBy first = (GIGroupBy) PXSelectBase<GIGroupBy, PXSelect<GIGroupBy, Where<GIGroupBy.designID, Equal<Current<GIDesign.designID>>, And<GIGroupBy.lineNbr, Greater<Current<GIGroupBy.lineNbr>>>>, OrderBy<Asc<GIGroupBy.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.GroupBy.Cache, (object) first, (object) this.GroupBy.Current);
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveUpOn()
  {
    if (this.JoinConditions.Current == null)
      return;
    GIOn first = (GIOn) PXSelectBase<GIOn, PXSelect<GIOn, Where<GIOn.designID, Equal<Current<GIDesign.designID>>, And<GIOn.relationNbr, Equal<Current<GIOn.relationNbr>>, And<GIOn.lineNbr, Less<Current<GIOn.lineNbr>>>>>, OrderBy<Desc<GIOn.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.JoinConditions.Cache, (object) first, (object) this.JoinConditions.Current);
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  protected void moveDownOn()
  {
    if (this.JoinConditions.Current == null)
      return;
    GIOn first = (GIOn) PXSelectBase<GIOn, PXSelect<GIOn, Where<GIOn.designID, Equal<Current<GIDesign.designID>>, And<GIOn.relationNbr, Equal<Current<GIOn.relationNbr>>, And<GIOn.lineNbr, Greater<Current<GIOn.lineNbr>>>>>, OrderBy<Asc<GIOn.lineNbr>>>.Config>.Select((PXGraph) this);
    if (first == null)
      return;
    this.SwapItems(this.JoinConditions.Cache, (object) first, (object) this.JoinConditions.Current);
  }

  private void SwapItems(PXCache cache, object first, object second)
  {
    object copy = cache.CreateCopy(first);
    (string NoteText, int? FilesCount, int? ActivitiesCount, string NotePopupText) noteSettings1 = this.GetNoteSettings(cache, first);
    (string NoteText, int? FilesCount, int? ActivitiesCount, string NotePopupText) noteSettings2 = this.GetNoteSettings(cache, second);
    foreach (System.Type bqlField in cache.BqlFields)
    {
      if (!cache.BqlKeys.Contains(bqlField))
      {
        cache.SetValue(first, bqlField.Name, cache.GetValue(second, bqlField.Name));
        cache.SetValue(second, bqlField.Name, cache.GetValue(copy, bqlField.Name));
      }
    }
    cache._ResetOriginalCounts(first, true, true, true);
    cache._ResetOriginalCounts(second, true, true, true);
    cache._SetOriginalCounts(first, noteSettings2.NoteText, noteSettings2.FilesCount, noteSettings2.ActivitiesCount, noteSettings2.NotePopupText);
    cache._SetOriginalCounts(second, noteSettings1.NoteText, noteSettings1.FilesCount, noteSettings1.ActivitiesCount, noteSettings1.NotePopupText);
    this.SwapTranslations(cache, first, second);
    cache.Update(first);
    cache.Update(second);
  }

  private (string NoteText, int? FilesCount, int? ActivitiesCount, string NotePopupText) GetNoteSettings(
    PXCache cache,
    object row)
  {
    Tuple<string, int?, int?, string> originalCounts = cache._GetOriginalCounts(row);
    return (originalCounts.Item1, originalCounts.Item2, originalCounts.Item3, originalCounts.Item4);
  }

  private void SwapTranslations(PXCache cache, object first, object second)
  {
    if (!PXDBLocalizableStringAttribute.IsEnabled)
      return;
    foreach (string localizableField in GenericInquiryDesigner.GetLocalizableFields(cache))
    {
      string fieldName = localizableField + "Translations";
      string[] valueExt1 = cache.GetValueExt(first, fieldName) as string[];
      string[] valueExt2 = cache.GetValueExt(second, fieldName) as string[];
      cache.SetValueExt(second, fieldName, (object) valueExt1);
      cache.SetValueExt(first, fieldName, (object) valueExt2);
    }
  }

  private static IEnumerable<string> GetLocalizableFields(PXCache cache)
  {
    return cache.BqlFields.Where<System.Type>((Func<System.Type, bool>) (f => cache.GetAttributes(f.Name).OfType<PXDBLocalizableStringAttribute>().Any<PXDBLocalizableStringAttribute>((Func<PXDBLocalizableStringAttribute, bool>) (a => a.MultiLingual)))).Select<System.Type, string>((Func<System.Type, string>) (t => t.Name));
  }

  private void SaveChanges()
  {
    RelatedTable relatedTables = this.RelatedTables.Current;
    IEnumerable<GITable> source = this.Tables.Select().RowCast<GITable>();
    if (!string.IsNullOrEmpty(relatedTables.ParentTable) && !source.Any<GITable>((Func<GITable, bool>) (t => t.Name == relatedTables.ParentTable)))
      this.AddTable(relatedTables.ParentTable, relatedTables.ParentAlias);
    if (!string.IsNullOrEmpty(relatedTables.ChildTable) && !source.Any<GITable>((Func<GITable, bool>) (t => t.Name == relatedTables.ChildTable)))
      this.AddTable(relatedTables.ChildTable, relatedTables.ChildAlias);
    if (string.IsNullOrEmpty(relatedTables.LinkedFrom))
      return;
    List<string> list1 = ((IEnumerable<string>) relatedTables.LinkedFrom.Split(',')).ToList<string>();
    List<string> list2 = ((IEnumerable<string>) relatedTables.LinkedToFields.Split(',')).ToList<string>();
    GIRelation giRelation = this.Relations.Insert();
    giRelation.DesignID = this.Designs.Current.DesignID;
    giRelation.ParentTable = relatedTables.ParentAlias;
    giRelation.ChildTable = relatedTables.ChildAlias;
    foreach (Tuple<string, string> tuple in EnumerableExtensions.Zip<string, string>((IEnumerable<string>) list1, (IEnumerable<string>) list2))
    {
      string str1;
      string str2;
      tuple.Deconstruct<string, string>(out str1, out str2);
      string str3 = str1;
      string str4 = str2;
      GIOn giOn = this.JoinConditions.Insert();
      giOn.ChildField = str4;
      giOn.ParentField = str3;
      giOn.RelationNbr = giRelation.LineNbr;
    }
  }

  [PXButton(ImageKey = "DataEntry", Tooltip = "Add a related table for the selected table")]
  [PXUIField(DisplayName = "Add Related Table", MapEnableRights = PXCacheRights.Update)]
  public virtual IEnumerable addRelatedTable(PXAdapter adapter)
  {
    bool flag1 = !string.IsNullOrEmpty(this.RelatedTables.Current?.ParentAlias);
    if (!flag1)
    {
      this.RelatedTables.Cache.Clear();
      if (!string.IsNullOrEmpty(this.Tables.Current?.Name))
      {
        this.RelatedTables.Cache.SetValue<RelatedTable.parentTable>((object) this.RelatedTables.Current, (object) this.Tables.Current.Name);
        this.SelectChildTable.SetVisible(true);
        this.SelectParentTable.SetVisible(false);
      }
      else
      {
        this.SelectChildTable.SetVisible(false);
        this.SelectParentTable.SetVisible(true);
      }
    }
    if (this.RelatedTables.AskExt(true) == WebDialogResult.OK & flag1)
    {
      RelatedTable current = this.RelatedTables.Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        bool? isNew = current.IsNew;
        bool flag2 = true;
        num = isNew.GetValueOrDefault() == flag2 & isNew.HasValue ? 1 : 0;
      }
      if (num != 0)
        this.SaveChanges();
    }
    this.RelatedTables.Cache.Clear();
    return adapter.Get();
  }

  [PXButton(ImageKey = "DataEntry", Tooltip = "Add a related table for the selected table")]
  [PXUIField(DisplayName = "Add Related Table", MapEnableRights = PXCacheRights.Update)]
  public virtual IEnumerable addRelatedTableRelations(PXAdapter adapter)
  {
    bool flag1 = !string.IsNullOrEmpty(this.RelatedTables.Current?.ParentAlias);
    if (!flag1)
      this.RelatedTables.Cache.Clear();
    if (this.RelatedTables.AskExt(true) == WebDialogResult.OK & flag1)
    {
      RelatedTable current = this.RelatedTables.Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        bool? isNew = current.IsNew;
        bool flag2 = true;
        num = isNew.GetValueOrDefault() == flag2 & isNew.HasValue ? 1 : 0;
      }
      if (num != 0)
        this.SaveChanges();
    }
    this.RelatedTables.Cache.Clear();
    return adapter.Get();
  }

  [PXButton(ImageKey = "DataEntry", Tooltip = "Add a relation between tables")]
  [PXUIField(DisplayName = "Add Relations", MapEnableRights = PXCacheRights.Update)]
  public virtual IEnumerable browseForRelation(PXAdapter adapter)
  {
    bool flag1 = !string.IsNullOrEmpty(this.RelatedTables.Current?.ParentAlias);
    if (!flag1)
    {
      string name1 = this.Tables.Select().RowCast<GITable>().Where<GITable>((Func<GITable, bool>) (t => string.Equals(t.Alias, this.Relations.Current.ParentTable, StringComparison.InvariantCulture))).FirstOrDefault<GITable>()?.Name;
      string name2 = this.Tables.Select().RowCast<GITable>().Where<GITable>((Func<GITable, bool>) (t => string.Equals(t.Alias, this.Relations.Current.ChildTable, StringComparison.InvariantCulture))).FirstOrDefault<GITable>()?.Name;
      this.RelatedTables.Cache.Clear();
      this.RelatedTables.Cache.SetValue<RelatedTable.parentTable>((object) this.RelatedTables.Current, (object) name1);
      this.RelatedTables.Cache.SetValue<RelatedTable.childTable>((object) this.RelatedTables.Current, (object) name2);
      this.SetRelations(this.RelatedTables.Current, this.JoinConditions.Select().RowCast<GIOn>().ToList<GIOn>(), this.Relations.Current.JoinType);
    }
    if (this.RelatedTables.AskExt(true) == WebDialogResult.OK & flag1)
    {
      RelatedTable current = this.RelatedTables.Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        bool? isNew = current.IsNew;
        bool flag2 = true;
        num = isNew.GetValueOrDefault() == flag2 & isNew.HasValue ? 1 : 0;
      }
      if (num != 0)
        this.SaveChanges();
    }
    this.RelatedTables.Cache.Clear();
    return adapter.Get();
  }

  protected void AddTable(string name, string alias)
  {
    GITable giTable = this.Tables.Insert();
    giTable.DesignID = this.Designs.Current.DesignID;
    giTable.Name = name;
    giTable.Alias = alias;
    giTable.Type = new int?(0);
  }

  [PXButton(ImageKey = "DataEntry", Tooltip = "Add the selected table as the parent one")]
  [PXUIField(DisplayName = "Select Parent Table", MapEnableRights = PXCacheRights.Update)]
  protected void selectParentTable()
  {
    string parentTable = this.RelatedTables.Current?.ParentTable;
    LinkedTable table = this.TablesInformation.Current;
    if (!string.IsNullOrEmpty(parentTable) || table == null || this.Tables.Select().RowCast<GITable>().Any<GITable>((Func<GITable, bool>) (tbl => string.Equals(tbl.Name, table.FullName, StringComparison.InvariantCultureIgnoreCase))))
      return;
    this.RelatedTables.Cache.SetValue<RelatedTable.parentTable>((object) this.RelatedTables.Current, (object) table.FullName);
    this.RelatedTables.Cache.SetValue<RelatedTable.isNew>((object) this.RelatedTables.Current, (object) true);
  }

  [PXButton(ImageKey = "DataEntry", Tooltip = "Add the selected table as the child one")]
  [PXUIField(DisplayName = "Select Related Table", MapEnableRights = PXCacheRights.Update)]
  protected void selectChildTable()
  {
    RelatedTable current1 = this.RelatedTables.Current;
    if (string.IsNullOrEmpty(current1?.ParentTable))
      return;
    LinkedTable current2 = this.TablesInformation.Current;
    current1.ChildTable = current2.FullName;
    current1.IsNew = new bool?(true);
    this.RelatedTables.Cache.Current = (object) current1;
    IEnumerable<string> strings1 = ((IEnumerable<string>) current2.LinkedFrom.Split(',')).Select<string, string>((Func<string, string>) (field => field.Trim()));
    IEnumerable<string> strings2 = ((IEnumerable<string>) current2.LinkedToFields.Split(',')).Select<string, string>((Func<string, string>) (field => field.Trim()));
    List<GIOn> conditions = new List<GIOn>();
    foreach (Tuple<string, string> tuple in EnumerableExtensions.Zip<string, string>(strings1, strings2))
    {
      string str1;
      string str2;
      tuple.Deconstruct<string, string>(out str1, out str2);
      string str3 = str1;
      string str4 = str2;
      GIOn giOn = new GIOn()
      {
        ChildField = str4,
        ParentField = str3,
        Operation = "A",
        Condition = "E"
      };
      conditions.Add(giOn);
    }
    this.SetRelations(current1, conditions, "I");
  }

  [PXButton(ImageKey = "DataEntry", Tooltip = "Display the list of combo box values")]
  [PXUIField(DisplayName = "Combo Box Values", MapEnableRights = PXCacheRights.Update)]
  protected void showAvailableValues()
  {
    if (this.Parameters.Current == null)
      throw new PXException("Please select the row to define combo box values for.");
    if (this.Parameters.Current.FieldName != typeof (CheckboxCombobox.combobox).FullName)
      throw new PXException("Combo box values cannot be defined for this row. Please first select the <Combobox> value in the Schema field.");
    if (this.ValuesLabels.AskExt() != WebDialogResult.OK)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (PXResult<ComboValues> pxResult in this.ValuesLabels.Select())
    {
      ComboValues comboValues = (ComboValues) pxResult;
      stringBuilder.Append(comboValues.Value);
      stringBuilder.Append(';');
      stringBuilder.Append(comboValues.Label);
      stringBuilder.Append(',');
    }
    this.Parameters.Current.AvailableValues = stringBuilder.ToString().Trim(',');
    this.Parameters.Cache.Update((object) this.Parameters.Current);
  }

  [PXButton]
  protected void navigateToDataSource()
  {
    GITable current = this.Tables.Current;
    if (string.IsNullOrEmpty(current?.Name))
      return;
    int? type = current.Type;
    int num = 1;
    Guid result;
    if (type.GetValueOrDefault() == num & type.HasValue && Guid.TryParse(current.Name, out result))
    {
      if (this.GenericInquiryDescriptionProvider.Get(result) == null)
        throw new PXException("This generic inquiry cannot be displayed. Please check the settings of the generic inquiry.");
      throw new PXRedirectToGIDesignerRequiredException(result, PXBaseRedirectException.WindowMode.New);
    }
    this.Actions["NavigateToDacSchemaBrowser"].Press();
  }

  protected IEnumerable valuesLabels()
  {
    List<string> values = new List<string>();
    List<string> labels = new List<string>();
    if (this.Parameters.Current != null)
      PXGenericInqGrph.ParseAvailableValues(this.Parameters.Current.AvailableValues, values, labels);
    for (int index = 0; index < values.Count && index < labels.Count; ++index)
    {
      ComboValues comboValues = new ComboValues()
      {
        DesignID = this.Designs.Current.DesignID,
        ParamNbr = this.Parameters.Current.LineNbr,
        Value = values[index],
        Label = labels[index]
      };
      if (this.ValuesLabels.Cache.Locate((object) comboValues) == null)
      {
        object obj = this.ValuesLabels.Cache.Insert((object) comboValues);
        if (obj != null)
          this.ValuesLabels.Cache.SetStatus(obj, PXEntryStatus.Held);
      }
    }
    foreach (ComboValues comboValues in this.ValuesLabels.Cache.Cached)
    {
      if (this.ValuesLabels.Cache.GetStatus((object) comboValues) != PXEntryStatus.Deleted)
      {
        Guid? designId1 = comboValues.DesignID;
        Guid? designId2 = this.Designs.Current.DesignID;
        if ((designId1.HasValue == designId2.HasValue ? (designId1.HasValue ? (designId1.GetValueOrDefault() == designId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          int? paramNbr = comboValues.ParamNbr;
          int? lineNbr = this.Parameters.Current.LineNbr;
          if (paramNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & paramNbr.HasValue == lineNbr.HasValue)
            yield return (object) comboValues;
        }
      }
    }
  }

  [InjectDependency]
  internal IDacRelationService DacRelations { get; set; }

  private IEnumerable FindParentTables()
  {
    foreach (object parentTable in this.Tables.Select().RowCast<GITable>())
      yield return parentTable;
    if (!string.IsNullOrEmpty(this.RelatedTables.Current?.ParentTable))
      yield return (object) new GITable()
      {
        Name = this.RelatedTables.Current.ParentTable,
        Alias = this.RelatedTables.Current.ParentAlias
      };
  }

  private IEnumerable<LinkedTable> FindRelatedTables(
    Dictionary<string, List<Tuple<HashSet<string>, HashSet<string>>>> linkedFields,
    bool isOutgoing)
  {
    string parentTable = this.RelatedTables.Current?.ParentTable;
    if (!string.IsNullOrEmpty(parentTable))
    {
      TableRelations tableRelations = this.DacRelations.GetTableRelations(PXBuildManager.GetType(parentTable, false));
      if (tableRelations != null)
      {
        foreach (TableRelations.Relation relation1 in isOutgoing ? tableRelations.OutRelations : tableRelations.InRelations)
        {
          System.Type childTable = isOutgoing ? relation1.ToTable : relation1.FromTable;
          string childFullName = childTable.FullName;
          if (!linkedFields.ContainsKey(childFullName))
            linkedFields[childFullName] = new List<Tuple<HashSet<string>, HashSet<string>>>();
          string dispName = (string) null;
          if (childTable.IsDefined(typeof (PXCacheNameAttribute), true))
            dispName = ((PXNameAttribute) childTable.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
          if (string.IsNullOrEmpty(dispName))
          {
            bool? includeHidden = this.RelatedTables.Current.IncludeHidden;
            bool flag = true;
            if (!(includeHidden.GetValueOrDefault() == flag & includeHidden.HasValue))
              continue;
          }
          foreach (ImmutableList<FieldRelations.Relation> immutableList in relation1.FieldRelations.Relations.Values)
          {
            foreach (FieldRelations.Relation relation2 in immutableList)
            {
              IEnumerable<string> linkedToFields = isOutgoing ? ((IEnumerable<Tuple<System.Type, System.Type>>) relation2.FieldPairs).Select<Tuple<System.Type, System.Type>, string>((Func<Tuple<System.Type, System.Type>, string>) (f => f.Item2.Name)) : ((IEnumerable<Tuple<System.Type, System.Type>>) relation2.FieldPairs).Select<Tuple<System.Type, System.Type>, string>((Func<Tuple<System.Type, System.Type>, string>) (f => f.Item1.Name));
              IEnumerable<string> linkedFromFields = isOutgoing ? ((IEnumerable<Tuple<System.Type, System.Type>>) relation2.FieldPairs).Select<Tuple<System.Type, System.Type>, string>((Func<Tuple<System.Type, System.Type>, string>) (f => f.Item1.Name)) : ((IEnumerable<Tuple<System.Type, System.Type>>) relation2.FieldPairs).Select<Tuple<System.Type, System.Type>, string>((Func<Tuple<System.Type, System.Type>, string>) (f => f.Item2.Name));
              if (!linkedFields[childFullName].Any<Tuple<HashSet<string>, HashSet<string>>>((Func<Tuple<HashSet<string>, HashSet<string>>, bool>) (fields => fields.Item1.SetEquals(linkedToFields) && fields.Item2.SetEquals(linkedFromFields))))
              {
                LinkedTable relatedTable = new LinkedTable();
                relatedTable.FullName = childFullName;
                relatedTable.Name = childTable.Name;
                relatedTable.DisplayName = dispName;
                relatedTable.LinkedToFields = string.Join(", ", linkedToFields);
                relatedTable.LinkedFrom = string.Join(", ", linkedFromFields);
                yield return relatedTable;
                linkedFields[childFullName].Add(new Tuple<HashSet<string>, HashSet<string>>(linkedToFields.ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase), linkedFromFields.ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)));
              }
            }
          }
          childTable = (System.Type) null;
          childFullName = (string) null;
          dispName = (string) null;
        }
      }
    }
  }

  private IEnumerable FindRelatedTables()
  {
    Dictionary<string, List<Tuple<HashSet<string>, HashSet<string>>>> linkedFields = new Dictionary<string, List<Tuple<HashSet<string>, HashSet<string>>>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    return (IEnumerable) this.FindRelatedTables(linkedFields, true).Union<LinkedTable>(this.FindRelatedTables(linkedFields, false));
  }

  protected IEnumerable tablesInformation()
  {
    if (string.IsNullOrEmpty(this.RelatedTables.Current?.ParentTable))
    {
      IEnumerable<string> excludedTables = this.Tables.Select().RowCast<GITable>().Select<GITable, string>((Func<GITable, string>) (t => t.Name));
      foreach (System.Type type in this.DacRegistry.Visible)
      {
        string str = (string) null;
        if (type.IsDefined(typeof (PXCacheNameAttribute), true))
          str = ((PXNameAttribute) type.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
        if (string.IsNullOrEmpty(str))
        {
          bool? includeHidden = this.RelatedTables.Current.IncludeHidden;
          bool flag = true;
          if (!(includeHidden.GetValueOrDefault() == flag & includeHidden.HasValue))
            continue;
        }
        if (!excludedTables.Contains<string>(type.FullName))
        {
          LinkedTable linkedTable = new LinkedTable();
          linkedTable.FullName = type.FullName;
          linkedTable.Name = type.Name;
          linkedTable.DisplayName = str;
          yield return (object) linkedTable;
        }
      }
      excludedTables = (IEnumerable<string>) null;
    }
    else
    {
      string childAlias = this.RelatedTables.Current.ChildAlias;
      string relation = this.RelatedTables.Current.Relation;
      foreach (object relatedTable in this.FindRelatedTables())
      {
        if (string.IsNullOrEmpty(childAlias) || !string.IsNullOrEmpty(relation) || childAlias == (relatedTable as LinkedTable).Name)
          yield return relatedTable;
      }
      childAlias = (string) null;
      relation = (string) null;
    }
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
    return (IEnumerable) (base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows) as List<object>);
  }

  public override void Clear()
  {
    base.Clear();
    this.views.Clear();
    this._allTables = (Dictionary<string, GITable>) null;
  }

  public override void Persist()
  {
    this.IsSiteMapAltered = this.IsSiteMapAltered | this.ScreenToSiteMapAddHelper.IsSiteMapAltered | this.ListEntryPoint.Cache.IsDirty;
    if (this.CheckIfCopyShouldBeSaved())
      return;
    base.Persist();
    this.PageCacheControl.InvalidateCache();
    this.InvalidateCacheRelatedToDesignId();
  }

  private bool CheckIfCopyShouldBeSaved()
  {
    PXCache cache = this.Views[this.PrimaryView].Cache;
    if (!this.IsDirty || !cache.IsInsertedUpdatedDeleted || NonGenericIEnumerableExtensions.Any_(cache.Deleted) || this.IsSiteMapAltered)
      return false;
    WebDialogResult? nullable = this.AskIfGIIsUsedByAnotherGI(this.CurrentDesign.Current.DesignID, "The inquiry is used in generic inquiries {0}. Do you want to save a copy of this inquiry or overwrite it?", this.CreateCopyOrOverwriteGIDialog);
    if (nullable.HasValue)
    {
      switch (nullable.GetValueOrDefault())
      {
        case WebDialogResult.Cancel:
          return true;
        case WebDialogResult.Yes:
          PXGraph designWithUniqueName = this.CreateCopyOfCurrentDesignWithUniqueName();
          if (designWithUniqueName == null)
            return false;
          this.Cancel.Press();
          throw new PXRedirectRequiredException(designWithUniqueName, false, string.Empty);
      }
    }
    return false;
  }

  private void InvalidateCacheRelatedToDesignId()
  {
    Guid? designId = (Guid?) this.CurrentDesign.Current?.DesignID;
    if (!designId.HasValue)
      return;
    this.ScreenInfoCacheControl.InvalidateCache(designId.Value);
  }

  private void SetRelations(RelatedTable table, List<GIOn> conditions, string joinType)
  {
    List<string> values1 = new List<string>();
    List<string> values2 = new List<string>();
    string str1 = string.Join(" ", table.ParentAlias, PXJoinTypeListAttribute.JoinTypes[joinType].ToUpper(), "JOIN", table.ChildAlias, "ON");
    for (int index = 0; index < conditions.Count; ++index)
    {
      GIOn condition = conditions[index];
      string str2 = (string.IsNullOrEmpty(condition.OpenBrackets) ? "" : condition.OpenBrackets) + this.GetCondition(condition.ParentField.StartsWith("=") ? condition.ParentField : $"{table.ParentAlias}.{condition.ParentField}", condition.ChildField.StartsWith("=") ? condition.ChildField : $"{table.ChildAlias}.{condition.ChildField}", condition.Condition);
      if (!string.IsNullOrEmpty(condition.CloseBrackets))
        str2 += condition.CloseBrackets;
      if (index < conditions.Count - 1)
        str2 += condition.Operation == "A" ? " AND" : " OR";
      str1 = $"{str1} {str2}";
      values1.Add(condition.ParentField);
      values2.Add(condition.ChildField);
    }
    table.Relation = str1;
    table.LinkedFrom = string.Join(",", (IEnumerable<string>) values1);
    table.LinkedToFields = string.Join(",", (IEnumerable<string>) values2);
  }

  private string GetCondition(string parentField, string childField, string conditionType)
  {
    string str = PXConditionListAttribute.Conditions[conditionType.Trim()];
    switch (str)
    {
      case "Equals":
        str = "=";
        break;
      case "Does Not Equal":
        str = "!=";
        break;
      case "Is Greater Than":
        str = ">";
        break;
      case "Is Greater Than or Equal To":
        str = ">=";
        break;
      case "Is Less Than":
        str = "<";
        break;
      case "Is Less Than or Equal To":
        str = "<=";
        break;
    }
    return string.Join(" ", parentField, str, childField);
  }

  protected void RelatedTable_ChildTable_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is RelatedTable row) || !(row.ChildTable != e.NewValue?.ToString()))
      return;
    cache.SetValue<RelatedTable.linkedFrom>((object) row, (object) null);
    cache.SetValue<RelatedTable.linkedToFields>((object) row, (object) null);
    cache.SetValue<RelatedTable.relation>((object) row, (object) null);
  }

  protected void RelatedTable_ParentTable_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is RelatedTable row) || !(row.ParentTable != e.NewValue?.ToString()))
      return;
    cache.SetValue<RelatedTable.childTable>((object) row, (object) null);
    cache.SetValue<RelatedTable.relation>((object) row, (object) null);
    cache.SetValue<RelatedTable.linkedFrom>((object) row, (object) null);
    cache.SetValue<RelatedTable.linkedToFields>((object) row, (object) null);
  }

  protected void _(Events.RowSelected<RelatedTable> e)
  {
    RelatedTable row = e.Row;
    bool flag = row?.ParentTable != null;
    PXUIFieldAttribute.SetEnabled<RelatedTable.childTable>(this.RelatedTables.Cache, (object) row, flag);
    PXUIFieldAttribute.SetVisible<LinkedTable.linkedFrom>(this.TablesInformation.Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<LinkedTable.linkedToFields>(this.TablesInformation.Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<PXTablesSelectorAttribute.SingleTable.displayName>(this.TablesInformation.Cache, (object) null, !flag);
    this.SelectChildTable.SetVisible(flag);
    this.SelectParentTable.SetVisible(!flag);
    if (!string.IsNullOrEmpty(row.ChildTable) && !string.IsNullOrEmpty(row.LinkedFrom))
      return;
    this.RelatedTables.Cache.SetValue<RelatedTable.relation>((object) row, (object) string.Empty);
  }

  protected virtual void ListEntryPoint_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Data.ListEntryPoint row = (PX.Data.ListEntryPoint) e.Row;
    if (row.ListScreenID != null)
      return;
    PX.SM.SiteMap siteMap = this.ScreenToSiteMapAddHelper.Cached.FirstOrDefault<PX.SM.SiteMap>();
    if (siteMap == null)
      return;
    row.ListScreenID = siteMap.ScreenID;
  }

  protected void GIDesign_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool flag1 = e.Row is GIDesign row && cache.GetStatus((object) row) != PXEntryStatus.Inserted;
    this.ViewInquiry.SetEnabled(flag1);
    this.ViewInquiry.SetIsLockedOnToolbar(flag1);
    this.ViewInquiry.SetConnotation(flag1 ? ActionConnotation.Success : ActionConnotation.None);
    if (row == null)
      return;
    bool flag2 = this.IsGenericInquiryScreen(row.PrimaryScreenID);
    if (flag2)
    {
      cache.SetValueExt<GIDesign.newRecordCreationEnabled>((object) row, (object) false);
      cache.SetValueExt<GIDesign.massRecordsUpdateEnabled>((object) row, (object) false);
      cache.SetValueExt<GIDesign.massActionsOnRecordsEnabled>((object) row, (object) false);
    }
    bool isEnabled = row.PrimaryScreenID != null && !flag2;
    PXUIFieldAttribute.SetEnabled<GIDesign.replacePrimaryScreen>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<GIDesign.newRecordCreationEnabled>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<GIDesign.massDeleteEnabled>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<GIDesign.autoConfirmDelete>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<GIDesign.massRecordsUpdateEnabled>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<GIDesign.massActionsOnRecordsEnabled>(cache, (object) row, isEnabled);
    PXSelect<GIRecordDefault, Where<GIRecordDefault.designID, Equal<Current<GIDesign.designID>>>> recordDefaults = this.RecordDefaults;
    int num;
    if (row.PrimaryScreenID != null)
    {
      bool? recordCreationEnabled = row.NewRecordCreationEnabled;
      bool flag3 = true;
      num = recordCreationEnabled.GetValueOrDefault() == flag3 & recordCreationEnabled.HasValue ? 1 : 0;
    }
    else
      num = 0;
    this.SetEnabled((PXSelectBase) recordDefaults, num != 0);
    this.SetEnabled((PXSelectBase) this.MassUpdateFields, row.PrimaryScreenID != null);
    this.SetEnabled((PXSelectBase) this.MassActions, row.PrimaryScreenID != null);
    GIRowValidationError[] array = this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GIDesign>(cache, row).ToArray<GIRowValidationError>();
    this.SetUiErrors(cache, (IBqlTable) row, (GIValidationError[]) array, true, "name", "primaryScreenID", "exposeViaOData", "defaultSortOrder");
    this.FillNavigationNbrList();
    this.FillNotesAndTablesList();
  }

  private void SetUiErrors(
    PXCache cache,
    IBqlTable row,
    GIValidationError[] errors,
    bool refreshErrors,
    params string[] fieldNames)
  {
    foreach (string fieldName1 in fieldNames)
    {
      string fieldName = fieldName1;
      GIFieldValidationError error = ((IEnumerable<GIValidationError>) errors).LastOrDefault<GIValidationError>((Func<GIValidationError, bool>) (e => e is GIFieldValidationError fieldValidationError && string.Equals(fieldValidationError.FieldName, fieldName, StringComparison.OrdinalIgnoreCase))) as GIFieldValidationError;
      IEnumerable<PXUIFieldAttribute> attributesOfType = cache.GetAttributesOfType<PXUIFieldAttribute>((object) row, fieldName);
      PXUIFieldAttribute pxuiFieldAttribute = attributesOfType != null ? attributesOfType.FirstOrDefault<PXUIFieldAttribute>() : (PXUIFieldAttribute) null;
      PXErrorLevel pxErrorLevel1 = pxuiFieldAttribute != null ? pxuiFieldAttribute.ErrorLevel : PXErrorLevel.Undefined;
      PXErrorLevel pxErrorLevel2 = error != null ? error.ErrorLevel : PXErrorLevel.Undefined;
      if (refreshErrors || pxErrorLevel2 >= pxErrorLevel1)
        cache.RaiseExceptionHandling(fieldName, (object) (error?.Row ?? row), cache.GetValue((object) row, fieldName), error != null ? (Exception) error.CreateSetPropertyException() : (Exception) null);
    }
  }

  private void FillNavigationNbrList()
  {
    List<int> intList = new List<int>();
    List<string> stringList = new List<string>();
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    PXResultset<GINavigationScreen> resultSet = this.NavigationScreensForColumns.Select();
    IEnumerable<\u003C\u003Ef__AnonymousType28<string, int>> source = resultSet.RowCast<GINavigationScreen>().GroupBy((Func<GINavigationScreen, string>) (g => g.Link), (key, cnt) => new
    {
      Link = key,
      Count = cnt.Count<GINavigationScreen>()
    });
    foreach (PXResult<GINavigationScreen> pxResult in resultSet)
    {
      GINavigationScreen navScreen = (GINavigationScreen) pxResult;
      if (navScreen.Link != null)
      {
        PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(navScreen.Link);
        if (screenIdUnsecure != null || NavigationTemplateHelper.IsExternalUrlOrTemplate(navScreen.Link))
        {
          string key = screenIdUnsecure == null ? navScreen.Link : $"{navScreen.Link} - {screenIdUnsecure.Title}";
          intList.Add(navScreen.LineNbr ?? -1);
          if (source.Where(a => a.Link == navScreen.Link).Select(a => a.Count).FirstOrDefault<int>() > 1)
          {
            if (!dictionary.ContainsKey(key))
              dictionary.Add(key, 0);
            stringList.Add($"{key} ({++dictionary[key]})");
          }
          else
            stringList.Add(key ?? "");
        }
      }
    }
    PXIntListAttribute.SetList<GIResult.navigationNbr>(this.Results.Cache, (object) null, intList.ToArray(), stringList.ToArray());
    PXUIFieldAttribute.SetEnabled<GIResult.navigationNbr>(this.Results.Cache, (object) null, intList.Count > 0);
  }

  private void FillNotesAndTablesList()
  {
    HashSet<string> tablesUsedInRelations = this.GetTablesUsedInRelations();
    foreach (PXResult<GIResult> pxResult in this.Results.Select())
    {
      GIResult giResult = (GIResult) pxResult;
      bool? isActive = giResult.IsActive;
      bool flag = true;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
        tablesUsedInRelations.Add(giResult.ObjectName);
    }
    PXStringListAttribute.SetList<GIDesign.notesAndFilesTable>(this.Designs.Cache, (object) null, new List<(string, string)>()
    {
      ("$<None>", "Not Applicable")
    }.Concat<(string, string)>(tablesUsedInRelations.Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x) && this.HasNoteField(x))).Select<string, (string, string)>((Func<string, (string, string)>) (x => (x, x)))).ToArray<(string, string)>());
  }

  private bool HasNoteField(string tableAlias)
  {
    GITable giTable = this.Tables.Select().RowCast<GITable>().FirstOrDefault<GITable>((Func<GITable, bool>) (x => x.Alias == tableAlias));
    if (string.IsNullOrEmpty(giTable?.Name))
      return false;
    System.Type type = PXBuildManager.GetType(giTable.Name, false);
    return !(type == (System.Type) null) && this.Caches[type].Fields.Contains("NoteID");
  }

  protected virtual void _(
    Events.FieldSelecting<GIDesign.notesAndFilesTable> e)
  {
    if (!this.IsCopyPasteContext)
      return;
    e.Cancel = true;
  }

  protected void GIDesign_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    GIDesign row = e.Row as GIDesign;
    if (!row.DesignID.HasValue || string.IsNullOrEmpty(row.SitemapScreenID))
      return;
    foreach (PXResult<GenericInquiryDesigner.NestedFilterHeader> pxResult in this.FilterHeaders.Select((object) row.SitemapScreenID))
      this.FilterHeaders.Delete((GenericInquiryDesigner.NestedFilterHeader) pxResult);
  }

  protected void GIDesign_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected void GIDesign_FilterColCount_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    GIDesign row = (GIDesign) e.Row;
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldVerifying, GIDesign, GIDesign.filterColCount>(cache, row, e.NewValue)).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw error.CreateSetPropertyException();
    }));
  }

  protected void GIDesign_ReplacePrimaryScreen_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is GIDesign row))
      return;
    PX.Data.ListEntryPoint listEntryPoint = this.ListEntryPoint.SelectSingle((object) row.PrimaryScreenID, (object) row.SitemapScreenID);
    int num;
    if (listEntryPoint != null)
    {
      bool? isActive = listEntryPoint.IsActive;
      bool flag = true;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag1 = num != 0;
    row.ReplacePrimaryScreen = new bool?(flag1);
    e.ReturnValue = (object) flag1;
  }

  protected void GIDesign_ReplacePrimaryScreen_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (this.IsCopyPasteContext)
      return;
    GIDesign row = e.Row as GIDesign;
    bool? newValue = e.NewValue as bool?;
    if (row == null)
      return;
    bool? replacePrimaryScreen = row.ReplacePrimaryScreen;
    bool? nullable = newValue;
    if (replacePrimaryScreen.GetValueOrDefault() == nullable.GetValueOrDefault() & replacePrimaryScreen.HasValue == nullable.HasValue)
      return;
    PXCache cach = this.Caches[typeof (PX.Data.ListEntryPoint)];
    PX.Data.ListEntryPoint listEntryPoint1 = this.ListEntryPoint.SelectSingle((object) row.PrimaryScreenID, (object) row.SitemapScreenID);
    bool flag1 = false;
    nullable = newValue;
    bool flag2 = true;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
    {
      if (listEntryPoint1 != null)
      {
        listEntryPoint1.IsActive = new bool?(true);
        cach.Update((object) listEntryPoint1);
        flag1 = true;
      }
      else
      {
        PX.Data.ListEntryPoint listEntryPoint2 = this.ListEntryPointSameEntry.SelectSingle((object) row.PrimaryScreenID);
        if (listEntryPoint2 != null)
        {
          nullable = listEntryPoint2.IsActive;
          bool flag3 = false;
          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue || this.Designs.Ask("This entry screen has already been replaced with another list screen. Do you want to overwrite the screen?", MessageButtons.YesNo) == WebDialogResult.Yes)
          {
            listEntryPoint2.ListScreenID = row.SitemapScreenID;
            listEntryPoint2.IsActive = new bool?(true);
            cach.Update((object) listEntryPoint2);
            flag1 = true;
          }
          else
          {
            e.Cancel = true;
            return;
          }
        }
        else
        {
          PX.Data.ListEntryPoint listEntryPoint3 = this.ListEntryPointSameList.SelectSingle((object) row.SitemapScreenID);
          bool flag4 = false;
          if (listEntryPoint3 != null)
          {
            if (this.Designs.Ask("This inquiry has already been used as a list for another entry screen. Do you want to replace it?", MessageButtons.YesNo) == WebDialogResult.Yes)
            {
              cach.Delete((object) listEntryPoint3);
              flag4 = true;
            }
          }
          else
            flag4 = true;
          if (flag4)
          {
            PX.Data.ListEntryPoint listEntryPoint4 = new PX.Data.ListEntryPoint()
            {
              IsActive = new bool?(true),
              EntryScreenID = row.PrimaryScreenID,
              ListScreenID = row.SitemapScreenID
            };
            cach.Insert((object) listEntryPoint4);
            flag1 = true;
          }
        }
      }
    }
    else if (listEntryPoint1 != null)
    {
      listEntryPoint1.IsActive = new bool?(false);
      cach.Update((object) listEntryPoint1);
      flag1 = true;
    }
    if (!flag1)
      return;
    bool flag5 = false;
    this.IsSiteMapAltered = true;
    GINavigationScreen navigationScreen = ((IEnumerable<GINavigationScreen>) this.NavigationScreens.Select<GINavigationScreen>()).FirstOrDefault<GINavigationScreen>((Func<GINavigationScreen, bool>) (x =>
    {
      if (!(x.Link == row.PrimaryScreenID) || !this.IsScreenNavigatedByKeys(x.LineNbr))
        return false;
      int? lineNbr = x.LineNbr;
      int num = 1;
      return lineNbr.GetValueOrDefault() == num & lineNbr.HasValue;
    }));
    if (navigationScreen != null)
    {
      nullable = newValue;
      bool flag6 = true;
      if (nullable.GetValueOrDefault() == flag6 & nullable.HasValue && navigationScreen.WindowMode != "I")
      {
        navigationScreen.WindowMode = "I";
        this.NavigationScreens.Update(navigationScreen);
        flag5 = true;
      }
      nullable = newValue;
      bool flag7 = false;
      if (nullable.GetValueOrDefault() == flag7 & nullable.HasValue && navigationScreen.WindowMode == "I")
      {
        navigationScreen.WindowMode = "N";
        this.NavigationScreens.Update(navigationScreen);
      }
    }
    nullable = newValue;
    bool flag8 = true;
    if (!(nullable.GetValueOrDefault() == flag8 & nullable.HasValue) || flag5)
      return;
    GITable inqTable = this.PrimaryScreenInqTable.SelectSingle((object) GIScreenHelper.GetCacheName(row.PrimaryScreenID));
    if (inqTable == null)
      return;
    this.InsertInNavigationPrimaryScreen(row, inqTable, newValue);
  }

  protected void GITable_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<GITable.alias>(cache, e.Row, false);
  }

  protected void GITable_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    GITable row = e.Row as GITable;
    PXUIFieldAttribute.SetEnabled<GITable.alias>(cache, e.Row, row == null || string.IsNullOrEmpty(row.Alias));
    if (row?.Name == null)
      return;
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GITable>(cache, row).OnLastError((System.Action<GIRowValidationError>) (error => cache.RaiseExceptionHandling<GITable.name>((object) row, (object) row.Name, error != null ? (Exception) error.CreateSetPropertyException() : (Exception) null)), true);
  }

  protected void GITable_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Designs.Current.DesignID;
  }

  protected void GITable_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (e.ExternalCall)
      return;
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowInserted, GITable>(cache, (GITable) e.Row).OnLastError(new System.Action<GIRowValidationError>(this.CheckPrimaryScreen));
  }

  protected void GITable_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (e.ExternalCall)
      return;
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowUpdated, GITable>(cache, (GITable) e.Row).OnLastError(new System.Action<GIRowValidationError>(this.CheckPrimaryScreen));
  }

  protected void GITable_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (e.ExternalCall)
      return;
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowDeleted, GITable>(cache, (GITable) e.Row).OnLastError(new System.Action<GIRowValidationError>(this.CheckPrimaryScreen));
  }

  private void CheckPrimaryScreen(GIRowValidationError error)
  {
    if (error == null)
      return;
    PXCache cache = this.Designs.Cache;
    IBqlTable row = error.Row;
    object newValue = (object) null;
    cache.SetValueExt<GIDesign.primaryScreenID>((object) row, newValue);
    cache.RaiseExceptionHandling<GIDesign.primaryScreenID>((object) row, newValue, (Exception) error.CreateSetPropertyException());
  }

  protected void _(Events.RowPersisting<GITable> e)
  {
    GITable row = e.Row;
    PXCache cache = e.Cache;
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowPersisting, GITable>(cache, e.Row).OnLastError((System.Action<GIRowValidationError>) (error => cache.RaiseExceptionHandling<GITable.name>((object) row, (object) row.Name, (Exception) error.CreateSetPropertyException())));
  }

  protected void _(Events.FieldUpdating<GITable.alias> e)
  {
    if (!(e.NewValue is string newValue))
      return;
    GITable row = (GITable) e.Row;
    if ((row != null ? (!row.NoteID.HasValue ? 1 : 0) : 1) != 0 || newValue.Equals(e.OldValue))
      return;
    e.NewValue = (object) this.NormalizeAlias(newValue);
  }

  protected void _(Events.FieldUpdating<GITable.type> e)
  {
    GITable row = (GITable) e.Row;
    if (!string.IsNullOrEmpty(row.Alias) || string.IsNullOrEmpty(row.Name) || !(e.NewValue is int newValue))
      return;
    string alias;
    if (newValue == 0)
    {
      alias = PXBuildManager.GetType(row.Name, true).Name;
    }
    else
    {
      if (newValue != 1)
        throw new NotImplementedException();
      GIDescription giDescription = PXGenericInqGrph.Def[Guid.Parse(row.Name)];
      alias = giDescription.Design.SitemapTitle ?? giDescription.Design.Name;
    }
    row.Alias = this.NormalizeAlias(alias);
    e.Cache.ClearQueryCache();
    e.Cache.Normalize();
  }

  private string NormalizeAlias(string alias)
  {
    string str = alias.Replace(' ', '_').Replace('.', '_').Replace('-', '_').Replace("_", "");
    int num = 2;
    string aliasToCheck = str;
    while (IsAliasExist(aliasToCheck))
      aliasToCheck = str + num++.ToString();
    return aliasToCheck;

    bool IsAliasExist(string aliasToCheck)
    {
      return (GITable) PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Current<GIDesign.designID>>, And<GITable.alias, Equal<Required<GITable.alias>>>>>.Config>.Select((PXGraph) this, (object) aliasToCheck) != null;
    }
  }

  private System.Type GetTableType(
    GITable tableToCheck,
    string fieldName,
    PXCache cache,
    object row,
    string warningMessage = null,
    PXErrorLevel errorLevel = PXErrorLevel.Warning)
  {
    System.Type type = PXBuildManager.GetType(tableToCheck.Name, false);
    if (type == (System.Type) null)
    {
      IEnumerable<PXUIFieldAttribute> attributesOfType = cache.GetAttributesOfType<PXUIFieldAttribute>(row, fieldName);
      if ((attributesOfType != null ? attributesOfType.FirstOrDefault<PXUIFieldAttribute>()?.ErrorLevel : new PXErrorLevel?()).GetValueOrDefault() >= errorLevel)
        return type;
      if (string.IsNullOrEmpty(warningMessage))
        warningMessage = "A table with the alias {0} cannot be found.";
      cache.RaiseExceptionHandling(fieldName, row, (object) tableToCheck.Name, (Exception) new PXSetPropertyException(warningMessage, errorLevel, new object[1]
      {
        (object) tableToCheck.Alias
      }));
    }
    return type;
  }

  private GITable GetTableByAlias(string tableAlias)
  {
    return (GITable) this.Tables.Search<GITable.alias>((object) tableAlias);
  }

  protected void GIRelation_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    this.JoinConditions.Cache.AllowInsert = e.Row != null;
    if (!(e.Row is GIRelation row))
      return;
    this.SetTablesList(cache, (object) row, typeof (GIRelation.parentTable).Name);
    this.SetTablesList(cache, (object) row, typeof (GIRelation.childTable).Name);
    GIRowValidationError[] array = this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GIRelation>(cache, row).ToArray<GIRowValidationError>();
    this.SetUiErrors(cache, (IBqlTable) row, (GIValidationError[]) array, false, "ParentTable", "ChildTable");
  }

  protected void GIRelation_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Designs.Current.DesignID;
  }

  protected void GIRelation_ParentTable_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    GIRelation row = (GIRelation) e.Row;
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldUpdating, GIRelation, GIRelation.parentTable>(cache, row, e.NewValue)).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw error.CreateSetPropertyException();
    }));
  }

  protected void GIRelation_ChildTable_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    GIRelation row = (GIRelation) e.Row;
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldUpdating, GIRelation, GIRelation.childTable>(cache, row, e.NewValue)).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw error.CreateSetPropertyException();
    }));
  }

  protected void _(Events.FieldUpdated<GIRelation.isActive> e)
  {
    if (!e.ExternalCall)
      return;
    this.FillNotesAndTablesList();
  }

  protected void _(Events.FieldUpdated<GIRelation.parentTable> e)
  {
    if (!e.ExternalCall)
      return;
    this.FillNotesAndTablesList();
  }

  protected void _(Events.FieldUpdated<GIRelation.childTable> e)
  {
    if (!e.ExternalCall)
      return;
    this.FillNotesAndTablesList();
  }

  protected void GIOn_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (this.IsCopyPasteContext)
      return;
    this.BrowseForRelation.SetEnabled(this.JoinConditions.Current != null);
    if (!(e.Row is GIOn row))
      return;
    string[] allParameters = this.GetAllParameters();
    List<string> strlist = new List<string>();
    List<string> strDispNames = new List<string>();
    PXSelectBase<GITable> pxSelectBase = (PXSelectBase<GITable>) new PXSelect<GITable, Where<GITable.designID, Equal<Current<GIDesign.designID>>, And<GITable.alias, Equal<Required<GITable.alias>>>>>((PXGraph) this);
    if (this.Relations.Current == null)
      return;
    GITable table1 = (GITable) pxSelectBase.Select((object) this.Relations.Current.ParentTable);
    strlist.AddRange((IEnumerable<string>) allParameters);
    strDispNames.AddRange((IEnumerable<string>) allParameters);
    this.AddTableFields(table1, false, strlist, strDispNames, (Func<PXCache, string, bool>) ((c, f) => !GenericInquiryDesigner.IsVirtualField(c, f)));
    string[] fieldsInRelation1 = this.GetFieldsInRelation(table1?.Alias);
    strlist.AddRange((IEnumerable<string>) fieldsInRelation1);
    strDispNames.AddRange((IEnumerable<string>) fieldsInRelation1);
    PXStringListAttribute.SetList<GIOn.parentField>(cache, e.Row, strlist.ToArray(), strDispNames.ToArray());
    strlist.Clear();
    strDispNames.Clear();
    GITable table2 = (GITable) pxSelectBase.Select((object) this.Relations.Current.ChildTable);
    strlist.AddRange((IEnumerable<string>) allParameters);
    strDispNames.AddRange((IEnumerable<string>) allParameters);
    this.AddTableFields(table2, false, strlist, strDispNames, (Func<PXCache, string, bool>) ((c, f) => !GenericInquiryDesigner.IsVirtualField(c, f)));
    string[] fieldsInRelation2 = this.GetFieldsInRelation(table2?.Alias);
    strlist.AddRange((IEnumerable<string>) fieldsInRelation2);
    strDispNames.AddRange((IEnumerable<string>) fieldsInRelation2);
    PXStringListAttribute.SetList<GIOn.childField>(cache, e.Row, strlist.ToArray(), strDispNames.ToArray());
    GIRowValidationError[] array = this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GIOn>(cache, row).ToArray<GIRowValidationError>();
    this.SetUiErrors(cache, (IBqlTable) row, (GIValidationError[]) array, false, "ParentField", "ChildField");
  }

  protected void GIOn_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    GIOn row = e.Row as GIOn;
  }

  protected void GIOn_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    GIOn row = e.Row as GIOn;
  }

  protected void GIOn_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldDefaulting, GIOn, GIOn.designID>(cache, (GIOn) e.Row, e.NewValue)).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw new PXException(error.Message);
    }));
    e.NewValue = (object) this.Relations.Current.DesignID;
  }

  protected void GIOn_RelationNbr_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldDefaulting, GIOn, GIOn.relationNbr>(cache, (GIOn) e.Row, e.NewValue)).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw new PXException(error.Message);
    }));
    e.NewValue = (object) this.Relations.Current.LineNbr;
  }

  protected void GIWhere_Condition_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    string condition = e.NewValue as string;
    GIWhere row = (GIWhere) e.Row;
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldUpdating, GIWhere, GIWhere.condition>(sender, row, (object) condition)).OnLastError((System.Action<GIRowValidationError>) (error => sender.RaiseExceptionHandling<GIWhere.condition>((object) row, (object) condition, (Exception) error.CreateSetPropertyException())));
  }

  internal static (string TableAlias, string FieldName) SplitDataFieldName(
    string dataFieldName,
    char delimiter = '.')
  {
    string[] strArray = dataFieldName != null ? dataFieldName.Split(delimiter, 2) : (string[]) null;
    return (strArray != null ? (strArray.Length != 2 ? 1 : 0) : 1) != 0 ? ((string) null, (string) null) : (strArray[0], strArray[1]);
  }

  protected void GIWhere_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    GIWhere row = e.Row as GIWhere;
    if (row == null)
      return;
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GIWhere>(cache, row).OnLastError((System.Action<GIRowValidationError>) (error => cache.RaiseExceptionHandling<GIWhere.dataFieldName>((object) row, (object) row.DataFieldName, (Exception) error.CreateSetPropertyException())));
  }

  protected void GIWhere_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Designs.Current.DesignID;
  }

  protected void GIWhere_DataFieldName_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    string[] allParameters = this.GetAllParameters();
    PXStringListAttribute.SetList(cache, e.Row, typeof (GIWhere.dataFieldName).Name, allParameters, allParameters);
    this.SetList<GIWhere.dataFieldName>(cache, e.Row, true, (Func<PXCache, string, bool>) ((c, f) => !GenericInquiryDesigner.IsVirtualField(c, f)));
  }

  protected void GIWhere_Value1_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    string value = e.NewValue as string;
    GIWhere row = (GIWhere) e.Row;
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldUpdating, GIWhere, GIWhere.value1>(cache, row, (object) value)).OnLastError((System.Action<GIRowValidationError>) (error => cache.RaiseExceptionHandling<GIWhere.value1>((object) row, (object) value, (Exception) error.CreateSetPropertyException())));
    this.EnsureParameterExists(e);
    if (value == null)
      return;
    e.NewValue = (object) value.ToInvariantString();
  }

  protected void GIWhere_Value2_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    this.EnsureParameterExists(e);
    if (e.NewValue == null)
      return;
    e.NewValue = (object) e.NewValue.ToInvariantString();
  }

  protected void GIWhere_IsExpression_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    GIWhere row = (GIWhere) e.Row;
    row.Value1 = (string) null;
    row.Value2 = (string) null;
  }

  protected void GIWhere_Value1_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    this.SetValueListInWhere<GIWhere.value1>(cache, e);
  }

  protected void GIWhere_Value2_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    this.SetValueListInWhere<GIWhere.value2>(cache, e);
  }

  protected void GISort_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Designs.Current.DesignID;
  }

  protected void GIFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (this.IsCopyPasteContext)
      return;
    GIFilter row = e.Row as GIFilter;
    if (row == null)
      return;
    string[] allowedValues = new string[2]
    {
      typeof (CheckboxCombobox.checkbox).FullName,
      typeof (CheckboxCombobox.combobox).FullName
    };
    string[] allowedLabels = new string[2]
    {
      "<Checkbox>",
      "<Combobox>"
    };
    PXStringListAttribute.SetList<GIFilter.fieldName>(cache, e.Row, allowedValues, allowedLabels);
    this.SetList<GIFilter.fieldName>(cache, e.Row, true);
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GIFilter>(cache, row).OnLastError((System.Action<GIRowValidationError>) (error => cache.RaiseExceptionHandling<GIFilter.fieldName>((object) row, (object) row.FieldName, (Exception) error.CreateSetPropertyException())));
    (string, string)[] valueTupleArray = this.Tables.Select().RowCast<GITable>().Where<GITable>((Func<GITable, bool>) (x =>
    {
      int? type = x.Type;
      int num = 1;
      return type.GetValueOrDefault() == num & type.HasValue;
    })).SelectMany<GITable, (string, string)>(new Func<GITable, IEnumerable<(string, string)>>(this.GetSubGIParameterNames)).ToArray<(string, string)>();
    if (valueTupleArray.Length == 0)
      valueTupleArray = new (string, string)[1]
      {
        ((string) null, (string) null)
      };
    PXStringListAttribute.SetList<GIFilter.name>(cache, e.Row, valueTupleArray);
  }

  private IEnumerable<(string, string)> GetSubGIParameterNames(GITable table)
  {
    GITable giTable = table;
    if ((giTable != null ? (!giTable.DesignID.HasValue ? 1 : 0) : 1) == 0)
    {
      GIDescription giDescription = this.GenericInquiryDescriptionProvider.Get(table.Name);
      if (giDescription != null)
      {
        foreach (GIFilter filter in giDescription.Filters)
          yield return ($"{table.Alias}_{filter.Name}", $"{table.Alias}.{filter.Name}");
      }
    }
  }

  protected void _(Events.RowUpdating<GIFilter> e)
  {
    if (string.IsNullOrEmpty(e.NewRow?.Name) || e.Row == null || string.Equals(e.Row.Name, e.NewRow.Name, StringComparison.OrdinalIgnoreCase))
      return;
    string[] split = e.NewRow.Name.Split('_', 2);
    if (split == null || split.Length != 2)
      return;
    GITable giTable = this.Tables.Select().RowCast<GITable>().FirstOrDefault<GITable>((Func<GITable, bool>) (x => x.Alias == split[0]));
    if (giTable == null)
      return;
    GIDescription giDescription = this.GenericInquiryDescriptionProvider.Get(giTable.Name);
    GIFilter giFilter = giDescription != null ? giDescription.Filters.FirstOrDefault<GIFilter>((Func<GIFilter, bool>) (x => string.Equals(x.Name, split[1]))) : (GIFilter) null;
    if (giFilter == null)
      return;
    GIFilter newRow1 = e.NewRow;
    bool? nullable = newRow1.Hidden;
    bool? hidden;
    if (!nullable.HasValue)
      newRow1.Hidden = hidden = giFilter.Hidden;
    GIFilter newRow2 = e.NewRow;
    nullable = newRow2.Required;
    if (!nullable.HasValue)
      newRow2.Required = hidden = giFilter.Hidden;
    GIFilter newRow3 = e.NewRow;
    string str;
    if (newRow3.Size == null)
      newRow3.Size = str = giFilter.Size;
    GIFilter newRow4 = e.NewRow;
    if (newRow4.AvailableValues == null)
      newRow4.AvailableValues = str = giFilter.AvailableValues;
    GIFilter newRow5 = e.NewRow;
    if (!newRow5.ColSpan.HasValue)
    {
      int? colSpan;
      newRow5.ColSpan = colSpan = giFilter.ColSpan;
    }
    GIFilter newRow6 = e.NewRow;
    if (newRow6.DataType == null)
      newRow6.DataType = str = giFilter.DataType;
    GIFilter newRow7 = e.NewRow;
    if (newRow7.DefaultValue == null)
      newRow7.DefaultValue = str = giFilter.DefaultValue;
    GIFilter newRow8 = e.NewRow;
    if (newRow8.DisplayName == null)
      newRow8.DisplayName = str = giFilter.DisplayName;
    e.NewRow.IsActive = giFilter.IsActive;
    e.NewRow.IsExpression = giFilter.IsExpression;
    GIFilter newRow9 = e.NewRow;
    if (newRow9.LabelSize == null)
      newRow9.LabelSize = str = giFilter.LabelSize;
    GIFilter newRow10 = e.NewRow;
    if (newRow10.FieldName != null)
      return;
    newRow10.FieldName = str = giFilter.FieldName == typeof (CheckboxCombobox.checkbox).FullName || giFilter.FieldName == typeof (CheckboxCombobox.combobox).FullName ? giFilter.FieldName : $"{giTable.Alias}.{giFilter.FieldName.Replace('.', '_')}";
  }

  protected void GIFilter_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is GIFilter row))
      return;
    Guid? designId = row.DesignID;
    if (!designId.HasValue || e.TranStatus != PXTranStatus.Completed)
      return;
    PXGenericInqGrph.ParametersChangedIndexer parametersChanged1 = PXGenericInqGrph.ParametersChanged;
    designId = row.DesignID;
    Guid designID1 = designId.Value;
    if (parametersChanged1[designID1])
      return;
    PXGenericInqGrph.ParametersChangedIndexer parametersChanged2 = PXGenericInqGrph.ParametersChanged;
    designId = row.DesignID;
    Guid designID2 = designId.Value;
    parametersChanged2[designID2] = true;
  }

  protected void GIFilter_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Designs.Current.DesignID;
  }

  protected void GIFilter_Name_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    if (this.IsCopyPasteContext)
      return;
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldUpdating, GIFilter, GIFilter.name>(cache, (GIFilter) e.Row, e.NewValue)).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw new PXException(error.Message);
    }));
  }

  protected void GIFilter_DefaultValue_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    GIFilter row = (GIFilter) e.Row;
    if (row == null || string.IsNullOrEmpty(row.FieldName))
      return;
    bool? isExpression = row.IsExpression;
    bool flag = true;
    if (!(isExpression.GetValueOrDefault() == flag & isExpression.HasValue))
      return;
    e.ReturnState = (object) this.GetFieldState(row.FieldName);
    if (e.ReturnState == null)
      return;
    ((PXFieldState) e.ReturnState).Value = cache.GetValue((object) row, "DefaultValue");
    e.Cancel = true;
  }

  protected void GIFilter_DefaultValue_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    if (e.Row is GIFilter && e.NewValue is string && string.IsNullOrEmpty((string) e.NewValue))
      e.NewValue = (object) null;
    if (e.NewValue == null)
      return;
    e.NewValue = (object) e.NewValue.ToInvariantString();
  }

  protected void GIGroupBy_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    GIGroupBy row = e.Row as GIGroupBy;
    if (row == null)
      return;
    PXCommandPreparingEventArgs.FieldDescription description;
    this.SetList<GISort.dataFieldName>(cache, (object) row, predicate: (Func<PXCache, string, bool>) ((c, fn) => GenericInquiryDesigner.IsFormulaField(fn) || c.RaiseCommandPreparing(fn, (object) null, (object) null, PXDBOperation.GroupBy | PXDBOperation.GroupByClause, c.GetItemType(), out description) && description != null && description.Expr != null && description.Expr.Oper() != SQLExpression.Operation.NULL));
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GIGroupBy>(cache, row).OnLastError((System.Action<GIRowValidationError>) (error => cache.RaiseExceptionHandling<GIGroupBy.dataFieldName>((object) row, (object) row.DataFieldName, (Exception) error.CreateSetPropertyException())));
  }

  protected void GIGroupBy_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Designs.Current.DesignID;
  }

  private bool IsTableUsedInSelect(string tableAlias)
  {
    IEnumerable<GIRelation> allRelations = this.Relations.Select().RowCast<GIRelation>();
    IEnumerable<GIResult> allResults = this.Results.Select().RowCast<GIResult>();
    IEnumerable<GITable> allTables = this.Tables.Select().RowCast<GITable>();
    return GenericInquiryHelpers.IsTableUsedInSelect(tableAlias, allRelations, allResults, allTables);
  }

  protected void GIResult_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (this.IsCopyPasteContext || !(e.Row is GIResult row))
      return;
    int? type1;
    if (!string.IsNullOrEmpty(row.ObjectName))
    {
      List<string> stringList = new List<string>();
      List<string> strDispNames = new List<string>();
      GITable tableByAlias = this.GetTableByAlias(row.ObjectName);
      if (tableByAlias != null)
      {
        this.AddTableFields(tableByAlias, false, stringList, strDispNames);
        if (!stringList.Any<string>())
          return;
        if (this.GroupBy.Select().Count > 0)
        {
          stringList.Insert(0, "$<Count>");
          strDispNames.Insert(0, "<Count>");
        }
        stringList.Insert(0, "$<All Fields>");
        strDispNames.Insert(0, "<All Fields>");
        stringList.Insert(0, string.Empty);
        strDispNames.Insert(0, string.Empty);
        PXStringListAttribute.SetList<GIResult.field>(cache, (object) row, stringList.ToArray(), strDispNames.ToArray());
        bool flag1 = GenericInquiryDesigner.IsFormulaField(row.Field);
        bool flag2 = !string.IsNullOrEmpty(row.Field);
        if (flag2)
        {
          type1 = tableByAlias.Type;
          flag2 = !type1.HasValue || type1.GetValueOrDefault() == 0;
        }
        if (flag2)
        {
          System.Type tableType = this.GetTableType(tableByAlias, typeof (GIResult.objectName).Name, cache, (object) row);
          if (tableType == (System.Type) null)
            return;
          this.SetAccessRightsFromAllGraphs(tableType);
          PXCache cach = this.Caches[tableType];
          PXCommandPreparingEventArgs.FieldDescription description;
          cach.RaiseCommandPreparing(row.Field, (object) null, (object) null, PXDBOperation.Select, cach.GetItemType(), out description);
          if (description?.Expr == null)
            cache.RaiseCommandPreparing(row.Field, (object) null, (object) null, PXDBOperation.External, cach.GetItemType(), out description);
          bool flag3 = description?.Expr == null;
          PXUIFieldAttribute.SetEnabled<GIResult.totalAggregateFunction>(cache, (object) row, flag1 || !flag3);
        }
      }
    }
    this.SetTablesList(cache, e.Row, typeof (GIResult.objectName).Name);
    List<string> stringList1 = new List<string>();
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<GITable> pxResult in this.Tables.Select())
    {
      GITable table = (GITable) pxResult;
      if (!string.IsNullOrEmpty(table.Name))
      {
        type1 = table.Type;
        int num = 1;
        if (type1.GetValueOrDefault() == num & type1.HasValue)
        {
          foreach (string str in this.GetGIFields(table, true).Select<GenericInquiryDesigner.FieldItem, string>((Func<GenericInquiryDesigner.FieldItem, string>) (x => x.Name)))
          {
            if (stringSet.Add(str))
              stringList1.Add(str);
          }
        }
        else
        {
          System.Type type2 = PXBuildManager.GetType(table.Name, false);
          if (!(type2 == (System.Type) null))
          {
            foreach (System.Type bqlField in this.Caches[type2].BqlFields)
            {
              string str = $"{table.Alias}.{this.Caches[type2].GetField(bqlField)}";
              if (stringSet.Add(str))
                stringList1.Add(str);
            }
          }
        }
      }
    }
    stringList1.Sort(new Comparison<string>(string.Compare));
    PXStringListAttribute.SetList<GIResult.schemaField>(cache, e.Row, stringList1.ToArray(), stringList1.ToArray());
    PXUIFieldAttribute.SetEnabled<GIResult.fastFilter>(cache, (object) row, this.IsFastFilterSupported(row));
    GIRowValidationError[] array = this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GIResult>(cache, row).ToArray<GIRowValidationError>();
    this.SetUiErrors(cache, (IBqlTable) row, (GIValidationError[]) array, true, "Field", "ObjectName", "SchemaField", "AggregateFunction", "TotalAggregateFunction");
  }

  private GIResult FindResultInDescription(
    GIDescription description,
    string fieldName,
    bool activeOnly = true)
  {
    return description == null ? (GIResult) null : description.Results.FirstOrDefault<GIResult>((Func<GIResult, bool>) (x =>
    {
      if (activeOnly)
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
          return false;
      }
      return string.Equals(GenericInquiryDesigner.GetSubGIFieldName(x), fieldName, StringComparison.OrdinalIgnoreCase);
    }));
  }

  protected void GIResult_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    GIResult row = (GIResult) e.Row;
    GIResult giResult = (GIResult) PXSelectBase<GIResult, PXSelect<GIResult, Where<GIResult.designID, Equal<Current<GIDesign.designID>>>, OrderBy<Desc<GIResult.lineNbr>>>.Config>.Select((PXGraph) this);
    if (giResult == null || !string.IsNullOrEmpty(row.ObjectName))
      return;
    row.ObjectName = giResult.ObjectName;
  }

  protected void GIResult_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    if (!(e.Row is GIResult row) || !(row.Field == "$<All Fields>"))
      return;
    GITable tableByAlias = this.GetTableByAlias(row.ObjectName);
    if (tableByAlias == null)
      return;
    e.Cancel = true;
    List<string> strlist = new List<string>();
    List<string> strDispNames = new List<string>();
    this.AddTableFields(tableByAlias, false, strlist, strDispNames);
    foreach (string subGIField in strlist)
    {
      GIResult instance = (GIResult) cache.CreateInstance();
      instance.ObjectName = row.ObjectName;
      instance.Field = subGIField;
      int? type = tableByAlias.Type;
      int num = 1;
      if (type.GetValueOrDefault() == num & type.HasValue)
        this.CopyResultFieldsFromSubGI(cache, instance, tableByAlias.Name, subGIField);
      cache.Insert((object) instance);
    }
  }

  protected void GIResult_DesignID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Designs.Current.DesignID;
  }

  protected void GIResult_FastFilter_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is GIResult row))
      return;
    bool? returnValue = e.ReturnValue as bool?;
    bool flag = true;
    if (!(returnValue.GetValueOrDefault() == flag & returnValue.HasValue) || this.IsFastFilterSupported(row))
      return;
    e.ReturnValue = (object) false;
  }

  protected void GIResult_ObjectName_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    GIResult row = (GIResult) e.Row;
    if (!e.ExternalCall || row == null)
      return;
    if (string.IsNullOrEmpty(row.Field) || !row.Field.StartsWith("="))
    {
      row.Field = (string) null;
      row.TotalAggregateFunction = (string) null;
      this.FillNotesAndTablesList();
    }
    cache.SetValue<GIResult.fastFilter>((object) row, (object) false);
  }

  protected void _(Events.FieldUpdated<GIResult.isActive> e)
  {
    if (!e.ExternalCall)
      return;
    this.FillNotesAndTablesList();
  }

  protected void GIResult_SchemaField_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall || !(e.Row is GIResult row))
      return;
    cache.SetValue<GIResult.fastFilter>((object) row, (object) false);
  }

  protected void GIResult_NavigationNbr_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is GIResult row) || e.OldValue != null || !row.NavigationNbr.HasValue)
      return;
    row.DefaultNav = new bool?(false);
  }

  protected void GIResult_Field_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    GIResult row = (GIResult) e.Row;
    if (row.Field == "$<All Fields>")
    {
      GITable tableByAlias = this.GetTableByAlias(row.ObjectName);
      if (tableByAlias == null)
        return;
      List<string> strlist = new List<string>();
      List<string> strDispNames = new List<string>();
      this.AddTableFields(tableByAlias, false, strlist, strDispNames);
      foreach (string subGIField in strlist)
      {
        GIResult instance = (GIResult) cache.CreateInstance();
        instance.ObjectName = row.ObjectName;
        instance.Field = subGIField;
        int? type = tableByAlias.Type;
        int num = 1;
        if (type.GetValueOrDefault() == num & type.HasValue)
          this.CopyResultFieldsFromSubGI(cache, instance, tableByAlias.Name, subGIField);
        cache.Insert((object) instance);
      }
      cache.Delete((object) row);
      this.Results.View.RequestRefresh();
    }
    else
    {
      if (!e.ExternalCall)
        return;
      if (string.IsNullOrEmpty(row.Field) || !GenericInquiryDesigner.IsFormulaField(row.Field))
        row.TotalAggregateFunction = (string) null;
      cache.SetValue<GIResult.fastFilter>((object) row, (object) false);
      GITable tableByAlias = this.GetTableByAlias(row.ObjectName);
      if (tableByAlias == null)
        return;
      int? type = tableByAlias.Type;
      int num = 1;
      if (!(type.GetValueOrDefault() == num & type.HasValue))
        return;
      this.CopyResultFieldsFromSubGI(cache, row, tableByAlias.Name, row.Field);
    }
  }

  private static bool IsFormulaField(string field) => field != null && field.StartsWith("=");

  private void CopyResultFieldsFromSubGI(
    PXCache cache,
    GIResult row,
    string subGIdesignId,
    string subGIField)
  {
    if (this.IsCopyPasteContext || string.IsNullOrEmpty(subGIField) || GenericInquiryDesigner.IsFormulaField(subGIField))
      return;
    GIDescription description = this.GenericInquiryDescriptionProvider.Get(subGIdesignId);
    if (description == null)
      return;
    GIResult resultInDescription = this.FindResultInDescription(description, subGIField, false);
    if (resultInDescription == null)
      return;
    cache.SetValue<GIResult.fastFilter>((object) row, (object) resultInDescription.FastFilter);
    cache.SetValue<GIResult.caption>((object) row, (object) resultInDescription.Caption);
    cache.SetValue<GIResult.width>((object) row, (object) resultInDescription.Width);
    cache.SetValue<GIResult.quickFilter>((object) row, (object) resultInDescription.QuickFilter);
    if (!string.IsNullOrEmpty(resultInDescription.SchemaField))
    {
      string baseObjectName = this.RemapFieldNameToBaseObjectName(resultInDescription.SchemaField, row.ObjectName);
      cache.SetValue<GIResult.schemaField>((object) row, (object) baseObjectName);
    }
    if (string.IsNullOrEmpty(resultInDescription.StyleFormula))
      return;
    string str = this.RemapFormulaFields(resultInDescription.StyleFormula, row.ObjectName);
    cache.SetValue<GIResult.styleFormula>((object) row, (object) str);
  }

  private string RemapFieldNameToBaseObjectName(string fieldWithDotSeparator, string objectName)
  {
    return $"{objectName}.{fieldWithDotSeparator.Replace('.', '_')}";
  }

  private string RemapFormulaFields(string formula, string objectName)
  {
    if (!GenericInquiryDesigner.IsFormulaField(formula))
      return formula;
    Dictionary<string, string> replaceMap = new Dictionary<string, string>();
    int numberOfFields = 0;
    this._formulaProcessor.TransformToExpression(formula, (SyFormulaFinalDelegate) (names =>
    {
      if (names == null)
        return (object) string.Empty;
      foreach (string name in names)
      {
        ++numberOfFields;
        if (!replaceMap.ContainsKey(name))
          replaceMap[name] = this.RemapFieldNameToBaseObjectName(name, objectName);
      }
      return (object) SQLExpression.Null();
    }));
    if (!replaceMap.Any<KeyValuePair<string, string>>())
      return formula;
    StringBuilder stringBuilder = new StringBuilder(formula, formula.Length + numberOfFields * (objectName.Length + 1));
    foreach (KeyValuePair<string, string> keyValuePair in replaceMap)
      stringBuilder.Replace(keyValuePair.Key, keyValuePair.Value);
    return stringBuilder.ToString();
  }

  protected void GISort_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (this.IsCopyPasteContext || !(e.Row is GISort row))
      return;
    this.SetList<GISort.dataFieldName>(cache, (object) row, predicate: (Func<PXCache, string, bool>) ((c, f) => !GenericInquiryDesigner.IsVirtualField(c, f)));
    GIRowValidationError[] array = this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GISort>(cache, row).ToArray<GIRowValidationError>();
    this.SetUiErrors(cache, (IBqlTable) row, (GIValidationError[]) array, false, "DataFieldName", "IsActive");
  }

  protected void NewFilter_Field_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    this.SetList<NewFilter.field>(cache, e.Row);
  }

  protected void ComboValues_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    ComboValues row = (ComboValues) e.Row;
    row.DesignID = this.Designs.Current.DesignID;
    row.ParamNbr = this.Parameters.Current.LineNbr;
  }

  protected void ComboValues_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  private bool IsFastFilterSupported(GIResult row, Dictionary<string, GITable> tables = null)
  {
    GITable giTable;
    if (row != null && !string.IsNullOrEmpty(row.ObjectName) && !string.IsNullOrEmpty(row.Field) && (tables ?? this.AllTables).TryGetValue(row.ObjectName, out giTable))
    {
      int? type1 = giTable.Type;
      int num = 1;
      if (type1.GetValueOrDefault() == num & type1.HasValue)
      {
        GIDescription description = this.GenericInquiryDescriptionProvider.Get(giTable.Name);
        GIResult resultInDescription = this.FindResultInDescription(description, row.Field, false);
        if (resultInDescription == null)
          return false;
        Dictionary<string, GITable> dictionary = description.Tables.ToDictionary<GITable, string, GITable>((Func<GITable, string>) (x => x.Alias), (Func<GITable, GITable>) (x => x));
        return this.IsFastFilterSupported(resultInDescription, dictionary);
      }
      System.Type type2 = PXBuildManager.GetType(giTable.Name, false);
      if (type2 != (System.Type) null)
      {
        PXCache cach = this.Caches[type2];
        bool flag = row.Field.StartsWith("=", StringComparison.OrdinalIgnoreCase);
        if (flag || !PXGenericInqGrph.IsVirtualField(cach, row.Field))
        {
          PXFieldState pxFieldState = (PXFieldState) null;
          if (!string.IsNullOrEmpty(row.SchemaField))
            pxFieldState = this.GetFieldState(row.SchemaField);
          else if (!flag)
            pxFieldState = cach.GetStateExt((object) null, row.Field) as PXFieldState;
          if (pxFieldState?.DataType != (System.Type) null)
          {
            switch (System.Type.GetTypeCode(pxFieldState.DataType))
            {
              case TypeCode.Object:
              case TypeCode.DateTime:
                return false;
            }
          }
          return true;
        }
      }
    }
    return false;
  }

  private HashSet<string> GetTablesUsedInRelations()
  {
    HashSet<string> tablesUsedInRelations = new HashSet<string>();
    foreach (PXResult<GIRelation> pxResult in this.Relations.Select())
    {
      GIRelation giRelation = (GIRelation) pxResult;
      bool? isActive = giRelation.IsActive;
      bool flag = true;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      {
        tablesUsedInRelations.Add(giRelation.ParentTable);
        tablesUsedInRelations.Add(giRelation.ChildTable);
      }
    }
    return tablesUsedInRelations;
  }

  public string[] GetAllFields()
  {
    return this.GetAllTableFields().OrderBy<(string, string), string>((Func<(string, string), string>) (r => r.TableAlias)).ThenBy<(string, string), string>((Func<(string, string), string>) (r => r.Field)).Select<(string, string), string>((Func<(string, string), string>) (r => $"[{r.TableAlias}.{r.Field}]")).ToArray<string>();
  }

  public IEnumerable<(string TableAlias, string Field)> GetAllTableFields()
  {
    GenericInquiryDesigner genericInquiryDesigner = this;
    foreach (PXResult<GITable> pxResult in genericInquiryDesigner.Tables.Select())
    {
      GITable table = (GITable) pxResult;
      if (!string.IsNullOrEmpty(table.Name))
      {
        int? type1 = table.Type;
        int num = 1;
        if (type1.GetValueOrDefault() == num & type1.HasValue)
        {
          IEnumerable<GIResult> results = genericInquiryDesigner.GenericInquiryDescriptionProvider.Get(table.Name)?.Results;
          if (results != null)
          {
            foreach (GIResult result in results.Where<GIResult>((Func<GIResult, bool>) (x =>
            {
              bool? isActive = x.IsActive;
              bool flag = true;
              return isActive.GetValueOrDefault() == flag & isActive.HasValue;
            })))
              yield return (table.Alias, GenericInquiryDesigner.GetSubGIFieldName(result));
          }
          else
            continue;
        }
        else
        {
          System.Type type2 = PXBuildManager.GetType(table.Name, false);
          if (!(type2 == (System.Type) null))
          {
            PXCache cache = genericInquiryDesigner.Caches[type2];
            foreach (string str in cache.Fields.Where<string>((Func<string, bool>) (f => !PXGenericInqGrph.IsVirtualField(cache, f))))
              yield return (table.Alias, str);
            string field1 = PXGenericInqGrph.GetDeletedDatabaseRecord(cache).Field;
            if (field1 != null)
              yield return (table.Alias, field1);
            string field2 = PXGenericInqGrph.GetDatabaseRecordStatus(cache).Field;
            if (field2 != null)
              yield return (table.Alias, field2);
          }
          else
            continue;
        }
        table = (GITable) null;
      }
    }
  }

  public string[] GetFieldsInRelation(string tableName)
  {
    if (string.IsNullOrEmpty(tableName))
      return Array.Empty<string>();
    GIRelation[] source1 = this.Relations.Select<GIRelation>();
    List<GIRelation> list = ((IEnumerable<GIRelation>) source1).Where<GIRelation>((Func<GIRelation, bool>) (r => string.Equals(r.ParentTable, tableName, StringComparison.OrdinalIgnoreCase) || string.Equals(r.ChildTable, tableName, StringComparison.OrdinalIgnoreCase))).ToList<GIRelation>();
    bool flag = true;
    while (flag)
    {
      flag = false;
      foreach (GIRelation giRelation in source1)
      {
        string[] tablesInRelation = new string[2]
        {
          giRelation.ParentTable,
          giRelation.ChildTable
        };
        if (!list.Contains(giRelation) && list.Any<GIRelation>((Func<GIRelation, bool>) (rel => ((IEnumerable<string>) tablesInRelation).Contains<string>(rel.ParentTable, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) || ((IEnumerable<string>) tablesInRelation).Contains<string>(rel.ChildTable, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))))
        {
          list.Add(giRelation);
          flag = true;
        }
      }
    }
    HashSet<string> source2 = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (GIRelation giRelation in list)
    {
      GIRelation relation = giRelation;
      if (relation != null)
      {
        foreach (PXResult<GITable> pxResult in this.Tables.Select().ToList<PXResult<GITable>>().Where<PXResult<GITable>>((Func<PXResult<GITable>, bool>) (x =>
        {
          if (string.IsNullOrEmpty(((GITable) x).Name))
            return false;
          return string.Equals(((GITable) x).Alias, relation.ParentTable, StringComparison.OrdinalIgnoreCase) || string.Equals(((GITable) x).Alias, relation.ChildTable, StringComparison.OrdinalIgnoreCase);
        })))
        {
          GITable table = (GITable) pxResult;
          int? type1 = table.Type;
          int num = 1;
          if (type1.GetValueOrDefault() == num & type1.HasValue)
          {
            IEnumerable<GenericInquiryDesigner.FieldItem> giFields = this.GetGIFields(table, true);
            EnumerableExtensions.AddRange<string>((ISet<string>) source2, giFields.Select<GenericInquiryDesigner.FieldItem, string>((Func<GenericInquiryDesigner.FieldItem, string>) (x => x.Name)));
          }
          else
          {
            System.Type type2 = PXBuildManager.GetType(table.Name, false);
            if (type2 != (System.Type) null)
            {
              PXCache cache = this.Caches[type2];
              foreach (string str in cache.Fields.Where<string>((Func<string, bool>) (f => !PXGenericInqGrph.IsVirtualField(cache, f))))
                source2.Add($"{table.Alias}.{str}");
            }
          }
        }
      }
    }
    return source2.ToArray<string>();
  }

  public string[] GetFieldsInRelation()
  {
    GIRelation relation = this.Relations.Current;
    if (relation == null)
      return Array.Empty<string>();
    List<string> stringList = new List<string>();
    PXResultset<GITable> source = this.Tables.Select();
    Expression<Func<PXResult<GITable>, bool>> predicate = (Expression<Func<PXResult<GITable>, bool>>) (x => string.Equals(((GITable) x).Alias, relation.ParentTable, StringComparison.OrdinalIgnoreCase) || string.Equals(((GITable) x).Alias, relation.ChildTable, StringComparison.OrdinalIgnoreCase));
    foreach (PXResult<GITable> pxResult in (IEnumerable<PXResult<GITable>>) source.Where<PXResult<GITable>>(predicate))
    {
      GITable giTable = (GITable) pxResult;
      System.Type type = PXBuildManager.GetType(giTable.Name, false);
      if (type != (System.Type) null)
      {
        PXCache cache = this.Caches[type];
        foreach (string str in cache.Fields.Where<string>((Func<string, bool>) (f => !PXGenericInqGrph.IsVirtualField(cache, f))))
          stringList.Add($"[{giTable.Alias}.{str}]");
      }
    }
    stringList.Sort();
    return stringList.ToArray();
  }

  public string[] GetAllParameters(bool inBrackets = true)
  {
    List<string> stringList = new List<string>();
    foreach (PXResult<GIFilter> pxResult in this.Parameters.Select())
    {
      GIFilter giFilter = (GIFilter) pxResult;
      stringList.Add(inBrackets ? $"[{giFilter.Name}]" : giFilter.Name);
    }
    return stringList.ToArray();
  }

  private Dictionary<string, GITable> AllTables
  {
    get
    {
      GIDesign design = this.Designs.Current;
      if (this._allTables == null || this._allTables.Count <= 0 || design != null && this._allTables.Any<KeyValuePair<string, GITable>>((Func<KeyValuePair<string, GITable>, bool>) (p =>
      {
        Guid? designId1 = p.Value.DesignID;
        Guid? designId2 = design.DesignID;
        if (designId1.HasValue != designId2.HasValue)
          return true;
        return designId1.HasValue && designId1.GetValueOrDefault() != designId2.GetValueOrDefault();
      })))
      {
        this._allTables = new Dictionary<string, GITable>();
        foreach (PXResult<GITable> pxResult in this.Tables.Select())
        {
          GITable giTable = (GITable) pxResult;
          if (!string.IsNullOrEmpty(giTable.Alias))
            this._allTables[giTable.Alias] = giTable;
        }
      }
      return this._allTables;
    }
  }

  private Tuple<string[], string[]> CollectList(Func<PXCache, string, bool> predicate = null)
  {
    List<string> strlist = new List<string>();
    List<string> strDispNames = new List<string>();
    foreach (string key in this.AllTables.Keys)
      this.AddTableFields(this.AllTables[key], true, strlist, strDispNames, predicate);
    return Tuple.Create<string[], string[]>(strlist.ToArray(), strDispNames.ToArray());
  }

  private void SetList<Field>(
    PXCache cache,
    object row,
    bool appendList = false,
    Func<PXCache, string, bool> predicate = null)
    where Field : IBqlField
  {
    Tuple<string[], string[]> tuple = this.CollectList(predicate);
    if (appendList)
      PXStringListAttribute.AppendList<Field>(cache, row, tuple.Item1, tuple.Item2);
    else
      PXStringListAttribute.SetList<Field>(cache, row, tuple.Item1, tuple.Item2);
  }

  private IEnumerable<GenericInquiryDesigner.FieldItem> GetGIFields(
    GITable table,
    bool needTableName,
    Func<PXCache, string, bool> predicate = null,
    bool forExternalReferencesOnly = false)
  {
    GIDescription description = this.GenericInquiryDescriptionProvider.Get(table.Name);
    if (description?.Results != null)
    {
      List<(GIResult, string)> list = description.Results.Where<GIResult>((Func<GIResult, bool>) (x =>
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        return isActive.GetValueOrDefault() == flag & isActive.HasValue && FieldCanBeAdded(description, x, predicate);
      })).Select<GIResult, (GIResult, string)>((Func<GIResult, (GIResult, string)>) (x => (x, GetDisplayName(x)))).ToList<(GIResult, string)>();
      HashSet<string> displayNameDuplicates = list.GroupBy<(GIResult, string), string>((Func<(GIResult, string), string>) (x => x.DisplayName.ToLower())).Where<IGrouping<string, (GIResult, string)>>((Func<IGrouping<string, (GIResult, string)>, bool>) (x => x.Count<(GIResult, string)>() > 1)).Select<IGrouping<string, (GIResult, string)>, string>((Func<IGrouping<string, (GIResult, string)>, string>) (x => x.Key)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach ((GIResult, string) valueTuple in list)
        yield return new GenericInquiryDesigner.FieldItem()
        {
          Name = GetFieldName(valueTuple.Item1),
          DispName = displayNameDuplicates.Contains(valueTuple.Item2) ? GetDisplayName(valueTuple.Item1, true) : valueTuple.Item2
        };
    }

    string GetFieldName(GIResult result)
    {
      return (needTableName ? table.Alias + "." : string.Empty) + GenericInquiryDesigner.GetSubGIFieldName(result);
    }

    string GetDisplayName(GIResult result, bool fullName = false)
    {
      return (needTableName ? table.Alias + "." : string.Empty) + (fullName ? result.ObjectName + "_" : string.Empty) + (result.Caption ?? result.Field);
    }

    bool FieldCanBeAdded(
      GIDescription description,
      GIResult result,
      Func<PXCache, string, bool> predicate)
    {
      if (predicate == null && !forExternalReferencesOnly)
        return true;
      (PXCache Cache, string FieldName, System.Type OriginalCacheType) underlyingField = this.GetUnderlyingField(description, result);
      if (underlyingField.Cache == null || forExternalReferencesOnly && !GenericInquiryDesigner.CanFieldBeUsedInExternalReferences(underlyingField.Cache, underlyingField.FieldName, underlyingField.OriginalCacheType, out System.Type _))
        return false;
      return predicate == null || predicate(underlyingField.Cache, underlyingField.FieldName);
    }
  }

  private (PXCache Cache, string FieldName, System.Type OriginalCacheType) GetUnderlyingField(
    GIDescription description,
    GIResult result)
  {
    GITable giTable = description.Tables.FirstOrDefault<GITable>((Func<GITable, bool>) (x => x.Alias == result.ObjectName));
    if (giTable == null)
      return ();
    int? type1 = giTable.Type;
    int num = 1;
    if (type1.GetValueOrDefault() == num & type1.HasValue)
    {
      GIDescription description1 = this.GenericInquiryDescriptionProvider.Get(giTable.Name);
      (string TableAlias, string FieldName) field = GenericInquiryDesigner.SplitDataFieldName(result.Field, '_');
      if (description1 == null || field.TableAlias == null)
        return ();
      GIResult result1 = description1.Results.FirstOrDefault<GIResult>((Func<GIResult, bool>) (x => x.ObjectName == field.TableAlias && x.Field == field.FieldName));
      return result1 == null ? () : this.GetUnderlyingField(description1, result1);
    }
    int? type2 = giTable.Type;
    if (type2.HasValue && type2.GetValueOrDefault() != 0)
      throw new NotImplementedException();
    System.Type type3 = PXBuildManager.GetType(giTable.Name, false);
    return type3 == (System.Type) null ? () : (this.Caches[type3], result.Field, type3);
  }

  internal static string GetGIFieldName(GIResult result)
  {
    if (GenericInquiryDesigner.IsFormulaField(result.Field))
      return GenericInquiryDesigner.GetFormulaFieldName(result);
    if (GenericInquiryDesigner.IsCountField(result))
      return GenericInquiryDesigner.GetCountFieldName(result);
    return !GenericInquiryDesigner.IsStringAggField(result) ? result.Field : GenericInquiryDesigner.GetStringAggFieldName(result);
  }

  private static string WithPrefix(string fieldName, GIResult result)
  {
    return $"{result.ObjectName}_{fieldName}";
  }

  private static string GetSubGIFieldName(GIResult result)
  {
    return GenericInquiryDesigner.WithPrefix(GenericInquiryDesigner.GetGIFieldName(result), result);
  }

  internal static bool IsCountField(GIResult result)
  {
    return result.Field == "$<Count>" || result.AggregateFunction == "COUNT";
  }

  internal static bool IsStringAggField(GIResult result) => result.AggregateFunction == "STRINGAGG";

  private static string GetFormulaFieldName(GIResult result)
  {
    return "Formula" + PXGenericInqGrph.GetExtFieldId(result);
  }

  private static string GetCountFieldName(GIResult result)
  {
    return "Count" + PXGenericInqGrph.GetExtFieldId(result);
  }

  private static string GetStringAggFieldName(GIResult result)
  {
    return "StringAgg" + PXGenericInqGrph.GetExtFieldId(result);
  }

  private void AddTableFields(
    GITable table,
    bool needTableName,
    List<string> strlist,
    List<string> strDispNames,
    Func<PXCache, string, bool> predicate = null)
  {
    if (table == null || string.IsNullOrEmpty(table.Name))
      return;
    List<GenericInquiryDesigner.FieldItem> source = new List<GenericInquiryDesigner.FieldItem>();
    int? type1 = table.Type;
    if (!type1.HasValue || type1.GetValueOrDefault() == 0)
    {
      System.Type type2 = PXBuildManager.GetType(table.Name, false);
      if (type2 == (System.Type) null)
        return;
      source = GenericInquiryDesigner.GetDACFieldItems((PXGraph) this, type2, needTableName, table.Alias, predicate);
      source.Sort((Comparison<GenericInquiryDesigner.FieldItem>) ((f1, f2) => strDispNames != null ? string.Compare(f1.DispName, f2.DispName, StringComparison.OrdinalIgnoreCase) : string.Compare(f1.Name, f2.Name, StringComparison.OrdinalIgnoreCase)));
    }
    int? type3 = table.Type;
    int num = 1;
    if (type3.GetValueOrDefault() == num & type3.HasValue)
    {
      bool forExternalReferencesOnly = needTableName;
      source.AddRange(this.GetGIFields(table, needTableName, predicate, forExternalReferencesOnly));
    }
    strlist.AddRange(source.Select<GenericInquiryDesigner.FieldItem, string>((Func<GenericInquiryDesigner.FieldItem, string>) (x => x.Name)));
    if (strDispNames == null)
      return;
    strDispNames.AddRange(source.Select<GenericInquiryDesigner.FieldItem, string>((Func<GenericInquiryDesigner.FieldItem, string>) (x => x.DispName)));
  }

  private static List<GenericInquiryDesigner.FieldItem> GetDACFieldItems(
    PXGraph graph,
    System.Type type,
    bool needTableName,
    string tableAlias,
    Func<PXCache, string, bool> predicate = null)
  {
    List<GenericInquiryDesigner.FieldItem> dacFieldItems = new List<GenericInquiryDesigner.FieldItem>();
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    PXCache cach = graph.Caches[type];
    foreach (string field in (List<string>) cach.Fields)
    {
      System.Type bqlField;
      if (GenericInquiryDesigner.CanFieldBeUsedInExternalReferences(cach, field, type, out bqlField))
      {
        string str = bqlField != (System.Type) null ? bqlField.Name : field;
        if (!dictionary.ContainsKey(field))
        {
          dictionary[field] = field;
          string fieldName = needTableName ? $"{tableAlias}.{field}" : field;
          PXFieldState stateExt = (PXFieldState) cach.GetStateExt((object) null, fieldName);
          if (stateExt != null && !string.IsNullOrEmpty(stateExt.DescriptionName) && (predicate == null || predicate(cach, str + "_description")))
          {
            GenericInquiryDesigner.FieldItem fieldItem = new GenericInquiryDesigner.FieldItem()
            {
              Name = needTableName ? $"{tableAlias}.{str}_description" : str + "_description",
              DispName = fieldName + "_Description"
            };
            dacFieldItems.Add(fieldItem);
          }
          if (predicate == null || predicate(cach, str))
          {
            GenericInquiryDesigner.FieldItem fieldItem = new GenericInquiryDesigner.FieldItem()
            {
              Name = needTableName ? $"{tableAlias}.{str}" : str,
              DispName = fieldName
            };
            dacFieldItems.Add(fieldItem);
          }
        }
      }
    }
    string field1 = PXGenericInqGrph.GetDeletedDatabaseRecord(cach).Field;
    if (field1 != null && !cach.Fields.Contains(field1))
    {
      string str = needTableName ? $"{tableAlias}.{field1}" : field1;
      dacFieldItems.Add(new GenericInquiryDesigner.FieldItem()
      {
        Name = str,
        DispName = str
      });
    }
    string field2 = PXGenericInqGrph.GetDatabaseRecordStatus(cach).Field;
    if (field2 != null && !cach.Fields.Contains(field2))
    {
      string str = needTableName ? $"{tableAlias}.{field2}" : field2;
      dacFieldItems.Add(new GenericInquiryDesigner.FieldItem()
      {
        Name = str,
        DispName = str
      });
    }
    return dacFieldItems;
  }

  internal static IEnumerable<string> GetDACPropertyNames(PXGraph graph, System.Type type)
  {
    return GenericInquiryDesigner.GetDACFieldItems(graph, type, false, string.Empty).Select<GenericInquiryDesigner.FieldItem, string>((Func<GenericInquiryDesigner.FieldItem, string>) (item => item.Name));
  }

  private static bool CanFieldBeUsedInExternalReferences(
    PXCache cache,
    string fieldName,
    System.Type originalCacheType,
    out System.Type bqlField)
  {
    bqlField = cache.GetBqlField(fieldName);
    return GenericInquiryDesigner.IsFormulaField(fieldName) || (!(bqlField != (System.Type) null) || BqlCommand.GetItemType(bqlField).IsAssignableFrom(originalCacheType)) && (!(bqlField == (System.Type) null) || fieldName.EndsWith("_Attributes") || fieldName.EndsWith("Signed") || cache.IsKvExtAttribute(fieldName)) && !cache.GetAttributes(fieldName).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBTimestampAttribute));
  }

  private static bool IsVirtualField(PXCache cache, string field)
  {
    return !GenericInquiryDesigner.IsFormulaField(field) && PXGenericInqGrph.IsVirtualField(cache, field);
  }

  private void SetTablesList(PXCache cache, object row, string fieldName)
  {
    List<string> stringList = new List<string>();
    foreach (PXResult<GITable> pxResult in PXSelectBase<GITable, PXSelect<GITable, Where<GITable.designID, Equal<Current<GIDesign.designID>>>>.Config>.Select((PXGraph) this))
    {
      GITable giTable = (GITable) pxResult;
      stringList.Add(giTable.Alias);
    }
    PXStringListAttribute.SetList(cache, row, fieldName, stringList.ToArray(), stringList.ToArray());
  }

  /// <summary>
  /// Sets correct field state and list of values based on field type.
  /// </summary>
  /// <param name="dataFieldName">Name of the DAC field corresponding to the value field state and list needed.</param>
  private void SetValueListInWhere<T>(PXCache cache, PXFieldSelectingEventArgs e) where T : IBqlField
  {
    GIWhere row = (GIWhere) e.Row;
    if (row == null)
      return;
    object obj = cache.GetValue((object) row, typeof (T).Name);
    if (row.DataFieldName != null)
    {
      bool? isExpression = row.IsExpression;
      bool flag = true;
      if (isExpression.GetValueOrDefault() == flag & isExpression.HasValue)
      {
        string dataFieldName = row.DataFieldName;
        string availableValues = (string) null;
        PXFieldState pxFieldState;
        if (row.DataFieldName.StartsWith("[") && row.DataFieldName.EndsWith("]"))
        {
          GIFilter giFilter = (GIFilter) PXSelectBase<GIFilter, PXSelect<GIFilter, Where<GIFilter.designID, Equal<Current<GIDesign.designID>>, And<GIFilter.name, Equal<Required<GIFilter.name>>>>>.Config>.Select((PXGraph) this, (object) row.DataFieldName.Trim('[', ']'));
          if (giFilter == null)
            return;
          pxFieldState = this.GetFieldState(giFilter.FieldName);
          availableValues = giFilter.AvailableValues;
        }
        else
          pxFieldState = this.GetFieldState(dataFieldName);
        if (pxFieldState is PXStringState pxStringState && Str.IsNullOrEmpty(pxStringState.InputMask))
        {
          pxStringState.EmptyPossible = true;
          pxStringState.IsReadOnly = false;
          pxFieldState = PXFormulaEditorState.CreateInstance((object) pxFieldState, $"_FormulaEditorView#{cache.GetItemType().FullName}_{typeof (T).Name}");
        }
        e.ReturnState = (object) pxFieldState;
        if (pxFieldState != null)
        {
          pxFieldState.Value = obj;
          if (pxFieldState.DataType == typeof (bool) && pxFieldState.Value == null)
            cache.SetValue((object) row, typeof (T).Name, (object) bool.FalseString);
          PXGenericInqGrph.TrySetAvailableValues(e.ReturnState, availableValues);
          e.Cancel = true;
          goto label_16;
        }
        goto label_16;
      }
    }
    string[] allParameters = this.GetAllParameters();
    PXStringListAttribute.SetList<T>(cache, e.Row, allParameters, allParameters);
label_16:
    if (obj != null)
      return;
    string key = row.Condition?.Trim();
    if (string.IsNullOrEmpty(key) || !(key != "NU") || !(key != "NN"))
      return;
    int num = typeof (T).Name == "value1" ? 1 : 0;
    string fieldName = num != 0 ? "Value1" : "Value2";
    if (num == 0 && !(key == "B"))
      return;
    if (!(e.ReturnState is PXFieldState pxFieldState1))
      pxFieldState1 = cache.GetStateExt((object) null, fieldName) as PXFieldState;
    string condition = PXConditionListAttribute.Conditions[key];
    pxFieldState1.Error = $"The {fieldName} cannot be empty for the {condition} condition.";
    pxFieldState1.ErrorLevel = PXErrorLevel.Error;
    e.ReturnState = (object) pxFieldState1;
  }

  private void EnsureParameterExists(PXFieldUpdatingEventArgs e)
  {
    string newValue = e.NewValue as string;
    GIWhere row = (GIWhere) e.Row;
    if (string.IsNullOrEmpty(newValue))
      return;
    bool? isExpression = row.IsExpression;
    bool flag1 = true;
    if (isExpression.GetValueOrDefault() == flag1 & isExpression.HasValue || !newValue.StartsWith("[") || !newValue.EndsWith("]"))
      return;
    string message = PXMessages.LocalizeFormatNoPrefix("A parameter with the name {0} does not exist. Do you want to add it?", (object) newValue);
    bool flag2 = false;
    string strB = newValue.Trim('[', ']');
    foreach (PXResult<GIFilter> pxResult in this.Parameters.Select())
    {
      if (string.Compare(((GIFilter) pxResult).Name, strB, true) == 0)
      {
        flag2 = true;
        break;
      }
    }
    if (!flag2 && !this.IsImport && this.Wheres.Ask("New Parameter", message, MessageButtons.YesNo, MessageIcon.Question) == WebDialogResult.Yes)
      this.Parameters.Cache.Insert((object) new GIFilter()
      {
        DesignID = this.Designs.Current.DesignID,
        Name = strB,
        FieldName = row.DataFieldName
      });
    else
      e.Cancel = true;
  }

  private void SplitFieldName(string fieldName, out string alias, out string field)
  {
    alias = (string) null;
    field = (string) null;
    if (string.IsNullOrEmpty(fieldName))
      return;
    string[] strArray = fieldName.Split(new char[1]{ '.' }, 2);
    if (strArray.Length < 2)
      return;
    alias = strArray[0].Trim();
    field = strArray[1].Trim();
  }

  private PXFieldState GetFieldState(string fieldName)
  {
    if (string.IsNullOrEmpty(fieldName) || this.AllTables == null)
      return (PXFieldState) null;
    if (fieldName == typeof (CheckboxCombobox.checkbox).FullName || fieldName == typeof (CheckboxCombobox.combobox).FullName)
      return this.Caches[typeof (CheckboxCombobox)].GetStateExt((object) null, PXBuildManager.GetType(fieldName, true).Name) as PXFieldState;
    string alias;
    string field;
    this.SplitFieldName(fieldName, out alias, out field);
    if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(field) || !this.AllTables.ContainsKey(alias))
      return (PXFieldState) null;
    GITable tableByAlias = this.GetTableByAlias(alias);
    if (tableByAlias == null)
      return (PXFieldState) null;
    PXFieldState fieldStateInternal = GenericInquiryDesigner.GetFieldStateInternal(this.GenericInquiryDescriptionProvider, this.Caches, tableByAlias, field);
    if (fieldStateInternal != null)
    {
      fieldStateInternal.Enabled = true;
      fieldStateInternal.Visible = true;
    }
    return fieldStateInternal;
  }

  internal static PXFieldState GetFieldStateInternal(
    IGenericInquiryDescriptionProvider giDescriptorProvider,
    PXCacheCollection caches,
    GITable table,
    string fieldName)
  {
    int? type1 = table.Type;
    int num = 1;
    PXCache cach;
    if (type1.GetValueOrDefault() == num & type1.HasValue)
    {
      GIDescription description = giDescriptorProvider.Get(table.Name);
      (cach, fieldName) = GenericInquiryDesigner.GetUnderlyingFieldAndCache(giDescriptorProvider, caches, description, fieldName);
      if (cach == null)
        return (PXFieldState) null;
    }
    else
    {
      System.Type type2 = PXBuildManager.GetType(table.Name, false);
      if (type2 == (System.Type) null)
        return (PXFieldState) null;
      cach = caches[type2];
    }
    return cach?.GetStateExt((object) null, fieldName) as PXFieldState;
  }

  private static bool IsValidAggregate(PXDbType? type, string aggregateFunc)
  {
    if (!type.HasValue || string.IsNullOrEmpty(aggregateFunc))
      return true;
    switch (ValFromStr.GetAggregate(aggregateFunc))
    {
      case AggregateFunction.Avg:
      case AggregateFunction.Sum:
        if (type.HasValue)
        {
          switch (type.GetValueOrDefault())
          {
            case PXDbType.Binary:
            case PXDbType.Bit:
            case PXDbType.Char:
            case PXDbType.DateTime:
            case PXDbType.NChar:
            case PXDbType.NVarChar:
            case PXDbType.Text:
            case PXDbType.Timestamp:
            case PXDbType.VarChar:
            case PXDbType.SmallDateTime:
              return false;
          }
        }
        else
          break;
        break;
    }
    return true;
  }

  protected virtual void GIDesign_PrimaryScreenID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (this.IsCopyPasteContext || !(e.Row is GIDesign row) || row.PrimaryScreenID == null || string.Equals(row.PrimaryScreenID, (string) e.OldValue, StringComparison.OrdinalIgnoreCase))
      return;
    GITable inqTable = this.PrimaryScreenInqTable.SelectSingle((object) GIScreenHelper.GetCacheName(row.PrimaryScreenID));
    if (inqTable == null || this.ContainsNavigationScreensPrimaryScreen(row.PrimaryScreenID))
      return;
    this.InsertInNavigationPrimaryScreen(row, inqTable, new bool?(true));
  }

  private void InsertInNavigationPrimaryScreen(GIDesign row, GITable inqTable, bool? newValue)
  {
    PXCache cache = this.NavigationScreens.Cache;
    GINavigationScreen instance = (GINavigationScreen) cache.CreateInstance();
    instance.DesignID = row.DesignID;
    instance.Link = row.PrimaryScreenID;
    GINavigationScreen navigationScreen = instance;
    bool? nullable = newValue;
    bool flag = true;
    string str = nullable.GetValueOrDefault() == flag & nullable.HasValue ? "I" : instance.WindowMode;
    navigationScreen.WindowMode = str;
    instance.LineNbr = new int?(1);
    this.RenumberNavigationScreens();
    cache.Insert((object) instance);
    cache.Normalize();
    this.GenerateNavigationParameters(row, instance, inqTable);
  }

  private void GenerateNavigationParameters(
    GIDesign row,
    GINavigationScreen screen,
    GITable inqTable)
  {
    PXCache cach = this.Caches[GIScreenHelper.GetCacheType(row.PrimaryScreenID)];
    Dictionary<string, GIResult> dictionary = new Dictionary<string, GIResult>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXResult<GIResult> pxResult in this.Results.Select())
    {
      GIResult giResult = (GIResult) pxResult;
      dictionary[giResult.Field] = giResult;
    }
    foreach (string key in (IEnumerable<string>) cach.Keys)
    {
      GINavigationParameter navigationParameter = this.NavigationParameters.Insert();
      navigationParameter.DesignID = row.DesignID;
      navigationParameter.NavigationScreenLineNbr = screen.LineNbr;
      navigationParameter.FieldName = key;
      navigationParameter.ParameterName = $"{inqTable.Alias}.{key}";
      GIResult giResult;
      if (dictionary.TryGetValue(navigationParameter.FieldName, out giResult))
      {
        bool? defaultNav = giResult.DefaultNav;
        bool flag = true;
        if (defaultNav.GetValueOrDefault() == flag & defaultNav.HasValue)
        {
          giResult.DefaultNav = new bool?(false);
          giResult.NavigationNbr = screen.LineNbr;
          this.Results.Update(giResult);
        }
      }
    }
    this.NavigationParameters.Cache.Normalize();
  }

  private void RenumberNavigationScreens()
  {
    PXCache cach1 = this.Caches[typeof (GINavigationScreen)];
    PXCache cach2 = this.Caches[typeof (GINavigationParameter)];
    PXCache cach3 = this.Caches[typeof (GINavigationCondition)];
    foreach (GINavigationScreen navigationScreen1 in ((IEnumerable<GINavigationScreen>) this.NavigationScreens.Select<GINavigationScreen>()).Reverse<GINavigationScreen>())
    {
      int? lineNbr1 = navigationScreen1.LineNbr;
      this.BypassInsertedHandler = true;
      PXSelect<GINavigationParameter, Where<GINavigationParameter.designID, Equal<Current<GIDesign.designID>>, And<GINavigationParameter.navigationScreenLineNbr, Equal<Required<GINavigationParameter.navigationScreenLineNbr>>>>> navigationParameters = this.ScreensNavigationParameters;
      object[] objArray1 = new object[1]
      {
        (object) lineNbr1
      };
      int? lineNbr2;
      foreach (GINavigationParameter navigationParameter1 in navigationParameters.Select<GINavigationParameter>(objArray1))
      {
        GINavigationParameter copy = (GINavigationParameter) cach2.CreateCopy((object) navigationParameter1);
        GINavigationParameter navigationParameter2 = copy;
        lineNbr2 = navigationScreen1.LineNbr;
        int? nullable = lineNbr2.HasValue ? new int?(lineNbr2.GetValueOrDefault() + 1) : new int?();
        navigationParameter2.NavigationScreenLineNbr = nullable;
        cach2.Delete((object) navigationParameter1);
        cach2.Insert((object) copy);
        cach2.Normalize();
      }
      PXSelect<GINavigationCondition, Where<GINavigationCondition.designID, Equal<Current<GIDesign.designID>>, And<GINavigationCondition.navigationScreenLineNbr, Equal<Required<GINavigationCondition.navigationScreenLineNbr>>>>> navigationConditions = this.ScreensNavigationConditions;
      object[] objArray2 = new object[1]
      {
        (object) lineNbr1
      };
      foreach (GINavigationCondition navigationCondition1 in navigationConditions.Select<GINavigationCondition>(objArray2))
      {
        GINavigationCondition copy = (GINavigationCondition) cach3.CreateCopy((object) navigationCondition1);
        GINavigationCondition navigationCondition2 = copy;
        lineNbr2 = navigationScreen1.LineNbr;
        int? nullable = lineNbr2.HasValue ? new int?(lineNbr2.GetValueOrDefault() + 1) : new int?();
        navigationCondition2.NavigationScreenLineNbr = nullable;
        cach3.Delete((object) navigationCondition1);
        cach3.Insert((object) copy);
        cach3.Normalize();
      }
      this.BypassInsertedHandler = false;
      GINavigationScreen copy1 = (GINavigationScreen) cach1.CreateCopy((object) navigationScreen1);
      GINavigationScreen navigationScreen2 = copy1;
      lineNbr2 = copy1.LineNbr;
      int? nullable1 = lineNbr2.HasValue ? new int?(lineNbr2.GetValueOrDefault() + 1) : new int?();
      navigationScreen2.LineNbr = nullable1;
      cach1.Delete((object) navigationScreen1);
      cach1.Insert((object) copy1);
      cach1.Normalize();
    }
  }

  protected virtual void GINavigationParameter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    GINavigationParameter row = e.Row as GINavigationParameter;
    if (row == null)
      return;
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes((object) row, "ParameterName"))
    {
      if (attribute is PrimaryViewValueListAttribute valueListAttribute)
      {
        bool? isExpression = row.IsExpression;
        bool flag = true;
        if (isExpression.GetValueOrDefault() == flag & isExpression.HasValue)
        {
          valueListAttribute.IsActive = true;
        }
        else
        {
          List<string> strlist = new List<string>();
          List<string> strDispNames = new List<string>();
          foreach (string key in this.AllTables.Keys)
            this.AddTableFields(this.AllTables[key], true, strlist, strDispNames);
          Tuple<string[], string[]> tuple = this.CollectList();
          valueListAttribute.IsActive = false;
          valueListAttribute.SetList(sender, tuple.Item1, tuple.Item2);
        }
      }
    }
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowSelected, GINavigationParameter>(sender, row).OnLastError((System.Action<GIRowValidationError>) (error => sender.RaiseExceptionHandling<GINavigationParameter.parameterName>((object) row, (object) row.ParameterName, (Exception) error.CreateSetPropertyException())));
  }

  protected void GINavigationParameter_ParameterName_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    GINavigationParameter row = e.Row as GINavigationParameter;
    ((IEnumerable<GIRowValidationError>) this.GenericInquiryValidationService.RunFieldValidation<PX.Data.GenericInquiry.FieldVerifying, GINavigationParameter, GINavigationParameter.parameterName>(cache, row, e.NewValue)).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw error.CreateSetPropertyException();
    }));
  }

  protected void GINavigationParameter_IsExpression_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (this.BypassInsertedHandler)
      return;
    ((GINavigationParameter) e.Row).ParameterName = (string) null;
  }

  /// <summary>Sets enabled/disabled state for a view in graph.</summary>
  private void SetEnabled(PXSelectBase view, bool value)
  {
    view.AllowInsert = view.AllowUpdate = view.AllowDelete = value;
  }

  /// <summary>Determines if specified node is Generic Inquiry.</summary>
  private bool IsGenericInquiryScreen(string screenID)
  {
    if (string.IsNullOrEmpty(screenID))
      return false;
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
    return screenIdUnsecure != null && !string.IsNullOrEmpty(screenIdUnsecure.Url) && PXSiteMap.IsGenericInquiry(screenIdUnsecure.Url);
  }

  protected void GINavigationScreen_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row is GINavigationScreen row)
    {
      int num1;
      if (row.Link != null)
      {
        int? lineNbr = row.LineNbr;
        int num2 = 1;
        if (lineNbr.GetValueOrDefault() == num2 & lineNbr.HasValue)
        {
          num1 = string.Equals(row.Link, this.Designs.Current.PrimaryScreenID, StringComparison.OrdinalIgnoreCase) ? 1 : 0;
          goto label_5;
        }
      }
      num1 = 0;
label_5:
      bool flag1 = num1 != 0;
      PXUIFieldAttribute.SetEnabled<GINavigationScreen.link>(sender, (object) row, !flag1);
      PXUIFieldAttribute.SetEnabled<GINavigationScreen.isActive>(sender, (object) row, !flag1 && row.WindowMode == "L");
      int num3;
      if (flag1)
      {
        bool? replacePrimaryScreen = this.Designs.Current.ReplacePrimaryScreen;
        bool flag2 = true;
        num3 = replacePrimaryScreen.GetValueOrDefault() == flag2 & replacePrimaryScreen.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
      bool flag3 = num3 != 0;
      PXUIFieldAttribute.SetEnabled<GINavigationScreen.windowMode>(sender, (object) row, !flag3);
      if (this.IsCopyPasteContext)
      {
        this.FillNavigationNbrList();
        PXWindowModeAttribute.SetList<GINavigationScreen.windowMode>(sender, (object) row, PXBaseRedirectException.WindowMode.Same, PXBaseRedirectException.WindowMode.New, PXBaseRedirectException.WindowMode.NewWindow, PXBaseRedirectException.WindowMode.Layer, PXBaseRedirectException.WindowMode.InlineWindow);
      }
      else if (flag3)
        PXWindowModeAttribute.SetList<GINavigationScreen.windowMode>(sender, (object) row, PXBaseRedirectException.WindowMode.InlineWindow);
      else
        PXWindowModeAttribute.SetList<GINavigationScreen.windowMode>(sender, (object) row, PXBaseRedirectException.WindowMode.Same, PXBaseRedirectException.WindowMode.New, PXBaseRedirectException.WindowMode.NewWindow, PXBaseRedirectException.WindowMode.Layer);
      if (flag3)
        row.WindowMode = "I";
      else if (row.WindowMode == null || row.WindowMode == "I")
        row.WindowMode = "S";
      this.NavigationConditions.AllowSelect = row.WindowMode == "L";
    }
    else
      this.NavigationConditions.AllowSelect = false;
  }

  private bool ContainsNavigationScreensPrimaryScreen(string PrimaryScreenID)
  {
    return ((IEnumerable<GINavigationScreen>) this.NavigationScreens.Select<GINavigationScreen>()).Any<GINavigationScreen>((Func<GINavigationScreen, bool>) (result =>
    {
      int? lineNbr = result.LineNbr;
      int num = 1;
      return lineNbr.GetValueOrDefault() == num & lineNbr.HasValue && string.Equals(result.Link, PrimaryScreenID, StringComparison.OrdinalIgnoreCase);
    }));
  }

  private bool IsScreenNavigatedByKeys(int? lineNbr)
  {
    Dictionary<string, bool> dictionary = new Dictionary<string, bool>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.ScreensNavigationParameters.Cache.Normalize();
    PXCache cach = this.Caches[GIScreenHelper.GetCacheType(this.Designs.Current.PrimaryScreenID)];
    foreach (PXResult<GINavigationParameter> pxResult in this.ScreensNavigationParameters.Select((object) lineNbr))
    {
      GINavigationParameter navigationParameter = (GINavigationParameter) pxResult;
      dictionary[navigationParameter?.ParameterFieldName ?? string.Empty] = false;
      foreach (string key in (IEnumerable<string>) cach.Keys)
      {
        if (string.Equals(navigationParameter?.ParameterFieldName ?? string.Empty, key, StringComparison.OrdinalIgnoreCase))
        {
          dictionary[navigationParameter?.ParameterFieldName ?? string.Empty] = true;
          break;
        }
      }
    }
    return !dictionary.ContainsValue(false);
  }

  protected void GINavigationScreen_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    this.GenericInquiryValidationService.RunRowValidation<PX.Data.GenericInquiry.RowDeleting, GINavigationScreen>(cache, (GINavigationScreen) e.Row).OnLastError((System.Action<GIRowValidationError>) (error =>
    {
      throw new PXException(error.Message);
    }));
  }

  protected void GINavigationScreen_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is GINavigationScreen row) || row.Link == null)
      return;
    foreach (PXResult<GIResult> pxResult in this.ResultsByNavigationScreen.Select((object) row.LineNbr))
    {
      GIResult giResult = (GIResult) pxResult;
      giResult.NavigationNbr = new int?();
      this.Results.Update(giResult);
    }
  }

  protected void GINavigationScreen_WindowMode_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is GINavigationScreen row))
      return;
    if (row.Link != null && row.WindowMode == "L")
    {
      foreach (PXResult<GIResult> pxResult in this.ResultsByNavigationScreen.Select((object) row.LineNbr))
      {
        GIResult giResult = (GIResult) pxResult;
        giResult.NavigationNbr = new int?();
        this.Results.Update(giResult);
      }
    }
    bool? isActive = row.IsActive;
    bool flag = false;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue) || !(row.WindowMode != "L"))
      return;
    row.IsActive = new bool?(true);
  }

  protected void _(Events.RowSelected<GINavigationCondition> e)
  {
    this.SetList<GINavigationCondition.dataField>(e.Cache, (object) e.Row, predicate: (Func<PXCache, string, bool>) ((c, f) => !GenericInquiryDesigner.IsVirtualField(c, f)));
  }

  protected void _(
    Events.FieldSelecting<GINavigationCondition.valueSt> e)
  {
    this.SetValueListInNavConditions<GINavigationCondition.valueSt>(e);
  }

  protected void _(
    Events.FieldSelecting<GINavigationCondition.valueSt2> e)
  {
    this.SetValueListInNavConditions<GINavigationCondition.valueSt2>(e);
  }

  /// <param name="notEvent">Unused param to inform the system that the method is not an event</param>
  private void SetValueListInNavConditions<T>(Events.FieldSelecting<T> e, bool notEvent = true) where T : class, IBqlField
  {
    PXStringListAttribute.SetList<T>(e.Cache, e.Row, new string[0], new string[0]);
    GINavigationCondition row = (GINavigationCondition) e.Row;
    if (row == null)
      return;
    if (row.DataField != null)
    {
      bool? isExpression = row.IsExpression;
      bool flag = true;
      if (isExpression.GetValueOrDefault() == flag & isExpression.HasValue)
      {
        PXFieldState fieldState = this.GetFieldState(row.DataField);
        e.ReturnState = (object) fieldState;
        if (fieldState == null)
          return;
        fieldState.Value = e.Cache.GetValue((object) row, typeof (T).Name);
        if (fieldState.DataType == typeof (bool) && fieldState.Value == null)
          e.Cache.SetValue((object) row, typeof (T).Name, (object) bool.FalseString);
        e.Cancel = true;
        return;
      }
    }
    string[] allParameters = this.GetAllParameters();
    PXStringListAttribute.SetList<T>(e.Cache, e.Row, allParameters, allParameters);
  }

  protected void _(
    Events.FieldUpdated<GINavigationCondition.isExpression> e)
  {
    GINavigationCondition row = (GINavigationCondition) e.Row;
    if (row == null || this.BypassInsertedHandler)
      return;
    row.ValueSt = (string) null;
    row.ValueSt2 = (string) null;
  }

  protected void _(Events.RowDeleting<GIDesign> e)
  {
    WebDialogResult? nullable = this.AskIfGIIsUsedByAnotherGI((Guid?) e.Row?.DesignID, "The inquiry is used in generic inquiries {0}. Do you want to delete this inquiry?", this.DeleteUsedGIDialog);
    if (!nullable.HasValue || nullable.GetValueOrDefault() != WebDialogResult.Cancel)
      return;
    e.Cancel = true;
  }

  protected void _(Events.RowDeleting<GIResult> e)
  {
    if (!e.ExternalCall)
      return;
    WebDialogResult? nullable1 = this.AskIfFieldIsUsedByAnotherGI(e.Row, "The field is used in generic inquiries {0}. Do you want to remove the field from the inquiry output?");
    if (!nullable1.HasValue)
      return;
    if (nullable1.GetValueOrDefault() == WebDialogResult.Cancel)
      e.Cancel = true;
    WebDialogResult? nullable2 = nullable1;
    WebDialogResult webDialogResult = WebDialogResult.OK;
    this._additionalResultsRefreshRequired = nullable2.GetValueOrDefault() == webDialogResult & nullable2.HasValue;
  }

  protected void _(Events.RowDeleted<GIResult> e)
  {
    if (!e.ExternalCall || !this._additionalResultsRefreshRequired)
      return;
    this.Results.View.RequestRefresh();
  }

  private WebDialogResult? AskIfGIIsUsedByAnotherGI(
    Guid? designId,
    string messageTemplate,
    PXFilter<GenericInquiryDesigner.CustomMessageDialogParams> dialog)
  {
    if (!designId.HasValue)
      return new WebDialogResult?();
    List<(string, Guid)> list = this.GenericInquiryReferenceInfoProvider.GetReferencesTo(designId.Value).ToList<(string, Guid)>();
    if (!list.Any<(string, Guid)>())
      return new WebDialogResult?();
    string referenceNames = this.GetReferenceNames((IEnumerable<(string, Guid)>) list);
    string str = PXMessages.LocalizeFormatNoPrefix(messageTemplate, (object) referenceNames);
    dialog.Current.Message = str;
    return new WebDialogResult?(dialog.AskExt(true));
  }

  private WebDialogResult? AskIfFieldIsUsedByAnotherGI(GIResult row, string messageTemplate)
  {
    if ((row != null ? (!row.DesignID.HasValue ? 1 : 0) : 1) != 0 || string.IsNullOrEmpty(row.Field) || string.IsNullOrEmpty(row.ObjectName))
      return new WebDialogResult?();
    Guid designIdToLookFor = row.DesignID.Value;
    string fieldName = GenericInquiryDesigner.GetSubGIFieldName(row);
    List<(string, Guid)> list1 = this.GenericInquiryReferenceInfoProvider.GetReferencesTo(designIdToLookFor).ToList<(string, Guid)>();
    if (!list1.Any<(string, Guid)>())
      return new WebDialogResult?();
    List<(string, Guid)> list2 = list1.Where<(string, Guid)>((Func<(string, Guid), bool>) (x => this.IsFieldUsedInGI(x.designId, fieldName))).ToList<(string, Guid)>();
    if (!list2.Any<(string, Guid)>())
      return new WebDialogResult?();
    string referenceNames = this.GetReferenceNames((IEnumerable<(string, Guid)>) list2);
    this.DeleteUsedFieldDialog.Current.Message = PXMessages.LocalizeFormatNoPrefix(messageTemplate, (object) referenceNames);
    return new WebDialogResult?(this.DeleteUsedFieldDialog.AskExt(true));
  }

  private bool IsFieldUsedInGI(Guid designId, string fieldName)
  {
    GIDescription description = this.GenericInquiryDescriptionProvider.Get(designId);
    if (description == null)
      return false;
    Guid? currentDesignId = this.CurrentDesign.Current.DesignID;
    foreach (string str in description.Tables.Where<GITable>((Func<GITable, bool>) (x => x.Name.Equals(currentDesignId.ToString(), StringComparison.OrdinalIgnoreCase))).Select<GITable, string>((Func<GITable, string>) (x => x.Alias)))
    {
      string alias = str;
      string fullFieldName = $"{alias}.{fieldName}";
      string fieldNameInFormula = $"[{fullFieldName}]";
      if (description.Results.Any<GIResult>((Func<GIResult, bool>) (x =>
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        return isActive.GetValueOrDefault() == flag & isActive.HasValue && x.ObjectName.Equals(alias, StringComparison.OrdinalIgnoreCase) && IsThisField(x.Field, fieldName, fieldNameInFormula);
      })) || description.Relations.Any<GIRelation>((Func<GIRelation, bool>) (r =>
      {
        bool? isActive = r.IsActive;
        bool flag = true;
        if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
          return false;
        if (r.ParentTable.Equals(alias, StringComparison.OrdinalIgnoreCase) && description.Ons.Any<GIOn>((Func<GIOn, bool>) (o =>
        {
          int? relationNbr = o.RelationNbr;
          int? lineNbr = r.LineNbr;
          return relationNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & relationNbr.HasValue == lineNbr.HasValue && IsThisField(o.ParentField, fieldName, fieldNameInFormula);
        })))
          return true;
        return r.ChildTable.Equals(alias, StringComparison.OrdinalIgnoreCase) && description.Ons.Any<GIOn>((Func<GIOn, bool>) (o =>
        {
          int? relationNbr = o.RelationNbr;
          int? lineNbr = r.LineNbr;
          return relationNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & relationNbr.HasValue == lineNbr.HasValue && IsThisField(o.ChildField, fieldName, fieldNameInFormula);
        }));
      })) || description.Wheres.Any<GIWhere>((Func<GIWhere, bool>) (x =>
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
          return false;
        return IsThisField(x.DataFieldName, fullFieldName, fieldNameInFormula) || IsThisField(x.Value1, fullFieldName, fieldNameInFormula) || IsThisField(x.Value2, fullFieldName, fieldNameInFormula);
      })) || description.GroupBys.Any<GIGroupBy>((Func<GIGroupBy, bool>) (x =>
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        return isActive.GetValueOrDefault() == flag & isActive.HasValue && IsThisField(x.DataFieldName, fullFieldName, fieldNameInFormula);
      })) || description.Sorts.Any<GISort>((Func<GISort, bool>) (x =>
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        return isActive.GetValueOrDefault() == flag & isActive.HasValue && IsThisField(x.DataFieldName, fullFieldName, fieldNameInFormula);
      })) || description.NavigationConditions.Any<GINavigationCondition>((Func<GINavigationCondition, bool>) (x =>
      {
        bool? isActive = x.IsActive;
        bool flag = true;
        if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
          return false;
        return IsThisField(x.DataField, fullFieldName, fieldNameInFormula) || IsThisField(x.ValueSt, fullFieldName, fieldNameInFormula) || IsThisField(x.ValueSt2, fullFieldName, fieldNameInFormula);
      })))
        return true;
    }
    return false;

    static bool IsThisField(string whereToSearch, string field, string formulaRepresentation)
    {
      if (!string.IsNullOrEmpty(whereToSearch) && whereToSearch.Equals(field, StringComparison.OrdinalIgnoreCase))
        return true;
      return GenericInquiryDesigner.IsFormulaField(whereToSearch) && Str.Contains(whereToSearch, formulaRepresentation, StringComparison.OrdinalIgnoreCase);
    }
  }

  private string GetReferenceNames(IEnumerable<(string Name, Guid _)> references)
  {
    return string.Join(", ", references.Select<(string, Guid), string>((Func<(string, Guid), string>) (x => x.Name)));
  }

  private PXGraph CreateCopyOfCurrentDesignWithUniqueName()
  {
    string name = this.CurrentDesign.Current?.Name;
    if (string.IsNullOrEmpty(name))
      return (PXGraph) null;
    PXCopyPasteData<PXGraph> currentUserClipboard = PXCopyPasteData<PXGraph>.CurrentUserClipboard;
    string uniqueDesignName = this.GetUniqueDesignName(name);
    GenericInquiryDesigner instance = PXGraph.CreateInstance<GenericInquiryDesigner>();
    PXView view = instance.Views[this.PrimaryView];
    PXCopyPasteData<PXGraph>.SaveClipboard(currentUserClipboard);
    currentUserClipboard.CopyFrom((PXGraph) this);
    currentUserClipboard.PasteTo((PXGraph) instance);
    view.Cache.SetValue<GIDesign.name>(view.Cache.Current, (object) uniqueDesignName);
    instance.Persist();
    return (PXGraph) instance;
  }

  private string GetUniqueDesignName(string currentName)
  {
    if (GenericInquiryDesigner.EndsWithDigit.IsMatch(currentName))
      currentName = currentName.Substring(0, currentName.LastIndexOf('-'));
    HashSet<string> hashSet = this.Designs.Select().RowCast<GIDesign>().Where<GIDesign>((Func<GIDesign, bool>) (x => x.Name.StartsWith(currentName, StringComparison.OrdinalIgnoreCase))).Select<GIDesign, string>((Func<GIDesign, string>) (x => x.Name)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    int num = 0;
    while (num++ < 1000)
    {
      string uniqueDesignName = $"{currentName}-{num}";
      if (!hashSet.Contains(uniqueDesignName))
        return uniqueDesignName;
    }
    return $"{currentName}-{Guid.NewGuid():N}";
  }

  private static (PXCache Cache, string fieldName) GetUnderlyingFieldAndCache(
    IGenericInquiryDescriptionProvider giDescrProvider,
    PXCacheCollection caches,
    GIDescription description,
    string fieldName)
  {
    if (description == null)
      return ();
    (string str1, string str2) = GenericInquiryDesigner.SplitDataFieldName(fieldName, '_');
    GITable giTable = description.Tables.FirstOrDefault<GITable>((Func<GITable, bool>) (x => string.Equals(x.Alias, str1, StringComparison.OrdinalIgnoreCase)));
    if (giTable == null)
      return ();
    int? type1 = giTable.Type;
    int num = 1;
    if (type1.GetValueOrDefault() == num & type1.HasValue)
    {
      GIDescription description1 = giDescrProvider.Get(giTable.Name);
      return GenericInquiryDesigner.GetUnderlyingFieldAndCache(giDescrProvider, caches, description1, str2);
    }
    type1 = giTable.Type;
    if (type1.HasValue && type1.GetValueOrDefault() != 0)
      throw new NotImplementedException();
    System.Type type2 = PXBuildManager.GetType(giTable.Name, false);
    return !(type2 == (System.Type) null) ? (caches[type2], str2) : ();
  }

  public class NestedFilterHeader : FilterHeader
  {
  }

  public class NestedFilterRow : FilterRow
  {
  }

  public class PXFormulaEditor_AddParametersAttribute : PXFormulaEditor.OptionsProviderAttribute
  {
    public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      GenericInquiryDesigner genericInquiryDesigner = (GenericInquiryDesigner) graph;
      foreach (string str in ((IEnumerable<string>) genericInquiryDesigner.GetAllParameters()).Concat<string>((IEnumerable<string>) genericInquiryDesigner.GetAllFields()))
        options.Add(new FormulaOption()
        {
          Category = "Fields & Parameters",
          Value = str
        });
    }
  }

  public class PXFormulaEditor_AddRelationFields : PXFormulaEditor.OptionsProviderAttribute
  {
    public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      GenericInquiryDesigner genericInquiryDesigner = (GenericInquiryDesigner) graph;
      string[] fieldsInRelation = genericInquiryDesigner.GetFieldsInRelation();
      foreach (string str in ((IEnumerable<string>) genericInquiryDesigner.GetAllParameters()).Concat<string>((IEnumerable<string>) fieldsInRelation))
        options.Add(new FormulaOption()
        {
          Category = "Fields & Parameters",
          Value = str
        });
    }
  }

  public class PXFormulaEditor_AddNavigationParameterFields : 
    PXFormulaEditor.OptionsProviderAttribute
  {
    public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      GenericInquiryDesigner genericInquiryDesigner = (GenericInquiryDesigner) graph;
      string[] allFields = genericInquiryDesigner.GetAllFields();
      foreach (string str in ((IEnumerable<string>) genericInquiryDesigner.GetAllParameters(false)).Select<string, string>((Func<string, string>) (f => "@" + f)).Concat<string>((IEnumerable<string>) allFields))
        options.Add(new FormulaOption()
        {
          Category = "Fields & Parameters",
          Value = str
        });
    }
  }

  public class PXFormulaEditor_AddFieldsAttribute : PXFormulaEditor.OptionsProviderAttribute
  {
    public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      foreach ((string TableAlias, string Field) allTableField in ((GenericInquiryDesigner) graph).GetAllTableFields())
        options.Add(new FormulaOption()
        {
          Category = "Fields/" + allTableField.TableAlias,
          Value = $"[{allTableField.TableAlias}.{allTableField.Field}]"
        });
    }
  }

  /// <exclude />
  private class FieldItem
  {
    public string Name;
    public string DispName;
  }

  [PXHidden]
  public class CustomMessageDialogParams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    public string Message { get; set; }

    /// <exclude />
    public abstract class message : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GenericInquiryDesigner.CustomMessageDialogParams.message>
    {
    }
  }
}
