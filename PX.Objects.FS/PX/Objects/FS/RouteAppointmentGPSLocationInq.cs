// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteAppointmentGPSLocationInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.FS;

public class RouteAppointmentGPSLocationInq : PXGraph<
#nullable disable
RouteAppointmentGPSLocationInq>
{
  public PXCancel<RouteAppointmentGPSLocationFilter> Cancel;
  public PXFilter<RouteAppointmentGPSLocationFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoinGroupBy<RouteAppointmentGPSLocationInq.AppointmentData, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.appointmentID, Equal<FSAppointmentInRoute.appointmentID>>>, Where2<Where<Current<RouteAppointmentGPSLocationFilter.loadData>, Equal<True>, And<RouteAppointmentGPSLocationInq.AppointmentData.routeDocumentID, IsNotNull, And<Where<FSAppointmentInRoute.closed, Equal<True>, Or<FSAppointmentInRoute.completed, Equal<True>>>>>>, And2<Where<Current<RouteAppointmentGPSLocationFilter.dateFrom>, IsNull, Or<FSAppointment.scheduledDateTimeBegin, GreaterEqual<Current<RouteAppointmentGPSLocationFilter.dateFrom>>>>, And2<Where<Current<RouteAppointmentGPSLocationFilter.dateTo>, IsNull, Or<FSAppointment.scheduledDateTimeEnd, LessEqual<Current<RouteAppointmentGPSLocationFilter.dateTo>>>>, And2<Where<Current<RouteAppointmentGPSLocationFilter.customerID>, IsNull, Or<RouteAppointmentGPSLocationInq.AppointmentData.customerID, Equal<Current<RouteAppointmentGPSLocationFilter.customerID>>>>, And2<Where<Current<RouteAppointmentGPSLocationFilter.customerLocationID>, IsNull, Or<RouteAppointmentGPSLocationInq.AppointmentData.locationID, Equal<Current<RouteAppointmentGPSLocationFilter.customerLocationID>>>>, And2<Where<Current<RouteAppointmentGPSLocationFilter.serviceID>, IsNull, Or<FSAppointmentDet.inventoryID, Equal<Current<RouteAppointmentGPSLocationFilter.serviceID>>>>, And2<Where<Current<RouteAppointmentGPSLocationFilter.routeDocumentID>, IsNull, Or<RouteAppointmentGPSLocationInq.AppointmentData.routeDocumentID, Equal<Current<RouteAppointmentGPSLocationFilter.routeDocumentID>>>>, And<Where<Current<RouteAppointmentGPSLocationFilter.routeID>, IsNull, Or<RouteAppointmentGPSLocationInq.AppointmentData.routeID, Equal<Current<RouteAppointmentGPSLocationFilter.routeID>>>>>>>>>>>>, Aggregate<GroupBy<FSAppointmentInRoute.appointmentID>>, OrderBy<Asc<RouteAppointmentGPSLocationInq.AppointmentData.routeID, Asc<FSAppointment.scheduledDateTimeBegin>>>> RouteAppointmentGPSLocationRecords;
  public PXAction<RouteAppointmentGPSLocationFilter> filterManually;
  public PXAction<RouteAppointmentGPSLocationFilter> report;
  public PXAction<RouteAppointmentGPSLocationFilter> openLocationScreen;
  public PXAction<RouteAppointmentGPSLocationFilter> editAppointment;

  public RouteAppointmentGPSLocationInq()
  {
    ((PXSelectBase) this.RouteAppointmentGPSLocationRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.RouteAppointmentGPSLocationRecords).Cache.AllowUpdate = false;
    ((PXSelectBase) this.RouteAppointmentGPSLocationRecords).Cache.AllowDelete = false;
  }

  [PXUIField(DisplayName = "Generate")]
  public virtual IEnumerable FilterManually(PXAdapter adapter)
  {
    ((PXSelectBase<RouteAppointmentGPSLocationFilter>) this.Filter).Current.LoadData = new bool?(true);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  protected virtual void Report()
  {
    PXReportResultset pxReportResultset = new PXReportResultset(new System.Type[1]
    {
      typeof (RouteAppointmentGPSLocationInq.AppointmentData)
    });
    foreach (PXResult<RouteAppointmentGPSLocationInq.AppointmentData> pxResult in ((PXSelectBase<RouteAppointmentGPSLocationInq.AppointmentData>) this.RouteAppointmentGPSLocationRecords).Select(Array.Empty<object>()))
    {
      RouteAppointmentGPSLocationInq.AppointmentData appointmentData = PXResult<RouteAppointmentGPSLocationInq.AppointmentData>.op_Implicit(pxResult);
      pxReportResultset.Add(new object[1]
      {
        (object) appointmentData
      });
    }
    throw new PXReportRequiredException((IPXResultset) pxReportResultset, "FS643000", (PXBaseRedirectException.WindowMode) 3, nameof (Report), (CurrentLocalization) null);
  }

  [PXButton]
  [PXUIField]
  protected virtual void OpenLocationScreen()
  {
    if (((PXSelectBase<RouteAppointmentGPSLocationInq.AppointmentData>) this.RouteAppointmentGPSLocationRecords).Current != null)
    {
      CustomerLocationMaint instance = PXGraph.CreateInstance<CustomerLocationMaint>();
      PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<RouteAppointmentGPSLocationInq.AppointmentData>) this.RouteAppointmentGPSLocationRecords).Current.CustomerID
      }));
      ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Search<PX.Objects.CR.Location.locationID>((object) ((PXSelectBase<RouteAppointmentGPSLocationInq.AppointmentData>) this.RouteAppointmentGPSLocationRecords).Current.LocationID, new object[1]
      {
        (object) baccount.AcctCD
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable EditAppointment(PXAdapter adapter)
  {
    if (((PXSelectBase<RouteAppointmentGPSLocationInq.AppointmentData>) this.RouteAppointmentGPSLocationRecords).Current == null)
      return (IEnumerable) ((PXSelectBase<RouteAppointmentGPSLocationFilter>) this.Filter).Select(Array.Empty<object>());
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) ((PXSelectBase<RouteAppointmentGPSLocationInq.AppointmentData>) this.RouteAppointmentGPSLocationRecords).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<RouteAppointmentGPSLocationInq.AppointmentData>) this.RouteAppointmentGPSLocationRecords).Current.SrvOrdType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<RouteAppointmentGPSLocationFilter, RouteAppointmentGPSLocationFilter.dateFrom> e)
  {
    if (e.Row == null)
      return;
    DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<RouteAppointmentGPSLocationFilter, RouteAppointmentGPSLocationFilter.dateFrom>, RouteAppointmentGPSLocationFilter, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate.Value;
  }

  protected virtual void _(
    PX.Data.Events.RowSelecting<RouteAppointmentGPSLocationFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<RouteAppointmentGPSLocationFilter> e)
  {
    if (e.Row == null || ((PXSelectBase<RouteAppointmentGPSLocationFilter>) this.Filter).Current == null || !((PXSelectBase<RouteAppointmentGPSLocationFilter>) this.Filter).Current.LoadData.HasValue)
      return;
    ((PXAction) this.report).SetEnabled(((PXSelectBase<RouteAppointmentGPSLocationFilter>) this.Filter).Current.LoadData.Value);
  }

  protected virtual void _(
    PX.Data.Events.RowInserting<RouteAppointmentGPSLocationFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowInserted<RouteAppointmentGPSLocationFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowUpdating<RouteAppointmentGPSLocationFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<RouteAppointmentGPSLocationFilter> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase<RouteAppointmentGPSLocationFilter>) this.Filter).Current.LoadData = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.RowDeleting<RouteAppointmentGPSLocationFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowDeleted<RouteAppointmentGPSLocationFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<RouteAppointmentGPSLocationFilter> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowPersisted<RouteAppointmentGPSLocationFilter> e)
  {
  }

  [Serializable]
  public class AppointmentData : FSAppointmentInRoute
  {
    [PXDBInt]
    [PXUIField(DisplayName = "Route", Visible = false)]
    [FSSelectorRouteID]
    public override int? RouteID { get; set; }

    [PXDBInt]
    [PXSelector(typeof (Search<FSRouteDocument.routeDocumentID>), SubstituteKey = typeof (FSRouteDocument.refNbr))]
    [PXUIField(DisplayName = "Route Nbr.", Visible = false)]
    public override int? RouteDocumentID { get; set; }

    [PXDBInt(BqlField = typeof (FSServiceOrder.customerID))]
    [PXUIField(DisplayName = "Customer")]
    [FSSelectorCustomer]
    public override int? CustomerID { get; set; }

    [PXDBInt(BqlField = typeof (FSServiceOrder.locationID))]
    [PXUIField(DisplayName = "Location")]
    [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID>), SubstituteKey = typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
    public override int? LocationID { get; set; }

    [PXString(50, IsUnicode = true)]
    [PXUIField(DisplayName = "Address")]
    public virtual string Address
    {
      get
      {
        return SharedFunctions.GetAddressForGeolocation(this.PostalCode, this.AddressLine1, this.AddressLine2, this.City, this.State, string.Empty);
      }
    }

    [PXString(250, IsUnicode = true)]
    [PXUIField(DisplayName = "GPS Start Coordinate")]
    public virtual string GPSStartCoordinate
    {
      get => SharedFunctions.GetCompleteCoordinate(this.GPSLongitudeStart, this.GPSLatitudeStart);
    }

    [PXString(250, IsUnicode = true)]
    [PXUIField(DisplayName = "GPS Start Address")]
    public virtual string GPSStartAddress
    {
      get
      {
        return !this.GPSLatitudeStart.HasValue || !this.GPSLongitudeStart.HasValue ? string.Empty : Geocoder.ReverseGeocode(new LatLng((double) this.GPSLatitudeStart.Value, (double) this.GPSLongitudeStart.Value), this.MapApiKey);
      }
    }

    [PXString(250, IsUnicode = true)]
    [PXUIField(DisplayName = "GPS Complete Coordinate")]
    public virtual string GPSCompleteCoordinate
    {
      get
      {
        return SharedFunctions.GetCompleteCoordinate(this.GPSLongitudeComplete, this.GPSLatitudeComplete);
      }
    }

    [PXString(250, IsUnicode = true)]
    [PXUIField(DisplayName = "GPS Complete Address")]
    public virtual string GPSCompleteAddress
    {
      get
      {
        return !this.GPSLatitudeComplete.HasValue || !this.GPSLongitudeComplete.HasValue ? string.Empty : Geocoder.ReverseGeocode(new LatLng((double) this.GPSLatitudeComplete.Value, (double) this.GPSLongitudeComplete.Value), this.MapApiKey);
      }
    }

    public new abstract class routeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.routeID>
    {
    }

    public new abstract class routeDocumentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.routeDocumentID>
    {
    }

    public new abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.customerID>
    {
    }

    public new abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.locationID>
    {
    }

    public abstract class address : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.address>
    {
    }

    public abstract class gPSStartCoordinate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.gPSStartCoordinate>
    {
    }

    public abstract class gPSStartAddress : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.gPSStartAddress>
    {
    }

    public abstract class gPSCompleteCoordinate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.gPSCompleteCoordinate>
    {
    }

    public abstract class gPSCompleteAddress : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RouteAppointmentGPSLocationInq.AppointmentData.gPSCompleteAddress>
    {
    }
  }
}
