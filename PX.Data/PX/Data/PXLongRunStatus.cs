// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLongRunStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
///   <para>This enumeration specifies the status type of a long-running operation.</para>
/// </summary>
/// <remarks>
///   <para>The <see cref="M:PX.Data.PXLongOperation.GetStatus(System.Object)">GetStatus</see> methods return a value of this type.</para>
/// </remarks>
public enum PXLongRunStatus
{
  /// <summary>
  /// The long-running operation does not exist on the server.
  /// </summary>
  NotExists,
  /// <summary>The long-running operation has not yet completed.</summary>
  InProcess,
  /// <summary>The long-running operation has completed.</summary>
  Completed,
  /// <summary>The long-running operation has been aborted.</summary>
  Aborted,
}
