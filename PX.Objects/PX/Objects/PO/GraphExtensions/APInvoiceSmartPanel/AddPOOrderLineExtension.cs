// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddPOOrderLineExtension
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
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;

/// <summary>
/// This class implements graph extension to use special dialogs called Smart Panel to perform "ADD PO LINE" (Screen AP301000)
/// </summary>
[Serializable]
public class AddPOOrderLineExtension : PXGraphExtension<LinkLineExtension, APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<POLineRS> poorderlineslist;
  public PXFilter<POOrderFilter> orderfilter;
  public PXAction<PX.Objects.AP.APInvoice> addPOOrderLine;
  public PXAction<PX.Objects.AP.APInvoice> addPOOrderLine2;
  public PXAction<PX.Objects.AP.APInvoice> viewPOOrderAddPOLine;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.poorderlineslist).Cache.AllowDelete = false;
    ((PXSelectBase) this.poorderlineslist).Cache.AllowInsert = false;
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddPOOrderLine(PXAdapter adapter)
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
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && ((PXSelectBase<POLineRS>) this.poorderlineslist).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddPOOrderLine\u003Eb__5_0)), true) == 1)
          return this.AddPOOrderLine2(adapter);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AddPOOrderLine2(PXAdapter adapter)
  {
    ((PXGraphExtension<APInvoiceEntry>) this).Base.updateTaxCalcMode();
    if (this.ShouldAddPOOrderLine())
    {
      ((PXGraphExtension<APInvoiceEntry>) this).Base.ProcessPOOrderLines((IEnumerable<IAPTranSource>) GraphHelper.RowCast<POLineRS>(((PXSelectBase) this.poorderlineslist).Cache.Updated).Where<POLineRS>((Func<POLineRS, bool>) (t => t.Selected.GetValueOrDefault())).ToList<POLineRS>());
      ((PXSelectBase) this.poorderlineslist).Cache.Clear();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual void ViewPOOrderAddPOLine()
  {
    POLineRS current = ((PXSelectBase<POLineRS>) this.poorderlineslist).Current;
    if (current == null)
      return;
    PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, current.OrderType, current.OrderNbr) ?? throw new PXException("The {0} {1} purchase order was not found.", new object[2]
    {
      (object) current.OrderType,
      (object) current.OrderNbr
    });
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<POOrderEntry>(), (object) poOrder, (PXRedirectHelper.WindowMode) 3);
  }

  public virtual bool ShouldAddPOOrderLine()
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
    ((PXAction) this.addPOOrderLine).SetVisible(documentState.IsDocumentInvoice);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poorderlineslist).Cache, (string) null, false);
    bool flag = documentState.IsDocumentEditable && !documentState.IsDocumentScheduled && ((PXSelectBase<PX.Objects.AP.Vendor>) ((PXGraphExtension<APInvoiceEntry>) this).Base.vendor).Current != null && !documentState.IsRetainageDebAdj;
    ((PXAction) this.addPOOrderLine).SetEnabled(flag);
    PXUIFieldAttribute.SetEnabled<POLineRS.selected>(((PXSelectBase) this.poorderlineslist).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<POOrderFilter.showBilledLines>(((PXSelectBase) this.orderfilter).Cache, (object) null, documentState.IsDocumentInvoice);
    PXUIFieldAttribute.SetVisible<POLineRS.unbilledQty>(((PXSelectBase) this.poorderlineslist).Cache, (object) null, documentState.IsDocumentInvoice);
    PXUIFieldAttribute.SetVisible<POLineRS.curyUnbilledAmt>(((PXSelectBase) this.poorderlineslist).Cache, (object) null, documentState.IsDocumentInvoice);
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<POLineRS.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Task", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<POLineRS.taskID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Cost Code", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<POLineRS.costCodeID> e)
  {
  }

  public virtual IEnumerable pOOrderLinesList()
  {
    PX.Objects.AP.APInvoice current1 = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    bool flag1 = current1.DocType == "INV";
    bool flag2 = current1.DocType == "ADR";
    bool flag3 = current1.DocType == "PPM";
    POOrderFilter current2 = ((PXSelectBase<POOrderFilter>) this.orderfilter).Current;
    if ((current1 != null ? (!current1.VendorID.HasValue ? 1 : 0) : 1) != 0 || !current1.VendorLocationID.HasValue || !flag1 && !flag2 && !flag3)
      return (IEnumerable) Enumerable.Empty<POLineRS>();
    PXSelectBase<POLineRS> pxSelectBase = (PXSelectBase<POLineRS>) new PXSelectReadonly<POLineRS, Where<POLineRS.orderType, NotIn3<POOrderType.blanket, POOrderType.standardBlanket>, And<POLineRS.cancelled, NotEqual<True>, And<POLineRS.curyID, Equal<Current<PX.Objects.AP.APInvoice.curyID>>, And2<NotExists<Select2<POOrderReceipt, InnerJoin<PX.Objects.PO.POReceipt, On<POOrderReceipt.FK.Receipt>>, Where<POOrderReceipt.pOType, Equal<POLineRS.orderType>, And<POOrderReceipt.pONbr, Equal<POLineRS.orderNbr>, And<PX.Objects.PO.POReceipt.status, Equal<POReceiptStatus.underCorrection>>>>>>, And<Where<Current<POOrderFilter.orderNbr>, IsNull, Or<POLineRS.orderNbr, Equal<Current<POOrderFilter.orderNbr>>>>>>>>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base);
    if (!flag2)
      pxSelectBase.WhereAnd<Where<POLineRS.closed, NotEqual<True>>>();
    if (!flag2)
      pxSelectBase.WhereAnd<Where<POLineRS.status, In3<POOrderStatus.awaitingLink, POOrderStatus.open, POOrderStatus.completed>>>();
    else
      pxSelectBase.WhereAnd<Where<POLineRS.status, In3<POOrderStatus.open, POOrderStatus.completed, POOrderStatus.closed>>>();
    if (flag1 | flag2)
      pxSelectBase.WhereAnd<Where<POLineRS.pOAccrualType, Equal<POAccrualType.order>>>();
    else if (flag3)
    {
      pxSelectBase.WhereAnd<Where<POLineRS.curyReqPrepaidAmt, Less<POLineRS.curyExtCost>>>();
      pxSelectBase.WhereAnd<Where<POLineRS.taxZoneID, Equal<Current<PX.Objects.AP.APInvoice.taxZoneID>>, Or<POLineRS.taxZoneID, IsNull, And<Current<PX.Objects.AP.APInvoice.taxZoneID>, IsNull>>>>();
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
      pxSelectBase.WhereAnd<Where<POLineRS.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<POLineRS.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<POLineRS.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>();
    else
      pxSelectBase.WhereAnd<Where<POLineRS.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<POLineRS.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>>>>();
    if (!current2.ShowBilledLines.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<POLineRS.unbilledQty, NotEqual<decimal0>, Or<POLineRS.curyUnbilledAmt, NotEqual<decimal0>>>>();
    Lazy<POAccrualSet> usedPOAccrual = new Lazy<POAccrualSet>((Func<POAccrualSet>) (() => ((PXGraphExtension<APInvoiceEntry>) this).Base.GetUsedPOAccrualSet()));
    return (IEnumerable) GraphHelper.RowCast<POLineRS>((IEnumerable) ((PXSelectBase) pxSelectBase).View.SelectMultiBound(new object[2]
    {
      (object) current1,
      (object) current2
    }, Array.Empty<object>())).AsEnumerable<POLineRS>().Where<POLineRS>((Func<POLineRS, bool>) (t => !usedPOAccrual.Value.Contains(t))).ToList<POLineRS>();
  }
}
