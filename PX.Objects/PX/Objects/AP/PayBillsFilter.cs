// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PayBillsFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class PayBillsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _PayTypeID;
  protected int? _PayAccountID;
  protected System.DateTime? _PayDate;
  protected string _PayFinPeriodID;
  protected short? _PayInLessThan;
  protected bool? _ShowPayInLessThan;
  protected short? _DueInLessThan;
  protected bool? _ShowDueInLessThan;
  protected short? _DiscountExpiresInLessThan;
  protected bool? _ShowDiscountExpiresInLessThan;
  protected Decimal? _Balance;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected string _Days;
  protected Decimal? _CurySelTotal;
  protected Decimal? _SelTotal;
  protected string _VendorClassID;
  protected bool? _TakeDiscAlways;
  protected Decimal? _CashBalance;
  protected Decimal? _GLBalance;

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.externalPaymentProcessor>, PX.Data.Or<Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.externalPaymentProcessor>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>>>>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
  public virtual string PayTypeID
  {
    get => this._PayTypeID;
    set => this._PayTypeID = value;
  }

  [CashAccount(typeof (PayBillsFilter.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<PayBillsFilter.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>), Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (Search2<PaymentMethodAccount.cashAccountID, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<PayBillsFilter.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.aPIsDefault, Equal<True>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<AccessInfo.branchID>>>>>>>))]
  public virtual int? PayAccountID
  {
    get => this._PayAccountID;
    set => this._PayAccountID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Payment Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? PayDate
  {
    get => this._PayDate;
    set => this._PayDate = value;
  }

  [APOpenPeriod(typeof (PayBillsFilter.payDate), typeof (PayBillsFilter.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, IsFilterMode = true)]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.Visible)]
  public virtual string PayFinPeriodID
  {
    get => this._PayFinPeriodID;
    set => this._PayFinPeriodID = value;
  }

  [PXDBShort]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (IIf<Where<PayBillsFilter.showPayInLessThan, Equal<True>>, Current<APSetup.paymentLeadTime>, short0>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? PayInLessThan
  {
    get => this._PayInLessThan;
    set => this._PayInLessThan = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Quick Batch Generation", Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (Search<PaymentMethodAccount.aPQuickBatchGeneration, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<PayBillsFilter.payTypeID>>, And<PaymentMethodAccount.cashAccountID, Equal<Current<PayBillsFilter.payAccountID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? APQuickBatchGeneration { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Pay Date Within", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowPayInLessThan
  {
    get => this._ShowPayInLessThan;
    set => this._ShowPayInLessThan = value;
  }

  [PXDBShort]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (IIf<Where<PayBillsFilter.showDueInLessThan, Equal<True>>, Current<APSetup.paymentLeadTime>, short0>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? DueInLessThan
  {
    get => this._DueInLessThan;
    set => this._DueInLessThan = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Due Date Within", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowDueInLessThan
  {
    get => this._ShowDueInLessThan;
    set => this._ShowDueInLessThan = value;
  }

  [PXDBShort]
  [PXUIField(Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (IIf<Where<PayBillsFilter.showDiscountExpiresInLessThan, Equal<True>>, Current<APSetup.paymentLeadTime>, short0>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual short? DiscountExpiresInLessThan
  {
    get => this._DiscountExpiresInLessThan;
    set => this._DiscountExpiresInLessThan = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cash Discount Expires Within", Visibility = PXUIVisibility.Visible)]
  public virtual bool? ShowDiscountExpiresInLessThan
  {
    get => this._ShowDiscountExpiresInLessThan;
    set => this._ShowDiscountExpiresInLessThan = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PayBillsFilter.curyID))]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Decimal? Balance
  {
    get => this._Balance;
    set => this._Balance = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [PXDefault(typeof (Search<PX.Objects.CA.CashAccount.curyID, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<PayBillsFilter.payAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField]
  [PXDefault("Days", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string Days
  {
    get => this._Days;
    set => this._Days = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (PayBillsFilter.curyInfoID), typeof (PayBillsFilter.selTotal), BaseCalc = false)]
  [PXUIField(DisplayName = "Selection Total", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual Decimal? CurySelTotal
  {
    get => this._CurySelTotal;
    set => this._CurySelTotal = value;
  }

  [PXDBDecimal(4)]
  public virtual Decimal? SelTotal
  {
    get => this._SelTotal;
    set => this._SelTotal = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Number of Rows Selected", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual int? SelCount { get; set; }

  [VendorActive(Visibility = PXUIVisibility.SelectorVisible, Required = false, DescriptionField = typeof (Vendor.acctName))]
  [PXDefault]
  public virtual int? VendorID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (VendorClass.vendorClassID), DescriptionField = typeof (VendorClass.descr))]
  [PXUIField(DisplayName = "Vendor Class", Required = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [APActiveProject]
  public virtual int? ProjectID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Always Take Cash Discount", Visibility = PXUIVisibility.Visible)]
  public virtual bool? TakeDiscAlways
  {
    get => this._TakeDiscAlways;
    set => this._TakeDiscAlways = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PayBillsFilter.curyID))]
  [PXUIField(DisplayName = "Available Balance", Enabled = false)]
  [PX.Objects.CA.CashBalance(typeof (PayBillsFilter.payAccountID))]
  public virtual Decimal? CashBalance
  {
    get => this._CashBalance;
    set => this._CashBalance = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PayBillsFilter.curyID))]
  [PXUIField(DisplayName = "GL Balance", Enabled = false)]
  [PX.Objects.CA.GLBalance(typeof (PayBillsFilter.payAccountID), typeof (PayBillsFilter.payFinPeriodID))]
  public virtual Decimal? GLBalance
  {
    get => this._GLBalance;
    set => this._GLBalance = value;
  }

  [APDocType.List]
  [PXDBString(3, IsFixed = true)]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string RefNbr { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PayBillsFilter.branchID>
  {
  }

  public abstract class payTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayBillsFilter.payTypeID>
  {
  }

  public abstract class payAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PayBillsFilter.payAccountID>
  {
  }

  public abstract class payDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  PayBillsFilter.payDate>
  {
  }

  public abstract class payFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PayBillsFilter.payFinPeriodID>
  {
  }

  public abstract class payInLessThan : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  PayBillsFilter.payInLessThan>
  {
  }

  public abstract class aPQuickBatchGeneration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PayBillsFilter.aPQuickBatchGeneration>
  {
  }

  public abstract class showPayInLessThan : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PayBillsFilter.showPayInLessThan>
  {
  }

  public abstract class dueInLessThan : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  PayBillsFilter.dueInLessThan>
  {
  }

  public abstract class showDueInLessThan : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PayBillsFilter.showDueInLessThan>
  {
  }

  public abstract class discountExpiresInLessThan : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    PayBillsFilter.discountExpiresInLessThan>
  {
  }

  public abstract class showDiscountExpiresInLessThan : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PayBillsFilter.showDiscountExpiresInLessThan>
  {
  }

  public abstract class balance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PayBillsFilter.balance>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayBillsFilter.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PayBillsFilter.curyInfoID>
  {
  }

  public abstract class days : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayBillsFilter.days>
  {
  }

  public abstract class curySelTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PayBillsFilter.curySelTotal>
  {
  }

  public abstract class selTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PayBillsFilter.selTotal>
  {
  }

  public abstract class selCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PayBillsFilter.selCount>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PayBillsFilter.vendorID>
  {
  }

  public abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PayBillsFilter.vendorClassID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PayBillsFilter.projectID>
  {
  }

  public abstract class takeDiscAlways : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PayBillsFilter.takeDiscAlways>
  {
  }

  public abstract class cashBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PayBillsFilter.cashBalance>
  {
  }

  public abstract class gLBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PayBillsFilter.gLBalance>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayBillsFilter.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PayBillsFilter.refNbr>
  {
  }
}
