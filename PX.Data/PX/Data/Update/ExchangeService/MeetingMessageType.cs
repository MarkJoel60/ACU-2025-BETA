// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MeetingMessageType
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
[XmlInclude(typeof (MeetingCancellationMessageType))]
[XmlInclude(typeof (MeetingResponseMessageType))]
[XmlInclude(typeof (MeetingRequestMessageType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class MeetingMessageType : MessageType
{
  private ItemIdType associatedCalendarItemIdField;
  private bool isDelegatedField;
  private bool isDelegatedFieldSpecified;
  private bool isOutOfDateField;
  private bool isOutOfDateFieldSpecified;
  private bool hasBeenProcessedField;
  private bool hasBeenProcessedFieldSpecified;
  private ResponseTypeType responseTypeField;
  private bool responseTypeFieldSpecified;
  private string uIDField;
  private System.DateTime recurrenceIdField;
  private bool recurrenceIdFieldSpecified;
  private System.DateTime dateTimeStampField;
  private bool dateTimeStampFieldSpecified;
  private bool isOrganizerField;
  private bool isOrganizerFieldSpecified;

  /// <remarks />
  public ItemIdType AssociatedCalendarItemId
  {
    get => this.associatedCalendarItemIdField;
    set => this.associatedCalendarItemIdField = value;
  }

  /// <remarks />
  public bool IsDelegated
  {
    get => this.isDelegatedField;
    set => this.isDelegatedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsDelegatedSpecified
  {
    get => this.isDelegatedFieldSpecified;
    set => this.isDelegatedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsOutOfDate
  {
    get => this.isOutOfDateField;
    set => this.isOutOfDateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsOutOfDateSpecified
  {
    get => this.isOutOfDateFieldSpecified;
    set => this.isOutOfDateFieldSpecified = value;
  }

  /// <remarks />
  public bool HasBeenProcessed
  {
    get => this.hasBeenProcessedField;
    set => this.hasBeenProcessedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool HasBeenProcessedSpecified
  {
    get => this.hasBeenProcessedFieldSpecified;
    set => this.hasBeenProcessedFieldSpecified = value;
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
  public string UID
  {
    get => this.uIDField;
    set => this.uIDField = value;
  }

  /// <remarks />
  public System.DateTime RecurrenceId
  {
    get => this.recurrenceIdField;
    set => this.recurrenceIdField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool RecurrenceIdSpecified
  {
    get => this.recurrenceIdFieldSpecified;
    set => this.recurrenceIdFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime DateTimeStamp
  {
    get => this.dateTimeStampField;
    set => this.dateTimeStampField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DateTimeStampSpecified
  {
    get => this.dateTimeStampFieldSpecified;
    set => this.dateTimeStampFieldSpecified = value;
  }

  /// <remarks />
  public bool IsOrganizer
  {
    get => this.isOrganizerField;
    set => this.isOrganizerField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsOrganizerSpecified
  {
    get => this.isOrganizerFieldSpecified;
    set => this.isOrganizerFieldSpecified = value;
  }
}
