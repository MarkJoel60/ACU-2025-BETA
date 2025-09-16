// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.LandedCostINAdjustmentFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.LandedCosts;

public class LandedCostINAdjustmentFactory
{
  private readonly PXGraph _pxGraph;

  public LandedCostINAdjustmentFactory(PXGraph pxGraph) => this._pxGraph = pxGraph;

  public virtual IDictionary<POLandedCostDetail, INAdjustmentWrapper> CreateLandedCostAdjustments(
    POLandedCostDoc doc,
    IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment> adjustments)
  {
    Dictionary<POLandedCostDetail, INAdjustmentWrapper> landedCostAdjustments = new Dictionary<POLandedCostDetail, INAdjustmentWrapper>();
    foreach (IGrouping<POLandedCostDetail, LandedCostAllocationService.POLandedCostReceiptLineAdjustment> source in adjustments.Where<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>((Func<LandedCostAllocationService.POLandedCostReceiptLineAdjustment, bool>) (t => t.LandedCostDetail != null)).GroupBy<LandedCostAllocationService.POLandedCostReceiptLineAdjustment, POLandedCostDetail>((Func<LandedCostAllocationService.POLandedCostReceiptLineAdjustment, POLandedCostDetail>) (t => t.LandedCostDetail)))
    {
      if (!(this.GetLandedCostCode(source.Key.LandedCostCodeID).AllocationMethod == "N"))
      {
        INAdjustmentWrapper adjustmentWrapper = new INAdjustmentWrapper(new PX.Objects.IN.INRegister()
        {
          DocType = "A",
          OrigModule = "PO",
          SiteID = new int?(),
          TranDate = doc.DocDate,
          FinPeriodID = doc.FinPeriodID,
          BranchID = doc.BranchID,
          Hold = new bool?(false)
        }, (ICollection<INTran>) this.CreateTransactions(doc, source.Key, source.AsEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>()));
        landedCostAdjustments.Add(source.Key, adjustmentWrapper);
      }
    }
    return (IDictionary<POLandedCostDetail, INAdjustmentWrapper>) landedCostAdjustments;
  }

