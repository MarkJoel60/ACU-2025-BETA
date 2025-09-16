// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOMiscLine2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select<SOLine, Where<SOLine.lineType, Equal<SOLineType.miscCharge>>>), Persistent = true)]
[Serializable]
public class SOMiscLine2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _Operation;
  protected int? _InventoryID;
  protected int? _SiteID;
  protected int? _ProjectID;
  protected DateTime? _ShipDate;
  protected long? _CuryInfoID;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BilledQty;
  protected Decimal? _BaseBilledQty;
  protected Decimal? _UnbilledQty;
  protected Decimal? _BaseUnbilledQty;
  protected Decimal? _CuryUnitPrice;
  protected Decimal? _CuryLineAmt;
  protected Decimal? _LineAmt;
  protected Decimal? _CuryBilledAmt;
  protected Decimal? _BilledAmt;
  protected Decimal? _CuryUnbilledAmt;
  protected Decimal? _UnbilledAmt;
  protected Decimal? _CuryDiscAmt;
  protected Decimal? _DiscAmt;
  protected Decimal? _DiscPct;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _TaxCategoryID;
  protected int? _SalesPersonID;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected string _TranDesc;
  protected Guid? _NoteID;
  protected bool? _Commissionable;
  protected bool? _IsFree;
  protected bool? _ManualPrice;
  protected bool? _ManualDisc;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected DateTime? _DRTermStartDate;
  protected DateTime? _DRTermEndDate;
  protected Decimal? _CuryUnitPriceDR;
  protected Decimal? _DiscPctDR;
  protected int? _DefScheduleID;

  [PXDBInt(BqlField = typeof (SOLine.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLine.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLine.orderNbr))]
  [PXParent(typeof (Select<SOOrder, Where<SOOrder.orderType, Equal<Current<SOMiscLine2.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOMiscLine2.orderNbr>>>>>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLine.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.sortOrder))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOLine.defaultOperation))]
  public virtual string DefaultOperation { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a", BqlField = typeof (SOLine.operation))]
  [PXUIField(DisplayName = "Operation")]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBShort(BqlField = typeof (SOLine.lineSign))]
  [PXDefault]
  public virtual short? LineSign { get; set; }

  [PXDBBool(BqlField = typeof (SOLine.completed))]
  [PXDefault]
  [PXUIField(DisplayName = "Completed")]
  public virtual bool? Completed { get; set; }

  [NonStockItem(BqlField = typeof (SOLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBDate(BqlField = typeof (SOLine.shipDate))]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  /// <summary>
  /// Type of the Invoice to which the return SO line is applied.
  /// </summary>
  [PXDBString(3, IsFixed = true, BqlField = typeof (SOLine.invoiceType))]
  public virtual string InvoiceType { get; set; }

  /// <summary>
  /// Number of the Invoice to which the return SO line is applied.
  /// </summary>
  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLine.invoiceNbr))]
  public virtual string InvoiceNbr { get; set; }

  /// <summary>
  /// Number of the Invoice line to which the return SO line is applied.
  /// </summary>
  [PXDBInt(BqlField = typeof (SOLine.invoiceLineNbr))]
  public virtual int? InvoiceLineNbr { get; set; }

  [PXDBDate(BqlField = typeof (SOLine.invoiceDate))]
  public virtual DateTime? InvoiceDate { get; set; }

  [PXDBLong(BqlField = typeof (SOLine.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [INUnit(typeof (SOMiscLine2.inventoryID), BqlField = typeof (SOLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.orderQty))]
  [PXDefault]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.billedQty))]
  [PXDefault]
  public virtual Decimal? BilledQty
  {
    get => this._BilledQty;
    set => this._BilledQty = value;
  }

  [PXDBBaseQuantity(typeof (SOMiscLine2.uOM), typeof (SOMiscLine2.billedQty), BqlField = typeof (SOLine.baseBilledQty))]
  [PXDefault]
  public virtual Decimal? BaseBilledQty
  {
    get => this._BaseBilledQty;
    set => this._BaseBilledQty = value;
  }

  [PXDBQuantity(BqlField = typeof (SOLine.unbilledQty))]
  [PXUnboundFormula(typeof (BqlOperand<SOMiscLine2.unbilledQty, IBqlDecimal>.Multiply<SOMiscLine2.lineSign>), typeof (SumCalc<SOOrder.unbilledOrderQty>))]
  [PXDefault]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  [PXDBBaseQuantity(typeof (SOMiscLine2.uOM), typeof (SOMiscLine2.unbilledQty), BqlField = typeof (SOLine.baseUnbilledQty))]
  public virtual Decimal? BaseUnbilledQty
  {
    get => this._BaseUnbilledQty;
    set => this._BaseUnbilledQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.curyUnitPrice))]
  [PXDefault]
  public virtual Decimal? CuryUnitPrice
  {
    get => this._CuryUnitPrice;
    set => this._CuryUnitPrice = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.curyExtPrice))]
  [PXDefault]
  public virtual Decimal? CuryExtPrice { get; set; }

  [PXDBCurrency(typeof (SOMiscLine2.curyInfoID), typeof (SOMiscLine2.lineAmt), BqlField = typeof (SOLine.curyLineAmt))]
  [PXUIField(DisplayName = "Ext. Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineAmt
  {
    get => this._CuryLineAmt;
    set => this._CuryLineAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (SOLine.lineAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt
  {
    get => this._LineAmt;
    set => this._LineAmt = value;
  }

  [PXDBCurrency(typeof (SOMiscLine2.curyInfoID), typeof (SOMiscLine2.billedAmt), BqlField = typeof (SOLine.curyBilledAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledAmt
  {
    get => this._CuryBilledAmt;
    set => this._CuryBilledAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (SOLine.billedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledAmt
  {
    get => this._BilledAmt;
    set => this._BilledAmt = value;
  }

  [PXDBCurrency(typeof (SOMiscLine2.curyInfoID), typeof (SOMiscLine2.unbilledAmt), BqlField = typeof (SOLine.curyUnbilledAmt))]
  [PXDefault]
  public virtual Decimal? CuryUnbilledAmt
  {
    get => this._CuryUnbilledAmt;
    set => this._CuryUnbilledAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (SOLine.unbilledAmt))]
  [PXDefault]
  public virtual Decimal? UnbilledAmt
  {
    get => this._UnbilledAmt;
    set => this._UnbilledAmt = value;
  }

  [PXDBCurrency(typeof (SOMiscLine2.curyInfoID), typeof (SOMiscLine2.discAmt), BqlField = typeof (SOLine.curyDiscAmt))]
  [PXUIField(DisplayName = "Ext. Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (SOLine.discAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt
  {
    get => this._DiscAmt;
    set => this._DiscAmt = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.discPct))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDBDecimal(18, BqlField = typeof (SOLine.groupDiscountRate))]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  [PXDBDecimal(18, BqlField = typeof (SOLine.documentDiscountRate))]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (SOLine.taxZoneID))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLine.taxCategoryID))]
  [SOUnbilledMiscTax2(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), Inventory = typeof (SOMiscLine2.inventoryID), UOM = typeof (SOMiscLine2.uOM), LineQty = typeof (SOMiscLine2.unbilledQty))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [SalesPerson(BqlField = typeof (SOLine.salesPersonID))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [Account(Visible = false, BqlField = typeof (SOLine.salesAcctID))]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  [SubAccount(typeof (SOMiscLine2.salesAcctID), Visible = false, BqlField = typeof (SOLine.salesSubID))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  [ActiveProjectTask(typeof (SOLine.projectID), "SO", BqlField = typeof (SOLine.taskID), DisplayName = "Project Task")]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.costCodeID))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (SOLine.tranDesc))]
  [PXUIField(DisplayName = "Line Description")]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXNote(BqlField = typeof (SOLine.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBBool(BqlField = typeof (SOLine.commissionable))]
  public bool? Commissionable
  {
    get => this._Commissionable;
    set => this._Commissionable = value;
  }

  [PXDBBool(BqlField = typeof (SOLine.isFree))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Free Item")]
  public virtual bool? IsFree
  {
    get => this._IsFree;
    set => this._IsFree = value;
  }

  [PXDBBool(BqlField = typeof (SOLine.manualPrice))]
  [PXDefault(false)]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  [PXDBBool(BqlField = typeof (SOLine.manualDisc))]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc
  {
    get => this._ManualDisc;
    set => this._ManualDisc = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (SOLine.discountID))]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = false)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (SOLine.discountSequenceID))]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBDate(BqlField = typeof (SOLine.dRTermStartDate))]
  [PXUIField(DisplayName = "Term Start Date")]
  public DateTime? DRTermStartDate
  {
    get => this._DRTermStartDate;
    set => this._DRTermStartDate = value;
  }

  [PXDBDate(BqlField = typeof (SOLine.dRTermEndDate))]
  [PXUIField(DisplayName = "Term End Date")]
  public DateTime? DRTermEndDate
  {
    get => this._DRTermEndDate;
    set => this._DRTermEndDate = value;
  }

  [PXUIField(DisplayName = "Unit Price for DR", Visible = false)]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>), BqlField = typeof (SOLine.curyUnitPriceDR))]
  public virtual Decimal? CuryUnitPriceDR
  {
    get => this._CuryUnitPriceDR;
    set => this._CuryUnitPriceDR = value;
  }

  [PXUIField(DisplayName = "Discount Percent for DR", Visible = false)]
  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0, BqlField = typeof (SOLine.discPctDR))]
  public virtual Decimal? DiscPctDR
  {
    get => this._DiscPctDR;
    set => this._DiscPctDR = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.defScheduleID))]
  public virtual int? DefScheduleID
  {
    get => this._DefScheduleID;
    set => this._DefScheduleID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (SOLine.blanketType))]
  public virtual string BlanketType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLine.blanketNbr))]
  public virtual string BlanketNbr { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.blanketLineNbr))]
  public virtual int? BlanketLineNbr { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.blanketSplitLineNbr))]
  public virtual int? BlanketSplitLineNbr { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.branchID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.sortOrder>
  {
  }

  public abstract class defaultOperation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOMiscLine2.defaultOperation>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.operation>
  {
  }

  public abstract class lineSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOMiscLine2.lineSign>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOMiscLine2.completed>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.siteID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.projectID>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOMiscLine2.shipDate>
  {
  }

  public abstract class invoiceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.invoiceType>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.invoiceNbr>
  {
  }

  public abstract class invoiceLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.invoiceLineNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOMiscLine2.invoiceDate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOMiscLine2.curyInfoID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.orderQty>
  {
  }

  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.billedQty>
  {
  }

  public abstract class baseBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.baseBilledQty>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.baseUnbilledQty>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.curyUnitPrice>
  {
  }

  public abstract class curyExtPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.curyExtPrice>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.lineAmt>
  {
  }

  public abstract class curyBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.curyBilledAmt>
  {
  }

  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.billedAmt>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.unbilledAmt>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.discAmt>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.discPct>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.documentDiscountRate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.taxZoneID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.taxCategoryID>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.salesPersonID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.salesSubID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.costCodeID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.tranDesc>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOMiscLine2.noteID>
  {
  }

  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOMiscLine2.commissionable>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOMiscLine2.isFree>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOMiscLine2.manualPrice>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOMiscLine2.manualDisc>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOMiscLine2.discountSequenceID>
  {
  }

  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOMiscLine2.dRTermStartDate>
  {
  }

  public abstract class dRTermEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOMiscLine2.dRTermEndDate>
  {
  }

  public abstract class curyUnitPriceDR : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOMiscLine2.curyUnitPriceDR>
  {
  }

  public abstract class discPctDR : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOMiscLine2.discPctDR>
  {
  }

  public abstract class defScheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.defScheduleID>
  {
  }

  public abstract class blanketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.blanketType>
  {
  }

  public abstract class blanketNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOMiscLine2.blanketNbr>
  {
  }

  public abstract class blanketLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOMiscLine2.blanketLineNbr>
  {
  }

  public abstract class blanketSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOMiscLine2.blanketSplitLineNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOMiscLine2.Tstamp>
  {
  }
}
