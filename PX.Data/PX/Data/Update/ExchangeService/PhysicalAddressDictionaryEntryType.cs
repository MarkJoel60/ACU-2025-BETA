// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PhysicalAddressDictionaryEntryType
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
public class PhysicalAddressDictionaryEntryType
{
  private string streetField;
  private string cityField;
  private string stateField;
  private string countryOrRegionField;
  private string postalCodeField;
  private PhysicalAddressKeyType keyField;

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
  public string CountryOrRegion
  {
    get => this.countryOrRegionField;
    set => this.countryOrRegionField = value;
  }

  /// <remarks />
  public string PostalCode
  {
    get => this.postalCodeField;
    set => this.postalCodeField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public PhysicalAddressKeyType Key
  {
    get => this.keyField;
    set => this.keyField = value;
  }
}
