// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Haversine
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS;

public static class Haversine
{
  public const double Rm = 3960.0;
  public const double Rk = 6371.0;

  public static double calculate(LatLng from, LatLng to, Haversine.DistanceUnit unit)
  {
    return Haversine.calculate(from.Latitude, from.Longitude, to.Latitude, to.Longitude, unit);
  }

  public static double calculate(
    double lat1,
    double lon1,
    double lat2,
    double lon2,
    Haversine.DistanceUnit unit)
  {
    double num1 = unit == Haversine.DistanceUnit.Miles ? 3960.0 : 6371.0;
    double radians1 = Haversine.toRadians(lat2 - lat1);
    double radians2 = Haversine.toRadians(lon2 - lon1);
    double radians3 = Haversine.toRadians(lat1);
    double radians4 = Haversine.toRadians(lat2);
    double num2 = 2.0 * Math.Asin(Math.Sqrt(Math.Sin(radians1 / 2.0) * Math.Sin(radians1 / 2.0) + Math.Sin(radians2 / 2.0) * Math.Sin(radians2 / 2.0) * Math.Cos(radians3) * Math.Cos(radians4)));
    return num1 * num2;
  }

  public static double toRadians(double angle) => Math.PI * angle / 180.0;

  public enum DistanceUnit
  {
    Miles,
    Kilometers,
  }
}
