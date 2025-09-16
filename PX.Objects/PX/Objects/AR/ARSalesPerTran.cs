// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPerTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A salesperson commission included in an <see cref="T:PX.Objects.AR.ARInvoice">accounts
/// receivable invoice</see>. The records of this type are created automatically
/// when the user specifies a salesperson ID in an <see cref="T:PX.Objects.AR.ARTran">
/// invoice line</see> on the Invoices and Memos (AR301000) form,
/// which corresponds to the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> graph.
/// </summary>
[PXCacheName("AR Salesperson Commission")]
[Serializable]
public class ARSalesPerTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected int? _SalespersonID;
  protected int? _RefCntr;
  protected int? _AdjNbr;
  protected string _AdjdDocType;
  protected string _AdjdRefNbr;
  protected int? _BranchID;
  protected long? _CuryInfoID;
  protected Decimal? _CommnPct;
  protected Decimal? _CuryCommnblAmt;
  protected Decimal? _CommnblAmt;
  protected Decimal? _CuryCommnAmt;
  protected Decimal? _CommnAmt;
  protected bool? _Released;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _ActuallyUsed;
  protected string _CommnPaymntPeriod;
  protected DateTime? _CommnPaymntDate;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (ARRegister.docType))]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (ARRegister.refNbr))]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARSalesPerTran.docType>>, And<ARRegister.refNbr, Equal<Current<ARSalesPerTran.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXForeignReference(typeof (Field<ARSalesPerTran.salespersonID>.IsRelatedTo<SalesPerson.salesPersonID>))]
  public virtual int? SalespersonID
  {
    get => this._SalespersonID;
    set => this._SalespersonID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? RefCntr
  {
    get => this._RefCntr;
    set => this._RefCntr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  public virtual int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  [PXDBString(3, IsFixed = true, IsKey = true)]
  [PXDefault("UND")]
  public virtual string AdjdDocType
  {
    get => this._AdjdDocType;
    set => this._AdjdDocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault("")]
  public virtual string AdjdRefNbr
  {
    get => this._AdjdRefNbr;
    set => this._AdjdRefNbr = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search<CustSalesPeople.commisionPct, Where<CustSalesPeople.bAccountID, Equal<Current<ARRegister.customerID>>, And<CustSalesPeople.locationID, Equal<Current<ARRegister.customerLocationID>>, And<CustSalesPeople.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>>, Search<SalesPerson.commnPct, Where<SalesPerson.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>))]
  [PXUIField(DisplayName = "Commission %")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  [PXDBCurrency(typeof (ARSalesPerTran.curyInfoID), typeof (ARSalesPerTran.commnblAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commissionable Amount", Enabled = false)]
  public virtual Decimal? CuryCommnblAmt
  {
    get => this._CuryCommnblAmt;
    set => this._CuryCommnblAmt = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnblAmt
  {
    get => this._CommnblAmt;
    set => this._CommnblAmt = value;
  }

  [PXDBCurrency(typeof (ARSalesPerTran.curyInfoID), typeof (ARSalesPerTran.commnAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commission Amt.", Enabled = false)]
  public virtual Decimal? CuryCommnAmt
  {
    get => this._CuryCommnAmt;
    set => this._CuryCommnAmt = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.baseCuryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<ARSalesPerTran.branchID>>>>))]
  public virtual string BaseCuryID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ActuallyUsed
  {
    get => this._ActuallyUsed;
    set => this._ActuallyUsed = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string CommnPaymntPeriod
  {
    get => this._CommnPaymntPeriod;
    set => this._CommnPaymntPeriod = value;
  }

  [PXDBDate]
  public virtual DateTime? CommnPaymntDate
  {
    get => this._CommnPaymntDate;
    set => this._CommnPaymntDate = value;
  }

  /// <exclude />
  public class PK : 
    PrimaryKeyOf<ARSalesPerTran>.By<ARSalesPerTran.docType, ARSalesPerTran.refNbr, ARSalesPerTran.salespersonID, ARSalesPerTran.adjNbr, ARSalesPerTran.adjdDocType, ARSalesPerTran.adjdRefNbr>
  {
    public static ARSalesPerTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? salespersonID,
      int? adjNbr,
      string adjdDocType,
      string adjdRefNbr,
      PKFindOptions options = 0)
    {
      return (ARSalesPerTran) PrimaryKeyOf<ARSalesPerTran>.By<ARSalesPerTran.docType, ARSalesPerTran.refNbr, ARSalesPerTran.salespersonID, ARSalesPerTran.adjNbr, ARSalesPerTran.adjdDocType, ARSalesPerTran.adjdRefNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) salespersonID, (object) adjNbr, (object) adjdDocType, (object) adjdRefNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARSalesPerTran>.By<ARSalesPerTran.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARSalesPerTran>.By<ARSalesPerTran.curyInfoID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPerTran.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPerTran.refNbr>
  {
  }

  public abstract class salespersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPerTran.salespersonID>
  {
  }

  public abstract class refCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPerTran.refCntr>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPerTran.adjNbr>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPerTran.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPerTran.adjdRefNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPerTran.branchID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARSalesPerTran.curyInfoID>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSalesPerTran.commnPct>
  {
  }

  public abstract class curyCommnblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARSalesPerTran.curyCommnblAmt>
  {
  }

  public abstract class commnblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSalesPerTran.commnblAmt>
  {
  }

  public abstract class curyCommnAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARSalesPerTran.curyCommnAmt>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSalesPerTran.commnAmt>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPerTran.baseCuryID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPerTran.released>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARSalesPerTran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARSalesPerTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPerTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPerTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARSalesPerTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPerTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPerTran.lastModifiedDateTime>
  {
  }

  public abstract class actuallyUsed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPerTran.actuallyUsed>
  {
  }

  public abstract class commnPaymntPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPerTran.commnPaymntPeriod>
  {
  }

  public abstract class commnPaymntDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPerTran.commnPaymntDate>
  {
  }
}
