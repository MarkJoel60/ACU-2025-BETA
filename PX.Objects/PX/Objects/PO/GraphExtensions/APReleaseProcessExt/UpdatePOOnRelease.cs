// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APReleaseProcessExt.UpdatePOOnRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO.LandedCosts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APReleaseProcessExt;

public class UpdatePOOnRelease : 
  BaseUpdatePOAccrual<APReleaseProcess.MultiCurrency, APReleaseProcess, PX.Objects.AP.APRegister>
{
  public PXSelect<PX.Objects.PO.POReceipt, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Required<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.PO.POReceipt.receiptNbr, Equal<Required<PX.Objects.PO.POReceipt.receiptNbr>>>>> poReceiptUPD;
  public PXSelectJoin<PX.Objects.PO.POReceiptLine, LeftJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceiptLine.FK.Receipt>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.lineNbr>>>>>> poReceiptLineUPD;
  public PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>> poOrderUPD;
  public PXSelectJoin<PX.Objects.PO.POLine, LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>> poOrderLineUPD;
  public PXSelect<POOrderPrepayment, Where<POOrderPrepayment.aPDocType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<POOrderPrepayment.aPRefNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>> poOrderPrepUpd;
  public PXSelect<POItemCostManager.POVendorInventoryPriceUpdate> poVendorInventoryPriceUpdate;
  public PXSelect<POTax> poTaxUpdate;
  public PXSelect<POTaxTran> poTaxTranUpdate;
  public PXSelect<POAccrualStatus> poAccrualUpdate;
  public PXSelect<POAccrualDetail, Where<POAccrualDetail.documentNoteID, Equal<Required<POAccrualDetail.documentNoteID>>, And<POAccrualDetail.lineNbr, Equal<Required<POAccrualDetail.lineNbr>>>>> poAccrualDetailUpdate;
  public PXSelect<POAccrualSplit> poAccrualSplitUpdate;
  public PXSelect<POLandedCostDetail> landedCostDetails;
  public PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>> poAdjustments;

  public static bool IsActive() => true;

  protected List<PX.Objects.AP.APTran> PPVLines { get; set; }

  protected List<AllocationServiceBase.POReceiptLineAdjustment> PPVTransactions { get; set; }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.GetTrackedExceptChildren" />
  /// </summary>
  [PXOverride]
  public virtual PXSelectBase[] GetTrackedExceptChildren(Func<PXSelectBase[]> baseImpl)
  {
    return ((IEnumerable<PXSelectBase>) baseImpl()).Union<PXSelectBase>((IEnumerable<PXSelectBase>) new PXSelectBase[2]
    {
      (PXSelectBase) this.poOrderPrepUpd,
      (PXSelectBase) this.poOrderLineUPD
    }).ToArray<PXSelectBase>();
  }

  [VendorActiveOrHoldPayments]
  [PXDefault(typeof (PX.Objects.AP.Vendor.bAccountID))]
  public virtual void POVendorInventoryPriceUpdate_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXOverride]
  public void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    persister.Update(((PXSelectBase) this.poReceiptLineUPD).Cache);
    persister.Update(((PXSelectBase) this.poReceiptUPD).Cache);
    persister.Update(((PXSelectBase) this.poOrderLineUPD).Cache);
    persister.Insert(((PXSelectBase) this.poOrderPrepUpd).Cache);
    persister.Update(((PXSelectBase) this.poOrderPrepUpd).Cache);
    persister.Insert(((PXSelectBase) this.poAdjustments).Cache);
    persister.Update(((PXSelectBase) this.poAdjustments).Cache);
    persister.Update(((PXSelectBase) this.poOrderUPD).Cache);
    persister.Insert(((PXSelectBase) this.poVendorInventoryPriceUpdate).Cache);
    persister.Update(((PXSelectBase) this.poVendorInventoryPriceUpdate).Cache);
    persister.Insert(((PXSelectBase) this.poAccrualUpdate).Cache);
    persister.Update(((PXSelectBase) this.poAccrualUpdate).Cache);
    persister.Insert(((PXSelectBase) this.poAccrualDetailUpdate).Cache);
    persister.Update(((PXSelectBase) this.poAccrualDetailUpdate).Cache);
    persister.Insert(((PXSelectBase) this.poAccrualSplitUpdate).Cache);
    persister.Update(((PXSelectBase) this.poAccrualSplitUpdate).Cache);
    persister.Update(((PXSelectBase) this.poTaxUpdate).Cache);
    persister.Update(((PXSelectBase) this.poTaxTranUpdate).Cache);
    persister.Update(((PXSelectBase) this.landedCostDetails).Cache);
  }

  [PXOverride]
  public virtual List<PX.Objects.AP.APRegister> ReleaseInvoice(
    JournalEntry je,
    ref PX.Objects.AP.APRegister doc,
    PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AP.Vendor> res,
    bool isPrebooking,
    out List<PX.Objects.IN.INRegister> inDocs,
    UpdatePOOnRelease.ReleaseInvoiceHandler baseImpl)
  {
    this.PPVLines = new List<PX.Objects.AP.APTran>();
    this.PPVTransactions = new List<AllocationServiceBase.POReceiptLineAdjustment>();
    return baseImpl(je, ref doc, res, isPrebooking, out inDocs);
  }

  /// <summary>
  /// Extends <see cref="M:PX.Objects.AP.APReleaseProcess.InvoiceTransactionsReleased(PX.Objects.AP.InvoiceTransactionsReleasedArgs)" />
  /// </summary>
  [PXOverride]
  public virtual void InvoiceTransactionsReleased(InvoiceTransactionsReleasedArgs args)
  {
    if (args.Invoice.DocType == "PPM")
    {
      this.UpdatePOOrderPrepaymentOnRelease(args.Invoice, args.IsPrebooking);
    }
    else
    {
      if (!this.PPVTransactions.Any<AllocationServiceBase.POReceiptLineAdjustment>())
        return;
      PX.Objects.IN.INRegister ppvAdjustment = this.CreatePPVAdjustment(args.Invoice);
      foreach (PX.Objects.AP.APTran ppvLine in this.PPVLines)
      {
        ppvLine.PPVDocType = ppvAdjustment.DocType;
        ppvLine.PPVRefNbr = ppvAdjustment.RefNbr;
        ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APReleaseProcess>) this).Base.APTran_TranType_RefNbr).Update(ppvLine);
      }
      foreach (POAccrualDetail poAccrualDetail in this.PPVLines.Select<PX.Objects.AP.APTran, POAccrualDetail>((Func<PX.Objects.AP.APTran, POAccrualDetail>) (n => this.PrepareAPTranAccrualDetail(n, (PX.Objects.AP.APRegister) args.Invoice))).Where<POAccrualDetail>((Func<POAccrualDetail, bool>) (x => x != null)).ToList<POAccrualDetail>())
      {
        poAccrualDetail.PPVAdjRefNbr = ppvAdjustment.RefNbr;
        poAccrualDetail.PPVAdjPosted = new bool?(false);
        ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Update(poAccrualDetail);
        POAccrualStatus poAccrualStatus1 = ((PXSelectBase<POAccrualStatus>) this.poAccrualUpdate).Locate(new POAccrualStatus()
        {
          RefNoteID = poAccrualDetail.POAccrualRefNoteID,
          LineNbr = poAccrualDetail.POAccrualLineNbr,
          Type = poAccrualDetail.POAccrualType
        });
        if (poAccrualStatus1 != null)
        {
          POAccrualStatus poAccrualStatus2 = poAccrualStatus1;
          int? unreleasedPpvAdjCntr = poAccrualStatus2.UnreleasedPPVAdjCntr;
          poAccrualStatus2.UnreleasedPPVAdjCntr = unreleasedPpvAdjCntr.HasValue ? new int?(unreleasedPpvAdjCntr.GetValueOrDefault() + 1) : new int?();
          ((PXSelectBase<POAccrualStatus>) this.poAccrualUpdate).Update(poAccrualStatus1);
        }
      }
      args.INDocuments.Add(ppvAdjustment);
    }
  }

  public virtual PX.Objects.IN.INRegister CreatePPVAdjustment(PX.Objects.AP.APInvoice apdoc)
  {
    INAdjustmentEntry instance = PXGraph.CreateInstance<INAdjustmentEntry>();
    ((PXGraph) instance).Clear();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.inventoryID>(UpdatePOOnRelease.\u003C\u003Ec.\u003C\u003E9__28_0 ?? (UpdatePOOnRelease.\u003C\u003Ec.\u003C\u003E9__28_0 = new PXFieldVerifying((object) UpdatePOOnRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreatePPVAdjustment\u003Eb__28_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.origRefNbr>(UpdatePOOnRelease.\u003C\u003Ec.\u003C\u003E9__28_1 ?? (UpdatePOOnRelease.\u003C\u003Ec.\u003C\u003E9__28_1 = new PXFieldVerifying((object) UpdatePOOnRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreatePPVAdjustment\u003Eb__28_1))));
    ((PXSelectBase<INSetup>) instance.insetup).Current.RequireControlTotal = new bool?(false);
    ((PXSelectBase<INSetup>) instance.insetup).Current.HoldEntry = new bool?(false);
    ((PXSelectBase<PX.Objects.IN.INRegister>) instance.adjustment).Insert(new PX.Objects.IN.INRegister()
    {
      DocType = "A",
      OrigModule = "AP",
      OrigRefNbr = apdoc.RefNbr,
      SiteID = new int?(),
      TranDate = apdoc.DocDate,
      FinPeriodID = apdoc.FinPeriodID,
      BranchID = apdoc.BranchID,
      IsPPVTran = new bool?(true)
    });
    this.GetPurchasePriceVarianceINAdjustmentFactory(instance).CreateAdjustmentTran(this.PPVTransactions, ((PXGraphExtension<APReleaseProcess>) this).Base.posetup.PPVReasonCodeID);
    ((PXAction) instance.Save).Press();
    return ((PXSelectBase<PX.Objects.IN.INRegister>) instance.adjustment).Current;
  }

  public virtual PurchasePriceVarianceINAdjustmentFactory GetPurchasePriceVarianceINAdjustmentFactory(
    INAdjustmentEntry inGraph)
  {
    return new PurchasePriceVarianceINAdjustmentFactory(inGraph);
  }

  /// <summary>
  /// Extends <see cref="M:PX.Objects.AP.APReleaseProcess.InvoiceTransactionReleasing(PX.Objects.AP.InvoiceTransactionReleasingArgs)" />
  /// </summary>
  [PXOverride]
  public virtual void InvoiceTransactionReleasing(InvoiceTransactionReleasingArgs args)
  {
    PX.Objects.PO.POReceiptLine receiptUnderCorrection = this.GetReceiptUnderCorrection(args.Transaction);
    if (receiptUnderCorrection != null)
      throw new PXException("The linked purchase receipt ({0}) is under correction or canceled.", new object[1]
      {
        (object) receiptUnderCorrection.ReceiptNbr
      });
    if (args.Invoice.DocType == "PPM")
    {
      PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder> pxResult = (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>) PXResultset<PX.Objects.PO.POLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POLine>) this.poOrderLineUPD).Select(new object[3]
      {
        (object) args.Transaction.POOrderType,
        (object) args.Transaction.PONbr,
        (object) args.Transaction.POLineNbr
      }));
      if (pxResult == null)
        return;
      PX.Objects.PO.POLine updLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      this.UpdatePOLine(args.Transaction, args.Invoice, PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), updLine, args.IsPrebooking);
    }
    else
    {
      PX.Objects.AP.APInvoice invoice = args.Invoice;
      bool flag1 = !args.IsPrebooking;
      bool flag2 = !args.IsPrebooking;
      this.UnlinkRelatedLandedCosts(args.Register, args.Transaction);
      this.ApplyLandedCostVariance(args.IsPrebooking, args.IsPrebookVoiding, args.JournalEntry, args.GLTransaction, args.CurrencyInfo, args.Transaction, args.LandedCostCode, args.PostedAmount);
      POAccrualStatus origRow = this.ApplyPurchasePriceVariance(args.IsPrebooking, args.JournalEntry, args.GLTransaction, args.CurrencyInfo, invoice, args.Transaction);
      PX.Objects.PO.POReceiptLine rctLine = (PX.Objects.PO.POReceiptLine) null;
      PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder> pxResult = (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>) PXResultset<PX.Objects.PO.POLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POLine>) this.poOrderLineUPD).Select(new object[3]
      {
        (object) args.Transaction.POOrderType,
        (object) args.Transaction.PONbr,
        (object) args.Transaction.POLineNbr
      }));
      if (pxResult != null)
      {
        PX.Objects.GL.GLTran glTransaction = args.GLTransaction;
        bool? reclassificationProhibited = glTransaction.ReclassificationProhibited;
        glTransaction.ReclassificationProhibited = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult).CommitmentID.HasValue ? new bool?(true) : reclassificationProhibited;
      }
      int? nullable1;
      Decimal? curyUnitCost;
      if (((((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck ? 0 : (!((PXGraphExtension<APReleaseProcess>) this).Base.IsInvoiceReclassification ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
      {
        if (!string.IsNullOrEmpty(args.Transaction.ReceiptNbr))
        {
          nullable1 = args.Transaction.ReceiptLineNbr;
          if (nullable1.HasValue)
            goto label_13;
        }
        if (args.Transaction.POAccrualType == "O" && !string.IsNullOrEmpty(args.Transaction.POOrderType) && !string.IsNullOrEmpty(args.Transaction.PONbr))
        {
          nullable1 = args.Transaction.POLineNbr;
          if (!nullable1.HasValue)
            goto label_26;
        }
        else
          goto label_26;
label_13:
        if (((PXGraphExtension<APReleaseProcess>) this).Base.apsetup?.VendorPriceUpdate == "B")
        {
          nullable1 = args.Transaction.InventoryID;
          if (nullable1.HasValue)
          {
            curyUnitCost = args.Transaction.CuryUnitCost;
            if (curyUnitCost.HasValue)
            {
              int? nullable2 = new int?();
              if (!string.IsNullOrEmpty(args.Transaction.ReceiptNbr))
              {
                nullable1 = args.Transaction.ReceiptLineNbr;
                if (nullable1.HasValue)
                {
                  rctLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.poReceiptLineUPD).Select(new object[3]
                  {
                    (object) args.Transaction.ReceiptType,
                    (object) args.Transaction.ReceiptNbr,
                    (object) args.Transaction.ReceiptLineNbr
                  }));
                  int? nullable3;
                  if (rctLine == null)
                  {
                    nullable1 = new int?();
                    nullable3 = nullable1;
                  }
                  else
                    nullable3 = rctLine.SubItemID;
                  nullable2 = nullable3;
                  goto label_24;
                }
              }
              if (pxResult != null)
                nullable2 = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult).SubItemID;
label_24:
              if (nullable2.HasValue)
              {
                APReleaseProcess graph = ((PXGraphExtension<APReleaseProcess>) this).Base;
                int? vendorId = args.Register.VendorID;
                int? vendorLocationId = args.Register.VendorLocationID;
                string curyId = args.Register.CuryID;
                int? inventoryId = args.Transaction.InventoryID;
                int? subItemID = nullable2;
                string uom = args.Transaction.UOM;
                curyUnitCost = args.Transaction.CuryUnitCost;
                Decimal curyCost = curyUnitCost.Value;
                POItemCostManager.Update((PXGraph) graph, vendorId, vendorLocationId, curyId, inventoryId, subItemID, uom, curyCost);
              }
            }
          }
        }
      }
label_26:
      int num1;
      if (!((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck && !((PXGraphExtension<APReleaseProcess>) this).Base.IsInvoiceReclassification && (!PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || ((PXGraphExtension<APReleaseProcess>) this).Base.apsetup?.VendorPriceUpdate == "B" || args.Transaction.PONbr == null && args.Transaction.ReceiptNbr == null))
      {
        nullable1 = args.Transaction.InventoryID;
        if (nullable1.HasValue)
        {
          curyUnitCost = args.Transaction.CuryUnitCost;
          num1 = curyUnitCost.HasValue ? 1 : 0;
          goto label_30;
        }
      }
      num1 = 0;
label_30:
      int num2 = flag2 ? 1 : 0;
      if ((num1 & num2) != 0)
      {
        APReleaseProcess graph = ((PXGraphExtension<APReleaseProcess>) this).Base;
        int? vendorId = args.Register.VendorID;
        int? vendorLocationId = args.Register.VendorLocationID;
        string curyId = args.Register.CuryID;
        int? inventoryId = args.Transaction.InventoryID;
        nullable1 = new int?();
        int? subItemID = nullable1;
        string uom = args.Transaction.UOM;
        curyUnitCost = args.Transaction.CuryUnitCost;
        Decimal curyCost = curyUnitCost.Value;
        POItemCostManager.Update((PXGraph) graph, vendorId, vendorLocationId, curyId, inventoryId, subItemID, uom, curyCost);
      }
      this.UpdatePOAccrualStatus(origRow, args.Transaction, invoice, PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), rctLine);
      this.UpdatePOAccrualDetail(args.Transaction, invoice);
      if (pxResult == null)
        return;
      PX.Objects.PO.POLine updLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      this.UpdatePOLine(args.Transaction, invoice, PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder>.op_Implicit(pxResult), updLine, args.IsPrebooking);
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AP.APReleaseProcess.ProcessPrepaymentRequestApplication(PX.Objects.AP.APRegister,PX.Objects.AP.APAdjust)" />
  /// </summary>
  /// <param name="prepaymentRequest"></param>
  /// <param name="prepaymentAdj"></param>
  [PXOverride]
  public virtual void ProcessPrepaymentRequestApplication(
    PX.Objects.AP.APRegister prepaymentRequest,
    APAdjust prepaymentAdj,
    Action<PX.Objects.AP.APRegister, APAdjust> baseImpl)
  {
    baseImpl(prepaymentRequest, prepaymentAdj);
    if (!(prepaymentAdj.AdjgDocType == "VCK") && (!prepaymentAdj.VoidAdjNbr.HasValue || !(prepaymentAdj.AdjgDocType != "VRF")))
      return;
    this.VoidPOOrderPrepaymentRequest(prepaymentRequest);
  }

  protected virtual void VoidPOOrderPrepaymentRequest(PX.Objects.AP.APRegister payRegister)
  {
    if (((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck || payRegister.DocType != "PPM" || payRegister.OrigModule != "PO")
      return;
    foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.AP.APTran> pxResult in PXSelectBase<PX.Objects.PO.POLine, PXSelectJoin<PX.Objects.PO.POLine, InnerJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.pOOrderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.AP.APTran.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.AP.APTran.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>>, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[2]
    {
      (object) payRegister.DocType,
      (object) payRegister.RefNbr
    }))
    {
      PX.Objects.PO.POLine copy = PXCache<PX.Objects.PO.POLine>.CreateCopy(PXResult<PX.Objects.PO.POLine, PX.Objects.AP.APTran>.op_Implicit(pxResult));
      PX.Objects.AP.APTran apTran = PXResult<PX.Objects.PO.POLine, PX.Objects.AP.APTran>.op_Implicit(pxResult);
      PX.Objects.PO.POLine poLine1 = copy;
      Decimal? nullable1 = poLine1.ReqPrepaidQty;
      Decimal? nullable2 = apTran.Qty;
      poLine1.ReqPrepaidQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      PX.Objects.PO.POLine poLine2 = copy;
      nullable2 = poLine2.CuryReqPrepaidAmt;
      Decimal? curyTranAmt = apTran.CuryTranAmt;
      Decimal? nullable3 = apTran.CuryRetainageAmt;
      nullable1 = curyTranAmt.HasValue & nullable3.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable4;
      if (!(nullable2.HasValue & nullable1.HasValue))
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
      poLine2.CuryReqPrepaidAmt = nullable4;
      ((PXSelectBase<PX.Objects.PO.POLine>) this.poOrderLineUPD).Update(copy);
    }
    POOrderPrepayment poOrderPrepayment = PXResultset<POOrderPrepayment>.op_Implicit(((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Select(new object[2]
    {
      (object) payRegister.DocType,
      (object) payRegister.RefNbr
    }));
    if (poOrderPrepayment == null)
      return;
    POOrderPrepayment copy1 = PXCache<POOrderPrepayment>.CreateCopy(poOrderPrepayment);
    copy1.CuryAppliedAmt = new Decimal?(0M);
    ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Update(copy1);
    PX.Objects.PO.POOrder copy2 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Select(new object[2]
    {
      (object) poOrderPrepayment.OrderType,
      (object) poOrderPrepayment.OrderNbr
    })));
    PX.Objects.PO.POOrder poOrder = copy2;
    Decimal? curyPrepaidTotal = poOrder.CuryPrepaidTotal;
    Decimal? curyOrigDocAmt = payRegister.CuryOrigDocAmt;
    poOrder.CuryPrepaidTotal = curyPrepaidTotal.HasValue & curyOrigDocAmt.HasValue ? new Decimal?(curyPrepaidTotal.GetValueOrDefault() - curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
    ((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Update(copy2);
  }

  public virtual POAccrualStatus UpdatePOAccrualStatus(
    POAccrualStatus origRow,
    PX.Objects.AP.APTran tran,
    PX.Objects.AP.APInvoice apdoc,
    PX.Objects.PO.POLine poLine,
    PX.Objects.PO.POOrder order,
    PX.Objects.PO.POReceiptLine rctLine)
  {
    if (((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck || ((PXGraphExtension<APReleaseProcess>) this).Base.IsInvoiceReclassification || tran.POAccrualType == null)
      return (POAccrualStatus) null;
    PXCache cache1 = ((PXSelectBase) this.poAccrualUpdate).Cache;
    POAccrualStatus row1;
    if (origRow == null)
    {
      POAccrualStatus poAccrualStatus = new POAccrualStatus()
      {
        Type = tran.POAccrualType,
        RefNoteID = tran.POAccrualRefNoteID,
        LineNbr = tran.POAccrualLineNbr
      };
      row1 = (POAccrualStatus) cache1.Insert((object) poAccrualStatus);
    }
    else
      row1 = (POAccrualStatus) cache1.CreateCopy((object) origRow);
    this.SetIfNotNull<POAccrualStatus.dropshipExpenseRecording>(cache1, row1, (object) order?.DropshipExpenseRecording);
    this.SetIfNotNull<POAccrualStatus.lineType>(cache1, row1, (object) tran.LineType);
    this.SetIfNotNull<POAccrualStatus.orderType>(cache1, row1, (object) tran.POOrderType);
    this.SetIfNotNull<POAccrualStatus.orderNbr>(cache1, row1, (object) tran.PONbr);
    this.SetIfNotNull<POAccrualStatus.orderLineNbr>(cache1, row1, (object) tran.POLineNbr);
    this.SetIfNotNull<POAccrualStatus.receiptType>(cache1, row1, (object) tran.ReceiptType);
    this.SetIfNotNull<POAccrualStatus.receiptNbr>(cache1, row1, (object) tran.ReceiptNbr);
    if (row1.MaxFinPeriodID == null || apdoc.FinPeriodID.CompareTo(row1.MaxFinPeriodID) > 0)
      this.SetIfNotNull<POAccrualStatus.maxFinPeriodID>(cache1, row1, (object) apdoc.FinPeriodID);
    this.SetIfNotNull<POAccrualStatus.origUOM>(cache1, row1, (object) tran.UOM);
    this.SetIfNotNull<POAccrualStatus.origCuryID>(cache1, row1, (object) order?.CuryID);
    this.SetIfNotNull<POAccrualStatus.vendorID>(cache1, row1, (object) ((int?) order?.VendorID ?? tran.VendorID));
    this.SetIfNotNull<POAccrualStatus.payToVendorID>(cache1, row1, (object) (int?) order?.PayToVendorID);
    this.SetIfNotNull<POAccrualStatus.inventoryID>(cache1, row1, (object) tran.InventoryID);
    this.SetIfNotNull<POAccrualStatus.subItemID>(cache1, row1, (object) (int?) ((int?) rctLine?.SubItemID ?? poLine?.SubItemID));
    this.SetIfNotNull<POAccrualStatus.siteID>(cache1, row1, (object) (int?) ((int?) rctLine?.SiteID ?? poLine?.SiteID));
    if (POLineType.UsePOAccrual(tran.LineType))
    {
      this.SetIfNotNull<POAccrualStatus.acctID>(cache1, row1, (object) tran.AccountID);
      this.SetIfNotNull<POAccrualStatus.subID>(cache1, row1, (object) tran.SubID);
    }
    ARReleaseProcess.Amount amount = (ARReleaseProcess.Amount) null;
    if (poLine != null && poLine.OrderNbr != null)
      amount = APReleaseProcess.GetExpensePostingAmount((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, poLine);
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
    int num1;
    if (origRow != null)
    {
      nullable3 = origRow.BilledQty;
      num1 = !nullable3.HasValue ? 1 : (!EnumerableExtensions.IsIn<string>(origRow.BilledUOM, (string) null, tran.UOM) ? 1 : 0);
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    row1.BilledUOM = flag1 ? (string) null : tran.UOM;
    POAccrualStatus poAccrualStatus1 = row1;
    nullable3 = poAccrualStatus1.BilledQty;
    nullable1 = flag1 ? new Decimal?() : tran.SignedQty;
    poAccrualStatus1.BilledQty = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    POAccrualStatus poAccrualStatus2 = row1;
    nullable1 = poAccrualStatus2.BaseBilledQty;
    Decimal sign1 = tran.Sign;
    Decimal? baseQty = tran.BaseQty;
    nullable3 = baseQty.HasValue ? new Decimal?(sign1 * baseQty.GetValueOrDefault()) : new Decimal?();
    poAccrualStatus2.BaseBilledQty = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    ARReleaseProcess.Amount expensePostingAmount = APReleaseProcess.GetExpensePostingAmount((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, tran);
    int num2;
    if (origRow != null)
    {
      nullable3 = origRow.CuryBilledCost;
      num2 = !nullable3.HasValue ? 1 : (!EnumerableExtensions.IsIn<string>(origRow.BillCuryID, (string) null, apdoc.CuryID) ? 1 : 0);
    }
    else
      num2 = 0;
    bool flag2 = num2 != 0;
    row1.BillCuryID = flag2 ? (string) null : apdoc.CuryID;
    POAccrualStatus poAccrualStatus3 = row1;
    nullable3 = poAccrualStatus3.CuryBilledAmt;
    Decimal? nullable9;
    if (!flag2)
    {
      Decimal sign2 = tran.Sign;
      Decimal? curyTranAmt = tran.CuryTranAmt;
      Decimal? curyRetainageAmt = tran.CuryRetainageAmt;
      Decimal? nullable10 = curyTranAmt.HasValue & curyRetainageAmt.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
      nullable9 = nullable10.HasValue ? new Decimal?(sign2 * nullable10.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable9 = new Decimal?();
    nullable1 = nullable9;
    poAccrualStatus3.CuryBilledAmt = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    POAccrualStatus poAccrualStatus4 = row1;
    nullable1 = poAccrualStatus4.BilledAmt;
    Decimal sign3 = tran.Sign;
    Decimal? tranAmt = tran.TranAmt;
    Decimal? nullable11 = tran.RetainageAmt;
    Decimal? nullable12 = tranAmt.HasValue & nullable11.HasValue ? new Decimal?(tranAmt.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable13;
    if (!nullable12.HasValue)
    {
      nullable11 = new Decimal?();
      nullable13 = nullable11;
    }
    else
      nullable13 = new Decimal?(sign3 * nullable12.GetValueOrDefault());
    nullable3 = nullable13;
    poAccrualStatus4.BilledAmt = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    POAccrualStatus poAccrualStatus5 = row1;
    nullable3 = poAccrualStatus5.CuryBilledCost;
    Decimal? nullable14;
    if (!flag2)
    {
      Decimal sign4 = tran.Sign;
      Decimal? cury = expensePostingAmount.Cury;
      if (!cury.HasValue)
      {
        nullable11 = new Decimal?();
        nullable14 = nullable11;
      }
      else
        nullable14 = new Decimal?(sign4 * cury.GetValueOrDefault());
    }
    else
      nullable14 = new Decimal?();
    nullable1 = nullable14;
    poAccrualStatus5.CuryBilledCost = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    POAccrualStatus poAccrualStatus6 = row1;
    nullable1 = poAccrualStatus6.BilledCost;
    Decimal sign5 = tran.Sign;
    Decimal? nullable15 = expensePostingAmount.Base;
    Decimal? nullable16;
    if (!nullable15.HasValue)
    {
      nullable11 = new Decimal?();
      nullable16 = nullable11;
    }
    else
      nullable16 = new Decimal?(sign5 * nullable15.GetValueOrDefault());
    nullable3 = nullable16;
    poAccrualStatus6.BilledCost = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    POAccrualStatus poAccrualStatus7 = row1;
    nullable3 = poAccrualStatus7.CuryBilledDiscAmt;
    Decimal? nullable17;
    if (!flag2)
    {
      Decimal sign6 = tran.Sign;
      Decimal? curyDiscAmt = tran.CuryDiscAmt;
      if (!curyDiscAmt.HasValue)
      {
        nullable11 = new Decimal?();
        nullable17 = nullable11;
      }
      else
        nullable17 = new Decimal?(sign6 * curyDiscAmt.GetValueOrDefault());
    }
    else
      nullable17 = new Decimal?();
    nullable1 = nullable17;
    poAccrualStatus7.CuryBilledDiscAmt = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    POAccrualStatus poAccrualStatus8 = row1;
    nullable1 = poAccrualStatus8.BilledDiscAmt;
    Decimal sign7 = tran.Sign;
    Decimal? discAmt = tran.DiscAmt;
    Decimal? nullable18;
    if (!discAmt.HasValue)
    {
      nullable11 = new Decimal?();
      nullable18 = nullable11;
    }
    else
      nullable18 = new Decimal?(sign7 * discAmt.GetValueOrDefault());
    nullable3 = nullable18;
    poAccrualStatus8.BilledDiscAmt = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    return ((PXSelectBase<POAccrualStatus>) this.poAccrualUpdate).Update(row1);
  }

  public virtual bool StorePOAccrualDetail(PX.Objects.AP.APTran tran)
  {
    if (tran.POAccrualType == null || !EnumerableExtensions.IsNotIn<string>(tran.LineType, "SV", "FT"))
      return false;
    return tran.POOrderType != "PD" || tran.DropshipExpenseRecording != "B";
  }

  public virtual POAccrualDetail FindPOReceiptLineAccrualDetail(POAccrualSplit split)
  {
    return this.FindPOReceiptLineAccrualDetail(split.POReceiptType, split.POReceiptNbr, split.POReceiptLineNbr);
  }

  protected virtual POAccrualDetail FindPOReceiptLineAccrualDetail(
    string receiptType,
    string receiptNbr,
    int? lineNbr)
  {
    return PXResultset<POAccrualDetail>.op_Implicit(PXSelectBase<POAccrualDetail, PXViewOf<POAccrualDetail>.BasedOn<SelectFromBase<POAccrualDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.pOReceiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<POAccrualDetail.pOReceiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<POAccrualDetail.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, 0, 1, new object[3]
    {
      (object) receiptType,
      (object) receiptNbr,
      (object) lineNbr
    }));
  }

  public virtual POAccrualDetail PrepareAPTranAccrualDetail(PX.Objects.AP.APTran tran, PX.Objects.AP.APRegister doc)
  {
    if (((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck || !this.StorePOAccrualDetail(tran))
      return (POAccrualDetail) null;
    POAccrualDetail poAccrualDetail1 = ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Locate(new POAccrualDetail()
    {
      DocumentNoteID = doc.NoteID,
      LineNbr = tran.LineNbr
    });
    if (poAccrualDetail1 == null)
      poAccrualDetail1 = PXResultset<POAccrualDetail>.op_Implicit(((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Select(new object[2]
      {
        (object) doc.NoteID,
        (object) tran.LineNbr
      }));
    POAccrualDetail poAccrualDetail2 = poAccrualDetail1;
    if (poAccrualDetail2 == null)
      poAccrualDetail2 = ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Insert(new POAccrualDetail()
      {
        DocumentNoteID = doc.NoteID,
        APDocType = tran.TranType,
        APRefNbr = tran.RefNbr,
        LineNbr = tran.LineNbr,
        POAccrualRefNoteID = tran.POAccrualRefNoteID,
        POAccrualLineNbr = tran.POAccrualLineNbr,
        POAccrualType = tran.POAccrualType,
        VendorID = tran.VendorID,
        IsDropShip = new bool?(POLineType.IsDropShip(tran.LineType)),
        BranchID = tran.BranchID,
        DocDate = tran.TranDate,
        FinPeriodID = tran.FinPeriodID,
        TranDesc = tran.TranDesc,
        UOM = tran.UOM,
        Posted = new bool?(true)
      });
    return poAccrualDetail2;
  }

  protected virtual POAccrualDetail UpdatePOAccrualDetail(PX.Objects.AP.APTran tran, PX.Objects.AP.APInvoice apdoc)
  {
    POAccrualDetail poAccrualDetail1 = this.PrepareAPTranAccrualDetail(tran, (PX.Objects.AP.APRegister) apdoc);
    if (poAccrualDetail1 == null)
      return (POAccrualDetail) null;
    POAccrualDetail poAccrualDetail2 = poAccrualDetail1;
    Decimal? nullable1 = poAccrualDetail2.AccruedQty;
    Decimal? signedQty = tran.SignedQty;
    poAccrualDetail2.AccruedQty = nullable1.HasValue & signedQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + signedQty.GetValueOrDefault()) : new Decimal?();
    POAccrualDetail poAccrualDetail3 = poAccrualDetail1;
    Decimal? nullable2 = poAccrualDetail3.BaseAccruedQty;
    Decimal sign1 = tran.Sign;
    Decimal? nullable3 = tran.BaseQty;
    nullable1 = nullable3.HasValue ? new Decimal?(sign1 * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable3 = new Decimal?();
      nullable4 = nullable3;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    poAccrualDetail3.BaseAccruedQty = nullable4;
    ARReleaseProcess.Amount expensePostingAmount = APReleaseProcess.GetExpensePostingAmount((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, tran);
    POAccrualDetail poAccrualDetail4 = poAccrualDetail1;
    nullable1 = poAccrualDetail4.AccruedCost;
    Decimal sign2 = tran.Sign;
    nullable3 = expensePostingAmount.Base;
    nullable2 = nullable3.HasValue ? new Decimal?(sign2 * nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable3 = new Decimal?();
      nullable5 = nullable3;
    }
    else
      nullable5 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    poAccrualDetail4.AccruedCost = nullable5;
    POAccrualDetail poAccrualDetail5 = poAccrualDetail1;
    nullable2 = poAccrualDetail5.PPVAmt;
    nullable1 = tran.POPPVAmt;
    Decimal? nullable6;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
    poAccrualDetail5.PPVAmt = nullable6;
    poAccrualDetail1.PPVAdjPosted = new bool?(true);
    poAccrualDetail1.TaxAdjPosted = new bool?(true);
    POAccrualDetail poAccrualDetail6 = ((PXSelectBase) this.poAccrualDetailUpdate).Cache.Updated.OfType<POAccrualDetail>().FirstOrDefault<POAccrualDetail>((Func<POAccrualDetail, bool>) (x =>
    {
      if (x.APDocType == apdoc.OrigDocType && x.APRefNbr == apdoc.OrigRefNbr)
      {
        int? lineNbr = x.LineNbr;
        int? origLineNbr = tran.OrigLineNbr;
        if (lineNbr.GetValueOrDefault() == origLineNbr.GetValueOrDefault() & lineNbr.HasValue == origLineNbr.HasValue)
          return x.IsReversed.GetValueOrDefault();
      }
      return false;
    }));
    poAccrualDetail1.IsReversing = new bool?(poAccrualDetail6 != null);
    poAccrualDetail1.ReversingFinPeriodID = poAccrualDetail6?.FinPeriodID;
    return ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Update(poAccrualDetail1);
  }

  protected virtual POAccrualSplit InsertPOAccrualSplit(
    POAccrualStatus poAccrual,
    PX.Objects.AP.APTran tran,
    PX.Objects.PO.POReceiptLine rctLine,
    string accruedUom,
    Decimal? accruedQty,
    Decimal? baseAccruedQty,
    Decimal? accruedCost,
    Decimal? ppvAmt)
  {
    PX.Objects.PO.POReceipt poReceipt1 = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.poReceiptUPD).Locate(new PX.Objects.PO.POReceipt()
    {
      ReceiptType = rctLine.ReceiptType,
      ReceiptNbr = rctLine.ReceiptNbr
    });
    if (poReceipt1 == null)
      poReceipt1 = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) this.poReceiptUPD).Select(new object[2]
      {
        (object) rctLine.ReceiptType,
        (object) rctLine.ReceiptNbr
      }));
    PX.Objects.PO.POReceipt poReceipt2 = poReceipt1;
    POAccrualSplit poAccrualSplit1 = ((PXSelectBase<POAccrualSplit>) this.poAccrualSplitUpdate).Insert(new POAccrualSplit()
    {
      RefNoteID = poAccrual.RefNoteID,
      LineNbr = poAccrual.LineNbr,
      Type = poAccrual.Type,
      APDocType = tran.TranType,
      APRefNbr = tran.RefNbr,
      APLineNbr = tran.LineNbr,
      POReceiptType = rctLine.ReceiptType,
      POReceiptNbr = rctLine.ReceiptNbr,
      POReceiptLineNbr = rctLine.LineNbr
    });
    poAccrualSplit1.UOM = accruedUom;
    POAccrualSplit poAccrualSplit2 = poAccrualSplit1;
    Decimal? nullable1 = accruedQty;
    Decimal apSign1 = this.GetAPSign(rctLine);
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * apSign1) : new Decimal?();
    poAccrualSplit2.AccruedQty = nullable2;
    POAccrualSplit poAccrualSplit3 = poAccrualSplit1;
    nullable1 = baseAccruedQty;
    Decimal apSign2 = this.GetAPSign(rctLine);
    Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * apSign2) : new Decimal?();
    poAccrualSplit3.BaseAccruedQty = nullable3;
    POAccrualSplit poAccrualSplit4 = poAccrualSplit1;
    nullable1 = accruedCost;
    Decimal apSign3 = this.GetAPSign(rctLine);
    Decimal? nullable4 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * apSign3) : new Decimal?();
    poAccrualSplit4.AccruedCost = nullable4;
    poAccrualSplit1.PPVAmt = ppvAmt;
    poAccrualSplit1.FinPeriodID = ((IEnumerable<string>) new string[2]
    {
      poReceipt2.FinPeriodID,
      tran.FinPeriodID
    }).Max<string>();
    if (poReceipt2.POType == "DP" && rctLine.INReleased.GetValueOrDefault())
    {
      INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.docType, Equal<INDocType.issue>>>>, And<BqlOperand<INTran.pOReceiptType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<INTran.pOReceiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INTran.pOReceiptLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, 0, 1, new object[3]
      {
        (object) rctLine.ReceiptType,
        (object) rctLine.ReceiptNbr,
        (object) rctLine.LineNbr
      }));
      if (inTran != null && inTran.FinPeriodID.CompareTo(poAccrualSplit1.FinPeriodID) > 0)
        poAccrualSplit1.FinPeriodID = inTran.FinPeriodID;
    }
    return ((PXSelectBase<POAccrualSplit>) this.poAccrualSplitUpdate).Update(poAccrualSplit1);
  }

  protected virtual POAccrualStatus ApplyPurchasePriceVariance(
    bool isPrebooking,
    JournalEntry je,
    PX.Objects.GL.GLTran patternTran,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo,
    PX.Objects.AP.APInvoice apdoc,
    PX.Objects.AP.APTran tran)
  {
    if (((((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck ? 1 : (((PXGraphExtension<APReleaseProcess>) this).Base.IsInvoiceReclassification ? 1 : 0)) | (isPrebooking ? 1 : 0)) != 0 || tran.POAccrualType == null)
      return (POAccrualStatus) null;
    POAccrualStatus poAccrual = POAccrualStatus.PK.FindDirty((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, tran.POAccrualRefNoteID, tran.POAccrualLineNbr, tran.POAccrualType);
    if (poAccrual == null)
    {
      tran.UnreceivedQty = tran.Qty;
      return poAccrual;
    }
    PPVCalculationResult ppv = this.CalculatePPV(apdoc, tran, poAccrual, (Action<OnAccrualSplitDefinedArgs>) (args =>
    {
      this.ApplyPPVAmount(args.PPVAmount, je, patternTran, tran, args.ReceiptLineToBill, curyInfo);
      Decimal? baseBillQty;
      this.UpdatePOReceiptLine(args.ReceiptLineToBill, tran, args.PPVAmount, args.IsBaseQty, args.ReceiptLineBillQty, out baseBillQty);
      if (args.SplitToReverse != null)
        this.ReversePOAccrual(args.SplitToReverse);
      this.InsertPOAccrualSplit(poAccrual, tran, args.ReceiptLineToBill, !args.IsBaseQty ? tran.UOM : (string) null, !args.IsBaseQty ? args.ReceiptLineBillQty : new Decimal?(), baseBillQty, args.ReceiptLineBillAmount, args.PPVAmount);
    }));
    if (this.IsReverseAPTran(apdoc, tran))
      this.ReverseOrigAPBillPOAccrual(apdoc, tran);
    POAccrualStatus poAccrualStatus = poAccrual;
    Decimal? ppvAmt = poAccrualStatus.PPVAmt;
    Decimal ppvAmount = ppv.PPVAmount;
    poAccrualStatus.PPVAmt = ppvAmt.HasValue ? new Decimal?(ppvAmt.GetValueOrDefault() + ppvAmount) : new Decimal?();
    tran.POPPVAmt = new Decimal?(ppv.PPVAmount);
    if (tran.TranType == "ADR")
    {
      if (tran.POAccrualType == "O")
      {
        Decimal? accrualBilledQty = ppv.AccrualBilledQty;
        Decimal num = 0M;
        if (accrualBilledQty.GetValueOrDefault() < num & accrualBilledQty.HasValue)
        {
          if (tran.POOrderType != "RS")
            throw new PXException("The document cannot be released because its detail line {0} is linked to the purchase order line which billed quantity is zero. Check the {1} purchase order details. Probably, another debit adjustment for the {2} line has already been released.", new object[3]
            {
              (object) tran.LineNbr,
              (object) tran.PONbr,
              (object) tran.POLineNbr
            });
          throw new PXException("The debit adjustment cannot be released because its detail line {0} has the Quantity that is greater than the billed quantity of the corresponding line of the {1} subcontract. To release the document, correct the Quantity first.", new object[2]
          {
            (object) tran.LineNbr,
            (object) tran.PONbr
          });
        }
      }
      if (tran.POAccrualType == "R")
      {
        Decimal? billQty1 = ppv.BillQty;
        Decimal num1 = 0M;
        if (!(billQty1.GetValueOrDefault() == num1 & billQty1.HasValue))
        {
          Decimal? billQty2 = ppv.BillQty;
          Decimal num2 = 0M;
          throw new PXException(billQty2.GetValueOrDefault() > num2 & billQty2.HasValue ? "The quantity in line {0} of the {1} debit adjustment exceeds the unbilled quantity in the linked line of the {2} purchase return. Review the released billing documents on the Billing tab of the Purchase Receipts (PO302000) form." : "The quantity in line {0} of the {1} debit adjustment exceeds the billed quantity in the linked line of the {2} purchase receipt. Review the released billing documents on the Billing tab of the Purchase Receipts (PO302000) form.", new object[3]
          {
            (object) tran.LineNbr,
            (object) tran.RefNbr,
            (object) tran.ReceiptNbr
          });
        }
      }
    }
    else if (tran.POAccrualType == "R" && tran.Sign > 0M)
    {
      Decimal? billQty = ppv.BillQty;
      Decimal num = 0M;
      if (billQty.GetValueOrDefault() > num & billQty.HasValue)
        throw new PXException("The quantity in line {0} of the {1} bill exceeds the unbilled quantity in the linked line of the {2} purchase receipt. Review the released billing documents on the Billing tab of the Purchase Receipts (PO302000) form.", new object[3]
        {
          (object) tran.LineNbr,
          (object) tran.RefNbr,
          (object) tran.ReceiptNbr
        });
    }
    if (!ppv.IsBaseQty)
    {
      PX.Objects.AP.APTran apTran = tran;
      Decimal? billQty = ppv.BillQty;
      Decimal sign = ppv.Sign;
      Decimal? nullable = billQty.HasValue ? new Decimal?(billQty.GetValueOrDefault() * sign) : new Decimal?();
      apTran.UnreceivedQty = nullable;
    }
    else
    {
      PX.Objects.AP.APTran apTran = tran;
      Decimal? billQty = ppv.BillQty;
      Decimal sign = ppv.Sign;
      Decimal? nullable = billQty.HasValue ? new Decimal?(billQty.GetValueOrDefault() * sign) : new Decimal?();
      apTran.BaseUnreceivedQty = nullable;
      PXDBQuantityAttribute.CalcTranQty<PX.Objects.AP.APTran.unreceivedQty>(((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base).Caches[typeof (PX.Objects.AP.APTran)], (object) tran);
    }
    return poAccrual;
  }

  protected virtual PX.Objects.PO.POReceiptLine UpdatePOReceiptLine(
    PX.Objects.PO.POReceiptLine rctLine,
    PX.Objects.AP.APTran tran,
    Decimal? ppvAmt,
    bool isBaseQty,
    Decimal? rctLineBillQty,
    out Decimal? baseBillQty)
  {
    Decimal? nullable1;
    Decimal? nullable2;
    if (!isBaseQty)
    {
      baseBillQty = rctLine.BaseUnbilledQty;
      PX.Objects.PO.POReceiptLine poReceiptLine = rctLine;
      Decimal? unbilledQty = poReceiptLine.UnbilledQty;
      nullable1 = rctLineBillQty;
      poReceiptLine.UnbilledQty = unbilledQty.HasValue & nullable1.HasValue ? new Decimal?(unbilledQty.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      PXDBQuantityAttribute.CalcBaseQty<PX.Objects.PO.POReceiptLine.unbilledQty>(((PXSelectBase) this.poReceiptLineUPD).Cache, (object) rctLine);
      ref Decimal? local = ref baseBillQty;
      nullable1 = baseBillQty;
      Decimal? baseUnbilledQty = rctLine.BaseUnbilledQty;
      Decimal? nullable3 = nullable1.HasValue & baseUnbilledQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - baseUnbilledQty.GetValueOrDefault()) : new Decimal?();
      local = nullable3;
    }
    else
    {
      PX.Objects.PO.POReceiptLine poReceiptLine = rctLine;
      nullable2 = poReceiptLine.BaseUnbilledQty;
      Decimal? nullable4 = rctLineBillQty;
      poReceiptLine.BaseUnbilledQty = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      PXDBQuantityAttribute.CalcTranQty<PX.Objects.PO.POReceiptLine.unbilledQty>(((PXSelectBase) this.poReceiptLineUPD).Cache, (object) rctLine);
      baseBillQty = rctLineBillQty;
    }
    PX.Objects.PO.POReceiptLine poReceiptLine1 = rctLine;
    nullable1 = poReceiptLine1.BillPPVAmt;
    nullable2 = ppvAmt;
    poReceiptLine1.BillPPVAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    return ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.poReceiptLineUPD).Update(rctLine);
  }

  protected virtual void ApplyPPVAmount(
    Decimal? ppvAmt,
    JournalEntry je,
    PX.Objects.GL.GLTran patternTran,
    PX.Objects.AP.APTran tran,
    PX.Objects.PO.POReceiptLine rctLine,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo)
  {
    if (ppvAmt.GetValueOrDefault() == 0M)
      return;
    if (this.IsInventoryPPV(rctLine))
    {
      PX.Objects.PO.POReceiptLine aLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelect<PX.Objects.PO.POReceiptLine, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.lineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[3]
      {
        (object) rctLine.ReceiptType,
        (object) rctLine.ReceiptNbr,
        (object) rctLine.LineNbr
      }));
      if (aLine == null)
        throw new PXException("Purchase Receipt# '{0}' was not found.", new object[1]
        {
          (object) rctLine.ReceiptNbr
        });
      Decimal toDistribute = -ppvAmt.Value;
      PurchasePriceVarianceAllocationService.Instance.AllocateRestOverRCTLines((IList<AllocationServiceBase.POReceiptLineAdjustment>) this.PPVTransactions, toDistribute - PurchasePriceVarianceAllocationService.Instance.AllocateOverRCTLine((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, this.PPVTransactions, aLine, toDistribute, tran.BranchID));
      if (this.PPVLines.Contains(tran))
        return;
      this.PPVLines.Add(tran);
    }
    else
    {
      PX.Objects.GL.GLTran copy1 = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) patternTran);
      PX.Objects.GL.GLTran copy2 = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) patternTran);
      int? aPPVAcctID = new int?();
      int? aPPVSubID = new int?();
      if (((PXGraphExtension<APReleaseProcess>) this).Base.posetup.PPVAllocationMode == "I" && POLineType.IsNonStock(rctLine.LineType) && rctLine.POType != "PD")
      {
        aPPVAcctID = rctLine.ExpenseAcctID;
        aPPVSubID = rctLine.ExpenseSubID;
      }
      else
        ((PXGraphExtension<APReleaseProcess>) this).Base.RetrievePPVAccount((PXGraph) je, rctLine, ref aPPVAcctID, ref aPPVSubID);
      Decimal curyval;
      PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(((PXSelectBase) je.currencyinfo).Cache, (object) curyInfo, ppvAmt.Value, out curyval);
      copy2.AccountID = aPPVAcctID;
      copy2.SubID = aPPVSubID;
      copy1.CuryDebitAmt = new Decimal?(curyval);
      copy1.CuryCreditAmt = new Decimal?(0M);
      copy1.DebitAmt = ppvAmt;
      copy1.CreditAmt = new Decimal?(0M);
      copy1.Qty = new Decimal?(0M);
      copy2.CuryDebitAmt = new Decimal?(0M);
      copy2.CuryCreditAmt = new Decimal?(curyval);
      copy2.DebitAmt = new Decimal?(0M);
      copy2.CreditAmt = ppvAmt;
      copy2.Qty = new Decimal?(0M);
      this.SetProjectForPPVTransaction(copy2, tran, rctLine);
      ((PXGraphExtension<APReleaseProcess>) this).Base.InsertInvoiceDetailsPOReceiptLineTransaction(je, copy1, new APReleaseProcess.GLTranInsertionContext()
      {
        APTranRecord = tran,
        POReceiptLineRecord = rctLine
      });
      ((PXGraphExtension<APReleaseProcess>) this).Base.InsertInvoiceDetailsPOReceiptLineTransaction(je, copy2, new APReleaseProcess.GLTranInsertionContext()
      {
        APTranRecord = tran,
        POReceiptLineRecord = rctLine
      });
    }
  }

  protected virtual void SetProjectForPPVTransaction(
    PX.Objects.GL.GLTran ppvTran,
    PX.Objects.AP.APTran tran,
    PX.Objects.PO.POReceiptLine rctLine)
  {
  }

  protected virtual bool IsInventoryPPV(PX.Objects.PO.POReceiptLine rctLine)
  {
    bool flag = false;
    if (rctLine.ReceiptType != "RN" && ((PXGraphExtension<APReleaseProcess>) this).Base.posetup.PPVAllocationMode == "I" && rctLine.POType != "PD")
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[1]
      {
        (object) rctLine.InventoryID
      }));
      flag = (inventoryItem == null ? 0 : (inventoryItem.ValMethod != "T" ? 1 : 0)) != 0 && !POLineType.IsNonStock(rctLine.LineType);
    }
    return flag;
  }

  protected virtual void UnlinkRelatedLandedCosts(PX.Objects.AP.APRegister doc, PX.Objects.AP.APTran tran)
  {
    if (this.GetLCOriginalBillLine(doc, tran) == null)
      return;
    POLandedCostDetail landedCostDetail = this.GetLandedCostDetail(tran.LCDocType, tran.LCRefNbr, tran.LCLineNbr.Value);
    landedCostDetail.APDocType = (string) null;
    landedCostDetail.APRefNbr = (string) null;
    GraphHelper.MarkUpdated(((PXSelectBase) this.landedCostDetails).Cache, (object) landedCostDetail, true);
  }

  protected virtual PX.Objects.AP.APTran GetLCOriginalBillLine(PX.Objects.AP.APRegister doc, PX.Objects.AP.APTran tran)
  {
    if (doc.DocType != "ADR" || string.IsNullOrEmpty(doc.OrigRefNbr) || string.IsNullOrEmpty(tran.LCDocType) || string.IsNullOrEmpty(tran.LCRefNbr) || !tran.LCLineNbr.HasValue)
      return (PX.Objects.AP.APTran) null;
    return GraphHelper.RowCast<PX.Objects.AP.APTran>((IEnumerable) PXSelectBase<PX.Objects.AP.APTran, PXSelectReadonly<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<PX.Objects.AP.APTran.lCRefNbr, IsNotNull>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[2]
    {
      (object) doc.OrigDocType,
      (object) doc.OrigRefNbr
    })).FirstOrDefault<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, bool>) (ot =>
    {
      if (!(ot.LCDocType == tran.LCDocType) || !(ot.LCRefNbr == tran.LCRefNbr))
        return false;
      int? lcLineNbr1 = ot.LCLineNbr;
      int? lcLineNbr2 = tran.LCLineNbr;
      return lcLineNbr1.GetValueOrDefault() == lcLineNbr2.GetValueOrDefault() & lcLineNbr1.HasValue == lcLineNbr2.HasValue;
    }));
  }

  protected virtual POLandedCostDetail GetLandedCostDetail(
    string docType,
    string refNbr,
    int lineNbr)
  {
    return PXResultset<POLandedCostDetail>.op_Implicit(PXSelectBase<POLandedCostDetail, PXSelect<POLandedCostDetail, Where<POLandedCostDetail.docType, Equal<Required<POLandedCostDetail.docType>>, And<POLandedCostDetail.refNbr, Equal<Required<POLandedCostDetail.refNbr>>, And<POLandedCostDetail.lineNbr, Equal<Required<POLandedCostDetail.lineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[3]
    {
      (object) docType,
      (object) refNbr,
      (object) lineNbr
    }));
  }

  public virtual Decimal GetLandedCostAmount(POLandedCostDetail landedCostDetail)
  {
    POLandedCostTax poLandedCostTax = GraphHelper.RowCast<POLandedCostTax>((IEnumerable) ((IQueryable<PXResult<POLandedCostTax>>) ((PXSelectBase<POLandedCostTax>) new PXSelectJoin<POLandedCostTax, InnerJoin<PX.Objects.TX.Tax, On<POLandedCostTax.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<POLandedCostTax.docType, Equal<Required<POLandedCostTax.docType>>, And<POLandedCostTax.refNbr, Equal<Required<POLandedCostTax.refNbr>>>>>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base)).Select(new object[2]
    {
      (object) landedCostDetail.DocType,
      (object) landedCostDetail.RefNbr
    })).Select<PXResult<POLandedCostTax>, PXResult<POLandedCostTax, PX.Objects.TX.Tax>>((Expression<Func<PXResult<POLandedCostTax>, PXResult<POLandedCostTax, PX.Objects.TX.Tax>>>) (t => (PXResult<POLandedCostTax, PX.Objects.TX.Tax>) t)).ToList<PXResult<POLandedCostTax, PX.Objects.TX.Tax>>().Where<PXResult<POLandedCostTax, PX.Objects.TX.Tax>>((Func<PXResult<POLandedCostTax, PX.Objects.TX.Tax>, bool>) (t => PXResult<POLandedCostTax, PX.Objects.TX.Tax>.op_Implicit(t).TaxCalcLevel == "0" && PXResult<POLandedCostTax, PX.Objects.TX.Tax>.op_Implicit(t).TaxType != "W" && !PXResult<POLandedCostTax, PX.Objects.TX.Tax>.op_Implicit(t).ReverseTax.GetValueOrDefault()))).FirstOrDefault<POLandedCostTax>((Func<POLandedCostTax, bool>) (t =>
    {
      int? lineNbr1 = t.LineNbr;
      int? lineNbr2 = landedCostDetail.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    }));
    return poLandedCostTax != null ? poLandedCostTax.TaxableAmt.GetValueOrDefault() : landedCostDetail.LineAmt.GetValueOrDefault();
  }

  protected virtual void ApplyLandedCostVariance(
    bool isPrebooking,
    bool isPrebookingVoiding,
    JournalEntry je,
    PX.Objects.GL.GLTran patternTran,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo,
    PX.Objects.AP.APTran n,
    LandedCostCode lcCode,
    ARReleaseProcess.Amount postedAmount)
  {
    if (string.IsNullOrEmpty(n.LCDocType) || string.IsNullOrEmpty(n.LCRefNbr))
      return;
    int? lcLineNbr = n.LCLineNbr;
    if (!lcLineNbr.HasValue || isPrebooking || isPrebookingVoiding)
      return;
    bool flag = this.GetLCOriginalBillLine(PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[2]
    {
      (object) n.TranType,
      (object) n.RefNbr
    })), n) != null;
    Decimal num1 = n.DrCr == "D" | flag ? 1M : -1M;
    string lcDocType = n.LCDocType;
    string lcRefNbr = n.LCRefNbr;
    lcLineNbr = n.LCLineNbr;
    int lineNbr = lcLineNbr.Value;
    Decimal landedCostAmount = this.GetLandedCostAmount(this.GetLandedCostDetail(lcDocType, lcRefNbr, lineNbr));
    Decimal num2 = num1 * landedCostAmount;
    Decimal? nullable = postedAmount.Base;
    Decimal num3 = num2;
    if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
      return;
    Decimal num4 = num2;
    nullable = postedAmount.Base;
    Decimal valueOrDefault = nullable.GetValueOrDefault();
    Decimal baseval = num4 - valueOrDefault;
    Decimal curyval;
    PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(((PXSelectBase) je.currencyinfo).Cache, (object) curyInfo, baseval, out curyval);
    if (!(baseval != 0M) && !(curyval != 0M))
      return;
    PX.Objects.GL.GLTran copy1 = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) patternTran);
    PX.Objects.GL.GLTran copy2 = (PX.Objects.GL.GLTran) ((PXSelectBase) je.GLTranModuleBatNbr).Cache.CreateCopy((object) patternTran);
    copy1.TranDesc = "Landed Cost Accrual correction";
    copy1.CuryDebitAmt = new Decimal?(n.DrCr == "D" ? curyval : 0M);
    copy1.DebitAmt = new Decimal?(n.DrCr == "D" ? baseval : 0M);
    copy1.CuryCreditAmt = new Decimal?(n.DrCr == "D" ? 0M : curyval);
    copy1.CreditAmt = new Decimal?(n.DrCr == "D" ? 0M : baseval);
    copy2.TranDesc = "Landed Cost Variance";
    copy2.CuryDebitAmt = new Decimal?(n.DrCr == "D" ? 0M : curyval);
    copy2.DebitAmt = new Decimal?(n.DrCr == "D" ? 0M : baseval);
    copy2.CuryCreditAmt = new Decimal?(n.DrCr == "D" ? curyval : 0M);
    copy2.CreditAmt = new Decimal?(n.DrCr == "D" ? baseval : 0M);
    copy2.AccountID = lcCode.LCVarianceAcct;
    copy2.SubID = lcCode.LCVarianceSub;
    ((PXGraphExtension<APReleaseProcess>) this).Base.InsertInvoiceDetailsTransaction(je, copy1, new APReleaseProcess.GLTranInsertionContext()
    {
      APTranRecord = n
    });
    ((PXGraphExtension<APReleaseProcess>) this).Base.InsertInvoiceDetailsTransaction(je, copy2, new APReleaseProcess.GLTranInsertionContext()
    {
      APTranRecord = n
    });
  }

  public virtual PX.Objects.PO.POLine UpdatePOLine(
    PX.Objects.AP.APTran n,
    PX.Objects.AP.APInvoice apdoc,
    PX.Objects.PO.POOrder srcDoc,
    PX.Objects.PO.POLine updLine,
    bool isPrebooking)
  {
    if (((((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck ? 1 : (((PXGraphExtension<APReleaseProcess>) this).Base.IsInvoiceReclassification ? 1 : 0)) | (isPrebooking ? 1 : 0)) != 0 || n.TranType != "PPM" && updLine.POAccrualType == null)
      return updLine;
    updLine = (PX.Objects.PO.POLine) ((PXSelectBase) this.poOrderLineUPD).Cache.CreateCopy((object) updLine);
    Decimal num1 = n.Qty.GetValueOrDefault();
    if (n.InventoryID.HasValue && !string.IsNullOrEmpty(n.UOM) && !string.IsNullOrEmpty(updLine.UOM) && !string.Equals(n.UOM, updLine.UOM, StringComparison.OrdinalIgnoreCase))
      num1 = INUnitAttribute.ConvertFromBase(((PXSelectBase) this.poOrderLineUPD).Cache, n.InventoryID, updLine.UOM, n.BaseQty.GetValueOrDefault(), INPrecision.QUANTITY);
    Decimal? nullable1 = n.CuryTranAmt;
    Decimal? curyRetainageAmt1 = n.CuryRetainageAmt;
    Decimal valueOrDefault1 = (nullable1.HasValue & curyRetainageAmt1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyRetainageAmt1.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
    Decimal? nullable2;
    if (apdoc.CuryID != srcDoc.CuryID)
    {
      PXCache cache = ((PXSelectBase) this.poOrderLineUPD).Cache;
      PX.Objects.PO.POLine row = updLine;
      nullable1 = n.TranAmt;
      nullable2 = n.RetainageAmt;
      Decimal valueOrDefault2 = (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      ref Decimal local = ref valueOrDefault1;
      PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(cache, (object) row, valueOrDefault2, out local);
    }
    if (n.TranType == "PPM")
    {
      PX.Objects.PO.POLine poLine1 = updLine;
      nullable1 = poLine1.ReqPrepaidQty;
      Decimal num2 = num1;
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num2);
      poLine1.ReqPrepaidQty = nullable3;
      PX.Objects.PO.POLine poLine2 = updLine;
      nullable1 = poLine2.CuryReqPrepaidAmt;
      Decimal num3 = valueOrDefault1;
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num3);
      poLine2.CuryReqPrepaidAmt = nullable4;
      updLine = ((PXSelectBase<PX.Objects.PO.POLine>) this.poOrderLineUPD).Update(updLine);
      int num4;
      if (POLineType.IsNonStock(updLine.LineType))
      {
        nullable2 = updLine.CuryExtCost;
        Decimal? curyRetainageAmt2 = updLine.CuryRetainageAmt;
        nullable1 = nullable2.HasValue & curyRetainageAmt2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyRetainageAmt2.GetValueOrDefault()) : new Decimal?();
        Decimal num5 = 0M;
        num4 = nullable1.GetValueOrDefault() < num5 & nullable1.HasValue ? 1 : 0;
      }
      else
        num4 = 0;
      bool flag = num4 != 0;
      Decimal? nullable5;
      Decimal? nullable6;
      if (!flag)
      {
        nullable1 = updLine.CuryReqPrepaidAmt;
        nullable2 = updLine.CuryExtCost;
        nullable5 = updLine.CuryRetainageAmt;
        nullable6 = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        if (nullable1.GetValueOrDefault() > nullable6.GetValueOrDefault() & nullable1.HasValue & nullable6.HasValue)
          goto label_20;
      }
      if (flag)
      {
        nullable6 = updLine.CuryReqPrepaidAmt;
        nullable5 = updLine.CuryExtCost;
        nullable2 = updLine.CuryRetainageAmt;
        nullable1 = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        if (!(nullable6.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable6.HasValue & nullable1.HasValue))
          goto label_77;
      }
      else
        goto label_77;
label_20:
      string str = !flag ? "The prepayment cannot be released because the sum of amounts in the released prepayments and current prepayment for purchase order line ({0}, {1}, {2}) exceeds the order amount {3}." : "The prepayment cannot be released because the sum of the amounts in the released prepayments and current prepayment for purchase order line ({0}, {1}, {2}) is less than the negative amount in order line {3}.";
      object[] objArray = new object[4]
      {
        (object) updLine.OrderNbr,
        (object) updLine.LineNbr,
        ((PXSelectBase) ((PXGraphExtension<APReleaseProcess>) this).Base.APTran_TranType_RefNbr).Cache.GetValueExt<PX.Objects.AP.APTran.inventoryID>((object) n),
        null
      };
      nullable1 = updLine.CuryExtCost;
      nullable6 = updLine.CuryRetainageAmt;
      Decimal? nullable7;
      if (!(nullable1.HasValue & nullable6.HasValue))
      {
        nullable2 = new Decimal?();
        nullable7 = nullable2;
      }
      else
        nullable7 = new Decimal?(nullable1.GetValueOrDefault() + nullable6.GetValueOrDefault());
      objArray[3] = (object) nullable7;
      throw new PXException(str, objArray);
    }
    PX.Objects.PO.POLine poLine3 = updLine;
    Decimal? billedQty = poLine3.BilledQty;
    Decimal num6 = n.Sign * num1;
    Decimal? nullable8;
    if (!billedQty.HasValue)
    {
      nullable1 = new Decimal?();
      nullable8 = nullable1;
    }
    else
      nullable8 = new Decimal?(billedQty.GetValueOrDefault() + num6);
    poLine3.BilledQty = nullable8;
    PX.Objects.PO.POLine poLine4 = updLine;
    Decimal? curyBilledAmt = poLine4.CuryBilledAmt;
    Decimal num7 = n.Sign * valueOrDefault1;
    Decimal? nullable9;
    if (!curyBilledAmt.HasValue)
    {
      nullable1 = new Decimal?();
      nullable9 = nullable1;
    }
    else
      nullable9 = new Decimal?(curyBilledAmt.GetValueOrDefault() + num7);
    poLine4.CuryBilledAmt = nullable9;
    POAccrualRecord accrualStatusSummary = this.GetAccrualStatusSummary(updLine);
    bool flag1 = updLine.CompletePOLine == "Q";
    int num8 = POLineType.IsService(updLine.LineType) ? 0 : (POOrderEntry.NeedsPOReceipt(updLine, false, ((PXGraphExtension<APReleaseProcess>) this).Base.posetup) ? 1 : 0);
    Decimal? nullable10;
    int num9;
    if (accrualStatusSummary.BilledUOM == null || !(accrualStatusSummary.BilledUOM == accrualStatusSummary.ReceivedUOM))
    {
      nullable10 = accrualStatusSummary.BaseBilledQty;
      nullable1 = accrualStatusSummary.BaseReceivedQty;
      num9 = nullable10.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable10.HasValue == nullable1.HasValue ? 1 : 0;
    }
    else
    {
      nullable1 = accrualStatusSummary.BilledQty;
      Decimal? receivedQty = accrualStatusSummary.ReceivedQty;
      num9 = nullable1.GetValueOrDefault() == receivedQty.GetValueOrDefault() & nullable1.HasValue == receivedQty.HasValue ? 1 : 0;
    }
    bool flag2 = num9 != 0;
    bool flag3 = false;
    bool? nullable11;
    if (num8 != 0)
    {
      if (flag2)
      {
        nullable11 = updLine.Completed;
        if (!nullable11.GetValueOrDefault() && flag1)
          goto label_64;
      }
      else
        goto label_64;
    }
    bool flag4;
    if (flag1)
    {
      nullable11 = updLine.Completed;
      if (nullable11.GetValueOrDefault() && (n.Sign > 0M || !POLineType.IsService(updLine.LineType) && !POLineType.IsProjectDropShip(updLine.LineType)))
      {
        flag4 = true;
      }
      else
      {
        int num10;
        if (accrualStatusSummary.BilledUOM == null || !(accrualStatusSummary.BilledUOM == updLine.UOM))
        {
          nullable2 = updLine.BaseOrderQty;
          Decimal? rcptQtyThreshold = updLine.RcptQtyThreshold;
          nullable10 = nullable2.HasValue & rcptQtyThreshold.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * rcptQtyThreshold.GetValueOrDefault() / 100M) : new Decimal?();
          nullable1 = accrualStatusSummary.BaseBilledQty;
          num10 = nullable10.GetValueOrDefault() <= nullable1.GetValueOrDefault() & nullable10.HasValue & nullable1.HasValue ? 1 : 0;
        }
        else
        {
          Decimal? orderQty = updLine.OrderQty;
          nullable2 = updLine.RcptQtyThreshold;
          nullable1 = orderQty.HasValue & nullable2.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * nullable2.GetValueOrDefault() / 100M) : new Decimal?();
          nullable10 = accrualStatusSummary.BilledQty;
          num10 = nullable1.GetValueOrDefault() <= nullable10.GetValueOrDefault() & nullable1.HasValue & nullable10.HasValue ? 1 : 0;
        }
        flag4 = num10 != 0;
      }
    }
    else if (accrualStatusSummary.BillCuryID != null && accrualStatusSummary.BillCuryID == srcDoc.CuryID)
    {
      nullable2 = updLine.CuryExtCost;
      Decimal? curyRetainageAmt3 = updLine.CuryRetainageAmt;
      nullable10 = nullable2.HasValue & curyRetainageAmt3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyRetainageAmt3.GetValueOrDefault()) : new Decimal?();
      nullable1 = updLine.RcptQtyThreshold;
      Decimal? nullable12 = nullable10.HasValue & nullable1.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
      int num11;
      if (nullable12.HasValue)
      {
        nullable1 = accrualStatusSummary.CuryBilledAmt;
        if (nullable1.HasValue)
        {
          if (Math.Sign(nullable12.Value) == 0)
          {
            nullable1 = accrualStatusSummary.CuryBilledAmt;
            if (Math.Sign(nullable1.Value) > 0)
              goto label_49;
          }
          int num12 = Math.Sign(nullable12.Value);
          nullable1 = accrualStatusSummary.CuryBilledAmt;
          int num13 = Math.Sign(nullable1.Value);
          if (num12 != num13)
            goto label_50;
label_49:
          Decimal num14 = Math.Abs(nullable12.Value);
          nullable1 = accrualStatusSummary.CuryBilledAmt;
          Decimal num15 = Math.Abs(nullable1.Value);
          num11 = num14 <= num15 ? 1 : 0;
          goto label_51;
        }
      }
