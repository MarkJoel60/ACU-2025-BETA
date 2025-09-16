// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DemandForecastModel
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public abstract class DemandForecastModel
{
  public abstract void Init(
    IEnumerable<DemandForecastModel.IDataPoint> dataSequence);

  public abstract DemandForecastModel.IDataPoint GetForecast(DemandForecastModel.IDataPoint point);

  public interface IDataPoint
  {
    double X { get; }

    double Y { get; set; }

    double YError { get; set; }
  }
}
