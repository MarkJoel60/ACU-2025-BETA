// Decompiled with JetBrains decompiler
// Type: PX.Data.GoogleMapLatLongRedirector
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Data;

public class GoogleMapLatLongRedirector : GoogleMapRedirector
{
  public void ShowAddressByLocation(Decimal? latitude, Decimal? longitude)
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    string empty4 = string.Empty;
    string empty5 = string.Empty;
    string empty6 = string.Empty;
    string empty7 = string.Empty;
    if (!latitude.HasValue)
      latitude = new Decimal?(0M);
    if (!longitude.HasValue)
      longitude = new Decimal?(0M);
    string str = $"{empty1}{Convert.ToString((object) latitude)},{Convert.ToString((object) longitude)}";
    ((MapRedirector) this).ShowAddress(empty2, empty3, empty4, empty5, empty6, empty7, str);
  }
}
