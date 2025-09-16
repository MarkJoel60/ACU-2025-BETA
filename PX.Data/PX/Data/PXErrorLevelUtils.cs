// Decompiled with JetBrains decompiler
// Type: PX.Data.PXErrorLevelUtils
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// An utilities class for <see cref="T:PX.Data.PXErrorLevel" /> enum.
/// </summary>
public static class PXErrorLevelUtils
{
  /// <summary>
  /// Does <paramref name="errorLevel" /> indicate error.
  /// </summary>
  /// <param name="errorLevel">The errorLevel to act on.</param>
  /// <returns>True if error, false if not.</returns>
  public static bool IsError(this PXErrorLevel errorLevel)
  {
    return errorLevel == PXErrorLevel.Error || errorLevel == PXErrorLevel.RowError;
  }

  /// <summary>
  /// Does <paramref name="errorLevel" /> indicate warning.
  /// </summary>
  /// <param name="errorLevel">The errorLevel to act on.</param>
  /// <returns>True if warning, false if not.</returns>
  public static bool IsWarning(this PXErrorLevel errorLevel)
  {
    return errorLevel == PXErrorLevel.Warning || errorLevel == PXErrorLevel.RowWarning;
  }

  /// <summary>
  /// Change error level for rows like <see cref="F:PX.Data.PXErrorLevel.RowError" />, <see cref="F:PX.Data.PXErrorLevel.RowWarning" />, <see cref="F:PX.Data.PXErrorLevel.RowInfo" />
  /// into the corresponding "non-row" error levels.
  /// </summary>
  /// <param name="errorLevel">The errorLevel to act on.</param>
  /// <param name="rowInfoErrorLevel">
  /// (Optional) The level to use for row information error level. If not specified then the <see cref="F:PX.Data.PXErrorLevel.Undefined" /> will be used.
  /// </param>
  /// <returns>
  /// A non-row <see cref="T:PX.Data.PXErrorLevel" />.
  /// </returns>
  public static PXErrorLevel ChangeIntoNonRowErrorLevel(
    this PXErrorLevel errorLevel,
    PXErrorLevel? rowInfoErrorLevel = null)
  {
    switch (errorLevel)
    {
      case PXErrorLevel.RowInfo:
        return rowInfoErrorLevel.GetValueOrDefault();
      case PXErrorLevel.RowWarning:
        return PXErrorLevel.Warning;
      case PXErrorLevel.RowError:
        return PXErrorLevel.Error;
      default:
        return errorLevel;
    }
  }

  /// <summary>
  /// Merge two error levels to return the result with the highest error level.
  /// If an error level for row and a non-row error level are merged then info regarding row is lost.
  /// For example, the merge of RowError and Error will return Error.
  /// </summary>
  /// <param name="originalErrorLevel">The original <see cref="T:PX.Data.PXErrorLevel" /> to act on.</param>
  /// <param name="errorLevelToMerge">The error level to merge.</param>
  /// <returns>
  /// A merged <see cref="T:PX.Data.PXErrorLevel" />.
  /// </returns>
  public static PXErrorLevel MergeWith(
    this PXErrorLevel originalErrorLevel,
    PXErrorLevel errorLevelToMerge)
  {
    if (originalErrorLevel == errorLevelToMerge || originalErrorLevel == PXErrorLevel.Error)
      return originalErrorLevel;
    PXErrorLevel pxErrorLevel1 = originalErrorLevel.ChangeIntoNonRowErrorLevel();
    bool flag1 = errorLevelToMerge.IsErrorLevelForRow();
    PXErrorLevel pxErrorLevel2 = errorLevelToMerge.ChangeIntoNonRowErrorLevel();
    bool flag2 = errorLevelToMerge.IsErrorLevelForRow();
    if (pxErrorLevel1 > pxErrorLevel2)
      return flag1 != flag2 ? pxErrorLevel1 : originalErrorLevel;
    if (pxErrorLevel1 >= pxErrorLevel2)
      return pxErrorLevel1;
    return flag1 != flag2 ? pxErrorLevel2 : errorLevelToMerge;
  }

  /// <summary>
  /// Check if <paramref name="errorLevel" /> is error level for rows.
  /// </summary>
  /// <param name="errorLevel">The errorLevel to act on.</param>
  /// <returns>
  /// True if error level is a row error level like <see cref="F:PX.Data.PXErrorLevel.RowError" />, false if not.
  /// </returns>
  public static bool IsErrorLevelForRow(this PXErrorLevel errorLevel)
  {
    switch (errorLevel)
    {
      case PXErrorLevel.RowInfo:
      case PXErrorLevel.RowWarning:
      case PXErrorLevel.RowError:
        return true;
      default:
        return false;
    }
  }
}
