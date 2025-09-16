// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone.APQuickCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Interfaces;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Descriptor;
using PX.Objects.PM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP.Standalone;

[PXProjection(typeof (Select2<APRegister, InnerJoin<APInvoice, On<APInvoice.docType, Equal<APRegister.docType>, And<APInvoice.refNbr, Equal<APRegister.refNbr>>>, InnerJoin<APPayment, On<APPayment.docType, Equal<APRegister.docType>, And<APPayment.refNbr, Equal<APRegister.refNbr>>>>>, Where<APRegister.docType, Equal<APDocType.quickCheck>, Or<APRegister.docType, Equal<APDocType.cashReturn>, Or<APRegister.docType, Equal<APDocType.voidQuickCheck>>>>>), Persistent = true, CommerceTranType = typeof (APInvoice))]
[PXSubstitute(GraphType = typeof (APQuickCheckEntry))]
[PXPrimaryGraph(typeof (APQuickCheckEntry))]
[PXCacheName("Cash Purchase")]
[Serializable]
public class APQuickCheck : 
  APRegister,
  IPrintCheckControlable,
  IProjectTaxes,
  IAssign,
  IApprovable,
  IApprovalDescription
{
  protected int? _RemitAddressID;
  protected 
  #nullable disable
  string _InvoiceNbr;
  protected System.DateTime? _InvoiceDate;
  protected string _TaxZoneID;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected bool? _SeparateCheck;
  protected bool? _PaySel = new bool?(false);
  protected int? _PrebookAcctID;
  protected int? _PrebookSubID;
  protected string _PaymentMethodID;
  protected int? _CashAccountID;
  protected System.DateTime? _AdjDate;
  protected string _AdjFinPeriodID;
  protected string _AdjTranPeriodID;
  protected int? _StubCntr;
  protected int? _BillCntr;
  protected bool? _Cleared;
  protected System.DateTime? _ClearDate;
  protected int? _ChargeCntr;
  protected bool? _DepositAsBatch;
  protected System.DateTime? _DepositAfter;
  protected bool? _Deposited;
  protected System.DateTime? _DepositDate;
  protected string _DepositType;
  protected string _DepositNbr;
  protected bool? _HasWithHoldTax;
  protected bool? _HasUseTax;

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegister.docType))]
  [PXDefault("QCK")]
  [APQuickCheckType.List]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false, TabOrder = 0)]
  [PXFieldDescription]
  [PXDependsOnFields(new System.Type[] {typeof (APQuickCheck.aPInvoiceDocType), typeof (APQuickCheck.aPPaymentDocType)})]
  public override string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (APRegister.refNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [APQuickCheckType.RefNbr(typeof (Search2<APQuickCheck.refNbr, InnerJoinSingleTable<PX.Objects.AP.Vendor, On<APQuickCheck.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<APQuickCheck.docType, Equal<Current<APQuickCheck.docType>>, PX.Data.And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, OrderBy<Desc<APQuickCheck.refNbr>>>), Filterable = true)]
  [APQuickCheckType.Numbering]
  [PXFieldDescription]
  [PXDependsOnFields(new System.Type[] {typeof (APQuickCheck.aPInvoiceRefNbr), typeof (APQuickCheck.aPPaymentRefNbr)})]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true, BqlField = typeof (PX.Objects.AP.APRegister.vendorID))]
  [PXDefault]
  public override int? VendorID { get; set; }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), Visibility = PXUIVisibility.SelectorVisible, BqlField = typeof (PX.Objects.AP.APRegister.vendorLocationID))]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AP.Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  public override int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor that supplied the goods.
  /// Always equals to VendorID in APQuickCheck.
  /// </value>
  [PXDBInt(BqlField = typeof (APInvoice.suppliedByVendorID))]
  [PXFormula(typeof (APQuickCheck.vendorID))]
  public virtual int? SuppliedByVendorID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.Location">Location</see> of the <see cref="T:PX.Objects.AP.Vendor">Supplied-By Vendor</see>, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field. Defaults to AP bill's <see cref="!:APInvoice.VendorLocationID">vendor location</see>.
  /// Always equals to VendorLocationID in APQuickCheck.
  /// </value>
  [PXDBInt(BqlField = typeof (APInvoice.suppliedByVendorLocationID))]
  [PXFormula(typeof (APQuickCheck.vendorLocationID))]
  public virtual int? SuppliedByVendorLocationID { get; set; }

  [PXDBInt(BqlField = typeof (APPayment.remitAddressID))]
  [APAddress(typeof (Select2<PX.Objects.CR.Location, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.vRemitAddressID>, PX.Data.And<Where<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, Or<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.parentBAccountID>>>>>, LeftJoin<APAddress, On<APAddress.vendorID, Equal<PX.Objects.CR.Address.bAccountID>, And<APAddress.vendorAddressID, Equal<PX.Objects.CR.Address.addressID>, And<APAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<APAddress.isDefaultAddress, Equal<True>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APQuickCheck.vendorLocationID>>>>>))]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  [PXSelector(typeof (APContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Remittance Contact", Visible = false)]
  [PXDBInt(BqlField = typeof (APPayment.remitContactID))]
  [APContact(typeof (Select2<PX.Objects.CR.Location, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.vRemitContactID>, PX.Data.And<Where<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, Or<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.parentBAccountID>>>>>, LeftJoin<APContact, On<APContact.vendorID, Equal<PX.Objects.CR.Contact.bAccountID>, And<APContact.vendorContactID, Equal<PX.Objects.CR.Contact.contactID>, And<APContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<APContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APQuickCheck.vendorLocationID>>>>>))]
  public virtual int? RemitContactID { get; set; }

  [PXDefault(typeof (Search<PX.Objects.CR.Location.aPAccountID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<APQuickCheck.vendorLocationID>>>>>))]
  [Account(typeof (APQuickCheck.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", BqlField = typeof (APRegister.aPAccountID), ControlAccountForModule = "AP")]
  public override int? APAccountID
  {
    get => this._APAccountID;
    set => this._APAccountID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.CR.Location.aPSubID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<APQuickCheck.vendorLocationID>>>>>))]
  [SubAccount(typeof (APQuickCheck.aPAccountID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible, BqlField = typeof (APRegister.aPSubID))]
  public override int? APSubID
  {
    get => this._APSubID;
    set => this._APSubID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (APInvoice.termsID))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.termsID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<APQuickCheck.vendorID>>>>))]
  [APTermsSelector]
  [Terms(typeof (APQuickCheck.docDate), null, null, typeof (APQuickCheck.curyDocBal), typeof (APQuickCheck.curyOrigDiscAmt), typeof (APQuickCheck.curyTaxTotal), typeof (APQuickCheck.curyOrigTaxDiscAmt), typeof (APQuickCheck.branchID))]
  [PXUIField(DisplayName = "Terms")]
  public virtual string TermsID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AP.APRegister.lineCntr))]
  [PXDefault(0)]
  public override int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.AP.APRegister.adjCntr))]
  [PXDefault(0)]
  public override int? AdjCntr { get; set; }

  [PXDBLong(BqlField = typeof (APRegister.curyInfoID))]
  [PX.Objects.CM.Extensions.CurrencyInfo]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.origDocAmt), BqlField = typeof (PX.Objects.AP.APRegister.curyOrigDocAmt))]
  [PXUIField(DisplayName = "Payment Amount", Visibility = PXUIVisibility.SelectorVisible)]
  public override Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APRegister.origDocAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.taxAmt), BqlField = typeof (APInvoice.curyTaxAmt))]
  [PXUIField(DisplayName = "Tax Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (APInvoice.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.docBal), BaseCalc = false, BqlField = typeof (PX.Objects.AP.APRegister.curyDocBal))]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public override Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APRegister.docBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.origDiscAmt), BqlField = typeof (PX.Objects.AP.APRegister.curyOrigDiscAmt))]
  [PXUIField(DisplayName = "Cash Discount Taken", Visibility = PXUIVisibility.SelectorVisible)]
  public override Decimal? CuryOrigDiscAmt
  {
    get => this._CuryOrigDiscAmt;
    set => this._CuryOrigDiscAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APRegister.origDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? OrigDiscAmt
  {
    get => this._OrigDiscAmt;
    set => this._OrigDiscAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.discTaken), BqlField = typeof (PX.Objects.AP.APRegister.curyDiscTaken))]
  public override Decimal? CuryDiscTaken
  {
    get => this._CuryDiscTaken;
    set => this._CuryDiscTaken = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APRegister.discTaken))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscTaken
  {
    get => this._DiscTaken;
    set => this._DiscTaken = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.discBal), BaseCalc = false, BqlField = typeof (APRegister.curyDiscBal))]
  [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public override Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (APRegister.discBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.origWhTaxAmt), BqlField = typeof (PX.Objects.AP.APRegister.curyOrigWhTaxAmt))]
  [PXUIField(DisplayName = "With. Tax", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public override Decimal? CuryOrigWhTaxAmt
  {
    get => this._CuryOrigWhTaxAmt;
    set => this._CuryOrigWhTaxAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APRegister.origWhTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? OrigWhTaxAmt
  {
    get => this._OrigWhTaxAmt;
    set => this._OrigWhTaxAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.whTaxBal), BaseCalc = false, BqlField = typeof (PX.Objects.AP.APRegister.curyWhTaxBal))]
  public override Decimal? CuryWhTaxBal
  {
    get => this._CuryWhTaxBal;
    set => this._CuryWhTaxBal = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APRegister.whTaxBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? WhTaxBal
  {
    get => this._WhTaxBal;
    set => this._WhTaxBal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.taxWheld), BqlField = typeof (PX.Objects.AP.APRegister.curyTaxWheld))]
  public override Decimal? CuryTaxWheld
  {
    get => this._CuryTaxWheld;
    set => this._CuryTaxWheld = value;
  }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (PX.Objects.AP.APRegister.taxWheld))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? TaxWheld
  {
    get => this._TaxWheld;
    set => this._TaxWheld = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (APRegister.docDesc))]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public override string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
  }

  [PXDBCreatedByID(BqlField = typeof (PX.Objects.AP.APRegister.createdByID))]
  public override Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID(BqlField = typeof (PX.Objects.AP.APRegister.createdByScreenID))]
  public override string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime(BqlField = typeof (PX.Objects.AP.APRegister.createdDateTime))]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public override System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.AP.APRegister.lastModifiedByID))]
  public override Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.AP.APRegister.lastModifiedByScreenID))]
  public override string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.AP.APRegister.lastModifiedDateTime))]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public override System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp(BqlField = typeof (PX.Objects.AP.APRegister.Tstamp), RecordComesFirst = true)]
  public override byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APRegister.batchNbr))]
  [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PX.Objects.GL.BatchNbr(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (PX.Objects.AP.APRegister.isMigratedRecord))]
  public override string BatchNbr { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APRegister.prebookBatchNbr))]
  [PXUIField(DisplayName = "Pre-releasing Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXSelector(typeof (PX.Objects.GL.Batch.batchNbr))]
  public override string PrebookBatchNbr
  {
    get => this._PrebookBatchNbr;
    set => this._PrebookBatchNbr = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APRegister.voidBatchNbr))]
  [PXUIField(DisplayName = "Void Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXSelector(typeof (PX.Objects.GL.Batch.batchNbr))]
  public override string VoidBatchNbr
  {
    get => this._VoidBatchNbr;
    set => this._VoidBatchNbr = value;
  }

  [PXDefault("H")]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [APDocStatus.List]
  [PXDBString(1, IsFixed = true, BqlField = typeof (APRegister.status))]
  [PXDependsOnFields(new System.Type[] {typeof (APQuickCheck.voided), typeof (APQuickCheck.hold), typeof (APQuickCheck.scheduled), typeof (APQuickCheck.released), typeof (APQuickCheck.printed), typeof (APQuickCheck.prebooked), typeof (APQuickCheck.openDoc), typeof (APQuickCheck.printCheck), typeof (PX.Objects.AP.APRegister.approved), typeof (PX.Objects.AP.APRegister.dontApprove), typeof (PX.Objects.AP.APRegister.rejected), typeof (APQuickCheck.docType)})]
  public override string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APRegister.released))]
  [PXDefault(false)]
  public override bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APRegister.openDoc))]
  [PXDefault(true)]
  public override bool? OpenDoc
  {
    get => this._OpenDoc;
    set => this._OpenDoc = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APRegister.scheduled))]
  [PXDefault(false)]
  public override bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APRegister.voided))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Void", Visible = false)]
  public override bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APRegister.prebooked))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Pre-Released", Visible = false)]
  public override bool? Prebooked
  {
    get => this._Prebooked;
    set => this._Prebooked = value;
  }

  [PXSearchable(1, "AP {0}: {1} - {3}", new System.Type[] {typeof (APQuickCheck.docType), typeof (APQuickCheck.refNbr), typeof (APQuickCheck.vendorID), typeof (PX.Objects.AP.Vendor.acctName)}, new System.Type[] {typeof (APQuickCheck.invoiceNbr), typeof (APQuickCheck.docDesc)}, NumberFields = new System.Type[] {typeof (APQuickCheck.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (APQuickCheck.docDate), typeof (APQuickCheck.status), typeof (APQuickCheck.invoiceNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (APQuickCheck.docDesc)}, MatchWithJoin = typeof (InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<APQuickCheck.vendorID>>>), SelectForFastIndexing = typeof (Select2<APQuickCheck, InnerJoin<PX.Objects.AP.Vendor, On<APQuickCheck.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>))]
  [PXNote(BqlField = typeof (PX.Objects.AP.APRegister.noteID), DescriptionField = typeof (APQuickCheck.refNbr))]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.AP.APRegister.closedDate))]
  [PXUIField(DisplayName = "Closed Date", Visibility = PXUIVisibility.Invisible)]
  public override System.DateTime? ClosedDate { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (APQuickCheck.branchID), null, null, null, null, true, false, null, typeof (APQuickCheck.closedTranPeriodID), null, true, true, BqlField = typeof (APRegister.closedFinPeriodID))]
  [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
  public override string ClosedFinPeriodID
  {
    get => this._ClosedFinPeriodID;
    set => this._ClosedFinPeriodID = value;
  }

  [PeriodID(null, null, null, true, BqlField = typeof (PX.Objects.AP.APRegister.closedTranPeriodID))]
  [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
  public override string ClosedTranPeriodID
  {
    get => this._ClosedTranPeriodID;
    set => this._ClosedTranPeriodID = value;
  }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.AP.APRegister.rGOLAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APRegister.scheduleID))]
  public override string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APRegister.impRefNbr))]
  public override string ImpRefNbr
  {
    get => this._ImpRefNbr;
    set => this._ImpRefNbr = value;
  }

  [PXDBString(3, IsFixed = true, BqlField = typeof (APInvoice.docType))]
  [PXDefault]
  [PXRestriction]
  public virtual string APInvoiceDocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (APInvoice.refNbr))]
  [PXDefault]
  [PXRestriction]
  public virtual string APInvoiceRefNbr
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

  [PXDBDate(BqlField = typeof (APInvoice.invoiceDate))]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Vendor Ref. Date", Visibility = PXUIVisibility.Invisible)]
  public virtual System.DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (APInvoice.taxZoneID))]
  [PXUIField(DisplayName = "Vendor Tax Zone", Visibility = PXUIVisibility.Visible)]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vTaxZoneID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APQuickCheck.vendorLocationID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [PXUIField(DisplayName = "Tax Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.taxTotal), BqlField = typeof (APInvoice.curyTaxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal
  {
    get => this._CuryTaxTotal;
    set => this._CuryTaxTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (APInvoice.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal
  {
    get => this._TaxTotal;
    set => this._TaxTotal = value;
  }

  [PXUIField(DisplayName = "Detail Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.lineTotal), BqlField = typeof (APInvoice.curyLineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (APInvoice.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.vatExemptTotal), BqlField = typeof (APInvoice.curyVatExemptTotal))]
  [PXUIField(DisplayName = "Tax Exempt Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal
  {
    get => this._CuryVatExemptTotal;
    set => this._CuryVatExemptTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (APInvoice.vatExemptTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal
  {
    get => this._VatExemptTotal;
    set => this._VatExemptTotal = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.vatTaxableTotal), BqlField = typeof (APInvoice.curyVatTaxableTotal))]
  [PXUIField(DisplayName = "Taxable Total", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal
  {
    get => this._CuryVatTaxableTotal;
    set => this._CuryVatTaxableTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (APInvoice.vatTaxableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal
  {
    get => this._VatTaxableTotal;
    set => this._VatTaxableTotal = value;
  }

  [PXDBBool(BqlField = typeof (APInvoice.separateCheck))]
  [PXDefault(true)]
  public virtual bool? SeparateCheck
  {
    get => this._SeparateCheck;
    set => this._SeparateCheck = value;
  }

  [PXDBBool(BqlField = typeof (APInvoice.paySel))]
  [PXDefault(false)]
  public bool? PaySel
  {
    get => this._PaySel;
    set => this._PaySel = value;
  }

  [PXDefault(typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<APQuickCheck.vendorID>>>>), SourceField = typeof (PX.Objects.AP.Vendor.prebookAcctID), PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(DisplayName = "Reclassification Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), BqlField = typeof (APInvoice.prebookAcctID), AvoidControlAccounts = true)]
  public virtual int? PrebookAcctID
  {
    get => this._PrebookAcctID;
    set => this._PrebookAcctID = value;
  }

  [PXDefault(typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<APQuickCheck.vendorID>>>>), SourceField = typeof (PX.Objects.AP.Vendor.prebookSubID), PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (APQuickCheck.prebookAcctID), DisplayName = "Reclassification Subaccount", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Sub.description), BqlField = typeof (APInvoice.prebookSubID))]
  public virtual int? PrebookSubID
  {
    get => this._PrebookSubID;
    set => this._PrebookSubID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.AP.Standalone.APInvoice.EntityUsageType" />
  [PXDefault("0", typeof (Search2<PX.Objects.CR.Location.cAvalaraCustomerUsageType, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<APQuickCheck.branchID>>>>))]
  [PXDBString(1, IsFixed = true, BqlField = typeof (APInvoice.entityUsageType))]
  [TXAvalaraCustomerUsageType.List]
  public virtual string EntityUsageType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.Standalone.APInvoice.ExternalTaxExemptionNumber" />
  [PXDefault(typeof (Search2<PX.Objects.CR.Location.cAvalaraExemptionNumber, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<BAccountR.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<APQuickCheck.branchID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBString(30, IsUnicode = true, BqlField = typeof (APInvoice.externalTaxExemptionNumber))]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (APPayment.docType))]
  [PXRestriction]
  [PXDefault]
  public virtual string APPaymentDocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (APPayment.refNbr))]
  [PXRestriction]
  [PXDefault]
  public virtual string APPaymentRefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.AP.Standalone.APPayment.PaymentMethodID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (APPayment.paymentMethodID))]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.paymentMethodID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APQuickCheck.vendorLocationID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.externalPaymentProcessor>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXDefault(typeof (Coalesce<Search2<PX.Objects.CR.Location.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CR.Location.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CR.Location.vPaymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APQuickCheck.vendorLocationID>>, And<PX.Objects.CR.Location.vPaymentMethodID, Equal<Current<APQuickCheck.paymentMethodID>>>>>>, Search2<PaymentMethodAccount.cashAccountID, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>, Where<PaymentMethodAccount.paymentMethodID, Equal<Current<APQuickCheck.paymentMethodID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<APQuickCheck.branchID>>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.aPIsDefault, Equal<True>>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [CashAccount(typeof (APQuickCheck.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And2<Where<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, Or<BqlField<APQuickCheck.docType, IBqlString>.FromCurrent, Equal<APDocType.cashReturn>>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<APQuickCheck.paymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>), Visibility = PXUIVisibility.Visible, BqlField = typeof (APPayment.cashAccountID))]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [Branch(null, null, true, true, true, IsDetail = false, BqlField = typeof (APRegister.branchID))]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<APQuickCheck.branchID>, PX.Data.IsNotNull>, Null, Case<Where<APQuickCheck.vendorLocationID, PX.Data.IsNotNull, And<Selector<APQuickCheck.vendorLocationID, PX.Objects.CR.Location.vBranchID>, PX.Data.IsNotNull>>, Selector<APQuickCheck.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<Current2<APQuickCheck.branchID>, PX.Data.IsNotNull>, Current2<APQuickCheck.branchID>>>>, Current<AccessInfo.branchID>>))]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (APRegister.curyID))]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public override string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (APPayment.extRefNbr))]
  [PXUIField(DisplayName = "Payment Ref.", Visibility = PXUIVisibility.SelectorVisible)]
  [PaymentRef(typeof (APQuickCheck.cashAccountID), typeof (APQuickCheck.paymentMethodID), typeof (APQuickCheck.stubCntr), Table = typeof (APPayment))]
  public virtual string ExtRefNbr { get; set; }

  [PXDBDate(BqlField = typeof (APPayment.adjDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? AdjDate
  {
    get => this._AdjDate;
    set => this._AdjDate = value;
  }

  [APOpenPeriod(typeof (APQuickCheck.adjDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, new System.Type[] {typeof (PeriodKeyProviderBase.SourceSpecification<APQuickCheck.branchID, True>), typeof (PeriodKeyProviderBase.SourceSpecification<APQuickCheck.cashAccountID, Selector<APQuickCheck.cashAccountID, PX.Objects.CA.CashAccount.branchID>, False>)}, typeof (APQuickCheck.adjTranPeriodID), IsHeader = true, BqlField = typeof (APPayment.adjFinPeriodID))]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string AdjFinPeriodID
  {
    get => this._AdjFinPeriodID;
    set => this._AdjFinPeriodID = value;
  }

  [PeriodID(null, null, null, true, BqlField = typeof (APPayment.adjTranPeriodID))]
  public virtual string AdjTranPeriodID
  {
    get => this._AdjTranPeriodID;
    set => this._AdjTranPeriodID = value;
  }

  [PXDBInt(BqlField = typeof (APPayment.stubCntr))]
  [PXDefault(0)]
  public virtual int? StubCntr
  {
    get => this._StubCntr;
    set => this._StubCntr = value;
  }

  [PXDBInt(BqlField = typeof (APPayment.billCntr))]
  [PXDefault(0)]
  public virtual int? BillCntr
  {
    get => this._BillCntr;
    set => this._BillCntr = value;
  }

  [PXDBBool(BqlField = typeof (APPayment.cleared))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  [PXDBDate(BqlField = typeof (APPayment.clearDate))]
  [PXUIField(DisplayName = "Clear Date", Visibility = PXUIVisibility.Visible)]
  public virtual System.DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBLong(BqlField = typeof (APPayment.cATranID))]
  [APQuickCheckCashTranID]
  public virtual long? CATranID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Discounted Tax Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APQuickCheck.curyInfoID), typeof (APQuickCheck.origTaxDiscAmt), BqlField = typeof (APPayment.curyOrigTaxDiscAmt))]
  public Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury(BqlField = typeof (APPayment.origTaxDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public Decimal? OrigTaxDiscAmt { get; set; }

  [PXDBInt(BqlField = typeof (APPayment.chargeCntr))]
  [PXDefault(0)]
  public virtual int? ChargeCntr
  {
    get => this._ChargeCntr;
    set => this._ChargeCntr = value;
  }

  [PXDBDate(BqlField = typeof (APRegister.docDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public override System.DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PeriodID(null, null, null, true, BqlField = typeof (PX.Objects.AP.APRegister.tranPeriodID))]
  public override string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [APOpenPeriod(typeof (APQuickCheck.docDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, new System.Type[] {typeof (PeriodKeyProviderBase.SourceSpecification<APQuickCheck.branchID, True>), typeof (PeriodKeyProviderBase.SourceSpecification<APQuickCheck.cashAccountID, Selector<APQuickCheck.cashAccountID, PX.Objects.CA.CashAccount.branchID>, False>)}, typeof (APQuickCheck.tranPeriodID), BqlField = typeof (APRegister.finPeriodID))]
  [PXDefault]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? VoidAppl
  {
    [PXDependsOnFields(new System.Type[] {typeof (APQuickCheck.docType)})] get
    {
      return new bool?(APPaymentType.VoidAppl(this._DocType));
    }
    set
    {
      if (!value.Value)
        return;
      this._DocType = APPaymentType.GetVoidingAPDocType(this.DocType) ?? "VCK";
    }
  }

  [PXDBBool(BqlField = typeof (APPayment.printCheck))]
  [FormulaDefault(typeof (IsNull<IIf<Where<PX.Objects.AP.APRegister.isMigratedRecord, Equal<True>>, False, Selector<APQuickCheck.paymentMethodID, PX.Objects.CA.PaymentMethod.printOrExport>>, False>))]
  [PXDefault]
  [PXUIField(DisplayName = "Print Check")]
  public virtual bool? PrintCheck { get; set; }

  /// <summary>
  /// Indicates that this check under printing processing to prevent update <see cref="T:PX.Objects.CA.CashAccountCheck" /> table by <see cref="T:PX.Objects.AP.PaymentRefAttribute" /> /&gt;
  /// </summary>
  [PXBool]
  public virtual bool? IsPrintingProcess { get; set; }

  /// <summary>
  /// Indicates that this check under release processing to prevent the question about the saving of the last check number by <see cref="T:PX.Objects.AP.PaymentRefAttribute" /> /&gt;
  /// </summary>
  [PXBool]
  public virtual bool? IsReleaseCheckProcess { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APRegister.printed))]
  [PXDefault]
  [PXFormula(typeof (IIf<Where<APQuickCheck.printCheck, NotEqual<True>, And<Selector<APQuickCheck.paymentMethodID, PX.Objects.CA.PaymentMethod.printOrExport>, Equal<True>, Or<Selector<APQuickCheck.paymentMethodID, PX.Objects.CA.PaymentMethod.printOrExport>, NotEqual<True>>>>, True, False>))]
  public override bool? Printed
  {
    get => this._Printed;
    set => this._Printed = value;
  }

  [PXDBBool(BqlField = typeof (APPayment.depositAsBatch))]
  [PXUIField(DisplayName = "Batch Deposit", Enabled = false)]
  [PXDefault(false, typeof (Search<PX.Objects.CA.CashAccount.clearingAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<APQuickCheck.cashAccountID>>>>))]
  public virtual bool? DepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  [PXDBDate(BqlField = typeof (APPayment.depositAfter))]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Deposit After", Enabled = false, Visible = false)]
  public virtual System.DateTime? DepositAfter
  {
    get => this._DepositAfter;
    set => this._DepositAfter = value;
  }

  [PXDBBool(BqlField = typeof (APPayment.deposited))]
  [PXUIField(DisplayName = "Deposited", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Deposited
  {
    get => this._Deposited;
    set => this._Deposited = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Batch Deposit Date", Enabled = false)]
  public virtual System.DateTime? DepositDate
  {
    get => this._DepositDate;
    set => this._DepositDate = value;
  }

  [PXUIField(Enabled = false)]
  [PXDBString(3, IsFixed = true, BqlField = typeof (APPayment.depositType))]
  public virtual string DepositType
  {
    get => this._DepositType;
    set => this._DepositType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (APPayment.depositNbr))]
  [PXUIField(DisplayName = "Batch Deposit Nbr.", Enabled = false)]
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  [PXBool]
  [RestrictWithholdingTaxCalcMode(typeof (APQuickCheck.taxZoneID), typeof (APQuickCheck.taxCalcMode))]
  public virtual bool? HasWithHoldTax
  {
    get => this._HasWithHoldTax;
    set => this._HasWithHoldTax = value;
  }

  [PXBool]
  [RestrictUseTaxCalcMode(typeof (APQuickCheck.taxZoneID), typeof (APQuickCheck.taxCalcMode))]
  public virtual bool? HasUseTax
  {
    get => this._HasUseTax;
    set => this._HasUseTax = value;
  }

  /// <inheritdoc cref="P:PX.Objects.AP.Standalone.APPayment.ExternalPaymentID" />
  [PXDBString(50, BqlField = typeof (APPayment.externalPaymentID))]
  [PXUIField(DisplayName = "External Payment ID", IsReadOnly = true)]
  public virtual string ExternalPaymentID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.Standalone.APPayment.ExternalPaymentStatus" />
  [PXDBString(50, BqlField = typeof (APPayment.externalPaymentStatus))]
  [PXUIField(DisplayName = "External Payment Status", IsReadOnly = true)]
  public virtual string ExternalPaymentStatus { get; set; }

  public new class PK : PrimaryKeyOf<APQuickCheck>.By<APQuickCheck.docType, APQuickCheck.refNbr>
  {
    public static APQuickCheck Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APQuickCheck>.By<APQuickCheck.docType, APQuickCheck.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.vendorID, APQuickCheck.vendorLocationID>
    {
    }

    public class SuppliedByVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.suppliedByVendorID>
    {
    }

    public class SuppliedByVendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.suppliedByVendorID, APQuickCheck.suppliedByVendorLocationID>
    {
    }

    public class RemittanceAddress : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.remitAddressID>
    {
    }

    public class RemittanceContact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.remitContactID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.curyID>
    {
    }

    public class APAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.aPAccountID>
    {
    }

    public class APSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.aPSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.scheduleID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<APQuickCheck>.By<APQuickCheck.termsID>
    {
    }
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.refNbr>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.vendorID>
  {
  }

  public new abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APQuickCheck.vendorLocationID>
  {
  }

  public abstract class suppliedByVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APQuickCheck.suppliedByVendorID>
  {
  }

  public abstract class suppliedByVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APQuickCheck.suppliedByVendorLocationID>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.remitContactID>
  {
  }

  public new abstract class aPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.aPAccountID>
  {
  }

  public new abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.aPSubID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.termsID>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.lineCntr>
  {
  }

  public new abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.adjCntr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APQuickCheck.curyInfoID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.origDocAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.taxAmt>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.docBal>
  {
  }

  public new abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyOrigDiscAmt>
  {
  }

  public new abstract class origDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.origDiscAmt>
  {
  }

  public new abstract class curyDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyDiscTaken>
  {
  }

  public new abstract class discTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.discTaken>
  {
  }

  public new abstract class curyDiscBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.discBal>
  {
  }

  public new abstract class curyOrigWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyOrigWhTaxAmt>
  {
  }

  public new abstract class origWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.origWhTaxAmt>
  {
  }

  public new abstract class curyWhTaxBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyWhTaxBal>
  {
  }

  public new abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.whTaxBal>
  {
  }

  public new abstract class curyTaxWheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyTaxWheld>
  {
  }

  public new abstract class taxWheld : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.taxWheld>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.docDesc>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APQuickCheck.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APQuickCheck.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APQuickCheck.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APQuickCheck.lastModifiedDateTime>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APQuickCheck.Tstamp>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.batchNbr>
  {
  }

  public new abstract class prebookBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.prebookBatchNbr>
  {
  }

  public new abstract class voidBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.voidBatchNbr>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.status>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.hold>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.scheduled>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.voided>
  {
  }

  public new abstract class prebooked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.prebooked>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APQuickCheck.noteID>
  {
  }

  public new abstract class closedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APQuickCheck.closedDate>
  {
  }

  public new abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.closedFinPeriodID>
  {
  }

  public new abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.closedTranPeriodID>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.rGOLAmt>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.scheduleID>
  {
  }

  public new abstract class impRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.impRefNbr>
  {
  }

  public abstract class aPInvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.aPInvoiceDocType>
  {
  }

  public abstract class aPInvoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.aPInvoiceRefNbr>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.invoiceNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APQuickCheck.invoiceDate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.taxZoneID>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.taxTotal>
  {
  }

  public abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyLineTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APQuickCheck.lineTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.vatTaxableTotal>
  {
  }

  public abstract class separateCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.separateCheck>
  {
  }

  public abstract class paySel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.paySel>
  {
  }

  public abstract class prebookAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.prebookAcctID>
  {
  }

  public abstract class prebookSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.prebookSubID>
  {
  }

  public abstract class entityUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.entityUsageType>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.externalTaxExemptionNumber>
  {
  }

  public abstract class aPPaymentDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.aPPaymentDocType>
  {
  }

  public abstract class aPPaymentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.aPPaymentRefNbr>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.cashAccountID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.branchID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.curyID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.extRefNbr>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APQuickCheck.adjDate>
  {
  }

  public abstract class adjFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.adjTranPeriodID>
  {
  }

  public abstract class stubCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.stubCntr>
  {
  }

  public abstract class billCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.billCntr>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APQuickCheck.clearDate>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APQuickCheck.cATranID>
  {
  }

  public abstract class curyOrigTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.curyOrigTaxDiscAmt>
  {
  }

  public abstract class origTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APQuickCheck.origTaxDiscAmt>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APQuickCheck.chargeCntr>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APQuickCheck.docDate>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.tranPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.finPeriodID>
  {
  }

  public new abstract class vendorID_Vendor_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.vendorID_Vendor_acctName>
  {
  }

  public abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.voidAppl>
  {
  }

  public abstract class printCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.printCheck>
  {
  }

  public abstract class isPrintingProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APQuickCheck.isPrintingProcess>
  {
  }

  public abstract class isReleaseProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APQuickCheck.isReleaseProcess>
  {
  }

  public new abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.printed>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.depositAsBatch>
  {
  }

  public abstract class depositAfter : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APQuickCheck.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.deposited>
  {
  }

  public abstract class depositDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APQuickCheck.depositDate>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.depositNbr>
  {
  }

  public new abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APQuickCheck.taxCalcMode>
  {
  }

  public abstract class hasWithHoldTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.hasWithHoldTax>
  {
  }

  public abstract class hasUseTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APQuickCheck.hasUseTax>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APQuickCheck.hasMultipleProjects>
  {
  }

  public abstract class externalPaymentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.externalPaymentID>
  {
  }

  public abstract class externalPaymentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APQuickCheck.externalPaymentStatus>
  {
  }
}
