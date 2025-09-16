// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.AttendeeType
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
public class AttendeeType
{
  private EmailAddressType mailboxField;
  private ResponseTypeType responseTypeField;
  private bool responseTypeFieldSpecified;
  private System.DateTime lastResponseTimeField;
  private bool lastResponseTimeFieldSpecified;
  private System.DateTime proposedStartField;
  private bool proposedStartFieldSpecified;
  private System.DateTime proposedEndField;
  private bool proposedEndFieldSpecified;

  /// <remarks />
  public EmailAddressType Mailbox
  {
    get => this.mailboxField;
    set => this.mailboxField = value;
  }

  /// <remarks />
  public ResponseTypeType ResponseType
  {
    get => this.responseTypeField;
    set => this.responseTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ResponseTypeSpecified
  {
    get => this.responseTypeFieldSpecified;
    set => this.responseTypeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime LastResponseTime
  {
    get => this.lastResponseTimeField;
    set => this.lastResponseTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LastResponseTimeSpecified
  {
    get => this.lastResponseTimeFieldSpecified;
    set => this.lastResponseTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime ProposedStart
  {
    get => this.proposedStartField;
    set => this.proposedStartField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ProposedStartSpecified
  {
    get => this.proposedStartFieldSpecified;
    set => this.proposedStartFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime ProposedEnd
  {
    get => this.proposedEndField;
    set => this.proposedEndField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ProposedEndSpecified
  {
    get => this.proposedEndFieldSpecified;
    set => this.proposedEndFieldSpecified = value;
  }
}
