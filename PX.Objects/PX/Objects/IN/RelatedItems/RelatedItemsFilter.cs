// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.RelatedItemsFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

/// <exclude />
[PXCacheName("Related Items Filter")]
public class RelatedItemsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  public virtual int? LineNbr { get; set; }

  [Inventory(Enabled = false)]
  public virtual int? InventoryID { get; set; }

  [PXInt]
  public virtual int? SubItemID { get; set; }

  [PXDate]
  public virtual DateTime? DocumentDate { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [PXPriceCost]
  [PXUIField(DisplayName = "Unit Price", Enabled = false)]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXCury(typeof (RelatedItemsFilter.curyID))]
  public virtual Decimal? OriginalCuryExtPrice { get; set; }

  [PXCury(typeof (RelatedItemsFilter.curyID))]
  [PXFormula(typeof (BqlOperand<RelatedItemsFilter.qty, IBqlDecimal>.Multiply<BqlOperand<RelatedItemsFilter.curyUnitPrice, IBqlDecimal>.When<BqlOperand<RelatedItemsFilter.originalQty, IBqlDecimal>.IsEqual<decimal0>>.Else<BqlOperand<RelatedItemsFilter.originalCuryExtPrice, IBqlDecimal>.Divide<RelatedItemsFilter.originalQty>>>))]
  [PXUIField(DisplayName = "Ext. Price", Enabled = false)]
  public virtual Decimal? CuryExtPrice { get; set; }

  [INUnit(typeof (RelatedItemsFilter.inventoryID), Enabled = false)]
  public virtual string UOM { get; set; }

  [PXQuantity]
  public virtual Decimal? OriginalQty { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXString(1, IsFixed = true)]
  [MultDiv.List]
  public virtual string BaseUnitMultDiv { get; set; }

  [PXDecimal(6)]
  public virtual Decimal? BaseUnitRate { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Qty. Available", Enabled = false)]
  public virtual Decimal? AvailableQty { get; set; }

  [PXInt]
  public virtual int? BranchID { get; set; }

  [Site(Enabled = false)]
  public virtual int? SiteID { get; set; }

  [PXBool]
  [PXUnboundDefault(typeof (Search<SOSetup.showOnlyAvailableRelatedItems>))]
  [PXUIField(DisplayName = "Show Only Available Items")]
  public virtual bool? OnlyAvailableItems { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Keep Original Price")]
  public virtual bool? KeepOriginalPrice { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show for All Warehouses")]
  public virtual bool? ShowForAllWarehouses { get; set; }

  [PXString(2, IsFixed = true, InputMask = ">aa")]
  [SOBehavior.List]
  public virtual string OrderBehavior { get; set; }

  [PXInt]
  public virtual int? RelatedItemsRelation { get; set; }

  [PXBool]
  public virtual bool? ShowSubstituteItems { get; set; }

  [PXBool]
  public virtual bool? ShowUpSellItems { get; set; }

  [PXBool]
  public virtual bool? ShowCrossSellItems { get; set; }

  [PXBool]
  public virtual bool? ShowOtherRelatedItems { get; set; }

  [PXBool]
  public virtual bool? ShowAllRelatedItems { get; set; }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItemsFilter.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItemsFilter.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItemsFilter.subItemID>
  {
  }

  public abstract class documentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RelatedItemsFilter.documentDate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItemsFilter.curyID>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemsFilter.curyUnitPrice>
  {
  }

  public abstract class originalCuryExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemsFilter.originalCuryExtPrice>
  {
  }

  public abstract class curyExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemsFilter.curyExtPrice>
  {
  }

  public abstract class uom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItemsFilter.uom>
  {
  }

  public abstract class originalQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemsFilter.originalQty>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItemsFilter.qty>
  {
  }

  public abstract class baseUnitMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemsFilter.baseUnitMultDiv>
  {
  }

  public abstract class baseUnitRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemsFilter.baseUnitRate>
  {
  }

  public abstract class availableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemsFilter.availableQty>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItemsFilter.branchID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItemsFilter.siteID>
  {
  }

  public abstract class onlyAvailableItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.onlyAvailableItems>
  {
  }

  public abstract class keepOriginalPrice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.keepOriginalPrice>
  {
  }

  public abstract class showForAllWarehouses : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.showForAllWarehouses>
  {
  }

  public abstract class orderBehavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemsFilter.orderBehavior>
  {
  }

  public abstract class relatedItemsRelation : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemsFilter.relatedItemsRelation>
  {
  }

  public abstract class showSubstituteItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.showSubstituteItems>
  {
  }

  public abstract class showUpSellItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.showUpSellItems>
  {
  }

  public abstract class showCrossSellItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.showCrossSellItems>
  {
  }

  public abstract class showOtherRelatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.showOtherRelatedItems>
  {
  }

  public abstract class showAllRelatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RelatedItemsFilter.showAllRelatedItems>
  {
  }
}
