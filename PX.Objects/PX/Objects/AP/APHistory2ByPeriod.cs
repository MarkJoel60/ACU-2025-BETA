// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APHistory2ByPeriod
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
namespace PX.Objects.AP;

[PXHidden]
[PXProjection(typeof (Select5<APHistory, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<APHistory.finPeriodID>>>, Aggregate<GroupBy<APHistory.branchID, GroupBy<APHistory.vendorID, GroupBy<APHistory.accountID, GroupBy<APHistory.subID, Max<APHistory.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>))]
[PXPrimaryGraph(new System.Type[] {typeof (APDocumentEnq), typeof (APVendorBalanceEnq)}, new System.Type[] {typeof (Where<APHistoryByPeriod.vendorID, PX.Data.IsNotNull>), typeof (Where<APHistoryByPeriod.vendorID, PX.Data.IsNull>)}, Filters = new System.Type[] {typeof (APDocumentEnq.APDocumentFilter), typeof (APVendorBalanceEnq.APHistoryFilter)})]
[PXCacheName("AP History by Period")]
[Serializable]
public class APHistory2ByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _VendorID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _LastActivityPeriod;
  protected string _FinPeriodID;

  [PXDBInt(IsKey = true, BqlField = typeof (APHistory.branchID))]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Vendor(IsKey = true, BqlField = typeof (APHistory.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [Account(IsKey = true, BqlField = typeof (APHistory.accountID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true, BqlField = typeof (APHistory.subID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (APHistory.finPeriodID))]
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
  APHistory2ByPeriod.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistory2ByPeriod.vendorID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistory2ByPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistory2ByPeriod.subID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APHistory2ByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APHistory2ByPeriod.finPeriodID>
  {
  }
}
