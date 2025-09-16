// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INReplenishmentSource
{
  public const 
  #nullable disable
  string None = "N";
  public const string Purchased = "P";
  public const string Manufactured = "M";
  public const string Transfer = "T";
  public const string DropShipToOrder = "D";
  public const string BlanketDropShipToOrder = "L";
  public const string PurchaseToOrder = "O";
  public const string BlanketPurchaseToOrder = "B";
  public const string TransferToPurchase = "X";
  public const string KitAssembly = "K";

  public static bool IsTransfer(string value)
  {
    return value == "T" || value == "O" || value == "D" || value == "P";
  }

  public class List : PXStringListAttribute
  {
    public List()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("N", "None"),
        PXStringListAttribute.Pair("P", "Purchase"),
        PXStringListAttribute.Pair("M", "Manufacturing"),
        PXStringListAttribute.Pair("T", "Transfer"),
        PXStringListAttribute.Pair("D", "Drop-Shipment"),
        PXStringListAttribute.Pair("O", "Purchase to Order"),
        PXStringListAttribute.Pair("K", "Kit Assembly")
      })
    {
    }
  }

  public class INPlanList : PXStringListAttribute
  {
    public INPlanList()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "None"),
        PXStringListAttribute.Pair("P", "Purchase"),
        PXStringListAttribute.Pair("T", "Transfer")
      })
    {
    }
  }

  [PXAttributeFamily(typeof (PXStringListAttribute))]
  public class SOList : PXStringListAttribute
  {
    public SOList()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("D", "Drop-Ship"),
        PXStringListAttribute.Pair("O", "Purchase to Order")
      })
    {
    }
  }

  [PXAttributeFamily(typeof (PXStringListAttribute))]
  public class SOListWithBlankets : PXStringListAttribute
  {
    public SOListWithBlankets()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("D", "Drop-Ship"),
        PXStringListAttribute.Pair("L", "Blanket for Drop-Ship"),
        PXStringListAttribute.Pair("O", "Purchase to Order"),
        PXStringListAttribute.Pair("B", "Blanket for Normal")
      })
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INReplenishmentSource.none>
  {
    public none()
      : base("N")
    {
    }
  }

  public class purchased : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INReplenishmentSource.purchased>
  {
    public purchased()
      : base("P")
    {
    }
  }

  public class transfer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INReplenishmentSource.transfer>
  {
    public transfer()
      : base("T")
    {
    }
  }

  public class manufactured : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INReplenishmentSource.manufactured>
  {
    public manufactured()
      : base("M")
    {
    }
  }

  public class dropShipToOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INReplenishmentSource.dropShipToOrder>
  {
    public dropShipToOrder()
      : base("D")
    {
    }
  }

  public class blanketDropShipToOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INReplenishmentSource.blanketDropShipToOrder>
  {
    public blanketDropShipToOrder()
      : base("L")
    {
    }
  }

  public class purchaseToOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INReplenishmentSource.purchaseToOrder>
  {
    public purchaseToOrder()
      : base("O")
    {
    }
  }

  public class blanketPurchaseToOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INReplenishmentSource.blanketPurchaseToOrder>
  {
    public blanketPurchaseToOrder()
      : base("B")
    {
    }
  }

  public class transferToPurchase : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INReplenishmentSource.transferToPurchase>
  {
    public transferToPurchase()
      : base("X")
    {
    }
  }
}
