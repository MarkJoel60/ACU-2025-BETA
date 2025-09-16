// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderBase`2
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.FS.SiteStatusLookup;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.FS;

public class ServiceOrderBase<TGraph, TPrimary> : 
  PXGraph<TGraph, TPrimary>,
  ISiteStatusLookupCompatible
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccounts;
  [PXHidden]
  public PXSelect<BAccountSelectorBase> BAccountSelectorBaseView;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> Vendors;
  [PXHidden]
  public PXSetup<FSSelectorHelper> Helper;
  [PXHidden]
  public PXSelect<PX.Objects.IN.InventoryItem> InventoryItemHelper;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly<FSProfitability> ProfitabilityRecords;
  public PXSetup<FSBillingCycle, InnerJoin<FSCustomerBillingSetup, On<FSCustomerBillingSetup.billingCycleID, Equal<FSBillingCycle.billingCycleID>>, CrossJoin<FSSetup>>, Where<FSCustomerBillingSetup.customerID, Equal<Current<FSServiceOrder.billCustomerID>>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>> BillingCycleRelated;

  [InjectDependency]
  protected PXSiteMapProvider SiteMapProvider { get; private set; }

  public virtual string GetDefaultTaxZone(
    int? billCustomerID,
    int? billLocationID,
    int? branchID,
    int? projectId)
  {
    string defaultTaxZone = (string) null;
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) billCustomerID,
      (object) billLocationID
    }));
    if (location1 != null)
    {
      if (!string.IsNullOrEmpty(location1.CTaxZoneID))
      {
        defaultTaxZone = location1.CTaxZoneID;
      }
      else
      {
        PX.Objects.CR.Address adrress = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) location1.DefAddressID
        }));
        if (adrress != null)
          defaultTaxZone = TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, (IAddressBase) adrress);
      }
    }
    if (defaultTaxZone == null && ((PXCache) GraphHelper.Caches<FSAddress>((PXGraph) this)).Current is FSAddress current)
      defaultTaxZone = TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, (IAddressBase) current);
    if (defaultTaxZone == null)
    {
      PX.Objects.CR.Location location2 = PXResult<PX.Objects.GL.Branch, PX.Objects.CR.BAccount, PX.Objects.CR.Location>.op_Implicit((PXResult<PX.Objects.GL.Branch, PX.Objects.CR.BAccount, PX.Objects.CR.Location>) PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) branchID
      })));
      if (location2 != null && location2.VTaxZoneID != null)
        defaultTaxZone = location2.VTaxZoneID;
    }
    return defaultTaxZone;
  }

  public virtual void FSAddress_RowUpdated_Handler(PX.Data.Events.RowUpdated<FSAddress> e, PXCache cache)
  {
    if (e.Row == null)
      return;
    FSAddress row = e.Row;
    FSAddress oldRow = e.OldRow;
    if (oldRow.PostalCode == row.PostalCode && oldRow.CountryID == row.CountryID && oldRow.State == row.State)
      return;
    switch (cache.Current)
    {
      case FSServiceOrder fsServiceOrder:
        cache.SetDefaultExt<FSServiceOrder.taxZoneID>((object) fsServiceOrder);
        break;
      case FSAppointment fsAppointment:
        cache.SetDefaultExt<FSAppointment.taxZoneID>((object) fsAppointment);
        break;
    }
  }

  public virtual void FSServiceOrder_BranchLocationID_FieldUpdated_Handler(
    PXGraph graph,
    PXFieldUpdatedEventArgs e,
    FSSrvOrdType fsSrvOrdTypeRow,
    PXSelectBase<FSServiceOrder> serviceOrderRelated)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = (FSServiceOrder) e.Row;
  }

  public virtual void FSServiceOrder_LocationID_FieldUpdated_Handler(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = (FSServiceOrder) e.Row;
    if (cache.Graph.IsCopyPasteContext)
      return;
    this.SetBillCustomerAndLocationID(cache, row);
  }

  public virtual void FSServiceOrder_ContactID_FieldUpdated_Handler(
    PXGraph graph,
    PXFieldUpdatedEventArgs e,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = (FSServiceOrder) e.Row;
  }

  public virtual void FSServiceOrder_BillCustomerID_FieldUpdated_Handler(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = (FSServiceOrder) e.Row;
    if (!cache.Graph.IsCopyPasteContext)
      cache.SetDefaultExt<FSServiceOrder.projectID>(e.Row);
    cache.SetValueExt<FSServiceOrder.billLocationID>(e.Row, (object) this.GetDefaultLocationID(cache.Graph, row.BillCustomerID));
    if (!row.BillServiceContractID.HasValue)
      return;
    int? billCustomerId1 = FSServiceContract.PK.Find((PXGraph) this, row.BillServiceContractID).BillCustomerID;
    int? billCustomerId2 = row.BillCustomerID;
    if (billCustomerId1.GetValueOrDefault() == billCustomerId2.GetValueOrDefault() & billCustomerId1.HasValue == billCustomerId2.HasValue)
      return;
    cache.SetValueExt<FSServiceOrder.billServiceContractID>(e.Row, (object) null);
  }

  public virtual List<FSSODet> DetailSiteLocationForUpdate(PXSelectBase<FSSODet> serviceOrderDetails)
  {
    return GraphHelper.RowCast<FSSODet>(((PXSelectBase) serviceOrderDetails).Cache.Inserted).Concat<FSSODet>(GraphHelper.RowCast<FSSODet>(((PXSelectBase) serviceOrderDetails).Cache.Updated).Where<FSSODet>((Func<FSSODet, bool>) (x =>
    {
      int? siteId = x.SiteID;
      int? valueOriginal1 = (int?) ((PXSelectBase) serviceOrderDetails).Cache.GetValueOriginal<FSSODet.siteID>((object) x);
      if (!(siteId.GetValueOrDefault() == valueOriginal1.GetValueOrDefault() & siteId.HasValue == valueOriginal1.HasValue))
        return true;
      int? siteLocationId = x.SiteLocationID;
      int? valueOriginal2 = (int?) ((PXSelectBase) serviceOrderDetails).Cache.GetValueOriginal<FSSODet.siteLocationID>((object) x);
      return !(siteLocationId.GetValueOrDefault() == valueOriginal2.GetValueOrDefault() & siteLocationId.HasValue == valueOriginal2.HasValue);
    }))).ToList<FSSODet>();
  }

  public virtual void UpdateDetailSiteLocationInAppt(
    AppointmentEntry graphAppointmentEntry,
    List<FSSODet> detailsForUpdate)
  {
    if (detailsForUpdate.Count == 0)
      return;
    foreach (FSSODet fssoDet in detailsForUpdate)
    {
      FSSODet soDetRow = fssoDet;
      foreach (FSAppointmentDet fsAppointmentDet in GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) graphAppointmentEntry.AppointmentDetails).Select(Array.Empty<object>())).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x =>
      {
        int? soDetId1 = x.SODetID;
        int? soDetId2 = soDetRow.SODetID;
        return soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue;
      })))
      {
        FSAppointmentDet apptDetRow = fsAppointmentDet;
        ((PXSelectBase<FSAppointmentDet>) graphAppointmentEntry.AppointmentDetails).Current = apptDetRow;
        foreach (FSApptLineSplit fsApptLineSplit in GraphHelper.RowCast<FSApptLineSplit>((IEnumerable) ((PXSelectBase<FSApptLineSplit>) graphAppointmentEntry.Splits).Select(Array.Empty<object>())).Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (x =>
        {
          int? lineNbr1 = x.LineNbr;
          int? lineNbr2 = apptDetRow.LineNbr;
          if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
            return false;
          int? inventoryId1 = x.InventoryID;
          int? inventoryId2 = apptDetRow.InventoryID;
          return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
        })))
        {
          if (fsApptLineSplit != null)
          {
            ((PXSelectBase<FSApptLineSplit>) graphAppointmentEntry.Splits).Delete(fsApptLineSplit);
            apptDetRow.LotSerialNbr = (string) null;
            ((PXSelectBase<FSAppointmentDet>) graphAppointmentEntry.AppointmentDetails).Update(apptDetRow);
          }
        }
        apptDetRow.SiteID = soDetRow.SiteID;
        apptDetRow.SiteLocationID = soDetRow.SiteLocationID;
        ((PXSelectBase<FSAppointmentDet>) graphAppointmentEntry.AppointmentDetails).Update(apptDetRow);
      }
    }
  }

  public virtual void FSServiceOrder_RowPersisting_Handler(
    ServiceOrderEntry graphServiceOrderEntry,
    PXCache cacheServiceOrder,
    PXRowPersistingEventArgs e,
    FSSrvOrdType fsSrvOrdTypeRow,
    PXSelectBase<FSSODet> serviceOrderDetails,
    ServiceOrderBase<TGraph, TPrimary>.ServiceOrderAppointments_View serviceOrderAppointments,
    AppointmentEntry graphAppointmentEntryCaller,
    bool forceAppointmentCheckings)
  {
    if (e.Row == null)
      return;
    FSServiceOrder row = (FSServiceOrder) e.Row;
    FSAppointment fsAppointment1 = (FSAppointment) null;
    if (graphAppointmentEntryCaller != null)
      fsAppointment1 = ((PXSelectBase<FSAppointment>) graphAppointmentEntryCaller.AppointmentRecords).Current;
    if (e.Operation == 2 || e.Operation == 1)
    {
      if (fsSrvOrdTypeRow == null)
        throw new PXException("The {0} service order type does not exist. Select another service order type or create a new one on the Service Order Types (FS202300) form.", new object[1]
        {
          (object) row.SrvOrdType
        });
      int? nullable1 = row.BillCustomerID;
      int? valueOriginal1 = (int?) cacheServiceOrder.GetValueOriginal<FSServiceOrder.billCustomerID>((object) row);
      bool flag1 = !(nullable1.GetValueOrDefault() == valueOriginal1.GetValueOrDefault() & nullable1.HasValue == valueOriginal1.HasValue);
      if (flag1)
      {
        if (((PXSelectBase<PX.Objects.AR.ARPayment>) graphServiceOrderEntry.Adjustments).Any<PX.Objects.AR.ARPayment>())
          throw new PXException("The service order contains at least one prepayment associated with a different customer. The billing customer cannot be modified.");
        if (GraphHelper.RowCast<FSSODet>((IEnumerable) serviceOrderDetails.Select(Array.Empty<object>())).Any<FSSODet>((Func<FSSODet, bool>) (x => x.LinkedEntityType == "ER" && !string.IsNullOrEmpty(x.LinkedDisplayRefNbr))))
          throw new PXException("The service order is related to at least one expense receipt associated with a different customer. The billing customer cannot be modified.");
        if (GraphHelper.RowCast<FSSODet>((IEnumerable) serviceOrderDetails.Select(Array.Empty<object>())).Any<FSSODet>((Func<FSSODet, bool>) (x => x.LinkedEntityType == "AP" && !string.IsNullOrEmpty(x.LinkedDisplayRefNbr))))
          throw new PXException("The service order is related to at least one bill. The billing customer cannot be modified.");
      }
      if (string.IsNullOrWhiteSpace(row.DocDesc))
        this.SetDocDesc((PXGraph) graphServiceOrderEntry, row);
      IEnumerable<FSSODet> source = GraphHelper.RowCast<FSSODet>((IEnumerable) ((IEnumerable<PXResult<FSSODet>>) serviceOrderDetails.Select(Array.Empty<object>())).AsEnumerable<PXResult<FSSODet>>());
      List<FSSODet> fssoDetList = this.DetailSiteLocationForUpdate(serviceOrderDetails);
      bool flag2 = GraphHelper.RowCast<FSSODet>(((PXSelectBase) serviceOrderDetails).Cache.Inserted).Any<FSSODet>() || GraphHelper.RowCast<FSSODet>(((PXSelectBase) serviceOrderDetails).Cache.Updated).Any<FSSODet>((Func<FSSODet, bool>) (x =>
      {
        int? projectTaskId = x.ProjectTaskID;
        int? valueOriginal2 = (int?) ((PXSelectBase) serviceOrderDetails).Cache.GetValueOriginal<FSSODet.projectTaskID>((object) x);
        return !(projectTaskId.GetValueOrDefault() == valueOriginal2.GetValueOrDefault() & projectTaskId.HasValue == valueOriginal2.HasValue);
      }));
      int? nullable2 = row.ProjectID;
      nullable1 = (int?) cacheServiceOrder.GetValueOriginal<FSServiceOrder.projectID>((object) row);
      bool flag3 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue);
      nullable1 = row.BranchID;
      nullable2 = (int?) cacheServiceOrder.GetValueOriginal<FSServiceOrder.branchID>((object) row);
      bool flag4 = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
      bool flag5 = row.CuryID != (string) cacheServiceOrder.GetValueOriginal<FSServiceOrder.curyID>((object) row);
      nullable2 = row.BillServiceContractID;
      nullable1 = (int?) cacheServiceOrder.GetValueOriginal<FSServiceOrder.billServiceContractID>((object) row);
      bool flag6 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue);
      bool flag7 = row.TaxZoneID != (string) cacheServiceOrder.GetValueOriginal<FSServiceOrder.taxZoneID>((object) row);
      if ((flag1 | flag7 | flag5 | flag3 | flag4 | flag2 | flag6 || fssoDetList.Any<FSSODet>()) && ((PXSelectBase<FSAppointment>) serviceOrderAppointments).Any<FSAppointment>())
      {
        AppointmentEntry appointmentEntry = PXContext.GetSlot<AppointmentEntry>() ?? PXContext.SetSlot<AppointmentEntry>(PXGraph.CreateInstance<AppointmentEntry>());
        appointmentEntry.DisableServiceOrderUnboundFieldCalc = true;
        PXCheckUnique.SetPersistingCheck<FSAppointmentDet.lineNbr>(((PXSelectBase) appointmentEntry.AppointmentDetails).Cache, false);
        PXCheckUnique.SetPersistingCheck<FSAppointmentDet.sODetID>(((PXSelectBase) appointmentEntry.AppointmentDetails).Cache, false);
        appointmentEntry.RecalculateExternalTaxesSync = true;
        foreach (PXResult<FSAppointment> pxResult1 in ((PXSelectBase<FSAppointment>) serviceOrderAppointments).Select(Array.Empty<object>()))
        {
          FSAppointment fsAppointmentRow = PXResult<FSAppointment>.op_Implicit(pxResult1);
          PXCache<FSAppointment>.StoreOriginal((PXGraph) appointmentEntry, fsAppointmentRow);
          nullable1 = (int?) fsAppointment1?.AppointmentID;
          nullable2 = fsAppointmentRow.AppointmentID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            appointmentEntry.SkipServiceOrderUpdate = true;
            PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords1 = appointmentEntry.AppointmentRecords;
            PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> appointmentRecords2 = appointmentEntry.AppointmentRecords;
            string refNbr = fsAppointmentRow.RefNbr;
            object[] objArray = new object[1]
            {
              (object) fsAppointmentRow.SrvOrdType
            };
            FSAppointment fsAppointment2;
            FSAppointment fsAppointment3 = fsAppointment2 = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) appointmentRecords2).Search<FSAppointment.refNbr>((object) refNbr, objArray));
            ((PXSelectBase<FSAppointment>) appointmentRecords1).Current = fsAppointment2;
            FSAppointment data = fsAppointment3;
            appointmentEntry.SkipServiceOrderUpdate = false;
            if (flag5)
            {
              data.CuryID = row.CuryID;
              data.CuryInfoID = this.CopyCurrencyInfo((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) graphServiceOrderEntry.currencyInfoView, (PXGraph) appointmentEntry, row.CuryInfoID);
            }
            if (flag1)
            {
              FSAppointment fsAppointment4 = data;
              nullable2 = new int?();
              int? nullable3 = nullable2;
              fsAppointment4.BillServiceContractID = nullable3;
            }
            if (flag3)
            {
              data.ProjectID = row.ProjectID;
              FSAppointment fsAppointment5 = data;
              nullable2 = new int?();
              int? nullable4 = nullable2;
              fsAppointment5.DfltProjectTaskID = nullable4;
            }
            data.BranchID = row.BranchID;
            data.TaxZoneID = row.TaxZoneID;
            ((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Update(data);
            ((PXSelectBase) appointmentEntry.AppointmentRecords).Cache.SetValueExtIfDifferent<FSAppointment.billServiceContractID>((object) data, (object) row.BillServiceContractID);
            List<FSAppointmentDet> list1 = GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((IEnumerable<PXResult<FSAppointmentDet>>) ((PXSelectBase<FSAppointmentDet>) appointmentEntry.AppointmentDetails).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSAppointmentDet>>()).ToList<FSAppointmentDet>();
            IEnumerable<FSAppointmentDet> fsAppointmentDets = list1.Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x =>
            {
              int? appointmentId1 = x.AppointmentID;
              int? appointmentId2 = fsAppointmentRow.AppointmentID;
              return appointmentId1.GetValueOrDefault() == appointmentId2.GetValueOrDefault() & appointmentId1.HasValue == appointmentId2.HasValue;
            }));
            PXSelect<FSAppointmentDet, Where<Current<FSAppointment.srvOrdType>, Equal<FSAppointmentDet.srvOrdType>, And<Current<FSAppointment.refNbr>, Equal<FSAppointmentDet.refNbr>>>> pxSelect1 = new PXSelect<FSAppointmentDet, Where<Current<FSAppointment.srvOrdType>, Equal<FSAppointmentDet.srvOrdType>, And<Current<FSAppointment.refNbr>, Equal<FSAppointmentDet.refNbr>>>>((PXGraph) appointmentEntry);
            ((PXSelectBase) pxSelect1).View.Clear();
            ((PXSelectBase<FSAppointmentDet>) pxSelect1).StoreResult(((IEnumerable<object>) list1).ToList<object>(), PXQueryParameters.ExplicitParameters(new object[2]
            {
              (object) data.SrvOrdType,
              (object) data.RefNbr
            }));
            PXSelect<FSApptLineSplit, Where<Current<FSAppointmentDet.srvOrdType>, Equal<FSApptLineSplit.srvOrdType>, And<Current<FSAppointmentDet.refNbr>, Equal<FSApptLineSplit.apptNbr>, And<Current<FSAppointmentDet.lineNbr>, Equal<FSApptLineSplit.lineNbr>>>>> pxSelect2 = new PXSelect<FSApptLineSplit, Where<Current<FSAppointmentDet.srvOrdType>, Equal<FSApptLineSplit.srvOrdType>, And<Current<FSAppointmentDet.refNbr>, Equal<FSApptLineSplit.apptNbr>, And<Current<FSAppointmentDet.lineNbr>, Equal<FSApptLineSplit.lineNbr>>>>>((PXGraph) appointmentEntry);
            List<FSApptLineSplit> list2 = GraphHelper.RowCast<FSApptLineSplit>((IEnumerable) PXSelectBase<FSApptLineSplit, PXSelect<FSApptLineSplit, Where<FSApptLineSplit.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSApptLineSplit.apptNbr, Equal<Current<FSAppointment.refNbr>>>>>.Config>.Select((PXGraph) appointmentEntry, Array.Empty<object>())).ToList<FSApptLineSplit>();
            ((PXSelectBase) pxSelect2).View.Clear();
            PXView view = ((PXGraph) appointmentEntry).TypedViews.GetView(PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<FSSODetSplit.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FSSODetSplit.pOCreate, IBqlBool>.IsEqual<False>>>.Order<PX.Data.BQL.Fluent.By<BqlField<FSSODetSplit.splitLineNbr, IBqlInt>.Asc>>>.Config>.GetCommand(), false);
            view.Clear();
            Dictionary<int?, List<object>> dictionary = GraphHelper.RowCast<FSSODetSplit>((IEnumerable) PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<FSSODetSplit.pOCreate, IBqlBool>.IsEqual<False>>>.Order<PX.Data.BQL.Fluent.By<BqlField<FSSODetSplit.splitLineNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) appointmentEntry, new object[2]
            {
              (object) data.SrvOrdType,
              (object) data.SORefNbr
            })).GroupBy<FSSODetSplit, int?>((Func<FSSODetSplit, int?>) (f => f.LineNbr)).ToDictionary<IGrouping<int?, FSSODetSplit>, int?, List<object>>((Func<IGrouping<int?, FSSODetSplit>, int?>) (g => g.Key), (Func<IGrouping<int?, FSSODetSplit>, List<object>>) (g => ((IEnumerable<object>) g).ToList<object>()));
            foreach (PXResult<FSSODet> pxResult2 in serviceOrderDetails.Select(Array.Empty<object>()))
            {
              FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult2);
              PrimaryKeyOf<FSSODet>.By<FSSODet.sODetID>.StoreResult((PXGraph) appointmentEntry, (FSSODet.sODetID) fssoDet, false);
              PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSSODet.sODetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.StoreResult((PXGraph) appointmentEntry, (IBqlTable) fssoDet);
              PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.StoreResult((PXGraph) appointmentEntry, (IBqlTable) fssoDet);
              PXCache<FSSODet>.StoreOriginal((PXGraph) appointmentEntry, fssoDet);
              List<object> objectList;
              if (!dictionary.TryGetValue(fssoDet.LineNbr, out objectList))
                objectList = new List<object>();
              view.StoreResult(objectList, PXQueryParameters.ExplicitParameters(new object[3]
              {
                (object) data.SrvOrdType,
                (object) data.SORefNbr,
                (object) fssoDet.LineNbr
              }));
            }
            foreach (FSAppointmentDet fsAppointmentDet1 in fsAppointmentDets)
            {
              FSAppointmentDet relatedAppointmentDetail = fsAppointmentDet1;
              PXCache<FSAppointmentDet>.StoreOriginal((PXGraph) appointmentEntry, relatedAppointmentDetail);
              List<object> list3 = ((IEnumerable<object>) list2.Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (g =>
              {
                int? lineNbr1 = g.LineNbr;
                int? lineNbr2 = relatedAppointmentDetail.LineNbr;
                return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
              }))).ToList<object>();
              relatedAppointmentDetail.FSSODetRow = GraphHelper.RowCast<FSSODet>((IEnumerable) ((IEnumerable<PXResult<FSSODet>>) serviceOrderDetails.Select(Array.Empty<object>())).AsEnumerable<PXResult<FSSODet>>()).Single<FSSODet>((Func<FSSODet, bool>) (x =>
              {
                int? soDetId1 = x.SODetID;
                int? soDetId2 = relatedAppointmentDetail.SODetID;
                return soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue;
              }));
              if (flag1)
              {
                int? nullable5 = relatedAppointmentDetail.SMEquipmentID;
                if (nullable5.HasValue)
                {
                  FSAppointmentDet fsAppointmentDet2 = relatedAppointmentDetail;
                  nullable5 = new int?();
                  int? nullable6 = nullable5;
                  fsAppointmentDet2.SMEquipmentID = nullable6;
                  FSAppointmentDet fsAppointmentDet3 = relatedAppointmentDetail;
                  nullable5 = new int?();
                  int? nullable7 = nullable5;
                  fsAppointmentDet3.ComponentID = nullable7;
                  FSAppointmentDet fsAppointmentDet4 = relatedAppointmentDetail;
                  nullable5 = new int?();
                  int? nullable8 = nullable5;
                  fsAppointmentDet4.EquipmentLineRef = nullable8;
                }
              }
              ((PXSelectBase<FSApptLineSplit>) pxSelect2).StoreResult(list3, PXQueryParameters.ExplicitParameters(new object[3]
              {
                (object) data.SrvOrdType,
                (object) data.RefNbr,
                (object) relatedAppointmentDetail.LineNbr
              }));
            }
            appointmentEntry.UpdateDetailsFromProjectTaskID(row);
            appointmentEntry.UpdateDetailsFromBranchID(row);
            if (fssoDetList.Count > 0)
              this.UpdateDetailSiteLocationInAppt(appointmentEntry, fssoDetList);
            try
            {
              appointmentEntry.SkipServiceOrderUpdate = true;
              ((PXAction) appointmentEntry.Save).Press();
            }
            finally
            {
              appointmentEntry.SkipServiceOrderUpdate = false;
            }
          }
        }
        ((PXGraph) graphServiceOrderEntry).SelectTimeStamp();
      }
      if (fsSrvOrdTypeRow.RequireContact.GetValueOrDefault())
      {
        nullable2 = row.ContactID;
        if (!nullable2.HasValue)
          throw new PXException("A contact is required. Select another service order type for this schedule, or clear the Require Contact check box on the Service Order Types (FS202300) form for the service order type of the schedule.");
      }
      this.UpdateServiceCounts(row, source.Where<FSSODet>((Func<FSSODet, bool>) (x => x.IsService)));
      this.UpdatePendingPostFlags(graphServiceOrderEntry, row, serviceOrderDetails);
      if (!row.Quote.GetValueOrDefault())
      {
        FSServiceOrder fsServiceOrder = row;
        nullable2 = row.ApptNeededLineCntr;
        int num = 0;
        bool? nullable9 = new bool?(nullable2.GetValueOrDefault() > num & nullable2.HasValue);
        fsServiceOrder.AppointmentsNeeded = nullable9;
      }
      if (e.Operation == 2)
        SharedFunctions.CopyNotesAndFiles(cacheServiceOrder, fsSrvOrdTypeRow, (object) row, row.CustomerID, row.LocationID);
      this.ValidateServiceContractDates(cacheServiceOrder, (object) row, ((PXSelectBase<FSServiceContract>) graphServiceOrderEntry.BillServiceContractRelated).Current);
    }
    else if (!this.CanDeleteServiceOrder((PXGraph) graphServiceOrderEntry, row))
      throw new PXException("The service order with the current status cannot be deleted.");
  }

  public virtual long? CopyCurrencyInfo(
    PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo> currencyInfoView,
    PXGraph appointmentEntryGraph,
    long? sourceCuryInfoID)
  {
    PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy((PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) currencyInfoView).Cache.Current);
    copy.CuryInfoID = new long?();
    appointmentEntryGraph.Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)].Clear();
    return ((PX.Objects.CM.Extensions.CurrencyInfo) appointmentEntryGraph.Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)].Insert((object) copy)).CuryInfoID;
  }

  public virtual void FSServiceOrder_CustomerID_FieldUpdated_Handler(
    PXCache cacheServiceOrder,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    PXSelectBase<FSSODet> serviceOrderDetails,
    PXSelectBase<FSAppointmentDet> appointmentDetails,
    PXResultset<FSAppointment> bqlResultSet_Appointment,
    int? oldCustomerID,
    DateTime? itemDateTime,
    bool allowCustomerChange,
    PX.Objects.AR.Customer customerRow)
  {
    if (fsServiceOrderRow == null || !allowCustomerChange && !this.CheckCustomerChange(cacheServiceOrder, fsServiceOrderRow, oldCustomerID, bqlResultSet_Appointment))
      return;
    fsServiceOrderRow.ContactID = new int?();
    int? defaultLocationId = this.GetDefaultLocationID(cacheServiceOrder.Graph, fsServiceOrderRow.CustomerID);
    int? locationId = fsServiceOrderRow.LocationID;
    int? nullable1 = defaultLocationId;
    if (!(locationId.GetValueOrDefault() == nullable1.GetValueOrDefault() & locationId.HasValue == nullable1.HasValue))
      cacheServiceOrder.SetValueExt<FSServiceOrder.locationID>((object) fsServiceOrderRow, (object) defaultLocationId);
    this.SetBillCustomerAndLocationID(cacheServiceOrder, fsServiceOrderRow);
    if (serviceOrderDetails != null)
      this.RefreshSalesPricesInTheWholeDocument(serviceOrderDetails);
    else if (appointmentDetails != null)
      this.RefreshSalesPricesInTheWholeDocument(appointmentDetails);
    if (cacheServiceOrder.Graph.IsCopyPasteContext)
      return;
    nullable1 = fsServiceOrderRow.ProjectID;
    int num = 0;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return;
    FSServiceOrder fsServiceOrder = fsServiceOrderRow;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    fsServiceOrder.ProjectID = nullable2;
  }

  protected bool HaveAnyBilledAppointmentsInServiceOrder(PXGraph graph, int? SOID)
  {
    return ((IQueryable<PXResult<FSAppointment>>) PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>, And<FSAppointment.status, Equal<ListField.AppointmentStatus.billed>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) SOID
    })).Any<PXResult<FSAppointment>>();
  }

  public virtual void FSServiceOrder_RowSelected_PartialHandler(
    PXGraph graph,
    PXCache cacheServiceOrder,
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    PX.Objects.CT.Contract contractRow,
    int appointmentCount,
    int detailsCount,
    PXCache cacheServiceOrderDetails,
    PXCache cacheServiceOrderAppointments,
    PXCache cacheServiceOrderEquipment,
    PXCache cacheServiceOrderEmployees,
    PXCache cacheServiceOrder_Contact,
    PXCache cacheServiceOrder_Address,
    bool? isBeingCalledFromQuickProcess,
    bool allowCustomerChange = false)
  {
    if (cacheServiceOrder.GetStatus((object) fsServiceOrderRow) == 2 && fsSrvOrdTypeRow == null)
      throw new PXException("The {0} service order type does not exist. Select another service order type or create a new one on the Service Order Types (FS202300) form.", new object[1]
      {
        (object) fsServiceOrderRow.SrvOrdType
      });
    this.EnableDisable_Document(graph, cacheServiceOrder, fsServiceOrderRow, fsAppointmentRow, fsSrvOrdTypeRow, appointmentCount, detailsCount, cacheServiceOrderDetails, cacheServiceOrderAppointments, cacheServiceOrderEquipment, cacheServiceOrderEmployees, cacheServiceOrder_Contact, cacheServiceOrder_Address, isBeingCalledFromQuickProcess, allowCustomerChange);
    Exception project = this.CheckIfCustomerBelongsToProject(graph, cacheServiceOrder, fsServiceOrderRow, contractRow);
    switch (graph)
    {
      case ServiceOrderEntry _ when fsServiceOrderRow != null:
        cacheServiceOrder.RaiseExceptionHandling<FSServiceOrder.projectID>((object) fsServiceOrderRow, (object) fsServiceOrderRow.ProjectID, project);
        break;
      case AppointmentEntry _ when fsAppointmentRow != null:
        ((PXSelectBase) ((AppointmentEntry) graph).AppointmentRecords).Cache.RaiseExceptionHandling<FSAppointment.projectID>((object) fsAppointmentRow, (object) fsAppointmentRow.ProjectID, project);
        break;
    }
  }

  public virtual Exception CheckIfCustomerBelongsToProject(
    PXGraph graph,
    PXCache cache,
    FSServiceOrder fsServiceOrderRow,
    PX.Objects.CT.Contract ContractRow)
  {
    if (fsServiceOrderRow == null)
      return (Exception) null;
    int? customerId1 = (int?) ContractRow?.CustomerID;
    Exception project = (Exception) null;
    if (customerId1.HasValue)
    {
      int? nullable = fsServiceOrderRow.CustomerID;
      if (nullable.HasValue)
      {
        nullable = customerId1;
        int? customerId2 = fsServiceOrderRow.CustomerID;
        if (!(nullable.GetValueOrDefault() == customerId2.GetValueOrDefault() & nullable.HasValue == customerId2.HasValue))
          project = (Exception) new PXSetPropertyException("The customer in the line is not the same as the customer in the project or contract.", (PXErrorLevel) 2);
      }
    }
    return project;
  }

  public virtual void RefreshSalesPricesInTheWholeDocument(PXSelectBase<FSSODet> serviceOrderDetails)
  {
    foreach (PXResult<FSSODet> pxResult in serviceOrderDetails.Select(Array.Empty<object>()))
    {
      FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult);
      ((PXSelectBase) serviceOrderDetails).Cache.SetDefaultExt<FSSODet.curyUnitPrice>((object) fssoDet);
      ((PXSelectBase) serviceOrderDetails).Cache.Update((object) fssoDet);
    }
  }

  public virtual void FSServiceOrder_ProjectID_FieldUpdated_PartialHandler(
    FSServiceOrder fsServiceOrderRow,
    PXSelectBase<FSSODet> serviceOrderDetails)
  {
    if (!fsServiceOrderRow.ProjectID.HasValue || serviceOrderDetails == null)
      return;
    foreach (PXResult<FSSODet> pxResult in serviceOrderDetails.Select(Array.Empty<object>()))
    {
      FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult);
      fssoDet.ProjectID = fsServiceOrderRow.ProjectID;
      fssoDet.ProjectTaskID = new int?();
      serviceOrderDetails.Update(fssoDet);
    }
  }

  public virtual void FSServiceOrder_BranchID_FieldUpdated_PartialHandler(
    FSServiceOrder fsServiceOrderRow,
    PXSelectBase<FSSODet> serviceOrderDetails)
  {
    if (!fsServiceOrderRow.BranchID.HasValue || serviceOrderDetails == null)
      return;
    foreach (PXResult<FSSODet> pxResult in serviceOrderDetails.Select(Array.Empty<object>()))
    {
      FSSODet fssoDet = PXResult<FSSODet>.op_Implicit(pxResult);
      fssoDet.BranchID = fsServiceOrderRow.BranchID;
      serviceOrderDetails.Update(fssoDet);
    }
  }

  /// <summary>
  /// Calls SetEnabled and SetPersistingCheck for the specified field depending on the event that is running.
  /// </summary>
  /// <typeparam name="Field">The field to set properties.</typeparam>
  /// <param name="cache">The cache that is executing the event.</param>
  /// <param name="row">The row for which the event is executed.</param>
  /// <param name="eventType">The type of the event that is running.</param>
  /// <param name="isEnabled">True to enable the field. False to disable it.</param>
  /// <param name="persistingCheck">
  /// <para>The type of PersistingCheck for the field.</para>
  /// <para>Pass NULL if you don't want to set the PersistingCheck property for the field.</para>
  /// </param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public virtual void SetEnabledAndPersistingCheck<Field>(
    PXCache cache,
    object row,
    ServiceOrderBase<TGraph, TPrimary>.EventType eventType,
    bool isEnabled,
    PXPersistingCheck? persistingCheck)
    where Field : class, IBqlField
  {
    if (eventType == ServiceOrderBase<TGraph, TPrimary>.EventType.RowSelectedEvent)
      PXUIFieldAttribute.SetEnabled<Field>(cache, row, isEnabled);
    if (!persistingCheck.HasValue)
      return;
    PXDefaultAttribute.SetPersistingCheck<Field>(cache, row, persistingCheck.Value);
  }

  public virtual void X_RowSelected<DAC>(
    PXCache cache,
    PXRowSelectedEventArgs e,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    bool disableSODetReferenceFields,
    bool docAllowsActualFieldEdition)
    where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    this.X_RowSelected<DAC>(cache, e.Row, ServiceOrderBase<TGraph, TPrimary>.EventType.RowSelectedEvent, fsServiceOrderRow, fsSrvOrdTypeRow, disableSODetReferenceFields, docAllowsActualFieldEdition);
  }

  public virtual void X_SetPersistingCheck<DAC>(
    PXCache cache,
    PXRowPersistingEventArgs e,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow)
    where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    this.X_RowSelected<DAC>(cache, e.Row, ServiceOrderBase<TGraph, TPrimary>.EventType.RowPersistingEvent, fsServiceOrderRow, fsSrvOrdTypeRow, true, false);
  }

  public virtual void X_LineType_FieldUpdated<DAC>(PXCache cache, PXFieldUpdatedEventArgs e) where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    if (e.Row == null)
      return;
    DAC row = (DAC) e.Row;
    string lineType = row.LineType;
    string oldValue = (string) e.OldValue;
    System.Type type1 = typeof (DAC);
    FSSODet fssoDet = (FSSODet) null;
    FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) null;
    System.Type type2 = typeof (FSAppointmentDet);
    if (type1 == type2)
      fsAppointmentDet = (FSAppointmentDet) e.Row;
    else
      fssoDet = (FSSODet) e.Row;
    if (lineType != oldValue && (lineType == "CM_LN" || lineType == "IT_LN" || oldValue == "CM_LN" || oldValue == "IT_LN"))
      cache.SetDefaultExt<FSSODet.isBillable>(e.Row);
    cache.SetDefaultExt<FSSODet.isFree>(e.Row);
    if (!this.IsInventoryLine(row.LineType))
    {
      row.ManualPrice = new bool?(false);
      row.InventoryID = new int?();
      row.SubItemID = new int?();
      row.BillingRule = "NONE";
      cache.SetDefaultExt<FSSODet.uOM>(e.Row);
      row.CuryUnitPrice = new Decimal?(0M);
      row.SetDuration(FieldType.EstimatedField, new int?(0), cache, false);
      row.SetQty(FieldType.EstimatedField, new Decimal?(0M), cache, false);
      row.SiteID = new int?();
      row.SiteLocationID = new int?();
      row.ProjectTaskID = new int?();
      row.AcctID = new int?();
      row.SubID = new int?();
      if (fsAppointmentDet != null)
      {
        fsAppointmentDet.ActualDuration = new int?(0);
        fsAppointmentDet.ActualQty = new Decimal?(0M);
      }
      if (fssoDet == null)
        return;
      fssoDet.EnablePO = new bool?(false);
      fssoDet.POVendorID = new int?();
      fssoDet.POVendorLocationID = new int?();
    }
    else
    {
      cache.SetDefaultExt<FSSODet.projectTaskID>(e.Row);
      if (fssoDet != null)
        cache.SetDefaultExt<FSSODet.enablePO>(e.Row);
    }
  }

  public virtual void X_RowSelected<DAC>(
    PXCache cache,
    object eRow,
    ServiceOrderBase<TGraph, TPrimary>.EventType eventType,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    bool disableSODetReferenceFields,
    bool docAllowsActualFieldEdition)
    where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    if (eRow == null)
      return;
    DAC dac = (DAC) eRow;
    System.Type type1 = typeof (DAC);
    FSSODet fssoDet1 = (FSSODet) null;
    bool flag1 = false;
    bool flag2 = false;
    FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) null;
    System.Type type2 = typeof (FSAppointmentDet);
    bool flag3;
    bool? nullable1;
    if (type1 == type2)
    {
      fsAppointmentDet = (FSAppointmentDet) eRow;
      flag2 = true;
      flag3 = ((PXGraph) this).Caches[typeof (TPrimary)].Current != null && (((FSAppointment) ((PXGraph) this).Caches[typeof (TPrimary)].Current).Status == "X" || ((FSAppointment) ((PXGraph) this).Caches[typeof (TPrimary)].Current).Status == "Z");
    }
    else
    {
      fssoDet1 = (FSSODet) eRow;
      flag1 = true;
      int num;
      if (((PXGraph) this).Caches[typeof (TPrimary)].Current != null)
      {
        if (!(((FSServiceOrder) ((PXGraph) this).Caches[typeof (TPrimary)].Current).Status == "X") && !(((FSServiceOrder) ((PXGraph) this).Caches[typeof (TPrimary)].Current).Status == "Z"))
        {
          nullable1 = ((FSServiceOrder) ((PXGraph) this).Caches[typeof (TPrimary)].Current).Mem_Invoiced;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = ((FSServiceOrder) ((PXGraph) this).Caches[typeof (TPrimary)].Current).AllowInvoice;
            num = nullable1.GetValueOrDefault() ? 1 : 0;
            goto label_9;
          }
        }
        num = 1;
      }
      else
        num = 0;
