// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCurrentBalanceOpenOrders
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<ARBalances, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.branchID, Equal<ARBalances.branchID>, And<PX.Objects.SO.SOOrder.customerID, Equal<ARBalances.customerID>, And<PX.Objects.SO.SOOrder.customerLocationID, Equal<ARBalances.customerLocationID>, And<PX.Objects.SO.SOOrder.inclCustOpenOrders, Equal<True>, And<PX.Objects.SO.SOOrder.cancelled, Equal<False>, And<PX.Objects.SO.SOOrder.hold, Equal<False>, And<PX.Objects.SO.SOOrder.creditHold, Equal<False>, And<PX.Objects.SO.SOOrder.unbilledOrderTotal, NotEqual<Zero>>>>>>>>>, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>>>>), Persistent = false)]
[PXHidden]
public class ARCurrentBalanceOpenOrders : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<SOOrderType.aRDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<SOOrderType.aRDocType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? BalanceSign { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<PX.Objects.SO.SOOrder.unbilledOrderTotal, IBqlDecimal>.Multiply<ARCurrentBalanceOpenOrders.balanceSign>), typeof (Decimal))]
  public virtual Decimal? UnbilledOrderTotal { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARCurrentBalanceOpenOrders>.By<ARCurrentBalanceOpenOrders.branchID, ARCurrentBalanceOpenOrders.customerID, ARCurrentBalanceOpenOrders.customerLocationID>
  {
    public static ARCurrentBalanceOpenOrders Find(
      PXGraph graph,
      int branchID,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARCurrentBalanceOpenOrders) PrimaryKeyOf<ARCurrentBalanceOpenOrders>.By<ARCurrentBalanceOpenOrders.branchID, ARCurrentBalanceOpenOrders.customerID, ARCurrentBalanceOpenOrders.customerLocationID>.FindBy(graph, (object) branchID, (object) customerID, (object) customerLocationID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCurrentBalanceOpenOrders.branchID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrders.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrders.customerLocationID>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrders.balanceSign>
  {
  }

  public abstract class unbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceOpenOrders.unbilledOrderTotal>
  {
  }
}
