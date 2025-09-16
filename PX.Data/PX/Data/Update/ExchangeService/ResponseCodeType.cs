// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ResponseCodeType
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
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public enum ResponseCodeType
{
  /// <remarks />
  NoError,
  /// <remarks />
  ErrorAccessDenied,
  /// <remarks />
  ErrorAccessModeSpecified,
  /// <remarks />
  ErrorAccountDisabled,
  /// <remarks />
  ErrorAddDelegatesFailed,
  /// <remarks />
  ErrorAddressSpaceNotFound,
  /// <remarks />
  ErrorADOperation,
  /// <remarks />
  ErrorADSessionFilter,
  /// <remarks />
  ErrorADUnavailable,
  /// <remarks />
  ErrorAutoDiscoverFailed,
  /// <remarks />
  ErrorAffectedTaskOccurrencesRequired,
  /// <remarks />
  ErrorAttachmentNestLevelLimitExceeded,
  /// <remarks />
  ErrorAttachmentSizeLimitExceeded,
  /// <remarks />
  ErrorArchiveFolderPathCreation,
  /// <remarks />
  ErrorArchiveMailboxNotEnabled,
  /// <remarks />
  ErrorArchiveMailboxServiceDiscoveryFailed,
  /// <remarks />
  ErrorAvailabilityConfigNotFound,
  /// <remarks />
  ErrorBatchProcessingStopped,
  /// <remarks />
  ErrorCalendarCannotMoveOrCopyOccurrence,
  /// <remarks />
  ErrorCalendarCannotUpdateDeletedItem,
  /// <remarks />
  ErrorCalendarCannotUseIdForOccurrenceId,
  /// <remarks />
  ErrorCalendarCannotUseIdForRecurringMasterId,
  /// <remarks />
  ErrorCalendarDurationIsTooLong,
  /// <remarks />
  ErrorCalendarEndDateIsEarlierThanStartDate,
  /// <remarks />
  ErrorCalendarFolderIsInvalidForCalendarView,
  /// <remarks />
  ErrorCalendarInvalidAttributeValue,
  /// <remarks />
  ErrorCalendarInvalidDayForTimeChangePattern,
  /// <remarks />
  ErrorCalendarInvalidDayForWeeklyRecurrence,
  /// <remarks />
  ErrorCalendarInvalidPropertyState,
  /// <remarks />
  ErrorCalendarInvalidPropertyValue,
  /// <remarks />
  ErrorCalendarInvalidRecurrence,
  /// <remarks />
  ErrorCalendarInvalidTimeZone,
  /// <remarks />
  ErrorCalendarIsCancelledForAccept,
  /// <remarks />
  ErrorCalendarIsCancelledForDecline,
  /// <remarks />
  ErrorCalendarIsCancelledForRemove,
  /// <remarks />
  ErrorCalendarIsCancelledForTentative,
  /// <remarks />
  ErrorCalendarIsDelegatedForAccept,
  /// <remarks />
  ErrorCalendarIsDelegatedForDecline,
  /// <remarks />
  ErrorCalendarIsDelegatedForRemove,
  /// <remarks />
  ErrorCalendarIsDelegatedForTentative,
  /// <remarks />
  ErrorCalendarIsNotOrganizer,
  /// <remarks />
  ErrorCalendarIsOrganizerForAccept,
  /// <remarks />
  ErrorCalendarIsOrganizerForDecline,
  /// <remarks />
  ErrorCalendarIsOrganizerForRemove,
  /// <remarks />
  ErrorCalendarIsOrganizerForTentative,
  /// <remarks />
  ErrorCalendarOccurrenceIndexIsOutOfRecurrenceRange,
  /// <remarks />
  ErrorCalendarOccurrenceIsDeletedFromRecurrence,
  /// <remarks />
  ErrorCalendarOutOfRange,
  /// <remarks />
  ErrorCalendarMeetingRequestIsOutOfDate,
  /// <remarks />
  ErrorCalendarViewRangeTooBig,
  /// <remarks />
  ErrorCallerIsInvalidADAccount,
  /// <remarks />
  ErrorCannotArchiveCalendarContactTaskFolderException,
  /// <remarks />
  ErrorCannotArchiveItemsInPublicFolders,
  /// <remarks />
  ErrorCannotArchiveItemsInArchiveMailbox,
  /// <remarks />
  ErrorCannotCreateCalendarItemInNonCalendarFolder,
  /// <remarks />
  ErrorCannotCreateContactInNonContactFolder,
  /// <remarks />
  ErrorCannotCreatePostItemInNonMailFolder,
  /// <remarks />
  ErrorCannotCreateTaskInNonTaskFolder,
  /// <remarks />
  ErrorCannotDeleteObject,
  /// <remarks />
  ErrorCannotDisableMandatoryExtension,
  /// <remarks />
  ErrorCannotGetSourceFolderPath,
  /// <remarks />
  ErrorCannotGetExternalEcpUrl,
  /// <remarks />
  ErrorCannotOpenFileAttachment,
  /// <remarks />
  ErrorCannotDeleteTaskOccurrence,
  /// <remarks />
  ErrorCannotEmptyFolder,
  /// <remarks />
  ErrorCannotSetCalendarPermissionOnNonCalendarFolder,
  /// <remarks />
  ErrorCannotSetNonCalendarPermissionOnCalendarFolder,
  /// <remarks />
  ErrorCannotSetPermissionUnknownEntries,
  /// <remarks />
  ErrorCannotSpecifySearchFolderAsSourceFolder,
  /// <remarks />
  ErrorCannotUseFolderIdForItemId,
  /// <remarks />
  ErrorCannotUseItemIdForFolderId,
  /// <remarks />
  ErrorChangeKeyRequired,
  /// <remarks />
  ErrorChangeKeyRequiredForWriteOperations,
  /// <remarks />
  ErrorClientDisconnected,
  /// <remarks />
  ErrorClientIntentInvalidStateDefinition,
  /// <remarks />
  ErrorClientIntentNotFound,
  /// <remarks />
  ErrorConnectionFailed,
  /// <remarks />
  ErrorContainsFilterWrongType,
  /// <remarks />
  ErrorContentConversionFailed,
  /// <remarks />
  ErrorContentIndexingNotEnabled,
  /// <remarks />
  ErrorCorruptData,
  /// <remarks />
  ErrorCreateItemAccessDenied,
  /// <remarks />
  ErrorCreateManagedFolderPartialCompletion,
  /// <remarks />
  ErrorCreateSubfolderAccessDenied,
  /// <remarks />
  ErrorCrossMailboxMoveCopy,
  /// <remarks />
  ErrorCrossSiteRequest,
  /// <remarks />
  ErrorDataSizeLimitExceeded,
  /// <remarks />
  ErrorDataSourceOperation,
  /// <remarks />
  ErrorDelegateAlreadyExists,
  /// <remarks />
  ErrorDelegateCannotAddOwner,
  /// <remarks />
  ErrorDelegateMissingConfiguration,
  /// <remarks />
  ErrorDelegateNoUser,
  /// <remarks />
  ErrorDelegateValidationFailed,
  /// <remarks />
  ErrorDeleteDistinguishedFolder,
  /// <remarks />
  ErrorDeleteItemsFailed,
  /// <remarks />
  ErrorDeleteUnifiedMessagingPromptFailed,
  /// <remarks />
  ErrorDistinguishedUserNotSupported,
  /// <remarks />
  ErrorDistributionListMemberNotExist,
  /// <remarks />
  ErrorDuplicateInputFolderNames,
  /// <remarks />
  ErrorDuplicateUserIdsSpecified,
  /// <remarks />
  ErrorEmailAddressMismatch,
  /// <remarks />
  ErrorEventNotFound,
  /// <remarks />
  ErrorExceededConnectionCount,
  /// <remarks />
  ErrorExceededSubscriptionCount,
  /// <remarks />
  ErrorExceededFindCountLimit,
  /// <remarks />
  ErrorExpiredSubscription,
  /// <remarks />
  ErrorExtensionNotFound,
  /// <remarks />
  ErrorFolderCorrupt,
  /// <remarks />
  ErrorFolderNotFound,
  /// <remarks />
  ErrorFolderPropertRequestFailed,
  /// <remarks />
  ErrorFolderSave,
  /// <remarks />
  ErrorFolderSaveFailed,
  /// <remarks />
  ErrorFolderSavePropertyError,
  /// <remarks />
  ErrorFolderExists,
  /// <remarks />
  ErrorFreeBusyGenerationFailed,
  /// <remarks />
  ErrorGetServerSecurityDescriptorFailed,
  /// <remarks />
  ErrorImContactLimitReached,
  /// <remarks />
  ErrorImGroupDisplayNameAlreadyExists,
  /// <remarks />
  ErrorImGroupLimitReached,
  /// <remarks />
  ErrorImpersonateUserDenied,
  /// <remarks />
  ErrorImpersonationDenied,
  /// <remarks />
  ErrorImpersonationFailed,
  /// <remarks />
  ErrorIncorrectSchemaVersion,
  /// <remarks />
  ErrorIncorrectUpdatePropertyCount,
  /// <remarks />
  ErrorIndividualMailboxLimitReached,
  /// <remarks />
  ErrorInsufficientResources,
  /// <remarks />
  ErrorInternalServerError,
  /// <remarks />
  ErrorInternalServerTransientError,
  /// <remarks />
  ErrorInvalidAccessLevel,
  /// <remarks />
  ErrorInvalidArgument,
  /// <remarks />
  ErrorInvalidAttachmentId,
  /// <remarks />
  ErrorInvalidAttachmentSubfilter,
  /// <remarks />
  ErrorInvalidAttachmentSubfilterTextFilter,
  /// <remarks />
  ErrorInvalidAuthorizationContext,
  /// <remarks />
  ErrorInvalidChangeKey,
  /// <remarks />
  ErrorInvalidClientSecurityContext,
  /// <remarks />
  ErrorInvalidCompleteDate,
  /// <remarks />
  ErrorInvalidContactEmailAddress,
  /// <remarks />
  ErrorInvalidContactEmailIndex,
  /// <remarks />
  ErrorInvalidCrossForestCredentials,
  /// <remarks />
  ErrorInvalidDelegatePermission,
  /// <remarks />
  ErrorInvalidDelegateUserId,
  /// <remarks />
  ErrorInvalidExcludesRestriction,
  /// <remarks />
  ErrorInvalidExpressionTypeForSubFilter,
  /// <remarks />
  ErrorInvalidExtendedProperty,
  /// <remarks />
  ErrorInvalidExtendedPropertyValue,
  /// <remarks />
  ErrorInvalidFolderId,
  /// <remarks />
  ErrorInvalidFolderTypeForOperation,
  /// <remarks />
  ErrorInvalidFractionalPagingParameters,
  /// <remarks />
  ErrorInvalidFreeBusyViewType,
  /// <remarks />
  ErrorInvalidId,
  /// <remarks />
  ErrorInvalidIdEmpty,
  /// <remarks />
  ErrorInvalidIdMalformed,
  /// <remarks />
  ErrorInvalidIdMalformedEwsLegacyIdFormat,
  /// <remarks />
  ErrorInvalidIdMonikerTooLong,
  /// <remarks />
  ErrorInvalidIdNotAnItemAttachmentId,
  /// <remarks />
  ErrorInvalidIdReturnedByResolveNames,
  /// <remarks />
  ErrorInvalidIdStoreObjectIdTooLong,
  /// <remarks />
  ErrorInvalidIdTooManyAttachmentLevels,
  /// <remarks />
  ErrorInvalidIdXml,
  /// <remarks />
  ErrorInvalidImContactId,
  /// <remarks />
  ErrorInvalidImDistributionGroupSmtpAddress,
  /// <remarks />
  ErrorInvalidImGroupId,
  /// <remarks />
  ErrorInvalidIndexedPagingParameters,
  /// <remarks />
  ErrorInvalidInternetHeaderChildNodes,
  /// <remarks />
  ErrorInvalidItemForOperationArchiveItem,
  /// <remarks />
  ErrorInvalidItemForOperationCreateItemAttachment,
  /// <remarks />
  ErrorInvalidItemForOperationCreateItem,
  /// <remarks />
  ErrorInvalidItemForOperationAcceptItem,
  /// <remarks />
  ErrorInvalidItemForOperationDeclineItem,
  /// <remarks />
  ErrorInvalidItemForOperationCancelItem,
  /// <remarks />
  ErrorInvalidItemForOperationExpandDL,
  /// <remarks />
  ErrorInvalidItemForOperationRemoveItem,
  /// <remarks />
  ErrorInvalidItemForOperationSendItem,
  /// <remarks />
  ErrorInvalidItemForOperationTentative,
  /// <remarks />
  ErrorInvalidLogonType,
  /// <remarks />
  ErrorInvalidLikeRequest,
  /// <remarks />
  ErrorInvalidMailbox,
  /// <remarks />
  ErrorInvalidManagedFolderProperty,
  /// <remarks />
  ErrorInvalidManagedFolderQuota,
  /// <remarks />
  ErrorInvalidManagedFolderSize,
  /// <remarks />
  ErrorInvalidMergedFreeBusyInterval,
  /// <remarks />
  ErrorInvalidNameForNameResolution,
  /// <remarks />
  ErrorInvalidOperation,
  /// <remarks />
  ErrorInvalidNetworkServiceContext,
  /// <remarks />
  ErrorInvalidOofParameter,
  /// <remarks />
  ErrorInvalidPagingMaxRows,
  /// <remarks />
  ErrorInvalidParentFolder,
  /// <remarks />
  ErrorInvalidPercentCompleteValue,
  /// <remarks />
  ErrorInvalidPermissionSettings,
  /// <remarks />
  ErrorInvalidPhoneCallId,
  /// <remarks />
  ErrorInvalidPhoneNumber,
  /// <remarks />
  ErrorInvalidUserInfo,
  /// <remarks />
  ErrorInvalidPropertyAppend,
  /// <remarks />
  ErrorInvalidPropertyDelete,
  /// <remarks />
  ErrorInvalidPropertyForExists,
  /// <remarks />
  ErrorInvalidPropertyForOperation,
  /// <remarks />
  ErrorInvalidPropertyRequest,
  /// <remarks />
  ErrorInvalidPropertySet,
  /// <remarks />
  ErrorInvalidPropertyUpdateSentMessage,
  /// <remarks />
  ErrorInvalidProxySecurityContext,
  /// <remarks />
  ErrorInvalidPullSubscriptionId,
  /// <remarks />
  ErrorInvalidPushSubscriptionUrl,
  /// <remarks />
  ErrorInvalidRecipients,
  /// <remarks />
  ErrorInvalidRecipientSubfilter,
  /// <remarks />
  ErrorInvalidRecipientSubfilterComparison,
  /// <remarks />
  ErrorInvalidRecipientSubfilterOrder,
  /// <remarks />
  ErrorInvalidRecipientSubfilterTextFilter,
  /// <remarks />
  ErrorInvalidReferenceItem,
  /// <remarks />
  ErrorInvalidRequest,
  /// <remarks />
  ErrorInvalidRestriction,
  /// <remarks />
  ErrorInvalidRetentionTagTypeMismatch,
  /// <remarks />
  ErrorInvalidRetentionTagInvisible,
  /// <remarks />
  ErrorInvalidRetentionTagInheritance,
  /// <remarks />
  ErrorInvalidRetentionTagIdGuid,
  /// <remarks />
  ErrorInvalidRoutingType,
  /// <remarks />
  ErrorInvalidScheduledOofDuration,
  /// <remarks />
  ErrorInvalidSchemaVersionForMailboxVersion,
  /// <remarks />
  ErrorInvalidSecurityDescriptor,
  /// <remarks />
  ErrorInvalidSendItemSaveSettings,
  /// <remarks />
  ErrorInvalidSerializedAccessToken,
  /// <remarks />
  ErrorInvalidServerVersion,
  /// <remarks />
  ErrorInvalidSid,
  /// <remarks />
  ErrorInvalidSIPUri,
  /// <remarks />
  ErrorInvalidSmtpAddress,
  /// <remarks />
  ErrorInvalidSubfilterType,
  /// <remarks />
  ErrorInvalidSubfilterTypeNotAttendeeType,
  /// <remarks />
  ErrorInvalidSubfilterTypeNotRecipientType,
  /// <remarks />
  ErrorInvalidSubscription,
  /// <remarks />
  ErrorInvalidSubscriptionRequest,
  /// <remarks />
  ErrorInvalidSyncStateData,
  /// <remarks />
  ErrorInvalidTimeInterval,
  /// <remarks />
  ErrorInvalidUserOofSettings,
  /// <remarks />
  ErrorInvalidUserPrincipalName,
  /// <remarks />
  ErrorInvalidUserSid,
  /// <remarks />
  ErrorInvalidUserSidMissingUPN,
  /// <remarks />
  ErrorInvalidValueForProperty,
  /// <remarks />
  ErrorInvalidWatermark,
  /// <remarks />
  ErrorIPGatewayNotFound,
  /// <remarks />
  ErrorIrresolvableConflict,
  /// <remarks />
  ErrorItemCorrupt,
  /// <remarks />
  ErrorItemNotFound,
  /// <remarks />
  ErrorItemPropertyRequestFailed,
  /// <remarks />
  ErrorItemSave,
  /// <remarks />
  ErrorItemSavePropertyError,
  /// <remarks />
  ErrorLegacyMailboxFreeBusyViewTypeNotMerged,
  /// <remarks />
  ErrorLocalServerObjectNotFound,
  /// <remarks />
  ErrorLogonAsNetworkServiceFailed,
  /// <remarks />
  ErrorMailboxConfiguration,
  /// <remarks />
  ErrorMailboxDataArrayEmpty,
  /// <remarks />
  ErrorMailboxDataArrayTooBig,
  /// <remarks />
  ErrorMailboxHoldNotFound,
  /// <remarks />
  ErrorMailboxLogonFailed,
  /// <remarks />
  ErrorMailboxMoveInProgress,
  /// <remarks />
  ErrorMailboxStoreUnavailable,
  /// <remarks />
  ErrorMailRecipientNotFound,
  /// <remarks />
  ErrorMailTipsDisabled,
  /// <remarks />
  ErrorManagedFolderAlreadyExists,
  /// <remarks />
  ErrorManagedFolderNotFound,
  /// <remarks />
  ErrorManagedFoldersRootFailure,
  /// <remarks />
  ErrorMeetingSuggestionGenerationFailed,
  /// <remarks />
  ErrorMessageDispositionRequired,
  /// <remarks />
  ErrorMessageSizeExceeded,
  /// <remarks />
  ErrorMimeContentConversionFailed,
  /// <remarks />
  ErrorMimeContentInvalid,
  /// <remarks />
  ErrorMimeContentInvalidBase64String,
  /// <remarks />
  ErrorMissingArgument,
  /// <remarks />
  ErrorMissingEmailAddress,
  /// <remarks />
  ErrorMissingEmailAddressForManagedFolder,
  /// <remarks />
  ErrorMissingInformationEmailAddress,
  /// <remarks />
  ErrorMissingInformationReferenceItemId,
  /// <remarks />
  ErrorMissingItemForCreateItemAttachment,
  /// <remarks />
  ErrorMissingManagedFolderId,
  /// <remarks />
  ErrorMissingRecipients,
  /// <remarks />
  ErrorMissingUserIdInformation,
  /// <remarks />
  ErrorMoreThanOneAccessModeSpecified,
  /// <remarks />
  ErrorMoveCopyFailed,
  /// <remarks />
  ErrorMoveDistinguishedFolder,
  /// <remarks />
  ErrorMultiLegacyMailboxAccess,
  /// <remarks />
  ErrorNameResolutionMultipleResults,
  /// <remarks />
  ErrorNameResolutionNoMailbox,
  /// <remarks />
  ErrorNameResolutionNoResults,
  /// <remarks />
  ErrorNoApplicableProxyCASServersAvailable,
  /// <remarks />
  ErrorNoCalendar,
  /// <remarks />
  ErrorNoDestinationCASDueToKerberosRequirements,
  /// <remarks />
  ErrorNoDestinationCASDueToSSLRequirements,
  /// <remarks />
  ErrorNoDestinationCASDueToVersionMismatch,
  /// <remarks />
  ErrorNoFolderClassOverride,
  /// <remarks />
  ErrorNoFreeBusyAccess,
  /// <remarks />
  ErrorNonExistentMailbox,
  /// <remarks />
  ErrorNonPrimarySmtpAddress,
  /// <remarks />
  ErrorNoPropertyTagForCustomProperties,
  /// <remarks />
  ErrorNoPublicFolderReplicaAvailable,
  /// <remarks />
  ErrorNoPublicFolderServerAvailable,
  /// <remarks />
  ErrorNoRespondingCASInDestinationSite,
  /// <remarks />
  ErrorNotDelegate,
  /// <remarks />
  ErrorNotEnoughMemory,
  /// <remarks />
  ErrorObjectTypeChanged,
  /// <remarks />
  ErrorOccurrenceCrossingBoundary,
  /// <remarks />
  ErrorOccurrenceTimeSpanTooBig,
  /// <remarks />
  ErrorOperationNotAllowedWithPublicFolderRoot,
  /// <remarks />
  ErrorParentFolderIdRequired,
  /// <remarks />
  ErrorParentFolderNotFound,
  /// <remarks />
  ErrorPasswordChangeRequired,
  /// <remarks />
  ErrorPasswordExpired,
  /// <remarks />
  ErrorPhoneNumberNotDialable,
  /// <remarks />
  ErrorPropertyUpdate,
  /// <remarks />
  ErrorPromptPublishingOperationFailed,
  /// <remarks />
  ErrorPropertyValidationFailure,
  /// <remarks />
  ErrorProxiedSubscriptionCallFailure,
  /// <remarks />
  ErrorProxyCallFailed,
  /// <remarks />
  ErrorProxyGroupSidLimitExceeded,
  /// <remarks />
  ErrorProxyRequestNotAllowed,
  /// <remarks />
  ErrorProxyRequestProcessingFailed,
  /// <remarks />
  ErrorProxyServiceDiscoveryFailed,
  /// <remarks />
  ErrorProxyTokenExpired,
  /// <remarks />
  ErrorPublicFolderMailboxDiscoveryFailed,
  /// <remarks />
  ErrorPublicFolderOperationFailed,
  /// <remarks />
  ErrorPublicFolderRequestProcessingFailed,
  /// <remarks />
  ErrorPublicFolderServerNotFound,
  /// <remarks />
  ErrorPublicFolderSyncException,
  /// <remarks />
  ErrorQueryFilterTooLong,
  /// <remarks />
  ErrorQuotaExceeded,
  /// <remarks />
  ErrorReadEventsFailed,
  /// <remarks />
  ErrorReadReceiptNotPending,
  /// <remarks />
  ErrorRecurrenceEndDateTooBig,
  /// <remarks />
  ErrorRecurrenceHasNoOccurrence,
  /// <remarks />
  ErrorRemoveDelegatesFailed,
  /// <remarks />
  ErrorRequestAborted,
  /// <remarks />
  ErrorRequestStreamTooBig,
  /// <remarks />
  ErrorRequiredPropertyMissing,
  /// <remarks />
  ErrorResolveNamesInvalidFolderType,
  /// <remarks />
  ErrorResolveNamesOnlyOneContactsFolderAllowed,
  /// <remarks />
  ErrorResponseSchemaValidation,
  /// <remarks />
  ErrorRestrictionTooLong,
  /// <remarks />
  ErrorRestrictionTooComplex,
  /// <remarks />
  ErrorResultSetTooBig,
  /// <remarks />
  ErrorInvalidExchangeImpersonationHeaderData,
  /// <remarks />
  ErrorSavedItemFolderNotFound,
  /// <remarks />
  ErrorSchemaValidation,
  /// <remarks />
  ErrorSearchFolderNotInitialized,
  /// <remarks />
  ErrorSendAsDenied,
  /// <remarks />
  ErrorSendMeetingCancellationsRequired,
  /// <remarks />
  ErrorSendMeetingInvitationsOrCancellationsRequired,
  /// <remarks />
  ErrorSendMeetingInvitationsRequired,
  /// <remarks />
  ErrorSentMeetingRequestUpdate,
  /// <remarks />
  ErrorSentTaskRequestUpdate,
  /// <remarks />
  ErrorServerBusy,
  /// <remarks />
  ErrorServiceDiscoveryFailed,
  /// <remarks />
  ErrorStaleObject,
  /// <remarks />
  ErrorSubmissionQuotaExceeded,
  /// <remarks />
  ErrorSubscriptionAccessDenied,
  /// <remarks />
  ErrorSubscriptionDelegateAccessNotSupported,
  /// <remarks />
  ErrorSubscriptionNotFound,
  /// <remarks />
  ErrorSubscriptionUnsubscribed,
  /// <remarks />
  ErrorSyncFolderNotFound,
  /// <remarks />
  ErrorTeamMailboxNotFound,
  /// <remarks />
  ErrorTeamMailboxNotLinkedToSharePoint,
  /// <remarks />
  ErrorTeamMailboxUrlValidationFailed,
  /// <remarks />
  ErrorTeamMailboxNotAuthorizedOwner,
  /// <remarks />
  ErrorTeamMailboxActiveToPendingDelete,
  /// <remarks />
  ErrorTeamMailboxFailedSendingNotifications,
  /// <remarks />
  ErrorTeamMailboxErrorUnknown,
  /// <remarks />
  ErrorTimeIntervalTooBig,
  /// <remarks />
  ErrorTimeoutExpired,
  /// <remarks />
  ErrorTimeZone,
  /// <remarks />
  ErrorToFolderNotFound,
  /// <remarks />
  ErrorTokenSerializationDenied,
  /// <remarks />
  ErrorTooManyObjectsOpened,
  /// <remarks />
  ErrorUpdatePropertyMismatch,
  /// <remarks />
  ErrorUnifiedMessagingDialPlanNotFound,
  /// <remarks />
  ErrorUnifiedMessagingReportDataNotFound,
  /// <remarks />
  ErrorUnifiedMessagingPromptNotFound,
  /// <remarks />
  ErrorUnifiedMessagingRequestFailed,
  /// <remarks />
  ErrorUnifiedMessagingServerNotFound,
  /// <remarks />
  ErrorUnableToGetUserOofSettings,
  /// <remarks />
  ErrorUnableToRemoveImContactFromGroup,
  /// <remarks />
  ErrorUnsupportedSubFilter,
  /// <remarks />
  ErrorUnsupportedCulture,
  /// <remarks />
  ErrorUnsupportedMapiPropertyType,
  /// <remarks />
  ErrorUnsupportedMimeConversion,
  /// <remarks />
  ErrorUnsupportedPathForQuery,
  /// <remarks />
  ErrorUnsupportedPathForSortGroup,
  /// <remarks />
  ErrorUnsupportedPropertyDefinition,
  /// <remarks />
  ErrorUnsupportedQueryFilter,
  /// <remarks />
  ErrorUnsupportedRecurrence,
  /// <remarks />
  ErrorUnsupportedTypeForConversion,
  /// <remarks />
  ErrorUpdateDelegatesFailed,
  /// <remarks />
  ErrorUserNotUnifiedMessagingEnabled,
  /// <remarks />
  ErrorVoiceMailNotImplemented,
  /// <remarks />
  ErrorValueOutOfRange,
  /// <remarks />
  ErrorVirusDetected,
  /// <remarks />
  ErrorVirusMessageDeleted,
  /// <remarks />
  ErrorWebRequestInInvalidState,
  /// <remarks />
  ErrorWin32InteropError,
  /// <remarks />
  ErrorWorkingHoursSaveFailed,
  /// <remarks />
  ErrorWorkingHoursXmlMalformed,
  /// <remarks />
  ErrorWrongServerVersion,
  /// <remarks />
  ErrorWrongServerVersionDelegate,
  /// <remarks />
  ErrorMissingInformationSharingFolderId,
  /// <remarks />
  ErrorDuplicateSOAPHeader,
  /// <remarks />
  ErrorSharingSynchronizationFailed,
  /// <remarks />
  ErrorSharingNoExternalEwsAvailable,
  /// <remarks />
  ErrorFreeBusyDLLimitReached,
  /// <remarks />
  ErrorInvalidGetSharingFolderRequest,
  /// <remarks />
  ErrorNotAllowedExternalSharingByPolicy,
  /// <remarks />
  ErrorUserNotAllowedByPolicy,
  /// <remarks />
  ErrorPermissionNotAllowedByPolicy,
  /// <remarks />
  ErrorOrganizationNotFederated,
  /// <remarks />
  ErrorMailboxFailover,
  /// <remarks />
  ErrorInvalidExternalSharingInitiator,
  /// <remarks />
  ErrorMessageTrackingPermanentError,
  /// <remarks />
  ErrorMessageTrackingTransientError,
  /// <remarks />
  ErrorMessageTrackingNoSuchDomain,
  /// <remarks />
  ErrorUserWithoutFederatedProxyAddress,
  /// <remarks />
  ErrorInvalidOrganizationRelationshipForFreeBusy,
  /// <remarks />
  ErrorInvalidFederatedOrganizationId,
  /// <remarks />
  ErrorInvalidExternalSharingSubscriber,
  /// <remarks />
  ErrorInvalidSharingData,
  /// <remarks />
  ErrorInvalidSharingMessage,
  /// <remarks />
  ErrorNotSupportedSharingMessage,
  /// <remarks />
  ErrorApplyConversationActionFailed,
  /// <remarks />
  ErrorInboxRulesValidationError,
  /// <remarks />
  ErrorOutlookRuleBlobExists,
  /// <remarks />
  ErrorRulesOverQuota,
  /// <remarks />
  ErrorNewEventStreamConnectionOpened,
  /// <remarks />
  ErrorMissedNotificationEvents,
  /// <remarks />
  ErrorDuplicateLegacyDistinguishedName,
  /// <remarks />
  ErrorInvalidClientAccessTokenRequest,
  /// <remarks />
  ErrorNoSpeechDetected,
  /// <remarks />
  ErrorUMServerUnavailable,
  /// <remarks />
  ErrorRecipientNotFound,
  /// <remarks />
  ErrorRecognizerNotInstalled,
  /// <remarks />
  ErrorSpeechGrammarError,
  /// <remarks />
  ErrorInvalidManagementRoleHeader,
  /// <remarks />
  ErrorLocationServicesDisabled,
  /// <remarks />
  ErrorLocationServicesRequestTimedOut,
  /// <remarks />
  ErrorLocationServicesRequestFailed,
  /// <remarks />
  ErrorLocationServicesInvalidRequest,
  /// <remarks />
  ErrorWeatherServiceDisabled,
  /// <remarks />
  ErrorMailboxScopeNotAllowedWithoutQueryString,
  /// <remarks />
  ErrorArchiveMailboxSearchFailed,
  /// <remarks />
  ErrorGetRemoteArchiveFolderFailed,
  /// <remarks />
  ErrorFindRemoteArchiveFolderFailed,
  /// <remarks />
  ErrorGetRemoteArchiveItemFailed,
  /// <remarks />
  ErrorExportRemoteArchiveItemsFailed,
  /// <remarks />
  ErrorInvalidPhotoSize,
  /// <remarks />
  ErrorSearchQueryHasTooManyKeywords,
  /// <remarks />
  ErrorSearchTooManyMailboxes,
  /// <remarks />
  ErrorInvalidRetentionTagNone,
  /// <remarks />
  ErrorDiscoverySearchesDisabled,
  /// <remarks />
  ErrorCalendarSeekToConditionNotSupported,
  /// <remarks />
  ErrorCalendarIsGroupMailboxForAccept,
  /// <remarks />
  ErrorCalendarIsGroupMailboxForDecline,
  /// <remarks />
  ErrorCalendarIsGroupMailboxForTentative,
  /// <remarks />
  ErrorCalendarIsGroupMailboxForSuppressReadReceipt,
}
