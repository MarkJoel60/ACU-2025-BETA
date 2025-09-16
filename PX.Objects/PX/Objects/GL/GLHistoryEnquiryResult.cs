// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryEnquiryResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL History Enquiry Results")]
[Serializable]
public class GLHistoryEnquiryResult : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ISignedBalances
{
  protected int? _LedgerID;
  protected int? _AccountID;
  protected int? _BranchID;
  protected 
  #nullable disable
  string _Type;
  protected string _Description;
  protected string _LastActivityPeriod;
  protected Decimal? _BegBalance;
  protected Decimal? _PtdDebitTotal;
  protected Decimal? _PtdCreditTotal;
  protected Decimal? _EndBalance;
  protected Decimal? _SignBegBalance;
  protected Decimal? _SignEndBalance;
  protected string _CuryID;
  protected Decimal? _CuryBegBalance;
  protected Decimal? _CuryPtdDebitTotal;
  protected Decimal? _CuryPtdCreditTotal;
  protected Decimal? _CuryEndBalance;
  protected Decimal? _SignCuryBegBalance;
  protected Decimal? _SignCuryEndBalance;
  protected string _ConsolAccountCD;
  protected string _AccountClassID;
  protected string _SubCD;

  [PXDBInt(IsKey = true)]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXUIField]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDimensionSelector("ACCOUNT", typeof (Account.accountCD), typeof (GLHistoryEnquiryResult.accountCD), new System.Type[] {typeof (Account.accountCD), typeof (Account.accountClassID), typeof (Account.type), typeof (Account.description)})]
  public virtual string AccountCD { get; set; }

  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(1)]
  [PXDefault("I")]
  [AccountType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXUIField(DisplayName = "Last Activity", Visible = false)]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  [PXDBBaseCury(typeof (GLHistoryEnquiryResult.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beg. Balance")]
  public virtual Decimal? BegBalance
  {
    get => this._BegBalance;
    set => this._BegBalance = value;
  }

  [PXDBBaseCury(typeof (GLHistoryEnquiryResult.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Debit Total")]
  public virtual Decimal? PtdDebitTotal
  {
    get => this._PtdDebitTotal;
    set => this._PtdDebitTotal = value;
  }

  [PXDBBaseCury(typeof (GLHistoryEnquiryResult.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Total")]
  public virtual Decimal? PtdCreditTotal
  {
    get => this._PtdCreditTotal;
    set => this._PtdCreditTotal = value;
  }

  [PXDBBaseCury(typeof (GLHistoryEnquiryResult.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Balance")]
  public virtual Decimal? EndBalance
  {
    get => this._EndBalance;
    set => this._EndBalance = value;
  }

  [PXDBBaseCury(typeof (GLHistoryEnquiryResult.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beg. Balance")]
  public virtual Decimal? SignBegBalance
  {
    get => this._SignBegBalance;
    set => this._SignBegBalance = value;
  }

  [PXBaseCury(typeof (GLHistoryEnquiryResult.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ending Balance")]
  public virtual Decimal? SignEndBalance
  {
    get => this._SignEndBalance;
    set => this._SignEndBalance = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Beg. Balance")]
  public virtual Decimal? CuryBegBalance
  {
    get => this._CuryBegBalance;
    set => this._CuryBegBalance = value;
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Debit Total")]
  public virtual Decimal? CuryPtdDebitTotal
  {
    get => this._CuryPtdDebitTotal;
    set => this._CuryPtdDebitTotal = value;
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Credit Total")]
  public virtual Decimal? CuryPtdCreditTotal
  {
    get => this._CuryPtdCreditTotal;
    set => this._CuryPtdCreditTotal = value;
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Ending Balance")]
  public virtual Decimal? CuryEndBalance
  {
    get => this._CuryEndBalance;
    set => this._CuryEndBalance = value;
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Beg. Balance")]
  public virtual Decimal? SignCuryBegBalance
  {
    get => this._SignCuryBegBalance;
    set => this._SignCuryBegBalance = value;
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Ending Balance")]
  public virtual Decimal? SignCuryEndBalance
  {
    get => this._SignCuryEndBalance;
    set => this._SignCuryEndBalance = value;
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXUIField(DisplayName = "Ptd. Total", Visible = false)]
  public Decimal? PtdSaldo
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLHistoryEnquiryResult.type), typeof (GLHistoryEnquiryResult.ptdDebitTotal), typeof (GLHistoryEnquiryResult.ptdCreditTotal)})] get
    {
      return new Decimal?(AccountRules.CalcSaldo(this._Type, this._PtdDebitTotal.GetValueOrDefault(), this._PtdCreditTotal.GetValueOrDefault()));
    }
  }

  [PXDBCury(typeof (GLHistoryEnquiryResult.curyID))]
  [PXUIField(DisplayName = "Cury. Ptd. Total", Visible = false)]
  public Decimal? CuryPtdSaldo
  {
    [PXDependsOnFields(new System.Type[] {typeof (GLHistoryEnquiryResult.type), typeof (GLHistoryEnquiryResult.curyPtdDebitTotal), typeof (GLHistoryEnquiryResult.curyPtdCreditTotal)})] get
    {
      return new Decimal?(AccountRules.CalcSaldo(this._Type, this._CuryPtdDebitTotal.GetValueOrDefault(), this._CuryPtdCreditTotal.GetValueOrDefault()));
    }
  }

  public virtual void recalculate(bool reversive)
  {
    if (reversive)
    {
      Decimal? endBalance = this._EndBalance;
      Decimal? nullable = this.PtdSaldo;
      this._BegBalance = endBalance.HasValue & nullable.HasValue ? new Decimal?(endBalance.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
      nullable = this._CuryEndBalance;
      Decimal? curyPtdSaldo = this.CuryPtdSaldo;
      this._CuryBegBalance = nullable.HasValue & curyPtdSaldo.HasValue ? new Decimal?(nullable.GetValueOrDefault() - curyPtdSaldo.GetValueOrDefault()) : new Decimal?();
    }
    else
    {
      Decimal? begBalance = this._BegBalance;
      Decimal? nullable = this.PtdSaldo;
      this._EndBalance = begBalance.HasValue & nullable.HasValue ? new Decimal?(begBalance.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
      nullable = this._CuryBegBalance;
      Decimal? curyPtdSaldo = this.CuryPtdSaldo;
      this._CuryEndBalance = nullable.HasValue & curyPtdSaldo.HasValue ? new Decimal?(nullable.GetValueOrDefault() + curyPtdSaldo.GetValueOrDefault()) : new Decimal?();
    }
  }

  public static void recalculateSignAmount(ISignedBalances item, bool uplaySignReverse)
  {
    if (uplaySignReverse && (item.Type == "I" || item.Type == "L"))
    {
      ISignedBalances signedBalances1 = item;
      Decimal? begBalance = item.BegBalance;
      Decimal? nullable1 = begBalance.HasValue ? new Decimal?(-begBalance.GetValueOrDefault()) : new Decimal?();
      signedBalances1.SignBegBalance = nullable1;
      ISignedBalances signedBalances2 = item;
      Decimal? endBalance = item.EndBalance;
      Decimal? nullable2 = endBalance.HasValue ? new Decimal?(-endBalance.GetValueOrDefault()) : new Decimal?();
      signedBalances2.SignEndBalance = nullable2;
      ISignedBalances signedBalances3 = item;
      Decimal? curyBegBalance = item.CuryBegBalance;
      Decimal? nullable3 = curyBegBalance.HasValue ? new Decimal?(-curyBegBalance.GetValueOrDefault()) : new Decimal?();
      signedBalances3.SignCuryBegBalance = nullable3;
      ISignedBalances signedBalances4 = item;
      Decimal? curyEndBalance = item.CuryEndBalance;
      Decimal? nullable4 = curyEndBalance.HasValue ? new Decimal?(-curyEndBalance.GetValueOrDefault()) : new Decimal?();
      signedBalances4.SignCuryEndBalance = nullable4;
    }
    else
    {
      item.SignBegBalance = item.BegBalance;
      item.SignEndBalance = item.EndBalance;
      item.SignCuryBegBalance = item.CuryBegBalance;
      item.SignCuryEndBalance = item.CuryEndBalance;
    }
  }

  public virtual void recalculateSignAmount(bool uplaySignReverse)
  {
    GLHistoryEnquiryResult.recalculateSignAmount((ISignedBalances) this, uplaySignReverse);
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (GLConsolAccount.accountCD), DescriptionField = typeof (GLConsolAccount.description))]
  public virtual string ConsolAccountCD
  {
    get => this._ConsolAccountCD;
    set => this._ConsolAccountCD = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Class")]
  [PXSelector(typeof (AccountClass.accountClassID), DescriptionField = typeof (AccountClass.descr))]
  public virtual string AccountClassID
  {
    get => this._AccountClassID;
    set => this._AccountClassID = value;
  }

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Subaccount")]
  [PXDimension("SUBACCOUNT")]
  public virtual string SubCD
  {
    get => this._SubCD;
    set => this._SubCD = value;
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryEnquiryResult.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryEnquiryResult.accountID>
  {
  }

  public abstract class accountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnquiryResult.accountCD>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryEnquiryResult.branchID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistoryEnquiryResult.type>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnquiryResult.description>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnquiryResult.lastActivityPeriod>
  {
  }

  public abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.begBalance>
  {
  }

  public abstract class ptdDebitTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.ptdDebitTotal>
  {
  }

  public abstract class ptdCreditTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.ptdCreditTotal>
  {
  }

  public abstract class endBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.endBalance>
  {
  }

  public abstract class signBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.signBegBalance>
  {
  }

  public abstract class signEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.signEndBalance>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistoryEnquiryResult.curyID>
  {
  }

  public abstract class curyBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.curyBegBalance>
  {
  }

  public abstract class curyPtdDebitTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.curyPtdDebitTotal>
  {
  }

  public abstract class curyPtdCreditTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.curyPtdCreditTotal>
  {
  }

  public abstract class curyEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.curyEndBalance>
  {
  }

  public abstract class signCuryBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.signCuryBegBalance>
  {
  }

  public abstract class signCuryEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.signCuryEndBalance>
  {
  }

  public abstract class ptdSaldo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.ptdSaldo>
  {
  }

  public abstract class curyPtdSaldo : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryEnquiryResult.curyPtdSaldo>
  {
  }

  public abstract class consolAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnquiryResult.consolAccountCD>
  {
  }

  public abstract class accountClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryEnquiryResult.accountClassID>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistoryEnquiryResult.subCD>
  {
  }
}
