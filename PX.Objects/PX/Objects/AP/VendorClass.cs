// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP.Standalone;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.TX;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("Vendor Class", CacheGlobal = true)]
[PXPrimaryGraph(typeof (VendorClassMaint))]
[Serializable]
public class VendorClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _VendorClassID;
  protected string _Descr;
  protected string _TermsID;
  protected string _PaymentMethodID;
  protected int? _CashAcctID;
  protected int? _PaymentByType;
  protected int? _APAcctID;
  protected int? _APSubID;
  protected int? _DiscTakenAcctID;
  protected int? _DiscTakenSubID;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected int? _DiscountAcctID;
  protected int? _DiscountSubID;
  protected int? _FreightAcctID;
  protected int? _FreightSubID;
  protected int? _PrepaymentAcctID;
  protected int? _PrepaymentSubID;
  protected int? _POAccrualAcctID;
  protected int? _POAccrualSubID;
  protected int? _PrebookAcctID;
  protected int? _PrebookSubID;
  protected int? _UnrealizedGainAcctID;
  protected int? _UnrealizedGainSubID;
  protected int? _UnrealizedLossAcctID;
  protected int? _UnrealizedLossSubID;
  protected string _CuryID;
  protected string _CuryRateTypeID;
  protected bool? _AllowOverrideCury;
  protected bool? _AllowOverrideRate;
  protected string _TaxZoneID;
  protected bool? _RequireTaxZone;
  protected string _CountryID;
  protected string _ShipTermsID;
  protected string _RcptQtyAction;
  protected bool? _PrintPO;
  protected bool? _EmailPO;
  protected bool? _DefaultLocationCDFromBranch;
  protected byte[] _GroupMask;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault(PersistingCheck = PXPersistingCheck.NullOrBlank)]
  [PXUIField(DisplayName = "Class ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search2<VendorClass.vendorClassID, LeftJoin<EPEmployeeClass, On<EPEmployeeClass.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<EPEmployeeClass.vendorClassID, PX.Data.IsNull>>), CacheGlobal = true)]
  [PXFieldDescription]
  public virtual string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDefault(typeof (Search2<VendorClass.termsID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), CacheGlobal = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.termsID>.IsRelatedTo<PX.Objects.CS.Terms.termsID>))]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDefault(0)]
  [RestrictOrganization]
  [PXUIField(DisplayName = "Restrict Visibility To", FieldClass = "VisibilityRestriction")]
  public int? OrgBAccountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search2<VendorClass.paymentMethodID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<PX.Data.True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<PX.Data.True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [CashAccount(typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<VendorClass.paymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<PX.Data.True>>>>>>))]
  public virtual int? CashAcctID
  {
    get => this._CashAcctID;
    set => this._CashAcctID = value;
  }

  [PXDBInt]
  [PXDefault(0, typeof (Search2<VendorClass.paymentByType, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>))]
  [APPaymentBy.List]
  [PXUIField(DisplayName = "Payment By")]
  public virtual int? PaymentByType
  {
    get => this._PaymentByType;
    set => this._PaymentByType = value;
  }

  [PXDefault(typeof (Search2<VendorClass.aPAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "AP Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "AP")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.aPAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? APAcctID
  {
    get => this._APAcctID;
    set => this._APAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.aPSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.aPAcctID), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.aPSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? APSubID
  {
    get => this._APSubID;
    set => this._APSubID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.discTakenAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Cash Discount Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.discTakenAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? DiscTakenAcctID
  {
    get => this._DiscTakenAcctID;
    set => this._DiscTakenAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.discTakenSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.discTakenAcctID), DisplayName = "Cash Discount Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.discTakenSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? DiscTakenSubID
  {
    get => this._DiscTakenSubID;
    set => this._DiscTakenSubID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.expenseAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Expense Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.expenseAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.expenseSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.expenseAcctID), DisplayName = "Expense Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.expenseSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.discountAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Discount Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.discountAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? DiscountAcctID
  {
    get => this._DiscountAcctID;
    set => this._DiscountAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.discountSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.discountAcctID), DisplayName = "Discount Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.discountSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? DiscountSubID
  {
    get => this._DiscountSubID;
    set => this._DiscountSubID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.freightAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Freight Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.freightAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? FreightAcctID
  {
    get => this._FreightAcctID;
    set => this._FreightAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.freightSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.freightAcctID), DisplayName = "Freight Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.freightSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? FreightSubID
  {
    get => this._FreightSubID;
    set => this._FreightSubID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.prepaymentAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Prepayment Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "AP")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.prepaymentAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? PrepaymentAcctID
  {
    get => this._PrepaymentAcctID;
    set => this._PrepaymentAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.prepaymentSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.prepaymentAcctID), DisplayName = "Prepayment Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.prepaymentSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? PrepaymentSubID
  {
    get => this._PrepaymentSubID;
    set => this._PrepaymentSubID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.pOAccrualAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "PO Accrual Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "PO")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.pOAccrualAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? POAccrualAcctID
  {
    get => this._POAccrualAcctID;
    set => this._POAccrualAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.pOAccrualSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.pOAccrualAcctID), DisplayName = "PO Accrual Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.pOAccrualSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? POAccrualSubID
  {
    get => this._POAccrualSubID;
    set => this._POAccrualSubID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.prebookAcctID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Reclassification Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.prebookAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? PrebookAcctID
  {
    get => this._PrebookAcctID;
    set => this._PrebookAcctID = value;
  }

  [PXDefault(typeof (Search2<VendorClass.prebookSubID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (VendorClass.prebookAcctID), DisplayName = "Reclassification Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.prebookSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? PrebookSubID
  {
    get => this._PrebookSubID;
    set => this._PrebookSubID = value;
  }

  [Account(null, DisplayName = "Unrealized Gain Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.unrealizedGainAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? UnrealizedGainAcctID
  {
    get => this._UnrealizedGainAcctID;
    set => this._UnrealizedGainAcctID = value;
  }

  [SubAccount(typeof (VendorClass.unrealizedGainAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Unrealized Gain Sub.", Visibility = PXUIVisibility.Visible)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.unrealizedGainSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? UnrealizedGainSubID
  {
    get => this._UnrealizedGainSubID;
    set => this._UnrealizedGainSubID = value;
  }

  [Account(null, DisplayName = "Unrealized Loss Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.unrealizedLossAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? UnrealizedLossAcctID
  {
    get => this._UnrealizedLossAcctID;
    set => this._UnrealizedLossAcctID = value;
  }

  [SubAccount(typeof (VendorClass.unrealizedLossAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Unrealized Loss Sub.", Visibility = PXUIVisibility.Visible)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.unrealizedLossSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? UnrealizedLossSubID
  {
    get => this._UnrealizedLossSubID;
    set => this._UnrealizedLossSubID = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault(typeof (Search2<VendorClass.curyID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<PX.Data.True>>>), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXDefault(typeof (Search2<PX.Objects.CM.CurrencyRateType.curyRateTypeID, LeftJoin<VendorClass, On<PX.Objects.CM.CurrencyRateType.curyRateTypeID, Equal<VendorClass.curyRateTypeID>>, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>, LeftJoin<CMSetup, On<PX.Objects.CM.CurrencyRateType.curyRateTypeID, Equal<CMSetup.aPRateTypeDflt>>>>>, Where<VendorClass.vendorClassID, NotEqual<Current<VendorClass.vendorClassID>>, Or<Current<VendorClass.vendorClassID>, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type")]
  public virtual string CuryRateTypeID
  {
    get => this._CuryRateTypeID;
    set => this._CuryRateTypeID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Enable Currency Override")]
  [PXDefault(false, typeof (Coalesce<Search<VendorClass.allowOverrideCury, Where<VendorClass.vendorClassID, Equal<Current<APSetup.dfltVendorClassID>>, And<Current<APSetup.dfltVendorClassID>, NotEqual<Current<VendorClass.vendorClassID>>>>>, Search<CMSetup.aPCuryOverride>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? AllowOverrideCury
  {
    get => this._AllowOverrideCury;
    set => this._AllowOverrideCury = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Enable Rate Override")]
  [PXDefault(false, typeof (Coalesce<Search<VendorClass.allowOverrideRate, Where<VendorClass.vendorClassID, Equal<Current<APSetup.dfltVendorClassID>>, And<Current<APSetup.dfltVendorClassID>, NotEqual<Current<VendorClass.vendorClassID>>>>>, Search<CMSetup.aPRateTypeOverride>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? AllowOverrideRate
  {
    get => this._AllowOverrideRate;
    set => this._AllowOverrideRate = value;
  }

  [PXDefault(typeof (Search2<VendorClass.taxZoneID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Tax Zone ID")]
  [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.TX.TaxZone.descr))]
  [PXForeignReference(typeof (VendorClass.FK.TaxZone), ReferenceBehavior.SetNull)]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<VendorClass.requireTaxZone, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Require Tax Zone")]
  public virtual bool? RequireTaxZone
  {
    get => this._RequireTaxZone;
    set => this._RequireTaxZone = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (TaxCalculationMode.taxSetting))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (Search<ShipTerms.shipTermsID>), CacheGlobal = true, DescriptionField = typeof (ShipTerms.description))]
  public virtual string ShipTermsID
  {
    get => this._ShipTermsID;
    set => this._ShipTermsID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("W")]
  [POReceiptQtyAction.List]
  [PXUIField(DisplayName = "Receipt Action")]
  public virtual string RcptQtyAction
  {
    get => this._RcptQtyAction;
    set => this._RcptQtyAction = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Print Orders")]
  public virtual bool? PrintPO
  {
    get => this._PrintPO;
    set => this._PrintPO = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Send Orders by Email")]
  public virtual bool? EmailPO
  {
    get => this._EmailPO;
    set => this._EmailPO = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default Location ID from Branch")]
  public virtual bool? DefaultLocationCDFromBranch
  {
    get => this._DefaultLocationCDFromBranch;
    set => this._DefaultLocationCDFromBranch = value;
  }

  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<PX.Data.True>>>), DescriptionField = typeof (Locale.translatedName))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Locale")]
  public virtual string LocaleName { get; set; }

  [PXNote(DescriptionField = typeof (VendorClass.vendorClassID))]
  public virtual Guid? NoteID { get; set; }

  [SingleGroup]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [Account(DisplayName = "Retainage Payable Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "AP")]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.retainageAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RetainageAcctID { get; set; }

  [SubAccount(typeof (VendorClass.retainageAcctID), DisplayName = "Retainage Payable Sub.", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<VendorClass.retainageSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RetainageSubID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Apply Retainage", FieldClass = "Retainage")]
  [PXDefault(false)]
  public virtual bool? RetainageApply { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Pay by Line", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
  [PXDefault(false)]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  public class PK : PrimaryKeyOf<VendorClass>.By<VendorClass.vendorClassID>
  {
    public static VendorClass Find(PXGraph graph, string vendorClassID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<VendorClass>.By<VendorClass.vendorClassID>.FindBy(graph, (object) vendorClassID, options);
    }
  }

  public static class FK
  {
    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<VendorClass>.By<VendorClass.termsID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<VendorClass>.By<VendorClass.paymentMethodID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<VendorClass>.By<VendorClass.cashAcctID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<VendorClass>.By<VendorClass.taxZoneID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<VendorClass>.By<VendorClass.curyID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<VendorClass>.By<VendorClass.curyRateTypeID>
    {
    }

    public class APAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.aPAcctID>
    {
    }

    public class APSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.aPSubID>
    {
    }

    public class DiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.discountAcctID>
    {
    }

    public class DiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.discountSubID>
    {
    }

    public class CashDiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.discTakenAcctID>
    {
    }

    public class CashDiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.discTakenSubID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.expenseAcctID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.expenseSubID>
    {
    }

    public class FreightAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.freightAcctID>
    {
    }

    public class FreightSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.freightSubID>
    {
    }

    public class PrepaymentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.prepaymentAcctID>
    {
    }

    public class PrepaymentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.prepaymentSubID>
    {
    }

    public class POAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.pOAccrualAcctID>
    {
    }

    public class POAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.pOAccrualSubID>
    {
    }

    public class ReclassificationAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.prebookAcctID>
    {
    }

    public class ReclassificationSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.prebookSubID>
    {
    }

    public class UnrealizedGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.unrealizedGainAcctID>
    {
    }

    public class UnrealizedGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.unrealizedGainSubID>
    {
    }

    public class UnrealizedLossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.unrealizedLossAcctID>
    {
    }

    public class UnrealizedLossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.unrealizedLossSubID>
    {
    }

    public class RetainageAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<VendorClass>.By<VendorClass.retainageAcctID>
    {
    }

    public class RetainageSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<VendorClass>.By<VendorClass.retainageSubID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<VendorClass>.By<VendorClass.countryID>
    {
    }
  }

  public abstract class vendorClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.vendorClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.descr>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.termsID>
  {
  }

  public abstract class orgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.orgBAccountID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorClass.paymentMethodID>
  {
  }

  public abstract class cashAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.cashAcctID>
  {
  }

  public abstract class paymentByType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.paymentByType>
  {
  }

  public abstract class aPAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.aPAcctID>
  {
  }

  public abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.aPSubID>
  {
  }

  public abstract class discTakenAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.discTakenAcctID>
  {
  }

  public abstract class discTakenSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.discTakenSubID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.expenseSubID>
  {
  }

  public abstract class discountAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.discountAcctID>
  {
  }

  public abstract class discountSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.discountSubID>
  {
  }

  public abstract class freightAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.freightAcctID>
  {
  }

  public abstract class freightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.freightSubID>
  {
  }

  public abstract class prepaymentAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.prepaymentAcctID>
  {
  }

  public abstract class prepaymentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.prepaymentSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.pOAccrualSubID>
  {
  }

  public abstract class prebookAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.prebookAcctID>
  {
  }

  public abstract class prebookSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.prebookSubID>
  {
  }

  public abstract class unrealizedGainAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorClass.unrealizedGainAcctID>
  {
  }

  public abstract class unrealizedGainSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorClass.unrealizedGainSubID>
  {
  }

  public abstract class unrealizedLossAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorClass.unrealizedLossAcctID>
  {
  }

  public abstract class unrealizedLossSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorClass.unrealizedLossSubID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.curyID>
  {
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorClass.curyRateTypeID>
  {
  }

  public abstract class allowOverrideCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VendorClass.allowOverrideCury>
  {
  }

  public abstract class allowOverrideRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VendorClass.allowOverrideRate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.taxZoneID>
  {
  }

  public abstract class requireTaxZone : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VendorClass.requireTaxZone>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.taxCalcMode>
  {
    [Obsolete("taxCalcMode.Net is obsolete and will be removed in Acumatica 2018 R1. Please use PX.Objects.TX.TaxCalculationMode.Net instead.")]
    public const string Net = "N";
    [Obsolete("taxCalcMode.Gross is obsolete and will be removed in Acumatica 2018 R1. Please use PX.Objects.TX.TaxCalculationMode.Gross instead.")]
    public const string Gross = "G";
    [Obsolete("taxCalcMode.taxSetting is obsolete and will be removed in Acumatica 2018 R1. Please use PX.Objects.TX.TaxCalculationMode.TaxSetting instead.")]
    public const string TaxSetting = "T";

    [Obsolete("taxCalcMode.List attribute is obsolete and will be removed in Acumatica 2018 R1. Please use PX.Objects.TX.TaxCalculationMode.ListAttribute instead.")]
    public class List : TaxCalculationMode.ListAttribute
    {
    }

    [Obsolete("taxCalcMode.gross is obsolete and will be removed in Acumatica 2018 R1. Please use PX.Objects.TX.TaxCalculationMode.gross instead.")]
    public class gross : TaxCalculationMode.gross
    {
    }

    [Obsolete("taxCalcMode.net is obsolete and will be removed in Acumatica 2018 R1. Please use PX.Objects.TX.TaxCalculationMode.net instead.")]
    public class net : TaxCalculationMode.net
    {
    }

    [Obsolete("taxCalcMode.taxSetting is obsolete and will be removed in Acumatica 2018 R1. Please use PX.Objects.TX.TaxCalculationMode.taxSetting instead.")]
    public class taxSetting : TaxCalculationMode.taxSetting
    {
    }
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.countryID>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.shipTermsID>
  {
  }

  public abstract class rcptQtyAction : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.rcptQtyAction>
  {
  }

  public abstract class printPO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VendorClass.printPO>
  {
  }

  public abstract class emailPO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VendorClass.emailPO>
  {
  }

  public abstract class defaultLocationCDFromBranch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VendorClass.defaultLocationCDFromBranch>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorClass.localeName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  VendorClass.noteID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  VendorClass.groupMask>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  VendorClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  VendorClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    VendorClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    VendorClass.lastModifiedDateTime>
  {
  }

  public abstract class retainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.retainageAcctID>
  {
  }

  public abstract class retainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorClass.retainageSubID>
  {
  }

  public abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  VendorClass.retainageApply>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VendorClass.paymentsByLinesAllowed>
  {
  }
}
