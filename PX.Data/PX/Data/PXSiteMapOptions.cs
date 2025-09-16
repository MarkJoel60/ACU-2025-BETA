// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class PXSiteMapOptions
{
  internal const string ConfigurationPrefix = "sitemap";

  public bool SecurityTrimmingEnabled { get; set; } = true;

  /// <remarks>
  /// Defines the behavior of prioritized roles together with
  /// <see cref="P:PX.Data.PXAccessProvider.UserManagedAssignmentToPrioritizedRoles" />.
  /// </remarks>
  public bool ConstrainAdminAccess { get; set; }
}
