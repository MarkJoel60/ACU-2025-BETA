// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ARReleaseProcessExt.ProcessInventory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.ARReleaseProcessExt;

public class ProcessInventory : PXGraphExtension<ARReleaseProcess>
{
  public PXSetup<PX.Objects.SO.SOSetup> SOSetup;
  public PXSelectJoin<INTran, InnerJoin<SOShipLine, On<INTran.FK.SOShipmentLine>>, Where<SOShipLine.shipmentType, Equal<Current<PX.Objects.AR.ARTran.sOShipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<PX.Objects.AR.ARTran.sOShipmentNbr>>, And<SOShipLine.invoiceGroupNbr, Equal<Current<PX.Objects.AR.ARTran.sOShipmentLineGroupNbr>>, And<SOShipLine.origOrderType, Equal<Current<PX.Objects.AR.ARTran.sOOrderType>>, And<SOShipLine.origOrderNbr, Equal<Current<PX.Objects.AR.ARTran.sOOrderNbr>>, And<SOShipLine.origLineNbr, Equal<Current<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>>>>>> intranselect;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXDBDefaultAttribute.SetDefaultForUpdate<INTran.refNbr>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTran.tranDate>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<INTran.origModule>(((PXSelectBase) this.intranselect).Cache, (object) null, false);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARReleaseProcess.ReleaseInvoiceTransactionPostProcessed(PX.Objects.GL.JournalEntry,PX.Objects.AR.ARInvoice,PX.Objects.AR.ARTran)" />
  /// </summary>
  [PXOverride]
  public virtual void ReleaseInvoiceTransactionPostProcessed(
    JournalEntry je,
    PX.Objects.AR.ARInvoice ardoc,
    PX.Objects.AR.ARTran n,
    Action<JournalEntry, PX.Objects.AR.ARInvoice, PX.Objects.AR.ARTran> baseMethod)
  {
    baseMethod(je, ardoc, n);
    this.ProcessARTranInventory(n, ardoc, je);
  }

  public virtual void ProcessARTranInventory(PX.Objects.AR.ARTran n, PX.Objects.AR.ARInvoice ardoc, JournalEntry je)
  {
    if (this.Base.IsIntegrityCheck)
      return;
    bool tranCostSet = false;
    if (!string.Equals(n.SOShipmentNbr, "<NEW>"))
      tranCostSet = this.HandleARTranCost(n, ardoc, je);
    this.HandleARTranCostOrig(n, tranCostSet);
  }

