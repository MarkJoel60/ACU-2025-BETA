// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.ComplianceDocumentFieldVisibilitySetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

internal static class ComplianceDocumentFieldVisibilitySetter
{
  public static void HideFieldsForProject(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
  }

  public static void HideFieldsForCustomer(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
  }

  public static void HideFieldsForContract(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
  }

  public static void HideFieldsForProjectTask(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
  }

  public static void HideFieldsForVendor(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
  }

  public static void HideFieldsForCommitments(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.purchaseOrder>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.subcontract>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
  }

  public static void HideFieldsForArInvoice(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.invoiceID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.invoiceAmount>(cache);
  }

  public static void ConfigureComplianceGridColumnsForApBill(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    PXUIFieldAttribute.SetVisibility<ComplianceDocument.linkToPayment>(cache, (object) null, (PXUIVisibility) 3);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.linkToPayment>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.jointVendorInternalId>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.jointVendorExternalName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.apCheckId>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.jointAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.lienWaiverAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.billID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.billAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
  }

  public static void HideFieldsForApPayment(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.apCheckId>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.checkNumber>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.apPaymentMethodID>(cache);
  }

  public static void HideFieldsForArPayment(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.arPaymentID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.arPaymentMethodID>(cache);
  }

  public static void HideFieldsForChangeOrder(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.changeOrderNumber>(cache);
  }

  public static void HideFieldsForProjectTransactionsForm(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.projectTransactionID>(cache);
  }

  public static void HideFieldsForPurchaseOrder(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.purchaseOrder>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.purchaseOrderLineItem>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
  }

  public static void HideFieldsForSubcontract(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideAllNotSharedFields(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.subcontract>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.subcontractLineItem>(cache);
    ComplianceDocumentFieldVisibilitySetter.ShowField<ComplianceDocument.accountID>(cache);
  }

  private static void HideAllNotSharedFields(PXCache cache)
  {
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.selected>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.linkToPayment>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.effectiveDate>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.limit>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.methodSent>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.customerID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.customerName>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.vendorID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.vendorName>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.purchaseOrder>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.purchaseOrderLineItem>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.subcontract>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.subcontractLineItem>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.invoiceID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.invoiceAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.billID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.billAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.lienWaiverAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.sponsorOrganization>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.certificateNumber>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.insuranceCompany>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.policy>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.apPaymentMethodID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.arPaymentMethodID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.accountID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.apCheckId>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.checkNumber>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.arPaymentID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.projectTransactionID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.receiptDate>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.dateIssued>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.throughDate>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.receiveDate>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.receivedBy>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.sourceType>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.isRequiredJointCheck>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointVendorInternalId>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointVendorExternalName>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointRelease>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointReleaseReceived>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.documentTypeValue>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.paymentDate>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.costTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.revenueTaskID>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.changeOrderNumber>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.lienNoticeAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointLienNoticeAmount>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.isReceivedFromJointVendor>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointReceivedDate>(cache);
    ComplianceDocumentFieldVisibilitySetter.HideField<ComplianceDocument.jointLienWaiverAmount>(cache);
  }

  private static void HideField<TField>(PXCache cache) where TField : IBqlField
  {
    PXUIFieldAttribute.SetVisible<TField>(cache, (object) null, false);
  }

  private static void ShowField<TField>(PXCache cache) where TField : IBqlField
  {
    PXUIFieldAttribute.SetVisible<TField>(cache, (object) null, true);
  }
}
