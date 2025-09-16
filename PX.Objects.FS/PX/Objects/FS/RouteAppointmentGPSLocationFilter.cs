// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteAppointmentGPSLocationFilter
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
public class RouteAppointmentGPSLocationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [Service(null, DisplayName = "Service")]
  public virtual int? ServiceID { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DateFrom { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? DateTo { get; set; }

  [PXDBInt]
  [PXUIField]
  [FSSelectorBAccountTypeCustomerOrCombined]
  public virtual int? CustomerID { get; set; }

  [PXDBInt]
  [PXFormula(typeof (Default<RouteAppointmentGPSLocationFilter.customerID>))]
  [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RouteAppointmentGPSLocationFilter.customerID>>>>), SubstituteKey = typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField(DisplayName = "Customer Location")]
  public virtual int? CustomerLocationID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Route")]
  [FSSelectorRouteID]
  public virtual int? RouteID { get; set; }

  [PXInt]
  [PXFormula(typeof (Default<RouteAppointmentGPSLocationFilter.routeID>))]
  [PXSelector(typeof (Search<FSRouteDocument.routeDocumentID, Where<FSRouteDocument.routeID, Equal<Current<RouteAppointmentGPSLocationFilter.routeID>>>>), SubstituteKey = typeof (FSRouteDocument.refNbr))]
  [PXUIField(DisplayName = "Route Nbr.")]
  public virtual int? RouteDocumentID { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generate", Visible = false)]
  public virtual bool? LoadData { get; set; }

  public abstract class serviceID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.serviceID>
  {
  }

  public abstract class dateFrom : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.dateFrom>
  {
  }

  public abstract class dateTo : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.dateTo>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.customerLocationID>
  {
  }

  public abstract class routeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.routeID>
  {
  }

  public abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.routeDocumentID>
  {
  }

  public abstract class loadData : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RouteAppointmentGPSLocationFilter.loadData>
  {
  }
}
