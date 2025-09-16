// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public static class POLineType
{
  public const 
  #nullable disable
  string GoodsForInventory = "GI";
  public const string GoodsForSalesOrder = "GS";
  public const string GoodsForServiceOrder = "GF";
  public const string GoodsForReplenishment = "GR";
  public const string GoodsForDropShip = "GP";
  public const string NonStockForDropShip = "NP";
  public const string NonStockForSalesOrder = "NO";
  public const string NonStockForServiceOrder = "NF";
  public const string NonStock = "NS";
  public const string Service = "SV";
  public const string Freight = "FT";
  public const string MiscCharges = "MC";
  public const string Description = "DN";
  public const string GoodsForProject = "PG";
  public const string NonStockForProject = "PN";
  public const string GoodsForManufacturing = "GM";
  public const string NonStockForManufacturing = "NM";

  public static bool IsStock(string lineType)
  {
    return SetOfConstants<string, Equal<IBqlOperand>, SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.goodsForInventory, POLineType.goodsForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.goodsForReplenishment, POLineType.goodsForManufacturing, POLineType.goodsForProject>>.Provider>.ContainsValue(lineType);
  }

  public static bool IsNonStock(string lineType)
  {
    return SetOfConstants<string, Equal<IBqlOperand>, SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.nonStock, POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.service, POLineType.nonStockForManufacturing, POLineType.nonStockForProject>>.Provider>.ContainsValue(lineType);
  }

  public static bool IsService(string lineType) => lineType == "SV";

  public static bool IsDropShip(string lineType)
  {
    return EnumerableExtensions.IsIn<string>(lineType, "GP", "NP");
  }

  public static bool IsProjectDropShip(string lineType) => lineType == "PG" || lineType == "PN";

  public static bool IsDefault(string lineType)
  {
    return lineType == "GI" || lineType == "NS" || lineType == "SV" || lineType == "FT" || lineType == "DN";
  }

  public static bool UsePOAccrual(string lineType)
  {
    if (POLineType.IsStock(lineType))
      return true;
    return POLineType.IsNonStock(lineType) && !POLineType.IsService(lineType);
  }

  public static bool IsStockNonDropShip(string lineType)
  {
    return POLineType.IsStock(lineType) && !POLineType.IsDropShip(lineType) && !POLineType.IsProjectDropShip(lineType);
  }

  public static bool IsNonStockNonServiceNonDropShip(string lineType)
  {
    return POLineType.IsNonStock(lineType) && !POLineType.IsService(lineType) && !POLineType.IsDropShip(lineType) && !POLineType.IsProjectDropShip(lineType);
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[14]
      {
        PXStringListAttribute.Pair("GI", "Goods for IN"),
        PXStringListAttribute.Pair("GS", "Goods for SO"),
        PXStringListAttribute.Pair("GF", "Goods for FS"),
        PXStringListAttribute.Pair("GR", "Goods for RP"),
        PXStringListAttribute.Pair("GP", "Goods for Drop-Ship"),
        PXStringListAttribute.Pair("NP", "Non-Stock for Drop-Ship"),
        PXStringListAttribute.Pair("NO", "Non-Stock for SO"),
        PXStringListAttribute.Pair("NF", "Non-Stock for FS"),
        PXStringListAttribute.Pair("PG", "Goods for Project"),
        PXStringListAttribute.Pair("PN", "Non-Stock for Project"),
        PXStringListAttribute.Pair("NS", "Non-Stock"),
        PXStringListAttribute.Pair("SV", "Service"),
        PXStringListAttribute.Pair("FT", "Freight"),
        PXStringListAttribute.Pair("DN", "Description")
      })
    {
    }
  }

  /// <summary>
  /// Selector. Provides a Default List of PO Line Types <br />
  /// i.e. GoodsForInventory, NonStock, Service
  /// </summary>
  public class DefaultListAttribute : PXStringListAttribute
  {
    public DefaultListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("GI", "Goods for IN"),
        PXStringListAttribute.Pair("NS", "Non-Stock"),
        PXStringListAttribute.Pair("SV", "Service")
      })
    {
    }
  }

  public class goodsForInventory : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.goodsForInventory>
  {
    public goodsForInventory()
      : base("GI")
    {
    }
  }

  public class goodsForSalesOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.goodsForSalesOrder>
  {
    public goodsForSalesOrder()
      : base("GS")
    {
    }
  }

  public class goodsForServiceOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.goodsForServiceOrder>
  {
    public goodsForServiceOrder()
      : base("GF")
    {
    }
  }

  public class goodsForReplenishment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.goodsForReplenishment>
  {
    public goodsForReplenishment()
      : base("GR")
    {
    }
  }

  public class goodsForDropShip : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.goodsForDropShip>
  {
    public goodsForDropShip()
      : base("GP")
    {
    }
  }

  public class nonStockForDropShip : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.nonStockForDropShip>
  {
    public nonStockForDropShip()
      : base("NP")
    {
    }
  }

  public class nonStockForSalesOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.nonStockForSalesOrder>
  {
    public nonStockForSalesOrder()
      : base("NO")
    {
    }
  }

  public class nonStockForServiceOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.nonStockForServiceOrder>
  {
    public nonStockForServiceOrder()
      : base("NF")
    {
    }
  }

  public class nonStock : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.nonStock>
  {
    public nonStock()
      : base("NS")
    {
    }
  }

  public class service : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.service>
  {
    public service()
      : base("SV")
    {
    }
  }

  public class freight : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.freight>
  {
    public freight()
      : base("FT")
    {
    }
  }

  public class miscCharges : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.miscCharges>
  {
    public miscCharges()
      : base("MC")
    {
    }
  }

  public class description : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.description>
  {
    public description()
      : base("DN")
    {
    }
  }

  public class goodsForProject : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLineType.goodsForProject>
  {
    public goodsForProject()
      : base("PG")
    {
    }
  }

  public class nonStockForProject : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.nonStockForProject>
  {
    public nonStockForProject()
      : base("PN")
    {
    }
  }

  public class goodsForManufacturing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.goodsForManufacturing>
  {
    public goodsForManufacturing()
      : base("GM")
    {
    }
  }

  public class nonStockForManufacturing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POLineType.nonStockForManufacturing>
  {
    public nonStockForManufacturing()
      : base("NM")
    {
    }
  }

  public class Goods : 
    SetOf.Strings.FilledWith<POLineType.goodsForInventory, POLineType.goodsForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.goodsForReplenishment, POLineType.goodsForManufacturing, POLineType.goodsForProject>
  {
  }

  public class NonStocks : 
    SetOf.Strings.FilledWith<POLineType.nonStock, POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.service, POLineType.nonStockForManufacturing, POLineType.nonStockForProject>
  {
  }
}
