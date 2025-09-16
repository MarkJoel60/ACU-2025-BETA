// Decompiled with JetBrains decompiler
// Type: PX.Logging.Sinks.SystemEventsDbSink.SystemEventCategory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Logging.Sinks.SystemEventsDbSink;

public class SystemEventCategory
{
  public const string CategoryName_All = "All";
  public const string CategoryName_Customization = "Customization";
  public const string CategoryName_ResourceGovernor = "ResourceGovernor";
  public const string CategoryName_Scheduler = "Scheduler";
  public const string CategoryName_System = "System";
  public const string CategoryName_PushNotifications = "PushNotifications";
  public const string CategoryName_BPEventNotifications = "BusinessEvents";
  public const string CategoryName_License = "License";
  public const string CategoryName_Commerce = "Commerce";
  public const string CategoryName_ActiveDirectory = "ActiveDirectory";
  public const string CategoryName_DataConsistency = "DataConsistency";
  public const string CategoryName_Email = "Email";
  public const string Customization_CustomizationPublishedEventId = "Customization_CustomizationPublishedEventId";
  public const string Customization_UnpublishedEventId = "Customization_UnpublishedEventId";
  public const string Customization_FailedWithErrorEventId = "Customization_FailedWithErrorEventId";
  public const string Customization_ValidationCompletedEventId = "Customization_ValidationCompletedEventId";
  public const string Customization_ValidationFailedEventId = "Customization_ValidationFailedEventId";
  public const string Customization_PublishBeginEventId = "Customization_PublishBeginEventId";
  public const string Customization_PublishedExceptPluginsEventId = "Customization_PublishedExceptPluginsEventId";
  public const string Customization_PluginsExecutionCompletedEventId = "Customization_PluginsExecutionCompletedEventId";
  public const string ResourceGovernor_RequestTerminatedEventId = "ResourceGovernor_RequestTerminatedEventId";
  public const string ResourceGovernor_MemoryWorkingSetExceededThresholdEventId = "ResourceGovernor_MemoryWorkingSetExceededThresholdEventId";
  public const string ResourceGovernor_RequestDeclinedEventId = "ResourceGovernor_RequestDeclinedEventId";
  public const string ResourceGovernor_SchedulerProcessorDisabledEventId = "ResourceGovernor_SchedulerProcessorDisabledEventId";
  public const string ResourceGovernor_ApiRequestTerminatedEventId = "ResourceGovernor_ApiRequestTerminatedEventId";
  public const string ResourceGovernor_ApiRequestDeclinedEventId = "ResourceGovernor_ApiRequestDeclinedEventId";
  public const string ResourceGovernor_SessionWasClearedId = "ResourceGovernor_SessionWasClearedId";
  public const string Scheduler_SchedulerThrewExceptionEventId = "Scheduler_SchedulerThrewExceptionEventId";
  public const string Scheduler_SchedulerInitializedEventId = "Scheduler_SchedulerInitializedEventId";
  public const string Scheduler_AtLeastOneErrorProcessingFailedEventId = "Scheduler_AtLeastOneErrorProcessingFailedEventId";
  public const string Scheduler_SchedulerStoppedEventId = "Scheduler_SchedulerStoppedEventId";
  public const string Scheduler_SchedulerSkippedEventId = "Scheduler_SchedulerSkippedEventId";
  public const string Scheduler_SavingToHistoryFailedEventId = "Scheduler_SavingToHistoryFailedEventId";
  public const string System_StartEventId = "System_StartEventId";
  public const string System_SiteShutDownEventId = "System_SiteShutDownEventId";
  public const string System_RestartRequestedEventId = "System_RestartRequestedEventId";
  public const string System_EnableFtsRequestedEventId = "System_EnableFtsRequestedEventId";
  public const string System_RestartRequestInProgressEventId = "System_RestartRequestInProgressEventId";
  public const string System_IsvServiceFailedEventId = "System_IsvServiceFailedEventId";
  public const string System_SiteUpgradeStartedEventId = "System_SiteUpgradeStartedEventId";
  public const string System_DbUpgradeStartedEventId = "System_DbUpgradeStartedEventId";
  public const string System_SiteUpgradeAppliedEventId = "System_SiteUpgradeAppliedEventId";
  public const string System_DbUpgradeAppliedEventId = "System_DbUpgradeAppliedEventId";
  public const string System_SiteUpgradeFailedEventId = "System_SiteUpgradeFailedEventId";
  public const string System_DbUpgradeFailedEventId = "System_DbUpgradeFailedEventId";
  public const string System_TenantCreatedEventId = "System_TenantCreatedEventId";
  public const string System_TenantDeletionStartEventId = "System_TenantDeletionStartEventId";
  public const string System_TenantDeletionEndEventId = "System_TenantDeletionEndEventId";
  public const string System_SnapshotCreatedEventId = "System_SnapshotCreatedEventId";
  public const string System_SnapshotCreationFailedEventId = "System_SnapshotCreationFailedEventId";
  public const string System_CompanyCopyCreatedEventId = "System_CompanyCopyCreatedEventId";
  public const string System_CompanyCopyCreationFailedEventId = "System_CompanyCopyCreationFailedEventId";
  public const string System_SnapshotRestoredEventId = "System_SnapshotRestoredEventId";
  public const string System_SnapshotRestorationFailedEventId = "System_SnapshotRestorationFailedEventId";
  public const string System_SnapshotDeletionStartEventId = "System_SnapshotDeletionStartEventId";
  public const string System_SnapshotDeletionEndEventId = "System_SnapshotDeletionEndEventId";
  public const string System_OrphanedSnapshotDeletionStartEventId = "System_OrphanedSnapshotDeletionStartEventId";
  public const string System_OrphanedSnapshotDeletionEndEventId = "System_OrphanedSnapshotDeletionEndEventId";
  public const string System_ExpensiveTaskRunningEventId = "System_ExpensiveTaskRunningEventId";
  public const string System_ExpensiveTaskFinishedEventId = "System_ExpensiveTaskFinishedEventId";
  public const string System_WorkflowFailedToInitializeId = "System_WorkflowFailedToInitialize";
  public const string System_WorkflowCouldNotAccessCacheId = "System_WorkflowCouldNotAccessCache";
  public const string System_WorkflowWillNotUsePassedRecordCache = "System_WorkflowWillNotUsePassedRecordCache";
  public const string System_WorkflowWillNotUsePassedRecordPersisted = "System_WorkflowWillNotUsePassedRecordPersisted";
  public const string System_WorkflowWillProbablyFail = "System_WorkflowWillProbablyFail";
  public const string System_WorkflowActionCategoryIsNullEventId = "System_WorkflowActionCategoryIsNullEventId";
  public const string System_CollectDacMetadataOnStartupTurnedOffInProductionEventId = "System_CollectDacMetadataOnStartupTurnedOffInProductionEventId";
  public const string License_LicenseActivatedEventId = "License_LicenseActivatedEventId";
  public const string License_LicenseDeletedEventId = "License_LicenseDeletedEventId";
  public const string License_LoginLimitExceededEventId = "License_LoginLimitExceededEventId";
  public const string License_ApiRequestsThrottlingEventId = "License_ApiRequestsThrottlingEventId";
  public const string PushNotifications_DispatcherRestartedEventId = "PushNotifications_DispatcherRestartedEventId";
  public const string PushNotifications_DispatcherFailedEventId = "PushNotifications_DispatcherFailedEventId";
  public const string PushNotifications_InvalidQueueStatusEventId = "PushNotifications_InvalidQueueStatusEventId";
  public const string PushNotifications_SendingFailedEventId = "PushNotifications_SendingFailedEventId";
  public const string PushNotifications_ProcessingFailedEventId = "PushNotifications_ProcessingFailedEventId";
  public const string PushNotifications_LongProcessingDetectedEventId = "PushNotifications_LongProcessingDetectedEventId";
  public const string BPEventNotifications_DispatcherRestartedEventId = "BPEventNotifications_DispatcherRestartedEventId";
  public const string BPEventNotifications_DispatcherFailedEventId = "BPEventNotifications_DispatcherFailedEventId";
  public const string BPEventNotifications_InvalidQueueStatusEventId = "BPEventNotifications_InvalidQueueStatusEventId";
  public const string BPEventNotifications_SendingFailedEventId = "BPEventNotifications_SendingFailedEventId";
  public const string BPEventNotifications_ProcessingFailedEventId = "BPEventNotifications_ProcessingFailedEventId";
  public const string BPEventNotifications_LongProcessingDetectedEventId = "BPEventNotifications_LongProcessingDetectedEventId";
  public const string Commerce_DispatcherRestartedEventId = "BPEventNotifications_DispatcherRestartedEventId";
  public const string Commerce_DispatcherFailedEventId = "BPEventNotifications_DispatcherFailedEventId";
  public const string Commerce_InvalidQueueStatusEventId = "BPEventNotifications_InvalidQueueStatusEventId";
  public const string Commerce_ProcessingFailedEventId = "BPEventNotifications_ProcessingFailedEventId";
  public const string Commerce_LongProcessingDetectedEventId = "BPEventNotifications_LongProcessingDetectedEventId";
  public const string ActiveDirectory_IntegrationFailedEventId = "ActiveDirectory_IntegrationFailedEventId";
  public const string DataConsistency_IssueDetectedEventID = "DataConsistency_IssueDetectedEventID";
  public const string System_IncidentReportedEventId = "System_IncidentReportedEventId";
  public const string Email_EmailsReceivedSuccessfully = "Email_EmailsReceivedSuccessfully";
  public const string Email_EmailsReceivedWithErrors = "Email_EmailsReceivedWithErrors";
}
