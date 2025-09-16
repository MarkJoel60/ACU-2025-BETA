// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionContent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequisitionContent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReqNbr;
  protected int? _ReqLineNbr;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected Decimal? _ItemQty;
  protected Decimal? _BaseItemQty;
  protected Decimal? _ReqQty;
  protected Decimal? _BaseReqQty;
  protected bool? _RecalcOnly;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBDefault(typeof (RQRequisition.reqNbr))]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (RQRequisitionContent.FK.RequisitionLine))]
  public virtual int? ReqLineNbr
  {
    get => this._ReqLineNbr;
    set => this._ReqLineNbr = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (RQRequisitionContent.FK.RequestLine))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (AddCalc<RQRequestLine.reqQty>))]
  [PXFormula(null, typeof (SubCalc<RQRequestLine.openQty>))]
  public virtual Decimal? ItemQty
  {
    get => this._ItemQty;
    set => this._ItemQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseItemQty
  {
    get => this._BaseItemQty;
    set => this._BaseItemQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (AddCalc<RQRequisitionLine.orderQty>))]
  public virtual Decimal? ReqQty
  {
    get => this._ReqQty;
    set => this._ReqQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReqQty
  {
    get => this._BaseReqQty;
    set => this._BaseReqQty = value;
  }

  [PXBool]
  public virtual bool? RecalcOnly
  {
    get => this._RecalcOnly;
    set => this._RecalcOnly = value;
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
    PrimaryKeyOf<RQRequisitionContent>.By<RQRequisitionContent.reqNbr, RQRequisitionContent.reqLineNbr, RQRequisitionContent.orderNbr, RQRequisitionContent.lineNbr>
  {
    public static RQRequisitionContent Find(
      PXGraph graph,
      string reqNbr,
      int? reqLineNbr,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (RQRequisitionContent) PrimaryKeyOf<RQRequisitionContent>.By<RQRequisitionContent.reqNbr, RQRequisitionContent.reqLineNbr, RQRequisitionContent.orderNbr, RQRequisitionContent.lineNbr>.FindBy(graph, (object) reqNbr, (object) reqLineNbr, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Requisition : 
      PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.ForeignKeyOf<RQRequisitionContent>.By<RQRequisitionContent.reqNbr>
    {
    }

    public class RequisitionLine : 
      PrimaryKeyOf<RQRequisitionLine>.By<RQRequisitionLine.reqNbr, RQRequisitionLine.lineNbr>.ForeignKeyOf<RQRequisitionContent>.By<RQRequisitionContent.reqNbr, RQRequisitionContent.reqLineNbr>
    {
    }

    public class RequestLine : 
      PrimaryKeyOf<RQRequestLine>.By<RQRequestLine.orderNbr, RQRequestLine.lineNbr>.ForeignKeyOf<RQRequisitionContent>.By<RQRequisitionContent.orderNbr, RQRequisitionContent.lineNbr>
    {
    }
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionContent.reqNbr>
  {
  }

  public abstract class reqLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionContent.reqLineNbr>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionContent.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionContent.lineNbr>
  {
  }

  public abstract class itemQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequisitionContent.itemQty>
  {
  }

  public abstract class baseItemQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionContent.baseItemQty>
  {
  }

  public abstract class reqQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequisitionContent.reqQty>
  {
  }

  public abstract class baseReqQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionContent.baseReqQty>
  {
  }

  public abstract class recalcOnly : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequisitionContent.recalcOnly>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQRequisitionContent.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequisitionContent.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionContent.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisitionContent.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQRequisitionContent.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionContent.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequisitionContent.lastModifiedDateTime>
  {
  }
}
