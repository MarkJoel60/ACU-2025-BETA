// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.NonEmptyArrayOfAllItemsType
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
public class NonEmptyArrayOfAllItemsType
{
  private ItemType[] itemsField;

  /// <remarks />
  [XmlElement("AcceptItem", typeof (AcceptItemType))]
  [XmlElement("AcceptSharingInvitation", typeof (AcceptSharingInvitationType))]
  [XmlElement("CalendarItem", typeof (CalendarItemType))]
  [XmlElement("CancelCalendarItem", typeof (CancelCalendarItemType))]
  [XmlElement("Contact", typeof (ContactItemType))]
  [XmlElement("DeclineItem", typeof (DeclineItemType))]
  [XmlElement("DistributionList", typeof (DistributionListType))]
  [XmlElement("ForwardItem", typeof (ForwardItemType))]
  [XmlElement("Item", typeof (ItemType))]
  [XmlElement("MeetingCancellation", typeof (MeetingCancellationMessageType))]
  [XmlElement("MeetingMessage", typeof (MeetingMessageType))]
  [XmlElement("MeetingRequest", typeof (MeetingRequestMessageType))]
  [XmlElement("MeetingResponse", typeof (MeetingResponseMessageType))]
  [XmlElement("Message", typeof (MessageType))]
  [XmlElement("PostItem", typeof (PostItemType))]
  [XmlElement("PostReplyItem", typeof (PostReplyItemType))]
  [XmlElement("RemoveItem", typeof (RemoveItemType))]
  [XmlElement("ReplyAllToItem", typeof (ReplyAllToItemType))]
  [XmlElement("ReplyToItem", typeof (ReplyToItemType))]
  [XmlElement("SuppressReadReceipt", typeof (SuppressReadReceiptType))]
  [XmlElement("Task", typeof (TaskType))]
  [XmlElement("TentativelyAcceptItem", typeof (TentativelyAcceptItemType))]
  public ItemType[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
  }
}
