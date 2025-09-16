// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXPageRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Api.Soap.Screen;
using PX.Common;
using PX.Common.Context;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Data.LocalizationKeyGenerators;
using PX.Data.Process;
using PX.Data.ProjectDefinition.Workflow;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.UI;

#nullable disable
namespace PX.Translation;

/// <exclude />
internal class PXPageRipper : PXRipper
{
  private const string MAIN_FILE_NAME = "Main";
  private const string CACHE_ATTACHED_POSTFIX = "CacheAttached";
  private const string COLLECTION_RESOURCES_KEY = "CollectingResources";

  private static void SetCollector(HttpContext httpContext, PXControlPropertiesCollector value)
  {
    httpContext.Items[(object) "CollectingResources"] = (object) value;
  }

  internal static PXControlPropertiesCollector GetCollector(HttpContext httpContext)
  {
    return httpContext.Items[(object) "CollectingResources"] as PXControlPropertiesCollector;
  }

  internal static bool ContainsCollector(HttpContext httpContext)
  {
    return httpContext.Items.Contains((object) "CollectingResources");
  }

  private static PXControlPropertiesCollector Collector
  {
    get
    {
      PXControlPropertiesCollector collector = (PXControlPropertiesCollector) null;
      if (HttpContext.Current != null && PXPageRipper.ContainsCollector(HttpContext.Current))
        collector = PXPageRipper.GetCollector(HttpContext.Current);
      return collector;
    }
  }

  protected override void Rip(
    System.IO.FileInfo file,
    List<string> processed,
    ResourceCollection result,
    LocalizationTranslationSetItem item = null,
    TranslationSetMaint graph = null,
    bool standalone = true)
  {
    if (item != null)
      item.IsCollected = new bool?(false);
    if (processed.Contains(file.FullName))
      return;
    processed.Add(file.FullName);
    string relatedCustomizedUrl = this.GetRelatedCustomizedUrl(file);
    string str;
    bool flag;
    if (relatedCustomizedUrl != null)
    {
      str = relatedCustomizedUrl;
      flag = true;
    }
    else
    {
      str = "~/" + file.FullName.Substring(this.directory.FullName.Length).Replace('\\', '/');
      if (string.CompareOrdinal(file.Name, "Main.aspx") == 0)
        ResourceCollectingManager.DontAddHeaderToResponce = new bool?(true);
      flag = ((str.StartsWith("~/Pages/", StringComparison.OrdinalIgnoreCase) ? 1 : (str.StartsWith("~/Wiki/", StringComparison.OrdinalIgnoreCase) ? 1 : 0)) | (standalone ? 1 : 0)) != 0;
    }
    if (!flag)
      return;
    HttpContext httpContext = HttpContext.Current;
    LocalizationResourceLite.CurrentScreenID = PXPageRipper.GetScreenId(str, standalone, item);
    try
    {
      PXControlPropertiesCollector propsCollector = this.CreatePropsCollector(result);
      Func<ISlotStore, IDisposable> contextInitializer = StringCollectingScope.CreateContextInitializer(SlotStore.Instance);
      httpContext = this.CreateDummyContext(str);
      PXPageRipper.SetCollector(HttpContext.Current, propsCollector);
      using (HttpContext.Current.Slots().BeginLifetimeScope())
      {
        using (contextInitializer(HttpContext.Current.Slots()))
        {
          using (new SuppressPerformanceInfoScope())
          {
            PXSiteMap.Provider.SetCurrentNode(PXSiteMap.Provider.FindSiteMapNode(str));
            HttpContext.Current.Server.Execute(str, (TextWriter) new StringWriter());
          }
        }
      }
    }
    catch (Exception ex)
    {
      if (ex.InnerException != null && ex.InnerException.InnerException != null)
      {
        if (!(ex.InnerException.InnerException is PXScreenMisconfigurationException))
          PXTrace.WriteError(ex.InnerException.InnerException, "failed to process URL: {URL}", (object) str);
      }
      else if (!(ex is PXScreenMisconfigurationException))
        PXTrace.WriteError("failed to process URL: {URL}", (object) str);
    }
    finally
    {
      PXSessionStateStore.DisposeCurrentStore();
      LocalizationResourceLite.CurrentScreenID = (string) null;
      ResourceCollectingManager.DontAddHeaderToResponce = new bool?(false);
    }
    if (item != null)
    {
      item.IsCollected = new bool?(true);
      if (graph != null)
      {
        LocalizationTranslationSetItem translationSetItem = graph.TranslationSetItem.Locate(item);
        if (translationSetItem != null)
        {
          translationSetItem.IsCollected = item.IsCollected;
          graph.TranslationSetItem.Update(translationSetItem);
        }
      }
    }
    GC.Collect();
    HttpContext.Current = httpContext;
    PXContext.SetSlot<PXGraph.GraphStaticInfoDictionary>((PXGraph.GraphStaticInfoDictionary) null);
    PXContext.SetSlot<PXCache.CacheStaticInfoDictionary>((PXCache.CacheStaticInfoDictionary) null);
  }

