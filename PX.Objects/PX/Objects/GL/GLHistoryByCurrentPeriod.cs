// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryByCurrentPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// The DAC is version of GLHistoryByPeriod <see cref="T:PX.Objects.GL.GLHistoryByPeriod" /> with restriction by GLHistory.FinPeriodID <see cref="T:PX.Objects.GL.GLHistory.finPeriodID" />
/// with current value of field GLHistoryFilter.FinPeriodID <see cref="T:PX.Objects.GL.GLHistoryFilter.finPeriodID" /> of cache GLHistoryFilter <see cref="T:PX.Objects.GL.GLHistoryFilter" />
/// </summary>
[PXProjection(typeof (Select5<GLHistory, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, Equal<CurrentValue<GLHistoryFilter.finPeriodID>>, And<MasterFinPeriod.finPeriodID, GreaterEqual<GLHistory.finPeriodID>>>>, Aggregate<Max<GLHistory.finPeriodID, GroupBy<GLHistory.branchID, GroupBy<GLHistory.ledgerID, GroupBy<GLHistory.accountID, GroupBy<GLHistory.subID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>))]
[GLHistoryPrimaryGraph]
[PXCacheName("GL History by Period")]
[Serializable]
public class GLHistoryByCurrentPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _LedgerID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _LastActivityPeriod;
  protected string _FinPeriodID;

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, which the history record belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BAccountID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (Branch.branchID), SubstituteKey = typeof (Branch.branchCD))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, which the history record belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.ledgerID))]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (Ledger.ledgerID), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account" /> associated with the history record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(IsKey = true, BqlField = typeof (GLHistory.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the history record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(IsKey = true, BqlField = typeof (GLHistory.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial Period</see> of the last activity on the Account and Subaccount associated with the history record,
  /// with regards to Ledger and Branch.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (GLHistory.finPeriodID))]
  [PXUIField]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  /// <summary>
  /// Identifier of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial Period</see>, for which the history data is given.
  /// </summary>
  [PXUIField]
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXString]
  public virtual string FinYear
  {
    get => this.FinPeriodID?.Substring(0, 4);
    set
    {
    }
  }

  public class PK : 
    PrimaryKeyOf<GLHistoryByCurrentPeriod>.By<GLHistoryByCurrentPeriod.ledgerID, GLHistoryByCurrentPeriod.branchID, GLHistoryByCurrentPeriod.accountID, GLHistoryByCurrentPeriod.subID, GLHistoryByCurrentPeriod.finPeriodID>
  {
    public static GLHistoryByCurrentPeriod Find(
      PXGraph graph,
      int? ledgerID,
      int? branchID,
      int? accountID,
      int? subID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (GLHistoryByCurrentPeriod) PrimaryKeyOf<GLHistoryByCurrentPeriod>.By<GLHistoryByCurrentPeriod.ledgerID, GLHistoryByCurrentPeriod.branchID, GLHistoryByCurrentPeriod.accountID, GLHistoryByCurrentPeriod.subID, GLHistoryByCurrentPeriod.finPeriodID>.FindBy(graph, (object) ledgerID, (object) branchID, (object) accountID, (object) subID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLHistoryByCurrentPeriod>.By<GLHistoryByCurrentPeriod.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLHistoryByCurrentPeriod>.By<GLHistoryByCurrentPeriod.ledgerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLHistoryByCurrentPeriod>.By<GLHistoryByCurrentPeriod.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLHistoryByCurrentPeriod>.By<GLHistoryByCurrentPeriod.subID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByCurrentPeriod.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByCurrentPeriod.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByCurrentPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByCurrentPeriod.subID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryByCurrentPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryByCurrentPeriod.finPeriodID>
  {
  }

  public abstract class finYear : IBqlField, IBqlOperand
  {
  }
}
