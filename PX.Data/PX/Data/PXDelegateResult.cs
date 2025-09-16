// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDelegateResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// The result of a data view delegate execution, which is truncated by a specific number of records, filtered, or sorted.
/// </summary>
public class PXDelegateResult : List<object>, IPXDelegateResult
{
  /// <summary>
  /// The Boolean value that indicates (if set to <see langword="true" />)
  /// that the result is sorted.
  /// </summary>
  public bool IsResultSorted { get; set; }

  /// <summary>
  /// The Boolean value that indicates (if set to <see langword="true" />)
  /// that the result is filtered.
  /// </summary>
  public bool IsResultFiltered { get; set; }

  /// <summary>
  /// The Boolean value that indicates (if set to <see langword="true" />)
  /// that the result is truncated by a specific number of records.
  /// </summary>
  public bool IsResultTruncated { get; set; }
}
