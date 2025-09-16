// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteOtimizer.Waypoint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.RouteOtimizer;

public class Waypoint
{
  public string name { get; set; }

  public RouteLocation location { get; set; }

  public Capacity deliveryMap { get; set; }

  public int serviceTimeSec { get; set; }

  public int priority { get; set; }

  public List<string> tagsIncludeAnd { get; set; }

  public List<string> tagsIncludeOr { get; set; }

  public List<string> tagsExclude { get; set; }

  public List<TimeWindow> timeWindows { get; set; }
}
