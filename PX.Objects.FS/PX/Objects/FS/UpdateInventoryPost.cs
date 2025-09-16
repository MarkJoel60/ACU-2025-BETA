// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.UpdateInventoryPost
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class UpdateInventoryPost : PXGraph<UpdateInventoryPost>
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  [PXHidden]
  public PXSetup<FSRouteSetup> RouteSetupRecord;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<FSAppointment> Appointment;
  [PXHidden]
  public PXSelect<PX.Objects.AR.Customer> Customer;
  public PXFilter<UpdateInventoryFilter> Filter;
  public PXCancel<UpdateInventoryFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (UpdateInventoryFilter))]
  public PXFilteredProcessingJoin<FSAppointmentDet, UpdateInventoryFilter, InnerJoin<FSAppointment, On<FSAppointment.appointmentID, Equal<FSAppointmentDet.appointmentID>>, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.billCustomerID>>, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSAppointment.srvOrdType>>, LeftJoin<FSPostInfo, On<FSPostInfo.postID, Equal<FSAppointmentDet.postID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSGeoZonePostalCode, On<FSGeoZonePostalCode.postalCode, Equal<FSAddress.postalCode>>, LeftJoin<FSGeoZone, On<FSGeoZone.geoZoneID, Equal<FSGeoZonePostalCode.geoZoneID>>>>>>>>>>, Where<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointment.closed, Equal<True>, And<FSSrvOrdType.enableINPosting, Equal<True>, And2<Where<FSPostInfo.postID, IsNull, Or<FSPostInfo.iNPosted, Equal<False>>>, And2<Where<Current<UpdateInventoryFilter.cutOffDate>, IsNull, Or<FSAppointment.executionDate, LessEqual<Current<UpdateInventoryFilter.cutOffDate>>>>, And2<Where<Current<UpdateInventoryFilter.routeDocumentID>, IsNull, Or<FSAppointment.routeDocumentID, Equal<Current<UpdateInventoryFilter.routeDocumentID>>>>, And<Where<Current<UpdateInventoryFilter.appointmentID>, IsNull, Or<FSAppointment.appointmentID, Equal<Current<UpdateInventoryFilter.appointmentID>>>>>>>>>>>> Appointments;
  public PXAction<UpdateInventoryFilter> viewPostBatch;
  private PostInfoEntry graphPostInfoEntry;
  private InventoryPostBatchMaint graphUpdatePostBatchMaint;

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  protected virtual void Customer_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSAppointment.refNbr>))]
  protected virtual void FSAppointment_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr>))]
  protected virtual void FSAppointment_SORefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "")]
  public virtual IEnumerable ViewPostBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<FSAppointmentDet>) this.Appointments).Current != null)
    {
      FSAppointmentDet current = ((PXSelectBase<FSAppointmentDet>) this.Appointments).Current;
      if (!string.IsNullOrEmpty(current.Mem_BatchNbr))
      {
        ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Search<FSPostBatch.batchNbr>((object) current.Mem_BatchNbr, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) this.graphUpdatePostBatchMaint, (string) null);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  public UpdateInventoryPost()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    UpdateInventoryPost.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new UpdateInventoryPost.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.\u003C\u003E4__this = this;
    this.graphPostInfoEntry = PXGraph.CreateInstance<PostInfoEntry>();
    this.graphUpdatePostBatchMaint = PXGraph.CreateInstance<InventoryPostBatchMaint>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.filter = ((PXSelectBase<UpdateInventoryFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<FSAppointmentDet>) this.Appointments).SetProcessDelegate(new PXProcessingBase<FSAppointmentDet>.ProcessListDelegate((object) cDisplayClass150, __methodptr(\u003C\u002Ector\u003Eb__0)));
    OpenPeriodAttribute.SetValidatePeriod<UpdateInventoryFilter.finPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  /// <summary>
  /// Gets the information of the Appointment and AppointmentInventoryItem using as reference the [appointmentID] and [appointmentInventoryItemID].
  /// </summary>
  public virtual SharedClasses.AppointmentInventoryItemInfo GetAppointmentInventoryItemInfo(
    int? appointmentID,
    int? appDetID,
    int index)
  {
    return new SharedClasses.AppointmentInventoryItemInfo();
  }

  protected virtual void UpdateInventoryFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    UpdateInventoryFilter row = (UpdateInventoryFilter) e.Row;
    bool flag = string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<UpdateInventoryFilter.finPeriodID>(cache, (object) row));
    ((PXProcessing<FSAppointmentDet>) this.Appointments).SetProcessAllEnabled(flag);
    ((PXProcessing<FSAppointmentDet>) this.Appointments).SetProcessEnabled(flag);
  }

  /// <summary>
  /// Group the Appointment List [fsAppointmentRows] to determine how to post them.
  /// </summary>
  public virtual void CreateDocuments(
    List<FSAppointmentDet> fsAppointmentInventoryItemRows,
    UpdateInventoryFilter filter)
  {
    List<SharedClasses.AppointmentInventoryItemInfo> list = fsAppointmentInventoryItemRows.Select<FSAppointmentDet, SharedClasses.AppointmentInventoryItemInfo>((Func<FSAppointmentDet, int, SharedClasses.AppointmentInventoryItemInfo>) ((n, i) => this.GetAppointmentInventoryItemInfo(n.AppointmentID, n.AppDetID, i))).ToList<SharedClasses.AppointmentInventoryItemInfo>();
    if (((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current == null)
      return;
    List<SharedClasses.AppointmentInventoryItemGroup> listGroupToInvoice = !((PXSelectBase<FSRouteSetup>) this.RouteSetupRecord).Current.GroupINDocumentsByPostingProcess.GetValueOrDefault() ? list.GroupBy(u => new
    {
      AppointmentID = u.AppointmentID,
      ServiceType = u.ServiceType
    }, (key, group) => new
    {
      Key = key,
      Group = group.ToList<SharedClasses.AppointmentInventoryItemInfo>()
    }).Select(List => new SharedClasses.AppointmentInventoryItemGroup(List.Key.AppointmentID.Value, List.Key.ServiceType, List.Group)).OrderBy<SharedClasses.AppointmentInventoryItemGroup, int>((Func<SharedClasses.AppointmentInventoryItemGroup, int>) (List => List.Pivot)).ToList<SharedClasses.AppointmentInventoryItemGroup>() : list.GroupBy(u => new
    {
      ServiceType = u.ServiceType
    }, (key, group) => new
    {
      Key = key,
      Group = group.ToList<SharedClasses.AppointmentInventoryItemInfo>()
    }).Select(List => new SharedClasses.AppointmentInventoryItemGroup(List.Key.ServiceType == "P" ? 1 : 0, List.Key.ServiceType, List.Group)).OrderBy<SharedClasses.AppointmentInventoryItemGroup, int>((Func<SharedClasses.AppointmentInventoryItemGroup, int>) (List => List.Pivot)).ToList<SharedClasses.AppointmentInventoryItemGroup>();
    if (listGroupToInvoice == null || listGroupToInvoice.Count <= 0)
      return;
    this.CreateDocumentByGroup(fsAppointmentInventoryItemRows, listGroupToInvoice, filter);
  }

  /// <summary>
  /// Defines where the AppointmentInventoryItems are going to be posted, depending of the ServiceType (Pickup or Delivery).
  /// </summary>
  /// <param name="fsAppointmentInventoryItemRows"> Items to be posted (Original List in the screen).</param>
  /// <param name="listGroupToInvoice"> Items to be posted (Groups to be posted after grouping rules).</param>
  /// <param name="filter"> Header of the screen (Filters).</param>
  public virtual void CreateDocumentByGroup(
    List<FSAppointmentDet> fsAppointmentInventoryItemRows,
    List<SharedClasses.AppointmentInventoryItemGroup> listGroupToInvoice,
    UpdateInventoryFilter filter)
  {
    FSPostBatch fsPostBatch = this.CreateFSPostBatch(fsAppointmentInventoryItemRows.Count, "IN", filter.CutOffDate, filter.FinPeriodID, filter.DocumentDate);
    int documentsInIn = this.CreateDocumentsInIN(fsPostBatch, fsAppointmentInventoryItemRows, listGroupToInvoice, filter);
    if (documentsInIn > 0)
    {
      fsPostBatch.QtyDoc = new int?(documentsInIn);
      this.UpdateFSPostBatch(fsPostBatch);
    }
    else
      this.DeleteFSPostBatch(fsPostBatch);
  }

  /// <summary>
  /// Creates one or more documents in Inventory depending of the number of FSAppointmentInventoryItem in the list [fsAppointmentInventoryItemRows].
  /// </summary>
  public virtual int CreateDocumentsInIN(
    FSPostBatch fsPostBatchRow,
    List<FSAppointmentDet> fsAppointmentInventoryItemRows,
    List<SharedClasses.AppointmentInventoryItemGroup> listGroupToUpdateInInventory,
    UpdateInventoryFilter filter)
  {
    INReceiptEntry instance1 = PXGraph.CreateInstance<INReceiptEntry>();
    INIssueEntry instance2 = PXGraph.CreateInstance<INIssueEntry>();
    int documentsInIn = 0;
    foreach (SharedClasses.AppointmentInventoryItemGroup inventoryItemGroup in listGroupToUpdateInInventory)
    {
      string inRefNbr = (string) null;
      string inDocType = (string) null;
      foreach (SharedClasses.AppointmentInventoryItemInfo appointmentInventoryItem in inventoryItemGroup.AppointmentInventoryItems)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          try
          {
            if (inventoryItemGroup.ServiceType == "P")
              this.CreateDocumentReceipt(instance1, appointmentInventoryItem, fsAppointmentInventoryItemRows[appointmentInventoryItem.Index], filter.DocumentDate, filter.FinPeriodID, fsPostBatchRow, ref inRefNbr, ref inDocType);
            else if (inventoryItemGroup.ServiceType == "D")
              this.CreateDocumentIssue(instance2, appointmentInventoryItem, fsAppointmentInventoryItemRows[appointmentInventoryItem.Index], filter.DocumentDate, filter.FinPeriodID, fsPostBatchRow, ref inRefNbr, ref inDocType);
            else if (inventoryItemGroup.ServiceType == "N")
              PXProcessing<FSAppointmentDet>.SetError(appointmentInventoryItem.Index, "The record cannot be processed because No Items Related is selected in the Pickup/Deliver Items box on the Non-Stock Items (IN202000) form for the service.");
            PXProcessing<FSAppointmentDet>.SetInfo(appointmentInventoryItem.Index, "Record processed successfully.");
            ++documentsInIn;
            transactionScope.Complete();
          }
          catch (Exception ex)
          {
            Exception withContextMessage = ExceptionHelper.GetExceptionWithContextMessage(PXMessages.Localize("Could not process this record."), ex);
            PXProcessing<FSAppointmentDet>.SetError(appointmentInventoryItem.Index, withContextMessage);
            transactionScope.Dispose();
            ((PXGraph) instance1).Actions.PressCancel();
          }
        }
      }
    }
    return documentsInIn;
  }

  /// <summary>
  /// Creates a Posting Batch that will be used in every Posting Process.
  /// </summary>
  public virtual FSPostBatch CreateFSPostBatch(
    int qtyDoc,
    string postTo,
    DateTime? cutOffDate,
    string invoicePeriodID = null,
    DateTime? invoiceDate = null,
    int? billingCycleID = null,
    DateTime? upToDate = null)
  {
    FSPostBatch fsPostBatch1 = new FSPostBatch();
    fsPostBatch1.QtyDoc = new int?(qtyDoc);
    fsPostBatch1.BillingCycleID = billingCycleID;
    FSPostBatch fsPostBatch2 = fsPostBatch1;
    DateTime dateTime;
    DateTime? nullable1;
    if (!invoiceDate.HasValue)
    {
      nullable1 = invoiceDate;
    }
    else
    {
      int year = invoiceDate.Value.Year;
      dateTime = invoiceDate.Value;
      int month = dateTime.Month;
      dateTime = invoiceDate.Value;
      int day = dateTime.Day;
      nullable1 = new DateTime?(new DateTime(year, month, day, 0, 0, 0));
    }
    fsPostBatch2.InvoiceDate = nullable1;
    fsPostBatch1.PostTo = postTo;
    FSPostBatch fsPostBatch3 = fsPostBatch1;
    DateTime? nullable2;
    if (!upToDate.HasValue)
    {
      nullable2 = upToDate;
    }
    else
    {
      dateTime = upToDate.Value;
      int year = dateTime.Year;
      dateTime = upToDate.Value;
      int month = dateTime.Month;
      dateTime = upToDate.Value;
      int day = dateTime.Day;
      nullable2 = new DateTime?(new DateTime(year, month, day, 0, 0, 0));
    }
    fsPostBatch3.UpToDate = nullable2;
    FSPostBatch fsPostBatch4 = fsPostBatch1;
    dateTime = cutOffDate.Value;
    int year1 = dateTime.Year;
    dateTime = cutOffDate.Value;
    int month1 = dateTime.Month;
    dateTime = cutOffDate.Value;
    int day1 = dateTime.Day;
    DateTime? nullable3 = new DateTime?(new DateTime(year1, month1, day1, 0, 0, 0));
    fsPostBatch4.CutOffDate = nullable3;
    fsPostBatch1.FinPeriodID = invoicePeriodID;
    ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Insert(fsPostBatch1);
    ((PXAction) this.graphUpdatePostBatchMaint.Save).Press();
    return ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Current;
  }

  /// <summary>
  /// Update a Posting Batch that will be used in every Posting Process.
  /// </summary>
  public virtual FSPostBatch UpdateFSPostBatch(FSPostBatch fsPostBatchRow)
  {
    ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Search<FSPostBatch.batchID>((object) fsPostBatchRow.BatchID, Array.Empty<object>()));
    ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Update(fsPostBatchRow);
    ((PXAction) this.graphUpdatePostBatchMaint.Save).Press();
    return ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Current;
  }

  /// <summary>Deletes a Posting Batch record.</summary>
  public virtual void DeleteFSPostBatch(FSPostBatch fsPostBatchRow)
  {
    ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Search<FSPostBatch.batchID>((object) fsPostBatchRow.BatchID, Array.Empty<object>()));
    ((PXSelectBase<FSPostBatch>) this.graphUpdatePostBatchMaint.BatchRecords).Delete(fsPostBatchRow);
    ((PXAction) this.graphUpdatePostBatchMaint.Save).Press();
  }

  /// <summary>
  /// Creates a Receipt document using the parameters [fsAppointmentRow], [fsServiceOrderRow], [fsServiceOrderTypeRow] and its posting information.
  /// </summary>
  public virtual void CreateDocumentReceipt(
    INReceiptEntry graphINReceiptEntry,
    SharedClasses.AppointmentInventoryItemInfo appointmentInventoryItemInfoRow,
    FSAppointmentDet fsAppointmentInventoryItemRow,
    DateTime? documentDate,
    string documentPeriod,
    FSPostBatch fsPostBatchRow,
    ref string inRefNbr,
    ref string inDocType)
  {
    if (appointmentInventoryItemInfoRow == null)
      throw new PXException("There are no lines to be posted in the Appointment selection.");
    INRegister inRegister1;
    if (string.IsNullOrEmpty(inRefNbr))
    {
      INRegister inRegister2 = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Current = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Insert(new INRegister()
      {
        DocType = "R",
        TranDate = documentDate,
        FinPeriodID = documentPeriod,
        TranDesc = appointmentInventoryItemInfoRow.FSAppointmentRow.DocDesc,
        Hold = new bool?(false),
        BranchID = appointmentInventoryItemInfoRow.FSServiceOrderRow.BranchID
      });
      inRegister1 = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Update(inRegister2);
    }
    else
      inRegister1 = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Search<INRegister.refNbr>((object) inRefNbr, Array.Empty<object>()));
    INTran inTran = ((PXSelectBase<INTran>) graphINReceiptEntry.transactions).Current = ((PXSelectBase<INTran>) graphINReceiptEntry.transactions).Insert(new INTran()
    {
      TranType = "RCP"
    });
    ((PXSelectBase) graphINReceiptEntry.transactions).Cache.SetValueExt<INTran.inventoryID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.InventoryID);
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      ((PXSelectBase) graphINReceiptEntry.transactions).Cache.SetValueExt<INTran.subItemID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.SubItemID);
    ((PXSelectBase) graphINReceiptEntry.transactions).Cache.SetValueExt<INTran.siteID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.SiteID);
    ((PXSelectBase) graphINReceiptEntry.transactions).Cache.SetValueExt<INTran.qty>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.ActualQty);
    ((PXSelectBase) graphINReceiptEntry.transactions).Cache.SetValueExt<INTran.unitCost>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.UnitPrice);
    ((PXSelectBase) graphINReceiptEntry.transactions).Cache.SetValueExt<INTran.projectID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.ProjectID);
    ((PXSelectBase) graphINReceiptEntry.transactions).Cache.SetValueExt<INTran.taskID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.ProjectTaskID);
    FSxINTran extension = ((PXSelectBase) graphINReceiptEntry.transactions).Cache.GetExtension<FSxINTran>((object) ((PXSelectBase<INTran>) graphINReceiptEntry.transactions).Current);
    extension.SrvOrdType = appointmentInventoryItemInfoRow.FSAppointmentRow.SrvOrdType;
    extension.AppointmentRefNbr = appointmentInventoryItemInfoRow.FSAppointmentRow.RefNbr;
    extension.AppointmentLineNbr = appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.LineNbr;
    ((PXSelectBase<INTran>) graphINReceiptEntry.transactions).Update(inTran);
    ((PXAction) graphINReceiptEntry.Save).Press();
    if (string.IsNullOrEmpty(inRefNbr))
    {
      inRefNbr = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Current.RefNbr;
      inDocType = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Current.DocType;
    }
    this.UpdateReceiptPostInfo(graphINReceiptEntry, this.graphUpdatePostBatchMaint, this.graphPostInfoEntry, fsAppointmentInventoryItemRow, appointmentInventoryItemInfoRow, fsPostBatchRow);
  }

  /// <summary>
  /// Creates an Issue document using the parameters <c>fsAppointmentRow</c>, <c>fsServiceOrderRow</c>, <c>fsServiceOrderTypeRow</c> and its posting information.
  /// </summary>
  protected virtual void CreateDocumentIssue(
    INIssueEntry graphINIssueEntry,
    SharedClasses.AppointmentInventoryItemInfo appointmentInventoryItemInfoRow,
    FSAppointmentDet fsAppointmentInventoryItemRow,
    DateTime? documentDate,
    string documentPeriod,
    FSPostBatch fsPostBatchRow,
    ref string inRefNbr,
    ref string inDocType)
  {
    if (appointmentInventoryItemInfoRow == null)
      throw new PXException("There are no lines to be posted in the Appointment selection.");
    INRegister inRegister1;
    if (string.IsNullOrEmpty(inRefNbr))
    {
      INRegister inRegister2 = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Current = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Insert(new INRegister()
      {
        DocType = "I",
        TranDate = documentDate,
        FinPeriodID = documentPeriod,
        TranDesc = appointmentInventoryItemInfoRow.FSAppointmentRow.DocDesc,
        Hold = new bool?(false)
      });
      inRegister1 = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Update(inRegister2);
    }
    else
      inRegister1 = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Current = PXResultset<INRegister>.op_Implicit(((PXSelectBase<INRegister>) graphINIssueEntry.issue).Search<INRegister.refNbr>((object) inRefNbr, Array.Empty<object>()));
    INTran inTran = ((PXSelectBase<INTran>) graphINIssueEntry.transactions).Current = ((PXSelectBase<INTran>) graphINIssueEntry.transactions).Insert(new INTran()
    {
      TranType = "III"
    });
    ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.inventoryID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.InventoryID);
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.subItemID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.SubItemID);
    ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.siteID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.SiteID);
    ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.qty>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.ActualQty);
    ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.unitPrice>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.UnitPrice);
    ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.tranAmt>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.TranAmt);
    ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.projectID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.ProjectID);
    ((PXSelectBase) graphINIssueEntry.transactions).Cache.SetValueExt<INTran.taskID>((object) inTran, (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.ProjectTaskID);
    FSxINTran extension = ((PXSelectBase) graphINIssueEntry.transactions).Cache.GetExtension<FSxINTran>((object) ((PXSelectBase<INTran>) graphINIssueEntry.transactions).Current);
    extension.SrvOrdType = appointmentInventoryItemInfoRow.FSAppointmentRow.SrvOrdType;
    extension.AppointmentRefNbr = appointmentInventoryItemInfoRow.FSAppointmentRow.RefNbr;
    extension.ServiceOrderRefNbr = appointmentInventoryItemInfoRow.FSAppointmentRow.SORefNbr;
    extension.AppointmentLineNbr = appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.LineNbr;
    ((PXSelectBase<INTran>) graphINIssueEntry.transactions).Update(inTran);
    ((PXAction) graphINIssueEntry.Save).Press();
    if (string.IsNullOrEmpty(inRefNbr))
    {
      inRefNbr = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Current.RefNbr;
      inDocType = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Current.DocType;
    }
    this.UpdateIssuePostInfo(graphINIssueEntry, this.graphUpdatePostBatchMaint, this.graphPostInfoEntry, fsAppointmentInventoryItemRow, appointmentInventoryItemInfoRow, fsPostBatchRow);
  }

  /// <summary>
  /// Update the references in <c>FSPostInfo</c> and <c>FSPostDet</c> when the posting process of every AppointmentInventoryItem is complete in IN.
  /// </summary>
  public virtual void UpdateReceiptPostInfo(
    INReceiptEntry graphINReceiptEntry,
    InventoryPostBatchMaint graphInventoryPostBatchMaint,
    PostInfoEntry graphPostInfoEntry,
    FSAppointmentDet fsAppointmentInventoryItemRow,
    SharedClasses.AppointmentInventoryItemInfo appointmentInventoryItemInfoRow,
    FSPostBatch fsPostBatchRow)
  {
    fsPostBatchRow = ((PXSelectBase<FSPostBatch>) graphInventoryPostBatchMaint.BatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) graphInventoryPostBatchMaint.BatchRecords).Search<FSPostBatch.batchID>((object) fsPostBatchRow.BatchID, Array.Empty<object>()));
    FSPostInfo fsPostInfo1 = PXResultset<FSPostInfo>.op_Implicit(PXSelectBase<FSPostInfo, PXSelect<FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.PostID
    }));
    int? nullable;
    FSPostInfo fsPostInfo2;
    if (fsPostInfo1 != null)
    {
      nullable = fsPostInfo1.PostID;
      if (nullable.HasValue)
      {
        fsPostInfo2 = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current = PXResultset<FSPostInfo>.op_Implicit(((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Search<FSPostInfo.postID>((object) fsPostInfo1.PostID, Array.Empty<object>()));
        goto label_4;
      }
    }
    FSPostInfo fsPostInfo3 = new FSPostInfo();
    fsPostInfo2 = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Insert(fsPostInfo3);
label_4:
    fsPostInfo2.INPosted = new bool?(true);
    fsPostInfo2.INDocType = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Current.DocType;
    fsPostInfo2.INRefNbr = ((PXSelectBase<INRegister>) graphINReceiptEntry.receipt).Current.RefNbr;
    foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) graphINReceiptEntry.transactions).Select(Array.Empty<object>()))
    {
      INTran inTran = PXResult<INTran>.op_Implicit(pxResult);
      FSxINTran extension = ((PXSelectBase) graphINReceiptEntry.transactions).Cache.GetExtension<FSxINTran>((object) inTran);
      if (extension != null && appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.SrvOrdType == extension.SrvOrdType && appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.RefNbr == extension.AppointmentRefNbr)
      {
        nullable = appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.LineNbr;
        int? appointmentLineNbr = extension.AppointmentLineNbr;
        if (nullable.GetValueOrDefault() == appointmentLineNbr.GetValueOrDefault() & nullable.HasValue == appointmentLineNbr.HasValue)
        {
          fsPostInfo2.INLineNbr = inTran.LineNbr;
          break;
        }
      }
    }
    fsPostInfo2.AppointmentID = appointmentInventoryItemInfoRow.FSAppointmentRow.AppointmentID;
    fsPostInfo2.SOID = appointmentInventoryItemInfoRow.FSAppointmentRow.SOID;
    ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Update(fsPostInfo2);
    ((PXAction) graphPostInfoEntry.Save).Press();
    FSPostInfo current = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current;
    ((PXSelectBase<FSPostDet>) graphInventoryPostBatchMaint.BatchDetails).Insert(new FSPostDet()
    {
      PostID = current.PostID,
      INPosted = current.INPosted,
      INDocType = current.INDocType,
      INRefNbr = current.INRefNbr,
      INLineNbr = current.INLineNbr
    });
    ((PXAction) graphInventoryPostBatchMaint.Save).Press();
    fsAppointmentInventoryItemRow.Mem_BatchNbr = fsPostBatchRow.BatchNbr;
    PXUpdate<Set<FSAppointmentDet.postID, Required<FSAppointmentDet.postID>>, FSAppointmentDet, Where<FSAppointmentDet.appDetID, Equal<Required<FSAppointmentDet.appDetID>>>>.Update((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current.PostID,
      (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.AppDetID
    });
  }

  /// <summary>
  /// Update the references in <c>FSPostInfo</c> and <c>FSPostDet</c> when the posting process of every AppointmentInventoryItem is complete in IN.
  /// </summary>
  public virtual void UpdateIssuePostInfo(
    INIssueEntry graphINIssueEntry,
    InventoryPostBatchMaint graphInventoryPostBatchMaint,
    PostInfoEntry graphPostInfoEntry,
    FSAppointmentDet fsAppointmentInventoryItemRow,
    SharedClasses.AppointmentInventoryItemInfo appointmentInventoryItemInfoRow,
    FSPostBatch fsPostBatchRow)
  {
    fsPostBatchRow = ((PXSelectBase<FSPostBatch>) graphInventoryPostBatchMaint.BatchRecords).Current = PXResultset<FSPostBatch>.op_Implicit(((PXSelectBase<FSPostBatch>) graphInventoryPostBatchMaint.BatchRecords).Search<FSPostBatch.batchID>((object) fsPostBatchRow.BatchID, Array.Empty<object>()));
    FSPostInfo fsPostInfo1 = PXResultset<FSPostInfo>.op_Implicit(PXSelectBase<FSPostInfo, PXSelect<FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.PostID
    }));
    int? nullable;
    FSPostInfo fsPostInfo2;
    if (fsPostInfo1 != null)
    {
      nullable = fsPostInfo1.PostID;
      if (nullable.HasValue)
      {
        fsPostInfo2 = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current = PXResultset<FSPostInfo>.op_Implicit(((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Search<FSPostInfo.postID>((object) fsPostInfo1.PostID, Array.Empty<object>()));
        goto label_4;
      }
    }
    FSPostInfo fsPostInfo3 = new FSPostInfo();
    fsPostInfo2 = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Insert(fsPostInfo3);
label_4:
    fsPostInfo2.INPosted = new bool?(true);
    fsPostInfo2.INDocType = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Current.DocType;
    fsPostInfo2.INRefNbr = ((PXSelectBase<INRegister>) graphINIssueEntry.issue).Current.RefNbr;
    foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) graphINIssueEntry.transactions).Select(Array.Empty<object>()))
    {
      INTran inTran = PXResult<INTran>.op_Implicit(pxResult);
      FSxINTran extension = ((PXSelectBase) graphINIssueEntry.transactions).Cache.GetExtension<FSxINTran>((object) inTran);
      if (extension != null && appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.SrvOrdType == extension.SrvOrdType && appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.RefNbr == extension.AppointmentRefNbr)
      {
        nullable = appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.LineNbr;
        int? appointmentLineNbr = extension.AppointmentLineNbr;
        if (nullable.GetValueOrDefault() == appointmentLineNbr.GetValueOrDefault() & nullable.HasValue == appointmentLineNbr.HasValue)
        {
          fsPostInfo2.INLineNbr = inTran.LineNbr;
          break;
        }
      }
    }
    fsPostInfo2.AppointmentID = appointmentInventoryItemInfoRow.FSAppointmentRow.AppointmentID;
    fsPostInfo2.SOID = appointmentInventoryItemInfoRow.FSAppointmentRow.SOID;
    ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Update(fsPostInfo2);
    ((PXAction) graphPostInfoEntry.Save).Press();
    FSPostInfo current = ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current;
    ((PXSelectBase<FSPostDet>) graphInventoryPostBatchMaint.BatchDetails).Insert(new FSPostDet()
    {
      PostID = current.PostID,
      INPosted = current.INPosted,
      INDocType = current.INDocType,
      INRefNbr = current.INRefNbr,
      INLineNbr = current.INLineNbr
    });
    ((PXAction) graphInventoryPostBatchMaint.Save).Press();
    fsAppointmentInventoryItemRow.Mem_BatchNbr = fsPostBatchRow.BatchNbr;
    PXUpdate<Set<FSAppointmentDet.postID, Required<FSAppointmentDet.postID>>, FSAppointmentDet, Where<FSAppointmentDet.appDetID, Equal<Required<FSAppointmentDet.appDetID>>>>.Update((PXGraph) this, new object[2]
    {
      (object) ((PXSelectBase<FSPostInfo>) graphPostInfoEntry.PostInfoRecords).Current.PostID,
      (object) appointmentInventoryItemInfoRow.FSAppointmentInventoryItem.AppDetID
    });
  }
}
