// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.GLocation
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

#nullable disable
namespace PX.Objects.FS;

/// <summary>
/// Class representing a location, defined by name and/or by latitude/longitude.
/// </summary>
public class GLocation
{
  private LatLng latLng;
  private string locationName;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Objects.FS.GLocation" /> class.
  /// </summary>
  /// <param name="locationName">Name of the location.</param>
  public GLocation(string locationName) => this.locationName = locationName;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Objects.FS.GLocation" /> class.
  /// </summary>
  /// <param name="latLng">The latitude/longitude of the location.</param>
  public GLocation(LatLng latLng) => this.latLng = latLng;

  internal GLocation(LatLng latLng, string locationName)
  {
    this.latLng = latLng;
    this.locationName = locationName;
  }

  /// <summary>Gets the latitude/longitude of the location.</summary>
  public LatLng LatLng => this.latLng;

  /// <summary>Gets the name/address of the location.</summary>
  /// <value>The name/address of the location.</value>
  public string LocationName => this.locationName;

  /// <summary>
  /// Returns a <see cref="T:System.String" /> that represents this instance.
  /// </summary>
  /// <returns>
  /// A <see cref="T:System.String" /> that represents this instance.
  /// </returns>
  public override string ToString()
  {
    return this.locationName != null ? this.locationName : this.latLng.ToString();
  }
}
