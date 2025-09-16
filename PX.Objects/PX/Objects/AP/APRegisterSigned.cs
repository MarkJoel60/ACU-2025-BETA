// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterSigned
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
public class APRegisterSigned : APRegister
{
  [PXDecimal(4)]
  [PXDependsOnFields(new System.Type[] {typeof (APRegisterSigned.docType)})]
  [PXDBCalced(typeof (Mult<APRegisterSigned.docBal, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? DocBalSigned { get; set; }

  [PXDecimal(4)]
  [PXDependsOnFields(new System.Type[] {typeof (APRegisterSigned.docType)})]
  [PXDBCalced(typeof (Mult<APRegisterSigned.origDocAmt, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck>>, decimal1, Case<Where<BqlOperand<APRegisterSigned.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.voidQuickCheck, APDocType.prepayment>>, decimal_1>>>), typeof (Decimal))]
  public virtual Decimal? OrigDocAmtSigned { get; set; }

  public new abstract class origDocType : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    APRegisterSigned.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterSigned.origRefNbr>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterSigned.released>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterSigned.isRetainageDocument>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterSigned.paymentsByLinesAllowed>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterSigned.docType>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterSigned.docBal>
  {
  }

  public new abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterSigned.origDocAmt>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterSigned.hasMultipleProjects>
  {
  }

  public abstract class docBalSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterSigned.docBalSigned>
  {
  }

  public abstract class origDocAmtSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterSigned.origDocAmtSigned>
  {
  }
}
