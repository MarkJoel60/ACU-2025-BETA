// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranSigned
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
public class APTranSigned : APTran
{
  [PXDecimal(4)]
  [PXDependsOnFields(new System.Type[] {typeof (APTranSigned.tranType)})]
  [PXDBCalced(typeof (Mult<APTranSigned.tranBal, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? TranBalSigned { get; set; }

  [PXDecimal(4)]
  [PXDependsOnFields(new System.Type[] {typeof (APTranSigned.tranType)})]
  [PXDBCalced(typeof (Mult<APTranSigned.origTranAmt, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck>>, decimal1, Case<Where<BqlOperand<APTranSigned.tranType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.voidQuickCheck, APDocType.prepayment>>, decimal_1>>>), typeof (Decimal))]
  public virtual Decimal? OrigTranAmtSigned { get; set; }

  public new abstract class tranType : BqlType<IBqlString, string>.Field<
  #nullable disable
  APTranSigned.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranSigned.refNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranSigned.lineNbr>
  {
  }

  public new abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranSigned.origLineNbr>
  {
  }

  public new abstract class tranBal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranSigned.tranBal>
  {
  }

  public new abstract class origTranAmt : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranSigned.origTranAmt>
  {
  }

  public abstract class tranBalSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranSigned.tranBalSigned>
  {
  }

  public abstract class origTranAmtSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranSigned.origTranAmtSigned>
  {
  }
}
