// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommnHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents aggregate historical information about commissions calculated
/// for a given salesperson in a given financial period, additionally broken
/// down by branch, customer, and customer location. The records of this type
/// are created by the Calculate Commissions process, which corresponds to the
/// <see cref="T:PX.Objects.AR.ARSPCommissionProcess" /> graph.
/// </summary>
[PXCacheName("AR Salesperson Commission History")]
[Serializable]
public class ARSPCommnHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SalesPersonID;
  protected 
  #nullable disable
  string _CommnPeriod;
  protected int? _BranchID;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected Decimal? _CommnAmt;
  protected Decimal? _CommnblAmt;
  protected byte[] _tstamp;

  [PXDefault]
  [SalesPerson(DescriptionField = typeof (SalesPerson.descr), IsKey = true)]
  [PXForeignReference(typeof (Field<ARSPCommnHistory.salesPersonID>.IsRelatedTo<SalesPerson.salesPersonID>))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDefault]
  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXUIField(DisplayName = "Commission Period")]
  public virtual string CommnPeriod
  {
    get => this._CommnPeriod;
    set => this._CommnPeriod = value;
  }

  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXForeignReference(typeof (CompositeKey<Field<ARSPCommnHistory.customerID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<ARSPCommnHistory.customerLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commission Amount")]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commissionable Amount")]
  public virtual Decimal? CommnblAmt
  {
    get => this._CommnblAmt;
    set => this._CommnblAmt = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? PRProcessedDate { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.baseCuryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<ARSPCommnHistory.branchID>>>>))]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
  public virtual string BaseCuryID { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUIField]
  [BAccountType.SalesPersonTypeList]
  [Obsolete("It is obsolete Payroll field and will be removed in 2019R1")]
  public virtual string Type { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <exclude />
  public class PK : 
    PrimaryKeyOf<ARSPCommnHistory>.By<ARSPCommnHistory.branchID, ARSPCommnHistory.salesPersonID, ARSPCommnHistory.commnPeriod, ARSPCommnHistory.customerID, ARSPCommnHistory.customerLocationID>
  {
    public static ARSPCommnHistory Find(
      PXGraph graph,
      int? branchID,
      int? salesPersonID,
      string commnPeriod,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARSPCommnHistory) PrimaryKeyOf<ARSPCommnHistory>.By<ARSPCommnHistory.branchID, ARSPCommnHistory.salesPersonID, ARSPCommnHistory.commnPeriod, ARSPCommnHistory.customerID, ARSPCommnHistory.customerLocationID>.FindBy(graph, (object) branchID, (object) salesPersonID, (object) commnPeriod, (object) customerID, (object) customerLocationID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARSPCommnHistory>.By<ARSPCommnHistory.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARSPCommnHistory>.By<ARSPCommnHistory.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ARSPCommnHistory>.By<ARSPCommnHistory.customerID, ARSPCommnHistory.customerLocationID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<SalesPerson>.By<SalesPerson.salesPersonID>.ForeignKeyOf<ARSPCommnHistory>.By<ARSPCommnHistory.salesPersonID>
    {
    }
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSPCommnHistory.salesPersonID>
  {
  }

  public abstract class commnPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommnHistory.commnPeriod>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSPCommnHistory.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSPCommnHistory.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSPCommnHistory.customerLocationID>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSPCommnHistory.commnAmt>
  {
  }

  public abstract class commnblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSPCommnHistory.commnblAmt>
  {
  }

  public abstract class pRProcessedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSPCommnHistory.pRProcessedDate>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommnHistory.baseCuryID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommnHistory.type>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARSPCommnHistory.Tstamp>
  {
  }
}
