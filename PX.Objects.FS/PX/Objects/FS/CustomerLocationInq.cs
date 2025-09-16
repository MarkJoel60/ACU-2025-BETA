// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CustomerLocationInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class CustomerLocationInq : PXGraph<CustomerLocationInq>
{
  public PXCancel<BAccountLocation> cancel;
  public PXFirst<BAccountLocation> First;
  public PXPrevious<BAccountLocation> Previous;
  public PXNext<BAccountLocation> Next;
  public PXLast<BAccountLocation> Last;
  public bool inactiveFlag = true;
  public PXSelect<BAccountLocation, Where<BAccountLocation.customerID, Equal<Optional<BAccountLocation.customerID>>>> LocationRecords;
  public PXSelectReadonly2<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>, And<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, Or<FSAppointmentDet.lineType, Equal<FSLineType.NonStockItem>>>>>, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.refNbr, Equal<FSAppointment.soRefNbr>>, LeftJoin<FSPostDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSPostBatch, On<FSPostBatch.batchID, Equal<FSPostDet.batchID>>>>>>>, Where<FSServiceOrder.locationID, Equal<Current<BAccountLocation.locationID>>>, OrderBy<Desc<FSAppointment.executionDate>>> Services;
  public PXSelectReadonly2<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.refNbr, Equal<FSAppointment.soRefNbr>>, LeftJoin<FSPostDet, On<FSAppointmentDet.postID, Equal<FSPostDet.postID>>, LeftJoin<FSPostBatch, On<FSPostBatch.batchID, Equal<FSPostDet.batchID>>>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSServiceOrder.locationID, Equal<Current<BAccountLocation.locationID>>>>, OrderBy<Desc<FSAppointment.executionDate>>> PickUpDeliveryItems;
  public PXSelectReadonly2<FSRouteContractScheduleFSServiceContract, InnerJoin<FSScheduleRoute, On<FSScheduleRoute.scheduleID, Equal<FSSchedule.scheduleID>>, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSScheduleRoute.dfltRouteID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSRouteContractSchedule.entityID>>>>>, Where<FSRouteContractScheduleFSServiceContract.customerLocationID, Equal<Current<BAccountLocation.locationID>>>, OrderBy<Desc<FSRouteContractSchedule.refNbr>>> RouteContractSchedules;
  public PXSelectReadonly2<ARPriceClass, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.cPriceClassID, Equal<ARPriceClass.priceClassID>>>, Where<PX.Objects.CR.Location.locationID, Equal<Current<BAccountLocation.locationID>>>> PriceClass1;
  public PXSelectReadonly2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PX.Objects.CR.Location.locationID, Equal<Current<BAccountLocation.locationID>>>> CustomerRecords;
  [PXFilterable(new System.Type[] {})]
  public PXSelectReadonly2<PX.Objects.CR.Location, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>, LeftJoin<FSBillingCycle, On<FSxCustomer.billingCycleID, Equal<FSBillingCycle.billingCycleID>>>>>>, Where<PX.Objects.CR.Location.locationID, Equal<Current<BAccountLocation.locationID>>>> LocationSelected;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> DummyView;
  public PXAction<BAccountLocation> OpenRouteServiceContract;
  public PXAction<BAccountLocation> OpenRouteSchedule;
  public PXAction<BAccountLocation> OpenAppointmentByPickUpDeliveryItem;
  public PXAction<BAccountLocation> OpenAppointmentByService;
  public PXAction<BAccountLocation> OpenDocumentByPickUpDeliveryItem;
  public PXAction<BAccountLocation> OpenDocumentByService;

  public CustomerLocationInq() => ((PXSelectBase) this.LocationRecords).Cache.AllowDelete = false;

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    BAccountLocation current = ((PXSelectBase<BAccountLocation>) this.LocationRecords).Current;
    if (current != null && current.CustomerID.HasValue && adapter.Searches.Length == 2 && adapter.Searches[0] != null && current.CustomerCD.Trim() != adapter.Searches[0].ToString().Trim())
    {
      PX.Objects.CR.Location location = PXResult<PX.Objects.CR.BAccount, PX.Objects.CR.Location>.op_Implicit((PXResult<PX.Objects.CR.BAccount, PX.Objects.CR.Location>) PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelectJoin<PX.Objects.CR.BAccount, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>, Where<PX.Objects.CR.BAccount.acctCD, Equal<Required<PX.Objects.CR.BAccount.acctCD>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        adapter.Searches[0]
      })));
      adapter.Searches[1] = (object) location.LocationCD;
    }
    IEnumerator enumerator = ((PXAction) new PXCancel<BAccountLocation>((PXGraph) this, nameof (Cancel))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
        return (IEnumerable) new object[1]
        {
          (object) (BAccountLocation) enumerator.Current
        };
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXButton]
  [PXUIField]
  public virtual void openRouteServiceContract()
  {
    if (((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current != null)
    {
      RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
      ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = FSServiceContract.PK.Find((PXGraph) this, ((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current.EntityID);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual void openRouteSchedule()
  {
    if (((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current != null)
    {
      RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
      ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = PXResultset<FSRouteContractSchedule>.op_Implicit(((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Search<FSSchedule.scheduleID>((object) ((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current.ScheduleID, new object[2]
      {
        (object) ((PXSelectBase<FSRouteContractScheduleFSServiceContract>) this.RouteContractSchedules).Current.EntityID,
        (object) ((PXSelectBase<BAccountLocation>) this.LocationRecords).Current.CustomerID
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual void openAppointmentByPickUpDeliveryItem()
  {
    if (((PXSelectBase<FSAppointmentDet>) this.PickUpDeliveryItems).Current == null)
      return;
    this.openAppointment(PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXSelectBase<FSAppointmentDet>) this.PickUpDeliveryItems).Current.AppointmentID
    })));
  }

  [PXButton]
  [PXUIField]
  public virtual void openAppointmentByService()
  {
    if (((PXSelectBase<FSAppointmentDet>) this.Services).Current == null)
      return;
    this.openAppointment(PXResultset<FSAppointment>.op_Implicit(PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXSelectBase<FSAppointmentDet>) this.Services).Current.AppointmentID
    })));
  }

  [PXButton]
  [PXUIField]
  public virtual void openDocumentByPickUpDeliveryItem()
  {
    if (((PXSelectBase<FSAppointmentDet>) this.PickUpDeliveryItems).Current == null)
      return;
    this.openDocument(PXResultset<FSPostDet>.op_Implicit(PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.postDetID, Equal<Required<FSPostDet.postDetID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXSelectBase<FSAppointmentDet>) this.PickUpDeliveryItems).Current.PostID
    })));
  }

  [PXButton]
  [PXUIField]
  public virtual void openDocumentByService()
  {
    if (((PXSelectBase<FSAppointmentDet>) this.Services).Current == null)
      return;
    this.openDocument(PXResultset<FSPostDet>.op_Implicit(PXSelectBase<FSPostDet, PXSelect<FSPostDet, Where<FSPostDet.postDetID, Equal<Required<FSPostDet.postDetID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXSelectBase<FSAppointmentDet>) this.Services).Current.PostID
    })));
  }

  public virtual void openDocument(FSPostDet fsPostDetRow)
  {
    if (fsPostDetRow.SOPosted.GetValueOrDefault())
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      {
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) fsPostDetRow.SOOrderNbr, new object[1]
        {
          (object) fsPostDetRow.SOOrderType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    else
    {
      if (fsPostDetRow.ARPosted.GetValueOrDefault())
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) fsPostDetRow.ARRefNbr, new object[1]
        {
          (object) fsPostDetRow.ARDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (fsPostDetRow.SOInvPosted.GetValueOrDefault())
      {
        SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) fsPostDetRow.ARRefNbr, new object[1]
        {
          (object) fsPostDetRow.ARDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (fsPostDetRow.APPosted.GetValueOrDefault())
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) fsPostDetRow.APRefNbr, new object[1]
        {
          (object) fsPostDetRow.APDocType
        }));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
  }

  public virtual void openAppointment(FSAppointment fsAppointmentRow)
  {
    if (fsAppointmentRow != null)
    {
      AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
      ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointmentRow.RefNbr, new object[1]
      {
        (object) fsAppointmentRow.SrvOrdType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }
}
