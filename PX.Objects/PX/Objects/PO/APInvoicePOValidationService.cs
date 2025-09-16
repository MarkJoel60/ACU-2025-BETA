// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.APInvoicePOValidationService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using PX.Objects.PO.GraphExtensions.APInvoiceEntryExt;
using System;

#nullable disable
namespace PX.Objects.PO;

public class APInvoicePOValidationService
{
  protected Lazy<POSetup> _poSetup;

  public APInvoicePOValidationService(Lazy<POSetup> poSetup) => this._poSetup = poSetup;

  public virtual bool IsLineValidationRequired(PX.Objects.AP.APTran tran)
  {
    return tran != null && tran.TranType == "INV" && tran.Sign > 0M && tran.POOrderType != null && tran.PONbr != null && tran.POLineNbr.HasValue && this._poSetup.Value.APInvoiceValidation == "W";
  }

  public virtual bool ShouldCreateRevision(
    PXCache cache,
    PX.Objects.AP.APTran tran,
    string apTranCuryID,
    APInvoicePOValidation.POLineDTO poLine)
  {
    if (!this.IsLineValidationRequired(tran))
      return false;
    return this.IsAPTranQtyExceedsPOLineUnbilledQty(tran, poLine) || this.IsAPTranUnitCostExceedsPOLineUnitCost(cache, tran, apTranCuryID, poLine) || this.IsAPTranAmountExceedsPOLineUnbilledAmount(tran, apTranCuryID, poLine);
  }

  public virtual bool IsAPTranQtyExceedsPOLineUnbilledQty(
    PX.Objects.AP.APTran tran,
    APInvoicePOValidation.POLineDTO poLine)
  {
    int num = tran.UOM == poLine.UOM ? 1 : 0;
    return (num != 0 ? tran.Qty.GetValueOrDefault() : tran.BaseQty.GetValueOrDefault()) > (num != 0 ? poLine.UnbilledQty.GetValueOrDefault() : poLine.BaseUnbilledQty.GetValueOrDefault());
  }

  public virtual bool IsAPTranUnitCostExceedsPOLineUnitCost(
    PXCache cache,
    PX.Objects.AP.APTran tran,
    string apTranCuryID,
    APInvoicePOValidation.POLineDTO poLine)
  {
    bool flag1 = tran.UOM == poLine.UOM;
    bool flag2 = apTranCuryID == poLine.CuryID;
    Decimal num1;
    Decimal num2;
    if (tran.InventoryID.HasValue && poLine.InventoryID.HasValue)
    {
      num1 = flag2 & flag1 ? tran.CuryUnitCost.GetValueOrDefault() : INUnitAttribute.ConvertFromBase(cache, tran.InventoryID, tran.UOM, tran.UnitCost.GetValueOrDefault(), INPrecision.UNITCOST);
      num2 = flag2 & flag1 ? poLine.CuryUnitCost.GetValueOrDefault() : INUnitAttribute.ConvertFromBase(cache, tran.InventoryID, poLine.UOM, poLine.UnitCost.GetValueOrDefault(), INPrecision.UNITCOST);
    }
    else
    {
      num1 = flag2 & flag1 ? tran.CuryUnitCost.GetValueOrDefault() : tran.UnitCost.GetValueOrDefault();
      num2 = flag2 & flag1 ? poLine.CuryUnitCost.GetValueOrDefault() : INUnitAttribute.ConvertGlobal(cache.Graph, poLine.UOM, tran.UOM, poLine.UnitCost.GetValueOrDefault(), INPrecision.UNITCOST);
    }
    return num1 > num2;
  }

  public virtual bool IsAPTranAmountExceedsPOLineUnbilledAmount(
    PX.Objects.AP.APTran tran,
    string apTranCuryID,
    APInvoicePOValidation.POLineDTO poLine)
  {
    int num1 = apTranCuryID == poLine.CuryID ? 1 : 0;
    Decimal? nullable;
    Decimal valueOrDefault1;
    if (num1 == 0)
    {
      nullable = tran.TranAmt;
      valueOrDefault1 = nullable.GetValueOrDefault();
    }
    else
    {
      nullable = tran.CuryTranAmt;
      valueOrDefault1 = nullable.GetValueOrDefault();
    }
    Decimal num2 = valueOrDefault1;
    Decimal valueOrDefault2;
    if (num1 == 0)
    {
      nullable = poLine.UnbilledAmt;
      valueOrDefault2 = nullable.GetValueOrDefault();
    }
    else
    {
      nullable = poLine.CuryUnbilledAmt;
      valueOrDefault2 = nullable.GetValueOrDefault();
    }
    Decimal num3 = valueOrDefault2;
    return num2 > num3;
  }
}
