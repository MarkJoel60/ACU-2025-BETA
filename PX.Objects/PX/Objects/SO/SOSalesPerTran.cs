// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSalesPerTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("SO Salesperson Commission")]
[Serializable]
public class SOSalesPerTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _SalespersonID;
  protected int? _RefCntr;
  protected long? _CuryInfoID;
  protected Decimal? _CommnPct;
  protected Decimal? _CuryCommnblAmt;
  protected Decimal? _CommnblAmt;
  protected Decimal? _CuryCommnAmt;
  protected Decimal? _CommnAmt;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SOOrder.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXParent(typeof (Select<SOOrder, Where<SOOrder.orderType, Equal<Current<SOSalesPerTran.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOSalesPerTran.orderNbr>>>>>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [SalesPerson(DirtyRead = true, Enabled = false, IsKey = true)]
  [PXForeignReference(typeof (SOSalesPerTran.FK.SalesPerson))]
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

  [PXDBLong]
  [CurrencyInfo(typeof (SOOrder.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search<CustSalesPeople.commisionPct, Where<CustSalesPeople.bAccountID, Equal<Current<SOOrder.customerID>>, And<CustSalesPeople.locationID, Equal<Current<SOOrder.customerLocationID>>, And<CustSalesPeople.salesPersonID, Equal<Current<SOSalesPerTran.salespersonID>>>>>>, Search<PX.Objects.AR.SalesPerson.commnPct, Where<PX.Objects.AR.SalesPerson.salesPersonID, Equal<Current<SOSalesPerTran.salespersonID>>>>>))]
  [PXUIField(DisplayName = "Commission %")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  [PXDBCurrency(typeof (SOSalesPerTran.curyInfoID), typeof (SOSalesPerTran.commnblAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commissionable Amount", Enabled = false)]
  public virtual Decimal? CuryCommnblAmt
  {
    get => this._CuryCommnblAmt;
    set => this._CuryCommnblAmt = value;
  }

  [PXDBBaseCury(null, null)]
  public virtual Decimal? CommnblAmt
  {
    get => this._CommnblAmt;
    set => this._CommnblAmt = value;
  }

  [PXDBCurrency(typeof (SOSalesPerTran.curyInfoID), typeof (SOSalesPerTran.commnAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commission Amt.", Enabled = false)]
  [PXFormula(typeof (Mult<SOSalesPerTran.curyCommnblAmt, Div<SOSalesPerTran.commnPct, decimal100>>))]
  public virtual Decimal? CuryCommnAmt
  {
    get => this._CuryCommnAmt;
    set => this._CuryCommnAmt = value;
  }

  [PXDBBaseCury(null, null)]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
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

  public class PK : 
    PrimaryKeyOf<SOSalesPerTran>.By<SOSalesPerTran.orderType, SOSalesPerTran.orderNbr, SOSalesPerTran.salespersonID>
  {
    public static SOSalesPerTran Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? salespersonID,
      PKFindOptions options = 0)
    {
      return (SOSalesPerTran) PrimaryKeyOf<SOSalesPerTran>.By<SOSalesPerTran.orderType, SOSalesPerTran.orderNbr, SOSalesPerTran.salespersonID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) salespersonID, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOSalesPerTran>.By<SOSalesPerTran.orderType, SOSalesPerTran.orderNbr>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOSalesPerTran>.By<SOSalesPerTran.orderType>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<SOSalesPerTran>.By<SOSalesPerTran.salespersonID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOSalesPerTran>.By<SOSalesPerTran.curyInfoID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOSalesPerTran.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOSalesPerTran.orderNbr>
  {
  }

  public abstract class salespersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOSalesPerTran.salespersonID>
  {
  }

  public abstract class refCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOSalesPerTran.refCntr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOSalesPerTran.curyInfoID>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOSalesPerTran.commnPct>
  {
  }

  public abstract class curyCommnblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOSalesPerTran.curyCommnblAmt>
  {
  }

  public abstract class commnblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOSalesPerTran.commnblAmt>
  {
  }

  public abstract class curyCommnAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOSalesPerTran.curyCommnAmt>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOSalesPerTran.commnAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOSalesPerTran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOSalesPerTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSalesPerTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSalesPerTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOSalesPerTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSalesPerTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSalesPerTran.lastModifiedDateTime>
  {
  }
}
