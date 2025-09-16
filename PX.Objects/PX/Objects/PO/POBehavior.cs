// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POBehavior
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class POBehavior
{
  public const 
  #nullable disable
  string Standard = "S";
  public const string ChangeOrder = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("S", "Standard"),
        PXStringListAttribute.Pair("C", "Change Order")
      })
    {
    }
  }

  public class standard : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POBehavior.standard>
  {
    public standard()
      : base("S")
    {
    }
  }

  public class changeOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POBehavior.changeOrder>
  {
    public changeOrder()
      : base("C")
    {
    }
  }
}
