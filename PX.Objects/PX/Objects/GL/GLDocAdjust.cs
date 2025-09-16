// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLDocAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.GL;

[Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
[PXHidden]
[Serializable]
public class GLDocAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAdjustment
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _Module;
  protected string _BatchNbr;
  protected int? _AdjgLineNbr;
  protected string _AdjgDocType;
  protected string _AdjgRefNbr;
  protected int? _AdjgBranchID;
  protected int? _BAccountID;
  protected long? _AdjdCuryInfoID;
  protected string _AdjdDocType;
  protected string _AdjdRefNbr;
  protected int? _AdjdBranchID;
  protected int? _AdjNbr;
  protected string _AdjBatchNbr;
  protected long? _AdjdOrigCuryInfoID;
  protected long? _AdjgCuryInfoID;
  protected DateTime? _AdjgDocDate;
  protected string _AdjgFinPeriodID;
  protected string _AdjgTranPeriodID;
  protected DateTime? _AdjdDocDate;
  protected string _AdjdFinPeriodID;
  protected string _AdjdClosedFinPeriodID;
  protected string _AdjdTranPeriodID;
  protected Decimal? _CuryAdjgDiscAmt;
  protected Decimal? _CuryAdjgWhTaxAmt;
  protected Decimal? _CuryAdjgAmt;
  protected Decimal? _AdjDiscAmt;
  protected Decimal? _CuryAdjdDiscAmt;
  protected Decimal? _AdjWhTaxAmt;
  protected Decimal? _CuryAdjdWhTaxAmt;
  protected Decimal? _AdjAmt;
  protected Decimal? _CuryAdjdAmt;
  protected Decimal? _RGOLAmt;
  protected bool? _Released;
  protected bool? _Hold;
  protected bool? _Voided;
  protected int? _AdjdAPAcct;
  protected int? _AdjdSub;
  protected int? _AdjdWhTaxAcctID;
  protected int? _AdjdWhTaxSubID;
  protected Decimal? _AdjdCuryRate;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected Decimal? _CuryDiscBal;
  protected Decimal? _DiscBal;
  protected Decimal? _CuryWhTaxBal;
  protected Decimal? _WhTaxBal;
  protected bool? _TakeDiscAlways = new bool?(false);
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (Batch))]
  [PXUIField]
  [BatchModule.List]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (Batch))]
  [PXParent(typeof (Select<Batch, Where<Batch.module, Equal<Current<GLDocAdjust.module>>, And<Batch.batchNbr, Equal<Current<GLDocAdjust.batchNbr>>>>>))]
  [PXUIField]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXLineNbr(typeof (Batch.lineCntr))]
  public virtual int? AdjgLineNbr
  {
    get => this._AdjgLineNbr;
    set => this._AdjgLineNbr = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDBDefault(typeof (GLTranDoc.tranType))]
  [PXUIField]
  public virtual string AdjgDocType
  {
    get => this._AdjgDocType;
    set => this._AdjgDocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.AP.APPayment.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<GLTranDoc, Where<GLTranDoc.tranType, Equal<Current<GLDocAdjust.adjgDocType>>, And<PX.Objects.AP.APPayment.refNbr, Equal<Current<GLDocAdjust.adjgRefNbr>>, And<PX.Objects.AP.APPayment.adjCntr, Equal<Current<GLDocAdjust.adjNbr>>>>>>))]
  public virtual string AdjgRefNbr
  {
    get => this._AdjgRefNbr;
    set => this._AdjgRefNbr = value;
  }

  [Branch(typeof (PX.Objects.AP.APPayment.branchID), null, true, true, true)]
  public virtual int? AdjgBranchID
  {
    get => this._AdjgBranchID;
    set => this._AdjgBranchID = value;
  }

  [Vendor]
  [PXDBDefault(typeof (GLTranDoc.bAccountID))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(ModuleCode = "AP", CuryIDField = "AdjdCuryID")]
  public virtual long? AdjdCuryInfoID
  {
    get => this._AdjdCuryInfoID;
    set => this._AdjdCuryInfoID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault("INV")]
  [PXUIField]
  [APInvoiceType.AdjdList]
  public virtual string AdjdDocType
  {
    get => this._AdjdDocType;
    set => this._AdjdDocType = value;
  }

  [PXString(3, IsFixed = true)]
  [APDocType.PrintList]
  [PXUIField]
  public virtual string PrintAdjdDocType
  {
    get => this._AdjdDocType;
    set
    {
    }
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [APInvoiceType.AdjdRefNbr(typeof (Search2<GLDocAdjust.APInvoice.refNbr, LeftJoin<GLDocAdjust, On<GLDocAdjust.adjdDocType, Equal<GLDocAdjust.APInvoice.docType>, And<GLDocAdjust.adjdRefNbr, Equal<GLDocAdjust.APInvoice.refNbr>, And<GLDocAdjust.released, Equal<False>, And<Where<GLDocAdjust.adjgDocType, NotEqual<Current<PX.Objects.AP.APPayment.docType>>, Or<GLDocAdjust.adjgRefNbr, NotEqual<Current<PX.Objects.AP.APPayment.refNbr>>>>>>>>, LeftJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APPayment.docType, Equal<GLDocAdjust.APInvoice.docType>, And<PX.Objects.AP.APPayment.refNbr, Equal<GLDocAdjust.APInvoice.refNbr>, And<Where<PX.Objects.AP.APPayment.docType, Equal<APDocType.prepayment>, Or<PX.Objects.AP.APPayment.docType, Equal<APDocType.debitAdj>>>>>>>>, Where<GLDocAdjust.APInvoice.vendorID, Equal<Optional<PX.Objects.AP.APPayment.vendorID>>, And<GLDocAdjust.APInvoice.docType, Equal<Optional<GLDocAdjust.adjdDocType>>, And2<Where<GLDocAdjust.APInvoice.released, Equal<True>, Or<PX.Objects.AP.APRegister.prebooked, Equal<True>>>, And<GLDocAdjust.APInvoice.openDoc, Equal<True>, And<GLDocAdjust.adjgRefNbr, IsNull, And2<Where<PX.Objects.AP.APPayment.refNbr, IsNull, And<Current<PX.Objects.AP.APPayment.docType>, NotEqual<APDocType.refund>, Or<PX.Objects.AP.APPayment.refNbr, IsNotNull, And<Current<PX.Objects.AP.APPayment.docType>, Equal<APDocType.refund>>>>>, And<GLDocAdjust.APInvoice.docDate, LessEqual<Current<PX.Objects.AP.APPayment.adjDate>>, And<GLDocAdjust.APInvoice.finPeriodID, LessEqual<Current<PX.Objects.AP.APPayment.adjFinPeriodID>>>>>>>>>>>), Filterable = true)]
  public virtual string AdjdRefNbr
  {
    get => this._AdjdRefNbr;
    set => this._AdjdRefNbr = value;
  }

  [Branch(null, null, true, true, false)]
  public virtual int? AdjdBranchID
  {
    get => this._AdjdBranchID;
    set => this._AdjdBranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (PX.Objects.AP.APPayment.adjCntr))]
  public virtual int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string AdjBatchNbr
  {
    get => this._AdjBatchNbr;
    set => this._AdjBatchNbr = value;
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(ModuleCode = "AP", CuryIDField = "AdjdOrigCuryID")]
  public virtual long? AdjdOrigCuryInfoID
  {
    get => this._AdjdOrigCuryInfoID;
    set => this._AdjdOrigCuryInfoID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.AP.APPayment.curyInfoID), CuryIDField = "AdjgCuryID")]
  public virtual long? AdjgCuryInfoID
  {
    get => this._AdjgCuryInfoID;
    set => this._AdjgCuryInfoID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (PX.Objects.AP.APPayment.adjDate))]
  public virtual DateTime? AdjgDocDate
  {
    get => this._AdjgDocDate;
    set => this._AdjgDocDate = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDBDefault(typeof (GLTranDoc.finPeriodID))]
  [PXUIField(DisplayName = "Application Period", Enabled = false)]
  public virtual string AdjgFinPeriodID
  {
    get => this._AdjgFinPeriodID;
    set => this._AdjgFinPeriodID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDBDefault(typeof (GLTranDoc.tranPeriodID))]
  public virtual string AdjgTranPeriodID
  {
    get => this._AdjgTranPeriodID;
    set => this._AdjgTranPeriodID = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? AdjdDocDate
  {
    get => this._AdjdDocDate;
    set => this._AdjdDocDate = value;
  }

  [FinPeriodID(typeof (GLDocAdjust.adjdDocDate), typeof (GLDocAdjust.adjdBranchID), null, null, null, null, true, false, null, typeof (GLDocAdjust.adjdTranPeriodID), null, true, true)]
  [PXUIField]
  public virtual string AdjdFinPeriodID
  {
    get => this._AdjdFinPeriodID;
    set => this._AdjdFinPeriodID = value;
  }

  [PXDBScalar(typeof (Search<PX.Objects.AP.APRegister.closedFinPeriodID, Where<GLDocAdjust.APRegister.docType, Equal<GLDocAdjust.adjdDocType>, And<GLDocAdjust.APRegister.refNbr, Equal<GLDocAdjust.adjdRefNbr>>>>))]
  [PXString]
  public virtual string AdjdClosedFinPeriodID
  {
    get => this._AdjdClosedFinPeriodID;
    set => this._AdjdClosedFinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string AdjdTranPeriodID
  {
    get => this._AdjdTranPeriodID;
    set => this._AdjdTranPeriodID = value;
  }

  [PXDBCurrency(typeof (GLDocAdjust.adjgCuryInfoID), typeof (GLDocAdjust.adjDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjgDiscAmt
  {
    get => this._CuryAdjgDiscAmt;
    set => this._CuryAdjgDiscAmt = value;
  }

  [PXDBCurrency(typeof (GLDocAdjust.adjgCuryInfoID), typeof (GLDocAdjust.adjWhTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjgWhTaxAmt
  {
    get => this._CuryAdjgWhTaxAmt;
    set => this._CuryAdjgWhTaxAmt = value;
  }

  [PXDBCurrency(typeof (GLDocAdjust.adjgCuryInfoID), typeof (GLDocAdjust.adjAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (SumCalc<PX.Objects.AP.APPayment.curyApplAmt>))]
  public virtual Decimal? CuryAdjgAmt
  {
    get => this._CuryAdjgAmt;
    set => this._CuryAdjgAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjDiscAmt
  {
    get => this._AdjDiscAmt;
    set => this._AdjDiscAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdDiscAmt
  {
    get => this._CuryAdjdDiscAmt;
    set => this._CuryAdjdDiscAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjWhTaxAmt
  {
    get => this._AdjWhTaxAmt;
    set => this._AdjWhTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdWhTaxAmt
  {
    get => this._CuryAdjdWhTaxAmt;
    set => this._CuryAdjdWhTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjAmt
  {
    get => this._AdjAmt;
    set => this._AdjAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdAmt
  {
    get => this._CuryAdjdAmt;
    set => this._CuryAdjdAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [Account]
  [PXDefault]
  public virtual int? AdjdAPAcct
  {
    get => this._AdjdAPAcct;
    set => this._AdjdAPAcct = value;
  }

  [SubAccount]
  [PXDefault]
  public virtual int? AdjdSub
  {
    get => this._AdjdSub;
    set => this._AdjdSub = value;
  }

  [Account]
  [PXDefault(typeof (Search2<APTaxTran.accountID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<GLDocAdjust.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<GLDocAdjust.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>))]
  public virtual int? AdjdWhTaxAcctID
  {
    get => this._AdjdWhTaxAcctID;
    set => this._AdjdWhTaxAcctID = value;
  }

  [SubAccount]
  [PXDefault(typeof (Search2<APTaxTran.subID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<GLDocAdjust.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<GLDocAdjust.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>))]
  public virtual int? AdjdWhTaxSubID
  {
    get => this._AdjdWhTaxSubID;
    set => this._AdjdWhTaxSubID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal(8)]
  [PXUIField]
  public virtual Decimal? AdjdCuryRate
  {
    get => this._AdjdCuryRate;
    set => this._AdjdCuryRate = value;
  }

  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (GLDocAdjust.adjgCuryInfoID), typeof (GLDocAdjust.docBal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXCurrency(typeof (GLDocAdjust.adjgCuryInfoID), typeof (GLDocAdjust.discBal), BaseCalc = false)]
  [PXUnboundDefault]
  [PXUIField]
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault]
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  [PXCurrency(typeof (GLDocAdjust.adjgCuryInfoID), typeof (GLDocAdjust.whTaxBal), BaseCalc = false)]
  [PXUnboundDefault]
  [PXUIField]
  public virtual Decimal? CuryWhTaxBal
  {
    get => this._CuryWhTaxBal;
    set => this._CuryWhTaxBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault]
  public virtual Decimal? WhTaxBal
  {
    get => this._WhTaxBal;
    set => this._WhTaxBal = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? VoidAppl
  {
    [PXDependsOnFields(new Type[] {typeof (GLDocAdjust.adjgDocType)})] get
    {
      return new bool?(APPaymentType.VoidAppl(this._AdjgDocType));
    }
    set
    {
      if (!value.Value)
        return;
      this._AdjgDocType = APPaymentType.GetVoidingAPDocType(this.AdjgDocType) ?? "VCK";
      this.Voided = new bool?(true);
    }
  }

  [PXBool]
  public virtual bool? ReverseGainLoss
  {
    [PXDependsOnFields(new Type[] {typeof (GLDocAdjust.adjgDocType)})] get
    {
      return new bool?(APPaymentType.DrCr(this._AdjgDocType) == "D");
    }
    set
    {
    }
  }

  [PXBool]
  public virtual bool? TakeDiscAlways
  {
    get => this._TakeDiscAlways;
    set => this._TakeDiscAlways = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocAdjust.selected>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocAdjust.module>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocAdjust.batchNbr>
  {
  }

  public abstract class adjgLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjgLineNbr>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocAdjust.adjgRefNbr>
  {
  }

  public abstract class adjgBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjgBranchID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.bAccountID>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLDocAdjust.adjdCuryInfoID>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocAdjust.adjdDocType>
  {
  }

  public abstract class printAdjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.printAdjdDocType>
  {
  }

  [PXHidden]
  [Serializable]
  public class APRegister : PX.Objects.AP.APRegister
  {
    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLDocAdjust.APRegister.docType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLDocAdjust.APRegister.refNbr>
    {
    }

    public new abstract class hasMultipleProjects : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GLDocAdjust.APRegister.hasMultipleProjects>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select2<GLDocAdjust.APRegister, LeftJoin<PX.Objects.AP.Standalone.APInvoice, On<PX.Objects.AP.Standalone.APInvoice.docType, Equal<GLDocAdjust.APRegister.docType>, And<PX.Objects.AP.Standalone.APInvoice.refNbr, Equal<GLDocAdjust.APRegister.refNbr>>>>>))]
  [Serializable]
  public class APInvoice : GLDocAdjust.APRegister
  {
    protected DateTime? _DueDate;
    protected string _InvoiceNbr;

    [PXDBDate(BqlField = typeof (PX.Objects.AP.Standalone.APInvoice.dueDate))]
    public virtual DateTime? DueDate
    {
      get => this._DueDate;
      set => this._DueDate = value;
    }

    [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Standalone.APInvoice.invoiceNbr))]
    public virtual string InvoiceNbr
    {
      get => this._InvoiceNbr;
      set => this._InvoiceNbr = value;
    }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.docType>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.refNbr>
    {
    }

    public new abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLDocAdjust.APInvoice.vendorID>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.released>
    {
    }

    public new abstract class openDoc : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLDocAdjust.APInvoice.openDoc>
    {
    }

    public new abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.docDate>
    {
    }

    public new abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.finPeriodID>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.dueDate>
    {
    }

    public abstract class invoiceNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.invoiceNbr>
    {
    }

    public new abstract class hasMultipleProjects : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GLDocAdjust.APInvoice.hasMultipleProjects>
    {
    }
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocAdjust.adjdRefNbr>
  {
  }

  public abstract class adjdBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjdBranchID>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjNbr>
  {
  }

  public abstract class adjBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLDocAdjust.adjBatchNbr>
  {
  }

  public abstract class adjdOrigCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    GLDocAdjust.adjdOrigCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  GLDocAdjust.adjgCuryInfoID>
  {
  }

  public abstract class adjgDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLDocAdjust.adjgDocDate>
  {
  }

  public abstract class adjgFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.adjgFinPeriodID>
  {
  }

  public abstract class adjgTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.adjgTranPeriodID>
  {
  }

  public abstract class adjdDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLDocAdjust.adjdDocDate>
  {
  }

  public abstract class adjdFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.adjdFinPeriodID>
  {
  }

  public abstract class adjdClosedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.adjdClosedFinPeriodID>
  {
  }

  public abstract class adjdTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.adjdTranPeriodID>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLDocAdjust.curyAdjgDiscAmt>
  {
  }

  public abstract class curyAdjgWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLDocAdjust.curyAdjgWhTaxAmt>
  {
  }

  public abstract class curyAdjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.curyAdjgAmt>
  {
  }

  public abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.adjDiscAmt>
  {
  }

  public abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLDocAdjust.curyAdjdDiscAmt>
  {
  }

  public abstract class adjWhTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.adjWhTaxAmt>
  {
  }

  public abstract class curyAdjdWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLDocAdjust.curyAdjdWhTaxAmt>
  {
  }

  public abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.adjAmt>
  {
  }

  public abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.curyAdjdAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.rGOLAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocAdjust.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocAdjust.hold>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocAdjust.voided>
  {
  }

  public abstract class adjdAPAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjdAPAcct>
  {
  }

  public abstract class adjdSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjdSub>
  {
  }

  public abstract class adjdWhTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjdWhTaxAcctID>
  {
  }

  public abstract class adjdWhTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLDocAdjust.adjdWhTaxSubID>
  {
  }

  public abstract class adjdCuryRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.adjdCuryRate>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.docBal>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.discBal>
  {
  }

  public abstract class curyWhTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.curyWhTaxBal>
  {
  }

  public abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLDocAdjust.whTaxBal>
  {
  }

  public abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocAdjust.voidAppl>
  {
  }

  public abstract class reverseGainLoss : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocAdjust.reverseGainLoss>
  {
  }

  public abstract class takeDiscAlways : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLDocAdjust.takeDiscAlways>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLDocAdjust.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLDocAdjust.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLDocAdjust.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLDocAdjust.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLDocAdjust.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLDocAdjust.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLDocAdjust.noteID>
  {
  }
}
