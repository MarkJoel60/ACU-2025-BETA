// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MeetingRequestMessageType
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
public class MeetingRequestMessageType : MeetingMessageType
{
  private MeetingRequestTypeType meetingRequestTypeField;
  private bool meetingRequestTypeFieldSpecified;
  private LegacyFreeBusyType intendedFreeBusyStatusField;
  private bool intendedFreeBusyStatusFieldSpecified;
  private System.DateTime startField;
  private bool startFieldSpecified;
  private System.DateTime endField;
  private bool endFieldSpecified;
  private System.DateTime originalStartField;
  private bool originalStartFieldSpecified;
  private bool isAllDayEventField;
  private bool isAllDayEventFieldSpecified;
  private LegacyFreeBusyType legacyFreeBusyStatusField;
  private bool legacyFreeBusyStatusFieldSpecified;
  private string locationField;
  private string whenField;
  private bool isMeetingField;
  private bool isMeetingFieldSpecified;
  private bool isCancelledField;
  private bool isCancelledFieldSpecified;
  private bool isRecurringField;
  private bool isRecurringFieldSpecified;
  private bool meetingRequestWasSentField;
  private bool meetingRequestWasSentFieldSpecified;
  private CalendarItemTypeType calendarItemTypeField;
  private bool calendarItemTypeFieldSpecified;
  private ResponseTypeType myResponseTypeField;
  private bool myResponseTypeFieldSpecified;
  private SingleRecipientType organizerField;
  private AttendeeType[] requiredAttendeesField;
  private AttendeeType[] optionalAttendeesField;
  private AttendeeType[] resourcesField;
  private int conflictingMeetingCountField;
  private bool conflictingMeetingCountFieldSpecified;
  private int adjacentMeetingCountField;
  private bool adjacentMeetingCountFieldSpecified;
  private NonEmptyArrayOfAllItemsType conflictingMeetingsField;
  private NonEmptyArrayOfAllItemsType adjacentMeetingsField;
  private string durationField;
  private string timeZoneField;
  private System.DateTime appointmentReplyTimeField;
  private bool appointmentReplyTimeFieldSpecified;
  private int appointmentSequenceNumberField;
  private bool appointmentSequenceNumberFieldSpecified;
  private int appointmentStateField;
  private bool appointmentStateFieldSpecified;
  private RecurrenceType recurrenceField;
  private OccurrenceInfoType firstOccurrenceField;
  private OccurrenceInfoType lastOccurrenceField;
  private OccurrenceInfoType[] modifiedOccurrencesField;
  private DeletedOccurrenceInfoType[] deletedOccurrencesField;
  private TimeZoneType meetingTimeZoneField;
  private TimeZoneDefinitionType startTimeZoneField;
  private TimeZoneDefinitionType endTimeZoneField;
  private int conferenceTypeField;
  private bool conferenceTypeFieldSpecified;
  private bool allowNewTimeProposalField;
  private bool allowNewTimeProposalFieldSpecified;
  private bool isOnlineMeetingField;
  private bool isOnlineMeetingFieldSpecified;
  private string meetingWorkspaceUrlField;
  private string netShowUrlField;
  private EnhancedLocationType enhancedLocationField;
  private ChangeHighlightsType changeHighlightsField;
  private System.DateTime startWallClockField;
  private bool startWallClockFieldSpecified;
  private System.DateTime endWallClockField;
  private bool endWallClockFieldSpecified;
  private string startTimeZoneIdField;
  private string endTimeZoneIdField;

