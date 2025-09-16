// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.AggregateCalculators.AggregateValidation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data.BQL.AggregateCalculators;

internal abstract class AggregateValidation
{
  protected ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState> _StateByGraph;

  public static void IgnoreDeltas(PXCache cache, object parentRow)
  {
    AggregateValidation.ValidationGraphState validationGraphState;
    if (!PXContext.EnsureSlot<ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>>("PXFormula.ValidationThreadState", (Func<ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>>) (() => new ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>())).TryGetValue(cache.Graph, out validationGraphState))
      return;
    System.Type parentType = cache.GetItemType();
    foreach (IEnumerable<AggregateValidation> source in validationGraphState.Validations.Values)
      EnumerableExtensions.ForEach<AggregateValidation>(source.Where<AggregateValidation>((Func<AggregateValidation, bool>) (_ => _.ParentType == parentType)), (System.Action<AggregateValidation>) (_ => _.Ignore(cache.Graph, parentRow)));
  }

  public static void IgnoreDeltas(PXGraph graph, System.Type parentFieldType, object parentRow)
  {
    AggregateValidation.ValidationGraphState validationGraphState;
    List<AggregateValidation> source;
    if (!PXContext.EnsureSlot<ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>>("PXFormula.ValidationThreadState", (Func<ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>>) (() => new ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>())).TryGetValue(graph, out validationGraphState) || !validationGraphState.Validations.TryGetValue(parentFieldType, out source))
      return;
    source.First<AggregateValidation>().Ignore(graph, parentRow);
  }

  public static AggregateValidation CreateValidation(System.Type parentField)
  {
    AggregateValidation validation;
    switch (System.Type.GetTypeCode(parentField))
    {
      case TypeCode.Int16:
        validation = (AggregateValidation) new AggregateValidation<short>(((short) 0, (short) 1), (Func<short, short, short>) ((a, b) => (short) ((int) a + (int) b)), (Func<short, short, short>) ((a, b) => (short) ((int) a - (int) b)), (Func<short, bool>) (diff => diff != (short) 0));
        break;
      case TypeCode.Int32:
        validation = (AggregateValidation) new AggregateValidation<int>((0, 1), (Func<int, int, int>) ((a, b) => a + b), (Func<int, int, int>) ((a, b) => a - b), (Func<int, bool>) (diff => diff != 0));
        break;
      case TypeCode.Int64:
        validation = (AggregateValidation) new AggregateValidation<long>((0L, 1L), (Func<long, long, long>) ((a, b) => a + b), (Func<long, long, long>) ((a, b) => a - b), (Func<long, bool>) (diff => diff != 0L));
        break;
      case TypeCode.Decimal:
        validation = (AggregateValidation) new AggregateValidation<Decimal>((0M, 1M), (Func<Decimal, Decimal, Decimal>) ((a, b) => a + b), (Func<Decimal, Decimal, Decimal>) ((a, b) => a - b), (Func<Decimal, bool>) (diff => System.Math.Abs(diff) > 0.005M));
        break;
      default:
        validation = (AggregateValidation) null;
        break;
    }
    return validation;
  }

  public TypeCode TypeCode { get; }

  public abstract System.Type ChildType { get; }

  public abstract System.Type ParentType { get; }

  public AggregateValidation(TypeCode typeCode) => this.TypeCode = typeCode;

  public abstract void Initialize(PXCache cache, System.Type parentFieldType, string fieldName);

  public abstract void Initialize(PXCache cache, System.Type parentFieldType, IBqlCreator formula);

  protected abstract void Ignore(PXGraph graph, object parentRow);

  protected ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState> StateByGraph
  {
    get
    {
      return this._StateByGraph ?? (this._StateByGraph = PXContext.EnsureSlot<ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>>("PXFormula.ValidationThreadState", (Func<ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>>) (() => new ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>())));
    }
  }

  protected class ValidationGraphState
  {
    public Dictionary<System.Type, IDictionary> Deltas = new Dictionary<System.Type, IDictionary>();
    public Dictionary<System.Type, List<AggregateValidation>> Validations = new Dictionary<System.Type, List<AggregateValidation>>();
    public AggregateValidation.DelegateList OnBeforeCommit = new AggregateValidation.DelegateList();
    public AggregateValidation.DelegateList OnBeforeUnload = new AggregateValidation.DelegateList();
    public AggregateValidation.DelegateList OnRequestCompleted = new AggregateValidation.DelegateList();

    public ValidationGraphState(PXGraph graph)
    {
      graph.OnBeforeCommit += new System.Action<PXGraph>(this.OnBeforeCommit.Execute);
      graph.BeforeUnload += new PXGraphBeforeUnloadDelegate(this.OnBeforeUnload.Execute);
      graph.OnRequestCompleted += new System.Action<PXGraph>(this.OnRequestCompleted.Execute);
    }
  }

  protected class DelegateList
  {
    private readonly List<System.Action<PXGraph>> _handlers = new List<System.Action<PXGraph>>();

    public void AddBefore(System.Action<PXGraph> del) => this._handlers.Insert(0, del);

    public void AddAfter(System.Action<PXGraph> del) => this._handlers.Add(del);

    public void Execute(PXGraph graph)
    {
      foreach (System.Action<PXGraph> action in this._handlers.ToList<System.Action<PXGraph>>())
        action(graph);
    }
  }
}