  private static string GetScreenId(
    string url,
    bool isStandalone,
    LocalizationTranslationSetItem item = null)
  {
    bool flag = url.StartsWith("~/Pages/", StringComparison.OrdinalIgnoreCase) && url.EndsWith(".aspx") && url.Length > "AA123456.aspx".Length && url[url.Length - "AA123456.aspx".Length - 1] == '/';
    if (PagePathConverter.IsPathCustomized(url))
    {
      string original = PagePathConverter.FromCustomizedToOriginal(url);
      if (File.Exists(HostingEnvironment.MapPath(original)))
      {
        url = original;
        flag = true;
      }
    }
    if (isStandalone && !flag)
      return "00000000";
    return item == null || string.IsNullOrWhiteSpace(item.ScreenID) ? url.Substring(url.Length - "AA123456.aspx".Length, 8).ToUpper() : item.ScreenID;
  }

  public static void TryToProcessAspxField(
    string fieldName,
    object fieldState,
    PXGraph graph,
    string viewName)
  {
    if (graph == null || string.IsNullOrEmpty(viewName) || fieldState == null || string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(viewName) || !ResourceCollectingManager.IsStringCollecting && !TranslationValidationManager.ValidateCurrentLocale())
      return;
    int length = fieldName.IndexOf("__");
    PXCache pxCache;
    if (length >= 0)
    {
      string key = fieldName.Substring(0, length);
      pxCache = graph.Caches[key];
      fieldName = fieldName.Substring(length + 2);
    }
    else
      pxCache = graph.Views[viewName].Cache;
    string a = graph.GetType().Namespace;
    if (pxCache == null || string.Equals(a, "Customization", StringComparison.OrdinalIgnoreCase) || string.Equals(pxCache.GetItemType().Namespace, "PX.Web.Controls.Maint.Wizard", StringComparison.OrdinalIgnoreCase))
      return;
    CollectStringsSettings? collectSettings = ResourceCollectingManager.CollectSettings;
    CollectStringsSettings collectStringsSettings = CollectStringsSettings.TranslationSet;
    CollectResourceSettings resourceSettings = collectSettings.GetValueOrDefault() == collectStringsSettings & collectSettings.HasValue ? CollectResourceSettings.Resource | CollectResourceSettings.ResourceByScreen : CollectResourceSettings.ResourceByScreen;
    if (fieldState is PXStringState pxStringState && pxStringState._NeutralLabels != null)
    {
      PXStringListAttribute stringListAttribute = pxCache.GetAttributesReadonly(fieldName).OfType<PXStringListAttribute>().FirstOrDefault<PXStringListAttribute>();
      if (stringListAttribute == null || stringListAttribute.IsLocalizable)
      {
        if (ResourceCollectingManager.IsStringCollecting)
          PXPageRipper.RipList(fieldName, pxCache, pxStringState._NeutralLabels, resourceSettings);
        else if (TranslationValidationManager.ValidateCurrentLocale())
          PXLocalizerRepository.ListLocalizer.ValidateTranslation(fieldName, pxCache, pxStringState._NeutralLabels, pxStringState.AllowedLabels);
      }
    }
    else
    {
      PXIntState pxIntState = fieldState as PXIntState;
      PXIntListAttribute intListAttribute = pxCache.GetAttributesReadonly(fieldName).OfType<PXIntListAttribute>().FirstOrDefault<PXIntListAttribute>();
      if ((intListAttribute == null || intListAttribute.IsLocalizable) && pxIntState != null && pxIntState._NeutralLabels != null)
      {
        if (ResourceCollectingManager.IsStringCollecting)
          PXPageRipper.RipList(fieldName, pxCache, pxIntState._NeutralLabels, resourceSettings);
        else if (TranslationValidationManager.ValidateCurrentLocale())
          PXLocalizerRepository.ListLocalizer.ValidateTranslation(fieldName, pxCache, pxIntState._NeutralLabels, pxIntState.AllowedLabels);
      }
    }
    PXUIFieldAttribute uiField = pxCache.GetAttributes(fieldName).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
    if (uiField != null && uiField.IsLocalizable)
    {
      if (ResourceCollectingManager.IsStringCollecting)
        PXPageRipper.RipUIField(uiField, pxCache, uiField.GetFieldSourceType(pxCache), resourceSettings);
      else if (TranslationValidationManager.ValidateCurrentLocale())
        PXLocalizerRepository.UIFieldLocalizer.ValidateTranslation(uiField, pxCache.BqlTable.FullName, pxCache, uiField.GetFieldSourceType(pxCache));
    }
    PXDBDateAndTimeAttribute dateTime = pxCache.GetAttributes(fieldName).OfType<PXDBDateAndTimeAttribute>().FirstOrDefault<PXDBDateAndTimeAttribute>();
    if (dateTime == null)
      return;
    if (ResourceCollectingManager.IsStringCollecting)
    {
      PXPageRipper.RipDateAndTime(dateTime, pxCache, resourceSettings);
    }
    else
    {
      if (!TranslationValidationManager.ValidateCurrentLocale())
        return;
      PXLocalizerRepository.DateTimeLocalizer.ValidateTranslation(dateTime, pxCache);
    }
  }

