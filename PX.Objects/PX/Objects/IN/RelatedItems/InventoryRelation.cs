// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.InventoryRelation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

public class InventoryRelation
{
  public const 
  #nullable disable
  string CrossSell = "CSELL";
  public const string UpSell = "USELL";
  public const string Substitute = "SUBST";
  public const string Other = "OTHER";
  public const string All = "ALL__";

  [Flags]
  public enum RelationType
  {
    None = 0,
    Substitute = 1,
    CrossSell = 2,
    Other = 4,
    UpSell = 8,
    CrossSellAndOther = Other | CrossSell, // 0x00000006
    All = CrossSellAndOther | UpSell | Substitute, // 0x0000000F
  }

  [PXLocalizable]
  public static class Desc
  {
    public const string CrossSell = "Cross-Sell";
    public const string UpSell = "Up-Sell";
    public const string Substitute = "Substitute";
    public const string Other = "Other";
    public const string All = "All";
  }

  public class ListAttribute : PXStringListAttribute
  {
    private static (string Value, string Label)[] _values = new (string, string)[4]
    {
      ("CSELL", "Cross-Sell"),
      ("USELL", "Up-Sell"),
      ("SUBST", "Substitute"),
      ("OTHER", "Other")
    };

    public ListAttribute()
      : base(InventoryRelation.ListAttribute._values)
    {
    }

    protected ListAttribute(
      params (string Value, string Label)[] additionalValues)
      : base(EnumerableExtensions.Append<(string, string)>(InventoryRelation.ListAttribute._values, additionalValues))
    {
    }

    public class WithAllAttribute : InventoryRelation.ListAttribute
    {
      public WithAllAttribute()
        : base(("ALL__", "All"))
      {
      }
    }
  }

  public class crossSell : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryRelation.crossSell>
  {
    public crossSell()
      : base("CSELL")
    {
    }
  }

  public class upSell : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryRelation.upSell>
  {
    public upSell()
      : base("USELL")
    {
    }
  }

  public class substitute : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryRelation.substitute>
  {
    public substitute()
      : base("SUBST")
    {
    }
  }

  public class other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryRelation.other>
  {
    public other()
      : base("OTHER")
    {
    }
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryRelation.all>
  {
    public all()
      : base("ALL__")
    {
    }
  }
}
