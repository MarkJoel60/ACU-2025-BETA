// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOReports
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.SO;

public class SOReports
{
  public const string PrintLabels = "SO645000";
  public const string PrintCommercialInvoices = "SO645010";
  public const string PrintPickList = "SO644000";
  public const string PrintShipmentConfirmation = "SO642000";
  public const string PrintInvoiceReport = "SO643000";
  public const string PrintSalesOrder = "SO641010";
  public const string PrintPackSlipBatch = "SO644005";
  public const string PrintPackSlipWave = "SO644007";
  public const string PrintPickerPickList = "SO644006";

  public static string GetReportID(int? actionID, string actionName)
  {
    if (actionID.HasValue && actionID.HasValue)
    {
      switch (actionID.GetValueOrDefault())
      {
        case 7:
          return "SO645000";
        case 10:
          return "SO644000";
        case 11:
          return "SO642000";
        case 12:
          return "SO645010";
      }
    }
    if (actionName != null && actionName != null)
    {
      switch (actionName.Length)
      {
        case 12:
          if (actionName == "Print Labels")
            break;
          goto label_20;
        case 15:
          if (actionName == "Print Pick List")
            goto label_18;
          goto label_20;
        case 20:
          if (actionName == "SO302000$printLabels")
            break;
          goto label_20;
        case 25:
          if (actionName == "Print Commercial Invoices")
            goto label_17;
          goto label_20;
        case 27:
          if (actionName == "Print Shipment Confirmation")
            goto label_19;
          goto label_20;
        case 28:
          if (actionName == "SO302000$printPickListAction")
            goto label_18;
          goto label_20;
        case 32 /*0x20*/:
          if (actionName == "SO302000$printCommercialInvoices")
            goto label_17;
          goto label_20;
        case 34:
          if (actionName == "SO302000$printShipmentConfirmation")
            goto label_19;
          goto label_20;
        default:
          goto label_20;
      }
      return "SO645000";
label_17:
      return "SO645010";
label_18:
      return "SO644000";
label_19:
      return "SO642000";
    }
label_20:
    return (string) null;
  }
}
