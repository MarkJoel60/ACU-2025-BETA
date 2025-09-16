// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Vendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.SM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// AP-specific business account data related to vendors, including default currency settings,
/// credit terms, and tax reporting settings for tax agency vendors.
/// Vendors are edited on the Vendors (AP303000) form, which corresponds to the <see cref="T:PX.Objects.AP.VendorMaint" /> graph.
/// </summary>
[PXTable(new System.Type[] {typeof (PX.Objects.CR.BAccount.bAccountID)})]
[PXPrimaryGraph(new System.Type[] {typeof (VendorMaint), typeof (EmployeeMaint)}, new System.Type[] {typeof (Select<Vendor, Where2<Where<Vendor.type, Equal<BAccountType.vendorType>, Or<Vendor.type, Equal<BAccountType.combinedType>>>, And<Vendor.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>>), typeof (Select<PX.Objects.EP.EPEmployee, Where<Vendor.type, Equal<BAccountType.employeeType>, And<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>>)})]
[PXODataDocumentTypesRestriction(typeof (VendorMaint))]
[PXCacheName("Vendor", PXDacType.Catalogue, CacheGlobal = true)]
[Serializable]
public class Vendor : PX.Objects.CR.BAccount, IIncludable, IRestricted
{
  protected 
  #nullable disable
  string _VendorClassID;
  protected string _TermsID;
  protected int? _DefPOAddressID;
  protected string _PriceListCuryID;
  protected string _DefaultUOM;
  protected int? _DiscTakenAcctID;
  protected int? _DiscTakenSubID;
  protected int? _PrepaymentAcctID;
  protected int? _PrepaymentSubID;
  protected int? _POAccrualAcctID;
  protected int? _POAccrualSubID;
  protected int? _PrebookAcctID;
  protected int? _PrebookSubID;
  protected int? _BaseRemitContactID;
  protected bool? _Vendor1099;
  protected short? _Box1099;
  protected bool? _TaxAgency;
  protected bool? _UpdClosedTaxPeriods;
  protected short? _TaxReportPrecision;
  protected string _TaxReportRounding;
  protected bool? _TaxUseVendorCurPrecision;
  protected bool? _TaxReportFinPeriod;
  protected string _TaxPeriodType;
  protected int? _SalesTaxAcctID;
  protected int? _SalesTaxSubID;
  protected int? _PurchTaxAcctID;
  protected int? _PurchTaxSubID;
  protected int? _TaxExpenseAcctID;
  protected int? _TaxExpenseSubID;
  protected new byte[] _GroupMask;
  protected bool? _LandedCostVendor;
  protected bool? _Included;
  protected string _LineDiscountTarget;
  protected bool? _IgnoreConfiguredDiscounts;

