// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.GraphExtensions.ApInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.Subcontracts.AP.Descriptor;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.AP.GraphExtensions;

public class ApInvoiceEntryExt : PXGraphExtension<APInvoiceEntry>
{
  public PXAction<PX.Objects.AP.APInvoice> ViewSubcontract;
  public PXAction<PX.Objects.AP.APInvoice> ViewPurchaseOrder;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && !SiteMapExtension.IsTaxBillsAndAdjustmentsScreenId();
  }

  [PXOverride]
  public virtual bool EnableRetainage(
    ApInvoiceEntryExt.EnableRetainageDelegate baseMethod)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.RetainageApply.GetValueOrDefault())
      return false;
    ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.RetainageApply = new bool?(true);
    ((PXSelectBase) this.Base.Document).Cache.SetDefaultExt<PX.Objects.AP.APRegister.defRetainagePct>((object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current);
    ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.AP.APInvoice.retainageApply>((object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (object) true, (Exception) new PXSetPropertyException("The Apply Retainage check box is selected automatically because you have added one or more lines with a retainage from the purchase order or subcontract.", (PXErrorLevel) 2));
    return true;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual void viewSubcontract()
  {
    this.ViewPoEntity((POOrderEntry) PXGraph.CreateInstance<SubcontractEntry>(), "View Subcontract", (Func<PX.Objects.PO.POOrder, bool>) (x => x.OrderType == "RS"));
  }

  [PXButton]
  [PXUIField]
  public virtual void viewPurchaseOrder()
  {
    this.ViewPoEntity(PXGraph.CreateInstance<POOrderEntry>(), "View PO Order", (Func<PX.Objects.PO.POOrder, bool>) (x => x.OrderType != "RS"));
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXStringListAttribute))]
  [LinkLineSelectedModeList]
  protected virtual void _(
    PX.Data.Events.CacheAttached<LinkLineFilter.selectedMode> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pOLineNbr> args)
  {
    if (!ApInvoiceEntryExt.IsSubcontract(args.Row))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pOLineNbr>>) args).ReturnValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pOOrderType> args)
  {
    if (!ApInvoiceEntryExt.IsSubcontract(args.Row))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pOOrderType>>) args).ReturnValue = (object) null;
  }

  protected virtual void _(PX.Data.Events.FieldSelecting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pONbr> args)
  {
    if (!ApInvoiceEntryExt.IsSubcontract(args.Row))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.AP.APTran, PX.Objects.AP.APTran.pONbr>>) args).ReturnValue = (object) null;
  }

  protected void APInvoice_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs args,
    PXRowSelected baseHandler)
  {
    if (!(args.Row is PX.Objects.AP.APInvoice))
      return;
    baseHandler.Invoke(cache, args);
    ((PXSelectBase) this.Base.Document).Cache.AllowUpdate = true;
  }

  private void ViewPoEntity(
    POOrderEntry graph,
    string message,
    Func<PX.Objects.PO.POOrder, bool> checkPurchaseOrderType)
  {
    if (((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Current == null)
      return;
    PX.Objects.PO.POOrder purchaseOrder = this.GetPurchaseOrder(((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Current);
    if (purchaseOrder != null && checkPurchaseOrderType(purchaseOrder))
    {
      ((PXSelectBase<PX.Objects.PO.POOrder>) graph.Document).Current = purchaseOrder;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) graph, message);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  private static bool IsSubcontract(PX.Objects.AP.APTran apTran) => apTran?.POOrderType == "RS";

  private PX.Objects.PO.POOrder GetPurchaseOrder(PX.Objects.AP.APTran apTran)
  {
    return ((PXSelectBase<PX.Objects.PO.POOrder>) new PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
    {
      (object) apTran.POOrderType,
      (object) apTran.PONbr
    });
  }

  public delegate void InvoicePoOrderDelegate(PX.Objects.PO.POOrder order, bool createNew, bool keepOrderTaxes = false);

  public delegate bool EnableRetainageDelegate();
}
