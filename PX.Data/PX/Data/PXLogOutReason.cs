// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLogOutReason
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public enum PXLogOutReason
{
  Manual,
  UsersExceeded,
  ConcurrentLoginsExceeded,
  CompaniesExceeded,
  SnapshotRestored,
  /// <summary>When current user has been disabled.</summary>
  UserDisabled,
  /// <summary>When access has been disabled.</summary>
  AccessDisabled,
}
