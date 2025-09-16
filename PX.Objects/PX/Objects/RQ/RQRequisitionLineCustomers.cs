// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionLineCustomers
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXProjection(typeof (Select5<RQRequisitionContent, LeftJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequisitionContent.orderNbr>>, LeftJoin<RQRequestClass, On<RQRequestClass.reqClassID, Equal<RQRequest.reqClassID>>>>, Where<RQRequestClass.customerRequest, Equal<boolTrue>>, Aggregate<GroupBy<RQRequisitionContent.reqNbr, GroupBy<RQRequisitionContent.reqLineNbr, GroupBy<RQRequestClass.customerRequest, Sum<RQRequisitionContent.reqQty>>>>>>), Persistent = false)]
[PXHidden]
[Serializable]
public class RQRequisitionLineCustomers : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReqNbr;
  protected int? _ReqLineNbr;
  protected bool? _CustomerRequest;
  protected Decimal? _ReqQty;

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (RQRequisitionContent.reqNbr))]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDBInt(BqlField = typeof (RQRequisitionContent.reqLineNbr))]
  public virtual int? ReqLineNbr
  {
    get => this._ReqLineNbr;
    set => this._ReqLineNbr = value;
  }

  [PXDBBool(BqlField = typeof (RQRequestClass.customerRequest))]
  public virtual bool? CustomerRequest
  {
    get => this._CustomerRequest;
    set => this._CustomerRequest = value;
  }

  [PXDBDecimal(2, BqlField = typeof (RQRequisitionContent.reqQty))]
  public virtual Decimal? ReqQty
  {
    get => this._ReqQty;
    set => this._ReqQty = value;
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLineCustomers.reqNbr>
  {
  }

  public abstract class reqLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisitionLineCustomers.reqLineNbr>
  {
  }

  public abstract class customerRequest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequisitionLineCustomers.customerRequest>
  {
  }

  public abstract class reqQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineCustomers.reqQty>
  {
  }
}
