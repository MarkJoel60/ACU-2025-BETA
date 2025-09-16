// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.BinaryStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common;
using PX.Common.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class BinaryStatement : IChainedStatement, IStatement
{
  private readonly bool _isAnd;
  private readonly bool _isUnary;
  private readonly UnaryStatement _unary;
  private readonly OperandStatement _leftOperand;
  private readonly CompareStatement _compare;
  private static readonly ReadOnlyBiDictionary<BinaryStatement.BinaryClassification, Type> Binaries = ReadOnlyBiDictionaryExt.ToBiDictionary<BinaryStatement.BinaryClassification, Type>((IReadOnlyDictionary<BinaryStatement.BinaryClassification, Type>) new Dictionary<BinaryStatement.BinaryClassification, Type>()
  {
    [new BinaryStatement.BinaryClassification(false, false, false)] = typeof (Or<,>),
    [new BinaryStatement.BinaryClassification(false, false, true)] = typeof (Or<,,>),
    [new BinaryStatement.BinaryClassification(false, true, false)] = typeof (Or<>),
    [new BinaryStatement.BinaryClassification(false, true, true)] = typeof (Or2<,>),
    [new BinaryStatement.BinaryClassification(true, false, false)] = typeof (And<,>),
    [new BinaryStatement.BinaryClassification(true, false, true)] = typeof (And<,,>),
    [new BinaryStatement.BinaryClassification(true, true, false)] = typeof (And<>),
    [new BinaryStatement.BinaryClassification(true, true, true)] = typeof (And2<,>)
  });

  internal BinaryStatement(bool isAnd, UnaryStatement unary)
  {
    this._isAnd = isAnd;
    this._unary = unary;
    this._isUnary = true;
  }

  internal BinaryStatement(
    bool isAnd,
    OperandStatement leftOperandStatement,
    CompareStatement compareStatement)
  {
    this._isAnd = isAnd;
    this._leftOperand = leftOperandStatement;
    this._compare = compareStatement;
    this._isUnary = false;
  }

  public Type Eval()
  {
    return BinaryStatement.Binaries[new BinaryStatement.BinaryClassification(this._isAnd, this._isUnary, false)].MakeGenericType(this.CreateArguments());
  }

  public Type Eval(Type next)
  {
    return BinaryStatement.Binaries[new BinaryStatement.BinaryClassification(this._isAnd, this._isUnary, true)].MakeGenericType(this.CreateArguments(next));
  }

  private Type[] CreateArguments(Type next = null)
  {
    return EnumerableExtensions.WhereNotNull<Type>((IEnumerable<Type>) new Type[4]
    {
      this._unary?.Eval(),
      this._leftOperand?.Eval(),
      this._compare?.Eval(),
      next
    }).ToArray<Type>();
  }

  internal static BinaryStatement[] FromRawStatement(Type additionalCondition)
  {
    additionalCondition.VerifyRawStatement<IBqlBinary>(nameof (additionalCondition));
    List<BinaryStatement> binaryStatementList = new List<BinaryStatement>();
    Type chain = additionalCondition;
    while (chain != (Type) null)
    {
      BinaryStatement binary;
      chain = BinaryStatement.DeconstructChain(chain, out binary);
      binaryStatementList.Add(binary);
    }
    return binaryStatementList.ToArray();
  }

  internal static Type DeconstructChain(Type chain, out BinaryStatement binary)
  {
    Type genericTypeDefinition = chain.GetGenericTypeDefinition();
    Type[] genericArguments = chain.GetGenericArguments();
    if (!BinaryStatement.Binaries.ContainsValue(genericTypeDefinition))
      throw new NotSupportedException();
    binary = BinaryStatement.Binaries[genericTypeDefinition].IsUnary ? new BinaryStatement(BinaryStatement.Binaries[genericTypeDefinition].IsAnd, UnaryStatement.FromRawStatement(genericArguments[0])) : new BinaryStatement(BinaryStatement.Binaries[genericTypeDefinition].IsAnd, OperandStatement.FromRawStatement(genericArguments[0]), CompareStatement.FromRawStatement(genericArguments[1]));
    if (!BinaryStatement.Binaries[genericTypeDefinition].IsChained)
      return (Type) null;
    return !BinaryStatement.Binaries[genericTypeDefinition].IsUnary ? genericArguments[2] : genericArguments[1];
  }

  private class BinaryClassification(bool isAnd, bool isUnary, bool isChained) : 
    Tuple<bool, bool, bool>(isAnd, isUnary, isChained)
  {
    public bool IsAnd => this.Item1;

    public bool IsUnary => this.Item2;

    public bool IsChained => this.Item3;
  }
}
