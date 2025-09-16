// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOAddItemMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOAddItemMode
{
  public const int BySite = 0;
  public const int ByCustomer = 1;

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new Tuple<int, string>[2]
      {
        PXIntListAttribute.Pair(0, "All Items"),
        PXIntListAttribute.Pair(1, "Sold Since")
      })
    {
    }
  }

  public class bySite : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  SOAddItemMode.bySite>
  {
    public bySite()
      : base(0)
    {
    }
  }

  public class byCustomer : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  SOAddItemMode.byCustomer>
  {
    public byCustomer()
      : base(1)
    {
    }
  }
}
