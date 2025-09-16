// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityProducts
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CR.Descriptor.Attributes;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Opportunity Products")]
[Serializable]
public class CROpportunityProducts : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IHasMinGrossProfit,
  ISortOrder,
  IMatrixItemLine
{
  protected Decimal? _Quantity;
  protected Decimal? _BaseQuantity;
  protected bool? _CalculateDiscountsOnImport;
  protected 
  #nullable disable
  string _TaxCategoryID;
  protected int? _CostCodeID;
  protected bool? _ManualPrice;
  protected int? _VendorID;
  protected byte[] _tstamp;
  protected ushort[] _DiscountsAppliedToLine;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _StockItemType;

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CROpportunity.quoteNoteID))]
  [PXParent(typeof (Select<CROpportunity, Where<CROpportunity.quoteNoteID, Equal<Current<CROpportunityProducts.quoteID>>>>))]
  [PXParent(typeof (Select<CRQuote, Where<CRQuote.noteID, Equal<Current<CROpportunityProducts.quoteID>>>>), LeaveChildren = true)]
  public virtual Guid? QuoteID { get; set; }

  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CROpportunity.productCntr))]
  public virtual int? LineNbr { get; set; }

  [PXUIField(DisplayName = "Sort Order", Visible = false, Enabled = false)]
  [PXDBInt]
  public virtual int? SortOrder { get; set; }

  [PXUIField(DisplayName = "Group Line Nbr.", Visible = false, Enabled = false)]
  [PXDBInt]
  public virtual int? GroupLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Group", Enabled = false)]
  public virtual bool? IsGroup { get; set; }

  [PXDBString(3, IsFixed = false)]
  [PXUIField(DisplayName = "Type")]
  [CROpportunityProductLineType]
  [PXDefault(typeof (CROpportunityProductLineTypeAttribute.distribution))]
  public virtual string LineType { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CROpportunity.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [Inventory(Filterable = true)]
  [PXForeignReference(typeof (Field<CROpportunityProducts.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  /// <summary>The identifier of the cost account group.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Cost Account Group")]
  public virtual int? ExpenseAccountGroupID { get; set; }

  /// <summary>The identifier of the revenue account group.</summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Revenue Account Group")]
  public virtual int? RevenueAccountGroupID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Employee ID")]
  [PXEPEmployeeSelector]
  public virtual int? EmployeeID { get; set; }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>>>))]
  [INUnit(typeof (CROpportunityProducts.inventoryID))]
  public virtual string UOM { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBOpportunityProductQuantity(typeof (CROpportunityProducts.uOM), typeof (CROpportunityProducts.baseQuantity), InventoryUnitType.SalesUnit)]
  [PXUIField]
  public virtual Decimal? Quantity
  {
    get => this._Quantity;
    set => this._Quantity = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseQuantity
  {
    get => this._BaseQuantity;
    set => this._BaseQuantity = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.unitPrice))]
  [PXUIField]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<INItemSiteSettings.pOCreate, Where<INItemSiteSettings.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<CROpportunityProducts.siteID>>>>>))]
  [PXUIField(DisplayName = "Mark for PO")]
  public virtual bool? POCreate { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrencyPriceCost(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.unitCost))]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Calculate automatic discounts on import")]
  public virtual bool? CalculateDiscountsOnImport
  {
    get => this._CalculateDiscountsOnImport;
    set => this._CalculateDiscountsOnImport = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost { get; set; }

  [ManualDiscountMode(typeof (CROpportunityProducts.curyDiscAmt), typeof (CROpportunityProducts.discPct), DiscountFeatureType.CustomerDiscount)]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount, %")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscPct { get; set; }

  [PXDBCurrency(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.extPrice))]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXFormula(typeof (Mult<CROpportunityProducts.quantity, CROpportunityProducts.curyUnitPrice>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtPrice { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPrice { get; set; }

  [PXDBCurrency(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.extCost))]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<CROpportunityProducts.quantity, CROpportunityProducts.curyUnitCost>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtCost { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.discAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.amount))]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  [PXFormula(typeof (Sub<CROpportunityProducts.curyExtPrice, CROpportunityProducts.curyDiscAmt>))]
  public virtual Decimal? CuryAmount { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? Amount { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (CROpportunity.bAccountID))]
  public virtual int? CustomerID { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  [SubItem(typeof (CROpportunityProducts.inventoryID))]
  public virtual int? SubItemID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXFormula(typeof (Default<CROpportunityProducts.inventoryID>))]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>>>))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (CROpportunity.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<CROpportunityProducts.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (CROpportunityProducts.projectID), "CR", DisplayName = "Project Task")]
  public virtual int? TaskID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Project Task")]
  [PXDimension("PROTASK")]
  public virtual string TaskCD { get; set; }

  [CostCode(null, typeof (CROpportunityProducts.taskID))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Free Item")]
  public virtual bool? IsFree { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price")]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  [PXFormula(typeof (Default<CROpportunityProducts.inventoryID>))]
  [POSiteAvail(typeof (CROpportunityProducts.inventoryID), typeof (CROpportunityProducts.subItemID), typeof (CostCenter.freeStock))]
  [PXDefault(typeof (Coalesce<Search<Location.cSiteID, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>, Search<InventoryItemCurySettings.dfltSiteID, Where<InventoryItemCurySettings.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<CROpportunity.branchID>>>>>>))]
  [PXForeignReference(typeof (Field<CROpportunityProducts.siteID>.IsRelatedTo<INSite.siteID>))]
  public virtual int? SiteID { get; set; }

  /// <summary>The text for display subtotals on products grid</summary>
  [PXUIField(DisplayName = "Availability footer", Enabled = false)]
  [PXString]
  public virtual string TextForProductsGrid { set; get; }

  /// <inheritdoc cref="T:PX.Objects.IN.InventoryItemCurySettings.preferredVendorID" />
  [PXInt]
  [PXUnboundDefault(typeof (Search<INItemSiteSettings.preferredVendorID, Where<INItemSiteSettings.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<CROpportunityProducts.siteID>>>>>))]
  public virtual int? PreferredVendorID { get; set; }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vStatus, IsNull, Or<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.oneTime>>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<CROpportunityProducts.vendorID>, IsPending>, Null, Case<Where<CROpportunityProducts.preferredVendorID, IsNotNull>, CROpportunityProducts.preferredVendorID, Case<Where<Current2<CROpportunityProducts.vendorID>, IsNotNull>, Current2<CROpportunityProducts.vendorID>>>>>))]
  [PXFormula(typeof (Default<CROpportunityProducts.siteID>))]
  [PXUIField(DisplayName = "Vendor ID")]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBPackedIntegerArray]
  public virtual ushort[] DiscountsAppliedToLine
  {
    get => this._DiscountsAppliedToLine;
    set => this._DiscountsAppliedToLine = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = true)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  /// <summary>
  /// Indicates (if selected) that the automatic line discounts are not applied to this line.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ignore Automatic Line Discounts", Visible = false)]
  [PXFormula(typeof (IIf<Where<BqlOperand<CROpportunityProducts.manualPrice, IBqlBool>.IsEqual<True>>, False, CROpportunityProducts.skipLineDiscounts>))]
  public virtual bool? SkipLineDiscounts { get; set; }

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

  public Decimal? Qty
  {
    get => this.Quantity;
    set => this.Quantity = value;
  }

  [PXString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<CROpportunityProducts.inventoryID, PX.Objects.IN.InventoryItem.itemType>))]
  public virtual string StockItemType
  {
    get => this._StockItemType;
    set => this._StockItemType = value;
  }

  public abstract class quoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunityProducts.quoteID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.sortOrder>
  {
  }

  public abstract class groupLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityProducts.groupLineNbr>
  {
  }

  public abstract class isGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityProducts.isGroup>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityProducts.lineType>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CROpportunityProducts.curyInfoID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.inventoryID>
  {
  }

  public abstract class expenseAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityProducts.expenseAccountGroupID>
  {
  }

  public abstract class revenueAccountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityProducts.revenueAccountGroupID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.employeeID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityProducts.uOM>
  {
  }

  public abstract class quantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.quantity>
  {
  }

  public abstract class baseQuantity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.baseQuantity>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.curyUnitPrice>
  {
  }

  public abstract class unitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.unitPrice>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityProducts.pOCreate>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.curyUnitCost>
  {
  }

  public abstract class calculateDiscountsOnImport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityProducts.calculateDiscountsOnImport>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.unitCost>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityProducts.manualDisc>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunityProducts.discPct>
  {
  }

  public abstract class curyExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.curyExtPrice>
  {
  }

  public abstract class extPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.extPrice>
  {
  }

  public abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunityProducts.extCost>
  {
  }

  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunityProducts.discAmt>
  {
  }

  public abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.curyAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CROpportunityProducts.amount>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.customerID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityProducts.descr>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.subItemID>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProducts.taxCategoryID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.taskID>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityProducts.taskCD>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.costCodeID>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityProducts.isFree>
  {
  }

  public abstract class manualPrice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityProducts.manualPrice>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.siteID>
  {
  }

  public abstract class textForProductsGrid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProducts.textForProductsGrid>
  {
  }

  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityProducts.preferredVendorID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProducts.vendorID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunityProducts.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CROpportunityProducts.Tstamp>
  {
  }

  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CROpportunityProducts.discountsAppliedToLine>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CROpportunityProducts.documentDiscountRate>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProducts.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProducts.discountSequenceID>
  {
  }

  public abstract class skipLineDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CROpportunityProducts.skipLineDiscounts>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityProducts.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProducts.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityProducts.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityProducts.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProducts.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityProducts.lastModifiedDateTime>
  {
  }

  public abstract class stockItemType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProducts.stockItemType>
  {
  }
}
