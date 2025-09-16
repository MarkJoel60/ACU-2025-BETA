// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionLineReceived
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequisitionLineReceived : RQRequisitionLine
{
  protected Decimal? _POOrderQty;
  protected Decimal? _POReceivedQty;
  protected 
  #nullable disable
  string _Status;

  [PXDBDecimal]
  [PXUIField]
  public override Decimal? OrderQty
  {
    get => base.OrderQty;
    set => base.OrderQty = value;
  }

  [PXDecimal]
  [PXDBScalar(typeof (FbqlSelect<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.rQReqNbr, Equal<RQRequisitionLine.reqNbr>>>>>.And<BqlOperand<POLine.rQReqLineNbr, IBqlInt>.IsEqual<RQRequisitionLine.lineNbr>>>.Aggregate<To<GroupBy<POLine.rQReqNbr>, GroupBy<POLine.rQReqLineNbr>, Sum<POLine.orderQty>>>, POLine>.SearchFor<POLine.orderQty>))]
  [PXUIField]
  public virtual Decimal? POOrderQty
  {
    get => this._POOrderQty;
    set => this._POOrderQty = value;
  }

  [PXDecimal]
  [PXDBScalar(typeof (FbqlSelect<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.rQReqNbr, Equal<RQRequisitionLine.reqNbr>>>>>.And<BqlOperand<POLine.rQReqLineNbr, IBqlInt>.IsEqual<RQRequisitionLine.lineNbr>>>.Aggregate<To<GroupBy<POLine.rQReqNbr>, GroupBy<POLine.rQReqLineNbr>, Sum<POLine.receivedQty>>>, POLine>.SearchFor<POLine.receivedQty>))]
  [PXUIField]
  public virtual Decimal? POReceivedQty
  {
    get => this._POReceivedQty;
    set => this._POReceivedQty = value;
  }

  [PXString]
  [RQRequisitionReceivedStatus.List]
  [PXFormula(typeof (SwitchMirror<IBqlString, TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<RQRequisitionLineReceived.orderQty, IBqlDecimal>.IsLessEqual<RQRequisitionLineReceived.pOReceivedQty>>, RQRequisitionReceivedStatus.closed>>, Case<Where<BqlOperand<RQRequisitionLineReceived.pOReceivedQty, IBqlDecimal>.IsGreater<decimal0>>, RQRequisitionReceivedStatus.partially>, RQRequisitionReceivedStatus.ordered>.When<BqlOperand<RQRequisitionLineReceived.pOOrderQty, IBqlDecimal>.IsGreater<decimal0>>.Else<RQRequisitionReceivedStatus.open>))]
  [PXUIField]
  public string Status { get; set; }

  public new abstract class reqNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLineReceived.reqNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLineReceived.lineNbr>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisitionLineReceived.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisitionLineReceived.subItemID>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLineReceived.description>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLineReceived.uOM>
  {
  }

  public new abstract class orderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineReceived.orderQty>
  {
  }

  public abstract class pOOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineReceived.pOOrderQty>
  {
  }

  public abstract class pOReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineReceived.pOReceivedQty>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLineReceived.status>
  {
  }
}
