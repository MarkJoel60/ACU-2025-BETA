// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlFunctionMirror`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.BQL;

/// <exclude />
[Obsolete("Mirror class should not be used directly")]
public abstract class BqlFunctionMirror<TFunction, TBqlType> : BqlFunction<TFunction, TBqlType>
  where TFunction : IBqlOperand, IBqlCreator, new()
  where TBqlType : class, IBqlDataType
{
  /// <summary>
  /// Returns the preceding operand if it is not null, or a <typeparamref name="TOperand" /> otherwise.
  /// Equivalent to SQL function ISNULL.
  /// Is a strongly typed version of <see cref="T:PX.Data.IsNull`2" />.
  /// </summary>
  public new sealed class IfNullThen<TOperand> : BqlFunction<PX.Data.IsNull<TFunction, TOperand>, TBqlType>
    where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns null if the preceding operand equals <typeparamref name="TOperand" /> and
  /// returns the preceding operand if the two expression are not equal.
  /// Equivalent to SQL function NULLIF.
  /// Is a strongly typed version of <see cref="T:PX.Data.NullIf`2" />.
  /// </summary>
  public new sealed class NullIf<TOperand> : BqlFunction<NullIf<TFunction, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the sum of the preceding operand and a <typeparamref name="TOperand" />.
  /// Is a strongly typed version of <see cref="T:PX.Data.Add`2" />.
  /// </summary>
  public new sealed class Add<TOperand> : BqlFunction<Add<TFunction, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the subtraction of a <typeparamref name="TOperand" /> from the preceding operand.
  /// Is a strongly typed version of <see cref="T:PX.Data.Sub`2" />.
  /// </summary>
  public new sealed class Subtract<TOperand> : BqlFunction<Sub<TFunction, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the multiplication of the preceding operand by a <typeparamref name="TOperand" />.
  /// Is a strongly typed version of <see cref="T:PX.Data.Mult`2" />.
  /// </summary>
  public new sealed class Multiply<TOperand> : BqlFunction<Mult<TFunction, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the division of the preceding operand and a <typeparamref name="TOperand" />.
  /// Is a strongly typed version of <see cref="T:PX.Data.Div`2" />.
  /// </summary>
  public new sealed class Divide<TOperand> : BqlFunction<Div<TFunction, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }
}
