// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INReleaseProcessExt.UpdatePOAccrual
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.Accumulators.Documents;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INReleaseProcessExt;

public class UpdatePOAccrual : PXGraphExtension<INReleaseProcess>
{
  public PXSelect<POAccrualStatus> poAccrualStatuses;
  public PXSelect<POAccrualSplit> poAccrualSplits;
  public PXSelect<POAccrualDetailPostedUpdate> poAccrualDetailPostedUpdate;
  public PXSelect<POAccrualDetailPPVAdjPostedUpdate> poAccrualDetailPPVAdjPostedUpdate;
  public PXSelect<POAccrualDetailTaxAdjPostedUpdate> poAccrualDetailTaxAdjPostedUpdate;
  protected PX.Objects.IN.INRegister ProcesssingDocument;
  protected DuplicatesSearchEngine<PX.Objects.PO.POReceiptLine> ProcessedStatuses;

  protected virtual void InitializeProcessedCaches(PX.Objects.IN.INRegister register)
  {
    this.ProcesssingDocument = register;
    this.ProcessedStatuses = new DuplicatesSearchEngine<PX.Objects.PO.POReceiptLine>((PXCache) GraphHelper.Caches<PX.Objects.PO.POReceiptLine>((PXGraph) this.Base), (IEnumerable<Type>) new Type[3]
    {
      typeof (PX.Objects.PO.POReceiptLine.pOAccrualRefNoteID),
      typeof (PX.Objects.PO.POReceiptLine.pOAccrualLineNbr),
      typeof (PX.Objects.PO.POReceiptLine.pOAccrualType)
    }, (ICollection<PX.Objects.PO.POReceiptLine>) Array<PX.Objects.PO.POReceiptLine>.Empty);
  }

  protected virtual void MarkProcessed(INTran tran, PX.Objects.PO.POReceiptLine receiptLine)
  {
    this.ProcessedStatuses.AddItem(receiptLine);
  }

  protected virtual bool IsProcessed(PX.Objects.PO.POReceiptLine receiptLine)
  {
    return this.ProcessedStatuses.Find(receiptLine) != null;
  }

