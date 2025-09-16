// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistoryByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A projection DAC over <see cref="T:PX.Objects.AR.CuryARHistory" /> that is intended to close the gaps
/// in AR history records. (The gaps in AR history records appear if AR history records do
/// not exist for every financial period defined in the system.) That is, the purpose of
/// this DAC is to calculate the <see cref="P:PX.Objects.AR.ARHistoryByPeriod.LastActivityPeriod">last activity period</see>
/// for every existing <see cref="T:PX.Objects.GL.FinPeriods.MasterFinPeriod">financial period</see>, so that inquiries and
/// reports that produce information for a given financial period can look at the latest
/// available <see cref="T:PX.Objects.AR.CuryARHistory" /> record. For example, this projection is
/// used in the Customer Summary (AR401000) form, which corresponds to the
/// <see cref="T:PX.Objects.AR.ARCustomerBalanceEnq" /> graph.
/// </summary>
[PXProjection(typeof (Select5<CuryARHistory, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<CuryARHistory.finPeriodID>>>, Aggregate<GroupBy<CuryARHistory.branchID, GroupBy<CuryARHistory.customerID, GroupBy<CuryARHistory.accountID, GroupBy<CuryARHistory.subID, GroupBy<CuryARHistory.curyID, Max<CuryARHistory.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>>))]
[PXCacheName("AR History by Period")]
[Serializable]
public class ARHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _CustomerID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _CuryID;
  protected string _LastActivityPeriod;
  protected string _FinPeriodID;

  [Branch(null, null, true, true, true, IsKey = true, BqlField = typeof (CuryARHistory.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Customer(IsKey = true, BqlField = typeof (CuryARHistory.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [Account(IsKey = true, BqlField = typeof (CuryARHistory.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true, BqlField = typeof (CuryARHistory.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL", BqlField = typeof (CuryARHistory.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (CuryARHistory.finPeriodID))]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryByPeriod.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryByPeriod.customerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryByPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryByPeriod.subID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARHistoryByPeriod.curyID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistoryByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistoryByPeriod.finPeriodID>
  {
  }
}
