// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlAggregatedOperand`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

/// <exclude />
public abstract class BqlAggregatedOperand<TAggregator, TBqlType> : 
  IBqlAggregatedOperand,
  IBqlCreator,
  IBqlVerifier,
  IImplement<TBqlType>,
  IImplement<IBqlCastableTo<TBqlType>>
  where TAggregator : IBqlSimpleAggregator, new()
  where TBqlType : class, IBqlDataType
{
  private readonly Lazy<FunctionWrapper<TAggregator>> _aggregator = Lazy.By<FunctionWrapper<TAggregator>>((Func<FunctionWrapper<TAggregator>>) (() => new FunctionWrapper<TAggregator>()));

  public virtual void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._aggregator.Value.Verify(cache, item, pars, ref result, ref value);
  }

  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._aggregator.Value.AppendExpression(ref exp, graph, info, selection);
  }

  /// <summary>
  /// Checks if the preceding operand satisfies a custom <typeparamref name="TComparison" />.
  /// </summary>
  public class Is<TComparison> : 
    BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<FunctionWrapper<TAggregator>, TComparison>>>>
    where TComparison : IBqlComparison, new()
  {
  }

  /// <summary>
  /// Compares the preceding operand with <typeparamref name="TAggregatedOperand" /> for equality.
  /// </summary>
  public sealed class IsEqual<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<Equal<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the preceding operand is not equal to <typeparamref name="TAggregatedOperand" />.
  /// </summary>
  public sealed class IsNotEqual<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotEqual<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the preceding operand is greater or equal to <typeparamref name="TAggregatedOperand" />.
  /// </summary>
  public sealed class IsGreaterEqual<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<GreaterEqual<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the preceding operand is greater than <typeparamref name="TAggregatedOperand" />.
  /// </summary>
  public sealed class IsGreater<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<Greater<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the preceding operand is less or equal to <typeparamref name="TAggregatedOperand" />.
  /// </summary>
  public sealed class IsLessEqual<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<LessEqual<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the preceding operand is less than <typeparamref name="TAggregatedOperand" />.
  /// </summary>
  public sealed class IsLess<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<Less<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Compares the preceding string operand with the pattern string specified in <typeparamref name="TAggregatedOperand" />.
  /// Equivalent to the SQL operator LIKE.
  /// </summary>
  public sealed class IsLike<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<Like<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<TBqlType>, IImplement<IBqlString>, new()
  {
  }

  /// <summary>
  /// Checks if the preceding string operand does not match the pattern string specified in <typeparamref name="TAggregatedOperand" />.
  /// Equivalent to SQL operator NOT LIKE.
  /// </summary>
  public sealed class IsNotLike<TAggregatedOperand> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotLike<FunctionWrapper<TAggregatedOperand>>>
    where TAggregatedOperand : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<TBqlType>, IImplement<IBqlString>, new()
  {
  }

  /// <summary>
  /// Checks if the value of the preceding operand falls between the values of
  /// <typeparamref name="TAggregatedOperand1" /> and <typeparamref name="TAggregatedOperand2" />.
  /// Equivalent to SQL operator BETWEEN.
  /// </summary>
  public sealed class IsBetween<TAggregatedOperand1, TAggregatedOperand2> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<Between<FunctionWrapper<TAggregatedOperand1>, FunctionWrapper<TAggregatedOperand2>>>
    where TAggregatedOperand1 : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TAggregatedOperand2 : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the value of the preceding operand does not fall between the
  /// values of <typeparamref name="TAggregatedOperand1" /> and <typeparamref name="TAggregatedOperand2" />.
  /// Equivalent to SQL operator NOT BETWEEN.
  /// </summary>
  public sealed class IsNotBetween<TAggregatedOperand1, TAggregatedOperand2> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotBetween<FunctionWrapper<TAggregatedOperand1>, FunctionWrapper<TAggregatedOperand2>>>
    where TAggregatedOperand1 : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TAggregatedOperand2 : IBqlAggregatedOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the preceding field is null. Equivalent to SQL operator IS NULL.
  /// </summary>
  public sealed class IsNull : BqlAggregatedOperand<TAggregator, TBqlType>.Is<PX.Data.IsNull>
  {
  }

  /// <summary>
  /// Checks if the preceding field is not null. Results in true for data records
  /// with this field containing a value. Equivalent to SQL operator IS NOT NULL.
  /// </summary>
  public sealed class IsNotNull : BqlAggregatedOperand<TAggregator, TBqlType>.Is<PX.Data.IsNotNull>
  {
  }

  /// <summary>
  /// Checks if the preceding operand matches any value in the array returned by the operand that should be the Required or Optional BQL parameter with a specified field name.
  /// The condition is true if the preceding operand is equal to a value from the array.
  /// Equivalent to the SQL operator IN.
  /// The In operator is used to replace multiple OR conditions in a BQL statement.
  /// </summary>
  public sealed class IsIn<TParameter> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In<TParameter>>
    where TParameter : IBqlParameter, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding operand does not match any value in the array returned by the operand that should be the Required or Optional BQL parameter with a specified field name.
  /// The condition is true if the array does not contain a value that is equal to the preceding operand.
  /// Equivalent to SQL operator NOT IN.</summary>
  public sealed class IsNotIn<TParameter> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn<TParameter>>
    where TParameter : IBqlParameter, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the specified field value matches any value in the list of constants defined by the operand.
  /// The condition is true if the field value is equal to a value from the list.
  /// </summary>
  public sealed class IsIn<TOp1, TOp2> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the specified field value does not match a value in the list of constants defined by the operand.
  /// The condition is true if the field value is not present in the list.
  /// </summary>
  public sealed class IsNotIn<TOp1, TOp2> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>, FunctionWrapper<TOp6>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp6 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>, FunctionWrapper<TOp6>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp6 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>, FunctionWrapper<TOp6>, FunctionWrapper<TOp7>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp6 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp7 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>, FunctionWrapper<TOp6>, FunctionWrapper<TOp7>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp6 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp7 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7, TOp8> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>, FunctionWrapper<TOp6>, FunctionWrapper<TOp7>, FunctionWrapper<TOp8>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp6 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp7 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp8 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7, TOp8> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<FunctionWrapper<TOp1>, FunctionWrapper<TOp2>, FunctionWrapper<TOp3>, FunctionWrapper<TOp4>, FunctionWrapper<TOp5>, FunctionWrapper<TOp6>, FunctionWrapper<TOp7>, FunctionWrapper<TOp8>>>
    where TOp1 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp2 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp3 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp4 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp5 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp6 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp7 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
    where TOp8 : IBqlAggregatedOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>, new()
  {
  }

  /// <summary>
  /// Checks if the specified field value matches any value in the list of constants defined by the operand.
  /// The condition is true if the field value is equal to a value from the list.
  /// </summary>
  public sealed class IsInSequence<TSequence> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<In3<TSequence>>
    where TSequence : IBqlConstantsOf<IImplement<IBqlEquitable>>, IBqlConstantsOf<IImplement<IBqlCastableTo<TBqlType>>>, new()
  {
  }

  /// <summary>
  /// Checks if the specified field value does not match a value in the list of constants defined by the operand.
  /// The condition is true if the field value is not present in the list.
  /// </summary>
  public sealed class IsNotInSequence<TSequence> : 
    BqlAggregatedOperand<TAggregator, TBqlType>.Is<NotIn3<TSequence>>
    where TSequence : IBqlConstantsOf<IImplement<IBqlEquitable>>, IBqlConstantsOf<IImplement<IBqlCastableTo<TBqlType>>>, new()
  {
  }
}
