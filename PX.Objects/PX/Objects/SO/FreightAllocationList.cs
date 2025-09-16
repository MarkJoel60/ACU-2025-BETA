// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.FreightAllocationList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public static class FreightAllocationList
{
  public const 
  #nullable disable
  string FullAmount = "A";
  public const string Prorate = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("A", "Full Amount First Time"),
        PXStringListAttribute.Pair("P", "Allocate Proportionally")
      })
    {
    }
  }

  public class fullAmount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FreightAllocationList.fullAmount>
  {
    public fullAmount()
      : base("A")
    {
    }
  }

  public class prorate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FreightAllocationList.prorate>
  {
    public prorate()
      : base("P")
    {
    }
  }
}
