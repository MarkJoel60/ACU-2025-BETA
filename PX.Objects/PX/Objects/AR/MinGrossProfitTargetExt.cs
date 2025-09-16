// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.MinGrossProfitTargetExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR;

internal static class MinGrossProfitTargetExt
{
  public static string ToFixWarning(this MinGrossProfitValidator.Target target)
  {
    switch (target)
    {
      case MinGrossProfitValidator.Target.Discount:
        return "Minimum Gross Profit requirement is not satisfied. Discount was reduced to maximum valid value.";
      case MinGrossProfitValidator.Target.SalesPrice:
        return "Minimum Gross Profit requirement is not satisfied. Sales price was set to minimum valid value.";
      case MinGrossProfitValidator.Target.UnitPrice:
        return "Minimum Gross Profit requirement is not satisfied. Unit price was set to minimum valid value.";
      default:
        throw new ArgumentOutOfRangeException(nameof (target), (object) target, (string) null);
    }
  }
}
