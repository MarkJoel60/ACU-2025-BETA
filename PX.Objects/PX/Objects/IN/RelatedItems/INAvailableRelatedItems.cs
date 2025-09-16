// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.INAvailableRelatedItems
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

[PXProjection(typeof (SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Field<INRelatedInventory.relatedInventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<PX.Objects.IN.InventoryItem, INRelatedInventory>>, And<BqlOperand<INRelatedInventory.isActive, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.isTemplate, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.unknown, InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion, InventoryItemStatus.noSales>>>>.And<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>>>>, FbqlJoins.Left<INSiteStatusByCostCenter>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.inventoryID, Equal<INRelatedInventory.relatedInventoryID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>.And<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>>>>, FbqlJoins.Left<INSubItem>.On<INSiteStatusByCostCenter.FK.SubItem>>, FbqlJoins.Left<INSite>.On<INSiteStatusByCostCenter.FK.Site>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSubItem.subItemID, IsNull>>>>.Or<CurrentMatch<INSubItem, AccessInfo.userName>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSite.siteID, IsNull>>>>.Or<CurrentMatch<INSite, AccessInfo.userName>>>>), Persistent = false)]
[PXHidden]
public class INAvailableRelatedItems : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (INRelatedInventory.inventoryID))]
  public int? OriginalInventoryID { get; set; }

  [PXDBInt(BqlField = typeof (INRelatedInventory.relatedInventoryID))]
  public int? InventoryID { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.IN.InventoryItem.stkItem))]
  public virtual bool? StkItem { get; set; }

  [PXDBDate(BqlField = typeof (INRelatedInventory.effectiveDate))]
  public DateTime? EffectiveDate { get; set; }

  [PXDBDate(BqlField = typeof (INRelatedInventory.expirationDate))]
  public DateTime? ExpirationDate { get; set; }

  [PXDBInt(BqlField = typeof (INSiteStatusByCostCenter.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(BqlField = typeof (INSiteStatusByCostCenter.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBInt(BqlField = typeof (INSite.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDBString(5, IsFixed = true, BqlField = typeof (INRelatedInventory.relation))]
  public 
  #nullable disable
  string Relation { get; set; }

  [PXDBBool(BqlField = typeof (INRelatedInventory.required))]
  public bool? Required { get; set; }

  public abstract class originalInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INAvailableRelatedItems.originalInventoryID>
  {
  }

  public abstract class relatedInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INAvailableRelatedItems.relatedInventoryID>
  {
  }

  public abstract class stkItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INAvailableRelatedItems.stkItem>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAvailableRelatedItems.effectiveDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INAvailableRelatedItems.expirationDate>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INAvailableRelatedItems.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INAvailableRelatedItems.siteID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INAvailableRelatedItems.branchID>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INAvailableRelatedItems.qtyAvail>
  {
  }

  public abstract class relation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INAvailableRelatedItems.relation>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INAvailableRelatedItems.required>
  {
  }
}
