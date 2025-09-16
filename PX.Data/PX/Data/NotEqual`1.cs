// Decompiled with JetBrains decompiler
// Type: PX.Data.NotEqual`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Checks if the preceding operand is not equal to <tt>Operand</tt>.
/// </summary>
/// <typeparam name="Operand">The operand to compare to.</typeparam>
/// <example><para>The code below shows the definition of a DAC field. The NotEqual&lt;&gt; class is used in the BQL command that retrieves the data records that can be selected in the lookup control.</para>
/// 	<code title="Example" lang="CS">
/// [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = "&gt;CCCCCCCCCCCCCCC")]
/// [PXDefault()]
/// [PXUIField(DisplayName = "Discount Code")]
/// [PXSelector(typeof(
///     Search&lt;APDiscount.discountID,
///         Where&lt;APDiscount.type, NotEqual&lt;DiscountType.LineDiscount&gt;&gt;&gt;))]
/// public virtual String DiscountID { get; set; }</code>
/// </example>
public class NotEqual<Operand> : ComparisonBase<Operand> where Operand : IBqlOperand
{
  protected override bool? verifyCore(object val, object value)
  {
    return new bool?(!this.collationComparer.Equals(val, value));
  }

  protected override bool isBypass(object val) => false;

  /// <exclude />
  public NotEqual()
    : base("<>", true)
  {
  }

  /// <exclude />
  public NotEqual(IBqlOperand op)
    : base("<>", true, op as IBqlCreator)
  {
  }
}