  public virtual bool HandleARTranCost(PX.Objects.AR.ARTran n, PX.Objects.AR.ARInvoice ardoc, JournalEntry je)
  {
    if (EnumerableExtensions.IsNotIn<string>(n.LineType, "GI", "GN"))
      return false;
    bool flag1 = false;
    n.TranCost = new Decimal?(0M);
    foreach (INTran intran in this.GetINTransBoundToARTran(n))
    {
      intran.ARDocType = n.TranType;
      intran.ARRefNbr = n.RefNbr;
      intran.ARLineNbr = n.LineNbr;
      int? inventoryId1 = intran.InventoryID;
      int? inventoryId2 = n.InventoryID;
      Decimal? nullable1;
      Decimal? nullable2;
      if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
      {
        INTran inTran1 = intran;
        PXCache cache = ((PXSelectBase) this.intranselect).Cache;
        INTran Row = intran;
        string uom1 = intran.UOM;
        string uom2 = n.UOM;
        nullable1 = n.UnitPrice;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromTo<INTran.inventoryID>(cache, (object) Row, uom1, uom2, valueOrDefault, INPrecision.UNITCOST));
        inTran1.UnitPrice = nullable3;
        bool flag2 = string.Equals(intran.UOM, n.UOM, StringComparison.OrdinalIgnoreCase);
        bool flag3 = n.DrCr == "C" && n.SOOrderLineOperation == "R" || n.DrCr == "D" && n.SOOrderLineOperation == "I";
        INTran inTran2 = intran;
        Decimal? nullable4;
        if (!flag3)
        {
          nullable4 = n.TranAmt;
        }
        else
        {
          nullable1 = n.TranAmt;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new Decimal?(-nullable1.GetValueOrDefault());
        }
        inTran2.TranAmt = nullable4;
        nullable1 = flag2 ? n.Qty : n.BaseQty;
        Decimal num = 0M;
        if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue) && (!n.SOShipmentLineNbr.HasValue || n.TaskID.HasValue))
        {
          nullable1 = intran.TranAmt;
          Decimal? nullable5;
          if (!flag2)
          {
            Decimal? baseQty1 = intran.BaseQty;
            Decimal? baseQty2 = n.BaseQty;
            nullable5 = baseQty1.HasValue & baseQty2.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() / baseQty2.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            Decimal? qty1 = intran.Qty;
            Decimal? qty2 = n.Qty;
            nullable5 = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() / qty2.GetValueOrDefault()) : new Decimal?();
          }
          nullable2 = nullable5;
          object obj = (object) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?());
          ((PXSelectBase) this.intranselect).Cache.RaiseFieldUpdating<INTran.tranAmt>((object) intran, ref obj);
          intran.TranAmt = (Decimal?) obj;
        }
      }
      GraphHelper.MarkUpdated(((PXSelectBase) this.intranselect).Cache, (object) intran, true);
      bool? nullable6 = intran.Released;
      if (nullable6.GetValueOrDefault())
      {
        INReleaseProcess.UpdateCustSalesStats((PXGraph) this.Base, intran);
        n.InvtReleased = new bool?(true);
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, n.InventoryID);
        if (inventoryItem != null)
        {
          nullable6 = inventoryItem.StkItem;
          if (!nullable6.GetValueOrDefault())
          {
            nullable6 = inventoryItem.KitItem;
            if (nullable6.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.kitAssemblies>() && ((PXSelectBase<PX.Objects.SO.SOSetup>) this.SOSetup).Current != null)
            {
              switch (((PXSelectBase<PX.Objects.SO.SOSetup>) this.SOSetup).Current.SalesProfitabilityForNSKits)
              {
                case "C":
                  PX.Objects.AR.ARTran arTran1 = n;
                  nullable2 = arTran1.TranCost;
                  nullable1 = intran.TranCost;
                  arTran1.TranCost = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
                  n.TranCostOrig = n.TranCost;
                  n.IsTranCostFinal = new bool?(true);
                  goto label_23;
                case "S":
                  PX.Objects.AR.ARTran arTran2 = n;
                  nullable1 = arTran2.TranCost;
                  nullable2 = intran.TranCost;
                  arTran2.TranCost = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                  n.TranCostOrig = n.TranCost;
                  n.IsTranCostFinal = new bool?(true);
                  flag1 = true;
                  goto label_23;
                default:
                  goto label_23;
              }
            }
          }
        }
        PX.Objects.AR.ARTran arTran = n;
        nullable2 = arTran.TranCost;
        nullable1 = intran.TranCost;
        arTran.TranCost = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        n.TranCostOrig = n.TranCost;
        n.IsTranCostFinal = new bool?(true);
        flag1 = true;
      }
