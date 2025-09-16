// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlOperand`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// A BQL scalar operand, that allows to use fluent comparisons.
/// </summary>
public abstract class BqlOperand<TSelf, TBqlType> : 
  IBqlOperand,
  IImplement<TBqlType>,
  IImplement<IBqlCastableTo<TBqlType>>,
  IShouldBeReplacedWith<TSelf>
  where TSelf : IBqlOperand
  where TBqlType : class, IBqlDataType
{
  /// <summary>
  /// Checks if the preceding operand satisfies a custom <typeparamref name="TComparison" />.
  /// </summary>
  public class Is<TComparison> : BqlChainableCondition<Compare<TSelf, TComparison>> where TComparison : IBqlComparison, new()
  {
  }

  /// <summary>
  /// Compares the preceding operand with <typeparamref name="TOperand" /> for equality.
  /// </summary>
  public sealed class IsEqual<TOperand> : BqlOperand<TSelf, TBqlType>.Is<Equal<TOperand>> where TOperand : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding operand is not equal to <typeparamref name="TOperand" />.
  /// </summary>
  public sealed class IsNotEqual<TOperand> : BqlOperand<TSelf, TBqlType>.Is<NotEqual<TOperand>> where TOperand : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding operand is greater or equal to <typeparamref name="TOperand" />.
  /// </summary>
  public sealed class IsGreaterEqual<TOperand> : 
    BqlOperand<TSelf, TBqlType>.Is<GreaterEqual<TOperand>>
    where TOperand : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding operand is greater than <typeparamref name="TOperand" />.
  /// </summary>
  public sealed class IsGreater<TOperand> : BqlOperand<TSelf, TBqlType>.Is<Greater<TOperand>> where TOperand : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding operand is less or equal to <typeparamref name="TOperand" />.
  /// </summary>
  public sealed class IsLessEqual<TOperand> : BqlOperand<TSelf, TBqlType>.Is<LessEqual<TOperand>> where TOperand : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding operand is less than <typeparamref name="TOperand" />.
  /// </summary>
  public sealed class IsLess<TOperand> : BqlOperand<TSelf, TBqlType>.Is<Less<TOperand>> where TOperand : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Compares the preceding string operand with the pattern string specified in <typeparamref name="TOperand" />.
  /// Equivalent to the SQL operator LIKE.
  /// </summary>
  public sealed class IsLike<TOperand> : BqlOperand<TSelf, TBqlType>.Is<Like<TOperand>> where TOperand : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding string operand does not match the pattern string specified in <typeparamref name="TOperand" />.
  /// Equivalent to SQL operator NOT LIKE.
  /// </summary>
  public sealed class IsNotLike<TOperand> : BqlOperand<TSelf, TBqlType>.Is<NotLike<TOperand>> where TOperand : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding string operand starts with the <typeparamref name="TOperand" /> string.
  /// Equivalent to SQL operator LIKE @P0 + '%'.
  /// </summary>
  public sealed class StartsWith<TOperand> : BqlOperand<TSelf, TBqlType>.Is<PX.Data.StartsWith<TOperand>>
    where TOperand : IBqlOperand, IImplement<IBqlEquitable>, IImplement<TBqlType>, IImplement<IBqlString>
  {
  }

  /// <summary>
  /// Checks if the preceding string operand ends with the <typeparamref name="TOperand" /> string.
  /// Equivalent to SQL operator LIKE '%' + @P0.
  /// </summary>
  public sealed class EndsWith<TOperand> : BqlOperand<TSelf, TBqlType>.Is<PX.Data.EndsWith<TOperand>>
    where TOperand : IBqlOperand, IImplement<IBqlEquitable>, IImplement<TBqlType>, IImplement<IBqlString>
  {
  }

  /// <summary>
  /// Checks if the preceding string operand contains the <typeparamref name="TOperand" /> string.
  /// Equivalent to SQL operator LIKE '%' + @P0 + '%'.
  /// </summary>
  public sealed class Contains<TOperand> : BqlOperand<TSelf, TBqlType>.Is<PX.Data.Contains<TOperand>>
    where TOperand : IBqlOperand, IImplement<IBqlEquitable>, IImplement<TBqlType>, IImplement<IBqlString>
  {
  }

  /// <summary>
  /// Checks if the value of the preceding operand falls between the values of
  /// <typeparamref name="TOperand1" /> and <typeparamref name="TOperand2" />.
  /// Equivalent to SQL operator BETWEEN.
  /// </summary>
  public sealed class IsBetween<TOperand1, TOperand2> : 
    BqlOperand<TSelf, TBqlType>.Is<Between<TOperand1, TOperand2>>
    where TOperand1 : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOperand2 : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the value of the preceding operand does not fall between the
  /// values of <typeparamref name="TOperand1" /> and <typeparamref name="TOperand2" />.
  /// Equivalent to SQL operator NOT BETWEEN.
  /// </summary>
  public sealed class IsNotBetween<TOperand1, TOperand2> : 
    BqlOperand<TSelf, TBqlType>.Is<NotBetween<TOperand1, TOperand2>>
    where TOperand1 : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOperand2 : IBqlOperand, IImplement<IBqlComparable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding field is null. Equivalent to SQL operator IS NULL.
  /// </summary>
  public sealed class IsNull : BqlOperand<TSelf, TBqlType>.Is<PX.Data.IsNull>
  {
  }

  /// <summary>
  /// Checks if the preceding field is not null. Results in true for data records
  /// with this field containing a value. Equivalent to SQL operator IS NOT NULL.
  /// </summary>
  public sealed class IsNotNull : BqlOperand<TSelf, TBqlType>.Is<PX.Data.IsNotNull>
  {
  }

  /// <summary>
  /// Checks if the preceding operand matches any value in the results of the Search-based statement that is defined by the operand.
  /// The condition is true if the preceding operand is equal to a value from the result set.
  /// Equivalent to SQL statement <tt>IN(SELECT … FROM …)</tt>.
  /// </summary>
  public sealed class IsInSubselect<TSubSelect> : BqlOperand<TSelf, TBqlType>.Is<In2<TSubSelect>> where TSubSelect : IBqlSearch, IBqlCreator
  {
  }

  /// <summary>
  /// Checks if the preceding operand does not match any value in the results of the Search-based statement that is defined by the operand.
  /// The condition is true if the <typeparamref name="TSubSelect" /> result set does not contain a value that is equal to the preceding operand.
  /// Equivalent to SQL statement <tt>NOT IN(SELECT … FROM …)</tt>.
  /// </summary>
  public sealed class IsNotInSubselect<TSubSelect> : 
    BqlOperand<TSelf, TBqlType>.Is<NotIn2<TSubSelect>>
    where TSubSelect : IBqlSearch, IBqlCreator
  {
  }

  /// <summary>
  /// Checks if the preceding operand matches any value in the array returned by the operand that should be the Required or Optional BQL parameter with a specified field name.
  /// The condition is true if the preceding operand is equal to a value from the array.
  /// Equivalent to the SQL operator IN.
  /// The In operator is used to replace multiple OR conditions in a BQL statement.
  /// </summary>
  public sealed class IsIn<TParameter> : BqlOperand<TSelf, TBqlType>.Is<In<TParameter>> where TParameter : IBqlParameter, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the preceding operand does not match any value in the array returned by the operand that should be the Required or Optional BQL parameter with a specified field name.
  /// The condition is true if the array does not contain a value that is equal to the preceding operand.
  /// Equivalent to SQL operator NOT IN.</summary>
  public sealed class IsNotIn<TParameter> : BqlOperand<TSelf, TBqlType>.Is<NotIn<TParameter>> where TParameter : IBqlParameter, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the specified field value matches any value in the list of constants defined by the operand.
  /// The condition is true if the field value is equal to a value from the list.
  /// </summary>
  public sealed class IsIn<TOp1, TOp2> : BqlOperand<TSelf, TBqlType>.Is<In3<TOp1, TOp2>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the specified field value does not match a value in the list of constants defined by the operand.
  /// The condition is true if the field value is not present in the list.
  /// </summary>
  public sealed class IsNotIn<TOp1, TOp2> : BqlOperand<TSelf, TBqlType>.Is<NotIn3<TOp1, TOp2>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3> : BqlOperand<TSelf, TBqlType>.Is<In3<TOp1, TOp2, TOp3>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3> : 
    BqlOperand<TSelf, TBqlType>.Is<NotIn3<TOp1, TOp2, TOp3>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4> : 
    BqlOperand<TSelf, TBqlType>.Is<In3<TOp1, TOp2, TOp3, TOp4>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4> : 
    BqlOperand<TSelf, TBqlType>.Is<NotIn3<TOp1, TOp2, TOp3, TOp4>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5> : 
    BqlOperand<TSelf, TBqlType>.Is<In3<TOp1, TOp2, TOp3, TOp4, TOp5>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5> : 
    BqlOperand<TSelf, TBqlType>.Is<NotIn3<TOp1, TOp2, TOp3, TOp4, TOp5>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6> : 
    BqlOperand<TSelf, TBqlType>.Is<In3<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp6 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6> : 
    BqlOperand<TSelf, TBqlType>.Is<NotIn3<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp6 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7> : 
    BqlOperand<TSelf, TBqlType>.Is<In3<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp6 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp7 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7> : 
    BqlOperand<TSelf, TBqlType>.Is<NotIn3<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp6 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp7 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7, TOp8> : 
    BqlOperand<TSelf, TBqlType>.Is<In3<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7, TOp8>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp6 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp7 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp8 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <exclude />
  public sealed class IsNotIn<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7, TOp8> : 
    BqlOperand<TSelf, TBqlType>.Is<NotIn3<TOp1, TOp2, TOp3, TOp4, TOp5, TOp6, TOp7, TOp8>>
    where TOp1 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp2 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp3 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp4 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp5 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp6 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp7 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
    where TOp8 : IBqlOperand, IImplement<IBqlEquitable>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Checks if the specified field value matches any value in the list of constants defined by the operand.
  /// The condition is true if the field value is equal to a value from the list.
  /// </summary>
  public sealed class IsInSequence<TSequence> : BqlOperand<TSelf, TBqlType>.Is<In3<TSequence>> where TSequence : IBqlConstantsOf<IImplement<IBqlEquitable>>, IBqlConstantsOf<IImplement<IBqlCastableTo<TBqlType>>>, new()
  {
  }

  /// <summary>
  /// Checks if the specified field value does not match a value in the list of constants defined by the operand.
  /// The condition is true if the field value is not present in the list.
  /// </summary>
  public sealed class IsNotInSequence<TSequence> : BqlOperand<TSelf, TBqlType>.Is<NotIn3<TSequence>> where TSequence : IBqlConstantsOf<IImplement<IBqlEquitable>>, IBqlConstantsOf<IImplement<IBqlCastableTo<TBqlType>>>, new()
  {
  }

  /// <summary>
  /// Returns the preceding operand if it is not null, or a <typeparamref name="TOperand" /> otherwise.
  /// Equivalent to SQL function ISNULL.
  /// Is a strongly typed version of <see cref="T:PX.Data.IsNull`2" />.
  /// </summary>
  public sealed class IfNullThen<TOperand> : BqlFunction<PX.Data.IsNull<TSelf, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns null if the preceding operand equals <typeparamref name="TOperand" /> and
  /// returns the preceding operand if the two expression are not equal.
  /// Equivalent to SQL function NULLIF.
  /// Is a strongly typed version of <see cref="T:PX.Data.NullIf`2" />.
  /// </summary>
  public sealed class NullIf<TOperand> : BqlFunction<NullIf<TSelf, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>Returns the sum of the preceding operand and a <span class="typeparameter">TOperand</span>.
  /// This class is a strongly typed version of <see cref="T:PX.Data.Add`2" />.</summary>
  /// <typeparam name="TOperand">The operand to be added.</typeparam>
  /// <example>
  ///   <para>The code below shows a DAC field definition, which is calculated by the following formula:
  ///   ARPayment.CuryDocBal - (ARPayment.CuryApplAmt + ARPayment.CurySOApplAmt)</para>
  ///   <code title="Example">[PXCurrency(typeof(ARPayment.curyInfoID), typeof(ARPayment.unappliedBal))]
  /// [PXUIField(DisplayName = "Available Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
  /// [PXFormula(typeof(ARPayment.curyDocBal.
  ///     Subtract&lt;ARPayment.curyApplAmt.Add&lt;ARPayment.curySOApplAmt&gt;&gt;))]
  /// public virtual Decimal? CuryUnappliedBal
  /// {
  /// ...
  /// }</code>
  /// </example>
  public sealed class Add<TOperand> : BqlFunction<Add<TSelf, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the subtraction of a <typeparamref name="TOperand" /> from the preceding operand.
  /// Is a strongly typed version of <see cref="T:PX.Data.Sub`2" />.
  /// </summary>
  /// <inheritdoc cref="T:PX.Data.BQL.BqlOperand`2.Add`1" path="/example" />
  public sealed class Subtract<TOperand> : BqlFunction<Sub<TSelf, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the multiplication of the preceding operand by a <typeparamref name="TOperand" />.
  /// Is a strongly typed version of <see cref="T:PX.Data.Mult`2" />.
  /// </summary>
  public sealed class Multiply<TOperand> : BqlFunction<Mult<TSelf, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the division of the preceding operand and a <typeparamref name="TOperand" />.
  /// Is a strongly typed version of <see cref="T:PX.Data.Div`2" />.
  /// </summary>
  public sealed class Divide<TOperand> : BqlFunction<Div<TSelf, TOperand>, TBqlType> where TOperand : IBqlOperand, IImplement<IBqlNumeric>, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>
  /// Returns the concatenation of the preceding string operand a <typeparamref name="TOperand" /> string.
  /// Is a strongly typed version of <see cref="T:PX.Data.Concat`1" />.
  /// </summary>
  public sealed class Concat<TOperand> : 
    BqlFunction<PX.Data.Concat<TypeArrayOf<IBqlOperand>.FilledWith<TSelf, TOperand>>, TBqlType>
    where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>, IImplement<IBqlString>
  {
  }

  /// <summary>
  /// Provides access to the date-diff components.
  /// It's members are strongly typed versions of <see cref="T:PX.Data.DateDiff`3" />.
  /// </summary>
  public static class Diff<TOperand> where TOperand : IBqlOperand, IImplement<TBqlType>, IImplement<IBqlDataType>
  {
    /// <summary>
    /// The years component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'yyyy').
    /// </summary>
    public class Years : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.year>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The quarter component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'qq').
    /// </summary>
    public class Quarters : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.quarter>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The months component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'mm').
    /// </summary>
    public class Months : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.month>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The weeks component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'ww').
    /// </summary>
    public class Weeks : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.week>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The days component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'dd').
    /// </summary>
    public class Days : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.day>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The hours component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'hh').
    /// </summary>
    public class Hours : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.hour>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The minutes component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'mi').
    /// </summary>
    public class Minutes : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.minute>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The seconds component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'ss').
    /// </summary>
    public class Seconds : BqlFunction<DateDiff<TSelf, TOperand, DateDiff.second>, IBqlDecimal>
    {
    }

    /// <summary>
    /// The milliseconds component of the date-diff.
    /// Equivalent to SQL function DATEDIFF(DATE1, DATE2, 'ms').
    /// </summary>
    public class Milliseconds : 
      BqlFunction<DateDiff<TSelf, TOperand, DateDiff.millisecond>, IBqlDecimal>
    {
    }
  }

  /// <summary>
  /// Searches for the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> attribute
  /// on the key field and calculates the provided expression for the data record
  /// currently referenced by the attribute.
  /// </summary>
  /// <typeparam name="TKeyField">The key field to which the PXSelector attribute should be attached.</typeparam>
  /// <example>
  ///   <code>[PXFormula(typeof(SOShipment.shipmentType.FromSelectorOf&lt;shipmentNbr&gt;.
  ///   IsEqual&lt;SOShipmentType.transfer&gt;))]</code>
  /// </example>
  public sealed class FromSelectorOf<TKeyField> : BqlFunction<Selector<TKeyField, TSelf>, TBqlType> where TKeyField : IBqlOperand
  {
  }

  /// <summary>
  /// Defines a condition of the ternary operator expression.
  /// </summary>
  public static class When<TCondition> where TCondition : IBqlUnary, new()
  {
    /// <summary>
    /// Finishes the ternary operator expression.
    /// Equivalent to SQL CASE-WHEN-ELSE chain.
    /// Is strongly typed version of <see cref="T:PX.Data.Switch`2" />.
    /// </summary>
    public sealed class Else<TOperand> : 
      Switch<TBqlType, TypeArrayOf<IBqlCase>.Empty, Case<Where<TCondition>, TSelf>, TOperand>,
      IDoNotConvert
      where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
    {
    }

    /// <summary>
    /// Finishes the ternary operator expression.
    /// Equivalent to SQL CASE-WHEN chain.
    /// Is strongly typed version of <see cref="T:PX.Data.Switch`1" />.
    /// </summary>
    public sealed class ElseNull : 
      BqlSwitchFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.FilledWith<Case<Where<TCondition>, TSelf>>, Null>, TBqlType>,
      IDoNotConvert
    {
    }

    /// <summary>
    /// Indicates that the preceeding operand should not be defaulted. See <see cref="T:PX.Data.Case2`2" />
    /// </summary>
    public static class NoDefault
    {
      /// <summary>
      /// Finishes the ternary operator expression.
      /// Equivalent to SQL CASE-WHEN-ELSE chain.
      /// Is strongly typed version of <see cref="T:PX.Data.Switch`2" />.
      /// </summary>
      public sealed class Else<TOperand> : 
        Switch<TBqlType, TypeArrayOf<IBqlCase>.Empty, Case2<Where<TCondition>, TSelf>, TOperand>,
        IDoNotConvert
        where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
      {
      }

      /// <summary>
      /// Finishes the ternary operator expression.
      /// Equivalent to SQL CASE-WHEN chain.
      /// Is strongly typed version of <see cref="T:PX.Data.Switch`1" />.
      /// </summary>
      public sealed class ElseNull : 
        BqlSwitchFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.FilledWith<Case2<Where<TCondition>, TSelf>>, Null>, TBqlType>,
        IDoNotConvert
      {
      }
    }
  }
}
