// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using Autofac;
using PX.Api.Export;
using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.CRCaseMaint_Extensions;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.OpportunityMaint_Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.Extensions.SalesTax;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.FS;

public class ServiceOrderEntry : 
  ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>,
  IGraphWithInitialization
{
  /// <summary>
  /// This allows to have access to the Appointment document that began the Save operation in ServiceOrderEntry.
  /// </summary>
  public AppointmentEntry GraphAppointmentEntryCaller;
  public bool SkipTaxCalcTotals;
  public bool ForceAppointmentCheckings;
  public bool DisableServiceOrderUnboundFieldCalc;
  public bool RunningPersist;
  public bool IsOrderDateFieldUpdated;
  public List<PXResult<FSAppointmentDet>> LastRefAppointmentDetails;
  private PXGraph dummyGraph;
  private bool updateSOCstmAssigneeEmpID;
  public bool allowCustomerChange;
  protected bool updateContractPeriod;
  private FSSelectorHelper fsSelectorHelper;
  private ExpenseClaimDetailEntry _expenseClaimDetailGraph;
  public PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> ServiceOrderRecords;
  [PXViewName("Answers")]
  public FSAttributeList<FSServiceOrder> Answers;
  [PXViewName("Service Order")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (FSServiceOrder.allowInvoice)})]
  public ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.CurrentServiceOrder_View CurrentServiceOrder;
  [PXHidden]
  public PXSelect<FSPostInfo, Where<FSPostInfo.sOID, Equal<Current<FSServiceOrder.sOID>>>> PostInfoDetails;
  [PXHidden]
  public PXSetup<PX.Objects.CR.BAccount>.Where<Where<PX.Objects.CR.BAccount.bAccountID, Equal<Optional<FSServiceOrder.customerID>>>> BAccount;
  [PXHidden]
  public PXSetup<PX.Objects.AR.Customer>.Where<Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<FSServiceOrder.billCustomerID>>>> TaxCustomer;
  [PXHidden]
  public PXSetup<PX.Objects.CR.Location>.Where<Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<FSServiceOrder.billLocationID>>>>> TaxLocation;
  [PXHidden]
  public PXSetup<PX.Objects.TX.TaxZone>.Where<Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<FSServiceOrder.taxZoneID>>>> TaxZone;
  [PXViewName("Field Service Contact")]
  public ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.FSContact_View ServiceOrder_Contact;
  [PXViewName("Field Service Address")]
  public ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.FSAddress_View ServiceOrder_Address;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>> ContractRelatedToProject;
  [PXViewName("Service Order Type")]
  public PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>> ServiceOrderTypeSelected;
  public PXSetup<FSBranchLocation>.Where<Where<FSBranchLocation.branchLocationID, Equal<Current<FSServiceOrder.branchLocationID>>>> CurrentBranchLocation;
  public PXSetup<PX.Objects.SO.SOOrderType>.Where<Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.postOrderType>>>> postSOOrderTypeSelected;
  public PXSetup<PX.Objects.SO.SOOrderType>.Where<Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>> AllocationSOOrderTypeSelected;
  [PXHidden]
  public PXSetup<PX.Objects.AR.Customer>.Where<Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<FSServiceOrder.billCustomerID>>>> BillCustomer;
  [PXHidden]
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<FSServiceOrder.curyInfoID>>>> currencyInfoView;
  [PXFilterable(new System.Type[] {})]
  [PXImport(typeof (FSServiceOrder))]
  [PXViewName("Service Order Details")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (FSSODet.status), typeof (FSSODet.curyBillableExtPrice), typeof (FSSODet.curyBillableTranAmt)})]
  public ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.ServiceOrderDetailsOrdered ServiceOrderDetails;
  [PXViewName("Appointments in Service Order")]
  [PXCopyPasteHiddenView]
  public ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.ServiceOrderAppointments_View ServiceOrderAppointments;
  [PXViewName("Service Order Employees")]
  public ServiceOrderEntry.ServiceOrderEmployees_View ServiceOrderEmployees;
  [PXViewName("Service Order Equipment")]
  public ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.ServiceOrderEquipment_View ServiceOrderEquipment;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> sitestatusview;
  public PXSelect<INItemSite> initemsite;
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Current<FSSODet.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Current<FSSODet.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Current<FSSODet.lineNbr>>>>>> Splits;
  public PXSelect<INTranSplit> intransplit;
  [PXFilterable(new System.Type[] {})]
  public PXFilter<SrvOrderTypeAux> ServiceOrderTypeSelector;
  public ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.RelatedServiceOrders_View RelatedServiceOrders;
  [PXViewName("Service Order Posting Info")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSPostDet, InnerJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>>, InnerJoin<FSPostBatch, On<FSPostBatch.batchID, Equal<FSPostDet.batchID>>>>, Where2<Where<FSSODet.sOID, Equal<Current<FSServiceOrder.sOID>>>, And<Where<FSPostDet.aRPosted, Equal<True>, Or<FSPostDet.aPPosted, Equal<True>, Or<FSPostDet.sOPosted, Equal<True>, Or<FSPostDet.pMPosted, Equal<True>, Or<FSPostDet.sOInvPosted, Equal<True>, Or<FSPostDet.iNPosted, Equal<True>>>>>>>>>, OrderBy<Desc<FSPostDet.batchID, Desc<FSPostDet.aRPosted, Desc<FSPostDet.aPPosted, Desc<FSPostDet.sOPosted, Desc<FSPostDet.pMPosted, Desc<FSPostDet.sOInvPosted, Desc<FSPostDet.iNPosted>>>>>>>>> ServiceOrderPostedIn;
  [PXCopyPasteHiddenView]
  public PXSelect<FSBillHistory, Where<FSBillHistory.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSBillHistory.serviceOrderRefNbr, Equal<Current<FSServiceOrder.refNbr>>, And<FSBillHistory.appointmentRefNbr, IsNull>>>, OrderBy<Desc<FSBillHistory.createdDateTime>>> InvoiceRecords;
  [PXCopyPasteHiddenView]
  public PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Current<FSServiceOrder.scheduleID>>>> ScheduleRecord;
  public PXSetup<FSServiceContract>.Where<Where<FSServiceContract.serviceContractID, Equal<Current<FSServiceOrder.billServiceContractID>>>> BillServiceContractRelated;
  public PXSetup<FSContractPeriod>.Where<Where<FSContractPeriod.contractPeriodID, Equal<Current<FSServiceOrder.billContractPeriodID>>, And<FSContractPeriod.serviceContractID, Equal<Current<FSServiceOrder.billServiceContractID>>, And<Current<FSBillingCycle.billingBy>, Equal<ListField_Billing_By.ServiceOrder>>>>> BillServiceContractPeriod;
  public PXSelect<FSContractPeriodDet, Where<FSContractPeriodDet.contractPeriodID, Equal<Current<FSContractPeriod.contractPeriodID>>, And<FSContractPeriodDet.serviceContractID, Equal<Current<FSContractPeriod.serviceContractID>>>>> BillServiceContractPeriodDetail;
  public PXSelect<FSAppointmentDet> apptDetView;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly2<ARPayment, InnerJoin<FSAdjust, On<ARPayment.docType, Equal<FSAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<FSAdjust.adjgRefNbr>>>>, Where<FSAdjust.adjdOrderType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSAdjust.adjdOrderNbr, Equal<Current<FSServiceOrder.refNbr>>, And<ARPayment.status, NotEqual<ARDocStatus.voided>>>>> Adjustments;
  [PXCopyPasteHiddenView]
  public PXSelect<FSServiceOrderTax, Where<FSServiceOrderTax.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSServiceOrderTax.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>, OrderBy<Asc<FSServiceOrderTax.taxID>>> TaxLines;
  [PXViewName("Service Order Tax")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSServiceOrderTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<FSServiceOrderTaxTran.taxID>>>, Where<FSServiceOrderTaxTran.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSServiceOrderTaxTran.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>, OrderBy<Asc<FSServiceOrderTaxTran.taxID, Asc<FSServiceOrderTaxTran.recordID>>>> Taxes;
  public TreeWFStageHelper.TreeWFStageView TreeWFStages;
  public PXAction<FSServiceOrder> report;
  public PXAction<FSServiceOrder> scheduleAppointment;
  public PXAction<FSServiceOrder> openSource;
  public PXAction<FSServiceOrder> openServiceOrderScreen;
  public PXAction<FSServiceOrder> createPurchaseOrder;
  public PXInitializeState<FSServiceOrder> initializeState;
  public PXAction<FSServiceOrder> putOnHold;
  public PXAction<FSServiceOrder> releaseFromHold;
  public PXAction<FSServiceOrder> confirmQuote;
  public PXAction<FSServiceOrder> completeOrder;
  public PXAction<FSServiceOrder> cancelOrder;
  public PXAction<FSServiceOrder> closeOrder;
  public PXAction<FSServiceOrder> uncloseOrder;
  public PXAction<FSServiceOrder> invoiceOrder;
  public PXAction<FSServiceOrder> reopenOrder;
  public PXAction<FSServiceOrder> viewDirectionOnMap;
  public PXAction<FSServiceOrder> validateAddress;
  public PXAction<FSServiceOrder> openEmployeeBoard;
  public PXAction<FSServiceOrder> OpenRoomBoard;
  public PXAction<FSServiceOrder> openUserCalendar;
  public PXAction<FSServiceOrder> createNewCustomer;
  public PXAction<FSServiceOrder> copyToServiceOrder;
  public PXAction<FSServiceOrder> OpenPostingDocument;
  public PXAction<FSServiceOrder> printServiceOrder;
  public PXAction<FSServiceOrder> printServiceTimeActivityReport;
  public PXAction<FSServiceOrder> serviceOrderAppointmentsReport;
  public PXAction<FSServiceOrder> allowBilling;
  public PXAction<FSServiceOrder> viewPayment;
  public PXAction<FSServiceOrder> createPrepayment;
  public PXAction<FSServiceOrder> OpenScheduleScreen;
  public ViewLinkedDoc<FSServiceOrder, FSSODet> viewLinkedDoc;
  public PXAction<FSServiceOrder> addReceipt;
  public PXAction<FSServiceOrder> addNewContact;
  public PXAction<FSServiceOrder> billReversal;
  public ViewPostBatch<FSServiceOrder> openPostBatch;
  public PXAction<FSServiceOrder> addBill;
  public PXWorkflowEventHandler<FSServiceOrder> OnServiceOrderDeleted;
  public PXWorkflowEventHandler<FSServiceOrder> OnServiceContractCleared;
  public PXWorkflowEventHandler<FSServiceOrder> OnServiceContractPeriodAssigned;
  public PXWorkflowEventHandler<FSServiceOrder> OnServiceContractPeriodCleared;
  public PXWorkflowEventHandler<FSServiceOrder> OnRequiredServiceContractPeriodCleared;
  public PXWorkflowEventHandler<FSServiceOrder> OnLastAppointmentCompleted;
  public PXWorkflowEventHandler<FSServiceOrder> OnLastAppointmentCanceled;
  public PXWorkflowEventHandler<FSServiceOrder> OnLastAppointmentClosed;
  public PXWorkflowEventHandler<FSServiceOrder> OnAppointmentReOpened;
  public PXWorkflowEventHandler<FSServiceOrder> OnAppointmentUnclosed;
  public PXWorkflowEventHandler<FSServiceOrder> OnAppointmentEdit;
  [PXCopyPasteHiddenView]
  public PXFilter<StaffSelectionFilter> StaffSelectorFilter;
  [PXCopyPasteHiddenView]
  public StaffSelectionHelper.SkillRecords_View SkillGridFilter;
  [PXCopyPasteHiddenView]
  public StaffSelectionHelper.LicenseTypeRecords_View LicenseTypeGridFilter;
  [PXCopyPasteHiddenView]
  public StaffSelectionHelper.StaffRecords_View StaffRecords;
  public PXAction<FSServiceOrder> openStaffSelectorFromServiceTab;
  public PXAction<FSServiceOrder> openStaffSelectorFromStaffTab;

  protected Dictionary<FSAppointment, string> AppointmentsWithErrors { get; set; }

  public bool RecalculateExternalTaxesSync { get; set; }

  public virtual void RecalculateExternalTaxes()
  {
  }

  public void SkipTaxCalcAndSave()
  {
    if (((PXGraph) this).GetExtension<ServiceOrderEntryExternalTax>() != null)
      ((PXGraph) this).GetExtension<ServiceOrderEntryExternalTax>().SkipTaxCalcAndSave();
    else
      ((PXAction) this.Save).Press();
  }

  public virtual void SkipTaxCalcAndSaveBeforeRunAction(PXCache cache, FSServiceOrder row)
  {
    PXEntryStatus status = cache.GetStatus((object) row);
    if (((!cache.AllowInsert ? 0 : (status == 2 ? 1 : 0)) | (!cache.AllowUpdate ? (false ? 1 : 0) : (status == 1 ? 1 : 0))) == 0)
      return;
    using (new SuppressErpTransactionsScope(true))
      this.SkipTaxCalcAndSave();
  }

  public ServiceOrderEntry()
  {
    FSSetup current = ((PXSelectBase<FSSetup>) this.SetupRecord).Current;
    PXUIFieldAttribute.SetDisplayName<FSxService.actionType>(((PXSelectBase) this.InventoryItemHelper).Cache, "Pickup/Deliver Items");
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual FSServiceOrder CalculateExternalTax(FSServiceOrder fsServiceOrderRow)
  {
    return fsServiceOrderRow;
  }

  public virtual void ClearTaxes(FSServiceOrder serviceOrderRow)
  {
    if (serviceOrderRow == null || !this.IsExternalTax(serviceOrderRow.TaxZoneID))
      return;
    PXView view = ((PXSelectBase) this.Taxes).View;
    object[] objArray1 = new object[1]
    {
      (object) serviceOrderRow
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<FSServiceOrderTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
      ((PXSelectBase<FSServiceOrderTaxTran>) this.Taxes).Delete(PXResult<FSServiceOrderTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    serviceOrderRow.CuryTaxTotal = new Decimal?(0M);
    serviceOrderRow.CuryDocTotal = new Decimal?(this.GetCuryDocTotal(serviceOrderRow.CuryBillableOrderTotal, serviceOrderRow.CuryDiscTot, new Decimal?(0M), new Decimal?(0M)));
  }

  public virtual void Persist()
  {
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    if (this.RecalculateExternalTaxesSync && current != null)
    {
      bool? externalTaxCalculation = current.SkipExternalTaxCalculation;
      bool flag = false;
      if (externalTaxCalculation.GetValueOrDefault() == flag & externalTaxCalculation.HasValue)
      {
        this.RecalculateExternalTaxes();
        ((PXGraph) this).SelectTimeStamp();
      }
    }
    try
    {
      this.RunningPersist = true;
      ((PXGraph) this).Persist();
      using (new SuppressErpTransactionsScope(true))
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          if (current != null)
          {
            bool? nullable = current.ProcessCompleteAction;
            if (nullable.GetValueOrDefault())
              this.CompleteProcess(current);
            nullable = current.ProcessCloseAction;
            if (nullable.GetValueOrDefault())
              this.CloseProcess(current);
            nullable = current.ProcessCancelAction;
            if (nullable.GetValueOrDefault())
              this.CancelProcess(current);
            nullable = current.ProcessReopenAction;
            if (nullable.GetValueOrDefault())
              this.ReopenProcess(current);
          }
          ((PXGraph) this).Persist();
          transactionScope.Complete();
        }
      }
    }
    finally
    {
      this.RunningPersist = false;
      if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current != null)
      {
        current.ProcessCompleteAction = new bool?(false);
        current.CompleteAppointments = new bool?(true);
        current.ProcessCloseAction = new bool?(false);
        current.CompleteAppointments = new bool?(true);
        current.ProcessCancelAction = new bool?(false);
        current.CancelAppointments = new bool?(true);
        current.ProcessReopenAction = new bool?(false);
      }
    }
    if (!this.RecalculateExternalTaxesSync && current != null)
    {
      bool? externalTaxCalculation = current.SkipExternalTaxCalculation;
      bool flag = false;
      if (externalTaxCalculation.GetValueOrDefault() == flag & externalTaxCalculation.HasValue)
      {
        this.RecalculateExternalTaxes();
        ((PXGraph) this).SelectTimeStamp();
        ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.tstamp = ((PXGraph) this).TimeStamp;
      }
    }
    ((PXSelectBase) this.ServiceOrderDetails).Cache.Clear();
    ((PXSelectBase) this.ServiceOrderDetails).View.Clear();
    ((PXSelectBase) this.ServiceOrderDetails).View.RequestRefresh();
  }

  public virtual void CompleteProcess(FSServiceOrder currentServiceOrder)
  {
    FSServiceOrder fsServiceOrderRow = currentServiceOrder;
    string srvOrdType = fsServiceOrderRow.SrvOrdType;
    string refNbr = fsServiceOrderRow.RefNbr;
    if (fsServiceOrderRow.CompleteAppointments.GetValueOrDefault())
    {
      this.CompleteAppointmentsInServiceOrder(this, fsServiceOrderRow);
      this.HardReloadServiceOrderAppointments();
    }
    List<PXResult<FSSODet, FSAppointmentDet>> list = PXSelectBase<FSSODet, PXSelectJoin<FSSODet, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.sODetID, Equal<FSSODet.sODetID>, And<FSAppointmentDet.srvOrdType, Equal<FSSODet.srvOrdType>>>>, Where<FSSODet.refNbr, Equal<Required<FSSODet.refNbr>>, And<FSSODet.srvOrdType, Equal<Required<FSSODet.srvOrdType>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) refNbr,
      (object) srvOrdType
    }).Cast<PXResult<FSSODet, FSAppointmentDet>>().ToList<PXResult<FSSODet, FSAppointmentDet>>();
    List<FSSODet> rows1 = new List<FSSODet>();
    List<FSSODet> rows2 = new List<FSSODet>();
    List<FSSODet> rows3 = new List<FSSODet>();
    foreach (IGrouping<int?, PXResult<FSSODet, FSAppointmentDet>> source1 in list.GroupBy<PXResult<FSSODet, FSAppointmentDet>, int?>((Func<PXResult<FSSODet, FSAppointmentDet>, int?>) (p => PXResult<FSSODet, FSAppointmentDet>.op_Implicit(p).SODetID)))
    {
      FSSODet fssoDet = PXResult<FSSODet, FSAppointmentDet>.op_Implicit(source1.First<PXResult<FSSODet, FSAppointmentDet>>());
      IEnumerable<FSAppointmentDet> source2 = source1.Where<PXResult<FSSODet, FSAppointmentDet>>((Func<PXResult<FSSODet, FSAppointmentDet>, bool>) (p => PXResult<FSSODet, FSAppointmentDet>.op_Implicit(p).AppDetID.HasValue)).Select<PXResult<FSSODet, FSAppointmentDet>, FSAppointmentDet>((Func<PXResult<FSSODet, FSAppointmentDet>, FSAppointmentDet>) (p => PXResult<FSSODet, FSAppointmentDet>.op_Implicit(p)));
      if (!source2.Any<FSAppointmentDet>() || source2.Any<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (p => p.Status == "CP")))
        rows1.Add(fssoDet);
      if (source2.Any<FSAppointmentDet>() && source2.All<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (p => p.Status == "NP" || p.Status == "CC")))
        rows2.Add(fssoDet);
      else if (source2.Any<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (p => p.Status == "NF")))
        rows3.Add(fssoDet);
    }
    this.ChangeItemLineStatus((IEnumerable<FSSODet>) rows1, "CP");
    this.ChangeItemLineStatus((IEnumerable<FSSODet>) rows2, "CC");
    this.ChangeItemLineStatus((IEnumerable<FSSODet>) rows3, "SN");
  }

  public virtual void CloseProcess(FSServiceOrder currentServiceOrder)
  {
    FSServiceOrder fsServiceOrderRow = currentServiceOrder;
    if (currentServiceOrder.CloseAppointments.GetValueOrDefault())
    {
      this.CloseAppointmentsInServiceOrder(this, fsServiceOrderRow);
      this.HardReloadServiceOrderAppointments();
    }
    FSServiceOrder copy = (FSServiceOrder) ((PXSelectBase) this.ServiceOrderRecords).Cache.CreateCopy((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current);
    string billingMode = this.GetBillingMode(copy);
    bool? allowInvoice = copy.AllowInvoice;
    bool flag = false;
    if (allowInvoice.GetValueOrDefault() == flag & allowInvoice.HasValue && billingMode == "SO")
      copy.AllowInvoice = new bool?(true);
    ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(copy);
    this.DeallocateUnusedItems(copy);
  }

  public virtual void CancelProcess(FSServiceOrder serviceOrder)
  {
    if (GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>())).Where<FSSODet>((Func<FSSODet, bool>) (x => x.Status == "CP")).Count<FSSODet>() > 0)
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The service order cannot be canceled because at least one item has the Completed status.", Array.Empty<object>()));
    if (serviceOrder.CancelAppointments.GetValueOrDefault())
      this.CancelAppointmentsInServiceOrder(this, serviceOrder);
    serviceOrder = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    serviceOrder = (FSServiceOrder) ((PXSelectBase) this.ServiceOrderRecords).Cache.CreateCopy((object) serviceOrder);
    serviceOrder.BillServiceContractID = new int?();
    ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(serviceOrder);
    this.ChangeItemLineStatus(GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>())).Where<FSSODet>((Func<FSSODet, bool>) (x => x.Status == "SC" || x.Status == "SN")), "CC");
  }

  public virtual void ReopenProcess(FSServiceOrder currentServiceOrder)
  {
    FSServiceOrder fsServiceOrder = currentServiceOrder;
    if (fsServiceOrder.BillServiceContractID.HasValue)
      ((PXSelectBase) this.ServiceOrderRecords).Cache.SetDefaultExt<FSServiceOrder.billContractPeriodID>((object) fsServiceOrder);
    if (fsServiceOrder.AllowInvoice.GetValueOrDefault())
      fsServiceOrder.AllowInvoice = new bool?(false);
    ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(fsServiceOrder);
    bool? memInvoiced = fsServiceOrder.Mem_Invoiced;
    bool flag = false;
    if (!(memInvoiced.GetValueOrDefault() == flag & memInvoiced.HasValue))
      return;
    foreach (FSSODet fssoDet in GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>())))
    {
      if (!(fssoDet.Status == "CC") && !(fssoDet.Status == "CP"))
      {
        FSSODet copy = (FSSODet) ((PXSelectBase) this.ServiceOrderDetails).Cache.CreateCopy((object) fssoDet);
        ((PXSelectBase) this.ServiceOrderDetails).Cache.SetDefaultExt<FSSODet.status>((object) copy);
        ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Update(copy);
      }
    }
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current != null && current.Behavior == "QT")
    {
      ICollection<(string, string)> excludedFields = (ICollection<(string, string)>) new List<(string, string)>()
      {
        ("ServiceOrderDetails", "AcctID"),
        ("ServiceOrderDetails", "SubID")
      };
      foreach (int index in EnumerableExtensions.SelectIndexesWhere<Command>((IEnumerable<Command>) script, (Func<Command, bool>) (command => excludedFields.Contains((command.ObjectName, command.FieldName)))).Reverse<int>())
      {
        script.RemoveAt(index);
        containers.RemoveAt(index);
      }
    }
    ((PXGraph) this).CopyPasteGetScript(isImportSimple, script, containers);
  }

  private FSSelectorHelper GetFsSelectorHelperInstance
  {
    get
    {
      if (this.fsSelectorHelper == null)
        this.fsSelectorHelper = new FSSelectorHelper();
      return this.fsSelectorHelper;
    }
  }

  protected ExpenseClaimDetailEntry GetExpenseClaimDetailGraph(bool clearGraph)
  {
    if (this._expenseClaimDetailGraph == null)
      this._expenseClaimDetailGraph = PXGraph.CreateInstance<ExpenseClaimDetailEntry>();
    else if (clearGraph)
      ((PXGraph) this._expenseClaimDetailGraph).Clear();
    return this._expenseClaimDetailGraph;
  }

  [PXFormula(typeof (Switch<Case<Where<Current<FSSrvOrdType.behavior>, Equal<ListField.ServiceOrderTypeBehavior.internalAppointment>>, Selector<Current<FSServiceOrder.branchLocationID>, FSBranchLocation.descr>>, Selector<FSServiceOrder.customerID, BAccountSelectorBase.acctName>>))]
  [PXMergeAttributes]
  protected virtual void FSServiceOrder_FormCaptionDescription_CacheAttached(PXCache sender)
  {
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void FSServiceOrder_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Name", Enabled = false)]
  protected virtual void BAccount_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">AAAAAAAAAAAAAAA")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (FSBillingCycle.billingCycleCD))]
  [NormalizeWhiteSpace]
  protected virtual void FSBillingCycle_BillingCycleCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField]
  protected virtual void ARPayment_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void FSSODet_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  protected virtual void _(PX.Data.Events.CacheAttached<FSAppointmentDet.tranDate> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSAppointment.lineCntr))]
  [PXCheckUnique(new System.Type[] {typeof (FSAppointment.appointmentID)}, Where = typeof (Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointment.refNbr>>>>), UniqueKeyIsPartOfPrimaryKey = true, ClearOnDuplicate = false)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  [PXFormula(null, typeof (MaxCalc<FSAppointment.maxLineNbr>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<FSAppointmentDet.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Invoice Total")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FSAppointment.appCompletedBillableTotal> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(PX.Data.Events.CacheAttached<FSAddress.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(PX.Data.Events.CacheAttached<FSAddress.longitude> e)
  {
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public IEnumerable invoiceRecords()
  {
    return (IEnumerable) ((IEnumerable<PXResult<FSBillHistory>>) PXSelectBase<FSBillHistory, PXSelect<FSBillHistory, Where<FSBillHistory.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSBillHistory.serviceOrderRefNbr, Equal<Current<FSServiceOrder.refNbr>>>>, OrderBy<Desc<FSBillHistory.createdDateTime>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AsEnumerable<PXResult<FSBillHistory>>().Where<PXResult<FSBillHistory>>((Func<PXResult<FSBillHistory>, bool>) (rec => !((PXResult) rec).GetItem<FSBillHistory>().IsChildDocDeleted.GetValueOrDefault())).ToList<PXResult<FSBillHistory>>();
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<FSServiceOrder>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (FSSODet), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<FSSODet.srvOrdType>((PXDbType) 3, (object) ((PXSelectBase<FSServiceOrder>) ((ServiceOrderEntry) graph).ServiceOrderRecords).Current?.SrvOrdType),
        (PXDataFieldValue) new PXDataFieldValue<FSSODet.refNbr>((object) ((PXSelectBase<FSServiceOrder>) ((ServiceOrderEntry) graph).ServiceOrderRecords).Current?.RefNbr)
      }))
    });
  }

  public virtual IEnumerable profitabilityRecords()
  {
    return this.GetProfitabilityRecords(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current);
  }

  public virtual IEnumerable GetProfitabilityRecords(FSServiceOrder row)
  {
    if (this.dummyGraph == null)
      this.dummyGraph = new PXGraph();
    return (IEnumerable) this.ProfitabilityRecords_INItems(this.dummyGraph, row, (FSAppointment) null).Concat<FSProfitability>((IEnumerable<FSProfitability>) this.ProfitabilityRecords_Logs(this.dummyGraph, row));
  }

  public virtual Decimal CalcEffectiveCostTotal(FSServiceOrder order)
  {
    if (this.dummyGraph == null)
      this.dummyGraph = PXGraph.CreateInstance<PXGraph>();
    IEnumerable<FSProfitability> source = this.ProfitabilityRecords_INItems(this.dummyGraph, order, (FSAppointment) null);
    PXResultset<FSAppointment> pxResultset = PXSelectBase<FSAppointment, PXViewOf<FSAppointment>.BasedOn<SelectFromBase<FSAppointment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSAppointmentLog>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentLog.docType, Equal<FSAppointment.srvOrdType>>>>>.And<BqlOperand<FSAppointmentLog.docRefNbr, IBqlString>.IsEqual<FSAppointment.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.srvOrdType, Equal<P.AsString>>>>, And<BqlOperand<FSAppointment.soRefNbr, IBqlString>.IsEqual<P.AsString>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.inProcess, Equal<True>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.paused, Equal<True>>>>, Or<BqlOperand<FSAppointment.completed, IBqlBool>.IsEqual<True>>>>.Or<BqlOperand<FSAppointment.closed, IBqlBool>.IsEqual<True>>>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointmentLog.bAccountID, IsNotNull>>>>.And<BqlOperand<FSAppointmentLog.curyUnitCost, IBqlDecimal>.IsNotEqual<decimal0>>>>.Aggregate<To<Sum<FSAppointmentLog.curyExtCost>>>>.Config>.Select(this.dummyGraph, new object[2]
    {
      (object) order.SrvOrdType,
      (object) order.RefNbr
    });
    return source.Sum<FSProfitability>((Func<FSProfitability, Decimal>) (it => it.CuryExtCost ?? 0.0M)) + GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) pxResultset).Sum<FSAppointmentLog>((Func<FSAppointmentLog, Decimal>) (log => log.CuryExtCost ?? 0.0M));
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (INSiteStatusByCostCenter), typeof (INSiteStatusByCostCenter));
    ((PXGraph) this).Caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
  }

  protected virtual IEnumerable treeWFStages([PXInt] int? wFStageID)
  {
    return ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current == null ? (IEnumerable) null : TreeWFStageHelper.treeWFStages((PXGraph) this, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.SrvOrdType, wFStageID);
  }

  [PXButton]
  [PXUIField(DisplayName = "Reports")]
  public virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (!string.IsNullOrEmpty(reportID))
    {
      ((PXAction) this.Save).Press();
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      PXReportRequiredException ex = (PXReportRequiredException) null;
      Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
      foreach (FSServiceOrder fsServiceOrder in list)
      {
        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
        dictionary2["FSServiceOrder.SrvOrdType"] = fsServiceOrder.SrvOrdType;
        dictionary2["FSServiceOrder.RefNbr"] = fsServiceOrder.RefNbr;
        string str = new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, fsServiceOrder.CustomerID, fsServiceOrder.BranchID);
        ex = PXReportRequiredException.CombineReport(ex, str, dictionary2, (CurrentLocalization) null);
        reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, dictionary2, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, str, fsServiceOrder.BranchID, (CurrentLocalization) null);
      }
      if (ex != null)
        ((PXGraph) this).LongOperationManager.StartOperation((Action<CancellationToken>) (async ct =>
        {
          int num = await SMPrintJobMaint.CreatePrintJobGroups(reportsToPrint, ct) ? 1 : 0;
          throw ex;
        }));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ScheduleAppointment(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    using (List<FSServiceOrder>.Enumerator enumerator = list.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        FSServiceOrder current = enumerator.Current;
        this.ValidateContact(current);
        this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, current);
        if (SharedFunctions.isThisAProspect((PXGraph) this, current.CustomerID))
          throw new PXException("Error");
        AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
        FSAppointment fsAppointment = new FSAppointment()
        {
          SrvOrdType = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.SrvOrdType,
          SOID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.SOID
        };
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Insert(fsAppointment);
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.soRefNbr>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) current.RefNbr);
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).SetValueExt<FSAppointment.customerID>(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current, (object) current.CustomerID);
        throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenSource(PXAdapter adapter)
  {
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    if (current != null)
    {
      switch (current.SourceType)
      {
        case "CR":
          CRCaseMaint instance1 = PXGraph.CreateInstance<CRCaseMaint>();
          CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.caseCD, Equal<Required<CRCase.caseCD>>>>.Config>.Select((PXGraph) instance1, new object[1]
          {
            (object) current.SourceRefNbr
          }));
          if (crCase != null)
          {
            ((PXSelectBase<CRCase>) instance1.Case).Current = crCase;
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          break;
        case "OP":
          OpportunityMaint instance2 = PXGraph.CreateInstance<OpportunityMaint>();
          CROpportunity crOpportunity = PXResultset<CROpportunity>.op_Implicit(PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.opportunityID, Equal<Required<CROpportunity.opportunityID>>>>.Config>.Select((PXGraph) instance2, new object[1]
          {
            (object) current.SourceRefNbr
          }));
          if (crOpportunity != null)
          {
            ((PXSelectBase<CROpportunity>) instance2.Opportunity).Current = crOpportunity;
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance2, (string) null);
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          break;
        case "SO":
          SOOrderEntry instance3 = PXGraph.CreateInstance<SOOrderEntry>();
          PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) instance3, new object[2]
          {
            (object) current.SourceDocType,
            (object) current.SourceRefNbr
          }));
          if (soOrder != null)
          {
            ((PXSelectBase<PX.Objects.SO.SOOrder>) instance3.Document).Current = soOrder;
            PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance3, (string) null);
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException;
          }
          break;
        case "SD":
          ServiceOrderEntry instance4 = PXGraph.CreateInstance<ServiceOrderEntry>();
          ((PXSelectBase<FSServiceOrder>) instance4.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance4.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) current.SourceRefNbr, new object[1]
          {
            (object) current.SourceDocType
          }));
          PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance4, (string) null);
          ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
          throw requiredException1;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void OpenServiceOrderScreen()
  {
    if (((PXSelectBase<RelatedServiceOrder>) this.RelatedServiceOrders).Current != null)
    {
      ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
      ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) ((PXSelectBase<RelatedServiceOrder>) this.RelatedServiceOrders).Current.RefNbr, new object[1]
      {
        (object) ((PXSelectBase<RelatedServiceOrder>) this.RelatedServiceOrders).Current.SrvOrdType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreatePurchaseOrder(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (!adapter.MassProcess)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      POCreate instance = PXGraph.CreateInstance<POCreate>();
      FSxPOCreateFilter extension = ((PXSelectBase) instance.Filter).Cache.GetExtension<FSxPOCreateFilter>((object) ((PXSelectBase<POCreate.POCreateFilter>) instance.Filter).Current);
      extension.SrvOrdType = list[0].SrvOrdType;
      extension.ServiceOrderRefNbr = list[0].RefNbr;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Confirm")]
  protected virtual IEnumerable ConfirmQuote(PXAdapter adapter) => adapter.Get();

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    ((PXSelectBase) this.InvoiceRecords).Cache.ClearQueryCache();
    ((PXSelectBase) this.ServiceOrderDetails).Cache.ClearQueryCache();
    return ((PXAction) new PXCancel<FSServiceOrder>((PXGraph) this, nameof (Cancel))).Press(a);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CompleteOrder(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (list.Count > 0)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      foreach (FSServiceOrder fsServiceOrder in list)
      {
        try
        {
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current = fsServiceOrder;
          fsServiceOrder.ProcessCompleteAction = new bool?(true);
          fsServiceOrder.CompleteAppointments = new bool?(true);
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(fsServiceOrder);
          this.SkipTaxCalcAndSave();
        }
        finally
        {
          fsServiceOrder.CompleteActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CancelOrder(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (list.Count > 0)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      foreach (FSServiceOrder fsServiceOrder in list)
      {
        try
        {
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current = fsServiceOrder;
          fsServiceOrder.ProcessCancelAction = new bool?(true);
          fsServiceOrder.CancelAppointments = new bool?(true);
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(fsServiceOrder);
          this.SkipTaxCalcAndSave();
        }
        finally
        {
          fsServiceOrder.CancelActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CloseOrder(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (list.Count > 0)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      foreach (FSServiceOrder fsServiceOrder in list)
      {
        try
        {
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current = fsServiceOrder;
          bool? nullable = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.AlertBeforeCloseServiceOrder;
          int num;
          if (nullable.GetValueOrDefault())
          {
            nullable = fsServiceOrder.IsCalledFromQuickProcess;
            if (!nullable.GetValueOrDefault())
            {
              num = !adapter.MassProcess ? 1 : 0;
              goto label_8;
            }
          }
          num = 0;
label_8:
          fsServiceOrder.CloseAppointments = new bool?(false);
          fsServiceOrder.UserConfirmedClosing = new bool?(true);
          fsServiceOrder.ProcessCloseAction = new bool?(true);
          ((PXSelectBase) this.ServiceOrderAppointments).Cache.ClearQueryCache();
          if (num != 0 && !adapter.MassProcess && ((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300100") && GraphHelper.RowCast<FSAppointment>((IEnumerable) ((PXSelectBase<FSAppointment>) this.ServiceOrderAppointments).Select(Array.Empty<object>())).Where<FSAppointment>((Func<FSAppointment, bool>) (x =>
          {
            if (!x.Completed.GetValueOrDefault())
              return false;
            bool? closed = x.Closed;
            bool flag = false;
            return closed.GetValueOrDefault() == flag & closed.HasValue;
          })).Count<FSAppointment>() > 0)
          {
            if (6 == ((PXSelectBase) this.ServiceOrderRecords).View.Ask("Confirm Service Order Closing", "This Service Order still has open Appointments. If you close the Service Order its appointments will also be closed. Do you want to proceed?", (MessageButtons) 4))
            {
              fsServiceOrder.UserConfirmedClosing = new bool?(true);
              fsServiceOrder.CloseAppointments = new bool?(true);
            }
            else
            {
              fsServiceOrder.UserConfirmedClosing = new bool?(false);
              fsServiceOrder.ProcessCloseAction = new bool?(false);
              fsServiceOrder.CloseAppointments = new bool?(false);
            }
          }
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(fsServiceOrder);
          this.SkipTaxCalcAndSave();
        }
        finally
        {
          fsServiceOrder.CloseActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  public virtual object UpdateKeepingNotchangedStatus(PXCache cache, object row)
  {
    if (row == null)
      return (object) null;
    PXEntryStatus status = cache.GetStatus(row);
    object obj = cache.Update(row);
    if (status != null)
      return obj;
    cache.SetStatus(row, status);
    return obj;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable UncloseOrder(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    foreach (FSServiceOrder row in list)
    {
      try
      {
        if (adapter.MassProcess)
        {
          if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Ask("Confirm Service Order Unclosing", "The current service order will be unclosed. Do you want to proceed?", (MessageButtons) 4) != 6)
            continue;
        }
        row.UserConfirmedUnclosing = new bool?(true);
        ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current = (FSServiceOrder) this.UpdateKeepingNotchangedStatus(((PXSelectBase) this.ServiceOrderRecords).Cache, (object) row);
        this.SkipTaxCalcAndSave();
      }
      finally
      {
        row.UnCloseActionRunning = new bool?(false);
      }
    }
    return (IEnumerable) list;
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable InvoiceOrder(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceOrderEntry.\u003C\u003Ec__DisplayClass126_0 displayClass1260 = new ServiceOrderEntry.\u003C\u003Ec__DisplayClass126_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1260.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1260.adapter = adapter;
    // ISSUE: reference to a compiler-generated field
    List<FSServiceOrder> list = displayClass1260.adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    // ISSUE: reference to a compiler-generated field
    displayClass1260.rows = new List<ServiceOrderToPost>();
    // ISSUE: reference to a compiler-generated field
    if (!displayClass1260.adapter.MassProcess && list.Count > 0)
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
    foreach (FSServiceOrder fsServiceOrder in list)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ServiceOrderEntry.\u003C\u003Ec__DisplayClass126_1 displayClass1261 = new ServiceOrderEntry.\u003C\u003Ec__DisplayClass126_1();
      // ISSUE: reference to a compiler-generated field
      displayClass1261.CS\u0024\u003C\u003E8__locals1 = displayClass1260;
      // ISSUE: reference to a compiler-generated field
      displayClass1261.fSServiceOrder = fsServiceOrder;
      // ISSUE: reference to a compiler-generated field
      bool? nullable = displayClass1261.fSServiceOrder.AllowInvoice;
      if (nullable.GetValueOrDefault())
      {
        // ISSUE: reference to a compiler-generated field
        nullable = displayClass1261.fSServiceOrder.WaitingForParts;
        if (nullable.GetValueOrDefault())
          throw new PXSetPropertyException("The service order cannot be billed because it contains items that are waiting to be purchased.");
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1261, __methodptr(\u003CInvoiceOrder\u003Eb__0)));
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReopenOrder(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (list.Count > 0)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      foreach (FSServiceOrder fsServiceOrder in list)
      {
        try
        {
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current = fsServiceOrder;
          fsServiceOrder.ProcessReopenAction = new bool?(true);
          ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(fsServiceOrder);
          this.SkipTaxCalcAndSave();
        }
        finally
        {
          fsServiceOrder.ReopenActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewDirectionOnMap()
  {
    FSAddress aAddr = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).SelectSingle(Array.Empty<object>());
    if (aAddr == null)
      return;
    BAccountUtility.ViewOnMap<FSAddress, FSAddress.countryID>((IAddress) aAddr);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddress(PXAdapter adapter)
  {
    ServiceOrderEntry aGraph = this;
    foreach (FSServiceOrder fsServiceOrder in adapter.Get<FSServiceOrder>())
    {
      if (fsServiceOrder != null)
      {
        FSAddress aAddress = PXResultset<FSAddress>.op_Implicit(((PXSelectBase<FSAddress>) aGraph.ServiceOrder_Address).Select(Array.Empty<object>()));
        if (aAddress != null)
        {
          bool? nullable = aAddress.IsDefaultAddress;
          bool flag1 = false;
          if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
          {
            nullable = aAddress.IsValidated;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
              PXAddressValidator.Validate<FSAddress>((PXGraph) aGraph, aAddress, true, true);
          }
        }
      }
      yield return (object) fsServiceOrder;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual void OpenEmployeeBoard()
  {
    if (SharedFunctions.isThisAProspect((PXGraph) this, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.CustomerID))
      return;
    this.SkipTaxCalcAndSave();
    this.OpenEmployeeBoard_Handler((PXGraph) this, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current);
  }

  [PXUIField]
  [PXButton]
  public virtual void openRoomBoard()
  {
    this.SkipTaxCalcAndSave();
    throw new PXRedirectToBoardRequiredException("pages/fs/calendars/MultiRoomDispatch/FS300700.aspx", this.GetServiceOrderUrlArguments(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current), (PXBaseRedirectException.WindowMode) 1);
  }

  [PXUIField]
  [PXButton]
  public virtual void OpenUserCalendar()
  {
    if (!SharedFunctions.isThisAProspect((PXGraph) this, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.CustomerID))
    {
      this.SkipTaxCalcAndSave();
      throw new PXRedirectToBoardRequiredException("pages/fs/calendars/SingleEmpDispatch/FS300400.aspx", this.GetServiceOrderUrlArguments(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current), (PXBaseRedirectException.WindowMode) 1);
    }
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void CreateNewCustomer()
  {
    CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
    ((PXSelectBase<PX.Objects.AR.Customer>) instance.CurrentCustomer).Insert(new PX.Objects.AR.Customer());
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField]
  public virtual void CopyToServiceOrder()
  {
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current;
    if (((PXSelectBase<SrvOrderTypeAux>) this.ServiceOrderTypeSelector).AskExt() == 1 && ((PXSelectBase<SrvOrderTypeAux>) this.ServiceOrderTypeSelector).Current.SrvOrdType != null)
    {
      ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
      FSServiceOrder serviceOrderCleanCopy = this.CreateServiceOrderCleanCopy(current);
      FSServiceOrder fsServiceOrder1 = current;
      fsServiceOrder1.Copied = new bool?(true);
      fsServiceOrder1.Status = "V";
      ((PXSelectBase) instance.ServiceOrderRecords).Cache.SetStatus((object) fsServiceOrder1, (PXEntryStatus) 1);
      string upper = ((PXSelectBase<SrvOrderTypeAux>) this.ServiceOrderTypeSelector).Current.SrvOrdType.ToUpper();
      serviceOrderCleanCopy.Quote = new bool?(false);
      serviceOrderCleanCopy.SrvOrdType = upper;
      serviceOrderCleanCopy.SourceType = "SD";
      serviceOrderCleanCopy.SourceDocType = current.SrvOrdType;
      serviceOrderCleanCopy.SourceRefNbr = current.RefNbr;
      serviceOrderCleanCopy.WFStageID = new int?();
      if (serviceOrderCleanCopy.ProblemID.HasValue)
      {
        if (((IQueryable<PXResult<FSSrvOrdTypeProblem>>) PXSelectBase<FSSrvOrdTypeProblem, PXViewOf<FSSrvOrdTypeProblem>.BasedOn<SelectFromBase<FSSrvOrdTypeProblem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSrvOrdTypeProblem.srvOrdType, Equal<P.AsString>>>>>.And<BqlOperand<FSSrvOrdTypeProblem.problemID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) upper,
          (object) serviceOrderCleanCopy.ProblemID
        })).Any<PXResult<FSSrvOrdTypeProblem>>())
          goto label_4;
      }
      serviceOrderCleanCopy.ProblemID = new int?();
label_4:
      FSServiceOrder fsServiceOrder2 = ((PXSelectBase<FSServiceOrder>) instance.CurrentServiceOrder).Current = ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Insert(serviceOrderCleanCopy);
      PXCache cache1 = ((PXSelectBase) instance.CurrentServiceOrder).Cache;
      cache1.SetValueExt<FSServiceOrder.branchID>((object) fsServiceOrder2, (object) current.BranchID);
      cache1.SetValueExt<FSServiceOrder.branchLocationID>((object) fsServiceOrder2, (object) current.BranchLocationID);
      cache1.SetValueExt<FSServiceOrder.customerID>((object) fsServiceOrder2, (object) current.CustomerID);
      cache1.SetValueExt<FSServiceOrder.locationID>((object) fsServiceOrder2, (object) current.LocationID);
      cache1.SetValueExt<FSServiceOrder.contactID>((object) fsServiceOrder2, (object) current.ContactID);
      cache1.SetValueExt<FSServiceOrder.serviceOrderAddressID>((object) fsServiceOrder2, (object) current.ServiceOrderAddressID);
      cache1.SetValueExt<FSServiceOrder.billCustomerID>((object) fsServiceOrder2, (object) current.BillCustomerID);
      cache1.SetValueExt<FSServiceOrder.billLocationID>((object) fsServiceOrder2, (object) current.BillLocationID);
      cache1.SetValueExt<FSServiceOrder.projectID>((object) fsServiceOrder2, (object) current.ProjectID);
      cache1.SetValueExt<FSServiceOrder.dfltProjectTaskID>((object) fsServiceOrder2, (object) current.DfltProjectTaskID);
      cache1.SetValueExt<FSServiceOrder.curyID>((object) fsServiceOrder2, (object) current.CuryID);
      if (current.Quote.GetValueOrDefault())
        cache1.SetValueExt<FSServiceOrder.billServiceContractID>((object) fsServiceOrder2, (object) current.BillServiceContractID);
      FSContact source1 = ((PXSelectBase<FSContact>) this.ServiceOrder_Contact).SelectSingle(Array.Empty<object>());
      FSContact dest1 = PXResultset<FSContact>.op_Implicit(((PXSelectBase<FSContact>) instance.ServiceOrder_Contact).Select(Array.Empty<object>()));
      InvoiceHelper.CopyContact((IContact) dest1, (IContact) source1);
      FSContact fsContact = ((PXSelectBase<FSContact>) instance.ServiceOrder_Contact).Update(dest1);
      bool? nullable = fsContact.OverrideContact;
      if (nullable.GetValueOrDefault())
      {
        PXCache cache2 = ((PXSelectBase) instance.ServiceOrder_Contact).Cache;
        cache2.SetValueExt<FSContact.overrideContact>((object) fsContact, (object) true);
        cache2.SetStatus((object) fsContact, (PXEntryStatus) 0);
      }
      FSAddress source2 = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).SelectSingle(Array.Empty<object>());
      FSAddress dest2 = PXResultset<FSAddress>.op_Implicit(((PXSelectBase<FSAddress>) instance.ServiceOrder_Address).Select(Array.Empty<object>()));
      InvoiceHelper.CopyAddress((IAddress) dest2, (IAddress) source2);
      FSAddress fsAddress = ((PXSelectBase<FSAddress>) instance.ServiceOrder_Address).Update(dest2);
      nullable = fsAddress.OverrideAddress;
      if (nullable.GetValueOrDefault())
      {
        PXCache cache3 = ((PXSelectBase) instance.ServiceOrder_Address).Cache;
        cache3.SetValueExt<FSAddress.overrideAddress>((object) fsAddress, (object) true);
        cache3.SetStatus((object) fsAddress, (PXEntryStatus) 0);
      }
      foreach (PXResult<FSSODet> pxResult in ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()))
      {
        FSSODet objSourceRow = PXResult<FSSODet>.op_Implicit(pxResult);
        FSSODet objNewRow = new FSSODet();
        FSSODet fsSODetRow = AppointmentEntry.InsertDetailLine<FSSODet, FSSODet>(((PXSelectBase) instance.ServiceOrderDetails).Cache, (object) objNewRow, ((PXSelectBase) this.ServiceOrderDetails).Cache, (object) objSourceRow, new Guid?(), new int?(), false, objSourceRow.TranDate, true, true);
        fsSODetRow.Status = this.CalculateLineStatus(fsSODetRow);
        PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) objSourceRow, ((PXSelectBase) instance.ServiceOrderDetails).Cache, (object) fsSODetRow, new bool?(true), new bool?(false));
      }
      instance.Answers.CopyAllAttributes((object) ((PXSelectBase<FSServiceOrder>) instance.CurrentServiceOrder).Current, (object) ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
      throw requiredException;
    }
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField]
  public virtual void openPostingDocument()
  {
    FSPostDet fsPostDet = PXResultset<FSPostDet>.op_Implicit(((PXSelectBase<FSPostDet>) this.ServiceOrderPostedIn).SelectWindowed(0, 1, Array.Empty<object>()));
    if (fsPostDet == null)
      return;
    if (fsPostDet.SOPosted.GetValueOrDefault())
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      {
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) fsPostDet.SOOrderNbr, new object[1]
        {
          (object) fsPostDet.SOOrderType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    else
    {
      if (fsPostDet.ARPosted.GetValueOrDefault())
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) fsPostDet.ARRefNbr, new object[1]
        {
          (object) fsPostDet.ARDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (fsPostDet.SOInvPosted.GetValueOrDefault())
      {
        SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) fsPostDet.SOInvRefNbr, new object[1]
        {
          (object) fsPostDet.SOInvDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (fsPostDet.APPosted.GetValueOrDefault())
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) fsPostDet.APRefNbr, new object[1]
        {
          (object) fsPostDet.APDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (fsPostDet.PMPosted.GetValueOrDefault())
      {
        RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
        ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance.Document).Search<PMRegister.refNbr>((object) fsPostDet.PMRefNbr, new object[1]
        {
          (object) fsPostDet.PMDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
  }

  [PXUIField]
  public virtual IEnumerable PrintServiceOrder(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (!adapter.MassProcess)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      Dictionary<string, string> serviceOrderParameters = this.GetServiceOrderParameters(list[0]);
      if (serviceOrderParameters.Count > 0)
        throw new PXReportRequiredException(serviceOrderParameters, "FS641000", string.Empty, (CurrentLocalization) null);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  public virtual IEnumerable PrintServiceTimeActivityReport(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (!adapter.MassProcess)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      Dictionary<string, string> serviceOrderParameters = this.GetServiceOrderParameters(list[0], true);
      if (serviceOrderParameters.Count > 0)
        throw new PXReportRequiredException(serviceOrderParameters, "FS654500", string.Empty, (CurrentLocalization) null);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  public virtual IEnumerable ServiceOrderAppointmentsReport(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (!adapter.MassProcess)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, list[0]);
      Dictionary<string, string> serviceOrderParameters = this.GetServiceOrderParameters(list[0]);
      if (serviceOrderParameters.Count > 0)
        throw new PXReportRequiredException(serviceOrderParameters, "FS642500", string.Empty, (CurrentLocalization) null);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable AllowBilling(PXAdapter adapter)
  {
    List<FSServiceOrder> list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    foreach (FSServiceOrder row in list)
    {
      this.SkipTaxCalcAndSaveBeforeRunAction(((PXSelectBase) this.ServiceOrderRecords).Cache, row);
      if (row != null)
      {
        ((PXSelectBase) this.ServiceOrderRecords).Cache.SetValueExt<FSServiceOrder.allowInvoice>((object) row, (object) true);
        ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Update(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current);
        ((PXAction) this.Save).Press();
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewPayment()
  {
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current != null && ((PXSelectBase<ARPayment>) this.Adjustments).Current != null)
    {
      ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) ((PXSelectBase<ARPayment>) this.Adjustments).Current.RefNbr, new object[1]
      {
        (object) ((PXSelectBase<ARPayment>) this.Adjustments).Current.DocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Payment");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual void CreatePrepayment()
  {
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current != null)
    {
      ((PXAction) this.Save).Press();
      PXGraph target;
      this.CreatePrepaymentDocument(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current, (FSAppointment) null, out target, "PPM");
      throw new PXPopupRedirectException(target, "New Payment", true);
    }
  }

  [PXButton]
  [PXUIField]
  protected virtual void openScheduleScreen()
  {
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current != null)
    {
      ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
      ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSContractSchedule>.op_Implicit(((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.ScheduleID, new object[2]
      {
        (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.ServiceContractID,
        (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.CustomerID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable AddReceipt(PXAdapter adapter)
  {
    FSServiceOrder current1 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    FSSrvOrdType current2 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current1 != null)
    {
      ((PXAction) this.Save).Press();
      ExpenseClaimDetailEntry claimDetailGraph = this.GetExpenseClaimDetailGraph(true);
      EPExpenseClaimDetails expenseClaimDetails1 = ((PXSelectBase<EPExpenseClaimDetails>) claimDetailGraph.ClaimDetails).Insert((EPExpenseClaimDetails) ((PXSelectBase) claimDetailGraph.ClaimDetails).Cache.CreateInstance());
      expenseClaimDetails1.ExpenseDate = current1.OrderDate;
      expenseClaimDetails1.BranchID = current1.BranchID;
      expenseClaimDetails1.CustomerID = current1.BillCustomerID;
      expenseClaimDetails1.CustomerLocationID = current1.BillLocationID;
      expenseClaimDetails1.ContractID = current1.ProjectID;
      expenseClaimDetails1.TaskID = current1.DfltProjectTaskID;
      if (current2 != null && !ProjectDefaultAttribute.IsNonProject(current1.ProjectID) && PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
        expenseClaimDetails1.CostCodeID = current2.DfltCostCodeID;
      EPExpenseClaimDetails expenseClaimDetails2 = ((PXSelectBase<EPExpenseClaimDetails>) claimDetailGraph.ClaimDetails).Update(expenseClaimDetails1);
      ((PXSelectBase) claimDetailGraph.ClaimDetails).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) expenseClaimDetails2);
      ((PXSelectBase) claimDetailGraph.ClaimDetails).Cache.SetValueExt<FSxEPExpenseClaimDetails.fsEntityTypeUI>((object) expenseClaimDetails2, (object) "PX.Objects.FS.FSServiceOrder");
      ((PXSelectBase) claimDetailGraph.ClaimDetails).Cache.SetValueExt<FSxEPExpenseClaimDetails.fsEntityNoteID>((object) expenseClaimDetails2, (object) current1.NoteID);
      ((PXSelectBase<EPExpenseClaimDetails>) claimDetailGraph.ClaimDetails).Update(expenseClaimDetails2);
      PXRedirectHelper.TryRedirect((PXGraph) claimDetailGraph, (PXRedirectHelper.WindowMode) 4);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddNewContact(PXAdapter adapter)
  {
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current != null && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.CustomerID.HasValue)
    {
      ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
      ((PXGraph) instance).Clear();
      PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Insert();
      contact.BAccountID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.CustomerID;
      PXResultset<CRContactClass>.op_Implicit(PXSelectBase<CRContactClass, PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Current<PX.Objects.CR.Contact.classID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) contact
      }, Array.Empty<object>()));
      ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Update(contact);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Contact");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable BillReversal(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceOrderEntry.\u003C\u003Ec__DisplayClass165_0 displayClass1650 = new ServiceOrderEntry.\u003C\u003Ec__DisplayClass165_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1650.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1650.list = adapter.Get<FSServiceOrder>().ToList<FSServiceOrder>();
    if (!adapter.MassProcess)
    {
      // ISSUE: reference to a compiler-generated field
      if (displayClass1650.list.Count > 0)
      {
        try
        {
          this.RecalculateExternalTaxesSync = true;
          ((PXAction) this.Save).Press();
        }
        finally
        {
          this.RecalculateExternalTaxesSync = false;
        }
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1650, __methodptr(\u003CBillReversal\u003Eb__0)));
      }
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1650.list;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable AddBill(PXAdapter adapter)
  {
    FSServiceOrder current1 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    FSSrvOrdType current2 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current1 != null)
    {
      ((PXAction) this.Save).Press();
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      PX.Objects.AP.APInvoice apInvoice = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Insert((PX.Objects.AP.APInvoice) ((PXSelectBase) instance.Document).Cache.CreateInstance());
      SM_APInvoiceEntry extension = ((PXGraph) instance).GetExtension<SM_APInvoiceEntry>();
      apInvoice.BranchID = current1.BranchID;
      apInvoice.DocDate = current1.OrderDate;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedEntityType = "PX.Objects.FS.FSServiceOrder";
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocNoteID = current1.NoteID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocDate = current1.OrderDate;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocCustomerID = current1.CustomerID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocCustomerLocationID = current1.LocationID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocProjectID = current1.ProjectID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocProjectTaskID = current1.DfltProjectTaskID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocCostCodeID = (int?) current2?.DfltCostCodeID;
      ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Update(apInvoice);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    return adapter.Get();
  }

  public virtual void DeallocateUnusedItems(FSServiceOrder serviceOrder)
  {
    if (serviceOrder.BillServiceContractID.HasValue || this.GetBillingMode(serviceOrder) == "SO")
      return;
    PXSelect<FSAppointmentDet> pxSelect1 = new PXSelect<FSAppointmentDet>((PXGraph) this);
    if (!((PXGraph) this).Views.Caches.Contains(typeof (FSAppointmentDet)))
      ((PXGraph) this).Views.Caches.Add(typeof (FSAppointmentDet));
    PXSelect<FSApptLineSplit> pxSelect2 = new PXSelect<FSApptLineSplit>((PXGraph) this);
    if (!((PXGraph) this).Views.Caches.Contains(typeof (FSApptLineSplit)))
      ((PXGraph) this).Views.Caches.Add(typeof (FSApptLineSplit));
    List<FSSODetSplit> fssoDetSplitList = new List<FSSODetSplit>();
    foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.completed, Equal<False>, And<FSSODetSplit.pOCreate, Equal<False>, And<FSSODetSplit.inventoryID, IsNotNull>>>>>, OrderBy<Asc<FSSODetSplit.lineNbr, Asc<FSSODetSplit.splitLineNbr>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) serviceOrder.SrvOrdType,
      (object) serviceOrder.RefNbr
    }))
    {
      FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
      fssoDetSplitList.Add((FSSODetSplit) ((PXSelectBase) this.Splits).Cache.CreateCopy((object) fssoDetSplit));
    }
    int? nullable1 = new int?();
    List<FSAppointmentDet> source1 = new List<FSAppointmentDet>();
    List<FSApptLineSplit> source2 = new List<FSApptLineSplit>();
    bool flag = false;
    List<FSSODetSplit> splitsToDeallocate = new List<FSSODetSplit>();
    Decimal? nullable2 = new Decimal?(0M);
    foreach (FSSODetSplit fssoDetSplit in fssoDetSplitList)
    {
      FSSODetSplit soSplit = fssoDetSplit;
      int? nullable3;
      if (nullable1.HasValue)
      {
        int? nullable4 = nullable1;
        nullable3 = soSplit.LineNbr;
        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
          goto label_40;
      }
      nullable2 = new Decimal?(0M);
      flag = SharedFunctions.IsLotSerialRequired(((PXSelectBase) this.ServiceOrderDetails).Cache, soSplit.InventoryID);
      nullable1 = soSplit.LineNbr;
      FSSODet fssoDet = (FSSODet) PXParentAttribute.SelectParent(((PXSelectBase) this.Splits).Cache, (object) soSplit, typeof (FSSODet));
      if (fssoDet == null)
        throw new PXException("The {0} record was not found.", new object[1]
        {
          (object) DACHelper.GetDisplayName(typeof (FSSODet))
        });
      source1.Clear();
      source2.Clear();
      int? soDetId = fssoDet.SODetID;
      nullable3 = new int?();
      int? apptDetID = nullable3;
      foreach (FSAppointmentDet fsAppointmentDet in this.GetRelatedApptLines((PXGraph) this, soDetId, false, apptDetID, false, false).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => !x.PostID.HasValue)))
        source1.Add((FSAppointmentDet) ((PXSelectBase) pxSelect1).Cache.CreateCopy((object) fsAppointmentDet));
      if (flag)
      {
        foreach (FSAppointmentDet fsAppointmentDet in source1)
        {
          foreach (FSApptLineSplit selectChild in PXParentAttribute.SelectChildren(((PXSelectBase) pxSelect2).Cache, (object) fsAppointmentDet, typeof (FSAppointmentDet)))
            source2.Add((FSApptLineSplit) ((PXSelectBase) pxSelect2).Cache.CreateCopy((object) selectChild));
        }
      }
      else
      {
        foreach (FSAppointmentDet fsAppointmentDet in source1.Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x =>
        {
          Decimal? baseEffTranQty = x.BaseEffTranQty;
          Decimal num = 0M;
          return baseEffTranQty.GetValueOrDefault() > num & baseEffTranQty.HasValue;
        })))
        {
          Decimal? nullable5 = nullable2;
          Decimal? baseEffTranQty = fsAppointmentDet.BaseEffTranQty;
          nullable2 = nullable5.HasValue & baseEffTranQty.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + baseEffTranQty.GetValueOrDefault()) : new Decimal?();
          fsAppointmentDet.BaseEffTranQty = new Decimal?(0M);
        }
      }
label_40:
      if (flag)
      {
        Decimal? nullable6 = new Decimal?(0M);
        foreach (FSApptLineSplit fsApptLineSplit in source2.Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (x => !string.IsNullOrEmpty(x.LotSerialNbr) && string.Equals(x.LotSerialNbr, soSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase))))
        {
          Decimal? nullable7 = nullable6;
          Decimal? baseQty = fsApptLineSplit.BaseQty;
          nullable6 = nullable7.HasValue & baseQty.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
          fsApptLineSplit.BaseQty = new Decimal?(0M);
        }
        Decimal? nullable8 = nullable6;
        Decimal? baseQty1 = soSplit.BaseQty;
        if (nullable8.GetValueOrDefault() <= baseQty1.GetValueOrDefault() & nullable8.HasValue & baseQty1.HasValue)
          soSplit.BaseQty = nullable6;
        splitsToDeallocate.Add(soSplit);
      }
      else
      {
        Decimal? nullable9 = nullable2;
        Decimal num = 0M;
        if (nullable9.GetValueOrDefault() <= num & nullable9.HasValue)
        {
          soSplit.BaseQty = new Decimal?(0M);
        }
        else
        {
          Decimal? baseQty = soSplit.BaseQty;
          Decimal? nullable10 = nullable2;
          if (baseQty.GetValueOrDefault() <= nullable10.GetValueOrDefault() & baseQty.HasValue & nullable10.HasValue)
          {
            nullable10 = nullable2;
            baseQty = soSplit.BaseQty;
            nullable2 = nullable10.HasValue & baseQty.HasValue ? new Decimal?(nullable10.GetValueOrDefault() - baseQty.GetValueOrDefault()) : new Decimal?();
          }
          else
            soSplit.BaseQty = nullable2;
        }
        splitsToDeallocate.Add(soSplit);
      }
    }
    FSAllocationProcess.DeallocateServiceOrderSplits(this, splitsToDeallocate, true);
  }

  public virtual int? GetPreferedSiteID()
  {
    int? preferedSiteId = new int?();
    FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelectJoin<FSSODet, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<FSSODet.siteID>>>, Where<FSSODet.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSSODet.refNbr, Equal<Current<FSServiceOrder.refNbr>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (fssoDet != null)
      preferedSiteId = fssoDet.SiteID;
    return preferedSiteId;
  }

  public virtual DateTime? GetShipDate(FSServiceOrder serviceOrder)
  {
    return AppointmentEntry.GetShipDateInt((PXGraph) this, serviceOrder, (FSAppointment) null);
  }

  private void EnableDisable_SODetLine(
    PXGraph graph,
    PXCache cache,
    FSSODet fsSODetRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSServiceOrder fsServiceOrderRow,
    FSServiceContract fsServiceContractRow)
  {
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = true;
    bool flag4 = false;
    bool flag5 = false;
    bool flag6 = true;
    bool flag7 = true;
    bool flag8 = false;
    bool flag9 = true;
    bool flag10 = true;
    bool? nullable1;
    if (fsSODetRow.IsPrepaid.GetValueOrDefault())
    {
      flag1 = false;
      flag2 = false;
      flag3 = false;
      flag4 = false;
      flag5 = false;
      flag7 = false;
      flag8 = false;
      flag9 = false;
      flag10 = false;
    }
    else
    {
      switch (fsSODetRow.LineType)
      {
        case "SERVI":
        case "NSTKI":
          int num1;
          if (fsServiceOrderRow == null)
          {
            num1 = 1;
          }
          else
          {
            nullable1 = fsServiceOrderRow.AllowInvoice;
            num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
          }
          flag2 = num1 != 0;
          flag3 = true;
          flag4 = true;
          flag5 = true;
          flag6 = true;
          flag7 = true;
          break;
        case "SLPRO":
          flag2 = true;
          flag3 = true;
          flag5 = true;
          flag6 = false;
          flag7 = true;
          break;
        case "CM_LN":
        case "IT_LN":
          flag2 = false;
          flag3 = false;
          flag5 = false;
          flag6 = false;
          flag7 = false;
          break;
      }
    }
    int? nullable2 = fsSODetRow.SODetID;
    int num2 = 0;
    if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
      flag1 = false;
    bool flag11 = flag4 && fsSODetRow.LineType == "SERVI" && fsSODetRow.Mem_LastReferencedBy == null;
    bool flag12 = flag2 && fsSODetRow.Mem_LastReferencedBy == null;
    bool flag13 = PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() || PXAccess.FeatureInstalled<FeaturesSet.routeManagementModule>();
    int num3;
    if (fsServiceOrderRow != null)
    {
      nullable2 = fsServiceOrderRow.BillServiceContractID;
      if (nullable2.HasValue)
      {
        num3 = fsServiceContractRow.BillingType == "STDB" ? 1 : 0;
        goto label_16;
      }
    }
    num3 = 0;
label_16:
    int num4 = flag13 ? 1 : 0;
    bool flag14 = (num3 & num4) != 0;
    PXUIFieldAttribute.SetEnabled<FSSODet.lineType>(cache, (object) fsSODetRow, flag1);
    PXUIFieldAttribute.SetEnabled<FSSODet.inventoryID>(cache, (object) fsSODetRow, flag12);
    PXDefaultAttribute.SetPersistingCheck<FSSODet.inventoryID>(cache, (object) fsSODetRow, flag3 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSSODet.billingRule>(cache, (object) fsSODetRow, flag11);
    PXUIFieldAttribute.SetEnabled<FSSODet.isFree>(cache, (object) fsSODetRow, flag5);
    PXUIFieldAttribute.SetEnabled<FSSODet.estimatedDuration>(cache, (object) fsSODetRow, flag6);
    PXUIFieldAttribute.SetEnabled<FSSODet.projectTaskID>(cache, (object) fsSODetRow, flag7);
    PXUIFieldAttribute.SetEnabled<FSSODet.subID>(cache, (object) fsSODetRow, flag8);
    PXUIFieldAttribute.SetEnabled<FSSODet.tranDesc>(cache, (object) fsSODetRow, flag9);
    PXDefaultAttribute.SetPersistingCheck<FSSODet.tranDesc>(cache, (object) fsSODetRow, flag10 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetVisible<FSSODet.contractRelated>(cache, (object) null, flag14);
    PXUIFieldAttribute.SetVisible<FSSODet.coveredQty>(cache, (object) null, flag14);
    PXUIFieldAttribute.SetVisible<FSSODet.extraUsageQty>(cache, (object) null, flag14);
    PXUIFieldAttribute.SetVisible<FSSODet.curyExtraUsageUnitPrice>(cache, (object) null, flag14);
    PXUIFieldAttribute.SetVisibility<FSSODet.contractRelated>(cache, (object) null, flag13 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<FSSODet.coveredQty>(cache, (object) null, flag13 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<FSSODet.extraUsageQty>(cache, (object) null, flag13 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<FSSODet.curyExtraUsageUnitPrice>(cache, (object) null, flag13 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    nullable1 = fsSODetRow.IsPrepaid;
    if (nullable1.GetValueOrDefault())
      return;
    this.EnableDisable_Acct_Sub(cache, (IFSSODetBase) fsSODetRow, fsSrvOrdTypeRow, fsServiceOrderRow);
  }

  /// <summary>
  /// Check the ManageRooms value on Setup to check/hide the Rooms Values options.
  /// </summary>
  public virtual void HideRooms(FSServiceOrder fsServiceOrderRow)
  {
    bool flag = ServiceManagementSetup.IsRoomManagementActive((PXGraph) this, ((PXSelectBase<FSSetup>) this.SetupRecord).Current);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.roomID>(((PXSelectBase) this.CurrentServiceOrder).Cache, (object) fsServiceOrderRow, flag);
    ((PXAction) this.OpenRoomBoard).SetVisible(flag);
  }

  public virtual bool isTheLineValid(PXCache cache, FSSODet fsSODetRow, PXErrorLevel errorLevel = 4)
  {
    bool flag = true;
    if (fsSODetRow == null)
      return flag;
    int? nullable;
    if (fsSODetRow.LineType == "CM_LN" || fsSODetRow.LineType == "IT_LN")
    {
      nullable = fsSODetRow.InventoryID;
      if (nullable.HasValue)
      {
        PXUIFieldAttribute.SetEnabled<FSSODet.inventoryID>(cache, (object) fsSODetRow, true);
        cache.RaiseExceptionHandling<FSSODet.inventoryID>((object) fsSODetRow, (object) null, (Exception) new PXSetPropertyException("The column must be empty for the selected line type.", errorLevel));
        flag = false;
      }
    }
    if (fsSODetRow.LineType == "SERVI" || fsSODetRow.LineType == "NSTKI" || fsSODetRow.LineType == "SLPRO")
    {
      nullable = fsSODetRow.InventoryID;
      if (!nullable.HasValue)
      {
        PXUIFieldAttribute.SetEnabled<FSSODet.inventoryID>(cache, (object) fsSODetRow, true);
        cache.RaiseExceptionHandling<FSSODet.inventoryID>((object) fsSODetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        flag = false;
      }
    }
    if ((fsSODetRow.LineType == "CM_LN" || fsSODetRow.LineType == "IT_LN") && string.IsNullOrEmpty(fsSODetRow.TranDesc))
    {
      cache.RaiseExceptionHandling<FSSODet.tranDesc>((object) fsSODetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
      flag = false;
    }
    if (fsSODetRow.LineType != "SERVI" && fsSODetRow.LineType != "NSTKI" && !fsSODetRow.EstimatedQty.HasValue)
    {
      PXUIFieldAttribute.SetEnabled<FSSODet.estimatedQty>(cache, (object) fsSODetRow, true);
      cache.RaiseExceptionHandling<FSSODet.estimatedQty>((object) fsSODetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
      flag = false;
    }
    if (fsSODetRow.LineType == "SLPRO" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "SO" && fsSODetRow.LastModifiedByScreenID != "FS500200")
    {
      nullable = fsSODetRow.SiteID;
      if (!nullable.HasValue)
      {
        cache.RaiseExceptionHandling<FSSODet.siteID>((object) fsSODetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", errorLevel));
        flag = false;
      }
    }
    return flag;
  }

  public virtual void ClearOpportunity(FSServiceOrder fsServiceOrderRow)
  {
    OpportunityMaint instance = PXGraph.CreateInstance<OpportunityMaint>();
    ((PXSelectBase<CROpportunity>) instance.Opportunity).Current = PXResultset<CROpportunity>.op_Implicit(((PXSelectBase<CROpportunity>) instance.Opportunity).Search<CROpportunity.opportunityID>((object) fsServiceOrderRow.SourceRefNbr, Array.Empty<object>()));
    CROpportunity current = ((PXSelectBase<CROpportunity>) instance.Opportunity).Current;
    if (current == null)
      return;
    this.ClearCRActivities(((PXSelectBase<CRPMTimeActivity>) ((PXGraph) instance).GetExtension<OpportunityMaint_ActivityDetailsExt>().Activities).Select(Array.Empty<object>()));
    FSxCROpportunity extension = ((PXSelectBase) instance.Opportunity).Cache.GetExtension<FSxCROpportunity>((object) current);
    ((PXSelectBase) instance.Opportunity).Cache.SetValueExt<FSxCROpportunity.sDEnabled>((object) current, (object) false);
    extension.ServiceOrderRefNbr = (string) null;
    extension.SrvOrdType = (string) null;
    ((PXSelectBase<CROpportunity>) instance.Opportunity).Update(current);
    ((PXAction) instance.Save).Press();
  }

  public virtual void ClearCase(FSServiceOrder fsServiceOrderRow)
  {
    CRCaseMaint instance = PXGraph.CreateInstance<CRCaseMaint>();
    ((PXSelectBase<CRCase>) instance.Case).Current = PXResultset<CRCase>.op_Implicit(((PXSelectBase<CRCase>) instance.Case).Search<CRCase.caseCD>((object) fsServiceOrderRow.SourceRefNbr, Array.Empty<object>()));
    CRCase current = ((PXSelectBase<CRCase>) instance.Case).Current;
    if (current == null)
      return;
    this.ClearCRActivities(((PXSelectBase<CRPMTimeActivity>) ((PXGraph) instance).GetExtension<CRCaseMaint_ActivityDetailsExt>().Activities).Select(Array.Empty<object>()));
    FSxCRCase extension = ((PXSelectBase) instance.Case).Cache.GetExtension<FSxCRCase>((object) current);
    ((PXSelectBase) instance.Case).Cache.SetValueExt<FSxCRCase.sDEnabled>((object) current, (object) false);
    extension.ServiceOrderRefNbr = (string) null;
    extension.SrvOrdType = (string) null;
    ((PXSelectBase<CRCase>) instance.Case).Update(current);
    ((PXAction) instance.Save).Press();
  }

  public virtual void ClearCRActivities(PXResultset<CRPMTimeActivity> activities)
  {
    CRTaskMaint instance = PXGraph.CreateInstance<CRTaskMaint>();
    foreach (PXResult<CRPMTimeActivity> activity in activities)
    {
      CRActivity crActivity = (CRActivity) PXResult<CRPMTimeActivity>.op_Implicit(activity);
      PMTimeActivity pmTimeActivity = PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.refNoteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) crActivity.NoteID
      }));
      if (pmTimeActivity != null)
      {
        FSxPMTimeActivity extension = PXCache<PMTimeActivity>.GetExtension<FSxPMTimeActivity>(pmTimeActivity);
        if (extension != null)
        {
          extension.ServiceID = new int?();
          ((PXSelectBase) instance.TimeActivity).Cache.Update((object) pmTimeActivity);
        }
      }
    }
    if (!((PXGraph) instance).IsDirty)
      return;
    ((PXAction) instance.Save).Press();
  }

  public virtual void ClearSalesOrder(FSServiceOrder fsServiceOrderRow)
  {
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) fsServiceOrderRow.SourceRefNbr, new object[1]
    {
      (object) fsServiceOrderRow.SourceDocType
    }));
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current == null)
      return;
    PX.Objects.SO.SOOrder copy1 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current);
    FSxSOOrder extension1 = ((PXSelectBase) instance.CurrentDocument).Cache.GetExtension<FSxSOOrder>((object) copy1);
    extension1.SDEnabled = new bool?(false);
    extension1.ServiceOrderRefNbr = (string) null;
    extension1.SrvOrdType = (string) null;
    ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.CurrentDocument).Update(copy1);
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in ((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.SO.SOLine copy2 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult));
      FSxSOLine extension2 = ((PXSelectBase) instance.Transactions).Cache.GetExtension<FSxSOLine>((object) copy2);
      bool? sdSelected = extension2.SDSelected;
      bool flag = false;
      if (!(sdSelected.GetValueOrDefault() == flag & sdSelected.HasValue))
      {
        extension2.SDSelected = new bool?(false);
        ((PXSelectBase<PX.Objects.SO.SOLine>) instance.Transactions).Update(copy2);
      }
    }
    ((PXAction) instance.Save).Press();
  }

  public virtual void ClearPrepayment(FSServiceOrder fsServiceOrderRow)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    SM_ARPaymentEntry extension = ((PXGraph) instance).GetExtension<SM_ARPaymentEntry>();
    foreach (PXResult<FSAdjust> pxResult in PXSelectBase<FSAdjust, PXSelect<FSAdjust, Where<FSAdjust.adjdOrderType, Equal<Required<FSAdjust.adjdOrderType>>, And<FSAdjust.adjdOrderNbr, Equal<Required<FSAdjust.adjdOrderNbr>>>>>.Config>.Select((PXGraph) instance, new object[2]
    {
      (object) fsServiceOrderRow.SrvOrdType,
      (object) fsServiceOrderRow.RefNbr
    }))
    {
      FSAdjust fsAdjust = PXResult<FSAdjust>.op_Implicit(pxResult);
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) fsAdjust.AdjgRefNbr, new object[1]
      {
        (object) fsAdjust.AdjgDocType
      }));
      if (((PXSelectBase<ARPayment>) instance.Document).Current != null)
      {
        ((PXSelectBase<FSAdjust>) extension.FSAdjustments).Delete(fsAdjust);
        ((PXAction) instance.Save).Press();
      }
    }
  }

  public virtual void ClearFSDocExpenseReceipts(Guid? noteID)
  {
    if (!noteID.HasValue)
      return;
    ExpenseClaimDetailEntry claimDetailGraph = this.GetExpenseClaimDetailGraph(true);
    PXResultset<EPExpenseClaimDetails> list = PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<FSxEPExpenseClaimDetails.fsEntityNoteID, Equal<Required<FSxEPExpenseClaimDetails.fsEntityNoteID>>>>.Config>.Select((PXGraph) claimDetailGraph, new object[1]
    {
      (object) noteID
    });
    this.ClearFSDocExpenseReceipts(claimDetailGraph, list);
  }

  public virtual void ClearFSDocExpenseReceipts(string DocRefNbr)
  {
    if (string.IsNullOrEmpty(DocRefNbr))
      return;
    ExpenseClaimDetailEntry claimDetailGraph = this.GetExpenseClaimDetailGraph(true);
    PXResultset<EPExpenseClaimDetails> list = PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) claimDetailGraph, new object[1]
    {
      (object) DocRefNbr
    });
    this.ClearFSDocExpenseReceipts(claimDetailGraph, list);
  }

  public virtual void ClearFSDocExpenseReceipts(
    ExpenseClaimDetailEntry graph,
    PXResultset<EPExpenseClaimDetails> list)
  {
    if (graph == null || list == null || list.Count == 0)
      return;
    foreach (PXResult<EPExpenseClaimDetails> pxResult in list)
    {
      EPExpenseClaimDetails expenseClaimDetails = PXResult<EPExpenseClaimDetails>.op_Implicit(pxResult);
      FSxEPExpenseClaimDetails extension = PXCache<EPExpenseClaimDetails>.GetExtension<FSxEPExpenseClaimDetails>(expenseClaimDetails);
      if (extension != null)
      {
        extension.FSEntityTypeUI = (string) null;
        extension.FSEntityNoteID = new Guid?();
        extension.FSBillable = new bool?(false);
        ((PXSelectBase) graph.ClaimDetails).Cache.Update((object) expenseClaimDetails);
      }
    }
    if (!((PXGraph) graph).IsDirty)
      return;
    ((PXAction) graph.Save).Press();
  }

  public virtual void ClearFSDocReferenceInAPDoc(string docType, string docRefnbr, int? lineNbr)
  {
    PXUpdate<Set<FSxAPTran.relatedEntityType, Null, Set<FSxAPTran.relatedDocNoteID, Null>>, PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<PX.Objects.AP.APTran.lineNbr, Equal<Required<PX.Objects.AP.APTran.lineNbr>>>>>>.Update((PXGraph) this, new object[3]
    {
      (object) docType,
      (object) docRefnbr,
      (object) lineNbr
    });
  }

  public virtual void SetSODetServiceLastReferencedBy(FSSODet fsSODetRow)
  {
    if (fsSODetRow == null)
      return;
    if (this.LastRefAppointmentDetails == null)
      this.LastRefAppointmentDetails = ((IEnumerable<PXResult<FSAppointmentDet>>) PXSelectBase<FSAppointmentDet, PXSelectJoinGroupBy<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>>, Where<FSAppointment.sOID, Equal<Required<FSServiceOrder.sOID>>, And<FSAppointment.canceled, Equal<False>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>>>>, Aggregate<GroupBy<FSAppointmentDet.sODetID>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) fsSODetRow.SOID
      })).ToList<PXResult<FSAppointmentDet>>();
    if (this.LastRefAppointmentDetails == null)
      return;
    FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(this.LastRefAppointmentDetails.Where<PXResult<FSAppointmentDet>>((Func<PXResult<FSAppointmentDet>, bool>) (x =>
    {
      int? soDetId1 = PXResult<FSAppointmentDet>.op_Implicit(x).SODetID;
      int? soDetId2 = fsSODetRow.SODetID;
      return soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue;
    })).FirstOrDefault<PXResult<FSAppointmentDet>>());
    if (fsAppointmentDet != null)
      fsSODetRow.Mem_LastReferencedBy = fsAppointmentDet.RefNbr;
    else
      fsSODetRow.Mem_LastReferencedBy = (string) null;
  }

  /// <summary>
  /// Update the assigned Employee for the Service Order in Sales Order customization if conditions apply.
  /// </summary>
  /// <param name="fsServiceOrderRow">FSServiceOrder row.</param>
  public virtual void UpdateAssignedEmpIDinSalesOrder(FSServiceOrder fsServiceOrderRow)
  {
    if (!this.updateSOCstmAssigneeEmpID || !(fsServiceOrderRow.SourceType == "SO"))
      return;
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsServiceOrderRow.SourceDocType,
      (object) fsServiceOrderRow.SourceRefNbr
    }));
    if (soOrder == null)
      return;
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) soOrder.OrderNbr, new object[1]
    {
      (object) soOrder.OrderType
    }));
    ((PXSelectBase) instance.Document).Cache.GetExtension<FSxSOOrder>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current).AssignedEmpID = fsServiceOrderRow.AssignedEmpID;
    ((PXSelectBase) instance.Document).Cache.SetStatus((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current, (PXEntryStatus) 1);
    ((PXAction) instance.Save).Press();
    this.updateSOCstmAssigneeEmpID = false;
  }

  /// <summary>
  /// Check if the given Service Order detail line is related with any Sales Order details.
  /// </summary>
  /// <param name="fsServiceOrderRow">Service Order row.</param>
  /// <param name="fsSODetRow">Service Order detail line.</param>
  /// <returns>Returns true if the Service Order detail is related with at least one Sales Order detail.</returns>
  public virtual bool IsThisLineRelatedToAsoLine(
    FSServiceOrder fsServiceOrderRow,
    FSSODet fsSODetRow)
  {
    if (!fsSODetRow.IsPrepaid.GetValueOrDefault() || !fsSODetRow.SourceLineNbr.HasValue)
      return false;
    return PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Required<PX.Objects.SO.SOLine.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Required<PX.Objects.SO.SOLine.orderNbr>>, And<PX.Objects.SO.SOLine.lineNbr, Equal<Required<PX.Objects.SO.SOLine.lineNbr>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) fsServiceOrderRow.SourceDocType,
      (object) fsServiceOrderRow.SourceRefNbr,
      (object) fsSODetRow.SourceLineNbr
    })) != null;
  }

  /// <summary>
  /// Hides or Shows Appointments, Staff, Resources Equipment, Related Service Orders, Post info Tabs.
  /// </summary>
  /// <param name="fsServiceOrderRow">Service Order row.</param>
  public virtual void HideOrShowTabs(FSServiceOrder fsServiceOrderRow)
  {
    bool valueOrDefault = fsServiceOrderRow.Quote.GetValueOrDefault();
    ((PXSelectBase) this.ServiceOrderAppointments).AllowSelect = !valueOrDefault;
    ((PXSelectBase) this.ServiceOrderEmployees).AllowSelect = !valueOrDefault && ((bool?) ((PXSelectBase<FSSetup>) this.SetupRecord).Current?.EnableDfltStaffOnServiceOrder).GetValueOrDefault();
    ((PXSelectBase) this.ServiceOrderEquipment).AllowSelect = !valueOrDefault && ((bool?) ((PXSelectBase<FSSetup>) this.SetupRecord).Current?.EnableDfltResEquipOnServiceOrder).GetValueOrDefault();
    ((PXSelectBase) this.RelatedServiceOrders).AllowSelect = valueOrDefault;
    bool flag = this.GetBillingMode(fsServiceOrderRow) == "SO";
    PXUIFieldAttribute.SetVisible<FSServiceOrder.allowInvoice>(((PXSelectBase) this.ServiceOrderRecords).Cache, (object) fsServiceOrderRow, flag);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.mem_Invoiced>(((PXSelectBase) this.ServiceOrderRecords).Cache, (object) fsServiceOrderRow, flag);
    PXUIFieldAttribute.SetVisible<FSSODet.staffID>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current, ((bool?) ((PXSelectBase<FSSetup>) this.SetupRecord).Current?.EnableDfltStaffOnServiceOrder).GetValueOrDefault());
    ((PXAction) this.openStaffSelectorFromServiceTab).SetVisible(((bool?) ((PXSelectBase<FSSetup>) this.SetupRecord).Current?.EnableDfltStaffOnServiceOrder).GetValueOrDefault());
    ((PXSelectBase) this.ServiceOrderPostedIn).AllowSelect = flag;
    fsServiceOrderRow.ShowInvoicesTab = new bool?(flag);
  }

  public virtual Dictionary<string, string> GetServiceOrderParameters(
    FSServiceOrder fsServiceOrderRow,
    bool isServiceTimeActivityReport = false)
  {
    Dictionary<string, string> serviceOrderParameters = new Dictionary<string, string>();
    if (fsServiceOrderRow == null)
      return serviceOrderParameters;
    string fieldName1 = SharedFunctions.GetFieldName<FSServiceOrder.srvOrdType>();
    string fieldName2 = SharedFunctions.GetFieldName<FSServiceOrder.refNbr>();
    serviceOrderParameters[fieldName1] = fsServiceOrderRow.SrvOrdType;
    serviceOrderParameters[fieldName2] = fsServiceOrderRow.RefNbr;
    if (isServiceTimeActivityReport)
    {
      string fieldName3 = SharedFunctions.GetFieldName<FSAppointment.soRefNbr>();
      string key1 = "DateFrom";
      string key2 = "DateTo";
      serviceOrderParameters[fieldName3] = fsServiceOrderRow.RefNbr;
      serviceOrderParameters[fieldName2] = (string) null;
      serviceOrderParameters[key1] = (string) null;
      serviceOrderParameters[key2] = (string) null;
    }
    return serviceOrderParameters;
  }

  public virtual void ChangeItemLineStatus(IEnumerable<FSSODet> rows, string newStatus)
  {
    foreach (object row in rows)
    {
      FSSODet copy = (FSSODet) ((PXSelectBase) this.ServiceOrderDetails).Cache.CreateCopy(row);
      copy.Status = newStatus;
      ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Update(copy);
    }
  }

  /// <summary>
  /// Completes all appointments belonging to <c>fsServiceOrderRow</c>, in case an error occurs with any appointment,
  /// the service order will not be completed and a message will be displayed alerting the user about the appointment's issue.
  /// The row of the appointment having problems is marked with its error.
  /// </summary>
  public virtual void CompleteAppointmentsInServiceOrder(
    ServiceOrderEntry graph,
    FSServiceOrder fsServiceOrderRow)
  {
    if (((PXGraph) graph).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200"))
      return;
    PXResultset<FSAppointmentInRoute> bqlResultSet = PXSelectBase<FSAppointmentInRoute, PXSelect<FSAppointmentInRoute, Where<FSAppointmentInRoute.sOID, Equal<Required<FSAppointmentInRoute.sOID>>, And<FSAppointmentInRoute.closed, Equal<False>, And<FSAppointmentInRoute.canceled, Equal<False>, And<FSAppointmentInRoute.completed, Equal<False>>>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) fsServiceOrderRow.SOID
    });
    if (bqlResultSet.Count <= 0)
      return;
    this.AppointmentsWithErrors = ServiceOrderEntry.CompleteAppointments(graph, bqlResultSet);
    if (this.AppointmentsWithErrors != null && this.AppointmentsWithErrors.Count > 0)
      throw new PXException("The service order cannot be completed because some appointments have issues. See details below.");
  }

  public virtual void HardReloadServiceOrderAppointments()
  {
    ((PXSelectBase) this.ServiceOrderAppointments).Cache.Clear();
    ((PXSelectBase) this.ServiceOrderAppointments).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.ServiceOrderAppointments).View.Clear();
    ((PXSelectBase<FSAppointment>) this.ServiceOrderAppointments).Select(Array.Empty<object>());
  }

  public virtual void ShowErrorsOnAppointmentLines()
  {
    if (this.AppointmentsWithErrors == null || this.AppointmentsWithErrors.Count <= 0)
      return;
    List<FSAppointment> list = GraphHelper.RowCast<FSAppointment>((IEnumerable) ((PXSelectBase<FSAppointment>) this.ServiceOrderAppointments).Select(Array.Empty<object>())).ToList<FSAppointment>();
    foreach (KeyValuePair<FSAppointment, string> appointmentsWithError in this.AppointmentsWithErrors)
    {
      KeyValuePair<FSAppointment, string> kvp = appointmentsWithError;
      FSAppointment fsAppointment = list.Where<FSAppointment>((Func<FSAppointment, bool>) (r => r.RefNbr == kvp.Key.RefNbr)).FirstOrDefault<FSAppointment>();
      if (fsAppointment != null)
        ((PXSelectBase) this.ServiceOrderAppointments).Cache.RaiseExceptionHandling<FSAppointment.refNbr>((object) fsAppointment, (object) fsAppointment.RefNbr, (Exception) new PXSetPropertyException(kvp.Value, (PXErrorLevel) 5));
    }
  }

  /// <summary>
  /// Closes all appointments belonging to <c>fsServiceOrderRow</c>, in case an error occurs with any appointment,
  /// the service order will not be closed and a message will be displayed alerting the user about the appointment's issue.
  /// The row of the appointment having problems is marked with its error.
  /// </summary>
  public virtual void CloseAppointmentsInServiceOrder(
    ServiceOrderEntry graph,
    FSServiceOrder fsServiceOrderRow)
  {
    if (((PXGraph) graph).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200"))
      return;
    PXResultset<FSAppointment> bqlResultSet = PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSServiceOrder.sOID>>, And<FSAppointment.completed, Equal<True>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) fsServiceOrderRow.SOID
    });
    if (bqlResultSet.Count <= 0)
      return;
    Dictionary<FSAppointment, string> dictionary = this.CloseAppointments(bqlResultSet);
    if (dictionary.Count > 0)
    {
      foreach (KeyValuePair<FSAppointment, string> keyValuePair in dictionary)
        ((PXSelectBase) graph.ServiceOrderAppointments).Cache.RaiseExceptionHandling<FSAppointment.refNbr>((object) keyValuePair.Key, (object) keyValuePair.Key.RefNbr, (Exception) new PXSetPropertyException(keyValuePair.Value, (PXErrorLevel) 5));
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The service order cannot be closed because some appointments have issues. See details below.", Array.Empty<object>()));
    }
  }

  /// <summary>
  /// Cancels all appointments belonging to <c>fsServiceOrderRow</c>, in case an error occurs with any appointment,
  /// the service order will not be canceled and a message will be displayed alerting the user about the appointment's issue.
  /// The row of the appointment having problems is marked with its error.
  /// </summary>
  public virtual void CancelAppointmentsInServiceOrder(
    ServiceOrderEntry graph,
    FSServiceOrder fsServiceOrderRow)
  {
    if (((PXGraph) graph).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200"))
      return;
    PXResultset<FSAppointment> bqlResultSet = PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSServiceOrder.sOID>>, And<FSAppointment.canceled, Equal<False>, And<FSAppointment.closed, Equal<False>, And<FSAppointment.completed, Equal<False>>>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) fsServiceOrderRow.SOID
    });
    if (bqlResultSet.Count <= 0)
      return;
    Dictionary<FSAppointment, string> dictionary = this.CancelAppointments(graph, bqlResultSet);
    if (dictionary.Count > 0)
    {
      foreach (KeyValuePair<FSAppointment, string> keyValuePair in dictionary)
        ((PXSelectBase) graph.ServiceOrderAppointments).Cache.RaiseExceptionHandling<FSAppointment.refNbr>((object) keyValuePair.Key, (object) keyValuePair.Key.RefNbr, (Exception) new PXSetPropertyException(keyValuePair.Value, (PXErrorLevel) 5));
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The service order cannot be canceled because some appointments have issues. See details below.", Array.Empty<object>()));
    }
  }

  public virtual bool IsManualPriceFlagNeeded(PXCache sender, IFSSODetBase row)
  {
    if (row != null && !row.ManualPrice.GetValueOrDefault() && (sender.Graph.IsImportFromExcel || sender.Graph.IsContractBasedAPI))
    {
      object valuePending1 = sender.GetValuePending<FSSODet.curyUnitPrice>((object) row);
      object valuePending2 = sender.GetValuePending<FSSODet.curyExtPrice>((object) row);
      object valuePending3 = sender.GetValuePending<FSSODet.manualPrice>((object) row);
      Decimal result;
      if ((valuePending1 != PXCache.NotSetValue && valuePending1 != null && Decimal.TryParse(valuePending1.ToString(), out result) || valuePending2 != PXCache.NotSetValue && valuePending2 != null && Decimal.TryParse(valuePending2.ToString(), out result)) && (valuePending3 == PXCache.NotSetValue || valuePending3 == null))
        return true;
    }
    return false;
  }

  public virtual void UpdatePOOptionsInAppointmentRelatedLines(FSSODet soDet)
  {
    if (!((PXGraph) this).Views.Caches.Contains(typeof (FSAppointment)))
      ((PXGraph) this).Views.Caches.Add(typeof (FSAppointment));
    if (!((PXGraph) this).Views.Caches.Contains(typeof (FSAppointmentDet)))
      ((PXGraph) this).Views.Caches.Add(typeof (FSAppointmentDet));
    int? nullable1 = new int?();
    List<FSAppointmentDet> relatedApptLines = this.GetRelatedApptLines((PXGraph) this, soDet.SODetID, false, new int?(), false, false);
    RelatedApptSummary relatedApptSummary = this.CalculateRelatedApptSummary((PXGraph) this, soDet.SODetID, soDet, new int?(), false);
    FSAppointment appointmentRow = (FSAppointment) null;
    foreach (FSAppointmentDet fsAppointmentDet in (IEnumerable<FSAppointmentDet>) relatedApptLines.Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => x.Status == "NS" || x.Status == "WP" || x.Status == "RP")).OrderBy<FSAppointmentDet, int?>((Func<FSAppointmentDet, int?>) (x => x.AppointmentID)))
    {
      if (nullable1.HasValue)
      {
        int? nullable2 = nullable1;
        int? appointmentId = fsAppointmentDet.AppointmentID;
        if (nullable2.GetValueOrDefault() == appointmentId.GetValueOrDefault() & nullable2.HasValue == appointmentId.HasValue)
          goto label_9;
      }
      nullable1 = fsAppointmentDet.AppointmentID;
      appointmentRow = (FSAppointment) PXParentAttribute.SelectParent(((PXSelectBase) this.apptDetView).Cache, (object) fsAppointmentDet, typeof (FSAppointment));
label_9:
      string valueOriginal = (string) ((PXSelectBase) this.apptDetView).Cache.GetValueOriginal<FSAppointmentDet.status>((object) fsAppointmentDet);
      FSAppointmentDet copy = (FSAppointmentDet) ((PXSelectBase) this.apptDetView).Cache.CreateCopy((object) fsAppointmentDet);
      this.AccumulateRelatedApptLine(relatedApptSummary, copy, -1);
      if (soDet.PONbr != copy.PONbr)
      {
        copy.POType = soDet.POType;
        copy.PONbr = soDet.PONbr;
        copy.POLineNbr = soDet.POLineNbr;
        copy.POStatus = soDet.POStatus;
        copy.POCompleted = soDet.POCompleted;
      }
      copy.EnablePO = soDet.EnablePO;
      copy.POSource = soDet.POSource;
      copy.POVendorID = soDet.POVendorID;
      copy.POVendorLocationID = soDet.POVendorLocationID;
      copy.Status = soDet.EnablePO.GetValueOrDefault() ? "WP" : "NS";
      AppointmentEntry.UpdateCanceledNotPerformed(((PXSelectBase) this.apptDetView).Cache, copy, appointmentRow, valueOriginal);
      FSAppointmentDet apptLine = ((PXSelectBase<FSAppointmentDet>) this.apptDetView).Update(copy);
      this.AccumulateRelatedApptLine(relatedApptSummary, apptLine, 1);
      soDet = this.UpdateRelatedApptSummaryFields(((PXSelectBase) this.ServiceOrderDetails).Cache, soDet, relatedApptSummary, ((PXSelectBase) this.apptDetView).Cache, fsAppointmentDet.AppDetID, valueOriginal, apptLine.Status);
      ((PXSelectBase) this.apptDetView).Cache.Persist((object) apptLine, (PXDBOperation) 1);
    }
  }

  public virtual RelatedApptSummary CalculateRelatedApptSummary(
    PXGraph graph,
    int? soDetID,
    FSSODet soDetRow,
    int? apptDetIDToIgnore,
    bool recalculateValues)
  {
    int? nullable;
    if (soDetRow != null)
    {
      int? soDetId = soDetRow.SODetID;
      nullable = soDetID;
      if (!(soDetId.GetValueOrDefault() == nullable.GetValueOrDefault() & soDetId.HasValue == nullable.HasValue))
        throw new PXArgumentException();
    }
    if (soDetID.HasValue)
    {
      nullable = soDetID;
      int num = 0;
      if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
      {
        RelatedApptSummary summ;
        if (soDetRow == null || recalculateValues)
        {
          summ = new RelatedApptSummary(soDetID);
          foreach (PXResult<FSAppointmentDet> pxResult in PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And2<Where<Required<FSAppointmentDet.appDetID>, IsNull, Or<FSAppointmentDet.appDetID, NotEqual<Required<FSAppointmentDet.appDetID>>>>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>>>>>>.Config>.Select(graph, new object[3]
          {
            (object) soDetID,
            (object) apptDetIDToIgnore,
            (object) apptDetIDToIgnore
          }))
          {
            FSAppointmentDet apptLine = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
            this.AccumulateRelatedApptLine(summ, apptLine, 1);
          }
        }
        else
          summ = this.InitializeRelatedApptSummary(soDetRow);
        return summ;
      }
    }
    return new RelatedApptSummary(soDetID);
  }

  public virtual RelatedApptSummary InitializeRelatedApptSummary(FSSODet soDetRow)
  {
    RelatedApptSummary relatedApptSummary = new RelatedApptSummary(soDetRow.SODetID)
    {
      ApptCntr = soDetRow.ApptCntr.Value
    };
    relatedApptSummary.ApptCntrIncludingRequestPO = relatedApptSummary.ApptCntr;
    relatedApptSummary.ApptEstimatedDuration = soDetRow.ApptEstimatedDuration.Value;
    relatedApptSummary.ApptActualDuration = soDetRow.ApptDuration.Value;
    relatedApptSummary.ApptEffTranQty = soDetRow.ApptQty.Value;
    relatedApptSummary.CuryApptEffTranAmt = soDetRow.CuryApptTranAmt.Value;
    relatedApptSummary.ApptEffTranAmt = soDetRow.ApptTranAmt.Value;
    return relatedApptSummary;
  }

  public virtual FSSODet UpdateRelatedApptSummaryFields(
    PXCache soDetCache,
    FSSODet soDetRow,
    RelatedApptSummary summ,
    PXCache apptDetCache,
    int? apptDetID,
    string oldApptLineStatus,
    string newApptLineStatus)
  {
    int? soDetId1 = soDetRow.SODetID;
    int? soDetId2 = summ.SODetID;
    if (!(soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue))
      return soDetRow;
    soDetRow = (FSSODet) soDetCache.CreateCopy((object) soDetRow);
    soDetRow.ApptCntr = new int?(summ.ApptCntr);
    soDetRow.ApptEstimatedDuration = new int?(summ.ApptEstimatedDuration);
    soDetRow.ApptDuration = new int?(summ.ApptActualDuration);
    soDetRow.ApptQty = new Decimal?(summ.ApptEffTranQty);
    soDetRow.CuryApptTranAmt = new Decimal?(summ.CuryApptEffTranAmt);
    soDetRow.ApptTranAmt = new Decimal?(summ.ApptEffTranAmt);
    soDetRow = this.UpdateSrvOrdLineStatusBasedOnApptLineStatusChange(soDetCache, soDetRow, apptDetCache, apptDetID, oldApptLineStatus, newApptLineStatus);
    return (FSSODet) soDetCache.Update((object) soDetRow);
  }

  public virtual void AccumulateRelatedApptLine(
    RelatedApptSummary summ,
    FSAppointmentDet apptLine,
    int invtMult)
  {
    if (apptLine.LineType != "SERVI" && apptLine.LineType != "NSTKI" && apptLine.LineType != "SLPRO" || apptLine.Status == "CC" || apptLine.Status == "NP")
      return;
    if (apptLine.Status != "RP")
    {
      summ.ApptCntr += invtMult;
      summ.ApptEstimatedDuration += apptLine.EstimatedDuration.Value * invtMult;
      summ.ApptEstimatedQty += apptLine.EstimatedQty.Value * (Decimal) invtMult;
      RelatedApptSummary relatedApptSummary1 = summ;
      int apptActualDuration = relatedApptSummary1.ApptActualDuration;
      int? nullable1 = apptLine.ActualDuration;
      int num1 = nullable1.Value * invtMult;
      relatedApptSummary1.ApptActualDuration = apptActualDuration + num1;
      RelatedApptSummary relatedApptSummary2 = summ;
      Decimal apptActualQty = relatedApptSummary2.ApptActualQty;
      Decimal? nullable2 = apptLine.ActualQty;
      Decimal num2 = nullable2.Value * (Decimal) invtMult;
      relatedApptSummary2.ApptActualQty = apptActualQty + num2;
      bool? nullable3;
      if (apptLine.Status == "NS")
      {
        RelatedApptSummary relatedApptSummary3 = summ;
        int apptEffTranDuration = relatedApptSummary3.ApptEffTranDuration;
        nullable1 = apptLine.EstimatedDuration;
        int num3 = nullable1.Value * invtMult;
        relatedApptSummary3.ApptEffTranDuration = apptEffTranDuration + num3;
        nullable3 = apptLine.IsPrepaid;
        if (nullable3.GetValueOrDefault())
        {
          RelatedApptSummary relatedApptSummary4 = summ;
          Decimal apptEffTranQty = relatedApptSummary4.ApptEffTranQty;
          nullable2 = apptLine.EstimatedQty;
          Decimal num4 = nullable2.Value * (Decimal) invtMult;
          relatedApptSummary4.ApptEffTranQty = apptEffTranQty + num4;
        }
      }
      else
      {
        RelatedApptSummary relatedApptSummary5 = summ;
        int apptEffTranDuration = relatedApptSummary5.ApptEffTranDuration;
        nullable1 = apptLine.ActualDuration;
        int num5 = nullable1.Value * invtMult;
        relatedApptSummary5.ApptEffTranDuration = apptEffTranDuration + num5;
        nullable3 = apptLine.IsPrepaid;
        if (nullable3.GetValueOrDefault())
        {
          RelatedApptSummary relatedApptSummary6 = summ;
          Decimal apptEffTranQty = relatedApptSummary6.ApptEffTranQty;
          nullable2 = apptLine.ActualQty;
          Decimal num6 = nullable2.Value * (Decimal) invtMult;
          relatedApptSummary6.ApptEffTranQty = apptEffTranQty + num6;
        }
      }
      if (summ.ApptEffTranQty < 0M)
        summ.ApptEffTranQty = 0M;
      if (apptLine.LinkedEntityType != "SO")
      {
        nullable3 = apptLine.IsBillable;
        if (nullable3.GetValueOrDefault())
        {
          RelatedApptSummary relatedApptSummary7 = summ;
          Decimal apptEffTranQty = relatedApptSummary7.ApptEffTranQty;
          nullable2 = apptLine.BillableQty;
          Decimal num7 = nullable2.Value * (Decimal) invtMult;
          relatedApptSummary7.ApptEffTranQty = apptEffTranQty + num7;
          RelatedApptSummary relatedApptSummary8 = summ;
          Decimal baseApptEffTranQty = relatedApptSummary8.BaseApptEffTranQty;
          nullable2 = apptLine.BaseBillableQty;
          Decimal num8 = nullable2.Value * (Decimal) invtMult;
          relatedApptSummary8.BaseApptEffTranQty = baseApptEffTranQty + num8;
        }
        else
        {
          nullable3 = apptLine.AreActualFieldsActive;
          bool flag = false;
          if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
          {
            RelatedApptSummary relatedApptSummary9 = summ;
            Decimal apptEffTranQty = relatedApptSummary9.ApptEffTranQty;
            nullable2 = apptLine.EstimatedQty;
            Decimal num9 = nullable2.Value * (Decimal) invtMult;
            relatedApptSummary9.ApptEffTranQty = apptEffTranQty + num9;
            RelatedApptSummary relatedApptSummary10 = summ;
            Decimal baseApptEffTranQty = relatedApptSummary10.BaseApptEffTranQty;
            nullable2 = apptLine.BaseEstimatedQty;
            Decimal num10 = nullable2.Value * (Decimal) invtMult;
            relatedApptSummary10.BaseApptEffTranQty = baseApptEffTranQty + num10;
          }
          else
          {
            RelatedApptSummary relatedApptSummary11 = summ;
            Decimal apptEffTranQty = relatedApptSummary11.ApptEffTranQty;
            nullable2 = apptLine.ActualQty;
            Decimal num11 = nullable2.Value * (Decimal) invtMult;
            relatedApptSummary11.ApptEffTranQty = apptEffTranQty + num11;
            RelatedApptSummary relatedApptSummary12 = summ;
            Decimal baseApptEffTranQty = relatedApptSummary12.BaseApptEffTranQty;
            nullable2 = apptLine.BaseActualQty;
            Decimal num12 = nullable2.Value * (Decimal) invtMult;
            relatedApptSummary12.BaseApptEffTranQty = baseApptEffTranQty + num12;
          }
        }
      }
      RelatedApptSummary relatedApptSummary13 = summ;
      Decimal curyApptEffTranAmt = relatedApptSummary13.CuryApptEffTranAmt;
      nullable2 = apptLine.CuryBillableTranAmt;
      Decimal num13 = nullable2.Value * (Decimal) invtMult;
      relatedApptSummary13.CuryApptEffTranAmt = curyApptEffTranAmt + num13;
      RelatedApptSummary relatedApptSummary14 = summ;
      Decimal apptEffTranAmt = relatedApptSummary14.ApptEffTranAmt;
      nullable2 = apptLine.BillableTranAmt;
      Decimal num14 = nullable2.Value * (Decimal) invtMult;
      relatedApptSummary14.ApptEffTranAmt = apptEffTranAmt + num14;
    }
    summ.ApptCntrIncludingRequestPO += invtMult;
  }

  public virtual void UpdateRelatedApptSummaryFields(
    PXCache AppointmentDetCache,
    FSAppointmentDet apptLine,
    PXCache SODetCache,
    FSSODet soLine)
  {
    int? soDetId1;
    int? soDetId2;
    if (apptLine.SODetID.HasValue)
    {
      soDetId1 = apptLine.SODetID;
      soDetId2 = soLine.SODetID;
      if (!(soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue))
        throw new PXArgumentException();
    }
    PXEntryStatus status = AppointmentDetCache.GetStatus((object) apptLine);
    string oldApptLineStatus = (string) null;
    string newApptLineStatus = (string) null;
    RelatedApptSummary relatedApptSummary1 = this.CalculateRelatedApptSummary((PXGraph) this, soLine.SODetID, soLine, apptLine.AppDetID, false);
    if (status == 2)
    {
      oldApptLineStatus = (string) null;
      newApptLineStatus = apptLine.Status;
      this.AccumulateRelatedApptLine(relatedApptSummary1, apptLine, 1);
    }
    else if (status == 1)
    {
      FSAppointmentDet original = (FSAppointmentDet) AppointmentDetCache.GetOriginal((object) apptLine);
      oldApptLineStatus = original != null ? original.Status : throw new PXInvalidOperationException();
      newApptLineStatus = apptLine.Status;
      soDetId2 = original.SODetID;
      soDetId1 = apptLine.SODetID;
      if (!(soDetId2.GetValueOrDefault() == soDetId1.GetValueOrDefault() & soDetId2.HasValue == soDetId1.HasValue))
      {
        FSSODet soDetRow = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) original.SODetID
        }));
        if (soDetRow == null)
          throw new PXInvalidOperationException();
        RelatedApptSummary relatedApptSummary2 = this.CalculateRelatedApptSummary((PXGraph) this, soDetRow.SODetID, soDetRow, original.AppDetID, false);
        this.AccumulateRelatedApptLine(relatedApptSummary2, original, -1);
        this.UpdateRelatedApptSummaryFields(SODetCache, soDetRow, relatedApptSummary2, AppointmentDetCache, apptLine.AppDetID, oldApptLineStatus, newApptLineStatus);
      }
      else
        this.AccumulateRelatedApptLine(relatedApptSummary1, original, -1);
      this.AccumulateRelatedApptLine(relatedApptSummary1, apptLine, 1);
    }
    else if (status == 3)
    {
      FSAppointmentDet original = (FSAppointmentDet) AppointmentDetCache.GetOriginal((object) apptLine);
      oldApptLineStatus = original != null ? original.Status : throw new PXInvalidOperationException();
      newApptLineStatus = (string) null;
      this.AccumulateRelatedApptLine(relatedApptSummary1, original, -1);
    }
    soLine = this.UpdateRelatedApptSummaryFields(SODetCache, soLine, relatedApptSummary1, AppointmentDetCache, apptLine.AppDetID, oldApptLineStatus, newApptLineStatus);
  }

  public virtual void ScheduleServiceOrderDetailLine(
    PXCache AppointmentDetCache,
    FSAppointmentDet apptLine,
    PXCache SODetCache,
    FSSODet soLine)
  {
    soLine = (FSSODet) SODetCache.CreateCopy((object) soLine);
    soLine.Status = "SC";
    soLine = (FSSODet) SODetCache.Update((object) soLine);
  }

  public virtual FSSODet UpdateSrvOrdLineStatusBasedOnApptLineStatusChange(
    PXCache soDetCache,
    FSSODet soLine,
    PXCache apptDetCache,
    int? apptDetID,
    string oldApptLineStatus,
    string newApptLineStatus)
  {
    string status = soLine.Status;
    string str;
    if (newApptLineStatus == "NF" && newApptLineStatus != oldApptLineStatus)
    {
      str = "SN";
    }
    else
    {
      object obj;
      soDetCache.RaiseFieldDefaulting<FSSODet.status>((object) soLine, ref obj);
      str = (string) obj;
      switch (newApptLineStatus)
      {
        case "CC":
          List<PXResult<FSAppointmentDet>> list = ((IEnumerable<PXResult<FSAppointmentDet>>) PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.appDetID, NotEqual<Required<FSAppointmentDet.appDetID>>, And<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>>>>.Config>.Select(apptDetCache.Graph, new object[2]
          {
            (object) apptDetID,
            (object) soLine.SODetID
          })).ToList<PXResult<FSAppointmentDet>>();
          if (list.Any<PXResult<FSAppointmentDet>>((Func<PXResult<FSAppointmentDet>, bool>) (p => PXResult<FSAppointmentDet>.op_Implicit(p).Status == "CP")))
          {
            str = "CP";
            break;
          }
          if (list.All<PXResult<FSAppointmentDet>>((Func<PXResult<FSAppointmentDet>, bool>) (p => PXResult<FSAppointmentDet>.op_Implicit(p).Status == "NP" || PXResult<FSAppointmentDet>.op_Implicit(p).Status == "CC")))
          {
            str = "CC";
            break;
          }
          if (list.Any<PXResult<FSAppointmentDet>>((Func<PXResult<FSAppointmentDet>, bool>) (p => PXResult<FSAppointmentDet>.op_Implicit(p).Status == "NF")))
          {
            str = "SN";
            break;
          }
          break;
        case "CP":
          if (((IQueryable<PXResult<FSAppointmentDet>>) PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.appDetID, NotEqual<Required<FSAppointmentDet.appDetID>>, And<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.InProcess>>>>>>>.Config>.Select(apptDetCache.Graph, new object[2]
          {
            (object) apptDetID,
            (object) soLine.SODetID
          })).Count<PXResult<FSAppointmentDet>>() == 0 && str == "SC")
          {
            str = "CP";
            break;
          }
          break;
      }
    }
    soLine.Status = str;
    return soLine;
  }

  public virtual string GetLineDisplayHint(
    PXGraph graph,
    string lineRefNbr,
    string lineDescr,
    int? inventoryID)
  {
    return MessageHelper.GetLineDisplayHint(graph, lineRefNbr, lineDescr, inventoryID);
  }

  public virtual bool ValidateCustomerBillingCycle(
    PXCache cache,
    object row,
    int? billCustomerID,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSSetup setupRecordRow,
    bool justWarn)
  {
    return this.ValidateCustomerBillingCycle<FSServiceOrder.billCustomerID>(cache, row, billCustomerID, fsSrvOrdTypeRow, setupRecordRow, justWarn);
  }

  public virtual int? Get_INItemAcctID_DefaultValue(
    PXGraph graph,
    string salesAcctSource,
    int? inventoryID,
    int? customerID,
    int? locationID)
  {
    return ServiceOrderEntry.Get_INItemAcctID_DefaultValueInt(graph, salesAcctSource, inventoryID, customerID, locationID);
  }

  public virtual Dictionary<FSAppointment, string> CloseAppointments(
    PXResultset<FSAppointment> bqlResultSet)
  {
    return ServiceOrderEntry.CloseAppointmentsInt(bqlResultSet);
  }

  public virtual Dictionary<FSAppointment, string> CancelAppointments(
    ServiceOrderEntry graph,
    PXResultset<FSAppointment> bqlResultSet)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    instance.SetServiceOrderEntryGraph(graph);
    Dictionary<FSAppointment, string> dictionary = new Dictionary<FSAppointment, string>();
    foreach (PXResult<FSAppointment> bqlResult in bqlResultSet)
    {
      FSAppointment key = PXResult<FSAppointment>.op_Implicit(bqlResult);
      try
      {
        bool? notStarted = key.NotStarted;
        bool flag = false;
        if (notStarted.GetValueOrDefault() == flag & notStarted.HasValue)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("The service order cannot be canceled because some appointments have invalid statuses. To cancel a service order, its appointments must have one of the following statuses: {0} or {1}.", new object[2]
          {
            (object) "Not Started",
            (object) "Canceled"
          }));
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) key.AppointmentID, new object[1]
        {
          (object) key.SrvOrdType
        }));
        try
        {
          instance.SkipCallSOAction = true;
          ((PXSelectBase) instance.AppointmentRecords).View.Answer = (WebDialogResult) 7;
          ((PXAction) instance.cancelAppointment).Press();
        }
        finally
        {
          ((PXSelectBase) instance.AppointmentRecords).View.Answer = (WebDialogResult) 0;
          instance.SkipCallSOAction = false;
        }
      }
      catch (PXException ex)
      {
        dictionary.Add(key, ((Exception) ex).Message);
      }
    }
    return dictionary;
  }

  public static Dictionary<FSAppointment, string> CompleteAppointments(
    ServiceOrderEntry graph,
    PXResultset<FSAppointmentInRoute> bqlResultSet)
  {
    Dictionary<FSAppointment, string> dictionary = ServiceOrderEntry.CompleteAppointmentsInt(graph, bqlResultSet);
    return dictionary == null || dictionary.Count == 0 ? (Dictionary<FSAppointment, string>) null : dictionary;
  }

  public virtual string CalculateLineStatus(FSSODet fsSODetRow)
  {
    if (fsSODetRow.IsCommentInstruction)
      return "SN";
    if (fsSODetRow.IsExpenseReceiptItem || fsSODetRow.IsAPBillItem)
      return "CP";
    bool flag = false;
    if (fsSODetRow.BillingRule == "TIME")
    {
      int? estimatedDuration1 = fsSODetRow.ApptEstimatedDuration;
      int? estimatedDuration2 = fsSODetRow.EstimatedDuration;
      if (estimatedDuration1.GetValueOrDefault() >= estimatedDuration2.GetValueOrDefault() & estimatedDuration1.HasValue & estimatedDuration2.HasValue)
      {
        flag = true;
        goto label_9;
      }
    }
    Decimal? apptQty = fsSODetRow.ApptQty;
    Decimal? estimatedQty = fsSODetRow.EstimatedQty;
    if (apptQty.GetValueOrDefault() >= estimatedQty.GetValueOrDefault() & apptQty.HasValue & estimatedQty.HasValue)
      flag = true;
label_9:
    return flag ? "SC" : "SN";
  }

  public static int? Get_INItemAcctID_DefaultValueInt(
    PXGraph graph,
    string salesAcctSource,
    int? inventoryID,
    int? customerID,
    int? locationID)
  {
    int? idDefaultValueInt = new int?();
    switch (salesAcctSource)
    {
      case "II":
        PX.Objects.IN.InventoryItem inventoryItemRow1 = SharedFunctions.GetInventoryItemRow(graph, inventoryID);
        INPostClass inPostClass1 = new INPostClass();
        if (inventoryItemRow1 != null)
        {
          idDefaultValueInt = inventoryItemRow1.SalesAcctID;
          if (!idDefaultValueInt.HasValue)
            idDefaultValueInt = (int?) PXResultset<INPostClass>.op_Implicit(PXSelectBase<INPostClass, PXSelectReadonly<INPostClass, Where<INPostClass.postClassID, Equal<Required<INPostClass.postClassID>>>>.Config>.Select(graph, new object[1]
            {
              (object) inventoryItemRow1.PostClassID
            }))?.SalesAcctID;
        }
        return idDefaultValueInt;
      case "PC":
        PX.Objects.IN.InventoryItem inventoryItemRow2 = SharedFunctions.GetInventoryItemRow(graph, inventoryID);
        INPostClass inPostClass2 = new INPostClass();
        if (inventoryItemRow2 != null)
          idDefaultValueInt = (int?) PXResultset<INPostClass>.op_Implicit(PXSelectBase<INPostClass, PXSelectReadonly<INPostClass, Where<INPostClass.postClassID, Equal<Required<INPostClass.postClassID>>>>.Config>.Select(graph, new object[1]
          {
            (object) inventoryItemRow2.PostClassID
          }))?.SalesAcctID;
        return idDefaultValueInt;
      case "CL":
        if (!customerID.HasValue || !locationID.HasValue)
          return new int?();
        return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(graph, new object[2]
        {
          (object) customerID,
          (object) locationID
        }))?.CSalesAcctID;
      default:
        return new int?();
    }
  }

  public static int? Get_TranAcctID_DefaultValueInt(
    PXGraph graph,
    string salesAcctSource,
    int? inventoryID,
    int? siteID,
    FSServiceOrder fsServiceOrderRow)
  {
    int? idDefaultValueInt1 = new int?();
    switch (salesAcctSource)
    {
      case "II":
        PX.Objects.IN.InventoryItem inventoryItemRow1 = SharedFunctions.GetInventoryItemRow(graph, inventoryID);
        INPostClass inPostClass1 = new INPostClass();
        if (inventoryItemRow1 != null)
        {
          idDefaultValueInt1 = inventoryItemRow1.SalesAcctID;
          if (!idDefaultValueInt1.HasValue)
            idDefaultValueInt1 = (int?) PXResultset<INPostClass>.op_Implicit(PXSelectBase<INPostClass, PXSelectReadonly<INPostClass, Where<INPostClass.postClassID, Equal<Required<INPostClass.postClassID>>>>.Config>.Select(graph, new object[1]
            {
              (object) inventoryItemRow1.PostClassID
            }))?.SalesAcctID;
        }
        return idDefaultValueInt1;
      case "WH":
        if (SharedFunctions.GetInventoryItemRow(graph, inventoryID) != null)
          idDefaultValueInt1 = (int?) PXResultset<PX.Objects.IN.INSite>.op_Implicit(PXSelectBase<PX.Objects.IN.INSite, PXSelectReadonly<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.siteID, Equal<Required<PX.Objects.IN.INSite.siteID>>>>.Config>.Select(graph, new object[1]
          {
            (object) siteID
          }))?.SalesAcctID;
        return idDefaultValueInt1;
      case "PC":
        PX.Objects.IN.InventoryItem inventoryItemRow2 = SharedFunctions.GetInventoryItemRow(graph, inventoryID);
        INPostClass inPostClass2 = new INPostClass();
        if (inventoryItemRow2 != null)
          idDefaultValueInt1 = (int?) PXResultset<INPostClass>.op_Implicit(PXSelectBase<INPostClass, PXSelectReadonly<INPostClass, Where<INPostClass.postClassID, Equal<Required<INPostClass.postClassID>>>>.Config>.Select(graph, new object[1]
          {
            (object) inventoryItemRow2.PostClassID
          }))?.SalesAcctID;
        return idDefaultValueInt1;
      case "CL":
        int? idDefaultValueInt2 = fsServiceOrderRow.CustomerID;
        if (idDefaultValueInt2.HasValue)
        {
          idDefaultValueInt2 = fsServiceOrderRow.LocationID;
          if (idDefaultValueInt2.HasValue)
          {
            PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(graph, new object[2]
            {
              (object) fsServiceOrderRow.BillCustomerID,
              (object) fsServiceOrderRow.BillLocationID
            }));
            if (location != null)
              return location.CSalesAcctID;
            idDefaultValueInt2 = new int?();
            return idDefaultValueInt2;
          }
        }
        idDefaultValueInt2 = new int?();
        return idDefaultValueInt2;
      default:
        return new int?();
    }
  }

  public static int? GetDefaultLocationIDInt(PXGraph graph, int? bAccountID)
  {
    if (!bAccountID.HasValue)
      return new int?();
    return PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.BAccount.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bAccountID
    }))?.LocationID;
  }

  public static string GetBillingMode(
    PXGraph graph,
    FSBillingCycle billingCycle,
    FSSrvOrdType srvOrdType,
    FSServiceOrder serviceOrder)
  {
    if (serviceOrder == null)
      return (string) null;
    string billingMode = serviceOrder.BillingBy;
    if (billingMode == null)
    {
      if (srvOrdType == null)
        srvOrdType = FSSrvOrdType.PK.Find(graph, serviceOrder.SrvOrdType);
      if (srvOrdType?.Behavior == "QT" || srvOrdType?.Behavior == "IN")
      {
        billingMode = (string) null;
      }
      else
      {
        if (billingCycle == null)
          billingCycle = PXResultset<FSBillingCycle>.op_Implicit(PXSelectBase<FSBillingCycle, PXSelectJoin<FSBillingCycle, InnerJoin<FSCustomerBillingSetup, On<FSBillingCycle.billingCycleID, Equal<FSCustomerBillingSetup.billingCycleID>>, CrossJoin<FSSetup>>, Where<FSCustomerBillingSetup.customerID, Equal<Required<FSServiceOrder.billCustomerID>>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>>.Config>.Select(graph, new object[2]
          {
            (object) serviceOrder.BillCustomerID,
            (object) serviceOrder.SrvOrdType
          }));
        billingMode = billingCycle?.BillingBy;
      }
    }
    return billingMode;
  }

  public static void UpdateServiceOrderUnboundFields(
    PXGraph graph,
    FSServiceOrder fsServiceOrderRow)
  {
    string billingMode = ServiceOrderEntry.GetBillingMode(graph, (FSBillingCycle) null, (FSSrvOrdType) null, fsServiceOrderRow);
    ServiceOrderEntry.UpdateServiceOrderUnboundFields(graph, fsServiceOrderRow, (FSAppointment) null, billingMode, false, false);
  }

  public static void UpdateServiceOrderUnboundFields(
    PXGraph graph,
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    string billingBy,
    bool disableServiceOrderUnboundFieldCalc,
    bool calcPrepaymentAmount)
  {
    if (fsServiceOrderRow == null || disableServiceOrderUnboundFieldCalc)
      return;
    fsServiceOrderRow.AppointmentDocTotal = new Decimal?(0M);
    fsServiceOrderRow.AppointmentTaxTotal = new Decimal?(0M);
    if (fsAppointmentRow != null)
      fsAppointmentRow.AppCompletedBillableTotal = new Decimal?(0M);
    int? nullable1;
    if (fsAppointmentRow != null)
    {
      int? appointmentId = fsAppointmentRow.AppointmentID;
      int num = 0;
      if (appointmentId.GetValueOrDefault() > num & appointmentId.HasValue)
      {
        nullable1 = fsAppointmentRow.AppointmentID;
        goto label_8;
      }
    }
    nullable1 = new int?();
label_8:
    int? nullable2 = nullable1;
    IQueryable<\u003C\u003Ef__AnonymousType16<Decimal?, Decimal?, Decimal?, Decimal?, Decimal?, bool?, bool?, bool?, bool?>> queryable = ((IQueryable<PXResult<FSAppointment>>) PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>, And2<Where<Required<FSAppointment.appointmentID>, IsNull, Or<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>, And<Where<FSAppointment.canceled, Equal<False>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) fsServiceOrderRow.SOID,
      (object) nullable2,
      (object) nullable2
    })).Select(appt => new
    {
      CuryBillableLineTotal = ((FSAppointment) appt).CuryBillableLineTotal,
      CuryTaxTotal = ((FSAppointment) appt).CuryTaxTotal,
      CuryDocTotal = ((FSAppointment) appt).CuryDocTotal,
      CuryCostTotal = ((FSAppointment) appt).CuryCostTotal,
      CuryLogBillableTranAmountTotal = ((FSAppointment) appt).CuryLogBillableTranAmountTotal,
      InProcess = ((FSAppointment) appt).InProcess,
      Completed = ((FSAppointment) appt).Completed,
      Closed = ((FSAppointment) appt).Closed,
      Paused = ((FSAppointment) appt).Paused
    });
    FSAppointment fsAppointment1 = new FSAppointment()
    {
      CuryBillableLineTotal = new Decimal?(0M),
      CuryTaxTotal = new Decimal?(0M),
      CuryDocTotal = new Decimal?(0M),
      CuryCostTotal = new Decimal?(0M),
      CuryLogBillableTranAmountTotal = new Decimal?(0M)
    };
    FSAppointment fsAppointment2 = new FSAppointment()
    {
      CuryBillableLineTotal = new Decimal?(0M),
      CuryTaxTotal = new Decimal?(0M),
      CuryDocTotal = new Decimal?(0M),
      CuryCostTotal = new Decimal?(0M),
      CuryLogBillableTranAmountTotal = new Decimal?(0M)
    };
    foreach (var data in queryable)
    {
      FSAppointment fsAppointment3 = fsAppointment1;
      Decimal? nullable3 = fsAppointment3.CuryBillableLineTotal;
      Decimal? nullable4 = data.CuryBillableLineTotal;
      fsAppointment3.CuryBillableLineTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment4 = fsAppointment1;
      nullable4 = fsAppointment4.CuryTaxTotal;
      nullable3 = data.CuryTaxTotal;
      fsAppointment4.CuryTaxTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment5 = fsAppointment1;
      nullable3 = fsAppointment5.CuryDocTotal;
      nullable4 = data.CuryDocTotal;
      fsAppointment5.CuryDocTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment6 = fsAppointment1;
      nullable4 = fsAppointment6.CuryCostTotal;
      nullable3 = data.CuryCostTotal;
      fsAppointment6.CuryCostTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment7 = fsAppointment1;
      nullable3 = fsAppointment7.CuryLogBillableTranAmountTotal;
      nullable4 = data.CuryLogBillableTranAmountTotal;
      fsAppointment7.CuryLogBillableTranAmountTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      bool? nullable5 = data.InProcess;
      if (!nullable5.GetValueOrDefault())
      {
        nullable5 = data.Completed;
        if (!nullable5.GetValueOrDefault())
        {
          nullable5 = data.Closed;
          if (!nullable5.GetValueOrDefault())
          {
            nullable5 = data.Paused;
            if (!nullable5.GetValueOrDefault())
              continue;
          }
        }
      }
      FSAppointment fsAppointment8 = fsAppointment2;
      nullable4 = fsAppointment8.CuryBillableLineTotal;
      nullable3 = data.CuryBillableLineTotal;
      fsAppointment8.CuryBillableLineTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment9 = fsAppointment2;
      nullable3 = fsAppointment9.CuryTaxTotal;
      nullable4 = data.CuryTaxTotal;
      fsAppointment9.CuryTaxTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment10 = fsAppointment2;
      nullable4 = fsAppointment10.CuryDocTotal;
      nullable3 = data.CuryDocTotal;
      fsAppointment10.CuryDocTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment11 = fsAppointment2;
      nullable3 = fsAppointment11.CuryCostTotal;
      nullable4 = data.CuryCostTotal;
      fsAppointment11.CuryCostTotal = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      FSAppointment fsAppointment12 = fsAppointment2;
      nullable4 = fsAppointment12.CuryLogBillableTranAmountTotal;
      nullable3 = data.CuryLogBillableTranAmountTotal;
      fsAppointment12.CuryLogBillableTranAmountTotal = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    }
    fsServiceOrderRow.CuryAppointmentTaxTotal = fsAppointment1.CuryTaxTotal;
    fsServiceOrderRow.CuryEffectiveLogBillableTranAmountTotal = fsAppointment1.CuryLogBillableTranAmountTotal;
    bool? nullable6;
    if (fsAppointmentRow != null)
    {
      nullable6 = fsAppointmentRow.InProcess;
      if (!nullable6.GetValueOrDefault())
      {
        nullable6 = fsAppointmentRow.Completed;
        if (!nullable6.GetValueOrDefault())
        {
          nullable6 = fsAppointmentRow.Closed;
          if (!nullable6.GetValueOrDefault())
          {
            nullable6 = fsAppointmentRow.Paused;
            if (!nullable6.GetValueOrDefault())
              goto label_25;
          }
        }
      }
      FSServiceOrder fsServiceOrder = fsServiceOrderRow;
      Decimal? appointmentTaxTotal = fsServiceOrder.CuryAppointmentTaxTotal;
      Decimal? curyTaxTotal = fsAppointmentRow.CuryTaxTotal;
      fsServiceOrder.CuryAppointmentTaxTotal = appointmentTaxTotal.HasValue & curyTaxTotal.HasValue ? new Decimal?(appointmentTaxTotal.GetValueOrDefault() + curyTaxTotal.GetValueOrDefault()) : new Decimal?();
    }
