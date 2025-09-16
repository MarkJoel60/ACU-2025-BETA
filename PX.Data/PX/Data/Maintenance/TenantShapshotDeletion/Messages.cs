// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.Messages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Maintenance.TenantShapshotDeletion;

[PXLocalizable]
public static class Messages
{
  public const string AcrossTenantsVisibility = "Across Tenants";
  public const string OrphanedVisibility = "Orphaned";
  public const string InvalidActionName = "The selected action is not supported on the Delete Snapshots and Tenants (SM503000) form.";
  public const string CurrentTenantCannotBeDeleted = "The tenant cannot be deleted because you are signed in to it.";
  public const string DeletionActionCacheName = "Deletion Action";
  public const string TenantSnapshotDeletionCacheName = "Tenant or Snapshot Deletion";
  public const string TenantTableDataDeletedTemplate = "Table tenant data has been deleted. Table name: {TableName}.";
  public const string SnapshotTableDataDeletedTemplate = "Table snapshot data has been deleted. Table name: {TableName}.";
  public const string AnotherDeletionProcessIsRunning = "A snapshot or tenant is being deleted at the moment. A new deletion process can be started only after the current process is finished.";
  public const string TenantCannotBeDeletedAnotherProcessIsRunning = "The tenant cannot be deleted because another snapshot or tenant is being deleted at the moment.";
  public const string SnapshotCannotBeDeletedAnotherProcessIsRunning = "The snapshot cannot be deleted because another snapshot or tenant is being deleted at the moment.";
}
