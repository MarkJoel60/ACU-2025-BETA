// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.VisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.CS;

/// <summary>Visibility Restriction utilits</summary>
public static class VisibilityRestriction
{
  /// <summary>
  /// Default BAccountID, that means, that visibility restriction is not set.
  /// </summary>
  public const int EmptyBAccountID = 0;

  /// <summary>
  /// Checks, if BAccountID, that is set as 'Restrict Visibility To' for some entity is empty (or default).
  /// </summary>
  /// <param name="restrictVisibilityToBAccountID"></param>
  /// <returns></returns>
  public static bool IsEmpty(int? restrictVisibilityToBAccountID)
  {
    if (!restrictVisibilityToBAccountID.HasValue)
      return true;
    int? nullable = restrictVisibilityToBAccountID;
    int num = 0;
    return nullable.GetValueOrDefault() == num & nullable.HasValue;
  }

  /// <summary>
  /// Checks, if BAccountID, that is set as 'Restrict Visibility To' for some entity is not empty (or default).
  /// </summary>
  /// <param name="restrictVisibilityToBAccountID"></param>
  /// <returns></returns>
  public static bool IsNotEmpty(int? restrictVisibilityToBAccountID)
  {
    return !VisibilityRestriction.IsEmpty(restrictVisibilityToBAccountID);
  }
}
