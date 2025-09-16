// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[INSiteStatusSummaryProjection]
[PXCacheName("IN Warehouse Status")]
public class INSiteStatusSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  /// <exclude />
  [Site(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.siteID))]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  /// <exclude />
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand { get; set; }

  /// <exclude />
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyNotAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Not Available")]
  public virtual Decimal? QtyNotAvail { get; set; }

  /// <exclude />
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    INSiteStatusSummary>.By<INSiteStatusSummary.inventoryID, INSiteStatusSummary.siteID>
  {
    public static INSiteStatusSummary Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      PKFindOptions options = 0)
    {
      return (INSiteStatusSummary) PrimaryKeyOf<INSiteStatusSummary>.By<INSiteStatusSummary.inventoryID, INSiteStatusSummary.siteID>.FindBy(graph, (object) inventoryID, (object) siteID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INSiteStatusSummary>.By<INSiteStatusSummary.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INSiteStatusSummary>.By<INSiteStatusSummary.siteID>
    {
    }

    public class ItemSite : 
      PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.ForeignKeyOf<INSiteStatusSummary>.By<INSiteStatusSummary.inventoryID, INSiteStatusSummary.siteID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSummary.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSummary.siteID>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusSummary.qtyOnHand>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusSummary.qtyNotAvail>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatusSummary.qtyAvail>
  {
  }
}
