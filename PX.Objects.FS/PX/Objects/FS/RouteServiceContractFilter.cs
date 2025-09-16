// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteServiceContractFilter
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
public class RouteServiceContractFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Route")]
  [FSSelectorRouteID]
  [PXFormula(typeof (Default<RouteServiceContractFilter.vehicleTypeID>))]
  public virtual int? RouteID { get; set; }

  [PXDateAndTime(UseTimeZone = false)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? FromDate { get; set; }

  [PXDateAndTime(UseTimeZone = false)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ToDate { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Vehicle Type")]
  [PXSelector(typeof (FSVehicleType.vehicleTypeID), SubstituteKey = typeof (FSVehicleType.vehicleTypeCD), DescriptionField = typeof (FSVehicleType.descr))]
  public virtual int? VehicleTypeID { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Preassign Driver")]
  public virtual bool? PreassignedDriver { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Preassign Vehicle")]
  public virtual bool? PreassignedVehicle { get; set; }

  [PXInt]
  public virtual int? ScheduleID { get; set; }

  public abstract class routeID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  RouteServiceContractFilter.routeID>
  {
  }

  public abstract class fromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteServiceContractFilter.fromDate>
  {
  }

  public abstract class toDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteServiceContractFilter.toDate>
  {
  }

  public abstract class vehicleTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteServiceContractFilter.vehicleTypeID>
  {
  }

  public abstract class preassignedDriver : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RouteServiceContractFilter.preassignedDriver>
  {
  }

  public abstract class preassignedVehicle : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RouteServiceContractFilter.preassignedVehicle>
  {
  }

  public abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteServiceContractFilter.scheduleID>
  {
  }
}
