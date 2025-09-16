// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.AggregateStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class AggregateStatement : IStatement
{
  public IEnumerable<ChainedAggregateStatement> ChainedAggregateFunctions { get; }

  internal AggregateStatement(ChainedAggregateStatement field, ChainedAggregateStatement[] fields)
    : this(((IEnumerable<ChainedAggregateStatement>) new ChainedAggregateStatement[1]
    {
      field
    }).Concat<ChainedAggregateStatement>((IEnumerable<ChainedAggregateStatement>) fields).ToArray<ChainedAggregateStatement>())
  {
  }

  internal AggregateStatement(ChainedAggregateStatement[] fields)
  {
    this.ChainedAggregateFunctions = (IEnumerable<ChainedAggregateStatement>) ((IEnumerable<ChainedAggregateStatement>) fields).ToArray<ChainedAggregateStatement>();
  }

  internal static AggregateStatement FromRawStatement(Type rawStatement)
  {
    if (rawStatement == (Type) null)
      throw new ArgumentNullException(nameof (rawStatement));
    if (!rawStatement.IsGenericType)
      throw new ArgumentException();
    Type chain;
    if (typeof (Aggregate<>) == rawStatement.GetGenericTypeDefinition())
      chain = ((IEnumerable<Type>) rawStatement.GetGenericArguments()).First<Type>();
    else
      chain = typeof (IBqlFunction).IsAssignableFrom(rawStatement) ? rawStatement : throw new NotSupportedException();
    List<ChainedAggregateStatement> aggregateStatementList = new List<ChainedAggregateStatement>();
    while (chain != (Type) null)
    {
      ChainedAggregateStatement aggregate;
      chain = ChainedAggregateStatement.DeconstructChain(chain, out aggregate);
      aggregateStatementList.Add(aggregate);
    }
    return new AggregateStatement(aggregateStatementList.ToArray());
  }

  public Type Eval()
  {
    return typeof (Aggregate<>).MakeGenericType(((IEnumerable<IChainedStatement>) this.ChainedAggregateFunctions).EvalToChain());
  }
}
