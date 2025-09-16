// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_ARInvoiceEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_ARInvoiceEntry : FSPostingBase<ARInvoiceEntry>, IInvoiceContractGraph
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public static bool IsFSIntegrationEnabled(PX.Objects.AR.ARInvoice arInvoiceRow, FSxARInvoice fsxARInvoiceRow)
  {
    return arInvoiceRow != null && (arInvoiceRow.CreatedByScreenID.Substring(0, 2) == "FS" || fsxARInvoiceRow.HasFSEquipmentInfo.GetValueOrDefault());
  }

  [PXOverride]
  public virtual IEnumerable ReverseInvoiceAndApplyToMemo(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseMethod)
  {
    if (((IQueryable<PXResult<FSBillHistory>>) PXSelectBase<FSBillHistory, PXViewOf<FSBillHistory>.BasedOn<SelectFromBase<FSBillHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSBillHistory.childEntityType, Equal<FSEntityType.soInvoice>>>>, And<BqlOperand<FSBillHistory.childDocType, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.docType, IBqlString>.FromCurrent>>>, And<BqlOperand<FSBillHistory.childRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<FSBillHistory.srvOrdType, IBqlString>.IsNotNull>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Any<PXResult<FSBillHistory>>())
      throw new PXException("The action cannot be performed because this AR invoice is a duplicate of the SO invoice that has been generated from the field service document. To proceed, perform the reversal procedure for the SO invoice with the same reference number on the Invoices (SO303000) form.");
    return baseMethod(adapter);
  }

  public static void SetUnpersistedFSInfo(PXCache cache, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) e.Row;
    if (row.CreatedByScreenID == null || row.CreatedByScreenID.Substring(0, 2) == "FS")
      return;
    FSxARInvoice extension = cache.GetExtension<FSxARInvoice>((object) row);
    if (!PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
    {
      extension.HasFSEquipmentInfo = new bool?(false);
    }
    else
    {
      if (extension.HasFSEquipmentInfo.HasValue)
        return;
      using (new PXConnectionScope())
      {
        PXResultset<PX.Objects.IN.InventoryItem> pxResultset = PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectJoin<PX.Objects.IN.InventoryItem, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.AR.ARTran.tranType, Equal<ARDocType.invoice>>>, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>, InnerJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.SO.SOLineSplit.lineNbr>>>>, LeftJoin<SOShipLineSplit, On<SOShipLineSplit.origOrderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<SOShipLineSplit.origOrderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>, And<SOShipLineSplit.origLineNbr, Equal<PX.Objects.SO.SOLineSplit.lineNbr>, And<SOShipLineSplit.origSplitLineNbr, Equal<PX.Objects.SO.SOLineSplit.splitLineNbr>>>>>>>>>, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<FSxSOLine.equipmentAction, NotEqual<ListField_EquipmentActionBase.None>>>>>.Config>.Select(cache.Graph, new object[2]
        {
          (object) row.DocType,
          (object) row.RefNbr
        });
        extension.HasFSEquipmentInfo = new bool?(pxResultset.Count > 0);
      }
    }
  }

  [PXMergeAttributes]
  [FSSelectorExtensionMaintenanceEquipment(typeof (PX.Objects.AR.ARTran.customerID), typeof (True))]
  protected virtual void _(PX.Data.Events.CacheAttached<FSxARTran.sMEquipmentID> e)
  {
  }

  [PXMergeAttributes]
  [FSSelectorEquipmentLineRefSOInvoice(typeof (True))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FSxARTran.equipmentComponentLineNbr> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.AR.ARInvoice> e)
  {
    SM_ARInvoiceEntry.SetUnpersistedFSInfo(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PX.Objects.AR.ARInvoice>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<PX.Objects.AR.ARInvoice>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    PX.Objects.AR.ARInvoice row = e.Row;
    this.ValidatePostBatchStatus(e.Operation, "AR", row.DocType, row.RefNbr);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    PX.Objects.AR.ARInvoice row = e.Row;
    if (e.Operation == 3 && e.TranStatus == null)
    {
      this.CleanPostingInfoLinkedToDoc((object) row);
      this.CleanContractPostingInfoLinkedToDoc((object) row);
    }
    if (e.Operation != 1 || e.TranStatus != null)
      return;
    this.EvaluateComponentLineNumbers(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.AR.ARInvoice>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.qty> e)
  {
    if (e.Row != null && SharedFunctions.isFSSetupSet((PXGraph) this.Base) && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, (object) e.Row, typeof (PX.Objects.AR.ARTran.qty).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM> e)
  {
    if (e.Row != null && SharedFunctions.isFSSetupSet((PXGraph) this.Base) && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, (object) e.Row, typeof (PX.Objects.AR.ARTran.uOM).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.inventoryID> e)
  {
    if (e.Row != null && SharedFunctions.isFSSetupSet((PXGraph) this.Base) && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, (object) e.Row, typeof (PX.Objects.AR.ARTran.inventoryID).Name))
      throw new PXSetPropertyException("This value cannot be updated because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.AR.ARTran> e)
  {
    this.UpdateARTran(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.AR.ARTran>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.AR.ARTran> e)
  {
    if (e.Row == null || !SharedFunctions.isFSSetupSet((PXGraph) this.Base))
      return;
    PX.Objects.AR.ARTran row = e.Row;
    if (e.ExternalCall && this.IsLineCreatedFromAppSO((PXGraph) this.Base, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, (object) row, (string) null))
      throw new PXException("The line cannot be deleted because it is related to an appointment or service order.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.ARTran> e)
  {
    if (e.Row == null || e.TranStatus != 2 || !this.IsInvoiceProcessRunning)
      return;
    MessageHelper.GetRowMessage(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.AR.ARTran>>) e).Cache, (IBqlTable) e.Row, false, false);
  }

  public override List<MessageHelper.ErrorInfo> GetErrorInfo()
  {
    return MessageHelper.GetErrorInfo<PX.Objects.AR.ARTran>(((PXSelectBase) this.Base.Document).Cache, (IBqlTable) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, (PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions, typeof (PX.Objects.AR.ARTran));
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
    SM_ARInvoiceEntry.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new SM_ARInvoiceEntry.\u003C\u003Ec__DisplayClass31_0();
    if (docLines.Count == 0)
      return;
    bool? nullable1 = new bool?(false);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.fsServiceOrderRow = docLines[0].fsServiceOrder;
    FSSrvOrdType fsSrvOrdType1 = docLines[0].fsSrvOrdType;
    FSPostDoc fsPostDoc1 = docLines[0].fsPostDoc;
    FSAppointment fsAppointment = docLines[0].fsAppointment;
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass310, __methodptr(\u003CCreateInvoice\u003Eb__0));
    try
    {
      ((PXGraph) this.Base).FieldDefaulting.AddHandler<PX.Objects.AR.ARInvoice.branchID>(pxFieldDefaulting);
      PX.Objects.AR.ARInvoice arInvoice1 = new PX.Objects.AR.ARInvoice();
      if (invtMult >= (short) 0)
      {
        arInvoice1.DocType = "INV";
        this.CheckAutoNumbering(((PXSelectBase<ARSetup>) this.Base.ARSetup).SelectSingle(Array.Empty<object>()).InvoiceNumberingID);
      }
      else
      {
        arInvoice1.DocType = "CRM";
        this.CheckAutoNumbering(((PXSelectBase<ARSetup>) this.Base.ARSetup).SelectSingle(Array.Empty<object>()).CreditAdjNumberingID);
      }
      arInvoice1.DocDate = invoiceDate;
      arInvoice1.FinPeriodID = invoiceFinPeriodID;
      // ISSUE: reference to a compiler-generated field
      arInvoice1.InvoiceNbr = cDisplayClass310.fsServiceOrderRow.CustPORefNbr;
      PX.Objects.AR.ARInvoice data1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Insert(arInvoice1);
      bool? holdEntry = ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.HoldEntry;
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.hold>((object) data1, (object) true);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.customerID>((object) data1, (object) cDisplayClass310.fsServiceOrderRow.BillCustomerID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.customerLocationID>((object) data1, (object) cDisplayClass310.fsServiceOrderRow.BillLocationID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.curyID>((object) data1, (object) cDisplayClass310.fsServiceOrderRow.CuryID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.taxZoneID>((object) data1, fsAppointment != null ? (object) fsAppointment.TaxZoneID : (object) cDisplayClass310.fsServiceOrderRow.TaxZoneID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.taxCalcMode>((object) data1, fsAppointment != null ? (object) fsAppointment.TaxCalcMode : (object) cDisplayClass310.fsServiceOrderRow.TaxCalcMode);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.externalTaxExemptionNumber>((object) data1, fsAppointment != null ? (object) fsAppointment.ExternalTaxExemptionNumber : (object) cDisplayClass310.fsServiceOrderRow.ExternalTaxExemptionNumber);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.avalaraCustomerUsageType>((object) data1, fsAppointment != null ? (object) fsAppointment.EntityUsageType : (object) cDisplayClass310.fsServiceOrderRow.EntityUsageType);
      // ISSUE: reference to a compiler-generated field
      string customerOrVendor = this.GetTermsIDFromCustomerOrVendor(graphProcess, cDisplayClass310.fsServiceOrderRow.BillCustomerID, new int?());
      if ((!(data1.DocType == "CRM") ? 1 : (((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.TermsInCreditMemos.GetValueOrDefault() ? 1 : 0)) != 0)
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.termsID>((object) data1, (object) (customerOrVendor ?? fsSrvOrdType1.DfltTermIDARSO));
      // ISSUE: reference to a compiler-generated field
      int? nullable2 = cDisplayClass310.fsServiceOrderRow.ProjectID;
      if (nullable2.HasValue)
      {
        // ISSUE: reference to a compiler-generated field
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.projectID>((object) data1, (object) cDisplayClass310.fsServiceOrderRow.ProjectID);
      }
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.docDesc>((object) data1, (object) cDisplayClass310.fsServiceOrderRow.DocDesc);
      data1.FinPeriodID = invoiceFinPeriodID;
      PX.Objects.AR.ARInvoice arInvoice2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Update(data1);
      // ISSUE: reference to a compiler-generated field
      this.SetContactAndAddress((PXGraph) this.Base, cDisplayClass310.fsServiceOrderRow);
      if (onDocumentHeaderInserted != null)
        onDocumentHeaderInserted((PXGraph) this.Base, (IBqlTable) arInvoice2);
      foreach (DocLineExt docLine1 in docLines)
      {
        // ISSUE: reference to a compiler-generated field
        bool flag = fsAppointment == null ? docLine1.fsSODet == docLines[0].fsSODet || cDisplayClass310.fsServiceOrderRow.RefNbr != docLine1.fsServiceOrder.RefNbr : docLine1.fsAppointmentDet == docLines[0].fsAppointmentDet || fsAppointment.RefNbr != docLine1.fsAppointment.RefNbr;
        IDocLine docLine2 = docLine1.docLine;
        FSPostDoc fsPostDoc2 = docLine1.fsPostDoc;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass310.fsServiceOrderRow = docLine1.fsServiceOrder;
        FSSrvOrdType fsSrvOrdType2 = docLine1.fsSrvOrdType;
        fsAppointment = docLine1.fsAppointment;
        FSAppointmentDet fsAppointmentDet = docLine1.fsAppointmentDet;
        FSSODet fsSoDet = docLine1.fsSODet;
        if (docLine2.LineType == "SLPRO")
          throw new PXException("An AR document cannot be generated for service orders or appointments with inventory items. Remove the inventory items from the document or specify another document type in the Generated Billing Documents box on the Service Order Types (FS202300) form for the type of the document.", new object[1]
          {
            (object) (fsAppointment != null ? "Appointment" : "Service Order")
          });
        PX.Objects.AR.ARTran data2 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Insert(new PX.Objects.AR.ARTran());
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.branchID>((object) data2, (object) docLine2.BranchID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.inventoryID>((object) data2, (object) docLine2.InventoryID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.uOM>((object) data2, (object) docLine2.UOM);
        PMTask pmTask = docLine1.pmTask;
        if (pmTask != null && pmTask.Status == "F")
        {
          // ISSUE: reference to a compiler-generated field
          throw new PXException("The {1} line of the {0} document cannot be processed because the {2} project task has already been completed.", new object[3]
          {
            (object) cDisplayClass310.fsServiceOrderRow.RefNbr,
            (object) this.GetLineDisplayHint((PXGraph) this.Base, docLine2.LineRef, docLine2.TranDesc, docLine2.InventoryID),
            (object) pmTask.TaskCD
          });
        }
        nullable2 = docLine2.ProjectID;
        if (nullable2.HasValue)
        {
          nullable2 = docLine2.ProjectTaskID;
          if (nullable2.HasValue)
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.taskID>((object) data2, (object) docLine2.ProjectTaskID);
        }
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.qty>((object) data2, (object) docLine2.GetQty(FieldType.BillableField));
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.tranDesc>((object) data2, (object) docLine2.TranDesc);
        PX.Objects.AR.ARTran arTran;
        fsPostDoc2.DocLineRef = (object) (arTran = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(data2));
        PXCache cache1 = ((PXSelectBase) this.Base.Transactions).Cache;
        PX.Objects.AR.ARTran data3 = arTran;
        // ISSUE: reference to a compiler-generated field
        FSServiceOrder fsServiceOrderRow = cDisplayClass310.fsServiceOrderRow;
        int? nullable3;
        if (fsServiceOrderRow == null)
        {
          nullable2 = new int?();
          nullable3 = nullable2;
        }
        else
          nullable3 = fsServiceOrderRow.SalesPersonID;
        // ISSUE: variable of a boxed type
        __Boxed<int?> newValue1 = (ValueType) nullable3;
        cache1.SetValueExtIfDifferent<PX.Objects.AR.ARTran.salesPersonID>((object) data3, (object) newValue1);
        nullable2 = docLine2.AcctID;
        // ISSUE: reference to a compiler-generated field
        int? newValue2 = !nullable2.HasValue ? this.Get_TranAcctID_DefaultValue(graphProcess, fsSrvOrdType2.SalesAcctSource, docLine2.InventoryID, docLine2.SiteID, cDisplayClass310.fsServiceOrderRow) : docLine2.AcctID;
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.accountID>((object) arTran, (object) newValue2);
        nullable2 = docLine2.SubID;
        if (nullable2.HasValue)
        {
          try
          {
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.subID>((object) arTran, (object) docLine2.SubID);
          }
          catch (PXException ex)
          {
            arTran.SubID = new int?();
          }
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.SetCombinedSubID(graphProcess, ((PXSelectBase) this.Base.Transactions).Cache, arTran, (APTran) null, (PX.Objects.SO.SOLine) null, fsSrvOrdType2, arTran.BranchID, arTran.InventoryID, arInvoice2.CustomerLocationID, cDisplayClass310.fsServiceOrderRow.BranchLocationID, cDisplayClass310.fsServiceOrderRow.SalesPersonID, docLine2.IsService);
        }
        bool? nullable4 = docLine2.IsFree;
        if (nullable4.GetValueOrDefault())
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualPrice>((object) arTran, (object) true);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyUnitPrice>((object) arTran, (object) 0M);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualDisc>((object) arTran, (object) true);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.discPct>((object) arTran, (object) 0M);
        }
        else
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualPrice>((object) arTran, (object) docLine2.ManualPrice);
          PXCache cache2 = ((PXSelectBase) this.Base.Transactions).Cache;
          PX.Objects.AR.ARTran data4 = arTran;
          Decimal? curyUnitPrice = docLine2.CuryUnitPrice;
          Decimal num = (Decimal) invtMult;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> newValue3 = (ValueType) (curyUnitPrice.HasValue ? new Decimal?(curyUnitPrice.GetValueOrDefault() * num) : new Decimal?());
          cache2.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyUnitPrice>((object) data4, (object) newValue3);
          bool newValue4 = false;
          if (docLine1.fsAppointmentDet != null)
          {
            nullable4 = docLine1.fsAppointmentDet.ManualDisc;
            newValue4 = nullable4.Value;
          }
          else if (docLine1.fsSODet != null)
          {
            nullable4 = docLine1.fsSODet.ManualDisc;
            newValue4 = nullable4.Value;
          }
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualDisc>((object) arTran, (object) newValue4);
          if (newValue4)
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.discPct>((object) arTran, (object) docLine2.DiscPct);
        }
        Decimal? billableExtPrice = docLine2.CuryBillableExtPrice;
        Decimal num1 = (Decimal) invtMult;
        Decimal? newValue5 = billableExtPrice.HasValue ? new Decimal?(billableExtPrice.GetValueOrDefault() * num1) : new Decimal?();
        newValue5 = new Decimal?(PXCurrencyAttribute.Round(((PXSelectBase) this.Base.Transactions).Cache, (object) arTran, newValue5.GetValueOrDefault(), CMPrecision.TRANCURY));
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyExtPrice>((object) arTran, (object) newValue5);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.taxCategoryID>((object) arTran, (object) docLine2.TaxCategoryID);
        PXCache cache3 = ((PXSelectBase) this.Base.Transactions).Cache;
        PX.Objects.AR.ARTran data5 = arTran;
        // ISSUE: reference to a compiler-generated field
        nullable4 = cDisplayClass310.fsServiceOrderRow.Commissionable;
        // ISSUE: variable of a boxed type
        __Boxed<bool> valueOrDefault = (ValueType) nullable4.GetValueOrDefault();
        cache3.SetValueExtIfDifferent<PX.Objects.AR.ARTran.commissionable>((object) data5, (object) valueOrDefault);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.costCodeID>((object) arTran, (object) docLine2.CostCodeID);
        FSxARTran extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxARTran>((object) arTran);
        // ISSUE: reference to a compiler-generated field
        extension.SrvOrdType = cDisplayClass310.fsServiceOrderRow.SrvOrdType;
        // ISSUE: reference to a compiler-generated field
        extension.ServiceOrderRefNbr = cDisplayClass310.fsServiceOrderRow.RefNbr;
        extension.AppointmentRefNbr = fsAppointment?.RefNbr;
        int? nullable5;
        if (fsSoDet == null)
        {
          nullable2 = new int?();
          nullable5 = nullable2;
        }
        else
          nullable5 = fsSoDet.LineNbr;
        extension.ServiceOrderLineNbr = nullable5;
        int? nullable6;
        if (fsAppointmentDet == null)
        {
          nullable2 = new int?();
          nullable6 = nullable2;
        }
        else
          nullable6 = fsAppointmentDet.LineNbr;
        extension.AppointmentLineNbr = nullable6;
        PX.Objects.AR.ARTran lineDocument = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(arTran);
        if (flag)
        {
          if (fsAppointment == null)
          {
            // ISSUE: reference to a compiler-generated field
            SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSServiceOrder)], ((PXSelectBase) this.Base.Document).Cache, (object) cDisplayClass310.fsServiceOrderRow, (object) arInvoice2, fsSrvOrdType2.CopyNotesToInvoice, fsSrvOrdType2.CopyAttachmentsToInvoice);
          }
          else
            SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSAppointment)], ((PXSelectBase) this.Base.Document).Cache, (object) fsAppointment, (object) arInvoice2, fsSrvOrdType2.CopyNotesToInvoice, fsSrvOrdType2.CopyAttachmentsToInvoice);
        }
        SharedFunctions.CopyNotesAndFiles(((PXSelectBase) this.Base.Transactions).Cache, (object) lineDocument, docLine2, fsSrvOrdType2);
        PX.Objects.AR.ARTran row;
        fsPostDoc2.DocLineRef = (object) (row = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(lineDocument));
        if (onTransactionInserted != null)
          onTransactionInserted((PXGraph) this.Base, (IBqlTable) row);
      }
      PX.Objects.AR.ARInvoice data6 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Update(arInvoice2);
      if (((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.RequireControlTotal.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) data6, (object) data6.CuryDocBal);
      if (!holdEntry.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.hold>((object) data6, (object) false);
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Update(data6);
    }
    finally
    {
      ((PXGraph) this.Base).FieldDefaulting.RemoveHandler<PX.Objects.AR.ARInvoice.branchID>(pxFieldDefaulting);
    }
  }

  public override FSCreatedDoc PressSave(
    int batchID,
    List<DocLineExt> docLines,
    BeforeSaveDelegate beforeSave)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current == null)
      throw new SharedClasses.TransactionScopeException();
    if (beforeSave != null)
      beforeSave((PXGraph) this.Base);
    ARInvoiceEntryExternalTax extension = ((PXGraph) this.Base).GetExtension<ARInvoiceEntryExternalTax>();
    if (extension != null)
      extension.SkipTaxCalcAndSave();
    else
      ((PXAction) this.Base.Save).Press();
    string docType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType;
    string refNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.RefNbr;
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) docType,
      (object) refNbr
    }));
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    return new FSCreatedDoc()
    {
      BatchID = new int?(batchID),
      PostTo = "AR",
      CreatedDocType = current.DocType,
      CreatedRefNbr = current.RefNbr
    };
  }

  public override void Clear() => ((PXGraph) this.Base).Clear((PXClearOption) 3);

  public override PXGraph GetGraph() => (PXGraph) this.Base;

  public override void DeleteDocument(FSCreatedDoc fsCreatedDocRow)
  {
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) fsCreatedDocRow.CreatedRefNbr, new object[1]
    {
      (object) fsCreatedDocRow.CreatedDocType
    }));
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current == null || !(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.RefNbr == fsCreatedDocRow.CreatedRefNbr) || !(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType == fsCreatedDocRow.CreatedDocType))
      return;
    ((PXAction) this.Base.Delete).Press();
  }

  public override void CleanPostInfo(PXGraph cleanerGraph, FSPostDet fsPostDetRow)
  {
    PXUpdate<Set<FSPostInfo.aRLineNbr, Null, Set<FSPostInfo.arRefNbr, Null, Set<FSPostInfo.arDocType, Null, Set<FSPostInfo.aRPosted, False>>>>, FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>, And<FSPostInfo.aRPosted, Equal<True>>>>.Update(cleanerGraph, new object[1]
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
    string refNbr = ((PX.Objects.AR.ARRegister) document).RefNbr;
    string docType = ((PX.Objects.AR.ARRegister) document).DocType;
    int? lineNbr = ((PX.Objects.AR.ARTran) lineDoc).LineNbr;
    return ((IQueryable<PXResult<FSPostInfo>>) PXSelectBase<FSPostInfo, PXSelect<FSPostInfo, Where<FSPostInfo.arRefNbr, Equal<Required<FSPostInfo.arRefNbr>>, And<FSPostInfo.arDocType, Equal<Required<FSPostInfo.arDocType>>, And<FSPostInfo.aRLineNbr, Equal<Required<FSPostInfo.aRLineNbr>>, And<FSPostInfo.aRPosted, Equal<True>>>>>>.Config>.Select(cleanerGraph, new object[3]
    {
      (object) refNbr,
      (object) docType,
      (object) lineNbr
    })).Count<PXResult<FSPostInfo>>() > 0;
  }

  public virtual void UpdateARTran(PXCache cache, PX.Objects.AR.ARTran arTranRow)
  {
    FSxARTran extension1 = cache.GetExtension<FSxARTran>((object) arTranRow);
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType != "CRM")
    {
      if (!SM_ARInvoiceEntry.SOExists(arTranRow))
        return;
      PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find(cache.Graph, arTranRow.SOOrderType, arTranRow.SOOrderNbr, arTranRow.SOOrderLineNbr);
      if (soLine == null)
        return;
      FSxSOLine extension2 = PXCache<PX.Objects.SO.SOLine>.GetExtension<FSxSOLine>(soLine);
      extension1.SrvOrdType = extension2.SrvOrdType;
      extension1.AppointmentRefNbr = extension2.AppointmentRefNbr;
      extension1.AppointmentLineNbr = extension2.AppointmentLineNbr;
      extension1.ServiceOrderRefNbr = extension2.ServiceOrderRefNbr;
      extension1.ServiceOrderLineNbr = extension2.ServiceOrderLineNbr;
      if (!string.IsNullOrEmpty(extension2?.ServiceContractRefNbr))
      {
        extension1.ServiceContractRefNbr = extension2.ServiceContractRefNbr;
        extension1.ServiceContractPeriodID = extension2.ServiceContractPeriodID;
      }
      extension1.SMEquipmentID = extension2.SMEquipmentID;
      extension1.ComponentID = extension2.ComponentID;
      extension1.EquipmentComponentLineNbr = extension2.EquipmentComponentLineNbr;
      extension1.EquipmentAction = extension2.EquipmentAction;
      extension1.Comment = extension2.Comment;
      PX.Objects.SO.SOLine soLineRow2 = PX.Objects.SO.SOLine.PK.Find(cache.Graph, arTranRow.SOOrderType, arTranRow.SOOrderNbr, extension2.NewEquipmentLineNbr);
      if (soLineRow2 == null)
        return;
      PX.Objects.AR.ARTran arTran = GraphHelper.RowCast<PX.Objects.AR.ARTran>(cache.Cached).Where<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (x =>
      {
        if (!(x.SOOrderType == arTranRow.SOOrderType) || !(x.SOOrderNbr == arTranRow.SOOrderNbr))
          return false;
        int? soOrderLineNbr = x.SOOrderLineNbr;
        int? lineNbr = soLineRow2.LineNbr;
        return soOrderLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & soOrderLineNbr.HasValue == lineNbr.HasValue;
      })).FirstOrDefault<PX.Objects.AR.ARTran>();
      extension1.NewEquipmentLineNbr = (int?) arTran?.LineNbr;
    }
    else
    {
      PX.Objects.AR.ARTran arTran = (PX.Objects.AR.ARTran) null;
      PXSelect<PX.Objects.AR.ARTran> pxSelect = new PXSelect<PX.Objects.AR.ARTran>((PXGraph) this.Base);
      ((PXSelectBase<PX.Objects.AR.ARTran>) pxSelect).WhereAnd<Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<PX.Objects.AR.ARTran.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>>();
      if (!string.IsNullOrEmpty(arTranRow.OrigInvoiceType) && !string.IsNullOrEmpty(arTranRow.OrigInvoiceNbr) && arTranRow.OrigInvoiceLineNbr.HasValue)
        arTran = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARTran>) pxSelect).Select(new object[3]
        {
          (object) arTranRow.OrigInvoiceType,
          (object) arTranRow.OrigInvoiceNbr,
          (object) arTranRow.OrigInvoiceLineNbr
        }));
      else if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.CurrentDocument).Current.OrigRefNbr != null)
        arTran = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARTran>) pxSelect).Select(new object[3]
        {
          (object) "INV",
          (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.CurrentDocument).Current.OrigRefNbr,
          (object) arTranRow.OrigLineNbr
        }));
      if (arTran == null)
        return;
      FSxARTran extension3 = cache.GetExtension<FSxARTran>((object) arTran);
      extension1.SrvOrdType = extension3.SrvOrdType;
      extension1.ServiceOrderRefNbr = extension3.ServiceOrderRefNbr;
      extension1.ServiceOrderLineNbr = extension3.ServiceOrderLineNbr;
      extension1.AppointmentRefNbr = extension3.AppointmentRefNbr;
      extension1.AppointmentLineNbr = extension3.AppointmentLineNbr;
      extension1.ServiceContractRefNbr = extension3.ServiceContractRefNbr;
      extension1.ServiceContractPeriodID = extension3.ServiceContractPeriodID;
      extension1.Comment = extension3.Comment;
      extension1.EquipmentAction = extension3.EquipmentAction;
      extension1.SMEquipmentID = extension3.SMEquipmentID;
      extension1.NewEquipmentLineNbr = extension3.NewEquipmentLineNbr;
      extension1.ComponentID = extension3.ComponentID;
      extension1.EquipmentComponentLineNbr = extension3.EquipmentComponentLineNbr;
      extension1.ReplaceSMEquipmentID = extension3.ReplaceSMEquipmentID;
    }
  }

  public virtual FSContractPostDoc CreateInvoiceByContract(
    PXGraph graphProcess,
    DateTime? invoiceDate,
    string invoiceFinPeriodID,
    FSContractPostBatch fsContractPostBatchRow,
    FSServiceContract fsServiceContractRow,
    FSContractPeriod fsContractPeriodRow,
    List<ContractInvoiceLine> docLines)
  {
    try
    {
      if (docLines.Count == 0)
        return (FSContractPostDoc) null;
      bool? nullable1 = new bool?(false);
      FSSetup serviceManagementSetup = ServiceManagementSetup.GetServiceManagementSetup(graphProcess);
      if (!((PXGraph) this.Base).Views.Caches.Contains(typeof (PX.Objects.AR.ARTran)))
        ((PXGraph) this.Base).Views.Caches.Add(typeof (PX.Objects.AR.ARTran));
      PX.Objects.AR.ARInvoice arInvoice = new PX.Objects.AR.ARInvoice();
      arInvoice.DocType = "INV";
      this.CheckAutoNumbering(((PXSelectBase<ARSetup>) this.Base.ARSetup).SelectSingle(Array.Empty<object>()).InvoiceNumberingID);
      arInvoice.DocDate = invoiceDate;
      arInvoice.FinPeriodID = invoiceFinPeriodID;
      PX.Objects.AR.ARInvoice data1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Insert(arInvoice);
      bool? hold = data1.Hold;
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.hold>((object) data1, (object) true);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.branchID>((object) data1, (object) fsServiceContractRow.BranchID);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.customerID>((object) data1, (object) fsServiceContractRow.BillCustomerID);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.customerLocationID>((object) data1, (object) fsServiceContractRow.BillLocationID);
      string localizedLabel = PXStringListAttribute.GetLocalizedLabel<FSServiceContract.billingType>(graphProcess.Caches[typeof (FSServiceContract)], (object) fsServiceContractRow);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.docDesc>((object) data1, (object) PXMessages.LocalizeFormatNoPrefix("{0} Contract: {1} {2}", new object[3]
      {
        (object) localizedLabel,
        (object) fsServiceContractRow.RefNbr,
        string.IsNullOrEmpty(fsServiceContractRow.DocDesc) ? (object) string.Empty : (object) fsServiceContractRow.DocDesc
      }));
      string customerOrVendor = this.GetTermsIDFromCustomerOrVendor(graphProcess, fsServiceContractRow.BillCustomerID, new int?());
      if (customerOrVendor != null)
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.termsID>((object) data1, (object) customerOrVendor);
      else
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.termsID>((object) data1, (object) serviceManagementSetup.DfltContractTermIDARSO);
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.projectID>((object) data1, (object) fsServiceContractRow.ProjectID);
      foreach (ContractInvoiceLine docLine in docLines)
      {
        PX.Objects.AR.ARTran data2 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Insert(new PX.Objects.AR.ARTran());
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.inventoryID>((object) data2, (object) docLine.InventoryID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.uOM>((object) data2, (object) docLine.UOM);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.salesPersonID>((object) data2, (object) docLine.SalesPersonID);
        PX.Objects.AR.ARTran arTran = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(data2);
        int? nullable2 = docLine.AcctID;
        int? newValue1;
        if (nullable2.HasValue)
        {
          newValue1 = docLine.AcctID;
        }
        else
        {
          PXGraph graph = graphProcess;
          string contractSalesAcctSource = serviceManagementSetup.ContractSalesAcctSource;
          int? inventoryId = docLine.InventoryID;
          int? customerID;
          if (fsServiceContractRow == null)
          {
            nullable2 = new int?();
            customerID = nullable2;
          }
          else
            customerID = fsServiceContractRow.CustomerID;
          int? locationID;
          if (fsServiceContractRow == null)
          {
            nullable2 = new int?();
            locationID = nullable2;
          }
          else
            locationID = fsServiceContractRow.CustomerLocationID;
          newValue1 = this.Get_INItemAcctID_DefaultValue(graph, contractSalesAcctSource, inventoryId, customerID, locationID);
        }
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.accountID>((object) arTran, (object) newValue1);
        nullable2 = docLine.SubID;
        if (nullable2.HasValue)
        {
          try
          {
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.subID>((object) arTran, (object) docLine.SubID);
          }
          catch (PXException ex)
          {
            arTran.SubID = new int?();
          }
        }
        else
          this.SetCombinedSubID(graphProcess, ((PXSelectBase) this.Base.Transactions).Cache, arTran, (APTran) null, (PX.Objects.SO.SOLine) null, serviceManagementSetup, arTran.BranchID, arTran.InventoryID, data1.CustomerLocationID, fsServiceContractRow.BranchLocationID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.qty>((object) arTran, (object) docLine.Qty);
        bool? nullable3 = docLine.IsFree;
        if (nullable3.GetValueOrDefault())
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualPrice>((object) arTran, (object) true);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyUnitPrice>((object) arTran, (object) 0M);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualDisc>((object) arTran, (object) true);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.discPct>((object) arTran, (object) 0M);
        }
        else
        {
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualPrice>((object) arTran, (object) docLine.ManualPrice);
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyUnitPrice>((object) arTran, (object) docLine.CuryUnitPrice);
          bool newValue2 = false;
          nullable2 = docLine.AppDetID;
          if (nullable2.HasValue)
          {
            FSAppointmentDet fsAppointmentDet = PXResultset<FSAppointmentDet>.op_Implicit(PXSelectBase<FSAppointmentDet, PXSelect<FSAppointmentDet, Where<FSAppointmentDet.appDetID, Equal<Required<FSAppointmentDet.appDetID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
            {
              (object) docLine.AppDetID
            }));
            bool? nullable4;
            if (fsAppointmentDet == null)
            {
              nullable3 = new bool?();
              nullable4 = nullable3;
            }
            else
              nullable4 = fsAppointmentDet.ManualDisc;
            nullable3 = nullable4;
            newValue2 = nullable3.Value;
          }
          else
          {
            nullable2 = docLine.SODetID;
            if (nullable2.HasValue)
            {
              FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXSelect<FSSODet, Where<FSSODet.sODetID, Equal<Required<FSSODet.sODetID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) docLine.SODetID
              }));
              bool? nullable5;
              if (fssoDet == null)
              {
                nullable3 = new bool?();
                nullable5 = nullable3;
              }
              else
                nullable5 = fssoDet.ManualDisc;
              nullable3 = nullable5;
              newValue2 = nullable3.Value;
            }
          }
          ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualDisc>((object) arTran, (object) newValue2);
          if (newValue2)
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.discPct>((object) arTran, (object) docLine.DiscPct);
        }
        nullable2 = docLine.ServiceContractID;
        if (nullable2.HasValue)
        {
          nullable3 = docLine.ContractRelated;
          bool flag = false;
          if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
          {
            nullable2 = docLine.SODetID;
            if (!nullable2.HasValue)
            {
              nullable2 = docLine.AppDetID;
              if (!nullable2.HasValue)
                goto label_42;
            }
            ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyExtPrice>((object) arTran, (object) docLine.CuryBillableExtPrice);
          }
        }