  [VendorRaw(IsKey = true)]
  [PXDefault]
  [PXFieldDescription]
  [PXPersonalDataWarning]
  public override string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXFieldDescription]
  [PXPersonalDataField]
  public override string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("VE")]
  [PXUIField(DisplayName = "Type")]
  [BAccountType.List]
  public override string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<APSetup.dfltVendorClassID>))]
  [PXSelector(typeof (Search2<VendorClass.vendorClassID, LeftJoin<PX.Objects.EP.Standalone.EPEmployeeClass, On<PX.Objects.EP.Standalone.EPEmployeeClass.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<PX.Objects.EP.Standalone.EPEmployeeClass.vendorClassID, PX.Data.IsNull, PX.Data.And<MatchUser>>>), DescriptionField = typeof (VendorClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Vendor Class")]
  public virtual string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), CacheGlobal = true)]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.termsID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Terms")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<Vendor.termsID>.IsRelatedTo<PX.Objects.CS.Terms.termsID>))]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  public virtual int? DefPOAddressID
  {
    get => this._DefPOAddressID;
    set => this._DefPOAddressID = value;
  }

  [CRAttributesField(typeof (Vendor.vendorClassID), typeof (PX.Objects.CR.BAccount.noteID), new System.Type[] {typeof (PX.Objects.CR.BAccount.classID), typeof (PX.Objects.AR.Customer.customerClassID)})]
  public override string[] Attributes { get; set; }

  [PXDBString(5, IsUnicode = true, BqlTable = typeof (Vendor))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<PX.Data.True>>>), CacheGlobal = true)]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.curyID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Currency ID")]
  public override string CuryID { get; set; }

  [PXDBString(6, IsUnicode = true, BqlTable = typeof (Vendor))]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.curyRateTypeID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<Vendor.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type")]
  public override string CuryRateTypeID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.curyID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string PriceListCuryID
  {
    get => this._PriceListCuryID;
    set => this._PriceListCuryID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Default UOM")]
  public virtual string DefaultUOM
  {
    get => this._DefaultUOM;
    set => this._DefaultUOM = value;
  }

  [PXDBBool(BqlTable = typeof (Vendor))]
  [PXUIField(DisplayName = "Enable Currency Override")]
  [PXDefault(false, typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.allowOverrideCury))]
  public override bool? AllowOverrideCury { get; set; }

  [PXDBBool(BqlTable = typeof (Vendor))]
  [PXUIField(DisplayName = "Enable Rate Override")]
  [PXDefault(false, typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.allowOverrideRate))]
  public override bool? AllowOverrideRate { get; set; }

  [Account(DisplayName = "Cash Discount Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.discTakenAcctID))]
  [PXForeignReference(typeof (Vendor.FK.CashDiscountAccount))]
  public virtual int? DiscTakenAcctID
  {
    get => this._DiscTakenAcctID;
    set => this._DiscTakenAcctID = value;
  }

  [SubAccount(typeof (Vendor.discTakenAcctID), DisplayName = "Cash Discount Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.discTakenSubID))]
  [PXReferentialIntegrityCheck(CheckPoint = CheckPoint.BeforePersisting)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<Vendor.discTakenSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? DiscTakenSubID
  {
    get => this._DiscTakenSubID;
    set => this._DiscTakenSubID = value;
  }

  [Account(DisplayName = "Prepayment Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "AP")]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.prepaymentAcctID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXForeignReference(typeof (Vendor.FK.PrepaymentAccount))]
  public virtual int? PrepaymentAcctID
  {
    get => this._PrepaymentAcctID;
    set => this._PrepaymentAcctID = value;
  }

  [SubAccount(typeof (Vendor.prepaymentAcctID), DisplayName = "Prepayment Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.prepaymentSubID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXReferentialIntegrityCheck(CheckPoint = CheckPoint.BeforePersisting)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<Vendor.prepaymentSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? PrepaymentSubID
  {
    get => this._PrepaymentSubID;
    set => this._PrepaymentSubID = value;
  }

  [Account(DisplayName = "PO Accrual Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "PO")]
  [PXDefault(typeof (Search<VendorClass.pOAccrualAcctID, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXForeignReference(typeof (Vendor.FK.POAccrualAccount))]
  public virtual int? POAccrualAcctID
  {
    get => this._POAccrualAcctID;
    set => this._POAccrualAcctID = value;
  }

  [SubAccount(typeof (Vendor.pOAccrualAcctID), DisplayName = "PO Accrual Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault(typeof (Search<VendorClass.pOAccrualSubID, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXReferentialIntegrityCheck(CheckPoint = CheckPoint.BeforePersisting)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<Vendor.pOAccrualSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? POAccrualSubID
  {
    get => this._POAccrualSubID;
    set => this._POAccrualSubID = value;
  }

  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.prebookAcctID), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Reclassification Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (Vendor.FK.ReclassificationAccount))]
  public virtual int? PrebookAcctID
  {
    get => this._PrebookAcctID;
    set => this._PrebookAcctID = value;
  }

  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.prebookSubID), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (Vendor.prebookAcctID), DisplayName = "Reclassification Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXReferentialIntegrityCheck(CheckPoint = CheckPoint.BeforePersisting)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<Vendor.prebookSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? PrebookSubID
  {
    get => this._PrebookSubID;
    set => this._PrebookSubID = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXUIField(DisplayName = "Default Contact", Visibility = PXUIVisibility.Invisible)]
  [PXSelector(typeof (Search<PX.Objects.CR.Contact.contactID>), DirtyRead = true)]
  public virtual int? BaseRemitContactID
  {
    get => this._BaseRemitContactID;
    set => this._BaseRemitContactID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Vendor Status")]
  [PXDefault("A")]
  [VendorStatus.List]
  public override string VStatus { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "1099 Vendor")]
  [PXDefault(false)]
  public virtual bool? Vendor1099
  {
    get => this._Vendor1099;
    set => this._Vendor1099 = value;
  }

  [PXDBShort]
  [Box1099NumberSelector]
  [PXUIField(DisplayName = "1099 Box", Visibility = PXUIVisibility.Visible)]
  public virtual short? Box1099
  {
    get => this._Box1099;
    set => this._Box1099 = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "FATCA")]
  [PXUIEnabled(typeof (Vendor.vendor1099))]
  public virtual bool? FATCA { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Vendor Is Tax Agency")]
  [PXDefault(false)]
  public virtual bool? TaxAgency
  {
    get => this._TaxAgency;
    set => this._TaxAgency = value;
  }

  [RestrictOrganization]
  [PXUIField(DisplayName = "Restrict Visibility To", FieldClass = "VisibilityRestriction", Required = false)]
  [PXDefault(0, typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (CustomerClass.orgBAccountID))]
  public override int? VOrgBAccountID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Type of TIN", FieldClass = "Reporting1099")]
  [Vendor.tinType.List]
  [PXDefault("E", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIVisible(typeof (Where<BqlOperand<Vendor.vendor1099, IBqlBool>.IsEqual<PX.Data.True>>))]
  public string TinType { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Update Closed Tax Periods")]
  [PXDefault(false)]
  public virtual bool? UpdClosedTaxPeriods
  {
    get => this._UpdClosedTaxPeriods;
    set => this._UpdClosedTaxPeriods = value;
  }

  [PXDBShort(MaxValue = 9, MinValue = 0)]
  [PXDefault(2, typeof (Search<PX.Objects.CM.Currency.decimalPlaces, Where<PX.Objects.CM.Currency.curyID, Equal<Current<Vendor.curyID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Tax Report Precision")]
  public virtual short? TaxReportPrecision
  {
    get => this._TaxReportPrecision;
    set => this._TaxReportPrecision = value;
  }

  [PXDBString(1)]
  [PXDefault("R")]
  [PXUIField(DisplayName = "Tax Report Rounding")]
  [PXStringList(new string[] {"R", "C", "F"}, new string[] {"Mathematical", "Ceiling", "Floor"})]
  public virtual string TaxReportRounding
  {
    get => this._TaxReportRounding;
    set => this._TaxReportRounding = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Use Currency Precision")]
  [PXDefault(false)]
  public virtual bool? TaxUseVendorCurPrecision
  {
    get => this._TaxUseVendorCurPrecision;
    set => this._TaxUseVendorCurPrecision = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Define Tax Period by End Date of Financial Period")]
  [PXDefault(false)]
  public virtual bool? TaxReportFinPeriod
  {
    get => this._TaxReportFinPeriod;
    set => this._TaxReportFinPeriod = value;
  }

  [PXDBString(1)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Default Tax Period Type")]
  [VendorTaxPeriodType.List]
  public virtual string TaxPeriodType
  {
    get => this._TaxPeriodType;
    set => this._TaxPeriodType = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Automatically Generate Tax Bill")]
  [PXDefault(true)]
  public virtual bool? AutoGenerateTaxBill { get; set; }

  [Account(DisplayName = "Tax Payable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  public virtual int? SalesTaxAcctID
  {
    get => this._SalesTaxAcctID;
    set => this._SalesTaxAcctID = value;
  }

  [SubAccount(typeof (Vendor.salesTaxAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Tax Payable Sub.")]
  public virtual int? SalesTaxSubID
  {
    get => this._SalesTaxSubID;
    set => this._SalesTaxSubID = value;
  }

  [Account(DisplayName = "Tax Claimable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "TX")]
  public virtual int? PurchTaxAcctID
  {
    get => this._PurchTaxAcctID;
    set => this._PurchTaxAcctID = value;
  }

  [SubAccount(typeof (Vendor.purchTaxAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Tax Claimable Sub.")]
  public virtual int? PurchTaxSubID
  {
    get => this._PurchTaxSubID;
    set => this._PurchTaxSubID = value;
  }

  [Account(DisplayName = "Tax Expense Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  public virtual int? TaxExpenseAcctID
  {
    get => this._TaxExpenseAcctID;
    set => this._TaxExpenseAcctID = value;
  }

  [SubAccount(typeof (Vendor.taxExpenseAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Tax Expense Sub.")]
  public virtual int? TaxExpenseSubID
  {
    get => this._TaxExpenseSubID;
    set => this._TaxExpenseSubID = value;
  }

  [PXDBGroupMask(BqlTable = typeof (Vendor))]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.groupMask), PersistingCheck = PXPersistingCheck.Nothing)]
  public new virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [Owner(typeof (PX.Objects.CR.BAccount.workgroupID), Visibility = PXUIVisibility.Invisible)]
  public override int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXSearchable(1411, "Vendor: {0}", new System.Type[] {typeof (Vendor.acctName)}, new System.Type[] {typeof (Vendor.acctName), typeof (Vendor.acctCD), typeof (Vendor.acctName), typeof (Vendor.acctCD), typeof (Vendor.defContactID), typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.eMail), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Contact.phone2), typeof (PX.Objects.CR.Contact.phone3), typeof (PX.Objects.CR.Contact.webSite), typeof (Vendor.defAddressID), typeof (PX.Objects.CR.Address.displayName)}, NumberFields = new System.Type[] {typeof (Vendor.acctCD)}, Line1Format = "{0}{2}{3}", Line1Fields = new System.Type[] {typeof (Vendor.acctCD), typeof (Vendor.defContactID), typeof (PX.Objects.CR.Contact.eMail), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Contact.phone2), typeof (PX.Objects.CR.Contact.phone3)}, Line2Format = "{1}{2}{3}", Line2Fields = new System.Type[] {typeof (Vendor.defAddressID), typeof (PX.Objects.CR.Address.displayName), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.state)}, SelectForFastIndexing = typeof (Select2<Vendor, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<Vendor.defContactID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<Vendor.defAddressID>>>>>), WhereConstraint = typeof (Where<BqlOperand<Vendor.type, IBqlString>.IsIn<BAccountType.vendorType, BAccountType.combinedType>>))]
  [PXUniqueNote(DescriptionField = typeof (Vendor.acctCD), Selector = typeof (VendorR.acctCD), ActivitiesCountByParent = true, ShowInReferenceSelector = true, PopupTextEnabled = true)]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Landed Cost Vendor")]
  [PXDefault(false)]
  public virtual bool? LandedCostVendor
  {
    get => this._LandedCostVendor;
    set => this._LandedCostVendor = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  [PXDBString(1, IsFixed = true)]
  [LineDiscountTargetType.List]
  [PXDefault("E")]
  [PXUIField(DisplayName = "Apply Line Discounts to", Visibility = PXUIVisibility.Visible, Required = true)]
  public virtual string LineDiscountTarget
  {
    get => this._LineDiscountTarget;
    set => this._LineDiscountTarget = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Ignore Configured Discounts When Vendor Price Is Defined")]
  [PXDefault(false)]
  public virtual bool? IgnoreConfiguredDiscounts
  {
    get => this._IgnoreConfiguredDiscounts;
    set => this._IgnoreConfiguredDiscounts = value;
  }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Foreign Entity")]
  public virtual bool? ForeignEntity { get; set; }

  [Obsolete("This field should not be used explicitly over Vendor dac. Use the VendorClassID or BAccountClassID field instead.")]
  public override string ClassID => this.VendorClassID;

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.BAccount.classID" /> field, which is used for internal purposes.
  /// </summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.classID))]
  [PXUIField(DisplayName = "Business Account Class", Visible = false, FieldClass = "CRM")]
  [PXDefault(typeof (CRSetup.defaultCustomerClassID), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string BAccountClassID { get; set; }

  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<PX.Data.True>>>), DescriptionField = typeof (Locale.translatedName))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Language/Locale")]
  [PXDefault(typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.localeName), PersistingCheck = PXPersistingCheck.Nothing)]
  public override string LocaleName { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [SVATTaxReversalMethods.List]
  [PXUIField(DisplayName = "VAT Recognition Method")]
  public virtual string SVATReversalMethod { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [VendorSVATTaxEntryRefNbr.InputList]
  [PXUIField(DisplayName = "Input Tax Entry Ref. Nbr.")]
  public virtual string SVATInputTaxEntryRefNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [VendorSVATTaxEntryRefNbr.OutputList]
  [PXUIField(DisplayName = "Output Tax Entry Ref. Nbr.")]
  public virtual string SVATOutputTaxEntryRefNbr { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Tax Invoice Numbering")]
  public virtual string SVATTaxInvoiceNumberingID { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor, whom the AP bill will belong to.
  /// </value>
  [PayToVendor(CacheGlobal = true, Filterable = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<Vendor.payToVendorID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? PayToVendorID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Apply Retainage", FieldClass = "Retainage")]
  [PXDefault(false, typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.retainageApply), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? RetainageApply { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Retainage Percent", Visibility = PXUIVisibility.Visible, FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (Default<Vendor.retainageApply>))]
  public virtual Decimal? RetainagePct { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Pay by Line", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
  [PXDefault(false, typeof (Select<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), SourceField = typeof (VendorClass.paymentsByLinesAllowed), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  public new class PK : PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>
  {
    public static Vendor Find(PXGraph graph, int? bAccountID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<Vendor>.By<Vendor.acctCD>
  {
    public static Vendor Find(PXGraph graph, string acctCD, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Vendor>.By<Vendor.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class VendorClass : 
      PrimaryKeyOf<VendorClass>.By<VendorClass.vendorClassID>.ForeignKeyOf<Vendor>.By<Vendor.vendorClassID>
    {
    }

    public class ParentBusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<BAccountR.bAccountID>.ForeignKeyOf<Vendor>.By<PX.Objects.CR.BAccount.parentBAccountID>
    {
    }

    public class DefaultPOAddress : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<Vendor>.By<Vendor.defPOAddressID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<Vendor>.By<Vendor.termsID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Vendor>.By<Vendor.curyID>
    {
    }

    public class PriceListCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Vendor>.By<Vendor.priceListCuryID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<Vendor>.By<Vendor.curyRateTypeID>
    {
    }

    public class CashDiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Vendor>.By<Vendor.discTakenAcctID>
    {
    }

    public class CashDiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Vendor>.By<Vendor.discTakenSubID>
    {
    }

    public class PrepaymentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Vendor>.By<Vendor.prepaymentAcctID>
    {
    }

    public class PrepaymentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Vendor>.By<Vendor.prepaymentSubID>
    {
    }

    public class POAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Vendor>.By<Vendor.pOAccrualAcctID>
    {
    }

    public class POAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Vendor>.By<Vendor.pOAccrualSubID>
    {
    }

    public class ReclassificationAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Vendor>.By<Vendor.prebookAcctID>
    {
    }

    public class ReclassificationSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Vendor>.By<Vendor.prebookSubID>
    {
    }

    public class TaxPayableAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Vendor>.By<Vendor.salesTaxAcctID>
    {
    }

    public class TaxPayableSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Vendor>.By<Vendor.salesTaxSubID>
    {
    }

    public class TaxClaimableAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Vendor>.By<Vendor.purchTaxAcctID>
    {
    }

    public class TaxClaimableSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Vendor>.By<Vendor.purchTaxSubID>
    {
    }

    public class TaxExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Vendor>.By<Vendor.taxExpenseAcctID>
    {
    }

    public class TaxExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Vendor>.By<Vendor.taxExpenseSubID>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<Vendor>.By<Vendor.defAddressID>
    {
    }

    public class ContactInfo : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<Vendor>.By<Vendor.defContactID>
    {
    }

    public class DefaultLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<Vendor>.By<Vendor.bAccountID, Vendor.defLocationID>
    {
    }

    public class PrimaryContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<Vendor>.By<PX.Objects.CR.BAccount.primaryContactID>
    {
    }

    public class PayToVendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<Vendor>.By<Vendor.payToVendorID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.acctName>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.type>
  {
  }

  public abstract class vendorClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.vendorClassID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.termsID>
  {
  }

  public abstract class defPOAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.defPOAddressID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.curyID>
  {
  }

  public new abstract class curyRateTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.curyRateTypeID>
  {
  }

  public abstract class priceListCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.priceListCuryID>
  {
  }

  public abstract class defaultUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.defaultUOM>
  {
  }

  public new abstract class allowOverrideCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Vendor.allowOverrideCury>
  {
  }

  public new abstract class allowOverrideRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Vendor.allowOverrideRate>
  {
  }

  public abstract class discTakenAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.discTakenAcctID>
  {
  }

  public abstract class discTakenSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.discTakenSubID>
  {
  }

  public abstract class prepaymentAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.prepaymentAcctID>
  {
  }

  public abstract class prepaymentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.prepaymentSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.pOAccrualSubID>
  {
  }

  public abstract class prebookAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.prebookAcctID>
  {
  }

  public abstract class prebookSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.prebookSubID>
  {
  }

  public abstract class baseRemitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.baseRemitContactID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.defLocationID>
  {
  }

  public new abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.defAddressID>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.defContactID>
  {
  }

  public new abstract class vStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.vStatus>
  {
  }

  public abstract class vendor1099 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.vendor1099>
  {
  }

  public abstract class box1099 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Vendor.box1099>
  {
  }

  public abstract class fATCA : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.fATCA>
  {
  }

  public abstract class taxAgency : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.taxAgency>
  {
  }

  public new abstract class vOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.vOrgBAccountID>
  {
  }

  public new abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.baseCuryID>
  {
  }

  public abstract class tinType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.tinType>
  {
    public const string EIN = "E";
    public const string SSN = "S";
    public const string ITIN = "I";
    public const string ATIN = "A";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[5]{ null, "E", "S", "I", "A" }, new string[5]
        {
          string.Empty,
          "EIN",
          "SSN",
          "ITIN",
          "ATIN"
        })
      {
      }
    }

    public class eIN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Vendor.tinType.eIN>
    {
      public eIN()
        : base("E")
      {
      }
    }

    public class sSN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Vendor.tinType.sSN>
    {
      public sSN()
        : base("S")
      {
      }
    }

    public class iTIN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Vendor.tinType.iTIN>
    {
      public iTIN()
        : base("I")
      {
      }
    }

    public class aTIN : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    Vendor.tinType.aTIN>
    {
      public aTIN()
        : base("A")
      {
      }
    }
  }

  public abstract class updClosedTaxPeriods : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Vendor.updClosedTaxPeriods>
  {
  }

  public abstract class taxReportPrecision : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    Vendor.taxReportPrecision>
  {
  }

  public abstract class taxReportRounding : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Vendor.taxReportRounding>
  {
  }

  public abstract class taxUseVendorCurPrecision : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Vendor.taxUseVendorCurPrecision>
  {
  }

  public abstract class taxReportFinPeriod : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.taxReportFinPeriod>
  {
  }

  public abstract class taxPeriodType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.taxPeriodType>
  {
  }

  public abstract class autoGenerateTaxBill : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Vendor.autoGenerateTaxBill>
  {
  }

  public abstract class salesTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.salesTaxAcctID>
  {
  }

  public abstract class salesTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.salesTaxSubID>
  {
  }

  public abstract class purchTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.purchTaxAcctID>
  {
  }

  public abstract class purchTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.purchTaxSubID>
  {
  }

  public abstract class taxExpenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.taxExpenseAcctID>
  {
  }

  public abstract class taxExpenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.taxExpenseSubID>
  {
  }

  public new abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Vendor.groupMask>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.ownerID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Vendor.noteID>
  {
  }

  public abstract class landedCostVendor : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.landedCostVendor>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.included>
  {
  }

  public abstract class lineDiscountTarget : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Vendor.lineDiscountTarget>
  {
  }

  public abstract class ignoreConfiguredDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Vendor.ignoreConfiguredDiscounts>
  {
  }

  public abstract class foreignEntity : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.foreignEntity>
  {
  }

  public abstract class bAccountClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.bAccountClassID>
  {
  }

  public new abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Vendor.localeName>
  {
  }

  public abstract class sVATReversalMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Vendor.sVATReversalMethod>
  {
  }

  public abstract class sVATInputTaxEntryRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Vendor.sVATInputTaxEntryRefNbr>
  {
  }

  public abstract class sVATOutputTaxEntryRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Vendor.sVATOutputTaxEntryRefNbr>
  {
  }

  public abstract class sVATTaxInvoiceNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Vendor.sVATTaxInvoiceNumberingID>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Vendor.payToVendorID>
  {
  }

  public abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Vendor.retainageApply>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Vendor.retainagePct>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Vendor.paymentsByLinesAllowed>
  {
  }
}