label_23:
      this.PostShippedNotInvoiced(intran, n, ardoc, je);
    }
    if (n.SOShipmentType == "H")
    {
      foreach (PXResult<INTran> pxResult in PXSelectBase<INTran, PXSelect<INTran, Where<INTran.pOReceiptNbr, Equal<Current<PX.Objects.AR.ARTran.sOShipmentNbr>>, And<INTran.pOReceiptLineNbr, Equal<Current<PX.Objects.AR.ARTran.sOShipmentLineNbr>>, And<INTran.docType, Equal<INDocType.adjustment>, And<INTran.released, Equal<True>, And<INTran.aRRefNbr, IsNull, And<INTran.sOShipmentNbr, IsNull>>>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
      {
        (object) n
      }, Array.Empty<object>()))
      {
        INTran inTran = PXResult<INTran>.op_Implicit(pxResult);
        PX.Objects.AR.ARTran arTran = n;
        Decimal? tranCost1 = arTran.TranCost;
        Decimal? tranCost2 = inTran.TranCost;
        arTran.TranCost = tranCost1.HasValue & tranCost2.HasValue ? new Decimal?(tranCost1.GetValueOrDefault() + tranCost2.GetValueOrDefault()) : new Decimal?();
      }
    }
    return flag1;
  }

  public virtual IEnumerable<INTran> GetINTransBoundToARTran(PX.Objects.AR.ARTran artran)
  {
    return artran.SOShipmentType == "H" ? GraphHelper.RowCast<INTran>((IEnumerable) PXSelectBase<INTran, PXSelect<INTran, Where<INTran.sOShipmentType, Equal<Current<PX.Objects.AR.ARTran.sOShipmentType>>, And<INTran.sOShipmentNbr, Equal<Current<PX.Objects.AR.ARTran.sOShipmentNbr>>, And<INTran.sOOrderType, Equal<Current<PX.Objects.AR.ARTran.sOOrderType>>, And<INTran.sOOrderNbr, Equal<Current<PX.Objects.AR.ARTran.sOOrderNbr>>, And<INTran.sOOrderLineNbr, Equal<Current<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) artran
    }, Array.Empty<object>())) : GraphHelper.RowCast<INTran>((IEnumerable) ((PXSelectBase) this.intranselect).View.SelectMultiBound(new object[1]
    {
      (object) artran
    }, Array.Empty<object>()));
  }

  public virtual void PostShippedNotInvoiced(
    INTran intran,
    PX.Objects.AR.ARTran n,
    PX.Objects.AR.ARInvoice ardoc,
    JournalEntry je)
  {
    if (!intran.UpdateShippedNotInvoiced.GetValueOrDefault())
      return;
    if (!intran.Released.GetValueOrDefault())
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Please release inventory Issue {0} before releasing Invoice.", new object[1]
      {
        (object) intran.RefNbr
      }));
    PXResultset<INTranCost> pxResultset = PXSelectBase<INTranCost, PXSelect<INTranCost, Where<INTranCost.costDocType, Equal<Required<INTranCost.costDocType>>, And<INTranCost.costRefNbr, Equal<Required<INTranCost.costRefNbr>>, And<INTranCost.lineNbr, Equal<Required<INTranCost.lineNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) intran.DocType,
      (object) intran.RefNbr,
      (object) intran.LineNbr
    });
    PMProject project;
    PMTask task;
    PMProjectHelper.TryToGetProjectAndTask((PXGraph) this.Base, (int?) n?.ProjectID, (int?) n?.TaskID, out project, out task);
    foreach (PXResult<INTranCost> pxResult1 in pxResultset)
    {
      INTranCost inTranCost = PXResult<INTranCost>.op_Implicit(pxResult1);
      PXResult<PX.Objects.IN.InventoryItem, INPostClass> pxResult2 = (PXResult<PX.Objects.IN.InventoryItem, INPostClass>) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<INPostClass, On<INPostClass.postClassID, Equal<PX.Objects.IN.InventoryItem.postClassID>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) intran.InventoryID
      }));
      PX.Objects.CS.ReasonCode parent = (PX.Objects.CS.ReasonCode) PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INTran>.By<INTran.reasonCode>.FindParent((PXGraph) this.Base, (INTran.reasonCode) intran, (PKFindOptions) 0);
      PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, intran.SiteID);
      if (inTranCost != null)
      {
        int? nullable1 = inTranCost.COGSAcctID;
        if (nullable1.HasValue && intran != null)
        {
          short? invtMult = inTranCost.InvtMult;
          nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          int num1 = 1;
          int num2 = nullable1.GetValueOrDefault() == num1 & nullable1.HasValue ? 1 : 0;
          bool? nullable2 = n.IsCancellation;
          int num3 = nullable2.GetValueOrDefault() ? 1 : 0;
          bool flag1 = (num2 ^ num3) != 0;
          Decimal curyval;
          PXCurrencyAttribute.CuryConvCury(((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)], (object) n, inTranCost.TranCost.GetValueOrDefault(), out curyval);
          PX.Objects.GL.GLTran tran1 = new PX.Objects.GL.GLTran();
          tran1.SummPost = new bool?(this.Base.SummPost);
          tran1.BranchID = intran.BranchID;
          tran1.TranType = n.TranType;
          tran1.TranClass = "S";
          PX.Objects.GL.GLTran glTran1 = tran1;
          nullable1 = intran.COGSAcctID;
          int? nullable3 = nullable1 ?? INReleaseProcess.GetAccountDefaults<INPostClass.cOGSAcctID>((PXGraph) this.Base, InventoryAccountServiceHelper.Params(PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), site, PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), intran, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
          glTran1.AccountID = nullable3;
          PX.Objects.GL.GLTran glTran2 = tran1;
          nullable1 = intran.COGSSubID;
          int? nullable4 = nullable1 ?? INReleaseProcess.GetAccountDefaults<INPostClass.cOGSSubID>((PXGraph) this.Base, InventoryAccountServiceHelper.Params(PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), site, PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), intran, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
          glTran2.SubID = nullable4;
          tran1.CuryDebitAmt = new Decimal?(flag1 ? curyval : 0M);
          tran1.DebitAmt = flag1 ? inTranCost.TranCost : new Decimal?(0M);
          tran1.CuryCreditAmt = new Decimal?(flag1 ? 0M : curyval);
          tran1.CreditAmt = flag1 ? new Decimal?(0M) : inTranCost.TranCost;
          tran1.RefNbr = n.RefNbr;
          tran1.InventoryID = inTranCost.InventoryID;
          PX.Objects.GL.GLTran glTran3 = tran1;
          Decimal? nullable5;
          if (!flag1)
          {
            Decimal? qty = inTranCost.Qty;
            nullable5 = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
          }
          else
            nullable5 = inTranCost.Qty;
          glTran3.Qty = nullable5;
          tran1.UOM = intran.UOM;
          tran1.TranDesc = intran.TranDesc;
          tran1.TranDate = n.TranDate;
          invtMult = inTranCost.InvtMult;
          nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          int num4 = 0;
          bool flag2 = nullable1.GetValueOrDefault() == num4 & nullable1.HasValue;
          tran1.ProjectID = flag2 ? ProjectDefaultAttribute.NonProject() : intran.ProjectID;
          PX.Objects.GL.GLTran glTran4 = tran1;
          int? nullable6;
          if (!flag2)
          {
            nullable6 = intran.TaskID;
          }
          else
          {
            nullable1 = new int?();
            nullable6 = nullable1;
          }
          glTran4.TaskID = nullable6;
          tran1.CostCodeID = tran1.CostCodeID;
          tran1.Released = new bool?(true);
          PX.Objects.GL.GLTran glTran5 = tran1;
          nullable2 = tran1.SummPost;
          int? nullable7;
          if (!nullable2.GetValueOrDefault())
          {
            nullable7 = n.LineNbr;
          }
          else
          {
            nullable1 = new int?();
            nullable7 = nullable1;
          }
          glTran5.TranLineNbr = nullable7;
          this.Base.InsertInvoiceDetailsINTranCostTransaction(je, tran1, new ARReleaseProcess.GLTranInsertionContext()
          {
            ARRegisterRecord = (PX.Objects.AR.ARRegister) ardoc,
            ARTranRecord = n,
            INTranRecord = intran,
            INTranCostRecord = inTranCost
          });
          PX.Objects.GL.GLTran tran2 = new PX.Objects.GL.GLTran();
          tran2.SummPost = new bool?(this.Base.SummPost);
          tran2.BranchID = n.BranchID;
          if (parent?.Usage == "I")
          {
            tran2.AccountID = parent.AccountID;
            tran2.SubID = INReleaseProcess.GetReasonCodeSubID((PXGraph) je, parent, InventoryAccountServiceHelper.Params(PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), site, PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
          }
          else
          {
            tran2.AccountID = INReleaseProcess.GetAccountDefaults<INPostClass.cOGSAcctID>((PXGraph) this.Base, InventoryAccountServiceHelper.Params(PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), site, PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
            if (PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2) != null)
            {
              nullable2 = PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2).COGSSubFromSales;
              if (!nullable2.GetValueOrDefault())
              {
                tran2.SubID = INReleaseProcess.GetAccountDefaults<INPostClass.cOGSSubID>((PXGraph) this.Base, InventoryAccountServiceHelper.Params(PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), site, PXResult<PX.Objects.IN.InventoryItem, INPostClass>.op_Implicit(pxResult2), (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
                goto label_23;
              }
            }
            tran2.SubID = n.SubID;
          }
label_23:
          tran2.CuryDebitAmt = new Decimal?(flag1 ? 0M : curyval);
          tran2.DebitAmt = flag1 ? new Decimal?(0M) : inTranCost.TranCost;
          tran2.CuryCreditAmt = new Decimal?(flag1 ? curyval : 0M);
          tran2.CreditAmt = flag1 ? inTranCost.TranCost : new Decimal?(0M);
          tran2.TranType = n.TranType;
          tran2.TranClass = "S";
          tran2.RefNbr = n.RefNbr;
          tran2.InventoryID = inTranCost.InventoryID;
          PX.Objects.GL.GLTran glTran6 = tran2;
          Decimal? nullable8;
          if (!flag1)
          {
            nullable8 = inTranCost.Qty;
          }
          else
          {
            Decimal? qty = inTranCost.Qty;
            nullable8 = qty.HasValue ? new Decimal?(-qty.GetValueOrDefault()) : new Decimal?();
          }
          glTran6.Qty = nullable8;
          tran2.UOM = intran.UOM;
          tran2.TranDesc = intran.TranDesc;
          tran2.TranDate = n.TranDate;
          PX.Objects.GL.GLTran glTran7 = tran2;
          invtMult = inTranCost.InvtMult;
          nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          int num5 = 1;
          int? nullable9 = nullable1.GetValueOrDefault() == num5 & nullable1.HasValue ? ProjectDefaultAttribute.NonProject() : intran.ProjectID;
          glTran7.ProjectID = nullable9;
          PX.Objects.GL.GLTran glTran8 = tran2;
          invtMult = inTranCost.InvtMult;
          nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
          int num6 = 1;
          int? nullable10;
          if (!(nullable1.GetValueOrDefault() == num6 & nullable1.HasValue))
          {
            nullable10 = intran.TaskID;
          }
          else
          {
            nullable1 = new int?();
            nullable10 = nullable1;
          }
          glTran8.TaskID = nullable10;
          tran2.CostCodeID = intran.CostCodeID;
          tran2.Released = new bool?(true);
          PX.Objects.GL.GLTran glTran9 = tran2;
          nullable2 = tran2.SummPost;
          int? nullable11;
          if (!nullable2.GetValueOrDefault())
          {
            nullable11 = n.LineNbr;
          }
          else
          {
            nullable1 = new int?();
            nullable11 = nullable1;
          }
          glTran9.TranLineNbr = nullable11;
          this.Base.InsertInvoiceDetailsINTranCostTransaction(je, tran2, new ARReleaseProcess.GLTranInsertionContext()
          {
            ARRegisterRecord = (PX.Objects.AR.ARRegister) ardoc,
            ARTranRecord = n,
            INTranRecord = intran,
            INTranCostRecord = inTranCost
          });
        }
      }
    }
  }

  public virtual void HandleARTranCostOrig(PX.Objects.AR.ARTran n, bool tranCostSet)
  {
    if (!n.InventoryID.HasValue || !EnumerableExtensions.IsIn<string>(n.LineType, (string) null, "MI") && (!EnumerableExtensions.IsIn<string>(n.LineType, "GI", "GN") || tranCostSet))
      return;
    PXResult<PX.Objects.IN.InventoryItem, INItemSite> pxResult = (PXResult<PX.Objects.IN.InventoryItem, INItemSite>) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, LeftJoin<INItemSite, On<INItemSite.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[2]
    {
      (object) n.SiteID,
      (object) n.InventoryID
    }));
    if (pxResult == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem, INItemSite>.op_Implicit(pxResult);
    INItemSite inItemSite = PXResult<PX.Objects.IN.InventoryItem, INItemSite>.op_Implicit(pxResult);
    if (((bool?) SOShipLine.PK.Find((PXGraph) this.Base, n.SOShipmentNbr, n.SOShipmentLineNbr)?.IsStockItem ?? (bool?) PX.Objects.SO.SOLine.PK.Find((PXGraph) this.Base, n.SOOrderType, n.SOOrderNbr, n.SOOrderLineNbr)?.IsStockItem ?? inventoryItem.StkItem).GetValueOrDefault())
    {
      Decimal? nullable1;
      if (inItemSite != null && inItemSite.TranUnitCost.HasValue)
      {
        PX.Objects.AR.ARTran arTran = n;
        Decimal? baseQty = n.BaseQty;
        Decimal? tranUnitCost = inItemSite.TranUnitCost;
        Decimal? nullable2 = baseQty.HasValue & tranUnitCost.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * tranUnitCost.GetValueOrDefault()) : new Decimal?();
        arTran.TranCostOrig = nullable2;
      }
      else
      {
        INItemCost inItemCost = PXResultset<INItemCost>.op_Implicit(PXSelectBase<INItemCost, PXSelectReadonly<INItemCost, Where<INItemCost.inventoryID, Equal<Required<PX.Objects.AR.ARTran.inventoryID>>, And<INItemCost.curyID, EqualBaseCuryID<Current<PX.Objects.AR.ARRegister.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) n.InventoryID
        }));
        if (inItemCost != null && inItemCost.TranUnitCost.HasValue)
        {
          PX.Objects.AR.ARTran arTran = n;
          Decimal? baseQty = n.BaseQty;
          nullable1 = inItemCost.TranUnitCost;
          Decimal? nullable3 = baseQty.HasValue & nullable1.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
          arTran.TranCostOrig = nullable3;
        }
      }
      nullable1 = n.BaseQty;
      Decimal num = 0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        return;
      n.IsTranCostFinal = new bool?(true);
    }
    else
    {
      if (!(n.SOShipmentType != "H") && !(n.LineType == "MI"))
        return;
      if (n.AccrueCost.GetValueOrDefault() && n.AccruedCost.HasValue)
      {
        n.TranCost = n.AccruedCost;
        n.TranCostOrig = n.AccruedCost;
        n.IsTranCostFinal = new bool?(true);
      }
      else
      {
        PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, n.BranchID);
        Decimal valueOrDefault = ((Decimal?) InventoryItemCurySettings.PK.Find((PXGraph) this.Base, inventoryItem.InventoryID, branch.BaseCuryID)?.StdCost).GetValueOrDefault();
        if (inventoryItem.KitItem.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.kitAssemblies>() && ((PXSelectBase<PX.Objects.SO.SOSetup>) this.SOSetup).Current != null)
        {
          switch (((PXSelectBase<PX.Objects.SO.SOSetup>) this.SOSetup).Current.SalesProfitabilityForNSKits)
          {
            case "K":
              PX.Objects.AR.ARTran arTran1 = n;
              Decimal? tranCost1 = arTran1.TranCost;
              Decimal? baseQty1 = n.BaseQty;
              Decimal num1 = valueOrDefault;
              Decimal? nullable4 = baseQty1.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() * num1) : new Decimal?();
              arTran1.TranCost = tranCost1.HasValue & nullable4.HasValue ? new Decimal?(tranCost1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
              n.TranCostOrig = n.TranCost;
              n.IsTranCostFinal = new bool?(true);
              break;
            case "C":
              PX.Objects.AR.ARTran arTran2 = n;
              Decimal? tranCost2 = arTran2.TranCost;
              Decimal? baseQty2 = n.BaseQty;
              Decimal num2 = valueOrDefault;
              Decimal? nullable5 = baseQty2.HasValue ? new Decimal?(baseQty2.GetValueOrDefault() * num2) : new Decimal?();
              arTran2.TranCost = tranCost2.HasValue & nullable5.HasValue ? new Decimal?(tranCost2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
              n.TranCostOrig = n.TranCost;
              n.IsTranCostFinal = new bool?(this.HasStockComponents(n));
              break;
            case "S":
              n.IsTranCostFinal = new bool?(this.HasStockComponents(n));
              break;
          }
        }
        else
        {
          PX.Objects.AR.ARTran arTran3 = n;
          Decimal? tranCost3 = arTran3.TranCost;
          Decimal? baseQty3 = n.BaseQty;
          Decimal num3 = valueOrDefault;
          Decimal? nullable6 = baseQty3.HasValue ? new Decimal?(baseQty3.GetValueOrDefault() * num3) : new Decimal?();
          arTran3.TranCost = tranCost3.HasValue & nullable6.HasValue ? new Decimal?(tranCost3.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
          n.TranCostOrig = n.TranCost;
          n.IsTranCostFinal = new bool?(true);
        }
      }
    }
  }

  public virtual bool HasStockComponents(PX.Objects.AR.ARTran n)
  {
    if (n.IsTranCostFinal.GetValueOrDefault())
      return true;
    if (((IQueryable<PXResult<SOShipLineSplit>>) PXSelectBase<SOShipLineSplit, PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Current<PX.Objects.AR.ARTran.sOShipmentNbr>>, And<SOShipLineSplit.lineNbr, Equal<Current<PX.Objects.AR.ARTran.sOShipmentLineNbr>>, And<SOShipLineSplit.isStockItem, Equal<True>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) n
    }, Array.Empty<object>())).Count<PXResult<SOShipLineSplit>>() != 0)
      return false;
    return ((IQueryable<PXResult<PX.Objects.SO.SOLineSplit>>) PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Current<PX.Objects.AR.ARTran.sOOrderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Current<PX.Objects.AR.ARTran.sOOrderNbr>>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<Current<PX.Objects.AR.ARTran.sOOrderLineNbr>>, And<PX.Objects.SO.SOLineSplit.isStockItem, Equal<True>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) n
    }, Array.Empty<object>())).Count<PXResult<PX.Objects.SO.SOLineSplit>>() == 0;
  }
}
