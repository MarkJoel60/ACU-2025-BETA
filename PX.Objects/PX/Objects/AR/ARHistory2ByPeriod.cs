// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistory2ByPeriod
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

[PXHidden]
[PXProjection(typeof (Select5<ARHistory, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<ARHistory.finPeriodID>>>, Aggregate<GroupBy<ARHistory.branchID, GroupBy<ARHistory.customerID, GroupBy<ARHistory.accountID, GroupBy<ARHistory.subID, Max<ARHistory.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>))]
[PXCacheName("AR History by Period")]
[Serializable]
public class ARHistory2ByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _CustomerID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _LastActivityPeriod;
  protected string _FinPeriodID;

  [Branch(null, null, true, true, true, IsKey = true, BqlField = typeof (ARHistory.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Customer(IsKey = true, BqlField = typeof (ARHistory.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [Account(IsKey = true, BqlField = typeof (ARHistory.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true, BqlField = typeof (ARHistory.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (ARHistory.finPeriodID))]
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
  ARHistory2ByPeriod.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory2ByPeriod.customerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory2ByPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistory2ByPeriod.subID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistory2ByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistory2ByPeriod.finPeriodID>
  {
  }
}
