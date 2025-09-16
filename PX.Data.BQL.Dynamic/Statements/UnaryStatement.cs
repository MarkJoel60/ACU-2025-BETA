// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Statements.UnaryStatement
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
public class UnaryStatement : IStatement
{
  private readonly Type _customUnary;
  private readonly bool _isOn;
  protected internal readonly bool IsUnary;
  protected internal readonly UnaryStatement Unary;
  protected internal readonly OperandStatement LeftOperand;
  protected internal readonly CompareStatement Compare;
  protected internal readonly bool HasBinaries;
  protected internal readonly IEnumerable<BinaryStatement> Binaries;
  private static readonly ReadOnlyBiDictionary<UnaryStatement.UnaryClassification, Type> Unaries = ReadOnlyBiDictionaryExt.ToBiDictionary<UnaryStatement.UnaryClassification, Type>((IReadOnlyDictionary<UnaryStatement.UnaryClassification, Type>) new Dictionary<UnaryStatement.UnaryClassification, Type>()
  {
    [new UnaryStatement.UnaryClassification(false, false, false)] = typeof (Where<,>),
    [new UnaryStatement.UnaryClassification(false, false, true)] = typeof (Where<,,>),
    [new UnaryStatement.UnaryClassification(false, true, false)] = typeof (Where<>),
    [new UnaryStatement.UnaryClassification(false, true, true)] = typeof (Where2<,>),
    [new UnaryStatement.UnaryClassification(true, false, false)] = typeof (On<,>),
    [new UnaryStatement.UnaryClassification(true, false, true)] = typeof (On<,,>),
    [new UnaryStatement.UnaryClassification(true, true, false)] = typeof (On<>),
    [new UnaryStatement.UnaryClassification(true, true, true)] = typeof (On2<,>)
  });

  private UnaryStatement(bool isOn, IEnumerable<BinaryStatement> binaries)
  {
    this.Binaries = binaries != null ? (IEnumerable<BinaryStatement>) binaries.ToArray<BinaryStatement>() : (IEnumerable<BinaryStatement>) (BinaryStatement[]) null;
    IEnumerable<BinaryStatement> binaries1 = this.Binaries;
    this.HasBinaries = binaries1 != null && binaries1.Any<BinaryStatement>();
    this._isOn = isOn;
  }

  internal UnaryStatement(Type customUnary)
  {
    customUnary.VerifyRawStatement<IBqlUnary>(nameof (customUnary));
    this._customUnary = customUnary;
  }

  internal UnaryStatement(bool isOn, UnaryStatement unary, IEnumerable<BinaryStatement> binaries)
    : this(isOn, binaries)
  {
    this.Unary = unary;
    this.IsUnary = true;
  }

  internal UnaryStatement(
    bool isOn,
    OperandStatement leftOperandStatement,
    CompareStatement compareStatement,
    IEnumerable<BinaryStatement> binaries)
    : this(isOn, binaries)
  {
    this.LeftOperand = leftOperandStatement;
    this.Compare = compareStatement;
    this.IsUnary = false;
  }

  public Type Eval()
  {
    Type customUnary = this._customUnary;
    return (object) customUnary != null ? customUnary : UnaryStatement.Unaries[new UnaryStatement.UnaryClassification(this._isOn, this.IsUnary, this.HasBinaries)].MakeGenericType(this.CreateArguments());
  }

  private Type[] CreateArguments()
  {
    Type[] typeArray = new Type[4]
    {
      this.Unary?.Eval(),
      this.LeftOperand?.Eval(),
      this.Compare?.Eval(),
      null
    };
    IEnumerable<BinaryStatement> binaries = this.Binaries;
    typeArray[3] = binaries != null ? ((IEnumerable<IChainedStatement>) binaries).EvalToChain() : (Type) null;
    return EnumerableExtensions.WhereNotNull<Type>((IEnumerable<Type>) typeArray).ToArray<Type>();
  }

  internal static UnaryStatement FromRawStatement(Type condition, bool topLevel = false)
  {
    if (topLevel)
      condition = ConditionConvertor.TryConvert(condition, true);
    Type genericTypeDefinition = condition.GetGenericTypeDefinition();
    Type[] genericArguments = condition.GetGenericArguments();
    if (!UnaryStatement.Unaries.ContainsValue(genericTypeDefinition))
      return new UnaryStatement(condition);
    return !UnaryStatement.Unaries[genericTypeDefinition].IsUnary ? new UnaryStatement(UnaryStatement.Unaries[genericTypeDefinition].IsOn, OperandStatement.FromRawStatement(genericArguments[0]), CompareStatement.FromRawStatement(genericArguments[1]), UnaryStatement.Unaries[genericTypeDefinition].HasBinaries ? (IEnumerable<BinaryStatement>) BinaryStatement.FromRawStatement(genericArguments[2]) : (IEnumerable<BinaryStatement>) (BinaryStatement[]) null) : new UnaryStatement(UnaryStatement.Unaries[genericTypeDefinition].IsOn, UnaryStatement.FromRawStatement(genericArguments[0]), UnaryStatement.Unaries[genericTypeDefinition].HasBinaries ? (IEnumerable<BinaryStatement>) BinaryStatement.FromRawStatement(genericArguments[1]) : (IEnumerable<BinaryStatement>) (BinaryStatement[]) null);
  }

  private class UnaryClassification(bool isOn, bool isUnary, bool hasBinaries) : 
    Tuple<bool, bool, bool>(isOn, isUnary, hasBinaries)
  {
    public bool IsOn => this.Item1;

    public bool IsUnary => this.Item2;

    public bool HasBinaries => this.Item3;
  }
}
