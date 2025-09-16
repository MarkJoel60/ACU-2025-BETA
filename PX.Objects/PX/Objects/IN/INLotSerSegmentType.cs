// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerSegmentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INLotSerSegmentType
{
  public const string NumericVal = "N";
  public const string FixedConst = "C";
  public const string DateConst = "D";
  public const string DayConst = "U";
  public const string MonthConst = "M";
  public const string MonthLongConst = "A";
  public const string YearConst = "Y";
  public const string YearLongConst = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[8]
      {
        PXStringListAttribute.Pair("N", "Auto-Incremental Value"),
        PXStringListAttribute.Pair("C", "Constant"),
        PXStringListAttribute.Pair("U", "Day"),
        PXStringListAttribute.Pair("M", "Month"),
        PXStringListAttribute.Pair("A", "Month Long"),
        PXStringListAttribute.Pair("Y", "Year"),
        PXStringListAttribute.Pair("L", "Year Long"),
        PXStringListAttribute.Pair("D", "Custom Date Format")
      })
    {
    }
  }
}