  protected virtual INTran[] CreateTransactions(
    POLandedCostDoc doc,
    POLandedCostDetail landedCostDetail,
    IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment> pOLinesToProcess)
  {
    List<INTran> inTranList = new List<INTran>();
    string reasonCode = this.GetLandedCostCode(landedCostDetail.LandedCostCodeID).ReasonCode;
    foreach (LandedCostAllocationService.POLandedCostReceiptLineAdjustment receiptLineAdjustment in pOLinesToProcess)
    {
      INTran inTran1 = new INTran();
      INTran originalInTran = LandedCostAllocationService.Instance.GetOriginalInTran(this._pxGraph, receiptLineAdjustment.ReceiptLine.ReceiptType, receiptLineAdjustment.ReceiptLine.ReceiptNbr, receiptLineAdjustment.ReceiptLine.LineNbr);
      bool flag = POLineType.IsDropShip(receiptLineAdjustment.ReceiptLine.LineType);
      bool? nullable1;
      if (POLineType.IsNonStockNonServiceNonDropShip(receiptLineAdjustment.ReceiptLine.LineType) && originalInTran == null)
      {
        nullable1 = PXResultset<INSetup>.op_Implicit(PXSelectBase<INSetup, PXSelectReadonly<INSetup>.Config>.Select(this._pxGraph, Array.Empty<object>())).UpdateGL;
        if (!nullable1.Value)
          continue;
      }
      if (!flag && originalInTran == null)
        throw new PXException("Inventory Receipt for Purchase Receipt# '{0}' was not found.", new object[1]
        {
          (object) receiptLineAdjustment.ReceiptLine.ReceiptNbr
        });
      if (POLineType.IsStockNonDropShip(receiptLineAdjustment.ReceiptLine.LineType))
      {
        inTran1.TranType = "RCA";
      }
      else
      {
        inTran1.IsCostUnmanaged = new bool?(true);
        inTran1.TranType = "ADJ";
        inTran1.InvtMult = new short?((short) 0);
      }
      inTran1.InventoryID = receiptLineAdjustment.ReceiptLine.InventoryID;
      inTran1.SubItemID = receiptLineAdjustment.ReceiptLine.SubItemID;
      inTran1.SiteID = receiptLineAdjustment.ReceiptLine.SiteID;
      inTran1.BAccountID = doc.VendorID;
      inTran1.BranchID = landedCostDetail.BranchID;
      int? nullable2;
      if (flag)
      {
        nullable2 = inTran1.SiteID;
        if (nullable2.HasValue)
        {
          INSite inSite = INSite.PK.Find(this._pxGraph, inTran1.SiteID);
          nullable2 = inSite.DropShipLocationID;
          if (!nullable2.HasValue)
            throw new PXException("Drop-Ship Location is not configured for warehouse {0}", new object[1]
            {
              (object) inSite.SiteCD
            });
          inTran1.LocationID = inSite.DropShipLocationID;
          goto label_15;
        }
      }
      INTran inTran2 = inTran1;
      nullable2 = receiptLineAdjustment.ReceiptLine.LocationID;
      int? nullable3 = nullable2 ?? originalInTran.LocationID;
      inTran2.LocationID = nullable3;
label_15:
      inTran1.LotSerialNbr = receiptLineAdjustment.ReceiptLine.LotSerialNbr;
      inTran1.POReceiptType = receiptLineAdjustment.ReceiptLine.ReceiptType;
      inTran1.POReceiptNbr = receiptLineAdjustment.ReceiptLine.ReceiptNbr;
      inTran1.POReceiptLineNbr = receiptLineAdjustment.ReceiptLine.LineNbr;
      inTran1.POLineType = receiptLineAdjustment.ReceiptLine.LineType;
      inTran1.TranDesc = landedCostDetail.Descr;
      inTran1.TranCost = new Decimal?(receiptLineAdjustment.AllocatedAmt);
      inTran1.ReasonCode = reasonCode;
      if (originalInTran != null)
      {
        nullable2 = originalInTran.CostCodeID;
        if (nullable2.HasValue)
          inTran1.CostCodeID = originalInTran.CostCodeID;
      }
      if (originalInTran != null && originalInTran.DocType == "I")
      {
        inTran1.ARDocType = originalInTran.ARDocType;
        inTran1.ARRefNbr = originalInTran.ARRefNbr;
        inTran1.ARLineNbr = originalInTran.ARLineNbr;
      }
      if (!flag)
      {
        inTran1.OrigDocType = originalInTran.DocType;
        inTran1.OrigTranType = originalInTran.TranType;
        inTran1.OrigRefNbr = originalInTran.RefNbr;
      }
      int? aAccountID = new int?();
      int? aSubID = new int?();
      inTran1.AcctID = landedCostDetail.LCAccrualAcct;
      inTran1.SubID = landedCostDetail.LCAccrualSub;
      this.GetLCVarianceAccountSub(ref aAccountID, ref aSubID, receiptLineAdjustment.ReceiptLine);
      inTran1.COGSAcctID = aAccountID;
      inTran1.COGSSubID = aSubID;
      if (originalInTran != null)
      {
        nullable1 = originalInTran.IsSpecialOrder;
        if (nullable1.GetValueOrDefault())
        {
          inTran1.IsSpecialOrder = new bool?(true);
          inTran1.SOOrderType = originalInTran.SOOrderType;
          inTran1.SOOrderNbr = originalInTran.SOOrderNbr;
          inTran1.SOOrderLineNbr = originalInTran.SOOrderLineNbr;
        }
      }
      inTranList.Add(inTran1);
    }
    return inTranList.ToArray();
  }

  protected virtual LandedCostCode GetLandedCostCode(string landedCostCodeID)
  {
    return LandedCostCode.PK.Find(this._pxGraph, landedCostCodeID);
  }

