// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PM.Descriptor.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PM.Descriptor;

[PXLocalizable("PM Error")]
public static class Messages
{
  public const string Subcontract = "Subcontract";
  private const string Prefix = "PM Error";
  public const string CreateSubcontract = "Create Subcontract";

  public static class PmTask
  {
    public const string TaskTypeIsNotAvailable = "Task Type is not valid";
  }

  public static class PmCommitment
  {
    public const string RelatedDocumentType = "Related Document Type";
    public const string PurchaseOrderType = "POOrder";
    public const string SalesOrderType = "SOOrder";
    public const string SubcontractType = "Subcontract";
    public const string PurchaseOrderLabel = "Purchase Order";
    public const string SalesOrderLabel = "Sales Order";
    public const string SubcontractLabel = "Subcontract";
  }

  public static class ChangeOrders
  {
    public const string CommitmentNbr = "Commitment Nbr.";
    public const string CommitmentLineNbr = "Commitment Line Nbr.";
  }

  public static class PmChangeOrderLine
  {
    public const string CommitmentType = "Commitment Type";
  }
}
