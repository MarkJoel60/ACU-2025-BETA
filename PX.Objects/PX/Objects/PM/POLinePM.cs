// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POLinePM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("PO Line")]
[PXProjection(typeof (Select2<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.PO.POOrder.approved, Equal<True>, And<PX.Objects.PO.POOrder.hold, Equal<False>>>>>, LeftJoin<PMCostCode, On<PX.Objects.PO.POLine.costCodeID, Equal<PMCostCode.costCodeID>>>>>))]
[Serializable]
public class POLinePM : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected string _LineType;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _VendorID;
  protected DateTime? _OrderDate;
  protected DateTime? _PromisedDate;
  protected bool? _Cancelled;
  protected bool? _Completed;
  protected int? _SiteID;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected Decimal? _ReceivedQty;
  protected Decimal? _BaseReceivedQty;
  protected string _TranDesc;
  protected int? _CostCodeID;
  protected string _AlternateID;
  protected int? _ExpenseAcctID;
  protected Decimal? _UnbilledQty;
  protected string _VendorRefNbr;
  protected string _CuryID;

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.orderType))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Type", Enabled = false)]
  [POOrderType.RPSList]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.PO.POLine.orderNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<POLinePM.orderType>>>>), DescriptionField = typeof (PX.Objects.PO.POOrder.orderDesc))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.lineNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Line Nbr.")]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.lineType))]
  [POLineType.List]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [Inventory(Filterable = true, BqlField = typeof (PX.Objects.PO.POLine.inventoryID), Enabled = false)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(BqlField = typeof (PX.Objects.PO.POLine.subItemID), Enabled = false)]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>), BqlField = typeof (PX.Objects.PO.POLine.vendorID), Enabled = false)]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POLine.orderDate))]
  [PXUIField(DisplayName = "Order Date", Enabled = false)]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POLine.promisedDate))]
  [PXUIField(DisplayName = "Promised", Enabled = false, Visible = false)]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.cancelled))]
  [PXUIField(DisplayName = "Canceled")]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.completed))]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POLine.uOM))]
  [PXUIField(DisplayName = "UOM", Enabled = false)]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.orderQty))]
  [PXUIField(DisplayName = "Order Qty.", Enabled = false)]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.baseOrderQty))]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.openQty))]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POLine.baseOpenQty))]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXUIField(DisplayName = "Qty. On Receipts")]
  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.receivedQty))]
  public virtual Decimal? ReceivedQty
  {
    get => this._ReceivedQty;
    set => this._ReceivedQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POLine.baseReceivedQty))]
  public virtual Decimal? BaseReceivedQty
  {
    get => this._BaseReceivedQty;
    set => this._BaseReceivedQty = value;
  }

  [PXDBPriceCost(BqlField = typeof (PX.Objects.PO.POLine.curyUnbilledAmt))]
  public virtual Decimal? CuryUnbilledAmt { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POLine.tranDesc))]
  [PXUIField(DisplayName = "Line Description", Enabled = false)]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [ProjectBase(BqlField = typeof (PX.Objects.PO.POLine.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<POLinePM.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveOrInPlanningProjectTask(typeof (POLinePM.projectID), "PO", DisplayName = "Project Task", BqlField = typeof (PX.Objects.PO.POLine.taskID))]
  public virtual int? TaskID { get; set; }

  [CostCode(BqlField = typeof (PX.Objects.PO.POLine.costCodeID), SkipVerification = true)]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBLong(BqlField = typeof (PX.Objects.PO.POLine.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXUIField(DisplayName = "Unit Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost(BqlField = typeof (PX.Objects.PO.POLine.curyUnitCost))]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.PO.POLine.unitCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? UnitCost { get; set; }

  [PXUIField(DisplayName = "Ext. Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POLinePM.curyInfoID), typeof (POLinePM.lineAmt), BaseCalc = false, BqlField = typeof (PX.Objects.PO.POLine.curyLineAmt))]
  public virtual Decimal? CuryLineAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.PO.POLine.lineAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost in Base Currency")]
  public virtual Decimal? LineAmount { get; set; }

  [PXUIField(DisplayName = "Line Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POLinePM.curyInfoID), typeof (POLinePM.extCost), BaseCalc = false, BqlField = typeof (PX.Objects.PO.POLine.curyExtCost))]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.PO.POLine.extCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Amount in Base Currency")]
  public virtual Decimal? ExtCost { get; set; }

  [PXUIField(DisplayName = "Alternate ID", Visible = false)]
  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POLine.alternateID))]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.expenseAcctID))]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.unbilledQty))]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POLine.taxCategoryID))]
  [PXUIField]
  public virtual string TaxCategoryID { get; set; }

  [PXDBString(40, BqlField = typeof (PX.Objects.PO.POOrder.vendorRefNbr))]
  [PXUIField(DisplayName = "Vendor Ref.", Enabled = false, Visible = false)]
  public virtual string VendorRefNbr
  {
    get => this._VendorRefNbr;
    set => this._VendorRefNbr = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.PO.POOrder.curyID))]
  [PXUIField(DisplayName = "Currency")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.HasMultipleProjects" />
  [PXDBBool(BqlField = typeof (PX.Objects.PO.POOrder.hasMultipleProjects))]
  public virtual bool? HasMultipleProjects { get; set; }

  [PXDBString(IsUnicode = true, BqlField = typeof (PMCostCode.costCodeCD), InputMask = "")]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  public virtual string CostCodeCD { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? CalcOpenQty
  {
    get
    {
      Decimal? nullable = this.OpenQty;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.UnbilledQty;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(Math.Min(valueOrDefault1, valueOrDefault2));
    }
  }

  [PXPriceCost]
  [PXUIField(DisplayName = "Open Amount")]
  public virtual Decimal? CalcCuryOpenAmt
  {
    get
    {
      if (this.OrderQty.GetValueOrDefault() == 0M || this.CalcOpenQty.GetValueOrDefault() == 0M)
        return this.CuryUnbilledAmt;
      Decimal? nullable1 = this.CuryLineAmt;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.OrderQty;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num = valueOrDefault1 / valueOrDefault2;
      Decimal? nullable2 = this.CuryUnbilledAmt;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.CalcOpenQty;
      Decimal val2 = nullable2.GetValueOrDefault() * num;
      return new Decimal?(Math.Min(valueOrDefault3, val2));
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLinePM.selected>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.lineNbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.lineType>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.subItemID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.vendorID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLinePM.orderDate>
  {
  }

  public abstract class promisedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLinePM.promisedDate>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLinePM.cancelled>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLinePM.completed>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.orderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.baseOrderQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.baseOpenQty>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLinePM.baseReceivedQty>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLinePM.curyUnbilledAmt>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.tranDesc>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.costCodeID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLinePM.curyInfoID>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.unitCost>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.lineAmt>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.extCost>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.alternateID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLinePM.expenseAcctID>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.unbilledQty>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.taxCategoryID>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.vendorRefNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.curyID>
  {
  }

  public abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POLinePM.hasMultipleProjects>
  {
  }

  public abstract class costCodeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLinePM.costCodeCD>
  {
  }

  public abstract class calcOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLinePM.calcOpenQty>
  {
  }

  public abstract class calcCuryOpenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLinePM.calcCuryOpenAmt>
  {
  }
}
