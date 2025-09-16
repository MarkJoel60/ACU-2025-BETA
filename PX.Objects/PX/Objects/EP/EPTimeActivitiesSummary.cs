// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimeActivitiesSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Stores information on activities that needed approval and were associated with a time card. The information will be displayed on the Approve Time Activity Summaries (EP507030) form.
/// </summary>
[PXCacheName("Time Activities Summary")]
[Serializable]
public class EPTimeActivitiesSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [SubordinateOwnerEmployee(IsKey = true, DisplayName = "Employee")]
  [PXDefault]
  [PXParent(typeof (Select<EPWeeklyCrewTimeActivity, Where<EPWeeklyCrewTimeActivity.workgroupID, Equal<Current<EPTimeActivitiesSummary.workgroupID>>, And<EPWeeklyCrewTimeActivity.week, Equal<Current<EPTimeActivitiesSummary.week>>>>>), ParentCreate = true, LeaveChildren = true)]
  public virtual int? ContactID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Workgroup", Enabled = false)]
  [PXDefault(typeof (EPWeeklyCrewTimeActivity.workgroupID))]
  [PXWorkgroupSelector]
  public virtual int? WorkgroupID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Week", Enabled = false)]
  [PXDefault(typeof (EPWeeklyCrewTimeActivity.week))]
  [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
  public virtual int? Week { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Monday", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? MondayTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Tuesday", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? TuesdayTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Wednesday", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? WednesdayTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Thursday", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? ThursdayTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Friday", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? FridayTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Saturday", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? SaturdayTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sunday", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? SundayTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Total Regular Time", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? TotalRegularTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Total Billable Time", Enabled = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? TotalBillableTime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Total Overtime", Enabled = false, Visible = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? TotalOvertime { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Total Billable Overtime", Enabled = false, Visible = false)]
  [PXTimeList]
  [PXDefault(0)]
  public virtual int? TotalBillableOvertime { get; set; }

  [PXString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [PXDBScalar(typeof (SearchFor<EPCompanyTreeMember.membershipType>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPCompanyTreeMember.workGroupID, Equal<EPTimeActivitiesSummary.workgroupID>>>>>.And<BqlOperand<EPCompanyTreeMember.contactID, IBqlInt>.IsEqual<EPTimeActivitiesSummary.contactID>>>))]
  [WorkgroupMemberStatus]
  public virtual 
  #nullable disable
  string Status { get; set; }

  [PXBool]
  [PXDBScalar(typeof (SearchFor<EPCompanyTreeMember.active>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPCompanyTreeMember.workGroupID, Equal<EPTimeActivitiesSummary.workgroupID>>>>>.And<BqlOperand<EPCompanyTreeMember.contactID, IBqlInt>.IsEqual<EPTimeActivitiesSummary.contactID>>>))]
  public virtual bool? IsMemberActive { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Employee Status")]
  [VendorStatus.List]
  [PXDBScalar(typeof (SearchFor<PX.Objects.CR.BAccount.vStatus>.Where<BqlOperand<PX.Objects.CR.BAccount.defContactID, IBqlInt>.IsEqual<EPTimeActivitiesSummary.contactID>>))]
  public virtual string EmployeeStatus { get; set; }

  [PXBool]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPTimeActivitiesSummary.totalRegularTime, NotEqual<Zero>>>>>.Or<BqlOperand<EPTimeActivitiesSummary.totalOvertime, IBqlInt>.IsNotEqual<Zero>>>.Else<True>))]
  public virtual bool? IsWithoutActivities { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeActivitiesSummary.selected>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeActivitiesSummary.contactID>
  {
  }

  public abstract class workgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.workgroupID>
  {
  }

  public abstract class week : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeActivitiesSummary.week>
  {
  }

  public abstract class mondayTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeActivitiesSummary.mondayTime>
  {
  }

  public abstract class tuesdayTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.tuesdayTime>
  {
  }

  public abstract class wednesdayTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.wednesdayTime>
  {
  }

  public abstract class thursdayTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.thursdayTime>
  {
  }

  public abstract class fridayTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeActivitiesSummary.fridayTime>
  {
  }

  public abstract class saturdayTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.saturdayTime>
  {
  }

  public abstract class sundayTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeActivitiesSummary.sundayTime>
  {
  }

  public abstract class totalRegularTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.totalRegularTime>
  {
  }

  public abstract class totalBillableTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.totalBillableTime>
  {
  }

  public abstract class totalOvertime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.totalOvertime>
  {
  }

  public abstract class totalBillableOvertime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPTimeActivitiesSummary.totalBillableOvertime>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeActivitiesSummary.status>
  {
  }

  public abstract class isMemberActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPTimeActivitiesSummary.isMemberActive>
  {
  }

  public abstract class employeeStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeActivitiesSummary.employeeStatus>
  {
  }

  public abstract class isWithoutActivities : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPTimeActivitiesSummary.isWithoutActivities>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPTimeActivitiesSummary.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeActivitiesSummary.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeActivitiesSummary.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPTimeActivitiesSummary.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeActivitiesSummary.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeActivitiesSummary.lastModifiedDateTime>
  {
  }
}