  public static void RipDateAndTime(
    PXDBDateAndTimeAttribute dateTime,
    PXCache dateCache,
    CollectResourceSettings resourceSettings)
  {
    PXControlPropertiesCollector collector = PXPageRipper.Collector;
    if (dateTime == null || dateCache == null || collector == null)
      return;
    switch (PXPageRipper.GetFieldSourceType(dateCache, dateTime.FieldName, true))
    {
      case FieldSourceType.DacField:
      case FieldSourceType.Undefined:
        PXPageRipper.ReadDateAndTime(dateTime, collector, resourceSettings, (IDateTimeLocalizationKeyGenerator) new DateTimeDacKeyGenerator(dateCache));
        break;
      case FieldSourceType.DacFieldCacheAttached:
        PXPageRipper.ReadDateAndTime(dateTime, collector, resourceSettings, (IDateTimeLocalizationKeyGenerator) new DateTimeCacheAttachedKeyGenerator(dateCache));
        break;
      case FieldSourceType.CacheExtensionField:
        System.Type extentionWithProperty = PXPageRipper.GetExtentionWithProperty(dateCache.GetExtensionTypes(), dateCache.GetItemType(), dateTime.FieldName);
        PXPageRipper.ReadDateAndTime(dateTime, collector, resourceSettings, (IDateTimeLocalizationKeyGenerator) new DateTimeExtensionKeyGenerator(extentionWithProperty));
        break;
    }
  }

  private static void ReadDateAndTime(
    PXDBDateAndTimeAttribute dateTime,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings,
    IDateTimeLocalizationKeyGenerator keyGenerator)
  {
    if (!string.IsNullOrEmpty(dateTime.NeutralDisplayNameDate))
    {
      LocalizationResourceLite resource = new LocalizationResourceLite(keyGenerator.DateKey, LocalizationResourceType.DisplayName, dateTime.NeutralDisplayNameDate);
      collector.Collect(resource, resourceSettings);
    }
    if (string.IsNullOrEmpty(dateTime.NeutralDisplayNameTime))
      return;
    LocalizationResourceLite resource1 = new LocalizationResourceLite(keyGenerator.TimeKey, LocalizationResourceType.DisplayName, dateTime.NeutralDisplayNameTime);
    collector.Collect(resource1, resourceSettings);
  }

