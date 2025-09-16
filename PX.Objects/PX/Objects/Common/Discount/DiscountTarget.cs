// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountTarget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Common.Discount;

/// <summary>Discount Targets</summary>
public static class DiscountTarget
{
  public const 
  #nullable disable
  string Customer = "CU";
  public const string CustomerAndInventory = "CI";
  public const string CustomerAndInventoryPrice = "CP";
  public const string CustomerPrice = "CE";
  public const string CustomerPriceAndInventory = "PI";
  public const string CustomerPriceAndInventoryPrice = "PP";
  public const string CustomerAndBranch = "CB";
  public const string CustomerPriceAndBranch = "PB";
  public const string Warehouse = "WH";
  public const string WarehouseAndInventory = "WI";
  public const string WarehouseAndCustomer = "WC";
  public const string WarehouseAndInventoryPrice = "WP";
  public const string WarehouseAndCustomerPrice = "WE";
  public const string Branch = "BR";
  public const string Vendor = "VE";
  public const string VendorAndInventory = "VI";
  public const string VendorAndInventoryPrice = "VP";
  public const string VendorLocation = "VL";
  public const string VendorLocationAndInventory = "LI";
  public const string Inventory = "IN";
  public const string InventoryPrice = "IE";
  public const string Unconditional = "UN";

  public class customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.customer>
  {
    public customer()
      : base("CU")
    {
    }
  }

  public class customerAndInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.customerAndInventory>
  {
    public customerAndInventory()
      : base("CI")
    {
    }
  }

  public class customerAndInventoryPrice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.customerAndInventoryPrice>
  {
    public customerAndInventoryPrice()
      : base("CP")
    {
    }
  }

  public class customerPrice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.customerPrice>
  {
    public customerPrice()
      : base("CE")
    {
    }
  }

  public class customerPriceAndInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.customerPriceAndInventory>
  {
    public customerPriceAndInventory()
      : base("PI")
    {
    }
  }

  public class customerPriceAndInventoryPrice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.customerPriceAndInventoryPrice>
  {
    public customerPriceAndInventoryPrice()
      : base("PP")
    {
    }
  }

  public class customerAndBranch : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.customerAndBranch>
  {
    public customerAndBranch()
      : base("CB")
    {
    }
  }

  public class customerPriceAndBranch : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.customerPriceAndBranch>
  {
    public customerPriceAndBranch()
      : base("PB")
    {
    }
  }

  public class warehouse : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.warehouse>
  {
    public warehouse()
      : base("WH")
    {
    }
  }

  public class warehouseAndInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.warehouseAndInventory>
  {
    public warehouseAndInventory()
      : base("WI")
    {
    }
  }

  public class warehouseAndCustomer : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.warehouseAndCustomer>
  {
    public warehouseAndCustomer()
      : base("WC")
    {
    }
  }

  public class warehouseAndInventoryPrice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.warehouseAndInventoryPrice>
  {
    public warehouseAndInventoryPrice()
      : base("WP")
    {
    }
  }

  public class warehouseAndCustomerPrice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.warehouseAndCustomerPrice>
  {
    public warehouseAndCustomerPrice()
      : base("WE")
    {
    }
  }

  public class branch : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.branch>
  {
    public branch()
      : base("BR")
    {
    }
  }

  public class vendor : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.vendor>
  {
    public vendor()
      : base("VE")
    {
    }
  }

  public class vendorAndInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.vendorAndInventory>
  {
    public vendorAndInventory()
      : base("VI")
    {
    }
  }

  public class vendorAndInventoryPrice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.vendorAndInventoryPrice>
  {
    public vendorAndInventoryPrice()
      : base("VP")
    {
    }
  }

  public class vendorLocation : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.vendorLocation>
  {
    public vendorLocation()
      : base("VL")
    {
    }
  }

  public class vendorLocationAndInventory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountTarget.vendorLocationAndInventory>
  {
    public vendorLocationAndInventory()
      : base("LI")
    {
    }
  }

  public class inventory : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.inventory>
  {
    public inventory()
      : base("IN")
    {
    }
  }

  public class inventoryPrice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.inventoryPrice>
  {
    public inventoryPrice()
      : base("IE")
    {
    }
  }

  public class unconditional : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountTarget.unconditional>
  {
    public unconditional()
      : base("UN")
    {
    }
  }
}
