// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Descriptor.Attributes.PXDBOpportunityProductQuantityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.CR.Descriptor.Attributes;

public class PXDBOpportunityProductQuantityAttribute(
  System.Type keyField,
  System.Type resultField,
  InventoryUnitType decimalVerifyUnits) : PXDBQuantityAttribute(keyField, resultField, decimalVerifyUnits)
{
  protected override Decimal? CalcResultValue(
    PXCache sender,
    PXDBQuantityAttribute.QtyConversionArgs e)
  {
    Decimal? baseValue = new Decimal?();
    if (this._ResultField != (System.Type) null && e.NewValue != null)
    {
      bool flag = false;
      if (this._HandleEmptyKey && string.IsNullOrEmpty(this.GetFromUnit(sender, e.Row)))
      {
        baseValue = new Decimal?((Decimal) e.NewValue);
        flag = true;
      }
      if (!flag)
      {
        if ((Decimal) e.NewValue == 0M)
        {
          baseValue = new Decimal?(0M);
        }
        else
        {
          ConversionInfo conversionInfo = this.ReadConversionInfo(sender, e.Row);
          if (conversionInfo?.Conversion != null)
          {
            baseValue = this.ConvertValue(sender, e.Row, new Decimal?((Decimal) e.NewValue), conversionInfo.Conversion);
            PXNotDecimalUnitException decimalUnitException = this.VerifyForDecimalValue(sender, conversionInfo.Inventory, e.Row, new Decimal?((Decimal) e.NewValue), baseValue);
            if (decimalUnitException != null && decimalUnitException.ErrorLevel == 4 && e.ThrowNotDecimalUnitException && !decimalUnitException.IsLazyThrow)
              throw decimalUnitException;
          }
          else if ((conversionInfo != null ? (!conversionInfo.Inventory.InventoryID.HasValue ? 1 : 0) : 1) != 0)
            baseValue = new Decimal?((Decimal) e.NewValue);
          else if (!e.ExternalCall)
            throw new PXUnitConversionException(this.GetFromUnit(sender, e.Row));
        }
      }
    }
    return baseValue;
  }
}
