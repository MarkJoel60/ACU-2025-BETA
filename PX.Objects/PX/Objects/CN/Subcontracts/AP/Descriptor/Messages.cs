// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.Descriptor.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CN.Subcontracts.AP.Descriptor;

[PXLocalizable("AP Error")]
public class Messages
{
  public const string AddSubcontract = "Add Subcontract";
  public const string AddSubcontractLine = "Add Subcontract Line";
  public const string ViewSubcontract = "View Subcontract";
  public const string ViewPoOrder = "View PO Order";
  public const string SubcontractViewName = "Subcontract";
  public const string FailedToAddSubcontractLinesError = "One subcontract line or multiple subcontract lines cannot be added to the bill. See Trace Log for details.";
  public const string CannotLinkSubcontractLine = "You cannot link this subcontract line to the current AP document line because the inventory item specified in the line requires a receipt. Select a line with the empty item code or with a non-stock item that is configured so that the system does not require a receipt for it.";
  public const string AutoApplyRetainageCheckBox = "The Apply Retainage check box is selected automatically because you have added one or more lines with a retainage from the purchase order or subcontract.";
  private const string Prefix = "AP Error";

  public static class LinkLineFilterMode
  {
    public const string PurchaseOrderOrSubcontract = "Purchase Order / Subcontract";
  }

  public static class Subcontract
  {
    public const string SubcontractNumber = "Subcontract Nbr.";
    public const string SubcontractTotal = "Subcontract Total";
    public const string SubcontractBilledQty = "Total Billed Qty.";
    public const string SubcontractBilledTotal = "Total Billed Amount";
    public const string Project = "Project";
    public const string SubcontractLine = "Subcontract Line";
    public const string SubcontractDate = "Date";
  }

  public static class FieldClass
  {
    public const string Distribution = "DISTR";
  }
}
