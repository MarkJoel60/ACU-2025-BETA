// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Location
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// The location of a business account, a customer or a vendor with specific settings for entities such as sales orders, purchase orders, and invoices.
/// </summary>
/// <remarks>
/// An user can specify more than one location for a business account, a customer, or a vendor, and each location can have specific settings
/// such as the following:
/// <list type="bullet">
/// <item><description>Accounts payable and expense accounts and the corresponding subaccounts</description></item>
/// <item><description>Payment settings and remittance information</description></item>
/// <item><description>Purchasing and shipping-related information</description></item>
/// <item><description>Tax-related information</description></item>
/// </list>
/// The records of this type are created and edited on the <i>Account Locations (CR303010)</i>, <i>Customer Locations (AR303020)</i>, and <i>Vendor Locations (AP303010)</i> forms,
/// which correspond to the <see cref="T:PX.Objects.CR.AccountLocationMaint" />, <see cref="T:PX.Objects.AR.CustomerLocationMaint" />, and <see cref="T:PX.Objects.AP.VendorLocationMaint" /> graphs respectively.
/// </remarks>
[PXPrimaryGraph(new System.Type[] {typeof (VendorLocationMaint), typeof (CustomerLocationMaint), typeof (BranchMaint), typeof (EmployeeMaint), typeof (AccountLocationMaint)}, new System.Type[] {typeof (Select<Location, Where<Location.bAccountID, Equal<Current<Location.bAccountID>>, And<Location.locationID, Equal<Current<Location.locationID>>, And<Where<Current<Location.locType>, Equal<LocTypeList.vendorLoc>, Or<Current<Location.locType>, Equal<LocTypeList.combinedLoc>>>>>>>), typeof (Select<Location, Where<Location.bAccountID, Equal<Current<Location.bAccountID>>, And<Location.locationID, Equal<Current<Location.locationID>>, And<Where<Current<Location.locType>, Equal<LocTypeList.customerLoc>, Or<Current<Location.locType>, Equal<LocTypeList.combinedLoc>>>>>>>), typeof (Select2<PX.Objects.GL.Branch, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<Location, On<Location.bAccountID, Equal<BAccount.bAccountID>, And<Location.locationID, Equal<BAccount.defLocationID>>>>>, Where<Location.bAccountID, Equal<Current<Location.bAccountID>>, And<Location.locationID, Equal<Current<Location.locationID>>, And<Current<Location.locType>, Equal<LocTypeList.companyLoc>>>>>), typeof (Select2<EPEmployee, InnerJoin<Location, On<Location.bAccountID, Equal<EPEmployee.bAccountID>, And<Location.locationID, Equal<EPEmployee.defLocationID>>>>, Where<Location.bAccountID, Equal<Current<Location.bAccountID>>, And<Location.locationID, Equal<Current<Location.locationID>>, And<Current<Location.locType>, Equal<LocTypeList.employeeLoc>>>>>), typeof (Select<Location, Where<Location.bAccountID, Equal<Current<Location.bAccountID>>, And<Location.locationID, Equal<Current<Location.locationID>>, And<Where<Current<Location.locType>, Equal<LocTypeList.companyLoc>, Or<Current<Location.locType>, Equal<LocTypeList.combinedLoc>>>>>>>)})]
[PXCacheName("Location")]
[PXProjection(typeof (Select2<Location, LeftJoin<LocationAPAccountSub, On<LocationAPAccountSub.bAccountID, Equal<Location.bAccountID>, And<LocationAPAccountSub.locationID, Equal<Location.vAPAccountLocationID>>>, LeftJoin<LocationARAccountSub, On<LocationARAccountSub.bAccountID, Equal<Location.bAccountID>, And<LocationARAccountSub.locationID, Equal<Location.cARAccountLocationID>>>, LeftJoin<LocationAPPaymentInfo, On<LocationAPPaymentInfo.bAccountID, Equal<Location.bAccountID>, And<LocationAPPaymentInfo.locationID, Equal<Location.vPaymentInfoLocationID>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<Location.bAccountID>>>>>>>), Persistent = true)]
[PXGroupMask(typeof (InnerJoin<BAccount, On<BAccount.bAccountID, Equal<Location.bAccountID>, And<Match<BAccount, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class Location : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPaymentTypeDetailMaster,
  ILocation
{
  protected int? _BAccountID;
  protected int? _LocationID;
  protected 
  #nullable disable
  string _LocationCD;
  protected string _LocType;
  protected string _Descr;
  protected string _TaxRegistrationID;
  protected int? _DefAddressID;
  protected int? _DefContactID;
  protected Guid? _NoteID;
  protected bool? _IsActive;
  protected string _CTaxZoneID;
  protected string _CAvalaraExemptionNumber;
  protected string _CAvalaraCustomerUsageType;
  protected string _CCarrierID;
  protected string _CShipTermsID;
  protected string _CShipZoneID;
  protected string _CFOBPointID;
  protected bool? _CResedential;
  protected bool? _CSaturdayDelivery;
  protected bool? _CGroundCollect;
  protected bool? _CInsurance;
  protected short? _CLeadTime;
  protected int? _CBranchID;
  protected int? _CSalesAcctID;
  protected int? _CSalesSubID;
  protected string _CPriceClassID;
  protected int? _CSiteID;
  protected int? _CDiscountAcctID;
  protected int? _CDiscountSubID;
  protected int? _CFreightAcctID;
  protected int? _CFreightSubID;
  protected string _CShipComplete;
  protected short? _COrderPriority;
  protected string _CCalendarID;
  protected int? _CDefProjectID;
  protected int? _CARAccountLocationID;
  protected int? _CARAccountID;
  protected int? _CARSubID;
  protected bool? _IsARAccountSameAsMain;
  protected string _VTaxZoneID;
  protected string _VCarrierID;
  protected string _VShipTermsID;
  protected string _VFOBPointID;
  protected short? _VLeadTime;
  protected int? _VBranchID;
  protected int? _VExpenseAcctID;
  protected int? _VExpenseSubID;
  protected int? _VFreightAcctID;
  protected int? _VFreightSubID;
  protected Decimal? _VRcptQtyMin;
  protected Decimal? _VRcptQtyMax;
  protected Decimal? _VRcptQtyThreshold;
  protected string _VRcptQtyAction;
  protected int? _VSiteID;
  protected bool? _VPrintOrder;
  protected bool? _VEmailOrder;
  protected int? _VDefProjectID;
  protected int? _VAPAccountLocationID;
  protected int? _VAPAccountID;
  protected int? _VAPSubID;
  protected int? _VPaymentInfoLocationID;
  protected bool? _IsRemitAddressSameAsMain;
  protected int? _VRemitAddressID;
  protected bool? _IsRemitContactSameAsMain;
  protected int? _VRemitContactID;
  protected string _VPaymentMethodID;
  protected int? _VCashAccountID;
  protected short? _VPaymentLeadTime;
  protected int? _VPaymentByType;
  protected bool? _VSeparateCheck;
  protected bool? _IsAPAccountSameAsMain;
  protected bool? _IsAPPaymentInfoSameAsMain;
  protected int? _LocationAPAccountSubBAccountID;
  protected int? _APAccountID;
  protected int? _APSubID;
  protected int? _LocationARAccountSubBAccountID;
  protected int? _ARAccountID;
  protected int? _ARSubID;
  protected int? _LocationAPPaymentInfoBAccountID;
  protected int? _RemitAddressID;
  protected int? _RemitContactID;
  protected string _PaymentMethodID;
  protected int? _CashAccountID;
  protected short? _PaymentLeadTime;
  protected bool? _SeparateCheck;
  protected int? _PaymentByType;
  protected int? _BAccountBAccountID;
  protected int? _VDefAddressID;
  protected int? _VDefContactID;
  protected int? _CMPSalesSubID;
  protected int? _CMPExpenseSubID;
  protected int? _CMPFreightSubID;
  protected int? _CMPDiscountSubID;
  protected int? _CMPGainLossSubID;
  protected int? _CMPSiteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CR.BAccount" /> record that is specified in the document to which the location belongs.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (BAccount.bAccountID))]
  [PXUIField]
  [PXParent(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<Location.bAccountID>>>>))]
  [PXSelector(typeof (Search<BAccount.bAccountID>), DirtyRead = true)]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The unique identifier assigned to the location.
  /// This field is the primary key field.
  /// </summary>
  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>
  /// The human-readable identifier of the location that is specified by the user when they create a location.
  /// This field is a natural key as opposed to the <see cref="P:PX.Objects.CR.Location.LocationID" /> surrogate key.
  /// </summary>
  [LocationRaw(typeof (Where<Location.bAccountID, Equal<Current<Location.bAccountID>>>), null)]
  [PXDefault]
  public virtual string LocationCD
  {
    get => this._LocationCD;
    set => this._LocationCD = value;
  }

  /// <summary>The type of the location.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.LocTypeList.ListAttribute" /> class.
  /// The default value depends on the form where the location was created.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [LocTypeList.List]
  [PXUIField]
  [PXDefault]
  public virtual string LocType
  {
    get => this._LocType;
    set => this._LocType = value;
  }

  /// <summary>The name of the location.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>
  /// The registration ID of the company in the state tax authority.
  /// </summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Registration ID")]
  [PXMassMergableField]
  [PXPersonalDataField]
  public virtual string TaxRegistrationID
  {
    get => this._TaxRegistrationID;
    set => this._TaxRegistrationID = value;
  }

  /// <summary>The identifier of the address for this location.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (Address.addressID))]
  [PXUIField]
  [PXSelector(typeof (Search<Address.addressID>), DirtyRead = true)]
  public virtual int? DefAddressID
  {
    get => this._DefAddressID;
    set => this._DefAddressID = value;
  }

  /// <summary>
  /// The identifier of the bisuness account contact of the location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXUIField(DisplayName = "Default Contact")]
  [PXSelector(typeof (Search<Contact.contactID>), ValidateValue = false, DirtyRead = true)]
  public virtual int? DefContactID
  {
    get => this._DefContactID;
    set => this._DefContactID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>This field indicates whether the location is active.</summary>
  /// <value>
  /// The default value is <see langword="true" />.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active", Enabled = false)]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  /// <summary>The current status of the location.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CR.LocationStatus.ListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.CR.LocationStatus.Active" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Status")]
  [LocationStatus.List]
  public virtual string Status { get; set; }

  /// <summary>
  /// This field indicates whether the location is default for the corresponding business account.
  /// </summary>
  /// <value>
  /// The field value is <see langword="true" /> when this location is the default location of the corresponding business account, and <see langword="false" /> otherwise.
  /// </value>
  [PXDBCalced(typeof (IIf<BqlOperand<BAccountR.defLocationID, IBqlInt>.IsEqual<Location.locationID>, True, False>), typeof (bool))]
  [PXBool]
  [PXUIField(DisplayName = "Default")]
  public virtual bool? IsDefault { get; set; }

  /// <summary>The customer's tax zone.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
  public virtual string CTaxZoneID
  {
    get => this._CTaxZoneID;
    set => this._CTaxZoneID = value;
  }

  /// <summary>The tax calculation mode of the customer.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.TX.TaxCalculationMode.ListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.TX.TaxCalculationMode.TaxSetting" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<CustomerClass.taxCalcMode, Where<CustomerClass.customerClassID, Equal<Current<CustomerClass.customerClassID>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string CTaxCalcMode { get; set; }

  /// <summary>
  /// The Avalara Exemption number of the customer location.
  /// </summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string CAvalaraExemptionNumber
  {
    get => this._CAvalaraExemptionNumber;
    set => this._CAvalaraExemptionNumber = value;
  }

  /// <summary>
  /// The customer's entity type for reporting purposes. This field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.TX.TXAvalaraCustomerUsageType.ListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.TX.TXAvalaraCustomerUsageType.Default" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [PXDefault("0")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string CAvalaraCustomerUsageType
  {
    get => this._CAvalaraCustomerUsageType;
    set => this._CAvalaraCustomerUsageType = value;
  }

  /// <summary>The shipping carrier for the vendor location.</summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="T:PX.Objects.CS.Carrier.carrierID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  public virtual string CCarrierID
  {
    get => this._CCarrierID;
    set => this._CCarrierID = value;
  }

  /// <summary>The customer's shipping terms.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CS.ShipTerms.ShipTermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  public virtual string CShipTermsID
  {
    get => this._CShipTermsID;
    set => this._CShipTermsID = value;
  }

  /// <summary>The customer's shipping zone.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CS.ShippingZone.ZoneID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Zone")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShippingZone.description))]
  public virtual string CShipZoneID
  {
    get => this._CShipZoneID;
    set => this._CShipZoneID = value;
  }

  /// <summary>The customer's FOB (free on board) shipping point.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CS.FOBPoint.FOBPointID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  public virtual string CFOBPointID
  {
    get => this._CFOBPointID;
    set => this._CFOBPointID = value;
  }

  /// <summary>
  /// This field indicates whether the residential delivery is available in this location.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? CResedential
  {
    get => this._CResedential;
    set => this._CResedential = value;
  }

  /// <summary>
  /// A Boolean value that enables the additional handling shipping option (if set to <see langword="true" />).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Additional Handling")]
  public bool? CAdditionalHandling { get; set; }

  /// <summary>
  /// A Boolean value that enables the lift gate shipping option (if set to <see langword="true" />).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Lift Gate")]
  public bool? CLiftGate { get; set; }

  /// <summary>
  /// A Boolean value that enables the inside delivery shipping option (if set to <see langword="true" />).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inside Delivery")]
  public bool? CInsideDelivery { get; set; }

  /// <summary>
  /// A Boolean value that enables the limited access shipping option (if set to <see langword="true" />).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Limited Access")]
  public bool? CLimitedAccess { get; set; }

  /// <summary>
  /// This field indicates whether the Saturday delivery is available in this location.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? CSaturdayDelivery
  {
    get => this._CSaturdayDelivery;
    set => this._CSaturdayDelivery = value;
  }

  /// <summary>
  /// This field indicates whether the FedEx Ground Collect program is available in this location.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ground Collect")]
  public virtual bool? CGroundCollect
  {
    get => this._CGroundCollect;
    set => this._CGroundCollect = value;
  }

  /// <summary>
  /// This field indicates whether the delivery insurance is available in this location.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? CInsurance
  {
    get => this._CInsurance;
    set => this._CInsurance = value;
  }

  /// <summary>
  /// The amount of lead days (the time in days from the moment when the production was finished to the moment when the customer's order was delivered).
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 100000)]
  [PXUIField(DisplayName = "Lead Time (Days)")]
  public virtual short? CLeadTime
  {
    get => this._CLeadTime;
    set => this._CLeadTime = value;
  }

  /// <summary>
  /// The identifier of the default branch of the customer location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, false)]
  public virtual int? CBranchID
  {
    get => this._CBranchID;
    set => this._CBranchID = value;
  }

  /// <summary>The identifier of the customer's sales account.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? CSalesAcctID
  {
    get => this._CSalesAcctID;
    set => this._CSalesAcctID = value;
  }

  /// <summary>The identifier of the customer's sales subaccount.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.cSalesAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.cSalesSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? CSalesSubID
  {
    get => this._CSalesSubID;
    set => this._CSalesSubID = value;
  }

  /// <summary>The price class of the customer.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AR.ARPriceClass.PriceClassID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (ARPriceClass.priceClassID), DescriptionField = typeof (ARPriceClass.description))]
  [PXUIField]
  public virtual string CPriceClassID
  {
    get => this._CPriceClassID;
    set => this._CPriceClassID = value;
  }

  /// <summary>The warehouse identifier of the customer location.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.IN.INSite.SiteID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  [PXForeignReference(typeof (Field<Location.cSiteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? CSiteID
  {
    get => this._CSiteID;
    set => this._CSiteID = value;
  }

  /// <summary>
  /// The identifier of the discount account of the customer location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? CDiscountAcctID
  {
    get => this._CDiscountAcctID;
    set => this._CDiscountAcctID = value;
  }

  /// <summary>
  /// The identifier of the discount subaccount of the customer's location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.cDiscountAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.cDiscountSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? CDiscountSubID
  {
    get => this._CDiscountSubID;
    set => this._CDiscountSubID = value;
  }

  /// <summary>
  /// The identifier of the retainage account of the customer's location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? CRetainageAcctID { get; set; }

  /// <summary>
  /// The identifier of the retainage subaccount of the customer's location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.cRetainageAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.cRetainageSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? CRetainageSubID { get; set; }

  /// <summary>
  /// The identifier of the freight account of the customer's location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? CFreightAcctID
  {
    get => this._CFreightAcctID;
    set => this._CFreightAcctID = value;
  }

  /// <summary>
  /// The identifier of the freight subaccount of the customer location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.cFreightAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.cFreightSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? CFreightSubID
  {
    get => this._CFreightSubID;
    set => this._CFreightSubID = value;
  }

  /// <summary>The shipping rule of the customer location.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.SO.SOShipComplete.ListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.SO.SOShipComplete.CancelRemainder" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string CShipComplete
  {
    get => this._CShipComplete;
    set => this._CShipComplete = value;
  }

  /// <summary>The order priority of the customer's location.</summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Order Priority")]
  public virtual short? COrderPriority
  {
    get => this._COrderPriority;
    set => this._COrderPriority = value;
  }

  /// <summary>
  /// The type of the work calendar in the customer location.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CS.CSCalendar.CalendarID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CCalendarID
  {
    get => this._CCalendarID;
    set => this._CCalendarID = value;
  }

  /// <summary>
  /// The identifier of the default project of the customer location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [Project(typeof (Where<PMProject.customerID, Equal<Current<Location.bAccountID>>>), DisplayName = "Default Project")]
  public virtual int? CDefProjectID
  {
    get => this._CDefProjectID;
    set => this._CDefProjectID = value;
  }

  /// <summary>
  /// The identifier of the AR account location of the customer location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault]
  [PXDBDefault(typeof (Location.locationID))]
  public virtual int? CARAccountLocationID
  {
    get => this._CARAccountLocationID;
    set => this._CARAccountLocationID = value;
  }

  /// <summary>
  /// The identifier of the AR account of the customer location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account", Required = true)]
  public virtual int? CARAccountID
  {
    get => this._CARAccountID;
    set => this._CARAccountID = value;
  }

  /// <summary>
  /// The identifier of the AR subaccount of the customer location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.cARAccountID), DisplayName = "AR Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description), Required = true)]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.cARSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? CARSubID
  {
    get => this._CARSubID;
    set => this._CARSubID = value;
  }

  /// <summary>
  /// This field indicates that the <see cref="P:PX.Objects.CR.Location.CARAccountLocationID">AR account location</see> is the same in the customer location.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Same As Default Location's")]
  [PXFormula(typeof (Switch<Case<Where<Location.locationID, Equal<Location.cARAccountLocationID>>, False>, True>))]
  public virtual bool? IsARAccountSameAsMain
  {
    get => this._IsARAccountSameAsMain;
    set => this._IsARAccountSameAsMain = value;
  }

  /// <summary>The vendor's tax zone.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.TX.TaxZone.taxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
  public virtual string VTaxZoneID
  {
    get => this._VTaxZoneID;
    set => this._VTaxZoneID = value;
  }

  /// <summary>The vendor's tax calculation mode.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.TX.TaxCalculationMode.ListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.TX.TaxCalculationMode.TaxSetting" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<VendorClass.taxCalcMode, Where<VendorClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string VTaxCalcMode { get; set; }

  /// <summary>The shipping carrier for the customer location.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CS.Carrier.CarrierID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  public virtual string VCarrierID
  {
    get => this._VCarrierID;
    set => this._VCarrierID = value;
  }

  /// <summary>The vendor's shipping terms.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.CS.ShipTerms.shipTermsID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  public virtual string VShipTermsID
  {
    get => this._VShipTermsID;
    set => this._VShipTermsID = value;
  }

  /// <summary>The vendor's FOB (free on board) shipping point.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CS.FOBPoint.fOBPointID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  public virtual string VFOBPointID
  {
    get => this._VFOBPointID;
    set => this._VFOBPointID = value;
  }

  /// <summary>
  /// The amount of lead days (the time in days from the moment when the production was finished to the moment when the vendor's order was delivered).
  /// </summary>
  [PXDBShort(MinValue = 0, MaxValue = 100000)]
  [PXUIField(DisplayName = "Lead Time (Days)")]
  public virtual short? VLeadTime
  {
    get => this._VLeadTime;
    set => this._VLeadTime = value;
  }

  /// <summary>
  /// The identifier of the default branch of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Branch.branchID" /> field.
  /// </value>
  [Branch(null, null, true, true, false)]
  public virtual int? VBranchID
  {
    get => this._VBranchID;
    set => this._VBranchID = value;
  }

  /// <summary>
  /// The identifier of the expense account of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? VExpenseAcctID
  {
    get => this._VExpenseAcctID;
    set => this._VExpenseAcctID = value;
  }

  /// <summary>
  /// The identifier of the expense subaccount of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.vExpenseAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.vExpenseSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? VExpenseSubID
  {
    get => this._VExpenseSubID;
    set => this._VExpenseSubID = value;
  }

  /// <summary>
  /// The identifier of the retainage account of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? VRetainageAcctID { get; set; }

  /// <summary>
  /// The identifier of the retainage subaccount of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.vRetainageAcctID))]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.vRetainageSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? VRetainageSubID { get; set; }

  /// <summary>
  /// The identidier of the freight account of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? VFreightAcctID
  {
    get => this._VFreightAcctID;
    set => this._VFreightAcctID = value;
  }

  /// <summary>
  /// The identifier of the freight subaccount of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.vFreightAcctID))]
  public virtual int? VFreightSubID
  {
    get => this._VFreightSubID;
    set => this._VFreightSubID = value;
  }

  /// <summary>
  /// The identifier of the discount account of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  public virtual int? VDiscountAcctID { get; set; }

  /// <summary>
  /// The identifier of the discount subaccount of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.vDiscountSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  [SubAccount(typeof (Location.vDiscountAcctID))]
  public virtual int? VDiscountSubID { get; set; }

  /// <summary>
  /// The minimal receipt amount for the vendor location in percentages.
  /// </summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Min. Receipt (%)")]
  public virtual Decimal? VRcptQtyMin
  {
    get => this._VRcptQtyMin;
    set => this._VRcptQtyMin = value;
  }

  /// <summary>
  /// The maximum receipt amount for the vendor location in percentages.
  /// </summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Max. Receipt (%)")]
  public virtual Decimal? VRcptQtyMax
  {
    get => this._VRcptQtyMax;
    set => this._VRcptQtyMax = value;
  }

  /// <summary>
  /// The threshold receipt amount for the vendor location in percentages.
  /// </summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Threshold Receipt (%)")]
  public virtual Decimal? VRcptQtyThreshold
  {
    get => this._VRcptQtyThreshold;
    set => this._VRcptQtyThreshold = value;
  }

  /// <summary>
  /// The type of the receipt action for the vendor location.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.PO.POReceiptQtyAction.ListAttribute" /> class.
  /// The default value is <see cref="F:PX.Objects.PO.POReceiptQtyAction.AcceptButWarn" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("W")]
  [POReceiptQtyAction.List]
  [PXUIField(DisplayName = "Receipt Action")]
  public virtual string VRcptQtyAction
  {
    get => this._VRcptQtyAction;
    set => this._VRcptQtyAction = value;
  }

  /// <summary>The warehouse identifier of the vendor location.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.INSite.siteID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  [PXForeignReference(typeof (Field<Location.vSiteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? VSiteID
  {
    get => this._VSiteID;
    set => this._VSiteID = value;
  }

  /// <exclude />
  [PXShort]
  [PXDBCalced(typeof (Switch<Case<Where<Location.vSiteID, IsNull>, shortMax>, short0>), typeof (short))]
  public virtual short? VSiteIDIsNull { get; set; }

  /// <summary>
  /// This field indicates whether the order an order should be printed in the vendor location.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Order")]
  public virtual bool? VPrintOrder
  {
    get => this._VPrintOrder;
    set => this._VPrintOrder = value;
  }

  /// <summary>
  /// This field indicates whether the order should be sent by email in the vendor location.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Email Order")]
  public virtual bool? VEmailOrder
  {
    get => this._VEmailOrder;
    set => this._VEmailOrder = value;
  }

  /// <summary>
  /// The identifier of the default project for the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.PM.PMProject.ContractID" /> field.
  /// </value>
  [Obsolete]
  [Project(DisplayName = "Default Project")]
  public virtual int? VDefProjectID
  {
    get => this._VDefProjectID;
    set => this._VDefProjectID = value;
  }

  /// <summary>
  /// The identifier of the AP location of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault]
  [PXDBDefault(typeof (Location.locationID))]
  public virtual int? VAPAccountLocationID
  {
    get => this._VAPAccountLocationID;
    set => this._VAPAccountLocationID = value;
  }

  /// <summary>
  /// The identifier of the AP account of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// </value>
  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", Required = true, ControlAccountForModule = "AP")]
  public virtual int? VAPAccountID
  {
    get => this._VAPAccountID;
    set => this._VAPAccountID = value;
  }

  /// <summary>
  /// The identifier of the AP subaccount of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Sub.subID" /> field.
  /// </value>
  [SubAccount(typeof (Location.vAPAccountID), DisplayName = "AP Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description), Required = true)]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<Location.vAPSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? VAPSubID
  {
    get => this._VAPSubID;
    set => this._VAPSubID = value;
  }

  /// <summary>
  /// The indentifier of the vendor payment info in the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [PXDBInt]
  [PXDefault]
  [PXDBDefault(typeof (Location.locationID))]
  public virtual int? VPaymentInfoLocationID
  {
    get => this._VPaymentInfoLocationID;
    set => this._VPaymentInfoLocationID = value;
  }

  /// <summary>
  /// This field indicates whether the remit address is not the same as the default address for this location.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Location.vRemitAddressID, Equal<Location.vDefAddressID>>, False>, True>))]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideRemitAddress { get; set; }

  /// <exclude />
  [Obsolete("Use OverrideRemitAddress instead")]
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsRemitAddressSameAsMain
  {
    get
    {
      if (!this.OverrideRemitAddress.HasValue)
        return new bool?();
      bool? overrideRemitAddress = this.OverrideRemitAddress;
      return !overrideRemitAddress.HasValue ? new bool?() : new bool?(!overrideRemitAddress.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The identifier of the remit address of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (Address.addressID))]
  [PXForeignReference(typeof (Location.FK.RemitAddress))]
  public virtual int? VRemitAddressID
  {
    get => this._VRemitAddressID;
    set => this._VRemitAddressID = value;
  }

  /// <summary>
  /// This field indicates whether the remit contact is not the same as the default contact for this location.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Location.vRemitContactID, Equal<Location.vDefContactID>>, False>, True>))]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideRemitContact { get; set; }

  /// <exclude />
  [Obsolete("Use OverrideRemitContact instead")]
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsRemitContactSameAsMain
  {
    get
    {
      if (!this.OverrideRemitContact.HasValue)
        return new bool?();
      bool? overrideRemitContact = this.OverrideRemitContact;
      return !overrideRemitContact.HasValue ? new bool?() : new bool?(!overrideRemitContact.GetValueOrDefault());
    }
  }

  /// <summary>
  /// The identifier of the remit contact identifier of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  [PXForeignReference(typeof (Location.FK.RemitContact))]
  public virtual int? VRemitContactID
  {
    get => this._VRemitContactID;
    set => this._VRemitContactID = value;
  }

  /// <summary>
  /// The payment method indentifier of the vendor location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXDefault]
  public virtual string VPaymentMethodID
  {
    get => this._VPaymentMethodID;
    set => this._VPaymentMethodID = value;
  }

  /// <summary>The cash account indentifier of the vendor location.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [CashAccount(typeof (Location.vBranchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<Location.vPaymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>))]
  public virtual int? VCashAccountID
  {
    get => this._VCashAccountID;
    set => this._VCashAccountID = value;
  }

  /// <summary>
  /// The amount of the payment lead days for the vendor location.
  /// </summary>
  [PXDBShort(MinValue = -3660, MaxValue = 3660)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Payment Lead Time (Days)")]
  public short? VPaymentLeadTime
  {
    get => this._VPaymentLeadTime;
    set => this._VPaymentLeadTime = value;
  }

  /// <summary>
  /// An option that defines when a vendor should be paid at this location.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.AP.APPaymentBy.List" /> class.
  /// The default value is <see cref="F:PX.Objects.AP.APPaymentBy.DueDate" />.
  /// </value>
  [PXDBInt]
  [PXDefault(0)]
  [APPaymentBy.List]
  [PXUIField(DisplayName = "Payment By")]
  public int? VPaymentByType
  {
    get => this._VPaymentByType;
    set => this._VPaymentByType = value;
  }

  /// <summary>
  /// This field indicates whether a vendor should pay separately in this location.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Pay Separately")]
  [PXDefault(false)]
  public virtual bool? VSeparateCheck
  {
    get => this._VSeparateCheck;
    set => this._VSeparateCheck = value;
  }

  /// <summary>
  /// The amount of prepayment percentage for the vendor location.
  /// </summary>
  /// <value>The default value is 100.</value>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Prepayment Percent")]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public virtual Decimal? VPrepaymentPct { get; set; }

  /// <summary>
  /// This field indicates that, in this location, a vendor can create a bill before creating a receipt.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Allow AP Bill Before Receipt")]
  [PXDefault(false)]
  public virtual bool? VAllowAPBillBeforeReceipt { get; set; }

  /// <summary>
  /// This field indicates that the vendor AP location is not the same as this location.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Same As Default Location's")]
  [PXFormula(typeof (Switch<Case<Where<Location.locationID, Equal<Location.vAPAccountLocationID>>, False>, True>))]
  public virtual bool? IsAPAccountSameAsMain
  {
    get => this._IsAPAccountSameAsMain;
    set => this._IsAPAccountSameAsMain = value;
  }

  /// <summary>
  /// This field indicates tha the AP payment info in the default location is not the same as the one specified in this location.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Same As Default Location's")]
  [PXFormula(typeof (Switch<Case<Where<Location.locationID, Equal<Location.vPaymentInfoLocationID>>, False>, True>))]
  public virtual bool? IsAPPaymentInfoSameAsMain
  {
    get => this._IsAPPaymentInfoSameAsMain;
    set => this._IsAPPaymentInfoSameAsMain = value;
  }

  /// <summary>
  /// The identifier of the AP subaccount of the business account.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.LocationARAccountSub.BAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (LocationAPAccountSub.bAccountID))]
  [PXExtraKey]
  public virtual int? LocationAPAccountSubBAccountID
  {
    get => new int?();
    set
    {
    }
  }

  /// <summary>The identifier of the AP account.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AP Account", BqlField = typeof (LocationAPAccountSub.vAPAccountID))]
  public virtual int? APAccountID
  {
    get => this._APAccountID;
    set => this._APAccountID = value;
  }

  /// <summary>The identifier of the AP subaccount.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Sub.subID" /> field.
  /// </value>
  [SubAccount(typeof (Location.aPAccountID), BqlField = typeof (LocationAPAccountSub.vAPSubID), DisplayName = "AP Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? APSubID
  {
    get => this._APSubID;
    set => this._APSubID = value;
  }

  /// <summary>The identifier of the AP retainage account.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AP.LocationAPAccountSub.VRetainageAcctID" /> field.
  /// </value>
  [Account]
  public virtual int? APRetainageAcctID { get; set; }

  /// <summary>The identifier of the AP retainage subaccount.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AP.LocationAPAccountSub.VRetainageSubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.vRetainageAcctID))]
  public virtual int? APRetainageSubID { get; set; }

  /// <summary>
  /// The identifier of the AR subaccount of the business account.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.LocationARAccountSub.BAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (LocationARAccountSub.bAccountID))]
  [PXExtraKey]
  public virtual int? LocationARAccountSubBAccountID
  {
    get => new int?();
    set
    {
    }
  }

  /// <summary>The identifier of the AR account.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account", BqlField = typeof (LocationARAccountSub.cARAccountID))]
  public virtual int? ARAccountID
  {
    get => this._ARAccountID;
    set => this._ARAccountID = value;
  }

  /// <summary>The identifier of the AR account.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.LocationARAccountSub.CARSubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.aRAccountID), BqlField = typeof (LocationARAccountSub.cARSubID), DisplayName = "AR Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? ARSubID
  {
    get => this._ARSubID;
    set => this._ARSubID = value;
  }

  /// <summary>The identifier of the AR retainage account.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CR.LocationARAccountSub.cRetainageAcctID" /> field.
  /// </value>
  [Account]
  public virtual int? ARRetainageAcctID { get; set; }

  /// <summary>The identifier of the AR retainage subaccount.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.LocationARAccountSub.CRetainageSubID" /> field.
  /// </value>
  [SubAccount(typeof (Location.aRRetainageAcctID))]
  public virtual int? ARRetainageSubID { get; set; }

  /// <summary>
  /// The identifier of AP payment info of the business account.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.AP.LocationAPPaymentInfo.bAccountID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (LocationAPPaymentInfo.bAccountID))]
  [PXExtraKey]
  public virtual int? LocationAPPaymentInfoBAccountID
  {
    get => new int?();
    set
    {
    }
  }

  /// <exclude />
  [Obsolete]
  [PXInt]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  /// <exclude />
  [Obsolete]
  [PXInt]
  public virtual int? RemitContactID
  {
    get => this._RemitContactID;
    set => this._RemitContactID = value;
  }

  /// <summary>The identifier of the payment method.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AP.LocationAPPaymentInfo.VPaymentMethodID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (LocationAPPaymentInfo.vPaymentMethodID))]
  [PXUIField(DisplayName = "Payment Method")]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  /// <summary>The identifier of the cash account.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.AP.LocationAPPaymentInfo.VCashAccountID" /> field.
  /// </value>
  [CashAccount(typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<Location.vPaymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>))]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  /// <summary>The amount of payment lead days.</summary>
  [PXDBShort(BqlField = typeof (LocationAPPaymentInfo.vPaymentLeadTime), MinValue = 0, MaxValue = 3660)]
  [PXUIField(DisplayName = "Payment Lead Time (Days)")]
  public short? PaymentLeadTime
  {
    get => this._PaymentLeadTime;
    set => this._PaymentLeadTime = value;
  }

  /// <summary>
  /// This field indicates whether the vendor should pay separately.
  /// </summary>
  /// <value>
  /// The default value is <see langword="false" />.
  /// </value>
  [PXDBBool(BqlField = typeof (LocationAPPaymentInfo.vSeparateCheck))]
  [PXUIField(DisplayName = "Pay Separately")]
  public virtual bool? SeparateCheck
  {
    get => this._SeparateCheck;
    set => this._SeparateCheck = value;
  }

  /// <summary>
  /// The option that defines when the customer should be paid at this location.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.AP.APPaymentBy.List" /> class.
  /// The default value is <see cref="F:PX.Objects.AP.APPaymentBy.DueDate" />.
  /// </value>
  [PXDBInt(BqlField = typeof (LocationAPPaymentInfo.vPaymentByType))]
  [PXDefault(0)]
  [APPaymentBy.List]
  [PXUIField(DisplayName = "Payment By")]
  public int? PaymentByType
  {
    get => this._PaymentByType;
    set => this._PaymentByType = value;
  }

  /// <exclude />
  [PXDBInt(BqlField = typeof (BAccountR.bAccountID))]
  [PXExtraKey]
  public virtual int? BAccountBAccountID
  {
    get => new int?();
    set
    {
    }
  }

  /// <summary>
  /// The identifier of the vendor's default address of the location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Address.AddressID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (BAccountR.defAddressID))]
  [PXDefault(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<Location.bAccountID>>>>), SourceField = typeof (BAccount.defAddressID))]
  public virtual int? VDefAddressID
  {
    get => this._VDefAddressID;
    set => this._VDefAddressID = value;
  }

  /// <summary>
  /// The identifier of the vendor's default contact of the location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (BAccountR.defContactID))]
  [PXDefault(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<Location.bAccountID>>>>), SourceField = typeof (BAccount.defContactID))]
  public virtual int? VDefContactID
  {
    get => this._VDefContactID;
    set => this._VDefContactID = value;
  }

  /// <summary>
  /// The identifier of the sales subacount of the company location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  public virtual int? CMPSalesSubID
  {
    get => this._CMPSalesSubID;
    set => this._CMPSalesSubID = value;
  }

  /// <summary>
  /// The identifier of the expense subaccount of the company location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  public virtual int? CMPExpenseSubID
  {
    get => this._CMPExpenseSubID;
    set => this._CMPExpenseSubID = value;
  }

  /// <summary>
  /// The identifier of the freight subaccount of the company location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  public virtual int? CMPFreightSubID
  {
    get => this._CMPFreightSubID;
    set => this._CMPFreightSubID = value;
  }

  /// <summary>
  /// The identifier of the discount subaccount of the company location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  public virtual int? CMPDiscountSubID
  {
    get => this._CMPDiscountSubID;
    set => this._CMPDiscountSubID = value;
  }

  /// <summary>
  /// The identifier of the currency gain and loss subaccount of the company location.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  public virtual int? CMPGainLossSubID
  {
    get => this._CMPGainLossSubID;
    set => this._CMPGainLossSubID = value;
  }

  /// <summary>The warehouse of the company location.</summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.INSite.siteID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  public virtual int? CMPSiteID
  {
    get => this._CMPSiteID;
    set => this._CMPSiteID = value;
  }

  /// <summary>The external reference number</summary>
  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "External ID", Visible = true)]
  public string ExtRefNbr { get; set; }

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

  [Obsolete("Use OverrideAddress instead")]
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsAddressSameAsMain
  {
    get
    {
      if (!this.OverrideAddress.HasValue)
        return new bool?();
      bool? overrideAddress = this.OverrideAddress;
      return !overrideAddress.HasValue ? new bool?() : new bool?(!overrideAddress.GetValueOrDefault());
    }
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the address
  /// overrides the default <see cref="T:PX.Objects.CR.Address" /> record, which is
  /// referenced by <see cref="P:PX.Objects.CR.Location.DefAddressID" />.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Location.defAddressID, Equal<Location.vDefAddressID>>, False>, True>))]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideAddress { get; set; }

  [Obsolete("Use OverrideContact instead")]
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsContactSameAsMain
  {
    get
    {
      if (!this.OverrideContact.HasValue)
        return new bool?();
      bool? overrideContact = this.OverrideContact;
      return !overrideContact.HasValue ? new bool?() : new bool?(!overrideContact.GetValueOrDefault());
    }
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the address
  /// overrides the default <see cref="T:PX.Objects.CR.Contact" /> record, which is
  /// referenced by <see cref="P:PX.Objects.CR.Location.DefContactID" />.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Location.defContactID, Equal<Location.vDefContactID>>, False>, True>))]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideContact { get; set; }

  public static string GetKeyImage(int? baccountID, int? locationID)
  {
    return $"{typeof (Location.bAccountID).Name}:{baccountID}, {typeof (Location.locationID).Name}:{locationID}";
  }

  public string GetKeyImage() => Location.GetKeyImage(this.BAccountID, this.LocationID);

  public static string GetImage(int? baccountID, int? locationID)
  {
    return $"{EntityHelper.GetFriendlyEntityName(typeof (Location))}[{Location.GetKeyImage(baccountID, locationID)}]";
  }

  public virtual string ToString() => Location.GetImage(this.BAccountID, this.LocationID);

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>
  {
    public static Location Find(
      PXGraph graph,
      int? bAccountID,
      int? locationID,
      PKFindOptions options = 0)
    {
      return (Location) PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.FindBy(graph, (object) bAccountID, (object) locationID, options);
    }
  }

  public class UK : PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationCD>
  {
    public static Location Find(
      PXGraph graph,
      int? bAccountID,
      string locationCD,
      PKFindOptions options = 0)
    {
      return (Location) PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationCD>.FindBy(graph, (object) bAccountID, (object) locationCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Default Address</summary>
    public class Address : 
      PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<Location>.By<Location.defAddressID>
    {
    }

    /// <summary>Default Contact</summary>
    public class ContactInfo : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<Location>.By<Location.defContactID>
    {
    }

    /// <summary>BAccount that locations belongs</summary>
    public class BAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<Location>.By<Location.bAccountID>
    {
    }

    /// <summary>Remit contact</summary>
    public class RemitContact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<Location>.By<Location.vRemitContactID>
    {
    }

    /// <summary>Remit address</summary>
    public class RemitAddress : 
      PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<Location>.By<Location.vRemitAddressID>
    {
    }
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.locationID>
  {
  }

  public abstract class locationCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.locationCD>
  {
  }

  public abstract class locType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.locType>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.descr>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Location.taxRegistrationID>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.defAddressID>
  {
  }

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.defContactID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Location.noteID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.isActive>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.status>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.isDefault>
  {
  }

  public abstract class cTaxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cTaxZoneID>
  {
  }

  public abstract class cTaxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cTaxCalcMode>
  {
  }

  public abstract class cAvalaraExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Location.cAvalaraExemptionNumber>
  {
  }

  public abstract class cAvalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Location.cAvalaraCustomerUsageType>
  {
  }

  public abstract class cCarrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cCarrierID>
  {
  }

  public abstract class cShipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cShipTermsID>
  {
  }

  public abstract class cShipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cShipZoneID>
  {
  }

  public abstract class cFOBPointID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cFOBPointID>
  {
  }

  public abstract class cResedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cResedential>
  {
  }

  public abstract class cAdditionalHandling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.cAdditionalHandling>
  {
  }

  public abstract class cLiftGate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cLiftGate>
  {
  }

  public abstract class cInsideDelivery : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cInsideDelivery>
  {
  }

  public abstract class cLimitedAccess : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cLimitedAccess>
  {
  }

  public abstract class cSaturdayDelivery : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cSaturdayDelivery>
  {
  }

  public abstract class cGroundCollect : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cGroundCollect>
  {
  }

  public abstract class cInsurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cInsurance>
  {
  }

  public abstract class cLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Location.cLeadTime>
  {
  }

  public abstract class cBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cBranchID>
  {
  }

  public abstract class cSalesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cSalesAcctID>
  {
  }

  public abstract class cSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cSalesSubID>
  {
  }

  public abstract class cPriceClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cPriceClassID>
  {
  }

  public abstract class cSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cSiteID>
  {
  }

  public abstract class cDiscountAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cDiscountAcctID>
  {
  }

  public abstract class cDiscountSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cDiscountSubID>
  {
  }

  public abstract class cRetainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cRetainageAcctID>
  {
  }

  public abstract class cRetainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cRetainageSubID>
  {
  }

  public abstract class cFreightAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cFreightAcctID>
  {
  }

  public abstract class cFreightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cFreightSubID>
  {
  }

  public abstract class cShipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cShipComplete>
  {
  }

  public abstract class cOrderPriority : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Location.cOrderPriority>
  {
  }

  public abstract class cCalendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.cCalendarID>
  {
  }

  public abstract class cDefProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cDefProjectID>
  {
  }

  public abstract class cARAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Location.cARAccountLocationID>
  {
  }

  public abstract class cARAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cARAccountID>
  {
  }

  public abstract class cARSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cARSubID>
  {
  }

  public abstract class isARAccountSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.isARAccountSameAsMain>
  {
  }

  public abstract class vTaxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.vTaxZoneID>
  {
  }

  public abstract class vTaxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.vTaxCalcMode>
  {
  }

  public abstract class vCarrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.vCarrierID>
  {
  }

  public abstract class vShipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.vShipTermsID>
  {
  }

  public abstract class vFOBPointID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.vFOBPointID>
  {
  }

  public abstract class vLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Location.vLeadTime>
  {
  }

  public abstract class vBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vBranchID>
  {
  }

  public abstract class vExpenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vExpenseAcctID>
  {
  }

  public abstract class vExpenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vExpenseSubID>
  {
  }

  public abstract class vRetainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vRetainageAcctID>
  {
  }

  public abstract class vRetainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vRetainageSubID>
  {
  }

  public abstract class vFreightAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vFreightAcctID>
  {
  }

  public abstract class vFreightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vFreightSubID>
  {
  }

  public abstract class vDiscountAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vDiscountAcctID>
  {
  }

  public abstract class vDiscountSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vDiscountSubID>
  {
  }

  public abstract class vRcptQtyMin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Location.vRcptQtyMin>
  {
  }

  public abstract class vRcptQtyMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Location.vRcptQtyMax>
  {
  }

  public abstract class vRcptQtyThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Location.vRcptQtyThreshold>
  {
  }

  public abstract class vRcptQtyAction : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.vRcptQtyAction>
  {
  }

  public abstract class vSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vSiteID>
  {
  }

  public abstract class vSiteIDIsNull : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Location.vSiteIDIsNull>
  {
  }

  public abstract class vPrintOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.vPrintOrder>
  {
  }

  public abstract class vEmailOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.vEmailOrder>
  {
  }

  public abstract class vDefProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vDefProjectID>
  {
  }

  public abstract class vAPAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Location.vAPAccountLocationID>
  {
  }

  public abstract class vAPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vAPAccountID>
  {
  }

  public abstract class vAPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vAPSubID>
  {
  }

  public abstract class vPaymentInfoLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Location.vPaymentInfoLocationID>
  {
  }

  public abstract class overrideRemitAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.overrideRemitAddress>
  {
  }

  /// <exclude />
  [Obsolete("Use OverrideRemitAddress instead")]
  public abstract class isRemitAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.isRemitAddressSameAsMain>
  {
  }

  public abstract class vRemitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vRemitAddressID>
  {
  }

  public abstract class overrideRemitContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.overrideRemitContact>
  {
  }

  /// <exclude />
  [Obsolete("Use OverrideRemitContact instead")]
  public abstract class isRemitContactSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.isRemitContactSameAsMain>
  {
  }

  public abstract class vRemitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vRemitContactID>
  {
  }

  public abstract class vPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Location.vPaymentMethodID>
  {
  }

  public abstract class vCashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vCashAccountID>
  {
  }

  public abstract class vPaymentLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Location.vPaymentLeadTime>
  {
  }

  public abstract class vPaymentByType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vPaymentByType>
  {
  }

  public abstract class vSeparateCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.vSeparateCheck>
  {
  }

  public abstract class vPrepaymentPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Location.vPrepaymentPct>
  {
  }

  public abstract class vAllowAPBillBeforeReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.vAllowAPBillBeforeReceipt>
  {
  }

  public abstract class isAPAccountSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.isAPAccountSameAsMain>
  {
  }

  public abstract class isAPPaymentInfoSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.isAPPaymentInfoSameAsMain>
  {
  }

  public abstract class locationAPAccountSubBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Location.locationAPAccountSubBAccountID>
  {
  }

  public abstract class aPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aPAccountID>
  {
  }

  public abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aPSubID>
  {
  }

  public abstract class aPRetainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aPRetainageAcctID>
  {
  }

  public abstract class aPRetainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aPRetainageSubID>
  {
  }

  public abstract class locationARAccountSubBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Location.locationARAccountSubBAccountID>
  {
  }

  public abstract class aRAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aRAccountID>
  {
  }

  public abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aRSubID>
  {
  }

  public abstract class aRRetainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aRRetainageAcctID>
  {
  }

  public abstract class aRRetainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.aRRetainageSubID>
  {
  }

  public abstract class locationAPPaymentInfoBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Location.locationAPPaymentInfoBAccountID>
  {
  }

  [Obsolete]
  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.remitAddressID>
  {
  }

  [Obsolete]
  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.remitContactID>
  {
  }

  public abstract class paymentMethodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cashAccountID>
  {
  }

  public abstract class paymentLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Location.paymentLeadTime>
  {
  }

  public abstract class separateCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.separateCheck>
  {
  }

  public abstract class paymentByType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.paymentByType>
  {
  }

  public abstract class bAccountBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.bAccountBAccountID>
  {
  }

  public abstract class vDefAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vDefAddressID>
  {
  }

  public abstract class vDefContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.vDefContactID>
  {
  }

  public abstract class cMPSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cMPSalesSubID>
  {
  }

  public abstract class cMPExpenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cMPExpenseSubID>
  {
  }

  public abstract class cMPFreightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cMPFreightSubID>
  {
  }

  public abstract class cMPDiscountSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cMPDiscountSubID>
  {
  }

  public abstract class cMPGainLossSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cMPGainLossSubID>
  {
  }

  public abstract class cMPSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.cMPSiteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.ExtRefNbr" />
  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.extRefNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Location.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Location.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Location.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Location.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Location.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Location.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Location.lastModifiedDateTime>
  {
  }

  [Obsolete("Use OverrideAddress instead")]
  public abstract class isAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.isAddressSameAsMain>
  {
  }

  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.overrideAddress>
  {
  }

  [Obsolete("Use OverrideContact instead")]
  public abstract class isContactSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Location.isContactSameAsMain>
  {
  }

  public abstract class overrideContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.overrideContact>
  {
  }
}