  protected virtual void GetLCVarianceAccountSub(
    ref int? aAccountID,
    ref int? aSubID,
    PX.Objects.PO.POReceiptLine receiptLine)
  {
    if (receiptLine.InventoryID.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(this._pxGraph, receiptLine.InventoryID);
      if (inventoryItem != null)
      {
        INPostClass postclass = INPostClass.PK.Find(this._pxGraph, inventoryItem.PostClassID);
        if (inventoryItem.StkItem.Value)
        {
          if (postclass == null)
            throw new PXException("Posting class is not defined for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}", new object[3]
            {
              (object) inventoryItem.InventoryCD,
              (object) receiptLine.ReceiptNbr,
              (object) receiptLine.LineNbr
            });
          INSite site = (INSite) null;
          int? nullable = receiptLine.SiteID;
          if (nullable.HasValue)
            site = INSite.PK.Find(this._pxGraph, receiptLine.SiteID);
          if (receiptLine.LineType == "GP")
          {
            PXGraph pxGraph = this._pxGraph;
            int? projectID;
            if (receiptLine == null)
            {
              nullable = new int?();
              projectID = nullable;
            }
            else
              projectID = receiptLine.ProjectID;
            int? taskID;
            if (receiptLine == null)
            {
              nullable = new int?();
              taskID = nullable;
            }
            else
              taskID = receiptLine.TaskID;
            PMProject project;
            ref PMProject local1 = ref project;
            PMTask task;
            ref PMTask local2 = ref task;
            PMProjectHelper.TryToGetProjectAndTask(pxGraph, projectID, taskID, out local1, out local2);
            aAccountID = INReleaseProcess.GetAcctID<INPostClass.cOGSAcctID>(this._pxGraph, postclass.COGSAcctDefault, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
            if (!aAccountID.HasValue)
              throw new PXException("GOGS Account can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
              {
                (object) inventoryItem.InventoryCD,
                (object) receiptLine.ReceiptNbr,
                (object) receiptLine.LineNbr,
                (object) postclass.PostClassID,
                site != null ? (object) site.SiteCD : (object) string.Empty
              });
            try
            {
              aSubID = INReleaseProcess.GetSubID<INPostClass.cOGSSubID>(this._pxGraph, postclass.COGSAcctDefault, postclass.COGSSubMask, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
            }
            catch (PXException ex)
            {
              if (!postclass.COGSSubID.HasValue || string.IsNullOrEmpty(postclass.COGSSubMask) || !inventoryItem.COGSSubID.HasValue || site == null || !site.COGSSubID.HasValue)
                throw new PXException("COGS Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
                {
                  (object) inventoryItem.InventoryCD,
                  (object) receiptLine.ReceiptNbr,
                  (object) receiptLine.LineNbr,
                  (object) postclass.PostClassID,
                  site != null ? (object) site.SiteCD : (object) string.Empty
                });
              throw ex;
            }
            if (!aSubID.HasValue)
              throw new PXException("COGS Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
              {
                (object) inventoryItem.InventoryCD,
                (object) receiptLine.ReceiptNbr,
                (object) receiptLine.LineNbr,
                (object) postclass.PostClassID,
                site != null ? (object) site.SiteCD : (object) string.Empty
              });
          }
          else
          {
            aAccountID = INReleaseProcess.GetAcctID<INPostClass.lCVarianceAcctID>(this._pxGraph, postclass.LCVarianceAcctDefault, inventoryItem, site, postclass);
            if (!aAccountID.HasValue)
              throw new PXException("Landed Cost Variance Account can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
              {
                (object) inventoryItem.InventoryCD,
                (object) receiptLine.ReceiptNbr,
                (object) receiptLine.LineNbr,
                (object) postclass.PostClassID,
                site != null ? (object) site.SiteCD : (object) string.Empty
              });
            try
            {
              aSubID = INReleaseProcess.GetSubID<INPostClass.lCVarianceSubID>(this._pxGraph, postclass.LCVarianceAcctDefault, postclass.LCVarianceSubMask, inventoryItem, site, postclass);
            }
            catch (PXException ex)
            {
              if (!postclass.LCVarianceSubID.HasValue || string.IsNullOrEmpty(postclass.LCVarianceSubMask) || !inventoryItem.LCVarianceSubID.HasValue || site == null || !site.LCVarianceSubID.HasValue)
                throw new PXException("Landed Cost Variance Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
                {
                  (object) inventoryItem.InventoryCD,
                  (object) receiptLine.ReceiptNbr,
                  (object) receiptLine.LineNbr,
                  (object) postclass.PostClassID,
                  site != null ? (object) site.SiteCD : (object) string.Empty
                });
              throw ex;
            }
            if (!aSubID.HasValue)
              throw new PXException("Landed Cost Variance Subaccount can't be found for the Inventory Item '{0}' in PO Receipt# '{1}' line {2}. Please, check the settings in the IN Post Class '{3}', Inventory Item and Warehouse '{4}'", new object[5]
              {
                (object) inventoryItem.InventoryCD,
                (object) receiptLine.ReceiptNbr,
                (object) receiptLine.LineNbr,
                (object) postclass.PostClassID,
                site != null ? (object) site.SiteCD : (object) string.Empty
              });
          }
        }
        else
        {
          aAccountID = receiptLine.ExpenseAcctID;
          aSubID = receiptLine.ExpenseSubID;
        }
      }
      else
        throw new PXException("Inventory Item '{0}' used in PO Receipt# '{1}' line {2} is not found in the system", new object[3]
        {
          (object) receiptLine.InventoryID,
          (object) receiptLine.ReceiptNbr,
          (object) receiptLine.LineNbr
        });
    }
    else
    {
      aAccountID = receiptLine.ExpenseAcctID;
      aSubID = receiptLine.ExpenseSubID;
    }
  }
}
