// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddPOReceiptLineExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO.DAC.Projections;
using PX.Objects.PO.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;

/// <summary>
/// This class implements graph extension to use special dialogs called Smart Panel to perform "ADD PO RECEIPT LINE" (Screen AP301000)
/// </summary>
[Serializable]
public class AddPOReceiptLineExtension : PXGraphExtension<LinkLineExtension, APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<POReceiptLineAdd> poReceiptLinesSelection;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POReceiptLineAdd, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptLineS.receiptType>, And<PX.Objects.PO.POReceipt.receiptNbr, Equal<POReceiptLineS.receiptNbr>>>>, Where<POReceiptLineS.receiptType, Equal<Required<LinkLineReceipt.receiptType>>, And<POReceiptLineS.receiptNbr, Equal<Required<LinkLineReceipt.receiptNbr>>, And<POReceiptLineS.lineNbr, Equal<Required<LinkLineReceipt.receiptLineNbr>>, And<PX.Objects.PO.POReceipt.canceled, Equal<False>, And<PX.Objects.PO.POReceipt.isUnderCorrection, Equal<False>>>>>>> ReceipLineAdd;
  public PXAction<PX.Objects.AP.APInvoice> addReceiptLine;
  public PXAction<PX.Objects.AP.APInvoice> addReceiptLine2;
  public PXAction<PX.Objects.AP.APInvoice> viewPOOrderReceiptLinesSelection;
  public PXAction<PX.Objects.AP.APInvoice> viewReceiptReceiptLinesSelection;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.poReceiptLinesSelection).Cache.AllowDelete = false;
    ((PXSelectBase) this.poReceiptLinesSelection).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled<PX.Objects.PO.POReceiptLine.subItemID>(((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base).Caches[typeof (PX.Objects.PO.POReceiptLine)], (object) null, false);
    ((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base).Views.Caches.Remove(typeof (POOrderRS));
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddReceiptLine(PXAdapter adapter)
  {
    ((PXGraphExtension<APInvoiceEntry>) this).Base.checkTaxCalcMode();
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null)
    {
      bool? released = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released;
      bool flag1 = false;
      if (released.GetValueOrDefault() == flag1 & released.HasValue)
      {
        bool? prebooked = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Prebooked;
        bool flag2 = false;
        // ISSUE: method pointer
        if (prebooked.GetValueOrDefault() == flag2 & prebooked.HasValue && ((PXSelectBase<POReceiptLineAdd>) this.poReceiptLinesSelection).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddReceiptLine\u003Eb__6_0)), true) == 1)
        {
          ((PXGraphExtension<APInvoiceEntry>) this).Base.updateTaxCalcMode();
          return this.AddReceiptLine2(adapter);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddReceiptLine2(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null)
    {
      bool? nullable1 = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released;
      bool flag1 = false;
      if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
      {
        nullable1 = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Prebooked;
        bool flag2 = false;
        if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
        {
          POAccrualSet usedPoAccrualSet = ((PXGraphExtension<APInvoiceEntry>) this).Base.GetUsedPOAccrualSet();
          HashSet<string> ordersWithDiscounts = new HashSet<string>();
          foreach (PXResult<POReceiptLineAdd> pxResult in ((PXSelectBase<POReceiptLineAdd>) this.poReceiptLinesSelection).Select(Array.Empty<object>()))
          {
            POReceiptLineAdd aLine = PXResult<POReceiptLineAdd>.op_Implicit(pxResult);
            nullable1 = aLine.Selected;
            if (nullable1.GetValueOrDefault())
            {
              Decimal? nullable2 = aLine.RetainagePct;
              if (nullable2.GetValueOrDefault() != 0M)
                ((PXGraphExtension<APInvoiceEntry>) this).Base.EnableRetainage();
              ((PXGraphExtension<APInvoiceEntry>) this).Base.AddPOReceiptLine((IAPTranSource) aLine, (HashSet<PX.Objects.AP.APTran>) usedPoAccrualSet);
              if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null)
              {
                nullable2 = aLine.DocumentDiscountRate;
                if (nullable2.HasValue)
                {
                  nullable2 = aLine.GroupDiscountRate;
                  Decimal num1 = (Decimal) 1;
                  if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
                  {
                    nullable2 = aLine.DocumentDiscountRate;
                    Decimal num2 = (Decimal) 1;
                    if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                      continue;
                  }
                  ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.SetWarningOnDiscount = new bool?(true);
                  ordersWithDiscounts.Add(aLine.PONbr);
                }
              }
            }
          }
          ((PXGraphExtension<APInvoiceEntry>) this).Base.AutoRecalculateDiscounts();
          ((PXGraphExtension<APInvoiceEntry>) this).Base.WritePODiscountWarningToTrace(((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current, ordersWithDiscounts);
        }
      }
    }
    ((PXSelectBase) this.poReceiptLinesSelection).View.Clear();
    ((PXSelectBase) this.poReceiptLinesSelection).Cache.Clear();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual void ViewPOOrderReceiptLinesSelection()
  {
    POReceiptLineAdd current = ((PXSelectBase<POReceiptLineAdd>) this.poReceiptLinesSelection).Current;
    if (current == null)
      return;
    PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, current.POType, current.PONbr) ?? throw new PXException("The {0} {1} purchase order was not found.", new object[2]
    {
      (object) current.POType,
      (object) current.PONbr
    });
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<POOrderEntry>(), (object) poOrder, (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual void ViewReceiptReceiptLinesSelection()
  {
    POReceiptLineAdd current = ((PXSelectBase<POReceiptLineAdd>) this.poReceiptLinesSelection).Current;
    if (current == null)
      return;
    PX.Objects.PO.POReceipt poReceipt = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, current.ReceiptType, current.ReceiptNbr) ?? throw new PXException("Document {0} {1} cannot be found in the system.", new object[2]
    {
      (object) current.ReceiptType,
      (object) current.ReceiptNbr
    });
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<POReceiptEntry>(), (object) poReceipt, (PXRedirectHelper.WindowMode) 3);
  }

  protected virtual void APInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AP.APInvoice row))
      return;
    APInvoiceState documentState = ((PXGraphExtension<APInvoiceEntry>) this).Base.GetDocumentState(cache, row);
    ((PXAction) this.addReceiptLine).SetVisible(documentState.IsDocumentInvoice);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poReceiptLinesSelection).Cache, (string) null, false);
    bool flag = documentState.IsDocumentEditable && !documentState.IsDocumentScheduled && ((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<APInvoiceEntry>) this).Base.vendor).Current != null && !documentState.IsRetainageDebAdj;
    ((PXAction) this.addReceiptLine).SetEnabled(flag);
    PXUIFieldAttribute.SetEnabled<POReceiptLineS.selected>(((PXSelectBase) this.poReceiptLinesSelection).Cache, (object) null, flag);
  }

  public virtual IEnumerable POReceiptLinesSelection()
  {
    AddPOReceiptLineExtension receiptLineExtension = this;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Document).Current;
    if ((current != null ? (!current.VendorID.HasValue ? 1 : 0) : 1) == 0 && current.VendorLocationID.HasValue && (!(current.DocType != "INV") || !(current.DocType != "ADR")))
    {
      string str = current.DocType == "INV" ? "RT" : "RN";
      POAccrualComparer comparer = new POAccrualComparer();
      HashSet<PX.Objects.AP.APTran> usedSourceLine = new HashSet<PX.Objects.AP.APTran>((IEqualityComparer<PX.Objects.AP.APTran>) comparer);
      HashSet<PX.Objects.AP.APTran> unusedSourceLine = new HashSet<PX.Objects.AP.APTran>((IEqualityComparer<PX.Objects.AP.APTran>) comparer);
      foreach (PX.Objects.AP.APTran apTran in ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.Inserted)
        usedSourceLine.Add(apTran);
      foreach (PX.Objects.AP.APTran apTran in ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.Deleted)
      {
        if (((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetStatus((object) apTran) != 4 && !usedSourceLine.Remove(apTran))
          unusedSourceLine.Add(apTran);
      }
      foreach (PX.Objects.AP.APTran apTran1 in ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.Updated)
      {
        PX.Objects.AP.APTran apTran2 = new PX.Objects.AP.APTran()
        {
          POAccrualType = (string) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualType>((object) apTran1),
          POAccrualRefNoteID = (Guid?) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualRefNoteID>((object) apTran1),
          POAccrualLineNbr = (int?) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOAccrualLineNbr>((object) apTran1),
          POOrderType = (string) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOOrderType>((object) apTran1),
          PONbr = (string) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pONbr>((object) apTran1),
          POLineNbr = (int?) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.pOLineNbr>((object) apTran1),
          ReceiptNbr = (string) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptNbr>((object) apTran1),
          ReceiptType = (string) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptType>((object) apTran1),
          ReceiptLineNbr = (int?) ((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base.Transactions).Cache.GetValueOriginal<PX.Objects.AP.APTran.receiptLineNbr>((object) apTran1)
        };
        if (!usedSourceLine.Remove(apTran2))
          unusedSourceLine.Add(apTran2);
        if (!unusedSourceLine.Remove(apTran1))
          usedSourceLine.Add(apTran1);
      }
      PXSelectBase<LinkLineReceipt> pxSelectBase = (PXSelectBase<LinkLineReceipt>) new PXSelect<LinkLineReceipt, Where<LinkLineReceipt.receiptType, Equal<Required<PX.Objects.PO.POReceipt.receiptType>>, And2<Where<LinkLineReceipt.orderNbr, Equal<Current<POReceiptFilter.orderNbr>>, Or<Current<POReceiptFilter.orderNbr>, IsNull>>, And2<Where<LinkLineReceipt.receiptLineNbr, Equal<Current<POReceiptFilter.receiptLineNbr>>, Or<Current<POReceiptFilter.receiptLineNbr>, IsNull>>, And<Where<LinkLineReceipt.receiptNbr, Equal<Current<POReceiptFilter.receiptNbr>>, Or<Current<POReceiptFilter.receiptNbr>, IsNull>>>>>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base);
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
        pxSelectBase.WhereAnd<Where<LinkLineReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<LinkLineReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<Where<LinkLineReceipt.payToVendorID, IsNull, Or<LinkLineReceipt.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>>>();
      else
        pxSelectBase.WhereAnd<Where<LinkLineReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<LinkLineReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>>>>();
      PXView view = ((PXSelectBase) pxSelectBase).View;
      object[] objArray1 = new object[1]{ (object) current };
      object[] objArray2 = new object[1]{ (object) str };
      foreach (LinkLineReceipt linkLineReceipt in view.SelectMultiBound(objArray1, objArray2))
      {
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
          PXResult<POReceiptLineAdd, PX.Objects.PO.POReceipt> pxResult = (PXResult<POReceiptLineAdd, PX.Objects.PO.POReceipt>) PXResultset<POReceiptLineAdd>.op_Implicit(((PXSelectBase<POReceiptLineAdd>) receiptLineExtension.ReceipLineAdd).Select(new object[3]
          {
            (object) linkLineReceipt.ReceiptType,
            (object) linkLineReceipt.ReceiptNbr,
            (object) linkLineReceipt.ReceiptLineNbr
          }));
          if (pxResult != null)
            yield return (object) pxResult;
        }
      }
      foreach (PX.Objects.AP.APTran apTran in unusedSourceLine.Where<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, bool>) (t => t.POAccrualType != null)))
      {
        APInvoiceEntry apInvoiceEntry = ((PXGraphExtension<APInvoiceEntry>) receiptLineExtension).Base;
        object[] objArray3 = new object[3]
        {
          (object) apTran.POAccrualType,
          (object) apTran.POAccrualRefNoteID,
          (object) apTran.POAccrualLineNbr
        };
        foreach (PXResult<POReceiptLineAdd, PX.Objects.PO.POReceipt> pxResult in PXSelectBase<POReceiptLineAdd, PXSelectJoin<POReceiptLineAdd, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptLineS.receiptType>, And<PX.Objects.PO.POReceipt.receiptNbr, Equal<POReceiptLineS.receiptNbr>>>>, Where<POReceiptLineS.pOAccrualType, Equal<Required<LinkLineReceipt.pOAccrualType>>, And<POReceiptLineS.pOAccrualRefNoteID, Equal<Required<LinkLineReceipt.pOAccrualRefNoteID>>, And<POReceiptLineS.pOAccrualLineNbr, Equal<Required<LinkLineReceipt.pOAccrualLineNbr>>, And<POReceiptLineS.unbilledQty, Greater<decimal0>>>>>>.Config>.Select((PXGraph) apInvoiceEntry, objArray3))
          yield return (object) pxResult;
      }
    }
  }

  public virtual void POReceiptLineAdd_ReceiptNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    POReceiptLineAdd row = (POReceiptLineAdd) e.Row;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    if (row == null || current == null)
      return;
    PX.Objects.PO.POOrder poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
    {
      (object) row.POType,
      (object) row.PONbr
    }));
    if (poOrder == null || !(poOrder.CuryID != current.CuryID))
      return;
    string name = typeof (PX.Objects.PO.POReceipt.curyID).Name;
    PXErrorLevel pxErrorLevel = (PXErrorLevel) 3;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, name, (string) null, (string) null, "The currency of the source document is different from the one of this document. The value may be recalculated or require correction.", pxErrorLevel, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.IsAltered = true;
  }

  public virtual void POReceiptLineS_ReceiptNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    POReceiptLineS row = (POReceiptLineS) e.Row;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    if (row == null || current == null)
      return;
    PX.Objects.PO.POOrder poOrder = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(PXSelectBase<PX.Objects.PO.POOrder, PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[2]
    {
      (object) row.POType,
      (object) row.PONbr
    }));
    if (poOrder == null || !(poOrder.CuryID != current.CuryID))
      return;
    string name = typeof (PX.Objects.PO.POReceipt.curyID).Name;
    PXErrorLevel pxErrorLevel = (PXErrorLevel) 3;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, name, (string) null, (string) null, "The currency of the source document is different from the one of this document. The value may be recalculated or require correction.", pxErrorLevel, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.IsAltered = true;
  }

  public virtual void POReceiptLineS_Selected_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POReceiptLineS row = (POReceiptLineS) e.Row;
    if (row == null || (bool) e.OldValue)
      return;
    bool? selected = row.Selected;
    if (!selected.Value)
      return;
    foreach (POReceiptLineS poReceiptLineS in ((PXSelectBase) this.Base1.linkLineReceiptTran).Cache.Updated)
    {
      selected = poReceiptLineS.Selected;
      if (selected.GetValueOrDefault() && poReceiptLineS != row)
      {
        sender.SetValue<POReceiptLineS.selected>((object) poReceiptLineS, (object) false);
        ((PXSelectBase) this.Base1.linkLineReceiptTran).View.RequestRefresh();
      }
    }
    foreach (POLineS poLineS in ((PXSelectBase) this.Base1.linkLineOrderTran).Cache.Updated)
    {
      if (poLineS.Selected.GetValueOrDefault())
      {
        ((PXSelectBase) this.Base1.linkLineOrderTran).Cache.SetValue<POLineS.selected>((object) poLineS, (object) false);
        ((PXSelectBase) this.Base1.linkLineOrderTran).View.RequestRefresh();
      }
    }
  }

  public virtual void POReceiptLineS_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    sender.IsDirty = false;
  }

  public virtual void POReceiptLineS_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }
}
