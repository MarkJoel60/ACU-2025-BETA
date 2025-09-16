// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRExpenseBalanceByPeriod
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

[PXProjection(typeof (Select5<DRExpenseBalance, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<DRExpenseBalance.finPeriodID>>>, Aggregate<GroupBy<DRExpenseBalance.branchID, GroupBy<DRExpenseBalance.acctID, GroupBy<DRExpenseBalance.subID, GroupBy<DRExpenseBalance.componentID, GroupBy<DRExpenseBalance.vendorID, GroupBy<DRExpenseBalance.projectID, Max<DRExpenseBalance.finPeriodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>>>))]
[PXCacheName("DR Expense Balance by Period")]
[Serializable]
public class DRExpenseBalanceByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AcctID;
  protected int? _SubID;
  protected int? _ComponentID;
  protected int? _VendorID;
  protected int? _ProjectID;
  protected 
  #nullable disable
  string _LastActivityPeriod;
  protected string _FinPeriodID;

  [Branch(null, null, true, true, true, IsKey = true, BqlField = typeof (DRExpenseBalance.branchID))]
  public virtual int? BranchID { get; set; }

  [Account]
  public virtual int? AcctID
  {
    get => this._AcctID;
    set => this._AcctID = value;
  }

  [SubAccount(typeof (DRExpenseBalance.acctID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (DRExpenseBalance.componentID))]
  [PXDefault]
  public virtual int? ComponentID
  {
    get => this._ComponentID;
    set => this._ComponentID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (DRExpenseBalance.vendorID))]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (DRExpenseBalance.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (DRExpenseBalance.finPeriodID))]
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
    PrimaryKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.branchID, DRExpenseBalanceByPeriod.acctID, DRExpenseBalanceByPeriod.subID, DRExpenseBalanceByPeriod.componentID, DRExpenseBalanceByPeriod.vendorID, DRExpenseBalanceByPeriod.projectID, DRExpenseBalanceByPeriod.finPeriodID>
  {
    public static DRExpenseBalanceByPeriod Find(
      PXGraph graph,
      int? branchID,
      int? acctID,
      int? subID,
      int? componentID,
      int? vendorID,
      int? projectID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (DRExpenseBalanceByPeriod) PrimaryKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.branchID, DRExpenseBalanceByPeriod.acctID, DRExpenseBalanceByPeriod.subID, DRExpenseBalanceByPeriod.componentID, DRExpenseBalanceByPeriod.vendorID, DRExpenseBalanceByPeriod.projectID, DRExpenseBalanceByPeriod.finPeriodID>.FindBy(graph, (object) branchID, (object) acctID, (object) subID, (object) componentID, (object) vendorID, (object) projectID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.subID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.componentID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.vendorID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRExpenseBalanceByPeriod>.By<DRExpenseBalanceByPeriod.projectID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalanceByPeriod.branchID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalanceByPeriod.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalanceByPeriod.subID>
  {
  }

  public abstract class componentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRExpenseBalanceByPeriod.componentID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalanceByPeriod.vendorID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalanceByPeriod.projectID>
  {
  }

  public abstract class lastActivityPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRExpenseBalanceByPeriod.lastActivityPeriod>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRExpenseBalanceByPeriod.finPeriodID>
  {
  }
}
