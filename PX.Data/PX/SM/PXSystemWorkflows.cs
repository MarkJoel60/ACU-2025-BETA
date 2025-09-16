// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSystemWorkflows
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Soap.Screen;
using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.WorkflowAPI;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace PX.SM;

internal static class PXSystemWorkflows
{
  private static Lazy<System.Type[]> _dependentType = new Lazy<System.Type[]>(new Func<System.Type[]>(PXSystemWorkflows.GetWorkflowDependentTypesInternal));
  private static string SLOT_KEY = "PXSystemWorkflows.SystemWorkflow";
  private static string DB_SLOT_KEY = "PXSystemWorkflows.DBSystemWorkflow";
  public static string AnyWorkflowID = "*";
  public static string AnyStateID = "*";
  private static bool _isSubscribed;

  public static PXSystemWorkflows.SystemWorkflow Definition
  {
    get
    {
      PXSystemWorkflows.SystemWorkflow slot = PXContext.GetSlot<PXSystemWorkflows.SystemWorkflow>(PXSystemWorkflows.SLOT_KEY);
      if (slot == null)
      {
        slot = PXDatabase.GetSlot<PXSystemWorkflows.SystemWorkflow>(PXSystemWorkflows.DB_SLOT_KEY, PXSystemWorkflows.GetWorkflowDependedTypes());
        PXContext.SetSlot<PXSystemWorkflows.SystemWorkflow>(PXSystemWorkflows.SLOT_KEY, slot);
      }
      return slot;
    }
  }

  static PXSystemWorkflows()
  {
    PXCodeDirectoryCompiler.NotifyOnChange(new System.Action(PXSystemWorkflows.ResetCacheForAllCompanies));
  }

  private static void ResetCacheForAllCompanies()
  {
    PXDatabase.ResetSlotForAllCompanies(PXSystemWorkflows.DB_SLOT_KEY);
  }

  private static void ResetCache()
  {
    PXDatabase.ResetSlot<PXSystemWorkflows.SystemWorkflow>(PXSystemWorkflows.DB_SLOT_KEY, PXSystemWorkflows.GetWorkflowDependedTypes());
  }

  internal static System.Type[] GetWorkflowDependedTypes()
  {
    return PXSystemWorkflows._dependentType.Value;
  }

  public static Exception CheckErrors(PXGraph g)
  {
    System.Type typeNotCustomized = CustomizedTypeManager.GetTypeNotCustomized(g.GetType());
    Exception exception;
    return PXSystemWorkflows.Definition != null && PXSystemWorkflows.Definition.SystemWorkflowContainer.InitializationExceptions.TryGetValue(typeNotCustomized, out exception) ? exception : (Exception) null;
  }

  public static string ResolveFieldName(PXCache c, string f, bool skipExistenceCheck = false)
  {
    if (f == null)
      return (string) null;
    string fieldName = c.GetFieldName(f, false);
    if (fieldName != null)
      return fieldName;
    if (!skipExistenceCheck)
      throw new PXException("Can not resolve " + f);
    return f;
  }

  private static Dictionary<System.Type, List<System.Type>> GetWorkflowExtensions()
  {
    Dictionary<System.Type, List<System.Type>> workflowExtensions = new Dictionary<System.Type, List<System.Type>>();
    foreach (KeyValuePair<System.Type, System.Type> allExtension in PXExtensionManager.GetAllExtensions())
    {
      System.Type key1 = allExtension.Key;
      System.Type key2 = allExtension.Value;
      if (!key2.IsAbstract)
      {
        if (!(key1.GetMethod("Configure", new System.Type[1]
        {
          typeof (PXScreenConfiguration)
        }).DeclaringType == typeof (PXGraphExtension)))
        {
          List<System.Type> typeList;
          if (workflowExtensions.TryGetValue(key2, out typeList))
            typeList.Add(key1);
          else
            workflowExtensions.Add(key2, new List<System.Type>()
            {
              key1
            });
        }
      }
    }
    foreach (System.Type key in workflowExtensions.Keys.ToArray<System.Type>())
      workflowExtensions[key] = PXExtensionManager.Sort(workflowExtensions[key]);
    return workflowExtensions;
  }

