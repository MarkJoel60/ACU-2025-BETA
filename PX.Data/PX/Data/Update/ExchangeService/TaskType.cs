// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.TaskType
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
public class TaskType : ItemType
{
  private int actualWorkField;
  private bool actualWorkFieldSpecified;
  private System.DateTime assignedTimeField;
  private bool assignedTimeFieldSpecified;
  private string billingInformationField;
  private int changeCountField;
  private bool changeCountFieldSpecified;
  private string[] companiesField;
  private System.DateTime completeDateField;
  private bool completeDateFieldSpecified;
  private string[] contactsField;
  private TaskDelegateStateType delegationStateField;
  private bool delegationStateFieldSpecified;
  private string delegatorField;
  private System.DateTime dueDateField;
  private bool dueDateFieldSpecified;
  private int isAssignmentEditableField;
  private bool isAssignmentEditableFieldSpecified;
  private bool isCompleteField;
  private bool isCompleteFieldSpecified;
  private bool isRecurringField;
  private bool isRecurringFieldSpecified;
  private bool isTeamTaskField;
  private bool isTeamTaskFieldSpecified;
  private string mileageField;
  private string ownerField;
  private double percentCompleteField;
  private bool percentCompleteFieldSpecified;
  private TaskRecurrenceType recurrenceField;
  private System.DateTime startDateField;
  private bool startDateFieldSpecified;
  private TaskStatusType statusField;
  private bool statusFieldSpecified;
  private string statusDescriptionField;
  private int totalWorkField;
  private bool totalWorkFieldSpecified;

  /// <remarks />
  public int ActualWork
  {
    get => this.actualWorkField;
    set => this.actualWorkField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ActualWorkSpecified
  {
    get => this.actualWorkFieldSpecified;
    set => this.actualWorkFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime AssignedTime
  {
    get => this.assignedTimeField;
    set => this.assignedTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AssignedTimeSpecified
  {
    get => this.assignedTimeFieldSpecified;
    set => this.assignedTimeFieldSpecified = value;
  }

  /// <remarks />
  public string BillingInformation
  {
    get => this.billingInformationField;
    set => this.billingInformationField = value;
  }

  /// <remarks />
  public int ChangeCount
  {
    get => this.changeCountField;
    set => this.changeCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ChangeCountSpecified
  {
    get => this.changeCountFieldSpecified;
    set => this.changeCountFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Companies
  {
    get => this.companiesField;
    set => this.companiesField = value;
  }

  /// <remarks />
  public System.DateTime CompleteDate
  {
    get => this.completeDateField;
    set => this.completeDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CompleteDateSpecified
  {
    get => this.completeDateFieldSpecified;
    set => this.completeDateFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("String", IsNullable = false)]
  public string[] Contacts
  {
    get => this.contactsField;
    set => this.contactsField = value;
  }

  /// <remarks />
  public TaskDelegateStateType DelegationState
  {
    get => this.delegationStateField;
    set => this.delegationStateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DelegationStateSpecified
  {
    get => this.delegationStateFieldSpecified;
    set => this.delegationStateFieldSpecified = value;
  }

  /// <remarks />
  public string Delegator
  {
    get => this.delegatorField;
    set => this.delegatorField = value;
  }

  /// <remarks />
  public System.DateTime DueDate
  {
    get => this.dueDateField;
    set => this.dueDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DueDateSpecified
  {
    get => this.dueDateFieldSpecified;
    set => this.dueDateFieldSpecified = value;
  }

  /// <remarks />
  public int IsAssignmentEditable
  {
    get => this.isAssignmentEditableField;
    set => this.isAssignmentEditableField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsAssignmentEditableSpecified
  {
    get => this.isAssignmentEditableFieldSpecified;
    set => this.isAssignmentEditableFieldSpecified = value;
  }

  /// <remarks />
  public bool IsComplete
  {
    get => this.isCompleteField;
    set => this.isCompleteField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsCompleteSpecified
  {
    get => this.isCompleteFieldSpecified;
    set => this.isCompleteFieldSpecified = value;
  }

  /// <remarks />
  public bool IsRecurring
  {
    get => this.isRecurringField;
    set => this.isRecurringField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsRecurringSpecified
  {
    get => this.isRecurringFieldSpecified;
    set => this.isRecurringFieldSpecified = value;
  }

  /// <remarks />
  public bool IsTeamTask
  {
    get => this.isTeamTaskField;
    set => this.isTeamTaskField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsTeamTaskSpecified
  {
    get => this.isTeamTaskFieldSpecified;
    set => this.isTeamTaskFieldSpecified = value;
  }

  /// <remarks />
  public string Mileage
  {
    get => this.mileageField;
    set => this.mileageField = value;
  }

  /// <remarks />
  public string Owner
  {
    get => this.ownerField;
    set => this.ownerField = value;
  }

  /// <remarks />
  public double PercentComplete
  {
    get => this.percentCompleteField;
    set => this.percentCompleteField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PercentCompleteSpecified
  {
    get => this.percentCompleteFieldSpecified;
    set => this.percentCompleteFieldSpecified = value;
  }

  /// <remarks />
  public TaskRecurrenceType Recurrence
  {
    get => this.recurrenceField;
    set => this.recurrenceField = value;
  }

  /// <remarks />
  public System.DateTime StartDate
  {
    get => this.startDateField;
    set => this.startDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartDateSpecified
  {
    get => this.startDateFieldSpecified;
    set => this.startDateFieldSpecified = value;
  }

  /// <remarks />
  public TaskStatusType Status
  {
    get => this.statusField;
    set => this.statusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StatusSpecified
  {
    get => this.statusFieldSpecified;
    set => this.statusFieldSpecified = value;
  }

  /// <remarks />
  public string StatusDescription
  {
    get => this.statusDescriptionField;
    set => this.statusDescriptionField = value;
  }

  /// <remarks />
  public int TotalWork
  {
    get => this.totalWorkField;
    set => this.totalWorkField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TotalWorkSpecified
  {
    get => this.totalWorkFieldSpecified;
    set => this.totalWorkFieldSpecified = value;
  }
}
