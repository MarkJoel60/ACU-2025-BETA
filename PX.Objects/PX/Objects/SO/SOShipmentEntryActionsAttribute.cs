// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentEntryActionsAttribute
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
public class SOShipmentEntryActionsAttribute : PXIntListAttribute
{
  public const int ConfirmShipment = 1;
  public const int CreateInvoice = 2;
  public const int PostInvoiceToIN = 3;
  public const int ApplyAssignmentRules = 4;
  public const int CorrectShipment = 5;
  public const int CreateDropshipInvoice = 6;
  public const int PrintLabels = 7;
  public const int GetReturnLabels = 8;
  public const int CancelReturn = 9;
  public const int PrintPickList = 10;
  public const int PrintShipmentConfirmation = 11;
  public const int PrintCommercialInvoices = 12;

  public SOShipmentEntryActionsAttribute()
    : base(new Tuple<int, string>[12]
    {
      PXIntListAttribute.Pair(1, "Confirm Shipment"),
      PXIntListAttribute.Pair(2, "Prepare Invoice"),
      PXIntListAttribute.Pair(3, "Update IN"),
      PXIntListAttribute.Pair(4, "Apply Assignment Rules"),
      PXIntListAttribute.Pair(5, "Correct Shipment"),
      PXIntListAttribute.Pair(6, "Prepare Drop-Ship Invoice"),
      PXIntListAttribute.Pair(7, "Print Labels"),
      PXIntListAttribute.Pair(12, "Print Commercial Invoices"),
      PXIntListAttribute.Pair(8, "Get Return Labels"),
      PXIntListAttribute.Pair(9, "Cancel Return"),
      PXIntListAttribute.Pair(10, "Print Pick List"),
      PXIntListAttribute.Pair(11, "Print Shipment Confirmation")
    })
  {
  }

  [PXLocalizable]
  public class Messages
  {
    public const string ConfirmShipment = "Confirm Shipment";
    public const string CreateInvoice = "Prepare Invoice";
    public const string PostInvoiceToIN = "Update IN";
    public const string ApplyAssignmentRules = "Apply Assignment Rules";
    public const string CorrectShipment = "Correct Shipment";
    public const string CreateDropshipInvoice = "Prepare Drop-Ship Invoice";
    public const string PrintLabels = "Print Labels";
    public const string PrintCommercialInvoices = "Print Commercial Invoices";
    public const string GetReturnLabels = "Get Return Labels";
    public const string CancelReturn = "Cancel Return";
    public const string PrintPickList = "Print Pick List";
    public const string PrintShipmentConfirmation = "Print Shipment Confirmation";
  }
}
