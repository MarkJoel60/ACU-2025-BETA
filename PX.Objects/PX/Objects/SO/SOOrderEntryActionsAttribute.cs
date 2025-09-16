// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderEntryActionsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public class SOOrderEntryActionsAttribute : PXIntListAttribute
{
  private static Tuple<int, string>[] ValuesToLabels
  {
    get
    {
      return new Tuple<int, string>[7]
      {
        PXIntListAttribute.Pair(1, "Create Shipment"),
        PXIntListAttribute.Pair(2, "Apply Assignment Rules"),
        PXIntListAttribute.Pair(1, "Create Shipment"),
        PXIntListAttribute.Pair(4, "Post Invoice to IN"),
        PXIntListAttribute.Pair(5, "Create Purchase Order"),
        PXIntListAttribute.Pair(6, "Create Transfer Order"),
        PXIntListAttribute.Pair(7, "Reopen Order")
      };
    }
  }

  public SOOrderEntryActionsAttribute()
    : base(SOOrderEntryActionsAttribute.ValuesToLabels)
  {
  }

  public static class Values
  {
    public const int CreateShipment = 1;
    public const int ApplyAssignmentRules = 2;
    public const int CreateInvoice = 3;
    public const int PostInvoiceToIN = 4;
    public const int CreatePurchaseOrder = 5;
    public const int CreateTransferOrder = 6;
    public const int ReopenOrder = 7;
  }

  [PXLocalizable]
  public static class DisplayNames
  {
    public const string CreateShipment = "Create Shipment";
    public const string ApplyAssignmentRules = "Apply Assignment Rules";
    public const string PrepareInvoice = "Prepare Invoice";
    public const string PostInvoiceToIN = "Post Invoice to IN";
    public const string CreatePurchaseOrder = "Create Purchase Order";
    public const string CreateTransferOrder = "Create Transfer Order";
    public const string ReopenOrder = "Reopen Order";
  }
}
