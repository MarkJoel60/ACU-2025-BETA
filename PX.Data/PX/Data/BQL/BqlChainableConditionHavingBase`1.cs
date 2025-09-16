// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlChainableConditionHavingBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.BQL;

public abstract class BqlChainableConditionHavingBase<TConditions> : IBqlHavingCondition where TConditions : ITypeArrayOf<IBqlBinary>
{
  private readonly Lazy<IBqlUnary> _predicate = Lazy.By<IBqlUnary>((Func<IBqlUnary>) (() => (IBqlUnary) Activator.CreateInstance(BqlChainableConditionHavingBase<TConditions>.PredicateType.Value)));

  private static Lazy<System.Type> PredicateType { get; } = Lazy.By<System.Type>((Func<System.Type>) (() => ConditionConvertor.TryConvert(typeof (BqlChainableConditionBase<TConditions>), true)));

  IBqlUnary IBqlHavingCondition.MatchingCondition => this._predicate.Value;

  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "and".
  /// </summary>
  public sealed class And<TCondition> : 
    BqlChainableConditionHavingMirror<TypeArrayOf<IBqlBinary>.Append<TConditions, PX.Data.And<HavingConditionWrapper<TCondition>>>>
    where TCondition : IBqlHavingCondition, new()
  {
  }

  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "or".
  /// </summary>
  public sealed class Or<TCondition> : 
    BqlChainableConditionHavingMirror<TypeArrayOf<IBqlBinary>.Append<TConditions, PX.Data.Or<HavingConditionWrapper<TCondition>>>>
    where TCondition : IBqlHavingCondition, new()
  {
  }
}
