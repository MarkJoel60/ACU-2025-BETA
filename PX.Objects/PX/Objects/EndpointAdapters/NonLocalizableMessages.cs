// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.NonLocalizableMessages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.EndpointAdapters;

public static class NonLocalizableMessages
{
  public const string PaymentInSOOrderNotFound = "Payment {0} {1} was not found in the list of payments applied to order.";
  public const string MissingPOOrderReference = "Both POOrderType and POOrderNumber must be provided to add a Purchase Order to details.";
  public const string PurchaseOrderDoesNotExist = "Purchase order {0} - {1} was not found.";
  public const string SubcontractNotFound = "Subcontract {0} was not found.";
  public const string SubcontractLineNotFound = "Subcontract {0}, Line Nbr.: {1} was not found.";
  public const string PurchaseOrderLineNotFound = "Order Line: {0} {1}, Line Nbr.: {2} not found.";
  public const string PurchaseReceiptNotFound = "Purchase Receipt {0} was not found.";
  public const string PurchaseReceiptLineNotFound = "Receipt Line {0} - {1} not found.";
  public const string MissingLCNbrReference = "Both LCType and LCNbr must be provided to add Landed Costs to details.";
  public const string LandedCostNotFound = "Landed Cost {0} was not found.";
  public const string LandedCostLineNotFound = "Landed Cost Line {0} - {1} not found.";
}
