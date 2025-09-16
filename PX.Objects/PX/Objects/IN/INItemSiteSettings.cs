// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select2<InventoryItem, CrossJoin<INSite, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.inventoryID, Equal<InventoryItem.inventoryID>, And<InventoryItemCurySettings.curyID, Equal<INSite.baseCuryID>>>, LeftJoin<INItemRep, On<INItemRep.inventoryID, Equal<InventoryItem.inventoryID>, And<INItemRep.curyID, Equal<INSite.baseCuryID>, And<INItemRep.replenishmentClassID, Equal<INSite.replenishmentClassID>>>>, LeftJoinSingleTable<INItemSite, On<INItemSite.inventoryID, Equal<InventoryItem.inventoryID>, And<INItemSite.siteID, Equal<INSite.siteID>>>, LeftJoin<INItemStats, On<INItemStats.FK.ItemSite>>>>>>>))]
[PXHidden]
[Serializable]
public class INItemSiteSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected int? _DefaultSubItemID;
  protected int? _SiteID;
  protected Decimal? _NegativeCost;

  [PXDBInt(IsKey = true, BqlField = typeof (InventoryItem.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (InventoryItem.defaultSubItemID))]
  public virtual int? DefaultSubItemID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INSite.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBCalced(typeof (IsNull<InventoryItemCurySettings.preferredVendorID, INItemSite.preferredVendorID>), typeof (int))]
  public virtual int? PreferredVendorID { get; set; }

  [PXDBCalced(typeof (IsNull<InventoryItemCurySettings.preferredVendorLocationID, INItemSite.preferredVendorLocationID>), typeof (int))]
  public virtual int? PreferredVendorLocationID { get; set; }

  [PXDBCalced(typeof (IsNull<INItemSite.replenishmentSource, INItemRep.replenishmentSource>), typeof (string))]
  public virtual 
  #nullable disable
  string ReplenishmentSource { get; set; }

  [PXDBCalced(typeof (IsNull<INItemSite.replenishmentSourceSiteID, INItemRep.replenishmentSourceSiteID>), typeof (int))]
  public virtual int? ReplenishmentSourceSiteID { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<IsNull<INItemSite.replenishmentSource, INItemRep.replenishmentSource>, Equal<INReplenishmentSource.purchaseToOrder>, Or<IsNull<INItemSite.replenishmentSource, INItemRep.replenishmentSource>, Equal<INReplenishmentSource.dropShipToOrder>>>, boolTrue>, boolFalse>), typeof (bool))]
  public virtual bool? POCreate { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<IsNull<INItemSite.replenishmentSource, INItemRep.replenishmentSource>, Equal<INReplenishmentSource.dropShipToOrder>>, INReplenishmentSource.dropShipToOrder, Case<Where<IsNull<INItemSite.replenishmentSource, INItemRep.replenishmentSource>, Equal<INReplenishmentSource.purchaseToOrder>>, INReplenishmentSource.purchaseToOrder>>, INReplenishmentSource.none>), typeof (string))]
  public virtual string POSource { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<INItemSite.inventoryID, IsNotNull>, True>, False>), typeof (bool))]
  public virtual bool? INItemSiteExists { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (InventoryItem.valMethod))]
  public virtual string ValMethod { get; set; }

  [PXDBCalced(typeof (IsNull<INItemSite.aBCCodeID, InventoryItem.aBCCodeID>), typeof (string))]
  public virtual string ABCCodeID { get; set; }

  [PXDBCalced(typeof (IsNull<INItemSite.movementClassID, InventoryItem.movementClassID>), typeof (string))]
  public virtual string MovementClassID { get; set; }

  [PXDBCalced(typeof (IsNull<INItemSite.invtAcctID, InventoryItem.invtAcctID>), typeof (int))]
  public virtual int? InvtAcctID { get; set; }

  [PXDBCalced(typeof (IsNull<INItemSite.invtSubID, InventoryItem.invtSubID>), typeof (int))]
  public virtual int? InvtSubID { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<InventoryItem.valMethod, Equal<INValMethod.standard>>, IsNull<INItemSite.stdCost, InventoryItemCurySettings.stdCost>, Case<Where<InventoryItem.valMethod, Equal<INValMethod.average>, And<INItemStats.qtyOnHand, NotEqual<decimal0>, And<Div<INItemStats.totalCost, INItemStats.qtyOnHand>, Greater<decimal0>>>>, Div<INItemStats.totalCost, INItemStats.qtyOnHand>, Case<Where<INItemStats.lastCostDate, GreaterEqual<INItemStats.dateAfterMinDate>>, INItemStats.lastCost>>>, Null>), typeof (Decimal))]
  [PXDecimal]
  public virtual Decimal? NegativeCost
  {
    get => this._NegativeCost;
    set => this._NegativeCost = value;
  }

  public class PK : 
    PrimaryKeyOf<INItemSiteSettings>.By<INItemSiteSettings.inventoryID, INItemSiteSettings.siteID>
  {
    public static INItemSiteSettings Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      PKFindOptions options = 0)
    {
      return (INItemSiteSettings) PrimaryKeyOf<INItemSiteSettings>.By<INItemSiteSettings.inventoryID, INItemSiteSettings.siteID>.FindBy(graph, (object) inventoryID, (object) siteID, options);
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteSettings.inventoryID>
  {
  }

  public abstract class defaultSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteSettings.defaultSubItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteSettings.siteID>
  {
  }

  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteSettings.preferredVendorID>
  {
  }

  public abstract class preferredVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteSettings.preferredVendorLocationID>
  {
  }

  public abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteSettings.replenishmentSource>
  {
  }

  public abstract class replenishmentSourceSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteSettings.replenishmentSourceSiteID>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSiteSettings.pOCreate>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSiteSettings.pOSource>
  {
  }

  public abstract class iNItemSiteExists : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSiteSettings.iNItemSiteExists>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSiteSettings.valMethod>
  {
  }

  public abstract class aBCCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSiteSettings.aBCCodeID>
  {
  }

  public abstract class movementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteSettings.movementClassID>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteSettings.invtAcctID>
  {
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteSettings.invtSubID>
  {
  }

  public abstract class negativeCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteSettings.negativeCost>
  {
  }
}
