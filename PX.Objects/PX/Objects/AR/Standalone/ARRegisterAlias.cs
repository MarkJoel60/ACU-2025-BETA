// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARRegisterAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.AR.Standalone;

/// <summary>
/// An alias descendant version of <see cref="T:PX.Objects.AR.Standalone.ARRegister" />. Can be
/// used e.g. to avoid behaviour when <see cref="T:PX.Data.PXSubstituteAttribute" />
/// substitutes <see cref="T:PX.Objects.AR.Standalone.ARRegister" /> for derived classes. Can also be used
/// as a table alias if <see cref="T:PX.Objects.AR.Standalone.ARRegister" /> is joined multiple times in BQL.
/// </summary>
/// <remarks>
/// Contains all BQL fields from the base class, which is enforced by unit tests.
/// This class should NOT override any properties. If you need such behaviour,
/// derive from this class, do not alter it.
/// </remarks>
[PXHidden]
[PXPrimaryGraph(new Type[] {typeof (SOInvoiceEntry), typeof (ARCashSaleEntry), typeof (ARInvoiceEntry), typeof (ARInvoiceEntry), typeof (ARPaymentEntry), typeof (ARPaymentEntry)}, new Type[] {typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<PX.Objects.AR.ARInvoice.released, Equal<False>>>>>>), typeof (Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARCashSale.docType>>, And<ARCashSale.refNbr, Equal<Current<ARCashSale.refNbr>>>>>), typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>>), typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<ARRegisterAlias.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<ARRegisterAlias.refNbr>>>>>), typeof (Select<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Current<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Current<PX.Objects.AR.ARPayment.refNbr>>>>>), typeof (Select<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Current<ARRegisterAlias.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Current<ARRegisterAlias.refNbr>>>>>)})]
[Serializable]
public class ARRegisterAlias : PX.Objects.AR.ARRegister
{
  public new abstract class externalRef : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.externalRef>
  {
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.selected>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias.branchID>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.docType>
  {
  }

  public new abstract class printDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.printDocType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.refNbr>
  {
  }

  public new abstract class documentKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.documentKey>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.origModule>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegisterAlias.docDate>
  {
  }

  public new abstract class origDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias.origDocDate>
  {
  }

  public new abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegisterAlias.dueDate>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.tranPeriodID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.finPeriodID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias.customerID>
  {
  }

  public new abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.customerID_Customer_acctName>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias.customerLocationID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.curyID>
  {
  }

  public new abstract class aRAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias.aRAccountID>
  {
  }

  public new abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias.aRSubID>
  {
  }

  public new abstract class prepaymentAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias.prepaymentAccountID>
  {
  }

  public new abstract class prepaymentSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias.prepaymentSubID>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias.lineCntr>
  {
  }

  public new abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias.adjCntr>
  {
  }

  public new abstract class drSchedCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias.drSchedCntr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARRegisterAlias.curyInfoID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.origDocAmt>
  {
  }

  public new abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias.docBal>
  {
  }

  public new abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyOrigDiscAmt>
  {
  }

  public new abstract class origDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.origDiscAmt>
  {
  }

  public new abstract class curyDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyDiscTaken>
  {
  }

  public new abstract class discTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.discTaken>
  {
  }

  public new abstract class curyDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias.discBal>
  {
  }

  public new abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias.docDisc>
  {
  }

  public new abstract class curyDocDisc : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyDocDisc>
  {
  }

  public new abstract class curyChargeAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyChargeAmt>
  {
  }

  public new abstract class chargeAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.chargeAmt>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.docDesc>
  {
  }

  public new abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.taxCalcMode>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARRegisterAlias.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias.lastModifiedDateTime>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARRegisterAlias.Tstamp>
  {
  }

  public new abstract class docClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.docClass>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.batchNbr>
  {
  }

  public new abstract class batchSeq : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARRegisterAlias.batchSeq>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.released>
  {
  }

  /// <exclude />
  public new abstract class releasedToVerify : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.releasedToVerify>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.scheduled>
  {
  }

  public new abstract class fromSchedule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.fromSchedule>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.voided>
  {
  }

  public new abstract class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.pendingProcessing>
  {
  }

  public new abstract class pendingPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.pendingPayment>
  {
  }

  public new abstract class postponePendingPaymentFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.postponePendingPaymentFlag>
  {
  }

  public new abstract class selfVoidingDoc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.selfVoidingDoc>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias.noteID>
  {
  }

  public new abstract class closedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias.closedDate>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias.refNoteID>
  {
  }

  public new abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.closedFinPeriodID>
  {
  }

  public new abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.closedTranPeriodID>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias.rGOLAmt>
  {
  }

  public new abstract class curyRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRoundDiff>
  {
  }

  public new abstract class roundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.roundDiff>
  {
  }

  public new abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.scheduleID>
  {
  }

  public new abstract class impRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.impRefNbr>
  {
  }

  public new abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias.statementDate>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias.salesPersonID>
  {
  }

  public new abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.disableAutomaticTaxCalculation>
  {
  }

  public new abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.isTaxValid>
  {
  }

  public new abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.isTaxPosted>
  {
  }

  public new abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.isTaxSaved>
  {
  }

  public new abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.nonTaxable>
  {
  }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias.origRefNbr>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias.status>
  {
  }

  public new abstract class curyDiscountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyDiscountedDocTotal>
  {
  }

  public new abstract class discountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.discountedDocTotal>
  {
  }

  public new abstract class curyDiscountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyDiscountedTaxableTotal>
  {
  }

  public new abstract class discountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.discountedTaxableTotal>
  {
  }

  public new abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyDiscountedPrice>
  {
  }

  public new abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.discountedPrice>
  {
  }

  public new abstract class hasPPDTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.hasPPDTaxes>
  {
  }

  public new abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.pendingPPD>
  {
  }

  public new abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyInitDocBal>
  {
  }

  public new abstract class initDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.initDocBal>
  {
  }

  public new abstract class displayCuryInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.displayCuryInitDocBal>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.isMigratedRecord>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.paymentsByLinesAllowed>
  {
  }

  public new abstract class approverID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias.approverID>
  {
  }

  public new abstract class approverWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias.approverWorkgroupID>
  {
  }

  public new abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.approved>
  {
  }

  public new abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.rejected>
  {
  }

  public new abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.dontApprove>
  {
  }

  public new abstract class retainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias.retainageAcctID>
  {
  }

  public new abstract class retainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias.retainageSubID>
  {
  }

  public new abstract class retainageApply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.retainageApply>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.isRetainageDocument>
  {
  }

  public new abstract class isRetainageReversing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.isRetainageReversing>
  {
  }

  public new abstract class defRetainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.defRetainagePct>
  {
  }

  public new abstract class curyLineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyLineRetainageTotal>
  {
  }

  public new abstract class lineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.lineRetainageTotal>
  {
  }

  public new abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRetainageTotal>
  {
  }

  public new abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.retainageTotal>
  {
  }

  public new abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRetainageUnreleasedAmt>
  {
  }

  public new abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.retainageUnreleasedAmt>
  {
  }

  public new abstract class curyRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRetainageReleased>
  {
  }

  public new abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.retainageReleased>
  {
  }

  public new abstract class curyRetainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRetainedTaxTotal>
  {
  }

  public new abstract class retainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.retainedTaxTotal>
  {
  }

  public new abstract class curyRetainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRetainedDiscTotal>
  {
  }

  public new abstract class retainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.retainedDiscTotal>
  {
  }

  public new abstract class curyRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRetainageUnpaidTotal>
  {
  }

  public new abstract class retainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.retainageUnpaidTotal>
  {
  }

  public new abstract class curyRetainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyRetainagePaidTotal>
  {
  }

  public new abstract class retainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.retainagePaidTotal>
  {
  }

  public new abstract class curyOrigDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.curyOrigDocAmtWithRetainageTotal>
  {
  }

  public new abstract class origDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias.origDocAmtWithRetainageTotal>
  {
  }

  public new abstract class isCancellation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.isCancellation>
  {
  }

  public new abstract class isCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.isCorrection>
  {
  }

  public new abstract class isUnderCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.isUnderCorrection>
  {
  }

  public new abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.canceled>
  {
  }

  public new abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.dontPrint>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.printed>
  {
  }

  public new abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.dontEmail>
  {
  }

  public new abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias.emailed>
  {
  }

  public new abstract class printInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.printInvoice>
  {
  }

  public new abstract class emailInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias.emailInvoice>
  {
  }
}
