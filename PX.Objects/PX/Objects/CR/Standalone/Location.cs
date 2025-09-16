// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Standalone.Location
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CR.Standalone;

/// <inheritdoc cref="T:PX.Objects.CR.Location" />
[PXPrimaryGraph(new System.Type[] {typeof (VendorLocationMaint), typeof (CustomerLocationMaint), typeof (BranchMaint), typeof (EmployeeMaint), typeof (AccountLocationMaint)}, new System.Type[] {typeof (Select<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<Location.locationID>>, And<Where<Current<Location.locType>, Equal<LocTypeList.vendorLoc>, Or<Current<Location.locType>, Equal<LocTypeList.combinedLoc>>>>>>>), typeof (Select<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<Location.locationID>>, And<Where<Current<Location.locType>, Equal<LocTypeList.customerLoc>, Or<Current<Location.locType>, Equal<LocTypeList.combinedLoc>>>>>>>), typeof (Select2<PX.Objects.GL.Branch, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<Location.locationID>>, And<Current<Location.locType>, Equal<LocTypeList.companyLoc>>>>>), typeof (Select2<PX.Objects.EP.EPEmployee, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.EP.EPEmployee.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.EP.EPEmployee.defLocationID>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<Location.locationID>>, And<Current<Location.locType>, Equal<LocTypeList.employeeLoc>>>>>), typeof (Select<Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<Location.locationID>>, And<Where<Current<Location.locType>, Equal<LocTypeList.companyLoc>, Or<Current<Location.locType>, Equal<LocTypeList.combinedLoc>>>>>>>)})]
[Serializable]
public class Location : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ILocation
{
  protected bool? _IsRemitAddressSameAsMain;
  protected bool? _IsRemitContactSameAsMain;

