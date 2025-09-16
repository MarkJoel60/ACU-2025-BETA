// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.INSiteStatusGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.AffectedAvailability;

[PXHidden]
[PXProjection(typeof (Select4<INSiteStatusByCostCenter, Aggregate<GroupBy<INSiteStatusByCostCenter.inventoryID, GroupBy<INSiteStatusByCostCenter.siteID, Sum<INSiteStatusByCostCenter.qtyOnHand, Sum<INSiteStatusByCostCenter.qtyHardAvail>>>>>>))]
public class INSiteStatusGroup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Inventory(BqlField = typeof (INSiteStatusByCostCenter.inventoryID), IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [Site(BqlField = typeof (INSiteStatusByCostCenter.siteID), IsKey = true)]
  public virtual int? SiteID { get; set; }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyOnHand))]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyHardAvail))]
  public virtual Decimal? QtyHardAvail { get; set; }

  public abstract class inventoryID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusGroup.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusGroup.siteID>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatusGroup.qtyOnHand>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusGroup.qtyHardAvail>
  {
  }
}
