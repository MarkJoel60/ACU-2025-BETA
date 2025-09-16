// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GetRemindersType
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
public class GetRemindersType : BaseRequestType
{
  private System.DateTime beginTimeField;
  private bool beginTimeFieldSpecified;
  private System.DateTime endTimeField;
  private bool endTimeFieldSpecified;
  private int maxItemsField;
  private bool maxItemsFieldSpecified;
  private GetRemindersTypeReminderType reminderTypeField;
  private bool reminderTypeFieldSpecified;

  /// <remarks />
  public System.DateTime BeginTime
  {
    get => this.beginTimeField;
    set => this.beginTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool BeginTimeSpecified
  {
    get => this.beginTimeFieldSpecified;
    set => this.beginTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime EndTime
  {
    get => this.endTimeField;
    set => this.endTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndTimeSpecified
  {
    get => this.endTimeFieldSpecified;
    set => this.endTimeFieldSpecified = value;
  }

  /// <remarks />
  public int MaxItems
  {
    get => this.maxItemsField;
    set => this.maxItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MaxItemsSpecified
  {
    get => this.maxItemsFieldSpecified;
    set => this.maxItemsFieldSpecified = value;
  }

  /// <remarks />
  public GetRemindersTypeReminderType ReminderType
  {
    get => this.reminderTypeField;
    set => this.reminderTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReminderTypeSpecified
  {
    get => this.reminderTypeFieldSpecified;
    set => this.reminderTypeFieldSpecified = value;
  }
}
