// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.MappedSelect`6
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.BQL;

/// <summary>The class that maps the results of a sub select operation from the <tt>TMappedTable</tt> parameter into the fields of the DAC class represented by the <tt>MappedToTable</tt> parameter.</summary>
/// <remarks>
/// The primary purpose of this BQL command is to facilitate the use of the <see cref="T:PX.Data.BQL.Union`2" /> and <see cref="T:PX.Data.BQL.UnionAll`2" /> operators in BQL.
/// This command is used to specify how a <tt>Union</tt> or <tt>UnionAll</tt> operation will be performed on a set of DAC classes representing the relevant database tables.
/// This command also specifies the shared DAC class in which the data of such an operation will be stored in.
/// </remarks>
/// <typeparam name="MappedToTable">The DAC class in which the result of the operation is stored. This DAC class does not necessarily need to represent an actual database table.</typeparam>
/// <typeparam name="TMappedTable">The class that contains the results of a sub select operation.</typeparam>
/// <typeparam name="TJoin">A BQL <tt>Join</tt> clause.</typeparam>
/// <typeparam name="TWhere">A BQL <tt>Where</tt> clause.</typeparam>
/// <typeparam name="TAggregate">A BQL aggregate expression.</typeparam>
/// <typeparam name="TOrderBy">A BQL <tt>OrderBy</tt> clause.</typeparam>
public class MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, TAggregate, TOrderBy> : 
  SelectBase<MappedToTable, TJoin, TWhere, TAggregate, TOrderBy>,
  IBqlUnionSelect
  where MappedToTable : IBqlTable, new()
  where TMappedTable : IMappedTable, new()
  where TJoin : IBqlJoin, new()
  where TWhere : IBqlWhere, new()
  where TAggregate : IBqlAggregate, new()
  where TOrderBy : IBqlOrderBy, new()
{
  private TMappedTable _mappedTable;
  private static ConcurrentDictionary<System.Type, Func<BqlCommand>> _dict = new ConcurrentDictionary<System.Type, Func<BqlCommand>>();

  protected IMappedTable ensureMappedTable()
  {
    if ((object) this._mappedTable != null)
      return (IMappedTable) this._mappedTable;
    return !(typeof (TMappedTable) == typeof (BqlNone)) ? (IMappedTable) (this._mappedTable = new TMappedTable()) : (IMappedTable) null;
  }

  protected override void BuildQueryFrom(
    PXGraph graph,
    Query query,
    System.Type mainTableType,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this.ensureMappedTable() == null)
      return;
    query.From(this._mappedTable.GetQuery(query, graph, info, selection).As(mainTableType.Name));
  }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph != null && info.BuildExpression)
    {
      PXCache cach = graph.Caches[typeof (MappedToTable)];
      cach.BypassCalced = cach.BqlSelect != null;
    }
    return base.GetQueryInternal(graph, info, selection);
  }

  public BqlCommand AddNewUnion(System.Type newUnion)
  {
    if (newUnion == (System.Type) null || typeof (BqlNone) == newUnion)
      return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, TAggregate, TOrderBy>();
    return (BqlCommand) Activator.CreateInstance(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, TAggregate, TOrderBy>.CreateNewUnionType(newUnion), typeof (TWhere), typeof (TAggregate), typeof (TOrderBy)));
  }

  protected static System.Type CreateNewUnionType(System.Type newUnion)
  {
    List<System.Type> list = ((IEnumerable<System.Type>) BqlCommand.Decompose(typeof (TMappedTable))).ToList<System.Type>();
    var data = list.Select((type, i) => new
    {
      type = type,
      i = i
    }).Last(c => typeof (IBqlUnion).IsAssignableFrom(c.type));
    if (data.type == typeof (BqlNone))
      list[0] = newUnion;
    else if (data.type == typeof (Union<,>))
    {
      list[data.i] = typeof (Union<,>);
      list.Add(newUnion);
    }
    else if (data.type == typeof (UnionAll<,>))
    {
      list[data.i] = typeof (UnionAll<,>);
      list.Add(newUnion);
    }
    return BqlCommand.Compose(list.ToArray());
  }

  public override BqlCommand AddNewJoin(System.Type newJoin)
  {
    if (newJoin == (System.Type) null || typeof (BqlNone) == newJoin)
      return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, TAggregate, TOrderBy>();
    return (BqlCommand) Activator.CreateInstance(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), SelectBase<MappedToTable, TJoin, TWhere, TAggregate, TOrderBy>.CreateNewJoinType(newJoin), typeof (TWhere), typeof (TAggregate), typeof (TOrderBy)));
  }

  /// <exclude />
  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, newAggregate, TOrderBy>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), typeof (TJoin), typeof (TWhere), newAggregate, typeof (TOrderBy)));
  }

  /// <summary>Constructs the command out of the current one replacing the where clause.</summary>
  /// <typeparam name="newWhere">New restriction</typeparam>
  /// <returns>New command with the where clause replaced</returns>
  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, newWhere, TAggregate, TOrderBy>();
  }

  /// <summary>
  /// Constructs the command out of the current one replacing the where clause.
  /// </summary>
  /// <param name="newWhere">New restriction</param>
  /// <returns>New command with the where clause replaced</returns>
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), typeof (TJoin), newWhere, typeof (TAggregate), typeof (TOrderBy)));
  }

  /// <summary>
  /// Constructs the command out of the current one appending the where clause with the And operator.
  /// </summary>
  /// <typeparam name="where">Additional restriction</typeparam>
  /// <returns>New command created</returns>
  public override BqlCommand WhereAnd<TWhere1>()
  {
    return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, Where2<TWhere, PX.Data.And<TWhere1>>, TAggregate, TOrderBy>();
  }

  /// <summary>
  /// Constructs the command out of the current one appending the where clause with the And operator.
  /// </summary>
  /// <param name="where">Additional restriction</param>
  /// <returns>New command created</returns>
  public override BqlCommand WhereAnd(System.Type where)
  {
    if (WebConfig.EnablePageOpenOptimizations)
    {
      Func<BqlCommand> func;
      if (!MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, TAggregate, TOrderBy>._dict.TryGetValue(where, out func))
      {
        func = ((Expression<Func<BqlCommand>>) (() => Expression.New(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), typeof (TJoin), typeof (Where2<,>).MakeGenericType(typeof (PXSetup<>.Where<>), typeof (PX.Data.And<>).MakeGenericType(where)), typeof (TAggregate), typeof (TOrderBy))))).Compile();
        MappedSelect<MappedToTable, TMappedTable, TJoin, TWhere, TAggregate, TOrderBy>._dict.TryAdd(where, func);
      }
      return func();
    }
    return (BqlCommand) Activator.CreateInstance(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), typeof (TJoin), typeof (Where2<,>).MakeGenericType(typeof (PXSetup<>.Where<>), typeof (PX.Data.And<>).MakeGenericType(where)), typeof (TAggregate), typeof (TOrderBy)));
  }

  /// <summary>
  /// Constructs the command out of the current one appending the where clause with the Or operator.
  /// </summary>
  /// <typeparam name="where">Additional restriction</typeparam>
  /// <returns>New command created</returns>
  public override BqlCommand WhereOr<TWhere1>()
  {
    return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, Where2<TWhere, PX.Data.Or<TWhere1>>, TAggregate, TOrderBy>();
  }

  /// <summary>
  /// Constructs the command out of the current one appending the where clause with the Or operator.
  /// </summary>
  /// <param name="where">Additional restriction</param>
  /// <returns>New command created</returns>
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), typeof (TJoin), typeof (Where2<,>).MakeGenericType(typeof (TWhere), typeof (PX.Data.Or<>).MakeGenericType(where)), typeof (TAggregate), typeof (TOrderBy)));
  }

  /// <summary>
  /// Constructs the command out of the current one inversing the where clause.
  /// </summary>
  /// <returns>New command created</returns>
  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, Where<Not<TWhere>>, TAggregate, TOrderBy>();
  }

  /// <summary>
  /// Constructs the command out of the current one replacing the sort columns list.
  /// </summary>
  /// <typeparam name="newOrderBy">Sort columns</typeparam>
  /// <returns>New command created</returns>
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, Where<Not<TWhere>>, TAggregate, newOrderBy>();
  }

  /// <summary>
  /// Constructs the command out of the current one replacing the sort columns list.
  /// </summary>
  /// <param name="newOrderBy">Sort columns</param>
  /// <returns>New command created</returns>
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new MappedSelect<MappedToTable, TMappedTable, TJoin, Where<Not<TWhere>>, TAggregate, BqlNone>();
    return (BqlCommand) Activator.CreateInstance(typeof (MappedSelect<,,,,,>).MakeGenericType(typeof (MappedToTable), typeof (TMappedTable), typeof (TJoin), typeof (TWhere), typeof (TAggregate), newOrderBy));
  }
}
