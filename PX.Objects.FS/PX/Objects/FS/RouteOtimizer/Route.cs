// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteOtimizer.Route
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.RouteOtimizer;

public class Route
{
  public OuputVehicle vehicle { get; set; }

  public List<RouteStep> steps { get; set; }

  public RouteStats routeStats { get; set; }

  public double cost { get; set; }
}
