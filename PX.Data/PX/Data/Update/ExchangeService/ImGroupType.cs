// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ImGroupType
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
public class ImGroupType
{
  private string displayNameField;
  private string groupTypeField;
  private ItemIdType exchangeStoreIdField;
  private ItemIdType[] memberCorrelationKeyField;
  private NonEmptyArrayOfExtendedPropertyType extendedPropertiesField;
  private string smtpAddressField;

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public string GroupType
  {
    get => this.groupTypeField;
    set => this.groupTypeField = value;
  }

  /// <remarks />
  public ItemIdType ExchangeStoreId
  {
    get => this.exchangeStoreIdField;
    set => this.exchangeStoreIdField = value;
  }

  /// <remarks />
  [XmlArrayItem("ItemId", IsNullable = false)]
  public ItemIdType[] MemberCorrelationKey
  {
    get => this.memberCorrelationKeyField;
    set => this.memberCorrelationKeyField = value;
  }

  /// <remarks />
  public NonEmptyArrayOfExtendedPropertyType ExtendedProperties
  {
    get => this.extendedPropertiesField;
    set => this.extendedPropertiesField = value;
  }

  /// <remarks />
  public string SmtpAddress
  {
    get => this.smtpAddressField;
    set => this.smtpAddressField = value;
  }
}