  /// <inheritdoc cref="P:PX.Objects.CR.Location.LocationID" />
  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? LocationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.BAccountID" />
  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? BAccountID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.LocationCD" />
  [PXDBString(IsKey = true, IsUnicode = true)]
  [PXDimension("INLOCATION")]
  public virtual 
  #nullable disable
  string LocationCD { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.LocType" />
  [PXDBString(2, IsFixed = true)]
  [LocTypeList.List]
  [PXUIField(DisplayName = "Location Type")]
  [PXDefault]
  public virtual string LocType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.Descr" />
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Location Name")]
  public virtual string Descr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.TaxRegistrationID" />
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Registration ID")]
  [PXPersonalDataField]
  [PXDeduplicationSearchField(false, true)]
  public virtual string TaxRegistrationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.DefAddressID" />
  [PXDBInt]
  [PXForeignReference(typeof (Field<Location.defAddressID>.IsRelatedTo<PX.Objects.CR.Address.addressID>))]
  [PXUIField(DisplayName = "Default Address")]
  public virtual int? DefAddressID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.OverrideAddress" />
  [PXBool]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideAddress { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsAddressSameAsMain" />
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

  /// <inheritdoc cref="P:PX.Objects.CR.Location.DefContactID" />
  [PXDBInt]
  [PXForeignReference(typeof (Field<Location.defContactID>.IsRelatedTo<PX.Objects.CR.Contact.contactID>))]
  [PXUIField(DisplayName = "Default Contact")]
  public virtual int? DefContactID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.OverrideContact" />
  [PXBool]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideContact { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsContactSameAsMain" />
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

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsActive" />
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.Status" />
  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Status")]
  [PX.Objects.CR.LocationStatus.List]
  public virtual string Status { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsDefault" />
  [PXBool]
  [PXUIField(DisplayName = "Default", Enabled = false)]
  public virtual bool? IsDefault { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CTaxZoneID" />
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXDefault]
  public virtual string CTaxZoneID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CTaxCalcMode" />
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<CustomerClass.taxCalcMode, Where<CustomerClass.customerClassID, Equal<Current<CustomerClass.customerClassID>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string CTaxCalcMode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CAvalaraExemptionNumber" />
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string CAvalaraExemptionNumber { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CAvalaraCustomerUsageType" />
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [PXDefault("0")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string CAvalaraCustomerUsageType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CCarrierID" />
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Ship Via")]
  public virtual string CCarrierID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CShipTermsID" />
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Shipping Terms")]
  public virtual string CShipTermsID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CShipZoneID" />
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Zone")]
  public virtual string CShipZoneID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CFOBPointID" />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  public virtual string CFOBPointID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CResedential" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Residential Delivery")]
  public virtual bool? CResedential { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CSaturdayDelivery" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday Delivery")]
  public virtual bool? CSaturdayDelivery { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CAdditionalHandling" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Additional Handling")]
  public bool? CAdditionalHandling { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CLiftGate" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Lift Gate")]
  public bool? CLiftGate { get; set; }

  /// <inheritdoc cref="T:PX.Objects.CR.Location.cInsideDelivery" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Inside Delivery")]
  public bool? CInsideDelivery { get; set; }

  /// <inheritdoc cref="T:PX.Objects.CR.Location.cLimitedAccess" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Limited Access")]
  public bool? CLimitedAccess { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CGroundCollect" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ground Collect")]
  public virtual bool? CGroundCollect { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CInsurance" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Insurance")]
  public virtual bool? CInsurance { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CLeadTime" />
  [PXDBShort(MinValue = 0, MaxValue = 100000)]
  [PXUIField(DisplayName = "Lead Time (Days)")]
  public virtual short? CLeadTime { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CBranchID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Shipping Branch")]
  public virtual int? CBranchID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CSalesAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Sales Account")]
  public virtual int? CSalesAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CSalesSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Sales Sub.")]
  public virtual int? CSalesSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CPriceClassID" />
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Price Class")]
  public virtual string CPriceClassID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CSiteID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Warehouse")]
  [PXForeignReference(typeof (Field<Location.cSiteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? CSiteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CDiscountAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Discount Account")]
  public virtual int? CDiscountAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CDiscountSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Discount Sub.")]
  public virtual int? CDiscountSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CRetainageAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Retainage Receivable Account")]
  public virtual int? CRetainageAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CRetainageSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Retainage Receivable Sub.")]
  public virtual int? CRetainageSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CFreightAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Freight Account")]
  public virtual int? CFreightAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CFreightSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Freight Sub.")]
  public virtual int? CFreightSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CShipComplete" />
  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string CShipComplete { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.COrderPriority" />
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Order Priority")]
  public virtual short? COrderPriority { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CCalendarID" />
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Calendar")]
  public virtual string CCalendarID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CDefProjectID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Default Project")]
  public virtual int? CDefProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CARAccountLocationID" />
  [PXDBInt]
  [PXDefault]
  public virtual int? CARAccountLocationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CARAccountID" />
  [PXDBInt]
  [PXUIField(DisplayName = "AR Account")]
  public virtual int? CARAccountID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CARSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "AR Sub.")]
  public virtual int? CARSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsARAccountSameAsMain" />
  [PXBool]
  [PXUIField(DisplayName = "Same As Default Location's")]
  [PXFormula(typeof (Switch<Case<Where<Location.locationID, Equal<Location.cARAccountLocationID>>, False>, True>))]
  public virtual bool? IsARAccountSameAsMain { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VTaxZoneID" />
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXDefault]
  public virtual string VTaxZoneID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VTaxCalcMode" />
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<VendorClass.taxCalcMode, Where<VendorClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string VTaxCalcMode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VCarrierID" />
  [PXUIField(DisplayName = "Ship Via")]
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  public virtual string VCarrierID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VShipTermsID" />
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Shipping Terms")]
  public virtual string VShipTermsID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VFOBPointID" />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  public virtual string VFOBPointID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VLeadTime" />
  [PXDBShort(MinValue = 0, MaxValue = 100000)]
  [PXUIField(DisplayName = "Lead Time (Days)")]
  public virtual short? VLeadTime { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VBranchID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Receiving Branch")]
  public virtual int? VBranchID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VExpenseAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Expense Account")]
  public virtual int? VExpenseAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VExpenseSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Expense Sub.")]
  public virtual int? VExpenseSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRetainageAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Retainage Payable Account")]
  public virtual int? VRetainageAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRetainageSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Retainage Payable Sub.")]
  public virtual int? VRetainageSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VFreightAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Freight Account")]
  public virtual int? VFreightAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VFreightSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Freight Sub.")]
  public virtual int? VFreightSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VDiscountAcctID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Discount Account")]
  public virtual int? VDiscountAcctID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VDiscountSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Discount Sub.")]
  public virtual int? VDiscountSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRcptQtyMin" />
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Min. Receipt (%)")]
  public virtual Decimal? VRcptQtyMin { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRcptQtyMax" />
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Max. Receipt (%)")]
  public virtual Decimal? VRcptQtyMax { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRcptQtyThreshold" />
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Threshold Receipt (%)")]
  public virtual Decimal? VRcptQtyThreshold { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRcptQtyAction" />
  [PXDBString(1, IsFixed = true)]
  [PXDefault("W")]
  [POReceiptQtyAction.List]
  [PXUIField(DisplayName = "Receipt Action")]
  public virtual string VRcptQtyAction { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VSiteID" />
  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD), DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (PX.Objects.IN.INSite.siteCD)})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  public virtual int? VSiteID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VSiteIDIsNull" />
  [PXShort]
  [PXDBCalced(typeof (Switch<Case<Where<Location.vSiteID, IsNull>, shortMax>, short0>), typeof (short))]
  public virtual short? VSiteIDIsNull { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VPrintOrder" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print Order")]
  public virtual bool? VPrintOrder { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VEmailOrder" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Email Order")]
  public virtual bool? VEmailOrder { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VDefProjectID" />
  [Obsolete]
  [PXDBInt]
  [PXUIField(DisplayName = "Default Project")]
  public virtual int? VDefProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VAPAccountLocationID" />
  [PXDBInt]
  [PXDefault]
  public virtual int? VAPAccountLocationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VAPAccountID" />
  [PXDBInt]
  [PXUIField(DisplayName = "AP Account")]
  public virtual int? VAPAccountID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VAPSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "AP Sub.")]
  public virtual int? VAPSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VPaymentInfoLocationID" />
  [PXDBInt]
  [PXDefault]
  public virtual int? VPaymentInfoLocationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.OverrideRemitAddress" />
  [PXBool]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideRemitAddress { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsRemitAddressSameAsMain" />
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

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRemitAddressID" />
  [PXDBInt]
  [PXForeignReference(typeof (Field<Location.vRemitAddressID>.IsRelatedTo<PX.Objects.CR.Address.addressID>))]
  public virtual int? VRemitAddressID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.OverrideRemitContact" />
  [PXBool]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideRemitContact { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsRemitContactSameAsMain" />
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

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VRemitContactID" />
  [PXDBInt]
  [PXForeignReference(typeof (Field<Location.vRemitContactID>.IsRelatedTo<PX.Objects.CR.Contact.contactID>))]
  public virtual int? VRemitContactID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VPaymentMethodID" />
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method")]
  [PXDefault]
  public virtual string VPaymentMethodID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VCashAccountID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Cash Account")]
  public virtual int? VCashAccountID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VPaymentLeadTime" />
  [PXDBShort(MinValue = 0, MaxValue = 3660)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Payment Lead Time (Days)")]
  public short? VPaymentLeadTime { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VPaymentByType" />
  [PXDBInt]
  [PXUIField(DisplayName = "Payment By")]
  [PXDefault(0)]
  [APPaymentBy.List]
  public int? VPaymentByType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VSeparateCheck" />
  [PXDBBool]
  [PXUIField(DisplayName = "Pay Separately")]
  [PXDefault(false)]
  public virtual bool? VSeparateCheck { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VPrepaymentPct" />
  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Prepayment Percent")]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public virtual Decimal? VPrepaymentPct { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.VAllowAPBillBeforeReceipt" />
  [PXDBBool]
  [PXUIField(DisplayName = "Allow AP Bill Before Receipt")]
  [PXDefault(false)]
  public virtual bool? VAllowAPBillBeforeReceipt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsAPAccountSameAsMain" />
  [PXBool]
  [PXUIField(DisplayName = "Same As Default Location's")]
  [PXFormula(typeof (Switch<Case<Where<Location.locationID, Equal<Location.vAPAccountLocationID>>, False>, True>))]
  public virtual bool? IsAPAccountSameAsMain { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.IsAPPaymentInfoSameAsMain" />
  [PXBool]
  [PXUIField(DisplayName = "Same As Default Location's")]
  [PXFormula(typeof (Switch<Case<Where<Location.locationID, Equal<Location.vPaymentInfoLocationID>>, False>, True>))]
  public virtual bool? IsAPPaymentInfoSameAsMain { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CMPSalesSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Sales Sub.")]
  public virtual int? CMPSalesSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CMPExpenseSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Expense Sub.")]
  public virtual int? CMPExpenseSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CMPFreightSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Freight Sub.")]
  public virtual int? CMPFreightSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CMPDiscountSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Discount Sub.")]
  public virtual int? CMPDiscountSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CMPGainLossSubID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Currency Gain/Loss Sub.")]
  public virtual int? CMPGainLossSubID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CR.Location.CMPSiteID" />
  [PXDBInt]
  [PXUIField(DisplayName = "Warehouse")]
  public virtual int? CMPSiteID { get; set; }

  /// <summary>The external reference number</summary>
  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXUIField(DisplayName = "External ID", Visible = true)]
  public string ExtRefNbr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<Location>.By<Location.locationID>
  {
    public static Location Find(PXGraph graph, int? locationID, PKFindOptions options = 0)
    {
      return (Location) PrimaryKeyOf<Location>.By<Location.locationID>.FindBy(graph, (object) locationID, options);
    }
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.locationID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.bAccountID>
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

  public abstract class overrideAddress : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.overrideAddress>
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

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Location.defContactID>
  {
  }

  public abstract class overrideContact : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.overrideContact>
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

  public abstract class cSaturdayDelivery : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Location.cSaturdayDelivery>
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

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.Location.ExtRefNbr" />
  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Location.extRefNbr>
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

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Location.Tstamp>
  {
  }
}
