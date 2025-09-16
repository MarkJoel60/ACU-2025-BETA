// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimecardWithTotals
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXProjection(typeof (Select2<EPTimeCard, InnerJoin<EPEmployeeEx, On<EPEmployeeEx.bAccountID, Equal<EPTimeCard.employeeID>>, LeftJoin<TimecardRegularCurrentTotals, On<TimecardRegularCurrentTotals.weekID, Equal<EPTimeCard.weekId>, And<EPTimeCard.isHold, Equal<True>, And<EPEmployeeEx.defContactID, Equal<TimecardRegularCurrentTotals.owner>>>>, LeftJoin<TimecardOvertimeCurrentTotals, On<TimecardOvertimeCurrentTotals.weekID, Equal<EPTimeCard.weekId>, And<EPTimeCard.isHold, Equal<True>, And<EPEmployeeEx.defContactID, Equal<TimecardOvertimeCurrentTotals.owner>>>>>>>>))]
[Serializable]
public class TimecardWithTotals : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _WeekID;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected DateTime? _WeekStartDate;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC", BqlField = typeof (EPTimeCard.timeCardCD))]
  [PXUIField]
  [PXSelector(typeof (Search2<EPTimeCard.timeCardCD, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPTimeCard.employeeID>>>, Where<EPTimeCard.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPTimeCard.noteID, Approver<Current<AccessInfo.contactID>>>>>>>), new Type[] {typeof (EPTimeCard.timeCardCD), typeof (EPTimeCard.employeeID), typeof (EPTimeCard.weekDescription), typeof (EPTimeCard.status)})]
  [PXFieldDescription]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual 
  #nullable disable
  string TimeCardCD { get; set; }

  [PXDBInt(BqlField = typeof (EPTimeCard.employeeID))]
  [PXUIField(DisplayName = "Employee")]
  [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.timeEntries))]
  [PXFieldDescription]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? EmployeeID { get; set; }

  [PXDBString(1, BqlField = typeof (EPTimeCard.status))]
  [EPTimeCardStatus]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual string Status { get; set; }

  [PXDBInt(BqlField = typeof (EPTimeCard.weekId))]
  [PXUIField(DisplayName = "Week")]
  [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? WeekID
  {
    get => this._WeekID;
    set => this._WeekID = value;
  }

  [PXNote(BqlField = typeof (EPTimeCard.noteID), DescriptionField = typeof (EPTimeCard.timeCardCD), Selector = typeof (EPTimeCard.timeCardCD))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBBool(BqlField = typeof (EPTimeCard.isApproved))]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsApproved { get; set; }

  [PXDBBool(BqlField = typeof (EPTimeCard.isRejected))]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsRejected { get; set; }

  [PXDBBool(BqlField = typeof (EPTimeCard.isHold))]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  public virtual bool? IsHold { get; set; }

  [PXDBBool(BqlField = typeof (EPTimeCard.isReleased))]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsReleased { get; set; }

  [PXDBCreatedByID(BqlField = typeof (EPTimeCard.createdByID))]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBInt(BqlField = typeof (EPTimeCard.timeSpent))]
  public virtual int? TimeSpentFinal { get; set; }

  [PXDBInt(BqlField = typeof (EPTimeCard.timeBillable))]
  public virtual int? TimeBillableFinal { get; set; }

  [PXDBInt(BqlField = typeof (EPTimeCard.overtimeSpent))]
  public virtual int? OvertimeSpentFinal { get; set; }

  [PXDBInt(BqlField = typeof (EPTimeCard.overtimeBillable))]
  public virtual int? OvertimeBillableFinal { get; set; }

  [PXDBInt(BqlField = typeof (TimecardRegularCurrentTotals.timeSpent))]
  public virtual int? TimeSpentCurrent { get; set; }

  [PXDBInt(BqlField = typeof (TimecardRegularCurrentTotals.timeBillable))]
  public virtual int? TimeBillableCurrent { get; set; }

  [PXDBInt(BqlField = typeof (TimecardOvertimeCurrentTotals.overtimeSpent))]
  public virtual int? OvertimeSpentCurrent { get; set; }

  [PXDBInt(BqlField = typeof (TimecardOvertimeCurrentTotals.overtimeBillable))]
  public virtual int? OvertimeBillableCurrent { get; set; }

  /// <summary>Gets sets Employee DefContactID</summary>
  [PXDBInt(BqlField = typeof (EPEmployeeEx.defContactID))]
  [PXUIField]
  public virtual int? DefContactID { get; set; }

  [PXInt]
  [PXTimeList(1, 1440)]
  [PXDependsOnFields(new Type[] {typeof (TimecardWithTotals.timeSpentFinal), typeof (TimecardWithTotals.timeSpentCurrent)})]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? TimeSpentCalc
  {
    get
    {
      int? nullable = this.TimeSpentFinal;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.TimeSpentCurrent;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      return new int?(valueOrDefault1 + valueOrDefault2);
    }
  }

  [PXInt]
  [PXTimeList(1, 1440)]
  [PXDependsOnFields(new Type[] {typeof (TimecardWithTotals.overtimeSpentFinal), typeof (TimecardWithTotals.overtimeSpentCurrent)})]
  [PXUIField(DisplayName = "Overtime Spent", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? OvertimeSpentCalc
  {
    get
    {
      int? nullable = this.OvertimeSpentFinal;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.OvertimeSpentCurrent;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num = valueOrDefault1 + valueOrDefault2;
      return num == 0 ? new int?() : new int?(num);
    }
  }

  [PXInt]
  [PXTimeList(1, 1440)]
  [PXDependsOnFields(new Type[] {typeof (TimecardWithTotals.timeSpentCalc), typeof (TimecardWithTotals.overtimeSpentCalc)})]
  [PXUIField(DisplayName = "Total Time Spent", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? TotalSpentCalc
  {
    get
    {
      int? timeSpentCalc = this.TimeSpentCalc;
      int valueOrDefault = this.OvertimeSpentCalc.GetValueOrDefault();
      return !timeSpentCalc.HasValue ? new int?() : new int?(timeSpentCalc.GetValueOrDefault() + valueOrDefault);
    }
  }

  [PXInt]
  [PXTimeList(1, 1440)]
  [PXDependsOnFields(new Type[] {typeof (TimecardWithTotals.timeBillableFinal), typeof (TimecardWithTotals.timeBillableCurrent)})]
  [PXUIField(DisplayName = "Billable", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? TimeBillableCalc
  {
    get
    {
      int? nullable = this.TimeBillableFinal;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.TimeBillableCurrent;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num = valueOrDefault1 + valueOrDefault2;
      return num == 0 ? new int?() : new int?(num);
    }
  }

  [PXInt]
  [PXTimeList(1, 1440)]
  [PXDependsOnFields(new Type[] {typeof (TimecardWithTotals.overtimeBillableFinal), typeof (TimecardWithTotals.overtimeBillableCurrent)})]
  [PXUIField(DisplayName = "Billable Overtime", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? OvertimeBillableCalc
  {
    get
    {
      int? nullable = this.OvertimeBillableFinal;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.OvertimeBillableCurrent;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num = valueOrDefault1 + valueOrDefault2;
      return num == 0 ? new int?() : new int?(num);
    }
  }

  [PXInt]
  [PXTimeList(1, 1440)]
  [PXDependsOnFields(new Type[] {typeof (TimecardWithTotals.timeBillableCalc), typeof (TimecardWithTotals.overtimeBillableCalc)})]
  [PXUIField(DisplayName = "Total Billable", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? TotalBillableCalc
  {
    get
    {
      int? nullable = this.TimeBillableCalc;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.OvertimeBillableCalc;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num = valueOrDefault1 + valueOrDefault2;
      return num == 0 ? new int?() : new int?(num);
    }
  }

  [PXInt]
  [PXDependsOnFields(new Type[] {typeof (TimecardWithTotals.totalBillableCalc), typeof (TimecardWithTotals.totalSpentCalc)})]
  [PXUIField(DisplayName = "Billing Ratio", Enabled = false)]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  public virtual int? BillingRateCalc
  {
    get
    {
      int? totalSpentCalc1 = this.TotalSpentCalc;
      int num = 0;
      if (totalSpentCalc1.GetValueOrDefault() == num & totalSpentCalc1.HasValue)
        return new int?(0);
      int? totalBillableCalc = this.TotalBillableCalc;
      int? nullable = totalBillableCalc.HasValue ? new int?(totalBillableCalc.GetValueOrDefault() * 100) : new int?();
      int? totalSpentCalc2 = this.TotalSpentCalc;
      return !(nullable.HasValue & totalSpentCalc2.HasValue) ? new int?() : new int?(nullable.GetValueOrDefault() / totalSpentCalc2.GetValueOrDefault());
    }
  }

  [TimecardWeekStartDate(typeof (TimecardWithTotals.weekId))]
  [PXUIField(DisplayName = "Week Start Date", Visible = false)]
  public virtual DateTime? WeekStartDate { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TimecardWithTotals.selected>
  {
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TimecardWithTotals.timeCardCD>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TimecardWithTotals.employeeID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TimecardWithTotals.status>
  {
  }

  public abstract class weekId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TimecardWithTotals.weekId>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TimecardWithTotals.noteID>
  {
  }

  public abstract class isApproved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TimecardWithTotals.isApproved>
  {
  }

  public abstract class isRejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TimecardWithTotals.isRejected>
  {
  }

  public abstract class isHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TimecardWithTotals.isHold>
  {
  }

  public abstract class isReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TimecardWithTotals.isReleased>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TimecardWithTotals.createdByID>
  {
  }

  public abstract class timeSpentFinal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.timeSpentFinal>
  {
  }

  public abstract class timeBillableFinal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.timeBillableFinal>
  {
  }

  public abstract class overtimeSpentFinal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.overtimeSpentFinal>
  {
  }

  public abstract class overtimeBillableFinal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.overtimeBillableFinal>
  {
  }

  public abstract class timeSpentCurrent : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.timeSpentCurrent>
  {
  }

  public abstract class timeBillableCurrent : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.timeBillableCurrent>
  {
  }

  public abstract class overtimeSpentCurrent : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.overtimeSpentCurrent>
  {
  }

  public abstract class overtimeBillableCurrent : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.overtimeBillableCurrent>
  {
  }

  /// <summary>DefContactID</summary>
  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TimecardWithTotals.defContactID>
  {
  }

  public abstract class timeSpentCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TimecardWithTotals.timeSpentCalc>
  {
  }

  public abstract class overtimeSpentCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.overtimeSpentCalc>
  {
  }

  public abstract class totalSpentCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.totalSpentCalc>
  {
  }

  public abstract class timeBillableCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.timeBillableCalc>
  {
  }

  public abstract class overtimeBillableCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.overtimeBillableCalc>
  {
  }

  public abstract class totalBillableCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.totalBillableCalc>
  {
  }

  public abstract class billingRateCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TimecardWithTotals.billingRateCalc>
  {
  }

  public abstract class weekStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TimecardWithTotals.weekStartDate>
  {
  }
}
