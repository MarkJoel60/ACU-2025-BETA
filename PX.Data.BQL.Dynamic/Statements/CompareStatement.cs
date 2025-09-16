// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.CompareStatement
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Common.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Statements;

[ImmutableObject(true)]
public class CompareStatement : IStatement
{
  private readonly Type _customCompare;
  private readonly CompareType _compareType;
  private readonly OperandStatement _rightOperand;
  private static readonly ReadOnlyBiDictionary<CompareType, Type> Operators = ReadOnlyBiDictionaryExt.ToBiDictionary<CompareType, Type>((IReadOnlyDictionary<CompareType, Type>) new Dictionary<CompareType, Type>()
  {
    [CompareType.Equal] = typeof (Equal<>),
    [CompareType.NotEqual] = typeof (NotEqual<>),
    [CompareType.Less] = typeof (Less<>),
    [CompareType.Greater] = typeof (Greater<>),
    [CompareType.LessEqual] = typeof (LessEqual<>),
    [CompareType.GreaterEqual] = typeof (GreaterEqual<>),
    [CompareType.Like] = typeof (Like<>),
    [CompareType.NotLike] = typeof (NotLike<>)
  });

  internal CompareStatement(Type customCompare)
  {
    customCompare.VerifyRawStatement<IBqlComparison>(nameof (customCompare));
    this._customCompare = customCompare;
  }

  internal CompareStatement(CompareType compareType, OperandStatement rightOperand)
  {
    this._compareType = compareType;
    this._rightOperand = rightOperand;
  }

  public Type Eval()
  {
    if (this._customCompare != (Type) null)
      return this._customCompare;
    if (this._compareType == CompareType.IsNull)
      return typeof (IsNull);
    if (this._compareType == CompareType.IsNotNull)
      return typeof (IsNotNull);
    return CompareStatement.Operators[this._compareType].MakeGenericType(this._rightOperand.Eval());
  }

  internal static CompareStatement FromRawStatement(Type compare)
  {
    if (compare == typeof (IsNull))
      return new CompareStatement(CompareType.IsNull, (OperandStatement) null);
    if (compare == typeof (IsNotNull))
      return new CompareStatement(CompareType.IsNotNull, (OperandStatement) null);
    Type genericTypeDefinition = compare.GetGenericTypeDefinition();
    Type[] genericArguments = compare.GetGenericArguments();
    return !CompareStatement.Operators.ContainsValue(genericTypeDefinition) ? new CompareStatement(compare) : new CompareStatement(CompareStatement.Operators[genericTypeDefinition], OperandStatement.FromRawStatement(genericArguments[0]));
  }
}
