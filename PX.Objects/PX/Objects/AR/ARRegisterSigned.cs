// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterSigned
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[ARRegisterCacheName("AR Document")]
[Serializable]
public class ARRegisterSigned : ARRegister
{
  [PXDecimal]
  [PXDependsOnFields(new Type[] {typeof (ARRegisterSigned.docType)})]
  [PXFormula(typeof (Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, decimal1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO, ARDocType.cashReturn>>, decimal_1>>))]
  [PXDBCalced(typeof (Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale>>, decimal1, Case<Where<BqlOperand<ARRegister.docType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO, ARDocType.cashReturn>>, decimal_1>>), typeof (Decimal))]
  public override Decimal? SignAmount { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<ARRegisterSigned.signAmount, ARRegisterSigned.origDocAmt>), typeof (Decimal))]
  public virtual Decimal? SignedOrigDocAmt { get; set; }

  public new abstract class docType : BqlType<IBqlString, string>.Field<
  #nullable disable
  ARRegisterSigned.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterSigned.refNbr>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterSigned.origDocAmt>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterSigned.released>
  {
  }

  public abstract class signAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterSigned.signAmount>
  {
  }

  public abstract class signedOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterSigned.signedOrigDocAmt>
  {
  }
}
