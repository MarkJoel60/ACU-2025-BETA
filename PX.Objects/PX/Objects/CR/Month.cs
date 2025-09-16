// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Month
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class Month
{
  public class ListAttribute : PXIntListAttribute
  {
    private static int _Jan = 1;
    private static int _Feb = 2;
    private static int _Mar = 3;
    private static int _Apr = 4;
    private static int _May = 5;
    private static int _Jun = 6;
    private static int _Jul = 7;
    private static int _Aug = 8;
    private static int _Sep = 9;
    private static int _Oct = 10;
    private static int _Nov = 11;
    private static int _Dec = 12;

    public ListAttribute()
      : base(new int[12]
      {
        Month.ListAttribute._Jan,
        Month.ListAttribute._Feb,
        Month.ListAttribute._Mar,
        Month.ListAttribute._Apr,
        Month.ListAttribute._May,
        Month.ListAttribute._Jun,
        Month.ListAttribute._Jul,
        Month.ListAttribute._Aug,
        Month.ListAttribute._Sep,
        Month.ListAttribute._Oct,
        Month.ListAttribute._Nov,
        Month.ListAttribute._Dec
      }, new string[12]
      {
        "January",
        "February",
        "March",
        "April",
        "May",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
      })
    {
    }
  }
}
