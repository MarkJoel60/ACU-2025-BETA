// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INItemStatus
{
  public const 
  #nullable disable
  string Active = "AC";
  public const string Inactive = "IN";
  public const string NoSales = "NS";
  public const string NoPurchases = "NP";
  public const string ToDelete = "DE";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("AC", "Active"),
        PXStringListAttribute.Pair("NS", "No Sales"),
        PXStringListAttribute.Pair("NP", "No Purchases"),
        PXStringListAttribute.Pair("IN", "Inactive"),
        PXStringListAttribute.Pair("DE", "Marked for Deletion")
      })
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemStatus.active>
  {
    public active()
      : base("AC")
    {
    }
  }

  public class inactive : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemStatus.inactive>
  {
    public inactive()
      : base("IN")
    {
    }
  }

  public class noSales : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemStatus.noSales>
  {
    public noSales()
      : base("NS")
    {
    }
  }

  public class noPurchases : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemStatus.noPurchases>
  {
    public noPurchases()
      : base("NP")
    {
    }
  }

  public class toDelete : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INItemStatus.toDelete>
  {
    public toDelete()
      : base("DE")
    {
    }
  }
}
