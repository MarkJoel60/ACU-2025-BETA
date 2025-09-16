// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindMessageTrackingSearchResultType
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
public class FindMessageTrackingSearchResultType
{
  private string subjectField;
  private EmailAddressType senderField;
  private EmailAddressType purportedSenderField;
  private EmailAddressType[] recipientsField;
  private System.DateTime submittedTimeField;
  private string messageTrackingReportIdField;
  private string previousHopServerField;
  private string firstHopServerField;
  private TrackingPropertyType[] propertiesField;

  /// <remarks />
  public string Subject
  {
    get => this.subjectField;
    set => this.subjectField = value;
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
  [XmlArrayItem("Mailbox", IsNullable = false)]
  public EmailAddressType[] Recipients
  {
    get => this.recipientsField;
    set => this.recipientsField = value;
  }

  /// <remarks />
  public System.DateTime SubmittedTime
  {
    get => this.submittedTimeField;
    set => this.submittedTimeField = value;
  }

  /// <remarks />
  public string MessageTrackingReportId
  {
    get => this.messageTrackingReportIdField;
    set => this.messageTrackingReportIdField = value;
  }

  /// <remarks />
  public string PreviousHopServer
  {
    get => this.previousHopServerField;
    set => this.previousHopServerField = value;
  }

  /// <remarks />
  public string FirstHopServer
  {
    get => this.firstHopServerField;
    set => this.firstHopServerField = value;
  }

  /// <remarks />
  [XmlArrayItem(IsNullable = false)]
  public TrackingPropertyType[] Properties
  {
    get => this.propertiesField;
    set => this.propertiesField = value;
  }
}
