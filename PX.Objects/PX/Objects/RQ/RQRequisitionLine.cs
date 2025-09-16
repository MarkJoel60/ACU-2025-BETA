// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXCacheName("Requisition Line")]
[Serializable]
public class RQRequisitionLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _ReqNbr;
  protected int? _LineNbr;
  protected int? _InventoryID;
  protected string _LineType;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected string _Description;
  protected string _UOM;
  protected string _AlternateID;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected Decimal? _OriginQty;
  protected Decimal? _BaseOriginQty;
  protected long? _CuryInfoID;
  protected Decimal? _CuryEstUnitCost;
  protected Decimal? _EstUnitCost;
  protected Decimal? _CuryEstExtCost;
  protected Decimal? _EstExtCost;
  protected Guid? _NoteID;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected Decimal? _RcptQtyMin;
  protected Decimal? _RcptQtyMax;
  protected Decimal? _RcptQtyThreshold;
  protected string _RcptQtyAction;
  protected bool? _IsUseMarkup;
  protected Decimal? _MarkupPct;
  protected DateTime? _RequestedDate;
  protected DateTime? _PromisedDate;
  protected bool? _Approved;
  protected bool? _Cancelled;
  protected bool? _TransferRequest = new bool?(false);
  protected string _TransferType = "N";
  protected string _SourceTranReqNbr;
  protected int? _SourceTranLineNbr;
  protected Decimal? _TransferQty;
  protected Decimal? _BaseTransferQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected Decimal? _BiddingQty;
  protected Decimal? _BaseBiddingQty;
  protected bool? _ByRequest;
  protected string _QTOrderNbr;
  protected int? _QTLineNbr;
  protected string _Availability;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(typeof (RQRequisition.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (RQRequisition.reqNbr), DefaultForUpdate = false)]
  [PXParent(typeof (RQRequisitionLine.FK.Requisition))]
  [PXUIField]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (RQRequisition.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDefault("D")]
  [PXString(1, IsFixed = true)]
  [PXStringList(new string[] {"D", "R"}, new string[] {"Draft", "Request"})]
  [PXUIField(DisplayName = "Line Source", Enabled = false)]
  public virtual string LineSource
  {
    [PXDependsOnFields(new System.Type[] {typeof (RQRequisitionLine.byRequest)})] get
    {
      return !this.ByRequest.GetValueOrDefault() ? "D" : "R";
    }
  }

  [PXDefault]
  [RQRequisitionInventoryItem(Filterable = true)]
  [PXForeignReference(typeof (RQRequisitionLine.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [POLineTypeList(typeof (RQRequisitionLine.inventoryID), new string[] {"GI", "NS", "SV"}, new string[] {"Goods for IN", "Non-Stock", "Service"})]
  [PXUIField]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequisitionLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (RQRequisitionLine.inventoryID))]
  [SubItemStatusVeryfier(typeof (RQRequisitionLine.inventoryID), typeof (RQRequisitionLine.siteID), new string[] {"IN", "NP", "NR"})]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXFormula(typeof (Default<RQRequisitionLine.inventoryID>))]
  [POSiteAvail(typeof (RQRequisitionLine.inventoryID), typeof (RQRequisitionLine.subItemID), typeof (CostCenter.freeStock))]
  [PXForeignReference(typeof (RQRequisitionLine.FK.Site))]
  [PXDefault(typeof (Coalesce<Coalesce<Coalesce<Coalesce<Search<RQRequisition.siteID, Where<RQRequisition.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQRequisition.shipDestType, Equal<POShippingDestination.site>>>>, Search<PX.Objects.CR.Location.cSiteID, Where<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.customerLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.customerID>>>>>>, Search<LocationBranchSettings.vSiteID, Where<LocationBranchSettings.locationID, Equal<Current<RQRequisition.vendorLocationID>>, And<LocationBranchSettings.bAccountID, Equal<Current<RQRequisition.vendorID>>, And<LocationBranchSettings.branchID, Equal<Current<RQRequisition.branchID>>>>>>, Search<PX.Objects.CR.Location.vSiteID, Where<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>>>>>, Search<InventoryItemCurySettings.dfltSiteID, Where<InventoryItemCurySettings.inventoryID, Equal<Current<RQRequisitionLine.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current<RQRequisition.branchID>>>>>>, Search<RQRequisitionLine.siteID, Where<RQRequisitionLine.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>>>>>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.descr, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequisitionLine.inventoryID>>>>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequisitionLine.inventoryID>>>>))]
  [INUnit(typeof (RQRequisitionLine.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [AlternativeItem(INPrimaryAlternateType.VPN, typeof (RQRequisitionLine.inventoryID), typeof (RQRequisitionLine.subItemID), typeof (RQRequisitionLine.uOM))]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBQuantity(typeof (RQRequisitionLine.uOM), typeof (RQRequisitionLine.baseOrderQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXDBQuantity(typeof (RQRequisitionLine.uOM), typeof (RQRequisitionLine.baseOriginQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Qty.", Enabled = false)]
  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public virtual Decimal? OriginQty
  {
    get => this._OriginQty;
    set => this._OriginQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public virtual Decimal? BaseOriginQty
  {
    get => this._BaseOriginQty;
    set => this._BaseOriginQty = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (RQRequisition.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (RQRequisitionLine.curyInfoID), typeof (RQRequisitionLine.estUnitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstUnitCost
  {
    get => this._CuryEstUnitCost;
    set => this._CuryEstUnitCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(typeof (Search<INItemCost.lastCost, Where<INItemCost.inventoryID, Equal<Current<RQRequisitionLine.inventoryID>>, And<INItemCost.curyID, EqualBaseCuryID<Current2<RQRequisition.branchID>>>>>))]
  [PXUIField(DisplayName = "Est. Unit Cost")]
  public virtual Decimal? EstUnitCost
  {
    get => this._EstUnitCost;
    set => this._EstUnitCost = value;
  }

  [PXDBCurrency(typeof (RQRequisitionLine.curyInfoID), typeof (RQRequisitionLine.estExtCost))]
  [PXUIField]
  [PXFormula(typeof (Mult<RQRequisitionLine.curyEstUnitCost, RQRequisitionLine.orderQty>), typeof (SumCalc<RQRequisition.curyEstExtCostTotal>), ValidateAggregateCalculation = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstExtCost
  {
    get => this._CuryEstExtCost;
    set => this._CuryEstExtCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Est. Ext. Cost")]
  public virtual Decimal? EstExtCost
  {
    get => this._EstExtCost;
    set => this._EstExtCost = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Visible = false)]
  public virtual bool? ManualPrice { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [Account]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [SubAccount(typeof (RQRequisitionLine.expenseAcctID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.CR.Location.vRcptQtyMin, Where<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>>>>))]
  [PXUIField(DisplayName = "Min. Receipt, %")]
  public virtual Decimal? RcptQtyMin
  {
    get => this._RcptQtyMin;
    set => this._RcptQtyMin = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0", typeof (Search<PX.Objects.CR.Location.vRcptQtyMax, Where<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>>>>))]
  [PXUIField(DisplayName = "Max. Receipt, %")]
  public virtual Decimal? RcptQtyMax
  {
    get => this._RcptQtyMax;
    set => this._RcptQtyMax = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0", typeof (Search<PX.Objects.CR.Location.vRcptQtyThreshold, Where<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>>>>))]
  [PXUIField(DisplayName = "Complete On, %")]
  public virtual Decimal? RcptQtyThreshold
  {
    get => this._RcptQtyThreshold;
    set => this._RcptQtyThreshold = value;
  }

  [PXDBString(1, IsFixed = true)]
  [POReceiptQtyAction.List]
  [PXDefault("W", typeof (Search<PX.Objects.CR.Location.vRcptQtyAction, Where<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>>>>))]
  [PXUIField(DisplayName = "Receipt Action")]
  public virtual string RcptQtyAction
  {
    get => this._RcptQtyAction;
    set => this._RcptQtyAction = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? IsUseMarkup
  {
    get => this._IsUseMarkup;
    set => this._IsUseMarkup = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search<INItemSite.markupPct, Where<INItemSite.inventoryID, Equal<Current<RQRequisitionLine.inventoryID>>, And<INItemSite.siteID, Equal<Current<RQRequisitionLine.siteID>>>>>, Search<PX.Objects.IN.InventoryItem.markupPct, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequisitionLine.inventoryID>>>>>))]
  [PXUIField(DisplayName = "Markup, %")]
  public virtual Decimal? MarkupPct
  {
    get => this._MarkupPct;
    set => this._MarkupPct = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Required Date")]
  public virtual DateTime? RequestedDate
  {
    get => this._RequestedDate;
    set => this._RequestedDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Promised Date")]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Transfer")]
  public virtual bool? TransferRequest
  {
    get => this._TransferRequest;
    set => this._TransferRequest = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Transfer Type", Enabled = false)]
  [RQTransferType.List]
  public virtual string TransferType
  {
    get => this._TransferType;
    set => this._TransferType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string SourceTranReqNbr
  {
    get => this._SourceTranReqNbr;
    set => this._SourceTranReqNbr = value;
  }

  [PXDBInt]
  [PXUIField]
  public virtual int? SourceTranLineNbr
  {
    get => this._SourceTranLineNbr;
    set => this._SourceTranLineNbr = value;
  }

  [PXDBQuantity(typeof (RQRequisitionLine.uOM), typeof (RQRequisitionLine.baseTransferQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transfer Qty.", Enabled = false)]
  public virtual Decimal? TransferQty
  {
    get => this._TransferQty;
    set => this._TransferQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseTransferQty
  {
    get => this._BaseTransferQty;
    set => this._BaseTransferQty = value;
  }

  [PXDBQuantity(typeof (RQRequisitionLine.uOM), typeof (RQRequisitionLine.baseOpenQty), HandleEmptyKey = true)]
  [PXFormula(typeof (RQRequisitionLine.orderQty), typeof (SumCalc<RQRequisition.openOrderQty>), ValidateAggregateCalculation = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXDBQuantity(typeof (RQRequisitionLine.uOM), typeof (RQRequisitionLine.baseBiddingQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Bidding Qty.", Enabled = false)]
  public virtual Decimal? BiddingQty
  {
    get => this._BiddingQty;
    set => this._BiddingQty = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseBiddingQty
  {
    get => this._BaseBiddingQty;
    set => this._BaseBiddingQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ByRequest
  {
    get => this._ByRequest;
    set => this._ByRequest = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  public virtual string QTOrderNbr
  {
    get => this._QTOrderNbr;
    set => this._QTOrderNbr = value;
  }

  [PXDBInt]
  public virtual int? QTLineNbr
  {
    get => this._QTLineNbr;
    set => this._QTLineNbr = value;
  }

  [PXString]
  [PXUIField]
  public virtual string Availability
  {
    get => this._Availability;
    set => this._Availability = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<RQRequisitionLine>.By<RQRequisitionLine.reqNbr, RQRequisitionLine.lineNbr>
  {
    public RQRequisitionLine Find(PXGraph graph, string reqNbr, int? lineNbr)
    {
      return (RQRequisitionLine) PrimaryKeyOf<RQRequisitionLine>.By<RQRequisitionLine.reqNbr, RQRequisitionLine.lineNbr>.FindBy(graph, (object) reqNbr, (object) lineNbr, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class Requisition : 
      PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.ForeignKeyOf<RQRequisitionLine>.By<RQRequisitionLine.reqNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<RQRequisitionLine>.By<RQRequisitionLine.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<RQRequisitionLine>.By<RQRequisitionLine.siteID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisitionLine.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.branchID>
  {
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLine.reqNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.lineNbr>
  {
  }

  public abstract class lineSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLine.lineSource>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      PX.Objects.IN.InventoryItem.baseUnit.PreventEditIfExists<Select2<RQRequisitionLine, InnerJoin<RQRequisition, On<RQRequisition.reqNbr, Equal<RQRequisitionLine.reqNbr>>>, Where<RQRequisitionLine.inventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>, And2<Where2<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.goodsForInventory, POLineType.goodsForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.goodsForReplenishment, POLineType.goodsForManufacturing, POLineType.goodsForProject>>.AsStrings.Contains<RQRequisitionLine.lineType>, Or<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.nonStock, POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.service, POLineType.nonStockForManufacturing, POLineType.nonStockForProject>>.AsStrings.Contains<RQRequisitionLine.lineType>>>, And<RQRequisition.approved, NotEqual<True>, And<RQRequisitionLine.cancelled, NotEqual<True>, And<RQRequisition.cancelled, NotEqual<True>>>>>>>>
    {
    }
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLine.lineType>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.siteID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLine.uOM>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.alternateID>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequisitionLine.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.baseOrderQty>
  {
  }

  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public abstract class originQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequisitionLine.originQty>
  {
  }

  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public abstract class baseOriginQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.baseOriginQty>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQRequisitionLine.curyInfoID>
  {
  }

  public abstract class curyEstUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.curyEstUnitCost>
  {
  }

  public abstract class estUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.estUnitCost>
  {
  }

  public abstract class curyEstExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.curyEstExtCost>
  {
  }

  public abstract class estExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.estExtCost>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisitionLine.manualPrice>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequisitionLine.noteID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.expenseSubID>
  {
  }

  public abstract class rcptQtyMin : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.rcptQtyMin>
  {
  }

  public abstract class rcptQtyMax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.rcptQtyMax>
  {
  }

  public abstract class rcptQtyThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.rcptQtyThreshold>
  {
  }

  public abstract class rcptQtyAction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.rcptQtyAction>
  {
  }

  public abstract class isUseMarkup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisitionLine.isUseMarkup>
  {
  }

  public abstract class markupPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequisitionLine.markupPct>
  {
  }

  public abstract class requestedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisitionLine.requestedDate>
  {
  }

  public abstract class promisedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisitionLine.promisedDate>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisitionLine.approved>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisitionLine.cancelled>
  {
  }

  public abstract class transferRequest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequisitionLine.transferRequest>
  {
  }

  public abstract class transferType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.transferType>
  {
  }

  public abstract class sourceTranReqNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.sourceTranReqNbr>
  {
  }

  public abstract class sourceTranLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisitionLine.sourceTranLineNbr>
  {
  }

  public abstract class transferQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.transferQty>
  {
  }

  public abstract class baseTransferQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.baseTransferQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequisitionLine.openQty>
  {
  }

  public abstract class baseOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.baseOpenQty>
  {
  }

  public abstract class biddingQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.biddingQty>
  {
  }

  public abstract class baseBiddingQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLine.baseBiddingQty>
  {
  }

  public abstract class byRequest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisitionLine.byRequest>
  {
  }

  public abstract class qTOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLine.qTOrderNbr>
  {
  }

  public abstract class qTLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLine.qTLineNbr>
  {
  }

  public abstract class availability : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.availability>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQRequisitionLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequisitionLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisitionLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQRequisitionLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisitionLine.lastModifiedDateTime>
  {
  }
}