  /// <remarks />
  public MeetingRequestTypeType MeetingRequestType
  {
    get => this.meetingRequestTypeField;
    set => this.meetingRequestTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MeetingRequestTypeSpecified
  {
    get => this.meetingRequestTypeFieldSpecified;
    set => this.meetingRequestTypeFieldSpecified = value;
  }

  /// <remarks />
  public LegacyFreeBusyType IntendedFreeBusyStatus
  {
    get => this.intendedFreeBusyStatusField;
    set => this.intendedFreeBusyStatusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IntendedFreeBusyStatusSpecified
  {
    get => this.intendedFreeBusyStatusFieldSpecified;
    set => this.intendedFreeBusyStatusFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime Start
  {
    get => this.startField;
    set => this.startField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartSpecified
  {
    get => this.startFieldSpecified;
    set => this.startFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime End
  {
    get => this.endField;
    set => this.endField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndSpecified
  {
    get => this.endFieldSpecified;
    set => this.endFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime OriginalStart
  {
    get => this.originalStartField;
    set => this.originalStartField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool OriginalStartSpecified
  {
    get => this.originalStartFieldSpecified;
    set => this.originalStartFieldSpecified = value;
  }

  /// <remarks />
  public bool IsAllDayEvent
  {
    get => this.isAllDayEventField;
    set => this.isAllDayEventField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsAllDayEventSpecified
  {
    get => this.isAllDayEventFieldSpecified;
    set => this.isAllDayEventFieldSpecified = value;
  }

  /// <remarks />
  public LegacyFreeBusyType LegacyFreeBusyStatus
  {
    get => this.legacyFreeBusyStatusField;
    set => this.legacyFreeBusyStatusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool LegacyFreeBusyStatusSpecified
  {
    get => this.legacyFreeBusyStatusFieldSpecified;
    set => this.legacyFreeBusyStatusFieldSpecified = value;
  }

  /// <remarks />
  public string Location
  {
    get => this.locationField;
    set => this.locationField = value;
  }

  /// <remarks />
  public string When
  {
    get => this.whenField;
    set => this.whenField = value;
  }

  /// <remarks />
  public bool IsMeeting
  {
    get => this.isMeetingField;
    set => this.isMeetingField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsMeetingSpecified
  {
    get => this.isMeetingFieldSpecified;
    set => this.isMeetingFieldSpecified = value;
  }

  /// <remarks />
  public bool IsCancelled
  {
    get => this.isCancelledField;
    set => this.isCancelledField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsCancelledSpecified
  {
    get => this.isCancelledFieldSpecified;
    set => this.isCancelledFieldSpecified = value;
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
  public bool MeetingRequestWasSent
  {
    get => this.meetingRequestWasSentField;
    set => this.meetingRequestWasSentField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MeetingRequestWasSentSpecified
  {
    get => this.meetingRequestWasSentFieldSpecified;
    set => this.meetingRequestWasSentFieldSpecified = value;
  }

  /// <remarks />
  public CalendarItemTypeType CalendarItemType
  {
    get => this.calendarItemTypeField;
    set => this.calendarItemTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CalendarItemTypeSpecified
  {
    get => this.calendarItemTypeFieldSpecified;
    set => this.calendarItemTypeFieldSpecified = value;
  }

  /// <remarks />
  public ResponseTypeType MyResponseType
  {
    get => this.myResponseTypeField;
    set => this.myResponseTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MyResponseTypeSpecified
  {
    get => this.myResponseTypeFieldSpecified;
    set => this.myResponseTypeFieldSpecified = value;
  }

  /// <remarks />
  public SingleRecipientType Organizer
  {
    get => this.organizerField;
    set => this.organizerField = value;
  }

  /// <remarks />
  [XmlArrayItem("Attendee", IsNullable = false)]
  public AttendeeType[] RequiredAttendees
  {
    get => this.requiredAttendeesField;
    set => this.requiredAttendeesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Attendee", IsNullable = false)]
  public AttendeeType[] OptionalAttendees
  {
    get => this.optionalAttendeesField;
    set => this.optionalAttendeesField = value;
  }

  /// <remarks />
  [XmlArrayItem("Attendee", IsNullable = false)]
  public AttendeeType[] Resources
  {
    get => this.resourcesField;
    set => this.resourcesField = value;
  }

  /// <remarks />
  public int ConflictingMeetingCount
  {
    get => this.conflictingMeetingCountField;
    set => this.conflictingMeetingCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ConflictingMeetingCountSpecified
  {
    get => this.conflictingMeetingCountFieldSpecified;
    set => this.conflictingMeetingCountFieldSpecified = value;
  }

  /// <remarks />
  public int AdjacentMeetingCount
  {
    get => this.adjacentMeetingCountField;
    set => this.adjacentMeetingCountField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AdjacentMeetingCountSpecified
  {
    get => this.adjacentMeetingCountFieldSpecified;
    set => this.adjacentMeetingCountFieldSpecified = value;
  }

  /// <remarks />
  public NonEmptyArrayOfAllItemsType ConflictingMeetings
  {
    get => this.conflictingMeetingsField;
    set => this.conflictingMeetingsField = value;
  }

  /// <remarks />
  public NonEmptyArrayOfAllItemsType AdjacentMeetings
  {
    get => this.adjacentMeetingsField;
    set => this.adjacentMeetingsField = value;
  }

  /// <remarks />
  public string Duration
  {
    get => this.durationField;
    set => this.durationField = value;
  }

  /// <remarks />
  public string TimeZone
  {
    get => this.timeZoneField;
    set => this.timeZoneField = value;
  }

  /// <remarks />
  public System.DateTime AppointmentReplyTime
  {
    get => this.appointmentReplyTimeField;
    set => this.appointmentReplyTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AppointmentReplyTimeSpecified
  {
    get => this.appointmentReplyTimeFieldSpecified;
    set => this.appointmentReplyTimeFieldSpecified = value;
  }

  /// <remarks />
  public int AppointmentSequenceNumber
  {
    get => this.appointmentSequenceNumberField;
    set => this.appointmentSequenceNumberField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AppointmentSequenceNumberSpecified
  {
    get => this.appointmentSequenceNumberFieldSpecified;
    set => this.appointmentSequenceNumberFieldSpecified = value;
  }

  /// <remarks />
  public int AppointmentState
  {
    get => this.appointmentStateField;
    set => this.appointmentStateField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AppointmentStateSpecified
  {
    get => this.appointmentStateFieldSpecified;
    set => this.appointmentStateFieldSpecified = value;
  }

  /// <remarks />
  public RecurrenceType Recurrence
  {
    get => this.recurrenceField;
    set => this.recurrenceField = value;
  }

  /// <remarks />
  public OccurrenceInfoType FirstOccurrence
  {
    get => this.firstOccurrenceField;
    set => this.firstOccurrenceField = value;
  }

  /// <remarks />
  public OccurrenceInfoType LastOccurrence
  {
    get => this.lastOccurrenceField;
    set => this.lastOccurrenceField = value;
  }

  /// <remarks />
  [XmlArrayItem("Occurrence", IsNullable = false)]
  public OccurrenceInfoType[] ModifiedOccurrences
  {
    get => this.modifiedOccurrencesField;
    set => this.modifiedOccurrencesField = value;
  }

  /// <remarks />
  [XmlArrayItem("DeletedOccurrence", IsNullable = false)]
  public DeletedOccurrenceInfoType[] DeletedOccurrences
  {
    get => this.deletedOccurrencesField;
    set => this.deletedOccurrencesField = value;
  }

  /// <remarks />
  public TimeZoneType MeetingTimeZone
  {
    get => this.meetingTimeZoneField;
    set => this.meetingTimeZoneField = value;
  }

  /// <remarks />
  public TimeZoneDefinitionType StartTimeZone
  {
    get => this.startTimeZoneField;
    set => this.startTimeZoneField = value;
  }

  /// <remarks />
  public TimeZoneDefinitionType EndTimeZone
  {
    get => this.endTimeZoneField;
    set => this.endTimeZoneField = value;
  }

  /// <remarks />
  public int ConferenceType
  {
    get => this.conferenceTypeField;
    set => this.conferenceTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ConferenceTypeSpecified
  {
    get => this.conferenceTypeFieldSpecified;
    set => this.conferenceTypeFieldSpecified = value;
  }

  /// <remarks />
  public bool AllowNewTimeProposal
  {
    get => this.allowNewTimeProposalField;
    set => this.allowNewTimeProposalField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AllowNewTimeProposalSpecified
  {
    get => this.allowNewTimeProposalFieldSpecified;
    set => this.allowNewTimeProposalFieldSpecified = value;
  }

  /// <remarks />
  public bool IsOnlineMeeting
  {
    get => this.isOnlineMeetingField;
    set => this.isOnlineMeetingField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsOnlineMeetingSpecified
  {
    get => this.isOnlineMeetingFieldSpecified;
    set => this.isOnlineMeetingFieldSpecified = value;
  }

  /// <remarks />
  public string MeetingWorkspaceUrl
  {
    get => this.meetingWorkspaceUrlField;
    set => this.meetingWorkspaceUrlField = value;
  }

  /// <remarks />
  public string NetShowUrl
  {
    get => this.netShowUrlField;
    set => this.netShowUrlField = value;
  }

  /// <remarks />
  public EnhancedLocationType EnhancedLocation
  {
    get => this.enhancedLocationField;
    set => this.enhancedLocationField = value;
  }

  /// <remarks />
  public ChangeHighlightsType ChangeHighlights
  {
    get => this.changeHighlightsField;
    set => this.changeHighlightsField = value;
  }

  /// <remarks />
  public System.DateTime StartWallClock
  {
    get => this.startWallClockField;
    set => this.startWallClockField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartWallClockSpecified
  {
    get => this.startWallClockFieldSpecified;
    set => this.startWallClockFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime EndWallClock
  {
    get => this.endWallClockField;
    set => this.endWallClockField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndWallClockSpecified
  {
    get => this.endWallClockFieldSpecified;
    set => this.endWallClockFieldSpecified = value;
  }

  /// <remarks />
  public string StartTimeZoneId
  {
    get => this.startTimeZoneIdField;
    set => this.startTimeZoneIdField = value;
  }

  /// <remarks />
  public string EndTimeZoneId
  {
    get => this.endTimeZoneIdField;
    set => this.endTimeZoneIdField = value;
  }
}
