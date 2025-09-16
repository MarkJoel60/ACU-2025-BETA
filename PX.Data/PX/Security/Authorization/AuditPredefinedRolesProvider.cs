// Decompiled with JetBrains decompiler
// Type: PX.Security.Authorization.AuditPredefinedRolesProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Data;

#nullable disable
namespace PX.Security.Authorization;

internal class AuditPredefinedRolesProvider(IOptions<AuditOptions> options) : 
  PredefinedRolesProviderBase(options.Value.AuditorRole)
{
}
