// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFontSizeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXFontSizeListAttribute : PXIntListAttribute
{
  private static readonly int[] _values;
  private static readonly string[] _labels;

  static PXFontSizeListAttribute()
  {
    int[] defaultSizes = FontFamilyEx.DefaultSizes;
    PXFontSizeListAttribute._values = new int[defaultSizes.Length];
    PXFontSizeListAttribute._labels = new string[defaultSizes.Length];
    int index = 0;
    foreach (int num in defaultSizes)
    {
      PXFontSizeListAttribute._values[index] = num;
      PXFontSizeListAttribute._labels[index] = num.ToString();
      ++index;
    }
  }

  public PXFontSizeListAttribute()
    : base(PXFontSizeListAttribute._values, PXFontSizeListAttribute._labels)
  {
  }
}
