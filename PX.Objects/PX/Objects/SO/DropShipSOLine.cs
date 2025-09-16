// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DropShipSOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.DAC;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXCacheName("SO Drop-Ship Line")]
[PXProjection(typeof (Select2<SOLine, LeftJoin<DropShipLink, On<DropShipLink.FK.SOLine>>, Where<SOLine.pOCreate, Equal<True>, And<SOLine.pOSource, Equal<INReplenishmentSource.dropShipToOrder>>>>))]
public class DropShipSOLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLine.orderType))]
  [PXUIField(DisplayName = "Order Type")]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLine.orderNbr))]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<DropShipSOLine.orderType>>>>))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [SOLineInventoryItem(Filterable = true, BqlField = typeof (SOLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (SOLine.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a", BqlField = typeof (SOLine.operation))]
  [SOOperation.List]
  public virtual string Operation { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLine.orderQty))]
  [PXUIField(DisplayName = "Not Linked Qty.")]
  public virtual Decimal? OrderQty { get; set; }

  [INUnit(typeof (DropShipSOLine.inventoryID), DisplayName = "UOM", BqlField = typeof (SOLine.uOM))]
  public virtual string UOM { get; set; }

  [PXDBBool(BqlField = typeof (SOLine.isLegacyDropShip))]
  public virtual bool? IsLegacyDropShip { get; set; }

  [PXNote(BqlField = typeof (SOLine.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (DropShipLink.pOOrderType))]
  public virtual string POOrderType { get; set; }

  [PXUIField(DisplayName = "Drop-Ship PO Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<DropShipSOLine.pOOrderType>>>>))]
  [PXDBString(15, IsUnicode = true, BqlField = typeof (DropShipLink.pOOrderNbr))]
  public virtual string POOrderNbr { get; set; }

  [PXUIField(DisplayName = "Drop-Ship PO Line Nbr.")]
  [PXDBInt(BqlField = typeof (DropShipLink.pOLineNbr))]
  public virtual int? POLineNbr { get; set; }

  [PXDBBool(BqlField = typeof (DropShipLink.active))]
  [PXUIField(DisplayName = "PO Linked")]
  public virtual bool? POLinkActive { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipSOLine.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipSOLine.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipSOLine.lineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipSOLine.inventoryID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipSOLine.tranDesc>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipSOLine.operation>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DropShipSOLine.orderQty>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipSOLine.uOM>
  {
  }

  public abstract class isLegacyDropShip : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DropShipSOLine.isLegacyDropShip>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DropShipSOLine.noteID>
  {
  }

  public abstract class pOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipSOLine.pOOrderType>
  {
  }

  public abstract class pOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DropShipSOLine.pOOrderNbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DropShipSOLine.pOLineNbr>
  {
  }

  public abstract class pOLinkActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DropShipSOLine.pOLinkActive>
  {
  }
}
