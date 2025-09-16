// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Scheduler.RouteInfo
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

#nullable disable
namespace PX.Objects.FS.Scheduler;

public class RouteInfo
{
  public int? RouteID;
  public int? ShiftID;
  public int? Sequence;

  public RouteInfo(int? routeID, int? sequence)
  {
    this.RouteID = routeID;
    this.Sequence = sequence;
  }
}
