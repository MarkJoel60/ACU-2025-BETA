// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MessageType
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
[XmlInclude(typeof (MeetingMessageType))]
[XmlInclude(typeof (MeetingCancellationMessageType))]
[XmlInclude(typeof (MeetingResponseMessageType))]
[XmlInclude(typeof (MeetingRequestMessageType))]
[XmlInclude(typeof (ResponseObjectCoreType))]
[XmlInclude(typeof (ResponseObjectType))]
[XmlInclude(typeof (PostReplyItemBaseType))]
[XmlInclude(typeof (PostReplyItemType))]
[XmlInclude(typeof (AddItemToMyCalendarType))]
[XmlInclude(typeof (RemoveItemType))]
[XmlInclude(typeof (ProposeNewTimeType))]
[XmlInclude(typeof (ReferenceItemResponseType))]
[XmlInclude(typeof (AcceptSharingInvitationType))]
[XmlInclude(typeof (SuppressReadReceiptType))]
[XmlInclude(typeof (SmartResponseBaseType))]
[XmlInclude(typeof (SmartResponseType))]
[XmlInclude(typeof (CancelCalendarItemType))]
[XmlInclude(typeof (ForwardItemType))]
[XmlInclude(typeof (ReplyAllToItemType))]
[XmlInclude(typeof (ReplyToItemType))]
[XmlInclude(typeof (WellKnownResponseObjectType))]
[XmlInclude(typeof (MeetingRegistrationResponseObjectType))]
[XmlInclude(typeof (DeclineItemType))]
[XmlInclude(typeof (TentativelyAcceptItemType))]
[XmlInclude(typeof (AcceptItemType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class MessageType : ItemType
{
  private SingleRecipientType senderField;
  private EmailAddressType[] toRecipientsField;
  private EmailAddressType[] ccRecipientsField;
  private EmailAddressType[] bccRecipientsField;
  private bool isReadReceiptRequestedField;
  private bool isReadReceiptRequestedFieldSpecified;
  private bool isDeliveryReceiptRequestedField;
  private bool isDeliveryReceiptRequestedFieldSpecified;
  private byte[] conversationIndexField;
  private string conversationTopicField;
  private SingleRecipientType fromField;
  private string internetMessageIdField;
  private bool isReadField;
  private bool isReadFieldSpecified;
  private bool isResponseRequestedField;
  private bool isResponseRequestedFieldSpecified;
  private string referencesField;
  private EmailAddressType[] replyToField;
  private SingleRecipientType receivedByField;
  private SingleRecipientType receivedRepresentingField;
  private ApprovalRequestDataType approvalRequestDataField;
  private VotingInformationType votingInformationField;
  private ReminderMessageDataType reminderMessageDataField;

  /// <remarks />
  public SingleRecipientType Sender
  {
    get => this.senderField;
    set => this.senderField = value;
  }

  /// <remarks />
  [XmlArrayItem("Mailbox", IsNullable = false)]
  public EmailAddressType[] ToRecipients
  {
    get => this.toRecipientsField;
    set => this.toRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("Mailbox", IsNullable = false)]
  public EmailAddressType[] CcRecipients
  {
    get => this.ccRecipientsField;
    set => this.ccRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("Mailbox", IsNullable = false)]
  public EmailAddressType[] BccRecipients
  {
    get => this.bccRecipientsField;
    set => this.bccRecipientsField = value;
  }

  /// <remarks />
  public bool IsReadReceiptRequested
  {
    get => this.isReadReceiptRequestedField;
    set => this.isReadReceiptRequestedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsReadReceiptRequestedSpecified
  {
    get => this.isReadReceiptRequestedFieldSpecified;
    set => this.isReadReceiptRequestedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsDeliveryReceiptRequested
  {
    get => this.isDeliveryReceiptRequestedField;
    set => this.isDeliveryReceiptRequestedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsDeliveryReceiptRequestedSpecified
  {
    get => this.isDeliveryReceiptRequestedFieldSpecified;
    set => this.isDeliveryReceiptRequestedFieldSpecified = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] ConversationIndex
  {
    get => this.conversationIndexField;
    set => this.conversationIndexField = value;
  }

  /// <remarks />
  public string ConversationTopic
  {
    get => this.conversationTopicField;
    set => this.conversationTopicField = value;
  }

  /// <remarks />
  public SingleRecipientType From
  {
    get => this.fromField;
    set => this.fromField = value;
  }

  /// <remarks />
  public string InternetMessageId
  {
    get => this.internetMessageIdField;
    set => this.internetMessageIdField = value;
  }

  /// <remarks />
  public bool IsRead
  {
    get => this.isReadField;
    set => this.isReadField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsReadSpecified
  {
    get => this.isReadFieldSpecified;
    set => this.isReadFieldSpecified = value;
  }

  /// <remarks />
  public bool IsResponseRequested
  {
    get => this.isResponseRequestedField;
    set => this.isResponseRequestedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsResponseRequestedSpecified
  {
    get => this.isResponseRequestedFieldSpecified;
    set => this.isResponseRequestedFieldSpecified = value;
  }

  /// <remarks />
  public string References
  {
    get => this.referencesField;
    set => this.referencesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Mailbox", IsNullable = false)]
  public EmailAddressType[] ReplyTo
  {
    get => this.replyToField;
    set => this.replyToField = value;
  }

  /// <remarks />
  public SingleRecipientType ReceivedBy
  {
    get => this.receivedByField;
    set => this.receivedByField = value;
  }

  /// <remarks />
  public SingleRecipientType ReceivedRepresenting
  {
    get => this.receivedRepresentingField;
    set => this.receivedRepresentingField = value;
  }

  /// <remarks />
  public ApprovalRequestDataType ApprovalRequestData
  {
    get => this.approvalRequestDataField;
    set => this.approvalRequestDataField = value;
  }

  /// <remarks />
  public VotingInformationType VotingInformation
  {
    get => this.votingInformationField;
    set => this.votingInformationField = value;
  }

  /// <remarks />
  public ReminderMessageDataType ReminderMessageData
  {
    get => this.reminderMessageDataField;
    set => this.reminderMessageDataField = value;
  }
}
