// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.RelatedItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

[PXCacheName("Related Item")]
[PXProjection(typeof (SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.inventoryID, Equal<BqlField<RelatedItemsFilter.inventoryID, IBqlInt>.FromCurrent.Value>>>>, And<BqlOperand<INRelatedInventory.isActive, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<INRelatedInventory.relatedInventoryID>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.isTemplate, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.unknown, InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion, InventoryItemStatus.noSales>>>>.And<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>>>>, FbqlJoins.Inner<INUnit>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.unitType, Equal<INUnitType.inventoryItem>>>>, And<BqlOperand<INUnit.inventoryID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>>.And<BqlOperand<INUnit.fromUnit, IBqlString>.IsEqual<INRelatedInventory.uom>>>>, FbqlJoins.Left<INSiteStatusByCostCenter>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.inventoryID, Equal<INRelatedInventory.relatedInventoryID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>.And<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>>>>, FbqlJoins.Left<INSubItem>.On<INSiteStatusByCostCenter.FK.SubItem>>, FbqlJoins.Left<PX.Objects.IN.INSite>.On<INSiteStatusByCostCenter.FK.Site>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CurrentValue<RelatedItemsFilter.documentDate>, IsNull>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.effectiveDate, IsNull>>>>.Or<BqlOperand<INRelatedInventory.effectiveDate, IBqlDateTime>.IsLessEqual<BqlField<RelatedItemsFilter.documentDate, IBqlDateTime>.FromCurrent.Value>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.expirationDate, IsNull>>>>.Or<BqlOperand<INRelatedInventory.expirationDate, IBqlDateTime>.IsGreaterEqual<BqlField<RelatedItemsFilter.documentDate, IBqlDateTime>.FromCurrent.Value>>>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INSite.siteID, IsNull>>>>.Or<BqlChainableConditionLite<CurrentMatch<PX.Objects.IN.INSite, AccessInfo.userName>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<FeatureInstalled<FeaturesSet.interBranch>>, Or<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, BqlField<RelatedItemsFilter.branchID, IBqlInt>.FromCurrent>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CurrentValue<RelatedItemsFilter.orderBehavior>, IsNotNull>>>>.And<BqlOperand<CurrentValue<RelatedItemsFilter.orderBehavior>, IBqlString>.IsEqual<SOBehavior.qT>>>>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSubItem.subItemID, IsNull>>>>.Or<CurrentMatch<INSubItem, AccessInfo.userName>>>>), Persistent = false)]
public class RelatedItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (INRelatedInventory.inventoryID))]
  public int? InventoryID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INRelatedInventory.lineID))]
  public virtual int? LineID { get; set; }

  [Inventory(BqlField = typeof (INRelatedInventory.relatedInventoryID), Enabled = false)]
  public virtual int? RelatedInventoryID { get; set; }

  [PXString]
  [PXUIField]
  [PXFormula(typeof (Selector<RelatedItem.relatedInventoryID, PX.Objects.IN.InventoryItem.descr>))]
  public virtual 
  #nullable disable
  string Desc { get; set; }

  [PXDBString(5, IsFixed = true, BqlField = typeof (INRelatedInventory.relation))]
  [PXUIField(DisplayName = "Relation", Enabled = false)]
  [InventoryRelation.List]
  public virtual string Relation { get; set; }

  [PXDBInt(BqlField = typeof (INRelatedInventory.rank))]
  [PXUIField(DisplayName = "Rank", Enabled = false)]
  public virtual int? Rank { get; set; }

  [PXDBString(4, IsFixed = true, BqlField = typeof (INRelatedInventory.tag))]
  [PXUIField(DisplayName = "Tag", Enabled = false)]
  [InventoryRelationTag.List]
  public virtual string Tag { get; set; }

  [INUnit(typeof (RelatedItem.relatedInventoryID), BqlField = typeof (INRelatedInventory.uom), Enabled = false)]
  public virtual string UOM { get; set; }

  [PXDBQuantity(BqlField = typeof (INRelatedInventory.qty))]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(BqlField = typeof (INRelatedInventory.baseQty))]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBBool(BqlField = typeof (INRelatedInventory.interchangeable))]
  [PXUIField(DisplayName = "Customer Approval Not Needed", Enabled = false)]
  public virtual bool? Interchangeable { get; set; }

  [PXDBBool(BqlField = typeof (INRelatedInventory.required))]
  [PXUIField(DisplayName = "Required", Enabled = false)]
  public virtual bool? Required { get; set; }

  [PXNote(PopupTextEnabled = true, BqlField = typeof (INRelatedInventory.noteID))]
  public virtual Guid? NoteID { get; set; }

  [SubItem(typeof (RelatedItem.inventoryID), BqlField = typeof (INSubItem.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INSubItem.subItemCD>, Empty>), typeof (string))]
  public virtual string SubItemCD { get; set; }

  [Site(BqlField = typeof (INSiteStatusByCostCenter.siteID), Enabled = false)]
  public virtual int? SiteID { get; set; }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<PX.Objects.IN.INSite.siteCD>, Empty>), typeof (string))]
  public virtual string SiteCD { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXQuantity]
  [PXDefault(typeof (BqlFunction<Mult<Current<RelatedItemsFilter.qty>, BqlOperand<Current<RelatedItemsFilter.baseUnitRate>, IBqlDecimal>.When<BqlOperand<Current<RelatedItemsFilter.baseUnitMultDiv>, IBqlString>.IsEqual<MultDiv.multiply>>.Else<BqlOperand<decimal1, IBqlDecimal>.Divide<BqlField<RelatedItemsFilter.baseUnitRate, IBqlDecimal>.FromCurrent>>>, IBqlDecimal>.Multiply<RelatedItem.qty>))]
  [PXUIField(DisplayName = "Qty. Selected", Enabled = false)]
  public virtual Decimal? QtySelected { get; set; }

  [PXDBDecimal(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  public virtual Decimal? BaseAvailableQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.InventoryItem.StkItem" />
  [PXDBBool(BqlField = typeof (PX.Objects.IN.InventoryItem.stkItem))]
  public virtual bool? StkItem { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsNotEqual<True>>.Else<BqlOperand<decimal0, IBqlDecimal>.When<BqlOperand<INSiteStatusByCostCenter.qtyAvail, IBqlDecimal>.IsNull>.Else<BqlOperand<Mult<INSiteStatusByCostCenter.qtyAvail, INUnit.unitRate>, IBqlDecimal>.When<BqlOperand<INUnit.unitMultDiv, IBqlString>.IsEqual<MultDiv.divide>>.Else<BqlOperand<INSiteStatusByCostCenter.qtyAvail, IBqlDecimal>.Divide<INUnit.unitRate>>>>), typeof (Decimal))]
  [PXUIField(DisplayName = "Qty. Available", Enabled = false)]
  public virtual Decimal? AvailableQty { get; set; }

  [PXPriceCost]
  [PXUIField(DisplayName = "Unit Price", Enabled = false)]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXCury(typeof (RelatedItemsFilter.curyID))]
  [PXFormula(typeof (BqlOperand<RelatedItem.curyUnitPrice, IBqlDecimal>.Multiply<RelatedItem.qtySelected>))]
  [PXUIField(DisplayName = "Ext. Price", Enabled = false)]
  public virtual Decimal? CuryExtPrice { get; set; }

  [PXCury(typeof (RelatedItemsFilter.curyID))]
  [PXFormula(typeof (Case<Where<BqlOperand<RelatedItem.relation, IBqlString>.IsNotEqual<InventoryRelation.crossSell>>, BqlOperand<RelatedItem.curyExtPrice, IBqlDecimal>.Subtract<BqlField<RelatedItemsFilter.curyExtPrice, IBqlDecimal>.FromCurrent>>))]
  [PXUIField(DisplayName = "Ext. Price Difference", Enabled = false)]
  public virtual Decimal? PriceDiff { get; set; }

  public class PK : 
    PrimaryKeyOf<RelatedItem>.By<RelatedItem.inventoryID, RelatedItem.lineID, RelatedItem.subItemCD, RelatedItem.siteCD>
  {
    public static RelatedItem Find(
      PXGraph graph,
      int? inventoryID,
      int? lineID,
      string subItemCD,
      string siteCD,
      PKFindOptions options = 0)
    {
      return (RelatedItem) PrimaryKeyOf<RelatedItem>.By<RelatedItem.inventoryID, RelatedItem.lineID, RelatedItem.subItemCD, RelatedItem.siteCD>.FindBy(graph, (object) inventoryID, (object) lineID, (object) subItemCD, (object) siteCD, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INRelatedInventory>.By<RelatedItem.inventoryID>
    {
    }

    public class RelatedInventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INRelatedInventory>.By<RelatedItem.relatedInventoryID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItem.inventoryID>
  {
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItem.lineID>
  {
  }

  public abstract class relatedInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItem.relatedInventoryID>
  {
  }

  public abstract class desc : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItem.relatedInventoryID>
  {
  }

  public abstract class relation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItem.relation>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItem.rank>
  {
  }

  public abstract class tag : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItem.tag>
  {
  }

  public abstract class uom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItem.uom>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItem.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItem.baseQty>
  {
  }

  public abstract class interchangeable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelatedItem.interchangeable>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelatedItem.required>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RelatedItem.noteID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItem.subItemID>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItem.subItemCD>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItem.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItem.siteCD>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelatedItem.selected>
  {
  }

  public abstract class qtySelected : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItem.qtySelected>
  {
  }

  public abstract class baseAvailableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItem.baseAvailableQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.IN.RelatedItems.RelatedItem.StkItem" />
  public abstract class stkItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelatedItem.stkItem>
  {
  }

  public abstract class availableQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItem.availableQty>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItem.curyUnitPrice>
  {
  }

  public abstract class curyExtPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItem.curyExtPrice>
  {
  }

  public abstract class priceDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItem.priceDiff>
  {
  }
}
