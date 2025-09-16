// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BaseGLHistory
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

[PXPrimaryGraph(typeof (AccountByPeriodEnq), Filter = typeof (AccountByPeriodFilter))]
[Serializable]
public class BaseGLHistory : PXBqlTable
{
  protected int? _LedgerID;
  protected int? _BranchID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _BalanceType;
  protected string _CuryID;
  protected Decimal? _FinPtdCredit;
  protected Decimal? _FinPtdDebit;
  protected Decimal? _FinYtdBalance;
  protected Decimal? _FinBegBalance;
  protected Decimal? _FinPtdRevalued;
  protected Decimal? _TranPtdCredit;
  protected Decimal? _TranPtdDebit;
  protected Decimal? _TranYtdBalance;
  protected Decimal? _TranBegBalance;
  protected Decimal? _CuryFinPtdCredit;
  protected Decimal? _CuryFinPtdDebit;
  protected Decimal? _CuryFinYtdBalance;
  protected Decimal? _CuryFinBegBalance;
  protected Decimal? _CuryTranPtdCredit;
  protected Decimal? _CuryTranPtdDebit;
  protected Decimal? _CuryTranYtdBalance;
  protected Decimal? _CuryTranBegBalance;
  protected bool? _FinFlag = new bool?(true);
  protected byte[] _tstamp;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Ledger">ledger</see> associated with the history record.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Ledger.ledgerID), new Type[] {typeof (Ledger.ledgerCD), typeof (Ledger.baseCuryID), typeof (Ledger.descr), typeof (Ledger.balanceType)}, DescriptionField = typeof (Ledger.descr), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> associated with the history record.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BAccountID" /> field.
  /// </value>
  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">account</see> associated with the history record.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> associated with the history record.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of the history record.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinPeriodID" /> field.
  /// </value>
  public virtual string FinPeriodID { get; set; }

