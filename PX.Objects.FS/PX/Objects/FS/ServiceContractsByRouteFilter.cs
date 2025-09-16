// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractsByRouteFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class ServiceContractsByRouteFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXDefault]
  [PXUIField(DisplayName = "Route")]
  [FSSelectorRouteID]
  public virtual int? RouteID { get; set; }

  [PXString(2, IsFixed = true)]
  [PXDefault("NT")]
  [PXUIField(DisplayName = "Weekday")]
  [ListField_WeekDays.ListAtrribute]
  public virtual 
  #nullable disable
  string WeekDay { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display active Service Contracts only")]
  public virtual bool? ServiceContractFlag { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Display active Schedules only")]
  public virtual bool? ScheduleFlag { get; set; }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceContractsByRouteFilter.routeID>
  {
  }

  public abstract class weekDay : ListField_WeekDays
  {
  }

  public abstract class serviceContractFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceContractsByRouteFilter.serviceContractFlag>
  {
  }

  public abstract class scheduleFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ServiceContractsByRouteFilter.scheduleFlag>
  {
  }
}
