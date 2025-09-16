// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.AppendToItemFieldType
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
public class AppendToItemFieldType : ItemChangeDescriptionType
{
  private ItemType item1Field;

  /// <remarks />
  [XmlElement("CalendarItem", typeof (CalendarItemType))]
  [XmlElement("Contact", typeof (ContactItemType))]
  [XmlElement("DistributionList", typeof (DistributionListType))]
  [XmlElement("Item", typeof (ItemType))]
  [XmlElement("MeetingCancellation", typeof (MeetingCancellationMessageType))]
  [XmlElement("MeetingMessage", typeof (MeetingMessageType))]
  [XmlElement("MeetingRequest", typeof (MeetingRequestMessageType))]
  [XmlElement("MeetingResponse", typeof (MeetingResponseMessageType))]
  [XmlElement("Message", typeof (MessageType))]
  [XmlElement("PostItem", typeof (PostItemType))]
  [XmlElement("Task", typeof (TaskType))]
  public ItemType Item1
  {
    get => this.item1Field;
    set => this.item1Field = value;
  }
}
