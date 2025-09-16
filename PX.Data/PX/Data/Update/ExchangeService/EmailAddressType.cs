// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.EmailAddressType
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
public class EmailAddressType : BaseEmailAddressType
{
  private string nameField;
  private string emailAddressField;
  private string routingTypeField;
  private MailboxTypeType mailboxTypeField;
  private bool mailboxTypeFieldSpecified;
  private ItemIdType itemIdField;
  private string originalDisplayNameField;

  /// <remarks />
  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  /// <remarks />
  public string EmailAddress
  {
    get => this.emailAddressField;
    set => this.emailAddressField = value;
  }

  /// <remarks />
  public string RoutingType
  {
    get => this.routingTypeField;
    set => this.routingTypeField = value;
  }

  /// <remarks />
  public MailboxTypeType MailboxType
  {
    get => this.mailboxTypeField;
    set => this.mailboxTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MailboxTypeSpecified
  {
    get => this.mailboxTypeFieldSpecified;
    set => this.mailboxTypeFieldSpecified = value;
  }

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  public string OriginalDisplayName
  {
    get => this.originalDisplayNameField;
    set => this.originalDisplayNameField = value;
  }
}
