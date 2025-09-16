// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.InvoiceSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.Attributes;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

/// <exclude />
[PXCacheName("Invoice Split")]
[InvoiceSplitProjection(true)]
public class InvoiceSplit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _AutoCreateIssueLine;

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(3, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.tranType))]
  [PXUIField(DisplayName = "AR Doc. Type", Visible = false)]
  public virtual 
  #nullable disable
  string ARDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.refNbr))]
  [PXUIField(DisplayName = "AR Doc. Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr, Where<BqlOperand<PX.Objects.AR.ARInvoice.docType, IBqlString>.IsEqual<BqlField<InvoiceSplit.aRDocType, IBqlString>.FromCurrent.NoDefault>>>))]
  public virtual string ARRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.lineNbr))]
  [PXUIField]
  public virtual int? ARLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARTran.lineType))]
  public virtual string ARLineType { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARTran.tranDate))]
  [PXUIField(DisplayName = "AR Doc. Date")]
  public virtual DateTime? ARTranDate { get; set; }

  [Customer(BqlField = typeof (PX.Objects.AR.ARTran.customerID))]
  public virtual int? CustomerID { get; set; }

  [Inventory(DisplayName = "Inventory ID", BqlField = typeof (PX.Objects.AR.ARTran.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (InvoiceSplit.inventoryID), BqlField = typeof (PX.Objects.AR.ARTran.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXBool]
  [PXDBCalced(typeof (IIf<Where<PX.Objects.AR.ARTran.sOShipmentType, Equal<PX.Objects.IN.INDocType.dropShip>>, True, False>), typeof (bool))]
  [PXUIField(DisplayName = "Drop Ship")]
  public virtual bool? DropShip { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.orderType))]
  [PXUIField(DisplayName = "Order Type", Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string SOOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.SO.SOLine.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<BqlOperand<PX.Objects.SO.SOOrder.orderType, IBqlString>.IsEqual<BqlField<InvoiceSplit.sOOrderType, IBqlString>.FromCurrent.NoDefault>>>))]
  public virtual string SOOrderNbr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? SOLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.lineType))]
  public virtual string SOLineType { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.orderDate))]
  [PXUIField(DisplayName = "Order Date")]
  public virtual DateTime? SOOrderDate { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.salesPersonID))]
  public virtual int? SalesPersonID { get; set; }

  [PXDBCalced(typeof (IsNull<INTran.docType, InvoiceSplit.iNDocType.emptyDoc>), typeof (string))]
  [PXString(1, IsKey = true, IsFixed = true)]
  public virtual string INDocType { get; set; }

  [PXDBCalced(typeof (IsNull<INTran.refNbr, InvoiceSplit.iNDocType.emptyDoc>), typeof (string))]
  [PXString(15, IsKey = true, IsUnicode = true)]
  public virtual string INRefNbr { get; set; }

  [PXDBCalced(typeof (IsNull<INTran.lineNbr, int0>), typeof (int))]
  [PXInt(IsKey = true)]
  public virtual int? INLineNbr { get; set; }

  [PXDBCalced(typeof (IsNull<INTranSplit.splitLineNbr, int0>), typeof (int))]
  [PXInt(IsKey = true)]
  public virtual int? INSplitLineNbr { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARTran.tranDesc))]
  [PXUIField(DisplayName = "Line Description")]
  public virtual string TranDesc { get; set; }

  [Inventory(DisplayName = "Component ID", IsDBField = false)]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.inventoryID, NotEqual<INTran.inventoryID>>, INTran.inventoryID>>), typeof (int))]
  public virtual int? ComponentID { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.inventoryID, NotEqual<INTran.inventoryID>>, INTran.tranDesc>>), typeof (string))]
  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Component Description")]
  public virtual string ComponentDesc { get; set; }

  [Site(IsDBField = false, Visible = false)]
  [PXDBCalced(typeof (IsNull<INTranSplit.siteID, IsNull<INTran.siteID, PX.Objects.SO.SOLine.siteID>>), typeof (int))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (InvoiceSplit.siteID), IsDBField = false, Visible = false)]
  [PXDBCalced(typeof (IsNull<INTranSplit.locationID, IsNull<INTran.locationID, PX.Objects.SO.SOLine.locationID>>), typeof (int))]
  public virtual int? LocationID { get; set; }

  [PX.Objects.IN.LotSerialNbr(BqlField = typeof (INTranSplit.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [INUnit(DisplayName = "UOM", Enabled = false, IsDBField = false)]
  [PXDBCalced(typeof (IsNull<INTranSplit.uOM, IsNull<INTran.uOM, PX.Objects.AR.ARTran.uOM>>), typeof (string))]
  public virtual string UOM { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Original Qty.")]
  [PXDBCalced(typeof (IsNull<INTranSplit.qty, IsNull<INTran.qty, IIf<Where<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsIn<PX.Objects.AR.ARDocType.creditMemo, PX.Objects.AR.ARDocType.cashReturn>>, Minus<PX.Objects.AR.ARTran.qty>, PX.Objects.AR.ARTran.qty>>>), typeof (Decimal))]
  public virtual Decimal? Qty { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INTranSplit.baseQty, IsNull<INTran.baseQty, PX.Objects.AR.ARTran.baseQty>>), typeof (Decimal))]
  public virtual Decimal? BaseQty { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Available for Return")]
  public virtual Decimal? QtyAvailForReturn { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Qty. Returned", Visible = false)]
  public virtual Decimal? QtyReturned { get; set; }

  [PXQuantity(MinValue = 0.0)]
  [PXUIField(DisplayName = "Qty. to Return")]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyToReturn { get; set; }

  [PXBool]
  public virtual bool? SerialIsOnHand { get; set; }

  [PXBool]
  public virtual bool? SerialIsAlreadyReceived { get; set; }

  [PXString]
  public virtual string SerialIsAlreadyReceivedRef { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.AR.ARTran.qty))]
  public virtual Decimal? ARTranQty { get; set; }

  [INUnit(BqlField = typeof (PX.Objects.AR.ARTran.uOM))]
  public virtual string ARTranUOM { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARTran.drCr))]
  public virtual string ARTranDrCr { get; set; }

  [PXDBQuantity(BqlField = typeof (INTran.qty))]
  public virtual Decimal? INTranQty { get; set; }

  [INUnit(BqlField = typeof (INTran.uOM))]
  public virtual string INTranUOM { get; set; }

  [PXBool]
  [PXDBCalced(typeof (IIf<Where<PX.Objects.AR.ARTran.inventoryID, Equal<INTran.inventoryID>>, False, True>), typeof (bool))]
  public virtual bool? IsKit { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the line of the Issue type will
  /// be created automatically for each order line of the Receipt type if the order is of the RR type.
  /// </summary>
  [PXBool]
  public virtual bool? AutoCreateIssueLine { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceSplit.selected>
  {
  }

  public abstract class aRDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.aRDocType>
  {
  }

  public abstract class aRRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.aRRefNbr>
  {
  }

  public abstract class aRLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.aRLineNbr>
  {
  }

  public abstract class aRlineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.aRlineType>
  {
  }

  public abstract class aRTranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  InvoiceSplit.aRTranDate>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.customerID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.subItemID>
  {
  }

  public abstract class dropShip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceSplit.dropShip>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.sOLineNbr>
  {
  }

  public abstract class sOlineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.sOlineType>
  {
  }

  public abstract class sOOrderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  InvoiceSplit.sOOrderDate>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.salesPersonID>
  {
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.iNDocType>
  {
    public const string EmptyDoc = "~";

    public class emptyDoc : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InvoiceSplit.iNDocType.emptyDoc>
    {
      public emptyDoc()
        : base("~")
      {
      }
    }
  }

  public abstract class iNRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.iNRefNbr>
  {
  }

  public abstract class iNLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.iNLineNbr>
  {
  }

  public abstract class iNSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.iNSplitLineNbr>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.tranDesc>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.componentID>
  {
  }

  public abstract class componentDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.componentDesc>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplit.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.lotSerialNbr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceSplit.baseQty>
  {
  }

  public abstract class qtyAvailForReturn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InvoiceSplit.qtyAvailForReturn>
  {
  }

  public abstract class qtyReturned : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceSplit.qtyReturned>
  {
  }

  public abstract class qtyToReturn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceSplit.qtyToReturn>
  {
  }

  public abstract class serialIsOnHand : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceSplit.serialIsOnHand>
  {
  }

  public abstract class serialIsAlreadyReceived : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InvoiceSplit.serialIsAlreadyReceived>
  {
  }

  public abstract class serialIsAlreadyReceivedRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplit.serialIsAlreadyReceivedRef>
  {
  }

  public abstract class aRTranQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceSplit.aRTranQty>
  {
  }

  public abstract class aRTranUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.aRTranUOM>
  {
  }

  public abstract class aRTranDrCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.aRTranDrCr>
  {
  }

  public abstract class iNTranQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceSplit.iNTranQty>
  {
  }

  public abstract class iNTranUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplit.iNTranUOM>
  {
  }

  public abstract class isKit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceSplit.isKit>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.DAC.Projections.InvoiceSplit.AutoCreateIssueLine" />
  public abstract class autoCreateIssueLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InvoiceSplit.autoCreateIssueLine>
  {
  }
}
