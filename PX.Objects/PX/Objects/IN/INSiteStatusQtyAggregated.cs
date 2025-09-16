// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusQtyAggregated
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select4<INSiteStatusByCostCenter, Aggregate<Max<INSiteStatusByCostCenter.lastModifiedDateTime, Sum<INSiteStatusByCostCenter.qtyAvail, GroupBy<INSiteStatusByCostCenter.inventoryID>>>>>))]
[PXCacheName]
public class INSiteStatusQtyAggregated : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Inventory(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBDecimal(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDBDate(BqlField = typeof (INSiteStatusByCostCenter.lastModifiedDateTime))]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    INSiteStatusQtyAggregated.inventoryID>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusQtyAggregated.qtyAvail>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSiteStatusQtyAggregated.lastModifiedDateTime>
  {
  }
}
