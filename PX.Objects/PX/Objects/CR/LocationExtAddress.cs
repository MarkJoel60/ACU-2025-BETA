// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LocationExtAddress
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
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXPrimaryGraph(new System.Type[] {typeof (LocationMaint)}, new System.Type[] {typeof (Select<Location, Where<Location.bAccountID, Equal<Current<LocationExtAddress.bAccountID>>, And<Location.locationID, Equal<Current<LocationExtAddress.locationID>>>>>)})]
[PXProjection(typeof (Select2<PX.Objects.CR.Standalone.Location, LeftJoin<Address, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Address.bAccountID>, And<PX.Objects.CR.Standalone.Location.defAddressID, Equal<Address.addressID>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>>>>>), Persistent = true)]
[PXCacheName("Location with Address")]
[Serializable]
public class LocationExtAddress : Address, IDefAddressAccessor, ILocation
{
  protected int? _LocationBAccountID;
  protected int? _LocationID;
  protected 
  #nullable disable
  string _LocationCD;
  protected string _LocType;
  protected string _Descr;
  protected string _TaxRegistrationID;
  protected int? _DefAddressID;
  protected int? _DefContactID;
  protected bool? _IsActive;
  protected string _CTaxZoneID;
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
  protected int? _VDiscountAcctID;
  protected int? _VDiscountSubID;
  protected Decimal? _VRcptQtyMin;
  protected Decimal? _VRcptQtyMax;
  protected Decimal? _VRcptQtyThreshold;
  protected string _VRcptQtyAction;
  protected int? _VSiteID;
  protected bool? _VPrintOrder;
  protected bool? _VEmailOrder;
  protected int? _VAPAccountLocationID;
  protected int? _VAPAccountID;
  protected int? _VAPSubID;
  protected int? _VPaymentInfoLocationID;
  protected int? _VRemitAddressID;
  protected int? _VRemitContactID;
  protected string _VPaymentMethodID;
  protected int? _VCashAccountID;
  protected short? _VPaymentLeadTime;
  protected int? _VPaymentByType;
  protected bool? _VSeparateCheck;
  protected bool? _IsAPAccountSameAsMain;
  protected int? _BAccountBAccountID;
  protected int? _VDefAddressID;
  protected int? _VDefContactID;
  protected int? _CMPSalesSubID;
  protected int? _CMPExpenseSubID;
  protected int? _CMPFreightSubID;
  protected int? _CMPDiscountSubID;
  protected int? _CMPGainLossSubID;
  protected int? _CMPSiteID;
  protected Guid? _LocationCreatedByID;
  protected string _LocationCreatedByScreenID;
  protected DateTime? _LocationCreatedDateTime;
  protected Guid? _LocationLastModifiedByID;
  protected string _LocationLastModifiedByScreenID;
  protected DateTime? _LocationLastModifiedDateTime;
  protected bool? _IsAddressSameAsMain;
  protected bool? _IsContactSameAsMain;

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.bAccountID))]
  [PXDBDefault(typeof (BAccount.bAccountID))]
  [PXUIField]
  [PXParent(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<LocationExtAddress.bAccountID>>, And<BAccount.type, NotEqual<BAccountType.combinedType>>>>), LeaveChildren = true)]
  [PXNavigateSelector(typeof (LocationExtAddress.locationBAccountID))]
  public virtual int? LocationBAccountID
  {
    get => this._LocationBAccountID;
    set => this._LocationBAccountID = value;
  }

  [PXDBIdentity(IsKey = false, BqlField = typeof (PX.Objects.CR.Standalone.Location.locationID))]
  [PXUIField]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDefault]
  [LocationRaw(typeof (Where<Location.bAccountID, Equal<Current<LocationExtAddress.bAccountID>>>), null)]
  public virtual string LocationCD
  {
    get => this._LocationCD;
    set => this._LocationCD = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.locType))]
  [LocTypeList.List]
  [PXUIField]
  public virtual string LocType
  {
    get => this._LocType;
    set => this._LocType = value;
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.descr))]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.taxRegistrationID))]
  [PXUIField(DisplayName = "Tax Registration ID")]
  public virtual string TaxRegistrationID
  {
    get => this._TaxRegistrationID;
    set => this._TaxRegistrationID = value;
  }

  [PXDBDefault(typeof (Address.addressID))]
  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.defAddressID))]
  [PXUIField]
  [PXSelector(typeof (Search<Address.addressID, Where<Address.bAccountID, Equal<Current<BAccount.bAccountID>>>>), DirtyRead = true)]
  public virtual int? DefAddressID
  {
    get => this._DefAddressID;
    set => this._DefAddressID = value;
  }

  [PXDBDefault(typeof (Contact.contactID))]
  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.defContactID))]
  public virtual int? DefContactID
  {
    get => this._DefContactID;
    set => this._DefContactID = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.isActive))]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.cTaxZoneID))]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
  public virtual string CTaxZoneID
  {
    get => this._CTaxZoneID;
    set => this._CTaxZoneID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.cTaxCalcMode))]
  [PXDefault("T", typeof (Search<CustomerClass.taxCalcMode, Where<CustomerClass.customerClassID, Equal<Current<CustomerClass.customerClassID>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string CTaxCalcMode { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType))]
  [PXDefault("0")]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string CAvalaraCustomerUsageType
  {
    get => this._CAvalaraCustomerUsageType;
    set => this._CAvalaraCustomerUsageType = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.cCarrierID), InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  public virtual string CCarrierID
  {
    get => this._CCarrierID;
    set => this._CCarrierID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.cShipTermsID))]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  public virtual string CShipTermsID
  {
    get => this._CShipTermsID;
    set => this._CShipTermsID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa", BqlField = typeof (PX.Objects.CR.Standalone.Location.cShipZoneID))]
  [PXUIField(DisplayName = "Shipping Zone ID")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShippingZone.description))]
  public virtual string CShipZoneID
  {
    get => this._CShipZoneID;
    set => this._CShipZoneID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.cFOBPointID))]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  public virtual string CFOBPointID
  {
    get => this._CFOBPointID;
    set => this._CFOBPointID = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cResedential))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? CResedential
  {
    get => this._CResedential;
    set => this._CResedential = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.Location.CAdditionalHandling" />
  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cAdditionalHandling))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Additional Handling")]
  public bool? CAdditionalHandling { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.Location.CLiftGate" />
  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cLiftGate))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Lift Gate")]
  public bool? CLiftGate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.Location.CInsideDelivery" />
  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cInsideDelivery))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inside Delivery")]
  public bool? CInsideDelivery { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.Location.CLimitedAccess" />
  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cLimitedAccess))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Limited Access")]
  public bool? CLimitedAccess { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cSaturdayDelivery))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? CSaturdayDelivery
  {
    get => this._CSaturdayDelivery;
    set => this._CSaturdayDelivery = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cGroundCollect))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ground Collect")]
  public virtual bool? CGroundCollect
  {
    get => this._CGroundCollect;
    set => this._CGroundCollect = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.cInsurance))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? CInsurance
  {
    get => this._CInsurance;
    set => this._CInsurance = value;
  }

  [PXDBShort(BqlField = typeof (PX.Objects.CR.Standalone.Location.cLeadTime))]
  [PXUIField(DisplayName = "Lead Time (Days)")]
  public virtual short? CLeadTime
  {
    get => this._CLeadTime;
    set => this._CLeadTime = value;
  }

  [Branch(null, null, true, true, false)]
  public virtual int? CBranchID
  {
    get => this._CBranchID;
    set => this._CBranchID = value;
  }

  [Account]
  public virtual int? CSalesAcctID
  {
    get => this._CSalesAcctID;
    set => this._CSalesAcctID = value;
  }

  [SubAccount(typeof (LocationExtAddress.cSalesAcctID))]
  public virtual int? CSalesSubID
  {
    get => this._CSalesSubID;
    set => this._CSalesSubID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.cPriceClassID))]
  [PXSelector(typeof (ARPriceClass.priceClassID))]
  [PXUIField]
  public virtual string CPriceClassID
  {
    get => this._CPriceClassID;
    set => this._CPriceClassID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.cSiteID))]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  public virtual int? CSiteID
  {
    get => this._CSiteID;
    set => this._CSiteID = value;
  }

  [Account]
  public virtual int? CDiscountAcctID
  {
    get => this._CDiscountAcctID;
    set => this._CDiscountAcctID = value;
  }

  [SubAccount(typeof (LocationExtAddress.cDiscountAcctID))]
  public virtual int? CDiscountSubID
  {
    get => this._CDiscountSubID;
    set => this._CDiscountSubID = value;
  }

  [Account]
  public virtual int? CRetainageAcctID { get; set; }

  [SubAccount(typeof (LocationExtAddress.cRetainageAcctID))]
  public virtual int? CRetainageSubID { get; set; }

  [Account]
  public virtual int? CFreightAcctID
  {
    get => this._CFreightAcctID;
    set => this._CFreightAcctID = value;
  }

  [SubAccount(typeof (LocationExtAddress.cFreightAcctID))]
  public virtual int? CFreightSubID
  {
    get => this._CFreightSubID;
    set => this._CFreightSubID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.cShipComplete))]
  [SOShipComplete.List]
  [PXDefault("L")]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string CShipComplete
  {
    get => this._CShipComplete;
    set => this._CShipComplete = value;
  }

  [PXDBShort(BqlField = typeof (PX.Objects.CR.Standalone.Location.cOrderPriority))]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Order Priority")]
  public virtual short? COrderPriority
  {
    get => this._COrderPriority;
    set => this._COrderPriority = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.cARAccountLocationID))]
  [PXDefault]
  public virtual int? CARAccountLocationID
  {
    get => this._CARAccountLocationID;
    set => this._CARAccountLocationID = value;
  }

  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AR Account", BqlField = typeof (PX.Objects.CR.Standalone.Location.cARAccountID), Required = true, ControlAccountForModule = "AR")]
  public virtual int? CARAccountID
  {
    get => this._CARAccountID;
    set => this._CARAccountID = value;
  }

  [SubAccount(typeof (LocationExtAddress.cARAccountID), BqlField = typeof (PX.Objects.CR.Standalone.Location.cARSubID), DisplayName = "AR Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description), Required = true)]
  public virtual int? CARSubID
  {
    get => this._CARSubID;
    set => this._CARSubID = value;
  }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<LocationExtAddress.locationID, Equal<LocationExtAddress.cARAccountLocationID>>, False>, True>))]
  public virtual bool? IsARAccountSameAsMain
  {
    get => this._IsARAccountSameAsMain;
    set => this._IsARAccountSameAsMain = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.vTaxZoneID))]
  [PXUIField(DisplayName = "Tax Zone ID")]
  [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
  public virtual string VTaxZoneID
  {
    get => this._VTaxZoneID;
    set => this._VTaxZoneID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.vTaxCalcMode))]
  [PXDefault("T", typeof (Search<VendorClass.taxCalcMode, Where<VendorClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string VTaxCalcMode { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.vCarrierID), InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
  public virtual string VCarrierID
  {
    get => this._VCarrierID;
    set => this._VCarrierID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.vShipTermsID))]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<PX.Objects.CS.ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.ShipTerms.description))]
  public virtual string VShipTermsID
  {
    get => this._VShipTermsID;
    set => this._VShipTermsID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.CR.Standalone.Location.vFOBPointID))]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (PX.Objects.CS.FOBPoint.fOBPointID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.FOBPoint.description))]
  public virtual string VFOBPointID
  {
    get => this._VFOBPointID;
    set => this._VFOBPointID = value;
  }

  [PXDBShort(BqlField = typeof (PX.Objects.CR.Standalone.Location.vLeadTime))]
  [PXUIField(DisplayName = "Lead Time (Days)")]
  public virtual short? VLeadTime
  {
    get => this._VLeadTime;
    set => this._VLeadTime = value;
  }

  [Branch(null, null, true, true, false)]
  public virtual int? VBranchID
  {
    get => this._VBranchID;
    set => this._VBranchID = value;
  }

  [Account]
  public virtual int? VExpenseAcctID
  {
    get => this._VExpenseAcctID;
    set => this._VExpenseAcctID = value;
  }

  [SubAccount(typeof (LocationExtAddress.vExpenseAcctID))]
  public virtual int? VExpenseSubID
  {
    get => this._VExpenseSubID;
    set => this._VExpenseSubID = value;
  }

  [Account]
  public virtual int? VRetainageAcctID { get; set; }

  [SubAccount(typeof (LocationExtAddress.vRetainageAcctID))]
  public virtual int? VRetainageSubID { get; set; }

  [Account]
  public virtual int? VFreightAcctID
  {
    get => this._VFreightAcctID;
    set => this._VFreightAcctID = value;
  }

  [SubAccount(typeof (LocationExtAddress.vFreightAcctID))]
  public virtual int? VFreightSubID
  {
    get => this._VFreightSubID;
    set => this._VFreightSubID = value;
  }

  [Account]
  public virtual int? VDiscountAcctID
  {
    get => this._VDiscountAcctID;
    set => this._VDiscountAcctID = value;
  }

  [SubAccount(typeof (LocationExtAddress.vDiscountAcctID))]
  public virtual int? VDiscountSubID
  {
    get => this._VDiscountSubID;
    set => this._VDiscountSubID = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0, BqlField = typeof (PX.Objects.CR.Standalone.Location.vRcptQtyMin))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Min. Receipt (%)")]
  public virtual Decimal? VRcptQtyMin
  {
    get => this._VRcptQtyMin;
    set => this._VRcptQtyMin = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0, BqlField = typeof (PX.Objects.CR.Standalone.Location.vRcptQtyMax))]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Max. Receipt (%)")]
  public virtual Decimal? VRcptQtyMax
  {
    get => this._VRcptQtyMax;
    set => this._VRcptQtyMax = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0, BqlField = typeof (PX.Objects.CR.Standalone.Location.vRcptQtyThreshold))]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Threshold Receipt (%)")]
  public virtual Decimal? VRcptQtyThreshold
  {
    get => this._VRcptQtyThreshold;
    set => this._VRcptQtyThreshold = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.vRcptQtyAction))]
  [POReceiptQtyAction.List]
  [PXDefault("W")]
  [PXUIField(DisplayName = "Receipt Action")]
  public virtual string VRcptQtyAction
  {
    get => this._VRcptQtyAction;
    set => this._VRcptQtyAction = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vSiteID))]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  public virtual int? VSiteID
  {
    get => this._VSiteID;
    set => this._VSiteID = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPrintOrder))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Orders")]
  public virtual bool? VPrintOrder
  {
    get => this._VPrintOrder;
    set => this._VPrintOrder = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.vEmailOrder))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Send Orders by Email")]
  public virtual bool? VEmailOrder
  {
    get => this._VEmailOrder;
    set => this._VEmailOrder = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPAccountLocationID))]
  [PXDefault]
  public virtual int? VAPAccountLocationID
  {
    get => this._VAPAccountLocationID;
    set => this._VAPAccountLocationID = value;
  }

  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPAccountID), Required = true, ControlAccountForModule = "AP")]
  public virtual int? VAPAccountID
  {
    get => this._VAPAccountID;
    set => this._VAPAccountID = value;
  }

  [SubAccount(typeof (LocationExtAddress.vAPAccountID), BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPSubID), DisplayName = "AP Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description), Required = true)]
  public virtual int? VAPSubID
  {
    get => this._VAPSubID;
    set => this._VAPSubID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID))]
  [PXDefault]
  public virtual int? VPaymentInfoLocationID
  {
    get => this._VPaymentInfoLocationID;
    set => this._VPaymentInfoLocationID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vRemitAddressID))]
  [PXDBChildIdentity(typeof (Address.addressID))]
  public virtual int? VRemitAddressID
  {
    get => this._VRemitAddressID;
    set => this._VRemitAddressID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vRemitContactID))]
  [PXDBChildIdentity(typeof (Contact.contactID))]
  public virtual int? VRemitContactID
  {
    get => this._VRemitContactID;
    set => this._VRemitContactID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentMethodID))]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXDefault]
  public virtual string VPaymentMethodID
  {
    get => this._VPaymentMethodID;
    set => this._VPaymentMethodID = value;
  }

  [CashAccount(typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<LocationExtAddress.vPaymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>))]
  public virtual int? VCashAccountID
  {
    get => this._VCashAccountID;
    set => this._VCashAccountID = value;
  }

  [PXDBShort(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentLeadTime), MinValue = -3660, MaxValue = 3660)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Payment Lead Time (Days)")]
  public short? VPaymentLeadTime
  {
    get => this._VPaymentLeadTime;
    set => this._VPaymentLeadTime = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentByType))]
  [PXDefault(0)]
  [APPaymentBy.List]
  [PXUIField(DisplayName = "Payment By")]
  public int? VPaymentByType
  {
    get => this._VPaymentByType;
    set => this._VPaymentByType = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.vSeparateCheck))]
  [PXUIField(DisplayName = "Pay Separately")]
  [PXDefault(false)]
  public virtual bool? VSeparateCheck
  {
    get => this._VSeparateCheck;
    set => this._VSeparateCheck = value;
  }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0, BqlField = typeof (PX.Objects.CR.Standalone.Location.vPrepaymentPct))]
  [PXUIField(DisplayName = "Prepayment Percent")]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public virtual Decimal? VPrepaymentPct { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.vAllowAPBillBeforeReceipt))]
  [PXUIField(DisplayName = "Allow AP Bill Before Receipt")]
  [PXDefault(false)]
  public virtual bool? VAllowAPBillBeforeReceipt { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<LocationExtAddress.locationID, Equal<LocationExtAddress.vAPAccountLocationID>>, False>, True>))]
  public virtual bool? IsAPAccountSameAsMain
  {
    get => this._IsAPAccountSameAsMain;
    set => this._IsAPAccountSameAsMain = value;
  }

  [PXDBInt(BqlField = typeof (BAccount.bAccountID))]
  [PXExtraKey]
  public virtual int? BAccountBAccountID
  {
    get => new int?();
    set
    {
    }
  }

  [PXDBInt(BqlField = typeof (BAccountR.defAddressID))]
  [PXDefault(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<LocationExtAddress.bAccountID>>>>), SourceField = typeof (BAccount.defAddressID))]
  public virtual int? VDefAddressID
  {
    get => this._VDefAddressID;
    set => this._VDefAddressID = value;
  }

  [PXDBInt(BqlField = typeof (BAccountR.defContactID))]
  [PXDefault(typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<LocationExtAddress.bAccountID>>>>), SourceField = typeof (BAccount.defContactID))]
  public virtual int? VDefContactID
  {
    get => this._VDefContactID;
    set => this._VDefContactID = value;
  }

  [SubAccount]
  public virtual int? CMPSalesSubID
  {
    get => this._CMPSalesSubID;
    set => this._CMPSalesSubID = value;
  }

  [SubAccount]
  public virtual int? CMPExpenseSubID
  {
    get => this._CMPExpenseSubID;
    set => this._CMPExpenseSubID = value;
  }

  [SubAccount(BqlField = typeof (PX.Objects.CR.Standalone.Location.cMPFreightSubID), DisplayName = "Freight Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? CMPFreightSubID
  {
    get => this._CMPFreightSubID;
    set => this._CMPFreightSubID = value;
  }

  [SubAccount(BqlField = typeof (PX.Objects.CR.Standalone.Location.cMPDiscountSubID), DisplayName = "Discount Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? CMPDiscountSubID
  {
    get => this._CMPDiscountSubID;
    set => this._CMPDiscountSubID = value;
  }

  [SubAccount]
  public virtual int? CMPGainLossSubID
  {
    get => this._CMPGainLossSubID;
    set => this._CMPGainLossSubID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.cMPSiteID))]
  [PXUIField(DisplayName = "Warehouse")]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  public virtual int? CMPSiteID
  {
    get => this._CMPSiteID;
    set => this._CMPSiteID = value;
  }

  [PXDBCreatedByID(BqlField = typeof (PX.Objects.CR.Standalone.Location.createdByID))]
  public virtual Guid? LocationCreatedByID
  {
    get => this._LocationCreatedByID;
    set => this._LocationCreatedByID = value;
  }

  [PXDBCreatedByScreenID(BqlField = typeof (PX.Objects.CR.Standalone.Location.createdByScreenID))]
  public virtual string LocationCreatedByScreenID
  {
    get => this._LocationCreatedByScreenID;
    set => this._LocationCreatedByScreenID = value;
  }

  [PXDBCreatedDateTime(BqlField = typeof (PX.Objects.CR.Standalone.Location.createdDateTime))]
  public virtual DateTime? LocationCreatedDateTime
  {
    get => this._LocationCreatedDateTime;
    set => this._LocationCreatedDateTime = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.CR.Standalone.Location.lastModifiedByID))]
  public virtual Guid? LocationLastModifiedByID
  {
    get => this._LocationLastModifiedByID;
    set => this._LocationLastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.CR.Standalone.Location.lastModifiedByScreenID))]
  public virtual string LocationLastModifiedByScreenID
  {
    get => this._LocationLastModifiedByScreenID;
    set => this._LocationLastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.CR.Standalone.Location.lastModifiedDateTime))]
  public virtual DateTime? LocationLastModifiedDateTime
  {
    get => this._LocationLastModifiedDateTime;
    set => this._LocationLastModifiedDateTime = value;
  }

  [PXBool]
  [PXFormula(typeof (Where<LocationExtAddress.defAddressID, IsNotNull, And<LocationExtAddress.defAddressID, Equal<Current<BAccount.defAddressID>>>>))]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsAddressSameAsMain
  {
    get => this._IsAddressSameAsMain;
    set => this._IsAddressSameAsMain = value;
  }

  [PXBool]
  [PXFormula(typeof (Where<LocationExtAddress.defContactID, IsNotNull, And<LocationExtAddress.defContactID, Equal<Current<BAccount.defContactID>>>>))]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsContactSameAsMain
  {
    get => this._IsContactSameAsMain;
    set => this._IsContactSameAsMain = value;
  }

  [PXExtraKey]
  [PXDBInt]
  [PXDBDefault(typeof (BAccount.bAccountID))]
  [PXUIField]
  public override int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXExtraKey]
  [PXDBInt]
  [PXUIField]
  public override int? AddressID
  {
    get => this._AddressID;
    set => this._AddressID = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (LocationExtAddress.countryID))]
  public override string State
  {
    get => this._State;
    set => this._State = value;
  }

  [PXUniqueNote]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public new class PK : PrimaryKeyOf<LocationExtAddress>.By<LocationExtAddress.locationID>
  {
    public static LocationExtAddress Find(PXGraph graph, int? locationID, PKFindOptions options = 0)
    {
      return (LocationExtAddress) PrimaryKeyOf<LocationExtAddress>.By<LocationExtAddress.locationID>.FindBy(graph, (object) locationID, options);
    }
  }

  public abstract class locationBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.locationBAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.locationID>
  {
  }

  public abstract class locationCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.locationCD>
  {
  }

  public abstract class locType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.locType>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.descr>
  {
  }

  public abstract class taxRegistrationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.taxRegistrationID>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.defAddressID>
  {
  }

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.defContactID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationExtAddress.isActive>
  {
  }

  public abstract class cTaxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.cTaxZoneID>
  {
  }

  public abstract class cTaxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.cTaxCalcMode>
  {
  }

  public abstract class cAvalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.cAvalaraCustomerUsageType>
  {
  }

  public abstract class cCarrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.cCarrierID>
  {
  }

  public abstract class cShipTermsID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.cShipTermsID>
  {
  }

  public abstract class cShipZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.cShipZoneID>
  {
  }

  public abstract class cFOBPointID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.cFOBPointID>
  {
  }

  public abstract class cResedential : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationExtAddress.cResedential>
  {
  }

  public abstract class cAdditionalHandling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.cAdditionalHandling>
  {
  }

  public abstract class cLiftGate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationExtAddress.cLiftGate>
  {
  }

  public abstract class cInsideDelivery : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.cInsideDelivery>
  {
  }

  public abstract class cLimitedAccess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.cLimitedAccess>
  {
  }

  public abstract class cSaturdayDelivery : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.cSaturdayDelivery>
  {
  }

  public abstract class cGroundCollect : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.cGroundCollect>
  {
  }

  public abstract class cInsurance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationExtAddress.cInsurance>
  {
  }

  public abstract class cLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  LocationExtAddress.cLeadTime>
  {
  }

  public abstract class cBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cBranchID>
  {
  }

  public abstract class cSalesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cSalesAcctID>
  {
  }

  public abstract class cSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cSalesSubID>
  {
  }

  public abstract class cPriceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.cPriceClassID>
  {
  }

  public abstract class cSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cSiteID>
  {
  }

  public abstract class cDiscountAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cDiscountAcctID>
  {
  }

  public abstract class cDiscountSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cDiscountSubID>
  {
  }

  public abstract class cRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cRetainageAcctID>
  {
  }

  public abstract class cRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cRetainageSubID>
  {
  }

  public abstract class cFreightAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cFreightAcctID>
  {
  }

  public abstract class cFreightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cFreightSubID>
  {
  }

  public abstract class cShipComplete : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.cShipComplete>
  {
  }

  public abstract class cOrderPriority : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationExtAddress.cOrderPriority>
  {
  }

  public abstract class cARAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cARAccountLocationID>
  {
  }

  public abstract class cARAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cARAccountID>
  {
  }

  public abstract class cARSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cARSubID>
  {
  }

  public abstract class isARAccountSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.isARAccountSameAsMain>
  {
  }

  public abstract class vTaxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.vTaxZoneID>
  {
  }

  public abstract class vTaxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.vTaxCalcMode>
  {
  }

  public abstract class vCarrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.vCarrierID>
  {
  }

  public abstract class vShipTermsID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.vShipTermsID>
  {
  }

  public abstract class vFOBPointID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.vFOBPointID>
  {
  }

  public abstract class vLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  LocationExtAddress.vLeadTime>
  {
  }

  public abstract class vBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vBranchID>
  {
  }

  public abstract class vExpenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vExpenseAcctID>
  {
  }

  public abstract class vExpenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vExpenseSubID>
  {
  }

  public abstract class vRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vRetainageAcctID>
  {
  }

  public abstract class vRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vRetainageSubID>
  {
  }

  public abstract class vFreightAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vFreightAcctID>
  {
  }

  public abstract class vFreightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vFreightSubID>
  {
  }

  public abstract class vDiscountAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vDiscountAcctID>
  {
  }

  public abstract class vDiscountSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vDiscountSubID>
  {
  }

  public abstract class vRcptQtyMin : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationExtAddress.vRcptQtyMin>
  {
  }

  public abstract class vRcptQtyMax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationExtAddress.vRcptQtyMax>
  {
  }

  public abstract class vRcptQtyThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationExtAddress.vRcptQtyThreshold>
  {
  }

  public abstract class vRcptQtyAction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.vRcptQtyAction>
  {
  }

  public abstract class vSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vSiteID>
  {
  }

  public abstract class vPrintOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationExtAddress.vPrintOrder>
  {
  }

  public abstract class vEmailOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LocationExtAddress.vEmailOrder>
  {
  }

  public abstract class vAPAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vAPAccountLocationID>
  {
  }

  public abstract class vAPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vAPAccountID>
  {
  }

  public abstract class vAPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vAPSubID>
  {
  }

  public abstract class vPaymentInfoLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vPaymentInfoLocationID>
  {
  }

  public abstract class vRemitAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vRemitAddressID>
  {
  }

  public abstract class vRemitContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vRemitContactID>
  {
  }

  public abstract class vPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.vPaymentMethodID>
  {
  }

  public abstract class vCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vCashAccountID>
  {
  }

  public abstract class vPaymentLeadTime : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationExtAddress.vPaymentLeadTime>
  {
  }

  public abstract class vPaymentByType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.vPaymentByType>
  {
  }

  public abstract class vSeparateCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.vSeparateCheck>
  {
  }

  public abstract class vPrepaymentPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationExtAddress.vPrepaymentPct>
  {
  }

  public abstract class vAllowAPBillBeforeReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.vAllowAPBillBeforeReceipt>
  {
  }

  public abstract class isAPAccountSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.isAPAccountSameAsMain>
  {
  }

  public abstract class bAccountBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.bAccountBAccountID>
  {
  }

  public abstract class vDefAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vDefAddressID>
  {
  }

  public abstract class vDefContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.vDefContactID>
  {
  }

  public abstract class cMPSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cMPSalesSubID>
  {
  }

  public abstract class cMPExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cMPExpenseSubID>
  {
  }

  public abstract class cMPFreightSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cMPFreightSubID>
  {
  }

  public abstract class cMPDiscountSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cMPDiscountSubID>
  {
  }

  public abstract class cMPGainLossSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationExtAddress.cMPGainLossSubID>
  {
  }

  public abstract class cMPSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.cMPSiteID>
  {
  }

  public abstract class locationCreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocationExtAddress.locationCreatedByID>
  {
  }

  public abstract class locationCreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.locationCreatedByScreenID>
  {
  }

  public abstract class locationCreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LocationExtAddress.locationCreatedDateTime>
  {
  }

  public abstract class locationLastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocationExtAddress.locationLastModifiedByID>
  {
  }

  public abstract class locationLastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.locationLastModifiedByScreenID>
  {
  }

  public abstract class locationLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LocationExtAddress.locationLastModifiedDateTime>
  {
  }

  public abstract class isAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.isAddressSameAsMain>
  {
  }

  public abstract class isContactSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationExtAddress.isContactSameAsMain>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.bAccountID>
  {
  }

  public new abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationExtAddress.addressID>
  {
  }

  public new abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationExtAddress.state>
  {
  }

  public new abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationExtAddress.postalCode>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LocationExtAddress.noteID>
  {
  }
}
