// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone.APRegisterAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP.Standalone;

/// <summary>
/// An alias descendant version of <see cref="T:PX.Objects.AP.Standalone.APRegister" />. Can be
/// used e.g. to avoid behaviour when <see cref="T:PX.Data.PXSubstituteAttribute" />
/// substitutes <see cref="T:PX.Objects.AP.Standalone.APRegister" /> for derived classes. Can also be used
/// as a table alias if <see cref="T:PX.Objects.AP.Standalone.APRegister" /> is joined multiple times in BQL.
/// </summary>
/// <remarks>
/// Contains all BQL fields from the base class, which is enforced by unit tests.
/// This class should NOT override any properties. If you need such behaviour,
/// derive from this class, do not alter it.
/// </remarks>
[PXHidden]
[PXPrimaryGraph(new System.Type[] {typeof (APQuickCheckEntry), typeof (TXInvoiceEntry), typeof (APInvoiceEntry), typeof (APPaymentEntry)}, new System.Type[] {typeof (Select<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<APQuickCheck.docType>>, And<APQuickCheck.refNbr, Equal<Current<APQuickCheck.refNbr>>>>>), typeof (Select<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>, PX.Data.And<Where<PX.Objects.AP.APInvoice.released, Equal<False>, And<PX.Objects.AP.APRegister.origModule, Equal<BatchModule.moduleTX>>>>>>>), typeof (Select<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>>>>), typeof (Select<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, Equal<Current<PX.Objects.AP.APPayment.docType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Current<PX.Objects.AP.APPayment.refNbr>>>>>)})]
[Serializable]
public class APRegisterAlias : PX.Objects.AP.APRegister
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    APRegisterAlias>.By<APRegisterAlias.docType, APRegisterAlias.refNbr>
  {
    public static APRegisterAlias Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APRegisterAlias>.By<APRegisterAlias.docType, APRegisterAlias.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.vendorID, APRegisterAlias.vendorLocationID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.curyID>
    {
    }

    public class APAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.aPAccountID>
    {
    }

    public class APSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.aPSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.scheduleID>
    {
    }

    public class RetainageAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.retainageAcctID>
    {
    }

    public class RetainageSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<APRegisterAlias>.By<APRegisterAlias.retainageSubID>
    {
    }
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.selected>
  {
  }

  public new abstract class hiddenKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.hiddenKey>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.branchID>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.docType>
  {
  }

  public new abstract class printDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.printDocType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.refNbr>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.origModule>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APRegisterAlias.docDate>
  {
  }

  public new abstract class origDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRegisterAlias.origDocDate>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.tranPeriodID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.finPeriodID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.vendorID>
  {
  }

  public new abstract class vendorID_Vendor_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.vendorID_Vendor_acctName>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegisterAlias.vendorLocationID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.curyID>
  {
  }

  public new abstract class aPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.aPAccountID>
  {
  }

  public new abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.aPSubID>
  {
  }

  public new abstract class prepaymentAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegisterAlias.prepaymentAccountID>
  {
  }

  public new abstract class prepaymentSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegisterAlias.prepaymentSubID>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.lineCntr>
  {
  }

  public new abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.adjCntr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APRegisterAlias.curyInfoID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.origDocAmt>
  {
  }

  public new abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterAlias.docBal>
  {
  }

  public new abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterAlias.discTot>
  {
  }

  public new abstract class curyDiscTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDiscTot>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterAlias.docDisc>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public new abstract class curyDocDisc : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDocDisc>
  {
  }

  public new abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyOrigDiscAmt>
  {
  }

  public new abstract class origDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.origDiscAmt>
  {
  }

  public new abstract class curyDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDiscTaken>
  {
  }

  public new abstract class discTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.discTaken>
  {
  }

  public new abstract class curyDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterAlias.discBal>
  {
  }

  public new abstract class curyOrigWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyOrigWhTaxAmt>
  {
  }

  public new abstract class origWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.origWhTaxAmt>
  {
  }

  public new abstract class curyWhTaxBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyWhTaxBal>
  {
  }

  public new abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterAlias.whTaxBal>
  {
  }

  public new abstract class curyTaxWheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyTaxWheld>
  {
  }

  public new abstract class taxWheld : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterAlias.taxWheld>
  {
  }

  public new abstract class curyChargeAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyChargeAmt>
  {
  }

  public new abstract class chargeAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.chargeAmt>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.docDesc>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegisterAlias.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRegisterAlias.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APRegisterAlias.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRegisterAlias.lastModifiedDateTime>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APRegisterAlias.Tstamp>
  {
  }

  public new abstract class docClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.docClass>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.batchNbr>
  {
  }

  public new abstract class prebookBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.prebookBatchNbr>
  {
  }

  public new abstract class voidBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.voidBatchNbr>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.released>
  {
  }

  /// <exclude />
  public new abstract class releasedToVerify : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.releasedToVerify>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.scheduled>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.voided>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.printed>
  {
  }

  public new abstract class prebooked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.prebooked>
  {
  }

  public new abstract class pendingPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.pendingPayment>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegisterAlias.noteID>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegisterAlias.refNoteID>
  {
  }

  public new abstract class closedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRegisterAlias.closedDate>
  {
  }

  public new abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.closedFinPeriodID>
  {
  }

  public new abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.closedTranPeriodID>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegisterAlias.rGOLAmt>
  {
  }

  public new abstract class curyRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRoundDiff>
  {
  }

  public new abstract class roundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.roundDiff>
  {
  }

  public new abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyTaxRoundDiff>
  {
  }

  public new abstract class taxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.taxRoundDiff>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.status>
  {
  }

  public new abstract class scheduleID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.scheduleID>
  {
  }

  public new abstract class impRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegisterAlias.impRefNbr>
  {
  }

  public new abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.isTaxValid>
  {
  }

  public new abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.isTaxPosted>
  {
  }

  public new abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.isTaxSaved>
  {
  }

  public new abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.nonTaxable>
  {
  }

  public new abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.origDocType>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.origRefNbr>
  {
  }

  public new abstract class releasedOrPrebooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.releasedOrPrebooked>
  {
  }

  public new abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.taxCalcMode>
  {
  }

  public new abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.approved>
  {
  }

  public new abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.rejected>
  {
  }

  public new abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.dontApprove>
  {
  }

  public new abstract class employeeWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegisterAlias.employeeWorkgroupID>
  {
  }

  public new abstract class employeeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegisterAlias.employeeID>
  {
  }

  public new abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.workgroupID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.ownerID>
  {
  }

  public new abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyInitDocBal>
  {
  }

  public new abstract class initDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.initDocBal>
  {
  }

  public new abstract class displayCuryInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.displayCuryInitDocBal>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.isMigratedRecord>
  {
  }

  public new abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.paymentsByLinesAllowed>
  {
  }

  public new abstract class retainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegisterAlias.retainageAcctID>
  {
  }

  public new abstract class retainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegisterAlias.retainageSubID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegisterAlias.projectID>
  {
  }

  public new abstract class retainageApply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.retainageApply>
  {
  }

  public new abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.isRetainageDocument>
  {
  }

  public new abstract class isRetainageReversing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.isRetainageReversing>
  {
  }

  public new abstract class defRetainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.defRetainagePct>
  {
  }

  public new abstract class curyLineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyLineRetainageTotal>
  {
  }

  public new abstract class lineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.lineRetainageTotal>
  {
  }

  public new abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRetainageTotal>
  {
  }

  public new abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.retainageTotal>
  {
  }

  public new abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRetainageUnreleasedAmt>
  {
  }

  public new abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.retainageUnreleasedAmt>
  {
  }

  public new abstract class curyRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRetainageReleased>
  {
  }

  public new abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.retainageReleased>
  {
  }

  public new abstract class curyRetainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRetainedTaxTotal>
  {
  }

  public new abstract class retainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.retainedTaxTotal>
  {
  }

  public new abstract class curyRetainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRetainedDiscTotal>
  {
  }

  public new abstract class retainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.retainedDiscTotal>
  {
  }

  public new abstract class curyRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRetainageUnpaidTotal>
  {
  }

  public new abstract class retainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.retainageUnpaidTotal>
  {
  }

  public new abstract class curyRetainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyRetainagePaidTotal>
  {
  }

  public new abstract class retainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.retainagePaidTotal>
  {
  }

  public new abstract class curyOrigDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyOrigDocAmtWithRetainageTotal>
  {
  }

  public new abstract class origDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.origDocAmtWithRetainageTotal>
  {
  }

  public new abstract class curyDiscountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDiscountedDocTotal>
  {
  }

  public new abstract class discountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.discountedDocTotal>
  {
  }

  public new abstract class curyDiscountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDiscountedTaxableTotal>
  {
  }

  public new abstract class discountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.discountedTaxableTotal>
  {
  }

  public new abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.curyDiscountedPrice>
  {
  }

  public new abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegisterAlias.discountedPrice>
  {
  }

  public new abstract class hasPPDTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.hasPPDTaxes>
  {
  }

  public new abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegisterAlias.pendingPPD>
  {
  }

  /// <exclude />
  public new abstract class taxCostINAdjRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterAlias.taxCostINAdjRefNbr>
  {
  }

  public new abstract class isExpectedPPVValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.isExpectedPPVValid>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.hasMultipleProjects>
  {
  }

  public new abstract class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterAlias.pendingProcessing>
  {
  }
}
