// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.ARHistoryLastRevaluation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// A view into the <see cref="T:PX.Objects.AR.CuryARHistory" /> entity, used by the Revalue AR History process
/// to determine the most recent period when revaluation of a particular account and subaccount
/// was performed (see <see cref="M:PX.Objects.CM.RevalueARAccounts.araccountlist" />).
/// </summary>
[PXProjection(typeof (Select4<CuryARHistory, Where<CuryARHistory.finPtdRevalued, NotEqual<decimal0>>, Aggregate<GroupBy<CuryARHistory.branchID, GroupBy<CuryARHistory.customerID, GroupBy<CuryARHistory.accountID, GroupBy<CuryARHistory.subID, GroupBy<CuryARHistory.curyID, Max<CuryARHistory.finPeriodID>>>>>>>>))]
[Serializable]
public class ARHistoryLastRevaluation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _CustomerID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _CuryID;
  protected string _LastActivityPeriod;

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

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (CuryARHistory.finPeriodID))]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryLastRevaluation.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryLastRevaluation.customerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryLastRevaluation.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryLastRevaluation.subID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARHistoryLastRevaluation.curyID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistoryLastRevaluation.lastActivityPeriod>
  {
  }
}
