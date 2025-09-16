// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.AggregateCalculators.AggregateValidation`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data.BQL.AggregateCalculators;

internal class AggregateValidation<TDataType> : AggregateValidation where TDataType : struct, IEquatable<TDataType>
{
  private readonly (TDataType Zero, TDataType One) Basis;
  private readonly Func<TDataType, TDataType, TDataType> Add;
  private readonly Func<TDataType, TDataType, TDataType> Subtract;
  private readonly Func<TDataType, bool> IsSignificantDiscrepancy;
  protected System.Type _ParentType;
  protected System.Type _ChildType;
  protected string _ParentFieldName;
  protected string _ChildFieldName;
  protected System.Type _ParentFieldType;
  protected Func<PXCache, object, object> _GetValueDelegate;
  protected bool _IgnoreDeletedRows = true;
  private readonly Dictionary<object, object> _Originals = new Dictionary<object, object>();

  public override System.Type ChildType => this._ChildType;

  public override System.Type ParentType => this._ParentType;

  public AggregateValidation(
    (TDataType Zero, TDataType One) basis,
    Func<TDataType, TDataType, TDataType> add,
    Func<TDataType, TDataType, TDataType> subtract,
    Func<TDataType, bool> isSignificantDiscrepancy)
    : base(System.Type.GetTypeCode(typeof (TDataType)))
  {
    this.Basis = basis;
    this.Add = add ?? throw new ArgumentNullException(nameof (add));
    this.Subtract = subtract ?? throw new ArgumentNullException(nameof (subtract));
    this.IsSignificantDiscrepancy = isSignificantDiscrepancy ?? throw new ArgumentNullException(nameof (isSignificantDiscrepancy));
  }

  public override void Initialize(PXCache cache1, System.Type parentFieldType, IBqlCreator formula)
  {
    if (formula is Const<One>)
    {
      this._GetValueDelegate = (Func<PXCache, object, object>) ((cache2, row) => (object) (row != null ? this.Basis.One : this.Basis.Zero));
      this._IgnoreDeletedRows = false;
    }
    else
    {
      if (formula == null)
        throw new ArgumentNullException(nameof (formula));
      this._GetValueDelegate = (Func<PXCache, object, object>) ((cache3, row) =>
      {
        bool? result = new bool?();
        object obj = (object) null;
        BqlFormula.Verify(cache3, row, formula, ref result, ref obj);
        return obj;
      });
    }
    this.Initialize(cache1, parentFieldType);
  }

  public override void Initialize(PXCache cache1, System.Type parentFieldType, string fieldName)
  {
    if (fieldName == null)
      throw new ArgumentNullException(nameof (fieldName));
    this._GetValueDelegate = (Func<PXCache, object, object>) ((cache2, row) => cache2.GetValue(row, fieldName));
    this.Initialize(cache1, parentFieldType);
  }

  private void Initialize(PXCache cache, System.Type parentFieldType)
  {
    this._ChildType = cache.GetItemType();
    this._ParentType = BqlCommand.GetItemType(parentFieldType);
    this._ParentFieldName = parentFieldType.Name;
    this._ParentFieldType = parentFieldType;
    AggregateValidation.ValidationGraphState validationGraphState = this.StateByGraph.GetValue(cache.Graph, (ConditionalWeakTable<PXGraph, AggregateValidation.ValidationGraphState>.CreateValueCallback) (graph => new AggregateValidation.ValidationGraphState(graph)));
    if (validationGraphState.Validations.Ensure<System.Type, List<AggregateValidation>>(this._ParentFieldType, (Func<List<AggregateValidation>>) (() => new List<AggregateValidation>())).Find((Predicate<AggregateValidation>) (a => ((AggregateValidation<TDataType>) a)._ChildType == this._ChildType)) != null)
      return;
    cache.Graph.RowPersisted.AddHandler(this._ParentType, new PXRowPersisted(this.OnRowPersistedMemorizeOrPurgeOriginals));
    cache.Graph.RowPersisted.AddHandler(this._ChildType, new PXRowPersisted(this.OnRowPersistedMemorizeOrPurgeOriginals));
    validationGraphState.OnBeforeCommit.AddBefore(new System.Action<PXGraph>(this.CalculateDataConsistency));
    validationGraphState.OnBeforeCommit.AddAfter(new System.Action<PXGraph>(this.ValidateDataConsistency));
    validationGraphState.OnBeforeUnload.AddBefore(new System.Action<PXGraph>(this.CalculateDataConsistencyOnUnload));
    validationGraphState.OnBeforeUnload.AddAfter(new System.Action<PXGraph>(this.ValidateDataConsistencyOnUnload));
    validationGraphState.OnRequestCompleted.AddBefore(new System.Action<PXGraph>(this.CalculateDataConsistencyOnUnload));
    validationGraphState.OnRequestCompleted.AddAfter(new System.Action<PXGraph>(this.ValidateDataConsistencyOnUnload));
    validationGraphState.Validations.Ensure<System.Type, List<AggregateValidation>>(this._ParentFieldType, (Func<List<AggregateValidation>>) (() => new List<AggregateValidation>())).Add((AggregateValidation) this);
  }

