// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Warnings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.PM;

[PXLocalizable("PM Warning")]
public static class Warnings
{
  public const string Prefix = "PM Warning";
  public const string AccountIsUsed = "This account is already added to the '{0}' account group. By clicking Save, you will move the account to the currently selected account group.";
  public const string StartDateOverlow = "The Start Date of the {0} project task must be within the date range defined by the Start Date and End Date of the project. ";
  public const string EndDateOverlow = "The End Date of the {0} project task must be within the date range defined by the Start Date and End Date of the project. ";
  public const string ProjectIsCompleted = "Project is Completed. It will not be available for data entry.";
  public const string ProjectIsNotActive = "Project is Not Active. Please Activate Project.";
  public const string NothingToAllocate = "Transactions were not created during the allocation.";
  public const string NothingToBill = "Invoice was not created during the billing. Nothing to bill.";
  public const string ProjectCustomerDontMatchTheDocument = "Customer on the Document doesn't match the Customer on the Project or Contract.";
  public const string SelectedProjectCustomerDontMatchTheDocument = "The customer in the selected project or contract differs from the customer in the current document.";
  public const string ProjectTaxZoneFeatureIsInUse = "This functionality is in use. If you clear this check box, the system will not use project-specific tax zones and addresses in project-related documents.";
  public const string NoPendingValuesToBeBilled = "The operation has been completed. The project has no pending values to be billed on {0}.";
  public const string LaborItemIsInactive = "The {0} labor item is inactive.";
  public const string RecalculateProjectBalances = "Recalculate the project balance by using the Recalculate Project Balance command on the More menu.";
}
