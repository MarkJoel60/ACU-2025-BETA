// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.IntercompanyGoodsInTransitFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// The DAC is used as a filter in Intercompany Goods in Transit Generic Inquiry
/// </summary>
[PXCacheName("Intercompany Goods in Transit Filter")]
public class IntercompanyGoodsInTransitFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [StockItem(DisplayName = "Inventory Item")]
  public virtual int? InventoryID { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Shipped Before")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? ShippedBefore { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show Only Overdue Items")]
  [PXDefault(false)]
  public virtual bool? ShowOverdueItems { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show Only Items Without Receipt")]
  [PXDefault(false)]
  public virtual bool? ShowItemsWithoutReceipt { get; set; }

  [VendorActive]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "This vendor does not belong to your organization. Select another vendor.", new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD)})]
  public virtual int? SellingCompany { get; set; }

  [Site(DisplayName = "Selling Warehouse")]
  public virtual int? SellingSiteID { get; set; }

  [CustomerActive]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.isBranch, Equal<True>>), "This customer does not belong to your organization. Select another customer.", new System.Type[] {typeof (Customer.acctCD)})]
  public virtual int? PurchasingCompany { get; set; }

  [Site(DisplayName = "Purchasing Warehouse")]
  public virtual int? PurchasingSiteID { get; set; }

  [OrganizationTree(null, true, FieldClass = "MultipleBaseCurrencies", Required = true)]
  public int? OrgBAccountID { get; set; }

  public abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.inventoryID>
  {
  }

  public abstract class shippedBefore : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.shippedBefore>
  {
  }

  public abstract class showOverdueItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.showOverdueItems>
  {
  }

  public abstract class showItemsWithoutReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.showItemsWithoutReceipt>
  {
  }

  public abstract class sellingCompany : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.sellingCompany>
  {
  }

  public abstract class sellingSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.sellingSiteID>
  {
  }

  public abstract class purchasingCompany : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.purchasingCompany>
  {
  }

  public abstract class purchasingSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    IntercompanyGoodsInTransitFilter.purchasingSiteID>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}
