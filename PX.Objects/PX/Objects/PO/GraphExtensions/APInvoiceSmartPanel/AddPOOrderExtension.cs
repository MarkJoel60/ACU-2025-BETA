// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddPOOrderExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;

/// <summary>
/// This class implements graph extension to use special dialogs called Smart Panel to perform "ADD PO" (Screen AP301000)
/// </summary>
[Serializable]
public class AddPOOrderExtension : PXGraphExtension<LinkLineExtension, APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<POOrderRS> poorderslist;
  public PXAction<PX.Objects.AP.APInvoice> viewPOOrderLinkLineReceiptTran;
  public PXAction<PX.Objects.AP.APInvoice> viewReceiptLinkLineReceiptTran;
  public PXAction<PX.Objects.AP.APInvoice> addPOOrder;
  public PXAction<PX.Objects.AP.APInvoice> addPOOrder2;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.poorderslist).Cache.AllowDelete = false;
    ((PXSelectBase) this.poorderslist).Cache.AllowInsert = false;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual void ViewPOOrderLinkLineReceiptTran()
  {
    POReceiptLineS current = ((PXSelectBase<POReceiptLineS>) this.Base1.linkLineReceiptTran).Current;
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
  protected virtual void ViewReceiptLinkLineReceiptTran()
  {
    POReceiptLineS current = ((PXSelectBase<POReceiptLineS>) this.Base1.linkLineReceiptTran).Current;
    if (current == null)
      return;
    PX.Objects.PO.POReceipt poReceipt = PX.Objects.PO.POReceipt.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, current.ReceiptType, current.ReceiptNbr) ?? throw new PXException("Document {0} {1} cannot be found in the system.", new object[2]
    {
      (object) current.ReceiptType,
      (object) current.ReceiptNbr
    });
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<POReceiptEntry>(), (object) poReceipt, (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddPOOrder(PXAdapter adapter)
  {
    ((PXGraphExtension<APInvoiceEntry>) this).Base.checkTaxCalcMode();
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null && EnumerableExtensions.IsIn<string>(((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.DocType, "INV", "PPM"))
    {
      bool? nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Prebooked;
        bool flag2 = false;
        // ISSUE: method pointer
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && ((PXSelectBase<POOrderRS>) this.poorderslist).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddPOOrder\u003Eb__9_0)), true) == 1)
        {
          ((PXGraphExtension<APInvoiceEntry>) this).Base.updateTaxCalcMode();
          return this.AddPOOrder2(adapter);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddPOOrder2(PXAdapter adapter)
  {
    if (this.ShouldAddPOOrder())
    {
      using (new APInvoiceEntry.SkipUpdAdjustments(((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.DocType + ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.RefNbr))
      {
        List<PX.Objects.PO.POOrder> list = GraphHelper.RowCast<PX.Objects.PO.POOrder>(((PXSelectBase) this.poorderslist).Cache.Updated).Where<PX.Objects.PO.POOrder>((Func<PX.Objects.PO.POOrder, bool>) (rc => rc.Selected.GetValueOrDefault())).ToList<PX.Objects.PO.POOrder>();
        foreach (PX.Objects.PO.POOrder order in list)
          ((PXGraphExtension<APInvoiceEntry>) this).Base.InvoicePOOrder(order, false);
        ((PXGraphExtension<APInvoiceEntry>) this).Base.AttachPrepayment(list);
      }
    }
    return adapter.Get();
  }

  public virtual bool ShouldAddPOOrder()
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current != null && ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.DocType == "INV")
    {
      bool? nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.Prebooked;
        bool flag2 = false;
        return nullable.GetValueOrDefault() == flag2 & nullable.HasValue;
      }
    }
    return false;
  }

  protected virtual void APInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AP.APInvoice row))
      return;
    APInvoiceState documentState = ((PXGraphExtension<APInvoiceEntry>) this).Base.GetDocumentState(cache, row);
    ((PXAction) this.addPOOrder).SetVisible(documentState.IsDocumentInvoice);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poorderslist).Cache, (string) null, false);
    bool flag = documentState.IsDocumentEditable && !documentState.IsDocumentScheduled && ((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<APInvoiceEntry>) this).Base.vendor).Current != null && !documentState.IsRetainageDebAdj;
    ((PXAction) this.addPOOrder).SetEnabled(flag);
    PXUIFieldAttribute.SetEnabled<POOrderRS.selected>(((PXSelectBase) this.poorderslist).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<POOrderRS.unbilledOrderQty>(((PXSelectBase) this.poorderslist).Cache, (object) null, documentState.IsDocumentInvoice);
    PXUIFieldAttribute.SetVisible<POOrderRS.curyUnbilledOrderTotal>(((PXSelectBase) this.poorderslist).Cache, (object) null, documentState.IsDocumentInvoice);
  }

  public virtual IEnumerable pOOrderslist()
  {
    foreach (PXResult<POOrderRS, PX.Objects.PO.POLine> poOrder in this.GetPOOrderList())
      yield return (object) PXResult<POOrderRS, PX.Objects.PO.POLine>.op_Implicit(poOrder);
  }

  public virtual IEnumerable<PXResult<POOrderRS, PX.Objects.PO.POLine>> GetPOOrderList()
  {
    AddPOOrderExtension poOrderExtension = this;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) poOrderExtension).Base.Document).Current;
    bool flag1 = current.DocType == "INV";
    bool flag2 = current.DocType == "ADR";
    bool isPrepayment = current.DocType == "PPM";
    if ((current != null ? (!current.VendorID.HasValue ? 1 : 0) : 1) == 0 && current.VendorLocationID.HasValue && (flag1 || flag2 || isPrepayment))
    {
      Dictionary<PX.Objects.AP.APTran, int> usedOrderLines = new Dictionary<PX.Objects.AP.APTran, int>((IEqualityComparer<PX.Objects.AP.APTran>) new POOrderComparer());
      foreach (PX.Objects.AP.APTran key in GraphHelper.RowCast<PX.Objects.AP.APTran>((IEnumerable) ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) poOrderExtension).Base.Transactions).Select(Array.Empty<object>())).AsEnumerable<PX.Objects.AP.APTran>().Where<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, bool>) (t =>
      {
        if (string.IsNullOrEmpty(t.PONbr))
          return false;
        return isPrepayment || t.POAccrualType == "O";
      })))
      {
        int num;
        usedOrderLines.TryGetValue(key, out num);
        usedOrderLines[key] = num + 1;
      }
      PXSelectBase<POOrderRS> pxSelectBase = (PXSelectBase<POOrderRS>) new PXSelectJoinGroupBy<POOrderRS, InnerJoin<PX.Objects.PO.POLine, On<PX.Objects.PO.POLine.orderType, Equal<PX.Objects.PO.POOrder.orderType>, And<PX.Objects.PO.POLine.orderNbr, Equal<POOrderRS.orderNbr>>>>, Where<PX.Objects.PO.POOrder.orderType, NotIn3<POOrderType.blanket, POOrderType.standardBlanket>, And<PX.Objects.PO.POOrder.curyID, Equal<Current<PX.Objects.AP.APInvoice.curyID>>, And<PX.Objects.PO.POLine.cancelled, NotEqual<True>, And<NotExists<Select2<POOrderReceipt, InnerJoin<PX.Objects.PO.POReceipt, On<POOrderReceipt.FK.Receipt>>, Where<POOrderReceipt.pOType, Equal<PX.Objects.PO.POLine.orderType>, And<POOrderReceipt.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.PO.POReceipt.status, Equal<POReceiptStatus.underCorrection>>>>>>>>>>, Aggregate<GroupBy<PX.Objects.PO.POOrder.orderType, GroupBy<POOrderRS.orderNbr, GroupBy<PX.Objects.PO.POOrder.orderDate, GroupBy<PX.Objects.PO.POOrder.curyID, GroupBy<PX.Objects.PO.POOrder.curyOrderTotal, GroupBy<PX.Objects.PO.POOrder.hold, GroupBy<PX.Objects.PO.POOrder.cancelled, Sum<PX.Objects.PO.POLine.orderQty, Sum<PX.Objects.PO.POLine.curyExtCost, Sum<PX.Objects.PO.POLine.extCost, Count<PX.Objects.PO.POLine.lineNbr>>>>>>>>>>>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) poOrderExtension).Base);
      if (!flag2)
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POLine.closed, NotEqual<True>>>();
      if (!flag2)
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POOrder.status, In3<POOrderStatus.awaitingLink, POOrderStatus.open, POOrderStatus.completed>>>();
      else
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed, POOrderStatus.closed>>>();
      if (flag1 | flag2)
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POLine.pOAccrualType, Equal<POAccrualType.order>>>();
      else if (isPrepayment)
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POOrder.taxZoneID, Equal<Current<PX.Objects.AP.APInvoice.taxZoneID>>, Or<PX.Objects.PO.POOrder.taxZoneID, IsNull, And<Current<PX.Objects.AP.APInvoice.taxZoneID>, IsNull>>>>();
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POOrder.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<PX.Objects.PO.POOrder.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<PX.Objects.PO.POOrder.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>();
      else
        pxSelectBase.WhereAnd<Where<PX.Objects.PO.POOrder.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<PX.Objects.PO.POOrder.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>>>>();
      PXView view = ((PXSelectBase) pxSelectBase).View;
      object[] objArray1 = new object[1]{ (object) current };
      object[] objArray2 = Array.Empty<object>();
      foreach (PXResult<POOrderRS, PX.Objects.PO.POLine> poOrder in view.SelectMultiBound(objArray1, objArray2))
      {
        POOrderRS poOrderRs = PXResult<POOrderRS, PX.Objects.PO.POLine>.op_Implicit(poOrder);
        int num1;
        usedOrderLines.TryGetValue(new PX.Objects.AP.APTran()
        {
          PONbr = poOrderRs.OrderNbr,
          POOrderType = poOrderRs.OrderType
        }, out num1);
        int num2 = num1;
        int? rowCount = ((PXResult) poOrder).RowCount;
        int valueOrDefault = rowCount.GetValueOrDefault();
        if (num2 < valueOrDefault & rowCount.HasValue)
          yield return poOrder;
      }
    }
  }

  public virtual void POOrderRS_CuryID_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    POOrderRS row = (POOrderRS) e.Row;
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    if (row == null || current == null || !(row.CuryID != current.CuryID))
      return;
    string name = typeof (PX.Objects.PO.POOrder.curyID).Name;
    PXErrorLevel pxErrorLevel = (PXErrorLevel) 3;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, name, (string) null, (string) null, "The currency of the source document is different from the one of this document. The value may be recalculated or require correction.", pxErrorLevel, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.IsAltered = true;
  }

  public virtual void POLineS_Selected_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    POLineS row = (POLineS) e.Row;
    if (row == null || (bool) e.OldValue)
      return;
    bool? selected = row.Selected;
    if (!selected.Value)
      return;
    foreach (POLineS poLineS in sender.Updated)
    {
      selected = poLineS.Selected;
      if (selected.GetValueOrDefault() && poLineS != row)
      {
        sender.SetValue<POLineS.selected>((object) poLineS, (object) false);
        ((PXSelectBase) this.Base1.linkLineOrderTran).View.RequestRefresh();
      }
    }
    foreach (POReceiptLineS poReceiptLineS in ((PXSelectBase) this.Base1.linkLineReceiptTran).Cache.Updated)
    {
      if (poReceiptLineS.Selected.GetValueOrDefault())
      {
        ((PXSelectBase) this.Base1.linkLineReceiptTran).Cache.SetValue<POReceiptLineS.selected>((object) poReceiptLineS, (object) false);
        ((PXSelectBase) this.Base1.linkLineReceiptTran).View.RequestRefresh();
      }
    }
  }

  public virtual void POLineS_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    sender.IsDirty = false;
  }

  public virtual void POLineS_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }
}
