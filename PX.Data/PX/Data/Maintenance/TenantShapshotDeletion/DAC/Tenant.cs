// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.TenantShapshotDeletion.DAC.Tenant
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Maintenance.TenantShapshotDeletion.DAC;

public sealed class Tenant : PXCacheExtension<
#nullable disable
TenantSnapshotDeletion>
{
  [PXUIField(DisplayName = "Tenant Name")]
  [PXString(IsUnicode = true)]
  public string TenantName { get; set; }

  [PXUIField(DisplayName = "Status")]
  [PXString(IsUnicode = true)]
  public string Status { get; set; }

  public static bool IsActive() => true;

  public abstract class tenantName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tenant.tenantName>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Tenant.status>
  {
  }
}
