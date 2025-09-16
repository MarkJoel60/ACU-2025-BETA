// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.NonDecimalUnitsNoVerifyOnHoldExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class NonDecimalUnitsNoVerifyOnHoldExt : 
  NonDecimalUnitsNoVerifyOnHoldExt<POReceiptEntry, POReceipt, POReceiptLine, POReceiptLine.receiptQty, POReceiptLineSplit, POReceiptLineSplit.qty>
{
  private NonDecimalUnitsNoVerifyOnDropShipExt _nonDecimalUnitsNoVerifyOnDropShipExt;

  protected NonDecimalUnitsNoVerifyOnDropShipExt NonDecimalUnitsNoVerifyOnDropShipExt
  {
    get
    {
      return this._nonDecimalUnitsNoVerifyOnDropShipExt = this._nonDecimalUnitsNoVerifyOnDropShipExt ?? ((PXGraph) this.Base).FindImplementation<NonDecimalUnitsNoVerifyOnDropShipExt>();
    }
  }

  public override bool HaveHoldStatus(POReceipt doc) => doc.Hold.GetValueOrDefault();

  public override int? GetLineNbr(POReceiptLine line) => line.LineNbr;

  public override int? GetLineNbr(POReceiptLineSplit split) => split.LineNbr;

  public override IEnumerable<POReceiptLine> GetLines()
  {
    return GraphHelper.RowCast<POReceiptLine>((IEnumerable) ((PXSelectBase<POReceiptLine>) this.Base.transactions).Select(Array.Empty<object>()));
  }

  protected override POReceiptLine LocateLine(POReceiptLineSplit split)
  {
    return (POReceiptLine) ((PXSelectBase) this.Base.transactions).Cache.Locate((object) new POReceiptLine()
    {
      ReceiptType = split.ReceiptType,
      ReceiptNbr = split.ReceiptNbr,
      LineNbr = split.LineNbr
    });
  }

  public override IEnumerable<POReceiptLineSplit> GetSplits()
  {
    return GraphHelper.RowCast<POReceiptLineSplit>((IEnumerable) PXSelectBase<POReceiptLineSplit, PXSelect<POReceiptLineSplit, Where<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POReceiptLineSplit>, POReceipt, POReceiptLineSplit>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected override void VerifyLine(PXCache lineCache, POReceiptLine line)
  {
    this.NonDecimalUnitsNoVerifyOnDropShipExt.SetDecimalVerifyMode(lineCache, line);
    base.VerifyLine(lineCache, line);
  }
}
