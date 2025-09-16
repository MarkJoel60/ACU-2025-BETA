// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.DAC.DeletionActionListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Maintenance.TenantShapshotDeletion.DAC;

internal class DeletionActionListAttribute : PXStringListAttribute
{
  private const string DeleteSnapshotLabel = "Delete Snapshot";
  private const string DeleteTenantLabel = "Delete Tenant";
  private const string DeleteOrphanedShapshotLabel = "Delete Orphaned Snapshot";
  internal const string DeleteSnapshotValue = "S";
  internal const string DeleteTenantValue = "T";
  internal const string DeleteOrphanedShapshotValue = "O";

  public DeletionActionListAttribute()
    : base(new string[3]{ "T", "S", "O" }, new string[3]
    {
      "Delete Tenant",
      "Delete Snapshot",
      "Delete Orphaned Snapshot"
    })
  {
  }
}
