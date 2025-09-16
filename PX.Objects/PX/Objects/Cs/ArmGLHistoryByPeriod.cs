// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ArmGLHistoryByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// The DAC used to simplify selection and aggregation of proper <see cref="T:PX.Objects.GL.GLHistory" /> records
/// on various inquiry and processing screens of the General Ledger module. The main purpose of this DAC is
/// to close the gaps in GL history records, which appear in case GL history records do not exist for
/// every financial period defined in the system. To close these gaps, this projection DAC
/// calculates the <see cref="P:PX.Objects.CS.ArmGLHistoryByPeriod.LastActivityPeriod">last activity period</see> for every existing
/// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see>, so that inquiries and reports that produce information
/// for a given financial period can look at the latest available <see cref="T:PX.Objects.GL.GLHistory" /> record.
/// </summary>
[PXProjection(typeof (Select5<GLHistory, LeftJoin<PX.Objects.GL.Account, On<GLHistory.accountID, Equal<PX.Objects.GL.Account.accountID>>, LeftJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<GLHistory.finPeriodID>>>>, Aggregate<Max<GLHistory.finPeriodID, Max<PX.Objects.GL.Account.accountClassID, GroupBy<GLHistory.branchID, GroupBy<GLHistory.ledgerID, GroupBy<GLHistory.accountID, GroupBy<GLHistory.subID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>>))]
[GLHistoryPrimaryGraph]
[PXCacheName("GL History by Period")]
public class ArmGLHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, which the history record belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BAccountID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, which the history record belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.ledgerID))]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (PX.Objects.GL.Ledger.ledgerID), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD))]
  public virtual int? LedgerID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Account" /> associated with the history record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(IsKey = true, BqlField = typeof (GLHistory.accountID))]
  public virtual int? AccountID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the history record.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(IsKey = true, BqlField = typeof (GLHistory.subID))]
  public virtual int? SubID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial Period</see> of the last activity on the Account and Subaccount associated with the history record,
  /// with regards to Ledger and Branch.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (GLHistory.finPeriodID))]
  [PXUIField]
  public virtual 
  #nullable disable
  string LastActivityPeriod { get; set; }

  [PXDBString(20, IsUnicode = true, BqlField = typeof (PX.Objects.GL.Account.accountClassID))]
  public virtual string AccountClassID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial Period</see>, for which the history data is given.
  /// </summary>
  [PXUIField]
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  public virtual string FinPeriodID { get; set; }

  /// <summary>Financial year, to which history data belongs</summary>
  [PXString]
  public virtual string FinYear
  {
    get => this.FinPeriodID?.Substring(0, 4);
    set
    {
    }
  }

  public class PK : 
    PrimaryKeyOf<ArmGLHistoryByPeriod>.By<ArmGLHistoryByPeriod.ledgerID, ArmGLHistoryByPeriod.branchID, ArmGLHistoryByPeriod.accountID, ArmGLHistoryByPeriod.subID, ArmGLHistoryByPeriod.finPeriodID>
  {
    public static ArmGLHistoryByPeriod Find(
      PXGraph graph,
      int? ledgerID,
      int? branchID,
      int? accountID,
      int? subID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (ArmGLHistoryByPeriod) PrimaryKeyOf<ArmGLHistoryByPeriod>.By<ArmGLHistoryByPeriod.ledgerID, ArmGLHistoryByPeriod.branchID, ArmGLHistoryByPeriod.accountID, ArmGLHistoryByPeriod.subID, ArmGLHistoryByPeriod.finPeriodID>.FindBy(graph, (object) ledgerID, (object) branchID, (object) accountID, (object) subID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ArmGLHistoryByPeriod>.By<ArmGLHistoryByPeriod.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<PX.Objects.GL.Ledger>.By<PX.Objects.GL.Ledger.ledgerID>.ForeignKeyOf<ArmGLHistoryByPeriod>.By<ArmGLHistoryByPeriod.ledgerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ArmGLHistoryByPeriod>.By<ArmGLHistoryByPeriod.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ArmGLHistoryByPeriod>.By<ArmGLHistoryByPeriod.subID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ArmGLHistoryByPeriod.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ArmGLHistoryByPeriod.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ArmGLHistoryByPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ArmGLHistoryByPeriod.subID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ArmGLHistoryByPeriod.lastActivityPeriod>
  {
  }

  public abstract class accountClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArmGLHistoryByPeriod.accountClassID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ArmGLHistoryByPeriod.finPeriodID>
  {
  }

  public abstract class finYear : IBqlField, IBqlOperand
  {
  }
}
