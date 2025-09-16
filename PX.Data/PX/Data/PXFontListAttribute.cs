// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFontListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXFontListAttribute : PXStringListAttribute
{
  private static readonly string[] _values;
  private static readonly string[] _labels;

  static PXFontListAttribute()
  {
    string[] fontNames = FontFamilyEx.GetFontNames();
    PXFontListAttribute._values = new string[fontNames.Length];
    PXFontListAttribute._labels = new string[fontNames.Length];
    int index = 0;
    foreach (string str in fontNames)
    {
      PXFontListAttribute._values[index] = str;
      PXFontListAttribute._labels[index] = str;
      ++index;
    }
  }

  public PXFontListAttribute()
    : base(PXFontListAttribute._values, PXFontListAttribute._labels)
  {
  }
}
