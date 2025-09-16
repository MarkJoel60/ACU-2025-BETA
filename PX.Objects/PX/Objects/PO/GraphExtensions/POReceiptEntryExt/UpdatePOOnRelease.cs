// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.UpdatePOOnRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class UpdatePOOnRelease : 
  BaseUpdatePOAccrual<POReceiptEntry.MultiCurrency, POReceiptEntry, PX.Objects.PO.POReceipt>
{
  public PXSelect<POAccrualStatus> poAccrualUpdate;
  public PXSelect<POAccrualDetail> poAccrualDetailUpdate;
  public PXSelect<POAccrualSplit> poAccrualSplitUpdate;
  public PXSelect<APTranReceiptUpdate, Where<APTranReceiptUpdate.pOAccrualType, Equal<Current<POAccrualStatus.type>>, And<APTranReceiptUpdate.pOOrderType, Equal<Current<POAccrualStatus.orderType>>, And<APTranReceiptUpdate.pONbr, Equal<Current<POAccrualStatus.orderNbr>>, And<APTranReceiptUpdate.pOLineNbr, Equal<Current<POAccrualStatus.orderLineNbr>>, And<APTranReceiptUpdate.released, Equal<True>, And<APTranReceiptUpdate.unreceivedQty, Greater<decimal0>, And<APTranReceiptUpdate.baseUnreceivedQty, Greater<decimal0>>>>>>>>> apTranUpdate;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() || PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.GetTrackedExceptChildren" />
  /// </summary>
  [PXOverride]
  public virtual PXSelectBase[] GetTrackedExceptChildren(Func<PXSelectBase[]> baseImpl)
  {
    return ((IEnumerable<PXSelectBase>) baseImpl()).Union<PXSelectBase>((IEnumerable<PXSelectBase>) new PXSelectBase[1]
    {
      (PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.poline
    }).ToArray<PXSelectBase>();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.ReleaseReceiptLine(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POLine,PX.Objects.PO.POOrder)" />
  /// </summary>
  [PXOverride]
  public virtual void ReleaseReceiptLine(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    PX.Objects.PO.POOrder poOrder,
    Action<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine, PX.Objects.PO.POOrder> baseImpl)
  {
    if (line.ReceiptType == "RX")
    {
      this.SetReceiptCostFinal(line, (POAccrualStatus) null);
    }
    else
    {
      POAccrualStatus dirty = POAccrualStatus.PK.FindDirty((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, line.POAccrualRefNoteID, line.POAccrualLineNbr, line.POAccrualType);
      this.SetReceiptCostFinal(line, dirty);
      this.UpdatePOAccrualStatus(dirty, poLine, line, poOrder, ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current);
      this.UpdatePOReceiptLineAccrualDetail(line);
    }
    baseImpl(line, poLine, poOrder);
  }

  [PXOverride]
  public PX.Objects.PO.POLine UpdateReceiptLineOnRelease(
    PXResult<PX.Objects.PO.POReceiptLine> row,
    PX.Objects.PO.POLine poLine,
    Func<PXResult<PX.Objects.PO.POReceiptLine>, PX.Objects.PO.POLine, PX.Objects.PO.POLine> base_UpdateReceiptLineOnRelease)
  {
    poLine = base_UpdateReceiptLineOnRelease(row, poLine);
    return this.UpdatePOLineOnReceipt(row, poLine);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.UpdateReceiptReleased(PX.Objects.IN.INRegister)" />
  /// </summary>
  [PXOverride]
  public virtual PX.Objects.PO.POReceipt UpdateReceiptReleased(
    PX.Objects.IN.INRegister inRegister,
    Func<PX.Objects.IN.INRegister, PX.Objects.PO.POReceipt> baseImpl)
  {
    PX.Objects.PO.POReceipt poReceipt = baseImpl(inRegister);
    if (poReceipt.ReceiptType != "RX" && (inRegister != null || poReceipt.POType == "DP"))
      this.IncUnreleasedReceiptCntr(poReceipt);
    return poReceipt;
  }

  public virtual PX.Objects.PO.POLine UpdatePOLineOnReceipt(
    PXResult<PX.Objects.PO.POReceiptLine> res,
    PX.Objects.PO.POLine poLine)
  {
    if (poLine == null || string.IsNullOrEmpty(poLine.OrderType) || string.IsNullOrEmpty(poLine.OrderNbr) || !poLine.LineNbr.HasValue)
      return poLine;
    PX.Objects.PO.POReceiptLine line = PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(res);
    PX.Objects.PO.POOrder poOrder = PXResult.Unwrap<PX.Objects.PO.POOrder>((object) res);
    poLine = PXCache<PX.Objects.PO.POLine>.CreateCopy(poLine);
    PX.Objects.PO.POLine poLine1 = poLine;
    Decimal? completedQty = poLine1.CompletedQty;
    Decimal? nullable1 = this.GetCompletedQtyDelta(line, poLine);
    poLine1.CompletedQty = completedQty.HasValue & nullable1.HasValue ? new Decimal?(completedQty.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    if (!poLine.Completed.GetValueOrDefault() || !poLine.Closed.GetValueOrDefault())
    {
      bool flag1 = false;
      bool flag2 = false;
      int num1 = PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectReadonly<PX.Objects.PO.POReceiptLine, Where<PX.Objects.PO.POReceiptLine.pOType, Equal<Required<PX.Objects.PO.POReceiptLine.pOType>>, And<PX.Objects.PO.POReceiptLine.pONbr, Equal<Required<PX.Objects.PO.POReceiptLine.pONbr>>, And<PX.Objects.PO.POReceiptLine.pOLineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.pOLineNbr>>, And<PX.Objects.PO.POReceiptLine.released, Equal<False>, And<Where<PX.Objects.PO.POReceiptLine.receiptType, NotEqual<Required<PX.Objects.PO.POReceiptLine.receiptType>>, Or<PX.Objects.PO.POReceiptLine.receiptNbr, NotEqual<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>>>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, new object[5]
      {
        (object) line.POType,
        (object) line.PONbr,
        (object) line.POLineNbr,
        (object) line.ReceiptType,
        (object) line.ReceiptNbr
      }).Count > 0 ? 1 : 0;
      if (num1 == 0 && poLine.AllowComplete.GetValueOrDefault())
        flag1 = true;
      if (num1 == 0 && poLine.POAccrualType == "O")
      {
        POAccrualRecord accrualStatusSummary = this.GetAccrualStatusSummary(poLine);
        int num2;
        if (accrualStatusSummary.BilledUOM == null || !(accrualStatusSummary.BilledUOM == accrualStatusSummary.ReceivedUOM))
        {
          nullable1 = accrualStatusSummary.BaseBilledQty;
          Decimal? baseReceivedQty = accrualStatusSummary.BaseReceivedQty;
          num2 = nullable1.GetValueOrDefault() == baseReceivedQty.GetValueOrDefault() & nullable1.HasValue == baseReceivedQty.HasValue ? 1 : 0;
        }
        else
        {
          Decimal? billedQty = accrualStatusSummary.BilledQty;
          nullable1 = accrualStatusSummary.ReceivedQty;
          num2 = billedQty.GetValueOrDefault() == nullable1.GetValueOrDefault() & billedQty.HasValue == nullable1.HasValue ? 1 : 0;
        }
        bool flag3 = poLine.CompletePOLine == "Q";
        bool flag4;
        if (flag3)
        {
          if (flag1)
          {
            flag4 = true;
          }
          else
          {
            int num3;
            if (accrualStatusSummary.BilledUOM == null || !(accrualStatusSummary.BilledUOM == poLine.UOM))
            {
              Decimal? baseOrderQty = poLine.BaseOrderQty;
              Decimal? rcptQtyThreshold = poLine.RcptQtyThreshold;
              nullable1 = baseOrderQty.HasValue & rcptQtyThreshold.HasValue ? new Decimal?(baseOrderQty.GetValueOrDefault() * rcptQtyThreshold.GetValueOrDefault() / 100M) : new Decimal?();
              Decimal? baseBilledQty = accrualStatusSummary.BaseBilledQty;
              num3 = nullable1.GetValueOrDefault() <= baseBilledQty.GetValueOrDefault() & nullable1.HasValue & baseBilledQty.HasValue ? 1 : 0;
            }
            else
            {
              Decimal? orderQty = poLine.OrderQty;
              Decimal? rcptQtyThreshold = poLine.RcptQtyThreshold;
              Decimal? nullable2 = orderQty.HasValue & rcptQtyThreshold.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * rcptQtyThreshold.GetValueOrDefault() / 100M) : new Decimal?();
              nullable1 = accrualStatusSummary.BilledQty;
              num3 = nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue ? 1 : 0;
            }
            flag4 = num3 != 0;
          }
        }
        else if (accrualStatusSummary.BillCuryID != null && accrualStatusSummary.BillCuryID == poOrder.CuryID)
        {
          Decimal? curyExtCost = poLine.CuryExtCost;
          Decimal? curyRetainageAmt = poLine.CuryRetainageAmt;
          nullable1 = curyExtCost.HasValue & curyRetainageAmt.HasValue ? new Decimal?(curyExtCost.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
          Decimal? rcptQtyThreshold = poLine.RcptQtyThreshold;
          Decimal? nullable3 = nullable1.HasValue & rcptQtyThreshold.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * rcptQtyThreshold.GetValueOrDefault() / 100M) : new Decimal?();
          flag4 = nullable3.HasValue && accrualStatusSummary.CuryBilledAmt.HasValue && Math.Sign(nullable3.Value) == Math.Sign(accrualStatusSummary.CuryBilledAmt.Value) && Math.Abs(nullable3.Value) <= Math.Abs(accrualStatusSummary.CuryBilledAmt.Value);
        }
        else
        {
          Decimal? extCost = poLine.ExtCost;
          Decimal? retainageAmt = poLine.RetainageAmt;
          Decimal? nullable4 = extCost.HasValue & retainageAmt.HasValue ? new Decimal?(extCost.GetValueOrDefault() + retainageAmt.GetValueOrDefault()) : new Decimal?();
          nullable1 = poLine.RcptQtyThreshold;
          Decimal? nullable5 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
          int num4;
          if (nullable5.HasValue)
          {
            nullable1 = accrualStatusSummary.BilledAmt;
            if (nullable1.HasValue)
            {
              int num5 = Math.Sign(nullable5.Value);
              nullable1 = accrualStatusSummary.BilledAmt;
              int num6 = Math.Sign(nullable1.Value);
              if (num5 == num6)
              {
                Decimal num7 = Math.Abs(nullable5.Value);
                nullable1 = accrualStatusSummary.BilledAmt;
                Decimal num8 = Math.Abs(nullable1.Value);
                num4 = num7 <= num8 ? 1 : 0;
                goto label_23;
              }
            }
          }
          num4 = 0;
label_23:
          flag4 = num4 != 0;
        }
        int num9 = flag4 ? 1 : 0;
        if ((num2 & num9) != 0 && (flag1 || !flag3))
          flag1 = flag2 = true;
      }
      if (flag1)
      {
        poLine.Completed = new bool?(true);
        if (flag2)
          poLine.Closed = new bool?(true);
      }
    }
    if (POLineType.UsePOAccrual(poLine.LineType) && !poLine.POAccrualAcctID.HasValue && !poLine.POAccrualSubID.HasValue)
    {
      poLine.POAccrualAcctID = line.POAccrualAcctID;
      poLine.POAccrualSubID = line.POAccrualSubID;
    }
    return ((PXSelectBase<PX.Objects.PO.POLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.poline).Update(poLine);
  }

  protected virtual Decimal? GetCompletedQtyDelta(PX.Objects.PO.POReceiptLine line, PX.Objects.PO.POLine poLine)
  {
    Decimal num1 = line.ReceiptQty.GetValueOrDefault();
    if (line.InventoryID.HasValue && !string.IsNullOrEmpty(line.UOM) && !string.IsNullOrEmpty(poLine.UOM) && !string.Equals(line.UOM, poLine.UOM, StringComparison.OrdinalIgnoreCase))
      num1 = INUnitAttribute.ConvertFromBase(((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base).Caches[typeof (PX.Objects.PO.POReceiptLine)], line.InventoryID, poLine.UOM, line.BaseReceiptQty.Value, INPrecision.QUANTITY);
    Decimal num2 = num1;
    short? invtMult = line.InvtMult;
    Decimal? nullable = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    return !nullable.HasValue ? new Decimal?() : new Decimal?(num2 * nullable.GetValueOrDefault());
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.ReleaseReturnLine(PX.Objects.PO.POReceiptLine,PX.Objects.PO.POLine,PX.Objects.PO.POReceiptLine2)" />
  /// </summary>
  [PXOverride]
  public virtual void ReleaseReturnLine(
    PX.Objects.PO.POReceiptLine line,
    PX.Objects.PO.POLine poLine,
    POReceiptLine2 origLine,
    Action<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POLine, POReceiptLine2> baseImpl)
  {
    this.UpdatePOAccrualStatus(POAccrualStatus.PK.FindDirty((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, line.POAccrualRefNoteID, line.POAccrualLineNbr, line.POAccrualType), poLine, line, (PX.Objects.PO.POOrder) null, ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current);
    this.UpdatePOReceiptLineAccrualDetail(line);
    baseImpl(line, poLine, origLine);
  }

  [PXOverride]
  public virtual void UpdateReturnLineOnRelease(PXResult<PX.Objects.PO.POReceiptLine> row, PX.Objects.PO.POLine pOLine)
  {
    this.UpdatePOLineOnReturn(PXResult<PX.Objects.PO.POReceiptLine>.op_Implicit(row), pOLine);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.UpdateReturnReleased(PX.Objects.IN.INRegister)" />
  /// </summary>
  [PXOverride]
  public virtual PX.Objects.PO.POReceipt UpdateReturnReleased(
    PX.Objects.IN.INRegister inRegister,
    Func<PX.Objects.IN.INRegister, PX.Objects.PO.POReceipt> baseImpl)
  {
    PX.Objects.PO.POReceipt poReceipt = baseImpl(inRegister);
    this.IncUnreleasedReceiptCntr(poReceipt);
    return poReceipt;
  }

  public virtual PX.Objects.PO.POLine UpdatePOLineOnReturn(PX.Objects.PO.POReceiptLine line, PX.Objects.PO.POLine poLine)
  {
    if (poLine == null || string.IsNullOrEmpty(poLine.OrderType) || string.IsNullOrEmpty(poLine.OrderNbr) || !poLine.LineNbr.HasValue)
      return poLine;
    poLine = PXCache<PX.Objects.PO.POLine>.CreateCopy(poLine);
    PX.Objects.PO.POLine poLine1 = poLine;
    Decimal? completedQty = poLine1.CompletedQty;
    Decimal? completedQtyDelta = this.GetCompletedQtyDelta(line, poLine);
    poLine1.CompletedQty = completedQty.HasValue & completedQtyDelta.HasValue ? new Decimal?(completedQty.GetValueOrDefault() + completedQtyDelta.GetValueOrDefault()) : new Decimal?();
    bool flag = POLineType.IsDropShip(line.LineType) && line.ReceiptType == "RN";
    if (poLine.AllowComplete.GetValueOrDefault() && poLine.Completed.GetValueOrDefault() && !flag)
    {
      poLine.Completed = new bool?(false);
      poLine.Closed = new bool?(false);
    }
    return ((PXSelectBase<PX.Objects.PO.POLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.poline).Update(poLine);
  }

  public virtual POAccrualStatus UpdatePOAccrualStatus(
    POAccrualStatus origRow,
    PX.Objects.PO.POLine poLine,
    PX.Objects.PO.POReceiptLine rctLine,
    PX.Objects.PO.POOrder order,
    PX.Objects.PO.POReceipt rct)
  {
    PXCache cache1 = ((PXSelectBase) this.poAccrualUpdate).Cache;
    POAccrualStatus row1;
    if (origRow == null)
    {
      POAccrualStatus poAccrualStatus = new POAccrualStatus()
      {
        Type = rctLine.POAccrualType,
        RefNoteID = rctLine.POAccrualRefNoteID,
        LineNbr = rctLine.POAccrualLineNbr
      };
      row1 = (POAccrualStatus) cache1.Insert((object) poAccrualStatus);
    }
    else
      row1 = (POAccrualStatus) cache1.CreateCopy((object) origRow);
    this.SetIfNotNull<POAccrualStatus.dropshipExpenseRecording>(cache1, row1, (object) order?.DropshipExpenseRecording);
    this.SetIfNotNull<POAccrualStatus.lineType>(cache1, row1, (object) rctLine.LineType);
    this.SetIfNotNull<POAccrualStatus.orderType>(cache1, row1, (object) rctLine.POType);
    this.SetIfNotNull<POAccrualStatus.orderNbr>(cache1, row1, (object) rctLine.PONbr);
    this.SetIfNotNull<POAccrualStatus.orderLineNbr>(cache1, row1, (object) rctLine.POLineNbr);
    if (rctLine.POAccrualType == "R")
    {
      this.SetIfNotNull<POAccrualStatus.receiptType>(cache1, row1, (object) rctLine.ReceiptType);
      this.SetIfNotNull<POAccrualStatus.receiptNbr>(cache1, row1, (object) rctLine.ReceiptNbr);
    }
    if (row1.MaxFinPeriodID == null || rct.FinPeriodID.CompareTo(row1.MaxFinPeriodID) > 0)
      this.SetIfNotNull<POAccrualStatus.maxFinPeriodID>(cache1, row1, (object) rct.FinPeriodID);
    this.SetIfNotNull<POAccrualStatus.origUOM>(cache1, row1, (object) poLine?.UOM);
    this.SetIfNotNull<POAccrualStatus.origCuryID>(cache1, row1, (object) order?.CuryID);
    this.SetIfNotNull<POAccrualStatus.vendorID>(cache1, row1, (object) rctLine.VendorID);
    this.SetIfNotNull<POAccrualStatus.payToVendorID>(cache1, row1, (object) (int?) order?.PayToVendorID);
    this.SetIfNotNull<POAccrualStatus.inventoryID>(cache1, row1, (object) rctLine.InventoryID);
    this.SetIfNotNull<POAccrualStatus.subItemID>(cache1, row1, (object) rctLine.SubItemID);
    this.SetIfNotNull<POAccrualStatus.siteID>(cache1, row1, (object) rctLine.SiteID);
    this.SetIfNotNull<POAccrualStatus.acctID>(cache1, row1, (object) rctLine.POAccrualAcctID);
    this.SetIfNotNull<POAccrualStatus.subID>(cache1, row1, (object) rctLine.POAccrualSubID);
    ARReleaseProcess.Amount amount = (ARReleaseProcess.Amount) null;
    if (poLine != null && poLine.OrderNbr != null)
      amount = APReleaseProcess.GetExpensePostingAmount((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, poLine);
    this.SetIfNotEmpty<POAccrualStatus.origQty>(cache1, row1, (Decimal?) poLine?.OrderQty);
    this.SetIfNotEmpty<POAccrualStatus.baseOrigQty>(cache1, row1, (Decimal?) poLine?.BaseOrderQty);
    PXCache cache2 = cache1;
    POAccrualStatus row2 = row1;
    Decimal? curyExtCost = (Decimal?) poLine?.CuryExtCost;
    Decimal? nullable1 = (Decimal?) poLine?.CuryRetainageAmt;
    Decimal? nullable2 = curyExtCost.HasValue & nullable1.HasValue ? new Decimal?(curyExtCost.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    this.SetIfNotEmpty<POAccrualStatus.curyOrigAmt>(cache2, row2, nullable2);
    PXCache cache3 = cache1;
    POAccrualStatus row3 = row1;
    nullable1 = (Decimal?) poLine?.ExtCost;
    Decimal? nullable3 = (Decimal?) poLine?.RetainageAmt;
    Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    this.SetIfNotEmpty<POAccrualStatus.origAmt>(cache3, row3, nullable4);
    PXCache cache4 = cache1;
    POAccrualStatus row4 = row1;
    Decimal? nullable5;
    if (amount == null)
    {
      nullable3 = new Decimal?();
      nullable5 = nullable3;
    }
    else
      nullable5 = amount.Cury;
    this.SetIfNotEmpty<POAccrualStatus.curyOrigCost>(cache4, row4, nullable5);
    PXCache cache5 = cache1;
    POAccrualStatus row5 = row1;
    Decimal? nullable6;
    if (amount == null)
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = amount.Base;
    this.SetIfNotEmpty<POAccrualStatus.origCost>(cache5, row5, nullable6);
    PXCache cache6 = cache1;
    POAccrualStatus row6 = row1;
    Decimal? nullable7;
    if (poLine == null)
    {
      nullable3 = new Decimal?();
      nullable7 = nullable3;
    }
    else
      nullable7 = poLine.CuryDiscAmt;
    this.SetIfNotEmpty<POAccrualStatus.curyOrigDiscAmt>(cache6, row6, nullable7);
    PXCache cache7 = cache1;
    POAccrualStatus row7 = row1;
    Decimal? nullable8;
    if (poLine == null)
    {
      nullable3 = new Decimal?();
      nullable8 = nullable3;
    }
    else
      nullable8 = poLine.DiscAmt;
    this.SetIfNotEmpty<POAccrualStatus.origDiscAmt>(cache7, row7, nullable8);
    int num;
    if (origRow != null)
    {
      nullable3 = origRow.ReceivedQty;
      num = !nullable3.HasValue ? 1 : (!EnumerableExtensions.IsIn<string>(origRow.ReceivedUOM, (string) null, rctLine.UOM) ? 1 : 0);
    }
    else
      num = 0;
    bool flag = num != 0;
    row1.ReceivedUOM = flag ? (string) null : rctLine.UOM;
    POAccrualStatus poAccrualStatus1 = row1;
    nullable3 = poAccrualStatus1.ReceivedQty;
    short? invtMult;
    Decimal? nullable9;
    Decimal? nullable10;
    if (!flag)
    {
      invtMult = rctLine.InvtMult;
      Decimal? nullable11 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      nullable9 = rctLine.ReceiptQty;
      nullable10 = nullable11.HasValue & nullable9.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * nullable9.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable10 = new Decimal?();
    nullable1 = nullable10;
    Decimal? nullable12;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable9 = new Decimal?();
      nullable12 = nullable9;
    }
    else
      nullable12 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
    poAccrualStatus1.ReceivedQty = nullable12;
    POAccrualStatus poAccrualStatus2 = row1;
    nullable1 = poAccrualStatus2.BaseReceivedQty;
    invtMult = rctLine.InvtMult;
    nullable9 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13 = rctLine.BaseReceiptQty;
    nullable3 = nullable9.HasValue & nullable13.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * nullable13.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable14;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable13 = new Decimal?();
      nullable14 = nullable13;
    }
    else
      nullable14 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    poAccrualStatus2.BaseReceivedQty = nullable14;
    POAccrualStatus poAccrualStatus3 = row1;
    nullable3 = poAccrualStatus3.ReceivedCost;
    invtMult = rctLine.InvtMult;
    nullable13 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    nullable9 = rctLine.TranCostFinal ?? rctLine.TranCost;
    nullable1 = nullable13.HasValue & nullable9.HasValue ? new Decimal?(nullable13.GetValueOrDefault() * nullable9.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable15;
    if (!(nullable3.HasValue & nullable1.HasValue))
    {
      nullable9 = new Decimal?();
      nullable15 = nullable9;
    }
    else
      nullable15 = new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
    poAccrualStatus3.ReceivedCost = nullable15;
    return ((PXSelectBase<POAccrualStatus>) this.poAccrualUpdate).Update(row1);
  }

  public virtual void IncUnreleasedReceiptCntr(PX.Objects.PO.POReceipt poReceipt)
  {
    List<POAccrualDetail> list = ((PXSelectBase) this.poAccrualDetailUpdate).Cache.Inserted.OfType<POAccrualDetail>().Where<POAccrualDetail>((Func<POAccrualDetail, bool>) (accrualDetail =>
    {
      if (accrualDetail.POReceiptType != poReceipt.ReceiptType || accrualDetail.POReceiptNbr != poReceipt.ReceiptNbr)
        return false;
      if (poReceipt.OrigReceiptNbr == null || poReceipt.POType == "DP")
        return true;
      PX.Objects.PO.POReceiptLine poReceiptLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Locate(new PX.Objects.PO.POReceiptLine()
      {
        ReceiptNbr = accrualDetail.POReceiptNbr,
        ReceiptType = accrualDetail.POReceiptType,
        LineNbr = accrualDetail.LineNbr
      }) ?? PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) ((PXGraphExtension<POReceiptEntry>) this).Base, accrualDetail.POReceiptType, accrualDetail.POReceiptNbr, accrualDetail.LineNbr);
      if (!poReceiptLine.IsAdjustedIN.GetValueOrDefault())
        return false;
      Decimal? baseOrigQty = poReceiptLine.BaseOrigQty;
      Decimal num1 = 0M;
      if (!(baseOrigQty.GetValueOrDefault() == num1 & baseOrigQty.HasValue))
        return true;
      Decimal? baseReceiptQty = poReceiptLine.BaseReceiptQty;
      Decimal num2 = 0M;
      return !(baseReceiptQty.GetValueOrDefault() == num2 & baseReceiptQty.HasValue);
    })).ToList<POAccrualDetail>();
    if (!list.Any<POAccrualDetail>())
      return;
    foreach (POAccrualStatus poAccrualStatus1 in new HashSet<POAccrualStatus>(list.Select<POAccrualDetail, POAccrualStatus>((Func<POAccrualDetail, POAccrualStatus>) (x => new POAccrualStatus()
    {
      RefNoteID = x.POAccrualRefNoteID,
      LineNbr = x.POAccrualLineNbr,
      Type = x.POAccrualType
    })), (IEqualityComparer<POAccrualStatus>) PXCacheEx.GetComparer(((PXSelectBase) this.poAccrualUpdate).Cache)))
    {
      POAccrualStatus poAccrualStatus2 = ((PXSelectBase<POAccrualStatus>) this.poAccrualUpdate).Locate(poAccrualStatus1);
      if (poAccrualStatus2 != null)
      {
        POAccrualStatus poAccrualStatus3 = poAccrualStatus2;
        int? unreleasedReceiptCntr = poAccrualStatus3.UnreleasedReceiptCntr;
        poAccrualStatus3.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() + 1) : new int?();
        ((PXSelectBase<POAccrualStatus>) this.poAccrualUpdate).Update(poAccrualStatus2);
      }
    }
  }

  public virtual bool StorePOAccrualDetail(PX.Objects.PO.POReceiptLine receiptLine)
  {
    if (!(receiptLine.ReceiptType != "RX") || !EnumerableExtensions.IsNotIn<string>(receiptLine.LineType, "SV", "FT"))
      return false;
    return receiptLine.POType != "PD" || receiptLine.DropshipExpenseRecording != "B";
  }

  public virtual POAccrualDetail PreparePOReceiptLineAccrualDetail(PX.Objects.PO.POReceiptLine receiptLine)
  {
    if (!this.StorePOAccrualDetail(receiptLine))
      return (POAccrualDetail) null;
    POAccrualDetail poAccrualDetail = ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Locate(new POAccrualDetail()
    {
      DocumentNoteID = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.NoteID,
      LineNbr = receiptLine.LineNbr
    });
    if (poAccrualDetail == null)
      poAccrualDetail = ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Insert(new POAccrualDetail()
      {
        DocumentNoteID = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.NoteID,
        POReceiptType = receiptLine.ReceiptType,
        POReceiptNbr = receiptLine.ReceiptNbr,
        LineNbr = receiptLine.LineNbr,
        POAccrualRefNoteID = receiptLine.POAccrualRefNoteID,
        POAccrualLineNbr = receiptLine.POAccrualLineNbr,
        POAccrualType = receiptLine.POAccrualType,
        VendorID = receiptLine.VendorID,
        IsDropShip = new bool?(POLineType.IsDropShip(receiptLine.LineType)),
        BranchID = receiptLine.BranchID,
        DocDate = receiptLine.ReceiptDate,
        FinPeriodID = ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.FinPeriodID,
        TranDesc = receiptLine.TranDesc,
        UOM = receiptLine.UOM
      });
    return poAccrualDetail;
  }

  public virtual POAccrualDetail UpdatePOReceiptLineAccrualDetail(PX.Objects.PO.POReceiptLine receiptLine)
  {
    POAccrualDetail poAccrualDetail1 = this.PreparePOReceiptLineAccrualDetail(receiptLine);
    if (poAccrualDetail1 == null)
      return (POAccrualDetail) null;
    POAccrualDetail poAccrualDetail2 = poAccrualDetail1;
    Decimal? accruedQty = poAccrualDetail2.AccruedQty;
    short? invtMult = receiptLine.InvtMult;
    Decimal? nullable1 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    Decimal? receiptQty = receiptLine.ReceiptQty;
    Decimal? nullable2 = nullable1.HasValue & receiptQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * receiptQty.GetValueOrDefault()) : new Decimal?();
    poAccrualDetail2.AccruedQty = accruedQty.HasValue & nullable2.HasValue ? new Decimal?(accruedQty.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    POAccrualDetail poAccrualDetail3 = poAccrualDetail1;
    nullable2 = poAccrualDetail3.BaseAccruedQty;
    invtMult = receiptLine.InvtMult;
    Decimal? nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    Decimal? baseReceiptQty = receiptLine.BaseReceiptQty;
    Decimal? nullable4 = nullable3.HasValue & baseReceiptQty.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * baseReceiptQty.GetValueOrDefault()) : new Decimal?();
    poAccrualDetail3.BaseAccruedQty = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    POAccrualDetail poAccrualDetail4 = poAccrualDetail1;
    nullable4 = poAccrualDetail4.AccruedCost;
    invtMult = receiptLine.InvtMult;
    Decimal? nullable5 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable6 = receiptLine.TranCostFinal ?? receiptLine.TranCost;
    nullable2 = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable7;
    if (!(nullable4.HasValue & nullable2.HasValue))
    {
      nullable6 = new Decimal?();
      nullable7 = nullable6;
    }
    else
      nullable7 = new Decimal?(nullable4.GetValueOrDefault() + nullable2.GetValueOrDefault());
    poAccrualDetail4.AccruedCost = nullable7;
    return ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Update(poAccrualDetail1);
  }

  public virtual void SetReceiptCostFinal(PX.Objects.PO.POReceiptLine rctLine, POAccrualStatus poAccrual)
  {
    if (rctLine.IsKit.GetValueOrDefault() && !rctLine.IsStockItem.GetValueOrDefault())
    {
      rctLine.TranCostFinal = new Decimal?(0M);
    }
    else
    {
      if (poAccrual != null)
      {
        Decimal? nullable1 = poAccrual.BaseReceivedQty;
        Decimal? nullable2 = poAccrual.BaseBilledQty;
        if (!(nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
        {
          PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraphExtension<POReceiptEntry>) this).Base.MultiCurrencyExt.GetDefaultCurrencyInfo();
          int num1;
          if (string.Equals(rctLine.UOM, poAccrual.BilledUOM, StringComparison.OrdinalIgnoreCase))
          {
            nullable2 = poAccrual.ReceivedQty;
            if (nullable2.HasValue)
            {
              num1 = EnumerableExtensions.IsIn<string>(poAccrual.ReceivedUOM, (string) null, rctLine.UOM) ? 1 : 0;
              goto label_9;
            }
          }
          num1 = 0;
label_9:
          bool flag = num1 != 0;
          Decimal? nullable3 = flag ? rctLine.ReceiptQty : rctLine.BaseReceiptQty;
          Decimal? nullable4;
          if (!flag)
          {
            nullable2 = poAccrual.BaseBilledQty;
            nullable1 = poAccrual.BaseReceivedQty;
            nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            nullable1 = poAccrual.BilledQty;
            nullable2 = poAccrual.ReceivedQty;
            nullable4 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          }
          Decimal? nullable5 = nullable4;
          Decimal? billedCost = poAccrual.BilledCost;
          Decimal? nullable6 = poAccrual.PPVAmt;
          nullable2 = billedCost.HasValue & nullable6.HasValue ? new Decimal?(billedCost.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
          nullable1 = poAccrual.ReceivedCost;
          Decimal? nullable7;
          if (!(nullable2.HasValue & nullable1.HasValue))
          {
            nullable6 = new Decimal?();
            nullable7 = nullable6;
          }
          else
            nullable7 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
          Decimal? nullable8 = nullable7;
          nullable1 = nullable3;
          nullable2 = nullable5;
          Decimal? nullable9;
          Decimal? nullable10;
          Decimal? nullable11;
          Decimal? nullable12;
          if (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          {
            nullable9 = nullable3;
            nullable10 = rctLine.BaseUnbilledQty;
            nullable6 = nullable3;
            Decimal? nullable13 = nullable8;
            nullable2 = nullable6.HasValue & nullable13.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * nullable13.GetValueOrDefault()) : new Decimal?();
            nullable1 = nullable5;
            Decimal? nullable14;
            if (!(nullable2.HasValue & nullable1.HasValue))
            {
              nullable13 = new Decimal?();
              nullable14 = nullable13;
            }
            else
              nullable14 = new Decimal?(nullable2.GetValueOrDefault() / nullable1.GetValueOrDefault());
            nullable11 = nullable14;
            nullable12 = nullable14;
            PX.Objects.PO.POReceiptLine poReceiptLine1 = rctLine;
            PX.Objects.PO.POReceiptLine poReceiptLine2 = rctLine;
            nullable1 = new Decimal?(0M);
            Decimal? nullable15 = nullable1;
            poReceiptLine2.UnbilledQty = nullable15;
            Decimal? nullable16 = nullable1;
            poReceiptLine1.BaseUnbilledQty = nullable16;
          }
          else
          {
            nullable9 = nullable5;
            nullable12 = nullable11 = nullable8;
            if (flag)
            {
              Decimal? baseUnbilledQty = rctLine.BaseUnbilledQty;
              PX.Objects.PO.POReceiptLine poReceiptLine = rctLine;
              nullable1 = poReceiptLine.UnbilledQty;
              nullable2 = nullable5;
              poReceiptLine.UnbilledQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
              PXDBQuantityAttribute.CalcBaseQty<PX.Objects.PO.POReceiptLine.unbilledQty>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache, (object) rctLine);
              nullable2 = baseUnbilledQty;
              nullable1 = rctLine.BaseUnbilledQty;
              nullable10 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
            }
            else
            {
              PX.Objects.PO.POReceiptLine poReceiptLine = rctLine;
              nullable1 = poReceiptLine.BaseUnbilledQty;
              nullable2 = nullable5;
              poReceiptLine.BaseUnbilledQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
              PXDBQuantityAttribute.CalcTranQty<PX.Objects.PO.POReceiptLine.unbilledQty>(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache, (object) rctLine);
              nullable10 = nullable5;
            }
            nullable2 = rctLine.UnbilledQty;
            Decimal num2 = 0M;
            if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
            {
              nullable2 = rctLine.ReceiptQty;
              Decimal num3 = 0M;
              if (nullable2.GetValueOrDefault() > num3 & nullable2.HasValue)
              {
                nullable2 = nullable12;
                Decimal? unbilledQty = rctLine.UnbilledQty;
                Decimal? tranCost = rctLine.TranCost;
                Decimal? nullable17 = rctLine.ReceiptQty;
                nullable6 = tranCost.HasValue & nullable17.HasValue ? new Decimal?(tranCost.GetValueOrDefault() / nullable17.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable18;
                if (!(unbilledQty.HasValue & nullable6.HasValue))
                {
                  nullable17 = new Decimal?();
                  nullable18 = nullable17;
                }
                else
                  nullable18 = new Decimal?(unbilledQty.GetValueOrDefault() * nullable6.GetValueOrDefault());
                nullable1 = nullable18;
                Decimal? nullable19;
                if (!(nullable2.HasValue & nullable1.HasValue))
                {
                  nullable6 = new Decimal?();
                  nullable19 = nullable6;
                }
                else
                  nullable19 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
                nullable12 = nullable19;
              }
            }
          }
          rctLine.TranCostFinal = new Decimal?(defaultCurrencyInfo.RoundBase(nullable12.GetValueOrDefault()));
          PX.Objects.PO.POReceiptLine rctLine1 = rctLine;
          POAccrualStatus poAccrual1 = poAccrual;
          string uom = flag ? rctLine.UOM : (string) null;
          Decimal? accruedQty;
          if (!flag)
          {
            nullable1 = new Decimal?();
            accruedQty = nullable1;
          }
          else
            accruedQty = nullable9;
          Decimal? baseAccruedQty = nullable10;
          Decimal? accruedCost = nullable11;
          this.InsertPOAccrualSplits(rctLine1, poAccrual1, uom, accruedQty, baseAccruedQty, accruedCost);
          goto label_36;
        }
      }
      rctLine.TranCostFinal = rctLine.TranCost;
    }
label_36:
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<POReceiptEntry>) this).Base.transactions).Cache, (object) rctLine, true);
  }

  public virtual void InsertPOAccrualSplits(
    PX.Objects.PO.POReceiptLine rctLine,
    POAccrualStatus poAccrual,
    string accruedUom,
    Decimal? accruedQty,
    Decimal? baseAccruedQty,
    Decimal? accruedCost)
  {
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraphExtension<POReceiptEntry>) this).Base.MultiCurrencyExt.GetDefaultCurrencyInfo();
    bool hasValue = accruedQty.HasValue;
    Decimal? nullable1 = hasValue ? accruedQty : baseAccruedQty;
    Decimal? nullable2 = accruedCost;
    foreach (APTranReceiptUpdate data in ((PXSelectBase) this.apTranUpdate).View.SelectMultiBound(new object[1]
    {
      (object) poAccrual
    }, Array.Empty<object>()))
    {
      Decimal? nullable3 = nullable1;
      Decimal num1 = 0M;
      if (nullable3.GetValueOrDefault() <= num1 & nullable3.HasValue)
        break;
      Decimal? nullable4 = hasValue ? data.UnreceivedQty : data.BaseUnreceivedQty;
      nullable3 = nullable4;
      Decimal? nullable5 = nullable1;
      Decimal? nullable6;
      Decimal? nullable7;
      Decimal? nullable8;
      if (nullable3.GetValueOrDefault() <= nullable5.GetValueOrDefault() & nullable3.HasValue & nullable5.HasValue)
      {
        ref Decimal? local = ref nullable6;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
        Decimal? nullable9 = nullable2;
        Decimal? nullable10 = nullable4;
        nullable5 = nullable9.HasValue & nullable10.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * nullable10.GetValueOrDefault()) : new Decimal?();
        nullable3 = nullable1;
        Decimal valueOrDefault = (nullable5.HasValue & nullable3.HasValue ? new Decimal?(nullable5.GetValueOrDefault() / nullable3.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        Decimal num2 = currencyInfo.RoundBase(valueOrDefault);
        local = new Decimal?(num2);
        nullable5 = nullable2;
        nullable3 = nullable6;
        nullable2 = nullable5.HasValue & nullable3.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
        nullable7 = nullable4;
        nullable8 = data.BaseUnreceivedQty;
        nullable3 = nullable1;
        nullable5 = nullable7;
        nullable1 = nullable3.HasValue & nullable5.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        APTranReceiptUpdate tranReceiptUpdate = data;
        data.UnreceivedQty = nullable5 = new Decimal?(0M);
        Decimal? nullable11 = nullable5;
        tranReceiptUpdate.BaseUnreceivedQty = nullable11;
      }
      else
      {
        nullable6 = nullable2;
        nullable2 = new Decimal?(0M);
        nullable7 = nullable1;
        nullable1 = new Decimal?(0M);
        if (hasValue)
        {
          Decimal? baseUnreceivedQty = data.BaseUnreceivedQty;
          APTranReceiptUpdate tranReceiptUpdate = data;
          nullable5 = tranReceiptUpdate.UnreceivedQty;
          nullable3 = nullable7;
          tranReceiptUpdate.UnreceivedQty = nullable5.HasValue & nullable3.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          PXDBQuantityAttribute.CalcBaseQty<APTranReceiptUpdate.unreceivedQty>(((PXSelectBase) this.apTranUpdate).Cache, (object) data);
          nullable3 = baseUnreceivedQty;
          nullable5 = data.BaseUnreceivedQty;
          nullable8 = nullable3.HasValue & nullable5.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          APTranReceiptUpdate tranReceiptUpdate = data;
          nullable5 = tranReceiptUpdate.BaseUnreceivedQty;
          nullable3 = nullable7;
          tranReceiptUpdate.BaseUnreceivedQty = nullable5.HasValue & nullable3.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          PXDBQuantityAttribute.CalcTranQty<APTranReceiptUpdate.unreceivedQty>(((PXSelectBase) this.apTranUpdate).Cache, (object) data);
          nullable8 = nullable7;
        }
      }
      ((PXSelectBase<APTranReceiptUpdate>) this.apTranUpdate).Update(data);
      POAccrualSplit poAccrualSplit1 = ((PXSelectBase<POAccrualSplit>) this.poAccrualSplitUpdate).Insert(new POAccrualSplit()
      {
        RefNoteID = poAccrual.RefNoteID,
        LineNbr = poAccrual.LineNbr,
        Type = poAccrual.Type,
        APDocType = data.TranType,
        APRefNbr = data.RefNbr,
        APLineNbr = data.LineNbr,
        POReceiptType = rctLine.ReceiptType,
        POReceiptNbr = rctLine.ReceiptNbr,
        POReceiptLineNbr = rctLine.LineNbr
      });
      poAccrualSplit1.UOM = accruedUom;
      POAccrualSplit poAccrualSplit2 = poAccrualSplit1;
      Decimal? nullable12;
      if (!hasValue)
      {
        nullable3 = new Decimal?();
        nullable12 = nullable3;
      }
      else
        nullable12 = nullable7;
      poAccrualSplit2.AccruedQty = nullable12;
      poAccrualSplit1.BaseAccruedQty = nullable8;
      poAccrualSplit1.AccruedCost = nullable6;
      poAccrualSplit1.PPVAmt = new Decimal?(0M);
      poAccrualSplit1.FinPeriodID = ((IEnumerable<string>) new string[2]
      {
        data.FinPeriodID,
        ((PXSelectBase<PX.Objects.PO.POReceipt>) ((PXGraphExtension<POReceiptEntry>) this).Base.Document).Current.FinPeriodID
      }).Max<string>();
      ((PXSelectBase<POAccrualSplit>) this.poAccrualSplitUpdate).Update(poAccrualSplit1);
    }
  }
}
