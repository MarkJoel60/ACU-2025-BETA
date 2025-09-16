// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Graphs.SubcontractEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Common.Helpers;
using PX.Objects.CN.Subcontracts.PO.CacheExtensions;
using PX.Objects.CN.Subcontracts.SC.DAC;
using PX.Objects.CN.Subcontracts.SC.Descriptor.Attributes;
using PX.Objects.CN.Subcontracts.SC.Views;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Graphs;

public class SubcontractEntry : POOrderEntry
{
  public CRAttributeList<PX.Objects.PO.POOrder> Answers;
  public SubcontractSetup SubcontractSetup;
  public PXAction<PX.Objects.PO.POOrder> printSubcontract;
  public PXAction<PX.Objects.PO.POOrder> emailSubcontract;
  public PXAction<PX.Objects.PO.POOrder> subcontractAuditReport;

  public SubcontractEntry()
  {
    FeaturesSetHelper.CheckConstructionFeature();
    this.UpdateDocumentSummaryFormLayout();
    this.UpdateDocumentDetailsGridLayout();
    this.RemoveShippingHandlers();
    this.AddSubcontractType();
  }

  [PXUIField]
  [PXDeleteButton(ConfirmationMessage = "The current subcontract record will be deleted.")]
  protected virtual IEnumerable delete(PXAdapter adapter)
  {
    ((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Delete(((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Current);
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintSubcontract(PXAdapter adapter, [PXString] string reportID = null)
  {
    return this.Report(adapter.Apply<PXAdapter>((System.Action<PXAdapter>) (a => a.Menu = "Print Subcontract")), new int?(), "SC641000", false, true);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailSubcontract(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "SUBCONTRACT");
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable SubcontractAuditReport(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["SubcontractNumber"] = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Current.OrderNbr
      }, "SC644000", (PXBaseRedirectException.WindowMode) 3, "SC644000", (CurrentLocalization) null);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POSetup> e)
  {
    PoSetupExt extension = PXCacheEx.GetExtension<PoSetupExt>((IBqlTable) e.Row);
    e.Row.RequireOrderControlTotal = extension.RequireSubcontractControlTotal;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.subcontractAuditReport).SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.PO.POOrder>>) e).Cache.GetStatus((object) e.Row) != 2);
  }

  protected virtual void _(PX.Data.Events.RowInserting<POShipAddress> args) => args.Cancel = true;