  protected virtual PX.Objects.PO.POReceiptLine FindReceiptLine(INTran tran)
  {
    return PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) this.Base, tran.POReceiptType, tran.POReceiptNbr, tran.POReceiptLineNbr);
  }

  protected virtual POAccrualStatus FindAccrualStatus(PX.Objects.PO.POReceiptLine receiptLine)
  {
    return PXResultset<POAccrualStatus>.op_Implicit(PXSelectBase<POAccrualStatus, PXViewOf<POAccrualStatus>.BasedOn<SelectFromBase<POAccrualStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.refNoteID, Equal<P.AsGuid>>>>, And<BqlOperand<POAccrualStatus.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<POAccrualStatus.type, IBqlString>.IsEqual<P.AsString.ASCII>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[3]
    {
      (object) receiptLine.POAccrualRefNoteID,
      (object) receiptLine.POAccrualLineNbr,
      (object) receiptLine.POAccrualType
    }));
  }

  protected virtual bool AffectsPOAccrual(INTran tran)
  {
    if (string.IsNullOrEmpty(tran.POReceiptNbr) || !tran.POReceiptLineNbr.HasValue || tran.POReceiptType == "RX")
      return false;
    PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.Base.inregister).Current;
    if (!current.IsPPVTran.GetValueOrDefault())
    {
      bool? nullable = current.IsTaxAdjustmentTran;
      if (!nullable.GetValueOrDefault() && !(current.DocType == "R") && (!(current.DocType == "I") || !(current.SOShipmentType == "H")) && (!(current.DocType == "I") || !(tran.POReceiptType == "RN")))
      {
        if (!(current.DocType == "I"))
          return false;
        nullable = current.IsCorrection;
        return nullable.GetValueOrDefault();
      }
    }
    return true;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.OnTranReleased(PX.Objects.IN.INTran)" />
  /// </summary>
  [PXOverride]
  public virtual void OnTranReleased(INTran tran, Action<INTran> baseImpl)
  {
    baseImpl(tran);
    if (!this.AffectsPOAccrual(tran))
      return;
    PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) this.Base.inregister).Current;
    PX.Objects.PO.POReceiptLine receiptLine;
    if (this.ProcesssingDocument?.DocType != current.DocType || this.ProcesssingDocument?.RefNbr != current.RefNbr)
    {
      this.InitializeProcessedCaches(current);
      receiptLine = this.FindReceiptLine(tran);
    }
    else
    {
      receiptLine = this.FindReceiptLine(tran);
      if (this.IsProcessed(receiptLine))
        return;
    }
    POAccrualStatus accrualStatus = this.FindAccrualStatus(receiptLine);
    bool? nullable = current.IsPPVTran;
    if (nullable.GetValueOrDefault())
    {
      this.UpdatePPVAdjPosted(tran, accrualStatus);
      POAccrualStatus poAccrualStatus = accrualStatus;
      int? unreleasedPpvAdjCntr = poAccrualStatus.UnreleasedPPVAdjCntr;
      poAccrualStatus.UnreleasedPPVAdjCntr = unreleasedPpvAdjCntr.HasValue ? new int?(unreleasedPpvAdjCntr.GetValueOrDefault() - 1) : new int?();
    }
    else
    {
      nullable = current.IsTaxAdjustmentTran;
      if (nullable.GetValueOrDefault())
      {
        this.UpdateTaxAdjPosted(tran, accrualStatus);
        POAccrualStatus poAccrualStatus = accrualStatus;
        int? unreleasedTaxAdjCntr = poAccrualStatus.UnreleasedTaxAdjCntr;
        poAccrualStatus.UnreleasedTaxAdjCntr = unreleasedTaxAdjCntr.HasValue ? new int?(unreleasedTaxAdjCntr.GetValueOrDefault() - 1) : new int?();
      }
      else
      {
        POAccrualDetailPostedUpdate detailPostedUpdate = this.UpdateAccrualDetailPosted(tran);
        POAccrualStatus poAccrualStatus = accrualStatus;
        int? unreleasedReceiptCntr = poAccrualStatus.UnreleasedReceiptCntr;
        poAccrualStatus.UnreleasedReceiptCntr = unreleasedReceiptCntr.HasValue ? new int?(unreleasedReceiptCntr.GetValueOrDefault() - 1) : new int?();
        if (current.DocType == "I" && current.SOShipmentType == "H")
        {
          detailPostedUpdate.FinPeriodID = current.FinPeriodID;
          ((PXSelectBase<POAccrualDetailPostedUpdate>) this.poAccrualDetailPostedUpdate).Update(detailPostedUpdate);
          if (current.FinPeriodID.CompareTo(accrualStatus.MaxFinPeriodID) > 0)
            accrualStatus.MaxFinPeriodID = current.FinPeriodID;
          FbqlSelect<SelectFromBase<POAccrualSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualSplit.pOReceiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<POAccrualSplit.pOReceiptNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<POAccrualSplit.pOReceiptLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<POAccrualSplit.finPeriodID, IBqlString>.IsLess<P.AsString.ASCII>>>, POAccrualSplit>.View view = new FbqlSelect<SelectFromBase<POAccrualSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualSplit.pOReceiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<POAccrualSplit.pOReceiptNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<POAccrualSplit.pOReceiptLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<POAccrualSplit.finPeriodID, IBqlString>.IsLess<P.AsString.ASCII>>>, POAccrualSplit>.View((PXGraph) this.Base);
          object[] objArray = new object[4]
          {
            (object) receiptLine.ReceiptType,
            (object) receiptLine.ReceiptNbr,
            (object) receiptLine.LineNbr,
            (object) current.FinPeriodID
          };
          foreach (POAccrualSplit poAccrualSplit in ((PXSelectBase<POAccrualSplit>) view).SelectMain(objArray))
          {
            poAccrualSplit.FinPeriodID = current.FinPeriodID;
            ((PXSelectBase<POAccrualSplit>) this.poAccrualSplits).Update(poAccrualSplit);
          }
        }
      }
    }
    ((PXSelectBase<POAccrualStatus>) this.poAccrualStatuses).Update(accrualStatus);
    this.MarkProcessed(tran, receiptLine);
  }

  protected virtual POAccrualDetailPostedUpdate UpdateAccrualDetailPosted(INTran tran)
  {
    POAccrualDetailPostedUpdate detailPostedUpdate = ((PXSelectBase<POAccrualDetailPostedUpdate>) this.poAccrualDetailPostedUpdate).Insert(new POAccrualDetailPostedUpdate()
    {
      POReceiptType = tran.POReceiptType,
      POReceiptNbr = tran.POReceiptNbr,
      LineNbr = tran.POReceiptLineNbr
    });
    detailPostedUpdate.Posted = new bool?(true);
    return ((PXSelectBase<POAccrualDetailPostedUpdate>) this.poAccrualDetailPostedUpdate).Update(detailPostedUpdate);
  }

  protected virtual POAccrualDetailPPVAdjPostedUpdate UpdatePPVAdjPosted(
    INTran tran,
    POAccrualStatus status)
  {
    POAccrualDetailPPVAdjPostedUpdate ppvAdjPostedUpdate = ((PXSelectBase<POAccrualDetailPPVAdjPostedUpdate>) this.poAccrualDetailPPVAdjPostedUpdate).Insert(new POAccrualDetailPPVAdjPostedUpdate()
    {
      POAccrualRefNoteID = status.RefNoteID,
      POAccrualLineNbr = status.LineNbr,
      POAccrualType = status.Type,
      PPVAdjRefNbr = tran.RefNbr
    });
    ppvAdjPostedUpdate.PPVAdjPosted = new bool?(true);
    return ((PXSelectBase<POAccrualDetailPPVAdjPostedUpdate>) this.poAccrualDetailPPVAdjPostedUpdate).Update(ppvAdjPostedUpdate);
  }

  protected virtual POAccrualDetailTaxAdjPostedUpdate UpdateTaxAdjPosted(
    INTran tran,
    POAccrualStatus status)
  {
    POAccrualDetailTaxAdjPostedUpdate taxAdjPostedUpdate = ((PXSelectBase<POAccrualDetailTaxAdjPostedUpdate>) this.poAccrualDetailTaxAdjPostedUpdate).Insert(new POAccrualDetailTaxAdjPostedUpdate()
    {
      POAccrualRefNoteID = status.RefNoteID,
      POAccrualLineNbr = status.LineNbr,
      POAccrualType = status.Type,
      TaxAdjRefNbr = tran.RefNbr
    });
    taxAdjPostedUpdate.TaxAdjPosted = new bool?(true);
    return ((PXSelectBase<POAccrualDetailTaxAdjPostedUpdate>) this.poAccrualDetailTaxAdjPostedUpdate).Update(taxAdjPostedUpdate);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryRelease.INReleaseProcess.UpdatePOReceiptLineCost(PX.Objects.IN.INTran,PX.Objects.IN.INTranCost,PX.Objects.IN.InventoryItem)" />
  /// </summary>
  [PXOverride]
  public virtual POReceiptLineUpdate UpdatePOReceiptLineCost(
    INTran tran,
    INTranCost tranCost,
    PX.Objects.IN.InventoryItem item,
    Func<INTran, INTranCost, PX.Objects.IN.InventoryItem, POReceiptLineUpdate> baseImpl)
  {
    POReceiptLineUpdate receiptLineUpdate = baseImpl(tran, tranCost, item);
    if (receiptLineUpdate != null)
      this.UpdateAccruedCost(tran, receiptLineUpdate);
    return receiptLineUpdate;
  }

  protected virtual void UpdateAccruedCost(INTran tran, POReceiptLineUpdate receiptLineUpdate)
  {
    POAccrualDetailPostedUpdate detailPostedUpdate1 = ((PXSelectBase<POAccrualDetailPostedUpdate>) this.poAccrualDetailPostedUpdate).Insert(new POAccrualDetailPostedUpdate()
    {
      POReceiptType = tran.POReceiptType,
      POReceiptNbr = tran.POReceiptNbr,
      LineNbr = tran.POReceiptLineNbr
    });
    detailPostedUpdate1.PreviousCost = detailPostedUpdate1.AccruedCost;
    POAccrualDetailPostedUpdate detailPostedUpdate2 = detailPostedUpdate1;
    Decimal? tranCostFinal = receiptLineUpdate.TranCostFinal;
    Decimal? nullable1 = tranCostFinal.HasValue ? new Decimal?(-tranCostFinal.GetValueOrDefault()) : new Decimal?();
    detailPostedUpdate2.AccruedCost = nullable1;
    ((PXSelectBase<POAccrualDetailPostedUpdate>) this.poAccrualDetailPostedUpdate).Update(detailPostedUpdate1);
    POAccrualStatus accrualStatus = this.FindAccrualStatus(this.FindReceiptLine(tran));
    POAccrualStatus poAccrualStatus = accrualStatus;
    tranCostFinal = receiptLineUpdate.TranCostFinal;
    Decimal? nullable2 = tranCostFinal.HasValue ? new Decimal?(-tranCostFinal.GetValueOrDefault()) : new Decimal?();
    poAccrualStatus.ReceivedCost = nullable2;
    ((PXSelectBase<POAccrualStatus>) this.poAccrualStatuses).Update(accrualStatus);
  }
}
