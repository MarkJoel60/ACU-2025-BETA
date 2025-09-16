// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterReport
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

[PXCacheName("Document")]
[Serializable]
public class APRegisterReport : APRegister
{
  [PXDecimal]
  [PXDependsOnFields(new System.Type[] {typeof (APRegisterReport.docType)})]
  [PXFormula(typeof (Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>))]
  [PXDBCalced(typeof (Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>), typeof (Decimal))]
  public override Decimal? SignBalance { get; set; }

  [PXDecimal]
  [PXDependsOnFields(new System.Type[] {typeof (APRegisterReport.docType)})]
  [PXFormula(typeof (Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck>>, decimal1, Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.voidQuickCheck, APDocType.prepayment>>, decimal_1>>))]
  [PXDBCalced(typeof (Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck>>, decimal1, Case<Where<BqlOperand<APRegister.docType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.voidQuickCheck, APDocType.prepayment>>, decimal_1>>), typeof (Decimal))]
  public override Decimal? SignAmount { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<APRegisterReport.signAmount, Case<Where<BqlOperand<APRegisterReport.isRetainageDocument, IBqlBool>.IsEqual<True>>, APRegisterReport.origDocAmt, Case<Where<BqlOperand<APRegisterReport.isRetainageDocument, IBqlBool>.IsNotEqual<True>>, Mult<decimal_1, APRegisterReport.retainageTotal>>>>), typeof (Decimal))]
  public virtual Decimal? SignReleasedRetainage { get; set; }

  public new abstract class docType : BqlType<IBqlString, string>.Field<
  #nullable disable
  APRegisterReport.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterReport.refNbr>
  {
  }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterReport.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterReport.origRefNbr>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APRegisterReport.docDate>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterReport.docBal>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterReport.origDocAmt>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterReport.curyOrigDocAmt>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterReport.released>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterReport.isRetainageDocument>
  {
  }

  public new abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterReport.retainageTotal>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterReport.projectID>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterReport.paymentsByLinesAllowed>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterReport.finPeriodID>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterReport.tranPeriodID>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterReport.openDoc>
  {
  }

  public new abstract class prebooked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterReport.prebooked>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterReport.voided>
  {
  }

  public new abstract class closedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRegisterReport.closedDate>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterReport.vendorID>
  {
  }

  public new abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterReport.closedFinPeriodID>
  {
  }

  public new abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterReport.closedTranPeriodID>
  {
  }

  public new abstract class retainageApply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterReport.retainageApply>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterReport.hasMultipleProjects>
  {
  }

  /// <summary>
  /// Read-only field indicating the sign of the document's impact on AP balance .
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
    APRegisterReport.signBalance>
  {
  }

  /// <summary>
  /// Read-only field indicating the sign of the document amount.
  /// Depends solely on the <see cref="!:DocType" />
  /// </summary>
  /// <value>
  /// Can be <c>1</c>, <c>-1</c> or <c>0</c>.
  /// </value>
  public abstract class signAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterReport.signAmount>
  {
  }

  public abstract class signReleasedRetainage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterReport.signReleasedRetainage>
  {
  }
}
