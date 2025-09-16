// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.ChainedAggregateStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

public abstract class ChainedAggregateStatement : IChainedStatement, IStatement
{
  private readonly AggregateType _function;
  private readonly Type _operand;
  private static readonly ReadOnlyBiDictionary<ChainedAggregateStatement.AggregateClassification, Type> Funcs = ReadOnlyBiDictionaryExt.ToBiDictionary<ChainedAggregateStatement.AggregateClassification, Type>((IReadOnlyDictionary<ChainedAggregateStatement.AggregateClassification, Type>) new Dictionary<ChainedAggregateStatement.AggregateClassification, Type>()
  {
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Count, false, typeof (IBqlField))] = typeof (Count<>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Group, false, typeof (IBqlField))] = typeof (GroupBy<>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Avg, false, typeof (IBqlField))] = typeof (Avg<>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Min, false, typeof (IBqlField))] = typeof (Min<>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Max, false, typeof (IBqlField))] = typeof (Max<>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Sum, false, typeof (IBqlField))] = typeof (Sum<>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Group, true, typeof (IBqlField))] = typeof (GroupBy<,>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Avg, true, typeof (IBqlField))] = typeof (Avg<,>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Min, true, typeof (IBqlField))] = typeof (Min<,>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Max, true, typeof (IBqlField))] = typeof (Max<,>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Sum, true, typeof (IBqlField))] = typeof (Sum<,>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Sum, false, typeof (IBqlAggregateOperand))] = typeof (Sum2<>),
    [new ChainedAggregateStatement.AggregateClassification(AggregateType.Sum, true, typeof (IBqlAggregateOperand))] = typeof (Sum2<,>)
  });

  internal ChainedAggregateStatement(AggregateType function, Type operand)
  {
    this._function = function;
    this._operand = operand;
  }

  protected abstract Type OperandType { get; }

  public Type Eval()
  {
    if (this._function == AggregateType.Count && this._operand == (Type) null)
      return typeof (Count);
    return ChainedAggregateStatement.Funcs[new ChainedAggregateStatement.AggregateClassification(this._function, false, this.OperandType)].MakeGenericType(this._operand);
  }

  public Type Eval(Type next)
  {
    return ChainedAggregateStatement.Funcs[new ChainedAggregateStatement.AggregateClassification(this._function, true, this.OperandType)].MakeGenericType(this._operand, next);
  }

  internal static Type DeconstructChain(Type chain, out ChainedAggregateStatement aggregate)
  {
    if (chain == typeof (Count))
    {
      aggregate = (ChainedAggregateStatement) new FieldAggregateStatement(AggregateType.Count, (Type) null);
      return (Type) null;
    }
    Type genericTypeDefinition = chain.GetGenericTypeDefinition();
    Type[] genericArguments = chain.GetGenericArguments();
    if (!ChainedAggregateStatement.Funcs.ContainsValue(genericTypeDefinition))
      throw new NotSupportedException();
    Type type = ((IEnumerable<Type>) genericArguments).First<Type>();
    aggregate = !typeof (IBqlField).IsAssignableFrom(type) ? (ChainedAggregateStatement) new OperandAggregateStatement(ChainedAggregateStatement.Funcs[genericTypeDefinition].Type, type) : (ChainedAggregateStatement) new FieldAggregateStatement(ChainedAggregateStatement.Funcs[genericTypeDefinition].Type, type);
    return !ChainedAggregateStatement.Funcs[genericTypeDefinition].IsChained ? (Type) null : genericArguments[1];
  }

  private class AggregateClassification(AggregateType type, bool isChained, Type operand) : 
    Tuple<AggregateType, bool, Type>(type, isChained, operand)
  {
    public AggregateType Type => this.Item1;

    public bool IsChained => this.Item2;

    public Type OperandType => this.Item3;
  }
}
