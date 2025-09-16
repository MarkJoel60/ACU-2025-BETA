// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class InventoryItemStatus
{
  public const 
  #nullable disable
  string Active = "AC";
  public const string NoSales = "NS";
  public const string NoPurchases = "NP";
  public const string NoRequest = "NR";
  public const string Inactive = "IN";
  public const string MarkedForDeletion = "DE";
  public const string Unknown = "XX";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("AC", "Active"),
        PXStringListAttribute.Pair("NS", "No Sales"),
        PXStringListAttribute.Pair("NP", "No Purchases"),
        PXStringListAttribute.Pair("NR", "No Request"),
        PXStringListAttribute.Pair("IN", "Inactive"),
        PXStringListAttribute.Pair("DE", "Marked for Deletion")
      })
    {
    }
  }

  public class SubItemListAttribute : PXStringListAttribute
  {
    public SubItemListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("AC", "Active"),
        PXStringListAttribute.Pair("NS", "No Sales"),
        PXStringListAttribute.Pair("NP", "No Purchases"),
        PXStringListAttribute.Pair("NR", "No Request"),
        PXStringListAttribute.Pair("IN", "Inactive")
      })
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryItemStatus.active>
  {
    public active()
      : base("AC")
    {
    }
  }

  public class noSales : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryItemStatus.noSales>
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
  InventoryItemStatus.noPurchases>
  {
    public noPurchases()
      : base("NP")
    {
    }
  }

  public class noRequest : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryItemStatus.noRequest>
  {
    public noRequest()
      : base("NR")
    {
    }
  }

  public class inactive : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryItemStatus.inactive>
  {
    public inactive()
      : base("IN")
    {
    }
  }

  public class markedForDeletion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InventoryItemStatus.markedForDeletion>
  {
    public markedForDeletion()
      : base("DE")
    {
    }
  }

  public class unknown : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryItemStatus.unknown>
  {
    public unknown()
      : base("XX")
    {
    }
  }
}
