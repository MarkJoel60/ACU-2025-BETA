// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetMessageTrackingReportRequestType
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
public class GetMessageTrackingReportRequestType : BaseRequestType
{
  private string scopeField;
  private MessageTrackingReportTemplateType reportTemplateField;
  private EmailAddressType recipientFilterField;
  private string messageTrackingReportIdField;
  private bool returnQueueEventsField;
  private bool returnQueueEventsFieldSpecified;
  private string diagnosticsLevelField;
  private TrackingPropertyType[] propertiesField;

  /// <remarks />
  public string Scope
  {
    get => this.scopeField;
    set => this.scopeField = value;
  }

  /// <remarks />
  public MessageTrackingReportTemplateType ReportTemplate
  {
    get => this.reportTemplateField;
    set => this.reportTemplateField = value;
  }

  /// <remarks />
  public EmailAddressType RecipientFilter
  {
    get => this.recipientFilterField;
    set => this.recipientFilterField = value;
  }

  /// <remarks />
  public string MessageTrackingReportId
  {
    get => this.messageTrackingReportIdField;
    set => this.messageTrackingReportIdField = value;
  }

  /// <remarks />
  public bool ReturnQueueEvents
  {
    get => this.returnQueueEventsField;
    set => this.returnQueueEventsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReturnQueueEventsSpecified
  {
    get => this.returnQueueEventsFieldSpecified;
    set => this.returnQueueEventsFieldSpecified = value;
  }

  /// <remarks />
  public string DiagnosticsLevel
  {
    get => this.diagnosticsLevelField;
    set => this.diagnosticsLevelField = value;
  }

  /// <remarks />
  [XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public TrackingPropertyType[] Properties
  {
    get => this.propertiesField;
    set => this.propertiesField = value;
  }
}
