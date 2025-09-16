// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AR.GraphExtensions.ArPaymentEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CN.Compliance.AR.CacheExtensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.Common.Abstractions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CN.Compliance.AR.GraphExtensions;

public class ArPaymentEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<ARPaymentEntry, ARPayment>, ARPaymentEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.arPaymentID>>>, Where<ComplianceDocumentReference.type, Equal<Current<ARPayment.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<ARPayment.refNbr>>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  public PXSelect<ComplianceDocumentReference> DocumentReference;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService service;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForArPayment(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  public virtual void _(PX.Data.Events.RowUpdated<ComplianceDocument> args)
  {
    ((PXSelectBase) this.ComplianceDocuments).View.RequestRefresh();
  }

  public IEnumerable complianceDocuments()
  {
    List<ComplianceDocument> list = this.GetComplianceDocuments().ToList<ComplianceDocument>();
    this.service.ValidateComplianceDocuments((PXCache) null, (IEnumerable<ComplianceDocument>) list, ((PXSelectBase) this.ComplianceDocuments).Cache);
    return (IEnumerable) list;
  }

  public IEnumerable adjustments_History()
  {
    foreach (PXResult pxResult in this.GetAdjustmentHistory())
    {
      this.ValidateAdjustments<ARAdjust.displayRefNbr, ARAdjust.displayCustomerID>(pxResult.GetItem<ARAdjust>());
      yield return (object) pxResult;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARAdjust> args)
  {
    if (args.Row == null)
      return;
    this.ValidateAdjustments<ARAdjust.adjdRefNbr, ARAdjust.adjdCustomerID>(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<ARPayment> args)
  {
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<ARPayment>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void ArPayment_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs arguments,
    PXRowSelected baseHandler)
  {
    if (!(arguments.Row is ARPayment))
      return;
    baseHandler.Invoke(cache, arguments);
    ((PXSelectBase) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Cache.AllowUpdate = true;
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Cache.Inserted);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARPayment> args)
  {
    ARPayment row = args.Row;
    if (row == null)
      return;
    this.service.ValidateRelatedField<ARPayment, ComplianceDocument.customerID, ARPayment.customerID>(row, (object) row.CustomerID);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<ARPayment> args)
  {
    if (args.Row == null)
      return;
    foreach (ComplianceDocument complianceDocument in this.GetComplianceDocuments())
    {
      complianceDocument.ArPaymentID = new Guid?();
      ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    this.service.UpdateExpirationIndicator(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserted<ComplianceDocument> args)
  {
    ARPayment current = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current;
    if (current == null)
      return;
    ComplianceDocument row = args.Row;
    if (row == null)
      return;
    row.CustomerID = current.CustomerID;
    row.CustomerName = this.GetCustomerName(row.CustomerID);
    row.ArPaymentID = this.CreateComplianceDocumentReference((ARRegister) current).ComplianceDocumentReferenceId;
    row.ArPaymentMethodID = current.PaymentMethodID;
  }

  private void ValidateAdjustments<TInvoiceField, TCustomerField>(ARAdjust adjustment)
    where TInvoiceField : IBqlField
    where TCustomerField : IBqlField
  {
    ARInvoice arInvoice = this.GetArInvoice(adjustment);
    if (arInvoice == null)
      return;
    bool flag1 = this.service.ValidateRelatedField<ARAdjust, ComplianceDocument.invoiceID, TInvoiceField>(adjustment, (object) ComplianceDocumentReferenceRetriever.GetComplianceDocumentReferenceId((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, (IDocumentKey) arInvoice));
    bool flag2 = this.service.ValidateRelatedField<ARAdjust, ComplianceDocument.customerID, TCustomerField>(adjustment, (object) arInvoice.CustomerID);
    this.service.ValidateRelatedRow<ARAdjust, ArAdjustExt.hasExpiredComplianceDocuments>(adjustment, flag1 | flag2);
  }

  private ARInvoice GetArInvoice(ARAdjust adjustment)
  {
    return ((PXSelectBase<ARInvoice>) new PXSelect<ARInvoice, Where<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>, And<ARInvoice.docType, Equal<Required<ARInvoice.docType>>>>>((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base)).SelectSingle(new object[2]
    {
      (object) adjustment.AdjdRefNbr,
      (object) adjustment.AdjdDocType
    });
  }

  private IEnumerable GetAdjustmentHistory()
  {
    return (IEnumerable) ((object) ((PXGraphExtension<ARPaymentEntry>) this).Base).GetType().GetMethod("adjustments_history", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke((object) ((PXGraphExtension<ARPaymentEntry>) this).Base, (object[]) null);
  }

  private string GetCustomerName(int? customerId)
  {
    if (!customerId.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[1]
    {
      (object) customerId
    }))?.AcctName;
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    using (new PXConnectionScope())
      return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.arPaymentID>>, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>>, Where<ComplianceDocumentReference.type, Equal<Current<ARPayment.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<ARPayment.refNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>>((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base)).Select(new object[2]
      {
        (object) ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.DocType,
        (object) ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.RefNbr
      }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private ComplianceDocumentReference CreateComplianceDocumentReference(ARRegister arPayment)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) this.DocumentReference).Insert(new ComplianceDocumentReference()
    {
      ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid()),
      Type = arPayment.DocType,
      ReferenceNumber = arPayment.RefNbr,
      RefNoteId = arPayment.NoteID
    });
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();
}
