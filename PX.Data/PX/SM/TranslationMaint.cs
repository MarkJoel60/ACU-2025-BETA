// Decompiled with JetBrains decompiler
// Type: PX.SM.TranslationMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Mobile.Legacy;
using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.PushNotifications;
using PX.Data.Update;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Export.Excel.Core;
using PX.Metadata;
using PX.Translation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
public class TranslationMaint : 
  PXGraph<TranslationMaint>,
  PXImportAttribute.IPXPrepareItems,
  ICanAlterSiteMap
{
  private static System.Type usageDetailsSource;
  private readonly TranslationMaint.Importer _importer;
  public PXFilter<LangFilter> LanguageFilter;
  [PXImport(typeof (LangFilter))]
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<LocalizationRecord, OrderBy<Asc<LocalizationRecord.neutralValue>>> DeltaResourcesDistinct;
  [PXImport(typeof (LangFilter))]
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<LocalizationRecordObsolete, OrderBy<Asc<LocalizationRecordObsolete.neutralValue>>> DeltaResourcesDistinctObsolete;
  public PXSelect<LocalizationExceptionRecord> ExceptionalResources;
  public PXSelect<LocalizationExceptionRecordObsolete> ExceptionalResourcesObsolete;
  public PXSelect<PX.Translation.LocalizationTranslation> LocalizationTranslation;
  public PXSelect<PX.Translation.LocalizationResource> LocalizationResource;
  public PXSelect<PX.Translation.LocalizationValue> LocalizationValue;
  public PXSelect<LocalizationResourceByScreen> ResourceByScreen;
  public PXFilter<PX.Translation.LocalizationRecordCache> LocalizationRecordCache;
  public PXSave<LangFilter> Save;
  public PXCancel<LangFilter> Cancel;
  public PXAction<LangFilter> CollectStrings;
  private Dictionary<string, LocalizationRecord> cachedParents;
  private Dictionary<string, LocalizationRecordObsolete> cachedParentsObsolete;
  public PXAction<LangFilter> DeleteObsoleteStrings;
  public PXAction<LangFilter> ViewUsageDetails;
  public PXAction<LangFilter> ViewUsageObsoleteDetails;
  public PXAction<LangFilter> ViewExceptionalUsageDetails;
  public PXAction<LangFilter> ViewExceptionalUsageObsoleteDetails;
  public PXSelect<LocalizationResourceByScreen> UsageDetails;

  public TranslationMaint.MultilingualTranslator Translator { get; private set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  public bool IsSiteMapAltered { get; set; }

  public bool IsCurrentCultureEditing
  {
    get
    {
      return this.LanguageFilter != null && this.LanguageFilter.Current != null && !string.IsNullOrEmpty(this.LanguageFilter.Current.Language) && this.LanguageFilter.Current.Language == CultureInfo.CurrentCulture.Name;
    }
  }

  public TranslationMaint()
  {
    this.Translator = new TranslationMaint.MultilingualTranslator(this);
    this._importer = new TranslationMaint.Importer(this);
    PXImportAttribute attribute = this.DeltaResourcesDistinct.GetAttribute<PXImportAttribute>();
    attribute.RowImporting += new EventHandler<PXImportAttribute.RowImportingEventArgs>(this._importer.RowImporting);
    attribute.MappingPropertiesInit += new EventHandler<PXImportAttribute.MappingPropertiesInitEventArgs>(this._importer.MappingPropertiesInit);
    PXStringListAttribute.SetList<LangFilter.showType>(this.LanguageFilter.Cache, (object) null, ResourceCollectingManager.ResourceForFilterTypes, ResourceCollectingManager.AllResourceForFilterNames);
    PXStringListAttribute.SetList<LangFilter.language>(this.LanguageFilter.Cache, (object) null, this.Translator.LocaleKeys, this.Translator.LocaleValues);
    PXUIFieldAttribute.SetReadOnly<LocalizationRecord.neutralValue>(this.DeltaResourcesDistinct.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<LocalizationExceptionRecord.neutralValue>(this.ExceptionalResources.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<LocalizationExceptionRecord.resKey>(this.ExceptionalResources.Cache, (object) null, false);
    PXSiteMapNodeSelectorAttribute.SetRestriction<LangFilter.screenID>(this.LanguageFilter.Cache, (object) null, (System.Func<SiteMap, bool>) (s => SiteMapRestrictionsTypes.IsWiki(s)));
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
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, this.Translator.GetNormalizedFilters(filters), ref startRow, maximumRows, ref totalRows);
  }

  [PXButton]
  [PXUIField(DisplayName = "Delete Obsolete Strings")]
  protected IEnumerable deleteObsoleteStrings(PXAdapter adapter)
  {
    if (this.LanguageFilter.View.Ask((object) null, "Warning", "This operation cannot be reverted. Are you sure?", MessageButtons.YesNo, MessageIcon.Question) == WebDialogResult.Yes)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PXLongOperation.StartOperation(this.UID, TranslationMaint.\u003C\u003EO.\u003C0\u003E__DeleteObsoleteCollectedStrings ?? (TranslationMaint.\u003C\u003EO.\u003C0\u003E__DeleteObsoleteCollectedStrings = new PXToggleAsyncDelegate(TranslationMaint.DeleteObsoleteCollectedStrings)));
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Usage Details")]
  protected IEnumerable viewUsageDetails(PXAdapter adapter)
  {
    TranslationMaint.usageDetailsSource = typeof (LocalizationRecord);
    if (this.UsageDetails.AskExt() == WebDialogResult.OK)
      this.RedirectToScreen();
    TranslationMaint.usageDetailsSource = (System.Type) null;
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Usage Details")]
  protected IEnumerable viewUsageObsoleteDetails(PXAdapter adapter)
  {
    TranslationMaint.usageDetailsSource = typeof (LocalizationRecordObsolete);
    if (this.UsageDetails.AskExt() == WebDialogResult.OK)
      this.RedirectToScreen();
    TranslationMaint.usageDetailsSource = (System.Type) null;
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Usage Details")]
  protected IEnumerable viewExceptionalUsageDetails(PXAdapter adapter)
  {
    TranslationMaint.usageDetailsSource = typeof (LocalizationExceptionRecord);
    if (this.UsageDetails.AskExt() == WebDialogResult.OK)
      this.RedirectToScreen();
    TranslationMaint.usageDetailsSource = (System.Type) null;
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Usage Details")]
  protected IEnumerable viewExceptionalUsageObsoleteDetails(PXAdapter adapter)
  {
    TranslationMaint.usageDetailsSource = typeof (LocalizationExceptionRecordObsolete);
    if (this.UsageDetails.AskExt() == WebDialogResult.OK)
      this.RedirectToScreen();
    TranslationMaint.usageDetailsSource = (System.Type) null;
    return adapter.Get();
  }

  private void RedirectToScreen()
  {
    if (this.UsageDetails.Current == null || string.IsNullOrEmpty(this.UsageDetails.Current.ScreenID))
      return;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.UsageDetails.Current.ScreenID);
    if (mapNodeByScreenId == null)
      return;
    string graphType = mapNodeByScreenId.GraphType;
    if (string.IsNullOrEmpty(graphType))
      return;
    System.Type type = PXBuildManager.GetType(graphType, false);
    if (type != (System.Type) null)
      throw new PXRedirectRequiredException(PXGraph.CreateInstance(type), true, string.Empty);
  }

  protected IEnumerable usageDetails()
  {
    IEnumerable enumerable = (IEnumerable) new LocalizationResourceByScreen[1]
    {
      new LocalizationResourceByScreen()
    };
    if (TranslationMaint.usageDetailsSource != (System.Type) null)
    {
      if (TranslationMaint.usageDetailsSource == typeof (LocalizationRecord))
        enumerable = this.Translator.GetUsageDetails(false);
      else if (TranslationMaint.usageDetailsSource == typeof (LocalizationRecordObsolete))
        enumerable = this.Translator.GetUsageDetails(true);
      else if (TranslationMaint.usageDetailsSource == typeof (LocalizationExceptionRecord))
        enumerable = this.Translator.GetExceptionalUsageDetails(false);
      else if (TranslationMaint.usageDetailsSource == typeof (LocalizationExceptionRecordObsolete))
        enumerable = this.Translator.GetExceptionalUsageDetails(true);
    }
    return enumerable;
  }

  protected IEnumerable deltaResourcesDistinct() => this.Translator.GetLocalizationRecords();

  protected IEnumerable deltaResourcesDistinctObsolete()
  {
    return this.Translator.GetLocalizationRecordsObsolete();
  }

  private string GetNeutralValue(bool isObsolete)
  {
    LocalizationRecord localizationRecord = isObsolete ? (LocalizationRecord) this.DeltaResourcesDistinctObsolete.Current : this.DeltaResourcesDistinct.Current;
    return localizationRecord != null && !string.IsNullOrEmpty(localizationRecord.NeutralValue) ? localizationRecord.NeutralValue : (string) null;
  }

  protected IEnumerable exceptionalResources()
  {
    return this.Translator.GetLocalizationExceptionalRecords(this.GetNeutralValue(false));
  }

  protected IEnumerable exceptionalResourcesObsolete()
  {
    return this.Translator.GetLocalizationExceptionalRecordsObsolete(this.GetNeutralValue(true));
  }

  [PXButton(Tooltip = "Look for new string constants in your system and add them to the dictionary for future translation.", DisplayOnMainToolbar = true, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Collect Strings", MapEnableRights = PXCacheRights.Insert)]
  public IEnumerable collectStrings(PXAdapter adapter)
  {
    if (this.LanguageFilter.View.Ask((object) null, "Warning", "This operation may take a significant amount of time. Do you want to continue?", MessageButtons.YesNo, MessageIcon.Question) == WebDialogResult.No)
      return adapter.Get();
    string appPath = HttpContext.Current.Request.PhysicalApplicationPath;
    ResourceCollectingManager.CollectSettings = new CollectStringsSettings?(CollectStringsSettings.AllStrings);
    Provider.PrepareSiteMap();
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => TranslationMaint.DoCollectStrings(appPath, (List<LocalizationTranslationSetItem>) null, (LocalizationResourceType[]) null, (TranslationSetMaint) null)));
    return adapter.Get();
  }

  public static void DeleteObsoleteCollectedStrings()
  {
    PXGraph graph = new PXGraph();
    PXCache cach1 = graph.Caches[typeof (PX.Translation.LocalizationValue)];
    PXCache cach2 = graph.Caches[typeof (PX.Translation.LocalizationResource)];
    PXCache cach3 = graph.Caches[typeof (LocalizationResourceByScreen)];
    graph.Views.Caches.Add(cach1.GetItemType());
    graph.Views.Caches.Add(cach2.GetItemType());
    graph.Views.Caches.Add(cach3.GetItemType());
    BqlCommand bqlCommand = (BqlCommand) new Select2<PX.Translation.LocalizationValue, LeftJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationResource.idValue, Equal<PX.Translation.LocalizationValue.id>>, LeftJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationValue.id>, And<LocalizationResourceByScreen.idRes, Equal<PX.Translation.LocalizationResource.id>>>>>>();
    BqlCommand select = !PXSiteMap.IsPortal ? bqlCommand.WhereNew<Where<PX.Translation.LocalizationValue.isSite, Equal<PX.Data.True>, And<PX.Translation.LocalizationValue.isObsolete, Equal<PX.Data.True>>>>() : bqlCommand.WhereNew<Where<PX.Translation.LocalizationValue.isPortal, Equal<PX.Data.True>, And<PX.Translation.LocalizationValue.isObsoletePortal, Equal<PX.Data.True>>>>();
    PXView pxView = new PXView(graph, false, select);
    int num1 = 0;
    int num2 = 0;
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    foreach (PXResult<PX.Translation.LocalizationValue, PX.Translation.LocalizationResource, LocalizationResourceByScreen> pxResult in pxView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 0, ref local2))
    {
      PX.Translation.LocalizationValue localizationValue = (PX.Translation.LocalizationValue) pxResult;
      PX.Translation.LocalizationResource localizationResource = (PX.Translation.LocalizationResource) pxResult;
      LocalizationResourceByScreen resourceByScreen = (LocalizationResourceByScreen) pxResult;
      bool? nullable;
      if (PXSiteMap.IsPortal)
      {
        nullable = localizationValue.IsSite;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          cach1.Delete((object) localizationValue);
          cach2.Delete((object) localizationResource);
          cach3.Delete((object) resourceByScreen);
        }
        else
        {
          localizationValue.IsPortal = new bool?(false);
          cach1.Update((object) localizationValue);
        }
      }
      else
      {
        nullable = localizationValue.IsPortal;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          cach1.Delete((object) localizationValue);
          cach2.Delete((object) localizationResource);
          cach3.Delete((object) resourceByScreen);
        }
        else
        {
          localizationValue.IsSite = new bool?(false);
          cach1.Update((object) localizationValue);
        }
      }
    }
    graph.Persist();
  }

  public static void DoCollectStrings(
    string appPath,
    List<LocalizationTranslationSetItem> translationSetItems,
    LocalizationResourceType[] resourceTypesToCollect,
    TranslationSetMaint setGraph)
  {
    TranslationMaint initializedGraph = TranslationMaint.GetInitializedGraph();
    CollectedResourcesCollection collectedRes = new CollectedResourcesCollection();
    PXSystemTranslator systemTranslator = new PXSystemTranslator(appPath, resourceTypesToCollect);
    Dictionary<string, PX.Translation.LocalizationValue> existingValue;
    Dictionary<string, PX.Translation.LocalizationResource> existingRes;
    HashSet<LocalizationResourceByScreen> existingResourceByScreen;
    TranslationMaint.LoadExistingInfo(initializedGraph, out existingValue, out existingRes, out existingResourceByScreen);
    if (ResourceCollectingManager.CollectSettings.HasValue)
    {
      CollectStringsSettings? collectSettings = ResourceCollectingManager.CollectSettings;
      CollectStringsSettings collectStringsSettings1 = CollectStringsSettings.AllStrings;
      if (!(collectSettings.GetValueOrDefault() == collectStringsSettings1 & collectSettings.HasValue))
      {
        collectSettings = ResourceCollectingManager.CollectSettings;
        CollectStringsSettings collectStringsSettings2 = CollectStringsSettings.TranslationSet;
        if (collectSettings.GetValueOrDefault() == collectStringsSettings2 & collectSettings.HasValue && translationSetItems != null)
        {
          ResourceByScreenCollection resourcesByScreens;
          IEnumerator enumerator = systemTranslator.GetNeutralForTranslationSet(translationSetItems, setGraph, out resourcesByScreens).GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              LocalizationResourceLite current = (LocalizationResourceLite) enumerator.Current;
              TranslationMaint.PlaceCollectedInfoToCaches(initializedGraph, collectedRes, current, existingValue, existingRes, existingResourceByScreen, resourcesByScreens);
            }
            goto label_17;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        else
          goto label_17;
      }
    }
    ResourceByScreenCollection resourcesByScreens1;
    foreach (LocalizationResourceLite resourceLite in systemTranslator.GetNeutral(out resourcesByScreens1))
      TranslationMaint.PlaceCollectedInfoToCaches(initializedGraph, collectedRes, resourceLite, existingValue, existingRes, existingResourceByScreen, resourcesByScreens1);
label_17:
    TranslationMaint.SaveCollectedInfo(initializedGraph);
    TranslationMaint.MarkObsoleteValues(initializedGraph, collectedRes, translationSetItems, resourceTypesToCollect);
  }

  private static TranslationMaint GetInitializedGraph()
  {
    TranslationMaint instance = PXGraph.CreateInstance<TranslationMaint>();
    instance.DeltaResourcesDistinct.Cache.Clear();
    instance.DeltaResourcesDistinctObsolete.Cache.Clear();
    instance.ExceptionalResources.Cache.Clear();
    instance.LocalizationTranslation.Cache.Clear();
    instance.Caches[typeof (PX.Translation.LocalizationValue)].Clear();
    instance.Caches[typeof (PX.Translation.LocalizationResource)].Clear();
    return instance;
  }

  private static void LoadExistingInfo(
    TranslationMaint graph,
    out Dictionary<string, PX.Translation.LocalizationValue> existingValue,
    out Dictionary<string, PX.Translation.LocalizationResource> existingRes,
    out HashSet<LocalizationResourceByScreen> existingResourceByScreen)
  {
    existingValue = new Dictionary<string, PX.Translation.LocalizationValue>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    existingRes = new Dictionary<string, PX.Translation.LocalizationResource>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (PXResult<PX.Translation.LocalizationValue, PX.Translation.LocalizationResource> pxResult in PXSelectBase<PX.Translation.LocalizationValue, PXSelectJoin<PX.Translation.LocalizationValue, LeftJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationValue.id, Equal<PX.Translation.LocalizationResource.idValue>>>>.Config>.Select((PXGraph) graph))
    {
      PX.Translation.LocalizationValue localizationValue = (PX.Translation.LocalizationValue) pxResult;
      PX.Translation.LocalizationResource localizationResource = (PX.Translation.LocalizationResource) pxResult;
      if (!existingValue.ContainsKey(localizationValue.NeutralValue))
        existingValue.Add(localizationValue.NeutralValue, localizationValue);
      string key = ResourceByScreenCollection.GetKey(localizationValue.NeutralValue, localizationResource.ResKey);
      if (localizationResource.Id != null && !existingRes.ContainsKey(key))
        existingRes.Add(key, localizationResource);
    }
    IEnumerable<LocalizationResourceByScreen> collection = (IEnumerable<LocalizationResourceByScreen>) graph.ResourceByScreen.Select().Select<PXResult<LocalizationResourceByScreen>, LocalizationResourceByScreen>((Expression<System.Func<PXResult<LocalizationResourceByScreen>, LocalizationResourceByScreen>>) (result => (LocalizationResourceByScreen) result));
    existingResourceByScreen = new HashSet<LocalizationResourceByScreen>(collection, (IEqualityComparer<LocalizationResourceByScreen>) new ResourceByScreenComparer());
    graph.ResourceByScreen.Cache.Clear();
    graph.Caches[typeof (PX.Translation.LocalizationValue)].Clear();
    graph.Caches[typeof (PX.Translation.LocalizationResource)].Clear();
  }

  private static void PlaceCollectedInfoToCaches(
    TranslationMaint graph,
    CollectedResourcesCollection collectedRes,
    LocalizationResourceLite resourceLite,
    Dictionary<string, PX.Translation.LocalizationValue> existingValue,
    Dictionary<string, PX.Translation.LocalizationResource> existingResource,
    HashSet<LocalizationResourceByScreen> existingResourceByScreen,
    ResourceByScreenCollection resourcesByScreens)
  {
    if (string.IsNullOrEmpty(resourceLite.NeutralValue))
      return;
    collectedRes.Add(resourceLite);
    PX.Translation.LocalizationValue localizationValue;
    string key;
    PX.Translation.LocalizationResource localizationResource;
    if (existingValue.TryGetValue(resourceLite.NeutralValue, out localizationValue))
    {
      key = ResourceByScreenCollection.GetKey(resourceLite.NeutralValue, resourceLite.ResKey);
      if (!existingResource.TryGetValue(key, out localizationResource))
      {
        localizationResource = TranslationMaint.CreateLocalizationResource(graph, resourceLite, existingValue[resourceLite.NeutralValue].Id);
        existingResource.Add(key, localizationResource);
      }
    }
    else
    {
      localizationValue = TranslationMaint.CreateLocalizationValue(graph, resourceLite);
      existingValue.Add(localizationValue.NeutralValue, localizationValue);
      localizationResource = TranslationMaint.CreateLocalizationResource(graph, resourceLite, localizationValue.Id);
      key = ResourceByScreenCollection.GetKey(localizationValue.NeutralValue, localizationResource.ResKey);
      existingResource.Add(key, localizationResource);
    }
    TranslationMaint.PlaceCollectedResourceByScreenToCache(graph, existingResourceByScreen, resourcesByScreens, key, localizationValue.Id, localizationResource.ResKey);
  }

  private static void PlaceCollectedResourceByScreenToCache(
    TranslationMaint graph,
    HashSet<LocalizationResourceByScreen> existingResourceByScreen,
    ResourceByScreenCollection resourcesByScreens,
    string key,
    string idValue,
    string resKey)
  {
    IEnumerable<LocalizationResourceByScreen> resourceByScreenUsage = resourcesByScreens.GetResourceByScreenUsage(key, PXCriptoHelper.CalculateMD5String(resKey), idValue);
    if (resourceByScreenUsage == null)
      return;
    foreach (LocalizationResourceByScreen resourceByScreen in resourceByScreenUsage.Where<LocalizationResourceByScreen>((System.Func<LocalizationResourceByScreen, bool>) (res => !existingResourceByScreen.Contains(res))))
      graph.ResourceByScreen.Insert(resourceByScreen);
  }

  private static void UpdateLocalizationUsage(
    PXGraph graph,
    PX.Translation.LocalizationValue lv,
    PX.Translation.LocalizationResource lr,
    LocalizationResourceLite lrLite,
    bool isValueExists,
    bool isResourceExists)
  {
    bool updated1 = false;
    bool updated2 = false;
    if (isValueExists)
    {
      if (PXSiteMap.IsPortal)
      {
        lv.ActivatePortalUsage(ref updated1);
        if (isResourceExists)
        {
          lr.ActivatePortalUsage(ref updated2);
          int? resType = lr.ResType;
          int type = lrLite.Type;
          if (!(resType.GetValueOrDefault() == type & resType.HasValue))
          {
            lr.ResType = new int?(lrLite.Type);
            updated2 = true;
          }
        }
        else
          lr.DeactivatePortalUsage(ref updated2);
      }
      else
      {
        lv.ActivateSiteUsage(ref updated1);
        if (isResourceExists)
          lr.ActivateSiteUsage(ref updated2);
        else
          lr.DeactivateSiteUsage(ref updated2);
      }
    }
    else if (PXSiteMap.IsPortal)
      lr.DeactivatePortalUsage(ref updated2);
    else
      lr.DeactivateSiteUsage(ref updated2);
    if (updated1)
      graph.Caches[typeof (PX.Translation.LocalizationValue)].Update((object) lv);
    if (!updated2)
      return;
    graph.Caches[typeof (PX.Translation.LocalizationResource)].Update((object) lr);
  }

  private static PX.Translation.LocalizationValue CreateLocalizationValue(
    TranslationMaint graph,
    LocalizationResourceLite resourceLite)
  {
    PX.Translation.LocalizationValue localizationValue = new PX.Translation.LocalizationValue()
    {
      Id = PXCriptoHelper.CalculateMD5LocalizationString(resourceLite.NeutralValue),
      NeutralValue = resourceLite.NeutralValue,
      IsSite = new bool?(!PXSiteMap.IsPortal),
      IsPortal = new bool?(PXSiteMap.IsPortal)
    };
    return (PX.Translation.LocalizationValue) graph.Caches[typeof (PX.Translation.LocalizationValue)].Insert((object) localizationValue);
  }

  private static PX.Translation.LocalizationResource CreateLocalizationResource(
    TranslationMaint graph,
    LocalizationResourceLite resourceLite,
    string idValue)
  {
    PX.Translation.LocalizationResource localizationResource = new PX.Translation.LocalizationResource()
    {
      Id = PXCriptoHelper.CalculateMD5String(resourceLite.ResKey),
      IdValue = idValue,
      ResKey = resourceLite.ResKey,
      ResType = new int?(resourceLite.Type),
      IsSite = new bool?(!PXSiteMap.IsPortal),
      IsPortal = new bool?(PXSiteMap.IsPortal)
    };
    return (PX.Translation.LocalizationResource) graph.Caches[typeof (PX.Translation.LocalizationResource)].Insert((object) localizationResource);
  }

  private static void SaveCollectedInfo(TranslationMaint graph)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      using (new SuppressPushNotificationsScope())
      {
        using (new SuppressPerformanceInfoScope())
        {
          TranslationMaint.CorrectResourses();
          graph.Caches[typeof (PX.Translation.LocalizationResource)].Persist(PXDBOperation.Insert);
          graph.Caches[typeof (PX.Translation.LocalizationResource)].Persist(PXDBOperation.Update);
          graph.Caches[typeof (PX.Translation.LocalizationValue)].Persist(PXDBOperation.Insert);
          graph.Caches[typeof (PX.Translation.LocalizationValue)].Persist(PXDBOperation.Update);
          graph.ResourceByScreen.Cache.Persist(PXDBOperation.Insert);
          graph.ResourceByScreen.Cache.Persist(PXDBOperation.Update);
          graph.ResourceByScreen.Cache.Clear();
          graph.Caches[typeof (PX.Translation.LocalizationValue)].Clear();
          graph.Caches[typeof (PX.Translation.LocalizationResource)].Clear();
          TranslationMaint.CorrectTranslationCompanyMask();
          transactionScope.Complete();
        }
      }
    }
  }

  private static void CorrectResourses()
  {
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
    int companyId = PXDatabase.Provider.getCompanyID(typeof (PX.Translation.LocalizationResource).Name, out companySetting _);
    List<CommandBase> commandBaseList1 = new List<CommandBase>();
    YaqlVectorQuery yaqlVectorQuery1 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("LocalizationValue", (string) null), (List<YaqlJoin>) null);
    yaqlVectorQuery1.Column = YaqlScalarAlilased.op_Implicit(Yaql.column<PX.Translation.LocalizationValue.id>((string) null));
    ((YaqlQueryBase) yaqlVectorQuery1).Condition = (YaqlCondition) Yaql.companyIdEq(companyId, (List<CompanyHeader>) null, false, (string) null, false);
    YaqlVectorQuery yaqlVectorQuery2 = yaqlVectorQuery1;
    CmdDelete cmdDelete = new CmdDelete(YaqlSchemaTable.op_Implicit("LocalizationResource"), (List<YaqlJoin>) null)
    {
      Condition = Yaql.and(Yaql.not(Yaql.isIn((YaqlScalar) Yaql.column<PX.Translation.LocalizationResource.idValue>((string) null), yaqlVectorQuery2)), (YaqlCondition) Yaql.companyIdEq(companyId, (List<CompanyHeader>) null, false, (string) null, false))
    };
    commandBaseList1.Add((CommandBase) cmdDelete);
    List<CommandBase> commandBaseList2 = commandBaseList1;
    ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null);
    dbServicesPoint.executeCommands((IEnumerable<CommandBase>) commandBaseList2, executionContext, false);
  }

  public override bool ProviderInsert(System.Type table, params PXDataFieldAssign[] pars)
  {
    pars = ((IEnumerable<PXDataFieldAssign>) pars).Where<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (_ => _ != PXDataFieldAssign.OperationSwitchAllowed)).ToArray<PXDataFieldAssign>();
    return base.ProviderInsert(table, pars);
  }

  private static void CorrectTranslationCompanyMask()
  {
    PointDbmsBase point = PXDatabase.Provider.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
    List<TableHeader> list = new List<System.Type>()
    {
      typeof (PX.Translation.LocalizationResource),
      typeof (PX.Translation.LocalizationValue),
      typeof (LocalizationResourceByScreen)
    }.Select<System.Type, TableHeader>((System.Func<System.Type, TableHeader>) (t => point.Schema.GetTable(t.Name))).ToList<TableHeader>();
    int companyId = PXDatabase.Provider.getCompanyID(typeof (PX.Translation.LocalizationResource).Name, out companySetting _);
    PXDatabase.Provider.GetMaintenance(point).CorrectCompanyMask(list, new int?(companyId));
  }

  internal void LangFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (this.LocalizationRecordCache.Current == null || this.LocalizationRecordCache.Current.SelectQueries == null)
      return;
    this.LocalizationRecordCache.Current.SelectQueries.Clear();
  }

  internal void LangFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is LangFilter row))
      return;
    bool? showUnboundOnly = row.ShowUnboundOnly;
    bool flag1 = true;
    bool isEnabled1 = !(showUnboundOnly.GetValueOrDefault() == flag1 & showUnboundOnly.HasValue);
    showUnboundOnly = row.ShowUnboundOnly;
    bool flag2 = true;
    bool isEnabled2 = showUnboundOnly.GetValueOrDefault() == flag2 & showUnboundOnly.HasValue;
    PXUIFieldAttribute.SetEnabled<LangFilter.screenID>(sender, (object) row, isEnabled1);
    PXUIFieldAttribute.SetEnabled<LangFilter.showType>(sender, (object) row, isEnabled2);
  }

  internal void LangFilter_Language_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (HttpContext.Current == null)
      return;
    string str = HttpContext.Current.Request.QueryString["Language"];
    if (string.IsNullOrEmpty(str))
      return;
    e.NewValue = (object) str;
  }

  internal void LangFilter_Language_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.Translator.InitializeLocalizationRecords();
    this.Translator.InitializeLocalizationExceptionRecords();
    this.Translator.InitializeLocalizationRecordsObsolete();
    this.Translator.InitializeLocalizationExceptionRecordsObsolete();
  }

  internal void LangFilter_Key_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) 0;
  }

  internal void LangFilter_ScreenID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) PXSiteMap.RootNode.ScreenID;
  }

  internal void LocalizationRecord_IsNotLocalized_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.LocalizationRecordIsNotLocalizedUpdated(e);
  }

  internal void LocalizationRecordObsolete_IsNotLocalized_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.LocalizationRecordIsNotLocalizedUpdated(e);
  }

  internal void LocalizationExceptionRecord_IsNotLocalized_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.LocalizationExceptionRecordIsNotLocalizedUpdated(e);
  }

  internal void LocalizationExceptionRecordObsolete_IsNotLocalized_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.LocalizationExceptionRecordIsNotLocalizedUpdated(e);
  }

  private void LocalizationRecordIsNotLocalizedUpdated(PXFieldUpdatedEventArgs e)
  {
    LocalizationRecord row = e.Row as LocalizationRecord;
    bool? oldValue = e.OldValue as bool?;
    if (row == null || !oldValue.HasValue)
      return;
    this.Translator.UpdateValueCache(row.Id, row.IsNotLocalized, new int?());
  }

  private void LocalizationExceptionRecordIsNotLocalizedUpdated(PXFieldUpdatedEventArgs e)
  {
    LocalizationExceptionRecord row = e.Row as LocalizationExceptionRecord;
    bool? oldValue = e.OldValue as bool?;
    if (row == null || !oldValue.HasValue)
      return;
    this.Translator.UpdateResourceCache(row.Id, row.IdRes, row.IsNotLocalized);
  }

  internal void LocalizationRecord_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    this.LocalizationRowPersisting(e);
  }

  internal void LocalizationRecordObsolete_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    this.LocalizationRowPersisting(e);
  }

  internal void LocalizationResourceByScreen_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is LocalizationResourceByScreen row) || string.IsNullOrEmpty(row.ScreenID))
      return;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(row.ScreenID);
    if (mapNodeByScreenId == null)
      return;
    row.Title = mapNodeByScreenId.Title;
  }

  private void LocalizationRowPersisting(PXRowPersistingEventArgs e) => e.Cancel = true;

  internal void LocalizationExceptionRecord_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    this.LocalizationRowPersisting(e);
  }

  internal void LocalizationExceptionRecordObsolete_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    this.LocalizationRowPersisting(e);
  }

  public bool IsSiteMapResource(LocalizationExceptionRecord resource)
  {
    bool flag = false;
    if (resource != null && !string.IsNullOrEmpty(resource.ResKey))
      flag = resource.ResKey.StartsWith("SiteMap ->");
    return flag;
  }

  public override void Persist()
  {
    base.Persist();
    PXLocalizer.Reload();
    PXMessages.ClearMessagePrefixes();
    PXSiteMap.Provider.Clear();
    if (this.LocalizationRecordCache.Current != null)
      this.LocalizationRecordCache.Current.SelectQueries = (PXViewQueryCollection) null;
    this.DeltaResourcesDistinct.Cache.Clear();
    this.DeltaResourcesDistinctObsolete.Cache.Clear();
    this.ExceptionalResources.Cache.Clear();
    this.LocalizationTranslation.Cache.Clear();
    this.ScreenInfoCacheControl.InvalidateCache();
    this.PageCacheControl.InvalidateCache();
  }

  public override void Load()
  {
    base.Load();
    string key1 = this.GetType().FullName + "$cachedParents";
    this.cachedParents = PXContext.SessionTyped<PXSessionStatePXData>().LocalizationGridParent[key1];
    string key2 = this.GetType().FullName + "$cachedParentsObsolete";
    this.cachedParentsObsolete = PXContext.SessionTyped<PXSessionStatePXData>().LocalizationGridParentObsolete[key2];
  }

  public override void Unload()
  {
    base.Unload();
    string key1 = this.GetType().FullName + "$cachedParents";
    PXContext.SessionTyped<PXSessionStatePXData>().LocalizationGridParent[key1] = this.cachedParents;
    string key2 = this.GetType().FullName + "$cachedParentsObsolete";
    PXContext.SessionTyped<PXSessionStatePXData>().LocalizationGridParentObsolete[key2] = this.cachedParentsObsolete;
  }

  private static void CreateBqlForObsoleteDefinition(
    PXGraph graph,
    List<LocalizationTranslationSetItem> translationSetItems,
    LocalizationResourceType[] resourceTypesToCollect,
    out PXView boundView,
    out object[] boundParams,
    out PXView unboundView,
    out object[] unboundParams)
  {
    bool flag = translationSetItems != null && translationSetItems.Count > 0 || resourceTypesToCollect != null && resourceTypesToCollect.Length != 0;
    if (flag && (translationSetItems == null || translationSetItems.Count == 0))
    {
      boundView = (PXView) null;
      boundParams = (object[]) null;
    }
    else
    {
      BqlCommand select;
      if (flag)
      {
        select = (BqlCommand) new Select2<PX.Translation.LocalizationValue, InnerJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationResource.idValue, Equal<PX.Translation.LocalizationValue.id>>, InnerJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationValue.id>, And<LocalizationResourceByScreen.idRes, Equal<PX.Translation.LocalizationResource.id>, And<LocalizationResourceByScreen.screenID, In<Required<LocalizationResourceByScreen.screenID>>>>>, LeftJoin<LocalizationResourceByScreenObsolete, On<LocalizationResourceByScreenObsolete.idValue, Equal<LocalizationResourceByScreen.idValue>, And<LocalizationResourceByScreenObsolete.screenID, NotIn<Required<LocalizationResourceByScreenObsolete.screenID>>>>>>>, Where<LocalizationResourceByScreenObsolete.idValue, IsNull>>();
        object[] array = (object[]) translationSetItems.Select<LocalizationTranslationSetItem, string>((System.Func<LocalizationTranslationSetItem, string>) (item => item.ScreenID)).ToArray<string>();
        boundParams = new object[2]
        {
          (object) array,
          (object) array
        };
      }
      else
      {
        select = (BqlCommand) new Select2<PX.Translation.LocalizationValue, InnerJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationResource.idValue, Equal<PX.Translation.LocalizationValue.id>>, LeftJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idRes, Equal<PX.Translation.LocalizationResource.id>, And<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationValue.id>>>>>>();
        boundParams = (object[]) null;
      }
      boundView = new PXView(graph, true, select);
    }
    object[] array1 = resourceTypesToCollect != null ? ((IEnumerable<LocalizationResourceType>) ResourceCollectingManager.ExceptBoundTypes((IEnumerable<LocalizationResourceType>) resourceTypesToCollect)).Select<LocalizationResourceType, int>((System.Func<LocalizationResourceType, int>) (type => (int) type)).ToArray<object>() : (object[]) null;
    if (flag && array1 != null && array1.Length != 0)
    {
      unboundParams = new object[1]{ (object) array1 };
      BqlCommand select = (BqlCommand) new Select2<PX.Translation.LocalizationValue, InnerJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationResource.idValue, Equal<PX.Translation.LocalizationValue.id>>, LeftJoin<LocalizationResourceForObsolete, On<LocalizationResourceForObsolete.idValue, Equal<PX.Translation.LocalizationResource.idValue>, And<LocalizationResourceForObsolete.id, NotEqual<PX.Translation.LocalizationResource.id>>>, LeftJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationValue.id>, And<LocalizationResourceByScreen.idRes, Equal<PX.Translation.LocalizationResource.id>>>>>>, Where<LocalizationResourceByScreen.screenID, IsNull, And<PX.Translation.LocalizationResource.resType, In<Required<PX.Translation.LocalizationResource.resType>>, And<LocalizationResourceForObsolete.id, IsNull>>>>();
      unboundView = new PXView(graph, true, select);
    }
    else
    {
      unboundParams = (object[]) null;
      unboundView = (PXView) null;
    }
  }

  private static void MarkObsoleteValues(
    TranslationMaint graph,
    CollectedResourcesCollection collectedRes,
    List<LocalizationTranslationSetItem> translationSetItems,
    LocalizationResourceType[] resourceTypesToCollect)
  {
    PXView boundView;
    object[] boundParams;
    PXView unboundView;
    object[] unboundParams;
    TranslationMaint.CreateBqlForObsoleteDefinition((PXGraph) graph, translationSetItems, resourceTypesToCollect, out boundView, out boundParams, out unboundView, out unboundParams);
    if (boundView != null)
      TranslationMaint.UpdateObsoleteValues((PXGraph) graph, collectedRes, boundView, boundParams);
    if (unboundView != null)
      TranslationMaint.UpdateObsoleteValues((PXGraph) graph, collectedRes, unboundView, unboundParams);
    TranslationMaint.UpdateNeutralValues((PXGraph) graph, collectedRes);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      graph.Caches[typeof (PX.Translation.LocalizationValue)].Persist(PXDBOperation.Update);
      graph.Caches[typeof (PX.Translation.LocalizationValue)].Clear();
      graph.Caches[typeof (PX.Translation.LocalizationResource)].Persist(PXDBOperation.Update);
      graph.Caches[typeof (PX.Translation.LocalizationResource)].Persist(PXDBOperation.Delete);
      graph.Caches[typeof (PX.Translation.LocalizationResource)].Clear();
      graph.Caches[typeof (LocalizationResourceByScreen)].Persist(PXDBOperation.Delete);
      graph.Caches[typeof (LocalizationResourceByScreen)].Clear();
      transactionScope.Complete();
    }
  }

  private static void UpdateObsoleteValues(
    PXGraph graph,
    CollectedResourcesCollection collectedRes,
    PXView valuesView,
    object[] valuesParams)
  {
    int startRow = 0;
    int totalRows = 0;
    IList list;
    using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 10))
      list = (IList) valuesView.Select((object[]) null, valuesParams, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref startRow, 0, ref totalRows);
    foreach (PXResult pxResult in (IEnumerable) list)
    {
      PX.Translation.LocalizationValue lv = pxResult[typeof (PX.Translation.LocalizationValue)] as PX.Translation.LocalizationValue;
      PX.Translation.LocalizationResource lr = pxResult[typeof (PX.Translation.LocalizationResource)] as PX.Translation.LocalizationResource;
      LocalizationResourceByScreen resourceByScreen = pxResult[typeof (LocalizationResourceByScreen)] as LocalizationResourceByScreen;
      bool flag1 = false;
      bool isValueExists = false;
      bool isResourceExists = false;
      LocalizationResourceLite lrLite = (LocalizationResourceLite) null;
      List<LocalizationResourceLite> resources;
      bool? nullable;
      if (collectedRes.TryGetResources(lv, out resources))
      {
        isValueExists = true;
        lrLite = resources.FirstOrDefault<LocalizationResourceLite>((System.Func<LocalizationResourceLite, bool>) (r => lr.ResKey.Equals(r.ResKey, StringComparison.OrdinalIgnoreCase)));
        isResourceExists = lrLite != null;
        if (PXSiteMap.IsPortal)
        {
          nullable = lv.IsObsoletePortal;
          bool flag2 = true;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          {
            lv.IsObsoletePortal = new bool?(false);
            flag1 = true;
          }
        }
        else
        {
          nullable = lv.IsObsolete;
          bool flag3 = true;
          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          {
            lv.IsObsolete = new bool?(false);
            flag1 = true;
          }
        }
      }
      else if (PXSiteMap.IsPortal)
      {
        nullable = lv.IsObsoletePortal;
        bool flag4 = false;
        if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
        {
          lv.IsObsoletePortal = new bool?(true);
          flag1 = true;
        }
      }
      else
      {
        nullable = lv.IsObsolete;
        bool flag5 = false;
        if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
        {
          lv.IsObsolete = new bool?(true);
          flag1 = true;
        }
      }
      TranslationMaint.UpdateLocalizationUsage(graph, lv, lr, lrLite, isValueExists, isResourceExists);
      if (flag1)
        graph.Caches[typeof (PX.Translation.LocalizationValue)].Update((object) lv);
      if (isValueExists && !isResourceExists)
      {
        nullable = lr.IsSite;
        bool flag6 = false;
        if (nullable.GetValueOrDefault() == flag6 & nullable.HasValue)
        {
          nullable = lr.IsPortal;
          bool flag7 = false;
          if (nullable.GetValueOrDefault() == flag7 & nullable.HasValue)
          {
            graph.Caches[typeof (PX.Translation.LocalizationResource)].Delete((object) lr);
            if (resourceByScreen != null)
              graph.Caches[typeof (LocalizationResourceByScreen)].Delete((object) resourceByScreen);
          }
        }
      }
    }
  }

  private static void UpdateNeutralValues(PXGraph graph, CollectedResourcesCollection collectedRes)
  {
    IList list;
    using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 10))
      list = (IList) PXSelectBase<PX.Translation.LocalizationValue, PXSelect<PX.Translation.LocalizationValue, Where<PX.Translation.LocalizationValue.isObsolete, Equal<False>>>.Config>.Select(graph);
    foreach (PXResult<PX.Translation.LocalizationValue> pxResult in (IEnumerable) list)
    {
      PX.Translation.LocalizationValue localizationValue = (PX.Translation.LocalizationValue) pxResult;
      if (collectedRes.Contains(localizationValue))
      {
        string neutralValue = collectedRes.GetNeutralValue(localizationValue);
        if (!neutralValue.Equals(localizationValue.NeutralValue, StringComparison.Ordinal))
        {
          localizationValue.NeutralValue = neutralValue;
          graph.Caches[typeof (PX.Translation.LocalizationValue)].Update((object) localizationValue);
        }
      }
    }
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (this.cachedParents.ContainsKey(values[(object) "NeutralValue"] as string))
    {
      keys[(object) "ID"] = (object) this.cachedParents[values[(object) "NeutralValue"] as string].Id;
      keys[(object) "Locale"] = (object) this.LanguageFilter.Current.Language;
    }
    return !values.Contains((object) "Value") || !(values[(object) "Value"] is string str) || !(str == string.Empty);
  }

  public bool RowImporting(string viewName, object row)
  {
    return row == null || !(((LocalizationRecord) row).Values.Values.FirstOrDefault<object>() is string str) || str.Trim().Length == 0;
  }

  public bool RowImported(string viewName, object row, object oldRow)
  {
    return oldRow == null || !(((LocalizationRecord) oldRow).Values.Values.FirstOrDefault<object>() is string str) || str.Trim().Length == 0;
  }

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public class Importer
  {
    private readonly TranslationMaint _translationGraph;

    public Importer(TranslationMaint graph) => this._translationGraph = graph;

    public void MappingPropertiesInit(
      object sender,
      PXImportAttribute.MappingPropertiesInitEventArgs e)
    {
      e.Names.Add(typeof (LocalizationRecord.neutralValue).Name);
      e.DisplayNames.Add(PXUIFieldAttribute.GetDisplayName<LocalizationRecord.neutralValue>(this._translationGraph.DeltaResourcesDistinct.Cache));
      foreach (KeyValuePair<string, string> locale in this._translationGraph.Translator.Locales)
      {
        e.Names.Add(locale.Key);
        e.DisplayNames.Add(locale.Value);
      }
    }

    public void RowImporting(object sender, PXImportAttribute.RowImportingEventArgs e)
    {
      e.Cancel = true;
      LocalizationRecord record = this.ParseRecord(e);
      PX.Translation.LocalizationValue localizationValue1 = this.GetLocalizationValue(record);
      PX.Translation.LocalizationValue localizationValue2 = this._translationGraph.Translator.SelectLocalizationValue(record.Id) ?? localizationValue1;
      bool rowExists = localizationValue2 != localizationValue1;
      if (rowExists)
        localizationValue2.IsNotLocalized = localizationValue1.IsNotLocalized;
      switch (e.Mode)
      {
        case PXImportAttribute.ImportMode.Value.UpdateExisting:
          this.UpdateExisting(record, localizationValue2, rowExists);
          break;
        case PXImportAttribute.ImportMode.Value.BypassExisting:
          this.BypassExisting(record, localizationValue2, rowExists);
          break;
        case PXImportAttribute.ImportMode.Value.InsertAllRecords:
          this.InsertAllRecords(record, localizationValue2, rowExists);
          break;
      }
    }

    private void UpdateExisting(LocalizationRecord record, PX.Translation.LocalizationValue value, bool rowExists)
    {
      if (rowExists)
        this._translationGraph.Translator.UpdateValueCache(value.Id, value.IsNotLocalized, new int?());
      else
        this._translationGraph.LocalizationValue.Insert(value);
      foreach (PX.Translation.LocalizationTranslation translation in this.GetTranslations(record))
        this._translationGraph.Translator.UpdateTranslationCache(translation.IdValue, translation.IdRes, translation.Value, translation.Locale);
    }

    private void BypassExisting(LocalizationRecord record, PX.Translation.LocalizationValue value, bool rowExists)
    {
      if (rowExists)
        return;
      this._translationGraph.LocalizationValue.Insert(value);
      foreach (PX.Translation.LocalizationTranslation translation in this.GetTranslations(record))
        this._translationGraph.LocalizationTranslation.Insert(translation);
    }

    private void InsertAllRecords(
      LocalizationRecord record,
      PX.Translation.LocalizationValue value,
      bool rowExists)
    {
      if (rowExists)
        throw new PXException("The row cannot be inserted. It already exists.");
      this._translationGraph.LocalizationValue.Insert(value);
      foreach (PX.Translation.LocalizationTranslation translation in this.GetTranslations(record))
      {
        if (this._translationGraph.Translator.SelectLocalizationTranslation(translation.IdValue, translation.IdRes, translation.Locale) != null)
          throw new PXException("The row cannot be inserted. It already exists.");
        this._translationGraph.LocalizationTranslation.Insert(translation);
      }
    }

    private LocalizationRecord ParseRecord(PXImportAttribute.RowImportingEventArgs e)
    {
      string str = Utils.DecodeXString(this.GetEntry(e.Values, typeof (LocalizationRecord.neutralValue).Name).Value as string);
      if (string.IsNullOrEmpty(str))
        throw new PXException("Neutral Value can't be null or empty.");
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      foreach (DictionaryEntry dictionaryEntry in e.Values)
      {
        string key = dictionaryEntry.Key as string;
        if (!string.IsNullOrEmpty(key) && ((IEnumerable<string>) this._translationGraph.Translator.LocaleKeys).Contains<string>(dictionaryEntry.Key as string))
          dictionary.Add(key, dictionaryEntry.Value);
      }
      DictionaryEntry entry = this.GetEntry(e.Values, typeof (LocalizationRecord.isNotLocalized).Name);
      bool result;
      return new LocalizationRecord()
      {
        Id = PXCriptoHelper.CalculateMD5LocalizationString(str),
        NeutralValue = str,
        Values = dictionary,
        IsNotLocalized = new bool?(bool.TryParse(entry.Value as string, out result) && result)
      };
    }

    private PX.Translation.LocalizationValue GetLocalizationValue(LocalizationRecord record)
    {
      return new PX.Translation.LocalizationValue()
      {
        Id = record.Id,
        NeutralValue = record.NeutralValue,
        IsNotLocalized = record.IsNotLocalized,
        IsSite = new bool?(!PXSiteMap.IsPortal),
        IsPortal = new bool?(PXSiteMap.IsPortal)
      };
    }

    private IEnumerable<PX.Translation.LocalizationTranslation> GetTranslations(
      LocalizationRecord record)
    {
      return record.Values.Where<KeyValuePair<string, object>>((System.Func<KeyValuePair<string, object>, bool>) (p => !string.IsNullOrEmpty(p.Value as string))).Select<KeyValuePair<string, object>, PX.Translation.LocalizationTranslation>((System.Func<KeyValuePair<string, object>, PX.Translation.LocalizationTranslation>) (p => new PX.Translation.LocalizationTranslation()
      {
        IdValue = record.Id,
        IdRes = "D41D8CD98F00B204E9800998ECF8427E",
        Locale = p.Key,
        Value = p.Value as string
      }));
    }

    private DictionaryEntry GetEntry(IDictionary dictionary, string key)
    {
      foreach (DictionaryEntry entry in dictionary)
      {
        if (string.Compare(entry.Key as string, key, StringComparison.OrdinalIgnoreCase) == 0)
          return entry;
      }
      return new DictionaryEntry();
    }
  }

  public class MultilingualTranslator
  {
    private static Dictionary<string, HashSet<string>> childNodesByScreenId;
    private readonly TranslationMaint translationGraph;
    public const string TRANSLATION_DEFAULT_VALUE_MD5 = "D41D8CD98F00B204E9800998ECF8427E";
    public const string TRANSLATION_DEFAULT_VALUE = "";
    public const char LANGUAGE_SEPARATOR = ',';
    public static readonly string[] FORBIDDEN_COLUMNS = new string[7]
    {
      typeof (LocalizationRecord.id).Name,
      typeof (LocalizationRecord.neutralValue).Name,
      typeof (LocalizationRecord.isNotLocalized).Name,
      typeof (LocalizationRecord.localizedValue).Name,
      "Values",
      "CreatedValues",
      "LastModifiedValues"
    };

    public event System.Action OnLocalizationRecordsInitialised;

    public event System.Action OnLocalizationRecordsObsoleteInitialised;

    public event System.Action OnLocalizationExceptionalRecordsInitialised;

    public event System.Action OnLocalizationExceptionalRecordsObsoleteInitialised;

    public Dictionary<string, string> Locales { get; private set; }

    public string[] LocaleKeys => this.Locales.Keys.ToArray<string>();

    public string[] LocaleValues => this.Locales.Values.ToArray<string>();

    public MultilingualTranslator(TranslationMaint graph)
    {
      this.translationGraph = graph;
      this.Locales = PXSelectBase<Locale, PXSelect<Locale>.Config>.Select((PXGraph) graph).Select<PXResult<Locale>, Locale>((Expression<System.Func<PXResult<Locale>, Locale>>) (locale => (Locale) locale)).ToDictionary<Locale, string, string>((System.Func<Locale, string>) (locale => locale.LocaleName), (System.Func<Locale, string>) (locale => locale.TranslatedName));
    }

    public void InitializeLocalizationRecords()
    {
      this.InitializeLocalizationRecordCache<LocalizationRecord>(this.translationGraph.DeltaResourcesDistinct.Cache, false);
      if (this.OnLocalizationRecordsInitialised == null)
        return;
      this.OnLocalizationRecordsInitialised();
    }

    public void InitializeLocalizationExceptionRecords()
    {
      this.InitializeLocalizationExceptionRecordCache<LocalizationExceptionRecord>(this.translationGraph.ExceptionalResources.Cache, this.translationGraph.DeltaResourcesDistinct.Cache);
      if (this.OnLocalizationExceptionalRecordsInitialised == null)
        return;
      this.OnLocalizationExceptionalRecordsInitialised();
    }

    public void InitializeLocalizationExceptionRecordsObsolete()
    {
      this.InitializeLocalizationExceptionRecordCache<LocalizationExceptionRecordObsolete>(this.translationGraph.ExceptionalResourcesObsolete.Cache, this.translationGraph.DeltaResourcesDistinctObsolete.Cache);
      if (this.OnLocalizationExceptionalRecordsObsoleteInitialised == null)
        return;
      this.OnLocalizationExceptionalRecordsObsoleteInitialised();
    }

    public void InitializeLocalizationRecordsObsolete()
    {
      this.InitializeLocalizationRecordCache<LocalizationRecordObsolete>(this.translationGraph.DeltaResourcesDistinctObsolete.Cache, true);
      if (this.OnLocalizationRecordsObsoleteInitialised == null)
        return;
      this.OnLocalizationRecordsObsoleteInitialised();
    }

    public IEnumerable GetLocalizationRecords()
    {
      return this.SelectLocalizationRecord<LocalizationRecord>(ref this.translationGraph.cachedParents, this.translationGraph.DeltaResourcesDistinct.Cache, false, new System.Action(this.InitializeLocalizationRecords));
    }

    public IEnumerable GetLocalizationRecordsObsolete()
    {
      return this.SelectLocalizationRecord<LocalizationRecordObsolete>(ref this.translationGraph.cachedParentsObsolete, this.translationGraph.DeltaResourcesDistinctObsolete.Cache, true, new System.Action(this.InitializeLocalizationRecordsObsolete));
    }

    public IEnumerable GetLocalizationExceptionalRecords(string neutralValue)
    {
      return this.SelectLocalizationExceptionalRecord<LocalizationExceptionRecord>(this.translationGraph.ExceptionalResources.Cache, new System.Action(this.InitializeLocalizationExceptionRecords), neutralValue);
    }

    public IEnumerable GetLocalizationExceptionalRecordsObsolete(string neutralValue)
    {
      return this.SelectLocalizationExceptionalRecord<LocalizationExceptionRecordObsolete>(this.translationGraph.ExceptionalResourcesObsolete.Cache, new System.Action(this.InitializeLocalizationExceptionRecordsObsolete), neutralValue);
    }

    public IEnumerable GetUsageDetails(bool isObsolete)
    {
      BqlCommand bqlCommand = (BqlCommand) new Select4<LocalizationResourceByScreen, Aggregate<GroupBy<LocalizationResourceByScreen.screenID>>>();
      PXView pxView = new PXView((PXGraph) this.translationGraph, true, !isObsolete ? bqlCommand.WhereAnd<Where<LocalizationResourceByScreen.idValue, Equal<Current<LocalizationRecord.id>>>>() : bqlCommand.WhereAnd<Where<LocalizationResourceByScreen.idValue, Equal<Current<LocalizationRecordObsolete.id>>>>());
      int startRow = PXView.StartRow;
      int num = 0;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> usageDetails = pxView.Select((object[]) null, (object[]) null, searches, sortColumns, descendings, filters, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      return (IEnumerable) usageDetails;
    }

    public IEnumerable GetExceptionalUsageDetails(bool isObsolete)
    {
      BqlCommand bqlCommand = (BqlCommand) new Select4<LocalizationResourceByScreen, Aggregate<GroupBy<LocalizationResourceByScreen.screenID>>>();
      PXView pxView = new PXView((PXGraph) this.translationGraph, true, !isObsolete ? bqlCommand.WhereAnd<Where<LocalizationResourceByScreen.idValue, Equal<Current<LocalizationExceptionRecord.id>>, And<LocalizationResourceByScreen.idRes, Equal<Current<LocalizationExceptionRecord.idRes>>>>>() : bqlCommand.WhereAnd<Where<LocalizationResourceByScreen.idValue, Equal<Current<LocalizationExceptionRecordObsolete.id>>, And<LocalizationResourceByScreen.idRes, Equal<Current<LocalizationExceptionRecordObsolete.idRes>>>>>());
      int startRow = PXView.StartRow;
      int num = 0;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> exceptionalUsageDetails = pxView.Select((object[]) null, (object[]) null, searches, sortColumns, descendings, filters, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      return (IEnumerable) exceptionalUsageDetails;
    }

    private IEnumerable SelectLocalizationExceptionalRecord<T>(
      PXCache cache,
      System.Action initializationMethod,
      string neutralValue)
      where T : LocalizationExceptionRecord, new()
    {
      Dictionary<string, T> dictionary = new Dictionary<string, T>();
      if (!string.IsNullOrEmpty(this.translationGraph.LanguageFilter.Current.Language))
      {
        initializationMethod();
        bool isDirty = cache.IsDirty;
        object[] parameters1;
        PXView exceptionalRecordView = this.GetLocalizationExceptionalRecordView(neutralValue, out parameters1);
        int startRow = PXView.StartRow;
        int num = 0;
        object[] parameters2 = parameters1;
        string[] sortColumns = PXView.SortColumns;
        bool[] descendings = PXView.Descendings;
        PXFilterRow[] filters = (PXFilterRow[]) PXView.Filters;
        ref int local1 = ref startRow;
        ref int local2 = ref num;
        foreach (PXResult<PX.Translation.LocalizationValue, PX.Translation.LocalizationResource, PX.Translation.LocalizationTranslation> pxResult in exceptionalRecordView.Select((object[]) null, parameters2, (object[]) null, sortColumns, descendings, filters, ref local1, 0, ref local2))
        {
          PX.Translation.LocalizationValue localizationValue = (PX.Translation.LocalizationValue) pxResult;
          PX.Translation.LocalizationTranslation localizationTranslation = (PX.Translation.LocalizationTranslation) pxResult;
          PX.Translation.LocalizationResource localizationResource = (PX.Translation.LocalizationResource) pxResult;
          T obj1 = new T();
          obj1.Id = localizationValue.Id;
          obj1.IdRes = localizationResource.Id;
          obj1.NeutralValue = localizationValue.NeutralValue;
          obj1.ResKey = localizationResource.ResKey;
          obj1.IsNotLocalized = localizationResource.IsNotLocalized;
          if (!(cache.Locate((object) obj1) is T obj2) && cache.Insert((object) obj1) is T obj3)
          {
            cache.SetStatus((object) obj3, PXEntryStatus.Held);
            obj2 = obj3;
          }
          if ((object) obj2 != null)
          {
            dictionary[ResourceByScreenCollection.GetKey(neutralValue, obj2.ResKey)] = obj2;
            if (!string.IsNullOrEmpty(localizationTranslation.Locale))
              obj2.Values[localizationTranslation.Locale] = (object) localizationTranslation.Value;
          }
        }
        PXView.StartRow = 0;
        cache.IsDirty = isDirty;
      }
      return (IEnumerable) dictionary.Values;
    }

    private IEnumerable SelectLocalizationRecord<T>(
      ref Dictionary<string, T> cachedRecords,
      PXCache cache,
      bool isObsolete,
      System.Action initializationMethod)
      where T : LocalizationRecord, new()
    {
      bool isDirty = cache.IsDirty;
      this.EnsureCachedParents<string, T>(ref cachedRecords);
      if (!string.IsNullOrEmpty(this.translationGraph.LanguageFilter.Current.Language))
      {
        initializationMethod();
        object[] parameters;
        PXView localizationRecordView = this.GetLocalizationRecordView(isObsolete, out parameters);
        int startRow = PXView.StartRow;
        int totalRows = 0;
        bool[] descendings = PXView.Descendings;
        if (startRow < 0)
        {
          startRow = -startRow - PXView.MaximumRows;
          for (int index = 0; index < descendings.Length; ++index)
            descendings[index] = !descendings[index];
        }
        foreach (PXResult<PX.Translation.LocalizationValue, PX.Translation.LocalizationTranslation> pxResult in localizationRecordView.Select((object[]) null, parameters, PXView.Searches, PXView.SortColumns, descendings, (PXFilterRow[]) PXView.Filters, ref startRow, 0, ref totalRows))
        {
          PX.Translation.LocalizationValue localizationValue = (PX.Translation.LocalizationValue) pxResult;
          PX.Translation.LocalizationTranslation localizationTranslation = (PX.Translation.LocalizationTranslation) pxResult;
          T obj1 = new T();
          obj1.Id = localizationValue.Id;
          obj1.NeutralValue = localizationValue.NeutralValue;
          obj1.IsNotLocalized = localizationValue.IsNotLocalized;
          T obj2 = obj1;
          if (!(cache.Locate((object) obj2) is T obj3) && cache.Insert((object) obj2) is T obj4)
          {
            cache.SetStatus((object) obj4, PXEntryStatus.Held);
            obj3 = obj4;
          }
          if ((object) obj3 != null && !string.IsNullOrEmpty(obj3.NeutralValue))
          {
            if (string.IsNullOrEmpty(localizationTranslation.Locale) || this.translationGraph.LanguageFilter.Current.Language.Contains(localizationTranslation.Locale))
              cachedRecords[obj3.NeutralValue] = obj3;
            if (!string.IsNullOrEmpty(localizationTranslation.Locale) && !obj3.Values.ContainsKey(localizationTranslation.Locale))
            {
              obj3.Values[localizationTranslation.Locale] = (object) localizationTranslation.Value;
              obj3.CreatedValues[localizationTranslation.Locale] = localizationTranslation.CreatedDateTime;
              obj3.LastModifiedValues[localizationTranslation.Locale] = localizationTranslation.LastModifiedDateTime;
            }
          }
        }
      }
      PXView.StartRow = 0;
      cache.IsDirty = isDirty;
      return (IEnumerable) cachedRecords.Values;
    }

    public PXFilterRow[] GetNormalizedFilters(PXFilterRow[] filters)
    {
      List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
      if (filters != null)
      {
        foreach (PXFilterRow filter in filters)
        {
          PXFilterRow row = filter;
          if ((string.Compare(row.DataField, typeof (LocalizationRecord.localizedValue).Name, StringComparison.OrdinalIgnoreCase) == 0 ? 1 : (((IEnumerable<string>) this.LocaleKeys).Any<string>((System.Func<string, bool>) (l => l.Equals(row.DataField, StringComparison.OrdinalIgnoreCase))) ? 1 : 0)) != 0)
            row.DataField = $"{typeof (PX.Translation.LocalizationTranslation).Name}__{typeof (PX.Translation.LocalizationTranslation.value).Name}";
          pxFilterRowList.Add(row);
        }
      }
      return pxFilterRowList.ToArray();
    }

    private HashSet<string> GetTreeScreenIdSet(string rootScreenId)
    {
      HashSet<string> treeScreenIdSet = (HashSet<string>) null;
      if (!string.IsNullOrEmpty(rootScreenId))
      {
        if (TranslationMaint.MultilingualTranslator.childNodesByScreenId == null)
          TranslationMaint.MultilingualTranslator.childNodesByScreenId = new Dictionary<string, HashSet<string>>();
        if (TranslationMaint.MultilingualTranslator.childNodesByScreenId.ContainsKey(rootScreenId))
        {
          treeScreenIdSet = TranslationMaint.MultilingualTranslator.childNodesByScreenId[rootScreenId];
        }
        else
        {
          HashSet<string> stringSet = new HashSet<string>();
          Queue<string> stringQueue = new Queue<string>();
          stringQueue.Enqueue(rootScreenId);
          while (stringQueue.Count > 0)
          {
            string screenID = stringQueue.Dequeue();
            PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID);
            if (screenIdUnsecure != null)
            {
              stringSet.Add(screenID);
              foreach (PXSiteMapNode pxSiteMapNode in PXSiteMap.Provider.GetChildNodesUnsecureSimple(screenIdUnsecure))
                stringQueue.Enqueue(pxSiteMapNode.ScreenID);
            }
          }
          TranslationMaint.MultilingualTranslator.childNodesByScreenId[rootScreenId] = stringSet;
          treeScreenIdSet = stringSet;
        }
      }
      return treeScreenIdSet;
    }

    private PXView GetLocalizationExceptionalRecordView(
      string neutralValue,
      out object[] parameters)
    {
      List<object> parameters1 = new List<object>();
      parameters1.Add((object) neutralValue);
      bool? showUnboundOnly = this.translationGraph.LanguageFilter.Current.ShowUnboundOnly;
      bool flag1 = true;
      BqlCommand select1;
      if (showUnboundOnly.GetValueOrDefault() == flag1 & showUnboundOnly.HasValue)
      {
        select1 = (BqlCommand) new Select2<PX.Translation.LocalizationValue, InnerJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationValue.id, Equal<PX.Translation.LocalizationResource.idValue>>, LeftJoin<PX.Translation.LocalizationTranslation, On<PX.Translation.LocalizationValue.id, Equal<PX.Translation.LocalizationTranslation.idValue>, And<PX.Translation.LocalizationResource.id, Equal<PX.Translation.LocalizationTranslation.idRes>>>, LeftJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationResource.idValue>, And<LocalizationResourceByScreen.idRes, Equal<PX.Translation.LocalizationResource.id>>>>>>, Where<PX.Translation.LocalizationValue.neutralValue, Equal<Required<PX.Translation.LocalizationValue.neutralValue>>, And<LocalizationResourceByScreen.idValue, IsNull>>, OrderBy<Asc<PX.Translation.LocalizationResource.resKey>>>();
        LocalizationResourceType[] resourceTypesFromString = ResourceCollectingManager.ParseResourceTypesFromString(this.translationGraph.LanguageFilter.Current.ShowType);
        if (resourceTypesFromString.Length != 0)
        {
          select1 = select1.WhereAnd(InHelper<PX.Translation.LocalizationResource.resType>.Create(resourceTypesFromString.Length));
          parameters1.AddRange(((IEnumerable<LocalizationResourceType>) resourceTypesFromString).Select<LocalizationResourceType, object>((System.Func<LocalizationResourceType, object>) (type => (object) (int) type)));
        }
      }
      else
      {
        select1 = (BqlCommand) new Select2<PX.Translation.LocalizationValue, InnerJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationValue.id, Equal<PX.Translation.LocalizationResource.idValue>>, LeftJoin<PX.Translation.LocalizationTranslation, On<PX.Translation.LocalizationValue.id, Equal<PX.Translation.LocalizationTranslation.idValue>, And<PX.Translation.LocalizationResource.id, Equal<PX.Translation.LocalizationTranslation.idRes>>>, LeftJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationValue.id>, And<LocalizationResourceByScreen.idRes, Equal<PX.Translation.LocalizationResource.id>>>>>>, Where<PX.Translation.LocalizationValue.neutralValue, Equal<Required<PX.Translation.LocalizationValue.neutralValue>>, And<LocalizationResourceByScreen.idValue, IsNotNull>>, OrderBy<Asc<PX.Translation.LocalizationResource.resKey>>>();
        string screenId = this.translationGraph.LanguageFilter.Current.ScreenID;
        this.AppendScreenIdRestriction<LocalizationResourceByScreen.screenID>(ref select1, parameters1, screenId);
      }
      bool? showExcluded = this.translationGraph.LanguageFilter.Current.ShowExcluded;
      bool flag2 = true;
      if (!(showExcluded.GetValueOrDefault() == flag2 & showExcluded.HasValue))
        select1 = select1.WhereAnd<Where<PX.Translation.LocalizationResource.isNotLocalized, Equal<False>>>();
      BqlCommand select2 = !PXSiteMap.IsPortal ? select1.WhereAnd<Where<PX.Translation.LocalizationResource.isSite, Equal<PX.Data.True>>>() : select1.WhereAnd<Where<PX.Translation.LocalizationResource.isPortal, Equal<PX.Data.True>>>();
      parameters = parameters1.ToArray();
      return new PXView((PXGraph) this.translationGraph, true, select2);
    }

    private void AppendScreenIdRestriction<TIBqlField>(
      ref BqlCommand select,
      List<object> parameters,
      string screenId)
      where TIBqlField : IBqlField
    {
      if (string.IsNullOrEmpty(screenId) || string.Compare(screenId, PXSiteMap.RootNode.ScreenID, StringComparison.OrdinalIgnoreCase) == 0)
        return;
      object[] array = ((IEnumerable<object>) this.GetTreeScreenIdSet(screenId)).ToArray<object>();
      parameters.Add((object) array);
      select = select.WhereAnd<Where<TIBqlField, In<Required<TIBqlField>>>>();
    }

    private PXView GetLocalizationRecordView(bool isObsolete, out object[] parameters)
    {
      List<object> parameters1 = new List<object>();
      parameters1.Add((object) "D41D8CD98F00B204E9800998ECF8427E");
      bool? showUnboundOnly = this.translationGraph.LanguageFilter.Current.ShowUnboundOnly;
      bool flag1 = true;
      BqlCommand select1;
      if (showUnboundOnly.GetValueOrDefault() == flag1 & showUnboundOnly.HasValue)
      {
        select1 = (BqlCommand) new Select5<PX.Translation.LocalizationValue, LeftJoin<PX.Translation.LocalizationTranslation, On<PX.Translation.LocalizationTranslation.idValue, Equal<PX.Translation.LocalizationValue.id>, And<Where<PX.Translation.LocalizationTranslation.idRes, IsNull, Or<PX.Translation.LocalizationTranslation.idRes, Equal<Required<PX.Translation.LocalizationTranslation.idRes>>>>>>, LeftJoin<PX.Translation.LocalizationResource, On<PX.Translation.LocalizationResource.idValue, Equal<PX.Translation.LocalizationValue.id>>, LeftJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationResource.idValue>, And<LocalizationResourceByScreen.idRes, Equal<PX.Translation.LocalizationResource.id>>>, LeftJoin<Locale, On<Locale.localeName, Equal<PX.Translation.LocalizationTranslation.locale>>>>>>, Where<LocalizationResourceByScreen.idValue, IsNull>, Aggregate<GroupBy<PX.Translation.LocalizationValue.id, GroupBy<PX.Translation.LocalizationValue.neutralValue, GroupBy<PX.Translation.LocalizationValue.isNotLocalized, GroupBy<PX.Translation.LocalizationTranslation.value, GroupBy<PX.Translation.LocalizationTranslation.locale, GroupBy<Locale.localeName, GroupBy<PX.Translation.LocalizationTranslation.createdDateTime, GroupBy<PX.Translation.LocalizationTranslation.lastModifiedDateTime>>>>>>>>>>();
        LocalizationResourceType[] resourceTypesFromString = ResourceCollectingManager.ParseResourceTypesFromString(this.translationGraph.LanguageFilter.Current.ShowType);
        if (resourceTypesFromString.Length != 0)
        {
          select1 = select1.WhereAnd(InHelper<PX.Translation.LocalizationResource.resType>.Create(resourceTypesFromString.Length));
          parameters1.AddRange(((IEnumerable<LocalizationResourceType>) resourceTypesFromString).Select<LocalizationResourceType, object>((System.Func<LocalizationResourceType, object>) (type => (object) (int) type)));
        }
      }
      else
      {
        select1 = (BqlCommand) new Select5<PX.Translation.LocalizationValue, LeftJoin<PX.Translation.LocalizationTranslation, On<PX.Translation.LocalizationTranslation.idValue, Equal<PX.Translation.LocalizationValue.id>, And<Where<PX.Translation.LocalizationTranslation.idRes, IsNull, Or<PX.Translation.LocalizationTranslation.idRes, Equal<Required<PX.Translation.LocalizationTranslation.idRes>>>>>>, LeftJoin<LocalizationResourceByScreen, On<LocalizationResourceByScreen.idValue, Equal<PX.Translation.LocalizationValue.id>>, LeftJoin<Locale, On<Locale.localeName, Equal<PX.Translation.LocalizationTranslation.locale>>>>>, Where<LocalizationResourceByScreen.idValue, IsNotNull>, Aggregate<GroupBy<PX.Translation.LocalizationValue.id, GroupBy<PX.Translation.LocalizationValue.neutralValue, GroupBy<PX.Translation.LocalizationValue.isNotLocalized, GroupBy<PX.Translation.LocalizationTranslation.value, GroupBy<PX.Translation.LocalizationTranslation.locale, GroupBy<Locale.localeName, GroupBy<PX.Translation.LocalizationTranslation.createdDateTime, GroupBy<PX.Translation.LocalizationTranslation.lastModifiedDateTime>>>>>>>>>>();
        string screenId = this.translationGraph.LanguageFilter.Current.ScreenID;
        this.AppendScreenIdRestriction<LocalizationResourceByScreen.screenID>(ref select1, parameters1, screenId);
      }
      BqlCommand select2 = !PXSiteMap.IsPortal ? select1.WhereAnd<Where<PX.Translation.LocalizationValue.isSite, Equal<PX.Data.True>>>().WhereAnd<Where<PX.Translation.LocalizationValue.isObsolete, Equal<Required<PX.Translation.LocalizationValue.isObsolete>>>>() : select1.WhereAnd<Where<PX.Translation.LocalizationValue.isPortal, Equal<PX.Data.True>>>().WhereAnd<Where<PX.Translation.LocalizationValue.isObsoletePortal, Equal<Required<PX.Translation.LocalizationValue.isObsoletePortal>>>>();
      parameters1.Add((object) isObsolete);
      bool? nullable1 = this.translationGraph.LanguageFilter.Current.ShowLocalized;
      bool flag2 = true;
      if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
      {
        select2 = select2.WhereAnd<Where<PX.Translation.LocalizationValue.translationCount, Less<Required<PX.Translation.LocalizationValue.translationCount>>>>();
        if (this.translationGraph.LanguageFilter.Current.Language == null || !this.translationGraph.LanguageFilter.Current.Language.Contains("en-US"))
          parameters1.Add((object) (this.Locales.Count - 1));
        else
          parameters1.Add((object) this.Locales.Count);
      }
      nullable1 = this.translationGraph.LanguageFilter.Current.ShowExcluded;
      bool flag3 = true;
      if (!(nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue))
        select2 = select2.WhereAnd<Where<PX.Translation.LocalizationValue.isNotLocalized, Equal<False>>>();
      System.DateTime? nullable2 = this.translationGraph.LanguageFilter.Current.CreatedDateTime;
      if (nullable2.HasValue)
      {
        select2 = select2.WhereAnd<Where<PX.Translation.LocalizationTranslation.createdDateTime, GreaterEqual<Required<PX.Translation.LocalizationTranslation.createdDateTime>>>>();
        parameters1.Add((object) this.translationGraph.LanguageFilter.Current.CreatedDateTime);
      }
      nullable2 = this.translationGraph.LanguageFilter.Current.LastModifiedDateTime;
      if (nullable2.HasValue)
      {
        select2 = select2.WhereAnd<Where<PX.Translation.LocalizationTranslation.lastModifiedDateTime, GreaterEqual<Required<PX.Translation.LocalizationTranslation.lastModifiedDateTime>>>>();
        parameters1.Add((object) this.translationGraph.LanguageFilter.Current.LastModifiedDateTime);
      }
      parameters = parameters1.ToArray();
      return (PXView) new LocalizationRecordView(this.translationGraph, true, select2);
    }

    private void EnsureCachedParents<TKey, TValue>(ref Dictionary<TKey, TValue> parents)
    {
      if (parents == null)
        parents = new Dictionary<TKey, TValue>();
      parents.Clear();
    }

    private void InitializeLocalizationExceptionRecordCache<T>(PXCache cache, PXCache parentCache) where T : LocalizationExceptionRecord
    {
      if (this.translationGraph.LanguageFilter.Current == null || string.IsNullOrEmpty(this.translationGraph.LanguageFilter.Current.Language))
        return;
      string[] source = this.translationGraph.LanguageFilter.Current.Language.Split(',');
      foreach (string key in this.Locales.Keys)
      {
        if (cache.Fields.Contains(key) && !((IEnumerable<string>) source).Contains<string>(key))
          cache.Fields.Remove(key);
        else if (((IEnumerable<string>) source).Contains<string>(key) && !cache.Fields.Contains(key))
        {
          cache.Fields.Add(key);
          PXUIFieldAttribute.SetDisplayName(cache, key, this.Locales[key]);
          this.AttachDynamicExceptionalFieldHandlers<T>(key, parentCache);
        }
      }
    }

    private void InitializeLocalizationRecordCache<T>(PXCache cache, bool isObsolete) where T : LocalizationRecord
    {
      if (this.translationGraph.LanguageFilter.Current == null || string.IsNullOrEmpty(this.translationGraph.LanguageFilter.Current.Language))
        return;
      string[] source = this.translationGraph.LanguageFilter.Current.Language.Split(',');
      foreach (string key in this.Locales.Keys)
      {
        if (cache.Fields.Contains(key) && !((IEnumerable<string>) source).Contains<string>(key))
        {
          cache.Fields.Remove(key);
          cache.FieldSelectingEvents.Remove(key);
          cache.FieldUpdatingEvents.Remove(key);
        }
        else if (((IEnumerable<string>) source).Contains<string>(key) && !cache.Fields.Contains(key))
        {
          cache.Fields.Add(key);
          PXUIFieldAttribute.SetDisplayName(cache, key, this.Locales[key]);
          this.AttachDynamicFieldHandlers<T>(key, isObsolete);
        }
      }
    }

    private object GetTranslatedValue(object row, string locale)
    {
      if (!(row is LocalizationRecord localizationRecord))
        return (object) null;
      return localizationRecord.Values == null || !localizationRecord.Values.ContainsKey(locale) ? (object) null : localizationRecord.Values[locale];
    }

    private void AttachDynamicExceptionalFieldHandlers<T>(string fieldName, PXCache parentCache) where T : LocalizationExceptionRecord
    {
      this.translationGraph.FieldSelecting.AddHandler(typeof (T), fieldName, (PXFieldSelecting) ((sender, args) =>
      {
        PXFieldSelectingEventArgs selectingEventArgs = args;
        object returnState = args.ReturnState;
        System.Type dataType = typeof (string);
        string str = fieldName;
        string locale = this.Locales[fieldName];
        bool? isKey = new bool?();
        bool? nullable = new bool?();
        int? required = new int?();
        int? precision = new int?();
        int? length = new int?();
        string fieldName1 = str;
        string displayName = locale;
        bool? enabled = new bool?();
        bool? visible = new bool?();
        bool? readOnly = new bool?();
        PXFieldState instance = PXFieldState.CreateInstance(returnState, dataType, isKey, nullable, required, precision, length, fieldName: fieldName1, displayName: displayName, enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Dynamic);
        selectingEventArgs.ReturnState = (object) instance;
        args.ReturnValue = this.GetTranslatedValue(args.Row, fieldName);
      }));
      this.translationGraph.FieldUpdating.AddHandler(typeof (T), fieldName, (PXFieldUpdating) ((sender, args) =>
      {
        if (!(args.Row is T row2))
          return;
        string newValue = args.NewValue as string;
        this.EnsureDefaultTranslationValue(parentCache, (LocalizationExceptionRecord) row2, fieldName);
        this.ValidateTranslatedValue(row2.NeutralValue, newValue);
        row2.Values[fieldName] = (object) newValue;
        this.UpdateTranslationCache(row2.Id, row2.IdRes, newValue, fieldName);
        if (!this.translationGraph.IsCurrentCultureEditing || this.translationGraph.IsSiteMapAltered)
          return;
        this.translationGraph.IsSiteMapAltered = this.translationGraph.IsSiteMapResource((LocalizationExceptionRecord) row2);
      }));
    }

    private void AttachDynamicFieldHandlers<T>(string fieldName, bool isObsolete) where T : LocalizationRecord
    {
      this.translationGraph.FieldSelecting.AddHandler(typeof (T), fieldName, (PXFieldSelecting) ((sender, args) =>
      {
        PXFieldSelectingEventArgs selectingEventArgs = args;
        object returnState = args.ReturnState;
        System.Type dataType = typeof (string);
        string str = fieldName;
        string locale = this.Locales[fieldName];
        bool? isKey = new bool?();
        bool? nullable = new bool?();
        int? required = new int?();
        int? precision = new int?();
        int? length = new int?();
        string fieldName1 = str;
        string displayName = locale;
        bool? enabled = new bool?();
        bool? visible = new bool?();
        bool? readOnly = new bool?();
        PXFieldState instance = PXFieldState.CreateInstance(returnState, dataType, isKey, nullable, required, precision, length, fieldName: fieldName1, displayName: displayName, enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Dynamic);
        selectingEventArgs.ReturnState = (object) instance;
        args.ReturnValue = this.GetTranslatedValue(args.Row, fieldName);
      }));
      this.translationGraph.FieldUpdating.AddHandler(typeof (T), fieldName, (PXFieldUpdating) ((sender, args) =>
      {
        if (!(args.Row is T row2))
          return;
        string newValue = args.NewValue as string;
        this.EnsureDefaultTranslationValue(newValue, (LocalizationRecord) row2, fieldName);
        this.ValidateTranslatedValue(row2.NeutralValue, newValue);
        if (row2.Values == null)
          return;
        row2.Values[fieldName] = args.NewValue;
        this.UpdateTranslationCache(row2.Id, "D41D8CD98F00B204E9800998ECF8427E", newValue, fieldName);
        if (string.IsNullOrEmpty(row2.NeutralValue) || !this.translationGraph.IsCurrentCultureEditing || this.translationGraph.IsSiteMapAltered)
          return;
        IQueryable<LocalizationExceptionRecord> source;
        if (!isObsolete)
          source = this.translationGraph.ExceptionalResources.Select().Select<PXResult<LocalizationExceptionRecord>, LocalizationExceptionRecord>((Expression<System.Func<PXResult<LocalizationExceptionRecord>, LocalizationExceptionRecord>>) (value => (LocalizationExceptionRecord) value));
        else
          source = this.translationGraph.ExceptionalResourcesObsolete.Select().Select<PXResult<LocalizationExceptionRecordObsolete>, LocalizationExceptionRecord>((Expression<System.Func<PXResult<LocalizationExceptionRecordObsolete>, LocalizationExceptionRecord>>) (value => (LocalizationExceptionRecordObsolete) value));
        this.translationGraph.IsSiteMapAltered = ((IEnumerable<LocalizationExceptionRecord>) Enumerable.Cast<LocalizationExceptionRecord>(source).ToArray<LocalizationExceptionRecord>()).Any<LocalizationExceptionRecord>(new System.Func<LocalizationExceptionRecord, bool>(this.translationGraph.IsSiteMapResource));
      }));
    }

    private void EnsureDefaultTranslationValue(
      string translatedValue,
      LocalizationRecord row,
      string locale)
    {
      if (!string.IsNullOrEmpty(translatedValue))
        return;
      if ((PXResult<PX.Translation.LocalizationTranslation, PX.Translation.LocalizationValue>) (PXResult<PX.Translation.LocalizationTranslation>) PXSelectBase<PX.Translation.LocalizationTranslation, PXSelectJoin<PX.Translation.LocalizationTranslation, InnerJoin<PX.Translation.LocalizationValue, On<PX.Translation.LocalizationValue.id, Equal<PX.Translation.LocalizationTranslation.idValue>>>, Where<PX.Translation.LocalizationTranslation.locale, Equal<Required<LangFilter.language>>, And<PX.Translation.LocalizationTranslation.value, NotEqual<Required<PX.Translation.LocalizationTranslation.value>>, And<PX.Translation.LocalizationValue.neutralValue, Equal<Required<PX.Translation.LocalizationValue.neutralValue>>, And<PX.Translation.LocalizationTranslation.idRes, NotEqual<Required<PX.Translation.LocalizationTranslation.idRes>>>>>>>.Config>.SelectMultiBound((PXGraph) this.translationGraph, (object[]) null, (object) locale, (object) "", (object) row.NeutralValue, (object) "D41D8CD98F00B204E9800998ECF8427E") != null)
        throw new PXException("You cannot delete the default translation because exceptional translations exist. If you want to delete all translations of this value, you must delete exceptional translations first.");
    }

    private void EnsureDefaultTranslationValue(
      PXCache localizationRecordCache,
      LocalizationExceptionRecord row,
      string locale)
    {
      PX.Translation.LocalizationTranslation localizationTranslation = (PX.Translation.LocalizationTranslation) ((PXResult<PX.Translation.LocalizationTranslation, PX.Translation.LocalizationValue>) (PXResult<PX.Translation.LocalizationTranslation>) PXSelectBase<PX.Translation.LocalizationTranslation, PXSelectJoin<PX.Translation.LocalizationTranslation, InnerJoin<PX.Translation.LocalizationValue, On<PX.Translation.LocalizationValue.id, Equal<PX.Translation.LocalizationTranslation.idValue>>>, Where<PX.Translation.LocalizationTranslation.locale, Equal<Required<LangFilter.language>>, And<PX.Translation.LocalizationValue.neutralValue, Equal<Required<PX.Translation.LocalizationValue.neutralValue>>, And<Where<PX.Translation.LocalizationTranslation.idRes, IsNull, Or<PX.Translation.LocalizationTranslation.idRes, Equal<Required<PX.Translation.LocalizationTranslation.idRes>>>>>>>>.Config>.SelectWindowed((PXGraph) this.translationGraph, 0, 1, (object) locale, (object) row.NeutralValue, (object) "D41D8CD98F00B204E9800998ECF8427E") ?? throw new PXException("Define a default value first."));
      LocalizationRecord localizationRecord1 = new LocalizationRecord()
      {
        Id = localizationTranslation.IdValue
      };
      if (!(localizationRecordCache.Locate((object) localizationRecord1) is LocalizationRecord localizationRecord2) || string.IsNullOrEmpty(localizationRecord2.Values[locale] as string))
        throw new PXException("Define a default value first.");
    }

    private void ValidateTranslatedValue(string neutral, string value)
    {
      if (string.IsNullOrEmpty(neutral) || string.IsNullOrEmpty(value))
        return;
      Regex regex = new Regex("(?<Par>{(\\w+|:+)})", RegexOptions.CultureInvariant);
      MatchCollection matchCollection1 = regex.Matches(neutral);
      MatchCollection matchCollection2 = regex.Matches(value);
      if (matchCollection1.Count > 0 && matchCollection2.Count == 0)
        throw new PXException($"The string parameters defined in the translated string do not match the number of parameters in the source string.{Environment.NewLine}{neutral}{Environment.NewLine}");
    }

    public void UpdateValueCache(string id, bool? isNotLocalized, int? translationsCountDelta)
    {
      if (string.IsNullOrEmpty(id) || !isNotLocalized.HasValue && !translationsCountDelta.HasValue)
        return;
      if (!(this.translationGraph.LocalizationValue.Cache.Locate((object) new PX.Translation.LocalizationValue()
      {
        Id = id
      }) is PX.Translation.LocalizationValue localizationValue1))
        localizationValue1 = this.SelectLocalizationValue(id);
      if (isNotLocalized.HasValue)
        localizationValue1.IsNotLocalized = isNotLocalized;
      if (translationsCountDelta.HasValue)
      {
        PX.Translation.LocalizationValue localizationValue2 = localizationValue1;
        int? translationCount = localizationValue2.TranslationCount;
        int? nullable = translationsCountDelta;
        localizationValue2.TranslationCount = translationCount.HasValue & nullable.HasValue ? new int?(translationCount.GetValueOrDefault() + nullable.GetValueOrDefault()) : new int?();
      }
      this.translationGraph.LocalizationValue.Cache.Update((object) localizationValue1);
    }

    public PX.Translation.LocalizationValue SelectLocalizationValue(string id)
    {
      return (PX.Translation.LocalizationValue) PXSelectBase<PX.Translation.LocalizationValue, PXSelectReadonly<PX.Translation.LocalizationValue, Where<PX.Translation.LocalizationValue.id, Equal<Required<PX.Translation.LocalizationValue.id>>>>.Config>.SelectWindowed((PXGraph) this.translationGraph, 0, 1, (object) id);
    }

    public void UpdateResourceCache(string idValue, string idRes, bool? isNotLocalized)
    {
      if (string.IsNullOrEmpty(idValue) || string.IsNullOrEmpty(idRes) || !isNotLocalized.HasValue)
        return;
      if (!(this.translationGraph.LocalizationResource.Cache.Locate((object) new PX.Translation.LocalizationResource()
      {
        IdValue = idValue,
        Id = idRes
      }) is PX.Translation.LocalizationResource localizationResource))
        localizationResource = (PX.Translation.LocalizationResource) PXSelectBase<PX.Translation.LocalizationResource, PXSelectReadonly<PX.Translation.LocalizationResource, Where<PX.Translation.LocalizationResource.idValue, Equal<Required<PX.Translation.LocalizationResource.idValue>>, And<PX.Translation.LocalizationResource.id, Equal<Required<PX.Translation.LocalizationResource.id>>>>>.Config>.SelectWindowed((PXGraph) this.translationGraph, 0, 1, (object) idValue, (object) idRes);
      localizationResource.IsNotLocalized = isNotLocalized;
      this.translationGraph.LocalizationResource.Cache.Update((object) localizationResource);
    }

    public PX.Translation.LocalizationTranslation SelectLocalizationTranslation(
      string idValue,
      string idRes,
      string locale)
    {
      return (PX.Translation.LocalizationTranslation) PXSelectBase<PX.Translation.LocalizationTranslation, PXSelectReadonly<PX.Translation.LocalizationTranslation, Where<PX.Translation.LocalizationTranslation.idValue, Equal<Required<PX.Translation.LocalizationTranslation.idValue>>, And<PX.Translation.LocalizationTranslation.locale, Equal<Required<LangFilter.language>>, And<PX.Translation.LocalizationTranslation.idRes, Equal<Required<PX.Translation.LocalizationTranslation.idRes>>>>>>.Config>.SelectWindowed((PXGraph) this.translationGraph, 0, 1, (object) idValue, (object) locale, (object) idRes);
    }

    public void UpdateTranslationCache(string idValue, string idRes, string value, string locale)
    {
      if (string.IsNullOrEmpty(idValue) || string.IsNullOrEmpty(idRes))
        return;
      if (!(this.translationGraph.LocalizationTranslation.Cache.Locate((object) new PX.Translation.LocalizationTranslation()
      {
        IdValue = idValue,
        IdRes = idRes,
        Locale = locale
      }) is PX.Translation.LocalizationTranslation localizationTranslation))
        localizationTranslation = this.SelectLocalizationTranslation(idValue, idRes, locale);
      string oldTranslationValue;
      if (localizationTranslation == null)
      {
        oldTranslationValue = (string) null;
        localizationTranslation = new PX.Translation.LocalizationTranslation()
        {
          IdValue = idValue,
          Locale = locale,
          IdRes = idRes,
          Value = value ?? ""
        };
        this.translationGraph.LocalizationTranslation.Cache.Insert((object) localizationTranslation);
      }
      else
      {
        oldTranslationValue = localizationTranslation.Value;
        localizationTranslation.Value = value ?? "";
        this.translationGraph.LocalizationTranslation.Cache.Update((object) localizationTranslation);
      }
      this.CorrectTranslationsCount(idValue, oldTranslationValue, localizationTranslation.Value);
    }

    private void CorrectTranslationsCount(
      string idValue,
      string oldTranslationValue,
      string newTranslationValue)
    {
      if (string.IsNullOrEmpty(oldTranslationValue) && !string.IsNullOrEmpty(newTranslationValue))
      {
        this.UpdateValueCache(idValue, new bool?(), new int?(1));
      }
      else
      {
        if (string.IsNullOrEmpty(oldTranslationValue) || !string.IsNullOrEmpty(newTranslationValue))
          return;
        this.UpdateValueCache(idValue, new bool?(), new int?(-1));
      }
    }
  }

  private class CachedResourceByScreen : IPrefetchable, IPXCompanyDependent
  {
    private HashSet<string> localizationValues;
    private HashSet<string> localizationResources;

    public void Prefetch()
    {
      this.localizationValues = new HashSet<string>();
      this.localizationResources = new HashSet<string>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<LocalizationResourceByScreen>(new PXDataField(typeof (LocalizationResourceByScreen.idValue).Name), new PXDataField(typeof (LocalizationResourceByScreen.idRes).Name), new PXDataField(typeof (LocalizationResourceByScreen.screenID).Name)))
      {
        if (pxDataRecord != null)
        {
          string idValue = pxDataRecord.GetString(0);
          string idRes = pxDataRecord.GetString(1);
          string screenId = pxDataRecord.GetString(2);
          this.localizationValues.Add(this.GenerateValueKey(idValue, screenId));
          this.localizationResources.Add(this.GenerateResourceKey(idValue, idRes, screenId));
        }
      }
    }

    public bool ContainsValue(PX.Translation.LocalizationValue value, string screenId)
    {
      bool flag = false;
      if (value != null && !string.IsNullOrEmpty(screenId) && this.localizationValues != null)
        flag = this.localizationValues.Contains(this.GenerateValueKey(value.Id, screenId));
      return flag;
    }

    public bool ContainsResource(PX.Translation.LocalizationResource resource, string screenId)
    {
      bool flag = false;
      if (resource != null && !string.IsNullOrEmpty(screenId) && this.localizationResources != null)
        flag = this.localizationResources.Contains(this.GenerateResourceKey(resource.IdValue, resource.Id, screenId));
      return flag;
    }

    private string GenerateValueKey(string idValue, string screenId) => $"{idValue}{screenId}";

    private string GenerateResourceKey(string idValue, string idRes, string screenId)
    {
      return $"{idValue}{idRes}{screenId}";
    }
  }
}
