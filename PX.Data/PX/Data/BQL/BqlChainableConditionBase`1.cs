// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlChainableConditionBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.BQL;

public abstract class BqlChainableConditionBase<TConditions> : 
  CustomPredicate,
  IBqlChainableCondition,
  IBqlPredicateChain,
  IDoNotConvert
  where TConditions : ITypeArrayOf<IBqlBinary>
{
  private static Lazy<System.Type> PredicateType { get; } = Lazy.By<System.Type>((Func<System.Type>) (() => ConditionConvertor.TryConvert(typeof (BqlChainableConditionBase<TConditions>), true)));

  protected BqlChainableConditionBase()
    : base((IBqlUnary) Activator.CreateInstance(BqlChainableConditionBase<TConditions>.PredicateType.Value))
  {
  }

  private IBqlPredicateChain Chain => (IBqlPredicateChain) this.Original;

  IBqlUnary IBqlPredicateChain.GetContainedPredicate() => this.Chain.GetContainedPredicate();

  bool IBqlPredicateChain.ContainsOperandWithComparison()
  {
    return this.Chain.ContainsOperandWithComparison();
  }

  IBqlBinary IBqlPredicateChain.GetNextPredicate() => this.Chain.GetNextPredicate();

  bool IBqlPredicateChain.UseParenthesis() => this.Chain.UseParenthesis();

  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "and".
  /// </summary>
  public sealed class And<TCondition> : 
    BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TConditions, PX.Data.And<TCondition>>>
    where TCondition : IBqlUnary, new()
  {
  }

  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "or".
  /// </summary>
  public sealed class Or<TCondition> : 
    BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TConditions, PX.Data.Or<TCondition>>>
    where TCondition : IBqlUnary, new()
  {
  }
}
