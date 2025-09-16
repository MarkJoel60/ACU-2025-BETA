// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeActivitiesApprove
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.EP;

public class EmployeeActivitiesApprove : PXGraph<
#nullable disable
EmployeeActivitiesApprove>
{
  [PXHidden]
  public PXSelect<ContractEx> dummyContract;
  [PXHidden]
  public PXSelect<CRCase> dummyCase;
  [PXHidden]
  public PXSetup<EPSetup> Setup;
  public PXFilter<EmployeeActivitiesApprove.EPActivityFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<EPActivityApprove, LeftJoin<CRActivityLink, On<CRActivityLink.noteID, Equal<PMTimeActivity.refNoteID>>, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<PMTimeActivity.earningTypeID>>, LeftJoin<PMProject, On<PMProject.contractID, Equal<EPActivityApprove.projectID>>, LeftJoin<CRCase, On<CRCase.noteID, Equal<CRActivityLink.refNoteID>>, LeftJoin<ContractEx, On<CRCase.contractID, Equal<ContractEx.contractID>>>>>>>, Where2<Where<EPActivityApprove.approverID, Equal<Current<EmployeeActivitiesApprove.EPActivityFilter.approverID>>, Or<PMProject.approverID, Equal<Current<EmployeeActivitiesApprove.EPActivityFilter.approverID>>>>, And<EPActivityApprove.approverID, IsNotNull, And2<Where<EPActivityApprove.approvalStatus, NotEqual<ActivityStatusListAttribute.canceled>, And<EPActivityApprove.approvalStatus, NotEqual<ActivityStatusListAttribute.completed>, And<EPActivityApprove.approvalStatus, NotEqual<ActivityStatusListAttribute.open>, And<PMTimeActivity.released, NotEqual<True>>>>>, And<EPActivityApprove.date, Less<Add<Current<EmployeeActivitiesApprove.EPActivityFilter.tillDate>, int1>>, And2<Where<EPActivityApprove.date, GreaterEqual<Current<EmployeeActivitiesApprove.EPActivityFilter.fromDate>>, Or<Current<EmployeeActivitiesApprove.EPActivityFilter.fromDate>, IsNull>>, And2<Where<EPActivityApprove.projectID, Equal<Current<EmployeeActivitiesApprove.EPActivityFilter.projectID>>, Or<Current<EmployeeActivitiesApprove.EPActivityFilter.projectID>, IsNull>>, And2<Where<EPActivityApprove.projectTaskID, Equal<Current<EmployeeActivitiesApprove.EPActivityFilter.projectTaskID>>, Or<Current<EmployeeActivitiesApprove.EPActivityFilter.projectTaskID>, IsNull>>, And2<Where<EPActivityApprove.ownerID, Equal<Current<EmployeeActivitiesApprove.EPActivityFilter.employeeID>>, Or<Current<EmployeeActivitiesApprove.EPActivityFilter.employeeID>, IsNull>>, And<PMTimeActivity.released, Equal<False>, And<EPActivityApprove.trackTime, Equal<True>>>>>>>>>>>, OrderBy<Desc<EPActivityApprove.date>>> Activity;
  public PXSave<EmployeeActivitiesApprove.EPActivityFilter> Save;
  public PXCancel<EmployeeActivitiesApprove.EPActivityFilter> Cancel;
  public PXAction<EmployeeActivitiesApprove.EPActivityFilter> viewDetails;
  public PXAction<EmployeeActivitiesApprove.EPActivityFilter> approveAll;
  public PXAction<EmployeeActivitiesApprove.EPActivityFilter> rejectAll;
  public PXAction<EmployeeActivitiesApprove.EPActivityFilter> viewCase;
  public PXAction<EmployeeActivitiesApprove.EPActivityFilter> viewContract;

  public EmployeeActivitiesApprove()
  {
    if (((PXSelectBase<EPSetup>) this.Setup).Current == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (EPSetup), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Time & Expenses Preferences")
      });
    PXUIFieldAttribute.SetVisible<EPActivityApprove.contractID>(((PXSelectBase) this.Activity).Cache, (object) null, !PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    PXUIFieldAttribute.SetVisible<ContractEx.contractCD>(((PXSelectBase) this.dummyContract).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    PXUIFieldAttribute.SetVisible<CRCase.caseCD>(((PXSelectBase) this.dummyCase).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    if (!PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
    {
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.timeSpent).Name, false);
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.timeBillable).Name, false);
      PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (PMTimeActivity.isBillable).Name, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesApprove.EPActivityFilter.regularOvertime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesApprove.EPActivityFilter.regularTime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesApprove.EPActivityFilter.regularTotal>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesApprove.EPActivityFilter.billableOvertime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesApprove.EPActivityFilter.billableTime>(((PXSelectBase) this.Filter).Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<EmployeeActivitiesApprove.EPActivityFilter.billableTotal>(((PXSelectBase) this.Filter).Cache, (object) null, false);
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return;
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Activity).Cache, typeof (EPActivityApprove.projectTaskID).Name, false);
    PXUIFieldAttribute.SetVisible<EmployeeActivitiesApprove.EPActivityFilter.projectTaskID>(((PXSelectBase) this.Filter).Cache, (object) null, false);
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

  [PXUIField(DisplayName = "Approve All")]
  [PXButton]
  public virtual IEnumerable ApproveAll(PXAdapter adapter)
  {
    if (((PXSelectBase<EPActivityApprove>) this.Activity).Current == null || ((PXSelectBase) this.Filter).View.Ask("Are you sure you want to approve all the listed records?", (MessageButtons) 4) != 6)
      return adapter.Get();
    foreach (PXResult<EPActivityApprove> pxResult in ((PXSelectBase<EPActivityApprove>) this.Activity).Select(Array.Empty<object>()))
    {
      EPActivityApprove epActivityApprove = PXResult<EPActivityApprove>.op_Implicit(pxResult);
      epActivityApprove.IsApproved = new bool?(true);
      epActivityApprove.IsReject = new bool?(false);
      ((PXSelectBase) this.Activity).Cache.Update((object) epActivityApprove);
    }
    ((PXGraph) this).Persist();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Reject All")]
  [PXButton]
  public virtual IEnumerable RejectAll(PXAdapter adapter)
  {
    if (((PXSelectBase<EPActivityApprove>) this.Activity).Current == null || ((PXSelectBase) this.Filter).View.Ask("Are you sure you want to reject all the listed records?", (MessageButtons) 4) != 6)
      return adapter.Get();
    foreach (PXResult<EPActivityApprove> pxResult in ((PXSelectBase<EPActivityApprove>) this.Activity).Select(Array.Empty<object>()))
    {
      EPActivityApprove epActivityApprove = PXResult<EPActivityApprove>.op_Implicit(pxResult);
      epActivityApprove.IsApproved = new bool?(false);
      epActivityApprove.IsReject = new bool?(true);
      ((PXSelectBase) this.Activity).Cache.Update((object) epActivityApprove);
    }
    ((PXGraph) this).Persist();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(Visible = false)]
  public virtual IEnumerable ViewCase(PXAdapter adapter)
  {
    EPActivityApprove current = ((PXSelectBase<EPActivityApprove>) this.Activity).Current;
    if (current != null && current.RefNoteID.HasValue)
    {
      CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.RefNoteID
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
      PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelectJoin<PX.Objects.CT.Contract, InnerJoin<CRCase, On<CRCase.contractID, Equal<PX.Objects.CT.Contract.contractID>>, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) current.RefNoteID
      }));
      if (contract == null)
        contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) current.ContractID
        }));
      if (contract != null)
        PXRedirectHelper.TryRedirect((PXGraph) this, (object) contract, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIEnabled(typeof (PMTimeActivity.isBillable))]
  [PXMergeAttributes]
  protected virtual void EPActivityApprove_TimeBillable_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPActivityFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EmployeeActivitiesApprove.EPActivityFilter row = (EmployeeActivitiesApprove.EPActivityFilter) e.Row;
    if (row == null)
      return;
    row.BillableTime = new int?(0);
    row.BillableOvertime = new int?(0);
    row.BillableTotal = new int?(0);
    row.RegularTime = new int?(0);
    row.RegularOvertime = new int?(0);
    row.RegularTotal = new int?(0);
    foreach (PXResult<EPActivityApprove, CRActivityLink, EPEarningType> pxResult in ((PXSelectBase<EPActivityApprove>) this.Activity).Select(Array.Empty<object>()))
    {
      EPActivityApprove epActivityApprove = PXResult<EPActivityApprove, CRActivityLink, EPEarningType>.op_Implicit(pxResult);
      int? nullable1;
      if (PXResult<EPActivityApprove, CRActivityLink, EPEarningType>.op_Implicit(pxResult).IsOvertime.GetValueOrDefault())
      {
        EmployeeActivitiesApprove.EPActivityFilter epActivityFilter1 = row;
        int? regularOvertime = epActivityFilter1.RegularOvertime;
        nullable1 = epActivityApprove.TimeSpent;
        int num1 = nullable1 ?? 0;
        int? nullable2;
        if (!regularOvertime.HasValue)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = new int?(regularOvertime.GetValueOrDefault() + num1);
        epActivityFilter1.RegularOvertime = nullable2;
        EmployeeActivitiesApprove.EPActivityFilter epActivityFilter2 = row;
        int? billableOvertime = epActivityFilter2.BillableOvertime;
        nullable1 = epActivityApprove.TimeBillable;
        int num2 = nullable1 ?? 0;
        int? nullable3;
        if (!billableOvertime.HasValue)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new int?(billableOvertime.GetValueOrDefault() + num2);
        epActivityFilter2.BillableOvertime = nullable3;
      }
      else
      {
        EmployeeActivitiesApprove.EPActivityFilter epActivityFilter3 = row;
        int? regularTime = epActivityFilter3.RegularTime;
        nullable1 = epActivityApprove.TimeSpent;
        int num3 = nullable1 ?? 0;
        int? nullable4;
        if (!regularTime.HasValue)
        {
          nullable1 = new int?();
          nullable4 = nullable1;
        }
        else
          nullable4 = new int?(regularTime.GetValueOrDefault() + num3);
        epActivityFilter3.RegularTime = nullable4;
        EmployeeActivitiesApprove.EPActivityFilter epActivityFilter4 = row;
        int? billableTime = epActivityFilter4.BillableTime;
        nullable1 = epActivityApprove.TimeBillable;
        int num4 = nullable1 ?? 0;
        int? nullable5;
        if (!billableTime.HasValue)
        {
          nullable1 = new int?();
          nullable5 = nullable1;
        }
        else
          nullable5 = new int?(billableTime.GetValueOrDefault() + num4);
        epActivityFilter4.BillableTime = nullable5;
      }
      EmployeeActivitiesApprove.EPActivityFilter epActivityFilter5 = row;
      int? billableTime1 = row.BillableTime;
      nullable1 = row.BillableOvertime;
      int? nullable6 = billableTime1.HasValue & nullable1.HasValue ? new int?(billableTime1.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new int?();
      epActivityFilter5.BillableTotal = nullable6;
      EmployeeActivitiesApprove.EPActivityFilter epActivityFilter6 = row;
      nullable1 = row.RegularTime;
      int? regularOvertime1 = row.RegularOvertime;
      int? nullable7 = nullable1.HasValue & regularOvertime1.HasValue ? new int?(nullable1.GetValueOrDefault() + regularOvertime1.GetValueOrDefault()) : new int?();
      epActivityFilter6.RegularTotal = nullable7;
    }
  }

  protected virtual void EPActivityFilter_FromDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    EmployeeActivitiesApprove.EPActivityFilter row = (EmployeeActivitiesApprove.EPActivityFilter) e.Row;
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
    EmployeeActivitiesApprove.EPActivityFilter row = (EmployeeActivitiesApprove.EPActivityFilter) e.Row;
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

  protected virtual void EPActivityFilter_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (((PXSelectBase) this.Activity).Cache.IsDirty && ((PXSelectBase) this.Filter).View.Ask("Any unsaved changes will be discarded.", (MessageButtons) 4) != 6)
      ((CancelEventArgs) e).Cancel = true;
    else
      ((PXSelectBase) this.Activity).Cache.Clear();
  }

  protected virtual void EPActivityApprove_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPActivityApprove.isApproved>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<EPActivityApprove.isReject>(sender, (object) row, true);
  }

  protected virtual void EPActivityApprove_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    EPActivityApprove row = (EPActivityApprove) e.Row;
    if (row == null || e.Operation == 3)
      return;
    bool? nullable = row.IsApproved;
    if (nullable.GetValueOrDefault())
    {
      sender.SetValueExt<EPActivityApprove.approvalStatus>((object) row, (object) "AP");
      row.ApprovedDate = ((PXGraph) this).Accessinfo.BusinessDate;
    }
    else
    {
      nullable = row.IsReject;
      if (nullable.GetValueOrDefault())
      {
        sender.SetValueExt<EPActivityApprove.approvalStatus>((object) row, (object) "RJ");
      }
      else
      {
        if (!(row.ApprovalStatus == "RJ") && !(row.ApprovalStatus == "AP"))
          return;
        sender.SetValueExt<EPActivityApprove.approvalStatus>((object) row, (object) "PA");
      }
    }
  }

  public virtual void Persist()
  {
    List<IGrouping<string, EPActivityApprove>> list = ((PXSelectBase) this.Activity).Cache.Updated.Cast<EPActivityApprove>().Where<EPActivityApprove>((Func<EPActivityApprove, bool>) (a => a.IsReject.GetValueOrDefault() && a.TimeCardCD != null)).GroupBy<EPActivityApprove, string>((Func<EPActivityApprove, string>) (a => a.TimeCardCD)).ToList<IGrouping<string, EPActivityApprove>>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (list.Count > 0)
      {
        TimeCardMaint instance = PXGraph.CreateInstance<TimeCardMaint>();
        ((PXSelectBase<EPTimeCard>) instance.Document).Join<InnerJoin<EPTimeCardSummary, On<EPTimeCardSummary.timeCardCD, Equal<EPTimeCard.timeCardCD>>>>();
        ((PXSelectBase<EPTimeCard>) instance.Document).Join<InnerJoin<PMTask, On<PMTask.taskID, Equal<EPTimeCardSummary.projectTaskID>>>>();
        ((PXSelectBase<EPTimeCard>) instance.Document).WhereOr<Where<PMTask.approverID, Equal<Current<EPSummaryFilter.approverID>>>>();
        foreach (IGrouping<string, EPActivityApprove> grouping in list)
        {
          ((PXGraph) instance).Clear();
          EPTimeCard epTimeCard = PXResultset<EPTimeCard>.op_Implicit(((PXSelectBase<EPTimeCard>) instance.Document).Search<EPTimeCard.timeCardCD>((object) grouping.Key, Array.Empty<object>()));
          ((PXSelectBase<EPTimeCard>) instance.Document).Current = epTimeCard;
          if (epTimeCard.Status == "A")
          {
            epTimeCard.Status = "C";
            epTimeCard.IsApproved = new bool?(false);
            epTimeCard.IsRejected = new bool?(true);
            ((PXSelectBase<EPTimeCard>) instance.Document).Update(epTimeCard);
          }
          else
            ((PXGraph) instance).Actions["Reject"].Press();
          ((PXGraph) instance).Persist();
        }
      }
      ((PXGraph) this).Persist();
      transactionScope.Complete();
    }
  }

  [PXHidden]
  [Serializable]
  public class EPActivityFilter : OwnedFilter
  {
    protected int? _ApproverID;

    [PXDBInt]
    [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.timeEntries))]
    [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
    [PXUIField]
    public virtual int? ApproverID { get; set; }

    [SubordinateAndWingmenOwnerEmployee(DisplayName = "Employee")]
    public virtual int? EmployeeID { set; get; }

    [PXDBDateAndTime(DisplayMask = "d", PreserveTime = true)]
    [PXUIField]
    public virtual DateTime? FromDate { set; get; }

    [PXDBDateAndTime(DisplayMask = "d", PreserveTime = true, UseTimeZone = true)]
    [BusinessDateTimeDefault]
    [PXUIField]
    public virtual DateTime? TillDate { set; get; }

    [EPProject(typeof (OwnedFilter.ownerID), DisplayName = "Project")]
    [PXFormula(typeof (Default<EmployeeActivitiesApprove.EPActivityFilter.approverID>))]
    public virtual int? ProjectID { set; get; }

    [ProjectTask(typeof (EmployeeActivitiesApprove.EPActivityFilter.projectID))]
    public virtual int? ProjectTaskID { set; get; }

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

    public abstract class approverID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.approverID>
    {
    }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.employeeID>
    {
    }

    public abstract class fromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.fromDate>
    {
    }

    public abstract class tillDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.tillDate>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.projectID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.projectTaskID>
    {
    }

    public abstract class regularTime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.regularTime>
    {
    }

    public abstract class regularOvertime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.regularOvertime>
    {
    }

    public abstract class regularTotal : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.regularTotal>
    {
    }

    public abstract class billableTime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.billableTime>
    {
    }

    public abstract class billableOvertime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.billableOvertime>
    {
    }

    public abstract class billableTotal : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmployeeActivitiesApprove.EPActivityFilter.billableTotal>
    {
    }
  }

  [PXDBInt]
  [PXUIField]
  public class ProjectByApproverAttribute : PXEntityAttribute
  {
    public ProjectByApproverAttribute()
    {
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", typeof (Search5<PMProject.contractID, LeftJoin<PMTask, On<PMTask.projectID, Equal<PMProject.contractID>, And<PMTask.approverID, Equal<Current<EmployeeActivitiesApprove.EPActivityFilter.approverID>>>>>, Where<PMProject.isActive, Equal<True>, And<Where<PMTask.taskID, IsNotNull, Or<PMProject.approverID, Equal<Current<EmployeeActivitiesApprove.EPActivityFilter.approverID>>>>>>, Aggregate<GroupBy<PMProject.contractID>>, OrderBy<Asc<PMProject.contractCD>>>), typeof (PMProject.contractCD), new System.Type[3]
      {
        typeof (PMProject.contractCD),
        typeof (PMProject.description),
        typeof (PMProject.status)
      })
      {
        DescriptionField = typeof (PMProject.description),
        ValidComboRequired = true,
        CacheGlobal = true
      });
      this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    }

    public virtual void CacheAttached(PXCache sender)
    {
      ((PXAggregateAttribute) this).CacheAttached(sender);
      this.Enabled = ProjectAttribute.IsPMVisible("TA");
    }
  }
}
