// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionGrouping`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

/// <summary>
/// For every row return object that implements <see cref="T:System.Linq.IGrouping`2" /> interface.
/// Only when client enumerates group, it performs SQL-query to retrieve data for group.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TElement"></typeparam>
internal class ProjectionGrouping<TKey, TElement> : ProjectionItem
{
  private readonly Query _groupDataQuery;
  private readonly List<SQLExpression> _groupByKeys;
  private readonly ProjectionItem _groupKeyProjection;
  private readonly PXGraph _graph;
  private readonly SQLinqBqlCommandInfo _info;

  /// Constructor is used via reflection in <see cref="!:SQLinqQueryModelVisitor" />
  public ProjectionGrouping(
    Query groupDataQuery,
    List<SQLExpression> groupByKeys,
    ProjectionItem groupKeyProjection,
    PXGraph graph,
    SQLinqBqlCommandInfo info)
  {
    this._groupDataQuery = (Query) groupDataQuery.Duplicate();
    this._groupByKeys = groupByKeys;
    this._groupKeyProjection = groupKeyProjection;
    this._graph = graph;
    this._info = info;
  }

  public override string ToString() => "Grouping";

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    MergeCacheContext mergeCacheContext = context;
    if ((mergeCacheContext != null ? (mergeCacheContext.CreateNew ? 1 : 0) : 0) != 0)
      return (object) null;
    Query query = (Query) this._groupDataQuery.Duplicate();
    query.GetGroupBy().Clear();
    SQLExpression r1 = (SQLExpression) null;
    for (int index = 0; index < data.FieldCount; ++index)
    {
      SQLExpression groupByKey = this._groupByKeys[index];
      object r2 = data.GetValue(position++);
      SQLExpression r3 = r2 != null ? groupByKey.EQ(r2) : groupByKey.IsNull();
      r1 = r1?.And(r3) ?? r3;
    }
    SQLExpression w = query.GetWhere()?.And(r1) ?? r1;
    query.Where(w);
    ProjectionGrouping<TKey, TElement>.DelagateEnumerable<TElement> groupData = new ProjectionGrouping<TKey, TElement>.DelagateEnumerable<TElement>((Func<IEnumerable<TElement>>) (() => SQLinqExecutor.ExecuteCollection<TElement>(query, this._info, this._graph, context?.LinqExecutor)));
    int position1 = 0;
    return (object) new ProjectionGrouping<TKey, TElement>.Grouping<TKey, TElement>((TKey) this._groupKeyProjection.GetValue(data, ref position1, context), (IEnumerable<TElement>) groupData);
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    ProjectionGrouping<TKey, TElement>.Grouping<TKey, TElement> groupData = (ProjectionGrouping<TKey, TElement>.Grouping<TKey, TElement>) value;
    return (object) new ProjectionGrouping<TKey, TElement>.Grouping<TKey, TElement>((TKey) this._groupKeyProjection.CloneValue((object) groupData.Key, context), (IEnumerable<TElement>) groupData);
  }

  internal override object Transform(
    object value,
    Func<System.Type, object, bool> predicate,
    Func<object, object> map)
  {
    if (predicate(this.type_, value))
      return map(value);
    ProjectionGrouping<TKey, TElement>.Grouping<TKey, TElement> source = (ProjectionGrouping<TKey, TElement>.Grouping<TKey, TElement>) value;
    TKey key = (TKey) this._groupKeyProjection.Transform((object) source.Key, predicate, map);
    ProjectionItem itemsProjection = this._groupDataQuery.Projection();
    IEnumerable<TElement> groupData = itemsProjection != null ? source.Select<TElement, TElement>((Func<TElement, TElement>) (i => (TElement) itemsProjection.Transform((object) i, predicate, map))) : (IEnumerable<TElement>) source;
    return (object) new ProjectionGrouping<TKey, TElement>.Grouping<TKey, TElement>(key, groupData);
  }

  private class DelagateEnumerable<T> : IEnumerable<T>, IEnumerable
  {
    private readonly Func<IEnumerable<T>> _dataGetter;

    public DelagateEnumerable(Func<IEnumerable<T>> dataGetter) => this._dataGetter = dataGetter;

    public IEnumerator<T> GetEnumerator() => this._dataGetter().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }

  private class Grouping<TKey2, TElement2> : 
    IGrouping<TKey2, TElement2>,
    IEnumerable<TElement2>,
    IEnumerable,
    IGroupingResult
  {
    private IEnumerable<TElement2> _groupData;

    public Grouping(TKey2 key, IEnumerable<TElement2> groupData)
    {
      this._groupData = groupData;
      this.Key = key;
    }

    public IEnumerator<TElement2> GetEnumerator() => this._groupData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public TKey2 Key { get; private set; }

    public void MaterializeGroup()
    {
      if (!(this._groupData is ProjectionGrouping<TKey, TElement>.DelagateEnumerable<TElement2> groupData))
        return;
      this._groupData = (IEnumerable<TElement2>) groupData.ToList<TElement2>();
    }
  }
}
