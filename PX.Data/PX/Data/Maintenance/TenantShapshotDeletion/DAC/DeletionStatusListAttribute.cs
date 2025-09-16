// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.DAC.DeletionStatusListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Maintenance.TenantShapshotDeletion.DAC;

public class DeletionStatusListAttribute : PXStringListAttribute
{
  private const string NotPlannedLabel = "Not Planned";
  private const string PlannedLabel = "Planned";
  private const string StartedLabel = "Started";
  public const string NotPlannedValue = "N";
  public const string PlannedValue = "P";
  public const string StartedValue = "S";

  public DeletionStatusListAttribute()
    : base(new string[3]{ "N", "P", "S" }, new string[3]
    {
      "Not Planned",
      "Planned",
      "Started"
    })
  {
  }
}
