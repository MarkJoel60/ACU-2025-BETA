// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.CalendarEventDetails
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
public class CalendarEventDetails
{
  private string idField;
  private string subjectField;
  private string locationField;
  private bool isMeetingField;
  private bool isRecurringField;
  private bool isExceptionField;
  private bool isReminderSetField;
  private bool isPrivateField;

  /// <remarks />
  public string ID
  {
    get => this.idField;
    set => this.idField = value;
  }

  /// <remarks />
  public string Subject
  {
    get => this.subjectField;
    set => this.subjectField = value;
  }

  /// <remarks />
  public string Location
  {
    get => this.locationField;
    set => this.locationField = value;
  }

  /// <remarks />
  public bool IsMeeting
  {
    get => this.isMeetingField;
    set => this.isMeetingField = value;
  }

  /// <remarks />
  public bool IsRecurring
  {
    get => this.isRecurringField;
    set => this.isRecurringField = value;
  }

  /// <remarks />
  public bool IsException
  {
    get => this.isExceptionField;
    set => this.isExceptionField = value;
  }

  /// <remarks />
  public bool IsReminderSet
  {
    get => this.isReminderSetField;
    set => this.isReminderSetField = value;
  }

  /// <remarks />
  public bool IsPrivate
  {
    get => this.isPrivateField;
    set => this.isPrivateField = value;
  }
}
