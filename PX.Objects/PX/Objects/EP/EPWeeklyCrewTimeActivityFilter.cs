// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPWeeklyCrewTimeActivityFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Weekly Crew Time Activity Filter")]
[Serializable]
public class EPWeeklyCrewTimeActivityFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [EPTimeCardProject]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (EPWeeklyCrewTimeActivityFilter.projectID))]
  public virtual int? ProjectTaskID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Day")]
  [DayOfWeek]
  public virtual int? Day { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Time", Enabled = false)]
  [PXUnboundDefault(0)]
  public virtual int? RegularTime { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Overtime", Enabled = false)]
  [PXUnboundDefault(0)]
  public virtual int? Overtime { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Total Time", Enabled = false)]
  [PXUnboundDefault(0)]
  [PXDependsOnFields(new Type[] {typeof (EPWeeklyCrewTimeActivityFilter.regularTime), typeof (EPWeeklyCrewTimeActivityFilter.overtime)})]
  public virtual int? TotalTime
  {
    get
    {
      int? nullable = this.RegularTime;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.Overtime;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      return new int?(valueOrDefault1 + valueOrDefault2);
    }
  }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable", Enabled = false)]
  [PXUnboundDefault(0)]
  public virtual int? BillableTime { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Billable Overtime", Enabled = false)]
  [PXUnboundDefault(0)]
  public virtual int? BillableOvertime { get; set; }

  [PXInt]
  [PXTimeList]
  [PXUIField(DisplayName = "Total Billable Time", Enabled = false)]
  [PXUnboundDefault(0)]
  [PXDependsOnFields(new Type[] {typeof (EPWeeklyCrewTimeActivityFilter.billableTime), typeof (EPWeeklyCrewTimeActivityFilter.billableOvertime)})]
  public virtual int? TotalBillableTime
  {
    get
    {
      int? nullable = this.BillableTime;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.BillableOvertime;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      return new int?(valueOrDefault1 + valueOrDefault2);
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "Workgroup Members", Enabled = false)]
  public virtual int? TotalWorkgroupMembers { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Members with Activities", Enabled = false)]
  public virtual int? TotalWorkgroupMembersWithActivities { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show All Members")]
  [PXUnboundDefault(false)]
  public virtual bool? ShowAllMembers { get; set; }

  public abstract class projectID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.projectTaskID>
  {
  }

  public abstract class day : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPWeeklyCrewTimeActivityFilter.day>
  {
  }

  public abstract class regularTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.regularTime>
  {
  }

  public abstract class overtime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.overtime>
  {
  }

  public abstract class totalTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.totalTime>
  {
  }

  public abstract class billableTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.billableTime>
  {
  }

  public abstract class billableOvertime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.billableOvertime>
  {
  }

  public abstract class totalBillableTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.totalBillableTime>
  {
  }

  public abstract class totalWorkgroupMembers : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.totalWorkgroupMembers>
  {
  }

  public abstract class totalWorkgroupMembersWithActivities : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.totalWorkgroupMembersWithActivities>
  {
  }

  public abstract class showAllMembers : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EPWeeklyCrewTimeActivityFilter.showAllMembers>
  {
  }
}
