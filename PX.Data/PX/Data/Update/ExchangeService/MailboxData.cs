// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MailboxData
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
public class MailboxData
{
  private EmailAddress emailField;
  private MeetingAttendeeType attendeeTypeField;
  private bool excludeConflictsField;
  private bool excludeConflictsFieldSpecified;

  /// <remarks />
  public EmailAddress Email
  {
    get => this.emailField;
    set => this.emailField = value;
  }

  /// <remarks />
  public MeetingAttendeeType AttendeeType
  {
    get => this.attendeeTypeField;
    set => this.attendeeTypeField = value;
  }

  /// <remarks />
  public bool ExcludeConflicts
  {
    get => this.excludeConflictsField;
    set => this.excludeConflictsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ExcludeConflictsSpecified
  {
    get => this.excludeConflictsFieldSpecified;
    set => this.excludeConflictsFieldSpecified = value;
  }
}
