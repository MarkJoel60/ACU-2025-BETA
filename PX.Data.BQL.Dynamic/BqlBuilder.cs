// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.BqlBuilder
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common;
using PX.Data.BQL.Dynamic.Builders;
using PX.Data.BQL.Dynamic.Statements;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic;

public class BqlBuilder
{
  private readonly List<JoinStatement> _joins = new List<JoinStatement>();
  private readonly List<ChainedAggregateStatement> _aggregates = new List<ChainedAggregateStatement>();
  private readonly List<FieldSortStatement> _sorts = new List<FieldSortStatement>();

  public static SelectBuilder SelectBuilder { get; } = new SelectBuilder();

  public static JoinBuilder JoinBuilder { get; } = new JoinBuilder();

  public static ConditionBuilder ConditionBuilder { get; } = new ConditionBuilder();

  public static AggregateBuilder AggregateBuilder { get; } = new AggregateBuilder();

  public static OrderByBuilder OrderByBuilder { get; } = new OrderByBuilder();

  public SelectStatement Select { get; private set; }

  public IEnumerable<JoinStatement> Joins => (IEnumerable<JoinStatement>) this._joins;

  public WhereStatement Where { get; private set; }

  public AggregateStatement Aggregate
  {
    get
    {
      return !this._aggregates.Any<ChainedAggregateStatement>() ? (AggregateStatement) null : new AggregateStatement(this._aggregates.ToArray());
    }
  }

  public IEnumerable<ChainedAggregateStatement> Aggregates
  {
    get => (IEnumerable<ChainedAggregateStatement>) this._aggregates;
  }

  public OrderByStatement OrderBy
  {
    get
    {
      return !this._sorts.Any<FieldSortStatement>() ? (OrderByStatement) null : new OrderByStatement(this._sorts.ToArray());
    }
  }

  public IEnumerable<FieldSortStatement> Sorts => (IEnumerable<FieldSortStatement>) this._sorts;

  public IEnumerable<Type> Tables
  {
    get
    {
      return (IEnumerable<Type>) ((IEnumerable<Type>) new Type[1]
      {
        this.Select.Table
      }).Concat<Type>(this._joins.Select<JoinStatement, Type>((Func<JoinStatement, Type>) (j => j.Table))).ToArray<Type>();
    }
  }

  internal BqlBuilder(SelectStatement select) => this.Select = BqlBuilder.VerifySelect(select);

  public static BqlBuilder FromCommand(BqlCommand command)
  {
    return BqlBuilder.FromCommand(command?.GetType());
  }

  public static BqlBuilder FromCommand<TCommand>() where TCommand : BqlCommand
  {
    return BqlBuilder.FromCommand(typeof (TCommand));
  }

  public static BqlBuilder FromCommand(Type commandType)
  {
    return SelectStatement.FromRawStatement(commandType);
  }

  public static BqlBuilder Append(Func<SelectBuilder, SelectStatement> select)
  {
    return BqlBuilder.Append(select(BqlBuilder.SelectBuilder));
  }

  public static BqlBuilder Append(SelectStatement select) => new BqlBuilder(select);

  public BqlBuilder Set(Func<SelectBuilder, SelectStatement> select)
  {
    return this.Set(select(BqlBuilder.SelectBuilder));
  }

