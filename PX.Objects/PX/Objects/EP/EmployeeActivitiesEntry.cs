// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeActivitiesEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web;

#nullable enable
namespace PX.Objects.EP;

public class EmployeeActivitiesEntry : PXGraph<
#nullable disable
EmployeeActivitiesEntry>
{
  [PXHidden]
  public PXSelect<ContractEx> dummyContract;
  [PXHidden]
  public PXSelect<CRActivity> dummy;
  public PXFilter<EmployeeActivitiesEntry.PMTimeActivityFilter> Filter;
  [PXImport(typeof (EmployeeActivitiesEntry.PMTimeActivityFilter))]
  public PXSelectJoin<EPActivityApprove, LeftJoin<CRActivityLink, On<CRActivityLink.noteID, Equal<PMTimeActivity.refNoteID>>, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<PMTimeActivity.earningTypeID>>, LeftJoin<CRCase, On<CRCase.noteID, Equal<CRActivityLink.refNoteID>>, LeftJoin<ContractEx, On<CRCase.contractID, Equal<ContractEx.contractID>>>>>>, Where<EPActivityApprove.ownerID, Equal<Current<EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID>>, And<EPActivityApprove.trackTime, Equal<True>>>, OrderBy<Asc<EPActivityApprove.date>>> Activity;
  public PXSetupOptional<EPSetup> Setup;
  [PXHidden]
  public PXFilter<CRActivityMaint.EPTempData> TempData;
  public PXSelect<CRActivity, Where<CRActivity.noteID, Equal<Current<PMTimeActivity.refNoteID>>, And<CRActivity.isLocked, Equal<False>>>> Parent;
  public PXSave<EmployeeActivitiesEntry.PMTimeActivityFilter> Save;
  public PXCancel<EmployeeActivitiesEntry.PMTimeActivityFilter> Cancel;
  public PXAction<EmployeeActivitiesEntry.PMTimeActivityFilter> View;
  public PXAction<EmployeeActivitiesEntry.PMTimeActivityFilter> viewCase;
  public PXAction<EmployeeActivitiesEntry.PMTimeActivityFilter> viewContract;
  protected EmployeeCostEngine costEngine;

  public EmployeeActivitiesEntry()
  {
    PXUIFieldAttribute.SetVisible<EPActivityApprove.contractID>(((PXSelectBase) this.Activity).Cache, (object) null, !PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    PXUIFieldAttribute.SetVisible<ContractEx.contractCD>(((PXSelectBase) this.dummyContract).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    if (!PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
    {
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.timeSpent).Name, false);
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.timeBillable).Name, false);
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (PMTimeActivity.isBillable).Name, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.regularOvertime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.regularTime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.regularTotal>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.billableOvertime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.billableTime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.billableTotal>(((PXSelectBase) this.Filter).Cache, (object) null, false);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.contractManagement>())
      PXUIFieldAttribute.SetVisible<PX.Objects.CT.Contract.contractCD>(((PXGraph) this).Caches[typeof (PX.Objects.CT.Contract)], (object) null, false);
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
    {
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.projectTaskID).Name, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.projectTaskID>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesEntry.PMTimeActivityFilter.includeReject>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EPActivityApprove.approvalStatus>(((PXSelectBase) this.Activity).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EPActivityApprove.approverID>(((PXSelectBase) this.Activity).Cache, (object) null, false);
    }
    EPActivityType epActivityType = EPActivityType.PK.Find((PXGraph) this, ((PXSelectBase<EPSetup>) this.Setup).Current.DefaultActivityType);
    if (epActivityType == null || !epActivityType.RequireTimeByDefault.GetValueOrDefault())
      throw new PXSetupNotEnteredException("Required configuration data is not entered. Default Time Activity on the Time & Expense Preference screen must be set to \"Track Time\" Activity. Please check the settings on the {0}", typeof (EPActivityType), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Activity Type")
      });
    PXGraph.FieldUpdatingEvents fieldUpdating = ((PXGraph) this).FieldUpdating;
    System.Type type = typeof (EPActivityApprove);
    EmployeeActivitiesEntry employeeActivitiesEntry = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) employeeActivitiesEntry, __vmethodptr(employeeActivitiesEntry, StartDateFieldUpdating));
    fieldUpdating.AddHandler(type, "Date_Date", pxFieldUpdating);
    if (PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null || PXGraph.ProxyIsActive)
      return;
    string screenId = PXContext.GetScreenID();
    if (this.ScreenIDsEqual(screenId, PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (SYImportMaint))?.ScreenID) || this.ScreenIDsEqual(screenId, PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (SYExportMaint))?.ScreenID))
      return;
    if (((PXGraph) this).IsExport || ((PXGraph) this).IsImport)
      throw new PXException("User must be an Employee to use current screen.");
    Redirector.Redirect(HttpContext.Current, $"~/Frames/Error.aspx?exceptionID={"User must be an Employee to use current screen."}&typeID={"error"}", false);
  }

  [PXUIField(DisplayName = "View")]
  [PXButton]
  public virtual IEnumerable view(PXAdapter adapter)
  {
    EPActivityApprove current = ((PXSelectBase<EPActivityApprove>) this.Activity).Current;
    if (current != null)
      PXRedirectHelper.TryRedirect((PXGraph) this, (object) current, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField(Visible = false)]
  [PXButton]
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

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable ViewContract(PXAdapter adapter)
  {
    EPActivityApprove current = ((PXSelectBase<EPActivityApprove>) this.Activity).Current;
    if (current != null)
    {
      PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelectJoin<PX.Objects.CT.Contract, InnerJoin<CRCase, On<CRCase.contractID, Equal<PX.Objects.CT.Contract.contractID>>, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.RefNoteID
      }));
      if (contract != null)
        PXRedirectHelper.TryRedirect((PXGraph) this, (object) contract, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  public EmployeeCostEngine CostEngine
  {
    get
    {
      if (this.costEngine == null)
        this.costEngine = this.CreateEmployeeCostEngine();
      return this.costEngine;
    }
  }

  public virtual EmployeeCostEngine CreateEmployeeCostEngine()
  {
    return new EmployeeCostEngine((PXGraph) this);
  }

  protected virtual IEnumerable activity()
  {
    List<object> objectList = new List<object>();
    EmployeeActivitiesEntry.PMTimeActivityFilter current = ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current;
    if (current == null)
      return (IEnumerable) null;
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new System.Type[1]
    {
      typeof (Select2<EPActivityApprove, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<PMTimeActivity.earningTypeID>>, LeftJoin<CRActivityLink, On<CRActivityLink.noteID, Equal<PMTimeActivity.refNoteID>>, LeftJoin<CRCase, On<CRCase.noteID, Equal<CRActivityLink.refNoteID>>, LeftJoin<ContractEx, On<CRCase.contractID, Equal<ContractEx.contractID>>>>>>, Where<EPActivityApprove.ownerID, Equal<Current<EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID>>, And<EPActivityApprove.trackTime, Equal<True>, And<PMTimeActivity.isCorrected, Equal<False>>>>, OrderBy<Desc<EPActivityApprove.date>>>)
    });
    int? nullable = current.ProjectID;
    if (nullable.HasValue)
      bqlCommand = bqlCommand.WhereAnd<Where<EPActivityApprove.projectID, Equal<Current<EmployeeActivitiesEntry.PMTimeActivityFilter.projectID>>>>();
    nullable = current.ProjectTaskID;
    if (nullable.HasValue)
      bqlCommand = bqlCommand.WhereAnd<Where<EPActivityApprove.projectTaskID, Equal<Current<EmployeeActivitiesEntry.PMTimeActivityFilter.projectTaskID>>>>();
    nullable = current.FromWeek;
    if (!nullable.HasValue)
    {
      nullable = current.TillWeek;
      if (!nullable.HasValue)
        goto label_19;
    }
    List<System.Type> typeList = new List<System.Type>();
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    if (current.IncludeReject.GetValueOrDefault())
    {
      typeList.Add(typeof (Where<,,>));
      typeList.Add(typeof (EPActivityApprove.approvalStatus));
      typeList.Add(typeof (Equal<ActivityStatusListAttribute.rejected>));
      typeList.Add(typeof (Or<>));
    }
    nullable = current.FromWeek;
    if (nullable.HasValue)
    {
      nullable = current.FromWeek;
      DateTime weekStartDate = PXWeekSelector2Attribute.GetWeekStartDate((PXGraph) this, nullable.Value);
      nullable = current.TillWeek;
      if (nullable.HasValue)
        typeList.Add(typeof (Where<,,>));
      else
        typeList.Add(typeof (Where<,>));
      typeList.Add(typeof (EPActivityApprove.date));
      typeList.Add(typeof (GreaterEqual<P.AsDateTime>));
      objectList.Add((object) PXTimeZoneInfo.ConvertTimeToUtc(weekStartDate, timeZone));
      nullable = current.TillWeek;
      if (nullable.HasValue)
        typeList.Add(typeof (And<>));
    }
    nullable = current.TillWeek;
    if (nullable.HasValue)
    {
      nullable = current.TillWeek;
      DateTime weekEndDateTime = PXWeekSelector2Attribute.GetWeekEndDateTime((PXGraph) this, nullable.Value);
      typeList.Add(typeof (Where<EPActivityApprove.date, LessEqual<P.AsDateTime>>));
      objectList.Add((object) PXTimeZoneInfo.ConvertTimeToUtc(weekEndDateTime, timeZone));
    }
    bqlCommand = bqlCommand.WhereAnd(BqlCommand.Compose(typeList.ToArray()));
label_19:
    if (current.NoteID.HasValue)
    {
      bqlCommand = bqlCommand.WhereAnd<Where<EPActivityApprove.noteID, Equal<Required<EmployeeActivitiesEntry.PMTimeActivityFilter.noteID>>>>();
      objectList.Add((object) current.NoteID);
    }
    return (IEnumerable) new PXView((PXGraph) this, false, bqlCommand).SelectMultiBound(new object[1]
    {
      (object) ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current
    }, objectList.ToArray());
  }

  [PXUIEnabled(typeof (PMTimeActivity.isBillable))]
  [PXMergeAttributes]
  protected virtual void EPActivityApprove_TimeBillable_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<PMProject.contractID, Where<PMProject.nonProject, Equal<True>>>))]
  [EPProject(typeof (EPActivityApprove.ownerID), FieldClass = "PROJECT", BqlField = typeof (PMTimeActivity.projectID))]
  [PXMergeAttributes]
  protected virtual void EPActivityApprove_ProjectID_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (Coalesce<Search<PMProject.certifiedJob, Where<PMProject.contractID, Equal<Current<EmployeeActivitiesEntry.PMTimeActivityFilter.projectID>>>>, Search<PMProject.certifiedJob, Where<PMProject.contractID, Equal<Current<PMTimeActivity.projectID>>>>, Search<PMProject.certifiedJob, Where<PMProject.nonProject, Equal<True>>>>))]
  [PXUIField(DisplayName = "Certified Job", FieldClass = "Construction")]
  [PXMergeAttributes]
  protected virtual void EPActivityApprove_CertifiedJob_CacheAttached(PXCache sender)
  {
  }

  protected virtual void PMTimeActivityFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXDBDateAndTimeAttribute.SetTimeVisible<EPActivityApprove.date>(((PXSelectBase) this.Activity).Cache, (object) null, ((PXSelectBase<EPSetup>) this.Setup).Current.RequireTimes.GetValueOrDefault());
    EmployeeActivitiesEntry.PMTimeActivityFilter row = (EmployeeActivitiesEntry.PMTimeActivityFilter) e.Row;
    if (row == null)
      return;
    row.BillableTime = new int?(0);
    row.BillableOvertime = new int?(0);
    row.BillableTotal = new int?(0);
    row.RegularTime = new int?(0);
    row.RegularOvertime = new int?(0);
    row.RegularTotal = new int?(0);
    foreach (PXResult<EPActivityApprove, EPEarningType> pxResult in ((PXSelectBase<EPActivityApprove>) this.Activity).Select(Array.Empty<object>()))
    {
      EPActivityApprove epActivityApprove = PXResult<EPActivityApprove, EPEarningType>.op_Implicit(pxResult);
      int? nullable1;
      int? nullable2;
      if (PXResult<EPActivityApprove, EPEarningType>.op_Implicit(pxResult).IsOvertime.GetValueOrDefault())
      {
        EmployeeActivitiesEntry.PMTimeActivityFilter timeActivityFilter1 = row;
        nullable1 = timeActivityFilter1.RegularOvertime;
        nullable2 = epActivityApprove.TimeSpent;
        int num1 = nullable2 ?? 0;
        int? nullable3;
        if (!nullable1.HasValue)
        {
          nullable2 = new int?();
          nullable3 = nullable2;
        }
        else
          nullable3 = new int?(nullable1.GetValueOrDefault() + num1);
        timeActivityFilter1.RegularOvertime = nullable3;
        EmployeeActivitiesEntry.PMTimeActivityFilter timeActivityFilter2 = row;
        nullable1 = timeActivityFilter2.BillableOvertime;
        nullable2 = epActivityApprove.TimeBillable;
        int num2 = nullable2 ?? 0;
        int? nullable4;
        if (!nullable1.HasValue)
        {
          nullable2 = new int?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new int?(nullable1.GetValueOrDefault() + num2);
        timeActivityFilter2.BillableOvertime = nullable4;
      }
      else
      {
        EmployeeActivitiesEntry.PMTimeActivityFilter timeActivityFilter3 = row;
        nullable1 = timeActivityFilter3.RegularTime;
        nullable2 = epActivityApprove.TimeSpent;
        int num3 = nullable2 ?? 0;
        int? nullable5;
        if (!nullable1.HasValue)
        {
          nullable2 = new int?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new int?(nullable1.GetValueOrDefault() + num3);
        timeActivityFilter3.RegularTime = nullable5;
        EmployeeActivitiesEntry.PMTimeActivityFilter timeActivityFilter4 = row;
        nullable1 = timeActivityFilter4.BillableTime;
        nullable2 = epActivityApprove.TimeBillable;
        int num4 = nullable2 ?? 0;
        int? nullable6;
        if (!nullable1.HasValue)
        {
          nullable2 = new int?();
          nullable6 = nullable2;
        }
        else
          nullable6 = new int?(nullable1.GetValueOrDefault() + num4);
        timeActivityFilter4.BillableTime = nullable6;
      }
      EmployeeActivitiesEntry.PMTimeActivityFilter timeActivityFilter5 = row;
      nullable1 = row.BillableTime;
      nullable2 = row.BillableOvertime;
      int? nullable7 = nullable1.HasValue & nullable2.HasValue ? new int?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new int?();
      timeActivityFilter5.BillableTotal = nullable7;
      EmployeeActivitiesEntry.PMTimeActivityFilter timeActivityFilter6 = row;
      nullable2 = row.RegularTime;
      nullable1 = row.RegularOvertime;
      int? nullable8 = nullable2.HasValue & nullable1.HasValue ? new int?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new int?();
      timeActivityFilter6.RegularTotal = nullable8;
    }
  }

  protected virtual void PMTimeActivityFilter_FromWeek_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    try
    {
      e.NewValue = (object) PXWeekSelector2Attribute.GetWeekID((PXGraph) this, ((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now);
    }
    catch (PXException ex)
    {
      sender.RaiseExceptionHandling<EmployeeActivitiesEntry.PMTimeActivityFilter.fromWeek>(e.Row, (object) null, (Exception) ex);
    }
  }

  protected virtual void PMTimeActivityFilter_TillWeek_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    try
    {
      e.NewValue = (object) PXWeekSelector2Attribute.GetWeekID((PXGraph) this, ((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now);
    }
    catch (PXException ex)
    {
      sender.RaiseExceptionHandling<EmployeeActivitiesEntry.PMTimeActivityFilter.fromWeek>(e.Row, (object) null, (Exception) ex);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EmployeeActivitiesEntry.PMTimeActivityFilter, EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID> e)
  {
    EmployeeActivitiesEntry.PMTimeActivityFilter row = e.Row;
    if (row == null || !row.OwnerID.HasValue)
      return;
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount>.Config>.Search<PX.Objects.CR.BAccount.defContactID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EmployeeActivitiesEntry.PMTimeActivityFilter, EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID>>) e).Cache.Graph, e.NewValue, Array.Empty<object>()));
    PXUIFieldAttribute.SetWarning<EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EmployeeActivitiesEntry.PMTimeActivityFilter, EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID>>) e).Cache, (object) row, baccount.VStatus == "I" ? "This employee is inactive." : (string) null);
  }

  protected virtual void PMTimeActivityFilter_FromWeek_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EmployeeActivitiesEntry.PMTimeActivityFilter row = (EmployeeActivitiesEntry.PMTimeActivityFilter) e.Row;
    if (row == null || !e.ExternalCall)
      return;
    int? fromWeek = row.FromWeek;
    int? tillWeek = row.TillWeek;
    if (!(fromWeek.GetValueOrDefault() > tillWeek.GetValueOrDefault() & fromWeek.HasValue & tillWeek.HasValue))
      return;
    row.TillWeek = row.FromWeek;
  }

  protected virtual void PMTimeActivityFilter_TillWeek_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EmployeeActivitiesEntry.PMTimeActivityFilter row = (EmployeeActivitiesEntry.PMTimeActivityFilter) e.Row;
    if (row == null || !e.ExternalCall)
      return;
    int? fromWeek = row.FromWeek;
    int? tillWeek = row.TillWeek;
    if (!(fromWeek.GetValueOrDefault() > tillWeek.GetValueOrDefault() & fromWeek.HasValue & tillWeek.HasValue))
      return;
    row.FromWeek = row.TillWeek;
  }

  protected virtual void PMTimeActivityFilter_ProjectID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((EmployeeActivitiesEntry.PMTimeActivityFilter) e.Row == null)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue, Array.Empty<object>()));
    if (pmProject == null)
      return;
    bool? nullable = pmProject.IsCompleted;
    nullable = !nullable.GetValueOrDefault() ? pmProject.IsCancelled : throw new PXSetPropertyException("Project is Completed and cannot be used for data entry.")
    {
      ErrorValue = (object) pmProject.ContractCD
    };
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("Project is Canceled and cannot be used for data entry.")
      {
        ErrorValue = (object) pmProject.ContractCD
      };
    if (pmProject.Status == "E")
      throw new PXSetPropertyException("Project is Suspended and cannot be used for data entry.")
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }

  protected virtual void PMTimeActivityFilter_ProjectTaskID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    EmployeeActivitiesEntry.PMTimeActivityFilter row = (EmployeeActivitiesEntry.PMTimeActivityFilter) e.Row;
    if (row == null || e.NewValue == null || !(e.NewValue is int))
      return;
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask>.Config>.Search<PMTask.projectID, PMTask.taskID>(sender.Graph, (object) row.ProjectID, e.NewValue, Array.Empty<object>()));
    if (pmTask == null)
      return;
    bool? nullable = pmTask.IsCompleted;
    nullable = !nullable.GetValueOrDefault() ? pmTask.IsCancelled : throw new PXSetPropertyException("Task is Completed and cannot be used for data entry.")
    {
      ErrorValue = (object) pmTask.TaskCD
    };
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("Task is Canceled and cannot be used for data entry.")
      {
        ErrorValue = (object) pmTask.TaskCD
      };
  }

  protected virtual void PMTimeActivityFilter_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (((PXSelectBase) this.Activity).Cache.IsDirty && ((PXSelectBase) this.Filter).View.Ask("Any unsaved changes will be discarded.", (MessageButtons) 4) != 6)
      ((CancelEventArgs) e).Cancel = true;
    else
      ((PXSelectBase) this.Activity).Cache.Clear();
  }

  protected virtual void StartDateFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    EmployeeActivitiesEntry.PMTimeActivityFilter current = ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current;
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (current == null || e.NewValue == null)
      return;
    DateTime? nullable1 = new DateTime?();
    DateTime result;
    if (e.NewValue is string && DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      nullable1 = new DateTime?(result);
    if (e.NewValue is DateTime)
      nullable1 = new DateTime?((DateTime) e.NewValue);
    if (!nullable1.HasValue)
      return;
    int weekId = PXWeekSelector2Attribute.GetWeekID((PXGraph) this, nullable1.Value.Date);
    int num1 = weekId;
    int? nullable2 = current.FromWeek;
    int valueOrDefault1 = nullable2.GetValueOrDefault();
    if (!(num1 < valueOrDefault1 & nullable2.HasValue))
    {
      int num2 = weekId;
      nullable2 = current.TillWeek;
      int valueOrDefault2 = nullable2.GetValueOrDefault();
      if (!(num2 > valueOrDefault2 & nullable2.HasValue))
        return;
    }
    sender.RaiseExceptionHandling<EPActivityApprove.date>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Date is out of range. It can only be within \"From Date\" and \"Till Date\"."));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPActivityApprove_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    EmployeeActivitiesEntry.PMTimeActivityFilter current = ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current;
    if (row == null || current == null)
      return;
    if (row.Released.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      PXDBDateAndTimeAttribute.SetTimeEnabled<EPActivityApprove.date>(sender, (object) row, false);
      PXDBDateAndTimeAttribute.SetDateEnabled<EPActivityApprove.date>(sender, (object) row, false);
    }
    else if (row.ApprovalStatus == "OP")
    {
      PXUIFieldAttribute.SetEnabled(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<EPActivityApprove.timeBillable>(sender, (object) row);
      PMProject pmProject = (PMProject) PXSelectorAttribute.Select<EPActivityApprove.projectID>(sender, (object) row);
      PXUIFieldAttribute.SetEnabled<EPActivityApprove.projectID>(sender, (object) row, !current.ProjectID.HasValue);
      if (pmProject != null)
        PXUIFieldAttribute.SetEnabled<EPActivityApprove.projectTaskID>(sender, (object) row, !current.ProjectTaskID.HasValue && pmProject.BaseType == "P");
      else
        PXUIFieldAttribute.SetEnabled<EPActivityApprove.projectTaskID>(sender, (object) row, !current.ProjectTaskID.HasValue);
      PXDBDateAndTimeAttribute.SetTimeEnabled<EPActivityApprove.date>(sender, (object) row, true);
      PXDBDateAndTimeAttribute.SetDateEnabled<EPActivityApprove.date>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<EPActivityApprove.approvalStatus>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<EPActivityApprove.approverID>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<EPActivityApprove.timeCardCD>(sender, (object) row, false);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      PXDBDateAndTimeAttribute.SetTimeEnabled<EPActivityApprove.date>(sender, (object) row, ((PXGraph) this).IsImport);
      PXDBDateAndTimeAttribute.SetDateEnabled<EPActivityApprove.date>(sender, (object) row, ((PXGraph) this).IsImport);
      PXUIFieldAttribute.SetEnabled<EPActivityApprove.hold>(sender, (object) row, true);
    }
    PXUIFieldAttribute.SetEnabled<EPActivityApprove.approvalStatus>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.employeeRate>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.reportedInTimeZoneID>(sender, (object) row, false);
    if (!(row.ApprovalStatus == "RJ"))
      return;
    sender.RaiseExceptionHandling<EPActivityApprove.hold>((object) row, (object) null, (Exception) new PXSetPropertyException("Rejected", (PXErrorLevel) 3));
  }

  protected virtual void EPActivityApprove_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    if (row.Released.GetValueOrDefault())
    {
      ((PXSelectBase) this.Filter).View.Ask("The time activity cannot be deleted because it has been released.", (MessageButtons) 0);
      ((CancelEventArgs) e).Cancel = true;
    }
    else if (row.ApprovalStatus == "AP")
    {
      ((PXSelectBase) this.Filter).View.Ask(PXMessages.LocalizeFormatNoPrefix("Activity in status \"{0}\" cannot be deleted.", new object[1]
      {
        sender.GetValueExt<EPActivityApprove.approvalStatus>((object) row)
      }), (MessageButtons) 0);
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (row.TimeCardCD == null)
        return;
      ((PXSelectBase) this.Filter).View.Ask("This Activity assigned to the Time Card. You may do changes only in a Time Card screen.", (MessageButtons) 0);
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EPActivityApprove_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if ((EPActivityApprove) e.Row == null || !((PXGraph) this).IsMobile)
      return;
    ((PXGraph) this).Persist();
  }

  protected virtual void EPActivityApprove_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    if (!row.Hold.GetValueOrDefault())
      PMActiveLaborItemAttribute.VerifyLaborItem<PMTimeActivity.labourItemID>(sender, e.NewRow);
    if (row.TimeCardCD == null)
      return;
    ((PXSelectBase) this.Filter).View.Ask("This Activity assigned to the Time Card. You may do changes only in a Time Card screen.", (MessageButtons) 0);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPActivityApprove> e)
  {
    DateTime? date = e.Row.Date;
    DateTime valueOrDefault1 = date.GetValueOrDefault();
    date = e.OldRow.Date;
    DateTime valueOrDefault2 = date.GetValueOrDefault();
    if (!(valueOrDefault1 != valueOrDefault2) && !(e.Row.EarningTypeID != e.OldRow.EarningTypeID))
    {
      int? nullable1 = e.Row.ProjectID;
      int valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = e.OldRow.ProjectID;
      int valueOrDefault4 = nullable1.GetValueOrDefault();
      if (valueOrDefault3 == valueOrDefault4)
      {
        nullable1 = e.Row.ProjectTaskID;
        int valueOrDefault5 = nullable1.GetValueOrDefault();
        nullable1 = e.OldRow.ProjectTaskID;
        int valueOrDefault6 = nullable1.GetValueOrDefault();
        if (valueOrDefault5 == valueOrDefault6)
        {
          nullable1 = e.Row.CostCodeID;
          int? nullable2 = e.OldRow.CostCodeID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && !(e.Row.UnionID != e.OldRow.UnionID))
          {
            nullable2 = e.Row.LabourItemID;
            nullable1 = e.OldRow.LabourItemID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              bool? certifiedJob = e.Row.CertifiedJob;
              int num1 = certifiedJob.GetValueOrDefault() ? 1 : 0;
              certifiedJob = e.OldRow.CertifiedJob;
              int num2 = certifiedJob.GetValueOrDefault() ? 1 : 0;
              if (num1 == num2)
              {
                nullable1 = e.Row.OwnerID;
                int valueOrDefault7 = nullable1.GetValueOrDefault();
                nullable1 = e.OldRow.OwnerID;
                int valueOrDefault8 = nullable1.GetValueOrDefault();
                if (valueOrDefault7 == valueOrDefault8)
                {
                  nullable1 = e.Row.ShiftID;
                  int valueOrDefault9 = nullable1.GetValueOrDefault();
                  nullable1 = e.OldRow.ShiftID;
                  int valueOrDefault10 = nullable1.GetValueOrDefault();
                  if (valueOrDefault9 == valueOrDefault10)
                    goto label_10;
                }
              }
            }
          }
        }
      }
    }
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.defContactID, Equal<Required<EPActivityApprove.ownerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.OwnerID
    }));
    if (epEmployee != null)
    {
      EmployeeCostEngine.LaborCost employeeCost = this.CostEngine.CalculateEmployeeCost((string) null, e.Row.EarningTypeID, e.Row.LabourItemID, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.CertifiedJob, e.Row.UnionID, epEmployee.BAccountID, e.Row.Date.Value, e.Row.ShiftID);
      e.Row.EmployeeRate = (Decimal?) employeeCost?.Rate;
    }
