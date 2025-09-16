// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ContactType
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
public class ContactType : EntityType
{
  private string personNameField;
  private string businessNameField;
  private PhoneType[] phoneNumbersField;
  private string[] urlsField;
  private string[] emailAddressesField;
  private string[] addressesField;
  private string contactStringField;

  /// <remarks />
  public string PersonName
  {
    get => this.personNameField;
    set => this.personNameField = value;
  }

  /// <remarks />
  public string BusinessName
  {
    get => this.businessNameField;
    set => this.businessNameField = value;
  }

  /// <remarks />
  [XmlArrayItem("Phone", IsNullable = false)]
  public PhoneType[] PhoneNumbers
  {
    get => this.phoneNumbersField;
    set => this.phoneNumbersField = value;
  }

  /// <remarks />
  [XmlArrayItem("Url", IsNullable = false)]
  public string[] Urls
  {
    get => this.urlsField;
    set => this.urlsField = value;
  }

  /// <remarks />
  [XmlArrayItem("EmailAddress", IsNullable = false)]
  public string[] EmailAddresses
  {
    get => this.emailAddressesField;
    set => this.emailAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public string[] Addresses
  {
    get => this.addressesField;
    set => this.addressesField = value;
  }

  /// <remarks />
  public string ContactString
  {
    get => this.contactStringField;
    set => this.contactStringField = value;
  }
}
