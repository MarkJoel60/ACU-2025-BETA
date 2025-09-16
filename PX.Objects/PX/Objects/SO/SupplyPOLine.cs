// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SupplyPOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Supply PO Line")]
[PXProjection(typeof (Select2<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>>, Where<PX.Objects.PO.POOrder.isLegacyDropShip, Equal<boolFalse>>>), new System.Type[] {typeof (PX.Objects.PO.POLine)})]
public class SupplyPOLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public virtual 
  #nullable disable
  int?[] SelectedSOLines { get; set; }

  public virtual int?[] LinkedSOLines { get; set; }

  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  [PXBool]
  public virtual bool? Selected { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXQuantity]
  public virtual Decimal? BaseDemandQty { get; set; }

  [PXDefault]
  [POOrderType.List]
  [PXUIField(DisplayName = "PO Type", Enabled = false)]
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.orderType))]
  public virtual string OrderType { get; set; }

  [PXDefault]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<SupplyPOLine.orderType>>>>), DescriptionField = typeof (PX.Objects.PO.POOrder.orderDesc))]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.PO.POLine.orderNbr))]
  [PXParent(typeof (SupplyPOLine.FK.SupplyOrder), LeaveChildren = true)]
  public virtual string OrderNbr { get; set; }

  [PXDefault]
  [PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [POLineType.List]
  [PXUIField(DisplayName = "Line Type", Enabled = false)]
  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.lineType))]
  public virtual string LineType { get; set; }

  [Inventory(Filterable = true, BqlField = typeof (PX.Objects.PO.POLine.inventoryID), Enabled = false)]
  public virtual int? InventoryID { get; set; }

  [SubItem(BqlField = typeof (PX.Objects.PO.POLine.subItemID), Enabled = false)]
  public virtual int? SubItemID { get; set; }

  [PXUIField(Visible = false, Enabled = false)]
  [PXDBLong(BqlField = typeof (PX.Objects.PO.POLine.planID))]
  public virtual long? PlanID { get; set; }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>), BqlField = typeof (PX.Objects.PO.POLine.vendorID), Enabled = false)]
  public virtual int? VendorID { get; set; }

  [PXUIField(DisplayName = "Vendor Ref.", Enabled = false)]
  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POOrder.vendorRefNbr))]
  public virtual string VendorRefNbr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POOrder.hold))]
  public virtual bool? Hold { get; set; }

  [PXUIField(DisplayName = "Order Date", Enabled = false)]
  [PXDBDate(BqlField = typeof (PX.Objects.PO.POLine.orderDate))]
  public virtual DateTime? OrderDate { get; set; }

  [PXUIField(DisplayName = "Promised", Enabled = false)]
  [PXDBDate(BqlField = typeof (PX.Objects.PO.POLine.promisedDate))]
  public virtual DateTime? PromisedDate { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.cancelled))]
  public virtual bool? Cancelled { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.completed))]
  public virtual bool? Completed { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.closed))]
  public virtual bool? Closed { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.siteID))]
  public virtual int? SiteID { get; set; }

  [INUnit(typeof (SupplyPOLine.inventoryID), DisplayName = "UOM", Enabled = false, BqlField = typeof (PX.Objects.PO.POLine.uOM))]
  public virtual string UOM { get; set; }

  [PXUIField(DisplayName = "Order Qty.", Enabled = false)]
  [PXDBQuantity(typeof (SupplyPOLine.uOM), typeof (SupplyPOLine.baseOrderQty), BqlField = typeof (PX.Objects.PO.POLine.orderQty))]
  [PXDefault]
  public virtual Decimal? OrderQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.baseOrderQty))]
  [PXDefault]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  [PXDBQuantity(typeof (SupplyPOLine.uOM), typeof (SupplyPOLine.baseOpenQty), BqlField = typeof (PX.Objects.PO.POLine.openQty))]
  [PXDefault]
  public virtual Decimal? OpenQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.baseOpenQty))]
  [PXDefault]
  public virtual Decimal? BaseOpenQty { get; set; }

  [PXDBQuantity(typeof (SupplyPOLine.uOM), typeof (SupplyPOLine.baseReceivedQty), BqlField = typeof (PX.Objects.PO.POLine.receivedQty))]
  [PXDefault]
  public virtual Decimal? ReceivedQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.baseReceivedQty))]
  [PXDefault]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXUIField(DisplayName = "Line Description", Enabled = false)]
  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POLine.tranDesc))]
  public virtual string TranDesc { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.taskID))]
  public virtual int? TaskID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.isSpecialOrder))]
  [PXUnboundFormula(typeof (IIf<Where<SupplyPOLine.isSpecialOrder, Equal<True>>, int1, int0>), typeof (SumCalc<SupplyPOOrder.specialLineCntr>))]
  public virtual bool? IsSpecialOrder { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.sODeleted))]
  public virtual bool? SODeleted { get; set; }

  /// <exclude />
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.PO.POOrder.curyID))]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID { get; set; }

  /// <exclude />
  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POLine.curyUnitCost))]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.PO.POLine.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.PO.POLine.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.PO.POLine.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<SupplyPOLine>.By<SupplyPOLine.orderType, SupplyPOLine.orderNbr, SupplyPOLine.lineNbr>
  {
    public static SupplyPOLine Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SupplyPOLine) PrimaryKeyOf<SupplyPOLine>.By<SupplyPOLine.orderType, SupplyPOLine.orderNbr, SupplyPOLine.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class SupplyOrder : 
      PrimaryKeyOf<SupplyPOOrder>.By<SupplyPOOrder.orderType, SupplyPOOrder.orderNbr>.ForeignKeyOf<SupplyPOLine>.By<SupplyPOLine.orderType, SupplyPOLine.orderNbr>
    {
    }
  }

  public abstract class selectedSOLines : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SupplyPOLine.selectedSOLines>
  {
  }

  public abstract class linkedSOLines : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    SupplyPOLine.linkedSOLines>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOLine.selected>
  {
  }

  public abstract class baseDemandQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SupplyPOLine.baseDemandQty>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOLine.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOLine.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.lineNbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOLine.lineType>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.subItemID>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SupplyPOLine.planID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.vendorID>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOLine.vendorRefNbr>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOLine.hold>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SupplyPOLine.orderDate>
  {
  }

  public abstract class promisedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SupplyPOLine.promisedDate>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOLine.cancelled>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOLine.completed>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOLine.closed>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOLine.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SupplyPOLine.orderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SupplyPOLine.baseOrderQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SupplyPOLine.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SupplyPOLine.baseOpenQty>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SupplyPOLine.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SupplyPOLine.baseReceivedQty>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOLine.tranDesc>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.costCodeID>
  {
  }

  public abstract class isSpecialOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOLine.isSpecialOrder>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SupplyPOLine.costCenterID>
  {
  }

  public abstract class sODeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SupplyPOLine.sODeleted>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SupplyPOLine.curyID>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SupplyPOLine.curyUnitCost>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SupplyPOLine.Tstamp>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SupplyPOLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SupplyPOLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SupplyPOLine.lastModifiedDateTime>
  {
  }
}