  private static PXSystemWorkflowContainer InitSystemWorkflows()
  {
    PXSystemWorkflowContainer workflowContainer = new PXSystemWorkflowContainer();
    Dictionary<System.Type, List<System.Type>> workflowExtensions = PXSystemWorkflows.GetWorkflowExtensions();
    foreach (ServiceManager.TypeInfo graph in ServiceManager.GraphList)
    {
      System.Type type = graph.Type;
      if (!(type == (System.Type) null))
      {
        try
        {
          PXScreenConfiguration context = new PXScreenConfiguration();
          PXSystemWorkflows.ProcessGraphType(type, workflowExtensions, context);
          Readonly.ScreenConfiguration result = context.Result;
          if (result != null)
            PXSystemWorkflows.Publish(workflowContainer, type, result);
        }
        catch (Exception ex)
        {
          PXTrace.Logger.ForSystemEvents("System", "System_WorkflowFailedToInitialize").ForCurrentCompanyContext().Error<System.Type>(ex, "The system has failed to initialize the workflow. GraphType:{GraphType}", type);
          if (WebConfig.EnableWorkflowValidationOnStartup)
            throw;
          workflowContainer.InitializationExceptions.Add(type, ex);
        }
      }
    }
    PXSystemWorkflows.ValidateWorkflowDefinitions(workflowContainer);
    workflowContainer.ClearServiceGraphs();
    if (!PXSystemWorkflows._isSubscribed)
    {
      PXSystemWorkflows._isSubscribed = true;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      PXDatabase.Subscribe(PXSystemWorkflows.\u003C\u003EO.\u003C0\u003E__ResetCache ?? (PXSystemWorkflows.\u003C\u003EO.\u003C0\u003E__ResetCache = new PXDatabaseTableChanged(PXSystemWorkflows.ResetCache)), (IEnumerable<System.Type>) PXSystemWorkflows.GetWorkflowDependedTypes());
    }
    return workflowContainer;
  }

  private static void ProcessGraphType(
    System.Type graphType,
    Dictionary<System.Type, List<System.Type>> extensionsList,
    PXScreenConfiguration context)
  {
    if (graphType.GetCustomAttribute<PXWorkflowInheritanceAttribute>(false) != null && graphType.BaseType != (System.Type) null && graphType.BaseType != typeof (PXGraph))
      PXSystemWorkflows.ProcessGraphType(graphType.BaseType, extensionsList, context);
    List<System.Type> typeList = new List<System.Type>();
    MethodInfo method = graphType.GetMethod("Configure", new System.Type[1]
    {
      typeof (PXScreenConfiguration)
    });
    if (!graphType.IsAbstract && method.DeclaringType != typeof (PXGraph) && method.DeclaringType == graphType)
      typeList.Add(graphType);
    List<System.Type> collection;
    if (extensionsList.TryGetValue(graphType, out collection))
      typeList.AddRange((IEnumerable<System.Type>) collection);
    if (typeList.Count == 0)
      return;
    foreach (System.Type type in typeList)
    {
      object uninitializedObject = FormatterServices.GetUninitializedObject(type);
      if (uninitializedObject is PXGraph pxGraph)
        pxGraph.Configure(context);
      else if (uninitializedObject is PXGraphExtension pxGraphExtension)
        pxGraphExtension.Configure(context);
    }
  }

