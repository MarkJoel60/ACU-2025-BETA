// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimeCard
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Stores information on the activities performed by an employee during a week. The information will be displayed on the Employee Time Cards (EP406000) form.
/// </summary>
[PXPrimaryGraph(typeof (TimeCardMaint))]
[PXCacheName("Employee Time Card")]
[Serializable]
public class EPTimeCard : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign
{
  protected int? _WeekID;
  protected int? _SummaryLineCntr;
  protected Guid? _NoteID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected DateTime? _WeekStartDate;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (EPSetup.timeCardNumberingID), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search2<EPTimeCard.timeCardCD, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPTimeCard.employeeID>>>, Where<EPTimeCard.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPTimeCard.noteID, Approver<Current<AccessInfo.contactID>>, Or<EPTimeCard.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.timeEntries>>>>>>>), new System.Type[] {typeof (EPTimeCard.timeCardCD), typeof (EPTimeCard.employeeID), typeof (EPTimeCard.employeeID_CREmployee_acctName), typeof (EPTimeCard.weekDescription), typeof (EPTimeCard.status)})]
  [PXFieldDescription]
  public virtual string TimeCardCD { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXUIField(DisplayName = "Employee")]
  [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.timeEntries))]
  [PXFieldDescription]
  public virtual int? EmployeeID { get; set; }

  [PXDBString(1)]
  [PXDefault("H")]
  [EPTimeCardStatus]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string Status { get; set; }

  [PXUIField(DisplayName = "Orig. Ref. Nbr.", Enabled = false)]
  [PXDBString(10, IsUnicode = true)]
  public virtual string OrigTimeCardCD { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Week")]
  [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
  public virtual int? WeekID
  {
    get => this._WeekID;
    set => this._WeekID = value;
  }

  [PXDBBool]
  [PXUIField(Visible = false)]
  [PXDefault(true)]
  public virtual bool? IsHold { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsApproved { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsRejected { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Visible = false)]
  public virtual bool? IsReleased { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Workgroup ID", Visible = false)]
  [PXSelector(typeof (EPCompanyTreeOwner.workGroupID), SubstituteKey = typeof (EPCompanyTreeOwner.description))]
  public virtual int? WorkgroupID { get; set; }

  [PXInt]
  [PXUIField(Visible = false)]
  public virtual int? OwnerID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? SummaryLineCntr
  {
    get => this._SummaryLineCntr;
    set => this._SummaryLineCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  public virtual int? TimeSpent { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Overtime", Enabled = false)]
  public virtual int? OvertimeSpent { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Time Billable", Enabled = false)]
  public virtual int? TimeBillable { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Billable Overtime", Enabled = false)]
  public virtual int? OvertimeBillable { get; set; }

  [PXInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Total Time Spent", Enabled = false)]
  public virtual int? TotalTimeSpent
  {
    [PXDependsOnFields(new System.Type[] {typeof (EPTimeCard.timeSpent), typeof (EPTimeCard.overtimeSpent)})] get
    {
      int? timeSpent = this.TimeSpent;
      int? overtimeSpent = this.OvertimeSpent;
      return !(timeSpent.HasValue & overtimeSpent.HasValue) ? new int?() : new int?(timeSpent.GetValueOrDefault() + overtimeSpent.GetValueOrDefault());
    }
  }

  [PXInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Total Time Billable", Enabled = false)]
  public virtual int? TotalTimeBillable
  {
    [PXDependsOnFields(new System.Type[] {typeof (EPTimeCard.timeBillable), typeof (EPTimeCard.overtimeBillable)})] get
    {
      int? timeBillable = this.TimeBillable;
      int? overtimeBillable = this.OvertimeBillable;
      return !(timeBillable.HasValue & overtimeBillable.HasValue) ? new int?() : new int?(timeBillable.GetValueOrDefault() + overtimeBillable.GetValueOrDefault());
    }
  }

  [PXString]
  [PXFormula(typeof (Selector<EPTimeCard.employeeID, BAccount.acctName>))]
  public string FormCaptionDescription { get; set; }

  [PXNote(ShowInReferenceSelector = true, DescriptionField = typeof (EPTimeCard.timeCardCD), Selector = typeof (EPTimeCard.timeCardCD))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [TimecardWeekStartDate(typeof (EPTimeCard.weekId))]
  [PXUIField(DisplayName = "Week Start Date")]
  [PXDependsOnFields(new System.Type[] {typeof (EPTimeCard.weekId)})]
  public virtual DateTime? WeekStartDate
  {
    get => this._WeekStartDate;
    set => this._WeekStartDate = value;
  }

  [PXDate]
  [PXDependsOnFields(new System.Type[] {typeof (EPTimeCard.weekStartDate)})]
  public virtual DateTime? WeekEndDate
  {
    get
    {
      ref DateTime? local = ref this._WeekStartDate;
      return !local.HasValue ? new DateTime?() : new DateTime?(local.GetValueOrDefault().AddDays(6.0));
    }
    set
    {
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Week")]
  [PXFormula(typeof (Selector<EPTimeCard.weekId, EPWeekRaw.description>))]
  public virtual string WeekDescription { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Week")]
  [PXFieldDescription]
  [PXFormula(typeof (Selector<EPTimeCard.weekId, EPWeekRaw.shortDescription>))]
  public virtual string WeekShortDescription { get; set; }

  [PXInt]
  [PXTimeList(30, 335, ExclusiveValues = false)]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  public virtual int? TimeSpentCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Overtime Spent", Enabled = false)]
  public virtual int? OvertimeSpentCalc { get; set; }

  [PXInt]
  [PXTimeList(30, 335, ExclusiveValues = false)]
  [PXUIField(DisplayName = "Total Time Spent", Enabled = false)]
  public virtual int? TotalSpentCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable", Enabled = false)]
  public virtual int? TimeBillableCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable Overtime", Enabled = false)]
  public virtual int? OvertimeBillableCalc { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Total Billable", Enabled = false)]
  public virtual int? TotalBillableCalc { get; set; }

  [EPTimecardType]
  [PXStringList(new string[] {"N", "C", "D"}, new string[] {"Normal", "Correction", "Normal Corrected"})]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string TimecardType { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Billing Ratio", Enabled = false)]
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

  /// <summary>The time spent total for Sunday.</summary>
  [PXInt]
  public virtual int? SunTotal { get; set; }

  /// <summary>The time spent total for Monday.</summary>
  [PXInt]
  public virtual int? MonTotal { get; set; }

  /// <summary>The time spent total for Tuesday.</summary>
  [PXInt]
  public virtual int? TueTotal { get; set; }

  /// <summary>The time spent total for Wednesday.</summary>
  [PXInt]
  public virtual int? WedTotal { get; set; }

  /// <summary>The time spent total for Thursday.</summary>
  [PXInt]
  public virtual int? ThuTotal { get; set; }

  /// <summary>The time spent total for Friday.</summary>
  [PXInt]
  public virtual int? FriTotal { get; set; }

  /// <summary>The time spent total for Saturday.</summary>
  [PXInt]
  public virtual int? SatTotal { get; set; }

  /// <summary>The time spent total for the whole week.</summary>
  [PXInt]
  [PXDependsOnFields(new System.Type[] {typeof (EPTimeCard.monTotal), typeof (EPTimeCard.tueTotal), typeof (EPTimeCard.wedTotal), typeof (EPTimeCard.thuTotal), typeof (EPTimeCard.friTotal), typeof (EPTimeCard.satTotal), typeof (EPTimeCard.sunTotal)})]
  public virtual int? WeekTotal
  {
    get
    {
      int? nullable = this.SunTotal;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.MonTotal;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.TueTotal;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      int num2 = num1 + valueOrDefault3;
      nullable = this.WedTotal;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      int num3 = num2 + valueOrDefault4;
      nullable = this.ThuTotal;
      int valueOrDefault5 = nullable.GetValueOrDefault();
      int num4 = num3 + valueOrDefault5;
      nullable = this.FriTotal;
      int valueOrDefault6 = nullable.GetValueOrDefault();
      int num5 = num4 + valueOrDefault6;
      nullable = this.SatTotal;
      int valueOrDefault7 = nullable.GetValueOrDefault();
      return new int?(num5 + valueOrDefault7);
    }
  }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>
  {
    public static EPTimeCard Find(PXGraph graph, string timeCardCD, PKFindOptions options = 0)
    {
      return (EPTimeCard) PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.FindBy(graph, (object) timeCardCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Employee</summary>
    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<EPTimeCard>.By<EPTimeCard.employeeID>
    {
    }

    /// <summary>Original/Corrected Timecard</summary>
    public class OriginalTimecard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<EPTimeCard>.By<EPTimeCard.origTimeCardCD>
    {
    }

    /// <summary>Owner</summary>
    public class OwnerContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<EPTimeCard>.By<EPTimeCard.ownerID>
    {
    }
  }

  public class Events : PXEntityEventBase<EPTimeCard>.Container<EPTimeCard.Events>
  {
    public PXEntityEvent<EPTimeCard> UpdateStatus;
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCard.timeCardCD>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.employeeID>
  {
  }

  public abstract class employeeID_CREmployee_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCard.employeeID_CREmployee_acctName>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCard.status>
  {
  }

  public abstract class origTimeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCard.origTimeCardCD>
  {
  }

  public abstract class weekId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.weekId>
  {
  }

  public abstract class isHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeCard.isHold>
  {
  }

  public abstract class isApproved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeCard.isApproved>
  {
  }

  public abstract class isRejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeCard.isRejected>
  {
  }

  public abstract class isReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeCard.isReleased>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.ownerID>
  {
  }

  public abstract class summaryLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.summaryLineCntr>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.timeSpent>
  {
  }

  public abstract class overtimeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.overtimeSpent>
  {
  }

  public abstract class timeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.timeBillable>
  {
  }

  public abstract class overtimeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.overtimeBillable>
  {
  }

  public abstract class totalTimeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.totalTimeSpent>
  {
  }

  public abstract class totalTimeBillable : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.totalTimeBillable>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCard.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPTimeCard.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCard.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCard.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeCard.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCard.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCard.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeCard.lastModifiedDateTime>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeCard.selected>
  {
  }

  public abstract class weekStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeCard.weekStartDate>
  {
  }

  public abstract class weekEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  EPTimeCard.weekEndDate>
  {
  }

  public abstract class weekDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCard.weekDescription>
  {
  }

  public abstract class weekShortDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCard.weekShortDescription>
  {
  }

  public abstract class timeSpentCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.timeSpentCalc>
  {
  }

  public abstract class overtimeSpentCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.overtimeSpentCalc>
  {
  }

  public abstract class totalSpentCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.totalSpentCalc>
  {
  }

  public abstract class timeBillableCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.timeBillableCalc>
  {
  }

  public abstract class overtimeBillableCalc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeCard.overtimeBillableCalc>
  {
  }

  public abstract class totalBillableCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.totalBillableCalc>
  {
  }

  public abstract class timecardType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCard.timecardType>
  {
  }

  public abstract class billingRateCalc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.billingRateCalc>
  {
  }

  public abstract class sunTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.sunTotal>
  {
  }

  public abstract class monTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.monTotal>
  {
  }

  public abstract class tueTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.tueTotal>
  {
  }

  public abstract class wedTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.wedTotal>
  {
  }

  public abstract class thuTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.thuTotal>
  {
  }

  public abstract class friTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.friTotal>
  {
  }

  public abstract class satTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.satTotal>
  {
  }

  public abstract class weekTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCard.weekTotal>
  {
  }
}
