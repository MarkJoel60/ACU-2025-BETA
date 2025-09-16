// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MailTips
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
public class MailTips
{
  private EmailAddressType recipientAddressField;
  private MailTipTypes pendingMailTipsField;
  private OutOfOfficeMailTip outOfOfficeField;
  private bool mailboxFullField;
  private bool mailboxFullFieldSpecified;
  private string customMailTipField;
  private int totalMemberCountField;
  private bool totalMemberCountFieldSpecified;
  private int externalMemberCountField;
  private bool externalMemberCountFieldSpecified;
  private int maxMessageSizeField;
  private bool maxMessageSizeFieldSpecified;
  private bool deliveryRestrictedField;
  private bool deliveryRestrictedFieldSpecified;
  private bool isModeratedField;
  private bool isModeratedFieldSpecified;
  private bool invalidRecipientField;
  private bool invalidRecipientFieldSpecified;
  private int scopeField;
  private bool scopeFieldSpecified;

  /// <remarks />
  public EmailAddressType RecipientAddress
  {
    get => this.recipientAddressField;
    set => this.recipientAddressField = value;
  }

  /// <remarks />
  public MailTipTypes PendingMailTips
  {
    get => this.pendingMailTipsField;
    set => this.pendingMailTipsField = value;
  }

  /// <remarks />
  public OutOfOfficeMailTip OutOfOffice
  {
    get => this.outOfOfficeField;
    set => this.outOfOfficeField = value;
  }

  /// <remarks />
  public bool MailboxFull
  {
    get => this.mailboxFullField;
    set => this.mailboxFullField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MailboxFullSpecified
  {
    get => this.mailboxFullFieldSpecified;
    set => this.mailboxFullFieldSpecified = value;
  }

  /// <remarks />
  public string CustomMailTip
  {
    get => this.customMailTipField;
    set => this.customMailTipField = value;
  }

  /// <remarks />
  public int TotalMemberCount
  {
    get => this.totalMemberCountField;
    set => this.totalMemberCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TotalMemberCountSpecified
  {
    get => this.totalMemberCountFieldSpecified;
    set => this.totalMemberCountFieldSpecified = value;
  }

  /// <remarks />
  public int ExternalMemberCount
  {
    get => this.externalMemberCountField;
    set => this.externalMemberCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ExternalMemberCountSpecified
  {
    get => this.externalMemberCountFieldSpecified;
    set => this.externalMemberCountFieldSpecified = value;
  }

  /// <remarks />
  public int MaxMessageSize
  {
    get => this.maxMessageSizeField;
    set => this.maxMessageSizeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MaxMessageSizeSpecified
  {
    get => this.maxMessageSizeFieldSpecified;
    set => this.maxMessageSizeFieldSpecified = value;
  }

  /// <remarks />
  public bool DeliveryRestricted
  {
    get => this.deliveryRestrictedField;
    set => this.deliveryRestrictedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DeliveryRestrictedSpecified
  {
    get => this.deliveryRestrictedFieldSpecified;
    set => this.deliveryRestrictedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsModerated
  {
    get => this.isModeratedField;
    set => this.isModeratedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsModeratedSpecified
  {
    get => this.isModeratedFieldSpecified;
    set => this.isModeratedFieldSpecified = value;
  }

  /// <remarks />
  public bool InvalidRecipient
  {
    get => this.invalidRecipientField;
    set => this.invalidRecipientField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool InvalidRecipientSpecified
  {
    get => this.invalidRecipientFieldSpecified;
    set => this.invalidRecipientFieldSpecified = value;
  }

  /// <remarks />
  public int Scope
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
}
