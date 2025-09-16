// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.AP.Services.BillsAndAdjustmentsServices.ComplianceDocumentsService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CN.Compliance.AP.GraphExtensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.JointChecks.AP.Services.BillsAndAdjustmentsServices;

public class ComplianceDocumentsService
{
  private readonly APInvoiceEntry graph;
  private readonly IEnumerable<JointPayeePayment> jointPayeePayments;

  public ComplianceDocumentsService(
    APInvoiceEntry graph,
    IEnumerable<JointPayeePayment> jointPayeePayments)
  {
    this.graph = graph;
    this.jointPayeePayments = jointPayeePayments;
  }

  public void UpdateComplianceDocumentsForVendorCheck(
    IEnumerable<ComplianceDocument> complianceDocuments,
    APRegister vendorCheck)
  {
    EnumerableExtensions.ForEach<ComplianceDocument>(complianceDocuments, (Action<ComplianceDocument>) (cd => this.UpdateComplianceDocumentForVendorCheck(cd, vendorCheck)));
  }

  public void UpdateComplianceForJointCheck(
    JointPayee jointPayee,
    ComplianceDocument complianceDocument,
    APRegister check)
  {
    if (!ComplianceDocumentsService.IsSameJointVendorInternalId(jointPayee, complianceDocument) && !ComplianceDocumentsService.IsSameJointVendorExternalName(jointPayee, complianceDocument))
      return;
    this.UpdateComplianceDocument(complianceDocument, check, false);
  }

  public List<ComplianceDocument> GetComplianceDocumentsToLink()
  {
    return ((PXSelectBase<ComplianceDocument>) ((PXGraph) this.graph).GetExtension<ApInvoiceEntryExt>().ComplianceDocuments).Select(Array.Empty<object>()).FirstTableItems.Where<ComplianceDocument>((Func<ComplianceDocument, bool>) (cd => cd.LinkToPayment.GetValueOrDefault() && !cd.ApCheckID.HasValue)).ToList<ComplianceDocument>();
  }

  private void UpdateComplianceDocumentForVendorCheck(
    ComplianceDocument complianceDocument,
    APRegister vendorCheck)
  {
    if (complianceDocument.JointVendorInternalId.HasValue || !Str.IsNullOrEmpty(complianceDocument.JointVendorExternalName))
      return;
    this.UpdateComplianceDocument(complianceDocument, vendorCheck, true);
  }

  private void UpdateComplianceDocument(
    ComplianceDocument complianceDocument,
    APRegister check,
    bool isVendorCheck)
  {
    int? documentType = complianceDocument.DocumentType;
    int? waiverDocumentTypeId = this.GetLienWaiverDocumentTypeId();
    if (documentType.GetValueOrDefault() == waiverDocumentTypeId.GetValueOrDefault() & documentType.HasValue == waiverDocumentTypeId.HasValue)
      this.UpdateComplianceDocumentForLienWaiverType(complianceDocument, (IRegister) check, isVendorCheck);
    ComplianceDocumentRefNoteAttribute.SetComplianceDocumentReference<ComplianceDocument.apCheckId>((PXCache) GraphHelper.Caches<ComplianceDocument>((PXGraph) this.graph), complianceDocument, check.DocType, check.RefNbr, check.NoteID);
  }

  private void UpdateComplianceDocumentForLienWaiverType(
    ComplianceDocument complianceDocument,
    IRegister check,
    bool isVendorCheck)
  {
    if (isVendorCheck)
    {
      complianceDocument.JointAmount = new Decimal?();
      ComplianceDocument complianceDocument1 = complianceDocument;
      Decimal? curyOrigDocAmt = check.CuryOrigDocAmt;
      Decimal? jointAmountToPaySum = this.GetJointAmountToPaySum();
      Decimal? nullable = curyOrigDocAmt.HasValue & jointAmountToPaySum.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() + jointAmountToPaySum.GetValueOrDefault()) : new Decimal?();
      complianceDocument1.LienWaiverAmount = nullable;
    }
    else
    {
      complianceDocument.JointAmount = check.CuryOrigDocAmt;
      complianceDocument.LienWaiverAmount = check.CuryOrigDocAmt;
    }
  }

  private Decimal? GetJointAmountToPaySum()
  {
    IEnumerable<JointPayeePayment> jointPayeePayments = this.jointPayeePayments;
    return jointPayeePayments == null ? new Decimal?() : jointPayeePayments.Sum<JointPayeePayment>((Func<JointPayeePayment, Decimal?>) (x => x.JointAmountToPay));
  }

  private static bool IsSameJointVendorInternalId(
    JointPayee jointPayee,
    ComplianceDocument complianceDocument)
  {
    if (!jointPayee.JointPayeeInternalId.HasValue || !complianceDocument.JointVendorInternalId.HasValue)
      return false;
    int? jointPayeeInternalId = jointPayee.JointPayeeInternalId;
    int? vendorInternalId = complianceDocument.JointVendorInternalId;
    return jointPayeeInternalId.GetValueOrDefault() == vendorInternalId.GetValueOrDefault() & jointPayeeInternalId.HasValue == vendorInternalId.HasValue;
  }

  private static bool IsSameJointVendorExternalName(
    JointPayee jointPayee,
    ComplianceDocument complianceDocument)
  {
    string payeeExternalName = jointPayee.JointPayeeExternalName;
    string vendorExternalName = complianceDocument.JointVendorExternalName;
    return !Str.IsNullOrEmpty(payeeExternalName) && !Str.IsNullOrEmpty(vendorExternalName) && string.Equals(payeeExternalName.Trim(), vendorExternalName.Trim(), StringComparison.CurrentCultureIgnoreCase);
  }

  private int? GetLienWaiverDocumentTypeId()
  {
    return ((PXSelectBase<ComplianceAttributeType>) new PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<ComplianceDocumentType.lienWaiver>>>((PXGraph) this.graph)).SelectSingle(Array.Empty<object>())?.ComplianceAttributeTypeID;
  }
}
