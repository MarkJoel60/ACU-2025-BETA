// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// A general ledger history record. An instance of this class represents a history record for
/// a particular <see cref="P:PX.Objects.GL.BaseGLHistory.LedgerID">ledger</see>, <see cref="P:PX.Objects.GL.BaseGLHistory.BranchID">
/// branch</see>, <see cref="P:PX.Objects.GL.BaseGLHistory.AccountID">account</see>, <see cref="P:PX.Objects.GL.BaseGLHistory.SubID">
/// subaccount</see>, and <see cref="P:PX.Objects.GL.BaseGLHistory.FinPeriodID">financial period</see>.
/// </summary>
[PXCacheName("GL History")]
[DebuggerDisplay("Account = {AccountID}, FinPeriod = {FinPeriodID}, FinPtdCredit = {FinPtdCredit}, FinPtdDebit = {FinPtdCredit}, FinYtdBalance = {FinYtdBalance}, FinBegBalance = {FinBegBalance}")]
[Serializable]
public class GLHistory : BaseGLHistory, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _DetDeleted;
  protected Decimal? _AllocPtdBalance;
  protected Decimal? _AllocBegBalance;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of the history record.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [PXDefault]
  [PXUIField]
  [FinPeriodSelector(null, null, typeof (GLHistory.branchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public override 
  #nullable disable
  string FinPeriodID { get; set; }

  /// <summary>The identifier of the financial year.</summary>
  /// <value>
  /// Determined from the <see cref="P:PX.Objects.GL.GLHistory.FinPeriodID" /> field.
  /// </value>
  [PXDBCalced(typeof (Substring<GLHistory.finPeriodID, int1, int4>), typeof (string))]
  public virtual string FinYear { get; set; }

  /// <summary>
  /// An unused obsolete field. In the past, it was used to
  /// indicate that the related <see cref="P:PX.Objects.GL.BaseGLHistory.AccountID">
  /// account</see> or <see cref="P:PX.Objects.GL.BaseGLHistory.SubID">subaccount</see>
  /// had been deleted from the system so as to exclude the corresponding
  /// financial periods from GL balance validation process.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [Obsolete("This field has been deprecated and is not used anymore.")]
  public virtual bool? DetDeleted
  {
    get => this._DetDeleted;
    set => this._DetDeleted = value;
  }

  /// <summary>
  /// The period-to-date allocation balance (the amount allocated since the beginning of the period of the history record).
  /// </summary>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocPtdBalance
  {
    get => this._AllocPtdBalance;
    set => this._AllocPtdBalance = value;
  }

  /// <summary>
  /// The beginning allocation balance (the amount allocated for periods preceding the period of the history record).
  /// </summary>
  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocBegBalance
  {
    get => this._AllocBegBalance;
    set => this._AllocBegBalance = value;
  }

  public class PK : 
    PrimaryKeyOf<GLHistory>.By<GLHistory.ledgerID, GLHistory.branchID, GLHistory.accountID, GLHistory.subID, GLHistory.finPeriodID>
  {
    public static GLHistory Find(
      PXGraph graph,
      int? ledgerID,
      int? branchID,
      int? accountID,
      int? subID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (GLHistory) PrimaryKeyOf<GLHistory>.By<GLHistory.ledgerID, GLHistory.branchID, GLHistory.accountID, GLHistory.subID, GLHistory.finPeriodID>.FindBy(graph, (object) ledgerID, (object) branchID, (object) accountID, (object) subID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLHistory>.By<GLHistory.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLHistory>.By<GLHistory.ledgerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLHistory>.By<GLHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLHistory>.By<GLHistory.subID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<GLHistory>.By<GLHistory.curyID>
    {
    }
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistory.ledgerID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistory.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistory.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistory.finPeriodID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistory.finYear>
  {
  }

  public abstract class balanceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistory.balanceType>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistory.curyID>
  {
  }

  [Obsolete("This field has been deprecated and is not used anymore.")]
  public abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLHistory.detDeleted>
  {
  }

  public abstract class finPtdCredit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistory.finPtdCredit>
  {
  }

  public abstract class finPtdDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistory.finPtdDebit>
  {
  }

  public abstract class finYtdBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistory.finYtdBalance>
  {
  }

  public abstract class finBegBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistory.finBegBalance>
  {
  }

  public abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.finPtdRevalued>
  {
  }

  public abstract class tranPtdCredit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistory.tranPtdCredit>
  {
  }

  public abstract class tranPtdDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistory.tranPtdDebit>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.tranYtdBalance>
  {
  }

  public abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.tranBegBalance>
  {
  }

  public abstract class curyFinPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyFinPtdCredit>
  {
  }

  public abstract class curyFinPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyFinPtdDebit>
  {
  }

  public abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyFinYtdBalance>
  {
  }

  public abstract class curyFinBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyFinBegBalance>
  {
  }

  public abstract class curyTranPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyTranPtdCredit>
  {
  }

  public abstract class curyTranPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyTranPtdDebit>
  {
  }

  public abstract class curyTranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyTranYtdBalance>
  {
  }

  public abstract class curyTranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.curyTranBegBalance>
  {
  }

  public abstract class allocPtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.allocPtdBalance>
  {
  }

  public abstract class allocBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory.allocBegBalance>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLHistory.Tstamp>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLHistory.lastModifiedDateTime>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public new abstract class rEFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLHistory.rEFlag>
  {
  }
}
