// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.Descriptor.ComplianceLabels
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CN.Compliance.Descriptor;

[PXLocalizable]
public class ComplianceLabels
{
  [PXLocalizable]
  public static class Subcontract
  {
    public const string SubcontractNumber = "Subcontract Nbr.";
    public const string SubcontractTotal = "Subcontract Total";
    public const string VendorReference = "Vendor Ref.";
    public const string Date = "Date";
    public const string Status = "Status";
    public const string Vendor = "Vendor";
    public const string VendorName = "Vendor Name";
    public const string Location = "Location";
    public const string Currency = "Currency";
  }

  [PXLocalizable]
  public static class LienWaiverSetup
  {
    public const string AutomaticallyGenerateLienWaivers = "Automatically Generate Lien Waivers";
    public const string GenerateWithoutCommitment = "Generate for AP Documents Not Linked to Commitments";
    public const string GenerateLienWaiversOn = "Generate Lien Waivers on";
    public const string ThroughDate = "Through Date";
    public const string GroupBy = "Calculate Amount By";
    public const string LienWaiverThroughDateSource_BillDate = "Bill Date";
    public const string LienWaiverThroughDateSource_PostingPeriodEndDate = "Posting Period End Date";
    public const string LienWaiverThroughDateSource_PaymentDate = "AP Payment Date";
    public const string CommitmentProjectTask = "Commitment, Project, Project Task";
    public const string CommitmentProject = "Commitment, Project";
    public const string ProjectTask = "Project, Project Task";
    public const string Project = "Project";
  }
}
