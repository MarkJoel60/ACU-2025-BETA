// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AR.GraphExtensions.ArInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CN.Common.Services;
using PX.Objects.CN.Compliance.AR.CacheExtensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.AR.GraphExtensions;

public class ArInvoiceEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<ARInvoiceEntry, ARInvoice>, ARInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.invoiceID>>>, Where<ComplianceDocumentReference.type, Equal<Current<ARInvoice.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<ARInvoice.refNbr>>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  public PXSelect<ComplianceDocumentReference> DocumentReference;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService service;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForArInvoice(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  public IEnumerable complianceDocuments()
  {
    List<ComplianceDocument> list = this.GetComplianceDocuments().ToList<ComplianceDocument>();
    this.service.ValidateComplianceDocuments((PXCache) null, (IEnumerable<ComplianceDocument>) list, ((PXSelectBase) this.ComplianceDocuments).Cache);
    return (IEnumerable) list;
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARTran> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId() || args.Row == null)
      return;
    this.ValidateTransaction(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARAdjust2> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId() || args.Row == null)
      return;
    this.ValidateAdjustment(args.Row);
  }

  protected virtual void ArInvoice_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs args,
    PXRowSelected baseHandler)
  {
    if (SiteMapExtension.IsInvoicesScreenId())
    {
      if (!(args.Row is ARInvoice))
        return;
      baseHandler.Invoke(cache, args);
    }
    else
    {
      if (!(args.Row is ARInvoice row))
        return;
      this.service.ValidateRelatedProjectField<ARInvoice, ARInvoice.projectID>(row, (object) row.ProjectID);
      this.service.ValidateRelatedField<ARInvoice, ComplianceDocument.customerID, ARInvoice.customerID>(row, (object) row.CustomerID);
      baseHandler.Invoke(cache, args);
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
      ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Cache.Inserted);
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ComplianceDocument> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId())
      return;
    ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Transactions).Cache.ClearItemAttributes();
    ((PXSelectBase) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Adjustments).Cache.ClearItemAttributes();
    ((PXSelectBase) this.ComplianceDocuments).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<ARInvoice> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId())
      return;
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<ARInvoice>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARInvoice> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId() || args.Row == null)
      return;
    foreach (ComplianceDocument complianceDocument in this.GetComplianceDocuments())
    {
      complianceDocument.InvoiceID = new Guid?();
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<ARInvoice.curyOrigDocAmt> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId())
      return;
    ARInvoice invoice = args.Row as ARInvoice;
    if (invoice == null)
      return;
    foreach (ComplianceDocument complianceDocument in ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>()).FirstTableItems.Where<ComplianceDocument>((Func<ComplianceDocument, bool>) (x =>
    {
      Guid? invoiceId = x.InvoiceID;
      Guid? documentReferenceId = this.GetComplianceDocumentReferenceId(invoice);
      if (invoiceId.HasValue != documentReferenceId.HasValue)
        return false;
      return !invoiceId.HasValue || invoiceId.GetValueOrDefault() == documentReferenceId.GetValueOrDefault();
    })).ToList<ComplianceDocument>())
    {
      complianceDocument.InvoiceAmount = invoice.OrigDocAmt;
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId())
      return;
    this.service.UpdateExpirationIndicator(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    if (SiteMapExtension.IsInvoicesScreenId())
      return;
    ARInvoice current = ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current;
    if (current == null)
      return;
    ComplianceDocument row = args.Row;
    if (row == null)
      return;
    row.CustomerID = current.CustomerID;
    row.CustomerName = this.GetCustomerName(row.CustomerID);
    row.InvoiceID = this.CreateComplianceDocumentReference((ARRegister) current).ComplianceDocumentReferenceId;
    row.InvoiceAmount = current.OrigDocAmt;
    row.AccountID = this.GetAccountId();
  }

  private void ValidateTransaction(ARTran transaction)
  {
    bool rowHasExpiredCompliance = this.service.ValidateRelatedField<ARTran, ComplianceDocument.revenueTaskID, ARTran.taskID>(transaction, (object) transaction.TaskID);
    this.service.ValidateRelatedRow<ARTran, ArTranExt.hasExpiredComplianceDocuments>(transaction, rowHasExpiredCompliance);
  }

  private void ValidateAdjustment(ARAdjust2 adjustment)
  {
    bool rowHasExpiredCompliance = this.service.ValidateRelatedField<ARAdjust2, ComplianceDocument.arPaymentID, ARAdjust2.adjgRefNbr>(adjustment, (object) ComplianceDocumentReferenceRetriever.GetComplianceDocumentReferenceId((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, (IDocumentAdjustment) adjustment));
    this.service.ValidateRelatedRow<ARAdjust2, ArAdjust2Ext.hasExpiredComplianceDocuments>(adjustment, rowHasExpiredCompliance);
  }

  private int? GetAccountId()
  {
    return ((PXSelectBase<ARTran>) new PXSelect<ARTran, Where<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>>, OrderBy<Asc<ARTran.lineNbr>>>((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base)).SelectSingle(Array.Empty<object>())?.AccountID;
  }

  private string GetCustomerName(int? customerId)
  {
    if (!customerId.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, new object[1]
    {
      (object) customerId
    }))?.AcctName;
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.invoiceID>>, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>>, Where<ComplianceDocumentReference.type, Equal<Current<ARInvoice.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<ARInvoice.refNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>>((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base)).Select(new object[2]
      {
        (object) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.DocType,
        (object) ((PXSelectBase<ARInvoice>) ((PXGraphExtension<ARInvoiceEntry>) this).Base.Document).Current.RefNbr
      }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private Guid? GetComplianceDocumentReferenceId(ARInvoice invoice)
  {
    return ComplianceDocumentReferenceRetriever.GetComplianceDocumentReferenceId((PXGraph) ((PXGraphExtension<ARInvoiceEntry>) this).Base, (IDocumentKey) invoice);
  }

  private ComplianceDocumentReference CreateComplianceDocumentReference(ARRegister invoice)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) this.DocumentReference).Insert(new ComplianceDocumentReference()
    {
      ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid()),
      Type = invoice.DocType,
      ReferenceNumber = invoice.RefNbr,
      RefNoteId = invoice.NoteID
    });
  }
}