label_25:
    FSServiceOrder fsServiceOrder1 = fsServiceOrderRow;
    Decimal? curyApptOrderTotal = fsServiceOrderRow.CuryApptOrderTotal;
    Decimal? appointmentTaxTotal1 = fsServiceOrderRow.CuryAppointmentTaxTotal;
    Decimal? nullable7 = curyApptOrderTotal.HasValue & appointmentTaxTotal1.HasValue ? new Decimal?(curyApptOrderTotal.GetValueOrDefault() + appointmentTaxTotal1.GetValueOrDefault()) : new Decimal?();
    fsServiceOrder1.CuryAppointmentDocTotal = nullable7;
    Decimal? nullable8;
    Decimal? nullable9;
    Decimal? nullable10;
    Decimal num1;
    Decimal? nullable11;
    switch (billingBy)
    {
      case "AP":
        fsServiceOrderRow.CuryEffectiveBillableLineTotal = new Decimal?(0M);
        fsServiceOrderRow.CuryEffectiveBillableTaxTotal = new Decimal?(0M);
        fsServiceOrderRow.CuryEffectiveBillableDocTotal = new Decimal?(0M);
        fsServiceOrderRow.CuryEffectiveCostTotal = new Decimal?(0M);
        fsServiceOrderRow.CuryEffectiveLogBillableTranAmountTotal = new Decimal?(0M);
        fsServiceOrderRow.CuryEffectiveBillableLineTotal = fsAppointment2.CuryBillableLineTotal;
        fsServiceOrderRow.CuryEffectiveBillableTaxTotal = fsAppointment2.CuryTaxTotal;
        fsServiceOrderRow.CuryEffectiveBillableDocTotal = fsAppointment2.CuryDocTotal;
        fsServiceOrderRow.CuryEffectiveCostTotal = fsAppointment2.CuryCostTotal;
        fsServiceOrderRow.CuryEffectiveLogBillableTranAmountTotal = fsAppointment2.CuryLogBillableTranAmountTotal;
        if (fsAppointmentRow != null)
        {
          nullable6 = fsAppointmentRow.InProcess;
          if (!nullable6.GetValueOrDefault())
          {
            nullable6 = fsAppointmentRow.Completed;
            if (!nullable6.GetValueOrDefault())
            {
              nullable6 = fsAppointmentRow.Closed;
              if (!nullable6.GetValueOrDefault())
              {
                nullable6 = fsAppointmentRow.Paused;
                if (!nullable6.GetValueOrDefault())
                  break;
              }
            }
          }
          fsAppointmentRow.AppCompletedBillableTotal = fsAppointmentRow.CuryDocTotal;
          FSServiceOrder fsServiceOrder2 = fsServiceOrderRow;
          Decimal? billableLineTotal1 = fsServiceOrder2.CuryEffectiveBillableLineTotal;
          Decimal? billableLineTotal2 = fsAppointmentRow.CuryBillableLineTotal;
          fsServiceOrder2.CuryEffectiveBillableLineTotal = billableLineTotal1.HasValue & billableLineTotal2.HasValue ? new Decimal?(billableLineTotal1.GetValueOrDefault() + billableLineTotal2.GetValueOrDefault()) : new Decimal?();
          FSServiceOrder fsServiceOrder3 = fsServiceOrderRow;
          nullable8 = fsServiceOrder3.CuryEffectiveBillableTaxTotal;
          Decimal? curyTaxTotal = fsAppointmentRow.CuryTaxTotal;
          fsServiceOrder3.CuryEffectiveBillableTaxTotal = nullable8.HasValue & curyTaxTotal.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + curyTaxTotal.GetValueOrDefault()) : new Decimal?();
          FSServiceOrder fsServiceOrder4 = fsServiceOrderRow;
          nullable9 = fsServiceOrder4.CuryEffectiveBillableDocTotal;
          nullable8 = fsAppointmentRow.CuryDocTotal;
          fsServiceOrder4.CuryEffectiveBillableDocTotal = nullable9.HasValue & nullable8.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
          FSServiceOrder fsServiceOrder5 = fsServiceOrderRow;
          nullable8 = fsServiceOrder5.CuryEffectiveCostTotal;
          nullable9 = fsAppointmentRow.CuryCostTotal;
          fsServiceOrder5.CuryEffectiveCostTotal = nullable8.HasValue & nullable9.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
          FSServiceOrder fsServiceOrder6 = fsServiceOrderRow;
          nullable9 = fsServiceOrder6.CuryEffectiveLogBillableTranAmountTotal;
          nullable8 = fsAppointmentRow.CuryLogBillableTranAmountTotal;
          fsServiceOrder6.CuryEffectiveLogBillableTranAmountTotal = nullable9.HasValue & nullable8.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
          FSServiceOrder fsServiceOrder7 = fsServiceOrderRow;
          nullable8 = fsServiceOrderRow.CuryEffectiveCostTotal;
          Decimal? nullable12;
          if (!(nullable8.GetValueOrDefault() == 0M))
          {
            nullable9 = fsServiceOrderRow.CuryApptOrderTotal;
            nullable10 = fsServiceOrderRow.CuryEffectiveCostTotal;
            nullable8 = nullable9.HasValue & nullable10.HasValue ? new Decimal?(nullable9.GetValueOrDefault() / nullable10.GetValueOrDefault()) : new Decimal?();
            num1 = (Decimal) 100;
            if (!nullable8.HasValue)
            {
              nullable10 = new Decimal?();
              nullable12 = nullable10;
            }
            else
              nullable12 = new Decimal?(nullable8.GetValueOrDefault() * num1);
          }
          else
          {
            num1 = 0M;
            nullable12 = new Decimal?(num1);
          }
          fsServiceOrder7.ProfitPercent = nullable12;
          break;
        }
        break;
      case "SO":
        fsServiceOrderRow.CuryEffectiveBillableLineTotal = fsServiceOrderRow.CuryBillableOrderTotal;
        fsServiceOrderRow.CuryEffectiveBillableTaxTotal = fsServiceOrderRow.CuryTaxTotal;
        fsServiceOrderRow.CuryEffectiveBillableDocTotal = fsServiceOrderRow.CuryDocTotal;
        fsServiceOrderRow.CuryEffectiveCostTotal = fsServiceOrderRow.CuryCostTotal;
        FSServiceOrder fsServiceOrder8 = fsServiceOrderRow;
        nullable8 = fsServiceOrderRow.CuryEffectiveCostTotal;
        Decimal? nullable13;
        if (!(nullable8.GetValueOrDefault() == 0M))
        {
          Decimal? billableOrderTotal = fsServiceOrderRow.CuryBillableOrderTotal;
          nullable9 = fsServiceOrderRow.CuryEffectiveCostTotal;
          nullable8 = billableOrderTotal.HasValue & nullable9.HasValue ? new Decimal?(billableOrderTotal.GetValueOrDefault() / nullable9.GetValueOrDefault()) : new Decimal?();
          Decimal num2 = (Decimal) 100;
          if (!nullable8.HasValue)
          {
            nullable9 = new Decimal?();
            nullable13 = nullable9;
          }
          else
            nullable13 = new Decimal?(nullable8.GetValueOrDefault() * num2);
        }
        else
          nullable13 = new Decimal?(0M);
        fsServiceOrder8.ProfitPercent = nullable13;
        FSServiceOrder fsServiceOrder9 = fsServiceOrderRow;
        nullable8 = fsServiceOrderRow.CuryBillableOrderTotal;
        Decimal? nullable14;
        if (!(nullable8.GetValueOrDefault() == 0M))
        {
          Decimal? billableOrderTotal1 = fsServiceOrderRow.CuryBillableOrderTotal;
          nullable11 = fsServiceOrderRow.CuryEffectiveCostTotal;
          nullable9 = billableOrderTotal1.HasValue & nullable11.HasValue ? new Decimal?(billableOrderTotal1.GetValueOrDefault() - nullable11.GetValueOrDefault()) : new Decimal?();
          Decimal? billableOrderTotal2 = fsServiceOrderRow.CuryBillableOrderTotal;
          Decimal? nullable15;
          if (!(nullable9.HasValue & billableOrderTotal2.HasValue))
          {
            nullable11 = new Decimal?();
            nullable15 = nullable11;
          }
          else
            nullable15 = new Decimal?(nullable9.GetValueOrDefault() / billableOrderTotal2.GetValueOrDefault());
          nullable8 = nullable15;
          num1 = (Decimal) 100;
          nullable14 = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() * num1) : new Decimal?();
        }
        else
        {
          num1 = 0M;
          nullable14 = new Decimal?(num1);
        }
        fsServiceOrder9.ProfitMarginPercent = nullable14;
        break;
    }
    if (graph is ServiceOrderEntry)
    {
      ServiceOrderEntry serviceOrderEntry = (ServiceOrderEntry) graph;
      fsServiceOrderRow.CuryEffectiveCostTotal = new Decimal?(serviceOrderEntry.CalcEffectiveCostTotal(fsServiceOrderRow));
      nullable8 = fsServiceOrderRow.CuryEffectiveBillableLineTotal;
      if (nullable8.HasValue)
      {
        nullable8 = fsServiceOrderRow.CuryEffectiveCostTotal;
        if (nullable8.HasValue)
        {
          Decimal? effectiveCostTotal = fsServiceOrderRow.CuryEffectiveCostTotal;
          Decimal? nullable16 = billingBy == "SO" ? fsServiceOrderRow.CuryBillableOrderTotal : fsServiceOrderRow.CuryApptOrderTotal;
          nullable8 = nullable16;
          nullable10 = effectiveCostTotal;
          Decimal? nullable17;
          if (!(nullable8.HasValue & nullable10.HasValue))
          {
            nullable9 = new Decimal?();
            nullable17 = nullable9;
          }
          else
            nullable17 = new Decimal?(nullable8.GetValueOrDefault() - nullable10.GetValueOrDefault());
          Decimal? nullable18 = nullable17;
          nullable10 = fsServiceOrderRow.CuryEffectiveLogBillableTranAmountTotal;
          if (nullable10.HasValue)
          {
            nullable10 = nullable18;
            nullable8 = fsServiceOrderRow.CuryEffectiveLogBillableTranAmountTotal;
            num1 = Math.Round(nullable8.Value, 2);
            Decimal? nullable19;
            if (!nullable10.HasValue)
            {
              nullable8 = new Decimal?();
              nullable19 = nullable8;
            }
            else
              nullable19 = new Decimal?(nullable10.GetValueOrDefault() + num1);
            nullable18 = nullable19;
          }
          FSServiceOrder fsServiceOrder10 = fsServiceOrderRow;
          nullable10 = effectiveCostTotal;
          num1 = 0.0M;
          Decimal? nullable20;
          if (!(nullable10.GetValueOrDefault() == num1 & nullable10.HasValue) && effectiveCostTotal.HasValue)
          {
            nullable8 = nullable18;
            nullable9 = effectiveCostTotal;
            Decimal? nullable21;
            if (!(nullable8.HasValue & nullable9.HasValue))
            {
              nullable11 = new Decimal?();
              nullable21 = nullable11;
            }
            else
              nullable21 = new Decimal?(nullable8.GetValueOrDefault() / nullable9.GetValueOrDefault());
            nullable10 = nullable21;
            num1 = (Decimal) 100;
            if (!nullable10.HasValue)
            {
              nullable9 = new Decimal?();
              nullable20 = nullable9;
            }
            else
              nullable20 = new Decimal?(nullable10.GetValueOrDefault() * num1);
          }
          else
            nullable20 = new Decimal?(0.0M);
          fsServiceOrder10.ProfitPercent = nullable20;
          FSServiceOrder fsServiceOrder11 = fsServiceOrderRow;
          nullable10 = nullable16;
          num1 = 0.0M;
          Decimal? nullable22;
          if (!(nullable10.GetValueOrDefault() == num1 & nullable10.HasValue) && nullable16.HasValue)
          {
            nullable9 = nullable18;
            nullable8 = nullable16;
            Decimal? nullable23;
            if (!(nullable9.HasValue & nullable8.HasValue))
            {
              nullable11 = new Decimal?();
              nullable23 = nullable11;
            }
            else
              nullable23 = new Decimal?(nullable9.GetValueOrDefault() / nullable8.GetValueOrDefault());
            nullable10 = nullable23;
            num1 = (Decimal) 100;
            if (!nullable10.HasValue)
            {
              nullable8 = new Decimal?();
              nullable22 = nullable8;
            }
            else
              nullable22 = new Decimal?(nullable10.GetValueOrDefault() * num1);
          }
          else
            nullable22 = new Decimal?(0.0M);
          fsServiceOrder11.ProfitMarginPercent = nullable22;
        }
      }
    }
    fsServiceOrderRow.SOPrepaymentReceived = new Decimal?(0M);
    fsServiceOrderRow.SOPrepaymentRemaining = new Decimal?(0M);
    fsServiceOrderRow.SOPrepaymentApplied = new Decimal?(0M);
    fsServiceOrderRow.SOCuryUnpaidBalanace = new Decimal?(0M);
    fsServiceOrderRow.SOCuryBillableUnpaidBalanace = new Decimal?(0M);
    if (!calcPrepaymentAmount)
      return;
    PXResultset<ARPayment> pxResultset = PXSelectBase<ARPayment, PXSelectJoin<ARPayment, InnerJoin<FSAdjust, On<ARPayment.docType, Equal<FSAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<FSAdjust.adjgRefNbr>>>>, Where<FSAdjust.adjdOrderType, Equal<Required<FSServiceOrder.srvOrdType>>, And<FSAdjust.adjdOrderNbr, Equal<Required<FSServiceOrder.refNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) fsServiceOrderRow.SrvOrdType,
      (object) fsServiceOrderRow.RefNbr
    });
    FSServiceOrder fsServiceOrder12 = fsServiceOrderRow;
    nullable10 = fsServiceOrderRow.CuryDocTotal;
    Decimal? nullable24 = new Decimal?(nullable10.GetValueOrDefault());
    fsServiceOrder12.SOCuryUnpaidBalanace = nullable24;
    FSServiceOrder fsServiceOrder13 = fsServiceOrderRow;
    nullable10 = fsServiceOrderRow.CuryEffectiveBillableDocTotal;
    Decimal? nullable25 = new Decimal?(nullable10.GetValueOrDefault());
    fsServiceOrder13.SOCuryBillableUnpaidBalanace = nullable25;
    foreach (PXResult<ARPayment> pxResult in pxResultset)
    {
      ARPayment row = PXResult<ARPayment>.op_Implicit(pxResult);
      ServiceOrderEntry.RecalcSOApplAmountsInt(graph, row);
      if (row.Status != "V")
      {
        FSServiceOrder fsServiceOrder14 = fsServiceOrderRow;
        nullable10 = fsServiceOrder14.SOPrepaymentReceived;
        nullable8 = row.CuryOrigDocAmt;
        num1 = nullable8.GetValueOrDefault();
        Decimal? nullable26;
        if (!nullable10.HasValue)
        {
          nullable8 = new Decimal?();
          nullable26 = nullable8;
        }
        else
          nullable26 = new Decimal?(nullable10.GetValueOrDefault() + num1);
        fsServiceOrder14.SOPrepaymentReceived = nullable26;
        FSServiceOrder fsServiceOrder15 = fsServiceOrderRow;
        nullable10 = fsServiceOrder15.SOPrepaymentApplied;
        nullable9 = row.CuryApplAmt;
        nullable11 = row.CurySOApplAmt;
        nullable8 = nullable9.HasValue & nullable11.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + nullable11.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable27;
        if (!(nullable10.HasValue & nullable8.HasValue))
        {
          nullable11 = new Decimal?();
          nullable27 = nullable11;
        }
        else
          nullable27 = new Decimal?(nullable10.GetValueOrDefault() + nullable8.GetValueOrDefault());
        fsServiceOrder15.SOPrepaymentApplied = nullable27;
        FSServiceOrder fsServiceOrder16 = fsServiceOrderRow;
        nullable8 = fsServiceOrder16.SOPrepaymentRemaining;
        nullable9 = row.CuryDocBal;
        num1 = nullable9.GetValueOrDefault();
        nullable9 = row.CuryApplAmt;
        Decimal? curySoApplAmt = row.CurySOApplAmt;
        nullable11 = nullable9.HasValue & curySoApplAmt.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + curySoApplAmt.GetValueOrDefault()) : new Decimal?();
        nullable10 = nullable11.HasValue ? new Decimal?(num1 - nullable11.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable28;
        if (!(nullable8.HasValue & nullable10.HasValue))
        {
          nullable11 = new Decimal?();
          nullable28 = nullable11;
        }
        else
          nullable28 = new Decimal?(nullable8.GetValueOrDefault() + nullable10.GetValueOrDefault());
        fsServiceOrder16.SOPrepaymentRemaining = nullable28;
        FSServiceOrder fsServiceOrder17 = fsServiceOrderRow;
        nullable10 = fsServiceOrder17.SOCuryUnpaidBalanace;
        nullable8 = row.CuryOrigDocAmt;
        num1 = nullable8.GetValueOrDefault();
        Decimal? nullable29;
        if (!nullable10.HasValue)
        {
          nullable8 = new Decimal?();
          nullable29 = nullable8;
        }
        else
          nullable29 = new Decimal?(nullable10.GetValueOrDefault() - num1);
        fsServiceOrder17.SOCuryUnpaidBalanace = nullable29;
        FSServiceOrder fsServiceOrder18 = fsServiceOrderRow;
        nullable10 = fsServiceOrder18.SOCuryBillableUnpaidBalanace;
        nullable8 = row.CuryOrigDocAmt;
        num1 = nullable8.GetValueOrDefault();
        Decimal? nullable30;
        if (!nullable10.HasValue)
        {
          nullable8 = new Decimal?();
          nullable30 = nullable8;
        }
        else
          nullable30 = new Decimal?(nullable10.GetValueOrDefault() - num1);
        fsServiceOrder18.SOCuryBillableUnpaidBalanace = nullable30;
      }
    }
  }

  public static void RecalcSOApplAmountsInt(PXGraph graph, ARPayment row)
  {
    Decimal? nullable1;
    Decimal? nullable2;
    if (!row.SOApplAmt.HasValue && !row.CurySOApplAmt.HasValue)
    {
      row.SOApplAmt = new Decimal?(0M);
      row.CurySOApplAmt = new Decimal?(0M);
      SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectGroupBy<SOAdjust, Where<SOAdjust.adjgDocType, Equal<Required<SOAdjust.adjgDocType>>, And<SOAdjust.adjgRefNbr, Equal<Required<SOAdjust.adjgRefNbr>>>>, Aggregate<GroupBy<SOAdjust.adjgDocType, GroupBy<SOAdjust.adjgRefNbr, Sum<SOAdjust.curyAdjgAmt, Sum<SOAdjust.adjAmt>>>>>>.Config>.Select(graph, new object[2]
      {
        (object) row.DocType,
        (object) row.RefNbr
      }));
      if (soAdjust != null && soAdjust.AdjdOrderNbr != null)
      {
        ARPayment arPayment1 = row;
        Decimal? soApplAmt = arPayment1.SOApplAmt;
        nullable1 = soAdjust.AdjAmt;
        arPayment1.SOApplAmt = soApplAmt.HasValue & nullable1.HasValue ? new Decimal?(soApplAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        ARPayment arPayment2 = row;
        nullable1 = arPayment2.CurySOApplAmt;
        nullable2 = soAdjust.CuryAdjgAmt;
        arPayment2.CurySOApplAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      }
    }
    nullable2 = row.ApplAmt;
    if (nullable2.HasValue)
      return;
    nullable2 = row.CuryApplAmt;
    if (nullable2.HasValue)
      return;
    row.ApplAmt = new Decimal?(0M);
    row.CuryApplAmt = new Decimal?(0M);
    ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectGroupBy<ARAdjust, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.released, Equal<False>>>>, Aggregate<GroupBy<ARAdjust.adjgDocType, GroupBy<ARAdjust.adjgRefNbr, Sum<ARAdjust.curyAdjgAmt, Sum<ARAdjust.adjAmt>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    }));
    if (arAdjust == null || arAdjust.AdjdRefNbr == null)
      return;
    ARPayment arPayment3 = row;
    nullable2 = arPayment3.ApplAmt;
    nullable1 = arAdjust.AdjAmt;
    arPayment3.ApplAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    ARPayment arPayment4 = row;
    nullable1 = arPayment4.CuryApplAmt;
    nullable2 = arAdjust.CuryAdjgAmt;
    arPayment4.CuryApplAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
  }

  public static Dictionary<FSAppointment, string> CloseAppointmentsInt(
    PXResultset<FSAppointment> bqlResultSet)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    Dictionary<FSAppointment, string> dictionary = new Dictionary<FSAppointment, string>();
    foreach (PXResult<FSAppointment> bqlResult in bqlResultSet)
    {
      FSAppointment key = PXResult<FSAppointment>.op_Implicit(bqlResult);
      bool? nullable = key.Closed;
      if (!nullable.GetValueOrDefault())
      {
        nullable = key.Canceled;
        if (!nullable.GetValueOrDefault())
        {
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) key.AppointmentID, new object[1]
          {
            (object) key.SrvOrdType
          }));
          try
          {
            try
            {
              instance.SkipCallSOAction = true;
              ((PXAction) instance.closeAppointment).Press();
            }
            finally
            {
              instance.SkipCallSOAction = false;
            }
          }
          catch (PXException ex)
          {
            dictionary.Add(key, ((Exception) ex).Message);
          }
        }
      }
    }
    return dictionary;
  }

  public static Dictionary<FSAppointment, string> CompleteAppointmentsInt(
    ServiceOrderEntry graph,
    PXResultset<FSAppointmentInRoute> bqlResultSet)
  {
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    instance.SetServiceOrderEntryGraph(graph);
    Dictionary<FSAppointment, string> dictionary = new Dictionary<FSAppointment, string>();
    foreach (PXResult<FSAppointmentInRoute> bqlResult in bqlResultSet)
    {
      FSAppointment key = (FSAppointment) PXResult<FSAppointmentInRoute>.op_Implicit(bqlResult);
      if (!key.Completed.GetValueOrDefault() && !key.Closed.GetValueOrDefault() && !key.Canceled.GetValueOrDefault())
      {
        ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.appointmentID>((object) key.AppointmentID, new object[1]
        {
          (object) key.SrvOrdType
        }));
        if (!((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.ActualDateTimeEnd.HasValue)
          ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current.ActualDateTimeEnd = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) graph).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
        try
        {
          try
          {
            instance.SkipCallSOAction = true;
            ((PXAction) instance.completeAppointment).Press();
          }
          finally
          {
            instance.SkipCallSOAction = false;
          }
        }
        catch (PXException ex)
        {
          dictionary.Add(key, ((Exception) ex).Message);
        }
      }
    }
    return dictionary;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<FSServiceOrder, FSServiceOrder.billingBy> e)
  {
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<FSServiceOrder, FSServiceOrder.billingBy>>) e).ReturnValue = (object) this.GetBillingMode(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSServiceOrder, FSServiceOrder.salesPersonID> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceOrder, FSServiceOrder.salesPersonID>, FSServiceOrder, object>) e).NewValue = (object) current.SalesPersonID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.projectID> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    this.FSServiceOrder_ProjectID_FieldUpdated_PartialHandler(row, (PXSelectBase<FSSODet>) this.ServiceOrderDetails);
    ((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Current = PXResultset<PX.Objects.CT.Contract>.op_Implicit(((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Select(new object[1]
    {
      (object) row.ProjectID
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchID> e)
  {
    if (e.Row == null)
      return;
    this.FSServiceOrder_BranchID_FieldUpdated_PartialHandler(e.Row, (PXSelectBase<FSSODet>) this.ServiceOrderDetails);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.orderDate> e)
  {
    if (e.Row == null)
      return;
    this.IsOrderDateFieldUpdated = true;
    FSServiceOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.orderDate>>) e).Cache;
    cache.SetDefaultExt<FSServiceOrder.billContractPeriodID>((object) row);
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.orderDate>, FSServiceOrder, object>) e).OldValue);
    DateTime? orderDate = row.OrderDate;
    if ((handlingDateTime.HasValue == orderDate.HasValue ? (handlingDateTime.HasValue ? (handlingDateTime.GetValueOrDefault() != orderDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      this.RefreshSalesPricesInTheWholeDocument((PXSelectBase<FSSODet>) this.ServiceOrderDetails);
      foreach (PXResult<FSSODet> pxResult in ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()))
      {
        FSSODet fsSODetRow = PXResult<FSSODet>.op_Implicit(pxResult);
        this.UpdateWarrantyFlag(cache, (IFSSODetBase) fsSODetRow, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.OrderDate);
        ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Update(fsSODetRow);
      }
    }
    this.IsOrderDateFieldUpdated = false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billServiceContractID> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    int? nullable1 = row.BillServiceContractID;
    if (!nullable1.HasValue)
    {
      FSServiceOrder fsServiceOrder = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      fsServiceOrder.BillContractPeriodID = nullable2;
      ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.ServiceContractCleared))).FireOn((PXGraph) this, e.Row);
    }
    else
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billServiceContractID>>) e).Cache.SetDefaultExt<FSServiceOrder.billContractPeriodID>((object) row);
      FSServiceContract fsServiceContract = FSServiceContract.PK.Find(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billServiceContractID>>) e).Cache.Graph, row.BillServiceContractID);
      if (!((PXGraph) this).IsCopyPasteContext && fsServiceContract != null)
      {
        nullable1 = e.Row.ProjectID;
        int? projectId = fsServiceContract.ProjectID;
        if (!(nullable1.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable1.HasValue == projectId.HasValue))
        {
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billServiceContractID>>) e).Cache.SetValueExt<FSServiceOrder.projectID>((object) row, (object) fsServiceContract.ProjectID);
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billServiceContractID>>) e).Cache.SetValueExt<FSServiceOrder.dfltProjectTaskID>((object) row, (object) fsServiceContract.DfltProjectTaskID);
        }
      }
      this.SetBillCustomerAndLocationID(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billServiceContractID>>) e).Cache, row);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billContractPeriodID> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billContractPeriodID>>) e).Cache;
    string billingMode = this.GetBillingMode(row);
    int? nullable = row.BillServiceContractID;
    if (nullable.HasValue && ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current?.BillingType == "STDB")
    {
      nullable = row.BillContractPeriodID;
      if (nullable.HasValue)
        ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.ServiceContractPeriodAssigned))).FireOn((PXGraph) this, e.Row);
      else if (billingMode == "SO")
        ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.RequiredServiceContractPeriodCleared))).FireOn((PXGraph) this, e.Row);
    }
    nullable = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billContractPeriodID>, FSServiceOrder, object>) e).OldValue;
    int? contractPeriodId = row.BillContractPeriodID;
    if (nullable.GetValueOrDefault() == contractPeriodId.GetValueOrDefault() & nullable.HasValue == contractPeriodId.HasValue)
      return;
    ((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Current = PXResultset<FSContractPeriod>.op_Implicit(((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Select(Array.Empty<object>()));
    ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Current = PXResultset<FSContractPeriodDet>.op_Implicit(((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>()));
    foreach (PXResult<FSSODet> pxResult in ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()))
    {
      FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult);
      ((PXSelectBase) this.ServiceOrderDetails).Cache.SetDefaultExt<FSSODet.contractRelated>((object) fssoDet);
      contractPeriodId = row.BillContractPeriodID;
      if (contractPeriodId.HasValue)
        ((PXSelectBase) this.ServiceOrderDetails).Cache.SetDefaultExt<FSSODet.isFree>((object) fssoDet);
      ((PXSelectBase) this.ServiceOrderDetails).Cache.Update((object) fssoDet);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.allowInvoice> e)
  {
    if (e.Row == null)
      return;
    this.updateContractPeriod = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>>) e).Cache;
    this.FSServiceOrder_BillCustomerID_FieldUpdated_Handler(cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>>) e).Args);
    this.ValidateCustomerBillingCycle(cache, (object) e.Row, e.Row.BillCustomerID, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, true);
    foreach (PXResult<FSSODet> pxResult in ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()))
    {
      FSSODet fssoDet1 = PXResult<FSSODet>.op_Implicit(pxResult);
      fssoDet1.BillCustomerID = (int?) e.NewValue;
      int? nullable1 = fssoDet1.SMEquipmentID;
      if (nullable1.HasValue)
      {
        FSSODet fssoDet2 = fssoDet1;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        fssoDet2.SMEquipmentID = nullable2;
        FSSODet fssoDet3 = fssoDet1;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        fssoDet3.ComponentID = nullable3;
        FSSODet fssoDet4 = fssoDet1;
        nullable1 = new int?();
        int? nullable4 = nullable1;
        fssoDet4.EquipmentLineRef = nullable4;
      }
      ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Update(fssoDet1);
    }
    if (e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>, FSServiceOrder, object>) e).OldValue)
      return;
    PXUIFieldAttribute.SetWarning<FSServiceOrder.billCustomerID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>>) e).Cache, (object) e.Row, "Changing the billing customer in the service order will affect all related appointments (if any). This change may update the currency, prices, tax zone, service contract, and project information in the related appointments because these settings originate from the billing customer.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.contactID> e)
  {
    this.FSServiceOrder_ContactID_FieldUpdated_Handler((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.contactID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID> e)
  {
    FSServiceOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID>>) e).Cache;
    this.FSServiceOrder_LocationID_FieldUpdated_Handler(cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID>>) e).Args);
    if (!row.LocationID.HasValue)
      cache.RaiseFieldUpdated<FSServiceOrder.customerID>((object) row, (object) row.CustomerID);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID>>) e).Cache.SetDefaultExt<FSServiceOrder.externalTaxExemptionNumber>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID>>) e).Cache.SetDefaultExt<FSServiceOrder.entityUsageType>((object) row);
    foreach (PXResult<FSSODet> pxResult in ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()))
    {
      FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult);
      bool? manualPrice = fssoDet.ManualPrice;
      bool flag = false;
      if (manualPrice.GetValueOrDefault() == flag & manualPrice.HasValue)
      {
        ((PXSelectBase) this.ServiceOrderDetails).Cache.SetDefaultExt<FSSODet.curyUnitPrice>((object) fssoDet);
        ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Update(fssoDet);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.customerID> e)
  {
    DateTime? itemDateTime = new DateTime?();
    FSServiceOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.customerID>>) e).Cache;
    if (row != null)
      itemDateTime = row.OrderDate;
    this.FSServiceOrder_CustomerID_FieldUpdated_Handler(cache, row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, (PXSelectBase<FSSODet>) this.ServiceOrderDetails, (PXSelectBase<FSAppointmentDet>) null, ((PXSelectBase<FSAppointment>) this.ServiceOrderAppointments).Select(Array.Empty<object>()), (int?) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.customerID>>) e).Args.OldValue, itemDateTime, this.allowCustomerChange, ((PXSelectBase<PX.Objects.AR.Customer>) this.BillCustomer).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchLocationID> e)
  {
    this.FSServiceOrder_BranchLocationID_FieldUpdated_Handler((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchLocationID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, (PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.assignedEmpID> e)
  {
    if (e.Row == null)
      return;
    this.updateSOCstmAssigneeEmpID = ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.assignedEmpID>>) e).ExternalCall;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.curyBillableOrderTotal> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || !(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "PM"))
      return;
    FSServiceOrder row = e.Row;
    Decimal? billableOrderTotal = e.Row.CuryBillableOrderTotal;
    Decimal? curyDiscTot = e.Row.CuryDiscTot;
    Decimal? nullable = billableOrderTotal.HasValue & curyDiscTot.HasValue ? new Decimal?(billableOrderTotal.GetValueOrDefault() - curyDiscTot.GetValueOrDefault()) : new Decimal?();
    row.CuryDocTotal = nullable;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.curyDiscTot> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || !(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "PM"))
      return;
    FSServiceOrder row = e.Row;
    Decimal? billableOrderTotal = e.Row.CuryBillableOrderTotal;
    Decimal? curyDiscTot = e.Row.CuryDiscTot;
    Decimal? nullable = billableOrderTotal.HasValue & curyDiscTot.HasValue ? new Decimal?(billableOrderTotal.GetValueOrDefault() - curyDiscTot.GetValueOrDefault()) : new Decimal?();
    row.CuryDocTotal = nullable;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    using (new PXConnectionScope())
    {
      PXResultset<FSAppointment> source = PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where2<Where<FSAppointment.closed, Equal<True>, Or<FSAppointment.completed, Equal<True>>>, And<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SOID
      });
      row.AppointmentsCompletedCntr = new int?(((IQueryable<PXResult<FSAppointment>>) source).Where<PXResult<FSAppointment>>((Expression<Func<PXResult<FSAppointment>, bool>>) (x => ((FSAppointment) x).Completed == (bool?) true && ((FSAppointment) x).Closed == (bool?) false)).Count<PXResult<FSAppointment>>());
      row.AppointmentsCompletedOrClosedCntr = new int?(((IQueryable<PXResult<FSAppointment>>) source).Count<PXResult<FSAppointment>>());
      this.UpdateServiceOrderUnboundFields(row, (FSAppointment) null, this.DisableServiceOrderUnboundFieldCalc);
      this.ValidateServiceContractDates(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSServiceOrder>>) e).Cache, (object) e.Row, ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrder>>) e).Cache;
    if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null)
    {
      this.SetReadOnly(((PXSelectBase) this.ServiceOrderRecords).Cache, true);
    }
    else
    {
      if (string.IsNullOrEmpty(row.SrvOrdType))
        return;
      int? nullable1 = row.ProjectID;
      if (nullable1.HasValue)
      {
        nullable1 = row.ProjectID;
        int num = 0;
        if (nullable1.GetValueOrDefault() >= num & nullable1.HasValue && ((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject)?.Current == null)
          ((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Current = PXResultset<PX.Objects.CT.Contract>.op_Implicit(((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Select(new object[1]
          {
            (object) row.ProjectID
          }));
      }
      if (((PXSelectBase<FSPostInfo>) this.PostInfoDetails)?.Current == null)
        ((PXSelectBase<FSPostInfo>) this.PostInfoDetails).Current = PXResultset<FSPostInfo>.op_Implicit(((PXSelectBase<FSPostInfo>) this.PostInfoDetails).Select(Array.Empty<object>()));
      this.HideRooms(row);
      this.HideOrShowTabs(row);
      this.HidePrepayments(((PXSelectBase) this.Adjustments).View, cache1, row, (FSAppointment) null, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
      PXCache cacheServiceOrder = cache1;
      FSServiceOrder fsServiceOrderRow = row;
      FSSrvOrdType current1 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
      PX.Objects.CT.Contract current2 = ((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject)?.Current;
      int count = ((PXSelectBase<FSAppointment>) this.ServiceOrderAppointments).Select(Array.Empty<object>()).Count;
      nullable1 = row.LineCntr;
      int valueOrDefault = nullable1.GetValueOrDefault();
      PXCache cache2 = ((PXSelectBase) this.ServiceOrderDetails).Cache;
      PXCache cache3 = ((PXSelectBase) this.ServiceOrderAppointments).Cache;
      PXCache cache4 = ((PXSelectBase) this.ServiceOrderEquipment).Cache;
      PXCache cache5 = ((PXSelectBase) this.ServiceOrderEmployees).Cache;
      PXCache cache6 = ((PXSelectBase) this.ServiceOrder_Contact).Cache;
      PXCache cache7 = ((PXSelectBase) this.ServiceOrder_Address).Cache;
      bool? fromQuickProcess = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.IsCalledFromQuickProcess;
      int num1 = this.allowCustomerChange ? 1 : 0;
      this.FSServiceOrder_RowSelected_PartialHandler((PXGraph) this, cacheServiceOrder, fsServiceOrderRow, (FSAppointment) null, current1, current2, count, valueOrDefault, cache2, cache3, cache4, cache5, cache6, cache7, fromQuickProcess, num1 != 0);
      PXCache cache8 = ((PXSelectBase) this.ServiceOrderDetails).Cache;
      FSSrvOrdType current3 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
      bool? nullable2;
      int num2;
      if (current3 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable2 = current3.PostToSOSIPM;
        num2 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      PXUIFieldAttribute.SetVisible<FSSODet.equipmentAction>(cache8, (object) null, num2 != 0);
      FSSrvOrdType current4 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
      int num3;
      if (current4 == null)
      {
        num3 = 0;
      }
      else
      {
        nullable2 = current4.PostToSOSIPM;
        num3 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      this.SetVisiblePODetFields(((PXSelectBase) this.ServiceOrderDetails).Cache, num3 != 0);
      bool flag1 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.Behavior == "QT";
      PXUIFieldAttribute.SetVisible<FSSODet.acctID>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, !flag1);
      PXUIFieldAttribute.SetVisible<FSSODet.subID>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, !flag1);
      PXUIFieldAttribute.SetVisibility<FSSODet.acctID>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, !flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
      PXUIFieldAttribute.SetVisibility<FSSODet.subID>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, !flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
      bool flag2 = this.ShouldShowMarkForPOFields(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.AllocationSOOrderTypeSelected).Current);
      PXUIFieldAttribute.SetVisible<FSSODet.enablePO>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, flag2);
      PXUIFieldAttribute.SetVisible<FSSODet.pOCreate>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, flag2);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.pOCreate>(((PXSelectBase) this.Splits).Cache, (object) null, flag2);
      if (((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current != null && ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current.BillingType == "STDB")
        PXUIFieldAttribute.SetEnabled<FSServiceOrder.projectID>(cache1, (object) row, false);
      PXUIFieldAttribute.SetVisible<FSPostDet.invoiceReferenceNbr>(((PXSelectBase) this.ServiceOrderPostedIn).Cache, (object) null, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo != "PM");
      PXUIFieldAttribute.SetVisible<FSPostDet.iNPostDocReferenceNbr>(((PXSelectBase) this.ServiceOrderPostedIn).Cache, (object) null, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo == "PM");
      bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.inventory>() && PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
      PXUIFieldAttribute.SetVisibility<FSSODet.comment>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
      PXUIFieldAttribute.SetVisibility<FSSODet.equipmentAction>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
      PXUIFieldAttribute.SetVisibility<FSSODet.newTargetEquipmentLineNbr>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
      FSBillingCycle current5 = ((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current;
      int num4;
      if (current5 == null)
      {
        num4 = 0;
      }
      else
      {
        nullable2 = current5.InvoiceOnlyCompletedServiceOrder;
        num4 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      int num5;
      if (num4 != 0)
      {
        nullable2 = e.Row.Completed;
        bool flag4 = false;
        if (nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue)
        {
          nullable2 = e.Row.Closed;
          bool flag5 = false;
          num5 = nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue ? 1 : 0;
          goto label_25;
        }
      }
      num5 = 0;
label_25:
      ((PXAction) this.invoiceOrder).SetEnabled(num5 == 0);
      this.ShowErrorsOnAppointmentLines();
      if (((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrder>>) e).Cache.GetEnabled<FSServiceOrder.locationID>((object) e.Row) && this.HaveAnyBilledAppointmentsInServiceOrder((PXGraph) this, e.Row.SOID))
        PXUIFieldAttribute.SetEnabled<FSServiceOrder.locationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrder>>) e).Cache, (object) e.Row, false);
      ((PXAction) this.openSource).SetVisible(((PXGraph) this).IsMobile);
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSServiceOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    SharedFunctions.InitializeNote(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSServiceOrder>>) e).Cache, ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSServiceOrder>>) e).Args);
    FSServiceOrder row = e.Row;
    int? soid = row.SOID;
    int num = 0;
    if (!(soid.GetValueOrDefault() < num & soid.HasValue))
      return;
    this.UpdateServiceOrderUnboundFields(row, (FSAppointment) null, this.DisableServiceOrderUnboundFieldCalc);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSServiceOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    if (GraphHelper.RowCast<FSBillHistory>((IEnumerable) PXSelectBase<FSBillHistory, PXViewOf<FSBillHistory>.BasedOn<SelectFromBase<FSBillHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSBillHistory.srvOrdType, Equal<BqlField<FSServiceOrder.srvOrdType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<FSBillHistory.appointmentRefNbr, IBqlString>.IsEqual<BqlField<FSServiceOrder.refNbr, IBqlString>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Where<FSBillHistory>((Func<FSBillHistory, bool>) (x =>
    {
      bool? isChildDocDeleted = x.IsChildDocDeleted;
      bool flag = false;
      return isChildDocDeleted.GetValueOrDefault() == flag & isChildDocDeleted.HasValue;
    })).Any<FSBillHistory>())
    {
      e.Cancel = true;
      throw new PXSetPropertyException<FSServiceOrder.refNbr>("A service order cannot be deleted because it was billed.");
    }
    FSServiceOrder row = e.Row;
    if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.Behavior == "QT" && ((PXSelectBase<RelatedServiceOrder>) this.RelatedServiceOrders).Select(Array.Empty<object>()).Count > 0)
      throw new PXException("The quote is linked to at least one service order. Delete the service orders displayed on the Related Service Orders tab before you delete this quote.");
    int? nullable = !this.ServiceOrderHasAppointment((PXGraph) this, row) ? row.APBillLineCntr : throw new PXException("The service order has at least one related appointment. Delete the appointments first.");
    int num = 0;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue) || !(((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300100")) || ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Ask("This document has at least one line associated with an AP bill. Do you want to delete the document?", (MessageButtons) 1) == 1)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSServiceOrder> e)
  {
    if (e.Row == null || !(e.Row.SourceType == "SD") || string.IsNullOrEmpty(e.Row.SourceDocType) || string.IsNullOrEmpty(e.Row.SourceRefNbr))
      return;
    if (PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.sourceDocType>>>, Where<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.quote>, And<FSServiceOrder.sourceDocType, Equal<Required<FSServiceOrder.sourceDocType>>, And<FSServiceOrder.sourceRefNbr, Equal<Required<FSServiceOrder.sourceRefNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) e.Row.SourceDocType,
      (object) e.Row.SourceRefNbr
    }).Count != 0)
      return;
    ServiceOrderEntry instance = PXGraph.CreateInstance<ServiceOrderEntry>();
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords1 = instance.ServiceOrderRecords;
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords2 = instance.ServiceOrderRecords;
    string sourceRefNbr = e.Row.SourceRefNbr;
    object[] objArray = new object[1]
    {
      (object) e.Row.SourceDocType
    };
    FSServiceOrder fsServiceOrder1;
    FSServiceOrder fsServiceOrder2 = fsServiceOrder1 = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderRecords2).Search<FSServiceOrder.refNbr>((object) sourceRefNbr, objArray));
    ((PXSelectBase<FSServiceOrder>) serviceOrderRecords1).Current = fsServiceOrder1;
    FSServiceOrder fsServiceOrder3 = fsServiceOrder2;
    ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.ServiceOrderDeleted))).FireOn((PXGraph) instance, fsServiceOrder3);
    ((PXSelectBase<FSServiceOrder>) instance.ServiceOrderRecords).Update(fsServiceOrder3);
    instance.SkipTaxCalcAndSave();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSServiceOrder>>) e).Cache;
    if (string.IsNullOrWhiteSpace(e.Row.SrvOrdType))
      GraphHelper.RaiseRowPersistingException<FSServiceOrder.srvOrdType>(cache, (object) row);
    if (ProjectDefaultAttribute.IsProject((PXGraph) this, row.ProjectID))
    {
      foreach (PXResult<FSSODet> pxResult in ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()))
      {
        FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult);
        if (!fssoDet.ProjectTaskID.HasValue && !this.IsInstructionOrComment((object) fssoDet))
          GraphHelper.RaiseRowPersistingException<FSSODet.projectTaskID>(((PXGraph) this).Caches[typeof (FSSODet)], (object) fssoDet);
      }
    }
    this.FSServiceOrder_RowPersisting_Handler((ServiceOrderEntry) cache.Graph, cache, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSServiceOrder>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, (PXSelectBase<FSSODet>) this.ServiceOrderDetails, this.ServiceOrderAppointments, this.GraphAppointmentEntryCaller, this.ForceAppointmentCheckings);
    this.ValidateCustomerBillingCycle(cache, (object) e.Row, e.Row.BillCustomerID, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, false);
    bool flag1 = this.AllowEnableCustomerID(row);
    PXCache pxCache1 = cache;
    FSServiceOrder fsServiceOrder1 = row;
    bool? baccountRequired = row.BAccountRequired;
    bool flag2 = false;
    int num1 = !(baccountRequired.GetValueOrDefault() == flag2 & baccountRequired.HasValue) & flag1 ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<FSServiceOrder.customerID>(pxCache1, (object) fsServiceOrder1, (PXPersistingCheck) num1);
    PXCache pxCache2 = cache;
    FSServiceOrder fsServiceOrder2 = row;
    baccountRequired = row.BAccountRequired;
    bool flag3 = false;
    int num2 = !(baccountRequired.GetValueOrDefault() == flag3 & baccountRequired.HasValue) ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<FSServiceOrder.locationID>(pxCache2, (object) fsServiceOrder2, (PXPersistingCheck) num2);
    if (e.Row == null || e.Operation == 3)
      return;
    this.ValidateDuplicateLineNbr((PXSelectBase<FSSODet>) this.ServiceOrderDetails, (PXSelectBase<FSAppointmentDet>) null);
    this.ValidateLinesLotSerials<FSSODet, FSSODetSplit, FSSODet.lotSerialNbr>(((PXSelectBase) this.ServiceOrderDetails).Cache, GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>())), ((PXSelectBase) this.Splits).View, (object) row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    if (e.TranStatus == 1)
    {
      PXDBOperation operation = e.Operation;
      if (operation != 2)
      {
        if (operation == 3)
        {
          switch (row.SourceType)
          {
            case "CR":
              this.ClearCase(row);
              break;
            case "OP":
              this.ClearOpportunity(row);
              break;
            case "SO":
              this.ClearSalesOrder(row);
              break;
          }
          this.ClearFSDocExpenseReceipts(row.NoteID);
          this.ClearPrepayment(row);
        }
      }
      else if (row.SourceRefNbr != null)
      {
        if (row.SourceType == "OP")
        {
          if (PXSelectBase<CROpportunity, PXSelect<CROpportunity, Where<CROpportunity.opportunityID, Equal<Required<CROpportunity.opportunityID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.SourceRefNbr
          }).Count > 0)
            PXUpdate<Set<FSxCROpportunity.serviceOrderRefNbr, Required<FSxCROpportunity.serviceOrderRefNbr>, Set<FSxCROpportunity.srvOrdType, Required<FSxCROpportunity.srvOrdType>, Set<FSxCROpportunity.branchLocationID, Required<FSxCROpportunity.branchLocationID>, Set<FSxCROpportunity.sDEnabled, True>>>>, CROpportunity, Where<CROpportunity.opportunityID, Equal<Required<CROpportunity.opportunityID>>>>.Update((PXGraph) this, new object[4]
            {
              (object) row.RefNbr,
              (object) row.SrvOrdType,
              (object) row.BranchLocationID,
              (object) row.SourceRefNbr
            });
        }
        else if (row.SourceType == "CR")
        {
          if (PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.caseCD, Equal<Required<CRCase.caseCD>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.SourceRefNbr
          }).Count > 0)
            PXUpdate<Set<FSxCRCase.sDEnabled, True, Set<FSxCRCase.branchLocationID, Required<FSxCRCase.branchLocationID>, Set<FSxCRCase.srvOrdType, Required<FSxCRCase.srvOrdType>, Set<FSxCRCase.serviceOrderRefNbr, Required<FSxCRCase.serviceOrderRefNbr>, Set<FSxCRCase.assignedEmpID, Required<FSxCRCase.assignedEmpID>, Set<FSxCRCase.problemID, Required<FSxCRCase.problemID>>>>>>>, CRCase, Where<CRCase.caseCD, Equal<Required<CRCase.caseCD>>>>.Update((PXGraph) this, new object[6]
            {
              (object) row.BranchLocationID,
              (object) row.SrvOrdType,
              (object) row.RefNbr,
              (object) row.AssignedEmpID,
              (object) row.ProblemID,
              (object) row.SourceRefNbr
            });
        }
        else if (row.SourceType == "SO" && row.RefNbr != null && row.SourceDocType != null && row.SourceRefNbr != null)
          PXUpdate<Set<FSxSOOrder.sDEnabled, True, Set<FSxSOOrder.srvOrdType, Required<FSxSOOrder.srvOrdType>, Set<FSxSOOrder.serviceOrderRefNbr, Required<FSxSOOrder.serviceOrderRefNbr>>>>, PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Update((PXGraph) this, new object[4]
          {
            (object) row.SrvOrdType,
            (object) row.RefNbr,
            (object) row.SourceDocType,
            (object) row.SourceRefNbr
          });
      }
    }
    if (e.TranStatus == 1 && (e.Operation == 2 || e.Operation == 1))
      this.UpdateServiceOrderUnboundFields(row, (FSAppointment) null, this.DisableServiceOrderUnboundFieldCalc);
    ((PXSelectBase<FSSelectorHelper>) this.Helper).Current = this.GetFsSelectorHelperInstance;
    if (e.TranStatus == null)
    {
      if (this.updateContractPeriod)
      {
        int? nullable1 = row.BillContractPeriodID;
        if (nullable1.HasValue)
        {
          int num1 = row.AllowInvoice.GetValueOrDefault() ? 1 : -1;
          FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current;
          if (current != null && current.RecordType == "NRSC")
          {
            ServiceContractEntry instance = PXGraph.CreateInstance<ServiceContractEntry>();
            ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) row.BillServiceContractID, new object[1]
            {
              (object) row.BillCustomerID
            }));
            ((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.contractPeriodID>(((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).Current, (object) row.BillContractPeriodID);
            Decimal? nullable2 = new Decimal?(0M);
            int? nullable3 = new int?(0);
            foreach (FSSODet fssoDet in GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>())).Where<FSSODet>((Func<FSSODet, bool>) (x => x.IsService && x.ContractRelated.GetValueOrDefault() && x.Status != "CC")))
            {
              FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(((IQueryable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Search<FSContractPeriodDet.inventoryID, FSContractPeriodDet.SMequipmentID, FSContractPeriodDet.billingRule>((object) fssoDet.InventoryID, (object) fssoDet.SMEquipmentID, (object) fssoDet.BillingRule, Array.Empty<object>())).FirstOrDefault<PXResult<FSContractPeriodDet>>());
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.Clear();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.ClearQueryCacheObsolete();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).View.Clear();
              ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>());
              if (contractPeriodDet != null)
              {
                Decimal? usedQty = contractPeriodDet.UsedQty;
                Decimal num2 = (Decimal) num1;
                Decimal? coveredQty = fssoDet.CoveredQty;
                Decimal? nullable4 = coveredQty.HasValue ? new Decimal?(num2 * coveredQty.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable5 = usedQty.HasValue & nullable4.HasValue ? new Decimal?(usedQty.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                Decimal num3 = (Decimal) num1;
                nullable4 = fssoDet.ExtraUsageQty;
                Decimal? nullable6 = nullable4.HasValue ? new Decimal?(num3 * nullable4.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable7;
                if (!(nullable5.HasValue & nullable6.HasValue))
                {
                  nullable4 = new Decimal?();
                  nullable7 = nullable4;
                }
                else
                  nullable7 = new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault());
                Decimal? nullable8 = nullable7;
                int? usedTime = contractPeriodDet.UsedTime;
                Decimal num4 = (Decimal) num1;
                nullable5 = fssoDet.CoveredQty;
                Decimal? nullable9;
                if (!nullable5.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable9 = nullable4;
                }
                else
                  nullable9 = new Decimal?(num4 * nullable5.GetValueOrDefault());
                nullable6 = nullable9;
                Decimal num5 = (Decimal) 60;
                int? nullable10 = nullable6.HasValue ? new int?((int) (nullable6.GetValueOrDefault() * num5)) : new int?();
                nullable1 = usedTime.HasValue & nullable10.HasValue ? new int?(usedTime.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new int?();
                Decimal num6 = (Decimal) num1;
                nullable5 = fssoDet.ExtraUsageQty;
                Decimal? nullable11;
                if (!nullable5.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable11 = nullable4;
                }
                else
                  nullable11 = new Decimal?(num6 * nullable5.GetValueOrDefault());
                nullable6 = nullable11;
                Decimal num7 = (Decimal) 60;
                int? nullable12;
                if (!nullable6.HasValue)
                {
                  nullable10 = new int?();
                  nullable12 = nullable10;
                }
                else
                  nullable12 = new int?((int) (nullable6.GetValueOrDefault() * num7));
                int? nullable13 = nullable12;
                int? nullable14;
                if (!(nullable1.HasValue & nullable13.HasValue))
                {
                  nullable10 = new int?();
                  nullable14 = nullable10;
                }
                else
                  nullable14 = new int?(nullable1.GetValueOrDefault() + nullable13.GetValueOrDefault());
                nullable3 = nullable14;
                contractPeriodDet.UsedQty = contractPeriodDet.BillingRule == "FLRA" ? nullable8 : new Decimal?(0M);
                contractPeriodDet.UsedTime = contractPeriodDet.BillingRule == "TIME" ? nullable3 : new int?(0);
              }
              ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Update(contractPeriodDet);
            }
            ((PXAction) instance.Save).Press();
          }
          else if (current != null && current.RecordType == "IRSC")
          {
            RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
            ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) row.BillServiceContractID, new object[1]
            {
              (object) row.BillCustomerID
            }));
            ((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.contractPeriodID>(((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).Current, (object) row.BillContractPeriodID);
            Decimal? nullable15 = new Decimal?(0M);
            int? nullable16 = new int?(0);
            foreach (FSSODet fssoDet in GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>())).Where<FSSODet>((Func<FSSODet, bool>) (x => x.IsService && x.ContractRelated.GetValueOrDefault() && x.Status != "CC")))
            {
              FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(((IQueryable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Search<FSContractPeriodDet.inventoryID, FSContractPeriodDet.SMequipmentID, FSContractPeriodDet.billingRule>((object) fssoDet.InventoryID, (object) fssoDet.SMEquipmentID, (object) fssoDet.BillingRule, Array.Empty<object>())).FirstOrDefault<PXResult<FSContractPeriodDet>>());
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.Clear();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.ClearQueryCacheObsolete();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).View.Clear();
              ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>());
              if (contractPeriodDet != null)
              {
                Decimal? usedQty = contractPeriodDet.UsedQty;
                Decimal num8 = (Decimal) num1;
                Decimal? coveredQty = fssoDet.CoveredQty;
                Decimal? nullable17 = coveredQty.HasValue ? new Decimal?(num8 * coveredQty.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable18 = usedQty.HasValue & nullable17.HasValue ? new Decimal?(usedQty.GetValueOrDefault() + nullable17.GetValueOrDefault()) : new Decimal?();
                Decimal num9 = (Decimal) num1;
                nullable17 = fssoDet.ExtraUsageQty;
                Decimal? nullable19 = nullable17.HasValue ? new Decimal?(num9 * nullable17.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable20;
                if (!(nullable18.HasValue & nullable19.HasValue))
                {
                  nullable17 = new Decimal?();
                  nullable20 = nullable17;
                }
                else
                  nullable20 = new Decimal?(nullable18.GetValueOrDefault() + nullable19.GetValueOrDefault());
                Decimal? nullable21 = nullable20;
                int? usedTime = contractPeriodDet.UsedTime;
                Decimal num10 = (Decimal) num1;
                nullable18 = fssoDet.CoveredQty;
                Decimal? nullable22;
                if (!nullable18.HasValue)
                {
                  nullable17 = new Decimal?();
                  nullable22 = nullable17;
                }
                else
                  nullable22 = new Decimal?(num10 * nullable18.GetValueOrDefault());
                nullable19 = nullable22;
                Decimal num11 = (Decimal) 60;
                int? nullable23 = nullable19.HasValue ? new int?((int) (nullable19.GetValueOrDefault() * num11)) : new int?();
                int? nullable24 = usedTime.HasValue & nullable23.HasValue ? new int?(usedTime.GetValueOrDefault() + nullable23.GetValueOrDefault()) : new int?();
                Decimal num12 = (Decimal) num1;
                nullable18 = fssoDet.ExtraUsageQty;
                Decimal? nullable25;
                if (!nullable18.HasValue)
                {
                  nullable17 = new Decimal?();
                  nullable25 = nullable17;
                }
                else
                  nullable25 = new Decimal?(num12 * nullable18.GetValueOrDefault());
                nullable19 = nullable25;
                Decimal num13 = (Decimal) 60;
                int? nullable26;
                if (!nullable19.HasValue)
                {
                  nullable23 = new int?();
                  nullable26 = nullable23;
                }
                else
                  nullable26 = new int?((int) (nullable19.GetValueOrDefault() * num13));
                nullable1 = nullable26;
                int? nullable27;
                if (!(nullable24.HasValue & nullable1.HasValue))
                {
                  nullable23 = new int?();
                  nullable27 = nullable23;
                }
                else
                  nullable27 = new int?(nullable24.GetValueOrDefault() + nullable1.GetValueOrDefault());
                nullable16 = nullable27;
                contractPeriodDet.UsedQty = contractPeriodDet.BillingRule == "FLRA" ? nullable21 : new Decimal?(0M);
                contractPeriodDet.UsedTime = contractPeriodDet.BillingRule == "TIME" ? nullable16 : new int?(0);
              }
              ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Update(contractPeriodDet);
            }
            ((PXAction) instance.Save).Press();
          }
          ServiceOrderEntry instance1 = PXGraph.CreateInstance<ServiceOrderEntry>();
          foreach (PXResult<FSServiceOrder> pxResult in PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.billCustomerID, Equal<Required<FSServiceOrder.billCustomerID>>, And<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.billContractPeriodID, Equal<Required<FSServiceOrder.billContractPeriodID>>, And<FSServiceOrder.hold, Equal<False>, And<FSServiceOrder.canceled, Equal<False>, And<FSServiceOrder.allowInvoice, Equal<False>, And<FSServiceOrder.sOID, NotEqual<Required<FSServiceOrder.sOID>>>>>>>>>>.Config>.Select((PXGraph) instance1, new object[4]
          {
            (object) row.BillCustomerID,
            (object) row.BillServiceContractID,
            (object) row.BillContractPeriodID,
            (object) row.SOID
          }))
          {
            FSServiceOrder fsServiceOrder = PXResult<FSServiceOrder>.op_Implicit(pxResult);
            ((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fsServiceOrder.RefNbr, new object[1]
            {
              (object) fsServiceOrder.SrvOrdType
            }));
            ((PXSelectBase) instance1.ServiceOrderRecords).Cache.SetDefaultExt<FSServiceOrder.billContractPeriodID>((object) fsServiceOrder);
            ((PXAction) instance1.Save).Press();
          }
        }
      }
      this.updateContractPeriod = false;
      this.InsertDeleteRelatedFixedRateContractBill(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSServiceOrder>>) e).Cache, (object) e.Row, e.Operation, (PXSelectBase<FSBillHistory>) this.InvoiceRecords);
    }
    this.UpdateAssignedEmpIDinSalesOrder(e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.acctID> e)
  {
    this.X_AcctID_FieldDefaulting<FSSODet>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.acctID>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.acctID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated> e)
  {
    FSSODet fsSODetRow = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated>>) e).Cache;
    bool flag = false;
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current == null || this.BillServiceContractPeriodDetail == null)
      return;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    if (current.Quote.GetValueOrDefault())
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated>, FSSODet, object>) e).NewValue = (object) false;
    }
    else
    {
      if (fsSODetRow.IsService)
      {
        string billingMode = this.GetBillingMode(current);
        if (((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current.BillServiceContractID.HasValue)
        {
          int? nullable = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current.BillContractPeriodID;
          if (nullable.HasValue && !(billingMode != "SO"))
          {
            FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Search<FSSODet.inventoryID, FSSODet.SMequipmentID, FSSODet.billingRule, FSSODet.contractRelated>((object) fsSODetRow.InventoryID, (object) fsSODetRow.SMEquipmentID, (object) fsSODetRow.BillingRule, (object) true, Array.Empty<object>()));
            int num;
            if (fssoDet != null)
            {
              nullable = fssoDet.LineNbr;
              int? lineNbr = fsSODetRow.LineNbr;
              num = !(nullable.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable.HasValue == lineNbr.HasValue) ? 1 : 0;
            }
            else
              num = 0;
            flag = num != 0;
            goto label_11;
          }
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated>, FSSODet, object>) e).NewValue = (object) false;
        return;
      }
label_11:
      if (fsSODetRow.IsInventoryItem && this.GetBillingMode(current) != "SO")
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated>, FSSODet, object>) e).NewValue = (object) false;
      else if (((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current?.BillingType != "STDB")
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated>, FSSODet, object>) e).NewValue = (object) false;
      }
      else
      {
        PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated> fieldDefaulting = e;
        int num;
        if (!flag)
          num = ((IQueryable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).Where<PXResult<FSContractPeriodDet>>((Expression<Func<PXResult<FSContractPeriodDet>, bool>>) (x => ((FSContractPeriodDet) x).InventoryID == fsSODetRow.InventoryID && (((FSContractPeriodDet) x).SMEquipmentID == fsSODetRow.SMEquipmentID || ((FSContractPeriodDet) x).SMEquipmentID == new int?()) && ((FSContractPeriodDet) x).BillingRule == fsSODetRow.BillingRule)).Count<PXResult<FSContractPeriodDet>>() == 1 ? 1 : 0;
        else
          num = 0;
        // ISSUE: variable of a boxed type
        __Boxed<bool> local = (ValueType) (bool) num;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.contractRelated>, FSSODet, object>) fieldDefaulting).NewValue = (object) local;
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty> e)
  {
    if (e.Row == null || ((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current == null)
      return;
    FSSODet fsSODetRow = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty>>) e).Cache;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    if (!fsSODetRow.IsService)
      return;
    if (!(this.GetBillingMode(current) != "SO"))
    {
      bool? contractRelated = fsSODetRow.ContractRelated;
      bool flag = false;
      if (!(contractRelated.GetValueOrDefault() == flag & contractRelated.HasValue))
      {
        FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(((IQueryable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).Where<PXResult<FSContractPeriodDet>>((Expression<Func<PXResult<FSContractPeriodDet>, bool>>) (x => ((FSContractPeriodDet) x).InventoryID == fsSODetRow.InventoryID && (((FSContractPeriodDet) x).SMEquipmentID == fsSODetRow.SMEquipmentID || ((FSContractPeriodDet) x).SMEquipmentID == new int?()) && ((FSContractPeriodDet) x).BillingRule == fsSODetRow.BillingRule)).FirstOrDefault<PXResult<FSContractPeriodDet>>());
        if (contractPeriodDet != null)
        {
          if (fsSODetRow.BillingRule == "TIME")
          {
            PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty> fieldDefaulting = e;
            int? remainingTime = contractPeriodDet.RemainingTime;
            int? estimatedDuration = fsSODetRow.EstimatedDuration;
            int? nullable1 = remainingTime.HasValue & estimatedDuration.HasValue ? new int?(remainingTime.GetValueOrDefault() - estimatedDuration.GetValueOrDefault()) : new int?();
            int num = 0;
            Decimal? nullable2;
            if (!(nullable1.GetValueOrDefault() >= num & nullable1.HasValue))
            {
              nullable1 = contractPeriodDet.RemainingTime;
              nullable2 = nullable1.HasValue ? new Decimal?((Decimal) (nullable1.GetValueOrDefault() / 60)) : new Decimal?();
            }
            else
              nullable2 = fsSODetRow.EstimatedQty;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) nullable2;
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty>, FSSODet, object>) fieldDefaulting).NewValue = (object) local;
            return;
          }
          PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty> fieldDefaulting1 = e;
          Decimal? remainingQty = contractPeriodDet.RemainingQty;
          Decimal? estimatedQty = fsSODetRow.EstimatedQty;
          Decimal? nullable = remainingQty.HasValue & estimatedQty.HasValue ? new Decimal?(remainingQty.GetValueOrDefault() - estimatedQty.GetValueOrDefault()) : new Decimal?();
          Decimal num1 = 0M;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local1 = (ValueType) (nullable.GetValueOrDefault() >= num1 & nullable.HasValue ? fsSODetRow.EstimatedQty : contractPeriodDet.RemainingQty);
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty>, FSSODet, object>) fieldDefaulting1).NewValue = (object) local1;
          return;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty>, FSSODet, object>) e).NewValue = (object) 0M;
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.coveredQty>, FSSODet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyExtraUsageUnitPrice> e)
  {
    if (e.Row == null || ((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current == null)
      return;
    FSSODet fsSODetRow = e.Row;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current;
    if (!fsSODetRow.IsService)
      return;
    if (!(this.GetBillingMode(current) != "SO"))
    {
      bool? contractRelated = fsSODetRow.ContractRelated;
      bool flag = false;
      if (!(contractRelated.GetValueOrDefault() == flag & contractRelated.HasValue))
      {
        FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(((IQueryable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).Where<PXResult<FSContractPeriodDet>>((Expression<Func<PXResult<FSContractPeriodDet>, bool>>) (x => ((FSContractPeriodDet) x).InventoryID == fsSODetRow.InventoryID && (((FSContractPeriodDet) x).SMEquipmentID == fsSODetRow.SMEquipmentID || ((FSContractPeriodDet) x).SMEquipmentID == new int?()) && ((FSContractPeriodDet) x).BillingRule == fsSODetRow.BillingRule)).FirstOrDefault<PXResult<FSContractPeriodDet>>());
        if (contractPeriodDet != null)
        {
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyExtraUsageUnitPrice>, FSSODet, object>) e).NewValue = (object) contractPeriodDet.OverageItemPrice;
          return;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyExtraUsageUnitPrice>, FSSODet, object>) e).NewValue = (object) 0M;
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyExtraUsageUnitPrice>, FSSODet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.subID> e)
  {
    this.X_SubID_FieldDefaulting<FSSODet>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.subID>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.subID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.enablePO> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo != "SO")
      return;
    PX.Objects.SO.SOOrderType current = ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.postSOOrderTypeSelected).Current;
    if (current == null)
      return;
    bool? nullable = current.RequireShipping;
    if (nullable.GetValueOrDefault())
    {
      nullable = current.RequireLocation;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = current.RequireAllocation;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.enablePO>, FSSODet, object>) e).NewValue = (object) false;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.enablePO>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.poVendorID> e)
  {
    if (e.Row == null)
      return;
    FSSODet row = e.Row;
    bool? enablePo = row.EnablePO;
    bool flag = false;
    if (!(enablePo.GetValueOrDefault() == flag & enablePo.HasValue) && row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.poVendorID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.poVendorLocationID> e)
  {
    if (e.Row == null)
      return;
    FSSODet row = e.Row;
    bool? enablePo = row.EnablePO;
    bool flag = false;
    if (!(enablePo.GetValueOrDefault() == flag & enablePo.HasValue))
    {
      int? nullable = row.InventoryID;
      if (nullable.HasValue)
      {
        nullable = row.POVendorID;
        if (nullable.HasValue)
          return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.poVendorLocationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitPrice> e)
  {
    if (e.Row == null || e.Row.IsLinkedItem)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitPrice>>) e).Cache;
    FSSODet row = e.Row;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current;
    bool flag = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo == "PM" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.BillingType == "CC";
    if (row.SkipUnitPriceCalc.GetValueOrDefault())
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitPrice>, FSSODet, object>) e).NewValue = (object) row.AlreadyCalculatedUnitPrice;
    else if (!flag)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ExtensionHelper.SelectCurrencyInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyInfoView, current.CuryInfoID);
      this.X_CuryUnitPrice_FieldDefaulting<FSSODet, FSSODet.curyUnitPrice>(cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitPrice>>) e).Args, row.EstimatedQty, current.OrderDate, current, (FSAppointment) null, currencyInfo);
    }
    else
    {
      if (!flag)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitPrice>, FSSODet, object>) e).NewValue = (object) row.CuryUnitCost.GetValueOrDefault();
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitPrice>>) e).Cancel = row.CuryUnitCost.HasValue;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitCost> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitCost>>) e).Cache;
    FSSODet row = e.Row;
    if (string.IsNullOrEmpty(row.UOM) || !row.InventoryID.HasValue)
      return;
    object obj;
    cache.RaiseFieldDefaulting<FSSODet.unitCost>((object) e.Row, ref obj);
    if (obj == null || !((Decimal) obj != 0M))
      return;
    Decimal curyval = INUnitAttribute.ConvertToBase<FSSODet.inventoryID, FSSODet.uOM>(cache, (object) row, (Decimal) obj, INPrecision.NOROUND);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ExtensionHelper.SelectCurrencyInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyInfoView, ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current.CuryInfoID);
    PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitCost>>) e).Cache, currencyInfo.GetCM(), curyval, out curyval);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitCost>, FSSODet, object>) e).NewValue = (object) curyval;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.curyUnitCost>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.uOM> e)
  {
    if (e.Row == null)
      return;
    FSSODet row = e.Row;
    if (!row.IsService && !row.IsInventoryItem)
      return;
    this.X_UOM_FieldDefaulting<FSSODet>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.uOM>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.uOM>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.costCodeID> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.costCodeID>>) e).Cache;
    this.SetCostCodeDefault((IFSSODetBase) e.Row, (int?) ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current?.ProjectID, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.costCodeID>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.unitCost> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsInventoryItem)
    {
      INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<FSSODet.inventoryID>>, And<INItemSite.siteID, Equal<Required<FSSODet.siteID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) e.Row.InventoryID,
        (object) e.Row.SiteID
      }));
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.unitCost>, FSSODet, object>) e).NewValue = (object) (Decimal?) inItemSite?.TranUnitCost;
    }
    else
    {
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, e.Row.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID);
      if (itemCurySettings == null)
        return;
      DateTime? stdCostDate = itemCurySettings.StdCostDate;
      DateTime? orderDate = (DateTime?) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current?.OrderDate;
      if ((stdCostDate.HasValue & orderDate.HasValue ? (stdCostDate.GetValueOrDefault() <= orderDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.unitCost>, FSSODet, object>) e).NewValue = (object) itemCurySettings.StdCost;
      else
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.unitCost>, FSSODet, object>) e).NewValue = (object) itemCurySettings.LastStdCost;
    }
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.siteID> e)
  {
    if (e.Row == null)
      return;
    FSSODet row = e.Row;
    if (this.IsInventoryLine(row.LineType) && row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.siteID>, FSSODet, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.siteID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.status> e)
  {
    if (e.Row == null)
      return;
    if (this.IsOrderDateFieldUpdated)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.status>, FSSODet, object>) e).NewValue = (object) e.Row.Status;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.status>, FSSODet, object>) e).NewValue = (object) this.CalculateLineStatus(e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.isFree> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsCommentInstruction)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.isFree>, FSSODet, object>) e).NewValue = (object) true;
    else if (e.Row.BillingRule == "NONE")
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.isFree>, FSSODet, object>) e).NewValue = (object) true;
    else if (((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current?.BillingType == "FIRB" && (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo != "PM" || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo == "PM" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.BillingType != "CC"))
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.isFree>, FSSODet, object>) e).NewValue = (object) true;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODet, FSSODet.isFree>, FSSODet, object>) e).NewValue = (object) false;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSSODet, FSSODet.billingRule> e)
  {
    this.X_BillingRule_FieldVerifying<FSSODet>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.billingRule>>) e).Cache, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.billingRule>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSSODet, FSSODet.curyUnitPrice> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.BillingRule == "NONE" && e.Row.InventoryID.HasValue)
    {
      e.Row.ManualPrice = new bool?(false);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.curyUnitPrice>, FSSODet, object>) e).NewValue = (object) 0M;
    }
    else
    {
      if (!this.IsManualPriceFlagNeeded(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.curyUnitPrice>>) e).Cache, (IFSSODetBase) e.Row))
        return;
      e.Row.ManualPrice = new bool?(true);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSSODet, FSSODet.curyExtPrice> e)
  {
    if (e.Row == null || !this.IsManualPriceFlagNeeded(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.curyExtPrice>>) e).Cache, (IFSSODetBase) e.Row))
      return;
    e.Row.ManualPrice = new bool?(true);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FSSODet, FSSODet.enablePO> e)
  {
    if (!(bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.enablePO>, FSSODet, object>) e).NewValue && (e.Row.POType != null || e.Row.PONbr != null))
      throw new PXSetPropertyException("You cannot clear the Mark for PO check box for this line because the purchase order for this line has already been created.", (PXErrorLevel) 4);
    int? apptCntr = e.Row.ApptCntr;
    int num = 1;
    if (apptCntr.GetValueOrDefault() > num & apptCntr.HasValue && this.GraphAppointmentEntryCaller != null)
      throw new PXSetPropertyException("You cannot clear the Mark for PO check box for this line because it has already been added to appointments.", (PXErrorLevel) 4);
    this.POCreateVerifyValue(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.enablePO>>) e).Cache, e.Row, (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.enablePO>, FSSODet, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSSODet, FSSODet.curyBillableExtPrice> e)
  {
    if (e.Row == null || !(e.Row.BillingRule == "NONE"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.curyBillableExtPrice>, FSSODet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FSSODet, FSSODet.discPct> e)
  {
    if (e.Row == null || !(e.Row.BillingRule == "NONE"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.discPct>, FSSODet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FSSODet, FSSODet.siteID> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.siteID>>) e).Cache.GetStatus((object) e.Row) != 1 || !(((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300100")))
      return;
    if (PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSAppointmentDet.srvOrdType>, And<FSAppointment.refNbr, Equal<FSAppointmentDet.refNbr>>>>, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<Where<FSAppointment.closed, Equal<True>, Or<FSAppointment.billed, Equal<True>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SODetID
    }).Count > 0)
      throw new PXSetPropertyException("The {0} cannot be changed because there is at least one billed or closed appointment that includes this service order line.", new object[1]
      {
        (object) ((PXFieldState) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.siteID>>) e).Cache.GetStateExt<FSSODet.siteID>((object) e.Row)).DisplayName
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSSODet, FSSODet.siteLocationID> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.siteLocationID>>) e).Cache.GetStatus((object) e.Row) != 1 || !(((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300100")))
      return;
    if (PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSAppointmentDet.srvOrdType>, And<FSAppointment.refNbr, Equal<FSAppointmentDet.refNbr>>>>, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<Where<FSAppointment.closed, Equal<True>, Or<FSAppointment.billed, Equal<True>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SODetID
    }).Count > 0)
      throw new PXSetPropertyException("The {0} cannot be changed because there is at least one billed or closed appointment that includes this service order line.", new object[1]
      {
        (object) ((PXFieldState) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.siteLocationID>>) e).Cache.GetStateExt<FSSODet.siteLocationID>((object) e.Row)).DisplayName
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSSODet, FSSODet.estimatedQty> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.estimatedQty>, FSSODet, object>) e).NewValue == null || !e.Row.IsInventoryItem)
      return;
    FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectGroupBy<FSAppointmentDet, Where<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>, And<FSAppointmentDet.isCanceledNotPerformed, Equal<False>, And<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>>>>, Aggregate<GroupBy<FSAppointmentDet.sODetID, Sum<FSAppointmentDet.effTranQty>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSSODet, FSSODet.estimatedQty>>) e).Cache.Graph, new object[1]
    {
      (object) e.Row.SODetID
    }));
    Decimal num = fsAppointmentDet != null ? fsAppointmentDet.EffTranQty.Value : 0M;
    if ((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.estimatedQty>, FSSODet, object>) e).NewValue < num)
      throw new PXSetPropertyException("The specified estimated quantity {0} is less than the sum of the estimated quantities of all lines {1} in the associated appointment(s).", new object[2]
      {
        (object) this.FormatQty((Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.estimatedQty>, FSSODet, object>) e).NewValue),
        (object) this.FormatQty(new Decimal?(num))
      });
    if (e.Row.EquipmentAction != null && !(e.Row.EquipmentAction == "NO") && Decimal.Remainder((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSSODet, FSSODet.estimatedQty>, FSSODet, object>) e).NewValue, 1M) != 0M)
      throw new PXSetPropertyException("A decimal number cannot be entered as an item quantity if any equipment action is specified in the line. Specify a whole number for the quantity of this item.", (PXErrorLevel) 4);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.estimatedQty> e)
  {
    this.X_Qty_FieldUpdated<FSSODet.curyUnitPrice>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.estimatedQty>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.estimatedQty>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.estimatedDuration> e)
  {
    if (e.Row == null)
      return;
    FSSODet row = e.Row;
    if (!row.IsService)
      return;
    this.X_Duration_FieldUpdated<FSSODet, FSSODet.estimatedQty>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.estimatedDuration>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.estimatedDuration>>) e).Args, row.EstimatedDuration);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.SMequipmentID> e)
  {
    if (e.Row == null)
      return;
    this.UpdateWarrantyFlag(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.SMequipmentID>>) e).Cache, (IFSSODetBase) e.Row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.OrderDate);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.componentID> e)
  {
    if (e.Row == null)
      return;
    this.UpdateWarrantyFlag(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.componentID>>) e).Cache, (IFSSODetBase) e.Row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.OrderDate);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.equipmentLineRef> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.equipmentLineRef>>) e).Cache;
    FSSODet row = e.Row;
    this.UpdateWarrantyFlag(cache, (IFSSODetBase) row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.OrderDate);
    if (row.ComponentID.HasValue)
      return;
    row.ComponentID = SharedFunctions.GetEquipmentComponentID((PXGraph) this, row.SMEquipmentID, row.EquipmentLineRef);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.inventoryID> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.inventoryID>>) e).Cache;
    FSSODet fsSODetRow = e.Row;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current;
    this.X_InventoryID_FieldUpdated<FSSODet, FSSODet.acctID, FSSODet.subItemID, FSSODet.siteID, FSSODet.siteLocationID, FSSODet.uOM, FSSODet.estimatedDuration, FSSODet.estimatedQty, FSSODet.billingRule, ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.fakeField, ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.fakeField>(cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.inventoryID>>) e).Args, current.BranchLocationID, ((PXSelectBase<PX.Objects.AR.Customer>) this.BillCustomer).Current, false);
    if (fsSODetRow.IsInventoryItem)
      SharedFunctions.UpdateEquipmentFields((PXGraph) this, cache, (object) fsSODetRow, fsSODetRow.InventoryID, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
    cache.SetDefaultExt<FSSODet.curyUnitCost>((object) fsSODetRow);
    cache.SetDefaultExt<FSSODet.enablePO>((object) fsSODetRow);
    if (fsSODetRow.IsService && fsSODetRow.ContractRelated.GetValueOrDefault())
    {
      FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(((IQueryable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).Where<PXResult<FSContractPeriodDet>>((Expression<Func<PXResult<FSContractPeriodDet>, bool>>) (x => ((FSContractPeriodDet) x).InventoryID == fsSODetRow.InventoryID && (((FSContractPeriodDet) x).SMEquipmentID == fsSODetRow.SMEquipmentID || ((FSContractPeriodDet) x).SMEquipmentID == new int?()) && ((FSContractPeriodDet) x).BillingRule == fsSODetRow.BillingRule)).FirstOrDefault<PXResult<FSContractPeriodDet>>());
      cache.SetValueExt<FSSODet.projectTaskID>((object) fsSODetRow, (object) contractPeriodDet.ProjectTaskID);
      cache.SetValueExt<FSSODet.costCodeID>((object) fsSODetRow, (object) contractPeriodDet.CostCodeID);
    }
    if (fsSODetRow.IsService)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.inventoryID>>) e).Cache.SetDefaultExt<FSSODet.equipmentAction>((object) e.Row);
    if ((((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.inventoryID>>) e).ExternalCall || !fsSODetRow.InventoryID.HasValue) && this.GraphAppointmentEntryCaller == null)
      return;
    foreach (PXSelectorAttribute selectorAttribute in cache.GetAttributes(typeof (FSSODet.inventoryID).Name).OfType<PXSelectorAttribute>())
      selectorAttribute.ShowPopupMessage = false;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.staffID> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.staffID>>) e).Cache;
    FSSODet row = e.Row;
    if (!row.IsService)
      return;
    if (row.StaffID.HasValue)
    {
      if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.staffID>, FSSODet, object>) e).OldValue != null)
      {
        FSSOEmployee fssoEmployee = PXResultset<FSSOEmployee>.op_Implicit(PXSelectBase<FSSOEmployee, PXSelect<FSSOEmployee, Where<FSSOEmployee.serviceLineRef, Equal<Required<FSSOEmployee.serviceLineRef>>, And<FSSOEmployee.employeeID, Equal<Required<FSSOEmployee.employeeID>>, And<FSSOEmployee.sOID, Equal<Current<FSServiceOrder.sOID>>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.LineRef,
          ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.staffID>, FSSODet, object>) e).OldValue
        }));
        fssoEmployee.EmployeeID = row.StaffID;
        ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Update(fssoEmployee);
      }
      else
      {
        if (((PXGraph) this).IsContractBasedAPI && string.IsNullOrEmpty(row.LineRef))
          cache.RaiseRowInserting((object) row);
        ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Insert(new FSSOEmployee()
        {
          EmployeeID = row.StaffID
        }).ServiceLineRef = row.LineRef;
      }
    }
    else
      ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Delete(PXResultset<FSSOEmployee>.op_Implicit(PXSelectBase<FSSOEmployee, PXSelect<FSSOEmployee, Where<FSSOEmployee.serviceLineRef, Equal<Required<FSSOEmployee.serviceLineRef>>, And<FSSOEmployee.employeeID, Equal<Required<FSSOEmployee.employeeID>>, And<FSSOEmployee.sOID, Equal<Current<FSServiceOrder.sOID>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.LineRef,
        ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.staffID>, FSSODet, object>) e).OldValue
      })));
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.lineType> e)
  {
    this.X_LineType_FieldUpdated<FSSODet>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.lineType>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.lineType>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isPrepaid> e)
  {
    this.X_IsPrepaid_FieldUpdated<FSSODet, FSSODet.manualPrice, FSSODet.isFree, FSSODet.estimatedDuration, ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.fakeField>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isPrepaid>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isPrepaid>>) e).Args, false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.manualPrice> e)
  {
    this.X_ManualPrice_FieldUpdated<FSSODet, FSSODet.curyUnitPrice, FSSODet.curyBillableExtPrice>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.manualPrice>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.manualPrice>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.billingRule> e)
  {
    this.X_BillingRule_FieldUpdated<FSSODet, FSSODet.estimatedDuration, ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.fakeField, FSSODet.curyUnitPrice, FSSODet.isFree>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.billingRule>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.billingRule>>) e).Args, false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.uOM> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.uOM>>) e).Cache;
    this.X_UOM_FieldUpdated<FSSODet.curyUnitPrice>(cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.uOM>>) e).Args);
    cache.SetDefaultExt<FSSODet.curyUnitCost>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.siteID> e)
  {
    this.X_SiteID_FieldUpdated<FSSODet.curyUnitPrice, FSSODet.acctID, FSSODet.subID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.siteID>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.siteID>>) e).Args);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.siteID>>) e).Cache.SetDefaultExt<FSSODet.curyUnitCost>((object) e.Row);
    if (e.NewValue == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.siteID>, FSSODet, object>) e).OldValue || string.IsNullOrEmpty(e.Row.PONbr))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.siteID>>) e).Cache.DisplayFieldWarning<FSSODet.siteID>((object) e.Row, e.NewValue, "Changing the warehouse will not affect the purchase order or the PO receipt.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.equipmentAction> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.equipmentAction>>) e).Cache;
    FSSODet row = e.Row;
    if (!row.IsInventoryItem || !(row.CreatedByScreenID == "FS300100"))
      return;
    SharedFunctions.ResetEquipmentFields(cache, (object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.billableQty> e)
  {
    if (e.Row == null || e.Row.IsLinkedItem)
      return;
    this.X_Qty_FieldUpdated<FSSODet.curyUnitPrice>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.billableQty>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.billableQty>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSODet, FSSODet.contractRelated> e)
  {
    if (e.Row == null)
      return;
    bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.contractRelated>, FSSODet, object>) e).OldValue;
    bool? contractRelated = e.Row.ContractRelated;
    if (oldValue.GetValueOrDefault() == contractRelated.GetValueOrDefault() & oldValue.HasValue == contractRelated.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.contractRelated>>) e).Cache.SetDefaultExt<FSSODet.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.tranDesc> e)
  {
    if (e.Row == null)
      return;
    FSSODet row = e.Row;
    if (string.IsNullOrEmpty(row.TranDesc) || row.LineType != null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.tranDesc>>) e).Cache.SetValueExt<FSSODet.lineType>((object) row, (object) "IT_LN");
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsFree.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree>>) e).Cache.SetValueExt<FSSODet.curyUnitPrice>((object) e.Row, (object) 0M);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree>>) e).Cache.SetValueExt<FSSODet.curyBillableExtPrice>((object) e.Row, (object) 0M);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree>>) e).Cache.SetValueExt<FSSODet.discPct>((object) e.Row, (object) 0M);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree>>) e).Cache.SetValueExt<FSSODet.curyDiscAmt>((object) e.Row, (object) 0M);
      if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree>>) e).ExternalCall)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree>>) e).Cache.SetValueExt<FSSODet.manualDisc>((object) e.Row, (object) true);
    }
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isFree>>) e).Cache.SetDefaultExt<FSSODet.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable> e)
  {
    if (e.Row == null)
      return;
    bool? isBillable = e.Row.IsBillable;
    bool flag = false;
    if (isBillable.GetValueOrDefault() == flag & isBillable.HasValue || e.Row.BillingRule == "NONE")
    {
      if (e.Row.Status == "CC")
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.curyBillableExtPrice>((object) e.Row, (object) 0M);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.discPct>((object) e.Row, (object) 0M);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.curyDiscAmt>((object) e.Row, (object) 0M);
      }
      else
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.isFree>((object) e.Row, (object) true);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.contractRelated>((object) e.Row, (object) false);
    }
    else
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.isFree>((object) e.Row, (object) false);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.manualPrice>((object) e.Row, (object) e.Row.IsExpenseReceiptItem);
      if (!e.Row.IsLinkedItem)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.curyUnitPrice>((object) e.Row, (object) e.Row.CuryUnitCost);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.isBillable>>) e).Cache.SetValueExt<FSSODet.curyBillableExtPrice>((object) e.Row, (object) e.Row.CuryExtCost);
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSSODet, FSSODet.status> e)
  {
    if (e.Row == null)
      return;
    string oldValue = (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSODet, FSSODet.status>, FSSODet, object>) e).OldValue;
    string status = e.Row.Status;
    if (!(oldValue != status) || !(oldValue == "CC") && !(status == "CC"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSODet, FSSODet.status>>) e).Cache.SetDefaultExt<FSSODet.isBillable>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSODet> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSODet>>) e).Cache;
    FSSODet row = e.Row;
    if (this.GraphAppointmentEntryCaller != null)
      return;
    using (new PXConnectionScope())
    {
      if (row.IsService)
      {
        PXResultset<FSSOEmployee> pxResultset = PXSelectBase<FSSOEmployee, PXSelect<FSSOEmployee, Where<FSSOEmployee.sOID, Equal<Required<FSServiceOrder.sOID>>, And<FSSOEmployee.serviceLineRef, Equal<Required<FSSOEmployee.serviceLineRef>>>>>.Config>.Select(cache.Graph, new object[2]
        {
          (object) row.SOID,
          (object) row.LineRef
        });
        if (pxResultset != null)
        {
          row.EnableStaffID = new bool?(pxResultset.Count <= 1);
          row.StaffID = pxResultset.Count != 1 ? new int?() : PXResultset<FSSOEmployee>.op_Implicit(pxResultset).EmployeeID;
        }
        else
        {
          FSSODet fssoDet1 = row;
          int? nullable1 = row.SOID;
          int num = 0;
          bool? nullable2 = new bool?(nullable1.GetValueOrDefault() < num & nullable1.HasValue);
          fssoDet1.EnableStaffID = nullable2;
          FSSODet fssoDet2 = row;
          nullable1 = new int?();
          int? nullable3 = nullable1;
          fssoDet2.StaffID = nullable3;
        }
      }
      this.SetSODetServiceLastReferencedBy(row);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSODet> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSODet>>) e).Cache;
    FSSODet row = e.Row;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current;
    this.EnableDisable_SODetLine((PXGraph) this, cache, row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, current, ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current);
    this.X_RowSelected<FSSODet>(cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSODet>>) e).Args, current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, false, false);
    this.POCreateVerifyValue(cache, row, row.EnablePO);
    FSLineType.SetLineTypeList<FSSODet.lineType>(((PXSelectBase) this.ServiceOrderDetails).Cache, (object) null, this.InventoryItemsAreIncluded(), false, false, true, false);
    bool? nullable;
    if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current != null)
    {
      if (!(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "PM"))
      {
        if (current != null)
        {
          nullable = current.AllowInvoice;
          if (!nullable.GetValueOrDefault())
            goto label_7;
        }
        else
          goto label_7;
      }
      PXUIFieldAttribute.SetEnabled<FSSODet.equipmentAction>(cache, (object) null, false);
    }
