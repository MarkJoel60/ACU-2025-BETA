// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteOtimizer.SingleDayOptimizationOutput
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.RouteOtimizer;

public class SingleDayOptimizationOutput
{
  public string reqID { get; set; }

  public string status { get; set; }

  public int elapsedSec { get; set; }

  public List<Route> routes { get; set; }

  public List<OutputWaypoint> unreachedWaypoints { get; set; }

  public List<OutputWaypoint> unreachableWaypoints { get; set; }

  public List<OuputVehicle> unneededVehicles { get; set; }

  public List<object> warnings { get; set; }
}
