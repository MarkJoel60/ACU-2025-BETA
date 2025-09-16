// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.MovingAverageModel
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class MovingAverageModel : DemandForecastModel
{
  protected static readonly string _modelName = "CMA";
  protected INUpdateReplenishmentRules.SalesStatInfo statsInfo;

  public static string ModelName => MovingAverageModel._modelName;

  public override void Init(
    IEnumerable<DemandForecastModel.IDataPoint> dataSequence)
  {
    this.statsInfo = new INUpdateReplenishmentRules.SalesStatInfo();
    foreach (DemandForecastModel.IDataPoint dataPoint in dataSequence)
    {
      this.statsInfo.Total += (Decimal) dataPoint.Y;
      ++this.statsInfo.Count;
    }
    this.statsInfo.CalcAverage();
    foreach (DemandForecastModel.IDataPoint dataPoint in dataSequence)
      this.statsInfo.CalcDevs((Decimal) dataPoint.Y);
    this.statsInfo.Recalc();
  }

  public override DemandForecastModel.IDataPoint GetForecast(DemandForecastModel.IDataPoint point)
  {
    point.Y = (double) this.statsInfo.Average;
    point.YError = Math.Abs((double) this.statsInfo.MSE);
    return point;
  }
}
