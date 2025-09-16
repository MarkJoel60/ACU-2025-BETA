// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ReminderType
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
public class ReminderType
{
  private string subjectField;
  private string locationField;
  private System.DateTime reminderTimeField;
  private System.DateTime startDateField;
  private System.DateTime endDateField;
  private ItemIdType itemIdField;
  private ItemIdType recurringMasterItemIdField;
  private ReminderGroupType reminderGroupField;
  private bool reminderGroupFieldSpecified;
  private string uIDField;

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
  public System.DateTime ReminderTime
  {
    get => this.reminderTimeField;
    set => this.reminderTimeField = value;
  }

  /// <remarks />
  public System.DateTime StartDate
  {
    get => this.startDateField;
    set => this.startDateField = value;
  }

  /// <remarks />
  public System.DateTime EndDate
  {
    get => this.endDateField;
    set => this.endDateField = value;
  }

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  public ItemIdType RecurringMasterItemId
  {
    get => this.recurringMasterItemIdField;
    set => this.recurringMasterItemIdField = value;
  }

  /// <remarks />
  public ReminderGroupType ReminderGroup
  {
    get => this.reminderGroupField;
    set => this.reminderGroupField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReminderGroupSpecified
  {
    get => this.reminderGroupFieldSpecified;
    set => this.reminderGroupFieldSpecified = value;
  }

  /// <remarks />
  public string UID
  {
    get => this.uIDField;
    set => this.uIDField = value;
  }
}