label_9:
      flag3 = num != 0;
    }
    bool flag4 = this.IsInventoryLine(dac.LineType);
    int num1;
    if (flag1)
    {
      if (fsServiceOrderRow == null)
      {
        num1 = 0;
      }
      else
      {
        nullable1 = fsServiceOrderRow.AllowInvoice;
        num1 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num1 = 0;
    bool flag5 = num1 != 0;
    bool flag6 = flag4 && dac.LineType != "SERVI" && dac.LineType != "NSTKI";
    bool flag7 = dac.LineType == "PU_DL";
    this.SetEnabledAndPersistingCheck<FSSODet.sODetID>(cache, eRow, eventType, !disableSODetReferenceFields && !flag7, new PXPersistingCheck?());
    this.SetEnabledAndPersistingCheck<FSSODet.lineType>(cache, eRow, eventType, !disableSODetReferenceFields, new PXPersistingCheck?());
    bool isEnabled1 = true;
    PXPersistingCheck pxPersistingCheck1 = (PXPersistingCheck) 1;
    if (!flag4 && dac.LineType != null)
    {
      isEnabled1 = false;
      pxPersistingCheck1 = (PXPersistingCheck) 2;
    }
    else
    {
      nullable1 = dac.IsPrepaid;
      if (!nullable1.GetValueOrDefault() && (!disableSODetReferenceFields || flag7))
      {
        if (flag1 && fsServiceOrderRow != null)
        {
          nullable1 = fsServiceOrderRow.AllowInvoice;
          if (nullable1.GetValueOrDefault())
            goto label_21;
        }
        if (!flag1 || string.IsNullOrEmpty(fssoDet1.Mem_LastReferencedBy))
          goto label_22;
      }
label_21:
      isEnabled1 = false;
    }
label_22:
    this.SetEnabledAndPersistingCheck<FSSODet.inventoryID>(cache, eRow, eventType, isEnabled1, new PXPersistingCheck?(pxPersistingCheck1));
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      this.SetEnabledAndPersistingCheck<FSSODet.subItemID>(cache, eRow, eventType, isEnabled1 & flag6, new PXPersistingCheck?(flag6 ? pxPersistingCheck1 : (PXPersistingCheck) (object) 2));
    this.SetEnabledAndPersistingCheck<FSSODet.uOM>(cache, eRow, eventType, isEnabled1, new PXPersistingCheck?(pxPersistingCheck1));
    bool isEnabled2 = false;
    if (dac.LineType == "SERVI")
    {
      nullable1 = dac.IsPrepaid;
      bool flag8 = false;
      if (nullable1.GetValueOrDefault() == flag8 & nullable1.HasValue && (fssoDet1 == null || fssoDet1.Mem_LastReferencedBy == null) && (flag2 && cache.GetStatus((object) fsAppointmentDet) == 2 && !fsAppointmentDet.SODetID.HasValue || flag1 && !flag5))
        isEnabled2 = true;
    }
    this.SetEnabledAndPersistingCheck<FSSODet.billingRule>(cache, eRow, eventType, isEnabled2, new PXPersistingCheck?());
    bool flag9 = true;
    nullable1 = dac.IsPrepaid;
    if (!nullable1.GetValueOrDefault() && flag4 && dac.InventoryID.HasValue && !(dac.BillingRule == "NONE"))
    {
      nullable1 = dac.IsBillable;
      bool flag10 = false;
      if (!(nullable1.GetValueOrDefault() == flag10 & nullable1.HasValue) && !flag5)
        goto label_30;
    }
    flag9 = false;
label_30:
    bool isEnabled3 = flag9 && (fsSrvOrdTypeRow?.PostTo != "PM" || fsSrvOrdTypeRow?.PostTo == "PM" && fsSrvOrdTypeRow.BillingType != "CC");
    this.SetEnabledAndPersistingCheck<FSSODet.manualPrice>(cache, eRow, eventType, isEnabled3, new PXPersistingCheck?());
    PXCache cache1 = cache;
    object row1 = eRow;
    int num2 = (int) eventType;
    int num3;
    if (isEnabled3)
    {
      nullable1 = dac.ContractRelated;
      bool flag11 = false;
      num3 = nullable1.GetValueOrDefault() == flag11 & nullable1.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    PXPersistingCheck? persistingCheck1 = new PXPersistingCheck?();
    this.SetEnabledAndPersistingCheck<FSSODet.isFree>(cache1, row1, (ServiceOrderBase<TGraph, TPrimary>.EventType) num2, num3 != 0, persistingCheck1);
    bool flag12 = true;
    nullable1 = dac.IsPrepaid;
    if (!nullable1.GetValueOrDefault() && !flag6 && !(dac.Status == "CC") && !(fsAppointmentDet?.Status == "NP") && flag4)
    {
      nullable1 = dac.ContractRelated;
      if (!nullable1.GetValueOrDefault() && !flag5)
        goto label_36;
    }
    flag12 = false;
label_36:
    bool isEnabled4 = flag12 && (fsSrvOrdTypeRow?.PostTo != "PM" || fsSrvOrdTypeRow.PostTo == "PM" && fsSrvOrdTypeRow?.BillingType != "CC");
    this.SetEnabledAndPersistingCheck<FSSODet.isBillable>(cache, eRow, eventType, isEnabled4, new PXPersistingCheck?());
    bool flag13 = true;
    if (!(dac.BillingRule == "NONE"))
    {
      nullable1 = dac.IsPrepaid;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = dac.IsBillable;
        bool flag14 = false;
        if (!(nullable1.GetValueOrDefault() == flag14 & nullable1.HasValue))
        {
          nullable1 = dac.ContractRelated;
          if (!nullable1.GetValueOrDefault() && flag4 && dac.InventoryID.HasValue && !flag5)
            goto label_41;
        }
      }
    }
    flag13 = false;
label_41:
    bool isEnabled5 = flag13 && (fsSrvOrdTypeRow?.PostTo != "PM" || fsSrvOrdTypeRow?.PostTo == "PM" && fsSrvOrdTypeRow?.BillingType != "CC");
    this.SetEnabledAndPersistingCheck<FSSODet.discPct>(cache, eRow, eventType, isEnabled5, new PXPersistingCheck?());
    this.SetEnabledAndPersistingCheck<FSSODet.curyBillableExtPrice>(cache, eRow, eventType, isEnabled5, new PXPersistingCheck?());
    this.SetEnabledAndPersistingCheck<FSSODet.curyDiscAmt>(cache, eRow, eventType, isEnabled5, new PXPersistingCheck?());
    int num4;
    if (isEnabled5)
    {
      nullable1 = dac.IsFree;
      bool flag15 = false;
      num4 = nullable1.GetValueOrDefault() == flag15 & nullable1.HasValue ? 1 : 0;
    }
    else
      num4 = 0;
    bool isEnabled6 = num4 != 0;
    this.SetEnabledAndPersistingCheck<FSSODet.manualDisc>(cache, eRow, eventType, isEnabled6, new PXPersistingCheck?());
    if (fsAppointmentDet != null)
      this.SetEnabledAndPersistingCheck<FSAppointmentDet.manualDisc>(cache, eRow, eventType, isEnabled6, new PXPersistingCheck?());
    bool isEnabled7 = false;
    if (dac.BillingRule != "NONE")
    {
      nullable1 = dac.IsPrepaid;
      bool flag16 = false;
      if (nullable1.GetValueOrDefault() == flag16 & nullable1.HasValue)
      {
        nullable1 = dac.ContractRelated;
        bool flag17 = false;
        if (nullable1.GetValueOrDefault() == flag17 & nullable1.HasValue && dac.InventoryID.HasValue)
        {
          nullable1 = dac.IsFree;
          if (!nullable1.GetValueOrDefault() && !flag5 && (fsSrvOrdTypeRow?.PostTo != "PM" || fsSrvOrdTypeRow?.PostTo == "PM" && fsSrvOrdTypeRow?.BillingType != "CC"))
            isEnabled7 = true;
        }
      }
    }
    this.SetEnabledAndPersistingCheck<FSSODet.curyUnitPrice>(cache, eRow, eventType, isEnabled7, new PXPersistingCheck?());
    bool isEnabled8 = false;
    if ((dac.LineType == "SERVI" || dac.LineType == "NSTKI") && dac.InventoryID.HasValue && !flag5)
      isEnabled8 = true;
    this.SetEnabledAndPersistingCheck<FSSODet.estimatedDuration>(cache, eRow, eventType, isEnabled8, new PXPersistingCheck?());
    if (fsAppointmentDet != null)
      this.SetEnabledAndPersistingCheck<FSAppointmentDet.actualDuration>(cache, eRow, eventType, isEnabled8 & docAllowsActualFieldEdition, new PXPersistingCheck?());
    bool flag18 = false;
    if (flag4 && dac.BillingRule != "TIME")
    {
      nullable1 = dac.IsPrepaid;
      bool flag19 = false;
      if (nullable1.GetValueOrDefault() == flag19 & nullable1.HasValue && dac.InventoryID.HasValue && !flag5)
        flag18 = true;
    }
    this.SetEnabledAndPersistingCheck<FSSODet.estimatedQty>(cache, eRow, eventType, flag18 || ((PXGraph) this).IsImport, new PXPersistingCheck?());
    if (fsAppointmentDet != null)
      this.SetEnabledAndPersistingCheck<FSAppointmentDet.actualQty>(cache, eRow, eventType, flag18 & docAllowsActualFieldEdition, new PXPersistingCheck?());
    bool isEnabled9 = false;
    PXPersistingCheck pxPersistingCheck2 = (PXPersistingCheck) 2;
    int? nullable2;
    if (dac.InventoryID.HasValue && fsSrvOrdTypeRow?.PostTo != "AR" && !flag5)
    {
      nullable1 = dac.IsPrepaid;
      bool flag20 = false;
      if (nullable1.GetValueOrDefault() == flag20 & nullable1.HasValue)
      {
        if (fsAppointmentDet != null)
        {
          FSSODet fssoDet2 = FSSODet.UK.Find(cache.Graph, fsAppointmentDet.SODetID);
          int num5;
          if (fssoDet2 != null)
          {
            if (fssoDet2.ApptCntr.GetValueOrDefault() == 1)
            {
              nullable2 = fsAppointmentDet.AppDetID;
              int num6 = 0;
              num5 = nullable2.GetValueOrDefault() > num6 & nullable2.HasValue ? 1 : 0;
            }
            else
              num5 = 0;
          }
          else
            num5 = 1;
          isEnabled9 = num5 != 0;
        }
        else
          isEnabled9 = true;
        pxPersistingCheck2 = (PXPersistingCheck) 1;
      }
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      this.SetEnabledAndPersistingCheck<FSSODet.siteID>(cache, eRow, eventType, isEnabled9, new PXPersistingCheck?(pxPersistingCheck2));
      this.SetEnabledAndPersistingCheck<FSSODet.siteLocationID>(cache, eRow, eventType, flag6 & isEnabled9, new PXPersistingCheck?(!isEnabled9 || !(dac.LineType == "SLPRO") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1));
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
    {
      bool isEnabled10 = false;
      PXPersistingCheck pxPersistingCheck3 = (PXPersistingCheck) 2;
      if (flag4)
      {
        nullable2 = dac.InventoryID;
        if (nullable2.HasValue && !flag5)
        {
          isEnabled10 = true;
          pxPersistingCheck3 = (PXPersistingCheck) 1;
        }
      }
      this.SetEnabledAndPersistingCheck<FSSODet.projectID>(cache, eRow, eventType, isEnabled10, new PXPersistingCheck?(pxPersistingCheck3));
      bool isEnabled11 = true;
      PXPersistingCheck pxPersistingCheck4 = (PXPersistingCheck) 2;
      if (flag4)
      {
        nullable2 = dac.InventoryID;
        if (nullable2.HasValue)
        {
          nullable1 = dac.ContractRelated;
          if (!nullable1.GetValueOrDefault() && !flag5)
          {
            if (ProjectDefaultAttribute.IsProject(cache.Graph, dac.ProjectID))
            {
              pxPersistingCheck4 = (PXPersistingCheck) 1;
              goto label_83;
            }
            goto label_83;
          }
        }
      }
      isEnabled11 = false;
label_83:
      this.SetEnabledAndPersistingCheck<FSSODet.projectTaskID>(cache, eRow, eventType, isEnabled11, new PXPersistingCheck?(pxPersistingCheck4));
    }
    bool isEnabled12 = true;
    PXPersistingCheck pxPersistingCheck5 = (PXPersistingCheck) 2;
    if (flag4)
    {
      nullable2 = dac.InventoryID;
      if (nullable2.HasValue)
      {
        nullable1 = dac.ContractRelated;
        if (!nullable1.GetValueOrDefault() && !flag5)
        {
          if (ProjectDefaultAttribute.IsProject(cache.Graph, dac.ProjectID))
          {
            pxPersistingCheck5 = (PXPersistingCheck) 1;
            goto label_90;
          }
          goto label_90;
        }
      }
    }
    isEnabled12 = false;
label_90:
    this.SetEnabledAndPersistingCheck<FSSODet.costCodeID>(cache, eRow, eventType, isEnabled12, new PXPersistingCheck?(pxPersistingCheck5));
    bool isEnabled13 = false;
    PXPersistingCheck pxPersistingCheck6 = (PXPersistingCheck) 2;
    if (flag4)
    {
      nullable2 = dac.InventoryID;
      if (nullable2.HasValue && fsServiceOrderRow != null)
      {
        nullable1 = fsServiceOrderRow.Quote;
        bool flag21 = false;
        if (nullable1.GetValueOrDefault() == flag21 & nullable1.HasValue && !flag5 && fsSrvOrdTypeRow?.Behavior != "IN")
        {
          nullable1 = dac.IsPrepaid;
          bool flag22 = false;
          if (nullable1.GetValueOrDefault() == flag22 & nullable1.HasValue)
          {
            isEnabled13 = true;
            pxPersistingCheck6 = (PXPersistingCheck) 1;
          }
        }
      }
    }
    this.SetEnabledAndPersistingCheck<FSSODet.acctID>(cache, eRow, eventType, isEnabled13 || ((PXGraph) this).IsImport, new PXPersistingCheck?(pxPersistingCheck6));
    this.SetEnabledAndPersistingCheck<FSSODet.subID>(cache, eRow, eventType, isEnabled13, new PXPersistingCheck?(pxPersistingCheck6));
    if (fsAppointmentDet != null)
    {
      this.SetEnabledAndPersistingCheck<FSAppointmentDet.acctID>(cache, eRow, eventType, isEnabled13 || ((PXGraph) this).IsImport, new PXPersistingCheck?(pxPersistingCheck6));
      this.SetEnabledAndPersistingCheck<FSAppointmentDet.subID>(cache, eRow, eventType, isEnabled13, new PXPersistingCheck?(pxPersistingCheck6));
    }
    if (fsAppointmentDet != null)
    {
      bool isEnabled14 = false;
      PXPersistingCheck pxPersistingCheck7 = (PXPersistingCheck) 2;
      if (flag7)
      {
        isEnabled14 = true;
        pxPersistingCheck7 = (PXPersistingCheck) 1;
      }
      this.SetEnabledAndPersistingCheck<FSAppointmentDet.pickupDeliveryAppLineRef>(cache, eRow, eventType, isEnabled14, new PXPersistingCheck?(pxPersistingCheck7));
    }
    if (fsAppointmentDet != null)
    {
      bool isEnabled15 = false;
      PXPersistingCheck pxPersistingCheck8 = (PXPersistingCheck) 2;
      if (dac.LineType == "SLPRO")
        isEnabled15 = true;
      this.SetEnabledAndPersistingCheck<FSSODet.lotSerialNbr>(cache, eRow, eventType, isEnabled15, new PXPersistingCheck?(pxPersistingCheck8));
    }
    bool isEnabled16 = true;
    nullable1 = dac.IsPrepaid;
    if (nullable1.GetValueOrDefault() || flag5)
      isEnabled16 = false;
    this.SetEnabledAndPersistingCheck<FSSODet.taxCategoryID>(cache, eRow, eventType, isEnabled16, new PXPersistingCheck?());
    if (flag7)
    {
      bool isEnabled17 = false;
      this.SetEnabledAndPersistingCheck<FSSODet.staffID>(cache, eRow, eventType, isEnabled17, new PXPersistingCheck?());
    }
    bool isEnabled18 = false;
    if (dac.LineType == "SERVI")
    {
      if (fssoDet1 != null)
      {
        nullable1 = fssoDet1.EnableStaffID;
        if (!nullable1.GetValueOrDefault())
          goto label_113;
      }
      isEnabled18 = true;
    }
label_113:
    this.SetEnabledAndPersistingCheck<FSSODet.staffID>(cache, eRow, eventType, isEnabled18, new PXPersistingCheck?());
    bool isEnabled19 = true;
    PXPersistingCheck pxPersistingCheck9 = (PXPersistingCheck) 2;
    if (!flag4)
      pxPersistingCheck9 = (PXPersistingCheck) 1;
    this.SetEnabledAndPersistingCheck<FSSODet.tranDesc>(cache, eRow, eventType, isEnabled19, new PXPersistingCheck?(pxPersistingCheck9));
    bool isEnabled20 = false;
    nullable2 = dac.InventoryID;
    if (nullable2.HasValue && flag4 && !flag5)
      isEnabled20 = true;
    this.SetEnabledAndPersistingCheck<FSSODet.taxCategoryID>(cache, eRow, eventType, isEnabled20, new PXPersistingCheck?());
    bool flag23 = false;
    nullable1 = dac.EnablePO;
    if (nullable1.GetValueOrDefault() && (fssoDet1 == null || fssoDet1.POSource == "O"))
      flag23 = true;
    bool flag24 = flag23 && (fsSrvOrdTypeRow.PostTo != "PM" || fsSrvOrdTypeRow.PostTo == "PM" && fsSrvOrdTypeRow.BillingType != "CC") || !flag3 && (dac.LineType == "NSTKI" || dac.LineType == "SERVI");
    this.SetEnabledAndPersistingCheck<FSSODet.curyUnitCost>(cache, eRow, eventType, flag24 || ((PXGraph) this).IsImport, new PXPersistingCheck?());
    bool isEnabled21 = false;
    nullable2 = dac.InventoryID;
    if (nullable2.HasValue && dac.LineType == "SLPRO" && !flag5)
      isEnabled21 = true;
    this.SetEnabledAndPersistingCheck<FSSODet.equipmentAction>(cache, eRow, eventType, isEnabled21, new PXPersistingCheck?());
    SharedFunctions.SetEquipmentFieldEnablePersistingCheck(cache, eRow, false);
    if (flag5)
    {
      bool isEnabled22 = false;
      this.SetEnabledAndPersistingCheck<FSSODet.SMequipmentID>(cache, eRow, eventType, isEnabled22, new PXPersistingCheck?());
    }
    bool isEnabled23 = false;
    if (fsServiceOrderRow != null)
    {
      nullable1 = fsServiceOrderRow.AllowInvoice;
      bool flag25 = false;
      if (nullable1.GetValueOrDefault() == flag25 & nullable1.HasValue)
        isEnabled23 = true;
    }
    this.SetEnabledAndPersistingCheck<FSSODet.branchID>(cache, eRow, eventType, isEnabled23, new PXPersistingCheck?());
    bool isEnabled24 = false;
    string str1 = fsSrvOrdTypeRow?.PostTo ?? string.Empty;
    string str2 = dac.Status ?? string.Empty;
    if (flag4)
    {
      nullable1 = dac.IsPrepaid;
      bool flag26 = false;
      if (nullable1.GetValueOrDefault() == flag26 & nullable1.HasValue && EnumerableExtensions.IsIn<string>(str1, "SO", "SI", "PM"))
      {
        if (fsAppointmentDet != null && !EnumerableExtensions.IsIn<string>(str2, "NS", "CP", "RP"))
        {
          nullable1 = fsAppointmentDet.CanChangeMarkForPO;
          if (!nullable1.GetValueOrDefault())
            goto label_135;
        }
        if (fssoDet1 != null)
        {
          nullable1 = fssoDet1.EnablePO;
          bool flag27 = false;
          if (!(nullable1.GetValueOrDefault() == flag27 & nullable1.HasValue) && !(fssoDet1.POSource == "O"))
            goto label_135;
        }
        if (fsServiceOrderRow != null)
        {
          nullable1 = fsServiceOrderRow.AllowInvoice;
          bool flag28 = false;
          if (nullable1.GetValueOrDefault() == flag28 & nullable1.HasValue)
            isEnabled24 = true;
        }
      }
    }
label_135:
    this.SetEnabledAndPersistingCheck<FSSODet.enablePO>(cache, eRow, eventType, isEnabled24, new PXPersistingCheck?());
    this.SetEnabledAndPersistingCheck<FSSODet.pOCreate>(cache, eRow, eventType, isEnabled24, new PXPersistingCheck?());
    PXCache cache2 = cache;
    object row2 = eRow;
    int num7 = (int) eventType;
    int num8;
    if (isEnabled24)
    {
      nullable1 = dac.EnablePO;
      num8 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num8 = 0;
    PXPersistingCheck? persistingCheck2 = new PXPersistingCheck?();
    this.SetEnabledAndPersistingCheck<FSSODet.poVendorID>(cache2, row2, (ServiceOrderBase<TGraph, TPrimary>.EventType) num7, num8 != 0, persistingCheck2);
    PXCache cache3 = cache;
    object row3 = eRow;
    int num9 = (int) eventType;
    int num10;
    if (isEnabled24)
    {
      nullable1 = dac.EnablePO;
      num10 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num10 = 0;
    PXPersistingCheck? persistingCheck3 = new PXPersistingCheck?();
    this.SetEnabledAndPersistingCheck<FSSODet.poVendorLocationID>(cache3, row3, (ServiceOrderBase<TGraph, TPrimary>.EventType) num9, num10 != 0, persistingCheck3);
  }

  public virtual void X_IsPrepaid_FieldUpdated<DAC, ManualPrice, IsFree, EstimatedDuration, ActualDuration>(
    PXCache cache,
    PXFieldUpdatedEventArgs e,
    bool useActualField)
    where DAC : class, IBqlTable, IFSSODetBase, new()
    where ManualPrice : class, IBqlField
    where IsFree : class, IBqlField
    where EstimatedDuration : class, IBqlField
    where ActualDuration : class, IBqlField
  {
    if (e.Row == null || !((DAC) e.Row).IsPrepaid.GetValueOrDefault())
      return;
    cache.SetValueExt<IsFree>(e.Row, (object) true);
    cache.RaiseFieldUpdated<EstimatedDuration>(e.Row, (object) 0);
    if (!useActualField)
      return;
    cache.RaiseFieldUpdated<ActualDuration>(e.Row, (object) 0);
  }

  public virtual void X_InventoryID_FieldUpdated<DAC, AcctID, SubItemID, SiteID, SiteLocationID, UOM, EstimatedDuration, EstimatedQty, BillingRule, ActualDuration, ActualQty>(
    PXCache cache,
    PXFieldUpdatedEventArgs e,
    int? branchLocationID,
    PX.Objects.AR.Customer billCustomerRow,
    bool useActualFields)
    where DAC : class, IBqlTable, IFSSODetBase, new()
    where AcctID : class, IBqlField
    where SubItemID : class, IBqlField
    where SiteID : class, IBqlField
    where SiteLocationID : class, IBqlField
    where UOM : class, IBqlField
    where EstimatedDuration : class, IBqlField
    where EstimatedQty : class, IBqlField
    where BillingRule : class, IBqlField
    where ActualDuration : class, IBqlField
    where ActualQty : class, IBqlField
  {
    if (e.Row == null)
      return;
    DAC row1 = (DAC) e.Row;
    PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow(cache.Graph, row1.InventoryID);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(cache.Graph, row1.InventoryID, cache.Graph.Accessinfo.BaseCuryID);
    if (inventoryItemRow != null && row1.LineType == null)
    {
      if (inventoryItemRow.StkItem.GetValueOrDefault())
        row1.LineType = "SLPRO";
      else if (inventoryItemRow.ItemType == "S")
        row1.LineType = "SERVI";
      else
        row1.LineType = "NSTKI";
      cache.SetDefaultExt<AcctID>((object) row1);
    }
    if (e.ExternalCall)
      row1.CuryUnitPrice = new Decimal?(0M);
    int? nullable1;
    if (this.IsInventoryLine(row1.LineType))
    {
      nullable1 = row1.InventoryID;
      if (nullable1.HasValue || !(row1.LineType != "PU_DL"))
      {
        row1.TranDesc = (string) null;
        if (inventoryItemRow != null)
          row1.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(cache.Graph.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItemRow, "Descr", billCustomerRow?.LocaleName);
        FSxService fsxService = (FSxService) null;
        row1.UOM = (string) null;
        cache.RaiseExceptionHandling<UOM>(e.Row, (object) null, (Exception) null);
        if (row1.LineType == "SERVI" && inventoryItemRow != null)
        {
          fsxService = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxService>(inventoryItemRow);
          cache.SetValueExt<BillingRule>(e.Row, (object) fsxService.BillingRule);
        }
        else
          cache.SetValueExt<BillingRule>(e.Row, (object) "FLRA");
        cache.SetDefaultExt<SubItemID>(e.Row);
        object obj1 = (object) null;
        cache.RaiseFieldDefaulting<SiteID>(e.Row, ref obj1);
        int? SiteID = (int?) obj1;
        bool flag = true;
        if (inventoryItemRow != null)
        {
          nullable1 = row1.SiteLocationID;
          if (!nullable1.HasValue && row1.LineType == "SLPRO")
          {
            if (!SiteID.HasValue)
              SiteID = itemCurySettings.DfltSiteID;
            if (!SiteID.HasValue)
            {
              INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXSelectJoin<INItemSite, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<INItemSite.siteID>>>, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>.Config>.Select(cache.Graph, new object[1]
              {
                (object) inventoryItemRow.InventoryID
              }));
              if (inItemSite != null)
              {
                SiteID = inItemSite.SiteID;
                flag = false;
              }
            }
          }
        }
        int? subItemId = row1.SubItemID;
        object obj2 = (object) null;
        cache.RaiseFieldDefaulting<UOM>(e.Row, ref obj2);
        string UOM = (string) obj2;
        this.CompleteItemInfoUsingBranchLocation(cache.Graph, branchLocationID, inventoryItemRow != null ? inventoryItemRow.DefaultSubItemOnEntry : new bool?(false), ref subItemId, ref UOM, ref SiteID);
        row1.SubItemID = subItemId;
        string str = string.Empty;
        if (cache.Graph is ServiceOrderEntry)
          str = ((PXSelectBase<FSSrvOrdType>) (cache.Graph as ServiceOrderEntry).ServiceOrderTypeSelected).Current?.PostTo;
        else if (cache.Graph is AppointmentEntry)
          str = ((PXSelectBase<FSSrvOrdType>) (cache.Graph as AppointmentEntry).ServiceOrderTypeSelected).Current?.PostTo;
        if (flag)
          SiteID = this.GetValidatedSiteID(cache.Graph, SiteID);
        if (SiteID.HasValue && str != "AR")
          cache.SetValueExt<SiteID>(e.Row, (object) SiteID);
        nullable1 = row1.SiteLocationID;
        int? nullable2;
        if (!nullable1.HasValue && inventoryItemRow != null && str != "AR")
        {
          nullable1 = row1.SiteID;
          nullable2 = itemCurySettings.DfltSiteID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && row1.LineType == "SLPRO")
            row1.SiteLocationID = itemCurySettings.DfltShipLocationID;
        }
        cache.SetValueExt<UOM>(e.Row, (object) UOM);
        row1.SetDuration(FieldType.EstimatedField, new int?(0), cache, false);
        if ((cache.GetStatus((object) row1) != null ? 0 : (cache.Locate((object) row1) == null ? 1 : 0)) == 0 || !cache.Graph.IsContractBasedAPI)
          cache.SetDefaultExt<EstimatedQty>(e.Row);
        if (row1.LineType == "SERVI" && fsxService != null)
        {
          PXCache pxCache = cache;
          object row2 = e.Row;
          nullable2 = fsxService.EstimatedDuration;
          // ISSUE: variable of a boxed type
          __Boxed<int> valueOrDefault = (ValueType) nullable2.GetValueOrDefault();
          pxCache.SetValueExt<EstimatedDuration>(row2, (object) valueOrDefault);
        }
        if (!useActualFields)
          return;
        cache.SetDefaultExt<ActualQty>(e.Row);
        cache.SetDefaultExt<ActualDuration>(e.Row);
        return;
      }
    }
    row1.IsFree = new bool?(true);
    row1.ManualPrice = new bool?(false);
    // ISSUE: variable of a boxed type
    __Boxed<DAC> local1 = (object) row1;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    local1.SubItemID = nullable3;
    // ISSUE: variable of a boxed type
    __Boxed<DAC> local2 = (object) row1;
    nullable1 = new int?();
    int? nullable4 = nullable1;
    local2.SiteID = nullable4;
    // ISSUE: variable of a boxed type
    __Boxed<DAC> local3 = (object) row1;
    nullable1 = new int?();
    int? nullable5 = nullable1;
    local3.SiteLocationID = nullable5;
    cache.SetDefaultExt<FSSODet.uOM>(e.Row);
    cache.RaiseExceptionHandling<UOM>(e.Row, (object) null, (Exception) null);
    row1.SetDuration(FieldType.EstimatedField, new int?(0), cache, false);
    row1.SetQty(FieldType.EstimatedField, new Decimal?(0M), cache, false);
    if (useActualFields)
    {
      row1.SetDuration(FieldType.ActualField, new int?(0), cache, false);
      row1.SetQty(FieldType.ActualField, new Decimal?(0M), cache, false);
    }
    row1.BillingRule = "NONE";
  }

  public virtual void X_BillingRule_FieldVerifying<DAC>(PXCache cache, PXFieldVerifyingEventArgs e) where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    if (e.Row == null)
      return;
    DAC row = (DAC) e.Row;
    if (!this.IsInventoryLine(row.LineType) || !row.InventoryID.HasValue)
    {
      e.NewValue = (object) "NONE";
    }
    else
    {
      if (!(row.LineType == "NSTKI") && !(row.LineType == "SLPRO") && !(row.LineType == "PU_DL"))
        return;
      e.NewValue = (object) "FLRA";
    }
  }

  public virtual void X_BillingRule_FieldUpdated<DAC, EstimatedDuration, ActualDuration, CuryUnitPrice, IsFree>(
    PXCache cache,
    PXFieldUpdatedEventArgs e,
    bool useActualField)
    where DAC : class, IBqlTable, IFSSODetBase, new()
    where EstimatedDuration : class, IBqlField
    where ActualDuration : class, IBqlField
    where CuryUnitPrice : class, IBqlField
    where IsFree : class, IBqlField
  {
    if (e.Row == null)
      return;
    DAC row = (DAC) e.Row;
    string billingRule = row.BillingRule;
    string oldValue = (string) e.OldValue;
    if (billingRule != oldValue && (billingRule == "NONE" || oldValue == "NONE"))
      cache.SetDefaultExt<IsFree>(e.Row);
    if (row.LineType == "SERVI" && row.BillingRule == "TIME")
    {
      cache.RaiseFieldUpdated<EstimatedDuration>(e.Row, (object) 0);
      if (!useActualField)
        return;
      cache.RaiseFieldUpdated<ActualDuration>(e.Row, (object) 0);
    }
    else
      cache.SetDefaultExt<CuryUnitPrice>(e.Row);
  }

  public virtual void X_UOM_FieldUpdated<CuryUnitPrice>(PXCache cache, PXFieldUpdatedEventArgs e) where CuryUnitPrice : class, IBqlField
  {
    if (e.Row == null)
      return;
    cache.SetDefaultExt<CuryUnitPrice>(e.Row);
  }

  public virtual void X_SiteID_FieldUpdated<CuryUnitPrice, AcctID, SubID>(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
    where CuryUnitPrice : class, IBqlField
    where AcctID : class, IBqlField
    where SubID : class, IBqlField
  {
    if (e.Row == null)
      return;
    cache.SetDefaultExt<CuryUnitPrice>(e.Row);
    cache.SetDefaultExt<AcctID>(e.Row);
    try
    {
      cache.SetDefaultExt<SubID>(e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      cache.SetValue<SubID>(e.Row, (object) null);
    }
  }

  public virtual void X_Qty_FieldUpdated<CuryUnitPrice>(PXCache cache, PXFieldUpdatedEventArgs e) where CuryUnitPrice : class, IBqlField
  {
    if (e.Row == null)
      return;
    cache.SetDefaultExt<CuryUnitPrice>(e.Row);
  }

  public virtual void X_ManualPrice_FieldUpdated<DAC, CuryUnitPrice, CuryBillableExtPrice>(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
    where DAC : class, IFSSODetBase, new()
    where CuryUnitPrice : class, IBqlField
    where CuryBillableExtPrice : class, IBqlField
  {
    if (e.Row == null)
      return;
    DAC row = (DAC) e.Row;
    cache.SetDefaultExt<CuryUnitPrice>(e.Row);
    bool? nullable = row.IsFree;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.ManualPrice;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    cache.SetValueExt<CuryBillableExtPrice>(e.Row, (object) 0M);
  }

  public virtual void X_CuryUnitPrice_FieldDefaulting<DAC, CuryUnitPrice>(
    PXCache cache,
    PXFieldDefaultingEventArgs e,
    Decimal? qty,
    DateTime? docDate,
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo)
    where DAC : class, IBqlTable, IFSSODetBase, new()
    where CuryUnitPrice : class, IBqlField
  {
    if (e.Row == null)
      return;
    DAC row = (DAC) e.Row;
    FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) null;
    if (typeof (DAC) == typeof (FSAppointmentDet))
      fsAppointmentDet = (FSAppointmentDet) e.Row;
    if (row.InventoryID.HasValue && row.UOM != null)
    {
      bool? nullable;
      if (row.BillingRule == "NONE")
      {
        nullable = row.ManualPrice;
        if (!nullable.GetValueOrDefault())
          goto label_7;
      }
      nullable = row.ManualPrice;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.IsFree;
        if (!nullable.GetValueOrDefault())
        {
          bool baseCurrencySetting = ARSalesPriceMaint.SingleARSalesPriceMaint.GetAlwaysFromBaseCurrencySetting(cache);
          SalesPriceSet customerContract = FSPriceManagement.CalculateSalesPriceWithCustomerContract(cache, fsServiceOrderRow.ServiceContractID, fsAppointmentRow != null ? fsAppointmentRow.BillServiceContractID : fsServiceOrderRow.BillServiceContractID, fsAppointmentRow != null ? fsAppointmentRow.BillContractPeriodID : fsServiceOrderRow.BillContractPeriodID, fsServiceOrderRow.BillCustomerID, fsServiceOrderRow.BillLocationID, row.ContractRelated, row.InventoryID, row.SiteID, qty, row.UOM, (docDate ?? cache.Graph.Accessinfo.BusinessDate).Value, row.CuryUnitPrice, baseCurrencySetting, currencyInfo?.GetCM(), false);
          e.NewValue = !(customerContract.ErrorCode == "UOM_INCONSISTENCY") ? (object) customerContract.Price.GetValueOrDefault() : throw new PXException(PXMessages.LocalizeFormatNoPrefix("There is an inconsistency in the UOM defined on the Sales Prices (AR202000) form and on the Non-Stock Items (IN202000) form for the {0} service.", new object[1]
          {
            (object) SharedFunctions.GetInventoryItemRow(cache.Graph, row.InventoryID).InventoryCD
          }), new object[1]{ (object) (PXErrorLevel) 4 });
          if (fsAppointmentDet != null)
          {
            fsAppointmentDet.PriceType = customerContract.PriceType;
            fsAppointmentDet.PriceCode = customerContract.PriceCode;
          }
          ARSalesPriceMaint.CheckNewUnitPrice<DAC, CuryUnitPrice>(cache, row, (object) customerContract.Price);
          return;
        }
      }
      e.NewValue = (object) row.CuryUnitPrice.GetValueOrDefault();
      ((CancelEventArgs) e).Cancel = row.CuryUnitPrice.HasValue;
      return;
    }
label_7:
    PXUIFieldAttribute.SetWarning<CuryUnitPrice>(cache, (object) row, (string) null);
    e.NewValue = (object) 0M;
    if (fsAppointmentDet == null)
      return;
    fsAppointmentDet.PriceType = (string) null;
    fsAppointmentDet.PriceCode = (string) null;
  }

  public virtual void X_Duration_FieldUpdated<DAC, Qty>(
    PXCache cache,
    PXFieldUpdatedEventArgs e,
    int? duration)
    where DAC : class, IBqlTable, IFSSODetBase, new()
    where Qty : class, IBqlField
  {
    if (e.Row == null)
      return;
    DAC row = (DAC) e.Row;
    if (!(row.LineType == "SERVI") || !(row.BillingRule == "TIME"))
      return;
    bool? isPrepaid = row.IsPrepaid;
    bool flag = false;
    if (!(isPrepaid.GetValueOrDefault() == flag & isPrepaid.HasValue))
      return;
    cache.SetValueExt<Qty>(e.Row, (object) PXDBQuantityAttribute.Round(new Decimal?(Decimal.Divide((Decimal) duration.GetValueOrDefault(), 60M))));
  }

  public virtual void CompleteItemInfoUsingBranchLocation(
    PXGraph graph,
    int? branchLocationID,
    bool? defaultSubItemOnEntry,
    ref int? SubItemID,
    ref string UOM,
    ref int? SiteID)
  {
    if (!branchLocationID.HasValue || (SubItemID.HasValue || !defaultSubItemOnEntry.GetValueOrDefault()) && !string.IsNullOrEmpty(UOM) && SiteID.HasValue)
      return;
    FSBranchLocation fsBranchLocation = FSBranchLocation.PK.Find(graph, branchLocationID);
    if (fsBranchLocation == null)
      return;
    if (!SubItemID.HasValue && defaultSubItemOnEntry.GetValueOrDefault())
      SubItemID = fsBranchLocation.DfltSubItemID;
    if (string.IsNullOrEmpty(UOM))
      UOM = fsBranchLocation.DfltUOM;
    if (SiteID.HasValue)
      return;
    SiteID = fsBranchLocation.DfltSiteID;
  }

  public virtual bool IsInventoryLine(string lineType)
  {
    return lineType != null && !(lineType == "CM_LN") && !(lineType == "IT_LN") && !(lineType == "TEMPL");
  }

  public virtual void CheckIfManualPrice<DAC, Qty>(PXCache cache, PXRowUpdatedEventArgs e)
    where DAC : class, IBqlTable, IFSSODetBase, new()
    where Qty : class, IBqlField
  {
    if (e.Row == null)
      return;
    DAC row = (DAC) e.Row;
    if ((string) cache.GetValue<FSSODet.billingRule>(e.Row) == "NONE" || (string) cache.GetValue<FSSODet.billingRule>(e.OldRow) == "NONE" || !e.ExternalCall && !cache.Graph.IsImport || !cache.ObjectsEqual<FSSODet.branchID>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.inventoryID>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.uOM>(e.Row, e.OldRow) || !cache.ObjectsEqual<Qty>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.siteID>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.manualPrice>(e.Row, e.OldRow) || cache.ObjectsEqual<FSSODet.curyUnitPrice>(e.Row, e.OldRow) && cache.ObjectsEqual<FSSODet.curyBillableExtPrice>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.status>(e.Row, e.OldRow))
      return;
    row.ManualPrice = new bool?(true);
  }

  public virtual void CheckSOIfManualCost(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null || !e.ExternalCall && !cache.Graph.IsImport || !cache.ObjectsEqual<FSSODet.branchID>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.inventoryID>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.uOM>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.siteID>(e.Row, e.OldRow) || !cache.ObjectsEqual<FSSODet.manualCost>(e.Row, e.OldRow) || cache.ObjectsEqual<FSSODet.curyUnitCost>(e.Row, e.OldRow))
      return;
    if (e.Row is FSSODet)
    {
      ((FSSODet) e.Row).ManualCost = new bool?(true);
    }
    else
    {
      if (!(e.Row is FSAppointmentDet))
        return;
      ((FSAppointmentDet) e.Row).ManualCost = new bool?(true);
    }
  }

  public virtual void X_AcctID_FieldDefaulting<DAC>(
    PXCache cache,
    PXFieldDefaultingEventArgs e,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSServiceOrder fsServiceOrderRow)
    where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    if (e.Row == null || fsSrvOrdTypeRow == null || fsServiceOrderRow == null)
      return;
    DAC row = (DAC) e.Row;
    if (!this.IsInventoryLine(row.LineType))
      e.NewValue = (object) null;
    else
      e.NewValue = (object) this.Get_TranAcctID_DefaultValue(cache.Graph, fsSrvOrdTypeRow.SalesAcctSource, row.InventoryID, row.SiteID, fsServiceOrderRow);
  }

  public virtual void X_SubID_FieldDefaulting<DAC>(
    PXCache cache,
    PXFieldDefaultingEventArgs e,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSServiceOrder fsServiceOrderRow)
    where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    if (e.Row == null || fsSrvOrdTypeRow == null || fsServiceOrderRow == null)
      return;
    DAC row = (DAC) e.Row;
    if (!row.AcctID.HasValue)
      return;
    e.NewValue = this.Get_IFSSODetBase_SubID_DefaultValue(cache, (IFSSODetBase) row, fsServiceOrderRow, fsSrvOrdTypeRow);
  }

  public virtual void X_UOM_FieldDefaulting<DAC>(PXCache cache, PXFieldDefaultingEventArgs e) where DAC : class, IBqlTable, IFSSODetBase, new()
  {
    if (e.Row == null)
      return;
    IFSSODetBase row = (IFSSODetBase) e.Row;
    string str = PXResultset<CommonSetup>.op_Implicit(PXSelectBase<CommonSetup, PXSelect<CommonSetup>.Config>.Select(cache.Graph, Array.Empty<object>()))?.WeightUOM;
    if (row.InventoryID.HasValue)
      str = PX.Objects.IN.InventoryItem.PK.Find(cache.Graph, row.InventoryID)?.SalesUnit;
    e.NewValue = (object) str;
  }

  /// <summary>
  /// If the given line is prepaid then disable all its editable fields.
  /// </summary>
  /// <param name="cacheAppointmentDet">Cache of the Appointment Detail.</param>
  /// <param name="fsAppointmentDetRow">Appointment Detail row.</param>
  public virtual void DisablePrepaidLine(
    PXCache cacheAppointmentDet,
    FSAppointmentDet fsAppointmentDetRow)
  {
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.tranDesc>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.lineType>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.inventoryID>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.billingRule>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.isFree>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.actualQty>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.estimatedQty>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.curyUnitPrice>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.projectTaskID>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.siteID>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.siteLocationID>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.acctID>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.subID>(cacheAppointmentDet, (object) fsAppointmentDetRow, false);
  }

  public virtual void FSAppointmentDet_RowSelected_PartialHandler(
    PXCache cacheAppointmentDet,
    FSAppointmentDet fsAppointmentDetRow,
    FSSetup fsSetupRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow = null,
    FSServiceContract fsServiceContractRow = null)
  {
    this.EnableDisable_LineType(cacheAppointmentDet, fsAppointmentDetRow, fsSetupRow, fsAppointmentRow, fsSrvOrdTypeRow, fsServiceContractRow);
    if (fsAppointmentDetRow.IsPrepaid.GetValueOrDefault())
      this.DisablePrepaidLine(cacheAppointmentDet, fsAppointmentDetRow);
    else
      this.EnableDisable_Acct_Sub(cacheAppointmentDet, (IFSSODetBase) fsAppointmentDetRow, fsSrvOrdTypeRow, fsServiceOrderRow);
  }

  public virtual void FSAppointmentDet_RowPersisting_PartialHandler(
    PXCache cacheAppointmentDet,
    FSAppointmentDet fsAppointmentDetRow,
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsAppointmentDetRow.LineType == "SLPRO" && fsSrvOrdTypeRow.PostTo == "SO" && fsAppointmentDetRow.LastModifiedByScreenID != "FS500200" && !fsAppointmentDetRow.SiteID.HasValue)
      cacheAppointmentDet.RaiseExceptionHandling<FSAppointmentDet.siteID>((object) fsAppointmentDetRow, (object) null, (Exception) new PXSetPropertyException("Data is required for the selected line type.", (PXErrorLevel) 4));
    this.ValidateQty(cacheAppointmentDet, fsAppointmentDetRow);
  }

  public virtual void RefreshSalesPricesInTheWholeDocument(
    PXSelectBase<FSAppointmentDet> appointmentDetails)
  {
    foreach (PXResult<FSAppointmentDet> pxResult in appointmentDetails.Select(Array.Empty<object>()))
    {
      FSAppointmentDet fsAppointmentDet = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
      ((PXSelectBase) appointmentDetails).Cache.SetDefaultExt<FSAppointmentDet.curyUnitPrice>((object) fsAppointmentDet);
      ((PXSelectBase) appointmentDetails).Cache.Update((object) fsAppointmentDet);
    }
  }

  public virtual bool ShouldShowMarkForPOFields(PX.Objects.SO.SOOrderType currentSOOrderType)
  {
    return ((bool?) currentSOOrderType?.RequireShipping).GetValueOrDefault();
  }

  public virtual void OpenEmployeeBoard_Handler(PXGraph graph, FSServiceOrder fsServiceOrder)
  {
    if (fsServiceOrder == null)
      return;
    bool? openDoc = fsServiceOrder.OpenDoc;
    bool flag = false;
    if (openDoc.GetValueOrDefault() == flag & openDoc.HasValue)
      throw new PXException("This action is invalid for the current service order status.");
    graph.GetSaveAction().Press();
    PXResultset<FSSODet> bqlResultSet_FSSODet = new PXResultset<FSSODet>();
    this.GetPendingLines(graph, fsServiceOrder.SOID, ref bqlResultSet_FSSODet);
    if (bqlResultSet_FSSODet.Count > 0)
      throw PXRedirectToBoardRequiredException.GenerateMultiEmployeeRedirect(this.SiteMapProvider, this.GetServiceOrderUrlArguments(fsServiceOrder), new MainAppointmentFilter()
      {
        InitialSORefNbr = fsServiceOrder.RefNbr
      }, (PXBaseRedirectException.WindowMode) 1);
    throw new PXException("The current document does not have services to schedule.");
  }

  /// <summary>
  /// Returns the url arguments for a Service Order [fsServiceOrderRow].
  /// </summary>
  public virtual KeyValuePair<string, string>[] GetServiceOrderUrlArguments(
    FSServiceOrder fsServiceOrderRow)
  {
    return new KeyValuePair<string, string>[2]
    {
      new KeyValuePair<string, string>(typeof (FSServiceOrder.refNbr).Name, fsServiceOrderRow.RefNbr),
      new KeyValuePair<string, string>("Date", fsServiceOrderRow.OrderDate.Value.ToString())
    };
  }

  public virtual void SetServiceOrderRecord_AsUpdated_IfItsNotchanged(
    PXCache cacheServiceOrder,
    FSServiceOrder fsServiceOrderRow)
  {
    if (cacheServiceOrder.GetStatus((object) fsServiceOrderRow) != null)
      return;
    cacheServiceOrder.SetStatus((object) fsServiceOrderRow, (PXEntryStatus) 1);
  }

  public virtual void DeleteServiceOrder(
    FSServiceOrder fsServiceOrderRow,
    ServiceOrderEntry graphServiceOrderEntry)
  {
    ((PXGraph) graphServiceOrderEntry).Clear();
    ((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) graphServiceOrderEntry.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fsServiceOrderRow.RefNbr, new object[1]
    {
      (object) fsServiceOrderRow.SrvOrdType
    }));
    ((PXAction) graphServiceOrderEntry.Delete).Press();
  }

  public virtual PXResultset<FSAppointment> GetEditableAppointments(
    PXGraph graph,
    int? sOID,
    int? appointmentID)
  {
    return PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where2<Where<FSAppointment.notStarted, Equal<True>>, And<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>, And<FSAppointment.appointmentID, NotEqual<Required<FSAppointment.appointmentID>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) sOID,
      (object) appointmentID
    });
  }

  /// <summary>
  /// Enable / Disable the document depending of the Status of the Appointment [fsAppointmentRow] and ServiceOrder [fsServiceOrderRow].
  /// </summary>
  public virtual void EnableDisable_Document(
    PXGraph graph,
    PXCache cacheServiceOrder,
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    int appointmentCount,
    int detailsCount,
    PXCache cacheServiceOrderDetails,
    PXCache cacheServiceOrderAppointments,
    PXCache cacheServiceOrderEquipment,
    PXCache cacheServiceOrderEmployees,
    PXCache cacheServiceOrder_Contact,
    PXCache cacheServiceOrder_Address,
    bool? isBeingCalledFromQuickProcess,
    bool allowCustomerChange = false)
  {
    bool flag1 = true;
    int? nullable1;
    if (fsServiceOrderRow != null && fsSrvOrdTypeRow != null && fsSrvOrdTypeRow.Behavior != "IN")
    {
      nullable1 = fsServiceOrderRow.CustomerID;
      flag1 = nullable1.HasValue;
    }
    bool? nullable2 = fsServiceOrderRow.Quote;
    bool valueOrDefault1 = nullable2.GetValueOrDefault();
    nullable2 = fsServiceOrderRow.AllowInvoice;
    int num1;
    if (!nullable2.GetValueOrDefault())
    {
      nullable2 = fsServiceOrderRow.Billed;
      num1 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag2 = num1 != 0;
    bool flag3;
    bool flag4;
    if (fsAppointmentRow != null)
    {
      flag3 = this.CanUpdateAppointment(fsAppointmentRow, fsSrvOrdTypeRow);
      flag4 = this.CanDeleteAppointment(fsAppointmentRow, fsServiceOrderRow, fsSrvOrdTypeRow);
    }
    else
    {
      flag4 = this.CanDeleteServiceOrder(graph, fsServiceOrderRow);
      flag3 = this.CanUpdateServiceOrder(fsServiceOrderRow, fsSrvOrdTypeRow);
    }
    cacheServiceOrder.AllowInsert = true;
    cacheServiceOrder.AllowUpdate = flag3 | allowCustomerChange || isBeingCalledFromQuickProcess.GetValueOrDefault();
    nullable2 = fsServiceOrderRow.Canceled;
    if (nullable2.GetValueOrDefault())
      cacheServiceOrder.AllowUpdate = false;
    nullable2 = fsServiceOrderRow.Completed;
    if (nullable2.GetValueOrDefault())
    {
      PXCache pxCache = cacheServiceOrder;
      nullable2 = fsSrvOrdTypeRow.Active;
      int num2 = nullable2.GetValueOrDefault() ? 1 : 0;
      pxCache.AllowUpdate = num2 != 0;
    }
    cacheServiceOrder.AllowDelete = flag4;
    if (cacheServiceOrderDetails != null)
    {
      PXCache pxCache1 = cacheServiceOrderDetails;
      int num3;
      if (flag3 & flag1)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag5 = false;
        num3 = nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
      pxCache1.AllowInsert = num3 != 0;
      cacheServiceOrderDetails.AllowUpdate = flag3 & flag1;
      PXCache pxCache2 = cacheServiceOrderDetails;
      int num4;
      if (flag3 & flag1)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag6 = false;
        num4 = nullable2.GetValueOrDefault() == flag6 & nullable2.HasValue ? 1 : 0;
      }
      else
        num4 = 0;
      pxCache2.AllowDelete = num4 != 0;
    }
    if (graph is ServiceOrderEntry)
    {
      PXCache cache = ((PXSelectBase) ((ServiceOrderEntry) graph).Splits).Cache;
      if (cache != null)
      {
        PXCache pxCache3 = cache;
        int num5;
        if (flag3 & flag1)
        {
          nullable2 = fsServiceOrderRow.AllowInvoice;
          bool flag7 = false;
          num5 = nullable2.GetValueOrDefault() == flag7 & nullable2.HasValue ? 1 : 0;
        }
        else
          num5 = 0;
        pxCache3.AllowInsert = num5 != 0;
        cache.AllowUpdate = flag3 & flag1;
        PXCache pxCache4 = cache;
        int num6;
        if (flag3 & flag1)
        {
          nullable2 = fsServiceOrderRow.AllowInvoice;
          bool flag8 = false;
          num6 = nullable2.GetValueOrDefault() == flag8 & nullable2.HasValue ? 1 : 0;
        }
        else
          num6 = 0;
        pxCache4.AllowDelete = num6 != 0;
      }
    }
    if (cacheServiceOrder_Contact != null)
    {
      PXCache pxCache5 = cacheServiceOrder_Contact;
      int num7;
      if (flag3)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag9 = false;
        num7 = nullable2.GetValueOrDefault() == flag9 & nullable2.HasValue ? 1 : 0;
      }
      else
        num7 = 0;
      pxCache5.AllowInsert = num7 != 0;
      PXCache pxCache6 = cacheServiceOrder_Contact;
      int num8;
      if (flag3)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag10 = false;
        num8 = nullable2.GetValueOrDefault() == flag10 & nullable2.HasValue ? 1 : 0;
      }
      else
        num8 = 0;
      pxCache6.AllowUpdate = num8 != 0;
      PXCache pxCache7 = cacheServiceOrder_Contact;
      int num9;
      if (flag3)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag11 = false;
        num9 = nullable2.GetValueOrDefault() == flag11 & nullable2.HasValue ? 1 : 0;
      }
      else
        num9 = 0;
      pxCache7.AllowDelete = num9 != 0;
    }
    if (cacheServiceOrder_Address != null)
    {
      PXCache pxCache8 = cacheServiceOrder_Address;
      int num10;
      if (flag3)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag12 = false;
        num10 = nullable2.GetValueOrDefault() == flag12 & nullable2.HasValue ? 1 : 0;
      }
      else
        num10 = 0;
      pxCache8.AllowInsert = num10 != 0;
      PXCache pxCache9 = cacheServiceOrder_Address;
      int num11;
      if (flag3)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag13 = false;
        num11 = nullable2.GetValueOrDefault() == flag13 & nullable2.HasValue ? 1 : 0;
      }
      else
        num11 = 0;
      pxCache9.AllowUpdate = num11 != 0;
      PXCache pxCache10 = cacheServiceOrder_Address;
      int num12;
      if (flag3)
      {
        nullable2 = fsServiceOrderRow.AllowInvoice;
        bool flag14 = false;
        num12 = nullable2.GetValueOrDefault() == flag14 & nullable2.HasValue ? 1 : 0;
      }
      else
        num12 = 0;
      pxCache10.AllowDelete = num12 != 0;
    }
    if (cacheServiceOrderAppointments != null)
    {
      cacheServiceOrderAppointments.AllowInsert = flag3;
      cacheServiceOrderAppointments.AllowUpdate = flag3;
      cacheServiceOrderAppointments.AllowDelete = flag3;
    }
    if (cacheServiceOrderEquipment != null)
    {
      cacheServiceOrderEquipment.AllowSelect = !valueOrDefault1;
      cacheServiceOrderEquipment.AllowInsert = flag3 && !valueOrDefault1;
      cacheServiceOrderEquipment.AllowUpdate = flag3 && !valueOrDefault1;
      cacheServiceOrderEquipment.AllowDelete = flag3 && !valueOrDefault1;
    }
    if (cacheServiceOrderEmployees != null)
    {
      cacheServiceOrderEmployees.AllowSelect = !valueOrDefault1;
      cacheServiceOrderEmployees.AllowInsert = flag3 && !valueOrDefault1;
      cacheServiceOrderEmployees.AllowUpdate = flag3 && !valueOrDefault1;
      cacheServiceOrderEmployees.AllowDelete = flag3 && !valueOrDefault1;
    }
    nullable2 = fsServiceOrderRow.BAccountRequired;
    bool valueOrDefault2 = nullable2.GetValueOrDefault();
    nullable2 = fsSrvOrdTypeRow.RequireContact;
    bool valueOrDefault3 = nullable2.GetValueOrDefault();
    bool flag15 = fsSrvOrdTypeRow.Behavior == "IN";
    bool flag16 = PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() || PXAccess.FeatureInstalled<FeaturesSet.routeManagementModule>();
    string billingMode = this.GetBillingMode(fsServiceOrderRow);
    bool flag17 = flag16 && billingMode == "SO";
    bool flag18 = this.AllowEnableCustomerID(fsServiceOrderRow);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.customerID>(cacheServiceOrder, (object) fsServiceOrderRow, valueOrDefault2 & flag18);
    PXUIFieldAttribute.SetRequired<FSServiceOrder.contactID>(cacheServiceOrder, valueOrDefault2 & valueOrDefault3);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.locationID>(cacheServiceOrder, (object) fsServiceOrderRow, valueOrDefault2 && !flag2);
    PXCache pxCache11 = cacheServiceOrder;
    FSServiceOrder fsServiceOrder1 = fsServiceOrderRow;
    int num13;
    if (flag16)
    {
      nullable2 = fsServiceOrderRow.AllowInvoice;
      bool flag19 = false;
      num13 = nullable2.GetValueOrDefault() == flag19 & nullable2.HasValue ? 1 : 0;
    }
    else
      num13 = 0;
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.billServiceContractID>(pxCache11, (object) fsServiceOrder1, num13 != 0);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.billServiceContractID>(cacheServiceOrder, (object) fsServiceOrderRow, flag16);
    PXCache pxCache12 = cacheServiceOrder;
    FSServiceOrder fsServiceOrder2 = fsServiceOrderRow;
    int num14;
    if (flag17)
    {
      nullable1 = fsServiceOrderRow.BillServiceContractID;
      num14 = nullable1.HasValue ? 1 : 0;
    }
    else
      num14 = 0;
    PXUIFieldAttribute.SetVisible<FSServiceOrder.billContractPeriodID>(pxCache12, (object) fsServiceOrder2, num14 != 0);
    PXDefaultAttribute.SetPersistingCheck<FSServiceOrder.customerID>(cacheServiceOrder, (object) fsServiceOrderRow, valueOrDefault2 & flag18 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSServiceOrder.contactID>(cacheServiceOrder, (object) fsServiceOrderRow, valueOrDefault2 & valueOrDefault3 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSServiceOrder.locationID>(cacheServiceOrder, (object) fsServiceOrderRow, valueOrDefault2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    this.EnableDisable_SLAETA(cacheServiceOrder, fsServiceOrderRow);
    bool flag20 = true;
    nullable1 = fsServiceOrderRow.AppointmentsCompletedOrClosedCntr;
    int num15 = 0;
    if (nullable1.GetValueOrDefault() > num15 & nullable1.HasValue)
      flag20 = false;
    bool flag21 = fsAppointmentRow == null || appointmentCount <= 1;
    bool flag22 = billingMode == "SO" && fsSrvOrdTypeRow.Behavior != "IN" && fsSrvOrdTypeRow.Behavior != "QT" && !fsServiceOrderRow.IsBilledOrClosed && (fsServiceOrderRow.Status == "O" || fsServiceOrderRow.Status == "C");
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.allowInvoice>(cacheServiceOrder, (object) fsServiceOrderRow, flag22);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.customerID>(cacheServiceOrder, (object) fsServiceOrderRow, cacheServiceOrder.GetStatus((object) fsServiceOrderRow) == 2 & valueOrDefault2 && detailsCount == 0 && !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.billCustomerID>(cacheServiceOrder, (object) fsServiceOrderRow, flag20 & flag21 && !flag15 && !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.billLocationID>(cacheServiceOrder, (object) fsServiceOrderRow, flag20 & flag21 && !flag15 && !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.branchID>(cacheServiceOrder, (object) fsServiceOrderRow, flag20 & flag21 && !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.branchLocationID>(cacheServiceOrder, (object) fsServiceOrderRow, flag20 & flag21 && !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.projectID>(cacheServiceOrder, (object) fsServiceOrderRow, flag20 & flag21 && !flag2);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.dfltProjectTaskID>(cacheServiceOrder, (object) fsServiceOrderRow, !ProjectDefaultAttribute.IsNonProject(fsServiceOrderRow.ProjectID));
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.dfltProjectTaskID>(cacheServiceOrder, (object) fsServiceOrderRow, flag20 & flag21 && !ProjectDefaultAttribute.IsNonProject(fsServiceOrderRow.ProjectID) && !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.roomID>(cacheServiceOrder, (object) fsServiceOrderRow, flag20 & flag21);
    PXUIFieldAttribute.SetRequired<FSServiceOrder.contactID>(cacheServiceOrder, flag20 & valueOrDefault2 & valueOrDefault3);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.taxZoneID>(cacheServiceOrder, (object) fsServiceOrderRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.custPORefNbr>(cacheServiceOrder, (object) fsServiceOrderRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.custWorkOrderRefNbr>(cacheServiceOrder, (object) fsServiceOrderRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.salesPersonID>(cacheServiceOrder, (object) fsServiceOrderRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.commissionable>(cacheServiceOrder, (object) fsServiceOrderRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.externalTaxExemptionNumber>(cacheServiceOrder, (object) fsServiceOrderRow, !flag2);
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.entityUsageType>(cacheServiceOrder, (object) fsServiceOrderRow, !flag2);
  }

  public virtual bool AreThereAnyItemsForPO(
    ServiceOrderEntry graph,
    FSServiceOrder fsServiceOrderRow)
  {
    if (fsServiceOrderRow == null)
      return false;
    int? pendingPoLineCntr = fsServiceOrderRow.PendingPOLineCntr;
    int num = 0;
    return pendingPoLineCntr.GetValueOrDefault() > num & pendingPoLineCntr.HasValue;
  }

  public virtual void EnableDisable_Acct_Sub(
    PXCache cache,
    IFSSODetBase fsSODetRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSServiceOrder fsServiceOrderRow)
  {
    int num;
    if (fsSrvOrdTypeRow != null && fsSrvOrdTypeRow.Behavior != "IN" && fsServiceOrderRow != null)
    {
      bool? quote = fsServiceOrderRow.Quote;
      bool flag = false;
      if (quote.GetValueOrDefault() == flag & quote.HasValue)
      {
        num = fsSODetRow.LineType == "NSTKI" || fsSODetRow.LineType == "SERVI" ? 1 : (fsSODetRow.LineType == "SLPRO" ? 1 : 0);
        goto label_4;
      }
    }
    num = 0;
label_4:
    bool flag1 = num != 0;
    switch (fsSODetRow)
    {
      case FSSODet _:
        PXUIFieldAttribute.SetEnabled<FSSODet.acctID>(cache, (object) fsSODetRow, flag1);
        PXUIFieldAttribute.SetEnabled<FSSODet.subID>(cache, (object) fsSODetRow, flag1);
        break;
      case FSAppointmentDet _:
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.acctID>(cache, (object) fsSODetRow, flag1);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.subID>(cache, (object) fsSODetRow, flag1);
        break;
    }
  }

  /// <summary>
  /// Returns true if a Service order [fsServiceOrderRow] can be updated based on its status and its SrvOrdtype's status.
  /// </summary>
  public virtual bool CanUpdateServiceOrder(
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsServiceOrderRow == null || fsSrvOrdTypeRow == null)
      return false;
    bool? nullable = fsServiceOrderRow.Closed;
    if (!nullable.GetValueOrDefault())
    {
      nullable = fsServiceOrderRow.Canceled;
      if (!nullable.GetValueOrDefault())
      {
        nullable = fsSrvOrdTypeRow.Active;
        bool flag = false;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
          return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Returns true if a Service order [fsServiceOrderRow] can be deleted based based in its status.
  /// </summary>
  public virtual bool CanDeleteServiceOrder(PXGraph graph, FSServiceOrder fsServiceOrderRow)
  {
    if (fsServiceOrderRow != null && !fsServiceOrderRow.Mem_Invoiced.GetValueOrDefault() && !fsServiceOrderRow.AllowInvoice.GetValueOrDefault())
    {
      bool? openDoc = fsServiceOrderRow.OpenDoc;
      bool flag1 = false;
      if (openDoc.GetValueOrDefault() == flag1 & openDoc.HasValue)
      {
        bool? hold = fsServiceOrderRow.Hold;
        bool flag2 = false;
        if (hold.GetValueOrDefault() == flag2 & hold.HasValue)
        {
          bool? quote = fsServiceOrderRow.Quote;
          bool flag3 = false;
          if (quote.GetValueOrDefault() == flag3 & quote.HasValue)
            goto label_4;
        }
      }
      int? appointmentsCompletedCntr = fsServiceOrderRow.AppointmentsCompletedCntr;
      int num = 0;
      return !(appointmentsCompletedCntr.GetValueOrDefault() > num & appointmentsCompletedCntr.HasValue);
    }
label_4:
    return false;
  }

  /// <summary>
  /// Returns true if a Service order [fsServiceOrderRow] has an appointment assigned.
  /// </summary>
  public virtual bool ServiceOrderHasAppointment(PXGraph graph, FSServiceOrder fsServiceOrderRow)
  {
    return PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.sOID, Equal<Required<FSAppointment.sOID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsServiceOrderRow.SOID
    }).Count != 0;
  }

  /// <summary>
  /// Returns true if a Service in the Service Order <c>fsSODetServiceRow</c> is linked with an appointment.
  /// </summary>
  public virtual bool FSSODetLinkedToAppointments(PXGraph graph, FSSODet fsSODetRow)
  {
    return PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.Select(graph, new object[1]
    {
      (object) fsSODetRow.SODetID
    }).Count > 0;
  }

  public virtual void EnableDisable_SLAETA(
    PXCache cacheServiceOrder,
    FSServiceOrder fsServiceOrderRow)
  {
    PXUIFieldAttribute.SetEnabled<FSServiceOrder.sLAETA>(cacheServiceOrder, (object) fsServiceOrderRow, fsServiceOrderRow.SourceType != "CR");
  }

  public virtual int? GetDefaultLocationID(PXGraph graph, int? bAccountID)
  {
    return ServiceOrderEntry.GetDefaultLocationIDInt(graph, bAccountID);
  }

  public virtual void SetDocDesc(PXGraph graph, FSServiceOrder fsServiceOrderRow)
  {
    FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sOID, Equal<Required<FSSODet.sOID>>>, OrderBy<Asc<FSSODet.sODetID>>>.Config>.Select(graph, new object[1]
    {
      (object) fsServiceOrderRow.SOID
    }));
    if (fssoDet == null)
      return;
    fsServiceOrderRow.DocDesc = fssoDet.TranDesc;
  }

  public virtual void SetBillCustomerAndLocationID(PXCache cache, FSServiceOrder fsServiceOrderRow)
  {
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<FSServiceOrder.customerID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) fsServiceOrderRow.CustomerID
    }));
    int? newValue1 = new int?();
    int? newValue2 = new int?();
    if (fsServiceOrderRow.BillServiceContractID.HasValue)
    {
      int? serviceContractId = fsServiceOrderRow.BillServiceContractID;
      int num = 0;
      if (!(serviceContractId.GetValueOrDefault() == num & serviceContractId.HasValue))
      {
        FSServiceContract fsServiceContract = FSServiceContract.PK.Find(cache.Graph, fsServiceOrderRow.BillServiceContractID);
        if (fsServiceContract != null && fsServiceContract.ServiceContractID.HasValue)
        {
          newValue1 = fsServiceContract.BillCustomerID;
          newValue2 = fsServiceContract.BillLocationID;
        }
      }
    }
    if (!newValue1.HasValue && (baccount == null || baccount.Type != "PR"))
    {
      FSxCustomer extension = PXCache<PX.Objects.AR.Customer>.GetExtension<FSxCustomer>(SharedFunctions.GetCustomerRow(cache.Graph, fsServiceOrderRow.CustomerID));
      switch (extension.DefaultBillingCustomerSource)
      {
        case "SO":
          newValue1 = fsServiceOrderRow.CustomerID;
          newValue2 = fsServiceOrderRow.LocationID;
          break;
        case "DC":
          newValue1 = fsServiceOrderRow.CustomerID;
          newValue2 = this.GetDefaultLocationID(cache.Graph, fsServiceOrderRow.CustomerID);
          break;
        case "LC":
          newValue1 = extension.BillCustomerID;
          newValue2 = extension.BillLocationID;
          break;
      }
    }
    cache.SetValueExtIfDifferent<FSServiceOrder.billCustomerID>((object) fsServiceOrderRow, (object) newValue1);
    cache.SetValueExtIfDifferent<FSServiceOrder.billLocationID>((object) fsServiceOrderRow, (object) newValue2);
  }

  public virtual bool AllowEnableCustomerID(FSServiceOrder fsServiceOrderRow)
  {
    return fsServiceOrderRow != null && fsServiceOrderRow.SourceType == "SD";
  }

  public virtual void GetPendingLines(
    PXGraph graph,
    int? sOID,
    ref PXResultset<FSSODet> bqlResultSet_FSSODet)
  {
    bqlResultSet_FSSODet = PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sOID, Equal<Required<FSSODet.sOID>>, And<FSSODet.status, Equal<FSSODet.ListField_Status_SODet.ScheduleNeeded>>>, OrderBy<Asc<FSSODet.sortOrder>>>.Config>.Select(graph, new object[1]
    {
      (object) sOID
    });
  }

  public virtual bool CheckCustomerChange(
    PXCache cacheServiceOrder,
    FSServiceOrder fsServiceOrderRow,
    int? oldCustomerID,
    PXResultset<FSAppointment> bqlResultSet)
  {
    bool? openDoc = fsServiceOrderRow.OpenDoc;
    bool flag1 = false;
    if (openDoc.GetValueOrDefault() == flag1 & openDoc.HasValue)
    {
      bool? hold = fsServiceOrderRow.Hold;
      bool flag2 = false;
      if (hold.GetValueOrDefault() == flag2 & hold.HasValue)
      {
        bool? quote = fsServiceOrderRow.Quote;
        bool flag3 = false;
        if (quote.GetValueOrDefault() == flag3 & quote.HasValue)
        {
          fsServiceOrderRow.CustomerID = oldCustomerID;
          cacheServiceOrder.RaiseExceptionHandling<FSServiceOrder.customerID>((object) fsServiceOrderRow, (object) oldCustomerID, (Exception) new PXSetPropertyException("The Customer cannot be changed because the Service Order status is already different from Open or Hold.", (PXErrorLevel) 2));
          return false;
        }
      }
    }
    foreach (PXResult<FSAppointment> bqlResult in bqlResultSet)
    {
      FSAppointment fsAppointment = PXResult<FSAppointment>.op_Implicit(bqlResult);
      int num;
      if (fsAppointment != null)
      {
        bool? notStarted = fsAppointment.NotStarted;
        bool flag4 = false;
        num = notStarted.GetValueOrDefault() == flag4 & notStarted.HasValue ? 1 : 0;
      }
      else
        num = 0;
      if (num != 0)
      {
        fsServiceOrderRow.CustomerID = oldCustomerID;
        cacheServiceOrder.RaiseExceptionHandling<FSServiceOrder.customerID>((object) fsServiceOrderRow, (object) oldCustomerID, (Exception) new PXSetPropertyException("The Customer cannot be changed because the Service Order already has ongoing Appointments.", (PXErrorLevel) 2));
        return false;
      }
    }
    return true;
  }

  public virtual void _EnableDisableActionButtons(PXAction<FSServiceOrder>[] pxActions, bool enable)
  {
    foreach (PXAction pxAction in pxActions)
      pxAction.SetEnabled(enable);
  }

  public virtual FSServiceOrder CreateServiceOrderCleanCopy(FSServiceOrder fsServiceOrderRow)
  {
    FSServiceOrder copy = PXCache<FSServiceOrder>.CreateCopy(fsServiceOrderRow);
    copy.SrvOrdType = (string) null;
    copy.RefNbr = (string) null;
    copy.SOID = new int?();
    copy.NoteID = new Guid?();
    copy.CuryInfoID = new long?();
    copy.BranchID = new int?();
    copy.BranchLocationID = new int?();
    copy.LocationID = new int?();
    copy.ContactID = new int?();
    copy.Status = (string) null;
    copy.ProjectID = new int?();
    copy.DfltProjectTaskID = new int?();
    copy.ServiceOrderContactID = new int?();
    copy.ServiceOrderAddressID = new int?();
    copy.AllowInvoice = new bool?(false);
    copy.Quote = new bool?(false);
    copy.OpenDoc = new bool?(false);
    copy.Hold = new bool?(false);
    copy.Billed = new bool?(false);
    copy.Awaiting = new bool?(false);
    copy.Completed = new bool?(false);
    copy.Canceled = new bool?(false);
    copy.Copied = new bool?(false);
    copy.Confirmed = new bool?(false);
    copy.WorkflowTypeID = (string) null;
    copy.EstimatedDurationTotal = new int?(0);
    copy.ApptDurationTotal = new int?(0);
    copy.CuryEstimatedOrderTotal = new Decimal?(0M);
    copy.CuryApptOrderTotal = new Decimal?(0M);
    copy.CuryBillableOrderTotal = new Decimal?(0M);
    copy.EstimatedOrderTotal = new Decimal?(0M);
    copy.ApptOrderTotal = new Decimal?(0M);
    copy.BillableOrderTotal = new Decimal?(0M);
    copy.LineCntr = new int?(0);
    copy.PendingPOLineCntr = new int?(0);
    copy.SplitLineCntr = new int?(0);
    return copy;
  }

  public virtual int? Get_TranAcctID_DefaultValue(
    PXGraph graph,
    string salesAcctSource,
    int? inventoryID,
    int? siteID,
    FSServiceOrder fsServiceOrderRow)
  {
    return ServiceOrderEntry.Get_TranAcctID_DefaultValueInt(graph, salesAcctSource, inventoryID, siteID, fsServiceOrderRow);
  }

  public virtual object Get_IFSSODetBase_SubID_DefaultValue(
    PXCache cache,
    IFSSODetBase fsSODetRow,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSAppointment fsAppointmentRow = null)
  {
    int? inventoryID = fsSODetRow.IsService ? fsSODetRow.InventoryID : fsSODetRow.InventoryID;
    int? salesPersonID = fsAppointmentRow == null ? fsServiceOrderRow.SalesPersonID : fsAppointmentRow.SalesPersonID;
    SharedClasses.SubAccountIDTupla subAccountIds = SharedFunctions.GetSubAccountIDs(cache.Graph, fsSrvOrdTypeRow, inventoryID, fsServiceOrderRow.BranchID, fsServiceOrderRow.LocationID, fsServiceOrderRow.BranchLocationID, salesPersonID, fsSODetRow.SiteID);
    if (subAccountIds == null)
      return (object) null;
    object subIdDefaultValue = (object) null;
    try
    {
      subIdDefaultValue = (object) SubAccountMaskAttribute.MakeSub<FSSrvOrdType.combineSubFrom>(cache.Graph, fsSrvOrdTypeRow.CombineSubFrom, new object[8]
      {
        (object) subAccountIds.branchLocation_SubID,
        (object) subAccountIds.branch_SubID,
        (object) subAccountIds.inventoryItem_SubID,
        (object) subAccountIds.customerLocation_SubID,
        (object) subAccountIds.postingClass_SubID,
        (object) subAccountIds.salesPerson_SubID,
        (object) subAccountIds.srvOrdType_SubID,
        (object) subAccountIds.warehouse_SubID
      }, new System.Type[8]
      {
        typeof (FSBranchLocation.subID),
        typeof (PX.Objects.CR.Location.cMPSalesSubID),
        typeof (PX.Objects.IN.InventoryItem.salesSubID),
        typeof (PX.Objects.CR.Location.cSalesSubID),
        typeof (INPostClass.salesSubID),
        typeof (PX.Objects.AR.SalesPerson.salesSubID),
        typeof (FSSrvOrdType.subID),
        typeof (PX.Objects.IN.INSite.salesSubID)
      });
      if (fsSODetRow is FSSODet)
        cache.RaiseFieldUpdating<FSSODet.subID>((object) fsSODetRow, ref subIdDefaultValue);
      if (fsSODetRow is FSAppointmentDet)
        cache.RaiseFieldUpdating<FSAppointmentDet.subID>((object) fsSODetRow, ref subIdDefaultValue);
    }
    catch (PXMaskArgumentException ex)
    {
      if (fsSODetRow is FSSODet)
        cache.RaiseExceptionHandling<FSSODet.subID>((object) fsSODetRow, (object) null, (Exception) new PXSetPropertyException(((Exception) ex).Message));
      if (fsSODetRow is FSAppointmentDet)
        cache.RaiseExceptionHandling<FSAppointmentDet.subID>((object) fsSODetRow, (object) null, (Exception) new PXSetPropertyException(((Exception) ex).Message));
      subIdDefaultValue = (object) null;
    }
    catch (PXSetPropertyException ex)
    {
      if (fsSODetRow is FSSODet)
        cache.RaiseExceptionHandling<FSSODet.subID>((object) fsSODetRow, subIdDefaultValue, (Exception) ex);
      if (fsSODetRow is FSAppointmentDet)
        cache.RaiseExceptionHandling<FSAppointmentDet.subID>((object) fsSODetRow, subIdDefaultValue, (Exception) ex);
      subIdDefaultValue = (object) null;
    }
    return subIdDefaultValue;
  }

  public virtual void UpdateServiceCounts(
    FSServiceOrder fsServiceOrderRow,
    IEnumerable<FSSODet> serviceDetails)
  {
    fsServiceOrderRow.ServiceCount = new int?(0);
    fsServiceOrderRow.CompleteServiceCount = new int?(0);
    fsServiceOrderRow.ScheduledServiceCount = new int?(0);
    fsServiceOrderRow.ServiceCount = new int?(serviceDetails.Where<FSSODet>((Func<FSSODet, bool>) (_ => _.Status != "CC")).Count<FSSODet>());
    fsServiceOrderRow.CompleteServiceCount = new int?(serviceDetails.Where<FSSODet>((Func<FSSODet, bool>) (_ => _.Status == "CP")).Count<FSSODet>());
    fsServiceOrderRow.ScheduledServiceCount = new int?(serviceDetails.Where<FSSODet>((Func<FSSODet, bool>) (_ => _.Status == "SC")).Count<FSSODet>());
  }

  public virtual void PropagateSODetStatusToAppointmentLines(
    PXGraph graph,
    FSSODet fsSODetServiceRow,
    FSAppointment fsAppointmentRow)
  {
    int? appointmentId = (int?) fsAppointmentRow?.AppointmentID;
    PXUpdateJoin<Set<FSAppointmentDet.status, Required<FSAppointmentDet.status>>, FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>>, Where<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And2<Where<FSAppointmentDet.status, NotEqual<Required<FSAppointmentDet.status>>, And<Where<FSAppointmentDet.appointmentID, NotEqual<Required<FSAppointmentDet.appointmentID>>, Or<Required<FSAppointmentDet.appointmentID>, IsNull>>>>, And<Where<FSAppointment.notStarted, Equal<True>, Or<FSAppointment.inProcess, Equal<True>>>>>>>.Update(graph, new object[5]
    {
      (object) fsSODetServiceRow.Status,
      (object) fsSODetServiceRow.SODetID,
      (object) fsSODetServiceRow.Status,
      (object) appointmentId,
      (object) appointmentId
    });
  }

  public virtual void UpdatePendingPostFlags(
    ServiceOrderEntry graphServiceOrderEntry,
    FSServiceOrder fsServiceOrderRow,
    PXSelectBase<FSSODet> serviceDetails)
  {
    int? nullable1 = new int?(0);
    if (fsServiceOrderRow.PostedBy == null)
    {
      foreach (PXResult<FSSODet> pxResult in serviceDetails.Select(Array.Empty<object>()))
      {
        FSSODet det = PXResult<FSSODet>.op_Implicit(pxResult);
        if (det.needToBePosted())
        {
          bool? isBillable = det.IsBillable;
          bool flag = false;
          if (!(isBillable.GetValueOrDefault() == flag & isBillable.HasValue))
          {
            FSPostInfo fsPostInfo = GraphHelper.RowCast<FSPostInfo>((IEnumerable) ((PXSelectBase<FSPostInfo>) graphServiceOrderEntry.PostInfoDetails).Select(Array.Empty<object>())).Where<FSPostInfo>((Func<FSPostInfo, bool>) (x =>
            {
              int? postId1 = x.PostID;
              int? postId2 = det.PostID;
              return postId1.GetValueOrDefault() == postId2.GetValueOrDefault() & postId1.HasValue == postId2.HasValue;
            })).FirstOrDefault<FSPostInfo>();
            if (fsPostInfo == null || !fsPostInfo.isPosted())
            {
              int? nullable2 = nullable1;
              nullable1 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
              break;
            }
          }
        }
      }
      FSServiceOrder fsServiceOrder = fsServiceOrderRow;
      int? nullable3 = nullable1;
      int num = 0;
      bool? nullable4 = new bool?(nullable3.GetValueOrDefault() > num & nullable3.HasValue);
      fsServiceOrder.PendingAPARSOPost = nullable4;
    }
    else
      fsServiceOrderRow.PendingAPARSOPost = new bool?(false);
    fsServiceOrderRow.PendingINPost = new bool?(false);
  }

  public virtual void UpdateWarrantyFlag(PXCache cache, IFSSODetBase fsSODetRow, DateTime? docDate)
  {
    fsSODetRow.Warranty = new bool?(false);
    if (!docDate.HasValue)
      return;
    int? nullable1 = fsSODetRow.SMEquipmentID;
    if (!nullable1.HasValue || fsSODetRow.EquipmentAction != "RT" && fsSODetRow.EquipmentAction != "RC" && fsSODetRow.EquipmentAction != "NO" && fsSODetRow.LineType != "SERVI")
      return;
    if (fsSODetRow.EquipmentAction == "RC")
    {
      nullable1 = fsSODetRow.EquipmentLineRef;
      if (!nullable1.HasValue)
        return;
    }
    FSEquipment equipmentRow = PXResultset<FSEquipment>.op_Implicit(PXSelectBase<FSEquipment, PXSelect<FSEquipment, Where<FSEquipment.SMequipmentID, Equal<Required<FSEquipment.SMequipmentID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) fsSODetRow.SMEquipmentID
    }));
    if (fsSODetRow.LineType != "SERVI" && fsSODetRow.LineType != "NSTKI" && fsSODetRow.LineType != "CM_LN" && fsSODetRow.LineType != "IT_LN" && fsSODetRow.EquipmentAction != "NO")
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(cache.Graph, fsSODetRow.InventoryID);
      FSxEquipmentModel fsxEquipmentModel = (FSxEquipmentModel) null;
      if (inventoryItem != null)
        fsxEquipmentModel = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(inventoryItem);
      if (inventoryItem == null || fsxEquipmentModel == null || fsxEquipmentModel != null && (fsxEquipmentModel.EquipmentItemClass == "ME" || fsxEquipmentModel.EquipmentItemClass == "CT"))
      {
        this.UpdateWarrantyFlagByTargetEquipment(cache, fsSODetRow, docDate, equipmentRow);
      }
      else
      {
        if (!(fsxEquipmentModel.EquipmentItemClass == "OI"))
          return;
        DateTime? cpnyWarrantyEndDate = equipmentRow.CpnyWarrantyEndDate;
        DateTime? nullable2 = docDate;
        if ((cpnyWarrantyEndDate.HasValue & nullable2.HasValue ? (cpnyWarrantyEndDate.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        {
          DateTime? vendorWarrantyEndDate = equipmentRow.VendorWarrantyEndDate;
          DateTime? nullable3 = docDate;
          if ((vendorWarrantyEndDate.HasValue & nullable3.HasValue ? (vendorWarrantyEndDate.GetValueOrDefault() >= nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            return;
        }
        fsSODetRow.Warranty = new bool?(true);
      }
    }
    else
      this.UpdateWarrantyFlagByTargetEquipment(cache, fsSODetRow, docDate, equipmentRow);
  }

  public virtual void UpdateWarrantyFlagByTargetEquipment(
    PXCache cache,
    IFSSODetBase fsSODetRow,
    DateTime? docDate,
    FSEquipment equipmentRow)
  {
    if (equipmentRow == null)
      return;
    if (!fsSODetRow.EquipmentLineRef.HasValue)
    {
      DateTime? nullable1 = equipmentRow.CpnyWarrantyEndDate;
      DateTime? nullable2 = docDate;
      if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      {
        nullable2 = equipmentRow.VendorWarrantyEndDate;
        nullable1 = docDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
      fsSODetRow.Warranty = new bool?(true);
    }
    else
    {
      FSEquipmentComponent equipmentComponent = PXResultset<FSEquipmentComponent>.op_Implicit(PXSelectBase<FSEquipmentComponent, PXSelect<FSEquipmentComponent, Where<FSEquipmentComponent.SMequipmentID, Equal<Required<FSEquipmentComponent.SMequipmentID>>, And<FSEquipmentComponent.lineNbr, Equal<Required<FSEquipmentComponent.lineNbr>>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) fsSODetRow.SMEquipmentID,
        (object) fsSODetRow.EquipmentLineRef
      }));
      DateTime? nullable3 = equipmentComponent.CpnyWarrantyEndDate;
      DateTime? nullable4;
      if (nullable3.HasValue)
      {
        nullable3 = equipmentComponent.CpnyWarrantyEndDate;
        nullable4 = docDate;
        if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          fsSODetRow.Warranty = new bool?(true);
          return;
        }
      }
      nullable4 = equipmentComponent.VendorWarrantyEndDate;
      if (nullable4.HasValue)
      {
        nullable4 = equipmentComponent.VendorWarrantyEndDate;
        nullable3 = docDate;
        if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() >= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          fsSODetRow.Warranty = new bool?(true);
          return;
        }
      }
      nullable3 = equipmentComponent.CpnyWarrantyEndDate;
      if (nullable3.HasValue)
        return;
      nullable3 = equipmentComponent.VendorWarrantyEndDate;
      if (nullable3.HasValue)
        return;
      nullable3 = equipmentRow.CpnyWarrantyEndDate;
      nullable4 = docDate;
      if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      {
        nullable4 = equipmentRow.VendorWarrantyEndDate;
        nullable3 = docDate;
        if ((nullable4.HasValue & nullable3.HasValue ? (nullable4.GetValueOrDefault() >= nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
      }
      fsSODetRow.Warranty = new bool?(true);
    }
  }

  public virtual bool AccountIsAProspect(PXGraph graph, int? bAccountID)
  {
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bAccountID
    }));
    return baccount != null && baccount.Type == "PR";
  }

  public virtual bool CustomerHasBillingCycle(
    PXGraph graph,
    FSSetup setupRecordRow,
    int? customerID,
    string srvOrdType)
  {
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) customerID
    }));
    if (customer == null)
      return false;
    FSxCustomer extension = PXCache<PX.Objects.AR.Customer>.GetExtension<FSxCustomer>(customer);
    if (setupRecordRow == null || !setupRecordRow.CustomerMultipleBillingOptions.GetValueOrDefault())
      return extension.BillingCycleID.HasValue;
    return ((IQueryable<PXResult<FSCustomerBillingSetup>>) PXSelectBase<FSCustomerBillingSetup, PXSelect<FSCustomerBillingSetup, Where<FSCustomerBillingSetup.customerID, Equal<Required<FSCustomerBillingSetup.customerID>>, And<FSCustomerBillingSetup.srvOrdType, Equal<Required<FSCustomerBillingSetup.srvOrdType>>>>>.Config>.Select(graph, new object[2]
    {
      (object) customerID,
      (object) srvOrdType
    })).Count<PXResult<FSCustomerBillingSetup>>() > 0;
  }

  public virtual bool ValidateCustomerBillingCycle<TField>(
    PXCache cache,
    object row,
    int? billCustomerID,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSSetup setupRecordRow,
    bool justWarn)
    where TField : IBqlField
  {
    if (row == null || fsSrvOrdTypeRow == null || fsSrvOrdTypeRow.Behavior == "QT" || fsSrvOrdTypeRow.Behavior == "IN" || row is FSServiceOrder && (row as FSServiceOrder).Quote.GetValueOrDefault() || this.AccountIsAProspect(cache.Graph, billCustomerID) || this.CustomerHasBillingCycle(cache.Graph, setupRecordRow, billCustomerID, fsSrvOrdTypeRow.SrvOrdType))
      return true;
    PXException pxException = (PXException) new PXSetPropertyException("The customer has no billing cycle specified for the current service order type. Assign the billing cycle before saving the service order.", (PXErrorLevel) 2);
    cache.RaiseExceptionHandling<TField>(row, (object) billCustomerID, (Exception) pxException);
    if (!justWarn)
      throw pxException;
    return false;
  }

  public virtual void CreatePrepaymentDocument(
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    out PXGraph target,
    string paymentType = "PMT")
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    target = (PXGraph) instance;
    ((PXGraph) instance).Clear();
    PX.Objects.AR.ARPayment arPayment1 = new PX.Objects.AR.ARPayment();
    arPayment1.DocType = paymentType;
    PX.Objects.AR.ARPayment arPayment2 = arPayment1;
    OpenPeriodAttribute.SetThrowErrorExternal<PX.Objects.AR.ARPayment.adjFinPeriodID>(((PXSelectBase) instance.Document).Cache, true);
    PX.Objects.AR.ARPayment copy = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Insert(arPayment2));
    OpenPeriodAttribute.SetThrowErrorExternal<PX.Objects.AR.ARPayment.adjFinPeriodID>(((PXSelectBase) instance.Document).Cache, false);
    copy.CustomerID = fsServiceOrderRow.BillCustomerID;
    copy.CustomerLocationID = fsServiceOrderRow.BillLocationID;
    if (string.Equals(fsServiceOrderRow.CuryID, copy.CuryID))
      copy.CuryOrigDocAmt = new Decimal?(fsServiceOrderRow.SOCuryUnpaidBalanace.GetValueOrDefault());
    else
      copy.CuryOrigDocAmt = new Decimal?(((PXGraph) instance).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().CuryConvCury(fsServiceOrderRow.SOUnpaidBalanace.Value));
    copy.ExtRefNbr = fsServiceOrderRow.CustWorkOrderRefNbr;
    copy.DocDesc = fsServiceOrderRow.DocDesc;
    PX.Objects.AR.ARPayment arPaymentRow = ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(copy);
    this.InsertSOAdjustments(fsServiceOrderRow, fsAppointmentRow, instance, arPaymentRow);
  }

  public virtual void InsertSOAdjustments(
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fSAppointmentRow,
    ARPaymentEntry arPaymentGraph,
    PX.Objects.AR.ARPayment arPaymentRow)
  {
    FSAdjust fsAdjust = new FSAdjust()
    {
      AdjdOrderType = fsServiceOrderRow.SrvOrdType,
      AdjdOrderNbr = fsServiceOrderRow.RefNbr,
      AdjdAppRefNbr = fSAppointmentRow?.RefNbr,
      SOCuryCompletedBillableTotal = fsServiceOrderRow.CuryEffectiveBillableDocTotal
    };
    SM_ARPaymentEntry extension = ((PXGraph) arPaymentGraph).GetExtension<SM_ARPaymentEntry>();
    try
    {
      ((PXSelectBase<FSAdjust>) extension.FSAdjustments).Insert(fsAdjust);
    }
    catch (PXSetPropertyException ex)
    {
      arPaymentRow.CuryOrigDocAmt = new Decimal?(0M);
    }
  }

  public virtual void RecalcSOApplAmounts(PXGraph graph, PX.Objects.AR.ARPayment row)
  {
    ServiceOrderEntry.RecalcSOApplAmountsInt(graph, row);
  }

  public virtual void HidePrepayments(
    PXView fsAdjustmentsView,
    PXCache cache,
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsServiceOrderRow == null || fsSrvOrdTypeRow == null)
      return;
    bool flag = fsSrvOrdTypeRow.PostToSOSIPM.GetValueOrDefault();
    int? serviceContractId;
    if (fsServiceOrderRow != null)
    {
      int num;
      if (flag)
      {
        serviceContractId = fsServiceOrderRow.BillServiceContractID;
        num = !serviceContractId.HasValue ? 1 : 0;
      }
      else
        num = 0;
      flag = num != 0;
      fsServiceOrderRow.IsPrepaymentEnable = new bool?(flag);
    }
    if (fsAppointmentRow != null)
    {
      int num;
      if (flag)
      {
        serviceContractId = fsAppointmentRow.BillServiceContractID;
        num = !serviceContractId.HasValue ? 1 : 0;
      }
      else
        num = 0;
      flag = num != 0;
      fsAppointmentRow.IsPrepaymentEnable = new bool?(flag);
    }
    fsAdjustmentsView.AllowSelect = flag;
    PXUIFieldAttribute.SetVisible<FSServiceOrder.sOPrepaymentApplied>(cache, (object) fsServiceOrderRow, flag);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.sOPrepaymentReceived>(cache, (object) fsServiceOrderRow, flag);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.sOPrepaymentRemaining>(cache, (object) fsServiceOrderRow, flag);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.sOCuryUnpaidBalanace>(cache, (object) fsServiceOrderRow, flag);
    PXUIFieldAttribute.SetVisible<FSServiceOrder.sOCuryBillableUnpaidBalanace>(cache, (object) fsServiceOrderRow, flag);
  }

  public virtual void UpdateServiceOrderUnboundFields(
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    bool disableServiceOrderUnboundFieldCalc)
  {
    string billingMode = this.GetBillingMode(fsServiceOrderRow);
    ServiceOrderEntry.UpdateServiceOrderUnboundFields((PXGraph) this, fsServiceOrderRow, fsAppointmentRow, billingMode, disableServiceOrderUnboundFieldCalc, true);
  }

  public virtual string GetBillingMode(FSServiceOrder fsServiceOrderRow)
  {
    return ServiceOrderEntry.GetBillingMode((PXGraph) this, ((PXSelectBase<FSBillingCycle>) this.BillingCycleRelated).Current, !(this is ServiceOrderEntry) ? ((PXSelectBase<FSSrvOrdType>) (this as AppointmentEntry).ServiceOrderTypeSelected).Current : ((PXSelectBase<FSSrvOrdType>) (this as ServiceOrderEntry).ServiceOrderTypeSelected).Current, fsServiceOrderRow);
  }

  public virtual void SetCostCodeDefault(
    IFSSODetBase fsSODetRow,
    int? projectID,
    FSSrvOrdType fsSrvOrdTypeRow,
    PXFieldDefaultingEventArgs e)
  {
    if (fsSrvOrdTypeRow == null || ProjectDefaultAttribute.IsNonProject(projectID) || !PXAccess.FeatureInstalled<FeaturesSet.costCodes>() || !fsSODetRow.InventoryID.HasValue)
      return;
    bool? nullable = fsSODetRow.IsPrepaid;
    bool flag1 = false;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      return;
    nullable = fsSODetRow.ContractRelated;
    bool flag2 = false;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      return;
    e.NewValue = (object) fsSrvOrdTypeRow.DfltCostCodeID;
  }

  public virtual void SetVisiblePODetFields(PXCache cache, bool showPOFields)
  {
    PXUIFieldAttribute.SetVisible<FSSODet.poNbr>(cache, (object) null, showPOFields);
    PXUIFieldAttribute.SetVisible<FSSODet.pOCreate>(cache, (object) null, showPOFields);
    PXUIFieldAttribute.SetVisible<FSSODet.poStatus>(cache, (object) null, showPOFields);
    PXUIFieldAttribute.SetVisible<FSSODet.poVendorID>(cache, (object) null, showPOFields);
    PXUIFieldAttribute.SetVisible<FSSODet.poVendorLocationID>(cache, (object) null, showPOFields);
    PXUIFieldAttribute.SetVisible<FSSODet.enablePO>(cache, (object) null, showPOFields);
    PXUIFieldAttribute.SetVisible<FSSODet.curyUnitCost>(cache, (object) null, showPOFields);
  }

  public virtual int? GetValidatedSiteID(PXGraph graph, int? siteID)
  {
    if (siteID.HasValue)
    {
      PX.Objects.IN.INSite inSite = PXResultset<PX.Objects.IN.INSite>.op_Implicit(PXSelectBase<PX.Objects.IN.INSite, PXSelect<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.siteID, Equal<Required<PX.Objects.IN.INSite.siteID>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>.Config>.Select(graph, new object[1]
      {
        (object) siteID
      }));
      if (inSite != null)
        return inSite.SiteID;
    }
    return new int?();
  }

  public virtual KeyValuePair<string, string>[] GetAppointmentUrlArguments(
    FSAppointment fsAppointmentRow)
  {
    return new KeyValuePair<string, string>[3]
    {
      new KeyValuePair<string, string>(typeof (FSAppointment.refNbr).Name, fsAppointmentRow.RefNbr),
      new KeyValuePair<string, string>("Date", fsAppointmentRow.ScheduledDateBegin.ToString()),
      new KeyValuePair<string, string>("AppSource", "1")
    };
  }

  public virtual void EnableDisable_ServiceActualDateTimes(
    PXCache appointmentDetCache,
    FSAppointment fsAppointmentRow,
    FSAppointmentDet fsAppointmentDetRow,
    bool enableByLineType)
  {
    if (fsAppointmentRow == null || fsAppointmentDetRow == null)
      return;
    int num;
    if (fsAppointmentRow != null)
    {
      bool? notStarted = fsAppointmentRow.NotStarted;
      bool flag = false;
      num = notStarted.GetValueOrDefault() == flag & notStarted.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag1 = num != 0;
    bool flag2 = enableByLineType & flag1;
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.actualDuration>(appointmentDetCache, (object) fsAppointmentDetRow, flag2);
  }

  public virtual void EnableDisable_TimeRelatedFields(
    PXCache appointmentEmployeeCache,
    FSSetup fsSetupRow,
    FSSrvOrdType fsSrvOrdType,
    FSAppointment fsAppointmentRow,
    FSAppointmentEmployee fsAppointmentEmployeeRow)
  {
    if (fsAppointmentRow == null || fsAppointmentEmployeeRow == null || fsSetupRow == null || fsSrvOrdType == null)
      return;
    bool? nullable;
    if (fsAppointmentRow != null)
    {
      nullable = fsAppointmentRow.NotStarted;
      bool flag = false;
      0 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    nullable = fsSetupRow.EnableEmpTimeCardIntegration;
    int num;
    if (nullable.Value)
    {
      nullable = fsSrvOrdType.CreateTimeActivitiesFromAppointment;
      if (nullable.Value && fsAppointmentEmployeeRow.Type == "EP")
      {
        num = fsAppointmentEmployeeRow.EmployeeID.HasValue ? 1 : 0;
        goto label_8;
      }
    }
    num = 0;
label_8:
    bool flag1 = num != 0;
    PXUIFieldAttribute.SetEnabled<FSAppointmentEmployee.trackTime>(appointmentEmployeeCache, (object) fsAppointmentEmployeeRow, flag1);
    PXUIFieldAttribute.SetEnabled<FSAppointmentEmployee.earningType>(appointmentEmployeeCache, (object) fsAppointmentEmployeeRow, flag1);
  }

  public virtual void SetVisible_TimeRelatedFields(
    PXCache appointmentEmployeeCache,
    FSSrvOrdType fsSrvOrdType)
  {
    if (fsSrvOrdType == null)
      return;
    PXUIFieldAttribute.SetVisible<FSAppointmentEmployee.trackTime>(appointmentEmployeeCache, (object) null, fsSrvOrdType.CreateTimeActivitiesFromAppointment.Value);
    PXUIFieldAttribute.SetVisible<FSAppointmentEmployee.earningType>(appointmentEmployeeCache, (object) null, fsSrvOrdType.CreateTimeActivitiesFromAppointment.Value);
  }

  public virtual void SetPersisting_TimeRelatedFields(
    PXCache appointmentEmployeeCache,
    FSSetup fsSetupRow,
    FSSrvOrdType fsSrvOrdType,
    FSAppointment fsAppointmentRow,
    FSAppointmentEmployee fsAppointmentEmployeeRow)
  {
    if (fsSetupRow == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSetup))
      });
    if (fsSrvOrdType == null || fsAppointmentRow == null || fsAppointmentEmployeeRow == null)
      return;
    bool? nullable = fsSetupRow.EnableEmpTimeCardIntegration;
    int num;
    if (nullable.Value)
    {
      nullable = fsSrvOrdType.CreateTimeActivitiesFromAppointment;
      if (nullable.Value)
      {
        num = fsAppointmentEmployeeRow.Type == "EP" ? 1 : 0;
        goto label_7;
      }
    }
    num = 0;
