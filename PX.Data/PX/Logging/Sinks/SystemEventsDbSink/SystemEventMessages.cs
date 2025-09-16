// Decompiled with JetBrains decompiler
// Type: PX.Logging.Sinks.SystemEventsDbSink.SystemEventMessages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Logging.Sinks.SystemEventsDbSink;

[PXLocalizable]
public class SystemEventMessages
{
  public const string Scheduler_SchedulerThrewException = "Automation Scheduler has failed with an exception";
  public const string Scheduler_AtLeastOneErrorProcessingFailed = "At least one record has not been processed successfully";
  public const string Scheduler_SchedulerInitialized = "Automation Scheduler has been initialized";
  public const string Scheduler_SchedulerStopped = "Automation Scheduler has been aborted";
  public const string Scheduler_ScheduleSkipped = "Automation Schedule scenario has not been performed due to the database lockout";
  public const string Scheduler_ActionDoesntExist = "An action with the {ActionName} name does not exist on the {ScreenID} form. Specify a correct action for the {ScheduleName} schedule.";
  public const string Scheduler_SavingToHistoryFailed = "The processed {ItemType} item does not contain NoteID, and the processing information cannot be saved to the schedule history.";
  public const string Customization_CustomizationPublished = "A customization project has been published";
  public const string Customization_CustomizationUnpublished = "A customization project has been unpublished";
  public const string Customization_CustomizationFailedWithError = "Publishing of a customization project failed with an error";
  public const string Customization_ValidationCompleted = "Validation of a customization project has been completed";
  public const string Customization_ValidationFailed = "Validation failed";
  public const string Customization_PublishBegin = "Publishing via the customization API has started";
  public const string Customization_PublishedExceptPlugins = "A customization project has been published (except plug-ins)";
  public const string Customization_PluginsExecutionCompleted = "Execution of a customization project plug-ins has been completed";
  public const string System_SystemSiteShutDown = "The site is shutting down";
  public const string System_SiteStart = "The site has started";
  public const string System_RestartRequested = "A request to restart the application has been made";
  public const string System_EnableFtsRequested = "A request to enable FTS has been made";
  public const string System_RestartRequestInProgress = "The Application Restart Observer is restarting the application";
  public const string System_IsvServiceFailed = "Failed to invoke the ISV service.";
  public const string System_SiteUpgradeStarted = "Site upgrade has started";
  public const string System_DbUpgradeStarted = "Database upgrade has started";
  public const string System_SiteUpgradeApplied = "Site upgrade has been applied successfully";
  public const string System_DbUpgradeApplied = "Database upgrade has been applied successfully";
  public const string System_SiteUpgradeFailed = "Site upgrade failed with exception";
  public const string System_DbUpgradeFailed = "Database upgrade completed with errors";
  public const string System_TenantCreated = "A new tenant has been created";
  public const string System_SnapshotCreated = "A snapshot has been created";
  public const string System_TenantDeletionStart = "Tenant deletion has been started";
  public const string System_TenantDeletionEnd = "Tenant deletion has been completed";
  public const string System_SnapshotCreationFailed = "Failed to create a snapshot";
  public const string System_CompanyCopyCreated = "A tenant copy has been created";
  public const string System_CompanyCopyFailed = "Creating of a tenant copy has failed";
  public const string System_SnapshotRestored = "A snapshot has been successfully restored";
  public const string System_SnapshotRestorationFailed = "Snapshot restoration failed";
  public const string PushNotifications_DispatcherRestarted = "Push notification queue dispatcher has been restarted";
  public const string System_SnapshotDeletionStart = "Snapshot deletion has been started";
  public const string System_SnapshotDeletionEnd = "Snapshot deletion has been completed";
  public const string System_OrphanedSnapshotDeletionStart = "Orphaned snapshot deletion has been started";
  public const string System_OrphanedSnapshotDeletionEnd = "Orphaned snapshot deletion has been completed";
  public const string PushNotifications_DispatcherFailed = "Push notification queue dispatcher failed";
  public const string PushNotifications_InvalidQueueStatus = "Push notification queue has issues";
  public const string PushNotifications_SendingFailed = "Sending of push notification has failed";
  public const string PushNotifications_ProcessingFailed = "Processing of a push notification has failed";
  public const string PushNotifications_LongProcessingDetected = "Processing of a push notification took longer than expected";
  public const string BPEventNotifications_DispatcherRestarted = "Business event queue dispatcher has been restarted";
  public const string BPEventNotifications_DispatcherFailed = "Business event queue dispatcher failed";
  public const string BPEventNotifications_InvalidQueueStatus = "Business event queue has issues";
  public const string BPEventNotifications_SendingFailed = "Sending of business event notification has failed";
  public const string BPEventNotifications_ProcessingFailed = "Processing of a business event has failed";
  public const string BPEventNotifications_LongProcessingDetected = "Processing of a business event took longer than expected";
  public const string Commerce_DispatcherRestarted = "Commerce queue dispatcher has been restarted";
  public const string Commerce_DispatcherFailed = "Commerce queue dispatcher failed";
  public const string Commerce_InvalidQueueStatus = "Commerce queue has issues";
  public const string Commerce_ProcessingFailed = "Processing of a commerce queue message has failed";
  public const string Commerce_LongProcessingDetected = "Processing of a commerce queue message took longer than expected";
  public const string ResourceGovernor_RequestTerminated = "A request has been terminated";
  public const string ResourceGovernor_MemoryWorkingSetExceededThreshold = "Memory working set exceeded threshold";
  public const string ResourceGovernor_RequestDeclined = "A request has been declined";
  public const string ResourceGovernor_SchedulerProcessorDisabled = "Scheduler processor has been disabled";
  public const string ResourceGovernor_ApiRequestTerminated = "An API request has been terminated";
  public const string ResourceGovernor_ApiRequestDeclined = "An API request has been declined";
  public const string ResourceGovernor_SessionWasCleared = "The session was cleared due to high memory consumption";
  public const string System_ExpensiveTaskStarted = "Time-consuming task has started";
  public const string System_ExpensiveTaskFinished = "Time-consuming task has finished";
  public const string System_WorkflowFailedToInitialize = "The system has failed to initialize the workflow.";
  public const string System_WorkflowCouldNotAccessCache = "The system could not access the cache when searching for information about the fields used in the workflow.";
  public const string System_WorkflowWillFailToChangeStatusOfRecord = "The current workflow will fail to update the record status and other record fields due to an error in the workflow code.";
  public const string System_WorkflowWillFailToGetCondition = "The current workflow will fail to obtain the condition of the transition.";
  public const string System_WorkflowWillCannotDeterminateTargetState = "The {ActionName} action of the {Screen} form is used in multiple transitions that have different target states so the system cannot determinate the target state in advance, and, therefore, the list of combo box values for the {SchemaField} field. You need to specify the list of combo box values without using a target state or use the action only in transitions with the same target state.";
  public const string System_WorkflowActionCategoryIsNullMessage = "The {ActionName} action will be added to the Other category of the More menu because no category has been specified in the WithCategory method.";
  public const string System_WorkflowActionCategoryIsNull = "No category is specified for the action";
  public const string System_WorkflowPassedRecordDiffersFromUpdatedRecordInCache = "The record used in the workflow differs from the record updated in the cache.";
  public const string System_WorkflowPassedRecordDiffersFromLastPersistedRecord = "The latest saved record may be more actual than the record used in the workflow.";
  public const string System_CollectDacMetadataOnStartupTurnedOffInProduction = "The CollectDacMetadataOnStartup key is set to false. This key must be set to true because it is required by various Platform features.";
  public const string License_LicenseActivated = "A license has been activated";
  public const string License_LicenseDeleted = "A license has been deleted";
  public const string License_LoginLimitExceeded = "Sign-In limit exceeded";
  public const string License_ApiRequestThrottled = "An API request has been throttled";
  public const string ActiveDirectory_IntegrationFailed = "Failed to authorize Active Directory integration";
  public const string DataConsistency_IssueDetected = "A data corruption state has been detected";
  public const string System_IncidentReported = "Incident report has been sent";
  public const string Email_EmailsReceivedSuccessfully = "Email messages were successfully received";
  public const string Email_EmailsReceivedSuccessfullyDetails = "{EmailsCount} email messages were successfully received. For details, check the email processing log for the {EmailAccountDescription} email account.";
  public const string Email_EmailsReceivedWithErrors = "An error occurred during the receiving of email messages";
  public const string Email_EmailsReceivedWithErrorsDetails = "{ErrorsCount} errors occurred during the receiving of email messages. {SuccessesCount} email messages were received successfully. For details, check the email processing log for the {EmailAccountDescription} email account.";
  public const string Email_EmailsReceivedWithErrorsShortDetails = "An error occurred during the receiving of email messages for the {EmailAccountDescription} email account.";
}
