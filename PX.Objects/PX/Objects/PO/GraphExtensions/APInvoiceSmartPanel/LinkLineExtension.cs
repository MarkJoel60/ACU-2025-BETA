// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.LinkLineExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;

/// <summary>
/// This class implements graph extension to use special dialogs called Smart Panel to perform "LINK LINE" (Screen AP301000)
/// </summary>
[Serializable]
public class LinkLineExtension : PXGraphExtension<APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POLandedCostDetailS, InnerJoin<POLandedCostDoc, On<POLandedCostDetailS.refNbr, Equal<POLandedCostDoc.refNbr>, And<POLandedCostDetailS.docType, Equal<POLandedCostDoc.docType>>>, InnerJoin<LandedCostCode, On<POLandedCostDetailS.landedCostCodeID, Equal<LandedCostCode.landedCostCodeID>>>>, Where<POLandedCostDoc.released, Equal<True>, And<POLandedCostDetailS.aPRefNbr, IsNull, And2<Where<POLandedCostDoc.vendorID, Equal<Current<PX.Objects.AP.APRegister.vendorID>>, Or<FeatureInstalled<FeaturesSet.vendorRelations>>>, And2<Where<POLandedCostDoc.vendorLocationID, Equal<Current<PX.Objects.AP.APRegister.vendorLocationID>>, Or<FeatureInstalled<FeaturesSet.vendorRelations>>>, And2<Where<POLandedCostDoc.payToVendorID, Equal<Current<PX.Objects.AP.APRegister.vendorID>>, Or<Not<FeatureInstalled<FeaturesSet.vendorRelations>>>>, And<POLandedCostDoc.curyID, Equal<Current<PX.Objects.AP.APRegister.curyID>>>>>>>>> LinkLineLandedCostDetail;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POLineS, LeftJoin<PX.Objects.PO.POOrder, On<POLineS.orderNbr, Equal<PX.Objects.PO.POOrder.orderNbr>, And<POLineS.orderType, Equal<PX.Objects.PO.POOrder.orderType>>>>, Where<POLineS.pOAccrualType, Equal<POAccrualType.order>, And<POLineS.orderNbr, Equal<Required<POLineS.orderNbr>>, And<POLineS.orderType, Equal<Required<POLineS.orderType>>, And<POLineS.lineNbr, Equal<Required<POLineS.lineNbr>>>>>>> POLineLink;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POReceiptLineS, LeftJoin<PX.Objects.PO.POReceipt, On<POReceiptLineS.FK.Receipt>>, Where<POReceiptLineS.receiptType, Equal<Required<LinkLineReceipt.receiptType>>, And<POReceiptLineS.receiptNbr, Equal<Required<LinkLineReceipt.receiptNbr>>, And<POReceiptLineS.lineNbr, Equal<Required<LinkLineReceipt.receiptLineNbr>>>>>> ReceipLineLinked;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POReceiptLineS, LeftJoin<PX.Objects.PO.POReceipt, On<POReceiptLineS.FK.Receipt>>> linkLineReceiptTran;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POLineS, LeftJoin<PX.Objects.PO.POOrder, On<POLineS.orderNbr, Equal<PX.Objects.PO.POOrder.orderNbr>, And<POLineS.orderType, Equal<PX.Objects.PO.POOrder.orderType>>>>> linkLineOrderTran;
  public PXFilter<LinkLineFilter> linkLineFilter;
  public PXFilter<POReceiptFilter> filter;
  public PXAction<PX.Objects.AP.APInvoice> linkLine;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.linkLineReceiptTran).Cache.AllowDelete = false;
    ((PXSelectBase) this.linkLineReceiptTran).Cache.AllowInsert = false;
    ((PXSelectBase) this.linkLineOrderTran).Cache.AllowDelete = false;
    ((PXSelectBase) this.linkLineOrderTran).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.linkLineReceiptTran).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<POReceiptLineS.selected>(((PXSelectBase) this.linkLineReceiptTran).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.linkLineOrderTran).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<LinkLineOrder.selected>(((PXSelectBase) this.linkLineOrderTran).Cache, (object) null, true);
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LinkLine(PXAdapter adapter)
  {
    this.Base.checkTaxCalcMode();
    if (((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Current != null && ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Current.InventoryID.HasValue)
    {
      ((PXSelectBase) this.Base.Transactions).Cache.ClearQueryCache();
      WebDialogResult webDialogResult;
      // ISSUE: method pointer
      if ((webDialogResult = ((PXSelectBase<LinkLineFilter>) this.linkLineFilter).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CLinkLine\u003Eb__9_0)), true)) != null && webDialogResult == 6 && (((PXSelectBase) this.linkLineReceiptTran).Cache.Updated.Count() > 0L || ((PXSelectBase) this.linkLineOrderTran).Cache.Updated.Count() > 0L))
      {
        PX.Objects.AP.APTran current = ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Current;
        current.ReceiptType = (string) null;
        current.ReceiptNbr = (string) null;
        current.ReceiptLineNbr = new int?();
        current.POOrderType = (string) null;
        current.PONbr = (string) null;
        current.POLineNbr = new int?();
        current.POAccrualType = (string) null;
        current.POAccrualRefNoteID = new Guid?();
        current.POAccrualLineNbr = new int?();
        current.AccountID = new int?();
        current.SubID = new int?();
        current.SiteID = new int?();
        if (((PXSelectBase<LinkLineFilter>) this.linkLineFilter).Current.SelectedMode == "R")
        {
          foreach (POReceiptLineS poReceiptLineS in ((PXSelectBase) this.linkLineReceiptTran).Cache.Updated)
          {
            if (poReceiptLineS.Selected.GetValueOrDefault())
            {
              poReceiptLineS.SetReferenceKeyTo(current);
              current.BranchID = poReceiptLineS.BranchID;
              current.LineType = poReceiptLineS.LineType;
              current.AccountID = poReceiptLineS.POAccrualAcctID ?? poReceiptLineS.ExpenseAcctID;
              current.SubID = poReceiptLineS.POAccrualSubID ?? poReceiptLineS.ExpenseSubID;
              current.SiteID = poReceiptLineS.SiteID;
              if (current.POOrderType == "PD")
              {
                current.ProjectID = poReceiptLineS.ProjectID;
                current.TaskID = poReceiptLineS.TaskID;
                current.CostCodeID = poReceiptLineS.CostCodeID;
                current.DropshipExpenseRecording = poReceiptLineS.DropshipExpenseRecording;
                break;
              }
              break;
            }
          }
        }
        if (((PXSelectBase<LinkLineFilter>) this.linkLineFilter).Current.SelectedMode == "O")
        {
          foreach (POLineS poLineS in ((PXSelectBase) this.linkLineOrderTran).Cache.Updated)
          {
            if (poLineS.Selected.GetValueOrDefault())
            {
              poLineS.SetReferenceKeyTo(current);
              current.BranchID = poLineS.BranchID;
              current.LineType = poLineS.LineType;
              current.AccountID = poLineS.POAccrualAcctID ?? poLineS.ExpenseAcctID;
              current.SubID = poLineS.POAccrualSubID ?? poLineS.ExpenseSubID;
              current.SiteID = poLineS.SiteID;
              if (current.POOrderType == "PD")
              {
                current.ProjectID = poLineS.ProjectID;
                current.TaskID = poLineS.TaskID;
                current.CostCodeID = poLineS.CostCodeID;
                current.DropshipExpenseRecording = poLineS.DropshipExpenseRecording;
                break;
              }
              break;
            }
          }
        }
        ((PXSelectBase) this.Base.Transactions).Cache.Update((object) current);
        if (string.IsNullOrEmpty(current.ReceiptNbr) && string.IsNullOrEmpty(current.PONbr))
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetDefaultExt<PX.Objects.AP.APTran.accountID>((object) current);
          ((PXSelectBase) this.Base.Transactions).Cache.SetDefaultExt<PX.Objects.AP.APTran.subID>((object) current);
        }
      }
    }
    else if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current != null && ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.LCEnabled.GetValueOrDefault())
    {
      ((PXSelectBase) this.Base.Transactions).Cache.ClearQueryCache();
      WebDialogResult webDialogResult;
      // ISSUE: method pointer
      if ((webDialogResult = ((PXSelectBase<LinkLineFilter>) this.linkLineFilter).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CLinkLine\u003Eb__9_1)), true)) != null && webDialogResult == 6 && ((PXSelectBase) this.LinkLineLandedCostDetail).Cache.Updated.Count() > 0L)
      {
        PX.Objects.AP.APTran current = ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Current;
        foreach (POLandedCostDetailS detail in ((PXSelectBase) this.LinkLineLandedCostDetail).Cache.Updated)
        {
          if (detail.Selected.GetValueOrDefault())
          {
            this.Base.LinkLandedCostDetailLine(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, current, detail);
            break;
          }
        }
      }
    }
    return adapter.Get();
  }

  protected virtual void APInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AP.APInvoice row))
      return;
    APInvoiceState documentState = this.Base.GetDocumentState(cache, row);
    ((PXAction) this.linkLine).SetVisible(documentState.IsDocumentInvoice);
    ((PXAction) this.linkLine).SetEnabled(documentState.IsDocumentEditable && !documentState.IsDocumentScheduled && !documentState.IsRetainageDebAdj);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.AP.APTran, PX.Objects.AP.APTran.qty> e)
  {
    PX.Objects.AP.APTran row = e.Row;
    if (row == null)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.AP.APTran, PX.Objects.AP.APTran.qty>, PX.Objects.AP.APTran, object>) e).NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() < num & newValue.HasValue && (!string.IsNullOrEmpty(row.PONbr) || !string.IsNullOrEmpty(row.ReceiptNbr)) && row.POOrderType != "RS")
      throw new PXSetPropertyException((IBqlTable) row, "Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  public virtual IEnumerable LinkLineReceiptTran()
  {
    LinkLineExtension linkLineExtension = this;
    PX.Objects.AP.APTran currentAPTran = ((PXSelectBase<PX.Objects.AP.APTran>) linkLineExtension.Base.Transactions).Current;
    if (currentAPTran != null)
    {
      POAccrualComparer comparer = new POAccrualComparer();
      HashSet<PX.Objects.AP.APTran> usedSourceLine = new HashSet<PX.Objects.AP.APTran>((IEqualityComparer<PX.Objects.AP.APTran>) comparer);
      HashSet<PX.Objects.AP.APTran> unusedSourceLine = new HashSet<PX.Objects.AP.APTran>((IEqualityComparer<PX.Objects.AP.APTran>) comparer);
      foreach (PX.Objects.AP.APTran apTran in ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.Inserted)
      {
        int? inventoryId1 = currentAPTran.InventoryID;
        int? inventoryId2 = apTran.InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue && currentAPTran.UOM == apTran.UOM)
          usedSourceLine.Add(apTran);
      }
      foreach (PX.Objects.AP.APTran apTran in ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.Deleted)
      {
        int? inventoryId3 = currentAPTran.InventoryID;
        int? inventoryId4 = apTran.InventoryID;
        if (inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue && currentAPTran.UOM == apTran.UOM && ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetStatus((object) apTran) != 4 && !usedSourceLine.Remove(apTran))
          unusedSourceLine.Add(apTran);
      }
      foreach (PX.Objects.AP.APTran apTran1 in ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.Updated)
      {
        int? inventoryId5 = currentAPTran.InventoryID;
        int? inventoryId6 = apTran1.InventoryID;
        if (inventoryId5.GetValueOrDefault() == inventoryId6.GetValueOrDefault() & inventoryId5.HasValue == inventoryId6.HasValue && currentAPTran.UOM == apTran1.UOM)
        {
          PX.Objects.AP.APTran apTran2 = new PX.Objects.AP.APTran()
          {
            POAccrualType = (string) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualType>((object) apTran1),
            POAccrualRefNoteID = (Guid?) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualRefNoteID>((object) apTran1),
            POAccrualLineNbr = (int?) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualLineNbr>((object) apTran1),
            POOrderType = (string) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOOrderType>((object) apTran1),
            PONbr = (string) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pONbr>((object) apTran1),
            POLineNbr = (int?) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOLineNbr>((object) apTran1),
            ReceiptNbr = (string) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptNbr>((object) apTran1),
            ReceiptType = (string) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptType>((object) apTran1),
            ReceiptLineNbr = (int?) ((PXSelectBase) linkLineExtension.Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptLineNbr>((object) apTran1)
          };
          if (!usedSourceLine.Remove(apTran2))
            unusedSourceLine.Add(apTran2);
          if (!unusedSourceLine.Remove(apTran1))
            usedSourceLine.Add(apTran1);
        }
      }
      unusedSourceLine.Add(currentAPTran);
      PXSelectBase<LinkLineReceipt> pxSelectBase = (PXSelectBase<LinkLineReceipt>) new PXSelect<LinkLineReceipt, Where2<Where<Current<LinkLineFilter.pOOrderNbr>, Equal<LinkLineReceipt.orderNbr>, Or<Current<LinkLineFilter.pOOrderNbr>, IsNull>>, And2<Where<Current<LinkLineFilter.siteID>, IsNull, Or<LinkLineReceipt.receiptSiteID, Equal<Current<LinkLineFilter.siteID>>>>, And<LinkLineReceipt.inventoryID, Equal<Current<PX.Objects.AP.APTran.inventoryID>>, And<LinkLineReceipt.uOM, Equal<Current<PX.Objects.AP.APTran.uOM>>, And<LinkLineReceipt.receiptType, Equal<POReceiptType.poreceipt>>>>>>>((PXGraph) linkLineExtension.Base);
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
        pxSelectBase.WhereAnd<Where<LinkLineReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<LinkLineReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<Where<LinkLineReceipt.payToVendorID, IsNull, Or<LinkLineReceipt.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>>>();
      else
        pxSelectBase.WhereAnd<Where<LinkLineReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<LinkLineReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>>>>();
      foreach (PXResult<LinkLineReceipt> pxResult1 in pxSelectBase.Select(Array.Empty<object>()))
      {
        LinkLineReceipt linkLineReceipt = PXResult<LinkLineReceipt>.op_Implicit(pxResult1);
        if (!usedSourceLine.Contains(new PX.Objects.AP.APTran()
        {
          POAccrualType = linkLineReceipt.POAccrualType,
          POAccrualRefNoteID = linkLineReceipt.POAccrualRefNoteID,
          POAccrualLineNbr = linkLineReceipt.POAccrualLineNbr,
          POOrderType = linkLineReceipt.OrderType,
          PONbr = linkLineReceipt.OrderNbr,
          POLineNbr = linkLineReceipt.OrderLineNbr,
          ReceiptType = linkLineReceipt.ReceiptType,
          ReceiptNbr = linkLineReceipt.ReceiptNbr,
          ReceiptLineNbr = linkLineReceipt.ReceiptLineNbr
        }))
        {
          PXResult<POReceiptLineS, PX.Objects.PO.POReceipt> pxResult2 = (PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>) PXResultset<POReceiptLineS>.op_Implicit(((PXSelectBase<POReceiptLineS>) linkLineExtension.ReceipLineLinked).Select(new object[3]
          {
            (object) linkLineReceipt.ReceiptType,
            (object) linkLineReceipt.ReceiptNbr,
            (object) linkLineReceipt.ReceiptLineNbr
          }));
          if (((PXSelectBase) linkLineExtension.linkLineReceiptTran).Cache.GetStatus((object) PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult2)) != 1 && PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult2).CompareReferenceKey(currentAPTran))
          {
            ((PXSelectBase) linkLineExtension.linkLineReceiptTran).Cache.SetValue<POReceiptLineS.selected>((object) PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult2), (object) true);
            GraphHelper.MarkUpdated(((PXSelectBase) linkLineExtension.linkLineReceiptTran).Cache, (object) PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult2), true);
          }
          yield return (object) pxResult2;
        }
      }
      foreach (PX.Objects.AP.APTran apTran in unusedSourceLine.Where<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, bool>) (t => t.POAccrualType != null)))
      {
        APInvoiceEntry apInvoiceEntry = linkLineExtension.Base;
        object[] objArray = new object[3]
        {
          (object) apTran.POAccrualType,
          (object) apTran.POAccrualRefNoteID,
          (object) apTran.POAccrualLineNbr
        };
        foreach (PXResult<POReceiptLineS, PX.Objects.PO.POReceipt> pxResult in PXSelectBase<POReceiptLineS, PXSelectJoin<POReceiptLineS, LeftJoin<PX.Objects.PO.POReceipt, On<POReceiptLineS.FK.Receipt>>, Where<POReceiptLineS.pOAccrualType, Equal<Required<LinkLineReceipt.pOAccrualType>>, And<POReceiptLineS.pOAccrualRefNoteID, Equal<Required<LinkLineReceipt.pOAccrualRefNoteID>>, And<POReceiptLineS.pOAccrualLineNbr, Equal<Required<LinkLineReceipt.pOAccrualLineNbr>>>>>>.Config>.Select((PXGraph) apInvoiceEntry, objArray))
        {
          int? inventoryId7 = currentAPTran.InventoryID;
          int? inventoryId8 = PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult).InventoryID;
          if (inventoryId7.GetValueOrDefault() == inventoryId8.GetValueOrDefault() & inventoryId7.HasValue == inventoryId8.HasValue)
          {
            if (((PXSelectBase) linkLineExtension.linkLineReceiptTran).Cache.GetStatus((object) PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult)) != 1 && PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult).CompareReferenceKey(currentAPTran))
            {
              ((PXSelectBase) linkLineExtension.linkLineReceiptTran).Cache.SetValue<POReceiptLineS.selected>((object) PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult), (object) true);
              GraphHelper.MarkUpdated(((PXSelectBase) linkLineExtension.linkLineReceiptTran).Cache, (object) PXResult<POReceiptLineS, PX.Objects.PO.POReceipt>.op_Implicit(pxResult), true);
            }
            yield return (object) pxResult;
          }
        }
      }
    }
  }

  public virtual IEnumerable LinkLineOrderTran()
  {
    LinkLineExtension linkLineExtension = this;
    PX.Objects.AP.APTran currentAPTran = ((PXSelectBase<PX.Objects.AP.APTran>) linkLineExtension.Base.Transactions).Current;
    if (currentAPTran != null)
    {
      PXSelectBase<LinkLineOrder> pxSelectBase = (PXSelectBase<LinkLineOrder>) new PXSelect<LinkLineOrder, Where2<Where<Current<LinkLineFilter.pOOrderNbr>, Equal<LinkLineOrder.orderNbr>, Or<Current<LinkLineFilter.pOOrderNbr>, IsNull>>, And2<Where<Current<LinkLineFilter.siteID>, IsNull, Or<LinkLineOrder.orderSiteID, Equal<Current<LinkLineFilter.siteID>>>>, And<LinkLineOrder.inventoryID, Equal<Current<PX.Objects.AP.APTran.inventoryID>>, And<LinkLineOrder.uOM, Equal<Current<PX.Objects.AP.APTran.uOM>>, And<LinkLineOrder.orderCuryID, Equal<Current<PX.Objects.AP.APInvoice.curyID>>>>>>>>((PXGraph) linkLineExtension.Base);
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
        pxSelectBase.WhereAnd<Where<LinkLineOrder.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<LinkLineOrder.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<LinkLineOrder.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>();
      else
        pxSelectBase.WhereAnd<Where<LinkLineOrder.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<LinkLineOrder.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>>>>();
      Lazy<POAccrualSet> usedPOAccrual = new Lazy<POAccrualSet>((Func<POAccrualSet>) (() =>
      {
        POAccrualSet usedPoAccrualSet = this.Base.GetUsedPOAccrualSet();
        usedPoAccrualSet.Remove(currentAPTran);
        return usedPoAccrualSet;
      }));
      foreach (LinkLineOrder linkLineOrder in GraphHelper.RowCast<LinkLineOrder>((IEnumerable) pxSelectBase.Select(Array.Empty<object>())).AsEnumerable<LinkLineOrder>().Where<LinkLineOrder>((Func<LinkLineOrder, bool>) (l => !usedPOAccrual.Value.Contains(l))))
      {
        PXResult<POLineS, PX.Objects.PO.POOrder> pxResult = (PXResult<POLineS, PX.Objects.PO.POOrder>) PXResultset<POLineS>.op_Implicit(((PXSelectBase<POLineS>) linkLineExtension.POLineLink).Select(new object[3]
        {
          (object) linkLineOrder.OrderNbr,
          (object) linkLineOrder.OrderType,
          (object) linkLineOrder.OrderLineNbr
        }));
        if (((PXSelectBase) linkLineExtension.linkLineOrderTran).Cache.GetStatus((object) PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult)) != 1 && PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult).CompareReferenceKey(currentAPTran))
        {
          ((PXSelectBase) linkLineExtension.linkLineOrderTran).Cache.SetValue<POLineS.selected>((object) PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult), (object) true);
          GraphHelper.MarkUpdated(((PXSelectBase) linkLineExtension.linkLineOrderTran).Cache, (object) PXResult<POLineS, PX.Objects.PO.POOrder>.op_Implicit(pxResult), true);
        }
        yield return (object) pxResult;
      }
    }
  }

  protected virtual IEnumerable linkLineLandedCostDetail()
  {
    IEnumerable enumerable = GraphHelper.QuickSelect((PXGraph) this.Base, ((PXSelectBase) this.LinkLineLandedCostDetail).View.BqlSelect);
    foreach (POLandedCostDetail landedCostDetail in GraphHelper.RowCast<POLandedCostDetail>(((PXCache) GraphHelper.Caches<POLandedCostDetail>((PXGraph) this.Base)).Updated))
    {
      POLandedCostDetail poLandedCostDetail = landedCostDetail;
      POLandedCostDetailS landedCostDetailS = GraphHelper.RowCast<POLandedCostDetailS>(enumerable).SingleOrDefault<POLandedCostDetailS>((Func<POLandedCostDetailS, bool>) (t =>
      {
        if (!(t.DocType == poLandedCostDetail.DocType) || !(t.RefNbr == poLandedCostDetail.RefNbr))
          return false;
        int? lineNbr1 = t.LineNbr;
        int? lineNbr2 = poLandedCostDetail.LineNbr;
        return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
      }));
      if (landedCostDetailS != null)
      {
        landedCostDetailS.APDocType = poLandedCostDetail.APDocType;
        landedCostDetailS.APRefNbr = poLandedCostDetail.APRefNbr;
      }
    }
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current != null)
      enumerable = (IEnumerable) GraphHelper.RowCast<POLandedCostDetailS>(enumerable).Where<POLandedCostDetailS>((Func<POLandedCostDetailS, bool>) (t => t.APRefNbr != ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.RefNbr));
    return enumerable;
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<LinkLineFilter.selectedMode> e)
  {
    if (!(e.Row is LinkLineFilter row))
      return;
    bool hasValue = ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Current.InventoryID.HasValue;
    if (object.Equals(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<LinkLineFilter.selectedMode>, object, object>) e).NewValue, (object) "L") & hasValue)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<LinkLineFilter.selectedMode>, object, object>) e).NewValue = (object) row.SelectedMode;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<LinkLineFilter.selectedMode>>) e).Cancel = true;
    }
    if (hasValue)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<LinkLineFilter.selectedMode>, object, object>) e).NewValue = (object) "L";
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<LinkLineFilter.selectedMode>>) e).Cancel = true;
  }

  public virtual void LinkLineFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    LinkLineFilter row = (LinkLineFilter) e.Row;
    if (row == null)
      return;
    PXCache cache = ((PXSelectBase) this.linkLineReceiptTran).Cache;
    ((PXSelectBase) this.linkLineReceiptTran).View.AllowSelect = row.SelectedMode == "R";
    ((PXSelectBase) this.linkLineOrderTran).View.AllowSelect = row.SelectedMode == "O";
    if (row.SelectedMode == "L")
      ((PXSelectBase) this.LinkLineLandedCostDetail).View.AllowSelect = true;
    else
      ((PXSelectBase) this.LinkLineLandedCostDetail).View.AllowSelect = false;
  }
}