  public BqlBuilder Set(SelectStatement select)
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it.Select = BqlBuilder.VerifySelect(select)));
  }

  public BqlBuilder SetWhere<TWhere>() where TWhere : IBqlWhere => this.SetWhere(typeof (TWhere));

  public BqlBuilder SetWhere(Type where) => this.Set(WhereStatement.FromRawStatement(where, true));

  public BqlBuilder Set(Func<ConditionBuilder, WhereStatement> condition)
  {
    return this.Set(condition(BqlBuilder.ConditionBuilder));
  }

  public BqlBuilder Set(WhereStatement condition)
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it.Where = condition));
  }

  public BqlBuilder RemoveWhere()
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it.Where = (WhereStatement) null));
  }

  public BqlBuilder AppendJoin<TJoin>() where TJoin : IBqlJoin => this.AppendJoin(typeof (TJoin));

  public BqlBuilder AppendJoin(Type join) => this.Append(JoinStatement.FromRawStatement(join));

  public BqlBuilder Append(Func<JoinBuilder, JoinStatement> join)
  {
    return this.Append(join(BqlBuilder.JoinBuilder));
  }

  public BqlBuilder Append(JoinStatement join)
  {
    return this.ApplyAppending<JoinStatement>(this._joins, join);
  }

  public BqlBuilder Append(
    Func<JoinBuilder, IEnumerable<JoinStatement>> joins)
  {
    return this.Append(joins(BqlBuilder.JoinBuilder));
  }

  public BqlBuilder Append(IEnumerable<JoinStatement> joins)
  {
    return this.ApplyAppending<JoinStatement>(this._joins, joins);
  }

  public BqlBuilder RemoveAllJoins()
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it._joins.Clear()));
  }

  public BqlBuilder RemoveJoin<TJoinedTable>() where TJoinedTable : IBqlTable
  {
    return this.RemoveJoin(typeof (TJoinedTable));
  }

  public BqlBuilder RemoveJoin(Type joinedTable)
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it._joins.Remove(it._joins.Find((Predicate<JoinStatement>) (j => j.Table == joinedTable)))));
  }

  public BqlBuilder AppendAggregate<TAggregate>() where TAggregate : IBqlAggregate
  {
    return this.AppendAggregate(typeof (TAggregate));
  }

  public BqlBuilder AppendAggregate(Type aggregate)
  {
    return this.Append(AggregateStatement.FromRawStatement(aggregate));
  }

  public BqlBuilder Append(
    Func<AggregateBuilder, ChainedAggregateStatement> aggregate)
  {
    return this.Append(aggregate(BqlBuilder.AggregateBuilder));
  }

  public BqlBuilder Append(ChainedAggregateStatement fieldAggregate)
  {
    return this.ApplyAppending<ChainedAggregateStatement>(this._aggregates, fieldAggregate);
  }

  public BqlBuilder Append(
    Func<AggregateBuilder, AggregateStatement> aggregate)
  {
    return this.Append(aggregate(BqlBuilder.AggregateBuilder));
  }

  public BqlBuilder Append(AggregateStatement aggregate)
  {
    return this.Append(aggregate.ChainedAggregateFunctions);
  }

  public BqlBuilder Append(
    Func<AggregateBuilder, IEnumerable<ChainedAggregateStatement>> aggregates)
  {
    return this.Append(aggregates(BqlBuilder.AggregateBuilder));
  }

  public BqlBuilder Append(IEnumerable<ChainedAggregateStatement> aggregates)
  {
    return this.ApplyAppending<ChainedAggregateStatement>(this._aggregates, aggregates);
  }

  public BqlBuilder RemoveAllAggregates()
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it._aggregates.Clear()));
  }

  public BqlBuilder RemoveAggregate<TAggregateFunction>() where TAggregateFunction : IBqlFunction
  {
    return this.RemoveAggregate(typeof (TAggregateFunction));
  }

  public BqlBuilder RemoveAggregate(Type aggregate)
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it._aggregates.Remove(it._aggregates.Find((Predicate<ChainedAggregateStatement>) (a => a.Eval() == aggregate)))));
  }

  public BqlBuilder AppendOrderBy<TOrderBy>() where TOrderBy : IBqlOrderBy
  {
    return this.AppendOrderBy(typeof (TOrderBy));
  }

  public BqlBuilder AppendOrderBy(Type orderBy)
  {
    return this.Append(OrderByStatement.FromRawStatement(orderBy));
  }

  public BqlBuilder Append(Func<OrderByBuilder, FieldSortStatement> sort)
  {
    return this.Append(sort(BqlBuilder.OrderByBuilder));
  }

  public BqlBuilder Append(FieldSortStatement sort)
  {
    return this.ApplyAppending<FieldSortStatement>(this._sorts, sort);
  }

  public BqlBuilder Append(Func<OrderByBuilder, OrderByStatement> sort)
  {
    return this.Append(sort(BqlBuilder.OrderByBuilder));
  }

  public BqlBuilder Append(OrderByStatement sort) => this.Append(sort.FieldSorts);

  public BqlBuilder Append(
    Func<OrderByBuilder, IEnumerable<FieldSortStatement>> sorts)
  {
    return this.Append(sorts(BqlBuilder.OrderByBuilder));
  }

  public BqlBuilder Append(IEnumerable<FieldSortStatement> sorts)
  {
    return this.ApplyAppending<FieldSortStatement>(this._sorts, sorts);
  }

  public BqlBuilder RemoveAllSorts()
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it._sorts.Clear()));
  }

  public BqlBuilder RemoveSort<TSortColumn>() where TSortColumn : IBqlSortColumn
  {
    return this.RemoveSort(typeof (TSortColumn));
  }

  public BqlBuilder RemoveSort(Type sortColumn)
  {
    return this.Apply<BqlBuilder>((Action<BqlBuilder>) (it => it._sorts.Remove(it._sorts.Find((Predicate<FieldSortStatement>) (s => s.Eval() == sortColumn)))));
  }

  public BqlBuilder AppendBinary<TAdditionalCondition>(Type joinedTable) where TAdditionalCondition : IBqlBinary
  {
    return this.AppendBinaryTo(joinedTable, typeof (TAdditionalCondition));
  }

  public BqlBuilder AppendBinary<TAdditionalCondition>() where TAdditionalCondition : IBqlBinary
  {
    return this.AppendBinary(typeof (TAdditionalCondition));
  }

  public BqlBuilder AppendBinary(Type additionalCondition)
  {
    return this.AppendBinaryTo((Type) null, additionalCondition);
  }

  public BqlBuilder AppendBinaryTo<TJoinedTable, TAdditionalCondition>()
    where TJoinedTable : IBqlTable
    where TAdditionalCondition : IBqlBinary
  {
    return this.AppendBinaryTo(typeof (TJoinedTable), typeof (TAdditionalCondition));
  }

  public BqlBuilder AppendBinaryTo<TJoinedTable>(Type additionalCondition) where TJoinedTable : IBqlTable
  {
    return this.AppendBinaryTo(typeof (TJoinedTable), additionalCondition);
  }

  public BqlBuilder AppendBinaryTo(Type joinedTable, Type additionalCondition)
  {
    return ((IEnumerable<BinaryStatement>) BinaryStatement.FromRawStatement(additionalCondition)).Aggregate<BinaryStatement, BqlBuilder>(this, (Func<BqlBuilder, BinaryStatement, BqlBuilder>) ((it, bs) => it.Append(joinedTable, bs)));
  }

  public BqlBuilder Append<TJoinedTable>(
    Func<ConditionBuilder, BinaryStatement> additionalCondition)
    where TJoinedTable : IBqlTable
  {
    return this.Append(typeof (TJoinedTable), additionalCondition);
  }

  public BqlBuilder Append(
    Func<ConditionBuilder, BinaryStatement> additionalCondition)
  {
    return this.Append((Type) null, additionalCondition);
  }

  public BqlBuilder Append(
    Type joinedTable,
    Func<ConditionBuilder, BinaryStatement> additionalCondition)
  {
    return this.Append(joinedTable, additionalCondition(BqlBuilder.ConditionBuilder));
  }

  public BqlBuilder Append<TJoinedTable>(BinaryStatement additionalCondition) where TJoinedTable : IBqlTable
  {
    return this.Append(typeof (TJoinedTable), additionalCondition);
  }

  public BqlBuilder Append(BinaryStatement additionalCondition)
  {
    return this.Append((Type) null, additionalCondition);
  }

  public BqlBuilder Append(Type joinedTable, BinaryStatement additionalCondition)
  {
    return this.AppendBinaryImpl(joinedTable, additionalCondition);
  }

  public BqlCommand ToBqlCommand()
  {
    return BqlCommand.CreateInstance(new Type[1]
    {
      this.ToBqlCommandType()
    });
  }

  public Type ToBqlCommandType()
  {
    if (this.Select == null)
      throw new InvalidOperationException();
    return this.Select.Eval(((IEnumerable<IChainedStatement>) this._joins).EvalToChain(), this.Where?.Eval(), this.Aggregate?.Eval(), this.OrderBy?.Eval());
  }

  private BqlBuilder ApplyAppending<T>(List<T> list, IEnumerable<T> elements)
  {
    list.AddRange(elements);
    return this;
  }

  private BqlBuilder ApplyAppending<T>(List<T> list, T element)
  {
    list.Add(element);
    return this;
  }

  private BqlBuilder AppendBinaryImpl(Type joinedTable, BinaryStatement additionalCondition)
  {
    if (joinedTable == (Type) null || this.Select.Table == joinedTable)
    {
      this.Where = this.Where != null ? this.Where.AppendBinary(additionalCondition) : throw new InvalidOperationException();
      return this;
    }
    JoinStatement joinStatement1 = this._joins.FirstOrDefault<JoinStatement>((Func<JoinStatement, bool>) (j => j.Table == joinedTable));
    int index = joinStatement1 != null ? this._joins.IndexOf(joinStatement1) : throw new InvalidOperationException();
    this._joins.RemoveAt(index);
    JoinStatement joinStatement2 = joinStatement1.AppendBinary(additionalCondition);
    this._joins.Insert(index, joinStatement2);
    return this;
  }

  private static SelectStatement VerifySelect(SelectStatement select)
  {
    return select != null ? select : throw new ArgumentNullException(nameof (select));
  }
}
