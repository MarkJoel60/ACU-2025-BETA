// Decompiled with JetBrains decompiler
// Type: PX.Data.PXQuickProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

/// <summary>
/// Serves as a common entry point for all attributes
/// that are involved into Quick Process declarative configuration,
/// and also contains a <see cref="M:PX.Data.PXQuickProcess.Start``3(``0,``1,``2)" />
/// method from which Quick Process could be configured and started
/// </summary>
public static class PXQuickProcess
{
  /// <summary>
  /// Start a Quick Process execution, configured by the <paramref name="quickProcessConfig" />
  /// </summary>
  /// <typeparam name="TGraph">Entry point graph type</typeparam>
  /// <typeparam name="TTable">Entry point entity type</typeparam>
  /// <typeparam name="TConfig">Configuration entity type</typeparam>
  /// <param name="graph">The graph that requests a Quick Process execution</param>
  /// <param name="startEntity">The entity from which processing should be started</param>
  /// <param name="quickProcessConfig">The entity that should configure the Quick Process execution</param>
  public static void Start<TGraph, TTable, TConfig>(
    #nullable disable
    TGraph graph,
    TTable startEntity,
    TConfig quickProcessConfig)
    where TGraph : PXGraph, new()
    where TTable : class, IBqlTable, new()
    where TConfig : class, IBqlTable, new()
  {
    PXQuickProcess.StartAndGetExceptions<TGraph, TTable, TConfig>(graph, startEntity, quickProcessConfig);
  }

