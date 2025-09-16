// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.Tenants.CloudTenantServiceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable enable
namespace PX.CloudServices.Tenants;

[PXInternalUseOnly]
public static class CloudTenantServiceExtensions
{
  [PXInternalUseOnly]
  public static string GetTenantIdString(this ICloudTenantService cloudTenantService)
  {
    return Conversions.CloudTenantIdToString(cloudTenantService.TenantId);
  }
}
