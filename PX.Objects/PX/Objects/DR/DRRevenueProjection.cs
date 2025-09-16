// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRRevenueProjection
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

[PXCacheName("DR Revenue Projection")]
[Serializable]
public class DRRevenueProjection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AcctID;
  protected int? _SubID;
  protected int? _ComponentID;
  protected int? _CustomerID;
  protected int? _ProjectID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected Decimal? _PTDProjected;
  protected Decimal? _PTDRecognized;
  protected Decimal? _PTDRecognizedSamePeriod;
  protected byte[] _tstamp;

  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID { get; set; }

  [Account]
  public virtual int? AcctID
  {
    get => this._AcctID;
    set => this._AcctID = value;
  }

  [SubAccount(typeof (DRRevenueProjection.acctID))]
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
  [PXUIField(DisplayName = "Projected Amount")]
  public virtual Decimal? PTDProjected
  {
    get => this._PTDProjected;
    set => this._PTDProjected = value;
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
  public virtual Decimal? TranPTDProjected { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recognized Amount")]
  public virtual Decimal? TranPTDRecognized { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recognized Amount in Same Period")]
  public virtual Decimal? TranPTDRecognizedSamePeriod { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<DRRevenueProjection>.By<DRRevenueProjection.branchID, DRRevenueProjection.acctID, DRRevenueProjection.subID, DRRevenueProjection.componentID, DRRevenueProjection.customerID, DRRevenueProjection.projectID, DRRevenueProjection.finPeriodID>
  {
    public static DRRevenueProjection Find(
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
      return (DRRevenueProjection) PrimaryKeyOf<DRRevenueProjection>.By<DRRevenueProjection.branchID, DRRevenueProjection.acctID, DRRevenueProjection.subID, DRRevenueProjection.componentID, DRRevenueProjection.customerID, DRRevenueProjection.projectID, DRRevenueProjection.finPeriodID>.FindBy(graph, (object) branchID, (object) acctID, (object) subID, (object) componentID, (object) customerID, (object) projectID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRRevenueProjection>.By<DRRevenueProjection.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRRevenueProjection>.By<DRRevenueProjection.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRRevenueProjection>.By<DRRevenueProjection.subID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRRevenueProjection>.By<DRRevenueProjection.componentID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<DRRevenueProjection>.By<DRRevenueProjection.customerID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRRevenueProjection>.By<DRRevenueProjection.projectID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjection.branchID>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjection.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjection.subID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjection.componentID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjection.customerID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjection.projectID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRRevenueProjection.finPeriodID>
  {
  }

  public abstract class pTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueProjection.pTDProjected>
  {
  }

  public abstract class pTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueProjection.pTDRecognized>
  {
  }

  public abstract class pTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueProjection.pTDRecognizedSamePeriod>
  {
  }

  public abstract class tranPTDProjected : IBqlField, IBqlOperand
  {
  }

  public abstract class tranPTDRecognized : IBqlField, IBqlOperand
  {
  }

  public abstract class tranPTDRecognizedSamePeriod : IBqlField, IBqlOperand
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  DRRevenueProjection.Tstamp>
  {
  }
}
