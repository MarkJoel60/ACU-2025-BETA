// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlChainableConditionLite`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Allows to use chainable conditions with classes that already implement <see cref="T:PX.Data.IBqlUnary" />.
/// </summary>
public abstract class BqlChainableConditionLite<TSelf> where TSelf : IBqlUnary, new()
{
  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "and".
  /// </summary>
  public sealed class And<TCondition> : 
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<TSelf>, PX.Data.And<TCondition>>>
    where TCondition : IBqlUnary, new()
  {
  }

  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "or".
  /// </summary>
  public sealed class Or<TCondition> : 
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<TSelf>, PX.Data.Or<TCondition>>>
    where TCondition : IBqlUnary, new()
  {
  }
}
