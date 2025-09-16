// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteOtimizer.Vehicle
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS.RouteOtimizer;

public class Vehicle
{
  public string name { get; set; }

  public Capacity maxCapacityMap { get; set; }

  public RouteLocation origin { get; set; }

  public TimeWindow timeWindow { get; set; }

  public RouteLocation destination { get; set; }

  public List<string> tags { get; set; }

  public bool allowBreakService { get; set; }

  public List<Break> breaks { get; set; }
}
