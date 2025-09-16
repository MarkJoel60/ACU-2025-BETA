// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[ARRegisterCacheName("AR Document")]
[Serializable]
public class ARRegisterReport : ARRegister
{
  [PXDecimal]
  [PXDependsOnFields(new Type[] {typeof (ARRegisterReport.docType)})]
  [PXFormula(typeof (Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.prepaymentInvoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>))]
  [PXDBCalced(typeof (Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.prepaymentInvoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>), typeof (Decimal))]
  public override Decimal? SignBalance { get; set; }

  [PXDecimal]
  [PXDependsOnFields(new Type[] {typeof (ARRegisterReport.docType)})]
  [PXFormula(typeof (Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.prepaymentInvoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, decimal1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO, ARDocType.cashReturn>>, decimal_1>>))]
  [PXDBCalced(typeof (Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.prepaymentInvoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, decimal1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO, ARDocType.cashReturn>>, decimal_1>>), typeof (Decimal))]
  public override Decimal? SignAmount { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<ARRegisterReport.signAmount, Case<Where<BqlOperand<ARRegisterReport.isRetainageDocument, IBqlBool>.IsEqual<True>>, ARRegisterReport.origDocAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterReport.isRetainageDocument, NotEqual<True>>>>>.And<BqlOperand<ARRegisterReport.isRetainageReversing, IBqlBool>.IsEqual<True>>>, Mult<decimal_1, ARRegisterReport.retainageTotal>>>>), typeof (Decimal))]
  public virtual Decimal? SignReleasedRetainage { get; set; }

  /// <exclude />
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARRegisterReport>.By<ARRegisterReport.docType, ARRegisterReport.refNbr>
  {
    public static ARRegisterReport Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARRegisterReport) PrimaryKeyOf<ARRegisterReport>.By<ARRegisterReport.docType, ARRegisterReport.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class OriginalDocument : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARRegisterReport>.By<ARRegisterReport.origDocType, ARRegisterReport.origRefNbr>
    {
    }
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterReport.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterReport.refNbr>
  {
  }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterReport.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterReport.origRefNbr>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegisterReport.docDate>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterReport.docBal>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterReport.origDocAmt>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterReport.released>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterReport.isRetainageDocument>
  {
  }

  public new abstract class isRetainageReversing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterReport.isRetainageReversing>
  {
  }

  public new abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterReport.retainageTotal>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterReport.status>
  {
  }

  /// <summary>
  /// Read-only field indicating the sign of the document's impact on AR balance .
  /// Depends solely on the <see cref="!:DocType" /> field.
  /// </summary>
  /// <value>
  /// Possible values are: <c>1</c>, <c>-1</c> or <c>0</c>.
  /// </value>
  public abstract class signBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterReport.signBalance>
  {
  }

  public abstract class signAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterReport.signAmount>
  {
  }

  public abstract class signReleasedRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterReport.signReleasedRetainage>
  {
  }
}