  public static void RipList(
    string fieldName,
    PXCache listCache,
    string[] allowedLabels,
    CollectResourceSettings resourceSettings)
  {
    PXControlPropertiesCollector collector = PXPageRipper.Collector;
    if (string.IsNullOrEmpty(fieldName) || listCache == null || allowedLabels == null || collector == null)
      return;
    string neutralDisplayName = PXUIFieldAttribute.GetNeutralDisplayName(listCache, fieldName);
    LocalizationResourceType resourceType = listCache.GetFieldType(fieldName) == typeof (int?) ? LocalizationResourceType.IntListItem : LocalizationResourceType.StringListItem;
    switch (PXPageRipper.GetFieldSourceType(listCache, fieldName, true))
    {
      case FieldSourceType.DacField:
        PXPageRipper.ReadDacFieldList((IEnumerable<string>) allowedLabels, neutralDisplayName, listCache.BqlTable.FullName, collector, resourceSettings, resourceType);
        break;
      case FieldSourceType.DacFieldCacheAttached:
        PXPageRipper.ReadDacFieldCacheAttachedList((IEnumerable<string>) allowedLabels, neutralDisplayName, listCache.BqlTable.FullName, listCache.Graph.GetType().FullName, collector, resourceSettings, resourceType);
        break;
      case FieldSourceType.CacheExtensionField:
        PXPageRipper.ReadCacheExtensionFieldList((IEnumerable<string>) allowedLabels, neutralDisplayName, listCache.GetItemType(), listCache.GetExtensionTypes(), fieldName, collector, resourceSettings, resourceType);
        break;
    }
  }

