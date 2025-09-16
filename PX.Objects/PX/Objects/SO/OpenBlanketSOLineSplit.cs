// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.OpenBlanketSOLineSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Projections;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Blanket SO Line Split for Add Blanket Line dialog")]
[PXVirtual]
public class OpenBlanketSOLineSplit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXString(2, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Visible = true, Enabled = false)]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXString(15, IsUnicode = true, IsKey = true)]
  [PXUIField]
  public virtual string OrderNbr { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? LineNbr { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? SplitLineNbr { get; set; }

  [Inventory(Enabled = false, Visible = true)]
  public virtual int? InventoryID { get; set; }

  [SubItem(Enabled = false)]
  public virtual int? SubItemID { get; set; }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string TranDesc { get; set; }

  [Site(Enabled = false)]
  [PXUIField(DisplayName = "Warehouse", Enabled = false, Visible = false)]
  public virtual int? SiteID { get; set; }

  [PXString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Customer Order Nbr.", Enabled = false)]
  public virtual string CustomerOrderNbr { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Sched. Order Date", Enabled = false)]
  public virtual DateTime? SchedOrderDate { get; set; }

  [PXInt]
  public virtual int? CustomerID { get; set; }

  [LocationActive(DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Ship-To Location", Enabled = false)]
  public virtual int? CustomerLocationID { get; set; }

  [INUnit(typeof (OpenBlanketSOLineSplit.inventoryID), Enabled = false)]
  public virtual string UOM { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Blanket Open Qty.", Enabled = false)]
  public virtual Decimal? BlanketOpenQty { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone", Enabled = false)]
  public virtual string TaxZoneID { get; set; }

  public class PK : 
    PrimaryKeyOf<OpenBlanketSOLineSplit>.By<OpenBlanketSOLineSplit.orderType, OpenBlanketSOLineSplit.orderNbr, OpenBlanketSOLineSplit.lineNbr, OpenBlanketSOLineSplit.splitLineNbr>
  {
    public static OpenBlanketSOLineSplit Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (OpenBlanketSOLineSplit) PrimaryKeyOf<OpenBlanketSOLineSplit>.By<OpenBlanketSOLineSplit.orderType, OpenBlanketSOLineSplit.orderNbr, OpenBlanketSOLineSplit.lineNbr, OpenBlanketSOLineSplit.splitLineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class BlanketOrderLine : 
      PrimaryKeyOf<BlanketSOLine>.By<BlanketSOLine.orderType, BlanketSOLine.orderNbr, BlanketSOLine.lineNbr>.ForeignKeyOf<OpenBlanketSOLineSplit>.By<OpenBlanketSOLineSplit.orderType, OpenBlanketSOLineSplit.orderNbr, OpenBlanketSOLineSplit.lineNbr>
    {
    }

    public class BlanketOrderLineSplit : 
      PrimaryKeyOf<BlanketSOLineSplit>.By<BlanketSOLineSplit.orderType, BlanketSOLineSplit.orderNbr, BlanketSOLineSplit.lineNbr, BlanketSOLineSplit.splitLineNbr>.ForeignKeyOf<OpenBlanketSOLineSplit>.By<OpenBlanketSOLineSplit.orderType, OpenBlanketSOLineSplit.orderNbr, OpenBlanketSOLineSplit.lineNbr, OpenBlanketSOLineSplit.splitLineNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.selected>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OpenBlanketSOLineSplit.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.lineNbr>
  {
  }

  public abstract class splitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OpenBlanketSOLineSplit.splitLineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.subItemID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.tranDesc>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.siteID>
  {
  }

  public abstract class customerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OpenBlanketSOLineSplit.customerOrderNbr>
  {
  }

  public abstract class schedOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OpenBlanketSOLineSplit.schedOrderDate>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OpenBlanketSOLineSplit.customerLocationID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OpenBlanketSOLineSplit.uOM>
  {
  }

  public abstract class blanketOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OpenBlanketSOLineSplit.blanketOpenQty>
  {
  }

  public abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OpenBlanketSOLineSplit.taxZoneID>
  {
  }
}
