// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DialogBoxSOApptCreation`3
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public abstract class DialogBoxSOApptCreation<TExtension, TGraph, TMain> : 
  PXGraphExtension<TExtension, TGraph>
  where TExtension : PXGraphExtension<TGraph>, new()
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  public CRValidationFilter<DBoxDocSettings> DocumentSettings;
  public bool ProjectSelectorEnabled = true;
  public PXAction<TMain> CreateSrvOrdDocument;
  public PXAction<TMain> CreateApptDocument;
  private PXAction<TMain> CreateInCalendar;

  [PXButton]
  [PXUIField]
  public virtual IEnumerable createSrvOrdDocument(PXAdapter adapter)
  {
    // ISSUE: method pointer
    this.ShowDialogBoxAndProcess(this.DocumentSettings.AskExtFullyValid(new PXView.InitializePanel((object) this, __methodptr(\u003CcreateSrvOrdDocument\u003Eb__3_0)), (DialogAnswerType) 1, false));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable createApptDocument(PXAdapter adapter)
  {
    // ISSUE: method pointer
    this.ShowDialogBoxAndProcess(this.DocumentSettings.AskExtFullyValid(new PXView.InitializePanel((object) this, __methodptr(\u003CcreateApptDocument\u003Eb__5_0)), (DialogAnswerType) 1, true));
    return adapter.Get();
  }

  [PXSuppressActionValidation]
  [PXButton]
  [PXUIField]
  public virtual void createInCalendar() => throw new NotImplementedException();

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<DBoxDocSettings, DBoxDocSettings.scheduledDateTimeBegin> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<DBoxDocSettings, DBoxDocSettings.scheduledDateTimeBegin>, DBoxDocSettings, object>) e).NewValue = (object) PXDBDateAndTimeAttribute.CombineDateTime(((PXGraphExtension<TGraph>) this).Base.Accessinfo.BusinessDate, new DateTime?(PXTimeZoneInfo.Now));
  }

  protected virtual void _(PX.Data.Events.RowSelected<DBoxDocSettings> e)
  {
    if (e.Row == null)
      return;
    DBoxDocSettings row = e.Row;
    PXDefaultAttribute.SetPersistingCheck<DBoxDocSettings.scheduledDateTimeBegin>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, (object) e.Row, row.DestinationDocument == "AP" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<DBoxDocSettings.scheduledDateTimeEnd>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, (object) e.Row, !(row.DestinationDocument == "AP") || !e.Row.HandleManuallyScheduleTime.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXUIFieldAttribute.SetVisible<DBoxDocSettings.projectTaskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, (object) e.Row, !ProjectDefaultAttribute.IsNonProject(e.Row.ProjectID));
    PXUIFieldAttribute.SetEnabled<DBoxDocSettings.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, (object) e.Row, this.ProjectSelectorEnabled);
    PXUIFieldAttribute.SetRequired<DBoxDocSettings.projectTaskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, ProjectDefaultAttribute.IsProject((PXGraph) ((PXGraphExtension<TGraph>) this).Base, e.Row.ProjectID));
    PXDefaultAttribute.SetPersistingCheck<DBoxDocSettings.projectTaskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DBoxDocSettings>>) e).Cache, (object) e.Row, ProjectDefaultAttribute.IsProject((PXGraph) ((PXGraphExtension<TGraph>) this).Base, e.Row.ProjectID) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public virtual FSServiceOrder CreateDocument(
    ServiceOrderEntry srvOrdGraph,
    AppointmentEntry apptGraph,
    string sourceDocumentEntity,
    string sourceDocType,
    string sourceDocRefNbr,
    int? sourceDocID,
    PXCache headerCache,
    PXCache linesCache,
    DBoxHeader header,
    List<DBoxDetails> details,
    bool createAppointment)
  {
    ((PXGraph) srvOrdGraph).Clear();
    bool flag1 = header.sourceDocument is CROpportunity;
    bool flag2 = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      flag2 = new ProjectSettingsManager().CalculateProjectSpecificTaxes & flag1;
    FSServiceOrder fsServiceOrder1 = ((PXSelectBase<FSServiceOrder>) srvOrdGraph.ServiceOrderRecords).Current = ((PXSelectBase<FSServiceOrder>) srvOrdGraph.ServiceOrderRecords).Insert(new FSServiceOrder()
    {
      SrvOrdType = header.SrvOrdType,
      SourceType = sourceDocumentEntity,
      SourceDocType = sourceDocType,
      SourceRefNbr = sourceDocRefNbr,
      SourceID = sourceDocID
    });
    DocGenerationHelper.ValidateAutoNumbering((PXGraph) ((PXGraphExtension<TGraph>) this).Base, ((PXSelectBase<FSSrvOrdType>) srvOrdGraph.ServiceOrderTypeSelected).SelectSingle(Array.Empty<object>())?.SrvOrdNumberingID);
    FSServiceOrder copy1 = (FSServiceOrder) ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache.CreateCopy((object) fsServiceOrder1);
    copy1.ProjectID = header.ProjectID;
    copy1.DfltProjectTaskID = header.ProjectTaskID;
    FSSrvOrdType fsSrvOrdTypeRow = FSSrvOrdType.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, header.SrvOrdType);
    if (fsSrvOrdTypeRow.Behavior != "IN")
    {
      copy1.CustomerID = header.CustomerID;
      copy1.LocationID = header.LocationID;
      FSServiceOrder fsServiceOrder2 = ((PXSelectBase<FSServiceOrder>) srvOrdGraph.ServiceOrderRecords).Update(copy1);
      copy1 = (FSServiceOrder) ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache.CreateCopy((object) fsServiceOrder2);
    }
    copy1.CuryID = header.CuryID;
    copy1.BranchID = header.BranchID;
    copy1.BranchLocationID = header.BranchLocationID;
    copy1.ContactID = header.ContactID;
    copy1.DocDesc = header.Description;
    copy1.LongDescr = header.LongDescr;
    copy1.SalesPersonID = header.SalesPersonID;
    copy1.OrderDate = header.OrderDate;
    copy1.SLAETA = header.SLAETA;
    copy1.ProblemID = header.ProblemID;
    copy1.AssignedEmpID = header.AssignedEmpID;
    FSServiceOrder fsServiceOrder3 = ((PXSelectBase<FSServiceOrder>) srvOrdGraph.ServiceOrderRecords).Update(copy1);
    FSServiceOrder copy2 = (FSServiceOrder) ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache.CreateCopy((object) fsServiceOrder3);
    if (header.Contact != null)
    {
      FSContact fsContact = PXResultset<FSContact>.op_Implicit(((PXSelectBase<FSContact>) srvOrdGraph.ServiceOrder_Contact).Select(Array.Empty<object>()));
      FSContact copy3 = (FSContact) ((PXSelectBase) srvOrdGraph.ServiceOrder_Contact).Cache.CreateCopy((object) fsContact);
      copy3.FullName = header.Contact.FullName;
      copy3.Title = header.Contact.Title;
      copy3.Attention = header.Contact.Attention;
      copy3.Email = header.Contact.Email;
      copy3.Phone1 = header.Contact.Phone1;
      copy3.Phone2 = header.Contact.Phone2;
      copy3.Phone3 = header.Contact.Phone3;
      copy3.Fax = header.Contact.Fax;
      ((PXSelectBase<FSContact>) srvOrdGraph.ServiceOrder_Contact).Update(copy3);
    }
    if (header.Address != null && !flag2)
    {
      FSAddress fsAddress = PXResultset<FSAddress>.op_Implicit(((PXSelectBase<FSAddress>) srvOrdGraph.ServiceOrder_Address).Select(Array.Empty<object>()));
      FSAddress copy4 = (FSAddress) ((PXSelectBase) srvOrdGraph.ServiceOrder_Address).Cache.CreateCopy((object) fsAddress);
      copy4.AddressLine1 = header.Address.AddressLine1;
      copy4.AddressLine2 = header.Address.AddressLine2;
      copy4.AddressLine3 = header.Address.AddressLine3;
      copy4.City = header.Address.City;
      copy4.CountryID = header.Address.CountryID;
      copy4.State = header.Address.State;
      copy4.PostalCode = header.Address.PostalCode;
      ((PXSelectBase<FSAddress>) srvOrdGraph.ServiceOrder_Address).Update(copy4);
    }
    if (copy2.TaxZoneID != header.TaxZoneID)
      copy2.TaxZoneID = header.TaxZoneID;
    FSServiceOrder fsServiceOrder4 = ((PXSelectBase<FSServiceOrder>) srvOrdGraph.ServiceOrderRecords).Update(copy2);
    bool? nullable1;
    if (!header.CopyNotes.GetValueOrDefault())
    {
      nullable1 = header.CopyFiles;
      if (!nullable1.GetValueOrDefault())
        goto label_13;
    }
    SharedFunctions.CopyNotesAndFiles(headerCache, ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache, header.sourceDocument, (object) fsServiceOrder4, header.CopyNotes, header.CopyFiles);
