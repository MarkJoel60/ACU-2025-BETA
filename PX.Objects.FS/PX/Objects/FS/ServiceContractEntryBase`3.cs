// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractEntryBase`3
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.FS.Scheduler;
using PX.Objects.PM;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading;

#nullable enable
namespace PX.Objects.FS;

public class ServiceContractEntryBase<TGraph, TPrimary, TWhere> : PXGraph<
#nullable disable
TGraph, TPrimary>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
  where TWhere : class, IBqlWhere, new()
{
  public bool isStatusChanged;
  public bool insertContractActionForSchedules;
  public bool skipStatusSmartPanels;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> Contact;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public CRAttributeList<FSServiceContract> Answers;
  public PXSelectJoin<FSServiceContract, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<TWhere>>> ServiceContractRecords;
  public PXSelect<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>> ServiceContractSelected;
  [PXCopyPasteHiddenView]
  public PXFilter<FSContractPeriodFilter> ContractPeriodFilter;
  public PXSelect<FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>, And<Where2<Where<Current<FSContractPeriodFilter.contractPeriodID>, IsNull, And<FSContractPeriod.status, NotEqual<ListField_Status_ContractPeriod.Active>>>, Or<FSContractPeriod.contractPeriodID, Equal<Current<FSContractPeriodFilter.contractPeriodID>>>>>>> ContractPeriodRecords;
  public PXSelect<FSContractPeriodDet, Where<FSContractPeriodDet.contractPeriodID, Equal<Current<FSContractPeriodFilter.contractPeriodID>>, And<FSContractPeriodDet.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>>> ContractPeriodDetRecords;
  public PXSelectReadonly3<FSScheduleDet, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleDet.scheduleID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSSchedule.entityID>, And<FSServiceContract.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>, And<FSScheduleDet.lineType, Equal<FSLineType.Service>>>>>>, OrderBy<Asc<FSSchedule.refNbr>>> ScheduleServicesByContract;
  public PXSelectReadonly2<FSScheduleDet, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleDet.scheduleID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSSchedule.entityID>, And<FSServiceContract.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>>>>, Where<FSScheduleDet.lineType, Equal<FSLineType.Service>>, OrderBy<Asc<FSSchedule.refNbr>>> ScheduleDetServicesByContract;
  public PXSelectReadonly2<FSScheduleDet, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleDet.scheduleID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSSchedule.entityID>, And<FSServiceContract.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>, And<FSScheduleDet.lineType, Equal<FSLineType.Inventory_Item>>>>>>, Where<True, Equal<True>>, OrderBy<Asc<FSSchedule.refNbr>>> ScheduleDetPartsByContract;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<FSSalesPrice, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSSalesPrice.inventoryID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSSalesPrice.serviceContractID>>>>, Where<FSServiceContract.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>> SalesPriceLines;
  [PXCopyPasteHiddenView]
  public PXSelect<FSContractAction, Where<FSContractAction.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>> ContractHistoryItems;
  [PXCopyPasteHiddenView]
  public PXFilter<FSActivationContractFilter> ActivationContractFilter;
  [PXCopyPasteHiddenView]
  public PXFilter<FSCopyContractFilter> CopyContractFilter;
  [PXCopyPasteHiddenView]
  public PXSelect<FSContractSchedule, Where<FSContractSchedule.entityID, Equal<Current<FSServiceContract.serviceContractID>>>> ContractSchedules;
  [PXCopyPasteHiddenView]
  public PXFilter<FSTerminateContractFilter> TerminateContractFilter;
  [PXCopyPasteHiddenView]
  public PXFilter<FSSuspendContractFilter> SuspendContractFilter;
  [PXCopyPasteHiddenView]
  public PXSelect<ActiveSchedule, Where<FSSchedule.entityID, Equal<Current<FSServiceContract.serviceContractID>>, And<FSSchedule.active, Equal<True>>>> ActiveScheduleRecords;
  [PXCopyPasteHiddenView]
  public PXSelect<FSContractPostDoc, Where<FSContractPostDoc.contractPeriodID, Equal<Current<FSContractPeriodFilter.contractPeriodID>>, And<FSContractPostDoc.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>>> ContractPostDocRecords;
  [PXCopyPasteHiddenView]
  public PXSelect<FSBillHistory, Where<FSBillHistory.serviceContractRefNbr, Equal<Current<FSServiceContract.refNbr>>, And<FSBillHistory.srvOrdType, IsNull>>, OrderBy<Desc<FSBillHistory.createdDateTime>>> InvoiceRecords;
  [PXCopyPasteHiddenView]
  public CRValidationFilter<FSContractForecastFilter> ForecastFilter;
  [PXCopyPasteHiddenView]
  public PXSelect<FSContractForecast, Where<FSContractForecast.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>, And<FSContractForecast.active, Equal<True>>>> forecastRecords;
  [PXCopyPasteHiddenView]
  public PXSelect<FSContractForecastDet, Where<FSContractForecastDet.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>, And<FSContractForecastDet.forecastID, Equal<Current<FSContractForecast.forecastID>>>>> forecastDetRecords;
  public PXAction<FSServiceContract> report;
  public PXAction<FSServiceContract> activateContract;
  public PXAction<FSServiceContract> suspendContract;
  public PXAction<FSServiceContract> renewContract;
  public PXAction<FSServiceContract> cancelContract;
  public PXDBAction<FSServiceContract> addSchedule;
  public PXAction<FSServiceContract> viewServiceOrderHistory;
  public PXAction<FSServiceContract> viewAppointmentHistory;
  public PXAction<FSServiceContract> viewContractScheduleDetails;
  public PXAction<FSServiceContract> viewCustomerContracts;
  public PXAction<FSServiceContract> viewCustomerContractSchedules;
  public PXAction<FSServiceContract> activatePeriod;
  public PXAction<FSServiceContract> forecastPrintQuote;
  public PXAction<FSServiceContract> emailQuoteContract;
  public PXAction<FSServiceContract> copyContract;
  public ViewPostBatch<FSServiceContract> openPostBatch;
  public PXAction<FSServiceContract> notification;

  public bool IsCopyContract { get; protected set; }

  public bool IsRenewContract { get; protected set; }

  public bool IsForcastProcess { get; protected set; }

  [PXButton]
  [PXUIField(DisplayName = "Reports")]
  public virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<FSServiceContract> list = adapter.Get<FSServiceContract>().ToList<FSServiceContract>();
    if (!string.IsNullOrEmpty(reportID))
    {
      ((PXAction) this.Save).Press();
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      PXReportRequiredException ex = (PXReportRequiredException) null;
      Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
      foreach (FSServiceContract fsServiceContract in list)
      {
        Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
        dictionary2["FSServiceContract.RefNbr"] = fsServiceContract.RefNbr;
        string str = new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, fsServiceContract.CustomerID, fsServiceContract.BranchID);
        ex = PXReportRequiredException.CombineReport(ex, str, dictionary2, (CurrentLocalization) null);
        reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, dictionary2, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, str, fsServiceContract.BranchID, (CurrentLocalization) null);
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

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    ((PXSelectBase) this.InvoiceRecords).Cache.ClearQueryCache();
    return ((PXAction) new PXCancel<FSServiceContract>((PXGraph) this, nameof (Cancel))).Press(a);
  }

  [PXButton(Category = "Processing")]
  [PXUIField(DisplayName = "Activate")]
  public virtual IEnumerable ActivateContract(PXAdapter adapter)
  {
    List<FSServiceContract> list = adapter.Get<FSServiceContract>().ToList<FSServiceContract>();
    string errorMessage = "";
    foreach (FSServiceContract fsServiceContractRow in list)
    {
      if (fsServiceContractRow.isEditable())
        ((PXAction) this.Save).Press();
      if (!this.CheckNewContractStatus((PXGraph) this, fsServiceContractRow, "A", ref errorMessage))
        throw new PXException(errorMessage);
      if (fsServiceContractRow.Status != "D")
      {
        if ((this.skipStatusSmartPanels || ((PXGraph) this).IsImport || ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).AskExt() == 1) && this.CheckDatesApplyOrScheduleStatusChange(((PXSelectBase) this.ServiceContractRecords).Cache, fsServiceContractRow, ((PXGraph) this).Accessinfo.BusinessDate, ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate))
        {
          this.ApplyOrScheduleStatusChange((PXGraph) this, ((PXSelectBase) this.ServiceContractRecords).Cache, fsServiceContractRow, ((PXGraph) this).Accessinfo.BusinessDate, ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate, "A");
          this.UpdateSchedulesByActivateContract();
          this.ApplyContractPeriodStatusChange(fsServiceContractRow);
        }
      }
      else
      {
        this.ApplyOrScheduleStatusChange((PXGraph) this, ((PXSelectBase) this.ServiceContractRecords).Cache, fsServiceContractRow, ((PXGraph) this).Accessinfo.BusinessDate, ((PXGraph) this).Accessinfo.BusinessDate, "A");
        this.ApplyContractPeriodStatusChange(fsServiceContractRow);
        if (fsServiceContractRow.BillingType == "STDB" || fsServiceContractRow.IsFixedRateContract.GetValueOrDefault())
          this.ActivateCurrentPeriod();
      }
    }
    return (IEnumerable) list;
  }

  [PXButton(Category = "Processing")]
  [PXUIField(DisplayName = "Suspend")]
  public virtual IEnumerable SuspendContract(PXAdapter adapter)
  {
    List<FSServiceContract> list = adapter.Get<FSServiceContract>().ToList<FSServiceContract>();
    string errorMessage = "";
    foreach (FSServiceContract fsServiceContract in list)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        if (!this.CheckNewContractStatus((PXGraph) this, fsServiceContract, "S", ref errorMessage))
          throw new PXException(errorMessage);
        if ((this.skipStatusSmartPanels || ((PXSelectBase<FSSuspendContractFilter>) this.SuspendContractFilter).AskExt() == 1) && this.CheckDatesApplyOrScheduleStatusChange(((PXSelectBase) this.ServiceContractRecords).Cache, fsServiceContract, ((PXGraph) this).Accessinfo.BusinessDate, ((PXSelectBase<FSSuspendContractFilter>) this.SuspendContractFilter).Current.SuspensionDate))
        {
          this.ApplyOrScheduleStatusChange((PXGraph) this, ((PXSelectBase) this.ServiceContractRecords).Cache, fsServiceContract, ((PXGraph) this).Accessinfo.BusinessDate, ((PXSelectBase<FSSuspendContractFilter>) this.SuspendContractFilter).Current.SuspensionDate, "S");
          this.UpdateSchedulesBySuspendContract(((PXSelectBase<FSSuspendContractFilter>) this.SuspendContractFilter).Current.SuspensionDate);
          this.ForceUpdateCacheAndSave(((PXSelectBase) this.ServiceContractRecords).Cache, (object) fsServiceContract);
        }
        transactionScope.Complete();
      }
    }
    return (IEnumerable) list;
  }

  [PXButton(Category = "Processing")]
  [PXUIField(DisplayName = "Renew")]
  public virtual IEnumerable RenewContract(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    try
    {
      this.IsRenewContract = true;
      bool flag = false;
      foreach (FSServiceContract fsServiceContract1 in adapter.Get<FSServiceContract>().ToList<FSServiceContract>())
      {
        FSServiceContract fsServiceContract2 = fsServiceContract1;
        DateTime? nullable1 = fsServiceContract1.EndDate;
        DateTime dateTime = nullable1.Value;
        DateTime? nullable2 = new DateTime?(dateTime.AddDays(1.0));
        fsServiceContract2.RenewalDate = nullable2;
        if (fsServiceContract1.DurationType != "C")
        {
          FSServiceContract fsServiceContract3 = fsServiceContract1;
          FSServiceContract scRow = fsServiceContract1;
          nullable1 = fsServiceContract1.RenewalDate;
          DateTime? startDate = new DateTime?(nullable1.Value);
          nullable1 = fsServiceContract1.RenewalDate;
          DateTime? actualValue = new DateTime?(nullable1.Value);
          DateTime? endDate = this.GetEndDate(scRow, startDate, actualValue);
          fsServiceContract3.EndDate = endDate;
        }
        else
        {
          FSServiceContract fsServiceContract4 = fsServiceContract1;
          nullable1 = fsServiceContract1.RenewalDate;
          ref DateTime? local = ref nullable1;
          DateTime? nullable3;
          if (!local.HasValue)
          {
            nullable3 = new DateTime?();
          }
          else
          {
            dateTime = local.GetValueOrDefault();
            dateTime = dateTime.AddDays((double) (fsServiceContract1.Duration ?? 1));
            nullable3 = new DateTime?(dateTime.AddDays(-1.0));
          }
          fsServiceContract4.EndDate = nullable3;
        }
        FSServiceContract fsServiceContract5 = fsServiceContract1;
        nullable1 = fsServiceContract1.EndDate;
        DateTime? nullable4 = new DateTime?(nullable1.Value);
        fsServiceContract5.StatusEffectiveUntilDate = nullable4;
        ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Update(fsServiceContract1);
        if (fsServiceContract1.BillingType == "STDB" || fsServiceContract1.IsFixedRateContract.GetValueOrDefault())
        {
          FSContractPeriod fsCurrentContractPeriodRow = PXResultset<FSContractPeriod>.op_Implicit(PXSelectBase<FSContractPeriod, PXSelect<FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>>, OrderBy<Desc<FSContractPeriod.endPeriodDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
          {
            (object) fsServiceContract1.ServiceContractID
          }));
          if (fsCurrentContractPeriodRow != null && fsCurrentContractPeriodRow.Status != "I")
          {
            flag = fsCurrentContractPeriodRow.Status != "A";
            List<FSContractPeriodDet> list = GraphHelper.RowCast<FSContractPeriodDet>((IEnumerable) PXSelectBase<FSContractPeriodDet, PXSelect<FSContractPeriodDet, Where<FSContractPeriodDet.contractPeriodID, Equal<Required<FSContractPeriod.contractPeriodID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) fsCurrentContractPeriodRow.ContractPeriodID
            })).ToList<FSContractPeriodDet>();
            this.GenerateNewContractPeriod(fsCurrentContractPeriodRow, list);
          }
        }
      }
      if (((PXGraph) this).IsDirty)
      {
        ((PXAction) this.Save).Press();
        this.IsRenewContract = false;
      }
      if (((PXSelectBase<FSSetup>) this.SetupRecord).Current != null)
      {
        if (((PXSelectBase<FSSetup>) this.SetupRecord).Current.EnableContractPeriodWhenInvoice.GetValueOrDefault())
        {
          if (flag)
            this.ActivateCurrentPeriod();
        }
      }
    }
    finally
    {
      this.IsRenewContract = false;
    }
    return adapter.Get();
  }

  [PXButton(Category = "Processing")]
  [PXUIField(DisplayName = "Cancel")]
  public virtual IEnumerable CancelContract(PXAdapter adapter)
  {
    List<FSServiceContract> list = adapter.Get<FSServiceContract>().ToList<FSServiceContract>();
    string errorMessage = "";
    foreach (FSServiceContract fsServiceContract in list)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        if (!this.CheckNewContractStatus((PXGraph) this, fsServiceContract, "X", ref errorMessage))
          throw new PXException(errorMessage);
        if ((this.skipStatusSmartPanels || ((PXSelectBase<FSTerminateContractFilter>) this.TerminateContractFilter).AskExt() == 1) && this.CheckDatesApplyOrScheduleStatusChange(((PXSelectBase) this.ServiceContractRecords).Cache, fsServiceContract, ((PXGraph) this).Accessinfo.BusinessDate, ((PXSelectBase<FSTerminateContractFilter>) this.TerminateContractFilter).Current.CancelationDate))
        {
          this.ApplyOrScheduleStatusChange((PXGraph) this, ((PXSelectBase) this.ServiceContractRecords).Cache, fsServiceContract, ((PXGraph) this).Accessinfo.BusinessDate, ((PXSelectBase<FSTerminateContractFilter>) this.TerminateContractFilter).Current.CancelationDate, "X");
          this.UpdateSchedulesByCancelContract(((PXSelectBase<FSTerminateContractFilter>) this.TerminateContractFilter).Current.CancelationDate);
          DateTime? billingInvoiceDate = fsServiceContract.NextBillingInvoiceDate;
          DateTime? cancelationDate = ((PXSelectBase<FSTerminateContractFilter>) this.TerminateContractFilter).Current.CancelationDate;
          if ((billingInvoiceDate.HasValue & cancelationDate.HasValue ? (billingInvoiceDate.GetValueOrDefault() > cancelationDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            fsServiceContract.NextBillingInvoiceDate = ((PXSelectBase<FSTerminateContractFilter>) this.TerminateContractFilter).Current.CancelationDate;
          ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.actions>(((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, (object) "SBP");
          this.ForceUpdateCacheAndSave(((PXSelectBase) this.ServiceContractRecords).Cache, (object) fsServiceContract);
        }
        transactionScope.Complete();
      }
    }
    return (IEnumerable) list;
  }

  [PXButton]
  [PXUIField(DisplayName = "Add Schedule")]
  public virtual void AddSchedule()
  {
  }

  [PXButton(Category = "Inquiries")]
  [PXUIField]
  protected virtual void ViewServiceOrderHistory()
  {
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
    if (current != null)
    {
      Dictionary<string, string> baseParameters = this.GetBaseParameters(current, true, true);
      baseParameters["ContractID"] = current.RefNbr;
      throw new PXRedirectToGIWithParametersRequiredException(new Guid("84b92648-c42e-41e8-855c-4aa9144b9eda"), baseParameters);
    }
  }

  [PXButton(Category = "Inquiries")]
  [PXUIField]
  protected virtual void ViewAppointmentHistory()
  {
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
    if (current != null)
    {
      AppointmentInq instance = PXGraph.CreateInstance<AppointmentInq>();
      ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) instance.Filter).Current = ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) instance.Filter).Insert(new AppointmentInq.AppointmentInqFilter()
      {
        BranchID = current.BranchID,
        BranchLocationID = current.BranchLocationID,
        CustomerID = current.CustomerID,
        CustomerLocationID = current.CustomerLocationID,
        ServiceContractID = current.ServiceContractID
      });
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
      throw requiredException;
    }
  }

  [PXButton(Category = "Inquiries")]
  [PXUIField]
  protected virtual void ViewContractScheduleDetails()
  {
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
    if (current != null)
      throw new PXRedirectToGIWithParametersRequiredException(new Guid("5566eca3-a20a-4d9c-8b1d-bb6b32ae6e9f"), this.GetBaseParameters(current, false, false));
  }

  [PXButton(Category = "Inquiries")]
  [PXUIField]
  protected virtual void ViewCustomerContracts()
  {
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
    if (current != null)
      throw new PXRedirectToGIWithParametersRequiredException(new Guid("4c33d513-ef82-4b7a-aafc-913e856bf89c"), this.GetBaseParameters(current, false, false));
  }

  [PXButton(Category = "Inquiries")]
  [PXUIField]
  protected virtual void ViewCustomerContractSchedules()
  {
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
    if (current != null)
      throw new PXRedirectToGIWithParametersRequiredException(new Guid("09c88688-263d-426a-a19e-de1d0c3d3ad3"), this.GetBaseParameters(current, false, false));
  }

  [PXButton]
  [PXUIField]
  public virtual void ActivatePeriod() => this.ActivateCurrentPeriod();

  [PXButton(Category = "Printing and Emailing")]
  [PXUIField(DisplayName = "Forecast & Print Quote")]
  public virtual void ForecastPrintQuote()
  {
    if (((PXSelectBase) this.ForecastFilter).View.Answer == null)
      ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    bool flag = this.ForecastFilter.AskExtFullyValid(new PXView.InitializePanel((object) this, __methodptr(\u003CForecastPrintQuote\u003Eb__68_0)), (DialogAnswerType) 1, false);
    if (((PXSelectBase) this.ForecastFilter).View.Answer != 1 || !flag)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceContractEntryBase<TGraph, TPrimary, TWhere>.\u003C\u003Ec__DisplayClass68_0 cDisplayClass680 = new ServiceContractEntryBase<TGraph, TPrimary, TWhere>.\u003C\u003Ec__DisplayClass68_0();
    if (((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass680.contract = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass680.parameters = new Dictionary<string, string>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass680.ex = (PXReportRequiredException) null;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass680.graphServiceContractEntry = GraphHelper.Clone<ServiceContractEntryBase<TGraph, TPrimary, TWhere>>(this);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass680.graphServiceContractEntry.IsForcastProcess = true;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass680, __methodptr(\u003CForecastPrintQuote\u003Eb__1)));
  }

  [PXButton(Category = "Printing and Emailing")]
  [PXUIField(DisplayName = "Email Quote")]
  public virtual IEnumerable EmailQuoteContract(PXAdapter adapter)
  {
    return this.Notification(adapter, "FS CONTRACT QUOTE");
  }

  [PXButton(Category = "Other")]
  [PXUIField(DisplayName = "Copy")]
  public virtual IEnumerable CopyContract(PXAdapter adapter)
  {
    List<FSServiceContract> list = adapter.Get<FSServiceContract>().ToList<FSServiceContract>();
    WebDialogResult webDialogResult = ((PXSelectBase<FSCopyContractFilter>) this.CopyContractFilter).AskExt();
    if (webDialogResult != 1)
    {
      if (!((PXGraph) this).IsContractBasedAPI)
        return (IEnumerable) list;
      if (webDialogResult != 6)
        return (IEnumerable) list;
    }
    if (!((PXSelectBase<FSCopyContractFilter>) this.CopyContractFilter).Current.StartDate.HasValue)
      return (IEnumerable) list;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceContractEntryBase<TGraph, TPrimary, TWhere>.\u003C\u003Ec__DisplayClass72_0 cDisplayClass720 = new ServiceContractEntryBase<TGraph, TPrimary, TWhere>.\u003C\u003Ec__DisplayClass72_0();
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass720.clone = GraphHelper.Clone<ServiceContractEntryBase<TGraph, TPrimary, TWhere>>(this);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass720, __methodptr(\u003CCopyContract\u003Eb__0)));
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Notifications", Visible = false)]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable Notification(PXAdapter adapter, [PXString] string notificationCD)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceContractEntryBase<TGraph, TPrimary, TWhere>.\u003C\u003Ec__DisplayClass75_0 cDisplayClass750 = new ServiceContractEntryBase<TGraph, TPrimary, TWhere>.\u003C\u003Ec__DisplayClass75_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass750.notificationCD = notificationCD;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass750.massProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass750.orders = adapter.Get<FSServiceContract>().ToArray<FSServiceContract>();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass750.orders.Length != 0)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass750, __methodptr(\u003CNotification\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass750.orders;
  }

  /// <summary>Enable or Disable the ServiceContract fields.</summary>
  public virtual void EnableDisable_Document(PXCache cache, FSServiceContract fsServiceContractRow)
  {
    bool flag1 = fsServiceContractRow.isEditable() || this.IsForcastProcess;
    bool flag2 = ServiceContractEntryBase<TGraph, TPrimary, TWhere>.CanDeleteServiceContract(fsServiceContractRow);
    ((PXSelectBase) this.ServiceContractRecords).Cache.AllowInsert = true;
    ((PXSelectBase) this.ServiceContractRecords).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ServiceContractRecords).Cache.AllowDelete = flag2;
    ((PXSelectBase) this.ServiceContractSelected).Cache.AllowInsert = true;
    ((PXSelectBase) this.ServiceContractSelected).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ServiceContractSelected).Cache.AllowDelete = flag2;
    ((PXSelectBase) this.ContractPeriodRecords).Cache.AllowInsert = ((PXSelectBase) this.ContractPeriodRecords).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ContractPeriodRecords).Cache.AllowDelete = flag2;
    ((PXSelectBase) this.ContractPeriodDetRecords).Cache.AllowInsert = ((PXSelectBase) this.ContractPeriodDetRecords).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ContractPeriodDetRecords).Cache.AllowDelete = flag2;
    ((PXSelectBase) this.SalesPriceLines).Cache.AllowInsert = ((PXSelectBase) this.SalesPriceLines).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.SalesPriceLines).Cache.AllowDelete = flag2;
    ((PXSelectBase) this.ContractHistoryItems).Cache.AllowInsert = ((PXSelectBase) this.ContractHistoryItems).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.ContractHistoryItems).Cache.AllowDelete = flag2;
    bool flag3 = flag1 || ((PXGraph) this).IsImport;
    PXUIFieldAttribute.SetEnabled<FSContractPeriodFilter.actions>(((PXSelectBase) this.ContractPeriodFilter).Cache, (object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, flag3);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodFilter.postDocRefNbr>(((PXSelectBase) this.ContractPeriodFilter).Cache, (object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, flag3);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodFilter.standardizedBillingTotal>(((PXSelectBase) this.ContractPeriodFilter).Cache, (object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, flag3);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodFilter.contractPeriodID>(((PXSelectBase) this.ContractPeriodFilter).Cache, (object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, flag3);
    PXUIFieldAttribute.SetVisible<FSCopyContractFilter.refNbr>(((PXSelectBase) this.CopyContractFilter).Cache, (object) ((PXSelectBase<FSCopyContractFilter>) this.CopyContractFilter).Current, this.RefNbrRequired());
    ((PXAction) this.addSchedule).SetEnabled(flag1);
    ((PXAction) this.activatePeriod).SetEnabled(this.EnableDisableActivatePeriodButton(fsServiceContractRow, ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current));
    if (!flag1)
      return;
    bool flag4 = fsServiceContractRow.Status == "D";
    bool flag5 = fsServiceContractRow.Status == "D";
    bool flag6 = fsServiceContractRow.Status == "D";
    bool flag7 = fsServiceContractRow.Status == "D";
    bool flag8 = fsServiceContractRow.Status == "D";
    PXUIFieldAttribute.SetEnabled<FSServiceContract.billingType>(cache, (object) fsServiceContractRow, flag7);
    PXUIFieldAttribute.SetEnabled<FSServiceContract.startDate>(cache, (object) fsServiceContractRow, flag4);
    PXUIFieldAttribute.SetEnabled<FSServiceContract.expirationType>(cache, (object) fsServiceContractRow, flag5);
    PXUIFieldAttribute.SetEnabled<FSServiceContract.billingPeriod>(cache, (object) fsServiceContractRow, flag8);
    PXDefaultAttribute.SetPersistingCheck<FSServiceContract.startDate>(cache, (object) fsServiceContractRow, flag4 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetEnabled<FSServiceContract.endDate>(cache, (object) fsServiceContractRow, flag6 || ((PXGraph) this).IsCopyPasteContext);
    PXDefaultAttribute.SetPersistingCheck<FSServiceContract.endDate>(cache, (object) fsServiceContractRow, fsServiceContractRow.ExpirationType == "E" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    int num;
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current != null)
    {
      bool? multipleBillingOptions = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.CustomerMultipleBillingOptions;
      bool flag9 = false;
      if (multipleBillingOptions.GetValueOrDefault() == flag9 & multipleBillingOptions.HasValue)
      {
        num = fsServiceContractRow.BillingType == "STDB" ? 1 : 0;
        goto label_5;
      }
    }
    num = 0;
label_5:
    bool flag10 = num != 0;
    PXUIFieldAttribute.SetVisible<FSServiceContract.usageBillingCycleID>(cache, (object) fsServiceContractRow, flag10);
  }

  /// <summary>
  /// Enables/Disables the actions defined for ServiceContract
  /// It's called by RowSelected event of FSServiceContract.
  /// </summary>
  public virtual void EnableDisable_ActionButtons(
    PXGraph graph,
    PXCache cache,
    FSServiceContract fsServiceContractRow)
  {
    if (cache.GetStatus((object) fsServiceContractRow) == 2)
    {
      ((PXAction) this.activateContract).SetEnabled(false);
      ((PXAction) this.suspendContract).SetEnabled(false);
      ((PXAction) this.cancelContract).SetEnabled(false);
      ((PXAction) this.addSchedule).SetEnabled(false);
      ((PXAction) this.viewContractScheduleDetails).SetEnabled(false);
      ((PXAction) this.viewServiceOrderHistory).SetEnabled(false);
      ((PXAction) this.viewAppointmentHistory).SetEnabled(false);
      ((PXAction) this.viewCustomerContracts).SetEnabled(false);
      ((PXAction) this.viewCustomerContractSchedules).SetEnabled(false);
      ((PXAction) this.activateContract).SetEnabled(false);
      ((PXAction) this.renewContract).SetEnabled(false);
      ((PXAction) this.emailQuoteContract).SetEnabled(false);
      ((PXAction) this.forecastPrintQuote).SetEnabled(false);
    }
    else
    {
      string empty = string.Empty;
      bool flag1 = this.CheckNewContractStatus((PXGraph) this, fsServiceContractRow, "A", ref empty) && (fsServiceContractRow.BillingType == "APFB" || (fsServiceContractRow.BillingType == "STDB" || fsServiceContractRow.IsFixedRateContract.GetValueOrDefault()) && ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Select(Array.Empty<object>()).Count > 0) || ((PXGraph) this).IsImport;
      bool flag2 = this.CheckNewContractStatus((PXGraph) this, fsServiceContractRow, "S", ref empty);
      bool flag3 = this.CheckNewContractStatus((PXGraph) this, fsServiceContractRow, "X", ref empty);
      bool flag4 = cache.GetStatus((object) fsServiceContractRow) != 2;
      ((PXAction) this.forecastPrintQuote).SetEnabled(fsServiceContractRow.Status != "E" && fsServiceContractRow.Status != "X");
      ((PXAction) this.emailQuoteContract).SetEnabled(fsServiceContractRow.HasForecast.GetValueOrDefault() && fsServiceContractRow.Status != "E" && fsServiceContractRow.Status != "X");
      ((PXAction) this.copyContract).SetEnabled(true);
      ((PXAction) this.activateContract).SetEnabled(flag1);
      ((PXAction) this.suspendContract).SetEnabled(flag2);
      ((PXAction) this.cancelContract).SetEnabled(flag3);
      ((PXAction) this.addSchedule).SetEnabled(flag4);
      ((PXAction) this.viewContractScheduleDetails).SetEnabled(flag4);
      ((PXAction) this.viewServiceOrderHistory).SetEnabled(flag4);
      ((PXAction) this.viewAppointmentHistory).SetEnabled(flag4);
      ((PXAction) this.viewCustomerContracts).SetEnabled(flag4);
      ((PXAction) this.viewCustomerContractSchedules).SetEnabled(flag4);
      ((PXAction) this.activatePeriod).SetEnabled(this.EnableDisableActivatePeriodButton(fsServiceContractRow, ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current));
      ((PXAction) this.renewContract).SetEnabled(fsServiceContractRow.Status == "A" && fsServiceContractRow.ExpirationType == "R");
    }
  }

  public virtual void ValidateDates(
    PXCache cache,
    FSServiceContract fsServiceContractRow,
    PXResultset<FSContractSchedule> contractRows)
  {
    if (!fsServiceContractRow.StartDate.HasValue)
      return;
    if (((IEnumerable<PXResult<FSContractSchedule>>) contractRows).AsEnumerable<PXResult<FSContractSchedule>>().Where<PXResult<FSContractSchedule>>((Func<PXResult<FSContractSchedule>, bool>) (y =>
    {
      DateTime? startDate1 = PXResult<FSContractSchedule>.op_Implicit(y).StartDate;
      DateTime? startDate2 = fsServiceContractRow.StartDate;
      if ((startDate1.HasValue & startDate2.HasValue ? (startDate1.GetValueOrDefault() < startDate2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return false;
      DateTime? startDate3 = PXResult<FSContractSchedule>.op_Implicit(y).StartDate;
      DateTime? valueOriginal = (DateTime?) cache.GetValueOriginal<FSServiceContract.startDate>((object) fsServiceContractRow);
      if (startDate3.HasValue != valueOriginal.HasValue)
        return true;
      return startDate3.HasValue && startDate3.GetValueOrDefault() != valueOriginal.GetValueOrDefault();
    })).Count<PXResult<FSContractSchedule>>() > 0)
      cache.RaiseExceptionHandling<FSServiceContract.startDate>((object) fsServiceContractRow, (object) fsServiceContractRow.StartDate, (Exception) new PXSetPropertyException("The dates are invalid. The contract start date cannot be later than the start date of any of the contract schedules.", (PXErrorLevel) 4));
    if (((IEnumerable<PXResult<FSContractSchedule>>) contractRows).AsEnumerable<PXResult<FSContractSchedule>>().Where<PXResult<FSContractSchedule>>((Func<PXResult<FSContractSchedule>, bool>) (y =>
    {
      DateTime? endDate1 = PXResult<FSContractSchedule>.op_Implicit(y).EndDate;
      DateTime? endDate2 = fsServiceContractRow.EndDate;
      if ((endDate1.HasValue & endDate2.HasValue ? (endDate1.GetValueOrDefault() > endDate2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return false;
      DateTime? endDate3 = PXResult<FSContractSchedule>.op_Implicit(y).EndDate;
      DateTime? valueOriginal = (DateTime?) cache.GetValueOriginal<FSServiceContract.endDate>((object) fsServiceContractRow);
      if (endDate3.HasValue != valueOriginal.HasValue)
        return true;
      return endDate3.HasValue && endDate3.GetValueOrDefault() != valueOriginal.GetValueOrDefault();
    })).Count<PXResult<FSContractSchedule>>() > 0)
      cache.RaiseExceptionHandling<FSServiceContract.endDate>((object) fsServiceContractRow, (object) fsServiceContractRow.EndDate, (Exception) new PXSetPropertyException("The dates are invalid. The contract end date cannot be earlier than the end date of any of the contract schedules.", (PXErrorLevel) 4));
    DateTime? nullable = fsServiceContractRow.EndDate;
    DateTime dateTime1;
    if (nullable.HasValue)
    {
      nullable = fsServiceContractRow.StartDate;
      dateTime1 = nullable.Value;
      ref DateTime local = ref dateTime1;
      nullable = fsServiceContractRow.EndDate;
      DateTime dateTime2 = nullable.Value;
      if (local.CompareTo(dateTime2) > 0)
      {
        cache.RaiseExceptionHandling<FSServiceContract.startDate>((object) fsServiceContractRow, (object) fsServiceContractRow.StartDate, (Exception) new PXSetPropertyException("The dates are invalid. The end date cannot be earlier than the start date.", (PXErrorLevel) 5));
        cache.RaiseExceptionHandling<FSServiceContract.endDate>((object) fsServiceContractRow, (object) fsServiceContractRow.EndDate, (Exception) new PXSetPropertyException("The dates are invalid. The end date cannot be earlier than the start date.", (PXErrorLevel) 5));
      }
    }
    if (fsServiceContractRow.ExpirationType == "E" && fsServiceContractRow.UpcomingStatus != null && fsServiceContractRow.UpcomingStatus != "E")
    {
      nullable = fsServiceContractRow.EndDate;
      dateTime1 = nullable.Value;
      DateTime date1 = dateTime1.Date;
      nullable = fsServiceContractRow.StatusEffectiveUntilDate;
      dateTime1 = nullable.Value;
      DateTime date2 = dateTime1.Date;
      if (date1 <= date2)
        cache.RaiseExceptionHandling<FSServiceContract.endDate>((object) fsServiceContractRow, (object) fsServiceContractRow.EndDate, (Exception) new PXSetPropertyException("The expiration date must be later than the upcoming status date.", (PXErrorLevel) 5));
    }
    if (!(fsServiceContractRow.UpcomingStatus == "E"))
      return;
    nullable = fsServiceContractRow.EndDate;
    if (!nullable.HasValue)
      return;
    nullable = fsServiceContractRow.EndDate;
    dateTime1 = nullable.Value;
    DateTime date3 = dateTime1.Date;
    nullable = ((PXGraph) this).Accessinfo.BusinessDate;
    dateTime1 = nullable.Value;
    DateTime date4 = dateTime1.Date;
    if (!(date3 <= date4) || this.IsRenewContract || this.IsForcastProcess)
      return;
    cache.RaiseExceptionHandling<FSServiceContract.endDate>((object) fsServiceContractRow, (object) fsServiceContractRow.EndDate, (Exception) new PXSetPropertyException("The expiration date must be later than the business date.", (PXErrorLevel) 5));
  }

  /// <summary>
  /// Sets the price configured in Price List for a Service when the <c>SourcePrice</c> is modified.
  /// </summary>
  public virtual Decimal? GetSalesPrice(PXCache cache, FSSalesPrice fsSalesPriceRow)
  {
    Decimal? salesPrice = new Decimal?();
    FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
    SalesPriceSet customerContract = FSPriceManagement.CalculateSalesPriceWithCustomerContract(cache, new int?(), new int?(), new int?(), current.CustomerID, current.CustomerLocationID, new bool?(), fsSalesPriceRow.InventoryID, new int?(), new Decimal?(0M), fsSalesPriceRow.UOM, (current.StartDate ?? cache.Graph.Accessinfo.BusinessDate).Value, fsSalesPriceRow.Mem_UnitPrice, true, (PX.Objects.CM.CurrencyInfo) null, true);
    switch (customerContract.ErrorCode)
    {
      case "OK":
        salesPrice = customerContract.Price;
        break;
      case "UOM_INCONSISTENCY":
        PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(cache.Graph, fsSalesPriceRow.InventoryID);
        cache.RaiseExceptionHandling<FSSalesPrice.uOM>((object) fsSalesPriceRow, (object) fsSalesPriceRow.UOM, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefix("There is an inconsistency in the UOM defined on the Sales Prices (AR202000) form and on the Non-Stock Items (IN202000) form for the {0} service.", new object[1]
        {
          (object) inventoryItemRow.InventoryCD
        }), (PXErrorLevel) 4));
        break;
      default:
        throw new PXException(customerContract.ErrorCode);
    }
    return salesPrice;
  }

  /// <summary>
  /// Updates all prices of <c>FSSalesPrice</c> lines.
  /// </summary>
  /// <param name="cache">PXCache instance.</param>
  /// <param name="fsServiceContractRow">FSServiceContract current row.</param>
  public virtual void UpdateSalesPrices(PXCache cache, FSServiceContract fsServiceContractRow)
  {
    foreach (PXResult<FSSalesPrice> pxResult in ((PXSelectBase<FSSalesPrice>) this.SalesPriceLines).Select(Array.Empty<object>()))
    {
      FSSalesPrice fsSalesPriceRow = PXResult<FSSalesPrice>.op_Implicit(pxResult);
      fsSalesPriceRow.Mem_UnitPrice = new Decimal?(this.GetSalesPrice(((PXSelectBase) this.SalesPriceLines).Cache, fsSalesPriceRow) ?? 0.0M);
      PXUIFieldAttribute.SetEnabled<FSSalesPrice.mem_UnitPrice>(((PXSelectBase) this.SalesPriceLines).Cache, (object) fsSalesPriceRow, ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.SourcePrice == "C");
    }
  }

  /// <summary>Verifies the cache of the views for FSSalesPrice.</summary>
  public virtual void SetUnitPriceForSalesPricesRows(FSServiceContract fsServiceContractRow)
  {
    if (!((PXSelectBase) this.SalesPriceLines).Cache.IsDirty)
      return;
    foreach (PXResult<FSSalesPrice> pxResult in ((PXSelectBase<FSSalesPrice>) this.SalesPriceLines).Select(Array.Empty<object>()))
    {
      FSSalesPrice fsSalesPrice1 = PXResult<FSSalesPrice>.op_Implicit(pxResult);
      Decimal? nullable1;
      if (fsServiceContractRow.SourcePrice == "P")
      {
        FSSalesPrice fsSalesPrice2 = fsSalesPrice1;
        nullable1 = new Decimal?();
        Decimal? nullable2 = nullable1;
        fsSalesPrice2.UnitPrice = nullable2;
      }
      else
      {
        FSSalesPrice fsSalesPrice3 = fsSalesPrice1;
        nullable1 = fsSalesPrice1.Mem_UnitPrice;
        Decimal? nullable3 = new Decimal?(nullable1 ?? 0.0M);
        fsSalesPrice3.UnitPrice = nullable3;
      }
      ((PXSelectBase) this.SalesPriceLines).Cache.SetStatus((object) fsSalesPrice1, (PXEntryStatus) 1);
    }
  }

  public virtual void SetVisibleActivatePeriodButton(
    PXCache cache,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow == null)
      return;
    ((PXAction) this.activatePeriod).SetVisible(fsServiceContractRow.BillingType != "APFB" && ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current.Actions == "MBP");
  }

  public virtual void SetVisibleContractBillingSettings(
    PXCache cache,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow == null)
      return;
    bool flag = fsServiceContractRow.BillingType == "APFB";
    PXUIFieldAttribute.SetVisible<FSServiceContract.billingPeriod>(cache, (object) fsServiceContractRow, !flag);
    PXUIFieldAttribute.SetVisible<FSServiceContract.lastBillingInvoiceDate>(cache, (object) fsServiceContractRow, !flag);
    PXUIFieldAttribute.SetVisible<FSServiceContract.nextBillingInvoiceDate>(cache, (object) fsServiceContractRow, !flag);
    PXUIFieldAttribute.SetVisible<FSServiceContract.sourcePrice>(cache, (object) fsServiceContractRow, flag);
  }

  public virtual void SetUpcommingStatus(FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow == null)
      return;
    if (fsServiceContractRow.ExpirationType == "U")
    {
      if (!(fsServiceContractRow.UpcomingStatus == "E"))
        return;
      fsServiceContractRow.UpcomingStatus = (string) null;
    }
    else
    {
      if (fsServiceContractRow.UpcomingStatus != null)
        return;
      fsServiceContractRow.UpcomingStatus = "E";
    }
  }

  public virtual void SetUsageBillingCycle(FSServiceContract fsServiceContractRow)
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current == null)
      return;
    bool? multipleBillingOptions = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.CustomerMultipleBillingOptions;
    bool flag = false;
    if (!(multipleBillingOptions.GetValueOrDefault() == flag & multipleBillingOptions.HasValue) || !(fsServiceContractRow.BillingType == "STDB") || !(fsServiceContractRow.BillTo == "C"))
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsServiceContractRow.CustomerID
    }));
    if (customer == null)
      return;
    FSxCustomer extension = PXCache<PX.Objects.AR.Customer>.GetExtension<FSxCustomer>(customer);
    fsServiceContractRow.UsageBillingCycleID = extension.BillingCycleID;
  }

  public virtual void SetBillInfo(PXCache cache, FSServiceContract fsServiceContractRow)
  {
    bool flag = fsServiceContractRow.BillTo == "C";
    PXUIFieldAttribute.SetEnabled<FSServiceContract.billCustomerID>(cache, (object) fsServiceContractRow, !flag);
    PXUIFieldAttribute.SetEnabled<FSServiceContract.billLocationID>(cache, (object) fsServiceContractRow, !flag);
    PXDefaultAttribute.SetPersistingCheck<FSServiceContract.billCustomerID>(cache, (object) fsServiceContractRow, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<FSServiceContract.billLocationID>(cache, (object) fsServiceContractRow, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }

  public virtual void SetBillTo(FSServiceContract fSServiceContractRow)
  {
    int? billCustomerId = fSServiceContractRow.BillCustomerID;
    int? customerId = fSServiceContractRow.CustomerID;
    if (billCustomerId.GetValueOrDefault() == customerId.GetValueOrDefault() & billCustomerId.HasValue == customerId.HasValue)
      fSServiceContractRow.BillTo = "C";
    else
      fSServiceContractRow.BillTo = "S";
  }

  public virtual Dictionary<string, string> GetBaseParameters(
    FSServiceContract fsServiceContractRow,
    bool loadBranch,
    bool loadBranchLocation)
  {
    Dictionary<string, string> baseParameters = new Dictionary<string, string>();
    if (loadBranch)
    {
      PX.SM.Branch branch = PXResultset<PX.SM.Branch>.op_Implicit(PXSelectBase<PX.SM.Branch, PXSelect<PX.SM.Branch, Where<PX.SM.Branch.branchID, Equal<Required<PX.SM.Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXGraph) this).Accessinfo.BranchID
      }));
      if (branch != null)
        baseParameters["BranchID"] = branch.BranchCD;
    }
    if (loadBranchLocation)
    {
      FSBranchLocation fsBranchLocation = PXResultset<FSBranchLocation>.op_Implicit(PXSelectBase<FSBranchLocation, PXSelect<FSBranchLocation, Where<FSBranchLocation.branchID, Equal<Required<PX.SM.Branch.branchID>>, And<FSBranchLocation.branchLocationID, Equal<Required<FSBranchLocation.branchLocationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ((PXGraph) this).Accessinfo.BranchID,
        (object) fsServiceContractRow.BranchLocationID
      }));
      if (fsBranchLocation != null)
        baseParameters["BranchLocationID"] = fsBranchLocation.BranchLocationCD;
    }
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsServiceContractRow.CustomerID
    }));
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsServiceContractRow.CustomerLocationID
    }));
    if (customer != null && location != null)
    {
      baseParameters["CustomerID"] = customer.AcctCD;
      baseParameters["CustomerLocationID"] = location.LocationCD;
    }
    baseParameters["ServiceContractRefNbr"] = fsServiceContractRow.RefNbr;
    return baseParameters;
  }

  public virtual void SetDefaultBillingRule(
    PXCache cache,
    FSContractPeriodDet fsContractPeriodDetRow)
  {
    string str = "TIME";
    if (fsContractPeriodDetRow.LineType == "NSTKI")
      str = "FLRA";
    else if (fsContractPeriodDetRow.LineType == "SERVI")
    {
      PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) this, fsContractPeriodDetRow.InventoryID);
      if (inventoryItemRow != null)
      {
        FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
        if (extension != null)
        {
          str = extension.BillingRule;
          if (extension.BillingRule == "NONE")
            str = "TIME";
        }
      }
    }
    cache.SetValueExt<FSContractPeriodDet.billingRule>((object) fsContractPeriodDetRow, (object) str);
  }

  public virtual void SetDefaultQtyTime(PXCache cache, FSContractPeriodDet fsContractPeriodDetRow)
  {
    if (fsContractPeriodDetRow.BillingRule == "TIME")
    {
      cache.SetValueExt<FSContractPeriodDet.time>((object) fsContractPeriodDetRow, (object) 60);
      cache.SetValueExt<FSContractPeriodDet.qty>((object) fsContractPeriodDetRow, (object) 0M);
    }
    else
    {
      if (!(fsContractPeriodDetRow.BillingRule == "FLRA"))
        return;
      cache.SetValueExt<FSContractPeriodDet.time>((object) fsContractPeriodDetRow, (object) 0);
      cache.SetValueExt<FSContractPeriodDet.qty>((object) fsContractPeriodDetRow, (object) 1.0M);
    }
  }

  public static Decimal? GetSalesPriceItemInfo(
    PXCache cacheDetail,
    FSServiceContract fsServiceContractRow,
    FSContractPeriodDet fsContractPeriodDet)
  {
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(cacheDetail.Graph, (int?) fsContractPeriodDet?.InventoryID);
    if (inventoryItemRow == null)
      return new Decimal?();
    SalesPriceSet customerContract = FSPriceManagement.CalculateSalesPriceWithCustomerContract(cacheDetail, fsServiceContractRow.ServiceContractID, new int?(), new int?(), fsServiceContractRow.CustomerID, fsServiceContractRow.CustomerLocationID, new bool?(), inventoryItemRow.InventoryID, new int?(), (Decimal?) fsContractPeriodDet?.Qty, fsContractPeriodDet?.UOM, cacheDetail.Graph.Accessinfo.BusinessDate.Value, fsContractPeriodDet.RecurringUnitPrice, true, (PX.Objects.CM.CurrencyInfo) null, true);
    return !(customerContract.ErrorCode == "UOM_INCONSISTENCY") ? customerContract.Price : throw new PXException(PXMessages.LocalizeFormatNoPrefix("There is an inconsistency in the UOM defined on the Sales Prices (AR202000) form and on the Non-Stock Items (IN202000) form for the {0} service.", new object[1]
    {
      (object) inventoryItemRow.InventoryCD
    }), new object[1]{ (object) (PXErrorLevel) 4 });
  }

  private static void EnableDisableContractPeriodDet(
    PXCache cache,
    FSContractPeriodDet fsContractPeriodDetRow)
  {
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.billingRule>(cache, (object) fsContractPeriodDetRow, fsContractPeriodDetRow.LineType == "SERVI");
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.time>(cache, (object) fsContractPeriodDetRow, fsContractPeriodDetRow.BillingRule == "TIME");
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.qty>(cache, (object) fsContractPeriodDetRow, fsContractPeriodDetRow.BillingRule == "FLRA");
  }

  private static string GetContractPeriodFilterDefaultAction(PXGraph graph, int? serviceContractID)
  {
    return ((IQueryable<PXResult<FSContractPeriod>>) PXSelectBase<FSContractPeriod, PXSelect<FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>.Config>.Select(graph, new object[1]
    {
      (object) serviceContractID
    })).Count<PXResult<FSContractPeriod>>() <= 0 ? "MBP" : "SBP";
  }

  private void AmountFieldSelectingHandler(
    PXCache cache,
    PXFieldSelectingEventArgs e,
    string name,
    string billingRule,
    int? time,
    Decimal? qty)
  {
    if (((PXGraph) this).IsCopyPasteContext)
      return;
    switch (billingRule)
    {
      case "TIME":
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(6), new bool?(), name, new bool?(false), new int?(), "#### h ## m", (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
        TimeSpan timeSpan = new TimeSpan(0, 0, time.GetValueOrDefault(), 0);
        int num = timeSpan.Days * 24 + timeSpan.Hours;
        e.ReturnValue = (object) string.Format("{1,4}{2:00}", (object) timeSpan.Days, (object) num, (object) timeSpan.Minutes);
        break;
      case "FLRA":
        e.ReturnState = (object) PXDecimalState.CreateInstance(e.ReturnState, new int?(2), name, new bool?(false), new int?(-1), new Decimal?(), new Decimal?());
        e.ReturnValue = (object) qty.GetValueOrDefault().ToString((IFormatProvider) CultureInfo.InvariantCulture);
        break;
    }
  }

  private static void SetRegularPrice(
    PXCache cache,
    FSContractPeriodDet fsContractPeriodDetRow,
    FSServiceContract fsServiceContractRow)
  {
    Decimal? salesPriceItemInfo = ServiceContractEntryBase<TGraph, TPrimary, TWhere>.GetSalesPriceItemInfo(cache, fsServiceContractRow, fsContractPeriodDetRow);
    cache.SetValueExt<FSContractPeriodDet.regularPrice>((object) fsContractPeriodDetRow, (object) salesPriceItemInfo.GetValueOrDefault());
  }

  private void InsertContractAction(object row, PXDBOperation operation, bool? changeRecurrence = false)
  {
    FSServiceContract fsServiceContractRow = (FSServiceContract) null;
    FSSchedule fsScheduleRow = (FSSchedule) null;
    this.insertContractActionForSchedules = false;
    if (!FSServiceContract.TryParse(row, out fsServiceContractRow) && !FSSchedule.TryParse(row, out fsScheduleRow) || operation != 2 && operation != 1)
      return;
    FSContractAction fsContractAction1 = new FSContractAction();
    fsContractAction1.Type = fsServiceContractRow != null ? "C" : "S";
    fsContractAction1.ServiceContractID = fsServiceContractRow != null ? fsServiceContractRow.ServiceContractID : fsScheduleRow.EntityID;
    fsContractAction1.ActionBusinessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    FSContractAction fsContractAction2;
    if (operation == 2)
    {
      FSContractAction fsContractAction3 = ((PXSelectBase<FSContractAction>) this.ContractHistoryItems).Insert(fsContractAction1);
      if (!this.IsCopyContract && fsServiceContractRow.OrigServiceContractRefNbr == null)
      {
        fsContractAction3.Action = "N";
        fsContractAction2 = ((PXSelectBase<FSContractAction>) this.ContractHistoryItems).Update(fsContractAction3);
      }
      else
      {
        fsContractAction3.Action = "C";
        fsContractAction3.OrigServiceContractRefNbr = fsServiceContractRow.OrigServiceContractRefNbr;
        fsContractAction2 = ((PXSelectBase<FSContractAction>) this.ContractHistoryItems).Update(fsContractAction3);
        fsServiceContractRow.OrigServiceContractRefNbr = (string) null;
      }
    }
    else if (operation == 1 && fsServiceContractRow != null && this.IsRenewContract)
    {
      FSContractAction fsContractAction4 = ((PXSelectBase<FSContractAction>) this.ContractHistoryItems).Insert(fsContractAction1);
      fsContractAction4.Action = "R";
      fsContractAction4.EffectiveDate = fsServiceContractRow.RenewalDate;
    }
    else
    {
      if (operation != 1 || fsServiceContractRow != null && !this.isStatusChanged)
        return;
      FSContractAction fsContractAction5 = ((PXSelectBase<FSContractAction>) this.ContractHistoryItems).Insert(fsContractAction1);
      if (fsServiceContractRow != null)
      {
        fsContractAction5.Action = this.GetActionFromServiceContractStatus(fsServiceContractRow);
        fsContractAction5.EffectiveDate = fsServiceContractRow.StatusEffectiveFromDate;
        if (fsContractAction5.Action == "A")
          this.insertContractActionForSchedules = true;
        this.isStatusChanged = false;
      }
      else
      {
        fsContractAction5.Action = this.GetActionFromSchedule(fsScheduleRow);
        fsContractAction5.ScheduleNextExecutionDate = fsScheduleRow.StartDate;
        fsContractAction5.ScheduleRecurrenceDescr = fsScheduleRow.RecurrenceDescription;
        fsContractAction5.ScheduleRefNbr = fsScheduleRow.RefNbr;
        fsContractAction5.ScheduleChangeRecurrence = new bool?(changeRecurrence.GetValueOrDefault());
      }
      fsContractAction2 = ((PXSelectBase<FSContractAction>) this.ContractHistoryItems).Update(fsContractAction5);
    }
    ((PXSelectBase) this.ContractHistoryItems).Cache.Persist((PXDBOperation) 2);
  }

  public virtual void InsertContractActionBySchedules(PXDBOperation operation)
  {
    if (!this.insertContractActionForSchedules)
      return;
    foreach (PXResult<ActiveSchedule> pxResult in ((PXSelectBase<ActiveSchedule>) this.ActiveScheduleRecords).Select(Array.Empty<object>()))
    {
      ActiveSchedule row = PXResult<ActiveSchedule>.op_Implicit(pxResult);
      this.InsertContractAction((object) row, operation, row.ChangeRecurrence);
    }
  }

  public virtual ScheduleProjection GetNextExecutionProjection(
    PXCache cache,
    FSSchedule fsScheduleRow,
    DateTime startDate)
  {
    ScheduleProjection executionProjection = new ScheduleProjection();
    DateTime dateTime1 = startDate.AddYears(1);
    DateTime? nullable;
    if (fsScheduleRow.LastGeneratedElementDate.HasValue)
    {
      DateTime dateTime2 = dateTime1;
      nullable = fsScheduleRow.LastGeneratedElementDate;
      if ((nullable.HasValue ? (dateTime2 <= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable = fsScheduleRow.LastGeneratedElementDate;
        dateTime1 = nullable.Value.AddYears(2);
      }
    }
    Period period1 = new Period(startDate, new DateTime?(dateTime1));
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList1 = new List<PX.Objects.FS.Scheduler.Schedule>();
    TimeSlotGenerator timeSlotGenerator = new TimeSlotGenerator();
    List<PX.Objects.FS.Scheduler.Schedule> schedule = MapFSScheduleToSchedule.convertFSScheduleToSchedule(cache, fsScheduleRow, new DateTime?(dateTime1), "NRSC", period1);
    Period period2 = period1;
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList2 = schedule;
    int? generationID = new int?();
    executionProjection.Date = new DateTime?(timeSlotGenerator.GenerateCalendar(period2, (IEnumerable<PX.Objects.FS.Scheduler.Schedule>) scheduleList2, generationID)[0].DateTimeBegin);
    nullable = executionProjection.Date;
    executionProjection.BeginDateOfWeek = new DateTime?(SharedFunctions.StartOfWeek(nullable.Value, DayOfWeek.Monday));
    return executionProjection;
  }

  public virtual bool CheckNewContractStatus(
    PXGraph graph,
    FSServiceContract fsServiceContractRow,
    string newContractStatus,
    ref string errorMessage)
  {
    errorMessage = string.Empty;
    if (((PXGraph) this).IsImport || fsServiceContractRow.Status == "D" && newContractStatus == "A" || fsServiceContractRow.Status == "A" && newContractStatus == "S" || fsServiceContractRow.Status == "A" && newContractStatus == "X" || fsServiceContractRow.Status == "A" && newContractStatus == "E" || fsServiceContractRow.Status == "S" && newContractStatus == "A" || fsServiceContractRow.Status == "S" && newContractStatus == "X")
      return true;
    errorMessage = "The transition of the appointment status is invalid.";
    return false;
  }

  public virtual void ApplyOrScheduleStatusChange(
    PXGraph graph,
    PXCache cache,
    FSServiceContract fsServiceContractRow,
    DateTime? businessDate,
    DateTime? effectiveDate,
    string newStatus)
  {
    if (fsServiceContractRow == null || !businessDate.HasValue || !effectiveDate.HasValue)
      return;
    if (newStatus == "E")
    {
      FSServiceOrder fsServiceOrder = PXResultset<FSServiceOrder>.op_Implicit(PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.completed, Equal<False>, And<FSServiceOrder.closed, Equal<False>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
      {
        (object) fsServiceContractRow.ServiceContractID
      }));
      PXResultset<FSAppointment> pxResultset;
      if (fsServiceOrder != null)
        pxResultset = (PXResultset<FSAppointment>) null;
      else
        pxResultset = PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.completed, Equal<False>, And<FSAppointment.closed, Equal<False>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
        {
          (object) fsServiceContractRow.ServiceContractID
        });
      if (PXResultset<FSAppointment>.op_Implicit(pxResultset) != null || fsServiceOrder != null)
        throw new PXException(PXMessages.LocalizeFormatNoPrefix("The document cannot be assigned the Expired status because at least one appointment or service order associated with the document has the status other than Completed or Closed.", Array.Empty<object>()), new object[1]
        {
          (object) (PXErrorLevel) 4
        });
    }
    DateTime date1 = effectiveDate.Value.Date;
    DateTime dateTime = businessDate.Value;
    DateTime date2 = dateTime.Date;
    if (date1 == date2)
    {
      cache.SetValueExt<FSServiceContract.status>((object) fsServiceContractRow, (object) newStatus);
      if (newStatus != "E" && newStatus != "X")
      {
        fsServiceContractRow.UpcomingStatus = fsServiceContractRow.ExpirationType != "U" ? "E" : (string) null;
        fsServiceContractRow.StatusEffectiveFromDate = effectiveDate;
        fsServiceContractRow.StatusEffectiveUntilDate = fsServiceContractRow.ExpirationType != "U" ? fsServiceContractRow.EndDate : new DateTime?();
      }
      else
      {
        fsServiceContractRow.UpcomingStatus = (string) null;
        fsServiceContractRow.StatusEffectiveFromDate = effectiveDate;
        fsServiceContractRow.StatusEffectiveUntilDate = new DateTime?();
      }
      if (!(newStatus == "X") && !(newStatus == "S"))
        return;
      this.DeleteScheduledAppSO(fsServiceContractRow, effectiveDate);
    }
    else
    {
      dateTime = effectiveDate.Value;
      DateTime date3 = dateTime.Date;
      dateTime = businessDate.Value;
      DateTime date4 = dateTime.Date;
      if (!(date3 > date4))
        return;
      fsServiceContractRow.UpcomingStatus = newStatus;
      fsServiceContractRow.StatusEffectiveUntilDate = effectiveDate;
    }
  }

  public void ExpireContract()
  {
    this.ApplyOrScheduleStatusChange((PXGraph) this, ((PXSelectBase) this.ServiceContractRecords).Cache, ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current, ((PXGraph) this).Accessinfo.BusinessDate, ((PXGraph) this).Accessinfo.BusinessDate, "E");
    this.ForceUpdateCacheAndSave(((PXSelectBase) this.ServiceContractRecords).Cache, (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current);
  }

  /// <summary>
  /// Return true if the Service Contract [fsServiceContractRow] can be deleted based on its status
  /// </summary>
  public static bool CanDeleteServiceContract(FSServiceContract fsServiceContractRow)
  {
    return fsServiceContractRow != null && !(fsServiceContractRow.Status != "D");
  }

  public virtual string GetActionFromServiceContractStatus(FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow == null)
      return (string) null;
    switch (fsServiceContractRow.Status)
    {
      case "A":
        return "A";
      case "S":
        return "S";
      case "E":
        return "E";
      case "X":
        return "X";
      default:
        return (string) null;
    }
  }

  public virtual string GetActionFromSchedule(FSSchedule fsScheduleRow)
  {
    if (fsScheduleRow == null)
      return (string) null;
    bool? active = fsScheduleRow.Active;
    if (active.GetValueOrDefault())
      return "A";
    active = fsScheduleRow.Active;
    bool flag = false;
    return active.GetValueOrDefault() == flag & active.HasValue ? "I" : (string) null;
  }

  public virtual void ApplyContractPeriodStatusChange(FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow.BillingType == "STDB")
    {
      FSContractPeriod fsContractPeriod = PXResultset<FSContractPeriod>.op_Implicit(PXSelectBase<FSContractPeriod, PXSelect<FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>>, OrderBy<Desc<FSContractPeriod.createdDateTime>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) fsServiceContractRow.ServiceContractID
      }));
      if (fsContractPeriod != null)
        fsContractPeriod.Status = "I";
    }
    this.ForceUpdateCacheAndSave(((PXSelectBase) this.ServiceContractRecords).Cache, (object) fsServiceContractRow);
  }

  public virtual void UpdateSchedulesByActivateContract()
  {
    foreach (PXResult<ActiveSchedule> pxResult in ((PXSelectBase<ActiveSchedule>) this.ActiveScheduleRecords).Select(Array.Empty<object>()))
    {
      ActiveSchedule fsScheduleRow = PXResult<ActiveSchedule>.op_Implicit(pxResult);
      ActiveSchedule activeSchedule1 = fsScheduleRow;
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = nullable1;
      activeSchedule1.EndDate = nullable2;
      ActiveSchedule activeSchedule2 = fsScheduleRow;
      nullable1 = fsScheduleRow.EffectiveRecurrenceStartDate;
      DateTime? nullable3 = nullable1 ?? fsScheduleRow.StartDate;
      activeSchedule2.StartDate = nullable3;
      ActiveSchedule activeSchedule3 = fsScheduleRow;
      nullable1 = fsScheduleRow.NextExecution;
      DateTime? nullable4 = nullable1 ?? SharedFunctions.GetNextExecution(((PXSelectBase) this.ActiveScheduleRecords).Cache, (FSSchedule) fsScheduleRow);
      activeSchedule3.NextExecutionDate = nullable4;
      fsScheduleRow.EnableExpirationDate = new bool?(false);
      ((PXSelectBase) this.ActiveScheduleRecords).Cache.Update((object) fsScheduleRow);
    }
  }

  public virtual void UpdateSchedulesByCancelContract(DateTime? cancelDate)
  {
    foreach (PXResult<ActiveSchedule> pxResult in ((PXSelectBase<ActiveSchedule>) this.ActiveScheduleRecords).Select(Array.Empty<object>()))
    {
      ActiveSchedule activeSchedule = PXResult<ActiveSchedule>.op_Implicit(pxResult);
      activeSchedule.EndDate = cancelDate;
      activeSchedule.EnableExpirationDate = new bool?(true);
      DateTime? nextExecutionDate = activeSchedule.NextExecutionDate;
      DateTime? nullable = cancelDate;
      if ((nextExecutionDate.HasValue & nullable.HasValue ? (nextExecutionDate.GetValueOrDefault() >= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        activeSchedule.NextExecutionDate = new DateTime?();
      ((PXSelectBase) this.ActiveScheduleRecords).Cache.Update((object) activeSchedule);
    }
  }

  public virtual void UpdateSchedulesBySuspendContract(DateTime? suspendDate)
  {
    foreach (PXResult<ActiveSchedule> pxResult in ((PXSelectBase<ActiveSchedule>) this.ActiveScheduleRecords).Select(Array.Empty<object>()))
    {
      ActiveSchedule activeSchedule = PXResult<ActiveSchedule>.op_Implicit(pxResult);
      activeSchedule.EndDate = suspendDate;
      activeSchedule.EnableExpirationDate = new bool?(true);
      DateTime? nextExecutionDate = activeSchedule.NextExecutionDate;
      DateTime? nullable = suspendDate;
      if ((nextExecutionDate.HasValue & nullable.HasValue ? (nextExecutionDate.GetValueOrDefault() >= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        activeSchedule.NextExecutionDate = new DateTime?();
      ((PXSelectBase) this.ActiveScheduleRecords).Cache.Update((object) activeSchedule);
    }
  }

  public virtual void SetEffectiveUntilDate(PXCache cache, FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow.ExpirationType == "U" && string.IsNullOrEmpty(fsServiceContractRow.UpcomingStatus))
      fsServiceContractRow.StatusEffectiveUntilDate = new DateTime?();
    else
      cache.RaiseFieldUpdated<FSServiceContract.endDate>((object) fsServiceContractRow, (object) null);
  }

  public virtual bool CheckDatesApplyOrScheduleStatusChange(
    PXCache cache,
    FSServiceContract fsServiceContractRow,
    DateTime? businessDate,
    DateTime? effectiveDate)
  {
    if (effectiveDate.HasValue)
    {
      if (!(effectiveDate.Value.Date < businessDate.Value.Date))
      {
        if (fsServiceContractRow.ExpirationType == "E")
        {
          DateTime date = effectiveDate.Value.Date;
          DateTime? endDate = fsServiceContractRow.EndDate;
          if ((endDate.HasValue ? (date >= endDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            goto label_5;
        }
        else
          goto label_5;
      }
      return false;
    }
label_5:
    return true;
  }

  public virtual void ActivateCurrentPeriod()
  {
    ((PXSelectBase) this.ContractPeriodFilter).Cache.SetValueExt<FSContractPeriodFilter.actions>((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, (object) "MBP");
    ((PXSelectBase) this.ContractPeriodFilter).Cache.RaiseRowSelected((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current);
    ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Select(Array.Empty<object>()));
    if (((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current == null || !(((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.Status != "N") || ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current == null || !(((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current.Status == "A"))
      return;
    FSContractPeriod current1 = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current;
    ((PXSelectBase) this.ContractPeriodRecords).Cache.SetValueExt<FSContractPeriod.status>((object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current, (object) "A");
    ((PXSelectBase) this.ContractPeriodRecords).Cache.SetStatus((object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current, (PXEntryStatus) 1);
    FSServiceContract current2 = ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current;
    if (current2.BillingType == "STDB")
    {
      FSServiceContract fsServiceContract1 = current2;
      DateTime? nullable1 = current2.NextBillingInvoiceDate;
      DateTime? nullable2 = nullable1 ?? current2.LastBillingInvoiceDate;
      fsServiceContract1.LastBillingInvoiceDate = nullable2;
      FSServiceContract fsServiceContract2 = current2;
      nullable1 = current1.EndPeriodDate;
      DateTime? nullable3;
      if (!nullable1.HasValue)
      {
        nullable1 = new DateTime?();
        nullable3 = nullable1;
      }
      else
        nullable3 = current1.EndPeriodDate;
      fsServiceContract2.NextBillingInvoiceDate = nullable3;
    }
    else if (current2.IsFixedRateContract.GetValueOrDefault())
      current2.NextBillingInvoiceDate = current1.StartPeriodDate;
    ((PXSelectBase) this.ServiceContractSelected).Cache.SetStatus((object) current2, (PXEntryStatus) 1);
    this.GenerateNewContractPeriod(current1, GraphHelper.RowCast<FSContractPeriodDet>((IEnumerable) ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Select(Array.Empty<object>())).ToList<FSContractPeriodDet>());
    ((PXAction) this.Save).Press();
    ((PXSelectBase) this.ContractPeriodFilter).Cache.SetDefaultExt<FSContractPeriodFilter.actions>((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current);
    this.UnholdAPPSORelatedToContractPeriod(((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current, current1);
  }

  public virtual void GenerateNewContractPeriod(
    FSContractPeriod fsCurrentContractPeriodRow,
    List<FSContractPeriodDet> fsContractPeriodDetRows)
  {
    if (fsCurrentContractPeriodRow == null)
      return;
    FSContractPeriod fsContractPeriod1 = new FSContractPeriod()
    {
      StartPeriodDate = new DateTime?(fsCurrentContractPeriodRow.EndPeriodDate.Value.AddDays(1.0))
    };
    fsContractPeriod1.EndPeriodDate = this.GetContractPeriodEndDate(((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current, new DateTime?(fsContractPeriod1.StartPeriodDate.Value));
    if (!fsContractPeriod1.EndPeriodDate.HasValue)
      return;
    DateTime? startPeriodDate = fsContractPeriod1.StartPeriodDate;
    DateTime? endPeriodDate = fsContractPeriod1.EndPeriodDate;
    if ((startPeriodDate.HasValue & endPeriodDate.HasValue ? (startPeriodDate.GetValueOrDefault() < endPeriodDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    FSContractPeriod fsContractPeriod2 = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Insert(fsContractPeriod1);
    if (fsContractPeriodDetRows == null)
      return;
    foreach (FSContractPeriodDet contractPeriodDetRow in fsContractPeriodDetRows)
    {
      FSContractPeriodDet copy = PXCache<FSContractPeriodDet>.CreateCopy(((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Insert(new FSContractPeriodDet()
      {
        ServiceContractID = contractPeriodDetRow.ServiceContractID,
        InventoryID = contractPeriodDetRow.InventoryID,
        LineType = contractPeriodDetRow.LineType
      }));
      copy.BillingRule = contractPeriodDetRow.BillingRule;
      copy.SMEquipmentID = contractPeriodDetRow.SMEquipmentID;
      if (contractPeriodDetRow.BillingRule == "TIME")
        copy.Time = contractPeriodDetRow.Time;
      else
        copy.Qty = contractPeriodDetRow.Qty;
      copy.RecurringUnitPrice = contractPeriodDetRow.RecurringUnitPrice;
      copy.OverageItemPrice = contractPeriodDetRow.OverageItemPrice;
      copy.Rollover = contractPeriodDetRow.Rollover;
      copy.ProjectID = contractPeriodDetRow.ProjectID;
      copy.ProjectTaskID = contractPeriodDetRow.ProjectTaskID;
      copy.CostCodeID = contractPeriodDetRow.CostCodeID;
      copy.DeferredCode = contractPeriodDetRow.DeferredCode;
      ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Update(copy);
    }
  }

  public virtual void DeleteScheduledAppSO(
    FSServiceContract fsServiceContractRow,
    DateTime? cancelationDate)
  {
    if (fsServiceContractRow == null || !cancelationDate.HasValue)
      return;
    ServiceOrderEntry instance1 = PXGraph.CreateInstance<ServiceOrderEntry>();
    AppointmentEntry instance2 = PXGraph.CreateInstance<AppointmentEntry>();
    if (fsServiceContractRow.ScheduleGenType == "SO")
    {
      foreach (PXResult<FSServiceOrder> pxResult in PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.serviceContractID, Equal<Required<FSServiceOrder.serviceContractID>>, And<FSServiceOrder.orderDate, Greater<Required<FSServiceOrder.orderDate>>, And<FSServiceOrder.openDoc, Equal<True>, And<FSServiceOrder.allowInvoice, Equal<False>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) fsServiceContractRow.ServiceContractID,
        (object) cancelationDate
      }))
      {
        FSServiceOrder fsServiceOrder = PXResult<FSServiceOrder>.op_Implicit(pxResult);
        ((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Search<FSServiceOrder.sOID>((object) fsServiceOrder.SOID, new object[1]
        {
          (object) fsServiceOrder.SrvOrdType
        }));
        ((PXAction) instance1.Delete).Press();
      }
    }
    else
    {
      foreach (PXResult<FSAppointment> pxResult in PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.serviceContractID, Equal<Required<FSAppointment.serviceContractID>>, And<FSAppointment.executionDate, Greater<Required<FSAppointment.executionDate>>, And<FSAppointment.notStarted, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) fsServiceContractRow.ServiceContractID,
        (object) cancelationDate
      }))
      {
        FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
        ((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsAppointment.AppointmentID, new object[1]
        {
          (object) fsAppointment.SrvOrdType
        }));
        ((PXAction) instance2.Delete).Press();
      }
    }
    foreach (PXResult<FSServiceOrder> pxResult in PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.orderDate, Greater<Required<FSServiceOrder.orderDate>>, And<FSServiceOrder.closed, Equal<False>, And<FSServiceOrder.allowInvoice, Equal<False>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsServiceContractRow.ServiceContractID,
      (object) cancelationDate
    }))
    {
      FSServiceOrder fsServiceOrder1 = PXResult<FSServiceOrder>.op_Implicit(pxResult);
      PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords1 = instance1.ServiceOrderRecords;
      PXSelectJoin<FSServiceOrder, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<FSServiceOrder.srvOrdType, Equal<Optional<FSServiceOrder.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> serviceOrderRecords2 = instance1.ServiceOrderRecords;
      // ISSUE: variable of a boxed type
      __Boxed<int?> soid = (ValueType) fsServiceOrder1.SOID;
      object[] objArray = new object[1]
      {
        (object) fsServiceOrder1.SrvOrdType
      };
      FSServiceOrder fsServiceOrder2;
      FSServiceOrder fsServiceOrder3 = fsServiceOrder2 = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderRecords2).Search<FSServiceOrder.sOID>((object) soid, objArray));
      ((PXSelectBase<FSServiceOrder>) serviceOrderRecords1).Current = fsServiceOrder2;
      FSServiceOrder fsServiceOrderRow = fsServiceOrder3;
      if (instance1.GetBillingMode(fsServiceOrderRow) == "SO")
      {
        ((PXSelectBase) instance1.ServiceOrderRecords).Cache.SetValueExt<FSServiceOrder.billServiceContractID>((object) fsServiceOrderRow, (object) null);
        ((PXAction) instance1.Save).Press();
      }
    }
    foreach (PXResult<FSAppointment> pxResult in PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.executionDate, Greater<Required<FSAppointment.executionDate>>, And<FSAppointment.closed, Equal<False>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsServiceContractRow.ServiceContractID,
      (object) cancelationDate
    }))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      ((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsAppointment.AppointmentID, new object[1]
      {
        (object) fsAppointment.SrvOrdType
      }));
      if (instance2.GetBillingMode(((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRelated).Current) == "AP")
      {
        ((PXSelectBase) instance2.AppointmentRecords).Cache.SetValueExt<FSAppointment.billServiceContractID>((object) ((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Current, (object) null);
        ((PXAction) instance2.Save).Press();
      }
    }
  }

  public virtual void SetBillingPeriod(FSServiceContract fsServiceContractRow)
  {
    if (this.IsRenewContract)
      return;
    if (((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current != null)
    {
      ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.StartPeriodDate = fsServiceContractRow.StartDate;
      ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.EndPeriodDate = this.GetContractPeriodEndDate(fsServiceContractRow, fsServiceContractRow.StartDate);
      if (((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.EndPeriodDate.HasValue)
      {
        if (((PXSelectBase) this.ContractPeriodRecords).Cache.GetStatus((object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current) != null)
          return;
        ((PXSelectBase) this.ContractPeriodRecords).Cache.SetStatus((object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current, (PXEntryStatus) 1);
      }
      else
      {
        if (!(((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.Status != "A"))
          return;
        ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Delete(((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current);
      }
    }
    else
    {
      if (!(fsServiceContractRow.BillingType == "STDB"))
        return;
      FSContractPeriod fsContractPeriod = new FSContractPeriod();
      fsContractPeriod.StartPeriodDate = fsServiceContractRow.StartDate ?? ((PXGraph) this).Accessinfo.BusinessDate;
      DateTime? contractPeriodEndDate = this.GetContractPeriodEndDate(fsServiceContractRow, new DateTime?(fsContractPeriod.StartPeriodDate.Value));
      if (contractPeriodEndDate.HasValue)
      {
        fsContractPeriod.EndPeriodDate = new DateTime?(contractPeriodEndDate.Value);
        ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Insert(fsContractPeriod);
      }
      ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.actions>(((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, (object) "MBP");
    }
  }

  public virtual bool EnableDisableActivatePeriodButton(
    FSServiceContract fsServiceContractRow,
    FSContractPeriod fsContractPeriodRow)
  {
    return fsServiceContractRow != null && fsServiceContractRow.Status == "A" && fsContractPeriodRow != null && fsContractPeriodRow.Status == "I" && this.AllowActivatePeriod(fsContractPeriodRow);
  }

  public virtual bool AllowActivatePeriod(FSContractPeriod fsContractPeriodRow)
  {
    return ((IQueryable<PXResult<FSContractPeriod>>) PXSelectBase<FSContractPeriod, PXSelect<FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current.ServiceContractID
    })).Count<PXResult<FSContractPeriod>>() == 0;
  }

  public virtual void MarkBillingPeriodAsInvoiced(
    FSSetup fsSetupRow,
    FSContractPostDoc fsContractPostDocRow)
  {
    ((PXSelectBase) this.ContractPeriodFilter).Cache.SetValueExt<FSContractPeriodFilter.actions>((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, (object) "SBP");
    ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current.ContractPeriodID = fsContractPostDocRow.ContractPeriodID;
    ((PXSelectBase) this.ContractPeriodFilter).Cache.RaiseRowSelected((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current);
    bool allowUpdate = ((PXSelectBase) this.ServiceContractRecords).Cache.AllowUpdate;
    if (((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current == null || ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current == null)
      return;
    ((PXSelectBase) this.ServiceContractRecords).Cache.AllowUpdate = true;
    ((PXSelectBase) this.ContractPeriodRecords).Cache.SetValueExt<FSContractPeriod.invoiced>((object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current, (object) true);
    ((PXSelectBase) this.ContractPeriodRecords).Cache.SetValueExt<FSContractPeriod.contractPostDocID>((object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current, (object) fsContractPostDocRow.ContractPostDocID);
    string status = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.Status;
    ((PXSelectBase) this.ContractPeriodRecords).Cache.SetValueExt<FSContractPeriod.status>((object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current, (object) "N");
    this.ForceUpdateCacheAndSave(((PXSelectBase) this.ContractPeriodRecords).Cache, (object) ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current);
    if (status != "P")
    {
      FSServiceContract current = ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current;
      DateTime? nullable1;
      bool? nullable2;
      if (current.BillingType == "STDB")
      {
        FSServiceContract fsServiceContract = current;
        nullable1 = current.NextBillingInvoiceDate;
        DateTime? nullable3 = nullable1 ?? current.LastBillingInvoiceDate;
        fsServiceContract.LastBillingInvoiceDate = nullable3;
      }
      else
      {
        nullable2 = current.IsFixedRateContract;
        if (nullable2.GetValueOrDefault())
          current.LastBillingInvoiceDate = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.StartPeriodDate;
      }
      FSServiceContract fsServiceContract1 = current;
      nullable1 = new DateTime?();
      DateTime? nullable4 = nullable1;
      fsServiceContract1.NextBillingInvoiceDate = nullable4;
      this.ForceUpdateCacheAndSave(((PXSelectBase) this.ServiceContractSelected).Cache, (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current);
      nullable2 = fsSetupRow.EnableContractPeriodWhenInvoice;
      if (nullable2.GetValueOrDefault())
        this.ActivateCurrentPeriod();
    }
    ((PXSelectBase) this.ServiceContractRecords).Cache.AllowUpdate = allowUpdate;
  }

  public virtual void UnholdAPPSORelatedToContractPeriod(
    FSServiceContract fsServiceContractRow,
    FSContractPeriod fsContractPeriodRow)
  {
    if (fsServiceContractRow == null && fsContractPeriodRow == null)
      return;
    ServiceOrderEntry instance1 = PXGraph.CreateInstance<ServiceOrderEntry>();
    AppointmentEntry instance2 = PXGraph.CreateInstance<AppointmentEntry>();
    foreach (PXResult<FSServiceOrder> pxResult in PXSelectBase<FSServiceOrder, PXSelect<FSServiceOrder, Where<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.orderDate, GreaterEqual<Required<FSServiceOrder.orderDate>>, And<FSServiceOrder.orderDate, LessEqual<Required<FSServiceOrder.orderDate>>, And<FSServiceOrder.closed, Equal<False>, And<FSServiceOrder.canceled, Equal<False>, And<FSServiceOrder.allowInvoice, Equal<False>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) fsServiceContractRow.ServiceContractID,
      (object) fsContractPeriodRow.StartPeriodDate,
      (object) fsContractPeriodRow.EndPeriodDate
    }))
    {
      FSServiceOrder fsServiceOrder = PXResult<FSServiceOrder>.op_Implicit(pxResult);
      ((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Search<FSServiceOrder.sOID>((object) fsServiceOrder.SOID, new object[1]
      {
        (object) fsServiceOrder.SrvOrdType
      }));
      ((PXSelectBase) instance1.ServiceOrderRecords).Cache.SetDefaultExt<FSServiceOrder.billContractPeriodID>((object) ((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Current);
      ((PXSelectBase) instance1.ServiceOrderRecords).Cache.Update((object) ((PXSelectBase<FSServiceOrder>) instance1.ServiceOrderRecords).Current);
      ((PXAction) instance1.Save).Press();
    }
    foreach (PXResult<FSAppointment> pxResult in PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.executionDate, GreaterEqual<Required<FSAppointment.executionDate>>, And<FSAppointment.executionDate, LessEqual<Required<FSAppointment.executionDate>>, And<FSAppointment.closed, Equal<False>, And<FSAppointment.canceled, Equal<False>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) fsServiceContractRow.ServiceContractID,
      (object) fsContractPeriodRow.StartPeriodDate,
      (object) fsContractPeriodRow.EndPeriodDate
    }))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(pxResult);
      ((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Search<FSAppointment.appointmentID>((object) fsAppointment.AppointmentID, new object[1]
      {
        (object) fsAppointment.SrvOrdType
      }));
      ((PXSelectBase) instance2.AppointmentRecords).Cache.SetDefaultExt<FSAppointment.billContractPeriodID>((object) ((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Current);
      ((PXSelectBase) instance2.AppointmentRecords).Cache.Update((object) ((PXSelectBase<FSAppointment>) instance2.AppointmentRecords).Current);
      ((PXAction) instance2.Save).Press();
    }
  }

  public virtual void SetBillCustomerAndLocationID(
    PXCache cache,
    FSServiceContract fsServiceContractRow)
  {
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<FSServiceOrder.customerID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) fsServiceContractRow.CustomerID
    }));
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    string str = "C";
    if (baccount == null || baccount.Type != "PR")
    {
      FSxCustomer extension = PXCache<PX.Objects.AR.Customer>.GetExtension<FSxCustomer>(SharedFunctions.GetCustomerRow(cache.Graph, fsServiceContractRow.CustomerID));
      switch (extension.DefaultBillingCustomerSource)
      {
        case "SO":
          nullable1 = fsServiceContractRow.CustomerID;
          nullable2 = fsServiceContractRow.CustomerLocationID;
          break;
        case "DC":
          str = "S";
          nullable1 = fsServiceContractRow.CustomerID;
          nullable2 = this.GetDefaultLocationID(cache.Graph, fsServiceContractRow.CustomerID);
          break;
        case "LC":
          str = "S";
          nullable1 = extension.BillCustomerID;
          nullable2 = extension.BillLocationID;
          break;
      }
    }
    cache.SetValueExt<FSServiceContract.billTo>((object) fsServiceContractRow, (object) str);
    cache.SetValueExt<FSServiceContract.billCustomerID>((object) fsServiceContractRow, (object) nullable1);
    cache.SetValueExt<FSServiceContract.billLocationID>((object) fsServiceContractRow, (object) nullable2);
  }

  public virtual void ForceUpdateCacheAndSave(PXCache cache, object row)
  {
    cache.AllowUpdate = true;
    cache.SetStatus(row, (PXEntryStatus) 1);
    ((PXGraph) this).GetSaveAction().Press();
  }

  public virtual int? GetDefaultLocationID(PXGraph graph, int? bAccountID)
  {
    return ServiceOrderEntry.GetDefaultLocationIDInt(graph, bAccountID);
  }

  public virtual DateTime? GetContractPeriodEndDate(
    FSServiceContract fsServiceContractRow,
    DateTime? lastGeneratedElementDate)
  {
    bool flag = false;
    TimeSlotGenerator timeSlotGenerator = new TimeSlotGenerator();
    List<PX.Objects.FS.Scheduler.Schedule> scheduleList = new List<PX.Objects.FS.Scheduler.Schedule>();
    List<PX.Objects.FS.Scheduler.Schedule> schedule = MapFSServiceContractToSchedule.convertFSServiceContractToSchedule(fsServiceContractRow, lastGeneratedElementDate);
    DateTime fromDate = lastGeneratedElementDate ?? fsServiceContractRow.StartDate.Value;
    DateTime? endDate1 = fsServiceContractRow.EndDate;
    ref bool local = ref flag;
    DateTime? contractPeriodEndDate1 = timeSlotGenerator.GenerateNextOccurrence((IEnumerable<PX.Objects.FS.Scheduler.Schedule>) schedule, fromDate, endDate1, out local);
    if (fsServiceContractRow.BillingPeriod != "W")
      contractPeriodEndDate1 = contractPeriodEndDate1?.AddDays(-1.0);
    if (flag)
      contractPeriodEndDate1 = fsServiceContractRow.EndDate;
    if (fsServiceContractRow.ExpirationType == "E" && fsServiceContractRow.EndDate.HasValue)
    {
      DateTime? endDate2 = fsServiceContractRow.EndDate;
      DateTime? contractPeriodEndDate2 = contractPeriodEndDate1;
      if ((endDate2.HasValue & contractPeriodEndDate2.HasValue ? (endDate2.GetValueOrDefault() < contractPeriodEndDate2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        contractPeriodEndDate2 = new DateTime?();
        return contractPeriodEndDate2;
      }
    }
    return contractPeriodEndDate1;
  }

  public virtual void CalculateBillHistoryUnboundFields(
    PXCache cache,
    FSBillHistory fsBillHistoryRow)
  {
    using (new PXConnectionScope())
    {
      ServiceOrderBase<ServiceOrderEntry, FSServiceOrder>.CalculateBillHistoryUnboundFieldsInt(cache, fsBillHistoryRow);
      int num = fsBillHistoryRow.ParentEntityType != null ? 1 : 0;
      string str1 = num != 0 ? fsBillHistoryRow.ParentDocType : fsBillHistoryRow.ChildDocType;
      string str2 = num != 0 ? fsBillHistoryRow.ParentRefNbr : fsBillHistoryRow.ChildRefNbr;
      FSContractPeriod fsContractPeriod = PXResultset<FSContractPeriod>.op_Implicit(PXSelectBase<FSContractPeriod, PXSelectJoin<FSContractPeriod, InnerJoin<FSContractPostDoc, On<FSContractPostDoc.contractPeriodID, Equal<FSContractPeriod.contractPeriodID>>>, Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>, And<FSContractPostDoc.postDocType, Equal<Required<FSContractPostDoc.postDocType>>, And<FSContractPostDoc.postRefNbr, Equal<Required<FSContractPostDoc.postRefNbr>>>>>>.Config>.Select(cache.Graph, new object[3]
      {
        (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current.ServiceContractID,
        (object) str1,
        (object) str2
      }));
      if (fsContractPeriod == null)
        return;
      fsBillHistoryRow.ServiceContractPeriodID = fsContractPeriod.ContractPeriodID;
      fsBillHistoryRow.ContractPeriodStatus = fsContractPeriod.Status;
    }
  }

  public virtual void ContractForecastProc(
    FSServiceContract contract,
    DateTime? startDate,
    DateTime? endDate)
  {
    if (!startDate.HasValue || !endDate.HasValue || DateTime.Compare(startDate.Value, endDate.Value) > 0)
      throw new ArgumentException();
    Period period1 = new Period(startDate.Value, endDate);
    this.DeactivatePreviousForecast(contract.ServiceContractID);
    FSContractForecast instance = (FSContractForecast) ((PXSelectBase) this.forecastRecords).Cache.CreateInstance();
    instance.ServiceContractID = contract.ServiceContractID;
    instance.DateTimeBegin = startDate;
    instance.DateTimeEnd = endDate;
    instance.Active = new bool?(true);
    ((PXSelectBase<FSContractForecast>) this.forecastRecords).Insert(instance);
    foreach (PXResult<FSSchedule> pxResult in PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.active, Equal<True>, And<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contract.ServiceContractID
    }))
    {
      FSSchedule fsScheduleRow = PXResult<FSSchedule>.op_Implicit(pxResult);
      DateTime? endDate1 = contract.EndDate;
      DateTime? endDate2 = fsScheduleRow.EndDate;
      if ((endDate1.HasValue == endDate2.HasValue ? (endDate1.HasValue ? (endDate1.GetValueOrDefault() == endDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        fsScheduleRow.EndDate = new DateTime?();
      List<PX.Objects.FS.Scheduler.Schedule> scheduleList1 = new List<PX.Objects.FS.Scheduler.Schedule>();
      TimeSlotGenerator timeSlotGenerator = new TimeSlotGenerator();
      List<PX.Objects.FS.Scheduler.Schedule> schedule = MapFSScheduleToSchedule.convertFSScheduleToSchedule(((PXSelectBase) this.forecastRecords).Cache, fsScheduleRow, endDate, "NRSC");
      Period period2 = period1;
      List<PX.Objects.FS.Scheduler.Schedule> scheduleList2 = schedule;
      int? generationID = new int?();
      int num = timeSlotGenerator.GenerateCalendar(period2, (IEnumerable<PX.Objects.FS.Scheduler.Schedule>) scheduleList2, generationID).Count<TimeSlot>();
      foreach (FSScheduleDet fsScheduleDet in GraphHelper.RowCast<FSScheduleDet>((IEnumerable) PXSelectBase<FSScheduleDet, PXSelect<FSScheduleDet, Where<FSScheduleDet.scheduleID, Equal<Required<FSScheduleDet.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) fsScheduleRow.ScheduleID
      })).Where<FSScheduleDet>((Func<FSScheduleDet, bool>) (x => x.LineType != "IT_LN" && x.LineType != "CM_LN")))
      {
        if (fsScheduleDet.LineType == "TEMPL")
        {
          foreach (FSServiceTemplateDet serviceTemplateDet in GraphHelper.RowCast<FSServiceTemplateDet>((IEnumerable) PXSelectBase<FSServiceTemplateDet, PXSelect<FSServiceTemplateDet, Where<FSServiceTemplateDet.serviceTemplateID, Equal<Required<FSServiceTemplateDet.serviceTemplateID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) fsScheduleDet.ServiceTemplateID
          })).Where<FSServiceTemplateDet>((Func<FSServiceTemplateDet, bool>) (x => x.LineType != "IT_LN" && x.LineType != "CM_LN")))
          {
            FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, serviceTemplateDet.InventoryID));
            FSServiceContract serviceContract = contract;
            string lineType = serviceTemplateDet.LineType;
            string billingRule = extension.BillingRule ?? fsScheduleDet.BillingRule;
            int? inventoryId = serviceTemplateDet.InventoryID;
            Decimal? qty1 = fsScheduleDet.Qty;
            Decimal? nullable1 = serviceTemplateDet.Qty;
            Decimal? qty2 = qty1.HasValue & nullable1.HasValue ? new Decimal?(qty1.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
            int? occurrences = new int?(num);
            int? scheduleId = fsScheduleDet.ScheduleID;
            int? scheduleDetId = fsScheduleDet.ScheduleDetID;
            string tranDesc1 = fsScheduleDet.TranDesc;
            string recurrenceDescription = fsScheduleRow.RecurrenceDescription;
            int? smEquipmentId = fsScheduleDet.SMEquipmentID;
            int? componentId = fsScheduleDet.ComponentID;
            string equipmentAction = fsScheduleDet.EquipmentAction;
            int? equipmentLineRef = fsScheduleDet.EquipmentLineRef;
            int? sMEquipmentID = smEquipmentId;
            string tranDesc2 = tranDesc1;
            string recurrenceDesc = recurrenceDescription;
            int? scheduleID = scheduleId;
            int? scheduleDetID = scheduleDetId;
            DateTime? nullable2 = startDate;
            string uom = fsScheduleDet.UOM;
            int? contractPeriodID = new int?();
            int? contractPeriodDetID = new int?();
            nullable1 = new Decimal?();
            Decimal? unitPrice = nullable1;
            nullable1 = new Decimal?();
            Decimal? overagePrice = nullable1;
            string uOM = uom;
            DateTime? priceStartDate = nullable2;
            ((PXSelectBase<FSContractForecastDet>) this.forecastDetRecords).Insert(this.CreateContractForecastDet(serviceContract, lineType, billingRule, "S", inventoryId, qty2, occurrences, componentId, equipmentAction, equipmentLineRef, sMEquipmentID, tranDesc2, recurrenceDesc, scheduleID, scheduleDetID, contractPeriodID, contractPeriodDetID, unitPrice, overagePrice, uOM, priceStartDate));
          }
        }
        else
        {
          FSServiceContract serviceContract = contract;
          string lineType = fsScheduleDet.LineType;
          string billingRule = fsScheduleDet.BillingRule;
          int? inventoryId = fsScheduleDet.InventoryID;
          Decimal? qty = fsScheduleDet.Qty;
          int? occurrences = new int?(num);
          int? scheduleId = fsScheduleDet.ScheduleID;
          int? scheduleDetId = fsScheduleDet.ScheduleDetID;
          string tranDesc3 = fsScheduleDet.TranDesc;
          string recurrenceDescription = fsScheduleRow.RecurrenceDescription;
          int? smEquipmentId = fsScheduleDet.SMEquipmentID;
          int? componentId = fsScheduleDet.ComponentID;
          string equipmentAction = fsScheduleDet.EquipmentAction;
          int? equipmentLineRef = fsScheduleDet.EquipmentLineRef;
          int? sMEquipmentID = smEquipmentId;
          string tranDesc4 = tranDesc3;
          string recurrenceDesc = recurrenceDescription;
          int? scheduleID = scheduleId;
          int? scheduleDetID = scheduleDetId;
          DateTime? nullable = startDate;
          int? contractPeriodID = new int?();
          int? contractPeriodDetID = new int?();
          Decimal? unitPrice = new Decimal?();
          Decimal? overagePrice = new Decimal?();
          DateTime? priceStartDate = nullable;
          ((PXSelectBase<FSContractForecastDet>) this.forecastDetRecords).Insert(this.CreateContractForecastDet(serviceContract, lineType, billingRule, "S", inventoryId, qty, occurrences, componentId, equipmentAction, equipmentLineRef, sMEquipmentID, tranDesc4, recurrenceDesc, scheduleID, scheduleDetID, contractPeriodID, contractPeriodDetID, unitPrice, overagePrice, priceStartDate: priceStartDate));
        }
      }
    }
    if (contract.BillingType != "APFB")
    {
      FSServiceContract copy = (FSServiceContract) ((PXSelectBase) this.ServiceContractRecords).Cache.CreateCopy((object) contract);
      copy.ExpirationType = "E";
      copy.StartDate = startDate;
      copy.EndDate = endDate;
      FSContractPeriod fsContractPeriod1 = PXResultset<FSContractPeriod>.op_Implicit(PXSelectBase<FSContractPeriod, PXSelect<FSContractPeriod, Where<FSContractPeriod.serviceContractID, Equal<Required<FSContractPeriod.serviceContractID>>>, OrderBy<Desc<FSContractPeriod.endPeriodDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) copy.ServiceContractID
      }));
      if (fsContractPeriod1 != null)
      {
        int num = 0;
        PXResultset<FSContractPeriodDet> source = PXSelectBase<FSContractPeriodDet, PXSelect<FSContractPeriodDet, Where<FSContractPeriodDet.contractPeriodID, Equal<Required<FSContractPeriod.contractPeriodID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) fsContractPeriod1.ContractPeriodID
        });
        if (source != null && ((IQueryable<PXResult<FSContractPeriodDet>>) source).Count<PXResult<FSContractPeriodDet>>() > 0)
        {
          FSContractPeriod fsContractPeriod2 = new FSContractPeriod();
          fsContractPeriod2.StartPeriodDate = startDate;
          FSContractPeriod fsContractPeriod3 = fsContractPeriod2;
          FSServiceContract fsServiceContractRow1 = copy;
          DateTime? nullable3 = fsContractPeriod2.StartPeriodDate;
          DateTime? lastGeneratedElementDate1 = new DateTime?(nullable3.Value);
          DateTime? contractPeriodEndDate1 = this.GetContractPeriodEndDate(fsServiceContractRow1, lastGeneratedElementDate1);
          fsContractPeriod3.EndPeriodDate = contractPeriodEndDate1;
          DateTime? nullable4;
          while (true)
          {
            nullable3 = fsContractPeriod2.EndPeriodDate;
            if (nullable3.HasValue)
            {
              nullable3 = fsContractPeriod2.StartPeriodDate;
              nullable4 = endDate;
              if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                ++num;
                FSContractPeriod fsContractPeriod4 = fsContractPeriod2;
                nullable3 = fsContractPeriod2.EndPeriodDate;
                DateTime? nullable5 = new DateTime?(nullable3.Value.AddDays(1.0));
                fsContractPeriod4.StartPeriodDate = nullable5;
                FSContractPeriod fsContractPeriod5 = fsContractPeriod2;
                FSServiceContract fsServiceContractRow2 = copy;
                nullable3 = fsContractPeriod2.StartPeriodDate;
                DateTime? lastGeneratedElementDate2 = new DateTime?(nullable3.Value);
                DateTime? contractPeriodEndDate2 = this.GetContractPeriodEndDate(fsServiceContractRow2, lastGeneratedElementDate2);
                fsContractPeriod5.EndPeriodDate = contractPeriodEndDate2;
              }
              else
                break;
            }
            else
              break;
          }
          foreach (PXResult<FSContractPeriodDet> pxResult in source)
          {
            FSContractPeriodDet detRows = PXResult<FSContractPeriodDet>.op_Implicit(pxResult);
            FSServiceContract serviceContract = contract;
            string lineType = detRows.LineType;
            string billingRule = detRows.BillingRule;
            int? inventoryId1 = detRows.InventoryID;
            Decimal? qty = detRows.Qty;
            int? occurrences = new int?(num);
            int? smEquipmentId = detRows.SMEquipmentID;
            int? contractPeriodId = detRows.ContractPeriodID;
            int? contractPeriodDetId = detRows.ContractPeriodDetID;
            Decimal? recurringUnitPrice = detRows.RecurringUnitPrice;
            Decimal? overageItemPrice = detRows.OverageItemPrice;
            string uom = detRows.UOM;
            int? componentID = new int?();
            int? equipmentLineRef = new int?();
            int? sMEquipmentID = smEquipmentId;
            int? scheduleID = new int?();
            int? scheduleDetID = new int?();
            int? contractPeriodID = contractPeriodId;
            int? contractPeriodDetID = contractPeriodDetId;
            Decimal? unitPrice = recurringUnitPrice;
            Decimal? overagePrice = overageItemPrice;
            string uOM = uom;
            nullable4 = new DateTime?();
            DateTime? priceStartDate = nullable4;
            ((PXSelectBase<FSContractForecastDet>) this.forecastDetRecords).Insert(this.CreateContractForecastDet(serviceContract, lineType, billingRule, "P", inventoryId1, qty, occurrences, componentID, equipmentLineRef: equipmentLineRef, sMEquipmentID: sMEquipmentID, scheduleID: scheduleID, scheduleDetID: scheduleDetID, contractPeriodID: contractPeriodID, contractPeriodDetID: contractPeriodDetID, unitPrice: unitPrice, overagePrice: overagePrice, uOM: uOM, priceStartDate: priceStartDate));
            if (contract.BillingType == "STDB")
              EnumerableExtensions.ForEach<FSContractForecastDet>(GraphHelper.RowCast<FSContractForecastDet>(((PXSelectBase) this.forecastDetRecords).Cache.Inserted).Where<FSContractForecastDet>((Func<FSContractForecastDet, bool>) (x =>
              {
                int? inventoryId2 = x.InventoryID;
                int? inventoryId3 = detRows.InventoryID;
                if (inventoryId2.GetValueOrDefault() == inventoryId3.GetValueOrDefault() & inventoryId2.HasValue == inventoryId3.HasValue)
                {
                  int? serviceContractId1 = x.ServiceContractID;
                  int? serviceContractId2 = contract.ServiceContractID;
                  if (serviceContractId1.GetValueOrDefault() == serviceContractId2.GetValueOrDefault() & serviceContractId1.HasValue == serviceContractId2.HasValue && !x.ContractPeriodID.HasValue)
                    return x.ForecastDetType == "S";
                }
                return false;
              })), (Action<FSContractForecastDet>) (x => x.TotalPrice = new Decimal?(0M)));
          }
        }
      }
    }
    if (!((PXGraph) this).IsDirty)
      return;
    ((PXGraph) this).Actions.PressSave();
  }

  public virtual void DeactivatePreviousForecast(int? serviceContractID)
  {
    foreach (PXResult<FSContractForecast> pxResult in PXSelectBase<FSContractForecast, PXSelect<FSContractForecast, Where<FSContractForecast.active, Equal<True>, And<FSContractForecast.serviceContractID, Equal<Required<FSContractForecast.serviceContractID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) serviceContractID
    }))
    {
      FSContractForecast contractForecast = PXResult<FSContractForecast>.op_Implicit(pxResult);
      contractForecast.Active = new bool?(false);
      ((PXSelectBase<FSContractForecast>) this.forecastRecords).Update(contractForecast);
    }
  }

  public virtual FSContractForecastDet CreateContractForecastDet(
    FSServiceContract serviceContract,
    string lineType,
    string billingRule,
    string detType,
    int? inventoryID,
    Decimal? qty,
    int? occurrences,
    int? componentID = null,
    string equipmentAction = null,
    int? equipmentLineRef = null,
    int? sMEquipmentID = null,
    string tranDesc = null,
    string recurrenceDesc = null,
    int? scheduleID = null,
    int? scheduleDetID = null,
    int? contractPeriodID = null,
    int? contractPeriodDetID = null,
    Decimal? unitPrice = null,
    Decimal? overagePrice = null,
    string uOM = null,
    DateTime? priceStartDate = null)
  {
    if (!unitPrice.HasValue && !priceStartDate.HasValue)
      throw new ArgumentException();
    FSContractForecastDet ret = new FSContractForecastDet();
    ret.ServiceContractID = serviceContract.ServiceContractID;
    ret.LineType = lineType;
    ret.BillingRule = billingRule;
    ret.ForecastDetType = detType;
    ret.Occurrences = occurrences;
    ret.ScheduleID = scheduleID;
    ret.ScheduleDetID = scheduleDetID;
    ret.InventoryID = inventoryID;
    ret.Qty = qty;
    ret.ComponentID = componentID;
    ret.EquipmentAction = equipmentAction;
    ret.EquipmentLineRef = equipmentLineRef;
    ret.SMEquipmentID = sMEquipmentID;
    ret.TranDesc = tranDesc;
    ret.RecurrenceDesc = recurrenceDesc;
    ret.ContractPeriodID = contractPeriodID;
    ret.ContractPeriodDetID = contractPeriodDetID;
    ret.UnitPrice = unitPrice;
    ret.UOM = uOM;
    ret.OveragePrice = overagePrice;
    if (detType == "S" && serviceContract.BillingType == "FIRB")
      ret.TotalPrice = new Decimal?(0M);
    if (!unitPrice.HasValue)
    {
      if (serviceContract.BillingType == "APFB" && serviceContract.SourcePrice == "C")
      {
        FSSalesPrice fsSalesPrice = GraphHelper.RowCast<FSSalesPrice>((IEnumerable) ((PXSelectBase<FSSalesPrice>) this.SalesPriceLines).Select(Array.Empty<object>())).Where<FSSalesPrice>((Func<FSSalesPrice, bool>) (x =>
        {
          int? inventoryId1 = x.InventoryID;
          int? inventoryId2 = ret.InventoryID;
          return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
        })).FirstOrDefault<FSSalesPrice>();
        if (fsSalesPrice != null)
        {
          ret.UnitPrice = fsSalesPrice.UnitPrice;
          ret.UOM = fsSalesPrice.UOM;
        }
      }
      else if (serviceContract.BillingType != "APFB" || serviceContract.SourcePrice == "P")
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, ret.InventoryID);
        SalesPriceSet customerContract = FSPriceManagement.CalculateSalesPriceWithCustomerContract(((PXSelectBase) this.forecastDetRecords).Cache, new int?(), new int?(), new int?(), serviceContract.CustomerID, serviceContract.CustomerLocationID, new bool?(), ret.InventoryID, new int?(), ret.Qty, inventoryItem?.SalesUnit, priceStartDate.Value, new Decimal?(), true, (PX.Objects.CM.CurrencyInfo) null, true);
        if (customerContract != null)
        {
          ret.UnitPrice = customerContract.Price;
          ret.UOM = inventoryItem.SalesUnit;
        }
      }
    }
    if (ret.BillingRule == "NONE")
      ret.UnitPrice = new Decimal?(0.0M);
    return ret;
  }

  public virtual void SetForecastFilterDefaults()
  {
    ((PXSelectBase<FSContractForecastFilter>) this.ForecastFilter).Current.StartDate = this.GetDfltForecastFilterStartDate();
    ((PXSelectBase<FSContractForecastFilter>) this.ForecastFilter).Current.EndDate = this.GetDfltForecastFilterEndDate();
  }

  public virtual DateTime? GetDfltForecastFilterStartDate()
  {
    switch (((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ExpirationType)
    {
      case "U":
      case "E":
        return ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.StartDate;
      case "R":
        return !(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.Status != "A") ? ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.RenewalDate ?? ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.StartDate : ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.StartDate;
      default:
        throw new NotImplementedException();
    }
  }

  public virtual DateTime? GetDfltForecastFilterEndDate()
  {
    switch (((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ExpirationType)
    {
      case "U":
        return new DateTime?(this.GetEndDateFromDuration(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.StartDate.Value, "Y", 1));
      case "E":
        return ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.EndDate;
      case "R":
        return ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.Status == "A" ? new DateTime?(this.GetEndDateFromDuration((((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.RenewalDate ?? ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.StartDate).Value, ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.DurationType, ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.Duration ?? 1)) : ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.EndDate;
      default:
        throw new NotImplementedException();
    }
  }

  public virtual void CopyContractProc(FSServiceContract contract, DateTime? date, string refNbr)
  {
    bool flag = this.RefNbrRequired();
    string errorMessage;
    if (flag && !this.RefNbrIsValid(refNbr, out errorMessage))
      throw new PXException(errorMessage);
    FSContractPeriod current1 = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current;
    ((PXGraph) this).Clear((PXClearOption) 1);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (PXResult<FSServiceContract> pxResult1 in PXSelectBase<FSServiceContract, PXSelect<FSServiceContract, Where<FSServiceContract.refNbr, Equal<Required<FSServiceContract.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) contract.RefNbr
      }))
      {
        FSServiceContract src = PXResult<FSServiceContract>.op_Implicit(pxResult1);
        FSServiceContract fsServiceContract1 = new FSServiceContract()
        {
          CustomerID = src.CustomerID,
          CustomerLocationID = src.CustomerLocationID
        };
        if (flag)
          fsServiceContract1.RefNbr = refNbr;
        FSServiceContract fsServiceContract2 = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Insert(fsServiceContract1);
        FSServiceContract copy1 = PXCache<FSServiceContract>.CreateCopy(src);
        copy1.RefNbr = fsServiceContract2.RefNbr;
        copy1.ServiceContractID = fsServiceContract2.ServiceContractID;
        copy1.StartDate = date;
        if (src.ExpirationType != "U" && src.StartDate.HasValue && src.EndDate.HasValue)
          copy1.EndDate = this.GetEndDate(copy1, copy1.StartDate, copy1.EndDate);
        copy1.UpcomingStatus = (string) null;
        copy1.Status = fsServiceContract2.Status;
        copy1.StatusEffectiveFromDate = new DateTime?();
        copy1.StatusEffectiveUntilDate = new DateTime?();
        copy1.NoteID = fsServiceContract2.NoteID;
        copy1.OrigServiceContractRefNbr = src.RefNbr;
        FSServiceContract fsServiceContract3 = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Update(copy1);
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) this.ServiceContractRecords).Cache, ((PXSelectBase) this.ServiceContractRecords).Cache, (object) contract, (object) fsServiceContract3, new bool?(true), new bool?(true));
        this.Answers.CopyAllAttributes((object) fsServiceContract3, (object) src);
        ((PXGraph) this).Actions.PressSave();
        FSServiceContract current2 = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
        if (src.BillingType != "APFB")
        {
          foreach (PXResult<FSContractPeriodDet> pxResult2 in PXSelectBase<FSContractPeriodDet, PXSelectReadonly<FSContractPeriodDet, Where<FSContractPeriodDet.serviceContractID, Equal<Required<FSContractPeriodDet.serviceContractID>>, And<FSContractPeriodDet.contractPeriodID, Equal<Required<FSContractPeriodDet.contractPeriodID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) src.ServiceContractID,
            (object) current1.ContractPeriodID
          }))
          {
            FSContractPeriodDet copy2 = PXCache<FSContractPeriodDet>.CreateCopy(PXResult<FSContractPeriodDet>.op_Implicit(pxResult2));
            copy2.ServiceContractID = current2.ServiceContractID;
            copy2.ContractPeriodID = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current.ContractPeriodID;
            copy2.ContractPeriodDetID = new int?();
            FSContractPeriodDet contractPeriodDet = (FSContractPeriodDet) ((PXSelectBase) this.ContractPeriodDetRecords).Cache.Insert((object) copy2);
            ((PXSelectBase) this.ContractPeriodDetRecords).Cache.SetDefaultExt<FSContractPeriodDet.remainingQty>((object) contractPeriodDet);
            ((PXSelectBase) this.ContractPeriodDetRecords).Cache.SetDefaultExt<FSContractPeriodDet.remainingTime>((object) contractPeriodDet);
          }
        }
        ((PXGraph) this).Actions.PressSave();
        FSServiceContract current3 = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current;
        this.CopySchedules(src.ServiceContractID, date);
        ((PXSelectBase<FSSalesPrice>) this.SalesPriceLines).Select(Array.Empty<object>());
        FSServiceContract fsServiceContract4 = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Search<FSServiceContract.refNbr>((object) current3.RefNbr, Array.Empty<object>()));
        if (src.SourcePrice == "C")
        {
          PXResultset<FSSalesPrice> pxResultset = PXSelectBase<FSSalesPrice, PXSelectReadonly<FSSalesPrice, Where<FSSalesPrice.serviceContractID, Equal<Required<FSSalesPrice.serviceContractID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) src.ServiceContractID
          });
          ((PXGraph) this).SelectTimeStamp();
          foreach (PXResult<FSSalesPrice> pxResult3 in pxResultset)
          {
            FSSalesPrice sourcePeriodPrice = PXResult<FSSalesPrice>.op_Implicit(pxResult3);
            foreach (object obj in GraphHelper.RowCast<FSSalesPrice>((IEnumerable) ((PXSelectBase<FSSalesPrice>) this.SalesPriceLines).Select(Array.Empty<object>())).Where<FSSalesPrice>((Func<FSSalesPrice, bool>) (x =>
            {
              int? inventoryId1 = x.InventoryID;
              int? inventoryId2 = sourcePeriodPrice.InventoryID;
              return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
            })))
            {
              FSSalesPrice copy3 = (FSSalesPrice) ((PXSelectBase) this.SalesPriceLines).Cache.CreateCopy(obj);
              copy3.UnitPrice = sourcePeriodPrice.UnitPrice;
              copy3.tstamp = ((PXGraph) this).TimeStamp;
              ((PXSelectBase) this.SalesPriceLines).Cache.Update((object) copy3);
            }
          }
        }
        ((PXGraph) this).Actions.PressSave();
        transactionScope.Complete();
        this.OpenCopiedContract(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current);
      }
    }
  }

  public virtual void CopySchedules(int? serviceContractID, DateTime? date)
  {
  }

  public virtual void OpenCopiedContract(FSServiceContract contract)
  {
  }

  public void UpdateSchedules(
    PXCache cache,
    FSServiceContract fsServiceContractRow,
    bool isRenewalAction = false)
  {
    if (fsServiceContractRow == null || fsServiceContractRow.Status != "D" && !isRenewalAction)
      return;
    int? nullable1 = (int?) cache.GetValueOriginal<FSServiceContract.customerID>((object) fsServiceContractRow);
    int? customerId = fsServiceContractRow.CustomerID;
    DateTime? nullable2;
    DateTime? nullable3;
    if (nullable1.GetValueOrDefault() == customerId.GetValueOrDefault() & nullable1.HasValue == customerId.HasValue)
    {
      int? nullable4 = (int?) cache.GetValueOriginal<FSServiceContract.customerLocationID>((object) fsServiceContractRow);
      nullable1 = fsServiceContractRow.CustomerLocationID;
      if (nullable4.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable4.HasValue == nullable1.HasValue)
      {
        nullable1 = (int?) cache.GetValueOriginal<FSServiceContract.projectID>((object) fsServiceContractRow);
        nullable4 = fsServiceContractRow.ProjectID;
        if (nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue)
        {
          nullable4 = (int?) cache.GetValueOriginal<FSServiceContract.dfltProjectTaskID>((object) fsServiceContractRow);
          nullable1 = fsServiceContractRow.DfltProjectTaskID;
          if (nullable4.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable4.HasValue == nullable1.HasValue)
          {
            nullable2 = (DateTime?) cache.GetValueOriginal<FSServiceContract.startDate>((object) fsServiceContractRow);
            nullable3 = fsServiceContractRow.StartDate;
            if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            {
              nullable3 = (DateTime?) cache.GetValueOriginal<FSServiceContract.endDate>((object) fsServiceContractRow);
              nullable2 = fsServiceContractRow.EndDate;
              if ((nullable3.HasValue == nullable2.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
                return;
            }
          }
        }
      }
    }
    if (fsServiceContractRow.RecordType == "NRSC")
    {
      ServiceContractScheduleEntry instance = PXGraph.CreateInstance<ServiceContractScheduleEntry>();
      foreach (FSContractSchedule contractSchedule in GraphHelper.RowCast<FSContractSchedule>((IEnumerable) ((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Select(Array.Empty<object>())).Where<FSContractSchedule>((Func<FSContractSchedule, bool>) (x => x.Active.GetValueOrDefault())))
      {
        ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSContractSchedule>.op_Implicit(((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) contractSchedule.ScheduleID, new object[2]
        {
          (object) contractSchedule.EntityID,
          (object) contractSchedule.CustomerID
        }));
        FSContractSchedule copy = (FSContractSchedule) ((PXSelectBase) instance.ContractScheduleRecords).Cache.CreateCopy((object) ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Current);
        copy.CustomerID = fsServiceContractRow.CustomerID;
        copy.CustomerLocationID = fsServiceContractRow.CustomerLocationID;
        copy.ProjectID = fsServiceContractRow.ProjectID;
        copy.DfltProjectTaskID = fsServiceContractRow.DfltProjectTaskID;
        bool flag = false;
        nullable2 = copy.EndDate;
        nullable3 = (DateTime?) cache.GetValueOriginal<FSServiceContract.endDate>((object) fsServiceContractRow);
        if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable3 = copy.EndDate;
          nullable2 = fsServiceContractRow.EndDate;
          if ((nullable3.HasValue == nullable2.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            copy.EndDate = fsServiceContractRow.EndDate;
            flag = true;
          }
        }
        nullable2 = copy.StartDate;
        nullable3 = (DateTime?) cache.GetValueOriginal<FSServiceContract.startDate>((object) fsServiceContractRow);
        if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable3 = copy.StartDate;
          nullable2 = fsServiceContractRow.StartDate;
          if ((nullable3.HasValue == nullable2.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            copy.StartDate = fsServiceContractRow.StartDate;
            flag = true;
          }
        }
        if (flag)
          copy.NextExecutionDate = SharedFunctions.GetNextExecution(((PXSelectBase) this.ContractSchedules).Cache, (FSSchedule) copy);
        ((PXSelectBase<FSContractSchedule>) instance.ContractScheduleRecords).Update(copy);
      }
      ((PXAction) instance.Save).Press();
    }
    else
    {
      if (!(fsServiceContractRow.RecordType == "IRSC"))
        return;
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      foreach (PXResult<FSContractSchedule> pxResult in ((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Select(Array.Empty<object>()))
      {
        FSContractSchedule contractSchedule = PXResult<FSContractSchedule>.op_Implicit(pxResult);
        ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) contractSchedule.ScheduleID, new object[2]
        {
          (object) contractSchedule.EntityID,
          (object) contractSchedule.CustomerID
        }));
        FSRouteContractSchedule copy = (FSRouteContractSchedule) ((PXSelectBase) instance.ContractScheduleRecords).Cache.CreateCopy((object) ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current);
        copy.CustomerID = fsServiceContractRow.CustomerID;
        copy.CustomerLocationID = fsServiceContractRow.CustomerLocationID;
        copy.ProjectID = fsServiceContractRow.ProjectID;
        copy.DfltProjectTaskID = fsServiceContractRow.DfltProjectTaskID;
        bool flag = false;
        nullable2 = copy.EndDate;
        nullable3 = (DateTime?) cache.GetValueOriginal<FSServiceContract.endDate>((object) fsServiceContractRow);
        if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable3 = copy.EndDate;
          nullable2 = fsServiceContractRow.EndDate;
          if ((nullable3.HasValue == nullable2.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            copy.EndDate = fsServiceContractRow.EndDate;
            flag = true;
          }
        }
        nullable2 = copy.StartDate;
        nullable3 = (DateTime?) cache.GetValueOriginal<FSServiceContract.startDate>((object) fsServiceContractRow);
        if ((nullable2.HasValue == nullable3.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          nullable3 = copy.StartDate;
          nullable2 = fsServiceContractRow.StartDate;
          if ((nullable3.HasValue == nullable2.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            copy.StartDate = fsServiceContractRow.StartDate;
            flag = true;
          }
        }
        if (flag)
          copy.NextExecutionDate = SharedFunctions.GetNextExecution(((PXSelectBase) this.ContractSchedules).Cache, (FSSchedule) copy);
        ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Update(copy);
      }
      ((PXAction) instance.Save).Press();
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.scheduleGenType> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.scheduleGenType>>) e).Cache;
    if (row.ScheduleGenType != null)
    {
      switch (row.ScheduleGenType)
      {
        case "NA":
          SharedFunctions.DefaultGenerationType(cache, row, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.scheduleGenType>>) e).Args);
          break;
        case "AP":
        case "SO":
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.scheduleGenType>, FSServiceContract, object>) e).NewValue = (object) row.ScheduleGenType;
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.scheduleGenType>>) e).Cancel = true;
          break;
      }
    }
    else
      SharedFunctions.DefaultGenerationType(cache, row, ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.scheduleGenType>>) e).Args);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.endDate> e)
  {
    FSServiceContract row = e.Row;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.endDate>, FSServiceContract, object>) e).NewValue = (object) this.GetEndDate(row, row.StartDate, (DateTime?) ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.endDate>, FSServiceContract, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.duration> e)
  {
    FSServiceContract row = e.Row;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.duration>, FSServiceContract, object>) e).NewValue = (object) this.GetDuration(row, (int?) ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSServiceContract, FSServiceContract.duration>, FSServiceContract, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.branchID> e)
  {
    if (e.Row == null)
      return;
    e.Row.BranchLocationID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billingPeriod> e)
  {
    if (e.Row == null)
      return;
    this.SetBillingPeriod(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.projectID> e)
  {
    if (e.Row == null)
      return;
    if (this.ContractPeriodDetRecords != null)
    {
      foreach (PXResult<FSContractPeriodDet> pxResult in ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Select(Array.Empty<object>()))
        ((PXSelectBase) this.ContractPeriodDetRecords).Cache.SetDefaultExt<FSContractPeriodDet.projectID>((object) PXResult<FSContractPeriodDet>.op_Implicit(pxResult));
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.projectID>>) e).Cache.SetDefaultExt<FSServiceContract.dfltProjectTaskID>((object) e.Row);
    if (!ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.projectID>>) e).Cache.SetDefaultExt<FSServiceContract.dfltCostCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.dfltProjectTaskID> e)
  {
    if (e.Row == null || this.ContractPeriodDetRecords == null)
      return;
    foreach (PXResult<FSContractPeriodDet> pxResult in ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Select(Array.Empty<object>()))
    {
      FSContractPeriodDet contractPeriodDet1 = PXResult<FSContractPeriodDet>.op_Implicit(pxResult);
      int? valueOriginal = (int?) ((PXSelectBase) this.ContractPeriodDetRecords).Cache.GetValueOriginal<FSContractPeriodDet.projectID>((object) contractPeriodDet1);
      int? projectId = contractPeriodDet1.ProjectID;
      int? nullable1 = valueOriginal;
      if (projectId.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId.HasValue == nullable1.HasValue)
      {
        nullable1 = contractPeriodDet1.ProjectTaskID;
        if (nullable1.HasValue)
          continue;
      }
      PMTask pmTask = (PMTask) null;
      nullable1 = contractPeriodDet1.ProjectID;
      if (nullable1.HasValue)
      {
        nullable1 = e.Row.DfltProjectTaskID;
        if (nullable1.HasValue)
          pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) contractPeriodDet1.ProjectID,
            (object) e.Row.DfltProjectTaskID
          }));
      }
      FSContractPeriodDet contractPeriodDet2 = contractPeriodDet1;
      int? nullable2;
      if (pmTask == null)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = pmTask.TaskID;
      contractPeriodDet2.ProjectTaskID = nullable2;
      ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Update(contractPeriodDet1);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.dfltCostCodeID> e)
  {
    if (e.Row == null || this.ContractPeriodDetRecords == null)
      return;
    foreach (PXResult<FSContractPeriodDet> pxResult in ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Select(Array.Empty<object>()))
    {
      FSContractPeriodDet contractPeriodDet = PXResult<FSContractPeriodDet>.op_Implicit(pxResult);
      if (ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID) || !contractPeriodDet.CostCodeID.HasValue)
      {
        contractPeriodDet.CostCodeID = e.Row.DfltCostCodeID;
        ((PXSelectBase<FSContractPeriodDet>) this.ContractPeriodDetRecords).Update(contractPeriodDet);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.startDate> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    this.UpdateSalesPrices(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.startDate>>) e).Cache, row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.startDate>>) e).Cache.SetDefaultExt<FSServiceContract.endDate>((object) e.Row);
    this.SetBillingPeriod(row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.endDate> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    if (row.UpcomingStatus == "E")
      row.StatusEffectiveUntilDate = row.EndDate;
    this.SetBillingPeriod(row);
    if (!(row.DurationType == "C"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.endDate>>) e).Cache.SetDefaultExt<FSServiceContract.duration>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.status> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    this.isStatusChanged = (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.status>, FSServiceContract, object>) e).OldValue != row.Status;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.customerID> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    int? nullable1 = row.CustomerID;
    PX.Objects.CR.Location location;
    if (!nullable1.HasValue)
      location = (PX.Objects.CR.Location) null;
    else
      location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.BAccount.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.CustomerID
      }));
    if (location == null)
    {
      FSServiceContract fsServiceContract = row;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      fsServiceContract.CustomerLocationID = nullable2;
    }
    else
      row.CustomerLocationID = location.LocationID;
    this.SetBillCustomerAndLocationID(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.customerID>>) e).Cache, row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.customerID>>) e).Cache.SetDefaultExt<FSServiceContract.projectID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.sourcePrice> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    this.UpdateSalesPrices(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.sourcePrice>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.expirationType> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.expirationType>>) e).Cache.SetDefaultExt<FSServiceContract.endDate>((object) e.Row);
    this.SetUpcommingStatus(row);
    this.SetEffectiveUntilDate(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.expirationType>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billingType> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    if (row.BillingType == "APFB" && ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current != null)
    {
      ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current.ContractPeriodID = new int?();
      ((PXSelectBase) this.ContractPeriodFilter).Cache.SetDefaultExt<FSContractPeriodFilter.actions>((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current);
    }
    if (row.BillingType == "STDB" || row.IsFixedRateContract.GetValueOrDefault())
    {
      if ((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billingType>, FSServiceContract, object>) e).OldValue != "STDB" && (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billingType>, FSServiceContract, object>) e).OldValue != "FIRB" && (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billingType>, FSServiceContract, object>) e).OldValue != "FRPB")
      {
        FSContractPeriod fsContractPeriod = new FSContractPeriod();
        fsContractPeriod.StartPeriodDate = row.StartDate ?? ((PXGraph) this).Accessinfo.BusinessDate;
        DateTime? contractPeriodEndDate = this.GetContractPeriodEndDate(row, fsContractPeriod.StartPeriodDate);
        if (contractPeriodEndDate.HasValue)
        {
          fsContractPeriod.EndPeriodDate = new DateTime?(contractPeriodEndDate.Value);
          ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Insert(fsContractPeriod);
        }
      }
      ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.actions>(((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current, (object) "MBP");
    }
    else
    {
      if (((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current == null)
        return;
      ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Delete(((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billTo> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    if (!((string) e.NewValue == "C"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billTo>>) e).Cache.SetValueExt<FSServiceContract.billCustomerID>((object) row, (object) row.CustomerID);
    row.BillLocationID = row.CustomerLocationID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.billCustomerID> e)
  {
    if (e.Row == null)
      return;
    e.Row.BillLocationID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.duration> e)
  {
    if (e.Row == null || !(e.Row.DurationType != "C"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSServiceContract, FSServiceContract.duration>>) e).Cache.SetDefaultExt<FSServiceContract.endDate>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSServiceContract> e)
  {
    if (e.Row == null)
      return;
    e.Row.HasForecast = new bool?(false);
    using (new PXConnectionScope())
    {
      PXResultset<FSContractForecast> source = PXSelectBase<FSContractForecast, PXSelectReadonly<FSContractForecast, Where<FSContractForecast.active, Equal<True>, And<FSContractForecast.serviceContractID, Equal<Required<FSContractForecast.serviceContractID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) e.Row.ServiceContractID
      });
      e.Row.HasForecast = new bool?(((IQueryable<PXResult<FSContractForecast>>) source).Count<PXResult<FSContractForecast>>() > 0);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSServiceContract> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSServiceContract>>) e).Cache;
    this.SetVisibleActivatePeriodButton(cache, row);
    this.EnableDisable_ActionButtons((PXGraph) this, cache, row);
    this.SetVisibleContractBillingSettings(cache, row);
    this.EnableDisable_Document(cache, row);
    this.SetUsageBillingCycle(row);
    this.SetBillInfo(cache, row);
    ((PXSelectBase) this.SalesPriceLines).AllowSelect = row.Mem_ShowPriceTab.GetValueOrDefault();
    ((PXSelectBase) this.SalesPriceLines).AllowSelect = row.Mem_ShowPriceTab.GetValueOrDefault();
    PXSelect<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>>> contractSelected = this.ServiceContractSelected;
    bool? memShowScheduleTab = row.Mem_ShowScheduleTab;
    bool flag = false;
    int num = memShowScheduleTab.GetValueOrDefault() == flag & memShowScheduleTab.HasValue ? 1 : 0;
    ((PXSelectBase) contractSelected).AllowSelect = num != 0;
    PXUIFieldAttribute.SetEnabled<FSServiceContract.customerID>(cache, (object) e.Row, row.Status == "D");
    PXUIFieldAttribute.SetEnabled<FSServiceContract.projectID>(cache, (object) row, row.Status == "D");
    bool valueOrDefault = row.IsFixedRateContract.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<FSContractPeriodDet.deferredCode>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.deferredCode>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FSContractPeriodDet.overageItemPrice>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.overageItemPrice>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<FSContractPeriodDet.remainingAmount>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.remainingAmount>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<FSContractPeriodDet.usedAmount>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.usedAmount>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<FSContractPeriodDet.scheduledAmount>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FSContractPeriodDet.scheduledAmount>(((PXSelectBase) this.ContractPeriodDetRecords).Cache, (object) null, !valueOrDefault);
    SharedFunctions.SetVisibleEnableProjectField<FSServiceContract.dfltProjectTaskID>(cache, (object) row, row.ProjectID);
    if (!CostCodeAttribute.UseCostCode())
      return;
    SharedFunctions.SetVisibleEnableProjectField<FSServiceContract.dfltCostCodeID>(cache, (object) row, row.ProjectID);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSServiceContract> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSServiceContract> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSServiceContract> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSServiceContract> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSServiceContract> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSServiceContract> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSServiceContract> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSServiceContract>>) e).Cache;
    if (((PXGraph) this).Accessinfo.ScreenID != "FS305800")
      cache.AllowUpdate = true;
    if (row.ExpirationType == "U")
      row.EndDate = new DateTime?();
    PXResultset<FSContractSchedule> contractRows = ((PXSelectBase<FSContractSchedule>) this.ContractSchedules).Select(Array.Empty<object>());
    this.ValidateDates(cache, row, contractRows);
    this.SetUnitPriceForSalesPricesRows(row);
    if (e.Operation != 3)
      return;
    ServiceContractScheduleEntryBase<ServiceContractScheduleEntry, FSSchedule, FSSchedule.scheduleID, FSSchedule.entityID, FSSchedule.customerID> instance = PXGraph.CreateInstance<ServiceContractScheduleEntryBase<ServiceContractScheduleEntry, FSSchedule, FSSchedule.scheduleID, FSSchedule.entityID, FSSchedule.customerID>>();
    foreach (PXResult<FSSchedule> pxResult in PXSelectBase<FSSchedule, PXSelect<FSSchedule, Where<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) row.ServiceContractID
    }))
    {
      FSSchedule fsSchedule = PXResult<FSSchedule>.op_Implicit(pxResult);
      ((PXSelectBase<FSSchedule>) instance.ContractScheduleRecords).Current = fsSchedule;
      ((PXAction) instance.Delete).Press();
    }
    PXUpdate<Set<FSServiceOrder.serviceContractID, Required<FSServiceOrder.serviceContractID>>, FSServiceOrder, Where<FSServiceOrder.serviceContractID, Equal<Required<FSServiceOrder.serviceContractID>>>>.Update((PXGraph) this, new object[2]
    {
      null,
      (object) row.ServiceContractID
    });
    PXUpdate<Set<FSAppointment.serviceContractID, Required<FSAppointment.serviceContractID>>, FSAppointment, Where<FSAppointment.serviceContractID, Equal<Required<FSAppointment.serviceContractID>>>>.Update((PXGraph) this, new object[2]
    {
      null,
      (object) row.ServiceContractID
    });
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSServiceContract> e)
  {
    if (e.Row == null)
      return;
    FSServiceContract row = e.Row;
    string valueOriginal1 = (string) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSServiceContract>>) e).Cache.GetValueOriginal<FSServiceContract.scheduleGenType>((object) row);
    int? valueOriginal2 = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSServiceContract>>) e).Cache.GetValueOriginal<FSServiceContract.branchID>((object) row);
    int? valueOriginal3 = (int?) ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSServiceContract>>) e).Cache.GetValueOriginal<FSServiceContract.branchLocationID>((object) row);
    if (e.TranStatus == null && (e.Operation == 2 || e.Operation == 1))
    {
      this.InsertContractAction((object) row, e.Operation);
      this.InsertContractActionBySchedules(e.Operation);
    }
    if ((row.BillingType == "STDB" || row.IsFixedRateContract.GetValueOrDefault()) && e.TranStatus == 1)
      ((PXSelectBase) this.ContractPeriodFilter).Cache.SetDefaultExt<FSContractPeriodFilter.contractPeriodID>((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current);
    if (e.TranStatus != null || e.Operation != 1)
      return;
    int? branchId = row.BranchID;
    int? nullable1 = valueOriginal2;
    if (branchId.GetValueOrDefault() == nullable1.GetValueOrDefault() & branchId.HasValue == nullable1.HasValue)
    {
      nullable1 = row.BranchLocationID;
      int? nullable2 = valueOriginal3;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        goto label_10;
    }
    PXUpdate<Set<FSSchedule.branchID, Required<FSSchedule.branchID>, Set<FSSchedule.branchLocationID, Required<FSSchedule.branchLocationID>>>, FSSchedule, Where<FSSchedule.customerID, Equal<Required<FSSchedule.customerID>>, And<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>>>>.Update((PXGraph) this, new object[4]
    {
      (object) row.BranchID,
      (object) row.BranchLocationID,
      (object) row.CustomerID,
      (object) row.ServiceContractID
    });
label_10:
    if (row.ScheduleGenType != valueOriginal1)
    {
      PXUpdate<Set<FSSchedule.scheduleGenType, Required<FSSchedule.scheduleGenType>>, FSSchedule, Where<FSSchedule.customerID, Equal<Required<FSSchedule.customerID>>, And<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>>>>.Update((PXGraph) this, new object[3]
      {
        (object) row.ScheduleGenType,
        (object) row.CustomerID,
        (object) row.ServiceContractID
      });
      if (row.ScheduleGenType == "NA")
        PXUpdate<Set<FSSchedule.active, Required<FSSchedule.active>>, FSSchedule, Where<FSSchedule.customerID, Equal<Required<FSSchedule.customerID>>, And<FSSchedule.entityID, Equal<Required<FSSchedule.entityID>>>>>.Update((PXGraph) this, new object[3]
        {
          (object) false,
          (object) row.CustomerID,
          (object) row.ServiceContractID
        });
      ((PXSelectBase) this.ContractSchedules).Cache.Clear();
      ((PXSelectBase) this.ContractSchedules).View.Clear();
      ((PXSelectBase) this.ContractSchedules).View.RequestRefresh();
    }
    this.UpdateSchedules(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSServiceContract>>) e).Cache, row, this.IsRenewContract);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSalesPrice> e)
  {
    if (e.Row == null)
      return;
    FSSalesPrice row = e.Row;
    if (((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current == null)
      return;
    if (((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.SourcePrice == "C")
      row.Mem_UnitPrice = row.UnitPrice;
    else
      row.Mem_UnitPrice = new Decimal?(this.GetSalesPrice(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSalesPrice>>) e).Cache, row) ?? 0.0M);
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSalesPrice> e)
  {
    if (e.Row == null || ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current == null)
      return;
    FSSalesPrice row = e.Row;
    bool flag = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.SourcePrice == "C";
    PXUIFieldAttribute.SetEnabled<FSSalesPrice.mem_UnitPrice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSalesPrice>>) e).Cache, (object) row, flag);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSalesPrice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSalesPrice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSalesPrice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSalesPrice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSalesPrice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSalesPrice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSalesPrice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSalesPrice> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSContractPeriodFilter, FSContractPeriodFilter.actions> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodFilter row = e.Row;
    if (((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSContractPeriodFilter, FSContractPeriodFilter.actions>, FSContractPeriodFilter, object>) e).NewValue = (object) ServiceContractEntryBase<TGraph, TPrimary, TWhere>.GetContractPeriodFilterDefaultAction((PXGraph) this, ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current.ServiceContractID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSContractPeriodFilter, FSContractPeriodFilter.actions> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodFilter row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSContractPeriodFilter, FSContractPeriodFilter.actions>>) e).Cache.SetDefaultExt<FSContractPeriodFilter.contractPeriodID>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSContractPeriodFilter> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodFilter row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSContractPeriodFilter>>) e).Cache;
    PXUIFieldAttribute.SetEnabled<FSContractPeriodFilter.contractPeriodID>(cache, (object) row, row.Actions == "SBP");
    PXUIFieldAttribute.SetVisible<FSContractPeriodFilter.postDocRefNbr>(cache, (object) row, row.Actions == "SBP");
    FSContractPeriod fsContractPeriod = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current;
    FSContractPostDoc fsContractPostDoc = ((PXSelectBase<FSContractPostDoc>) this.ContractPostDocRecords).Current;
    if (fsContractPeriod == null)
    {
      fsContractPeriod = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current = PXResultset<FSContractPeriod>.op_Implicit(((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Select(Array.Empty<object>()));
      ((PXSelectBase) this.ContractPeriodFilter).Cache.SetDefaultExt<FSContractPeriodFilter.contractPeriodID>((object) ((PXSelectBase<FSContractPeriodFilter>) this.ContractPeriodFilter).Current);
    }
    int? contractPeriodId1;
    int? contractPeriodId2;
    if (fsContractPeriod != null)
    {
      contractPeriodId1 = fsContractPeriod.ContractPeriodID;
      contractPeriodId2 = row.ContractPeriodID;
      if (!(contractPeriodId1.GetValueOrDefault() == contractPeriodId2.GetValueOrDefault() & contractPeriodId1.HasValue == contractPeriodId2.HasValue))
        fsContractPeriod = ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current = PXResultset<FSContractPeriod>.op_Implicit(((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Select(Array.Empty<object>()));
    }
    bool? nullable;
    if (fsContractPostDoc == null && fsContractPeriod != null)
    {
      nullable = fsContractPeriod.Invoiced;
      if (nullable.GetValueOrDefault())
        fsContractPostDoc = ((PXSelectBase<FSContractPostDoc>) this.ContractPostDocRecords).Current = PXResultset<FSContractPostDoc>.op_Implicit(((PXSelectBase<FSContractPostDoc>) this.ContractPostDocRecords).Select(Array.Empty<object>()));
    }
    if (fsContractPostDoc != null)
    {
      contractPeriodId2 = fsContractPostDoc.ContractPeriodID;
      contractPeriodId1 = row.ContractPeriodID;
      if (!(contractPeriodId2.GetValueOrDefault() == contractPeriodId1.GetValueOrDefault() & contractPeriodId2.HasValue == contractPeriodId1.HasValue))
        fsContractPostDoc = ((PXSelectBase<FSContractPostDoc>) this.ContractPostDocRecords).Current = PXResultset<FSContractPostDoc>.op_Implicit(((PXSelectBase<FSContractPostDoc>) this.ContractPostDocRecords).Select(Array.Empty<object>()));
    }
    int num;
    if (fsContractPeriod != null)
    {
      if (!(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.BillingType == "STDB") || !(row.Actions != "SBP") || !(fsContractPeriod.Status == "I") || !((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.isEditable())
      {
        nullable = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.IsFixedRateContract;
        num = !nullable.GetValueOrDefault() ? 0 : (!(row.Actions == "SBP") || !(fsContractPeriod.Status == "A") && !(fsContractPeriod.Status == "P") ? (row.Actions == "MBP" ? 1 : 0) : 1);
      }
      else
        num = 1;
    }
    else
      num = 0;
    bool flag = num != 0;
    ((PXSelectBase) this.ContractPeriodDetRecords).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.ContractPeriodDetRecords).Cache.AllowInsert = flag;
    ((PXSelectBase) this.ContractPeriodDetRecords).Cache.AllowDelete = flag;
    if (fsContractPeriod != null)
    {
      contractPeriodId1 = row.ContractPeriodID;
      if (contractPeriodId1.HasValue)
      {
        row.PostDocRefNbr = fsContractPostDoc == null ? string.Empty : fsContractPostDoc.PostRefNbr;
        row.StandardizedBillingTotal = fsContractPeriod.PeriodTotal;
        goto label_21;
      }
    }
    row.PostDocRefNbr = string.Empty;
    row.StandardizedBillingTotal = new Decimal?(0M);
label_21:
    ((PXAction) this.activatePeriod).SetEnabled(this.EnableDisableActivatePeriodButton(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current, ((PXSelectBase<FSContractPeriod>) this.ContractPeriodRecords).Current));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSContractPeriodFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.amount> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    this.AmountFieldSelectingHandler(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.amount>>) e).Cache, ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.amount>>) e).Args, typeof (FSContractPeriodDet.amount).Name, row.BillingRule, row.Time, row.Qty);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.usedAmount> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    this.AmountFieldSelectingHandler(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.usedAmount>>) e).Cache, ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.usedAmount>>) e).Args, typeof (FSContractPeriodDet.usedAmount).Name, row.BillingRule, row.UsedTime, row.UsedQty);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.remainingAmount> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    this.AmountFieldSelectingHandler(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.remainingAmount>>) e).Cache, ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.remainingAmount>>) e).Args, typeof (FSContractPeriodDet.remainingAmount).Name, row.BillingRule, row.RemainingTime, row.RemainingQty);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.scheduledAmount> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    this.AmountFieldSelectingHandler(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.scheduledAmount>>) e).Cache, ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<FSContractPeriodDet, FSContractPeriodDet.scheduledAmount>>) e).Args, typeof (FSContractPeriodDet.scheduledAmount).Name, row.BillingRule, row.ScheduledTime, row.ScheduledQty);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.deferredCode> e)
  {
    if (e.Row == null || ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current == null || !((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.IsFixedRateContract.GetValueOrDefault())
      return;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<FSContractPeriodDet.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.deferredCode>>) e).Cache, (object) e.Row);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.deferredCode>, FSContractPeriodDet, object>) e).NewValue = (object) inventoryItem?.DeferredCode;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.qty> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<FSContractPeriodDet.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.qty>>) e).Cache, (object) e.Row);
    if (inventoryItem == null)
      return;
    FSxService extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItem);
    if (!((e.Row.BillingRule ?? extension.BillingRule) == "TIME"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.qty>, FSContractPeriodDet, object>) e).NewValue = (object) Decimal.Divide((Decimal) e.Row.Time.GetValueOrDefault(), 60M);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.qty>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.costCodeID> e)
  {
    if (e.Row == null || ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current == null || ProjectDefaultAttribute.IsNonProject(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ProjectID) || !CostCodeAttribute.UseCostCode())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.costCodeID>, FSContractPeriodDet, object>) e).NewValue = (object) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.DfltCostCodeID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSContractPeriodDet, FSContractPeriodDet.costCodeID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.billingRule> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    this.SetDefaultQtyTime(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.billingRule>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.amount> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.amount>>) e).Cache;
    if (row.BillingRule == "TIME")
    {
      int result = 0;
      if (!int.TryParse(row.Amount.Replace(" ", "0"), out result))
        return;
      int minutes = result % 100;
      TimeSpan timeSpan = new TimeSpan(0, (result - minutes) / 100, minutes, 0);
      cache.SetValueExt<FSContractPeriodDet.time>((object) row, (object) (int) timeSpan.TotalMinutes);
    }
    else
    {
      if (!(row.BillingRule == "FLRA"))
        return;
      Decimal result = 0.0M;
      Decimal.TryParse(row.Amount, out result);
      cache.SetValueExt<FSContractPeriodDet.qty>((object) row, (object) result);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.inventoryID> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.inventoryID>>) e).Cache;
    this.SetDefaultBillingRule(cache, row);
    cache.SetDefaultExt<FSContractPeriodDet.uOM>((object) e.Row);
    ServiceContractEntryBase<TGraph, TPrimary, TWhere>.SetRegularPrice(cache, row, ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current);
    cache.SetValueExt<FSContractPeriodDet.recurringUnitPrice>((object) row, (object) row.RegularPrice);
    cache.SetValueExt<FSContractPeriodDet.overageItemPrice>((object) row, (object) row.RegularPrice);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.uOM> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSContractPeriodDet, FSContractPeriodDet.uOM>>) e).Cache;
    ServiceContractEntryBase<TGraph, TPrimary, TWhere>.SetRegularPrice(cache, row, ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current);
    cache.SetValueExt<FSContractPeriodDet.recurringUnitPrice>((object) row, (object) row.RegularPrice);
    cache.SetValueExt<FSContractPeriodDet.overageItemPrice>((object) row, (object) row.RegularPrice);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSContractPeriodDet> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    using (new PXConnectionScope())
    {
      ServiceContractEntryBase<TGraph, TPrimary, TWhere>.SetRegularPrice(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSContractPeriodDet>>) e).Cache, row, ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current);
      IEnumerable<PXResult<FSSODet>> source1 = ((IEnumerable<PXResult<FSSODet>>) PXSelectBase<FSSODet, PXSelectJoin<FSSODet, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSSODet.sOID>>>, Where2<Where<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.billContractPeriodID, Equal<Required<FSServiceOrder.billContractPeriodID>>, And<FSServiceOrder.canceled, Equal<False>, And<FSServiceOrder.allowInvoice, Equal<False>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>>>>>>, And<Where2<Where<FSSODet.inventoryID, Equal<Required<FSSODet.inventoryID>>, And<FSSODet.contractRelated, Equal<True>>>, And<Where2<Where<FSSODet.billingRule, Equal<Required<FSSODet.billingRule>>, Or<Required<FSSODet.billingRule>, IsNull>>, And<Where<FSSODet.SMequipmentID, Equal<Required<FSSODet.SMequipmentID>>, Or<Required<FSSODet.SMequipmentID>, IsNull>>>>>>>>>.Config>.Select((PXGraph) this, new object[7]
      {
        (object) row.ServiceContractID,
        (object) row.ContractPeriodID,
        (object) row.InventoryID,
        (object) row.BillingRule,
        (object) row.BillingRule,
        (object) row.SMEquipmentID,
        (object) row.SMEquipmentID
      })).AsEnumerable<PXResult<FSSODet>>();
      IEnumerable<PXResult<FSAppointmentDet>> source2 = ((IEnumerable<PXResult<FSAppointmentDet>>) PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>>>, Where2<Where<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.billContractPeriodID, Equal<Required<FSAppointment.billContractPeriodID>>, And<FSAppointment.closed, Equal<False>, And<FSAppointment.canceled, Equal<False>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>>>>>>, And<Where2<Where<FSAppointmentDet.inventoryID, Equal<Required<FSAppointmentDet.inventoryID>>, And<FSAppointmentDet.contractRelated, Equal<True>>>, And<Where2<Where<FSSODet.billingRule, Equal<Required<FSSODet.billingRule>>, Or<Required<FSSODet.billingRule>, IsNull>>, And<Where<FSAppointmentDet.SMequipmentID, Equal<Required<FSAppointmentDet.SMequipmentID>>, Or<Required<FSAppointmentDet.SMequipmentID>, IsNull>>>>>>>>>.Config>.Select((PXGraph) this, new object[7]
      {
        (object) row.ServiceContractID,
        (object) row.ContractPeriodID,
        (object) row.InventoryID,
        (object) row.BillingRule,
        (object) row.BillingRule,
        (object) row.SMEquipmentID,
        (object) row.SMEquipmentID
      })).AsEnumerable<PXResult<FSAppointmentDet>>();
      Decimal? nullable1 = new Decimal?(0M);
      if (source1.Count<PXResult<FSSODet>>() > 0 || source2.Count<PXResult<FSAppointmentDet>>() > 0)
      {
        nullable1 = source1.Sum<PXResult<FSSODet>>((Func<PXResult<FSSODet>, Decimal?>) (x => PXResult<FSSODet>.op_Implicit(x).EstimatedQty));
        foreach (PXResult<FSAppointmentDet, FSAppointment, FSSODet> pxResult in source2)
        {
          FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet, FSAppointment, FSSODet>.op_Implicit(pxResult);
          Decimal? nullable2;
          Decimal? nullable3;
          if (PXResult<FSAppointmentDet, FSAppointment, FSSODet>.op_Implicit(pxResult).NotStarted.GetValueOrDefault())
          {
            nullable2 = nullable1;
            nullable3 = fsAppointmentDet.EstimatedQty;
            nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            nullable3 = nullable1;
            nullable2 = fsAppointmentDet.ActualQty;
            nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          }
        }
      }
      if (row.BillingRule == "FLRA")
      {
        row.ScheduledQty = nullable1;
      }
      else
      {
        if (!(row.BillingRule == "TIME"))
          return;
        FSContractPeriodDet contractPeriodDet = row;
        Decimal? nullable4 = nullable1;
        Decimal num = (Decimal) 60;
        int? nullable5 = nullable4.HasValue ? new int?((int) (nullable4.GetValueOrDefault() * num)) : new int?();
        contractPeriodDet.ScheduledTime = nullable5;
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSContractPeriodDet> e)
  {
    if (e.Row == null)
      return;
    FSContractPeriodDet row = e.Row;
    ServiceContractEntryBase<TGraph, TPrimary, TWhere>.EnableDisableContractPeriodDet(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSContractPeriodDet>>) e).Cache, row);
    SharedFunctions.SetEnableCostCodeProjectTask<FSContractPeriodDet.projectTaskID, FSContractPeriodDet.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSContractPeriodDet>>) e).Cache, (object) row, row.LineType, (int?) ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current?.ProjectID);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSContractPeriodDet> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSActivationContractFilter, FSActivationContractFilter.activationDate> e)
  {
    if (e.Row == null)
      return;
    FSActivationContractFilter row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSActivationContractFilter, FSActivationContractFilter.activationDate>>) e).Cache;
    DateTime? nullable = row.ActivationDate;
    if (nullable.HasValue)
    {
      nullable = row.ActivationDate;
      DateTime date1 = nullable.Value;
      DateTime date2 = date1.Date;
      nullable = ((PXGraph) this).Accessinfo.BusinessDate;
      date1 = nullable.Value;
      DateTime date3 = date1.Date;
      if (date2 < date3)
        cache.RaiseExceptionHandling<FSActivationContractFilter.activationDate>((object) row, (object) row.ActivationDate, (Exception) new PXSetPropertyException("The effective date must be later than or the same as the actual date.", (PXErrorLevel) 4));
      if (((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ExpirationType == "E")
      {
        date1 = row.ActivationDate.Value.Date;
        nullable = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.EndDate;
        if ((nullable.HasValue ? (date1 >= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          cache.RaiseExceptionHandling<FSActivationContractFilter.activationDate>((object) row, (object) row.ActivationDate, (Exception) new PXSetPropertyException("The effective date must be earlier than the expiration date.", (PXErrorLevel) 4));
      }
      foreach (PXResult<ActiveSchedule> pxResult in ((PXSelectBase<ActiveSchedule>) this.ActiveScheduleRecords).Select(Array.Empty<object>()))
      {
        ActiveSchedule activeSchedule = PXResult<ActiveSchedule>.op_Implicit(pxResult);
        if (activeSchedule.ChangeRecurrence.GetValueOrDefault())
          ((PXSelectBase) this.ActiveScheduleRecords).Cache.SetValueExt<ActiveSchedule.effectiveRecurrenceStartDate>((object) activeSchedule, (object) ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate);
      }
    }
    else
      cache.RaiseExceptionHandling<FSActivationContractFilter.activationDate>((object) row, (object) row.ActivationDate, (Exception) new PXSetPropertyException("This element cannot be empty.", (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSTerminateContractFilter, FSTerminateContractFilter.cancelationDate> e)
  {
    if (e.Row == null)
      return;
    FSTerminateContractFilter row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSTerminateContractFilter, FSTerminateContractFilter.cancelationDate>>) e).Cache;
    DateTime? nullable = row.CancelationDate;
    if (nullable.HasValue)
    {
      nullable = row.CancelationDate;
      DateTime date1 = nullable.Value;
      DateTime date2 = date1.Date;
      nullable = ((PXGraph) this).Accessinfo.BusinessDate;
      date1 = nullable.Value;
      DateTime date3 = date1.Date;
      if (date2 < date3)
        cache.RaiseExceptionHandling<FSTerminateContractFilter.cancelationDate>((object) row, (object) row.CancelationDate, (Exception) new PXSetPropertyException("The effective date must be later than or the same as the actual date.", (PXErrorLevel) 4));
      if (!(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ExpirationType == "E"))
        return;
      date1 = row.CancelationDate.Value.Date;
      nullable = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.EndDate;
      if ((nullable.HasValue ? (date1 >= nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
      cache.RaiseExceptionHandling<FSTerminateContractFilter.cancelationDate>((object) row, (object) row.CancelationDate, (Exception) new PXSetPropertyException("The effective date must be earlier than the expiration date.", (PXErrorLevel) 4));
    }
    else
      cache.RaiseExceptionHandling<FSTerminateContractFilter.cancelationDate>((object) row, (object) row.CancelationDate, (Exception) new PXSetPropertyException("This element cannot be empty.", (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSuspendContractFilter, FSSuspendContractFilter.suspensionDate> e)
  {
    if (e.Row == null)
      return;
    FSSuspendContractFilter row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSuspendContractFilter, FSSuspendContractFilter.suspensionDate>>) e).Cache;
    DateTime? nullable = row.SuspensionDate;
    if (nullable.HasValue)
    {
      nullable = row.SuspensionDate;
      DateTime date1 = nullable.Value;
      DateTime date2 = date1.Date;
      nullable = ((PXGraph) this).Accessinfo.BusinessDate;
      date1 = nullable.Value;
      DateTime date3 = date1.Date;
      if (date2 < date3)
        cache.RaiseExceptionHandling<FSSuspendContractFilter.suspensionDate>((object) row, (object) row.SuspensionDate, (Exception) new PXSetPropertyException("The effective date must be later than or the same as the actual date.", (PXErrorLevel) 4));
      if (!(((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.ExpirationType == "E"))
        return;
      date1 = row.SuspensionDate.Value.Date;
      nullable = ((PXSelectBase<FSServiceContract>) this.ServiceContractRecords).Current.EndDate;
      if ((nullable.HasValue ? (date1 >= nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
      cache.RaiseExceptionHandling<FSSuspendContractFilter.suspensionDate>((object) row, (object) row.SuspensionDate, (Exception) new PXSetPropertyException("The effective date must be earlier than the expiration date.", (PXErrorLevel) 4));
    }
    else
      cache.RaiseExceptionHandling<FSSuspendContractFilter.suspensionDate>((object) row, (object) row.SuspensionDate, (Exception) new PXSetPropertyException("This element cannot be empty.", (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<ActiveSchedule, ActiveSchedule.effectiveRecurrenceStartDate> e)
  {
    if (e.Row == null)
      return;
    ActiveSchedule row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<ActiveSchedule, ActiveSchedule.effectiveRecurrenceStartDate>>) e).Cache;
    DateTime? nullable = ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate;
    if (!nullable.HasValue)
      return;
    nullable = row.EffectiveRecurrenceStartDate;
    if (nullable.HasValue)
      return;
    if (row.ChangeRecurrence.GetValueOrDefault())
    {
      cache.SetValueExt<ActiveSchedule.effectiveRecurrenceStartDate>((object) row, (object) ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate);
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<ActiveSchedule, ActiveSchedule.effectiveRecurrenceStartDate>>) e).ReturnValue = (object) ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate;
    }
    else
    {
      cache.SetValueExt<ActiveSchedule.effectiveRecurrenceStartDate>((object) row, (object) row.StartDate);
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<ActiveSchedule, ActiveSchedule.effectiveRecurrenceStartDate>>) e).ReturnValue = (object) row.StartDate;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ActiveSchedule, ActiveSchedule.changeRecurrence> e)
  {
    if (e.Row == null || !((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate.HasValue)
      return;
    ActiveSchedule row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ActiveSchedule, ActiveSchedule.changeRecurrence>>) e).Cache;
    if (row.ChangeRecurrence.GetValueOrDefault())
      cache.SetValueExt<ActiveSchedule.effectiveRecurrenceStartDate>((object) row, (object) ((PXSelectBase<FSActivationContractFilter>) this.ActivationContractFilter).Current.ActivationDate);
    else
      cache.SetValueExt<ActiveSchedule.effectiveRecurrenceStartDate>((object) row, (object) row.StartDate);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ActiveSchedule, ActiveSchedule.effectiveRecurrenceStartDate> e)
  {
    if (e.Row == null)
      return;
    ActiveSchedule row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ActiveSchedule, ActiveSchedule.effectiveRecurrenceStartDate>>) e).Cache;
    DateTime? nullable1 = row.EffectiveRecurrenceStartDate;
    if (!nullable1.HasValue)
      return;
    ActiveSchedule copy = (ActiveSchedule) cache.CreateCopy((object) row);
    ActiveSchedule activeSchedule = copy;
    nullable1 = new DateTime?();
    DateTime? nullable2 = nullable1;
    activeSchedule.EndDate = nullable2;
    copy.StartDate = row.EffectiveRecurrenceStartDate;
    row.NextExecution = SharedFunctions.GetNextExecution(cache, (FSSchedule) copy);
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

  /// <summary>Update visibility and other UI things for duration options</summary>
  protected virtual void EnableDisableRenewalFields(
    PXCache cache,
    FSServiceContract serviceContract)
  {
    bool flag1 = serviceContract.ExpirationType == "E" || serviceContract.ExpirationType == "R";
    bool flag2 = serviceContract.Status == "D";
    PXUIFieldAttribute.SetVisible<FSServiceContract.duration>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<FSServiceContract.durationType>(cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<FSServiceContract.renewalDate>(cache, (object) null, serviceContract.ExpirationType == "R");
    PXUIFieldAttribute.SetEnabled<FSServiceContract.durationType>(cache, (object) serviceContract, flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceContract.duration>(cache, (object) serviceContract, flag2 && serviceContract.DurationType != "C");
    PXUIFieldAttribute.SetEnabled<FSServiceContract.endDate>(cache, (object) serviceContract, flag2 && serviceContract.DurationType == "C");
  }

  protected virtual DateTime? GetEndDate(
    FSServiceContract scRow,
    DateTime? startDate,
    DateTime? actualValue)
  {
    if (scRow.ExpirationType != "E" && scRow.ExpirationType != "R")
      return actualValue;
    int? duration1 = scRow.Duration;
    int num = 0;
    if (duration1.GetValueOrDefault() == num & duration1.HasValue || !startDate.HasValue)
      return startDate;
    string durationType1 = scRow.DurationType;
    if (!(durationType1 == "Y") && !(durationType1 == "Q") && !(durationType1 == "M"))
      return actualValue;
    DateTime date = startDate.Value;
    string durationType2 = scRow.DurationType;
    duration1 = scRow.Duration;
    int duration2 = duration1 ?? 1;
    return new DateTime?(this.GetEndDateFromDuration(date, durationType2, duration2));
  }

  public virtual DateTime GetEndDateFromDuration(DateTime date, string durationType, int duration)
  {
    switch (durationType)
    {
      case "Y":
        return this.AddMonth(date, duration * this.GetBaseDurationOnMonths(durationType)).AddDays(-1.0);
      case "Q":
        return this.AddMonth(date, duration * this.GetBaseDurationOnMonths(durationType)).AddDays(-1.0);
      case "M":
        return this.AddMonth(date, duration * this.GetBaseDurationOnMonths(durationType)).AddDays(-1.0);
      default:
        throw new NotImplementedException();
    }
  }

  public virtual int GetBaseDurationOnMonths(string durationType)
  {
    switch (durationType)
    {
      case "Y":
        return 12;
      case "Q":
        return 3;
      case "M":
        return 1;
      default:
        throw new NotImplementedException();
    }
  }

  public virtual DateTime AddMonth(DateTime date, int count)
  {
    if (count == 0)
      return date;
    if (date.Day != DateTime.DaysInMonth(date.Year, date.Month))
      return date.AddMonths(count);
    DateTime dateTime = date.AddDays(1.0);
    dateTime = dateTime.AddMonths(count);
    return dateTime.AddDays(-1.0);
  }

  protected virtual int? GetDuration(FSServiceContract scRow, int? actualValue)
  {
    if (scRow.ExpirationType != "E" && scRow.ExpirationType != "R" || !(scRow.DurationType == "C"))
      return actualValue;
    if (!scRow.StartDate.HasValue || !scRow.EndDate.HasValue)
      return new int?();
    int? duration;
    ref int? local = ref duration;
    DateTime? nullable1 = scRow.EndDate;
    DateTime dateTime1 = nullable1.Value;
    nullable1 = scRow.RenewalDate;
    DateTime dateTime2;
    if (!nullable1.HasValue)
    {
      nullable1 = scRow.StartDate;
      dateTime2 = nullable1.Value;
    }
    else
    {
      nullable1 = scRow.RenewalDate;
      dateTime2 = nullable1.Value;
    }
    int num1 = (dateTime1 - dateTime2).Days + 1;
    local = new int?(num1);
    int? nullable2 = duration;
    int num2 = 0;
    if (nullable2.GetValueOrDefault() < num2 & nullable2.HasValue)
      duration = new int?(0);
    return duration;
  }

  private bool RefNbrRequired()
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current == null || ((PXSelectBase<FSSetup>) this.SetupRecord).Current.ServiceContractNumberingID == null)
      return false;
    return PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSSetup>) this.SetupRecord).Current.ServiceContractNumberingID
    })).UserNumbering.GetValueOrDefault();
  }

  private bool RefNbrIsValid(string refNbr, out string errorMessage)
  {
    errorMessage = string.Empty;
    if (string.IsNullOrEmpty(refNbr))
    {
      string displayName = PXUIFieldAttribute.GetDisplayName<FSCopyContractFilter.refNbr>(((PXSelectBase) this.CopyContractFilter).Cache);
      errorMessage = PXLocalizer.LocalizeFormat("{0} cannot be empty.", new object[1]
      {
        (object) displayName
      });
      return false;
    }
    if (((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current != null && refNbr == ((PXSelectBase<FSServiceContract>) this.ServiceContractSelected).Current.RefNbr)
    {
      errorMessage = "The service contract ID must be different from the service contract ID of the source service contract.";
      return false;
    }
    if (!((IQueryable<PXResult<FSServiceContract>>) PXSelectBase<FSServiceContract, PXSelect<FSServiceContract, Where<FSServiceContract.refNbr, Equal<Required<FSServiceContract.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) refNbr
    })).Any<PXResult<FSServiceContract>>())
      return true;
    errorMessage = "A service contract with the specified ID already exists. Specify another service contract ID for a new service contract.";
    return false;
  }

  public static class FSMailing
  {
    public const string EMAIL_SERVICE_CONTRACT_QUOTE = "FS CONTRACT QUOTE";
  }

  public class ServiceContractEntryBase_ActivityDetailsExt : 
    ActivityDetailsExt<ServiceContractEntryBase<TGraph, TPrimary, TWhere>, FSServiceContract, FSServiceContract.noteID>
  {
    public override string GetPrimaryRecipientFromContext(
      NotificationUtility utility,
      string type,
      object row,
      NotificationSource source)
    {
      List<MailAddress> mailAddressList = new List<MailAddress>();
      if (!(((PXGraph) this.Base).Caches[typeof (FSServiceContract)].Current is FSServiceContract current) || source == null || string.IsNullOrEmpty(current.EmailNotificationCD))
        return (string) null;
      NotificationSetup notificationSetup = NotificationSetup.PK.Find((PXGraph) this.Base, source.SetupID);
      if (notificationSetup == null)
        return (string) null;
      using (IEnumerator<PXResult<NotificationSetupRecipient>> enumerator = PXSelectBase<NotificationSetupRecipient, PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Required<NotificationSetupRecipient.setupID>>, And<NotificationSetupRecipient.active, Equal<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) notificationSetup.SetupID
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          NotificationSetupRecipient notificationSetupRecipient = PXResult<NotificationSetupRecipient>.op_Implicit(enumerator.Current);
          string address = string.Empty;
          switch (notificationSetupRecipient.ContactType)
          {
            case "C":
              PX.Objects.CR.Contact contact1 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) current.CustomerContactID
              }));
              if (contact1 != null && contact1.EMail != null)
              {
                address = contact1.EMail;
                break;
              }
              break;
            case "B":
              PX.Objects.CR.Contact contact2 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) current.BillCustomerID
              }));
              if (contact2 != null && contact2.EMail != null)
              {
                address = contact2.EMail;
                break;
              }
              break;
            case "U":
              PX.Objects.CR.Contact contact3 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) current.CustomerID
              }));
              if (contact3 != null && contact3.EMail != null)
              {
                address = contact3.EMail;
                break;
              }
              break;
            case "X":
              PX.Objects.CR.Contact contact4 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) current.VendorID
              }));
              if (contact4 != null && contact4.EMail != null)
              {
                address = contact4.EMail;
                break;
              }
              break;
            case "L":
              PX.Objects.CR.Contact contact5 = PXResult<PX.Objects.AR.SalesPerson, EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>.op_Implicit((PXResult<PX.Objects.AR.SalesPerson, EPEmployee, PX.Objects.CR.BAccount, PX.Objects.CR.Contact>) PXResultset<PX.Objects.AR.SalesPerson>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesPerson, PXSelectJoin<PX.Objects.AR.SalesPerson, InnerJoin<EPEmployee, On<EPEmployee.salesPersonID, Equal<PX.Objects.AR.SalesPerson.salesPersonID>>, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<EPEmployee.bAccountID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.BAccount.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.CR.BAccount.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>>, Where<PX.Objects.AR.SalesPerson.salesPersonID, Equal<Required<FSAppointment.salesPersonID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) current.SalesPersonID
              })));
              if (contact5 != null && contact5.EMail != null)
              {
                address = contact5.EMail;
                break;
              }
              break;
            case "E":
              PX.Objects.CR.Contact contact6 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) notificationSetupRecipient.ContactID
              }));
              if (contact6 != null && contact6.EMail != null)
              {
                address = contact6.EMail;
                break;
              }
              break;
          }
          if (string.IsNullOrEmpty(address))
          {
            mailAddressList.Add(new MailAddress(address));
            source.RecipientsBehavior = "O";
          }
          return PXDBEmailAttribute.ToString((IEnumerable<MailAddress>) mailAddressList);
        }
      }
      return (string) null;
    }

    public class FSContractContactType : NotificationContactType
    {
      public const string Customer = "U";
      public const string Vendor = "X";
      public const string Salesperson = "L";
    }
  }
}
