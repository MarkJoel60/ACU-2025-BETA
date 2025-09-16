// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteHistByCostCenterD
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
public class INItemSiteHistByCostCenterD : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(IsKey = true)]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [StockItem(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [SubItem(IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CostCenterID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXDefault]
  public virtual DateTime? SDate { get; set; }

  [PXDBInt]
  public virtual int? SYear { get; set; }

  [PXDBInt]
  public virtual int? SMonth { get; set; }

  [PXDBInt]
  public virtual int? SQuater { get; set; }

  [PXDBInt]
  public virtual int? SDay { get; set; }

  [PXDBInt]
  public virtual int? SDayOfWeek { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received")]
  public virtual Decimal? QtyReceived { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Issued")]
  public virtual Decimal? QtyIssued { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Sales")]
  public virtual Decimal? QtySales { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Memos")]
  public virtual Decimal? QtyCreditMemos { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop Ship Sales")]
  public virtual Decimal? QtyDropShipSales { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer In")]
  public virtual Decimal? QtyTransferIn { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer Out")]
  public virtual Decimal? QtyTransferOut { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly In")]
  public virtual Decimal? QtyAssemblyIn { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Assembly Out")]
  public virtual Decimal? QtyAssemblyOut { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Adjusted")]
  public virtual Decimal? QtyAdjusted { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BegQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? EndQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyDebit { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyCredit { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CostDebit { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CostCredit { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? EndCost { get; set; }

  [PXDBTimestamp]
  public virtual 
  #nullable disable
  byte[] tstamp { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<INItemSiteHistByCostCenterD>.By<INItemSiteHistByCostCenterD.siteID, INItemSiteHistByCostCenterD.inventoryID, INItemSiteHistByCostCenterD.subItemID, INItemSiteHistByCostCenterD.costCenterID, INItemSiteHistByCostCenterD.sDate>
  {
    public static INItemSiteHistByCostCenterD Find(
      PXGraph graph,
      int? siteID,
      int? inventoryID,
      int? subItemID,
      int? costCenterID,
      DateTime? sDate)
    {
      return (INItemSiteHistByCostCenterD) PrimaryKeyOf<INItemSiteHistByCostCenterD>.By<INItemSiteHistByCostCenterD.siteID, INItemSiteHistByCostCenterD.inventoryID, INItemSiteHistByCostCenterD.subItemID, INItemSiteHistByCostCenterD.costCenterID, INItemSiteHistByCostCenterD.sDate>.FindBy(graph, (object) siteID, (object) inventoryID, (object) subItemID, (object) costCenterID, (object) sDate, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemSiteHistByCostCenterD>.By<INItemSiteHistByCostCenterD.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSiteHistByCostCenterD>.By<INItemSiteHistByCostCenterD.siteID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemSiteHistByCostCenterD>.By<INItemSiteHistByCostCenterD.subItemID>
    {
    }

    public class ItemSiteReplenishment : 
      PrimaryKeyOf<INItemSiteReplenishment>.By<INItemSiteReplenishment.inventoryID, INItemSiteReplenishment.siteID, INItemSiteReplenishment.subItemID>.ForeignKeyOf<INItemSiteHistByCostCenterD>.By<INItemSiteHistByCostCenterD.inventoryID, INItemSiteHistByCostCenterD.siteID, INItemSiteHistByCostCenterD.subItemID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByCostCenterD.siteID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.subItemID>
  {
  }

  public abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.costCenterID>
  {
  }

  public abstract class sDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.sDate>
  {
  }

  public abstract class sYear : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByCostCenterD.sYear>
  {
  }

  public abstract class sMonth : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByCostCenterD.sMonth>
  {
  }

  public abstract class sQuater : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByCostCenterD.sQuater>
  {
  }

  public abstract class sDay : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteHistByCostCenterD.sDay>
  {
  }

  public abstract class sDayOfWeek : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.sDayOfWeek>
  {
  }

  public abstract class qtyReceived : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyReceived>
  {
  }

  public abstract class qtyIssued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyIssued>
  {
  }

  public abstract class qtySales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtySales>
  {
  }

  public abstract class qtyCreditMemos : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyCreditMemos>
  {
  }

  public abstract class qtyDropShipSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyDropShipSales>
  {
  }

  public abstract class qtyTransferIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyTransferIn>
  {
  }

  public abstract class qtyTransferOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyTransferOut>
  {
  }

  public abstract class qtyAssemblyIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyAssemblyIn>
  {
  }

  public abstract class qtyAssemblyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyAssemblyOut>
  {
  }

  public abstract class qtyAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyAdjusted>
  {
  }

  public abstract class begQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.begQty>
  {
  }

  public abstract class endQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.endQty>
  {
  }

  public abstract class qtyDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyDebit>
  {
  }

  public abstract class qtyCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.qtyCredit>
  {
  }

  public abstract class costDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.costDebit>
  {
  }

  public abstract class costCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.costCredit>
  {
  }

  public abstract class endCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.endCost>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.Tstamp>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSiteHistByCostCenterD.lastModifiedDateTime>
  {
  }
}
