// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.InventoryRelationTag
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

public class InventoryRelationTag
{
  public const 
  #nullable disable
  string Complementary = "COMP";
  public const string Interested = "INTS";
  public const string UsersBought = "USER";
  public const string Accessory = "ESNT";
  public const string Service = "SERV";
  public const string Premium = "PREM";
  public const string Custom = "CUST";
  public const string Option = "OPTN";
  public const string Promotional = "PROM";
  public const string Popular = "POPL";
  public const string Seasonal = "SEAS";
  public const string Related = "RLTD";
  public const string Substitute = "SUBS";
  public const string Alternative = "ALTN";
  public const string All = "ALL_";

  [PXLocalizable]
  public static class Desc
  {
    public const string Complementary = "Complementary Items";
    public const string Interested = "Items of Interest";
    public const string UsersBought = "Other Users Bought";
    public const string Accessory = "Essential Related Products";
    public const string Service = "Services";
    public const string Premium = "Premium";
    public const string Custom = "Customization";
    public const string Option = "Options";
    public const string Promotional = "Promotional";
    public const string Popular = "Popular";
    public const string Seasonal = "Seasonal";
    public const string Related = "Related";
    public const string Substitute = "Substitute";
    public const string Alternative = "Alternative";
    public const string All = "All";
  }

  public class ListAttribute : PXStringListAttribute
  {
    private static (string Value, string Label)[] _values = new (string, string)[14]
    {
      ("COMP", "Complementary Items"),
      ("INTS", "Items of Interest"),
      ("USER", "Other Users Bought"),
      ("ESNT", "Essential Related Products"),
      ("SERV", "Services"),
      ("PREM", "Premium"),
      ("CUST", "Customization"),
      ("OPTN", "Options"),
      ("PROM", "Promotional"),
      ("POPL", "Popular"),
      ("SEAS", "Seasonal"),
      ("RLTD", "Related"),
      ("SUBS", "Substitute"),
      ("ALTN", "Alternative")
    };

    public ListAttribute()
      : base(InventoryRelationTag.ListAttribute._values)
    {
    }

    protected ListAttribute(
      params (string Value, string Label)[] additionalValues)
      : base(EnumerableExtensions.Append<(string, string)>(InventoryRelationTag.ListAttribute._values, additionalValues))
    {
    }

    public class WithAllAttribute : InventoryRelationTag.ListAttribute
    {
      public WithAllAttribute()
        : base(("ALL_", "All"))
      {
      }
    }
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  InventoryRelationTag.all>
  {
    public all()
      : base("ALL_")
    {
    }
  }
}
