// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARRegisterAlias2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
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
[Serializable]
public class ARRegisterAlias2 : PX.Objects.AR.ARRegister
{
  public new abstract class externalRef : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.externalRef>
  {
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.selected>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias2.branchID>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.docType>
  {
  }

  public new abstract class printDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.printDocType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.refNbr>
  {
  }

  public new abstract class documentKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.documentKey>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.origModule>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegisterAlias2.docDate>
  {
  }

  public new abstract class origDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias2.origDocDate>
  {
  }

  public new abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegisterAlias2.dueDate>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.tranPeriodID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.finPeriodID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias2.customerID>
  {
  }

  public new abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.customerID_Customer_acctName>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias2.customerLocationID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.curyID>
  {
  }

  public new abstract class aRAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias2.aRAccountID>
  {
  }

  public new abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias2.aRSubID>
  {
  }

  public new abstract class prepaymentAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias2.prepaymentAccountID>
  {
  }

  public new abstract class prepaymentSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias2.prepaymentSubID>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias2.lineCntr>
  {
  }

  public new abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias2.adjCntr>
  {
  }

  public new abstract class drSchedCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterAlias2.drSchedCntr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARRegisterAlias2.curyInfoID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.origDocAmt>
  {
  }

  public new abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias2.docBal>
  {
  }

  public new abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyOrigDiscAmt>
  {
  }

  public new abstract class origDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.origDiscAmt>
  {
  }

  public new abstract class curyDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyDiscTaken>
  {
  }

  public new abstract class discTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.discTaken>
  {
  }

  public new abstract class curyDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias2.discBal>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias2.docDisc>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class curyDocDisc : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyDocDisc>
  {
  }

  public new abstract class curyChargeAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyChargeAmt>
  {
  }

  public new abstract class chargeAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.chargeAmt>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.docDesc>
  {
  }

  public new abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.taxCalcMode>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias2.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias2.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARRegisterAlias2.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias2.lastModifiedDateTime>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARRegisterAlias2.Tstamp>
  {
  }

  public new abstract class docClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.docClass>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.batchNbr>
  {
  }

  public new abstract class batchSeq : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARRegisterAlias2.batchSeq>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.released>
  {
  }

  /// <exclude />
  public new abstract class releasedToVerify : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.releasedToVerify>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.scheduled>
  {
  }

  public new abstract class fromSchedule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.fromSchedule>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.voided>
  {
  }

  public new abstract class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.pendingProcessing>
  {
  }

  public new abstract class pendingPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.pendingPayment>
  {
  }

  public new abstract class postponePendingPaymentFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.postponePendingPaymentFlag>
  {
  }

  public new abstract class selfVoidingDoc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.selfVoidingDoc>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias2.noteID>
  {
  }

  public new abstract class closedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias2.closedDate>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias2.refNoteID>
  {
  }

  public new abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.closedFinPeriodID>
  {
  }

  public new abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.closedTranPeriodID>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegisterAlias2.rGOLAmt>
  {
  }

  public new abstract class curyRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRoundDiff>
  {
  }

  public new abstract class roundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.roundDiff>
  {
  }

  public new abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.scheduleID>
  {
  }

  public new abstract class impRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.impRefNbr>
  {
  }

  public new abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegisterAlias2.statementDate>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias2.salesPersonID>
  {
  }

  public new abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.disableAutomaticTaxCalculation>
  {
  }

  public new abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.isTaxValid>
  {
  }

  public new abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.isTaxPosted>
  {
  }

  public new abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.isTaxSaved>
  {
  }

  public new abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.nonTaxable>
  {
  }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterAlias2.origRefNbr>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterAlias2.status>
  {
  }

  public new abstract class curyDiscountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyDiscountedDocTotal>
  {
  }

  public new abstract class discountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.discountedDocTotal>
  {
  }

  public new abstract class curyDiscountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyDiscountedTaxableTotal>
  {
  }

  public new abstract class discountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.discountedTaxableTotal>
  {
  }

  public new abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyDiscountedPrice>
  {
  }

  public new abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.discountedPrice>
  {
  }

  public new abstract class hasPPDTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.hasPPDTaxes>
  {
  }

  public new abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.pendingPPD>
  {
  }

  public new abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyInitDocBal>
  {
  }

  public new abstract class initDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.initDocBal>
  {
  }

  public new abstract class displayCuryInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.displayCuryInitDocBal>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.isMigratedRecord>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.paymentsByLinesAllowed>
  {
  }

  public new abstract class approverID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegisterAlias2.approverID>
  {
  }

  public new abstract class approverWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias2.approverWorkgroupID>
  {
  }

  public new abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.approved>
  {
  }

  public new abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.rejected>
  {
  }

  public new abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.dontApprove>
  {
  }

  public new abstract class retainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias2.retainageAcctID>
  {
  }

  public new abstract class retainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterAlias2.retainageSubID>
  {
  }

  public new abstract class retainageApply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.retainageApply>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.isRetainageDocument>
  {
  }

  public new abstract class isRetainageReversing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.isRetainageReversing>
  {
  }

  public new abstract class defRetainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.defRetainagePct>
  {
  }

  public new abstract class curyLineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyLineRetainageTotal>
  {
  }

  public new abstract class lineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.lineRetainageTotal>
  {
  }

  public new abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRetainageTotal>
  {
  }

  public new abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.retainageTotal>
  {
  }

  public new abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRetainageUnreleasedAmt>
  {
  }

  public new abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.retainageUnreleasedAmt>
  {
  }

  public new abstract class curyRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRetainageReleased>
  {
  }

  public new abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.retainageReleased>
  {
  }

  public new abstract class curyRetainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRetainedTaxTotal>
  {
  }

  public new abstract class retainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.retainedTaxTotal>
  {
  }

  public new abstract class curyRetainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRetainedDiscTotal>
  {
  }

  public new abstract class retainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.retainedDiscTotal>
  {
  }

  public new abstract class curyRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRetainageUnpaidTotal>
  {
  }

  public new abstract class retainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.retainageUnpaidTotal>
  {
  }

  public new abstract class curyRetainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyRetainagePaidTotal>
  {
  }

  public new abstract class retainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.retainagePaidTotal>
  {
  }

  public new abstract class curyOrigDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.curyOrigDocAmtWithRetainageTotal>
  {
  }

  public new abstract class origDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegisterAlias2.origDocAmtWithRetainageTotal>
  {
  }

  /// <exclude />
  public new abstract class isCancellation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.isCancellation>
  {
  }

  /// <exclude />
  public new abstract class isCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.isCorrection>
  {
  }

  /// <exclude />
  public new abstract class isUnderCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.isUnderCorrection>
  {
  }

  /// <exclude />
  public new abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.canceled>
  {
  }

  public new abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.dontPrint>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.printed>
  {
  }

  public new abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.dontEmail>
  {
  }

  public new abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegisterAlias2.emailed>
  {
  }

  public new abstract class printInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.printInvoice>
  {
  }

  public new abstract class emailInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterAlias2.emailInvoice>
  {
  }
}
