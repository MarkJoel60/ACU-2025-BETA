// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCurrentBalanceUnreleased
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select2<ARBalances, LeftJoin<ARRegister, On<ARRegister.branchID, Equal<ARBalances.branchID>, And<ARRegister.customerID, Equal<ARBalances.customerID>, And<ARRegister.customerLocationID, Equal<ARBalances.customerLocationID>, And<ARRegister.released, Equal<False>, And<ARRegister.hold, Equal<False>, And<ARRegister.scheduled, Equal<False>, And<ARRegister.voided, Equal<False>, And<ARRegister.docBal, NotEqual<Zero>>>>>>>>>, LeftJoin<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>>>, Where<ARInvoice.docType, IsNull, Or<ARInvoice.creditHold, Equal<False>, And<ARInvoice.pendingProcessing, Equal<False>>>>>), Persistent = false)]
[PXHidden]
public class ARCurrentBalanceUnreleased : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARBalances))]
  public virtual Decimal? UnreleasedBal { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.invoice, ARDocType.debitMemo, ARDocType.refund, ARDocType.voidRefund, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.creditMemo, ARDocType.smallBalanceWO>>, decimal_1>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? BalanceSign { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<ARRegister.origDocAmt, IBqlDecimal>.Multiply<ARCurrentBalanceUnreleased.balanceSign>), typeof (Decimal))]
  public virtual Decimal? UnreleasedBalSigned { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARCurrentBalanceUnreleased>.By<ARCurrentBalanceUnreleased.branchID, ARCurrentBalanceUnreleased.customerID, ARCurrentBalanceUnreleased.customerLocationID>
  {
    public static ARCurrentBalanceUnreleased Find(
      PXGraph graph,
      int branchID,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARCurrentBalanceUnreleased) PrimaryKeyOf<ARCurrentBalanceUnreleased>.By<ARCurrentBalanceUnreleased.branchID, ARCurrentBalanceUnreleased.customerID, ARCurrentBalanceUnreleased.customerLocationID>.FindBy(graph, (object) branchID, (object) customerID, (object) customerLocationID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCurrentBalanceUnreleased.branchID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceUnreleased.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalanceUnreleased.customerLocationID>
  {
  }

  public abstract class unreleasedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceUnreleased.unreleasedBal>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceUnreleased.balanceSign>
  {
  }

  public abstract class unreleasedBalSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalanceUnreleased.unreleasedBalSigned>
  {
  }
}