  protected override void Ignore(PXGraph graph, object parentRow)
  {
    AggregateValidation.ValidationGraphState validationGraphState;
    if (!this.StateByGraph.TryGetValue(graph, out validationGraphState))
      return;
    object obj = graph.Caches[this._ParentType].Locate(parentRow);
    ((Dictionary<object, (bool, TDataType, TDataType)>) validationGraphState.Deltas.Ensure<System.Type, IDictionary>(this._ParentFieldType, (Func<IDictionary>) (() => (IDictionary) new Dictionary<object, (bool, TDataType, TDataType)>())))[obj ?? parentRow] = (true, this.Basis.Zero, this.Basis.Zero);
  }

  private void OnRowPersistedMemorizeOrPurgeOriginals(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == PXTranStatus.Open)
    {
      this._Originals[e.Row] = e.Operation == PXDBOperation.Insert ? (object) null : cache.GetOriginal(e.Row);
    }
    else
    {
      if (!EnumerableExtensions.IsIn<PXTranStatus>(e.TranStatus, PXTranStatus.Completed, PXTranStatus.Aborted))
        return;
      this._Originals.Remove(e.Row);
    }
  }

  private bool ShouldPerformValidationOnUnload(PXGraph graph)
  {
    return !PXLongOperation.Exists(graph) && !PXLongOperation.IsLongRunOperation;
  }

  private void CalculateDataConsistencyOnUnload(PXGraph graph)
  {
    if (!this.ShouldPerformValidationOnUnload(graph))
      return;
    this.CalculateDataConsistency(graph);
  }

  private void CalculateDataConsistency(PXGraph graph)
  {
    try
    {
      if (!graph.Views.Caches.Contains(this._ChildType) || !graph.Views.Caches.Contains(this._ParentType))
        return;
      this.CalculateDeltas(graph);
    }
    catch
    {
    }
  }

  private void CalculateDeltas(PXGraph graph)
  {
    AggregateValidation.ValidationGraphState validationGraphState;
    if (!this.StateByGraph.TryGetValue(graph, out validationGraphState))
      return;
    Dictionary<object, (bool, TDataType, TDataType)> deltas = (Dictionary<object, (bool, TDataType, TDataType)>) validationGraphState.Deltas.Ensure<System.Type, IDictionary>(this._ParentFieldType, (Func<IDictionary>) (() => (IDictionary) new Dictionary<object, (bool, TDataType, TDataType)>()));
    this.CalculateParentDelta(graph, deltas);
    this.CalculateChildrenDelta(graph, deltas);
  }

  private void CalculateParentDelta(
    PXGraph graph,
    Dictionary<object, (bool IgnoreFlag, TDataType ParentValue, TDataType ChildValue)> deltas)
  {
    PXCache parentCache = graph.Caches[this._ParentType];
    foreach (object obj in parentCache.Cached.Cast<object>().Where<object>((Func<object, bool>) (p => HasProperStatus(parentCache, p))))
    {
      if (!deltas.ContainsKey(obj))
        deltas[obj] = (false, this.Subtract(parentCache.GetStatus(obj) == PXEntryStatus.Deleted ? this.Basis.Zero : GetValue(obj), GetValue(this.GetOriginal(parentCache, obj))), this.Basis.Zero);
    }

    bool HasProperStatus(PXCache cache, object row)
    {
      return !this._IgnoreDeletedRows ? EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(row), PXEntryStatus.Inserted, PXEntryStatus.Updated, PXEntryStatus.Deleted) : EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(row), PXEntryStatus.Inserted, PXEntryStatus.Updated);
    }

    TDataType GetValue(object row)
    {
      return ((TDataType?) parentCache.GetValue(row, this._ParentFieldName)).GetValueOrDefault();
    }
  }

  private void CalculateChildrenDelta(
    PXGraph graph,
    Dictionary<object, (bool IgnoreFlag, TDataType ParentValue, TDataType ChildValue)> deltas)
  {
    PXCache cach = graph.Caches[this._ParentType];
    PXCache childCache = graph.Caches[this._ChildType];
    foreach (object obj in childCache.Cached.Cast<object>().Where<object>((Func<object, bool>) (c => HasProperStatus(childCache, c))))
    {
      object parent = this.GetParent(childCache, obj, this._ParentType);
      if (parent != null)
      {
        (bool, TDataType, TDataType) valueTuple;
        if (!deltas.TryGetValue(parent, out valueTuple))
          valueTuple = (cach.GetStatus(parent) == PXEntryStatus.Deleted, this.Basis.Zero, this.Basis.Zero);
        deltas[parent] = (valueTuple.Item1, valueTuple.Item2, this.Add(valueTuple.Item3, childCache.GetStatus(obj) == PXEntryStatus.Deleted ? this.Basis.Zero : GetValue(obj)));
      }
      object original = this.GetOriginal(childCache, obj);
      if (original != null)
      {
        if (!PXParentAttribute.IsParentReferenceTheSame(childCache, obj, original, this._ParentType, out bool _))
          parent = this.GetParent(childCache, original, this._ParentType);
        (bool IgnoreFlag, TDataType ParentValue, TDataType ChildValue) tuple;
        if (parent != null && deltas.TryGetValue(parent, out tuple))
          deltas[parent] = (tuple.IgnoreFlag, tuple.ParentValue, this.Subtract(tuple.ChildValue, GetValue(original)));
      }
    }

    static bool HasProperStatus(PXCache cache, object row)
    {
      return EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(row), PXEntryStatus.Inserted, PXEntryStatus.Updated, PXEntryStatus.Deleted);
    }

    TDataType GetValue(object row)
    {
      return ((TDataType?) this._GetValueDelegate(childCache, row)).GetValueOrDefault();
    }
  }

  private void ValidateDataConsistencyOnUnload(PXGraph graph)
  {
    if (!this.ShouldPerformValidationOnUnload(graph))
      return;
    this.ValidateDataConsistency(graph);
  }

  private void ValidateDataConsistency(PXGraph graph)
  {
    try
    {
      if (!graph.Views.Caches.Contains(this._ChildType) || !graph.Views.Caches.Contains(this._ParentType))
        return;
      this.VerifyDeltas(graph);
    }
    catch
    {
    }
  }

  private void VerifyDeltas(PXGraph graph)
  {
    AggregateValidation.ValidationGraphState validationGraphState;
    if (!this.StateByGraph.TryGetValue(graph, out validationGraphState))
      return;
    IDictionary dictionary;
    if (!validationGraphState.Deltas.TryGetValue(this._ParentFieldType, out dictionary))
      return;
    try
    {
      foreach (KeyValuePair<object, (bool, TDataType, TDataType)> keyValuePair in (IEnumerable) dictionary)
      {
        TDataType difference = this.Subtract(keyValuePair.Value.Item2, keyValuePair.Value.Item3);
        if (!keyValuePair.Value.Item1 && this.IsSignificantDiscrepancy(difference))
          this.SetDataConsistencyIssue(graph, keyValuePair.Key, difference);
      }
      validationGraphState.Deltas.Remove(this._ParentFieldType);
    }
    finally
    {
      this._Originals.Clear();
    }
  }

  private void SetDataConsistencyIssue(PXGraph graph, object row, TDataType difference)
  {
    AggregateValidation.ValidationGraphState validationGraphState;
    List<AggregateValidation> source;
    if (!this.StateByGraph.TryGetValue(graph, out validationGraphState) || !validationGraphState.Validations.TryGetValue(this._ParentFieldType, out source))
      throw new PXArgumentException(nameof (graph));
    string str = string.Join(";", source.Select<AggregateValidation, string>((Func<AggregateValidation, string>) (_ => _.ChildType.Name)));
    string fullName = this._ParentFieldType.FullName;
    string name = "Aggregate Validation: " + fullName;
    string diagnosticDetails = $"Difference: {difference}, Parent: {fullName}, Children: {str}";
    graph.SetDataConsistencyIssue(name, diagnosticDetails, false);
  }

  private object GetOriginal(PXCache cache, object row)
  {
    object original;
    if (this._Originals.TryGetValue(row, out original))
      return original;
    return cache.GetStatus(row) != PXEntryStatus.Inserted ? cache.GetOriginal(row) : (object) null;
  }

  private object GetParent(PXCache cache, object child, System.Type parentType)
  {
    return PXParentAttribute.FindParent(cache, child, parentType) ?? PXParentAttribute.SelectParent(cache, child, parentType);
  }
}
