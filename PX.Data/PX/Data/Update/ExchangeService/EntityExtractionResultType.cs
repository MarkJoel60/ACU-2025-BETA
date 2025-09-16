// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.EntityExtractionResultType
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
public class EntityExtractionResultType
{
  private AddressEntityType[] addressesField;
  private MeetingSuggestionType[] meetingSuggestionsField;
  private TaskSuggestionType[] taskSuggestionsField;
  private EmailAddressEntityType[] emailAddressesField;
  private ContactType[] contactsField;
  private UrlEntityType[] urlsField;
  private PhoneEntityType[] phoneNumbersField;

  /// <remarks />
  [XmlArrayItem("AddressEntity", IsNullable = false)]
  public AddressEntityType[] Addresses
  {
    get => this.addressesField;
    set => this.addressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("MeetingSuggestion", IsNullable = false)]
  public MeetingSuggestionType[] MeetingSuggestions
  {
    get => this.meetingSuggestionsField;
    set => this.meetingSuggestionsField = value;
  }

  /// <remarks />
  [XmlArrayItem("TaskSuggestion", IsNullable = false)]
  public TaskSuggestionType[] TaskSuggestions
  {
    get => this.taskSuggestionsField;
    set => this.taskSuggestionsField = value;
  }

  /// <remarks />
  [XmlArrayItem("EmailAddressEntity", IsNullable = false)]
  public EmailAddressEntityType[] EmailAddresses
  {
    get => this.emailAddressesField;
    set => this.emailAddressesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Contact", IsNullable = false)]
  public ContactType[] Contacts
  {
    get => this.contactsField;
    set => this.contactsField = value;
  }

  /// <remarks />
  [XmlArrayItem("UrlEntity", IsNullable = false)]
  public UrlEntityType[] Urls
  {
    get => this.urlsField;
    set => this.urlsField = value;
  }

  /// <remarks />
  [XmlArrayItem("Phone", IsNullable = false)]
  public PhoneEntityType[] PhoneNumbers
  {
    get => this.phoneNumbersField;
    set => this.phoneNumbersField = value;
  }
}
