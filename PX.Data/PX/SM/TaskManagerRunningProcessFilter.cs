// Decompiled with JetBrains decompiler
// Type: PX.SM.TaskManagerRunningProcessFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.SM;

[PXHidden]
[Serializable]
public class TaskManagerRunningProcessFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool(IsKey = false)]
  [PXUIField(DisplayName = "Show All Users")]
  public bool? ShowAllUsers { get; set; }

  [PXInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Login Type")]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"All", "User Interface", "API"})]
  public virtual int? LoginType { get; set; }

  [PXUIField(DisplayName = "Managed Memory, Mb", Enabled = false)]
  [PXLong]
  public long? GCTotalMemory => new long?(GC.GetTotalMemory(false) / 1000000L);

  [PXUIField(DisplayName = "GC Collections", Enabled = false)]
  [PXString]
  public 
  #nullable disable
  string GCCollection => $"{GC.CollectionCount(0)}/{GC.CollectionCount(1)}/{GC.CollectionCount(2)}";

  [PXUIField(DisplayName = "Working Set, Mb", Enabled = false)]
  [PXLong]
  public long? WorkingSet => new long?(Process.GetCurrentProcess().WorkingSet64 / 1000000L);

  [PXUIField(DisplayName = "Current Utilization", Enabled = false)]
  [PXString]
  public string CurrentUtilization => PXPerformanceMonitor.CPUUtilization.ToString() + "%";

  [PXUIField(DisplayName = "Up Time", Enabled = false)]
  [PXString]
  public string UpTime
  {
    get => string.Format("{0:%d}d {0:%h}h {0:%m}min", (object) PXPerformanceMonitor.GetUpTime());
  }

  [PXUIField(DisplayName = "Active Requests", Enabled = false)]
  [PXInt]
  public int? ActiveRequests => new int?(PXPerformanceMonitor.GetCurrentWorkingThreadsCount());

  [PXUIField(DisplayName = "Requests For Last Minute", Enabled = false)]
  [PXInt]
  public int? RequestsSumLastMinute => new int?(PXPerformanceMonitor.RequestsSumLastMinute);

  [PXStringList(new string[] {"All", "ActiveDirectory", "BusinessEvents", "Commerce", "Customization", "DataConsistency", "Email", "License", "PushNotifications", "ResourceGovernor", "Scheduler", "System"}, new string[] {"All", "ActiveDirectory", "BusinessEvents", "Commerce", "Customization", "DataConsistency", "Email", "License", "PushNotifications", "ResourceGovernor", "Scheduler", "System"}, MultiSelect = true)]
  [PXUIField(DisplayName = "Source")]
  [PXString]
  [PXDefault("All")]
  public string Source { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Level")]
  [PXIntList(new int[] {2, 3, 4, 5}, new string[] {"Information", "Warning", "Error", "Fatal"})]
  [PXDefault(2)]
  public int? Level { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "From")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public System.DateTime? FromDate { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "To")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public System.DateTime? ToDate { get; set; }

  public abstract class showAllUsers : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    TaskManagerRunningProcessFilter.showAllUsers>
  {
  }

  public abstract class loginType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaskManagerRunningProcessFilter.loginType>
  {
  }

  public abstract class source : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaskManagerRunningProcessFilter.source>
  {
  }

  public abstract class level : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaskManagerRunningProcessFilter.level>
  {
  }

  public abstract class fromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TaskManagerRunningProcessFilter.fromDate>
  {
  }

  public abstract class toDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TaskManagerRunningProcessFilter.toDate>
  {
  }
}
