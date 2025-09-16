// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLotSerialNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PO;

public class POLotSerialNbrAttribute : INLotSerialNbrAttribute
{
  public POLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type ParentLotSerialNbrType,
    Type CostCenterType)
    : base(InventoryType, SubItemType, LocationType, ParentLotSerialNbrType, CostCenterType)
  {
  }

  public POLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type CostCenterType)
    : base(InventoryType, SubItemType, LocationType, CostCenterType)
  {
  }

  protected POLotSerialNbrAttribute()
  {
  }

  protected override bool IsTracked(
    ILSMaster row,
    INLotSerClass lotSerClass,
    string tranType,
    int? invMult)
  {
    POReceiptLineSplit receiptLineSplit = row as POReceiptLineSplit;
    if (tranType == "III" && lotSerClass.LotSerAssign == "U")
      return false;
    return receiptLineSplit != null && EnumerableExtensions.IsIn<string>(receiptLineSplit.LineType, "GP", "PG") || base.IsTracked(row, lotSerClass, tranType, invMult);
  }

  private bool GetPOReceiptLineLotSerialNbrRequiredForDropship(
    PXCache cache,
    object row,
    INLotSerClass inLotSerClass)
  {
    POReceiptLine poReceiptLine;
    switch (row)
    {
      case POReceiptLineSplit _:
      case PX.Objects.PO.Unassigned.POReceiptLineSplit _:
      case PX.Objects.PO.Reverse.POReceiptLineSplit _:
      case PX.Objects.PO.Table.POReceiptLineSplit _:
        poReceiptLine = PXParentAttribute.SelectParent<POReceiptLine>(cache, row);
        break;
      case POReceiptLine _:
        poReceiptLine = (POReceiptLine) row;
        break;
      default:
        return inLotSerClass.RequiredForDropship.GetValueOrDefault();
    }
    return poReceiptLine.LotSerialNbrRequiredForDropship.GetValueOrDefault();
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, ((ILSMaster) e.Row).InventoryID);
    if (pxResult == null || !((ILSMaster) e.Row).SubItemID.HasValue || !((ILSMaster) e.Row).LocationID.HasValue)
      return;
    if (EnumerableExtensions.IsIn<string>((string) sender.GetValue<POReceiptLineSplit.lineType>(e.Row), "GP", "PG"))
    {
      if (!this.GetPOReceiptLineLotSerialNbrRequiredForDropship(sender, e.Row, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult)))
        return;
      ILSMaster row = (ILSMaster) e.Row;
      INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
      string tranType = ((ILSMaster) e.Row).TranType;
      short? invtMult = ((ILSMaster) e.Row).InvtMult;
      int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      if (!this.IsTracked(row, lotSerClass, tranType, invMult))
        return;
      Decimal? qty = ((ILSMaster) e.Row).Qty;
      Decimal num = 0M;
      if (qty.GetValueOrDefault() == num & qty.HasValue)
        return;
      ((IPXRowPersistingSubscriber) ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex]).RowPersisting(sender, e);
    }
    else
      base.RowPersisting(sender, e);
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (POLineType.IsProjectDropShip((string) sender.GetValue<POReceiptLineSplit.lineType>(e.Row)))
    {
      PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, ((ILSMaster) e.Row).InventoryID);
      if (pxResult != null)
      {
        bool requiredForDropship = this.GetPOReceiptLineLotSerialNbrRequiredForDropship(sender, e.Row, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
        if (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack != "N" & requiredForDropship)
          return;
      }
    }
    base.FieldVerifying(sender, e);
  }
}