  private static void ValidateWorkflowDefinitions(PXSystemWorkflowContainer result)
  {
    List<AUWorkflowForm> allForms = result.GetItems<AUWorkflowForm>();
    List<AUScreenActionState> allActions = result.GetItems<AUScreenActionState>();
    List<AUWorkflowTransition> allTransitions = result.GetItems<AUWorkflowTransition>();
    foreach ((AUWorkflowFormField workflowFormField, AUScreenActionState screenActionState) in result.GetItems<AUWorkflowFormField>().Where<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (field => field.ComboBoxValuesSource == "T")).Select<AUWorkflowFormField, (AUWorkflowFormField, AUWorkflowForm)>((Func<AUWorkflowFormField, (AUWorkflowFormField, AUWorkflowForm)>) (field => (field, allForms.SingleOrDefault<AUWorkflowForm>((Func<AUWorkflowForm, bool>) (form => string.Equals(form.FormName, field.FormName, StringComparison.OrdinalIgnoreCase) && string.Equals(form.Screen, field.Screen, StringComparison.OrdinalIgnoreCase)))))).SelectMany<(AUWorkflowFormField, AUWorkflowForm), (AUWorkflowFormField, AUScreenActionState)>((Func<(AUWorkflowFormField, AUWorkflowForm), IEnumerable<(AUWorkflowFormField, AUScreenActionState)>>) (fieldAndForm => allActions.Where<AUScreenActionState>((Func<AUScreenActionState, bool>) (action => string.Equals(action.ScreenID, fieldAndForm.Item2.Screen, StringComparison.OrdinalIgnoreCase) && string.Equals(action.Form, fieldAndForm.Item2.FormName, StringComparison.OrdinalIgnoreCase))).Select<AUScreenActionState, (AUWorkflowFormField, AUScreenActionState)>((Func<AUScreenActionState, (AUWorkflowFormField, AUScreenActionState)>) (action => (fieldAndForm.field, action))))).Where<(AUWorkflowFormField, AUScreenActionState)>((Func<(AUWorkflowFormField, AUScreenActionState), bool>) (fieldAndAction => EnumerableExtensions.Distinct<AUWorkflowTransition, string>(allTransitions.Where<AUWorkflowTransition>((Func<AUWorkflowTransition, bool>) (tran => string.Equals(tran.ScreenID, fieldAndAction.action.ScreenID, StringComparison.OrdinalIgnoreCase) && string.Equals(tran.ActionName, fieldAndAction.action.ActionName, StringComparison.OrdinalIgnoreCase))), (Func<AUWorkflowTransition, string>) (tran => tran.TargetStateName)).Count<AUWorkflowTransition>() > 1)))
      PXTrace.Logger.ForSystemEvents("System", "System_WorkflowWillProbablyFail").ForCurrentCompanyContext().Warning<string, string, string>("The {ActionName} action of the {Screen} form is used in multiple transitions that have different target states so the system cannot determinate the target state in advance, and, therefore, the list of combo box values for the {SchemaField} field. You need to specify the list of combo box values without using a target state or use the action only in transitions with the same target state.", screenActionState.ActionName, workflowFormField.Screen, workflowFormField.SchemaField);
  }

  private static System.Type[] GetWorkflowDependentTypesInternal()
  {
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (PXGraph.FeaturesSet),
      typeof (AUScreenExtraAction)
    };
    List<System.Type> source = new List<System.Type>();
    foreach (ServiceManager.TypeInfo graph in ServiceManager.GraphList)
    {
      System.Type graphType = graph.Type;
      if (!(graphType == (System.Type) null))
      {
        try
        {
          List<System.Type> list = PXExtensionManager.GetExtensions(graphType, false).Where<System.Type>((Func<System.Type, bool>) (extType => typeof (PXGraphExtension).IsAssignableFrom(extType) && ((IEnumerable<System.Type>) extType.BaseType.GetInheritanceChain().First<System.Type>((Func<System.Type, bool>) (t => t.IsGenericType && t.BaseType == typeof (PXGraphExtension))).GetGenericArguments()).Single<System.Type>() == graphType)).ToList<System.Type>();
          list.Insert(0, graphType);
          if (!list.All<System.Type>((Func<System.Type, bool>) (t =>
          {
            MethodInfo method = t.GetMethod("Configure", new System.Type[1]
            {
              typeof (PXScreenConfiguration)
            });
            return method.DeclaringType == typeof (PXGraph) || method.DeclaringType == typeof (PXGraphExtension);
          })))
          {
            foreach (System.Type c in list)
            {
              if (!c.IsAbstract)
              {
                if (typeof (PXGraph).IsAssignableFrom(c))
                {
                  PXWorkflowDependsOnTypeAttribute customAttribute = c.GetMethod("Configure", new System.Type[1]
                  {
                    typeof (PXScreenConfiguration)
                  }).GetCustomAttribute<PXWorkflowDependsOnTypeAttribute>(false);
                  if (customAttribute != null)
                    source.AddRange((IEnumerable<System.Type>) customAttribute.Types);
                }
                else if (typeof (PXGraphExtension).IsAssignableFrom(c))
                {
                  PXWorkflowDependsOnTypeAttribute customAttribute = c.GetMethod("Configure", new System.Type[1]
                  {
                    typeof (PXScreenConfiguration)
                  }).GetCustomAttribute<PXWorkflowDependsOnTypeAttribute>(false);
                  if (customAttribute != null)
                    source.AddRange((IEnumerable<System.Type>) customAttribute.Types);
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          if (WebConfig.EnableWorkflowValidationOnStartup)
            throw;
        }
      }
    }
    typeList.AddRange(source.Distinct<System.Type>());
    return typeList.ToArray();
  }

  public static Guid? GuidFromWorkflowId(string workflowId, string workflowSubId)
  {
    return PXSystemWorkflows.GuidFromString(string.IsNullOrEmpty(workflowSubId) || workflowSubId == "DEFAULT" ? workflowId ?? "DEF_WORKFLOW_ID" : $"{workflowId ?? "DEF_WORKFLOW_ID"}__{workflowSubId}");
  }

  public static Guid? GuidFromString(string input)
  {
    if (input == null)
      return new Guid?();
    if (string.IsNullOrEmpty(input))
      return new Guid?(Guid.Empty);
    try
    {
      using (MD5 md5 = MD5.Create())
        return new Guid?(new Guid(md5.ComputeHash(Encoding.Default.GetBytes(input))));
    }
    catch (Exception ex)
    {
      switch (ex)
      {
        case TargetInvocationException _:
        case InvalidOperationException _:
          return new Guid?(PXSystemWorkflows.ToGuid(PXReflectionSerializer.GetStableHash(input)));
        default:
          throw;
      }
    }
  }

  private static Guid ToGuid(int value)
  {
    byte[] b = new byte[16 /*0x10*/];
    BitConverter.GetBytes(value).CopyTo((Array) b, 0);
    return new Guid(b);
  }

  private static void Publish(
    PXSystemWorkflowContainer dest,
    System.Type graphType,
    Readonly.ScreenConfiguration screenConfiguration)
  {
    IList<string> screensIdFromGraphType = PXPageIndexingService.GetScreensIDFromGraphType(graphType);
    if (screensIdFromGraphType == null)
      return;
    foreach (string screenID in (IEnumerable<string>) screensIdFromGraphType)
    {
      if (screenConfiguration.StateIdentifier != null)
      {
        AUWorkflowDefinition workflowDefinition = new AUWorkflowDefinition()
        {
          ScreenID = screenID
        };
        screenConfiguration.CopyTo(workflowDefinition);
        dest.Insert<AUWorkflowDefinition>(workflowDefinition);
      }
      screenConfiguration.Publish(dest, screenID);
    }
  }

  public static IEnumerable<T> SelectTable<T>() where T : class, IBqlTable, new()
  {
    if (PXSystemWorkflows.Definition == null)
      return (IEnumerable<T>) null;
    PXGraph graph = new PXGraph();
    List<T> list1 = PXSelectBase<T, PXSelect<T>.Config>.Select(graph).FirstTableItems.ToList<T>();
    PXCaseInsensitiveDacComparer<T> comparer = new PXCaseInsensitiveDacComparer<T>();
    List<T> items = PXSystemWorkflows.Definition.SystemWorkflowContainer.GetItems<T>();
    List<T> list2 = list1.Intersect<T>((IEnumerable<T>) items, (IEqualityComparer<T>) comparer).ToList<T>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable<T>) Enumerable.ToArray<T>(((IEnumerable<T>) Enumerable.ToArray<T>(((IEnumerable<T>) Enumerable.ToArray<T>(((IEnumerable<T>) Enumerable.ToArray<T>(list1.Union<T>((IEnumerable<T>) items, (IEqualityComparer<T>) comparer))).Except<T>((IEnumerable<T>) list2, (IEqualityComparer<T>) comparer))).Union<T>(list2.Join<T, T, T, T>((IEnumerable<T>) items, (Func<T, T>) (table => table), (Func<T, T>) (table => table), (Func<T, T, T>) ((customItem, systemItem) => !(customItem is AUWorkflowBaseTable customizedItem) ? customItem : PXSystemWorkflows.Merge<AUWorkflowBaseTable>((object) systemItem as AUWorkflowBaseTable, customizedItem, graph) as T), (IEqualityComparer<T>) comparer)))).Where<T>(PXSystemWorkflows.\u003CSelectTable\u003EO__24_0<T>.\u003C0\u003E__IsActive ?? (PXSystemWorkflows.\u003CSelectTable\u003EO__24_0<T>.\u003C0\u003E__IsActive = new Func<T, bool>(PXSystemWorkflows.IsActive))));
  }

  public static IEnumerable<T> SelectTable<T>(
    string screenID,
    ref bool isCustomized,
    bool skipSystemWorkflowCache = false)
    where T : class, IScreenItem, new()
  {
    if (screenID == null)
      throw new ArgumentNullException(nameof (screenID));
    if (PXSystemWorkflows.Definition == null)
      return (IEnumerable<T>) null;
    PXGraph graph = new PXGraph();
    List<T> list1 = PXSelectBase<T, PXSelect<T>.Config>.Select(graph).FirstTableItems.Where<T>((Func<T, bool>) (x => string.Equals(x?.ScreenID, screenID, StringComparison.OrdinalIgnoreCase))).ToList<T>();
    isCustomized |= list1.Any<T>();
    PXCaseInsensitiveDacComparer<T> comparer = new PXCaseInsensitiveDacComparer<T>();
    List<T> itemsForScreen = (skipSystemWorkflowCache ? PXDatabase.GetSlot<PXSystemWorkflows.SystemWorkflow>(PXSystemWorkflows.DB_SLOT_KEY, PXSystemWorkflows.GetWorkflowDependedTypes()) : PXSystemWorkflows.Definition).SystemWorkflowContainer.GetItemsForScreen<T>(screenID);
    List<T> list2 = list1.Intersect<T>((IEnumerable<T>) itemsForScreen, (IEqualityComparer<T>) comparer).ToList<T>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return list1.Union<T>((IEnumerable<T>) itemsForScreen, (IEqualityComparer<T>) comparer).Except<T>((IEnumerable<T>) list2, (IEqualityComparer<T>) comparer).Union<T>(list2.Join<T, T, T, T>((IEnumerable<T>) itemsForScreen, (Func<T, T>) (table => table), (Func<T, T>) (table => table), (Func<T, T, T>) ((customItem, systemItem) => !(customItem is AUWorkflowBaseTable customizedItem) ? customItem : PXSystemWorkflows.Merge<AUWorkflowBaseTable>((object) systemItem as AUWorkflowBaseTable, customizedItem, graph) as T), (IEqualityComparer<T>) comparer)).Where<T>(PXSystemWorkflows.\u003CSelectTable\u003EO__25_0<T>.\u003C0\u003E__IsActive ?? (PXSystemWorkflows.\u003CSelectTable\u003EO__25_0<T>.\u003C0\u003E__IsActive = new Func<T, bool>(PXSystemWorkflows.IsActive)));
  }

  public static T Merge<T>(T systemItem, T customizedItem, PXGraph graph) where T : AUWorkflowBaseTable
  {
    if ((object) customizedItem == null)
      return systemItem;
    if ((object) systemItem == null)
      return customizedItem;
    if ((object) customizedItem == (object) systemItem)
      return systemItem;
    PXCache cache = graph.Caches[systemItem.GetType()];
    object copy = cache.CreateCopy((object) systemItem);
    ((T) copy).IsActive = customizedItem.IsActive;
    foreach (string fieldName in cache.Fields.Where<string>((Func<string, bool>) (it => !it.EndsWith("Customized") && !cache.Keys.Contains(it))))
    {
      if (cache.Fields.Contains(fieldName + "Customized"))
      {
        bool? nullable = (bool?) cache.GetValue((object) customizedItem, fieldName + "Customized");
        bool flag = true;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          cache.SetValue(copy, fieldName, cache.GetValue((object) customizedItem, fieldName));
      }
    }
    return (T) copy;
  }

  private static bool IsActive(object row)
  {
    if (!(row is IRemovable removable))
      return true;
    bool? isActive = removable.IsActive;
    bool flag = true;
    return isActive.GetValueOrDefault() == flag & isActive.HasValue;
  }

  public static AUWorkflowDefinition GetSystemWorkflowDefinition(string screenId)
  {
    PXSystemWorkflows.SystemWorkflow definition = PXSystemWorkflows.Definition;
    return definition == null ? (AUWorkflowDefinition) null : definition.SystemWorkflowContainer.GetItemsForScreen<AUWorkflowDefinition>(screenId).FirstOrDefault<AUWorkflowDefinition>();
  }

  public static string ResolveFieldName(System.Type table, string fieldName, bool skipExistenceCheck = false)
  {
    if (table == (System.Type) null)
      return (string) null;
    if (fieldName == null)
      return (string) null;
    if (!WebConfig.EnableWorkflowValidationOnStartup)
      return fieldName;
    try
    {
      PropertyInfo propertyInfo = table.GetInheritanceChain().Select<System.Type, PropertyInfo>((Func<System.Type, PropertyInfo>) (t => t.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public))).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (it => it != (PropertyInfo) null));
      return propertyInfo != (PropertyInfo) null ? propertyInfo.Name : PXSystemWorkflows.ResolveFieldName(new PXGraph().Caches[table], fieldName, skipExistenceCheck);
    }
    catch (PXException ex)
    {
      PXTrace.Logger.ForSystemEvents("System", "System_WorkflowFailedToInitialize").ForCurrentCompanyContext().Error<System.Type>((Exception) ex, "The system has failed to initialize the workflow. CacheType:{CacheType}", table);
      throw;
    }
    catch (Exception ex)
    {
      PXTrace.Logger.ForSystemEvents("System", "System_WorkflowCouldNotAccessCache").ForCurrentCompanyContext().Warning<System.Type>(ex, "The system could not access the cache when searching for information about the fields used in the workflow. CacheType:{CacheType}", table);
      return fieldName;
    }
  }

  public static IEnumerable<AUScreenConditionState> GetConditionsList(string screenID)
  {
    IEnumerable<AUScreenConditionState> systemConditions = PXSystemWorkflows.GetSystemConditionsList(screenID);
    PXSystemWorkflows.SystemWorkflow definition = PXSystemWorkflows.Definition;
    return definition == null ? (IEnumerable<AUScreenConditionState>) null : definition.SystemWorkflowContainer.GetItemsForScreen<AUScreenConditionState>(screenID).Where<AUScreenConditionState>((Func<AUScreenConditionState, bool>) (_ => !systemConditions.Any<AUScreenConditionState>((Func<AUScreenConditionState, bool>) (s => s.ConditionName == _.ConditionName)))).Concat<AUScreenConditionState>(systemConditions);
  }

  public static IEnumerable<AUScreenConditionState> GetSystemConditionsList(string screenID)
  {
    PXGraph graph = new PXGraph();
    PXSystemWorkflows.SystemWorkflow definition = PXSystemWorkflows.Definition;
    return definition == null ? (IEnumerable<AUScreenConditionState>) null : (IEnumerable<AUScreenConditionState>) definition.SystemWorkflowContainer.SystemConditions.Values.Where<WorkflowConditionEvaluator>((Func<WorkflowConditionEvaluator, bool>) (_ => IsMatchScreenID(_.Record))).Select<WorkflowConditionEvaluator, AUScreenConditionState>((Func<WorkflowConditionEvaluator, AUScreenConditionState>) (_ => new AUScreenConditionState()
    {
      InternalImplementation = (IWorkflowCondition) _,
      ScreenID = screenID,
      ConditionID = PXSystemWorkflows.GuidFromString(_.ConditionName),
      ConditionName = _.DisplayName,
      IsSystem = new bool?(true)
    })).ToList<AUScreenConditionState>();

    bool IsMatchScreenID(IBqlTable r)
    {
      return string.Equals(screenID, (string) graph.Caches[r.GetType()].GetValue((object) r, "ScreenID"), StringComparison.OrdinalIgnoreCase);
    }
  }

  public static void ApplyComboBoxModifications(
    PXCache cache,
    PXStringState stringState,
    Dictionary<string, ComboBoxItemsModification>.ValueCollection comboBoxModifications)
  {
    List<string> list1 = ((IEnumerable<string>) stringState.AllowedValues).ToList<string>();
    List<string> list2 = ((IEnumerable<string>) stringState.AllowedLabels).ToList<string>();
    int num = PXSystemWorkflows.ApplyComboBoxModifications(comboBoxModifications, list1, list2) ? 1 : 0;
    stringState.AllowedValues = list1.ToArray();
    stringState.AllowedLabels = list2.ToArray();
    if (num == 0 || ResourceCollectingManager.IsStringCollecting || stringState.AllowedLabels == null)
      return;
    string[] neutralAllowedLabels = (string[]) stringState.AllowedLabels.Clone();
    PXLocalizerRepository.ListLocalizer.Localize(stringState.Name, cache, neutralAllowedLabels, stringState.AllowedLabels);
  }

  public static bool ApplyComboBoxModifications(
    Dictionary<string, ComboBoxItemsModification>.ValueCollection comboBoxModifications,
    List<string> allowedValues,
    List<string> allowedLabels)
  {
    bool flag = false;
    foreach (ComboBoxItemsModification comboBoxModification in comboBoxModifications)
    {
      if (comboBoxModification.Action == ComboBoxItemsModificationAction.Set || comboBoxModification.Action == ComboBoxItemsModificationAction.SetFromUI)
      {
        int index = allowedValues.IndexOf(comboBoxModification.ID);
        if (index >= 0)
        {
          if (allowedLabels[index] != comboBoxModification.Description)
            flag = true;
          allowedLabels[index] = comboBoxModification.Description;
        }
        else
        {
          flag = true;
          allowedValues.Add(comboBoxModification.ID);
          allowedLabels.Add(comboBoxModification.Description);
        }
      }
      else if (comboBoxModification.Action == ComboBoxItemsModificationAction.Remove)
      {
        int index = allowedValues.IndexOf(comboBoxModification.ID);
        if (index >= 0)
        {
          allowedValues.RemoveAt(index);
          allowedLabels.RemoveAt(index);
        }
      }
    }
    return flag;
  }

  internal class SystemWorkflow : IPrefetchable, IPXCompanyDependent
  {
    public PXSystemWorkflowContainer SystemWorkflowContainer { get; private set; }

    public void Prefetch()
    {
      this.SystemWorkflowContainer = PXSystemWorkflows.InitSystemWorkflows();
      this.SystemWorkflowContainer.InsertRange<AUScreenActionState>((IList<AUScreenActionState>) ExtraActionsProvider.SelectActions().ToList<AUScreenActionState>());
      this.SystemWorkflowContainer.InsertRange<AUWorkflowForm>((IList<AUWorkflowForm>) ExtraActionsProvider.SelectActionForms().ToList<AUWorkflowForm>());
      this.SystemWorkflowContainer.InsertRange<AUWorkflowFormField>((IList<AUWorkflowFormField>) ExtraActionsProvider.SelectActionFormFields().ToList<AUWorkflowFormField>());
    }
  }
}
