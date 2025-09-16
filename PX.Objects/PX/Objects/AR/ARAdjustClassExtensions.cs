// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAdjustClassExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR;

public static class ARAdjustClassExtensions
{
  /// <summary>
  /// Returnes <c>true</c> if the same document is entered as an adjusting and adjusted document.
  /// </summary>
  public static bool IsSelfAdjustment(this ARAdjust adj)
  {
    return adj.AdjdDocType == adj.AdjgDocType && adj.AdjdRefNbr == adj.AdjgRefNbr;
  }

  /// <summary>
  /// Returnes <c>true</c> if this is an original application for the CreditWO document.
  /// </summary>
  public static bool IsOrigSmallCreditWOApp(this ARAdjust adj)
  {
    return adj.AdjdDocType == "SMC" && adj.AdjNbr.GetValueOrDefault() == -1 && !adj.VoidAdjNbr.HasValue;
  }
}
