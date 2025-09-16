// Decompiled with JetBrains decompiler
// Type: PX.Owin.DeviceHub.BaseJob
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Owin.DeviceHub;

public abstract class BaseJob
{
  public string JobID { get; set; }

  public string DeviceHubID { get; set; }

  public string Status { get; set; }

  public DateTime Version { get; set; }
}
