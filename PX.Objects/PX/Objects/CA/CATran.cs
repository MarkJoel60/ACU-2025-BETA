// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>A cash account transaction.</summary>
[PXPrimaryGraph(new System.Type[] {typeof (CATranEntry), typeof (CashTransferEntry), typeof (CADepositEntry), typeof (ARCashSaleEntry), typeof (ARPaymentEntry), typeof (APQuickCheckEntry), typeof (APPaymentEntry), typeof (CABatchEntry), typeof (JournalEntry)}, new System.Type[] {typeof (Select<CAAdj, Where<CAAdj.tranID, Equal<Current<CATran.tranID>>, And<CAAdj.adjTranType, NotEqual<CATranType.cATransferExp>>>>), typeof (Select<CATransfer, Where<CATransfer.tranIDIn, Equal<Current<CATran.tranID>>, Or<CATransfer.tranIDOut, Equal<Current<CATran.tranID>>, Or<Where<CATransfer.transferNbr, Equal<Current<CATran.origRefNbr>>, And<Current<CATran.origTranType>, Equal<CATranType.cATransferExp>>>>>>>), typeof (Select<CADeposit, Where<CADeposit.tranType, Equal<Current<CATran.origTranType>>, And<CADeposit.refNbr, Equal<Current<CATran.origRefNbr>>>>>), typeof (Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<CATran.origTranType>>, And<ARCashSale.refNbr, Equal<Current<CATran.origRefNbr>>>>>), typeof (Select<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Current<CATran.origTranType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Current<CATran.origRefNbr>>, And<Current<CATran.origModule>, Equal<BatchModule.moduleAR>>>>>), typeof (Select<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<CATran.origTranType>>, And<APQuickCheck.refNbr, Equal<Current<CATran.origRefNbr>>>>>), typeof (Select<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, Equal<Current<CATran.origTranType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Current<CATran.origRefNbr>>, And<Current<CATran.origModule>, Equal<BatchModule.moduleAP>>>>>), typeof (Select<CABatch, Where<CABatch.batchNbr, Equal<Current<CATran.origRefNbr>>, And<Current<CATran.origModule>, Equal<BatchModule.moduleAP>, And<Current<CATran.origTranType>, Equal<CATranType.cABatch>>>>>), typeof (Select<PX.Objects.GL.Batch, Where<PX.Objects.GL.Batch.module, Equal<Current<CATran.origModule>>, And<PX.Objects.GL.Batch.batchNbr, Equal<Current<CATran.origRefNbr>>>>>)})]
[PXCacheName("CA Transaction")]
[Serializable]
public class CATran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAccountable
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXCury(typeof (CATran.curyID))]
  [PXUIField(Visible = false)]
  public virtual Decimal? BegBal { get; set; }

  [PXCury(typeof (CATran.curyID))]
  [PXUIField(DisplayName = "Ending Balance", Enabled = false)]
  public virtual Decimal? EndBal { get; set; }

  [PXString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Day of Week", Enabled = false)]
  public virtual 
  #nullable disable
  string DayDesc { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [BatchModule.List]
  [PXUIField(DisplayName = "Module")]
  public virtual string OrigModule { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Orig. Doc. Type")]
  [CAAPARTranType.ListByModule(typeof (CATran.origModule))]
  public virtual string OrigTranType { get; set; }

  [PXString]
  [PXDBCalced(typeof (Switch<Case<Where<CATran.origTranType, Equal<APDocType.check>>, ARDocType.payment, Case<Where<CATran.origTranType, Equal<APDocType.voidCheck>>, ARDocType.voidPayment, Case<Where<CATran.origTranType, Equal<CATranType.cACashDropTransaction>>, CATranType.cADeposit, Case<Where<CATran.origTranType, Equal<CATranType.cACashDropVoidTransaction>>, CATranType.cAVoidDeposit>>>>, CATran.origTranType>), typeof (string))]
  [PXUIField(DisplayName = "Tran. Type")]
  [CAAPARTranType.ListByModuleUI(typeof (CATran.origModule))]
  public virtual string OrigTranTypeUI { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Orig. Doc. Number")]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// Indicates that CATran was created by ARPaymentChargeTran or APPaymentChargeTran
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPaymentChargeTran { get; set; }

  [PXDBInt]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [PXDefault]
  [CashAccount]
  public virtual int? CashAccountID { get; set; }

  [PXFormula(typeof (Default<CATran.cashAccountID>))]
  [PXDefault(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CATran.cashAccountID>>>>))]
  [PXDBInt]
  public virtual int? BranchID { get; set; }

  [PXDBLongIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Document Number")]
  [PXVerifySelector(typeof (Search<CATran.tranID, Where<CATran.cashAccountID, Equal<Current<CARecon.cashAccountID>>, And<Where<CATran.reconNbr, IsNull, Or<CATran.reconNbr, Equal<Current<CARecon.reconNbr>>>>>>>), new System.Type[] {typeof (CATran.extRefNbr), typeof (CATran.tranDate), typeof (CATran.origModule), typeof (CATran.origTranType), typeof (CATran.origRefNbr), typeof (CATran.status), typeof (CATran.curyDebitAmt), typeof (CATran.curyCreditAmt), typeof (CATran.tranDesc), typeof (CATran.cleared), typeof (CATran.clearDate)}, VerifyField = false, DescriptionField = typeof (CATran.extRefNbr))]
  public virtual long? TranID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Doc. Date")]
  [CADailyAccumulator]
  public virtual DateTime? TranDate { get; set; }

  [PXDefault]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disb. / Receipt")]
  public virtual string DrCr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public virtual int? ReferenceID { get; set; }

  [PXUIField]
  [PXString(60, IsUnicode = true)]
  public virtual string ReferenceName { get; set; }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string TranDesc { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (CATran.cashAccountID), typeof (Selector<CATran.cashAccountID, CashAccount.branchID>), null, null, null, true, false, null, typeof (CATran.tranPeriodID), null, true, true)]
  [PXDefault]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Search<CASetup.holdEntry>))]
  public virtual bool? Hold { get; set; }

  /// <summary>Indicates that CATran is pending approval</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingApproval { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Posted { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [BatchStatus.List]
  public virtual string Status
  {
    [PXDependsOnFields(new System.Type[] {typeof (CATran.posted), typeof (CATran.released), typeof (CATran.hold), typeof (CATran.pendingApproval)})] get
    {
      if (this.Posted.GetValueOrDefault())
      {
        if (this.Voided.GetValueOrDefault())
          return "V";
        return this.Released.GetValueOrDefault() ? "P" : "U";
      }
      if (this.Voided.GetValueOrDefault() && !this.Posted.GetValueOrDefault())
        return "V";
      if (this.Released.GetValueOrDefault() && !this.Posted.GetValueOrDefault())
        return "R";
      if (this.Hold.GetValueOrDefault())
        return "H";
      return this.PendingApproval.GetValueOrDefault() ? "D" : "B";
    }
    set
    {
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Reconciled")]
  public virtual bool? Reconciled { get; set; }

  [PXDBDate]
  public virtual DateTime? ReconDate { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Reconciled Number", Enabled = false)]
  [PXParent(typeof (Select<CARecon, Where<CARecon.reconNbr, Equal<Current<CATran.reconNbr>>>>), UseCurrent = true, LeaveChildren = true)]
  public virtual string ReconNbr { get; set; }

  [PXDBCurrency(typeof (CATran.curyInfoID), typeof (CATran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran. Amount")]
  public virtual Decimal? TranAmt { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number")]
  public virtual string BatchNbr { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<CATran.cashAccountID>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date")]
  public virtual DateTime? ClearDate { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Receipt")]
  public virtual Decimal? CuryDebitAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
    {
      return !(this.DrCr == "D") ? new Decimal?(0M) : this.CuryTranAmt;
    }
    set
    {
    }
  }

  [PXDecimal]
  [PXUIField(DisplayName = "Disbursement")]
  public virtual Decimal? CuryCreditAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
    {
      if (!(this.DrCr == "C"))
        return new Decimal?(0M);
      Decimal? curyTranAmt = this.CuryTranAmt;
      return !curyTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyTranAmt.GetValueOrDefault());
    }
    set
    {
    }
  }

  [PXDecimal]
  [PXUIField(DisplayName = "Receipt")]
  public virtual Decimal? CuryClearedDebitAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (CATran.cleared), typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
    {
      return !this.Cleared.GetValueOrDefault() || !(this.DrCr == "D") ? new Decimal?(0M) : this.CuryTranAmt;
    }
    set
    {
    }
  }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Disbursement")]
  public virtual Decimal? CuryClearedCreditAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (CATran.cleared), typeof (CATran.drCr), typeof (CATran.curyTranAmt)})] get
    {
      if (!this.Cleared.GetValueOrDefault() || !(this.DrCr == "C"))
        return new Decimal?(0M);
      Decimal? curyTranAmt = this.CuryTranAmt;
      return !curyTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyTranAmt.GetValueOrDefault());
    }
    set
    {
    }
  }

  [PXNote(DescriptionField = typeof (CATran.tranID))]
  public virtual Guid? NoteID { get; set; }

  [CashAccount]
  public virtual int? RefTranAccountID { get; set; }

  [PXDBLong]
  public virtual long? RefTranID { get; set; }

  [PXDBInt]
  public virtual int? RefSplitLineNbr { get; set; }

  [PXDBLong]
  public virtual long? VoidedTranID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public static void Redirect(PXCache sender, CATran catran)
  {
    if (catran == null)
      return;
    if (sender != null)
      sender.IsDirty = false;
    RedirectionToOrigDoc.TryRedirect(catran.OrigTranType, catran.OrigRefNbr, catran.OrigModule);
  }

  public class PK : PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>
  {
    public static CATran Find(
      PXGraph graph,
      int? cashAccountID,
      long? tranID,
      PKFindOptions options = 0)
    {
      return (CATran) PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.FindBy(graph, (object) cashAccountID, (object) tranID, options);
    }
  }

  public class UK : PrimaryKeyOf<CATran>.By<CATran.tranID>
  {
    public static CATran Find(PXGraph graph, long? tranID, PKFindOptions options = 0)
    {
      return (CATran) PrimaryKeyOf<CATran>.By<CATran.tranID>.FindBy(graph, (object) tranID, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CATran>.By<CATran.cashAccountID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CATran>.By<CATran.branchID>
    {
    }

    public class BusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<CATran>.By<CATran.referenceID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CATran>.By<CATran.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CATran>.By<CATran.curyInfoID>
    {
    }

    public class ReconciliationStatement : 
      PrimaryKeyOf<CARecon>.By<CARecon.cashAccountID, CARecon.reconNbr>.ForeignKeyOf<CATran>.By<CATran.cashAccountID, CATran.reconNbr>
    {
    }

    public class Batch : 
      PrimaryKeyOf<PX.Objects.GL.Batch>.By<PX.Objects.GL.Batch.module, PX.Objects.GL.Batch.batchNbr>.ForeignKeyOf<CATran>.By<CATran.origModule, CATran.batchNbr>
    {
    }

    public class ChildCashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CATran>.By<CATran.refTranAccountID>
    {
    }

    public class ChildCashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CATran>.By<CATran.refTranAccountID, CATran.refTranID>
    {
    }

    public class VoidedCashAccountTransaction : 
      PrimaryKeyOf<CATran>.By<CATran.cashAccountID, CATran.tranID>.ForeignKeyOf<CATran>.By<CATran.cashAccountID, CATran.voidedTranID>
    {
    }

    public class ARPayment : 
      PrimaryKeyOf<PX.Objects.AR.ARPayment>.By<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>.ForeignKeyOf<CATran>.By<CATran.origTranType, CATran.origRefNbr>
    {
    }

    public class APPayment : 
      PrimaryKeyOf<PX.Objects.AP.APPayment>.By<PX.Objects.AP.APPayment.docType, PX.Objects.AP.APPayment.refNbr>.ForeignKeyOf<CATran>.By<CATran.origTranType, CATran.origRefNbr>
    {
    }

    public class CashTransaction : 
      PrimaryKeyOf<CAAdj>.By<CAAdj.adjTranType, CAAdj.adjRefNbr>.ForeignKeyOf<CATran>.By<CATran.origTranType, CATran.origRefNbr>
    {
    }

    public class CashAccountDeposit : 
      PrimaryKeyOf<CADeposit>.By<CADeposit.tranType, CADeposit.refNbr>.ForeignKeyOf<CATran>.By<CATran.origTranType, CATran.origRefNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.selected>
  {
  }

  public abstract class begBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATran.begBal>
  {
  }

  public abstract class endBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATran.endBal>
  {
  }

  public abstract class dayDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.dayDesc>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.origModule>
  {
  }

  public abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.origTranType>
  {
  }

  public abstract class origTranTypeUI : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.origTranTypeUI>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.origRefNbr>
  {
  }

  public abstract class isPaymentChargeTran : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CATran.isPaymentChargeTran>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran.origLineNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.extRefNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran.cashAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran.branchID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATran.tranID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATran.tranDate>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.drCr>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran.referenceID>
  {
  }

  public abstract class referenceName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.referenceName>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.tranDesc>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.finPeriodID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATran.curyInfoID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.hold>
  {
  }

  public abstract class pendingApproval : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.pendingApproval>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.voided>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.posted>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.status>
  {
  }

  public abstract class reconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.reconciled>
  {
  }

  public abstract class reconDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATran.reconDate>
  {
  }

  public abstract class reconNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.reconNbr>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATran.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATran.tranAmt>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.batchNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATran.curyID>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATran.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATran.clearDate>
  {
  }

  public abstract class curyDebitAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATran.curyDebitAmt>
  {
  }

  public abstract class curyCreditAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATran.curyCreditAmt>
  {
  }

  public abstract class curyClearedDebitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATran.curyClearedDebitAmt>
  {
  }

  public abstract class curyClearedCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATran.curyClearedCreditAmt>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CATran.noteID>
  {
  }

  public abstract class refTranAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran.refTranAccountID>
  {
  }

  public abstract class refTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATran.refTranID>
  {
  }

  public abstract class refSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATran.refSplitLineNbr>
  {
  }

  public abstract class voidedTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATran.voidedTranID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CATran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CATran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CATran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CATran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CATran.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CATran.Tstamp>
  {
  }
}
