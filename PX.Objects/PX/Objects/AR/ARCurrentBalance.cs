// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCurrentBalance
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (SelectFromMirror<ARBalances, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.released, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.branchID, Equal<ARBalances.branchID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.customerID, Equal<ARBalances.customerID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.customerLocationID, Equal<ARBalances.customerLocationID>>>>>.And<BqlOperand<ARRegister.docBal, IBqlDecimal>.IsNotEqual<Zero>>>>>>>>.LeftJoin<ARTranPostGL>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.docType, Equal<ARRegister.docType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.refNbr, Equal<ARRegister.refNbr>>>>>.And<BqlOperand<ARTranPostGL.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>>>), Persistent = false)]
[PXHidden]
public class ARCurrentBalance : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlTable = typeof (ARBalances))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBDecimal(BqlTable = typeof (ARBalances))]
  public virtual Decimal? CurrentBal { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, decimal1>, decimal_1>), typeof (Decimal))]
  public virtual Decimal? BalanceSign { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>, ARTranPostGL.balanceAmt>, BqlOperand<ARRegister.docBal, IBqlDecimal>.Multiply<ARCurrentBalance.balanceSign>>), typeof (Decimal))]
  public virtual Decimal? CurrentBalSigned { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARCurrentBalance>.By<ARCurrentBalance.branchID, ARCurrentBalance.customerID, ARCurrentBalance.customerLocationID>
  {
    public static ARCurrentBalance Find(
      PXGraph graph,
      int branchID,
      int? customerID,
      int? customerLocationID,
      PKFindOptions options = 0)
    {
      return (ARCurrentBalance) PrimaryKeyOf<ARCurrentBalance>.By<ARCurrentBalance.branchID, ARCurrentBalance.customerID, ARCurrentBalance.customerLocationID>.FindBy(graph, (object) branchID, (object) customerID, (object) customerLocationID, options);
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCurrentBalance.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCurrentBalance.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCurrentBalance.customerLocationID>
  {
  }

  public abstract class currentBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCurrentBalance.currentBal>
  {
  }

  public abstract class balanceSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalance.balanceSign>
  {
  }

  public abstract class currentBalSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCurrentBalance.currentBalSigned>
  {
  }
}
