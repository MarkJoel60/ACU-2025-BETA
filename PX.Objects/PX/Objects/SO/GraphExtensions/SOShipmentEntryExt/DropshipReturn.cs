// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.DropshipReturn
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class DropshipReturn : PXGraphExtension<SOShipmentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  [PXOverride]
  public virtual IEnumerable<PXResult<SOShipLine, PX.Objects.SO.SOLine>> CollectDropshipDetails(
    PX.Objects.SO.SOOrderShipment shipment,
    Func<PX.Objects.SO.SOOrderShipment, IEnumerable<PXResult<SOShipLine, PX.Objects.SO.SOLine>>> baseFunc)
  {
    DropshipReturn dropshipReturn = this;
    if (shipment.Operation != "R")
    {
      foreach (PXResult<SOShipLine, PX.Objects.SO.SOLine> pxResult in baseFunc(shipment))
        yield return pxResult;
    }
    else
    {
      SOShipmentEntry soShipmentEntry = dropshipReturn.Base;
      object[] objArray1 = new object[1]
      {
        (object) shipment
      };
      object[] objArray2 = new object[2]
      {
        shipment.Operation == "R" ? (object) "RN" : (object) "RT",
        (object) shipment.ShipmentNbr
      };
      foreach (PXResult<POReceiptLine, PX.Objects.SO.SOLine> pxResult in PXSelectBase<POReceiptLine, PXSelectJoin<POReceiptLine, InnerJoin<PX.Objects.SO.SOLine, On<POReceiptLine.FK.SOLine>>, Where<POReceiptLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<POReceiptLine.receiptType, Equal<Required<POReceiptLine.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceiptLine.receiptNbr>>, And<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.orderNbr>>>>>>>>.Config>.SelectMultiBound((PXGraph) soShipmentEntry, objArray1, objArray2))
        yield return new PXResult<SOShipLine, PX.Objects.SO.SOLine>(SOShipLine.FromDropShip(PXResult<POReceiptLine, PX.Objects.SO.SOLine>.op_Implicit(pxResult), PXResult<POReceiptLine, PX.Objects.SO.SOLine>.op_Implicit(pxResult)), PXResult<POReceiptLine, PX.Objects.SO.SOLine>.op_Implicit(pxResult));
    }
  }
}
