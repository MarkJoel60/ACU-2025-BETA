// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.ReleaseChecksFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class ReleaseChecksFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PayTypeID;
  protected int? _PayAccountID;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _CashBalance;
  protected string _PayFinPeriodID;
  protected Decimal? _GLBalance;

  [PXDefault(typeof (AccessInfo.branchID))]
  [Branch(null, null, true, true, true, Visible = true, Enabled = true)]
  public virtual int? BranchID { get; set; }

  [PXDefault]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.externalPaymentProcessor>, PX.Data.Or<Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.externalPaymentProcessor>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>>>>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
  public virtual string PayTypeID
  {
    get => this._PayTypeID;
    set => this._PayTypeID = value;
  }

  [CashAccount(typeof (ReleaseChecksFilter.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<ReleaseChecksFilter.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>), Visibility = PXUIVisibility.Visible)]
  [PXDefault(typeof (Search2<PaymentMethodAccount.cashAccountID, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<ReleaseChecksFilter.payTypeID>>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.aPIsDefault, Equal<True>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<AccessInfo.branchID>>>>>>>))]
  public virtual int? PayAccountID
  {
    get => this._PayAccountID;
    set => this._PayAccountID = value;
  }

  [PXDBString(10)]
  [PXUIField(DisplayName = "Action")]
  [ReleaseChecksFilter.action.List]
  public virtual string Action { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [PXDefault(typeof (Search<PX.Objects.CA.CashAccount.curyID, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<ReleaseChecksFilter.payAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (ReleaseChecksFilter.curyID))]
  [PXUIField(DisplayName = "Available Balance", Enabled = false)]
  [PX.Objects.CA.CashBalance(typeof (ReleaseChecksFilter.payAccountID))]
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
  [PXDBCury(typeof (ReleaseChecksFilter.curyID))]
  [PXUIField(DisplayName = "GL Balance", Enabled = false)]
  [PX.Objects.CA.GLBalance(typeof (ReleaseChecksFilter.payAccountID), typeof (ReleaseChecksFilter.payFinPeriodID))]
  public virtual Decimal? GLBalance
  {
    get => this._GLBalance;
    set => this._GLBalance = value;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReleaseChecksFilter.branchID>
  {
  }

  public abstract class payTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReleaseChecksFilter.payTypeID>
  {
  }

  public abstract class payAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReleaseChecksFilter.payAccountID>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReleaseChecksFilter.action>
  {
    public const string ReleaseChecks = "R";
    public const string ReprintChecks = "D";
    public const string VoidAndReprintChecks = "V";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[1]{ "R" }, new string[1]
        {
          "Release"
        })
      {
      }
    }

    public class releaseChecks : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ReleaseChecksFilter.action.releaseChecks>
    {
      public releaseChecks()
        : base("R")
      {
      }
    }

    public class reprintChecks : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ReleaseChecksFilter.action.reprintChecks>
    {
      public reprintChecks()
        : base("D")
      {
      }
    }

    public class voidAndReprintChecks : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ReleaseChecksFilter.action.voidAndReprintChecks>
    {
      public voidAndReprintChecks()
        : base("V")
      {
      }
    }
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReleaseChecksFilter.curyID>
  {
  }

  [Obsolete("This field is not used anymore")]
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ReleaseChecksFilter.curyInfoID>
  {
  }

  public abstract class cashBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReleaseChecksFilter.cashBalance>
  {
  }

  public abstract class payFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReleaseChecksFilter.payFinPeriodID>
  {
  }

  public abstract class gLBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReleaseChecksFilter.gLBalance>
  {
  }
}