  /// <summary>The type of the balance.</summary>
  /// <value>
  /// Allowed values are:
  /// <c>"A"</c> - Actual,
  /// <c>"R"</c> - Reporting,
  /// <c>"S"</c> - Statistical,
  /// <c>"B"</c> - Budget.
  /// See <see cref="P:PX.Objects.GL.Ledger.BalanceType" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [LedgerBalanceType.List]
  [PXUIField(DisplayName = "Balance Type")]
  public virtual string BalanceType
  {
    get => this._BalanceType;
    set => this._BalanceType = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Currency" /> of the history record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// The period-to-date credit total of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field,
  /// which can be overridden by the user. See also <see cref="P:PX.Objects.GL.BaseGLHistory.TranPtdCredit" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Fin. PTD Credit")]
  public virtual Decimal? FinPtdCredit
  {
    get => this._FinPtdCredit;
    set => this._FinPtdCredit = value;
  }

  /// <summary>
  /// The period-to-date debit total of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field,
  /// which can be overridden by the user. See also <see cref="P:PX.Objects.GL.BaseGLHistory.TranPtdDebit" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Fin. PTD Debit")]
  public virtual Decimal? FinPtdDebit
  {
    get => this._FinPtdDebit;
    set => this._FinPtdDebit = value;
  }

  /// <summary>
  /// The year-to-date balance of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field, which
  /// can be overridden by the user. See also <see cref="P:PX.Objects.GL.BaseGLHistory.TranYtdBalance" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Fin. YTD Balance")]
  public virtual Decimal? FinYtdBalance
  {
    get => this._FinYtdBalance;
    set => this._FinYtdBalance = value;
  }

  /// <summary>
  /// The beginning balance of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field, which
  /// can be overridden by the user. See also <see cref="P:PX.Objects.GL.BaseGLHistory.TranBegBalance" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Fin. Begining Balance")]
  public virtual Decimal? FinBegBalance
  {
    get => this._FinBegBalance;
    set => this._FinBegBalance = value;
  }

  /// <summary>
  /// The period-to-date revalued balance of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field,
  /// which can be overridden by the user. There is no corresponding amount field that depends on the
  /// <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRevalued
  {
    get => this._FinPtdRevalued;
    set => this._FinPtdRevalued = value;
  }

  /// <summary>
  /// The period-to-date credit total of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field,
  /// which cannot be overridden by the user and is determined by the date of the transactions.
  /// See also <see cref="P:PX.Objects.GL.BaseGLHistory.FinPtdCredit" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCredit
  {
    get => this._TranPtdCredit;
    set => this._TranPtdCredit = value;
  }

  /// <summary>
  /// The period-to-date debit total of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field,
  /// which cannot be overridden by the user and is determined by the date of the transactions.
  /// See also <see cref="P:PX.Objects.GL.BaseGLHistory.FinPtdDebit" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDebit
  {
    get => this._TranPtdDebit;
    set => this._TranPtdDebit = value;
  }

  /// <summary>
  /// The year-to-date balance of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field,
  /// which cannot be overridden by the user and is determined by the date of the transactions.
  /// See also <see cref="P:PX.Objects.GL.BaseGLHistory.FinYtdBalance" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdBalance
  {
    get => this._TranYtdBalance;
    set => this._TranYtdBalance = value;
  }

  /// <summary>
  /// The beginning balance of the account in the <see cref="T:PX.Objects.GL.Branch.baseCuryID">base currency</see> of the branch.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field,
  /// which cannot be overridden by the user and is determined by the date of the transactions.
  /// See also <see cref="P:PX.Objects.GL.BaseGLHistory.FinBegBalance" />.
  /// </value>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranBegBalance
  {
    get => this._TranBegBalance;
    set => this._TranBegBalance = value;
  }

  /// <summary>
  /// The period-to-date credit total of the account in the <see cref="!:BaseGLHistory.curyID">currency</see> of the account.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field,
  /// which can be overridden by the user. See also <see cref="P:PX.Objects.GL.BaseGLHistory.CuryTranPtdCredit" />.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdCredit
  {
    get => this._CuryFinPtdCredit;
    set => this._CuryFinPtdCredit = value;
  }

  /// <summary>
  /// The period-to-date debit total of the account in the <see cref="!:BaseGLHistory.curyID">currency</see> of the account.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field,
  /// which can be overridden by the user. See also <see cref="P:PX.Objects.GL.BaseGLHistory.CuryTranPtdDebit" />.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdDebit
  {
    get => this._CuryFinPtdDebit;
    set => this._CuryFinPtdDebit = value;
  }

  /// <summary>
  /// The year-to-date balance of the account in the <see cref="!:BaseGLHistory.curyID">currency</see> of the account.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field,
  /// which can be overridden by the user. See also <see cref="P:PX.Objects.GL.BaseGLHistory.CuryTranYtdBalance" />.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CuryFinYtdBalance")]
  public virtual Decimal? CuryFinYtdBalance
  {
    get => this._CuryFinYtdBalance;
    set => this._CuryFinYtdBalance = value;
  }

  /// <summary>
  /// The beginning balance of the account in the <see cref="!:BaseGLHistory.curyID">currency</see> of the account.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.FinPeriodID" /> field, which can be overridden by the user.
  /// See also <see cref="P:PX.Objects.GL.BaseGLHistory.CuryTranBegBalance" />.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CuryFinBegBalance")]
  public virtual Decimal? CuryFinBegBalance
  {
    get => this._CuryFinBegBalance;
    set => this._CuryFinBegBalance = value;
  }

  /// <summary>
  /// The period-to-date credit of the account in the <see cref="!:BaseGLHistory.curyID">currency</see> of the account.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field, which cannot be overridden by
  /// the user and is determined by the date of the transactions. See also <see cref="P:PX.Objects.GL.BaseGLHistory.CuryFinPtdCredit" />.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdCredit
  {
    get => this._CuryTranPtdCredit;
    set => this._CuryTranPtdCredit = value;
  }

  /// <summary>
  /// The period-to-date debit of the account in the <see cref="!:BaseGLHistory.curyID">currency</see> of the account.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field,
  /// which cannot be overridden by the user and is determined by the date of the transactions.
  /// See also <see cref="P:PX.Objects.GL.BaseGLHistory.CuryFinPtdDebit" />.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdDebit
  {
    get => this._CuryTranPtdDebit;
    set => this._CuryTranPtdDebit = value;
  }

  /// <summary>
  /// The year-to-date balance of the account in the <see cref="!:BaseGLHistory.curyID">currency</see> of the account.
  /// </summary>
  /// <value>
  /// The value of this field is based on the <see cref="P:PX.Objects.GL.GLTran.TranPeriodID" /> field,
  /// which cannot be overridden by the user and is determined by the date of the transactions.
  /// See also <see cref="P:PX.Objects.GL.BaseGLHistory.CuryFinYtdBalance" />.
  /// </value>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranYtdBalance
  {
    get => this._CuryTranYtdBalance;
    set => this._CuryTranYtdBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranBegBalance
  {
    get => this._CuryTranBegBalance;
    set => this._CuryTranBegBalance = value;
  }

  /// <summary>
  /// The flag determining the balance fields, to which the <see cref="P:PX.Objects.GL.BaseGLHistory.PtdCredit" />, <see cref="P:PX.Objects.GL.BaseGLHistory.PtdDebit" />,
  /// <see cref="P:PX.Objects.GL.BaseGLHistory.YtdBalance" />, <see cref="P:PX.Objects.GL.BaseGLHistory.BegBalance" />, <see cref="P:PX.Objects.GL.BaseGLHistory.PtdRevalued" /> and their Cury* counterparts are mapped.
  /// </summary>
  /// <value>
  /// When set to <c>true</c>, the above fields are mapped to their Fin* analogs (e.g. <see cref="P:PX.Objects.GL.BaseGLHistory.PtdDebit" /> will represent - get and set - <see cref="P:PX.Objects.GL.BaseGLHistory.FinPtdDebit" />),
  /// otherwise they are mapped to their Tran* analogs (e.g. <see cref="P:PX.Objects.GL.BaseGLHistory.PtdDebit" /> corresponds to <see cref="P:PX.Objects.GL.BaseGLHistory.TranPtdDebit" />
  /// </value>
  [PXBool]
  public virtual bool? FinFlag
  {
    get => this._FinFlag;
    set => this._FinFlag = value;
  }

  /// <summary>An obsolete unused field.</summary>
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  [PXBool]
  public virtual bool? REFlag { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? PtdCredit
  {
    get => !this._FinFlag.Value ? this._TranPtdCredit : this._FinPtdCredit;
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdCredit = value;
      else
        this._TranPtdCredit = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdDebit
  {
    get => !this._FinFlag.Value ? this._TranPtdDebit : this._FinPtdDebit;
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdDebit = value;
      else
        this._TranPtdDebit = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdBalance
  {
    get => !this._FinFlag.Value ? this._TranYtdBalance : this._FinYtdBalance;
    set
    {
      if (this._FinFlag.Value)
        this._FinYtdBalance = value;
      else
        this._TranYtdBalance = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? BegBalance
  {
    get => !this._FinFlag.Value ? this._TranBegBalance : this._FinBegBalance;
    set
    {
      if (this._FinFlag.Value)
        this._FinBegBalance = value;
      else
        this._TranBegBalance = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdRevalued
  {
    get => !this._FinFlag.Value ? new Decimal?() : this._FinPtdRevalued;
    set
    {
      if (!this._FinFlag.Value)
        return;
      this._FinPtdRevalued = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdCredit
  {
    get => !this._FinFlag.Value ? this._CuryTranPtdCredit : this._CuryFinPtdCredit;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdCredit = value;
      else
        this._CuryTranPtdCredit = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdDebit
  {
    get => !this._FinFlag.Value ? this._CuryTranPtdDebit : this._CuryFinPtdDebit;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdDebit = value;
      else
        this._CuryTranPtdDebit = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdBalance
  {
    get => !this._FinFlag.Value ? this._CuryTranYtdBalance : this._CuryFinYtdBalance;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinYtdBalance = value;
      else
        this._CuryTranYtdBalance = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryBegBalance
  {
    get => !this._FinFlag.Value ? this._CuryTranBegBalance : this._CuryFinBegBalance;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinBegBalance = value;
      else
        this._CuryTranBegBalance = value;
    }
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public abstract class rEFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BaseGLHistory.rEFlag>
  {
  }
}
