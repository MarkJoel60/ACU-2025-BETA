// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_SOInvoiceEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_SOInvoiceEntry : FSPostingBase<SOInvoiceEntry>
{
  [PXHidden]
  public PXSelect<FSBillHistory> BillHistoryRecords;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  public virtual bool IsFSIntegrationEnabled()
  {
    if (!SM_SOInvoiceEntry.IsActive())
      return false;
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    FSxARInvoice extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<FSxARInvoice>((object) current);
    return SM_ARInvoiceEntry.IsFSIntegrationEnabled(current, extension);
  }

  [PXOverride]
  public virtual IEnumerable Release(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseMethod)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXGraph.InstanceCreated.AddHandler<ARReleaseProcess>(SM_SOInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (SM_SOInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXGraph.InstanceCreatedDelegate<ARReleaseProcess>((object) SM_SOInvoiceEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRelease\u003Eb__3_0))));
    }
    return baseMethod(adapter);
  }

  [PXOverride]
  public virtual void NonTransferApplicationQuery(
    PXSelectBase<ARPayment> cmd,
    SM_SOInvoiceEntry.NonTransferApplicationQueryDelegate baseMethod)
  {
    cmd.Join<LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARPayment.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARPayment.refNbr>, And<ARAdjust2.adjNbr, Equal<ARPayment.adjCntr>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.hold, Equal<True>, And<ARAdjust2.voided, Equal<False>, And<Where<ARAdjust2.adjdDocType, NotEqual<Current<PX.Objects.AR.ARInvoice.docType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>>>>>>>>>();
    cmd.Join<LeftJoin<FSAdjust, On<FSAdjust.adjgDocType, Equal<ARPayment.docType>, And<FSAdjust.adjgRefNbr, Equal<ARPayment.refNbr>>>>>();
    cmd.Join<LeftJoin<SOAdjust, On<SOAdjust.adjgDocType, Equal<ARPayment.docType>, And<SOAdjust.adjgRefNbr, Equal<ARPayment.refNbr>, And<SOAdjust.adjAmt, Greater<decimal0>>>>>>();
    cmd.WhereAnd<Where<ARPayment.finPeriodID, LessEqual<Current<PX.Objects.AR.ARInvoice.finPeriodID>>, And2<Where2<Where<ARPayment.released, Equal<True>>, Or<Where<FSAdjust.adjdOrderType, IsNotNull, And<FSAdjust.adjdOrderNbr, IsNotNull>>>>, And<ARAdjust2.adjdRefNbr, IsNull, And<SOAdjust.adjgRefNbr, IsNull>>>>>();
  }

  public virtual string GetLineDisplayHint(
    PXGraph graph,
    string lineRefNbr,
    string lineDescr,
    int? inventoryID)
  {
    return MessageHelper.GetLineDisplayHint(graph, lineRefNbr, lineDescr, inventoryID);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM>>) e).Args.NewValue != null)
      return;
    if (!e.Row.InventoryID.HasValue)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM>, PX.Objects.AR.ARTran, object>) e).NewValue = (object) null;
    }
    else
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM>, PX.Objects.AR.ARTran, object>) e).NewValue = SharedFunctions.IsLotSerialRequired(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.uOM>>) e).Cache, e.Row.InventoryID) ? (object) inventoryItem?.BaseUnit : (object) inventoryItem?.SalesUnit;
    }
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
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.ARInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOInvoice> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.SO.SOInvoice> e)
  {
    if (e.Row == null || e.TranStatus != null || e.Operation != 3)
      return;
    FSAllocationProcess.ReallocateServiceOrderSplits(FSAllocationProcess.GetRequiredAllocationList((PXGraph) this.Base, (object) e.Row));
    this.CleanPostingInfoLinkedToDoc((object) e.Row);
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
  }

  protected virtual void _(PX.Data.Events.RowUpdating<PX.Objects.AR.ARTran> e)
  {
    if (e.Row != null && ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current != null && !(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType != "CRM") && ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<PX.Objects.AR.ARTran>>) e).Cache.GetExtension<FSxARTran>((object) e.Row).IsFSRelated.GetValueOrDefault() && ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<PX.Objects.AR.ARTran>>) e).Cache.GetStatus((object) e.Row) != 2 && !((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<PX.Objects.AR.ARTran>>) e).Cache.ObjectsEqualExceptFields<PX.Objects.AR.ARTran.accountID, PX.Objects.AR.ARTran.subID>((object) e.Row, (object) e.NewRow))
      throw new PXSetPropertyException("The credit memo line cannot be updated because it is related to a field service document.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.AR.ARTran> e)
  {
    if (e.Row != null && ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current != null && !(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType != "CRM") && ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current) != 3 && ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<PX.Objects.AR.ARTran>>) e).Cache.GetExtension<FSxARTran>((object) e.Row).IsFSRelated.GetValueOrDefault())
      throw new PXSetPropertyException("The credit memo line cannot be deleted because it is related to a field service document.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.AR.ARTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.ARTran> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.SO.SOInvoice current = ((PXSelectBase<PX.Objects.SO.SOInvoice>) this.Base.SODocument).Current;
    if (e.TranStatus == null && e.Operation == 2 && current.DocType != "CRM")
    {
      FSxARTran extension = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.AR.ARTran>>) e).Cache.GetExtension<FSxARTran>((object) e.Row);
      FSBillHistory historyRowsFromArTran = this.CreateBillHistoryRowsFromARTran(((PXSelectBase) this.BillHistoryRecords).Cache, e.Row, extension, "PXSI", current.DocType, current.RefNbr, "PXSO", (string) null, (string) null, true);
      if (historyRowsFromArTran != null)
      {
        int? recordId = historyRowsFromArTran.RecordID;
        int num = 0;
        if (recordId.GetValueOrDefault() < num & recordId.HasValue)
          ((PXSelectBase) this.BillHistoryRecords).Cache.Persist((PXDBOperation) 2);
      }
    }
    if (e.TranStatus != 2 || !this.IsInvoiceProcessRunning)
      return;
    MessageHelper.GetRowMessage(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<PX.Objects.AR.ARTran>>) e).Cache, (IBqlTable) e.Row, false, false);
  }

  public override List<MessageHelper.ErrorInfo> GetErrorInfo()
  {
    return MessageHelper.GetErrorInfo<PX.Objects.AR.ARTran>(((PXSelectBase) this.Base.Document).Cache, (IBqlTable) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, (PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions, typeof (PX.Objects.AR.ARTran));
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
    SM_SOInvoiceEntry.\u003C\u003Ec__DisplayClass39_0 cDisplayClass390 = new SM_SOInvoiceEntry.\u003C\u003Ec__DisplayClass39_0();
    if (docLines.Count == 0)
      return;
    bool? nullable1 = new bool?(false);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.fsServiceOrderRow = docLines[0].fsServiceOrder;
    FSSrvOrdType fsSrvOrdType = docLines[0].fsSrvOrdType;
    FSPostDoc fsPostDoc = docLines[0].fsPostDoc;
    FSAppointment fsAppointment = docLines[0].fsAppointment;
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass390, __methodptr(\u003CCreateInvoice\u003Eb__0));
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
      arInvoice1.InvoiceNbr = cDisplayClass390.fsServiceOrderRow.CustPORefNbr;
      PX.Objects.AR.ARInvoice data1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Insert(arInvoice1);
      bool? hold = data1.Hold;
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.hold>((object) data1, (object) true);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.customerID>((object) data1, (object) cDisplayClass390.fsServiceOrderRow.BillCustomerID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.customerLocationID>((object) data1, (object) cDisplayClass390.fsServiceOrderRow.BillLocationID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.curyID>((object) data1, (object) cDisplayClass390.fsServiceOrderRow.CuryID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.taxZoneID>((object) data1, fsAppointment != null ? (object) fsAppointment.TaxZoneID : (object) cDisplayClass390.fsServiceOrderRow.TaxZoneID);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.taxCalcMode>((object) data1, fsAppointment != null ? (object) fsAppointment.TaxCalcMode : (object) cDisplayClass390.fsServiceOrderRow.TaxCalcMode);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.externalTaxExemptionNumber>((object) data1, fsAppointment != null ? (object) fsAppointment.ExternalTaxExemptionNumber : (object) cDisplayClass390.fsServiceOrderRow.ExternalTaxExemptionNumber);
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.avalaraCustomerUsageType>((object) data1, fsAppointment != null ? (object) fsAppointment.EntityUsageType : (object) cDisplayClass390.fsServiceOrderRow.EntityUsageType);
      // ISSUE: reference to a compiler-generated field
      string customerOrVendor = this.GetTermsIDFromCustomerOrVendor(graphProcess, cDisplayClass390.fsServiceOrderRow.BillCustomerID, new int?());
      if ((!(data1.DocType == "CRM") ? 1 : (((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.TermsInCreditMemos.GetValueOrDefault() ? 1 : 0)) != 0)
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.termsID>((object) data1, (object) (customerOrVendor ?? fsSrvOrdType.DfltTermIDARSO));
      // ISSUE: reference to a compiler-generated field
      int? nullable2 = cDisplayClass390.fsServiceOrderRow.ProjectID;
      if (nullable2.HasValue)
      {
        // ISSUE: reference to a compiler-generated field
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.projectID>((object) data1, (object) cDisplayClass390.fsServiceOrderRow.ProjectID);
      }
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.docDesc>((object) data1, (object) cDisplayClass390.fsServiceOrderRow.DocDesc);
      data1.FinPeriodID = invoiceFinPeriodID;
      nullable2 = (int?) fsAppointment?.BranchID;
      // ISSUE: reference to a compiler-generated field
      int? nullable3 = (int?) (nullable2 ?? cDisplayClass390.fsServiceOrderRow?.BranchID);
      if (nullable3.HasValue)
      {
        nullable2 = data1.BranchID;
        int? nullable4 = nullable3;
        if (!(nullable2.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue))
          data1.BranchID = nullable3;
      }
      PX.Objects.AR.ARInvoice arInvoice2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Update(data1);
      // ISSUE: reference to a compiler-generated field
      this.SetContactAndAddress((PXGraph) this.Base, cDisplayClass390.fsServiceOrderRow);
      if (onDocumentHeaderInserted != null)
        onDocumentHeaderInserted((PXGraph) this.Base, (IBqlTable) arInvoice2);
      List<SharedClasses.SOARLineEquipmentComponent> equipmentComponentList = new List<SharedClasses.SOARLineEquipmentComponent>();
      FSSODet fssoDet = (FSSODet) null;
      FSAppointmentDet fsAppointmentDet = (FSAppointmentDet) null;
      PX.Objects.AR.ARTran Row = (PX.Objects.AR.ARTran) null;
      foreach (DocLineExt docLine in docLines)
      {
        bool firstInADoc = docLine.fsAppointment == null ? fssoDet == null || fssoDet.RefNbr != docLine.fsServiceOrder.RefNbr : fsAppointmentDet == null || fsAppointmentDet.RefNbr != docLine.fsAppointment.RefNbr;
        fssoDet = docLine.fsSODet;
        fsAppointmentDet = docLine.fsAppointmentDet;
        if (SharedFunctions.IsLotSerialRequired(((PXSelectBase) this.Base.Transactions).Cache, docLine.docLine.InventoryID))
        {
          bool flag1 = false;
          Decimal? nullable5 = new Decimal?(0M);
          Decimal? nullable6;
          if (fsAppointment == null)
          {
            foreach (FSSODetSplit fssoDetSplit in GraphHelper.RowCast<FSSODetSplit>((IEnumerable) PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>>>>, OrderBy<Asc<FSSODetSplit.splitLineNbr>>>.Config>.Select((PXGraph) this.Base, new object[3]
            {
              (object) fssoDet.SrvOrdType,
              (object) fssoDet.RefNbr,
              (object) fssoDet.LineNbr
            })).Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (x =>
            {
              bool? poCreate = x.POCreate;
              bool flag2 = false;
              return poCreate.GetValueOrDefault() == flag2 & poCreate.HasValue && !string.IsNullOrEmpty(x.LotSerialNbr);
            })))
            {
              Row = this.InsertSOInvoiceLine(graphProcess, arInvoice2, docLine, new int?((int) invtMult), fssoDetSplit.Qty, fssoDetSplit.UOM, fssoDetSplit.SiteID, fssoDetSplit.LocationID, fssoDetSplit.CostCenterID, fssoDetSplit.LotSerialNbr, onTransactionInserted, equipmentComponentList, firstInADoc);
              flag1 = true;
              nullable6 = nullable5;
              Decimal? qty = Row.Qty;
              nullable5 = nullable6.HasValue & qty.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + qty.GetValueOrDefault()) : new Decimal?();
            }
          }
          else if (fsAppointment != null)
          {
            foreach (FSApptLineSplit fsApptLineSplit in GraphHelper.RowCast<FSApptLineSplit>((IEnumerable) PXSelectBase<FSApptLineSplit, PXSelect<FSApptLineSplit, Where<FSApptLineSplit.srvOrdType, Equal<Required<FSApptLineSplit.srvOrdType>>, And<FSApptLineSplit.apptNbr, Equal<Required<FSApptLineSplit.apptNbr>>, And<FSApptLineSplit.lineNbr, Equal<Required<FSApptLineSplit.lineNbr>>>>>, OrderBy<Asc<FSApptLineSplit.splitLineNbr>>>.Config>.Select((PXGraph) this.Base, new object[3]
            {
              (object) fsAppointmentDet.SrvOrdType,
              (object) fsAppointmentDet.RefNbr,
              (object) fsAppointmentDet.LineNbr
            })).Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (x => !string.IsNullOrEmpty(x.LotSerialNbr))))
            {
              Row = this.InsertSOInvoiceLine(graphProcess, arInvoice2, docLine, new int?((int) invtMult), fsApptLineSplit.Qty, fsApptLineSplit.UOM, fsApptLineSplit.SiteID, fsApptLineSplit.LocationID, fsApptLineSplit.CostCenterID, fsApptLineSplit.LotSerialNbr, onTransactionInserted, equipmentComponentList, firstInADoc);
              flag1 = true;
              Decimal? nullable7 = nullable5;
              nullable6 = Row.Qty;
              nullable5 = nullable7.HasValue & nullable6.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
            }
          }
          if (!flag1)
          {
            Row = this.InsertSOInvoiceLine(graphProcess, arInvoice2, docLine, new int?((int) invtMult), docLine.docLine.GetQty(FieldType.BillableField), docLine.docLine.UOM, docLine.docLine.SiteID, docLine.docLine.SiteLocationID, docLine.docLine.CostCenterID, docLine.docLine.LotSerialNbr, onTransactionInserted, equipmentComponentList, firstInADoc);
          }
          else
          {
            Decimal num = INUnitAttribute.ConvertFromTo<PX.Objects.AR.ARTran.inventoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) Row, Row.UOM, docLine.docLine.UOM, nullable5.GetValueOrDefault(), INPrecision.QUANTITY);
            nullable6 = docLine.docLine.GetQty(FieldType.BillableField);
            Decimal valueOrDefault = nullable6.GetValueOrDefault();
            if (!(num == valueOrDefault & nullable6.HasValue))
              throw new PXException("The quantity in the posted document does not match the quantity in the source document.");
          }
        }
        else
          Row = this.InsertSOInvoiceLine(graphProcess, arInvoice2, docLine, new int?((int) invtMult), docLine.docLine.GetQty(FieldType.BillableField), docLine.docLine.UOM, docLine.docLine.SiteID, docLine.docLine.SiteLocationID, docLine.docLine.CostCenterID, docLine.docLine.LotSerialNbr, onTransactionInserted, equipmentComponentList, firstInADoc);
      }
      if (equipmentComponentList.Count > 0)
      {
        foreach (SharedClasses.SOARLineEquipmentComponent equipmentComponent1 in equipmentComponentList.Where<SharedClasses.SOARLineEquipmentComponent>((Func<SharedClasses.SOARLineEquipmentComponent, bool>) (x => x.equipmentAction == "ST")))
        {
          foreach (SharedClasses.SOARLineEquipmentComponent equipmentComponent2 in equipmentComponentList.Where<SharedClasses.SOARLineEquipmentComponent>((Func<SharedClasses.SOARLineEquipmentComponent, bool>) (x => x.equipmentAction == "CC" || x.equipmentAction == "UC" || x.equipmentAction == "NO")))
          {
            if (equipmentComponent2.sourceNewTargetEquipmentLineNbr == equipmentComponent1.sourceLineRef)
            {
              equipmentComponent2.fsxARTranRow.ComponentID = equipmentComponent2.componentID;
              equipmentComponent2.fsxARTranRow.NewEquipmentLineNbr = equipmentComponent1.currentLineRef;
              ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(equipmentComponent2.arTranRow);
            }
          }
        }
      }
      PX.Objects.AR.ARInvoice data2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Update(arInvoice2);
      if (((PXSelectBase<ARSetup>) this.Base.ARSetup).Current.RequireControlTotal.GetValueOrDefault())
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) data2, (object) data2.CuryDocBal);
      if (!hold.GetValueOrDefault() || quickProcessFlow != null)
        ((PXSelectBase) this.Base.Document).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARInvoice.hold>((object) data2, (object) false);
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Update(data2);
    }
    finally
    {
      ((PXGraph) this.Base).FieldDefaulting.RemoveHandler<PX.Objects.AR.ARInvoice.branchID>(pxFieldDefaulting);
    }
  }

  public virtual PX.Objects.AR.ARTran InsertSOInvoiceLine(
    PXGraph graphProcess,
    PX.Objects.AR.ARInvoice arInvoiceRow,
    DocLineExt docLineExt,
    int? invtMult,
    Decimal? qty,
    string UOM,
    int? siteID,
    int? locationID,
    int? costCenterID,
    string lotSerialNbr,
    OnTransactionInsertedDelegate onTransactionInserted,
    List<SharedClasses.SOARLineEquipmentComponent> componentList,
    bool firstInADoc)
  {
    IDocLine docLine = docLineExt.docLine;
    FSPostDoc fsPostDoc = docLineExt.fsPostDoc;
    FSServiceOrder fsServiceOrder = docLineExt.fsServiceOrder;
    FSSrvOrdType fsSrvOrdType = docLineExt.fsSrvOrdType;
    FSAppointment fsAppointment = docLineExt.fsAppointment;
    FSAppointmentDet fsAppointmentDet = docLineExt.fsAppointmentDet;
    FSSODet fsSoDet = docLineExt.fsSODet;
    PX.Objects.AR.ARTran copy = (PX.Objects.AR.ARTran) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Insert(new PX.Objects.AR.ARTran()));
    copy.BranchID = docLine.BranchID;
    copy.InventoryID = docLine.InventoryID;
    PMTask pmTask = docLineExt.pmTask;
    if (pmTask != null && pmTask.Status == "F")
      throw new PXException("The {1} line of the {0} document cannot be processed because the {2} project task has already been completed.", new object[3]
      {
        (object) fsServiceOrder.RefNbr,
        (object) docLine.LineRef,
        (object) pmTask.TaskCD
      });
    if (docLine.ProjectID.HasValue && docLine.ProjectTaskID.HasValue)
      copy.TaskID = docLine.ProjectTaskID;
    copy.UOM = UOM;
    copy.SiteID = siteID;
    copy.Qty = new Decimal?(0M);
    PX.Objects.AR.ARTran data = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(copy);
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.locationID>((object) data, (object) locationID);
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.costCenterID>((object) data, (object) costCenterID);
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.lotSerialNbr>((object) data, (object) lotSerialNbr);
    PX.Objects.AR.ARTran arTran1 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(data);
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.qty>((object) arTran1, (object) qty);
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.tranDesc>((object) arTran1, (object) docLine.TranDesc);
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.salesPersonID>((object) arTran1, (object) (int?) fsServiceOrder?.SalesPersonID);
    int? newValue1 = !docLine.AcctID.HasValue ? this.Get_TranAcctID_DefaultValue(graphProcess, fsSrvOrdType.SalesAcctSource, docLine.InventoryID, docLine.SiteID, fsServiceOrder) : docLine.AcctID;
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.accountID>((object) arTran1, (object) newValue1);
    if (docLine.SubID.HasValue)
    {
      try
      {
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.subID>((object) arTran1, (object) docLine.SubID);
        ((PXSelectBase) this.Base.Transactions).Cache.SetDefaultExt<PX.Objects.AR.ARTran.expenseSubID>((object) arTran1);
      }
      catch (PXException ex)
      {
        arTran1.SubID = new int?();
      }
    }
    else
      this.SetCombinedSubID(graphProcess, ((PXSelectBase) this.Base.Transactions).Cache, arTran1, (APTran) null, (PX.Objects.SO.SOLine) null, fsSrvOrdType, arTran1.BranchID, arTran1.InventoryID, arInvoiceRow.CustomerLocationID, fsServiceOrder.BranchLocationID, fsServiceOrder.SalesPersonID, docLine.IsService);
    Decimal? nullable1;
    if (docLine.IsFree.GetValueOrDefault())
    {
      ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualPrice>((object) arTran1, (object) true);
      ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyUnitPrice>((object) arTran1, (object) 0M);
      ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualDisc>((object) arTran1, (object) true);
      ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.discPct>((object) arTran1, (object) 0M);
    }
    else
    {
      ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualPrice>((object) arTran1, (object) docLine.ManualPrice);
      Decimal? curyUnitPrice = docLine.CuryUnitPrice;
      int? nullable2 = invtMult;
      nullable1 = nullable2.HasValue ? new Decimal?((Decimal) nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal valueOrDefault = (curyUnitPrice.HasValue & nullable1.HasValue ? new Decimal?(curyUnitPrice.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      if (arTran1.UOM != docLine.UOM)
        valueOrDefault = INUnitAttribute.ConvertFromTo<PX.Objects.AR.ARTran.inventoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) arTran1, arTran1.UOM, docLine.UOM, valueOrDefault, INPrecision.UNITCOST);
      ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyUnitPrice>((object) arTran1, (object) valueOrDefault);
      bool newValue2 = false;
      if (docLineExt.fsAppointmentDet != null)
        newValue2 = docLineExt.fsAppointmentDet.ManualDisc.Value;
      else if (docLineExt.fsSODet != null)
        newValue2 = docLineExt.fsSODet.ManualDisc.Value;
      ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.manualDisc>((object) arTran1, (object) newValue2);
      if (newValue2)
        ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.discPct>((object) arTran1, (object) docLine.DiscPct);
    }
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.taxCategoryID>((object) arTran1, (object) docLine.TaxCategoryID);
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.commissionable>((object) arTran1, (object) fsServiceOrder.Commissionable.GetValueOrDefault());
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.costCodeID>((object) arTran1, (object) docLine.CostCodeID);
    Decimal valueOrDefault1 = arTran1.Qty.GetValueOrDefault();
    if (arTran1.UOM != docLine.UOM)
      valueOrDefault1 = INUnitAttribute.ConvertFromTo<PX.Objects.AR.ARTran.inventoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) arTran1, arTran1.UOM, docLine.UOM, valueOrDefault1, INPrecision.NOROUND);
    Decimal? qty1 = docLine.GetQty(FieldType.BillableField);
    Decimal num1 = 0M;
    int? nullable3;
    Decimal? nullable4;
    if (qty1.GetValueOrDefault() == num1 & qty1.HasValue)
    {
      nullable4 = new Decimal?(0M);
    }
    else
    {
      Decimal? billableExtPrice = docLine.CuryBillableExtPrice;
      nullable3 = invtMult;
      Decimal? nullable5 = nullable3.HasValue ? new Decimal?((Decimal) nullable3.GetValueOrDefault()) : new Decimal?();
      nullable1 = billableExtPrice.HasValue & nullable5.HasValue ? new Decimal?(billableExtPrice.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable6 = docLine.GetQty(FieldType.BillableField);
      Decimal? nullable7;
      if (!(nullable1.HasValue & nullable6.HasValue))
      {
        nullable5 = new Decimal?();
        nullable7 = nullable5;
      }
      else
        nullable7 = new Decimal?(nullable1.GetValueOrDefault() / nullable6.GetValueOrDefault());
      Decimal? nullable8 = nullable7;
      Decimal num2 = valueOrDefault1;
      if (!nullable8.HasValue)
      {
        nullable6 = new Decimal?();
        nullable4 = nullable6;
      }
      else
        nullable4 = new Decimal?(nullable8.GetValueOrDefault() * num2);
    }
    Decimal? newValue3 = nullable4;
    newValue3 = new Decimal?(PXCurrencyAttribute.Round(((PXSelectBase) this.Base.Transactions).Cache, (object) arTran1, newValue3.GetValueOrDefault(), CMPrecision.TRANCURY));
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExtIfDifferent<PX.Objects.AR.ARTran.curyExtPrice>((object) arTran1, (object) newValue3);
    FSxARTran extension = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<FSxARTran>((object) arTran1);
    extension.SrvOrdType = fsServiceOrder.SrvOrdType;
    extension.ServiceOrderRefNbr = fsServiceOrder.RefNbr;
    extension.AppointmentRefNbr = fsAppointment?.RefNbr;
    FSxARTran fsxArTran1 = extension;
    int? nullable9;
    if (fsSoDet == null)
    {
      nullable3 = new int?();
      nullable9 = nullable3;
    }
    else
      nullable9 = fsSoDet.LineNbr;
    fsxArTran1.ServiceOrderLineNbr = nullable9;
    FSxARTran fsxArTran2 = extension;
    int? nullable10;
    if (fsAppointmentDet == null)
    {
      nullable3 = new int?();
      nullable10 = nullable3;
    }
    else
      nullable10 = fsAppointmentDet.LineNbr;
    fsxArTran2.AppointmentLineNbr = nullable10;
    PX.Objects.AR.ARTran lineDocument = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(arTran1);
    if (firstInADoc)
    {
      if (fsAppointment == null)
        SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSServiceOrder)], ((PXSelectBase) this.Base.Document).Cache, (object) fsServiceOrder, (object) arInvoiceRow, fsSrvOrdType.CopyNotesToInvoice, fsSrvOrdType.CopyAttachmentsToInvoice);
      else
        SharedFunctions.CopyNotesAndFiles(((PXGraph) this.Base).Caches[typeof (FSAppointment)], ((PXSelectBase) this.Base.Document).Cache, (object) fsAppointment, (object) arInvoiceRow, fsSrvOrdType.CopyNotesToInvoice, fsSrvOrdType.CopyAttachmentsToInvoice);
    }
    SharedFunctions.CopyNotesAndFiles(((PXSelectBase) this.Base.Transactions).Cache, (object) lineDocument, docLine, fsSrvOrdType);
    PX.Objects.AR.ARTran arTran2;
    fsPostDoc.DocLineRef = (object) (arTran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(lineDocument));
    if (PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>())
    {
      if (docLine.EquipmentAction != null)
      {
        extension.EquipmentAction = docLine.EquipmentAction;
        extension.SMEquipmentID = docLine.SMEquipmentID;
        extension.EquipmentComponentLineNbr = docLine.EquipmentLineRef;
        extension.Comment = docLine.Comment;
        if (docLine.EquipmentAction == "ST" || (docLine.EquipmentAction == "CC" || docLine.EquipmentAction == "UC" || docLine.EquipmentAction == "NO") && !string.IsNullOrEmpty(docLine.NewTargetEquipmentLineNbr))
        {
          componentList.Add(new SharedClasses.SOARLineEquipmentComponent(docLine, arTran2, extension));
        }
        else
        {
          extension.ComponentID = docLine.ComponentID;
          arTran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(arTran2);
        }
      }
    }
    else
      arTran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Update(arTran2);
    if (onTransactionInserted != null)
      onTransactionInserted((PXGraph) this.Base, (IBqlTable) arTran2);
    return arTran2;
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
    ((PXGraph) this.Base).SelectTimeStamp();
    ((PXAction) this.Base.Save).Press();
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current;
    return new FSCreatedDoc()
    {
      BatchID = new int?(batchID),
      PostTo = "SI",
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
    PXUpdate<Set<FSPostInfo.sOInvLineNbr, Null, Set<FSPostInfo.sOInvRefNbr, Null, Set<FSPostInfo.sOInvDocType, Null, Set<FSPostInfo.sOInvPosted, False>>>>, FSPostInfo, Where<FSPostInfo.postID, Equal<Required<FSPostInfo.postID>>, And<FSPostInfo.sOInvPosted, Equal<True>>>>.Update(cleanerGraph, new object[1]
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
    return ((IQueryable<PXResult<FSPostInfo>>) PXSelectBase<FSPostInfo, PXSelect<FSPostInfo, Where<FSPostInfo.sOInvRefNbr, Equal<Required<FSPostInfo.sOInvRefNbr>>, And<FSPostInfo.sOInvDocType, Equal<Required<FSPostInfo.sOInvDocType>>, And<FSPostInfo.sOInvLineNbr, Equal<Required<FSPostInfo.sOInvLineNbr>>, And<FSPostInfo.sOInvPosted, Equal<True>>>>>>.Config>.Select(cleanerGraph, new object[3]
    {
      (object) refNbr,
      (object) docType,
      (object) lineNbr
    })).Count<PXResult<FSPostInfo>>() > 0;
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

  public delegate void NonTransferApplicationQueryDelegate(PXSelectBase<ARPayment> cmd);
}
