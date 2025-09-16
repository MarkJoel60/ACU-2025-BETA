// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AP.GraphExtensions.ApPaymentEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CN.Compliance.AP.GraphExtensions;

public class ApPaymentEntryExt : 
  PXGraphExtension<ComplianceViewEntityExtension<APPaymentEntry, APPayment>, APPaymentEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.apCheckId>>>, Where<ComplianceDocumentReference.type, Equal<Current<APPayment.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<APPayment.refNbr>>>>> ComplianceDocuments;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> ComplianceAttributeGroups;
  public PXSelect<ComplianceAnswer> ComplianceAnswers;
  public PXSelect<ComplianceDocumentReference> DocumentReference;
  public PXSelectJoin<ComplianceDocumentBill, InnerJoin<APInvoice, On<ComplianceDocumentBill.docType, Equal<APInvoice.docType>, And<ComplianceDocumentBill.refNbr, Equal<APInvoice.refNbr>>>>, Where<ComplianceDocumentBill.complianceDocumentID, Equal<Current<ComplianceDocument.complianceDocumentID>>>> ComplianceDetails;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  private ComplianceDocumentService service;
  public PXAction<APPayment> complianceViewDetails;

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBScalarAttribute))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ComplianceDocument.checkNumber> e)
  {
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.service = new ComplianceDocumentService((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base, (PXSelectBase<CSAttributeGroup>) this.ComplianceAttributeGroups, (PXSelectBase<ComplianceDocument>) this.ComplianceDocuments, "ComplianceDocuments");
    this.service.GenerateColumns(((PXSelectBase) this.ComplianceDocuments).Cache, "ComplianceAnswers");
    this.service.AddExpirationDateEventHandlers();
    ComplianceDocumentFieldVisibilitySetter.HideFieldsForApPayment(((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ComplianceViewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Current != null && ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Current.IsCreatedAutomatically.GetValueOrDefault())
      ((PXSelectBase<ComplianceDocumentBill>) this.ComplianceDetails).AskExt();
    return adapter.Get();
  }

  public IEnumerable adjustments_History()
  {
    foreach (PXResult pxResult in this.GetAdjustmentHistory())
    {
      this.service.ValidateApAdjustment<APAdjust.displayRefNbr>(pxResult.GetItem<APAdjust>());
      yield return (object) pxResult;
    }
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

  protected virtual void _(PX.Data.Events.RowSelected<APAdjust> args)
  {
    if (args.Row == null)
      return;
    this.service.ValidateApAdjustment<APAdjust.adjdRefNbr>(args.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<ComplianceDocument, ComplianceDocument.checkNumber> e)
  {
    if (((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<ComplianceDocument, ComplianceDocument.checkNumber>>) e).ReturnValue = (object) ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.ExtRefNbr;
  }

  protected virtual void ApPayment_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs arguments,
    PXRowSelected baseHandler)
  {
    if (!(arguments.Row is APPayment))
      return;
    baseHandler.Invoke(cache, arguments);
    ((PXSelectBase) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Cache.AllowUpdate = true;
    ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Select(Array.Empty<object>());
    ((PXSelectBase) this.ComplianceDocuments).AllowInsert = !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Cache.Inserted);
  }

  protected virtual void _(PX.Data.Events.RowSelected<APPayment> args)
  {
    APPayment row = args.Row;
    if (row != null)
      this.service.ValidateRelatedField<APPayment, ComplianceDocument.vendorID, APPayment.vendorID>(row, (object) row.VendorID);
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    this.service.ValidateComplianceDocuments(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<APPayment>>) args).Cache, complianceDocuments, ((PXSelectBase) this.ComplianceDocuments).Cache);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APPayment> args)
  {
    if (args.Row == null)
      return;
    IEnumerable<ComplianceDocument> complianceDocuments = this.GetComplianceDocuments();
    ComplianceAttributeType complianceAttributeType = PXResultset<ComplianceAttributeType>.op_Implicit(PXSelectBase<ComplianceAttributeType, PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<Required<ComplianceAttributeType.type>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base, new object[1]
    {
      (object) "Lien Waiver"
    }));
    foreach (ComplianceDocument complianceDocument in complianceDocuments)
    {
      int? documentType = complianceDocument.DocumentType;
      int? complianceAttributeTypeId = complianceAttributeType.ComplianceAttributeTypeID;
      if (!(documentType.GetValueOrDefault() == complianceAttributeTypeId.GetValueOrDefault() & documentType.HasValue == complianceAttributeTypeId.HasValue))
      {
        complianceDocument.ApCheckID = new Guid?();
        ((PXSelectBase<ComplianceDocument>) this.ComplianceDocuments).Update(complianceDocument);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    this.service.UpdateExpirationIndicator(args.Row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceDocument> args)
  {
    APPayment current = ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current;
    if (current == null)
      return;
    ComplianceDocument row = args.Row;
    if (row == null || row.SkipInit.GetValueOrDefault())
      return;
    row.VendorID = current.VendorID;
    row.VendorName = this.GetVendorName(row.VendorID);
    ComplianceDocumentRefNoteAttribute.SetComplianceDocumentReference<ComplianceDocument.apCheckId>(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<ComplianceDocument>>) args).Cache, args.Row, current.DocType, current.RefNbr, current.NoteID);
    row.CheckNumber = current.ExtRefNbr;
    row.ApPaymentMethodID = current.PaymentMethodID;
  }

  private IEnumerable GetAdjustmentHistory()
  {
    return (IEnumerable) ((object) ((PXGraphExtension<APPaymentEntry>) this).Base).GetType().GetMethod("adjustments_history", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke((object) ((PXGraphExtension<APPaymentEntry>) this).Base, (object[]) null);
  }

  private string GetVendorName(int? vendorId)
  {
    if (!vendorId.HasValue)
      return (string) null;
    return PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base, new object[1]
    {
      (object) vendorId
    }))?.AcctName;
  }

  private IEnumerable<ComplianceDocument> GetComplianceDocuments()
  {
    if (((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current == null)
      return (IEnumerable<ComplianceDocument>) new PXResultset<ComplianceDocument>().FirstTableItems.ToList<ComplianceDocument>();
    return (IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.apCheckId>>, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>>>, Where<ComplianceDocumentReference.type, Equal<Current<APPayment.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Current<APPayment.refNbr>>, And<Where<PMProject.contractID, IsNull, Or<MatchUserFor<PMProject>>>>>>>((PXGraph) ((PXGraphExtension<APPaymentEntry>) this).Base)).Select(new object[2]
    {
      (object) ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.DocType,
      (object) ((PXSelectBase<APPayment>) ((PXGraphExtension<APPaymentEntry>) this).Base.Document).Current.RefNbr
    }).FirstTableItems.ToList<ComplianceDocument>();
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();
}