  protected virtual void _(PX.Data.Events.RowInserting<POShipContact> args) => args.Cancel = true;

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.PO.POOrder> args)
  {
    PX.Objects.PO.POOrder newRow = args.NewRow;
    if (newRow == null)
      return;
    newRow.OrderType = "RS";
  }

  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.siteID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PX.Objects.PO.PO.NumberingAttribute))]
  [AutoNumber(typeof (PoSetupExt.subcontractNumberingID), typeof (PX.Objects.PO.POOrder.orderDate))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Canceled")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.cancelled> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Discount Percent", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.discPct> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Discount Amount", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.curyDiscAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(true)]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.manualDisc> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Discount Code", Visible = false, Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.discountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.discountSequenceID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Line Discounts", Visible = false, Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.curyLineDiscTotal> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Document Discounts", Visible = false, Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.curyDiscTot> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUIVerifyAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POLine.curyLineAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSearchableAttribute))]
  [PXRemoveBaseAttribute(typeof (PXNoteAttribute))]
  [BorrowedNote(typeof (Subcontract), typeof (SubcontractEntry), ShowInReferenceSelector = true, Selector = typeof (Search<PX.Objects.PO.POOrder.noteID, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularSubcontract>>>))]
  [SubcontractSearchable]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.noteID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.PO.POLine> args)
  {
    PX.Objects.PO.POLine row = args.Row;
    if (row == null)
      return;
    row.LineType = "SV";
  }

  protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderQty> e)
  {
  }

  protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.orderedQty> e)
  {
  }

  protected override void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.completedQty> e)
  {
  }

  protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.billedQty> e)
  {
  }

  protected override void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.reqPrepaidQty> e)
  {
  }

  protected override void _(
    PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID> e)
  {
    base._(e);
    this.RaiseErrorIfReceiptIsRequired((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.PO.POLine, PX.Objects.PO.POLine.inventoryID>, PX.Objects.PO.POLine, object>) e).NewValue);
  }

  protected override void POOrder_ExpectedDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs args)
  {
    if (!((PXSelectBase<PX.Objects.PO.POLine>) this.Transactions).Any<PX.Objects.PO.POLine>())
      return;
    ((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Ask("Warning", "Changing of the subcontract 'Start date' will reset 'Start Date' dates for all it's details to their default values. Continue?", (MessageButtons) 4, (MessageIcon) 2);
  }

  protected override void POOrder_OrderDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs args)
  {
    if (!((PXSelectBase<PX.Objects.PO.POLine>) this.Transactions).Any<PX.Objects.PO.POLine>())
      return;
    ((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Ask("Warning", "Changing of the subcontract date will reset the 'Requested' and 'Start Date' dates for all order lines to new values. Do you want to continue?", (MessageButtons) 4, (MessageIcon) 2);
  }

  protected override void POOrder_OrderType_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs args)
  {
    args.NewValue = (object) "RS";
  }

  protected override void POOrder_RowSelected(PXCache cache, PXRowSelectedEventArgs args)
  {
    ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.OrderRequestApproval = new bool?(SubcontractSetupApproval.IsActive);
    base.POOrder_RowSelected(cache, args);
    if (((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current == null || !(args.Row is PX.Objects.PO.POOrder row))
      return;
    PoSetupExt extension = PXCacheEx.GetExtension<PoSetupExt>((IBqlTable) ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current);
    this.SetDefaultPurchaseOrderPreferences();
    SubcontractEntry.UpdatePurchaseOrderBasedOnPreferences(cache, row, extension);
  }

  protected override void POOrder_RowDeleting(PXCache cache, PXRowDeletingEventArgs args)
  {
    PX.Objects.PO.POOrder row = (PX.Objects.PO.POOrder) args.Row;
    if (row == null)
      return;
    this.ValidateSubcontractOnDelete(row);
  }

  protected override void POOrder_RowUpdated(PXCache cache, PXRowUpdatedEventArgs args)
  {
    if (!(args.Row is PX.Objects.PO.POOrder row))
      return;
    POSetupApproval poSetupApproval = ((PXSelectBase<POSetupApproval>) this.SetupApproval).SelectSingle(Array.Empty<object>());
    if (poSetupApproval != null)
      poSetupApproval.IsActive = new bool?(SubcontractSetupApproval.IsActive);
    this.SetControlTotalIfRequired(cache, row);
    base.POOrder_RowUpdated(cache, args);
  }

  protected override void POLine_CuryUnitCost_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs args)
  {
    if (this.SkipCostDefaulting || !(args.Row is PX.Objects.PO.POLine row))
      return;
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Current;
    args.NewValue = (object) this.GetCurrencyUnitCost(current, row, cache);
    int? nullable = row.InventoryID;
    if (!nullable.HasValue || current == null)
      return;
    nullable = current.VendorID;
    if (!nullable.HasValue)
      return;
    APVendorPriceMaint.CheckNewUnitCost<PX.Objects.PO.POLine, PX.Objects.PO.POLine.curyUnitCost>(cache, row, args.NewValue);
  }

  public override bool GetRequireControlTotal(string aOrderType)
  {
    if (!(aOrderType == "RS"))
      return base.GetRequireControlTotal(aOrderType);
    PoSetupExt extension = ((PXSelectBase) this.POSetup).Cache.GetExtension<PoSetupExt>((object) ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current);
    return extension != null && extension.RequireSubcontractControlTotal.GetValueOrDefault();
  }

  private void SetControlTotalIfRequired(PXCache cache, PX.Objects.PO.POOrder order)
  {
    bool? cancelled = order.Cancelled;
    bool flag1 = false;
    if (!(cancelled.GetValueOrDefault() == flag1 & cancelled.HasValue))
      return;
    bool? orderControlTotal = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.RequireOrderControlTotal;
    bool flag2 = false;
    if (!(orderControlTotal.GetValueOrDefault() == flag2 & orderControlTotal.HasValue))
      return;
    Decimal? curyOrderTotal = order.CuryOrderTotal;
    Decimal? curyControlTotal = order.CuryControlTotal;
    if (curyOrderTotal.GetValueOrDefault() == curyControlTotal.GetValueOrDefault() & curyOrderTotal.HasValue == curyControlTotal.HasValue)
      return;
    Decimal? nullable = order.CuryOrderTotal.IsNullOrZero() ? new Decimal?(0M) : order.CuryOrderTotal;
    cache.SetValueExt<PX.Objects.PO.POOrder.curyControlTotal>((object) order, (object) nullable);
  }

  private static void UpdatePurchaseOrderBasedOnPreferences(
    PXCache cache,
    PX.Objects.PO.POOrder order,
    PoSetupExt setup)
  {
    bool valueOrDefault = setup.RequireSubcontractControlTotal.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POOrder.curyControlTotal>(cache, (object) order, valueOrDefault);
  }

  private void RaiseErrorIfReceiptIsRequired(int? inventoryID)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID);
    if (inventoryItem != null && (inventoryItem.StkItem.GetValueOrDefault() || inventoryItem.NonStockReceipt.GetValueOrDefault()))
      throw new PXSetPropertyException((IBqlTable) inventoryItem, "You cannot add inventory items for which the system requires receipt to subcontracts. Select a non-stock item that is configured so that the system does not require receipt for it.")
      {
        ErrorValue = (object) inventoryItem.InventoryCD
      };
  }

  private void SetDefaultPurchaseOrderPreferences()
  {
    ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.UpdateSubOnOwnerChange = new bool?(false);
    ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.AutoReleaseAP = new bool?(false);
    ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.UpdateSubOnOwnerChange = new bool?(false);
  }

  private void UpdateDocumentSummaryFormLayout()
  {
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POOrder.orderNbr>(((PXSelectBase) this.Document).Cache, "Subcontract Nbr.");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POOrder.expectedDate>(((PXSelectBase) this.Document).Cache, "Start Date");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POOrder.curyOrderTotal>(((PXSelectBase) this.Document).Cache, "Subcontract Total");
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POLine.receivedQty>(((PXSelectBase) this.Transactions).Cache, "Received Qty.");
  }

  private void UpdateDocumentDetailsGridLayout()
  {
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POOrder.orderType>(((PXSelectBase) this.Document).Cache, (object) null, false);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.PO.POLine.promisedDate>(((PXSelectBase) this.Transactions).Cache, "Start Date");
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.rcptQtyAction>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.requestedDate>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.promisedDate>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.completed>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.cancelled>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.pONbr>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.baseOrderQty>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<PX.Objects.PO.POLine.receivedQty>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
  }

  private void RemoveShippingHandlers()
  {
    PXGraph.RowInsertedEvents rowInserted = ((PXGraph) this).RowInserted;
    SubcontractEntry subcontractEntry1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) subcontractEntry1, __vmethodptr(subcontractEntry1, POOrder_RowInserted));
    rowInserted.RemoveHandler<PX.Objects.PO.POOrder>(pxRowInserted);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = ((PXGraph) this).FieldUpdated;
    SubcontractEntry subcontractEntry2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) subcontractEntry2, __vmethodptr(subcontractEntry2, POOrder_ShipDestType_FieldUpdated));
    fieldUpdated1.RemoveHandler<PX.Objects.PO.POOrder.shipDestType>(pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = ((PXGraph) this).FieldUpdated;
    SubcontractEntry subcontractEntry3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) subcontractEntry3, __vmethodptr(subcontractEntry3, POOrder_SiteID_FieldUpdated));
    fieldUpdated2.RemoveHandler<PX.Objects.PO.POOrder.siteID>(pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = ((PXGraph) this).FieldUpdated;
    SubcontractEntry subcontractEntry4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) subcontractEntry4, __vmethodptr(subcontractEntry4, POOrder_ShipToLocationID_FieldUpdated));
    fieldUpdated3.RemoveHandler<PX.Objects.PO.POOrder.shipToLocationID>(pxFieldUpdated3);
  }

  private Decimal? GetCurrencyUnitCost(PX.Objects.PO.POOrder subcontract, PX.Objects.PO.POLine subcontractLine, PXCache cache)
  {
    if (subcontractLine.ManualPrice.GetValueOrDefault() || subcontractLine.UOM == null || !subcontractLine.InventoryID.HasValue || (subcontract != null ? (!subcontract.VendorID.HasValue ? 1 : 0) : 1) != 0)
      return new Decimal?(subcontractLine.CuryUnitCost.GetValueOrDefault());
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(subcontract.CuryInfoID);
    return APVendorPriceMaint.CalculateUnitCost(cache, subcontractLine.VendorID, subcontract.VendorLocationID, subcontractLine.InventoryID, subcontractLine.SiteID, currencyInfo.GetCM(), subcontractLine.UOM, subcontractLine.OrderQty, subcontract.OrderDate.GetValueOrDefault(), subcontractLine.CuryUnitCost);
  }

  private void AddSubcontractType()
  {
    if (this.IsSubcontractAlreadyExists())
      return;
    string[] array = "RS".CreateArray<string>();
    PXStringListAttribute.AppendList<PX.Objects.PO.POOrder.orderType>(((PXSelectBase) this.Document).Cache, (object) null, array, array);
  }

  private bool IsSubcontractAlreadyExists()
  {
    foreach (PXStringListAttribute stringListAttribute in ((PXSelectBase) this.Document).Cache.GetAttributes<PX.Objects.PO.POOrder.orderType>().OfType<PXStringListAttribute>())
    {
      if (Array.IndexOf<string>(stringListAttribute.GetAllowedValues(((PXSelectBase) this.Document).Cache), "RS") > 0)
        return true;
    }
    return false;
  }

  private void ValidateSubcontractOnDelete(PX.Objects.PO.POOrder purchaseOrder)
  {
    if (!purchaseOrder.Hold.GetValueOrDefault() && purchaseOrder.Behavior == "C")
      throw new PXException("The subcontract cannot be removed: change order workflow has been enabled for the document because it contains lines related to projects with change order workflow enabled.");
    int? nullable = this.GetSubcontractReceiptsCount(purchaseOrder);
    int num1 = 0;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
      throw new PXException("The subcontract cannot be deleted because some quantity of items for this subcontract have been received.");
    nullable = this.GetSubcontractBillsReleasedCount(purchaseOrder);
    int num2 = 0;
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      throw new PXException("The subcontract cannot be deleted because there is at least one AP bill has been released for this subcontract.");
    nullable = this.GetSubcontractBillsGeneratedCount(purchaseOrder);
    int num3 = 0;
    if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
      throw new PXException("The subcontract cannot be deleted because one or multiple AP bills have been generated for this order. To proceed, delete AP bills first.");
    ((PXSelectBase) this.Transactions).View.SetAnswer("Subcontract line linked to Sales Order Line", (WebDialogResult) 1);
  }

  private int? GetSubcontractReceiptsCount(PX.Objects.PO.POOrder purchaseOrder)
  {
    return PXSelectBase<PX.Objects.PO.POOrderReceipt, PXSelectGroupBy<PX.Objects.PO.POOrderReceipt, Where<PX.Objects.PO.POOrderReceipt.pONbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>, And<PX.Objects.PO.POOrderReceipt.pOType, Equal<Required<PX.Objects.PO.POOrder.orderType>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) purchaseOrder.OrderNbr,
      (object) purchaseOrder.OrderType
    }).RowCount;
  }

  private int? GetSubcontractBillsReleasedCount(PX.Objects.PO.POOrder purchaseOrder)
  {
    return PXSelectBase<PX.Objects.AP.APTran, PXSelectGroupBy<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.pONbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>, And<PX.Objects.AP.APTran.pOOrderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.AP.APTran.released, Equal<True>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) purchaseOrder.OrderNbr,
      (object) purchaseOrder.OrderType
    }).RowCount;
  }

  private int? GetSubcontractBillsGeneratedCount(PX.Objects.PO.POOrder purchaseOrder)
  {
    return PXSelectBase<PX.Objects.AP.APTran, PXSelectGroupBy<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.pONbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>, And<PX.Objects.AP.APTran.pOOrderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) purchaseOrder.OrderNbr,
      (object) purchaseOrder.OrderType
    }).RowCount;
  }

  protected override void ValidateBeforeOpen()
  {
    if (!POOrderEntryExt.IsCommitmentsEnabled((PXGraph) this))
      return;
    List<PXException> source = new List<PXException>();
    foreach (PXResult<PX.Objects.PO.POLine> pxResult in ((PXSelectBase<PX.Objects.PO.POLine>) this.Transactions).Select(Array.Empty<object>()))
    {
      PXException pxException = POOrderEntryExt.ValidateProjectRelatedLineExpenseAccount((PXGraph) this, ((PXSelectBase) this.Transactions).Cache, PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult));
      if (pxException != null)
        source.Add(pxException);
    }
    PX.Objects.PO.POOrder current = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Document).Current;
    Decimal? nullable = !source.Any<PXException>() && !ObjectEx.IsNull((object) current) ? current.UnbilledOrderTotal : throw new PXException("A project commitment cannot be created for at least one document line. For details, see the trace log.");
    Decimal num1 = 0M;
    bool? hold;
    if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
    {
      hold = current.Hold;
      bool flag = false;
      if (hold.GetValueOrDefault() == flag & hold.HasValue)
        throw new PXRowPersistingException(typeof (PX.Objects.PO.POOrder.unbilledOrderTotal).Name, (object) null, "The total unbilled amount of the subcontract cannot be negative.", new object[1]
        {
          (object) "unbilledOrderTotal"
        });
    }
    nullable = current.UnbilledOrderQty;
    Decimal num2 = 0M;
    if (!(nullable.GetValueOrDefault() < num2 & nullable.HasValue))
      return;
    hold = current.Hold;
    bool flag1 = false;
    if (hold.GetValueOrDefault() == flag1 & hold.HasValue)
      throw new PXRowPersistingException(typeof (PX.Objects.PO.POOrder.unbilledOrderQty).Name, (object) null, "The total unbilled quantity of the subcontract cannot be negative.", new object[1]
      {
        (object) "unbilledOrderQty"
      });
  }

  protected override bool IsMigrationModeAllowed => true;

  protected override string GetPOOrderAPDocStatusText()
  {
    return this.GetAPDocStatusText(this.GetPOOrderAPDocTranSigned(false), this.GetPOOrderAPDocTranSigned(true));
  }

  protected virtual string GetAPDocStatusText(PX.Objects.PO.APTranSigned apTranTotal, PX.Objects.PO.APTranSigned apTranReleased)
  {
    int fieldPrecision1 = this.GetFieldPrecision<PX.Objects.PO.APTranSigned, PX.Objects.PO.APTranSigned.signedCuryTranAmt>(apTranTotal);
    int fieldPrecision2 = this.GetFieldPrecision<PX.Objects.PO.APTranSigned, PX.Objects.PO.APTranSigned.pOPPVAmt>(apTranTotal);
    (Decimal TotalBilledQty, Decimal TotalBilledAmt, Decimal TotalPPVAmt) apDocStatusValues1 = this.GetAPDocStatusValues(apTranTotal);
    (Decimal TotalBilledQty, Decimal TotalBilledAmt, Decimal TotalPPVAmt) apDocStatusValues2 = this.GetAPDocStatusValues(apTranReleased);
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() ? PXMessages.LocalizeFormatNoPrefix("Total Billed Qty. {0}, Total Billed Amt. {1}, Total Released Amt. {2}, Total PPV Amt. {3}", new object[4]
    {
      (object) this.FormatQty(new Decimal?(apDocStatusValues1.TotalBilledQty)),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues1.TotalBilledAmt), fieldPrecision1),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues2.TotalBilledAmt), fieldPrecision1),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues1.TotalPPVAmt), fieldPrecision2)
    }) : PXMessages.LocalizeFormatNoPrefix("Total Billed Qty. {0}, Total Billed Amt. {1}, Total Released Amt. {2}", new object[3]
    {
      (object) this.FormatQty(new Decimal?(apDocStatusValues1.TotalBilledQty)),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues1.TotalBilledAmt), fieldPrecision1),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues2.TotalBilledAmt), fieldPrecision1)
    });
  }
}
