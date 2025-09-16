// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.LicensingService.DailyStatistic
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update.LicensingService;

public class DailyStatistic
{
  public System.DateTime Date { get; set; }

  public int CommerceTranCount { get; set; }

  public int ERPTranCount { get; set; }

  public int MaxAPIRate { get; set; }

  public int RejectedAPICount { get; set; }

  public int ThrottledAPICount { get; set; }

  public bool Violation { get; set; }

  public bool LicenseSuspended { get; set; }

  public string SuspensionType { get; set; }

  public System.DateTime SuspendedTillDate { get; set; }
}
