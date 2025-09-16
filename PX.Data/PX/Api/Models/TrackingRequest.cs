// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.TrackingRequest
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api.Models;

public class TrackingRequest
{
  public bool IsActive { get; set; }

  public double? Interval { get; set; }

  public double? Distance { get; set; }

  public double? BreakMinutes { get; set; }
}
