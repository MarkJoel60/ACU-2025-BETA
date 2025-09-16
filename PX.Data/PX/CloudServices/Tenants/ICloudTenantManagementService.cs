// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.Tenants.ICloudTenantManagementService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Points.DbmsBase;
using PX.SM;
using System;

#nullable disable
namespace PX.CloudServices.Tenants;

[PXInternalUseOnly]
public interface ICloudTenantManagementService
{
  Action<int> OnApplySnapshot(
    PointDbmsBase point,
    int sourceCompanyID,
    UPSnapshot snapshot,
    UPCompany targetCompany);

  void OnCopyCompany(PointDbmsBase point, int targetCompanyID);
}
