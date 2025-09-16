// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.TrackingPoint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Api.Models;

public class TrackingPoint
{
  public DateTimeOffset? ExecutionDate { get; set; }

  public double? Latitude { get; set; }

  public double? Longitude { get; set; }

  public double? Altitude { get; set; }
}