label_7:
    bool flag = false;
    List<System.Type> fieldsToIgnore = new List<System.Type>();
    if (row.Status == "CC")
    {
      fieldsToIgnore.Add(typeof (FSSODet.status));
      flag = true;
    }
    if (row.IsLinkedItem)
    {
      fieldsToIgnore.Add(typeof (FSSODet.SMequipmentID));
      fieldsToIgnore.Add(typeof (FSSODet.newTargetEquipmentLineNbr));
      fieldsToIgnore.Add(typeof (FSSODet.componentID));
      fieldsToIgnore.Add(typeof (FSSODet.equipmentLineRef));
      if (ProjectDefaultAttribute.IsNonProject(current.ProjectID))
        fieldsToIgnore.Add(typeof (FSSODet.isBillable));
      nullable = row.IsBillable;
      if (nullable.GetValueOrDefault())
      {
        fieldsToIgnore.Add(typeof (FSSODet.curyUnitPrice));
        fieldsToIgnore.Add(typeof (FSSODet.manualPrice));
        fieldsToIgnore.Add(typeof (FSSODet.manualDisc));
        fieldsToIgnore.Add(typeof (FSSODet.curyBillableExtPrice));
        fieldsToIgnore.Add(typeof (FSSODet.curyExtCost));
        fieldsToIgnore.Add(typeof (FSSODet.discPct));
        fieldsToIgnore.Add(typeof (FSSODet.curyDiscAmt));
        fieldsToIgnore.Add(typeof (FSSODet.isFree));
        fieldsToIgnore.Add(typeof (FSSODet.taxCategoryID));
        fieldsToIgnore.Add(typeof (FSSODet.acctID));
        fieldsToIgnore.Add(typeof (FSSODet.subID));
      }
      flag = true;
    }
    if (!flag)
      return;
    this.DisableAllDACFields(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSODet>>) e).Cache, (object) row, fieldsToIgnore);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSODet> e)
  {
    ServiceOrderHandlers.FSSODet_RowInserting(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSSODet>>) e).Cache, ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSSODet>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSODet> e)
  {
    this.MarkHeaderAsUpdated(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSSODet>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSODet> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<FSSODet>>) e).Cache;
    FSSODet row = e.Row;
    if (row.IsInventoryItem)
      EquipmentHelper.CheckReplaceComponentLines<FSSODet, FSSODet.equipmentLineRef>(cache, ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()), (IFSSODetBase) e.NewRow);
    if (e.NewRow.Status == "CC" && row.EnablePO.GetValueOrDefault() && (!string.IsNullOrEmpty(row.PONbr) || !string.IsNullOrEmpty(row.POType)))
      throw new PXException("The selected line is associated with a purchase order and cannot be canceled directly. To cancel this line, first cancel or delete the corresponding line in the related purchase order.", new object[1]
      {
        (object) row.SourceLineNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSODet> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSODet>>) e).Cache;
    this.MarkHeaderAsUpdated(cache, (object) e.Row);
    if (((PXGraph) this).IsCopyPasteContext && (e.Row.LinkedEntityType == "AP" || e.Row.LinkedEntityType == "ER"))
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSODet>>) e).Cache.Delete((object) e.Row);
    }
    else
    {
      if (!((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.IsINReleaseProcess && (!cache.ObjectsEqual<FSSODet.curyUnitCost>((object) e.Row, (object) e.OldRow) || !cache.ObjectsEqual<FSSODet.curyExtCost>((object) e.Row, (object) e.OldRow)))
      {
        foreach (PXResult<FSSODetSplit> pxResult in ((PXSelectBase<FSSODetSplit>) this.Splits).Select(Array.Empty<object>()))
        {
          FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
          ((PXSelectBase) this.Splits).Cache.SetValueExt<FSSODetSplit.curyUnitCost>((object) fssoDetSplit, (object) e.Row.CuryUnitCost);
          ((PXSelectBase) this.Splits).Cache.SetValueExt<FSSODetSplit.curyExtCost>((object) fssoDetSplit, (object) e.Row.CuryExtCost);
        }
      }
      if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSODet>>) e).Cache.GetStatus((object) e.Row) == 2)
      {
        Decimal? curyUnitCost = e.Row.CuryUnitCost;
        Decimal? curyOrigUnitCost = e.Row.CuryOrigUnitCost;
        if (!(curyUnitCost.GetValueOrDefault() == curyOrigUnitCost.GetValueOrDefault() & curyUnitCost.HasValue == curyOrigUnitCost.HasValue))
        {
          e.Row.CuryOrigUnitCost = e.Row.CuryUnitCost;
          e.Row.OrigUnitCost = e.Row.UnitCost;
        }
      }
      this.CheckIfManualPrice<FSSODet, FSSODet.estimatedQty>(cache, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSODet>>) e).Args);
      this.CheckSOIfManualCost(cache, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSODet>>) e).Args);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSODet> e)
  {
    if (e.Row == null)
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<FSSODet>>) e).Cache;
    FSSODet fsSODetRow = e.Row;
    if (this.FSSODetLinkedToAppointments((PXGraph) this, fsSODetRow))
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("This {0} is linked to at least one appointment. Delete the {0}s in the appointments before you delete the {0}.", new object[1]
      {
        (object) this.GetLineType(fsSODetRow.LineType)
      }), new object[1]{ (object) (PXErrorLevel) 4 });
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current != null && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current.SourceType == "SO" && e.ExternalCall && this.IsThisLineRelatedToAsoLine(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current, fsSODetRow))
      throw new PXException("This line cannot be deleted because it is related to the {0} line of the sales order from which this service order has been created. Delete the line in the source sales order on the Sales Orders (SO301000) form.", new object[1]
      {
        (object) fsSODetRow.SourceLineNbr
      });
    if (fsSODetRow.IsService)
    {
      foreach (FSSOEmployee fssoEmployee in GraphHelper.RowCast<FSSOEmployee>((IEnumerable) ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Select(Array.Empty<object>())).Where<FSSOEmployee>((Func<FSSOEmployee, bool>) (y => y.ServiceLineRef == fsSODetRow.LineRef)))
        ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Delete(fssoEmployee);
    }
    if (fsSODetRow.EnablePO.GetValueOrDefault() && (!string.IsNullOrEmpty(fsSODetRow.PONbr) || !string.IsNullOrEmpty(fsSODetRow.POType)))
      throw new PXException("The line cannot be deleted because the purchase order has already been created for this line.", new object[1]
      {
        (object) fsSODetRow.SourceLineNbr
      });
    if (!e.ExternalCall || !fsSODetRow.IsAPBillItem || ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Ask("This line is associated with an AP bill. Do you want to delete the line?", (MessageButtons) 1) == 1)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSODet> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSSODet>>) e).Cache;
    FSSODet row = e.Row;
    this.MarkHeaderAsUpdated(cache, (object) e.Row);
    this.ClearTaxes(((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSODet> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSSODet>>) e).Cache;
    FSSODet row = e.Row;
    if (row.IsInventoryItem)
    {
      string empty = string.Empty;
      if (e.Operation != 3 && !SharedFunctions.AreEquipmentFieldsValid(cache, row.InventoryID, row.SMEquipmentID, (object) row.NewTargetEquipmentLineNbr, row.EquipmentAction, ref empty))
        cache.RaiseExceptionHandling<FSSODet.equipmentAction>((object) row, (object) row.EquipmentAction, (Exception) new PXSetPropertyException(empty));
      if (this.GetBillingMode(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRecords).Current) == "SO" && this.GetLotSerialClass(row.InventoryID)?.LotSerAssign == "U")
        cache.RaiseExceptionHandling<FSSODet.inventoryID>((object) row, (object) row.InventoryID, (Exception) new PXSetPropertyException((IBqlTable) row, "Items that have a lot or serial class with the When Used assignment method cannot be used with a service document where Billing By is set to Service Orders."));
      if (!EquipmentHelper.CheckReplaceComponentLines<FSSODet, FSSODet.equipmentLineRef>(cache, ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()), (IFSSODetBase) e.Row))
        return;
    }
    string valueOriginal1 = (string) cache.GetValueOriginal<FSSODet.status>((object) row);
    if ((row.Status == "CC" || row.Status == "CP") && row.Status != valueOriginal1 && this.GraphAppointmentEntryCaller == null)
    {
      if (row.Status == "CC")
      {
        if (((IQueryable<PXResult<FSAppointmentDet>>) PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.SODetID
        })).Count<PXResult<FSAppointmentDet>>() != 0)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("The {0} line cannot be canceled because one or multiple related lines are not canceled in the associated appointments. Cancel these lines on the Appointments (FS300200) form first.", new object[1]
          {
            (object) this.GetLineDisplayHint((PXGraph) this, row.LineRef, row.TranDesc, row.InventoryID)
          }));
      }
      if (row.Status == "CP")
      {
        if (((IQueryable<PXResult<FSAppointmentDet>>) PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<Where<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotFinished>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Completed>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.requestForPO>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.SODetID
        })).Count<PXResult<FSAppointmentDet>>() != 0)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("The {0} line cannot be completed because one or multiple related lines have the Not Started, In Progress, or Waiting for Purchased Items status in the associated appointments. Select the appropriate status on the Appointments (FS300200) form first.", new object[1]
          {
            (object) this.GetLineDisplayHint((PXGraph) this, row.LineRef, row.TranDesc, row.InventoryID)
          }));
      }
    }
    this.X_SetPersistingCheck<FSSODet>(cache, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSSODet>>) e).Args, ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
    if (e.Operation != 1)
      return;
    bool? valueOriginal2 = (bool?) ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSSODet>>) e).Cache.GetValueOriginal<FSSODet.enablePO>((object) e.Row);
    string valueOriginal3 = (string) ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSSODet>>) e).Cache.GetValueOriginal<FSSODet.poNbr>((object) e.Row);
    bool? enablePo = e.Row.EnablePO;
    bool? nullable = valueOriginal2;
    if (enablePo.GetValueOrDefault() == nullable.GetValueOrDefault() & enablePo.HasValue == nullable.HasValue && !(e.Row.PONbr != valueOriginal3) || this.GraphAppointmentEntryCaller != null)
      return;
    this.UpdatePOOptionsInAppointmentRelatedLines(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSODet> e)
  {
    if (e.Row == null)
      return;
    FSSODet row = e.Row;
    if (((PXGraph) this).Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("EP301020") && ((PXGraph) this).Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("EP301000") && e.TranStatus == 1 && e.Operation == 3 && ((PXSelectBase) this.ServiceOrderRecords).Cache.GetStatus((object) ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current) != 3 && row.IsExpenseReceiptItem)
      this.ClearFSDocExpenseReceipts(row.LinkedDocRefNbr);
    if (!(((PXGraph) this).Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("AP301000")) || e.TranStatus != 1 || e.Operation != 3 || !row.IsAPBillItem)
      return;
    this.ClearFSDocReferenceInAPDoc(e.Row.LinkedDocType, e.Row.LinkedDocRefNbr, e.Row.LinkedLineNbr);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSOEmployee, FSSOEmployee.employeeID> e)
  {
    if (e.Row == null)
      return;
    FSSOEmployee row = e.Row;
    row.Type = SharedFunctions.GetBAccountType((PXGraph) this, row.EmployeeID);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSOEmployee> e)
  {
    if (e.Row == null)
      return;
    FSSOEmployee row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSOEmployee>>) e).Cache;
    FSSOEmployee fssoEmployee1 = row;
    int? employeeId = row.EmployeeID;
    int num1 = !employeeId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSOEmployee.employeeID>(cache, (object) fssoEmployee1, num1 != 0);
    FSSOEmployee fssoEmployee2 = row;
    employeeId = row.EmployeeID;
    int num2 = employeeId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSOEmployee.comment>(cache, (object) fssoEmployee2, num2 != 0);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSOEmployee> e)
  {
    if (e.Row == null)
      return;
    FSSOEmployee row = e.Row;
    if (row.LineRef != null)
      return;
    row.LineRef = row.LineNbr.Value.ToString("000");
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSOEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSOResource> e)
  {
    if (e.Row == null)
      return;
    FSSOResource row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSOResource>>) e).Cache;
    PXUIFieldAttribute.SetEnabled<FSSOResource.SMequipmentID>(cache, (object) row, !row.SMEquipmentID.HasValue);
    FSSOResource fssoResource1 = row;
    int? smEquipmentId = row.SMEquipmentID;
    int num1 = smEquipmentId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSOResource.qty>(cache, (object) fssoResource1, num1 != 0);
    FSSOResource fssoResource2 = row;
    smEquipmentId = row.SMEquipmentID;
    int num2 = smEquipmentId.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSOResource.comment>(cache, (object) fssoResource2, num2 != 0);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSOResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSPostDet> e)
  {
    FSPostDet row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSPostDet>>) e).Cache;
    if (row.SOPosted.GetValueOrDefault())
    {
      using (new PXConnectionScope())
      {
        PX.Objects.SO.SOOrderShipment soOrderShipment = PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelectReadonly<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>>>>.Config>.Select(cache.Graph, new object[2]
        {
          (object) row.SOOrderNbr,
          (object) row.SOOrderType
        }));
        row.InvoiceRefNbr = soOrderShipment?.InvoiceNbr;
        row.InvoiceDocType = soOrderShipment?.InvoiceType;
      }
    }
    else
    {
      if (!row.ARPosted.GetValueOrDefault() && !row.SOInvPosted.GetValueOrDefault())
        return;
      row.InvoiceRefNbr = row.Mem_DocNbr;
      row.InvoiceDocType = row.ARDocType;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSPostDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<ARPayment> e)
  {
    if (e.Row == null)
      return;
    ARPayment row = e.Row;
    using (new PXConnectionScope())
      this.RecalcSOApplAmounts((PXGraph) this, row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ARPayment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSBillHistory> e)
  {
    if (e.Row == null)
      return;
    FSBillHistory row = e.Row;
    this.CalculateBillHistoryUnboundFields(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSBillHistory>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSBillHistory> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSBillHistory> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.curyExtCost> e)
  {
    if (e.Row == null)
      return;
    FSSODetSplit row = e.Row;
    PX.Data.Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.curyExtCost> fieldDefaulting = e;
    Decimal? curyUnitCost = row.CuryUnitCost;
    Decimal? qty = row.Qty;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (curyUnitCost.HasValue & qty.HasValue ? new Decimal?(curyUnitCost.GetValueOrDefault() * qty.GetValueOrDefault()) : new Decimal?());
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.curyExtCost>, FSSODetSplit, object>) fieldDefaulting).NewValue = (object) local;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.curyExtCost>>) e).Cancel = true;
  }

  protected virtual void MarkHeaderAsUpdated(PXCache cache, object row)
  {
    if (row == null || ((PXSelectBase) this.CurrentServiceOrder).Cache.GetStatus((object) ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current) != null)
      return;
    ((PXSelectBase) this.CurrentServiceOrder).Cache.SetStatus((object) ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current, (PXEntryStatus) 1);
  }

  public virtual Decimal GetCuryDocTotal(
    Decimal? curyLineTotal,
    Decimal? curyDiscTotal,
    Decimal? curyTaxTotal,
    Decimal? curyInclTaxTotal)
  {
    return curyLineTotal.GetValueOrDefault() - curyDiscTotal.GetValueOrDefault() + curyTaxTotal.GetValueOrDefault() - curyInclTaxTotal.GetValueOrDefault();
  }

  public virtual void POCreateVerifyValue(PXCache sender, FSSODet row, bool? value)
  {
    ServiceOrderEntry.POCreateVerifyValueInt<FSSODet.enablePO>(sender, (object) row, row.InventoryID, value);
  }

  public static void POCreateVerifyValueInt<POCreateField>(
    PXCache sender,
    object row,
    int? inventoryID,
    bool? value)
    where POCreateField : IBqlField
  {
    if (row == null || !inventoryID.HasValue || !value.GetValueOrDefault())
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
    if (inventoryItem == null || inventoryItem.StkItem.GetValueOrDefault())
      return;
    if (inventoryItem.KitItem.GetValueOrDefault())
    {
      sender.RaiseExceptionHandling<POCreateField>(row, (object) value, (Exception) new PXSetPropertyException("Non-Stock kit items cannot be linked with purchase order.", (PXErrorLevel) 4));
    }
    else
    {
      if (inventoryItem.NonStockShip.GetValueOrDefault() && inventoryItem.NonStockReceipt.GetValueOrDefault())
        return;
      sender.RaiseExceptionHandling<POCreateField>(row, (object) value, (Exception) new PXSetPropertyException("Require Ship/Receipt is OFF in the Non-Stock settings.", (PXErrorLevel) 2));
    }
  }

  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Ref. Nbr.")]
  [FSSelectorServiceOrderSODetID]
  protected virtual void StaffSelectionFilter_ServiceLineRef_CacheAttached(PXCache sender)
  {
  }

  public virtual IEnumerable skillGridFilter()
  {
    return StaffSelectionHelper.SkillFilterDelegate<FSSODet>((PXGraph) this, (PXSelectBase<FSSODet>) this.ServiceOrderDetails, this.StaffSelectorFilter, (PXSelectBase<PX.Objects.FS.SkillGridFilter>) this.SkillGridFilter);
  }

  public virtual IEnumerable licenseTypeGridFilter()
  {
    return StaffSelectionHelper.LicenseTypeFilterDelegate<FSSODet>((PXGraph) this, (PXSelectBase<FSSODet>) this.ServiceOrderDetails, this.StaffSelectorFilter, (PXSelectBase<PX.Objects.FS.LicenseTypeGridFilter>) this.LicenseTypeGridFilter);
  }

  protected virtual IEnumerable staffRecords()
  {
    return StaffSelectionHelper.StaffRecordsDelegate((object) this.ServiceOrderEmployees, (PXSelectBase<PX.Objects.FS.SkillGridFilter>) this.SkillGridFilter, (PXSelectBase<PX.Objects.FS.LicenseTypeGridFilter>) this.LicenseTypeGridFilter, this.StaffSelectorFilter);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<StaffSelectionFilter, StaffSelectionFilter.serviceLineRef> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase) this.SkillGridFilter).Cache.Clear();
    ((PXSelectBase) this.LicenseTypeGridFilter).Cache.Clear();
    ((PXSelectBase) this.StaffRecords).Cache.Clear();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<BAccountStaffMember> e)
  {
    BAccountStaffMember row = e.Row;
    if (((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current != null)
    {
      if (row.Selected.GetValueOrDefault())
      {
        if (((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current != null)
        {
          if (((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current.LineRef != ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef)
            ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Search<FSSODet.lineRef>((object) ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef, Array.Empty<object>()));
          if (!GraphHelper.RowCast<FSSOEmployee>((IEnumerable) ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Select(Array.Empty<object>())).Where<FSSOEmployee>((Func<FSSOEmployee, bool>) (_ => _.ServiceLineRef == ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef)).Any<FSSOEmployee>() && ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current.IsService)
            ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current.StaffID = row.BAccountID;
          else
            ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current.StaffID = new int?();
        }
        ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Insert(new FSSOEmployee()
        {
          EmployeeID = row.BAccountID,
          ServiceLineRef = ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef
        });
      }
      else
      {
        FSSOEmployee fssoEmployee = (FSSOEmployee) ((PXSelectBase) this.ServiceOrderEmployees).Cache.Locate((object) PXResultset<FSSOEmployee>.op_Implicit(PXSelectBase<FSSOEmployee, PXSelect<FSSOEmployee, Where2<Where<FSSOEmployee.serviceLineRef, Equal<Required<FSSOEmployee.serviceLineRef>>, Or<Where<FSSOEmployee.serviceLineRef, IsNull, And<Required<FSSOEmployee.serviceLineRef>, IsNull>>>>, And<Where<FSSOEmployee.sOID, Equal<Current<FSServiceOrder.sOID>>, And<FSSOEmployee.employeeID, Equal<Required<FSSOEmployee.employeeID>>>>>>>.Config>.Select(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<BAccountStaffMember>>) e).Cache.Graph, new object[3]
        {
          (object) ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef,
          (object) ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef,
          (object) row.BAccountID
        })));
        if (fssoEmployee != null)
          ((PXSelectBase<FSSOEmployee>) this.ServiceOrderEmployees).Delete(fssoEmployee);
      }
    }
    ((PXSelectBase) this.StaffRecords).View.RequestRefresh();
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenStaffSelectorFromServiceTab()
  {
    ((PXSelectBase<FSContact>) this.ServiceOrder_Contact).Current = ((PXSelectBase<FSContact>) this.ServiceOrder_Contact).SelectSingle(Array.Empty<object>());
    ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).SelectSingle(Array.Empty<object>());
    if (((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current != null)
    {
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.PostalCode = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current.PostalCode;
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ProjectID = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current.ProjectID;
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ScheduledDateTimeBegin = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current.SLAETA;
    }
    FSSODet current = ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current;
    if (current != null && current.IsService)
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef = current.LineRef;
    else
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef = (string) null;
    ((PXSelectBase) this.SkillGridFilter).Cache.Clear();
    ((PXSelectBase) this.LicenseTypeGridFilter).Cache.Clear();
    ((PXSelectBase) this.StaffRecords).Cache.Clear();
    new StaffSelectionHelper().LaunchStaffSelector((PXGraph) this, this.StaffSelectorFilter);
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenStaffSelectorFromStaffTab()
  {
    ((PXSelectBase<FSContact>) this.ServiceOrder_Contact).Current = ((PXSelectBase<FSContact>) this.ServiceOrder_Contact).SelectSingle(Array.Empty<object>());
    ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).SelectSingle(Array.Empty<object>());
    if (((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current != null)
    {
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.PostalCode = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current.PostalCode;
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ProjectID = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current.ProjectID;
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ScheduledDateTimeBegin = ((PXSelectBase<FSServiceOrder>) this.CurrentServiceOrder).Current.SLAETA;
    }
    ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef = (string) null;
    ((PXSelectBase) this.SkillGridFilter).Cache.Clear();
    ((PXSelectBase) this.LicenseTypeGridFilter).Cache.Clear();
    ((PXSelectBase) this.StaffRecords).Cache.Clear();
    new StaffSelectionHelper().LaunchStaffSelector((PXGraph) this, this.StaffSelectorFilter);
  }

  public FSServiceOrderLineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<FSServiceOrderLineSplittingExtension>();
  }

  public FSServiceOrderLineSplittingAllocatedExtension LineSplittingAllocatedExt
  {
    get => ((PXGraph) this).FindImplementation<FSServiceOrderLineSplittingAllocatedExtension>();
  }

  public ServiceOrderEntry.ServiceOrderQuickProcess ServiceOrderQuickProcessExt
  {
    get => ((PXGraph) this).GetExtension<ServiceOrderEntry.ServiceOrderQuickProcess>();
  }

  public override bool InventoryItemsAreIncluded()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return false;
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    return current != null && current.AllowInventoryItems;
  }

  public class ServiceOrderEmployees_View : 
    PXSelectJoin<FSSOEmployee, LeftJoin<PX.Objects.CR.BAccount, On<FSSOEmployee.employeeID, Equal<PX.Objects.CR.BAccount.bAccountID>>, LeftJoin<FSSODetEmployee, On<FSSODetEmployee.lineRef, Equal<FSSOEmployee.serviceLineRef>, And<FSSODetEmployee.sOID, Equal<FSSOEmployee.sOID>>>>>, Where<FSSOEmployee.sOID, Equal<Current<FSServiceOrder.sOID>>>, OrderBy<Asc<FSSOEmployee.lineRef>>>
  {
    public ServiceOrderEmployees_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceOrderEmployees_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ServiceOrderQuickProcess : PXGraphExtension<ServiceOrderEntry>
  {
    public PXSelect<SOOrderTypeQuickProcess, Where<SOOrderTypeQuickProcess.orderType, Equal<Current<FSSrvOrdType.postOrderType>>>> currentSOOrderType;
    public static bool isSOInvoice;
    public PXQuickProcess.Action<FSServiceOrder>.ConfiguredBy<FSSrvOrdQuickProcessParams> quickProcess;
    public PXAction<FSServiceOrder> quickProcessOk;
    public PXFilter<FSSrvOrdQuickProcessParams> QuickProcessParameters;

    public static bool IsActive() => true;

    [PXButton(CommitChanges = true)]
    [PXUIField(DisplayName = "Quick Process")]
    protected virtual IEnumerable QuickProcess(PXAdapter adapter)
    {
      // ISSUE: method pointer
      ((PXSelectBase<FSSrvOrdQuickProcessParams>) this.QuickProcessParameters).AskExt(new PXView.InitializePanel((object) null, __methodptr(InitQuickProcessPanel)));
      if (((PXSelectBase) this.Base.ServiceOrderRecords).AllowUpdate)
        this.Base.SkipTaxCalcAndSave();
      PXQuickProcess.Start<ServiceOrderEntry, FSServiceOrder, FSSrvOrdQuickProcessParams>(this.Base, ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRecords).Current, ((PXSelectBase<FSSrvOrdQuickProcessParams>) this.QuickProcessParameters).Current);
      return (IEnumerable) new FSServiceOrder[1]
      {
        ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRecords).Current
      };
    }

    [PXButton]
    [PXUIField(DisplayName = "OK")]
    public virtual IEnumerable QuickProcessOk(PXAdapter adapter)
    {
      ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRecords).Current.IsCalledFromQuickProcess = new bool?(true);
      return adapter.Get();
    }

    protected virtual void _(PX.Data.Events.RowSelected<FSServiceOrder> e)
    {
      if (e.Row == null)
        return;
      if (((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Current == null)
        ((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Current = PXResultset<SOOrderTypeQuickProcess>.op_Implicit(((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Select(Array.Empty<object>()));
      ServiceOrderEntry.ServiceOrderQuickProcess.isSOInvoice = ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current?.PostTo == "SI";
      PXQuickProcess.Action<FSServiceOrder>.ConfiguredBy<FSSrvOrdQuickProcessParams> quickProcess = this.quickProcess;
      FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current;
      int num = current != null ? (current.AllowQuickProcess.GetValueOrDefault() ? 1 : 0) : 0;
      ((PXAction) quickProcess).SetEnabled(num != 0);
    }

    protected virtual void _(PX.Data.Events.RowSelected<FSSrvOrdQuickProcessParams> e)
    {
      if (e.Row == null)
        return;
      PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSrvOrdQuickProcessParams>>) e).Cache;
      ((PXAction) this.quickProcessOk).SetEnabled(true);
      FSSrvOrdQuickProcessParams row = e.Row;
      this.SetQuickProcessSettingsVisibility(cache, ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRecords).Current, row);
      bool? fromServiceOrder = row.GenerateInvoiceFromServiceOrder;
      if (fromServiceOrder.GetValueOrDefault() && ServiceOrderEntry.ServiceOrderQuickProcess.isSOInvoice)
      {
        PXUIFieldAttribute.SetEnabled<FSSrvOrdQuickProcessParams.releaseInvoice>(cache, (object) row, true);
        PXUIFieldAttribute.SetEnabled<FSSrvOrdQuickProcessParams.emailInvoice>(cache, (object) row, true);
      }
      else
      {
        fromServiceOrder = row.GenerateInvoiceFromServiceOrder;
        bool flag = false;
        if (!(fromServiceOrder.GetValueOrDefault() == flag & fromServiceOrder.HasValue) || !ServiceOrderEntry.ServiceOrderQuickProcess.isSOInvoice)
          return;
        PXUIFieldAttribute.SetEnabled<FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder>(cache, (object) row, true);
        PXUIFieldAttribute.SetEnabled<FSSrvOrdQuickProcessParams.releaseInvoice>(cache, (object) row, false);
        PXUIFieldAttribute.SetEnabled<FSSrvOrdQuickProcessParams.emailInvoice>(cache, (object) row, false);
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<FSSrvOrdQuickProcessParams, FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder> e)
    {
      if (e.Row == null)
        return;
      FSSrvOrdQuickProcessParams row = e.Row;
      if (!ServiceOrderEntry.ServiceOrderQuickProcess.isSOInvoice)
        return;
      bool? fromServiceOrder = row.GenerateInvoiceFromServiceOrder;
      if (!fromServiceOrder.GetValueOrDefault())
        return;
      fromServiceOrder = row.GenerateInvoiceFromServiceOrder;
      bool oldValue = (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSrvOrdQuickProcessParams, FSSrvOrdQuickProcessParams.generateInvoiceFromServiceOrder>, FSSrvOrdQuickProcessParams, object>) e).OldValue;
      if (fromServiceOrder.GetValueOrDefault() == oldValue & fromServiceOrder.HasValue)
        return;
      row.PrepareInvoice = new bool?(false);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<FSSrvOrdQuickProcessParams, FSSrvOrdQuickProcessParams.sOQuickProcess> e)
    {
      if (e.Row == null)
        return;
      FSSrvOrdQuickProcessParams row = e.Row;
      bool? soQuickProcess = row.SOQuickProcess;
      bool oldValue = (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSrvOrdQuickProcessParams, FSSrvOrdQuickProcessParams.sOQuickProcess>, FSSrvOrdQuickProcessParams, object>) e).OldValue;
      if (soQuickProcess.GetValueOrDefault() == oldValue & soQuickProcess.HasValue)
        return;
      ServiceOrderEntry.ServiceOrderQuickProcess.SetQuickProcessOptions((PXGraph) this.Base, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSrvOrdQuickProcessParams, FSSrvOrdQuickProcessParams.sOQuickProcess>>) e).Cache, row, true);
    }

    private void SetQuickProcessSettingsVisibility(
      PXCache cache,
      FSSrvOrdType fsSrvOrdTypeRow,
      FSServiceOrder fsServiceOrderRow,
      FSSrvOrdQuickProcessParams fsQuickProcessParametersRow)
    {
      if (fsSrvOrdTypeRow == null || fsServiceOrderRow == null)
        return;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = fsSrvOrdTypeRow.PostTo == "SO";
      bool flag4 = fsSrvOrdTypeRow.PostTo == "SI";
      if (flag3)
      {
        SOOrderTypeQuickProcess current = ((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Current;
        if ((current != null ? (current.AllowQuickProcess.HasValue ? 1 : 0) : 0) != 0)
        {
          flag1 = ((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Current.Behavior == "IN";
          flag2 = ((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Current.AllowQuickProcess.Value;
          goto label_7;
        }
      }
      if (flag4)
        flag1 = true;
label_7:
      bool? nullable;
      int num1;
      if (flag2)
      {
        nullable = fsQuickProcessParametersRow.GenerateInvoiceFromServiceOrder;
        if (nullable.GetValueOrDefault())
        {
          nullable = fsQuickProcessParametersRow.PrepareInvoice;
          bool flag5 = false;
          if (!(nullable.GetValueOrDefault() == flag5 & nullable.HasValue))
          {
            nullable = fsQuickProcessParametersRow.SOQuickProcess;
            num1 = nullable.GetValueOrDefault() ? 1 : 0;
            goto label_13;
          }
          num1 = 1;
          goto label_13;
        }
      }
      num1 = 0;
label_13:
      bool flag6 = num1 != 0;
      PXUIFieldAttribute.SetVisible<FSSrvOrdQuickProcessParams.sOQuickProcess>(cache, (object) fsQuickProcessParametersRow, flag3 & flag2);
      PXUIFieldAttribute.SetVisible<FSSrvOrdQuickProcessParams.emailSalesOrder>(cache, (object) fsQuickProcessParametersRow, flag3);
      PXCache pxCache1 = cache;
      FSSrvOrdQuickProcessParams quickProcessParams1 = fsQuickProcessParametersRow;
      int num2;
      if (flag3 & flag1)
      {
        nullable = fsQuickProcessParametersRow.SOQuickProcess;
        bool flag7 = false;
        num2 = nullable.GetValueOrDefault() == flag7 & nullable.HasValue ? 1 : 0;
      }
      else
        num2 = 0;
      PXUIFieldAttribute.SetVisible<FSSrvOrdQuickProcessParams.prepareInvoice>(pxCache1, (object) quickProcessParams1, num2 != 0);
      PXCache pxCache2 = cache;
      FSSrvOrdQuickProcessParams quickProcessParams2 = fsQuickProcessParametersRow;
      int num3;
      if ((flag3 | flag4) & flag1)
      {
        nullable = fsQuickProcessParametersRow.SOQuickProcess;
        bool flag8 = false;
        num3 = nullable.GetValueOrDefault() == flag8 & nullable.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
      PXUIFieldAttribute.SetVisible<FSSrvOrdQuickProcessParams.releaseInvoice>(pxCache2, (object) quickProcessParams2, num3 != 0);
      PXCache pxCache3 = cache;
      FSSrvOrdQuickProcessParams quickProcessParams3 = fsQuickProcessParametersRow;
      int num4;
      if ((flag3 | flag4) & flag1)
      {
        nullable = fsQuickProcessParametersRow.SOQuickProcess;
        bool flag9 = false;
        num4 = nullable.GetValueOrDefault() == flag9 & nullable.HasValue ? 1 : 0;
      }
      else
        num4 = 0;
      PXUIFieldAttribute.SetVisible<FSSrvOrdQuickProcessParams.emailInvoice>(pxCache3, (object) quickProcessParams3, num4 != 0);
      PXCache pxCache4 = cache;
      FSSrvOrdQuickProcessParams quickProcessParams4 = fsQuickProcessParametersRow;
      int num5;
      if (flag3)
      {
        nullable = fsServiceOrderRow.AllowInvoice;
        bool flag10 = false;
        num5 = nullable.GetValueOrDefault() == flag10 & nullable.HasValue ? 1 : 0;
      }
      else
        num5 = 0;
      PXUIFieldAttribute.SetVisible<FSSrvOrdQuickProcessParams.allowInvoiceServiceOrder>(pxCache4, (object) quickProcessParams4, num5 != 0);
      PXUIFieldAttribute.SetEnabled<FSSrvOrdQuickProcessParams.sOQuickProcess>(cache, (object) fsQuickProcessParametersRow, flag6);
      nullable = fsQuickProcessParametersRow.ReleaseInvoice;
      bool flag11 = false;
      if (!(nullable.GetValueOrDefault() == flag11 & nullable.HasValue))
        return;
      nullable = fsQuickProcessParametersRow.EmailInvoice;
      bool flag12 = false;
      if (!(nullable.GetValueOrDefault() == flag12 & nullable.HasValue))
        return;
      nullable = fsQuickProcessParametersRow.SOQuickProcess;
      bool flag13 = false;
      if (!(nullable.GetValueOrDefault() == flag13 & nullable.HasValue))
        return;
      nullable = fsQuickProcessParametersRow.GenerateInvoiceFromServiceOrder;
      if (!nullable.GetValueOrDefault())
        return;
      PXUIFieldAttribute.SetEnabled<FSSrvOrdQuickProcessParams.prepareInvoice>(cache, (object) fsQuickProcessParametersRow, true);
    }

    private static string[] GetExcludedFields()
    {
      return new string[7]
      {
        SharedFunctions.GetFieldName<FSQuickProcessParameters.allowInvoiceServiceOrder>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.completeServiceOrder>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.closeServiceOrder>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.generateInvoiceFromServiceOrder>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.sOQuickProcess>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.emailSalesOrder>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.srvOrdType>()
      };
    }

    private static void SetQuickProcessOptions(
      PXGraph graph,
      PXCache targetCache,
      FSSrvOrdQuickProcessParams fsSrvOrdQuickProcessParamsRow,
      bool ignoreUpdateSOQuickProcess)
    {
      ServiceOrderEntry.ServiceOrderQuickProcess orderQuickProcessExt = ((ServiceOrderEntry) graph).ServiceOrderQuickProcessExt;
      if (string.IsNullOrEmpty(((PXSelectBase<FSSrvOrdQuickProcessParams>) orderQuickProcessExt.QuickProcessParameters).Current.OrderType))
      {
        ((PXSelectBase) orderQuickProcessExt.QuickProcessParameters).Cache.Clear();
        ServiceOrderEntry.ServiceOrderQuickProcess.ResetSalesOrderQuickProcessValues(((PXSelectBase<FSSrvOrdQuickProcessParams>) orderQuickProcessExt.QuickProcessParameters).Current);
      }
      if (fsSrvOrdQuickProcessParamsRow != null)
        ServiceOrderEntry.ServiceOrderQuickProcess.ResetSalesOrderQuickProcessValues(fsSrvOrdQuickProcessParamsRow);
      FSQuickProcessParameters FSQuickProcessParametersRowSource = PXResultset<FSQuickProcessParameters>.op_Implicit(PXSelectBase<FSQuickProcessParameters, PXSelectReadonly<FSQuickProcessParameters, Where<FSQuickProcessParameters.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>>.Config>.Select((PXGraph) orderQuickProcessExt.Base, Array.Empty<object>()));
      bool? nullable;
      int num1;
      if (fsSrvOrdQuickProcessParamsRow != null)
      {
        nullable = fsSrvOrdQuickProcessParamsRow.SOQuickProcess;
        if (nullable.GetValueOrDefault())
        {
          num1 = 1;
          goto label_12;
        }
      }
      if (fsSrvOrdQuickProcessParamsRow == null)
      {
        if (FSQuickProcessParametersRowSource == null)
        {
          num1 = 0;
        }
        else
        {
          nullable = FSQuickProcessParametersRowSource.SOQuickProcess;
          num1 = nullable.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num1 = 0;
label_12:
      bool flag = num1 != 0;
      PXCache pxCache = targetCache ?? ((PXSelectBase) orderQuickProcessExt.QuickProcessParameters).Cache;
      FSSrvOrdQuickProcessParams quickProcessParams = fsSrvOrdQuickProcessParamsRow ?? ((PXSelectBase<FSSrvOrdQuickProcessParams>) orderQuickProcessExt.QuickProcessParameters).Current;
      SOOrderTypeQuickProcess current = ((PXSelectBase<SOOrderTypeQuickProcess>) orderQuickProcessExt.currentSOOrderType).Current;
      int num2;
      if (current == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current.AllowQuickProcess;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      int num3 = flag ? 1 : 0;
      if ((num2 & num3) != 0)
      {
        PXCache<FSSrvOrdQuickProcessParams> cacheSource = new PXCache<FSSrvOrdQuickProcessParams>((PXGraph) orderQuickProcessExt.Base);
        FSSrvOrdQuickProcessParams rowSource = PXResultset<FSSrvOrdQuickProcessParams>.op_Implicit(PXSelectBase<FSSrvOrdQuickProcessParams, PXSelectReadonly<FSSrvOrdQuickProcessParams, Where<FSSrvOrdQuickProcessParams.orderType, Equal<Current<FSSrvOrdType.postOrderType>>>>.Config>.Select((PXGraph) orderQuickProcessExt.Base, Array.Empty<object>()));
        SharedFunctions.CopyCommonFields(pxCache, (IBqlTable) quickProcessParams, (PXCache) cacheSource, (IBqlTable) rowSource, ServiceOrderEntry.ServiceOrderQuickProcess.GetExcludedFields());
        nullable = quickProcessParams.CreateShipment;
        if (nullable.GetValueOrDefault())
        {
          orderQuickProcessExt.EnsureSiteID(pxCache, quickProcessParams);
          DateTime? shipDate = orderQuickProcessExt.Base.GetShipDate(((PXSelectBase<FSServiceOrder>) orderQuickProcessExt.Base.ServiceOrderRecords).Current);
          SOQuickProcessParametersShipDateExt.SetDate(pxCache, (SOQuickProcessParameters) quickProcessParams, shipDate.Value);
        }
      }
      else
        ServiceOrderEntry.ServiceOrderQuickProcess.SetCommonValues(quickProcessParams, FSQuickProcessParametersRowSource);
      if (ignoreUpdateSOQuickProcess)
        return;
      ServiceOrderEntry.ServiceOrderQuickProcess.SetServiceOrderTypeValues(quickProcessParams, FSQuickProcessParametersRowSource);
    }

    public static void InitQuickProcessPanel(PXGraph graph, string viewName)
    {
      ServiceOrderEntry.ServiceOrderQuickProcess.SetQuickProcessOptions(graph, (PXCache) null, (FSSrvOrdQuickProcessParams) null, false);
    }

    private static void ResetSalesOrderQuickProcessValues(
      FSSrvOrdQuickProcessParams fsSrvOrdQuickProcessParamsRow)
    {
      fsSrvOrdQuickProcessParamsRow.CreateShipment = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.ConfirmShipment = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.UpdateIN = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.PrepareInvoiceFromShipment = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.PrepareInvoice = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.EmailInvoice = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.ReleaseInvoice = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.AutoRedirect = new bool?(false);
      fsSrvOrdQuickProcessParamsRow.AutoDownloadReports = new bool?(false);
    }

    private static void SetCommonValues(
      FSSrvOrdQuickProcessParams fsSrvOrdQuickProcessParamsRowTarget,
      FSQuickProcessParameters FSQuickProcessParametersRowSource)
    {
      bool? nullable1;
      if (ServiceOrderEntry.ServiceOrderQuickProcess.isSOInvoice && fsSrvOrdQuickProcessParamsRowTarget.GenerateInvoiceFromServiceOrder.GetValueOrDefault())
      {
        fsSrvOrdQuickProcessParamsRowTarget.PrepareInvoice = new bool?(false);
      }
      else
      {
        FSSrvOrdQuickProcessParams quickProcessParams = fsSrvOrdQuickProcessParamsRowTarget;
        nullable1 = FSQuickProcessParametersRowSource.GenerateInvoiceFromAppointment;
        bool? nullable2 = nullable1.Value ? FSQuickProcessParametersRowSource.PrepareInvoice : new bool?(false);
        quickProcessParams.PrepareInvoice = nullable2;
      }
      FSSrvOrdQuickProcessParams quickProcessParams1 = fsSrvOrdQuickProcessParamsRowTarget;
      nullable1 = FSQuickProcessParametersRowSource.GenerateInvoiceFromServiceOrder;
      bool? nullable3 = nullable1.Value ? FSQuickProcessParametersRowSource.ReleaseInvoice : new bool?(false);
      quickProcessParams1.ReleaseInvoice = nullable3;
      FSSrvOrdQuickProcessParams quickProcessParams2 = fsSrvOrdQuickProcessParamsRowTarget;
      nullable1 = FSQuickProcessParametersRowSource.GenerateInvoiceFromServiceOrder;
      bool? nullable4 = nullable1.Value ? FSQuickProcessParametersRowSource.EmailInvoice : new bool?(false);
      quickProcessParams2.EmailInvoice = nullable4;
    }

    private static void SetServiceOrderTypeValues(
      FSSrvOrdQuickProcessParams fsSrvOrdQuickProcessParamsRowTarget,
      FSQuickProcessParameters FSQuickProcessParametersRowSource)
    {
      fsSrvOrdQuickProcessParamsRowTarget.AllowInvoiceServiceOrder = FSQuickProcessParametersRowSource.AllowInvoiceServiceOrder;
      fsSrvOrdQuickProcessParamsRowTarget.CompleteServiceOrder = FSQuickProcessParametersRowSource.CompleteServiceOrder;
      fsSrvOrdQuickProcessParamsRowTarget.CloseServiceOrder = FSQuickProcessParametersRowSource.CloseServiceOrder;
      fsSrvOrdQuickProcessParamsRowTarget.GenerateInvoiceFromServiceOrder = FSQuickProcessParametersRowSource.GenerateInvoiceFromServiceOrder;
      fsSrvOrdQuickProcessParamsRowTarget.SrvOrdType = FSQuickProcessParametersRowSource.SrvOrdType;
      fsSrvOrdQuickProcessParamsRowTarget.SOQuickProcess = FSQuickProcessParametersRowSource.SOQuickProcess;
      fsSrvOrdQuickProcessParamsRowTarget.EmailSalesOrder = FSQuickProcessParametersRowSource.GenerateInvoiceFromServiceOrder.Value ? FSQuickProcessParametersRowSource.EmailSalesOrder : new bool?(false);
    }

    protected virtual void EnsureSiteID(PXCache sender, FSSrvOrdQuickProcessParams row)
    {
      if (row.SiteID.HasValue)
        return;
      int? preferedSiteId = this.Base.GetPreferedSiteID();
      if (!preferedSiteId.HasValue)
        return;
      sender.SetValueExt<FSSrvOrdQuickProcessParams.siteID>((object) row, (object) preferedSiteId);
    }
  }

  public class MultiCurrency : SMMultiCurrencyGraph<ServiceOrderEntry, FSServiceOrder>
  {
    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[4]
      {
        (PXSelectBase) this.Base.ServiceOrderRecords,
        (PXSelectBase) this.Base.ServiceOrderDetails,
        (PXSelectBase) this.Base.TaxLines,
        (PXSelectBase) this.Base.Taxes
      };
    }

    protected override MultiCurrencyGraph<ServiceOrderEntry, FSServiceOrder>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ServiceOrderEntry, FSServiceOrder>.DocumentMapping(typeof (FSServiceOrder))
      {
        BAccountID = typeof (FSServiceOrder.billCustomerID),
        DocumentDate = typeof (FSServiceOrder.orderDate)
      };
    }
  }

  public class SalesTax : TaxGraph<ServiceOrderEntry, FSServiceOrder>
  {
    protected override bool CalcGrossOnDocumentLevel
    {
      get => true;
      set => base.CalcGrossOnDocumentLevel = value;
    }

    protected override PXView DocumentDetailsView
    {
      get => ((PXSelectBase) this.Base.ServiceOrderDetails).View;
    }

    protected override TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.DocumentMapping GetDocumentMapping()
    {
      return new TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.DocumentMapping(typeof (FSServiceOrder))
      {
        DocumentDate = typeof (FSServiceOrder.orderDate),
        CuryDocBal = typeof (FSServiceOrder.curyDocTotal),
        CuryDiscountLineTotal = typeof (FSServiceOrder.curyLineDocDiscountTotal),
        CuryDiscTot = typeof (FSServiceOrder.curyDiscTot),
        BranchID = typeof (FSServiceOrder.branchID),
        FinPeriodID = typeof (FSServiceOrder.finPeriodID),
        TaxZoneID = typeof (FSServiceOrder.taxZoneID),
        CuryLinetotal = typeof (FSServiceOrder.curyBillableOrderTotal),
        CuryTaxTotal = typeof (FSServiceOrder.curyTaxTotal),
        TaxCalcMode = typeof (FSServiceOrder.taxCalcMode)
      };
    }

    protected override TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.DetailMapping GetDetailMapping()
    {
      return new TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.DetailMapping(typeof (FSSODet))
      {
        CuryTranAmt = typeof (FSSODet.curyBillableTranAmt),
        TaxCategoryID = typeof (FSSODet.taxCategoryID),
        DocumentDiscountRate = typeof (FSSODet.documentDiscountRate),
        GroupDiscountRate = typeof (FSSODet.groupDiscountRate),
        CuryTranDiscount = typeof (FSSODet.curyDiscAmt),
        CuryTranExtPrice = typeof (FSSODet.curyBillableExtPrice),
        Qty = typeof (FSSODet.billableQty)
      };
    }

    protected override TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.TaxDetailMapping GetTaxDetailMapping()
    {
      return new TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.TaxDetailMapping(typeof (FSServiceOrderTax), typeof (FSServiceOrderTax.taxID));
    }

    protected override TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.TaxTotalMapping GetTaxTotalMapping()
    {
      return new TaxBaseGraph<ServiceOrderEntry, FSServiceOrder>.TaxTotalMapping(typeof (FSServiceOrderTaxTran), typeof (FSServiceOrderTaxTran.taxID));
    }

    protected virtual void Document_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (e.Row == null)
        return;
      PX.Objects.Extensions.SalesTax.Document extension = sender.GetExtension<PX.Objects.Extensions.SalesTax.Document>(e.Row);
      if (!extension.TaxCalc.HasValue)
        extension.TaxCalc = new TaxCalc?(TaxCalc.Calc);
      if (((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current.PostTo == "PM")
        extension.TaxCalc = new TaxCalc?(TaxCalc.NoCalc);
      Decimal num = (Decimal) (this.ParentGetValue<FSServiceOrder.curyBillableOrderTotal>() ?? (object) 0M);
      ((FSServiceOrder) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.SalesTax.Document>(((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current)).CuryEstimatedBillableTotal = new Decimal?(num);
    }

    public void CalcTaxes() => this.CalcTaxes((object) null);

    protected override void CalcDocTotals(
      object row,
      Decimal CuryTaxTotal,
      Decimal CuryInclTaxTotal,
      Decimal CuryWhTaxTotal)
    {
      if (this.Base.SkipTaxCalcTotals)
        return;
      base.CalcDocTotals(row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal);
      FSServiceOrder main = (FSServiceOrder) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.SalesTax.Document>(((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current);
      Decimal curyDocTotal = this.Base.GetCuryDocTotal(new Decimal?((Decimal) (this.ParentGetValue<FSServiceOrder.curyBillableOrderTotal>() ?? (object) 0M)), new Decimal?((Decimal) (this.ParentGetValue<FSServiceOrder.curyDiscTot>() ?? (object) 0M)), new Decimal?(CuryTaxTotal), new Decimal?(CuryInclTaxTotal));
      if (object.Equals((object) curyDocTotal, (object) (Decimal) (this.ParentGetValue<FSServiceOrder.curyDocTotal>() ?? (object) 0M)))
        return;
      this.ParentSetValue<FSServiceOrder.curyDocTotal>((object) curyDocTotal);
    }

    protected override string GetExtCostLabel(PXCache sender, object row)
    {
      return ((PXFieldState) sender.GetValueExt<FSSODet.curyBillableExtPrice>(row)).DisplayName;
    }

    protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
    {
      if (!(child is PXResult<Detail> pxResult))
        return;
      ((FSSODet) PXResult.Unwrap<Detail>((object) pxResult).Base).CuryBillableExtPrice = value;
      sender.Update((object) pxResult);
    }

    protected override List<object> SelectTaxes<Where>(
      PXGraph graph,
      object row,
      PXTaxCheck taxchk,
      params object[] parameters)
    {
      IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
      ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxComparer", (string) null);
      Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> dictionary = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
      object[] objArray = new object[2]
      {
        row == null || !(row is Detail) ? (object) null : ((PXSelectBase) this.Details).Cache.GetMain<Detail>((Detail) row),
        (object) ((PXSelectBase<FSServiceOrder>) ((ServiceOrderEntry) graph).CurrentServiceOrder).Current
      };
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Current<FSServiceOrder.orderDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
        dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = pxResult;
      List<object> objectList = new List<object>();
      switch (taxchk)
      {
        case PXTaxCheck.Line:
          foreach (PXResult<FSServiceOrderTax> pxResult1 in PXSelectBase<FSServiceOrderTax, PXSelect<FSServiceOrderTax, Where<FSServiceOrderTax.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSServiceOrderTax.refNbr, Equal<Current<FSServiceOrder.refNbr>>, And<FSServiceOrderTax.lineNbr, Equal<Current<FSSODet.lineNbr>>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            FSServiceOrderTax fsServiceOrderTax = PXResult<FSServiceOrderTax>.op_Implicit(pxResult1);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult2;
            if (dictionary.TryGetValue(fsServiceOrderTax.TaxID, out pxResult2))
            {
              int count = objectList.Count;
              while (count > 0 && calculationLevelComparer.Compare(PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2)) > 0)
                --count;
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2));
              objectList.Insert(count, (object) new PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>(fsServiceOrderTax, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2)));
            }
          }
          return objectList;
        case PXTaxCheck.RecalcLine:
          foreach (PXResult<FSServiceOrderTax> pxResult3 in PXSelectBase<FSServiceOrderTax, PXSelect<FSServiceOrderTax, Where<FSServiceOrderTax.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSServiceOrderTax.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            FSServiceOrderTax fsServiceOrderTax = PXResult<FSServiceOrderTax>.op_Implicit(pxResult3);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult4;
            if (dictionary.TryGetValue(fsServiceOrderTax.TaxID, out pxResult4))
            {
              int count;
              for (count = objectList.Count; count > 0; --count)
              {
                int? lineNbr1 = PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]).LineNbr;
                int? lineNbr2 = fsServiceOrderTax.LineNbr;
                if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || calculationLevelComparer.Compare(PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4)) <= 0)
                  break;
              }
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4));
              objectList.Insert(count, (object) new PXResult<FSServiceOrderTax, PX.Objects.TX.Tax, TaxRev>(fsServiceOrderTax, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4)));
            }
          }
          return objectList;
        case PXTaxCheck.RecalcTotals:
          foreach (PXResult<FSServiceOrderTaxTran> pxResult5 in PXSelectBase<FSServiceOrderTaxTran, PXSelect<FSServiceOrderTaxTran, Where<FSServiceOrderTaxTran.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSServiceOrderTaxTran.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>, OrderBy<Asc<FSServiceOrderTaxTran.srvOrdType, Asc<FSServiceOrderTaxTran.refNbr, Asc<FSServiceOrderTaxTran.taxID>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            FSServiceOrderTaxTran serviceOrderTaxTran = PXResult<FSServiceOrderTaxTran>.op_Implicit(pxResult5);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult6;
            if (serviceOrderTaxTran.TaxID != null && dictionary.TryGetValue(serviceOrderTaxTran.TaxID, out pxResult6))
            {
              int count = objectList.Count;
              while (count > 0 && calculationLevelComparer.Compare(PXResult<FSServiceOrderTaxTran, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSServiceOrderTaxTran, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6)) > 0)
                --count;
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6));
              objectList.Insert(count, (object) new PXResult<FSServiceOrderTaxTran, PX.Objects.TX.Tax, TaxRev>(serviceOrderTaxTran, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6)));
            }
          }
          return objectList;
        default:
          return objectList;
      }
    }

    protected override List<object> SelectDocumentLines(PXGraph graph, object row)
    {
      return GraphHelper.RowCast<FSSODet>((IEnumerable) PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSSODet.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
      {
        row
      }, Array.Empty<object>())).Select<FSSODet, object>((Func<FSSODet, object>) (_ => (object) _)).ToList<object>();
    }

    protected virtual void _(PX.Data.Events.RowSelected<FSServiceOrderTaxTran> e)
    {
      if (e.Row == null)
        return;
      PXUIFieldAttribute.SetEnabled<FSServiceOrderTaxTran.taxID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrderTaxTran>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrderTaxTran>>) e).Cache.GetStatus((object) e.Row) == 2);
    }

    protected virtual void _(PX.Data.Events.RowPersisting<FSServiceOrderTaxTran> e)
    {
      FSServiceOrderTaxTran row = e.Row;
      if (row == null)
        return;
      if (e.Operation == 3)
      {
        FSServiceOrderTax fsServiceOrderTax = (FSServiceOrderTax) ((PXSelectBase) this.Base.TaxLines).Cache.Locate((object) this.FindFSServiceOrderTax(row));
        if (((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) fsServiceOrderTax) == 3 || ((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) fsServiceOrderTax) == 4)
          e.Cancel = true;
      }
      if (e.Operation != 1 || ((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) (FSServiceOrderTax) ((PXSelectBase) this.Base.TaxLines).Cache.Locate((object) this.FindFSServiceOrderTax(row))) != 1)
        return;
      e.Cancel = true;
    }

    public virtual FSServiceOrderTax FindFSServiceOrderTax(FSServiceOrderTaxTran tran)
    {
      return GraphHelper.RowCast<FSServiceOrderTax>((IEnumerable) PXSelectBase<FSServiceOrderTax, PXSelect<FSServiceOrderTax, Where<FSServiceOrderTax.srvOrdType, Equal<Required<FSServiceOrderTax.srvOrdType>>, And<FSServiceOrderTax.refNbr, Equal<Required<FSServiceOrderTax.refNbr>>, And<FSServiceOrderTax.lineNbr, Equal<Required<FSServiceOrderTax.lineNbr>>, And<FSServiceOrderTax.taxID, Equal<Required<FSServiceOrderTax.taxID>>>>>>>.Config>.SelectSingleBound(new PXGraph(), new object[0], Array.Empty<object>())).FirstOrDefault<FSServiceOrderTax>();
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<FSServiceOrder, FSServiceOrder.taxZoneID> e)
    {
      if (e.Row == null)
        return;
      FSServiceOrder row = e.Row;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceOrder, FSServiceOrder.taxZoneID>, FSServiceOrder, object>) e).NewValue = (object) this.Base.GetDefaultTaxZone(row.BillCustomerID, row.BillLocationID, row.BranchID, row.ProjectID);
    }

    protected virtual void _(PX.Data.Events.RowUpdated<FSAddress> e)
    {
      this.Base.FSAddress_RowUpdated_Handler(e, ((PXSelectBase) this.Base.ServiceOrderRecords).Cache);
    }
  }

  /// <summary>
  /// A per-unit tax graph extension for which will forbid edit of per-unit taxes in UI.
  /// </summary>
  public class PerUnitTaxDisableExt : 
    PerUnitTaxDataEntryGraphExtension<ServiceOrderEntry, FSServiceOrderTaxTran>
  {
    public static bool IsActive()
    {
      return PerUnitTaxDataEntryGraphExtension<ServiceOrderEntry, FSServiceOrderTaxTran>.IsActiveBase();
    }
  }

  public class ContactAddress : SrvOrdContactAddressGraph<ServiceOrderEntry>
  {
  }

  public class ExtensionSorting : Module
  {
    private static readonly Dictionary<System.Type, int> _order = new Dictionary<System.Type, int>()
    {
      {
        typeof (ServiceOrderEntry.ContactAddress),
        1
      },
      {
        typeof (ServiceOrderEntry.MultiCurrency),
        2
      },
      {
        typeof (ServiceOrderEntry.SalesTax),
        5
      }
    };

    protected virtual void Load(ContainerBuilder builder)
    {
      ApplicationStartActivation.RunOnApplicationStart(builder, (System.Action) (() => PXBuildManager.SortExtensions += (Action<List<System.Type>>) (list => PXBuildManager.PartialSort(list, ServiceOrderEntry.ExtensionSorting._order))), (string) null);
    }
  }

  /// <exclude />
  public class ServiceOrderEntryAddressLookupExtension : 
    AddressLookupExtension<ServiceOrderEntry, FSServiceOrder, FSAddress>
  {
    protected override string AddressView => "ServiceOrder_Address";

    protected override string ViewOnMap => "viewDirectionOnMap";
  }
}
