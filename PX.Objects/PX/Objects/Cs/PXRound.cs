// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXRound
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CS;

public static class PXRound
{
  public static Decimal Math(Decimal value, int decimals)
  {
    return System.Math.Round(value, decimals, MidpointRounding.AwayFromZero);
  }

  public static Decimal Floor(Decimal value, int decimals)
  {
    int num1 = value < 0M ? 1 : 0;
    if (num1 != 0)
      value = -value;
    Decimal num2 = System.Math.Floor(value * (Decimal) System.Math.Pow(10.0, (double) decimals)) / (Decimal) System.Math.Pow(10.0, (double) decimals);
    if (num1 != 0)
      num2 = -num2;
    return num2;
  }

  public static Decimal Ceil(Decimal value, int decimals)
  {
    int num1 = value < 0M ? 1 : 0;
    if (num1 != 0)
      value = -value;
    Decimal num2 = System.Math.Ceiling(value * (Decimal) System.Math.Pow(10.0, (double) decimals)) / (Decimal) System.Math.Pow(10.0, (double) decimals);
    if (num1 != 0)
      num2 = -num2;
    return num2;
  }
}
