// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.APHistoryLastRevaluation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// A view into the <see cref="T:PX.Objects.AP.CuryAPHistory" /> entity, used by the Revalue AP History process
/// to determine the most recent period when revaluation of a particular account and subaccount
/// was performed (see <see cref="M:PX.Objects.CM.RevalueAPAccounts.apaccountlist" />).
/// </summary>
[PXProjection(typeof (Select4<CuryAPHistory, Where<CuryAPHistory.finPtdRevalued, NotEqual<decimal0>>, Aggregate<GroupBy<CuryAPHistory.branchID, GroupBy<CuryAPHistory.vendorID, GroupBy<CuryAPHistory.accountID, GroupBy<CuryAPHistory.subID, GroupBy<CuryAPHistory.curyID, Max<CuryAPHistory.finPeriodID>>>>>>>>))]
[Serializable]
public class APHistoryLastRevaluation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _VendorID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _CuryID;
  protected string _LastActivityPeriod;

  [Branch(null, null, true, true, true, IsKey = true, BqlField = typeof (CuryAPHistory.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Vendor(IsKey = true, BqlField = typeof (CuryAPHistory.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [Account(IsKey = true, BqlField = typeof (CuryAPHistory.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true, BqlField = typeof (CuryAPHistory.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL", BqlField = typeof (CuryAPHistory.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (CuryAPHistory.finPeriodID))]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryLastRevaluation.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryLastRevaluation.vendorID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryLastRevaluation.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryLastRevaluation.subID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APHistoryLastRevaluation.curyID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APHistoryLastRevaluation.lastActivityPeriod>
  {
  }
}
