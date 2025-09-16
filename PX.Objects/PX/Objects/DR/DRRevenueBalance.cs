// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRRevenueBalance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.DR;

[PXAccumulator(new Type[] {typeof (DRRevenueBalance.endBalance), typeof (DRRevenueBalance.endProjected), typeof (DRRevenueBalance.endBalance), typeof (DRRevenueBalance.endProjected), typeof (DRRevenueBalance.tranEndBalance), typeof (DRRevenueBalance.tranEndProjected), typeof (DRRevenueBalance.tranEndBalance), typeof (DRRevenueBalance.tranEndProjected)}, new Type[] {typeof (DRRevenueBalance.begBalance), typeof (DRRevenueBalance.begProjected), typeof (DRRevenueBalance.endBalance), typeof (DRRevenueBalance.endProjected), typeof (DRRevenueBalance.tranBegBalance), typeof (DRRevenueBalance.tranBegProjected), typeof (DRRevenueBalance.tranEndBalance), typeof (DRRevenueBalance.tranEndProjected)})]
[PXCacheName("DR Revenue Balance")]
[Serializable]
public class DRRevenueBalance : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AcctID;
  protected int? _SubID;
  protected int? _ComponentID;
  protected int? _CustomerID;
  protected int? _ProjectID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected Decimal? _BegBalance;
  protected Decimal? _BegProjected;
  protected Decimal? _PTDDeferred;
  protected Decimal? _PTDRecognized;
  protected Decimal? _PTDRecognizedSamePeriod;
  protected Decimal? _PTDProjected;
  protected Decimal? _EndBalance;
  protected Decimal? _EndProjected;
  protected byte[] _tstamp;

  [Branch(null, null, true, true, true, IsKey = true)]
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

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? ComponentID
  {
    get => this._ComponentID;
    set => this._ComponentID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXUIField(DisplayName = "FinPeriod", Enabled = false)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Begin Balance")]
  public virtual Decimal? BegBalance
  {
    get => this._BegBalance;
    set => this._BegBalance = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Begin Projected")]
  public virtual Decimal? BegProjected
  {
    get => this._BegProjected;
    set => this._BegProjected = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Deferred Amount")]
  public virtual Decimal? PTDDeferred
  {
    get => this._PTDDeferred;
    set => this._PTDDeferred = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recognized Amount")]
  public virtual Decimal? PTDRecognized
  {
    get => this._PTDRecognized;
    set => this._PTDRecognized = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recognized Amount in Same Period")]
  public virtual Decimal? PTDRecognizedSamePeriod
  {
    get => this._PTDRecognizedSamePeriod;
    set => this._PTDRecognizedSamePeriod = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Amount")]
  public virtual Decimal? PTDProjected
  {
    get => this._PTDProjected;
    set => this._PTDProjected = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "End Balance")]
  public virtual Decimal? EndBalance
  {
    get => this._EndBalance;
    set => this._EndBalance = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "End Projected")]
  public virtual Decimal? EndProjected
  {
    get => this._EndProjected;
    set => this._EndProjected = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Begin Balance")]
  public virtual Decimal? TranBegBalance { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Begin Projected")]
  public virtual Decimal? TranBegProjected { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Deferred Amount")]
  public virtual Decimal? TranPTDDeferred { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recognized Amount")]
  public virtual Decimal? TranPTDRecognized { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recognized Amount in Same Period")]
  public virtual Decimal? TranPTDRecognizedSamePeriod { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Projected Amount")]
  public virtual Decimal? TranPTDProjected { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "End Balance")]
  public virtual Decimal? TranEndBalance { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "End Projected")]
  public virtual Decimal? TranEndProjected { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<DRRevenueBalance>.By<DRRevenueBalance.branchID, DRRevenueBalance.acctID, DRRevenueBalance.subID, DRRevenueBalance.componentID, DRRevenueBalance.customerID, DRRevenueBalance.projectID, DRRevenueBalance.finPeriodID>
  {
    public static DRRevenueBalance Find(
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
      return (DRRevenueBalance) PrimaryKeyOf<DRRevenueBalance>.By<DRRevenueBalance.branchID, DRRevenueBalance.acctID, DRRevenueBalance.subID, DRRevenueBalance.componentID, DRRevenueBalance.customerID, DRRevenueBalance.projectID, DRRevenueBalance.finPeriodID>.FindBy(graph, (object) branchID, (object) acctID, (object) subID, (object) componentID, (object) customerID, (object) projectID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRRevenueBalance>.By<DRRevenueBalance.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRRevenueBalance>.By<DRRevenueBalance.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRRevenueBalance>.By<DRRevenueBalance.subID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRRevenueBalance>.By<DRRevenueBalance.componentID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<DRRevenueBalance>.By<DRRevenueBalance.customerID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRRevenueBalance>.By<DRRevenueBalance.projectID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance.branchID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance.subID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance.componentID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance.customerID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance.projectID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRRevenueBalance.finPeriodID>
  {
  }

  public abstract class begBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRRevenueBalance.begBalance>
  {
  }

  public abstract class begProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance.begProjected>
  {
  }

  public abstract class pTDDeferred : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance.pTDDeferred>
  {
  }

  public abstract class pTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance.pTDRecognized>
  {
  }

  public abstract class pTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance.pTDRecognizedSamePeriod>
  {
  }

  public abstract class pTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance.pTDProjected>
  {
  }

  public abstract class endBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  DRRevenueBalance.endBalance>
  {
  }

  public abstract class endProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance.endProjected>
  {
  }

  public abstract class tranBegBalance : IBqlField, IBqlOperand
  {
  }

  public abstract class tranBegProjected : IBqlField, IBqlOperand
  {
  }

  public abstract class tranPTDDeferred : IBqlField, IBqlOperand
  {
  }

  public abstract class tranPTDRecognized : IBqlField, IBqlOperand
  {
  }

  public abstract class tranPTDRecognizedSamePeriod : IBqlField, IBqlOperand
  {
  }

  public abstract class tranPTDProjected : IBqlField, IBqlOperand
  {
  }

  public abstract class tranEndBalance : IBqlField, IBqlOperand
  {
  }

  public abstract class tranEndProjected : IBqlField, IBqlOperand
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DRRevenueBalance.Tstamp>
  {
  }
}
