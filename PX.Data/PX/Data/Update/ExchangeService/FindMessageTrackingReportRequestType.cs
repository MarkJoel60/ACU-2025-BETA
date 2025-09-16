// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindMessageTrackingReportRequestType
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
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public class FindMessageTrackingReportRequestType : BaseRequestType
{
  private string scopeField;
  private string domainField;
  private EmailAddressType senderField;
  private EmailAddressType purportedSenderField;
  private EmailAddressType recipientField;
  private string subjectField;
  private System.DateTime startDateTimeField;
  private bool startDateTimeFieldSpecified;
  private System.DateTime endDateTimeField;
  private bool endDateTimeFieldSpecified;
  private string messageIdField;
  private EmailAddressType federatedDeliveryMailboxField;
  private string diagnosticsLevelField;
  private string serverHintField;
  private TrackingPropertyType[] propertiesField;

  /// <remarks />
  public string Scope
  {
    get => this.scopeField;
    set => this.scopeField = value;
  }

  /// <remarks />
  public string Domain
  {
    get => this.domainField;
    set => this.domainField = value;
  }

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
  public EmailAddressType Recipient
  {
    get => this.recipientField;
    set => this.recipientField = value;
  }

  /// <remarks />
  public string Subject
  {
    get => this.subjectField;
    set => this.subjectField = value;
  }

  /// <remarks />
  public System.DateTime StartDateTime
  {
    get => this.startDateTimeField;
    set => this.startDateTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartDateTimeSpecified
  {
    get => this.startDateTimeFieldSpecified;
    set => this.startDateTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime EndDateTime
  {
    get => this.endDateTimeField;
    set => this.endDateTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndDateTimeSpecified
  {
    get => this.endDateTimeFieldSpecified;
    set => this.endDateTimeFieldSpecified = value;
  }

  /// <remarks />
  public string MessageId
  {
    get => this.messageIdField;
    set => this.messageIdField = value;
  }

  /// <remarks />
  public EmailAddressType FederatedDeliveryMailbox
  {
    get => this.federatedDeliveryMailboxField;
    set => this.federatedDeliveryMailboxField = value;
  }

  /// <remarks />
  public string DiagnosticsLevel
  {
    get => this.diagnosticsLevelField;
    set => this.diagnosticsLevelField = value;
  }

  /// <remarks />
  public string ServerHint
  {
    get => this.serverHintField;
    set => this.serverHintField = value;
  }

  /// <remarks />
  [XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public TrackingPropertyType[] Properties
  {
    get => this.propertiesField;
    set => this.propertiesField = value;
  }
}
