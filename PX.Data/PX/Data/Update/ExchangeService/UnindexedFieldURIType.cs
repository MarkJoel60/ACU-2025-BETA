// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UnindexedFieldURIType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum UnindexedFieldURIType
{
  /// <remarks />
  [XmlEnum("folder:FolderId")] folderFolderId,
  /// <remarks />
  [XmlEnum("folder:ParentFolderId")] folderParentFolderId,
  /// <remarks />
  [XmlEnum("folder:DisplayName")] folderDisplayName,
  /// <remarks />
  [XmlEnum("folder:UnreadCount")] folderUnreadCount,
  /// <remarks />
  [XmlEnum("folder:TotalCount")] folderTotalCount,
  /// <remarks />
  [XmlEnum("folder:ChildFolderCount")] folderChildFolderCount,
  /// <remarks />
  [XmlEnum("folder:FolderClass")] folderFolderClass,
  /// <remarks />
  [XmlEnum("folder:SearchParameters")] folderSearchParameters,
  /// <remarks />
  [XmlEnum("folder:ManagedFolderInformation")] folderManagedFolderInformation,
  /// <remarks />
  [XmlEnum("folder:PermissionSet")] folderPermissionSet,
  /// <remarks />
  [XmlEnum("folder:EffectiveRights")] folderEffectiveRights,
  /// <remarks />
  [XmlEnum("folder:SharingEffectiveRights")] folderSharingEffectiveRights,
  /// <remarks />
  [XmlEnum("folder:DistinguishedFolderId")] folderDistinguishedFolderId,
  /// <remarks />
  [XmlEnum("folder:PolicyTag")] folderPolicyTag,
  /// <remarks />
  [XmlEnum("folder:ArchiveTag")] folderArchiveTag,
  /// <remarks />
  [XmlEnum("item:ItemId")] itemItemId,
  /// <remarks />
  [XmlEnum("item:ParentFolderId")] itemParentFolderId,
  /// <remarks />
  [XmlEnum("item:ItemClass")] itemItemClass,
  /// <remarks />
  [XmlEnum("item:MimeContent")] itemMimeContent,
  /// <remarks />
  [XmlEnum("item:Attachments")] itemAttachments,
  /// <remarks />
  [XmlEnum("item:Subject")] itemSubject,
  /// <remarks />
  [XmlEnum("item:DateTimeReceived")] itemDateTimeReceived,
  /// <remarks />
  [XmlEnum("item:Size")] itemSize,
  /// <remarks />
  [XmlEnum("item:Categories")] itemCategories,
  /// <remarks />
  [XmlEnum("item:HasAttachments")] itemHasAttachments,
  /// <remarks />
  [XmlEnum("item:Importance")] itemImportance,
  /// <remarks />
  [XmlEnum("item:InReplyTo")] itemInReplyTo,
  /// <remarks />
  [XmlEnum("item:InternetMessageHeaders")] itemInternetMessageHeaders,
  /// <remarks />
  [XmlEnum("item:IsAssociated")] itemIsAssociated,
  /// <remarks />
  [XmlEnum("item:IsDraft")] itemIsDraft,
  /// <remarks />
  [XmlEnum("item:IsFromMe")] itemIsFromMe,
  /// <remarks />
  [XmlEnum("item:IsResend")] itemIsResend,
  /// <remarks />
  [XmlEnum("item:IsSubmitted")] itemIsSubmitted,
  /// <remarks />
  [XmlEnum("item:IsUnmodified")] itemIsUnmodified,
  /// <remarks />
  [XmlEnum("item:DateTimeSent")] itemDateTimeSent,
  /// <remarks />
  [XmlEnum("item:DateTimeCreated")] itemDateTimeCreated,
  /// <remarks />
  [XmlEnum("item:Body")] itemBody,
  /// <remarks />
  [XmlEnum("item:ResponseObjects")] itemResponseObjects,
  /// <remarks />
  [XmlEnum("item:Sensitivity")] itemSensitivity,
  /// <remarks />
  [XmlEnum("item:ReminderDueBy")] itemReminderDueBy,
  /// <remarks />
  [XmlEnum("item:ReminderIsSet")] itemReminderIsSet,
  /// <remarks />
  [XmlEnum("item:ReminderNextTime")] itemReminderNextTime,
  /// <remarks />
  [XmlEnum("item:ReminderMinutesBeforeStart")] itemReminderMinutesBeforeStart,
  /// <remarks />
  [XmlEnum("item:DisplayTo")] itemDisplayTo,
  /// <remarks />
  [XmlEnum("item:DisplayCc")] itemDisplayCc,
  /// <remarks />
  [XmlEnum("item:Culture")] itemCulture,
  /// <remarks />
  [XmlEnum("item:EffectiveRights")] itemEffectiveRights,
  /// <remarks />
  [XmlEnum("item:LastModifiedName")] itemLastModifiedName,
  /// <remarks />
  [XmlEnum("item:LastModifiedTime")] itemLastModifiedTime,
  /// <remarks />
  [XmlEnum("item:ConversationId")] itemConversationId,
  /// <remarks />
  [XmlEnum("item:UniqueBody")] itemUniqueBody,
  /// <remarks />
  [XmlEnum("item:Flag")] itemFlag,
  /// <remarks />
  [XmlEnum("item:StoreEntryId")] itemStoreEntryId,
  /// <remarks />
  [XmlEnum("item:InstanceKey")] itemInstanceKey,
  /// <remarks />
  [XmlEnum("item:NormalizedBody")] itemNormalizedBody,
  /// <remarks />
  [XmlEnum("item:EntityExtractionResult")] itemEntityExtractionResult,
  /// <remarks />
  [XmlEnum("item:PolicyTag")] itemPolicyTag,
  /// <remarks />
  [XmlEnum("item:ArchiveTag")] itemArchiveTag,
  /// <remarks />
  [XmlEnum("item:RetentionDate")] itemRetentionDate,
  /// <remarks />
  [XmlEnum("item:Preview")] itemPreview,
  /// <remarks />
  [XmlEnum("item:NextPredictedAction")] itemNextPredictedAction,
  /// <remarks />
  [XmlEnum("item:GroupingAction")] itemGroupingAction,
  /// <remarks />
  [XmlEnum("item:PredictedActionReasons")] itemPredictedActionReasons,
  /// <remarks />
  [XmlEnum("item:IsClutter")] itemIsClutter,
  /// <remarks />
  [XmlEnum("item:RightsManagementLicenseData")] itemRightsManagementLicenseData,
  /// <remarks />
  [XmlEnum("item:BlockStatus")] itemBlockStatus,
  /// <remarks />
  [XmlEnum("item:HasBlockedImages")] itemHasBlockedImages,
  /// <remarks />
  [XmlEnum("item:WebClientReadFormQueryString")] itemWebClientReadFormQueryString,
  /// <remarks />
  [XmlEnum("item:WebClientEditFormQueryString")] itemWebClientEditFormQueryString,
  /// <remarks />
  [XmlEnum("item:TextBody")] itemTextBody,
  /// <remarks />
  [XmlEnum("item:IconIndex")] itemIconIndex,
  /// <remarks />
  [XmlEnum("message:ConversationIndex")] messageConversationIndex,
  /// <remarks />
  [XmlEnum("message:ConversationTopic")] messageConversationTopic,
  /// <remarks />
  [XmlEnum("message:InternetMessageId")] messageInternetMessageId,
  /// <remarks />
  [XmlEnum("message:IsRead")] messageIsRead,
  /// <remarks />
  [XmlEnum("message:IsResponseRequested")] messageIsResponseRequested,
  /// <remarks />
  [XmlEnum("message:IsReadReceiptRequested")] messageIsReadReceiptRequested,
  /// <remarks />
  [XmlEnum("message:IsDeliveryReceiptRequested")] messageIsDeliveryReceiptRequested,
  /// <remarks />
  [XmlEnum("message:ReceivedBy")] messageReceivedBy,
  /// <remarks />
  [XmlEnum("message:ReceivedRepresenting")] messageReceivedRepresenting,
  /// <remarks />
  [XmlEnum("message:References")] messageReferences,
  /// <remarks />
  [XmlEnum("message:ReplyTo")] messageReplyTo,
  /// <remarks />
  [XmlEnum("message:From")] messageFrom,
  /// <remarks />
  [XmlEnum("message:Sender")] messageSender,
  /// <remarks />
  [XmlEnum("message:ToRecipients")] messageToRecipients,
  /// <remarks />
  [XmlEnum("message:CcRecipients")] messageCcRecipients,
  /// <remarks />
  [XmlEnum("message:BccRecipients")] messageBccRecipients,
  /// <remarks />
  [XmlEnum("message:ApprovalRequestData")] messageApprovalRequestData,
  /// <remarks />
  [XmlEnum("message:VotingInformation")] messageVotingInformation,
  /// <remarks />
  [XmlEnum("message:ReminderMessageData")] messageReminderMessageData,
  /// <remarks />
  [XmlEnum("meeting:AssociatedCalendarItemId")] meetingAssociatedCalendarItemId,
  /// <remarks />
  [XmlEnum("meeting:IsDelegated")] meetingIsDelegated,
  /// <remarks />
  [XmlEnum("meeting:IsOutOfDate")] meetingIsOutOfDate,
  /// <remarks />
  [XmlEnum("meeting:HasBeenProcessed")] meetingHasBeenProcessed,
  /// <remarks />
  [XmlEnum("meeting:ResponseType")] meetingResponseType,
  /// <remarks />
  [XmlEnum("meeting:ProposedStart")] meetingProposedStart,
  /// <remarks />
  [XmlEnum("meeting:ProposedEnd")] meetingProposedEnd,
  /// <remarks />
  [XmlEnum("meetingRequest:MeetingRequestType")] meetingRequestMeetingRequestType,
  /// <remarks />
  [XmlEnum("meetingRequest:IntendedFreeBusyStatus")] meetingRequestIntendedFreeBusyStatus,
  /// <remarks />
  [XmlEnum("meetingRequest:ChangeHighlights")] meetingRequestChangeHighlights,
  /// <remarks />
  [XmlEnum("calendar:Start")] calendarStart,
  /// <remarks />
  [XmlEnum("calendar:End")] calendarEnd,
  /// <remarks />
  [XmlEnum("calendar:OriginalStart")] calendarOriginalStart,
  /// <remarks />
  [XmlEnum("calendar:StartWallClock")] calendarStartWallClock,
  /// <remarks />
  [XmlEnum("calendar:EndWallClock")] calendarEndWallClock,
  /// <remarks />
  [XmlEnum("calendar:StartTimeZoneId")] calendarStartTimeZoneId,
  /// <remarks />
  [XmlEnum("calendar:EndTimeZoneId")] calendarEndTimeZoneId,
  /// <remarks />
  [XmlEnum("calendar:IsAllDayEvent")] calendarIsAllDayEvent,
  /// <remarks />
  [XmlEnum("calendar:LegacyFreeBusyStatus")] calendarLegacyFreeBusyStatus,
  /// <remarks />
  [XmlEnum("calendar:Location")] calendarLocation,
  /// <remarks />
  [XmlEnum("calendar:EnhancedLocation")] calendarEnhancedLocation,
  /// <remarks />
  [XmlEnum("calendar:When")] calendarWhen,
  /// <remarks />
  [XmlEnum("calendar:IsMeeting")] calendarIsMeeting,
  /// <remarks />
  [XmlEnum("calendar:IsCancelled")] calendarIsCancelled,
  /// <remarks />
  [XmlEnum("calendar:IsRecurring")] calendarIsRecurring,
  /// <remarks />
  [XmlEnum("calendar:MeetingRequestWasSent")] calendarMeetingRequestWasSent,
  /// <remarks />
  [XmlEnum("calendar:IsResponseRequested")] calendarIsResponseRequested,
  /// <remarks />
  [XmlEnum("calendar:CalendarItemType")] calendarCalendarItemType,
  /// <remarks />
  [XmlEnum("calendar:MyResponseType")] calendarMyResponseType,
  /// <remarks />
  [XmlEnum("calendar:Organizer")] calendarOrganizer,
  /// <remarks />
  [XmlEnum("calendar:RequiredAttendees")] calendarRequiredAttendees,
  /// <remarks />
  [XmlEnum("calendar:OptionalAttendees")] calendarOptionalAttendees,
  /// <remarks />
  [XmlEnum("calendar:Resources")] calendarResources,
  /// <remarks />
  [XmlEnum("calendar:ConflictingMeetingCount")] calendarConflictingMeetingCount,
  /// <remarks />
  [XmlEnum("calendar:AdjacentMeetingCount")] calendarAdjacentMeetingCount,
  /// <remarks />
  [XmlEnum("calendar:ConflictingMeetings")] calendarConflictingMeetings,
  /// <remarks />
  [XmlEnum("calendar:AdjacentMeetings")] calendarAdjacentMeetings,
  /// <remarks />
  [XmlEnum("calendar:Duration")] calendarDuration,
  /// <remarks />
  [XmlEnum("calendar:TimeZone")] calendarTimeZone,
  /// <remarks />
  [XmlEnum("calendar:AppointmentReplyTime")] calendarAppointmentReplyTime,
  /// <remarks />
  [XmlEnum("calendar:AppointmentSequenceNumber")] calendarAppointmentSequenceNumber,
  /// <remarks />
  [XmlEnum("calendar:AppointmentState")] calendarAppointmentState,
  /// <remarks />
  [XmlEnum("calendar:Recurrence")] calendarRecurrence,
  /// <remarks />
  [XmlEnum("calendar:FirstOccurrence")] calendarFirstOccurrence,
  /// <remarks />
  [XmlEnum("calendar:LastOccurrence")] calendarLastOccurrence,
  /// <remarks />
  [XmlEnum("calendar:ModifiedOccurrences")] calendarModifiedOccurrences,
  /// <remarks />
  [XmlEnum("calendar:DeletedOccurrences")] calendarDeletedOccurrences,
  /// <remarks />
  [XmlEnum("calendar:MeetingTimeZone")] calendarMeetingTimeZone,
  /// <remarks />
  [XmlEnum("calendar:ConferenceType")] calendarConferenceType,
  /// <remarks />
  [XmlEnum("calendar:AllowNewTimeProposal")] calendarAllowNewTimeProposal,
  /// <remarks />
  [XmlEnum("calendar:IsOnlineMeeting")] calendarIsOnlineMeeting,
  /// <remarks />
  [XmlEnum("calendar:MeetingWorkspaceUrl")] calendarMeetingWorkspaceUrl,
  /// <remarks />
  [XmlEnum("calendar:NetShowUrl")] calendarNetShowUrl,
  /// <remarks />
  [XmlEnum("calendar:UID")] calendarUID,
  /// <remarks />
  [XmlEnum("calendar:RecurrenceId")] calendarRecurrenceId,
  /// <remarks />
  [XmlEnum("calendar:DateTimeStamp")] calendarDateTimeStamp,
  /// <remarks />
  [XmlEnum("calendar:StartTimeZone")] calendarStartTimeZone,
  /// <remarks />
  [XmlEnum("calendar:EndTimeZone")] calendarEndTimeZone,
  /// <remarks />
  [XmlEnum("calendar:JoinOnlineMeetingUrl")] calendarJoinOnlineMeetingUrl,
  /// <remarks />
  [XmlEnum("calendar:OnlineMeetingSettings")] calendarOnlineMeetingSettings,
  /// <remarks />
  [XmlEnum("calendar:IsOrganizer")] calendarIsOrganizer,
  /// <remarks />
  [XmlEnum("task:ActualWork")] taskActualWork,
  /// <remarks />
  [XmlEnum("task:AssignedTime")] taskAssignedTime,
  /// <remarks />
  [XmlEnum("task:BillingInformation")] taskBillingInformation,
  /// <remarks />
  [XmlEnum("task:ChangeCount")] taskChangeCount,
  /// <remarks />
  [XmlEnum("task:Companies")] taskCompanies,
  /// <remarks />
  [XmlEnum("task:CompleteDate")] taskCompleteDate,
  /// <remarks />
  [XmlEnum("task:Contacts")] taskContacts,
  /// <remarks />
  [XmlEnum("task:DelegationState")] taskDelegationState,
  /// <remarks />
  [XmlEnum("task:Delegator")] taskDelegator,
  /// <remarks />
  [XmlEnum("task:DueDate")] taskDueDate,
  /// <remarks />
  [XmlEnum("task:IsAssignmentEditable")] taskIsAssignmentEditable,
  /// <remarks />
  [XmlEnum("task:IsComplete")] taskIsComplete,
  /// <remarks />
  [XmlEnum("task:IsRecurring")] taskIsRecurring,
  /// <remarks />
  [XmlEnum("task:IsTeamTask")] taskIsTeamTask,
  /// <remarks />
  [XmlEnum("task:Mileage")] taskMileage,
  /// <remarks />
  [XmlEnum("task:Owner")] taskOwner,
  /// <remarks />
  [XmlEnum("task:PercentComplete")] taskPercentComplete,
  /// <remarks />
  [XmlEnum("task:Recurrence")] taskRecurrence,
  /// <remarks />
  [XmlEnum("task:StartDate")] taskStartDate,
  /// <remarks />
  [XmlEnum("task:Status")] taskStatus,
  /// <remarks />
  [XmlEnum("task:StatusDescription")] taskStatusDescription,
  /// <remarks />
  [XmlEnum("task:TotalWork")] taskTotalWork,
  /// <remarks />
  [XmlEnum("contacts:Alias")] contactsAlias,
  /// <remarks />
  [XmlEnum("contacts:AssistantName")] contactsAssistantName,
  /// <remarks />
  [XmlEnum("contacts:Birthday")] contactsBirthday,
  /// <remarks />
  [XmlEnum("contacts:BusinessHomePage")] contactsBusinessHomePage,
  /// <remarks />
  [XmlEnum("contacts:Children")] contactsChildren,
  /// <remarks />
  [XmlEnum("contacts:Companies")] contactsCompanies,
  /// <remarks />
  [XmlEnum("contacts:CompanyName")] contactsCompanyName,
  /// <remarks />
  [XmlEnum("contacts:CompleteName")] contactsCompleteName,
  /// <remarks />
  [XmlEnum("contacts:ContactSource")] contactsContactSource,
  /// <remarks />
  [XmlEnum("contacts:Culture")] contactsCulture,
  /// <remarks />
  [XmlEnum("contacts:Department")] contactsDepartment,
  /// <remarks />
  [XmlEnum("contacts:DisplayName")] contactsDisplayName,
  /// <remarks />
  [XmlEnum("contacts:DirectoryId")] contactsDirectoryId,
  /// <remarks />
  [XmlEnum("contacts:DirectReports")] contactsDirectReports,
  /// <remarks />
  [XmlEnum("contacts:EmailAddresses")] contactsEmailAddresses,
  /// <remarks />
  [XmlEnum("contacts:FileAs")] contactsFileAs,
  /// <remarks />
  [XmlEnum("contacts:FileAsMapping")] contactsFileAsMapping,
  /// <remarks />
  [XmlEnum("contacts:Generation")] contactsGeneration,
  /// <remarks />
  [XmlEnum("contacts:GivenName")] contactsGivenName,
  /// <remarks />
  [XmlEnum("contacts:ImAddresses")] contactsImAddresses,
  /// <remarks />
  [XmlEnum("contacts:Initials")] contactsInitials,
  /// <remarks />
  [XmlEnum("contacts:JobTitle")] contactsJobTitle,
  /// <remarks />
  [XmlEnum("contacts:Manager")] contactsManager,
  /// <remarks />
  [XmlEnum("contacts:ManagerMailbox")] contactsManagerMailbox,
  /// <remarks />
  [XmlEnum("contacts:MiddleName")] contactsMiddleName,
  /// <remarks />
  [XmlEnum("contacts:Mileage")] contactsMileage,
  /// <remarks />
  [XmlEnum("contacts:MSExchangeCertificate")] contactsMSExchangeCertificate,
  /// <remarks />
  [XmlEnum("contacts:Nickname")] contactsNickname,
  /// <remarks />
  [XmlEnum("contacts:Notes")] contactsNotes,
  /// <remarks />
  [XmlEnum("contacts:OfficeLocation")] contactsOfficeLocation,
  /// <remarks />
  [XmlEnum("contacts:PhoneNumbers")] contactsPhoneNumbers,
  /// <remarks />
  [XmlEnum("contacts:PhoneticFullName")] contactsPhoneticFullName,
  /// <remarks />
  [XmlEnum("contacts:PhoneticFirstName")] contactsPhoneticFirstName,
  /// <remarks />
  [XmlEnum("contacts:PhoneticLastName")] contactsPhoneticLastName,
  /// <remarks />
  [XmlEnum("contacts:Photo")] contactsPhoto,
  /// <remarks />
  [XmlEnum("contacts:PhysicalAddresses")] contactsPhysicalAddresses,
  /// <remarks />
  [XmlEnum("contacts:PostalAddressIndex")] contactsPostalAddressIndex,
  /// <remarks />
  [XmlEnum("contacts:Profession")] contactsProfession,
  /// <remarks />
  [XmlEnum("contacts:SpouseName")] contactsSpouseName,
  /// <remarks />
  [XmlEnum("contacts:Surname")] contactsSurname,
  /// <remarks />
  [XmlEnum("contacts:WeddingAnniversary")] contactsWeddingAnniversary,
  /// <remarks />
  [XmlEnum("contacts:UserSMIMECertificate")] contactsUserSMIMECertificate,
  /// <remarks />
  [XmlEnum("contacts:HasPicture")] contactsHasPicture,
  /// <remarks />
  [XmlEnum("distributionlist:Members")] distributionlistMembers,
  /// <remarks />
  [XmlEnum("postitem:PostedTime")] postitemPostedTime,
  /// <remarks />
  [XmlEnum("conversation:ConversationId")] conversationConversationId,
  /// <remarks />
  [XmlEnum("conversation:ConversationTopic")] conversationConversationTopic,
  /// <remarks />
  [XmlEnum("conversation:UniqueRecipients")] conversationUniqueRecipients,
  /// <remarks />
  [XmlEnum("conversation:GlobalUniqueRecipients")] conversationGlobalUniqueRecipients,
  /// <remarks />
  [XmlEnum("conversation:UniqueUnreadSenders")] conversationUniqueUnreadSenders,
  /// <remarks />
  [XmlEnum("conversation:GlobalUniqueUnreadSenders")] conversationGlobalUniqueUnreadSenders,
  /// <remarks />
  [XmlEnum("conversation:UniqueSenders")] conversationUniqueSenders,
  /// <remarks />
  [XmlEnum("conversation:GlobalUniqueSenders")] conversationGlobalUniqueSenders,
  /// <remarks />
  [XmlEnum("conversation:LastDeliveryTime")] conversationLastDeliveryTime,
  /// <remarks />
  [XmlEnum("conversation:GlobalLastDeliveryTime")] conversationGlobalLastDeliveryTime,
  /// <remarks />
  [XmlEnum("conversation:Categories")] conversationCategories,
  /// <remarks />
  [XmlEnum("conversation:GlobalCategories")] conversationGlobalCategories,
  /// <remarks />
  [XmlEnum("conversation:FlagStatus")] conversationFlagStatus,
  /// <remarks />
  [XmlEnum("conversation:GlobalFlagStatus")] conversationGlobalFlagStatus,
  /// <remarks />
  [XmlEnum("conversation:HasAttachments")] conversationHasAttachments,
  /// <remarks />
  [XmlEnum("conversation:GlobalHasAttachments")] conversationGlobalHasAttachments,
  /// <remarks />
  [XmlEnum("conversation:HasIrm")] conversationHasIrm,
  /// <remarks />
  [XmlEnum("conversation:GlobalHasIrm")] conversationGlobalHasIrm,
  /// <remarks />
  [XmlEnum("conversation:MessageCount")] conversationMessageCount,
  /// <remarks />
  [XmlEnum("conversation:GlobalMessageCount")] conversationGlobalMessageCount,
  /// <remarks />
  [XmlEnum("conversation:UnreadCount")] conversationUnreadCount,
  /// <remarks />
  [XmlEnum("conversation:GlobalUnreadCount")] conversationGlobalUnreadCount,
  /// <remarks />
  [XmlEnum("conversation:Size")] conversationSize,
  /// <remarks />
  [XmlEnum("conversation:GlobalSize")] conversationGlobalSize,
  /// <remarks />
  [XmlEnum("conversation:ItemClasses")] conversationItemClasses,
  /// <remarks />
  [XmlEnum("conversation:GlobalItemClasses")] conversationGlobalItemClasses,
  /// <remarks />
  [XmlEnum("conversation:Importance")] conversationImportance,
  /// <remarks />
  [XmlEnum("conversation:GlobalImportance")] conversationGlobalImportance,
  /// <remarks />
  [XmlEnum("conversation:ItemIds")] conversationItemIds,
  /// <remarks />
  [XmlEnum("conversation:GlobalItemIds")] conversationGlobalItemIds,
  /// <remarks />
  [XmlEnum("conversation:LastModifiedTime")] conversationLastModifiedTime,
  /// <remarks />
  [XmlEnum("conversation:InstanceKey")] conversationInstanceKey,
  /// <remarks />
  [XmlEnum("conversation:Preview")] conversationPreview,
  /// <remarks />
  [XmlEnum("conversation:NextPredictedAction")] conversationNextPredictedAction,
  /// <remarks />
  [XmlEnum("conversation:GroupingAction")] conversationGroupingAction,
  /// <remarks />
  [XmlEnum("conversation:IconIndex")] conversationIconIndex,
  /// <remarks />
  [XmlEnum("conversation:GlobalIconIndex")] conversationGlobalIconIndex,
  /// <remarks />
  [XmlEnum("conversation:DraftItemIds")] conversationDraftItemIds,
  /// <remarks />
  [XmlEnum("conversation:HasClutter")] conversationHasClutter,
  /// <remarks />
  [XmlEnum("persona:PersonaId")] personaPersonaId,
  /// <remarks />
  [XmlEnum("persona:PersonaType")] personaPersonaType,
  /// <remarks />
  [XmlEnum("persona:GivenName")] personaGivenName,
  /// <remarks />
  [XmlEnum("persona:CompanyName")] personaCompanyName,
  /// <remarks />
  [XmlEnum("persona:Surname")] personaSurname,
  /// <remarks />
  [XmlEnum("persona:DisplayName")] personaDisplayName,
  /// <remarks />
  [XmlEnum("persona:EmailAddress")] personaEmailAddress,
  /// <remarks />
  [XmlEnum("persona:FileAs")] personaFileAs,
  /// <remarks />
  [XmlEnum("persona:HomeCity")] personaHomeCity,
  /// <remarks />
  [XmlEnum("persona:CreationTime")] personaCreationTime,
  /// <remarks />
  [XmlEnum("persona:RelevanceScore")] personaRelevanceScore,
  /// <remarks />
  [XmlEnum("persona:WorkCity")] personaWorkCity,
  /// <remarks />
  [XmlEnum("persona:PersonaObjectStatus")] personaPersonaObjectStatus,
  /// <remarks />
  [XmlEnum("persona:FileAsId")] personaFileAsId,
  /// <remarks />
  [XmlEnum("persona:DisplayNamePrefix")] personaDisplayNamePrefix,
  /// <remarks />
  [XmlEnum("persona:YomiCompanyName")] personaYomiCompanyName,
  /// <remarks />
  [XmlEnum("persona:YomiFirstName")] personaYomiFirstName,
  /// <remarks />
  [XmlEnum("persona:YomiLastName")] personaYomiLastName,
  /// <remarks />
  [XmlEnum("persona:Title")] personaTitle,
  /// <remarks />
  [XmlEnum("persona:EmailAddress")] personaEmailAddress1,
  /// <remarks />
  [XmlEnum("persona:EmailAddresses")] personaEmailAddresses,
  /// <remarks />
  [XmlEnum("persona:PhoneNumber")] personaPhoneNumber,
  /// <remarks />
  [XmlEnum("persona:ImAddress")] personaImAddress,
  /// <remarks />
  [XmlEnum("persona:ImAddresses")] personaImAddresses,
  /// <remarks />
  [XmlEnum("persona:ImAddresses2")] personaImAddresses2,
  /// <remarks />
  [XmlEnum("persona:ImAddresses3")] personaImAddresses3,
  /// <remarks />
  [XmlEnum("persona:FolderIds")] personaFolderIds,
  /// <remarks />
  [XmlEnum("persona:Attributions")] personaAttributions,
  /// <remarks />
  [XmlEnum("persona:DisplayNames")] personaDisplayNames,
  /// <remarks />
  [XmlEnum("persona:Initials")] personaInitials,
  /// <remarks />
  [XmlEnum("persona:FileAses")] personaFileAses,
  /// <remarks />
  [XmlEnum("persona:FileAsIds")] personaFileAsIds,
  /// <remarks />
  [XmlEnum("persona:DisplayNamePrefixes")] personaDisplayNamePrefixes,
  /// <remarks />
  [XmlEnum("persona:GivenNames")] personaGivenNames,
  /// <remarks />
  [XmlEnum("persona:MiddleNames")] personaMiddleNames,
  /// <remarks />
  [XmlEnum("persona:Surnames")] personaSurnames,
  /// <remarks />
  [XmlEnum("persona:Generations")] personaGenerations,
  /// <remarks />
  [XmlEnum("persona:Nicknames")] personaNicknames,
  /// <remarks />
  [XmlEnum("persona:YomiCompanyNames")] personaYomiCompanyNames,
  /// <remarks />
  [XmlEnum("persona:YomiFirstNames")] personaYomiFirstNames,
  /// <remarks />
  [XmlEnum("persona:YomiLastNames")] personaYomiLastNames,
  /// <remarks />
  [XmlEnum("persona:BusinessPhoneNumbers")] personaBusinessPhoneNumbers,
  /// <remarks />
  [XmlEnum("persona:BusinessPhoneNumbers2")] personaBusinessPhoneNumbers2,
  /// <remarks />
  [XmlEnum("persona:HomePhones")] personaHomePhones,
  /// <remarks />
  [XmlEnum("persona:HomePhones2")] personaHomePhones2,
  /// <remarks />
  [XmlEnum("persona:MobilePhones")] personaMobilePhones,
  /// <remarks />
  [XmlEnum("persona:MobilePhones2")] personaMobilePhones2,
  /// <remarks />
  [XmlEnum("persona:AssistantPhoneNumbers")] personaAssistantPhoneNumbers,
  /// <remarks />
  [XmlEnum("persona:CallbackPhones")] personaCallbackPhones,
  /// <remarks />
  [XmlEnum("persona:CarPhones")] personaCarPhones,
  /// <remarks />
  [XmlEnum("persona:HomeFaxes")] personaHomeFaxes,
  /// <remarks />
  [XmlEnum("persona:OrganizationMainPhones")] personaOrganizationMainPhones,
  /// <remarks />
  [XmlEnum("persona:OtherFaxes")] personaOtherFaxes,
  /// <remarks />
  [XmlEnum("persona:OtherTelephones")] personaOtherTelephones,
  /// <remarks />
  [XmlEnum("persona:OtherPhones2")] personaOtherPhones2,
  /// <remarks />
  [XmlEnum("persona:Pagers")] personaPagers,
  /// <remarks />
  [XmlEnum("persona:RadioPhones")] personaRadioPhones,
  /// <remarks />
  [XmlEnum("persona:TelexNumbers")] personaTelexNumbers,
  /// <remarks />
  [XmlEnum("persona:WorkFaxes")] personaWorkFaxes,
  /// <remarks />
  [XmlEnum("persona:Emails1")] personaEmails1,
  /// <remarks />
  [XmlEnum("persona:Emails2")] personaEmails2,
  /// <remarks />
  [XmlEnum("persona:Emails3")] personaEmails3,
  /// <remarks />
  [XmlEnum("persona:BusinessHomePages")] personaBusinessHomePages,
  /// <remarks />
  [XmlEnum("persona:School")] personaSchool,
  /// <remarks />
  [XmlEnum("persona:PersonalHomePages")] personaPersonalHomePages,
  /// <remarks />
  [XmlEnum("persona:OfficeLocations")] personaOfficeLocations,
  /// <remarks />
  [XmlEnum("persona:BusinessAddresses")] personaBusinessAddresses,
  /// <remarks />
  [XmlEnum("persona:HomeAddresses")] personaHomeAddresses,
  /// <remarks />
  [XmlEnum("persona:OtherAddresses")] personaOtherAddresses,
  /// <remarks />
  [XmlEnum("persona:Titles")] personaTitles,
  /// <remarks />
  [XmlEnum("persona:Departments")] personaDepartments,
  /// <remarks />
  [XmlEnum("persona:CompanyNames")] personaCompanyNames,
  /// <remarks />
  [XmlEnum("persona:Managers")] personaManagers,
  /// <remarks />
  [XmlEnum("persona:AssistantNames")] personaAssistantNames,
  /// <remarks />
  [XmlEnum("persona:Professions")] personaProfessions,
  /// <remarks />
  [XmlEnum("persona:SpouseNames")] personaSpouseNames,
  /// <remarks />
  [XmlEnum("persona:Hobbies")] personaHobbies,
  /// <remarks />
  [XmlEnum("persona:WeddingAnniversaries")] personaWeddingAnniversaries,
  /// <remarks />
  [XmlEnum("persona:Birthdays")] personaBirthdays,
  /// <remarks />
  [XmlEnum("persona:Children")] personaChildren,
  /// <remarks />
  [XmlEnum("persona:Locations")] personaLocations,
  /// <remarks />
  [XmlEnum("persona:ExtendedProperties")] personaExtendedProperties,
  /// <remarks />
  [XmlEnum("persona:PostalAddress")] personaPostalAddress,
  /// <remarks />
  [XmlEnum("persona:Bodies")] personaBodies,
}
