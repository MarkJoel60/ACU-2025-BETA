// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.NotificationType
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
public class NotificationType
{
  private string subscriptionIdField;
  private string previousWatermarkField;
  private bool moreEventsField;
  private bool moreEventsFieldSpecified;
  private BaseNotificationEventType[] itemsField;
  private ItemsChoiceType[] itemsElementNameField;

  /// <remarks />
  public string SubscriptionId
  {
    get => this.subscriptionIdField;
    set => this.subscriptionIdField = value;
  }

  /// <remarks />
  public string PreviousWatermark
  {
    get => this.previousWatermarkField;
    set => this.previousWatermarkField = value;
  }

  /// <remarks />
  public bool MoreEvents
  {
    get => this.moreEventsField;
    set => this.moreEventsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MoreEventsSpecified
  {
    get => this.moreEventsFieldSpecified;
    set => this.moreEventsFieldSpecified = value;
  }

  /// <remarks />
  [XmlElement("CopiedEvent", typeof (MovedCopiedEventType))]
  [XmlElement("CreatedEvent", typeof (BaseObjectChangedEventType))]
  [XmlElement("DeletedEvent", typeof (BaseObjectChangedEventType))]
  [XmlElement("FreeBusyChangedEvent", typeof (BaseObjectChangedEventType))]
  [XmlElement("ModifiedEvent", typeof (ModifiedEventType))]
  [XmlElement("MovedEvent", typeof (MovedCopiedEventType))]
  [XmlElement("NewMailEvent", typeof (BaseObjectChangedEventType))]
  [XmlElement("StatusEvent", typeof (BaseNotificationEventType))]
  [XmlChoiceIdentifier("ItemsElementName")]
  public BaseNotificationEventType[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
  }

  /// <remarks />
  [XmlElement("ItemsElementName")]
  [XmlIgnore]
  public ItemsChoiceType[] ItemsElementName
  {
    get => this.itemsElementNameField;
    set => this.itemsElementNameField = value;
  }
}
