// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Archiving;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.RelatedItems;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO.Attributes;
using PX.Objects.SO.Interfaces;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.SO;

/// <summary>Represents sales order and transfer order documents.</summary>
/// <remarks>
/// The records of this type are created and edited on:
/// <list type="bullet">
/// <item><description>The <i>Sales Orders (SO301000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph)</description></item>
/// <item><description>The <i>Process Orders (SO501000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOCreateShipment" /> graph)</description></item>
/// <item><description>The <i>Print/Email Orders (SO502000)</i> form (corresponds to the <see cref="T:PX.Objects.SO.SOOrderProcess" /> graph)</description></item>
/// </list>
/// </remarks>
[PXPrimaryGraph(typeof (SOOrderEntry))]
[PXGroupMask(typeof (LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<SOOrder.customerID>>>), WhereRestriction = typeof (Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>))]
[PXCacheName("Sales Order")]
[DebuggerDisplay("OrderType = {OrderType}, OrderNbr = {OrderNbr}")]
public class SOOrder : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  IFreightBase,
  IInvoice,
  PX.Objects.CM.IRegister,
  IDocumentKey,
  ICreatePaymentDocument,
  ISubstitutableDocument,
  INotable
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _OrderType;
  protected string _Behavior;
  protected string _ARDocType;
  protected string _OrderNbr;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected int? _BranchID;
  protected DateTime? _OrderDate;
  protected string _CustomerOrderNbr;
  protected string _CustomerRefNbr;
  protected DateTime? _CancelDate;
  protected DateTime? _RequestDate;
  protected DateTime? _ShipDate;
  protected bool? _DontApprove;
  protected bool? _Hold;
  protected bool? _Approved;
  protected bool? _Rejected = new bool?(false);
  protected bool? _CreditHold;
  protected bool? _Completed;
  protected bool? _Cancelled;
  protected bool? _OpenDoc;
  protected bool? _ShipmentDeleted;
  protected bool? _BackOrdered;
  protected int? _LastSiteID;
  protected DateTime? _LastShipDate;
  protected bool? _BillSeparately;
  protected bool? _ShipSeparately;
  protected string _Status;
  protected Guid? _NoteID;
  protected int? _LineCntr;
  protected int? _BilledCntr;
  protected int? _ReleasedCntr;
  protected int? _PaymentCntr;
  protected string _OrderDesc;
  protected int? _BillAddressID;
  protected int? _ShipAddressID;
  protected int? _BillContactID;
  protected int? _ShipContactID;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _CuryOrderTotal;
  protected Decimal? _OrderTotal;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryPremiumFreightAmt;
  protected Decimal? _PremiumFreightAmt;
  protected Decimal? _CuryFreightCost;
  protected Decimal? _FreightCost;
  protected bool? _FreightCostIsValid;
  protected bool? _IsPackageValid;
  protected Decimal? _CuryFreightAmt;
  protected Decimal? _FreightAmt;
  protected Decimal? _CuryFreightTot;
  /// <summary>
  /// The sum of the <see cref="T:PX.Objects.SO.SOOrder.premiumFreightAmt">additional freight charges</see> and
  /// the <see cref="T:PX.Objects.SO.SOOrder.freightAmt">freight amount</see> values.
  /// </summary>
  protected Decimal? _FreightTot;
  protected string _FreightTaxCategoryID;
  protected Decimal? _CuryMiscTot;
  protected Decimal? _MiscTot;
  protected Decimal? _OrderQty;
  protected Decimal? _OrderWeight;
  protected Decimal? _OrderVolume;
  protected Decimal? _CuryOpenOrderTotal;
  protected Decimal? _OpenOrderTotal;
  protected Decimal? _CuryOpenLineTotal;
  protected Decimal? _OpenLineTotal;
  protected Decimal? _CuryOpenTaxTotal;
  protected Decimal? _OpenTaxTotal;
  protected Decimal? _OpenOrderQty;
  protected Decimal? _CuryUnbilledOrderTotal;
  protected Decimal? _UnbilledOrderTotal;
  protected Decimal? _CuryUnbilledLineTotal;
  protected Decimal? _UnbilledLineTotal;
  protected Decimal? _CuryUnbilledMiscTot;
  protected Decimal? _UnbilledMiscTot;
  protected Decimal? _CuryUnbilledTaxTotal;
  protected Decimal? _UnbilledTaxTotal;
  protected Decimal? _UnbilledOrderQty;
  protected Decimal? _CuryControlTotal;
  protected Decimal? _ControlTotal;
  protected Decimal? _CuryPaymentTotal;
  protected Decimal? _PaymentTotal;
  protected bool? _OverrideTaxZone;
  protected string _TaxZoneID;
  protected string _AvalaraCustomerUsageType;
  protected int? _ProjectID;
  protected string _ShipComplete;
  protected string _FOBPoint;
  protected string _ShipVia;
  protected int? _PackageLineCntr;
  protected Decimal? _PackageWeight;
  protected bool? _UseCustomerAccount;
  protected bool? _Resedential;
  protected bool? _SaturdayDelivery;
  protected bool? _GroundCollect;
  protected bool? _Insurance;
  protected short? _Priority;
  protected int? _SalesPersonID;
  /// <summary>The default commission percentage of the salesperson.</summary>
  protected Decimal? _CommnPct;
  protected string _TermsID;
  protected DateTime? _DueDate;
  protected DateTime? _DiscDate;
  protected string _InvoiceNbr;
  protected DateTime? _InvoiceDate;
  protected string _FinPeriodID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected int? _EmployeeID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected Decimal? _CuryTermsDiscAmt = new Decimal?(0M);
  protected Decimal? _TermsDiscAmt = new Decimal?(0M);
  protected string _ShipTermsID;
  protected string _ShipZoneID;
  protected bool? _InclCustOpenOrders;
  protected int? _ShipmentCntr;
  protected int? _OpenShipmentCntr;
  protected int? _OpenLineCntr;
  protected int? _DefaultSiteID;
  protected int? _DestinationSiteID;
  protected string _DefaultOperation;
  protected string _OrigOrderType;
  protected string _OrigOrderNbr;
  protected Decimal? _ManDisc;
  protected Decimal? _CuryManDisc;
  protected bool? _ApprovedCredit;
  protected Decimal? _ApprovedCreditAmt;
  protected string _PaymentMethodID;
  protected int? _PMInstanceID;
  protected int? _CashAccountID;
  protected string _ExtRefNbr;
  protected bool? _IsManualPackage = new bool?(false);
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected bool? _DisableAutomaticDiscountCalculation;

  /// <summary>The number of risks of the order.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? RiskLineCntr { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the record is selected for
  /// processing by a user.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// The type of the document, which is a part of the identifier of the order.
  /// The identifier of the <see cref="T:PX.Objects.SO.SOOrderType">order type</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.OrderType" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOOrderType.orderType" /> field.
  /// </value>
  /// <remarks>
  /// The type of the document, which is one of the predefined order types or a custom order type created by
  /// using the Order Types (SO201000) form.
  /// </remarks>
  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa")]
  [PXDefault("SO", typeof (SOSetup.defaultOrderType))]
  [PXSelector(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On2<SOOrderTypeOperation.FK.OrderType, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>>, Where<BqlChainableConditionLite<FeatureInstalled<FeaturesSet.inventory>>.Or<BqlOperand<SOOrderType.behavior, IBqlString>.IsNotEqual<SOBehavior.bL>>>>))]
  [PXRestrictor(typeof (Where<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, Or<FeatureInstalled<FeaturesSet.warehouse>>>), "'{0}' cannot be found in the system.", new System.Type[] {typeof (SOOrderType.orderType)})]
  [PXRestrictor(typeof (Where<SOOrderType.requireAllocation, NotEqual<True>, Or<AllocationAllowed>>), "'{0}' cannot be found in the system.", new System.Type[] {typeof (SOOrderType.orderType)})]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), null, new System.Type[] {})]
  [PXUIField]
  [PXFieldDescription]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  /// <summary>
  /// The behavior, which is defined by <see cref="P:PX.Objects.SO.SOOrder.OrderType" /> (which is the link to <see cref="T:PX.Objects.SO.SOOrderType" />).
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.SO.SOBehavior" />.
  /// </value>
  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Search<SOOrderType.behavior, Where<SOOrderType.orderType, Equal<Current<SOOrder.orderType>>>>))]
  [PXUIField(DisplayName = "Behavior", Enabled = false, IsReadOnly = true)]
  [SOBehavior.List]
  public virtual string Behavior
  {
    get => this._Behavior;
    set => this._Behavior = value;
  }

  /// <summary>
  /// The type of an accounts receivable document to be generated on release of a document of this type.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Invoice" /> foreign key.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.AR.ARDocType" />.
  /// </value>
  [PXString(3, IsFixed = true)]
  [PXFormula(typeof (Selector<SOOrder.orderType, SOOrderType.aRDocType>))]
  public virtual string ARDocType
  {
    get => this._ARDocType;
    set => this._ARDocType = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the order is used only for an invoice.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.aRDocType, NotEqual<PX.Objects.AR.ARDocType.noUpdate>, And<Selector<SOOrder.orderType, SOOrderType.requireShipping>, Equal<False>>>))]
  public virtual bool? IsInvoiceOrder { get; set; }

  /// <summary>The unique reference number of the order.</summary>
  /// <remarks>
  /// When the new sales order is saved for the first time, the system automatically generates
  /// this number by using the numbering sequence assigned to orders of <see cref="T:PX.Objects.SO.SOOrderType" />.
  /// </remarks>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PX.Objects.SO.SO.RefNbr(typeof (Search2<SOOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>, And<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where<SOOrder.orderType, Equal<Optional<SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, Or<Exists<Select<SOOrderType, Where<SOOrderType.orderType, Equal<SOOrder.orderType>, And<SOOrderType.aRDocType, Equal<PX.Objects.AR.ARDocType.noUpdate>, And<SOOrderType.behavior, In3<SOBehavior.sO, SOBehavior.tR>>>>>>>>>>, OrderBy<Desc<SOOrder.orderNbr>>>), Filterable = true)]
  [PX.Objects.SO.SO.Numbering]
  [PXFieldDescription]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer">customer</see>. The field is a part of the identifier of the
  /// <see cref="T:PX.Objects.CR.Location">customer location</see>.
  /// The field is included in the foreign keys <see cref="T:PX.Objects.SO.SOOrder.FK.Customer" /> and <see cref="T:PX.Objects.SO.SOOrder.FK.CustomerLocation" />.
  /// </summary>
  /// <value>
  /// For a customer, the value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.Customer.bAccountID" />
  /// field.
  /// For a customer location, the value of this field corresponds to the value of the
  /// <see cref="T:PX.Objects.CR.Location.bAccountID" /> field.
  /// </value>
  [PXDefault]
  [Customer(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
  [CustomerOrOrganizationInNoUpdateDocRestrictor]
  [PXRestrictor(typeof (Where<Optional<SOOrder.isTransferOrder>, Equal<True>, Or<PX.Objects.AR.Customer.status, IsNull, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.active>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.oneTime>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.creditHold>>>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.AR.Customer.status)})]
  [PXForeignReference(typeof (Field<SOOrder.customerID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Location">customer location</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.CustomerLocation" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.Location.locationID" /> field.
  /// </value>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>))]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<SOOrder.customerID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<SOOrder.customerLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.Contact">contact</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Contact" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.Contact.contactID" /> field.
  /// </value>
  [ContactRaw(typeof (SOOrder.customerID), WithContactDefaultingByBAccount = true)]
  [PXUIEnabled(typeof (Where<SOOrder.customerID, IsNotNull>))]
  public virtual int? ContactID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Branch" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Branch.branchID" /> field.
  /// </value>
  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<SOOrder.customerLocationID, IsNotNull, And<Selector<SOOrder.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<SOOrder.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<Current2<SOOrder.branchID>, IsNotNull>, Current2<SOOrder.branchID>>>, Current<AccessInfo.branchID>>))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The date of the document.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  /// <summary>
  /// The reference number of the original customer document that the sales order is based on.
  /// </summary>
  /// <remarks>
  /// A reference number must be specified if the
  /// <see cref="T:PX.Objects.SO.SOOrderType.customerOrderIsRequired">Require Customer Order Nbr</see> field is
  /// <see langword="true" /> for the order type.
  /// This field is available for orders of the <see cref="T:PX.Objects.SO.SOBehavior.tR">TR type</see>.
  /// </remarks>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string CustomerOrderNbr
  {
    get => this._CustomerOrderNbr;
    set => this._CustomerOrderNbr = value;
  }

  /// <summary>
  /// The reference number of the sales order in a third-party application if Acumatica ERP is integrated with
  /// such an application and imports the sales orders from it.
  /// </summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "External Reference")]
  public virtual string CustomerRefNbr
  {
    get => this._CustomerRefNbr;
    set => this._CustomerRefNbr = value;
  }

  /// <summary>
  /// The expiration date of the order, by which the order can be selected for canceling on
  /// the Process Orders (SO501000) form.
  /// </summary>
  [PXDBDate]
  [PXFormula(typeof (Switch<Case<Where<MaxDate, Less<Add<SOOrder.orderDate, Selector<SOOrder.orderType, SOOrderType.daysToKeep>>>>, MaxDate>, Add<SOOrder.orderDate, Selector<SOOrder.orderType, SOOrderType.daysToKeep>>>))]
  [PXUIField]
  public virtual DateTime? CancelDate
  {
    get => this._CancelDate;
    set => this._CancelDate = value;
  }

  /// <summary>
  /// The date when the customer wants to receive the goods.
  /// </summary>
  /// <remarks>
  /// This date provides the default values for the <see cref="T:PX.Objects.SO.SOLine.requestDate" /> dates for order lines.
  /// </remarks>
  /// <value>
  /// The default value is the <see cref="T:PX.Data.AccessInfo.businessDate">current business date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? RequestDate
  {
    get => this._RequestDate;
    set => this._RequestDate = value;
  }

  /// <summary>
  /// The date when the ordered goods are scheduled to be shipped.
  /// </summary>
  /// <remarks>
  /// By default, it is the date that is specified in <see cref="T:PX.Objects.SO.SOOrder.requestDate" /> minus the number of lead days,
  /// but it is not earlier than the <see cref="T:PX.Data.AccessInfo.businessDate">current business date</see>.
  /// </remarks>
  [PXDBDate]
  [PXFormula(typeof (DateMinusDaysNotLessThenDate<SOOrder.requestDate, IsNull<Selector<Current<SOOrder.customerLocationID>, PX.Objects.CR.Location.cLeadTime>, decimal0>, SOOrder.orderDate>))]
  [PXUIField]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that the order does not need to
  /// be approved.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(true)]
  [PXUIField(DisplayName = "Don't Approve", Visible = false, Enabled = false)]
  public virtual bool? DontApprove
  {
    get => this._DontApprove;
    set => this._DontApprove = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the order is on hold.
  /// </summary>
  /// <remarks>
  /// If the order is on hold, additions and changes can be made and order quantities do not affect the item
  /// availability.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false, typeof (Search<SOOrderType.holdEntry, Where<SOOrderType.orderType, Equal<Current<SOOrder.orderType>>>>))]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the sales order has been approved.
  /// </summary>
  /// <remarks>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.approvalWorkflow">Approval Workflow</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the order was rejected by one of the persons assigned to approve it.
  /// </summary>
  /// <remarks>
  /// This status is available for orders of the SO, SA, CM, CS, TR, QT, and IN order types only
  /// if the <see cref="T:PX.Objects.CS.FeaturesSet.approvalWorkflow">Approval Workflow</see>feature is enabled on
  /// the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXBool]
  [PXDefault(false)]
  public bool? Rejected
  {
    get => this._Rejected;
    set => this._Rejected = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the document has been emailed to the
  /// <see cref="P:PX.Objects.SO.SOOrder.CustomerID">customer</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Emailed")]
  public virtual bool? Emailed { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the document has been printed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Printed")]
  public virtual bool? Printed { get; set; }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that the customer has failed the credit
  /// check, which the system performed when the order was taken off hold.
  /// </summary>
  /// <remarks>
  /// An order with this status can be saved with only the Credit Hold or On Hold status if the
  /// <see cref="T:PX.Objects.SO.SOOrderType.creditHoldEntry">Hold Document on Failed Credit Check</see> field is
  /// <see langword="true" /> for the order type.
  /// This status is available for orders of the QT, SO, SA, IN, and CM order types.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Credit Hold")]
  public virtual bool? CreditHold
  {
    get => this._CreditHold;
    set => this._CreditHold = value;
  }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that all related inventory documents
  /// required for the order type have been generated and released.
  /// </summary>
  /// <remarks>
  /// Completed orders of the QT order type can be opened again.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed")]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the order has been canceled on the
  /// date specified in <see cref="P:PX.Objects.SO.SOOrder.CancelDate" />.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Canceled")]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the order has unbilled amount.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? OpenDoc
  {
    get => this._OpenDoc;
    set => this._OpenDoc = value;
  }

  /// <exclude />
  [PXBool]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public virtual bool? ShipmentDeleted
  {
    get => this._ShipmentDeleted;
    set => this._ShipmentDeleted = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the order can not be shipped because the specified items are not
  /// available.
  /// </summary>
  /// <remarks>
  /// <para>This status can be assigned to an open order manually in the following case: When a user attempts
  /// to create a shipment, the system detects that the order cannot be shipped in full and displays a message
  /// about this.</para>
  /// <para>This status can be assigned to orders automatically when shipment for order is created if the
  /// shipping rules specified on the document level and on the line level do not allow shipment creation.</para>
  /// <para>This status is available for sales orders of the SO and SA types.</para>
  /// </remarks>
  [PXBool]
  [PXUIField(DisplayName = "BackOrdered")]
  public virtual bool? BackOrdered
  {
    get => this._BackOrdered;
    set => this._BackOrdered = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSite">warehouse</see> of the last confirmed shipment.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.LastSite" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.INSite.siteID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(DisplayName = "Last Shipment Site")]
  public virtual int? LastSiteID
  {
    get => this._LastSiteID;
    set => this._LastSiteID = value;
  }

  /// <summary>The date of the last confirmed shipment.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Last Shipment Date")]
  public virtual DateTime? LastShipDate
  {
    get => this._LastShipDate;
    set => this._LastShipDate = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the document should be billed separately (that is, it requires a
  /// separate invoice).
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBBool]
  [PXDefault(typeof (Search<SOOrderType.billSeparately, Where<SOOrderType.orderType, Equal<Current<SOOrder.orderType>>>>))]
  [PXUIField(DisplayName = "Bill Separately")]
  public virtual bool? BillSeparately
  {
    get => this._BillSeparately;
    set => this._BillSeparately = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the goods for the customer should be shipped separately for each
  /// sales order.
  /// </summary>
  /// <remarks>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBBool]
  [PXDefault(typeof (Search<SOOrderType.shipSeparately, Where<SOOrderType.orderType, Equal<Current<SOOrder.orderType>>>>))]
  [PXUIField(DisplayName = "Ship Separately")]
  public virtual bool? ShipSeparately
  {
    get => this._ShipSeparately;
    set => this._ShipSeparately = value;
  }

  /// <summary>The status of the order.</summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.SO.SOOrderStatus" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [SOOrderStatus.List]
  [PXDefault]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// The value corresponds to the <see cref="T:PX.Data.Note.noteID">Note.NoteID</see> field.
  /// </value>
  [PXSearchable(256 /*0x0100*/, "{0}: {1} - {3}", new System.Type[] {typeof (SOOrder.orderType), typeof (SOOrder.orderNbr), typeof (SOOrder.customerID), typeof (PX.Objects.AR.Customer.acctName)}, new System.Type[] {typeof (SOOrder.customerRefNbr), typeof (SOOrder.customerOrderNbr), typeof (SOOrder.orderDesc)}, NumberFields = new System.Type[] {typeof (SOOrder.orderNbr)}, Line1Format = "{0:d}{1}{2}{3}", Line1Fields = new System.Type[] {typeof (SOOrder.orderDate), typeof (SOOrder.status), typeof (SOOrder.customerRefNbr), typeof (SOOrder.customerOrderNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (SOOrder.orderDesc)}, MatchWithJoin = typeof (InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<SOOrder.customerID>>>), SelectForFastIndexing = typeof (Select2<SOOrder, LeftJoin<PX.Objects.AR.Customer, On<SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<SOOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>, And<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, Or<Exists<Select<SOOrderType, Where<SOOrderType.orderType, Equal<SOOrder.orderType>, And<SOOrderType.aRDocType, Equal<PX.Objects.AR.ARDocType.noUpdate>>>>>>>, OrderBy<Desc<SOOrder.orderNbr>>>))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>The counter of the child lines added to the order.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  /// <summary>
  /// The counter of the shipments added to the order that have an invoice.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BilledCntr
  {
    get => this._BilledCntr;
    set => this._BilledCntr = value;
  }

  /// <summary>
  /// The counter of the shipments added to the order that have a released invoice.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ReleasedCntr
  {
    get => this._ReleasedCntr;
    set => this._ReleasedCntr = value;
  }

  /// <summary>
  /// The counter of the shipments added to the order that have a payment.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PaymentCntr
  {
    get => this._PaymentCntr;
    set => this._PaymentCntr = value;
  }

  /// <summary>
  /// The counter of the applied payments in pre-authorized state.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? AuthorizedPaymentCntr { get; set; }

  /// <summary>A brief description of the document.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string OrderDesc
  {
    get => this._OrderDesc;
    set => this._OrderDesc = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOBillingAddress">billing address</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.BillingAddress" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOAddress.addressID" /> field.
  /// </value>
  [PXDBInt]
  [SOBillingAddress(typeof (Select2<BAccountR, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<BAccountR.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defBillAddressID>>>, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNull, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>>>>, LeftJoin<SOBillingAddress, On<SOBillingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<SOBillingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<SOBillingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<SOBillingAddress.isDefaultAddress, Equal<boolTrue>>>>>>>>>, Where<BAccountR.bAccountID, Equal<Current<SOOrder.customerID>>>>))]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOShippingAddress">shipping address</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.ShippingAddress" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOShippingAddress.addressID" /> field.
  /// </value>
  [PXDBInt]
  [SOShippingAddress(typeof (Select2<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Standalone.Location.defAddressID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>>, LeftJoin<SOShippingAddress, On<SOShippingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<SOShippingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<SOShippingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<SOShippingAddress.isDefaultAddress, Equal<True>>>>>>>, Where<True, Equal<True>>>))]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOBillingContact">billing contact</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.BillingContact" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOBillingContact.contactID" /> field.
  /// </value>
  [PXDBInt]
  [SOBillingContact(typeof (Select2<BAccountR, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<BAccountR.bAccountID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNull, And<PX.Objects.CR.Contact.contactID, Equal<BAccountR.defContactID>>>>>>>, LeftJoin<SOBillingContact, On<SOBillingContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<SOBillingContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<SOBillingContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<SOBillingContact.isDefaultContact, Equal<boolTrue>>>>>>>>>, Where<BAccountR.bAccountID, Equal<Current<SOOrder.customerID>>>>))]
  public virtual int? BillContactID
  {
    get => this._BillContactID;
    set => this._BillContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOShippingContact">shipping contact</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.ShippingContact" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOShippingContact.contactID" /> field.
  /// </value>
  [PXDBInt]
  [SOShippingContact(typeof (Select2<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Standalone.Location.defContactID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>>, LeftJoin<SOShippingContact, On<SOShippingContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<SOShippingContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<SOShippingContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<SOShippingContact.isDefaultContact, Equal<True>>>>>>>, Where<True, Equal<True>>>))]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Currency">currency</see> of the document.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Currency" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CM.Currency.curyID" /> field.
  /// </value>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.multicurrency">Multicurrency Accounting</see> feature is enabled on
  /// the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">currency and exchange rate information</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.CurrencyInfo" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CM.CurrencyInfo.curyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The total <see cref="T:PX.Objects.SO.SOOrder.lineDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.lineDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? CuryLineDiscTotal { get; set; }

  /// <summary>
  /// The total line discount of the document, which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? LineDiscTotal { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.SO.SOOrder.groupDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of group discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.groupDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group Discounts", Enabled = false)]
  public virtual Decimal? CuryGroupDiscTotal { get; set; }

  /// <summary>
  /// The total group discount of the document, which is calculated as the sum of group discounts of the order.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group Discounts", Enabled = false)]
  public virtual Decimal? GroupDiscTotal { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.SO.SOOrder.documentDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of document discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.documentDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = false)]
  public virtual Decimal? CuryDocumentDiscTotal { get; set; }

  /// <summary>
  /// The total document discount of the document, which is calculated as the sum of document discounts of the order.
  /// </summary>
  /// <remarks>
  /// <para>If the <see cref="T:PX.Objects.CS.FeaturesSet.customerDiscounts">Customer Discounts</see> feature is not enabled on
  /// the Enable/Disable Features (CS100000) form,
  /// a user can enter a document-level discount manually. This manual discount has no discount code or
  /// sequence and is not recalculated by the system. If the manual discount needs to be changed, a user has to
  /// correct it manually.</para>
  /// <para>This field is not available for blanket sales orders.</para>
  /// </remarks>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = false)]
  public virtual Decimal? DocumentDiscTotal { get; set; }

  /// <summary>
  /// The total group and document discount of the document, which is calculated as the sum of group and document discounts of the order.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Group and Document Discount Total")]
  public virtual Decimal? DiscTot { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.SO.SOOrder.discTot">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of all discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discounts")]
  public virtual Decimal? CuryDiscTot { get; set; }

  /// <summary>
  /// The total <see cref="T:PX.Objects.SO.SOOrder.orderDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of all group, document and line discounts of the order.
  /// </summary>
  [PXCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.orderDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<SOOrder.curyDiscTot, SOOrder.curyLineDiscTotal>), typeof (Decimal))]
  [PXFormula(typeof (Add<SOOrder.curyDiscTot, SOOrder.curyLineDiscTotal>))]
  [PXUIField(DisplayName = "Discount Total", Enabled = false)]
  public virtual Decimal? CuryOrderDiscTotal { get; set; }

  /// <summary>
  /// The total discount of the document, which is calculated as the sum of group, document and line discounts of the order.
  /// </summary>
  /// <remarks>
  /// <para>This field is not available for blanket sales orders.</para>
  /// </remarks>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<SOOrder.discTot, SOOrder.lineDiscTotal>), typeof (Decimal))]
  [PXUIField(DisplayName = "Discount Total")]
  public virtual Decimal? OrderDiscTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.orderTotal">total amount of the document</see> (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.orderTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Order Total")]
  public virtual Decimal? CuryOrderTotal
  {
    get => this._CuryOrderTotal;
    set => this._CuryOrderTotal = value;
  }

  /// <summary>The total amount of the document.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderTotal
  {
    get => this._OrderTotal;
    set => this._OrderTotal = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.lineTotal">total amount</see> on all lines of the document, except for Misc. Charges, after Line-level discounts
  /// are applied (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Total")]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  /// <summary>
  /// The total amount on all lines of the document, except for Misc. Charges, after Line-level discounts are applied.
  /// </summary>
  /// <remarks>
  /// <para>This total is calculated as the sum of the amounts in the
  /// <see cref="T:PX.Objects.SO.SOLine.curyLineAmt">Amount</see> for all stock items and non-stock items that require shipment.
  /// This total does not include the freight amount.</para>
  /// <para>This field is not available for transfer orders.
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  /// <summary>
  /// The document <see cref="T:PX.Objects.SO.SOOrder.vatExemptTotal">total that is exempt</see>
  /// from VAT (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal
  {
    get => this._CuryVatExemptTotal;
    set => this._CuryVatExemptTotal = value;
  }

  /// <summary>The document total that is exempt from VAT.</summary>
  /// <remarks>
  /// <para>This total is calculated as the taxable amount for the tax with the
  /// <see cref="T:PX.Objects.TX.Tax.exemptTax">Include in VAT Exempt Total</see> field is <see langword="true" />.</para>
  /// <para>This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.vATReporting">VAT Reporting</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal
  {
    get => this._VatExemptTotal;
    set => this._VatExemptTotal = value;
  }

  /// <summary>
  /// The document <see cref="T:PX.Objects.SO.SOOrder.vatTaxableTotal">total that is subject</see>
  /// to VAT (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal
  {
    get => this._CuryVatTaxableTotal;
    set => this._CuryVatTaxableTotal = value;
  }

  /// <summary>The document total that is subject to VAT.</summary>
  /// <remarks>
  /// <para>This field is available only if the <see cref="T:PX.Objects.TX.Tax.exemptTax">Include in VAT Exempt Total</see> field
  /// is <see langword="true" />. If the document contains multiple transactions with different taxes applied and
  /// for each of the applied taxes <see cref="T:PX.Objects.TX.Tax.exemptTax">Include in VAT Exempt Total</see> field is
  /// <see langword="true" />, the taxable amount calculated for each line of the document is added to this field.
  /// </para>
  /// <para>This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.vATReporting">VAT Reporting</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal
  {
    get => this._VatTaxableTotal;
    set => this._VatTaxableTotal = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.taxTotal">total amount</see> of tax paid on the document (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Total")]
  public virtual Decimal? CuryTaxTotal
  {
    get => this._CuryTaxTotal;
    set => this._CuryTaxTotal = value;
  }

  /// <summary>The total amount of tax paid on the document.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal
  {
    get => this._TaxTotal;
    set => this._TaxTotal = value;
  }

  /// <summary>
  /// Any <see cref="T:PX.Objects.SO.SOOrder.premiumFreightAmt">additional freight charges</see> for handling the order
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.premiumFreightAmt))]
  [PXUIField(DisplayName = "Premium Freight Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryPremiumFreightAmt
  {
    get => this._CuryPremiumFreightAmt;
    set => this._CuryPremiumFreightAmt = value;
  }

  /// <summary>
  /// Any additional freight charges for handling the order.
  /// </summary>
  /// <remarks>
  /// <para>This field is not available for transfer orders.</para>
  /// <para>To correct the excessive freight charges in a previous order of the customer, premium freight amount
  /// can be manually adjusted.</para>
  /// </remarks>
  [PXDBDecimal(4)]
  public virtual Decimal? PremiumFreightAmt
  {
    get => this._PremiumFreightAmt;
    set => this._PremiumFreightAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.freightCost">freight cost</see> calculated for the document
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.freightCost))]
  [PXUIField(DisplayName = "Freight Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightCost
  {
    get => this._CuryFreightCost;
    set => this._CuryFreightCost = value;
  }

  /// <summary>The freight cost calculated for the document.</summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDecimal(4)]
  public virtual Decimal? FreightCost
  {
    get => this._FreightCost;
    set => this._FreightCost = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the freight rates are up to date.
  /// </summary>
  /// <value>
  /// If the value is set to <see langword="false" />, the sales order has been modified and the rates
  /// should be updated.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Freight Cost Is up-to-date", Enabled = false)]
  public virtual bool? FreightCostIsValid
  {
    get => this._FreightCostIsValid;
    set => this._FreightCostIsValid = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the packages of the orders are calculated correct.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPackageValid
  {
    get => this._IsPackageValid;
    set => this._IsPackageValid = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the <see cref="T:PX.Objects.SO.SOOrder.freightAmt">Freight Price</see> can be changed
  /// manually.
  /// </summary>
  /// <remarks>
  /// The system will preserve the manually entered <see cref="T:PX.Objects.SO.SOOrder.freightAmt">Freight Price</see> value in
  /// the sales order and will not recalculate the value if the quantity, extended price, or amount is modified
  /// in order lines.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Freight Price")]
  public virtual bool? OverrideFreightAmount { get; set; }

  /// <summary>
  /// The document from which the system extracts data to calculate the freight price in the sales order
  /// invoice created for the current sales order.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.CS.FreightAmountSourceAttribute" />.
  /// The default value is <see cref="T:PX.Objects.CS.FreightAmountSourceAttribute.shipmentBased" />.
  /// </value>
  /// <remarks>
  /// If the shipping terms are selected in the <see cref="T:PX.Objects.SO.SOOrder.shipTermsID">Shipping Terms</see> field, the system
  /// fills in this field automatically, based on the value of the
  /// <see cref="T:PX.Objects.CS.ShipTerms.freightAmountSource">Invoice Freight Price Based On</see>.
  /// </remarks>
  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PX.Objects.CS.FreightAmountSource]
  [PXUIField(DisplayName = "Invoice Freight Price Based On", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<SOOrder.overrideFreightAmount, Equal<True>>, FreightAmountSourceAttribute.orderBased>, IsNull<Selector<SOOrder.shipTermsID, PX.Objects.CS.ShipTerms.freightAmountSource>, FreightAmountSourceAttribute.shipmentBased>>))]
  public virtual string FreightAmountSource { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.freightAmt">freight amount</see> calculated in accordance with
  /// the shipping terms (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.freightAmt))]
  [PXUIField(DisplayName = "Freight Price", Enabled = false)]
  [PXUIVerify]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightAmt
  {
    get => this._CuryFreightAmt;
    set => this._CuryFreightAmt = value;
  }

  /// <summary>
  /// The freight amount calculated in accordance with the shipping terms from the
  /// <see cref="T:PX.Objects.SO.SOOrder.shipTermsID">Shipping Terms</see> field.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDecimal(4)]
  public virtual Decimal? FreightAmt
  {
    get => this._FreightAmt;
    set => this._FreightAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.freightTot">sum</see> of the
  /// <see cref="T:PX.Objects.SO.SOOrder.curyPremiumFreightAmt">additional freight charges</see> and
  /// the <see cref="T:PX.Objects.SO.SOOrder.curyFreightAmt">freight amount</see> values.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.freightTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<SOOrder.curyPremiumFreightAmt, SOOrder.curyFreightAmt>))]
  [PXUIField(DisplayName = "Freight Total")]
  public virtual Decimal? CuryFreightTot
  {
    get => this._CuryFreightTot;
    set => this._CuryFreightTot = value;
  }

  [PXDBDecimal(4)]
  public virtual Decimal? FreightTot
  {
    get => this._FreightTot;
    set => this._FreightTot = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.TX.TaxCategory">tax category that applies to the total freight amount</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.FreightTaxCategory" /> foreign key.
  /// </summary>
  /// <remarks>
  /// <para>The default value is the tax category associated with the <see cref="T:PX.Objects.SO.SOOrder.shipVia">ship via code</see>
  /// ship via code of the order.</para>
  /// <para>This field is not available for transfer orders.</para>
  /// </remarks>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.TX.TaxCategory.taxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [SOOrderTax(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), typeof (SOOrder.taxCalcMode), TaxCalc = TaxCalc.ManualLineCalc, CuryTaxableAmtField = typeof (SOOrder.curyTaxableFreightAmt))]
  [SOOrderUnbilledFreightTax(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), typeof (SOOrder.taxCalcMode), TaxCalc = TaxCalc.ManualLineCalc)]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.CS.Carrier.taxCategoryID, Where<PX.Objects.CS.Carrier.carrierID, Equal<Current<SOOrder.shipVia>>>>))]
  public virtual string FreightTaxCategoryID
  {
    get => this._FreightTaxCategoryID;
    set => this._FreightTaxCategoryID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.miscTot">total amount</see> calculated as the sum of the amounts in
  /// <see cref="T:PX.Objects.SO.SOLine.curyLineAmt">Amount</see> of the order non-stock items
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.miscTot))]
  [PXUIField(DisplayName = "Misc. Charges", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryMiscTot
  {
    get => this._CuryMiscTot;
    set => this._CuryMiscTot = value;
  }

  /// <summary>
  /// The total amount calculated as the sum of the amounts in <see cref="T:PX.Objects.SO.SOLine.curyLineAmt">Amount</see>
  /// of the order non-stock items.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDecimal(4)]
  public virtual Decimal? MiscTot
  {
    get => this._MiscTot;
    set => this._MiscTot = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.goodsExtPriceTotal">total amount</see> on all lines of the document, except for Misc. Charges, before Line-level discounts
  /// are applied (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.goodsExtPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Goods")]
  public virtual Decimal? CuryGoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount on all lines of the document, except for Misc. Charges, before Line-level discounts are applied.
  /// </summary>
  /// <remarks>
  /// <para>This total is calculated as the sum of the amounts in the
  /// <see cref="T:PX.Objects.SO.SOLine.curyExtPrice">Ext. Price</see> for all stock items and non-stock items that require shipment.
  /// This total does not include the freight amount.</para>
  /// <para>This field is not available for transfer orders.
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.miscTot">total amount</see> calculated as the sum of the amounts in
  /// <see cref="T:PX.Objects.SO.SOLine.curyExtPrice">Ext. Price</see> of the order non-stock items
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.miscExtPriceTotal))]
  [PXUIField(DisplayName = "Misc. Charges", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryMiscExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount calculated as the sum of the amounts in <see cref="T:PX.Objects.SO.SOLine.curyExtPrice">Ext. Price</see>
  /// of the order non-stock items.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDecimal(4)]
  public virtual Decimal? MiscExtPriceTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.detailExtPriceTotal">sum</see> of the
  /// <see cref="T:PX.Objects.SO.SOOrder.curyGoodsExtPriceTotal">goods</see> and
  /// the <see cref="T:PX.Objects.SO.SOOrder.curyMiscExtPriceTotal">misc. charges amount</see> values.
  /// </summary>
  [PXCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.detailExtPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCalced(typeof (Add<SOOrder.curyGoodsExtPriceTotal, SOOrder.curyMiscExtPriceTotal>), typeof (Decimal))]
  [PXFormula(typeof (Add<SOOrder.curyGoodsExtPriceTotal, SOOrder.curyMiscExtPriceTotal>))]
  [PXUIField(DisplayName = "Detail Total")]
  public virtual Decimal? CuryDetailExtPriceTotal { get; set; }

  /// <summary>
  /// The sum of the <see cref="T:PX.Objects.SO.SOOrder.goodsExtPriceTotal">goods</see> and
  /// the <see cref="T:PX.Objects.SO.SOOrder.miscExtPriceTotal">misc. charges amount</see> values.
  /// </summary>
  [PXDecimal(4)]
  [PXDBCalced(typeof (Add<SOOrder.goodsExtPriceTotal, SOOrder.miscExtPriceTotal>), typeof (Decimal))]
  public virtual Decimal? DetailExtPriceTotal { get; set; }

  /// <summary>
  /// The summarized quantity of all items that have been added to the child order from the blanket sales order.
  /// </summary>
  /// <remarks>
  /// If any items that are not from the current blanket sales order have been added to the child order,
  /// their quantity is not summed up to the value in this field.
  /// </remarks>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Ordered Qty.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  /// <summary>
  /// The total weight of the goods according to the document.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Order Weight", Enabled = false)]
  public virtual Decimal? OrderWeight
  {
    get => this._OrderWeight;
    set => this._OrderWeight = value;
  }

  /// <summary>The total volume of goods according to the document.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Order Volume", Enabled = false)]
  public virtual Decimal? OrderVolume
  {
    get => this._OrderVolume;
    set => this._OrderVolume = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.openOrderTotal">sum of unshipped amounts</see> calculated for the lines with nonzero
  /// unshipped quantities of stock items (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.openOrderTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unshipped Amount", Enabled = false)]
  public virtual Decimal? CuryOpenOrderTotal
  {
    get => this._CuryOpenOrderTotal;
    set => this._CuryOpenOrderTotal = value;
  }

  /// <summary>
  /// The sum of unshipped amounts calculated for the lines with nonzero unshipped quantities of stock items.
  /// </summary>
  /// <remarks>
  /// The unshipped amount for each line is calculated as the amount in the
  /// <see cref="T:PX.Objects.SO.SOLine.curyExtPrice">Ext. Price</see> field (after Line-level discounts were applied) divided
  /// by the line quantity (the <see cref="T:PX.Objects.SO.SOLine.orderQty">Qty.</see> field) and multiplied by the unshipped
  /// quantity (the <see cref="T:PX.Objects.SO.SOLine.openQty">Open Qty.</see> field). At the moment of order creation when
  /// no item quantities are shipped, the amount is equal to the <see cref="T:PX.Objects.SO.SOOrder.lineTotal">Line Total</see>;
  /// this total does not include any freight amount.
  /// <para />
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenOrderTotal
  {
    get => this._OpenOrderTotal;
    set => this._OpenOrderTotal = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.openLineTotal">sum of line open amount</see> (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.openLineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unshipped Line Total")]
  public virtual Decimal? CuryOpenLineTotal
  {
    get => this._CuryOpenLineTotal;
    set => this._CuryOpenLineTotal = value;
  }

  /// <summary>
  /// The sum of child <see cref="T:PX.Objects.SO.SOLine4.openAmt">line open amounts</see>. When <see cref="T:PX.Objects.SO.SOLine4.operation" />
  /// is not equal <see cref="T:PX.Objects.SO.SOOrder.defaultOperation" />, the line open amounts are with negative values.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenLineTotal
  {
    get => this._OpenLineTotal;
    set => this._OpenLineTotal = value;
  }

  /// <summary>
  /// The discount <see cref="T:PX.Objects.SO.SOOrder.openDiscTotal">total</see> of open lines (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.openDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOpenDiscTotal { get; set; }

  /// <summary>
  /// The discount total of open lines, which is calculated as a sum of values for each line.
  /// The value for each line is calculated as the <see cref="T:PX.Objects.SO.SOLine4.openAmt">line open amount</see>
  /// multiplied by the difference between <see cref="T:PX.Objects.SO.SOLine4.groupDiscountRate" /> and
  /// <see cref="T:PX.Objects.SO.SOLine4.documentDiscountRate" />. The negative value is used for the
  /// <see cref="T:PX.Objects.SO.SOLine4.openAmt">line open amount</see> if <see cref="T:PX.Objects.SO.SOLine4.operation" /> is not equal to
  /// <see cref="T:PX.Objects.SO.SOOrder.defaultOperation" />.
  /// </summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenDiscTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.openTaxTotal">sum</see> of tax amounts of child transactions
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.openTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unshipped Tax Total")]
  public virtual Decimal? CuryOpenTaxTotal
  {
    get => this._CuryOpenTaxTotal;
    set => this._CuryOpenTaxTotal = value;
  }

  /// <summary>
  /// The sum of <see cref="T:PX.Objects.SO.SOTaxTran.taxAmt">tax amounts</see> of child transactions.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenTaxTotal
  {
    get => this._OpenTaxTotal;
    set => this._OpenTaxTotal = value;
  }

  /// <summary>
  /// The quantity of the stock items not yet shipped according to the sales order.
  /// </summary>
  /// <remarks>
  /// <para>The unshipped quantity for each line is specified in <see cref="T:PX.Objects.SO.SOLine.openQty" />.
  /// </para>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Unshipped Quantity")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenOrderQty
  {
    get => this._OpenOrderQty;
    set => this._OpenOrderQty = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.unbilledOrderTotal">unbilled balance</see> of the sales order, which is calculated as
  /// the sum of the <see cref="T:PX.Objects.SO.SOLine.curyUnbilledAmt">unbilled line amounts</see>
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unbilledOrderTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Balance", Enabled = false)]
  public virtual Decimal? CuryUnbilledOrderTotal
  {
    get => this._CuryUnbilledOrderTotal;
    set => this._CuryUnbilledOrderTotal = value;
  }

  /// <summary>
  /// The unbilled balance of the sales order, which is calculated as the sum
  /// of the <see cref="T:PX.Objects.SO.SOLine.curyUnbilledAmt">unbilled line amounts</see>.
  /// </summary>
  /// <remarks>
  /// The value is calculated as the sum of the <see cref="T:PX.Objects.SO.SOLine.curyUnbilledAmt">unbilled line amounts</see>,
  /// unbilled amount of the freight price and premium freight price, and the unbilled amount of all taxes,
  /// minus all line, group, and document discounts of the order.
  /// <para>This field is not available for transfer orders.</para>
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledOrderTotal
  {
    get => this._UnbilledOrderTotal;
    set => this._UnbilledOrderTotal = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.unbilledLineTotal">sum</see> of unbilled amounts of child lines
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unbilledLineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Line Total")]
  public virtual Decimal? CuryUnbilledLineTotal
  {
    get => this._CuryUnbilledLineTotal;
    set => this._CuryUnbilledLineTotal = value;
  }

  /// <summary>
  /// The sum of <see cref="!:BlanketSOLine.unbilledAmt">unbilled amount</see> of child lines for which the
  /// following condition is <see langword="true" />: <see cref="!:BlanketSOLine.lineType" /> is not equal to
  /// <see cref="T:PX.Objects.SO.SOLineType.miscCharge" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledLineTotal
  {
    get => this._UnbilledLineTotal;
    set => this._UnbilledLineTotal = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.unbilledMiscTot">sum</see> of unbilled amounts of child lines
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unbilledMiscTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Misc. Total")]
  public virtual Decimal? CuryUnbilledMiscTot
  {
    get => this._CuryUnbilledMiscTot;
    set => this._CuryUnbilledMiscTot = value;
  }

  /// <summary>
  /// The sum of <see cref="!:BlanketSOLine.unbilledAmt">unbilled amounts</see> of child lines for which the
  /// following condition is <see langword="true" />:
  /// <see cref="!:BlanketSOLine.lineType" /> is equal to <see cref="T:PX.Objects.SO.SOLineType.miscCharge" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledMiscTot
  {
    get => this._UnbilledMiscTot;
    set => this._UnbilledMiscTot = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.unbilledTaxTotal">total amount</see> of tax paid on the unbilled part of the document
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unbilledTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Tax Total")]
  public virtual Decimal? CuryUnbilledTaxTotal
  {
    get => this._CuryUnbilledTaxTotal;
    set => this._CuryUnbilledTaxTotal = value;
  }

  /// <summary>
  /// The total amount of tax paid on the unbilled part of the document.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledTaxTotal
  {
    get => this._UnbilledTaxTotal;
    set => this._UnbilledTaxTotal = value;
  }

  /// <summary>
  /// The discount <see cref="T:PX.Objects.SO.SOOrder.unbilledDiscTotal">total</see> for unbilled lines (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unbilledDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnbilledDiscTotal { get; set; }

  /// <summary>The discount total for unbilled lines.</summary>
  [PXDBDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledDiscTotal { get; set; }

  /// <summary>
  /// The sum of amounts of <see cref="T:PX.Objects.SO.SOFreightDetail">the freight details</see> that have been
  /// transferred to the SO invoice generated for the sales order (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.billedFreightTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledFreightTot { get; set; }

  /// <summary>
  /// The sum of amounts of <see cref="T:PX.Objects.SO.SOFreightDetail">the freight details</see> that have been
  /// transferred to the SO invoice generated for the sales order (in the base currency).
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledFreightTot { get; set; }

  /// <summary>
  /// The unbilled part of the sales order <see cref="T:PX.Objects.SO.SOOrder.curyFreightTot">freight total</see>
  /// in the document currency.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unbilledFreightTot))]
  [PXFormula(typeof (IIf<Where<SOOrder.completed, Equal<True>, And<Add<SOOrder.releasedCntr, SOOrder.billedCntr>, GreaterEqual<SOOrder.shipmentCntr>, Or<Selector<SOOrder.orderType, SOOrderType.requireShipping>, Equal<False>, And<Add<SOOrder.releasedCntr, SOOrder.billedCntr>, Greater<int0>, Or<Sub<SOOrder.curyFreightTot, SOOrder.curyBilledFreightTot>, Less<decimal0>>>>>>, decimal0, Sub<SOOrder.curyFreightTot, SOOrder.curyBilledFreightTot>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnbilledFreightTot { get; set; }

  /// <summary>
  /// The unbilled part of the sales order <see cref="T:PX.Objects.SO.SOOrder.curyFreightTot">freight total</see>
  /// in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledFreightTot { get; set; }

  /// <summary>
  /// The quantity of stock and non-stock items that were not yet billed.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBQuantity]
  [PXUIField(DisplayName = "Unbilled Quantity")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledOrderQty
  {
    get => this._UnbilledOrderQty;
    set => this._UnbilledOrderQty = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.controlTotal">control total</see> of the document (in the currency of the document).
  /// A user enters this amount manually.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.controlTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total")]
  public virtual Decimal? CuryControlTotal
  {
    get => this._CuryControlTotal;
    set => this._CuryControlTotal = value;
  }

  /// <summary>
  /// The control total of the document. A user enters this amount manually.
  /// </summary>
  /// <remarks>
  /// <para>This amount should be equal to the sum of the amounts of all detail lines of the document.</para>
  /// <para>The document can be released only if the value in this field is equal to the value in
  /// <see cref="T:PX.Objects.SO.SOOrder.orderTotal" />.</para>
  /// <para>This field is available only if the
  /// <see cref="T:PX.Objects.SO.SOOrderType.requireControlTotal">Require Control Total</see> field is <see langword="true" />.
  /// </para>
  /// </remarks>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlTotal
  {
    get => this._ControlTotal;
    set => this._ControlTotal = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.paymentTotal">total amount</see> that has been paid for this sales order (in the currency
  /// of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.paymentTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Paid", Enabled = false)]
  public virtual Decimal? CuryPaymentTotal
  {
    get => this._CuryPaymentTotal;
    set => this._CuryPaymentTotal = value;
  }

  /// <summary>
  /// The total amount that has been paid for this sales order.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PaymentTotal
  {
    get => this._PaymentTotal;
    set => this._PaymentTotal = value;
  }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that the specified customer tax zone will
  /// not be overridden if any location-related information is changed for the sales order.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Tax Zone")]
  public virtual bool? OverrideTaxZone
  {
    get => this._OverrideTaxZone;
    set => this._OverrideTaxZone = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.TX.TaxZone">tax zone</see> to be used to process customer sales orders.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.TaxZone" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.TX.TaxZone.taxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<SOOrder.customerLocationID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>
  /// The tax calculation mode to be used for the sales order.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list type="bullet">
  /// <item><description><see cref="T:PX.Objects.TX.TaxCalculationMode.taxSetting" /> (default): The document
  /// uses the settings of the selected customer, or of the location of the customer if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.accountLocations">Business Account Locations</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</description></item>
  /// <item><description><see cref="T:PX.Objects.TX.TaxCalculationMode.gross" />: The tax amount is included in
  /// the item price.</description></item>
  /// <item><description><see cref="T:PX.Objects.TX.TaxCalculationMode.net" />: The tax amount is not included in
  /// the item price.</description></item>
  /// </list>
  /// </value>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.netGrossEntryMode">Net/Gross Entry Mode</see>
  /// feature has been enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<PX.Objects.CR.Location.cTaxCalcMode, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <summary>The tax exemption number for reporting purposes.</summary>
  /// <remarks>
  /// The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled on
  /// the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cAvalaraExemptionNumber, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  /// <summary>
  /// The entity usage type of the customer location if sales to this location are tax-exempt.
  /// </summary>
  /// <value>
  /// By default, the system copies the value of this field from the customer record.
  /// </value>
  /// <remarks>
  /// <para>This field is available only if the
  /// <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// <para>This field is not available for transfer orders.</para>
  /// </remarks>
  [PXDefault("0", typeof (Search<PX.Objects.CR.Location.cAvalaraCustomerUsageType, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType
  {
    get => this._AvalaraCustomerUsageType;
    set => this._AvalaraCustomerUsageType = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Project" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMProject.contractID" /> field.
  /// The value specifies the project with which this sales order is associated or the non-project code, which
  /// indicates that this order is not associated with any project.
  /// The non-project code is specified on the Projects Preferences (PM101000) form.
  /// </value>
  /// <remarks>
  /// <para>This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.projectAccounting">Project Accounting</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form and the integration of the Projects
  /// submodule with Sales Orders has been enabled
  /// (that is, <see cref="T:PX.Objects.PM.PMSetup.visibleInSO" /> is <see langword="true" />.</para>
  /// </remarks>
  [ProjectDefault("SO")]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInSO, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.restrictProjectSelect, Equal<PMRestrictOption.allProjects>, Or<PMProject.customerID, Equal<Current<SOOrder.customerID>>, Or<Current<SOOrder.behavior>, Equal<SOBehavior.tR>, Or<PMProject.nonProject, Equal<True>>>>>), "The customer in the document does not match the customer specified in the selected project or contract ({0}).", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// An option that controls whether incomplete and partial shipments for the order are allowed.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list type="bullet">
  /// <item><description><see cref="T:PX.Objects.SO.SOShipComplete.shipComplete" />: The first shipment for
  /// the order should include all lines of the order.</description></item>
  /// <item><description><see cref="T:PX.Objects.SO.SOShipComplete.backOrderAllowed" />: The first shipment
  /// for the order should include at least one order line.</description></item>
  /// <item><description><see cref="T:PX.Objects.SO.SOShipComplete.cancelRemainder" />: The first shipment
  /// for the order should include at least one order line.</description></item>
  /// </list>
  /// </value>
  /// <remarks>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// form.
  /// </remarks>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.FOBPoint">point</see> where ownership of
  /// the goods is transferred to the customer.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.FOBPoint" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.FOBPoint" /> field.
  /// </value>
  /// <remarks>
  /// This field is available only for blanket sales orders and cannot be empty.
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (Search<PX.Objects.CS.FOBPoint.fOBPointID>), DescriptionField = typeof (PX.Objects.CS.FOBPoint.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cFOBPointID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  public virtual string FOBPoint
  {
    get => this._FOBPoint;
    set => this._FOBPoint = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Carrier">ship via code</see> that represents the carrier and
  /// its service to be used for shipping the ordered goods.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Carrier" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.Carrier.carrierID" /> field.
  /// </value>
  /// <remarks>
  /// For this ship via code, if Manual is specified as the freight calculation method, the freight amount must
  /// be specified in the <see cref="P:PX.Objects.SO.SOOrder.CuryFreightAmt">Freight Price</see> field.
  /// Changing the Ship Via code for an open sales order may update the
  /// <see cref="T:PX.Objects.SO.SOOrder.taxZoneID">customer tax zone</see> field.
  /// </remarks>
  [PXUIField(DisplayName = "Ship Via")]
  [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isCommonCarrier), typeof (PX.Objects.CS.Carrier.confirmationRequired), typeof (PX.Objects.CS.Carrier.packageRequired)}, DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cCarrierID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>, And<Current<SOOrder.behavior>, NotEqual<SOBehavior.bL>>>>>))]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the customer picks the goods from the warehouse (will call).
  /// </summary>
  /// <value><para>If the value is <see langword="false" />, the common carrier is
  /// to be used for shipping goods.</para>
  /// <para>If <see cref="T:PX.Objects.SO.SOOrder.shipVia" /> is <see langword="null" />,
  /// the value of this field is <see langword="true" />.
  /// If <see cref="T:PX.Objects.SO.SOOrder.shipVia" /> is not <see langword="null" />, the value of this field directly corresponds
  /// to the state of the <see cref="T:PX.Objects.CS.Carrier.isCommonCarrier">Common Carrier</see> field for
  /// the selected ship via code.</para>
  /// </value>
  [PXBool]
  [PXFormula(typeof (Switch<Case2<Where<Selector<SOOrder.shipVia, PX.Objects.CS.Carrier.isCommonCarrier>, NotEqual<True>>, True>, False>))]
  [PXUIField(DisplayName = "Will Call", IsReadOnly = true)]
  public bool? WillCall { get; set; }

  /// <summary>
  /// The counter of child package lines added to the order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PackageLineCntr
  {
    get => this._PackageLineCntr;
    set => this._PackageLineCntr = value;
  }

  /// <summary>
  /// The total (gross) weight of the packages for this sales order, including the weight of the boxes used for
  /// packages.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Package Weight", Enabled = false)]
  public virtual Decimal? PackageWeight
  {
    get => this._PackageWeight;
    set => this._PackageWeight = value;
  }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that the customer account with the
  /// carrier should be billed for the shipping of this order.
  /// </summary>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.carrierIntegration">Shipping Carrier Integration</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Customer's Account")]
  public virtual bool? UseCustomerAccount
  {
    get => this._UseCustomerAccount;
    set => this._UseCustomerAccount = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the shipment should be delivered to a residential area.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cResedential, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? Resedential
  {
    get => this._Resedential;
    set => this._Resedential = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the order may be delivered on Saturday.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cSaturdayDelivery, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? SaturdayDelivery
  {
    get => this._SaturdayDelivery;
    set => this._SaturdayDelivery = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether a user selects to use the FedEx Ground Collect option.
  /// </summary>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.carrierIntegration">Shipping Carrier Integration</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form, integration with the FedEx carrier
  /// is established, and FedEx is selected in the <see cref="T:PX.Objects.SO.SOOrder.shipVia">Ship Via</see> field.
  /// </remarks>
  [PXDBBool]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cGroundCollect, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [PXUIField(DisplayName = "Ground Collect")]
  public virtual bool? GroundCollect
  {
    get => this._GroundCollect;
    set => this._GroundCollect = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether a user selects to indicate that insurance is required for this order.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cInsurance, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? Insurance
  {
    get => this._Insurance;
    set => this._Insurance = value;
  }

  /// <summary>
  /// The level of priority for processing orders of this customer, as specified
  /// <see cref="T:PX.Objects.CR.Location.cOrderPriority" /> field for the <see cref="T:PX.Objects.SO.SOOrder.FK.Customer">customer</see>.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Priority")]
  public virtual short? Priority
  {
    get => this._Priority;
    set => this._Priority = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.SalesPerson">salesperson</see> to be used by default
  /// for each sales order line.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.SalesPerson" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.SalesPerson.salesPersonID" /> field.
  /// </value>
  [SalesPerson(DisplayName = "Default Salesperson")]
  [PXDefault(typeof (Search<CustDefSalesPeople.salesPersonID, Where<CustDefSalesPeople.bAccountID, Equal<Current<SOOrder.customerID>>, And<CustDefSalesPeople.locationID, Equal<Current<SOOrder.customerLocationID>>, And<CustDefSalesPeople.isDefault, Equal<True>>>>>))]
  [PXForeignReference(typeof (Field<SOOrder.salesPersonID>.IsRelatedTo<PX.Objects.AR.SalesPerson.salesPersonID>))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Terms">credit terms</see> used in relations with the customer.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Terms" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.Terms.termsID" /> field.
  /// </value>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.termsID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<SOOrder.customerID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  [Terms(typeof (SOOrder.invoiceDate), typeof (SOOrder.dueDate), typeof (SOOrder.discDate), typeof (SOOrder.curyOrderTotal), typeof (SOOrder.curyTermsDiscAmt), typeof (SOOrder.curyTaxTotal), typeof (SOOrder.branchID))]
  [PXForeignReference(typeof (Field<SOOrder.termsID>.IsRelatedTo<PX.Objects.CS.Terms.termsID>))]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  /// <summary>
  /// The due date of the invoice according to the credit terms.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Due Date")]
  public virtual DateTime? DueDate
  {
    get => this._DueDate;
    set => this._DueDate = value;
  }

  /// <summary>
  /// The date when the cash discount is available for the invoice based on the credit terms.
  /// </summary>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? DiscDate
  {
    get => this._DiscDate;
    set => this._DiscDate = value;
  }

  /// <summary>
  /// The reference number of the original invoice (which lists the goods that were ordered and
  /// later returned by the customer).
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Invoice" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOInvoice.refNbr" /> field.
  /// </value>
  /// <remarks>
  /// This field is available for orders of the CR, RC, RR, and RM types.
  /// </remarks>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [SOInvoiceNbr]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  /// <summary>The date of the invoice generated for the order.</summary>
  /// <remarks>
  /// <para>Date can be entered manually if the <see cref="T:PX.Objects.SO.SOOrderType.billSeparately">Bill Separately</see>
  /// field is <see langword="true" /> for the order type.</para>
  /// <para>This field is not available for transfer orders.</para>
  /// </remarks>
  [PXDBDate]
  [PXDefault]
  [PXUIField]
  [PXFormula(typeof (Default<SOOrder.orderDate>))]
  public virtual DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  /// <summary>
  /// The period to post the transactions generated by the invoice.
  /// </summary>
  [PXDefault]
  [SOFinPeriod(typeof (SOOrder.invoiceDate), typeof (SOOrder.branchID), null, null, null, null, true, false)]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.TM.EPCompanyTree">workgroup</see> responsible for the sales order.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Workgroup" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.TM.EPCompanyTree.workGroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.CR.BAccount.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Workgroup", Enabled = false)]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="N:PX.Objects.CR.Standalone">employee</see> in the workgroup who is responsible for
  /// the sales order.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.Owner" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.Standalone.EPEmployee.bAccountID" />
  /// field.
  /// </value>
  [PXDefault(typeof (Coalesce<Search<CREmployee.defContactID, Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, And<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>>>, Search<PX.Objects.CR.BAccount.ownerID, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<SOOrder.customerID>>>>>))]
  [Owner(typeof (SOOrder.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.BAccount">business account</see> of the employee of the order's
  /// <see cref="T:PX.Objects.SO.SOOrder.ownerID">owner</see>.
  /// </summary>
  [PXInt]
  [PXFormula(typeof (Switch<Case<Where<SOOrder.ownerID, IsNotNull>, Selector<SOOrder.ownerID, PX.Objects.EP.EPEmployee.bAccountID>>, Null>))]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
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

  [PXDBGotReadyForArchive]
  [PXFormula(typeof (BqlOperand<Today, IBqlDateTime>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.cancelled, Equal<True>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.completed, Equal<True>>>>, And<BqlOperand<SOOrder.openLineCntr, IBqlInt>.IsEqual<Zero>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.behavior, Equal<SOBehavior.qT>>>>, Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.behavior, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.cM, SOBehavior.rM>>>>, And<BqlOperand<SOOrder.shipmentCntr, IBqlInt>.IsGreater<Zero>>>>.And<BqlOperand<SOOrder.shipmentCntr, IBqlInt>.IsEqual<SOOrder.releasedCntr>>>>, Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.behavior, Equal<SOBehavior.tR>>>>, And<BqlOperand<SOOrder.shipmentCntr, IBqlInt>.IsGreater<Zero>>>>.And<BqlOperand<SOOrder.openShipmentCntr, IBqlInt>.IsEqual<Zero>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.behavior, Equal<SOBehavior.bL>>>>, And<BqlOperand<SOOrder.unbilledOrderQty, IBqlDecimal>.IsEqual<Zero>>>>.And<BqlOperand<SOOrder.curyUnbilledOrderTotal, IBqlDecimal>.IsEqual<Zero>>>>>>.ElseNull))]
  public virtual DateTime? GotReadyForArchiveAt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.termsDiscAmt">discount amount</see> of the prompt payment (in the currency of the document).
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? CuryTermsDiscAmt
  {
    get => this._CuryTermsDiscAmt;
    set => this._CuryTermsDiscAmt = value;
  }

  /// <summary>The discount amount of the prompt payment.</summary>
  [PXDecimal(4)]
  public virtual Decimal? TermsDiscAmt
  {
    get => this._TermsDiscAmt;
    set => this._TermsDiscAmt = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.ShipTerms">shipping terms</see> used for this customer.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.ShipTerms" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.ShipTerms.shipTermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (PX.Objects.CS.ShipTerms.shipTermsID), DescriptionField = typeof (PX.Objects.CS.ShipTerms.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cShipTermsID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  public virtual string ShipTermsID
  {
    get => this._ShipTermsID;
    set => this._ShipTermsID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.ShippingZone">shipping zone</see> of the customer to be used to
  /// calculate freight.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.ShippingZone" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.ShippingZone.zoneID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Zone")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), DescriptionField = typeof (PX.Objects.CS.ShippingZone.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cShipZoneID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  public virtual string ShipZoneID
  {
    get => this._ShipZoneID;
    set => this._ShipZoneID = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the sales order totals affect balances of the customer.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? InclCustOpenOrders
  {
    get => this._InclCustOpenOrders;
    set => this._InclCustOpenOrders = value;
  }

  /// <summary>
  /// The counter of <see cref="T:PX.Objects.SO.SOOrderShipment">order-shipment relations</see> added to the order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ShipmentCntr
  {
    get => this._ShipmentCntr;
    set => this._ShipmentCntr = value;
  }

  /// <summary>
  /// The counter of unconfirmed <see cref="T:PX.Objects.SO.SOShipment">shipments</see> added to the order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? OpenShipmentCntr
  {
    get => this._OpenShipmentCntr;
    set => this._OpenShipmentCntr = value;
  }

  /// <summary>
  /// The number of warehouses without shipments of the order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? OpenSiteCntr { get; set; }

  /// <summary>
  /// The counter of warehouses for all shipments of the order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? SiteCntr { get; set; }

  /// <summary>The counter of open lines added to the order.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? OpenLineCntr
  {
    get => this._OpenLineCntr;
    set => this._OpenLineCntr = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSite">warehouse</see> from which the goods should be shipped.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.DefaultSite" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.INSite.siteID" /> field.
  /// </value>
  /// <remarks>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.inventory">Inventory</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [Site(DisplayName = "Preferred Warehouse ID", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cSiteID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>))]
  [PXForeignReference(typeof (Field<SOOrder.defaultSiteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? DefaultSiteID
  {
    get => this._DefaultSiteID;
    set => this._DefaultSiteID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSite">destination warehouse</see> for the items to be transferred.
  /// The field is included in the foreign keys <see cref="T:PX.Objects.SO.SOOrder.FK.DestinationSite" /> and <see cref="T:PX.Objects.SO.SOOrder.FK.ToSite" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.INSite.siteID" /> field.
  /// </value>
  /// <remarks>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.warehouse">Multiple Warehouses</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDefault]
  [ToSite(typeof (INTransferType.twoStep), typeof (SOOrder.branchID), DisplayName = "Destination Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXUIRequired(typeof (BqlOperand<SOOrder.isTransferOrder, IBqlBool>.IsEqual<True>))]
  [PXForeignReference(typeof (Field<SOOrder.destinationSiteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? DestinationSiteID
  {
    get => this._DestinationSiteID;
    set => this._DestinationSiteID = value;
  }

  /// <summary>
  /// The error message that is raised because of the setting of the
  /// <see cref="T:PX.Objects.SO.SOOrder.shipAddressID">shipping address</see> or
  /// the <see cref="T:PX.Objects.SO.SOOrder.shipContactID">shipping contact</see> fields.
  /// </summary>
  [PXString(150, IsUnicode = true)]
  public virtual string DestinationSiteIdErrorMessage { get; set; }

  /// <summary>
  /// The default <see cref="T:PX.Objects.SO.SOOrderTypeOperation.operation">operation</see> (which can be an issue or receipt)
  /// of the <see cref="T:PX.Objects.SO.SOOrder.orderType">order type</see>.
  /// </summary>
  [PXString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<SOOrder.orderType, SOOrderType.defaultOperation>))]
  [PXSelectorMarker(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<SOOrder.orderType>>>>), CacheGlobal = true)]
  public virtual string DefaultOperation
  {
    get => this._DefaultOperation;
    set => this._DefaultOperation = value;
  }

  /// <summary>
  /// The number of active <see cref="T:PX.Objects.SO.SOOrderTypeOperation">operations</see> (which can be an issue or receipt)
  /// of the <see cref="T:PX.Objects.SO.SOOrder.orderType">order type</see>.
  /// </summary>
  [PXInt]
  [PXFormula(typeof (Selector<SOOrder.orderType, SOOrderType.activeOperationsCntr>))]
  public virtual int? ActiveOperationsCntr { get; set; }

  /// <summary>The default transfer type.</summary>
  /// <value>
  /// The value is the <see cref="T:PX.Objects.SO.SOOrder.orderType">order type</see> of the order if the order type is
  /// <see cref="T:PX.Objects.SO.SOOrderType.iNDocType">Inventory Transaction Type</see>.
  /// </value>
  [PXString(3, IsFixed = true)]
  [PXFormula(typeof (Selector<SOOrder.orderType, SOOrderType.iNDocType>))]
  public virtual string DefaultTranType { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOOrderType">type</see> of the original order.
  /// The field is included in the foreign keys <see cref="T:PX.Objects.SO.SOOrder.FK.OriginalOrderType" /> and
  /// <see cref="T:PX.Objects.SO.SOOrder.FK.OriginalOrder" />.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOOrderType.orderType" /> field.
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOOrder.orderType" /> field.
  /// </value>
  /// <remarks>The field is used only for returns.</remarks>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Order Type", Enabled = false)]
  public virtual string OrigOrderType
  {
    get => this._OrigOrderType;
    set => this._OrigOrderType = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOOrder">reference number</see> of the original sales order.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.OriginalOrder" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOOrder.orderNbr" /> field.
  /// </value>
  /// <remarks>The field is used only for returns.</remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOOrder.origOrderType>>>>))]
  public virtual string OrigOrderNbr
  {
    get => this._OrigOrderNbr;
    set => this._OrigOrderNbr = value;
  }

  /// <exclude />
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public virtual Decimal? ManDisc
  {
    get => this._ManDisc;
    set => this._ManDisc = value;
  }

  /// <exclude />
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.manDisc))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Manual Total")]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public virtual Decimal? CuryManDisc
  {
    get => this._CuryManDisc;
    set => this._CuryManDisc = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether a user approved an order that has a failed credit check.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ApprovedCredit
  {
    get => this._ApprovedCredit;
    set => this._ApprovedCredit = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether a user approved an order that has a failed credit check by a payment.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ApprovedCreditByPayment { get; set; }

  /// <summary>
  /// The amount of the order that a user approved on a failed credit check.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ApprovedCreditAmt
  {
    get => this._ApprovedCreditAmt;
    set => this._ApprovedCreditAmt = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CA.PaymentMethod">payment method</see> to be used to pay for the sales
  /// order. The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.PaymentMethod" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CA.PaymentMethod.paymentMethodID" />
  /// field. By default, the value is the default payment method of the customer.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>>>>, Search<PX.Objects.AR.Customer.defPaymentMethodID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<SOOrder.customerID>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<boolTrue>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<boolTrue>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method")]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.CustomerPaymentMethod">default card or account number</see> for
  /// the payment method (for payment methods that require card or account numbers).
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.CustomerPaymentMethod" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.CustomerPaymentMethod.pMInstanceID" /> field.
  /// </value>
  /// <remarks>
  /// If the customer has more than one card or account number, a user can select one from the list of cards
  /// or accounts available for the customer.
  /// </remarks>
  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.AR.CustomerPaymentMethod.pMInstanceID))]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current2<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOOrder.paymentMethodID>>>>>>, Search2<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, LeftJoin<CCProcessingCenterPmntMethodBranch, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethodBranch.paymentMethodID>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenterPmntMethodBranch.processingCenterID>, And<Current2<SOOrder.branchID>, Equal<CCProcessingCenterPmntMethodBranch.branchID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOOrder.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Asc<Switch<Case<Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, IsNull>, True>, False>, Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOOrder.paymentMethodID>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>, Or<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current<SOOrder.pMInstanceID>>>>), "The {0} card/account number is inactive on the Customer Payment Methods (AR303010) form and cannot be processed.", new System.Type[] {typeof (PX.Objects.AR.CustomerPaymentMethod.descr)})]
  [DeprecatedProcessing]
  [DisabledProcCenter]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">cash account</see> associated with the customer payment method.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.CashAccount" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// By default, the value is filled in with the cash account specified as the default for the selected method.
  /// </value>
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<SOOrder.pMInstanceID>>>>>, Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.aRIsDefault, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOOrder.paymentMethodID>>>>>>>, Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<SOOrder.branchID>>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [CashAccount(typeof (SOOrder.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOOrder.paymentMethodID>>>>>>, Where<Match<Current<AccessInfo.userName>>>>), SuppressCurrencyValidation = false)]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  /// <summary>The reference number of the payment.</summary>
  /// <remarks>
  /// This field is available only for sales orders of the Cash Sales or Cash Return type.
  /// </remarks>
  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Payment Ref.", Enabled = false)]
  [SOOrderPaymentRef(typeof (SOOrder.cashAccountID), typeof (SOOrder.paymentMethodID), typeof (SOOrder.updateNextNumber))]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  /// <summary>A part of the reference number of the payment.</summary>
  /// <remarks>
  /// The field is used by the <see cref="T:PX.Objects.SO.Attributes.SOOrderPaymentRefAttribute">SOOrderPaymentRef</see> attribute.
  /// </remarks>
  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? UpdateNextNumber { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.unreleasedPaymentAmt">sum of the payments</see> that have been applied to the sales order
  /// and that have not been released yet (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unreleasedPaymentAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Not Released", Enabled = false)]
  public virtual Decimal? CuryUnreleasedPaymentAmt { get; set; }

  /// <summary>
  /// The sum of the payments that have been applied to the sales order and that have not been released yet.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnreleasedPaymentAmt { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.cCAuthorizedAmt">sum</see> of all authorized credit card payments that have been applied
  /// to the sales order (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.cCAuthorizedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Authorized", Enabled = false)]
  public virtual Decimal? CuryCCAuthorizedAmt { get; set; }

  /// <summary>
  /// The sum of all authorized credit card payments that have been applied to the sales order.
  /// </summary>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.integratedCardProcessing">Integrated Card Processing</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CCAuthorizedAmt { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.paidAmt">sum</see> of all released payments that have been applied to the sales order
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.paidAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual Decimal? CuryPaidAmt { get; set; }

  /// <summary>
  /// The sum of all released payments that have been applied to the sales order.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PaidAmt { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.billedPaymentTotal">sum of amounts</see> of the payments or prepayments that have been
  /// applied to the AR invoice generated for the order (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.billedPaymentTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Transferred to Invoices", Enabled = false)]
  public virtual Decimal? CuryBilledPaymentTotal { get; set; }

  /// <summary>
  /// The sum of amounts of the payments or prepayments that have been applied to the AR invoice generated for
  /// the order.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledPaymentTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.transferredToChildrenPaymentTotal">sum of amounts</see> of the payments or prepayments that
  /// have been applied to the child orders generated for the blanket order. (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.transferredToChildrenPaymentTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Transferred to Child Orders", Enabled = false, Visible = false)]
  public virtual Decimal? CuryTransferredToChildrenPaymentTotal { get; set; }

  /// <summary>
  /// The sum of amounts of the payments or prepayments that have been applied to the child orders generated for
  /// the blanket order.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TransferredToChildrenPaymentTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.unpaidBalance">unpaid amount</see> of the order (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unpaidBalance))]
  [PXFormula(typeof (IIf<Where<SOOrder.behavior, Equal<SOBehavior.bL>, And<SOOrder.completed, Equal<True>>>, decimal0, Maximum<Sub<Switch<Case<Where<SOOrder.behavior, Equal<SOBehavior.bL>>, Sub<SOOrder.curyOrderTotal, SOOrder.curyTransferredToChildrenPaymentTotal>, Case<Where<Add<SOOrder.releasedCntr, SOOrder.billedCntr>, Equal<int0>>, SOOrder.curyOrderTotal>>, SOOrder.curyUnbilledOrderTotal>, SOOrder.curyPaymentTotal>, decimal0>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unpaid Balance", Enabled = false)]
  public Decimal? CuryUnpaidBalance { get; set; }

  /// <summary>The unpaid amount of the order.</summary>
  /// <remarks>
  /// Once an invoice for the unpaid amount of the order is generated, the unpaid amount becomes 0.
  /// </remarks>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? UnpaidBalance { get; set; }

  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.unrefundedBalance))]
  [PXFormula(typeof (Switch<Case<Where<SOOrder.behavior, Equal<SOBehavior.mO>, And<SOOrder.curyOrderTotal, Less<decimal0>>>, Maximum<Sub<Minus<Switch<Case<Where<Add<SOOrder.releasedCntr, SOOrder.billedCntr>, Equal<int0>>, SOOrder.curyOrderTotal>, SOOrder.curyUnbilledOrderTotal>>, SOOrder.curyPaymentTotal>, decimal0>>, decimal0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIVisible(typeof (Where<BqlOperand<SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.mO>>))]
  [PXUIField(DisplayName = "Unrefunded Balance", Enabled = false)]
  public Decimal? CuryUnrefundedBalance { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? UnrefundedBalance { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system prevents automatic recalculation of packages on any
  /// changes to the order or on automatic creation of shipments.
  /// </summary>
  /// <remarks>
  /// <para>If field value is <see langword="true" />, the packages will not be automatically recalculated to
  /// further optimize the cost even if the order is included in a consolidated shipment.</para>
  /// <para>This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.autoPackaging">Automatic Packaging</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBBool]
  [PXUIField(DisplayName = "Manual Packaging")]
  public virtual bool? IsManualPackage
  {
    get => this._IsManualPackage;
    set => this._IsManualPackage = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the system needs to update taxes after saving the order.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system needs to update unshipped taxes after saving the order.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsOpenTaxValid { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system needs to update unbilled taxes after saving the order.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnbilledTaxValid { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the taxes were calculated by an external system.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? ExternalTaxesImportInProgress { get; set; }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="false" />)
  /// that <see cref="T:PX.Objects.SO.SOOrder.disableAutomaticTaxCalculation" /> is
  /// <see langword="true" /> and a user changed the order. In this case, a warning is displayed.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false, Visible = false)]
  public virtual bool? IsManualTaxesValid { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that order taxes are fully allocated to
  /// the invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? OrderTaxAllocated { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CM.IInvoice.CuryDocBal" />
  [PXFormula(typeof (Switch<Case<Where<Add<SOOrder.releasedCntr, SOOrder.billedCntr>, Equal<int0>, And<SOOrder.behavior, NotEqual<SOBehavior.bL>>>, IIf<BqlOperand<SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.mO>, Abs<SOOrder.curyOrderTotal>, SOOrder.curyOrderTotal>>, IIf<BqlOperand<SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.mO>, Abs<SOOrder.curyUnbilledOrderTotal>, SOOrder.curyUnbilledOrderTotal>>))]
  [PXCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.docBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CM.IInvoice.DocBal" />
  [PXBaseCury]
  [PXFormula(typeof (Switch<Case<Where<Add<SOOrder.releasedCntr, SOOrder.billedCntr>, Equal<int0>>, IIf<BqlOperand<SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.mO>, Abs<SOOrder.orderTotal>, SOOrder.orderTotal>>, IIf<BqlOperand<SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.mO>, Abs<SOOrder.unbilledOrderTotal>, SOOrder.unbilledOrderTotal>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CM.IInvoice.CuryDiscBal" />
  public Decimal? CuryDiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CM.IInvoice.DiscBal" />
  public Decimal? DiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CM.IInvoice.CuryWhTaxBal" />
  public Decimal? CuryWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.CM.IInvoice.WhTaxBal" />
  public Decimal? WhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  /// <inheritdoc cref="!:IInvoice.DocType" />
  public string DocType
  {
    get => (string) null;
    set
    {
    }
  }

  /// <inheritdoc cref="!:IInvoice.RefNbr" />
  public string RefNbr
  {
    get => (string) null;
    set
    {
    }
  }

  /// <inheritdoc cref="!:IInvoice.OrigModule" />
  public string OrigModule
  {
    get => (string) null;
    set
    {
    }
  }

  /// <inheritdoc cref="!:IInvoice.CuryOrigDocAmt" />
  public Decimal? CuryOrigDocAmt
  {
    get => new Decimal?();
    set
    {
    }
  }

  /// <inheritdoc cref="!:IInvoice.OrigDocAmt" />
  public Decimal? OrigDocAmt
  {
    get => new Decimal?();
    set
    {
    }
  }

  /// <inheritdoc cref="!:IInvoice.DocDate" />
  public DateTime? DocDate
  {
    get => new DateTime?();
    set
    {
    }
  }

  /// <inheritdoc cref="!:IInvoice.DocDesc" />
  public string DocDesc
  {
    get => (string) null;
    set
    {
    }
  }

  /// <summary>
  /// A Boolean value that indicates whether the system treats discounts that have already been applied to the
  /// selected sales order as manual.
  /// </summary>
  /// <value>
  /// <para>If the value is <see langword="false" />) for the sales order, the system updates all automatic line,
  /// group, and document discounts when users run discount recalculation or add new lines to the order.</para>
  /// <para>The default state of this field is the same as the state of the
  /// <see cref="T:PX.Objects.SO.SOOrderType.disableAutomaticDiscountCalculation">Disable Automatic Discount Update</see>
  /// for the applicable order type.</para>
  /// </value>
  [PXDBBool]
  [PXDefault(false, typeof (Search<SOOrderType.disableAutomaticDiscountCalculation, Where<SOOrderType.orderType, Equal<Current<SOOrder.orderType>>>>))]
  [PXUIField(DisplayName = "Disable Automatic Discount Update")]
  public virtual bool? DisableAutomaticDiscountCalculation
  {
    get => this._DisableAutomaticDiscountCalculation;
    set => this._DisableAutomaticDiscountCalculation = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the system will update discounts,
  /// taxes and prices after saving the order, and the system will not update them before.
  /// </summary>
  /// <value>If the value is <see langword="false" />,
  /// the system updates discounts, taxes, and prices while the order is editing.
  /// </value>
  /// <remarks>
  /// The field depends on <see cref="T:PX.Objects.SO.SOOrderType.deferPriceDiscountRecalculation" />.
  /// </remarks>
  [PXBool]
  [PXUIField(DisplayName = "Defer Price/Discount Recalculation")]
  public virtual bool? DeferPriceDiscountRecalculation { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system needs to update prices and discounts after saving the
  /// order.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Prices and discounts are up to date.", Enabled = false)]
  public virtual bool? IsPriceAndDiscountsValid { get; set; } = new bool?(true);

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />)
  /// that the system does not need to calculate taxes, because they are already calculated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (Search<SOOrderType.disableAutomaticTaxCalculation, Where<SOOrderType.orderType, Equal<Current<SOOrder.orderType>>>>))]
  [PXUIField(DisplayName = "Disable Automatic Tax Calculation")]
  public virtual bool? DisableAutomaticTaxCalculation { get; set; }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that fields
  /// <see cref="T:PX.Objects.SO.SOOrder.prepaymentReqPct">Prepayment Percent</see> and
  /// <see cref="T:PX.Objects.SO.SOOrder.curyPrepaymentReqAmt">Prepayment Amount</see> will have a higher priority than the predefined
  /// values that has been specified for the credit terms of the customer.
  /// </summary>
  /// <remarks>
  /// This field is available for sales orders with the <see cref="T:PX.Objects.SO.SOBehavior.sO">Sales Orders</see> automation
  /// behavior when the <see cref="T:PX.Objects.CS.Terms.prepaymentRequired">Prepayment Required</see> field of the customer is
  /// <see langword="true" />.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Prepayment")]
  public virtual bool? OverridePrepayment { get; set; }

  /// <summary>
  /// The percent of the total amount of this sales order that the customer has to make before the user can
  /// proceed to shipping the ordered items and preparing an invoice for the sales order.
  /// </summary>
  /// <remarks>
  /// <para>If the <see cref="T:PX.Objects.SO.SOOrder.curyPrepaymentReqAmt">Prepayment Amount</see> changed, than this amount is updated
  /// automatically, and if this value changed, the system automatically updates the
  /// <see cref="T:PX.Objects.SO.SOOrder.curyPrepaymentReqAmt">Prepayment Amount</see> to the appropriate percent based on the amount.
  /// </para>
  /// <para>This field is available when the <see cref="T:PX.Objects.CS.Terms.prepaymentRequired">Prepayment Required</see> field
  /// is <see langword="true" /> and becomes available only for sales orders with the
  /// <see cref="T:PX.Objects.SO.SOBehavior.sO">Sales Orders</see> automation behavior when the
  /// <see cref="T:PX.Objects.SO.SOOrder.overridePrepayment">Override Prepayment</see> field is <see langword="true" />.</para>
  /// </remarks>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXFormula(typeof (IIf<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.defaultTranType, NotEqual<INTranType.transfer>>>>, And<BqlOperand<SOOrder.termsID, IBqlString>.IsNotNull>>>.And<BqlOperand<SOOrder.allowsRequiredPrepayment, IBqlBool>.IsEqual<True>>>, IsNull<Selector<SOOrder.termsID, PX.Objects.CS.Terms.prepaymentPct>, decimal0>, decimal0>))]
  [PXUIField(DisplayName = "Prepayment Percent")]
  public virtual Decimal? PrepaymentReqPct { get; set; }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  public virtual Decimal? PrepaymentReqPctToRestore { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.prepaymentReqAmt">amount</see> of funds (in the currency of the document)
  /// the customer has to pay before the user can proceed
  /// to shipping and preparing an invoice for the sales order.
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.prepaymentReqAmt))]
  [PXDefault]
  [PXUIField(DisplayName = "Prepayment Amount")]
  [PXFormula(typeof (BqlOperand<Div<Mult<SOOrder.prepaymentReqPct, SOOrder.curyOrderTotal>, decimal100>, IBqlDecimal>.When<BqlOperand<SOOrder.overridePrepayment, IBqlBool>.IsNotEqual<True>>.Else<BqlField<SOOrder.curyPrepaymentReqAmt, IBqlDecimal>.FromCurrent>))]
  public virtual Decimal? CuryPrepaymentReqAmt { get; set; }

  /// <summary>
  /// The amount of funds the customer has to pay before the user can proceed to shipping and preparing
  /// an invoice for the sales order.
  /// </summary>
  /// <remarks>
  /// <para>If the <see cref="T:PX.Objects.SO.SOOrder.prepaymentReqPct">Prepayment Percent</see> changed, than this amount is updated
  /// automatically, and if this value changed, the system automatically updates the
  /// <see cref="T:PX.Objects.SO.SOOrder.prepaymentReqPct">Prepayment Percent</see> to the appropriate percent based on the amount.
  /// </para>
  /// <para>This field is available when the <see cref="T:PX.Objects.CS.Terms.prepaymentRequired">Prepayment Required</see> field
  /// is <see langword="true" /> and becomes available only for sales orders with the
  /// <see cref="T:PX.Objects.SO.SOBehavior.sO">Sales Orders</see> automation behavior when the
  /// <see cref="T:PX.Objects.SO.SOOrder.overridePrepayment">Override Prepayment</see> field is <see langword="true" />.</para>
  /// </remarks>
  [PXDBBaseCury(null, null)]
  [PXDefault]
  public virtual Decimal? PrepaymentReqAmt { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.paymentOverall">sum</see> of amounts of child payments (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.paymentOverall))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryPaymentOverall { get; set; }

  /// <summary>
  /// The sum of <see cref="T:PX.Objects.SO.SOAdjust.curyAdjdAmt" /> and <see cref="T:PX.Objects.SO.SOAdjust.curyAdjdBilledAmt" />
  /// of all order's payments for which the following condition is <see langword="true" />:
  /// <see cref="T:PX.Objects.SO.SOAdjust.voided" /> is <see langword="false" />
  /// or <see cref="T:PX.Objects.SO.SOAdjust.isCCPayment" /> is <see langword="false" /> or <see cref="T:PX.Objects.SO.SOAdjust.isCCAuthorized" />
  /// is <see langword="true" /> or <see cref="T:PX.Objects.SO.SOAdjust.isCCCaptured" /> is <see langword="true" /> or
  /// <see cref="T:PX.Objects.SO.SOAdjust.paymentReleased" /> is <see langword="true" />.
  /// </summary>
  /// <remarks>
  /// The field is used to calculate <see cref="T:PX.Objects.SO.SOOrder.prepaymentReqSatisfied" />.
  /// </remarks>
  [PXDBBaseCury(null, null)]
  [PXDefault]
  public virtual Decimal? PaymentOverall { get; set; }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that the amount of the prepayment or
  /// repayments applied to the sales order is greater than or equal to the value specified in the
  /// <see cref="T:PX.Objects.SO.SOOrder.curyPrepaymentReqAmt">Prepayment Amount</see> field.
  /// </summary>
  /// <remarks>
  /// This field is not available only for sales orders with the <see cref="T:PX.Objects.SO.SOBehavior.sO">Sales Orders</see>
  /// automation behavior when the <see cref="T:PX.Objects.CS.Terms.prepaymentRequired">Prepayment Required</see> field is
  /// <see langword="true" />.
  /// </remarks>
  [PXDBBool]
  [PXDefault(typeof (BqlOperand<True, IBqlBool>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.allowsRequiredPrepayment, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.curyPaymentOverall, GreaterEqual<SOOrder.curyPrepaymentReqAmt>>>>>.Or<BqlOperand<SOOrder.curyUnpaidBalance, IBqlDecimal>.IsEqual<decimal0>>>>.Else<False>))]
  [PXUIField(DisplayName = "Prepayment Requirements Satisfied", Enabled = false)]
  public virtual bool? PrepaymentReqSatisfied { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order is completed independently of the
  /// <see cref="T:PX.Objects.SO.SOOrder.openLineCntr" />.
  /// </summary>
  /// <remarks>The field is used for drop-ship processing.</remarks>
  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? ForceCompleteOrder { get; set; }

  /// <summary>
  /// The counter of payments added to the order for which the following condition is <see langword="true" />:
  /// <see cref="T:PX.Objects.SO.SOAdjust.isCCPayment" /> is
  /// <see langword="true" />, <see cref="T:PX.Objects.SO.SOAdjust.syncLock" /> is <see langword="true" />,
  /// <see cref="T:PX.Objects.SO.SOAdjust.syncLockReason" /> is not equal to <see cref="T:PX.Objects.AR.ARPayment.syncLockReason.newCard" />, and
  /// <see cref="T:PX.Objects.SO.SOAdjust.curyAdjdAmt" /> is not equal to 0.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PaymentsNeedValidationCntr { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order does not have the transfer behavior and has a related cash
  /// invoice.
  /// </summary>
  [PXBool]
  [PXUIField(Enabled = false, Visible = false)]
  [PXFormula(typeof (IIf<Where<SOOrder.defaultTranType, NotEqual<INTranType.transfer>, And<SOOrder.aRDocType, NotIn3<PX.Objects.AR.ARDocType.cashSale, PX.Objects.AR.ARDocType.cashReturn, PX.Objects.AR.ARDocType.cashSaleOrReturn>>>, True, False>))]
  public virtual bool? ArePaymentsApplicable { get; set; }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that <see cref="T:PX.Objects.SO.SOOrder.curyPaymentOverall" />
  /// is greater than 0 and <see cref="T:PX.Objects.SO.SOOrder.curyPaymentOverall" /> is greater than or equal to sum of
  /// <see cref="T:PX.Objects.SO.SOOrder.curyUnbilledOrderTotal" /> and <see cref="T:PX.Objects.SO.SOOrder.curyBilledPaymentTotal" />.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.curyPaymentOverall, Greater<decimal0>>>>>.And<BqlOperand<SOOrder.curyPaymentOverall, IBqlDecimal>.IsGreaterEqual<BqlOperand<SOOrder.curyUnbilledOrderTotal, IBqlDecimal>.Add<SOOrder.curyBilledPaymentTotal>>>))]
  public virtual bool? IsFullyPaid { get; set; }

  /// <summary>
  /// If <see langword="true" />, the Prepayment Required functionality works in the current Sales Order
  /// </summary>
  [PXBool]
  [PXFormula(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.behavior, Equal<SOBehavior.sO>>>>>.And<BqlOperand<SOOrder.aRDocType, IBqlString>.IsNotEqual<PX.Objects.AR.ARDocType.noUpdate>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.behavior, Equal<SOBehavior.rM>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.aRDocType, In3<PX.Objects.AR.ARDocType.invoice, PX.Objects.AR.ARDocType.debitMemo>>>>>.And<BqlOperand<SOOrder.defaultOperation, IBqlString>.IsEqual<SOOperation.issue>>>>))]
  public bool? AllowsRequiredPrepayment { get; set; }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />) that the following condition is
  /// <see langword="true" />: <see cref="T:PX.Objects.SO.SOOrder.behavior" /> is one of <see cref="T:PX.Objects.SO.SOBehavior.sO" />,
  /// <see cref="T:PX.Objects.SO.SOBehavior.iN" />, <see cref="T:PX.Objects.SO.SOBehavior.rM" />, or <see cref="T:PX.Objects.SO.SOBehavior.cM" />,
  /// <see cref="T:PX.Objects.CR.BAccount.isBranch" /> that is related to <see cref="T:PX.Objects.SO.SOOrder.customerID" /> is equal to
  /// <see langword="true" />, and <see cref="T:PX.Objects.SO.SOOrder.defaultTranType" /> is not equal to <see cref="T:PX.Objects.IN.INTranType.transfer" />.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.behavior, In3<SOBehavior.sO, SOBehavior.iN, SOBehavior.rM, SOBehavior.cM>, And<Selector<SOOrder.customerID, PX.Objects.CR.BAccount.isBranch>, Equal<True>, And<SOOrder.defaultTranType, NotEqual<INTranType.transfer>>>>))]
  [PXDefault]
  public virtual bool? IsIntercompany { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PO.POOrder">type</see> of the purchase order for which the sales order
  /// has been created.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.IntercompanyPOOrder" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PO.POOrder.orderType" /> field.
  /// </value>
  /// <remarks>
  /// The field is visible for orders with the <see cref="T:PX.Objects.SO.SOBehavior.iN">Invoice</see> and
  /// <see cref="T:PX.Objects.SO.SOBehavior.sO">Sales Orders</see> automation behavior.
  /// </remarks>
  [PXDBString(2, IsFixed = true)]
  [POOrderType.List]
  [PXUIField(DisplayName = "Related Order Type", Enabled = false, FieldClass = "InterBranch")]
  public virtual string IntercompanyPOType { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PO.POOrder">purchase order</see> for which the sales order has been
  /// created. The field is included in the <see cref="T:PX.Objects.SO.SOOrder.FK.IntercompanyPOOrder" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PO.POOrder.orderNbr" /> field.
  /// </value>
  /// <remarks>
  /// This field is available for orders with the <see cref="T:PX.Objects.SO.SOBehavior.iN">Invoice</see> and
  /// <see cref="T:PX.Objects.SO.SOBehavior.sO">Sales Orders</see> automation behavior.
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Related Order Nbr.", Enabled = false, FieldClass = "InterBranch")]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<SOOrder.intercompanyPOType>>>>))]
  public virtual string IntercompanyPONbr { get; set; }

  /// <summary>
  /// The link to the purchase return for which the return order has been created.
  /// </summary>
  /// <remarks>
  /// This field is available only for orders with the
  /// <see cref="T:PX.Objects.SO.SOBehavior.rM">RMA</see> automation behavior.
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Related PO Return Nbr.", Enabled = false, FieldClass = "InterBranch")]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreturn>>>))]
  public virtual string IntercompanyPOReturnNbr { get; set; }

  /// <summary>
  /// A Boolean value that specifies (if set to <see langword="true" />)
  /// that <see cref="T:PX.Objects.SO.SOOrder.isIntercompany" /> is <see langword="true" /> and
  /// a child <see cref="T:PX.Objects.PO.POLine" /> exists for which the following condition is <see langword="true" />:
  /// <see cref="T:PX.Objects.PO.POLine.inventoryID" /> is <see langword="null" /> and <see cref="T:PX.Objects.PO.POLine.lineType" />
  /// is not equal to <see cref="F:PX.Objects.PO.POLineType.Description" />.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IntercompanyPOWithEmptyInventory { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the related items can be suggested for the order.
  /// </summary>
  [PXBool]
  public virtual bool? SuggestRelatedItems { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order has a related credit memo.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.aRDocType, In3<PX.Objects.AR.ARDocType.creditMemo, PX.Objects.AR.ARDocType.cashReturn>, And<SOOrder.defaultTranType, In3<INTranType.creditMemo, INTranType.return_, INTranType.noUpdate>>>))]
  public virtual bool? IsCreditMemoOrder { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order has the RMA behavior.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.behavior, Equal<SOBehavior.rM>>))]
  public virtual bool? IsRMAOrder { get; set; }

  [PXBool]
  [PXFormula(typeof (Where<SOOrder.behavior, Equal<SOBehavior.mO>>))]
  public virtual bool? IsMixedOrder { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order has the transfer behavior.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.behavior, Equal<SOBehavior.tR>, Or<SOOrder.defaultTranType, Equal<INTranType.transfer>>>))]
  public virtual bool? IsTransferOrder { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order has a related debit memo.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.aRDocType, Equal<PX.Objects.AR.ARDocType.debitMemo>, And<SOOrder.defaultTranType, Equal<INTranType.debitMemo>>>))]
  public virtual bool? IsDebitMemoOrder { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order does not have an invoice.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.aRDocType, Equal<PX.Objects.AR.ARDocType.noUpdate>>))]
  public virtual bool? IsNoAROrder { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order has a related cash invoice.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.aRDocType, In3<PX.Objects.AR.ARDocType.cashSale, PX.Objects.AR.ARDocType.cashReturn, PX.Objects.AR.ARDocType.cashSaleOrReturn>>))]
  public virtual bool? IsCashSaleOrder { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order can be paid.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.isCashSaleOrder, Equal<True>, Or<Selector<SOOrder.orderType, SOOrderType.canHavePayments>, Equal<True>, Or<Selector<SOOrder.orderType, SOOrderType.canHaveRefunds>, Equal<True>>>>))]
  public virtual bool? IsPaymentInfoEnabled { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order type is Manual Invoice Numbering.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<Selector<SOOrder.orderType, SOOrderType.userInvoiceNumbering>, Equal<True>>))]
  public virtual bool? IsUserInvoiceNumbering { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order does not have the transfer behavior.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Where<SOOrder.behavior, NotIn3<SOBehavior.tR, SOBehavior.bL>, And<SOOrder.defaultTranType, NotEqual<INTranType.transfer>>>))]
  public virtual bool? IsFreightAvailable { get; set; }

  /// <summary>
  /// A boolean value that indicates (if set to <see langword="true" />) that separate invoicing
  /// is allowed for the lines where <see cref="T:PX.Objects.SO.SOLine.lineType" /> equals to <see cref="T:PX.Objects.SO.SOLineType.miscCharge" />.
  /// </summary>
  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? IsLegacyMiscBilling { get; set; }

  /// <summary>The expiration date of a blanket sales order.</summary>
  /// <remarks>
  /// When the date in this field is earlier than the current business date, the system displays a warning message
  /// about the expiration of the order, and the order cannot be changed.
  /// </remarks>
  [PXDBDate]
  [PXUIField(DisplayName = "Expires On")]
  public virtual DateTime? ExpireDate { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order is expired.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsExpired { get; set; }

  /// <summary>
  /// The date on which a child order should be generated for the line of the blanket sales order.
  /// </summary>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXDBDate]
  [PXUIField(DisplayName = "Sched. Order Date", Enabled = false, Visible = false)]
  public virtual DateTime? MinSchedOrderDate { get; set; }

  /// <summary>
  /// The quantity of the stock and non-stock items in a blanket sales order distributed among child orders.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. on Child Orders", Enabled = false)]
  public virtual Decimal? QtyOnOrders { get; set; }

  /// <summary>
  /// The summarized quantity of the stock or non-stock items in the blanket sales order that has not been
  /// transferred to child orders.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Quantity", Enabled = false)]
  public virtual Decimal? BlanketOpenQty { get; set; }

  /// <summary>
  /// The counter of the sales order detail lines added to the parent blanket order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BlanketLineCntr { get; set; }

  /// <summary>
  /// The counter of the <see cref="T:PX.Objects.SO.SOAdjust"></see> lines added to the current order and having blanket reference.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BlanketSOAdjustCntr { get; set; }

  /// <summary>
  /// The counter of the special-ordered child lines. Corresponds to <see cref="T:PX.Objects.SO.SOLine.isSpecialOrder" />.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? SpecialLineCntr { get; set; }

  /// <summary>
  /// The number of detail lines with the empty <see cref="P:PX.Objects.SO.SOLine.MarginPct">margin</see>.
  /// Such lines are not considered in the <see cref="T:PX.Objects.SO.SOOrder.marginPct">order margin</see> calculation.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? NoMarginLineCntr { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.SO.SOOrder.CuryFreightAmt">freight amount</see> and <see cref="P:PX.Objects.SO.SOOrder.CuryPremiumFreightAmt">additional freight charges</see>
  /// that are subject to taxes (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.taxableFreightAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxableFreightAmt { get; set; }

  /// <summary>
  /// The sum of <see cref="P:PX.Objects.SO.SOOrder.FreightAmt">freight amount</see> and <see cref="P:PX.Objects.SO.SOOrder.PremiumFreightAmt">additional freight charges</see>
  /// that are subject to taxes.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableFreightAmt { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOLine.CuryExtCost">extended costs</see> of the detail lines for which the <see cref="P:PX.Objects.SO.SOLine.MarginPct">margin</see> is not empty
  /// (in the currency of the document).
  /// </summary>
  /// <remarks>
  /// The line's <see cref="P:PX.Objects.SO.SOLine.CuryExtCost">extended cost</see> is used with the negative sign for the receipt lines.
  /// </remarks>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.salesCostTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CurySalesCostTotal { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOLine.ExtCost">extended costs</see> of the detail lines for which the <see cref="P:PX.Objects.SO.SOLine.MarginPct">margin</see> is not empty.
  /// </summary>
  /// <remarks>
  /// The line's <see cref="P:PX.Objects.SO.SOLine.ExtCost">extended cost</see> is used with the negative sign for the receipt lines.
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SalesCostTotal { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOLine.CuryNetSales">line amounts without the taxes and with the applied group and document discounts</see> (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.netSalesTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryNetSalesTotal { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOLine.NetSales">line amounts without the taxes and with the applied group and document discounts</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? NetSalesTotal { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOOrder.CuryNetSalesTotal">line amounts without the taxes and with the applied group and document discounts</see>
  /// and the <see cref="P:PX.Objects.SO.SOOrder.CuryTaxableFreightAmt">taxable freight amount</see> (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.orderNetSales))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrderNetSales { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOOrder.NetSalesTotal">line amounts without the taxes and with the applied group and document discounts</see>
  /// and the <see cref="P:PX.Objects.SO.SOOrder.TaxableFreightAmt">taxable freight amount</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderNetSales { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOOrder.CurySalesCostTotal">extended costs of the detail lines</see>
  /// and the <see cref="P:PX.Objects.SO.SOOrder.CuryFreightCost">freight cost</see> (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.orderCosts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrderCosts { get; set; }

  /// <summary>
  /// The sum of the <see cref="P:PX.Objects.SO.SOOrder.SalesCostTotal">extended costs of the detail lines</see>
  /// and the <see cref="P:PX.Objects.SO.SOOrder.FreightCost">freight cost</see> (in the currency of the document).
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderCosts { get; set; }

  /// <summary>
  /// The order's estimated margin amount (in the currency of the document).
  /// </summary>
  /// <remarks>The value is not available for the transfer order.</remarks>
  [PXDBCurrency(typeof (SOOrder.curyInfoID), typeof (SOOrder.marginAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Est. Margin Amount")]
  public virtual Decimal? CuryMarginAmt { get; set; }

  /// <summary>The order's estimated margin amount.</summary>
  /// <remarks>The value is not available for the transfer order.</remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? MarginAmt { get; set; }

  /// <summary>The order's estimated margin percent.</summary>
  /// <remarks>The value is not available for the transfer order.</remarks>
  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Est. Margin (%)")]
  public virtual Decimal? MarginPct { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the Discounts tab is shown on the Sales Orders (SO301000) form.
  /// </summary>
  [PXBool]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<SOOrder.behavior, NotIn3<SOBehavior.bL, SOBehavior.tR>>, True>, False>))]
  public virtual bool? ShowDiscountsTab { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the Shipments tab is shown on the Sales Orders (SO301000) form.
  /// </summary>
  [PXBool]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<SOOrder.behavior, NotEqual<SOBehavior.bL>>, True>, False>))]
  public virtual bool? ShowShipmentsTab { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the Child Orders tab is shown on the Sales Orders (SO301000).
  /// </summary>
  [PXBool]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<SOOrder.behavior, Equal<SOBehavior.bL>>, True>, False>))]
  public virtual bool? ShowOrdersTab { get; set; }

  /// <summary>
  /// The counter of the sales order details added to child orders.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChildLineCntr { get; set; }

  /// <inheritdoc cref="P:PX.Data.EP.IAssign.WorkgroupID" />
  int? IAssign.WorkgroupID { get; set; }

  /// <inheritdoc cref="P:PX.Data.EP.IAssign.OwnerID" />
  int? IAssign.OwnerID { get; set; }

  bool? IInvoice.Released
  {
    get => new bool?(true);
    set
    {
    }
  }

  /// <summary>
  /// A Boolean value that indicates whether orchestration is allowed for the current <see cref="T:PX.Objects.SO.SOOrderType">sales order type</see>.
  /// </summary>
  [PXUnboundDefault(false)]
  [PXBool]
  public bool? IsOrchestrationAllowed { get; set; }

  /// <summary>The status of the order orchestration.</summary>
  [PXDBString(2, IsFixed = true)]
  [PX.Objects.SO.OrchestrationStatus.List]
  [PXDefault("NA")]
  [PXUIField(DisplayName = "Orchestration Status", Enabled = false, FieldClass = "OrderOrchestration")]
  public string OrchestrationStatus { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderType.OrchestrationStrategy" />
  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (Switch<Case<Where<SOOrder.willCall, Equal<True>>, OrchestrationStrategies.doNotOrchestrate>, Selector<SOOrder.orderType, SOOrderType.orchestrationStrategy>>))]
  [PXUIField(DisplayName = "Fulfillment Strategy", FieldClass = "OrderOrchestration")]
  [OrchestrationStrategies.List]
  [PXFormula(typeof (Default<SOOrder.willCall>))]
  public string OrchestrationStrategy { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderType.LimitWarehouse" />
  [PXDBBool]
  [PXUIField(DisplayName = "Limit Number of Fulfillment Warehouses", FieldClass = "OrderOrchestration")]
  [PXDefault(typeof (Switch<Case<Where<SOOrder.orchestrationStrategy, Equal<OrchestrationStrategies.doNotOrchestrate>, Or<SOOrder.orchestrationStrategy, IsNull>>, False>, Switch<Case<Where<Selector<SOOrder.orderType, SOOrderType.orchestrationStrategy>, NotEqual<SOOrder.orchestrationStrategy>>, False>, Selector<SOOrder.orderType, SOOrderType.limitWarehouse>>>))]
  [PXFormula(typeof (Default<SOOrder.orchestrationStrategy>))]
  public bool? LimitWarehouse { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrderType.NumberOfWarehouses" />
  [PXDefault(typeof (Switch<Case<Where<SOOrder.limitWarehouse, Equal<False>>, int0>, Switch<Case<Where<SOOrder.limitWarehouse, Equal<True>, And<Selector<SOOrder.orderType, SOOrderType.orchestrationStrategy>, NotEqual<SOOrder.orchestrationStrategy>>>, int1>, Selector<SOOrder.orderType, SOOrderType.numberOfWarehouses>>>))]
  [PXFormula(typeof (Default<SOOrder.limitWarehouse>))]
  [PXUIVerify]
  [PXDBInt]
  [PXUIField(DisplayName = "Number of Fulfillment Warehouses", FieldClass = "OrderOrchestration")]
  public int? NumberOfWarehouses { get; set; }

  public class PK : PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>
  {
    public static SOOrder Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (SOOrder) PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.FindBy(graph, (object) orderType, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOOrder>.By<SOOrder.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<SOOrder>.By<SOOrder.customerID, SOOrder.customerLocationID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<SOOrder>.By<SOOrder.contactID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<SOOrder>.By<SOOrder.branchID>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOOrder>.By<SOOrder.orderType>
    {
    }

    public class BillingAddress : 
      PrimaryKeyOf<SOBillingAddress>.By<SOBillingAddress.addressID>.ForeignKeyOf<SOOrder>.By<SOOrder.billAddressID>
    {
    }

    public class ShippingAddress : 
      PrimaryKeyOf<SOShippingAddress>.By<SOShippingAddress.addressID>.ForeignKeyOf<SOOrder>.By<SOOrder.shipAddressID>
    {
    }

    public class BillingContact : 
      PrimaryKeyOf<SOBillingContact>.By<SOBillingContact.contactID>.ForeignKeyOf<SOOrder>.By<SOOrder.billContactID>
    {
    }

    public class ShippingContact : 
      PrimaryKeyOf<SOShippingContact>.By<SOShippingContact.contactID>.ForeignKeyOf<SOOrder>.By<SOOrder.shipContactID>
    {
    }

    public class FreightTaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<SOOrder>.By<SOOrder.freightTaxCategoryID>
    {
    }

    public class FOBPoint : 
      PrimaryKeyOf<PX.Objects.CS.FOBPoint>.By<PX.Objects.CS.FOBPoint.fOBPointID>.ForeignKeyOf<SOOrder>.By<SOOrder.fOBPoint>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<SOInvoice>.By<SOInvoice.docType, SOInvoice.refNbr>.ForeignKeyOf<SOOrder>.By<SOOrder.aRDocType, SOOrder.invoiceNbr>
    {
    }

    public class ShipTerms : 
      PrimaryKeyOf<PX.Objects.CS.ShipTerms>.By<PX.Objects.CS.ShipTerms.shipTermsID>.ForeignKeyOf<SOOrder>.By<SOOrder.shipTermsID>
    {
    }

    public class ShippingZone : 
      PrimaryKeyOf<PX.Objects.CS.ShippingZone>.By<PX.Objects.CS.ShippingZone.zoneID>.ForeignKeyOf<SOOrder>.By<SOOrder.shipZoneID>
    {
    }

    public class Carrier : 
      PrimaryKeyOf<PX.Objects.CS.Carrier>.By<PX.Objects.CS.Carrier.carrierID>.ForeignKeyOf<SOOrder>.By<SOOrder.shipVia>
    {
    }

    public class DefaultSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrder>.By<SOOrder.defaultSiteID>
    {
    }

    public class DestinationSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrder>.By<SOOrder.destinationSiteID>
    {
    }

    public class OriginalOrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOOrder>.By<SOOrder.origOrderType>
    {
    }

    public class OriginalOrder : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOOrder>.By<SOOrder.origOrderType, SOOrder.origOrderNbr>
    {
    }

    public class LastSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrder>.By<SOOrder.lastSiteID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOOrder>.By<SOOrder.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<SOOrder>.By<SOOrder.curyID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<SOOrder>.By<SOOrder.taxZoneID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<SOOrder>.By<SOOrder.projectID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<SOOrder>.By<SOOrder.salesPersonID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<SOOrder>.By<SOOrder.termsID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<SOOrder>.By<SOOrder.workgroupID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<SOOrder>.By<SOOrder.ownerID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOOrder>.By<SOOrder.destinationSiteID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<SOOrder>.By<SOOrder.paymentMethodID>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<PX.Objects.AR.CustomerPaymentMethod>.By<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<SOOrder>.By<SOOrder.pMInstanceID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOOrder>.By<SOOrder.cashAccountID>
    {
    }

    public class IntercompanyPOOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<SOOrder>.By<SOOrder.intercompanyPOType, SOOrder.intercompanyPONbr>
    {
    }
  }

  public class Events : PXEntityEventBase<SOOrder>.Container<SOOrder.Events>
  {
    public PXEntityEvent<SOOrder> ShipmentCreationFailed;
    public PXEntityEvent<SOOrder> OrderDeleted;
    public PXEntityEvent<SOOrder> PaymentRequirementsSatisfied;
    public PXEntityEvent<SOOrder> PaymentRequirementsViolated;
    public PXEntityEvent<SOOrder> ObtainedPaymentInPendingProcessing;
    public PXEntityEvent<SOOrder> LostLastPaymentInPendingProcessing;
    public PXEntityEvent<SOOrder> CreditLimitSatisfied;
    public PXEntityEvent<SOOrder> CreditLimitViolated;
    public PXEntityEvent<SOOrder> BlanketCompleted;
    public PXEntityEvent<SOOrder> BlanketReopened;
    public PXEntityEvent<SOOrder, PX.Objects.CR.CRQuote> OrderCreatedFromQuote;
    public PXEntityEvent<SOOrder> GotShipmentConfirmed;
    public PXEntityEvent<SOOrder> GotShipmentCorrected;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.RiskLineCntr" />
  public abstract class riskLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.riskLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Selected" />
  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.selected>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderType" />
  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.orderType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Behavior" />
  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.behavior>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ARDocType" />
  public abstract class aRDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.aRDocType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsInvoiceOrder" />
  public abstract class isInvoiceOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isInvoiceOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderNbr" />
  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.orderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CustomerID" />
  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.customerID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CustomerLocationID" />
  public abstract class customerLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.customerLocationID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ContactID" />
  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.contactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BranchID" />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.branchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderDate" />
  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.orderDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CustomerOrderNbr" />
  public abstract class customerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.customerOrderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CustomerRefNbr" />
  public abstract class customerRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.customerRefNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CancelDate" />
  public abstract class cancelDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.cancelDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.RequestDate" />
  public abstract class requestDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.requestDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipDate" />
  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.shipDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DontApprove" />
  public abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.dontApprove>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Hold" />
  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.hold>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Approved" />
  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.approved>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Rejected" />
  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.rejected>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Emailed" />
  public abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.emailed>
  {
  }

  public abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.printed>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CreditHold" />
  public abstract class creditHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.creditHold>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Completed" />
  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.completed>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Cancelled" />
  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.cancelled>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenDoc" />
  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.openDoc>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipmentDeleted" />
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public abstract class shipmentDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.shipmentDeleted>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BackOrdered" />
  public abstract class backOrdered : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.backOrdered>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.LastSiteID" />
  public abstract class lastSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.lastSiteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.LastShipDate" />
  public abstract class lastShipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.lastShipDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BillSeparately" />
  public abstract class billSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.billSeparately>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipSeparately" />
  public abstract class shipSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.shipSeparately>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Status" />
  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.status>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.NoteID" />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrder.noteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.LineCntr" />
  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.lineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BilledCntr" />
  public abstract class billedCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.billedCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ReleasedCntr" />
  public abstract class releasedCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.releasedCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PaymentCntr" />
  public abstract class paymentCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.paymentCntr>
  {
  }

  public abstract class authorizedPaymentCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrder.authorizedPaymentCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderDesc" />
  public abstract class orderDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.orderDesc>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BillAddressID" />
  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.billAddressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipAddressID" />
  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.shipAddressID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BillContactID" />
  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.billContactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipContactID" />
  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.shipContactID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryID" />
  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.curyID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryInfoID" />
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOOrder.curyInfoID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryLineDiscTotal" />
  public abstract class curyLineDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyLineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.LineDiscTotal" />
  public abstract class lineDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.lineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryGroupDiscTotal" />
  public abstract class curyGroupDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyGroupDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.GroupDiscTotal" />
  public abstract class groupDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.groupDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryDocumentDiscTotal" />
  public abstract class curyDocumentDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyDocumentDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DocumentDiscTotal" />
  public abstract class documentDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.documentDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DiscTot" />
  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.discTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryDiscTot" />
  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyDiscTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOrderDiscTotal" />
  public abstract class curyOrderDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyOrderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderDiscTotal" />
  public abstract class orderDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.orderDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOrderTotal" />
  public abstract class curyOrderTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyOrderTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderTotal" />
  public abstract class orderTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.orderTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryLineTotal" />
  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyLineTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryLineTotal" />
  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.lineTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryVatExemptTotal" />
  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyVatExemptTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.VatExemptTotal" />
  public abstract class vatExemptTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.vatExemptTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryVatTaxableTotal" />
  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyVatTaxableTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.VatTaxableTotal" />
  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.vatTaxableTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryTaxTotal" />
  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyTaxTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.TaxTotal" />
  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.taxTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryPremiumFreightAmt" />
  public abstract class curyPremiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyPremiumFreightAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PremiumFreightAmt" />
  public abstract class premiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.premiumFreightAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryFreightCost" />
  public abstract class curyFreightCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyFreightCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FreightCost" />
  public abstract class freightCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.freightCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FreightCostIsValid" />
  public abstract class freightCostIsValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.freightCostIsValid>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsPackageValid" />
  public abstract class isPackageValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isPackageValid>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OverrideFreightAmount" />
  public abstract class overrideFreightAmount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.overrideFreightAmount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FreightAmountSource" />
  public abstract class freightAmountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.freightAmountSource>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryFreightAmt" />
  public abstract class curyFreightAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyFreightAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FreightAmt" />
  public abstract class freightAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.freightAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryFreightTot" />
  public abstract class curyFreightTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyFreightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FreightTot" />
  public abstract class freightTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.freightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FreightTaxCategoryID" />
  public abstract class freightTaxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.freightTaxCategoryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryMiscTot" />
  public abstract class curyMiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyMiscTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.MiscTot" />
  public abstract class miscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.miscTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryGoodsExtPriceTotal" />
  public abstract class curyGoodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyGoodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.GoodsExtPriceTotal" />
  public abstract class goodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.goodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryMiscExtPriceTotal" />
  public abstract class curyMiscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyMiscExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.MiscExtPriceTotal" />
  public abstract class miscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.miscExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryDetailExtPriceTotal" />
  public abstract class curyDetailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyDetailExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DetailExtPriceTotal" />
  public abstract class detailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.detailExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderQty" />
  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.orderQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderWeight" />
  public abstract class orderWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.orderWeight>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderVolume" />
  public abstract class orderVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.orderVolume>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOpenOrderTotal" />
  public abstract class curyOpenOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyOpenOrderTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenOrderTotal" />
  public abstract class openOrderTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.openOrderTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOpenLineTotal" />
  public abstract class curyOpenLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyOpenLineTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenLineTotal" />
  public abstract class openLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.openLineTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOpenDiscTotal" />
  public abstract class curyOpenDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyOpenDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenDiscTotal" />
  public abstract class openDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.openDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOpenTaxTotal" />
  public abstract class curyOpenTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyOpenTaxTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenTaxTotal" />
  public abstract class openTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.openTaxTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenOrderQty" />
  public abstract class openOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.openOrderQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnbilledOrderTotal" />
  public abstract class curyUnbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnbilledOrderTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnbilledOrderTotal" />
  public abstract class unbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unbilledOrderTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnbilledLineTotal" />
  public abstract class curyUnbilledLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnbilledLineTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnbilledLineTotal" />
  public abstract class unbilledLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unbilledLineTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnbilledMiscTot" />
  public abstract class curyUnbilledMiscTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnbilledMiscTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnbilledMiscTot" />
  public abstract class unbilledMiscTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unbilledMiscTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnbilledTaxTotal" />
  public abstract class curyUnbilledTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnbilledTaxTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnbilledTaxTotal" />
  public abstract class unbilledTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unbilledTaxTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnbilledDiscTotal" />
  public abstract class curyUnbilledDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnbilledDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnbilledDiscTotal" />
  public abstract class unbilledDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unbilledDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryBilledFreightTot" />
  public abstract class curyBilledFreightTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyBilledFreightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BilledFreightTot" />
  public abstract class billedFreightTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.billedFreightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnbilledFreightTot" />
  public abstract class curyUnbilledFreightTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnbilledFreightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnbilledFreightTot" />
  public abstract class unbilledFreightTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unbilledFreightTot>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnbilledOrderQty" />
  public abstract class unbilledOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unbilledOrderQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryControlTotal" />
  public abstract class curyControlTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyControlTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ControlTotal" />
  public abstract class controlTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.controlTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryPaymentTotal" />
  public abstract class curyPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyPaymentTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PaymentTotal" />
  public abstract class paymentTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.paymentTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OverrideTaxZone" />
  public abstract class overrideTaxZone : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.overrideTaxZone>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.TaxZoneID" />
  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.taxZoneID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.TaxCalcMode" />
  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.taxCalcMode>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ExternalTaxExemptionNumber" />
  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.externalTaxExemptionNumber>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.AvalaraCustomerUsageType" />
  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.avalaraCustomerUsageType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ProjectID" />
  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.projectID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipComplete" />
  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.shipComplete>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FOBPoint" />
  public abstract class fOBPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.fOBPoint>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipVia" />
  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.shipVia>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.WillCall" />
  public abstract class willCall : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.willCall>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PackageLineCntr" />
  public abstract class packageLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.packageLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PackageWeight" />
  public abstract class packageWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.packageWeight>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UseCustomerAccount" />
  public abstract class useCustomerAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.useCustomerAccount>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Resedential" />
  public abstract class resedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.resedential>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.SaturdayDelivery" />
  public abstract class saturdayDelivery : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.saturdayDelivery>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.GroundCollect" />
  public abstract class groundCollect : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.groundCollect>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Insurance" />
  public abstract class insurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.insurance>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.Priority" />
  public abstract class priority : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOOrder.priority>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.SalesPersonID" />
  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.salesPersonID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CommnPct" />
  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.commnPct>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.TermsID" />
  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.termsID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DueDate" />
  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.dueDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DiscDate" />
  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.discDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.InvoiceNbr" />
  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.invoiceNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.InvoiceDate" />
  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.invoiceDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.FinPeriodID" />
  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.finPeriodID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.WorkgroupID" />
  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.workgroupID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OwnerID" />
  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.ownerID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.EmployeeID" />
  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.employeeID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrder.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrder.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrder.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrder.lastModifiedDateTime>
  {
  }

  public abstract class gotReadyForArchiveAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrder.gotReadyForArchiveAt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrder.Tstamp>
  {
  }

  /// <summary>
  /// The account name of the <see cref="T:PX.Objects.AR.Customer">customer</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AR.Customer.acctName" /> field.
  /// </value>
  public abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.customerID_Customer_acctName>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryTermsDiscAmt" />
  public abstract class curyTermsDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyTermsDiscAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.TermsDiscAmt" />
  public abstract class termsDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.termsDiscAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipTermsID" />
  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.shipTermsID>
  {
    public class PreventEditIfSOExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<PX.Objects.CS.ShipTerms.freightAmountSource>>.On<ShipTermsMaint>.IfExists<Select<SOOrder, Where<SOOrder.shipTermsID, Equal<Current<PX.Objects.CS.ShipTerms.shipTermsID>>>>>
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object so,
        string fld,
        string tbl,
        string foreignTbl)
      {
        return PXMessages.LocalizeFormat("Cannot change shipping terms because current shipping terms are used in the {1} sales order of the {0} type.", new object[2]
        {
          (object) ((SOOrder) so).OrderType,
          (object) ((SOOrder) so).OrderNbr
        });
      }
    }
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipZoneID" />
  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.shipZoneID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.InclCustOpenOrders" />
  public abstract class inclCustOpenOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.inclCustOpenOrders>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShipmentCntr" />
  public abstract class shipmentCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.shipmentCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenShipmentCntr" />
  public abstract class openShipmentCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.openShipmentCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenSiteCntr" />
  public abstract class openSiteCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.openSiteCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.SiteCntr" />
  public abstract class siteCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.siteCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OpenLineCntr" />
  public abstract class openLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.openLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DefaultSiteID" />
  public abstract class defaultSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.defaultSiteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DestinationSiteID" />
  public abstract class destinationSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.destinationSiteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DestinationSiteIdErrorMessage" />
  public abstract class destinationSiteIdErrorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.destinationSiteIdErrorMessage>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DefaultOperation" />
  public abstract class defaultOperation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.defaultOperation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ActiveOperationsCntr" />
  public abstract class activeOperationsCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrder.activeOperationsCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DefaultTranType" />
  public abstract class defaultTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.defaultTranType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrigOrderType" />
  public abstract class origOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.origOrderType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrigOrderNbr" />
  public abstract class origOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.origOrderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ManDisc" />
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public abstract class manDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.manDisc>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryManDisc" />
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public abstract class curyManDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyManDisc>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ApprovedCredit" />
  public abstract class approvedCredit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.approvedCredit>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ApprovedCreditByPayment" />
  public abstract class approvedCreditByPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.approvedCreditByPayment>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ApprovedCreditAmt" />
  public abstract class approvedCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.approvedCreditAmt>
  {
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INSite.descr">description</see> of the <see cref="T:PX.Objects.IN.INSite">warehouse</see>
  /// from which the goods should be shipped.
  /// </summary>
  public abstract class defaultSiteID_INSite_descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.defaultSiteID_INSite_descr>
  {
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.CS.Carrier.description">description</see> of the <see cref="T:PX.Objects.CS.Carrier">ship via code</see>
  /// that represents the carrier and its service to be used for shipping the ordered goods.
  /// </summary>
  public abstract class shipVia_Carrier_description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.shipVia_Carrier_description>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PaymentMethodID" />
  public abstract class paymentMethodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.paymentMethodID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PMInstanceID" />
  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.pMInstanceID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CashAccountID" />
  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.cashAccountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ExtRefNbr" />
  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrder.extRefNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UpdateNextNumber" />
  public abstract class updateNextNumber : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.updateNextNumber>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnreleasedPaymentAmt" />
  public abstract class curyUnreleasedPaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnreleasedPaymentAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnreleasedPaymentAmt" />
  public abstract class unreleasedPaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unreleasedPaymentAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryCCAuthorizedAmt" />
  public abstract class curyCCAuthorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyCCAuthorizedAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CCAuthorizedAmt" />
  public abstract class cCAuthorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.cCAuthorizedAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryPaidAmt" />
  public abstract class curyPaidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyPaidAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PaidAmt" />
  public abstract class paidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.paidAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryBilledPaymentTotal" />
  public abstract class curyBilledPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyBilledPaymentTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BilledPaymentTotal" />
  public abstract class billedPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.billedPaymentTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryTransferredToChildrenPaymentTotal" />
  public abstract class curyTransferredToChildrenPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyTransferredToChildrenPaymentTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.TransferredToChildrenPaymentTotal" />
  public abstract class transferredToChildrenPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.transferredToChildrenPaymentTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryUnpaidBalance" />
  public abstract class curyUnpaidBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnpaidBalance>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.UnpaidBalance" />
  public abstract class unpaidBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.unpaidBalance>
  {
  }

  public abstract class curyUnrefundedBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyUnrefundedBalance>
  {
  }

  public abstract class unrefundedBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.unrefundedBalance>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsManualPackage" />
  public abstract class isManualPackage : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isManualPackage>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsTaxValid" />
  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isTaxValid>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsOpenTaxValid" />
  public abstract class isOpenTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isOpenTaxValid>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsUnbilledTaxValid" />
  public abstract class isUnbilledTaxValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.isUnbilledTaxValid>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ExternalTaxesImportInProgress" />
  public abstract class externalTaxesImportInProgress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.externalTaxesImportInProgress>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsManualTaxesValid" />
  public abstract class isManualTaxesValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isTaxValid>
  {
  }

  public abstract class orderTaxAllocated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.orderTaxAllocated>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryDocBal" />
  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyDocBal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DocBal" />
  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.docBal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DisableAutomaticDiscountCalculation" />
  public abstract class disableAutomaticDiscountCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.disableAutomaticDiscountCalculation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DeferPriceDiscountRecalculation" />
  public abstract class deferPriceDiscountRecalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.deferPriceDiscountRecalculation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsPriceAndDiscountsValid" />
  public abstract class isPriceAndDiscountsValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.disableAutomaticDiscountCalculation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.DisableAutomaticTaxCalculation" />
  public abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.disableAutomaticTaxCalculation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OverridePrepayment" />
  public abstract class overridePrepayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.overridePrepayment>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PrepaymentReqPct" />
  public abstract class prepaymentReqPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.prepaymentReqPct>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PrepaymentReqPctToRestore" />
  public abstract class prepaymentReqPctToRestore : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.prepaymentReqPctToRestore>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryPrepaymentReqAmt" />
  public abstract class curyPrepaymentReqAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyPrepaymentReqAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PrepaymentReqAmt" />
  public abstract class prepaymentReqAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.prepaymentReqAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryPaymentOverall" />
  public abstract class curyPaymentOverall : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyPaymentOverall>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PaymentOverall" />
  public abstract class paymentOverall : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.paymentOverall>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PrepaymentReqSatisfied" />
  public abstract class prepaymentReqSatisfied : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.prepaymentReqSatisfied>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ForceCompleteOrder" />
  public abstract class forceCompleteOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.forceCompleteOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.PaymentsNeedValidationCntr" />
  public abstract class paymentsNeedValidationCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrder.paymentsNeedValidationCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ArePaymentsApplicable" />
  public abstract class arePaymentsApplicable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.arePaymentsApplicable>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsFullyPaid" />
  public abstract class isFullyPaid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isFullyPaid>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.AllowsRequiredPrepayment" />
  public abstract class allowsRequiredPrepayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.allowsRequiredPrepayment>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsIntercompany" />
  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isIntercompany>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IntercompanyPOType" />
  public abstract class intercompanyPOType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.intercompanyPOType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IntercompanyPONbr" />
  public abstract class intercompanyPONbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.intercompanyPONbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IntercompanyPOReturnNbr" />
  public abstract class intercompanyPOReturnNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.intercompanyPOReturnNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IntercompanyPOWithEmptyInventory" />
  public abstract class intercompanyPOWithEmptyInventory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.intercompanyPOWithEmptyInventory>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.SuggestRelatedItems" />
  public abstract class suggestRelatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.suggestRelatedItems>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsCreditMemoOrder" />
  public abstract class isCreditMemoOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isCreditMemoOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsRMAOrder" />
  public abstract class isRMAOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isRMAOrder>
  {
  }

  public abstract class isMixedOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isMixedOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsTransferOrder" />
  public abstract class isTransferOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isTransferOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsDebitMemoOrder" />
  public abstract class isDebitMemoOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isDebitMemoOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsNoAROrder" />
  public abstract class isNoAROrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isNoAROrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsCashSaleOrder" />
  public abstract class isCashSaleOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isCashSaleOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsPaymentInfoEnabled" />
  public abstract class isPaymentInfoEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.isPaymentInfoEnabled>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsUserInvoiceNumbering" />
  public abstract class isUserInvoiceNumbering : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.isUserInvoiceNumbering>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsFreightAvailable" />
  public abstract class isFreightAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.isFreightAvailable>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsLegacyMiscBilling" />
  public abstract class isLegacyMiscBilling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.isLegacyMiscBilling>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ExpireDate" />
  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOOrder.expireDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.IsExpired" />
  public abstract class isExpired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.isExpired>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.MinSchedOrderDate" />
  public abstract class minSchedOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrder.minSchedOrderDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.QtyOnOrders" />
  public abstract class qtyOnOrders : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.qtyOnOrders>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BlanketOpenQty" />
  public abstract class blanketOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.blanketOpenQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BlanketLineCntr" />
  public abstract class blanketLineCntr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.blanketLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BlanketSOAdjustCntr" />
  public abstract class blanketSOAdjustCntr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.blanketSOAdjustCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.SpecialLineCntr" />
  public abstract class specialLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.specialLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.NoMarginLineCntr" />
  public abstract class noMarginLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.noMarginLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryTaxableFreightAmt" />
  public abstract class curyTaxableFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyTaxableFreightAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.TaxableFreightAmt" />
  public abstract class taxableFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.taxableFreightAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CurySalesCostTotal" />
  public abstract class curySalesCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curySalesCostTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.SalesCostTotal" />
  public abstract class salesCostTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.salesCostTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryNetSalesTotal" />
  public abstract class curyNetSalesTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyNetSalesTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.NetSalesTotal" />
  public abstract class netSalesTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.netSalesTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOrderNetSales" />
  public abstract class curyOrderNetSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrder.curyOrderNetSales>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderNetSales" />
  public abstract class orderNetSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.orderNetSales>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryOrderCosts" />
  public abstract class curyOrderCosts : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyOrderCosts>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.OrderCosts" />
  public abstract class orderCosts : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.orderCosts>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CuryMarginAmt" />
  public abstract class curyMarginAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.curyMarginAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.MarginAmt" />
  public abstract class marginAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.marginAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.MarginPct" />
  public abstract class marginPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOOrder.marginPct>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShowDiscountsTab" />
  public abstract class showDiscountsTab : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.showDiscountsTab>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShowShipmentsTab" />
  public abstract class showShipmentsTab : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.showShipmentsTab>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ShowOrdersTab" />
  public abstract class showOrdersTab : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.showOrdersTab>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.ChildLineCntr" />
  public abstract class childLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.childLineCntr>
  {
  }

  public abstract class isOrchestrationAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrder.isOrchestrationAllowed>
  {
  }

  public abstract class orchestrationStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.orchestrationStatus>
  {
  }

  public abstract class orchestrationStrategy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrder.orchestrationStrategy>
  {
  }

  public abstract class limitWarehouse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrder.limitWarehouse>
  {
  }

  public abstract class numberOfWarehouses : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrder.numberOfWarehouses>
  {
  }
}