  /// <summary>
  /// Start a Quick Process execution, configured by the <paramref name="quickProcessConfig" />
  /// </summary>
  /// <typeparam name="TGraph">Entry point graph type</typeparam>
  /// <typeparam name="TTable">Entry point entity type</typeparam>
  /// <typeparam name="TConfig">Configuration entity type</typeparam>
  /// <param name="graph">The graph that requests a Quick Process execution</param>
  /// <param name="startEntity">The entity from which processing should be started</param>
  /// <param name="quickProcessConfig">The entity that should configure the Quick Process execution</param>
  public static IEnumerable<Exception> StartAndGetExceptions<TGraph, TTable, TConfig>(
    TGraph graph,
    TTable startEntity,
    TConfig quickProcessConfig)
    where TGraph : PXGraph, new()
    where TTable : class, IBqlTable, new()
    where TConfig : class, IBqlTable, new()
  {
    if ((object) graph == null)
      throw new ArgumentNullException(nameof (graph));
    if ((object) startEntity == null)
      throw new ArgumentNullException(nameof (startEntity));
    if ((object) quickProcessConfig == null)
      throw new ArgumentNullException(nameof (quickProcessConfig));
    PXCache qpCache = graph.Caches[typeof (TConfig)];
    \u003C\u003Ef__AnonymousType60<PXQuickProcess.StepInfo[], PXQuickProcess.Step.RelatedParameterAttribute[], PXQuickProcess.Step.IsStartPointAttribute[], PXQuickProcess.AutoRedirectOptionAttribute[], PXQuickProcess.AutoDownloadReportsOptionAttribute[]>[] array1 = qpCache.Fields.Select(fieldName => new
    {
      fieldName = fieldName,
      attributes = qpCache.GetAttributesReadonly(fieldName)
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      steps = _param1.attributes.OfType<PXQuickProcess.Step.IsBoundToAttribute>().Select<PXQuickProcess.Step.IsBoundToAttribute, PXQuickProcess.StepInfo>((Func<PXQuickProcess.Step.IsBoundToAttribute, PXQuickProcess.StepInfo>) (a => a.GetStepInfo())).ToArray<PXQuickProcess.StepInfo>()
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier1 = _param1,
      pars = _param1.\u003C\u003Eh__TransparentIdentifier0.attributes.OfType<PXQuickProcess.Step.RelatedParameterAttribute>().ToArray<PXQuickProcess.Step.RelatedParameterAttribute>()
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier2 = _param1,
      start = _param1.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.attributes.OfType<PXQuickProcess.Step.IsStartPointAttribute>().ToArray<PXQuickProcess.Step.IsStartPointAttribute>()
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier3 = _param1,
      autoRedirect = _param1.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.attributes.OfType<PXQuickProcess.AutoRedirectOptionAttribute>().ToArray<PXQuickProcess.AutoRedirectOptionAttribute>()
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier4 = _param1,
      autoDownload = _param1.\u003C\u003Eh__TransparentIdentifier3.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.\u003C\u003Eh__TransparentIdentifier0.attributes.OfType<PXQuickProcess.AutoDownloadReportsOptionAttribute>().ToArray<PXQuickProcess.AutoDownloadReportsOptionAttribute>()
    }).Select(_param1 => new
    {
      steps = _param1.\u003C\u003Eh__TransparentIdentifier4.\u003C\u003Eh__TransparentIdentifier3.\u003C\u003Eh__TransparentIdentifier2.\u003C\u003Eh__TransparentIdentifier1.steps,
      pars = _param1.\u003C\u003Eh__TransparentIdentifier4.\u003C\u003Eh__TransparentIdentifier3.\u003C\u003Eh__TransparentIdentifier2.pars,
      start = _param1.\u003C\u003Eh__TransparentIdentifier4.\u003C\u003Eh__TransparentIdentifier3.start,
      autoRedirect = _param1.\u003C\u003Eh__TransparentIdentifier4.autoRedirect,
      autoDownload = _param1.autoDownload
    }).ToArray();
    PXQuickProcess.StepInfo[] stepsArr = array1.SelectMany(s => (IEnumerable<PXQuickProcess.StepInfo>) s.steps).ToArray<PXQuickProcess.StepInfo>();
    if (stepsArr.Length == 0)
      throw new PXInvalidOperationException("An entity of {0} type cannot configure Quick Process, because it isn't marked with any Quick Process related attribute.", new object[1]
      {
        (object) typeof (TConfig)
      });
    ILookup<string, PXQuickProcess.Step.RelatedParameterAttribute> lookup = array1.SelectMany(p => (IEnumerable<PXQuickProcess.Step.RelatedParameterAttribute>) p.pars).ToLookup<PXQuickProcess.Step.RelatedParameterAttribute, string>((Func<PXQuickProcess.Step.RelatedParameterAttribute, string>) (p => p.StepField.Name), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    Dictionary<string, PXQuickProcess.Step.IsStartPointAttribute> dictionary = array1.SelectMany(p => (IEnumerable<PXQuickProcess.Step.IsStartPointAttribute>) p.start).ToDictionary<PXQuickProcess.Step.IsStartPointAttribute, string>((Func<PXQuickProcess.Step.IsStartPointAttribute, string>) (a => a.FieldName), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    PXQuickProcess.AutoRedirectOptionAttribute[] array2 = array1.SelectMany(p => (IEnumerable<PXQuickProcess.AutoRedirectOptionAttribute>) p.autoRedirect).ToArray<PXQuickProcess.AutoRedirectOptionAttribute>();
    PXQuickProcess.AutoRedirectOptionAttribute o1 = array2.Length <= 1 ? ((IEnumerable<PXQuickProcess.AutoRedirectOptionAttribute>) array2).FirstOrDefault<PXQuickProcess.AutoRedirectOptionAttribute>() : throw new PXInvalidOperationException("An entity of {0} type has several properties marked with {1}, while only a single property may be marked with this type of attribute.", new object[2]
    {
      (object) typeof (TConfig),
      (object) "AutoRedirectOptionAttribute"
    });
    bool? nullable;
    int num1;
    if (o1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = o1.With<PXQuickProcess.AutoRedirectOptionAttribute, bool?>((Func<PXQuickProcess.AutoRedirectOptionAttribute, bool?>) (a => (bool?) qpCache.GetValue((object) (TConfig) quickProcessConfig, a.FieldName)));
      bool flag = true;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    bool autoRedirectFlag = num1 != 0;
    PXQuickProcess.AutoDownloadReportsOptionAttribute[] array3 = array1.SelectMany(p => (IEnumerable<PXQuickProcess.AutoDownloadReportsOptionAttribute>) p.autoDownload).ToArray<PXQuickProcess.AutoDownloadReportsOptionAttribute>();
    PXQuickProcess.AutoDownloadReportsOptionAttribute o2 = array3.Length <= 1 ? ((IEnumerable<PXQuickProcess.AutoDownloadReportsOptionAttribute>) array3).FirstOrDefault<PXQuickProcess.AutoDownloadReportsOptionAttribute>() : throw new PXInvalidOperationException("An entity of {0} type has several properties marked with {1}, while only a single property may be marked with this type of attribute.", new object[2]
    {
      (object) typeof (TConfig),
      (object) "AutoDownloadReportsOptionAttribute"
    });
    int num2;
    if (o2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = o2.With<PXQuickProcess.AutoDownloadReportsOptionAttribute, bool?>((Func<PXQuickProcess.AutoDownloadReportsOptionAttribute, bool?>) (a => (bool?) qpCache.GetValue((object) (TConfig) quickProcessConfig, a.FieldName)));
      bool flag = true;
      num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    bool autoDownloadFlag = num2 != 0;
    for (int index = 0; index < stepsArr.Length; ++index)
    {
      string fieldName = stepsArr[index].FieldName;
      ref PXQuickProcess.StepInfo local = ref stepsArr[index];
      nullable = (bool?) qpCache.GetValue(qpCache.Current, fieldName);
      int num3 = nullable.GetValueOrDefault() ? 1 : 0;
      local.IsSwitchedOn = num3 != 0;
      stepsArr[index].IsStartPoint = dictionary.ContainsKey(fieldName) && dictionary[fieldName].IsStartPoint(qpCache, (object) quickProcessConfig);
      if (lookup[fieldName].Any<PXQuickProcess.Step.RelatedParameterAttribute>())
        stepsArr[index].Parameters = lookup[fieldName].ToDictionary<PXQuickProcess.Step.RelatedParameterAttribute, string, object>((Func<PXQuickProcess.Step.RelatedParameterAttribute, string>) (p => p.ActionParameterName), (Func<PXQuickProcess.Step.RelatedParameterAttribute, object>) (p => qpCache.GetValue((object) (TConfig) quickProcessConfig, p.FieldName)));
    }
    stepsArr = PXQuickProcess.SortSteps(qpCache, stepsArr);
    List<Exception> errors = new List<Exception>();
    PXLongOperation.StartOperation((PXGraph) graph, (PXToggleAsyncDelegate) (() => PXQuickProcess.QuickProcessOperation<TGraph, TTable>(startEntity, stepsArr, autoRedirectFlag, autoDownloadFlag, errors)));
    return (IEnumerable<Exception>) errors;
  }

  private static PXQuickProcess.StepInfo[] SortSteps(
    PXCache qpCache,
    PXQuickProcess.StepInfo[] steps)
  {
    \u003C\u003Ef__AnonymousType63<IEnumerable<PXQuickProcess.Step.IsInsertedJustBeforeAttribute>, IEnumerable<PXQuickProcess.Step.IsInsertedJustAfterAttribute>>[] array1 = qpCache.Fields.Select(fieldName => new
    {
      fieldName = fieldName,
      attributes = qpCache.GetAttributesReadonly(fieldName)
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      before = _param1.attributes.OfType<PXQuickProcess.Step.IsInsertedJustBeforeAttribute>()
    }).Select(_param1 => new
    {
      \u003C\u003Eh__TransparentIdentifier1 = _param1,
      after = _param1.\u003C\u003Eh__TransparentIdentifier0.attributes.OfType<PXQuickProcess.Step.IsInsertedJustAfterAttribute>()
    }).Select(_param1 => new
    {
      before = _param1.\u003C\u003Eh__TransparentIdentifier1.before,
      after = _param1.after
    }).ToArray();
    Dictionary<string, string> insertedBefore = array1.SelectMany(a => a.before).ToDictionary<PXQuickProcess.Step.IsInsertedJustBeforeAttribute, string, string>((Func<PXQuickProcess.Step.IsInsertedJustBeforeAttribute, string>) (a => a.FieldName), (Func<PXQuickProcess.Step.IsInsertedJustBeforeAttribute, string>) (a => a.SuccessorField.Name), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    Dictionary<string, string> insertedAfter = array1.SelectMany(a => a.after).ToDictionary<PXQuickProcess.Step.IsInsertedJustAfterAttribute, string, string>((Func<PXQuickProcess.Step.IsInsertedJustAfterAttribute, string>) (a => a.FieldName), (Func<PXQuickProcess.Step.IsInsertedJustAfterAttribute, string>) (a => a.PredecessorField.Name), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<PXQuickProcess.StepInfo> list1 = ((IEnumerable<PXQuickProcess.StepInfo>) steps).Where<PXQuickProcess.StepInfo>((Func<PXQuickProcess.StepInfo, bool>) (s => insertedBefore.ContainsKey(s.FieldName))).ToList<PXQuickProcess.StepInfo>();
    List<PXQuickProcess.StepInfo> list2 = ((IEnumerable<PXQuickProcess.StepInfo>) steps).Where<PXQuickProcess.StepInfo>((Func<PXQuickProcess.StepInfo, bool>) (s => insertedAfter.ContainsKey(s.FieldName))).Reverse<PXQuickProcess.StepInfo>().ToList<PXQuickProcess.StepInfo>();
    LinkedList<PXQuickProcess.StepInfo> linkedList = EnumerableExtensions.ToLinkedList<PXQuickProcess.StepInfo>(EnumerableExtensions.ExceptBy<PXQuickProcess.StepInfo, string>((IEnumerable<PXQuickProcess.StepInfo>) steps, list1.Concat<PXQuickProcess.StepInfo>((IEnumerable<PXQuickProcess.StepInfo>) list2), (Func<PXQuickProcess.StepInfo, string>) (t => t.FieldName), (IEqualityComparer<string>) null));
    do
    {
      int count = linkedList.Count;
      PXQuickProcess.StepInfo[] array2 = list1.ToArray();
      list1.Clear();
      foreach (PXQuickProcess.StepInfo stepInfo in array2)
      {
        string beforeThis = insertedBefore[stepInfo.FieldName];
        LinkedListNode<PXQuickProcess.StepInfo> node = EnumerableExtensions.Find<PXQuickProcess.StepInfo>(linkedList, (Func<PXQuickProcess.StepInfo, bool>) (f => StringComparer.OrdinalIgnoreCase.Equals(f.FieldName, beforeThis)));
        if (node == null)
          list1.Add(stepInfo);
        else
          linkedList.AddBefore(node, stepInfo);
      }
      PXQuickProcess.StepInfo[] array3 = list2.ToArray();
      list2.Clear();
      foreach (PXQuickProcess.StepInfo stepInfo in array3)
      {
        string afterThis = insertedAfter[stepInfo.FieldName];
        LinkedListNode<PXQuickProcess.StepInfo> last = EnumerableExtensions.FindLast<PXQuickProcess.StepInfo>(linkedList, (Func<PXQuickProcess.StepInfo, bool>) (f => StringComparer.OrdinalIgnoreCase.Equals(f.FieldName, afterThis)));
        if (last == null)
          list2.Add(stepInfo);
        else
          linkedList.AddAfter(last, stepInfo);
      }
      if (linkedList.Count == count && (list1.Any<PXQuickProcess.StepInfo>() || list2.Any<PXQuickProcess.StepInfo>()))
        throw new PXInvalidOperationException("There are steps that cannot be inserted into Quick Process flow, because they are configured to be inserted relatively to the step that does not exist.");
    }
    while (list1.Any<PXQuickProcess.StepInfo>() || list2.Any<PXQuickProcess.StepInfo>());
    return linkedList.ToArray<PXQuickProcess.StepInfo>();
  }

  private static void QuickProcessOperation<TGraph, TTable>(
    TTable startEntity,
    PXQuickProcess.StepInfo[] stepInfos,
    bool autoRedirect,
    bool autoDownloadReports,
    List<Exception> errors)
    where TGraph : PXGraph, new()
    where TTable : class, IBqlTable, new()
  {
    PXQuickProcess.StepInfo[] array = ((IEnumerable<PXQuickProcess.StepInfo>) stepInfos).Where<PXQuickProcess.StepInfo>((Func<PXQuickProcess.StepInfo, bool>) (s => s.IsSwitchedOn)).ToArray<PXQuickProcess.StepInfo>();
    if (array.Length == 0)
      throw new PXInvalidOperationException("Quick Process cannot be started, because its flow is empty.");
    if (!array[0].IsStartPoint)
      throw new PXInvalidOperationException("Quick Process cannot be started from {0} action, because it isn't marked as a Start Point.", new object[1]
      {
        (object) array[0].ActionName
      });
    IEnumerable<Exception> collection = new PXQuickProcess.Engine((IEnumerable<PXQuickProcess.Engine.GraphAction>) ((IEnumerable<PXQuickProcess.StepInfo>) array).Select<PXQuickProcess.StepInfo, PXQuickProcess.Engine.GraphAction>((Func<PXQuickProcess.StepInfo, PXQuickProcess.Engine.GraphAction>) (step => new PXQuickProcess.Engine.GraphAction(step))).ToArray<PXQuickProcess.Engine.GraphAction>())
    {
      AutoRedirect = autoRedirect,
      AutoDownload = autoDownloadReports
    }.ExecuteFlow<TGraph, TTable>(startEntity);
    errors.AddRange(collection);
  }

  [PXLocalizable]
  public class Messages
  {
    public const string NoActionNamed = "Graph doesn't contain action named \"{0}\"";
    public const string EntityCannotConfigureQuickProcess = "An entity of {0} type cannot configure Quick Process, because it isn't marked with any Quick Process related attribute.";
    public const string TooManyPropertiesMarkedWithAttribute = "An entity of {0} type has several properties marked with {1}, while only a single property may be marked with this type of attribute.";
    public const string BrokenStepsRelativeOrdering = "There are steps that cannot be inserted into Quick Process flow, because they are configured to be inserted relatively to the step that does not exist.";
    public const string QuickProcessCanBeStartedOnlyFromStartPoint = "Quick Process cannot be started from {0} action, because it isn't marked as a Start Point.";
    public const string FlowIsEmpty = "Quick Process cannot be started, because its flow is empty.";
    public const string NoTargetForAction = "The {0} step has been skipped because none of the previous steps provided any {1} entity for the step.";
  }

  public enum ActionFlow
  {
    NoFlow,
    LastInFlow,
    HasNextInFlow,
  }

  public interface IStarterAction
  {
  }

  internal struct StepInfo
  {
    public StepInfo(
      System.Type graphType,
      string actionName,
      string actionDisplayName,
      string onSuccessMessage,
      string onFailureMessage)
      : this()
    {
      this.GraphType = graphType;
      this.ActionName = actionName;
      this.ActionDisplayName = actionDisplayName;
      this.OnSuccessMessage = onSuccessMessage;
      this.OnFailureMessage = onFailureMessage;
    }

    public System.Type GraphType { get; }

    public string ActionName { get; }

    public string ActionDisplayName { get; }

    public string OnSuccessMessage { get; }

    public string OnFailureMessage { get; }

    public string FieldName { get; set; }

    public bool IsStartPoint { get; set; }

    public bool IsSwitchedOn { get; set; }

    public Dictionary<string, object> Parameters { get; set; }

    public override string ToString()
    {
      return $"({(this.IsSwitchedOn ? "On " : "Off")}) {this.GraphType.Name}->{this.ActionName}{(this.IsStartPoint ? " (Start Point)" : "")}";
    }
  }

  /// <summary>
  /// Special Quick Process <see cref="T:PX.Data.PXAction" />,
  /// which state should be configured with <see cref="T:PX.Data.PXQuickProcess.Action`1.ConfiguredBy`1" /> option
  /// </summary>
  /// <typeparam name="TPrimary">The primary entity type</typeparam>
  public static class Action<TPrimary> where TPrimary : class, IBqlTable, new()
  {
    /// <summary>
    /// Special Quick Process <see cref="T:PX.Data.PXAction" />
    /// which state is automatically configured by a <typeparamref name="TConfig" /> instance
    /// </summary>
    /// <typeparam name="TConfig">The configuring entity type</typeparam>
    public class ConfiguredBy<TConfig> : PXAction<TPrimary>, PXQuickProcess.IStarterAction where TConfig : class, IBqlTable, new()
    {
      /// <exclude />
      public ConfiguredBy(PXGraph graph)
        : base(graph)
      {
        this.AddHandler(graph);
      }

      /// <exclude />
      public ConfiguredBy(PXGraph graph, string name)
        : base(graph, name)
      {
        this.AddHandler(graph);
      }

      /// <exclude />
      public ConfiguredBy(PXGraph graph, Delegate handler)
        : base(graph, handler)
      {
        this.AddHandler(graph);
      }

      private void AddHandler(PXGraph graph)
      {
        graph.RowSelected.AddHandler<TPrimary>(new PXRowSelected(this.Primary_RowSelected));
      }

      protected virtual void Primary_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
      {
        if (PXGraph.ProxyIsActive)
          return;
        bool isEnabled = false;
        PXCache qpCache = this._Graph.Caches[typeof (TConfig)];
        var data = qpCache.Fields.SelectMany<string, PXEventSubscriberAttribute>((Func<string, IEnumerable<PXEventSubscriberAttribute>>) (fn => (IEnumerable<PXEventSubscriberAttribute>) qpCache.GetAttributesReadonly(fn))).OfType<PXQuickProcess.Step.IsStartPointAttribute>().Where<PXQuickProcess.Step.IsStartPointAttribute>((Func<PXQuickProcess.Step.IsStartPointAttribute, bool>) (a => a.IsStartPoint(qpCache, qpCache.Current))).SelectMany<PXQuickProcess.Step.IsStartPointAttribute, PXEventSubscriberAttribute>((Func<PXQuickProcess.Step.IsStartPointAttribute, IEnumerable<PXEventSubscriberAttribute>>) (a => (IEnumerable<PXEventSubscriberAttribute>) qpCache.GetAttributesReadonly(a.FieldName))).OfType<PXQuickProcess.Step.IsBoundToAttribute>().Select(ba => new
        {
          GraphType = ba.GraphType,
          ActionName = ba.ActionName
        }).FirstOrDefault();
        if (data != null)
        {
          if (data.GraphType == CustomizedTypeManager.GetTypeNotCustomized(this._Graph.GetType()))
          {
            PXButtonState state = (PXButtonState) (this._Graph.Actions[data.ActionName] ?? throw new PXException("Graph doesn't contain action named \"{0}\"", new object[1]
            {
              (object) data
            })).GetState(e.Row);
            isEnabled = state.Visible && state.Enabled;
          }
          else
            isEnabled = true;
        }
        this.SetEnabled(isEnabled);
      }
    }
  }

  /// <summary>Quick Process declarative configuration attributes</summary>
  public static class Step
  {
    public interface IDefinition
    {
      System.Type Graph { get; }

      string ActionName { get; }

      string OnSuccessMessage { get; }

      string OnFailureMessage { get; }
    }

    public abstract class Definition<TGraph> : PXQuickProcess.Step.IDefinition where TGraph : PXGraph
    {
      public Definition(Expression<Func<TGraph, PXAction>> actionSelector)
      {
        this.ActionName = ((MemberExpression) actionSelector.Body).Member.Name;
      }

      public System.Type Graph => typeof (TGraph);

      public string ActionName { get; }

      public abstract string OnSuccessMessage { get; }

      public abstract string OnFailureMessage { get; }
    }

    /// <summary>
    /// Binds <see cref="T:PX.Data.IBqlField" /> with a graph action
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class IsBoundToAttribute : PXAggregateAttribute, IPXRowSelectedSubscriber
    {
      private bool _performChecks;

      private PXUIFieldAttribute UIField
      {
        get => this._Attributes.OfType<PXUIFieldAttribute>().First<PXUIFieldAttribute>();
      }

      /// <summary>The graph type</summary>
      public System.Type GraphType { get; set; }

      /// <summary>The action name</summary>
      public string ActionName { get; set; }

      /// <summary>On success message</summary>
      public string OnSuccessMessage { get; set; }

      /// <summary>On failure message</summary>
      public string OnFailureMessage { get; set; }

      /// <summary>
      /// Gets or sets the field name displayed in the user interface.
      /// This name is rendered as the input control label on a form or as the grid column header.
      /// </summary>
      /// <remarks>The default value is the menuID.</remarks>
      public string DisplayName
      {
        get => this.UIField.DisplayName;
        set => this.UIField.DisplayName = value;
      }

      /// <summary>
      /// Binds <see cref="T:PX.Data.IBqlField" /> with a graph action
      /// </summary>
      /// <param name="graphType">The graph type</param>
      /// <param name="actionName">The action name</param>
      /// <param name="onSuccessMsg">On success message</param>
      /// <param name="onFailureMsg">On failure message</param>
      /// <param name="nonDatabaseField">Indicates whether target field should be an unbound field</param>
      public IsBoundToAttribute(
        System.Type graphType,
        string actionName,
        string onSuccessMsg,
        string onFailureMsg,
        bool nonDatabaseField = false)
      {
        this.Init(graphType, actionName, onSuccessMsg, onFailureMsg, nonDatabaseField);
      }

      /// <summary>
      /// Binds <see cref="T:PX.Data.IBqlField" /> with a graph action
      /// </summary>
      /// <param name="stepDefinition">Type that implements <see cref="T:PX.Data.PXQuickProcess.Step.IDefinition" /> and defines a graph type and action name</param>
      /// <param name="nonDatabaseField">Indicates whether target field should be an unbound field</param>
      public IsBoundToAttribute(System.Type stepDefinition, bool nonDatabaseField = false)
      {
        if (stepDefinition == (System.Type) null)
          throw new PXArgumentException(nameof (stepDefinition), "The argument cannot be null.");
        PXQuickProcess.Step.IDefinition definition = typeof (PXQuickProcess.Step.IDefinition).IsAssignableFrom(stepDefinition) ? (PXQuickProcess.Step.IDefinition) Activator.CreateInstance(stepDefinition) : throw new PXArgumentException(nameof (stepDefinition));
        this.Init(definition.Graph, definition.ActionName, definition.OnSuccessMessage, definition.OnFailureMessage, nonDatabaseField);
      }

      private void Init(
        System.Type graphType,
        string actionName,
        string onSuccessMsg,
        string onFailureMsg,
        bool nonDatabaseField)
      {
        this.GraphType = graphType;
        this.ActionName = actionName;
        this.OnSuccessMessage = onSuccessMsg;
        this.OnFailureMessage = onFailureMsg;
        this._Attributes.Add(nonDatabaseField ? (PXEventSubscriberAttribute) new PXBoolAttribute() : (PXEventSubscriberAttribute) new PXDBBoolAttribute());
        this._Attributes.Add((PXEventSubscriberAttribute) new PXUIFieldAttribute()
        {
          DisplayName = actionName
        });
        this._Attributes.Add((PXEventSubscriberAttribute) new PXDefaultAttribute((object) false));
      }

      internal PXQuickProcess.StepInfo GetStepInfo()
      {
        return new PXQuickProcess.StepInfo(this.GraphType, this.ActionName, this.DisplayName, this.OnSuccessMessage, this.OnFailureMessage)
        {
          FieldName = this._FieldName
        };
      }

      /// <exclude />
      public override void CacheAttached(PXCache cache)
      {
        base.CacheAttached(cache);
        cache.Fields.SelectMany<string, PXEventSubscriberAttribute>(new Func<string, IEnumerable<PXEventSubscriberAttribute>>(cache.GetAttributesReadonly)).OfType<PXQuickProcess.Step.IsBoundToAttribute>().First<PXQuickProcess.Step.IsBoundToAttribute>()._performChecks = true;
      }

      /// <exclude />
      public void RowSelected(PXCache cache, PXRowSelectedEventArgs e)
      {
        if (!this._performChecks)
          return;
        PXQuickProcess.Step.IsApplicableAttribute.ProcessApplicability(cache, e);
        PXQuickProcess.Step.StepsRelationAttribute.ProcessRelations(cache, e);
        PXQuickProcess.Step.RelatedFieldAttribute.ProcessRelatedFields(cache, e);
      }
    }

    /// <summary>
    /// Indicates a field that is related to a specific Quick Process step
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class RelatedFieldAttribute : PXEventSubscriberAttribute
    {
      /// <summary>Quick Process step reference</summary>
      public System.Type StepField { get; set; }

      /// <summary>
      /// Indicates whether visibility of the targeted field should be synchronized
      /// with visibility of the related Quick Process step.
      /// </summary>
      public bool SyncVisibilityWithRelatedStep { get; set; } = true;

      /// <summary>
      /// Indicates a field that is related to a specific Quick Process step
      /// </summary>
      /// <param name="stepField">Quick Process step reference</param>
      public RelatedFieldAttribute(System.Type stepField) => this.StepField = stepField;

      /// <summary>
      /// Extract related fields info of the Quick Process steps
      /// </summary>
      /// <param name="cache">Quick Process configuration entity cache</param>
      /// <param name="row">Quick Process configuration entity</param>
      /// <returns>Step-fields names to related fields names map</returns>
      public static ILookup<PXQuickProcess.Step.RelatedFieldAttribute, string> GetStepRelatedFields(
        PXCache cache,
        object row)
      {
        return cache.Fields.SelectMany<string, PXEventSubscriberAttribute>(new Func<string, IEnumerable<PXEventSubscriberAttribute>>(cache.GetAttributesReadonly)).OfType<PXQuickProcess.Step.RelatedFieldAttribute>().ToLookup<PXQuickProcess.Step.RelatedFieldAttribute, PXQuickProcess.Step.RelatedFieldAttribute, string>((Func<PXQuickProcess.Step.RelatedFieldAttribute, PXQuickProcess.Step.RelatedFieldAttribute>) (a => a), (Func<PXQuickProcess.Step.RelatedFieldAttribute, string>) (a => a.StepField.Name));
      }

      internal static void ProcessRelatedFields(PXCache cache, PXRowSelectedEventArgs e)
      {
        ILookup<PXQuickProcess.Step.RelatedFieldAttribute, string> stepRelatedFields = PXQuickProcess.Step.RelatedFieldAttribute.GetStepRelatedFields(cache, e.Row);
        Func<string, bool> predicate = Func.Memorize<string, bool>((Func<string, bool>) (fn => e.Row != null && (bool) cache.GetValue(e.Row, fn)));
        foreach (IGrouping<PXQuickProcess.Step.RelatedFieldAttribute, string> source in stepRelatedFields.Where<IGrouping<PXQuickProcess.Step.RelatedFieldAttribute, string>>((Func<IGrouping<PXQuickProcess.Step.RelatedFieldAttribute, string>, bool>) (rfg => rfg.Key.SyncVisibilityWithRelatedStep)))
          PXUIFieldAttribute.SetVisible(cache, e.Row, source.Key.FieldName, source.Any<string>(predicate));
      }
    }

    /// <summary>
    /// Declares a parameter for a specific Quick Process step
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class RelatedParameterAttribute : PXQuickProcess.Step.RelatedFieldAttribute
    {
      /// <summary>
      /// Parameter name of the bound method of the related step
      /// </summary>
      public string ActionParameterName { get; set; }

      /// <summary>
      /// Declares a parameter for a specific Quick process step
      /// </summary>
      /// <param name="stepField">Quick Process step reference</param>
      /// <param name="actionParameterName">Parameter name of the bound method of the related step</param>
      public RelatedParameterAttribute(System.Type stepField, string actionParameterName)
        : base(stepField)
      {
        this.ActionParameterName = actionParameterName;
      }
    }

    /// <summary>Declares dependencies between Quick Process steps</summary>
    public class StepsRelationAttribute : PXEventSubscriberAttribute
    {
      private readonly System.Type[] _relatedFields;
      private readonly bool _currentFieldIsDependent;

      protected StepsRelationAttribute(System.Type[] relatedFields, bool currentFieldIsDependent)
      {
        this._relatedFields = relatedFields;
        this._currentFieldIsDependent = currentFieldIsDependent;
      }

      protected StepsRelationAttribute(System.Type relatedField, bool currentFieldIsDependent)
        : this(TypeArrayOf<IBqlField>.CheckAndExtract(TypeArrayOf<IBqlField>.EmptyOrSingleOrSelf(relatedField), (string) null), currentFieldIsDependent)
      {
      }

      /// <summary>
      /// Extract presence change restriction info of the Quick Process steps
      /// </summary>
      /// <param name="cache">Quick Process configuration entity cache</param>
      /// <param name="row">Quick Process configuration entity</param>
      /// <returns>Fields names to restrict reasons map</returns>
      public static Dictionary<string, PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason> GetRestrictedSteps(
        PXCache cache,
        object row)
      {
        \u003C\u003Ef__AnonymousType53<string, string>[] array = EnumerableExtensions.Denormalize(cache.Fields.SelectMany<string, PXEventSubscriberAttribute>(new Func<string, IEnumerable<PXEventSubscriberAttribute>>(cache.GetAttributesReadonly)).OfType<PXQuickProcess.Step.StepsRelationAttribute>(), (Func<PXQuickProcess.Step.StepsRelationAttribute, IEnumerable<System.Type>>) (atr => (IEnumerable<System.Type>) atr._relatedFields), (atr, rf) => new
        {
          Field = atr._currentFieldIsDependent ? atr._FieldName : rf.Name,
          Requires = atr._currentFieldIsDependent ? rf.Name : atr._FieldName
        }).ToArray();
        ILookup<string, string> fieldsThatAreRequiredBy = array.ToLookup(r => r.Field, r => r.Requires, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        ILookup<string, string> fieldsThatRequire = array.ToLookup(r => r.Requires, r => r.Field, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        HashSet<string> hashSet = cache.Fields.SelectMany<string, PXEventSubscriberAttribute>(new Func<string, IEnumerable<PXEventSubscriberAttribute>>(cache.GetAttributesReadonly)).OfType<PXQuickProcess.Step.IsStartPointAttribute>().Where<PXQuickProcess.Step.IsStartPointAttribute>((Func<PXQuickProcess.Step.IsStartPointAttribute, bool>) (a => a.PreventStepPresenceChanging && a.IsStartPoint(cache, row))).Select<PXQuickProcess.Step.IsStartPointAttribute, string>((Func<PXQuickProcess.Step.IsStartPointAttribute, string>) (a => a.FieldName)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        Func<string, bool> getValueOf = Func.Memorize<string, bool>((Func<string, bool>) (fn => cache.GetValue(row, fn).With<object, bool>((Func<object, bool>) (v =>
        {
          if (!(v is bool))
            return v != null;
          bool? nullable = (bool?) v;
          bool flag = true;
          return nullable.GetValueOrDefault() == flag & nullable.HasValue;
        }))));
        Func<string, bool> getVisibleOf = Func.Memorize<string, bool>((Func<string, bool>) (fn =>
        {
          PXUIFieldAttribute pxuiFieldAttribute = cache.GetAttributesReadonly(row, fn).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
          return pxuiFieldAttribute != null && pxuiFieldAttribute.Visible;
        }));
        Dictionary<string, PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason> restrictedSteps = new Dictionary<string, PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason>();
        foreach (string key in cache.Fields.Where<string>((Func<string, bool>) (fn => fieldsThatAreRequiredBy[fn].Any<string>() || fieldsThatRequire[fn].Any<string>())))
        {
          bool flag1 = fieldsThatAreRequiredBy[key].All<string>(new Func<string, bool>(ifVisibleThenEnabled));
          bool flag2 = fieldsThatRequire[key].All<string>(new Func<string, bool>(ifVisibleThenDisabled));
          bool flag3 = hashSet.Contains(key);
          restrictedSteps.Add(key, (PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason) ((!flag3 ? 0 : 3) | (flag1 ? 0 : 1) | (flag2 ? 0 : 2)));
        }
        return restrictedSteps;

        bool ifVisibleThenEnabled(string fieldName)
        {
          return getVisibleOf(fieldName).Implies((Func<bool>) (() => getValueOf(fieldName)));
        }

        bool ifVisibleThenDisabled(string fieldName)
        {
          return getVisibleOf(fieldName).Implies((Func<bool>) (() => !getValueOf(fieldName)));
        }
      }

      /// <summary>
      /// Inspect whether change of the step presence is restricted
      /// </summary>
      /// <typeparam name="TField">Inspected field type</typeparam>
      /// <param name="cache">Quick Process configuration entity cache</param>
      /// <param name="row">Quick Process configuration entity</param>
      /// <returns><c>true</c> if the step is restricted, otherwise - <c>false</c></returns>
      public static bool IsStepRestricted<TField>(PXCache cache, object row) where TField : IBqlField
      {
        return PXQuickProcess.Step.StepsRelationAttribute.IsStepRestricted(cache, row, typeof (TField).Name);
      }

      /// <summary>
      /// Inspect whether change of the step presence is restricted
      /// </summary>
      /// <param name="cache">Quick Process configuration entity cache</param>
      /// <param name="row">Quick Process configuration entity</param>
      /// <param name="fieldName">Inspected field name</param>
      /// <returns><c>true</c> if the step is restricted, otherwise - <c>false</c></returns>
      public static bool IsStepRestricted(PXCache cache, object row, string fieldName)
      {
        return PXQuickProcess.Step.StepsRelationAttribute.GetRestrictedSteps(cache, row).With<Dictionary<string, PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason>, bool>((Func<Dictionary<string, PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason>, bool>) (rs => rs.ContainsKey(fieldName) && rs[fieldName] != 0));
      }

      internal static void ProcessRelations(PXCache cache, PXRowSelectedEventArgs e)
      {
        foreach (KeyValuePair<string, PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason> restrictedStep in PXQuickProcess.Step.StepsRelationAttribute.GetRestrictedSteps(cache, e.Row))
          PXUIFieldAttribute.SetEnabled(cache, e.Row, restrictedStep.Key, restrictedStep.Value == PXQuickProcess.Step.StepsRelationAttribute.RestrictChangeReason.None);
      }

      /// <summary>
      /// Indicates why change of the step presence is restricted
      /// </summary>
      [Flags]
      public enum RestrictChangeReason
      {
        None = 0,
        PreviousStepIsDisabled = 1,
        SubsequentStepIsEnabled = 2,
        IsStartPoint = SubsequentStepIsEnabled | PreviousStepIsDisabled, // 0x00000003
      }
    }

    /// <summary>
    /// Indicates that the current step requires presence of some other steps
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    /// <summary>
    /// Indicates that the current step requires presence of some other steps
    /// </summary>
    public class RequiresStepsAttribute(System.Type relatedField, params System.Type[] relatedFields) : 
      PXQuickProcess.Step.StepsRelationAttribute(EnumerableExtensions.Prepend<System.Type>(relatedFields, relatedField), true)
    {
    }

    /// <summary>
    /// Indicates that the current step presence is required by some other steps
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    /// <summary>
    /// Indicates that the current step presence is required by some other steps
    /// </summary>
    public class IsRequiredByStepsAttribute(System.Type relatedField, params System.Type[] relatedFields) : 
      PXQuickProcess.Step.StepsRelationAttribute(EnumerableExtensions.Prepend<System.Type>(relatedFields, relatedField), false)
    {
    }

    /// <summary>Declares applicability condition of the current step</summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    /// <summary>Declares applicability condition of the current step</summary>
    public class IsApplicableAttribute(System.Type condition) : PXBaseConditionAttribute(condition)
    {
      /// <summary>Extract applicability info of the Quick Process steps</summary>
      /// <param name="cache">Quick Process configuration entity cache</param>
      /// <param name="row">Quick Process configuration entity</param>
      /// <returns>Fields names to their applicability map</returns>
      public static Dictionary<string, bool> GetStepsApplicability(PXCache cache, object row)
      {
        return cache.Fields.SelectMany<string, PXEventSubscriberAttribute>(new Func<string, IEnumerable<PXEventSubscriberAttribute>>(cache.GetAttributesReadonly)).OfType<PXQuickProcess.Step.IsApplicableAttribute>().ToDictionary<PXQuickProcess.Step.IsApplicableAttribute, string, bool>((Func<PXQuickProcess.Step.IsApplicableAttribute, string>) (a => a.FieldName), (Func<PXQuickProcess.Step.IsApplicableAttribute, bool>) (a => a.IsApplicable(cache, row)));
      }

      /// <summary>
      /// Indicates whether applicability condition of the current step is met
      /// </summary>
      /// <param name="cache">Quick Process configuration entity cache</param>
      /// <param name="row">Quick Process configuration entity</param>
      /// <returns><c>true</c> if applicability condition of the current step is met, otherwise - <c>false</c></returns>
      public bool IsApplicable(PXCache cache, object row)
      {
        return row != null && PXBaseConditionAttribute.GetConditionResult(cache, row, this.Condition);
      }

      internal static void ProcessApplicability(PXCache cache, PXRowSelectedEventArgs e)
      {
        foreach (KeyValuePair<string, bool> keyValuePair in PXQuickProcess.Step.IsApplicableAttribute.GetStepsApplicability(cache, e.Row))
        {
          PXUIFieldAttribute.SetVisible(cache, e.Row, keyValuePair.Key, keyValuePair.Value);
          if (!keyValuePair.Value && e.Row != null)
          {
            bool? nullable = (bool?) cache.GetValue(e.Row, keyValuePair.Key);
            bool flag = true;
            if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
              cache.SetValue(e.Row, keyValuePair.Key, (object) false);
          }
        }
      }
    }

    /// <summary>
    /// Indicates when the current step is considered as a flow start point.
    /// Quick Process flow could be started only from a start point step
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    /// <summary>
    /// Indicates when the current step is considered as a flow start point.
    /// Quick Process flow could be started only from a start point step
    /// </summary>
    public class IsStartPointAttribute(System.Type condition) : 
      PXBaseConditionAttribute(condition),
      IPXFieldDefaultingSubscriber
    {
      /// <summary>
      /// Indicates whether step presence changing should be prevented in case when the start point condition is met
      /// </summary>
      public bool PreventStepPresenceChanging { get; set; } = true;

      /// <exclude />
      public void FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
      {
        e.NewValue = (object) this.IsStartPoint(cache, e.Row);
      }

      /// <summary>
      /// Indicates whether start point condition of the current step is met
      /// </summary>
      /// <param name="cache">Quick Process configuration entity cache</param>
      /// <param name="row">Quick Process configuration entity</param>
      /// <returns><c>true</c> if tart point condition of the current step is met, otherwise - <c>false</c></returns>
      public bool IsStartPoint(PXCache cache, object row)
      {
        return row != null && PXBaseConditionAttribute.GetConditionResult(cache, row, this.Condition);
      }
    }

    /// <summary>
    /// Declares that the current step should be inserted just after the related step in the Quick Process flow
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class IsInsertedJustAfterAttribute : PXEventSubscriberAttribute
    {
      /// <summary>Predecessor step</summary>
      public System.Type PredecessorField { get; }

      /// <summary>
      /// Declares that the current step should be inserted just after the related step in the Quick Process flow
      /// </summary>
      public IsInsertedJustAfterAttribute(System.Type predecessorField)
      {
        this.PredecessorField = predecessorField;
      }
    }

    /// <summary>
    /// Declares that the current step should be inserted just before the related step in the Quick Process flow
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class IsInsertedJustBeforeAttribute : PXEventSubscriberAttribute
    {
      /// <summary>Successor step</summary>
      public System.Type SuccessorField { get; }

      /// <summary>
      /// Declares that the current step should be inserted just before the related step in the Quick Process flow
      /// </summary>
      public IsInsertedJustBeforeAttribute(System.Type successorField)
      {
        this.SuccessorField = successorField;
      }
    }
  }

  /// <summary>
  /// Indicates whether Quick Process reports and documents should be opened immediately on their creation.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public class AutoRedirectOptionAttribute : PXAggregateAttribute
  {
  }

  /// <summary>
  /// Indicates whether Quick Process reports should be downloaded immediately on their creation.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public class AutoDownloadReportsOptionAttribute : PXAggregateAttribute
  {
  }

  internal class Engine
  {
    private const string QuickProcessLogKey = "QuickProcessLogKey";

    public static PXQuickProcess.Engine Instance
    {
      get => PXContext.GetSlot<PXQuickProcess.Engine>("QuickProcessEngine");
      private set => PXContext.SetSlot<PXQuickProcess.Engine>("QuickProcessEngine", value);
    }

    public Engine(
      IEnumerable<PXQuickProcess.Engine.GraphAction> actions)
    {
      foreach (PXQuickProcess.Engine.GraphAction action in actions)
        this.UpcomingFlow.Enqueue(action);
      this.OriginalFlow = (IEnumerable<PXQuickProcess.Engine.GraphAction>) this.UpcomingFlow.ToArray();
    }

    public Queue<PXQuickProcess.Engine.GraphAction> UpcomingFlow { get; private set; } = new Queue<PXQuickProcess.Engine.GraphAction>();

    public IEnumerable<PXQuickProcess.Engine.GraphAction> OriginalFlow { get; private set; }

    public bool AutoRedirect { get; set; }

    public bool AutoDownload { get; set; }

    public bool StorePersisted(System.Type table, IEnumerable<object> records)
    {
      if (records != null && records.Any<object>())
      {
        Queue<PXQuickProcess.Engine.GraphAction> upcomingFlow = this.UpcomingFlow;
        if ((upcomingFlow != null ? (upcomingFlow.Any<PXQuickProcess.Engine.GraphAction>(new Func<PXQuickProcess.Engine.GraphAction, bool>(IsWatching)) ? 1 : 0) : 0) != 0)
        {
          object record = EnumerableExtensions.WhereNotNull<object>(records).Last<object>();
          EnumerableExtensions.ForEach<PXQuickProcess.Engine.GraphAction>(this.UpcomingFlow.Where<PXQuickProcess.Engine.GraphAction>(new Func<PXQuickProcess.Engine.GraphAction, bool>(IsWatching)), (System.Action<PXQuickProcess.Engine.GraphAction>) (a => a.ProcessedEntity = record));
          return true;
        }
      }
      return false;

      bool IsWatching(PXQuickProcess.Engine.GraphAction a)
      {
        System.Type entityType = a.EntityType;
        return (object) entityType != null && entityType.IsAssignableFrom(table);
      }
    }

    public IEnumerable<Exception> ExecuteFlow<TGraph, TTable>(TTable entryPointEntity)
      where TGraph : PXGraph, new()
      where TTable : class, IBqlTable, new()
    {
      try
      {
        this.ClearLog();
        PXQuickProcess.Engine.Instance = this;
        PXQuickProcess.Engine.GraphAction action = this.UpcomingFlow.Dequeue();
        action.ActualizeGraph((PXGraph) PXGraph.CreateInstance<TGraph>());
        action.ProcessedEntity = (object) entryPointEntity;
        this.ExecuteAction(action);
        return (IEnumerable<Exception>) EnumerableExtensions.WhereNotNull<Exception>(PXQuickProcess.Engine.GetLog().Select<PXQuickProcess.Engine.LogEntry, Exception>((Func<PXQuickProcess.Engine.LogEntry, Exception>) (log => log.Exception))).ToArray<Exception>();
      }
      finally
      {
        PXQuickProcess.Engine.Instance = (PXQuickProcess.Engine) null;
      }
    }

    private void ExecuteAction(PXQuickProcess.Engine.GraphAction action)
    {
      PXQuickProcess.Engine.GraphAction action1 = (PXQuickProcess.Engine.GraphAction) null;
      Exception ex1 = (Exception) null;
      try
      {
        action.Started = true;
        this.PressActionButton(action);
        action.Completed = true;
        action1 = this.PrepareNextAction(action);
      }
      catch (Exception ex2)
      {
        ex1 = ex2;
        switch (ex2)
        {
          case PXBaseRedirectException _:
          case PXOperationCompletedException _:
            action.Completed = true;
            action1 = this.PrepareNextAction(action);
            break;
          default:
            action.Failed = true;
            break;
        }
      }
      this.Log(action, ex1);
      if (action1 == null)
        return;
      this.ExecuteAction(action1);
    }

    private void PressActionButton(PXQuickProcess.Engine.GraphAction action)
    {
      PXAdapter adapter = new PXAdapter((PXView) new PXView.Dummy(action.Processor, action.Processor.Views[action.Processor.PrimaryView].BqlSelect, EnumerableExtensions.AsSingleEnumerable<object>(action.ProcessedEntity).ToList<object>()))
      {
        AllowRedirect = true,
        ForceButtonEnabledCheck = true,
        Arguments = EnumerableExtensions.ToDictionary<string, object>((IEnumerable<KeyValuePair<string, object>>) action.Arguments),
        QuickProcessFlow = this.UpcomingFlow.Any<PXQuickProcess.Engine.GraphAction>() ? PXQuickProcess.ActionFlow.HasNextInFlow : PXQuickProcess.ActionFlow.LastInFlow
      };
      PXCache cach = action.Processor.Caches[action.EntityType];
      if (cach.Locate(action.ProcessedEntity) != action.ProcessedEntity && EnumerableExtensions.IsIn<PXEntryStatus>(cach.GetStatus(action.ProcessedEntity), PXEntryStatus.Held, PXEntryStatus.Notchanged))
      {
        cach.Remove(action.ProcessedEntity);
        cach.Hold(action.ProcessedEntity);
      }
      cach.Current = action.ProcessedEntity;
      EnumerableExtensions.Consume(action.Processor.Actions[action.Name].Press(adapter));
    }

    private PXQuickProcess.Engine.GraphAction PrepareNextAction(
      PXQuickProcess.Engine.GraphAction prevAction)
    {
      if (this.UpcomingFlow.Any<PXQuickProcess.Engine.GraphAction>())
      {
        PXQuickProcess.Engine.GraphAction action = this.UpcomingFlow.Dequeue();
        action.ActualizeGraph(prevAction.Processor);
        if (action.ProcessedEntity == null)
          action.ProcessedEntity = action.Processor.Caches[action.EntityType].Current;
        if (action.ProcessedEntity != null)
          return action;
        string str = ((IEnumerable<string>) EntityHelper.GetFriendlyEntityName(action.EntityType).Split('.')).Last<string>();
        this.Log(action, (Exception) new PXException("The {0} step has been skipped because none of the previous steps provided any {1} entity for the step.", new object[2]
        {
          (object) action.DisplayName,
          (object) str
        }));
      }
      return (PXQuickProcess.Engine.GraphAction) null;
    }

    internal void Log(PXQuickProcess.Engine.GraphAction action, Exception ex)
    {
      List<PXQuickProcess.Engine.LogEntry> info = (List<PXQuickProcess.Engine.LogEntry>) PXLongOperation.GetCustomInfoForCurrentThread("QuickProcessLogKey") ?? new List<PXQuickProcess.Engine.LogEntry>();
      PXQuickProcess.Engine.LogEntry logEntry = new PXQuickProcess.Engine.LogEntry();
      bool flag = ex != null;
      if (ex is PXBaseRedirectException)
      {
        flag = false;
        ((PXBaseRedirectException) ex).Mode = PXBaseRedirectException.WindowMode.New;
        if (ex is PXRedirectRequiredException)
        {
          PXRedirectRequiredException requiredException = (PXRedirectRequiredException) ex;
          string[] keyNames = requiredException.Graph.GetKeyNames(requiredException.Graph.PrimaryView);
          logEntry.DocumentID = new List<Tuple<string, object>>();
          PXCache cache = requiredException.Graph.Views[requiredException.Graph.PrimaryView].Cache;
          foreach (string fieldName in keyNames)
            logEntry.DocumentID.Add(Tuple.Create<string, object>(fieldName, cache.GetValue(cache.Current, fieldName)));
        }
        logEntry.AutoRedirect = this.AutoRedirect;
        if (ex is PXReportRequiredException)
          logEntry.AutoExport = this.AutoDownload;
      }
      logEntry.Exception = ex;
      logEntry.Name = (ex == null | flag ? action.OnFailureMsg : action.OnSuccessMsg) ?? action.Name;
      info.Add(logEntry);
      PXLongOperation.SetCustomInfoInternal(PXLongOperation.GetOperationKey(), "QuickProcessLogKey", (object) info);
    }

    internal void ClearLog(object key = null)
    {
      key = key ?? PXLongOperation.GetOperationKey();
      List<PXQuickProcess.Engine.LogEntry> customInfo = (List<PXQuickProcess.Engine.LogEntry>) PXLongOperation.GetCustomInfo(key, "QuickProcessLogKey");
      if (customInfo == null)
        return;
      lock (((ICollection) customInfo).SyncRoot)
        customInfo.Clear();
    }

    internal static List<PXQuickProcess.Engine.LogEntry> GetLog(object key = null)
    {
      key = key ?? PXLongOperation.GetOperationKey();
      List<PXQuickProcess.Engine.LogEntry> customInfo = (List<PXQuickProcess.Engine.LogEntry>) PXLongOperation.GetCustomInfo(key, "QuickProcessLogKey");
      if (customInfo == null)
        return (List<PXQuickProcess.Engine.LogEntry>) null;
      lock (((ICollection) customInfo).SyncRoot)
        return customInfo.ToList<PXQuickProcess.Engine.LogEntry>();
    }

    internal class GraphAction
    {
      private static readonly ConcurrentDictionary<System.Type, System.Type> GraphToEntity = new ConcurrentDictionary<System.Type, System.Type>();

      private System.Type GetPrimaryEntityTypeFor(System.Type graphType)
      {
        return graphType == (System.Type) null ? (System.Type) null : PXQuickProcess.Engine.GraphAction.GraphToEntity.GetOrAdd(graphType, (Func<System.Type, System.Type>) (gt => PXPageIndexingService.GetScreenIDFromGraphType(gt).With<string, PXSiteMap.ScreenInfo>((Func<string, PXSiteMap.ScreenInfo>) (screenID => ScreenUtils.ScreenInfo.TryGetWithInvariantLocale(screenID))).With<PXSiteMap.ScreenInfo, string>((Func<PXSiteMap.ScreenInfo, string>) (screenInfo => screenInfo.PrimaryViewTypeName)).With<string, System.Type>((Func<string, System.Type>) (pvtn =>
        {
          System.Type type = PXBuildManager.GetType(pvtn, false);
          return (object) type != null ? type : System.Type.GetType(pvtn);
        }))));
      }

      public GraphAction(
        System.Type graphType,
        string actionName,
        string actionDisplayName,
        string onSuccessMsg,
        string onFailureMsg,
        IDictionary<string, object> arguments)
      {
        this.GraphType = graphType;
        this.EntityType = this.GetPrimaryEntityTypeFor(graphType);
        this.Name = actionName;
        this.DisplayName = actionDisplayName;
        this.OnSuccessMsg = PXMessages.LocalizeNoPrefix(onSuccessMsg);
        this.OnFailureMsg = PXMessages.LocalizeNoPrefix(onFailureMsg);
        this.Arguments = (IReadOnlyDictionary<string, object>) ((arguments != null ? EnumerableExtensions.ToDictionary<string, object>((IEnumerable<KeyValuePair<string, object>>) arguments) : (Dictionary<string, object>) null) ?? new Dictionary<string, object>());
      }

      public GraphAction(PXQuickProcess.StepInfo stepInfo)
        : this(stepInfo.GraphType, stepInfo.ActionName, stepInfo.ActionDisplayName, stepInfo.OnSuccessMessage, stepInfo.OnFailureMessage, (IDictionary<string, object>) stepInfo.Parameters)
      {
      }

      private GraphAction()
      {
      }

      public System.Type GraphType { get; }

      public System.Type EntityType { get; }

      public string Name { get; }

      public string DisplayName { get; }

      public string OnSuccessMsg { get; }

      public string OnFailureMsg { get; }

      public IReadOnlyDictionary<string, object> Arguments { get; }

      public PXGraph Processor { get; private set; }

      public object ProcessedEntity { get; set; }

      public bool Started { get; set; }

      public bool Completed { get; set; }

      public bool Failed { get; set; }

      private string Status
      {
        get
        {
          if (this.Failed)
            return "X";
          if (this.Completed)
            return "V";
          return !this.Started ? "-" : "~";
        }
      }

      public void ActualizeGraph(PXGraph graph)
      {
        if (CustomizedTypeManager.GetTypeNotCustomized(graph) != this.GraphType)
        {
          object uid = graph.UID;
          graph = PXGraph.CreateInstance(this.GraphType);
          graph.UID = uid;
        }
        graph.SelectTimeStamp();
        this.Processor = graph;
      }

      public override string ToString()
      {
        return $"({this.Status}) {this.GraphType.Name}.{this.Name}({this.EntityType.Name} entity) : [\"{this.DisplayName}\"]";
      }
    }

    internal class LogEntry
    {
      public Exception Exception { get; set; }

      public List<Tuple<string, object>> DocumentID { get; set; }

      public string Name { get; set; }

      public bool AutoRedirect { get; internal set; }

      public bool AutoExport { get; internal set; }
    }
  }
}
