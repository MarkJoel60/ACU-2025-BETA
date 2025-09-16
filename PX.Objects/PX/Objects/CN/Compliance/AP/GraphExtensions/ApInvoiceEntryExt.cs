// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AP.GraphExtensions.ApInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.Compliance.AP.CacheExtensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.AP.GraphExtensions;

public class ApInvoiceEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<APInvoiceEntry, PX.Objects.AP.APInvoice>, APInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.billID>>>, Where<ComplianceDocumentReference.type, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  public PXSelect<ComplianceDocumentReference> DocumentReference;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService service;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && !SiteMapExtension.IsTaxBillsAndAdjustmentsScreenId();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.ConfigureComplianceGridColumnsForApBill(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  public IEnumerable complianceDocuments()
  {
    List<ComplianceDocument> list = this.GetComplianceDocuments().ToList<ComplianceDocument>();
    this.service.ValidateComplianceDocuments((PXCache) null, (IEnumerable<ComplianceDocument>) list, ((PXSelectBase) this.ComplianceDocuments).Cache);
    return (IEnumerable) list;
  }

  public virtual void _(PX.Data.Events.RowUpdated<ComplianceDocument> args)
  {
    ((PXSelectBase) this.ComplianceDocuments).View.RequestRefresh();
  }

  protected void APInvoice_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs args,
    PXRowSelected baseHandler)
  {
    if (!(args.Row is PX.Objects.AP.APInvoice row))
      return;
    this.service.ValidateRelatedField<PX.Objects.AP.APInvoice, ComplianceDocument.vendorID, PX.Objects.AP.APInvoice.vendorID>(row, (object) row.VendorID);
    baseHandler.Invoke(cache, args);
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Cache.Inserted);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AP.APInvoice> args)
  {
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice> args)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APInvoice>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AP.APInvoice> args)
  {
    if (args.Row == null)
      return;
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    ComplianceAttributeType complianceAttributeType = PXResultset<ComplianceAttributeType>.op_Implicit(PXSelectBase<ComplianceAttributeType, PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<Required<ComplianceAttributeType.type>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[1]
    {
      (object) "Lien Waiver"
    }));
    foreach (ComplianceDocument complianceDocument in complianceDocuments)
    {
      int? documentType = complianceDocument.DocumentType;
      int? complianceAttributeTypeId = complianceAttributeType.ComplianceAttributeTypeID;
      if (documentType.GetValueOrDefault() == complianceAttributeTypeId.GetValueOrDefault() & documentType.HasValue == complianceAttributeTypeId.HasValue && !complianceDocument.IsCreatedAutomatically.GetValueOrDefault())
      {
        ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Delete(complianceDocument);
      }
      else
      {
        complianceDocument.BillID = new Guid?();
        ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
      }
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AP.APInvoice.curyOrigDocAmt> args)
  {
    PX.Objects.AP.APInvoice bill = args.Row as PX.Objects.AP.APInvoice;
    if (bill == null)
      return;
    foreach (ComplianceDocument complianceDocument in ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>()).FirstTableItems.Where<ComplianceDocument>((Func<ComplianceDocument, bool>) (x =>
    {
      Guid? invoiceId = x.InvoiceID;
      Guid? documentReferenceId = this.GetComplianceDocumentReferenceId(bill);
      if (invoiceId.HasValue != documentReferenceId.HasValue)
        return false;
      return !invoiceId.HasValue || invoiceId.GetValueOrDefault() == documentReferenceId.GetValueOrDefault();
    })).ToList<ComplianceDocument>())
    {
      complianceDocument.BillAmount = bill.OrigDocAmt;
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current;
    ComplianceDocument row = args.Row;
    if (current == null || row == null)
      return;
    PX.Objects.AP.APTran transaction = ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Select(Array.Empty<object>()).FirstTableItems.OrderBy<PX.Objects.AP.APTran, int?>((Func<PX.Objects.AP.APTran, int?>) (x => x.LineNbr)).FirstOrDefault<PX.Objects.AP.APTran>();
    this.FillInvoiceInfo(row, current, transaction);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    this.service.UpdateExpirationIndicator(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APTran> args)
  {
    if (args.Row == null)
      return;
    this.ValidateTransaction(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<APInvoiceEntry.APAdjust> args)
  {
    if (args.Row == null)
      return;
    this.ValidateAdjustment(args.Row);
  }

  private void FillInvoiceInfo(
    ComplianceDocument complianceDocument,
    PX.Objects.AP.APInvoice invoice,
    PX.Objects.AP.APTran transaction)
  {
    complianceDocument.VendorID = invoice.VendorID;
    complianceDocument.VendorName = this.GetVendorName(complianceDocument.VendorID);
    ComplianceDocumentRefNoteAttribute.SetComplianceDocumentReference<ComplianceDocument.billID>(((PXSelectBase) this.ComplianceDocuments).Cache, complianceDocument, invoice.DocType, invoice.RefNbr, invoice.NoteID);
    complianceDocument.BillAmount = invoice.OrigDocAmt;
    complianceDocument.AccountID = this.GetAccountId();
    complianceDocument.ProjectID = (int?) transaction?.ProjectID;
    complianceDocument.CostTaskID = (int?) transaction?.TaskID;
    complianceDocument.CostCodeID = (int?) transaction?.CostCodeID;
  }

  private void ValidateAdjustment(APInvoiceEntry.APAdjust adjustment)
  {
    Guid? documentReferenceId = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReferenceId((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, (APAdjust) adjustment);
    bool rowHasExpiredCompliance = this.service.ValidateRelatedField<APInvoiceEntry.APAdjust, ComplianceDocument.apCheckId, APAdjust.displayRefNbr>(adjustment, (object) documentReferenceId);
    this.service.ValidateRelatedRow<APInvoiceEntry.APAdjust, ApAdjustExt.hasExpiredComplianceDocuments>(adjustment, rowHasExpiredCompliance);
  }

  private void ValidateTransaction(PX.Objects.AP.APTran transaction)
  {
    bool flag1 = this.service.ValidateProjectVendorRelatedField<PX.Objects.AP.APTran, PX.Objects.AP.APTran.projectID>(transaction, transaction.ProjectID, transaction.VendorID);
    bool flag2 = this.service.ValidateRelatedField<PX.Objects.AP.APTran, ComplianceDocument.costTaskID, PX.Objects.AP.APTran.taskID>(transaction, (object) transaction.TaskID);
    bool flag3 = this.IsSubcontract(transaction) ? this.service.ValidateRelatedField<PX.Objects.AP.APTran, ComplianceDocument.subcontract, PX.Objects.CN.Subcontracts.AP.CacheExtensions.ApTranExt.subcontractNbr>(transaction, (object) transaction.PONbr) : this.service.ValidateRelatedField<PX.Objects.AP.APTran, ComplianceDocument.purchaseOrder, PX.Objects.AP.APTran.pONbr>(transaction, (object) ComplianceDocumentReferenceRetriever.GetComplianceDocumentReferenceId((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, transaction));
    this.service.ValidateRelatedRow<PX.Objects.AP.APTran, PX.Objects.CN.Compliance.AP.CacheExtensions.ApTranExt.hasExpiredComplianceDocuments>(transaction, flag1 | flag2 | flag3);
  }

  private int? GetAccountId()
  {
    return ((PXSelectBase<PX.Objects.AP.APTran>) new PXSelect<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.refNbr, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>>, OrderBy<Asc<PX.Objects.AP.APTran.lineNbr>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base)).SelectSingle(Array.Empty<object>())?.AccountID;
  }

  private string GetVendorName(int? vendorId)
  {
    if (!vendorId.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[1]
    {
      (object) vendorId
    }))?.AcctName;
  }

  private bool IsSubcontract(PX.Objects.AP.APTran apTran)
  {
    PX.Objects.PO.POOrder purchaseOrder = this.GetPurchaseOrder(apTran);
    return purchaseOrder != null && purchaseOrder.OrderType == "RS";
  }

  private PX.Objects.PO.POOrder GetPurchaseOrder(PX.Objects.AP.APTran apTran)
  {
    return ((PXSelectBase<PX.Objects.PO.POOrder>) new PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base)).SelectSingle(new object[2]
    {
      (object) apTran.POOrderType,
      (object) apTran.PONbr
    });
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    List<ComplianceDocument> list = ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.billID>>, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>>, Where<ComplianceDocumentReference.type, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base)).Select(new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.DocType,
      (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Document).Current.RefNbr
    }).FirstTableItems.ToList<ComplianceDocument>();
    foreach (PXResult<ComplianceDocumentBill> pxResult in ((PXSelectBase<ComplianceDocumentBill>) new PXSelect<ComplianceDocumentBill, Where<ComplianceDocumentBill.docType, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<ComplianceDocumentBill.refNbr, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>>>>((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base)).Select(Array.Empty<object>()))
    {
      ComplianceDocument complianceDocument = PXResultset<ComplianceDocument>.op_Implicit(PXSelectBase<ComplianceDocument, PXSelect<ComplianceDocument, Where<ComplianceDocument.complianceDocumentID, Equal<Required<ComplianceDocument.complianceDocumentID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[1]
      {
        (object) PXResult<ComplianceDocumentBill>.op_Implicit(pxResult).ComplianceDocumentID
      }));
      if (!list.Contains(complianceDocument))
        list.Add(complianceDocument);
    }
    return (IEnumerable<ComplianceDocument>) list;
  }

  private Guid? GetComplianceDocumentReferenceId(PX.Objects.AP.APInvoice bill)
  {
    return ComplianceDocumentReferenceRetriever.GetComplianceDocumentReferenceId((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, bill);
  }
}
