// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_APInvoiceEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_APInvoiceEntry : FSPostingBase<APInvoiceEntry>
{
  [PXHidden]
  public PXSelect<FSServiceOrder> ServiceOrderRecords;
  [PXHidden]
  public PXSelect<FSAppointment> AppointmentRecords;
  [PXHidden]
  public PXFilter<CreateAPFilter> apFilter;
  public PXAction<PX.Objects.AP.APInvoice> openFSDocument;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual bool IsFSIntegrationEnabled()
  {
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current;
    return current != null && current.CreatedByScreenID.Substring(0, 2) == "FS";
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXSelectorAttribute), "DescriptionField", typeof (FSSrvOrdType.srvOrdType))]
  protected virtual void FSServiceOrder_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXSelectorAttribute), "DescriptionField", typeof (FSSrvOrdType.srvOrdType))]
  protected virtual void FSAppointment_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable OpenFSDocument(PXAdapter adapter)
  {
    APTran current = ((PXSelectBase<APTran>) this.Base.Transactions).Current;
    if (current != null)
    {
      FSxAPTran extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) current);
      if (extension != null)
      {
        object entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow(extension.RelatedDocNoteID, true);
        switch (entityRow)
        {
          case FSAppointment _:
            FSAppointment fsAppointment = (FSAppointment) entityRow;
            AppointmentEntry instance1 = PXGraph.CreateInstance<AppointmentEntry>();
            ((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance1.AppointmentRecords).Search<FSAppointment.refNbr>((object) fsAppointment.RefNbr, new object[1]
            {
              (object) fsAppointment.SrvOrdType
            }));
            PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
            ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException1;
          case FSServiceOrder _:
            FSServiceOrder fsServiceOrder = (FSServiceOrder) entityRow;
            ServiceOrderEntry instance2 = PXGraph.CreateInstance<ServiceOrderEntry>();
            ((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) instance2.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) fsServiceOrder.RefNbr, new object[1]
            {
              (object) fsServiceOrder.SrvOrdType
            }));
            PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException((PXGraph) instance2, (string) null);
            ((PXBaseRedirectException) requiredException2).Mode = (PXBaseRedirectException.WindowMode) 3;
            throw requiredException2;
        }
      }
    }
    return adapter.Get();
  }

  [PXOverride]
  public virtual IEnumerable ReverseInvoice(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseMethod)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.DocType == "ADR" && GraphHelper.RowCast<APTran>((IEnumerable) ((PXSelectBase<APTran>) this.Base.Transactions).Select(Array.Empty<object>())).Select<APTran, FSxAPTran>(new Func<APTran, FSxAPTran>(((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>)).Any<FSxAPTran>((Func<FSxAPTran, bool>) (_ => _.RelatedDocNoteID.HasValue || !string.IsNullOrEmpty(_.RelatedEntityType) || !string.IsNullOrEmpty(_.AppointmentRefNbr) || !string.IsNullOrEmpty(_.ServiceOrderRefNbr))))
      throw new PXException("A debit adjustment with a link to a service order or an appointment cannot be reversed automatically. Create a credit adjustment or a bill manually.");
    return baseMethod(adapter);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.AP.APInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.AP.APInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.AP.APInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.AP.APInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AP.APInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.AP.APInvoice> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base) || this.GetAPTranRecordsToProcess(e.Row, (PXDBOperation) 3).Where<APTran>((Func<APTran, bool>) (row =>
    {
      if (((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) row)?.RelatedEntityType == null)
        return false;
      FSxAPTran extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) row);
      return extension != null && extension.RelatedDocNoteID.HasValue;
    })).ToList<APTran>().Count<APTran>() <= 0 || ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Ask("At least one line in this document is associated with a field service document. Do you want to delete this document?", (MessageButtons) 1) == 1)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AP.APInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AP.APInvoice> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    PX.Objects.AP.APInvoice row = e.Row;
    this.ValidatePostBatchStatus(e.Operation, "AP", row.DocType, row.RefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AP.APInvoice> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    PX.Objects.AP.APInvoice row = e.Row;
    if (e.Operation == 3 && e.TranStatus == null)
      this.CleanPostingInfoLinkedToDoc((object) row);
    if (e.TranStatus != null || !(((PXGraph) this.Base).Accessinfo.ScreenID.Substring(0, 2) != "FS"))
      return;
    this.UpdateFSDocument(row, e.Operation);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<APTran, APTran.projectID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    CreateAPFilter current = ((PXSelectBase<CreateAPFilter>) this.apFilter).Current;
    if ((current != null ? (current.RelatedDocProjectID.HasValue ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APTran, APTran.projectID>, APTran, object>) e).NewValue = (object) ((PXSelectBase<CreateAPFilter>) this.apFilter).Current.RelatedDocProjectID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APTran, APTran.projectID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<APTran, APTran.taskID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    CreateAPFilter current = ((PXSelectBase<CreateAPFilter>) this.apFilter).Current;
    if ((current != null ? (current.RelatedDocProjectTaskID.HasValue ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APTran, APTran.taskID>, APTran, object>) e).NewValue = (object) ((PXSelectBase<CreateAPFilter>) this.apFilter).Current.RelatedDocProjectTaskID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APTran, APTran.taskID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APTran, APTran.costCodeID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    CreateAPFilter current = ((PXSelectBase<CreateAPFilter>) this.apFilter).Current;
    if ((current != null ? (current.RelatedDocCostCodeID.HasValue ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APTran, APTran.costCodeID>, APTran, object>) e).NewValue = (object) ((PXSelectBase<CreateAPFilter>) this.apFilter).Current.RelatedDocCostCodeID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<APTran, APTran.costCodeID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<APTran, APTran.qty> e)
  {
    if (e.Row != null && SharedFunctions.isFSSetupSet((PXGraph) this.Base) && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (object) e.Row, typeof (APTran.qty).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<APTran, APTran.inventoryID> e)
  {
    if (e.Row != null && SharedFunctions.isFSSetupSet((PXGraph) this.Base) && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (object) e.Row, typeof (APTran.inventoryID).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<APTran, APTran.uOM> e)
  {
    if (e.Row != null && SharedFunctions.isFSSetupSet((PXGraph) this.Base) && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (object) e.Row, typeof (APTran.uOM).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APTran, APTran.inventoryID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APTran, APTran.inventoryID>, APTran, object>) e).NewValue;
    int? inventoryId = e.Row.InventoryID;
    if (newValue.GetValueOrDefault() == inventoryId.GetValueOrDefault() & newValue.HasValue == inventoryId.HasValue)
      return;
    this.VerifyAPFieldCanBeUpdated<APTran.inventoryID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<APTran, APTran.inventoryID>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<APTran, APTran.qty> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APTran, APTran.qty>, APTran, object>) e).NewValue;
    Decimal? qty = e.Row.Qty;
    if (newValue.GetValueOrDefault() == qty.GetValueOrDefault() & newValue.HasValue == qty.HasValue)
      return;
    this.VerifyAPFieldCanBeUpdated<APTran.qty>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<APTran, APTran.qty>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<APTran, APTran.uOM> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base) || !((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APTran, APTran.uOM>, APTran, object>) e).NewValue != e.Row.UOM))
      return;
    this.VerifyAPFieldCanBeUpdated<APTran.uOM>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<APTran, APTran.uOM>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<APTran, APTran.taskID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APTran, APTran.taskID>, APTran, object>) e).NewValue;
    int? taskId = e.Row.TaskID;
    if (newValue.GetValueOrDefault() == taskId.GetValueOrDefault() & newValue.HasValue == taskId.HasValue)
      return;
    this.VerifyAPFieldCanBeUpdated<APTran.taskID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<APTran, APTran.taskID>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<APTran, APTran.costCodeID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<APTran, APTran.costCodeID>, APTran, object>) e).NewValue;
    int? costCodeId = e.Row.CostCodeID;
    if (newValue.GetValueOrDefault() == costCodeId.GetValueOrDefault() & newValue.HasValue == costCodeId.HasValue)
      return;
    this.VerifyAPFieldCanBeUpdated<APTran.costCodeID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<APTran, APTran.costCodeID>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<APTran, APTran.inventoryID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    this.UpdateAPLineFieldsFromFSDocument(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<APTran, APTran.inventoryID>>) e).Cache, e.Row, false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSxAPTran.relatedEntityType> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base) || !((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSxAPTran.relatedEntityType>, object, object>) e).OldValue != (string) e.NewValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSxAPTran.relatedEntityType>>) e).Cache.SetValueExt<FSxAPTran.relatedDocNoteID>(e.Row, (object) null);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FSxAPTran.relatedDocNoteID> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    APTran row = (APTran) e.Row;
    Guid? oldValue = (Guid?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSxAPTran.relatedDocNoteID>, object, object>) e).OldValue;
    Guid? newValue = (Guid?) e.NewValue;
    if ((oldValue.HasValue == newValue.HasValue ? (oldValue.HasValue ? (oldValue.GetValueOrDefault() != newValue.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 || e.NewValue == null)
      return;
    this.UpdateAPLineFieldsFromFSDocument(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSxAPTran.relatedDocNoteID>>) e).Cache, row, true);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<APTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<APTran> e)
  {
    bool isVisible = this.IsFSIntegrationEnabled();
    PXUIFieldAttribute.SetVisible<FSxAPTran.relatedEntityType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APTran>>) e).Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<FSxAPTran.relatedDocNoteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APTran>>) e).Cache, (object) null, isVisible);
    this.EnableDisableRelatedSMFields(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APTran>>) e).Cache, e.Row);
    this.SetExtensionVisibleInvisible<FSxAPTran>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APTran>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APTran>>) e).Args, isVisible, false);
  }

  protected virtual void _(PX.Data.Events.RowInserting<APTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<APTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<APTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<APTran> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    FSxAPTran extension = ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<APTran>>) e).Cache.GetExtension<FSxAPTran>((object) e.Row);
    if (extension == null || !e.ExternalCall || extension.RelatedEntityType == null || !extension.RelatedDocNoteID.HasValue || !(e.Row.CreatedByScreenID.Substring(0, 2) != "FS") || ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Ask("This line is associated with an appointment or service order. Do you want to delete the line?", (MessageButtons) 1) == 1)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APTran> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    APTran row = e.Row;
    if (e.ExternalCall && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (object) row, (string) null))
      throw new PXException("The line cannot be deleted because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APTran> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    this.ValidateLinkedFSDocFields(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<APTran>>) e).Cache, e.Row, (object) null, true);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<APTran> e)
  {
    if (e.Row == null)
      return;
    APTran row = e.Row;
    if (e.TranStatus != 2 || !this.IsInvoiceProcessRunning)
      return;
    MessageHelper.GetRowMessage(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<APTran>>) e).Cache, (IBqlTable) row, false, false);
  }

  public override List<MessageHelper.ErrorInfo> GetErrorInfo()
  {
    return MessageHelper.GetErrorInfo<APTran>(((PXSelectBase) this.Base.Document).Cache, (IBqlTable) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, (PXSelectBase<APTran>) this.Base.Transactions, typeof (APTran));
  }

  public virtual string GetLineDisplayHint(
    PXGraph graph,
    string lineRefNbr,
    string lineDescr,
    int? inventoryID)
  {
    return MessageHelper.GetLineDisplayHint(graph, lineRefNbr, lineDescr, inventoryID);
  }

  public override void CreateInvoice(
    PXGraph graphProcess,
    List<DocLineExt> docLines,
    short invtMult,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    OnDocumentHeaderInsertedDelegate onDocumentHeaderInserted,
    OnTransactionInsertedDelegate onTransactionInserted,
    PXQuickProcess.ActionFlow quickProcessFlow)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SM_APInvoiceEntry.\u003C\u003Ec__DisplayClass46_0 cDisplayClass460 = new SM_APInvoiceEntry.\u003C\u003Ec__DisplayClass46_0();
    if (docLines.Count == 0)
      return;
    bool? nullable1 = new bool?(false);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass460.fsServiceOrderRow = docLines[0].fsServiceOrder;
    FSSrvOrdType fsSrvOrdType1 = docLines[0].fsSrvOrdType;
    FSPostDoc fsPostDoc1 = docLines[0].fsPostDoc;
    FSAppointment fsAppointment = docLines[0].fsAppointment;
    // ISSUE: reference to a compiler-generated field
    if (this.GetVendorRow(graphProcess, cDisplayClass460.fsServiceOrderRow.BillCustomerID) == null)
      throw new PXException("This customer is not defined as a vendor. To extend the customer account to a customer and vendor account, select the Extend to Vendor action for this account on the form toolbar of the Customers (AR303000) form.");
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass460, __methodptr(\u003CCreateInvoice\u003Eb__0));
    try
    {
      ((PXGraph) this.Base).FieldDefaulting.AddHandler<PX.Objects.AP.APInvoice.branchID>(pxFieldDefaulting);
      PX.Objects.AP.APInvoice apInvoice1 = new PX.Objects.AP.APInvoice();
      if (invtMult >= (short) 0)
      {
        apInvoice1.DocType = "ADR";
        this.CheckAutoNumbering(((PXSelectBase<APSetup>) this.Base.APSetup).SelectSingle(Array.Empty<object>()).DebitAdjNumberingID);
      }
      else
      {
        apInvoice1.DocType = "INV";
        this.CheckAutoNumbering(((PXSelectBase<APSetup>) this.Base.APSetup).SelectSingle(Array.Empty<object>()).InvoiceNumberingID);
      }
      apInvoice1.DocDate = invoiceDate;
      apInvoice1.FinPeriodID = invoiceFinPeriodID;
      PX.Objects.AP.APInvoice copy = PXCache<PX.Objects.AP.APInvoice>.CreateCopy(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Insert(apInvoice1));
      bool? hold = copy.Hold;
      // ISSUE: reference to a compiler-generated field
      copy.VendorID = cDisplayClass460.fsServiceOrderRow.BillCustomerID;
      // ISSUE: reference to a compiler-generated field
      copy.VendorLocationID = cDisplayClass460.fsServiceOrderRow.BillLocationID;
      // ISSUE: reference to a compiler-generated field
      copy.CuryID = cDisplayClass460.fsServiceOrderRow.CuryID;
      // ISSUE: reference to a compiler-generated field
      copy.TaxZoneID = fsAppointment != null ? fsAppointment.TaxZoneID : cDisplayClass460.fsServiceOrderRow.TaxZoneID;
      // ISSUE: reference to a compiler-generated field
      copy.TaxCalcMode = fsAppointment != null ? fsAppointment.TaxCalcMode : cDisplayClass460.fsServiceOrderRow.TaxCalcMode;
      // ISSUE: reference to a compiler-generated field
      copy.SuppliedByVendorLocationID = cDisplayClass460.fsServiceOrderRow.BillLocationID;
      // ISSUE: reference to a compiler-generated field
      string str = this.GetTermsIDFromCustomerOrVendor(graphProcess, new int?(), cDisplayClass460.fsServiceOrderRow.BillCustomerID) ?? fsSrvOrdType1.DfltTermIDAP;
      copy.FinPeriodID = invoiceFinPeriodID;
      copy.TermsID = str;
      // ISSUE: reference to a compiler-generated field
      copy.DocDesc = cDisplayClass460.fsServiceOrderRow.DocDesc;
      copy.Hold = new bool?(true);
      PX.Objects.AP.APInvoice apInvoice2 = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Update(copy);
      apInvoice2.TaxCalcMode = "T";
      PX.Objects.AP.APInvoice apInvoice3 = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Update(apInvoice2);
      // ISSUE: reference to a compiler-generated field
      this.SetContactAndAddress((PXGraph) this.Base, cDisplayClass460.fsServiceOrderRow);
      if (onDocumentHeaderInserted != null)
        onDocumentHeaderInserted((PXGraph) this.Base, (IBqlTable) apInvoice3);
      foreach (DocLineExt docLine1 in docLines)
      {
        // ISSUE: reference to a compiler-generated field
        bool flag1 = fsAppointment == null ? docLine1.fsSODet == docLines[0].fsSODet || cDisplayClass460.fsServiceOrderRow.RefNbr != docLine1.fsServiceOrder.RefNbr : docLine1.fsAppointmentDet == docLines[0].fsAppointmentDet || fsAppointment.RefNbr != docLine1.fsAppointment.RefNbr;
        IDocLine docLine2 = docLine1.docLine;
        FSPostDoc fsPostDoc2 = docLine1.fsPostDoc;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass460.fsServiceOrderRow = docLine1.fsServiceOrder;
        FSSrvOrdType fsSrvOrdType2 = docLine1.fsSrvOrdType;
        fsAppointment = docLine1.fsAppointment;
        FSAppointmentDet fsAppointmentDet = docLine1.fsAppointmentDet;
        FSSODet fsSoDet = docLine1.fsSODet;
        APTran apTran1 = ((PXSelectBase<APTran>) this.Base.Transactions).Insert(new APTran());
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.branchID>((object) apTran1, (object) docLine2.BranchID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.inventoryID>((object) apTran1, (object) docLine2.InventoryID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.uOM>((object) apTran1, (object) docLine2.UOM);
        PMTask pmTask = docLine1.pmTask;
        if (pmTask != null && pmTask.Status == "F")
        {
          // ISSUE: reference to a compiler-generated field
          throw new PXException("The {1} line of the {0} document cannot be processed because the {2} project task has already been completed.", new object[3]
          {
            (object) cDisplayClass460.fsServiceOrderRow.RefNbr,
            (object) this.GetLineDisplayHint((PXGraph) this.Base, docLine2.LineRef, docLine2.TranDesc, docLine2.InventoryID),
            (object) pmTask.TaskCD
          });
        }
        if (docLine2.AcctID.HasValue)
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.accountID>((object) apTran1, (object) docLine2.AcctID);
        if (docLine2.SubID.HasValue)
        {
          try
          {
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.subID>((object) apTran1, (object) docLine2.SubID);
          }
          catch (PXException ex)
          {
            apTran1.SubID = new int?();
          }
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.SetCombinedSubID(graphProcess, ((PXSelectBase) this.Base.Transactions).Cache, (PX.Objects.AR.ARTran) null, apTran1, (PX.Objects.SO.SOLine) null, fsSrvOrdType2, apTran1.BranchID, apTran1.InventoryID, apInvoice3.VendorLocationID, cDisplayClass460.fsServiceOrderRow.BranchLocationID, cDisplayClass460.fsServiceOrderRow.SalesPersonID, docLine2.IsService);
        }
        if (docLine2.ProjectID.HasValue && docLine2.ProjectTaskID.HasValue)
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.taskID>((object) apTran1, (object) docLine2.ProjectTaskID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.qty>((object) apTran1, (object) docLine2.GetQty(FieldType.BillableField));
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.tranDesc>((object) apTran1, (object) docLine2.TranDesc);
        APTran apTran2 = ((PXSelectBase<APTran>) this.Base.Transactions).Update(apTran1);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.manualPrice>((object) apTran2, (object) docLine2.ManualPrice);
        PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
        APTran data = apTran2;
        bool? isFree = docLine2.IsFree;
        bool flag2 = false;
        Decimal? nullable2;
        if (!(isFree.GetValueOrDefault() == flag2 & isFree.HasValue))
        {
          nullable2 = new Decimal?(0M);
        }
        else
        {
          Decimal? curyUnitPrice = docLine2.CuryUnitPrice;
          Decimal num = (Decimal) invtMult;
          nullable2 = curyUnitPrice.HasValue ? new Decimal?(curyUnitPrice.GetValueOrDefault() * num) : new Decimal?();
        }
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> newValue = (ValueType) nullable2;
        cache.SetValueExtIfDifferent<APTran.curyUnitCost>((object) data, (object) newValue);
        if (docLine2.ProjectID.HasValue)
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.projectID>((object) apTran2, (object) docLine2.ProjectID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.taxCategoryID>((object) apTran2, (object) docLine2.TaxCategoryID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.costCodeID>((object) apTran2, (object) docLine2.CostCodeID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<APTran.discPct>((object) apTran2, (object) docLine2.DiscPct);
        FSxAPTran extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) apTran2);
        // ISSUE: reference to a compiler-generated field
        extension.SrvOrdType = cDisplayClass460.fsServiceOrderRow.SrvOrdType;
        // ISSUE: reference to a compiler-generated field
        extension.ServiceOrderRefNbr = cDisplayClass460.fsServiceOrderRow.RefNbr;
        extension.AppointmentRefNbr = fsAppointment?.RefNbr;
        extension.ServiceOrderLineNbr = (int?) fsSoDet?.LineNbr;
        extension.AppointmentLineNbr = (int?) fsAppointmentDet?.LineNbr;
        if (docLine2.BillingBy == "AP")
        {
          extension.RelatedEntityType = "PX.Objects.FS.FSAppointment";
          extension.RelatedDocNoteID = fsAppointment.NoteID;
        }
        else if (docLine2.BillingBy == "SO")
        {
          extension.RelatedEntityType = "PX.Objects.FS.FSServiceOrder";
          // ISSUE: reference to a compiler-generated field
          extension.RelatedDocNoteID = cDisplayClass460.fsServiceOrderRow.NoteID;
        }
        extension.Mem_PreviousPostID = docLine2.PostID;
        extension.Mem_TableSource = docLine2.SourceTable;
        if (flag1)
        {
          if (fsAppointment == null)
          {
            // ISSUE: reference to a compiler-generated field
            SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSServiceOrder)], ((PXSelectBase) this.Base.Document).Cache, (object) cDisplayClass460.fsServiceOrderRow, (object) apInvoice3, fsSrvOrdType2.CopyNotesToInvoice, fsSrvOrdType2.CopyAttachmentsToInvoice);
          }
          else
            SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSAppointment)], ((PXSelectBase) this.Base.Document).Cache, (object) fsAppointment, (object) apInvoice3, fsSrvOrdType2.CopyNotesToInvoice, fsSrvOrdType2.CopyAttachmentsToInvoice);
        }
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) this.Base.Transactions).Cache, (object) apTran2, docLine2, fsSrvOrdType2);
        APTran row;
        fsPostDoc2.DocLineRef = (object) (row = ((PXSelectBase<APTran>) this.Base.Transactions).Update(apTran2));
        if (onTransactionInserted != null)
          onTransactionInserted((PXGraph) this.Base, (IBqlTable) row);
      }
      PX.Objects.AP.APInvoice data1 = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Update(apInvoice3);
      if (((PXSelectBase<APSetup>) this.Base.APSetup).Current.RequireControlTotal.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AP.APInvoice.curyOrigDocAmt>((object) data1, (object) data1.CuryDocBal);
      if (!hold.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AP.APInvoice.hold>((object) data1, (object) false);
      ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Update(data1);
    }
    finally
    {
      ((PXGraph) this.Base).FieldDefaulting.RemoveHandler<PX.Objects.AP.APInvoice.branchID>(pxFieldDefaulting);
    }
  }

  public override FSCreatedDoc PressSave(
    int batchID,
    List<DocLineExt> docLines,
    BeforeSaveDelegate beforeSave)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current == null)
      throw new SharedClasses.TransactionScopeException();
    if (beforeSave != null)
      beforeSave((PXGraph) this.Base);
    APInvoiceEntryExternalTax extension = ((PXGraph) this.Base).GetExtension<APInvoiceEntryExternalTax>();
    if (extension != null)
      extension.SkipTaxCalcAndSave();
    else
      ((PXAction) this.Base.Save).Press();
    string docType = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.DocType;
    string refNbr = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.RefNbr;
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) docType,
      (object) refNbr
    }));
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current;
    return new FSCreatedDoc()
    {
      BatchID = new int?(batchID),
      PostTo = "AP",
      CreatedDocType = current.DocType,
      CreatedRefNbr = current.RefNbr
    };
  }

  public override void Clear() => ((PXGraph) this.Base).Clear((PXClearOption) 3);

  public override PXGraph GetGraph() => (PXGraph) this.Base;

  public override void DeleteDocument(FSCreatedDoc fsCreatedDocRow)
  {
    ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) fsCreatedDocRow.CreatedRefNbr, new object[1]
    {
      (object) fsCreatedDocRow.CreatedDocType
    }));
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current == null || !(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.RefNbr == fsCreatedDocRow.CreatedRefNbr) || !(((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current.DocType == fsCreatedDocRow.CreatedDocType))
      return;
    ((PXAction) this.Base.Delete).Press();
  }

  public override void CleanPostInfo(PXGraph cleanerGraph, FSPostDet fsPostDetRow)
  {
    PXUpdate<Set<FSPostInfo.aPLineNbr, Null, Set<FSPostInfo.apRefNbr, Null, Set<FSPostInfo.apDocType, Null, Set<FSPostInfo.aPPosted, False>>>>, FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>, And<FSPostInfo.aPPosted, Equal<True>>>>.Update(cleanerGraph, new object[1]
    {
      (object) fsPostDetRow.PostID
    });
  }

  public virtual bool IsLineCreatedFromAppSO(
    PXGraph cleanerGraph,
    object document,
    object lineDoc,
    string fieldName)
  {
    if (document == null || lineDoc == null || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS300100" || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS300200" || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS500600" || ((PXGraph) this.Base).Accessinfo.ScreenID.Replace(".", "") == "FS500100")
      return false;
    string refNbr = ((APRegister) document).RefNbr;
    string docType = ((APRegister) document).DocType;
    int? lineNbr = ((APTran) lineDoc).LineNbr;
    return ((IQueryable<PXResult<FSPostInfo>>) PXSelectBase<FSPostInfo, PXSelect<FSPostInfo, Where<FSPostInfo.apRefNbr, Equal<Required<FSPostInfo.apRefNbr>>, And<FSPostInfo.apDocType, Equal<Required<FSPostInfo.apDocType>>, And<FSPostInfo.aPLineNbr, Equal<Required<FSPostInfo.aPLineNbr>>, And<FSPostInfo.aPPosted, Equal<True>>>>>>.Config>.Select(cleanerGraph, new object[3]
    {
      (object) refNbr,
      (object) docType,
      (object) lineNbr
    })).Count<PXResult<FSPostInfo>>() > 0;
  }

  public virtual PX.Objects.AP.Vendor GetVendorRow(PXGraph graph, int? vendorID)
  {
    if (!vendorID.HasValue)
      return (PX.Objects.AP.Vendor) null;
    return PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) vendorID
    }));
  }

  public virtual void EnableDisableRelatedSMFields(PXCache cache, APTran row)
  {
    if (row == null)
      return;
    bool flag1 = false;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<APTran.inventoryID>(cache, (object) row);
    int num;
    if (inventoryItem == null)
    {
      num = 0;
    }
    else
    {
      bool? stkItem = inventoryItem.StkItem;
      bool flag2 = false;
      num = stkItem.GetValueOrDefault() == flag2 & stkItem.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      cache.GetExtension<FSxAPTran>((object) row);
      bool flag3 = row.CreatedByScreenID.Substring(0, 2) == "FS";
      flag1 = row.InventoryID.HasValue && !flag3 && (row.TranType == "INV" || row.TranType == "ADR" || row.TranType == "ACR") && row.PONbr == null;
    }
    PXUIFieldAttribute.SetEnabled<FSxAPTran.relatedEntityType>(cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<FSxAPTran.relatedDocNoteID>(cache, (object) row, flag1);
  }

  public virtual void UpdateAPLineFieldsFromFSDocument(PXCache cache, APTran row, bool runDefaults)
  {
    if (row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<APTran.inventoryID>(cache, (object) row);
    int num;
    if (inventoryItem == null)
    {
      num = 0;
    }
    else
    {
      bool? stkItem = inventoryItem.StkItem;
      bool flag = false;
      num = stkItem.GetValueOrDefault() == flag & stkItem.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    FSxAPTran extension = cache.GetExtension<FSxAPTran>((object) row);
    object entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow((Guid?) extension?.RelatedDocNoteID, true);
    if (entityRow == null || !(row.CreatedByScreenID?.Substring(0, 2) != "FS") || !runDefaults)
      return;
    string str = string.Empty;
    if (extension.RelatedEntityType == "PX.Objects.FS.FSAppointment")
    {
      FSAppointment fsAppointment = (FSAppointment) entityRow;
      cache.SetValueExt<APTran.branchID>((object) row, (object) fsAppointment.BranchID);
      cache.SetValueExt<APTran.projectID>((object) row, (object) fsAppointment.ProjectID);
      cache.SetValueExt<APTran.taskID>((object) row, (object) fsAppointment.DfltProjectTaskID);
      str = fsAppointment.SrvOrdType;
    }
    else if (extension.RelatedEntityType == "PX.Objects.FS.FSServiceOrder")
    {
      FSServiceOrder fsServiceOrder = (FSServiceOrder) entityRow;
      cache.SetValueExt<APTran.branchID>((object) row, (object) fsServiceOrder.BranchID);
      cache.SetValueExt<APTran.projectID>((object) row, (object) fsServiceOrder.ProjectID);
      cache.SetValueExt<APTran.taskID>((object) row, (object) fsServiceOrder.DfltProjectTaskID);
      str = fsServiceOrder.SrvOrdType;
    }
    if (ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    FSSrvOrdType fsSrvOrdType = PXResultset<FSSrvOrdType>.op_Implicit(PXSelectBase<FSSrvOrdType, PXSelect<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) str
    }));
    cache.SetValueExt<APTran.costCodeID>((object) row, (object) (int?) fsSrvOrdType?.DfltCostCodeID);
  }

  public virtual void UpdateFSDocument(PX.Objects.AP.APInvoice apInvoice, PXDBOperation operation)
  {
    PXGraph graphHelper = (PXGraph) null;
    Guid? lastDocNoteID = new Guid?();
    foreach (APTran row in this.GetAPTranRecordsToProcess(apInvoice, operation))
    {
      PXEntryStatus status = ((PXSelectBase) this.Base.Transactions).Cache.GetStatus((object) row);
      if (status == 2 || status == 1 || status == 3)
      {
        int? nullable1 = row.InventoryID;
        if (!nullable1.HasValue)
        {
          nullable1 = (int?) ((PXSelectBase) this.Base.Transactions).Cache.GetValueOriginal<APTran.inventoryID>((object) row);
          int? inventoryId = row.InventoryID;
          if (nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue)
            continue;
        }
        FSxAPTran extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) row);
        if (row.CreatedByScreenID.Substring(0, 2) != "FS")
        {
          Guid? valueOriginal1 = (Guid?) ((PXSelectBase) this.Base.Transactions).Cache.GetValueOriginal<FSxAPTran.relatedDocNoteID>((object) row);
          string valueOriginal2 = (string) ((PXSelectBase) this.Base.Transactions).Cache.GetValueOriginal<FSxAPTran.relatedEntityType>((object) row);
          Guid? relatedDocNoteId;
          if (valueOriginal1.HasValue)
          {
            Guid? nullable2 = valueOriginal1;
            relatedDocNoteId = extension.RelatedDocNoteID;
            if ((nullable2.HasValue == relatedDocNoteId.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != relatedDocNoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
              this.InsertUpdateDeleteFSDocumentLine(apInvoice, row, valueOriginal2, valueOriginal1, (PXEntryStatus) 3, ref lastDocNoteID, ref graphHelper);
          }
          relatedDocNoteId = extension.RelatedDocNoteID;
          if (relatedDocNoteId.HasValue)
            this.InsertUpdateDeleteFSDocumentLine(apInvoice, row, extension.RelatedEntityType, extension.RelatedDocNoteID, status, ref lastDocNoteID, ref graphHelper);
        }
      }
    }
    this.SaveFSGraphHelper(graphHelper);
  }

  public virtual List<APTran> GetAPTranRecordsToProcess(
    PX.Objects.AP.APInvoice apInvoice,
    PXDBOperation operation)
  {
    List<APTran> list;
    if (operation == 2)
      list = GraphHelper.RowCast<APTran>(((PXSelectBase) this.Base.Transactions).Cache.Inserted).Where<APTran>((Func<APTran, bool>) (row => row.CreatedByScreenID.Substring(0, 2) != "FS")).OrderBy<APTran, string>((Func<APTran, string>) (row => ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) row)?.RelatedEntityType)).ThenBy<APTran, Guid?>((Func<APTran, Guid?>) (row => ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) row)?.RelatedDocNoteID)).ToList<APTran>();
    else
      list = GraphHelper.RowCast<APTran>((IEnumerable) PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<APTran.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>, OrderBy<Asc<FSxAPTran.relatedEntityType, Asc<FSxAPTran.relatedDocNoteID>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) apInvoice.DocType,
        (object) apInvoice.RefNbr
      })).Where<APTran>((Func<APTran, bool>) (x => !x.CreatedByScreenID.Contains("FS"))).ToList<APTran>();
    List<APTran> tranRecordsToDelete = this.GetAPTranRecordsToDelete();
    if (tranRecordsToDelete.Count > 0)
      list = list.Concat<APTran>((IEnumerable<APTran>) tranRecordsToDelete).ToList<APTran>();
    return list;
  }

  public virtual List<APTran> GetAPTranRecordsToDelete()
  {
    return GraphHelper.RowCast<APTran>(((PXSelectBase) this.Base.Transactions).Cache.Deleted).Where<APTran>((Func<APTran, bool>) (row => row.CreatedByScreenID.Substring(0, 2) != "FS")).OrderBy<APTran, string>((Func<APTran, string>) (row => ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) row)?.RelatedEntityType)).ThenBy<APTran, Guid?>((Func<APTran, Guid?>) (row => ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxAPTran>((object) row)?.RelatedDocNoteID)).ToList<APTran>();
  }

  public virtual void SaveFSGraphHelper(PXGraph graphHelper)
  {
    if (graphHelper == null || !graphHelper.IsDirty)
      return;
    graphHelper.GetSaveAction().Press();
    this.UpdateFSCacheInAPDoc(graphHelper);
  }

  public virtual void UpdateFSCacheInAPDoc(PXGraph graphHelper)
  {
    switch (graphHelper)
    {
      case AppointmentEntry _:
        AppointmentEntry appointmentEntry = (AppointmentEntry) graphHelper;
        if (((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Current == null || ((PXGraph) this.Base).Caches[typeof (FSAppointment)].GetStatus((object) ((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Current) != 1)
          break;
        ((PXGraph) this.Base).Caches[typeof (FSAppointment)].Update((object) ((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Current);
        ((PXGraph) this.Base).SelectTimeStamp();
        break;
      case ServiceOrderEntry _:
        ServiceOrderEntry serviceOrderEntry = (ServiceOrderEntry) graphHelper;
        if (((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current == null || ((PXGraph) this.Base).Caches[typeof (FSServiceOrder)].GetStatus((object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current) != 1)
          break;
        ((PXGraph) this.Base).Caches[typeof (FSServiceOrder)].Update((object) ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current);
        ((PXGraph) this.Base).SelectTimeStamp();
        break;
    }
  }

  public virtual void InsertUpdateDeleteFSDocumentLine(
    PX.Objects.AP.APInvoice apInvoiceRow,
    APTran row,
    string relatedEntityType,
    Guid? relatedDocNoteID,
    PXEntryStatus apTranStatus,
    ref Guid? lastDocNoteID,
    ref PXGraph graphHelper)
  {
    object entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow(relatedDocNoteID, true);
    if (entityRow == null)
      return;
    if (relatedEntityType == "PX.Objects.FS.FSAppointment" && entityRow is FSAppointment)
    {
      this.InsertUpdateDeleteAppointmentLine(apInvoiceRow, row, relatedDocNoteID, apTranStatus, (FSAppointment) entityRow, ref graphHelper, ref lastDocNoteID);
    }
    else
    {
      if (!(relatedEntityType == "PX.Objects.FS.FSServiceOrder") || !(entityRow is FSServiceOrder))
        return;
      this.InsertUpdateDeleteServiceOrderLine(apInvoiceRow, row, relatedDocNoteID, apTranStatus, (FSServiceOrder) entityRow, ref graphHelper, ref lastDocNoteID);
    }
  }

  public virtual void InsertUpdateDeleteServiceOrderLine(
    PX.Objects.AP.APInvoice apInvoiceRow,
    APTran row,
    Guid? relatedDocNoteID,
    PXEntryStatus apTranStatus,
    FSServiceOrder serviceOrder,
    ref PXGraph graphHelper,
    ref Guid? lastDocNoteID)
  {
    if (graphHelper == null || graphHelper is AppointmentEntry)
    {
      this.SaveFSGraphHelper(graphHelper);
      graphHelper = (PXGraph) PXGraph.CreateInstance<ServiceOrderEntry>();
    }
    ServiceOrderEntry serviceOrderEntry = (ServiceOrderEntry) graphHelper;
    if (lastDocNoteID.HasValue)
    {
      Guid? nullable1 = lastDocNoteID;
      Guid? nullable2 = relatedDocNoteID;
      if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        goto label_7;
    }
    if (graphHelper.IsDirty)
    {
      this.SaveFSGraphHelper(graphHelper);
      ((PXGraph) serviceOrderEntry).Clear((PXClearOption) 3);
    }
    ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current = PXResultset<FSServiceOrder>.op_Implicit(((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Search<FSServiceOrder.refNbr>((object) serviceOrder.RefNbr, new object[1]
    {
      (object) serviceOrder.SrvOrdType
    }));
label_7:
    if (apTranStatus == 3 || this.ValidateLinkedFSDocFields(((PXSelectBase) this.Base.Transactions).Cache, row, (object) ((PXSelectBase<FSSODet>) serviceOrderEntry.ServiceOrderDetails).Current, false))
      this.InsertUpdateDeleteDocDetail(((PXSelectBase) serviceOrderEntry.ServiceOrderDetails).Cache, (IEnumerable<IFSSODetBase>) GraphHelper.RowCast<FSSODet>((IEnumerable) ((PXSelectBase<FSSODet>) serviceOrderEntry.ServiceOrderDetails).Select(Array.Empty<object>())), apInvoiceRow, row, apTranStatus, ((PXSelectBase<FSServiceOrder>) serviceOrderEntry.ServiceOrderRecords).Current.IsBilledOrClosed);
    lastDocNoteID = serviceOrder.NoteID;
  }

  public virtual void InsertUpdateDeleteAppointmentLine(
    PX.Objects.AP.APInvoice apInvoiceRow,
    APTran row,
    Guid? relatedDocNoteID,
    PXEntryStatus apTranStatus,
    FSAppointment appointment,
    ref PXGraph graphHelper,
    ref Guid? lastDocNoteID)
  {
    if (graphHelper == null || graphHelper is ServiceOrderEntry)
    {
      this.SaveFSGraphHelper(graphHelper);
      graphHelper = (PXGraph) PXGraph.CreateInstance<AppointmentEntry>();
    }
    AppointmentEntry appointmentEntry = (AppointmentEntry) graphHelper;
    if (lastDocNoteID.HasValue)
    {
      Guid? nullable1 = lastDocNoteID;
      Guid? nullable2 = relatedDocNoteID;
      if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        goto label_7;
    }
    if (graphHelper.IsDirty)
    {
      this.SaveFSGraphHelper(graphHelper);
      ((PXGraph) appointmentEntry).Clear((PXClearOption) 3);
    }
    ((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Search<FSAppointment.refNbr>((object) appointment.RefNbr, new object[1]
    {
      (object) appointment.SrvOrdType
    }));
label_7:
    if (apTranStatus == 3 || this.ValidateLinkedFSDocFields(((PXSelectBase) this.Base.Transactions).Cache, row, (object) ((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Current, false))
      this.InsertUpdateDeleteDocDetail(((PXSelectBase) appointmentEntry.AppointmentDetails).Cache, (IEnumerable<IFSSODetBase>) GraphHelper.RowCast<FSAppointmentDet>((IEnumerable) ((PXSelectBase<FSAppointmentDet>) appointmentEntry.AppointmentDetails).Select(Array.Empty<object>())), apInvoiceRow, row, apTranStatus, ((PXSelectBase<FSAppointment>) appointmentEntry.AppointmentRecords).Current.IsBilledOrClosed);
    lastDocNoteID = appointment.NoteID;
  }

  public virtual bool ValidateLinkedFSDocFields(
    PXCache apTranCache,
    APTran row,
    object fsDoc,
    bool throwError)
  {
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    FSxAPTran fsxApTran = (FSxAPTran) null;
    if (fsDoc == null)
    {
      fsxApTran = apTranCache.GetExtension<FSxAPTran>((object) row);
      PXEntryStatus status = apTranCache.GetStatus((object) row);
      if ((status == 2 || status == 1) && fsxApTran != null && fsxApTran.RelatedDocNoteID.HasValue)
        fsDoc = new EntityHelper((PXGraph) this.Base).GetEntityRow(fsxApTran.RelatedDocNoteID, true);
    }
    if (fsDoc != null)
    {
      int? nullable = row.InventoryID;
      if (!nullable.HasValue)
      {
        propertyException = new PXSetPropertyException("The document cannot be added because the inventory ID is empty. Select a non-stock item or a service in the document's Inventory ID box.", (PXErrorLevel) 4);
      }
      else
      {
        nullable = row.InventoryID;
        if (nullable.HasValue)
        {
          PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<APTran.inventoryID>(apTranCache, (object) row);
          if ((inventoryItem != null ? (inventoryItem.StkItem.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            propertyException = new PXSetPropertyException("The document cannot be added because the inventory ID corresponds to a stock item. Select a non-stock item or a service in the document's Inventory ID box.", (PXErrorLevel) 4);
        }
      }
      if (fsDoc is FSAppointment)
      {
        FSAppointment fsAppointment = (FSAppointment) fsDoc;
        nullable = fsAppointment.ProjectID;
        int? projectId = row.ProjectID;
        if (!(nullable.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable.HasValue == projectId.HasValue))
          propertyException = new PXSetPropertyException("The document cannot be assigned to {0} because its project {1} differs from the {0} project {2}.", new object[4]
          {
            (object) "AP Bill",
            (object) PMProject.PK.Find((PXGraph) this.Base, fsAppointment.ProjectID)?.ContractCD,
            apTranCache.GetValueExt<APTran.projectID>((object) row),
            (object) (PXErrorLevel) 4
          });
      }
      else if (fsDoc is FSServiceOrder)
      {
        FSServiceOrder fsServiceOrder = (FSServiceOrder) fsDoc;
        int? projectId = fsServiceOrder.ProjectID;
        nullable = row.ProjectID;
        if (!(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue))
          propertyException = new PXSetPropertyException("The document cannot be assigned to {0} because its project {1} differs from the {0} project {2}.", new object[4]
          {
            (object) "AP Bill",
            (object) PMProject.PK.Find((PXGraph) this.Base, fsServiceOrder.ProjectID)?.ContractCD,
            apTranCache.GetValueExt<APTran.projectID>((object) row),
            (object) (PXErrorLevel) 4
          });
      }
    }
    if (throwError && propertyException != null)
      apTranCache.RaiseExceptionHandling<FSxAPTran.relatedDocNoteID>((object) row, (object) fsxApTran.RelatedDocNoteID, (Exception) propertyException);
    return propertyException == null;
  }

  public virtual void InsertUpdateDeleteDocDetail(
    PXCache cache,
    IEnumerable<IFSSODetBase> rowList,
    PX.Objects.AP.APInvoice apInvoiceRow,
    APTran apTranRow,
    PXEntryStatus apTranStatus,
    bool IsDocBilledOrClosed)
  {
    if (apInvoiceRow == null || apTranRow == null)
      return;
    IFSSODetBase fssoDetBase1 = (IFSSODetBase) null;
    List<IFSSODetBase> list = rowList.Where<IFSSODetBase>((Func<IFSSODetBase, bool>) (det =>
    {
      if (!(det.LinkedEntityType == "AP") || !(det.LinkedDocType?.Trim() == apTranRow.TranType) || !(det.LinkedDocRefNbr?.Trim() == apTranRow.RefNbr.Trim()))
        return false;
      int? linkedLineNbr = det.LinkedLineNbr;
      int? lineNbr = apTranRow.LineNbr;
      return linkedLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & linkedLineNbr.HasValue == lineNbr.HasValue;
    })).ToList<IFSSODetBase>();
    IFSSODetBase fssoDetBase2 = (IFSSODetBase) null;
    int? nullable1;
    int? nullable2;
    if (list.Count > 0)
    {
      fssoDetBase1 = list.First<IFSSODetBase>();
      nullable1 = fssoDetBase1.InventoryID;
      nullable2 = apTranRow.InventoryID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) || apTranStatus == 3)
      {
        fssoDetBase2 = fssoDetBase1;
        fssoDetBase1 = (IFSSODetBase) null;
      }
    }
    if (apTranStatus != 3)
    {
      nullable2 = apTranRow.InventoryID;
      if (nullable2.HasValue)
      {
        if (fssoDetBase1 == null)
        {
          IFSSODetBase instance = (IFSSODetBase) cache.CreateInstance();
          instance.InventoryID = apTranRow.InventoryID;
          instance.BillingRule = "FLRA";
          instance.TranDesc = apTranRow.TranDesc;
          instance.ProjectID = apTranRow.ProjectID;
          instance.EstimatedDuration = new int?(0);
          instance.CuryUnitPrice = new Decimal?(0M);
          instance.UOM = apTranRow.UOM;
          instance.EstimatedQty = apTranRow.Qty;
          instance.ProjectTaskID = apTranRow.TaskID;
          instance.CostCodeID = apTranRow.CostCodeID;
          instance.LinkedEntityType = "AP";
          instance.LinkedDocType = apInvoiceRow.DocType;
          instance.LinkedDocRefNbr = apInvoiceRow.RefNbr;
          instance.LinkedLineNbr = apTranRow.LineNbr;
          instance.IsBillable = new bool?(false);
          fssoDetBase1 = (IFSSODetBase) cache.Insert((object) instance);
        }
        IFSSODetBase copy = (IFSSODetBase) cache.CreateCopy((object) fssoDetBase1);
        bool flag = false;
        if (copy.TranDesc != apTranRow.TranDesc)
        {
          copy.TranDesc = apTranRow.TranDesc;
          flag = true;
        }
        Decimal num1 = (Decimal) (apInvoiceRow.DocType == "ADR" ? -1 : 1);
        Decimal? nullable3 = copy.CuryUnitCost;
        Decimal num2 = num1;
        Decimal? nullable4 = apTranRow.CuryUnitCost;
        Decimal? nullable5 = nullable4.HasValue ? new Decimal?(num2 * nullable4.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable6;
        if (!(nullable3.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable3.HasValue == nullable5.HasValue))
        {
          IFSSODetBase fssoDetBase3 = copy;
          Decimal num3 = num1;
          nullable6 = apTranRow.CuryUnitCost;
          Decimal? nullable7;
          if (!nullable6.HasValue)
          {
            nullable3 = new Decimal?();
            nullable7 = nullable3;
          }
          else
            nullable7 = new Decimal?(num3 * nullable6.GetValueOrDefault());
          fssoDetBase3.CuryUnitCost = nullable7;
          flag = true;
        }
        nullable6 = copy.CuryExtCost;
        Decimal num4 = num1;
        nullable4 = apTranRow.CuryLineAmt;
        nullable3 = nullable4.HasValue ? new Decimal?(num4 * nullable4.GetValueOrDefault()) : new Decimal?();
        if (!(nullable6.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable6.HasValue == nullable3.HasValue))
        {
          IFSSODetBase fssoDetBase4 = copy;
          Decimal num5 = num1;
          nullable3 = apTranRow.CuryLineAmt;
          Decimal? nullable8;
          if (!nullable3.HasValue)
          {
            nullable6 = new Decimal?();
            nullable8 = nullable6;
          }
          else
            nullable8 = new Decimal?(num5 * nullable3.GetValueOrDefault());
          fssoDetBase4.CuryExtCost = nullable8;
          flag = true;
        }
        if (!IsDocBilledOrClosed)
        {
          if (copy.UOM != apTranRow.UOM)
          {
            copy.UOM = apTranRow.UOM;
            flag = true;
          }
          nullable3 = copy.EstimatedQty;
          nullable6 = apTranRow.Qty;
          if (!(nullable3.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable3.HasValue == nullable6.HasValue))
          {
            copy.EstimatedQty = apTranRow.Qty;
            flag = true;
          }
          nullable2 = copy.ProjectTaskID;
          nullable1 = apTranRow.TaskID;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          {
            copy.ProjectTaskID = apTranRow.TaskID;
            flag = true;
          }
          nullable1 = copy.CostCodeID;
          nullable2 = apTranRow.CostCodeID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            copy.CostCodeID = apTranRow.CostCodeID;
            flag = true;
          }
          if (copy.IsBillable.GetValueOrDefault())
          {
            nullable6 = copy.CuryBillableExtPrice;
            nullable3 = copy.CuryExtCost;
            if (!(nullable6.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable6.HasValue == nullable3.HasValue))
            {
              copy.CuryBillableExtPrice = copy.CuryExtCost;
              flag = true;
            }
          }
        }
        if (flag)
        {
          IFSSODetBase fssoDetBase5 = (IFSSODetBase) cache.Update((object) copy);
        }
      }
    }
    if (fssoDetBase2 == null)
      return;
    cache.Delete((object) fssoDetBase2);
  }

  public virtual void VerifyAPFieldCanBeUpdated<Field>(PXCache cache, APTran row) where Field : class, IBqlField
  {
    FSxAPTran extension = cache.GetExtension<FSxAPTran>((object) row);
    if (!extension.RelatedDocNoteID.HasValue || !(row.CreatedByScreenID?.Substring(0, 2) != "FS"))
      return;
    bool? docBilledOrClosed = extension.IsDocBilledOrClosed;
    if (!docBilledOrClosed.HasValue)
    {
      object entityRow = new EntityHelper((PXGraph) this.Base).GetEntityRow(extension.RelatedDocNoteID, true);
      if (entityRow != null)
        extension.IsDocBilledOrClosed = new bool?(this.IsFSDocumentBilledOrClosed(entityRow));
    }
    Guid? valueOriginal = (Guid?) cache.GetValueOriginal<FSxAPTran.relatedDocNoteID>((object) row);
    docBilledOrClosed = extension.IsDocBilledOrClosed;
    if (!docBilledOrClosed.GetValueOrDefault())
      return;
    Guid? nullable = valueOriginal;
    Guid? relatedDocNoteId = extension.RelatedDocNoteID;
    if ((nullable.HasValue == relatedDocNoteId.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == relatedDocNoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      throw new PXSetPropertyException("{0} cannot be changed because an associated FS document is closed or billed.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Field>(cache)
      });
  }

  public virtual bool IsFSDocumentBilledOrClosed(object row)
  {
    switch (row)
    {
      case null:
        return false;
      case FSAppointment _:
        return ((FSAppointment) row).IsBilledOrClosed;
      case FSServiceOrder _:
        return ((FSServiceOrder) row).IsBilledOrClosed;
      default:
        return false;
    }
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PX.Objects.AP.APInvoice>((PXGraph) this.Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }
}