label_42:
        string newValue3 = fsServiceContractRow.BillingType == "STDB" ? docLine.TranDescPrefix + arTran.TranDesc : arTran.TranDesc;
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.tranDesc>((object) arTran, (object) newValue3);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.taskID>((object) arTran, (object) docLine.ProjectTaskID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.costCodeID>((object) arTran, (object) docLine.CostCodeID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.deferredCode>((object) arTran, (object) docLine.DeferredCode);
        PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
        PX.Objects.AR.ARTran data3 = arTran;
        nullable3 = docLine.Commissionable;
        // ISSUE: variable of a boxed type
        __Boxed<bool> valueOrDefault = (ValueType) nullable3.GetValueOrDefault();
        cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.commissionable>((object) data3, (object) valueOrDefault);
        FSxARTran extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxARTran>((object) arTran);
        extension.ServiceContractRefNbr = fsServiceContractRow.RefNbr;
        extension.ServiceContractPeriodID = fsContractPeriodRow.ContractPeriodID;
        nullable3 = docLine.ContractRelated;
        bool flag1 = false;
        if (nullable3.GetValueOrDefault() == flag1 & nullable3.HasValue)
        {
          extension.SrvOrdType = docLine.SrvOrdType;
          extension.ServiceOrderRefNbr = docLine.fsSODet.RefNbr;
          extension.ServiceOrderLineNbr = docLine.fsSODet.LineNbr;
          extension.AppointmentRefNbr = docLine.fsAppointmentDet?.RefNbr;
          FSxARTran fsxArTran = extension;
          FSAppointmentDet fsAppointmentDet = docLine.fsAppointmentDet;
          int? nullable6;
          if (fsAppointmentDet == null)
          {
            nullable2 = new int?();
            nullable6 = nullable2;
          }
          else
            nullable6 = fsAppointmentDet.LineNbr;
          fsxArTran.AppointmentLineNbr = nullable6;
        }
        ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(arTran);
      }
      this.SetChildCustomerShipToInfo(fsServiceContractRow.ServiceContractID);
      bool? nullable7 = ((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.RequireControlTotal;
      if (nullable7.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) data1, (object) data1.CuryDocBal);
      if (!hold.GetValueOrDefault())
      {
        bool? nullable8 = ((PXSelectBase) this.Base.Document).Cache.GetValue<PX.Objects.AR.ARInvoice.hold>((object) data1) as bool?;
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.hold>((object) data1, (object) false);
        bool? nullable9 = ((PXSelectBase) this.Base.Document).Cache.GetValue<PX.Objects.AR.ARInvoice.hold>((object) data1) as bool?;
        nullable7 = nullable8;
        bool? nullable10 = nullable9;
        if (!(nullable7.GetValueOrDefault() == nullable10.GetValueOrDefault() & nullable7.HasValue == nullable10.HasValue))
          ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Update(data1);
      }
      Exception exception = (Exception) null;
      try
      {
        ((PXAction) this.Base.Save).Press();
      }
      catch (Exception ex)
      {
        exception = this.GetErrorInfoInLines(this.GetErrorInfo(), ex);
      }
      if (exception != null)
        throw exception;
      PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
      return new FSContractPostDoc()
      {
        ContractPeriodID = fsContractPeriodRow.ContractPeriodID,
        ContractPostBatchID = fsContractPostBatchRow.ContractPostBatchID,
        PostDocType = current.DocType,
        PostedTO = "AR",
        PostRefNbr = current.RefNbr,
        ServiceContractID = fsServiceContractRow.ServiceContractID
      };
    }
    finally
    {
      this.Clear();
    }
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PX.Objects.AR.ARInvoice>((PXGraph) this.Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  public virtual int? Get_INItemAcctID_DefaultValue(
    PXGraph graph,
    string salesAcctSource,
    int? inventoryID,
    int? customerID,
    int? locationID)
  {
    return ServiceOrderEntry.Get_INItemAcctID_DefaultValueInt(graph, salesAcctSource, inventoryID, customerID, locationID);
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

  protected virtual bool IsRunningServiceContractBilling(PXGraph graph)
  {
    return InvoiceHelper.IsRunningServiceContractBilling(graph);
  }

  protected virtual void GetChildCustomerShippingContactAndAddress(
    PXGraph graph,
    int? serviceContractID,
    out PX.Objects.CR.Contact shippingContact,
    out PX.Objects.CR.Address shippingAddress)
  {
    InvoiceHelper.GetChildCustomerShippingContactAndAddress(graph, serviceContractID, out shippingContact, out shippingAddress);
  }

  public virtual void SetChildCustomerShipToInfo(int? serviceContractID)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current == null || !this.IsRunningServiceContractBilling((PXGraph) this.Base) || ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current) != 2)
      return;
    PX.Objects.CR.Contact shippingContact = (PX.Objects.CR.Contact) null;
    PX.Objects.CR.Address shippingAddress = (PX.Objects.CR.Address) null;
    this.GetChildCustomerShippingContactAndAddress((PXGraph) this.Base, serviceContractID, out shippingContact, out shippingAddress);
    if (shippingContact != null)
    {
      ARShippingContact arShippingContact;
      ((PXSelectBase<ARShippingContact>) this.Base.Shipping_Contact).Current = arShippingContact = ((PXSelectBase<ARShippingContact>) this.Base.Shipping_Contact).SelectSingle(Array.Empty<object>());
      if (arShippingContact != null)
      {
        ((PXSelectBase) this.Base.Shipping_Contact).Cache.SetValueExt<ARShippingContact.overrideContact>((object) arShippingContact, (object) true);
        ARShippingContact copy = (ARShippingContact) ((PXSelectBase) this.Base.Shipping_Contact).Cache.CreateCopy((object) ((PXSelectBase<ARShippingContact>) this.Base.Shipping_Contact).SelectSingle(Array.Empty<object>()));
        int? customerId = copy.CustomerID;
        int? customerContactId = copy.CustomerContactID;
        InvoiceHelper.CopyContact((IContact) copy, (IContact) shippingContact);
        copy.CustomerID = customerId;
        copy.CustomerContactID = customerContactId;
        copy.RevisionID = new int?(0);
        copy.IsDefaultContact = new bool?(false);
        ((PXSelectBase<ARShippingContact>) this.Base.Shipping_Contact).Update(copy);
      }
    }
    if (shippingAddress == null)
      return;
    ARShippingAddress arShippingAddress;
    ((PXSelectBase<ARShippingAddress>) this.Base.Shipping_Address).Current = arShippingAddress = ((PXSelectBase<ARShippingAddress>) this.Base.Shipping_Address).SelectSingle(Array.Empty<object>());
    if (arShippingAddress == null)
      return;
    ((PXSelectBase) this.Base.Shipping_Address).Cache.SetValueExt<ARShippingAddress.overrideAddress>((object) arShippingAddress, (object) true);
    ARShippingAddress copy1 = (ARShippingAddress) ((PXSelectBase) this.Base.Shipping_Address).Cache.CreateCopy((object) ((PXSelectBase<ARShippingAddress>) this.Base.Shipping_Address).SelectSingle(Array.Empty<object>()));
    int? customerId1 = copy1.CustomerID;
    int? customerAddressId = copy1.CustomerAddressID;
    InvoiceHelper.CopyAddress((IAddress) copy1, shippingAddress);
    copy1.CustomerID = customerId1;
    copy1.CustomerAddressID = customerAddressId;
    copy1.RevisionID = new int?(0);
    copy1.IsDefaultAddress = new bool?(false);
    ((PXSelectBase<ARShippingAddress>) this.Base.Shipping_Address).Update(copy1);
  }

  private void EvaluateComponentLineNumbers(PXCache cache, PX.Objects.AR.ARInvoice arInvoiceRow)
  {
    PXCache arTranCache = cache.Graph.Caches[typeof (PX.Objects.AR.ARTran)];
    IEnumerable<PX.Objects.AR.ARTran> source = GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Select(new object[2]
    {
      (object) arInvoiceRow.DocType,
      (object) arInvoiceRow.RefNbr
    }));
    foreach (PX.Objects.AR.ARTran arTran1 in source.Where<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (t => arTranCache.GetExtension<FSxARTran>((object) t).EquipmentAction == "CC" && !arTranCache.GetExtension<FSxARTran>((object) t).NewEquipmentLineNbr.HasValue)))
    {
      if (SM_ARInvoiceEntry.SOExists(arTran1))
      {
        FSxSOLine fsCompSOLine = PXCache<PX.Objects.SO.SOLine>.GetExtension<FSxSOLine>(PX.Objects.SO.SOLine.PK.Find(cache.Graph, arTran1.SOOrderType, arTran1.SOOrderNbr, arTran1.SOOrderLineNbr));
        PX.Objects.AR.ARTran arTran2 = source.FirstOrDefault<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (t =>
        {
          int? soOrderLineNbr = t.SOOrderLineNbr;
          int? equipmentLineNbr = fsCompSOLine.NewEquipmentLineNbr;
          return soOrderLineNbr.GetValueOrDefault() == equipmentLineNbr.GetValueOrDefault() & soOrderLineNbr.HasValue == equipmentLineNbr.HasValue;
        }));
        if (arTran2 != null)
        {
          arTranCache.GetExtension<FSxARTran>((object) arTran1).NewEquipmentLineNbr = arTran2.LineNbr;
          arTranCache.Persist((object) arTran1, (PXDBOperation) 1);
        }
      }
    }
  }

  private static bool SOExists(PX.Objects.AR.ARTran arTran)
  {
    return arTran.SOOrderType != null && arTran.SOOrderNbr != null && arTran.SOOrderLineNbr.HasValue;
  }
}
