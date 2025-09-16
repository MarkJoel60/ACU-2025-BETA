// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MailTipsServiceConfiguration
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
public class MailTipsServiceConfiguration : ServiceConfiguration
{
  private bool mailTipsEnabledField;
  private int maxRecipientsPerGetMailTipsRequestField;
  private int maxMessageSizeField;
  private int largeAudienceThresholdField;
  private bool showExternalRecipientCountField;
  private SmtpDomain[] internalDomainsField;
  private bool policyTipsEnabledField;
  private int largeAudienceCapField;

  /// <remarks />
  public bool MailTipsEnabled
  {
    get => this.mailTipsEnabledField;
    set => this.mailTipsEnabledField = value;
  }

  /// <remarks />
  public int MaxRecipientsPerGetMailTipsRequest
  {
    get => this.maxRecipientsPerGetMailTipsRequestField;
    set => this.maxRecipientsPerGetMailTipsRequestField = value;
  }

  /// <remarks />
  public int MaxMessageSize
  {
    get => this.maxMessageSizeField;
    set => this.maxMessageSizeField = value;
  }

  /// <remarks />
  public int LargeAudienceThreshold
  {
    get => this.largeAudienceThresholdField;
    set => this.largeAudienceThresholdField = value;
  }

  /// <remarks />
  public bool ShowExternalRecipientCount
  {
    get => this.showExternalRecipientCountField;
    set => this.showExternalRecipientCountField = value;
  }

  /// <remarks />
  [XmlArrayItem("Domain", IsNullable = false)]
  public SmtpDomain[] InternalDomains
  {
    get => this.internalDomainsField;
    set => this.internalDomainsField = value;
  }

  /// <remarks />
  public bool PolicyTipsEnabled
  {
    get => this.policyTipsEnabledField;
    set => this.policyTipsEnabledField = value;
  }

  /// <remarks />
  public int LargeAudienceCap
  {
    get => this.largeAudienceCapField;
    set => this.largeAudienceCapField = value;
  }
}
