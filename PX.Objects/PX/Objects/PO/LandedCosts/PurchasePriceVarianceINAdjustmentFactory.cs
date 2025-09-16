// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.PurchasePriceVarianceINAdjustmentFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public class PurchasePriceVarianceINAdjustmentFactory
{
  private readonly INAdjustmentEntry _inGraph;

  public PurchasePriceVarianceINAdjustmentFactory(INAdjustmentEntry inGraph)
  {
    this._inGraph = inGraph;
  }

  public virtual void CreateAdjustmentTran(
    List<AllocationServiceBase.POReceiptLineAdjustment> pOLinesToProcess,
    string ReasonCode)
  {
    Dictionary<int?, PX.Objects.PM.Lite.PMProject> projects;
    Dictionary<int?, PX.Objects.PM.Lite.PMTask> tasks;
    PurchasePriceVarianceAllocationService.Instance.GetProjectsAndTasks((PXGraph) this._inGraph, pOLinesToProcess, out projects, out tasks);
    foreach (AllocationServiceBase.POReceiptLineAdjustment receiptLineAdjustment in pOLinesToProcess)
    {
      INTran tran = new INTran();
      INTran originalInTran = PurchasePriceVarianceAllocationService.Instance.GetOriginalInTran((PXGraph) this._inGraph, receiptLineAdjustment.ReceiptLine.ReceiptType, receiptLineAdjustment.ReceiptLine.ReceiptNbr, receiptLineAdjustment.ReceiptLine.LineNbr);
      bool flag = receiptLineAdjustment.ReceiptLine.LineType == "GP" || receiptLineAdjustment.ReceiptLine.LineType == "NP";
      if (!flag && originalInTran == null)
        throw new PXException("Inventory Receipt for Purchase Receipt# '{0}' was not found.", new object[1]
        {
          (object) receiptLineAdjustment.ReceiptLine.ReceiptNbr
        });
      if (POLineType.IsStockNonDropShip(receiptLineAdjustment.ReceiptLine.LineType))
      {
        tran.TranType = "RCA";
      }
      else
      {
        tran.IsCostUnmanaged = new bool?(true);
        tran.TranType = "ADJ";
        tran.InvtMult = new short?((short) 0);
      }
      tran.IsStockItem = receiptLineAdjustment.ReceiptLine.IsStockItem;
      tran.InventoryID = receiptLineAdjustment.ReceiptLine.InventoryID;
      tran.SubItemID = receiptLineAdjustment.ReceiptLine.SubItemID;
      tran.SiteID = receiptLineAdjustment.ReceiptLine.SiteID;
      tran.BAccountID = receiptLineAdjustment.ReceiptLine.VendorID;
      tran.BranchID = receiptLineAdjustment.BranchID;
      int? nullable1;
      if (flag)
      {
        nullable1 = tran.SiteID;
        if (nullable1.HasValue)
        {
          INSite inSite = INSite.PK.Find((PXGraph) this._inGraph, tran.SiteID);
          nullable1 = inSite.DropShipLocationID;
          if (!nullable1.HasValue)
            throw new PXException("Drop-Ship Location is not configured for warehouse {0}", new object[1]
            {
              (object) inSite.SiteCD
            });
          tran.LocationID = inSite.DropShipLocationID;
          goto label_13;
        }
      }
      INTran inTran = tran;
      nullable1 = receiptLineAdjustment.ReceiptLine.LocationID;
      int? nullable2 = nullable1 ?? originalInTran.LocationID;
      inTran.LocationID = nullable2;
label_13:
      tran.LotSerialNbr = receiptLineAdjustment.ReceiptLine.LotSerialNbr;
      tran.POReceiptType = receiptLineAdjustment.ReceiptLine.ReceiptType;
      tran.POReceiptNbr = receiptLineAdjustment.ReceiptLine.ReceiptNbr;
      tran.POReceiptLineNbr = receiptLineAdjustment.ReceiptLine.LineNbr;
      tran.POLineType = receiptLineAdjustment.ReceiptLine.LineType;
      tran.ProjectID = receiptLineAdjustment.ReceiptLine.ProjectID;
      tran.TaskID = receiptLineAdjustment.ReceiptLine.TaskID;
      tran.CostCodeID = receiptLineAdjustment.ReceiptLine.CostCodeID;
      tran.TranDesc = receiptLineAdjustment.ReceiptLine.TranDesc;
      tran.TranCost = new Decimal?(receiptLineAdjustment.AllocatedAmt);
      tran.ReasonCode = ReasonCode;
      if (originalInTran != null && originalInTran.DocType == "I")
      {
        tran.ARDocType = originalInTran.ARDocType;
        tran.ARRefNbr = originalInTran.ARRefNbr;
        tran.ARLineNbr = originalInTran.ARLineNbr;
      }
      if (!flag)
      {
        tran.OrigDocType = originalInTran.DocType;
        tran.OrigTranType = originalInTran.TranType;
        tran.OrigRefNbr = originalInTran.RefNbr;
      }
      int? aAccountID = new int?();
      int? aSubID = new int?();
      if (originalInTran != null)
      {
        tran.AcctID = originalInTran.AcctID;
        tran.SubID = originalInTran.SubID;
      }
      else if (flag)
      {
        tran.AcctID = receiptLineAdjustment.ReceiptLine.POAccrualAcctID;
        tran.SubID = receiptLineAdjustment.ReceiptLine.POAccrualSubID;
      }
      PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this._inGraph, ReasonCode);
      if (reasonCode == null)
        throw new PXException("Reason Code '{0}' cannot be found", new object[1]
        {
          (object) ReasonCode
        });
      PX.Objects.PM.Lite.PMProject project = (PX.Objects.PM.Lite.PMProject) null;
      PX.Objects.PM.Lite.PMTask task = (PX.Objects.PM.Lite.PMTask) null;
      if (projects != null)
      {
        nullable1 = receiptLineAdjustment.ReceiptLine.ProjectID;
        if (nullable1.HasValue)
        {
          nullable1 = receiptLineAdjustment.ReceiptLine.ProjectID;
          int? nullable3 = ProjectDefaultAttribute.NonProject();
          if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
          {
            projects.TryGetValue(receiptLineAdjustment.ReceiptLine.ProjectID, out project);
            if (tasks != null)
            {
              nullable3 = receiptLineAdjustment.ReceiptLine.TaskID;
              if (nullable3.HasValue)
                tasks.TryGetValue(receiptLineAdjustment.ReceiptLine.TaskID, out task);
            }
          }
        }
      }
      APReleaseProcess.GetPPVAccountSub(ref aAccountID, ref aSubID, (PXGraph) this._inGraph, receiptLineAdjustment.ReceiptLine, reasonCode, project, task);
      tran.COGSAcctID = aAccountID;
      tran.COGSSubID = aSubID;
      if (originalInTran != null && originalInTran.IsSpecialOrder.GetValueOrDefault())
      {
        tran.IsSpecialOrder = new bool?(true);
        tran.SOOrderType = originalInTran.SOOrderType;
        tran.SOOrderNbr = originalInTran.SOOrderNbr;
        tran.SOOrderLineNbr = originalInTran.SOOrderLineNbr;
      }
      this._inGraph.CostCenterDispatcherExt?.SetInventorySource(tran);
      ((PXSelectBase<INTran>) this._inGraph.transactions).Insert(tran);
    }
  }
}
