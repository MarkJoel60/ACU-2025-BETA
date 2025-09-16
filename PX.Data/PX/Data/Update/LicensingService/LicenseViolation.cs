// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.LicensingService.LicenseViolation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update.LicensingService;

public class LicenseViolation
{
  public System.DateTime Date { get; set; }

  public string LimitType { get; set; }

  public string TranType { get; set; }

  public int Limit { get; set; }

  public string Status { get; set; }

  public System.DateTime CloseDate { get; set; }

  public string Reason { get; set; }
}
