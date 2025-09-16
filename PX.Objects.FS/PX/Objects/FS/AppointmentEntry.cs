// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using Autofac;
using PX.Api.Export;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.FS;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
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
using PX.Reports;
using PX.Reports.Data;
using PX.Reports.Mail;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.FS;

public class AppointmentEntry : 
  ServiceOrderBase<
  #nullable disable
  AppointmentEntry, FSAppointment>,
  IGraphWithInitialization
{
  public bool IsCloningAppointment;
  public bool NeedRecalculateRouteStats;
  public bool CalculateGoogleStats = true;
  public bool AvoidCalculateRouteStats;
  public bool SkipServiceOrderUpdate;
  public bool SkipTimeCardUpdate;
  public string UpdateSOStatusOnAppointmentDeleting = string.Empty;
  public bool IsGeneratingAppointment;
  public bool SkipChangingContract;
  public bool SkipManualTimeFlagUpdate;
  public bool SkipCallSOAction;
  public bool DisableServiceOrderUnboundFieldCalc;
  public string UncloseDialogMessage = "The current appointment will be unclosed. Do you want to proceed?";
  protected bool RetakeGeoLocation;
  public bool SkipLongOperation;
  public bool SkipLotSerialFieldVerifying;
  public bool SkipEarningTypeCheck;
  protected bool serviceOrderIsAlreadyUpdated;
  protected Exception CatchedServiceOrderUpdateException;
  protected bool serviceOrderRowPersistedPassedWithStatusAbort;
  protected bool insertingServiceOrder;
  protected bool persistContext;
  public bool recalculateCuryID;
  private FSSelectorHelper fsSelectorHelper;
  private ServiceOrderEntry _ServiceOrderEntryGraph;
  private EmployeeActivitiesEntry _EmployeeActivitiesEntryGraph;
  protected Dictionary<object, object> _oldRows;
  protected bool updateContractPeriod;
  private Dictionary<FSAppointmentDet, FSSODet> _ApptLinesWithSrvOrdLineUpdated;
  private Dictionary<FSApptLineSplit, FSSODetSplit> _ApptSplitsWithSrvOrdSplitUpdated;
  public PXInitializeState<FSAppointment> initializeState;
  public PXAction<FSAppointment> putOnHold;
  public PXAction<FSAppointment> releaseFromHold;
  public PXAction<FSAppointment> startTravel;
  public PXAction<FSAppointment> completeTravel;
  public PXAction<FSAppointment> viewDirectionOnMap;
  public PXAction<FSAppointment> viewStartGPSOnMap;
  public PXAction<FSAppointment> viewCompleteGPSOnMap;
  public PXAction<FSAppointment> editAppointment;
  public PXAction<FSAppointment> cloneAppointment;
  public PXAction<FSAppointment> report;
  public PXAction<FSAppointment> validateAddress;
  public PXAction<FSAppointment> startAppointment;
  public PXAction<FSAppointment> pauseAppointment;
  public PXAction<FSAppointment> resumeAppointment;
  public PXAction<FSAppointment> cancelAppointment;
  public PXAction<FSAppointment> reopenAppointment;
  public PXAction<FSAppointment> completeAppointment;
  public PXAction<FSAppointment> closeAppointment;
  public PXAction<FSAppointment> uncloseAppointment;
  public PXAction<FSAppointment> invoiceAppointment;
  public PXAction<FSAppointment> openEmployeeBoard;
  public PXAction<FSAppointment> openRoomBoard;
  public PXAction<FSAppointment> openUserCalendar;
  public PXAction<FSAppointment> openSourceDocument;
  public PXAction<FSAppointment> createNewCustomer;
  public PXAction<FSAppointment> emailConfirmationToStaffMember;
  public PXAction<FSAppointment> emailConfirmationToCustomer;
  public PXAction<FSAppointment> emailConfirmationToGeoZoneStaff;
  public PXAction<FSAppointment> emailSignedAppointment;
  public PXAction<FSAppointment> OpenPostingDocument;
  public PXAction<FSAppointment> OpenBillDocument;
  public PXAction<FSAppointment> printAppointmentReport;
  public PXAction<FSAppointment> printServiceTimeActivityReport;
  public PXAction<FSAppointment> startItemLine;
  public PXAction<FSAppointment> pauseItemLine;
  public PXAction<FSAppointment> resumeItemLine;
  public PXAction<FSAppointment> completeItemLine;
  public PXAction<FSAppointment> cancelItemLine;
  public PXAction<FSAppointment> startStaff;
  public PXAction<FSAppointment> pauseStaff;
  public PXAction<FSAppointment> resumeStaff;
  public PXAction<FSAppointment> completeStaff;
  public PXAction<FSAppointment> startAssignedStaff;
  public PXAction<FSAppointment> departStaff;
  public PXAction<FSAppointment> arriveStaff;
  public PXAction<FSAppointment> viewPayment;
  public PXAction<FSAppointment> createPrepayment;
  public PXAction<FSAppointment> quickProcessMobile;
  public PXAction<FSAppointment> openScheduleScreen;
  public ViewLinkedDoc<FSAppointment, FSAppointmentDet> viewLinkedDoc;
  public PXAction<FSAppointment> addReceipt;
  public PXAction<FSAppointment> addBill;
  public PXAction<FSAppointment> createPurchaseOrder;
  public PXAction<FSAppointment> createPurchaseOrderMobile;
  public PXAction<FSAppointment> billReversal;
  public PXAction<FSAppointment> addNewContact;
  public ViewPostBatch<FSAppointment> openPostBatch;
  [PXHidden]
  public PXSelect<FSRouteSetup> RouteSetupRecord;
  [PXHidden]
  public PXSelect<FSSODet> FSSODets;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXFilter<FSLogActionFilter> LogActionFilter;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<INLotSerialStatusByCostCenter> dummyLotSerStatusByCostCenter;
  [PXCopyPasteHiddenView]
  public PXFilter<FSLogActionStartFilter> LogActionStartFilter;
  [PXCopyPasteHiddenView]
  public PXFilter<FSLogActionPCRFilter> LogActionPCRFilter;
  [PXCopyPasteHiddenView]
  public PXFilter<FSLogActionStartServiceFilter> LogActionStartServiceFilter;
  [PXCopyPasteHiddenView]
  public PXFilter<FSLogActionStartStaffFilter> LogActionStartStaffFilter;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (FSAppointment.soRefNbr)})]
  public PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> AppointmentRecords;
  [PXViewName("Appointment")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (FSAppointment.soRefNbr), typeof (FSAppointment.fullNameSignature), typeof (FSAppointment.CustomerSignaturePath), typeof (FSAppointment.serviceContractID), typeof (FSAppointment.scheduleID), typeof (FSAppointment.logLineCntr)})]
  public ServiceOrderBase<AppointmentEntry, FSAppointment>.AppointmentSelected_View AppointmentSelected;
  [PXHidden]
  public PXSelect<FSSODet, Where<FSSODet.sOID, Equal<Current<FSServiceOrder.sOID>>>> ServiceOrderDetails;
  [PXHidden]
  public PXSelect<FSPostInfo, Where<FSPostInfo.appointmentID, Equal<Current<FSAppointment.appointmentID>>>> PostInfoDetails;
  [PXViewName("Service Order Type")]
  public PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>> ServiceOrderTypeSelected;
  [PXHidden]
  public PXSetup<PX.Objects.SO.SOOrderType>.Where<Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.postOrderType>>>> postSOOrderTypeSelected;
  [PXHidden]
  public PXSetup<PX.Objects.SO.SOOrderType>.Where<Where<PX.Objects.SO.SOOrderType.orderType, Equal<Current<FSSrvOrdType.allocationOrderType>>>> AllocationSOOrderTypeSelected;
  [PXHidden]
  public PXSetup<FSBranchLocation>.Where<Where<FSBranchLocation.branchLocationID, Equal<Current<FSServiceOrder.branchLocationID>>>> CurrentBranchLocation;
  [PXHidden]
  public PXSetup<PX.Objects.AR.Customer>.Where<Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<FSServiceOrder.billCustomerID>>>> TaxCustomer;
  [PXHidden]
  public PXSetup<PX.Objects.CR.Location>.Where<Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<FSServiceOrder.billLocationID>>>>> TaxLocation;
  [PXHidden]
  public PXSetup<PX.Objects.TX.TaxZone>.Where<Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<FSAppointment.taxZoneID>>>> TaxZone;
  [PXViewName("Service Order")]
  public AppointmentEntry.ServiceOrderRelated_View ServiceOrderRelated;
  [PXViewName("Field Service Contact")]
  public ServiceOrderBase<AppointmentEntry, FSAppointment>.FSContact_View ServiceOrder_Contact;
  [PXViewName("Field Service Address")]
  public ServiceOrderBase<AppointmentEntry, FSAppointment>.FSAddress_View ServiceOrder_Address;
  [PXHidden]
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<FSAppointment.curyInfoID>>>> currencyInfoView;
  [PXFilterable(new System.Type[] {})]
  [PXImport(typeof (FSAppointment))]
  [PXViewName("Appointment Details")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (FSAppointmentDet.sODetID), typeof (FSAppointmentDet.lotSerialNbr), typeof (FSAppointmentDet.status), typeof (FSAppointmentDet.uiStatus), typeof (FSSODet.curyBillableExtPrice), typeof (FSSODet.curyBillableTranAmt)})]
  public ServiceOrderBase<AppointmentEntry, FSAppointment>.AppointmentDetails_View AppointmentDetails;
  [PXFilterable(new System.Type[] {})]
  [PXViewName("Appointment Employees")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (FSAppointmentEmployee.lineNbr), typeof (FSAppointmentEmployee.lineRef), typeof (FSAppointmentEmployee.serviceLineRef)})]
  public AppointmentEntry.AppointmentServiceEmployees_View AppointmentServiceEmployees;
  [PXViewName("Appointment Resources")]
  public ServiceOrderBase<AppointmentEntry, FSAppointment>.AppointmentResources_View AppointmentResources;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly2<ARPayment, InnerJoin<FSAdjust, On<ARPayment.docType, Equal<FSAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<FSAdjust.adjgRefNbr>>>>, Where<FSAdjust.adjdOrderType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSAdjust.adjdOrderNbr, Equal<Current<FSServiceOrder.refNbr>>, And<ARPayment.status, NotEqual<ARDocStatus.voided>>>>> Adjustments;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSPostDet, InnerJoin<FSSODet, On<FSSODet.postID, Equal<FSPostDet.postID>>, InnerJoin<FSPostBatch, On<FSPostBatch.batchID, Equal<FSPostDet.batchID>>>>, Where2<Where<FSSODet.sOID, Equal<Current<FSAppointment.sOID>>>, And<Where<FSPostDet.aRPosted, Equal<True>, Or<FSPostDet.aPPosted, Equal<True>, Or<FSPostDet.sOPosted, Equal<True>, Or<FSPostDet.pMPosted, Equal<True>, Or<FSPostDet.sOInvPosted, Equal<True>, Or<FSPostDet.iNPosted, Equal<True>>>>>>>>>, OrderBy<Desc<FSPostDet.batchID, Desc<FSPostDet.aRPosted, Desc<FSPostDet.aPPosted, Desc<FSPostDet.sOPosted, Desc<FSPostDet.aPPosted, Desc<FSPostDet.pMPosted, Desc<FSPostDet.sOInvPosted, Desc<FSPostDet.iNPosted>>>>>>>>>> ServiceOrderPostedIn;
  [PXFilterable(new System.Type[] {})]
  [PXImport(typeof (FSAppointment))]
  [PXCopyPasteHiddenView]
  public ServiceOrderBase<AppointmentEntry, FSAppointment>.AppointmentLog_View LogRecords;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelect<FSAppointmentLogExtItemLine, Where2<Where<FSAppointmentLogExtItemLine.docID, Equal<Current<FSAppointment.appointmentID>>, And<Where<Current<FSLogActionFilter.me>, Equal<False>, Or<FSAppointmentLogExtItemLine.userID, Equal<Current<AccessInfo.userID>>>>>>, And2<Where2<Where<Current<FSLogActionFilter.type>, Equal<FSLogTypeAction.Travel>, And<FSAppointmentLogExtItemLine.itemType, Equal<ListField_Log_ItemType.travel>>>, Or<Where<Current<FSLogActionFilter.type>, NotEqual<FSLogTypeAction.Travel>, And<FSAppointmentLogExtItemLine.itemType, NotEqual<ListField_Log_ItemType.travel>>>>>, And<Where2<Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Pause>, And<FSAppointmentLogExtItemLine.status, Equal<ListField_Status_Log.InProcess>>>, Or<Where2<Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Resume>, And<FSAppointmentLogExtItemLine.status, Equal<ListField_Status_Log.Paused>>>, Or<Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Complete>, And<Where<FSAppointmentLogExtItemLine.status, Equal<ListField_Status_Log.InProcess>, Or<FSAppointmentLogExtItemLine.status, Equal<ListField_Status_Log.Paused>>>>>>>>>>>>, OrderBy<Desc<FSAppointmentLogExtItemLine.selected>>> LogActionLogRecords;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelect<FSAppointmentStaffExtItemLine, Where<FSAppointmentStaffExtItemLine.docID, Equal<Current<FSAppointment.appointmentID>>, And2<Where<FSAppointmentStaffExtItemLine.inventoryID, IsNull, Or<Where<FSAppointmentStaffExtItemLine.inventoryID, IsNotNull, And<FSAppointmentStaffExtItemLine.isTravelItem, NotEqual<True>>>>>, And<Where<Current<FSLogActionFilter.me>, Equal<True>, And<FSAppointmentStaffExtItemLine.userID, Equal<Current<AccessInfo.userID>>, Or<Current<FSLogActionFilter.me>, Equal<False>>>>>>>, OrderBy<Desc<FSAppointmentStaffExtItemLine.selected>>> LogActionStaffRecords;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelect<FSAppointmentStaffDistinct, Where<FSAppointmentStaffDistinct.docID, Equal<Current<FSAppointment.appointmentID>>>, OrderBy<Desc<FSAppointmentStaffDistinct.selected>>> LogActionStaffDistinctRecords;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelect<FSDetailFSLogAction, Where<FSDetailFSLogAction.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSDetailFSLogAction.isTravelItem, NotEqual<True>>>> ServicesLogAction;
  public PXSetup<FSServiceContract>.Where<Where<FSServiceContract.serviceContractID, Equal<Current<FSAppointment.billServiceContractID>>>> BillServiceContractRelated;
  public PXSetup<FSContractPeriod>.Where<Where<FSContractPeriod.contractPeriodID, Equal<Current<FSAppointment.billContractPeriodID>>, And<FSContractPeriod.serviceContractID, Equal<Current<FSAppointment.billServiceContractID>>, And<Current<FSBillingCycle.billingBy>, Equal<ListField_Billing_By.Appointment>>>>> BillServiceContractPeriod;
  public PXSelect<FSContractPeriodDet, Where<FSContractPeriodDet.contractPeriodID, Equal<Current<FSContractPeriod.contractPeriodID>>, And<FSContractPeriodDet.serviceContractID, Equal<Current<FSContractPeriod.serviceContractID>>>>> BillServiceContractPeriodDetail;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>> ContractRelatedToProject;
  [PXViewName("Appointment Posting Info")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSPostDet, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSPostBatch, On<FSPostBatch.batchID, Equal<FSPostDet.batchID>>>>, Where2<Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>>, And<Where<FSPostDet.aRPosted, Equal<True>, Or<FSPostDet.aPPosted, Equal<True>, Or<FSPostDet.sOPosted, Equal<True>, Or<FSPostDet.pMPosted, Equal<True>, Or<FSPostDet.sOInvPosted, Equal<True>, Or<FSPostDet.iNPosted, Equal<True>>>>>>>>>, OrderBy<Desc<FSPostDet.batchID, Desc<FSPostDet.aRPosted, Desc<FSPostDet.aPPosted, Desc<FSPostDet.sOPosted, Desc<FSPostDet.pMPosted, Desc<FSPostDet.sOInvPosted, Desc<FSPostDet.iNPosted>>>>>>>>> AppointmentPostedIn;
  [PXCopyPasteHiddenView]
  public PXSelect<FSBillHistory, Where<FSBillHistory.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSBillHistory.appointmentRefNbr, Equal<Current<FSAppointment.refNbr>>>>, OrderBy<Desc<FSBillHistory.createdDateTime>>> InvoiceRecords;
  [PXCopyPasteHiddenView]
  public PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Current<FSAppointment.scheduleID>>>> ScheduleRecord;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;
  [PXCopyPasteHiddenView]
  public new PXSelectReadonly<FSProfitability> ProfitabilityRecords;
  [PXViewName("Answers")]
  public FSAttributeList<FSAppointment> Answers;
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<FSApptLineSplit, Where<FSApptLineSplit.srvOrdType, Equal<Current<FSAppointmentDet.srvOrdType>>, And<FSApptLineSplit.apptNbr, Equal<Current<FSAppointmentDet.refNbr>>, And<FSApptLineSplit.lineNbr, Equal<Current<FSAppointmentDet.lineNbr>>>>>> Splits;
  [PXCopyPasteHiddenView]
  public PXSelect<FSAppointmentTax, Where<FSAppointmentTax.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentTax.refNbr, Equal<Current<FSAppointment.refNbr>>>>, OrderBy<Asc<FSAppointmentTax.taxID>>> TaxLines;
  [PXViewName("Appointment Tax")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSAppointmentTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<FSAppointmentTaxTran.taxID>>>, Where<FSAppointmentTaxTran.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentTaxTran.refNbr, Equal<Current<FSAppointment.refNbr>>>>, OrderBy<Asc<FSAppointmentTaxTran.taxID, Asc<FSAppointmentTaxTran.recordID>>>> Taxes;
  public TreeWFStageHelper.TreeWFStageView TreeWFStages;
  public PXWorkflowEventHandler<FSAppointment> OnServiceContractCleared;
  public PXWorkflowEventHandler<FSAppointment> OnServiceContractPeriodAssigned;
  public PXWorkflowEventHandler<FSAppointment> OnServiceContractPeriodCleared;
  public PXWorkflowEventHandler<FSAppointment> OnRequiredServiceContractPeriodCleared;
  public PXWorkflowEventHandler<FSAppointment> OnAppointmentUnposted;
  public PXWorkflowEventHandler<FSAppointment> OnAppointmentPosted;
  public PXWorkflowEventHandler<FSAppointment> OnAppointmentStatusChanged;
  [PXCopyPasteHiddenView]
  public PXFilter<StaffSelectionFilter> StaffSelectorFilter;
  [PXCopyPasteHiddenView]
  public StaffSelectionHelper.SkillRecords_View SkillGridFilter;
  [PXCopyPasteHiddenView]
  public StaffSelectionHelper.LicenseTypeRecords_View LicenseTypeGridFilter;
  [PXCopyPasteHiddenView]
  public StaffSelectionHelper.StaffRecords_View StaffRecords;
  public PXAction<FSAppointment> openStaffSelectorFromServiceTab;
  public PXAction<FSAppointment> openStaffSelectorFromStaffTab;

  [InjectDependency]
  protected new PXSiteMapProvider SiteMapProvider { get; private set; }

  protected bool UpdatingItemLinesBecauseOfDocStatusChange { get; set; }

  public bool RecalculateExternalTaxesSync { get; set; }

  protected virtual void RecalculateExternalTaxes()
  {
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

  private void RefreshServiceOrderRelated()
  {
    ((PXSelectBase) this.ServiceOrderRelated).Cache.Clear();
    ((PXSelectBase) this.ServiceOrderRelated).View.Clear();
    ((PXSelectBase) this.ServiceOrderRelated).View.RequestRefresh();
  }

  public void ClearServiceOrderEntry()
  {
    ((PXGraph) this._ServiceOrderEntryGraph)?.Clear((PXClearOption) 3);
  }

  protected ServiceOrderEntry GetServiceOrderEntryGraph(bool clearGraph)
  {
    if (this._ServiceOrderEntryGraph == null)
      this._ServiceOrderEntryGraph = PXGraph.CreateInstance<ServiceOrderEntry>();
    else if (clearGraph && !this._ServiceOrderEntryGraph.RunningPersist)
      ((PXGraph) this._ServiceOrderEntryGraph).Clear();
    if (!this._ServiceOrderEntryGraph.RunningPersist)
    {
      this._ServiceOrderEntryGraph.RecalculateExternalTaxesSync = true;
      this._ServiceOrderEntryGraph.GraphAppointmentEntryCaller = this;
    }
    return this._ServiceOrderEntryGraph;
  }

  public void SetServiceOrderEntryGraph(ServiceOrderEntry soGraph)
  {
    this._ServiceOrderEntryGraph = soGraph;
  }

  protected EmployeeActivitiesEntry GetEmployeeActivitiesEntryGraph(bool clearGraph = true)
  {
    if (this._EmployeeActivitiesEntryGraph == null)
      this._EmployeeActivitiesEntryGraph = PXGraph.CreateInstance<EmployeeActivitiesEntry>();
    else if (clearGraph)
      ((PXGraph) this._EmployeeActivitiesEntryGraph).Clear((PXClearOption) 1);
    ((PXGraph) this._EmployeeActivitiesEntryGraph).GetExtension<SM_EmployeeActivitiesEntry>().GraphAppointmentEntryCaller = this;
    return this._EmployeeActivitiesEntryGraph;
  }

  protected virtual Dictionary<FSAppointmentDet, FSSODet> ApptLinesWithSrvOrdLineUpdated
  {
    get => this._ApptLinesWithSrvOrdLineUpdated;
    set => this._ApptLinesWithSrvOrdLineUpdated = value;
  }

  protected virtual Dictionary<FSApptLineSplit, FSSODetSplit> ApptSplitsWithSrvOrdSplitUpdated
  {
    get => this._ApptSplitsWithSrvOrdSplitUpdated;
    set => this._ApptSplitsWithSrvOrdSplitUpdated = value;
  }

  public static bool IsReadyToBeUsed(PXGraph graph, string callerScreenID)
  {
    return PXSelectBase<FSSetup, PXSelect<FSSetup, Where<FSSetup.calendarID, IsNotNull>>.Config>.Select(graph, Array.Empty<object>()).Count > 0 & TimeCardHelper.CanCurrentUserEnterTimeCards(graph, callerScreenID);
  }

  public virtual void SkipTaxCalcAndSave()
  {
    if (((PXGraph) this).GetExtension<AppointmentEntryExternalTax>() != null)
      ((PXGraph) this).GetExtension<AppointmentEntryExternalTax>().SkipTaxCalcAndSave();
    else
      ((PXAction) this.Save).Press();
  }

  public virtual void SaveBeforeApplyAction(PXCache cache, FSAppointment row)
  {
    PXEntryStatus status = cache.GetStatus((object) row);
    if (((!cache.AllowInsert ? 0 : (status == 2 ? 1 : 0)) | (!cache.AllowUpdate ? (false ? 1 : 0) : (status == 1 ? 1 : 0))) == 0)
      return;
    using (new SuppressErpTransactionsScope(true))
    {
      this.SkipTaxCalcAndSave();
      ((PXGraph) this).SelectTimeStamp();
      row.tstamp = ((PXGraph) this).TimeStamp;
    }
  }

  public virtual void ChangeStatusSaveAndSkipExternalTaxCalc(FSAppointment fsAppointmentRow)
  {
    AppointmentEntryExternalTax extension = ((PXGraph) this).GetExtension<AppointmentEntryExternalTax>();
    if (extension != null)
    {
      bool externalTaxCalcOnSave = extension.skipExternalTaxCalcOnSave;
      try
      {
        extension.skipExternalTaxCalcOnSave = true;
        this.ForceUpdateCacheAndSave(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow);
      }
      finally
      {
        extension.skipExternalTaxCalcOnSave = externalTaxCalcOnSave;
      }
    }
    else
      this.ForceUpdateCacheAndSave(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow);
  }

  public AppointmentEntry()
  {
    FSSetup current = ((PXSelectBase<FSSetup>) this.SetupRecord).Current;
    this.NeedRecalculateRouteStats = false;
    PXGraph.FieldUpdatedEvents fieldUpdated = ((PXGraph) this).FieldUpdated;
    string name = ((PXSelectBase) this.AppointmentRecords).Name;
    string str1 = typeof (FSAppointment.scheduledDateTimeBegin).Name + "_Date";
    AppointmentEntry appointmentEntry1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) appointmentEntry1, __vmethodptr(appointmentEntry1, FSAppointment_ScheduledDateTimeBegin_FieldUpdated));
    fieldUpdated.AddHandler(name, str1, pxFieldUpdated);
    PXGraph.FieldUpdatingEvents fieldUpdating1 = ((PXGraph) this).FieldUpdating;
    System.Type type1 = typeof (FSAppointment);
    string str2 = typeof (FSAppointment.actualDateTimeBegin).Name + "_Time";
    AppointmentEntry appointmentEntry2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) appointmentEntry2, __vmethodptr(appointmentEntry2, FSAppointment_ActualDateTimeBegin_Time_FieldUpdating));
    fieldUpdating1.AddHandler(type1, str2, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = ((PXGraph) this).FieldUpdating;
    System.Type type2 = typeof (FSAppointment);
    string str3 = typeof (FSAppointment.actualDateTimeEnd).Name + "_Time";
    AppointmentEntry appointmentEntry3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) appointmentEntry3, __vmethodptr(appointmentEntry3, FSAppointment_ActualDateTimeEnd_Time_FieldUpdating));
    fieldUpdating2.AddHandler(type2, str3, pxFieldUpdating2);
    if (!PXGraph.ProxyIsActive && !TimeCardHelper.CanCurrentUserEnterTimeCards((PXGraph) this, ((PXGraph) this).Accessinfo.ScreenID))
    {
      if (((PXGraph) this).IsExport || ((PXGraph) this).IsImport || HttpContext.Current.Request.Form["__CALLBACKID"] != null)
        throw new PXException("User must be an Employee to use current screen.");
      Redirector.Redirect(HttpContext.Current, $"~/Frames/Error.aspx?exceptionID={"User must be an Employee to use current screen."}&typeID={"error"}", false);
    }
    PXUIFieldAttribute.SetDisplayName<FSAppointment.mapLatitude>(((PXSelectBase) this.AppointmentRecords).Cache, "Appointment Location");
    PXUIFieldAttribute.SetDisplayName<FSAppointment.gPSLatitudeStart>(((PXSelectBase) this.AppointmentRecords).Cache, "Start Location");
    PXUIFieldAttribute.SetDisplayName<FSAppointment.gPSLatitudeComplete>(((PXSelectBase) this.AppointmentRecords).Cache, "End Location");
    PXUIFieldAttribute.SetDisplayName<FSxService.actionType>(((PXSelectBase) this.InventoryItemHelper).Cache, "Pickup/Deliver Items");
  }

  [PXFormula(typeof (Switch<Case<Where<Current<FSSrvOrdType.behavior>, Equal<ListField.ServiceOrderTypeBehavior.internalAppointment>>, Selector<FSAppointment.soRefNbr, Selector<Current<FSServiceOrder.branchLocationID>, FSBranchLocation.descr>>>, Selector<FSAppointment.customerID, BAccountSelectorBase.acctName>>))]
  [PXMergeAttributes]
  protected virtual void FSAppointment_FormCaptionDescription_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Name", Enabled = false)]
  protected virtual void BAccount_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.status, IsNull, Or<PX.Objects.CR.BAccount.status, Equal<CustomerStatus.active>, Or<PX.Objects.CR.BAccount.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.CR.BAccount.status)})]
  [FSSelectorBAccountCustomerOrCombined]
  protected virtual void FSServiceOrder_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "External Reference", Visible = false)]
  protected virtual void FSServiceOrder_CustWorkOrderRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Customer Order", Visible = false)]
  protected virtual void FSServiceOrder_CustPORefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void FSServiceOrder_Priority_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void FSServiceOrder_Severity_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Problem", Visible = false)]
  protected virtual void FSServiceOrder_ProblemID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void FSServiceOrder_AssignedEmpID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">AAAAAAAAAAAAAAA")]
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
  protected virtual void FSAppointmentDet_InventoryID_CacheAttached(PXCache sender)
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

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    ((PXSelectBase) this.InvoiceRecords).Cache.ClearQueryCache();
    return ((PXAction) new PXCancel<FSAppointment>((PXGraph) this, nameof (Cancel))).Press(a);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable StartTravel(PXAdapter adapter)
  {
    if (((PXSelectBase<FSLogActionStartFilter>) this.LogActionStartFilter).Current != null)
    {
      // ISSUE: method pointer
      int num = this.LogActionStartFilter.AskExtFullyValid(new PXView.InitializePanel((object) this, __methodptr(\u003CStartTravel\u003Eb__88_0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionStartFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
      {
        if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.DetLineRef == null && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.DfltBillableTravelItem.HasValue)
        {
          string itemLineRef = this.GetItemLineRef((PXGraph) this, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.AppointmentID, true);
          if (itemLineRef == null)
          {
            FSAppointmentDet instance = (FSAppointmentDet) ((PXSelectBase) this.AppointmentDetails).Cache.CreateInstance();
            instance.LineType = "SERVI";
            instance.InventoryID = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.DfltBillableTravelItem;
            ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.DetLineRef = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Insert(instance).LineRef;
          }
          else
            ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.DetLineRef = itemLineRef;
        }
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CompleteTravel(PXAdapter adapter)
  {
    if (((PXSelectBase<FSLogActionPCRFilter>) this.LogActionPCRFilter).Current != null)
    {
      // ISSUE: method pointer
      int num = this.LogActionPCRFilter.AskExtFullyValid(new PXView.InitializePanel((object) this, __methodptr(\u003CCompleteTravel\u003Eb__90_0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionPCRFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
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
  public virtual IEnumerable ViewStartGPSOnMap(PXAdapter adapter)
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current != null && ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.SOID.HasValue)
      new PX.Data.GoogleMapLatLongRedirector().ShowAddressByLocation(((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.GPSLatitudeStart, ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.GPSLongitudeStart);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCompleteGPSOnMap(PXAdapter adapter)
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current != null && ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.SOID.HasValue)
      new PX.Data.GoogleMapLatLongRedirector().ShowAddressByLocation(((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.GPSLatitudeComplete, ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.GPSLongitudeComplete);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable EditAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      foreach (FSAppointment fsAppointment in list)
      {
        FSAppointment fsAppointmentRow = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Update(fsAppointment);
        try
        {
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            if (this.UpdateServiceOrderWhenAppointmentEdit(fsAppointmentRow))
            {
              ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.MustUpdateServiceOrder = new bool?(true);
              this.SetServiceOrderStatusFromAppointment(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, fsAppointmentRow, AppointmentEntry.ActionButton.EditAppointment);
            }
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
        }
        finally
        {
          fsAppointmentRow.EditActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CloneAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!adapter.MassProcess)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      if (!string.IsNullOrEmpty(list[0].RefNbr))
      {
        if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.Completed.GetValueOrDefault())
        {
          ServiceOrderEntry serviceOrderEntryGraph = this.GetServiceOrderEntryGraph(true);
          ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.RefNbr, new object[1]
          {
            (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SrvOrdType
          }));
          ((PXAction) serviceOrderEntryGraph.reopenOrder).Press();
        }
        CloneAppointmentProcess instance = PXGraph.CreateInstance<CloneAppointmentProcess>();
        ((PXSelectBase<FSCloneAppointmentFilter>) instance.Filter).Current.SrvOrdType = list[0].SrvOrdType;
        ((PXSelectBase<FSCloneAppointmentFilter>) instance.Filter).Current.RefNbr = list[0].RefNbr;
        ((PXSelectBase<FSAppointment>) instance.AppointmentSelected).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentSelected).Select(Array.Empty<object>()));
        ((PXAction) instance.cancel).Press();
        throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
      }
    }
    return (IEnumerable) list;
  }

  [PXButton]
  [PXUIField(DisplayName = "Reports")]
  public virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!string.IsNullOrEmpty(reportID))
    {
      ((PXAction) this.Save).Press();
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      PXReportRequiredException ex = (PXReportRequiredException) null;
      Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
      foreach (FSAppointment fsAppointment in list)
      {
        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
        dictionary2["FSAppointment.SrvOrdType"] = fsAppointment.SrvOrdType;
        dictionary2["FSAppointment.RefNbr"] = fsAppointment.RefNbr;
        string str = new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.CustomerID, fsAppointment.BranchID);
        ex = PXReportRequiredException.CombineReport(ex, str, dictionary2, (CurrentLocalization) null);
        reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, dictionary2, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, str, fsAppointment.BranchID, (CurrentLocalization) null);
      }
      if (ex != null)
        ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (async ct =>
        {
          int num = await SMPrintJobMaint.CreatePrintJobGroups(reportsToPrint, ct) ? 1 : 0;
          throw ex;
        }));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddress(PXAdapter adapter)
  {
    AppointmentEntry aGraph = this;
    foreach (FSAppointment fsAppointment in adapter.Get<FSAppointment>())
    {
      if (fsAppointment != null)
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
      yield return (object) fsAppointment;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable StartAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      foreach (FSAppointment fsAppointment1 in list)
      {
        FSAppointment fsAppointment2 = fsAppointment1;
        try
        {
          ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = fsAppointment2;
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            fsAppointment2.StartActionRunning = new bool?(true);
            fsAppointment2.HandleManuallyActualTime = new bool?(false);
            fsAppointment2 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Update(fsAppointment2);
            this.ForceAppointmentDetActualFieldsUpdate(false);
            try
            {
              this.SkipManualTimeFlagUpdate = true;
              DateTime? dateTimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
              FSSrvOrdType current1 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
              if ((current1 != null ? (current1.OnStartApptSetStartTimeInHeader.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                this.SetHeaderActualDateTimeBegin(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment2, dateTimeBegin);
              else
                ((PXSelectBase) this.AppointmentRecords).Cache.SetDefaultExt<FSAppointment.executionDate>((object) fsAppointment2);
              fsAppointment2 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Update(fsAppointment2);
              FSSrvOrdType current2 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
              if ((current2 != null ? (current2.OnStartApptStartUnassignedStaff.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                this.StartStaffAction(GraphHelper.RowCast<FSAppointmentStaffExtItemLine>((IEnumerable) PXSelectBase<FSAppointmentStaffExtItemLine, PXSelect<FSAppointmentStaffExtItemLine, Where<FSAppointmentStaffExtItemLine.detLineRef, IsNull, And<FSAppointmentStaffExtItemLine.docID, Equal<Required<FSAppointmentStaffExtItemLine.docID>>>>>.Config>.Select((PXGraph) this, new object[1]
                {
                  (object) fsAppointment2.AppointmentID
                })), dateTimeBegin);
              FSSrvOrdType current3 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
              if ((current3 != null ? (current3.OnStartApptStartServiceAndStaff.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                this.StartServiceBasedOnAssignmentAction(GraphHelper.RowCast<FSDetailFSLogAction>((IEnumerable) PXSelectBase<FSDetailFSLogAction, PXSelectJoin<FSDetailFSLogAction, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSDetailFSLogAction.inventoryID>>>, Where<FSDetailFSLogAction.appointmentID, Equal<Required<FSDetailFSLogAction.appointmentID>>, And<FSxService.isTravelItem, Equal<False>>>>.Config>.Select((PXGraph) this, new object[1]
                {
                  (object) fsAppointment2.AppointmentID
                })), dateTimeBegin);
              FSSrvOrdType current4 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
              if ((current4 != null ? (current4.OnStartApptSetNotStartItemInProcess.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                foreach (FSAppointmentDet apptDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r =>
                {
                  if (r.Status != "CC" && r.Status != "WP" && r.Status != "RP")
                  {
                    bool? isTravelItem = r.IsTravelItem;
                    bool flag = false;
                    if (isTravelItem.GetValueOrDefault() == flag & isTravelItem.HasValue)
                      return !r.IsLinkedItem;
                  }
                  return false;
                })))
                  this.ChangeItemLineStatus(apptDet, "IP");
              }
              FSSrvOrdType current5 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
              int num1;
              if (current5 == null)
              {
                num1 = 0;
              }
              else
              {
                bool? setEndTimeInHeader = current5.OnCompleteApptSetEndTimeInHeader;
                bool flag = false;
                num1 = setEndTimeInHeader.GetValueOrDefault() == flag & setEndTimeInHeader.HasValue ? 1 : 0;
              }
              if (num1 != 0)
              {
                FSSrvOrdType current6 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
                int num2;
                if (current6 == null)
                {
                  num2 = 0;
                }
                else
                {
                  bool? headerBasedOnLog = current6.SetTimeInHeaderBasedOnLog;
                  bool flag = false;
                  num2 = headerBasedOnLog.GetValueOrDefault() == flag & headerBasedOnLog.HasValue ? 1 : 0;
                }
                if (num2 != 0)
                  ((PXSelectBase) this.AppointmentSelected).Cache.SetValueExtIfDifferent<FSAppointment.handleManuallyActualTime>((object) fsAppointment2, (object) true);
              }
            }
            finally
            {
              this.SkipManualTimeFlagUpdate = false;
            }
            if (((PXGraph) this).IsMobile && ((PXSelectBase<FSSetup>) this.SetupRecord).Current != null && ((PXSelectBase<FSSetup>) this.SetupRecord).Current.TrackAppointmentLocation.GetValueOrDefault())
            {
              FSGPSTrackingHistory fsgpsTrackingHistory = PXResultset<FSGPSTrackingHistory>.op_Implicit(PXSelectBase<FSGPSTrackingHistory, PXSelectJoin<FSGPSTrackingHistory, InnerJoin<FSGPSTrackingRequest, On<FSGPSTrackingRequest.trackingID, Equal<FSGPSTrackingHistory.trackingID>>>, Where<FSGPSTrackingRequest.userName, Equal<Required<AccessInfo.userName>>, And<FSGPSTrackingHistory.executionDate, GreaterEqual<Required<FSAppointment.executionDate>>>>, OrderBy<Desc<FSGPSTrackingHistory.executionDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
              {
                (object) ((PXGraph) this).Accessinfo.UserName,
                (object) fsAppointment2.ExecutionDate
              }));
              if (fsgpsTrackingHistory != null)
              {
                Decimal? nullable = fsgpsTrackingHistory.Longitude;
                if (nullable.HasValue)
                {
                  nullable = fsgpsTrackingHistory.Latitude;
                  if (nullable.HasValue)
                  {
                    fsAppointment2.GPSLatitudeStart = fsgpsTrackingHistory.Latitude;
                    fsAppointment2.GPSLongitudeStart = fsgpsTrackingHistory.Longitude;
                  }
                }
              }
            }
            this.PerformTransition(fsAppointment2);
            ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Update(fsAppointment2);
            this.SkipTaxCalcAndSave();
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
          this.LoadServiceOrderRelatedAfterStatusChange(fsAppointment2);
        }
        finally
        {
          fsAppointment2.StartActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable PauseAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list1 = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list1.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list1[0]);
      foreach (FSAppointment fsAppointment in list1)
      {
        try
        {
          ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = fsAppointment;
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            List<FSAppointmentLog> list2 = GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) PXSelectBase<FSAppointmentLog, PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.status, Equal<ListField_Status_Log.InProcess>, And<FSAppointmentLog.itemType, NotEqual<ListField_Log_ItemType.travel>>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) (int?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.AppointmentID
            })).ToList<FSAppointmentLog>();
            this.CompletePauseMultipleLogs(PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now)), "IP", "S", false, list2);
            this.PerformTransition(fsAppointment);
            this.SkipTaxCalcAndSave();
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
          this.LoadServiceOrderRelatedAfterStatusChange(fsAppointment);
        }
        finally
        {
          fsAppointment.PauseActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list1;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ResumeAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      foreach (FSAppointment fsAppointment in list)
      {
        try
        {
          ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = fsAppointment;
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            this.ResumeMultipleLogs((PXSelectBase<FSAppointmentLog>) new PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.status, Equal<ListField_Status_Log.Paused>>>>((PXGraph) this), (object) fsAppointment.AppointmentID);
            this.PerformTransition(fsAppointment);
            this.SkipTaxCalcAndSave();
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
          this.LoadServiceOrderRelatedAfterStatusChange(fsAppointment);
        }
        finally
        {
          fsAppointment.ResumeActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CancelAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      foreach (FSAppointment fsAppointment1 in list)
      {
        FSAppointment fsAppointment2 = fsAppointment1;
        try
        {
          ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = fsAppointment2;
          FSAppointmentDet fsAppointmentDet = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r => r.Status != "CC" && !this.CanItemLineBeCanceled(r))).FirstOrDefault<FSAppointmentDet>();
          if (fsAppointmentDet != null)
            throw new PXException("The {0} action is not allowed for the {1} line with the {2} status. To perform this action, a selected line must have the Not Started or Not Performed status.", new object[3]
            {
              (object) PXLocalizer.Localize(((PXAction) this.cancelItemLine).GetCaption()),
              (object) this.GetLineDisplayHint((PXGraph) this, fsAppointmentDet.LineRef, fsAppointmentDet.TranDesc, fsAppointmentDet.InventoryID),
              (object) PXStringListAttribute.GetLocalizedLabel<FSAppointment.status>(((PXSelectBase) this.AppointmentDetails).Cache, (object) fsAppointmentDet, fsAppointmentDet.Status)
            });
          if (GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (r => r.Status == "P" || r.Status == "C")).FirstOrDefault<FSAppointmentLog>() != null)
            throw new PXException("An appointment with log lines cannot be canceled. To cancel this appointment, delete all log lines on the Log tab first.");
          string newStatus = "CC";
          string latestServiceOrderStatus = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.Status;
          if (6 == ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Ask("Cancel Appointment Details", "Will a new appointment be required for scheduling?", (MessageButtons) 4))
            newStatus = "NP";
          else
            latestServiceOrderStatus = this.GetFinalServiceOrderStatus(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, fsAppointment2);
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            foreach (FSAppointmentDet apptDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r => r.Status != "CC" && !r.IsLinkedItem)))
              this.ChangeItemLineStatus(apptDet, newStatus);
            fsAppointment2 = (FSAppointment) ((PXSelectBase) this.AppointmentRecords).Cache.CreateCopy((object) fsAppointment2);
            fsAppointment2.BillServiceContractID = new int?();
            fsAppointment2 = (FSAppointment) ((PXSelectBase) this.AppointmentRecords).Cache.Update((object) fsAppointment2);
            this.updateContractPeriod = true;
            this.PerformTransition(fsAppointment2);
            this.SkipTaxCalcAndSave();
            if (!string.IsNullOrEmpty(latestServiceOrderStatus))
              this.SetLatestServiceOrderStatusBaseOnAppointmentStatus(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, latestServiceOrderStatus);
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
          this.LoadServiceOrderRelatedAfterStatusChange(fsAppointment2);
        }
        finally
        {
          fsAppointment2.CancelActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReopenAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      foreach (FSAppointment fsAppointment1 in list)
      {
        FSAppointment fsAppointment2 = fsAppointment1;
        try
        {
          ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = fsAppointment2;
          if (fsAppointment1.Canceled.GetValueOrDefault())
            ((PXSelectBase) this.AppointmentRecords).Cache.AllowUpdate = true;
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            ((PXSelectBase) this.AppointmentRecords).Cache.SetDefaultExt<FSAppointment.billContractPeriodID>((object) fsAppointment2);
            this.ForceAppointmentDetActualFieldsUpdate(true);
            this.SetServiceOrderStatusFromAppointment(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, fsAppointment2, AppointmentEntry.ActionButton.ReOpenAppointment);
            this.ClearAppointmentLog();
            fsAppointment2 = (FSAppointment) ((PXSelectBase) this.AppointmentRecords).Cache.CreateCopy((object) fsAppointment2);
            fsAppointment2.HandleManuallyActualTime = new bool?(false);
            fsAppointment2.ActualDateTimeBegin = new DateTime?();
            fsAppointment2.ActualDateTimeEnd = new DateTime?();
            fsAppointment2 = (FSAppointment) ((PXSelectBase) this.AppointmentRecords).Cache.Update((object) fsAppointment2);
            this.PerformTransition(fsAppointment2);
            this.SkipTaxCalcAndSave();
            ((PXSelectBase) this.LogRecords).Cache.Clear();
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
          this.LoadServiceOrderRelatedAfterStatusChange(fsAppointment2);
        }
        finally
        {
          fsAppointment2.ReopenActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CompleteAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list1 = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list1.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list1[0]);
      foreach (FSAppointment fsAppointmentRow in list1)
      {
        FSAppointment fsAppointment = fsAppointmentRow;
        try
        {
          ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = fsAppointment;
          FSSrvOrdType current1 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
          if ((current1 != null ? (current1.OnCompleteApptRequireLog.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            this.VerifyOnCompleteApptRequireLog(((PXSelectBase) this.AppointmentSelected).Cache);
          DateTime? dateTimeEnd = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
          FSSrvOrdType current2 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
          if ((current2 != null ? (current2.OnCompleteApptSetEndTimeInHeader.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            this.SetHeaderActualDateTimeEnd(((PXSelectBase) this.AppointmentSelected).Cache, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, dateTimeEnd);
          if (((PXGraph) this).IsMobile)
            this.ValidateSignatureFields(((PXSelectBase) this.AppointmentSelected).Cache, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, this.GetRequireCustomerSignature((PXGraph) this, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current));
          if (GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => x.IsService)).Count<FSAppointmentDet>() == 0)
            throw new PXException("An Appointment without Services cannot be completed. At least one Service must be added in the Details tab.", new object[1]
            {
              (object) (PXErrorLevel) 4
            });
          PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
          foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r =>
          {
            bool? isTravelItem = r.IsTravelItem;
            bool flag = false;
            if (!(isTravelItem.GetValueOrDefault() == flag & isTravelItem.HasValue))
              return false;
            if (r.Status == "NS" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.OnCompleteApptSetNotStartedItemsAs == "DN")
              return true;
            return r.Status == "IP" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.OnCompleteApptSetInProcessItemsAs == "DN";
          })))
          {
            if (propertyException1 == null)
              propertyException1 = new PXSetPropertyException("The appointment cannot be completed because it contains detail lines with the Not-Started or In-Process status.");
            ((PXSelectBase) this.AppointmentDetails).Cache.RaiseExceptionHandling<FSAppointmentDet.status>((object) fsAppointmentDet, (object) fsAppointmentDet.Status, (Exception) propertyException1);
          }
          foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r => r.EnablePO.GetValueOrDefault() && !r.POCompleted.GetValueOrDefault() && r.Status == "WP")))
          {
            if (propertyException1 == null)
              propertyException1 = new PXSetPropertyException("The appointment cannot be completed because it contains lines with the Waiting for Purchased Items status.");
            ((PXSelectBase) this.AppointmentDetails).Cache.RaiseExceptionHandling<FSAppointmentDet.status>((object) fsAppointmentDet, (object) fsAppointmentDet.Status, (Exception) propertyException1);
          }
          PXSetPropertyException propertyException2 = (PXSetPropertyException) null;
          foreach (FSAppointmentLog fsAppointmentLog in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (r =>
          {
            bool? travel = r.Travel;
            bool flag = false;
            return travel.GetValueOrDefault() == flag & travel.HasValue && r.Status == "P" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.OnCompleteApptSetInProcessItemsAs == "DN";
          })))
          {
            if (propertyException2 == null)
              propertyException2 = new PXSetPropertyException("The appointment cannot be completed because it contains log lines with the In-Process status.");
            ((PXSelectBase) this.LogRecords).Cache.RaiseExceptionHandling<FSAppointmentLog.status>((object) fsAppointmentLog, (object) fsAppointmentLog.Status, (Exception) propertyException2);
          }
          if (propertyException1 != null || propertyException2 != null)
            throw new PXException(((Exception) propertyException1)?.Message + Environment.NewLine + ((Exception) propertyException2)?.Message);
          int num = 0;
          if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.OnCompleteApptSetInProcessItemsAs != "DN")
          {
            List<FSAppointmentLog> list2 = GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) PXSelectBase<FSAppointmentLog, PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.itemType, NotEqual<ListField_Log_ItemType.travel>, And<Where<FSAppointmentLog.status, Equal<ListField_Status_Log.InProcess>, Or<FSAppointmentLog.status, Equal<ListField_Status_Log.Paused>>>>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) (int?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.AppointmentID
            })).ToList<FSAppointmentLog>();
            string str = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.OnCompleteApptSetInProcessItemsAs == "CP" ? "CP" : "NF";
            num += this.CompletePauseMultipleLogs(dateTimeEnd, str, "C", true, list2);
            IEnumerable<FSAppointmentDet> fsAppointmentDets = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r =>
            {
              bool? isTravelItem = r.IsTravelItem;
              bool flag = false;
              return isTravelItem.GetValueOrDefault() == flag & isTravelItem.HasValue && r.Status == "IP";
            }));
            this.SplitAppoinmentLogLinesByDays();
            foreach (FSAppointmentDet apptDet in fsAppointmentDets)
            {
              this.ChangeItemLineStatus(apptDet, str);
              ++num;
            }
          }
          if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.OnCompleteApptSetNotStartedItemsAs != "DN")
          {
            string newStatus = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.OnCompleteApptSetNotStartedItemsAs == "CP" ? "CP" : "NP";
            foreach (FSAppointmentDet apptDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r =>
            {
              if (r.Status == "NS")
              {
                bool? isTravelItem = r.IsTravelItem;
                bool flag = false;
                if (isTravelItem.GetValueOrDefault() == flag & isTravelItem.HasValue)
                  return !r.IsLinkedItem;
              }
              return false;
            })))
            {
              this.ChangeItemLineStatus(apptDet, newStatus);
              ++num;
            }
          }
          if (!this.ActualDateAndTimeValidation(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current))
            throw new PXException("The Appointment cannot be completed. Please fill out the actual Date and Time fields.");
          if (((PXGraph) this).IsMobile)
            this.ValidateSignatureFields(((PXSelectBase) this.AppointmentSelected).Cache, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, this.GetRequireCustomerSignature((PXGraph) this, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current));
          fsAppointment = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
          if (num > 0)
          {
            using (new SuppressErpTransactionsScope(true))
              this.SkipTaxCalcAndSave();
          }
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            this.SetServiceOrderStatusFromAppointment(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, fsAppointmentRow, AppointmentEntry.ActionButton.CompleteAppointment);
            if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SourceType == "SO")
              this.UpdateSalesOrderByCompletingAppointment((PXGraph) this, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SourceDocType, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SourceRefNbr);
            if (((PXGraph) this).IsMobile && ((PXSelectBase<FSSetup>) this.SetupRecord).Current != null && ((PXSelectBase<FSSetup>) this.SetupRecord).Current.TrackAppointmentLocation.GetValueOrDefault())
            {
              FSGPSTrackingHistory fsgpsTrackingHistory = PXResultset<FSGPSTrackingHistory>.op_Implicit(PXSelectBase<FSGPSTrackingHistory, PXSelectJoin<FSGPSTrackingHistory, InnerJoin<FSGPSTrackingRequest, On<FSGPSTrackingRequest.trackingID, Equal<FSGPSTrackingHistory.trackingID>>>, Where<FSGPSTrackingRequest.userName, Equal<Required<AccessInfo.userName>>, And<FSGPSTrackingHistory.executionDate, GreaterEqual<Required<FSAppointment.executionDate>>>>, OrderBy<Desc<FSGPSTrackingHistory.executionDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
              {
                (object) ((PXGraph) this).Accessinfo.UserName,
                (object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate
              }));
              if (fsgpsTrackingHistory != null)
              {
                Decimal? nullable = fsgpsTrackingHistory.Longitude;
                if (nullable.HasValue)
                {
                  nullable = fsgpsTrackingHistory.Latitude;
                  if (nullable.HasValue)
                  {
                    fsAppointment.GPSLatitudeComplete = fsgpsTrackingHistory.Latitude;
                    fsAppointment.GPSLongitudeComplete = fsgpsTrackingHistory.Longitude;
                  }
                }
              }
            }
            this.CalculateCosts();
            this.PerformTransition(fsAppointment);
            this.SkipTaxCalcAndSave();
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
          this.LoadServiceOrderRelatedAfterStatusChange(fsAppointment);
        }
        finally
        {
          fsAppointment.CompleteActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list1;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CloseAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    List<FSAppointment> fsAppointmentList = new List<FSAppointment>();
    if (list.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      foreach (FSAppointment fsAppointment1 in list)
      {
        FSAppointment fsAppointment2 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Update(fsAppointment1);
        bool flag1 = GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Any<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (x => x.TrackTime.GetValueOrDefault()));
        try
        {
          if (flag1)
          {
            bool? nullable = fsAppointment2.TimeRegistered;
            bool flag2 = false;
            if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
            {
              nullable = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.RequireTimeApprovalToInvoice;
              if (nullable.GetValueOrDefault())
                throw new PXException("At least one staff time has not been approved.");
            }
          }
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            this.updateContractPeriod = true;
            this.SetServiceOrderStatusFromAppointment(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, fsAppointment2, AppointmentEntry.ActionButton.CloseAppointment);
            this.PerformTransition(fsAppointment2);
            this.SkipTaxCalcAndSave();
            transactionScope.Complete();
            this.RefreshServiceOrderRelated();
          }
          this.LoadServiceOrderRelatedAfterStatusChange(fsAppointment2);
          fsAppointmentList.Add(fsAppointment2);
        }
        finally
        {
          fsAppointment2.CloseActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) fsAppointmentList;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable UncloseAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (list.Count > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      foreach (FSAppointment fsAppointment1 in list)
      {
        FSAppointment fsAppointment2 = fsAppointment1;
        ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = fsAppointment2;
        try
        {
          fsAppointment2.UserConfirmedUnclosing = new bool?(true);
          if (!adapter.MassProcess)
            fsAppointment2.UserConfirmedUnclosing = 6 != ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Ask("Confirm Appointment Unclosing", this.UncloseDialogMessage, (MessageButtons) 4) ? new bool?(false) : new bool?(true);
          if (fsAppointment2.UserConfirmedUnclosing.GetValueOrDefault())
          {
            using (PXTransactionScope transactionScope = new PXTransactionScope())
            {
              fsAppointment2 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Update(fsAppointment2);
              this.SetServiceOrderStatusFromAppointment(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, fsAppointment2, AppointmentEntry.ActionButton.UnCloseAppointment);
              this.updateContractPeriod = true;
              this.PerformTransition(fsAppointment2);
              this.SkipTaxCalcAndSave();
              transactionScope.Complete();
              this.RefreshServiceOrderRelated();
            }
          }
        }
        finally
        {
          fsAppointment2.UnCloseActionRunning = new bool?(false);
        }
      }
    }
    return (IEnumerable) list;
  }

  [PXButton]
  [PXUIField]
  public IEnumerable InvoiceAppointment(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass122_0 displayClass1220 = new AppointmentEntry.\u003C\u003Ec__DisplayClass122_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1220.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1220.adapter = adapter;
    // ISSUE: reference to a compiler-generated field
    List<FSAppointment> list = displayClass1220.adapter.Get<FSAppointment>().ToList<FSAppointment>();
    // ISSUE: reference to a compiler-generated field
    displayClass1220.rows = new List<AppointmentToPost>();
    // ISSUE: reference to a compiler-generated field
    if (!displayClass1220.adapter.MassProcess)
      this.SaveWithRecalculateExternalTaxesSync();
    if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "SO")
      this.ValidateContact(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
    foreach (FSAppointment fsAppointment in list)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new AppointmentEntry.\u003C\u003Ec__DisplayClass122_1()
      {
        CS\u0024\u003C\u003E8__locals1 = displayClass1220,
        fsAppointmentRow = fsAppointment
      }, __methodptr(\u003CInvoiceAppointment\u003Eb__0)));
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenEmployeeBoard(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!adapter.MassProcess && list.Count<FSAppointment>() > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      throw PXRedirectToBoardRequiredException.GenerateMultiEmployeeRedirect(this.SiteMapProvider, this.GetAppointmentUrlArguments(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current), new MainAppointmentFilter()
      {
        InitialRefNbr = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.RefNbr
      }, (PXBaseRedirectException.WindowMode) 1);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenRoomBoard(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!adapter.MassProcess && list.Count<FSAppointment>() > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      throw new PXRedirectToBoardRequiredException("pages/fs/calendars/MultiRoomDispatch/FS300700.aspx", this.GetAppointmentUrlArguments(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current), (PXBaseRedirectException.WindowMode) 1);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenUserCalendar(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!adapter.MassProcess && list.Count<FSAppointment>() > 0)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      throw new PXRedirectToBoardRequiredException("pages/fs/calendars/SingleEmpDispatch/FS300400.aspx", this.GetAppointmentUrlArguments(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current), (PXBaseRedirectException.WindowMode) 1);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenSourceDocument(PXAdapter adapter)
  {
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    if (current != null && !string.IsNullOrEmpty(current.SourceType) && !string.IsNullOrEmpty(current.SourceRefNbr))
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
          PX.Objects.CR.CROpportunity crOpportunity = PXResultset<PX.Objects.CR.CROpportunity>.op_Implicit(PXSelectBase<PX.Objects.CR.CROpportunity, PXSelect<PX.Objects.CR.CROpportunity, Where<PX.Objects.CR.CROpportunity.opportunityID, Equal<Required<PX.Objects.CR.CROpportunity.opportunityID>>>>.Config>.Select((PXGraph) instance2, new object[1]
          {
            (object) current.SourceRefNbr
          }));
          if (crOpportunity != null)
          {
            ((PXSelectBase<PX.Objects.CR.CROpportunity>) instance2.Opportunity).Current = crOpportunity;
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
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void CreateNewCustomer()
  {
    CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
    ((PXSelectBase<PX.Objects.AR.Customer>) instance.CurrentCustomer).Insert(new PX.Objects.AR.Customer());
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable EmailConfirmationToStaffMember(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    foreach (FSAppointment fsAppointment in list)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment);
      this.SendNotification(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment, "NOTIFY STAFF", ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.BranchID);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable EmailConfirmationToCustomer(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    foreach (FSAppointment fsAppointment in list)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment);
      this.SendNotification(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment, "NOTIFY CUSTOMER", ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.BranchID);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable EmailConfirmationToGeoZoneStaff(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    foreach (FSAppointment fsAppointment in list)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment);
      this.SendNotification(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment, "NOTIFY SERVICE AREA STAFF", ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.BranchID);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable EmailSignedAppointment(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    foreach (FSAppointment fsAppointment in list)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment);
      List<Guid?> attachments = new List<Guid?>();
      if (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.CustomerSignedReport.HasValue)
        attachments.Add(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.CustomerSignedReport);
      this.SendNotification(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointment, "NOTIFY SIGNED APPOINTMENT", ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.BranchID, (IList<Guid?>) attachments);
    }
    return (IEnumerable) list;
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField]
  public virtual void openPostingDocument()
  {
    FSPostDet fsPostDet = PXResultset<FSPostDet>.op_Implicit(((PXSelectBase<FSPostDet>) this.AppointmentPostedIn).SelectWindowed(0, 1, Array.Empty<object>()));
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
      if (fsPostDet.ARPosted.GetValueOrDefault() && !((PXGraph) this).IsMobile)
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
      if (!fsPostDet.INPosted.GetValueOrDefault())
        return;
      if (fsPostDet.INDocType.Trim() == "R")
      {
        INReceiptEntry instance = PXGraph.CreateInstance<INReceiptEntry>();
        ((PXSelectBase<PX.Objects.IN.INRegister>) instance.receipt).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) instance.receipt).Search<PX.Objects.IN.INRegister.refNbr>((object) fsPostDet.INRefNbr, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      INIssueEntry instance1 = PXGraph.CreateInstance<INIssueEntry>();
      ((PXSelectBase<PX.Objects.IN.INRegister>) instance1.issue).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) instance1.issue).Search<PX.Objects.IN.INRegister.refNbr>((object) fsPostDet.INRefNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
      ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException1;
    }
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField]
  public virtual IEnumerable openBillDocument(PXAdapter adapter)
  {
    FSBillHistory current = ((PXSelectBase<FSBillHistory>) this.InvoiceRecords).Current;
    if (current != null)
    {
      if (current.ChildEntityType == "PXSO" && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      {
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) current.ChildRefNbr, new object[1]
        {
          (object) current.ChildDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.ChildEntityType == "PXAR")
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.ChildRefNbr, new object[1]
        {
          (object) current.ChildDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.ChildEntityType == "PXSI")
      {
        SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.ChildRefNbr, new object[1]
        {
          (object) current.ChildDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.ChildEntityType == "PXAP")
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) current.ChildRefNbr, new object[1]
        {
          (object) current.ChildDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.ChildEntityType == "PXPM")
      {
        RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
        ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance.Document).Search<PMRegister.refNbr>((object) current.ChildRefNbr, new object[1]
        {
          (object) current.ChildDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.ChildEntityType == "PXIR")
      {
        INReceiptEntry instance = PXGraph.CreateInstance<INReceiptEntry>();
        ((PXSelectBase<PX.Objects.IN.INRegister>) instance.receipt).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) instance.receipt).Search<PX.Objects.IN.INRegister.refNbr>((object) current.ChildRefNbr, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (current.ChildEntityType == "PXIS")
      {
        INIssueEntry instance = PXGraph.CreateInstance<INIssueEntry>();
        ((PXSelectBase<PX.Objects.IN.INRegister>) instance.issue).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) instance.issue).Search<PX.Objects.IN.INRegister.refNbr>((object) current.ChildRefNbr, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable PrintAppointmentReport(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!adapter.MassProcess)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      string fieldName1 = SharedFunctions.GetFieldName<FSAppointment.srvOrdType>();
      string fieldName2 = SharedFunctions.GetFieldName<FSAppointment.refNbr>();
      dictionary[fieldName1] = list[0].SrvOrdType;
      dictionary[fieldName2] = list[0].RefNbr;
      throw new PXReportRequiredException(dictionary, "FS642000", string.Empty, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  public virtual IEnumerable PrintServiceTimeActivityReport(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!adapter.MassProcess)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      string fieldName1 = SharedFunctions.GetFieldName<FSAppointment.srvOrdType>();
      string fieldName2 = SharedFunctions.GetFieldName<FSAppointment.refNbr>();
      string fieldName3 = SharedFunctions.GetFieldName<FSAppointment.soRefNbr>();
      string str1 = "DateFrom";
      string str2 = "DateTo";
      dictionary[fieldName1] = list[0].SrvOrdType;
      dictionary[fieldName2] = list[0].RefNbr;
      dictionary[fieldName3] = list[0].SORefNbr;
      string key1 = str1;
      DateTime? executionDate = list[0].ExecutionDate;
      string str3 = executionDate.ToString();
      dictionary[key1] = str3;
      string key2 = str2;
      executionDate = list[0].ExecutionDate;
      string str4 = executionDate.ToString();
      dictionary[key2] = str4;
      throw new PXReportRequiredException(dictionary, "FS654500", string.Empty, (CurrentLocalization) null);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable StartItemLine(PXAdapter adapter)
  {
    FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
    if (current != null && !this.CanLogBeStarted(current))
      throw new PXException("The {0} action is not allowed for the {1} line with the {2} status. To perform this action, the appointment must have the In Process status or the selected detail must be a travel item.", new object[3]
      {
        (object) PXLocalizer.Localize(((PXAction) this.startItemLine).GetCaption()),
        (object) this.GetLineDisplayHint((PXGraph) this, current.LineRef, current.TranDesc, current.InventoryID),
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointmentDet.status>(((PXSelectBase) this.AppointmentDetails).Cache, (object) current, current.Status)
      });
    if (current != null && (current.LineType == "SLPRO" || current.LineType == "CM_LN" || current.LineType == "IT_LN"))
    {
      this.ChangeItemLineStatus(current, "IP");
      ((PXGraph) this).Actions.PressSave();
    }
    else
      this.LogActionBase(adapter, "ST", PXLocalizer.Localize(((PXAction) this.startItemLine).GetCaption()), ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current, (PXSelectBase<FSAppointmentLog>) null);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable PauseItemLine(PXAdapter adapter)
  {
    FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
    if (current != null && !this.CanLogBePaused(current))
      throw new PXException("The {0} action is not allowed for the {1} line with the {2} status. To perform this action, a selected line must have the In Process status and the selected detail must be a service, travel or non-stock item.", new object[3]
      {
        (object) PXLocalizer.Localize(((PXAction) this.pauseItemLine).GetCaption()),
        (object) this.GetLineDisplayHint((PXGraph) this, current.LineRef, current.TranDesc, current.InventoryID),
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointmentDet.status>(((PXSelectBase) this.AppointmentDetails).Cache, (object) current, current.Status)
      });
    if (current != null && (current.LineType == "SLPRO" || current.LineType == "CM_LN" || current.LineType == "IT_LN"))
      return adapter.Get();
    PXSelectBase<FSAppointmentLog> logSelect = (PXSelectBase<FSAppointmentLog>) null;
    object[] objArray = (object[]) null;
    if (this.GetLogType(current) == "NS")
    {
      logSelect = (PXSelectBase<FSAppointmentLog>) new PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.detLineRef, Equal<Required<FSAppointmentLog.detLineRef>>, And<FSAppointmentLog.itemType, Equal<ListField_Log_ItemType.nonStock>, And<FSAppointmentLog.status, Equal<ListField_Status_Log.InProcess>>>>>>((PXGraph) this);
      objArray = new object[2]
      {
        (object) (int?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.AppointmentID,
        (object) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current?.LineRef
      };
    }
    this.LogActionBase(adapter, "PA", PXLocalizer.Localize(((PXAction) this.pauseItemLine).GetCaption()), current, logSelect, objArray);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ResumeItemLine(PXAdapter adapter)
  {
    FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
    if (current != null && !this.CanLogBeResumed(current))
      throw new PXException("The {0} action is not allowed for the {1} line with the {2} status. To perform this action, a selected line must have the In Process status and the document must have the In Process status.", new object[3]
      {
        (object) PXLocalizer.Localize(((PXAction) this.resumeItemLine).GetCaption()),
        (object) this.GetLineDisplayHint((PXGraph) this, current.LineRef, current.TranDesc, current.InventoryID),
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointmentDet.status>(((PXSelectBase) this.AppointmentDetails).Cache, (object) current, current.Status)
      });
    if (current != null && (current.LineType == "SLPRO" || current.LineType == "CM_LN" || current.LineType == "IT_LN"))
      return adapter.Get();
    PXSelectBase<FSAppointmentLog> logSelect = (PXSelectBase<FSAppointmentLog>) null;
    object[] objArray = (object[]) null;
    if (this.GetLogType(current) == "NS")
    {
      logSelect = (PXSelectBase<FSAppointmentLog>) new PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.detLineRef, Equal<Required<FSAppointmentLog.detLineRef>>, And<FSAppointmentLog.itemType, Equal<ListField_Log_ItemType.nonStock>, And<FSAppointmentLog.status, Equal<ListField_Status_Log.Paused>>>>>>((PXGraph) this);
      objArray = new object[2]
      {
        (object) (int?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.AppointmentID,
        (object) current.LineRef
      };
    }
    this.LogActionBase(adapter, "RE", PXLocalizer.Localize(((PXAction) this.resumeItemLine).GetCaption()), current, logSelect, objArray);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CompleteItemLine(PXAdapter adapter)
  {
    FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
    if (current != null && !this.CanItemLineBeCompleted(current))
      throw new PXException("The {0} action is not allowed for the {1} line with the {2} status. To perform this action, a selected line must have the In Process status and the document must have the In Process or Paused status.", new object[3]
      {
        (object) PXLocalizer.Localize(((PXAction) this.completeItemLine).GetCaption()),
        (object) this.GetLineDisplayHint((PXGraph) this, current.LineRef, current.TranDesc, current.InventoryID),
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointmentDet.status>(((PXSelectBase) this.AppointmentDetails).Cache, (object) current, current.Status)
      });
    if (current != null && (current.LineType == "SLPRO" || current.LineType == "CM_LN" || current.LineType == "IT_LN"))
    {
      this.ChangeItemLineStatus(current, "CP");
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      PXSelectBase<FSAppointmentLog> logSelect = (PXSelectBase<FSAppointmentLog>) null;
      object[] objArray = (object[]) null;
      if (this.GetLogType(((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current) == "NS")
      {
        logSelect = (PXSelectBase<FSAppointmentLog>) new PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>, And<FSAppointmentLog.detLineRef, Equal<Required<FSAppointmentLog.detLineRef>>, And<FSAppointmentLog.itemType, Equal<ListField_Log_ItemType.nonStock>, And<Where<FSAppointmentLog.status, Equal<ListField_Status_Log.InProcess>, Or<FSAppointmentLog.status, Equal<ListField_Status_Log.Paused>>>>>>>>((PXGraph) this);
        objArray = new object[2]
        {
          (object) (int?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.AppointmentID,
          (object) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current?.LineRef
        };
      }
      this.LogActionBase(adapter, "CP", PXLocalizer.Localize(((PXAction) this.completeItemLine).GetCaption()), ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current, logSelect, objArray);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CancelItemLine(PXAdapter adapter)
  {
    FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
    if (current == null)
      return adapter.Get();
    this.ChangeItemLineStatus(current, "CC");
    ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  public virtual void LogActionBase(
    PXAdapter adapter,
    string logActionID,
    string logActionLabel,
    FSAppointmentDet apptDet,
    PXSelectBase<FSAppointmentLog> logSelect,
    params object[] logSelectArgs)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass159_0 displayClass1590 = new AppointmentEntry.\u003C\u003Ec__DisplayClass159_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1590.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1590.logActionID = logActionID;
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current == null)
      return;
    bool flag1 = true;
    // ISSUE: reference to a compiler-generated field
    displayClass1590.logType = this.GetLogType(((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current);
    // ISSUE: reference to a compiler-generated field
    if (displayClass1590.logType == "NS")
    {
      flag1 = false;
      ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type = "NS";
      ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
    }
    else
      apptDet = (FSAppointmentDet) null;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.VerifyLogActionWithAppointmentStatus(displayClass1590.logActionID, logActionLabel, displayClass1590.logType, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
    WebDialogResult webDialogResult = (WebDialogResult) 0;
    bool flag2 = false;
    if (flag1)
    {
      // ISSUE: reference to a compiler-generated field
      if (displayClass1590.logActionID == "ST")
      {
        // ISSUE: reference to a compiler-generated field
        if ("SA" == displayClass1590.logType)
        {
          // ISSUE: method pointer
          flag2 = this.LogActionStartStaffFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1590, __methodptr(\u003CLogActionBase\u003Eb__0)), (DialogAnswerType) 1, true);
          webDialogResult = ((PXSelectBase) this.LogActionStartStaffFilter).View.Answer;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if ("SB" == displayClass1590.logType)
          {
            // ISSUE: method pointer
            flag2 = this.LogActionStartServiceFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1590, __methodptr(\u003CLogActionBase\u003Eb__1)), (DialogAnswerType) 1, true);
            webDialogResult = ((PXSelectBase) this.LogActionStartServiceFilter).View.Answer;
          }
          else
          {
            // ISSUE: method pointer
            flag2 = this.LogActionStartFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1590, __methodptr(\u003CLogActionBase\u003Eb__2)), (DialogAnswerType) 1, true);
            webDialogResult = ((PXSelectBase) this.LogActionStartFilter).View.Answer;
          }
        }
      }
      else
      {
        // ISSUE: method pointer
        flag2 = this.LogActionPCRFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1590, __methodptr(\u003CLogActionBase\u003Eb__3)), (DialogAnswerType) 1, true);
        webDialogResult = ((PXSelectBase) this.LogActionPCRFilter).View.Answer;
      }
    }
    if (flag1 && (!flag2 || webDialogResult != 1 && webDialogResult != 6))
      return;
    // ISSUE: reference to a compiler-generated field
    this.RunLogAction(displayClass1590.logActionID, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, apptDet, logSelect, logSelectArgs);
  }

  public virtual string GetDfltLogTypeForStaffAction(
    FSAppointmentEmployee staffRow,
    string defaultLogType)
  {
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    if (current == null || current.Status == null)
      return (string) null;
    bool? nullable = current.NotStarted;
    if (!nullable.GetValueOrDefault())
    {
      nullable = current.Completed;
      if (!nullable.GetValueOrDefault() && (staffRow == null || string.IsNullOrEmpty(staffRow.ServiceLineRef) || !(this.GetLogTypeCheckingTravelWithLogFormula(((PXSelectBase) this.LogRecords).Cache, (FSAppointmentDet) PXSelectorAttribute.Select<FSAppointmentEmployee.serviceLineRef>(((PXSelectBase) this.AppointmentServiceEmployees).Cache, (object) staffRow)) == "TR")))
        return defaultLogType;
    }
    return "TR";
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable StartStaff(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass162_0 displayClass1620 = new AppointmentEntry.\u003C\u003Ec__DisplayClass162_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1620.\u003C\u003E4__this = this;
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current != null)
    {
      // ISSUE: reference to a compiler-generated field
      displayClass1620.logType = ((PXGraph) this).IsMobile ? "SA" : this.GetDfltLogTypeForStaffAction(((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current, "SA");
      // ISSUE: reference to a compiler-generated field
      this.VerifyLogActionWithAppointmentStatus("ST", PXLocalizer.Localize(((PXAction) this.startStaff).GetCaption()), displayClass1620.logType, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
      bool flag;
      WebDialogResult answer;
      // ISSUE: reference to a compiler-generated field
      if ("SA" == displayClass1620.logType)
      {
        if (((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current != null && !string.IsNullOrEmpty(((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.ServiceLineRef))
        {
          FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.lineRef, Equal<Required<FSAppointmentDet.lineRef>>, And<FSAppointmentDet.appointmentID, Equal<Current<FSAppointmentDet.appointmentID>>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.ServiceLineRef
          }));
          if (fsAppointmentDet != null && fsAppointmentDet.IsTravelItem.GetValueOrDefault())
            throw new PXException(PXMessages.LocalizeFormatNoPrefix("The {0} action is not available for a staff line associated with travel items.", new object[1]
            {
              (object) ((PXAction) this.startStaff).GetCaption()
            }));
        }
        // ISSUE: method pointer
        flag = this.LogActionStartStaffFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1620, __methodptr(\u003CStartStaff\u003Eb__0)), (DialogAnswerType) 1, true);
        answer = ((PXSelectBase) this.LogActionStartStaffFilter).View.Answer;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if ("SB" == displayClass1620.logType)
        {
          // ISSUE: method pointer
          flag = this.LogActionStartServiceFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1620, __methodptr(\u003CStartStaff\u003Eb__1)), (DialogAnswerType) 1, true);
          answer = ((PXSelectBase) this.LogActionStartServiceFilter).View.Answer;
        }
        else
        {
          // ISSUE: method pointer
          flag = this.LogActionStartFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1620, __methodptr(\u003CStartStaff\u003Eb__2)), (DialogAnswerType) 1, true);
          answer = ((PXSelectBase) this.LogActionStartFilter).View.Answer;
        }
      }
      if (flag && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable PauseStaff(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass164_0 displayClass1640 = new AppointmentEntry.\u003C\u003Ec__DisplayClass164_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1640.\u003C\u003E4__this = this;
    if (((PXSelectBase<FSLogActionPCRFilter>) this.LogActionPCRFilter).Current != null)
    {
      // ISSUE: reference to a compiler-generated field
      displayClass1640.logType = this.GetDfltLogTypeForStaffAction(((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current, "SE");
      // ISSUE: method pointer
      int num = this.LogActionPCRFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1640, __methodptr(\u003CPauseStaff\u003Eb__0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionPCRFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ResumeStaff(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass166_0 displayClass1660 = new AppointmentEntry.\u003C\u003Ec__DisplayClass166_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1660.\u003C\u003E4__this = this;
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current != null)
    {
      // ISSUE: reference to a compiler-generated field
      displayClass1660.logType = this.GetDfltLogTypeForStaffAction(((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current, "SE");
      // ISSUE: reference to a compiler-generated field
      this.VerifyLogActionWithAppointmentStatus("RE", PXLocalizer.Localize(((PXAction) this.resumeStaff).GetCaption()), displayClass1660.logType, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
      // ISSUE: method pointer
      int num = this.LogActionPCRFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1660, __methodptr(\u003CResumeStaff\u003Eb__0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionPCRFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CompleteStaff(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass168_0 displayClass1680 = new AppointmentEntry.\u003C\u003Ec__DisplayClass168_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1680.\u003C\u003E4__this = this;
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current != null)
    {
      // ISSUE: reference to a compiler-generated field
      displayClass1680.logType = this.GetDfltLogTypeForStaffAction(((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current, "SE");
      // ISSUE: reference to a compiler-generated field
      this.VerifyLogActionWithAppointmentStatus("CP", PXLocalizer.Localize(((PXAction) this.completeStaff).GetCaption()), displayClass1680.logType, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
      // ISSUE: method pointer
      int num = this.LogActionPCRFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1680, __methodptr(\u003CCompleteStaff\u003Eb__0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionPCRFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable StartAssignedStaff(PXAdapter adapter)
  {
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current != null)
    {
      if (((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current != null)
      {
        FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
        if ((current != null ? (current.IsTravelItem.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("The {0} action is not available for travel items.", new object[1]
          {
            (object) ((PXAction) this.startAssignedStaff).GetCaption()
          }));
      }
      this.VerifyLogActionWithAppointmentStatus("ST", PXLocalizer.Localize(((PXAction) this.startAssignedStaff).GetCaption()), "SB", ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
      // ISSUE: method pointer
      int num = this.LogActionStartServiceFilter.AskExtFullyValid(new PXView.InitializePanel((object) this, __methodptr(\u003CStartAssignedStaff\u003Eb__170_0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionStartServiceFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable DepartStaff(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass172_0 displayClass1720 = new AppointmentEntry.\u003C\u003Ec__DisplayClass172_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1720.\u003C\u003E4__this = this;
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current != null)
    {
      // ISSUE: reference to a compiler-generated field
      displayClass1720.logType = "TR";
      // ISSUE: method pointer
      int num = this.LogActionStartFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1720, __methodptr(\u003CDepartStaff\u003Eb__0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionStartFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ArriveStaff(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass174_0 displayClass1740 = new AppointmentEntry.\u003C\u003Ec__DisplayClass174_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1740.\u003C\u003E4__this = this;
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current != null)
    {
      // ISSUE: reference to a compiler-generated field
      displayClass1740.logType = "TR";
      // ISSUE: reference to a compiler-generated field
      this.VerifyLogActionWithAppointmentStatus("CP", PXLocalizer.Localize(((PXAction) this.completeStaff).GetCaption()), displayClass1740.logType, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
      // ISSUE: method pointer
      int num = this.LogActionPCRFilter.AskExtFullyValid(new PXView.InitializePanel((object) displayClass1740, __methodptr(\u003CArriveStaff\u003Eb__0)), (DialogAnswerType) 1, true) ? 1 : 0;
      WebDialogResult answer = ((PXSelectBase) this.LogActionPCRFilter).View.Answer;
      if (num != 0 && (answer == 1 || answer == 6))
        this.RunLogAction(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type, (FSAppointmentDet) null, (PXSelectBase<FSAppointmentLog>) null, (object[]) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual void ViewPayment()
  {
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null && ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current != null && ((PXSelectBase<ARPayment>) this.Adjustments).Current != null)
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
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null && ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current != null)
    {
      ((PXAction) this.Save).Press();
      PXGraph target;
      this.CreatePrepaymentDocument(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, out target, "PPM");
      throw new PXPopupRedirectException(target, "New Payment", true);
    }
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField]
  public IEnumerable QuickProcessMobile(PXAdapter adapter)
  {
    if (((PXSelectBase) this.AppointmentRecords).Cache.GetStatus((object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current) != 2)
    {
      FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
      current.IsCalledFromQuickProcess = new bool?(true);
      AppointmentEntry.AppointmentQuickProcess.InitQuickProcessPanel((PXGraph) this, "");
      ((PXSelectBase) this.AppointmentQuickProcessExt.QuickProcessParameters).Cache.RaiseRowSelected((object) ((PXSelectBase<FSAppQuickProcessParams>) this.AppointmentQuickProcessExt.QuickProcessParameters).Current);
      PXQuickProcess.Start<AppointmentEntry, FSAppointment, FSAppQuickProcessParams>(this, current, ((PXSelectBase<FSAppQuickProcessParams>) this.AppointmentQuickProcessExt.QuickProcessParameters).Current);
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  protected virtual void OpenScheduleScreen()
  {
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null)
      return;
    if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.Behavior == "RO")
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ScheduleID, new object[2]
      {
        (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ServiceContractID,
        (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.CustomerID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    ServiceContractScheduleEntry instance1 = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
    ((PXSelectBase<FSContractSchedule>) instance1.ContractScheduleRecords).Current = PXResultset<FSContractSchedule>.op_Implicit(((PXSelectBase<FSContractSchedule>) instance1.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ScheduleID, new object[2]
    {
      (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ServiceContractID,
      (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.CustomerID
    }));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable AddReceipt(PXAdapter adapter)
  {
    FSAppointment current1 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    FSServiceOrder current2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    FSSrvOrdType current3 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current1 != null && current2 != null)
    {
      ((PXAction) this.Save).Press();
      ExpenseClaimDetailEntry instance = PXGraph.CreateInstance<ExpenseClaimDetailEntry>();
      EPExpenseClaimDetails expenseClaimDetails1 = ((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Insert((EPExpenseClaimDetails) ((PXSelectBase) instance.ClaimDetails).Cache.CreateInstance());
      expenseClaimDetails1.ExpenseDate = current1.ExecutionDate;
      expenseClaimDetails1.BranchID = current1.BranchID;
      expenseClaimDetails1.CustomerID = current2.BillCustomerID;
      expenseClaimDetails1.CustomerLocationID = current2.BillLocationID;
      expenseClaimDetails1.ContractID = current1.ProjectID;
      expenseClaimDetails1.TaskID = current1.DfltProjectTaskID;
      if (current3 != null && !ProjectDefaultAttribute.IsNonProject(current1.ProjectID) && PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
        expenseClaimDetails1.CostCodeID = current3.DfltCostCodeID;
      EPExpenseClaimDetails expenseClaimDetails2 = ((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Update(expenseClaimDetails1);
      ((PXSelectBase) instance.ClaimDetails).Cache.GetExtension<FSxEPExpenseClaimDetails>((object) expenseClaimDetails2);
      ((PXSelectBase) instance.ClaimDetails).Cache.SetValueExt<FSxEPExpenseClaimDetails.fsEntityTypeUI>((object) expenseClaimDetails2, (object) "PX.Objects.FS.FSAppointment");
      ((PXSelectBase) instance.ClaimDetails).Cache.SetValueExt<FSxEPExpenseClaimDetails.fsEntityNoteID>((object) expenseClaimDetails2, (object) current1.NoteID);
      ((PXSelectBase<EPExpenseClaimDetails>) instance.ClaimDetails).Update(expenseClaimDetails2);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable AddBill(PXAdapter adapter)
  {
    FSAppointment current1 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    FSServiceOrder current2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    FSSrvOrdType current3 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current1 != null && current2 != null)
    {
      ((PXAction) this.Save).Press();
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      PX.Objects.AP.APInvoice apInvoice = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Insert((PX.Objects.AP.APInvoice) ((PXSelectBase) instance.Document).Cache.CreateInstance());
      SM_APInvoiceEntry extension = ((PXGraph) instance).GetExtension<SM_APInvoiceEntry>();
      apInvoice.BranchID = current1.BranchID;
      apInvoice.DocDate = current1.ExecutionDate;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedEntityType = "PX.Objects.FS.FSAppointment";
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocNoteID = current1.NoteID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocDate = current1.ExecutionDate;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocCustomerID = current1.CustomerID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocCustomerLocationID = current2.LocationID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocProjectID = current2.ProjectID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocProjectTaskID = current2.DfltProjectTaskID;
      ((PXSelectBase<CreateAPFilter>) extension.apFilter).Current.RelatedDocCostCodeID = (int?) current3?.DfltCostCodeID;
      ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Update(apInvoice);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreatePurchaseOrder(PXAdapter adapter)
  {
    List<FSAppointment> list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    if (!adapter.MassProcess)
    {
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, list[0]);
      POCreate instance = PXGraph.CreateInstance<POCreate>();
      FSxPOCreateFilter extension = ((PXSelectBase) instance.Filter).Cache.GetExtension<FSxPOCreateFilter>((object) ((PXSelectBase<POCreate.POCreateFilter>) instance.Filter).Current);
      extension.SrvOrdType = list[0].SrvOrdType;
      extension.ServiceOrderRefNbr = list[0].SORefNbr;
      throw new PXRedirectRequiredException((PXGraph) instance, (string) null);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CreatePurchaseOrderMobile(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass191_0 displayClass1910 = new AppointmentEntry.\u003C\u003Ec__DisplayClass191_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1910.list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    // ISSUE: reference to a compiler-generated field
    if (!adapter.MassProcess && displayClass1910.list.Count > 0)
    {
      // ISSUE: reference to a compiler-generated field
      this.SaveBeforeApplyAction(((PXSelectBase) this.AppointmentRecords).Cache, displayClass1910.list[0]);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1910, __methodptr(\u003CCreatePurchaseOrderMobile\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1910.list;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable BillReversal(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass193_0 displayClass1930 = new AppointmentEntry.\u003C\u003Ec__DisplayClass193_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1930.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1930.list = adapter.Get<FSAppointment>().ToList<FSAppointment>();
    // ISSUE: reference to a compiler-generated field
    if (!adapter.MassProcess && displayClass1930.list.Count > 0)
    {
      this.SaveWithRecalculateExternalTaxesSync();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1930, __methodptr(\u003CBillReversal\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1930.list;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddNewContact(PXAdapter adapter)
  {
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.CustomerID.HasValue)
    {
      ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
      ((PXGraph) instance).Clear();
      PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Insert();
      contact.BAccountID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.CustomerID;
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

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public IEnumerable invoiceRecords()
  {
    return (IEnumerable) ((IEnumerable<PXResult<FSBillHistory>>) PXSelectBase<FSBillHistory, PXViewOf<FSBillHistory>.BasedOn<SelectFromBase<FSBillHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSBillHistory.srvOrdType, Equal<BqlField<FSAppointment.srvOrdType, IBqlString>.FromCurrent>>>>, And<BqlOperand<FSBillHistory.serviceOrderRefNbr, IBqlString>.IsEqual<BqlField<FSAppointment.soRefNbr, IBqlString>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSBillHistory.appointmentRefNbr, Equal<BqlField<FSAppointment.refNbr, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<FSBillHistory.appointmentRefNbr, IBqlString>.IsNull>>>.Order<By<BqlField<FSBillHistory.createdDateTime, IBqlDateTime>.Desc>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AsEnumerable<PXResult<FSBillHistory>>().Where<PXResult<FSBillHistory>>((Func<PXResult<FSBillHistory>, bool>) (rec => !((PXResult) rec).GetItem<FSBillHistory>().IsChildDocDeleted.GetValueOrDefault())).ToList<PXResult<FSBillHistory>>();
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  [InjectDependency]
  protected IReportLoaderService ReportLoader { get; private set; }

  [InjectDependency]
  protected IReportRenderer ReportRenderer { get; private set; }

  [InjectDependency]
  protected IReportDataBinder ReportDataBinder { get; private set; }

  [InjectDependency]
  protected Func<string, ReportNotificationGenerator> ReportNotificationGeneratorFactory { get; private set; }

  public virtual void MyPersist()
  {
    this.serviceOrderIsAlreadyUpdated = false;
    this.CatchedServiceOrderUpdateException = (Exception) null;
    this.serviceOrderRowPersistedPassedWithStatusAbort = false;
    this.insertingServiceOrder = false;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    this.persistContext = true;
    try
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        try
        {
          ((PXGraph) this).Persist(typeof (FSServiceOrder), (PXDBOperation) 2);
          ((PXGraph) this).Persist(typeof (FSServiceOrder), (PXDBOperation) 1);
        }
        catch
        {
          ((PXGraph) this).Caches[typeof (FSServiceOrder)].Persisted(true);
          throw;
        }
        try
        {
          bool? nullable;
          if (this.RecalculateExternalTaxesSync && current != null)
          {
            nullable = current.SkipExternalTaxCalculation;
            bool flag = false;
            if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
            {
              this.CalculateExternalTax(current);
              ((PXGraph) this).SelectTimeStamp();
            }
          }
          if (!this.SkipServiceOrderUpdate)
            this.SplitAppoinmentLogLinesByDays();
          ((PXGraph) this).Persist();
          if (!this.RecalculateExternalTaxesSync && current != null)
          {
            nullable = current.SkipExternalTaxCalculation;
            bool flag = false;
            if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
              this.RecalculateExternalTaxes();
          }
          if (current != null)
          {
            nullable = current.TrackTimeChanged;
            if (nullable.GetValueOrDefault())
              this.ValidateTrackTimeField();
          }
        }
        catch
        {
          if (!this.serviceOrderRowPersistedPassedWithStatusAbort)
            ((PXGraph) this).Caches[typeof (FSServiceOrder)].Persisted(true);
          throw;
        }
        transactionScope.Complete();
      }
    }
    finally
    {
      this.serviceOrderIsAlreadyUpdated = false;
      this.CatchedServiceOrderUpdateException = (Exception) null;
      this.serviceOrderRowPersistedPassedWithStatusAbort = false;
      this.insertingServiceOrder = false;
      this.persistContext = false;
    }
  }

  private void ValidateTrackTimeField()
  {
    foreach (FSAppointmentLog fsAppointmentLogRow in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (x =>
    {
      bool? trackTime = x.TrackTime;
      bool flag = false;
      return trackTime.GetValueOrDefault() == flag & trackTime.HasValue && x.BAccountID.HasValue && x.BAccountType == "EP";
    })))
    {
      PX.Objects.CR.Standalone.EPEmployee tmEmployee = this.FindTMEmployee((PXGraph) this, fsAppointmentLogRow.BAccountID);
      EPActivityApprove epActivityApprove = this.FindEPActivityApprove((PXGraph) this, fsAppointmentLogRow, tmEmployee);
      if (epActivityApprove != null && !this.ValidateInsertUpdateTimeActivity(epActivityApprove))
        throw new PXException("{0} cannot be updated because this record is related to a released or approved time activity.", new object[1]
        {
          (object) "trackTime"
        });
    }
  }

  private void PerformTransition(FSAppointment apptRow)
  {
    ((SelectedEntityEvent<FSAppointment>) PXEntityEventBase<FSAppointment>.Container<FSAppointment.Events>.Select((Expression<Func<FSAppointment.Events, PXEntityEvent<FSAppointment.Events>>>) (ev => ev.AppointmentStatusChanged))).FireOn((PXGraph) this, apptRow);
  }

  public virtual void Persist() => this.MyPersist();

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<FSAppointment>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (FSAppointmentDet), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<FSAppointmentDet.srvOrdType>((PXDbType) 3, (object) ((PXSelectBase<FSAppointment>) ((AppointmentEntry) graph).AppointmentRecords).Current?.SrvOrdType),
        (PXDataFieldValue) new PXDataFieldValue<FSAppointmentDet.refNbr>((object) ((PXSelectBase<FSAppointment>) ((AppointmentEntry) graph).AppointmentRecords).Current?.RefNbr)
      }))
    });
  }

  protected virtual IEnumerable treeWFStages([PXInt] int? wFStageID)
  {
    return ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current == null ? (IEnumerable) null : TreeWFStageHelper.treeWFStages((PXGraph) this, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.SrvOrdType, wFStageID);
  }

  /// <summary>
  /// Gets the license types related for the given appointment services. Also sets a list with the License Type identifiers
  /// related to the appointment services.
  /// </summary>
  /// <param name="bqlResultSet">Set of appointment detail services.</param>
  /// <param name="serviceLicenseIDs">This list contains the union of all license types related to the given appointment services.</param>
  /// <returns>List of services with their respective related license types.</returns>
  public virtual List<AppointmentEntry.ServiceRequirement> GetAppointmentDetServiceRowLicenses(
    List<FSAppointmentDet> appointmentServiceDetails,
    ref List<int?> serviceLicenseIDs)
  {
    List<AppointmentEntry.ServiceRequirement> source = new List<AppointmentEntry.ServiceRequirement>();
    List<object> objectList1 = new List<object>();
    BqlCommand bqlCommand = ((BqlCommand) new Select2<FSServiceLicenseType, InnerJoin<PX.Objects.IN.InventoryItem, On<FSServiceLicenseType.serviceID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<True, Equal<True>>>()).WhereAnd(InHelper<PX.Objects.IN.InventoryItem.inventoryID>.Create(appointmentServiceDetails.Count));
    foreach (FSAppointmentDet appointmentServiceDetail in appointmentServiceDetails)
      objectList1.Add((object) appointmentServiceDetail.InventoryID);
    List<object> objectList2 = new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList1.ToArray());
    if (objectList2.Count == 0)
      return source;
    foreach (PXResult<FSServiceLicenseType, PX.Objects.IN.InventoryItem> pxResult in objectList2)
    {
      PX.Objects.IN.InventoryItem fsServiceRow = PXResult<FSServiceLicenseType, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      FSServiceLicenseType serviceLicenseType = PXResult<FSServiceLicenseType, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      serviceLicenseIDs.Add(serviceLicenseType.LicenseTypeID);
      AppointmentEntry.ServiceRequirement serviceRequirement1 = source.Where<AppointmentEntry.ServiceRequirement>((Func<AppointmentEntry.ServiceRequirement, bool>) (list =>
      {
        int serviceId = list.serviceID;
        int? inventoryId = fsServiceRow.InventoryID;
        int valueOrDefault = inventoryId.GetValueOrDefault();
        return serviceId == valueOrDefault & inventoryId.HasValue;
      })).FirstOrDefault<AppointmentEntry.ServiceRequirement>();
      int? nullable1;
      if (serviceRequirement1 != null)
      {
        List<int?> requirementIdList = serviceRequirement1.requirementIDList;
        nullable1 = serviceLicenseType.LicenseTypeID;
        int? nullable2 = new int?(nullable1.Value);
        requirementIdList.Add(nullable2);
      }
      else
      {
        AppointmentEntry.ServiceRequirement serviceRequirement2 = new AppointmentEntry.ServiceRequirement();
        nullable1 = fsServiceRow.InventoryID;
        serviceRequirement2.serviceID = nullable1.Value;
        AppointmentEntry.ServiceRequirement serviceRequirement3 = serviceRequirement2;
        serviceRequirement3.requirementIDList.Add(serviceLicenseType.LicenseTypeID);
        source.Add(serviceRequirement3);
      }
    }
    return source;
  }

  public virtual List<int?> GetAppointmentEmpoyeeLicenseIDs(
    PXResultset<FSAppointmentEmployee> bqlResultSet)
  {
    List<int?> empoyeeLicenseIds = new List<int?>();
    List<object> objectList = new List<object>();
    DateTime dateTime1;
    ref DateTime local = ref dateTime1;
    DateTime dateTime2 = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.ScheduledDateTimeBegin.Value;
    int year = dateTime2.Year;
    dateTime2 = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.ScheduledDateTimeBegin.Value;
    int month = dateTime2.Month;
    dateTime2 = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.ScheduledDateTimeBegin.Value;
    int day = dateTime2.Day;
    local = new DateTime(year, month, day);
    BqlCommand bqlCommand1 = (BqlCommand) new Select4<FSLicense, Where<FSLicense.expirationDate, GreaterEqual<Required<FSAppointment.scheduledDateTimeBegin>>, Or<FSLicense.expirationDate, IsNull>>, Aggregate<GroupBy<FSLicense.licenseTypeID>>, OrderBy<Asc<FSLicense.licenseID>>>();
    objectList.Add((object) dateTime1);
    BqlCommand bqlCommand2 = bqlCommand1.WhereAnd(InHelper<FSLicense.employeeID>.Create(bqlResultSet.Count));
    foreach (PXResult<FSAppointmentEmployee> bqlResult in bqlResultSet)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(bqlResult);
      objectList.Add((object) appointmentEmployee.EmployeeID);
    }
    foreach (FSLicense fsLicense in new PXView((PXGraph) this, true, bqlCommand2).SelectMulti(objectList.ToArray()))
      empoyeeLicenseIds.Add(fsLicense.LicenseTypeID);
    return empoyeeLicenseIds;
  }

  /// <summary>
  /// Gets the skills related for the given appointment services. Also sets a list with the skills identifiers
  /// related to the appointment services.
  /// </summary>
  /// <param name="bqlResultSet">Set of appointment detail services.</param>
  /// <param name="serviceSkillIDs">This list contains the union of all skills related to the given appointment services.</param>
  /// <returns>List of services with their respective related skills.</returns>
  public virtual List<AppointmentEntry.ServiceRequirement> GetAppointmentDetServiceRowSkills(
    List<FSAppointmentDet> appointmentServiceDetails,
    ref List<int?> serviceSkillIDs)
  {
    List<AppointmentEntry.ServiceRequirement> source = new List<AppointmentEntry.ServiceRequirement>();
    List<object> objectList1 = new List<object>();
    BqlCommand bqlCommand = ((BqlCommand) new Select2<FSServiceSkill, InnerJoin<PX.Objects.IN.InventoryItem, On<FSServiceSkill.serviceID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<True, Equal<True>>>()).WhereAnd(InHelper<PX.Objects.IN.InventoryItem.inventoryID>.Create(appointmentServiceDetails.Count));
    foreach (FSAppointmentDet appointmentServiceDetail in appointmentServiceDetails)
      objectList1.Add((object) appointmentServiceDetail.InventoryID);
    List<object> objectList2 = new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList1.ToArray());
    if (objectList2.Count == 0)
      return source;
    foreach (PXResult<FSServiceSkill, PX.Objects.IN.InventoryItem> pxResult in objectList2)
    {
      PX.Objects.IN.InventoryItem fsServiceRow = PXResult<FSServiceSkill, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      FSServiceSkill fsServiceSkill = PXResult<FSServiceSkill, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      List<int?> nullableList = serviceSkillIDs;
      int? nullable1 = fsServiceSkill.SkillID;
      int? nullable2 = new int?(nullable1.Value);
      nullableList.Add(nullable2);
      AppointmentEntry.ServiceRequirement serviceRequirement1 = source.Where<AppointmentEntry.ServiceRequirement>((Func<AppointmentEntry.ServiceRequirement, bool>) (list =>
      {
        int serviceId = list.serviceID;
        int? inventoryId = fsServiceRow.InventoryID;
        int valueOrDefault = inventoryId.GetValueOrDefault();
        return serviceId == valueOrDefault & inventoryId.HasValue;
      })).FirstOrDefault<AppointmentEntry.ServiceRequirement>();
      if (serviceRequirement1 != null)
      {
        List<int?> requirementIdList = serviceRequirement1.requirementIDList;
        nullable1 = fsServiceSkill.SkillID;
        int? nullable3 = new int?(nullable1.Value);
        requirementIdList.Add(nullable3);
      }
      else
      {
        AppointmentEntry.ServiceRequirement serviceRequirement2 = new AppointmentEntry.ServiceRequirement();
        nullable1 = fsServiceRow.InventoryID;
        serviceRequirement2.serviceID = nullable1.Value;
        AppointmentEntry.ServiceRequirement serviceRequirement3 = serviceRequirement2;
        List<int?> requirementIdList = serviceRequirement3.requirementIDList;
        nullable1 = fsServiceSkill.SkillID;
        int? nullable4 = new int?(nullable1.Value);
        requirementIdList.Add(nullable4);
        source.Add(serviceRequirement3);
      }
    }
    return source;
  }

  public virtual List<int?> GetAppointmentEmpoyeeSkillIDs(
    PXResultset<FSAppointmentEmployee> bqlResultSet)
  {
    List<int?> appointmentEmpoyeeSkillIds = new List<int?>();
    List<object> objectList = new List<object>();
    BqlCommand bqlCommand = ((BqlCommand) new Select4<FSEmployeeSkill, Where<True, Equal<True>>, Aggregate<GroupBy<FSEmployeeSkill.skillID>>, OrderBy<Asc<FSEmployeeSkill.skillID>>>()).WhereAnd(InHelper<FSEmployeeSkill.employeeID>.Create(bqlResultSet.Count));
    foreach (PXResult<FSAppointmentEmployee> bqlResult in bqlResultSet)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(bqlResult);
      objectList.Add((object) appointmentEmployee.EmployeeID);
    }
    foreach (FSEmployeeSkill fsEmployeeSkill in new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList.ToArray()))
      appointmentEmpoyeeSkillIds.Add(fsEmployeeSkill.SkillID);
    return appointmentEmpoyeeSkillIds;
  }

  /// <summary>
  /// Updates ProjectID in the Lines of the Appointment using the project in the <c>fsServiceOrderRow</c>. Also, sets ProjectTaskID to null.
  /// </summary>
  public virtual void UpdateDetailsFromProjectID(int? projectID)
  {
    if (!projectID.HasValue || this.AppointmentDetails == null)
      return;
    foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()))
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      fsAppointmentDet.ProjectID = projectID;
      fsAppointmentDet.ProjectTaskID = new int?();
      ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(fsAppointmentDet);
    }
  }

  /// <summary>
  /// Appointment detail project tasks are removed due to SetValueExtIfDifferent for FSAppointment.billServiceContractID
  /// Reassigning project tasks
  /// </summary>
  /// <param name="serviceOrder">service order for the appointment</param>
  public void UpdateDetailsFromProjectTaskID(FSServiceOrder serviceOrder)
  {
    if (serviceOrder == null || this.AppointmentDetails == null)
      return;
    foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()))
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      FSAppointmentDet copy = (FSAppointmentDet) ((PXSelectBase) this.AppointmentDetails).Cache.CreateCopy((object) fsAppointmentDet);
      copy.ProjectTaskID = fsAppointmentDet.FSSODetRow.ProjectTaskID;
      ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(copy);
    }
  }

  /// <summary>
  /// Updates BranchID in the Lines of the Appointment using the branch in the <c>fsServiceOrderRow</c>.
  /// </summary>
  public virtual void UpdateDetailsFromBranchID(FSServiceOrder fsServiceOrderRow)
  {
    if (!fsServiceOrderRow.BranchID.HasValue || this.AppointmentDetails == null)
      return;
    foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()))
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      fsAppointmentDet.BranchID = fsServiceOrderRow.BranchID;
      ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(fsAppointmentDet);
    }
  }

  public virtual void CalculateLaborCosts()
  {
    foreach (FSAppointmentLog row in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (x => x.BAccountID.HasValue)))
    {
      object obj;
      ((PXSelectBase) this.LogRecords).Cache.RaiseFieldDefaulting<FSAppointmentLog.unitCost>((object) row, ref obj);
      row.UnitCost = new Decimal?((Decimal) obj);
      Decimal curyval = (Decimal) obj;
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PXSelectBase) this.LogRecords).Cache, (object) row, curyval, out curyval, CommonSetupDecPl.PrcCst);
      row.CuryUnitCost = new Decimal?(curyval);
      ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Update(row);
    }
  }

  public virtual void CalculateCosts()
  {
    foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x =>
    {
      if (x.LineType == "NSTKI" && !x.IsCanceledNotPerformed.GetValueOrDefault() && !x.IsLinkedItem)
      {
        bool? manualCost = x.ManualCost;
        bool flag1 = false;
        if (manualCost.GetValueOrDefault() == flag1 & manualCost.HasValue)
        {
          bool? enablePo = x.EnablePO;
          bool flag2 = false;
          return enablePo.GetValueOrDefault() == flag2 & enablePo.HasValue;
        }
      }
      return false;
    })))
    {
      object obj;
      ((PXSelectBase) this.AppointmentDetails).Cache.RaiseFieldDefaulting<FSAppointmentDet.unitCost>((object) fsAppointmentDet, ref obj);
      fsAppointmentDet.UnitCost = new Decimal?((Decimal) obj);
      Decimal curyval = INUnitAttribute.ConvertToBase<FSAppointmentDet.inventoryID, FSAppointmentDet.uOM>(((PXSelectBase) this.AppointmentDetails).Cache, (object) fsAppointmentDet, (Decimal) obj, INPrecision.NOROUND);
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PXSelectBase) this.AppointmentDetails).Cache, (object) fsAppointmentDet, curyval, out curyval, CommonSetupDecPl.PrcCst);
      fsAppointmentDet.CuryUnitCost = new Decimal?(curyval);
      ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(fsAppointmentDet);
    }
    this.CalculateLaborCosts();
  }

  public virtual Decimal? CalculateLaborCost(
    PXCache cache,
    FSAppointmentLog fsAppointmentLogRow,
    FSAppointment fsAppointmentRow)
  {
    if (!fsAppointmentLogRow.LaborItemID.HasValue)
      return new Decimal?();
    PMLaborCostRate pmLaborCostRate = PXResult<PMLaborCostRate>.op_Implicit(((IEnumerable<PXResult<PMLaborCostRate>>) PXSelectBase<PMLaborCostRate, PXSelect<PMLaborCostRate, Where<PMLaborCostRate.type, Equal<PMLaborCostRateType.employee>, And<PMLaborCostRate.employeeID, Equal<Required<PMLaborCostRate.employeeID>>, And<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, And<PMLaborCostRate.employmentType, Equal<RateTypesAttribute.hourly>, And<PMLaborCostRate.curyID, Equal<Required<PMLaborCostRate.curyID>>, And<PMLaborCostRate.effectiveDate, LessEqual<Required<PMLaborCostRate.effectiveDate>>>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) fsAppointmentLogRow.BAccountID,
      (object) fsAppointmentLogRow.LaborItemID,
      (object) fsAppointmentRow.CuryID,
      (object) fsAppointmentRow.ExecutionDate
    })).AsEnumerable<PXResult<PMLaborCostRate>>().FirstOrDefault<PXResult<PMLaborCostRate>>());
    if (pmLaborCostRate == null)
      pmLaborCostRate = PXResult<PMLaborCostRate>.op_Implicit(((IEnumerable<PXResult<PMLaborCostRate>>) PXSelectBase<PMLaborCostRate, PXSelect<PMLaborCostRate, Where<PMLaborCostRate.type, Equal<PMLaborCostRateType.item>, And<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, And<PMLaborCostRate.employmentType, Equal<RateTypesAttribute.hourly>, And<PMLaborCostRate.curyID, Equal<Required<PMLaborCostRate.curyID>>, And<PMLaborCostRate.effectiveDate, LessEqual<Required<PMLaborCostRate.effectiveDate>>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) fsAppointmentLogRow.LaborItemID,
        (object) fsAppointmentRow.CuryID,
        (object) fsAppointmentRow.ExecutionDate
      })).AsEnumerable<PXResult<PMLaborCostRate>>().FirstOrDefault<PXResult<PMLaborCostRate>>());
    return pmLaborCostRate?.Rate;
  }

  public virtual IEnumerable profitabilityRecords()
  {
    return (IEnumerable) this.ProfitabilityRecords_INItems((PXGraph) this, (FSServiceOrder) null, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current).Concat<FSProfitability>((IEnumerable<FSProfitability>) this.ProfitabilityRecords_Logs((PXGraph) this, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current));
  }

  public virtual void CalculateProfitValues()
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current == null)
      return;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    Decimal? nullable1 = current.CuryCostTotal;
    if (nullable1.HasValue)
    {
      nullable1 = current.CuryCostTotal;
      Decimal num1 = 0M;
      if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
      {
        nullable1 = current.CuryActualBillableTotal;
        if (nullable1.HasValue)
        {
          FSAppointment fsAppointment = current;
          nullable1 = current.CuryActualBillableTotal;
          Decimal num2 = nullable1.Value;
          nullable1 = current.CuryCostTotal;
          Decimal num3 = nullable1.Value;
          Decimal num4 = (num2 - num3) * 100M;
          nullable1 = current.CuryCostTotal;
          Decimal num5 = nullable1.Value;
          Decimal? nullable2 = new Decimal?(num4 / num5);
          fsAppointment.ProfitPercent = nullable2;
        }
      }
    }
    nullable1 = current.CuryActualBillableTotal;
    if (!nullable1.HasValue)
      return;
    nullable1 = current.CuryActualBillableTotal;
    Decimal num6 = 0M;
    if (nullable1.GetValueOrDefault() == num6 & nullable1.HasValue)
      return;
    nullable1 = current.CuryCostTotal;
    if (!nullable1.HasValue)
      return;
    FSAppointment fsAppointment1 = current;
    nullable1 = current.CuryActualBillableTotal;
    Decimal num7 = nullable1.Value;
    nullable1 = current.CuryCostTotal;
    Decimal num8 = nullable1.Value;
    Decimal num9 = (num7 - num8) * 100M;
    nullable1 = current.CuryActualBillableTotal;
    Decimal num10 = nullable1.Value;
    Decimal? nullable3 = new Decimal?(num9 / num10);
    fsAppointment1.ProfitMarginPercent = nullable3;
  }

  /// <summary>Sends Mail.</summary>
  public virtual void SendNotification(
    AppointmentEntry graph,
    PXCache cache,
    string mailing,
    int? branchID,
    IList<Guid?> attachments = null)
  {
    this.SendNotification(graph, cache, mailing, branchID, (IDictionary<string, string>) null, attachments);
  }

  public virtual void FillDocDesc(FSAppointment fsAppointmentRow)
  {
    FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.appointmentID, Equal<Required<FSAppointmentDet.appointmentID>>>, OrderBy<Asc<FSAppointmentDet.sODetID>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) fsAppointmentRow.AppointmentID
    }));
    fsAppointmentRow.DocDesc = fsAppointmentDet?.TranDesc;
  }

  /// <summary>
  /// Sets the TimeRegister depending on <c>Setup.RequireTimeApprovalToInvoice</c> and ActualTime.
  /// </summary>
  public virtual void SetTimeRegister(
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    PXDBOperation operation)
  {
    bool? nullable = new bool?(true);
    if (fsSrvOrdTypeRow.RequireTimeApprovalToInvoice.GetValueOrDefault() && operation == 1)
    {
      if (PXSelectBase<FSAppointmentLog, PXSelect<FSAppointmentLog, Where<FSAppointmentLog.approvedTime, Equal<False>, And<FSAppointmentLog.trackTime, Equal<True>, And<FSAppointmentLog.docID, Equal<Required<FSAppointmentLog.docID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.AppointmentID
      }).Count > 0 || ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>()).Count == 0)
        nullable = new bool?(false);
    }
    else if (!fsAppointmentRow.ActualDateTimeBegin.HasValue)
      nullable = new bool?(false);
    fsAppointmentRow.TimeRegistered = nullable;
  }

  public virtual void CalculateEndTimeWithLinesDuration(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    AppointmentEntry.DateFieldType dateFieldType,
    bool forceUpdate = false)
  {
    bool? nullable;
    if (!forceUpdate)
    {
      if (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current == null)
        return;
      nullable = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.isBeingCloned;
      if (nullable.GetValueOrDefault())
        return;
    }
    nullable = fsAppointmentRow.HandleManuallyScheduleTime;
    bool flag = nullable.Value;
    DateTime? scheduledDateTimeBegin = fsAppointmentRow.ScheduledDateTimeBegin;
    int num = fsAppointmentRow.EstimatedDurationTotal.Value;
    if (!scheduledDateTimeBegin.HasValue || !forceUpdate && flag)
      return;
    DateTime? newValue = new DateTime?(scheduledDateTimeBegin.Value.AddMinutes((double) num));
    bool manualTimeFlagUpdate = this.SkipManualTimeFlagUpdate;
    try
    {
      this.SkipManualTimeFlagUpdate = true;
      if (dateFieldType != AppointmentEntry.DateFieldType.ScheduleField)
        return;
      cache.SetValueExtIfDifferent<FSAppointment.scheduledDateTimeEnd>((object) fsAppointmentRow, (object) newValue);
      cache.SetValuePending((object) fsAppointmentRow, typeof (FSAppointment.scheduledDateTimeEnd).Name, PXCache.NotSetValue);
      cache.SetValuePending((object) fsAppointmentRow, typeof (FSAppointment.scheduledDateTimeEnd).Name + "_Time", PXCache.NotSetValue);
    }
    finally
    {
      this.SkipManualTimeFlagUpdate = manualTimeFlagUpdate;
    }
  }

  /// <summary>
  /// Check the ManageRooms value on Setup to check/hide the Rooms Values options.
  /// </summary>
  public virtual void HideRooms(FSAppointment fsAppointmentRow, FSSetup fSSetupRow)
  {
    bool flag = ServiceManagementSetup.IsRoomManagementActive((PXGraph) this, fSSetupRow);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.roomID>(((PXSelectBase) this.ServiceOrderRelated).Cache, (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).SelectSingle(Array.Empty<object>()), flag);
    ((PXAction) this.openRoomBoard).SetVisible(flag);
  }

  public virtual void SetServiceOrderStatusFromAppointment(
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    AppointmentEntry.ActionButton action)
  {
    if (this.SkipCallSOAction)
      return;
    if (action == AppointmentEntry.ActionButton.CompleteAppointment)
    {
      bool? srvOrdWhenSrvDone = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.CompleteSrvOrdWhenSrvDone;
      bool flag = false;
      if (srvOrdWhenSrvDone.GetValueOrDefault() == flag & srvOrdWhenSrvDone.HasValue)
        return;
    }
    if (action == AppointmentEntry.ActionButton.CloseAppointment)
    {
      bool? srvOrdWhenSrvDone = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.CloseSrvOrdWhenSrvDone;
      bool flag = false;
      if (srvOrdWhenSrvDone.GetValueOrDefault() == flag & srvOrdWhenSrvDone.HasValue)
        return;
    }
    FSAppointment changingAppointment = fsAppointmentRow;
    bool? nullable = fsAppointmentRow.Closed;
    int num;
    if (!nullable.GetValueOrDefault())
    {
      nullable = fsAppointmentRow.CloseActionRunning;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 1;
    if (!this.IsUpdatingTheLatestActiveAppointmentOfServiceOrder(changingAppointment, num != 0) && action != AppointmentEntry.ActionButton.EditAppointment)
      return;
    ServiceOrderEntry serviceOrderEntryGraph = this.GetServiceOrderEntryGraph(true);
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords1 = serviceOrderEntryGraph.ServiceOrderRecords;
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords2 = serviceOrderEntryGraph.ServiceOrderRecords;
    string refNbr = fsServiceOrderRow.RefNbr;
    object[] objArray = new object[1]
    {
      (object) fsServiceOrderRow.SrvOrdType
    };
    FSServiceOrder fsServiceOrder1;
    FSServiceOrder fsServiceOrder2 = fsServiceOrder1 = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderRecords2).Search<FSServiceOrder.refNbr>((object) refNbr, objArray));
    ((PXSelectBase<FSServiceOrder>) serviceOrderRecords1).Current = fsServiceOrder1;
    FSServiceOrder fsServiceOrder3 = fsServiceOrder2;
    if (action == AppointmentEntry.ActionButton.CompleteAppointment)
    {
      nullable = fsServiceOrderRow.OpenDoc;
      if (nullable.GetValueOrDefault())
      {
        fsServiceOrder3.CompleteAppointments = new bool?(false);
        ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.LastAppointmentCompleted))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
        ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
        serviceOrderEntryGraph.SkipTaxCalcAndSave();
      }
      nullable = fsServiceOrderRow.Closed;
      if (!nullable.GetValueOrDefault())
        return;
      ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.AppointmentUnclosed))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
      ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
      serviceOrderEntryGraph.SkipTaxCalcAndSave();
    }
    else if (action == AppointmentEntry.ActionButton.CloseAppointment || action == AppointmentEntry.ActionButton.InvoiceAppointment)
    {
      nullable = fsServiceOrderRow.Completed;
      if (!nullable.GetValueOrDefault())
        return;
      fsServiceOrder3.CloseAppointments = new bool?(false);
      ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.LastAppointmentClosed))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
      ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
      serviceOrderEntryGraph.SkipTaxCalcAndSave();
    }
    else if (action == AppointmentEntry.ActionButton.UnCloseAppointment)
    {
      nullable = fsServiceOrderRow.Closed;
      if (!nullable.GetValueOrDefault())
        return;
      ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.AppointmentUnclosed))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
      ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
      serviceOrderEntryGraph.SkipTaxCalcAndSave();
    }
    else if (action == AppointmentEntry.ActionButton.PutOnHold || action == AppointmentEntry.ActionButton.ReOpenAppointment || action == AppointmentEntry.ActionButton.ReleaseFromHold)
    {
      nullable = fsServiceOrderRow.Canceled;
      if (!nullable.GetValueOrDefault())
      {
        nullable = fsServiceOrderRow.Completed;
        if (!nullable.GetValueOrDefault())
          return;
      }
      ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.AppointmentReOpened))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
      ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
      serviceOrderEntryGraph.SkipTaxCalcAndSave();
    }
    else
    {
      if (action != AppointmentEntry.ActionButton.EditAppointment)
        return;
      nullable = fsServiceOrderRow.Completed;
      if (!nullable.GetValueOrDefault())
        return;
      ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.AppointmentEdit))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
      ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
      serviceOrderEntryGraph.SkipTaxCalcAndSave();
    }
  }

  public virtual bool UpdateServiceOrderWhenAppointmentEdit(FSAppointment fsAppointmentRow)
  {
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    return (current != null ? (current.CompleteSrvOrdWhenSrvDone.GetValueOrDefault() ? 1 : 0) : 0) != 0 && !((IQueryable<PXResult<FSAppointment>>) PXSelectBase<FSAppointment, PXViewOf<FSAppointment>.BasedOn<SelectFromBase<FSAppointment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.sOID, Equal<BqlField<FSAppointment.sOID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSAppointment.appointmentID, NotEqual<BqlField<FSAppointment.appointmentID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<FSAppointment.status, IBqlString>.IsNotIn<ListField.AppointmentStatus.canceled, ListField.AppointmentStatus.completed>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<PXResult<FSAppointment>>();
  }

  public virtual void SetLatestServiceOrderStatusBaseOnAppointmentStatus(
    FSServiceOrder fsServiceOrderRow,
    string latestServiceOrderStatus)
  {
    if (this.SkipCallSOAction || string.IsNullOrEmpty(latestServiceOrderStatus))
      return;
    ServiceOrderEntry serviceOrderEntryGraph = this.GetServiceOrderEntryGraph(true);
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords1 = serviceOrderEntryGraph.ServiceOrderRecords;
    PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords2 = serviceOrderEntryGraph.ServiceOrderRecords;
    string refNbr = fsServiceOrderRow.RefNbr;
    object[] objArray = new object[1]
    {
      (object) fsServiceOrderRow.SrvOrdType
    };
    FSServiceOrder fsServiceOrder1;
    FSServiceOrder fsServiceOrder2 = fsServiceOrder1 = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderRecords2).Search<FSServiceOrder.refNbr>((object) refNbr, objArray));
    ((PXSelectBase<FSServiceOrder>) serviceOrderRecords1).Current = fsServiceOrder1;
    FSServiceOrder fsServiceOrder3 = fsServiceOrder2;
    if (((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current != null && ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current.Status == latestServiceOrderStatus)
      return;
    switch (latestServiceOrderStatus)
    {
      case "X":
        ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.LastAppointmentCanceled))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
        ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
        ((PXAction) serviceOrderEntryGraph.Save).Press();
        break;
      case "C":
        ((SelectedEntityEvent<FSServiceOrder>) PXEntityEventBase<FSServiceOrder>.Container<FSServiceOrder.Events>.Select((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder.Events>>>) (ev => ev.LastAppointmentCompleted))).FireOn((PXGraph) serviceOrderEntryGraph, fsServiceOrder3);
        ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Update(fsServiceOrder3);
        ((PXAction) serviceOrderEntryGraph.Save).Press();
        break;
    }
  }

  public virtual bool IsUpdatingTheLatestActiveAppointmentOfServiceOrder(
    FSAppointment changingAppointment,
    bool considerCompletedStatus = false)
  {
    if (changingAppointment.Completed.GetValueOrDefault())
    {
      bool? srvOrdWhenSrvDone = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.CompleteSrvOrdWhenSrvDone;
      bool flag = false;
      if (srvOrdWhenSrvDone.GetValueOrDefault() == flag & srvOrdWhenSrvDone.HasValue)
        goto label_4;
    }
    if (changingAppointment.Closed.GetValueOrDefault())
    {
      bool? srvOrdWhenSrvDone = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.CloseSrvOrdWhenSrvDone;
      bool flag = false;
      if (srvOrdWhenSrvDone.GetValueOrDefault() == flag & srvOrdWhenSrvDone.HasValue)
        goto label_4;
    }
    BqlCommand bqlCommand = (BqlCommand) new Select<FSAppointment, Where<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>, And<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>>();
    List<object> objectList = new PXView((PXGraph) this, true, considerCompletedStatus ? bqlCommand.WhereAnd(typeof (Where<FSAppointment.notStarted, Equal<True>, Or<FSAppointment.inProcess, Equal<True>, Or<Where<FSAppointment.completed, Equal<True>, And<FSAppointment.closed, Equal<False>>>>>>)) : bqlCommand.WhereAnd(typeof (Where<FSAppointment.notStarted, Equal<True>, Or<FSAppointment.inProcess, Equal<True>>>))).SelectMulti(new List<object>()
    {
      (object) changingAppointment.AppointmentID,
      (object) changingAppointment.SOID
    }.ToArray());
    if (objectList != null && objectList.Count > 0)
      return false;
    if ((changingAppointment.Completed.GetValueOrDefault() || changingAppointment.CompleteActionRunning.GetValueOrDefault()) && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.CompleteSrvOrdWhenSrvDone.GetValueOrDefault())
    {
      if (((IQueryable<PXResult<FSSODet>>) PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sOID, Equal<Required<FSSODet.sOID>>, And<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.ScheduleNeeded>, And<FSSODet.lineType, NotEqual<ListField_LineType_ALL.Instruction>, And<FSSODet.lineType, NotEqual<ListField_LineType_ALL.Comment>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) changingAppointment.SOID
      })).Count<PXResult<FSSODet>>() > 0)
        return false;
    }
    return true;
label_4:
    return false;
  }

  public virtual string GetFinalServiceOrderStatus(
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow)
  {
    if (this.IsUpdatingTheLatestActiveAppointmentOfServiceOrder(fsAppointmentRow))
    {
      if (PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>, And<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>, And<Where<FSAppointment.canceled, Equal<False>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) fsAppointmentRow.AppointmentID,
        (object) fsAppointmentRow.SOID
      }).Count == 0)
        return "X";
      bool? nullable = fsServiceOrderRow.Completed;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = fsServiceOrderRow.Closed;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          if (PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sOID, Equal<Required<FSSODet.sOID>>, And<Where<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.ScheduleNeeded>, Or<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.Scheduled>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
          {
            (object) fsAppointmentRow.SOID
          }).Count == 0 && ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Ask("Complete Service Order", "This Service Order does not have any other Appointment to be completed. Do you want to complete this Service Order?", (MessageButtons) 4) == 6)
            return "C";
        }
      }
    }
    return string.Empty;
  }

  public virtual void CheckScheduledDateTimes(PXCache cache, FSAppointment fsAppointmentRow)
  {
    if (!fsAppointmentRow.ScheduledDateTimeBegin.HasValue || !fsAppointmentRow.ScheduledDateTimeEnd.HasValue)
      return;
    bool? manuallyScheduleTime = fsAppointmentRow.HandleManuallyScheduleTime;
    bool flag = false;
    if (manuallyScheduleTime.GetValueOrDefault() == flag & manuallyScheduleTime.HasValue)
      return;
    PXSetPropertyException propertyException = this.CheckDateTimes(fsAppointmentRow.ScheduledDateTimeBegin.Value, fsAppointmentRow.ScheduledDateTimeEnd.Value, true);
    if (propertyException != null)
      cache.RaiseExceptionHandling<FSAppointment.scheduledDateTimeEnd>((object) fsAppointmentRow, (object) fsAppointmentRow.ScheduledDateTimeEnd, (Exception) propertyException);
    else
      cache.RaiseExceptionHandling<FSAppointment.scheduledDateTimeEnd>((object) fsAppointmentRow, (object) fsAppointmentRow.ScheduledDateTimeEnd, (Exception) null);
  }

  public virtual PXSetPropertyException CheckDateTimes(
    DateTime actualDateTimeBegin,
    DateTime actualDateTimeEnd,
    bool isScheduled)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (actualDateTimeBegin > actualDateTimeEnd)
      propertyException = !isScheduled ? new PXSetPropertyException("The appointment actual end date and time cannot be earlier than the appointment actual start date and time.", (PXErrorLevel) 5) : new PXSetPropertyException("The appointment scheduled end date and time cannot be earlier than the appointment scheduled start date and time.", (PXErrorLevel) 5);
    return propertyException;
  }

  public virtual void CheckActualDateTimes(PXCache cache, FSAppointment fsAppointmentRow)
  {
    if (!fsAppointmentRow.ActualDateTimeBegin.HasValue || !fsAppointmentRow.ActualDateTimeEnd.HasValue)
      return;
    PXSetPropertyException propertyException = this.CheckDateTimes(fsAppointmentRow.ActualDateTimeBegin.Value, fsAppointmentRow.ActualDateTimeEnd.Value, false);
    if (((PXFieldState) cache.GetStateExt<FSAppointment.actualDateTimeEnd>((object) fsAppointmentRow)).Error == null)
      cache.RaiseExceptionHandling<FSAppointment.actualDateTimeEnd>((object) fsAppointmentRow, (object) fsAppointmentRow.ActualDateTimeEnd, (Exception) propertyException);
    if (!fsAppointmentRow.BillContractPeriodID.HasValue || ((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Current == null)
      return;
    DateTime? nullable1 = ((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Current.StartPeriodDate;
    DateTime? nullable2;
    if (nullable1.HasValue)
    {
      nullable1 = fsAppointmentRow.ActualDateTimeBegin;
      nullable2 = ((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Current.StartPeriodDate;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        cache.RaiseExceptionHandling<FSAppointment.executionDate>((object) fsAppointmentRow, (object) fsAppointmentRow.ExecutionDate, (Exception) new PXSetPropertyException("The appointment actual start date cannot be earlier than the contract period start date.", (PXErrorLevel) 5));
        cache.RaiseExceptionHandling<FSAppointment.actualDateTimeBegin>((object) fsAppointmentRow, (object) fsAppointmentRow.ActualDateTimeBegin, (Exception) new PXSetPropertyException("The appointment actual start date cannot be earlier than the contract period start date.", (PXErrorLevel) 5));
      }
    }
    nullable2 = ((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Current.EndPeriodDate;
    if (!nullable2.HasValue)
      return;
    nullable2 = fsAppointmentRow.ActualDateTimeEnd;
    nullable1 = ((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Current.EndPeriodDate;
    DateTime dateTime = nullable1.Value.AddDays(1.0);
    if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) == 0)
      return;
    cache.RaiseExceptionHandling<FSAppointment.actualDateTimeEnd>((object) fsAppointmentRow, (object) fsAppointmentRow.ActualDateTimeEnd, (Exception) new PXSetPropertyException("The appointment actual end date cannot be later than the contract period end date.", (PXErrorLevel) 5));
  }

  public virtual void CheckMinMaxActualDateTimes(PXCache cache, FSAppointment fsAppointmentRow)
  {
    DateTime? minLogTimeBegin = fsAppointmentRow.MinLogTimeBegin;
    DateTime? nullable = fsAppointmentRow.ActualDateTimeBegin;
    if ((minLogTimeBegin.HasValue & nullable.HasValue ? (minLogTimeBegin.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      cache.RaiseExceptionHandling<FSAppointment.actualDateTimeBegin>((object) fsAppointmentRow, (object) fsAppointmentRow.ActualDateTimeBegin, (Exception) new PXSetPropertyException("The appointment actual start date and time is later than the earliest start date and time specified on the Log tab.", (PXErrorLevel) 2));
    nullable = fsAppointmentRow.MaxLogTimeEnd;
    DateTime? actualDateTimeEnd = fsAppointmentRow.ActualDateTimeEnd;
    if ((nullable.HasValue & actualDateTimeEnd.HasValue ? (nullable.GetValueOrDefault() > actualDateTimeEnd.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    cache.RaiseExceptionHandling<FSAppointment.actualDateTimeEnd>((object) fsAppointmentRow, (object) fsAppointmentRow.ActualDateTimeEnd, (Exception) new PXSetPropertyException("The appointment actual end date and time is earlier than the latest start date and time on the Log tab.", (PXErrorLevel) 2));
  }

  public virtual void AutoConfirm(FSAppointment fsAppointmentRow)
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current == null)
      return;
    int? appAutoConfirmGap = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.AppAutoConfirmGap;
    if (!appAutoConfirmGap.HasValue)
      return;
    appAutoConfirmGap = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.AppAutoConfirmGap;
    int num = 0;
    if (!(appAutoConfirmGap.GetValueOrDefault() > num & appAutoConfirmGap.HasValue))
      return;
    DateTime? scheduledDateTimeBegin = fsAppointmentRow.ScheduledDateTimeBegin;
    DateTime now = PXTimeZoneInfo.Now;
    double totalMinutes = (scheduledDateTimeBegin.HasValue ? new TimeSpan?(scheduledDateTimeBegin.GetValueOrDefault() - now) : new TimeSpan?()).Value.TotalMinutes;
    appAutoConfirmGap = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.AppAutoConfirmGap;
    double? nullable = appAutoConfirmGap.HasValue ? new double?((double) appAutoConfirmGap.GetValueOrDefault()) : new double?();
    double valueOrDefault = nullable.GetValueOrDefault();
    if (!(totalMinutes <= valueOrDefault & nullable.HasValue))
      return;
    fsAppointmentRow.Confirmed = new bool?(true);
  }

  /// <summary>
  /// Validates if the required information in the Signature tab is complete.
  /// </summary>
  /// <param name="cache">PXCache instance.</param>
  /// <param name="fsAppointmentRow">Current FSAppointment object.</param>
  /// <param name="mustValidateSignature">Indicates if the validation process will be applied.</param>
  public virtual void ValidateSignatureFields(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    bool mustValidateSignature)
  {
    if (mustValidateSignature && !this.IsAnySignatureAttached(cache, fsAppointmentRow))
    {
      if (fsAppointmentRow.CustomerSignedReport.HasValue)
      {
        Guid? customerSignedReport = fsAppointmentRow.CustomerSignedReport;
        Guid empty = Guid.Empty;
        if ((customerSignedReport.HasValue ? (customerSignedReport.GetValueOrDefault() == empty ? 1 : 0) : 0) == 0)
          return;
      }
      throw new PXException("The appointment cannot be completed. Make sure that the appointment has been signed and saved and the signature has been uploaded to the appointment.");
    }
  }

  public virtual void ValidateLicenses<fieldType>(PXCache currentCache, object currentRow) where fieldType : IBqlField
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnByLicense == "N")
      return;
    IEnumerable<FSAppointmentDet> source = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => x.IsService));
    if (source.Count<FSAppointmentDet>() == 0)
      return;
    PXResultset<FSAppointmentEmployee> bqlResultSet = PXSelectBase<FSAppointmentEmployee, PXSelect<FSAppointmentEmployee, Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSAppointmentEmployee.refNbr, Equal<Current<FSAppointment.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    List<int?> serviceLicenseIDs = new List<int?>();
    List<int?> empoyeeLicenseIds = this.GetAppointmentEmpoyeeLicenseIDs(bqlResultSet);
    List<AppointmentEntry.ServiceRequirement> serviceRowLicenses = this.GetAppointmentDetServiceRowLicenses(source.ToList<FSAppointmentDet>(), ref serviceLicenseIDs);
    List<int?> list1 = serviceLicenseIDs.Distinct<int?>().ToList<int?>();
    if (!list1.Except<int?>((IEnumerable<int?>) empoyeeLicenseIds).Any<int?>())
      return;
    List<int?> list2 = list1.Except<int?>((IEnumerable<int?>) empoyeeLicenseIds).ToList<int?>();
    bool flag1 = false;
    foreach (FSAppointmentDet fsAppointmentDet in source)
    {
      FSAppointmentDet fsAppointmentDetRow = fsAppointmentDet;
      bool flag2 = false;
      AppointmentEntry.ServiceRequirement serviceRequirement = serviceRowLicenses.Where<AppointmentEntry.ServiceRequirement>((Func<AppointmentEntry.ServiceRequirement, bool>) (list =>
      {
        int serviceId = list.serviceID;
        int? inventoryId = fsAppointmentDetRow.InventoryID;
        int valueOrDefault = inventoryId.GetValueOrDefault();
        return serviceId == valueOrDefault & inventoryId.HasValue;
      })).FirstOrDefault<AppointmentEntry.ServiceRequirement>();
      if (serviceRequirement != null)
        flag2 = list2.Intersect<int?>((IEnumerable<int?>) serviceRequirement.requirementIDList).Any<int?>();
      if (flag2)
      {
        PXErrorLevel pxErrorLevel = (PXErrorLevel) 2;
        if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnByLicense == "D")
        {
          flag1 = true;
          pxErrorLevel = (PXErrorLevel) 5;
        }
        currentCache.RaiseExceptionHandling<fieldType>(currentRow, (object) null, (Exception) new PXSetPropertyException("The Employees in this Appointment do not have the Licenses that the Service requires.", pxErrorLevel));
      }
    }
    if (flag1)
      throw new PXException("The Employees in this Appointment do not have the Licenses that the Service requires.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }

  public virtual void ValidateSkills<fieldType>(PXCache currentCache, object currentRow) where fieldType : IBqlField
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnBySkill == "N")
      return;
    IEnumerable<FSAppointmentDet> source = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => x.IsService));
    if (source.Count<FSAppointmentDet>() == 0)
      return;
    PXResultset<FSAppointmentEmployee> bqlResultSet = PXSelectBase<FSAppointmentEmployee, PXSelect<FSAppointmentEmployee, Where<FSAppointmentEmployee.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentEmployee.refNbr, Equal<Current<FSAppointment.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    List<object> objectList = new List<object>();
    List<int?> serviceSkillIDs = new List<int?>();
    List<int?> appointmentEmpoyeeSkillIds = this.GetAppointmentEmpoyeeSkillIDs(bqlResultSet);
    List<AppointmentEntry.ServiceRequirement> serviceRowSkills = this.GetAppointmentDetServiceRowSkills(source.ToList<FSAppointmentDet>(), ref serviceSkillIDs);
    List<int?> list1 = serviceSkillIDs.Distinct<int?>().ToList<int?>();
    if (!list1.Except<int?>((IEnumerable<int?>) appointmentEmpoyeeSkillIds).Any<int?>())
      return;
    List<int?> list2 = list1.Except<int?>((IEnumerable<int?>) appointmentEmpoyeeSkillIds).ToList<int?>();
    bool flag1 = false;
    foreach (FSAppointmentDet fsAppointmentDet in source)
    {
      FSAppointmentDet fsAppointmentDetRow = fsAppointmentDet;
      bool flag2 = false;
      AppointmentEntry.ServiceRequirement serviceRequirement = serviceRowSkills.Where<AppointmentEntry.ServiceRequirement>((Func<AppointmentEntry.ServiceRequirement, bool>) (list =>
      {
        int serviceId = list.serviceID;
        int? inventoryId = fsAppointmentDetRow.InventoryID;
        int valueOrDefault = inventoryId.GetValueOrDefault();
        return serviceId == valueOrDefault & inventoryId.HasValue;
      })).FirstOrDefault<AppointmentEntry.ServiceRequirement>();
      if (serviceRequirement != null)
        flag2 = list2.Intersect<int?>((IEnumerable<int?>) serviceRequirement.requirementIDList).Any<int?>();
      if (flag2)
      {
        PXErrorLevel pxErrorLevel = (PXErrorLevel) 2;
        if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnBySkill == "D")
        {
          flag1 = true;
          pxErrorLevel = (PXErrorLevel) 5;
        }
        currentCache.RaiseExceptionHandling<fieldType>(currentRow, (object) null, (Exception) new PXSetPropertyException("The Employees in this Appointment do not have the Skills that the Service requires.", pxErrorLevel));
      }
    }
    if (flag1)
      throw new PXException("The Employees in this Appointment do not have the Skills that the Service requires.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }

  public virtual void ValidateGeoZones<fieldType>(PXCache currentCache, object currentRow) where fieldType : IBqlField
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnByGeoZone == "N")
      return;
    FSAddress fsAddress = PXResultset<FSAddress>.op_Implicit(((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Select(Array.Empty<object>()));
    if (fsAddress == null || string.IsNullOrEmpty(fsAddress.PostalCode))
      return;
    PXResultset<FSAppointmentEmployee> pxResultset = PXSelectBase<FSAppointmentEmployee, PXSelect<FSAppointmentEmployee, Where<FSAppointmentEmployee.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentEmployee.refNbr, Equal<Current<FSAppointment.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    bool flag = false;
    List<object> objectList = new List<object>();
    List<int?> first = new List<int?>();
    List<int?> second = new List<int?>();
    BqlCommand bqlCommand = ((BqlCommand) new Select2<FSGeoZoneEmp, InnerJoin<FSGeoZonePostalCode, On<FSGeoZonePostalCode.geoZoneID, Equal<FSGeoZoneEmp.geoZoneID>>>>()).WhereAnd(InHelper<FSGeoZoneEmp.employeeID>.Create(pxResultset.Count));
    foreach (PXResult<FSAppointmentEmployee> pxResult in pxResultset)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
      objectList.Add((object) appointmentEmployee.EmployeeID);
      first.Add(appointmentEmployee.EmployeeID);
    }
    foreach (PXResult<FSGeoZoneEmp, FSGeoZonePostalCode> pxResult in new PXView((PXGraph) this, true, bqlCommand).SelectMulti(objectList.ToArray()))
    {
      FSGeoZoneEmp fsGeoZoneEmp = PXResult<FSGeoZoneEmp, FSGeoZonePostalCode>.op_Implicit(pxResult);
      FSGeoZonePostalCode geoZonePostalCode = PXResult<FSGeoZoneEmp, FSGeoZonePostalCode>.op_Implicit(pxResult);
      if (Regex.Match(fsAddress.PostalCode.Trim(), geoZonePostalCode.PostalCode.Trim()).Success)
        second.Add(fsGeoZoneEmp.EmployeeID);
    }
    List<int?> list = first.Except<int?>((IEnumerable<int?>) second).ToList<int?>();
    if (list.Count <= 0)
      return;
    foreach (PXResult<FSAppointmentEmployee> pxResult in pxResultset)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
      if (list.IndexOf(appointmentEmployee.EmployeeID) != -1)
      {
        PXErrorLevel pxErrorLevel = (PXErrorLevel) 2;
        if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnByGeoZone == "D")
        {
          flag = true;
          pxErrorLevel = (PXErrorLevel) 5;
        }
        currentCache.RaiseExceptionHandling<fieldType>(currentRow, (object) null, (Exception) new PXSetPropertyException("Employee not assigned to work on this service area. The postal code for this Appointment is not included in the service area where this employee could work.", pxErrorLevel));
      }
    }
    if (flag)
      throw new PXException("Employee not assigned to work on this service area. The postal code for this Appointment is not included in the service area where this employee could work.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }

  public virtual void ClearEmployeesGrid()
  {
    foreach (PXResult<FSAppointmentEmployee> pxResult in ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>()))
      ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Delete(PXResult<FSAppointmentEmployee>.op_Implicit(pxResult));
  }

  public virtual void GetEmployeesFromServiceOrder(
    PXCache appDetCache,
    FSAppointment fsAppointmentRow)
  {
    this.ClearEmployeesGrid();
    foreach (PXResult<FSSOEmployee> pxResult in PXSelectBase<FSSOEmployee, PXSelectJoin<FSSOEmployee, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSSOEmployee.sOID>>>, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsAppointmentRow.SOID
    }))
    {
      FSSOEmployee fssoEmployee = PXResult<FSSOEmployee>.op_Implicit(pxResult);
      FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee();
      appointmentEmployee.EmployeeID = fssoEmployee.EmployeeID;
      bool flag = false;
      if (!string.IsNullOrEmpty(fssoEmployee.ServiceLineRef))
      {
        foreach (FSAppointmentDet fsAppointmentDet in appDetCache.Inserted)
        {
          if (fsAppointmentDet.FSSODetRow != null && fsAppointmentDet.FSSODetRow.LineRef == fssoEmployee.ServiceLineRef)
          {
            appointmentEmployee.ServiceLineRef = fsAppointmentDet.LineRef;
            flag = true;
            break;
          }
        }
      }
      if (string.IsNullOrEmpty(fssoEmployee.ServiceLineRef) || flag)
        ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Insert(appointmentEmployee);
    }
  }

  public virtual void ClearResourceGrid()
  {
    foreach (PXResult<FSAppointmentResource> pxResult in ((PXSelectBase<FSAppointmentResource>) this.AppointmentResources).Select(Array.Empty<object>()))
      ((PXSelectBase<FSAppointmentResource>) this.AppointmentResources).Delete(PXResult<FSAppointmentResource>.op_Implicit(pxResult));
  }

  public virtual void ClearPrepayment(FSAppointment fsAppointmentRow)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    SM_ARPaymentEntry extension = ((PXGraph) instance).GetExtension<SM_ARPaymentEntry>();
    foreach (PXResult<FSAdjust> pxResult in PXSelectBase<FSAdjust, PXSelect<FSAdjust, Where<FSAdjust.adjdOrderType, Equal<Required<FSAdjust.adjdOrderType>>, And<FSAdjust.adjdOrderNbr, Equal<Required<FSAdjust.adjdOrderNbr>>, And<FSAdjust.adjdAppRefNbr, Equal<Required<FSAdjust.adjdAppRefNbr>>>>>>.Config>.Select((PXGraph) instance, new object[3]
    {
      (object) fsAppointmentRow.SrvOrdType,
      (object) fsAppointmentRow.SORefNbr,
      (object) fsAppointmentRow.RefNbr
    }))
    {
      FSAdjust fsAdjust = PXResult<FSAdjust>.op_Implicit(pxResult);
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) fsAdjust.AdjgRefNbr, new object[1]
      {
        (object) fsAdjust.AdjgDocType
      }));
      if (((PXSelectBase<ARPayment>) instance.Document).Current != null)
      {
        fsAdjust.AdjdAppRefNbr = string.Empty;
        ((PXSelectBase<FSAdjust>) extension.FSAdjustments).Update(fsAdjust);
        ((PXAction) instance.Save).Press();
      }
    }
  }

  public virtual void GetResourcesFromServiceOrder(FSAppointment fsAppointmentRow)
  {
    this.ClearResourceGrid();
    foreach (PXResult<FSSOResource> pxResult in PXSelectBase<FSSOResource, PXSelectJoin<FSSOResource, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSSOResource.sOID>>>, Where<FSServiceOrder.sOID, Equal<Required<FSServiceOrder.sOID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsAppointmentRow.SOID
    }))
    {
      FSSOResource fssoResource = PXResult<FSSOResource>.op_Implicit(pxResult);
      ((PXSelectBase<FSAppointmentResource>) this.AppointmentResources).Insert(new FSAppointmentResource()
      {
        SMEquipmentID = fssoResource.SMEquipmentID,
        Comment = fssoResource.Comment
      });
    }
  }

  public virtual void UncheckUnreachedCustomerByScheduledDate(
    DateTime? oldValue,
    DateTime? currentValue,
    FSAppointment fsAppointmentRow)
  {
    DateTime? nullable1 = currentValue;
    DateTime? nullable2 = oldValue;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    fsAppointmentRow.UnreachedCustomer = new bool?(false);
  }

  public void ValidateActualDurationEdit(FSAppointmentDet apptDetRow, int? newValue)
  {
    int? actualDuration = apptDetRow.ActualDuration;
    int? nullable = newValue;
    if (!(actualDuration.GetValueOrDefault() == nullable.GetValueOrDefault() & actualDuration.HasValue == nullable.HasValue) && this.GetLogTrackingCount(apptDetRow.LineRef) > 0)
      throw new PXSetPropertyException<FSAppointmentDet.actualDuration>("The service actual duration cannot be edited because there is a log line or lines on the Log tab related to the service time tracking.");
  }

  public virtual void ValidateEmployeeAvailability<fieldType>(
    FSAppointment fsAppointmentRow,
    PXCache currentCache,
    object currentRow)
    where fieldType : IBqlField
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnByAppOverlap == "N")
      return;
    PXResultset<FSAppointmentEmployee> pxResultset = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>());
    if (pxResultset.Count == 0)
      return;
    List<object> objectList1 = new List<object>();
    List<int?> nullableList1 = new List<int?>();
    List<int?> nullableList2 = new List<int?>();
    BqlCommand bqlCommand1 = (BqlCommand) new Select2<FSAppointment, InnerJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.appointmentID, Equal<FSAppointment.appointmentID>>>, Where2<Where<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>, And<FSAppointment.canceled, Equal<False>>>, And<FSAppointment.completed, Equal<False>, And<FSAppointment.closed, Equal<False>, And<Where<FSAppointment.scheduledDateTimeEnd, Greater<Required<FSAppointment.scheduledDateTimeBegin>>, And<FSAppointment.scheduledDateTimeBegin, Less<Required<FSAppointment.scheduledDateTimeEnd>>>>>>>>>();
    objectList1.Add((object) fsAppointmentRow.AppointmentID);
    objectList1.Add((object) fsAppointmentRow.ScheduledDateTimeBegin);
    objectList1.Add((object) fsAppointmentRow.ScheduledDateTimeEnd);
    BqlCommand bqlCommand2 = bqlCommand1.WhereAnd(InHelper<FSAppointmentEmployee.employeeID>.Create(pxResultset.Count));
    foreach (PXResult<FSAppointmentEmployee> pxResult in pxResultset)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
      objectList1.Add((object) appointmentEmployee.EmployeeID);
      nullableList1.Add(appointmentEmployee.EmployeeID);
    }
    List<object> objectList2 = new PXView((PXGraph) this, true, bqlCommand2).SelectMulti(objectList1.ToArray());
    foreach (PXResult<FSAppointment, FSAppointmentEmployee> pxResult in objectList2)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointment, FSAppointmentEmployee>.op_Implicit(pxResult);
      nullableList2.Add(appointmentEmployee.EmployeeID);
    }
    if (objectList2.Count <= 0)
      return;
    bool flag = false;
    foreach (PXResult<FSAppointmentEmployee> pxResult in pxResultset)
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
      if (nullableList2.IndexOf(appointmentEmployee.EmployeeID) != -1)
      {
        PXErrorLevel pxErrorLevel = (PXErrorLevel) 2;
        if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.DenyWarnByAppOverlap == "D")
        {
          flag = true;
          pxErrorLevel = (PXErrorLevel) 5;
        }
        currentCache.RaiseExceptionHandling<fieldType>(currentRow, (object) null, (Exception) new PXSetPropertyException("This employee has at least one appointment for the given date and time.", pxErrorLevel));
      }
    }
    if (flag)
      throw new PXException("This employee has at least one appointment for the given date and time.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }

  public virtual void ValidateRoomAvailability(PXCache cache, FSAppointment fsAppointmentRow)
  {
  }

  public virtual void ValidateRoom(FSAppointment fsAppointmentRow)
  {
    FSSrvOrdType fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).SelectSingle(new object[1]
    {
      (object) fsAppointmentRow.SrvOrdType
    });
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    if (!fsSrvOrdType.RequireRoom.GetValueOrDefault() || !string.IsNullOrEmpty(current.RoomID))
      return;
    ((PXSelectBase) this.ServiceOrderRelated).Cache.RaiseExceptionHandling<FSServiceOrder.roomID>((object) current, (object) current.RoomID, (Exception) new PXSetPropertyException("A room ID has to be specified for the selected service order type.", (PXErrorLevel) 4));
  }

  /// <summary>
  /// Validates if the maximum amount of appointments it is exceed for a specific route.
  /// </summary>
  public virtual void ValidateMaxAppointmentQty(FSAppointment fsAppointmentRow)
  {
    if (!fsAppointmentRow.RouteID.HasValue)
      return;
    FSRoute fsRoute = FSRoute.PK.Find((PXGraph) this, fsAppointmentRow.RouteID);
    if (fsRoute == null)
      return;
    bool? appointmentLimit = fsRoute.NoAppointmentLimit;
    bool flag = false;
    if (!(appointmentLimit.GetValueOrDefault() == flag & appointmentLimit.HasValue))
      return;
    DateTime? scheduledDateTimeBegin = fsAppointmentRow.ScheduledDateTimeBegin;
    DateTime? nullable1;
    ref DateTime? local1 = ref nullable1;
    DateTime dateTime1 = scheduledDateTimeBegin.Value;
    int year1 = dateTime1.Year;
    dateTime1 = scheduledDateTimeBegin.Value;
    int month1 = dateTime1.Month;
    dateTime1 = scheduledDateTimeBegin.Value;
    int day1 = dateTime1.Day;
    DateTime dateTime2 = new DateTime(year1, month1, day1, 0, 0, 0);
    local1 = new DateTime?(dateTime2);
    DateTime? nullable2;
    ref DateTime? local2 = ref nullable2;
    dateTime1 = scheduledDateTimeBegin.Value;
    int year2 = dateTime1.Year;
    dateTime1 = scheduledDateTimeBegin.Value;
    int month2 = dateTime1.Month;
    dateTime1 = scheduledDateTimeBegin.Value;
    int day2 = dateTime1.Day;
    DateTime dateTime3 = new DateTime(year2, month2, day2, 23, 59, 59);
    local2 = new DateTime?(dateTime3);
    int count = PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.routeID, Equal<Required<FSAppointment.routeID>>, And<FSAppointment.scheduledDateTimeBegin, Between<Required<FSAppointment.scheduledDateTimeBegin>, Required<FSAppointment.scheduledDateTimeBegin>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) fsAppointmentRow.RouteID,
      (object) nullable1,
      (object) nullable2
    }).Count;
    int? maxAppointmentQty = fsRoute.MaxAppointmentQty;
    int valueOrDefault = maxAppointmentQty.GetValueOrDefault();
    if (count >= valueOrDefault & maxAppointmentQty.HasValue)
      throw new PXException("The appointment cannot be created. The maximum number of appointments has been exceeded for the route.", new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }

  /// <summary>
  /// Validates if the appointment Week Code is valid with the <c>datetime</c> of the appointment.
  /// </summary>
  public virtual void ValidateWeekCode(FSAppointment fsAppointmentRow)
  {
    if (!fsAppointmentRow.ScheduleID.HasValue)
      return;
    FSSchedule fsSchedule = PXResultset<FSSchedule>.op_Implicit(PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsAppointmentRow.ScheduleID
    }));
    DateTime? scheduleTime;
    ref DateTime? local = ref scheduleTime;
    DateTime dateTime1 = fsAppointmentRow.ScheduledDateTimeBegin.Value;
    int year = dateTime1.Year;
    dateTime1 = fsAppointmentRow.ScheduledDateTimeBegin.Value;
    int month = dateTime1.Month;
    dateTime1 = fsAppointmentRow.ScheduledDateTimeBegin.Value;
    int day = dateTime1.Day;
    DateTime dateTime2 = new DateTime(year, month, day, 0, 0, 0);
    local = new DateTime?(dateTime2);
    if (fsSchedule != null && fsSchedule.WeekCode != null && !SharedFunctions.WeekCodeIsValid(fsSchedule.WeekCode, scheduleTime, (PXGraph) this))
    {
      object[] objArray = new object[2]
      {
        (object) fsSchedule.RefNbr,
        null
      };
      dateTime1 = scheduleTime.Value;
      objArray[1] = (object) dateTime1.ToShortDateString();
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The appointment cannot be created on {1}. The {0} schedule does not contain a week code related to this date.", objArray), new object[1]
      {
        (object) (PXErrorLevel) 4
      });
    }
    PXResult<FSScheduleRoute, FSRoute> pxResult = (PXResult<FSScheduleRoute, FSRoute>) PXResultset<FSScheduleRoute>.op_Implicit(PXSelectBase<FSScheduleRoute, PXSelectJoin<FSScheduleRoute, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSScheduleRoute.dfltRouteID>>>, Where<FSScheduleRoute.scheduleID, Equal<Required<FSScheduleRoute.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsSchedule.ScheduleID
    }));
    if (pxResult != null)
    {
      FSRoute fsRoute = PXResult<FSScheduleRoute, FSRoute>.op_Implicit(pxResult);
      if (fsRoute != null && fsRoute.WeekCode != null && !SharedFunctions.WeekCodeIsValid(fsRoute.WeekCode, scheduleTime, (PXGraph) this))
      {
        object[] objArray = new object[2]
        {
          (object) fsSchedule.RefNbr,
          null
        };
        dateTime1 = scheduleTime.Value;
        objArray[1] = (object) dateTime1.ToShortDateString();
        throw new PXException(PXMessages.LocalizeFormatNoPrefix("The appointment cannot be created on {1}. The {0} schedule of the default route does not contain a week code related to this date.", objArray), new object[1]
        {
          (object) (PXErrorLevel) 4
        });
      }
    }
    if (!fsAppointmentRow.RouteID.HasValue)
      return;
    FSRoute fsRoute1 = FSRoute.PK.Find((PXGraph) this, fsAppointmentRow.RouteID);
    if (fsRoute1 != null && fsRoute1.WeekCode != null && !SharedFunctions.WeekCodeIsValid(fsRoute1.WeekCode, scheduleTime, (PXGraph) this))
    {
      object[] objArray = new object[2]
      {
        (object) fsSchedule.RefNbr,
        null
      };
      dateTime1 = scheduleTime.Value;
      objArray[1] = (object) dateTime1.ToShortDateString();
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The appointment cannot be created on {1}. The {0} schedule of the route for the appointment does not contain a week code related to this date.", objArray), new object[1]
      {
        (object) (PXErrorLevel) 4
      });
    }
  }

  /// <summary>
  /// Assign the [fsAppointmentRow] position on the current [fsRouteDocumentRow].
  /// </summary>
  public virtual void SetRoutePosition(
    FSRouteDocument fsRouteDocumentRow,
    FSAppointment fsAppointmentRow)
  {
    if (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current == null)
      return;
    int? nullable1 = fsAppointmentRow.RoutePosition;
    if (nullable1.HasValue)
    {
      nullable1 = fsAppointmentRow.RoutePosition;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() < num & nullable1.HasValue))
        return;
    }
    FSAppointment fsAppointment = PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>, OrderBy<Desc<FSAppointment.routePosition>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsRouteDocumentRow.RouteDocumentID
    }));
    int? nullable2;
    if (fsAppointment != null)
    {
      nullable1 = fsAppointment.RoutePosition;
      if (nullable1.HasValue)
      {
        bool? nullable3 = fsAppointmentRow.IsReassigned;
        if (!nullable3.GetValueOrDefault())
        {
          nullable3 = fsAppointmentRow.NotStarted;
          if (!nullable3.GetValueOrDefault())
            goto label_11;
        }
        nullable3 = ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.SetFirstManualAppointment;
        if (nullable3.GetValueOrDefault())
        {
          nullable2 = new int?(1);
          int? routeDocumentId = fsRouteDocumentRow.RouteDocumentID;
          int? appointmentId = fsAppointmentRow.AppointmentID;
          nullable1 = nullable2;
          int? firstPositionToSet = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
          this.UpdateRouteAppointmentsOrder((PXGraph) this, routeDocumentId, appointmentId, firstPositionToSet);
          goto label_12;
        }
label_11:
        nullable1 = fsAppointment.RoutePosition;
        nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
        goto label_12;
      }
    }
    nullable2 = new int?(1);
label_12:
    fsAppointmentRow.RoutePosition = nullable2;
  }

  /// <summary>
  /// Updates the appointments' order in a route in ascending order setting the initial order.
  /// </summary>
  public virtual void UpdateRouteAppointmentsOrder(
    PXGraph graph,
    int? routeDocumentID,
    int? appointmentID,
    int? firstPositionToSet)
  {
    foreach (PXResult<FSAppointment> pxResult in PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>, And<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>, OrderBy<Asc<FSAppointment.routePosition>>>.Config>.Select(graph, new object[2]
    {
      (object) routeDocumentID,
      (object) appointmentID
    }))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      object[] objArray = new object[2];
      int? nullable1;
      int? nullable2 = nullable1 = firstPositionToSet;
      firstPositionToSet = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
      objArray[0] = (object) nullable1;
      objArray[1] = (object) fsAppointment.AppointmentID;
      PXUpdate<Set<FSAppointment.routePosition, Required<FSAppointment.routePosition>>, FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Update((PXGraph) this, objArray);
    }
  }

  /// <summary>
  /// Set the route info necessary to the [fsAppointmentRow] using the [fsAppointmentRow].RouteID, [fsAppointmentRow].RouteDocumentID and [fsServiceOrderRow].BranchID.
  /// </summary>
  public virtual void SetAppointmentRouteInfo(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrderRow)
  {
    FSRouteDocument orGenerateRoute = this.GetOrGenerateRoute(fsAppointmentRow.RouteID, fsAppointmentRow.RouteDocumentID, fsAppointmentRow.ScheduledDateTimeBegin, fsServiceOrderRow.BranchID);
    this.SetRoutePosition(orGenerateRoute, fsAppointmentRow);
    fsAppointmentRow.RouteDocumentID = orGenerateRoute.RouteDocumentID;
    PXUpdate<Set<FSAppointment.routeDocumentID, Required<FSAppointment.routeDocumentID>, Set<FSAppointment.routeID, Required<FSAppointment.routeID>, Set<FSAppointment.routePosition, Required<FSAppointment.routePosition>>>>, FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Update((PXGraph) this, new object[4]
    {
      (object) orGenerateRoute.RouteDocumentID,
      (object) orGenerateRoute.RouteID,
      (object) fsAppointmentRow.RoutePosition,
      (object) fsAppointmentRow.AppointmentID
    });
    cache.Graph.SelectTimeStamp();
  }

  /// <summary>
  /// Set schedule times to the [fsAppointmentRow] using Route and Schedule.
  /// </summary>
  public virtual void SetScheduleTimesByRouteAndContract(
    FSRouteDocument fsRouteDocumentRow,
    FSAppointment fsAppointmentRow)
  {
    bool hasValue = fsRouteDocumentRow.TimeBegin.HasValue;
    DateTime? scheduledDateTimeEnd1;
    ref DateTime? local1 = ref scheduledDateTimeEnd1;
    DateTime date1 = fsRouteDocumentRow.Date.Value;
    int year1 = date1.Year;
    date1 = fsRouteDocumentRow.Date.Value;
    int month1 = date1.Month;
    date1 = fsRouteDocumentRow.Date.Value;
    int day1 = date1.Day;
    int hour1;
    if (!hasValue)
    {
      hour1 = 0;
    }
    else
    {
      date1 = fsRouteDocumentRow.TimeBegin.Value;
      hour1 = date1.Hour;
    }
    int minute1;
    if (!hasValue)
    {
      minute1 = 0;
    }
    else
    {
      date1 = fsRouteDocumentRow.TimeBegin.Value;
      minute1 = date1.Minute;
    }
    DateTime dateTime1 = new DateTime(year1, month1, day1, hour1, minute1, 0);
    local1 = new DateTime?(dateTime1);
    DateTime? nullable1 = scheduledDateTimeEnd1;
    DateTime? timeEnd = fsRouteDocumentRow.TimeEnd;
    List<object> objectList = new List<object>();
    date1 = nullable1.Value;
    DateTime date2 = date1.Date;
    DateTime dateTime2;
    if (timeEnd.HasValue)
    {
      date1 = timeEnd.Value;
      date1 = date1.Date;
      dateTime2 = date1.AddDays(1.0);
    }
    else
    {
      date1 = date2.Date;
      dateTime2 = date1.AddDays(1.0);
    }
    double num = (double) fsAppointmentRow.EstimatedDurationTotal.Value;
    DateTime? slotEnd;
    ref DateTime? local2 = ref slotEnd;
    date1 = scheduledDateTimeEnd1.Value;
    DateTime dateTime3 = date1.AddMinutes(num);
    local2 = new DateTime?(dateTime3);
    PXSelectReadonly<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>, And<FSAppointment.scheduledDateTimeBegin, GreaterEqual<Required<FSAppointment.scheduledDateTimeBegin>>, And<FSAppointment.scheduledDateTimeEnd, LessEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeEnd, Greater<Required<FSAppointment.scheduledDateTimeBegin>>, And<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>>>>, OrderBy<Asc<FSAppointment.routePosition>>> pxSelectReadonly = new PXSelectReadonly<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>, And<FSAppointment.scheduledDateTimeBegin, GreaterEqual<Required<FSAppointment.scheduledDateTimeBegin>>, And<FSAppointment.scheduledDateTimeEnd, LessEqual<Required<FSAppointment.scheduledDateTimeEnd>>, And<FSAppointment.scheduledDateTimeEnd, Greater<Required<FSAppointment.scheduledDateTimeBegin>>, And<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>>>>, OrderBy<Asc<FSAppointment.routePosition>>>((PXGraph) this);
    objectList.Add((object) fsRouteDocumentRow.RouteDocumentID);
    objectList.Add((object) date2);
    objectList.Add((object) dateTime2);
    objectList.Add((object) nullable1);
    objectList.Add((object) fsAppointmentRow.AppointmentID);
    object[] array = objectList.ToArray();
    foreach (PXResult<FSAppointment> pxResult in ((PXSelectBase<FSAppointment>) pxSelectReadonly).Select(array))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      if (fsAppointment != null && fsAppointment.AppointmentID.HasValue)
      {
        DateTime? scheduledDateTimeEnd2 = fsAppointment.ScheduledDateTimeEnd;
        DateTime? nullable2 = scheduledDateTimeEnd1;
        if ((scheduledDateTimeEnd2.HasValue & nullable2.HasValue ? (scheduledDateTimeEnd2.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          AppointmentEntry.SlotIsContained slotIsContained = this.SlotIsContainedInSlot(scheduledDateTimeEnd1, slotEnd, fsAppointment.ScheduledDateTimeBegin, fsAppointment.ScheduledDateTimeEnd);
          if (slotIsContained == AppointmentEntry.SlotIsContained.Contained || slotIsContained == AppointmentEntry.SlotIsContained.PartiallyContained || slotIsContained == AppointmentEntry.SlotIsContained.ExceedsContainment)
          {
            scheduledDateTimeEnd1 = fsAppointment.ScheduledDateTimeEnd;
            date1 = scheduledDateTimeEnd1.Value;
            slotEnd = new DateTime?(date1.AddMinutes(num));
          }
          if (slotIsContained == AppointmentEntry.SlotIsContained.NotContained)
            break;
        }
      }
    }
    fsAppointmentRow.ScheduledDateTimeBegin = scheduledDateTimeEnd1;
    fsAppointmentRow.ScheduledDateTimeEnd = slotEnd;
    if (!fsAppointmentRow.ScheduleID.HasValue)
      return;
    FSContractSchedule fsContractScheduleRow = PXResultset<FSContractSchedule>.op_Implicit(PXSelectBase<FSContractSchedule, PXSelect<FSContractSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsAppointmentRow.ScheduleID
    }));
    if (fsContractScheduleRow == null || !fsContractScheduleRow.RestrictionMax.GetValueOrDefault() && !fsContractScheduleRow.RestrictionMin.GetValueOrDefault() || this.IsAppointmentInValidRestriction(fsAppointmentRow, fsContractScheduleRow))
      return;
    DateTime? nullable3;
    if (fsContractScheduleRow.RestrictionMin.GetValueOrDefault())
    {
      FSAppointment fsAppointment = fsAppointmentRow;
      int year2 = fsAppointmentRow.ScheduledDateTimeBegin.Value.Year;
      nullable3 = fsAppointmentRow.ScheduledDateTimeBegin;
      int month2 = nullable3.Value.Month;
      nullable3 = fsAppointmentRow.ScheduledDateTimeBegin;
      int day2 = nullable3.Value.Day;
      nullable3 = fsContractScheduleRow.RestrictionMinTime;
      int hour2 = nullable3.Value.Hour;
      nullable3 = fsContractScheduleRow.RestrictionMinTime;
      int minute2 = nullable3.Value.Minute;
      nullable3 = fsContractScheduleRow.RestrictionMinTime;
      int second = nullable3.Value.Second;
      DateTime? nullable4 = new DateTime?(new DateTime(year2, month2, day2, hour2, minute2, second));
      fsAppointment.ScheduledDateTimeBegin = nullable4;
    }
    else
    {
      FSAppointment fsAppointment = fsAppointmentRow;
      int year3 = fsAppointmentRow.ScheduledDateTimeBegin.Value.Year;
      nullable3 = fsAppointmentRow.ScheduledDateTimeBegin;
      int month3 = nullable3.Value.Month;
      nullable3 = fsAppointmentRow.ScheduledDateTimeBegin;
      int day3 = nullable3.Value.Day;
      nullable3 = fsContractScheduleRow.RestrictionMaxTime;
      int hour3 = nullable3.Value.Hour;
      nullable3 = fsContractScheduleRow.RestrictionMaxTime;
      int minute3 = nullable3.Value.Minute;
      nullable3 = fsContractScheduleRow.RestrictionMaxTime;
      int second = nullable3.Value.Second;
      DateTime? nullable5 = new DateTime?(new DateTime(year3, month3, day3, hour3, minute3, second));
      fsAppointment.ScheduledDateTimeBegin = nullable5;
    }
    FSAppointment fsAppointment1 = fsAppointmentRow;
    nullable3 = fsAppointmentRow.ScheduledDateTimeBegin;
    DateTime? nullable6 = new DateTime?(nullable3.Value.AddMinutes(num));
    fsAppointment1.ScheduledDateTimeEnd = nullable6;
  }

  /// <summary>
  /// Set schedule times to the [fsAppointmentRow] using Contract and Schedule.
  /// </summary>
  public virtual void SetScheduleTimesByContract(FSAppointment fsAppointmentRow)
  {
    List<object> objectList = new List<object>();
    DateTime date1 = fsAppointmentRow.ScheduledDateTimeBegin.Value.Date;
    DateTime date2 = fsAppointmentRow.ScheduledDateTimeEnd.Value;
    date2 = date2.Date;
    DateTime dateTime1 = date2.AddDays(1.0);
    FSContractSchedule fsContractScheduleRow = PXResultset<FSContractSchedule>.op_Implicit(PXSelectBase<FSContractSchedule, PXSelect<FSContractSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsAppointmentRow.ScheduleID
    }));
    DateTime? slotBegin;
    DateTime? nullable1;
    DateTime dateTime2;
    if (fsContractScheduleRow != null)
    {
      bool? nullable2 = fsContractScheduleRow.RestrictionMax;
      if (!nullable2.GetValueOrDefault())
      {
        nullable2 = fsContractScheduleRow.RestrictionMin;
        if (!nullable2.GetValueOrDefault())
          goto label_6;
      }
      nullable2 = fsContractScheduleRow.RestrictionMin;
      if (nullable2.GetValueOrDefault())
      {
        ref DateTime? local = ref slotBegin;
        int year = fsAppointmentRow.ScheduledDateTimeBegin.Value.Year;
        nullable1 = fsAppointmentRow.ScheduledDateTimeBegin;
        dateTime2 = nullable1.Value;
        int month = dateTime2.Month;
        nullable1 = fsAppointmentRow.ScheduledDateTimeBegin;
        dateTime2 = nullable1.Value;
        int day = dateTime2.Day;
        nullable1 = fsContractScheduleRow.RestrictionMinTime;
        dateTime2 = nullable1.Value;
        int hour = dateTime2.Hour;
        nullable1 = fsContractScheduleRow.RestrictionMinTime;
        dateTime2 = nullable1.Value;
        int minute = dateTime2.Minute;
        nullable1 = fsContractScheduleRow.RestrictionMinTime;
        dateTime2 = nullable1.Value;
        int second = dateTime2.Second;
        DateTime dateTime3 = new DateTime(year, month, day, hour, minute, second);
        local = new DateTime?(dateTime3);
        goto label_7;
      }
      ref DateTime? local1 = ref slotBegin;
      int year1 = fsAppointmentRow.ScheduledDateTimeBegin.Value.Year;
      nullable1 = fsAppointmentRow.ScheduledDateTimeBegin;
      dateTime2 = nullable1.Value;
      int month1 = dateTime2.Month;
      nullable1 = fsAppointmentRow.ScheduledDateTimeBegin;
      dateTime2 = nullable1.Value;
      int day1 = dateTime2.Day;
      nullable1 = fsContractScheduleRow.RestrictionMaxTime;
      dateTime2 = nullable1.Value;
      int hour1 = dateTime2.Hour;
      nullable1 = fsContractScheduleRow.RestrictionMaxTime;
      dateTime2 = nullable1.Value;
      int minute1 = dateTime2.Minute;
      nullable1 = fsContractScheduleRow.RestrictionMaxTime;
      dateTime2 = nullable1.Value;
      int second1 = dateTime2.Second;
      DateTime dateTime4 = new DateTime(year1, month1, day1, hour1, minute1, second1);
      local1 = new DateTime?(dateTime4);
      goto label_7;
    }
label_6:
    slotBegin = new DateTime?(date1);
label_7:
    int? nullable3 = fsAppointmentRow.EstimatedDurationTotal;
    double num = (double) nullable3.Value;
    DateTime? slotEnd;
    ref DateTime? local2 = ref slotEnd;
    dateTime2 = slotBegin.Value;
    DateTime dateTime5 = dateTime2.AddMinutes(num);
    local2 = new DateTime?(dateTime5);
    PXSelectReadonly<FSAppointment, Where<FSAppointment.scheduledDateTimeBegin, Less<Required<FSAppointment.scheduledDateTimeBegin>>, And<FSAppointment.scheduledDateTimeEnd, Greater<Required<FSAppointment.scheduledDateTimeEnd>>>>, OrderBy<Asc<FSAppointment.scheduledDateTimeBegin>>> pxSelectReadonly = new PXSelectReadonly<FSAppointment, Where<FSAppointment.scheduledDateTimeBegin, Less<Required<FSAppointment.scheduledDateTimeBegin>>, And<FSAppointment.scheduledDateTimeEnd, Greater<Required<FSAppointment.scheduledDateTimeEnd>>>>, OrderBy<Asc<FSAppointment.scheduledDateTimeBegin>>>((PXGraph) this);
    objectList.Add((object) dateTime1);
    objectList.Add((object) date1);
    object[] array = objectList.ToArray();
    foreach (PXResult<FSAppointment> pxResult in ((PXSelectBase<FSAppointment>) pxSelectReadonly).Select(array))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      if (fsAppointment != null)
      {
        nullable3 = fsAppointment.AppointmentID;
        if (nullable3.HasValue)
        {
          nullable1 = fsAppointment.ScheduledDateTimeEnd;
          DateTime? nullable4 = slotBegin;
          if ((nullable1.HasValue & nullable4.HasValue ? (nullable1.GetValueOrDefault() > nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            AppointmentEntry.SlotIsContained slotIsContained = this.SlotIsContainedInSlot(slotBegin, slotEnd, fsAppointment.ScheduledDateTimeBegin, fsAppointment.ScheduledDateTimeEnd);
            if (slotIsContained == AppointmentEntry.SlotIsContained.Contained || slotIsContained == AppointmentEntry.SlotIsContained.PartiallyContained || slotIsContained == AppointmentEntry.SlotIsContained.ExceedsContainment)
            {
              slotBegin = fsAppointment.ScheduledDateTimeEnd;
              dateTime2 = slotBegin.Value;
              slotEnd = new DateTime?(dateTime2.AddMinutes(num));
            }
            if (slotIsContained == AppointmentEntry.SlotIsContained.NotContained)
              break;
          }
        }
      }
    }
    fsAppointmentRow.ScheduledDateTimeBegin = slotBegin;
    fsAppointmentRow.ScheduledDateTimeEnd = slotEnd;
    if (!fsAppointmentRow.ScheduleID.HasValue || fsContractScheduleRow == null)
      return;
    bool? nullable5 = fsContractScheduleRow.RestrictionMax;
    if (!nullable5.GetValueOrDefault())
    {
      nullable5 = fsContractScheduleRow.RestrictionMin;
      if (!nullable5.GetValueOrDefault())
        return;
    }
    if (this.IsAppointmentInValidRestriction(fsAppointmentRow, fsContractScheduleRow))
      return;
    nullable5 = fsContractScheduleRow.RestrictionMin;
    DateTime? nullable6;
    DateTime dateTime6;
    if (nullable5.GetValueOrDefault())
    {
      FSAppointment fsAppointment = fsAppointmentRow;
      int year = fsAppointmentRow.ScheduledDateTimeBegin.Value.Year;
      nullable6 = fsAppointmentRow.ScheduledDateTimeBegin;
      dateTime6 = nullable6.Value;
      int month = dateTime6.Month;
      nullable6 = fsAppointmentRow.ScheduledDateTimeBegin;
      dateTime6 = nullable6.Value;
      int day = dateTime6.Day;
      nullable6 = fsContractScheduleRow.RestrictionMinTime;
      dateTime6 = nullable6.Value;
      int hour = dateTime6.Hour;
      nullable6 = fsContractScheduleRow.RestrictionMinTime;
      dateTime6 = nullable6.Value;
      int minute = dateTime6.Minute;
      nullable6 = fsContractScheduleRow.RestrictionMinTime;
      dateTime6 = nullable6.Value;
      int second = dateTime6.Second;
      DateTime? nullable7 = new DateTime?(new DateTime(year, month, day, hour, minute, second));
      fsAppointment.ScheduledDateTimeBegin = nullable7;
    }
    else
    {
      FSAppointment fsAppointment = fsAppointmentRow;
      int year = fsAppointmentRow.ScheduledDateTimeBegin.Value.Year;
      nullable6 = fsAppointmentRow.ScheduledDateTimeBegin;
      dateTime6 = nullable6.Value;
      int month = dateTime6.Month;
      nullable6 = fsAppointmentRow.ScheduledDateTimeBegin;
      dateTime6 = nullable6.Value;
      int day = dateTime6.Day;
      nullable6 = fsContractScheduleRow.RestrictionMaxTime;
      dateTime6 = nullable6.Value;
      int hour = dateTime6.Hour;
      nullable6 = fsContractScheduleRow.RestrictionMaxTime;
      dateTime6 = nullable6.Value;
      int minute = dateTime6.Minute;
      nullable6 = fsContractScheduleRow.RestrictionMaxTime;
      dateTime6 = nullable6.Value;
      int second = dateTime6.Second;
      DateTime? nullable8 = new DateTime?(new DateTime(year, month, day, hour, minute, second));
      fsAppointment.ScheduledDateTimeBegin = nullable8;
    }
    FSAppointment fsAppointment1 = fsAppointmentRow;
    nullable6 = fsAppointmentRow.ScheduledDateTimeBegin;
    dateTime6 = nullable6.Value;
    DateTime? nullable9 = new DateTime?(dateTime6.AddMinutes(num));
    fsAppointment1.ScheduledDateTimeEnd = nullable9;
  }

  /// <summary>
  /// Verifies if the [fsAppointmentRow].ScheduleTimeBegin and [fsAppointmentRow].ScheduleTimeEnd are valid in the fsContractScheduleRow restrictions.
  /// </summary>
  public virtual bool IsAppointmentInValidRestriction(
    FSAppointment fsAppointmentRow,
    FSContractSchedule fsContractScheduleRow)
  {
    DateTime? nullable;
    if (fsContractScheduleRow.RestrictionMax.GetValueOrDefault())
    {
      int year = fsAppointmentRow.ScheduledDateTimeBegin.Value.Year;
      nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      int month = nullable.Value.Month;
      nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      int day = nullable.Value.Day;
      nullable = fsContractScheduleRow.RestrictionMaxTime;
      int hour = nullable.Value.Hour;
      nullable = fsContractScheduleRow.RestrictionMaxTime;
      int minute = nullable.Value.Minute;
      nullable = fsContractScheduleRow.RestrictionMaxTime;
      int second = nullable.Value.Second;
      DateTime dateTime1 = new DateTime(year, month, day, hour, minute, second);
      nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      DateTime dateTime2 = dateTime1;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() > dateTime2 ? 1 : 0) : 0) != 0)
        return false;
    }
    if (fsContractScheduleRow.RestrictionMin.GetValueOrDefault())
    {
      nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      int year = nullable.Value.Year;
      nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      int month = nullable.Value.Month;
      nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      int day = nullable.Value.Day;
      nullable = fsContractScheduleRow.RestrictionMinTime;
      int hour = nullable.Value.Hour;
      nullable = fsContractScheduleRow.RestrictionMinTime;
      int minute = nullable.Value.Minute;
      nullable = fsContractScheduleRow.RestrictionMinTime;
      int second = nullable.Value.Second;
      DateTime dateTime3 = new DateTime(year, month, day, hour, minute, second);
      nullable = fsAppointmentRow.ScheduledDateTimeBegin;
      DateTime dateTime4 = dateTime3;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime4 ? 1 : 0) : 0) != 0)
        return false;
    }
    return true;
  }

  /// <summary>
  /// Get the specific route in the Routes Module using the [routeID], [routeDocumentID] and [appointmentScheduledDate].
  /// </summary>
  public virtual FSRouteDocument GetOrGenerateRoute(
    int? routeID,
    int? routeDocumentID,
    DateTime? appointmentScheduledDate,
    int? branchID)
  {
    DateTime? nullable1;
    ref DateTime? local1 = ref nullable1;
    DateTime dateTime1 = appointmentScheduledDate.Value;
    int year1 = dateTime1.Year;
    dateTime1 = appointmentScheduledDate.Value;
    int month1 = dateTime1.Month;
    dateTime1 = appointmentScheduledDate.Value;
    int day1 = dateTime1.Day;
    DateTime dateTime2 = new DateTime(year1, month1, day1, 0, 0, 0);
    local1 = new DateTime?(dateTime2);
    DateTime? nullable2;
    ref DateTime? local2 = ref nullable2;
    DateTime dateTime3 = appointmentScheduledDate.Value;
    int year2 = dateTime3.Year;
    dateTime3 = appointmentScheduledDate.Value;
    int month2 = dateTime3.Month;
    dateTime3 = appointmentScheduledDate.Value;
    int day2 = dateTime3.Day;
    DateTime dateTime4 = new DateTime(year2, month2, day2, 23, 59, 59);
    local2 = new DateTime?(dateTime4);
    FSRouteDocument orGenerateRoute;
    if (!routeDocumentID.HasValue)
      orGenerateRoute = PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelect<FSRouteDocument, Where<FSRouteDocument.routeID, Equal<Required<FSRouteDocument.routeID>>, And<FSRouteDocument.timeBegin, Between<Required<FSRouteDocument.timeBegin>, Required<FSRouteDocument.timeBegin>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) routeID,
        (object) nullable1,
        (object) nullable2
      }));
    else
      orGenerateRoute = PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelect<FSRouteDocument, Where<FSRouteDocument.routeID, Equal<Required<FSRouteDocument.routeID>>, And<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>, OrderBy<Desc<FSRouteDocument.routeDocumentID>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) routeID,
        (object) routeDocumentID
      }));
    if (orGenerateRoute == null)
    {
      FSRoute fsRouteRow = PXResultset<FSRoute>.op_Implicit(PXSelectBase<FSRoute, PXSelect<FSRoute, Where<FSRoute.routeID, Equal<Required<FSRoute.routeID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) routeID
      }));
      DateTime? nullable3 = new DateTime?(new DateTime());
      int dayOfWeek = (int) appointmentScheduledDate.Value.DayOfWeek;
      ref DateTime? local3 = ref nullable3;
      SharedFunctions.ValidateExecutionDay(fsRouteRow, (DayOfWeek) dayOfWeek, ref local3);
      if (!nullable3.HasValue)
      {
        ref DateTime? local4 = ref nullable3;
        DateTime dateTime5 = appointmentScheduledDate.Value;
        int year3 = dateTime5.Year;
        dateTime5 = appointmentScheduledDate.Value;
        int month3 = dateTime5.Month;
        dateTime5 = appointmentScheduledDate.Value;
        int day3 = dateTime5.Day;
        DateTime dateTime6 = new DateTime(year3, month3, day3, 0, 0, 0);
        local4 = new DateTime?(dateTime6);
      }
      RouteDocumentMaint instance = PXGraph.CreateInstance<RouteDocumentMaint>();
      FSRouteDocument fsRouteDocument1 = new FSRouteDocument();
      fsRouteDocument1.GeneratedBySystem = new bool?(true);
      fsRouteDocument1.RouteID = routeID;
      fsRouteDocument1.BranchID = branchID;
      fsRouteDocument1.Date = new DateTime?(new DateTime(appointmentScheduledDate.Value.Year, appointmentScheduledDate.Value.Month, appointmentScheduledDate.Value.Day, 0, 0, 0));
      FSRouteDocument fsRouteDocument2 = fsRouteDocument1;
      int year4 = appointmentScheduledDate.Value.Year;
      DateTime dateTime7 = appointmentScheduledDate.Value;
      int month4 = dateTime7.Month;
      dateTime7 = appointmentScheduledDate.Value;
      int day4 = dateTime7.Day;
      dateTime7 = nullable3.Value;
      int hour = dateTime7.Hour;
      dateTime7 = nullable3.Value;
      int minute = dateTime7.Minute;
      DateTime? nullable4 = new DateTime?(new DateTime(year4, month4, day4, hour, minute, 0));
      fsRouteDocument2.TimeBegin = nullable4;
      FSRouteDocument fsRouteDocument3 = fsRouteDocument1;
      dateTime7 = appointmentScheduledDate.Value;
      int year5 = dateTime7.Year;
      dateTime7 = appointmentScheduledDate.Value;
      int month5 = dateTime7.Month;
      dateTime7 = appointmentScheduledDate.Value;
      int day5 = dateTime7.Day;
      DateTime? nullable5 = new DateTime?(new DateTime(year5, month5, day5, 23, 59, 59));
      fsRouteDocument3.TimeEnd = nullable5;
      fsRouteDocument1.Status = "O";
      ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Insert(fsRouteDocument1);
      ((PXAction) instance.Save).Press();
      orGenerateRoute = ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Current;
    }
    return orGenerateRoute;
  }

  /// <summary>
  /// Calculate all the statistics for the routes involving the given appointment.
  /// </summary>
  /// <param name="graph">Context graph instance.</param>
  /// <param name="fsAppointmentRow">FSAppointment Row.</param>
  /// <param name="simpleStatsOnly">Boolean flag that controls whereas only single statistics need to be calculated or not.</param>
  public virtual void CalculateRouteStats(
    FSAppointment fsAppointmentRow,
    string apiKey,
    bool simpleStatsOnly = false)
  {
    PXGraph graph = (PXGraph) this;
    RouteDocumentMaint instance = PXGraph.CreateInstance<RouteDocumentMaint>();
    FSRouteDocument fsRouteDocumentRow1 = PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsAppointmentRow.RouteDocumentID
    }));
    FSRouteDocument fsRouteDocumentRow2 = PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsAppointmentRow.Mem_LastRouteDocumentID
    }));
    if (fsRouteDocumentRow1 != null)
    {
      fsRouteDocumentRow1.RouteStatsUpdated = new bool?(!simpleStatsOnly);
      this.SetRouteSimpleStats(graph, fsAppointmentRow.RouteDocumentID, ref fsRouteDocumentRow1);
      if (!simpleStatsOnly)
        this.SetRouteMapStats(graph, fsAppointmentRow, fsAppointmentRow.RouteDocumentID, ref fsRouteDocumentRow1, apiKey);
      ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Update(fsRouteDocumentRow1);
      ((PXAction) instance.Save).Press();
    }
    if (fsRouteDocumentRow2 == null)
      return;
    this.SetRouteSimpleStats(graph, fsRouteDocumentRow2.RouteDocumentID, ref fsRouteDocumentRow2);
    if (!simpleStatsOnly)
      this.SetRouteMapStats(graph, fsAppointmentRow, fsRouteDocumentRow2.RouteDocumentID, ref fsRouteDocumentRow2, apiKey);
    ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Update(fsRouteDocumentRow2);
    ((PXAction) instance.Save).Press();
  }

  public virtual int? CalculateRouteTotalServicesDuration(PXResultset<FSAppointmentDet> bqlResultSet)
  {
    int? servicesDuration = new int?(0);
    foreach (PXResult<FSAppointmentDet> bqlResult in bqlResultSet)
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(bqlResult);
      int? nullable = servicesDuration;
      int? estimatedDuration = fsAppointmentDet.EstimatedDuration;
      servicesDuration = nullable.HasValue & estimatedDuration.HasValue ? new int?(nullable.GetValueOrDefault() + estimatedDuration.GetValueOrDefault()) : new int?();
    }
    return servicesDuration;
  }

  /// <summary>
  /// Return the total duration of the appointments within a given route.
  /// </summary>
  /// <param name="graph">Context graph instance.</param>
  /// <param name="routeDocumentID">Id for Route Document.</param>
  /// <param name="fsAppointmentRow">FSAppointment object.</param>
  /// <returns>RowCount of appointments.</returns>
  public virtual int? CalculateRouteTotalAppointmentsDuration(
    PXGraph graph,
    int? routeDocumentID,
    FSAppointment fsAppointmentRow)
  {
    int? nullable1 = new int?(0);
    int? nullable2 = fsAppointmentRow.EstimatedDurationTotal;
    int? appointmentsDuration = nullable1.HasValue & nullable2.HasValue ? new int?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new int?();
    foreach (PXResult<FSAppointment> pxResult in PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>, And<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) routeDocumentID,
      (object) fsAppointmentRow.AppointmentID
    }))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      nullable2 = appointmentsDuration;
      int? scheduledDuration = fsAppointment.ScheduledDuration;
      appointmentsDuration = nullable2.HasValue & scheduledDuration.HasValue ? new int?(nullable2.GetValueOrDefault() + scheduledDuration.GetValueOrDefault()) : new int?();
    }
    return appointmentsDuration;
  }

  /// <summary>
  /// Return the total number of appointments for a given route.
  /// </summary>
  public virtual int? GetRouteTotalAppointments(PXGraph graph, int? routeDocumentID)
  {
    return PXSelectBase<FSAppointment, PXSelectGroupBy<FSAppointment, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>, Aggregate<Count<FSAppointment.appointmentID>>>.Config>.Select(graph, new object[1]
    {
      (object) routeDocumentID
    }).RowCount;
  }

  /// <summary>Return the services for a given route.</summary>
  public virtual PXResultset<FSAppointmentDet> GetRouteServices(PXGraph graph, int? routeDocumentID)
  {
    return PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>>>, Where<FSSODet.lineType, Equal<FSLineType.Service>, And<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>>>.Config>.Select(graph, new object[1]
    {
      (object) routeDocumentID
    });
  }

  /// <summary>
  /// Split an array [geoLocationArray] in a list of array of [length] element.
  /// </summary>
  public virtual List<GLocation[]> SplitArrayInList(GLocation[] geoLocationArray, int length)
  {
    List<GLocation[]> glocationArrayList = new List<GLocation[]>();
    int num = 1 + (int) Math.Ceiling(((double) geoLocationArray.Length - (double) length) / ((double) length - 1.0));
    int index1 = 0;
    if (num == 1)
    {
      glocationArrayList.Add(geoLocationArray);
      return glocationArrayList;
    }
    for (int index2 = 0; index2 < num; ++index2)
    {
      int length1 = length;
      GLocation[] glocationArray;
      if (index2 == num - 1 && index2 != 0)
      {
        length1 = geoLocationArray.Length - index1;
        glocationArray = new GLocation[length1 + 1];
      }
      else
        glocationArray = new GLocation[length1];
      for (int index3 = 0; index3 < length1; ++index3)
      {
        if (index2 != 0)
        {
          glocationArray[index3] = geoLocationArray[index1 - 1];
          if (index3 != length1 - 1)
            ++index1;
        }
        else
        {
          glocationArray[index3] = geoLocationArray[index1];
          ++index1;
        }
      }
      if (index2 == num - 1 && index2 != 0)
        glocationArray[length1] = geoLocationArray[geoLocationArray.Length - 1];
      glocationArrayList.Add(glocationArray);
    }
    return glocationArrayList;
  }

  /// <summary>
  /// Calculate the google map statistics for a given route.
  /// </summary>
  /// <param name="graph">Context graph instance.</param>
  /// <param name="routeDocumentID">ID for the route.</param>
  /// <param name="totalDistance">Total driving distance in meters.</param>
  /// <param name="totalDistanceFriendly">Total driving distance user friendly.</param>
  /// <param name="totalDuration">Total driving duration in seconds.</param>
  public virtual void CalculateRouteMapStats(
    PXGraph graph,
    int? routeDocumentID,
    ref Decimal? totalDistance,
    ref string totalDistanceFriendly,
    ref int? totalDuration,
    string apiKey)
  {
    FSRouteDocument fsRouteDocument = PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>.Config>.Select(graph, new object[1]
    {
      (object) routeDocumentID
    }));
    PXResultset<FSAppointment> pxResultset = PXSelectBase<FSAppointment, PXSelectReadonly2<FSAppointment, InnerJoin<FSServiceOrder, On<FSAppointment.sOID, Equal<FSServiceOrder.sOID>>, InnerJoin<FSContact, On<FSContact.contactID, Equal<FSServiceOrder.serviceOrderContactID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>>>>, Where<FSAppointment.routeDocumentID, Equal<Required<FSAppointment.routeDocumentID>>>, OrderBy<Asc<FSAppointment.routePosition>>>.Config>.Select(graph, new object[1]
    {
      (object) routeDocumentID
    });
    List<GLocation> glocationList = new List<GLocation>();
    FSBranchLocation fsBranchLocationRow1 = PXResultset<FSBranchLocation>.op_Implicit(PXSelectBase<FSBranchLocation, PXSelectJoin<FSBranchLocation, InnerJoin<FSRoute, On<FSRoute.beginBranchLocationID, Equal<FSBranchLocation.branchLocationID>>>, Where<FSRoute.routeID, Equal<Required<FSRoute.routeID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsRouteDocument.RouteID
    }));
    FSBranchLocation fsBranchLocationRow2 = PXResultset<FSBranchLocation>.op_Implicit(PXSelectBase<FSBranchLocation, PXSelectJoin<FSBranchLocation, InnerJoin<FSRoute, On<FSRoute.endBranchLocationID, Equal<FSBranchLocation.branchLocationID>>>, Where<FSRoute.routeID, Equal<Required<FSRoute.routeID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsRouteDocument.RouteID
    }));
    string branchLocationAddress1 = SharedFunctions.GetBranchLocationAddress(graph, fsBranchLocationRow1);
    if (!string.IsNullOrEmpty(branchLocationAddress1))
      glocationList.Add(new GLocation(branchLocationAddress1));
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    instance.CalculateGoogleStats = false;
    List<FSAppointment> source = new List<FSAppointment>();
    foreach (PXResult<FSAppointment, FSServiceOrder, FSContact, FSAddress> pxResult in pxResultset)
    {
      source.Add(PXResult<FSAppointment, FSServiceOrder, FSContact, FSAddress>.op_Implicit(pxResult));
      PXResult<FSAppointment, FSServiceOrder, FSContact, FSAddress>.op_Implicit(pxResult);
      string appointmentAddress = SharedFunctions.GetAppointmentAddress(PXResult<FSAppointment, FSServiceOrder, FSContact, FSAddress>.op_Implicit(pxResult));
      if (!string.IsNullOrEmpty(appointmentAddress))
        glocationList.Add(new GLocation(appointmentAddress));
    }
    string branchLocationAddress2 = SharedFunctions.GetBranchLocationAddress(graph, fsBranchLocationRow2);
    if (!string.IsNullOrEmpty(branchLocationAddress2))
      glocationList.Add(new GLocation(branchLocationAddress2));
    List<GLocation[]> glocationArrayList = this.SplitArrayInList(glocationList.ToArray(), 10);
    try
    {
      int index = 0;
      double num1 = 0.0;
      totalDuration = new int?(0);
      totalDistance = new Decimal?(0M);
      DateTime? nullable1 = new DateTime?(DateTime.Now);
      foreach (GLocation[] glocationArray in glocationArrayList)
      {
        Route route = RouteDirections.GetRoute("distance", apiKey, glocationArray);
        if (route != null)
        {
          string distanceDescr = route.Legs[0].DistanceDescr;
          string str = distanceDescr.Substring(distanceDescr.IndexOf(" "));
          int? nullable2;
          foreach (RouteLeg leg in route.Legs)
          {
            if (source.ElementAtOrDefault<FSAppointment>(index) != null)
            {
              ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) source.ElementAt<FSAppointment>(index).RefNbr, new object[1]
              {
                (object) source.ElementAt<FSAppointment>(index).SrvOrdType
              }));
              FSAppointment current = ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current;
              nullable2 = current.EstimatedDurationTotal;
              double num2 = (double) nullable2.Value;
              double num3 = num2 == 0.0 ? 1.0 : num2;
              DateTime? nullable3;
              DateTime dateTime;
              if (index == 0)
              {
                FSAppointment fsAppointment1 = current;
                nullable3 = current.ScheduledDateTimeBegin;
                dateTime = nullable3.Value;
                int year = dateTime.Year;
                nullable3 = current.ScheduledDateTimeBegin;
                dateTime = nullable3.Value;
                int month = dateTime.Month;
                nullable3 = current.ScheduledDateTimeBegin;
                dateTime = nullable3.Value;
                int day = dateTime.Day;
                nullable3 = fsRouteDocument.TimeBegin;
                dateTime = nullable3.Value;
                int hour = dateTime.Hour;
                nullable3 = fsRouteDocument.TimeBegin;
                dateTime = nullable3.Value;
                int minute = dateTime.Minute;
                nullable3 = fsRouteDocument.TimeBegin;
                dateTime = nullable3.Value;
                int second = dateTime.Second;
                DateTime? nullable4 = new DateTime?(new DateTime(year, month, day, hour, minute, second));
                fsAppointment1.ScheduledDateTimeBegin = nullable4;
                FSAppointment fsAppointment2 = current;
                nullable3 = current.ScheduledDateTimeBegin;
                dateTime = nullable3.Value;
                DateTime? nullable5 = new DateTime?(dateTime.AddSeconds((double) leg.Duration));
                fsAppointment2.ScheduledDateTimeBegin = nullable5;
              }
              else
              {
                current.ScheduledDateTimeBegin = nullable1;
                FSAppointment fsAppointment = current;
                nullable3 = current.ScheduledDateTimeBegin;
                dateTime = nullable3.Value;
                DateTime? nullable6 = new DateTime?(dateTime.AddSeconds((double) leg.Duration));
                fsAppointment.ScheduledDateTimeBegin = nullable6;
              }
              current.ScheduledDateTimeEnd = current.ScheduledDateTimeBegin;
              FSAppointment fsAppointment3 = current;
              nullable3 = current.ScheduledDateTimeEnd;
              dateTime = nullable3.Value;
              DateTime? nullable7 = new DateTime?(dateTime.AddMinutes(num3));
              fsAppointment3.ScheduledDateTimeEnd = nullable7;
              nullable3 = current.ScheduledDateTimeEnd;
              DateTime? scheduledDateTimeEnd = current.ScheduledDateTimeEnd;
              if (nullable3.HasValue & scheduledDateTimeEnd.HasValue)
              {
                TimeSpan timeSpan = nullable3.GetValueOrDefault() - scheduledDateTimeEnd.GetValueOrDefault();
              }
              PXUpdate<Set<FSAppointment.scheduledDateTimeBegin, Required<FSAppointment.scheduledDateTimeBegin>, Set<FSAppointment.scheduledDateTimeEnd, Required<FSAppointment.scheduledDateTimeEnd>, Set<FSAppointment.routePosition, Required<FSAppointment.routePosition>>>>, FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Update(graph, new object[4]
              {
                (object) current.ScheduledDateTimeBegin,
                (object) current.ScheduledDateTimeEnd,
                (object) current.RoutePosition,
                (object) current.AppointmentID
              });
              nullable1 = current.ScheduledDateTimeEnd;
            }
            int length = leg.DistanceDescr.IndexOf(" ");
            num1 += Convert.ToDouble(leg.DistanceDescr.Substring(0, length));
            ++index;
          }
          ref Decimal? local1 = ref totalDistance;
          Decimal? nullable8 = totalDistance;
          Decimal distance = (Decimal) route.Distance;
          Decimal? nullable9 = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + distance) : new Decimal?();
          local1 = nullable9;
          totalDistanceFriendly = $"{num1.ToString()} {str}";
          ref int? local2 = ref totalDuration;
          nullable2 = totalDuration;
          int num4 = route.Duration / 60;
          int? nullable10 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + num4) : new int?();
          local2 = nullable10;
        }
      }
      graph.SelectTimeStamp();
    }
    catch (Exception ex)
    {
    }
  }

  /// <summary>Set the simple stats for a given route.</summary>
  /// <param name="graph">Context graph instance.</param>
  /// <param name="routeDocumentID">ID of the route.</param>
  /// <param name="fsRouteDocumentRow">FSRoute object.</param>
  public virtual void SetRouteSimpleStats(
    PXGraph graph,
    int? routeDocumentID,
    ref FSRouteDocument fsRouteDocumentRow)
  {
    fsRouteDocumentRow.TotalNumAppointments = this.GetRouteTotalAppointments(graph, routeDocumentID);
    PXResultset<FSAppointmentDet> routeServices = this.GetRouteServices(graph, routeDocumentID);
    fsRouteDocumentRow.TotalServices = new int?(routeServices.Count);
    fsRouteDocumentRow.TotalServicesDuration = this.CalculateRouteTotalServicesDuration(routeServices);
  }

  public virtual void SetRouteMapStats(
    PXGraph graph,
    FSAppointment fsAppointmentRow,
    int? routeDocumentID,
    ref FSRouteDocument fsRouteDocumentRow,
    string apiKey)
  {
    Decimal? totalDistance = new Decimal?();
    string totalDistanceFriendly = (string) null;
    int? totalDuration1 = new int?();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        this.CalculateRouteMapStats(graph, routeDocumentID, ref totalDistance, ref totalDistanceFriendly, ref totalDuration1, apiKey);
        transactionScope.Complete();
      }
      catch
      {
        transactionScope.Dispose();
      }
    }
    if (totalDistance.HasValue)
      fsRouteDocumentRow.TotalDistance = totalDistance;
    if (totalDistanceFriendly != null)
      fsRouteDocumentRow.TotalDistanceFriendly = totalDistanceFriendly;
    if (!totalDuration1.HasValue)
      return;
    fsRouteDocumentRow.TotalDuration = totalDuration1;
    FSRouteDocument fsRouteDocument = fsRouteDocumentRow;
    int? appointmentsDuration = this.CalculateRouteTotalAppointmentsDuration(graph, routeDocumentID, fsAppointmentRow);
    int? totalDuration2 = fsRouteDocumentRow.TotalDuration;
    int? nullable = appointmentsDuration.HasValue & totalDuration2.HasValue ? new int?(appointmentsDuration.GetValueOrDefault() + totalDuration2.GetValueOrDefault()) : new int?();
    fsRouteDocument.TotalTravelTime = nullable;
  }

  public virtual void ResetLatLong(FSSrvOrdType fsSrvOrdTypeRow)
  {
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current;
    if (fsSrvOrdTypeRow == null || current == null || !(fsSrvOrdTypeRow.Behavior == "RO"))
      return;
    current.MapLatitude = new Decimal?();
    current.MapLongitude = new Decimal?();
  }

  private void SetGeoLocation(FSAddress fsAddressRow, FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsSrvOrdTypeRow == null || !(fsSrvOrdTypeRow.Behavior == "RO"))
      return;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current;
    if (current == null)
      return;
    Decimal? nullable = current.MapLatitude;
    if (nullable.HasValue)
    {
      nullable = current.MapLongitude;
      if (nullable.HasValue)
        return;
    }
    try
    {
      GLocation[] glocationArray = Geocoder.Geocode(SharedFunctions.GetAppointmentAddress(fsAddressRow), ((PXSelectBase<FSSetup>) this.SetupRecord).Current.MapApiKey);
      if (glocationArray == null || glocationArray.Length == 0)
        return;
      current.MapLatitude = new Decimal?((Decimal) glocationArray[0].LatLng.Latitude);
      current.MapLongitude = new Decimal?((Decimal) glocationArray[0].LatLng.Longitude);
    }
    catch
    {
    }
  }

  /// <summary>
  /// Return true if the current appointment has at least one <c>FSAppointmentEmployee</c> row with employee or employee combined type.
  /// </summary>
  public virtual bool AreThereAnyEmployees()
  {
    return PXResultset<FSAppointmentEmployee>.op_Implicit(PXSelectBase<FSAppointmentEmployee, PXSelect<FSAppointmentEmployee, Where<Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<Where<FSAppointmentEmployee.type, Equal<BAccountType.employeeType>, Or<FSAppointmentEmployee.type, Equal<BAccountType.empCombinedType>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null;
  }

  public virtual void HideOrShowRouteInfo(FSAppointment fsAppointmentRow)
  {
    PXUIFieldAttribute.SetVisible<FSAppointment.routeID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, fsAppointmentRow.IsRouteAppoinment.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<FSAppointment.routeDocumentID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, fsAppointmentRow.IsRouteAppoinment.GetValueOrDefault());
  }

  /// <summary>
  /// Hides or shows fields related to the Employee Time Cards Integration.
  /// </summary>
  public virtual void HideOrShowTimeCardsIntegration(PXCache cache, FSAppointment fsAppointmentRow)
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null)
      return;
    bool flag1 = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.EnableEmpTimeCardIntegration.GetValueOrDefault() && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.CreateTimeActivitiesFromAppointment.GetValueOrDefault();
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() && PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>();
    PXUIFieldAttribute.SetVisible<FSAppointmentLog.timeCardCD>(((PXSelectBase) this.LogRecords).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<FSAppointmentLog.approvedTime>(((PXSelectBase) this.LogRecords).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisibility<FSAppointmentLog.isBillable>(((PXSelectBase) this.LogRecords).Cache, (object) null, !flag1 || !flag2 ? (PXUIVisibility) 1 : (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<FSAppointmentLog.billableTimeDuration>(((PXSelectBase) this.LogRecords).Cache, (object) null, !flag1 || !flag2 ? (PXUIVisibility) 1 : (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<FSAppointmentLog.curyBillableTranAmount>(((PXSelectBase) this.LogRecords).Cache, (object) null, !flag1 || !flag2 ? (PXUIVisibility) 1 : (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisible<FSAppointmentLog.earningType>(((PXSelectBase) this.LogRecords).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<FSAppointmentLog.trackTime>(((PXSelectBase) this.LogRecords).Cache, (object) null, flag1);
  }

  /// <summary>
  /// Sets the BillServiceContractID field from the ServiceOrder's ServiceContractID/BillServiceContractID field depending on the contract's billing type.
  /// </summary>
  public virtual void SetBillServiceContractIDFromSO(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrder)
  {
    if (fsServiceOrder.ServiceContractID.HasValue)
    {
      FSServiceContract fsServiceContract = FSServiceContract.PK.Find(cache.Graph, fsServiceOrder.ServiceContractID);
      if (fsServiceContract == null)
        return;
      int num = fsServiceContract.BillingType == "STDB" ? 1 : 0;
      string billingMode = this.GetBillingMode(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
      if (num == 0 || !(billingMode == "AP"))
        return;
      cache.SetValueExt<FSAppointment.billServiceContractID>((object) fsAppointmentRow, (object) fsServiceContract.ServiceContractID);
    }
    else
    {
      if (!fsServiceOrder.BillServiceContractID.HasValue)
        return;
      FSServiceContract fsServiceContract = FSServiceContract.PK.Find(cache.Graph, fsServiceOrder.BillServiceContractID);
      if (fsServiceContract == null)
        return;
      cache.SetValueExt<FSAppointment.billServiceContractID>((object) fsAppointmentRow, (object) fsServiceContract.ServiceContractID);
    }
  }

  protected virtual void SetServiceOrderRelatedBySORefNbr(
    PXCache cache,
    FSAppointment fsAppointmentRow)
  {
    FSServiceOrder fsServiceOrder = (FSServiceOrder) PXSelectorAttribute.Select<FSAppointment.soRefNbr>(cache, (object) fsAppointmentRow);
    fsAppointmentRow.SOID = fsServiceOrder.SOID;
    this.LoadServiceOrderRelated(fsAppointmentRow);
    PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this.GetServiceOrderEntryGraph(false), new object[1]
    {
      (object) fsServiceOrder.CuryInfoID
    })));
    copy.CuryInfoID = new long?();
    ((PXSelectBase) this.currencyInfoView).Cache.Clear();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyInfoView).Insert(copy);
    cache.SetValue<FSAppointment.curyID>((object) fsAppointmentRow, (object) fsServiceOrder.CuryID);
    cache.SetValue<FSAppointment.curyInfoID>((object) fsAppointmentRow, (object) currencyInfo.CuryInfoID);
    cache.SetValueExt<FSAppointment.taxCalcMode>((object) fsAppointmentRow, (object) fsServiceOrder.TaxCalcMode);
    cache.SetValueExt<FSAppointment.projectID>((object) fsAppointmentRow, (object) fsServiceOrder.ProjectID);
    cache.SetValueExt<FSAppointment.taxZoneID>((object) fsAppointmentRow, (object) fsServiceOrder.TaxZoneID);
    cache.SetValueExt<FSAppointment.dfltProjectTaskID>((object) fsAppointmentRow, (object) fsServiceOrder.DfltProjectTaskID);
    cache.SetDefaultExt<FSAppointment.salesPersonID>((object) fsAppointmentRow);
    cache.SetDefaultExt<FSAppointment.commissionable>((object) fsAppointmentRow);
    cache.SetValueExt<FSAppointment.entityUsageType>((object) fsAppointmentRow, (object) fsServiceOrder.EntityUsageType);
    cache.SetValueExt<FSAppointment.externalTaxExemptionNumber>((object) fsAppointmentRow, (object) fsServiceOrder.ExternalTaxExemptionNumber);
    this.SetBillServiceContractIDFromSO(cache, fsAppointmentRow, fsServiceOrder);
    if (fsAppointmentRow.DocDesc != null)
      return;
    fsAppointmentRow.DocDesc = fsServiceOrder.DocDesc;
  }

  protected virtual bool CanExecuteAppointmentRowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    FSAppointment row = (FSAppointment) e.Row;
    if (string.IsNullOrWhiteSpace(row.SrvOrdType))
      GraphHelper.RaiseRowPersistingException<FSAppointment.srvOrdType>(cache, e.Row);
    this.LoadServiceOrderRelated(row);
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current == null)
      throw new PXException("Technical Error: ServiceOrderRelated.Current is NULL.");
    this.BackupOriginalValues(cache, e);
    if ((e.Operation & 3) != 3)
    {
      int? soid = row.SOID;
      int num = 0;
      if (soid.GetValueOrDefault() < num & soid.HasValue)
      {
        row.SOID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SOID;
        row.SORefNbr = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.RefNbr;
      }
    }
    return true;
  }

  public virtual bool UpdateServiceOrder(
    FSAppointment fsAppointmentRow,
    AppointmentEntry graphAppointmentEntry,
    object rowInProcessing,
    PXDBOperation operation,
    PXTranStatus? tranStatus)
  {
    if (this.CatchedServiceOrderUpdateException != null)
      return false;
    try
    {
      return this.UpdateServiceOrderWithoutErrorHandler(fsAppointmentRow, graphAppointmentEntry, rowInProcessing, operation, tranStatus);
    }
    catch (ServiceOrderUpdateException ex)
    {
      this.CatchedServiceOrderUpdateException = (Exception) ex;
      return false;
    }
    catch (ServiceOrderUpdateException2 ex)
    {
      this.CatchedServiceOrderUpdateException = (Exception) ex;
      return false;
    }
  }

  public virtual bool UpdateServiceOrderWithoutErrorHandler(
    FSAppointment fsAppointmentRow,
    AppointmentEntry graphAppointmentEntry,
    object rowInProcessing,
    PXDBOperation operation,
    PXTranStatus? tranStatus)
  {
    if (this.serviceOrderIsAlreadyUpdated || this.SkipServiceOrderUpdate || fsAppointmentRow == null || !fsAppointmentRow.MustUpdateServiceOrder.GetValueOrDefault())
      return true;
    if (tranStatus.HasValue && tranStatus.GetValueOrDefault() == 2)
      return false;
    bool flag1 = false;
    bool flag2 = false;
    PXEntryStatus status1 = ((PXSelectBase) graphAppointmentEntry.AppointmentRecords).Cache.GetStatus((object) fsAppointmentRow);
    if (status1 == 3)
    {
      if (!tranStatus.HasValue || tranStatus.GetValueOrDefault() != 1 || operation != 3 || !(rowInProcessing is FSAppointment))
        return true;
      flag1 = true;
    }
    ServiceOrderEntry serviceOrderEntryGraph = this.GetServiceOrderEntryGraph(true);
    serviceOrderEntryGraph.DisableServiceOrderUnboundFieldCalc = true;
    ServiceOrderEntry soGraph = serviceOrderEntryGraph;
    AppointmentEntry apptGraph = graphAppointmentEntry;
    if (!serviceOrderEntryGraph.RunningPersist)
      ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fsAppointmentRow.SORefNbr, new object[1]
      {
        (object) fsAppointmentRow.SrvOrdType
      }));
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current;
    if (current == null || current.SrvOrdType != fsAppointmentRow.SrvOrdType || current.RefNbr != fsAppointmentRow.SORefNbr)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSServiceOrder))
      });
    bool? nullable;
    if (!flag1)
    {
      if (status1 != 2)
      {
        bool? valueOriginal1 = (bool?) ((PXSelectBase) graphAppointmentEntry.AppointmentRecords).Cache.GetValueOriginal<FSAppointment.notStarted>((object) fsAppointmentRow);
        bool? valueOriginal2 = (bool?) ((PXSelectBase) graphAppointmentEntry.AppointmentRecords).Cache.GetValueOriginal<FSAppointment.inProcess>((object) fsAppointmentRow);
        flag2 = (valueOriginal1.GetValueOrDefault() ? 1 : (valueOriginal2.GetValueOrDefault() ? 1 : 0)) != (fsAppointmentRow.NotStarted.GetValueOrDefault() ? 1 : (fsAppointmentRow.InProcess.GetValueOrDefault() ? 1 : 0));
        bool? finished = fsAppointmentRow.Finished;
        bool? valueOriginal3 = (bool?) ((PXSelectBase) graphAppointmentEntry.AppointmentRecords).Cache.GetValueOriginal<FSAppointment.finished>((object) fsAppointmentRow);
        if (!(finished.GetValueOrDefault() == valueOriginal3.GetValueOrDefault() & finished.HasValue == valueOriginal3.HasValue))
          flag2 = true;
      }
      else if (fsAppointmentRow.NotStarted.GetValueOrDefault() || fsAppointmentRow.InProcess.GetValueOrDefault())
      {
        nullable = fsAppointmentRow.Finished;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          flag2 = true;
      }
      if (!flag2)
        flag2 = this.IsThereAnySODetReferenceBeingDeleted<FSAppointmentDet.sODetID>(((PXSelectBase) graphAppointmentEntry.AppointmentDetails).Cache);
    }
    long? curyInfoId = ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current.CuryInfoID;
    long num = 0;
    if (curyInfoId.GetValueOrDefault() < num & curyInfoId.HasValue)
      ((PXSelectBase) serviceOrderEntryGraph.ServiceOrderRecords).Cache.SetValueExt<FSServiceOrder.curyInfoID>((object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current, (object) fsAppointmentRow.CuryInfoID);
    PXEntryStatus status2 = ((PXSelectBase) this.ServiceOrderRelated).Cache.GetStatus((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
    if (flag1 || flag2 || status2 != null)
    {
      ((PXSelectBase) serviceOrderEntryGraph.ServiceOrderRecords).Cache.SetStatus((object) current, (PXEntryStatus) 1);
      ((PXSelectBase) serviceOrderEntryGraph.ServiceOrderRecords).Cache.IsDirty = true;
    }
    this.UpdateRelatedApptSummaryFieldsByDeletedLines(soGraph, graphAppointmentEntry);
    if (flag1)
    {
      try
      {
        serviceOrderEntryGraph.GraphAppointmentEntryCaller = (AppointmentEntry) null;
        serviceOrderEntryGraph.ForceAppointmentCheckings = true;
        ((PXAction) serviceOrderEntryGraph.Save).Press();
        this.serviceOrderIsAlreadyUpdated = true;
      }
      catch (Exception ex)
      {
        this.ReplicateServiceOrderExceptions(graphAppointmentEntry, serviceOrderEntryGraph, ex);
        this.VerifyIfTransactionWasAborted((PXGraph) serviceOrderEntryGraph, ex);
        return false;
      }
      finally
      {
        serviceOrderEntryGraph.ForceAppointmentCheckings = false;
      }
    }
    else
    {
      PXResultset<FSAppointmentDet> apptLines = ((PXSelectBase<FSAppointmentDet>) graphAppointmentEntry.AppointmentDetails).Select(Array.Empty<object>());
      List<FSApptLineSplit> apptSplits = new List<FSApptLineSplit>();
      serviceOrderEntryGraph.SkipTaxCalcTotals = true;
      if (status2 == 2 && ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current.BillServiceContractID.HasValue)
        ((PXSelectBase) serviceOrderEntryGraph.ServiceOrderRecords).Cache.SetValueExt<FSServiceOrder.billServiceContractID>((object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current, (object) ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current.BillServiceContractID);
      PXResultset<FSAppointmentDet> source = apptLines;
      Expression<Func<PXResult<FSAppointmentDet>, bool>> predicate = (Expression<Func<PXResult<FSAppointmentDet>, bool>>) (x => ((FSAppointmentDet) x).LineType != "PU_DL");
      foreach (PXResult<FSAppointmentDet> pxResult in (IEnumerable<PXResult<FSAppointmentDet>>) ((IQueryable<PXResult<FSAppointmentDet>>) source).Where<PXResult<FSAppointmentDet>>(predicate))
      {
        FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
        PXEntryStatus status3 = ((PXSelectBase) graphAppointmentEntry.AppointmentDetails).Cache.GetStatus((object) fsAppointmentDet);
        nullable = fsAppointmentRow.EditActionRunning;
        if (nullable.GetValueOrDefault() && this.UpdateServiceOrderWhenAppointmentEdit(fsAppointmentRow))
        {
          FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) soGraph.ServiceOrderDetails).Search<FSSODet.sODetID>((object) fsAppointmentDet.SODetID, Array.Empty<object>()));
          fsAppointmentDet.FSSODetRow = ((PXSelectBase<FSSODet>) soGraph.ServiceOrderDetails).Current = fssoDet;
          serviceOrderEntryGraph.ScheduleServiceOrderDetailLine(((PXSelectBase) this.AppointmentDetails).Cache, fsAppointmentDet, ((PXSelectBase) serviceOrderEntryGraph.ServiceOrderDetails).Cache, fsAppointmentDet.FSSODetRow);
        }
        if (status3 == 2 || status3 == 1)
        {
          ((PXSelectBase<FSAppointmentDet>) apptGraph.AppointmentDetails).Current = fsAppointmentDet;
          this.InsertUpdateSODet(((PXSelectBase) graphAppointmentEntry.AppointmentDetails).Cache, fsAppointmentDet, (PXSelectBase<FSSODet>) serviceOrderEntryGraph.ServiceOrderDetails, fsAppointmentRow);
          List<FSApptLineSplit> list = GraphHelper.RowCast<FSApptLineSplit>((IEnumerable) ((PXSelectBase<FSApptLineSplit>) apptGraph.Splits).Select(Array.Empty<object>())).Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (r => !string.IsNullOrEmpty(r.LotSerialNbr))).ToList<FSApptLineSplit>();
          this.UpdateSrvOrdSplits(apptGraph, fsAppointmentDet, list, soGraph);
          apptSplits.AddRange((IEnumerable<FSApptLineSplit>) list);
          serviceOrderEntryGraph.UpdateRelatedApptSummaryFields(((PXSelectBase) this.AppointmentDetails).Cache, fsAppointmentDet, ((PXSelectBase) serviceOrderEntryGraph.ServiceOrderDetails).Cache, fsAppointmentDet.FSSODetRow);
        }
      }
      try
      {
        serviceOrderEntryGraph.GraphAppointmentEntryCaller = graphAppointmentEntry;
        serviceOrderEntryGraph.ForceAppointmentCheckings = flag2;
        if (this.insertingServiceOrder)
        {
          serviceOrderEntryGraph.Answers.Select(Array.Empty<object>());
          serviceOrderEntryGraph.Answers.CopyAttributes((PXGraph) serviceOrderEntryGraph, (object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current, (PXGraph) graphAppointmentEntry, (object) ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current, true);
          this.insertingServiceOrder = false;
        }
        if (serviceOrderEntryGraph.ForceAppointmentCheckings || ((PXGraph) serviceOrderEntryGraph).IsDirty)
        {
          serviceOrderEntryGraph.SkipTaxCalcTotals = false;
          ((PXGraph) serviceOrderEntryGraph).GetExtension<ServiceOrderEntry.SalesTax>().CalcTaxes();
          if (!serviceOrderEntryGraph.RunningPersist)
          {
            using (new SuppressErpTransactionsScope(true))
            {
              ((PXGraph) serviceOrderEntryGraph).SelectTimeStamp();
              serviceOrderEntryGraph.SkipTaxCalcAndSave();
              serviceOrderEntryGraph.RecalculateExternalTaxes();
            }
          }
        }
        this.serviceOrderIsAlreadyUpdated = true;
      }
      catch (Exception ex)
      {
        this.ReplicateServiceOrderExceptions(graphAppointmentEntry, serviceOrderEntryGraph, ex);
        this.VerifyIfTransactionWasAborted((PXGraph) serviceOrderEntryGraph, ex);
        return false;
      }
      finally
      {
        serviceOrderEntryGraph.GraphAppointmentEntryCaller = (AppointmentEntry) null;
        serviceOrderEntryGraph.ForceAppointmentCheckings = false;
        serviceOrderEntryGraph.SkipTaxCalcTotals = false;
      }
      this.FillDictionaryWithUpdatedFSSODets(apptLines);
      this.FillDictionaryWithUpdatedFSSODetSplits(apptSplits);
    }
    if (!flag1 && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null && ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current != null)
    {
      int? soid1 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SOID;
      int? soid2 = ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current.SOID;
      if (soid1.GetValueOrDefault() == soid2.GetValueOrDefault() & soid1.HasValue == soid2.HasValue)
      {
        foreach (string field in (List<string>) ((PXSelectBase) this.ServiceOrderRelated).Cache.Fields)
        {
          if (!(field == "AppointmentsCompletedCntr") && !(field == "AppointmentsCompletedOrClosedCntr"))
            ((PXSelectBase) this.ServiceOrderRelated).Cache.SetValue((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, field, ((PXSelectBase) serviceOrderEntryGraph.ServiceOrderRecords).Cache.GetValue((object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntryGraph.ServiceOrderRecords).Current, field));
        }
      }
    }
    return true;
  }

  public virtual void VerifyIfTransactionWasAborted(PXGraph graph, Exception exception)
  {
    string str = PXMessages.LocalizeNoPrefix("{0} '{1}' record raised at least one error. Please review the errors.").Replace("{0}", "").Replace("{1}", "").Replace("''", "");
    if (exception.Message.Contains(str.Trim()))
      throw PXException.PreserveStack(exception);
  }

  public virtual void UpdateRelatedApptSummaryFieldsByDeletedLines(
    ServiceOrderEntry soGraph,
    AppointmentEntry graphAppointmentEntry)
  {
    foreach (FSAppointmentDet apptLine in ((PXSelectBase) graphAppointmentEntry.AppointmentDetails).Cache.Deleted)
    {
      if (!(apptLine.LineType == "PU_DL"))
      {
        int? soDetId = apptLine.SODetID;
        if (soDetId.HasValue)
        {
          soDetId = apptLine.SODetID;
          int num = 0;
          if (!(soDetId.GetValueOrDefault() < num & soDetId.HasValue))
          {
            apptLine.FSSODetRow = FSSODet.UK.Find((PXGraph) soGraph, apptLine.SODetID);
            if (apptLine.FSSODetRow != null)
              soGraph.UpdateRelatedApptSummaryFields(((PXSelectBase) this.AppointmentDetails).Cache, apptLine, ((PXSelectBase) soGraph.ServiceOrderDetails).Cache, apptLine.FSSODetRow);
          }
        }
      }
    }
  }

  public virtual void ValidateLogDateTime(PXCache logCache, FSAppointmentLog logRow)
  {
    if (logRow == null)
      return;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    if (current == null || !(logRow.ItemType != "TR"))
      return;
    DateTime? nullable1 = current.ActualDateTimeBegin;
    DateTime? nullable2 = logRow.DateTimeBegin;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      logCache.RaiseExceptionHandling<FSAppointmentLog.dateTimeBegin>((object) logRow, (object) logRow.DateTimeBegin, (Exception) new PXSetPropertyException("The log start date and time is earlier than the appointment actual start date and time specified on the Settings tab.", (PXErrorLevel) 2));
    nullable2 = current.ActualDateTimeEnd;
    nullable1 = logRow.DateTimeEnd;
    if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    logCache.RaiseExceptionHandling<FSAppointmentLog.dateTimeEnd>((object) logRow, (object) logRow.DateTimeEnd, (Exception) new PXSetPropertyException("The log end date and time is later than the appointment actual end date and time specified on the Settings tab.", (PXErrorLevel) 2));
  }

  private void UpdateSrvOrdSplits(
    AppointmentEntry apptGraph,
    FSAppointmentDet apptLine,
    List<FSApptLineSplit> apptSplits,
    ServiceOrderEntry soGraph)
  {
    int? soDetId1;
    int? soDetId2;
    if (apptLine.SODetID.HasValue)
    {
      soDetId1 = apptLine.SODetID;
      soDetId2 = apptLine.FSSODetRow.SODetID;
      if (!(soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue))
        throw new PXArgumentException();
    }
    if (!(this.GetLotSerialClass(apptLine.InventoryID)?.LotSerAssign == "U"))
    {
      FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) soGraph.ServiceOrderDetails).Search<FSSODet.sODetID>((object) apptLine.FSSODetRow.SODetID, Array.Empty<object>()));
      if (fssoDet != null)
      {
        soDetId2 = fssoDet.SODetID;
        soDetId1 = apptLine.FSSODetRow.SODetID;
        if (soDetId2.GetValueOrDefault() == soDetId1.GetValueOrDefault() & soDetId2.HasValue == soDetId1.HasValue)
        {
          apptLine.FSSODetRow = ((PXSelectBase<FSSODet>) soGraph.ServiceOrderDetails).Current = fssoDet;
          Decimal? nullable1 = fssoDet.BaseEstimatedQty;
          Decimal num1 = nullable1.Value;
          int num2 = 0;
          FSSODetSplit fssoDetSplit1 = (FSSODetSplit) null;
          foreach (FSApptLineSplit apptSplit1 in apptSplits)
          {
            FSApptLineSplit apptSplit = apptSplit1;
            FSSODetSplit fssoDetSplit2 = ((IEnumerable<FSSODetSplit>) ((PXSelectBase<FSSODetSplit>) soGraph.Splits).SelectMain(Array.Empty<object>())).FirstOrDefault<FSSODetSplit>((Func<FSSODetSplit, bool>) (r => string.Equals(r.LotSerialNbr, apptSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase)));
            if (fssoDetSplit2 == null)
            {
              FSSODetSplit fssoDetSplit3 = new FSSODetSplit();
              FSSODetSplit copy = (FSSODetSplit) ((PXSelectBase) soGraph.Splits).Cache.CreateCopy((object) ((PXSelectBase<FSSODetSplit>) soGraph.Splits).Insert(fssoDetSplit3));
              copy.LotSerialNbr = apptSplit.LotSerialNbr;
              copy.Qty = apptSplit.Qty;
              FSSODetSplit fssoDetSplit4 = copy;
              PXCache cache = ((PXSelectBase) soGraph.Splits).Cache;
              int? inventoryId = copy.InventoryID;
              string uom = copy.UOM;
              nullable1 = copy.Qty;
              Decimal num3 = nullable1.Value;
              Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertToBase(cache, inventoryId, uom, num3, INPrecision.QUANTITY));
              fssoDetSplit4.BaseQty = nullable2;
              FSSODetSplit errorSourceRow = ((PXSelectBase<FSSODetSplit>) soGraph.Splits).Update(copy);
              this.PropagateServiceOrderErrors(((PXSelectBase) soGraph.Splits).Cache, (object) errorSourceRow, ((PXSelectBase) apptGraph.Splits).Cache, (object) apptSplit, "Inserting Lot/Serial Number {0} specified in Appointment Line {1} to the service order.", apptSplit.LotSerialNbr, apptLine.LineRef);
              ++num2;
              fssoDetSplit2 = errorSourceRow;
            }
            apptSplit.FSSODetSplitRow = fssoDetSplit2;
            if (fssoDetSplit1 == null)
              fssoDetSplit1 = fssoDetSplit2;
          }
          Decimal num4 = fssoDet.BaseEstimatedQty.Value > num1 ? fssoDet.BaseEstimatedQty.Value - num1 : 0M;
          Decimal? nullable3;
          while (num4 > 0M)
          {
            FSSODetSplit fssoDetSplit5 = GraphHelper.RowCast<FSSODetSplit>((IEnumerable) ((PXSelectBase<FSSODetSplit>) soGraph.Splits).Select(Array.Empty<object>())).Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (r =>
            {
              if (!string.IsNullOrEmpty(r.LotSerialNbr))
                return false;
              bool? completed = r.Completed;
              bool flag = false;
              return completed.GetValueOrDefault() == flag & completed.HasValue;
            })).FirstOrDefault<FSSODetSplit>();
            if (fssoDetSplit5 != null)
            {
              FSSODetSplit copy = (FSSODetSplit) ((PXSelectBase) soGraph.Splits).Cache.CreateCopy((object) fssoDetSplit5);
              nullable3 = copy.BaseQty;
              Decimal num5 = num4;
              if (nullable3.GetValueOrDefault() >= num5 & nullable3.HasValue)
              {
                FSSODetSplit fssoDetSplit6 = copy;
                nullable3 = fssoDetSplit6.BaseQty;
                Decimal num6 = num4;
                fssoDetSplit6.BaseQty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num6) : new Decimal?();
                num4 = 0M;
              }
              else
              {
                Decimal num7 = num4;
                nullable3 = copy.BaseQty;
                Decimal num8 = nullable3.Value;
                num4 = num7 - num8;
                copy.BaseQty = new Decimal?(0M);
              }
              nullable3 = copy.BaseQty;
              Decimal num9 = 0M;
              if (nullable3.GetValueOrDefault() == num9 & nullable3.HasValue)
              {
                ((PXSelectBase<FSSODetSplit>) soGraph.Splits).Delete(fssoDetSplit5);
              }
              else
              {
                FSSODetSplit fssoDetSplit7 = copy;
                PXCache cache = ((PXSelectBase) soGraph.Splits).Cache;
                int? inventoryId = copy.InventoryID;
                string uom = copy.UOM;
                nullable3 = copy.BaseQty;
                Decimal num10 = nullable3.Value;
                Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num10, INPrecision.QUANTITY));
                fssoDetSplit7.Qty = nullable4;
                ((PXSelectBase<FSSODetSplit>) soGraph.Splits).Update(copy);
              }
            }
            else
              break;
          }
          Decimal num11 = num1;
          nullable3 = fssoDet.BaseEstimatedQty;
          Decimal num12 = nullable3.Value;
          if (num11 != num12)
          {
            Exception exception = (Exception) new PXSetPropertyException("Updating the lot/serial numbers in the service order with the lot/serial numbers in appointment, ended in an attempt to increase the quantity specified on the service order line. If you need to change one lot/serial number for another, please go to the service order and make the change there.", (PXErrorLevel) 4);
            ((PXSelectBase) apptGraph.AppointmentDetails).Cache.RaiseExceptionHandling<FSAppointmentDet.lotSerialNbr>((object) apptLine, (object) null, exception);
            throw new ServiceOrderUpdateException2("Updating the lot/serial numbers in the service order with the lot/serial numbers in appointment, ended in an attempt to increase the quantity specified on the service order line. If you need to change one lot/serial number for another, please go to the service order and make the change there.", Array.Empty<object>());
          }
          apptLine.FSSODetRow = ((PXSelectBase<FSSODet>) soGraph.ServiceOrderDetails).Current;
          return;
        }
      }
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSODet))
      });
    }
  }

  protected virtual void PropagateServiceOrderErrors(
    PXCache errorSourceCache,
    object errorSourceRow,
    PXCache mappingCache,
    object mappingRow,
    string actionMessage,
    params string[] messageParams)
  {
    Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(errorSourceCache, errorSourceRow, new PXErrorLevel[3]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5,
      null
    });
    if (errors == null)
      return;
    string str = PXMessages.LocalizeFormatNoPrefix(actionMessage, (object[]) messageParams);
    List<string> uiFields = SharedFunctions.GetUIFields(mappingCache, mappingRow);
    bool flag = false;
    foreach (KeyValuePair<string, string> keyValuePair in errors)
    {
      KeyValuePair<string, string> entry = keyValuePair;
      Exception exception = (Exception) new PXSetPropertyException("{0}~Error occurred during action: {1}", (PXErrorLevel) 4, new object[2]
      {
        (object) entry.Value,
        (object) str
      });
      if (uiFields.Any<string>((Func<string, bool>) (e => e.Equals(entry.Key, StringComparison.OrdinalIgnoreCase))))
        mappingCache.RaiseExceptionHandling(entry.Key, mappingRow, (object) null, exception);
      else
        flag = true;
    }
    if (errors.Count <= 0)
      return;
    if (!flag)
      throw new ServiceOrderUpdateException(errors, mappingCache.Graph.GetType(), mappingRow, actionMessage, (object[]) messageParams);
    throw new PXOuterException(errors, mappingCache.Graph.GetType(), mappingRow, actionMessage, (object[]) messageParams);
  }

  private void FillDictionaryWithUpdatedFSSODets(PXResultset<FSAppointmentDet> apptLines)
  {
    if (this.ApptLinesWithSrvOrdLineUpdated == null)
      this.ApptLinesWithSrvOrdLineUpdated = new Dictionary<FSAppointmentDet, FSSODet>();
    else
      this.ApptLinesWithSrvOrdLineUpdated.Clear();
    foreach (PXResult<FSAppointmentDet> apptLine in apptLines)
    {
      FSAppointmentDet key = PXResult<FSAppointmentDet>.op_Implicit(apptLine);
      if (key.FSSODetRow != null)
        this.ApptLinesWithSrvOrdLineUpdated[key] = key.FSSODetRow;
    }
  }

  private void FillDictionaryWithUpdatedFSSODetSplits(List<FSApptLineSplit> apptSplits)
  {
    if (this.ApptSplitsWithSrvOrdSplitUpdated == null)
      this.ApptSplitsWithSrvOrdSplitUpdated = new Dictionary<FSApptLineSplit, FSSODetSplit>();
    else
      this.ApptSplitsWithSrvOrdSplitUpdated.Clear();
    foreach (FSApptLineSplit apptSplit in apptSplits)
    {
      if (apptSplit.FSSODetSplitRow != null)
        this.ApptSplitsWithSrvOrdSplitUpdated[apptSplit] = apptSplit.FSSODetSplitRow;
    }
  }

  protected virtual void InsertUpdateSODet(
    PXCache cacheAppointmentDet,
    FSAppointmentDet fsAppointmentDetRow,
    PXSelectBase<FSSODet> viewSODet,
    FSAppointment apptRow)
  {
    PXEntryStatus status = cacheAppointmentDet.GetStatus((object) fsAppointmentDetRow);
    if (status != 2 && status != 1)
      return;
    FSSODet fssoDet1 = (FSSODet) null;
    int? nullable1;
    if (fsAppointmentDetRow.SODetID.HasValue)
    {
      fssoDet1 = FSSODet.UK.Find(((PXSelectBase) viewSODet).Cache.Graph, fsAppointmentDetRow.SODetID);
      if (fssoDet1 != null)
      {
        nullable1 = fssoDet1.SODetID;
        int? soDetId = fsAppointmentDetRow.SODetID;
        if (nullable1.GetValueOrDefault() == soDetId.GetValueOrDefault() & nullable1.HasValue == soDetId.HasValue)
        {
          PXCache<FSSODet>.StoreOriginal(((PXSelectBase) viewSODet).Cache.Graph, fssoDet1);
          goto label_6;
        }
      }
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSODet))
      });
    }
label_6:
    bool flag = false;
    FSSODet fssoDet2;
    if (fssoDet1 == null)
    {
      fssoDet2 = new FSSODet();
      if (fssoDet2.UOM != fsAppointmentDetRow.UOM)
      {
        fssoDet2.UOM = fsAppointmentDetRow.UOM;
        flag = true;
      }
      try
      {
        fssoDet2.SkipUnitPriceCalc = new bool?(true);
        fssoDet2.AlreadyCalculatedUnitPrice = fsAppointmentDetRow.CuryUnitPrice;
        fssoDet2 = AppointmentEntry.InsertDetailLine<FSSODet, FSAppointmentDet>(((PXSelectBase) viewSODet).Cache, (object) fssoDet2, cacheAppointmentDet, (object) fsAppointmentDetRow, new Guid?(), new int?(), true, fsAppointmentDetRow.TranDate, true, false);
        fsAppointmentDetRow.SODetCreate = new bool?(true);
        flag = true;
      }
      finally
      {
        fssoDet2.SkipUnitPriceCalc = new bool?(false);
        fssoDet2.AlreadyCalculatedUnitPrice = new Decimal?();
      }
      fsAppointmentDetRow.FSSODetRow = fssoDet2;
    }
    else
    {
      fssoDet2 = (FSSODet) ((PXSelectBase) viewSODet).Cache.CreateCopy((object) fssoDet1);
      int? nullable2 = fssoDet2.BranchID;
      nullable1 = fsAppointmentDetRow.BranchID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        ((PXSelectBase) viewSODet).Cache.SetValue<FSSODet.branchID>((object) fssoDet2, (object) fsAppointmentDetRow.BranchID);
        flag = true;
      }
      nullable1 = fssoDet2.SiteID;
      nullable2 = fsAppointmentDetRow.SiteID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        fssoDet2.SiteID = fsAppointmentDetRow.SiteID;
        flag = true;
      }
      nullable2 = fssoDet2.SiteLocationID;
      nullable1 = fsAppointmentDetRow.SiteLocationID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        fssoDet2.SiteLocationID = fsAppointmentDetRow.SiteLocationID;
        flag = true;
      }
      bool? nullable3 = fsAppointmentDetRow.SODetCreate;
      if (nullable3.GetValueOrDefault())
      {
        fssoDet2.TranDesc = fsAppointmentDetRow.TranDesc;
        flag = true;
      }
      Decimal? nullable4 = fssoDet2.CuryUnitCost;
      Decimal? curyUnitCost = fsAppointmentDetRow.CuryUnitCost;
      if (!(nullable4.GetValueOrDefault() == curyUnitCost.GetValueOrDefault() & nullable4.HasValue == curyUnitCost.HasValue))
      {
        nullable1 = fssoDet2.ApptCntr;
        int num = 1;
        if (nullable1.GetValueOrDefault() <= num & nullable1.HasValue)
        {
          fssoDet2.CuryUnitCost = fsAppointmentDetRow.CuryUnitCost;
          flag = true;
        }
      }
      bool? nullable5;
      Decimal? nullable6;
      if (this.CanEditSrvOrdLineValues(cacheAppointmentDet, fsAppointmentDetRow, fssoDet2))
      {
        nullable3 = fssoDet2.POCreate;
        bool? enablePo = fsAppointmentDetRow.EnablePO;
        if (!(nullable3.GetValueOrDefault() == enablePo.GetValueOrDefault() & nullable3.HasValue == enablePo.HasValue))
        {
          fssoDet2.POCreate = fsAppointmentDetRow.EnablePO;
          flag = true;
        }
        if (fssoDet2.POSource != fsAppointmentDetRow.POSource)
        {
          fssoDet2.POSource = fsAppointmentDetRow.POSource;
          flag = true;
        }
        nullable1 = fssoDet2.POVendorID;
        nullable2 = fsAppointmentDetRow.POVendorID;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          fssoDet2.POVendorID = fsAppointmentDetRow.POVendorID;
          flag = true;
        }
        nullable2 = fssoDet2.POVendorLocationID;
        nullable1 = fsAppointmentDetRow.POVendorLocationID;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        {
          fssoDet2.POVendorLocationID = fsAppointmentDetRow.POVendorLocationID;
          flag = true;
        }
        nullable5 = fssoDet2.ManualCost;
        nullable3 = fsAppointmentDetRow.ManualCost;
        if (!(nullable5.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable5.HasValue == nullable3.HasValue))
        {
          fssoDet2.ManualCost = fsAppointmentDetRow.ManualCost;
          flag = true;
        }
        nullable6 = fssoDet2.EstimatedQty;
        nullable4 = fsAppointmentDetRow.EstimatedQty;
        if (!(nullable6.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable6.HasValue == nullable4.HasValue))
        {
          fssoDet2.EstimatedQty = fsAppointmentDetRow.EstimatedQty;
          flag = true;
        }
      }
      if (fsAppointmentDetRow.IsExpenseReceiptItem)
      {
        nullable4 = fssoDet2.CuryUnitCost;
        nullable6 = fsAppointmentDetRow.CuryUnitCost;
        if (!(nullable4.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable4.HasValue == nullable6.HasValue))
        {
          fssoDet2.CuryUnitCost = fsAppointmentDetRow.CuryUnitCost;
          flag = true;
        }
        nullable6 = fssoDet2.CuryUnitPrice;
        nullable4 = fsAppointmentDetRow.CuryUnitPrice;
        if (!(nullable6.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable6.HasValue == nullable4.HasValue))
        {
          fssoDet2.CuryUnitPrice = fsAppointmentDetRow.CuryUnitPrice;
          flag = true;
        }
        nullable4 = fssoDet2.EstimatedQty;
        nullable6 = fsAppointmentDetRow.EstimatedQty;
        if (!(nullable4.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable4.HasValue == nullable6.HasValue))
        {
          fssoDet2.EstimatedQty = fsAppointmentDetRow.EstimatedQty;
          flag = true;
        }
        if (fssoDet2.UOM != fsAppointmentDetRow.UOM)
        {
          fssoDet2.UOM = fsAppointmentDetRow.UOM;
          flag = true;
        }
        nullable3 = fssoDet2.IsBillable;
        nullable5 = fsAppointmentDetRow.IsBillable;
        if (!(nullable3.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable3.HasValue == nullable5.HasValue))
        {
          fssoDet2.IsBillable = fsAppointmentDetRow.IsBillable;
          flag = true;
        }
        nullable1 = fssoDet2.CostCodeID;
        nullable2 = fsAppointmentDetRow.CostCodeID;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          fssoDet2.CostCodeID = fsAppointmentDetRow.CostCodeID;
          flag = true;
        }
        nullable2 = fssoDet2.ProjectTaskID;
        nullable1 = fsAppointmentDetRow.ProjectTaskID;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        {
          fssoDet2.ProjectTaskID = fsAppointmentDetRow.ProjectTaskID;
          flag = true;
        }
        nullable6 = fssoDet2.CuryExtCost;
        nullable4 = fsAppointmentDetRow.CuryExtCost;
        if (!(nullable6.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable6.HasValue == nullable4.HasValue))
        {
          fssoDet2.CuryExtCost = fsAppointmentDetRow.CuryExtCost;
          flag = true;
        }
        nullable4 = fssoDet2.CuryBillableExtPrice;
        nullable6 = fsAppointmentDetRow.CuryBillableExtPrice;
        if (!(nullable4.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable4.HasValue == nullable6.HasValue))
        {
          fssoDet2.CuryBillableExtPrice = fsAppointmentDetRow.CuryBillableExtPrice;
          flag = true;
        }
      }
      nullable1 = fssoDet2.ProjectTaskID;
      nullable2 = fsAppointmentDetRow.ProjectTaskID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        fssoDet2.ProjectTaskID = fsAppointmentDetRow.ProjectTaskID;
        flag = true;
      }
    }
    if (flag)
      fssoDet2 = viewSODet.Update(fssoDet2);
    fsAppointmentDetRow.FSSODetRow = fssoDet2;
  }

  public virtual bool CanEditSrvOrdLineValues(
    PXCache cacheAppointmentDet,
    FSAppointmentDet fsAppointmentDetRow,
    FSSODet fsSODetRow)
  {
    bool? nullable;
    if (!fsAppointmentDetRow.EnablePO.GetValueOrDefault())
    {
      nullable = (bool?) cacheAppointmentDet.GetValueOriginal<FSAppointmentDet.enablePO>((object) fsAppointmentDetRow);
      if (!nullable.GetValueOrDefault())
        goto label_5;
    }
    nullable = fsAppointmentDetRow.CanChangeMarkForPO;
    if (nullable.GetValueOrDefault())
    {
      int? apptCntr = fsSODetRow.ApptCntr;
      int num = 1;
      if (apptCntr.GetValueOrDefault() <= num & apptCntr.HasValue)
      {
        nullable = fsSODetRow.IsPrepaid;
        bool flag = false;
        return nullable.GetValueOrDefault() == flag & nullable.HasValue;
      }
    }
label_5:
    return false;
  }

  protected virtual bool IsThereAnySODetReferenceBeingDeleted<SODetIDType>(PXCache cache) where SODetIDType : IBqlField
  {
    IEnumerator enumerator = cache.Deleted.GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        object current = enumerator.Current;
        return true;
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    foreach (object obj in cache.Updated)
    {
      int? nullable = (int?) cache.GetValue<SODetIDType>(obj);
      int? valueOriginal = (int?) cache.GetValueOriginal<SODetIDType>(obj);
      if (!(nullable.GetValueOrDefault() == valueOriginal.GetValueOrDefault() & nullable.HasValue == valueOriginal.HasValue))
        return true;
    }
    foreach (object obj in cache.Inserted)
    {
      int? nullable = (int?) cache.GetValue<SODetIDType>(obj);
      int? valueOriginal = (int?) cache.GetValueOriginal<SODetIDType>(obj);
      if (!(nullable.GetValueOrDefault() == valueOriginal.GetValueOrDefault() & nullable.HasValue == valueOriginal.HasValue))
        return true;
    }
    return false;
  }

  protected virtual bool IsAppointmentBeingDeleted(int? appointmentID, PXCache cache)
  {
    foreach (FSAppointment fsAppointment in cache.Deleted)
    {
      int? appointmentId = fsAppointment.AppointmentID;
      int? nullable = appointmentID;
      if (appointmentId.GetValueOrDefault() == nullable.GetValueOrDefault() & appointmentId.HasValue == nullable.HasValue)
        return true;
    }
    return false;
  }

  protected virtual int ReplicateServiceOrderExceptions(
    AppointmentEntry graphAppointmentEntry,
    ServiceOrderEntry graphServiceOrderEntry,
    Exception exception)
  {
    int num = 0 + SharedFunctions.ReplicateCacheExceptions(((PXSelectBase) graphAppointmentEntry.AppointmentRecords).Cache, (IBqlTable) ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current, ((PXSelectBase) graphAppointmentEntry.ServiceOrderRelated).Cache, (IBqlTable) ((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current, ((PXSelectBase) graphServiceOrderEntry.ServiceOrderRecords).Cache, (IBqlTable) ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).Current);
    foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) graphAppointmentEntry.AppointmentDetails).Select(Array.Empty<object>()))
    {
      FSAppointmentDet row = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      if (row.FSSODetRow != null)
        num += SharedFunctions.ReplicateCacheExceptions(((PXSelectBase) graphAppointmentEntry.AppointmentDetails).Cache, (IBqlTable) row, ((PXSelectBase) graphServiceOrderEntry.ServiceOrderDetails).Cache, (IBqlTable) row.FSSODetRow);
    }
    return num != 0 ? num : throw PXException.PreserveStack(exception);
  }

  protected void RestoreOriginalValues(PXCache cache, PXRowPersistedEventArgs e)
  {
    object obj;
    if (this._oldRows == null || (e.Operation & 3) == 3 || e.TranStatus != 2 || !this._oldRows.TryGetValue(e.Row, out obj) || !(e.Row.GetType() == obj.GetType()))
      return;
    cache.RestoreCopy(e.Row, obj);
  }

  protected void BackupOriginalValues(PXCache cache, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    if (this._oldRows == null)
      this._oldRows = new Dictionary<object, object>();
    object obj;
    if (this._oldRows.TryGetValue(e.Row, out obj) && obj.GetType() == e.Row.GetType())
      cache.RestoreCopy(obj, e.Row);
    else
      this._oldRows[e.Row] = cache.CreateCopy(e.Row);
  }

  public virtual void ValidateRouteDriverDeletionFromRouteDocument(
    FSAppointmentEmployee fsAppointmentEmployeeRow)
  {
    if (!this.IsAppointmentBeingDeleted(fsAppointmentEmployeeRow.AppointmentID, ((PXSelectBase) this.AppointmentRecords).Cache) && fsAppointmentEmployeeRow.IsDriver.GetValueOrDefault() && ((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200") && ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.RouteDocumentID.HasValue)
      throw new PXException("This driver cannot be deleted on this form. To delete the driver, open the Route Document Details (FS304000) form, select the {0} route execution in the Route Nbr. box, and clear the Driver box.", new object[1]
      {
        (object) PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.RouteDocumentID
        })).RefNbr
      });
  }

  public virtual void SetRequireSerialWarning(
    PXCache cache,
    FSAppointmentDet fsAppointmentDetServiceRow)
  {
    if (!fsAppointmentDetServiceRow.SMEquipmentID.HasValue)
      return;
    if (PXResultset<FSEquipmentComponent>.op_Implicit(PXSelectBase<FSEquipmentComponent, PXSelect<FSEquipmentComponent, Where<FSEquipmentComponent.requireSerial, Equal<True>, And<FSEquipmentComponent.serialNumber, IsNull, And<FSEquipmentComponent.SMequipmentID, Equal<Required<FSEquipmentComponent.SMequipmentID>>>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) fsAppointmentDetServiceRow.SMEquipmentID
    })) == null)
      return;
    cache.RaiseExceptionHandling<FSAppointmentDet.SMequipmentID>((object) fsAppointmentDetServiceRow, (object) null, (Exception) new PXSetPropertyException("The serial number is required.", (PXErrorLevel) 2));
  }

  public virtual void UpdateAppointmentDetService_StaffID(
    string serviceLineRef,
    string oldServiceLineRef)
  {
    if (!string.IsNullOrEmpty(serviceLineRef))
    {
      IEnumerable<FSAppointmentEmployee> source1 = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (y => y.ServiceLineRef == serviceLineRef));
      IEnumerable<FSAppointmentDet> source2 = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (y => y.IsService && y.LineRef == serviceLineRef));
      if (source2.Count<FSAppointmentDet>() == 1)
      {
        FSAppointmentDet fsAppointmentDet = source2.ElementAt<FSAppointmentDet>(0);
        int? nullable = new int?(source1.Count<FSAppointmentEmployee>());
        fsAppointmentDet.StaffID = nullable.GetValueOrDefault() == 1 ? source1.ElementAt<FSAppointmentEmployee>(0).EmployeeID : new int?();
        ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(fsAppointmentDet);
      }
    }
    if (string.IsNullOrEmpty(oldServiceLineRef))
      return;
    IEnumerable<FSAppointmentEmployee> source3 = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (y => y.ServiceLineRef == oldServiceLineRef));
    IEnumerable<FSAppointmentDet> source4 = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (y => y.IsService && y.LineRef == oldServiceLineRef));
    if (source4.Count<FSAppointmentDet>() != 1)
      return;
    FSAppointmentDet fsAppointmentDet1 = source4.ElementAt<FSAppointmentDet>(0);
    int? nullable1 = new int?(source3.Count<FSAppointmentEmployee>());
    fsAppointmentDet1.StaffID = nullable1.GetValueOrDefault() == 1 ? source3.ElementAt<FSAppointmentEmployee>(0).EmployeeID : new int?();
    ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(fsAppointmentDet1);
  }

  public virtual bool IsAnySignatureAttached(PXCache cache, FSAppointment fsAppointmentRow)
  {
    if (!((IQueryable<PXResult<NoteDoc>>) PXSelectBase<NoteDoc, PXSelectJoin<NoteDoc, InnerJoin<UploadFile, On<UploadFile.fileID, Equal<NoteDoc.fileID>, And<UploadFile.name, Like<Required<UploadFile.name>>>>>, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) "%signature%",
      (object) fsAppointmentRow.NoteID
    })).Any<PXResult<NoteDoc>>())
      return false;
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(cache, (object) fsAppointmentRow);
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    foreach (Guid guid in fileNotes)
    {
      FileInfo fileWithNoData = instance.GetFileWithNoData(guid);
      if (fileWithNoData != null && fileWithNoData.FullName.Contains("signature"))
        return true;
    }
    return false;
  }

  public virtual void GenerateSignedReport(PXCache cache, FSAppointment fsAppointmentRow)
  {
    if (!this.IsAnySignatureAttached(cache, fsAppointmentRow))
      return;
    string str1 = "FS642000";
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string fieldName1 = SharedFunctions.GetFieldName<FSAppointment.srvOrdType>();
    string fieldName2 = SharedFunctions.GetFieldName<FSAppointment.refNbr>();
    dictionary[fieldName1] = fsAppointmentRow.SrvOrdType;
    dictionary[fieldName2] = fsAppointmentRow.RefNbr;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      using (PX.Reports.Controls.Report report = this.ReportLoader.LoadReport(str1, (IPXResultset) null))
      {
        if (report == null)
          throw new Exception("Unable to call Acumatica report writer for specified report : " + str1);
        this.ReportLoader.InitDefaultReportParameters(report, (IDictionary<string, string>) dictionary);
        using (StreamManager streamManager = new StreamManager())
        {
          this.ReportRenderer.Render("PDF", report, (Hashtable) null, streamManager);
          UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
          string str2 = $"{fsAppointmentRow.RefNbr} - {"Signed"} - {DateTime.Now.ToString("MM_dd_yy_hh_mm_ss")}.pdf";
          FileInfo fileInfo = new FileInfo(Guid.NewGuid(), str2, (string) null, streamManager.MainStream.GetBytes());
          instance.SaveFile(fileInfo, (FileExistsAction) 1);
          PXCache<NoteDoc> pxCache = new PXCache<NoteDoc>((PXGraph) this);
          pxCache.Insert(new NoteDoc()
          {
            NoteID = fsAppointmentRow.NoteID,
            FileID = fileInfo.UID
          });
          ((PXCache) pxCache).Persist((PXDBOperation) 2);
          foreach (Guid fileNote in PXNoteAttribute.GetFileNotes(cache, (object) fsAppointmentRow))
          {
            FileInfo fileWithNoData = instance.GetFileWithNoData(fileNote);
            if (fileWithNoData != null)
            {
              Guid? uid = fileWithNoData.UID;
              Guid? customerSignedReport = fsAppointmentRow.CustomerSignedReport;
              if ((uid.HasValue == customerSignedReport.HasValue ? (uid.HasValue ? (uid.GetValueOrDefault() == customerSignedReport.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 || fileWithNoData.FullName.Contains("signature"))
                UploadFileMaintenance.DeleteFile(fileWithNoData.UID);
            }
          }
          fsAppointmentRow.CustomerSignedReport = fileInfo.UID;
          PXUpdate<Set<FSAppointment.customerSignedReport, Required<FSAppointment.customerSignedReport>>, FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Update((PXGraph) this, new object[2]
          {
            (object) fsAppointmentRow.CustomerSignedReport,
            (object) fsAppointmentRow.AppointmentID
          });
          cache.Graph.SelectTimeStamp();
          fsAppointmentRow.tstamp = cache.Graph.TimeStamp;
        }
      }
      transactionScope.Complete();
    }
  }

  public virtual void HandleServiceLineStatusChange(
    ref List<FSAppointmentLog> deleteReleatedTimeActivity,
    ref List<FSAppointmentLog> createReleatedTimeActivity)
  {
    foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>(((PXSelectBase) this.AppointmentDetails).Cache.Updated).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => x.IsService)))
    {
      object valueOriginal = ((PXSelectBase) this.AppointmentDetails).Cache.GetValueOriginal<FSAppointmentDet.status>((object) fsAppointmentDet);
      string str = valueOriginal != null ? (string) valueOriginal : string.Empty;
      if (!(str == fsAppointmentDet.Status))
      {
        if (fsAppointmentDet.IsCanceledNotPerformed.GetValueOrDefault())
          this.FillRelatedTimeActivityList(fsAppointmentDet.LineRef, ref deleteReleatedTimeActivity);
        else if (str == "CC")
          this.FillRelatedTimeActivityList(fsAppointmentDet.LineRef, ref createReleatedTimeActivity);
      }
    }
  }

  public virtual void FillRelatedTimeActivityList(
    string lineRef,
    ref List<FSAppointmentLog> fsAppointmentDetEmployeeList)
  {
    foreach (FSAppointmentLog fsAppointmentLog in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (y => y.DetLineRef == lineRef)))
      fsAppointmentDetEmployeeList.Add(fsAppointmentLog);
  }

  public virtual void SetCurrentAppointmentSalesPersonID(FSServiceOrder fsServiceOrderRow)
  {
    if (fsServiceOrderRow == null)
      return;
    FSSrvOrdType current1 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current1 == null)
      return;
    int? nullable1 = current1.SalesPersonID;
    if (nullable1.HasValue)
      return;
    CustDefSalesPeople custDefSalesPeople = PXResultset<CustDefSalesPeople>.op_Implicit(PXSelectBase<CustDefSalesPeople, PXSelect<CustDefSalesPeople, Where<CustDefSalesPeople.bAccountID, Equal<Required<CustDefSalesPeople.bAccountID>>, And<CustDefSalesPeople.locationID, Equal<Required<CustDefSalesPeople.locationID>>, And<CustDefSalesPeople.isDefault, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsServiceOrderRow.CustomerID,
      (object) fsServiceOrderRow.LocationID
    }));
    if (custDefSalesPeople != null)
    {
      ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.SalesPersonID = custDefSalesPeople.SalesPersonID;
      ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.Commissionable = new bool?(false);
    }
    else
    {
      FSAppointment current2 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      current2.SalesPersonID = nullable2;
      ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.Commissionable = new bool?(false);
    }
  }

  public virtual bool GetSrvOrdLineBalance(
    int? soDetID,
    int? apptDetID,
    out Decimal srvOrdAllocatedQty,
    out Decimal otherAppointmentsUsedQty)
  {
    if (soDetID.HasValue)
    {
      int? nullable1 = soDetID;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() < num & nullable1.HasValue))
      {
        FSSODet fssoDet = FSSODet.UK.Find((PXGraph) this, soDetID);
        srvOrdAllocatedQty = fssoDet.EstimatedQty.Value;
        FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectGroupBy<FSAppointmentDet, Where<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>, And<FSAppointmentDet.isCanceledNotPerformed, Equal<False>, And<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<FSAppointmentDet.appDetID, NotEqual<Required<FSAppointmentDet.appDetID>>>>>>, Aggregate<GroupBy<FSAppointmentDet.sODetID, Sum<FSAppointmentDet.effTranQty>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) soDetID,
          (object) apptDetID
        }));
        Decimal? nullable2 = fsAppointmentDet != null ? fsAppointmentDet.EffTranQty : new Decimal?(0M);
        otherAppointmentsUsedQty = nullable2.HasValue ? nullable2.Value : 0M;
        return true;
      }
    }
    srvOrdAllocatedQty = 0M;
    otherAppointmentsUsedQty = 0M;
    return false;
  }

  public virtual void VerifySrvOrdLineQty(
    PXCache cache,
    FSAppointmentDet apptLine,
    object newValue,
    System.Type QtyField,
    bool runningFieldVerifying)
  {
    if (newValue == null || !(newValue is Decimal num1) || num1 == 0M || !apptLine.IsInventoryItem || apptLine.IsCanceledNotPerformed.GetValueOrDefault() || !apptLine.SODetID.HasValue)
      return;
    int? soDetId = apptLine.SODetID;
    int num2 = 0;
    if (!(soDetId.GetValueOrDefault() > num2 & soDetId.HasValue))
      return;
    FSSODet fsSODetRow = FSSODet.UK.Find(cache.Graph, apptLine.SODetID);
    if (this.CanEditSrvOrdLineValues(cache, apptLine, fsSODetRow))
      return;
    Decimal srvOrdAllocatedQty;
    Decimal otherAppointmentsUsedQty;
    this.GetSrvOrdLineBalance(apptLine.SODetID, apptLine.AppDetID, out srvOrdAllocatedQty, out otherAppointmentsUsedQty);
    if (!(otherAppointmentsUsedQty + (Decimal) newValue > srvOrdAllocatedQty))
      return;
    PXSetPropertyException propertyException;
    if (otherAppointmentsUsedQty == 0M)
      propertyException = new PXSetPropertyException("The specified quantity ({0}) is greater than the quantity in the associated service order line ({1}).", (PXErrorLevel) 4, new object[2]
      {
        (object) ((Decimal) newValue).ToString("0.00"),
        (object) srvOrdAllocatedQty.ToString("0.00")
      });
    else
      propertyException = new PXSetPropertyException("The specified quantity ({0}) along with the quantity in other appointments ({1}) is greater than the quantity in the associated service order line ({2}).", (PXErrorLevel) 4, new object[3]
      {
        (object) ((Decimal) newValue).ToString("0.00"),
        (object) otherAppointmentsUsedQty.ToString("0.00"),
        (object) srvOrdAllocatedQty.ToString("0.00")
      });
    if (runningFieldVerifying)
      throw propertyException;
    cache.RaiseExceptionHandling(QtyField.Name, (object) apptLine, newValue, (Exception) propertyException);
  }

  protected void VerifyIfQtyDivisible(PXCache cache, FSAppointmentDet apptLine, object newValue)
  {
    if (newValue != null && apptLine.EquipmentAction != null && !(apptLine.EquipmentAction == "NO") && apptLine.IsInventoryItem && Decimal.Remainder((Decimal) newValue, 1M) != 0M)
      throw new PXSetPropertyException("A decimal number cannot be entered as an item quantity if any equipment action is specified in the line. Specify a whole number for the quantity of this item.", (PXErrorLevel) 4);
  }

  public virtual void LoadServiceOrderRelatedAfterStatusChange(FSAppointment fsAppointmentRow)
  {
    this.LoadServiceOrderRelated(fsAppointmentRow);
    this.UpdateServiceOrderUnboundFields(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, fsAppointmentRow, this.DisableServiceOrderUnboundFieldCalc);
  }

  protected virtual void SetUnitCostByLotSerialNbr(
    PXCache cache,
    FSAppointmentDet fsAppointmentDetRow,
    string oldLotSerialNbr)
  {
    if (fsAppointmentDetRow.EnablePO.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(fsAppointmentDetRow.LotSerialNbr))
    {
      UnitCostHelper.UnitCostPair curyUnitCost = UnitCostHelper.CalculateCuryUnitCost<FSAppointmentDet.unitCost, FSAppointmentDet.inventoryID, FSAppointmentDet.uOM>(cache, (object) fsAppointmentDetRow, true, new Decimal?(0M));
      cache.SetValueExt<FSAppointmentDet.unitCost>((object) fsAppointmentDetRow, (object) curyUnitCost.unitCost);
      cache.SetValueExt<FSAppointmentDet.curyUnitCost>((object) fsAppointmentDetRow, (object) curyUnitCost.curyUnitCost);
    }
    else
    {
      if (string.Equals(fsAppointmentDetRow.LotSerialNbr, oldLotSerialNbr, StringComparison.OrdinalIgnoreCase) || ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current?.PostedBy == null)
        return;
      PXResult<FSSODet> pxResult = ((IQueryable<PXResult<FSSODet>>) PXSelectBase<FSSODet, PXSelectJoin<FSSODet, InnerJoin<FSPostDet, On<FSPostDet.postDetID, Equal<FSSODet.postID>>, InnerJoin<INTran, On<INTran.sOOrderType, Equal<FSPostDet.sOOrderType>, And<INTran.sOOrderNbr, Equal<FSPostDet.sOOrderNbr>, And<INTran.sOOrderLineNbr, Equal<FSPostDet.sOLineNbr>>>>>>, Where<FSSODet.lineType, Equal<FSLineType.Inventory_Item>, And<FSSODet.srvOrdType, Equal<Required<FSSODet.srvOrdType>>, And<FSSODet.refNbr, Equal<Required<FSSODet.refNbr>>, And<FSSODet.inventoryID, Equal<Required<FSSODet.inventoryID>>, And<INTran.lotSerialNbr, Equal<Required<INTran.lotSerialNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
      {
        (object) fsAppointmentDetRow?.SrvOrdType,
        (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current?.RefNbr,
        (object) (int?) fsAppointmentDetRow?.InventoryID,
        (object) fsAppointmentDetRow?.LotSerialNbr
      })).FirstOrDefault<PXResult<FSSODet>>();
      if (pxResult == null)
        return;
      INTran inTran = PXResult<FSSODet, FSPostDet, INTran>.op_Implicit((PXResult<FSSODet, FSPostDet, INTran>) pxResult);
      UnitCostHelper.UnitCostPair curyUnitCost = UnitCostHelper.CalculateCuryUnitCost<FSAppointmentDet.unitCost, FSAppointmentDet.inventoryID, FSAppointmentDet.uOM>(cache, (object) fsAppointmentDetRow, false, inTran.UnitCost);
      cache.SetValueExt<FSAppointmentDet.unitCost>((object) fsAppointmentDetRow, (object) curyUnitCost.unitCost);
      cache.SetValueExt<FSAppointmentDet.curyUnitCost>((object) fsAppointmentDetRow, (object) curyUnitCost.curyUnitCost);
    }
  }

  protected virtual void UpdateManualFlag(
    PXCache cache,
    PXFieldUpdatingEventArgs e,
    DateTime? currentDateTime,
    ref bool? manualFlag)
  {
    if (this.SkipManualTimeFlagUpdate)
      return;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    if (!handlingDateTime.HasValue || !currentDateTime.HasValue)
      return;
    DateTime? nullable1 = handlingDateTime;
    DateTime? nullable2 = currentDateTime;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    manualFlag = new bool?(true);
  }

  public virtual string GetLineDisplayHint(
    PXGraph graph,
    string lineRefNbr,
    string lineDescr,
    int? inventoryID)
  {
    return MessageHelper.GetLineDisplayHint(graph, lineRefNbr, lineDescr, inventoryID);
  }

  public virtual void InitServiceOrderRelated(FSAppointment fsAppointmentRow)
  {
    if (!fsAppointmentRow.SOID.HasValue)
    {
      bool isDirty = ((PXSelectBase) this.ServiceOrderRelated).Cache.IsDirty;
      FSServiceOrder instance = (FSServiceOrder) ((PXSelectBase) this.ServiceOrderRelated).Cache.CreateInstance();
      instance.SrvOrdType = fsAppointmentRow.SrvOrdType;
      instance.DocDesc = fsAppointmentRow.DocDesc;
      FSServiceOrder fsServiceOrder = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Insert(instance);
      fsAppointmentRow.SOID = fsServiceOrder.SOID;
      ((PXSelectBase) this.ServiceOrderRelated).Cache.IsDirty = isDirty;
    }
    else
      this.LoadServiceOrderRelated(fsAppointmentRow);
  }

  public virtual void ForceUpdateCacheAndSave(PXCache cache, object row)
  {
    cache.AllowUpdate = true;
    cache.SetStatus(row, (PXEntryStatus) 1);
    ((PXGraph) this).GetSaveAction().Press();
  }

  /// <summary>
  /// Force calculate external taxes.
  /// When changing status is a good practice to calculate again the taxes. This is because line Qty can be modified.
  /// Also, new lines can be inserted on Details or Logs.
  /// </summary>
  public virtual void ForceExternalTaxCalc()
  {
    ((PXSelectBase) this.AppointmentRecords).Cache.SetValueExt<FSAppointment.isTaxValid>((object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, (object) false);
    this.RecalculateExternalTaxes();
  }

  public virtual void SetItemLineUIStatusList(PXCache cache, FSAppointmentDet row)
  {
    List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
    foreach (Tuple<string, string> full in FSAppointmentDet.ListField_Status_AppointmentDet.ListAttribute.FullList)
    {
      if ((!(row.UIStatus != full.Item1) || !(full.Item1 == "RP") && !(full.Item1 == "WP")) && (row.UIStatus == full.Item1 || this.IsNewItemLineStatusValid(row, full.Item1)))
        tupleList.Add(full);
    }
    PXStringListAttribute.SetList<FSAppointmentDet.uiStatus>(cache, (object) row, tupleList.ToArray());
  }

  public virtual int ChangeItemLineStatus(FSAppointmentDet apptDet, string newStatus)
  {
    if (apptDet.Status == newStatus)
      return 0;
    if (!this.IsItemLineStatusChangeValid(apptDet, newStatus))
      throw new PXException("The status of the {0} line cannot be changed. The status of the {1} lines cannot be changed to {2}.", new object[3]
      {
        (object) apptDet.LineRef,
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointmentDet.status>(((PXSelectBase) this.AppointmentDetails).Cache, (object) apptDet, apptDet.Status),
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointmentDet.status>(((PXSelectBase) this.AppointmentDetails).Cache, (object) apptDet, newStatus)
      });
    FSAppointmentDet copy = (FSAppointmentDet) ((PXSelectBase) this.AppointmentDetails).Cache.CreateCopy((object) apptDet);
    object obj = (object) newStatus;
    ((PXSelectBase) this.AppointmentDetails).Cache.RaiseFieldUpdating<FSAppointmentDet.status>((object) copy, ref obj);
    copy.Status = (string) obj;
    ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(copy);
    return 1;
  }

  /// <summary>
  /// This method does not consider the current item line status.
  /// This performs the basic validation of the new status for the given item line.
  /// </summary>
  /// <param name="apptDet"></param>
  /// <param name="newStatus"></param>
  /// <returns></returns>
  public virtual bool IsNewItemLineStatusValid(FSAppointmentDet apptDet, string newStatus)
  {
    if (newStatus == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return false;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current;
    bool flag1 = apptDet.Status == "CC";
    bool flag2 = false;
    if (apptDet.ShouldBeWaitingPO)
    {
      if (newStatus == "WP")
        return true;
      if (newStatus != "CC" && !flag1)
        return false;
    }
    if (apptDet.ShouldBeRequestPO)
    {
      if (newStatus == "RP")
        return true;
      if (newStatus != "CC" && !flag1)
        return false;
    }
    switch (newStatus)
    {
      case "NS":
        if (current != null && (current.NotStarted.GetValueOrDefault() || current.Hold.GetValueOrDefault() || current.InProcess.GetValueOrDefault() || current.Paused.GetValueOrDefault() || current.ReopenActionRunning.GetValueOrDefault() || apptDet.IsTravelItem.GetValueOrDefault() && current.Completed.GetValueOrDefault()))
        {
          flag2 = true;
          break;
        }
        break;
      case "IP":
        if (current != null && (current.Hold.GetValueOrDefault() || current.InProcess.GetValueOrDefault() || current.Paused.GetValueOrDefault() || current.StartActionRunning.GetValueOrDefault() || apptDet.IsTravelItem.GetValueOrDefault() && (current.NotStarted.GetValueOrDefault() || current.Completed.GetValueOrDefault())))
        {
          flag2 = true;
          break;
        }
        break;
      case "CP":
        if (current != null && (current.Hold.GetValueOrDefault() || current.InProcess.GetValueOrDefault() || current.Paused.GetValueOrDefault() || current.Completed.GetValueOrDefault() || current.Closed.GetValueOrDefault() || apptDet.IsTravelItem.GetValueOrDefault() && (current.NotStarted.GetValueOrDefault() || current.Completed.GetValueOrDefault())))
        {
          flag2 = true;
          break;
        }
        break;
      case "NF":
        if (current != null && (current.Hold.GetValueOrDefault() || current.InProcess.GetValueOrDefault() || current.Paused.GetValueOrDefault() || current.Completed.GetValueOrDefault() || current.Closed.GetValueOrDefault()))
        {
          flag2 = true;
          break;
        }
        break;
      case "NP":
        if (current != null && (current.Hold.GetValueOrDefault() || current.InProcess.GetValueOrDefault() || current.Paused.GetValueOrDefault() || current.Completed.GetValueOrDefault() || current.Closed.GetValueOrDefault() || current.Canceled.GetValueOrDefault() || current.CancelActionRunning.GetValueOrDefault()))
        {
          flag2 = true;
          break;
        }
        break;
      case "CC":
        if (current != null && (current.NotStarted.GetValueOrDefault() || current.Hold.GetValueOrDefault() || current.InProcess.GetValueOrDefault() || current.Paused.GetValueOrDefault() || current.Completed.GetValueOrDefault() || current.Closed.GetValueOrDefault() || current.Canceled.GetValueOrDefault()) || apptDet.ShouldBeWaitingPO || apptDet.ShouldBeRequestPO)
        {
          flag2 = true;
          break;
        }
        break;
    }
    return flag2;
  }

  /// <summary>
  /// This method considers the current item line status
  /// and it's used into the actions Start, Complete, Cancel, etc.
  /// The idea with this method is to force the normal workflow.
  /// </summary>
  /// <param name="apptDet"></param>
  /// <param name="newStatus"></param>
  /// <returns></returns>
  public virtual bool IsItemLineStatusChangeValid(FSAppointmentDet apptDet, string newStatus)
  {
    if (!this.IsNewItemLineStatusValid(apptDet, newStatus))
      return false;
    switch (newStatus)
    {
      case "IP":
        return this.CanLogBeStarted(apptDet);
      case "CP":
        return this.CanItemLineBeCompleted(apptDet);
      case "CC":
        return this.CanItemLineBeCanceled(apptDet);
      case "NS":
      case "NF":
      case "NP":
        return true;
      default:
        return false;
    }
  }

  public virtual bool CanItemLineBeCompleted(FSAppointmentDet row)
  {
    return row != null && this.IsNewItemLineStatusValid(row, "CP") && row.Status != null && EnumerableExtensions.IsNotIn<string>(row.Status, "CP", "CC", "NF", "NP");
  }

  public virtual bool CanItemLineBeCanceled(FSAppointmentDet row)
  {
    return row != null && this.IsNewItemLineStatusValid(row, "CC") && row.Status != null && EnumerableExtensions.IsNotIn<string>(row.Status, "CP", "CC", "NF", "IP");
  }

  public virtual PXSetPropertyException ValidateItemLineStatus(
    PXCache cache,
    FSAppointmentDet apptDet,
    FSAppointment appt)
  {
    if (apptDet.IsTravelItem.GetValueOrDefault())
      return (PXSetPropertyException) null;
    if (appt.Completed.GetValueOrDefault())
    {
      bool? reopenActionRunning = appt.ReopenActionRunning;
      bool flag = false;
      if (reopenActionRunning.GetValueOrDefault() == flag & reopenActionRunning.HasValue && (apptDet.Status == "NS" || apptDet.Status == "IP"))
      {
        PXSetPropertyException propertyException = new PXSetPropertyException("Non-travel items can be added to a completed appointment only if they have the Completed status.");
        cache.RaiseExceptionHandling<FSAppointmentDet.status>((object) apptDet, (object) apptDet.Status, (Exception) propertyException);
        return propertyException;
      }
    }
    return (PXSetPropertyException) null;
  }

  public virtual bool CanLogBeStarted(FSAppointmentDet row)
  {
    return row != null && this.IsNewItemLineStatusValid(row, "IP") && row.Status != null && EnumerableExtensions.IsNotIn<string>(row.Status, "CP", "CC", "NF", "NP");
  }

  public virtual bool CanLogBePausedResumed(FSAppointmentDet row)
  {
    return row != null && row.Status == "IP" && row.LineType != null && EnumerableExtensions.IsNotIn<string>(row.LineType, "CM_LN", "IT_LN", "SLPRO");
  }

  public virtual bool CanLogBePaused(FSAppointmentDet row) => this.CanLogBePausedResumed(row);

  public virtual bool CanLogBeResumed(FSAppointmentDet row)
  {
    return this.CanLogBePausedResumed(row) && this.IsNewItemLineStatusValid(row, "IP");
  }

  public virtual PXSetPropertyException ValidateLogStatus(
    PXCache cache,
    FSAppointmentLog log,
    FSAppointment appt)
  {
    if (log.Travel.GetValueOrDefault())
      return (PXSetPropertyException) null;
    if (appt.Completed.GetValueOrDefault())
    {
      bool? reopenActionRunning = appt.ReopenActionRunning;
      bool flag = false;
      if (reopenActionRunning.GetValueOrDefault() == flag & reopenActionRunning.HasValue && log.Status == "P")
      {
        PXSetPropertyException propertyException = new PXSetPropertyException("Non-travel items can be added to a completed appointment only if they have the Completed status.");
        cache.RaiseExceptionHandling<FSAppointmentLog.status>((object) log, (object) log.Status, (Exception) propertyException);
        return propertyException;
      }
    }
    return (PXSetPropertyException) null;
  }

  public virtual void SplitAppoinmentLogLinesByDays()
  {
    foreach (FSAppointmentLog row in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (_ => _.TrackTime.GetValueOrDefault() && (_.Status == "C" || _.Status == "S") && (string) ((PXSelectBase) this.LogRecords).Cache.GetValueOriginal<FSAppointmentLog.status>((object) _) != _.Status && string.IsNullOrEmpty(_.TimeCardCD))))
    {
      int? timeDuration = row.TimeDuration;
      int num = 0;
      if (timeDuration.GetValueOrDefault() < num & timeDuration.HasValue)
        this.SplitNegativeAppoinmentLogLinesByDays(((PXSelectBase) this.LogRecords).Cache, row);
      else
        this.SplitAppointmentLogLineByDays(((PXSelectBase) this.LogRecords).Cache, row);
    }
  }

  public virtual void SplitAppointmentLogLineByDays(PXCache cache, FSAppointmentLog row)
  {
    if (!row.DateTimeBegin.HasValue || !row.DateTimeEnd.HasValue)
      return;
    DateTime date1 = row.DateTimeBegin.Value.Date;
    DateTime? nullable = row.DateTimeEnd;
    DateTime date2 = nullable.Value.Date;
    if (date1 == date2)
      return;
    nullable = row.DateTimeBegin;
    DateTime dateTime1 = nullable.Value;
    DateTime dateTime2 = dateTime1;
    nullable = row.DateTimeEnd;
    DateTime dateTime3 = nullable.Value;
    FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) PXSelectorAttribute.Select<FSAppointmentLog.detLineRef>(cache, (object) row);
    string status1 = fsAppointmentDet?.Status;
    string status2 = row.Status;
    DateTime dateTime4;
    for (; dateTime1 < dateTime3; dateTime1 = dateTime4)
    {
      dateTime4 = dateTime1.AddDays(1.0);
      dateTime4 = new DateTime(dateTime4.Year, dateTime4.Month, dateTime4.Day, 0, 0, 0);
      if (fsAppointmentDet != null)
        fsAppointmentDet.Status = "IP";
      if (dateTime2.Date == dateTime1.Date)
      {
        nullable = row.DateTimeEnd;
        DateTime dateTime5 = dateTime4;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() != dateTime5 ? 1 : 0) : 1) != 0)
        {
          FSAppointmentLog copy = (FSAppointmentLog) cache.CreateCopy((object) row);
          copy.DateTimeEnd = new DateTime?(dateTime4);
          if (copy.Status == "S")
            copy.Status = "C";
          cache.Update((object) copy);
        }
      }
      else
      {
        FSAppointmentLog copy = (FSAppointmentLog) cache.CreateCopy((object) row);
        copy.LineNbr = new int?();
        copy.LineRef = (string) null;
        copy.LogID = new int?();
        copy.NoteID = new Guid?();
        copy.tstamp = (byte[]) null;
        copy.TimeDuration = new int?();
        copy.BillableTimeDuration = new int?();
        copy.CuryBillableTranAmount = new Decimal?();
        copy.BillableTranAmount = new Decimal?();
        copy.BillableQty = new Decimal?();
        copy.CuryExtCost = new Decimal?();
        if (status2 == "S" && dateTime4 < dateTime3)
          copy.Status = "C";
        else
          copy.Status = status2;
        copy.DateTimeBegin = new DateTime?(dateTime1);
        copy.DateTimeEnd = new DateTime?(dateTime3.Date == dateTime1.Date ? dateTime3 : dateTime4);
        ((PXSelectBase) this.LogRecords).Cache.Insert((object) copy);
      }
    }
    if (fsAppointmentDet == null)
      return;
    fsAppointmentDet.Status = status1;
  }

  public virtual void SplitNegativeAppoinmentLogLinesByDays(PXCache cache, FSAppointmentLog row)
  {
    if (!row.TimeDuration.HasValue || !row.DateTimeBegin.HasValue)
      return;
    DateTime dateTime = row.DateTimeBegin.Value;
    int num1 = dateTime.Hour * 60 + dateTime.Minute - 1440;
    int? nullable1 = row.TimeDuration;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.TimeDuration;
    int num2 = num1;
    if (!(nullable1.GetValueOrDefault() < num2 & nullable1.HasValue))
      return;
    nullable1 = row.TimeDuration;
    int num3 = nullable1.Value + -num1;
    FSAppointmentLog copy1 = (FSAppointmentLog) cache.CreateCopy((object) row);
    copy1.TimeDuration = new int?(num1);
    cache.Update((object) copy1);
    int num4 = 1;
    while (num3 < 0)
    {
      FSAppointmentLog copy2 = (FSAppointmentLog) cache.CreateCopy((object) row);
      FSAppointmentLog fsAppointmentLog1 = copy2;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      fsAppointmentLog1.LineNbr = nullable2;
      copy2.LineRef = (string) null;
      FSAppointmentLog fsAppointmentLog2 = copy2;
      nullable1 = new int?();
      int? nullable3 = nullable1;
      fsAppointmentLog2.LogID = nullable3;
      copy2.NoteID = new Guid?();
      copy2.tstamp = (byte[]) null;
      FSAppointmentLog fsAppointmentLog3 = copy2;
      nullable1 = new int?();
      int? nullable4 = nullable1;
      fsAppointmentLog3.TimeDuration = nullable4;
      FSAppointmentLog fsAppointmentLog4 = (FSAppointmentLog) ((PXSelectBase) this.LogRecords).Cache.Insert((object) copy2);
      fsAppointmentLog4.DateTimeBegin = new DateTime?(dateTime.Date.AddDays((double) num4));
      fsAppointmentLog4.TimeDuration = new int?(num3 < -1440 ? -1440 : num3);
      ((PXSelectBase) this.LogRecords).Cache.Update((object) fsAppointmentLog4);
      num3 += 1440;
      ++num4;
    }
  }

  public virtual void ClearAppointmentLog()
  {
    foreach (FSAppointmentLog fsAppointmentLog in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (_ => _.ItemType != "TR")))
      ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Delete(fsAppointmentLog);
  }

  public virtual void SetLogInfoFromDetails(PXCache cache, FSAppointmentLog fsLogRow)
  {
    FSAppointmentDet apptDet = (FSAppointmentDet) null;
    if (!string.IsNullOrWhiteSpace(fsLogRow.DetLineRef))
      apptDet = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (_ => _.LineRef == fsLogRow.DetLineRef)).FirstOrDefault<FSAppointmentDet>();
    int? nullable1 = fsLogRow.BAccountID;
    if (!nullable1.HasValue)
    {
      fsLogRow.EarningType = (string) null;
      FSAppointmentLog fsAppointmentLog = fsLogRow;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      fsAppointmentLog.LaborItemID = nullable2;
      if (apptDet != null && (apptDet.LineType == "SERVI" || apptDet.LineType == "NSTKI"))
      {
        fsLogRow.ProjectID = apptDet.ProjectID;
        fsLogRow.ProjectTaskID = apptDet.ProjectTaskID;
        fsLogRow.CostCodeID = apptDet.CostCodeID;
      }
    }
    if (apptDet != null)
      fsLogRow.Descr = apptDet.TranDesc;
    string str = apptDet != null || !fsLogRow.Travel.GetValueOrDefault() ? this.GetLogTypeCheckingTravelWithLogFormula(cache, apptDet) : "TR";
    cache.SetValueExt<FSAppointmentLog.itemType>((object) fsLogRow, (object) str);
  }

  public virtual void SetLogActionDefaultSelection(
    FSLogActionFilter current,
    string type,
    bool fromStaffTab)
  {
    IEnumerable<FSAppointmentLogExtItemLine> appointmentLogExtItemLines = (IEnumerable<FSAppointmentLogExtItemLine>) null;
    if (type == "TR" || type == "SE")
      appointmentLogExtItemLines = GraphHelper.RowCast<FSAppointmentLogExtItemLine>((IEnumerable) ((PXSelectBase<FSAppointmentLogExtItemLine>) this.LogActionLogRecords).Select(Array.Empty<object>()));
    switch (type)
    {
      case "TR":
        if (!fromStaffTab)
        {
          FSAppointmentDet current1 = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
          int num = current1 != null ? (current1.IsTravelItem.GetValueOrDefault() ? 1 : 0) : 0;
          current.DetLineRef = num == 0 || !this.CanLogBeStarted(((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current) ? this.GetItemLineRef((PXGraph) this, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.AppointmentID, true) : ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current.LineRef;
          this.UpdateLogActionFilter(current);
          using (IEnumerator<FSAppointmentLogExtItemLine> enumerator = appointmentLogExtItemLines.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              FSAppointmentLogExtItemLine current2 = enumerator.Current;
              current2.Selected = new bool?(current2.DetLineRef == current.DetLineRef || current.DetLineRef == null);
              ((PXSelectBase<FSAppointmentLogExtItemLine>) this.LogActionLogRecords).Update(current2);
            }
            break;
          }
        }
        if (current.Action == "ST")
        {
          using (IEnumerator<PXResult<FSAppointmentStaffDistinct>> enumerator = ((PXSelectBase<FSAppointmentStaffDistinct>) this.LogActionStaffDistinctRecords).Select(Array.Empty<object>()).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              FSAppointmentStaffDistinct appointmentStaffDistinct = PXResult<FSAppointmentStaffDistinct>.op_Implicit(enumerator.Current);
              appointmentStaffDistinct.Selected = new bool?(false);
              if (((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current != null)
              {
                int? baccountId = appointmentStaffDistinct.BAccountID;
                int? employeeId = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.EmployeeID;
                if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue)
                  appointmentStaffDistinct.Selected = new bool?(true);
              }
              ((PXSelectBase<FSAppointmentStaffDistinct>) this.LogActionStaffDistinctRecords).Update(appointmentStaffDistinct);
            }
            break;
          }
        }
        using (IEnumerator<FSAppointmentLogExtItemLine> enumerator = appointmentLogExtItemLines.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            FSAppointmentLogExtItemLine current3 = enumerator.Current;
            current3.Selected = new bool?(false);
            if (((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current != null)
            {
              int? baccountId = current3.BAccountID;
              int? employeeId = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.EmployeeID;
              if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue && (string.IsNullOrEmpty(((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.ServiceLineRef) || current3.DetLineRef == ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.ServiceLineRef))
                current3.Selected = new bool?(true);
            }
            ((PXSelectBase<FSAppointmentLogExtItemLine>) this.LogActionLogRecords).Update(current3);
          }
          break;
        }
      case "SE":
        current.DetLineRef = !(((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current?.LineType == "SERVI") || fromStaffTab ? (string) null : ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current.LineRef;
        this.UpdateLogActionFilter(current);
        int? nullable1 = (int?) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current?.EmployeeID;
        if (((PXGraph) this).IsMobile && ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current == null)
          nullable1 = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).BAccountID;
        using (IEnumerator<FSAppointmentLogExtItemLine> enumerator = appointmentLogExtItemLines.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            FSAppointmentLogExtItemLine current4 = enumerator.Current;
            int? nullable2;
            if (!fromStaffTab)
              current4.Selected = new bool?(current4.DetLineRef == current.DetLineRef);
            else if (((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current != null)
            {
              int? baccountId = current4.BAccountID;
              nullable2 = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.EmployeeID;
              if (baccountId.GetValueOrDefault() == nullable2.GetValueOrDefault() & baccountId.HasValue == nullable2.HasValue && (string.IsNullOrEmpty(((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.ServiceLineRef) || current4.DetLineRef == ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.ServiceLineRef))
                current4.Selected = new bool?(true);
            }
            else
            {
              FSAppointmentLogExtItemLine appointmentLogExtItemLine = current4;
              nullable2 = current4.BAccountID;
              int? nullable3 = nullable1;
              bool? nullable4 = new bool?(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue);
              appointmentLogExtItemLine.Selected = nullable4;
            }
            ((PXSelectBase<FSAppointmentLogExtItemLine>) this.LogActionLogRecords).Update(current4);
          }
          break;
        }
      case "SA":
        using (IEnumerator<FSAppointmentStaffExtItemLine> enumerator = GraphHelper.RowCast<FSAppointmentStaffExtItemLine>((IEnumerable) ((PXSelectBase<FSAppointmentStaffExtItemLine>) this.LogActionStaffRecords).Select(Array.Empty<object>())).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            FSAppointmentStaffExtItemLine current5 = enumerator.Current;
            current5.Selected = new bool?(false);
            if (fromStaffTab)
            {
              if (((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current != null && current5.LineRef == ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.LineRef)
              {
                int? baccountId = current5.BAccountID;
                int? employeeId = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.EmployeeID;
                if (baccountId.GetValueOrDefault() == employeeId.GetValueOrDefault() & baccountId.HasValue == employeeId.HasValue)
                  current5.Selected = new bool?(true);
              }
            }
            else if (((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current != null && current5.DetLineRef == ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current.LineRef)
              current5.Selected = new bool?(true);
            ((PXSelectBase<FSAppointmentStaffExtItemLine>) this.LogActionStaffRecords).Update(current5);
          }
          break;
        }
      case "SB":
        using (IEnumerator<FSDetailFSLogAction> enumerator = GraphHelper.RowCast<FSDetailFSLogAction>((IEnumerable) ((PXSelectBase<FSDetailFSLogAction>) this.ServicesLogAction).Select(Array.Empty<object>())).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            FSDetailFSLogAction current6 = enumerator.Current;
            current6.Selected = new bool?(false);
            if (fromStaffTab)
            {
              if (((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current != null && current6.LineRef == ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Current.ServiceLineRef)
                current6.Selected = new bool?(true);
            }
            else if (((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current != null && current6.LineRef == ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current.LineRef)
              current6.Selected = new bool?(true);
            ((PXSelectBase<FSDetailFSLogAction>) this.ServicesLogAction).Update(current6);
          }
          break;
        }
    }
  }

  public virtual void SetLogActionPanelDefaults(
    PXView view,
    FSLogActionFilter current,
    string dfltAction,
    string dfltLogType,
    bool fromStaffTab = false)
  {
    this.ClearLogActionsViewCaches();
    ((PXSelectBase) this.LogActionFilter).Cache.Clear();
    ((PXSelectBase) this.LogActionFilter).Cache.ClearQueryCache();
    view.Cache.Clear();
    view.Cache.ClearQueryCache();
    current.Action = dfltAction;
    current.Type = dfltLogType;
    current.VerifyRequired = new bool?(true);
    current.LogDateTime = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
    this.UpdateLogActionFilter(current);
    this.SetLogActionDefaultSelection(current, dfltLogType, fromStaffTab);
  }

  public virtual void RunLogActionBase(
    string action,
    string logType,
    FSAppointmentDet apptDet,
    PXSelectBase<FSAppointmentLog> logSelect,
    params object[] logSelectArgs)
  {
    if (this.IsExternalTax(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.TaxZoneID))
      this.SkipLongOperation = true;
    switch (action)
    {
      case "ST":
        bool flag = false;
        switch (logType)
        {
          case "TR":
            this.StartTravelAction();
            flag = ((PXSelectBase) this.LogRecords).Cache.IsDirty || ((PXSelectBase) this.AppointmentServiceEmployees).Cache.IsDirty;
            break;
          case "SE":
            this.StartServiceAction();
            flag = ((PXSelectBase) this.LogRecords).Cache.IsDirty;
            break;
          case "NS":
            this.StartNonStockAction(apptDet);
            flag = true;
            break;
          case "SA":
            this.StartStaffAction();
            flag = ((PXSelectBase) this.LogRecords).Cache.IsDirty || ((PXSelectBase) this.AppointmentServiceEmployees).Cache.IsDirty;
            break;
          case "SB":
            this.StartServiceBasedOnAssignmentAction();
            flag = ((PXSelectBase) this.LogRecords).Cache.IsDirty || ((PXSelectBase) this.AppointmentServiceEmployees).Cache.IsDirty;
            break;
        }
        if (flag)
        {
          if (this.SkipLongOperation)
          {
            this.SaveWithRecalculateExternalTaxesSync();
            break;
          }
          ((PXGraph) this).Actions.PressSave();
          break;
        }
        break;
      case "CP":
      case "PA":
      case "RE":
        List<FSAppointmentLog> logList;
        if (logSelect != null)
        {
          logList = new List<FSAppointmentLog>();
          foreach (PXResult<FSAppointmentLog> pxResult in logSelect.Select(logSelectArgs))
          {
            FSAppointmentLog fsAppointmentLog = PXResult<FSAppointmentLog>.op_Implicit(pxResult);
            logList.Add(fsAppointmentLog);
          }
        }
        else
          logList = ((IEnumerable<FSAppointmentLog>) GraphHelper.RowCast<FSAppointmentLogExtItemLine>((IEnumerable) ((PXSelectBase<FSAppointmentLogExtItemLine>) this.LogActionLogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLogExtItemLine>((Func<FSAppointmentLogExtItemLine, bool>) (_ => _.Selected.GetValueOrDefault()))).ToList<FSAppointmentLog>();
        if (action == "RE")
        {
          foreach (FSAppointmentLog fsAppointmentLog1 in logList)
          {
            FSAppointmentLog fsAppointmentLog2 = new FSAppointmentLog();
            fsAppointmentLog2.ItemType = fsAppointmentLog1.ItemType;
            fsAppointmentLog2.Status = "P";
            fsAppointmentLog2.BAccountID = fsAppointmentLog1.BAccountID;
            fsAppointmentLog2.DetLineRef = fsAppointmentLog1.DetLineRef;
            fsAppointmentLog2.DateTimeBegin = (DateTime?) ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current?.LogDateTime;
            FSAppointmentLog fsAppointmentLog3 = (FSAppointmentLog) ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog2);
          }
        }
        this.CompletePauseAction(action, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime, apptDet, logList);
        if (logType == "TR" && action == "CP" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.OnTravelCompleteStartAppt.GetValueOrDefault())
        {
          FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
          if ((current != null ? (current.NotStarted.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            object obj = ((PXSelectBase) this.AppointmentRecords).Cache.Locate((object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
            if (obj == null)
              ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = (FSAppointment) PrimaryKeyOf<FSAppointment>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<FSAppointment.srvOrdType, FSAppointment.refNbr>>.Find((PXGraph) this, (TypeArrayOf<IBqlField>.IFilledWith<FSAppointment.srvOrdType, FSAppointment.refNbr>) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, (PKFindOptions) 0);
            else if (obj != ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current)
              ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current = (FSAppointment) obj;
            this.startAppointment.PressImpl(true, false);
            break;
          }
          break;
        }
        break;
    }
    this.SkipLongOperation = false;
    PXCache cache = ((PXSelectBase) this.AppointmentSelected).Cache;
    if (!NonGenericIEnumerableExtensions.Any_(cache.Updated) || !cache.IsDirty)
      return;
    ((PXGraph) this).Actions.PressSave();
  }

  public virtual void RunLogAction(
    string action,
    string type,
    FSAppointmentDet apptDet,
    PXSelectBase<FSAppointmentLog> logSCanChangePOOptionselect,
    params object[] logSelectArgs)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AppointmentEntry.\u003C\u003Ec__DisplayClass394_0 displayClass3940 = new AppointmentEntry.\u003C\u003Ec__DisplayClass394_0();
    // ISSUE: reference to a compiler-generated field
    displayClass3940.action = action;
    // ISSUE: reference to a compiler-generated field
    displayClass3940.type = type;
    // ISSUE: reference to a compiler-generated field
    displayClass3940.apptDet = apptDet;
    // ISSUE: reference to a compiler-generated field
    displayClass3940.logSelectArgs = logSelectArgs;
    // ISSUE: reference to a compiler-generated field
    if (displayClass3940.type != "NS")
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.VerifyBeforeAction(((PXSelectBase) this.LogActionFilter).Cache, ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current, displayClass3940.action, displayClass3940.type, true);
    }
    if (!this.SkipLongOperation)
    {
      // ISSUE: reference to a compiler-generated field
      displayClass3940.graphAppointmentEntry = GraphHelper.Clone<AppointmentEntry>(this);
      // ISSUE: reference to a compiler-generated field
      displayClass3940.selectCopy = (PXSelect<FSAppointmentLog>) null;
      if (logSCanChangePOOptionselect != null)
      {
        // ISSUE: reference to a compiler-generated field
        PXView pxView = new PXView((PXGraph) displayClass3940.graphAppointmentEntry, true, ((PXSelectBase) logSCanChangePOOptionselect).View.BqlSelect);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        displayClass3940.selectCopy = new PXSelect<FSAppointmentLog>((PXGraph) displayClass3940.graphAppointmentEntry);
        // ISSUE: reference to a compiler-generated field
        ((PXSelectBase) displayClass3940.selectCopy).View = pxView;
      }
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass3940, __methodptr(\u003CRunLogAction\u003Eb__0)));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      this.RunLogActionBase(displayClass3940.action, displayClass3940.type, displayClass3940.apptDet, logSCanChangePOOptionselect, displayClass3940.logSelectArgs);
    }
  }

  public virtual void SetVisibleCompletePauseLogActionGrid(FSLogActionFilter filter)
  {
    ((PXSelectBase) this.LogActionLogRecords).Cache.AllowSelect = filter.Action == "CP" || filter.Action == "PA" || filter.Action == "RE";
  }

  public virtual void ClearLogActionsViewCaches()
  {
    ((PXSelectBase) this.LogActionLogRecords).Cache.Clear();
    ((PXSelectBase) this.LogActionLogRecords).Cache.ClearQueryCache();
    ((PXSelectBase) this.LogActionStaffRecords).Cache.Clear();
    ((PXSelectBase) this.LogActionStaffRecords).Cache.ClearQueryCache();
    ((PXSelectBase) this.LogActionStaffDistinctRecords).Cache.Clear();
    ((PXSelectBase) this.LogActionStaffDistinctRecords).Cache.ClearQueryCache();
  }

  public virtual void VerifyOnCompleteApptRequireLog(PXCache cache)
  {
    if (GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (_ =>
    {
      if (_.LineType == "SERVI")
      {
        bool? nullable = _.IsTravelItem;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          nullable = _.IsCanceledNotPerformed;
          bool flag2 = false;
          return nullable.GetValueOrDefault() == flag2 & nullable.HasValue;
        }
      }
      return false;
    })).GroupJoin(GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())), (Func<FSAppointmentDet, string>) (d => d.LineRef), (Func<FSAppointmentLog, string>) (l => l.DetLineRef), (d, l) => new
    {
      ApptDet = d,
      LogCount = l.Count<FSAppointmentLog>()
    }).Any(g => g.LogCount == 0))
      throw new PXException("At least one service does not have a log. Make sure that all the necessary logs have been added on the Logs tab. If logs are not required for all services, clear the Require Service Logs on Appointment Completion check box on the Service Order Types (FS202300) form for the service order type.");
  }

  public virtual void VerifyLogActionWithAppointmentStatus(
    string logActionID,
    string logActionLabel,
    string logType,
    FSAppointment appointment)
  {
    if ((appointment.NotStarted.GetValueOrDefault() || appointment.Completed.GetValueOrDefault()) && logType != "TR")
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The {0} action is not valid for non-travel items when the appointment status is one of the following: {1} or {2}.", new object[3]
      {
        (object) logActionLabel,
        (object) "Not Started",
        (object) "Completed"
      }));
  }

  public virtual string GetItemLineStatusFromLog(FSAppointmentDet appointmentDet)
  {
    if (appointmentDet?.Status == "NF")
      return "NF";
    string newStatus = "NS";
    if (appointmentDet != null && (appointmentDet.LineType == "SERVI" || appointmentDet.LineType == "NSTKI"))
    {
      IEnumerable<FSAppointmentLog> source = GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (_ => _.DetLineRef == appointmentDet.LineRef));
      if (!source.Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (_ => _.Status == "P" || _.Status == "S")).Any<FSAppointmentLog>())
      {
        if (source.Count<FSAppointmentLog>() > 0)
        {
          if (appointmentDet.Status == "CP" || appointmentDet.Status == "NF")
            return appointmentDet.Status;
          newStatus = "CP";
        }
      }
      else
        newStatus = "IP";
    }
    if (appointmentDet != null && !this.IsNewItemLineStatusValid(appointmentDet, newStatus))
      newStatus = "NS";
    return newStatus;
  }

  public virtual string GetLogType(FSAppointmentDet apptDet)
  {
    if (apptDet == null)
      return "SA";
    if (apptDet.IsTravelItem.GetValueOrDefault())
      return "TR";
    return !(apptDet.LineType == "NSTKI") ? "SE" : "NS";
  }

  public virtual string GetLogTypeCheckingTravelWithLogFormula(
    PXCache logCache,
    FSAppointmentDet apptDet)
  {
    object obj = (object) null;
    logCache.RaiseFieldDefaulting<FSAppointmentLog.itemType>((object) null, ref obj);
    if (obj != null)
    {
      string travelWithLogFormula = (string) obj;
      if (travelWithLogFormula == "TR")
        return travelWithLogFormula;
    }
    return this.GetLogType(apptDet);
  }

  public virtual void PrimaryDriver_FieldUpdated_Handler(
    PXCache cache,
    FSAppointmentEmployee fsAppointmentEmployeeRow)
  {
    PXResultset<FSAppointmentEmployee> pxResultset = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>());
    foreach (FSAppointmentEmployee appointmentEmployee in GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) pxResultset).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ =>
    {
      int? employeeId1 = _.EmployeeID;
      int? employeeId2 = fsAppointmentEmployeeRow.EmployeeID;
      return employeeId1.GetValueOrDefault() == employeeId2.GetValueOrDefault() & employeeId1.HasValue == employeeId2.HasValue;
    })))
    {
      appointmentEmployee.PrimaryDriver = fsAppointmentEmployeeRow.PrimaryDriver;
      if (cache.GetStatus((object) appointmentEmployee) == null)
        cache.SetStatus((object) appointmentEmployee, (PXEntryStatus) 1);
    }
    if (fsAppointmentEmployeeRow.PrimaryDriver.GetValueOrDefault())
    {
      foreach (FSAppointmentEmployee appointmentEmployee in GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) pxResultset).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ =>
      {
        int? employeeId3 = _.EmployeeID;
        int? employeeId4 = fsAppointmentEmployeeRow.EmployeeID;
        return !(employeeId3.GetValueOrDefault() == employeeId4.GetValueOrDefault() & employeeId3.HasValue == employeeId4.HasValue) && _.PrimaryDriver.GetValueOrDefault();
      })))
      {
        appointmentEmployee.PrimaryDriver = new bool?(false);
        if (cache.GetStatus((object) appointmentEmployee) == null)
          cache.SetStatus((object) appointmentEmployee, (PXEntryStatus) 1);
      }
    }
    ((PXSelectBase) this.AppointmentServiceEmployees).View.RequestRefresh();
  }

  public virtual void PrimaryDriver_RowDeleting_Handler(
    PXCache cache,
    FSAppointmentEmployee fsAppointmentEmployeeRow)
  {
    if (!fsAppointmentEmployeeRow.PrimaryDriver.GetValueOrDefault())
      return;
    PXResultset<FSAppointmentEmployee> pxResultset = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>());
    if (GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) pxResultset).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ =>
    {
      int? employeeId1 = _.EmployeeID;
      int? employeeId2 = fsAppointmentEmployeeRow.EmployeeID;
      return employeeId1.GetValueOrDefault() == employeeId2.GetValueOrDefault() & employeeId1.HasValue == employeeId2.HasValue;
    })).Any<FSAppointmentEmployee>())
      return;
    IEnumerable<FSAppointmentEmployee> source = (IEnumerable<FSAppointmentEmployee>) GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) pxResultset).OrderBy<FSAppointmentEmployee, int?>((Func<FSAppointmentEmployee, int?>) (_ => _.LineNbr));
    if (source.Any<FSAppointmentEmployee>())
      cache.SetValueExt<FSAppointmentEmployee.primaryDriver>((object) source.First<FSAppointmentEmployee>(), (object) true);
    ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.PrimaryDriver = (int?) source.FirstOrDefault<FSAppointmentEmployee>()?.EmployeeID;
  }

  public virtual void ValidatePrimaryDriver()
  {
    PXResultset<FSAppointmentEmployee> pxResultset = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>());
    if (pxResultset.Count > 0 && !GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) pxResultset).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ => _.PrimaryDriver.GetValueOrDefault())).Any<FSAppointmentEmployee>())
      throw new PXException("Select the primary driver on the Staff tab.");
  }

  public virtual void VerifyBeforeAction(
    PXCache cache,
    FSLogActionFilter current,
    string action,
    string type,
    bool throwException = false)
  {
    cache.RaiseExceptionHandling<FSLogActionFilter.type>((object) current, (object) current.Type, (Exception) null);
    cache.RaiseExceptionHandling<FSLogActionFilter.detLineRef>((object) current, (object) current.DetLineRef, (Exception) null);
    bool? verifyRequired = current.VerifyRequired;
    bool flag1 = false;
    if (verifyRequired.GetValueOrDefault() == flag1 & verifyRequired.HasValue)
      return;
    bool flag2 = false;
    switch (action)
    {
      case "ST":
        switch (type)
        {
          case "TR":
          case "SE":
            int num1 = GraphHelper.RowCast<FSAppointmentStaffDistinct>((IEnumerable) ((PXSelectBase<FSAppointmentStaffDistinct>) this.LogActionStaffDistinctRecords).Select(Array.Empty<object>())).Where<FSAppointmentStaffDistinct>((Func<FSAppointmentStaffDistinct, bool>) (x => x.Selected.GetValueOrDefault())).Count<FSAppointmentStaffDistinct>();
            flag2 = current.Me.GetValueOrDefault() || num1 > 0;
            if (type == "SE" && current.DetLineRef == null)
            {
              cache.RaiseExceptionHandling<FSLogActionFilter.detLineRef>((object) current, (object) current.DetLineRef, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
              {
                (object) PXUIFieldAttribute.GetDisplayName<FSLogActionFilter.detLineRef>(cache)
              })));
              flag2 = false;
              break;
            }
            break;
          case "SA":
            int num2 = GraphHelper.RowCast<FSAppointmentStaffExtItemLine>((IEnumerable) ((PXSelectBase<FSAppointmentStaffExtItemLine>) this.LogActionStaffRecords).Select(Array.Empty<object>())).Where<FSAppointmentStaffExtItemLine>((Func<FSAppointmentStaffExtItemLine, bool>) (x => x.Selected.GetValueOrDefault())).Count<FSAppointmentStaffExtItemLine>();
            bool isMobile = ((PXGraph) this).IsMobile;
            flag2 = num2 > 0 || isMobile && current.Me.GetValueOrDefault();
            break;
          case "SB":
            flag2 = GraphHelper.RowCast<FSDetailFSLogAction>((IEnumerable) ((PXSelectBase<FSDetailFSLogAction>) this.ServicesLogAction).Select(Array.Empty<object>())).Where<FSDetailFSLogAction>((Func<FSDetailFSLogAction, bool>) (x => x.Selected.GetValueOrDefault())).Count<FSDetailFSLogAction>() > 0;
            break;
        }
        break;
      case "CP":
      case "PA":
      case "RE":
        flag2 = GraphHelper.RowCast<FSAppointmentLogExtItemLine>((IEnumerable) ((PXSelectBase<FSAppointmentLogExtItemLine>) this.LogActionLogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLogExtItemLine>((Func<FSAppointmentLogExtItemLine, bool>) (x => x.Selected.GetValueOrDefault())).Count<FSAppointmentLogExtItemLine>() > 0;
        break;
    }
    if (flag2)
      return;
    cache.RaiseExceptionHandling<FSLogActionFilter.type>((object) current, (object) current.Type, (Exception) new PXSetPropertyException("Select at least one record in the table to perform this action."));
    if (throwException)
      throw new PXRowPersistingException((string) null, (object) null, "Select at least one record in the table to perform this action.");
  }

  public virtual void StartTravelAction(
    IEnumerable<FSAppointmentStaffDistinct> createLogItems = null)
  {
    IEnumerable<FSAppointmentStaffDistinct> appointmentStaffDistincts = (IEnumerable<FSAppointmentStaffDistinct>) null;
    DateTime? nullable1 = new DateTime?();
    string str;
    DateTime? nullable2;
    if (createLogItems == null)
    {
      str = ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current?.DetLineRef;
      nullable2 = (DateTime?) ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current?.LogDateTime;
      if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Me.GetValueOrDefault())
      {
        PX.Objects.EP.EPEmployee employeeByUserID = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        if (employeeByUserID != null)
        {
          int num = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (x =>
          {
            int? employeeId = x.EmployeeID;
            int? baccountId = employeeByUserID.BAccountID;
            return employeeId.GetValueOrDefault() == baccountId.GetValueOrDefault() & employeeId.HasValue == baccountId.HasValue;
          })).Count<FSAppointmentEmployee>() > 0 ? 1 : 0;
          bool flag = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (x => x.PrimaryDriver.GetValueOrDefault())).Count<FSAppointmentEmployee>() > 0;
          if (num == 0)
          {
            FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee()
            {
              EmployeeID = employeeByUserID.BAccountID
            };
            if (!flag)
              appointmentEmployee.PrimaryDriver = new bool?(true);
            ((PXSelectBase) this.AppointmentServiceEmployees).Cache.Insert((object) appointmentEmployee);
          }
          FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
          fsAppointmentLog.BAccountID = employeeByUserID.BAccountID;
          fsAppointmentLog.ItemType = "TR";
          fsAppointmentLog.DetLineRef = str;
          fsAppointmentLog.DateTimeBegin = nullable2;
          ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
        }
      }
      else
        appointmentStaffDistincts = GraphHelper.RowCast<FSAppointmentStaffDistinct>((IEnumerable) ((PXSelectBase<FSAppointmentStaffDistinct>) this.LogActionStaffDistinctRecords).Select(Array.Empty<object>())).Where<FSAppointmentStaffDistinct>((Func<FSAppointmentStaffDistinct, bool>) (x => x.Selected.GetValueOrDefault()));
    }
    else
    {
      str = (string) null;
      nullable2 = PXDBDateAndTimeAttribute.CombineDateTime(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate, new DateTime?(PXTimeZoneInfo.Now));
      appointmentStaffDistincts = createLogItems;
    }
    if (appointmentStaffDistincts == null)
      return;
    foreach (FSAppointmentStaffDistinct appointmentStaffDistinct in appointmentStaffDistincts)
    {
      FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
      fsAppointmentLog.BAccountID = appointmentStaffDistinct.BAccountID;
      fsAppointmentLog.ItemType = "TR";
      fsAppointmentLog.DetLineRef = str;
      fsAppointmentLog.DateTimeBegin = nullable2;
      ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
    }
  }

  public virtual void StartServiceAction(
    IEnumerable<FSAppointmentStaffDistinct> createLogItems = null)
  {
    IEnumerable<FSAppointmentStaffDistinct> appointmentStaffDistincts = (IEnumerable<FSAppointmentStaffDistinct>) null;
    DateTime? nullable1 = new DateTime?();
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current?.DetLineRef == null)
      return;
    string str;
    DateTime? nullable2;
    if (createLogItems == null)
    {
      str = ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current?.DetLineRef;
      nullable2 = (DateTime?) ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current?.LogDateTime;
      if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Me.GetValueOrDefault())
      {
        PX.Objects.EP.EPEmployee employeeByUserID = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        if (employeeByUserID != null)
        {
          int num = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (x =>
          {
            int? employeeId = x.EmployeeID;
            int? baccountId = employeeByUserID.BAccountID;
            return employeeId.GetValueOrDefault() == baccountId.GetValueOrDefault() & employeeId.HasValue == baccountId.HasValue;
          })).Count<FSAppointmentEmployee>() > 0 ? 1 : 0;
          bool flag = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (x => x.PrimaryDriver.GetValueOrDefault())).Count<FSAppointmentEmployee>() > 0;
          if (num == 0)
          {
            FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee()
            {
              EmployeeID = employeeByUserID.BAccountID
            };
            if (!flag)
              appointmentEmployee.PrimaryDriver = new bool?(true);
            ((PXSelectBase) this.AppointmentServiceEmployees).Cache.Insert((object) appointmentEmployee);
          }
          FSAppointmentLog fsAppointmentLog1 = new FSAppointmentLog();
          fsAppointmentLog1.ItemType = "SE";
          fsAppointmentLog1.BAccountID = employeeByUserID.BAccountID;
          fsAppointmentLog1.DetLineRef = str;
          fsAppointmentLog1.DateTimeBegin = nullable2;
          FSAppointmentLog fsAppointmentLog2 = (FSAppointmentLog) ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog1);
        }
      }
      else
        appointmentStaffDistincts = GraphHelper.RowCast<FSAppointmentStaffDistinct>((IEnumerable) ((PXSelectBase<FSAppointmentStaffDistinct>) this.LogActionStaffDistinctRecords).Select(Array.Empty<object>())).Where<FSAppointmentStaffDistinct>((Func<FSAppointmentStaffDistinct, bool>) (x => x.Selected.GetValueOrDefault()));
    }
    else
    {
      str = (string) null;
      nullable2 = PXDBDateAndTimeAttribute.CombineDateTime(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate, new DateTime?(PXTimeZoneInfo.Now));
      appointmentStaffDistincts = createLogItems;
    }
    if (appointmentStaffDistincts == null)
      return;
    foreach (FSAppointmentStaffDistinct appointmentStaffDistinct in appointmentStaffDistincts)
    {
      FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
      fsAppointmentLog.ItemType = "SE";
      fsAppointmentLog.BAccountID = appointmentStaffDistinct.BAccountID;
      fsAppointmentLog.DetLineRef = str;
      fsAppointmentLog.DateTimeBegin = nullable2;
      ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
    }
  }

  public virtual void StartNonStockAction(FSAppointmentDet fsAppointmentDet)
  {
    if (fsAppointmentDet == null)
      return;
    FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
    fsAppointmentLog.ItemType = "NS";
    fsAppointmentLog.BAccountID = new int?();
    fsAppointmentLog.DetLineRef = fsAppointmentDet.LineRef;
    fsAppointmentLog.DateTimeBegin = PXDBDateAndTimeAttribute.CombineDateTime(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate, new DateTime?(PXTimeZoneInfo.Now));
    fsAppointmentLog.TimeDuration = new int?(fsAppointmentDet.EstimatedDuration.GetValueOrDefault());
    fsAppointmentLog.Status = "P";
    fsAppointmentLog.TrackTime = new bool?(false);
    fsAppointmentLog.Descr = fsAppointmentDet.TranDesc;
    fsAppointmentLog.TrackOnService = new bool?(true);
    fsAppointmentLog.ProjectID = fsAppointmentDet.ProjectID;
    fsAppointmentLog.ProjectTaskID = fsAppointmentDet.ProjectTaskID;
    fsAppointmentLog.CostCodeID = fsAppointmentDet.CostCodeID;
    ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
  }

  public virtual void CompletePauseAction(
    string logAction,
    DateTime? dateTimeEnd,
    FSAppointmentDet apptDet,
    List<FSAppointmentLog> logList)
  {
    string logStatus = logAction == "PA" ? "S" : "C";
    apptDet = (logAction == "PA" ? 1 : (logAction == "RE" ? 1 : 0)) != 0 ? (FSAppointmentDet) null : apptDet;
    int num = this.CompletePauseMultipleLogs(dateTimeEnd, "CP", logStatus, apptDet == null, logList);
    if (apptDet != null)
      num += this.ChangeItemLineStatus(apptDet, "CP");
    if (num <= 0)
      return;
    if (this.SkipLongOperation)
      this.SaveWithRecalculateExternalTaxesSync();
    else
      ((PXGraph) this).Actions.PressSave();
  }

  public virtual int CompletePauseMultipleLogs(
    DateTime? dateTimeEnd,
    string newAppDetStatus,
    string logStatus,
    bool completeRelatedItemLines,
    List<FSAppointmentLog> logList)
  {
    if (!dateTimeEnd.HasValue)
      dateTimeEnd = PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(PXTimeZoneInfo.Now), new DateTime?(PXTimeZoneInfo.Now));
    int num = 0;
    List<FSAppointmentDet> apptDetRows = (List<FSAppointmentDet>) null;
    if (completeRelatedItemLines)
      apptDetRows = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).ToList<FSAppointmentDet>();
    if (logList != null && logList.Count > 0)
    {
      IEnumerable<FSAppointmentLog> source = GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>()));
      foreach (FSAppointmentLog log in logList)
      {
        FSAppointmentLog listRow = log;
        this.ChangeLogAndRelatedItemLinesStatus(source.Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (_ => _.LineRef == listRow.LineRef)).FirstOrDefault<FSAppointmentLog>(), logStatus, dateTimeEnd.Value, newAppDetStatus, apptDetRows);
        ++num;
      }
    }
    return num;
  }

  public virtual void ResumeMultipleLogs(
    PXSelectBase<FSAppointmentLog> logSelect,
    params object[] logSelectArgs)
  {
    ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime = PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
    this.RunLogActionBase("RE", (string) null, (FSAppointmentDet) null, logSelect, logSelectArgs);
  }

  public virtual FSAppointmentLog ChangeLogAndRelatedItemLinesStatus(
    FSAppointmentLog logRow,
    string newLogStatus,
    DateTime newDateTimeEnd,
    string newApptDetStatus,
    List<FSAppointmentDet> apptDetRows)
  {
    if (logRow == null)
      return (FSAppointmentLog) null;
    bool flag1 = logRow.Status == "S" && newLogStatus == "C";
    logRow = PXCache<FSAppointmentLog>.CreateCopy(logRow);
    logRow.Status = newLogStatus;
    bool? keepDateTimes = logRow.KeepDateTimes;
    bool flag2 = false;
    if (keepDateTimes.GetValueOrDefault() == flag2 & keepDateTimes.HasValue && !flag1)
      logRow.DateTimeEnd = new DateTime?(newDateTimeEnd);
    if (apptDetRows != null && !string.IsNullOrWhiteSpace(logRow.DetLineRef))
    {
      FSAppointmentDet apptDet = apptDetRows.Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r => r.LineRef == logRow.DetLineRef)).FirstOrDefault<FSAppointmentDet>();
      if (apptDet != null)
        this.ChangeItemLineStatus(apptDet, newApptDetStatus);
    }
    return (FSAppointmentLog) ((PXSelectBase) this.LogRecords).Cache.Update((object) logRow);
  }

  public virtual void StartStaffAction(
    IEnumerable<FSAppointmentStaffExtItemLine> createLogItems = null,
    DateTime? dateTimeBegin = null)
  {
    IEnumerable<FSAppointmentStaffExtItemLine> staffExtItemLines = (IEnumerable<FSAppointmentStaffExtItemLine>) null;
    if (createLogItems == null)
    {
      if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Me.GetValueOrDefault())
      {
        PX.Objects.EP.EPEmployee employeeByUserID = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        if (employeeByUserID != null)
        {
          int num = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (x =>
          {
            int? employeeId = x.EmployeeID;
            int? baccountId = employeeByUserID.BAccountID;
            return employeeId.GetValueOrDefault() == baccountId.GetValueOrDefault() & employeeId.HasValue == baccountId.HasValue;
          })).Count<FSAppointmentEmployee>() > 0 ? 1 : 0;
          bool flag = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (x => x.PrimaryDriver.GetValueOrDefault())).Count<FSAppointmentEmployee>() > 0;
          if (num == 0)
          {
            FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee()
            {
              EmployeeID = employeeByUserID.BAccountID
            };
            if (!flag)
              appointmentEmployee.PrimaryDriver = new bool?(true);
            ((PXSelectBase) this.AppointmentServiceEmployees).Cache.Insert((object) appointmentEmployee);
            FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
            fsAppointmentLog.ItemType = "SA";
            fsAppointmentLog.BAccountID = employeeByUserID.BAccountID;
            fsAppointmentLog.DetLineRef = (string) null;
            fsAppointmentLog.DateTimeBegin = dateTimeBegin ?? ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime;
            ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
          }
          else
            staffExtItemLines = GraphHelper.RowCast<FSAppointmentStaffExtItemLine>((IEnumerable) ((PXSelectBase<FSAppointmentStaffExtItemLine>) this.LogActionStaffRecords).Select(Array.Empty<object>())).Where<FSAppointmentStaffExtItemLine>((Func<FSAppointmentStaffExtItemLine, bool>) (x => x.Selected.GetValueOrDefault()));
        }
      }
      else
        staffExtItemLines = GraphHelper.RowCast<FSAppointmentStaffExtItemLine>((IEnumerable) ((PXSelectBase<FSAppointmentStaffExtItemLine>) this.LogActionStaffRecords).Select(Array.Empty<object>())).Where<FSAppointmentStaffExtItemLine>((Func<FSAppointmentStaffExtItemLine, bool>) (x => x.Selected.GetValueOrDefault()));
    }
    else
      staffExtItemLines = createLogItems;
    if (staffExtItemLines == null)
      return;
    foreach (FSAppointmentStaffExtItemLine staffExtItemLine in staffExtItemLines)
    {
      if (staffExtItemLine != null && staffExtItemLine.EstimatedDuration.HasValue)
      {
        int? estimatedDuration = staffExtItemLine.EstimatedDuration;
      }
      FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
      fsAppointmentLog.ItemType = "SA";
      fsAppointmentLog.BAccountID = staffExtItemLine.BAccountID;
      fsAppointmentLog.DetLineRef = staffExtItemLine.DetLineRef;
      fsAppointmentLog.DateTimeBegin = dateTimeBegin ?? ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime;
      ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
    }
  }

  public virtual void StartServiceBasedOnAssignmentAction(
    IEnumerable<FSDetailFSLogAction> createLogItems = null,
    DateTime? dateTimeBegin = null)
  {
    IEnumerable<FSDetailFSLogAction> detailFsLogActions = createLogItems != null ? createLogItems : GraphHelper.RowCast<FSDetailFSLogAction>((IEnumerable) ((PXSelectBase<FSDetailFSLogAction>) this.ServicesLogAction).Select(Array.Empty<object>())).Where<FSDetailFSLogAction>((Func<FSDetailFSLogAction, bool>) (x => x.Selected.GetValueOrDefault()));
    if (detailFsLogActions == null)
      return;
    foreach (FSDetailFSLogAction detailFsLogAction in detailFsLogActions)
    {
      FSDetailFSLogAction fsDetailLogActionRow = detailFsLogAction;
      IEnumerable<FSAppointmentEmployee> source = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (x => x.ServiceLineRef == fsDetailLogActionRow.LineRef));
      if (source.Count<FSAppointmentEmployee>() > 0)
      {
        foreach (FSAppointmentEmployee appointmentEmployee in source)
        {
          FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
          fsAppointmentLog.ItemType = "SA";
          fsAppointmentLog.BAccountID = appointmentEmployee.EmployeeID;
          fsAppointmentLog.DetLineRef = appointmentEmployee.ServiceLineRef;
          fsAppointmentLog.DateTimeBegin = dateTimeBegin ?? ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime;
          ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
        }
      }
      else
      {
        FSAppointmentLog fsAppointmentLog = new FSAppointmentLog();
        fsAppointmentLog.ItemType = "SE";
        fsAppointmentLog.BAccountID = new int?();
        fsAppointmentLog.DetLineRef = fsDetailLogActionRow.LineRef;
        fsAppointmentLog.DateTimeBegin = dateTimeBegin ?? ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime;
        ((PXSelectBase) this.LogRecords).Cache.Insert((object) fsAppointmentLog);
      }
    }
  }

  public virtual void POCreateVerifyValue(PXCache sender, FSAppointmentDet row, bool? value)
  {
    ServiceOrderEntry.POCreateVerifyValueInt<FSAppointmentDet.enablePO>(sender, (object) row, row.InventoryID, value);
  }

  public virtual void SetHeaderActualDateTimeBegin(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    DateTime? dateTimeBegin)
  {
    if (fsAppointmentRow == null)
      return;
    bool? manuallyActualTime = fsAppointmentRow.HandleManuallyActualTime;
    bool flag = false;
    if (!(manuallyActualTime.GetValueOrDefault() == flag & manuallyActualTime.HasValue))
      return;
    cache.SetValueExtIfDifferent<FSAppointment.executionDate>((object) fsAppointmentRow, (object) dateTimeBegin.Value.Date);
    cache.SetValueExtIfDifferent<FSAppointment.actualDateTimeBegin>((object) fsAppointmentRow, (object) dateTimeBegin);
  }

  public virtual void SetHeaderActualDateTimeEnd(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    DateTime? dateTimeEnd)
  {
    if (fsAppointmentRow == null)
      return;
    bool? manuallyActualTime = fsAppointmentRow.HandleManuallyActualTime;
    bool flag = false;
    if (!(manuallyActualTime.GetValueOrDefault() == flag & manuallyActualTime.HasValue))
      return;
    cache.SetValueExtIfDifferent<FSAppointment.actualDateTimeEnd>((object) fsAppointmentRow, (object) dateTimeEnd);
  }

  public virtual bool ActualDateAndTimeValidation(FSAppointment fsAppointmentRow)
  {
    return fsAppointmentRow.ActualDateTimeBegin.HasValue && fsAppointmentRow.ActualDateTimeEnd.HasValue;
  }

  public virtual string GetValidAppDetStatus(FSAppointmentDet row, string newStatus)
  {
    if (newStatus != "CC" && newStatus != "WP" && newStatus != "RP")
    {
      if (row.ShouldBeWaitingPO)
        return "WP";
      if (row.ShouldBeRequestPO)
        return "RP";
    }
    return newStatus;
  }

  public virtual int GetLogTrackingCount(string apptDetLineRef)
  {
    return GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (l => l.DetLineRef == apptDetLineRef && l.TrackOnService.GetValueOrDefault())).Count<FSAppointmentLog>();
  }

  public virtual void ForceAppointmentDetActualFieldsUpdate(bool reopeningAppointment)
  {
    foreach (FSAppointmentDet apptDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r => !r.IsLinkedItem)))
    {
      if (reopeningAppointment && apptDet.Status != "NS" && apptDet.Status != "WP" && apptDet.Status != "RP")
        this.ChangeItemLineStatus(apptDet, "NS");
      FSAppointmentDet copy = (FSAppointmentDet) ((PXSelectBase) this.AppointmentDetails).Cache.CreateCopy((object) apptDet);
      ((PXSelectBase) this.AppointmentDetails).Cache.SetDefaultExt<FSAppointmentDet.areActualFieldsActive>((object) copy);
      if (!((PXSelectBase) this.AppointmentDetails).Cache.ObjectsEqual<FSAppointmentDet.curyEstimatedTranAmt, FSAppointmentDet.actualDuration, FSAppointmentDet.actualQty, FSAppointmentDet.curyTranAmt, FSAppointmentDet.curyExtPrice>((object) apptDet, (object) copy))
        ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(copy);
    }
  }

  public virtual void OnApptStartTimeChangeUpdateLogStartTime(
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    ServiceOrderBase<AppointmentEntry, FSAppointment>.AppointmentLog_View logRecords)
  {
    if (fsAppointmentRow == null || fsSrvOrdTypeRow == null)
      return;
    bool? nullable = fsSrvOrdTypeRow.OnStartTimeChangeUpdateLogStartTime;
    if (!nullable.GetValueOrDefault())
      return;
    foreach (FSAppointmentLog fsAppointmentLog in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) logRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (_ => _.ItemType != "TR" && _.DateTimeBegin.HasValue)).GroupBy(_ => new
    {
      BAccountID = _.BAccountID,
      DetLineRef = _.DetLineRef
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType1<int?, string>, FSAppointmentLog>, FSAppointmentLog>(_ => _.OrderBy<FSAppointmentLog, DateTime?>((Func<FSAppointmentLog, DateTime?>) (d => d.DateTimeBegin)).First<FSAppointmentLog>()))
    {
      DateTime? dateTimeBegin = fsAppointmentLog.DateTimeBegin;
      DateTime? actualDateTimeBegin = fsAppointmentRow.ActualDateTimeBegin;
      if ((dateTimeBegin.HasValue == actualDateTimeBegin.HasValue ? (dateTimeBegin.HasValue ? (dateTimeBegin.GetValueOrDefault() == actualDateTimeBegin.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      {
        nullable = fsAppointmentLog.KeepDateTimes;
        if (!nullable.GetValueOrDefault())
        {
          FSAppointmentLog copy = (FSAppointmentLog) ((PXSelectBase) logRecords).Cache.CreateCopy((object) fsAppointmentLog);
          copy.DateTimeBegin = fsAppointmentRow.ActualDateTimeBegin;
          DateTime? dateTimeEnd = copy.DateTimeEnd;
          dateTimeBegin = copy.DateTimeBegin;
          if ((dateTimeEnd.HasValue & dateTimeBegin.HasValue ? (dateTimeEnd.GetValueOrDefault() < dateTimeBegin.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            copy.DateTimeEnd = copy.DateTimeBegin;
          ((PXSelectBase) logRecords).Cache.Update((object) copy);
        }
      }
    }
  }

  public virtual void OnApptEndTimeChangeUpdateLogEndTime(
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    ServiceOrderBase<AppointmentEntry, FSAppointment>.AppointmentLog_View logRecords)
  {
    if (fsAppointmentRow == null || fsSrvOrdTypeRow == null)
      return;
    bool? nullable = fsSrvOrdTypeRow.OnEndTimeChangeUpdateLogEndTime;
    if (!nullable.GetValueOrDefault())
      return;
    foreach (FSAppointmentLog fsAppointmentLog in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) logRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (_ => _.ItemType != "TR")).GroupBy(_ => new
    {
      BAccountID = _.BAccountID,
      DetLineRef = _.DetLineRef
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType1<int?, string>, FSAppointmentLog>, FSAppointmentLog>(_ => _.OrderByDescending<FSAppointmentLog, DateTime?>((Func<FSAppointmentLog, DateTime?>) (d => d.DateTimeEnd)).First<FSAppointmentLog>()))
    {
      DateTime? dateTimeEnd = fsAppointmentLog.DateTimeEnd;
      DateTime? actualDateTimeEnd = fsAppointmentRow.ActualDateTimeEnd;
      if ((dateTimeEnd.HasValue == actualDateTimeEnd.HasValue ? (dateTimeEnd.HasValue ? (dateTimeEnd.GetValueOrDefault() == actualDateTimeEnd.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      {
        nullable = fsAppointmentLog.KeepDateTimes;
        if (!nullable.GetValueOrDefault())
        {
          nullable = fsSrvOrdTypeRow.AllowManualLogTimeEdition;
          if (!nullable.GetValueOrDefault() && !(fsAppointmentLog.Status != "P"))
          {
            FSAppointmentLog copy = (FSAppointmentLog) ((PXSelectBase) logRecords).Cache.CreateCopy((object) fsAppointmentLog);
            copy.DateTimeEnd = fsAppointmentRow.ActualDateTimeEnd;
            DateTime? dateTimeBegin = copy.DateTimeBegin;
            dateTimeEnd = copy.DateTimeEnd;
            if ((dateTimeBegin.HasValue & dateTimeEnd.HasValue ? (dateTimeBegin.GetValueOrDefault() > dateTimeEnd.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              copy.DateTimeBegin = copy.DateTimeEnd;
            ((PXSelectBase) logRecords).Cache.Update((object) copy);
          }
        }
      }
    }
  }

  protected void DeleteUnpersistedServiceOrderRelated(FSAppointment fsAppointmentRow)
  {
    int? soid = fsAppointmentRow.SOID;
    int num = 0;
    if (!(soid.GetValueOrDefault() < num & soid.HasValue))
      return;
    ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Delete(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).SelectSingle(Array.Empty<object>()));
    fsAppointmentRow.SOID = new int?();
  }

  protected virtual void LoadServiceOrderRelated(FSAppointment fsAppointmentRow)
  {
    if (fsAppointmentRow.ReloadServiceOrderRelated.GetValueOrDefault())
    {
      ((PXSelectBase) this.ServiceOrderRelated).Cache.ClearQueryCache();
      ((PXSelectBase) this.ServiceOrderRelated).Cache.Clear();
      ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current = (FSServiceOrder) null;
      fsAppointmentRow.ReloadServiceOrderRelated = new bool?(false);
    }
    if (fsAppointmentRow.SrvOrdType == null || !fsAppointmentRow.SOID.HasValue)
      return;
    int? nullable1;
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null)
    {
      int? soid1 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SOID;
      int? soid2 = fsAppointmentRow.SOID;
      if (soid1.GetValueOrDefault() == soid2.GetValueOrDefault() & soid1.HasValue == soid2.HasValue)
        return;
      nullable1 = fsAppointmentRow.SOID;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
        return;
    }
    ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).SelectSingle(new object[1]
    {
      (object) fsAppointmentRow.SOID
    });
    FSAppointment fsAppointment1 = fsAppointmentRow;
    FSServiceOrder current1 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    int? nullable2;
    if (current1 == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = current1.CustomerID;
    fsAppointment1.CustomerID = nullable2;
    FSAppointment fsAppointment2 = fsAppointmentRow;
    FSServiceOrder current2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    int? nullable3;
    if (current2 == null)
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    else
      nullable3 = current2.BillCustomerID;
    fsAppointment2.BillCustomerID = nullable3;
    FSAppointment fsAppointment3 = fsAppointmentRow;
    FSServiceOrder current3 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    int? nullable4;
    if (current3 == null)
    {
      nullable1 = new int?();
      nullable4 = nullable1;
    }
    else
      nullable4 = current3.BranchID;
    fsAppointment3.BranchID = nullable4;
    fsAppointmentRow.CuryID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current?.CuryID;
  }

  protected virtual void VerifyIsAlreadyPosted<Field>(
    PXCache cache,
    FSAppointmentDet fsAppointmentDetRow,
    FSBillingCycle billingCycleRow)
    where Field : class, IBqlField
  {
    if (fsAppointmentDetRow == null || ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current == null || billingCycleRow == null)
      return;
    IFSSODetBase eRow = (IFSSODetBase) null;
    int? nullable1 = new int?(-1);
    if (fsAppointmentDetRow.IsInventoryItem)
    {
      eRow = (IFSSODetBase) fsAppointmentDetRow;
      nullable1 = fsAppointmentDetRow.SODetID;
    }
    else if (fsAppointmentDetRow.IsPickupDelivery)
    {
      eRow = (IFSSODetBase) fsAppointmentDetRow;
      int? nullable2 = fsAppointmentDetRow.AppDetID;
      int num = 0;
      int? nullable3;
      if (!(nullable2.GetValueOrDefault() > num & nullable2.HasValue))
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = fsAppointmentDetRow.AppDetID;
      nullable1 = nullable3;
    }
    PXEntryStatus status = ((PXSelectBase) this.ServiceOrderRelated).Cache.GetStatus((object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
    int num1 = status == 1 ? 1 : (status == 0 ? 1 : 0);
    bool flag = ((PXSelectBase<FSPostDet>) this.ServiceOrderPostedIn).SelectWindowed(0, 1, Array.Empty<object>()).Count > 0;
    string billingMode = this.GetBillingMode(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
    if (num1 == 0 || nullable1.HasValue || this.IsInstructionOrComment((object) eRow) || !(billingMode == "SO") || !flag)
      return;
    cache.RaiseExceptionHandling<Field>((object) eRow, (object) eRow.InventoryID, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormat("A new {0} cannot be added to this appointment because an invoice has already been generated for the related service order.", new object[1]
    {
      (object) this.GetLineType(eRow.LineType, true)
    }), (PXErrorLevel) 5));
  }

  public virtual bool ValidateCustomerBillingCycle(
    PXCache serviceOrderCache,
    FSServiceOrder serviceOrder,
    PXCache appointmentCache,
    FSAppointment appointment,
    int? billCustomerID,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSSetup setupRecordRow,
    bool justWarn)
  {
    return this.ValidateCustomerBillingCycle<FSServiceOrder.billCustomerID>(serviceOrderCache, (object) serviceOrder, billCustomerID, fsSrvOrdTypeRow, setupRecordRow, justWarn);
  }

  public virtual void ClearAPBillReferences(FSAppointment fsAppointmentRow)
  {
    ServiceOrderEntry serviceOrderEntry = (ServiceOrderEntry) null;
    foreach (FSAppointmentDet fsAppointmentDet in ((PXSelectBase) this.AppointmentDetails).Cache.Deleted)
    {
      if (serviceOrderEntry == null)
      {
        serviceOrderEntry = this.GetServiceOrderEntryGraph(true);
        if (!serviceOrderEntry.RunningPersist)
          ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fsAppointmentRow.SORefNbr, new object[1]
          {
            (object) fsAppointmentRow.SrvOrdType
          }));
      }
      if (fsAppointmentDet.IsAPBillItem)
      {
        FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.Select((PXGraph) serviceOrderEntry, new object[1]
        {
          (object) fsAppointmentDet.SODetID
        }));
        ((PXSelectBase) serviceOrderEntry.ServiceOrderDetails).Cache.Delete((object) fssoDet);
      }
    }
    if (serviceOrderEntry == null || !((PXGraph) serviceOrderEntry).IsDirty || serviceOrderEntry.RunningPersist)
      return;
    ((PXGraph) serviceOrderEntry).Actions.PressSave();
  }

  public virtual void SaveWithRecalculateExternalTaxesSync()
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
  }

  public virtual bool CanChangePOOptions(
    FSAppointmentDet apptLine,
    ref FSSODet soLine,
    string fieldName,
    out PXException exception)
  {
    return this.CanChangePOOptions(apptLine, false, ref soLine, fieldName, out exception);
  }

  public virtual bool CanChangePOOptions(
    FSAppointmentDet apptLine,
    bool runningRowSelecting,
    ref FSSODet soLine,
    string fieldName,
    out PXException exception)
  {
    exception = (PXException) null;
    if (apptLine == null)
      return false;
    if (apptLine.SODetID.HasValue)
    {
      int? nullable1 = apptLine.SODetID;
      int num1 = 0;
      if (!(nullable1.GetValueOrDefault() < num1 & nullable1.HasValue))
      {
        if (soLine == null)
        {
          soLine = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) apptLine.SODetID
          }));
          if (soLine == null)
            return false;
        }
        nullable1 = soLine.ApptCntr;
        int num2 = 1;
        if (!(nullable1.GetValueOrDefault() > num2 & nullable1.HasValue))
        {
          bool? nullable2 = soLine.IsPrepaid;
          if (!nullable2.GetValueOrDefault())
          {
            if (soLine.POType != null || soLine.PONbr != null)
            {
              if (fieldName == typeof (FSAppointmentDet.enablePO).Name)
                exception = (PXException) new PXSetPropertyException("You cannot clear the Mark for PO check box for this line because the purchase order for this line has already been created.", (PXErrorLevel) 4);
              if (fieldName == typeof (FSAppointmentDet.pOSource).Name)
                exception = (PXException) new PXSetPropertyException("You cannot change the PO source for this line because the purchase order for this line has already been created.", (PXErrorLevel) 4);
              return false;
            }
            nullable1 = soLine.ApptCntr;
            Decimal num3 = (Decimal) nullable1.Value;
            nullable1 = apptLine.AppDetID;
            int num4 = 0;
            if (nullable1.GetValueOrDefault() > num4 & nullable1.HasValue)
            {
              nullable2 = apptLine.SODetCreate;
              if (nullable2.GetValueOrDefault())
              {
                if ((!runningRowSelecting ? (string) ((PXGraph) this).Caches[typeof (FSAppointmentDet)].GetValueOriginal<FSAppointmentDet.status>((object) apptLine) : apptLine.Status) == "RP")
                  ++num3;
              }
            }
            nullable1 = apptLine.AppDetID;
            int num5 = 0;
            if (!(nullable1.GetValueOrDefault() > num5 & nullable1.HasValue) || !(num3 > 1M))
            {
              nullable1 = apptLine.AppDetID;
              int num6 = 0;
              if (!(nullable1.GetValueOrDefault() < num6 & nullable1.HasValue) || !(num3 > 0M))
              {
                nullable2 = apptLine.SODetCreate;
                bool flag = false;
                if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
                  return true;
                if (fieldName == typeof (FSAppointmentDet.enablePO).Name)
                  exception = (PXException) new PXSetPropertyException("You cannot select the Mark for PO check box in this line because the line has been created on the Service Orders (FS300100) form.", (PXErrorLevel) 4);
                if (fieldName == typeof (FSAppointmentDet.pOSource).Name)
                  exception = (PXException) new PXSetPropertyException("You cannot change the PO source for this line because the purchase has been requested from the service order.", (PXErrorLevel) 4);
                return false;
              }
            }
            if (fieldName == typeof (FSAppointmentDet.enablePO).Name)
              exception = (PXException) new PXSetPropertyException("You cannot clear the Mark for PO check box for this line because other appointments have lines that are associated with the service order line related to this line.", (PXErrorLevel) 4);
            if (fieldName == typeof (FSAppointmentDet.pOSource).Name)
              exception = (PXException) new PXSetPropertyException("You cannot change the PO source for this line because other appointments have lines that are associated with the service order line related to this line.", (PXErrorLevel) 4);
            return false;
          }
        }
        return false;
      }
    }
    return true;
  }

  public virtual DateTime? GetDateTimeEnd(
    DateTime? dateTimeBegin,
    int hour = 0,
    int minute = 0,
    int second = 0,
    int milisecond = 0)
  {
    return AppointmentEntry.GetDateTimeEndInt(dateTimeBegin, hour, minute, second, milisecond);
  }

  public virtual FSSODet GetSODetFromAppointmentDet(
    PXGraph graph,
    FSAppointmentDet fsAppointmentDetRow)
  {
    return AppointmentEntry.GetSODetFromAppointmentDetInt(graph, fsAppointmentDetRow);
  }

  /// <summary>
  /// Evaluates whether the Employee's slot can contain the Appointment's duration.
  /// </summary>
  /// <param name="slotBegin">DateTime of Start of the Employee Schedule.</param>
  /// <param name="slotEnd">DateTime of End of the Employee Schedule.</param>
  /// <param name="beginTime">Begin DateTime of the possible overlap Slot.</param>
  /// <param name="endTime">End DateTime of the possible overlap Slot.</param>
  /// <returns><c>Enum</c> indicating if the appointment is contained, partially contained or not contained in the Employee's work slot.</returns>
  public virtual AppointmentEntry.SlotIsContained SlotIsContainedInSlot(
    DateTime? slotBegin,
    DateTime? slotEnd,
    DateTime? beginTime,
    DateTime? endTime)
  {
    return AppointmentEntry.SlotIsContainedInSlotInt(slotBegin, slotEnd, slotEnd, beginTime);
  }

  public virtual string GetItemLineRef(PXGraph graph, int? appointmentID, bool isTravel = false)
  {
    FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.isTravelItem, Equal<Required<FSAppointmentDet.isTravelItem>>, And<FSAppointmentDet.appointmentID, Equal<Required<FSAppointment.appointmentID>>, And<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.InProcess>>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) isTravel,
      (object) appointmentID
    }));
    return fsAppointmentDet == null || !(fsAppointmentDet.LineType == "SERVI") ? (string) null : fsAppointmentDet.LineRef;
  }

  public virtual void ValidateSrvOrdTypeNumberingSequence(PXGraph graph, string srvOrdType)
  {
    AppointmentEntry.ValidateSrvOrdTypeNumberingSequenceInt(graph, srvOrdType);
  }

  public virtual bool GetRequireCustomerSignature(
    PXGraph graph,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSServiceOrder fsServiceOrderRow)
  {
    if (fsSrvOrdTypeRow == null || fsServiceOrderRow == null)
      return false;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsServiceOrderRow.CustomerID
    }));
    FSxCustomer extension = customer != null ? PXCache<PX.Objects.AR.Customer>.GetExtension<FSxCustomer>(customer) : (FSxCustomer) null;
    if (fsSrvOrdTypeRow.RequireCustomerSignature.GetValueOrDefault())
      return true;
    return extension != null && extension.RequireCustomerSignature.GetValueOrDefault();
  }

  public virtual NotificationSource GetSource(
    PXGraph graph,
    string classID,
    Guid setupID,
    int? branchID)
  {
    PXResultset<NotificationSource> pxResultset = PXSelectBase<NotificationSource, PXSelect<NotificationSource, Where<NotificationSource.setupID, Equal<Required<NotificationSource.setupID>>, And<NotificationSource.classID, Equal<Required<NotificationSource.classID>>, And<NotificationSource.active, Equal<True>>>>>.Config>.Select(graph, new object[2]
    {
      (object) setupID,
      (object) classID
    });
    NotificationSource source1 = (NotificationSource) null;
    foreach (PXResult<NotificationSource> pxResult in pxResultset)
    {
      NotificationSource source2 = PXResult<NotificationSource>.op_Implicit(pxResult);
      int? nbranchId = source2.NBranchID;
      int? nullable = branchID;
      if (nbranchId.GetValueOrDefault() == nullable.GetValueOrDefault() & nbranchId.HasValue == nullable.HasValue)
        return source2;
      nullable = source2.NBranchID;
      if (!nullable.HasValue)
        source1 = source2;
    }
    return source1;
  }

  /// <summary>Add the EmailSource.</summary>
  public virtual void AddEmailSource(PXGraph graph, int? sourceEmailID, RecipientList recipients)
  {
    EMailAccount emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Required<EMailAccount.emailAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) sourceEmailID
    }));
    if (emailAccount == null || emailAccount.Address == null)
      return;
    NotificationRecipient notificationRecipient = new NotificationRecipient()
    {
      Active = new bool?(true),
      Email = emailAccount.Address,
      AddTo = "T",
      Format = "H"
    };
    if (notificationRecipient == null)
      return;
    recipients.Add(notificationRecipient);
  }

  /// <summary>
  /// Add the Customer info as a recipient in the Email template generated by Appointment.
  /// </summary>
  public virtual void AddCustomerRecipient(
    AppointmentEntry graphAppointmentEntry,
    NotificationRecipient recSetup,
    RecipientList recipients)
  {
    NotificationRecipient notificationRecipient = (NotificationRecipient) null;
    if (PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
    {
      (object) ((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current.CustomerID
    })) == null)
      return;
    FSContact fsContact = ((PXSelectBase<FSContact>) graphAppointmentEntry.ServiceOrder_Contact).SelectSingle(Array.Empty<object>());
    if (fsContact != null && fsContact.Email != null)
    {
      notificationRecipient = new NotificationRecipient()
      {
        Active = new bool?(true),
        Email = fsContact.Email,
        AddTo = recSetup.AddTo,
        Format = recSetup.Format
      };
    }
    else
    {
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
      {
        (object) ((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current.ContactID
      }));
      if (contact != null && contact.EMail != null)
        notificationRecipient = new NotificationRecipient()
        {
          Active = new bool?(true),
          Email = contact.EMail,
          AddTo = recSetup.AddTo,
          Format = recSetup.Format
        };
    }
    if (notificationRecipient == null)
      return;
    recipients.Add(notificationRecipient);
  }

  private static void AddStaffRecipient(
    AppointmentEntry graphAppointmentEntry,
    int? bAccountID,
    string type,
    NotificationRecipient recSetup,
    RecipientList recipients)
  {
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) null;
    switch (type)
    {
      case "EP":
        contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.CR.BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.BAccount.type, Equal<Required<PX.Objects.CR.BAccount.type>>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[2]
        {
          (object) bAccountID,
          (object) type
        }));
        break;
      case "VE":
        contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>, And<PX.Objects.CR.BAccount.type, Equal<Required<PX.Objects.CR.BAccount.type>>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[2]
        {
          (object) bAccountID,
          (object) type
        }));
        break;
    }
    if (contact == null || contact.EMail == null)
      return;
    NotificationRecipient notificationRecipient = new NotificationRecipient()
    {
      Active = new bool?(true),
      Email = contact.EMail,
      AddTo = recSetup.AddTo,
      Format = recSetup.Format
    };
    if (notificationRecipient == null)
      return;
    recipients.Add(notificationRecipient);
  }

  /// <summary>
  /// Add the Employee info defined in the Notification tab defined in the <c>SrvOrdType</c> as a recipient(s) in the Email template generated by Appointment.
  /// </summary>
  public virtual void AddEmployeeRecipient(
    PXGraph graph,
    NotificationRecipient recSetup,
    RecipientList recipients)
  {
    PXResult<PX.Objects.CR.Contact, PX.Objects.CR.BAccount, PX.Objects.EP.EPEmployee> pxResult = (PXResult<PX.Objects.CR.Contact, PX.Objects.CR.BAccount, PX.Objects.EP.EPEmployee>) PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.BAccount.parentBAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>, And<PX.Objects.CR.BAccount.type, Equal<Required<PX.Objects.CR.BAccount.type>>>>>.Config>.Select(graph, new object[2]
    {
      (object) recSetup.ContactID,
      (object) "EP"
    }));
    PX.Objects.CR.Contact contact = PXResult<PX.Objects.CR.Contact, PX.Objects.CR.BAccount, PX.Objects.EP.EPEmployee>.op_Implicit(pxResult);
    PXResult<PX.Objects.CR.Contact, PX.Objects.CR.BAccount, PX.Objects.EP.EPEmployee>.op_Implicit(pxResult);
    if (PXResult<PX.Objects.CR.Contact, PX.Objects.CR.BAccount, PX.Objects.EP.EPEmployee>.op_Implicit(pxResult) == null || contact == null || contact.EMail == null)
      return;
    NotificationRecipient notificationRecipient = new NotificationRecipient()
    {
      Active = new bool?(true),
      Email = contact.EMail,
      AddTo = recSetup.AddTo,
      Format = recSetup.Format
    };
    if (notificationRecipient == null)
      return;
    recipients.Add(notificationRecipient);
  }

  /// <summary>
  /// Add the Billing Customer info as a recipient(s) in the Email template generated by Appointment.
  /// </summary>
  public virtual void AddBillingRecipient(
    AppointmentEntry graphAppointmentEntry,
    NotificationRecipient recSetup,
    RecipientList recipients)
  {
    NotificationRecipient notificationRecipient = (NotificationRecipient) null;
    if (((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current.BillCustomerID.HasValue)
    {
      if (PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
      {
        (object) ((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current.BillCustomerID
      })) == null)
        return;
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
      {
        (object) ((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current.BillCustomerID
      }));
      if (contact != null && contact.EMail != null)
        notificationRecipient = new NotificationRecipient()
        {
          Active = new bool?(true),
          Email = contact.EMail,
          AddTo = recSetup.AddTo,
          Format = recSetup.Format
        };
    }
    if (notificationRecipient == null)
      return;
    recipients.Add(notificationRecipient);
  }

  /// <summary>
  /// Adds the Employee(s) belonging to the Appointment's Service Area as recipients in the Email template generated by Appointment.
  /// </summary>
  public virtual void AddGeoZoneStaffRecipient(
    AppointmentEntry graphAppointmentEntry,
    NotificationRecipient recSetup,
    RecipientList recipients)
  {
    List<FSGeoZoneEmp> source = new List<FSGeoZoneEmp>();
    FSAddress fsAddress = ((PXSelectBase<FSAddress>) graphAppointmentEntry.ServiceOrder_Address).SelectSingle(Array.Empty<object>());
    if (fsAddress != null && fsAddress.PostalCode != null)
    {
      FSGeoZonePostalCode geoZonePostalCode = StaffSelectionHelper.GetMatchingGeoZonePostalCode((PXGraph) graphAppointmentEntry, fsAddress.PostalCode);
      if (geoZonePostalCode != null)
      {
        foreach (PXResult<FSGeoZonePostalCode, FSGeoZoneEmp> pxResult in PXSelectBase<FSGeoZonePostalCode, PXSelectJoin<FSGeoZonePostalCode, InnerJoin<FSGeoZoneEmp, On<FSGeoZoneEmp.geoZoneID, Equal<FSGeoZonePostalCode.geoZoneID>>>, Where<FSGeoZonePostalCode.postalCode, Equal<Required<FSGeoZonePostalCode.postalCode>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
        {
          (object) geoZonePostalCode.PostalCode
        }))
          source.Add(PXResult<FSGeoZonePostalCode, FSGeoZoneEmp>.op_Implicit(pxResult));
      }
    }
    List<FSGeoZoneEmp> list = source.GroupBy<FSGeoZoneEmp, int?>((Func<FSGeoZoneEmp, int?>) (x => x.EmployeeID)).Select<IGrouping<int?, FSGeoZoneEmp>, FSGeoZoneEmp>((Func<IGrouping<int?, FSGeoZoneEmp>, FSGeoZoneEmp>) (grp => grp.First<FSGeoZoneEmp>())).ToList<FSGeoZoneEmp>();
    if (list.Count <= 0)
      return;
    foreach (FSGeoZoneEmp fsGeoZoneEmp in list)
      AppointmentEntry.AddStaffRecipient(graphAppointmentEntry, fsGeoZoneEmp.EmployeeID, "EP", recSetup, recipients);
  }

  /// <summary>
  /// Add the Employee email that has assigned the salesperson as a recipient in the Email template generated by Appointment.
  /// </summary>
  public virtual void AddSalespersonRecipient(
    AppointmentEntry graphAppointmentEntry,
    NotificationRecipient recSetup,
    RecipientList recipients)
  {
    PXResult<PX.Objects.AR.SalesPerson, PX.Objects.EP.EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact> pxResult = (PXResult<PX.Objects.AR.SalesPerson, PX.Objects.EP.EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>) PXResultset<PX.Objects.AR.SalesPerson>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesPerson, PXSelectJoin<PX.Objects.AR.SalesPerson, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.salesPersonID, Equal<PX.Objects.AR.SalesPerson.salesPersonID>>, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.EP.EPEmployee.bAccountID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.BAccount.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.CR.BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>>, Where<PX.Objects.AR.SalesPerson.salesPersonID, Equal<Required<FSAppointment.salesPersonID>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
    {
      (object) ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current.SalesPersonID
    }));
    PX.Objects.CR.Contact contact = PXResult<PX.Objects.AR.SalesPerson, PX.Objects.EP.EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit(pxResult);
    PXResult<PX.Objects.AR.SalesPerson, PX.Objects.EP.EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit(pxResult);
    PX.Objects.EP.EPEmployee epEmployee = PXResult<PX.Objects.AR.SalesPerson, PX.Objects.EP.EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit(pxResult);
    PX.Objects.AR.SalesPerson salesPerson = PXResult<PX.Objects.AR.SalesPerson, PX.Objects.EP.EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit(pxResult);
    if (epEmployee == null || salesPerson == null || contact == null || contact.EMail == null)
      return;
    NotificationRecipient notificationRecipient = new NotificationRecipient()
    {
      Active = new bool?(true),
      Email = contact.EMail,
      AddTo = recSetup.AddTo,
      Format = recSetup.Format
    };
    if (notificationRecipient == null)
      return;
    recipients.Add(notificationRecipient);
  }

  public virtual RecipientList GetRecipients(
    AppointmentEntry graphAppointmentEntry,
    int? sourceID,
    int? sourceEmailID)
  {
    RecipientList recipients = new RecipientList();
    RecipientList recipientList = new RecipientList();
    bool flag = true;
    PXResultset<NotificationRecipient> pxResultset = PXSelectBase<NotificationRecipient, PXSelect<NotificationRecipient, Where<NotificationRecipient.sourceID, Equal<Required<NotificationRecipient.sourceID>>, And<NotificationRecipient.active, Equal<True>>>, OrderBy<Asc<NotificationRecipient.notificationID>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
    {
      (object) sourceID
    });
    foreach (PXResult<NotificationRecipient> pxResult in pxResultset)
    {
      if (PXResult<NotificationRecipient>.op_Implicit(pxResult).AddTo != "B")
      {
        flag = false;
        break;
      }
    }
    if (flag)
    {
      this.AddEmailSource((PXGraph) graphAppointmentEntry, sourceEmailID, recipientList);
      this.VerifyRecipientsAndAddToList(recipientList, recipients, false, (string) null);
    }
    List<FSAppointmentEmployee> list = ((IEnumerable<PXResult<FSAppointmentEmployee>>) ((PXSelectBase<FSAppointmentEmployee>) graphAppointmentEntry.AppointmentServiceEmployees).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentEmployee>>().GroupBy((Func<PXResult<FSAppointmentEmployee>, int?>) (p => PXResult<FSAppointmentEmployee>.op_Implicit(p).EmployeeID), (key, group) => new
    {
      Group = PXResult<FSAppointmentEmployee>.op_Implicit(group.First<PXResult<FSAppointmentEmployee>>())
    }).Select(g => g.Group).ToList<FSAppointmentEmployee>();
    foreach (PXResult<NotificationRecipient> pxResult in pxResultset)
    {
      NotificationRecipient recSetup = PXResult<NotificationRecipient>.op_Implicit(pxResult);
      string contactType = recSetup.ContactType;
      if (contactType != null && contactType.Length == 1)
      {
        switch (contactType[0])
        {
          case 'B':
            this.AddBillingRecipient(graphAppointmentEntry, recSetup, recipientList);
            this.VerifyRecipientsAndAddToList(recipientList, recipients, true, "Failed to send the email. Email address is not specified for the current billing customer.");
            continue;
          case 'E':
            this.AddEmployeeRecipient((PXGraph) graphAppointmentEntry, recSetup, recipientList);
            this.VerifyRecipientsAndAddToList(recipientList, recipients, true, "Failed to send the email. Email address is not specified for the notification recipient of the Employee type.");
            continue;
          case 'F':
            using (List<FSAppointmentEmployee>.Enumerator enumerator = list.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                FSAppointmentEmployee current = enumerator.Current;
                if (current.Type == "EP")
                {
                  AppointmentEntry.AddStaffRecipient(graphAppointmentEntry, current.EmployeeID, current.Type, recSetup, recipientList);
                  this.VerifyRecipientsAndAddToList(recipientList, recipients, true, "Failed to send the email. Email address is not specified for the current staff of the Employee type.");
                }
              }
              continue;
            }
          case 'G':
            this.AddGeoZoneStaffRecipient(graphAppointmentEntry, recSetup, recipientList);
            this.VerifyRecipientsAndAddToList(recipientList, recipients, true, "Failed to send the email. Email address is not specified for the staff in the current geographical zone.");
            continue;
          case 'L':
            this.AddSalespersonRecipient(graphAppointmentEntry, recSetup, recipientList);
            this.VerifyRecipientsAndAddToList(recipientList, recipients, true, "Failed to send the email. Email address is not specified for the current salesperson.");
            continue;
          case 'U':
            if (((PXSelectBase<FSServiceOrder>) graphAppointmentEntry.ServiceOrderRelated).Current.CustomerID.HasValue)
            {
              this.AddCustomerRecipient(graphAppointmentEntry, recSetup, recipientList);
              this.VerifyRecipientsAndAddToList(recipientList, recipients, true, "Failed to send the email. Email address is not specified for the current customer.");
              continue;
            }
            continue;
          case 'X':
            using (List<FSAppointmentEmployee>.Enumerator enumerator = list.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                FSAppointmentEmployee current = enumerator.Current;
                if (current.Type == "VE")
                {
                  AppointmentEntry.AddStaffRecipient(graphAppointmentEntry, current.EmployeeID, current.Type, recSetup, recipientList);
                  this.VerifyRecipientsAndAddToList(recipientList, recipients, true, "Failed to send the email. Email address is not specified for the current staff of the Vendor type.");
                }
              }
              continue;
            }
          default:
            continue;
        }
      }
    }
    if (recipients.Count<NotificationRecipient>() == 1 && flag)
      recipients = (RecipientList) null;
    return recipients;
  }

  public virtual void VerifyRecipientsAndAddToList(
    RecipientList unverifiedRecipients,
    RecipientList verifiedRecipients,
    bool throwError,
    string errorMessage)
  {
    foreach (NotificationRecipient unverifiedRecipient in unverifiedRecipients)
    {
      if (string.IsNullOrWhiteSpace(unverifiedRecipient.Email))
      {
        if (throwError)
          throw new PXException(errorMessage);
      }
      else
        verifiedRecipients.Add(unverifiedRecipient);
    }
  }

  /// <summary>
  /// Returns the emails address for the "To" and "BCC" sections.
  /// </summary>
  public virtual void GetsRecipientsFields(
    IEnumerable<NotificationRecipient> notificationRecipientSet,
    ref string emailToAccounts,
    ref string emailBCCAccounts)
  {
    bool flag1 = true;
    bool flag2 = true;
    foreach (NotificationRecipient notificationRecipient in notificationRecipientSet)
    {
      if (notificationRecipient.AddTo != "B")
      {
        if (flag1)
        {
          flag1 = false;
          emailToAccounts = notificationRecipient.Email;
        }
        else
          emailToAccounts = $"{emailToAccounts}; {notificationRecipient.Email}";
      }
      else if (flag2)
      {
        flag2 = false;
        emailBCCAccounts = notificationRecipient.Email;
      }
      else
        emailBCCAccounts = $"{emailBCCAccounts}; {notificationRecipient.Email}";
    }
  }

  public virtual void SendNotification(
    AppointmentEntry graphAppointmentEntry,
    PXCache sourceCache,
    string notificationCD,
    int? branchID,
    IDictionary<string, string> parameters,
    IList<Guid?> attachments = null)
  {
    if (sourceCache.Current == null)
      throw new PXException("Email send failed. Source notification object not defined to proceed operation.");
    if (!branchID.HasValue)
      branchID = ((PXGraph) graphAppointmentEntry).Accessinfo.BranchID;
    (NotificationSetup SetupWithBranch, NotificationSetup SetupWithoutBranch) tuple = new NotificationUtility((PXGraph) graphAppointmentEntry).SearchSetup("Appt", notificationCD, branchID);
    Guid? setupId = (Guid?) (tuple.SetupWithBranch ?? tuple.SetupWithoutBranch)?.SetupID;
    if (!setupId.HasValue)
      throw new PXException("Email send failed. Notification Settings '{0}' not found.", new object[1]
      {
        (object) notificationCD
      });
    if (parameters == null)
    {
      parameters = (IDictionary<string, string>) new Dictionary<string, string>();
      foreach (string key in (IEnumerable<string>) sourceCache.Keys)
      {
        object valueExt = sourceCache.GetValueExt(sourceCache.Current, key);
        parameters[key] = valueExt?.ToString();
      }
    }
    this.Send(graphAppointmentEntry, sourceCache, setupId.Value, branchID, parameters, attachments);
  }

  public virtual void Send(
    AppointmentEntry graphAppointmentEntry,
    PXCache sourceCache,
    Guid setupID,
    int? branchID,
    IDictionary<string, string> reportParams,
    IList<Guid?> attachments = null)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    FSAppointment current = ((PXSelectBase<FSAppointment>) graphAppointmentEntry.AppointmentRecords).Current;
    Guid? noteId = current.NoteID;
    Guid? nullable1 = new Guid?();
    string srvOrdType = current.SrvOrdType;
    string notificationCd = PXResultset<NotificationSetup>.op_Implicit(PXSelectBase<NotificationSetup, PXSelect<NotificationSetup, Where<NotificationSetup.setupID, Equal<Required<NotificationSetup.setupID>>>>.Config>.Select((PXGraph) graphAppointmentEntry, new object[1]
    {
      (object) setupID
    }))?.NotificationCD;
    NotificationSource source = this.GetSource((PXGraph) graphAppointmentEntry, srvOrdType, setupID, branchID);
    int? nullable2 = source != null ? source.EMailAccountID : throw new PXException("Failed to send the email. The [0] notification setting was not found for the [1] service order type.", new object[2]
    {
      (object) notificationCd,
      (object) srvOrdType
    });
    int? sourceEmailID = nullable2 ?? MailAccountManager.DefaultMailAccountID;
    if (!sourceEmailID.HasValue)
      throw new PXException("The email cannot be sent because an email account has not been specified for the {0} mailing of the {1} service order type. Specify the email account on the Service Order Types (FS202300) form or configure the default email account on the {2} form.", new object[3]
      {
        (object) notificationCd,
        (object) srvOrdType,
        (object) "Email Preferences"
      });
    RecipientList recipients = this.GetRecipients(graphAppointmentEntry, source.SourceID, sourceEmailID);
    if (recipients == null || recipients.Count<NotificationRecipient>() == 0)
      throw new PXException("Failed to send the email. The list of email recipients is empty.");
    this.GetsRecipientsFields((IEnumerable<NotificationRecipient>) recipients, ref empty1, ref empty2);
    bool flag = false;
    nullable2 = source.NotificationID;
    if (nullable2.HasValue)
    {
      FSAppointment row = current;
      nullable2 = source.NotificationID;
      int? notificationId = new int?(nullable2.Value);
      TemplateNotificationGenerator notificationGenerator = TemplateNotificationGenerator.Create((object) row, notificationId);
      nullable2 = source.EMailAccountID;
      if (nullable2.HasValue)
        notificationGenerator.MailAccountId = sourceEmailID;
      string body = notificationGenerator.Body;
      FSAppointment.ReplaceWildCards((PXGraph) graphAppointmentEntry, ref body, (object) current);
      notificationGenerator.Body = body;
      notificationGenerator.BodyFormat = source.ReportID != null ? "H" : source.Format;
      notificationGenerator.RefNoteID = noteId;
      notificationGenerator.ParentNoteID = nullable1;
      notificationGenerator.To = empty1;
      notificationGenerator.Bcc = empty2;
      if (source.ReportID != null)
      {
        PX.Reports.Controls.Report report = this.ReportLoader.LoadReport(source.ReportID, (IPXResultset) null);
        if (report == null)
          throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("Report '{0}' cannot be found", new object[1]
          {
            (object) source.ReportID
          }), "reportId");
        this.ReportLoader.InitDefaultReportParameters(report, reportParams);
        report.MailSettings.Format = ReportNotificationGenerator.ConvertFormat(source.Format);
        ReportNode reportNode = this.ReportDataBinder.ProcessReportDataBinding(report);
        reportNode.SendMailMode = true;
        Message message = reportNode.Groups.Select<GroupNode, MailSettings>((Func<GroupNode, MailSettings>) (g => g.MailSettings)).Where<MailSettings>((Func<MailSettings, bool>) (msg => msg != null && msg.ShouldSerialize())).Select<MailSettings, Message>((Func<MailSettings, Message>) (msg => new Message(MailSettings.op_Implicit(msg), reportNode, MailSettings.op_Implicit(msg)))).FirstOrDefault<Message>();
        if (message == null)
          throw new InvalidOperationException(PXMessages.LocalizeFormatNoPrefixNLA("Email cannot be created for the specified report '{0}' because the report has not been generated or the email settings are not specified.", new object[1]
          {
            (object) source.ReportID
          }));
        foreach (ReportStream attachment in message.Attachments)
        {
          if (notificationGenerator.Body == null && notificationGenerator.BodyFormat == "H" && attachment.MimeType == "text/html")
            notificationGenerator.Body = attachment.Encoding.GetString(attachment.GetBytes());
          else
            notificationGenerator.AddAttachment(attachment.Name, attachment.GetBytes(), attachment.CID);
        }
      }
      if (attachments != null)
      {
        foreach (Guid? attachment in (IEnumerable<Guid?>) attachments)
        {
          if (attachment.HasValue)
            notificationGenerator.AddAttachmentLink(attachment.Value);
        }
      }
      flag |= notificationGenerator.Send().Any<CRSMEmail>();
    }
    else if (source.ReportID != null)
    {
      ReportNotificationGenerator notificationGenerator = this.ReportNotificationGeneratorFactory(source.ReportID);
      notificationGenerator.MailAccountId = sourceEmailID;
      notificationGenerator.Format = source.Format;
      notificationGenerator.AdditionalRecipents = (IEnumerable<NotificationRecipient>) recipients;
      notificationGenerator.Parameters = reportParams;
      notificationGenerator.NotificationID = source.NotificationID;
      flag |= notificationGenerator.Send().Any<CRSMEmail>();
    }
    if (!flag)
      throw new PXException("Email send failed. Email isn't created or email recipient list is empty.");
  }

  public virtual bool IsEnableEmailSignedAppointment()
  {
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    if (current != null)
    {
      bool? nullable = current.Hold;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = current.Awaiting;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          return current.CustomerSignedReport.HasValue;
      }
    }
    return false;
  }

  public virtual bool IsItemLineUpdateRequired(
    PXCache cache,
    FSAppointment row,
    FSAppointment oldRow)
  {
    return !cache.ObjectsEqual<FSAppointment.areActualFieldsActive>((object) row, (object) oldRow);
  }

  public virtual void UpdateItemLinesBecauseOfDocStatusChange()
  {
    PXCache cache = ((PXSelectBase) this.AppointmentDetails).Cache;
    foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()))
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      object obj;
      cache.RaiseFieldDefaulting<FSAppointmentDet.areActualFieldsActive>((object) fsAppointmentDet, ref obj);
      bool? nullable = (bool?) obj;
      bool? actualFieldsActive = fsAppointmentDet.AreActualFieldsActive;
      if (!(nullable.GetValueOrDefault() == actualFieldsActive.GetValueOrDefault() & nullable.HasValue == actualFieldsActive.HasValue))
      {
        if (fsAppointmentDet.IsLinkedItem)
        {
          cache.SetValue<FSAppointmentDet.areActualFieldsActive>((object) fsAppointmentDet, obj);
        }
        else
        {
          FSAppointmentDet copy = (FSAppointmentDet) cache.CreateCopy((object) fsAppointmentDet);
          cache.SetValueExt<FSAppointmentDet.areActualFieldsActive>((object) copy, obj);
          cache.Update((object) copy);
        }
      }
    }
  }

  public virtual void RecalculateAreActualFieldsActive(PXCache cache, FSAppointment row)
  {
    if (row == null)
      return;
    object obj;
    cache.RaiseFieldDefaulting<FSAppointment.areActualFieldsActive>((object) row, ref obj);
    bool? nullable = (bool?) obj;
    bool? actualFieldsActive = row.AreActualFieldsActive;
    if (nullable.GetValueOrDefault() == actualFieldsActive.GetValueOrDefault() & nullable.HasValue == actualFieldsActive.HasValue)
      return;
    cache.SetValueExt<FSAppointment.areActualFieldsActive>((object) row, obj);
  }

  public static List<FSAppointmentDet> GetRelatedApptLinesInt(
    PXGraph graph,
    int? soDetID,
    bool excludeSpecificApptLine,
    int? apptDetID,
    bool onlyMarkForPOLines,
    bool sortResult)
  {
    BqlCommand bqlCommand = (BqlCommand) new Select<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>>>>();
    List<object> objectList = new List<object>();
    objectList.Add((object) soDetID);
    if (excludeSpecificApptLine)
    {
      if (!apptDetID.HasValue)
        throw new ArgumentException();
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSAppointmentDet.appDetID, NotEqual<Required<FSAppointmentDet.appDetID>>>));
      objectList.Add((object) apptDetID);
    }
    if (onlyMarkForPOLines)
      bqlCommand = bqlCommand.WhereAnd(typeof (Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.waitingForPO>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>>>));
    if (sortResult)
      bqlCommand = bqlCommand.OrderByNew(typeof (OrderBy<Asc<FSAppointmentDet.tranDate, Asc<FSAppointmentDet.srvOrdType, Asc<FSAppointmentDet.refNbr, Asc<FSAppointmentDet.sortOrder>>>>>));
    return GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) new PXView(graph, false, bqlCommand).SelectMulti(objectList.ToArray())).ToList<FSAppointmentDet>();
  }

  public static Decimal GetCuryDocTotal(
    Decimal? curyLineTotal,
    Decimal? curyLogBillableTranAmountTotal,
    Decimal? curyDiscTotal,
    Decimal? curyTaxTotal,
    Decimal? curyInclTaxTotal)
  {
    return curyLineTotal.GetValueOrDefault() + curyLogBillableTranAmountTotal.GetValueOrDefault() - curyDiscTotal.GetValueOrDefault() + curyTaxTotal.GetValueOrDefault() - curyInclTaxTotal.GetValueOrDefault();
  }

  public static void UpdateCanceledNotPerformed(
    PXCache cache,
    FSAppointmentDet row,
    FSAppointment appointmentRow,
    string oldStatusValue)
  {
    bool flag = appointmentRow != null && appointmentRow.InProcess.GetValueOrDefault() || appointmentRow != null && appointmentRow.Paused.GetValueOrDefault();
    object obj;
    cache.RaiseFieldDefaulting<FSAppointmentDet.isCanceledNotPerformed>((object) row, ref obj);
    bool? nullable = (bool?) obj;
    bool? canceledNotPerformed = row.IsCanceledNotPerformed;
    if (!(nullable.GetValueOrDefault() == canceledNotPerformed.GetValueOrDefault() & nullable.HasValue == canceledNotPerformed.HasValue) || row.Status == "NS" && flag)
      cache.SetValueExt<FSAppointmentDet.isCanceledNotPerformed>((object) row, obj);
    else
      row.IsCanceledNotPerformed = obj == null ? new bool?(false) : (bool?) obj;
  }

  /// <summary>
  /// Gets the corresponding Service Order Detail from the <c>fsAppointmentDetRow.SODetID</c>.
  /// </summary>
  public static FSSODet GetSODetFromAppointmentDetInt(
    PXGraph graph,
    FSAppointmentDet fsAppointmentDetRow)
  {
    FSSODet appointmentDetInt = new FSSODet();
    if (fsAppointmentDetRow != null)
      appointmentDetInt = FSSODet.UK.Find(graph, fsAppointmentDetRow.SODetID);
    return appointmentDetInt;
  }

  public virtual bool ValidateTimeIntegration(PXGraph graph)
  {
    return TimeCardHelper.IsTheTimeCardIntegrationEnabled(graph) && PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>();
  }

  public virtual void VerifyTimeActivityUpdate(
    PXCache cache,
    FSAppointmentLog logRow,
    string fieldName)
  {
    if (!this.ValidateTimeIntegration((PXGraph) this) || !logRow.BAccountID.HasValue || logRow.BAccountType != "EP")
      return;
    PX.Objects.CR.Standalone.EPEmployee tmEmployee = this.FindTMEmployee((PXGraph) this, logRow.BAccountID);
    EPActivityApprove epActivityApprove = this.FindEPActivityApprove((PXGraph) this, logRow, tmEmployee);
    if (epActivityApprove != null && !this.ValidateInsertUpdateTimeActivity(epActivityApprove))
      throw new PXSetPropertyException("{0} cannot be updated because this record is related to a released or approved time activity.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName(cache, fieldName)
      });
  }

  public virtual void InsertUpdateDeleteTimeActivities(
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrderRow,
    FSAppointmentLog fsAppointmentLogRow,
    PXCache cache)
  {
    if (fsAppointmentLogRow.BAccountType != "EP")
      return;
    EmployeeActivitiesEntry employeeActivitiesEntry = (EmployeeActivitiesEntry) null;
    if (((PXSelectBase) this.LogRecords).Cache.GetStatus((object) fsAppointmentLogRow) == 2 || ((PXSelectBase) this.LogRecords).Cache.GetStatus((object) fsAppointmentLogRow) == 1)
    {
      PX.Objects.CR.Standalone.EPEmployee tmEmployee1 = this.FindTMEmployee((PXGraph) this, fsAppointmentLogRow.BAccountID);
      EPActivityApprove epActivityApprove1 = this.FindEPActivityApprove((PXGraph) this, fsAppointmentLogRow, tmEmployee1);
      if (fsAppointmentLogRow.TrackTime.GetValueOrDefault() && fsAppointmentLogRow.Status == "C")
      {
        EmployeeActivitiesEntry graphEmployeeActivitiesEntry = employeeActivitiesEntry ?? this.GetEmployeeActivitiesEntryGraph();
        int? valueOriginal = (int?) cache.GetValueOriginal<FSAppointmentLog.bAccountID>((object) fsAppointmentLogRow);
        int? baccountId = fsAppointmentLogRow.BAccountID;
        int? nullable = valueOriginal;
        if (!(baccountId.GetValueOrDefault() == nullable.GetValueOrDefault() & baccountId.HasValue == nullable.HasValue))
        {
          PX.Objects.CR.Standalone.EPEmployee tmEmployee2 = this.FindTMEmployee((PXGraph) this, valueOriginal);
          EPActivityApprove epActivityApprove2 = this.FindEPActivityApprove((PXGraph) this, fsAppointmentLogRow, tmEmployee2);
          if (epActivityApprove2 != null)
            this.DeleteEPActivityApprove(graphEmployeeActivitiesEntry, epActivityApprove2, tmEmployee2, fsAppointmentLogRow);
        }
        if (!this.ValidateTimeIntegration((PXGraph) this))
          return;
        this.InsertUpdateEPActivityApprove((PXGraph) this, graphEmployeeActivitiesEntry, fsAppointmentLogRow, fsAppointmentRow, fsServiceOrderRow, epActivityApprove1, tmEmployee1);
      }
      else
      {
        if (epActivityApprove1 == null)
          return;
        this.DeleteEPActivityApprove(this.GetEmployeeActivitiesEntryGraph(), epActivityApprove1, tmEmployee1, fsAppointmentLogRow);
      }
    }
    else
    {
      if (((PXSelectBase) this.LogRecords).Cache.GetStatus((object) fsAppointmentLogRow) != 3)
        return;
      this.SearchAndDeleteEPActivity(fsAppointmentLogRow, this.GetEmployeeActivitiesEntryGraph());
    }
  }

  public virtual void InsertUpdateDeleteTimeActivities(
    PXCache appointmentCache,
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrderRow,
    List<FSAppointmentLog> deleteReleatedTimeActivity,
    List<FSAppointmentLog> createReleatedTimeActivity)
  {
    EmployeeActivitiesEntry graphEmployeeActivitiesEntry1 = (EmployeeActivitiesEntry) null;
    if ((string) appointmentCache.GetValueOriginal<FSAppointment.status>((object) fsAppointmentRow) == "C" && fsAppointmentRow.Status == "N")
    {
      graphEmployeeActivitiesEntry1 = graphEmployeeActivitiesEntry1 ?? this.GetEmployeeActivitiesEntryGraph();
      foreach (FSAppointmentLog fsAppointmentLogRow in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (row => row.BAccountType == "EP")))
        this.SearchAndDeleteEPActivity(fsAppointmentLogRow, graphEmployeeActivitiesEntry1);
      foreach (FSAppointmentLog fsAppointmentLogRow in GraphHelper.RowCast<FSAppointmentLog>(((PXSelectBase) this.LogRecords).Cache.Deleted).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (row => row.BAccountType == "EP")))
        this.SearchAndDeleteEPActivity(fsAppointmentLogRow, graphEmployeeActivitiesEntry1);
    }
    if (deleteReleatedTimeActivity != null && deleteReleatedTimeActivity.Count > 0)
    {
      graphEmployeeActivitiesEntry1 = graphEmployeeActivitiesEntry1 ?? this.GetEmployeeActivitiesEntryGraph();
      foreach (FSAppointmentLog fsAppointmentLogRow in deleteReleatedTimeActivity)
      {
        if (fsAppointmentLogRow.BAccountType == "EP")
          this.SearchAndDeleteEPActivity(fsAppointmentLogRow, graphEmployeeActivitiesEntry1);
      }
    }
    if (!this.ValidateTimeIntegration((PXGraph) this) || createReleatedTimeActivity == null || createReleatedTimeActivity.Count <= 0)
      return;
    EmployeeActivitiesEntry graphEmployeeActivitiesEntry2 = graphEmployeeActivitiesEntry1 ?? this.GetEmployeeActivitiesEntryGraph();
    foreach (FSAppointmentLog fsAppointmentLogRow in createReleatedTimeActivity)
    {
      if (fsAppointmentLogRow.BAccountType == "EP")
      {
        PX.Objects.CR.Standalone.EPEmployee tmEmployee = this.FindTMEmployee((PXGraph) this, fsAppointmentLogRow.BAccountID);
        this.InsertUpdateEPActivityApprove((PXGraph) this, graphEmployeeActivitiesEntry2, fsAppointmentLogRow, fsAppointmentRow, fsServiceOrderRow, (EPActivityApprove) null, tmEmployee);
      }
    }
  }

  public virtual void SearchAndDeleteEPActivity(
    FSAppointmentLog fsAppointmentLogRow,
    EmployeeActivitiesEntry graphEmployeeActivitiesEntry)
  {
    PX.Objects.CR.Standalone.EPEmployee tmEmployee = this.FindTMEmployee((PXGraph) this, fsAppointmentLogRow.BAccountID);
    EPActivityApprove epActivityApprove = this.FindEPActivityApprove((PXGraph) this, fsAppointmentLogRow, tmEmployee);
    if (epActivityApprove == null)
      return;
    this.DeleteEPActivityApprove(graphEmployeeActivitiesEntry, epActivityApprove, tmEmployee, fsAppointmentLogRow);
  }

  public virtual void DeleteEPActivityApprove(
    EmployeeActivitiesEntry graphEmployeeActivitiesEntry,
    EPActivityApprove epActivityApproveRow,
    PX.Objects.CR.Standalone.EPEmployee epEmployeeRow)
  {
    if (epActivityApproveRow == null)
      return;
    if (!this.ValidateInsertUpdateTimeActivity(epActivityApproveRow))
      throw new PXSetPropertyException("The log line cannot be deleted because at least one time activity associated with {0} log line has been released or approved.", new object[2]
      {
        (object) epEmployeeRow?.AcctCD,
        (object) (PXErrorLevel) 4
      });
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).Delete(epActivityApproveRow);
    ((PXAction) graphEmployeeActivitiesEntry.Save).Press();
  }

  public virtual void DeleteEPActivityApprove(
    EmployeeActivitiesEntry graphEmployeeActivitiesEntry,
    EPActivityApprove epActivityApproveRow,
    PX.Objects.CR.Standalone.EPEmployee epEmployeeRow,
    FSAppointmentLog fsAppointmentLogRow)
  {
    if (epActivityApproveRow == null)
      return;
    if (!this.ValidateInsertUpdateTimeActivity(epActivityApproveRow))
      throw new PXSetPropertyException((IBqlTable) fsAppointmentLogRow, "The log line cannot be deleted because at least one time activity associated with {0} and the {1} log line has been released or approved.", new object[3]
      {
        (object) epEmployeeRow?.AcctCD,
        (object) fsAppointmentLogRow?.LineRef,
        (object) (PXErrorLevel) 4
      });
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).Delete(epActivityApproveRow);
    ((PXAction) graphEmployeeActivitiesEntry.Save).Press();
  }

  public virtual PX.Objects.CR.Standalone.EPEmployee FindTMEmployee(PXGraph graph, int? employeeID)
  {
    return PXResultset<PX.Objects.CR.Standalone.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.EPEmployee, PXSelect<PX.Objects.CR.Standalone.EPEmployee, Where<PX.Objects.CR.Standalone.EPEmployee.bAccountID, Equal<Required<PX.Objects.CR.Standalone.EPEmployee.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) employeeID
    })) ?? throw new Exception("The time activities cannot be updated because at least one staff member is not associated with a user in the Linked Entity box on the Users (SM201010) form.");
  }

  public virtual EPActivityApprove FindEPActivityApprove(
    PXGraph graph,
    FSAppointmentLog fsAppointmentLogRow,
    PX.Objects.CR.Standalone.EPEmployee epEmployeeRow)
  {
    if (fsAppointmentLogRow == null || epEmployeeRow == null)
      return (EPActivityApprove) null;
    return PXResultset<EPActivityApprove>.op_Implicit(PXSelectBase<EPActivityApprove, PXSelect<EPActivityApprove, Where<EPActivityApprove.ownerID, Equal<Required<EPActivityApprove.ownerID>>, And<FSxPMTimeActivity.appointmentID, Equal<Required<FSxPMTimeActivity.appointmentID>>, And<FSxPMTimeActivity.logLineNbr, Equal<Required<FSxPMTimeActivity.logLineNbr>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) epEmployeeRow.DefContactID,
      (object) fsAppointmentLogRow.DocID,
      (object) fsAppointmentLogRow.LineNbr
    }));
  }

  public virtual bool ValidateInsertUpdateTimeActivity(EPActivityApprove epActivityApproveRow)
  {
    if (epActivityApproveRow == null)
      return true;
    if (epActivityApproveRow.ApprovalStatus != "AP" && epActivityApproveRow.ApprovalStatus != "RL")
    {
      bool? released = epActivityApproveRow.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
        return epActivityApproveRow.TimeCardCD == null;
    }
    return false;
  }

  public virtual void InsertUpdateEPActivityApprove(
    PXGraph graph,
    EmployeeActivitiesEntry graphEmployeeActivitiesEntry,
    FSAppointmentLog fsAppointmentLogRow,
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrderRow,
    EPActivityApprove epActivityApproveRow,
    PX.Objects.CR.Standalone.EPEmployee epEmployeeRow)
  {
    if (!this.ValidateInsertUpdateTimeActivity(epActivityApproveRow))
      return;
    FSAppointmentDet fsAppointmentDetRow = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.lineRef, Equal<Required<FSAppointmentDet.lineRef>>, And<FSAppointmentDet.appointmentID, Equal<Required<FSAppointmentDet.appointmentID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) fsAppointmentLogRow.DetLineRef,
      (object) fsAppointmentRow.AppointmentID
    }));
    if (fsAppointmentDetRow != null && fsAppointmentDetRow.IsCanceledNotPerformed.GetValueOrDefault())
      return;
    SM_EmployeeActivitiesEntry extension1 = ((PXGraph) graphEmployeeActivitiesEntry).GetExtension<SM_EmployeeActivitiesEntry>();
    if (extension1 != null)
      extension1.GraphAppointmentEntryCaller = this;
    if (epActivityApproveRow == null)
    {
      epActivityApproveRow = new EPActivityApprove();
      epActivityApproveRow.OwnerID = epEmployeeRow.DefContactID;
      epActivityApproveRow = ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).Insert(epActivityApproveRow);
    }
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).SetValueExt<EPActivityApprove.hold>(epActivityApproveRow, (object) false);
    epActivityApproveRow.WorkgroupID = fsAppointmentLogRow.WorkgroupID;
    epActivityApproveRow.Date = fsAppointmentLogRow.DateTimeBegin;
    epActivityApproveRow.EarningTypeID = fsAppointmentLogRow.EarningType;
    epActivityApproveRow.TimeSpent = fsAppointmentLogRow.TimeDuration;
    epActivityApproveRow.Summary = this.GetDescriptionToUseInEPActivityApprove(fsAppointmentRow, fsAppointmentLogRow, fsAppointmentDetRow);
    epActivityApproveRow.CostCodeID = (int?) fsAppointmentLogRow?.CostCodeID;
    FSxPMTimeActivity extension2 = PXCache<PMTimeActivity>.GetExtension<FSxPMTimeActivity>((PMTimeActivity) epActivityApproveRow);
    extension2.AppointmentID = fsAppointmentRow.AppointmentID;
    extension2.AppointmentCustomerID = fsServiceOrderRow.CustomerID;
    extension2.LogLineNbr = fsAppointmentLogRow.LineNbr;
    extension2.ServiceID = fsAppointmentLogRow.DetLineRef != null ? (int?) fsAppointmentDetRow?.InventoryID : new int?();
    epActivityApproveRow = ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).Update(epActivityApproveRow);
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).SetValueExt<EPActivityApprove.projectID>(epActivityApproveRow, (object) fsServiceOrderRow.ProjectID);
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).SetValueExt<EPActivityApprove.projectTaskID>(epActivityApproveRow, (object) fsAppointmentLogRow.ProjectTaskID);
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).SetValueExt<PMTimeActivity.isBillable>(epActivityApproveRow, (object) fsAppointmentLogRow.IsBillable);
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).SetValueExt<EPActivityApprove.timeBillable>(epActivityApproveRow, (object) fsAppointmentLogRow.BillableTimeDuration);
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).SetValueExt<EPActivityApprove.approvalStatus>(epActivityApproveRow, (object) this.GetStatusToUseInEPActivityApprove());
    ((PXSelectBase<EPActivityApprove>) graphEmployeeActivitiesEntry.Activity).SetValueExt<PMTimeActivity.labourItemID>(epActivityApproveRow, (object) fsAppointmentLogRow.LaborItemID);
    ((PXAction) graphEmployeeActivitiesEntry.Save).Press();
  }

  public virtual string GetDescriptionToUseInEPActivityApprove(
    FSAppointment fsAppointmentRow,
    FSAppointmentLog fsAppointmentLogRow,
    FSAppointmentDet fsAppointmentDetRow)
  {
    if (fsAppointmentLogRow != null)
    {
      if (fsAppointmentLogRow.ItemType == "TR")
        return fsAppointmentLogRow.Descr;
      if (fsAppointmentLogRow.DetLineRef != null && fsAppointmentDetRow != null)
        return fsAppointmentDetRow.TranDesc;
    }
    return fsAppointmentRow.DocDesc;
  }

  public virtual string GetStatusToUseInEPActivityApprove() => "CD";

  public virtual int? GetPreferedSiteID()
  {
    int? preferedSiteId = new int?();
    FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<FSAppointmentDet.siteID>>>, Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointment.refNbr>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (fsAppointmentDet != null)
      preferedSiteId = fsAppointmentDet.SiteID;
    return preferedSiteId;
  }

  public virtual DateTime? GetShipDate(FSServiceOrder serviceOrder, FSAppointment appointment)
  {
    return AppointmentEntry.GetShipDateInt((PXGraph) this, serviceOrder, appointment);
  }

  public virtual bool ShouldUpdateAppointmentLogBillableFieldsFromTimeCard()
  {
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    return current != null && current.PostTo == "PM" && current.BillingType == "CC" && current.CreateTimeActivitiesFromAppointment.GetValueOrDefault();
  }

  public static DateTime? GetShipDateInt(
    PXGraph graph,
    FSServiceOrder serviceOrder,
    FSAppointment appointment)
  {
    DateTime? nullable1 = new DateTime?();
    DateTime? nullable2;
    if (appointment != null)
    {
      nullable2 = appointment.ActualDateTimeBegin;
      ref DateTime? local = ref nullable2;
      nullable1 = local.HasValue ? new DateTime?(local.GetValueOrDefault().Date) : new DateTime?();
    }
    else if (serviceOrder != null)
      nullable1 = serviceOrder.OrderDate;
    nullable2 = graph.Accessinfo.BusinessDate;
    DateTime? nullable3 = nullable1;
    return (nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0 ? nullable1 : graph.Accessinfo.BusinessDate;
  }

  public static DateTime? GetDateTimeEndInt(
    DateTime? dateTimeBegin,
    int hour = 0,
    int minute = 0,
    int second = 0,
    int milisecond = 0)
  {
    return dateTimeBegin.HasValue ? new DateTime?(new DateTime(dateTimeBegin.Value.Year, dateTimeBegin.Value.Month, dateTimeBegin.Value.Day, hour, minute, second, milisecond)) : new DateTime?();
  }

  public static AppointmentEntry.SlotIsContained SlotIsContainedInSlotInt(
    DateTime? slotBegin,
    DateTime? slotEnd,
    DateTime? beginTime,
    DateTime? endTime)
  {
    DateTime? nullable1 = beginTime;
    DateTime? nullable2 = slotBegin;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      DateTime? nullable3 = endTime;
      DateTime? nullable4 = slotEnd;
      if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return AppointmentEntry.SlotIsContained.ExceedsContainment;
    }
    DateTime? nullable5 = beginTime;
    DateTime? nullable6 = slotBegin;
    if ((nullable5.HasValue & nullable6.HasValue ? (nullable5.GetValueOrDefault() >= nullable6.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      DateTime? nullable7 = endTime;
      DateTime? nullable8 = slotEnd;
      if ((nullable7.HasValue & nullable8.HasValue ? (nullable7.GetValueOrDefault() <= nullable8.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return AppointmentEntry.SlotIsContained.Contained;
    }
    DateTime? nullable9 = beginTime;
    DateTime? nullable10 = slotBegin;
    if ((nullable9.HasValue & nullable10.HasValue ? (nullable9.GetValueOrDefault() < nullable10.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      DateTime? nullable11 = endTime;
      DateTime? nullable12 = slotBegin;
      if ((nullable11.HasValue & nullable12.HasValue ? (nullable11.GetValueOrDefault() > nullable12.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        goto label_10;
    }
    DateTime? nullable13 = beginTime;
    DateTime? nullable14 = slotEnd;
    if ((nullable13.HasValue & nullable14.HasValue ? (nullable13.GetValueOrDefault() < nullable14.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      DateTime? nullable15 = endTime;
      DateTime? nullable16 = slotEnd;
      if ((nullable15.HasValue & nullable16.HasValue ? (nullable15.GetValueOrDefault() > nullable16.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        goto label_10;
    }
    return AppointmentEntry.SlotIsContained.NotContained;
label_10:
    return AppointmentEntry.SlotIsContained.PartiallyContained;
  }

  public static void ValidateSrvOrdTypeNumberingSequenceInt(PXGraph graph, string srvOrdType)
  {
    FSSrvOrdType fsSrvOrdType = PXResultset<FSSrvOrdType>.op_Implicit(PXSelectBase<FSSrvOrdType, PXSelect<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>>>.Config>.Select(graph, new object[1]
    {
      (object) srvOrdType
    }));
    Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsSrvOrdType?.SrvOrdNumberingID
    }));
    if (numbering == null)
      throw new PXSetPropertyException("Numbering ID is null.");
    if (numbering.UserNumbering.GetValueOrDefault())
      throw new PXSetPropertyException("The appointment cannot be saved because a manual numbering sequence is assigned to the service order type {0} and the service order cannot be created automatically for the appointment. Create the service order on the Service Orders (FS300100) form first or modify the numbering sequence of the service order type on the Service Order Types (FS202300) form.", new object[1]
      {
        (object) srvOrdType
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<FSServiceOrder, FSServiceOrder.billingBy> e)
  {
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<FSServiceOrder, FSServiceOrder.billingBy>>) e).ReturnValue = (object) this.GetBillingMode(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchID> e)
  {
    if (e.Row == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    ((PXSelectBase) this.AppointmentSelected).Cache.SetValueExt<FSAppointment.branchID>((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current, (object) e.Row.BranchID);
    this.UpdateDetailsFromBranchID(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchLocationID> e)
  {
    if (e.Row == null)
      return;
    this.FSServiceOrder_BranchLocationID_FieldUpdated_Handler((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.branchLocationID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, (PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID> e)
  {
    if (e.Row == null)
      return;
    this.FSServiceOrder_LocationID_FieldUpdated_Handler(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.locationID>>) e).Args);
    FSServiceOrder row = e.Row;
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    this.SetCurrentAppointmentSalesPersonID(row);
    ((PXSelectBase) this.AppointmentRecords).Cache.SetDefaultExt<FSAppointment.externalTaxExemptionNumber>((object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
    ((PXSelectBase) this.AppointmentRecords).Cache.SetDefaultExt<FSAppointment.entityUsageType>((object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
    foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()))
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      bool? manualPrice = fsAppointmentDet.ManualPrice;
      bool flag = false;
      if (manualPrice.GetValueOrDefault() == flag & manualPrice.HasValue)
      {
        ((PXSelectBase) this.AppointmentDetails).Cache.SetDefaultExt<FSAppointmentDet.curyUnitPrice>((object) fsAppointmentDet);
        ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(fsAppointmentDet);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.contactID> e)
  {
    this.FSServiceOrder_ContactID_FieldUpdated_Handler((PXGraph) this, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.contactID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID> e)
  {
    if (e.Row == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    FSServiceOrder row = e.Row;
    try
    {
      if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>>) e).ExternalCall)
        this.recalculateCuryID = true;
      ((PXSelectBase) this.AppointmentSelected).Cache.SetValueExt<FSAppointment.billCustomerID>((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current, (object) row.BillCustomerID);
    }
    finally
    {
      this.recalculateCuryID = false;
    }
    this.FSServiceOrder_BillCustomerID_FieldUpdated_Handler(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>>) e).Args);
    this.ValidateCustomerBillingCycle(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billCustomerID>>) e).Cache, e.Row, ((PXSelectBase) this.AppointmentRecords).Cache, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, e.Row.BillCustomerID, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, true);
    if (!this.SkipChangingContract)
    {
      ((PXSelectBase) this.AppointmentSelected).Cache.SetDefaultExt<FSAppointment.billServiceContractID>((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current);
      ((PXSelectBase) this.AppointmentSelected).Cache.SetDefaultExt<FSAppointment.billContractPeriodID>((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current);
    }
    this.SkipChangingContract = false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceOrder, FSServiceOrder.billLocationID> e)
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    ((PXSelectBase) this.AppointmentSelected).Cache.SetDefaultExt<FSAppointment.taxZoneID>((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current);
    ((PXSelectBase) this.AppointmentSelected).Cache.SetDefaultExt<FSAppointment.taxCalcMode>((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSServiceOrder> e)
  {
    if (e.Row == null || this.SkipServiceOrderUpdate || this.persistContext)
      return;
    FSServiceOrder row = e.Row;
    using (new PXConnectionScope())
    {
      this.UpdateServiceOrderUnboundFields(row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, this.DisableServiceOrderUnboundFieldCalc);
      PXResultset<FSAppointment> pxResultset = PXSelectBase<FSAppointment, PXSelectReadonly<FSAppointment, Where2<Where<FSAppointment.closed, Equal<True>, Or<FSAppointment.completed, Equal<True>>>, And<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SOID
      });
      FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
      row.AppointmentsCompletedOrClosedCntr = new int?(0);
      row.AppointmentsCompletedCntr = new int?(0);
      foreach (PXResult<FSAppointment> pxResult in pxResultset)
      {
        FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
        int? nullable1;
        if (current != null)
        {
          if (current != null)
          {
            int? appointmentId = current.AppointmentID;
            nullable1 = fsAppointment.AppointmentID;
            if (appointmentId.GetValueOrDefault() == nullable1.GetValueOrDefault() & appointmentId.HasValue == nullable1.HasValue)
              continue;
          }
          else
            continue;
        }
        FSServiceOrder fsServiceOrder1 = row;
        nullable1 = fsServiceOrder1.AppointmentsCompletedOrClosedCntr;
        fsServiceOrder1.AppointmentsCompletedOrClosedCntr = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
        bool? nullable2 = fsAppointment.Completed;
        if (nullable2.GetValueOrDefault())
        {
          nullable2 = fsAppointment.Closed;
          bool flag = false;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          {
            FSServiceOrder fsServiceOrder2 = row;
            nullable1 = fsServiceOrder2.AppointmentsCompletedCntr;
            fsServiceOrder2.AppointmentsCompletedCntr = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
          }
        }
      }
      if (current == null)
        return;
      bool? closed;
      if (current.Completed.GetValueOrDefault())
      {
        closed = current.Closed;
        bool flag = false;
        if (closed.GetValueOrDefault() == flag & closed.HasValue)
        {
          FSServiceOrder fsServiceOrder3 = row;
          int? completedOrClosedCntr = fsServiceOrder3.AppointmentsCompletedOrClosedCntr;
          fsServiceOrder3.AppointmentsCompletedOrClosedCntr = completedOrClosedCntr.HasValue ? new int?(completedOrClosedCntr.GetValueOrDefault() + 1) : new int?();
          FSServiceOrder fsServiceOrder4 = row;
          int? appointmentsCompletedCntr = fsServiceOrder4.AppointmentsCompletedCntr;
          fsServiceOrder4.AppointmentsCompletedCntr = appointmentsCompletedCntr.HasValue ? new int?(appointmentsCompletedCntr.GetValueOrDefault() + 1) : new int?();
          return;
        }
      }
      closed = current.Closed;
      if (!closed.GetValueOrDefault())
        return;
      FSServiceOrder fsServiceOrder = row;
      int? completedOrClosedCntr1 = fsServiceOrder.AppointmentsCompletedOrClosedCntr;
      fsServiceOrder.AppointmentsCompletedOrClosedCntr = completedOrClosedCntr1.HasValue ? new int?(completedOrClosedCntr1.GetValueOrDefault() + 1) : new int?();
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrder>>) e).Cache;
    if (string.IsNullOrEmpty(row.SrvOrdType))
      return;
    if (((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject)?.Current == null)
      ((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Current = PXResultset<PX.Objects.CT.Contract>.op_Implicit(((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Select(new object[1]
      {
        (object) row.ProjectID
      }));
    int count = PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>.Config>.SelectWindowed(cache.Graph, 0, 2, new object[1]
    {
      (object) row.SOID
    }).Count;
    this.FSServiceOrder_RowSelected_PartialHandler(cache.Graph, cache, row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Current, count, ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()).Count, (PXCache) null, (PXCache) null, (PXCache) null, (PXCache) null, (PXCache) null, (PXCache) null, new bool?());
    if (((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrder>>) e).Cache.GetEnabled<FSServiceOrder.locationID>((object) e.Row) && this.HaveAnyBilledAppointmentsInServiceOrder((PXGraph) this, e.Row.SOID))
      PXUIFieldAttribute.SetEnabled<FSServiceOrder.locationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrder>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.billCustomerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceOrder>>) e).Cache, (object) e.Row, false);
    ((PXSelectBase) this.AppointmentDetails).Cache.ClearQueryCache();
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSServiceOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSServiceOrder> e)
  {
    if (e.Row == null)
      return;
    SharedFunctions.InitializeNote(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSServiceOrder>>) e).Cache, ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSServiceOrder>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSServiceOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSServiceOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSServiceOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSServiceOrder> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSServiceOrder> e)
  {
    if ((e.Operation & 3) != 2)
      return;
    FSServiceOrder row = e.Row;
    if (string.IsNullOrWhiteSpace(row.SrvOrdType))
      GraphHelper.RaiseRowPersistingException<FSAppointment.srvOrdType>(((PXSelectBase) this.AppointmentRecords).Cache, (object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
    row.CustomerID = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.CustomerID;
    row.BillServiceContractID = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.BillServiceContractID;
    row.CuryID = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.CuryID;
    row.TaxZoneID = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.TaxZoneID;
    row.TaxCalcMode = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.TaxCalcMode;
    row.ProjectID = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ProjectID;
    row.DfltProjectTaskID = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.DfltProjectTaskID;
    row.DocDesc = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.DocDesc;
    row.OrderDate = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ScheduledDateTimeBegin;
    row.Commissionable = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.Commissionable;
    row.WFStageID = new int?();
    this.ValidateSrvOrdTypeNumberingSequence((PXGraph) this, row.SrvOrdType);
    this.insertingServiceOrder = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSServiceOrder> e)
  {
    if (e.TranStatus != 2)
      return;
    this.serviceOrderRowPersistedPassedWithStatusAbort = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.billContractPeriodID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.billContractPeriodID>, FSAppointment, object>) e).NewValue != null)
      return;
    FSAppointment row = e.Row;
    object[] objArray = new object[2];
    DateTime? nullable1 = row.ScheduledDateTimeBegin;
    ref DateTime? local1 = ref nullable1;
    DateTime valueOrDefault;
    DateTime? nullable2;
    if (!local1.HasValue)
    {
      nullable2 = new DateTime?();
    }
    else
    {
      valueOrDefault = local1.GetValueOrDefault();
      nullable2 = new DateTime?(valueOrDefault.Date);
    }
    objArray[0] = (object) nullable2;
    nullable1 = row.ScheduledDateTimeEnd;
    ref DateTime? local2 = ref nullable1;
    DateTime? nullable3;
    if (!local2.HasValue)
    {
      nullable3 = new DateTime?();
    }
    else
    {
      valueOrDefault = local2.GetValueOrDefault();
      nullable3 = new DateTime?(valueOrDefault.Date);
    }
    objArray[1] = (object) nullable3;
    FSContractPeriod fsContractPeriod = PXResultset<FSContractPeriod>.op_Implicit(PXSelectBase<FSContractPeriod, PXSelectJoin<FSContractPeriod, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSContractPeriod.serviceContractID>>>, Where<FSContractPeriod.startPeriodDate, LessEqual<Required<FSContractPeriod.startPeriodDate>>, And<FSContractPeriod.endPeriodDate, GreaterEqual<Required<FSContractPeriod.startPeriodDate>>, And<FSContractPeriod.serviceContractID, Equal<Current<FSAppointment.billServiceContractID>>, And2<Where2<Where<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>, Or<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Pending>>>, Or<Where<FSServiceContract.isFixedRateContract, Equal<True>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Invoiced>>>>>, And<Current<FSBillingCycle.billingBy>, Equal<ListField_Billing_By.Appointment>>>>>>>.Config>.Select((PXGraph) this, objArray));
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.billContractPeriodID>, FSAppointment, object>) e).NewValue = (object) (int?) fsContractPeriod?.ContractPeriodID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.scheduledDateTimeBegin> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.scheduledDateTimeBegin>, FSAppointment, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.scheduledDateTimeEnd> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.scheduledDateTimeEnd>, FSAppointment, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void FSAppointment_ActualDateTimeBegin_Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    FSAppointment row = (FSAppointment) e.Row;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    if (!handlingDateTime.HasValue)
      return;
    e.NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(row.ExecutionDate, handlingDateTime);
    cache.SetValuePending(e.Row, typeof (FSAppointment.actualDateTimeBegin).Name + "headerNewTime", e.NewValue);
  }

  protected virtual void FSAppointment_ActualDateTimeEnd_Time_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    FSAppointment row = (FSAppointment) e.Row;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(cache, e.NewValue);
    if (!handlingDateTime.HasValue)
      return;
    DateTime? nullable = row.ActualDateTimeEnd.HasValue ? PXDBDateAndTimeAttribute.CombineDateTime(row.ActualDateTimeEnd, handlingDateTime) : PXDBDateAndTimeAttribute.CombineDateTime(row.ActualDateTimeBegin, handlingDateTime);
    e.NewValue = (object) nullable;
    cache.SetValuePending(e.Row, typeof (FSAppointment.actualDateTimeEnd).Name + "newTime", (object) nullable);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSAppointment, FSAppointment.executionDate> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointment, FSAppointment.executionDate>>) e).NewValue != null)
      return;
    FSAppointment row = e.Row;
    DateTime? scheduledDateTimeBegin = row.ScheduledDateTimeBegin;
    if (!scheduledDateTimeBegin.HasValue)
      return;
    PX.Data.Events.FieldUpdating<FSAppointment, FSAppointment.executionDate> fieldUpdating = e;
    scheduledDateTimeBegin = row.ScheduledDateTimeBegin;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> date = (ValueType) scheduledDateTimeBegin.Value.Date;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointment, FSAppointment.executionDate>>) fieldUpdating).NewValue = (object) date;
  }

  protected virtual void FSAppointment_NoteFiles_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = (FSAppointment) e.Row;
    if (!(e.NewValue is Guid[]) || ((Guid[]) e.NewValue).Length == 0)
      return;
    Guid? customerSignedReport = row.CustomerSignedReport;
    if (customerSignedReport.HasValue)
    {
      customerSignedReport = row.CustomerSignedReport;
      Guid empty = Guid.Empty;
      if ((customerSignedReport.HasValue ? (customerSignedReport.GetValueOrDefault() == empty ? 1 : 0) : 0) == 0)
        return;
    }
    int? appointmentId = row.AppointmentID;
    int num = 0;
    if (!(appointmentId.GetValueOrDefault() > num & appointmentId.HasValue))
      return;
    this.GenerateSignedReport(cache, row);
  }

  protected virtual void FSAppointment_ScheduledDateTimeBegin_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = (FSAppointment) e.Row;
    DateTime? oldValue = (DateTime?) e.OldValue;
    DateTime? nullable = oldValue;
    DateTime? scheduledDateTimeBegin = row.ScheduledDateTimeBegin;
    if ((nullable.HasValue == scheduledDateTimeBegin.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != scheduledDateTimeBegin.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    this.UncheckUnreachedCustomerByScheduledDate(oldValue, row.ScheduledDateTimeBegin, row);
    if (oldValue.HasValue)
    {
      scheduledDateTimeBegin = row.ScheduledDateTimeBegin;
      if (scheduledDateTimeBegin.HasValue)
      {
        DateTime dateTime = oldValue.Value;
        DateTime date1 = dateTime.Date;
        scheduledDateTimeBegin = row.ScheduledDateTimeBegin;
        dateTime = scheduledDateTimeBegin.Value;
        DateTime date2 = dateTime.Date;
        if (!(date1 != date2))
          return;
      }
    }
    cache.SetDefaultExt<FSAppointment.executionDate>(e.Row);
    cache.SetDefaultExt<FSAppointment.billContractPeriodID>(e.Row);
    this.RefreshSalesPricesInTheWholeDocument((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.soRefNbr> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    int? nullable1;
    if (string.IsNullOrEmpty(row.SORefNbr))
    {
      nullable1 = row.SOID;
      if (nullable1.HasValue)
      {
        nullable1 = row.SOID;
        int num = 0;
        if (!(nullable1.GetValueOrDefault() >= num & nullable1.HasValue))
          goto label_7;
      }
      FSAppointment fsAppointment = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      fsAppointment.SOID = nullable2;
      this.InitServiceOrderRelated(row);
    }
    else
    {
      this.DeleteUnpersistedServiceOrderRelated(row);
      this.SetServiceOrderRelatedBySORefNbr(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.soRefNbr>>) e).Cache, row);
    }
label_7:
    IEnumerable inserted1 = ((PXSelectBase) this.ServiceOrder_Address).Cache.Inserted;
    FSAddress fsAddress = inserted1 != null ? GraphHelper.RowCast<FSAddress>(inserted1).FirstOrDefault<FSAddress>() : (FSAddress) null;
    IEnumerable inserted2 = ((PXSelectBase) this.ServiceOrder_Contact).Cache.Inserted;
    FSContact fsContact = inserted2 != null ? GraphHelper.RowCast<FSContact>(inserted2).FirstOrDefault<FSContact>() : (FSContact) null;
    if (fsAddress != null)
    {
      nullable1 = fsAddress.AddressID;
      int num = 0;
      if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
        ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Delete(fsAddress);
    }
    if (fsContact != null)
    {
      nullable1 = fsContact.ContactID;
      int num = 0;
      if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
        ((PXSelectBase<FSContact>) this.ServiceOrder_Contact).Delete(fsContact);
    }
    if (!this.IsCloningAppointment && !this.IsGeneratingAppointment && row.SORefNbr != null)
    {
      ((PXSelectBase<FSSelectorHelper>) this.Helper).Current = this.GetFsSelectorHelperInstance;
      PXResultset<FSSODet> bqlResultSet_FSSODet = new PXResultset<FSSODet>();
      this.GetPendingLines((PXGraph) this, row.SOID, ref bqlResultSet_FSSODet);
      this.InsertServiceOrderDetailsInAppointment(bqlResultSet_FSSODet, ((PXSelectBase) this.AppointmentDetails).Cache);
      this.GetEmployeesFromServiceOrder(((PXSelectBase) this.AppointmentDetails).Cache, row);
      this.GetResourcesFromServiceOrder(row);
      this.Answers.Current = PXResultset<CSAnswers>.op_Implicit(this.Answers.Select(Array.Empty<object>()));
      this.Answers.CopyAllAttributes((object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
      PXCache cache = ((PXSelectBase) this.AppointmentRecords).Cache;
      FSAppointment current1 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
      FSServiceOrder current2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
      int? nullable3;
      if (current2 == null)
      {
        nullable1 = new int?();
        nullable3 = nullable1;
      }
      else
        nullable3 = current2.BillServiceContractID;
      // ISSUE: variable of a boxed type
      __Boxed<int?> newValue = (ValueType) nullable3;
      cache.SetValueExtIfDifferent<FSAppointment.billServiceContractID>((object) current1, (object) newValue);
      this.UpdateDetailsFromProjectTaskID(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
    }
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if ((current != null ? (current.CopyNotesToAppoinment.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.soRefNbr>>) e).Cache.SetValueExt<FSAppointment.longDescr>((object) row, (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current?.LongDescr);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.executionDate> e)
  {
    FSAppointment row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.executionDate>>) e).Cache;
    DateTime? oldValue = (DateTime?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.executionDate>, FSAppointment, object>) e).OldValue;
    DateTime? nullable = row.ExecutionDate;
    if ((oldValue.HasValue == nullable.HasValue ? (oldValue.HasValue ? (oldValue.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    nullable = row.ActualDateTimeBegin;
    if (nullable.HasValue)
    {
      DateTime? newValue = PXDBDateAndTimeAttribute.CombineDateTime(row.ExecutionDate, row.ActualDateTimeBegin);
      cache.SetValueExtIfDifferent<FSAppointment.actualDateTimeBegin>((object) e.Row, (object) newValue);
    }
    foreach (FSAppointmentDet fsSODetRow in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => !x.IsPickupDelivery)))
    {
      this.UpdateWarrantyFlag(cache, (IFSSODetBase) fsSODetRow, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate);
      ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(fsSODetRow);
    }
    this.CalculateLaborCosts();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.routeDocumentID> e)
  {
    if (e.Row == null)
      return;
    e.Row.Mem_LastRouteDocumentID = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.routeDocumentID>, FSAppointment, object>) e).OldValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.handleManuallyScheduleTime> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    this.CalculateEndTimeWithLinesDuration(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.handleManuallyScheduleTime>>) e).Cache, row, AppointmentEntry.DateFieldType.ScheduleField);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.handleManuallyActualTime> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>> orderTypeSelected = this.ServiceOrderTypeSelected;
    bool? nullable;
    int num;
    if (orderTypeSelected == null)
    {
      num = 0;
    }
    else
    {
      nullable = ((PXSelectBase<FSSrvOrdType>) orderTypeSelected).Current.SetTimeInHeaderBasedOnLog;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0)
      return;
    nullable = row.HandleManuallyActualTime;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.handleManuallyActualTime>>) e).Cache.SetValueExt<FSAppointment.actualDateTimeEnd>((object) row, (object) row.MaxLogTimeEnd);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.actualDateTimeBegin> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.actualDateTimeBegin>>) e).Cache;
    object valuePending = cache.GetValuePending((object) e.Row, typeof (FSAppointment.actualDateTimeBegin).Name + "headerNewTime");
    if (PXCache.NotSetValue != valuePending)
    {
      DateTime? nullable = (DateTime?) valuePending;
      if (nullable.HasValue)
        row.ActualDateTimeBegin = nullable;
    }
    this.OnApptStartTimeChangeUpdateLogStartTime(row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, this.LogRecords);
    cache.SetDefaultExt<FSAppointment.actualDuration>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.actualDateTimeEnd> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.actualDateTimeEnd>>) e).Cache;
    object valuePending = cache.GetValuePending((object) e.Row, typeof (FSAppointment.actualDateTimeEnd).Name + "newTime");
    if (PXCache.NotSetValue != valuePending)
    {
      DateTime? nullable = (DateTime?) valuePending;
      if (nullable.HasValue)
        row.ActualDateTimeEnd = nullable;
    }
    if (!this.SkipManualTimeFlagUpdate)
      this.OnApptEndTimeChangeUpdateLogEndTime(row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, this.LogRecords);
    cache.SetDefaultExt<FSAppointment.actualDuration>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.confirmed> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    bool? nullable = row.Confirmed;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.UnreachedCustomer;
    if (!nullable.GetValueOrDefault())
      return;
    row.UnreachedCustomer = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.unreachedCustomer> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    bool? nullable = row.UnreachedCustomer;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.Confirmed;
    if (!nullable.GetValueOrDefault())
      return;
    row.Confirmed = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.scheduledDateTimeEnd> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    this.UncheckUnreachedCustomerByScheduledDate((DateTime?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.scheduledDateTimeEnd>, FSAppointment, object>) e).OldValue, row.ScheduledDateTimeEnd, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.billContractPeriodID> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    int? nullable = row.BillServiceContractID;
    if (nullable.HasValue && ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current?.BillingType == "STDB")
    {
      nullable = row.BillContractPeriodID;
      if (nullable.HasValue)
        ((SelectedEntityEvent<FSAppointment>) PXEntityEventBase<FSAppointment>.Container<FSAppointment.Events>.Select((Expression<Func<FSAppointment.Events, PXEntityEvent<FSAppointment.Events>>>) (ev => ev.ServiceContractPeriodAssigned))).FireOn((PXGraph) this, e.Row);
      else if (((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current?.BillingBy == "AP")
        ((SelectedEntityEvent<FSAppointment>) PXEntityEventBase<FSAppointment>.Container<FSAppointment.Events>.Select((Expression<Func<FSAppointment.Events, PXEntityEvent<FSAppointment.Events>>>) (ev => ev.RequiredServiceContractPeriodCleared))).FireOn((PXGraph) this, e.Row);
    }
    nullable = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.billContractPeriodID>, FSAppointment, object>) e).OldValue;
    int? contractPeriodId = row.BillContractPeriodID;
    if (nullable.GetValueOrDefault() == contractPeriodId.GetValueOrDefault() & nullable.HasValue == contractPeriodId.HasValue)
      return;
    ((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Current = PXResultset<FSContractPeriod>.op_Implicit(((PXSelectBase<FSContractPeriod>) this.BillServiceContractPeriod).Select(Array.Empty<object>()));
    ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Current = PXResultset<FSContractPeriodDet>.op_Implicit(((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>()));
    foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => !x.IsPickupDelivery)))
    {
      ((PXSelectBase) this.AppointmentDetails).Cache.SetDefaultExt<FSAppointmentDet.contractRelated>((object) fsAppointmentDet);
      contractPeriodId = row.BillContractPeriodID;
      if (contractPeriodId.HasValue)
        ((PXSelectBase) this.AppointmentDetails).Cache.SetDefaultExt<FSAppointmentDet.isFree>((object) fsAppointmentDet);
      ((PXSelectBase) this.AppointmentDetails).Cache.Update((object) fsAppointmentDet);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.billServiceContractID> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    int? nullable1 = row.BillServiceContractID;
    if (!nullable1.HasValue)
    {
      FSAppointment fsAppointment = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      fsAppointment.BillContractPeriodID = nullable2;
      ((SelectedEntityEvent<FSAppointment>) PXEntityEventBase<FSAppointment>.Container<FSAppointment.Events>.Select((Expression<Func<FSAppointment.Events, PXEntityEvent<FSAppointment.Events>>>) (ev => ev.ServiceContractCleared))).FireOn((PXGraph) this, e.Row);
    }
    else
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.billServiceContractID>>) e).Cache.SetDefaultExt<FSAppointment.billContractPeriodID>((object) row);
      FSServiceContract fsServiceContract = FSServiceContract.PK.Find(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.billServiceContractID>>) e).Cache.Graph, row.BillServiceContractID);
      if (fsServiceContract != null)
      {
        nullable1 = fsServiceContract.ServiceContractID;
        if (nullable1.HasValue)
        {
          this.SkipChangingContract = true;
          FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
          nullable1 = fsServiceContract.BillCustomerID;
          int? billCustomerId = current.BillCustomerID;
          if (nullable1.GetValueOrDefault() == billCustomerId.GetValueOrDefault() & nullable1.HasValue == billCustomerId.HasValue)
          {
            int? billLocationId = fsServiceContract.BillLocationID;
            nullable1 = current.BillLocationID;
            if (billLocationId.GetValueOrDefault() == nullable1.GetValueOrDefault() & billLocationId.HasValue == nullable1.HasValue)
              goto label_9;
          }
          ((PXSelectBase) this.ServiceOrderRelated).Cache.SetValueExt<FSServiceOrder.billCustomerID>((object) current, (object) fsServiceContract.BillCustomerID);
          ((PXSelectBase) this.ServiceOrderRelated).Cache.SetValueExt<FSServiceOrder.billLocationID>((object) current, (object) fsServiceContract.BillLocationID);
        }
      }
label_9:
      if (((PXGraph) this).IsCopyPasteContext || fsServiceContract == null)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.billServiceContractID>>) e).Cache.SetValueExt<FSAppointment.projectID>((object) row, (object) fsServiceContract.ProjectID);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.billServiceContractID>>) e).Cache.SetValueExt<FSAppointment.dfltProjectTaskID>((object) row, (object) fsServiceContract.DfltProjectTaskID);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.curyBillableLineTotal> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || !(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "PM"))
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.curyBillableLineTotal>>) e).Cache;
    FSAppointment row = e.Row;
    Decimal? billableLineTotal = e.Row.CuryBillableLineTotal;
    Decimal? nullable1 = e.Row.CuryLogBillableTranAmountTotal;
    Decimal? nullable2 = billableLineTotal.HasValue & nullable1.HasValue ? new Decimal?(billableLineTotal.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? curyDiscTot = e.Row.CuryDiscTot;
    Decimal? nullable3;
    if (!(nullable2.HasValue & curyDiscTot.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() - curyDiscTot.GetValueOrDefault());
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) nullable3;
    cache.SetValueExt<FSAppointment.curyDocTotal>((object) row, (object) local);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.curyLogBillableTranAmountTotal> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || !(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "PM"))
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.curyLogBillableTranAmountTotal>>) e).Cache;
    FSAppointment row = e.Row;
    Decimal? billableLineTotal = e.Row.CuryBillableLineTotal;
    Decimal? nullable1 = e.Row.CuryLogBillableTranAmountTotal;
    Decimal? nullable2 = billableLineTotal.HasValue & nullable1.HasValue ? new Decimal?(billableLineTotal.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? curyDiscTot = e.Row.CuryDiscTot;
    Decimal? nullable3;
    if (!(nullable2.HasValue & curyDiscTot.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() - curyDiscTot.GetValueOrDefault());
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) nullable3;
    cache.SetValueExt<FSAppointment.curyDocTotal>((object) row, (object) local);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.curyDiscTot> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || !(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "PM"))
      return;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.curyDiscTot>>) e).Cache;
    FSAppointment row = e.Row;
    Decimal? billableLineTotal = e.Row.CuryBillableLineTotal;
    Decimal? nullable1 = e.Row.CuryLogBillableTranAmountTotal;
    Decimal? nullable2 = billableLineTotal.HasValue & nullable1.HasValue ? new Decimal?(billableLineTotal.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? curyDiscTot = e.Row.CuryDiscTot;
    Decimal? nullable3;
    if (!(nullable2.HasValue & curyDiscTot.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() - curyDiscTot.GetValueOrDefault());
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) nullable3;
    cache.SetValueExt<FSAppointment.curyDocTotal>((object) row, (object) local);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.minLogTimeBegin> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>> orderTypeSelected = this.ServiceOrderTypeSelected;
    if ((orderTypeSelected != null ? (((PXSelectBase<FSSrvOrdType>) orderTypeSelected).Current.SetTimeInHeaderBasedOnLog.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.minLogTimeBegin>>) e).Cache.SetValueExt<FSAppointment.actualDateTimeBegin>((object) row, (object) row.MinLogTimeBegin);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.maxLogTimeEnd> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    PXSetup<FSSrvOrdType>.Where<Where<FSSrvOrdType.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>> orderTypeSelected = this.ServiceOrderTypeSelected;
    bool? nullable;
    int num;
    if (orderTypeSelected == null)
    {
      num = 0;
    }
    else
    {
      nullable = ((PXSelectBase<FSSrvOrdType>) orderTypeSelected).Current.SetTimeInHeaderBasedOnLog;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0)
      return;
    nullable = row.HandleManuallyActualTime;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.maxLogTimeEnd>>) e).Cache.SetValueExt<FSAppointment.actualDateTimeEnd>((object) row, (object) row.MaxLogTimeEnd);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.projectID> e)
  {
    if (e.Row == null)
      return;
    this.UpdateDetailsFromProjectID(e.Row.ProjectID);
    ((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Current = PXResultset<PX.Objects.CT.Contract>.op_Implicit(((PXSelectBase<PX.Objects.CT.Contract>) this.ContractRelatedToProject).Select(new object[1]
    {
      (object) e.Row.ProjectID
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.status> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.customerID> e)
  {
    if (e.Row == null)
      return;
    if (((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.customerID>>) e).Cache.Graph.IsCopyPasteContext)
    {
      ((PXSelectBase) this.AppointmentDetails).Cache.AllowInsert = true;
      ((PXSelectBase) this.AppointmentDetails).Cache.AllowUpdate = true;
    }
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    if (current == null)
      return;
    int? customerId1 = current.CustomerID;
    int? customerId2 = e.Row.CustomerID;
    if (customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue)
      return;
    ((PXSelectBase) this.ServiceOrderRelated).Cache.SetValueExtIfDifferent<FSServiceOrder.customerID>((object) current, (object) e.Row.CustomerID);
    PXResultset<FSAppointment> bqlResultSet_Appointment = PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.SOID
    });
    this.FSServiceOrder_CustomerID_FieldUpdated_Handler(((PXSelectBase) this.ServiceOrderRelated).Cache, current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, (PXSelectBase<FSSODet>) null, (PXSelectBase<FSAppointmentDet>) this.AppointmentDetails, bqlResultSet_Appointment, (int?) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointment, FSAppointment.customerID>>) e).Args.OldValue, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ScheduledDateTimeBegin, false, ((PXSelectBase<PX.Objects.AR.Customer>) this.TaxCustomer).Current);
    this.SetCurrentAppointmentSalesPersonID(current);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSAppointment> e)
  {
    if (e.Row == null || this.SkipServiceOrderUpdate || this.persistContext)
      return;
    FSAppointment row = e.Row;
    using (new PXConnectionScope())
    {
      this.UpdateServiceOrderUnboundFields(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, row, this.DisableServiceOrderUnboundFieldCalc);
      if (((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current != null && ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.SrvOrdType == row.SrvOrdType && ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.RefNbr == row.RefNbr)
        ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.AppCompletedBillableTotal = row.AppCompletedBillableTotal;
      this.ValidateServiceContractDates(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSAppointment>>) e).Cache, (object) e.Row, ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointment> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointment>>) e).Cache;
    if (((PXGraph) this).IsMobile)
      PXUIFieldAttribute.SetEnabled<FSAppointment.customerID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current != null);
    else if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null)
    {
      this.SetReadOnly(((PXSelectBase) this.AppointmentRecords).Cache, true);
      return;
    }
    this.LoadServiceOrderRelated(row);
    if (((PXSelectBase<FSSODet>) this.ServiceOrderDetails)?.Current == null)
      ((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Current = PXResultset<FSSODet>.op_Implicit(((PXSelectBase<FSSODet>) this.ServiceOrderDetails).Select(Array.Empty<object>()));
    if (((PXSelectBase<FSPostInfo>) this.PostInfoDetails)?.Current == null)
      ((PXSelectBase<FSPostInfo>) this.PostInfoDetails).Current = PXResultset<FSPostInfo>.op_Implicit(((PXSelectBase<FSPostInfo>) this.PostInfoDetails).Select(Array.Empty<object>()));
    int? nullable1;
    if (cache1.GetStatus((object) row) == 1)
    {
      nullable1 = row.AppointmentID;
      int num = 0;
      if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
        cache1.SetStatus((object) row, (PXEntryStatus) 2);
    }
    PXDefaultAttribute.SetPersistingCheck<FSAppointment.soRefNbr>(cache1, (object) row, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSAppointment.soRefNbr>(cache1, (object) row, (PXPersistingCheck) 2);
    this.EnableDisable_Document(row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, this.SkipTimeCardUpdate, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.IsCalledFromQuickProcess);
    PXCache cache2 = ((PXSelectBase) this.AppointmentDetails).Cache;
    FSSrvOrdType current1 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    bool? nullable2;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable2 = current1.PostToSOSIPM;
      num1 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<FSAppointmentDet.equipmentAction>(cache2, (object) null, num1 != 0);
    row.IsRouteAppoinment = this.ServiceOrderTypeSelected == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null ? new bool?(false) : new bool?(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.Behavior == "RO");
    if (row != null && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null)
    {
      DateTime? executionDate = row.ExecutionDate;
      DateTime? slaeta = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.SLAETA;
      if ((executionDate.HasValue & slaeta.HasValue ? (executionDate.GetValueOrDefault() > slaeta.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        cache1.RaiseExceptionHandling<FSAppointment.executionDate>((object) row, (object) row.ExecutionDate, (Exception) new PXSetPropertyException("The actual date is later than the SLA date.", (PXErrorLevel) 2));
    }
    nullable2 = row.WaitingForParts;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = row.InProcess;
      if (nullable2.GetValueOrDefault())
        cache1.RaiseExceptionHandling<FSAppointment.waitingForParts>((object) row, (object) row.WaitingForParts, (Exception) new PXSetPropertyException("The receipt of at least one item is needed.", (PXErrorLevel) 2));
    }
    nullable2 = row.Finished;
    bool flag1 = false;
    if (nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue)
    {
      nullable2 = row.Completed;
      if (nullable2.GetValueOrDefault())
        cache1.RaiseExceptionHandling<FSAppointment.finished>((object) row, (object) row.Finished, (Exception) new PXSetPropertyException("Appointment not finished.", (PXErrorLevel) 2));
    }
    this.HideRooms(row, ((PXSelectBase<FSSetup>) this.SetupRecord)?.Current);
    this.HideOrShowTimeCardsIntegration(cache1, row);
    this.HideOrShowRouteInfo(row);
    this.CheckMinMaxActualDateTimes(cache1, row);
    this.HidePrepayments(((PXSelectBase) this.Adjustments).View, ((PXSelectBase) this.ServiceOrderRelated).Cache, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
    if (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current == null)
      ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current = PXResultset<FSRouteSetup>.op_Implicit(((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Select(Array.Empty<object>()));
    PXCache cache3 = ((PXSelectBase) this.ServiceOrderRelated).Cache;
    FSServiceOrder current2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    FSServiceOrder current3 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    int num2;
    if (current3 == null)
    {
      num2 = 1;
    }
    else
    {
      nullable1 = current3.CustomerID;
      num2 = !nullable1.HasValue ? 1 : 0;
    }
    int num3;
    if (num2 != 0)
    {
      FSServiceOrder current4 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
      int num4;
      if (current4 == null)
      {
        num4 = 1;
      }
      else
      {
        nullable1 = current4.ContactID;
        num4 = !nullable1.HasValue ? 1 : 0;
      }
      num3 = num4 == 0 ? 1 : 0;
    }
    else
      num3 = 1;
    PXUIFieldAttribute.SetEnabled<FSManufacturer.allowOverrideContactAddress>(cache3, (object) current2, num3 != 0);
    PXCache cache4 = ((PXSelectBase) this.AppointmentDetails).Cache;
    nullable2 = row.IsRouteAppoinment;
    int num5 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<FSAppointmentDet.pickupDeliveryAppLineRef>(cache4, (object) null, num5 != 0);
    PXCache cache5 = ((PXSelectBase) this.AppointmentDetails).Cache;
    nullable2 = row.IsRouteAppoinment;
    int num6 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<FSAppointmentDet.pickupDeliveryServiceID>(cache5, (object) null, num6 != 0);
    PXCache cache6 = ((PXSelectBase) this.AppointmentDetails).Cache;
    nullable2 = row.IsRouteAppoinment;
    int num7 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<FSAppointmentDet.serviceType>(cache6, (object) null, num7 != 0);
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.inventory>() && PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
    PXUIFieldAttribute.SetVisibility<FSAppointmentDet.comment>(((PXSelectBase) this.AppointmentDetails).Cache, (object) null, flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<FSAppointmentDet.equipmentAction>(((PXSelectBase) this.AppointmentDetails).Cache, (object) null, flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<FSAppointmentDet.newTargetEquipmentLineNbr>(((PXSelectBase) this.AppointmentDetails).Cache, (object) null, flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    bool flag3 = this.ShouldShowMarkForPOFields(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.AllocationSOOrderTypeSelected).Current);
    PXUIFieldAttribute.SetVisible<FSAppointmentDet.enablePO>(((PXSelectBase) this.AppointmentDetails).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<FSApptLineSplit.pOCreate>(((PXSelectBase) this.Splits).Cache, (object) null, flag3);
    ServiceOrderBase<AppointmentEntry, FSAppointment>.AppointmentLog_View logRecords = this.LogRecords;
    nullable2 = row.Awaiting;
    bool flag4 = false;
    int num8 = nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue ? 1 : 0;
    ((PXSelectBase) logRecords).AllowInsert = num8 != 0;
    ((PXAction) this.openSourceDocument).SetVisible(((PXGraph) this).IsMobile);
    this.CalculateProfitValues();
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSAppointment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAppointment> e)
  {
    if (e.Row == null)
      return;
    FSAppointment row = e.Row;
    if (!string.IsNullOrEmpty(row.SrvOrdType))
    {
      this.InitServiceOrderRelated(row);
      SharedFunctions.InitializeNote(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSAppointment>>) e).Cache, ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSAppointment>>) e).Args);
    }
    int? appointmentId = row.AppointmentID;
    int num = 0;
    if (appointmentId.GetValueOrDefault() < num & appointmentId.HasValue)
      this.UpdateServiceOrderUnboundFields(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, row, this.DisableServiceOrderUnboundFieldCalc);
    row.MustUpdateServiceOrder = new bool?(true);
    this.RecalculateAreActualFieldsActive(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSAppointment>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSAppointment> e)
  {
    if (e.Row == null || !((PXGraph) this).IsMobile)
      return;
    FSAppointment row = e.Row;
    FSAppointment newRow = e.NewRow;
    if (!(row.SrvOrdType != newRow.SrvOrdType))
      return;
    int? nullable1 = row.SOID;
    int num = 0;
    if (!(nullable1.GetValueOrDefault() < num & nullable1.HasValue))
    {
      nullable1 = row.SOID;
      if (nullable1.HasValue)
        return;
    }
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null)
      ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Delete(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
    FSAppointment fsAppointment1 = row;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    fsAppointment1.SOID = nullable2;
    FSAppointment fsAppointment2 = newRow;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    fsAppointment2.SOID = nullable3;
    this.InitServiceOrderRelated(newRow);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAppointment> e)
  {
    if (e.Row == null)
      return;
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointment>>) e).Cache.ObjectsEqual<FSAppointment.srvOrdType>((object) e.Row, (object) e.OldRow) && e.OldRow.SrvOrdType == null)
      this.InitServiceOrderRelated(e.Row);
    this.CalculateEndTimeWithLinesDuration(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointment>>) e).Cache, e.Row, AppointmentEntry.DateFieldType.ScheduleField);
    this.RecalculateAreActualFieldsActive(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointment>>) e).Cache, e.Row);
    if (this.UpdatingItemLinesBecauseOfDocStatusChange)
      return;
    try
    {
      this.UpdatingItemLinesBecauseOfDocStatusChange = true;
      if (!this.IsItemLineUpdateRequired(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointment>>) e).Cache, e.Row, e.OldRow))
        return;
      this.UpdateItemLinesBecauseOfDocStatusChange();
    }
    finally
    {
      this.UpdatingItemLinesBecauseOfDocStatusChange = false;
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSAppointment> e)
  {
    if (this.AppointmentRecords == null)
      return;
    if (((IQueryable<PXResult<FSBillHistory>>) PXSelectBase<FSBillHistory, PXViewOf<FSBillHistory>.BasedOn<SelectFromBase<FSBillHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSBillHistory.srvOrdType, Equal<BqlField<FSAppointment.srvOrdType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<FSBillHistory.appointmentRefNbr, IBqlString>.IsEqual<BqlField<FSAppointment.refNbr, IBqlString>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<PXResult<FSBillHistory>>((Expression<Func<PXResult<FSBillHistory>, bool>>) (bh => ((FSBillHistory) bh).IsChildDocDeleted == (bool?) false)))
    {
      e.Cancel = true;
      throw new PXSetPropertyException<FSAppointment.refNbr>("An appointment cannot be deleted because it was billed.");
    }
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    int? apBillLineCntr = current.APBillLineCntr;
    int num = 0;
    if (apBillLineCntr.GetValueOrDefault() > num & apBillLineCntr.HasValue && ((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200") && ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Ask("This document has at least one line associated with an AP bill. Do you want to delete the document?", (MessageButtons) 1) != 1)
      e.Cancel = true;
    if (PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>, And<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) current.SOID,
      (object) current.AppointmentID
    }).Count <= 0 || this.CanDeleteServiceOrder((PXGraph) this, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current))
      return;
    this.UpdateSOStatusOnAppointmentDeleting = this.GetFinalServiceOrderStatus(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAppointment> e)
  {
    if (e.Row == null)
      return;
    this.DeleteUnpersistedServiceOrderRelated(e.Row);
    e.Row.MustUpdateServiceOrder = new bool?(true);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSAppointment> e)
  {
    PXCache cache1 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointment>>) e).Cache;
    if (!this.CanExecuteAppointmentRowPersisting(cache1, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointment>>) e).Args))
      return;
    FSAppointment row = e.Row;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    if (current != null)
    {
      bool flag1 = this.AllowEnableCustomerID(current);
      PXCache cache2 = ((PXSelectBase) this.AppointmentRecords).Cache;
      FSAppointment fsAppointment = row;
      bool? baccountRequired = current.BAccountRequired;
      bool flag2 = false;
      int num = !(baccountRequired.GetValueOrDefault() == flag2 & baccountRequired.HasValue) & flag1 ? 1 : 2;
      PXDefaultAttribute.SetPersistingCheck<FSAppointment.customerID>(cache2, (object) fsAppointment, (PXPersistingCheck) num);
    }
    if (e.Operation == 2)
      this.AutoConfirm(row);
    int? nullable1;
    int? nullable2;
    if (e.Operation == 2 || e.Operation == 1)
    {
      if (this.AreThereAnyEmployees())
      {
        this.ValidateLicenses<FSAppointment.docDesc>(cache1, (object) e.Row);
        this.ValidateSkills<FSAppointment.docDesc>(cache1, (object) e.Row);
        this.ValidateGeoZones<FSAppointment.docDesc>(cache1, (object) e.Row);
      }
      this.ValidateRoom(row);
      this.ValidateMaxAppointmentQty(row);
      this.ValidateWeekCode(row);
      this.ValidateCustomerBillingCycle(((PXSelectBase) this.ServiceOrderRelated).Cache, current, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointment>>) e).Cache, e.Row, current.BillCustomerID, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, false);
      if (!this.UpdateServiceOrder(row, this, (object) e.Row, e.Operation, new PXTranStatus?()))
        return;
      nullable1 = row.RouteID;
      if (nullable1.HasValue)
      {
        FSRoute fsRouteRow = FSRoute.PK.Find((PXGraph) this, row.RouteID);
        DateTime? beginTimeOnWeekDay = new DateTime?(DateTime.Now);
        bool flag = false;
        if (e.Operation == 1)
        {
          nullable1 = (int?) cache1.GetValueOriginal<FSAppointment.routeID>((object) row);
          int? routeId = row.RouteID;
          flag = !(nullable1.GetValueOrDefault() == routeId.GetValueOrDefault() & nullable1.HasValue == routeId.HasValue);
        }
        if (flag || e.Operation == 2)
          SharedFunctions.ValidateExecutionDay(fsRouteRow, row.ScheduledDateTimeBegin.Value.DayOfWeek, ref beginTimeOnWeekDay);
      }
      this.CheckActualDateTimes(cache1, row);
      this.CheckMinMaxActualDateTimes(cache1, row);
      this.CheckScheduledDateTimes(cache1, row);
      if (string.IsNullOrEmpty(row.DocDesc))
        this.FillDocDesc(row);
      this.CalculateEndTimeWithLinesDuration(cache1, row, AppointmentEntry.DateFieldType.ScheduleField);
      if (!this.SkipServiceOrderUpdate)
        row.BranchID = (int?) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current?.BranchID;
      this.SetTimeRegister(row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, e.Operation);
      this.ValidateRoomAvailability(cache1, row);
      this.ValidateEmployeeAvailability<FSAppointment.docDesc>(row, cache1, (object) e.Row);
      TimeCardHelper.CheckTimeCardAppointmentApprovalsAndComplete(this, cache1, row);
      if (!this.SkipTimeCardUpdate)
      {
        List<FSAppointmentLog> deleteReleatedTimeActivity = new List<FSAppointmentLog>();
        List<FSAppointmentLog> createReleatedTimeActivity = new List<FSAppointmentLog>();
        if (TimeCardHelper.IsTheTimeCardIntegrationEnabled((PXGraph) this))
          this.HandleServiceLineStatusChange(ref deleteReleatedTimeActivity, ref createReleatedTimeActivity);
        this.InsertUpdateDeleteTimeActivities(cache1, row, current, deleteReleatedTimeActivity, createReleatedTimeActivity);
      }
      if (e.Operation == 2)
      {
        if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.Behavior == "RO")
        {
          nullable2 = row.ScheduleID;
          if (nullable2.HasValue)
          {
            nullable2 = row.RouteID;
            if (!nullable2.HasValue)
              this.SetScheduleTimesByContract(row);
          }
        }
        nullable2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ScheduleID;
        if (!nullable2.HasValue)
        {
          nullable2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ServiceContractID;
          if (!nullable2.HasValue)
            goto label_32;
        }
        row.ScheduleID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ScheduleID;
        row.ServiceContractID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ServiceContractID;
      }
label_32:
      if (((PXSelectBase) this.AppointmentRecords).Cache.GetStatus((object) row) == 1 && (string) ((PXSelectBase) this.AppointmentRecords).Cache.GetValueOriginal<FSAppointment.status>((object) row) != row.Status)
      {
        ((PXSelectBase) this.AppointmentRecords).Cache.AllowUpdate = true;
        ((PXSelectBase) this.ServiceOrderRelated).Cache.AllowUpdate = true;
      }
      this.UpdatePendingPostFlags(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointment>>) e).Cache, this.AppointmentDetails, (PXSelectBase<FSPostInfo>) this.PostInfoDetails, row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
      this.ValidatePrimaryDriver();
      this.ValidateServiceContractDates(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointment>>) e).Cache, (object) row, ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current);
    }
    if ((e.Operation == 2 || e.Operation == 1 || e.Operation == 3) && !this.AvoidCalculateRouteStats)
    {
      int? valueOriginal1 = (int?) cache1.GetValueOriginal<FSAppointment.routeID>((object) row);
      int? nullable3 = (int?) cache1.GetValue<FSAppointment.routeID>((object) row);
      int? valueOriginal2 = (int?) cache1.GetValueOriginal<FSAppointment.routePosition>((object) row);
      int? nullable4 = (int?) cache1.GetValue<FSAppointment.routePosition>((object) row);
      int? valueOriginal3 = (int?) cache1.GetValueOriginal<FSAppointment.routeDocumentID>((object) row);
      int? nullable5 = (int?) cache1.GetValue<FSAppointment.routeDocumentID>((object) row);
      int? valueOriginal4 = (int?) cache1.GetValueOriginal<FSAppointment.estimatedDurationTotal>((object) row);
      int? nullable6 = (int?) cache1.GetValue<FSAppointment.estimatedDurationTotal>((object) row);
      DateTime? nullable7 = (DateTime?) cache1.GetValue<FSAppointment.scheduledDateTimeBegin>((object) row);
      DateTime? valueOriginal5 = (DateTime?) cache1.GetValueOriginal<FSAppointment.scheduledDateTimeBegin>((object) row);
      DateTime? nullable8 = (DateTime?) cache1.GetValue<FSAppointment.scheduledDateTimeEnd>((object) row);
      DateTime? valueOriginal6 = (DateTime?) cache1.GetValueOriginal<FSAppointment.scheduledDateTimeEnd>((object) row);
      nullable2 = valueOriginal1;
      nullable1 = nullable3;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable1 = valueOriginal2;
        nullable2 = nullable4;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = valueOriginal3;
          nullable1 = nullable5;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            DateTime? nullable9 = valueOriginal5;
            DateTime? nullable10 = nullable7;
            if ((nullable9.HasValue == nullable10.HasValue ? (nullable9.HasValue ? (nullable9.GetValueOrDefault() != nullable10.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            {
              nullable10 = valueOriginal6;
              DateTime? nullable11 = nullable8;
              if ((nullable10.HasValue == nullable11.HasValue ? (nullable10.HasValue ? (nullable10.GetValueOrDefault() != nullable11.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
              {
                nullable1 = valueOriginal4;
                nullable2 = nullable6;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && (e.Operation != 3 || !valueOriginal1.HasValue))
                  goto label_43;
              }
            }
          }
        }
      }
      this.NeedRecalculateRouteStats = true;
    }
label_43:
    if (e.Operation == 2)
      SharedFunctions.CopyNotesAndFiles(cache1, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, (object) row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.CustomerID, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.LocationID);
    if ((e.Operation & 3) != 3 && this.RetakeGeoLocation)
    {
      row.ROOptimizationStatus = "NO";
      if (!row.MapLatitude.HasValue || !row.MapLongitude.HasValue)
      {
        this.RetakeGeoLocation = false;
        this.ResetLatLong(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
        if (((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current != null)
        {
          nullable2 = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current.AddressID;
          int num = 0;
          if (nullable2.GetValueOrDefault() < num & nullable2.HasValue && ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null)
          {
            nullable2 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ServiceOrderAddressID;
            nullable1 = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current.AddressID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
              ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current = PXResultset<FSAddress>.op_Implicit(((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Select(Array.Empty<object>()));
          }
        }
        this.SetGeoLocation(((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
      }
    }
    if (e.Row == null || e.Operation == 3)
      return;
    this.ValidateDuplicateLineNbr((PXSelectBase<FSSODet>) null, (PXSelectBase<FSAppointmentDet>) this.AppointmentDetails);
    this.ValidateLinesLotSerials<FSAppointmentDet, FSApptLineSplit, FSAppointmentDet.lotSerialNbr>(((PXSelectBase) this.AppointmentDetails).Cache, GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())), ((PXSelectBase) this.Splits).View, (object) row, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo);
    PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
    foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()))
    {
      PXSetPropertyException propertyException2 = this.ValidateItemLineStatus(((PXSelectBase) this.AppointmentDetails).Cache, PXResult<FSAppointmentDet>.op_Implicit(pxResult), ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
      if (propertyException2 != null && propertyException1 == null)
        propertyException1 = propertyException2;
    }
    PXSetPropertyException propertyException3 = (PXSetPropertyException) null;
    foreach (PXResult<FSAppointmentLog> pxResult in ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>()))
    {
      PXSetPropertyException propertyException4 = this.ValidateLogStatus(((PXSelectBase) this.LogRecords).Cache, PXResult<FSAppointmentLog>.op_Implicit(pxResult), ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
      if (propertyException4 != null && propertyException3 == null)
        propertyException3 = propertyException4;
    }
    if (propertyException1 != null)
      throw new PXException(((Exception) propertyException1)?.Message);
    if (propertyException3 != null)
      throw new PXException(((Exception) propertyException3)?.Message);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSAppointment> e)
  {
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSAppointment>>) e).Cache;
    this.RestoreOriginalValues(cache, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSAppointment>>) e).Args);
    FSAppointment row = e.Row;
    if (e.TranStatus == 1 && e.Operation == 3)
      this.GetServiceOrderEntryGraph(false).ClearFSDocExpenseReceipts(row.NoteID);
    int? nullable1;
    bool? nullable2;
    if (e.TranStatus == 1)
    {
      if (e.Operation == 2 || e.Operation == 1 || e.Operation == 3)
      {
        nullable1 = row.RouteID;
        if (nullable1.HasValue)
        {
          nullable1 = row.RouteDocumentID;
          if (nullable1.HasValue)
          {
            nullable1 = row.RoutePosition;
            if (nullable1.HasValue)
              goto label_8;
          }
          this.SetAppointmentRouteInfo(cache, row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
          ((PXGraph) this).SelectTimeStamp();
          row.tstamp = ((PXGraph) this).TimeStamp;
        }
label_8:
        if (e.Operation != 3 || !this.SkipServiceOrderUpdate)
          this.GenerateSignedReport(cache, row);
      }
      if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.Behavior == "RO" && this.NeedRecalculateRouteStats)
      {
        this.NeedRecalculateRouteStats = false;
        int num;
        if (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current != null)
        {
          nullable2 = ((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.AutoCalculateRouteStats;
          bool flag = false;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          {
            num = 1;
            goto label_15;
          }
        }
        num = !this.CalculateGoogleStats ? 1 : 0;
label_15:
        bool simpleStatsOnly = num != 0;
        this.CalculateRouteStats(row, ((PXSelectBase<FSSetup>) this.SetupRecord).Current.MapApiKey, simpleStatsOnly);
        ((PXGraph) this).SelectTimeStamp();
        row.tstamp = ((PXGraph) this).TimeStamp;
      }
    }
    if (e.Operation == 3 && e.TranStatus == 1)
    {
      if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current == null)
        throw new PXException("Technical Error: ServiceOrderRelated.Current is NULL.");
      this.ClearPrepayment(row);
      if (!this.UpdateServiceOrder(row, this, (object) e.Row, e.Operation, new PXTranStatus?(e.TranStatus)))
        throw PXException.PreserveStack(this.CatchedServiceOrderUpdateException);
      if (!string.IsNullOrEmpty(this.UpdateSOStatusOnAppointmentDeleting))
      {
        this.SetLatestServiceOrderStatusBaseOnAppointmentStatus(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, this.UpdateSOStatusOnAppointmentDeleting);
        this.UpdateSOStatusOnAppointmentDeleting = string.Empty;
      }
    }
    if (e.TranStatus == null)
    {
      if (this.updateContractPeriod)
      {
        nullable1 = row.BillContractPeriodID;
        if (nullable1.HasValue)
        {
          nullable2 = row.Closed;
          if (!nullable2.GetValueOrDefault())
          {
            nullable2 = row.CloseActionRunning;
            if (!nullable2.GetValueOrDefault())
              goto label_29;
          }
          nullable2 = row.UnCloseActionRunning;
          bool flag = false;
          int num1;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          {
            num1 = 1;
            goto label_31;
          }
label_29:
          num1 = -1;
label_31:
          int num2 = num1;
          FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current;
          if (current != null && current.RecordType == "NRSC")
          {
            ServiceContractEntry instance = PXGraph.CreateInstance<ServiceContractEntry>();
            ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) row.BillServiceContractID, new object[1]
            {
              (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.BillCustomerID
            }));
            ((PXSelectBase) instance.ContractPeriodFilter).Cache.SetDefaultExt<FSContractPeriodFilter.contractPeriodID>((object) ((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).Current);
            int? nullable3;
            if (((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).Current != null)
            {
              nullable1 = ((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).Current.ContractPeriodID;
              nullable3 = row.BillContractPeriodID;
              if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
                ((PXSelectBase) instance.ContractPeriodFilter).Cache.SetValueExt<FSContractPeriodFilter.contractPeriodID>((object) ((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).Current, (object) row.BillContractPeriodID);
            }
            Decimal? nullable4 = new Decimal?(0M);
            int? nullable5 = new int?(0);
            foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => x.IsService && x.ContractRelated.GetValueOrDefault() && !x.IsCanceledNotPerformed.GetValueOrDefault())))
            {
              FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(((IEnumerable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Search<FSContractPeriodDet.inventoryID, FSContractPeriodDet.SMequipmentID, FSContractPeriodDet.billingRule>((object) fsAppointmentDet.InventoryID, (object) fsAppointmentDet.SMEquipmentID, (object) fsAppointmentDet.BillingRule, Array.Empty<object>())).AsEnumerable<PXResult<FSContractPeriodDet>>().FirstOrDefault<PXResult<FSContractPeriodDet>>());
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.Clear();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.ClearQueryCacheObsolete();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).View.Clear();
              ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>());
              if (contractPeriodDet != null)
              {
                Decimal? usedQty = contractPeriodDet.UsedQty;
                Decimal num3 = (Decimal) num2;
                Decimal? coveredQty = fsAppointmentDet.CoveredQty;
                Decimal? nullable6 = coveredQty.HasValue ? new Decimal?(num3 * coveredQty.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable7 = usedQty.HasValue & nullable6.HasValue ? new Decimal?(usedQty.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
                Decimal num4 = (Decimal) num2;
                nullable6 = fsAppointmentDet.ExtraUsageQty;
                Decimal? nullable8 = nullable6.HasValue ? new Decimal?(num4 * nullable6.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable9;
                if (!(nullable7.HasValue & nullable8.HasValue))
                {
                  nullable6 = new Decimal?();
                  nullable9 = nullable6;
                }
                else
                  nullable9 = new Decimal?(nullable7.GetValueOrDefault() + nullable8.GetValueOrDefault());
                Decimal? nullable10 = nullable9;
                int? usedTime = contractPeriodDet.UsedTime;
                Decimal num5 = (Decimal) num2;
                nullable7 = fsAppointmentDet.CoveredQty;
                Decimal? nullable11;
                if (!nullable7.HasValue)
                {
                  nullable6 = new Decimal?();
                  nullable11 = nullable6;
                }
                else
                  nullable11 = new Decimal?(num5 * nullable7.GetValueOrDefault());
                nullable8 = nullable11;
                Decimal num6 = (Decimal) 60;
                int? nullable12 = nullable8.HasValue ? new int?((int) (nullable8.GetValueOrDefault() * num6)) : new int?();
                nullable3 = usedTime.HasValue & nullable12.HasValue ? new int?(usedTime.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new int?();
                Decimal num7 = (Decimal) num2;
                nullable7 = fsAppointmentDet.ExtraUsageQty;
                Decimal? nullable13;
                if (!nullable7.HasValue)
                {
                  nullable6 = new Decimal?();
                  nullable13 = nullable6;
                }
                else
                  nullable13 = new Decimal?(num7 * nullable7.GetValueOrDefault());
                nullable8 = nullable13;
                Decimal num8 = (Decimal) 60;
                int? nullable14;
                if (!nullable8.HasValue)
                {
                  nullable12 = new int?();
                  nullable14 = nullable12;
                }
                else
                  nullable14 = new int?((int) (nullable8.GetValueOrDefault() * num8));
                nullable1 = nullable14;
                int? nullable15;
                if (!(nullable3.HasValue & nullable1.HasValue))
                {
                  nullable12 = new int?();
                  nullable15 = nullable12;
                }
                else
                  nullable15 = new int?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
                int? nullable16 = nullable15;
                contractPeriodDet.UsedQty = contractPeriodDet.BillingRule == "FLRA" ? nullable10 : new Decimal?(0M);
                contractPeriodDet.UsedTime = contractPeriodDet.BillingRule == "TIME" ? nullable16 : new int?(0);
              }
              ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Update(contractPeriodDet);
            }
            GraphHelper.PressButton((PXAction) instance.Save);
          }
          else if (current != null && current.RecordType == "IRSC")
          {
            RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
            ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) row.BillServiceContractID, new object[1]
            {
              (object) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.BillCustomerID
            }));
            ((PXSelectBase) instance.ContractPeriodFilter).Cache.SetDefaultExt<FSContractPeriodFilter.contractPeriodID>((object) ((PXSelectBase<FSContractPeriodFilter>) instance.ContractPeriodFilter).Current);
            Decimal? nullable17 = new Decimal?(0M);
            int? nullable18 = new int?(0);
            foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => x.IsService && x.ContractRelated.GetValueOrDefault() && !x.IsCanceledNotPerformed.GetValueOrDefault())))
            {
              FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(((IEnumerable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Search<FSContractPeriodDet.inventoryID, FSContractPeriodDet.SMequipmentID, FSContractPeriodDet.billingRule>((object) fsAppointmentDet.InventoryID, (object) fsAppointmentDet.SMEquipmentID, (object) fsAppointmentDet.BillingRule, Array.Empty<object>())).AsEnumerable<PXResult<FSContractPeriodDet>>().FirstOrDefault<PXResult<FSContractPeriodDet>>());
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.Clear();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).Cache.ClearQueryCacheObsolete();
              ((PXSelectBase) this.BillServiceContractPeriodDetail).View.Clear();
              ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>());
              if (contractPeriodDet != null)
              {
                Decimal? usedQty = contractPeriodDet.UsedQty;
                Decimal num9 = (Decimal) num2;
                Decimal? coveredQty = fsAppointmentDet.CoveredQty;
                Decimal? nullable19 = coveredQty.HasValue ? new Decimal?(num9 * coveredQty.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable20 = usedQty.HasValue & nullable19.HasValue ? new Decimal?(usedQty.GetValueOrDefault() + nullable19.GetValueOrDefault()) : new Decimal?();
                Decimal num10 = (Decimal) num2;
                nullable19 = fsAppointmentDet.ExtraUsageQty;
                Decimal? nullable21 = nullable19.HasValue ? new Decimal?(num10 * nullable19.GetValueOrDefault()) : new Decimal?();
                Decimal? nullable22;
                if (!(nullable20.HasValue & nullable21.HasValue))
                {
                  nullable19 = new Decimal?();
                  nullable22 = nullable19;
                }
                else
                  nullable22 = new Decimal?(nullable20.GetValueOrDefault() + nullable21.GetValueOrDefault());
                Decimal? nullable23 = nullable22;
                int? usedTime = contractPeriodDet.UsedTime;
                Decimal num11 = (Decimal) num2;
                nullable20 = fsAppointmentDet.CoveredQty;
                Decimal? nullable24;
                if (!nullable20.HasValue)
                {
                  nullable19 = new Decimal?();
                  nullable24 = nullable19;
                }
                else
                  nullable24 = new Decimal?(num11 * nullable20.GetValueOrDefault());
                nullable21 = nullable24;
                Decimal num12 = (Decimal) 60;
                int? nullable25 = nullable21.HasValue ? new int?((int) (nullable21.GetValueOrDefault() * num12)) : new int?();
                nullable1 = usedTime.HasValue & nullable25.HasValue ? new int?(usedTime.GetValueOrDefault() + nullable25.GetValueOrDefault()) : new int?();
                Decimal num13 = (Decimal) num2;
                nullable20 = fsAppointmentDet.ExtraUsageQty;
                Decimal? nullable26;
                if (!nullable20.HasValue)
                {
                  nullable19 = new Decimal?();
                  nullable26 = nullable19;
                }
                else
                  nullable26 = new Decimal?(num13 * nullable20.GetValueOrDefault());
                nullable21 = nullable26;
                Decimal num14 = (Decimal) 60;
                int? nullable27;
                if (!nullable21.HasValue)
                {
                  nullable25 = new int?();
                  nullable27 = nullable25;
                }
                else
                  nullable27 = new int?((int) (nullable21.GetValueOrDefault() * num14));
                int? nullable28 = nullable27;
                int? nullable29;
                if (!(nullable1.HasValue & nullable28.HasValue))
                {
                  nullable25 = new int?();
                  nullable29 = nullable25;
                }
                else
                  nullable29 = new int?(nullable1.GetValueOrDefault() + nullable28.GetValueOrDefault());
                int? nullable30 = nullable29;
                contractPeriodDet.UsedQty = contractPeriodDet.BillingRule == "FLRA" ? nullable23 : new Decimal?(0M);
                contractPeriodDet.UsedTime = contractPeriodDet.BillingRule == "TIME" ? nullable30 : new int?(0);
              }
              ((PXSelectBase<FSContractPeriodDet>) instance.ContractPeriodDetRecords).Update(contractPeriodDet);
            }
            GraphHelper.PressButton((PXAction) instance.Save);
          }
          AppointmentEntry instance1 = PXGraph.CreateInstance<AppointmentEntry>();
          foreach (PXResult<FSAppointment> pxResult in PXSelectBase<FSAppointment, PXSelectJoin<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>>, Where<FSServiceOrder.billCustomerID, Equal<Required<FSServiceOrder.billCustomerID>>, And<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.billContractPeriodID, Equal<Required<FSAppointment.billContractPeriodID>>, And<FSAppointment.closed, Equal<False>, And<FSAppointment.canceled, Equal<False>, And<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>>>>>>.Config>.Select((PXGraph) instance1, new object[4]
          {
            (object) (int?) ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current?.BillCustomerID,
            (object) row.BillServiceContractID,
            (object) row.BillContractPeriodID,
            (object) row.AppointmentID
          }))
          {
            FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
            ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Search<FSServiceOrder.refNbr>((object) fsAppointment.RefNbr, new object[1]
            {
              (object) fsAppointment.SrvOrdType
            }));
            ((PXSelectBase) instance1.AppointmentRecords).Cache.SetDefaultExt<FSAppointment.billContractPeriodID>((object) fsAppointment);
          }
          GraphHelper.PressButton((PXAction) instance1.Save);
        }
      }
      this.updateContractPeriod = false;
      this.InsertDeleteRelatedFixedRateContractBill(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSAppointment>>) e).Cache, (object) e.Row, e.Operation, (PXSelectBase<FSBillHistory>) this.InvoiceRecords);
    }
    if (e.TranStatus == 1 && (e.Operation == 2 || e.Operation == 1))
      this.UpdateServiceOrderUnboundFields(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, row, this.DisableServiceOrderUnboundFieldCalc);
    if (e.TranStatus != 1 || e.Operation != 1 && e.Operation != 3)
      return;
    this.ClearAPBillReferences(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAddress, FSAddress.countryID> e)
  {
    if (e.Row == null)
      return;
    FSAddress row = e.Row;
    if (!(row.CountryID != (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAddress, FSAddress.countryID>, FSAddress, object>) e).OldValue))
      return;
    row.State = (string) null;
    row.PostalCode = (string) null;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSAddress> e)
  {
    if ((e.Operation & 3) != 1)
      return;
    FSAddress row = e.Row;
    string valueOriginal1 = (string) ((PXSelectBase) this.ServiceOrder_Address).Cache.GetValueOriginal<FSAddress.postalCode>((object) row);
    string valueOriginal2 = (string) ((PXSelectBase) this.ServiceOrder_Address).Cache.GetValueOriginal<FSAddress.addressLine1>((object) row);
    string valueOriginal3 = (string) ((PXSelectBase) this.ServiceOrder_Address).Cache.GetValueOriginal<FSAddress.addressLine2>((object) row);
    string valueOriginal4 = (string) ((PXSelectBase) this.ServiceOrder_Address).Cache.GetValueOriginal<FSAddress.city>((object) row);
    string valueOriginal5 = (string) ((PXSelectBase) this.ServiceOrder_Address).Cache.GetValueOriginal<FSAddress.state>((object) row);
    string valueOriginal6 = (string) ((PXSelectBase) this.ServiceOrder_Address).Cache.GetValueOriginal<FSAddress.countryID>((object) row);
    if (!(row.PostalCode != valueOriginal1) && !(row.AddressLine1 != valueOriginal2) && !(row.AddressLine2 != valueOriginal3) && !(row.City != valueOriginal4) && !(row.State != valueOriginal5) && !(row.CountryID != valueOriginal6))
      return;
    this.RetakeGeoLocation = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentEmployee, FSAppointmentEmployee.primaryDriver> e)
  {
    if (e.Row == null)
      return;
    PXResultset<FSAppointmentEmployee> pxResultset = ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>());
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentEmployee, FSAppointmentEmployee.primaryDriver>, FSAppointmentEmployee, object>) e).NewValue = (object) (pxResultset.Count == 0);
    if (pxResultset.Count <= 0)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentEmployee, FSAppointmentEmployee.primaryDriver>, FSAppointmentEmployee, object>) e).NewValue = (object) GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) pxResultset).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ =>
    {
      if (!_.PrimaryDriver.GetValueOrDefault())
        return false;
      int? employeeId1 = _.EmployeeID;
      int? employeeId2 = e.Row.EmployeeID;
      return employeeId1.GetValueOrDefault() == employeeId2.GetValueOrDefault() & employeeId1.HasValue == employeeId2.HasValue;
    })).Any<FSAppointmentEmployee>();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentEmployee, FSAppointmentEmployee.earningType> e)
  {
    if (e.Row == null || e.Row.Type != "EP")
      return;
    string str = string.Empty;
    FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.appointmentID, Equal<Required<FSAppointmentDet.appointmentID>>, And<FSAppointmentDet.lineRef, Equal<Required<FSAppointmentDet.lineRef>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) e.Row.AppointmentID,
      (object) e.Row.ServiceLineRef
    }));
    if (fsAppointmentDet != null)
    {
      FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>((PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<FSAppointmentDet.inventoryID>(((PXSelectBase) this.AppointmentDetails).Cache, (object) fsAppointmentDet));
      if (extension != null && extension.DfltEarningType != null)
        str = extension.DfltEarningType;
    }
    if (string.IsNullOrEmpty(str) && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.DfltEarningType != null)
      str = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.DfltEarningType;
    if (string.IsNullOrEmpty(str))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentEmployee, FSAppointmentEmployee.earningType>, FSAppointmentEmployee, object>) e).NewValue = (object) str;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentEmployee, FSAppointmentEmployee.earningType> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentEmployee, FSAppointmentEmployee.earningType>>) e).Cancel = this.SkipEarningTypeCheck;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.serviceLineRef> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentEmployee row = e.Row;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.serviceLineRef>>) e).Cache;
    string oldValue = (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.serviceLineRef>, FSAppointmentEmployee, object>) e).OldValue;
    string serviceLineRef = row.ServiceLineRef;
    FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.lineRef, Equal<Required<FSAppointmentDet.lineRef>>, And<FSAppointmentDet.appointmentID, Equal<Current<FSAppointmentDet.appointmentID>>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) serviceLineRef
    }));
    row.DfltProjectID = (int?) fsAppointmentDet?.ProjectID;
    row.DfltProjectTaskID = (int?) fsAppointmentDet?.ProjectTaskID;
    row.CostCodeID = (int?) fsAppointmentDet?.CostCodeID;
    if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.serviceLineRef>>) e).ExternalCall)
      return;
    this.UpdateAppointmentDetService_StaffID(row.ServiceLineRef, oldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.employeeID> e)
  {
    if (e.Row == null)
      return;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    FSAppointmentEmployee row = e.Row;
    row.Type = SharedFunctions.GetBAccountType((PXGraph) this, row.EmployeeID);
    row.DfltProjectTaskID = (int?) current?.DfltProjectTaskID;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.employeeID>>) e).Cache.SetDefaultExt<FSAppointmentEmployee.trackTime>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.employeeID>>) e).Cache.SetDefaultExt<FSAppointmentEmployee.earningType>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.employeeID>>) e).Cache.SetDefaultExt<FSAppointmentEmployee.laborItemID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.employeeID>>) e).Cache.SetDefaultExt<FSAppointmentEmployee.primaryDriver>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.primaryDriver> e)
  {
    if (e.Row == null)
      return;
    this.PrimaryDriver_FieldUpdated_Handler(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentEmployee, FSAppointmentEmployee.primaryDriver>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSAppointmentEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointmentEmployee> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentEmployee row = e.Row;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentEmployee>>) e).Cache;
    this.EnableDisable_StaffRelatedFields(cache, row);
    this.EnableDisable_TimeRelatedFields(cache, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, row);
    this.SetVisible_TimeRelatedFields(cache, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
    this.SetPersisting_TimeRelatedFields(cache, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSAppointmentEmployee> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentEmployee row = e.Row;
    if (row.LineRef != null)
      return;
    row.LineRef = row.LineNbr.Value.ToString("000");
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAppointmentEmployee> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentEmployee row = e.Row;
    this.UpdateAppointmentDetService_StaffID(row.ServiceLineRef, (string) null);
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    int? employeeId = row.EmployeeID;
    if (!row.PrimaryDriver.GetValueOrDefault())
      return;
    int num;
    if (current == null)
    {
      num = employeeId.HasValue ? 1 : 0;
    }
    else
    {
      int? primaryDriver = current.PrimaryDriver;
      int? nullable = employeeId;
      num = !(primaryDriver.GetValueOrDefault() == nullable.GetValueOrDefault() & primaryDriver.HasValue == nullable.HasValue) ? 1 : 0;
    }
    if (num == 0)
      return;
    ((PXSelectBase) this.AppointmentRecords).Cache.SetValueExt<FSAppointment.primaryDriver>((object) current, (object) employeeId);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSAppointmentEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAppointmentEmployee> e)
  {
    this.MarkHeaderAsUpdated(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentEmployee>>) e).Cache, (object) e.Row);
    if (!e.Row.PrimaryDriver.GetValueOrDefault())
      return;
    ((PXSelectBase) this.AppointmentRecords).Cache.SetValueExt<FSAppointment.primaryDriver>((object) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, (object) e.Row.EmployeeID);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSAppointmentEmployee> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentEmployee row = e.Row;
    this.ValidateRouteDriverDeletionFromRouteDocument(row);
    this.UpdateAppointmentDetService_StaffID(row.ServiceLineRef, (string) null);
    this.PrimaryDriver_RowDeleting_Handler(((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<FSAppointmentEmployee>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAppointmentEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSAppointmentEmployee> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentEmployee row = e.Row;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    this.SetPersisting_TimeRelatedFields(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentEmployee>>) e).Cache, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, current, row);
    if (current != null && !ProjectDefaultAttribute.IsNonProject(current.ProjectID) && PXAccess.FeatureInstalled<FeaturesSet.costCodes>() && !e.Row.CostCodeID.HasValue)
      throw new PXException("Error: 'Cost Code' cannot be empty. Please specify the default cost code for the related service order type.");
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSAppointmentEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointmentResource> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentResource row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentResource>>) e).Cache;
    bool flag = !row.SMEquipmentID.HasValue;
    PXUIFieldAttribute.SetEnabled<FSAppointmentResource.SMequipmentID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<FSAppointmentResource.qty>(cache, (object) row, !flag);
    PXUIFieldAttribute.SetEnabled<FSAppointmentResource.comment>(cache, (object) row, !flag);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSAppointmentResource> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSAppointmentResource> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.acctID> e)
  {
    this.X_AcctID_FieldDefaulting<FSAppointmentDet>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.acctID>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.acctID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.subID> e)
  {
    this.X_SubID_FieldDefaulting<FSAppointmentDet>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.subID>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.subID>>) e).Args, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.uOM> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsService && !row.IsInventoryItem)
      return;
    this.X_UOM_FieldDefaulting<FSAppointmentDet>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.uOM>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.uOM>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.unitCost> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsInventoryItem)
    {
      INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<FSAppointmentDet.inventoryID>>, And<INItemSite.siteID, Equal<Required<FSAppointmentDet.siteID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) e.Row.InventoryID,
        (object) e.Row.SiteID
      }));
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.unitCost>, FSAppointmentDet, object>) e).NewValue = (object) (Decimal?) inItemSite?.TranUnitCost;
    }
    else
    {
      InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, e.Row.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID);
      DateTime? nullable1 = (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current == null ? 0 : (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.NotStarted.GetValueOrDefault() ? 1 : 0)) != 0 ? (DateTime?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.ScheduledDateTimeBegin : (DateTime?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.ActualDateTimeBegin;
      if (itemCurySettings == null)
        return;
      DateTime? stdCostDate = itemCurySettings.StdCostDate;
      DateTime? nullable2 = nullable1;
      if ((stdCostDate.HasValue & nullable2.HasValue ? (stdCostDate.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.unitCost>, FSAppointmentDet, object>) e).NewValue = (object) itemCurySettings.StdCost;
      else
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.unitCost>, FSAppointmentDet, object>) e).NewValue = (object) itemCurySettings.LastStdCost;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitCost> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (string.IsNullOrEmpty(row.UOM) || !row.InventoryID.HasValue)
      return;
    object obj;
    ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitCost>>) e).Cache.RaiseFieldDefaulting<FSAppointmentDet.unitCost>((object) e.Row, ref obj);
    if (obj == null || !((Decimal) obj != 0M))
      return;
    Decimal curyval = INUnitAttribute.ConvertToBase<FSAppointmentDet.inventoryID, FSAppointmentDet.uOM>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitCost>>) e).Cache, (object) row, (Decimal) obj, INPrecision.NOROUND);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ExtensionHelper.SelectCurrencyInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyInfoView, ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.CuryInfoID);
    PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitCost>>) e).Cache, currencyInfo.GetCM(), curyval, out curyval);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitCost>, FSAppointmentDet, object>) e).NewValue = (object) curyval;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitCost>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.costCodeID> e)
  {
    if (e.Row == null)
      return;
    this.SetCostCodeDefault((IFSSODetBase) e.Row, (int?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.ProjectID, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.costCodeID>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.contractRelated> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    FSAppointmentDet fsAppointmentDetRow = e.Row;
    if (!fsAppointmentDetRow.IsService)
      return;
    if (!(this.GetBillingMode(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current) != "AP"))
    {
      int? nullable = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.BillServiceContractID;
      if (nullable.HasValue)
      {
        nullable = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.BillContractPeriodID;
        if (nullable.HasValue)
        {
          if (((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current?.BillingType != "STDB")
          {
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.contractRelated>, FSAppointmentDet, object>) e).NewValue = (object) false;
            return;
          }
          FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Search<FSAppointmentDet.inventoryID, FSAppointmentDet.SMequipmentID, FSAppointmentDet.billingRule, FSAppointmentDet.contractRelated>((object) fsAppointmentDetRow.InventoryID, (object) fsAppointmentDetRow.SMEquipmentID, (object) fsAppointmentDetRow.BillingRule, (object) true, Array.Empty<object>()));
          int num;
          if (fsAppointmentDet != null)
          {
            nullable = fsAppointmentDet.LineID;
            int? lineId = fsAppointmentDetRow.LineID;
            num = !(nullable.GetValueOrDefault() == lineId.GetValueOrDefault() & nullable.HasValue == lineId.HasValue) ? 1 : 0;
          }
          else
            num = 0;
          bool flag = num != 0;
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.contractRelated>, FSAppointmentDet, object>) e).NewValue = (object) (bool) (flag ? 0 : (GraphHelper.RowCast<FSContractPeriodDet>((IEnumerable) ((IEnumerable<PXResult<FSContractPeriodDet>>) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSContractPeriodDet>>()).Where<FSContractPeriodDet>((Func<FSContractPeriodDet, bool>) (x =>
          {
            int? inventoryId1 = x.InventoryID;
            int? inventoryId2 = fsAppointmentDetRow.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? smEquipmentId1 = x.SMEquipmentID;
              int? smEquipmentId2 = fsAppointmentDetRow.SMEquipmentID;
              if (smEquipmentId1.GetValueOrDefault() == smEquipmentId2.GetValueOrDefault() & smEquipmentId1.HasValue == smEquipmentId2.HasValue || !x.SMEquipmentID.HasValue)
                return x.BillingRule == fsAppointmentDetRow.BillingRule;
            }
            return false;
          })).Count<FSContractPeriodDet>() == 1 ? 1 : 0));
          return;
        }
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.contractRelated>, FSAppointmentDet, object>) e).NewValue = (object) false;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.coveredQty> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet fsAppointmentDetRow = e.Row;
    if (!fsAppointmentDetRow.IsService)
      return;
    if (!(this.GetBillingMode(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current) != "AP"))
    {
      bool? contractRelated = fsAppointmentDetRow.ContractRelated;
      bool flag1 = false;
      if (!(contractRelated.GetValueOrDefault() == flag1 & contractRelated.HasValue))
      {
        FSContractPeriodDet contractPeriodDet = GraphHelper.RowCast<FSContractPeriodDet>((IEnumerable) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).Where<FSContractPeriodDet>((Func<FSContractPeriodDet, bool>) (x =>
        {
          int? inventoryId1 = x.InventoryID;
          int? inventoryId2 = fsAppointmentDetRow.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? smEquipmentId1 = x.SMEquipmentID;
            int? smEquipmentId2 = fsAppointmentDetRow.SMEquipmentID;
            if (smEquipmentId1.GetValueOrDefault() == smEquipmentId2.GetValueOrDefault() & smEquipmentId1.HasValue == smEquipmentId2.HasValue || !x.SMEquipmentID.HasValue)
              return x.BillingRule == fsAppointmentDetRow.BillingRule;
          }
          return false;
        })).FirstOrDefault<FSContractPeriodDet>();
        if (contractPeriodDet != null)
        {
          bool? nullable1 = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.NotStarted;
          int? nullable2;
          if (nullable1.GetValueOrDefault())
          {
            nullable1 = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.StartActionRunning;
            bool flag2 = false;
            if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
            {
              nullable2 = fsAppointmentDetRow.EstimatedDuration;
              goto label_11;
            }
          }
          nullable2 = fsAppointmentDetRow.ActualDuration;
label_11:
          int? nullable3 = nullable2;
          nullable1 = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.NotStarted;
          Decimal? nullable4;
          if (nullable1.GetValueOrDefault())
          {
            nullable1 = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.StartActionRunning;
            bool flag3 = false;
            if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
            {
              nullable4 = fsAppointmentDetRow.EstimatedQty;
              goto label_15;
            }
          }
          nullable4 = fsAppointmentDetRow.ActualQty;
label_15:
          Decimal? nullable5 = nullable4;
          if (fsAppointmentDetRow.BillingRule == "TIME")
          {
            PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.coveredQty> fieldDefaulting = e;
            int? remainingTime = contractPeriodDet.RemainingTime;
            int? nullable6 = nullable3;
            int? nullable7 = remainingTime.HasValue & nullable6.HasValue ? new int?(remainingTime.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new int?();
            int num = 0;
            Decimal? nullable8;
            if (!(nullable7.GetValueOrDefault() >= num & nullable7.HasValue))
            {
              nullable7 = contractPeriodDet.RemainingTime;
              nullable8 = nullable7.HasValue ? new Decimal?((Decimal) (nullable7.GetValueOrDefault() / 60)) : new Decimal?();
            }
            else
              nullable8 = nullable5;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal?> local = (ValueType) nullable8;
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.coveredQty>, FSAppointmentDet, object>) fieldDefaulting).NewValue = (object) local;
            return;
          }
          PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.coveredQty> fieldDefaulting1 = e;
          Decimal? remainingQty = contractPeriodDet.RemainingQty;
          Decimal? nullable9 = nullable5;
          Decimal? nullable10 = remainingQty.HasValue & nullable9.HasValue ? new Decimal?(remainingQty.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new Decimal?();
          Decimal num1 = 0M;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local1 = (ValueType) (nullable10.GetValueOrDefault() >= num1 & nullable10.HasValue ? nullable5 : contractPeriodDet.RemainingQty);
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.coveredQty>, FSAppointmentDet, object>) fieldDefaulting1).NewValue = (object) local1;
          return;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.coveredQty>, FSAppointmentDet, object>) e).NewValue = (object) 0M;
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.coveredQty>, FSAppointmentDet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyExtraUsageUnitPrice> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet fsAppointmentDetRow = e.Row;
    if (!fsAppointmentDetRow.IsService)
      return;
    if (!(this.GetBillingMode(((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current) != "AP"))
    {
      bool? contractRelated = fsAppointmentDetRow.ContractRelated;
      bool flag = false;
      if (!(contractRelated.GetValueOrDefault() == flag & contractRelated.HasValue))
      {
        FSContractPeriodDet contractPeriodDet = GraphHelper.RowCast<FSContractPeriodDet>((IEnumerable) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).Where<FSContractPeriodDet>((Func<FSContractPeriodDet, bool>) (x =>
        {
          int? inventoryId1 = x.InventoryID;
          int? inventoryId2 = fsAppointmentDetRow.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? smEquipmentId1 = x.SMEquipmentID;
            int? smEquipmentId2 = fsAppointmentDetRow.SMEquipmentID;
            if (smEquipmentId1.GetValueOrDefault() == smEquipmentId2.GetValueOrDefault() & smEquipmentId1.HasValue == smEquipmentId2.HasValue || !x.SMEquipmentID.HasValue)
              return x.BillingRule == fsAppointmentDetRow.BillingRule;
          }
          return false;
        })).FirstOrDefault<FSContractPeriodDet>();
        if (contractPeriodDet != null)
        {
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyExtraUsageUnitPrice>, FSAppointmentDet, object>) e).NewValue = (object) contractPeriodDet.OverageItemPrice;
          return;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyExtraUsageUnitPrice>, FSAppointmentDet, object>) e).NewValue = (object) 0M;
        return;
      }
    }
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyExtraUsageUnitPrice>, FSAppointmentDet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitPrice> e)
  {
    if (e.Row == null || e.Row.IsLinkedItem)
      return;
    FSAppointmentDet row = e.Row;
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    DateTime? docDate = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.ScheduledDateTimeBegin;
    int num = !(((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo == "PM") ? 0 : (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.BillingType == "CC" ? 1 : 0);
    if (docDate.HasValue)
      docDate = new DateTime?(new DateTime(docDate.Value.Year, docDate.Value.Month, docDate.Value.Day));
    if (num == 0)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ExtensionHelper.SelectCurrencyInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyInfoView, ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.CuryInfoID);
      Decimal? billableQty = row.BillableQty;
      this.X_CuryUnitPrice_FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitPrice>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitPrice>>) e).Cache, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitPrice>>) e).Args, billableQty, docDate, current, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, currencyInfo);
    }
    else
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitPrice>, FSAppointmentDet, object>) e).NewValue = (object) row.CuryUnitCost.GetValueOrDefault();
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.curyUnitPrice>>) e).Cancel = row.CuryUnitCost.HasValue;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.siteID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (this.IsInventoryLine(row.LineType) && row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.siteID>, FSAppointmentDet, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.effTranQty> e)
  {
    if (e.Row.AreActualFieldsActive.GetValueOrDefault())
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.effTranQty>, FSAppointmentDet, object>) e).NewValue = (object) e.Row.ActualQty;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.effTranQty>, FSAppointmentDet, object>) e).NewValue = (object) e.Row.EstimatedQty;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.actualDuration> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.actualDuration>, FSAppointmentDet, object>) e).NewValue = (object) 0;
    if (!(e.Row.LineType == "SERVI") && !(e.Row.LineType == "NSTKI") || !e.Row.AreActualFieldsActive.GetValueOrDefault())
      return;
    int logTrackingCount = this.GetLogTrackingCount(e.Row.LineRef);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.actualDuration>, FSAppointmentDet, object>) e).NewValue = (object) (logTrackingCount > 0 ? e.Row.LogActualDuration : e.Row.EstimatedDuration);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.enablePO> e)
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
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.enablePO>, FSAppointmentDet, object>) e).NewValue = (object) false;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.enablePO>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.poVendorID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    bool? enablePo = row.EnablePO;
    bool flag = false;
    if (!(enablePo.GetValueOrDefault() == flag & enablePo.HasValue) && row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.poVendorID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.poVendorLocationID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
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
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.poVendorLocationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.isFree> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsCommentInstruction)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.isFree>, FSAppointmentDet, object>) e).NewValue = (object) true;
    else if (e.Row.BillingRule == "NONE")
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.isFree>, FSAppointmentDet, object>) e).NewValue = (object) true;
    else if (((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current?.BillingType == "FIRB" && (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo != "PM" || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.PostTo == "PM" && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.BillingType != "CC"))
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.isFree>, FSAppointmentDet, object>) e).NewValue = (object) true;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.isFree>, FSAppointmentDet, object>) e).NewValue = (object) false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.actualQty> e)
  {
    if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).NewValue == null || e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsPickupDelivery || !(Convert.ToDecimal(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).NewValue) < 0M))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).Cache.RaiseExceptionHandling<FSAppointmentDet.actualQty>((object) e.Row, (object) row.ActualQty, (Exception) new PXSetPropertyException("The quantity must be greater than 0.", (PXErrorLevel) 2));
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).NewValue = (object) row.ActualQty;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.uiStatus> e)
  {
    if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.uiStatus>>) e).NewValue == null || e.Row == null)
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.uiStatus>>) e).NewValue = (object) this.GetValidAppDetStatus(e.Row, (string) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.uiStatus>>) e).NewValue);
    ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.uiStatus>>) e).Cache.SetValueExt<FSAppointmentDet.status>((object) e.Row, ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.uiStatus>>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.status> e)
  {
    if (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.status>>) e).NewValue == null || e.Row == null)
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.status>>) e).NewValue = (object) this.GetValidAppDetStatus(e.Row, (string) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentDet, FSAppointmentDet.status>>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.billingRule> e)
  {
    FSAppointmentDet row = e.Row;
    if (e.Row != null && !row.IsService)
      return;
    this.X_BillingRule_FieldVerifying<FSAppointmentDet>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.billingRule>>) e).Cache, ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.billingRule>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.lotSerialNbr> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsInventoryItem)
      return;
    int? nullable1 = new int?(((IQueryable<PXResult<FSAppointmentDet>>) PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>>, Where<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>, And<FSAppointment.canceled, Equal<False>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>, And<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<FSAppointmentDet.appDetID, NotEqual<Required<FSAppointmentDet.appDetID>>, And<FSAppointmentDet.lotSerialNbr, Equal<Required<FSAppointmentDet.lotSerialNbr>>>>>>>>>.Config>.Select(new PXGraph(), new object[3]
    {
      (object) row.SODetID,
      (object) row.AppDetID,
      (object) (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.lotSerialNbr>, FSAppointmentDet, object>) e).NewValue
    })).Count<PXResult<FSAppointmentDet>>());
    if (!nullable1.HasValue)
      return;
    int? nullable2 = nullable1;
    int num = 0;
    if (!(nullable2.GetValueOrDefault() > num & nullable2.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.lotSerialNbr>>) e).Cache.RaiseExceptionHandling<FSAppointmentDet.lotSerialNbr>((object) row, (object) null, (Exception) new PXSetPropertyException("A serial number cannot be repeated for a stock item in different appointments.", (PXErrorLevel) 4));
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.lotSerialNbr>, FSAppointmentDet, object>) e).NewValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.uiStatus> e)
  {
    if (e.Row == null || ((PXGraph) this).IsCopyPasteContext)
      return;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.uiStatus>, FSAppointmentDet, object>) e).NewValue;
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.uiStatus>>) e).ExternalCall)
      return;
    if (newValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FSAppointmentDet.uiStatus>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.uiStatus>>) e).Cache)
      });
    if (!this.IsNewItemLineStatusValid(e.Row, newValue))
    {
      FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current;
      throw new PXSetPropertyException("The {0} status cannot be selected if the appointment has the {1} status.", new object[2]
      {
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointmentDet.uiStatus>(((PXSelectBase) this.AppointmentDetails).Cache, (object) e.Row, newValue),
        (object) PXStringListAttribute.GetLocalizedLabel<FSAppointment.status>(((PXSelectBase) this.AppointmentSelected).Cache, (object) current, current.Status)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.curyUnitPrice> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.BillingRule == "NONE" && e.Row.InventoryID.HasValue)
    {
      e.Row.ManualPrice = new bool?(false);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.curyUnitPrice>, FSAppointmentDet, object>) e).NewValue = (object) 0M;
    }
    else
    {
      if (!this.GetServiceOrderEntryGraph(false).IsManualPriceFlagNeeded(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.curyUnitPrice>>) e).Cache, (IFSSODetBase) e.Row))
        return;
      e.Row.ManualPrice = new bool?(true);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.curyExtPrice> e)
  {
    if (e.Row == null || !this.GetServiceOrderEntryGraph(false).IsManualPriceFlagNeeded(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.curyExtPrice>>) e).Cache, (IFSSODetBase) e.Row))
      return;
    e.Row.ManualPrice = new bool?(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualDuration> e)
  {
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualDuration>>) e).ExternalCall)
      return;
    this.ValidateActualDurationEdit(e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualDuration>, FSAppointmentDet, object>) e).NewValue as int?);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.estimatedQty> e)
  {
    Decimal srvOrdAllocatedQty;
    Decimal otherAppointmentsUsedQty;
    if (this.GetSrvOrdLineBalance(e.Row.SODetID, e.Row.AppDetID, out srvOrdAllocatedQty, out otherAppointmentsUsedQty))
    {
      Decimal num = srvOrdAllocatedQty - otherAppointmentsUsedQty;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.estimatedQty>, FSAppointmentDet, object>) e).NewValue = (object) (num < 1M ? num : 1M);
    }
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentDet, FSAppointmentDet.estimatedQty>, FSAppointmentDet, object>) e).NewValue = (object) 1M;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.estimatedQty> e)
  {
    this.VerifySrvOrdLineQty(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.estimatedQty>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.estimatedQty>, FSAppointmentDet, object>) e).NewValue, typeof (FSAppointmentDet.estimatedQty), true);
    this.VerifyIfQtyDivisible(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.estimatedQty>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.estimatedQty>, FSAppointmentDet, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualQty> e)
  {
    this.VerifySrvOrdLineQty(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualQty>, FSAppointmentDet, object>) e).NewValue, typeof (FSAppointmentDet.actualQty), true);
    this.VerifyIfQtyDivisible(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.actualQty>, FSAppointmentDet, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.enablePO> e)
  {
    int? soDetId = e.Row.SODetID;
    int num = 0;
    if (soDetId.GetValueOrDefault() > num & soDetId.HasValue)
    {
      FSSODet soLine = FSSODet.UK.Find((PXGraph) this, e.Row.SODetID);
      bool? nullable = soLine != null ? soLine.EnablePO : throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSODet))
      });
      if (nullable.Value != (bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.enablePO>, FSAppointmentDet, object>) e).NewValue)
      {
        nullable = e.Row.CanChangeMarkForPO;
        if (!nullable.GetValueOrDefault())
        {
          PXException exception;
          this.CanChangePOOptions(e.Row, ref soLine, typeof (FSAppointmentDet.enablePO).Name, out exception);
          if (exception != null)
            throw exception;
        }
      }
    }
    this.POCreateVerifyValue(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.enablePO>>) e).Cache, e.Row, (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.enablePO>, FSAppointmentDet, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.pOSource> e)
  {
    if (!((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.pOSource>>) e).ExternalCall)
      return;
    int? soDetId = e.Row.SODetID;
    int num = 0;
    if (!(soDetId.GetValueOrDefault() > num & soDetId.HasValue))
      return;
    FSSODet soLine = FSSODet.UK.Find((PXGraph) this, e.Row.SODetID);
    if (soLine == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSODet))
      });
    if (e.Row.CanChangeMarkForPO.GetValueOrDefault())
      return;
    PXException exception;
    this.CanChangePOOptions(e.Row, ref soLine, typeof (FSAppointmentDet.pOSource).Name, out exception);
    if (exception != null)
      throw exception;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.curyBillableExtPrice> e)
  {
    if (e.Row == null || !(e.Row.BillingRule == "NONE"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.curyBillableExtPrice>, FSAppointmentDet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.discPct> e)
  {
    if (e.Row == null || !(e.Row.BillingRule == "NONE"))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.discPct>, FSAppointmentDet, object>) e).NewValue = (object) 0M;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteID> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Cache.GetStatus((object) e.Row) != 1 || !(((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200")))
      return;
    if (PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SODetID
    }).Count > 1)
      throw new PXSetPropertyException("The {0} cannot be changed for the appointment line because the corresponding service order line has been added to at least one more appointment. You can change the {0} for the service order line on the Service Order (FS300100) form", new object[1]
      {
        (object) ((PXFieldState) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Cache.GetStateExt<FSAppointmentDet.siteID>((object) e.Row)).DisplayName
      });
    FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, InnerJoin<FSSODet, On<FSSODet.srvOrdType, Equal<FSServiceOrder.srvOrdType>, And<FSSODet.refNbr, Equal<FSServiceOrder.refNbr>>>>, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SODetID
    }));
    bool? nullable;
    if (fsServiceOrder != null)
    {
      nullable = fsServiceOrder.AllowInvoice;
      if (nullable.GetValueOrDefault())
        goto label_9;
    }
    if (fsServiceOrder != null)
    {
      nullable = fsServiceOrder.Billed;
      if (nullable.GetValueOrDefault())
        goto label_9;
    }
    if (fsServiceOrder == null)
      return;
    nullable = fsServiceOrder.Closed;
    if (!nullable.GetValueOrDefault())
      return;
label_9:
    throw new PXSetPropertyException("The {0} cannot be changed because the service order of this appointment is billed or closed, or the Allow Billing command is selected for this service order.", new object[1]
    {
      (object) ((PXFieldState) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Cache.GetStateExt<FSAppointmentDet.siteLocationID>((object) e.Row)).DisplayName
    });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteLocationID> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteLocationID>>) e).Cache.GetStatus((object) e.Row) != 1 || !(((PXGraph) this).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200")))
      return;
    if (PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SODetID
    }).Count > 1)
      throw new PXSetPropertyException("The {0} cannot be changed for the appointment line because the corresponding service order line has been added to at least one more appointment. You can change the {0} for the service order line on the Service Order (FS300100) form", new object[1]
      {
        (object) ((PXFieldState) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteLocationID>>) e).Cache.GetStateExt<FSAppointmentDet.siteLocationID>((object) e.Row)).DisplayName
      });
    FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, InnerJoin<FSSODet, On<FSSODet.srvOrdType, Equal<FSServiceOrder.srvOrdType>, And<FSSODet.refNbr, Equal<FSServiceOrder.refNbr>>>>, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SODetID
    }));
    bool? nullable;
    if (fsServiceOrder != null)
    {
      nullable = fsServiceOrder.AllowInvoice;
      if (nullable.GetValueOrDefault())
        goto label_9;
    }
    if (fsServiceOrder != null)
    {
      nullable = fsServiceOrder.Billed;
      if (nullable.GetValueOrDefault())
        goto label_9;
    }
    if (fsServiceOrder == null)
      return;
    nullable = fsServiceOrder.Closed;
    if (!nullable.GetValueOrDefault())
      return;
label_9:
    throw new PXSetPropertyException("The {0} cannot be changed because the service order of this appointment is billed or closed, or the Allow Billing command is selected for this service order.", new object[1]
    {
      (object) ((PXFieldState) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentDet, FSAppointmentDet.siteLocationID>>) e).Cache.GetStateExt<FSAppointmentDet.siteLocationID>((object) e.Row)).DisplayName
    });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isPrepaid> e)
  {
    this.X_IsPrepaid_FieldUpdated<FSAppointmentDet, FSAppointmentDet.manualPrice, FSAppointmentDet.isFree, FSAppointmentDet.estimatedDuration, FSAppointmentDet.actualDuration>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isPrepaid>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isPrepaid>>) e).Args, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.manualPrice> e)
  {
    this.X_ManualPrice_FieldUpdated<FSAppointmentDet, FSAppointmentDet.curyUnitPrice, FSAppointmentDet.curyBillableExtPrice>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.manualPrice>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.manualPrice>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet fsAppointmentDetRow = e.Row;
    FSServiceOrder current1 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    this.X_InventoryID_FieldUpdated<FSAppointmentDet, FSAppointmentDet.acctID, FSAppointmentDet.subItemID, FSAppointmentDet.siteID, FSAppointmentDet.siteLocationID, FSAppointmentDet.uOM, FSAppointmentDet.estimatedDuration, FSAppointmentDet.estimatedQty, FSAppointmentDet.billingRule, FSAppointmentDet.actualDuration, FSAppointmentDet.actualQty>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Args, current1.BranchLocationID, ((PXSelectBase<PX.Objects.AR.Customer>) this.TaxCustomer).Current, true);
    if (fsAppointmentDetRow.IsService)
    {
      fsAppointmentDetRow.ServiceType = (string) null;
      PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.Graph, fsAppointmentDetRow.InventoryID);
      if (inventoryItemRow != null)
      {
        FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
        fsAppointmentDetRow.ServiceType = extension?.ActionType;
      }
      if (fsAppointmentDetRow.ContractRelated.GetValueOrDefault())
      {
        FSContractPeriodDet contractPeriodDet = GraphHelper.RowCast<FSContractPeriodDet>((IEnumerable) ((PXSelectBase<FSContractPeriodDet>) this.BillServiceContractPeriodDetail).Select(Array.Empty<object>())).Where<FSContractPeriodDet>((Func<FSContractPeriodDet, bool>) (x =>
        {
          int? inventoryId1 = x.InventoryID;
          int? inventoryId2 = fsAppointmentDetRow.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? smEquipmentId1 = x.SMEquipmentID;
            int? smEquipmentId2 = fsAppointmentDetRow.SMEquipmentID;
            if (smEquipmentId1.GetValueOrDefault() == smEquipmentId2.GetValueOrDefault() & smEquipmentId1.HasValue == smEquipmentId2.HasValue || !x.SMEquipmentID.HasValue)
              return x.BillingRule == fsAppointmentDetRow.BillingRule;
          }
          return false;
        })).FirstOrDefault<FSContractPeriodDet>();
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.SetValueExt<FSAppointmentDet.projectTaskID>((object) fsAppointmentDetRow, (object) contractPeriodDet.ProjectTaskID);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.SetValueExt<FSAppointmentDet.costCodeID>((object) fsAppointmentDetRow, (object) contractPeriodDet.CostCodeID);
      }
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.SetDefaultExt<FSAppointmentDet.equipmentAction>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.SetDefaultExt<FSAppointmentDet.curyUnitCost>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.SetDefaultExt<FSAppointmentDet.enablePO>((object) e.Row);
    }
    else if (fsAppointmentDetRow.IsInventoryItem)
    {
      PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache;
      FSAppointmentDet row = fsAppointmentDetRow;
      int? inventoryId = fsAppointmentDetRow.InventoryID;
      FSSrvOrdType current2 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
      FSAppointment current3 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
      int num;
      if (current3 == null)
      {
        num = 0;
      }
      else
      {
        bool? notStarted = current3.NotStarted;
        bool flag = false;
        num = notStarted.GetValueOrDefault() == flag & notStarted.HasValue ? 1 : 0;
      }
      SharedFunctions.UpdateEquipmentFields((PXGraph) this, cache, (object) row, inventoryId, current2, num != 0);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.SetDefaultExt<FSAppointmentDet.curyUnitCost>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.SetDefaultExt<FSAppointmentDet.enablePO>((object) e.Row);
    }
    if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).ExternalCall || !fsAppointmentDetRow.InventoryID.HasValue)
      return;
    foreach (PXSelectorAttribute selectorAttribute in ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.inventoryID>>) e).Cache.GetAttributes(typeof (FSAppointmentDet.inventoryID).Name).OfType<PXSelectorAttribute>())
      selectorAttribute.ShowPopupMessage = false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.tranDesc> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (string.IsNullOrEmpty(row.TranDesc) || row.LineType != null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.tranDesc>>) e).Cache.SetValueExt<FSAppointmentDet.lineType>((object) row, (object) "IT_LN");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.status> e)
  {
    if (e.Row == null)
      return;
    string oldValue = (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.status>, FSAppointmentDet, object>) e).OldValue;
    AppointmentEntry.UpdateCanceledNotPerformed(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.status>>) e).Cache, e.Row, ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current, oldValue);
    if (!(e.Row.Status != oldValue))
      return;
    if (e.Row.Status == "RP" || e.Row.Status == "WP")
      this.SetItemLineUIStatusList(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.status>>) e).Cache, e.Row);
    if (!e.Row.IsTravelItem.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.status>>) e).Cache.SetDefaultExt<FSAppointmentDet.areActualFieldsActive>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isCanceledNotPerformed> e)
  {
    if (e.Row == null || !e.Row.IsService || !e.Row.IsCanceledNotPerformed.GetValueOrDefault())
      return;
    foreach (PXResult<FSAppointmentEmployee> pxResult in ((IEnumerable<PXResult<FSAppointmentEmployee>>) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentEmployee>>().Where<PXResult<FSAppointmentEmployee>>((Func<PXResult<FSAppointmentEmployee>, bool>) (y => PXResult<FSAppointmentEmployee>.op_Implicit(y).ServiceLineRef == e.Row.LineRef)))
    {
      FSAppointmentEmployee appointmentEmployee = PXResult<FSAppointmentEmployee>.op_Implicit(pxResult);
      appointmentEmployee.TrackTime = new bool?(false);
      ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Update(appointmentEmployee);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.sODetID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsPickupDelivery)
    {
      FSSODet soLine = FSSODet.UK.Find((PXGraph) this, e.Row.SODetID);
      row.FSSODetRow = soLine;
      this.GetSODetValues<FSAppointmentDet, FSSODet>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.sODetID>>) e).Cache, row, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, soLine);
      row.CanChangeMarkForPO = new bool?(this.CanChangePOOptions(row, ref soLine, typeof (FSAppointmentDet.enablePO).Name, out PXException _));
    }
    else
    {
      string oldValue = (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.sODetID>, FSAppointmentDet, object>) e).OldValue;
      string str = row.LineRef ?? oldValue;
      FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>>, Where<FSSODet.lineRef, Equal<Required<FSSODet.lineRef>>, And<FSSODet.sOID, Equal<Current<FSAppointment.sOID>>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.sODetID>>) e).Cache.Graph, new object[1]
      {
        (object) str
      }));
      row.ProjectTaskID = (int?) fsAppointmentDet?.ProjectTaskID;
      row.CostCodeID = (int?) fsAppointmentDet?.CostCodeID;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedQty> e)
  {
    Decimal? oldValue = (Decimal?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedQty>, FSAppointmentDet, object>) e).OldValue;
    Decimal? estimatedQty = e.Row.EstimatedQty;
    if (oldValue.GetValueOrDefault() == estimatedQty.GetValueOrDefault() & oldValue.HasValue == estimatedQty.HasValue)
      return;
    if (e.Row.IsLinkedItem || ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.InProcess.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedQty>>) e).Cache.SetValueExt<FSAppointmentDet.actualQty>((object) e.Row, (object) e.Row.EstimatedQty);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedQty>>) e).Cache.SetDefaultExt<FSAppointmentDet.effTranQty>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.contractRelated> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (row.IsPickupDelivery)
      return;
    bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.contractRelated>, FSAppointmentDet, object>) e).OldValue;
    bool? contractRelated = row.ContractRelated;
    if (oldValue.GetValueOrDefault() == contractRelated.GetValueOrDefault() & oldValue.HasValue == contractRelated.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.contractRelated>>) e).Cache.SetDefaultExt<FSAppointmentDet.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.billableQty> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (row.IsLinkedItem || !row.IsService && !row.IsInventoryItem)
      return;
    Decimal? oldValue = (Decimal?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.billableQty>, FSAppointmentDet, object>) e).OldValue;
    Decimal? billableQty = e.Row.BillableQty;
    if (oldValue.GetValueOrDefault() == billableQty.GetValueOrDefault() & oldValue.HasValue == billableQty.HasValue)
      return;
    this.X_Qty_FieldUpdated<FSAppointmentDet.curyUnitPrice>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.billableQty>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.billableQty>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (row.IsService || row.IsPickupDelivery)
      this.X_Qty_FieldUpdated<FSAppointmentDet.curyUnitPrice>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).Args);
    Decimal? oldValue = (Decimal?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty>, FSAppointmentDet, object>) e).OldValue;
    Decimal? actualQty = row.ActualQty;
    if (!(oldValue.GetValueOrDefault() == actualQty.GetValueOrDefault() & oldValue.HasValue == actualQty.HasValue))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).Cache.SetDefaultExt<FSAppointmentDet.effTranQty>((object) row);
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).ExternalCall)
      return;
    int? appDetId = row.AppDetID;
    int num = 0;
    if (!(appDetId.GetValueOrDefault() < num & appDetId.HasValue) || current == null)
      return;
    bool? nullable = current.InProcess;
    if (!nullable.GetValueOrDefault())
    {
      nullable = current.Completed;
      if (!nullable.GetValueOrDefault())
        return;
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty>>) e).Cache.SetValueExtIfDifferent<FSAppointmentDet.estimatedQty>((object) row, (object) row.ActualQty);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualDuration> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsService)
      return;
    this.X_Duration_FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualQty>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualDuration>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.actualDuration>>) e).Args, row.ActualDuration);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedDuration> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsService)
      return;
    if (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.InProcess.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedDuration>>) e).Cache.SetValueExt<FSAppointmentDet.actualDuration>((object) row, (object) row.EstimatedDuration);
    this.X_Duration_FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedQty>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedDuration>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedDuration>>) e).Args, row.EstimatedDuration);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lineType> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsPickupDelivery && ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lineType>>) e).ExternalCall)
      this.X_LineType_FieldUpdated<FSAppointmentDet>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lineType>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lineType>>) e).Args);
    if (!(row.LineType == "PU_DL") || !((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lineType>, FSAppointmentDet, object>) e).OldValue != "PU_DL"))
      return;
    IEnumerable<FSAppointmentDet> source = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((IEnumerable<PXResult<FSAppointmentDet>>) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentDet>>()).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x =>
    {
      if (!(x.LineType == "SERVI"))
        return false;
      int? appDetId = x.AppDetID;
      int num = 0;
      return appDetId.GetValueOrDefault() > num & appDetId.HasValue;
    }));
    if (source.Count<FSAppointmentDet>() != 1)
      return;
    FSAppointmentDet fsAppointmentDet = source.First<FSAppointmentDet>();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lineType>>) e).Cache.SetValueExt<FSAppointmentDet.pickupDeliveryAppLineRef>((object) row, (object) fsAppointmentDet.LineRef);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.SMequipmentID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    this.UpdateWarrantyFlag(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.SMequipmentID>>) e).Cache, (IFSSODetBase) row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.componentID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    this.UpdateWarrantyFlag(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.componentID>>) e).Cache, (IFSSODetBase) row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.equipmentLineRef> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    this.UpdateWarrantyFlag(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.equipmentLineRef>>) e).Cache, (IFSSODetBase) row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate);
    if (row.ComponentID.HasValue)
      return;
    row.ComponentID = SharedFunctions.GetEquipmentComponentID((PXGraph) this, row.SMEquipmentID, row.EquipmentLineRef);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.equipmentAction> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsInventoryItem)
      return;
    SharedFunctions.ResetEquipmentFields(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.equipmentAction>>) e).Cache, (object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lotSerialNbr> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (!row.IsInventoryItem)
      return;
    this.SetUnitCostByLotSerialNbr(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lotSerialNbr>>) e).Cache, row, (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.lotSerialNbr>, FSAppointmentDet, object>) e).OldValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.billingRule> e)
  {
    this.X_BillingRule_FieldUpdated<FSAppointmentDet, FSAppointmentDet.estimatedDuration, FSAppointmentDet.actualDuration, FSAppointmentDet.curyUnitPrice, FSAppointmentDet.isFree>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.billingRule>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.billingRule>>) e).Args, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.uOM> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    this.X_UOM_FieldUpdated<FSAppointmentDet.curyUnitPrice>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.uOM>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.uOM>>) e).Args);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.uOM>>) e).Cache.SetDefaultExt<FSAppointmentDet.curyUnitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.siteID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    this.X_SiteID_FieldUpdated<FSAppointmentDet.curyUnitPrice, FSAppointmentDet.acctID, FSAppointmentDet.subID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Args);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Cache.SetDefaultExt<FSAppointmentDet.curyUnitCost>((object) e.Row);
    if (e.NewValue == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.siteID>, FSAppointmentDet, object>) e).OldValue || string.IsNullOrEmpty(row.PONbr))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.siteID>>) e).Cache.DisplayFieldWarning<FSAppointmentDet.siteID>((object) row, e.NewValue, "Changing the warehouse will not affect the purchase order or the PO receipt.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.curyUnitCost> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    if (current == null || !(current.PostTo == "PM") || !(current.BillingType == "CC"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.curyUnitCost>>) e).Cache.SetDefaultExt<FSAppointmentDet.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsFree.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree>>) e).Cache.SetValueExt<FSAppointmentDet.curyUnitPrice>((object) e.Row, (object) 0M);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree>>) e).Cache.SetValueExt<FSAppointmentDet.curyBillableExtPrice>((object) e.Row, (object) 0M);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree>>) e).Cache.SetValueExt<FSAppointmentDet.discPct>((object) e.Row, (object) 0M);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree>>) e).Cache.SetValueExt<FSAppointmentDet.curyDiscAmt>((object) e.Row, (object) 0M);
      if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree>>) e).ExternalCall)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree>>) e).Cache.SetValueExt<FSAppointmentDet.manualDisc>((object) e.Row, (object) true);
    }
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isFree>>) e).Cache.SetDefaultExt<FSAppointmentDet.curyUnitPrice>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable> e)
  {
    if (e.Row == null)
      return;
    bool? isBillable = e.Row.IsBillable;
    bool? nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>, FSAppointmentDet, object>) e).OldValue;
    if (isBillable.GetValueOrDefault() == nullable.GetValueOrDefault() & isBillable.HasValue == nullable.HasValue)
      return;
    nullable = e.Row.IsBillable;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue || e.Row.BillingRule == "NONE")
    {
      nullable = e.Row.IsCanceledNotPerformed;
      if (nullable.GetValueOrDefault())
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.curyBillableExtPrice>((object) e.Row, (object) 0M);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.discPct>((object) e.Row, (object) 0M);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.curyDiscAmt>((object) e.Row, (object) 0M);
      }
      else
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.isFree>((object) e.Row, (object) true);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.contractRelated>((object) e.Row, (object) false);
    }
    else
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.isFree>((object) e.Row, (object) false);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.manualPrice>((object) e.Row, (object) e.Row.IsExpenseReceiptItem);
      if (!e.Row.IsLinkedItem)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.curyUnitPrice>((object) e.Row, (object) e.Row.CuryUnitCost);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.isBillable>>) e).Cache.SetValueExt<FSAppointmentDet.curyBillableExtPrice>((object) e.Row, (object) e.Row.CuryExtCost);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.effTranQty> e)
  {
    if (e.Row.AreActualFieldsActive.GetValueOrDefault())
    {
      Decimal? actualQty = e.Row.ActualQty;
      Decimal? effTranQty = e.Row.EffTranQty;
      if (actualQty.GetValueOrDefault() == effTranQty.GetValueOrDefault() & actualQty.HasValue == effTranQty.HasValue)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.effTranQty>>) e).Cache.SetValueExt<FSAppointmentDet.actualQty>((object) e.Row, (object) e.Row.EffTranQty);
    }
    else
    {
      Decimal? estimatedQty = e.Row.EstimatedQty;
      Decimal? effTranQty = e.Row.EffTranQty;
      if (estimatedQty.GetValueOrDefault() == effTranQty.GetValueOrDefault() & estimatedQty.HasValue == effTranQty.HasValue)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.effTranQty>>) e).Cache.SetValueExt<FSAppointmentDet.estimatedQty>((object) e.Row, (object) e.Row.EffTranQty);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.enablePO> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.ShouldBeRequestPO)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.enablePO>>) e).Cache.SetValueExt<FSAppointmentDet.status>((object) e.Row, (object) "RP");
    else if (e.Row.ShouldBeWaitingPO)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.enablePO>>) e).Cache.SetValueExt<FSAppointmentDet.status>((object) e.Row, (object) "WP");
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.enablePO>>) e).Cache.SetValueExt<FSAppointmentDet.status>((object) e.Row, (object) "NS");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.pOSource> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.ShouldBeWaitingPO)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.pOSource>>) e).Cache.SetValueExt<FSAppointmentDet.status>((object) e.Row, (object) "WP");
    else if (e.Row.ShouldBeRequestPO)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.pOSource>>) e).Cache.SetValueExt<FSAppointmentDet.status>((object) e.Row, (object) "RP");
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.pOSource>>) e).Cache.SetValueExt<FSAppointmentDet.status>((object) e.Row, (object) "NS");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.linkedDocRefNbr> e)
  {
    if (e.Row == null || !e.Row.IsAPBillItem)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.linkedDocRefNbr>>) e).Cache.SetValueExt<FSAppointmentDet.actualDuration>((object) e.Row, (object) 0);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentDet, FSAppointmentDet.linkedDocRefNbr>>) e).Cache.SetValueExt<FSAppointmentDet.actualQty>((object) e.Row, (object) e.Row.EstimatedQty);
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<FSAppointmentDet.status> e)
  {
    Exception exception = (Exception) (((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<FSAppointmentDet.status>>) e).Exception as PXSetPropertyException);
    if (exception == null)
      return;
    FSAppointmentDet row = (FSAppointmentDet) e.Row;
    ((PX.Data.Events.Event<PXExceptionHandlingEventArgs, PX.Data.Events.ExceptionHandling<FSAppointmentDet.status>>) e).Cache.RaiseExceptionHandling<FSAppointmentDet.uiStatus>((object) row, (object) row.Status, exception);
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<FSAppointmentDet.effTranQty> e)
  {
    Exception exception = (Exception) (((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<FSAppointmentDet.effTranQty>>) e).Exception as PXSetPropertyException);
    if (exception == null)
      return;
    FSAppointmentDet row = (FSAppointmentDet) e.Row;
    if (row.AreActualFieldsActive.GetValueOrDefault())
      ((PX.Data.Events.Event<PXExceptionHandlingEventArgs, PX.Data.Events.ExceptionHandling<FSAppointmentDet.effTranQty>>) e).Cache.RaiseExceptionHandling<FSAppointmentDet.actualQty>((object) row, (object) row.EffTranQty, exception);
    else
      ((PX.Data.Events.Event<PXExceptionHandlingEventArgs, PX.Data.Events.ExceptionHandling<FSAppointmentDet.effTranQty>>) e).Cache.RaiseExceptionHandling<FSAppointmentDet.estimatedQty>((object) row, (object) row.EffTranQty, exception);
  }

  protected virtual PXSetPropertyException GetSetPropertyException<TField>(
    PXCache cache,
    object row,
    Exception currentException)
    where TField : IBqlField
  {
    PXErrorLevel pxErrorLevel = (PXErrorLevel) 4;
    PXFieldState pxFieldState;
    try
    {
      pxFieldState = (PXFieldState) cache.GetStateExt<TField>(row);
    }
    catch
    {
      pxFieldState = (PXFieldState) null;
    }
    if (pxFieldState != null)
      pxErrorLevel = pxFieldState.ErrorLevel == null ? (PXErrorLevel) 2 : pxFieldState.ErrorLevel;
    return new PXSetPropertyException(currentException, pxErrorLevel, currentException.Message, Array.Empty<object>());
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSAppointmentDet> e)
  {
    if (e.Row == null || this.SkipServiceOrderUpdate)
      return;
    FSAppointmentDet row = e.Row;
    row.UIStatus = row.Status;
    using (new PXConnectionScope())
    {
      FSSODet soLine = (FSSODet) null;
      row.CanChangeMarkForPO = new bool?(this.CanChangePOOptions(row, true, ref soLine, typeof (FSAppointmentDet.enablePO).Name, out PXException _));
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointmentDet> e)
  {
    if (e.Row == null || ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current == null && ((PXGraph) this).IsMobile)
      return;
    FSAppointmentDet row = e.Row;
    bool includeIN = this.InventoryItemsAreIncluded();
    FSLineType.SetLineTypeList<FSAppointmentDet.lineType>(((PXSelectBase) this.AppointmentDetails).Cache, (object) row, includeIN, false, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current?.Behavior == "RO", true, false);
    if (!row.IsPickupDelivery)
      this.FSAppointmentDet_RowSelected_PartialHandler(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache, row, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current, ((PXSelectBase<FSServiceContract>) this.BillServiceContractRelated).Current);
    if (row.IsService)
      this.SetRequireSerialWarning(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache, row);
    bool flag1 = false;
    int? nullable1;
    int? nullable2;
    if (!row.IsPickupDelivery)
    {
      int? appDetId = row.AppDetID;
      int num1 = 0;
      int num2;
      if (appDetId.GetValueOrDefault() > num1 & appDetId.HasValue)
      {
        nullable1 = row.InventoryID;
        nullable2 = (int?) ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache.GetValueOriginal<FSAppointmentDet.inventoryID>((object) e.Row);
        num2 = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue ? 1 : 0;
      }
      else
        num2 = 0;
      flag1 = num2 != 0;
    }
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache;
    PXRowSelectedEventArgs args = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Args;
    FSServiceOrder current1 = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current;
    FSSrvOrdType current2 = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    int num3 = flag1 ? 1 : 0;
    FSAppointment current3 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    int num4;
    if (current3 == null)
    {
      num4 = 0;
    }
    else
    {
      bool? notStarted = current3.NotStarted;
      bool flag2 = false;
      num4 = notStarted.GetValueOrDefault() == flag2 & notStarted.HasValue ? 1 : 0;
    }
    this.X_RowSelected<FSAppointmentDet>(cache, args, current1, current2, num3 != 0, num4 != 0);
    this.POCreateVerifyValue(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache, row, row.EnablePO);
    if (((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.PostTo == "PM")
      PXUIFieldAttribute.SetEnabled<FSScheduleDet.equipmentAction>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache, (object) null, false);
    bool flag3 = false;
    List<System.Type> fieldsToIgnore = new List<System.Type>();
    if (row.Status == "CC" || row.Status == "NP")
    {
      fieldsToIgnore.Add(typeof (FSAppointmentDet.uiStatus));
      flag3 = true;
    }
    else if (row.Status == "RP")
    {
      fieldsToIgnore.Add(typeof (FSAppointmentDet.uiStatus));
      bool? nullable3 = row.CanChangeMarkForPO;
      if (nullable3.GetValueOrDefault())
        fieldsToIgnore.Add(typeof (FSAppointmentDet.enablePO));
      nullable3 = row.EnablePO;
      if (nullable3.GetValueOrDefault())
      {
        nullable3 = row.CanChangeMarkForPO;
        if (nullable3.GetValueOrDefault())
        {
          fieldsToIgnore.Add(typeof (FSAppointmentDet.pOSource));
          fieldsToIgnore.Add(typeof (FSAppointmentDet.poVendorID));
          fieldsToIgnore.Add(typeof (FSAppointmentDet.poVendorLocationID));
          fieldsToIgnore.Add(typeof (FSAppointmentDet.curyUnitCost));
          fieldsToIgnore.Add(typeof (FSAppointmentDet.estimatedQty));
        }
      }
      flag3 = true;
    }
    else if (row.IsLinkedItem)
    {
      fieldsToIgnore.Add(typeof (FSAppointmentDet.SMequipmentID));
      fieldsToIgnore.Add(typeof (FSAppointmentDet.newTargetEquipmentLineNbr));
      fieldsToIgnore.Add(typeof (FSAppointmentDet.componentID));
      fieldsToIgnore.Add(typeof (FSAppointmentDet.equipmentLineRef));
      FSAppointment current4 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
      int? nullable4;
      if (current4 == null)
      {
        nullable1 = new int?();
        nullable4 = nullable1;
      }
      else
        nullable4 = current4.ProjectID;
      nullable2 = nullable4;
      int? projectID = nullable2 ?? row.ProjectID;
      if (!projectID.HasValue || ProjectDefaultAttribute.IsNonProject(projectID))
        fieldsToIgnore.Add(typeof (FSAppointmentDet.isBillable));
      if (row.IsBillable.GetValueOrDefault())
      {
        fieldsToIgnore.Add(typeof (FSAppointmentDet.curyUnitPrice));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.manualPrice));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.manualDisc));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.curyBillableExtPrice));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.curyExtCost));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.discPct));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.curyDiscAmt));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.isFree));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.taxCategoryID));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.acctID));
        fieldsToIgnore.Add(typeof (FSAppointmentDet.subID));
      }
      flag3 = true;
    }
    if (flag3)
      this.DisableAllDACFields(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache, (object) row, fieldsToIgnore);
    if (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.Status == "C")
      PXUIFieldAttribute.SetEnabled<FSAppointmentDet.enablePO>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache, (object) null, false);
    this.SetItemLineUIStatusList(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentDet>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSAppointmentDet> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (row.LineRef == null)
      row.LineRef = row.LineNbr.Value.ToString("0000");
    object obj;
    if ((obj = ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSAppointmentDet>>) e).Cache.Locate((object) e.Row)) != null && ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSAppointmentDet>>) e).Cache.GetStatus(obj) == 3)
      ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSAppointmentDet>>) e).Cache.SetValue<FSAppointmentDet.appDetID>((object) e.Row, ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSAppointmentDet>>) e).Cache.GetValue<FSAppointmentDet.appDetID>(obj));
    if (row.SODetID.HasValue || !row.IsPickupDelivery)
      return;
    PXResultset<FSAppointmentDet> pxResultset = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>());
    if (pxResultset.Count != 1)
      return;
    FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResultset[0]);
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSAppointmentDet>>) e).Cache.SetValueExt<FSAppointmentDet.sODetID>((object) row, (object) fsAppointmentDet.SODetID);
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAppointmentDet> e)
  {
    if (e.Row == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    FSAppointmentDet row = e.Row;
    row.UIStatus = row.Status;
    if (row.SMEquipmentID.HasValue)
      this.UpdateWarrantyFlag(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSAppointmentDet>>) e).Cache, (IFSSODetBase) row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate);
    if (row.IsPickupDelivery)
      return;
    this.MarkHeaderAsUpdated(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSAppointmentDet>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSAppointmentDet> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    if (row.IsInventoryItem)
      EquipmentHelper.CheckReplaceComponentLines<FSAppointmentDet, FSAppointmentDet.equipmentLineRef>(((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<FSAppointmentDet>>) e).Cache, ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()), (IFSSODetBase) e.NewRow);
    if (e.NewRow.Status == "CC" && row.EnablePO.GetValueOrDefault() && (!string.IsNullOrEmpty(row.PONbr) || !string.IsNullOrEmpty(row.POType)))
      throw new PXException("The selected line is associated with a purchase order and cannot be canceled directly. To cancel this line, first cancel or delete the corresponding line in the related purchase order.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAppointmentDet> e)
  {
    if (e.Row == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    FSAppointmentDet fsAppointmentDetRow = e.Row;
    FSAppointmentDet oldRow = e.OldRow;
    FSAppointment current = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current;
    fsAppointmentDetRow.UIStatus = fsAppointmentDetRow.Status;
    this.MarkHeaderAsUpdated(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache, (object) e.Row);
    if (((PXGraph) this).IsCopyPasteContext && (e.Row.LinkedEntityType == "AP" || e.Row.LinkedEntityType == "ER"))
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache.Delete((object) e.Row);
    }
    else
    {
      if (current.NotStarted.GetValueOrDefault())
        this.CheckIfManualPrice<FSAppointmentDet, FSAppointmentDet.estimatedQty>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Args);
      else
        this.CheckIfManualPrice<FSAppointmentDet, FSAppointmentDet.actualQty>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Args);
      this.CheckSOIfManualCost(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Args);
      if (e.ExternalCall)
      {
        int? staffId = (int?) fsAppointmentDetRow?.StaffID;
        int? nullable = (int?) oldRow?.StaffID;
        if (!(staffId.GetValueOrDefault() == nullable.GetValueOrDefault() & staffId.HasValue == nullable.HasValue))
        {
          PXCache cache = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache;
          FSAppointmentDet fsAppointmentDetRow1 = fsAppointmentDetRow;
          AppointmentEntry.AppointmentServiceEmployees_View serviceEmployees = this.AppointmentServiceEmployees;
          int? oldStaffID;
          if (oldRow == null)
          {
            nullable = new int?();
            oldStaffID = nullable;
          }
          else
            oldStaffID = oldRow.StaffID;
          this.InsertUpdateDelete_AppointmentDetService_StaffID(cache, fsAppointmentDetRow1, serviceEmployees, oldStaffID);
        }
      }
      if (fsAppointmentDetRow.IsInventoryItem || fsAppointmentDetRow.IsPickupDelivery)
        this.VerifyIsAlreadyPosted<FSAppointmentDet.inventoryID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache, fsAppointmentDetRow, ((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current);
      if (!current.IsINReleaseProcess && (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache.ObjectsEqual<FSAppointmentDet.curyUnitCost>((object) e.Row, (object) e.OldRow) || !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentDet>>) e).Cache.ObjectsEqual<FSAppointmentDet.curyExtCost>((object) e.Row, (object) e.OldRow)))
      {
        foreach (PXResult<FSApptLineSplit> pxResult in ((PXSelectBase<FSApptLineSplit>) this.Splits).Select(Array.Empty<object>()))
        {
          FSApptLineSplit fsApptLineSplit = PXResult<FSApptLineSplit>.op_Implicit(pxResult);
          ((PXSelectBase) this.Splits).Cache.SetValueExt<FSApptLineSplit.curyUnitCost>((object) fsApptLineSplit, (object) e.Row.CuryUnitCost);
          ((PXSelectBase) this.Splits).Cache.SetValueExt<FSApptLineSplit.curyExtCost>((object) fsApptLineSplit, (object) e.Row.CuryExtCost);
        }
      }
      if (!(fsAppointmentDetRow.Status != oldRow.Status) || !(fsAppointmentDetRow.Status == "NF") || !fsAppointmentDetRow.IsService)
        return;
      DateTime? nullable1 = PXDBDateAndTimeAttribute.CombineDateTime(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ExecutionDate, new DateTime?(PXTimeZoneInfo.Now));
      foreach (FSAppointmentLog logRow in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) ((IEnumerable<PXResult<FSAppointmentLog>>) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentLog>>()).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (y => y.DetLineRef == fsAppointmentDetRow.LineRef && y.Status == "P")))
        this.ChangeLogAndRelatedItemLinesStatus(logRow, "C", nullable1.Value, (string) null, (List<FSAppointmentDet>) null);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSAppointmentDet> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet fsAppointmentDetRow = e.Row;
    if (e.ExternalCall && fsAppointmentDetRow.IsAPBillItem && ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Ask("This line is associated with an AP bill. Do you want to delete the line?", (MessageButtons) 1) != 1)
      e.Cancel = true;
    if (!fsAppointmentDetRow.IsService)
      return;
    if (!this.IsAppointmentBeingDeleted(fsAppointmentDetRow.AppointmentID, ((PXSelectBase) this.AppointmentRecords).Cache) && this.ServiceLinkedToPickupDeliveryItem((PXGraph) this, fsAppointmentDetRow, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current))
      throw new PXException("This service is related to at least one picked up or delivered item. Delete picked up or delivered items before you delete the service.");
    foreach (PXResult<FSAppointmentEmployee> pxResult in ((IEnumerable<PXResult<FSAppointmentEmployee>>) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentEmployee>>().Where<PXResult<FSAppointmentEmployee>>((Func<PXResult<FSAppointmentEmployee>, bool>) (y => PXResult<FSAppointmentEmployee>.op_Implicit(y).ServiceLineRef == fsAppointmentDetRow.LineRef)))
      ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Delete(PXResult<FSAppointmentEmployee>.op_Implicit(pxResult));
    foreach (PXResult<FSAppointmentLog> pxResult in ((IEnumerable<PXResult<FSAppointmentLog>>) ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentLog>>().Where<PXResult<FSAppointmentLog>>((Func<PXResult<FSAppointmentLog>, bool>) (y => PXResult<FSAppointmentLog>.op_Implicit(y).DetLineRef == fsAppointmentDetRow.LineRef)))
    {
      FSAppointmentLog fsAppointmentLog = PXResult<FSAppointmentLog>.op_Implicit(pxResult);
      if (fsAppointmentLog.BAccountID.HasValue)
      {
        fsAppointmentLog.DetLineRef = (string) null;
        ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Update(fsAppointmentLog);
      }
      else
        ((PXSelectBase<FSAppointmentLog>) this.LogRecords).Delete(fsAppointmentLog);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAppointmentDet> e)
  {
    if (e.Row == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null || e.Row.IsPickupDelivery)
      return;
    this.MarkHeaderAsUpdated(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSAppointmentDet>>) e).Cache, (object) e.Row);
    this.ClearTaxes(((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSAppointmentDet> e)
  {
    if (e.Row == null || ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current == null)
      return;
    FSAppointmentDet row = e.Row;
    if (row.IsInventoryItem || row.IsPickupDelivery)
      this.VerifyIsAlreadyPosted<FSAppointmentDet.inventoryID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, row, ((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current);
    if (!row.IsPickupDelivery)
    {
      this.BackupOriginalValues(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Args);
      if (!this.UpdateServiceOrder(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, this, (object) e.Row, e.Operation, new PXTranStatus?()))
        return;
      FSSODet fssoDet;
      if (this.ApptLinesWithSrvOrdLineUpdated != null && this.ApptLinesWithSrvOrdLineUpdated.TryGetValue(row, out fssoDet))
      {
        row.SODetID = fssoDet.SODetID;
        row.OrigSrvOrdNbr = fssoDet.RefNbr;
        row.OrigLineNbr = fssoDet.LineNbr;
      }
      this.FSAppointmentDet_RowPersisting_PartialHandler(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
      if (row.IsInventoryItem)
      {
        string empty = string.Empty;
        if (e.Operation != 3 && !SharedFunctions.AreEquipmentFieldsValid(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, row.InventoryID, row.SMEquipmentID, (object) row.NewTargetEquipmentLineNbr, row.EquipmentAction, ref empty))
          ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache.RaiseExceptionHandling<FSAppointmentDet.equipmentAction>((object) row, (object) row.EquipmentAction, (Exception) new PXSetPropertyException(empty));
        if (!EquipmentHelper.CheckReplaceComponentLines<FSAppointmentDet, FSAppointmentDet.equipmentLineRef>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>()), (IFSSODetBase) row))
          return;
      }
      if (e.Operation == 2)
        SharedFunctions.CopyNotesAndFiles(((PXGraph) this).Caches[typeof (FSSODet)], ((PXSelectBase) this.AppointmentDetails).Cache, (object) row.FSSODetRow, (object) row, (bool?) ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected)?.Current?.CopyNotesToAppoinment, (bool?) ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected)?.Current?.CopyAttachmentsToAppoinment);
    }
    this.VerifySrvOrdLineQty(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, e.Row, (object) e.Row.EffTranQty, typeof (FSAppointmentDet.effTranQty), false);
    this.X_SetPersistingCheck<FSAppointmentDet>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Args, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current);
    if (e.Operation == 3)
      return;
    this.ValidateItemLineStatus(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentDet>>) e).Cache, e.Row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSAppointmentDet> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSAppointmentDet>>) e).Cache;
    if (((PXGraph) this).Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("EP301020") && ((PXGraph) this).Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("EP301000") && e.TranStatus == 1 && e.Operation == 3 && ((PXSelectBase) this.AppointmentRecords).Cache.GetStatus((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current) != 3 && row.IsExpenseReceiptItem)
      this.GetServiceOrderEntryGraph(false).ClearFSDocExpenseReceipts(row.LinkedDocRefNbr);
    if (row.IsPickupDelivery)
      return;
    this.RestoreOriginalValues(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSAppointmentDet>>) e).Cache, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSAppointmentDet>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSApptLineSplit, FSApptLineSplit.curyExtCost> e)
  {
    if (e.Row == null)
      return;
    FSApptLineSplit row = e.Row;
    PX.Data.Events.FieldDefaulting<FSApptLineSplit, FSApptLineSplit.curyExtCost> fieldDefaulting = e;
    Decimal? curyUnitCost = row.CuryUnitCost;
    Decimal? qty = row.Qty;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (curyUnitCost.HasValue & qty.HasValue ? new Decimal?(curyUnitCost.GetValueOrDefault() * qty.GetValueOrDefault()) : new Decimal?());
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSApptLineSplit, FSApptLineSplit.curyExtCost>, FSApptLineSplit, object>) fieldDefaulting).NewValue = (object) local;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSApptLineSplit, FSApptLineSplit.curyExtCost>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSApptLineSplit> e)
  {
    this.MarkApptLineAsUpdated(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSApptLineSplit>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSApptLineSplit> e)
  {
    this.MarkApptLineAsUpdated(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSApptLineSplit>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSApptLineSplit> e)
  {
    this.MarkApptLineAsUpdated(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<FSApptLineSplit>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSApptLineSplit> e)
  {
    if (e.Row == null || ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current == null)
      return;
    FSApptLineSplit row = e.Row;
    FSSODetSplit fssoDetSplit;
    if (!this.UpdateServiceOrder(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, this, (object) e.Row, e.Operation, new PXTranStatus?()) || this.ApptSplitsWithSrvOrdSplitUpdated == null || !this.ApptSplitsWithSrvOrdSplitUpdated.TryGetValue(row, out fssoDetSplit))
      return;
    row.OrigSrvOrdType = fssoDetSplit.SrvOrdType;
    row.OrigSrvOrdNbr = fssoDetSplit.RefNbr;
    row.OrigLineNbr = fssoDetSplit.LineNbr;
    row.OrigSplitLineNbr = fssoDetSplit.SplitLineNbr;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSPostDet> e)
  {
    if (e.Row == null)
      return;
    FSPostDet row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSPostDet>>) e).Cache;
    bool? nullable = row.SOPosted;
    if (nullable.GetValueOrDefault())
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
      nullable = row.ARPosted;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.SOInvPosted;
        if (!nullable.GetValueOrDefault())
        {
          nullable = row.APPosted;
          if (nullable.GetValueOrDefault())
          {
            row.InvoiceRefNbr = row.Mem_DocNbr;
            row.InvoiceDocType = row.APDocType;
            goto label_12;
          }
          goto label_12;
        }
      }
      row.InvoiceRefNbr = row.Mem_DocNbr;
      row.InvoiceDocType = row.ARDocType;
    }
label_12:
    using (new PXConnectionScope())
    {
      FSPostBatch fsPostBatch = PXResultset<FSPostBatch>.op_Implicit(PXSelectBase<FSPostBatch, PXSelect<FSPostBatch, Where<FSPostBatch.batchID, Equal<Required<FSPostBatch.batchID>>>>.Config>.Select(cache.Graph, new object[1]
      {
        (object) row.BatchID
      }));
      row.BatchNbr = fsPostBatch?.BatchNbr;
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

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.unitCost> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentLog row = e.Row;
    Decimal? laborCost = this.CalculateLaborCost(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.unitCost>>) e).Cache, row, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
    if (!laborCost.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.unitCost>, FSAppointmentLog, object>) e).NewValue = (object) laborCost;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.unitCost>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyUnitCost> e)
  {
    if (e.Row == null)
      return;
    object obj;
    ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyUnitCost>>) e).Cache.RaiseFieldDefaulting<FSAppointmentLog.unitCost>((object) e.Row, ref obj);
    if (obj == null || !((Decimal) obj != 0M))
      return;
    Decimal curyval = (Decimal) obj;
    PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyUnitCost>>) e).Cache, (object) e.Row, curyval, out curyval, CommonSetupDecPl.PrcCst);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyUnitCost>, FSAppointmentLog, object>) e).NewValue = (object) curyval;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyUnitCost>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.trackOnService> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.trackOnService>, FSAppointmentLog, object>) e).NewValue = (object) (e.Row.DetLineRef != null);
    if (!e.Row.Travel.GetValueOrDefault() || !e.Row.BAccountID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.trackOnService>, FSAppointmentLog, object>) e).NewValue = (object) (bool) (!(bool) ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.trackOnService>, FSAppointmentLog, object>) e).NewValue ? 0 : (GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ =>
    {
      int? employeeId = _.EmployeeID;
      int? baccountId = e.Row.BAccountID;
      return employeeId.GetValueOrDefault() == baccountId.GetValueOrDefault() & employeeId.HasValue == baccountId.HasValue && _.PrimaryDriver.GetValueOrDefault();
    })).Any<FSAppointmentEmployee>() ? 1 : 0));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.descr> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.DetLineRef != null)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.descr>, FSAppointmentLog, object>) e).NewValue = (object) e.Row.Descr;
    }
    else
    {
      if (!e.Row.Travel.GetValueOrDefault())
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.descr>, FSAppointmentLog, object>) e).NewValue = (object) PXMessages.LocalizeNoPrefix("Travel");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.projectID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.projectID>, FSAppointmentLog, object>) e).NewValue != null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.projectID>, FSAppointmentLog, object>) e).NewValue = (object) (int?) ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current?.ProjectID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.billableQty> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.billableQty>, FSAppointmentLog, object>) e).NewValue = (object) PXDBQuantityAttribute.Round(new Decimal?(Decimal.Divide((Decimal) e.Row.BillableTimeDuration.GetValueOrDefault(), 60M)));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.extCost> e)
  {
    if (e.Row == null)
      return;
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.extCost> fieldDefaulting = e;
    Decimal num = PXDBQuantityAttribute.Round(new Decimal?(Decimal.Divide((Decimal) e.Row.TimeDuration.GetValueOrDefault(), 60M)));
    Decimal? unitCost = e.Row.UnitCost;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (unitCost.HasValue ? new Decimal?(num * unitCost.GetValueOrDefault()) : new Decimal?());
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.extCost>, FSAppointmentLog, object>) fieldDefaulting).NewValue = (object) local;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyExtCost> e)
  {
    if (e.Row == null)
      return;
    PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyExtCost> fieldDefaulting = e;
    Decimal num = PXDBQuantityAttribute.Round(new Decimal?(Decimal.Divide((Decimal) e.Row.TimeDuration.GetValueOrDefault(), 60M)));
    Decimal? curyUnitCost = e.Row.CuryUnitCost;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (curyUnitCost.HasValue ? new Decimal?(num * curyUnitCost.GetValueOrDefault()) : new Decimal?());
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointmentLog, FSAppointmentLog.curyExtCost>, FSAppointmentLog, object>) fieldDefaulting).NewValue = (object) local;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSAppointmentLog, FSAppointmentLog.trackTime> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.BAccountID.HasValue)
    {
      if (SharedFunctions.GetBAccountType((PXGraph) this, e.Row.BAccountID) == "EP")
        return;
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentLog, FSAppointmentLog.trackTime>>) e).NewValue = (object) false;
    }
    else
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSAppointmentLog, FSAppointmentLog.trackTime>>) e).NewValue = (object) false;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.detLineRef> e)
  {
    if (e.Row == null || !((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.detLineRef>, FSAppointmentLog, object>) e).NewValue != e.Row.DetLineRef))
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.detLineRef>>) e).Cache, e.Row, "detLineRef");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.dateTimeBegin> e)
  {
    if (e.Row == null)
      return;
    DateTime? newValue = (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.dateTimeBegin>, FSAppointmentLog, object>) e).NewValue;
    DateTime? dateTimeBegin = e.Row.DateTimeBegin;
    if ((newValue.HasValue == dateTimeBegin.HasValue ? (newValue.HasValue ? (newValue.GetValueOrDefault() != dateTimeBegin.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.dateTimeBegin>>) e).Cache, e.Row, "dateTimeBegin");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.dateTimeEnd> e)
  {
    if (e.Row == null)
      return;
    DateTime? newValue = (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.dateTimeEnd>, FSAppointmentLog, object>) e).NewValue;
    DateTime? dateTimeEnd = e.Row.DateTimeEnd;
    if ((newValue.HasValue == dateTimeEnd.HasValue ? (newValue.HasValue ? (newValue.GetValueOrDefault() != dateTimeEnd.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.dateTimeEnd>>) e).Cache, e.Row, "dateTimeEnd");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration> e)
  {
    if (e.Row == null)
      return;
    int? newValue1 = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration>, FSAppointmentLog, object>) e).NewValue;
    int? timeDuration = e.Row.TimeDuration;
    if (newValue1.GetValueOrDefault() == timeDuration.GetValueOrDefault() & newValue1.HasValue == timeDuration.HasValue)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration>>) e).Cache, e.Row, "timeDuration");
    int result;
    int? nullable1 = new int?(!(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration>, FSAppointmentLog, object>) e).NewValue is int newValue2) ? (!(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration>, FSAppointmentLog, object>) e).NewValue is string newValue3) || !int.TryParse(newValue3, out result) ? 0 : result) : newValue2);
    if (!(e.Row.Status == "C") || !((string) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration>>) e).Cache.GetValueOriginal<FSAppointmentLog.status>((object) e.Row) == "C") || !nullable1.HasValue)
      return;
    int? nullable2 = nullable1;
    int num = 1440;
    if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration>, FSAppointmentLog, object>) e).NewValue = (object) nullable1.ToString();
      throw new PXSetPropertyException("Time duration cannot be greater than 24 hours once the log line is assigned the Completed status. Insert a new log line and assign it to the corresponding date.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FSAppointmentLog.timeDuration>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.timeDuration>>) e).Cache)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.earningType> e)
  {
    if (e.Row == null || !((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.earningType>, FSAppointmentLog, object>) e).NewValue != e.Row.EarningType))
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.earningType>>) e).Cache, e.Row, "earningType");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.costCodeID> e)
  {
    if (e.Row == null)
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.costCodeID>, FSAppointmentLog, object>) e).NewValue;
    int? costCodeId = e.Row.CostCodeID;
    if (newValue.GetValueOrDefault() == costCodeId.GetValueOrDefault() & newValue.HasValue == costCodeId.HasValue)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.costCodeID>>) e).Cache, e.Row, "costCodeID");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.laborItemID> e)
  {
    if (e.Row == null)
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.laborItemID>, FSAppointmentLog, object>) e).NewValue;
    int? laborItemId = e.Row.LaborItemID;
    if (newValue.GetValueOrDefault() == laborItemId.GetValueOrDefault() & newValue.HasValue == laborItemId.HasValue)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.laborItemID>>) e).Cache, e.Row, "laborItemID");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.trackTime> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.trackTime>, FSAppointmentLog, object>) e).NewValue;
    bool? trackTime = e.Row.TrackTime;
    if (newValue.GetValueOrDefault() == trackTime.GetValueOrDefault() & newValue.HasValue == trackTime.HasValue)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.trackTime>>) e).Cache, e.Row, "trackTime");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.isBillable> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.isBillable>, FSAppointmentLog, object>) e).NewValue;
    bool? isBillable = e.Row.IsBillable;
    if (newValue.GetValueOrDefault() == isBillable.GetValueOrDefault() & newValue.HasValue == isBillable.HasValue)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.isBillable>>) e).Cache, e.Row, "isBillable");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.workgroupID> e)
  {
    if (e.Row == null)
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.workgroupID>, FSAppointmentLog, object>) e).NewValue;
    int? workgroupId = e.Row.WorkgroupID;
    if (newValue.GetValueOrDefault() == workgroupId.GetValueOrDefault() & newValue.HasValue == workgroupId.HasValue)
      return;
    this.VerifyTimeActivityUpdate(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSAppointmentLog, FSAppointmentLog.workgroupID>>) e).Cache, e.Row, "workgroupID");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.bAccountID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.bAccountID>>) e).Cache.SetValueExt<FSAppointmentLog.bAccountType>((object) e.Row, (object) SharedFunctions.GetBAccountType((PXGraph) this, e.Row.BAccountID));
    this.SetLogInfoFromDetails(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.bAccountID>>) e).Cache, e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.bAccountID>>) e).Cache.SetDefaultExt<FSAppointmentLog.earningType>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.bAccountID>>) e).Cache.SetDefaultExt<FSAppointmentLog.trackTime>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.timeDuration> e)
  {
    if (e.Row == null)
      return;
    int? timeDuration = e.Row.TimeDuration;
    int? oldValue = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.timeDuration>, FSAppointmentLog, object>) e).OldValue;
    if (timeDuration.GetValueOrDefault() == oldValue.GetValueOrDefault() & timeDuration.HasValue == oldValue.HasValue)
      return;
    int num = 0;
    if (e.Row.TimeDuration.HasValue)
      num = e.Row.TimeDuration.Value;
    if (num >= 0)
    {
      DateTime? nullable = e.Row.DateTimeBegin;
      if (!nullable.HasValue)
        return;
      if (num <= 0)
      {
        nullable = e.Row.DateTimeEnd;
        if (!nullable.HasValue)
          return;
      }
      nullable = e.Row.DateTimeBegin;
      DateTime dateTime1 = nullable.Value;
      DateTime dateTime2 = dateTime1.AddMinutes((double) num);
      nullable = e.Row.DateTimeEnd;
      dateTime1 = dateTime2;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() != dateTime1 ? 1 : 0) : 1) == 0)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.timeDuration>>) e).Cache.SetValueExt<FSAppointmentLog.dateTimeEnd>((object) e.Row, (object) dateTime2);
    }
    else if (e.Row.TrackTime.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.timeDuration>>) e).Cache.SetValue<FSAppointmentLog.status>((object) e.Row, (object) "C");
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.timeDuration>>) e).Cache.SetValue<FSAppointmentLog.dateTimeEnd>((object) e.Row, (object) null);
    }
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.timeDuration>>) e).Cache.SetValueExt<FSAppointmentLog.timeDuration>((object) e.Row, (object) (num * -1));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.detLineRef> e)
  {
    if (e.Row == null)
      return;
    this.SetLogInfoFromDetails(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.detLineRef>>) e).Cache, e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.detLineRef>>) e).Cache.SetDefaultExt<FSAppointmentLog.earningType>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.detLineRef>>) e).Cache.SetDefaultExt<FSAppointmentLog.trackTime>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.laborItemID> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentLog row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.laborItemID>>) e).Cache.SetDefaultExt<FSAppointmentLog.curyUnitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.travel> e)
  {
    if (e.Row == null)
      return;
    FSAppointmentDet apptDet = (FSAppointmentDet) null;
    if (!string.IsNullOrWhiteSpace(e.Row.DetLineRef))
    {
      apptDet = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r =>
      {
        if (!(r.LineRef == e.Row.DetLineRef))
          return false;
        bool? isTravelItem = r.IsTravelItem;
        bool? travel = e.Row.Travel;
        return isTravelItem.GetValueOrDefault() == travel.GetValueOrDefault() & isTravelItem.HasValue == travel.HasValue;
      })).FirstOrDefault<FSAppointmentDet>();
      if (apptDet == null)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.travel>>) e).Cache.SetValueExt<FSAppointmentLog.detLineRef>((object) e.Row, (object) null);
    }
    string str;
    if (e.Row.Travel.GetValueOrDefault())
    {
      str = "TR";
    }
    else
    {
      str = this.GetLogTypeCheckingTravelWithLogFormula(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.travel>>) e).Cache, apptDet);
      if (str == "TR")
        str = "SA";
    }
    if (str != e.Row.ItemType)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.travel>>) e).Cache.SetValueExt<FSAppointmentLog.itemType>((object) e.Row, (object) str);
    bool? travel1 = e.Row.Travel;
    bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.travel>, FSAppointmentLog, object>) e).OldValue;
    if (travel1.GetValueOrDefault() == oldValue.GetValueOrDefault() & travel1.HasValue == oldValue.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.travel>>) e).Cache.SetDefaultExt<FSAppointmentLog.descr>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.trackTime> e)
  {
    if (e.Row == null)
      return;
    bool? trackTime = e.Row.TrackTime;
    bool? nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.trackTime>, FSAppointmentLog, object>) e).OldValue;
    if (trackTime.GetValueOrDefault() == nullable.GetValueOrDefault() & trackTime.HasValue == nullable.HasValue)
      return;
    nullable = e.Row.TrackTime;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.trackTime>, FSAppointmentLog, object>) e).OldValue;
      if (nullable.GetValueOrDefault() && ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current != null)
        ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.TrackTimeChanged = new bool?(true);
    }
    nullable = e.Row.TrackTime;
    bool flag2 = false;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      return;
    int? timeDuration = e.Row.TimeDuration;
    int num = 0;
    if (!(timeDuration.GetValueOrDefault() < num & timeDuration.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppointmentLog, FSAppointmentLog.trackTime>>) e).Cache.SetValueExt<FSAppointmentLog.timeDuration>((object) e.Row, (object) 0);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSAppointmentLog> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSAppointmentLog> e)
  {
    if (e.Row == null)
      return;
    this.EnableDisable_TimeRelatedLogFields(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentLog>>) e).Cache, e.Row, ((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current);
    this.ValidateLogDateTime(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentLog>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSAppointmentLog> e)
  {
    FSAppointmentLog row = e.Row;
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSAppointmentLog> e)
  {
    if (e.Row == null)
      return;
    this.OnRowInsertedFSAppointmentLog(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSAppointmentLog> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSAppointmentLog> e)
  {
    this.MarkHeaderAsUpdated(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache, (object) e.Row);
    if (e.OldRow != null && e.Row != null && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache.ObjectsEqual<FSAppointmentLog.detLineRef>((object) e.Row, (object) e.OldRow) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache.ObjectsEqual<FSAppointmentLog.timeDuration>((object) e.Row, (object) e.OldRow) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache.ObjectsEqual<FSAppointmentLog.trackOnService>((object) e.Row, (object) e.OldRow) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache.ObjectsEqual<FSAppointmentLog.status>((object) e.Row, (object) e.OldRow))
      return;
    if ((e.ExternalCall || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache.Graph.IsImport) && ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current.AllowManualLogTimeEdition.GetValueOrDefault() && (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache.ObjectsEqual<FSAppointmentLog.timeDuration>((object) e.Row, (object) e.OldRow) || !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSAppointmentLog>>) e).Cache.ObjectsEqual<FSAppointmentLog.dateTimeEnd>((object) e.Row, (object) e.OldRow)))
      e.Row.KeepDateTimes = new bool?(true);
    this.OnRowDeletedFSAppointmentLog(e.OldRow);
    this.OnRowInsertedFSAppointmentLog(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSAppointmentLog> e)
  {
    if (e.Row == null || !e.Row.BAccountID.HasValue || !e.ExternalCall)
      return;
    PX.Objects.CR.Standalone.EPEmployee tmEmployee = this.FindTMEmployee((PXGraph) this, e.Row.BAccountID);
    EPActivityApprove epActivityApprove = this.FindEPActivityApprove((PXGraph) this, e.Row, tmEmployee);
    if (epActivityApprove == null || !this.ValidateInsertUpdateTimeActivity(epActivityApprove) || ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Ask("At least one time activity related to this log line has been generated. If you delete the log line, the related time activity will also be deleted.", (MessageButtons) 1) == 1)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSAppointmentLog> e)
  {
    if (e.Row == null)
      return;
    this.OnRowDeletedFSAppointmentLog(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSAppointmentLog> e)
  {
    if (e.Row == null)
      return;
    FSAppointment current1 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    if (e.Operation != 3)
    {
      this.ValidateLogStatus(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentLog>>) e).Cache, e.Row, current1);
      this.ValidateLogDateTime(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentLog>>) e).Cache, e.Row);
    }
    if (e.Operation != 1 || !this.SkipTimeCardUpdate)
      return;
    FSAppointmentLog original = (FSAppointmentLog) ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentLog>>) e).Cache.GetOriginal((object) e.Row);
    if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSAppointmentLog>>) e).Cache.ObjectsEqual<FSAppointmentLog.earningType, FSAppointmentLog.timeDuration, FSAppointmentLog.isBillable, FSAppointmentLog.billableTimeDuration>((object) e.Row, (object) original))
      return;
    FSAppointment current2 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current;
    if (current2 != null && current2.Status == "Z")
      throw new PXException("The appointment {0}-{1} cannot be modified because it is closed.", new object[2]
      {
        (object) current2.SrvOrdType,
        (object) current2.RefNbr
      });
    if (current2 != null && current2.IsPosted.GetValueOrDefault())
      throw new PXException("The appointment {0}-{1} cannot be modified because it has already been billed.", new object[2]
      {
        (object) current2.SrvOrdType,
        (object) current2.RefNbr
      });
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSAppointmentLog> e)
  {
    if (e.Row == null || e.TranStatus != null || this.SkipTimeCardUpdate)
      return;
    this.InsertUpdateDeleteTimeActivities(((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current, ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current, e.Row, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSAppointmentLog>>) e).Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSLogActionFilter, FSLogActionFilter.logDateTime> e)
  {
    if (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSLogActionFilter, FSLogActionFilter.logDateTime>, FSLogActionFilter, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(((PXGraph) this).Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSLogActionFilter> e)
  {
    if (e.Row == null)
      return;
    this.SetVisibleCompletePauseLogActionGrid(e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSLogActionFilter.action> e)
  {
    if (!(e.Row is FSLogActionFilter row))
      return;
    this.SetVisibleCompletePauseLogActionGrid(row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSLogActionFilter> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      bool? headerBasedOnLog = current.SetTimeInHeaderBasedOnLog;
      bool flag = false;
      num = headerBasedOnLog.GetValueOrDefault() == flag & headerBasedOnLog.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    DateTime? nullable1;
    DateTime? nullable2;
    if (e.Row.Type != "TR" && e.Row.Action == "ST")
    {
      nullable1 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ActualDateTimeBegin;
      nullable2 = e.Row.LogDateTime;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSLogActionFilter>>) e).Cache.RaiseExceptionHandling<FSLogActionFilter.logDateTime>((object) e.Row, (object) e.Row.LogDateTime, (Exception) new PXException("The log start date and time is earlier than the appointment actual start date and time specified on the Settings tab."));
    }
    if (!(e.Row.Type != "TR") || !(e.Row.Action == "CP"))
      return;
    nullable2 = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ActualDateTimeEnd;
    nullable1 = e.Row.LogDateTime;
    if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSLogActionFilter>>) e).Cache.RaiseExceptionHandling<FSLogActionFilter.logDateTime>((object) e.Row, (object) e.Row.LogDateTime, (Exception) new PXException("The log end date and time is later than the appointment actual end date and time specified on the Settings tab."));
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSLogActionPCRFilter> e)
  {
    if (e.Row == null)
      return;
    FSLogActionPCRFilter row = e.Row;
    this.VerifyBeforeAction(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLogActionPCRFilter>>) e).Cache, (FSLogActionFilter) e.Row, row.Action, row.Type);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSLogActionPCRFilter> e)
  {
    if (e.Row == null)
      return;
    this.UpdateLogActionFilter((FSLogActionFilter) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSLogActionStartFilter> e)
  {
    if (e.Row == null)
      return;
    FSLogActionStartFilter row = e.Row;
    this.VerifyBeforeAction(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLogActionStartFilter>>) e).Cache, (FSLogActionFilter) e.Row, row.Action, row.Type);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSLogActionStartFilter> e)
  {
    if (e.Row == null)
      return;
    this.UpdateLogActionFilter((FSLogActionFilter) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<FSLogActionStartServiceFilter> e)
  {
    if (e.Row == null)
      return;
    FSLogActionStartServiceFilter row = e.Row;
    this.VerifyBeforeAction(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLogActionStartServiceFilter>>) e).Cache, (FSLogActionFilter) e.Row, row.Action, row.Type);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSLogActionStartServiceFilter> e)
  {
    if (e.Row == null)
      return;
    this.UpdateLogActionFilter((FSLogActionFilter) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSLogActionStartStaffFilter> e)
  {
    if (e.Row == null)
      return;
    FSLogActionStartStaffFilter row = e.Row;
    this.VerifyBeforeAction(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLogActionStartStaffFilter>>) e).Cache, (FSLogActionFilter) e.Row, row.Action, row.Type);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSLogActionStartStaffFilter> e)
  {
    if (e.Row == null)
      return;
    this.UpdateLogActionFilter((FSLogActionFilter) e.Row);
  }

  public virtual void UpdateLogActionFilter(FSLogActionFilter newRow)
  {
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current == null)
      ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current = (FSLogActionFilter) ((PXSelectBase) this.LogActionFilter).Cache.CreateInstance();
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Type != newRow.Type)
      ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).SetValueExt<FSLogActionFilter.type>(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current, (object) newRow.Type);
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Action != newRow.Action)
      ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).SetValueExt<FSLogActionFilter.action>(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current, (object) newRow.Action);
    bool? me1 = ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.Me;
    bool? me2 = newRow.Me;
    if (!(me1.GetValueOrDefault() == me2.GetValueOrDefault() & me1.HasValue == me2.HasValue))
      ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).SetValueExt<FSLogActionFilter.me>(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current, (object) newRow.Me);
    if (((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.DetLineRef != newRow.DetLineRef)
      ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).SetValueExt<FSLogActionFilter.detLineRef>(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current, (object) newRow.DetLineRef);
    DateTime? logDateTime1 = ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current.LogDateTime;
    DateTime? logDateTime2 = newRow.LogDateTime;
    if ((logDateTime1.HasValue == logDateTime2.HasValue ? (logDateTime1.HasValue ? (logDateTime1.GetValueOrDefault() != logDateTime2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    ((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).SetValueExt<FSLogActionFilter.logDateTime>(((PXSelectBase<FSLogActionFilter>) this.LogActionFilter).Current, (object) newRow.LogDateTime);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSBillHistory> e)
  {
    if (e.Row == null || this.persistContext)
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

  public virtual void EnableDisable_Document(
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrderRow,
    FSSetup fsSetupRow,
    FSBillingCycle fsBillingCycleRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    bool skipTimeCardUpdate,
    bool? isBeingCalledFromQuickProcess)
  {
    bool flag1 = true;
    int? nullable1;
    if (fsServiceOrderRow != null && fsSrvOrdTypeRow != null && fsSrvOrdTypeRow.Behavior != "IN")
    {
      nullable1 = fsServiceOrderRow.CustomerID;
      flag1 = nullable1.HasValue;
    }
    bool? nullable2 = new bool?(((PXSelectBase) this.AppointmentRecords).Cache.AllowUpdate);
    bool masterEnable = this.CanUpdateAppointment(fsAppointmentRow, fsSrvOrdTypeRow) | skipTimeCardUpdate || isBeingCalledFromQuickProcess.GetValueOrDefault();
    bool flag2 = this.CanDeleteAppointment(fsAppointmentRow, fsServiceOrderRow, fsSrvOrdTypeRow);
    bool? nullable3;
    if (((PXGraph) this).Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS304010"))
    {
      ((PXSelectBase) this.AppointmentSelected).Cache.AllowInsert = masterEnable;
      PXCache cache1 = ((PXSelectBase) this.AppointmentSelected).Cache;
      int num1;
      if (!masterEnable && !((PXGraph) this).IsMobile)
      {
        nullable3 = fsAppointmentRow.Awaiting;
        num1 = nullable3.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 1;
      cache1.AllowUpdate = num1 != 0;
      ((PXSelectBase) this.AppointmentSelected).Cache.AllowDelete = flag2;
      ((PXSelectBase) this.AppointmentRecords).Cache.AllowInsert = true;
      PXCache cache2 = ((PXSelectBase) this.AppointmentRecords).Cache;
      int num2;
      if (!masterEnable && !((PXGraph) this).IsMobile)
      {
        nullable3 = fsAppointmentRow.Awaiting;
        num2 = nullable3.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 1;
      cache2.AllowUpdate = num2 != 0;
      ((PXSelectBase) this.AppointmentRecords).Cache.AllowDelete = flag2;
    }
    nullable3 = nullable2;
    bool allowUpdate = ((PXSelectBase) this.AppointmentRecords).Cache.AllowUpdate;
    if (!(nullable3.GetValueOrDefault() == allowUpdate & nullable3.HasValue))
      PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, ((PXSelectBase) this.AppointmentRecords).Cache.AllowUpdate);
    ((PXSelectBase) this.AppointmentDetails).Cache.AllowInsert = masterEnable & flag1;
    ((PXSelectBase) this.AppointmentDetails).Cache.AllowUpdate = masterEnable & flag1;
    ((PXSelectBase) this.AppointmentDetails).Cache.AllowDelete = masterEnable & flag1;
    PXCache cache3 = ((PXSelectBase) this.LogRecords).Cache;
    if (cache3 != null)
    {
      cache3.AllowInsert = masterEnable;
      cache3.AllowUpdate = masterEnable;
      cache3.AllowDelete = masterEnable;
    }
    ((PXSelectBase) this.AppointmentServiceEmployees).Cache.AllowInsert = masterEnable;
    ((PXSelectBase) this.AppointmentServiceEmployees).Cache.AllowUpdate = masterEnable;
    ((PXSelectBase) this.AppointmentServiceEmployees).Cache.AllowDelete = masterEnable;
    ((PXSelectBase) this.ServiceOrder_Contact).Cache.AllowInsert = masterEnable;
    ((PXSelectBase) this.ServiceOrder_Contact).Cache.AllowUpdate = masterEnable;
    ((PXSelectBase) this.ServiceOrder_Contact).Cache.AllowDelete = masterEnable;
    ((PXSelectBase) this.ServiceOrder_Address).Cache.AllowInsert = masterEnable;
    ((PXSelectBase) this.ServiceOrder_Address).Cache.AllowUpdate = masterEnable;
    ((PXSelectBase) this.ServiceOrder_Address).Cache.AllowDelete = masterEnable;
    ((PXSelectBase) this.AppointmentResources).Cache.AllowInsert = masterEnable;
    ((PXSelectBase) this.AppointmentResources).Cache.AllowUpdate = masterEnable;
    ((PXSelectBase) this.AppointmentResources).Cache.AllowDelete = masterEnable;
    PXCache cache4 = ((PXSelectBase) this.AppointmentRecords).Cache;
    FSAppointment fsAppointment1 = fsAppointmentRow;
    int num3;
    if (((PXSelectBase) this.ServiceOrderRelated).Cache.GetStatus((object) fsServiceOrderRow) == 2)
    {
      nullable3 = fsSrvOrdTypeRow.BAccountRequired;
      if (nullable3.GetValueOrDefault())
      {
        nullable1 = fsAppointmentRow.MaxLineNbr;
        int num4 = 0;
        if (!(nullable1.GetValueOrDefault() == num4 & nullable1.HasValue))
        {
          nullable1 = fsAppointmentRow.MaxLineNbr;
          num3 = !nullable1.HasValue ? 1 : 0;
          goto label_20;
        }
        num3 = 1;
        goto label_20;
      }
    }
    num3 = 0;
label_20:
    PXUIFieldAttribute.SetEnabled<FSAppointment.customerID>(cache4, (object) fsAppointment1, num3 != 0);
    if (fsServiceOrderRow != null)
    {
      bool flag3 = this.AllowEnableCustomerID(fsServiceOrderRow);
      PXCache cache5 = ((PXSelectBase) this.AppointmentRecords).Cache;
      FSAppointment fsAppointment2 = fsAppointmentRow;
      nullable3 = fsServiceOrderRow.BAccountRequired;
      bool flag4 = false;
      int num5 = !(nullable3.GetValueOrDefault() == flag4 & nullable3.HasValue) & flag3 ? 1 : 2;
      PXDefaultAttribute.SetPersistingCheck<FSAppointment.customerID>(cache5, (object) fsAppointment2, (PXPersistingCheck) num5);
    }
    bool flag5 = fsSrvOrdTypeRow?.Behavior == "RO";
    this.EnableDisable_ScheduleDateTimes(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointmentRow, masterEnable && !flag5);
    this.EnableDisable_UnreachedCustomer(((PXSelectBase) this.AppointmentRecords).Cache, fsAppointmentRow, masterEnable);
    this.EnableDisable_AppointmentActualDateTimes(((PXSelectBase) this.AppointmentRecords).Cache, fsSetupRow, fsAppointmentRow, fsSrvOrdTypeRow);
    if (fsServiceOrderRow != null)
    {
      bool flag6 = ProjectDefaultAttribute.IsNonProject(fsAppointmentRow.ProjectID);
      PXUIFieldAttribute.SetVisible<FSAppointment.dfltProjectTaskID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, !flag6);
      PXUIFieldAttribute.SetEnabled<FSAppointment.dfltProjectTaskID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, masterEnable && !flag6);
      PXUIFieldAttribute.SetRequired<FSAppointment.dfltProjectTaskID>(((PXSelectBase) this.AppointmentRecords).Cache, !flag6);
      PXDefaultAttribute.SetPersistingCheck<FSAppointment.dfltProjectTaskID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, !flag6 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    }
    if (fsAppointmentRow != null)
    {
      PXCache cache6 = ((PXSelectBase) this.AppointmentRecords).Cache;
      FSAppointment fsAppointment3 = fsAppointmentRow;
      nullable1 = fsAppointmentRow.SOID;
      int num6;
      if (nullable1.HasValue)
      {
        nullable1 = fsAppointmentRow.SOID;
        int num7 = 0;
        num6 = nullable1.GetValueOrDefault() < num7 & nullable1.HasValue ? 1 : 0;
      }
      else
        num6 = 0;
      PXUIFieldAttribute.SetEnabled<FSAppointment.soRefNbr>(cache6, (object) fsAppointment3, num6 != 0);
    }
    PXUIFieldAttribute.SetEnabled<FSAppointment.routeDocumentID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointment.executionDate>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, masterEnable);
    int num8;
    if (fsSrvOrdTypeRow != null)
    {
      nullable3 = fsSrvOrdTypeRow.OnCompleteApptSetEndTimeInHeader;
      if (nullable3.GetValueOrDefault())
      {
        num8 = 1;
        goto label_35;
      }
    }
    if (fsSrvOrdTypeRow == null)
    {
      num8 = 0;
    }
    else
    {
      nullable3 = fsSrvOrdTypeRow.SetTimeInHeaderBasedOnLog;
      num8 = nullable3.GetValueOrDefault() ? 1 : 0;
    }
label_35:
    bool flag7 = num8 != 0;
    PXUIFieldAttribute.SetEnabled<FSAppointment.handleManuallyActualTime>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, flag7);
    bool flag8 = fsBillingCycleRow != null && (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() || PXAccess.FeatureInstalled<FeaturesSet.routeManagementModule>());
    string billingMode = ServiceOrderEntry.GetBillingMode((PXGraph) this, fsBillingCycleRow, fsSrvOrdTypeRow, fsServiceOrderRow);
    bool flag9 = flag8 && billingMode == "AP";
    PXUIFieldAttribute.SetVisible<FSAppointment.billServiceContractID>(((PXSelectBase) this.AppointmentRecords).Cache, (object) fsAppointmentRow, flag8);
    PXCache cache7 = ((PXSelectBase) this.AppointmentRecords).Cache;
    FSAppointment fsAppointment4 = fsAppointmentRow;
    int num9;
    if (flag9)
    {
      nullable1 = fsAppointmentRow.BillServiceContractID;
      num9 = nullable1.HasValue ? 1 : 0;
    }
    else
      num9 = 0;
    PXUIFieldAttribute.SetVisible<FSAppointment.billContractPeriodID>(cache7, (object) fsAppointment4, num9 != 0);
  }

  public virtual void EnableDisable_ScheduleDateTimes(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    bool masterEnable)
  {
    PXUIFieldAttribute.SetEnabled<FSAppointment.scheduledDateTimeBegin>(cache, (object) fsAppointmentRow, masterEnable);
    PXUIFieldAttribute.SetEnabled<FSAppointment.scheduledDateTimeEnd>(cache, (object) fsAppointmentRow, masterEnable);
  }

  public virtual void EnableDisable_UnreachedCustomer(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    bool masterEnable)
  {
    bool flag = false;
    if (fsAppointmentRow != null)
      flag = fsAppointmentRow.NotStarted.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<FSAppointment.unreachedCustomer>(cache, (object) fsAppointmentRow, flag & masterEnable);
  }

  public virtual void EnableDisable_AppointmentActualDateTimes(
    PXCache appointmentCache,
    FSSetup fsSetupRow,
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsSetupRow == null || fsAppointmentRow == null || fsSrvOrdTypeRow == null)
      return;
    int num;
    if (fsAppointmentRow != null)
    {
      bool? nullable = fsAppointmentRow.NotStarted;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = fsAppointmentRow.Hold;
        bool flag2 = false;
        num = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
      }
      else
        num = 0;
    }
    else
      num = 0;
    bool flag3 = num != 0;
    bool flag4 = flag3 && fsAppointmentRow.ActualDateTimeBegin.HasValue;
    PXUIFieldAttribute.SetEnabled<FSAppointment.actualDateTimeBegin>(appointmentCache, (object) fsAppointmentRow, flag3);
    PXUIFieldAttribute.SetEnabled<FSAppointment.actualDateTimeEnd>(appointmentCache, (object) fsAppointmentRow, flag4);
  }

  public IEnumerable skillGridFilter()
  {
    return StaffSelectionHelper.SkillFilterDelegate<FSAppointmentDet>((PXGraph) this, (PXSelectBase<FSAppointmentDet>) this.AppointmentDetails, this.StaffSelectorFilter, (PXSelectBase<PX.Objects.FS.SkillGridFilter>) this.SkillGridFilter);
  }

  public IEnumerable licenseTypeGridFilter()
  {
    return StaffSelectionHelper.LicenseTypeFilterDelegate<FSAppointmentDet>((PXGraph) this, (PXSelectBase<FSAppointmentDet>) this.AppointmentDetails, this.StaffSelectorFilter, (PXSelectBase<PX.Objects.FS.LicenseTypeGridFilter>) this.LicenseTypeGridFilter);
  }

  protected virtual IEnumerable staffRecords()
  {
    return StaffSelectionHelper.StaffRecordsDelegate((object) this.AppointmentServiceEmployees, (PXSelectBase<PX.Objects.FS.SkillGridFilter>) this.SkillGridFilter, (PXSelectBase<PX.Objects.FS.LicenseTypeGridFilter>) this.LicenseTypeGridFilter, this.StaffSelectorFilter);
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
    PXCache cache = ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<BAccountStaffMember>>) e).Cache;
    if (((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current != null)
    {
      if (row.Selected.GetValueOrDefault())
      {
        if (((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current != null)
        {
          if (((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current.LineRef != ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef)
            ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current = PXResultset<FSAppointmentDet>.op_Implicit(((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Search<FSAppointmentDet.lineRef>((object) ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef, Array.Empty<object>()));
          if (!GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ => _.ServiceLineRef == ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef)).Any<FSAppointmentEmployee>())
            ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current.StaffID = row.BAccountID;
          else
            ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current.StaffID = new int?();
        }
        ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Insert(new FSAppointmentEmployee()
        {
          EmployeeID = row.BAccountID,
          ServiceLineRef = ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef
        });
      }
      else
      {
        FSAppointmentEmployee appointmentEmployee = (FSAppointmentEmployee) ((PXSelectBase) this.AppointmentServiceEmployees).Cache.Locate((object) PXResultset<FSAppointmentEmployee>.op_Implicit(PXSelectBase<FSAppointmentEmployee, PXSelectJoin<FSAppointmentEmployee, LeftJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>, LeftJoin<FSSODet, On<FSSODet.sOID, Equal<FSAppointment.sOID>, And<FSSODet.lineRef, Equal<FSAppointmentEmployee.serviceLineRef>>>>>, Where2<Where<FSSODet.lineRef, Equal<Required<FSSODet.lineRef>>, Or<Where<FSSODet.lineRef, IsNull, And<Required<FSSODet.lineRef>, IsNull>>>>, And<Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSAppointmentEmployee.employeeID, Equal<Required<FSAppointmentEmployee.employeeID>>>>>>>.Config>.Select(cache.Graph, new object[3]
        {
          (object) ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef,
          (object) ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef,
          (object) row.BAccountID
        })));
        if (appointmentEmployee != null)
          ((PXSelectBase<FSAppointmentEmployee>) this.AppointmentServiceEmployees).Delete(appointmentEmployee);
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
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null)
    {
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.PostalCode = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current.PostalCode;
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ProjectID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ProjectID;
    }
    if (((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current != null)
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ScheduledDateTimeBegin = ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.ScheduledDateTimeBegin;
    FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Current;
    if (current != null && current.LineType == "SERVI")
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
    if (((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current != null)
    {
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.PostalCode = ((PXSelectBase<FSAddress>) this.ServiceOrder_Address).Current.PostalCode;
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ProjectID = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderRelated).Current.ProjectID;
    }
    if (((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current != null)
      ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ScheduledDateTimeBegin = ((PXSelectBase<FSAppointment>) this.AppointmentRecords).Current.ScheduledDateTimeBegin;
    ((PXSelectBase<StaffSelectionFilter>) this.StaffSelectorFilter).Current.ServiceLineRef = (string) null;
    ((PXSelectBase) this.SkillGridFilter).Cache.Clear();
    ((PXSelectBase) this.LicenseTypeGridFilter).Cache.Clear();
    ((PXSelectBase) this.StaffRecords).Cache.Clear();
    new StaffSelectionHelper().LaunchStaffSelector((PXGraph) this, this.StaffSelectorFilter);
  }

  protected virtual void MarkHeaderAsUpdated(PXCache cache, object row)
  {
    if (row == null || ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current == null)
      return;
    if (((PXSelectBase) this.AppointmentSelected).Cache.GetStatus((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current) == null && ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.RefNbr != null)
      ((PXSelectBase) this.AppointmentSelected).Cache.SetStatus((object) ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current, (PXEntryStatus) 1);
    ((PXSelectBase<FSAppointment>) this.AppointmentSelected).Current.MustUpdateServiceOrder = new bool?(true);
  }

  public virtual void MarkApptLineAsUpdated(PXCache cache, FSApptLineSplit lineSplit)
  {
    if (lineSplit == null)
      return;
    FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) PXParentAttribute.SelectParent(cache, (object) lineSplit, typeof (FSAppointmentDet));
    if (fsAppointmentDet == null || ((PXSelectBase) this.AppointmentDetails).Cache.GetStatus((object) fsAppointmentDet) != null)
      return;
    ((PXSelectBase) this.AppointmentDetails).Cache.SetStatus((object) fsAppointmentDet, (PXEntryStatus) 1);
  }

  public virtual void InsertServiceOrderDetailsInAppointment(
    PXResultset<FSSODet> bqlResultSet_FSSODet,
    PXCache cacheAppDetails)
  {
    foreach (PXResult<FSSODet> pxResult in bqlResultSet_FSSODet)
    {
      FSSODet objSourceRow = PXResult<FSSODet>.op_Implicit(pxResult);
      AppointmentEntry.InsertDetailLine<FSAppointmentDet, FSSODet>(((PXSelectBase) this.AppointmentDetails).Cache, (object) new FSAppointmentDet()
      {
        FSSODetRow = objSourceRow
      }, ((PXGraph) this).Caches[typeof (FSSODet)], (object) objSourceRow, new Guid?(), objSourceRow.SODetID, false, objSourceRow.TranDate, false, false);
    }
  }

  public virtual void CopyAppointmentLineValues<TargetRowType, SourceRowType>(
    PXCache targetCache,
    object objTargetRow,
    PXCache sourceCache,
    object objSourceRow,
    bool copyTranDate,
    DateTime? tranDate,
    bool ForceFormulaCalculation,
    bool copyIsFreeItem)
    where TargetRowType : class, IBqlTable, IFSSODetBase, new()
    where SourceRowType : class, IBqlTable, IFSSODetBase, new()
  {
    TargetRowType argetRowType1 = (TargetRowType) objTargetRow;
    SourceRowType objSourceRow1 = (SourceRowType) objSourceRow;
    TargetRowType argetRowType2 = default (TargetRowType);
    if (ForceFormulaCalculation)
      argetRowType2 = (TargetRowType) targetCache.CreateCopy((object) argetRowType1);
    if ((object) argetRowType1 is FSSODet)
    {
      FSSODet fssoDet = (FSSODet) objTargetRow;
      if (copyTranDate)
        fssoDet.TranDate = tranDate;
    }
    else
    {
      FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) objTargetRow;
      if (copyTranDate)
        fsAppointmentDet.TranDate = tranDate;
    }
    targetCache.SetValueExtIfDifferent<FSSODet.lineType>((object) argetRowType1, (object) objSourceRow1.LineType);
    TargetRowType argetRowType3 = AppointmentEntry.CopyDependentFieldsOfSODet<TargetRowType, SourceRowType>(targetCache, (object) argetRowType1, sourceCache, (object) objSourceRow1, false, copyIsFreeItem);
    if (!ForceFormulaCalculation)
      return;
    targetCache.RaiseRowUpdated((object) argetRowType3, (object) argetRowType2);
  }

  public static NewRowType InsertDetailLine<NewRowType, SourceRowType>(
    PXCache newRowCache,
    object objNewRow,
    PXCache sourceCache,
    object objSourceRow,
    Guid? noteID,
    int? soDetID,
    bool copyTranDate,
    DateTime? tranDate,
    bool SetValuesAfterAssigningSODetID,
    bool copyingFromQuote)
    where NewRowType : class, IBqlTable, IFSSODetBase, new()
    where SourceRowType : class, IBqlTable, IFSSODetBase, new()
  {
    NewRowType newRowType1 = (NewRowType) objNewRow;
    SourceRowType objSourceRow1 = (SourceRowType) objSourceRow;
    FSSODet fssoDet = (FSSODet) null;
    FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) null;
    if ((object) newRowType1 is FSSODet)
      fssoDet = (FSSODet) objNewRow;
    else if ((object) newRowType1 is FSAppointmentDet)
      fsAppointmentDet = (FSAppointmentDet) objNewRow;
    newRowType1.LineType = objSourceRow1.LineType;
    newRowType1.UOM = objSourceRow1.UOM;
    if (fssoDet != null)
    {
      fssoDet.RefNbr = (string) null;
      fssoDet.SOID = new int?();
      fssoDet.LineRef = (string) null;
      fssoDet.SODetID = new int?();
      fssoDet.LotSerialNbr = (string) null;
      if (copyTranDate)
        fssoDet.TranDate = tranDate;
      fssoDet.NoteID = noteID;
    }
    else
    {
      fsAppointmentDet.RefNbr = (string) null;
      fsAppointmentDet.AppointmentID = new int?();
      fsAppointmentDet.LineRef = (string) null;
      fsAppointmentDet.AppDetID = new int?();
      fsAppointmentDet.LineNbr = new int?();
      fsAppointmentDet.LotSerialNbr = (string) null;
      if (copyTranDate)
        fsAppointmentDet.TranDate = tranDate;
      fsAppointmentDet.NoteID = noteID;
    }
    NewRowType newRowType2 = (NewRowType) newRowCache.Insert((object) newRowType1);
    NewRowType copy = (NewRowType) newRowCache.CreateCopy((object) newRowType2);
    if (fsAppointmentDet != null)
      newRowCache.SetValueExtIfDifferent<FSAppointmentDet.sODetID>((object) newRowType2, (object) soDetID);
    if (SetValuesAfterAssigningSODetID || fssoDet != null)
      newRowType2 = AppointmentEntry.CopyDependentFieldsOfSODet<NewRowType, SourceRowType>(newRowCache, (object) newRowType2, sourceCache, (object) objSourceRow1, copyingFromQuote, true);
    if (fsAppointmentDet != null && sourceCache.Graph.Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS500201") && EnumerableExtensions.IsNotIn<string>(fsAppointmentDet.EquipmentAction ?? string.Empty, "NO", "ST"))
    {
      fsAppointmentDet.EquipmentAction = "NO";
      fsAppointmentDet.SMEquipmentID = new int?();
      fsAppointmentDet.NewTargetEquipmentLineNbr = (string) null;
      fsAppointmentDet.ComponentID = new int?();
      fsAppointmentDet.EquipmentLineRef = new int?();
    }
    newRowCache.RaiseRowUpdated((object) newRowType2, (object) copy);
    return newRowType2;
  }

  protected static TargetRowType CopyDependentFieldsOfSODet<TargetRowType, SourceRowType>(
    PXCache targetCache,
    object objTargetRow,
    PXCache sourceCache,
    object objSourceRow,
    bool copyingFromQuote,
    bool copyIsFreeItem)
    where TargetRowType : class, IBqlTable, IFSSODetBase, new()
    where SourceRowType : class, IBqlTable, IFSSODetBase, new()
  {
    TargetRowType data1 = (TargetRowType) objTargetRow;
    SourceRowType sourceRowType = (SourceRowType) objSourceRow;
    if (sourceRowType.InventoryID.HasValue)
      targetCache.SetValueExtIfDifferent<FSSODet.siteID>((object) data1, (object) sourceRowType.SiteID);
    targetCache.SetValueExtIfDifferent<FSSODet.branchID>((object) data1, (object) sourceRowType.BranchID);
    targetCache.SetValueExtIfDifferent<FSSODet.inventoryID>((object) data1, (object) sourceRowType.InventoryID);
    int? nullable1 = data1.InventoryID;
    if (nullable1.HasValue)
    {
      targetCache.SetValueExtIfDifferent<FSSODet.isPrepaid>((object) data1, (object) sourceRowType.IsPrepaid);
      bool? isBillable = sourceRowType.IsBillable;
      bool flag1 = false;
      if (isBillable.GetValueOrDefault() == flag1 & isBillable.HasValue && sourceRowType.Status == "RP")
        targetCache.SetValueExtIfDifferent<FSSODet.isBillable>((object) data1, (object) true);
      else
        targetCache.SetValueExtIfDifferent<FSSODet.isBillable>((object) data1, (object) sourceRowType.IsBillable);
      targetCache.SetValueExtIfDifferent<FSSODet.billingRule>((object) data1, (object) sourceRowType.BillingRule);
      targetCache.SetValueExtIfDifferent<FSSODet.manualPrice>((object) data1, (object) sourceRowType.ManualPrice);
      if (copyIsFreeItem)
        targetCache.SetValueExtIfDifferent<FSSODet.isFree>((object) data1, (object) sourceRowType.IsFree);
      targetCache.SetValueExtIfDifferent<FSSODet.subItemID>((object) data1, (object) sourceRowType.SubItemID);
      targetCache.SetValueExtIfDifferent<FSSODet.uOM>((object) data1, (object) sourceRowType.UOM);
      targetCache.SetValueExtIfDifferent<FSSODet.siteID>((object) data1, (object) sourceRowType.SiteID);
      targetCache.SetValueExtIfDifferent<FSSODet.siteLocationID>((object) data1, (object) sourceRowType.SiteLocationID);
      Decimal? qty = sourceRowType.GetQty(FieldType.EstimatedField);
      Decimal? nullable2 = sourceRowType.GetApptQty();
      Decimal? nullable3;
      int? nullable4;
      if (qty.GetValueOrDefault() > nullable2.GetValueOrDefault() & qty.HasValue & nullable2.HasValue)
      {
        PXCache cache1 = targetCache;
        // ISSUE: variable of a boxed type
        __Boxed<TargetRowType> data2 = (object) data1;
        nullable2 = sourceRowType.GetQty(FieldType.EstimatedField);
        nullable3 = sourceRowType.GetApptQty();
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> newValue1 = (ValueType) (nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?());
        cache1.SetValueExtIfDifferent<FSSODet.estimatedQty>((object) data2, (object) newValue1);
        if (sourceRowType.LineType == "SERVI" && sourceRowType.BillingRule == "TIME")
        {
          PXCache cache2 = targetCache;
          // ISSUE: variable of a boxed type
          __Boxed<TargetRowType> data3 = (object) data1;
          nullable1 = sourceRowType.GetDuration(FieldType.EstimatedField);
          nullable4 = sourceRowType.GetApptDuration();
          // ISSUE: variable of a boxed type
          __Boxed<int?> newValue2 = (ValueType) (nullable1.HasValue & nullable4.HasValue ? new int?(nullable1.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new int?());
          cache2.SetValueExtIfDifferent<FSSODet.estimatedDuration>((object) data3, (object) newValue2);
        }
        else
          targetCache.SetValueExtIfDifferent<FSSODet.estimatedDuration>((object) data1, (object) sourceRowType.GetDuration(FieldType.EstimatedField));
      }
      else
      {
        switch (sourceRowType.LineType)
        {
          case "SLPRO":
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedDuration>((object) data1, (object) 0);
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedQty>((object) data1, (object) 0M);
            break;
          case "SERVI":
            if (sourceRowType.BillingRule == "TIME")
            {
              targetCache.SetValueExtIfDifferent<FSSODet.estimatedDuration>((object) data1, (object) 1);
              break;
            }
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedDuration>((object) data1, (object) sourceRowType.GetDuration(FieldType.EstimatedField));
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedQty>((object) data1, (object) 1M);
            break;
          case "NSTKI":
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedDuration>((object) data1, (object) sourceRowType.GetDuration(FieldType.EstimatedField));
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedQty>((object) data1, (object) 1M);
            break;
          default:
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedDuration>((object) data1, (object) 0);
            targetCache.SetValueExtIfDifferent<FSSODet.estimatedQty>((object) data1, (object) 0M);
            break;
        }
      }
      if (sourceRowType.IsLinkedItem)
      {
        targetCache.SetValueExtIfDifferent<FSSODet.linkedEntityType>((object) data1, (object) sourceRowType.LinkedEntityType);
        targetCache.SetValueExtIfDifferent<FSSODet.linkedDocType>((object) data1, (object) sourceRowType.LinkedDocType);
        targetCache.SetValueExtIfDifferent<FSSODet.linkedDocRefNbr>((object) data1, (object) sourceRowType.LinkedDocRefNbr);
        targetCache.SetValueExtIfDifferent<FSSODet.linkedLineNbr>((object) data1, (object) sourceRowType.LinkedLineNbr);
      }
      if (data1.ManualPrice.GetValueOrDefault())
      {
        PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = targetCache.Graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
        nullable3 = sourceRowType.UnitPrice;
        Decimal newValue3 = defaultCurrencyInfo.CuryConvCury(nullable3.GetValueOrDefault(), new int?(CommonSetupDecPl.PrcCst));
        nullable3 = sourceRowType.BillableExtPrice;
        Decimal newValue4 = defaultCurrencyInfo.CuryConvCury(nullable3.GetValueOrDefault());
        targetCache.SetValueExtIfDifferent<FSSODet.curyUnitPrice>((object) data1, (object) newValue3);
        if (newValue3 != 0M)
          PXUIFieldAttribute.SetWarning<FSSODet.curyUnitPrice>(targetCache, (object) data1, (string) null);
        targetCache.SetValueExtIfDifferent<FSSODet.curyBillableExtPrice>((object) data1, (object) newValue4);
        if (newValue4 != 0M)
          PXUIFieldAttribute.SetWarning<FSSODet.curyBillableExtPrice>(targetCache, (object) data1, (string) null);
      }
      bool flag2 = sourceRowType.IsLinkedItem;
      if ((object) sourceRowType is FSSODet)
      {
        FSSODet fssoDet = (object) sourceRowType as FSSODet;
        int num;
        if (!flag2)
        {
          nullable3 = fssoDet.CuryUnitCost;
          nullable2 = fssoDet.CuryOrigUnitCost;
          if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
          {
            num = fssoDet.ManualCost.GetValueOrDefault() ? 1 : 0;
            goto label_31;
          }
        }
        num = 1;
label_31:
        flag2 = num != 0;
      }
      if (sourceRowType.EnablePO.GetValueOrDefault())
      {
        flag2 = true;
        if (objTargetRow is FSAppointmentDet)
        {
          FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) objTargetRow;
          if (objSourceRow is FSSODet)
            fsAppointmentDet.CanChangeMarkForPO = new bool?(false);
        }
        data1.POType = sourceRowType.POType;
        data1.PONbr = sourceRowType.PONbr;
        data1.POLineNbr = sourceRowType.POLineNbr;
        data1.POCompleted = sourceRowType.POCompleted;
        data1.POStatus = sourceRowType.POStatus;
        data1.POSource = sourceRowType.POSource;
        data1.POVendorID = sourceRowType.POVendorID;
        data1.POVendorLocationID = sourceRowType.POVendorLocationID;
        targetCache.SetValueExtIfDifferent<FSSODet.enablePO>((object) data1, (object) sourceRowType.EnablePO);
        targetCache.SetValueExt<FSSODet.pOSource>((object) data1, (object) sourceRowType.POSource);
        targetCache.SetValueExt<FSSODet.poVendorID>((object) data1, (object) sourceRowType.POVendorID);
        targetCache.SetValueExt<FSSODet.poVendorLocationID>((object) data1, (object) sourceRowType.POVendorLocationID);
      }
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo1 = targetCache.Graph.FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
      if (flag2)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo1;
        nullable2 = sourceRowType.UnitCost;
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        Decimal newValue = currencyInfo.CuryConvCury(valueOrDefault);
        targetCache.SetValueExtIfDifferent<FSSODet.curyUnitCost>((object) data1, (object) newValue);
        if (newValue != 0M)
          PXUIFieldAttribute.SetWarning<FSSODet.curyUnitCost>(targetCache, (object) data1, (string) null);
      }
      if (sourceRowType.IsLinkedItem)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo1;
        nullable2 = sourceRowType.ExtCost;
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        Decimal newValue = currencyInfo.CuryConvCury(valueOrDefault);
        targetCache.SetValueExtIfDifferent<FSSODet.curyExtCost>((object) data1, (object) newValue);
      }
      targetCache.SetValueExtIfDifferent<FSSODet.taxCategoryID>((object) data1, (object) sourceRowType.TaxCategoryID);
      targetCache.SetValueExtIfDifferent<FSSODet.projectID>((object) data1, (object) sourceRowType.ProjectID);
      targetCache.SetValueExtIfDifferent<FSSODet.projectTaskID>((object) data1, (object) sourceRowType.ProjectTaskID);
      nullable4 = sourceRowType.AcctID;
      if (nullable4.HasValue || !copyingFromQuote)
      {
        targetCache.SetValueExtIfDifferent<FSSODet.acctID>((object) data1, (object) sourceRowType.AcctID);
        nullable4 = sourceRowType.SubID;
        if (nullable4.HasValue || !copyingFromQuote)
          targetCache.SetValueExtIfDifferent<FSSODet.subID>((object) data1, (object) sourceRowType.SubID);
      }
      targetCache.SetValueExtIfDifferent<FSSODet.costCodeID>((object) data1, (object) sourceRowType.CostCodeID);
      switch (objSourceRow)
      {
        case FSSODet _:
          FSSODet fssoDet1 = (FSSODet) objSourceRow;
          targetCache.SetValueExtIfDifferent<FSSODet.manualDisc>((object) data1, (object) fssoDet1.ManualDisc);
          targetCache.SetValueExtIfDifferent<FSSODet.manualCost>((object) data1, (object) fssoDet1.ManualCost);
          break;
        case FSAppointmentDet _:
          FSAppointmentDet fsAppointmentDet1 = (FSAppointmentDet) objSourceRow;
          targetCache.SetValueExtIfDifferent<FSSODet.manualDisc>((object) data1, (object) fsAppointmentDet1.ManualDisc);
          targetCache.SetValueExtIfDifferent<FSSODet.manualCost>((object) data1, (object) fsAppointmentDet1.ManualCost);
          break;
      }
      targetCache.SetValueExtIfDifferent<FSSODet.discPct>((object) data1, (object) sourceRowType.DiscPct);
      targetCache.SetValueExtIfDifferent<FSSODet.curyDiscAmt>((object) data1, (object) sourceRowType.CuryDiscAmt);
      if (copyingFromQuote)
      {
        object obj;
        targetCache.RaiseFieldDefaulting<FSSODet.acctID>((object) data1, ref obj);
        targetCache.SetValue<FSSODet.acctID>((object) data1, obj);
        targetCache.RaiseFieldDefaulting<FSSODet.subID>((object) data1, ref obj);
        targetCache.SetValue<FSSODet.subID>((object) data1, obj);
      }
    }
    bool flag = true;
    if (objTargetRow is FSAppointmentDet && sourceCache.Graph.Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS500201") && EnumerableExtensions.IsNotIn<string>(((FSAppointmentDet) objTargetRow).EquipmentAction ?? string.Empty, "NO", "ST"))
      flag = false;
    if (flag)
    {
      targetCache.SetValueExtIfDifferent<FSSODet.equipmentAction>((object) data1, (object) sourceRowType.EquipmentAction);
      targetCache.SetValueExtIfDifferent<FSSODet.SMequipmentID>((object) data1, (object) sourceRowType.SMEquipmentID);
      targetCache.SetValueExtIfDifferent<FSSODet.newTargetEquipmentLineNbr>((object) data1, sourceCache.GetValue<FSAppointmentDet.newTargetEquipmentLineNbr>((object) sourceRowType));
      targetCache.SetValueExtIfDifferent<FSSODet.componentID>((object) data1, (object) sourceRowType.ComponentID);
      targetCache.SetValueExtIfDifferent<FSSODet.equipmentLineRef>((object) data1, (object) sourceRowType.EquipmentLineRef);
    }
    else
    {
      targetCache.SetValueExt<FSSODet.equipmentAction>((object) data1, (object) "NO");
      targetCache.SetValueExt<FSSODet.SMequipmentID>((object) data1, (object) null);
      targetCache.SetValueExt<FSSODet.newTargetEquipmentLineNbr>((object) data1, (object) null);
      targetCache.SetValueExt<FSSODet.componentID>((object) data1, (object) null);
      targetCache.SetValueExt<FSSODet.equipmentLineRef>((object) data1, (object) null);
    }
    targetCache.SetValueExtIfDifferent<FSSODet.tranDesc>((object) data1, (object) sourceRowType.TranDesc);
    if (objTargetRow is FSAppointmentDet && objSourceRow is FSSODet)
    {
      FSSODet fssoDet = (FSSODet) objSourceRow;
      FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) objTargetRow;
      fsAppointmentDet.OrigSrvOrdNbr = fssoDet.RefNbr;
      fsAppointmentDet.OrigLineNbr = fssoDet.LineNbr;
    }
    return data1;
  }

  protected virtual void OnRowInsertedFSAppointmentLog(FSAppointmentLog row)
  {
    if (row == null || row.DetLineRef == null)
      return;
    FSAppointmentDet appointmentDet = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((IEnumerable<PXResult<FSAppointmentDet>>) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentDet>>()).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r => r.LineRef == row.DetLineRef)).FirstOrDefault<FSAppointmentDet>();
    if (appointmentDet == null)
      return;
    FSAppointmentDet copy = (FSAppointmentDet) ((PXSelectBase) this.AppointmentDetails).Cache.CreateCopy((object) appointmentDet);
    bool? trackOnService = row.TrackOnService;
    if (trackOnService.GetValueOrDefault())
    {
      FSAppointmentDet fsAppointmentDet = copy;
      int? logActualDuration = fsAppointmentDet.LogActualDuration;
      int? timeDuration = row.TimeDuration;
      fsAppointmentDet.LogActualDuration = logActualDuration.HasValue & timeDuration.HasValue ? new int?(logActualDuration.GetValueOrDefault() + timeDuration.GetValueOrDefault()) : new int?();
    }
    if (!copy.IsLinkedItem)
      copy.Status = this.GetItemLineStatusFromLog(appointmentDet);
    trackOnService = row.TrackOnService;
    if (trackOnService.GetValueOrDefault())
      ((PXSelectBase) this.AppointmentDetails).Cache.SetDefaultExt<FSAppointmentDet.actualDuration>((object) copy);
    ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(copy);
  }

  protected virtual void OnRowDeletedFSAppointmentLog(FSAppointmentLog row)
  {
    if (row == null || row.DetLineRef == null)
      return;
    FSAppointmentDet appointmentDet = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((IEnumerable<PXResult<FSAppointmentDet>>) ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentDet>>()).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (r => r.LineRef == row.DetLineRef)).FirstOrDefault<FSAppointmentDet>();
    if (appointmentDet == null)
      return;
    FSAppointmentDet copy = (FSAppointmentDet) ((PXSelectBase) this.AppointmentDetails).Cache.CreateCopy((object) appointmentDet);
    bool? trackOnService = row.TrackOnService;
    if (trackOnService.GetValueOrDefault())
    {
      FSAppointmentDet fsAppointmentDet = copy;
      int? logActualDuration = fsAppointmentDet.LogActualDuration;
      int? timeDuration = row.TimeDuration;
      fsAppointmentDet.LogActualDuration = logActualDuration.HasValue & timeDuration.HasValue ? new int?(logActualDuration.GetValueOrDefault() - timeDuration.GetValueOrDefault()) : new int?();
    }
    if (!copy.IsLinkedItem)
      copy.Status = this.GetItemLineStatusFromLog(appointmentDet);
    trackOnService = row.TrackOnService;
    if (trackOnService.GetValueOrDefault())
      ((PXSelectBase) this.AppointmentDetails).Cache.SetDefaultExt<FSAppointmentDet.actualDuration>((object) copy);
    ((PXSelectBase<FSAppointmentDet>) this.AppointmentDetails).Update(copy);
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual FSAppointment CalculateExternalTax(FSAppointment fsAppointmentRow)
  {
    return fsAppointmentRow;
  }

  public void ClearTaxes(FSAppointment appointmentRow)
  {
    if (appointmentRow == null || !this.IsExternalTax(appointmentRow.TaxZoneID))
      return;
    PXView view = ((PXSelectBase) this.Taxes).View;
    object[] objArray1 = new object[1]
    {
      (object) appointmentRow
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<FSAppointmentTaxTran, PX.Objects.TX.Tax> pxResult in view.SelectMultiBound(objArray1, objArray2))
      ((PXSelectBase<FSAppointmentTaxTran>) this.Taxes).Delete(PXResult<FSAppointmentTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
    appointmentRow.CuryTaxTotal = new Decimal?(0M);
    appointmentRow.CuryDocTotal = new Decimal?(AppointmentEntry.GetCuryDocTotal(appointmentRow.CuryBillableLineTotal, appointmentRow.CuryLogBillableTranAmountTotal, appointmentRow.CuryDiscTot, new Decimal?(0M), new Decimal?(0M)));
  }

  public FSAppointmentLineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<FSAppointmentLineSplittingExtension>();
  }

  public AppointmentEntry.AppointmentQuickProcess AppointmentQuickProcessExt
  {
    get => ((PXGraph) this).GetExtension<AppointmentEntry.AppointmentQuickProcess>();
  }

  public override bool InventoryItemsAreIncluded()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return false;
    FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeSelected).Current;
    return current != null && current.PostToSOSIPM.GetValueOrDefault();
  }

  public class ServiceRequirement
  {
    public int serviceID;
    public List<int?> requirementIDList = new List<int?>();
  }

  public enum DateFieldType
  {
    ScheduleField,
    ActualField,
  }

  public static class FSMailing
  {
    public const string EMAIL_CONFIRMATION_TO_CUSTOMER = "NOTIFY CUSTOMER";
    public const string EMAIL_CONFIRMATION_TO_STAFF = "NOTIFY STAFF";
    public const string EMAIL_NOTIFICATION_TO_GEOZONE_STAFF = "NOTIFY SERVICE AREA STAFF";
    public const string EMAIL_NOTIFICATION_SIGNED_APPOINTMENT = "NOTIFY SIGNED APPOINTMENT";
  }

  public class FSNotificationContactType : NotificationContactType
  {
    public const string Customer = "U";
    public const string EmployeeStaff = "F";
    public const string VendorStaff = "X";
    public const string GeoZoneStaff = "G";
    public const string Salesperson = "L";
  }

  public enum SlotIsContained
  {
    NotContained = 1,
    Contained = 2,
    PartiallyContained = 3,
    ExceedsContainment = 4,
  }

  public enum ActionButton
  {
    PutOnHold = 1,
    ReleaseFromHold = 2,
    StartAppointment = 3,
    CompleteAppointment = 4,
    ReOpenAppointment = 5,
    CancelAppointment = 6,
    CloseAppointment = 7,
    UnCloseAppointment = 8,
    InvoiceAppointment = 9,
    EditAppointment = 10, // 0x0000000A
  }

  public class ServiceOrderRelated_View : 
    PXSelect<FSServiceOrder, Where<FSServiceOrder.sOID, Equal<Optional<FSAppointment.sOID>>>>
  {
    public ServiceOrderRelated_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceOrderRelated_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class AppointmentServiceEmployees_View : 
    PXSelectJoin<FSAppointmentEmployee, LeftJoin<PX.Objects.CR.BAccount, On<FSAppointmentEmployee.employeeID, Equal<PX.Objects.CR.BAccount.bAccountID>>, LeftJoin<FSAppointmentServiceEmployee, On<FSAppointmentServiceEmployee.lineRef, Equal<FSAppointmentEmployee.serviceLineRef>, And<FSAppointmentServiceEmployee.appointmentID, Equal<FSAppointmentEmployee.appointmentID>>>>>, Where<FSAppointmentEmployee.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<Where<FSAppointmentEmployee.serviceLineRef, IsNull, Or<FSAppointmentServiceEmployee.lineType, Equal<ListField_LineType_ALL.Service>>>>>, OrderBy<Asc<FSAppointmentEmployee.lineRef>>>
  {
    public AppointmentServiceEmployees_View(PXGraph graph)
      : base(graph)
    {
    }

    public AppointmentServiceEmployees_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class AppointmentQuickProcess : PXGraphExtension<AppointmentEntry>
  {
    public PXSelect<SOOrderTypeQuickProcess, Where<SOOrderTypeQuickProcess.orderType, Equal<Current<FSSrvOrdType.postOrderType>>>> currentSOOrderType;
    public static bool isSOInvoice;
    public PXQuickProcess.Action<FSAppointment>.ConfiguredBy<FSAppQuickProcessParams> quickProcess;
    public PXAction<FSAppointment> quickProcessOk;
    public PXFilter<FSAppQuickProcessParams> QuickProcessParameters;

    public static bool IsActive() => true;

    [PXButton(CommitChanges = true)]
    [PXUIField(DisplayName = "Quick Process")]
    protected virtual IEnumerable QuickProcess(PXAdapter adapter)
    {
      // ISSUE: method pointer
      ((PXSelectBase<FSAppQuickProcessParams>) this.QuickProcessParameters).AskExt(new PXView.InitializePanel((object) null, __methodptr(InitQuickProcessPanel)));
      if (((PXSelectBase) this.Base.AppointmentRecords).AllowUpdate)
        this.Base.SkipTaxCalcAndSave();
      PXQuickProcess.Start<AppointmentEntry, FSAppointment, FSAppQuickProcessParams>(this.Base, ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current, ((PXSelectBase<FSAppQuickProcessParams>) this.QuickProcessParameters).Current);
      return (IEnumerable) new FSAppointment[1]
      {
        ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current
      };
    }

    [PXButton]
    [PXUIField(DisplayName = "OK")]
    public virtual IEnumerable QuickProcessOk(PXAdapter adapter)
    {
      ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.IsCalledFromQuickProcess = new bool?(true);
      return adapter.Get();
    }

    protected virtual void _(PX.Data.Events.RowSelected<FSAppointment> e)
    {
      if (e.Row == null)
        return;
      if (((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Current == null)
        ((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Current = PXResultset<SOOrderTypeQuickProcess>.op_Implicit(((PXSelectBase<SOOrderTypeQuickProcess>) this.currentSOOrderType).Select(Array.Empty<object>()));
      AppointmentEntry.AppointmentQuickProcess.isSOInvoice = ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current?.PostTo == "SI";
      PXQuickProcess.Action<FSAppointment>.ConfiguredBy<FSAppQuickProcessParams> quickProcess = this.quickProcess;
      FSSrvOrdType current = ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current;
      int num = current != null ? (current.AllowQuickProcess.GetValueOrDefault() ? 1 : 0) : 0;
      ((PXAction) quickProcess).SetEnabled(num != 0);
    }

    protected virtual void _(PX.Data.Events.RowSelected<FSAppQuickProcessParams> e)
    {
      if (e.Row == null)
        return;
      ((PXAction) this.quickProcessOk).SetEnabled(true);
      FSAppQuickProcessParams row = e.Row;
      PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppQuickProcessParams>>) e).Cache;
      this.SetQuickProcessSettingsVisibility(cache, ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current, ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current, row);
      if (!AppointmentEntry.AppointmentQuickProcess.isSOInvoice)
        return;
      PXUIFieldAttribute.SetEnabled<FSAppQuickProcessParams.generateInvoiceFromAppointment>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<FSAppQuickProcessParams.releaseInvoice>(cache, (object) row, row.GenerateInvoiceFromAppointment.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<FSAppQuickProcessParams.emailInvoice>(cache, (object) row, row.GenerateInvoiceFromAppointment.GetValueOrDefault());
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.sOQuickProcess> e)
    {
      if (e.Row == null)
        return;
      FSAppQuickProcessParams row = e.Row;
      bool? soQuickProcess = row.SOQuickProcess;
      bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.sOQuickProcess>, FSAppQuickProcessParams, object>) e).OldValue;
      if (soQuickProcess.GetValueOrDefault() == oldValue.GetValueOrDefault() & soQuickProcess.HasValue == oldValue.HasValue)
        return;
      AppointmentEntry.AppointmentQuickProcess.SetQuickProcessOptions((PXGraph) this.Base, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.sOQuickProcess>>) e).Cache, row, true);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.generateInvoiceFromAppointment> e)
    {
      if (e.Row == null)
        return;
      FSAppQuickProcessParams row = e.Row;
      if (!AppointmentEntry.AppointmentQuickProcess.isSOInvoice)
        return;
      bool? invoiceFromAppointment = row.GenerateInvoiceFromAppointment;
      bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.generateInvoiceFromAppointment>, FSAppQuickProcessParams, object>) e).OldValue;
      if (invoiceFromAppointment.GetValueOrDefault() == oldValue.GetValueOrDefault() & invoiceFromAppointment.HasValue == oldValue.HasValue)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.generateInvoiceFromAppointment>>) e).Cache.SetValueExt<FSAppQuickProcessParams.prepareInvoice>((object) row, (object) row.GenerateInvoiceFromAppointment.GetValueOrDefault());
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.prepareInvoice> e)
    {
      if (e.Row == null)
        return;
      FSAppQuickProcessParams row = e.Row;
      if (!AppointmentEntry.AppointmentQuickProcess.isSOInvoice)
        return;
      bool? prepareInvoice1 = row.PrepareInvoice;
      bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSAppQuickProcessParams, FSAppQuickProcessParams.prepareInvoice>, FSAppQuickProcessParams, object>) e).OldValue;
      if (prepareInvoice1.GetValueOrDefault() == oldValue.GetValueOrDefault() & prepareInvoice1.HasValue == oldValue.HasValue)
        return;
      bool? prepareInvoice2 = row.PrepareInvoice;
      bool flag = false;
      if (!(prepareInvoice2.GetValueOrDefault() == flag & prepareInvoice2.HasValue))
        return;
      row.ReleaseInvoice = new bool?(false);
      row.EmailInvoice = new bool?(false);
    }

    private void SetQuickProcessSettingsVisibility(
      PXCache cache,
      FSSrvOrdType fsSrvOrdTypeRow,
      FSAppointment fsAppointmentRow,
      FSAppQuickProcessParams fsQuickProcessParametersRow)
    {
      if (fsSrvOrdTypeRow == null)
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
          goto label_6;
        }
      }
      if (flag4)
        flag1 = true;
label_6:
      bool? nullable;
      int num1;
      if (flag2)
      {
        nullable = fsQuickProcessParametersRow.GenerateInvoiceFromAppointment;
        if (nullable.GetValueOrDefault())
        {
          nullable = fsQuickProcessParametersRow.PrepareInvoice;
          bool flag5 = false;
          if (!(nullable.GetValueOrDefault() == flag5 & nullable.HasValue))
          {
            nullable = fsQuickProcessParametersRow.SOQuickProcess;
            num1 = nullable.GetValueOrDefault() ? 1 : 0;
            goto label_12;
          }
          num1 = 1;
          goto label_12;
        }
      }
      num1 = 0;
label_12:
      bool flag6 = num1 != 0;
      PXUIFieldAttribute.SetVisible<FSAppQuickProcessParams.sOQuickProcess>(cache, (object) fsQuickProcessParametersRow, flag3 & flag2);
      PXUIFieldAttribute.SetVisible<FSAppQuickProcessParams.emailSalesOrder>(cache, (object) fsQuickProcessParametersRow, flag3);
      PXCache pxCache1 = cache;
      FSAppQuickProcessParams quickProcessParams1 = fsQuickProcessParametersRow;
      int num2;
      if (flag3 & flag1)
      {
        nullable = fsQuickProcessParametersRow.SOQuickProcess;
        bool flag7 = false;
        num2 = nullable.GetValueOrDefault() == flag7 & nullable.HasValue ? 1 : 0;
      }
      else
        num2 = 0;
      PXUIFieldAttribute.SetVisible<FSAppQuickProcessParams.prepareInvoice>(pxCache1, (object) quickProcessParams1, num2 != 0);
      PXCache pxCache2 = cache;
      FSAppQuickProcessParams quickProcessParams2 = fsQuickProcessParametersRow;
      int num3;
      if ((flag3 | flag4) & flag1)
      {
        nullable = fsQuickProcessParametersRow.SOQuickProcess;
        bool flag8 = false;
        num3 = nullable.GetValueOrDefault() == flag8 & nullable.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
      PXUIFieldAttribute.SetVisible<FSAppQuickProcessParams.releaseInvoice>(pxCache2, (object) quickProcessParams2, num3 != 0);
      PXCache pxCache3 = cache;
      FSAppQuickProcessParams quickProcessParams3 = fsQuickProcessParametersRow;
      int num4;
      if ((flag3 | flag4) & flag1)
      {
        nullable = fsQuickProcessParametersRow.SOQuickProcess;
        bool flag9 = false;
        num4 = nullable.GetValueOrDefault() == flag9 & nullable.HasValue ? 1 : 0;
      }
      else
        num4 = 0;
      PXUIFieldAttribute.SetVisible<FSAppQuickProcessParams.emailInvoice>(pxCache3, (object) quickProcessParams3, num4 != 0);
      PXUIFieldAttribute.SetEnabled<FSAppQuickProcessParams.sOQuickProcess>(cache, (object) fsQuickProcessParametersRow, flag6);
      PXUIFieldAttribute.SetEnabled<FSAppQuickProcessParams.emailSignedAppointment>(cache, (object) fsQuickProcessParametersRow, this.Base.IsEnableEmailSignedAppointment());
      nullable = fsQuickProcessParametersRow.ReleaseInvoice;
      bool flag10 = false;
      if (nullable.GetValueOrDefault() == flag10 & nullable.HasValue)
      {
        nullable = fsQuickProcessParametersRow.EmailInvoice;
        bool flag11 = false;
        if (nullable.GetValueOrDefault() == flag11 & nullable.HasValue)
        {
          nullable = fsQuickProcessParametersRow.SOQuickProcess;
          bool flag12 = false;
          if (nullable.GetValueOrDefault() == flag12 & nullable.HasValue)
          {
            nullable = fsQuickProcessParametersRow.GenerateInvoiceFromAppointment;
            if (nullable.GetValueOrDefault())
              PXUIFieldAttribute.SetEnabled<FSAppQuickProcessParams.prepareInvoice>(cache, (object) fsQuickProcessParametersRow, true);
          }
        }
      }
      if (this.Base.IsEnableEmailSignedAppointment() || !(((PXGraph) this.Base).Accessinfo.ScreenID == SharedFunctions.SetScreenIDToDotFormat("FS300200")))
        return;
      cache.RaiseExceptionHandling<FSQuickProcessParameters.emailSignedAppointment>((object) fsQuickProcessParametersRow, (object) false, (Exception) new PXSetPropertyException("The Email Signed Appointment action cannot be performed. To perform the action, sign the appointment by using the mobile app.", (PXErrorLevel) 2));
    }

    public static string[] GetExcludedFields()
    {
      return new string[5]
      {
        SharedFunctions.GetFieldName<FSQuickProcessParameters.closeAppointment>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.generateInvoiceFromAppointment>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.sOQuickProcess>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.emailSalesOrder>(),
        SharedFunctions.GetFieldName<FSQuickProcessParameters.srvOrdType>()
      };
    }

    public static void SetQuickProcessOptions(
      PXGraph graph,
      PXCache targetCache,
      FSAppQuickProcessParams fsAppQuickProcessParamsRow,
      bool ignoreUpdateSOQuickProcess)
    {
      AppointmentEntry.AppointmentQuickProcess appointmentQuickProcessExt = ((AppointmentEntry) graph).AppointmentQuickProcessExt;
      if (string.IsNullOrEmpty(((PXSelectBase<FSAppQuickProcessParams>) appointmentQuickProcessExt.QuickProcessParameters).Current.OrderType))
      {
        ((PXSelectBase) appointmentQuickProcessExt.QuickProcessParameters).Cache.Clear();
        AppointmentEntry.AppointmentQuickProcess.ResetSalesOrderQuickProcessValues(((PXSelectBase<FSAppQuickProcessParams>) appointmentQuickProcessExt.QuickProcessParameters).Current);
      }
      if (fsAppQuickProcessParamsRow != null)
        AppointmentEntry.AppointmentQuickProcess.ResetSalesOrderQuickProcessValues(fsAppQuickProcessParamsRow);
      FSQuickProcessParameters FSQuickProcessParametersRowSource = PXResultset<FSQuickProcessParameters>.op_Implicit(PXSelectBase<FSQuickProcessParameters, PXSelectReadonly<FSQuickProcessParameters, Where<FSQuickProcessParameters.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>>>.Config>.Select((PXGraph) appointmentQuickProcessExt.Base, Array.Empty<object>()));
      bool? nullable;
      int num1;
      if (fsAppQuickProcessParamsRow != null)
      {
        nullable = fsAppQuickProcessParamsRow.SOQuickProcess;
        if (nullable.GetValueOrDefault())
        {
          num1 = 1;
          goto label_12;
        }
      }
      if (fsAppQuickProcessParamsRow == null)
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
      PXCache pxCache = targetCache ?? ((PXSelectBase) appointmentQuickProcessExt.QuickProcessParameters).Cache;
      FSAppQuickProcessParams quickProcessParams = fsAppQuickProcessParamsRow ?? ((PXSelectBase<FSAppQuickProcessParams>) appointmentQuickProcessExt.QuickProcessParameters).Current;
      SOOrderTypeQuickProcess current = ((PXSelectBase<SOOrderTypeQuickProcess>) appointmentQuickProcessExt.currentSOOrderType).Current;
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
        PXCache<FSAppQuickProcessParams> cacheSource = new PXCache<FSAppQuickProcessParams>((PXGraph) appointmentQuickProcessExt.Base);
        FSAppQuickProcessParams rowSource = PXResultset<FSAppQuickProcessParams>.op_Implicit(PXSelectBase<FSAppQuickProcessParams, PXSelectReadonly<FSAppQuickProcessParams, Where<FSAppQuickProcessParams.orderType, Equal<Current<FSSrvOrdType.postOrderType>>>>.Config>.Select((PXGraph) appointmentQuickProcessExt.Base, Array.Empty<object>()));
        SharedFunctions.CopyCommonFields(pxCache, (IBqlTable) quickProcessParams, (PXCache) cacheSource, (IBqlTable) rowSource, AppointmentEntry.AppointmentQuickProcess.GetExcludedFields());
        nullable = quickProcessParams.CreateShipment;
        if (nullable.GetValueOrDefault())
        {
          appointmentQuickProcessExt.EnsureSiteID(pxCache, quickProcessParams);
          DateTime? shipDate = appointmentQuickProcessExt.Base.GetShipDate(((PXSelectBase<FSServiceOrder>) appointmentQuickProcessExt.Base.ServiceOrderRelated).Current, ((PXSelectBase<FSAppointment>) appointmentQuickProcessExt.Base.AppointmentRecords).Current);
          SOQuickProcessParametersShipDateExt.SetDate(pxCache, (SOQuickProcessParameters) quickProcessParams, shipDate.Value);
        }
      }
      else
        AppointmentEntry.AppointmentQuickProcess.SetCommonValues(quickProcessParams, FSQuickProcessParametersRowSource);
      if (ignoreUpdateSOQuickProcess)
        return;
      AppointmentEntry.AppointmentQuickProcess.SetServiceOrderTypeValues(graph, quickProcessParams, FSQuickProcessParametersRowSource);
    }

    public static void InitQuickProcessPanel(PXGraph graph, string viewName)
    {
      AppointmentEntry.AppointmentQuickProcess.SetQuickProcessOptions(graph, (PXCache) null, (FSAppQuickProcessParams) null, false);
    }

    public static void ResetSalesOrderQuickProcessValues(
      FSAppQuickProcessParams fsAppQuickProcessParamsRow)
    {
      fsAppQuickProcessParamsRow.CreateShipment = new bool?(false);
      fsAppQuickProcessParamsRow.ConfirmShipment = new bool?(false);
      fsAppQuickProcessParamsRow.UpdateIN = new bool?(false);
      fsAppQuickProcessParamsRow.PrepareInvoiceFromShipment = new bool?(false);
      fsAppQuickProcessParamsRow.PrepareInvoice = new bool?(false);
      fsAppQuickProcessParamsRow.EmailInvoice = new bool?(false);
      fsAppQuickProcessParamsRow.ReleaseInvoice = new bool?(false);
      fsAppQuickProcessParamsRow.AutoRedirect = new bool?(false);
      fsAppQuickProcessParamsRow.AutoDownloadReports = new bool?(false);
    }

    public static void SetCommonValues(
      FSAppQuickProcessParams fsAppQuickProcessParamsRowTarget,
      FSQuickProcessParameters FSQuickProcessParametersRowSource)
    {
      bool? invoiceFromAppointment;
      if (AppointmentEntry.AppointmentQuickProcess.isSOInvoice && fsAppQuickProcessParamsRowTarget.GenerateInvoiceFromAppointment.GetValueOrDefault())
      {
        fsAppQuickProcessParamsRowTarget.PrepareInvoice = new bool?(false);
      }
      else
      {
        FSAppQuickProcessParams quickProcessParams = fsAppQuickProcessParamsRowTarget;
        invoiceFromAppointment = FSQuickProcessParametersRowSource.GenerateInvoiceFromAppointment;
        bool? nullable = invoiceFromAppointment.Value ? FSQuickProcessParametersRowSource.PrepareInvoice : new bool?(false);
        quickProcessParams.PrepareInvoice = nullable;
      }
      FSAppQuickProcessParams quickProcessParams1 = fsAppQuickProcessParamsRowTarget;
      invoiceFromAppointment = FSQuickProcessParametersRowSource.GenerateInvoiceFromAppointment;
      bool? nullable1 = invoiceFromAppointment.Value ? FSQuickProcessParametersRowSource.ReleaseInvoice : new bool?(false);
      quickProcessParams1.ReleaseInvoice = nullable1;
      FSAppQuickProcessParams quickProcessParams2 = fsAppQuickProcessParamsRowTarget;
      invoiceFromAppointment = FSQuickProcessParametersRowSource.GenerateInvoiceFromAppointment;
      bool? nullable2 = invoiceFromAppointment.Value ? FSQuickProcessParametersRowSource.EmailInvoice : new bool?(false);
      quickProcessParams2.EmailInvoice = nullable2;
    }

    public static void SetServiceOrderTypeValues(
      PXGraph graph,
      FSAppQuickProcessParams fsAppQuickProcessParamsRowTarget,
      FSQuickProcessParameters FSQuickProcessParametersRowSource)
    {
      fsAppQuickProcessParamsRowTarget.CloseAppointment = FSQuickProcessParametersRowSource.CloseAppointment;
      FSAppQuickProcessParams quickProcessParams1 = fsAppQuickProcessParamsRowTarget;
      bool? nullable1;
      int num;
      if (((AppointmentEntry) graph).IsEnableEmailSignedAppointment())
      {
        nullable1 = FSQuickProcessParametersRowSource.EmailSignedAppointment;
        if (nullable1.HasValue)
        {
          nullable1 = FSQuickProcessParametersRowSource.EmailSignedAppointment;
          num = nullable1.Value ? 1 : 0;
        }
        else
          num = 0;
      }
      else
        num = 0;
      bool? nullable2 = new bool?(num != 0);
      quickProcessParams1.EmailSignedAppointment = nullable2;
      fsAppQuickProcessParamsRowTarget.GenerateInvoiceFromAppointment = FSQuickProcessParametersRowSource.GenerateInvoiceFromAppointment;
      FSAppQuickProcessParams quickProcessParams2 = fsAppQuickProcessParamsRowTarget;
      nullable1 = FSQuickProcessParametersRowSource.GenerateInvoiceFromAppointment;
      bool? nullable3 = nullable1.Value ? FSQuickProcessParametersRowSource.EmailSalesOrder : new bool?(false);
      quickProcessParams2.EmailSalesOrder = nullable3;
      fsAppQuickProcessParamsRowTarget.SOQuickProcess = FSQuickProcessParametersRowSource.SOQuickProcess;
      fsAppQuickProcessParamsRowTarget.SrvOrdType = FSQuickProcessParametersRowSource.SrvOrdType;
      if (!AppointmentEntry.AppointmentQuickProcess.isSOInvoice)
        return;
      nullable1 = fsAppQuickProcessParamsRowTarget.GenerateInvoiceFromAppointment;
      if (!nullable1.GetValueOrDefault())
        return;
      fsAppQuickProcessParamsRowTarget.PrepareInvoice = new bool?(false);
    }

    protected virtual void EnsureSiteID(PXCache sender, FSAppQuickProcessParams row)
    {
      if (row.SiteID.HasValue)
        return;
      int? preferedSiteId = this.Base.GetPreferedSiteID();
      if (!preferedSiteId.HasValue)
        return;
      sender.SetValueExt<FSAppQuickProcessParams.siteID>((object) row, (object) preferedSiteId);
    }
  }

  public class MultiCurrency : SMMultiCurrencyGraph<AppointmentEntry, FSAppointment>
  {
    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[5]
      {
        (PXSelectBase) this.Base.AppointmentRecords,
        (PXSelectBase) this.Base.AppointmentDetails,
        (PXSelectBase) this.Base.ServiceOrderRelated,
        (PXSelectBase) this.Base.TaxLines,
        (PXSelectBase) this.Base.Taxes
      };
    }

    protected override MultiCurrencyGraph<AppointmentEntry, FSAppointment>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<AppointmentEntry, FSAppointment>.DocumentMapping(typeof (FSAppointment))
      {
        BAccountID = typeof (FSAppointment.billCustomerID),
        DocumentDate = typeof (FSAppointment.executionDate)
      };
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
    {
      if (e.Row == null)
        return;
      if (((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.MultiCurrency.Document>(e.Row) is FSAppointment)
      {
        AppointmentEntry graph = (AppointmentEntry) ((PXSelectBase) this.Documents).Cache.Graph;
        if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID>>) e).ExternalCall && e.Row?.CuryID != null && !graph.recalculateCuryID)
          return;
        this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID>>) e).Cache, (IBqlTable) e.Row);
      }
      else
        base._(e);
    }

    protected virtual void _(PX.Data.Events.RowUpdating<FSServiceOrder> e)
    {
      if (this.IsModified(((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<FSServiceOrder>>) e).Cache, (IBqlTable) e.NewRow, (IBqlTable) e.Row))
        return;
      e.Cancel = true;
    }
  }

  public class SalesTax : TaxGraph<AppointmentEntry, FSAppointment>
  {
    protected override bool CalcGrossOnDocumentLevel
    {
      get => true;
      set => base.CalcGrossOnDocumentLevel = value;
    }

    protected override PXView DocumentDetailsView
    {
      get => ((PXSelectBase) this.Base.AppointmentDetails).View;
    }

    protected override TaxBaseGraph<AppointmentEntry, FSAppointment>.DocumentMapping GetDocumentMapping()
    {
      return new TaxBaseGraph<AppointmentEntry, FSAppointment>.DocumentMapping(typeof (FSAppointment))
      {
        DocumentDate = typeof (FSAppointment.executionDate),
        CuryDocBal = typeof (FSAppointment.curyDocTotal),
        CuryDiscountLineTotal = typeof (FSAppointment.curyLineDocDiscountTotal),
        CuryDiscTot = typeof (FSAppointment.curyDiscTot),
        BranchID = typeof (FSAppointment.branchID),
        FinPeriodID = typeof (FSAppointment.finPeriodID),
        TaxZoneID = typeof (FSAppointment.taxZoneID),
        CuryLinetotal = typeof (FSAppointment.curyBillableLineTotal),
        CuryTaxTotal = typeof (FSAppointment.curyTaxTotal),
        TaxCalcMode = typeof (FSAppointment.taxCalcMode)
      };
    }

    protected override TaxBaseGraph<AppointmentEntry, FSAppointment>.DetailMapping GetDetailMapping()
    {
      return new TaxBaseGraph<AppointmentEntry, FSAppointment>.DetailMapping(typeof (FSAppointmentDet))
      {
        CuryTranAmt = typeof (FSAppointmentDet.curyBillableTranAmt),
        TaxCategoryID = typeof (FSAppointmentDet.taxCategoryID),
        DocumentDiscountRate = typeof (FSAppointmentDet.documentDiscountRate),
        GroupDiscountRate = typeof (FSAppointmentDet.groupDiscountRate),
        CuryTranDiscount = typeof (FSAppointmentDet.curyDiscAmt),
        CuryTranExtPrice = typeof (FSAppointmentDet.curyBillableExtPrice),
        Qty = typeof (FSAppointmentDet.billableQty)
      };
    }

    protected override TaxBaseGraph<AppointmentEntry, FSAppointment>.TaxDetailMapping GetTaxDetailMapping()
    {
      return new TaxBaseGraph<AppointmentEntry, FSAppointment>.TaxDetailMapping(typeof (FSAppointmentTax), typeof (FSAppointmentTax.taxID));
    }

    protected override TaxBaseGraph<AppointmentEntry, FSAppointment>.TaxTotalMapping GetTaxTotalMapping()
    {
      return new TaxBaseGraph<AppointmentEntry, FSAppointment>.TaxTotalMapping(typeof (FSAppointmentTaxTran), typeof (FSAppointmentTaxTran.taxID));
    }

    protected virtual void Document_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      PX.Objects.Extensions.SalesTax.Document extension = sender.GetExtension<PX.Objects.Extensions.SalesTax.Document>(e.Row);
      if (extension == null)
        return;
      if (!extension.TaxCalc.HasValue)
        extension.TaxCalc = new TaxCalc?(TaxCalc.Calc);
      if (((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current != null && ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected).Current.PostTo == "PM")
        extension.TaxCalc = new TaxCalc?(TaxCalc.NoCalc);
      Decimal num = (Decimal) (this.ParentGetValue<FSAppointment.curyBillableLineTotal>() ?? (object) 0M);
      ((FSAppointment) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.SalesTax.Document>(((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current)).CuryActualBillableTotal = new Decimal?(num);
    }

    protected override void CalcDocTotals(
      object row,
      Decimal CuryTaxTotal,
      Decimal CuryInclTaxTotal,
      Decimal CuryWhTaxTotal)
    {
      base.CalcDocTotals(row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal);
      FSAppointment main = (FSAppointment) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.SalesTax.Document>(((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current);
      Decimal curyDocTotal = AppointmentEntry.GetCuryDocTotal(new Decimal?((Decimal) (this.ParentGetValue<FSAppointment.curyBillableLineTotal>() ?? (object) 0M)), new Decimal?((Decimal) (this.ParentGetValue<FSAppointment.curyLogBillableTranAmountTotal>() ?? (object) 0M)), new Decimal?((Decimal) (this.ParentGetValue<FSAppointment.curyDiscTot>() ?? (object) 0M)), new Decimal?(CuryTaxTotal), new Decimal?(CuryInclTaxTotal));
      if (object.Equals((object) curyDocTotal, (object) (Decimal) (this.ParentGetValue<FSAppointment.curyDocTotal>() ?? (object) 0M)))
        return;
      this.ParentSetValue<FSAppointment.curyDocTotal>((object) curyDocTotal);
    }

    protected override string GetExtCostLabel(PXCache sender, object row)
    {
      return ((PXFieldState) sender.GetValueExt<FSAppointmentDet.curyBillableExtPrice>(row)).DisplayName;
    }

    protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
    {
      if (!(child is PXResult<Detail> pxResult))
        return;
      ((FSAppointmentDet) PXResult.Unwrap<Detail>((object) pxResult).Base).CuryBillableExtPrice = value;
      sender.Update((object) pxResult);
    }

    protected override List<object> SelectTaxes<Where>(
      PXGraph graph,
      object row,
      PXTaxCheck taxchk,
      params object[] parameters)
    {
      Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> dictionary = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
      IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
      ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxComparer", (string) null);
      object[] objArray = new object[2]
      {
        row == null || !(row is Detail) ? (object) null : ((PXSelectBase) this.Details).Cache.GetMain<Detail>((Detail) row),
        (object) ((PXSelectBase<FSAppointment>) ((AppointmentEntry) graph).AppointmentSelected).Current
      };
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<False>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<False>, And<Current<FSAppointment.executionDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
        dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = pxResult;
      List<object> objectList = new List<object>();
      switch (taxchk)
      {
        case PXTaxCheck.Line:
          foreach (PXResult<FSAppointmentTax> pxResult1 in PXSelectBase<FSAppointmentTax, PXSelect<FSAppointmentTax, Where<FSAppointmentTax.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentTax.refNbr, Equal<Current<FSAppointment.refNbr>>, And<FSAppointmentTax.lineNbr, Equal<Current<FSAppointmentDet.lineNbr>>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            FSAppointmentTax fsAppointmentTax = PXResult<FSAppointmentTax>.op_Implicit(pxResult1);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult2;
            if (dictionary.TryGetValue(fsAppointmentTax.TaxID, out pxResult2))
            {
              int count = objectList.Count;
              while (count > 0 && calculationLevelComparer.Compare(PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2)) > 0)
                --count;
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2));
              objectList.Insert(count, (object) new PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>(fsAppointmentTax, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2)));
            }
          }
          return objectList;
        case PXTaxCheck.RecalcLine:
          foreach (PXResult<FSAppointmentTax> pxResult3 in PXSelectBase<FSAppointmentTax, PXSelect<FSAppointmentTax, Where<FSAppointmentTax.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentTax.refNbr, Equal<Current<FSAppointment.refNbr>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            FSAppointmentTax fsAppointmentTax = PXResult<FSAppointmentTax>.op_Implicit(pxResult3);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult4;
            if (dictionary.TryGetValue(fsAppointmentTax.TaxID, out pxResult4))
            {
              int count;
              for (count = objectList.Count; count > 0; --count)
              {
                int? lineNbr1 = PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]).LineNbr;
                int? lineNbr2 = fsAppointmentTax.LineNbr;
                if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || calculationLevelComparer.Compare(PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4)) <= 0)
                  break;
              }
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4));
              objectList.Insert(count, (object) new PXResult<FSAppointmentTax, PX.Objects.TX.Tax, TaxRev>(fsAppointmentTax, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4)));
            }
          }
          return objectList;
        case PXTaxCheck.RecalcTotals:
          foreach (PXResult<FSAppointmentTaxTran> pxResult5 in PXSelectBase<FSAppointmentTaxTran, PXSelect<FSAppointmentTaxTran, Where<FSAppointmentTaxTran.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentTaxTran.refNbr, Equal<Current<FSAppointment.refNbr>>>>, OrderBy<Asc<FSAppointmentTaxTran.srvOrdType, Asc<FSAppointmentTaxTran.refNbr, Asc<FSAppointmentTaxTran.taxID>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            FSAppointmentTaxTran appointmentTaxTran = PXResult<FSAppointmentTaxTran>.op_Implicit(pxResult5);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult6;
            if (appointmentTaxTran.TaxID != null && dictionary.TryGetValue(appointmentTaxTran.TaxID, out pxResult6))
            {
              int count = objectList.Count;
              while (count > 0 && calculationLevelComparer.Compare(PXResult<FSAppointmentTaxTran, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<FSAppointmentTaxTran, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6)) > 0)
                --count;
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6));
              objectList.Insert(count, (object) new PXResult<FSAppointmentTaxTran, PX.Objects.TX.Tax, TaxRev>(appointmentTaxTran, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6)));
            }
          }
          return objectList;
        default:
          return objectList;
      }
    }

    protected override List<object> SelectDocumentLines(PXGraph graph, object row)
    {
      return GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointment.refNbr>>>>>.Config>.SelectMultiBound(graph, new object[1]
      {
        row
      }, Array.Empty<object>())).Select<FSAppointmentDet, object>((Func<FSAppointmentDet, object>) (_ => (object) _)).ToList<object>();
    }

    protected virtual void _(PX.Data.Events.RowSelected<FSAppointmentTaxTran> e)
    {
      if (e.Row == null)
        return;
      PXUIFieldAttribute.SetEnabled<FSAppointmentTaxTran.taxID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentTaxTran>>) e).Cache, (object) e.Row, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSAppointmentTaxTran>>) e).Cache.GetStatus((object) e.Row) == 2);
    }

    protected virtual void _(PX.Data.Events.RowPersisting<FSAppointmentTaxTran> e)
    {
      FSAppointmentTaxTran row = e.Row;
      if (row == null)
        return;
      if (e.Operation == 3)
      {
        FSAppointmentTax fsAppointmentTax = (FSAppointmentTax) ((PXSelectBase) this.Base.TaxLines).Cache.Locate((object) AppointmentEntry.SalesTax.FindFSAppointmentTax(row));
        if (((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) fsAppointmentTax) == 3 || ((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) fsAppointmentTax) == 4)
          e.Cancel = true;
      }
      if (e.Operation != 1 || ((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) (FSAppointmentTax) ((PXSelectBase) this.Base.TaxLines).Cache.Locate((object) AppointmentEntry.SalesTax.FindFSAppointmentTax(row))) != 1)
        return;
      e.Cancel = true;
    }

    internal static FSAppointmentTax FindFSAppointmentTax(FSAppointmentTaxTran tran)
    {
      return GraphHelper.RowCast<FSAppointmentTax>((IEnumerable) PXSelectBase<FSAppointmentTax, PXSelect<FSAppointmentTax, Where<FSAppointmentTax.srvOrdType, Equal<Required<FSAppointmentTax.srvOrdType>>, And<FSAppointmentTax.refNbr, Equal<Required<FSAppointmentTax.refNbr>>, And<FSAppointmentTax.lineNbr, Equal<Required<FSAppointmentTax.lineNbr>>, And<FSAppointmentTax.taxID, Equal<Required<FSAppointmentTax.taxID>>>>>>>.Config>.SelectSingleBound(new PXGraph(), new object[0], Array.Empty<object>())).FirstOrDefault<FSAppointmentTax>();
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.taxZoneID> e)
    {
      FSAppointment row = e.Row;
      if (row == null)
        return;
      AppointmentEntry.ServiceOrderRelated_View serviceOrderRelated = this.Base.ServiceOrderRelated;
      FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRelated).Current;
      if (current == null)
      {
        ((PXSelectBase) serviceOrderRelated).Cache.SetValueExt<FSServiceOrder.taxZoneID>((object) ((PXSelectBase<FSServiceOrder>) serviceOrderRelated).Current, (object) null);
      }
      else
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.taxZoneID>, FSAppointment, object>) e).NewValue = (object) this.Base.GetDefaultTaxZone(row.BillCustomerID, current.BillLocationID, row.BranchID, row.ProjectID);
        ((PXSelectBase) serviceOrderRelated).Cache.SetValueExt<FSServiceOrder.taxZoneID>((object) ((PXSelectBase<FSServiceOrder>) serviceOrderRelated).Current, ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSAppointment, FSAppointment.taxZoneID>, FSAppointment, object>) e).NewValue);
      }
    }

    protected virtual void _(PX.Data.Events.RowUpdated<FSAddress> e)
    {
      this.Base.FSAddress_RowUpdated_Handler(e, ((PXSelectBase) this.Base.AppointmentRecords).Cache);
    }
  }

  /// <summary>
  /// A per-unit tax graph extension for which will forbid edit of per-unit taxes in UI.
  /// </summary>
  public class PerUnitTaxDisableExt : 
    PerUnitTaxDataEntryGraphExtension<AppointmentEntry, FSAppointmentTaxTran>
  {
    public static bool IsActive()
    {
      return PerUnitTaxDataEntryGraphExtension<AppointmentEntry, FSAppointmentTaxTran>.IsActiveBase();
    }
  }

  public class ContactAddress : SrvOrdContactAddressGraph<AppointmentEntry>
  {
  }

  public class ExtensionSorting : Module
  {
    private static readonly Dictionary<System.Type, int> _order = new Dictionary<System.Type, int>()
    {
      {
        typeof (AppointmentEntry.ContactAddress),
        1
      },
      {
        typeof (AppointmentEntry.MultiCurrency),
        2
      },
      {
        typeof (AppointmentEntry.SalesTax),
        5
      }
    };

    protected virtual void Load(ContainerBuilder builder)
    {
      ApplicationStartActivation.RunOnApplicationStart(builder, (System.Action) (() => PXBuildManager.SortExtensions += (Action<List<System.Type>>) (list => PXBuildManager.PartialSort(list, AppointmentEntry.ExtensionSorting._order))), (string) null);
    }
  }

  /// <exclude />
  public class AppointmentEntryAddressLookupExtension : 
    AddressLookupExtension<AppointmentEntry, FSAppointment, FSAddress>
  {
    protected override string AddressView => "ServiceOrder_Address";

    protected override string ViewOnMap => "viewDirectionOnMap";
  }
}
