// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.NonDecimalUnitsNoVerifyOnDropShipExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;
using PX.Objects.IN;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

/// <summary>
/// Disabling of validation for decimal values for drop ship lines in PO Receipt
/// </summary>
public class NonDecimalUnitsNoVerifyOnDropShipExt : 
  NonDecimalUnitsNoVerifyOnDropShipExt<POReceiptEntry, PX.Objects.PO.POReceiptLine>
{
  protected override bool IsDropShipLine(PX.Objects.PO.POReceiptLine line)
  {
    return POLineType.IsDropShip(line.LineType);
  }

  protected override DecimalVerifyMode GetLineVerifyMode(PXCache cache, PX.Objects.PO.POReceiptLine line)
  {
    if (!this.IsDropShipLine(line))
    {
      PXDBQuantityAttribute quantityAttribute = cache.GetAttributesOfType<PXDBQuantityAttribute>((object) null, "receiptQty").FirstOrDefault<PXDBQuantityAttribute>();
      if (quantityAttribute != null)
        return quantityAttribute.DecimalVerifyMode;
    }
    return DecimalVerifyMode.Off;
  }
}
