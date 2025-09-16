// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.Descriptor.ComplianceMessages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CN.Compliance.Descriptor;

[PXLocalizable]
public static class ComplianceMessages
{
  public const string HasExpiredComplianceDocuments = "Expired Compliance";
  public const string ComplianceDocumentIsExpiredMessage = "The compliance document has expired.";
  public const string VendorHasExpiredComplianceDocumentsMessage = "Vendor has expired compliance documents.";
  public const string DocumentHasExpiredComplianceMessage = "Document has expired compliance.";
  public const string CustomerHasExpiredComplianceMessage = "Customer has expired compliance.";
  public const string ExpiredComplianceMessage = "Expired Compliance.";
  public const string ExpiredProjectComplianceMessage = "The {0} project has at least one expired compliance document.";
  public const string AttributeNotFoundMessage = "'Attribute' cannot be found in the system.";
  public const string RequiredFieldMessage = "The field is required.";
  public const string UniqueConstraintMessage = "A compliance document with the same information (document category, policy, vendor, project, effective date, expiration date, and limit) already exists.";
  public const string OnlyOneVendorIsAllowed = "You cannot specify the Joint Payee (Vendor) and Joint Payee at the same time. Either clear the Joint Payee (Vendor) or Joint Payee.";
  public const string DeleteComplianceAttributeConfirmationDialogBody = "This action will delete the attribute from the compliance documents and all attribute values from corresponding records";
  public const string CannotDeleteAttributeMessage = "The value can not be deleted. It is used in at least one compliance document.";
  public const string WouldYouLikeToAddVendorClassToExistingProjects = "Would you like to add Vendor Class to existing projects for automatic Lien Waiver generation?";
  public const string CostTaskIsNotLinkedToChangeOrder = "Cost Task is not linked to the selected Change Order";
  public const string CostCodeIsNotLinkedToChangeOrder = "Cost Code is not linked to the selected Change Order";
  public const string RevenueTaskIsNotLinkedToChangeOrder = "Revenue Task is not linked to the selected Change Order";

  [PXLocalizable]
  public static class LienWaiver
  {
    public const string VendorHasOutstandingLienWaiver = "The vendor has at least one outstanding lien waiver.  ";
    public const string CheckWillBeAssignedOnHoldStatus = "The AP payment or payments will be assigned the On Hold status.";
    public const string JointPayeeHasOutstandingLienWaiver = "The joint payee has at least one outstanding lien waiver.";
    public const string VendorAndJointPayeeHaveOutstandingLienWaiver = "The vendor and joint payee have at least one outstanding lien waiver:";
    public const string BillHasOneOrMoreOutstandingLienWaivers = "The accounts payable bill has at least one outstanding lien waiver.";
    public const string BillHasOutstandingLienWaiverStopPayment = "The accounts payable bill has at least one outstanding lien waiver. Payment is not allowed because the Stop Payment of AP Bill When There Are Outstanding Lien Waivers check box is selected on the Compliance Preferences (CL301000) form.";
    public const string ManuallyCreatedLienWaiverIsReferredToApCheck = "A manually created lien waiver is linked to an AP payment. Generate an additional lien waiver automatically?";
    public const string WouldYouLikeToVoidAutomaticallyCreatedLienWaiver = "Would you like to void automatically created lien waivers referred to the {0}?";
    public const string DocumentTypeOptionVendorAndProjectMustBeSpecified = "The lien waiver cannot be processed, because at least one of the following values is not specified in its settings: Document Category, Vendor, or Project.";
    public const string LienWaiverGenerationFailed = "The lien waiver generation has failed. Do one of the following: switch off automatic lien waiver generation on the Compliance Preferences (CL301000) form for a check to be processed or clear the Lien Notice Amount box on the Compliance Management (CL401000) form for the following lien waivers: {0}. Please also contact your Acumatica support provider.";
    public const string OutstandingLienWaiver = "There is an outstanding lien waiver for the {0} vendor.";
    public const string OpenBalanceForCommitmentProjectTask = "One or more bills from the {0} payment have open balance for the {1} commitment, the {2} project, and the {3} project task. To set the lien waiver as final, click OK.";
    public const string OpenBalanceForCommitmentProject = "One or more bills from the {0} payment have open balance for the {1} commitment and the {2} project. To set the lien waiver as final, click OK.";
    public const string OpenBalanceForProjectTask = "One or more bills from the {0} payment have open balance for the {1} project and the {2} project task. To set the lien waiver as final, click OK.";
    public const string OpenBalanceForProject = "One or more bills from the {0} payment have open balance for the {1} project. To set the lien waiver as final, click OK.";
    public const string FinalLienWaiverExistsForCombination = "The final lien waiver already exists for the combination: {0}, {1}, {2}.";
    public const string FinalLienWaiverAlreadyExists = "The final lien waiver already exists for the following combination of calculation parameters: {0}. For details, see the trace log.";
  }
}
