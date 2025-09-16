// Decompiled with JetBrains decompiler
// Type: PX.SM.TranslationSetMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Mobile.Legacy;
using PX.Data;
using PX.Translation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

#nullable disable
namespace PX.SM;

/// <exclude />
public class TranslationSetMaint : PXGraph<TranslationSetMaint, LocalizationTranslationSet>
{
  public const string STANDALONE_SCREEN_ID = "00000000";
  public PXSelect<LocalizationTranslationSet> TranslationSet;
  public PXSelect<LocalizationTranslationSetItem, Where<LocalizationTranslationSetItem.setId, Equal<Current<LocalizationTranslationSet.id>>>> TranslationSetItem;
  public PXSelectSiteMapTree<PX.Data.True, PX.Data.True, PX.Data.True, PX.Data.True, PX.Data.True> SiteMap;
  public PXAction<LocalizationTranslationSet> AddToGrid;
  public PXAction<LocalizationTranslationSet> AddStandalonePages;
  public PXAction<LocalizationTranslationSet> ActivateAllScreens;
  public PXAction<LocalizationTranslationSet> DeactivateAllScreens;
  public PXAction<LocalizationTranslationSet> Collect;

  [PXButton]
  [PXUIField(DisplayName = "Add To Grid")]
  public virtual IEnumerable addToGrid(PXAdapter adapter)
  {
    if (this.SiteMap.Current != null)
    {
      if (!string.IsNullOrEmpty(this.SiteMap.Current.ScreenID) && !string.IsNullOrEmpty(this.SiteMap.Current.Graphtype))
      {
        this.AddScreenToGrid(this.TranslationSet.Current.Id, this.SiteMap.Current.ScreenID);
      }
      else
      {
        Guid? nodeId1 = this.SiteMap.Current.NodeID;
        if (nodeId1.HasValue)
        {
          Guid? id = this.TranslationSet.Current.Id;
          nodeId1 = this.SiteMap.Current.NodeID;
          Guid nodeId2 = nodeId1.Value;
          this.AddFolderToGrid(id, nodeId2);
        }
      }
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Add Standalone Pages")]
  public IEnumerable addStandalonePages(PXAdapter adapter)
  {
    this.AddScreenToGrid(this.TranslationSet.Current.Id, "00000000");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Activate All")]
  public IEnumerable activateAllScreens(PXAdapter adapter)
  {
    this.SetActiveToSetItems(true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Deactivate All")]
  public IEnumerable deactivateAllScreens(PXAdapter adapter)
  {
    this.SetActiveToSetItems(false);
    return adapter.Get();
  }

  public TranslationSetMaint()
  {
    PXStringListAttribute.SetList<LocalizationTranslationSet.resourceToCollect>(this.TranslationSet.Cache, (object) null, ResourceCollectingManager.AllUnboundResourceTypes, ResourceCollectingManager.AllUnboundResourceNames);
  }

  private void SetActiveToSetItems(bool isActive)
  {
    foreach (PXResult<LocalizationTranslationSetItem> pxResult in this.TranslationSetItem.Select())
    {
      LocalizationTranslationSetItem translationSetItem = (LocalizationTranslationSetItem) pxResult;
      translationSetItem.IsActive = new bool?(isActive);
      this.TranslationSetItem.Update(translationSetItem);
    }
  }

  private void AddFolderToGrid(Guid? setId, Guid nodeId)
  {
    PXSiteMapNode nodeFromKeyUnsecure = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(nodeId);
    if (nodeFromKeyUnsecure == null)
      return;
    foreach (PXSiteMapNode descendant in PXSiteMap.Provider.GetDescendants(nodeFromKeyUnsecure))
    {
      if (!string.IsNullOrEmpty(descendant.ScreenID) && !string.IsNullOrEmpty(descendant.GraphType))
        this.AddScreenToGrid(setId, descendant.ScreenID);
    }
  }

  protected void AddScreenToGrid(Guid? setId, string screenId)
  {
    LocalizationTranslationSetItem translationSetItem = new LocalizationTranslationSetItem()
    {
      SetId = setId,
      ScreenID = screenId
    };
    if (this.TranslationSetItem.Locate(translationSetItem) != null)
      return;
    this.TranslationSetItem.Insert(translationSetItem);
  }

  [PXButton]
  [PXUIField(DisplayName = "Collect")]
  public IEnumerable collect(PXAdapter adapter)
  {
    if (this.TranslationSet.Current != null && this.TranslationSet.View.Ask((object) null, "Warning", "This operation may take a significant amount of time. Do you want to continue?", MessageButtons.YesNo, MessageIcon.Question) == WebDialogResult.Yes)
    {
      string appPath = HostingEnvironment.ApplicationPhysicalPath;
      Provider.PrepareSiteMap();
      PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => TranslationSetMaint.CollectStrings(appPath, this.GetSetItemsForCollecting(), this.TranslationSet.Current)));
    }
    return adapter.Get();
  }

  public List<LocalizationTranslationSetItem> GetSetItemsForCollecting()
  {
    List<LocalizationTranslationSetItem> itemsForCollecting = new List<LocalizationTranslationSetItem>();
    foreach (PXResult<LocalizationTranslationSetItem, PX.SM.SiteMap> pxResult in PXSelectBase<LocalizationTranslationSetItem, PXSelectJoin<LocalizationTranslationSetItem, LeftJoin<PX.SM.SiteMap, On<PX.SM.SiteMap.screenID, Equal<LocalizationTranslationSetItem.screenID>>>, Where<LocalizationTranslationSetItem.setId, Equal<Current<LocalizationTranslationSet.id>>, And<LocalizationTranslationSetItem.isActive, Equal<PX.Data.True>>>>.Config>.Select((PXGraph) this))
    {
      LocalizationTranslationSetItem translationSetItem1 = (LocalizationTranslationSetItem) pxResult;
      PX.SM.SiteMap siteMap = (PX.SM.SiteMap) pxResult;
      LocalizationTranslationSetItem translationSetItem2 = new LocalizationTranslationSetItem()
      {
        SetId = translationSetItem1.SetId,
        ScreenID = translationSetItem1.ScreenID
      };
      translationSetItem2.NameForStringCollection = siteMap != null ? Path.GetFileName(siteMap.Url) : translationSetItem1.ScreenID.Trim() + ".aspx";
      itemsForCollecting.Add(translationSetItem2);
    }
    return itemsForCollecting;
  }

  public static void CollectStrings(
    string appPath,
    List<LocalizationTranslationSetItem> items,
    LocalizationTranslationSet set)
  {
    if (string.IsNullOrEmpty(appPath) || items == null || set == null)
      return;
    TranslationSetMaint instance = PXGraph.CreateInstance<TranslationSetMaint>();
    TranslationSetMaint.SetBeforeStringCollectingState(instance, set);
    ResourceCollectingManager.CollectSettings = new CollectStringsSettings?(CollectStringsSettings.TranslationSet);
    LocalizationResourceType[] resourceTypesFromString = ResourceCollectingManager.ParseResourceTypesFromString(set.ResourceToCollect);
    if (items.Count == 0 && resourceTypesFromString.Length == 0)
      throw new PXException("You must provide items for the translation set before string collection.");
    LocalizationResourceType[] resourceTypesToCollect = ResourceCollectingManager.EnsureBoundResourceTypes(resourceTypesFromString);
    TranslationMaint.DoCollectStrings(appPath, items, resourceTypesToCollect, instance);
    TranslationSetMaint.SetAfterStringCollectingState(instance, set);
  }

  private void CorrectCollectActionEnable()
  {
    this.Collect.SetEnabled(!string.IsNullOrEmpty(this.TranslationSet.Current.Description) && !this.TranslationSet.Cache.IsDirty && !this.TranslationSetItem.Cache.IsDirty);
  }

  private static void SetBeforeStringCollectingState(
    TranslationSetMaint graph,
    LocalizationTranslationSet set)
  {
    set.SystemVersion = (string) null;
    set.SystemTime = new System.DateTime?();
    set.IsCollected = new bool?(false);
    graph.TranslationSet.Update(set);
    foreach (PXResult<LocalizationTranslationSetItem> pxResult in graph.TranslationSetItem.Select())
    {
      LocalizationTranslationSetItem translationSetItem = (LocalizationTranslationSetItem) pxResult;
      translationSetItem.IsCollected = new bool?(false);
      graph.TranslationSetItem.Update(translationSetItem);
    }
    graph.Persist();
  }

  private static void SetAfterStringCollectingState(
    TranslationSetMaint graph,
    LocalizationTranslationSet set)
  {
    set.SystemVersion = PXVersionInfo.Version;
    set.SystemTime = new System.DateTime?(System.DateTime.UtcNow);
    set.IsCollected = new bool?(true);
    graph.TranslationSet.Update(set);
    graph.Persist();
  }

  protected virtual void LocalizationTranslationSet_Id_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void LocalizationTranslationSet_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is LocalizationTranslationSet row))
      return;
    row.CurrentSystemVersion = PXVersionInfo.Version;
    this.CorrectCollectActionEnable();
  }

