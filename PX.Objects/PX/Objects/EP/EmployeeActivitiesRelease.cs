// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeActivitiesRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.EP;

public class EmployeeActivitiesRelease : PXGraph<
#nullable disable
EmployeeActivitiesRelease>
{
  public PXCancel<EmployeeActivitiesRelease.EPActivityFilter> Cancel;
  public PXAction<EmployeeActivitiesRelease.EPActivityFilter> viewDetails;
  [PXHidden]
  public PXSelect<CRCase> dummyCase;
  [PXHidden]
  public PXSelect<ContractEx> dummyContract;
  public PXFilter<EmployeeActivitiesRelease.EPActivityFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<EPActivityApprove, EmployeeActivitiesRelease.EPActivityFilter, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<PMTimeActivity.earningTypeID>>, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPActivityApprove.ownerID>, And<Where<EPEmployee.timeCardRequired, NotEqual<True>, Or<EPEmployee.timeCardRequired, IsNull>>>>, LeftJoin<CRActivityLink, On<CRActivityLink.noteID, Equal<PMTimeActivity.refNoteID>>, LeftJoin<CRCase, On<CRCase.noteID, Equal<CRActivityLink.refNoteID>>, LeftJoin<CRCaseClass, On<CRCaseClass.caseClassID, Equal<CRCase.caseClassID>>, LeftJoin<ContractEx, On<CRCase.contractID, Equal<ContractEx.contractID>>>>>>>>, Where2<Where2<Where<EPActivityApprove.approvalStatus, Equal<ActivityStatusListAttribute.completed>, And<EPActivityApprove.approverID, IsNull>>, Or<Where<EPActivityApprove.approvalStatus, Equal<ActivityStatusListAttribute.approved>, And<EPActivityApprove.approverID, IsNotNull>>>>, And<EPActivityApprove.date, Less<Current<EmployeeActivitiesRelease.EPActivityFilter.tillDatePlusOne>>, And2<Where<EPActivityApprove.date, GreaterEqual<Current<EmployeeActivitiesRelease.EPActivityFilter.fromDate>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.fromDate>, IsNull>>, And2<Where<EPActivityApprove.projectID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.projectID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.projectID>, IsNull>>, And2<Where<EPActivityApprove.projectTaskID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.projectTaskID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.projectTaskID>, IsNull>>, And2<Where<ContractEx.contractID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.contractID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.contractID>, IsNull, Or<EPActivityApprove.contractID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.contractID>>>>>, And<EPActivityApprove.projectID, IsNotNull, And<PMTimeActivity.released, NotEqual<True>, And<EPActivityApprove.trackTime, Equal<True>, And<PMTimeActivity.origNoteID, IsNull, And2<Where<EPActivityApprove.ownerID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.employeeID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.employeeID>, IsNull>>, And<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>, Or<CRCaseClass.caseClassID, IsNull, Or<CRCaseClass.isBillable, Equal<False>>>>>>>>>>>>>>>>, OrderBy<Desc<EPActivityApprove.date>>> Activity;
  public PXSelectJoinGroupBy<EPActivityApprove, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<EPActivityApprove.ownerID>, And<Where<EPEmployee.timeCardRequired, NotEqual<True>, Or<EPEmployee.timeCardRequired, IsNull>>>>, LeftJoin<CRActivityLink, On<CRActivityLink.noteID, Equal<PMTimeActivity.refNoteID>>, LeftJoin<CRCase, On<CRCase.noteID, Equal<CRActivityLink.refNoteID>>, LeftJoin<CRCaseClass, On<CRCaseClass.caseClassID, Equal<CRCase.caseClassID>>, LeftJoin<ContractEx, On<CRCase.contractID, Equal<ContractEx.contractID>>>>>>>, Where2<Where2<Where<EPActivityApprove.approvalStatus, Equal<ActivityStatusListAttribute.completed>, And<EPActivityApprove.approverID, IsNull>>, Or<Where<EPActivityApprove.approvalStatus, Equal<ActivityStatusListAttribute.approved>, And<EPActivityApprove.approverID, IsNotNull>>>>, And<EPActivityApprove.date, Less<Current<EmployeeActivitiesRelease.EPActivityFilter.tillDatePlusOne>>, And2<Where<EPActivityApprove.date, GreaterEqual<Current<EmployeeActivitiesRelease.EPActivityFilter.fromDate>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.fromDate>, IsNull>>, And2<Where<EPActivityApprove.projectID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.projectID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.projectID>, IsNull>>, And2<Where<EPActivityApprove.projectTaskID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.projectTaskID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.projectTaskID>, IsNull>>, And2<Where<ContractEx.contractID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.contractID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.contractID>, IsNull, Or<EPActivityApprove.contractID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.contractID>>>>>, And<EPActivityApprove.projectID, IsNotNull, And<PMTimeActivity.released, NotEqual<True>, And<EPActivityApprove.trackTime, Equal<True>, And<PMTimeActivity.origNoteID, IsNull, And2<Where<EPActivityApprove.ownerID, Equal<Current<EmployeeActivitiesRelease.EPActivityFilter.employeeID>>, Or<Current<EmployeeActivitiesRelease.EPActivityFilter.employeeID>, IsNull>>, And<Where<CRCaseClass.perItemBilling, Equal<BillingTypeListAttribute.perActivity>, Or<CRCaseClass.caseClassID, IsNull, Or<CRCaseClass.isBillable, Equal<False>>>>>>>>>>>>>>>>, Aggregate<Sum<EPActivityApprove.timeSpent, Sum<EPActivityApprove.overtimeSpent, Sum<EPActivityApprove.timeBillable, Sum<EPActivityApprove.overtimeBillable>>>>>> Totals;
  public PXAction<EmployeeActivitiesRelease.EPActivityFilter> viewCase;
  public PXAction<EmployeeActivitiesRelease.EPActivityFilter> viewContract;

  public EmployeeActivitiesRelease()
  {
    if (PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object[]) null) == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (EPSetup), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Time & Expenses Preferences")
      });
    PXUIFieldAttribute.SetVisible<EPActivityApprove.contractID>(((PXSelectBase) this.Activity).Cache, (object) null, !PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    PXUIFieldAttribute.SetVisible<ContractEx.contractCD>(((PXSelectBase) this.dummyContract).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    if (!PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
    {
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.timeSpent).Name, false);
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.timeBillable).Name, false);
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (PMTimeActivity.isBillable).Name, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.regularOvertime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.regularTime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.regularTotal>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.billableOvertime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.billableTime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.billableTotal>(((PXSelectBase) this.Filter).Cache, (object) null, false);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
    {
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.projectTaskID).Name, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.projectTaskID>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<PMTimeActivity.approvedDate>(((PXSelectBase) this.Activity).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EPActivityApprove.approverID>(((PXSelectBase) this.Activity).Cache, (object) null, false);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.contractManagement>())
    {
      PXUIFieldAttribute.SetVisible<PX.Objects.CT.Contract.contractCD>(((PXSelectBase) this.dummyContract).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesRelease.EPActivityFilter.contractID>(((PXSelectBase) this.Filter).Cache, (object) null, false);
    }
    // ISSUE: method pointer
    ((PXProcessingBase<EPActivityApprove>) this.Activity).SetProcessDelegate(new PXProcessingBase<EPActivityApprove>.ProcessListDelegate((object) null, __methodptr(ReleaseActivities)));
    ((PXProcessing<EPActivityApprove>) this.Activity).SetProcessCaption("Release");
    ((PXProcessing<EPActivityApprove>) this.Activity).SetProcessAllCaption("Release All");
    ((PXProcessingBase<EPActivityApprove>) this.Activity).SetSelected<PMTimeActivity.selected>();
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Case Status")]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.status> e)
  {
  }

  [PXUIField(DisplayName = "")]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    EPActivityApprove current = ((PXSelectBase<EPActivityApprove>) this.Activity).Current;
    if (current != null)
      PXRedirectHelper.TryRedirect((PXGraph) this, (object) current, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(Visible = false)]
  public virtual IEnumerable ViewCase(PXAdapter adapter)
  {
    EPActivityApprove current = ((PXSelectBase<EPActivityApprove>) this.Activity).Current;
    EPActivityApprove epActivityApprove = current;
    if (current != null && current.RefNoteID.HasValue)
    {
      CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) epActivityApprove.RefNoteID
      }));
      if (crCase != null)
        PXRedirectHelper.TryRedirect((PXGraph) this, (object) crCase, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(Visible = false)]
  public virtual IEnumerable ViewContract(PXAdapter adapter)
  {
    EPActivityApprove current = ((PXSelectBase<EPActivityApprove>) this.Activity).Current;
    if (current != null)
    {
      PX.Objects.CT.Contract contract;
      if (current.ContractID.HasValue)
        contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.ContractID
        }));
      else
        contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelectJoin<PX.Objects.CT.Contract, InnerJoin<CRCase, On<CRCase.contractID, Equal<PX.Objects.CT.Contract.contractID>>, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.RefNoteID
        }));
      if (contract != null)
        PXRedirectHelper.TryRedirect((PXGraph) this, (object) contract, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  protected virtual void EPActivityFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EPActivityApprove epActivityApprove = PXResultset<EPActivityApprove>.op_Implicit(((PXSelectBase<EPActivityApprove>) this.Totals).Select(Array.Empty<object>()));
    EmployeeActivitiesRelease.EPActivityFilter row = (EmployeeActivitiesRelease.EPActivityFilter) e.Row;
    if (epActivityApprove == null || row == null)
      return;
    EmployeeActivitiesRelease.EPActivityFilter epActivityFilter1 = row;
    int? timeBillable = epActivityApprove.TimeBillable;
    int? nullable1 = epActivityApprove.OvertimeBillable;
    int? nullable2 = timeBillable.HasValue & nullable1.HasValue ? new int?(timeBillable.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new int?();
    epActivityFilter1.BillableTime = nullable2;
    row.BillableOvertime = epActivityApprove.OvertimeBillable;
    row.BillableTotal = epActivityApprove.TimeBillable;
    EmployeeActivitiesRelease.EPActivityFilter epActivityFilter2 = row;
    nullable1 = epActivityApprove.TimeSpent;
    int? overtimeSpent = epActivityApprove.OvertimeSpent;
    int? nullable3 = nullable1.HasValue & overtimeSpent.HasValue ? new int?(nullable1.GetValueOrDefault() - overtimeSpent.GetValueOrDefault()) : new int?();
    epActivityFilter2.RegularTime = nullable3;
    row.RegularOvertime = epActivityApprove.OvertimeSpent;
    row.RegularTotal = epActivityApprove.TimeSpent;
  }

  protected virtual void EPActivityFilter_FromDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EmployeeActivitiesRelease.EPActivityFilter row = (EmployeeActivitiesRelease.EPActivityFilter) e.Row;
    if (row == null || !e.ExternalCall)
      return;
    DateTime? fromDate = row.FromDate;
    DateTime? tillDate = row.TillDate;
    if ((fromDate.HasValue & tillDate.HasValue ? (fromDate.GetValueOrDefault() > tillDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    row.TillDate = row.FromDate;
  }

  protected virtual void EPActivityFilter_TillDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EmployeeActivitiesRelease.EPActivityFilter row = (EmployeeActivitiesRelease.EPActivityFilter) e.Row;
    if (row == null || !e.ExternalCall)
      return;
    DateTime? fromDate = row.FromDate;
    if (!fromDate.HasValue)
      return;
    fromDate = row.FromDate;
    DateTime? tillDate = row.TillDate;
    if ((fromDate.HasValue & tillDate.HasValue ? (fromDate.GetValueOrDefault() > tillDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    row.FromDate = row.TillDate;
  }

  public void ReleaseAllActivities(List<EPActivityApprove> activities)
  {
    EmployeeActivitiesRelease.ReleaseActivities(activities);
  }

  protected static void ReleaseActivities(List<EPActivityApprove> activities)
  {
    RegisterEntry instance = (RegisterEntry) PXGraph.CreateInstance(typeof (RegisterEntry));
    bool activityAdded = false;
    bool flag1 = false;
    bool flag2 = false;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      List<EPActivityApprove> activities1 = EmployeeActivitiesRelease.RecordContractUsage(activities, "Contract-Usage");
      if (activities1.Count != activities.Count)
        flag1 = true;
      if (!EmployeeActivitiesRelease.RecordCostTrans(instance, activities1, out activityAdded) && !activityAdded)
        flag2 = true;
      if (!flag1)
      {
        if (!flag2)
          transactionScope.Complete();
      }
    }
    if (flag1)
      throw new PXException("The system failed to create contract-usage transactions.");
    EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
    if (activityAdded && epSetup != null && epSetup.AutomaticReleasePM.GetValueOrDefault())
      RegisterRelease.Release(((PXSelectBase<PMRegister>) instance.Document).Current);
    if (flag2)
      throw new PXException("The system failed to create cost transactions.");
  }

  public static List<EPActivityApprove> RecordContractUsage(
    List<EPActivityApprove> activities,
    string description)
  {
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.projectID>(EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9__20_0 ?? (EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9__20_0 = new PXFieldVerifying((object) EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRecordContractUsage\u003Eb__20_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.inventoryID>(EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9__20_1 ?? (EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9__20_1 = new PXFieldVerifying((object) EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRecordContractUsage\u003Eb__20_1))));
    ((PXSelectBase) instance.Document).Cache.Insert();
    ((PXSelectBase<PMRegister>) instance.Document).Current.Description = description;
    ((PXSelectBase<PMRegister>) instance.Document).Current.Released = new bool?(true);
    ((PXSelectBase<PMRegister>) instance.Document).Current.Status = "R";
    ((PXGraph) instance).Views.Caches.Add(typeof (EPActivityApprove));
    PXCache pxCache = (PXCache) GraphHelper.Caches<EPActivityApprove>((PXGraph) instance);
    List<EPActivityApprove> epActivityApproveList = new List<EPActivityApprove>();
    for (int index = 0; index < activities.Count; ++index)
    {
      EPActivityApprove activity = activities[index];
      try
      {
        PMActiveLaborItemAttribute.VerifyLaborItem<PMTimeActivity.labourItemID>(pxCache, (object) activity);
        PMTran pmTran = (PMTran) null;
        int? nullable = activity.ContractID;
        if (nullable.HasValue)
        {
          RegisterEntry graph = instance;
          int? contractId = activity.ContractID;
          EPActivityApprove timeActivity = activity;
          nullable = activity.TimeBillable;
          int valueOrDefault = nullable.GetValueOrDefault();
          pmTran = EmployeeActivitiesRelease.CreateContractUsage(graph, contractId, (PMTimeActivity) timeActivity, valueOrDefault);
          activity.Billed = new bool?(true);
          pxCache.Update((object) activity);
        }
        else if (activity.RefNoteID.HasValue)
        {
          if (PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) activity.RefNoteID
          }).Count == 1)
          {
            RegisterEntry registerEntry = instance;
            EPActivityApprove timeActivity = activity;
            nullable = activity.TimeBillable;
            int valueOrDefault = nullable.GetValueOrDefault();
            pmTran = registerEntry.CreateContractUsage((PMTimeActivity) timeActivity, valueOrDefault);
            activity.Billed = new bool?(true);
            pxCache.Update((object) activity);
          }
        }
        if (pmTran != null)
          UsageMaint.AddUsage(pxCache, pmTran.ProjectID, pmTran.InventoryID, pmTran.BillableQty.GetValueOrDefault(), pmTran.UOM);
        epActivityApproveList.Add(activity);
      }
      catch (Exception ex)
      {
        PXProcessing<EPActivityApprove>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
      }
    }
    if (((PXSelectBase) instance.Transactions).Cache.IsInsertedUpdatedDeleted)
      ((PXAction) instance.Save).Press();
    else
      pxCache.Persist((PXDBOperation) 1);
    return epActivityApproveList;
  }

  public static bool RecordCostTrans(
    RegisterEntry registerEntry,
    List<EPActivityApprove> activities,
    out bool activityAdded)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) registerEntry).FieldVerifying.AddHandler<PMTran.inventoryID>(EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9__21_0 ?? (EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9__21_0 = new PXFieldVerifying((object) EmployeeActivitiesRelease.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRecordCostTrans\u003Eb__21_0))));
    EmployeeCostEngine employeeCostEngine1 = new EmployeeCostEngine((PXGraph) registerEntry);
    ((PXGraph) registerEntry).Views.Caches.Add(typeof (EPActivityApprove));
    PXCache pxCache = (PXCache) GraphHelper.Caches<EPActivityApprove>((PXGraph) registerEntry);
    ((PXSelectBase) registerEntry.Document).Cache.Insert();
    bool flag = true;
    activityAdded = false;
    for (int index = 0; index < activities.Count; ++index)
    {
      EPActivityApprove activity = PXResultset<EPActivityApprove>.op_Implicit(PXSelectBase<EPActivityApprove, PXSelect<EPActivityApprove>.Config>.Search<EPActivityApprove.noteID>((PXGraph) registerEntry, (object) activities[index].NoteID, Array.Empty<object>()));
      if (!activity.Released.GetValueOrDefault())
      {
        try
        {
          EPSetup setup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select((PXGraph) registerEntry, Array.Empty<object>()));
          if (setup != null)
          {
            EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.defContactID>((PXGraph) registerEntry, (object) activity.OwnerID, Array.Empty<object>()));
            string postingOption = EPSetupMaint.GetPostingOption((PXGraph) registerEntry, setup, epEmployee.BAccountID);
            int? nullable = activity.ProjectTaskID;
            if (nullable.HasValue && postingOption != "N")
            {
              activity.LabourItemID = employeeCostEngine1.GetLaborClass((PMTimeActivity) activity);
              nullable = activity.LabourItemID;
              if (!nullable.HasValue)
                throw new PXException("Cannot find the labor item for the {0} employee.", new object[1]
                {
                  (object) epEmployee.AcctName
                });
              EmployeeCostEngine employeeCostEngine2 = employeeCostEngine1;
              string timeCardCd = activity.TimeCardCD;
              string earningTypeId = activity.EarningTypeID;
              int? labourItemId = activity.LabourItemID;
              int? projectId = activity.ProjectID;
              int? projectTaskId = activity.ProjectTaskID;
              bool? certifiedJob = activity.CertifiedJob;
              string unionId = activity.UnionID;
              int? baccountId1 = epEmployee.BAccountID;
              DateTime? date1 = activity.Date;
              DateTime date2 = date1.Value;
              int? shiftId = activity.ShiftID;
              EmployeeCostEngine.LaborCost employeeCost = employeeCostEngine2.CalculateEmployeeCost(timeCardCd, earningTypeId, labourItemId, projectId, projectTaskId, certifiedJob, unionId, baccountId1, date2, shiftId);
              if (employeeCost != null)
              {
                if (EPSetupMaint.GetPostPMTransaction((PXGraph) registerEntry, setup, epEmployee.BAccountID))
                {
                  RegisterEntry registerEntry1 = registerEntry;
                  EPActivityApprove timeActivity = activity;
                  int? baccountId2 = epEmployee.BAccountID;
                  date1 = activity.Date;
                  DateTime date3 = date1.Value;
                  int? timeSpent = activity.TimeSpent;
                  int? timeBillable = activity.TimeBillable;
                  Decimal? rate = employeeCost.Rate;
                  Decimal? overtimeMultiplier = employeeCost.OvertimeMultiplier;
                  string curyId = employeeCost.CuryID;
                  RegisterEntry.CreatePMTran createPMTran = new RegisterEntry.CreatePMTran((PMTimeActivity) timeActivity, baccountId2, date3, timeSpent, timeBillable, rate, overtimeMultiplier, curyId, true);
                  registerEntry1.CreateTransaction(createPMTran);
                }
                activity.EmployeeRate = employeeCost.Rate;
              }
              else if (postingOption != "A")
                throw new PXException("The Employee Labor Cost Rate has not been found.");
              activityAdded = true;
            }
          }
          activity.Released = new bool?(true);
          activity.ApprovalStatus = "RL";
          if (activity.RefNoteID.HasValue)
            PXUpdate<Set<CRActivity.isLocked, True>, CRActivity, Where<CRActivity.noteID, Equal<Required<CRActivity.noteID>>>>.Update((PXGraph) registerEntry, new object[1]
            {
              (object) activity.RefNoteID
            });
          pxCache.Update((object) activity);
        }
        catch (Exception ex)
        {
          PXProcessing<EPActivityApprove>.SetError(index, ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message);
          flag = false;
        }
      }
    }
    if (activityAdded)
    {
      ((PXAction) registerEntry.Save).Press();
    }
    else
    {
      pxCache.Persist((PXDBOperation) 1);
      ((PXGraph) registerEntry).SelectTimeStamp();
    }
    return flag;
  }

  /// NO CRM Mode
  public static PMTran CreateContractUsage(
    RegisterEntry graph,
    int? contractID,
    PMTimeActivity timeActivity,
    int billableMinutes)
  {
    if (timeActivity.ApprovalStatus == "CL")
      return (PMTran) null;
    if (!timeActivity.IsBillable.GetValueOrDefault())
      return (PMTran) null;
    PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) contractID
    }));
    if (contract == null)
      return (PMTran) null;
    graph.ValidateContractBaseCurrency(contract);
    int? nullable = EmployeeActivitiesRelease.GetContractLaborClassID((PXGraph) graph, contractID, timeActivity);
    if (!nullable.HasValue)
    {
      EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.defContactID, Equal<Required<PMTimeActivity.ownerID>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) timeActivity.OwnerID
      }));
      if (epEmployee != null)
        nullable = EPEmployeeClassLaborMatrix.GetLaborClassID((PXGraph) graph, epEmployee.BAccountID, timeActivity.EarningTypeID) ?? epEmployee.LabourItemID;
    }
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) nullable
    }));
    if (inventoryItem == null)
      throw new PXException("Labor Item cannot be found");
    int num = billableMinutes < 0 ? -1 : 1;
    billableMinutes = Math.Abs(billableMinutes);
    if (billableMinutes <= 0)
      return (PMTran) null;
    PMTran pmTran = new PMTran()
    {
      ProjectID = contractID,
      InventoryID = inventoryItem.InventoryID,
      AccountGroupID = contract.ContractAccountGroup,
      OrigRefID = timeActivity.NoteID,
      BAccountID = contract.CustomerID,
      LocationID = contract.LocationID,
      Description = timeActivity.Summary,
      StartDate = timeActivity.Date,
      EndDate = timeActivity.Date,
      Date = timeActivity.Date,
      UOM = inventoryItem.SalesUnit,
      Qty = new Decimal?((Decimal) num * Convert.ToDecimal(TimeSpan.FromMinutes((double) billableMinutes).TotalHours))
    };
    pmTran.BillableQty = pmTran.Qty;
    pmTran.Released = new bool?(true);
    pmTran.Allocated = new bool?(true);
    pmTran.IsQtyOnly = new bool?(true);
    pmTran.BillingID = contract.BillingID;
    return ((PXSelectBase<PMTran>) graph.Transactions).Insert(pmTran);
  }

  /// NO CRM Mode
  public static int? GetContractLaborClassID(
    PXGraph graph,
    int? contractID,
    PMTimeActivity activity)
  {
    EPContractRate epContractRate = PXResultset<EPContractRate>.op_Implicit(PXSelectBase<EPContractRate, PXSelectJoin<EPContractRate, InnerJoin<EPEmployee, On<EPContractRate.employeeID, Equal<EPEmployee.bAccountID>>>, Where<EPContractRate.contractID, Equal<Required<EPContractRate.contractID>>, And<EPContractRate.earningType, Equal<Required<CRPMTimeActivity.earningTypeID>>, And<EPEmployee.defContactID, Equal<Required<CRPMTimeActivity.ownerID>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) contractID,
      (object) activity.EarningTypeID,
      (object) activity.OwnerID
    }));
    if (epContractRate == null)
      epContractRate = PXResultset<EPContractRate>.op_Implicit(PXSelectBase<EPContractRate, PXSelect<EPContractRate, Where<EPContractRate.contractID, Equal<Required<EPContractRate.contractID>>, And<EPContractRate.earningType, Equal<Required<CRPMTimeActivity.earningTypeID>>, And<EPContractRate.employeeID, IsNull>>>>.Config>.Select(graph, new object[2]
      {
        (object) contractID,
        (object) activity.EarningTypeID
      }));
    return epContractRate?.LabourItemID;
  }

  [PXHidden]
  [Serializable]
  public class EPActivityFilter : OwnedFilter
  {
    [SubordinateAndWingmenOwnerEmployee(DisplayName = "Employee")]
    public virtual int? EmployeeID { set; get; }

    [PXDBDateAndTime(DisplayMask = "d", PreserveTime = true, UseTimeZone = true)]
    [PXUIField]
    public virtual DateTime? FromDate { set; get; }

    [PXDBDateAndTime(DisplayMask = "d", PreserveTime = true, UseTimeZone = true)]
    [BusinessDateTimeDefault]
    [PXUIField]
    public virtual DateTime? TillDate { set; get; }

    [PXDBDateAndTime(DisplayMask = "d", PreserveTime = true, UseTimeZone = true)]
    public virtual DateTime? TillDatePlusOne
    {
      get => new DateTime?((this.TillDate ?? DateTime.Now).AddDays(1.0));
    }

    [EPProject(typeof (OwnedFilter.ownerID), DisplayName = "Project")]
    public virtual int? ProjectID { set; get; }

    [ProjectTask(typeof (EmployeeActivitiesRelease.EPActivityFilter.projectID))]
    public virtual int? ProjectTaskID { set; get; }

    [PXDBInt]
    [PXUIField(DisplayName = "Contract")]
    [PXDimensionSelector("CONTRACT", typeof (Search2<ContractExEx.contractID, LeftJoin<ContractBillingSchedule, On<ContractExEx.contractID, Equal<ContractBillingSchedule.contractID>>>, Where<ContractExEx.baseType, Equal<CTPRType.contract>>, OrderBy<Desc<ContractExEx.contractCD>>>), typeof (ContractExEx.contractCD), DescriptionField = typeof (ContractExEx.description), Filterable = true)]
    public virtual int? ContractID { set; get; }

    [PXInt]
    [PXTimeList]
    [PXUIField]
    public virtual int? RegularTime { set; get; }

    [PXInt]
    [PXTimeList]
    [PXUIField]
    public virtual int? RegularOvertime { set; get; }

    [PXInt]
    [PXTimeList]
    [PXUIField]
    public virtual int? RegularTotal { set; get; }

    [PXInt]
    [PXTimeList]
    [PXUIField]
    public virtual int? BillableTime { set; get; }

    [PXInt]
    [PXTimeList]
    [PXUIField]
    public virtual int? BillableOvertime { set; get; }

    [PXInt]
    [PXTimeList]
    [PXUIField]
    public virtual int? BillableTotal { set; get; }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.employeeID>
    {
    }

    public abstract class fromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.fromDate>
    {
    }

    public abstract class tillDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.tillDate>
    {
    }

    public abstract class tillDatePlusOne : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.tillDatePlusOne>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.projectID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.projectTaskID>
    {
    }

    public abstract class contractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.contractID>
    {
    }

    public abstract class regularTime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.regularTime>
    {
    }

    public abstract class regularOvertime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.regularOvertime>
    {
    }

    public abstract class regularTotal : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.regularTotal>
    {
    }

    public abstract class billableTime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.billableTime>
    {
    }

    public abstract class billableOvertime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.billableOvertime>
    {
    }

    public abstract class billableTotal : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesRelease.EPActivityFilter.billableTotal>
    {
    }
  }
}
