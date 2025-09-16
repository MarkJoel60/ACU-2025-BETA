// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaySelReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXCacheName("Bill For Payment")]
[PXProjection(typeof (Select2<APInvoice, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<APInvoice.payAccountID>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APAdjust.released, Equal<False>>>>, CrossJoin<APSetup>>>>>, Where<APInvoice.openDoc, Equal<True>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And2<Where<APInvoice.paySel, Equal<True>, Or<APSetup.requireApprovePayments, Equal<False>>>, And<APInvoice.voided, Equal<False>, And<APInvoice.dueDate, PX.Data.IsNotNull, And<APAdjust.adjgRefNbr, PX.Data.IsNull>>>>>>>))]
[PXPrimaryGraph(new System.Type[] {typeof (APInvoiceEntry)}, new System.Type[] {typeof (Select<APInvoice, Where<APInvoice.docType, Equal<Current<APPaySelReport.docType>>, And<APInvoice.refNbr, Equal<Current<APPaySelReport.refNbr>>>>>)})]
[Serializable]
public class APPaySelReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  protected string _PayNbr;
  protected bool? _SeparateCheck;
  protected string _InvoiceNbr;
  protected System.DateTime? _DueDate;
  protected System.DateTime? _DiscDate;
  protected System.DateTime? _PayDate;
  protected bool? _PaySel;
  protected long? _CuryInfoID;
  protected string _BaseCuryID;
  protected string _CuryRateType;
  protected string _CashCuryID;
  protected string _DocCuryID;
  protected string _OrigCuryMultDiv;
  protected Decimal? _OrigCuryRate;
  protected Decimal? _CuryOrigDocAmt;
  protected Decimal? _OrigDocAmt;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected Decimal? _CuryDiscBal;
  protected Decimal? _DiscBal;
  protected Decimal? _CuryPayOrigDocAmt;
  protected Decimal? _PayOrigDocAmt;
  protected Decimal? _CuryPayDocBal;
  protected Decimal? _PayDocBal;
  protected Decimal? _CuryPayDiscBal;
  protected Decimal? _PayDiscBal;

  [Branch(null, null, true, true, true, BqlField = typeof (APInvoice.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [CashAccount(BqlField = typeof (APInvoice.payAccountID))]
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

  [PXDBString(10, IsUnicode = true, BqlField = typeof (APInvoice.payTypeID))]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.Visible)]
  public virtual string PayTypeID
  {
    get => this._PayTypeID;
    set => this._PayTypeID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (APInvoice.termsID))]
  [PXUIField(DisplayName = "Terms", Visibility = PXUIVisibility.Visible)]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APInvoice.docType))]
  [PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.SelectorVisible)]
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

  [PXDBString(15)]
  [PXUIField(DisplayName = "Payment Nbr.", Visibility = PXUIVisibility.Visible)]
  public virtual string PayNbr
  {
    [PXDependsOnFields(new System.Type[] {typeof (APPaySelReport.separateCheck), typeof (APPaySelReport.refNbr)})] get
    {
      return !this._SeparateCheck.GetValueOrDefault() ? "" : this._RefNbr;
    }
    set => this._PayNbr = value;
  }

  [PXDBBool(BqlField = typeof (APInvoice.separateCheck))]
  [PXUIField(DisplayName = "Separate Check", Visibility = PXUIVisibility.Visible)]
  public virtual bool? SeparateCheck
  {
    get => this._SeparateCheck;
    set => this._SeparateCheck = value;
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
  [PXUIField(DisplayName = "Pay Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? PayDate
  {
    get => this._PayDate;
    set => this._PayDate = value;
  }

  [PXDBBool(BqlField = typeof (APInvoice.paySel))]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? PaySel
  {
    get => this._PaySel;
    set => this._PaySel = value;
  }

  [PXDBLong(BqlField = typeof (APInvoice.curyInfoID))]
  [PXUIField(DisplayName = "Currency Info")]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.baseCuryID))]
  [PXUIField(DisplayName = "Base Currency ID")]
  public virtual string BaseCuryID
  {
    get => this._BaseCuryID;
    set => this._BaseCuryID = value;
  }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyRateTypeID))]
  public virtual string CuryRateType
  {
    get => this._CuryRateType;
    set => this._CuryRateType = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.CA.CashAccount.curyID))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField(DisplayName = "Cash Account Currency")]
  public virtual string CashCuryID
  {
    get => this._CashCuryID;
    set => this._CashCuryID = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyID))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField(DisplayName = "Document Currency")]
  public virtual string DocCuryID
  {
    get => this._DocCuryID;
    set => this._DocCuryID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyMultDiv))]
  [PXDefault("M")]
  public virtual string OrigCuryMultDiv
  {
    get => this._OrigCuryMultDiv;
    set => this._OrigCuryMultDiv = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.CM.CurrencyInfo.curyRate))]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? OrigCuryRate
  {
    get => this._OrigCuryRate;
    set => this._OrigCuryRate = value;
  }

  [PXDBCury(typeof (APPaySelReport.cashCuryID), BqlField = typeof (APInvoice.curyOrigDocAmt))]
  [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.SelectorVisible)]
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
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.docBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXDBCury(typeof (APPaySelReport.cashCuryID), BqlField = typeof (APInvoice.curyDiscBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (APInvoice.discBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  [PXCury(typeof (APPaySelReport.cashCuryID))]
  public virtual Decimal? CuryPayOrigDocAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (APPaySelReport.curyOrigDocAmt), typeof (APPaySelReport.origDocAmt)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (APPaySelReport.curyDocBal), typeof (APPaySelReport.docBal)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (APPaySelReport.curyDiscBal), typeof (APPaySelReport.discBal)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (APPaySelReport.docType)})] get
    {
      return APDocType.SignBalance(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor that supplied the goods.
  /// </value>
  [PXDBInt(BqlField = typeof (APInvoice.suppliedByVendorID))]
  public virtual int? SuppliedByVendorID { get; set; }

  [APActiveProject(BqlField = typeof (APRegister.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APRegister.HasMultipleProjects" />
  [PXDBBool(BqlField = typeof (APRegister.hasMultipleProjects))]
  [PXDefault(false)]
  public virtual bool? HasMultipleProjects { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPaySelReport.branchID>
  {
  }

  public abstract class payAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPaySelReport.payAccountID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPaySelReport.vendorID>
  {
  }

  public abstract class payTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.payTypeID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.termsID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.docType>
  {
  }

  public abstract class printDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.printDocType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.refNbr>
  {
  }

  public abstract class payNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.payNbr>
  {
  }

  public abstract class separateCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPaySelReport.separateCheck>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.invoiceNbr>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPaySelReport.dueDate>
  {
  }

  public abstract class discDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPaySelReport.discDate>
  {
  }

  public abstract class payDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPaySelReport.payDate>
  {
  }

  public abstract class paySel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPaySelReport.paySel>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPaySelReport.curyInfoID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.baseCuryID>
  {
  }

  public abstract class curyRateType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.curyRateType>
  {
  }

  public abstract class cashCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.cashCuryID>
  {
  }

  public abstract class docCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPaySelReport.docCuryID>
  {
  }

  public abstract class origCuryMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPaySelReport.origCuryMultDiv>
  {
  }

  public abstract class origCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPaySelReport.origCuryRate>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPaySelReport.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.origDocAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.docBal>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.discBal>
  {
  }

  public abstract class curyPayOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPaySelReport.curyPayOrigDocAmt>
  {
  }

  public abstract class payOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPaySelReport.payOrigDocAmt>
  {
  }

  public abstract class curyPayDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPaySelReport.curyPayDocBal>
  {
  }

  public abstract class payDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.payDocBal>
  {
  }

  public abstract class curyPayDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPaySelReport.curyPayDiscBal>
  {
  }

  public abstract class payDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.payDiscBal>
  {
  }

  public abstract class signBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APPaySelReport.signBalance>
  {
  }

  public abstract class suppliedByVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APPaySelReport.suppliedByVendorID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPaySelReport.projectID>
  {
  }

  public abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APPaySelReport.hasMultipleProjects>
  {
  }
}
