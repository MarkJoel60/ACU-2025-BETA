// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisition
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXPrimaryGraph(typeof (RQRequisitionEntry))]
[PXCacheName("Requisition")]
[PXGroupMask(typeof (LeftJoinSingleTable<PX.Objects.AR.Customer, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, Equal<RQRequisition.customerID>>>>>.And<Match<PX.Objects.AR.Customer, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, Equal<RQRequisition.vendorID>>>>>.And<Match<PX.Objects.AP.Vendor, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>>), WhereRestriction = typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNotNull>>>>.Or<BqlOperand<RQRequisition.customerID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.bAccountID, IsNotNull>>>>.Or<BqlOperand<RQRequisition.vendorID, IBqlInt>.IsNull>>))]
public class RQRequisition : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _ReqNbr;
  protected DateTime? _OrderDate;
  protected int? _Priority;
  protected string _Status;
  protected string _Description;
  protected bool? _Hold;
  protected bool? _Approved;
  protected bool? _Rejected = new bool?(false);
  protected bool? _Cancelled;
  protected bool? _Splittable;
  protected bool? _Released;
  protected bool? _BiddingComplete;
  protected bool? _Quoted;
  protected Guid? _NoteID;
  protected int? _EmployeeID;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected int? _ApprovalWorkgroupID;
  protected int? _ApprovalOwnerID;
  protected string _ShipDestType;
  protected int? _SiteID;
  protected int? _ShipToBAccountID;
  protected int? _ShipToLocationID;
  protected int? _ShipAddressID;
  protected int? _ShipContactID;
  protected string _FOBPoint;
  protected string _ShipVia;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected string _VendorRefNbr;
  protected bool? _VendorRequestSent = new bool?(false);
  protected bool? _SkipValidateWithVendorCuryOrRate;
  protected string _TermsID;
  protected int? _RemitAddressID;
  protected int? _RemitContactID;
  protected Decimal? _OpenOrderQty;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _EstExtCostTotal;
  protected Decimal? _CuryEstExtCostTotal;
  protected string _POType;
  protected int? _LineCntr;
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

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (RQSetup.requisitionNumberingID), typeof (RQRequisition.orderDate))]
  [PXSelector(typeof (Search2<RQRequisition.reqNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<RQRequisition.customerID>>, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<RQRequisition.vendorID>>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>>), new System.Type[] {typeof (RQRequisition.reqNbr), typeof (RQRequisition.status), typeof (RQRequisition.employeeID), typeof (RQRequisition.vendorID)}, Filterable = true)]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXDefault(1)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Low", "Normal", "High"})]
  public virtual int? Priority
  {
    get => this._Priority;
    set => this._Priority = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [RQRequisitionStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
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

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? Splittable
  {
    get => this._Splittable;
    set => this._Splittable = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? BiddingComplete
  {
    get => this._BiddingComplete;
    set => this._BiddingComplete = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Quoted
  {
    get => this._Quoted;
    set => this._Quoted = value;
  }

  [PXSearchable(512 /*0x0200*/, "Requisition: {0} - {2}", new System.Type[] {typeof (RQRequisition.reqNbr), typeof (RQRequisition.employeeID), typeof (PX.Objects.EP.EPEmployee.acctName)}, new System.Type[] {typeof (RQRequisition.vendorRefNbr), typeof (RQRequisition.description)}, NumberFields = new System.Type[] {typeof (RQRequisition.reqNbr)}, Line1Format = "{0:d}{1}", Line1Fields = new System.Type[] {typeof (RQRequisition.orderDate), typeof (RQRequisition.status)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (RQRequisition.description)})]
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.bAccountID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXSubordinateSelector]
  [PXUIField]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [CustomerActive(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
  [CustomerOrOrganizationInNoUpdateDocRestrictor]
  [PXForeignReference(typeof (Field<RQRequisition.customerID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.AR.Customer.defLocationID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<RQRequisition.customerID>>>>))]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXForeignReference(typeof (Field<RQRequisition.customerLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXSubordinateGroupSelector]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [PXDefault(typeof (PX.Objects.AP.Vendor.ownerID))]
  [Owner(typeof (RQRequisition.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXInt]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Approval Workgroup ID", Enabled = false)]
  public virtual int? ApprovalWorkgroupID
  {
    get => this._ApprovalWorkgroupID;
    set => this._ApprovalWorkgroupID = value;
  }

  [Owner(IsDBField = false, DisplayName = "Approver", Enabled = false)]
  public virtual int? ApprovalOwnerID
  {
    get => this._ApprovalOwnerID;
    set => this._ApprovalOwnerID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [POShippingDestination.List]
  [PXDefault("L")]
  [PXUIField(DisplayName = "Shipping Destination Type")]
  public virtual string ShipDestType
  {
    get => this._ShipDestType;
    set => this._ShipDestType = value;
  }

  [Site(DescriptionField = typeof (INSite.descr))]
  [PXDefault(null, typeof (Coalesce<Search<LocationBranchSettings.vSiteID, Where<Current<RQRequisition.shipDestType>, Equal<POShippingDestination.site>, And<LocationBranchSettings.bAccountID, Equal<Current<RQRequisition.vendorID>>, And<LocationBranchSettings.locationID, Equal<Current<RQRequisition.vendorLocationID>>, And<LocationBranchSettings.branchID, Equal<Current<RQRequisition.branchID>>>>>>>, Search<PX.Objects.CR.Location.vSiteID, Where<Current<RQRequisition.shipDestType>, Equal<POShippingDestination.site>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>>>>>, Search<INSite.siteID, Where<Current<RQRequisition.shipDestType>, Equal<POShippingDestination.site>, And<INSite.active, Equal<True>, And<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>>))]
  [PXForeignReference(typeof (RQRequisition.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<INSite.branchID, Current<RQRequisition.branchID>>>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXString(150, IsUnicode = true)]
  public virtual string SiteIdErrorMessage { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search2<BAccount2.bAccountID, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccount2.bAccountID>, And<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<BAccount2.bAccountID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccount2.bAccountID>, And<Match<PX.Objects.GL.Branch, Current<AccessInfo.userName>>>>>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<Optional<RQRequisition.shipDestType>, Equal<POShippingDestination.vendor>, Or<Where<PX.Objects.GL.Branch.bAccountID, IsNotNull, And<Optional<RQRequisition.shipDestType>, Equal<POShippingDestination.company>, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Optional<RQRequisition.shipDestType>, Equal<POShippingDestination.customer>>>>>>>>>>), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.type), typeof (PX.Objects.CR.BAccount.acctReferenceNbr), typeof (PX.Objects.CR.BAccount.parentBAccountID)}, SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField(DisplayName = "Ship To")]
  [PXDefault(null, typeof (Search<PX.Objects.GL.Branch.bAccountID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>, And<Optional<RQRequisition.shipDestType>, Equal<POShippingDestination.company>>>>))]
  [PXForeignReference(typeof (Field<RQRequisition.shipToBAccountID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? ShipToBAccountID
  {
    get => this._ShipToBAccountID;
    set => this._ShipToBAccountID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.shipToBAccountID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(null, typeof (Search<BAccount2.defLocationID, Where<BAccount2.bAccountID, Equal<Current<RQRequisition.shipToBAccountID>>>>))]
  [PXUIField(DisplayName = "Shipping Location")]
  [PXForeignReference(typeof (Field<RQRequisition.shipToLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>))]
  public virtual int? ShipToLocationID
  {
    get => this._ShipToLocationID;
    set => this._ShipToLocationID = value;
  }

  [PXDBInt]
  [POShipAddress(typeof (Select2<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Standalone.Location.defAddressID>, And<Current<RQRequisition.shipDestType>, NotEqual<POShippingDestination.site>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<RQRequisition.shipToBAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<RQRequisition.shipToLocationID>>>>>>>, LeftJoin<POShipAddress, On<POShipAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<POShipAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<POShipAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<POShipAddress.isDefaultAddress, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Standalone.Location.locationCD, IsNotNull>>))]
  [PXUIField]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  [PXDBInt]
  [POShipContact(typeof (Select2<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Standalone.Location.defContactID>, And<Current<RQRequisition.shipDestType>, NotEqual<POShippingDestination.site>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<RQRequisition.shipToBAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<RQRequisition.shipToLocationID>>>>>>>, LeftJoin<POShipContact, On<POShipContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<POShipContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<POShipContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<POShipContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Standalone.Location.locationCD, IsNotNull>>))]
  [PXUIField]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (Search<PX.Objects.CS.FOBPoint.fOBPointID>), DescriptionField = typeof (PX.Objects.CS.FOBPoint.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vFOBPointID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>>>>))]
  public virtual string FOBPoint
  {
    get => this._FOBPoint;
    set => this._FOBPoint = value;
  }

  [PXUIField(DisplayName = "Ship Via")]
  [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vCarrierID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>>>>))]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  [VendorNonEmployeeActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQRequisition.vendorID>>>>))]
  [PXForeignReference(typeof (Field<RQRequisition.vendorLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Ref.")]
  public virtual string VendorRefNbr
  {
    get => this._VendorRefNbr;
    set => this._VendorRefNbr = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Vendor Requests Sent")]
  public virtual bool? VendorRequestSent
  {
    get => this._VendorRequestSent;
    set => this._VendorRequestSent = value;
  }

  [PXBool]
  public virtual bool? SkipValidateWithVendorCuryOrRate
  {
    get => this._SkipValidateWithVendorCuryOrRate;
    set => this._SkipValidateWithVendorCuryOrRate = value;
  }

  [PXDBString(10, IsUnicode = true, IsFixed = true)]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.termsID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQRequisition.vendorID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBInt]
  [PORemitAddress(typeof (Select2<BAccount2, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<BAccount2.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PX.Objects.PO.PORemitAddress, On<PX.Objects.PO.PORemitAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PX.Objects.PO.PORemitAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PX.Objects.PO.PORemitAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PX.Objects.PO.PORemitAddress.isDefaultAddress, Equal<boolTrue>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>>>>), Required = false)]
  [PXUIField]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  [PXDBInt]
  [PORemitContact(typeof (Select2<PX.Objects.CR.Location, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<PX.Objects.PO.PORemitContact, On<PX.Objects.PO.PORemitContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.PO.PORemitContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PX.Objects.PO.PORemitContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<PX.Objects.PO.PORemitContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequisition.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequisition.vendorLocationID>>>>>), Required = false)]
  public virtual int? RemitContactID
  {
    get => this._RemitContactID;
    set => this._RemitContactID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Open Quantity")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenOrderQty
  {
    get => this._OpenOrderQty;
    set => this._OpenOrderQty = value;
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
  [CurrencyInfo(ModuleCode = "PO")]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? EstExtCostTotal
  {
    get => this._EstExtCostTotal;
    set => this._EstExtCostTotal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (RQRequisition.curyInfoID), typeof (RQRequisition.estExtCostTotal))]
  [PXUIField]
  public virtual Decimal? CuryEstExtCostTotal
  {
    get => this._CuryEstExtCostTotal;
    set => this._CuryEstExtCostTotal = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("RO")]
  [POOrderType.RequisitionList]
  [PXUIField(DisplayName = "PO Type", Enabled = true)]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
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

  int? IAssign.WorkgroupID
  {
    get => this.ApprovalWorkgroupID;
    set => this.ApprovalWorkgroupID = value;
  }

  int? IAssign.OwnerID
  {
    get => this.ApprovalOwnerID;
    set => this.ApprovalOwnerID = value;
  }

  public class PK : PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>
  {
    public static RQRequisition Find(PXGraph graph, string reqNbr, PKFindOptions options = 0)
    {
      return (RQRequisition) PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.FindBy(graph, (object) reqNbr, options);
    }
  }

  public static class FK
  {
    public class FOBPoint : 
      PrimaryKeyOf<PX.Objects.CS.FOBPoint>.By<PX.Objects.CS.FOBPoint.fOBPointID>.ForeignKeyOf<RQRequisition>.By<RQRequisition.fOBPoint>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<RQRequisition>.By<RQRequisition.siteID>
    {
    }
  }

  public class Events : PXEntityEventBase<RQRequisition>.Container<RQRequisition.Events>
  {
    public PXEntityEvent<RQRequisition> BiddingCompleted;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.branchID>
  {
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.reqNbr>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RQRequisition.orderDate>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.priority>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.status>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.description>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.rejected>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.cancelled>
  {
  }

  public abstract class splittable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.splittable>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.released>
  {
  }

  public abstract class biddingComplete : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequisition.biddingComplete>
  {
  }

  public abstract class quoted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisition.quoted>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequisition.noteID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.employeeID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisition.customerLocationID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.ownerID>
  {
  }

  public abstract class approvalWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisition.approvalWorkgroupID>
  {
  }

  public abstract class approvalOwnerID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQRequisition.approvalOwnerID>
  {
  }

  public abstract class shipDestType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.shipDestType>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.siteID>
  {
  }

  public abstract class siteIdErrorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisition.siteIdErrorMessage>
  {
  }

  public abstract class shipToBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisition.shipToBAccountID>
  {
  }

  public abstract class shipToLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisition.shipToLocationID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.shipContactID>
  {
  }

  public abstract class fOBPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.fOBPoint>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.shipVia>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisition.vendorLocationID>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.vendorRefNbr>
  {
  }

  public abstract class vendorRequestSent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequisition.vendorRequestSent>
  {
  }

  public abstract class skipValidateWithVendorCuryOrRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequisition.skipValidateWithVendorCuryOrRate>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.termsID>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.remitContactID>
  {
  }

  public abstract class openOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisition.openOrderQty>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQRequisition.curyInfoID>
  {
  }

  public abstract class estExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisition.estExtCostTotal>
  {
  }

  public abstract class curyEstExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisition.curyEstExtCostTotal>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisition.pOType>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisition.lineCntr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQRequisition.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequisition.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisition.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisition.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQRequisition.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisition.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisition.lastModifiedDateTime>
  {
  }
}