label_10:
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<EPActivityApprove>>) e).Cache, (PMTimeActivity) e.Row, e.OldRow.Date, e.Row.Date);
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPActivityApprove> e)
  {
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<EPActivityApprove>>) e).Cache, (PMTimeActivity) e.Row, new DateTime?(), e.Row.Date);
    ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<EPActivityApprove>>) e).Cache.SetDefaultExt<PMTimeActivity.costCodeID>((object) e.Row);
  }

  protected virtual void EPActivityApprove_ProjectID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((EPActivityApprove) e.Row == null || e.NewValue == null || !(e.NewValue is int))
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue, Array.Empty<object>()));
    if (pmProject == null)
      return;
    bool? nullable = pmProject.IsCompleted;
    nullable = !nullable.GetValueOrDefault() ? pmProject.IsCancelled : throw new PXSetPropertyException("Project is Completed and cannot be used for data entry.")
    {
      ErrorValue = (object) pmProject.ContractCD
    };
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("Project is Canceled and cannot be used for data entry.")
      {
        ErrorValue = (object) pmProject.ContractCD
      };
    if (pmProject.Status == "E")
      throw new PXSetPropertyException("Project is Suspended and cannot be used for data entry.")
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }

  protected virtual void EPActivityApprove_ProjectTaskID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null || e.NewValue == null || !(e.NewValue is int))
      return;
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask>.Config>.Search<PMTask.projectID, PMTask.taskID>(sender.Graph, (object) row.ProjectID, e.NewValue, Array.Empty<object>()));
    if (pmTask == null)
      return;
    bool? nullable = pmTask.IsCompleted;
    nullable = !nullable.GetValueOrDefault() ? pmTask.IsCancelled : throw new PXSetPropertyException("Task is Completed and cannot be used for data entry.")
    {
      ErrorValue = (object) pmTask.TaskCD
    };
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("Task is Canceled and cannot be used for data entry.")
      {
        ErrorValue = (object) pmTask.TaskCD
      };
  }

  protected virtual void EPActivityApprove_EarningTypeID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null || ((PXSelectBase<EPSetup>) this.Setup).Current == null)
      return;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.defContactID>((PXGraph) this, (object) ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current.OwnerID, Array.Empty<object>()));
    if (epEmployee == null)
      return;
    DateTime? date1 = row.Date;
    if (!date1.HasValue)
      return;
    string calendarId = epEmployee.CalendarID;
    date1 = row.Date;
    DateTime date2 = date1.Value;
    if (CalendarHelper.IsWorkDay((PXGraph) this, calendarId, date2))
      e.NewValue = (object) ((PXSelectBase<EPSetup>) this.Setup).Current.RegularHoursType;
    else
      e.NewValue = (object) ((PXSelectBase<EPSetup>) this.Setup).Current.HolidaysType;
  }

  protected virtual void EPActivityApprove_ProjectID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    int? projectId = ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current.ProjectID;
    if (projectId.HasValue)
    {
      e.NewValue = (object) ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current.ProjectID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      EPEarningType epEarningType = PXResultset<EPEarningType>.op_Implicit(PXSelectBase<EPEarningType, PXSelect<EPEarningType, Where<EPEarningType.typeCD, Equal<Required<EPEarningType.typeCD>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.EarningTypeID
      }));
      if (epEarningType == null)
        return;
      projectId = epEarningType.ProjectID;
      if (!projectId.HasValue || PXSelectorAttribute.Select(cache, e.Row, cache.GetField(typeof (EPActivityApprove.projectID)), (object) epEarningType.ProjectID) == null)
        return;
      e.NewValue = (object) epEarningType.ProjectID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EPActivityApprove_ProjectTaskID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    int? nullable = ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current.ProjectTaskID;
    if (nullable.HasValue)
    {
      e.NewValue = (object) ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current.ProjectTaskID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      nullable = row.ProjectID;
      if (nullable.HasValue)
      {
        nullable = row.ProjectTaskID;
        if (nullable.HasValue)
        {
          e.NewValue = (object) row.ProjectTaskID;
          ((CancelEventArgs) e).Cancel = true;
          return;
        }
      }
      if (row.ParentTaskNoteID.HasValue)
      {
        EPActivityApprove epActivityApprove = PXResultset<EPActivityApprove>.op_Implicit(PXSelectBase<EPActivityApprove, PXSelect<EPActivityApprove>.Config>.Search<EPActivityApprove.noteID>((PXGraph) this, (object) row.ParentTaskNoteID, Array.Empty<object>()));
        if (epActivityApprove != null)
        {
          nullable = epActivityApprove.ProjectID;
          int? projectId = row.ProjectID;
          if (nullable.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable.HasValue == projectId.HasValue)
          {
            e.NewValue = (object) epActivityApprove.ProjectTaskID;
            ((CancelEventArgs) e).Cancel = true;
          }
        }
      }
      EPEarningType epEarningType = (EPEarningType) PXSelectorAttribute.Select<PMTimeActivity.earningTypeID>(cache, (object) row);
      if (e.NewValue != null || epEarningType == null)
        return;
      int? projectId1 = epEarningType.ProjectID;
      nullable = row.ProjectID;
      if (!(projectId1.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId1.HasValue == nullable.HasValue) || !(PXSelectorAttribute.Select(cache, e.Row, cache.GetField(typeof (EPTimeCardSummary.projectTaskID)), (object) epEarningType.TaskID) is PMTask pmTask) || !pmTask.VisibleInTA.GetValueOrDefault())
        return;
      e.NewValue = (object) epEarningType.TaskID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EPActivityApprove_Hold_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    if (row.ApprovalStatus == "AP")
      cache.RaiseExceptionHandling<EPActivityApprove.hold>((object) row, (object) null, (Exception) new PXSetPropertyException("Approved", (PXErrorLevel) 3));
    if (!row.RefNoteID.HasValue)
      return;
    CRActivity crActivity = (CRActivity) ((PXSelectBase) this.Parent).View.SelectSingleBound((object[]) new EPActivityApprove[1]
    {
      row
    }, Array.Empty<object>());
    if (crActivity == null)
      return;
    crActivity.UIStatus = row.Hold.GetValueOrDefault() ? "OP" : "CD";
    ((PXSelectBase<CRActivity>) this.Parent).Update(crActivity);
  }

  protected virtual void EPActivityApprove_Hold_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    e.ReturnValue = (object) (row.ApprovalStatus == "OP");
  }

  protected virtual int? GetTimeBillable(EPActivityApprove row, int? OldTimeSpent)
  {
    if (row.TimeCardCD != null || row.Billed.GetValueOrDefault())
      return row.TimeBillable;
    if (!row.IsBillable.GetValueOrDefault())
      return new int?(0);
    if (OldTimeSpent.GetValueOrDefault() != 0)
    {
      int? nullable = OldTimeSpent;
      int? timeBillable = row.TimeBillable;
      if (!(nullable.GetValueOrDefault() == timeBillable.GetValueOrDefault() & nullable.HasValue == timeBillable.HasValue))
        return row.TimeBillable;
    }
    return row.TimeSpent;
  }

  protected virtual void EPActivityApprove_IsBillable_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    row.TimeBillable = this.GetTimeBillable(row, new int?());
  }

  protected virtual void EPActivityApprove_TimeSpent_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    row.TimeBillable = this.GetTimeBillable(row, (int?) e.OldValue);
  }

  protected virtual void EPActivityApprove_TimeBillable_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPActivityApprove row))
      return;
    row.TimeBillable = this.GetTimeBillable(row, (int?) e.OldValue);
  }

  protected virtual void EPActivityApprove_Type_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if ((EPActivityApprove) e.Row == null)
    {
      e.NewValue = (object) null;
    }
    else
    {
      e.NewValue = (object) ((PXSelectBase<EPSetup>) this.Setup).Current.DefaultActivityType;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EPActivityApprove_Date_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXGraph) this).IsImport)
      return;
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      e.NewValue = (object) null;
    else
      e.NewValue = (object) CRActivityMaint.GetNextActivityStartDate<EPActivityApprove>((PXGraph) this, ((PXSelectBase<EPActivityApprove>) this.Activity).Select(Array.Empty<object>()), (PMTimeActivity) row, ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current.FromWeek, ((PXSelectBase<EmployeeActivitiesEntry.PMTimeActivityFilter>) this.Filter).Current.TillWeek, ((PXSelectBase) this.TempData).Cache, typeof (CRActivityMaint.EPTempData.lastEnteredDate));
  }

  protected virtual void EPActivityApprove_Date_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    if (row.Date.HasValue)
      ((PXSelectBase) this.TempData).Cache.SetValue<CRActivityMaint.EPTempData.lastEnteredDate>((object) ((PXSelectBase<CRActivityMaint.EPTempData>) this.TempData).Current, (object) row.Date);
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(cache, (PMTimeActivity) row, (DateTime?) e.OldValue, row.Date);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.projectID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.unionID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.certifiedJob>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.labourItemID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.projectID>>) e).Cache.SetDefaultExt<PMTimeActivity.employeeRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.projectTaskID>>) e).Cache.SetDefaultExt<PMTimeActivity.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPActivityApprove, PMTimeActivity.labourItemID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, PMTimeActivity.labourItemID>>) e).Cache.SetDefaultExt<PMTimeActivity.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPActivityApprove, PMTimeActivity.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, PMTimeActivity.costCodeID>>) e).Cache.SetDefaultExt<PMTimeActivity.workCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.hold> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, EPActivityApprove.hold>>) e).Cache.SetDefaultExt<EPActivityApprove.approverID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPActivityApprove, PMTimeActivity.earningTypeID> e)
  {
    if (e.Row.ProjectID.HasValue && !ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityApprove, PMTimeActivity.earningTypeID>>) e).Cache.SetDefaultExt<EPActivityApprove.projectID>((object) e.Row);
  }

  public virtual void Persist() => ((PXGraph) this).Persist();

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPActivityApprove.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPTimecardProjectTask(typeof (EPActivityApprove.projectID), "TA", DisplayName = "Project Task", BqlField = typeof (PMTimeActivity.projectTaskID))]
  [PXFormula(typeof (Default<EPActivityApprove.parentTaskNoteID, PMTimeActivity.earningTypeID>))]
  [PXMergeAttributes]
  public virtual void EPActivityApprove_ProjectTaskID_CacheAttached(PXCache cache)
  {
  }

  [PXDefault(typeof (EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID))]
  [PXDBInt(BqlField = typeof (PMTimeActivity.ownerID))]
  [PXMergeAttributes]
  public virtual void EPActivityApprove_OwnerID_CacheAttached(PXCache cache)
  {
  }

  [PXDBString(255 /*0xFF*/, InputMask = "", IsUnicode = true, BqlField = typeof (PMTimeActivity.summary))]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<Current<EPActivityApprove.summary>, IsNotNull>, Current<EPActivityApprove.summary>, Case<Where<EPActivityApprove.parentTaskNoteID, IsNotNull>, Selector<EPActivityApprove.parentTaskNoteID, EPActivityApprove.summary>, Case<Where<EPActivityApprove.projectTaskID, IsNotNull>, Selector<EPActivityApprove.projectTaskID, PMTask.description>>>>>))]
  [PXUIField]
  [PXFieldDescription]
  [PXMergeAttributes]
  public virtual void EPActivityApprove_Summary_CacheAttached(PXCache cache)
  {
  }

  [PXSelector(typeof (Search<CRActivity.noteID, Where<CRActivity.noteID, NotEqual<Current<EPActivityApprove.noteID>>, And<CRActivity.ownerID, Equal<Current<EPActivityApprove.ownerID>>, And<CRActivity.classID, NotEqual<CRActivityClass.events>, And<Where<CRActivity.classID, Equal<CRActivityClass.task>, Or<CRActivity.classID, Equal<CRActivityClass.events>>>>>>>, OrderBy<Desc<CRActivity.createdDateTime>>>), new System.Type[] {typeof (CRActivity.subject), typeof (CRActivity.uistatus)}, DescriptionField = typeof (CRActivity.subject))]
  [PXMergeAttributes]
  protected virtual void EPActivityApprove_ParentTaskNoteID_CacheAttached(PXCache cache)
  {
  }

  [PXUIField]
  [PXMergeAttributes]
  public virtual void EPActivityApprove_TimeCardCD_CacheAttached(PXCache cache)
  {
  }

  [PXDimensionSelector("CONTRACT", typeof (Search2<PX.Objects.CT.Contract.contractCD, InnerJoin<ContractBillingSchedule, On<PX.Objects.CT.Contract.contractID, Equal<ContractBillingSchedule.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CT.Contract.customerID>>>>, Where<ContractExEx.baseType, Equal<CTPRType.contract>>>), typeof (PX.Objects.CT.Contract.contractCD), new System.Type[] {typeof (PX.Objects.CT.Contract.contractCD), typeof (PX.Objects.CT.Contract.customerID), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CT.Contract.locationID), typeof (PX.Objects.CT.Contract.description), typeof (PX.Objects.CT.Contract.status), typeof (PX.Objects.CT.Contract.expireDate), typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)}, DescriptionField = typeof (PX.Objects.CT.Contract.description), Filterable = true)]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual void ContractEx_ContractCD_CacheAttached(PXCache cache)
  {
  }

  public static void UpdateReportedInTimeZoneIDIfNeeded(
    PXCache cache,
    PMTimeActivity row,
    DateTime? oldValue,
    DateTime? newValue)
  {
    DateTime? nullable1 = oldValue;
    DateTime? nullable2 = newValue;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    string id = newValue.HasValue ? LocaleInfo.GetTimeZone()?.Id : (string) null;
    cache.SetValueExt<PMTimeActivity.reportedInTimeZoneID>((object) row, (object) id);
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  private bool ScreenIDsEqual(string id1, string id2)
  {
    return !string.IsNullOrEmpty(id1) && !string.IsNullOrEmpty(id2) && id1.Replace(".", "").Equals(id2.Replace(".", ""), StringComparison.OrdinalIgnoreCase);
  }

  [Serializable]
  public class PMTimeActivityFilter : OwnedFilter
  {
    private int? _fromWeek;

    [PXDefault(typeof (AccessInfo.contactID))]
    [SubordinateAndWingmenOwnerEmployee(DisplayName = "Employee")]
    public override int? OwnerID { set; get; }

    [PXDBInt]
    [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
    [PXUIField]
    public virtual int? FromWeek
    {
      set => this._fromWeek = value;
      get => this._fromWeek;
    }

    [PXDBInt]
    [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
    [PXUIField]
    public virtual int? TillWeek { set; get; }

    [EPProject(typeof (EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID), DisplayName = "Project")]
    public virtual int? ProjectID { get; set; }

    [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EmployeeActivitiesEntry.PMTimeActivityFilter.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
    [EPTimecardProjectTask(typeof (EmployeeActivitiesEntry.PMTimeActivityFilter.projectID), "TA", DisplayName = "Project Task", AllowNull = true)]
    public virtual int? ProjectTaskID { set; get; }

    [PXGuid]
    public virtual Guid? NoteID { get; set; }

    [PXBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Include All Rejected")]
    public virtual bool? IncludeReject { set; get; }

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

    public new abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.ownerID>
    {
    }

    public abstract class fromWeek : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.fromWeek>
    {
    }

    public abstract class tillWeek : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.tillWeek>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.projectID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.projectTaskID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.noteID>
    {
    }

    public abstract class includeReject : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.includeReject>
    {
    }

    public abstract class regularTime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.regularTime>
    {
    }

    public abstract class regularOvertime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.regularOvertime>
    {
    }

    public abstract class regularTotal : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.regularTotal>
    {
    }

    public abstract class billableTime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.billableTime>
    {
    }

    public abstract class billableOvertime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.billableOvertime>
    {
    }

    public abstract class billableTotal : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesEntry.PMTimeActivityFilter.billableTotal>
    {
    }
  }

  public class CrewTimeEntry : 
    CrewTimeEntryGraph<EmployeeActivitiesEntry, EmployeeActivitiesEntry.PMTimeActivityFilter>
  {
  }
}
