// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DemandForecastModelType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class DemandForecastModelType
{
  public const 
  #nullable disable
  string None = "NNN";
  public const string MovingAverage = "CMA";
  public const string ExponentialSmoothing = "ESC";
  public const string ExponentialSmoothingTrend = "EST";
  public const string ExponentialSmoothingSeasons = "ESS";
  public const string ExponentialSmoothingTrendAndSeasons = "ETS";
  public const string LinearRegression = "LRT";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("NNN", "None"),
        PXStringListAttribute.Pair("CMA", "Moving Average")
      })
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DemandForecastModelType.none>
  {
    public none()
      : base("NNN")
    {
    }
  }
}
