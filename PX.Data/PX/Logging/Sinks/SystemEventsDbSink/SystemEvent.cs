// Decompiled with JetBrains decompiler
// Type: PX.Logging.Sinks.SystemEventsDbSink.SystemEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Logging.Sinks.SystemEventsDbSink;

[PXHidden]
public class SystemEvent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXDBDefault]
  [PXUIField(DisplayName = "ID", Visible = false)]
  public virtual int? Id { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Level")]
  [SystemEventLevelList]
  public virtual int? Level { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Tenant")]
  public virtual 
  #nullable disable
  string TenantName { get; set; }

  [PXDBString(64 /*0x40*/)]
  [PXUIField(DisplayName = "Source")]
  [PXStringList(new string[] {"All", "Customization", "ResourceGovernor", "License", "PushNotifications", "BusinessEvents", "Scheduler", "System", "ActiveDirectory", "DataConsistency"}, new string[] {"All", "Customization", "ResourceGovernor", "License", "PushNotifications", "BusinessEvents", "Scheduler", "System", "ActiveDirectory", "DataConsistency"})]
  public virtual string Source { get; set; }

  [PXDBString(1024 /*0x0400*/)]
  [PXUIField(DisplayName = "Event")]
  [PXStringList(new string[] {"Customization_CustomizationPublishedEventId", "Customization_UnpublishedEventId", "Customization_FailedWithErrorEventId", "Customization_ValidationCompletedEventId", "Customization_ValidationFailedEventId", "Customization_PublishBeginEventId", "Customization_PublishedExceptPluginsEventId", "Customization_PluginsExecutionCompletedEventId", "System_IsvServiceFailedEventId", "ResourceGovernor_RequestTerminatedEventId", "ResourceGovernor_MemoryWorkingSetExceededThresholdEventId", "ResourceGovernor_RequestDeclinedEventId", "ResourceGovernor_SchedulerProcessorDisabledEventId", "ResourceGovernor_ApiRequestTerminatedEventId", "ResourceGovernor_ApiRequestDeclinedEventId", "ResourceGovernor_SessionWasClearedId", "Scheduler_SchedulerThrewExceptionEventId", "Scheduler_AtLeastOneErrorProcessingFailedEventId", "Scheduler_SchedulerInitializedEventId", "Scheduler_SchedulerStoppedEventId", "Scheduler_SchedulerSkippedEventId", "Scheduler_SavingToHistoryFailedEventId", "System_SiteShutDownEventId", "System_RestartRequestedEventId", "System_RestartRequestInProgressEventId", "System_StartEventId", "System_SiteUpgradeStartedEventId", "System_DbUpgradeStartedEventId", "System_SiteUpgradeAppliedEventId", "System_DbUpgradeAppliedEventId", "System_SiteUpgradeFailedEventId", "System_DbUpgradeFailedEventId", "System_TenantCreatedEventId", "System_TenantDeletionStartEventId", "System_TenantDeletionEndEventId", "System_SnapshotCreatedEventId", "System_SnapshotCreationFailedEventId", "System_CompanyCopyCreatedEventId", "System_CompanyCopyCreationFailedEventId", "System_SnapshotRestoredEventId", "System_SnapshotRestorationFailedEventId", "System_SnapshotDeletionStartEventId", "System_SnapshotDeletionEndEventId", "System_OrphanedSnapshotDeletionStartEventId", "System_OrphanedSnapshotDeletionEndEventId", "System_ExpensiveTaskRunningEventId", "System_ExpensiveTaskFinishedEventId", "System_WorkflowFailedToInitialize", "System_WorkflowCouldNotAccessCache", "System_WorkflowWillNotUsePassedRecordCache", "System_WorkflowWillNotUsePassedRecordPersisted", "System_WorkflowWillProbablyFail", "System_WorkflowActionCategoryIsNullEventId", "System_CollectDacMetadataOnStartupTurnedOffInProductionEventId", "PushNotifications_DispatcherRestartedEventId", "PushNotifications_DispatcherFailedEventId", "PushNotifications_InvalidQueueStatusEventId", "PushNotifications_SendingFailedEventId", "PushNotifications_ProcessingFailedEventId", "PushNotifications_LongProcessingDetectedEventId", "BPEventNotifications_DispatcherRestartedEventId", "BPEventNotifications_DispatcherFailedEventId", "BPEventNotifications_InvalidQueueStatusEventId", "BPEventNotifications_SendingFailedEventId", "BPEventNotifications_ProcessingFailedEventId", "BPEventNotifications_LongProcessingDetectedEventId", "License_LicenseActivatedEventId", "License_LicenseDeletedEventId", "License_LoginLimitExceededEventId", "License_ApiRequestsThrottlingEventId", "ActiveDirectory_IntegrationFailedEventId", "DataConsistency_IssueDetectedEventID", "System_IncidentReportedEventId", "Email_EmailsReceivedSuccessfully", "Email_EmailsReceivedWithErrors"}, new string[] {"A customization project has been published", "A customization project has been unpublished", "Publishing of a customization project failed with an error", "Validation of a customization project has been completed", "Validation failed", "Publishing via the customization API has started", "A customization project has been published (except plug-ins)", "Execution of a customization project plug-ins has been completed", "Failed to invoke the ISV service.", "A request has been terminated", "Memory working set exceeded threshold", "A request has been declined", "Scheduler processor has been disabled", "An API request has been terminated", "An API request has been declined", "The session was cleared due to high memory consumption", "Automation Scheduler has failed with an exception", "At least one record has not been processed successfully", "Automation Scheduler has been initialized", "Automation Scheduler has been aborted", "Automation Schedule scenario has not been performed due to the database lockout", "The processed {ItemType} item does not contain NoteID, and the processing information cannot be saved to the schedule history.", "The site is shutting down", "A request to restart the application has been made", "The Application Restart Observer is restarting the application", "The site has started", "Site upgrade has started", "Database upgrade has started", "Site upgrade has been applied successfully", "Database upgrade has been applied successfully", "Site upgrade failed with exception", "Database upgrade completed with errors", "A new tenant has been created", "Tenant deletion has been started", "Tenant deletion has been completed", "A snapshot has been created", "Failed to create a snapshot", "A tenant copy has been created", "Creating of a tenant copy has failed", "A snapshot has been successfully restored", "Snapshot restoration failed", "Snapshot deletion has been started", "Snapshot deletion has been completed", "Orphaned snapshot deletion has been started", "Orphaned snapshot deletion has been completed", "Time-consuming task has started", "Time-consuming task has finished", "The system has failed to initialize the workflow.", "The system could not access the cache when searching for information about the fields used in the workflow.", "The record used in the workflow differs from the record updated in the cache.", "The latest saved record may be more actual than the record used in the workflow.", "The current workflow will fail to update the record status and other record fields due to an error in the workflow code.", "No category is specified for the action", "The CollectDacMetadataOnStartup key is set to false. This key must be set to true because it is required by various Platform features.", "Push notification queue dispatcher has been restarted", "Push notification queue dispatcher failed", "Push notification queue has issues", "Sending of push notification has failed", "Processing of a push notification has failed", "Processing of a push notification took longer than expected", "Business event queue dispatcher has been restarted", "Business event queue dispatcher failed", "Business event queue has issues", "Sending of business event notification has failed", "Processing of a business event has failed", "Processing of a business event took longer than expected", "A license has been activated", "A license has been deleted", "Sign-In limit exceeded", "An API request has been throttled", "Failed to authorize Active Directory integration", "A data corruption state has been detected", "Incident report has been sent", "Email messages were successfully received", "An error occurred during the receiving of email messages"})]
  public virtual string EventID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "")]
  public virtual string FormattedProperties { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Screen ID")]
  public virtual string ScreenId { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Link to Screen")]
  public virtual string LinkToEntity { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false, InputMask = "G", DisplayMask = "G")]
  [PXUIField(DisplayName = "Date and Time (local)")]
  public virtual System.DateTime? Date { get; set; }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "User")]
  public virtual string User { get; set; }

  [PXDBString(-1, IsUnicode = true)]
  [PXUIField(DisplayName = "Message")]
  public virtual string Details { get; set; }

  [PXDBString(-1, IsUnicode = true)]
  public virtual string Properties { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SystemEvent.id>
  {
  }

  public abstract class level : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SystemEvent.level>
  {
  }

  public abstract class tenantName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.tenantName>
  {
  }

  public abstract class source : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.source>
  {
  }

  public abstract class eventId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.eventId>
  {
  }

  public abstract class formattedProperties : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SystemEvent.formattedProperties>
  {
  }

  public abstract class screenId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.screenId>
  {
  }

  public abstract class linkToEntity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.linkToEntity>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SystemEvent.date>
  {
  }

  public abstract class user : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.user>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.details>
  {
  }

  public abstract class properties : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SystemEvent.properties>
  {
  }
}
