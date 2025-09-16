// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusByCostCenterShort
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// A shortened version of the <see cref="T:PX.Objects.IN.INSiteStatusByCostCenter" /> DAC which includes only commonly used fields with item quantities.
/// </summary>
[PXCacheName("IN Site Status by Cost Center Short")]
[PXProjection(typeof (SelectFrom<INSiteStatusByCostCenter>), Persistent = false)]
public class INSiteStatusByCostCenterShort : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  [PXDBInt(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.inventoryID))]
  public virtual int? InventoryID { get; set; }

  /// <exclude />
  [PXDBInt(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.subItemID))]
  public virtual int? SubItemID { get; set; }

  /// <exclude />
  [PXDBInt(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.siteID))]
  public virtual int? SiteID { get; set; }

  /// <exclude />
  [PXDBInt(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.costCenterID))]
  [PXDefault]
  public virtual int? CostCenterID { get; set; }

  /// <exclude />
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyOnHand))]
  public virtual Decimal? QtyOnHand { get; set; }

  /// <exclude />
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyNotAvail))]
  public virtual Decimal? QtyNotAvail { get; set; }

  /// <exclude />
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  public virtual Decimal? QtyAvail { get; set; }

  /// <exclude />
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyHardAvail))]
  public virtual Decimal? QtyHardAvail { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    INSiteStatusByCostCenterShort>.By<INSiteStatusByCostCenterShort.inventoryID, INSiteStatusByCostCenterShort.subItemID, INSiteStatusByCostCenterShort.siteID, INSiteStatusByCostCenterShort.costCenterID>
  {
    public static INSiteStatusByCostCenterShort Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? costCenterID)
    {
      return (INSiteStatusByCostCenterShort) PrimaryKeyOf<INSiteStatusByCostCenterShort>.By<INSiteStatusByCostCenterShort.inventoryID, INSiteStatusByCostCenterShort.subItemID, INSiteStatusByCostCenterShort.siteID, INSiteStatusByCostCenterShort.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) costCenterID, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INSiteStatusByCostCenterShort>.By<INSiteStatusByCostCenterShort.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INSiteStatusByCostCenterShort>.By<INSiteStatusByCostCenterShort.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INSiteStatusByCostCenterShort>.By<INSiteStatusByCostCenterShort.siteID>
    {
    }

    public class ItemSite : 
      PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.ForeignKeyOf<INSiteStatusByCostCenterShort>.By<INSiteStatusByCostCenterShort.inventoryID, INSiteStatusByCostCenterShort.siteID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSiteStatusByCostCenterShort.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSiteStatusByCostCenterShort.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusByCostCenterShort.siteID>
  {
  }

  public abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSiteStatusByCostCenterShort.costCenterID>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenterShort.qtyOnHand>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenterShort.qtyNotAvail>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenterShort.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenterShort.qtyHardAvail>
  {
  }
}
