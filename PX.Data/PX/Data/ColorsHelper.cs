// Decompiled with JetBrains decompiler
// Type: PX.Data.ColorsHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace PX.Data;

internal static class ColorsHelper
{
  public static string GetHexColor(string colorName)
  {
    return string.IsNullOrEmpty(colorName) || ColorsHelper.IsRgbHexCode(colorName) ? colorName : $"#{Color.FromName(colorName).ToArgb() & 16777215 /*0xFFFFFF*/:x6}";
  }

  public static string CreateColor(string colorName)
  {
    if (ColorsHelper.IsRgbHexCode(colorName))
      return ColorsHelper.GetArgbFromRgbHexCode(colorName);
    int result;
    return int.TryParse(colorName, NumberStyles.HexNumber, (IFormatProvider) CultureInfo.CurrentCulture, out result) ? ColorsHelper.FindColor(result) : Color.FromName(colorName).ToArgb().ToString("X").PadLeft(6, '0').PadLeft(8, 'F');
  }

  private static bool IsRgbHexCode(string color) => color.StartsWith("#") && color.Length == 7;

  private static string GetArgbFromRgbHexCode(string rgbHex) => rgbHex.Replace("#", "FF");

  private static string FindColor(int argb)
  {
    string[] colorNames = PX.Common.Drawing.GetColorNames();
    Color color1 = Color.FromArgb(argb);
    string color2 = (string) null;
    int num1 = int.MaxValue;
    foreach (string name in colorNames)
    {
      Color color3 = Color.FromName(name);
      int num2 = System.Math.Abs((int) color3.R - (int) color1.R) + System.Math.Abs((int) color3.G - (int) color1.G) + System.Math.Abs((int) color3.B - (int) color1.B);
      if (num2 == 0)
        return name;
      if (num2 < num1)
      {
        num1 = num2;
        color2 = name;
      }
    }
    return color2;
  }
}
