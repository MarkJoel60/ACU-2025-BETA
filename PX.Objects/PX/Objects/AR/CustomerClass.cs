// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.GL;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Customer Class")]
[PXPrimaryGraph(typeof (CustomerClassMaint))]
[Serializable]
public class CustomerClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CustomerClassID;
  protected string _Descr;
  protected string _TermsID;
  protected string _TaxZoneID;
  protected bool? _RequireTaxZone;
  protected string _AvalaraCustomerUsageType;
  protected bool? _RequireAvalaraCustomerUsageType;
  protected string _PriceClassID;
  protected string _CuryID;
  protected string _CuryRateTypeID;
  protected bool? _AllowOverrideCury;
  protected bool? _AllowOverrideRate;
  protected int? _ARAcctID;
  protected int? _ARSubID;
  protected int? _DiscountAcctID;
  protected int? _DiscountSubID;
  protected int? _DiscTakenAcctID;
  protected int? _DiscTakenSubID;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected int? _COGSAcctID;
  protected int? _COGSSubID;
  protected int? _FreightAcctID;
  protected int? _FreightSubID;
  protected int? _MiscAcctID;
  protected int? _MiscSubID;
  protected int? _PrepaymentAcctID;
  protected int? _PrepaymentSubID;
  protected int? _UnrealizedGainAcctID;
  protected int? _UnrealizedGainSubID;
  protected int? _UnrealizedLossAcctID;
  protected int? _UnrealizedLossSubID;
  protected bool? _AutoApplyPayments;
  protected bool? _PrintStatements;
  protected bool? _PrintCuryStatements;
  protected string _CreditRule;
  protected Decimal? _CreditLimit;
  protected short? _CreditDaysPastDue;
  protected string _StatementType;
  protected string _StatementCycleId;
  protected bool? _SmallBalanceAllow;
  protected Decimal? _SmallBalanceLimit;
  protected bool? _FinChargeApply;
  protected string _FinChargeID;
  protected string _CountryID;
  protected Decimal? _OverLimitAmount;
  protected string _DefPaymentMethodID;
  protected bool? _PrintInvoices;
  protected bool? _MailInvoices;
  protected bool? _PrintDunningLetters;
  protected bool? _MailDunningLetters;
  protected bool? _DefaultLocationCDFromBranch;
  protected string _ShipVia;
  protected string _ShipComplete;
  protected string _ShipTermsID;
  protected int? _SalesPersonID;
  protected Decimal? _DiscountLimit;
  protected byte[] _GroupMask;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (CustomerClass.customerClassID), CacheGlobal = true)]
  [PXFieldDescription]
  [PXReferentialIntegrityCheck]
  public virtual string CustomerClassID
  {
    get => this._CustomerClassID;
    set => this._CustomerClassID = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDefault(0)]
  [RestrictOrganization]
  [PXUIField(DisplayName = "Restrict Visibility To", FieldClass = "VisibilityRestriction")]
  public int? OrgBAccountID { get; set; }

  [PXDefault(typeof (Search2<CustomerClass.termsID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), CacheGlobal = true)]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.taxZoneID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Tax Zone ID")]
  [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), CacheGlobal = true, DescriptionField = typeof (PX.Objects.TX.TaxZone.descr))]
  [PXForeignReference(typeof (Field<CustomerClass.taxZoneID>.IsRelatedTo<PX.Objects.TX.TaxZone.taxZoneID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.requireTaxZone, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
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

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type", Required = true)]
  [PXDefault("0")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType
  {
    get => this._AvalaraCustomerUsageType;
    set => this._AvalaraCustomerUsageType = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.requireAvalaraCustomerUsageType, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Require Tax Exemption Type")]
  public virtual bool? RequireAvalaraCustomerUsageType
  {
    get => this._RequireAvalaraCustomerUsageType;
    set => this._RequireAvalaraCustomerUsageType = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.priceClassID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Price Class")]
  [PXSelector(typeof (Search<ARPriceClass.priceClassID>), CacheGlobal = true, DescriptionField = typeof (ARPriceClass.description))]
  public virtual string PriceClassID
  {
    get => this._PriceClassID;
    set => this._PriceClassID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.curyID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXDefault(typeof (Coalesce<Search2<CustomerClass.curyRateTypeID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>, Search<CMSetup.aRRateTypeDflt>>))]
  [PXForeignReference(typeof (Field<CustomerClass.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Currency Rate Type")]
  public virtual string CuryRateTypeID
  {
    get => this._CuryRateTypeID;
    set => this._CuryRateTypeID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Enable Currency Override")]
  [PXDefault(false, typeof (Coalesce<Search2<CustomerClass.allowOverrideCury, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>, Search<CMSetup.aRCuryOverride>>))]
  public virtual bool? AllowOverrideCury
  {
    get => this._AllowOverrideCury;
    set => this._AllowOverrideCury = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Enable Rate Override")]
  [PXDefault(false, typeof (Coalesce<Search2<CustomerClass.allowOverrideRate, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>, Search<CMSetup.aRRateTypeOverride>>))]
  public virtual bool? AllowOverrideRate
  {
    get => this._AllowOverrideRate;
    set => this._AllowOverrideRate = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.aRAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<CustomerClass.aRAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? ARAcctID
  {
    get => this._ARAcctID;
    set => this._ARAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.aRSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.aRAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.aRSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? ARSubID
  {
    get => this._ARSubID;
    set => this._ARSubID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.discountAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<CustomerClass.discountAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? DiscountAcctID
  {
    get => this._DiscountAcctID;
    set => this._DiscountAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.discountSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.discountAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.discountSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? DiscountSubID
  {
    get => this._DiscountSubID;
    set => this._DiscountSubID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.discTakenAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<CustomerClass.discTakenAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? DiscTakenAcctID
  {
    get => this._DiscTakenAcctID;
    set => this._DiscTakenAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.discTakenSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.discTakenAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.discTakenSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? DiscTakenSubID
  {
    get => this._DiscTakenSubID;
    set => this._DiscTakenSubID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.salesAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<CustomerClass.salesAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.salesSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.salesAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.salesSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.cOGSAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  public virtual int? COGSAcctID
  {
    get => this._COGSAcctID;
    set => this._COGSAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.cOGSSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.cOGSAcctID))]
  public virtual int? COGSSubID
  {
    get => this._COGSSubID;
    set => this._COGSSubID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.freightAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<CustomerClass.freightAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? FreightAcctID
  {
    get => this._FreightAcctID;
    set => this._FreightAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.freightSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.freightAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.freightSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? FreightSubID
  {
    get => this._FreightSubID;
    set => this._FreightSubID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.miscAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  public virtual int? MiscAcctID
  {
    get => this._MiscAcctID;
    set => this._MiscAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.miscSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.miscAcctID))]
  public virtual int? MiscSubID
  {
    get => this._MiscSubID;
    set => this._MiscSubID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.prepaymentAcctID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<CustomerClass.prepaymentAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? PrepaymentAcctID
  {
    get => this._PrepaymentAcctID;
    set => this._PrepaymentAcctID = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.prepaymentSubID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [SubAccount(typeof (CustomerClass.prepaymentAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.prepaymentSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? PrepaymentSubID
  {
    get => this._PrepaymentSubID;
    set => this._PrepaymentSubID = value;
  }

  [Account(null)]
  [PXForeignReference(typeof (Field<CustomerClass.unrealizedGainAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? UnrealizedGainAcctID
  {
    get => this._UnrealizedGainAcctID;
    set => this._UnrealizedGainAcctID = value;
  }

  [SubAccount(typeof (CustomerClass.unrealizedGainAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.unrealizedGainSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? UnrealizedGainSubID
  {
    get => this._UnrealizedGainSubID;
    set => this._UnrealizedGainSubID = value;
  }

  [Account(null)]
  [PXForeignReference(typeof (Field<CustomerClass.unrealizedLossAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? UnrealizedLossAcctID
  {
    get => this._UnrealizedLossAcctID;
    set => this._UnrealizedLossAcctID = value;
  }

  [SubAccount(typeof (CustomerClass.unrealizedLossAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.unrealizedLossSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? UnrealizedLossSubID
  {
    get => this._UnrealizedLossSubID;
    set => this._UnrealizedLossSubID = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.autoApplyPayments, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Auto-Apply Payments")]
  public virtual bool? AutoApplyPayments
  {
    get => this._AutoApplyPayments;
    set => this._AutoApplyPayments = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.printStatements, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Print Statements")]
  public virtual bool? PrintStatements
  {
    get => this._PrintStatements;
    set => this._PrintStatements = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.printCuryStatements, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Multi-Currency Statements")]
  public virtual bool? PrintCuryStatements
  {
    get => this._PrintCuryStatements;
    set => this._PrintCuryStatements = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.sendStatementByEmail, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Send Statements by Email")]
  public virtual bool? SendStatementByEmail { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D", typeof (Search2<CustomerClass.creditRule, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PX.Objects.AR.CreditRule]
  [PXUIField(DisplayName = "Credit Verification")]
  public virtual string CreditRule
  {
    get => this._CreditRule;
    set => this._CreditRule = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<CustomerClass.creditLimit, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Credit Limit")]
  public virtual Decimal? CreditLimit
  {
    get => this._CreditLimit;
    set => this._CreditLimit = value;
  }

  [PXDefault(0, typeof (Search2<CustomerClass.creditDaysPastDue, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXDBShort(MinValue = 0, MaxValue = 3650)]
  [PXUIField(DisplayName = "Credit Days Past Due")]
  public virtual short? CreditDaysPastDue
  {
    get => this._CreditDaysPastDue;
    set => this._CreditDaysPastDue = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("O", typeof (Search2<CustomerClass.statementType, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [LabelList(typeof (ARStatementType))]
  [PXUIField(DisplayName = "Statement Type")]
  public virtual string StatementType
  {
    get => this._StatementType;
    set => this._StatementType = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search2<CustomerClass.statementCycleId, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Statement Cycle ID")]
  [PXSelector(typeof (ARStatementCycle.statementCycleId), DescriptionField = typeof (ARStatementCycle.descr))]
  public virtual string StatementCycleId
  {
    get => this._StatementCycleId;
    set => this._StatementCycleId = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.smallBalanceAllow, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Enable Write-Offs")]
  public virtual bool? SmallBalanceAllow
  {
    get => this._SmallBalanceAllow;
    set => this._SmallBalanceAllow = value;
  }

  [PXDBCury(typeof (CustomerClass.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<CustomerClass.smallBalanceLimit, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Write-Off Limit")]
  public virtual Decimal? SmallBalanceLimit
  {
    get => this._SmallBalanceLimit;
    set => this._SmallBalanceLimit = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.finChargeApply, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Apply Overdue Charges")]
  public virtual bool? FinChargeApply
  {
    get => this._FinChargeApply;
    set => this._FinChargeApply = value;
  }

  [PXDefault(typeof (Search2<CustomerClass.finChargeID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Overdue Charge ID")]
  [PXSelector(typeof (ARFinCharge.finChargeID), DescriptionField = typeof (ARFinCharge.finChargeDesc))]
  public virtual string FinChargeID
  {
    get => this._FinChargeID;
    set => this._FinChargeID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(100)]
  [PXUIField(DisplayName = "Country")]
  [Country]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<CustomerClass.overLimitAmount, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Over-Limit Amount")]
  public virtual Decimal? OverLimitAmount
  {
    get => this._OverLimitAmount;
    set => this._OverLimitAmount = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method")]
  [PXDefault(typeof (Search2<CustomerClass.defPaymentMethodID, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr), CacheGlobal = true)]
  public virtual string DefPaymentMethodID
  {
    get => this._DefPaymentMethodID;
    set => this._DefPaymentMethodID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [SavePaymentProfileCode.List]
  [PXUIField(DisplayName = "Save Payment Profiles")]
  public virtual string SavePaymentProfiles { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.printInvoices, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Print Invoices")]
  public virtual bool? PrintInvoices
  {
    get => this._PrintInvoices;
    set => this._PrintInvoices = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search2<CustomerClass.mailInvoices, InnerJoin<ARSetup, On<CustomerClass.customerClassID, Equal<ARSetup.dfltCustomerClassID>>>>))]
  [PXUIField(DisplayName = "Send Invoices by Email")]
  public virtual bool? MailInvoices
  {
    get => this._MailInvoices;
    set => this._MailInvoices = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Print Dunning Letters")]
  [PXDefault(true)]
  public virtual bool? PrintDunningLetters
  {
    get => this._PrintDunningLetters;
    set => this._PrintDunningLetters = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Send Dunning Letters by Email")]
  [PXDefault(false)]
  public virtual bool? MailDunningLetters
  {
    get => this._MailDunningLetters;
    set => this._MailDunningLetters = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default Location ID from Branch")]
  public virtual bool? DefaultLocationCDFromBranch
  {
    get => this._DefaultLocationCDFromBranch;
    set => this._DefaultLocationCDFromBranch = value;
  }

  [PXUIField(DisplayName = "Ship Via")]
  [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("L")]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Terms")]
  [PXSelector(typeof (PX.Objects.CS.ShipTerms.shipTermsID), DescriptionField = typeof (PX.Objects.CS.ShipTerms.description), CacheGlobal = true)]
  public virtual string ShipTermsID
  {
    get => this._ShipTermsID;
    set => this._ShipTermsID = value;
  }

  [SalesPerson]
  [PXForeignReference(typeof (Field<CustomerClass.salesPersonID>.IsRelatedTo<SalesPerson.salesPersonID>))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDBDecimal(MaxValue = 100.0, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "50.0")]
  [PXUIField(DisplayName = "Group/Document Discount Limit (%)")]
  public virtual Decimal? DiscountLimit
  {
    get => this._DiscountLimit;
    set => this._DiscountLimit = value;
  }

  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<True>>>), DescriptionField = typeof (Locale.translatedName))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Locale")]
  public virtual string LocaleName { get; set; }

  [PXNote(DescriptionField = typeof (CustomerClass.customerClassID))]
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

  [Account]
  [PXForeignReference(typeof (Field<CustomerClass.retainageAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RetainageAcctID { get; set; }

  [SubAccount(typeof (CustomerClass.retainageAcctID))]
  [PXForeignReference(typeof (Field<CustomerClass.retainageSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RetainageSubID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Apply Retainage", FieldClass = "Retainage")]
  [PXDefault(false)]
  public virtual bool? RetainageApply { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  public class PK : PrimaryKeyOf<CustomerClass>.By<CustomerClass.customerClassID>
  {
    public static CustomerClass Find(PXGraph graph, string customerClassID, PKFindOptions options = 0)
    {
      return (CustomerClass) PrimaryKeyOf<CustomerClass>.By<CustomerClass.customerClassID>.FindBy(graph, (object) customerClassID, options);
    }
  }

  public static class FK
  {
    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.termsID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.taxZoneID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.curyID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.curyRateTypeID>
    {
    }

    public class ARAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.aRAcctID>
    {
    }

    public class ARSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.aRSubID>
    {
    }

    public class DiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.discountAcctID>
    {
    }

    public class DiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.discountSubID>
    {
    }

    public class CashDiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.discTakenAcctID>
    {
    }

    public class CashDiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.discTakenSubID>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.salesSubID>
    {
    }

    public class COGSAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.cOGSAcctID>
    {
    }

    public class COGSSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.cOGSSubID>
    {
    }

    public class FreightAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.freightAcctID>
    {
    }

    public class FreightSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.freightSubID>
    {
    }

    public class MiscAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.miscAcctID>
    {
    }

    public class MiscSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.miscSubID>
    {
    }

    public class PrepaymentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.prepaymentAcctID>
    {
    }

    public class PrepaymentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.prepaymentSubID>
    {
    }

    public class UnrealizedGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.unrealizedGainAcctID>
    {
    }

    public class UnrealizedGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.unrealizedGainSubID>
    {
    }

    public class UnrealizedLossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.unrealizedLossAcctID>
    {
    }

    public class UnrealizedLossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.unrealizedLossSubID>
    {
    }

    public class RetainageAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.retainageAcctID>
    {
    }

    public class RetainageSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.retainageSubID>
    {
    }

    public class DefaultPaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.defPaymentMethodID>
    {
    }

    public class StatementCycle : 
      PrimaryKeyOf<ARStatementCycle>.By<ARStatementCycle.statementCycleId>.ForeignKeyOf<CustomerClass>.By<CustomerClass.statementCycleId>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.countryID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<SalesPerson>.By<SalesPerson.salesPersonID>.ForeignKeyOf<CustomerClass>.By<CustomerClass.salesPersonID>
    {
    }
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.customerClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.descr>
  {
  }

  public abstract class orgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.orgBAccountID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.termsID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.taxZoneID>
  {
  }

  public abstract class requireTaxZone : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerClass.requireTaxZone>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.taxCalcMode>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.avalaraCustomerUsageType>
  {
  }

  public abstract class requireAvalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.requireAvalaraCustomerUsageType>
  {
  }

  public abstract class priceClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.priceClassID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.curyID>
  {
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.curyRateTypeID>
  {
  }

  public abstract class allowOverrideCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.allowOverrideCury>
  {
  }

  public abstract class allowOverrideRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.allowOverrideRate>
  {
  }

  public abstract class aRAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.aRAcctID>
  {
  }

  public abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.aRSubID>
  {
  }

  public abstract class discountAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.discountAcctID>
  {
  }

  public abstract class discountSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.discountSubID>
  {
  }

  public abstract class discTakenAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.discTakenAcctID>
  {
  }

  public abstract class discTakenSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.discTakenSubID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.salesSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.cOGSAcctID>
  {
  }

  public abstract class cOGSSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.cOGSSubID>
  {
  }

  public abstract class freightAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.freightAcctID>
  {
  }

  public abstract class freightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.freightSubID>
  {
  }

  public abstract class miscAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.miscAcctID>
  {
  }

  public abstract class miscSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.miscSubID>
  {
  }

  public abstract class prepaymentAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerClass.prepaymentAcctID>
  {
  }

  public abstract class prepaymentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.prepaymentSubID>
  {
  }

  public abstract class unrealizedGainAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerClass.unrealizedGainAcctID>
  {
  }

  public abstract class unrealizedGainSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerClass.unrealizedGainSubID>
  {
  }

  public abstract class unrealizedLossAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerClass.unrealizedLossAcctID>
  {
  }

  public abstract class unrealizedLossSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerClass.unrealizedLossSubID>
  {
  }

  public abstract class autoApplyPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.autoApplyPayments>
  {
  }

  public abstract class printStatements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.printStatements>
  {
  }

  public abstract class printCuryStatements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.printCuryStatements>
  {
  }

  public abstract class sendStatementByEmail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.sendStatementByEmail>
  {
  }

  public abstract class creditRule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.creditRule>
  {
  }

  public abstract class creditLimit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CustomerClass.creditLimit>
  {
  }

  public abstract class creditDaysPastDue : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CustomerClass.creditDaysPastDue>
  {
  }

  public abstract class statementType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.statementType>
  {
  }

  public abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.statementCycleId>
  {
  }

  public abstract class smallBalanceAllow : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.smallBalanceAllow>
  {
  }

  public abstract class smallBalanceLimit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CustomerClass.smallBalanceLimit>
  {
  }

  public abstract class finChargeApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerClass.finChargeApply>
  {
  }

  public abstract class finChargeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.finChargeID>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.countryID>
  {
  }

  public abstract class overLimitAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CustomerClass.overLimitAmount>
  {
  }

  public abstract class defPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.defPaymentMethodID>
  {
  }

  public abstract class savePaymentProfiles : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.savePaymentProfiles>
  {
  }

  public abstract class printInvoices : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerClass.printInvoices>
  {
  }

  public abstract class mailInvoices : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerClass.mailInvoices>
  {
  }

  public abstract class printDunningLetters : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.printDunningLetters>
  {
  }

  public abstract class mailDunningLetters : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.mailDunningLetters>
  {
  }

  public abstract class defaultLocationCDFromBranch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.defaultLocationCDFromBranch>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.shipVia>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.shipComplete>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.shipTermsID>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.salesPersonID>
  {
  }

  public abstract class discountLimit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CustomerClass.discountLimit>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerClass.localeName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CustomerClass.noteID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CustomerClass.groupMask>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CustomerClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CustomerClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CustomerClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerClass.lastModifiedDateTime>
  {
  }

  public abstract class retainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.retainageAcctID>
  {
  }

  public abstract class retainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerClass.retainageSubID>
  {
  }

  public abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomerClass.retainageApply>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerClass.paymentsByLinesAllowed>
  {
  }
}
