// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.LicensingService.LicenseConstraint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update.LicensingService;

public class LicenseConstraint
{
  public System.DateTime Date { get; set; }

  public int CustomerCompanyId { get; set; }

  public int FixedAssets { get; set; }

  public int InventoryItems { get; set; }

  public int Baccount { get; set; }

  public bool Violation { get; set; }

  public int PayrollEmployees { get; set; }

  public int FSStaffVehicles { get; set; }

  public int FSAppointments { get; set; }

  public int BusinessCardsRecognized { get; set; }

  public int ExpenseReceiptsRecognized { get; set; }

  public int DocumentsRecognized { get; set; }

  public int BankFeedAccounts { get; set; }

  public int GILinesProcessed { get; set; }

  public int GIAnomaliesProcessed { get; set; }
}
