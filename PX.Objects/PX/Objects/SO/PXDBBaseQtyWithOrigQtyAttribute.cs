// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PXDBBaseQtyWithOrigQtyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

public class PXDBBaseQtyWithOrigQtyAttribute : PXDBBaseQuantityAttribute
{
  private Type _origUomField;
  private Type _baseOrigQtyField;
  private Type _origQtyField;

  public PXDBBaseQtyWithOrigQtyAttribute(
    Type uomField,
    Type qtyField,
    Type origUomField,
    Type baseOrigQtyField,
    Type origQtyField)
    : base(uomField, qtyField)
  {
    this._origUomField = origUomField ?? throw new ArgumentException(nameof (origUomField));
    this._baseOrigQtyField = baseOrigQtyField ?? throw new ArgumentException(nameof (baseOrigQtyField));
    this._origQtyField = origQtyField ?? throw new ArgumentException(nameof (origQtyField));
  }

  protected override Decimal? CalcResultValue(
    PXCache sender,
    PXDBQuantityAttribute.QtyConversionArgs e)
  {
    object objA = sender.GetValue(e.Row, this.KeyField.Name);
    object obj = sender.GetValue(e.Row, this._origUomField.Name);
    object newValue = e.NewValue;
    object objB1 = sender.GetValue(e.Row, this._baseOrigQtyField.Name);
    object objB2 = obj;
    if (object.Equals(objA, objB2) && object.Equals(newValue, objB1))
    {
      Decimal? nullable = (Decimal?) newValue;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        return (Decimal?) sender.GetValue(e.Row, this._origQtyField.Name);
    }
    return base.CalcResultValue(sender, e);
  }
}
