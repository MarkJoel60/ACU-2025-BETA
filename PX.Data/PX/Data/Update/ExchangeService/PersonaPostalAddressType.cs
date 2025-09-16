// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PersonaPostalAddressType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class PersonaPostalAddressType
{
  private string streetField;
  private string cityField;
  private string stateField;
  private string countryField;
  private string postalCodeField;
  private string postOfficeBoxField;
  private string typeField;
  private double latitudeField;
  private bool latitudeFieldSpecified;
  private double longitudeField;
  private bool longitudeFieldSpecified;
  private double accuracyField;
  private bool accuracyFieldSpecified;
  private double altitudeField;
  private bool altitudeFieldSpecified;
  private double altitudeAccuracyField;
  private bool altitudeAccuracyFieldSpecified;
  private string formattedAddressField;
  private string locationUriField;
  private LocationSourceType locationSourceField;
  private bool locationSourceFieldSpecified;

  /// <remarks />
  public string Street
  {
    get => this.streetField;
    set => this.streetField = value;
  }

  /// <remarks />
  public string City
  {
    get => this.cityField;
    set => this.cityField = value;
  }

  /// <remarks />
  public string State
  {
    get => this.stateField;
    set => this.stateField = value;
  }

  /// <remarks />
  public string Country
  {
    get => this.countryField;
    set => this.countryField = value;
  }

  /// <remarks />
  public string PostalCode
  {
    get => this.postalCodeField;
    set => this.postalCodeField = value;
  }

  /// <remarks />
  public string PostOfficeBox
  {
    get => this.postOfficeBoxField;
    set => this.postOfficeBoxField = value;
  }

  /// <remarks />
  public string Type
  {
    get => this.typeField;
    set => this.typeField = value;
  }

  /// <remarks />
  public double Latitude
  {
    get => this.latitudeField;
    set => this.latitudeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LatitudeSpecified
  {
    get => this.latitudeFieldSpecified;
    set => this.latitudeFieldSpecified = value;
  }

  /// <remarks />
  public double Longitude
  {
    get => this.longitudeField;
    set => this.longitudeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LongitudeSpecified
  {
    get => this.longitudeFieldSpecified;
    set => this.longitudeFieldSpecified = value;
  }

  /// <remarks />
  public double Accuracy
  {
    get => this.accuracyField;
    set => this.accuracyField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AccuracySpecified
  {
    get => this.accuracyFieldSpecified;
    set => this.accuracyFieldSpecified = value;
  }

  /// <remarks />
  public double Altitude
  {
    get => this.altitudeField;
    set => this.altitudeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AltitudeSpecified
  {
    get => this.altitudeFieldSpecified;
    set => this.altitudeFieldSpecified = value;
  }

  /// <remarks />
  public double AltitudeAccuracy
  {
    get => this.altitudeAccuracyField;
    set => this.altitudeAccuracyField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AltitudeAccuracySpecified
  {
    get => this.altitudeAccuracyFieldSpecified;
    set => this.altitudeAccuracyFieldSpecified = value;
  }

  /// <remarks />
  public string FormattedAddress
  {
    get => this.formattedAddressField;
    set => this.formattedAddressField = value;
  }

  /// <remarks />
  public string LocationUri
  {
    get => this.locationUriField;
    set => this.locationUriField = value;
  }

  /// <remarks />
  public LocationSourceType LocationSource
  {
    get => this.locationSourceField;
    set => this.locationSourceField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LocationSourceSpecified
  {
    get => this.locationSourceFieldSpecified;
    set => this.locationSourceFieldSpecified = value;
  }
}
