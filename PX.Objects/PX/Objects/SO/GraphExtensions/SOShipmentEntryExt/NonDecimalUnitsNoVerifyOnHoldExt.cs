// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.NonDecimalUnitsNoVerifyOnHoldExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class NonDecimalUnitsNoVerifyOnHoldExt : 
  NonDecimalUnitsNoVerifyOnHoldExt<SOShipmentEntry, SOShipment, SOShipLine, SOShipLine.shippedQty, SOShipLineSplit, SOShipLineSplit.qty>
{
  public override bool HaveHoldStatus(SOShipment doc) => doc.Hold.GetValueOrDefault();

  public override int? GetLineNbr(SOShipLine line) => line.LineNbr;

  public override int? GetLineNbr(SOShipLineSplit split) => split.LineNbr;

  public override IEnumerable<SOShipLine> GetLines()
  {
    return GraphHelper.RowCast<SOShipLine>((IEnumerable) ((PXSelectBase<SOShipLine>) this.Base.Transactions).Select(Array.Empty<object>()));
  }

  public override IEnumerable<SOShipLineSplit> GetSplits()
  {
    return GraphHelper.RowCast<SOShipLineSplit>((IEnumerable) PXSelectBase<SOShipLineSplit, PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override SOShipLine LocateLine(SOShipLineSplit split)
  {
    return (SOShipLine) ((PXSelectBase) this.Base.Transactions).Cache.Locate((object) new SOShipLine()
    {
      ShipmentNbr = split.ShipmentNbr,
      ShipmentType = ((PXSelectBase<SOShipment>) this.Base.CurrentDocument).Current.ShipmentType,
      LineNbr = split.LineNbr
    });
  }
}