label_50:
      num11 = 0;
label_51:
      flag4 = num11 != 0;
    }
    else
    {
      Decimal? extCost = updLine.ExtCost;
      nullable2 = updLine.RetainageAmt;
      nullable1 = extCost.HasValue & nullable2.HasValue ? new Decimal?(extCost.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      nullable10 = updLine.RcptQtyThreshold;
      Decimal? nullable13;
      if (!(nullable1.HasValue & nullable10.HasValue))
      {
        nullable2 = new Decimal?();
        nullable13 = nullable2;
      }
      else
        nullable13 = new Decimal?(nullable1.GetValueOrDefault() * nullable10.GetValueOrDefault() / 100M);
      Decimal? nullable14 = nullable13;
      int num16;
      if (nullable14.HasValue)
      {
        nullable10 = accrualStatusSummary.BilledAmt;
        if (nullable10.HasValue)
        {
          if (Math.Sign(nullable14.Value) == 0)
          {
            nullable10 = accrualStatusSummary.BilledAmt;
            if (Math.Sign(nullable10.Value) > 0)
              goto label_60;
          }
          int num17 = Math.Sign(nullable14.Value);
          nullable10 = accrualStatusSummary.BilledAmt;
          int num18 = Math.Sign(nullable10.Value);
          if (num17 != num18)
            goto label_61;
label_60:
          Decimal num19 = Math.Abs(nullable14.Value);
          nullable10 = accrualStatusSummary.BilledAmt;
          Decimal num20 = Math.Abs(nullable10.Value);
          num16 = num19 <= num20 ? 1 : 0;
          goto label_62;
        }
      }
label_61:
      num16 = 0;
label_62:
      flag4 = num16 != 0;
    }
    flag3 = flag4;
label_64:
    if (!flag3)
    {
      if (!flag3)
      {
        nullable11 = updLine.Closed;
        if (!nullable11.GetValueOrDefault() || !(n.Sign < 0M))
          goto label_71;
      }
      else
        goto label_71;
    }
    updLine.Closed = new bool?(flag3);
    if (flag3)
      updLine.Completed = new bool?(true);
    else if (updLine.OrderType == "RS")
      updLine.Completed = new bool?(false);
label_71:
    updLine = ((PXSelectBase<PX.Objects.PO.POLine>) this.poOrderLineUPD).Update(updLine);
    if (updLine.POAccrualType == "O" && updLine.RcptQtyAction == "R")
    {
      Decimal? nullable15;
      ref Decimal? local = ref nullable15;
      nullable10 = updLine.OrderQty;
      nullable1 = updLine.RcptQtyMax;
      Decimal? nullable16;
      if (!(nullable10.HasValue & nullable1.HasValue))
      {
        nullable2 = new Decimal?();
        nullable16 = nullable2;
      }
      else
        nullable16 = new Decimal?(nullable10.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M);
      Decimal num21 = PXDBQuantityAttribute.Round(nullable16);
      local = new Decimal?(num21);
      nullable1 = updLine.BilledQty;
      nullable10 = nullable15;
      if (nullable1.GetValueOrDefault() > nullable10.GetValueOrDefault() & nullable1.HasValue & nullable10.HasValue)
        throw new PXException("Cannot release the bill because the billed quantity of the related line in the {0} purchase order cannot exceed {1}. Correct the {2} line of the purchase order or remove the {3} line from the bill.", new object[4]
        {
          (object) updLine.OrderNbr,
          (object) nullable15,
          (object) updLine.LineNbr,
          (object) n.LineNbr
        });
    }
label_77:
    return updLine;
  }

  protected virtual void ReversePOAccrual(POAccrualSplit split)
  {
    split.IsReversed = new bool?(true);
    split = ((PXSelectBase<POAccrualSplit>) this.poAccrualSplitUpdate).Update(split);
  }

  protected virtual void ReverseOrigAPBillPOAccrual(PX.Objects.AP.APInvoice apdoc, PX.Objects.AP.APTran tran)
  {
    switch (tran.POAccrualType)
    {
      case "O":
        POAccrualDetail poAccrualDetail = PXResultset<POAccrualDetail>.op_Implicit(PXSelectBase<POAccrualDetail, PXViewOf<POAccrualDetail>.BasedOn<SelectFromBase<POAccrualDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.aPDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<POAccrualDetail.aPRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<POAccrualDetail.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, 0, 1, new object[3]
        {
          (object) apdoc.OrigDocType,
          (object) apdoc.OrigRefNbr,
          (object) tran.OrigLineNbr
        }));
        if (poAccrualDetail == null)
          break;
        poAccrualDetail.IsReversed = new bool?(true);
        poAccrualDetail.ReversedFinPeriodID = apdoc.FinPeriodID;
        ((PXSelectBase<POAccrualDetail>) this.poAccrualDetailUpdate).Update(poAccrualDetail);
        break;
      case "R":
        PX.Objects.AP.APTran apTran = PX.Objects.AP.APTran.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, apdoc.OrigDocType, apdoc.OrigRefNbr, tran.OrigLineNbr);
        Decimal? nullable = tran.Qty;
        Decimal? qty = (Decimal?) apTran?.Qty;
        if (!(nullable.GetValueOrDefault() == qty.GetValueOrDefault() & nullable.HasValue == qty.HasValue) || !string.Equals(tran.UOM, apTran?.UOM, StringComparison.OrdinalIgnoreCase))
          break;
        Decimal? curyTranAmt = tran.CuryTranAmt;
        nullable = (Decimal?) apTran?.CuryTranAmt;
        if (!(curyTranAmt.GetValueOrDefault() == nullable.GetValueOrDefault() & curyTranAmt.HasValue == nullable.HasValue))
          break;
        goto case "O";
    }
  }

  [PXOverride]
  public virtual void ProcessPayment(
    JournalEntry je,
    PX.Objects.AP.APRegister doc,
    PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AP.Vendor, PX.Objects.CA.CashAccount> res,
    Action<JournalEntry, PX.Objects.AP.APRegister, PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AP.Vendor, PX.Objects.CA.CashAccount>> baseImpl)
  {
    baseImpl(je, doc, res);
    this.ReleasePOAdjustments(PXResult<PX.Objects.AP.APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, PX.Objects.AP.Vendor, PX.Objects.CA.CashAccount>.op_Implicit(res));
  }

  protected virtual void ReleasePOAdjustments(PX.Objects.AP.APPayment payment)
  {
    if (payment.DocType == "PPM")
    {
      PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>> poAdjustments = this.poAdjustments;
      object[] objArray = new object[2]
      {
        (object) payment.DocType,
        (object) payment.RefNbr
      };
      foreach (POAdjust poadjustment in ((PXSelectBase<POAdjust>) poAdjustments).SelectMain(objArray))
        this.ReleasePOAdjustment(payment, poadjustment);
    }
    else
    {
      if (!(payment.DocType == "VCK"))
        return;
      PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>> poAdjustments1 = this.poAdjustments;
      object[] objArray1 = new object[2]
      {
        (object) payment.OrigDocType,
        (object) payment.OrigRefNbr
      };
      foreach (POAdjust poadjustment in ((PXSelectBase<POAdjust>) poAdjustments1).SelectMain(objArray1))
        this.VoidPOAdjustment(payment, poadjustment);
      PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>>>> poAdjustments2 = this.poAdjustments;
      object[] objArray2 = new object[2]
      {
        (object) payment.DocType,
        (object) payment.RefNbr
      };
      foreach (POAdjust poAdjust in ((PXSelectBase<POAdjust>) poAdjustments2).SelectMain(objArray2))
      {
        if (!poAdjust.Voided.GetValueOrDefault())
        {
          poAdjust.Released = new bool?(true);
          poAdjust.Voided = new bool?(true);
          GraphHelper.MarkUpdated(((PXSelectBase) this.poAdjustments).Cache, (object) poAdjust, true);
        }
      }
    }
  }

  protected virtual void ReleasePOAdjustment(PX.Objects.AP.APPayment payment, POAdjust poadjustment)
  {
    if (!poadjustment.IsRequest.GetValueOrDefault() && !poadjustment.Released.GetValueOrDefault())
    {
      POOrderPrepayment poOrderPrepayment1 = ((PXSelectBase<POOrderPrepayment>) new PXSelect<POOrderPrepayment, Where<POOrderPrepayment.orderType, Equal<Required<POOrderPrepayment.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Required<POOrderPrepayment.orderNbr>>, And<POOrderPrepayment.aPDocType, Equal<Required<POOrderPrepayment.aPDocType>>, And<POOrderPrepayment.aPRefNbr, Equal<Required<POOrderPrepayment.aPRefNbr>>>>>>>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base)).SelectSingle(new object[4]
      {
        (object) poadjustment.AdjdOrderType,
        (object) poadjustment.AdjdOrderNbr,
        (object) payment.DocType,
        (object) payment.RefNbr
      });
      if (poOrderPrepayment1 == null)
        poOrderPrepayment1 = ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Insert(new POOrderPrepayment()
        {
          OrderType = poadjustment.AdjdOrderType,
          OrderNbr = poadjustment.AdjdOrderNbr,
          APDocType = payment.DocType,
          APRefNbr = payment.RefNbr,
          IsRequest = new bool?(false),
          CuryAppliedAmt = new Decimal?(0M)
        });
      POOrderPrepayment poOrderPrepayment2 = poOrderPrepayment1;
      Decimal? curyAppliedAmt = poOrderPrepayment2.CuryAppliedAmt;
      Decimal? nullable1 = poadjustment.CuryAdjdAmt;
      poOrderPrepayment2.CuryAppliedAmt = curyAppliedAmt.HasValue & nullable1.HasValue ? new Decimal?(curyAppliedAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      poOrderPrepayment1.PayDocType = payment.DocType;
      poOrderPrepayment1.PayRefNbr = payment.RefNbr;
      ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Update(poOrderPrepayment1);
      PX.Objects.PO.POOrder copy = PXCache<PX.Objects.PO.POOrder>.CreateCopy(PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Select(new object[2]
      {
        (object) poadjustment.AdjdOrderType,
        (object) poadjustment.AdjdOrderNbr
      })) ?? throw new RowNotFoundException(((PXSelectBase) this.poOrderUPD).Cache, new object[2]
      {
        (object) poadjustment.AdjdOrderType,
        (object) poadjustment.AdjdOrderNbr
      }));
      PX.Objects.PO.POOrder poOrder1 = copy;
      nullable1 = poOrder1.CuryPrepaidTotal;
      Decimal? nullable2 = poadjustment.CuryAdjdAmt;
      poOrder1.CuryPrepaidTotal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      PX.Objects.PO.POOrder poOrder2 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Update(copy);
      nullable2 = poOrder2.CuryUnbilledLineTotal;
      nullable1 = poOrder2.CuryLineTotal;
      Decimal? nullable3 = nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue ? poOrder2.CuryOrderTotal : poOrder2.CuryUnbilledOrderTotal;
      nullable1 = poOrder2.CuryPrepaidTotal;
      nullable2 = nullable3;
      if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        throw new PXException("The value of the Unbilled Prepayment Total box of the {0} purchase order cannot exceed its unbilled or total amount.", new object[1]
        {
          (object) poOrder2.OrderNbr
        });
    }
    poadjustment.Released = new bool?(true);
    ((PXSelectBase<POAdjust>) this.poAdjustments).Update(poadjustment);
  }

  protected virtual void VoidPOAdjustment(PX.Objects.AP.APPayment payment, POAdjust poadjustment)
  {
    if (!poadjustment.IsRequest.GetValueOrDefault() && poadjustment.Released.GetValueOrDefault() && !poadjustment.Voided.GetValueOrDefault())
    {
      POOrderPrepayment poOrderPrepayment1 = ((PXSelectBase<POOrderPrepayment>) new PXSelect<POOrderPrepayment, Where<POOrderPrepayment.orderType, Equal<Required<POOrderPrepayment.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Required<POOrderPrepayment.orderNbr>>, And<POOrderPrepayment.aPDocType, Equal<Required<POOrderPrepayment.aPDocType>>, And<POOrderPrepayment.aPRefNbr, Equal<Required<POOrderPrepayment.aPRefNbr>>>>>>>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base)).SelectSingle(new object[4]
      {
        (object) poadjustment.AdjdOrderType,
        (object) poadjustment.AdjdOrderNbr,
        (object) payment.OrigDocType,
        (object) payment.OrigRefNbr
      });
      POOrderPrepayment poOrderPrepayment2 = poOrderPrepayment1 != null ? poOrderPrepayment1 : throw new RowNotFoundException(((PXSelectBase) this.poOrderPrepUpd).Cache, new object[4]
      {
        (object) poadjustment.AdjdOrderType,
        (object) poadjustment.AdjdOrderNbr,
        (object) payment.OrigDocType,
        (object) payment.OrigRefNbr
      });
      Decimal? curyAppliedAmt = poOrderPrepayment2.CuryAppliedAmt;
      Decimal? nullable = poadjustment.CuryAdjdAmt;
      poOrderPrepayment2.CuryAppliedAmt = curyAppliedAmt.HasValue & nullable.HasValue ? new Decimal?(curyAppliedAmt.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Update(poOrderPrepayment1);
      PX.Objects.PO.POOrder poOrder1 = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Select(new object[2]
      {
        (object) poadjustment.AdjdOrderType,
        (object) poadjustment.AdjdOrderNbr
      }));
      PX.Objects.PO.POOrder poOrder2 = poOrder1 != null ? PXCache<PX.Objects.PO.POOrder>.CreateCopy(poOrder1) : throw new RowNotFoundException(((PXSelectBase) this.poOrderUPD).Cache, new object[2]
      {
        (object) poadjustment.AdjdOrderType,
        (object) poadjustment.AdjdOrderNbr
      });
      Decimal valueOrDefault1;
      if (poOrder1.CuryID == payment.CuryID)
      {
        nullable = poadjustment.CuryAdjgAmt;
        valueOrDefault1 = nullable.GetValueOrDefault();
      }
      else
      {
        PXCache cache = ((PXSelectBase) this.poOrderUPD).Cache;
        PX.Objects.PO.POOrder row = poOrder2;
        nullable = poadjustment.AdjgAmt;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        ref Decimal local = ref valueOrDefault1;
        PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(cache, (object) row, valueOrDefault2, out local);
      }
      PX.Objects.PO.POOrder poOrder3 = poOrder2;
      nullable = poOrder3.CuryPrepaidTotal;
      Decimal num = valueOrDefault1;
      poOrder3.CuryPrepaidTotal = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num) : new Decimal?();
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Update(poOrder2);
    }
    if (poadjustment.Voided.GetValueOrDefault())
      return;
    poadjustment.Voided = new bool?(true);
    GraphHelper.MarkUpdated(((PXSelectBase) this.poAdjustments).Cache, (object) poadjustment, true);
  }

  protected virtual void UpdatePOOrderPrepaymentOnRelease(PX.Objects.AP.APInvoice apdoc, bool isPrebooking)
  {
    if (((((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck ? 1 : (((PXGraphExtension<APReleaseProcess>) this).Base.IsInvoiceReclassification ? 1 : 0)) | (isPrebooking ? 1 : 0)) != 0 || apdoc.DocType != "PPM" || apdoc.OrigModule != "PO")
      return;
    POOrderPrepayment poOrderPrepayment1 = PXResultset<POOrderPrepayment>.op_Implicit(((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Select(new object[2]
    {
      (object) apdoc.DocType,
      (object) apdoc.RefNbr
    }));
    if (poOrderPrepayment1 == null)
      return;
    POOrderPrepayment copy1 = PXCache<POOrderPrepayment>.CreateCopy(poOrderPrepayment1);
    copy1.CuryAppliedAmt = apdoc.CuryOrigDocAmt;
    POOrderPrepayment poOrderPrepayment2 = ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Update(copy1);
    PX.Objects.PO.POOrder copy2 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Select(new object[2]
    {
      (object) poOrderPrepayment2.OrderType,
      (object) poOrderPrepayment2.OrderNbr
    })));
    PX.Objects.PO.POOrder poOrder1 = copy2;
    Decimal? curyPrepaidTotal1 = poOrder1.CuryPrepaidTotal;
    Decimal? nullable1 = apdoc.CuryDocBal;
    poOrder1.CuryPrepaidTotal = curyPrepaidTotal1.HasValue & nullable1.HasValue ? new Decimal?(curyPrepaidTotal1.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    PX.Objects.PO.POOrder poOrder2 = ((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Update(copy2);
    nullable1 = poOrder2.CuryUnbilledLineTotal;
    Decimal? curyLineTotal = poOrder2.CuryLineTotal;
    Decimal? nullable2 = nullable1.GetValueOrDefault() == curyLineTotal.GetValueOrDefault() & nullable1.HasValue == curyLineTotal.HasValue ? poOrder2.CuryOrderTotal : poOrder2.CuryUnbilledOrderTotal;
    Decimal? curyPrepaidTotal2 = poOrder2.CuryPrepaidTotal;
    nullable1 = nullable2;
    if (curyPrepaidTotal2.GetValueOrDefault() > nullable1.GetValueOrDefault() & curyPrepaidTotal2.HasValue & nullable1.HasValue)
      throw new PXException("The value of the Unbilled Prepayment Total box of the {0} purchase order cannot exceed its unbilled or total amount.", new object[1]
      {
        (object) poOrder2.OrderNbr
      });
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AP.APReleaseProcess.ProcessPrepaymentRequestAppliedToCheck(PX.Objects.AP.APRegister,PX.Objects.AP.APAdjust)" />
  /// </summary>
  [PXOverride]
  public virtual void ProcessPrepaymentRequestAppliedToCheck(
    PX.Objects.AP.APRegister prepaymentRequest,
    APAdjust prepaymentAdj,
    Action<PX.Objects.AP.APRegister, APAdjust> baseImpl)
  {
    baseImpl(prepaymentRequest, prepaymentAdj);
    if (!(prepaymentRequest.DocType == "PPM") || !(prepaymentRequest.OrigModule == "PO"))
      return;
    POOrderPrepayment poOrderPrepayment = PXResultset<POOrderPrepayment>.op_Implicit(((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Select(new object[2]
    {
      (object) prepaymentRequest.DocType,
      (object) prepaymentRequest.RefNbr
    }));
    if (poOrderPrepayment == null)
      return;
    poOrderPrepayment.PayDocType = prepaymentAdj.AdjgDocType;
    poOrderPrepayment.PayRefNbr = prepaymentAdj.AdjgRefNbr;
    POOrderPrepayment orderPrepayment = ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).Update(poOrderPrepayment);
    if (!orderPrepayment.IsRequest.GetValueOrDefault())
      return;
    this.InsertPOAdjustment(prepaymentRequest, prepaymentAdj, orderPrepayment);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AP.APReleaseProcess.AdjustmentProcessingOnApplication(PX.Objects.AP.APRegister,PX.Objects.AP.APAdjust)" />
  /// </summary>
  [PXOverride]
  public virtual void AdjustmentProcessingOnApplication(
    PX.Objects.AP.APRegister paymentRegister,
    APAdjust adj,
    Action<PX.Objects.AP.APRegister, APAdjust> baseImpl)
  {
    if (!((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck && paymentRegister.DocType == "PPM")
      this.UpdatePOOrderPrepaymentOnApplication(paymentRegister, adj);
    baseImpl(paymentRegister, adj);
  }

  public virtual void UpdatePOOrderPrepaymentOnApplication(PX.Objects.AP.APRegister payRegister, APAdjust adj)
  {
    POOrderPrepayment[] source = ((PXSelectBase<POOrderPrepayment>) this.poOrderPrepUpd).SelectMain(new object[2]
    {
      (object) payRegister.DocType,
      (object) payRegister.RefNbr
    });
    Decimal? nullable1 = adj.AdjgBalSign;
    Decimal? nullable2 = adj.CuryAdjgAmt;
    Decimal? billedAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    foreach (POOrderPrepayment poOrderPrepayment in ((IEnumerable<POOrderPrepayment>) source).Where<POOrderPrepayment>((Func<POOrderPrepayment, bool>) (prepay => prepay.IsRequest.GetValueOrDefault())))
    {
      PX.Objects.PO.POOrder copy = PXCache<PX.Objects.PO.POOrder>.CreateCopy(PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Select(new object[2]
      {
        (object) poOrderPrepayment.OrderType,
        (object) poOrderPrepayment.OrderNbr
      })));
      PX.Objects.PO.POOrder poOrder = copy;
      nullable2 = poOrder.CuryPrepaidTotal;
      nullable1 = billedAmt;
      poOrder.CuryPrepaidTotal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Update(copy);
    }
    this.UpdatePrepaymentPOAdjust(adj, billedAmt);
    if (adj.AdjdDocType == "PPM")
      return;
    Decimal? nullable3 = billedAmt;
    Decimal num1 = 0M;
    if (nullable3.GetValueOrDefault() <= num1 & nullable3.HasValue || !((IEnumerable<POOrderPrepayment>) source).Any<POOrderPrepayment>((Func<POOrderPrepayment, bool>) (prepay => !prepay.IsRequest.GetValueOrDefault())))
      return;
    PX.Objects.AP.APTran[] array1 = GraphHelper.RowCast<PX.Objects.AP.APTran>((IEnumerable) PXSelectBase<PX.Objects.AP.APTran, PXSelectGroupBy<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<PX.Objects.AP.APTran.pOOrderType, IsNotNull, And<PX.Objects.AP.APTran.pONbr, IsNotNull>>>>, Aggregate<GroupBy<PX.Objects.AP.APTran.pOOrderType, GroupBy<PX.Objects.AP.APTran.pONbr, Sum<PX.Objects.AP.APTran.curyTranAmt>>>>, OrderBy<Desc<PX.Objects.AP.APTran.curyTranAmt>>>.Config>.Select((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, new object[2]
    {
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    })).ToArray<PX.Objects.AP.APTran>();
    if (array1.Length == 0)
      return;
    POAdjust[] array2 = GraphHelper.RowCast<POAdjust>((IEnumerable) ((PXSelectBase<POAdjust>) this.poAdjustments).Select(new object[2]
    {
      (object) payRegister.DocType,
      (object) payRegister.RefNbr
    })).Where<POAdjust>((Func<POAdjust, bool>) (poAdjust => !poAdjust.IsRequest.GetValueOrDefault())).ToArray<POAdjust>();
    foreach (PX.Objects.AP.APTran apTran in array1)
    {
      PX.Objects.AP.APTran aptranGroup = apTran;
      Decimal? nullable4;
      foreach (POAdjust row1 in ((IEnumerable<POAdjust>) array2).Where<POAdjust>((Func<POAdjust, bool>) (poAdjust => poAdjust.AdjdOrderType == aptranGroup.POOrderType && poAdjust.AdjdOrderNbr == aptranGroup.PONbr)))
      {
        nullable3 = row1.CuryAdjgAmt;
        nullable4 = billedAmt;
        Decimal? nullable5 = nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue ? row1.CuryAdjgAmt : billedAmt;
        nullable4 = nullable5;
        Decimal num2 = 0M;
        if (!(nullable4.GetValueOrDefault() == num2 & nullable4.HasValue))
        {
          POAdjust copy1 = PXCache<POAdjust>.CreateCopy(row1);
          POAdjust poAdjust1 = copy1;
          nullable4 = poAdjust1.CuryAdjgAmt;
          nullable3 = nullable5;
          poAdjust1.CuryAdjgAmt = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          POAdjust poAdjust2 = copy1;
          nullable3 = poAdjust2.CuryAdjgBilledAmt;
          nullable4 = nullable5;
          poAdjust2.CuryAdjgBilledAmt = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          ((PXSelectBase<POAdjust>) this.poAdjustments).Update(copy1);
          nullable4 = billedAmt;
          nullable3 = nullable5;
          billedAmt = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          PX.Objects.PO.POOrder row2 = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Select(new object[2]
          {
            (object) row1.AdjdOrderType,
            (object) row1.AdjdOrderNbr
          }));
          PX.Objects.PO.POOrder copy2 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(row2);
          if (row2.CuryID == payRegister.CuryID)
          {
            PX.Objects.PO.POOrder poOrder = copy2;
            nullable3 = poOrder.CuryPrepaidTotal;
            nullable4 = nullable5;
            poOrder.CuryPrepaidTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            Decimal baseval;
            PX.Objects.CM.PXCurrencyAttribute.CuryConvBase<POAdjust.adjgCuryInfoID>(((PXSelectBase) this.poAdjustments).Cache, (object) row1, nullable5.GetValueOrDefault(), out baseval);
            Decimal curyval;
            PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(((PXSelectBase) this.poOrderUPD).Cache, (object) row2, baseval, out curyval);
            PX.Objects.PO.POOrder poOrder = copy2;
            nullable4 = poOrder.CuryPrepaidTotal;
            Decimal num3 = curyval;
            Decimal? nullable6;
            if (!nullable4.HasValue)
            {
              nullable3 = new Decimal?();
              nullable6 = nullable3;
            }
            else
              nullable6 = new Decimal?(nullable4.GetValueOrDefault() - num3);
            poOrder.CuryPrepaidTotal = nullable6;
          }
          ((PXSelectBase<PX.Objects.PO.POOrder>) this.poOrderUPD).Update(copy2);
        }
      }
      nullable4 = billedAmt;
      Decimal num4 = 0M;
      if (nullable4.GetValueOrDefault() == num4 & nullable4.HasValue)
        break;
    }
  }

  protected virtual void InsertPOAdjustment(
    PX.Objects.AP.APRegister prepaymentRequest,
    APAdjust prepaymentAdj,
    POOrderPrepayment orderPrepayment)
  {
    if (this.GetPrepaymentPOAjust(prepaymentRequest.RefNbr) != null)
      return;
    POAdjust poAdjust = ((PXSelectBase<POAdjust>) this.poAdjustments).Insert(new POAdjust()
    {
      AdjgDocType = prepaymentRequest.DocType,
      AdjgRefNbr = prepaymentRequest.RefNbr,
      AdjdOrderType = orderPrepayment.OrderType,
      AdjdOrderNbr = orderPrepayment.OrderNbr,
      AdjdDocType = prepaymentRequest.DocType,
      AdjdRefNbr = prepaymentRequest.RefNbr,
      AdjNbr = new int?(0),
      IsRequest = new bool?(true)
    });
    poAdjust.CuryAdjgAmt = prepaymentAdj.CuryAdjdAmt;
    ((PXSelectBase<POAdjust>) this.poAdjustments).Update(poAdjust);
  }

  protected virtual POAdjust GetPrepaymentPOAjust(string prepaymentNbr)
  {
    return PXResultset<POAdjust>.op_Implicit(((PXSelectBase<POAdjust>) new PXSelect<POAdjust, Where<POAdjust.adjgDocType, Equal<Required<PX.Objects.AP.APPayment.docType>>, And<POAdjust.adjgRefNbr, Equal<Required<PX.Objects.AP.APPayment.refNbr>>, And<POAdjust.adjNbr, Equal<int0>, And<POAdjust.adjdDocType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<POAdjust.adjdRefNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>>>>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base)).Select(new object[4]
    {
      (object) "PPM",
      (object) prepaymentNbr,
      (object) "PPM",
      (object) prepaymentNbr
    }));
  }

  protected virtual void UpdatePrepaymentPOAdjust(APAdjust adj, Decimal? billedAmt)
  {
    if (!(adj.AdjgDocType == "PPM"))
      return;
    POAdjust prepaymentPoAjust = this.GetPrepaymentPOAjust(adj.AdjgRefNbr);
    if (prepaymentPoAjust == null)
      return;
    POAdjust copy = PXCache<POAdjust>.CreateCopy(prepaymentPoAjust);
    POAdjust poAdjust1 = copy;
    Decimal? nullable1 = poAdjust1.CuryAdjgAmt;
    Decimal? nullable2 = billedAmt;
    poAdjust1.CuryAdjgAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    POAdjust poAdjust2 = copy;
    nullable2 = poAdjust2.CuryAdjgBilledAmt;
    nullable1 = billedAmt;
    poAdjust2.CuryAdjgBilledAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    ((PXSelectBase<POAdjust>) this.poAdjustments).Update(copy);
  }

  public delegate List<PX.Objects.AP.APRegister> ReleaseInvoiceHandler(
    JournalEntry je,
    ref PX.Objects.AP.APRegister doc,
    PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AP.Vendor> res,
    bool isPrebooking,
    out List<PX.Objects.IN.INRegister> inDocs);
}