  protected virtual void LocalizationTranslationSet_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    this.CorrectCollectActionEnable();
  }

  protected virtual void LocalizationTranslationSetItem_IsCollected_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is LocalizationTranslationSetItem row))
      return;
    bool? isCollected = row.IsCollected;
    bool flag = true;
    if (!(isCollected.GetValueOrDefault() == flag & isCollected.HasValue))
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(false), new bool?(false), length: new int?(4), fieldName: typeof (LocalizationTranslationSetItem.isCollected).Name, displayName: "Collected", error: "Collected", errorLevel: PXErrorLevel.RowInfo, enabled: new bool?(false), visible: new bool?(true), readOnly: new bool?(false));
  }

  protected virtual void LocalizationTranslationSetItem_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (!(e.Row is LocalizationTranslationSetItem row))
      return;
    Guid? nullable = row.SetId;
    if (nullable.HasValue || this.TranslationSet.Current == null)
      return;
    nullable = this.TranslationSet.Current.Id;
    if (!nullable.HasValue)
      return;
    row.SetId = this.TranslationSet.Current.Id;
  }

  protected virtual void LocalizationTranslationSetItem_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is LocalizationTranslationSetItem row) || string.IsNullOrEmpty(row.ScreenID))
      return;
    if (string.CompareOrdinal(row.ScreenID, "00000000") != 0)
    {
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(row.ScreenID);
      if (mapNodeByScreenId == null)
        return;
      row.Title = mapNodeByScreenId.Title;
    }
    else
      row.Title = "Standalone Screens";
  }
}