label_7:
    PXPersistingCheck pxPersistingCheck = num != 0 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXDefaultAttribute.SetPersistingCheck<FSAppointmentEmployee.trackTime>(appointmentEmployeeCache, (object) fsAppointmentEmployeeRow, pxPersistingCheck);
    PXDefaultAttribute.SetPersistingCheck<FSAppointmentEmployee.earningType>(appointmentEmployeeCache, (object) fsAppointmentEmployeeRow, pxPersistingCheck);
  }

  public virtual void EnableDisable_StaffRelatedFields(
    PXCache appointmentEmployeeCache,
    FSAppointmentEmployee fsAppointmentEmployeeRow)
  {
    if (fsAppointmentEmployeeRow == null)
      return;
    bool hasValue = fsAppointmentEmployeeRow.EmployeeID.HasValue;
    PXUIFieldAttribute.SetEnabled<FSAppointmentEmployee.employeeID>(appointmentEmployeeCache, (object) fsAppointmentEmployeeRow, !hasValue);
  }

  public virtual void EnableDisable_TimeRelatedLogFields(
    PXCache cache,
    FSAppointmentLog fsLogRow,
    FSSetup fsSetupRow,
    FSSrvOrdType fsSrvOrdType,
    FSAppointment fsAppointmentRow)
  {
    if (fsSetupRow == null)
      return;
    bool flag1 = !fsLogRow.BAccountID.HasValue || fsLogRow.BAccountType != "EP";
    bool flag2 = fsSetupRow.EnableEmpTimeCardIntegration.GetValueOrDefault() && fsSrvOrdType.CreateTimeActivitiesFromAppointment.GetValueOrDefault() && !flag1;
    bool flag3 = fsLogRow.ItemType == "TR";
    int? nullable1 = fsLogRow.TimeDuration;
    int num1 = 0;
    bool flag4 = nullable1.GetValueOrDefault() < num1 & nullable1.HasValue;
    bool flag5 = string.IsNullOrEmpty(fsLogRow.TimeCardCD) || fsLogRow.TimeActivityStatus == "OP" || fsLogRow.TimeActivityStatus == "CD";
    if (cache.GetStatus((object) fsLogRow) != 2)
      PXUIFieldAttribute.SetEnabled<FSAppointmentLog.bAccountID>(cache, (object) fsLogRow, !flag1);
    int num2;
    if (fsAppointmentRow != null)
    {
      bool? nullable2 = fsAppointmentRow.NotStarted;
      bool flag6 = false;
      if (nullable2.GetValueOrDefault() == flag6 & nullable2.HasValue)
      {
        nullable2 = fsAppointmentRow.Hold;
        bool flag7 = false;
        num2 = nullable2.GetValueOrDefault() == flag7 & nullable2.HasValue ? 1 : 0;
      }
      else
        num2 = 0;
    }
    else
      num2 = 0;
    bool flag8 = num2 != 0;
    int num3;
    if (fsAppointmentRow != null)
    {
      nullable1 = fsAppointmentRow.ProjectID;
      if (nullable1.HasValue)
      {
        int? projectID;
        if (fsAppointmentRow == null)
        {
          nullable1 = new int?();
          projectID = nullable1;
        }
        else
          projectID = fsAppointmentRow.ProjectID;
        num3 = !ProjectDefaultAttribute.IsNonProject(projectID) ? 1 : 0;
        goto label_15;
      }
    }
    num3 = 0;
label_15:
    bool flag9 = num3 != 0;
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.travel>(cache, (object) fsLogRow, flag8 && fsLogRow.ItemType != "NS");
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.laborItemID>(cache, (object) fsLogRow, !flag1);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.projectTaskID>(cache, (object) fsLogRow, !flag1 & flag9);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.costCodeID>(cache, (object) fsLogRow, !flag1);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.trackTime>(cache, (object) fsLogRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.earningType>(cache, (object) fsLogRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.descr>(cache, (object) fsLogRow, flag3);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.trackOnService>(cache, (object) fsLogRow, fsLogRow.ItemType != "NS");
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.dateTimeEnd>(cache, (object) fsLogRow, !flag4 & flag5);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.dateTimeBegin>(cache, (object) fsLogRow, flag5);
    PXUIFieldAttribute.SetEnabled<FSAppointmentLog.status>(cache, (object) fsLogRow, !flag4);
  }

  public virtual void UpdateStaffRelatedUnboundFields(
    FSAppointmentDet fsAppointmentDetServiceRow,
    AppointmentEntry.AppointmentServiceEmployees_View appointmentEmployees,
    PXSelectBase<FSAppointmentLog> logView,
    int? numEmployeeLinkedToService)
  {
    this.UpdateStaffRelatedUnboundFields(fsAppointmentDetServiceRow, (AppointmentEntry.AppointmentServiceEmployees_View) null, logView, new int?());
  }

  public virtual void InsertUpdateDelete_AppointmentDetService_StaffID(
    PXCache cache,
    FSAppointmentDet fsAppointmentDetRow,
    AppointmentEntry.AppointmentServiceEmployees_View appointmentEmployees,
    int? oldStaffID)
  {
    if (fsAppointmentDetRow.StaffID.HasValue && oldStaffID.HasValue)
    {
      FSAppointmentEmployee appointmentEmployee = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) appointmentEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ =>
      {
        if (!(_.ServiceLineRef == fsAppointmentDetRow.LineRef))
          return false;
        int? employeeId = _.EmployeeID;
        int? nullable = oldStaffID;
        return employeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & employeeId.HasValue == nullable.HasValue;
      })).FirstOrDefault<FSAppointmentEmployee>();
      if (appointmentEmployee == null)
        return;
      appointmentEmployee.EmployeeID = fsAppointmentDetRow.StaffID;
      ((PXSelectBase<FSAppointmentEmployee>) appointmentEmployees).Update(appointmentEmployee);
    }
    else if (fsAppointmentDetRow.StaffID.HasValue && !oldStaffID.HasValue)
    {
      FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee()
      {
        ServiceLineRef = fsAppointmentDetRow.LineRef,
        EmployeeID = fsAppointmentDetRow.StaffID
      };
      ((PXSelectBase<FSAppointmentEmployee>) appointmentEmployees).Insert(appointmentEmployee);
    }
    else
    {
      FSAppointmentEmployee appointmentEmployee = GraphHelper.RowCast<FSAppointmentEmployee>((IEnumerable) ((PXSelectBase<FSAppointmentEmployee>) appointmentEmployees).Select(Array.Empty<object>())).Where<FSAppointmentEmployee>((Func<FSAppointmentEmployee, bool>) (_ =>
      {
        if (!(_.ServiceLineRef == fsAppointmentDetRow.LineRef))
          return false;
        int? employeeId = _.EmployeeID;
        int? nullable = oldStaffID;
        return employeeId.GetValueOrDefault() == nullable.GetValueOrDefault() & employeeId.HasValue == nullable.HasValue;
      })).FirstOrDefault<FSAppointmentEmployee>();
      ((PXSelectBase<FSAppointmentEmployee>) appointmentEmployees).Delete(appointmentEmployee);
    }
  }

  public void EnableDisable_LineType(
    PXCache cache,
    FSAppointmentDet fsAppointmentDetRow,
    FSSetup fsSetupRow,
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSServiceContract fsServiceContractRow)
  {
    if (fsAppointmentRow == null)
      return;
    bool? nullable = fsSrvOrdTypeRow.RequireTimeApprovalToInvoice;
    bool flag1 = false;
    bool enableByLineType = nullable.GetValueOrDefault() == flag1 & nullable.HasValue;
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>() || PXAccess.FeatureInstalled<FeaturesSet.routeManagementModule>();
    bool flag3 = ((!fsAppointmentRow.BillServiceContractID.HasValue ? 0 : (fsServiceContractRow?.BillingType == "STDB" ? 1 : 0)) & (flag2 ? 1 : 0)) != 0;
    switch (fsAppointmentDetRow.LineType)
    {
      case "SERVI":
      case "NSTKI":
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.inventoryID>(cache, (object) fsAppointmentDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSAppointmentDet.inventoryID>(cache, (object) fsAppointmentDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.isFree>(cache, (object) fsAppointmentDetRow, true);
        PXCache pxCache1 = cache;
        FSAppointmentDet fsAppointmentDet1 = fsAppointmentDetRow;
        int num1;
        if (fsAppointmentRow != null)
        {
          nullable = fsAppointmentRow.NotStarted;
          bool flag4 = false;
          num1 = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.actualQty>(pxCache1, (object) fsAppointmentDet1, num1 != 0);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.estimatedQty>(cache, (object) fsAppointmentDetRow, true);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.projectTaskID>(cache, (object) fsAppointmentDetRow, true);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.estimatedDuration>(cache, (object) fsAppointmentDetRow, true);
        PXUIFieldAttribute.SetVisible<FSSODet.contractRelated>(cache, (object) null, flag3);
        PXUIFieldAttribute.SetVisible<FSSODet.coveredQty>(cache, (object) null, flag3);
        PXUIFieldAttribute.SetVisible<FSSODet.extraUsageQty>(cache, (object) null, flag3);
        PXUIFieldAttribute.SetVisible<FSSODet.curyExtraUsageUnitPrice>(cache, (object) null, flag3);
        PXUIFieldAttribute.SetVisibility<FSSODet.contractRelated>(cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
        PXUIFieldAttribute.SetVisibility<FSSODet.coveredQty>(cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
        PXUIFieldAttribute.SetVisibility<FSSODet.extraUsageQty>(cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
        PXUIFieldAttribute.SetVisibility<FSSODet.curyExtraUsageUnitPrice>(cache, (object) null, flag3 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
        break;
      case "CM_LN":
      case "IT_LN":
        PXDefaultAttribute.SetPersistingCheck<FSAppointmentDet.tranDesc>(cache, (object) fsAppointmentDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.inventoryID>(cache, (object) fsAppointmentDetRow, false);
        PXDefaultAttribute.SetPersistingCheck<FSAppointmentDet.inventoryID>(cache, (object) fsAppointmentDetRow, (PXPersistingCheck) 2);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.isFree>(cache, (object) fsAppointmentDetRow, false);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.actualQty>(cache, (object) fsAppointmentDetRow, false);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.estimatedQty>(cache, (object) fsAppointmentDetRow, false);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.projectTaskID>(cache, (object) fsAppointmentDetRow, false);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.staffID>(cache, (object) fsAppointmentDetRow, false);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.billingRule>(cache, (object) fsAppointmentDetRow, false);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.estimatedDuration>(cache, (object) fsAppointmentDetRow, false);
        enableByLineType = false;
        break;
      default:
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.inventoryID>(cache, (object) fsAppointmentDetRow, true);
        PXDefaultAttribute.SetPersistingCheck<FSAppointmentDet.inventoryID>(cache, (object) fsAppointmentDetRow, (PXPersistingCheck) 1);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.isFree>(cache, (object) fsAppointmentDetRow, true);
        PXCache pxCache2 = cache;
        FSAppointmentDet fsAppointmentDet2 = fsAppointmentDetRow;
        int num2;
        if (fsAppointmentRow != null)
        {
          nullable = fsAppointmentRow.NotStarted;
          bool flag5 = false;
          num2 = nullable.GetValueOrDefault() == flag5 & nullable.HasValue ? 1 : 0;
        }
        else
          num2 = 0;
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.actualQty>(pxCache2, (object) fsAppointmentDet2, num2 != 0);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.estimatedQty>(cache, (object) fsAppointmentDetRow, true);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.projectTaskID>(cache, (object) fsAppointmentDetRow, true);
        PXUIFieldAttribute.SetEnabled<FSAppointmentDet.staffID>(cache, (object) fsAppointmentDetRow, false);
        break;
    }
    this.EnableDisable_ServiceActualDateTimes(cache, fsAppointmentRow, fsAppointmentDetRow, enableByLineType);
  }

  /// <summary>
  /// Returns true if an Appointment [fsAppointmentRow] can be updated based in its status and the status of the Service Order [fsServiceOrderRow].
  /// </summary>
  public virtual bool CanUpdateAppointment(
    FSAppointment fsAppointmentRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsAppointmentRow == null || fsSrvOrdTypeRow == null)
      return false;
    bool? nullable = fsAppointmentRow.Closed;
    if (!nullable.GetValueOrDefault())
    {
      nullable = fsAppointmentRow.Canceled;
      if (!nullable.GetValueOrDefault())
      {
        nullable = fsSrvOrdTypeRow.Active;
        bool flag = false;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        {
          nullable = fsAppointmentRow.Billed;
          if (!nullable.GetValueOrDefault())
            return true;
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Returns true if an Appointment [fsAppointmentRow] can be deleted based in its status and the status of the Service Order [fsServiceOrderRow].
  /// </summary>
  public virtual bool CanDeleteAppointment(
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrderRow,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    bool flag1 = true;
    if (fsServiceOrderRow != null)
      flag1 = this.CanUpdateServiceOrder(fsServiceOrderRow, fsSrvOrdTypeRow);
    if (fsAppointmentRow != null)
    {
      bool? notStarted = fsAppointmentRow.NotStarted;
      bool flag2 = false;
      if (notStarted.GetValueOrDefault() == flag2 & notStarted.HasValue)
      {
        bool? inProcess = fsAppointmentRow.InProcess;
        bool flag3 = false;
        if (inProcess.GetValueOrDefault() == flag3 & inProcess.HasValue)
        {
          bool? hold = fsAppointmentRow.Hold;
          bool flag4 = false;
          if (hold.GetValueOrDefault() == flag4 & hold.HasValue)
            goto label_6;
        }
      }
      return flag1;
    }
label_6:
    return false;
  }

  public virtual void GetSODetValues<AppointmentDetType, SODetType>(
    PXCache cacheAppointmentDet,
    AppointmentDetType fsAppointmentDetRow,
    FSServiceOrder fsServiceOrderRow,
    FSAppointment fsAppointmentRow,
    FSSODet fsSODetRow)
    where AppointmentDetType : FSAppointmentDet, new()
    where SODetType : FSSODet, new()
  {
    if (!fsAppointmentDetRow.SODetID.HasValue)
      return;
    PXCache sourceCache = (PXCache) GraphHelper.Caches<FSSODet>(cacheAppointmentDet.Graph);
    if (fsSODetRow == null)
      fsSODetRow = FSSODet.UK.Find(cacheAppointmentDet.Graph, fsAppointmentDetRow.SODetID);
    if (fsSODetRow == null)
      return;
    ((AppointmentEntry) cacheAppointmentDet.Graph).CopyAppointmentLineValues<AppointmentDetType, FSSODet>(cacheAppointmentDet, (object) fsAppointmentDetRow, sourceCache, (object) fsSODetRow, false, fsSODetRow.TranDate, false, fsAppointmentRow == null || !fsAppointmentRow.BillServiceContractID.HasValue);
    bool? nullable;
    int num;
    if (fsAppointmentRow != null)
    {
      nullable = fsAppointmentRow.NotStarted;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    if (num != 0)
    {
      cacheAppointmentDet.SetValueExtIfDifferent<FSAppointmentDet.actualDuration>((object) fsAppointmentDetRow, (object) 0);
      cacheAppointmentDet.SetValueExtIfDifferent<FSAppointmentDet.actualQty>((object) fsAppointmentDetRow, (object) 0M);
    }
    if (fsServiceOrderRow.SourceRefNbr == null || !(fsServiceOrderRow.SourceType == "SO"))
      return;
    nullable = fsAppointmentDetRow.IsPrepaid;
    if (!nullable.GetValueOrDefault())
      return;
    fsAppointmentDetRow.LinkedDocRefNbr = fsServiceOrderRow.SourceRefNbr;
    fsAppointmentDetRow.LinkedDocType = fsServiceOrderRow.SourceDocType;
    fsAppointmentDetRow.LinkedEntityType = "SO";
  }

  public virtual void ValidateQty(
    PXCache cache,
    FSAppointmentDet fsAppointmentDetRow,
    PXErrorLevel errorLevel = 4)
  {
    Decimal? actualQty = fsAppointmentDetRow.ActualQty;
    Decimal num = 0M;
    if (!(actualQty.GetValueOrDefault() < num & actualQty.HasValue))
      return;
    PXUIFieldAttribute.SetEnabled<FSAppointmentDet.actualQty>(cache, (object) fsAppointmentDetRow, true);
    cache.RaiseExceptionHandling<FSAppointmentDet.actualQty>((object) fsAppointmentDetRow, (object) null, (Exception) new PXSetPropertyException("This value cannot be negative.", errorLevel));
  }

  /// <summary>
  /// Determines if a Service line has at least one pickup/delivery item related.
  /// </summary>
  public virtual bool ServiceLinkedToPickupDeliveryItem(
    PXGraph graph,
    FSAppointmentDet fsAppointmentDetRow,
    FSAppointment fsAppointmentRow)
  {
    return PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.sODetID, Equal<Required<FSAppointmentDet.sODetID>>, And<FSAppointmentDet.appointmentID, Equal<Required<FSAppointment.appointmentID>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) fsAppointmentDetRow.SODetID,
      (object) fsAppointmentRow.AppointmentID
    }).Count > 0;
  }

  public virtual void UpdatePendingPostFlags(
    PXCache cache,
    ServiceOrderBase<TGraph, TPrimary>.AppointmentDetails_View appointmentDetails,
    PXSelectBase<FSPostInfo> PostInfoDetails,
    FSAppointment fsAppointmentRow,
    FSServiceOrder fsServiceOrder,
    FSSrvOrdType srvOrdType)
  {
    bool flag1 = false;
    if ((fsServiceOrder.PostedBy == null || fsServiceOrder.PostedBy == "AP") && fsAppointmentRow.PostingStatusAPARSO != "PT")
    {
      int? nullable1 = new int?(0);
      int? nullable2 = new int?(0);
      int? nullable3 = new int?(0);
      bool flag2 = srvOrdType != null && srvOrdType.EnableINPosting.GetValueOrDefault() && fsAppointmentRow.IsRouteAppoinment.GetValueOrDefault();
      foreach (PXResult<FSAppointmentDet> pxResult in ((PXSelectBase<FSAppointmentDet>) appointmentDetails).Select(Array.Empty<object>()))
      {
        FSAppointmentDet det = PXResult<FSAppointmentDet>.op_Implicit(pxResult);
        FSPostInfo fsPostInfo = GraphHelper.RowCast<FSPostInfo>((IEnumerable) PostInfoDetails.Select(Array.Empty<object>())).Where<FSPostInfo>((Func<FSPostInfo, bool>) (x =>
        {
          int? postId1 = x.PostID;
          int? postId2 = det.PostID;
          return postId1.GetValueOrDefault() == postId2.GetValueOrDefault() & postId1.HasValue == postId2.HasValue;
        })).FirstOrDefault<FSPostInfo>();
        bool? isBillable;
        int? nullable4;
        if (det.IsService && det.needToBePosted())
        {
          isBillable = det.IsBillable;
          bool flag3 = false;
          if (!(isBillable.GetValueOrDefault() == flag3 & isBillable.HasValue))
          {
            if (fsPostInfo == null || !fsPostInfo.isPosted())
            {
              nullable4 = nullable1;
              nullable1 = nullable4.HasValue ? new int?(nullable4.GetValueOrDefault() + 1) : new int?();
            }
          }
          else
            continue;
        }
        if (det.IsInventoryItem && det.needToBePosted())
        {
          isBillable = det.IsBillable;
          bool flag4 = false;
          if (!(isBillable.GetValueOrDefault() == flag4 & isBillable.HasValue))
          {
            if (fsPostInfo == null || !fsPostInfo.isPosted())
            {
              nullable4 = nullable2;
              nullable2 = nullable4.HasValue ? new int?(nullable4.GetValueOrDefault() + 1) : new int?();
            }
          }
          else
            continue;
        }
        if (det.IsPickupDelivery && (fsPostInfo == null || !fsPostInfo.isPosted()))
        {
          nullable4 = nullable3;
          nullable3 = nullable4.HasValue ? new int?(nullable4.GetValueOrDefault() + 1) : new int?();
        }
        nullable4 = nullable1;
        int num1 = 0;
        if (!(nullable4.GetValueOrDefault() > num1 & nullable4.HasValue))
        {
          nullable4 = nullable2;
          int num2 = 0;
          if (!(nullable4.GetValueOrDefault() > num2 & nullable4.HasValue))
            continue;
        }
        if (flag2)
        {
          if (flag2)
          {
            nullable4 = nullable3;
            int num3 = 0;
            if (nullable4.GetValueOrDefault() > num3 & nullable4.HasValue)
              break;
          }
        }
        else
          break;
      }
      int? nullable5 = nullable1;
      int num4 = 0;
      if (!(nullable5.GetValueOrDefault() > num4 & nullable5.HasValue))
      {
        nullable5 = nullable2;
        int num5 = 0;
        if (!(nullable5.GetValueOrDefault() > num5 & nullable5.HasValue))
        {
          nullable5 = nullable3;
          int num6 = 0;
          if (!(nullable5.GetValueOrDefault() > num6 & nullable5.HasValue))
          {
            flag1 = true;
            goto label_31;
          }
        }
      }
      fsAppointmentRow.PendingAPARSOPost = new bool?(true);
      fsAppointmentRow.PostingStatusAPARSO = "PP";
      nullable5 = nullable3;
      int num7 = 0;
      if (nullable5.GetValueOrDefault() > num7 & nullable5.HasValue)
      {
        fsAppointmentRow.PendingINPost = new bool?(true);
      }
      else
      {
        fsAppointmentRow.PendingINPost = new bool?(false);
        fsAppointmentRow.PostingStatusIN = "NP";
      }
    }
    else
      flag1 = true;
label_31:
    if (!flag1)
      return;
    fsAppointmentRow.PendingAPARSOPost = new bool?(false);
    fsAppointmentRow.PendingINPost = new bool?(false);
    fsAppointmentRow.PostingStatusAPARSO = fsAppointmentRow.PostingStatusAPARSO != "PT" ? "NP" : "PT";
    fsAppointmentRow.PostingStatusIN = fsAppointmentRow.PostingStatusIN != "PT" ? "NP" : "PT";
  }

  public virtual void UpdateSalesOrderByCompletingAppointment(
    PXGraph graph,
    string sourceDocType,
    string sourceRefNbr)
  {
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) sourceDocType,
      (object) sourceRefNbr
    }));
    if (soOrder == null)
      throw new PXException("This Service Order has an invalid Sales Order reference. Please contact M5 technical support.");
    PXUpdate<Set<FSxSOOrder.installed, True>, PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Update(graph, new object[2]
    {
      (object) sourceDocType,
      (object) sourceRefNbr
    });
    foreach (PXResult<PX.Objects.SO.SOOrderShipment> pxResult in PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelect<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.orderType, Equal<Required<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<Required<PX.Objects.SO.SOOrderShipment.orderNbr>>>>>.Config>.Select(graph, new object[2]
    {
      (object) soOrder.OrderType,
      (object) soOrder.OrderNbr
    }))
    {
      PX.Objects.SO.SOOrderShipment soOrderShipment = PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(pxResult);
      PXUpdate<Set<FSxSOShipment.installed, True>, PX.Objects.SO.SOShipment, Where<PX.Objects.SO.SOShipment.shipmentNbr, Equal<Required<PX.Objects.SO.SOShipment.shipmentNbr>>>>.Update(graph, new object[1]
      {
        (object) soOrderShipment.ShipmentNbr
      });
    }
  }

  public virtual void SendNotification(
    PXCache cache,
    FSAppointment fsAppointmentRow,
    string mailing,
    int? branchID,
    IList<Guid?> attachments = null)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceOrderBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass127_0 displayClass1270 = new ServiceOrderBase<TGraph, TPrimary>.\u003C\u003Ec__DisplayClass127_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1270.cache = cache;
    // ISSUE: reference to a compiler-generated field
    displayClass1270.mailing = mailing;
    // ISSUE: reference to a compiler-generated field
    displayClass1270.branchID = branchID;
    // ISSUE: reference to a compiler-generated field
    displayClass1270.attachments = attachments;
    // ISSUE: reference to a compiler-generated field
    displayClass1270.graphAppointmentEntry = PXGraph.CreateInstance<AppointmentEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase<FSAppointment>) displayClass1270.graphAppointmentEntry.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) displayClass1270.graphAppointmentEntry.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointmentRow.RefNbr, new object[1]
    {
      (object) fsAppointmentRow.SrvOrdType
    }));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXLongOperation.StartOperation(displayClass1270.cache.Graph, new PXToggleAsyncDelegate((object) displayClass1270, __methodptr(\u003CSendNotification\u003Eb__0)));
  }

  public virtual List<FSAppointmentDet> GetRelatedApptLines(
    PXGraph graph,
    int? soDetID,
    bool excludeSpecificApptLine,
    int? apptDetID,
    bool onlyMarkForPOLines,
    bool sortResult)
  {
    return AppointmentEntry.GetRelatedApptLinesInt(graph, soDetID, excludeSpecificApptLine, apptDetID, onlyMarkForPOLines, sortResult);
  }

  public virtual void DisableAllDACFields(PXCache cache, object row, List<System.Type> fieldsToIgnore)
  {
    List<string> stringList = new List<string>();
    foreach (System.Type type in fieldsToIgnore)
      stringList.Add(type.Name.ToLower());
    foreach (string field in (List<string>) cache.Fields)
    {
      if (!field.Contains("_") && !stringList.Contains(field.ToLower()) && cache.GetAttributesReadonly(row, field).OfType<IPXInterfaceField>().FirstOrDefault<IPXInterfaceField>() != null)
        PXUIFieldAttribute.SetEnabled(cache, row, field, false);
    }
  }

  public virtual string GetLineType(string lineType, bool lower = false)
  {
    string lineType1 = "";
    switch (lineType)
    {
      case "SERVI":
        lineType1 = "Service";
        break;
      case "NSTKI":
        lineType1 = "Non-Stock Item";
        break;
      case "SLPRO":
        lineType1 = "Inventory Item";
        break;
      case "PU_DL":
        lineType1 = "Pickup/Delivery Item";
        break;
      case "CM_LN":
        lineType1 = "Comment";
        break;
      case "IT_LN":
        lineType1 = "Instruction";
        break;
    }
    if (lower)
      lineType1.ToLower();
    return lineType1;
  }

  public virtual void ValidateDuplicateLineNbr(
    PXSelectBase<FSSODet> srvOrdDetails,
    PXSelectBase<FSAppointmentDet> apptDetails)
  {
    List<int?> lineNbrs = new List<int?>();
    if (srvOrdDetails != null)
      this.ValidateDuplicateLineNbr<FSSODet>(lineNbrs, ((IEnumerable<PXResult<FSSODet>>) srvOrdDetails.Select(Array.Empty<object>())).ToList<PXResult<FSSODet>>().Select<PXResult<FSSODet>, FSSODet>((Func<PXResult<FSSODet>, FSSODet>) (e => PXResult<FSSODet>.op_Implicit(e))).ToList<FSSODet>(), ((PXSelectBase) srvOrdDetails).Cache);
    if (apptDetails == null)
      return;
    this.ValidateDuplicateLineNbr<FSAppointmentDet>(lineNbrs, ((IEnumerable<PXResult<FSAppointmentDet>>) apptDetails.Select(Array.Empty<object>())).ToList<PXResult<FSAppointmentDet>>().Select<PXResult<FSAppointmentDet>, FSAppointmentDet>((Func<PXResult<FSAppointmentDet>, FSAppointmentDet>) (e => PXResult<FSAppointmentDet>.op_Implicit(e))).ToList<FSAppointmentDet>(), ((PXSelectBase) apptDetails).Cache);
  }

  public virtual void ValidateDuplicateLineNbr<DetailType>(
    List<int?> lineNbrs,
    List<DetailType> list,
    PXCache cache)
    where DetailType : IBqlTable, IFSSODetBase
  {
    foreach (DetailType detailType in list)
    {
      DetailType row = detailType;
      if (!lineNbrs.Find((Predicate<int?>) (lineNbr =>
      {
        int? nullable = lineNbr;
        int? lineNbr1 = row.LineNbr;
        return nullable.GetValueOrDefault() == lineNbr1.GetValueOrDefault() & nullable.HasValue == lineNbr1.HasValue;
      })).HasValue)
      {
        lineNbrs.Add(row.LineNbr);
      }
      else
      {
        PXFieldState valueExt = cache.GetValueExt<FSSODet.lineNbr>((object) (DetailType) row) as PXFieldState;
        cache.RaiseExceptionHandling<FSSODet.lineNbr>((object) (DetailType) row, valueExt != null ? valueExt.Value : cache.GetValue<FSSODet.lineNbr>((object) (DetailType) row), (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
        throw new PXRowPersistingException(typeof (FSSODet.lineNbr).Name, (object) null, "An attempt was made to add a duplicate entry.");
      }
    }
  }

  public virtual void ValidateContact(FSServiceOrder row)
  {
    if (!row.ContactID.HasValue)
      return;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this, row.ContactID);
    string str = $"The contact {contact?.DisplayName} is deactivated. To perform the operation, select an active contact.";
    if (contact?.ContactType == "EP")
    {
      if (PXResult<PX.Objects.CR.BAccount>.op_Implicit(((IEnumerable<PXResult<PX.Objects.CR.BAccount>>) PXSelectBase<PX.Objects.CR.BAccount, PXViewOf<PX.Objects.CR.BAccount>.BasedOn<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.parentBAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.BAccount.defContactID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) contact.BAccountID,
        (object) contact.ContactID
      })).SingleOrNull<PXResult<PX.Objects.CR.BAccount>>())?.VStatus != "A")
        throw new PXException(str, new object[1]
        {
          (object) (PXErrorLevel) 4
        });
    }
    else if (contact?.Status != "A")
      throw new PXException(str, new object[1]
      {
        (object) (PXErrorLevel) 4
      });
  }

  public virtual void RevertInvoiceDocument(object currentRow, List<FSPostDet> postDetList)
  {
    if (currentRow == null || postDetList == null || postDetList.Count == 0)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (FSBillHistory)];
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      switch (currentRow)
      {
        case FSAppointment _:
          FSAppointment fsAppointment = (FSAppointment) currentRow;
          str1 = fsAppointment.SrvOrdType;
          str2 = fsAppointment.SORefNbr;
          str3 = fsAppointment.RefNbr;
          break;
        case FSServiceOrder _:
          FSServiceOrder fsServiceOrder = (FSServiceOrder) currentRow;
          str1 = fsServiceOrder.SrvOrdType;
          str2 = fsServiceOrder.RefNbr;
          str3 = (string) null;
          break;
      }
      FSPostDet fsPostDet1 = postDetList.Where<FSPostDet>((Func<FSPostDet, bool>) (x => x.PMPosted.GetValueOrDefault())).FirstOrDefault<FSPostDet>();
      FSPostDet fsPostDet2 = postDetList.Where<FSPostDet>((Func<FSPostDet, bool>) (x => x.INPosted.GetValueOrDefault())).FirstOrDefault<FSPostDet>();
      RegisterEntry registerEntry = (RegisterEntry) null;
      INIssueEntry graph = (INIssueEntry) null;
      bool? nullable;
      if (fsPostDet1 != null)
      {
        nullable = fsPostDet1.PMPosted;
        if (nullable.GetValueOrDefault())
        {
          registerEntry = PXGraph.CreateInstance<RegisterEntry>();
          ((PXSelectBase<PMRegister>) registerEntry.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) registerEntry.Document).Search<PMRegister.refNbr>((object) fsPostDet1.PMRefNbr, new object[1]
          {
            (object) fsPostDet1.PMDocType
          }));
          if (((PXSelectBase<PMRegister>) registerEntry.Document).Current != null && ((PXSelectBase<PMRegister>) registerEntry.Document).Current.Status != "R")
            throw new PXException("The bill cannot be reversed because it has an associated unreleased project transaction. Either release the project transaction or delete it.", new object[1]
            {
              (object) (PXErrorLevel) 4
            });
        }
      }
      if (fsPostDet2 != null)
      {
        nullable = fsPostDet2.INPosted;
        if (nullable.GetValueOrDefault())
        {
          graph = PXGraph.CreateInstance<INIssueEntry>();
          ((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Search<PX.Objects.IN.INRegister.refNbr>((object) fsPostDet2.INRefNbr, new object[1]
          {
            (object) fsPostDet2.INDocType
          }));
          if (((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current != null && ((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current.Status != "R")
            throw new PXException("The bill cannot be reversed because it has an associated unreleased issue. Either release the issue or delete it.", new object[1]
            {
              (object) (PXErrorLevel) 4
            });
        }
      }
      if (registerEntry != null && ((PXSelectBase<PMRegister>) registerEntry.Document).Current != null)
      {
        SM_RegisterEntry extension = ((PXGraph) registerEntry).GetExtension<SM_RegisterEntry>();
        PMRegister pmRegister = extension.RevertInvoice();
        cach.Insert((object) new FSBillHistory()
        {
          SrvOrdType = str1,
          ServiceOrderRefNbr = str2,
          AppointmentRefNbr = str3,
          ChildEntityType = "PXPM",
          ChildDocType = pmRegister.Module,
          ChildRefNbr = pmRegister.RefNbr,
          ParentEntityType = "PXPM",
          ParentDocType = ((PXSelectBase<PMRegister>) registerEntry.Document).Current.Module,
          ParentRefNbr = ((PXSelectBase<PMRegister>) registerEntry.Document).Current.RefNbr
        });
        extension.CleanPostingInfoLinkedToDoc((object) ((PXSelectBase<PMRegister>) registerEntry.Document).Current);
      }
      if (graph != null && ((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current != null)
      {
        SM_INIssueEntry extension = ((PXGraph) graph).GetExtension<SM_INIssueEntry>();
        PX.Objects.IN.INRegister inRegister = extension.RevertInvoice();
        cach.Insert((object) new FSBillHistory()
        {
          SrvOrdType = str1,
          ServiceOrderRefNbr = str2,
          AppointmentRefNbr = str3,
          ChildEntityType = "PXIS",
          ChildDocType = inRegister.DocType,
          ChildRefNbr = inRegister.RefNbr,
          ParentEntityType = "PXIS",
          ParentDocType = ((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current.DocType,
          ParentRefNbr = ((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current.RefNbr
        });
        FSAllocationProcess.ReallocateServiceOrderSplits(FSAllocationProcess.GetRequiredAllocationList((PXGraph) graph, (object) ((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current));
        extension.CleanPostingInfoLinkedToDoc((object) ((PXSelectBase<PX.Objects.IN.INRegister>) graph.issue).Current);
      }
      cach.Persist((PXDBOperation) 2);
      transactionScope.Complete();
    }
  }

  public virtual void CalculateBillHistoryUnboundFields(
    PXCache cache,
    FSBillHistory fsBillHistoryRow)
  {
    using (new PXConnectionScope())
      ServiceOrderBase<TGraph, TPrimary>.CalculateBillHistoryUnboundFieldsInt(cache, fsBillHistoryRow);
  }

  public static void CalculateBillHistoryUnboundFieldsInt(
    PXCache cache,
    FSBillHistory fsBillHistoryRow)
  {
    if (fsBillHistoryRow.ChildEntityType == "PXSO")
    {
      PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) fsBillHistoryRow.ChildDocType,
        (object) fsBillHistoryRow.ChildRefNbr
      }));
      if (soOrder != null)
      {
        fsBillHistoryRow.ChildDocDate = soOrder.OrderDate;
        fsBillHistoryRow.ChildDocDesc = soOrder.OrderDesc;
        fsBillHistoryRow.ChildAmount = soOrder.CuryOrderTotal;
        fsBillHistoryRow.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.SO.SOOrder.status>((PXCache) new PXCache<PX.Objects.SO.SOOrder>(cache.Graph), (object) soOrder, soOrder.Status);
      }
    }
    else if (fsBillHistoryRow.ChildEntityType == "PXAR" || fsBillHistoryRow.ChildEntityType == "PXAM")
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) fsBillHistoryRow.ChildDocType,
        (object) fsBillHistoryRow.ChildRefNbr
      }));
      if (arInvoice != null)
      {
        fsBillHistoryRow.ChildDocDate = arInvoice.DocDate;
        fsBillHistoryRow.ChildDocDesc = arInvoice.DocDesc;
        fsBillHistoryRow.ChildAmount = arInvoice.CuryOrigDocAmt;
        fsBillHistoryRow.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.AR.ARInvoice.status>((PXCache) new PXCache<PX.Objects.AR.ARInvoice>(cache.Graph), (object) arInvoice, arInvoice.Status);
      }
    }
    else if (fsBillHistoryRow.ChildEntityType == "PXSI" || fsBillHistoryRow.ChildEntityType == "PXSM")
    {
      PX.Objects.SO.SOInvoice soInvoice = PXResultset<PX.Objects.SO.SOInvoice>.op_Implicit(PXSelectBase<PX.Objects.SO.SOInvoice, PXSelect<PX.Objects.SO.SOInvoice, Where<PX.Objects.SO.SOInvoice.docType, Equal<Required<PX.Objects.SO.SOInvoice.docType>>, And<PX.Objects.SO.SOInvoice.refNbr, Equal<Required<PX.Objects.SO.SOInvoice.refNbr>>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) fsBillHistoryRow.ChildDocType,
        (object) fsBillHistoryRow.ChildRefNbr
      }));
      if (soInvoice != null)
      {
        fsBillHistoryRow.ChildDocDate = soInvoice.DocDate;
        fsBillHistoryRow.ChildDocDesc = soInvoice.DocDesc;
        fsBillHistoryRow.ChildAmount = soInvoice.CuryOrigDocAmt;
        fsBillHistoryRow.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.SO.SOInvoice.status>((PXCache) new PXCache<PX.Objects.SO.SOInvoice>(cache.Graph), (object) soInvoice, soInvoice.Status);
      }
    }
    else if (fsBillHistoryRow.ChildEntityType == "PXAP")
    {
      PX.Objects.AP.APInvoice apInvoice = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) fsBillHistoryRow.ChildDocType,
        (object) fsBillHistoryRow.ChildRefNbr
      }));
      if (apInvoice != null)
      {
        fsBillHistoryRow.ChildDocDate = apInvoice.DocDate;
        fsBillHistoryRow.ChildDocDesc = apInvoice.DocDesc;
        fsBillHistoryRow.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.AP.APInvoice.status>((PXCache) new PXCache<PX.Objects.AP.APInvoice>(cache.Graph), (object) apInvoice, apInvoice.Status);
      }
    }
    else if (fsBillHistoryRow.ChildEntityType == "PXPM")
    {
      PMRegister pmRegister = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.module, Equal<Required<PMRegister.module>>, And<PMRegister.refNbr, Equal<Required<PMRegister.refNbr>>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) fsBillHistoryRow.ChildDocType,
        (object) fsBillHistoryRow.ChildRefNbr
      }));
      if (pmRegister != null)
      {
        fsBillHistoryRow.ChildDocDesc = pmRegister.Description;
        fsBillHistoryRow.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PMRegister.status>((PXCache) new PXCache<PMRegister>(cache.Graph), (object) pmRegister, pmRegister.Status);
      }
    }
    else if (fsBillHistoryRow.ChildEntityType == "PXIS" || fsBillHistoryRow.ChildEntityType == "PXIR")
    {
      PX.Objects.IN.INRegister inRegister = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXSelect<PX.Objects.IN.INRegister, Where<PX.Objects.IN.INRegister.docType, Equal<Required<PX.Objects.IN.INRegister.docType>>, And<PX.Objects.IN.INRegister.refNbr, Equal<Required<PX.Objects.IN.INRegister.refNbr>>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) fsBillHistoryRow.ChildDocType,
        (object) fsBillHistoryRow.ChildRefNbr
      }));
      if (inRegister != null)
      {
        fsBillHistoryRow.ChildDocDate = inRegister.TranDate;
        fsBillHistoryRow.ChildDocDesc = inRegister.TranDesc;
        fsBillHistoryRow.ChildDocStatus = PXStringListAttribute.GetLocalizedLabel<PX.Objects.IN.INRegister.status>((PXCache) new PXCache<PX.Objects.IN.INRegister>(cache.Graph), (object) inRegister, inRegister.Status);
      }
    }
    if (!string.IsNullOrEmpty(fsBillHistoryRow.ChildDocStatus))
      return;
    fsBillHistoryRow.ChildDocStatus = "Deleted";
    fsBillHistoryRow.IsChildDocDeleted = new bool?(true);
  }

  public virtual void InsertDeleteRelatedFixedRateContractBill(
    PXCache docCache,
    object document,
    PXDBOperation operation,
    PXSelectBase<FSBillHistory> invoiceRecords)
  {
    bool flag = false;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    if (document is FSAppointment)
    {
      FSAppointment fsAppointment = (FSAppointment) document;
      nullable1 = (int?) fsAppointment?.BillServiceContractID;
      nullable2 = (int?) fsAppointment?.BillContractPeriodID;
    }
    else if (document is FSServiceOrder)
    {
      FSServiceOrder fsServiceOrder = (FSServiceOrder) document;
      nullable1 = (int?) fsServiceOrder?.BillServiceContractID;
      nullable2 = (int?) fsServiceOrder?.BillContractPeriodID;
    }
    if (operation == 1)
    {
      int? nullable3 = nullable1;
      int? nullable4 = (int?) docCache.GetValueOriginal<FSAppointment.billServiceContractID>(document);
      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
      {
        nullable4 = nullable2;
        int? valueOriginal = (int?) docCache.GetValueOriginal<FSAppointment.billContractPeriodID>(document);
        if (nullable4.GetValueOrDefault() == valueOriginal.GetValueOrDefault() & nullable4.HasValue == valueOriginal.HasValue)
          goto label_8;
      }
      this.DeleteRelatedFixedRateContractBills(docCache, document, invoiceRecords);
      flag = true;
    }
label_8:
    if (!nullable1.HasValue || !(operation == 2 | flag))
      return;
    this.InsertRelatedFixedRateContractBill(docCache, document, invoiceRecords);
  }

  public virtual void DeleteRelatedFixedRateContractBills(
    PXCache docCache,
    object document,
    PXSelectBase<FSBillHistory> invoiceRecords)
  {
    FSServiceContract serviceContract = PXResultset<FSServiceContract>.op_Implicit(PXSelectBase<FSServiceContract, PXSelect<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>, And<FSServiceContract.isFixedRateContract, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (int?) docCache.GetValueOriginal<FSAppointment.billServiceContractID>(document)
    }));
    if (serviceContract == null)
      return;
    foreach (FSBillHistory fsBillHistory in GraphHelper.RowCast<FSBillHistory>((IEnumerable) invoiceRecords.Select(Array.Empty<object>())).Where<FSBillHistory>((Func<FSBillHistory, bool>) (x => x.ServiceContractRefNbr == serviceContract.RefNbr)))
      invoiceRecords.Delete(fsBillHistory);
    ((PXSelectBase) invoiceRecords).Cache.Persist((PXDBOperation) 3);
  }

  public virtual void InsertRelatedFixedRateContractBill(
    PXCache docCache,
    object document,
    PXSelectBase<FSBillHistory> invoiceRecords)
  {
    FSAppointment appointment = (FSAppointment) null;
    FSServiceOrder serviceOrder = (FSServiceOrder) null;
    switch (document)
    {
      case FSAppointment _:
        appointment = (FSAppointment) document;
        break;
      case FSServiceOrder _:
        serviceOrder = (FSServiceOrder) document;
        break;
    }
    int? nullable1 = (int?) serviceOrder?.BillServiceContractID;
    int? nullable2 = (int?) (nullable1 ?? appointment?.BillServiceContractID);
    nullable1 = (int?) serviceOrder?.BillContractPeriodID;
    int? nullable3 = (int?) (nullable1 ?? appointment?.BillContractPeriodID);
    string srvOrdType = serviceOrder?.SrvOrdType ?? appointment?.SrvOrdType;
    PXResultset<FSBillHistory> source = PXSelectBase<FSBillHistory, PXSelectJoin<FSBillHistory, InnerJoin<FSServiceContract, On<FSServiceContract.refNbr, Equal<FSBillHistory.serviceContractRefNbr>>, InnerJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Invoiced>>>, InnerJoin<FSContractPostDoc, On<FSContractPostDoc.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPostDoc.contractPeriodID, Equal<FSContractPeriod.contractPeriodID>, And<FSContractPostDoc.postDocType, Equal<FSBillHistory.childDocType>, And<FSContractPostDoc.postRefNbr, Equal<FSBillHistory.childRefNbr>>>>>>>>, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>, And<FSContractPeriod.contractPeriodID, Equal<Required<FSContractPeriod.contractPeriodID>>, And<FSServiceContract.isFixedRateContract, Equal<True>, And<FSBillHistory.appointmentRefNbr, IsNull, And<FSBillHistory.serviceOrderRefNbr, IsNull>>>>>>.Config>.Select(docCache.Graph, new object[2]
    {
      (object) nullable2,
      (object) nullable3
    });
    if (source == null)
      return;
    if (((IQueryable<PXResult<FSBillHistory>>) source).Count<PXResult<FSBillHistory>>() == 1)
    {
      FSBillHistory contractBillHistory = PXResultset<FSBillHistory>.op_Implicit(source);
      if (GraphHelper.RowCast<FSBillHistory>((IEnumerable) invoiceRecords.Select(Array.Empty<object>())).Where<FSBillHistory>((Func<FSBillHistory, bool>) (x => x.ServiceContractRefNbr == contractBillHistory.ServiceContractRefNbr && x.SrvOrdType == srvOrdType && x.AppointmentRefNbr == appointment?.RefNbr && x.ServiceOrderRefNbr == serviceOrder?.RefNbr && x.ChildDocType == contractBillHistory.ChildDocType && x.ChildRefNbr == contractBillHistory.ChildRefNbr)).Any<FSBillHistory>())
        return;
      ((PXSelectBase) invoiceRecords).Cache.Insert((object) new FSBillHistory()
      {
        ServiceContractRefNbr = contractBillHistory.ServiceContractRefNbr,
        SrvOrdType = srvOrdType,
        AppointmentRefNbr = appointment?.RefNbr,
        ServiceOrderRefNbr = serviceOrder?.RefNbr,
        ChildEntityType = contractBillHistory.ChildEntityType,
        ChildDocType = contractBillHistory.ChildDocType,
        ChildRefNbr = contractBillHistory.ChildRefNbr
      });
      ((PXSelectBase) invoiceRecords).Cache.Persist((PXDBOperation) 2);
      ((PXSelectBase) invoiceRecords).Cache.Clear();
    }
    else if (((IQueryable<PXResult<FSBillHistory>>) source).Count<PXResult<FSBillHistory>>() > 1)
      throw new InvalidOperationException();
  }

  protected void SetReadOnly(PXCache documentCache, bool isReadOnly)
  {
    bool allowInsert = documentCache.AllowInsert;
    PXCache[] array = new PXCache[((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Count];
    try
    {
      ((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Values.CopyTo(array, 0);
    }
    catch (ArgumentException ex)
    {
      array = new PXCache[((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Count + 5];
      ((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Values.CopyTo(array, 0);
    }
    foreach (PXCache pxCache in array)
    {
      if (pxCache != null)
      {
        pxCache.AllowDelete = !isReadOnly;
        pxCache.AllowUpdate = !isReadOnly;
        pxCache.AllowInsert = !isReadOnly;
      }
    }
    documentCache.AllowInsert = allowInsert;
  }

  public virtual bool IsValidForProfitability(string lineType, Decimal? curyUnitCost)
  {
    return lineType == "SLPRO" || lineType == "NSTKI" || lineType == "SERVI";
  }

  public virtual bool IsValidForProfitability(FSSODet row)
  {
    return row.Status != "CC" && this.IsValidForProfitability(row.LineType, row.CuryUnitCost);
  }

  public virtual bool IsValidForProfitability(FSAppointmentDet row)
  {
    return row.Status != null && EnumerableExtensions.IsNotIn<string>(row.Status, "CC", "NP", "RP") && this.IsValidForProfitability(row.LineType, row.CuryUnitCost);
  }

  public virtual IEnumerable<FSProfitability> ProfitabilityRecords_INItems(
    PXGraph graph,
    FSServiceOrder serviceOrder,
    FSAppointment appointment)
  {
    if (appointment != null)
      return GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.srvOrdType, Equal<Required<FSAppointmentDet.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Required<FSAppointmentDet.refNbr>>>>, OrderBy<Asc<FSAppointmentDet.sortOrder, Asc<FSAppointmentDet.lineNbr>>>>.Config>.Select(graph, new object[2]
      {
        (object) appointment.SrvOrdType,
        (object) appointment.RefNbr
      })).Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x => this.IsValidForProfitability(x))).Select<FSAppointmentDet, FSProfitability>((Func<FSAppointmentDet, FSProfitability>) (x => this.PrepareProfitabilityRow(x)));
    if (serviceOrder == null)
      return Enumerable.Empty<FSProfitability>();
    string billingBy = this.GetBillingMode(serviceOrder);
    return GraphHelper.RowCast<FSSODet>((IEnumerable) PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.srvOrdType, Equal<Required<FSSODet.srvOrdType>>, And<FSSODet.refNbr, Equal<Required<FSSODet.refNbr>>>>, OrderBy<Asc<FSSODet.sortOrder, Asc<FSSODet.lineNbr>>>>.Config>.Select(graph, new object[2]
    {
      (object) serviceOrder.SrvOrdType,
      (object) serviceOrder.RefNbr
    })).Where<FSSODet>((Func<FSSODet, bool>) (x => this.IsValidForProfitability(x))).Select<FSSODet, FSProfitability>((Func<FSSODet, FSProfitability>) (x => this.PrepareProfitabilityRow(x, billingBy)));
  }

  public virtual FSProfitability PrepareProfitabilityRow(FSSODet soDet, string billingBy)
  {
    return new FSProfitability(soDet, billingBy);
  }

  public virtual FSProfitability PrepareProfitabilityRow(FSAppointmentDet appointmentDet)
  {
    return new FSProfitability(appointmentDet);
  }

  public virtual List<FSProfitability> ProfitabilityRecords_Logs(
    PXGraph graph,
    FSServiceOrder serviceOrder)
  {
    List<FSProfitability> first = new List<FSProfitability>();
    if (serviceOrder == null)
      return first;
    foreach (FSAppointment appointment in GraphHelper.RowCast<FSAppointment>((IEnumerable) PXSelectBase<FSAppointment, PXSelect<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Required<FSAppointment.srvOrdType>>, And<FSAppointment.soRefNbr, Equal<Required<FSAppointment.soRefNbr>>>>, OrderBy<Asc<FSAppointment.srvOrdType, Asc<FSAppointment.refNbr>>>>.Config>.Select(graph, new object[2]
    {
      (object) serviceOrder.SrvOrdType,
      (object) serviceOrder.RefNbr
    })).Where<FSAppointment>((Func<FSAppointment, bool>) (x => x.InProcess.GetValueOrDefault() || x.Paused.GetValueOrDefault() || x.Completed.GetValueOrDefault() || x.Closed.GetValueOrDefault())))
      first = first.Concat<FSProfitability>((IEnumerable<FSProfitability>) this.ProfitabilityRecords_Logs((PXGraph) this, appointment)).ToList<FSProfitability>();
    return first;
  }

  public virtual List<FSProfitability> ProfitabilityRecords_Logs(
    PXGraph graph,
    FSAppointment appointment)
  {
    List<FSProfitability> fsProfitabilityList = new List<FSProfitability>();
    if (appointment == null)
      return fsProfitabilityList;
    foreach (FSAppointmentLog appointmentLog in GraphHelper.RowCast<FSAppointmentLog>((IEnumerable) PXSelectBase<FSAppointmentLog, PXSelect<FSAppointmentLog, Where<FSAppointmentLog.docType, Equal<Required<FSAppointmentLog.docType>>, And<FSAppointmentLog.docRefNbr, Equal<Required<FSAppointmentLog.docRefNbr>>>>, OrderBy<Asc<FSAppointmentLog.lineNbr>>>.Config>.Select(graph, new object[2]
    {
      (object) appointment.SrvOrdType,
      (object) appointment.RefNbr
    })).Where<FSAppointmentLog>((Func<FSAppointmentLog, bool>) (x =>
    {
      if (!x.BAccountID.HasValue)
        return false;
      Decimal? curyUnitCost = x.CuryUnitCost;
      Decimal num = 0M;
      return !(curyUnitCost.GetValueOrDefault() == num & curyUnitCost.HasValue);
    })))
      fsProfitabilityList.Add(this.PrepareProfitabilityRow(graph, appointmentLog));
    return fsProfitabilityList;
  }

  public virtual FSProfitability PrepareProfitabilityRow(
    PXGraph graph,
    FSAppointmentLog appointmentLog)
  {
    return new FSProfitability((FSLog) appointmentLog)
    {
      Descr = PX.Objects.IN.InventoryItem.PK.Find(graph, appointmentLog.LaborItemID).Descr
    };
  }

  public virtual void ValidateServiceContractDates(
    PXCache docCache,
    object document,
    FSServiceContract fsServiceContractRow)
  {
    int? serviceContractID = new int?();
    DateTime? nullable1 = new DateTime?();
    string str = string.Empty;
    if (document == null)
      return;
    if (document is FSAppointment)
    {
      FSAppointment fsAppointment = document as FSAppointment;
      serviceContractID = fsAppointment.BillServiceContractID;
      nullable1 = fsAppointment.ExecutionDate;
      str = typeof (FSAppointment.executionDate).Name;
    }
    else if (document is FSServiceOrder)
    {
      FSServiceOrder fsServiceOrder = document as FSServiceOrder;
      serviceContractID = fsServiceOrder.BillServiceContractID;
      nullable1 = fsServiceOrder.OrderDate;
      str = typeof (FSServiceOrder.orderDate).Name;
    }
    if (!serviceContractID.HasValue)
      return;
    if (fsServiceContractRow == null)
      fsServiceContractRow = FSServiceContract.PK.Find(docCache.Graph, serviceContractID);
    if (!nullable1.HasValue)
    {
      docCache.RaiseExceptionHandling(str, document, (object) nullable1, (Exception) new PXSetPropertyException("The date is incorrect. The date must be the same as or later than the start date of the related contract.", (PXErrorLevel) 4));
    }
    else
    {
      if (!(fsServiceContractRow.ExpirationType == "E") && !(fsServiceContractRow.ExpirationType == "R"))
        return;
      DateTime? nullable2 = fsServiceContractRow.StartDate;
      DateTime? nullable3 = nullable1;
      if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        docCache.RaiseExceptionHandling(str, document, (object) nullable1, (Exception) new PXSetPropertyException("The date is incorrect. The date must be the same as or later than the start date of the related contract.", (PXErrorLevel) 4));
      }
      else
      {
        nullable3 = fsServiceContractRow.EndDate;
        nullable2 = nullable1;
        if ((nullable3.HasValue & nullable2.HasValue ? (nullable3.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
        if (fsServiceContractRow.ExpirationType == "E")
          docCache.RaiseExceptionHandling(str, document, (object) nullable1, (Exception) new PXSetPropertyException("The date is incorrect. The date must be earlier than or the same as the expiration date of the related contract.", (PXErrorLevel) 4));
        else
          docCache.RaiseExceptionHandling(str, document, (object) nullable1, (Exception) new PXSetPropertyException("The date is later than the related service contract expiration date.", (PXErrorLevel) 2));
      }
    }
  }

  protected virtual bool IsInstructionOrComment(object eRow)
  {
    if (eRow == null)
      return false;
    IFSSODetBase fssoDetBase = (IFSSODetBase) eRow;
    return fssoDetBase.LineType == "CM_LN" || fssoDetBase.LineType == "IT_LN";
  }

  public virtual string FormatQty(Decimal? value)
  {
    return value.HasValue ? value.Value.ToString("N" + CommonSetupDecPl.Qty.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) : string.Empty;
  }

  public virtual bool InventoryItemsAreIncluded() => false;

  protected virtual INLotSerClass GetLotSerialClass(int? inventoryID)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID);
    return inventoryItem != null && inventoryItem.LotSerClassID != null ? INLotSerClass.PK.Find((PXGraph) this, inventoryItem.LotSerClassID) : (INLotSerClass) null;
  }

  protected void ValidateLinesLotSerials<DetailType, DetailSplitType, Field>(
    PXCache detailsCache,
    IEnumerable<DetailType> details,
    PXView splitsView,
    object record,
    string postTo)
    where DetailType : IBqlTable, IFSSODetBase
    where DetailSplitType : IBqlTable, ILSDetail
    where Field : IBqlField
  {
    if (postTo != "SO")
      return;
    foreach (DetailType detail in details)
    {
      PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = SharedFunctions.ReadInventoryItem(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], detail.InventoryID);
      if (pxResult != null)
      {
        INLotSerClass inLotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
        if ((inLotSerClass != null ? (!inLotSerClass.UseLotSerSpecificDetails.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        {
          if (GraphHelper.RowCast<DetailSplitType>((IEnumerable) splitsView.SelectMultiBound(new object[2]
          {
            record,
            (object) detail
          }, Array.Empty<object>())).GroupBy<DetailSplitType, string>((Func<DetailSplitType, string>) (s => s.LotSerialNbr)).Count<IGrouping<string, DetailSplitType>>() > 1)
            this.RaiseLineLotSerialException<DetailType, Field>(detailsCache, detail, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
        }
      }
    }
  }

  private void RaiseLineLotSerialException<DetailType, Field>(
    PXCache cache,
    DetailType line,
    PX.Objects.IN.InventoryItem inventoryItem)
    where DetailType : IBqlTable, IFSSODetBase
    where Field : IBqlField
  {
    string str = inventoryItem.InventoryCD.TrimEnd();
    if (cache.RaiseExceptionHandling<Field>((object) line, (object) null, (Exception) new PXSetPropertyException<Field>("Selecting multiple lot or serial numbers in a single line for the item for which the Specify Lot/Serial Price and Description check box is selected in the item's lot or serial class is not supported in sales orders and invoices. To avoid errors during the billing process, add a separate line for each lot or serial number.", new object[1]
    {
      (object) str
    })))
      throw new PXRowPersistingException(typeof (Field).Name, (object) null, "Selecting multiple lot or serial numbers in a single line for the item for which the Specify Lot/Serial Price and Description check box is selected in the item's lot or serial class is not supported in sales orders and invoices. To avoid errors during the billing process, add a separate line for each lot or serial number.", new object[1]
      {
        (object) str
      });
  }

  public abstract class fakeField : IBqlField, IBqlOperand
  {
  }

  public enum EventType
  {
    RowSelectedEvent,
    RowPersistingEvent,
  }

  public class CurrentServiceOrder_View : 
    PXSelect<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>>
  {
    public CurrentServiceOrder_View(PXGraph graph)
      : base(graph)
    {
    }

    public CurrentServiceOrder_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ServiceOrderAppointments_View : 
    PXSelect<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSAppointment.soRefNbr, Equal<Current<FSServiceOrder.refNbr>>>>, OrderBy<Asc<FSAppointment.refNbr>>>
  {
    public ServiceOrderAppointments_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceOrderAppointments_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class ServiceOrderEquipment_View : 
    PXSelectJoin<FSSOResource, LeftJoin<FSEquipment, On<FSEquipment.SMequipmentID, Equal<FSSOResource.SMequipmentID>>>, Where<FSSOResource.sOID, Equal<Current<FSServiceOrder.sOID>>>>
  {
    public ServiceOrderEquipment_View(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceOrderEquipment_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class RelatedServiceOrders_View : 
    PXSelectReadonly<RelatedServiceOrder, Where<FSServiceOrder.sourceDocType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSServiceOrder.sourceRefNbr, Equal<Current<FSServiceOrder.refNbr>>>>>
  {
    public RelatedServiceOrders_View(PXGraph graph)
      : base(graph)
    {
    }

    public RelatedServiceOrders_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class FSContact_View : 
    PXSelect<FSContact, Where<FSContact.contactID, Equal<Current<FSServiceOrder.serviceOrderContactID>>>>
  {
    public FSContact_View(PXGraph graph)
      : base(graph)
    {
    }

    public FSContact_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class FSAddress_View : 
    PXSelect<FSAddress, Where<FSAddress.addressID, Equal<Current<FSServiceOrder.serviceOrderAddressID>>>>
  {
    public FSAddress_View(PXGraph graph)
      : base(graph)
    {
    }

    public FSAddress_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  [PXDynamicButton(new string[] {"DetailsPasteLine", "DetailsResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
  public class ServiceOrderDetailsOrdered : 
    PXOrderedSelect<FSServiceOrder, FSSODet, Where<FSSODet.srvOrdType, Equal<Current<FSServiceOrder.srvOrdType>>, And<FSSODet.refNbr, Equal<Current<FSServiceOrder.refNbr>>>>, OrderBy<Asc<FSSODet.srvOrdType, Asc<FSSODet.refNbr, Asc<FSSODet.sortOrder, Asc<FSSODet.lineNbr>>>>>>
  {
    public const string DetailsPasteLineCommand = "DetailsPasteLine";
    public const string DetailsResetOrderCommand = "DetailsResetOrder";

    public ServiceOrderDetailsOrdered(PXGraph graph)
      : base(graph)
    {
    }

    public ServiceOrderDetailsOrdered(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void AddActions(PXGraph graph)
    {
      PXGraph pxGraph1 = graph;
      ServiceOrderBase<TGraph, TPrimary>.ServiceOrderDetailsOrdered orderDetailsOrdered1 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) orderDetailsOrdered1, __vmethodptr(orderDetailsOrdered1, PasteLine));
      ((PXOrderedSelectBase<FSServiceOrder, FSSODet>) this).AddAction(pxGraph1, "DetailsPasteLine", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
      PXGraph pxGraph2 = graph;
      ServiceOrderBase<TGraph, TPrimary>.ServiceOrderDetailsOrdered orderDetailsOrdered2 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) orderDetailsOrdered2, __vmethodptr(orderDetailsOrdered2, ResetOrder));
      ((PXOrderedSelectBase<FSServiceOrder, FSSODet>) this).AddAction(pxGraph2, "DetailsResetOrder", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
    }
  }

  public class AppointmentRecords_View : 
    PXSelectJoin<FSAppointment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSAppointment.customerID>>>, Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>
  {
    public AppointmentRecords_View(PXGraph graph)
      : base(graph)
    {
    }

    public AppointmentRecords_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class AppointmentSelected_View : 
    PXSelect<FSAppointment, Where<FSAppointment.appointmentID, Equal<Current<FSAppointment.appointmentID>>>>
  {
    public AppointmentSelected_View(PXGraph graph)
      : base(graph)
    {
    }

    public AppointmentSelected_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  [PXDynamicButton(new string[] {"DetailsPasteLine", "DetailsResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
  public class AppointmentDetails_View : 
    PXOrderedSelect<FSAppointment, FSAppointmentDet, Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointment.refNbr>>>>, OrderBy<Asc<FSAppointmentDet.srvOrdType, Asc<FSAppointmentDet.refNbr, Asc<FSAppointmentDet.sortOrder, Asc<FSAppointmentDet.lineNbr>>>>>>
  {
    public const string DetailsPasteLineCommand = "DetailsPasteLine";
    public const string DetailsResetOrderCommand = "DetailsResetOrder";

    public AppointmentDetails_View(PXGraph graph)
      : base(graph)
    {
    }

    public AppointmentDetails_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void AddActions(PXGraph graph)
    {
      PXGraph pxGraph1 = graph;
      ServiceOrderBase<TGraph, TPrimary>.AppointmentDetails_View appointmentDetailsView1 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) appointmentDetailsView1, __vmethodptr(appointmentDetailsView1, PasteLine));
      ((PXOrderedSelectBase<FSAppointment, FSAppointmentDet>) this).AddAction(pxGraph1, "DetailsPasteLine", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
      PXGraph pxGraph2 = graph;
      ServiceOrderBase<TGraph, TPrimary>.AppointmentDetails_View appointmentDetailsView2 = this;
      // ISSUE: virtual method pointer
      PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) appointmentDetailsView2, __vmethodptr(appointmentDetailsView2, ResetOrder));
      ((PXOrderedSelectBase<FSAppointment, FSAppointmentDet>) this).AddAction(pxGraph2, "DetailsResetOrder", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
    }
  }

  public class AppointmentResources_View : 
    PXSelectJoin<FSAppointmentResource, LeftJoin<FSEquipment, On<FSEquipment.SMequipmentID, Equal<FSAppointmentResource.SMequipmentID>>>, Where<FSAppointmentResource.appointmentID, Equal<Current<FSAppointment.appointmentID>>>>
  {
    public AppointmentResources_View(PXGraph graph)
      : base(graph)
    {
    }

    public AppointmentResources_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }

  public class AppointmentLog_View : 
    PXSelectJoin<FSAppointmentLog, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.lineRef, Equal<FSAppointmentLog.detLineRef>, And<FSAppointmentDet.appointmentID, Equal<FSAppointmentLog.docID>>>>, Where<FSAppointmentLog.docID, Equal<Current<FSAppointment.appointmentID>>>>
  {
    public AppointmentLog_View(PXGraph graph)
      : base(graph)
    {
    }

    public AppointmentLog_View(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
