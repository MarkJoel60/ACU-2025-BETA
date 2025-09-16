// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceShipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.MigrationMode;
using PX.Objects.Common.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
public class SOInvoiceShipment : PXGraph<
#nullable disable
SOInvoiceShipment>
{
  public PXCancel<SOShipmentFilter> Cancel;
  public PXAction<SOShipmentFilter> viewDocument;
  public PXFilter<SOShipmentFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<SOShipment, SOShipmentFilter> Orders;
  public PXSelect<SOShipLine> dummy_select_to_bind_events;
  public PXSetup<SOSetup> sosetup;
  public PXSelect<PX.Objects.IN.INSite> INSites;
  public PXSelect<PX.Objects.CS.Carrier> Carriers;
  protected bool _ActionChanged;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<SOShipment>) this.Orders).Current == null)
      return adapter.Get();
    if (((PXSelectBase<SOShipment>) this.Orders).Current.ShipmentType == "H")
    {
      POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
      string str = ((PXSelectBase<SOShipment>) this.Orders).Current.Operation == "R" ? "RN" : "RT";
      ((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Current = PXResultset<PX.Objects.PO.POReceipt>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceipt>) instance.Document).Search<PX.Objects.PO.POReceipt.receiptNbr>((object) ((PXSelectBase<SOShipment>) this.Orders).Current.ShipmentNbr, new object[1]
      {
        (object) str
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Purchase Receipt");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    SOShipmentEntry instance1 = PXGraph.CreateInstance<SOShipmentEntry>();
    ((PXSelectBase<SOShipment>) instance1.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) instance1.Document).Search<SOShipment.shipmentNbr>((object) ((PXSelectBase<SOShipment>) this.Orders).Current.ShipmentNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Shipment");
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.INSite.descr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CS.Carrier.description> e)
  {
  }

  [PXMergeAttributes]
  [CopiedNoteID(typeof (SOShipment))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOShipment.noteID> e)
  {
  }

  public SOInvoiceShipment()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXProcessingBase<SOShipment>) this.Orders).SetSelected<SOShipment.selected>();
    SOSetup current = ((PXSelectBase<SOSetup>) this.sosetup).Current;
  }

  public virtual void SOShipmentFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    SOShipmentFilter filter = e.Row as SOShipmentFilter;
    if (filter != null && !string.IsNullOrEmpty(filter.Action))
    {
      Dictionary<string, object> dictionary = ((PXSelectBase) this.Filter).Cache.ToDictionary((object) filter);
      ((PXProcessingBase<SOShipment>) this.Orders).SetProcessWorkflowAction(filter.Action, dictionary);
    }
    PXUIFieldAttribute.SetEnabled<SOShipmentFilter.invoiceDate>(sender, (object) filter, EnumerableExtensions.IsIn<string>(filter.Action, "SO302000$createInvoice", "SO302000$createDropshipInvoice"));
    PXUIFieldAttribute.SetEnabled<SOShipmentFilter.packagingType>(sender, (object) filter, filter.Action != "SO302000$createDropshipInvoice");
    PXUIFieldAttribute.SetEnabled<SOShipmentFilter.siteID>(sender, (object) filter, filter.Action != "SO302000$createDropshipInvoice");
    PXUIFieldAttribute.SetVisible<SOShipmentFilter.showPrinted>(sender, (object) filter, EnumerableExtensions.IsIn<string>(filter.Action, "SO302000$printShipmentConfirmation", "SO302000$printLabels", "SO302000$printCommercialInvoices", "SO302000$printPickListAction"));
    PXUIFieldAttribute.SetDisplayName<SOShipment.shipmentNbr>(((PXSelectBase) this.Orders).Cache, filter.Action == "SO302000$createDropshipInvoice" ? "Receipt Nbr." : "Shipment Nbr.");
    PXUIFieldAttribute.SetDisplayName<SOShipment.shipDate>(((PXSelectBase) this.Orders).Cache, filter.Action == "SO302000$createDropshipInvoice" ? "Date" : "Shipment Date");
    bool? nullable = ((PXSelectBase<SOSetup>) this.sosetup).Current.UseShipDateForInvoiceDate;
    if (nullable.GetValueOrDefault())
    {
      sender.RaiseExceptionHandling<SOShipmentFilter.invoiceDate>((object) filter, (object) null, (Exception) new PXSetPropertyException("Shipment Date will be used for Invoice Date.", (PXErrorLevel) 2));
      PXUIFieldAttribute.SetEnabled<SOShipmentFilter.invoiceDate>(sender, (object) filter, false);
    }
    Exception exception = (GraphExtensionMethods.IsScheduler((PXGraph) this) || !(filter.Action == "SO302000$updateIN") ? 0 : (UpdateInventoryExtension.NeedWarningShipNotInvoicedUpdateIN((PXGraph) this, ((PXSelectBase<SOSetup>) this.sosetup).Current, (IEnumerable<SOShipment>) ((PXSelectBase<SOShipment>) this.Orders).SelectMain(Array.Empty<object>())) ? 1 : 0)) != 0 ? (Exception) new PXSetPropertyException("There is one or multiple order types in which the shipped-not-invoiced account is not specified. On the invoices for sales orders of these types, the revenue may be posted to a financial period that will not match the COGS period.", (PXErrorLevel) 2) : (Exception) null;
    sender.RaiseExceptionHandling<SOShipmentFilter.action>((object) filter, (object) null, exception);
    bool flag1 = this.IsPrintingAllowed(filter);
    PXUIFieldAttribute.SetVisible<SOShipmentFilter.printWithDeviceHub>(sender, (object) filter, flag1);
    PXUIFieldAttribute.SetVisible<SOShipmentFilter.definePrinterManually>(sender, (object) filter, flag1);
    PXUIFieldAttribute.SetVisible<SOShipmentFilter.printerID>(sender, (object) filter, flag1);
    PXUIFieldAttribute.SetVisible<SOShipmentFilter.numberOfCopies>(sender, (object) filter, flag1);
    if (PXContext.GetSlot<AUSchedule>() == null)
    {
      PXCache pxCache1 = sender;
      SOShipmentFilter soShipmentFilter1 = filter;
      nullable = filter.PrintWithDeviceHub;
      int num1 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<SOShipmentFilter.definePrinterManually>(pxCache1, (object) soShipmentFilter1, num1 != 0);
      PXCache pxCache2 = sender;
      SOShipmentFilter soShipmentFilter2 = filter;
      nullable = filter.PrintWithDeviceHub;
      int num2 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<SOShipmentFilter.numberOfCopies>(pxCache2, (object) soShipmentFilter2, num2 != 0);
      PXCache pxCache3 = sender;
      SOShipmentFilter soShipmentFilter3 = filter;
      nullable = filter.PrintWithDeviceHub;
      int num3;
      if (nullable.GetValueOrDefault())
      {
        nullable = filter.DefinePrinterManually;
        num3 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num3 = 0;
      PXUIFieldAttribute.SetEnabled<SOShipmentFilter.printerID>(pxCache3, (object) soShipmentFilter3, num3 != 0);
    }
    nullable = filter.PrintWithDeviceHub;
    if (nullable.GetValueOrDefault())
    {
      nullable = filter.DefinePrinterManually;
      if (nullable.GetValueOrDefault())
        goto label_13;
    }
    filter.PrinterID = new Guid?();
label_13:
    bool flag2 = EnumerableExtensions.IsIn<string>(filter.Action, "SO302000$createInvoice", "SO302000$createDropshipInvoice");
    PXUIFieldAttribute.SetEnabled<SOShipment.billSeparately>(((PXSelectBase) this.Orders).Cache, (object) null, flag2 && PXLongOperation.GetStatus(((PXGraph) this).UID) == 0);
    PXUIFieldAttribute.SetVisible<SOShipment.billSeparately>(((PXSelectBase) this.Orders).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<SOShipment.billingInOrders>(((PXSelectBase) this.Orders).Cache, (object) null, filter.Action == "SO302000$createInvoice");
    PXCacheEx.Adjust<CopiedNoteIDAttribute>(((PXSelectBase) this.Orders).Cache, (object) null).For<SOShipment.noteID>((Action<CopiedNoteIDAttribute>) (a =>
    {
      bool flag3 = filter.Action == "SO302000$createDropshipInvoice";
      a.EntityType = flag3 ? typeof (PX.Objects.PO.POReceipt) : typeof (SOShipment);
      a.GraphType = flag3 ? typeof (POReceiptEntry) : typeof (SOShipmentEntry);
    }));
  }

  protected virtual bool IsPrintingAllowed(SOShipmentFilter filter)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && EnumerableExtensions.IsIn<string>(filter.Action, "SO302000$printLabels", "SO302000$printCommercialInvoices", "SO302000$printPickListAction", "SO302000$printShipmentConfirmation");
  }

  public virtual void SOShipmentFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this._ActionChanged = !sender.ObjectsEqual<SOShipmentFilter.action>(e.Row, e.OldRow);
    if (this._ActionChanged && e.Row != null)
    {
      SOShipmentFilter row = (SOShipmentFilter) e.Row;
      row.PackagingType = "B";
      if (row.Action == "SO302000$createDropshipInvoice")
        row.SiteID = new int?();
    }
    if (!this._ActionChanged && sender.ObjectsEqual<SOShipmentFilter.definePrinterManually>(e.Row, e.OldRow) && sender.ObjectsEqual<SOShipmentFilter.printWithDeviceHub>(e.Row, e.OldRow) || ((PXSelectBase<SOShipmentFilter>) this.Filter).Current == null || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || !((PXSelectBase<SOShipmentFilter>) this.Filter).Current.PrintWithDeviceHub.GetValueOrDefault() || !((PXSelectBase<SOShipmentFilter>) this.Filter).Current.DefinePrinterManually.GetValueOrDefault() || PXContext.GetSlot<AUSchedule>() != null && ((PXSelectBase<SOShipmentFilter>) this.Filter).Current.PrinterID.HasValue && !((SOShipmentFilter) e.OldRow).PrinterID.HasValue)
      return;
    ((PXSelectBase<SOShipmentFilter>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("Customer", SOReports.GetReportID(new int?(), ((PXSelectBase<SOShipmentFilter>) this.Filter).Current.Action), ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void SOShipmentFilter_PrinterName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOShipmentFilter row = (SOShipmentFilter) e.Row;
    if (row == null || this.IsPrintingAllowed(row))
      return;
    e.NewValue = (object) null;
  }

  public virtual IEnumerable orders()
  {
    SOInvoiceShipment soInvoiceShipment = this;
    PXUIFieldAttribute.SetDisplayName<SOShipment.customerID>(((PXGraph) soInvoiceShipment).Caches[typeof (SOShipment)], "Customer ID");
    if (!(((PXSelectBase<SOShipmentFilter>) soInvoiceShipment.Filter).Current.Action == "<SELECT>"))
    {
      if (soInvoiceShipment._ActionChanged)
      {
        ((PXSelectBase) soInvoiceShipment.Orders).Cache.ClearQueryCache();
        ((PXSelectBase) soInvoiceShipment.Orders).Cache.Clear();
      }
      PXSelectBase cmd = soInvoiceShipment.GetShipmentsSelectCommand(((PXSelectBase<SOShipmentFilter>) soInvoiceShipment.Filter).Current);
      if (cmd is PXSelectBase<SOShipment> shCmd)
      {
        soInvoiceShipment.ApplyShipmentFilters(shCmd, ((PXSelectBase<SOShipmentFilter>) soInvoiceShipment.Filter).Current);
        int startRow = PXView.StartRow;
        int num = 0;
        foreach (object obj in ((PXSelectBase) shCmd).View.Select((object[]) null, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
        {
          SOShipment soShipment1 = PXResult.Unwrap<SOShipment>(obj);
          SOShipment soShipment2 = (SOShipment) ((PXSelectBase) soInvoiceShipment.Orders).Cache.Locate((object) soShipment1);
          if (soShipment2 != null)
          {
            soShipment1.Selected = soShipment2.Selected;
            soShipment1.BillSeparately = soShipment2.BillSeparately;
          }
          yield return obj;
        }
        PXView.StartRow = 0;
      }
      if (cmd is PXSelectBase<PX.Objects.PO.POReceipt> rtCmd)
      {
        soInvoiceShipment.ApplyReceiptFilters(rtCmd, ((PXSelectBase<SOShipmentFilter>) soInvoiceShipment.Filter).Current);
        int startRow = PXView.StartRow;
        int num = 0;
        string[] strArray = soInvoiceShipment.AlterDropshipSortColumns(PXView.SortColumns);
        PXFilterRow[] pxFilterRowArray = soInvoiceShipment.AlterDropshipFilters(PXView.Filters);
        foreach (object obj in ((PXSelectBase) rtCmd).View.Select((object[]) null, (object[]) null, PXView.Searches, strArray, PXView.Descendings, pxFilterRowArray, ref startRow, PXView.MaximumRows, ref num))
        {
          SOShipment soShipment3 = SOShipment.FromDropshipPOReceipt(PXResult.Unwrap<PX.Objects.PO.POReceipt>(obj));
          SOShipment soShipment4 = (SOShipment) ((PXSelectBase) soInvoiceShipment.Orders).Cache.Locate((object) soShipment3);
          if (soShipment4 == null)
          {
            ((PXSelectBase) soInvoiceShipment.Orders).Cache.SetStatus((object) soShipment3, (PXEntryStatus) 5);
          }
          else
          {
            soShipment3.Selected = soShipment4.Selected;
            soShipment3.BillSeparately = soShipment4.BillSeparately;
          }
          yield return (object) soShipment3;
        }
        PXView.StartRow = 0;
      }
      ((PXSelectBase) soInvoiceShipment.Orders).Cache.IsDirty = false;
    }
  }

  protected virtual PXSelectBase GetShipmentsSelectCommand(SOShipmentFilter filter)
  {
    PXSelectBase shipmentsSelectCommand;
    switch (filter.Action)
    {
      case "SO302000$createInvoice":
        shipmentsSelectCommand = (PXSelectBase) new FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOShipment.FK.Site>>, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOShipment.FK.Carrier>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.confirmed, Equal<True>>>>, And<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>, And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.And<Exists<SelectFromBase<SOOrderShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderShipment.shipmentNbr, Equal<SOShipment.shipmentNbr>>>>, And<BqlOperand<SOOrderShipment.shipmentType, IBqlString>.IsEqual<SOShipment.shipmentType>>>, And<BqlOperand<SOOrderShipment.invoiceNbr, IBqlString>.IsNull>>>.And<BqlOperand<SOOrderShipment.createARDoc, IBqlBool>.IsEqual<True>>>>>>, SOShipment>.View((PXGraph) this);
        break;
      case "SO302000$updateIN":
        shipmentsSelectCommand = (PXSelectBase) new FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOShipment.FK.Site>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOShipment.FK.Carrier>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.confirmed, Equal<True>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>, And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.And<Exists<SelectFromBase<SOOrderShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderShipment.shipmentNbr, Equal<SOShipment.shipmentNbr>>>>, And<BqlOperand<SOOrderShipment.shipmentType, IBqlString>.IsEqual<SOShipment.shipmentType>>>, And<BqlOperand<SOOrderShipment.invtRefNbr, IBqlString>.IsNull>>>.And<BqlOperand<SOOrderShipment.createINDoc, IBqlBool>.IsEqual<True>>>>>>, SOShipment>.View((PXGraph) this);
        break;
      case "SO302000$createDropshipInvoice":
        shipmentsSelectCommand = (PXSelectBase) new FbqlSelect<SelectFromBase<PX.Objects.PO.POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceipt.released, Equal<True>>>>, And<Exists<SelectFromBase<SOOrderShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderType>.On<SOOrderShipment.FK.OrderType>>, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<BqlOperand<SOOrderShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderShipment.shipmentNbr, Equal<PX.Objects.PO.POReceipt.receiptNbr>>>>, And<BqlOperand<SOOrderShipment.shipmentType, IBqlString>.IsEqual<INDocType.dropShip>>>, And<BqlOperand<SOOrderShipment.invoiceNbr, IBqlString>.IsNull>>, And<BqlOperand<SOOrderShipment.operation, IBqlString>.IsEqual<Use<Switch<Case<Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<POReceiptType.poreturn>>, SOOperation.receipt>, SOOperation.issue>>.AsString>>>, And<BqlOperand<SOOrderShipment.createARDoc, IBqlBool>.IsEqual<True>>>, And<BqlOperand<SOOrderShipment.canceled, IBqlBool>.IsEqual<False>>>>.And<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>>, And<BqlOperand<PX.Objects.PO.POReceipt.isUnderCorrection, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.PO.POReceipt.canceled, IBqlBool>.IsEqual<False>>>, PX.Objects.PO.POReceipt>.View((PXGraph) this);
        break;
      default:
        shipmentsSelectCommand = (PXSelectBase) new FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOShipment.FK.Site>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.bAccountID>>.SingleTableOnly>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<SOShipment.FK.Carrier>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>.And<Exists<SelectFromBase<SOOrderShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderShipment.shipmentNbr, Equal<SOShipment.shipmentNbr>>>>>.And<BqlOperand<SOOrderShipment.shipmentType, IBqlString>.IsEqual<SOShipment.shipmentType>>>>>>, SOShipment>.View((PXGraph) this);
        break;
    }
    return shipmentsSelectCommand;
  }

  protected virtual void ApplyShipmentFilters(
    PXSelectBase<SOShipment> shCmd,
    SOShipmentFilter filter)
  {
    shCmd.WhereAnd<Where<BqlOperand<SOShipment.shipDate, IBqlDateTime>.IsLessEqual<BqlField<SOShipmentFilter.endDate, IBqlDateTime>.FromCurrent>>>();
    shCmd.WhereAnd<Where<WorkflowAction.IsEnabled<SOShipment, SOShipmentFilter.action>>>();
    if (filter.CustomerID.HasValue)
      shCmd.WhereAnd<Where<BqlOperand<SOShipment.customerID, IBqlInt>.IsEqual<BqlField<SOShipmentFilter.customerID, IBqlInt>.FromCurrent>>>();
    if (!string.IsNullOrEmpty(filter.ShipVia))
      shCmd.WhereAnd<Where<BqlOperand<SOShipment.shipVia, IBqlString>.IsEqual<BqlField<SOShipmentFilter.shipVia, IBqlString>.FromCurrent>>>();
    if (filter.StartDate.HasValue)
      shCmd.WhereAnd<Where<BqlOperand<SOShipment.shipDate, IBqlDateTime>.IsGreaterEqual<BqlField<SOShipmentFilter.startDate, IBqlDateTime>.FromCurrent>>>();
    if (!string.IsNullOrEmpty(filter.CarrierPluginID))
      shCmd.WhereAnd<Where<BqlOperand<PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<BqlField<SOShipmentFilter.carrierPluginID, IBqlString>.FromCurrent>>>();
    if (filter.SiteID.HasValue)
      shCmd.WhereAnd<Where<BqlOperand<SOShipment.siteID, IBqlInt>.IsEqual<BqlField<SOShipmentFilter.siteID, IBqlInt>.FromCurrent>>>();
    bool? showPrinted = filter.ShowPrinted;
    bool flag = false;
    if (showPrinted.GetValueOrDefault() == flag & showPrinted.HasValue)
    {
      if (filter.Action == "SO302000$printShipmentConfirmation")
        shCmd.WhereAnd<Where<BqlOperand<SOShipment.confirmationPrinted, IBqlBool>.IsEqual<False>>>();
      else if (filter.Action == "SO302000$printLabels")
        shCmd.WhereAnd<Where<BqlOperand<SOShipment.labelsPrinted, IBqlBool>.IsEqual<False>>>();
      else if (filter.Action == "SO302000$printCommercialInvoices")
        shCmd.WhereAnd<Where<BqlOperand<SOShipment.commercialInvoicesPrinted, IBqlBool>.IsEqual<False>>>();
      else if (filter.Action == "SO302000$printPickListAction")
        shCmd.WhereAnd<Where<BqlOperand<SOShipment.pickListPrinted, IBqlBool>.IsEqual<False>>>();
    }
    if (filter.PackagingType == "M")
      shCmd.WhereAnd<Where<BqlOperand<SOShipment.isManualPackage, IBqlBool>.IsEqual<True>>>();
    else if (filter.PackagingType == "A")
      shCmd.WhereAnd<Where<BqlOperand<SOShipment.isManualPackage, IBqlBool>.IsEqual<False>>>();
    if (!(filter.Action == "SO302000$getReturnLabelsAction"))
      return;
    shCmd.WhereAnd<Where<BqlOperand<SOShipment.unlimitedPackages, IBqlBool>.IsEqual<False>>>();
  }

  protected virtual void ApplyReceiptFilters(PXSelectBase<PX.Objects.PO.POReceipt> rtCmd, SOShipmentFilter filter)
  {
    rtCmd.WhereAnd<Where<BqlOperand<PX.Objects.PO.POReceipt.receiptDate, IBqlDateTime>.IsLessEqual<BqlField<SOShipmentFilter.endDate, IBqlDateTime>.FromCurrent>>>();
    if (filter.CustomerID.HasValue)
      rtCmd.WhereAnd<Where<BqlOperand<PX.Objects.PO.POReceipt.dropshipCustomerID, IBqlInt>.IsEqual<BqlField<SOShipmentFilter.customerID, IBqlInt>.FromCurrent>>>();
    if (filter.ShipVia != null)
      rtCmd.WhereAnd<Where<BqlOperand<PX.Objects.PO.POReceipt.dropshipShipVia, IBqlString>.IsEqual<BqlField<SOShipmentFilter.shipVia, IBqlString>.FromCurrent>>>();
    if (filter.CarrierPluginID != null)
    {
      rtCmd.Join<InnerJoin<PX.Objects.CS.Carrier, On<BqlOperand<PX.Objects.CS.Carrier.carrierID, IBqlString>.IsEqual<PX.Objects.PO.POReceipt.dropshipShipVia>>>>();
      rtCmd.WhereAnd<Where<BqlOperand<PX.Objects.CS.Carrier.carrierPluginID, IBqlString>.IsEqual<BqlField<SOShipmentFilter.carrierPluginID, IBqlString>.FromCurrent>>>();
    }
    if (!filter.StartDate.HasValue)
      return;
    rtCmd.WhereAnd<Where<BqlOperand<PX.Objects.PO.POReceipt.receiptDate, IBqlDateTime>.IsGreaterEqual<BqlField<SOShipmentFilter.startDate, IBqlDateTime>.FromCurrent>>>();
  }

  protected virtual Dictionary<string, string> DropshipFieldsMapping
  {
    get
    {
      return new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
      {
        {
          "shipmentNbr",
          "receiptNbr"
        },
        {
          "customerID",
          "dropshipCustomerID"
        },
        {
          "customerLocationID",
          "dropshipCustomerLocationID"
        },
        {
          "customerOrderNbr",
          "dropshipCustomerOrderNbr"
        },
        {
          "shipVia",
          "dropshipShipVia"
        },
        {
          "shipDate",
          "receiptDate"
        }
      };
    }
  }

  protected virtual string[] AlterDropshipSortColumns(string[] sortColumns)
  {
    Dictionary<string, string> fieldsMapping = this.DropshipFieldsMapping;
    return ((IEnumerable<string>) sortColumns).Select<string, string>((Func<string, string>) (col => !fieldsMapping.ContainsKey(col) ? col : fieldsMapping[col])).ToArray<string>();
  }

  protected virtual PXFilterRow[] AlterDropshipFilters(PXView.PXFilterRowCollection filters)
  {
    List<PXFilterRow> pxFilterRowList1 = new List<PXFilterRow>();
    Dictionary<string, string> dropshipFieldsMapping = this.DropshipFieldsMapping;
    foreach (PXFilterRow filter in filters)
    {
      List<PXFilterRow> pxFilterRowList2 = pxFilterRowList1;
      PXFilterRow pxFilterRow;
      if (!dropshipFieldsMapping.ContainsKey(filter.DataField))
      {
        pxFilterRow = filter;
      }
      else
      {
        pxFilterRow = new PXFilterRow(filter);
        pxFilterRow.DataField = dropshipFieldsMapping[filter.DataField];
      }
      pxFilterRowList2.Add(pxFilterRow);
    }
    return pxFilterRowList1.ToArray();
  }

  public class WellKnownActions
  {
    public class SOShipmentScreen
    {
      public const string ScreenID = "SO302000";
      public const string ConfirmShipment = "SO302000$confirmShipmentAction";
      public const string CreateInvoice = "SO302000$createInvoice";
      public const string CreateDropshipInvoice = "SO302000$createDropshipInvoice";
      public const string UpdateIN = "SO302000$updateIN";
      public const string PrintLabels = "SO302000$printLabels";
      public const string PrintCommercialInvoices = "SO302000$printCommercialInvoices";
      public const string EmailShipment = "SO302000$emailShipment";
      public const string PrintPickList = "SO302000$printPickListAction";
      public const string PrintShipmentConfirmation = "SO302000$printShipmentConfirmation";
      public const string GetReturnLabels = "SO302000$getReturnLabelsAction";

      public class confirmShipment : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        SOInvoiceShipment.WellKnownActions.SOShipmentScreen.confirmShipment>
      {
        public confirmShipment()
          : base("SO302000$confirmShipmentAction")
        {
        }
      }
    }
  }
}
