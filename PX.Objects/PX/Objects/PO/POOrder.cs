// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.RQ;
using PX.Objects.TX;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXPrimaryGraph(new System.Type[] {typeof (SubcontractEntry), typeof (POOrderEntry)}, new System.Type[] {typeof (Where<BqlOperand<POOrder.orderType, IBqlString>.IsEqual<POOrderType.regularSubcontract>>), typeof (Where<BqlOperand<True, IBqlBool>.IsEqual<True>>)})]
[PXCacheName("Purchase Order")]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POOrder.vendorID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class POOrder : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  IProjectHeader,
  IProjectTaxes,
  INotable
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected DateTime? _OrderDate;
  protected DateTime? _ExpectedDate;
  protected DateTime? _ExpirationDate;
  protected string _Status;
  protected bool? _Hold;
  protected bool? _Approved;
  protected bool? _Rejected = new bool?(false);
  protected bool? _RequestApproval;
  protected bool? _Cancelled;
  protected Guid? _NoteID;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected int? _LineCntr;
  protected string _VendorRefNbr;
  protected Decimal? _CuryOrderTotal;
  protected Decimal? _OrderTotal;
  protected Decimal? _CuryControlTotal;
  protected Decimal? _ControlTotal;
  protected Decimal? _OrderQty;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected string _TaxZoneID;
  protected string _TaxCalcMode;
  protected int? _RemitAddressID;
  protected int? _RemitContactID;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected string _BLType;
  protected string _BLOrderNbr;
  protected string _RQReqNbr;
  protected string _OrderDesc;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _ShipDestType;
  protected int? _SiteID;
  protected int? _ShipToBAccountID;
  protected int? _ShipToLocationID;
  protected int? _ShipAddressID;
  protected int? _ShipContactID;
  protected Decimal? _OpenTaxTotal;
  protected int? _EmployeeID;
  protected int? _OwnerWorkgroupID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected bool? _DontPrint;
  protected bool? _Printed;
  protected bool? _DontEmail;
  protected bool? _Emailed;
  protected string _FOBPoint;
  protected string _ShipVia;
  protected Decimal? _OrderWeight;
  protected Decimal? _OrderVolume;
  protected bool? _DisableAutomaticDiscountCalculation;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<POOrder.vendorLocationID, IsNotNull, And<Selector<POOrder.vendorLocationID, PX.Objects.CR.Location.vBranchID>, IsNotNull>>, Selector<POOrder.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<Current2<POOrder.branchID>, IsNotNull>, Current2<POOrder.branchID>>>, Current<AccessInfo.branchID>>))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("RO")]
  [POOrderType.List]
  [PXUIField]
  [PXFieldDescription]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PX.Objects.PO.PO.Numbering]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<POOrder.orderType, Equal<Optional<POOrder.orderType>>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull>>, OrderBy<Desc<POOrder.orderNbr>>>), Filterable = true)]
  [PXFieldDescription]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField(DisplayName = "Workflow", FieldClass = "CHANGEORDER", Visible = false)]
  [PXDefault("S")]
  [POBehavior.List]
  public virtual string Behavior { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? OverrideCurrency { get; set; }

  [POVendor]
  [PXDefault]
  [PXForeignReference(typeof (POOrder.FK.Vendor))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [PXForeignReference(typeof (Field<POOrder.vendorLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (POOrder.orderDate))]
  [PXUIField(DisplayName = "Promised On")]
  public virtual DateTime? ExpectedDate
  {
    get => this._ExpectedDate;
    set => this._ExpectedDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (POOrder.orderDate))]
  [PXUIField(DisplayName = "Expires On")]
  public virtual DateTime? ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [POOrderStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  [PXDefault(true)]
  [PXNoUpdate]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Rejected
  {
    get => this._Rejected;
    set => this._Rejected = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Request Approval", Visible = false)]
  public virtual bool? RequestApproval
  {
    get => this._RequestApproval;
    set => this._RequestApproval = value;
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
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnbilledTaxValid { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? ExternalTaxesImportInProgress { get; set; }

  [PXSearchable(128 /*0x80*/, "{0} - {2}", new System.Type[] {typeof (POOrder.orderNbr), typeof (POOrder.vendorID), typeof (PX.Objects.AP.Vendor.acctName)}, new System.Type[] {typeof (POOrder.vendorRefNbr), typeof (POOrder.orderDesc)}, NumberFields = new System.Type[] {typeof (POOrder.orderNbr)}, Line1Format = "{0}{1:d}{2}{3}{4}", Line1Fields = new System.Type[] {typeof (POOrder.orderType), typeof (POOrder.orderDate), typeof (POOrder.status), typeof (POOrder.vendorRefNbr), typeof (POOrder.expectedDate)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (POOrder.orderDesc)}, MatchWithJoin = typeof (InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POOrder.vendorID>>>), SelectForFastIndexing = typeof (Select2<POOrder, InnerJoin<PX.Objects.AP.Vendor, On<POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>>, OrderBy<Desc<POOrder.orderNbr>>>))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LinesToCloseCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LinesToCompleteCntr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string VendorRefNbr
  {
    get => this._VendorRefNbr;
    set => this._VendorRefNbr = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.orderTotal))]
  [PXUIField]
  public virtual Decimal? CuryOrderTotal
  {
    get => this._CuryOrderTotal;
    set => this._CuryOrderTotal = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Order Total")]
  public virtual Decimal? OrderTotal
  {
    get => this._OrderTotal;
    set => this._OrderTotal = value;
  }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.controlTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlTotal
  {
    get => this._CuryControlTotal;
    set => this._CuryControlTotal = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlTotal
  {
    get => this._ControlTotal;
    set => this._ControlTotal = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Order Qty")]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  /// <summary>
  /// The total <see cref="T:PX.Objects.PO.POOrder.lineDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.lineDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? CuryLineDiscTotal { get; set; }

  /// <summary>
  /// The total line discount of the document, which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? LineDiscTotal { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.PO.POOrder.groupDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of group discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.groupDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group Discounts", Enabled = false)]
  public virtual Decimal? CuryGroupDiscTotal { get; set; }

  /// <summary>
  /// The total group discount of the document, which is calculated as the sum of group discounts of the order.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group Discounts", Enabled = false)]
  public virtual Decimal? GroupDiscTotal { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.PO.POOrder.documentDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of document discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.documentDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = false)]
  public virtual Decimal? CuryDocumentDiscTotal { get; set; }

  /// <summary>
  /// The total document discount of the document, which is calculated as the sum of document discounts of the order.
  /// </summary>
  /// <remarks>
  /// <para>If the <see cref="T:PX.Objects.CS.FeaturesSet.vendorDiscounts">Vendor Discounts</see> feature is not enabled on
  /// the Enable/Disable Features (CS100000) form,
  /// a user can enter a document-level discount manually. This manual discount has no discount code or
  /// sequence and is not recalculated by the system. If the manual discount needs to be changed, a user has to
  /// correct it manually.</para>
  /// </remarks>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = false)]
  public virtual Decimal? DocumentDiscTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTot { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discounts", Enabled = true)]
  public virtual Decimal? CuryDiscTot { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.PO.POOrder.orderDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of all group, document and line discounts of the order.
  /// </summary>
  [PXCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.orderDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<POOrder.curyDiscTot, POOrder.curyLineDiscTotal>), typeof (Decimal))]
  [PXFormula(typeof (Add<POOrder.curyDiscTot, POOrder.curyLineDiscTotal>))]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? CuryOrderDiscTotal { get; set; }

  /// <summary>
  /// The total discount of the document, which is calculated as the sum of group, document and line discounts of the order.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<POOrder.discTot, POOrder.lineDiscTotal>), typeof (Decimal))]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? OrderDiscTotal { get; set; }

  /// <summary>
  /// The total amount on all lines of the document, except for Freight, Description and Service lines, before Line-level discounts
  /// are applied (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.goodsExtCostTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Goods")]
  public virtual Decimal? CuryGoodsExtCostTotal { get; set; }

  /// <summary>
  /// The total amount on all lines of the document, except for Freight, Description and Service lines, before Line-level discounts
  /// are applied.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GoodsExtCostTotal { get; set; }

  /// <summary>
  /// The total amount on all Service lines of the document, before Line-level discounts
  /// are applied (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.serviceExtCostTotal))]
  [PXUIField(DisplayName = "Services", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryServiceExtCostTotal { get; set; }

  /// <summary>
  /// The total amount on all Service lines of the document, before Line-level discounts are applied.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? ServiceExtCostTotal { get; set; }

  /// <summary>
  /// The total amount on all Freight lines of the document, before Line-level discounts
  /// are applied (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.freightTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Freight Total")]
  public virtual Decimal? CuryFreightTot { get; set; }

  /// <summary>
  /// The total amount on all Freight lines of the document, before Line-level discounts are applied.
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? FreightTot { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PO.POOrder.detailExtCostTotal">sum</see> of the
  /// <see cref="T:PX.Objects.PO.POOrder.curyGoodsExtCostTotal">goods</see>,
  /// <see cref="T:PX.Objects.PO.POOrder.curyServiceExtCostTotal">services</see> and
  /// the <see cref="T:PX.Objects.PO.POOrder.curyFreightTot">freight amount</see> values.
  /// </summary>
  [PXCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.detailExtCostTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<Add<POOrder.curyGoodsExtCostTotal, POOrder.curyServiceExtCostTotal>, POOrder.curyFreightTot>), typeof (Decimal))]
  [PXFormula(typeof (Add<Add<POOrder.curyGoodsExtCostTotal, POOrder.curyServiceExtCostTotal>, POOrder.curyFreightTot>))]
  [PXUIField(DisplayName = "Detail Total")]
  public virtual Decimal? CuryDetailExtCostTotal { get; set; }

  /// <summary>
  /// The sum of the
  /// <see cref="T:PX.Objects.PO.POOrder.goodsExtCostTotal">goods</see>,
  /// <see cref="T:PX.Objects.PO.POOrder.serviceExtCostTotal">services</see> and
  /// the <see cref="T:PX.Objects.PO.POOrder.freightTot">freight amount</see> values.
  /// </summary>
  [PXDecimal(4)]
  [PXDBCalced(typeof (Add<Add<POOrder.goodsExtCostTotal, POOrder.serviceExtCostTotal>, POOrder.freightTot>), typeof (Decimal))]
  public virtual Decimal? DetailExtCostTotal { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.lineTotal))]
  [PXUIField]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Total")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.taxTotal))]
  [PXUIField(DisplayName = "Tax Total")]
  public virtual Decimal? CuryTaxTotal
  {
    get => this._CuryTaxTotal;
    set => this._CuryTaxTotal = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal
  {
    get => this._TaxTotal;
    set => this._TaxTotal = value;
  }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal
  {
    get => this._CuryVatExemptTotal;
    set => this._CuryVatExemptTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal
  {
    get => this._VatExemptTotal;
    set => this._VatExemptTotal = value;
  }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal
  {
    get => this._CuryVatTaxableTotal;
    set => this._CuryVatTaxableTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal
  {
    get => this._VatTaxableTotal;
    set => this._VatTaxableTotal = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<POOrder.vendorLocationID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<PX.Objects.CR.Location.vTaxCalcMode, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode
  {
    get => this._TaxCalcMode;
    set => this._TaxCalcMode = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.termsID, Where2<FeatureInstalled<FeaturesSet.vendorRelations>, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POOrder.payToVendorID>>, Or2<Not<FeatureInstalled<FeaturesSet.vendorRelations>>, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POOrder.vendorID>>>>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string TermsID { get; set; }

  [PXDBInt]
  [PORemitAddress(typeof (Select2<PX.Objects.CR.Location, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PORemitAddress, On<PORemitAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PORemitAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PORemitAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PORemitAddress.isDefaultAddress, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>>>>))]
  [PXUIField]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  [PXDBInt]
  [PORemitContact(typeof (Select2<PX.Objects.CR.Location, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<PORemitContact, On<PORemitContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PORemitContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PORemitContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<PORemitContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>>>>))]
  public virtual int? RemitContactID
  {
    get => this._RemitContactID;
    set => this._RemitContactID = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<boolTrue>>>))]
  [PXUIField]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POOrder.sOOrderType>>>>))]
  [PXFormula(typeof (Default<POOrder.sOOrderType>))]
  [PXUIField]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string BLType
  {
    get => this._BLType;
    set => this._BLType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string BLOrderNbr
  {
    get => this._BLOrderNbr;
    set => this._BLOrderNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Requisition Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (RQRequisition.reqNbr))]
  public virtual string RQReqNbr
  {
    get => this._RQReqNbr;
    set => this._RQReqNbr = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string OrderDesc
  {
    get => this._OrderDesc;
    set => this._OrderDesc = value;
  }

  [PXDBString(2, IsFixed = true)]
  [POOrderType.List]
  [PXUIField(DisplayName = "Originating PO Type", Enabled = false, FieldClass = "DropShipments")]
  public virtual string OriginalPOType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Originating PO Nbr.", Enabled = false, FieldClass = "DropShipments")]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Current<POOrder.originalPOType>>>>), ValidateValue = false)]
  public virtual string OriginalPONbr { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Normal PO Nbr.", Enabled = false, FieldClass = "DropShipments")]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<POOrderType.regularOrder>>>), ValidateValue = false)]
  public virtual string SuccessorPONbr { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public virtual int? DropShipLinesCount { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  public virtual int? DropShipLinkedLinesCount { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public virtual int? DropShipActiveLinksCount { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  public virtual int? DropShipOpenLinesCntr { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  public virtual int? DropShipNotLinkedLinesCntr { get; set; }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? IsLegacyDropShip { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBString(1, IsFixed = true)]
  [POShippingDestination.List]
  [PXFormula(typeof (Switch<Case<Where<Current<POOrder.orderType>, Equal<POOrderType.dropShip>>, POShippingDestination.customer, Case<Where<Current<POOrder.orderType>, Equal<POOrderType.projectDropShip>>, POShippingDestination.projectSite, Case<Where<Selector<POOrder.vendorLocationID, PX.Objects.CR.Location.vSiteID>, IsNotNull, Or<Where<Current<POOrder.vendorLocationID>, IsNotNull, And<Current<POSetup.shipDestType>, Equal<POShipDestType.site>>>>>, POShippingDestination.site>>>, POShippingDestination.company>))]
  [PXUIField(DisplayName = "Shipping Destination Type")]
  public virtual string ShipDestType
  {
    get => this._ShipDestType;
    set => this._ShipDestType = value;
  }

  [Site]
  [PXDefault(null, typeof (Coalesce<Search2<LocationBranchSettings.vSiteID, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<LocationBranchSettings.vSiteID>>>, Where<Current<POOrder.shipDestType>, Equal<POShippingDestination.site>, And<LocationBranchSettings.bAccountID, Equal<Current<POOrder.vendorID>>, And<LocationBranchSettings.locationID, Equal<Current<POOrder.vendorLocationID>>, And<LocationBranchSettings.branchID, Equal<Current<POOrder.branchID>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>>, Search2<PX.Objects.CR.Location.vSiteID, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<PX.Objects.CR.Location.vSiteID>>>, Where<Current<POOrder.shipDestType>, Equal<POShippingDestination.site>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>, Search<PX.Objects.IN.INSite.siteID, Where<Current<POOrder.shipDestType>, Equal<POShippingDestination.site>, And<PX.Objects.IN.INSite.active, Equal<True>, And<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And2<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POOrder.branchID>>>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>>>))]
  [PXForeignReference(typeof (POOrder.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXString(150, IsUnicode = true)]
  public virtual string SiteIdErrorMessage { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search2<BAccount2.bAccountID, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccount2.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<BAccount2.bAccountID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccount2.bAccountID>, And<Match<PX.Objects.GL.Branch, Current<AccessInfo.userName>>>>>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<Optional<POOrder.shipDestType>, Equal<POShippingDestination.vendor>, And2<Where<BAccount2.type, In3<BAccountType.vendorType, BAccountType.combinedType>, Or<Optional<POOrder.orderType>, NotEqual<POOrderType.dropShip>>>, Or<Where<PX.Objects.GL.Branch.bAccountID, IsNotNull, And<Optional<POOrder.shipDestType>, Equal<POShippingDestination.company>, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Optional<POOrder.shipDestType>, Equal<POShippingDestination.customer>>>>>>>>>>>), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.type), typeof (PX.Objects.CR.BAccount.acctReferenceNbr), typeof (PX.Objects.CR.BAccount.parentBAccountID)}, SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName), CacheGlobal = true)]
  [PXUIField(DisplayName = "Ship To")]
  [PXDefault(null, typeof (Search<PX.Objects.GL.Branch.bAccountID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<POOrder.branchID>>, And<Optional<POOrder.shipDestType>, Equal<POShippingDestination.company>>>>))]
  [PXForeignReference(typeof (POOrder.FK.ShipToAccount))]
  public virtual int? ShipToBAccountID
  {
    get => this._ShipToBAccountID;
    set => this._ShipToBAccountID = value;
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.shipToBAccountID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(null, typeof (Search<BAccount2.defLocationID, Where<BAccount2.bAccountID, Equal<Optional<POOrder.shipToBAccountID>>>>))]
  [PXUIField(DisplayName = "Shipping Location")]
  [PXForeignReference(typeof (Field<POOrder.shipToLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>))]
  public virtual int? ShipToLocationID
  {
    get => this._ShipToLocationID;
    set => this._ShipToLocationID = value;
  }

  [PXDBInt]
  [POShipAddress(typeof (Select2<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Standalone.Location.defAddressID>, And<Current<POOrder.shipDestType>, NotEqual<POShippingDestination.site>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<POOrder.shipToBAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<POOrder.shipToLocationID>>>>>>>, LeftJoin<POShipAddress, On<POShipAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<POShipAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<POShipAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<POShipAddress.isDefaultAddress, Equal<boolTrue>>>>>>>, Where<True, Equal<True>>>))]
  [PXUIField]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  [PXDBInt]
  [POShipContact(typeof (Select2<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Standalone.Location.defContactID>, And<Current<POOrder.shipDestType>, NotEqual<POShippingDestination.site>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<POOrder.shipToBAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<POOrder.shipToLocationID>>>>>>>, LeftJoin<POShipContact, On<POShipContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<POShipContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<POShipContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<POShipContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<True, Equal<True>>>))]
  [PXUIField]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Open Quantity", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenOrderQty { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.unbilledOrderTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
  public virtual Decimal? CuryUnbilledOrderTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledOrderTotal { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.unbilledLineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Line Total", Enabled = false)]
  public virtual Decimal? CuryUnbilledLineTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledLineTotal { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.unbilledTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Tax Total", Enabled = false)]
  public virtual Decimal? CuryUnbilledTaxTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxTotal { get; set; }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Unbilled Quantity")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledOrderQty { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.bAccountID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXFormula(typeof (Selector<POOrder.ownerID, OwnerAttribute.Owner.employeeBAccountID>))]
  [PXEPEmployeeSelector]
  [PXUIField]
  [Obsolete]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search5<EPCompanyTree.workGroupID, InnerJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.workGroupID, Equal<EPCompanyTree.workGroupID>>, InnerJoin<PX.Objects.EP.EPEmployee, On<EPCompanyTreeMember.contactID, Equal<PX.Objects.EP.EPEmployee.defContactID>>>>, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<POOrder.ownerID>>>, Aggregate<GroupBy<EPCompanyTree.workGroupID, GroupBy<EPCompanyTree.description>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Workgroup ID", Enabled = false)]
  public virtual int? OwnerWorkgroupID
  {
    get => this._OwnerWorkgroupID;
    set => this._OwnerWorkgroupID = value;
  }

  [PXInt]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Approval Workgroup ID", Enabled = false)]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [PXDefault(typeof (AccessInfo.contactID))]
  [Owner(typeof (POOrder.ownerWorkgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Do Not Print")]
  public virtual bool? DontPrint
  {
    get => this._DontPrint;
    set => this._DontPrint = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Printed")]
  public virtual bool? Printed
  {
    get => this._Printed;
    set => this._Printed = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Do Not Email")]
  public virtual bool? DontEmail
  {
    get => this._DontEmail;
    set => this._DontEmail = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Emailed")]
  public virtual bool? Emailed
  {
    get => this._Emailed;
    set => this._Emailed = value;
  }

  [PXBool]
  public virtual bool? PrintedExt
  {
    [PXDependsOnFields(new System.Type[] {typeof (POOrder.dontPrint), typeof (POOrder.printed)})] get
    {
      return new bool?(this._DontPrint.GetValueOrDefault() || this._Printed.GetValueOrDefault());
    }
  }

  [PXBool]
  public virtual bool? EmailedExt
  {
    [PXDependsOnFields(new System.Type[] {typeof (POOrder.dontEmail), typeof (POOrder.emailed)})] get
    {
      return new bool?(this._DontEmail.GetValueOrDefault() || this._Emailed.GetValueOrDefault());
    }
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (Search<PX.Objects.CS.FOBPoint.fOBPointID>), DescriptionField = typeof (PX.Objects.CS.FOBPoint.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vFOBPointID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>>>>))]
  public virtual string FOBPoint
  {
    get => this._FOBPoint;
    set => this._FOBPoint = value;
  }

  [PXUIField(DisplayName = "Ship Via")]
  [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vCarrierID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>>>>))]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor, whom the AP bill will belong to.
  /// </value>
  [PXFormula(typeof (Validate<POOrder.curyID>))]
  [POOrderPayToVendor(CacheGlobal = true, Filterable = true)]
  [PXDefault]
  [PXForeignReference(typeof (POOrder.FK.PayToVendor))]
  public virtual int? PayToVendorID { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Prepayment Percent")]
  [PXDefault]
  public virtual Decimal? PrepaymentPct { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Weight", Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderWeight
  {
    get => this._OrderWeight;
    set => this._OrderWeight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Volume", Visible = false)]
  public virtual Decimal? OrderVolume
  {
    get => this._OrderVolume;
    set => this._OrderVolume = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LockCommitment { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Apply Retainage", FieldClass = "Retainage")]
  [PXFormula(typeof (Switch<Case<Where<Current<POOrder.orderType>, Equal<POOrderType.dropShip>>, boolFalse>, Selector<POOrder.vendorID, PX.Objects.AP.Vendor.retainageApply>>))]
  public virtual bool? RetainageApply { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Retainage Percent", FieldClass = "Retainage")]
  [PXFormula(typeof (Selector<POOrder.vendorID, PX.Objects.AP.Vendor.retainagePct>))]
  public virtual Decimal? DefRetainagePct { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.lineRetainageTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineRetainageTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineRetainageTotal { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.retainedTaxTotal))]
  [PXUIField(DisplayName = "Tax on Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainedTaxTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedTaxTotal { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.retainedDiscTotal))]
  [PXUIField(DisplayName = "Discount on Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainedDiscTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedDiscTotal { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.retainageTotal))]
  [PXUIField(DisplayName = "Retainage Total", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageTotal { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.prepaidTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Prepayment Total", Enabled = false)]
  public virtual Decimal? CuryPrepaidTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PrepaidTotal { get; set; }

  [PXDBCurrency(typeof (POOrder.curyInfoID), typeof (POOrder.unprepaidTotal))]
  [PXFormula(typeof (Maximum<decimal0, Sub<POOrder.curyUnbilledOrderTotal, POOrder.curyPrepaidTotal>>))]
  [PXUIField(DisplayName = "Unpaid Amount", Enabled = false)]
  public virtual Decimal? CuryUnprepaidTotal { get; set; }

  [PXDBBaseCury]
  public virtual Decimal? UnprepaidTotal { get; set; }

  [ProjectDefault]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInPO, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<Current<PMSetup.visibleInPO>, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<Current<POOrder.orderType>, Equal<POOrderType.projectDropShip>, And<PMProject.nonProject, NotEqual<True>, Or<Current<POOrder.orderType>, NotEqual<POOrderType.projectDropShip>>>>), "Non-Project is not a valid option.", new System.Type[] {})]
  [ProjectBase]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// A Boolean value that shows whether the purchase order contains lines for different projects.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HasMultipleProjects { get; set; }

  [PXDBString(1)]
  [PXFormula(typeof (Switch<Case<Where<POOrder.orderType, NotEqual<POOrderType.projectDropShip>>, Null>, Selector<POOrder.projectID, PMProject.dropshipReceiptProcessing>>))]
  [PXUIField(DisplayName = "Drop-Ship Receipt Processing", Enabled = false)]
  public virtual string DropshipReceiptProcessing { get; set; }

  [PXDBString(1)]
  [PXFormula(typeof (Switch<Case<Where<POOrder.orderType, NotEqual<POOrderType.projectDropShip>>, Null>, Selector<POOrder.projectID, PMProject.dropshipExpenseRecording>>))]
  [PXUIField(DisplayName = "Record Drop-Ship Expenses", Enabled = false)]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXBool]
  [PXFormula(typeof (boolTrue))]
  public virtual bool? UpdateVendorCost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Automatic Discount Update")]
  public virtual bool? DisableAutomaticDiscountCalculation
  {
    get => this._DisableAutomaticDiscountCalculation;
    set => this._DisableAutomaticDiscountCalculation = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<POOrder.vendorLocationID, PX.Objects.CR.Location.vAllowAPBillBeforeReceipt>))]
  [PXUIField(DisplayName = "Allow AP Bill Before Receipt", Enabled = false)]
  public virtual bool? OrderBasedAPBill { get; set; }

  [PXString(1, IsFixed = true)]
  [PXFormula(typeof (Switch<Case<Where<POOrder.orderBasedAPBill, Equal<True>, Or<Where<POOrder.orderType, Equal<POOrderType.projectDropShip>, And<POOrder.dropshipReceiptProcessing, Equal<DropshipReceiptProcessingOption.skipReceipt>>>>>, PX.Objects.PO.POAccrualType.order>, PX.Objects.PO.POAccrualType.receipt>))]
  [PX.Objects.PO.POAccrualType.List]
  [PXUIField(DisplayName = "Billing Based On", Enabled = false)]
  public virtual string POAccrualType { get; set; }

  [PXBool]
  public virtual bool? LinesStatusUpdated { get; set; }

  [PXBool]
  public virtual bool? HasUsedLine { get; set; }

  [PXDBBool]
  [PXFormula(typeof (Where<POOrder.orderType, In3<POOrderType.regularOrder, POOrderType.dropShip>, And<Selector<POOrder.vendorID, PX.Objects.CR.BAccount.isBranch>, Equal<True>>>))]
  [PXDefault]
  public virtual bool? IsIntercompany { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsIntercompanySOCreated { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Related Order Type", Enabled = false, FieldClass = "InterBranch")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  public virtual string IntercompanySOType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Related Order Nbr.", Enabled = false, FieldClass = "InterBranch")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POOrder.intercompanySOType>>>>))]
  public virtual string IntercompanySONbr { get; set; }

  [PXBool]
  public virtual bool? IntercompanySOCancelled { get; set; }

  [PXBool]
  public virtual bool? IntercompanySOWithEmptyInventory { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude from Intercompany Processing", FieldClass = "InterBranch")]
  public virtual bool? ExcludeFromIntercompanyProc { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? SpecialLineCntr { get; set; }

  int? IAssign.WorkgroupID
  {
    get => this.WorkgroupID;
    set => this.WorkgroupID = value;
  }

  /// <summary>
  /// The tax exemption number for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  [PXDefault(typeof (Search2<PX.Objects.CR.Location.cAvalaraExemptionNumber, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<POOrder.branchID>>>>))]
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <summary>
  /// The entity type for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.TX.TXAvalaraCustomerUsageType.ListAttribute" />.
  /// Defaults to the <see cref="P:PX.Objects.CR.Location.CAvalaraCustomerUsageType">Tax Exemption Type</see>
  /// that is specified for the <see cref="P:PX.Objects.PO.POOrder.BranchID">location of the branch</see>.
  /// </value>
  [PXDefault("0", typeof (Search2<PX.Objects.CR.Location.cAvalaraCustomerUsageType, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<POOrder.branchID>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string EntityUsageType { get; set; }

  public class PK : PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>
  {
    public static POOrder Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (POOrder) PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.FindBy(graph, (object) orderType, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POOrder>.By<POOrder.branchID>
    {
    }

    public class RemittanceAddress : 
      PrimaryKeyOf<PORemitAddress>.By<PORemitAddress.addressID>.ForeignKeyOf<POOrder>.By<POOrder.remitAddressID>
    {
    }

    public class RemittanceContact : 
      PrimaryKeyOf<PORemitContact>.By<PORemitContact.contactID>.ForeignKeyOf<POOrder>.By<POOrder.remitContactID>
    {
    }

    public class SOOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<POOrder>.By<POOrder.sOOrderType>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<POOrder>.By<POOrder.sOOrderType, POOrder.sOOrderNbr>
    {
    }

    public class DemandSOOrder : 
      PrimaryKeyOf<DemandSOOrder>.By<DemandSOOrder.orderType, DemandSOOrder.orderNbr>.ForeignKeyOf<POOrder>.By<POOrder.sOOrderType, POOrder.sOOrderNbr>
    {
    }

    public class Requisition : 
      PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.ForeignKeyOf<POOrder>.By<POOrder.rQReqNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<POOrder>.By<POOrder.siteID>
    {
    }

    public class ShipAddress : 
      PrimaryKeyOf<POShipAddress>.By<POShipAddress.addressID>.ForeignKeyOf<POOrder>.By<POOrder.shipAddressID>
    {
    }

    public class ShipContact : 
      PrimaryKeyOf<POContact>.By<POShipContact.contactID>.ForeignKeyOf<POOrder>.By<POOrder.shipContactID>
    {
    }

    public class FOBPoint : 
      PrimaryKeyOf<PX.Objects.CS.FOBPoint>.By<PX.Objects.CS.FOBPoint.fOBPointID>.ForeignKeyOf<POOrder>.By<POOrder.fOBPoint>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<POOrder>.By<POOrder.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<POOrder>.By<POOrder.vendorID, POOrder.vendorLocationID>
    {
    }

    public class BlanketOrder : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POOrder>.By<POOrder.bLType, POOrder.bLOrderNbr>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POOrder>.By<POOrder.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POOrder>.By<POOrder.curyInfoID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<POOrder>.By<POOrder.taxZoneID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<POOrder>.By<POOrder.termsID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<POOrder>.By<POOrder.ownerID>
    {
    }

    public class ShipToAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<POOrder>.By<POOrder.shipToBAccountID>
    {
    }

    public class ShipToLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<POOrder>.By<POOrder.shipToBAccountID, POOrder.shipToLocationID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<POOrder>.By<POOrder.ownerWorkgroupID>
    {
    }

    public class Carrier : 
      PrimaryKeyOf<PX.Objects.CS.Carrier>.By<PX.Objects.CS.Carrier.carrierID>.ForeignKeyOf<POOrder>.By<POOrder.shipVia>
    {
    }

    public class PayToVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POOrder>.By<POOrder.payToVendorID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<POOrder>.By<POOrder.projectID>
    {
    }
  }

  public class Events : PXEntityEventBase<POOrder>.Container<POOrder.Events>
  {
    public PXEntityEvent<POOrder> LinesCompleted;
    public PXEntityEvent<POOrder> LinesClosed;
    public PXEntityEvent<POOrder> LinesReopened;
    public PXEntityEvent<POOrder> LinesLinked;
    public PXEntityEvent<POOrder> LinesUnlinked;
    public PXEntityEvent<POOrder> Printed;
    public PXEntityEvent<POOrder> DoNotPrintChecked;
    public PXEntityEvent<POOrder> DoNotEmailChecked;
    public PXEntityEvent<POOrder> ReleaseChangeOrder;
    public PXEntityEvent<POOrder> OrderDeleted;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.branchID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.orderNbr>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.behavior>
  {
  }

  public abstract class overrideCurrency : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.overrideCurrency>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.vendorID>
  {
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.vendorLocationID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POOrder.orderDate>
  {
  }

  public abstract class expectedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POOrder.expectedDate>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POOrder.expirationDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.status>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.rejected>
  {
  }

  public abstract class requestApproval : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.requestApproval>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.cancelled>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.isTaxValid>
  {
  }

  public abstract class isUnbilledTaxValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.isUnbilledTaxValid>
  {
  }

  /// <summary>
  /// Indicates that taxes were calculated by an external system
  /// </summary>
  public abstract class externalTaxesImportInProgress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.externalTaxesImportInProgress>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POOrder.noteID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POOrder.curyInfoID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.lineCntr>
  {
  }

  public abstract class linesToCloseCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.linesToCloseCntr>
  {
  }

  public abstract class linesToCompleteCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrder.linesToCompleteCntr>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.vendorRefNbr>
  {
  }

  public abstract class curyOrderTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.curyOrderTotal>
  {
  }

  public abstract class orderTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.orderTotal>
  {
  }

  public abstract class curyControlTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyControlTotal>
  {
  }

  public abstract class controlTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.controlTotal>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.orderQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryLineDiscTotal" />
  public abstract class curyLineDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyLineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.LineDiscTotal" />
  public abstract class lineDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.lineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryGroupDiscTotal" />
  public abstract class curyGroupDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyGroupDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.GroupDiscTotal" />
  public abstract class groupDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.groupDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryDocumentDiscTotal" />
  public abstract class curyDocumentDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyDocumentDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.DocumentDiscTotal" />
  public abstract class documentDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.documentDiscTotal>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.discTot>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.curyDiscTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryOrderDiscTotal" />
  public abstract class curyOrderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyOrderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.OrderDiscTotal" />
  public abstract class orderDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.orderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryGoodsExtCostTotal" />
  public abstract class curyGoodsExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyGoodsExtCostTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.GoodsExtCostTotal" />
  public abstract class goodsExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.goodsExtCostTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryServiceExtCostTotal" />
  public abstract class curyServiceExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyServiceExtCostTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.ServiceExtCostTotal" />
  public abstract class serviceExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.serviceExtCostTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryFreightTot" />
  public abstract class curyFreightTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.curyFreightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.FreightTot" />
  public abstract class freightTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.freightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.CuryDetailExtCostTotal" />
  public abstract class curyDetailExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyDetailExtCostTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POOrder.DetailExtCostTotal" />
  public abstract class detailExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.detailExtCostTotal>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.lineTotal>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.taxTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.vatTaxableTotal>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.taxCalcMode>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.termsID>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.remitContactID>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.sOOrderNbr>
  {
  }

  public abstract class bLType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.bLType>
  {
  }

  public abstract class bLOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.bLOrderNbr>
  {
  }

  public abstract class rQReqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.rQReqNbr>
  {
  }

  public abstract class orderDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.orderDesc>
  {
  }

  public abstract class originalPOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.originalPOType>
  {
  }

  public abstract class originalPONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.originalPONbr>
  {
  }

  public abstract class successorPONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.successorPONbr>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public abstract class dropShipLinesCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.dropShipLinesCount>
  {
  }

  public abstract class dropShipLinkedLinesCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrder.dropShipLinkedLinesCount>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public abstract class dropShipActiveLinksCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrder.dropShipActiveLinksCount>
  {
  }

  public abstract class dropShipOpenLinesCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrder.dropShipOpenLinesCntr>
  {
  }

  public abstract class dropShipNotLinkedLinesCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrder.dropShipNotLinkedLinesCntr>
  {
  }

  public abstract class isLegacyDropShip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.isLegacyDropShip>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POOrder.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POOrder.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POOrder.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POOrder.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POOrder.lastModifiedDateTime>
  {
  }

  public abstract class shipDestType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.shipDestType>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.siteID>
  {
  }

  public abstract class siteIdErrorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.siteIdErrorMessage>
  {
  }

  public abstract class shipToBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.shipToBAccountID>
  {
  }

  public abstract class shipToLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.shipToLocationID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.shipContactID>
  {
  }

  public abstract class vendorID_Vendor_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.vendorID_Vendor_acctName>
  {
  }

  public abstract class openOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.openOrderQty>
  {
  }

  public abstract class curyUnbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyUnbilledOrderTotal>
  {
  }

  public abstract class unbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.unbilledOrderTotal>
  {
  }

  public abstract class curyUnbilledLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyUnbilledLineTotal>
  {
  }

  public abstract class unbilledLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.unbilledLineTotal>
  {
  }

  public abstract class curyUnbilledTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyUnbilledTaxTotal>
  {
  }

  public abstract class unbilledTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.unbilledTaxTotal>
  {
  }

  public abstract class unbilledOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.unbilledOrderQty>
  {
  }

  [Obsolete]
  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.employeeID>
  {
  }

  public abstract class ownerWorkgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.ownerWorkgroupID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.ownerID>
  {
  }

  public abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.dontPrint>
  {
  }

  public abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.printed>
  {
  }

  public abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.dontEmail>
  {
  }

  public abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.emailed>
  {
  }

  public abstract class printedExt : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.printedExt>
  {
  }

  public abstract class emailedExt : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.emailedExt>
  {
  }

  public abstract class fOBPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.fOBPoint>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.shipVia>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.payToVendorID>
  {
  }

  public abstract class prepaymentPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.prepaymentPct>
  {
  }

  public abstract class orderWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.orderWeight>
  {
  }

  public abstract class orderVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.orderVolume>
  {
  }

  public abstract class lockCommitment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.lockCommitment>
  {
  }

  public abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.retainageApply>
  {
  }

  public abstract class defRetainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.defRetainagePct>
  {
  }

  public abstract class curyLineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyLineRetainageTotal>
  {
  }

  public abstract class lineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.lineRetainageTotal>
  {
  }

  public abstract class curyRetainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyRetainedTaxTotal>
  {
  }

  public abstract class retainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.retainedTaxTotal>
  {
  }

  public abstract class curyRetainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyRetainedDiscTotal>
  {
  }

  public abstract class retainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.retainedDiscTotal>
  {
  }

  public abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyRetainageTotal>
  {
  }

  public abstract class retainageTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.retainageTotal>
  {
  }

  public abstract class curyPrepaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyPrepaidTotal>
  {
  }

  public abstract class prepaidTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.prepaidTotal>
  {
  }

  public abstract class curyUnprepaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.curyUnprepaidTotal>
  {
  }

  public abstract class unprepaidTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrder.unprepaidTotal>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrder.projectID>
  {
  }

  public abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.hasMultipleProjects>
  {
  }

  public abstract class dropshipReceiptProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.dropshipReceiptProcessing>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.dropshipExpenseRecording>
  {
  }

  public abstract class updateVendorCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.updateVendorCost>
  {
  }

  public abstract class disableAutomaticDiscountCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.disableAutomaticDiscountCalculation>
  {
  }

  public abstract class orderBasedAPBill : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.orderBasedAPBill>
  {
  }

  public abstract class pOAccrualType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.pOAccrualType>
  {
  }

  public abstract class linesStatusUpdated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.linesStatusUpdated>
  {
  }

  public abstract class hasUsedLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.hasUsedLine>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POOrder.isIntercompany>
  {
  }

  public abstract class isIntercompanySOCreated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.isIntercompanySOCreated>
  {
  }

  public abstract class intercompanySOType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.intercompanySOType>
  {
  }

  public abstract class intercompanySONbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.intercompanySONbr>
  {
  }

  public abstract class intercompanySOCancelled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.intercompanySOCancelled>
  {
  }

  public abstract class intercompanySOWithEmptyInventory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.intercompanySOWithEmptyInventory>
  {
  }

  public abstract class excludeFromIntercompanyProc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrder.excludeFromIntercompanyProc>
  {
  }

  public abstract class specialLineCntr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrder.specialLineCntr>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrder.externalTaxExemptionNumber>
  {
  }

  public abstract class entityUsageType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrder.entityUsageType>
  {
  }
}
