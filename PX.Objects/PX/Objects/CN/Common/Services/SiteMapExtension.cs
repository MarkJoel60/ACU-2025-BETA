// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.SiteMapExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CN.Common.Services;

public static class SiteMapExtension
{
  public static bool IsTaxBillsAndAdjustmentsScreenId()
  {
    return PXContext.GetScreenID() == "TX.30.30.00" || PXContext.GetScreenID() == "TX.30.30.PL";
  }

  public static bool IsInvoicesScreenId()
  {
    return PXContext.GetScreenID() == "SO.30.30.00" || PXContext.GetScreenID() == "SO.30.30.PL";
  }

  public static bool IsChecksAndPaymentsScreenId() => PXContext.GetScreenID() == "AP.30.20.00";

  public static bool IsPreparePaymentsScreenId() => PXContext.GetScreenID() == "AP.50.30.00";

  public static string WithoutSeparator(this string screenId)
  {
    return screenId.Replace(".", string.Empty);
  }

  public static bool IsReleasePaymentsScreenId() => PXContext.GetScreenID() == "AP.50.52.00";
}
