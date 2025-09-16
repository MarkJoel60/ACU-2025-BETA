// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PrintChecksFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.PaymentProcessor.Data;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class PrintChecksFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _PayAccountID;
  protected 
  #nullable disable
  string _NextCheckNbr;
  protected Decimal? _Balance;
  protected Decimal? _CurySelTotal;
  protected Decimal? _SelTotal;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _CashBalance;
  protected string _PayFinPeriodID;
  protected Decimal? _GLBalance;

  [PXDefault(typeof (AccessInfo.branchID))]
  [Branch(null, null, true, true, true, Visible = true, Enabled = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.aPPrintChecks, Equal<True>, Or<PX.Objects.CA.PaymentMethod.aPCreateBatchPayment, Equal<True>, PX.Data.Or<Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.externalPaymentProcessor>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>>>>>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
  public virtual string PayTypeID { get; set; }

  [CashAccount(typeof (PrintChecksFilter.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<PrintChecksFilter.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>), Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (Search2<PaymentMethodAccount.cashAccountID, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<PrintChecksFilter.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.aPIsDefault, Equal<True>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<AccessInfo.branchID>>>>>>>))]
  public virtual int? PayAccountID
  {
    get => this._PayAccountID;
    set => this._PayAccountID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Next Check Number", Visible = false)]
  public virtual string NextCheckNbr
  {
    get => this._NextCheckNbr;
    set => this._NextCheckNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Next Payment Ref. Number", Visible = false)]
  public virtual string NextPaymentRefNbr
  {
    [PXDependsOnFields(new System.Type[] {typeof (PrintChecksFilter.nextCheckNbr)})] get
    {
      return this.NextCheckNbr;
    }
    set => this.NextCheckNbr = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Decimal? Balance
  {
    get => this._Balance;
    set => this._Balance = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PrintChecksFilter.curyInfoID), typeof (PrintChecksFilter.selTotal), BaseCalc = false)]
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
  [PXUIField(DisplayName = "Number of Payments", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual int? SelCount { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [PXDefault(typeof (Search<PX.Objects.CA.CashAccount.curyID, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<PrintChecksFilter.payAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "AP")]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PrintChecksFilter.curyID))]
  [PXUIField(DisplayName = "Available Balance", Enabled = false)]
  [PX.Objects.CA.CashBalance(typeof (PrintChecksFilter.payAccountID))]
  public virtual Decimal? CashBalance
  {
    get => this._CashBalance;
    set => this._CashBalance = value;
  }

  [FinPeriodID(typeof (AccessInfo.businessDate), null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.Visible)]
  public virtual string PayFinPeriodID
  {
    get => this._PayFinPeriodID;
    set => this._PayFinPeriodID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PrintChecksFilter.curyID))]
  [PXUIField(DisplayName = "GL Balance", Enabled = false)]
  [PX.Objects.CA.GLBalance(typeof (PrintChecksFilter.payAccountID), typeof (PrintChecksFilter.payFinPeriodID))]
  public virtual Decimal? GLBalance
  {
    get => this._GLBalance;
    set => this._GLBalance = value;
  }

  internal virtual MfaResponse MfaResult { get; set; }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R1.")]
  public bool IsNextNumberDuplicated(PXGraph graph, string nextNumber)
  {
    return PaymentRefAttribute.IsNextNumberDuplicated(graph, this.PayAccountID, this.PayTypeID, nextNumber);
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PrintChecksFilter.branchID>
  {
  }

  public abstract class payTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PrintChecksFilter.payTypeID>
  {
  }

  public abstract class payAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PrintChecksFilter.payAccountID>
  {
  }

  public abstract class nextCheckNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PrintChecksFilter.nextCheckNbr>
  {
  }

  public abstract class nextPaymentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PrintChecksFilter.nextPaymentRefNbr>
  {
  }

  public abstract class balance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PrintChecksFilter.balance>
  {
  }

  public abstract class curySelTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PrintChecksFilter.curySelTotal>
  {
  }

  public abstract class selTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PrintChecksFilter.selTotal>
  {
  }

  public abstract class selCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PrintChecksFilter.selCount>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PrintChecksFilter.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PrintChecksFilter.curyInfoID>
  {
  }

  public abstract class cashBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PrintChecksFilter.cashBalance>
  {
  }

  public abstract class payFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PrintChecksFilter.payFinPeriodID>
  {
  }

  public abstract class gLBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PrintChecksFilter.gLBalance>
  {
  }
}
