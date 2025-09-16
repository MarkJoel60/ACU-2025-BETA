// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryByPeriod
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
/// The DAC that is used to simplify selection and aggregation of proper <see cref="T:PX.Objects.GL.GLHistory" /> records
/// on various inquiry and processing forms related to the general ledger functionality.
/// The main purpose of this DAC is
/// to close the gaps in GL history records, which appear in case GL history records do not exist for
/// every financial period defined in the system. To close these gaps, this projection DAC
/// calculates the <see cref="P:PX.Objects.GL.GLHistoryByPeriod.LastActivityPeriod">last activity period</see> for every existing
/// <see cref="T:PX.Objects.GL.FinPeriods.MasterFinPeriod">master financial period</see>,
/// so that inquiries and reports that produce information
/// for a given master financial period can look at the latest available <see cref="T:PX.Objects.GL.GLHistory" /> record.
/// </summary>
[PXProjection(typeof (Select5<GLHistory, LeftJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<GLHistory.finPeriodID>>>, Aggregate<Max<GLHistory.finPeriodID, Max<GLHistory.lastModifiedDateTime, GroupBy<GLHistory.branchID, GroupBy<GLHistory.ledgerID, GroupBy<GLHistory.accountID, GroupBy<GLHistory.subID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>>))]
[GLHistoryPrimaryGraph]
[PXCacheName("GL History by Period")]
[Serializable]
public class GLHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [PXDBLastModifiedDateTime(BqlField = typeof (GLHistory.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<GLHistoryByPeriod>.By<GLHistoryByPeriod.ledgerID, GLHistoryByPeriod.branchID, GLHistoryByPeriod.accountID, GLHistoryByPeriod.subID, GLHistoryByPeriod.finPeriodID>
  {
    public static GLHistoryByPeriod Find(
      PXGraph graph,
      int? ledgerID,
      int? branchID,
      int? accountID,
      int? subID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (GLHistoryByPeriod) PrimaryKeyOf<GLHistoryByPeriod>.By<GLHistoryByPeriod.ledgerID, GLHistoryByPeriod.branchID, GLHistoryByPeriod.accountID, GLHistoryByPeriod.subID, GLHistoryByPeriod.finPeriodID>.FindBy(graph, (object) ledgerID, (object) branchID, (object) accountID, (object) subID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLHistoryByPeriod>.By<GLHistoryByPeriod.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLHistoryByPeriod>.By<GLHistoryByPeriod.ledgerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLHistoryByPeriod>.By<GLHistoryByPeriod.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLHistoryByPeriod>.By<GLHistoryByPeriod.subID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriod.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriod.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriod.subID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryByPeriod.finPeriodID>
  {
  }

  public abstract class finYear : IBqlField, IBqlOperand
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLHistoryByPeriod.lastModifiedDateTime>
  {
  }
}
