// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
[Serializable]
public class AccountByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _FinPeriodID;
  protected int? _AccountID;
  protected DateTime? _PeriodDate;
  protected string _PerDesc;
  protected Decimal? _CreditAmount;
  protected Decimal? _DebitAmount;

  [OpenPeriod(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [AccountAny]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? PeriodDate
  {
    get => this._PeriodDate;
    set => this._PeriodDate = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string PerDesc
  {
    get => this._PerDesc;
    set => this._PerDesc = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CreditAmount
  {
    get => this._CreditAmount;
    set => this._CreditAmount = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? DebitAmount
  {
    get => this._DebitAmount;
    set => this._DebitAmount = value;
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountByPeriod.finPeriodID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByPeriod.accountID>
  {
  }

  public abstract class periodDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AccountByPeriod.periodDate>
  {
  }

  public abstract class perDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountByPeriod.perDesc>
  {
  }

  public abstract class creditAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriod.creditAmount>
  {
  }

  public abstract class debitAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AccountByPeriod.debitAmount>
  {
  }
}