  public static void RipWorkflow(PXGraph graph)
  {
    IAUWorkflowEngine auWorkflowEngine = (IAUWorkflowEngine) null;
    IAUWorkflowFormsEngine workflowFormsEngine = (IAUWorkflowFormsEngine) null;
    if (ServiceLocator.IsLocationProviderSet)
    {
      auWorkflowEngine = ServiceLocator.Current.GetInstance<IAUWorkflowEngine>();
      workflowFormsEngine = ServiceLocator.Current.GetInstance<IAUWorkflowFormsEngine>();
    }
    if (string.IsNullOrEmpty(LocalizationResourceLite.CurrentScreenID) || auWorkflowEngine?.GetScreenWorkflows(LocalizationResourceLite.CurrentScreenID) == null)
      return;
    PXControlPropertiesCollector collector = PXPageRipper.Collector;
    foreach (AUWorkflowForm auWorkflowForm in PXSystemWorkflows.SelectTable<AUWorkflowForm>().ToList<AUWorkflowForm>().Where<AUWorkflowForm>((Func<AUWorkflowForm, bool>) (f => f.Screen == LocalizationResourceLite.CurrentScreenID)))
    {
      FormDefinition formDefinition = workflowFormsEngine.GetFormDefinition(auWorkflowForm.Screen, auWorkflowForm.FormName);
      PXControllRipper.RipControl(formDefinition.Form.DisplayName, HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath, formDefinition.FormName, "FormName", CollectResourceSettings.Resource | CollectResourceSettings.ResourceByScreen);
      foreach (AUWorkflowFormField screenField in workflowFormsEngine.GetScreenFields(auWorkflowForm.Screen))
      {
        if (!string.IsNullOrEmpty(screenField.DisplayName))
        {
          string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(screenField.SchemaField);
          collector.Collect(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.DisplayName, screenField.DisplayName), CollectResourceSettings.Resource | CollectResourceSettings.ResourceByScreen);
        }
      }
    }
    foreach (AUScreenFieldState screenFieldState in PXSystemWorkflows.SelectTable<AUScreenFieldState>().ToList<AUScreenFieldState>().Where<AUScreenFieldState>((Func<AUScreenFieldState, bool>) (f => f.ScreenID == LocalizationResourceLite.CurrentScreenID)))
    {
      if (!string.IsNullOrEmpty(screenFieldState.DisplayName))
      {
        string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(screenFieldState.TableName);
        collector.Collect(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.DisplayName, screenFieldState.DisplayName), CollectResourceSettings.Resource | CollectResourceSettings.ResourceByScreen);
      }
    }
    foreach (AUWorkflowHandler auWorkflowHandler in PXSystemWorkflows.SelectTable<AUWorkflowHandler>().ToList<AUWorkflowHandler>().Where<AUWorkflowHandler>((Func<AUWorkflowHandler, bool>) (f => f.ScreenID == LocalizationResourceLite.CurrentScreenID)))
    {
      if (!string.IsNullOrEmpty(auWorkflowHandler.DisplayName))
      {
        string localizationKey = AutomationKeyGenerator.GetLocalizationKey(graph.GetType().FullName);
        collector.Collect(new LocalizationResourceLite(localizationKey, LocalizationResourceType.ActionDisplayName, auWorkflowHandler.DisplayName), CollectResourceSettings.Resource | CollectResourceSettings.ResourceByScreen);
      }
    }
  }

  public static void RipDescription(string message, PXCache messageCache)
  {
    if (string.IsNullOrEmpty(message))
      return;
    PXPageRipper.Collector.Collect(new LocalizationResourceLite(PXSpecialKeyGenerator.GetMessageLocalizationKey(messageCache.GetItemType().FullName), LocalizationResourceType.DescriptionDisplayName, message), CollectResourceSettings.Resource);
  }

  private static void ReadCacheExtensionFieldList(
    IEnumerable<string> allowedLabels,
    string neutralDisplayName,
    System.Type classType,
    System.Type[] extentionTypes,
    string fieldName,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings,
    LocalizationResourceType resourceType)
  {
    System.Type extentionWithProperty = PXPageRipper.GetExtentionWithProperty(extentionTypes, classType, fieldName);
    if (!(extentionWithProperty != (System.Type) null))
      return;
    string listLocalizationKey = PXListKeyGenerator.GetCacheExtensionListLocalizationKey(extentionWithProperty.FullName);
    foreach (string allowedLabel in allowedLabels)
    {
      string localizationValue = PXListKeyGenerator.GetLocalizationValue(neutralDisplayName, allowedLabel);
      collector.Collect(new LocalizationResourceLite(listLocalizationKey, resourceType, localizationValue), resourceSettings);
    }
  }

  private static void ReadDacFieldList(
    IEnumerable<string> allowedLabels,
    string neutralDisplayName,
    string bqlTableFullName,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings,
    LocalizationResourceType resourceType)
  {
    string listLocalizationKey = PXListKeyGenerator.GetDacListLocalizationKey(bqlTableFullName);
    foreach (string allowedLabel in allowedLabels)
    {
      string localizationValue = PXListKeyGenerator.GetLocalizationValue(neutralDisplayName, allowedLabel);
      collector.Collect(new LocalizationResourceLite(listLocalizationKey, resourceType, localizationValue), resourceSettings);
    }
  }

  private static void ReadDacFieldCacheAttachedList(
    IEnumerable<string> allowedLabels,
    string neutralDisplayName,
    string bqlTableFullName,
    string graphFullName,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings,
    LocalizationResourceType resourceType)
  {
    string attachedLocalizationKey = PXListKeyGenerator.GetDacListCacheAttachedLocalizationKey(bqlTableFullName, graphFullName);
    foreach (string allowedLabel in allowedLabels)
    {
      string localizationValue = PXListKeyGenerator.GetLocalizationValue(neutralDisplayName, allowedLabel);
      collector.Collect(new LocalizationResourceLite(attachedLocalizationKey, resourceType, localizationValue), resourceSettings);
    }
  }

  public static void RipUIField(
    PXUIFieldAttribute uiField,
    PXCache fieldCache,
    UIFieldSourceType fieldSourceType,
    CollectResourceSettings resourceSettings)
  {
    PXControlPropertiesCollector collector = PXPageRipper.Collector;
    if (uiField == null || fieldCache == null || collector == null)
      return;
    switch (fieldSourceType)
    {
      case UIFieldSourceType.ActionName:
        PXPageRipper.ReadActionName(uiField.NeutralDisplayName, fieldCache.Graph.GetType().FullName, collector, resourceSettings);
        break;
      case UIFieldSourceType.AutomationButtonName:
        PXPageRipper.ReadAutomationButton(uiField.NeutralDisplayName, fieldCache.Graph, collector, resourceSettings);
        break;
      case UIFieldSourceType.DacFieldName:
        PXPageRipper.ReadDacFieldName(uiField.NeutralDisplayName, fieldCache.GetItemType(), fieldCache.BqlTable, collector, resourceSettings);
        break;
      case UIFieldSourceType.ParentDacFieldName:
        if (!(fieldCache.GetItemType().BaseType != (System.Type) null))
          break;
        PXPageRipper.ReadDacFieldName(uiField.NeutralDisplayName, fieldCache.GetItemType().BaseType, fieldCache.BqlTable, collector, resourceSettings);
        break;
      case UIFieldSourceType.CacheExtensionFieldName:
        PXPageRipper.ReadCacheExtensionFieldName(uiField.NeutralDisplayName, fieldCache.GetItemType(), fieldCache.GetExtensionTypes(), uiField.FieldName, collector, resourceSettings);
        break;
    }
  }

  private static void ReadDacFieldName(
    string neutralDisplayName,
    System.Type classType,
    System.Type bqlTable,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings)
  {
    if (string.IsNullOrEmpty(neutralDisplayName) || !(classType != (System.Type) null) || !(bqlTable != (System.Type) null) || collector == null)
      return;
    LocalizationResourceType resourceType = classType == bqlTable ? LocalizationResourceType.DisplayName : LocalizationResourceType.VirtualTableDisplayName;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(classType.FullName);
    collector.Collect(new LocalizationResourceLite(nameLocalizationKey, resourceType, neutralDisplayName), resourceSettings);
  }

  private static void ReadCacheExtensionFieldName(
    string neutralDisplayName,
    System.Type classType,
    System.Type[] extentionTypes,
    string fieldName,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings)
  {
    if (string.IsNullOrEmpty(neutralDisplayName) || extentionTypes == null || string.IsNullOrEmpty(fieldName) || collector == null)
      return;
    System.Type extentionWithProperty = PXPageRipper.GetExtentionWithProperty(extentionTypes, classType, fieldName);
    if (!(extentionWithProperty != (System.Type) null))
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetCacheExtensionNameLocalizationKey(extentionWithProperty.FullName);
    collector.Collect(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.VirtualTableDisplayName, neutralDisplayName), resourceSettings);
  }

  private static void ReadActionName(
    string neutralDisplayName,
    string fieldGraphFullName,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings)
  {
    if (string.IsNullOrEmpty(neutralDisplayName) || string.IsNullOrEmpty(fieldGraphFullName) || collector == null)
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetActionNameLocalizationKey(fieldGraphFullName);
    collector.Collect(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.ActionDisplayName, neutralDisplayName), resourceSettings);
  }

  private static void ReadAutomationButton(
    string neutralDisplayName,
    PXGraph fieldGraph,
    PXControlPropertiesCollector collector,
    CollectResourceSettings resourceSettings)
  {
    if (string.IsNullOrEmpty(neutralDisplayName) || fieldGraph == null)
      return;
    string[] array = fieldGraph.Actions.Keys.ToArray<string>();
    System.Type type = fieldGraph.GetType();
    string str = neutralDisplayName;
    PXCollationComparer collationComparer = PXLocalesProvider.CollationComparer;
    if (((IEnumerable<string>) array).Contains<string>(str, (IEqualityComparer<string>) collationComparer) || PXPageRipper.CustomizedGraphContainsAction(type, neutralDisplayName))
      return;
    string nameLocalizationKey = PXUIFieldKeyGenerator.GetAutomationButtonNameLocalizationKey(type.FullName);
    collector.Collect(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.ActionDisplayName, neutralDisplayName), resourceSettings);
  }

  private static bool CustomizedGraphContainsAction(System.Type graphType, string actionName)
  {
    string customizedTypeFullName = CustomizedTypeManager.GetCustomizedTypeFullName(graphType);
    if (customizedTypeFullName != graphType.FullName)
    {
      System.Type type = PXBuildManager.GetType(customizedTypeFullName, false);
      if (type != (System.Type) null && ((IEnumerable<string>) ServiceManager.GetActions(type)).Contains<string>(actionName, (IEqualityComparer<string>) PXLocalesProvider.CollationComparer))
        return true;
    }
    return false;
  }

  [Obsolete("This method is obsolete. Use PX.Data.PXCacheEx.GetDacExtensionsContainingDacField method instead.")]
  public static System.Type GetExtentionWithProperty(
    System.Type[] extentionTypes,
    System.Type senderType,
    string fieldName)
  {
    return PXCacheEx.GetDacExtensionsContainingDacField(extentionTypes, senderType, fieldName).FirstOrDefault<System.Type>();
  }

  public static FieldSourceType GetFieldSourceType(
    PXCache fieldCache,
    string fieldName,
    bool declaredOnlyField)
  {
    FieldSourceType fieldSourceType = FieldSourceType.Undefined;
    if (fieldCache != null && !string.IsNullOrEmpty(fieldName))
    {
      if (PXPageRipper.IsDacField(fieldCache.GetItemType(), fieldName, declaredOnlyField))
        fieldSourceType = FieldSourceType.DacField;
      else if (PXPageRipper.IsCacheExtensionField(fieldCache.GetItemType(), fieldName, fieldCache.GetExtensionTypes()))
        fieldSourceType = FieldSourceType.CacheExtensionField;
      else if (PXPageRipper.IsDacFieldCacheAttached(fieldCache.GetItemType(), fieldName, fieldCache.Graph.GetType()))
        fieldSourceType = FieldSourceType.DacFieldCacheAttached;
    }
    return fieldSourceType;
  }

  private static bool IsDacFieldCacheAttached(System.Type cacheType, string fieldName, System.Type graphType)
  {
    string cacheAttachedMethod = $"{cacheType.Name}_{fieldName}_{"CacheAttached"}";
    return ((IEnumerable<MethodInfo>) graphType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).Any<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name.Equals(cacheAttachedMethod, StringComparison.OrdinalIgnoreCase)));
  }

  private static bool IsCacheExtensionField(
    System.Type cacheType,
    string fieldName,
    System.Type[] cacheExtensionTypes)
  {
    return PXPageRipper.GetExtentionWithProperty(cacheExtensionTypes, cacheType, fieldName) != (System.Type) null;
  }

  private static bool IsDacField(System.Type cacheType, string fieldName, bool declaredOnlyField)
  {
    BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
    if (declaredOnlyField)
      bindingAttr |= BindingFlags.DeclaredOnly;
    return cacheType.GetProperty(fieldName, bindingAttr) != (PropertyInfo) null;
  }

  private string GetRelatedCustomizedUrl(System.IO.FileInfo file)
  {
    string virtualPath = "~/" + Path.Combine("CstPublished", Path.Combine(Path.GetDirectoryName(file.FullName.Substring(this.directory.FullName.Length).ToLower()).Replace('\\', '_'), Path.GetFileNameWithoutExtension(file.Name).ToUpper() + file.Extension)).Replace('\\', '/');
    return !File.Exists(HostingEnvironment.MapPath(virtualPath)) ? (string) null : virtualPath;
  }

  protected virtual PXControlPropertiesCollector CreatePropsCollector(ResourceCollection result)
  {
    return new PXControlPropertiesCollector()
    {
      result = result
    };
  }

  private HttpContext CreateDummyContext(string url)
  {
    DummyRequest dummyRequest = new DummyRequest(url.Substring(1));
    HttpContext httpContext1 = (HttpContext) typeof (HttpContext).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[2]
    {
      typeof (HttpWorkerRequest),
      typeof (bool)
    }, (ParameterModifier[]) null).Invoke(new object[2]
    {
      (object) dummyRequest,
      (object) false
    });
    HttpContext current = HttpContext.Current;
    HttpContext.Current = httpContext1;
    IHttpAsyncHandler httpAsyncHandler1 = (IHttpAsyncHandler) PXBuildManager.GetType("System.Web.HttpApplicationFactory", true).GetMethod("GetApplicationInstance", BindingFlags.Static | BindingFlags.NonPublic).Invoke((object) null, new object[1]
    {
      (object) httpContext1
    });
    PropertyInfo property = typeof (HttpContext).GetProperty("AsyncAppHandler", BindingFlags.Instance | BindingFlags.NonPublic);
    PXSessionStateStore.AddHttpSessionStateToContext(httpContext1, httpContext1.CreateSessionId(), 10);
    httpContext1.Handler = (IHttpHandler) BuildManager.CreateInstanceFromVirtualPath(url, typeof (Page));
    HttpContext httpContext2 = httpContext1;
    IHttpAsyncHandler httpAsyncHandler2 = httpAsyncHandler1;
    object[] index = new object[0];
    property.SetValue((object) httpContext2, (object) httpAsyncHandler2, index);
    httpContext1.ApplicationInstance = (HttpApplication) httpAsyncHandler1;
    httpContext1.User = (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity("admin"), PXAccess.GetAdministratorRoles());
    typeof (HttpResponse).GetMethod("InitResponseWriter", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((object) httpContext1.Response, new object[0]);
    return current;
  }

  private void AddRequestHeader(HttpRequest request, string name, string value)
  {
    System.Type type = request.Headers.GetType();
    type.InvokeMember("MakeReadWrite", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) request.Headers, (object[]) null);
    type.InvokeMember("InvalidateCachedArrays", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) request.Headers, (object[]) null);
    type.InvokeMember("BaseAdd", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) request.Headers, new object[2]
    {
      (object) name,
      (object) value
    });
    type.InvokeMember("MakeReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) request.Headers, (object[]) null);
  }
}
