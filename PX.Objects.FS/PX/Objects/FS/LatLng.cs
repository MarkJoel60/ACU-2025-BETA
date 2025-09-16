// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.LatLng
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Globalization;
using System.Xml;

#nullable disable
namespace PX.Objects.FS;

/// <summary>Class representing a latitude/longitude pair.</summary>
public class LatLng
{
  private double latitude;
  private double longitude;

  public LatLng()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Objects.FS.LatLng" /> class.
  /// </summary>
  /// <param name="latitude">The latitude.</param>
  /// <param name="longitude">The longitude.</param>
  public LatLng(double latitude, double longitude)
  {
    this.latitude = latitude;
    this.longitude = longitude;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Objects.FS.LatLng" /> class.
  /// </summary>
  /// <param name="latitude">The latitude.</param>
  /// <param name="longitude">The longitude.</param>
  public LatLng(Decimal? latitude, Decimal? longitude)
  {
    this.latitude = Convert.ToDouble((object) latitude);
    this.longitude = Convert.ToDouble((object) longitude);
  }

  internal LatLng(XmlElement locationElement, XmlNamespaceManager nameSpace)
  {
    this.latitude = double.Parse(locationElement.SelectSingleNode($".//{"bingSchema:"}Latitude", nameSpace).InnerText, (IFormatProvider) CultureInfo.InvariantCulture);
    this.longitude = double.Parse(locationElement.SelectSingleNode($".//{"bingSchema:"}Longitude", nameSpace).InnerText, (IFormatProvider) CultureInfo.InvariantCulture);
  }

  /// <summary>Gets the latitude.</summary>
  public double Latitude => this.latitude;

  /// <summary>Gets the longitude.</summary>
  public double Longitude => this.longitude;

  /// <summary>
  /// Returns a <see cref="T:System.String" /> that represents this instance.
  /// </summary>
  /// <returns>
  /// A <see cref="T:System.String" /> that represents this instance.
  /// </returns>
  public override string ToString() => $"{this.latitude.ToString()}, {this.longitude.ToString()}";
}
