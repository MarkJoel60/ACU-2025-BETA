// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ReminderMessageDataType
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
public class ReminderMessageDataType
{
  private string reminderTextField;
  private string locationField;
  private System.DateTime startTimeField;
  private bool startTimeFieldSpecified;
  private System.DateTime endTimeField;
  private bool endTimeFieldSpecified;
  private ItemIdType associatedCalendarItemIdField;

  /// <remarks />
  public string ReminderText
  {
    get => this.reminderTextField;
    set => this.reminderTextField = value;
  }

  /// <remarks />
  public string Location
  {
    get => this.locationField;
    set => this.locationField = value;
  }

  /// <remarks />
  public System.DateTime StartTime
  {
    get => this.startTimeField;
    set => this.startTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartTimeSpecified
  {
    get => this.startTimeFieldSpecified;
    set => this.startTimeFieldSpecified = value;
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
  public ItemIdType AssociatedCalendarItemId
  {
    get => this.associatedCalendarItemIdField;
    set => this.associatedCalendarItemIdField = value;
  }
}
