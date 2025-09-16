// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MessageTrackingReportType
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
public class MessageTrackingReportType
{
  private EmailAddressType senderField;
  private EmailAddressType purportedSenderField;
  private string subjectField;
  private System.DateTime submitTimeField;
  private bool submitTimeFieldSpecified;
  private EmailAddressType[] originalRecipientsField;
  private RecipientTrackingEventType[] recipientTrackingEventsField;
  private TrackingPropertyType[] propertiesField;

  /// <remarks />
  public EmailAddressType Sender
  {
    get => this.senderField;
    set => this.senderField = value;
  }

  /// <remarks />
  public EmailAddressType PurportedSender
  {
    get => this.purportedSenderField;
    set => this.purportedSenderField = value;
  }

  /// <remarks />
  public string Subject
  {
    get => this.subjectField;
    set => this.subjectField = value;
  }

  /// <remarks />
  public System.DateTime SubmitTime
  {
    get => this.submitTimeField;
    set => this.submitTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool SubmitTimeSpecified
  {
    get => this.submitTimeFieldSpecified;
    set => this.submitTimeFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("Address", IsNullable = false)]
  public EmailAddressType[] OriginalRecipients
  {
    get => this.originalRecipientsField;
    set => this.originalRecipientsField = value;
  }

  /// <remarks />
  [XmlArrayItem("RecipientTrackingEvent", IsNullable = false)]
  public RecipientTrackingEventType[] RecipientTrackingEvents
  {
    get => this.recipientTrackingEventsField;
    set => this.recipientTrackingEventsField = value;
  }

  /// <remarks />
  [XmlArrayItem(IsNullable = false)]
  public TrackingPropertyType[] Properties
  {
    get => this.propertiesField;
    set => this.propertiesField = value;
  }
}
