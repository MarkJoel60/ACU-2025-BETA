// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRRevenueBalanceByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.DR;

[PXProjection(typeof (Select5<DRRevenueBalance, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<DRRevenueBalance.finPeriodID>>>, Aggregate<GroupBy<DRRevenueBalance.branchID, GroupBy<DRRevenueBalance.acctID, GroupBy<DRRevenueBalance.subID, GroupBy<DRRevenueBalance.componentID, GroupBy<DRRevenueBalance.customerID, GroupBy<DRRevenueBalance.projectID, Max<DRRevenueBalance.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>>>))]
[PXCacheName("DR Revenue Balance by Period")]
[Serializable]
public class DRRevenueBalanceByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AcctID;
  protected int? _SubID;
  protected int? _ComponentID;
  protected int? _CustomerID;
  protected int? _ProjectID;
  protected 
  #nullable disable
  string _LastActivityPeriod;
  protected string _FinPeriodID;

  [Branch(null, null, true, true, true, IsKey = true, BqlField = typeof (DRRevenueBalance.branchID))]
  public virtual int? BranchID { get; set; }

  [Account]
  public virtual int? AcctID
  {
    get => this._AcctID;
    set => this._AcctID = value;
  }

  [SubAccount(typeof (DRRevenueBalance.acctID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (DRRevenueBalance.componentID))]
  [PXDefault]
  public virtual int? ComponentID
  {
    get => this._ComponentID;
    set => this._ComponentID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (DRRevenueBalance.customerID))]
  [PXDefault]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (DRRevenueBalance.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (DRRevenueBalance.finPeriodID))]
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

  public class PK : 
    PrimaryKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.branchID, DRRevenueBalanceByPeriod.acctID, DRRevenueBalanceByPeriod.subID, DRRevenueBalanceByPeriod.componentID, DRRevenueBalanceByPeriod.customerID, DRRevenueBalanceByPeriod.projectID, DRRevenueBalanceByPeriod.finPeriodID>
  {
    public static DRRevenueBalanceByPeriod Find(
      PXGraph graph,
      int? branchID,
      int? acctID,
      int? subID,
      int? componentID,
      int? customerID,
      int? projectID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (DRRevenueBalanceByPeriod) PrimaryKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.branchID, DRRevenueBalanceByPeriod.acctID, DRRevenueBalanceByPeriod.subID, DRRevenueBalanceByPeriod.componentID, DRRevenueBalanceByPeriod.customerID, DRRevenueBalanceByPeriod.projectID, DRRevenueBalanceByPeriod.finPeriodID>.FindBy(graph, (object) branchID, (object) acctID, (object) subID, (object) componentID, (object) customerID, (object) projectID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.subID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.componentID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.customerID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRRevenueBalanceByPeriod>.By<DRRevenueBalanceByPeriod.projectID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalanceByPeriod.branchID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalanceByPeriod.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalanceByPeriod.subID>
  {
  }

  public abstract class componentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRevenueBalanceByPeriod.componentID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalanceByPeriod.customerID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalanceByPeriod.projectID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRRevenueBalanceByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRRevenueBalanceByPeriod.finPeriodID>
  {
  }
}
