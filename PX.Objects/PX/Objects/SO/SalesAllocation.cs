// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SalesAllocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXCacheName("Sales Allocation")]
[PXProjection(typeof (SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrder>.On<SOLine.FK.Order>>, FbqlJoins.Inner<SOOrderType>.On<KeysRelation<Field<SOOrder.orderType>.IsRelatedTo<SOOrderType.orderType>.AsSimpleKey.WithTablesOf<SOOrderType, SOOrder>, SOOrderType, SOOrder>.And<BqlOperand<SOOrderType.behavior, IBqlString>.IsIn<SOBehavior.bL, SOBehavior.sO, SOBehavior.tR, SOBehavior.rM>>>>, FbqlJoins.Inner<SOOrderTypeOperation>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Field<SOOrderTypeOperation.orderType>.IsRelatedTo<SOOrderType.orderType>.AsSimpleKey.WithTablesOf<SOOrderType, SOOrderTypeOperation>>, And<BqlOperand<SOOrderTypeOperation.operation, IBqlString>.IsEqual<SOOperation.issue>>>>.And<BqlOperand<SOOrderTypeOperation.active, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<SOOrder.FK.Customer>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Field<SOLine.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<PX.Objects.IN.InventoryItem, SOLine>>, And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>>.And<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>>>>, FbqlJoins.Inner<SOLineSiteAllocation>.On<KeysRelation<CompositeKey<Field<SOLineSiteAllocation.orderType>.IsRelatedTo<SOLine.orderType>, Field<SOLineSiteAllocation.orderNbr>.IsRelatedTo<SOLine.orderNbr>, Field<SOLineSiteAllocation.lineNbr>.IsRelatedTo<SOLine.lineNbr>>.WithTablesOf<SOLine, SOLineSiteAllocation>, SOLine, SOLineSiteAllocation>.And<BqlOperand<SOLineSiteAllocation.siteID, IBqlInt>.IsEqual<BqlField<SalesAllocationsFilter.siteID, IBqlInt>.FromCurrent.Value>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.isSpecialOrder, NotEqual<True>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderType.behavior, Equal<SOBehavior.tR>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNotNull>>>>.And<CurrentMatch<PX.Objects.AR.Customer, AccessInfo.userName>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.pOCreate, NotEqual<True>>>>>.Or<BqlOperand<SOLine.pOSource, IBqlString>.IsEqual<INReplenishmentSource.purchaseToOrder>>>>>.And<BqlOperand<SOLine.completed, IBqlBool>.IsNotEqual<True>>>), Persistent = false)]
public class SalesAllocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLine.orderType))]
  [PXUIField(DisplayName = "Order Type")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<SOOrderType, TypeArrayOf<IFbqlJoin>.Empty>, SOOrderType>.SearchFor<SOOrderType.orderType>), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLine.orderNbr))]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOOrder.orderType, IBqlString>.IsEqual<BqlField<SalesAllocation.orderType, IBqlString>.FromCurrent>>, SOOrder>.SearchFor<SOOrder.orderNbr>))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLine.lineNbr))]
  [PXUIField(DisplayName = "SO Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  [Site(BqlField = typeof (SOLine.siteID), Visible = false)]
  public virtual int? LineSiteID { get; set; }

  [StockItem(BqlField = typeof (SOLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (SOLine.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [INUnit(typeof (SOLine.inventoryID), BqlField = typeof (SOLine.uOM), Visible = false)]
  public virtual string UOM { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLine.orderQty))]
  [PXUIField(DisplayName = "Line Qty.", Visible = false)]
  public virtual Decimal? LineQty { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLine.baseOrderQty))]
  [PXUIField(DisplayName = "Base Qty.", Visible = false)]
  public virtual Decimal? BaseLineQty { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOLine.shipComplete))]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Line Shipping Rule")]
  public virtual string ShipComplete { get; set; }

  [PXDBDate(BqlField = typeof (SOLine.requestDate))]
  [PXUIField(DisplayName = "Line Requested On")]
  public virtual DateTime? RequestDate { get; set; }

  [PXDBDate(BqlField = typeof (SOLine.shipDate))]
  [PXUIField(DisplayName = "Line Ship On")]
  public virtual DateTime? ShipDate { get; set; }

  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (SalesAllocation.curyInfoID), typeof (SalesAllocation.lineAmt), BqlField = typeof (SOLine.curyLineAmt))]
  [PXUIField(DisplayName = "Line Amount", Visible = false)]
  public virtual Decimal? CuryLineAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOLine.lineAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt { get; set; }

  [Site(DisplayName = "Alloc. Warehouse", BqlField = typeof (SOLineSiteAllocation.siteID), Visible = false)]
  public virtual int? SplitSiteID { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLineSiteAllocation.qtyAllocated))]
  [PXUIField(DisplayName = "Qty. Allocated")]
  public virtual Decimal? QtyAllocated { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLineSiteAllocation.qtyUnallocated))]
  [PXUIField(DisplayName = "Qty. Unallocated")]
  public virtual Decimal? QtyUnallocated { get; set; }

  [PXDBQuantity(BqlField = typeof (SOLineSiteAllocation.lotSerialQtyAllocated))]
  public virtual Decimal? LotSerialQtyAllocated { get; set; }

  [INUnit(typeof (SOLine.inventoryID), DisplayName = "Base UOM", BqlField = typeof (PX.Objects.IN.InventoryItem.baseUnit), Visible = false)]
  public virtual string BaseUOM { get; set; }

  [PXDBString(IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual string InventoryCD { get; set; }

  [PXDBShort(BqlField = typeof (SOOrder.priority))]
  [PXUIField(DisplayName = "Order Priority")]
  public virtual short? OrderPriority { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOOrder.status))]
  [PXUIField(DisplayName = "Order Status")]
  [SOOrderStatus.List]
  public virtual string OrderStatus { get; set; }

  [PXDBBool(BqlField = typeof (SOOrder.hold))]
  public virtual bool? OrderHold { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (SOOrder.orderDesc))]
  [PXUIField(DisplayName = "Order Description", Visible = false)]
  public virtual string OrderDesc { get; set; }

  [CustomerActive(BqlField = typeof (SOOrder.customerID), DescriptionField = null)]
  public virtual int? CustomerID { get; set; }

  [PXDBDate(BqlField = typeof (SOOrder.orderDate))]
  [PXUIField(DisplayName = "Order Date")]
  public virtual DateTime? OrderDate { get; set; }

  [PXDBDate(BqlField = typeof (SOOrder.cancelDate))]
  [PXUIField(DisplayName = "Cancel By")]
  public virtual DateTime? CancelDate { get; set; }

  [SalesPerson(BqlField = typeof (SOOrder.salesPersonID), Visible = false)]
  public virtual int? SalesPersonID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (SOOrder.curyID))]
  [PXUIField(DisplayName = "Currency", Visible = false)]
  public virtual string CuryID { get; set; }

  [PXDBDateAndTime(BqlField = typeof (SOOrder.createdDateTime))]
  public virtual DateTime? OrderCreatedOn { get; set; }

  [PXDBCurrency(typeof (SalesAllocation.curyInfoID), typeof (SOOrder.orderTotal), BqlField = typeof (SOOrder.curyOrderTotal))]
  [PXUIField(DisplayName = "Order Total", Visible = false)]
  public virtual Decimal? CuryOrderTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOOrder.orderTotal))]
  public virtual Decimal? OrderTotal { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXUIField(DisplayName = "Customer Name", Visible = false)]
  public virtual string CustomerName { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.customerClassID))]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<CustomerClass, TypeArrayOf<IFbqlJoin>.Empty>, CustomerClass>.SearchFor<CustomerClass.customerClassID>), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Customer Class", Visible = false)]
  public virtual string CustomerClassID { get; set; }

  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Available for Shipping")]
  [PXUIVisible(typeof (BqlOperand<Current<SalesAllocationsFilter.action>, IBqlString>.IsNotEqual<SalesAllocationsFilter.action.deallocateSalesOrders>))]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXQuantity(MinValue = 0.0)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. to Allocate")]
  [PXUIVisible(typeof (BqlOperand<Current<SalesAllocationsFilter.action>, IBqlString>.IsNotEqual<SalesAllocationsFilter.action.deallocateSalesOrders>))]
  [PXUIEnabled(typeof (BqlOperand<Current<SalesAllocationsFilter.action>, IBqlString>.IsNotEqual<SalesAllocationsFilter.action.deallocateSalesOrders>))]
  public virtual Decimal? QtyToAllocate { get; set; }

  [PXQuantity(MinValue = 0.0)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. to Deallocate")]
  [PXUIVisible(typeof (BqlOperand<Current<SalesAllocationsFilter.action>, IBqlString>.IsEqual<SalesAllocationsFilter.action.deallocateSalesOrders>))]
  [PXUIEnabled(typeof (BqlOperand<Current<SalesAllocationsFilter.action>, IBqlString>.IsEqual<SalesAllocationsFilter.action.deallocateSalesOrders>))]
  public virtual Decimal? QtyToDeallocate { get; set; }

  [PXQuantity(MinValue = 0.0)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BufferedQty { get; set; }

  [PXDateAndTime]
  public virtual DateTime? BufferedTime { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? IsExtraAllocation { get; set; }

  [PXNote(BqlField = typeof (SOLine.noteID))]
  public virtual Guid? NoteID { get; set; }

  public static class FK
  {
    public class SiteStatusByCostCenterShort : 
      PrimaryKeyOf<INSiteStatusByCostCenterShort>.By<INSiteStatusByCostCenterShort.inventoryID, INSiteStatusByCostCenterShort.subItemID, INSiteStatusByCostCenterShort.siteID, INSiteStatusByCostCenterShort.costCenterID>.ForeignKeyOf<SalesAllocation>.By<SalesAllocation.inventoryID, SalesAllocation.subItemID, SalesAllocation.lineSiteID, SalesAllocation.costCenterID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SalesAllocation.selected>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.lineNbr>
  {
  }

  public abstract class lineSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.lineSiteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.subItemID>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.costCenterID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.tranDesc>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.uOM>
  {
  }

  public abstract class lineQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SalesAllocation.lineQty>
  {
  }

  public abstract class baseLineQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.baseLineQty>
  {
  }

  public abstract class shipComplete : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesAllocation.shipComplete>
  {
  }

  public abstract class requestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesAllocation.requestDate>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SalesAllocation.shipDate>
  {
  }

  [PXDBLong(BqlField = typeof (SOLine.curyInfoID))]
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SalesAllocation.curyInfoID>
  {
  }

  public abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SalesAllocation.lineAmt>
  {
  }

  public abstract class splitSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.splitSiteID>
  {
  }

  public abstract class qtyAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.qtyAllocated>
  {
  }

  public abstract class qtyUnallocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.qtyUnallocated>
  {
  }

  public abstract class lotSerialQtyAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.lotSerialQtyAllocated>
  {
  }

  public abstract class baseUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.baseUOM>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.inventoryCD>
  {
  }

  public abstract class orderPriority : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SalesAllocation.orderPriority>
  {
  }

  public abstract class orderStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.orderStatus>
  {
  }

  public abstract class orderHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SalesAllocation.orderHold>
  {
  }

  public abstract class orderDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.orderDesc>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.customerID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SalesAllocation.orderDate>
  {
  }

  public abstract class cancelDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesAllocation.cancelDate>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocation.salesPersonID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocation.curyID>
  {
  }

  public abstract class orderCreatedOn : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesAllocation.orderCreatedOn>
  {
  }

  public abstract class curyOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.curyOrderTotal>
  {
  }

  public abstract class orderTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SalesAllocation.orderTotal>
  {
  }

  public abstract class customerName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesAllocation.customerName>
  {
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesAllocation.customerClassID>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.qtyHardAvail>
  {
  }

  public abstract class qtyToAllocate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.qtyToAllocate>
  {
  }

  public abstract class qtyToDeallocate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.qtyToDeallocate>
  {
  }

  public abstract class bufferedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SalesAllocation.bufferedQty>
  {
  }

  public abstract class bufferedTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesAllocation.bufferedTime>
  {
  }

  public abstract class isExtraAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SalesAllocation.isExtraAllocation>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SalesAllocation.noteID>
  {
  }
}
