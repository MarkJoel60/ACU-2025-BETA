// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlField`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <exclude />
public abstract class BqlField<TSelf, TBqlType> : BqlOperand<TSelf, TBqlType>, IBqlField, IBqlOperand
  where TSelf : class, IBqlField
  where TBqlType : class, IBqlDataType
{
  /// <summary>
  /// Indicates that the field is contained in a specific <typeparamref name="TTable" />.
  /// </summary>
  public abstract class Of<TTable> : 
    BqlField<TSelf, TBqlType>,
    IBqlFieldOf<TTable>,
    IBqlField,
    IBqlOperand
    where TTable : IBqlTable
  {
  }

  /// <summary>
  /// Inserts the field value from the <tt>Current</tt> property of the cache. If the <tt>Current</tt> property is null or the field value is null, the parameter will be replaced by the default value.
  /// </summary>
  public sealed class FromCurrent : BqlParameter<Current<TSelf>, TBqlType>
  {
    /// <summary>
    /// The default value will not be inserted if NULL was passed to the parameter.
    /// </summary>
    public sealed class NoDefault : BqlParameter<Current2<TSelf>, TBqlType>
    {
    }

    /// <summary>
    /// Allows to properly use Current parameters in the <tt>PXProjection</tt> attribute.
    /// </summary>
    public sealed class Value : BqlOperand<CurrentValue<TSelf>, TBqlType>
    {
    }
  }

  /// <summary>
  /// Inserts the <tt>Current</tt> property value of the cache or the value explicitly passed to the <tt>Select()</tt> method. In the latter case, the
  /// parameter causes raising of the <tt>FieldUpdating</tt> event for the specified field (which can modify or substitute the value). If the null value is passed or
  /// the <tt>Current</tt> property is null, the default value of the field is inserted.
  /// </summary>
  public sealed class AsOptional : BqlParameter<Optional<TSelf>, TBqlType>
  {
    /// <summary>
    /// The default value will not be inserted if NULL was passed to the parameter.
    /// </summary>
    public sealed class NoDefault : BqlParameter<Optional2<TSelf>, TBqlType>
    {
    }
  }

  /// <summary>
  /// Allows you to refer to the field that is created by applying the <see cref="T:PX.Data.GroupBy`1" /> aggregation function to the preceding field, in a <see cref="T:PX.Data.Having`1" /> clause.
  /// </summary>
  public sealed class Grouped : BqlAggregatedOperand<GroupBy<TSelf>, TBqlType>
  {
  }

  /// <summary>
  /// Allows you to refer to the field that is created by applying the <see cref="T:PX.Data.Sum`1" /> aggregation function to the preceding field, in a <see cref="T:PX.Data.Having`1" /> clause.
  /// </summary>
  public sealed class Summarized : BqlAggregatedOperand<Sum<TSelf>, TBqlType>
  {
  }

  /// <summary>
  /// Allows you to refer to the field that is created by applying the <see cref="T:PX.Data.Max`1" /> aggregation function to the preceding field, in a <see cref="T:PX.Data.Having`1" /> clause.
  /// </summary>
  public sealed class Maximized : BqlAggregatedOperand<Max<TSelf>, TBqlType>
  {
  }

  /// <summary>
  /// Allows you to refer to the field that is created by applying the <see cref="T:PX.Data.Min`1" /> aggregation function to the preceding field, in a <see cref="T:PX.Data.Having`1" /> clause.
  /// </summary>
  public sealed class Minimized : BqlAggregatedOperand<Min<TSelf>, TBqlType>
  {
  }

  /// <summary>
  /// Allows you to refer to the field that is created by applying the <see cref="T:PX.Data.Avg`1" /> aggregation function to the preceding field, in a <see cref="T:PX.Data.Having`1" /> clause.
  /// </summary>
  public sealed class Averaged : BqlAggregatedOperand<Avg<TSelf>, TBqlType>
  {
  }

  /// <summary>
  /// Allows you to refer to the field that is created by applying the <see cref="T:PX.Data.Count`1" /> aggregation function to the preceding field, in a <see cref="T:PX.Data.Having`1" /> clause.
  /// </summary>
  public sealed class Counted : BqlAggregatedOperand<Count<TSelf>, IBqlInt>
  {
  }

  public sealed class Asc : 
    AscDescBase<TSelf, BqlNone>,
    IBqlSortColumnAscending,
    IBqlSortColumnDirected
  {
    public override bool IsDescending => false;
  }

  public sealed class Desc : 
    AscDescBase<TSelf, BqlNone>,
    IBqlSortColumnDescending,
    IBqlSortColumnDirected
  {
    public override bool IsDescending => true;
  }

  /// <summary>
  /// Sets the preceding field equal to <typeparamref name="TOperand" />.
  /// Should be used within the <tt>Set</tt> section of the <tt>Update</tt> command.
  /// Use <tt>IsEqual</tt> for comparison.
  /// </summary>
  public sealed class EqualTo<TOperand> : SetBase<TSelf, TOperand, BqlNone>, IFbqlSet, IBqlSet where TOperand : IBqlOperand, IImplement<IBqlCastableTo<TBqlType>>
  {
  }

  /// <summary>Returns the value of the specified field from the setup data record.
  /// The setup data record is obtained by the <see cref="M:PX.Data.PXSetup`1.Select(PX.Data.PXGraph,System.Object[])" /> method.</summary>
  /// <example>
  ///     <code>[PXFormula(typeof(SOSetup.autoReleaseIN.FromSetup.IsEqual&lt;True&gt;))]</code>
  /// </example>
  public sealed class FromSetup : BqlFunction<Setup<TSelf>, TBqlType>
  {
  }

  /// <summary>Returns the value of the specified field from the parent data record.
  /// The parent data record is defined by the <see cref="T:PX.Data.PXParentAttribute">PXParent</see> attribute.</summary>
  /// <example>
  ///   <code>[PXFormula(typeof(SOShipment.shipmentType.FromParent.
  ///   IsEqual&lt;SOShipmentType.transfer&gt;))]</code>
  /// </example>
  public sealed class FromParent : BqlFunction<Parent<TSelf>, TBqlType>
  {
  }
}
