// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteWrkSheetInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.FS;

public class RouteWrkSheetInq : PXGraph<
#nullable disable
RouteWrkSheetInq>
{
  private RouteDocumentMaint routeDocumentMaint;
  public PXFilter<RouteWrkSheetInq.RouteWrkSheetFilter> Filter;
  public PXCancel<RouteWrkSheetInq.RouteWrkSheetFilter> Cancel;
  public PXSelect<EPEmployee, Where<FSxEPEmployee.sDEnabled, Equal<True>, And<FSxEPEmployee.isDriver, Equal<True>, And<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>>> currentEmployee;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRouteDocument.routeID, Equal<FSRoute.routeID>>>, Where2<Where<Current2<RouteWrkSheetInq.RouteWrkSheetFilter.driverID>, IsNull, Or<FSRouteDocument.driverID, Equal<Current2<RouteWrkSheetInq.RouteWrkSheetFilter.driverID>>, Or<FSRouteDocument.additionalDriverID, Equal<Current2<RouteWrkSheetInq.RouteWrkSheetFilter.driverID>>>>>, And2<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>>>, And2<Where<Current2<RouteWrkSheetInq.RouteWrkSheetFilter.fromDate>, IsNull, Or<FSRouteDocument.date, GreaterEqual<Current2<RouteWrkSheetInq.RouteWrkSheetFilter.fromDate>>>>, And<Where<Current2<RouteWrkSheetInq.RouteWrkSheetFilter.toDate>, IsNull, Or<FSRouteDocument.date, LessEqual<Current2<RouteWrkSheetInq.RouteWrkSheetFilter.toDate>>>>>>>>, OrderBy<Asc<FSRouteDocument.date, Asc<FSRouteDocument.timeBegin>>>> Routes;
  public SharedClasses.RouteSelected_view VehicleRouteSelected;
  public VehicleSelectionHelper.VehicleRecords_View VehicleRecords;
  public PXFilter<VehicleSelectionFilter> VehicleFilter;
  public SharedClasses.RouteSelected_view DriverRouteSelected;
  public DriverSelectionHelper.DriverRecords_View DriverRecords;
  public PXFilter<DriverSelectionFilter> DriverFilter;
  public PXAction<RouteWrkSheetInq.RouteWrkSheetFilter> OpenRouteDocument;
  public PXAction<RouteWrkSheetInq.RouteWrkSheetFilter> openDriverSelector;
  public PXAction<RouteWrkSheetInq.RouteWrkSheetFilter> openVehicleSelector;
  public PXAction<RouteWrkSheetInq.RouteWrkSheetFilter> CreateNew;
  public PXAction<RouteWrkSheetInq.RouteWrkSheetFilter> EditDetail;

  public RouteWrkSheetInq()
  {
    ((PXSelectBase) this.Routes).Cache.AllowInsert = false;
    if (this.routeDocumentMaint == null)
      return;
    ((PXAction) this.CreateNew).SetEnabled(this.routeDocumentMaint.IsReadyToBeUsed((PXGraph) this));
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  protected virtual void FSRouteDocument_Date_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField]
  [FSSelectorRouteID]
  protected virtual void FSRouteDocument_RouteID_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  [PXUIField]
  public virtual void openRouteDocument()
  {
    if (((PXSelectBase<FSRouteDocument>) this.Routes).Current != null)
    {
      RouteDocumentMaint instance = (RouteDocumentMaint) PXGraph.CreateInstance(typeof (RouteDocumentMaint));
      ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Search<FSAppointment.refNbr>((object) ((PXSelectBase<FSRouteDocument>) this.Routes).Current.RefNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenDriverSelector()
  {
    if (((PXSelectBase<FSRouteDocument>) this.Routes).Current == null || !(((PXSelectBase<FSRouteDocument>) this.Routes).Current.Status != "X"))
      return;
    ((PXSelectBase<FSRouteDocument>) this.DriverRouteSelected).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) this.DriverRouteSelected).Search<FSRouteDocument.routeDocumentID>((object) ((PXSelectBase<FSRouteDocument>) this.Routes).Current.RouteDocumentID, Array.Empty<object>()));
    if (((PXSelectBase<FSRouteDocument>) this.DriverRouteSelected).AskExt() != 1 || ((PXSelectBase<EPEmployee>) this.DriverRecords).Current == null)
      return;
    int? driverId = ((PXSelectBase<FSRouteDocument>) this.Routes).Current.DriverID;
    int? baccountId = ((PXSelectBase<EPEmployee>) this.DriverRecords).Current.BAccountID;
    if (driverId.GetValueOrDefault() == baccountId.GetValueOrDefault() & driverId.HasValue == baccountId.HasValue)
      return;
    ((PXSelectBase<FSRouteDocument>) this.Routes).Current.DriverID = ((PXSelectBase<EPEmployee>) this.DriverRecords).Current.BAccountID;
    this.UpdateRoute(((PXSelectBase<FSRouteDocument>) this.Routes).Current);
  }

  [PXButton]
  [PXUIField]
  public virtual void OpenVehicleSelector()
  {
    if (((PXSelectBase<FSRouteDocument>) this.Routes).Current == null || ((PXSelectBase<VehicleSelectionFilter>) this.VehicleFilter).Current == null || !(((PXSelectBase<FSRouteDocument>) this.Routes).Current.Status != "X"))
      return;
    ((PXSelectBase<VehicleSelectionFilter>) this.VehicleFilter).Current.RouteDocumentID = ((PXSelectBase<FSRouteDocument>) this.Routes).Current.RouteDocumentID;
    ((PXSelectBase<FSRouteDocument>) this.VehicleRouteSelected).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) this.VehicleRouteSelected).Search<FSRouteDocument.routeDocumentID>((object) ((PXSelectBase<FSRouteDocument>) this.Routes).Current.RouteDocumentID, Array.Empty<object>()));
    if (((PXSelectBase<FSRouteDocument>) this.VehicleRouteSelected).AskExt() != 1 || ((PXSelectBase<FSVehicle>) this.VehicleRecords).Current == null)
      return;
    int? vehicleId = ((PXSelectBase<FSRouteDocument>) this.Routes).Current.VehicleID;
    int? smEquipmentId = ((PXSelectBase<FSVehicle>) this.VehicleRecords).Current.SMEquipmentID;
    if (vehicleId.GetValueOrDefault() == smEquipmentId.GetValueOrDefault() & vehicleId.HasValue == smEquipmentId.HasValue)
      return;
    ((PXSelectBase<FSRouteDocument>) this.Routes).Current.VehicleID = ((PXSelectBase<FSVehicle>) this.VehicleRecords).Current.SMEquipmentID;
    this.UpdateRoute(((PXSelectBase<FSRouteDocument>) this.Routes).Current);
  }

  [PXInsertButton]
  [PXUIField(DisplayName = "")]
  protected virtual void createNew()
  {
    RouteDocumentMaint instance = (RouteDocumentMaint) PXGraph.CreateInstance(typeof (RouteDocumentMaint));
    ((PXGraph) instance).Clear((PXClearOption) 3);
    ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Insert((FSRouteDocument) ((PXSelectBase) instance.RouteRecords).Cache.CreateInstance());
    ((PXSelectBase) instance.RouteRecords).Cache.IsDirty = false;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
  }

  [PXEditDetailButton]
  [PXUIField(DisplayName = "")]
  protected virtual void editDetail()
  {
    if (((PXSelectBase<FSRouteDocument>) this.Routes).Current != null)
    {
      FSRouteDocument fsRouteDocument = PXResultset<FSRouteDocument>.op_Implicit(PXSelectBase<FSRouteDocument, PXSelect<FSRouteDocument, Where<FSRouteDocument.routeDocumentID, Equal<Required<FSRouteDocument.routeDocumentID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) ((PXSelectBase<FSRouteDocument>) this.Routes).Current.RouteDocumentID
      }));
      RouteDocumentMaint instance = (RouteDocumentMaint) PXGraph.CreateInstance(typeof (RouteDocumentMaint));
      ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Search<FSAppointment.refNbr>((object) fsRouteDocument.RefNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  protected virtual IEnumerable vehicleRecords()
  {
    return VehicleSelectionHelper.VehicleRecordsDelegate((PXGraph) this, this.VehicleRouteSelected, this.VehicleFilter);
  }

  protected virtual IEnumerable driverRecords()
  {
    return DriverSelectionHelper.DriverRecordsDelegate((PXGraph) this, this.DriverRouteSelected, this.DriverFilter);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.vehicleID> e)
  {
    if (e.Row == null)
      return;
    this.UpdateRoute(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.additionalVehicleID1> e)
  {
    if (e.Row == null)
      return;
    this.UpdateRoute(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.additionalVehicleID2> e)
  {
    if (e.Row == null)
      return;
    this.UpdateRoute(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.driverID> e)
  {
    if (e.Row == null)
      return;
    this.UpdateRoute(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSRouteDocument, FSRouteDocument.additionalDriverID> e)
  {
    if (e.Row == null)
      return;
    this.UpdateRoute(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSRouteDocument> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSRouteDocument>>) e).Cache;
    bool flag = row.Status != "X";
    ((PXSelectBase) this.Routes).Cache.IsDirty = false;
    ((PXSelectBase) this.VehicleRouteSelected).Cache.IsDirty = false;
    ((PXSelectBase) this.DriverRouteSelected).Cache.IsDirty = false;
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.refNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.routeID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.date>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.timeBegin>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.vehicleID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.additionalVehicleID1>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.additionalVehicleID2>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.driverID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.additionalDriverID>(cache, (object) row, flag);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSRouteDocument> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSRouteDocument> e)
  {
  }

  /// <summary>Save the FSRouteDocument row.</summary>
  public virtual void UpdateRoute(FSRouteDocument fsRouteDocumentRow)
  {
    if (this.routeDocumentMaint == null)
      this.routeDocumentMaint = PXGraph.CreateInstance<RouteDocumentMaint>();
    FSRouteDocument fsRouteDocument = ((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Search<FSRouteDocument.routeDocumentID>((object) fsRouteDocumentRow.RouteDocumentID, Array.Empty<object>()));
    int? nullable1 = fsRouteDocument.RouteDocumentID;
    int? nullable2 = fsRouteDocumentRow.RouteDocumentID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSRouteDocument))
      });
    nullable2 = fsRouteDocument.DriverID;
    nullable1 = fsRouteDocumentRow.DriverID;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
    {
      nullable1 = fsRouteDocument.AdditionalDriverID;
      nullable2 = fsRouteDocumentRow.AdditionalDriverID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = fsRouteDocument.VehicleID;
        nullable1 = fsRouteDocumentRow.VehicleID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = fsRouteDocument.AdditionalVehicleID1;
          nullable2 = fsRouteDocumentRow.AdditionalVehicleID1;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = fsRouteDocument.AdditionalVehicleID2;
            nullable1 = fsRouteDocumentRow.AdditionalVehicleID2;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              return;
          }
        }
      }
    }
    nullable1 = fsRouteDocument.VehicleID;
    nullable2 = fsRouteDocumentRow.VehicleID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      ((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).SetValueExt<FSRouteDocument.vehicleID>(((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Current, (object) fsRouteDocumentRow.VehicleID);
    nullable2 = fsRouteDocument.AdditionalVehicleID1;
    nullable1 = fsRouteDocumentRow.AdditionalVehicleID1;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      ((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).SetValueExt<FSRouteDocument.additionalVehicleID1>(((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Current, (object) fsRouteDocumentRow.AdditionalVehicleID1);
    nullable1 = fsRouteDocument.AdditionalVehicleID2;
    nullable2 = fsRouteDocumentRow.AdditionalVehicleID2;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      ((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).SetValueExt<FSRouteDocument.additionalVehicleID2>(((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Current, (object) fsRouteDocumentRow.AdditionalVehicleID2);
    nullable2 = fsRouteDocument.DriverID;
    nullable1 = fsRouteDocumentRow.DriverID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      ((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).SetValueExt<FSRouteDocument.driverID>(((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Current, (object) fsRouteDocumentRow.DriverID);
    nullable1 = fsRouteDocument.AdditionalDriverID;
    nullable2 = fsRouteDocumentRow.AdditionalDriverID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      ((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).SetValueExt<FSRouteDocument.additionalDriverID>(((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Current, (object) fsRouteDocumentRow.AdditionalDriverID);
    ((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Update(((PXSelectBase<FSRouteDocument>) this.routeDocumentMaint.RouteRecords).Current);
    ((PXAction) this.routeDocumentMaint.Save).Press();
  }

  [Serializable]
  public class RouteWrkSheetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? FromDate { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? ToDate { get; set; }

    [PXInt]
    [PXUIField]
    [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<FSxEPEmployee.sDEnabled, Equal<True>, And<FSxEPEmployee.isDriver, Equal<True>, And<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>>>))]
    [FSSelector_Driver_All]
    public virtual int? DriverID { get; set; }

    public abstract class fromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RouteWrkSheetInq.RouteWrkSheetFilter.fromDate>
    {
    }

    public abstract class toDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RouteWrkSheetInq.RouteWrkSheetFilter.toDate>
    {
    }

    public abstract class driverID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RouteWrkSheetInq.RouteWrkSheetFilter.driverID>
    {
    }
  }
}
