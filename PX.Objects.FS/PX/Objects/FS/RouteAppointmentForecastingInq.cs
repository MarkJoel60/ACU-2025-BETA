// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteAppointmentForecastingInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class RouteAppointmentForecastingInq : PXGraph<RouteAppointmentForecastingInq>
{
  public PXCancel<RouteAppointmentForecastingFilter> Cancel;
  public PXAction<RouteAppointmentForecastingFilter> OpenServiceContractScreen;
  public PXAction<RouteAppointmentForecastingFilter> OpenScheduleScreen;
  public PXAction<RouteAppointmentForecastingFilter> OpenLocationScreen;
  public PXAction<RouteAppointmentForecastingFilter> generateProjection;
  public PXFilter<RouteAppointmentForecastingFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoinGroupBy<FSRouteAppointmentForecasting, LeftJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSRouteAppointmentForecasting.serviceContractID>>, LeftJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSRouteAppointmentForecasting.scheduleID>>, LeftJoin<FSScheduleDet, On<FSScheduleDet.scheduleID, Equal<FSRouteAppointmentForecasting.scheduleID>>>>>, Where2<Where<Current2<RouteAppointmentForecastingFilter.dateBegin>, IsNull, Or<FSRouteAppointmentForecasting.startDate, GreaterEqual<Current2<RouteAppointmentForecastingFilter.dateBegin>>>>, And2<Where<Current2<RouteAppointmentForecastingFilter.dateEnd>, IsNull, Or<FSRouteAppointmentForecasting.startDate, LessEqual<Current2<RouteAppointmentForecastingFilter.dateEnd>>>>, And2<Where<Current2<RouteAppointmentForecastingFilter.customerID>, IsNull, Or<FSRouteAppointmentForecasting.customerID, Equal<Current2<RouteAppointmentForecastingFilter.customerID>>>>, And2<Where<Current2<RouteAppointmentForecastingFilter.customerLocationID>, IsNull, Or<FSRouteAppointmentForecasting.customerLocationID, Equal<Current2<RouteAppointmentForecastingFilter.customerLocationID>>>>, And2<Where<Current2<RouteAppointmentForecastingFilter.routeID>, IsNull, Or<FSRouteAppointmentForecasting.routeID, Equal<Current2<RouteAppointmentForecastingFilter.routeID>>>>, And<Where<Current2<RouteAppointmentForecastingFilter.serviceID>, IsNull, Or<FSScheduleDet.inventoryID, Equal<Current2<RouteAppointmentForecastingFilter.serviceID>>>>>>>>>>, Aggregate<GroupBy<FSRouteAppointmentForecasting.startDate, GroupBy<FSRouteAppointmentForecasting.scheduleID>>>, OrderBy<Asc<FSRouteAppointmentForecasting.startDate, Asc<FSRouteAppointmentForecasting.sequenceOrder>>>> RouteAppointmentForecastingRecords;
  public PXSelect<FSSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>> ScheduleSelected;
  public PXSelect<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>>> ServiceContractSelected;
  public PXSelect<FSScheduleRoute, Where<FSScheduleRoute.scheduleID, Equal<Required<FSScheduleRoute.scheduleID>>>> ScheduleRouteSelected;

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual void openServiceContractScreen()
  {
    if (((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current != null)
    {
      RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
      ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = FSServiceContract.PK.Find((PXGraph) this, ((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current.ServiceContractID);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual void openScheduleScreen()
  {
    if (((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current != null)
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      FSRouteContractSchedule contractSchedule = PXResultset<FSRouteContractSchedule>.op_Implicit(PXSelectBase<FSRouteContractSchedule, PXSelect<FSRouteContractSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>, And<FSRouteContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>, And<FSRouteContractSchedule.entityID, Equal<Required<FSRouteContractSchedule.entityID>>, And<FSRouteContractSchedule.customerID, Equal<Required<FSRouteContractSchedule.customerID>>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) ((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current.ScheduleID,
        (object) ((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current.ServiceContractID,
        (object) ((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current.CustomerID
      }));
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = contractSchedule;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual void openLocationScreen()
  {
    if (((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current != null)
    {
      CustomerLocationMaint instance = PXGraph.CreateInstance<CustomerLocationMaint>();
      PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current.CustomerID
      }));
      ((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Current = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) instance.Location).Search<PX.Objects.CR.Location.locationID>((object) ((PXSelectBase<FSRouteAppointmentForecasting>) this.RouteAppointmentForecastingRecords).Current.CustomerLocationID, new object[1]
      {
        (object) baccount.AcctCD
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable GenerateProjection(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RouteAppointmentForecastingInq.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new RouteAppointmentForecastingInq.\u003C\u003Ec__DisplayClass8_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.\u003C\u003E4__this = this;
    if (((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current != null)
    {
      DateTime? nullable = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateBegin;
      if (nullable.HasValue)
      {
        nullable = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateEnd;
        if (nullable.HasValue)
        {
          nullable = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateBegin;
          DateTime? dateEnd = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateEnd;
          if ((nullable.HasValue & dateEnd.HasValue ? (nullable.GetValueOrDefault() < dateEnd.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            // ISSUE: reference to a compiler-generated field
            cDisplayClass80.beginFromFilter = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateBegin.Value;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass80.endToFilter = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateEnd.Value;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass80.recordType = "IRSC";
            // ISSUE: reference to a compiler-generated field
            cDisplayClass80.serviceID = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.ServiceID;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass80.customerID = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.CustomerID;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass80.customerLocationID = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.CustomerLocationID;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass80.routeID = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.RouteID;
            // ISSUE: method pointer
            PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass80, __methodptr(\u003CGenerateProjection\u003Eb__0)));
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Schedule ID")]
  protected virtual void FSSchedule_RefNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<RouteAppointmentForecastingFilter> e)
  {
    if (e.Row == null)
      return;
    PXAction<RouteAppointmentForecastingFilter> generateProjection = this.generateProjection;
    DateTime? nullable = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateBegin;
    int num;
    if (nullable.HasValue)
    {
      nullable = ((PXSelectBase<RouteAppointmentForecastingFilter>) this.Filter).Current.DateEnd;
      num = nullable.HasValue ? 1 : 0;
    }
    else
      num = 0;
    ((PXAction) generateProjection).SetEnabled(num != 0);
  }
}
