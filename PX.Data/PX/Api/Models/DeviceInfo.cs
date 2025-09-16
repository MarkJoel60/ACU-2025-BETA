// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.DeviceInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api.Models;

public class DeviceInfo
{
  public string DeviceID { get; set; }

  public string DeviceName { get; set; }

  public string Model { get; set; }

  public string ModelName { get; set; }

  public string SystemName { get; set; }

  public string SystemVersion { get; set; }

  public string TimeZone { get; set; }
}
