// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRouteAppointmentForecasting
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (RouteAppointmentForecastingInq))]
[Serializable]
public class FSRouteAppointmentForecasting : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<FSSchedule.scheduleID, Where<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSSchedule.entityID, Equal<Current<FSRouteAppointmentForecasting.serviceContractID>>>>>))]
  [PXUIField(Enabled = false)]
  public virtual int? ScheduleID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXUIField]
  public virtual DateTime? StartDate { get; set; }

  [PXDBInt]
  [FSSelectorRouteID]
  [PXUIField(DisplayName = "Route ID")]
  public virtual int? RouteID { get; set; }

  [PXDBInt]
  [PXUIField]
  [FSSelectorCustomer]
  public virtual int? CustomerID { get; set; }

  [LocationID(DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<FSRouteAppointmentForecasting.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  [PXUIField(DisplayName = "Service Contract ID", Enabled = false)]
  public virtual int? ServiceContractID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sequence Order")]
  public virtual int? SequenceOrder { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    FSRouteAppointmentForecasting>.By<FSRouteAppointmentForecasting.scheduleID, FSRouteAppointmentForecasting.startDate>
  {
    public static FSRouteAppointmentForecasting Find(
      PXGraph graph,
      int? scheduleID,
      DateTime? startDate,
      PKFindOptions options = 0)
    {
      return (FSRouteAppointmentForecasting) PrimaryKeyOf<FSRouteAppointmentForecasting>.By<FSRouteAppointmentForecasting.scheduleID, FSRouteAppointmentForecasting.startDate>.FindBy(graph, (object) scheduleID, (object) startDate, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSRouteAppointmentForecasting>.By<FSRouteAppointmentForecasting.scheduleID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSRouteAppointmentForecasting>.By<FSRouteAppointmentForecasting.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSRouteAppointmentForecasting>.By<FSRouteAppointmentForecasting.customerID, FSRouteAppointmentForecasting.customerLocationID>
    {
    }

    public class Route : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSRouteAppointmentForecasting>.By<FSRouteAppointmentForecasting.routeID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSRouteAppointmentForecasting>.By<FSRouteAppointmentForecasting.serviceContractID>
    {
    }
  }

  public abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteAppointmentForecasting.scheduleID>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteAppointmentForecasting.startDate>
  {
  }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteAppointmentForecasting.routeID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteAppointmentForecasting.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteAppointmentForecasting.customerLocationID>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteAppointmentForecasting.serviceContractID>
  {
  }

  public abstract class sequenceOrder : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteAppointmentForecasting.sequenceOrder>
  {
  }
}
