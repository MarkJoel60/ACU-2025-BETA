// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlChainableConditionHavingMirror`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.BQL;

[Obsolete("Mirror class should not be used directly")]
public class BqlChainableConditionHavingMirror<TConditions> : 
  BqlChainableConditionHavingBase<TConditions>
  where TConditions : ITypeArrayOf<IBqlBinary>
{
  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "and".
  /// </summary>
  public new sealed class And<TCondition> : 
    BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.Append<TConditions, PX.Data.And<HavingConditionWrapper<TCondition>>>>
    where TCondition : IBqlHavingCondition, new()
  {
  }

  /// <summary>
  /// Appends a unary operator to a conditional expression via logical "or".
  /// </summary>
  public new sealed class Or<TCondition> : 
    BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.Append<TConditions, PX.Data.Or<HavingConditionWrapper<TCondition>>>>
    where TCondition : IBqlHavingCondition, new()
  {
  }
}
