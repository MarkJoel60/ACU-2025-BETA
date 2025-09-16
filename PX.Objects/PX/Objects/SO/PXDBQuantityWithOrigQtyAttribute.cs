// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PXDBQuantityWithOrigQtyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// This PXDBQuantityWithOrigQtyAttribute is introduced to resolve
/// potential rounding issues that may occur due to double unit conversions
/// like from base UOM -&gt; line UOM -&gt; base UOM.
/// 
/// For example consider following chain of conversions
/// BaseShippedQty(baseOrigQty) =&gt; ShippedQty(origQty) =&gt; UnbilledQty(qty) =&gt; BaseUnbilledQty(baseQty)
/// </summary>
public class PXDBQuantityWithOrigQtyAttribute : PXDBQuantityAttribute
{
  private Type _origQtyField;
  private Type _baseOrigQtyField;

  public PXDBQuantityWithOrigQtyAttribute(
    Type uomField,
    Type baseQtyField,
    Type origQtyField,
    Type baseOrigQtyField)
    : base(uomField, baseQtyField)
  {
    this._origQtyField = origQtyField ?? throw new ArgumentException(nameof (origQtyField));
    this._baseOrigQtyField = baseOrigQtyField ?? throw new ArgumentException(nameof (baseOrigQtyField));
  }

  protected override Decimal? CalcResultValue(
    PXCache sender,
    PXDBQuantityAttribute.QtyConversionArgs e)
  {
    object objB = sender.GetValue(e.Row, this._origQtyField.Name);
    object obj = sender.GetValue(e.Row, this._baseOrigQtyField.Name);
    return object.Equals(e.NewValue, objB) ? (Decimal?) obj : base.CalcResultValue(sender, e);
  }
}
