// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Interfaces.IAppInstanceInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Services.Interfaces;

/// <summary>
/// Contains the information about the current application instance.
/// </summary>
[PXInternalUseOnly]
public interface IAppInstanceInfo
{
  /// <summary>
  /// Returns the version of the current application instance (not including the published customizations).
  /// </summary>
  string Version { get; }

  /// <summary>
  /// Returns the Installation ID that is unique for each application instance.
  /// </summary>
  string InstallationId { get; }

  /// <summary>
  /// Returns <see langword="true" /> if the current application instance is Self-Service Portal;
  /// <see langword="false" /> otherwise.
  /// </summary>
  bool IsPortal { get; }

  /// <summary>
  /// Returns <see langword="true" /> if the current application instance is a part of a cluster configuration;
  /// <see langword="false" /> otherwise.
  /// </summary>
  bool IsCluster { get; }
}
