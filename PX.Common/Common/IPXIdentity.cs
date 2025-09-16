// Decompiled with JetBrains decompiler
// Type: PX.Common.IPXIdentity
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Globalization;
using System.Security.Principal;

#nullable disable
namespace PX.Common;

/// <summary>
/// Contains the context information for the current user identity.
/// </summary>
[PXInternalUseOnly]
public interface IPXIdentity
{
  /// <summary>
  ///   <para>Returns the username for the current user.</para>
  ///   <para>Please note that the tenant ID is not returned as a part of the username;
  ///       it is returned by <see cref="P:PX.Common.IPXIdentity.TenantId" /> property.</para>
  /// </summary>
  string Username { get; }

  /// <summary>
  /// Returns Tenant ID (login name) for the current tenant,
  /// or <see langword="null" /> if there is only a single tenant in the system.
  /// </summary>
  string TenantId { get; }

  /// <summary>
  /// Returns the ID of the current branch, or <see langword="null" />
  /// if Multi-Branch Support feature is turned off or there are no branches in the system.
  /// </summary>
  int? BranchId { get; }

  /// <summary>
  /// Returns the current culture. If it is not defined, invariant culture should be returned.
  /// </summary>
  CultureInfo Culture { get; }

  /// <summary>
  /// Returns the current business date. If it is not defined, the current date should be returned.
  /// </summary>
  DateTime BusinessDate { get; }

  /// <summary>
  /// Returns the current time zone. If it is not defined, invariant timezone should be returned.
  /// </summary>
  PXTimeZoneInfo TimeZone { get; }

  /// <summary>Returns the current user.</summary>
  IPrincipal User { get; }
}
