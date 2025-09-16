// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ClientExtensionType
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
public class ClientExtensionType
{
  private string[] specificUsersField;
  private byte[] manifestField;
  private bool isAvailableField;
  private bool isAvailableFieldSpecified;
  private bool isMandatoryField;
  private bool isMandatoryFieldSpecified;
  private bool isEnabledByDefaultField;
  private bool isEnabledByDefaultFieldSpecified;
  private ClientExtensionProvidedToType providedToField;
  private bool providedToFieldSpecified;
  private ClientExtensionTypeType typeField;
  private bool typeFieldSpecified;
  private ClientExtensionScopeType scopeField;
  private bool scopeFieldSpecified;
  private string marketplaceAssetIdField;
  private string marketplaceContentMarketField;
  private string appStatusField;
  private string etokenField;

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] SpecificUsers
  {
    get => this.specificUsersField;
    set => this.specificUsersField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] Manifest
  {
    get => this.manifestField;
    set => this.manifestField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool IsAvailable
  {
    get => this.isAvailableField;
    set => this.isAvailableField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsAvailableSpecified
  {
    get => this.isAvailableFieldSpecified;
    set => this.isAvailableFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool IsMandatory
  {
    get => this.isMandatoryField;
    set => this.isMandatoryField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsMandatorySpecified
  {
    get => this.isMandatoryFieldSpecified;
    set => this.isMandatoryFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool IsEnabledByDefault
  {
    get => this.isEnabledByDefaultField;
    set => this.isEnabledByDefaultField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsEnabledByDefaultSpecified
  {
    get => this.isEnabledByDefaultFieldSpecified;
    set => this.isEnabledByDefaultFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ClientExtensionProvidedToType ProvidedTo
  {
    get => this.providedToField;
    set => this.providedToField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ProvidedToSpecified
  {
    get => this.providedToFieldSpecified;
    set => this.providedToFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ClientExtensionTypeType Type
  {
    get => this.typeField;
    set => this.typeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TypeSpecified
  {
    get => this.typeFieldSpecified;
    set => this.typeFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public ClientExtensionScopeType Scope
  {
    get => this.scopeField;
    set => this.scopeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ScopeSpecified
  {
    get => this.scopeFieldSpecified;
    set => this.scopeFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string MarketplaceAssetId
  {
    get => this.marketplaceAssetIdField;
    set => this.marketplaceAssetIdField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string MarketplaceContentMarket
  {
    get => this.marketplaceContentMarketField;
    set => this.marketplaceContentMarketField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string AppStatus
  {
    get => this.appStatusField;
    set => this.appStatusField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string Etoken
  {
    get => this.etokenField;
    set => this.etokenField = value;
  }
}