label_13:
    UDFHelper.CopyAttributes(headerCache, header.sourceDocument, ((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache, (object) fsServiceOrder4, (string) null);
    foreach (DBoxDetails detail in details)
    {
      int? nullable2 = detail.InventoryID;
      if (nullable2.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItemRow = SharedFunctions.GetInventoryItemRow((PXGraph) srvOrdGraph, detail.InventoryID);
        nullable1 = inventoryItemRow.StkItem;
        if (nullable1.GetValueOrDefault() && ((PXSelectBase<FSSrvOrdType>) srvOrdGraph.ServiceOrderTypeSelected).Current.PostTo == "AR")
          throw new PXException("The service order cannot be created because the {0} stock item cannot be added to orders of the selected service order type. Select a service order type that generates invoices in the Sales Orders module.", new object[1]
          {
            (object) inventoryItemRow.InventoryCD
          });
        FSSODet fssoDet1 = new FSSODet();
        PXCache cache = ((PXSelectBase) srvOrdGraph.ServiceOrderDetails).Cache;
        fssoDet1.SourceNoteID = detail.SourceNoteID;
        FSSODet fssoDet2;
        FSSODet fssoDet3 = fssoDet2 = (FSSODet) cache.Insert((object) fssoDet1);
        cache.Current = (object) fssoDet2;
        FSSODet copy5 = (FSSODet) cache.CreateCopy((object) fssoDet3);
        copy5.LineType = detail.LineType;
        copy5.InventoryID = detail.InventoryID;
        copy5.UOM = detail.UOM;
        copy5.IsFree = detail.IsFree;
        FSSODet fssoDet4;
        FSSODet fssoDet5 = fssoDet4 = (FSSODet) cache.Update((object) copy5);
        cache.Current = (object) fssoDet4;
        FSSODet copy6 = (FSSODet) cache.CreateCopy((object) fssoDet5);
        copy6.BillingRule = detail.BillingRule;
        copy6.TranDesc = detail.TranDesc;
        nullable2 = detail.SiteID;
        if (nullable2.HasValue)
          copy6.SiteID = detail.SiteID;
        nullable2 = detail.EstimatedDuration;
        if (nullable2.HasValue)
          copy6.EstimatedDuration = detail.EstimatedDuration;
        copy6.EstimatedQty = detail.EstimatedQty;
        FSSODet copy7 = (FSSODet) cache.CreateCopy((object) (FSSODet) cache.Update((object) copy6));
        copy7.CuryUnitPrice = detail.CuryUnitPrice;
        copy7.ManualPrice = detail.ManualPrice;
        nullable2 = detail.ProjectID;
        if (nullable2.HasValue)
        {
          nullable2 = detail.ProjectTaskID;
          if (nullable2.HasValue)
          {
            copy7.ProjectID = detail.ProjectID;
            copy7.ProjectTaskID = detail.ProjectTaskID;
            goto label_26;
          }
        }
        copy7.ProjectID = header.ProjectID;
        copy7.ProjectTaskID = header.ProjectTaskID;
label_26:
        copy7.CostCodeID = detail.CostCodeID;
        copy7.ManualCost = detail.EnablePO;
        nullable1 = copy7.ManualCost;
        if (nullable1.GetValueOrDefault())
          copy7.CuryUnitCost = detail.CuryUnitCost;
        copy7.EnablePO = detail.EnablePO;
        copy7.POVendorID = detail.POVendorID;
        copy7.POVendorLocationID = detail.POVendorLocationID;
        copy7.TaxCategoryID = detail.TaxCategoryID;
        copy7.DiscPct = detail.DiscPct;
        copy7.CuryDiscAmt = detail.CuryDiscAmt;
        copy7.CuryBillableExtPrice = detail.CuryBillableExtPrice;
        FSSODet fssoDet6 = copy7;
        nullable1 = detail.POCreate;
        bool? nullable3 = nullable1.GetValueOrDefault() ? this.GetPOCreate((PXGraph) ((PXGraphExtension<TGraph>) this).Base, detail, fsSrvOrdTypeRow) : new bool?(false);
        fssoDet6.POCreate = nullable3;
        FSSODet fssoDet7;
        FSSODet dstObj = fssoDet7 = (FSSODet) cache.Update((object) copy7);
        cache.Current = (object) fssoDet7;
        SharedFunctions.CopyNotesAndFiles(linesCache, ((PXSelectBase) srvOrdGraph.ServiceOrderDetails).Cache, detail.sourceLine, (object) dstObj, header.CopyNotes, header.CopyFiles);
        FSSODet current = (FSSODet) cache.Current;
        this.OnAfterServiceOrderLineInsert(((PXSelectBase) srvOrdGraph.ServiceOrderDetails).Cache, current, linesCache, detail.sourceLine);
      }
    }
    FSServiceOrder current1 = ((PXSelectBase<FSServiceOrder>) srvOrdGraph.ServiceOrderRecords).Current;
    this.OnBeforeServiceOrderPersist(((PXSelectBase) srvOrdGraph.ServiceOrderRecords).Cache, current1, headerCache, header.sourceDocument);
    ((PXGraph) srvOrdGraph).Actions.PressSave();
    FSServiceOrder current2 = ((PXSelectBase<FSServiceOrder>) srvOrdGraph.ServiceOrderRecords).Current;
    if (createAppointment)
    {
      FSAppointment fsAppointment1 = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Current = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Insert(new FSAppointment()
      {
        SrvOrdType = current2.SrvOrdType
      });
      FSAppointment copy8 = (FSAppointment) ((PXSelectBase) apptGraph.AppointmentRecords).Cache.CreateCopy((object) fsAppointment1);
      copy8.SORefNbr = current2.RefNbr;
      FSAppointment fsAppointment2 = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Current = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Update(copy8);
      FSAppointment copy9 = (FSAppointment) ((PXSelectBase) apptGraph.AppointmentRecords).Cache.CreateCopy((object) fsAppointment2);
      copy9.ScheduledDateTimeBegin = header.ScheduledDateTimeBegin;
      bool? nullable4 = fsSrvOrdTypeRow.CopyNotesToAppoinment;
      if (nullable4.GetValueOrDefault())
        copy9.LongDescr = header.LongDescr;
      nullable4 = header.HandleManuallyScheduleTime;
      if (nullable4.GetValueOrDefault())
      {
        copy9.HandleManuallyScheduleTime = header.HandleManuallyScheduleTime;
        copy9.ScheduledDateTimeEnd = header.ScheduledDateTimeEnd;
      }
      FSAppointment fsAppointment3 = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Current = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Update(copy9);
      UDFHelper.CopyAttributes(headerCache, header.sourceDocument, ((PXSelectBase) apptGraph.AppointmentRecords).Cache, (object) fsAppointment3, (string) null);
      this.OnBeforeAppointmentPersist(((PXSelectBase) apptGraph.AppointmentRecords).Cache, fsAppointment3, headerCache, header.sourceDocument);
      ((PXGraph) apptGraph).Actions.PressSave();
      FSAppointment current3 = ((PXSelectBase<FSAppointment>) apptGraph.AppointmentRecords).Current;
    }
    return current2;
  }

  private bool? GetPOCreate(PXGraph graph, DBoxDetails dBoxDetail, FSSrvOrdType fsSrvOrdTypeRow)
  {
    return fsSrvOrdTypeRow.PostTo != "NA" && fsSrvOrdTypeRow.PostTo != "AR" ? new bool?(true) : new bool?(false);
  }

  public virtual void SetDBoxDefaults(string destinationDocument)
  {
    ((PXSelectBase<DBoxDocSettings>) this.DocumentSettings).Current.DestinationDocument = destinationDocument;
    this.PrepareDBoxDefaults();
  }

  public virtual void ShowDialogBoxAndProcess(bool requiredFieldsFilled)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DialogBoxSOApptCreation<TExtension, TGraph, TMain>.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new DialogBoxSOApptCreation<TExtension, TGraph, TMain>.\u003C\u003Ec__DisplayClass13_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.dBoxAnswer = ((PXSelectBase) this.DocumentSettings).View.Answer;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!requiredFieldsFilled || cDisplayClass130.dBoxAnswer != 1 && cDisplayClass130.dBoxAnswer != 6)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.processingGraph = GraphHelper.Clone<TGraph>(((PXGraphExtension<TGraph>) this).Base);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new PXToggleAsyncDelegate((object) cDisplayClass130, __methodptr(\u003CShowDialogBoxAndProcess\u003Eb__0)));
  }

  public virtual void PrepareFilterFields(DBoxHeader header, List<DBoxDetails> details)
  {
    this.PrepareHeaderAndDetails(header, details);
  }

  public virtual void OnBeforeServiceOrderPersist(
    PXCache cacheSrvOrd,
    FSServiceOrder srvOrd,
    PXCache sourceDocCache,
    object sourceDoc)
  {
  }

  public virtual void OnAfterServiceOrderLineInsert(
    PXCache cacheSrvOrdLine,
    FSSODet srvOrdLine,
    PXCache sourceLineCache,
    object sourceLine)
  {
  }

  public virtual void OnBeforeAppointmentPersist(
    PXCache cacheAppt,
    FSAppointment appt,
    PXCache sourceDocCache,
    object sourceDoc)
  {
  }

  public abstract void PrepareDBoxDefaults();

  public abstract void PrepareHeaderAndDetails(DBoxHeader header, List<DBoxDetails> details);

  public abstract void CreateDocument(
    ServiceOrderEntry srvOrdGraph,
    AppointmentEntry apptGraph,
    DBoxHeader header,
    List<DBoxDetails> details);
}
