// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryByPeriodCurrent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXProjection(typeof (Select5<GLHistory, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<GLHistory.finPeriodID>>, InnerJoin<Branch, On<GLHistory.branchID, Equal<Branch.branchID>>, InnerJoin<OrganizationFinPeriodCurrent, On<OrganizationFinPeriodCurrent.masterFinPeriodID, Equal<CurrentValue<GLHistoryFilter.finPeriodID>>, And<MasterFinPeriod.finPeriodID, Equal<OrganizationFinPeriodCurrent.prevFinPeriodID>, And<Branch.organizationID, Equal<OrganizationFinPeriodCurrent.organizationID>>>>>>>, Aggregate<Max<GLHistory.finPeriodID, GroupBy<GLHistory.branchID, GroupBy<GLHistory.ledgerID, GroupBy<GLHistory.accountID, GroupBy<GLHistory.subID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>))]
[GLHistoryPrimaryGraph]
[PXCacheName("GL History by Period")]
[Serializable]
public class GLHistoryByPeriodCurrent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, which the history record belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BAccountID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (Branch.branchID), SubstituteKey = typeof (Branch.branchCD))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, which the history record belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.ledgerID))]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (Ledger.ledgerID), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
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

  /// <summary>
  /// Identifier of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial Period</see>, for which the history data is given.
  /// </summary>
  [PXUIField]
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  public virtual string FinPeriodID { get; set; }

  [PXString]
  public virtual string FinYear
  {
    get => this.FinPeriodID?.Substring(0, 4);
    set
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriodCurrent.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriodCurrent.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriodCurrent.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryByPeriodCurrent.subID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryByPeriodCurrent.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryByPeriodCurrent.finPeriodID>
  {
  }

  public abstract class finYear : IBqlField, IBqlOperand
  {
  }
}
