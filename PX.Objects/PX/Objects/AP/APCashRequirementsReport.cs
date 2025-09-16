// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APCashRequirementsReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("Cash Requirement")]
[PXProjection(typeof (SelectFromBase<APInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Location>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CR.Location.bAccountID, Equal<APInvoice.vendorID>>>>>.And<BqlOperand<PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<APInvoice.vendorLocationID>>>>, FbqlJoins.Inner<LocationAPPaymentInfo>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<LocationAPPaymentInfo.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>>>>.And<BqlOperand<LocationAPPaymentInfo.locationID, IBqlInt>.IsEqual<PX.Objects.CR.Location.vPaymentInfoLocationID>>>>, FbqlJoins.Left<PaymentMethodAccount>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PaymentMethodAccount.paymentMethodID, Equal<LocationAPPaymentInfo.vPaymentMethodID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PaymentMethodAccount.branchID, Equal<APInvoice.branchID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PaymentMethodAccount.useForAP, Equal<True>>>>>.And<BqlOperand<PaymentMethodAccount.aPIsDefault, IBqlBool>.IsEqual<True>>>>>>, FbqlJoins.Left<PX.Objects.CA.CashAccount>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.CA.CashAccount.cashAccountID, Equal<APInvoice.payAccountID>>>>, PX.Data.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.payAccountID, PX.Data.IsNull>>>>.And<BqlOperand<PX.Objects.CA.CashAccount.cashAccountID, IBqlInt>.IsEqual<LocationAPPaymentInfo.vCashAccountID>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.payAccountID, PX.Data.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<LocationAPPaymentInfo.vCashAccountID, PX.Data.IsNull>>>>.And<BqlOperand<PX.Objects.CA.CashAccount.cashAccountID, IBqlInt>.IsEqual<PaymentMethodAccount.cashAccountID>>>>>>, FbqlJoins.Inner<PX.Objects.CM.CurrencyInfo>.On<BqlOperand<PX.Objects.CM.CurrencyInfo.curyInfoID, IBqlLong>.IsEqual<APInvoice.curyInfoID>>>, FbqlJoins.Left<APAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjdDocType, Equal<APInvoice.docType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>>>>>.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.prepayment>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.openDoc, Equal<True>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.released, Equal<True>>>>>.Or<BqlOperand<APInvoice.prebooked, IBqlBool>.IsEqual<True>>>>, PX.Data.And<BqlOperand<APInvoice.dueDate, IBqlDateTime>.IsNotNull>>, PX.Data.And<BqlOperand<PX.Objects.CA.CashAccount.cashAccountID, IBqlInt>.IsNotNull>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.curyAdjdAmt, PX.Data.IsNull>>>>.Or<BqlOperand<APAdjust.curyAdjdAmt, IBqlDecimal>.IsLess<APInvoice.curyOrigDocAmt>>>>>.And<BqlOperand<APInvoice.docBal, IBqlDecimal>.IsNotEqual<decimal0>>>))]
[Serializable]
public class APCashRequirementsReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _PayAccountID;
  protected int? _VendorID;
  protected 
  #nullable disable
  string _PayTypeID;
  protected string _TermsID;
  protected string _DocType;
  protected string _RefNbr;
  protected string _InvoiceNbr;
  protected System.DateTime? _DueDate;
  protected System.DateTime? _DiscDate;
  protected System.DateTime? _PayDate;
  protected bool? _PaySel;
  protected System.DateTime? _EstPayDate;
  protected string _DocDesc;
  protected long? _CuryInfoID;
  protected string _CuryRateType;
  protected string _BaseCuryID;
  protected string _CashCuryID;
  protected string _DocCuryID;
  /// <summary>
  /// The operation required for currency conversion: Divide or Multiply.
  /// </summary>
  protected string _OrigCuryMultDiv;
  /// <summary>The currency rate.</summary>
  protected Decimal? _OrigCuryRate;
  protected Decimal? _CuryOrigDocAmt;
  /// <summary>
  /// The invoice amount to be paid for the document in the currency of the document.
  /// </summary>
  protected Decimal? _OrigDocAmt;
  protected Decimal? _CuryDocBal;
  /// <summary>The invoice balance.</summary>
  protected Decimal? _DocBal;
  protected Decimal? _CuryDiscBal;
  /// <summary>The cash discount balance of the invoice.</summary>
  protected Decimal? _DiscBal;
  protected Decimal? _CuryPayOrigDocAmt;
  /// <summary>The invoice amount to be paid.</summary>
  protected Decimal? _PayOrigDocAmt;
  protected Decimal? _CuryPayDocBal;
  /// <summary>The invoice balance.</summary>
  protected Decimal? _PayDocBal;
  protected Decimal? _CuryPayDiscBal;
  /// <summary>The cash discount balance of the invoice.</summary>
  protected Decimal? _PayDiscBal;

  [Branch(null, null, true, true, true, BqlField = typeof (APInvoice.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [CashAccount(BqlField = typeof (PX.Objects.CA.CashAccount.cashAccountID))]
  public virtual int? PayAccountID
  {
    get => this._PayAccountID;
    set => this._PayAccountID = value;
  }

  [Vendor(BqlField = typeof (APInvoice.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (LocationAPPaymentInfo.vPaymentMethodID))]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.Visible)]
  public virtual string PayTypeID
  {
    get => this._PayTypeID;
    set => this._PayTypeID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (APInvoice.termsID))]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APInvoice.docType))]
  [APInvoiceType.List]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXString(3, IsFixed = true)]
  [APDocType.PrintList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
  public virtual string PrintDocType
  {
    get => this._DocType;
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APInvoice.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (APInvoice.invoiceNbr))]
  [PXUIField(DisplayName = "Vendor Ref.", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  [PXDBDate(BqlField = typeof (APInvoice.dueDate))]
  [PXUIField(DisplayName = "Due Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DueDate
  {
    get => this._DueDate;
    set => this._DueDate = value;
  }

  [PXDBDate(BqlField = typeof (APInvoice.discDate))]
  [PXUIField(DisplayName = "Cash Discount Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DiscDate
  {
    get => this._DiscDate;
    set => this._DiscDate = value;
  }

  [PXDBDate(BqlField = typeof (APInvoice.payDate))]
  public virtual System.DateTime? PayDate
  {
    get => this._PayDate;
    set => this._PayDate = value;
  }

  [PXDBBool(BqlField = typeof (APInvoice.paySel))]
  [PXUIField(DisplayName = "Approved for Payment")]
  public virtual bool? PaySel
  {
    get => this._PaySel;
    set => this._PaySel = value;
  }

  [PXDBDate(BqlField = typeof (APInvoice.estPayDate))]
  [PXUIField(DisplayName = "Estimated Pay Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? EstPayDate
  {
    get => this._EstPayDate;
    set => this._EstPayDate = value;
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (APInvoice.docDesc))]
  public virtual string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
  }

  [PXDBLong(BqlField = typeof (APInvoice.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyRateTypeID))]
  public virtual string CuryRateType
  {
    get => this._CuryRateType;
    set => this._CuryRateType = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.baseCuryID))]
  public virtual string BaseCuryID
  {
    get => this._BaseCuryID;
    set => this._BaseCuryID = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.CA.CashAccount.curyID))]
  [PXUIField(DisplayName = "Cash Account Currency")]
  public virtual string CashCuryID
  {
    get => this._CashCuryID;
    set => this._CashCuryID = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyID))]
  [PXUIField(DisplayName = "Document Currency")]
  public virtual string DocCuryID
  {
    get => this._DocCuryID;
    set => this._DocCuryID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyMultDiv))]
  public virtual string OrigCuryMultDiv
  {
    get => this._OrigCuryMultDiv;
    set => this._OrigCuryMultDiv = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyRate))]
  public virtual Decimal? OrigCuryRate
  {
    get => this._OrigCuryRate;
    set => this._OrigCuryRate = value;
  }

  [PXDBCury(typeof (APPaySelReport.cashCuryID), BqlField = typeof (APInvoice.curyOrigDocAmt))]
  public virtual Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.origDocAmt))]
  public virtual Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
  }

  [PXDBCury(typeof (APPaySelReport.cashCuryID), BqlField = typeof (APInvoice.curyDocBal))]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.docBal))]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXDBCury(typeof (APPaySelReport.cashCuryID), BqlField = typeof (APInvoice.curyDiscBal))]
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.discBal))]
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  [PXCury(typeof (APPaySelReport.cashCuryID))]
  public virtual Decimal? CuryPayOrigDocAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (APCashRequirementsReport.curyOrigDocAmt), typeof (APCashRequirementsReport.origDocAmt)})] get
    {
      return this._CuryPayOrigDocAmt;
    }
    set => this._CuryPayOrigDocAmt = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.origDocAmt))]
  public virtual Decimal? PayOrigDocAmt
  {
    get => this._PayOrigDocAmt;
    set => this._PayOrigDocAmt = value;
  }

  [PXCury(typeof (APPaySelReport.cashCuryID))]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Decimal? CuryPayDocBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (APCashRequirementsReport.curyDocBal), typeof (APCashRequirementsReport.docBal)})] get
    {
      return this._CuryPayDocBal;
    }
    set => this._CuryPayDocBal = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.docBal))]
  public virtual Decimal? PayDocBal
  {
    get => this._PayDocBal;
    set => this._PayDocBal = value;
  }

  [PXCury(typeof (APPaySelReport.cashCuryID))]
  [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Decimal? CuryPayDiscBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (APCashRequirementsReport.curyDiscBal), typeof (APCashRequirementsReport.discBal)})] get
    {
      return this._CuryPayDiscBal;
    }
    set => this._CuryPayDiscBal = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.discBal))]
  public virtual Decimal? PayDiscBal
  {
    get => this._PayDiscBal;
    set => this._PayDiscBal = value;
  }

  [PXDecimal(0)]
  [PXUIField(DisplayName = "SignBalance", Visibility = PXUIVisibility.Invisible)]
  public virtual Decimal? SignBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (APCashRequirementsReport.docType)})] get
    {
      return APDocType.SignBalance(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>The payment method of the invoice.</summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (APInvoice.payTypeID))]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.Visible)]
  public virtual string InvoicePayTypeID { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APCashRequirementsReport.branchID>
  {
  }

  public abstract class payAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APCashRequirementsReport.payAccountID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APCashRequirementsReport.vendorID>
  {
  }

  public abstract class payTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.payTypeID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APCashRequirementsReport.termsID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APCashRequirementsReport.docType>
  {
  }

  public abstract class printDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.printDocType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APCashRequirementsReport.refNbr>
  {
  }

  public abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.invoiceNbr>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APCashRequirementsReport.dueDate>
  {
  }

  public abstract class discDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APCashRequirementsReport.discDate>
  {
  }

  public abstract class payDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APCashRequirementsReport.payDate>
  {
  }

  public abstract class paySel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APCashRequirementsReport.paySel>
  {
  }

  public abstract class estPayDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APCashRequirementsReport.estPayDate>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APCashRequirementsReport.docDesc>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    APCashRequirementsReport.curyInfoID>
  {
  }

  public abstract class curyRateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.curyRateType>
  {
  }

  public abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.baseCuryID>
  {
  }

  public abstract class cashCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.cashCuryID>
  {
  }

  public abstract class docCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.docCuryID>
  {
  }

  public abstract class origCuryMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.origCuryMultDiv>
  {
  }

  public abstract class origCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.origCuryRate>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.origDocAmt>
  {
  }

  public abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APCashRequirementsReport.docBal>
  {
  }

  public abstract class curyDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.curyDiscBal>
  {
  }

  public abstract class discBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.discBal>
  {
  }

  public abstract class curyPayOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.curyPayOrigDocAmt>
  {
  }

  public abstract class payOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.payOrigDocAmt>
  {
  }

  public abstract class curyPayDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.curyPayDocBal>
  {
  }

  public abstract class payDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.payDocBal>
  {
  }

  public abstract class curyPayDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.curyPayDiscBal>
  {
  }

  public abstract class payDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.payDiscBal>
  {
  }

  public abstract class signBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APCashRequirementsReport.signBalance>
  {
  }

  public abstract class invoicePayTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APCashRequirementsReport.invoicePayTypeID>
  {
  }
}
