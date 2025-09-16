// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEmployeeClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXPrimaryGraph(typeof (EmployeeClassMaint))]
[PXTable]
[PXCacheName("Employee Class")]
[Serializable]
public class EPEmployeeClass : VendorClass
{
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected 
  #nullable disable
  string _CalendarID;
  protected string _HoursValidation;
  protected string _DefaultDateInActivity;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (EPEmployeeClass.vendorClassID), CacheGlobal = true)]
  public override string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [PXDefault(typeof (Coalesce<Search2<EPEmployeeClass.discTakenAcctID, InnerJoin<APSetup, On<EPEmployeeClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>, Search2<EPVendorClass.discTakenAcctID, InnerJoin<APSetup, On<EPVendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>>))]
  [Account]
  [PXForeignReference(typeof (Field<EPEmployeeClass.discTakenAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public override int? DiscTakenAcctID
  {
    get => this._DiscTakenAcctID;
    set => this._DiscTakenAcctID = value;
  }

  [PXDefault(typeof (Coalesce<Search2<EPEmployeeClass.discTakenSubID, InnerJoin<APSetup, On<EPEmployeeClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>, Search2<EPVendorClass.discTakenSubID, InnerJoin<APSetup, On<EPVendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>>))]
  [SubAccount(typeof (EPEmployeeClass.discTakenAcctID))]
  [PXForeignReference(typeof (Field<EPEmployeeClass.discTakenSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public override int? DiscTakenSubID
  {
    get => this._DiscTakenSubID;
    set => this._DiscTakenSubID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  public override string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [CashAccount(typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<EPEmployeeClass.paymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>))]
  public override int? CashAcctID
  {
    get => this._CashAcctID;
    set => this._CashAcctID = value;
  }

  [Account]
  [PXForeignReference(typeof (Field<EPEmployeeClass.salesAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  [SubAccount(typeof (EPEmployeeClass.salesAcctID))]
  [PXForeignReference(typeof (Field<EPEmployeeClass.salesSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CalendarID
  {
    get => this._CalendarID;
    set => this._CalendarID = value;
  }

  [PXDBString(1)]
  [PXUIField(DisplayName = "Regular Hours Validation")]
  [HoursValidationOption.List]
  [PXDefault("V")]
  public virtual string HoursValidation
  {
    get => this._HoursValidation;
    set => this._HoursValidation = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [APPaymentBy.List]
  [PXUIField(DisplayName = "Payment By")]
  public override int? PaymentByType
  {
    get => this._PaymentByType;
    set => this._PaymentByType = value;
  }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Default Date in Time Cards")]
  [EPEmployeeClass.defaultDateInActivity.List]
  [PXDefault("NW")]
  public virtual string DefaultDateInActivity
  {
    get => this._DefaultDateInActivity;
    set => this._DefaultDateInActivity = value;
  }

  /// <inheritdoc />
  [PXDBString(5, IsUnicode = true)]
  [PXDefault(typeof (Search2<VendorClass.curyID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isFinancial, Equal<True>>>), DescriptionField = typeof (CurrencyList.description), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency")]
  public override string CuryID { get; set; }

  /// <inheritdoc />
  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID), DescriptionField = typeof (PX.Objects.CM.CurrencyRateType.descr))]
  [PXDefault(typeof (Search2<PX.Objects.CM.CurrencyRateType.curyRateTypeID, LeftJoin<VendorClass, On<PX.Objects.CM.CurrencyRateType.curyRateTypeID, Equal<VendorClass.curyRateTypeID>>, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>, LeftJoin<CMSetup, On<PX.Objects.CM.CurrencyRateType.curyRateTypeID, Equal<CMSetup.aPRateTypeDflt>>>>>, Where<VendorClass.vendorClassID, NotEqual<Current<VendorClass.vendorClassID>>, Or<Current<VendorClass.vendorClassID>, Equal<APSetup.dfltVendorClassID>>>>))]
  [PXForeignReference(typeof (Field<EPEmployeeClass.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type")]
  public override string CuryRateTypeID { get; set; }

  /// <inheritdoc />
  [PXDefault(typeof (Search2<VendorClass.taxZoneID, InnerJoin<APSetup, On<VendorClass.vendorClassID, Equal<APSetup.dfltVendorClassID>>>>))]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
  [PXForeignReference]
  public override string TaxZoneID { get; set; }

  /// <summary>The probation period (in months).</summary>
  [PXDBInt(MinValue = 0, MaxValue = 12)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Probation Period (Months)")]
  public virtual int? ProbationPeriodMonths { get; set; }

  [Account]
  [PXForeignReference(typeof (Field<EPEmployeeClass.discountAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public override int? DiscountAcctID { get; set; }

  [SubAccount(typeof (EPEmployeeClass.discountAcctID))]
  [PXForeignReference(typeof (Field<EPEmployeeClass.discountSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public override int? DiscountSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (Field<EPEmployeeClass.freightAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public override int? FreightAcctID { get; set; }

  [SubAccount(typeof (EPEmployeeClass.freightAcctID))]
  [PXForeignReference(typeof (Field<EPEmployeeClass.freightSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public override int? FreightSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (Field<EPEmployeeClass.pOAccrualAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public override int? POAccrualAcctID { get; set; }

  [SubAccount(typeof (EPEmployeeClass.pOAccrualAcctID))]
  [PXForeignReference(typeof (Field<EPEmployeeClass.pOAccrualSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public override int? POAccrualSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (Field<EPEmployeeClass.prebookAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public override int? PrebookAcctID { get; set; }

  [SubAccount(typeof (EPEmployeeClass.prebookAcctID))]
  [PXForeignReference(typeof (Field<EPEmployeeClass.prebookSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public override int? PrebookSubID { get; set; }

  public new class PK : PrimaryKeyOf<EPEmployeeClass>.By<EPEmployeeClass.vendorClassID>
  {
    public static EPEmployeeClass Find(PXGraph graph, string vendorClassID, PKFindOptions options = 0)
    {
      return (EPEmployeeClass) PrimaryKeyOf<EPEmployeeClass>.By<EPEmployeeClass.vendorClassID>.FindBy(graph, (object) vendorClassID, options);
    }
  }

  public new static class FK
  {
    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<EPEmployeeClass>.By<VendorClass.termsID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.paymentMethodID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.cashAcctID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.taxZoneID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.curyID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.curyRateTypeID>
    {
    }

    public class APAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeClass>.By<VendorClass.aPAcctID>
    {
    }

    public class APSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployeeClass>.By<VendorClass.aPSubID>
    {
    }

    public class CashDiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.discTakenAcctID>
    {
    }

    public class CashDiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.discTakenSubID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeClass>.By<VendorClass.expenseAcctID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployeeClass>.By<VendorClass.expenseSubID>
    {
    }

    public class PrepaymentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeClass>.By<VendorClass.prepaymentAcctID>
    {
    }

    public class PrepaymentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployeeClass>.By<VendorClass.prepaymentSubID>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<EPEmployeeClass>.By<EPEmployeeClass.salesSubID>
    {
    }
  }

  public new abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeClass.vendorClassID>
  {
  }

  public new abstract class discTakenAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.discTakenAcctID>
  {
  }

  public new abstract class discTakenSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.discTakenSubID>
  {
  }

  public new abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeClass.paymentMethodID>
  {
  }

  public new abstract class cashAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeClass.cashAcctID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeClass.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeClass.salesSubID>
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployeeClass.calendarID>
  {
  }

  public abstract class hoursValidation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeClass.hoursValidation>
  {
  }

  public new abstract class paymentByType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.paymentByType>
  {
  }

  public abstract class defaultDateInActivity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeClass.defaultDateInActivity>
  {
    public const string LastDay = "LD";
    public const string NextWorkDay = "NW";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "NW", "LD" }, new string[2]
        {
          "Next Work Day",
          "Last Day Entered"
        })
      {
      }
    }
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployeeClass.curyID>
  {
  }

  public new abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEmployeeClass.curyRateTypeID>
  {
  }

  public new abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployeeClass.taxZoneID>
  {
  }

  public abstract class probationPeriodMonths : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.probationPeriodMonths>
  {
  }

  public new abstract class discountAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.discountAcctID>
  {
  }

  public new abstract class discountSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.discountSubID>
  {
  }

  public new abstract class freightAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.freightAcctID>
  {
  }

  public new abstract class freightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeClass.freightSubID>
  {
  }

  public new abstract class pOAccrualAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.pOAccrualAcctID>
  {
  }

  public new abstract class pOAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.pOAccrualSubID>
  {
  }

  public new abstract class prebookAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClass.prebookAcctID>
  {
  }

  public new abstract class prebookSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployeeClass.prebookSubID>
  {
  }
}
