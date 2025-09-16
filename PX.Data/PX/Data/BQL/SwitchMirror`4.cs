// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.SwitchMirror`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.BQL;

[Obsolete("Mirror class should not be used directly")]
public abstract class SwitchMirror<TBqlType, TCases, TPrevCase, TOperand> : 
  BqlSwitchFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TCases, TPrevCase>, TOperand>, TBqlType>
  where TBqlType : class, IBqlDataType
  where TCases : ITypeArrayOf<IBqlCase>
  where TPrevCase : IBqlCase
  where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
{
  /// <summary>
  /// Defines a condition section of the N-ary operator expression.
  /// </summary>
  public new static class When<TCondition> where TCondition : IBqlUnary, new()
  {
    /// <summary>
    /// Finishes the N-ary operator expression.
    /// Equivalent to SQL CASE-WHEN-ELSE chain.
    /// Is strongly typed version of <see cref="T:PX.Data.Switch`2" />.
    /// </summary>
    public class Else<TDefault> : 
      Switch<TBqlType, TypeArrayOf<IBqlCase>.Append<TCases, TPrevCase>, Case<Where<TCondition>, TOperand>, TDefault>,
      IDoNotConvert
      where TDefault : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
    {
    }

    /// <summary>
    /// Finishes the N-ary operator expression.
    /// Equivalent to SQL CASE-WHEN chain.
    /// Is strongly typed version of <see cref="T:PX.Data.Switch`1" />.
    /// </summary>
    public class ElseNull : 
      BqlSwitchFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Append<TCases, TPrevCase>, Case<Where<TCondition>, TOperand>>, Null>, TBqlType>,
      IDoNotConvert
    {
    }

    /// <summary>
    /// Indicates that the preceeding operand should not be defaulted. See <see cref="T:PX.Data.Case2`2" />
    /// </summary>
    public static class NoDefault
    {
      /// <summary>
      /// Finishes the N-ary operator expression.
      /// Equivalent to SQL CASE-WHEN-ELSE chain.
      /// Is strongly typed version of <see cref="T:PX.Data.Switch`2" />.
      /// </summary>
      public class Else<TDefault> : 
        Switch<TBqlType, TypeArrayOf<IBqlCase>.Append<TCases, TPrevCase>, Case2<Where<TCondition>, TOperand>, TDefault>,
        IDoNotConvert
        where TDefault : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
      {
      }

      /// <summary>
      /// Finishes the N-ary operator expression.
      /// Equivalent to SQL CASE-WHEN chain.
      /// Is strongly typed version of <see cref="T:PX.Data.Switch`1" />.
      /// </summary>
      public class ElseNull : 
        BqlSwitchFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Append<TCases, TPrevCase>, Case2<Where<TCondition>, TOperand>>, Null>, TBqlType>,
        IDoNotConvert
      {
      }
    }
  }
}
