// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Archiving;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO.Attributes;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXPrimaryGraph(typeof (SOShipmentEntry))]
[PXCacheName("Shipment")]
[PXGroupMask(typeof (LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<SOShipment.customerID>>, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<SOShipment.siteID>>>>), WhereRestriction = typeof (Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<Where<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class SOShipment : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  IFreightBase,
  INotable
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _ShipmentNbr;
  protected DateTime? _ShipDate;
  protected string _Operation;
  protected string _ShipmentType;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected string _CustomerOrderNbr;
  protected int? _SiteID;
  protected int? _DestinationSiteID;
  protected string _ShipmentDesc;
  protected int? _ShipAddressID;
  protected int? _ShipContactID;
  protected string _FOBPoint;
  protected string _ShipVia;
  protected bool? _UseCustomerAccount;
  protected bool? _Resedential;
  protected bool? _SaturdayDelivery;
  protected bool? _Insurance;
  protected bool? _GroundCollect;
  protected bool? _LabelsPrinted;
  protected bool? _PickListPrinted;
  protected bool? _ShippedViaCarrier;
  protected string _ShipTermsID;
  protected string _ShipZoneID;
  protected Decimal? _LineTotal;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _CuryFreightCost;
  protected Decimal? _FreightCost;
  protected Decimal? _CuryFreightAmt;
  protected Decimal? _FreightAmt;
  protected Decimal? _CuryPremiumFreightAmt;
  protected Decimal? _PremiumFreightAmt;
  protected Decimal? _CuryTotalFreightAmt;
  protected Decimal? _TotalFreightAmt;
  protected string _TaxCategoryID;
  protected Guid? _NoteID;
  protected bool? _Hold;
  protected bool? _Confirmed;
  protected bool? _Released;
  protected string _Status;
  protected int? _LineCntr;
  protected int? _OrderCntr;
  protected int? _BilledOrderCntr;
  protected int? _UnbilledOrderCntr;
  protected int? _ReleasedOrderCntr;
  protected Decimal? _ControlQty;
  protected Decimal? _ShipmentQty;
  protected Decimal? _ShipmentWeight;
  protected Decimal? _ShipmentVolume;
  protected int? _PackageLineCntr;
  protected Decimal? _PackageWeight;
  protected bool? _IsPackageValid;
  protected int? _PackageCount;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected bool? _Hidden = new bool?(false);
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected Decimal? _FreeItemQtyTot;
  protected string _StatusIsNull;

  [PXBool]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<SOShipment.excluded, Equal<True>>, True>, Current<SOShipment.selected>>))]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (SOSetup.shipmentNumberingID), typeof (SOShipment.shipDate))]
  [PXSelector(typeof (Search2<SOShipment.shipmentNbr, InnerJoin<PX.Objects.IN.INSite, On<SOShipment.FK.Site>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOShipment.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where2<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<SOShipment.shipmentNbr>>>))]
  [PXFieldDescription]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [PXDBDate]
  [PXUIField]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField(DisplayName = "Operation")]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXExtraKey]
  [PXDefault("I")]
  [SOShipmentType.ShortList]
  [PXUIField(DisplayName = "Type")]
  public virtual string ShipmentType
  {
    get => this._ShipmentType;
    set => this._ShipmentType = value;
  }

  [Customer(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
  [CustomerOrOrganizationRestrictor(typeof (SOShipment.shipmentType))]
  [PXRestrictor(typeof (Where<Optional<SOShipment.shipmentType>, Equal<INDocType.transfer>, Or<PX.Objects.AR.Customer.status, IsNull, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.active>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.oneTime>>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.AR.Customer.status)})]
  [PXForeignReference(typeof (Field<SOShipment.customerID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOShipment.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>))]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<SOShipment.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOShipment.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<SOShipment.customerID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<SOShipment.customerLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Customer Order Nbr.", Enabled = false)]
  public virtual string CustomerOrderNbr
  {
    get => this._CustomerOrderNbr;
    set => this._CustomerOrderNbr = value;
  }

  [Site]
  [PXDefault(typeof (Search<PX.Objects.CR.Standalone.Location.cSiteID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOShipment.customerID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<SOShipment.customerLocationID>>>>>))]
  [PXForeignReference(typeof (SOShipment.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXInt]
  [PXFormula(typeof (Selector<SOShipment.siteID, PX.Objects.IN.INSite.branchID>))]
  public virtual int? SiteBranchID { get; set; }

  [ToSite(typeof (INTransferType.twoStep), typeof (SOShipment.siteBranchID), DisplayName = "To Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXForeignReference(typeof (Field<SOShipment.destinationSiteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? DestinationSiteID
  {
    get => this._DestinationSiteID;
    set => this._DestinationSiteID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string ShipmentDesc
  {
    get => this._ShipmentDesc;
    set => this._ShipmentDesc = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Shipping Address")]
  [SOShipmentAddress(typeof (Select2<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Standalone.Location.defAddressID>>>, LeftJoin<SOShipmentAddress, On<SOShipmentAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<SOShipmentAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<SOShipmentAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<SOShipmentAddress.isDefaultAddress, Equal<True>>>>>>>, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOShipment.customerID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<SOShipment.customerLocationID>>>>>))]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  [PXDBInt]
  [SOShipmentContact(typeof (Select2<PX.Objects.CR.Contact, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Standalone.Location.defContactID>>>, LeftJoin<SOShipmentContact, On<SOShipmentContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<SOShipmentContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<SOShipmentContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<SOShipmentContact.isDefaultContact, Equal<True>>>>>>>, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOShipment.customerID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<SOShipment.customerLocationID>>>>>))]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (Search<PX.Objects.CS.FOBPoint.fOBPointID>), DescriptionField = typeof (PX.Objects.CS.FOBPoint.description), CacheGlobal = true)]
  public virtual string FOBPoint
  {
    get => this._FOBPoint;
    set => this._FOBPoint = value;
  }

  [PXUIField(DisplayName = "Ship Via")]
  [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  [PXBool]
  [PXFormula(typeof (Switch<Case2<Where<Selector<SOShipment.shipVia, PX.Objects.CS.Carrier.isCommonCarrier>, NotEqual<True>>, True>, False>))]
  [PXUIField(DisplayName = "Will Call", IsReadOnly = true, Enabled = false)]
  public bool? WillCall { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Customer's Account")]
  public virtual bool? UseCustomerAccount
  {
    get => this._UseCustomerAccount;
    set => this._UseCustomerAccount = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? Resedential
  {
    get => this._Resedential;
    set => this._Resedential = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? SaturdayDelivery
  {
    get => this._SaturdayDelivery;
    set => this._SaturdayDelivery = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? Insurance
  {
    get => this._Insurance;
    set => this._Insurance = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ground Collect")]
  public virtual bool? GroundCollect
  {
    get => this._GroundCollect;
    set => this._GroundCollect = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Labels Printed")]
  public virtual bool? LabelsPrinted
  {
    get => this._LabelsPrinted;
    set => this._LabelsPrinted = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CommercialInvoicesPrinted { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Pick List Printed")]
  public virtual bool? PickListPrinted
  {
    get => this._PickListPrinted;
    set => this._PickListPrinted = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Shipment Confirmation Printed")]
  public virtual bool? ConfirmationPrinted { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ShippedViaCarrier
  {
    get => this._ShippedViaCarrier;
    set => this._ShippedViaCarrier = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (PX.Objects.CS.ShipTerms.shipTermsID), DescriptionField = typeof (PX.Objects.CS.ShipTerms.description), CacheGlobal = true)]
  public virtual string ShipTermsID
  {
    get => this._ShipTermsID;
    set => this._ShipTermsID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Zone ID")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), DescriptionField = typeof (PX.Objects.CS.ShippingZone.description), CacheGlobal = true)]
  public virtual string ShipZoneID
  {
    get => this._ShipZoneID;
    set => this._ShipZoneID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Line Total")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "SO")]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (SOShipment.curyInfoID), typeof (SOShipment.freightCost))]
  [PXUIField(DisplayName = "Freight Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightCost
  {
    get => this._CuryFreightCost;
    set => this._CuryFreightCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Freight Cost", Enabled = false)]
  public virtual Decimal? FreightCost
  {
    get => this._FreightCost;
    set => this._FreightCost = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Freight Price")]
  public virtual bool? OverrideFreightAmount { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PX.Objects.CS.FreightAmountSource]
  [PXUIField(DisplayName = "Invoice Freight Price Based On", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<SOShipment.overrideFreightAmount, Equal<True>>, FreightAmountSourceAttribute.shipmentBased, Case<Where<SOShipment.orderCntr, Equal<int0>, And<SOShipment.shipTermsID, IsNull>>, Null>>, IsNull<Selector<SOShipment.shipTermsID, PX.Objects.CS.ShipTerms.freightAmountSource>, Current<SOShipment.freightAmountSource>>>))]
  public virtual string FreightAmountSource { get; set; }

  [PXDBCurrency(typeof (SOShipment.curyInfoID), typeof (SOShipment.freightAmt))]
  [PXUIField(DisplayName = "Freight Price")]
  [PXUIVerify]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFreightAmt
  {
    get => this._CuryFreightAmt;
    set => this._CuryFreightAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Freight Price", Enabled = false)]
  public virtual Decimal? FreightAmt
  {
    get => this._FreightAmt;
    set => this._FreightAmt = value;
  }

  [PXDBCurrency(typeof (SOShipment.curyInfoID), typeof (SOShipment.premiumFreightAmt))]
  [PXUIField(DisplayName = "Premium Freight Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryPremiumFreightAmt
  {
    get => this._CuryPremiumFreightAmt;
    set => this._CuryPremiumFreightAmt = value;
  }

  [PXDBDecimal(4)]
  public virtual Decimal? PremiumFreightAmt
  {
    get => this._PremiumFreightAmt;
    set => this._PremiumFreightAmt = value;
  }

  [PXDBCurrency(typeof (SOShipment.curyInfoID), typeof (SOShipment.totalFreightAmt))]
  [PXFormula(typeof (Add<SOShipment.curyPremiumFreightAmt, SOShipment.curyFreightAmt>))]
  [PXUIField(DisplayName = "Total Freight Price", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTotalFreightAmt
  {
    get => this._CuryTotalFreightAmt;
    set => this._CuryTotalFreightAmt = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Total Freight Price")]
  public virtual Decimal? TotalFreightAmt
  {
    get => this._TotalFreightAmt;
    set => this._TotalFreightAmt = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXDefault(typeof (Search<PX.Objects.CS.Carrier.taxCategoryID, Where<PX.Objects.CS.Carrier.carrierID, Equal<Current<SOShipment.shipVia>>>>))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXSearchable(256 /*0x0100*/, "{0}: {1} - {3}", new System.Type[] {typeof (SOShipment.shipmentType), typeof (SOShipment.shipmentNbr), typeof (SOShipment.customerID), typeof (PX.Objects.AR.Customer.acctName)}, new System.Type[] {typeof (SOShipment.shipVia), typeof (SOShipment.shipmentDesc)}, NumberFields = new System.Type[] {typeof (SOShipment.shipmentNbr)}, Line1Format = "{0:d}{1}{2}{3}", Line1Fields = new System.Type[] {typeof (SOShipment.shipDate), typeof (SOShipment.status), typeof (SOShipment.shipmentQty), typeof (SOShipment.shipVia)}, Line2Format = "{0}{2}{3}{4}{5}", Line2Fields = new System.Type[] {typeof (SOShipment.shipmentDesc), typeof (SOShipment.shipAddressID), typeof (SOAddress.addressLine1), typeof (SOAddress.addressLine2), typeof (SOAddress.city), typeof (SOAddress.state)}, MatchWithJoin = typeof (InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<SOShipment.customerID>>>), SelectForFastIndexing = typeof (Select2<SOShipment, InnerJoin<PX.Objects.AR.Customer, On<SOShipment.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<SOShipment.shipmentNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<SOShipment.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<SOShipment.shipmentNbr>>>))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBBool]
  [PXDefault(typeof (SOSetup.holdShipments))]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? Confirmed
  {
    get => this._Confirmed;
    set => this._Confirmed = value;
  }

  [PXDBRestrictionBool(typeof (SOShipment.confirmed))]
  public virtual bool? ConfirmedToVerify { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Picked")]
  public virtual bool? Picked { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Picked via Worksheet")]
  public virtual bool? PickedViaWorksheet { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? PackedQty { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [SOShipmentStatus.List]
  [PXDefault]
  [SOShipmentStatusVerifier]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
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
  public virtual int? OrderCntr
  {
    get => this._OrderCntr;
    set => this._OrderCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BilledOrderCntr
  {
    get => this._BilledOrderCntr;
    set => this._BilledOrderCntr = value;
  }

  /// <summary>
  /// Count of related sales orders that have billSeparately field of true value.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BillSeparatelyCntr { get; set; }

  /// <summary>
  /// Represents what part of the orders included in this shipment has the BillSeparately field of true value.
  /// </summary>
  [PXString]
  [PXFormula(typeof (Switch<IBqlString, TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<SOShipment.billSeparatelyCntr, IBqlInt>.IsEqual<Zero>>, SOShipment.billingInOrders.aggregated>, SOShipment.billingInOrders.separate>.When<BqlOperand<SOShipment.billSeparatelyCntr, IBqlInt>.IsEqual<SOShipment.orderCntr>>.Else<SOShipment.billingInOrders.mixed>))]
  [SOShipment.billingInOrders.List]
  [PXUIField(DisplayName = "Billing in Orders")]
  public virtual string BillingInOrders { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? UnbilledOrderCntr
  {
    get => this._UnbilledOrderCntr;
    set => this._UnbilledOrderCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ReleasedOrderCntr
  {
    get => this._ReleasedOrderCntr;
    set => this._ReleasedOrderCntr = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Quantity")]
  public virtual Decimal? ControlQty
  {
    get => this._ControlQty;
    set => this._ControlQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ShipmentQty
  {
    get => this._ShipmentQty;
    set => this._ShipmentQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Weight", Enabled = false)]
  public virtual Decimal? ShipmentWeight
  {
    get => this._ShipmentWeight;
    set => this._ShipmentWeight = value;
  }

  public virtual Decimal? OrderWeight
  {
    get => this._ShipmentWeight;
    set => this._ShipmentWeight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipped Volume", Enabled = false)]
  public virtual Decimal? ShipmentVolume
  {
    get => this._ShipmentVolume;
    set => this._ShipmentVolume = value;
  }

  public virtual Decimal? OrderVolume
  {
    get => this._ShipmentVolume;
    set => this._ShipmentVolume = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? PackageLineCntr
  {
    get => this._PackageLineCntr;
    set => this._PackageLineCntr = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Package Weight", Enabled = false)]
  public virtual Decimal? PackageWeight
  {
    get => this._PackageWeight;
    set => this._PackageWeight = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPackageValid
  {
    get => this._IsPackageValid;
    set => this._IsPackageValid = value;
  }

  [PXInt]
  [PXDefault(0)]
  public virtual int? RecalcPackagesReason { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Packages", Enabled = false)]
  public virtual int? PackageCount
  {
    get => this._PackageCount;
    set => this._PackageCount = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Manual Packaging")]
  public virtual bool? IsManualPackage { get; set; }

  [PXBool]
  public virtual bool? IsPackageContentDeleted { get; set; }

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
  public virtual DateTime? GotReadyForArchiveAt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXBool]
  public virtual bool? Hidden
  {
    get => this._Hidden;
    set => this._Hidden = value;
  }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXFormula(typeof (Selector<SOShipment.customerID, Selector<PX.Objects.CR.BAccount.workgroupID, EPCompanyTree.description>>))]
  [PXUIField]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [Owner(typeof (SOShipment.workgroupID))]
  [PXFormula(typeof (Selector<SOShipment.customerID, PX.Objects.CR.BAccount.ownerID>))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Free Items Quantity Total")]
  public virtual Decimal? FreeItemQtyTot
  {
    get => this._FreeItemQtyTot;
    set => this._FreeItemQtyTot = value;
  }

  [PXString]
  [PXDBCalced(typeof (Switch<Case<Where<SOShipment.status, IsNull>, SOShipmentStatus.autoGenerated>, SOShipment.status>), typeof (string))]
  [SOShipmentStatus.List]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string StatusIsNull
  {
    get => this._StatusIsNull;
    set => this._StatusIsNull = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the shipment should be invoiced by a separate invoice.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Bill Separately")]
  public virtual bool? BillSeparately { get; set; }

  [DBConditionalModifiedByID(typeof (SOShipment.confirmed), true)]
  [PXSelector(typeof (Search<Users.pKID>), new System.Type[] {typeof (Users.displayName)}, DescriptionField = typeof (Users.displayName), Headers = new string[] {"Confirmed By"})]
  [PXUIField(DisplayName = "Confirmed By", Enabled = false, IsReadOnly = true)]
  public virtual Guid? ConfirmedByID { get; set; }

  [DBConditionalModifiedDateTime(typeof (SOShipment.confirmed), true)]
  [PXUIField(DisplayName = "Confirmed On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? ConfirmedDateTime { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<SOShipment.orderCntr, Equal<Zero>>, Null, Case<Where<Add<SOShipment.unbilledOrderCntr, Add<SOShipment.billedOrderCntr, SOShipment.releasedOrderCntr>>, Greater<Zero>>, True>>, False>))]
  public virtual bool? CreateARDoc { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<SOPickingWorksheet.worksheetNbr>))]
  [PXUIField]
  [PXUIVisible(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.currentWorksheetNbr, IsNotNull>>>>.And<Where<Selector<SOShipment.currentWorksheetNbr, SOPickingWorksheet.worksheetType>, NotEqual<SOPickingWorksheet.worksheetType.single>>>))]
  [PXForeignReference]
  public virtual string CurrentWorksheetNbr { get; set; }

  /// <summary>
  /// The boolean flag indicating that license limit for the number of packages in the shipment is not checked.
  /// </summary>
  /// <remarks>
  /// If <c>true</c>, the Shop For Rates, Get Labels, Get Return Labels and Auto-Packaging functionality are not supported.
  /// </remarks>
  [PXDBBool]
  [PXUIField(DisplayName = "Packaging Processed in External System", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? UnlimitedPackages { get; set; }

  /// <summary>The Shop For Rates button error message</summary>
  [PXString(IsUnicode = true)]
  [PXUIField(Enabled = false)]
  [PXUIVisible(typeof (SOShipment.unlimitedPackages))]
  public virtual string ShopForRatesErrorMessage
  {
    get
    {
      return PXLocalizer.Localize("The shipment was imported from an external system without validation of the number of packages against the license restriction. You cannot shop for rates.", typeof (Messages).FullName);
    }
    set
    {
    }
  }

  [PXDBBool]
  [PXFormula(typeof (Where<Selector<SOShipment.customerID, PX.Objects.CR.BAccount.isBranch>, Equal<True>, And<SOShipment.shipmentType, Equal<INDocType.issue>>>))]
  [PXDefault]
  public virtual bool? IsIntercompany { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreceipt>>>))]
  [PXUIField(DisplayName = "Related PO Receipt Nbr.", Enabled = false, FieldClass = "InterBranch")]
  public virtual string IntercompanyPOReceiptNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude from Intercompany Processing", FieldClass = "InterBranch")]
  public virtual bool? ExcludeFromIntercompanyProc { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Excluded")]
  public virtual bool? Excluded { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the <see cref="P:PX.Objects.SO.SOShipment.ShipVia" /> field is updated from shop for rate dialog or not.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? ShipViaUpdateFromShopForRate { get; set; }

  /// <summary>Branch ID of intercompany sales orders</summary>
  [PXDBInt]
  public virtual int? OrderBranchID { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the freight rates are up to date.
  /// </summary>
  /// <value>
  /// If the value is set to <see langword="false" />, the shipment has been modified and the rates
  /// should be updated.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? FreightCostIsValid { get; set; }

  public static SOShipment FromDropshipPOReceipt(PX.Objects.PO.POReceipt rec)
  {
    return new SOShipment()
    {
      ShipmentNbr = rec.ReceiptNbr,
      ShipmentType = "H",
      Operation = rec.ReceiptType == "RN" ? "R" : "I",
      CustomerID = rec.DropshipCustomerID,
      CustomerLocationID = rec.DropshipCustomerLocationID,
      CustomerOrderNbr = rec.DropshipCustomerOrderNbr,
      Confirmed = new bool?(true),
      Hold = new bool?(false),
      CreatedByID = rec.CreatedByID,
      CreatedByScreenID = rec.CreatedByScreenID,
      CreatedDateTime = rec.CreatedDateTime,
      LastModifiedByID = rec.LastModifiedByID,
      LastModifiedByScreenID = rec.LastModifiedByScreenID,
      LastModifiedDateTime = rec.LastModifiedDateTime,
      ShipDate = rec.ReceiptDate,
      ShipmentQty = rec.OrderQty,
      ShipmentVolume = rec.ReceiptVolume,
      ShipmentWeight = rec.ReceiptWeight,
      Status = "R",
      WorkgroupID = rec.WorkgroupID,
      OwnerID = rec.OwnerID,
      SiteID = new int?(),
      ShipVia = rec.DropshipShipVia,
      FOBPoint = (string) null,
      NoteID = rec.NoteID
    };
  }

  public class PK : PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>
  {
    public static SOShipment Find(PXGraph graph, string shipmentNbr, PKFindOptions options = 0)
    {
      return (SOShipment) PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.FindBy(graph, (object) shipmentNbr, options);
    }
  }

  public class UK : PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentType, SOShipment.shipmentNbr>
  {
    public static SOShipment Find(
      PXGraph graph,
      string shipmentType,
      string shipmentNbr,
      PKFindOptions options = 0)
    {
      return (SOShipment) PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentType, SOShipment.shipmentNbr>.FindBy(graph, (object) shipmentType, (object) shipmentNbr, options);
    }
  }

  public static class FK
  {
    public class ShipAddress : 
      PrimaryKeyOf<SOShipmentAddress>.By<SOShipmentAddress.addressID>.ForeignKeyOf<SOShipment>.By<SOShipment.shipAddressID>
    {
    }

    public class ShipContact : 
      PrimaryKeyOf<SOShipmentContact>.By<SOShipmentContact.contactID>.ForeignKeyOf<SOShipment>.By<SOShipment.shipContactID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOShipment>.By<SOShipment.siteID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOShipment>.By<SOShipment.destinationSiteID>
    {
    }

    public class DestinationSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOShipment>.By<SOShipment.destinationSiteID>
    {
    }

    public class ShipTerms : 
      PrimaryKeyOf<PX.Objects.CS.ShipTerms>.By<PX.Objects.CS.ShipTerms.shipTermsID>.ForeignKeyOf<SOShipment>.By<SOShipment.shipTermsID>
    {
    }

    public class ShipZone : 
      PrimaryKeyOf<PX.Objects.CS.ShippingZone>.By<PX.Objects.CS.ShippingZone.zoneID>.ForeignKeyOf<SOShipment>.By<SOShipment.shipZoneID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<SOShipment>.By<SOShipment.taxCategoryID>
    {
    }

    public class Carrier : 
      PrimaryKeyOf<PX.Objects.CS.Carrier>.By<PX.Objects.CS.Carrier.carrierID>.ForeignKeyOf<SOShipment>.By<SOShipment.shipVia>
    {
    }

    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOShipment>.By<SOShipment.currentWorksheetNbr>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOShipment>.By<SOShipment.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<SOShipment>.By<SOShipment.customerID, SOShipment.customerLocationID>
    {
    }

    public class FOBPoint : 
      PrimaryKeyOf<PX.Objects.CS.FOBPoint>.By<PX.Objects.CS.FOBPoint.fOBPointID>.ForeignKeyOf<SOShipment>.By<SOShipment.fOBPoint>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<SOShipment>.By<SOShipment.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOShipment>.By<SOShipment.curyInfoID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<SOShipment>.By<SOShipment.workgroupID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<SOShipment>.By<SOShipment.ownerID>
    {
    }

    public class ConfirmedByUser : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<SOShipment>.By<SOShipment.confirmedByID>
    {
    }
  }

  public class Events : PXEntityEventBase<SOShipment>.Container<SOShipment.Events>
  {
    public PXEntityEvent<SOShipment> ShipmentConfirmed;
    public PXEntityEvent<SOShipment> ShipmentCorrected;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.selected>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.shipmentNbr>
  {
    public const string DisplayName = "Shipment Nbr.";
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOShipment.shipDate>
  {
    public const string DisplayName = "Shipment Date";
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.operation>
  {
  }

  public abstract class shipmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.shipmentType>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.customerID>
  {
    public class PreventEditBAccountCOrgBAccountID<TGraph> : 
      PreventEditBAccountRestrictToBase<PX.Objects.CR.BAccount.cOrgBAccountID, TGraph, SOShipment, SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      SOShipment.shipmentType, 
      #nullable disable
      NotEqual<INDocType.transfer>>>>>.And<BqlOperand<
      #nullable enable
      SOShipment.customerID, IBqlInt>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.CR.BAccount.bAccountID, IBqlInt>.FromCurrent>>>>
      where TGraph : 
      #nullable disable
      PXGraph
    {
      protected override string GetErrorMessage(
        PX.Objects.CR.BAccount baccount,
        SOShipment document,
        string documentBaseCurrency)
      {
        return PXMessages.LocalizeFormatNoPrefix("A branch with the base currency other than {0} cannot be associated with the {1} customer because {1} is selected in the {2} shipment.", new object[3]
        {
          (object) documentBaseCurrency,
          (object) baccount.AcctCD,
          (object) document.ShipmentNbr
        });
      }
    }

    public class PreventEditBAccountCOrgBAccountIDOnVendorMaint : 
      SOShipment.customerID.PreventEditBAccountCOrgBAccountID<VendorMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }

    public class PreventEditBAccountCOrgBAccountIDOnCustomerMaint : 
      SOShipment.customerID.PreventEditBAccountCOrgBAccountID<CustomerMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipment.customerLocationID>
  {
  }

  public abstract class customerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.customerOrderNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.siteID>
  {
  }

  public abstract class siteBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.siteBranchID>
  {
  }

  public abstract class destinationSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.destinationSiteID>
  {
  }

  public abstract class shipmentDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.shipmentDesc>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.shipContactID>
  {
  }

  public abstract class fOBPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.fOBPoint>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.shipVia>
  {
  }

  public abstract class willCall : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.willCall>
  {
  }

  public abstract class useCustomerAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.useCustomerAccount>
  {
  }

  public abstract class resedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.resedential>
  {
  }

  public abstract class saturdayDelivery : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.saturdayDelivery>
  {
  }

  public abstract class insurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.insurance>
  {
  }

  public abstract class groundCollect : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.groundCollect>
  {
  }

  public abstract class labelsPrinted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.labelsPrinted>
  {
  }

  public abstract class commercialInvoicesPrinted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.commercialInvoicesPrinted>
  {
  }

  public abstract class pickListPrinted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.pickListPrinted>
  {
  }

  public abstract class confirmationPrinted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.confirmationPrinted>
  {
  }

  public abstract class shippedViaCarrier : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.shippedViaCarrier>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.shipTermsID>
  {
    public class PreventEditIfShipmentExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<PX.Objects.CS.ShipTerms.freightAmountSource>>.On<ShipTermsMaint>.IfExists<Select<SOShipment, Where<SOShipment.shipTermsID, Equal<Current<PX.Objects.CS.ShipTerms.shipTermsID>>>>>
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object sh,
        string fld,
        string tbl,
        string foreignTbl)
      {
        return PXMessages.LocalizeFormat("Cannot change shipping terms because current shipping terms are used in the shipment {0}.", new object[1]
        {
          (object) ((SOShipment) sh).ShipmentNbr
        });
      }
    }
  }

  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.shipZoneID>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.lineTotal>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOShipment.curyInfoID>
  {
  }

  public abstract class curyFreightCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.curyFreightCost>
  {
  }

  public abstract class freightCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.freightCost>
  {
  }

  public abstract class overrideFreightAmount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.overrideFreightAmount>
  {
  }

  public abstract class freightAmountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.freightAmountSource>
  {
  }

  public abstract class curyFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.curyFreightAmt>
  {
  }

  public abstract class freightAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.freightAmt>
  {
  }

  public abstract class curyPremiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.curyPremiumFreightAmt>
  {
  }

  public abstract class premiumFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.premiumFreightAmt>
  {
  }

  public abstract class curyTotalFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.curyTotalFreightAmt>
  {
  }

  public abstract class totalFreightAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.totalFreightAmt>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.taxCategoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipment.noteID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.hold>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.confirmed>
  {
  }

  public abstract class confirmedToVerify : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.confirmedToVerify>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.released>
  {
  }

  public abstract class picked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.picked>
  {
  }

  public abstract class pickedViaWorksheet : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.pickedViaWorksheet>
  {
  }

  public abstract class pickedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.pickedQty>
  {
  }

  public abstract class packedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.packedQty>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.status>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.lineCntr>
  {
  }

  public abstract class orderCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.orderCntr>
  {
  }

  public abstract class billedOrderCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.billedOrderCntr>
  {
  }

  public abstract class billSeparatelyCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipment.billSeparatelyCntr>
  {
  }

  public abstract class billingInOrders : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.billingInOrders>
  {
    public const string Mixed = "M";
    public const string Aggregated = "A";
    public const string Separate = "S";

    public class mixed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOShipment.billingInOrders.mixed>
    {
      public mixed()
        : base("M")
      {
      }
    }

    public class aggregated : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SOShipment.billingInOrders.aggregated>
    {
      public aggregated()
        : base("A")
      {
      }
    }

    public class separate : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOShipment.billingInOrders.separate>
    {
      public separate()
        : base("S")
      {
      }
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("M", "Mixed Billing"),
          PXStringListAttribute.Pair("A", "Aggregated Billing"),
          PXStringListAttribute.Pair("S", "Separate Billing Only")
        })
      {
      }
    }

    [PXLocalizable]
    public static class DisplayName
    {
      public const string Mixed = "Mixed Billing";
      public const string Aggregated = "Aggregated Billing";
      public const string Separate = "Separate Billing Only";
    }
  }

  public abstract class unbilledOrderCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.unbilledOrderCntr>
  {
  }

  public abstract class releasedOrderCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.releasedOrderCntr>
  {
  }

  public abstract class controlQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.controlQty>
  {
  }

  public abstract class shipmentQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.shipmentQty>
  {
  }

  public abstract class shipmentWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.shipmentWeight>
  {
  }

  public abstract class shipmentVolume : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.shipmentVolume>
  {
  }

  public abstract class packageLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.packageLineCntr>
  {
  }

  public abstract class packageWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipment.packageWeight>
  {
  }

  public abstract class isPackageValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.isPackageValid>
  {
  }

  public abstract class recalcPackagesReason : IBqlField, IBqlOperand
  {
    public const int None = 0;
    public const int ShipVia = 1;
    public const int ShipLine = 2;
  }

  public abstract class packageCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.packageCount>
  {
  }

  public abstract class isManualPackage : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.isManualPackage>
  {
  }

  public abstract class isPackageContentDeleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.isPackageContentDeleted>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipment.lastModifiedDateTime>
  {
  }

  public abstract class gotReadyForArchiveAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipment.gotReadyForArchiveAt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOShipment.Tstamp>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.hidden>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipment.ownerID>
  {
  }

  public abstract class freeItemQtyTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipment.freeItemQtyTot>
  {
  }

  public abstract class siteID_INSite_descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.siteID_INSite_descr>
  {
  }

  public abstract class shipVia_Carrier_description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.shipVia_Carrier_description>
  {
  }

  public abstract class statusIsNull : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipment.statusIsNull>
  {
  }

  public abstract class billSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.billSeparately>
  {
  }

  public abstract class confirmedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipment.confirmedByID>
  {
  }

  public abstract class confirmedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipment.confirmedDateTime>
  {
  }

  public abstract class createARDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.createARDoc>
  {
  }

  public abstract class currentWorksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.currentWorksheetNbr>
  {
  }

  public abstract class unlimitedPackages : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.unlimitedPackages>
  {
  }

  public abstract class shopForRatesErrorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.shopForRatesErrorMessage>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.isIntercompany>
  {
  }

  public abstract class intercompanyPOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipment.intercompanyPOReceiptNbr>
  {
  }

  public abstract class excludeFromIntercompanyProc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.excludeFromIntercompanyProc>
  {
  }

  public abstract class excluded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.excluded>
  {
  }

  public abstract class shipViaUpdateFromShopForRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.shipViaUpdateFromShopForRate>
  {
  }

  public abstract class orderBranchID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipment.orderBranchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOShipment.FreightCostIsValid" />
  public abstract class freightCostIsValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipment.freightCostIsValid>
  {
  }
}
