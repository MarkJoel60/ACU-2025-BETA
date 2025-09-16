// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranAdjustment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The adjustments to accounts payable or accounts receivable documents.
/// A record is a link between an adjusted document and a bank transaction.
/// </summary>
[PXCacheName("Bank Transaction Adjustment")]
[Serializable]
public class CABankTranAdjustment : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICADocAdjust,
  IAdjustment
{
  protected bool? _Selected = new bool?(false);
  protected Decimal? _AdjgBalSign;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CABankTran.tranID))]
  public virtual int? TranID { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string AdjdModule { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = "")]
  [PXDefault("INV")]
  [PXUIField]
  [APInvoiceType.AdjdList]
  public virtual string AdjdDocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXInvoiceSelector(typeof (CABankTran.origModule))]
  public virtual string AdjdRefNbr { get; set; }

  [Branch(null, null, true, true, false)]
  public virtual int? AdjdBranchID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (CABankTran.lineCntr))]
  [PXParent(typeof (Select<CABankTran, Where<CABankTran.tranID, Equal<Current<CABankTranAdjustment.tranID>>>>))]
  [PXFormula(null, typeof (CountCalc<CABankTran.countAdjustments>))]
  [PXDefault(TypeCode.Int32, "0")]
  public virtual int? AdjNbr { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdAmt { get; set; }

  [PXBool]
  [PXUIField]
  public virtual bool? SeparateCheck { get; set; }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(typeof (CABankTran.curyInfoID))]
  public virtual long? AdjdCuryInfoID { get; set; }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string AdjdCuryID { get; set; }

  [PXString(3, IsFixed = true)]
  [APDocType.PrintList]
  [PXUIField]
  public virtual string PrintAdjdDocType
  {
    get => this.AdjdDocType;
    set
    {
    }
  }

  [PXDBString(40, IsUnicode = true)]
  public virtual string StubNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string AdjBatchNbr { get; set; }

  [PXDBInt]
  public virtual int? VoidAdjNbr { get; set; }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(typeof (CABankTran.curyInfoID))]
  public virtual long? AdjdOrigCuryInfoID { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CABankTran.curyInfoID))]
  public virtual long? AdjgCuryInfoID { get; set; }

  [PXDBDate]
  [PXDBDefault(typeof (CABankTran.tranDate))]
  public virtual DateTime? AdjgDocDate { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField(DisplayName = "Application Period", Enabled = false)]
  public virtual string AdjgFinPeriodID { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string AdjgTranPeriodID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? AdjdDocDate { get; set; }

  [FinPeriodID(typeof (CABankTranAdjustment.adjdDocDate), typeof (CABankTranAdjustment.adjdBranchID), null, null, null, null, true, false, null, typeof (CABankTranAdjustment.adjdTranPeriodID), null, true, true, RedefaultOrRevalidateOnOrganizationSourceUpdated = false, AutoCalculateMasterPeriod = false)]
  [PXUIField(DisplayName = "Post Period", Enabled = false, Visible = false)]
  public virtual string AdjdFinPeriodID { get; set; }

  [PXDBScalar(typeof (Search<PX.Objects.AP.APRegister.closedFinPeriodID, Where<PX.Objects.AP.APRegister.docType, Equal<CABankTranAdjustment.adjdDocType>, And<PX.Objects.AP.APRegister.refNbr, Equal<CABankTranAdjustment.adjdRefNbr>>>>))]
  [PXString]
  public virtual string AdjdClosedFinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string AdjdTranPeriodID { get; set; }

  [PXDBCurrency(typeof (CABankTranAdjustment.adjgCuryInfoID), typeof (CABankTranAdjustment.adjDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjgDiscAmt { get; set; }

  [PXDBCurrency(typeof (CABankTranAdjustment.adjgCuryInfoID), typeof (CABankTranAdjustment.adjWhTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjgWhTaxAmt { get; set; }

  [PXDBCurrency(typeof (CABankTranAdjustment.adjgCuryInfoID), typeof (CABankTranAdjustment.adjAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Mult<CABankTranAdjustment.adjgBalSign, CABankTranAdjustment.curyAdjgAmt>), typeof (SumCalc<CABankTran.curyApplAmt>))]
  public virtual Decimal? CuryAdjgAmt { get; set; }

  public virtual Decimal? CuryAdjgAmount
  {
    get => this.CuryAdjgAmt;
    set => this.CuryAdjgAmt = value;
  }

  [PXDBCurrency(typeof (CABankTranAdjustment.adjgCuryInfoID), typeof (CABankTranAdjustment.origDocAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjWhTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdWhTaxAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Hold { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided { get; set; }

  [Account(SuppressCurrencyValidation = true)]
  public virtual int? AdjdAPAcct { get; set; }

  [SubAccount]
  public virtual int? AdjdAPSub { get; set; }

  [Account]
  public virtual int? AdjdARAcct { get; set; }

  [SubAccount]
  public virtual int? AdjdARSub { get; set; }

  [Account]
  [PXDefault(typeof (Search2<APTaxTran.accountID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<CABankTranAdjustment.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>))]
  public virtual int? AdjdWhTaxAcctID { get; set; }

  [SubAccount]
  [PXDefault(typeof (Search2<APTaxTran.subID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<CABankTranAdjustment.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>))]
  public virtual int? AdjdWhTaxSubID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// with activated <see cref="P:PX.Objects.CS.FeaturesSet.PaymentsByLines" /> feature and
  /// such document allow payments by lines.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(8)]
  [PXUIField]
  public virtual Decimal? AdjdCuryRate { get; set; }

  /// <exclude />
  [PXString(40, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.invoiceNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<CABankTranAdjustment.adjdDocType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.APInvoice.invoiceNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<CABankTranAdjustment.adjdDocType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>>>>))]
  [PXFormula(typeof (Default<CABankTranAdjustment.adjdRefNbr>))]
  [PXDBScalar(typeof (Search<PX.Objects.AP.APInvoice.invoiceNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<CABankTranAdjustment.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<CABankTranAdjustment.adjdRefNbr>>>>))]
  public string APExtRefNbr { get; set; }

  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField]
  public virtual Decimal? CuryDocBal { get; set; }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal { get; set; }

  [PXCury(typeof (CABankTran.curyID))]
  [PXUnboundDefault]
  [PXUIField]
  public virtual Decimal? CuryDiscBal { get; set; }

  [PXDecimal(4)]
  [PXUnboundDefault]
  public virtual Decimal? DiscBal { get; set; }

  [PXCurrency(typeof (CABankTranAdjustment.adjdCuryInfoID), typeof (CABankTranAdjustment.whTaxBal))]
  [PXUnboundDefault]
  [PXUIField]
  public virtual Decimal? CuryWhTaxBal { get; set; }

  [PXDecimal(4)]
  [PXUnboundDefault]
  public virtual Decimal? WhTaxBal { get; set; }

  [PXFormula(typeof (Switch<Case<Where<CABankTranAdjustment.adjdDocType, NotEqual<ARDocType.creditMemo>>, Current<ARSetup.balanceWriteOff>>>))]
  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<ReasonCode.reasonCodeID, Where<ReasonCode.usage, Equal<ReasonCodeUsages.creditWriteOff>, Or<ReasonCode.usage, Equal<ReasonCodeUsages.balanceWriteOff>>>>))]
  [PXUIField]
  public virtual string WriteOffReasonCode { get; set; }

  [PXCurrency(typeof (CABankTranAdjustment.adjdCuryInfoID), typeof (CABankTranAdjustment.adjgWOAmt))]
  [PXUIField]
  [PXFormula(null, typeof (SumCalc<CABankTran.curyWOAmt>))]
  public virtual Decimal? CuryAdjgWOAmt
  {
    get => this.CuryAdjgWhTaxAmt;
    set => this.CuryAdjgWhTaxAmt = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? AdjgWOAmt { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? AdjgBalSign
  {
    get => new Decimal?(this._AdjgBalSign ?? 1M);
    set => this._AdjgBalSign = value;
  }

  public bool? ReverseGainLoss
  {
    get => new bool?(false);
    set
    {
    }
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : 
    PrimaryKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.tranID, CABankTranAdjustment.adjNbr>
  {
    public static CABankTranAdjustment Find(
      PXGraph graph,
      int? tranID,
      int? adjNbr,
      PKFindOptions options = 0)
    {
      return (CABankTranAdjustment) PrimaryKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.tranID, CABankTranAdjustment.adjNbr>.FindBy(graph, (object) tranID, (object) adjNbr, options);
    }
  }

  public static class FK
  {
    public class BankTransaction : 
      PrimaryKeyOf<CABankTran>.By<CABankTran.tranID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.tranID>
    {
    }

    public class BranchOfAdjustedDocument : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdBranchID>
    {
    }

    public class CurrencyInfoOfAdjustedDocument : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdCuryInfoID>
    {
    }

    public class OriginalCurrencyInfoOfAdjustedDocument : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdOrigCuryInfoID>
    {
    }

    public class AdjustmentBatch : 
      PrimaryKeyOf<PX.Objects.GL.Batch>.By<PX.Objects.GL.Batch.module, PX.Objects.GL.Batch.batchNbr>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdModule, CABankTranAdjustment.adjBatchNbr>
    {
    }

    public class CurrencyInfoOfAdjustingPayment : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjgCuryInfoID>
    {
    }

    public class AccountOfAdjustedBill : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdAPAcct>
    {
    }

    public class SubaccountOfAdjustedBill : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdAPSub>
    {
    }

    public class AccountOfAdjustedInvoice : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdARAcct>
    {
    }

    public class SubaccountOfAdjustedInvoice : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdARSub>
    {
    }

    public class WithholdingTaxAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdWhTaxAcctID>
    {
    }

    public class WithholdingTaxSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.adjdWhTaxSubID>
    {
    }

    public class WriteOffReasonCode : 
      PrimaryKeyOf<ReasonCode>.By<ReasonCode.reasonCodeID>.ForeignKeyOf<CABankTranAdjustment>.By<CABankTranAdjustment.writeOffReasonCode>
    {
    }
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.tranID>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranAdjustment.selected>
  {
  }

  public abstract class adjdModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdModule>
  {
  }

  public abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdDocType>
  {
  }

  public abstract class adjdRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdRefNbr>
  {
  }

  public abstract class adjdBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.adjdBranchID>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.adjNbr>
  {
  }

  public abstract class curyAdjdAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyAdjdAmt>
  {
  }

  public abstract class separateCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTranAdjustment.separateCheck>
  {
  }

  public abstract class adjdCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    CABankTranAdjustment.adjdCuryInfoID>
  {
  }

  public abstract class adjdCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdCuryID>
  {
  }

  public abstract class printAdjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.printAdjdDocType>
  {
  }

  public abstract class stubNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTranAdjustment.stubNbr>
  {
  }

  public abstract class adjBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjBatchNbr>
  {
  }

  public abstract class voidAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.voidAdjNbr>
  {
  }

  public abstract class adjdOrigCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    CABankTranAdjustment.adjdOrigCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    CABankTranAdjustment.adjgCuryInfoID>
  {
  }

  public abstract class adjgDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranAdjustment.adjgDocDate>
  {
  }

  public abstract class adjgFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjgFinPeriodID>
  {
  }

  public abstract class adjgTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjgTranPeriodID>
  {
  }

  public abstract class adjdDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranAdjustment.adjdDocDate>
  {
  }

  public abstract class adjdFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdFinPeriodID>
  {
  }

  public abstract class adjdClosedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdClosedFinPeriodID>
  {
  }

  public abstract class adjdTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.adjdTranPeriodID>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyAdjgDiscAmt>
  {
  }

  public abstract class curyAdjgWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyAdjgWhTaxAmt>
  {
  }

  public abstract class curyAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyAdjgAmt>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyOrigDocAmt>
  {
  }

  public abstract class adjDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.adjDiscAmt>
  {
  }

  public abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyAdjdDiscAmt>
  {
  }

  public abstract class adjWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.adjWhTaxAmt>
  {
  }

  public abstract class curyAdjdWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyAdjdWhTaxAmt>
  {
  }

  public abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranAdjustment.adjAmt>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.origDocAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranAdjustment.rGOLAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranAdjustment.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranAdjustment.hold>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTranAdjustment.voided>
  {
  }

  public abstract class adjdAPAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.adjdAPAcct>
  {
  }

  public abstract class adjdAPSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.adjdAPSub>
  {
  }

  public abstract class adjdARAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.adjdARAcct>
  {
  }

  public abstract class adjdARSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTranAdjustment.adjdARSub>
  {
  }

  public abstract class adjdWhTaxAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTranAdjustment.adjdWhTaxAcctID>
  {
  }

  public abstract class adjdWhTaxSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTranAdjustment.adjdWhTaxSubID>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTranAdjustment.paymentsByLinesAllowed>
  {
  }

  public abstract class adjdCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.adjdCuryRate>
  {
  }

  public abstract class apExtRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.apExtRefNbr>
  {
  }

  public abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranAdjustment.docBal>
  {
  }

  public abstract class curyDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranAdjustment.discBal>
  {
  }

  public abstract class curyWhTaxBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyWhTaxBal>
  {
  }

  public abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTranAdjustment.whTaxBal>
  {
  }

  public abstract class writeOffReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.writeOffReasonCode>
  {
  }

  public abstract class curyAdjgWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.curyAdjgWOAmt>
  {
  }

  public abstract class adjgWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.adjgWOAmt>
  {
  }

  public abstract class adjgBalSign : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTranAdjustment.adjgBalSign>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTranAdjustment.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankTranAdjustment.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTranAdjustment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranAdjustment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankTranAdjustment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTranAdjustment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTranAdjustment.lastModifiedDateTime>
  {
  }
}
