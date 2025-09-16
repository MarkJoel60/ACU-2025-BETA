// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteOtimizer.RouteStep
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.RouteOtimizer;

public class RouteStep
{
  public int stepNumber { get; set; }

  public OutputWaypoint waypoint { get; set; }

  public double latitude { get; set; }

  public double longitude { get; set; }

  public TimeWindow timeWindow { get; set; }

  public int arrivalTimeSec { get; set; }

  public int idleTimeSec { get; set; }

  public int serviceStartTimeSec { get; set; }

  public int departureTimeSec { get; set; }

  public int nextStepDriveTimeSec { get; set; }

  public int nextStepDistanceMt { get; set; }

  public int driveBreakTimeSec { get; set; }

  public List<RouteStepBreak> driveBreaks { get; set; }

  public int serviceBreakTimeSec { get; set; }

  /// This field is omitted if no Vehicle Breaks are defined in the input request.
  public List<RouteStepBreak> serviceBreaks { get; set; }

  public RouteStats cumulativeRouteStats { get; set; }

  public Capacity cumulativeCapacityMap { get; set; }
}
