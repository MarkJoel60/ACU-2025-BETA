// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BaseAPHistoryByPeriod
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

/// <summary>
/// A projection DAC over <see cref="T:PX.Objects.AP.APHistory" /> that is intended to close the gaps
/// in AP history records. (The gaps in AP history records appear if AR history records
/// do not exist for every financial period defined in the system.) That is, the purpose
/// of this DAC is to calculate the <see cref="P:PX.Objects.AP.BaseAPHistoryByPeriod.LastActivityPeriod">last activity period</see>
/// for every existing <see cref="T:PX.Objects.GL.FinPeriods.MasterFinPeriod">financial period</see>, so that inquiries and reports
/// that produce information for a given financial period can look at the latest available
/// <see cref="T:PX.Objects.AP.APHistory" /> record. For example, this projection is used in the Vendor
/// Summary (AP401000) form, which corresponds to the <see cref="T:PX.Objects.AP.APVendorBalanceEnq" /> graph.
/// </summary>
[PXCacheName("Base AP History by Period")]
[PXProjection(typeof (Select5<APHistory, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<APHistory.finPeriodID>>>, Aggregate<GroupBy<APHistory.branchID, GroupBy<APHistory.vendorID, GroupBy<APHistory.accountID, GroupBy<APHistory.subID, Max<APHistory.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>))]
[PXPrimaryGraph(new System.Type[] {typeof (APDocumentEnq), typeof (APVendorBalanceEnq)}, new System.Type[] {typeof (Where<BaseAPHistoryByPeriod.vendorID, PX.Data.IsNotNull>), typeof (Where<BaseAPHistoryByPeriod.vendorID, PX.Data.IsNull>)}, Filters = new System.Type[] {typeof (APDocumentEnq.APDocumentFilter), typeof (APVendorBalanceEnq.APHistoryFilter)})]
[Serializable]
public class BaseAPHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXUIField(DisplayName = "Branch")]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Vendor(IsKey = true, BqlField = typeof (APHistory.vendorID), CacheGlobal = true)]
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
  [PXUIField(DisplayName = "Last Activity Period")]
  public virtual string LastActivityPeriod
  {
    get => this._LastActivityPeriod;
    set => this._LastActivityPeriod = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BaseAPHistoryByPeriod.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BaseAPHistoryByPeriod.vendorID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BaseAPHistoryByPeriod.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BaseAPHistoryByPeriod.subID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BaseAPHistoryByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BaseAPHistoryByPeriod.finPeriodID>
  {
  }
}
