// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteAppointmentForecastingFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class RouteAppointmentForecastingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Service ID")]
  [Service(null)]
  public virtual int? ServiceID { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DateBegin { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DateEnd { get; set; }

  [PXDBInt]
  [PXUIField]
  [FSSelectorBAccountTypeCustomerOrCombined]
  public virtual int? CustomerID { get; set; }

  [PXDBInt]
  [PXFormula(typeof (Default<RouteAppointmentForecastingFilter.customerID>))]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RouteAppointmentForecastingFilter.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField(DisplayName = "Customer Location")]
  public virtual int? CustomerLocationID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Route ID")]
  [FSSelectorRouteID]
  public virtual int? RouteID { get; set; }

  public abstract class serviceID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentForecastingFilter.serviceID>
  {
  }

  public abstract class dateBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteAppointmentForecastingFilter.dateBegin>
  {
  }

  public abstract class dateEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteAppointmentForecastingFilter.dateEnd>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentForecastingFilter.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentForecastingFilter.customerLocationID>
  {
  }

  public abstract class routeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentForecastingFilter.routeID>
  {
  }
}
