// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARCashSale
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.CA;
using PX.Objects.CC;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Interfaces;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Descriptor;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.AR.Standalone;

[PXCacheName("Cash Sale")]
[PXProjection(typeof (Select2<ARRegister, InnerJoin<ARInvoice, On<ARInvoice.docType, Equal<ARRegister.docType>, And<ARInvoice.refNbr, Equal<ARRegister.refNbr>>>, InnerJoin<ARPayment, On<ARPayment.docType, Equal<ARRegister.docType>, And<ARPayment.refNbr, Equal<ARRegister.refNbr>>>>>, Where<ARRegister.docType, Equal<ARDocType.cashSale>, Or<ARRegister.docType, Equal<ARDocType.cashReturn>>>>), Persistent = true, CommerceTranType = typeof (ARInvoice))]
[PXSubstitute(GraphType = typeof (ARCashSaleEntry))]
[PXPrimaryGraph(typeof (ARCashSaleEntry))]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ARCashSale.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class ARCashSale : ARRegister, ICCPayment, IApprovable, IAssign, IApprovalDescription
{
  protected int? _BillAddressID;
  protected 
  #nullable disable
  string _InvoiceNbr;
  protected DateTime? _InvoiceDate;
  protected string _TaxZoneID;
  protected string _AvalaraCustomerUsageType;
  protected string _MasterRefNbr;
  protected short? _InstallmentNbr;
  protected Decimal? _CuryTaxTotal;
  protected Decimal? _TaxTotal;
  protected Decimal? _CuryLineTotal;
  protected Decimal? _CuryVatExemptTotal;
  protected Decimal? _VatExemptTotal;
  protected Decimal? _CuryVatTaxableTotal;
  protected Decimal? _VatTaxableTotal;
  protected Decimal? _LineTotal;
  protected Decimal? _CommnPct;
  protected Decimal? _CuryCommnAmt;
  protected Decimal? _CommnAmt;
  protected Decimal? _CuryCommnblAmt;
  protected Decimal? _CommnblAmt;
  protected bool? _CreditHold;
  protected bool? _ApprovedCredit;
  protected Decimal? _ApprovedCreditAmt;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected int? _ProjectID;
  protected bool? _ApplyOverdueCharge;
  protected string _PaymentMethodID;
  protected int? _PMInstanceID;
  protected int? _CashAccountID;
  protected string _ExtRefNbr;
  protected DateTime? _AdjDate;
  protected string _AdjFinPeriodID;
  protected string _AdjTranPeriodID;
  protected bool? _Cleared;
  protected DateTime? _ClearDate;
  protected bool? _DepositAsBatch;
  protected int? _ChargeCntr;
  protected bool? _Deposited;
  protected DateTime? _DepositDate;
  protected string _DepositType;
  protected string _DepositNbr;
  protected int? _PaymentProjectID;
  protected int? _PaymentTaskID;
  protected Decimal? _CuryConsolidateChargeTotal;
  protected Decimal? _ConsolidateChargeTotal;
  protected string _RefTranExtNbr;
  protected string _CCPaymentStateDescr;

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARRegister.docType))]
  [PXDefault]
  [ARCashSaleType.List]
  [PXUIField]
  [PXFieldDescription]
  [PXDependsOnFields(new System.Type[] {typeof (ARCashSale.aRInvoiceDocType), typeof (ARCashSale.aRPaymentDocType)})]
  public override string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", IsUnicode = true, BqlField = typeof (ARRegister.refNbr))]
  [PXDefault]
  [PXUIField]
  [ARCashSaleType.RefNbr(typeof (Search2<ARCashSale.refNbr, InnerJoinSingleTable<PX.Objects.AR.Customer, On<ARCashSale.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<ARCashSale.docType, Equal<Current<ARCashSale.docType>>, And2<Where<PX.Objects.AR.ARRegister.origModule, NotEqual<BatchModule.moduleSO>, Or<ARCashSale.released, Equal<True>>>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<ARCashSale.refNbr>>>), Filterable = true)]
  [ARCashSaleType.Numbering]
  [PXFieldDescription]
  [PXDependsOnFields(new System.Type[] {typeof (ARCashSale.aRInvoiceRefNbr), typeof (ARCashSale.aRPaymentRefNbr)})]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [CustomerActive]
  [PXDefault]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARCashSale.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>))]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>>))]
  public override int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDefault]
  [Account(typeof (ARCashSale.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AR Account", BqlField = typeof (ARRegister.aRAccountID), ControlAccountForModule = "AR")]
  public override int? ARAccountID
  {
    get => this._ARAccountID;
    set => this._ARAccountID = value;
  }

  [PXDefault]
  [SubAccount(typeof (ARCashSale.aRAccountID))]
  public override int? ARSubID
  {
    get => this._ARSubID;
    set => this._ARSubID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (ARInvoice.termsID))]
  [PXDefault(typeof (Search<PX.Objects.AR.Customer.termsID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARCashSale.customerID>>>>))]
  [PXUIField]
  [ARTermsSelector]
  [Terms(typeof (ARCashSale.docDate), null, null, typeof (ARCashSale.curyDocBal), typeof (ARCashSale.curyOrigDiscAmt), typeof (ARCashSale.curyTaxTotal), typeof (ARCashSale.curyOrigTaxDiscAmt), typeof (ARCashSale.branchID))]
  public virtual string TermsID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARRegister.lineCntr))]
  [PXDefault(0)]
  public override int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBLong(BqlField = typeof (ARRegister.curyInfoID))]
  [CurrencyInfo]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.origDocAmt), BqlField = typeof (PX.Objects.AR.ARRegister.curyOrigDocAmt))]
  [PXUIField]
  public override Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AR.ARRegister.origDocAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.docBal), BaseCalc = false, BqlField = typeof (PX.Objects.AR.ARRegister.curyDocBal))]
  [PXUIField]
  public override Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AR.ARRegister.docBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.origDiscAmt), BqlField = typeof (PX.Objects.AR.ARRegister.curyOrigDiscAmt))]
  [PXUIField]
  public override Decimal? CuryOrigDiscAmt
  {
    get => this._CuryOrigDiscAmt;
    set => this._CuryOrigDiscAmt = value;
  }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AR.ARRegister.origDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? OrigDiscAmt
  {
    get => this._OrigDiscAmt;
    set => this._OrigDiscAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.discTaken), BqlField = typeof (PX.Objects.AR.ARRegister.curyDiscTaken))]
  public override Decimal? CuryDiscTaken
  {
    get => this._CuryDiscTaken;
    set => this._CuryDiscTaken = value;
  }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AR.ARRegister.discTaken))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscTaken
  {
    get => this._DiscTaken;
    set => this._DiscTaken = value;
  }

  [PXUIField]
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.discBal), BaseCalc = false, BqlField = typeof (PX.Objects.AR.ARRegister.curyDiscBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AR.ARRegister.discBal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (ARRegister.docDesc))]
  [PXUIField]
  public override string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
  }

  [PXDBCreatedByID(BqlField = typeof (PX.Objects.AR.ARRegister.createdByID))]
  public override Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID(BqlField = typeof (PX.Objects.AR.ARRegister.createdByScreenID))]
  public override string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime(BqlField = typeof (PX.Objects.AR.ARRegister.createdDateTime))]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public override DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.AR.ARRegister.lastModifiedByID))]
  public override Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.AR.ARRegister.lastModifiedByScreenID))]
  public override string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.AR.ARRegister.lastModifiedDateTime))]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public override DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp(BqlField = typeof (PX.Objects.AR.ARRegister.Tstamp), RecordComesFirst = true)]
  public override byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARRegister.batchNbr))]
  [PXUIField]
  [PX.Objects.GL.BatchNbr(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (PX.Objects.AR.ARRegister.isMigratedRecord))]
  public override string BatchNbr { get; set; }

  [PXDBShort(BqlField = typeof (PX.Objects.AR.ARRegister.batchSeq))]
  [PXDefault(0)]
  public override short? BatchSeq
  {
    get => this._BatchSeq;
    set => this._BatchSeq = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (ARRegister.status))]
  [PXDefault("H")]
  [PXUIField]
  [ARDocStatus.List]
  public override string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool(BqlField = typeof (ARRegister.released))]
  [PXDefault(false)]
  public override bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool(BqlField = typeof (ARRegister.openDoc))]
  [PXDefault(true)]
  public override bool? OpenDoc
  {
    get => this._OpenDoc;
    set => this._OpenDoc = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.hold))]
  [PXUIField]
  [PXDefault(true, typeof (Search<ARSetup.holdEntry>))]
  public override bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.approved))]
  [PXDefault(false)]
  public override bool? Approved { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.rejected))]
  [PXDefault(false)]
  public override bool? Rejected { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.scheduled))]
  [PXDefault(false)]
  public override bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.voided))]
  [PXDefault(false)]
  public override bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [PXSearchable(2, "AR {0}: {1} - {3}", new System.Type[] {typeof (ARCashSale.docType), typeof (ARCashSale.refNbr), typeof (ARCashSale.customerID), typeof (PX.Objects.AR.Customer.acctName), typeof (ARCashSale.aRInvoiceDocType), typeof (ARCashSale.aRInvoiceRefNbr), typeof (ARCashSale.aRPaymentDocType), typeof (ARCashSale.aRPaymentRefNbr)}, new System.Type[] {typeof (ARCashSale.extRefNbr), typeof (ARCashSale.docDesc)}, NumberFields = new System.Type[] {typeof (ARCashSale.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (ARCashSale.docDate), typeof (ARCashSale.status), typeof (ARCashSale.invoiceNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (ARCashSale.docDesc)}, MatchWithJoin = typeof (InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ARCashSale.customerID>>>), SelectForFastIndexing = typeof (Select2<ARCashSale, InnerJoin<PX.Objects.AR.Customer, On<ARCashSale.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>))]
  [PXNote(BqlField = typeof (PX.Objects.AR.ARRegister.noteID))]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBGuid(false, BqlField = typeof (ARInvoice.refNoteID))]
  public override Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARRegister.closedDate))]
  [PXUIField]
  public override DateTime? ClosedDate { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (ARCashSale.branchID), null, null, null, null, true, false, null, typeof (ARCashSale.closedTranPeriodID), null, true, true, BqlField = typeof (ARRegister.closedFinPeriodID))]
  [PXUIField]
  public override string ClosedFinPeriodID
  {
    get => this._ClosedFinPeriodID;
    set => this._ClosedFinPeriodID = value;
  }

  [PeriodID(null, null, null, true, BqlField = typeof (PX.Objects.AR.ARRegister.closedTranPeriodID))]
  [PXUIField]
  public override string ClosedTranPeriodID
  {
    get => this._ClosedTranPeriodID;
    set => this._ClosedTranPeriodID = value;
  }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AR.ARRegister.rGOLAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARRegister.scheduleID))]
  public override string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARRegister.impRefNbr))]
  public override string ImpRefNbr
  {
    get => this._ImpRefNbr;
    set => this._ImpRefNbr = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARRegister.statementDate))]
  public override DateTime? StatementDate
  {
    get => this._StatementDate;
    set => this._StatementDate = value;
  }

  [SalesPerson(BqlField = typeof (PX.Objects.AR.ARRegister.salesPersonID), DisplayName = "Default Salesperson")]
  [PXDefault(typeof (Search<CustDefSalesPeople.salesPersonID, Where<CustDefSalesPeople.bAccountID, Equal<Current<ARRegister.customerID>>, And<CustDefSalesPeople.locationID, Equal<Current<PX.Objects.AR.ARRegister.customerLocationID>>, And<CustDefSalesPeople.isDefault, Equal<True>>>>>))]
  public override int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDBString(3, IsFixed = true, BqlField = typeof (ARInvoice.docType))]
  [PXDefault]
  [PXRestriction]
  public virtual string ARInvoiceDocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (ARInvoice.refNbr))]
  [PXDefault]
  [PXRestriction]
  public virtual string ARInvoiceRefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(BqlField = typeof (ARInvoice.billAddressID))]
  [ARAddress(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AR.Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defBillAddressID>>>, LeftJoin<ARAddress, On<ARAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<ARAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<ARAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<ARAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARCashSale.customerID>>>>))]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  [PXDBInt(BqlField = typeof (ARInvoice.billContactID))]
  [PXSelector(typeof (ARContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Billing Contact", Visible = false)]
  [ARContact(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AR.Customer.defLocationID>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defBillContactID>>>, LeftJoin<ARContact, On<ARContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<ARContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<ARContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<ARContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARCashSale.customerID>>>>))]
  public virtual int? BillContactID { get; set; }

  [PXDBInt(BqlField = typeof (ARInvoice.shipAddressID))]
  [ARShippingAddress(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<ARCashSale.customerLocationID>>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<ARShippingAddress, On<ARShippingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<ARShippingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<ARShippingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<ARShippingAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARCashSale.customerID>>>>))]
  public virtual int? ShipAddressID { get; set; }

  [PXDBInt(BqlField = typeof (ARInvoice.shipContactID))]
  [PXSelector(typeof (ARShippingContact.contactID), ValidateValue = false)]
  [PXUIField(DisplayName = "Shipping Contact", Visible = false)]
  [ARShippingContact(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<ARCashSale.customerLocationID>>>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<ARShippingContact, On<ARShippingContact.customerID, Equal<PX.Objects.CR.Contact.bAccountID>, And<ARShippingContact.customerContactID, Equal<PX.Objects.CR.Contact.contactID>, And<ARShippingContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<ARShippingContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARCashSale.customerID>>>>))]
  public virtual int? ShipContactID { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (ARInvoice.invoiceNbr))]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  [PXDBDate(BqlField = typeof (ARInvoice.invoiceDate))]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  public virtual DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (ARInvoice.taxZoneID))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXFormula(typeof (Default<ARCashSale.customerLocationID>))]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>
  /// The tax exemption number for reporting purposes. The field is used if the system is integrated with External Tax Calculation
  /// and the <see cref="P:PX.Objects.CS.FeaturesSet.AvalaraTax">External Tax Calculation Integration</see> feature is enabled.
  /// </summary>
  [PXDefault(typeof (Search<PX.Objects.CR.Location.cAvalaraExemptionNumber, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARCashSale.customerLocationID>>>>>))]
  [PXDBString(30, IsUnicode = true, BqlField = typeof (ARInvoice.externalTaxExemptionNumber))]
  [PXUIField(DisplayName = "Tax Exemption Number")]
  public virtual string ExternalTaxExemptionNumber { get; set; }

  [PXDefault("0", typeof (Search<PX.Objects.CR.Location.cAvalaraCustomerUsageType, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARCashSale.customerLocationID>>>>>))]
  [PXDBString(1, IsFixed = true, BqlField = typeof (ARInvoice.avalaraCustomerUsageType))]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType
  {
    get => this._AvalaraCustomerUsageType;
    set => this._AvalaraCustomerUsageType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (ARInvoice.masterRefNbr))]
  public virtual string MasterRefNbr
  {
    get => this._MasterRefNbr;
    set => this._MasterRefNbr = value;
  }

  [PXDBShort(BqlField = typeof (ARInvoice.installmentNbr))]
  public virtual short? InstallmentNbr
  {
    get => this._InstallmentNbr;
    set => this._InstallmentNbr = value;
  }

  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.taxTotal), BqlField = typeof (ARInvoice.curyTaxTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal
  {
    get => this._CuryTaxTotal;
    set => this._CuryTaxTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.taxTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal
  {
    get => this._TaxTotal;
    set => this._TaxTotal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.origTaxDiscAmt), BqlField = typeof (ARPayment.curyOrigTaxDiscAmt))]
  public Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (ARPayment.origTaxDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? OrigTaxDiscAmt { get; set; }

  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.lineTotal), BqlField = typeof (ARInvoice.curyLineTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.vatExemptTotal), BqlField = typeof (ARInvoice.curyVatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal
  {
    get => this._CuryVatExemptTotal;
    set => this._CuryVatExemptTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.vatExemptTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal
  {
    get => this._VatExemptTotal;
    set => this._VatExemptTotal = value;
  }

  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.vatTaxableTotal), BqlField = typeof (ARInvoice.curyVatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal
  {
    get => this._CuryVatTaxableTotal;
    set => this._CuryVatTaxableTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.vatTaxableTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal
  {
    get => this._VatTaxableTotal;
    set => this._VatTaxableTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.lineTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  [PXDBDecimal(6, BqlField = typeof (ARInvoice.commnPct))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.commnAmt), BqlField = typeof (ARInvoice.curyCommnAmt))]
  [PXUIField(DisplayName = "Commission Amt.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCommnAmt
  {
    get => this._CuryCommnAmt;
    set => this._CuryCommnAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.commnAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.commnblAmt), BqlField = typeof (ARInvoice.curyCommnblAmt))]
  [PXUIField(DisplayName = "Total Commissionable", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCommnblAmt
  {
    get => this._CuryCommnblAmt;
    set => this._CuryCommnblAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.commnblAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnblAmt
  {
    get => this._CommnblAmt;
    set => this._CommnblAmt = value;
  }

  [PXDBBool(BqlField = typeof (ARInvoice.creditHold))]
  [PXDefault(false)]
  public virtual bool? CreditHold
  {
    get => this._CreditHold;
    set => this._CreditHold = value;
  }

  [PXDBBool(BqlField = typeof (ARInvoice.approvedCredit))]
  [PXDefault(false)]
  public virtual bool? ApprovedCredit
  {
    get => this._ApprovedCredit;
    set => this._ApprovedCredit = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARInvoice.approvedCreditAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ApprovedCreditAmt
  {
    get => this._ApprovedCreditAmt;
    set => this._ApprovedCreditAmt = value;
  }

  [PXCompanyTreeSelector]
  [PXDefault(typeof (PX.Objects.CR.BAccount.workgroupID))]
  [PXDBInt(BqlField = typeof (ARInvoice.workgroupID))]
  [PXUIField(DisplayName = "Workgroup ID")]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [PXDefault(typeof (PX.Objects.CR.BAccount.ownerID))]
  [Owner(typeof (ARCashSale.workgroupID), BqlField = typeof (ARInvoice.ownerID), DisplayName = "Owner ID")]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [ProjectDefault("AR", typeof (Search<PX.Objects.CR.Location.cDefProjectID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARCashSale.customerLocationID>>>>>))]
  [PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(typeof (ARCashSale.customerID), BqlField = typeof (ARInvoice.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The total <see cref="T:PX.Objects.AR.Standalone.ARCashSale.lineDiscTotal">discount of the document</see> (in the currency of the document),
  /// which is calculated as the sum of line discounts of the order.
  /// </summary>
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.lineDiscTotal), BqlField = typeof (ARInvoice.curyLineDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false, Required = false)]
  public virtual Decimal? CuryLineDiscTotal { get; set; }

  /// <summary>
  /// The total line discount of the document, which is calculated as the sum of line discounts of the invoice.
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (ARInvoice.lineDiscTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Discounts", Enabled = false)]
  public virtual Decimal? LineDiscTotal { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.AR.Standalone.ARCashSale.goodsExtPriceTotal">total amount</see> on all lines of the document, except for Misc. Charges and empty or null Line Types
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.goodsExtPriceTotal), BqlField = typeof (ARInvoice.curyGoodsExtPriceTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Goods")]
  public virtual Decimal? CuryGoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The total amount on all lines of the document, except for Misc. Charges and empty or null Line Types
  /// (in base currency).
  /// </summary>
  [PXDBBaseCury(BqlField = typeof (ARInvoice.goodsExtPriceTotal))]
  public virtual Decimal? GoodsExtPriceTotal { get; set; }

  /// <summary>
  /// The <see cref="!:miscTot">total amount</see> calculated as the sum of the amounts in
  /// <see cref="T:PX.Objects.AR.ARTran.curyExtPrice">Ext. Price</see>
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.miscExtPriceTotal), BqlField = typeof (ARInvoice.curyMiscExtPriceTotal))]
  [PXUIField(DisplayName = "Misc. Charges", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryMiscExtPriceTotal { get; set; }

  /// <summary>
  ///  The total amount calculated as the sum of the amounts in <see cref="T:PX.Objects.AR.ARTran.curyExtPrice">Ext. Price</see>
  /// (in base currency).
  ///  </summary>
  [PXDBBaseCury(BqlField = typeof (ARInvoice.miscExtPriceTotal))]
  public virtual Decimal? MiscExtPriceTotal { get; set; }

  /// <summary>Sum of ext value (line total and line discount total)</summary>
  [PXBaseCury]
  [PXFormula(typeof (Add<ARCashSale.lineTotal, ARCashSale.lineDiscTotal>))]
  public virtual Decimal? DetailExtLineTotal { get; set; }

  /// <summary>Sum of ext value (line total and line discount total)</summary>
  [PXCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.detailExtPriceTotal))]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<ARCashSale.curyLineTotal, ARCashSale.curyLineDiscTotal>))]
  [PXUIField(DisplayName = "Detail Total")]
  public virtual Decimal? CuryDetailExtPriceTotal { get; set; }

  [PXDBBool(BqlField = typeof (ARInvoice.applyOverdueCharge))]
  [PXDefault(true)]
  public virtual bool? ApplyOverdueCharge
  {
    get => this._ApplyOverdueCharge;
    set => this._ApplyOverdueCharge = value;
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARInvoice.IsPaymentsTransferred" />
  [PXDBBool(BqlField = typeof (ARInvoice.isPaymentsTransferred))]
  [PXDefault(true)]
  public virtual bool? IsPaymentsTransferred { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (ARPayment.docType))]
  [PXDefault]
  [PXRestriction]
  public virtual string ARPaymentDocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (ARPayment.refNbr))]
  [PXDefault]
  [PXRestriction]
  public virtual string ARPaymentRefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, BqlField = typeof (ARRegister.branchID), TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<ARCashSale.branchID>, IsPending>, Null, Case<Where<ARCashSale.customerLocationID, IsNotNull, And<Selector<ARCashSale.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<ARCashSale.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<Current2<ARCashSale.branchID>, IsNotNull>, Current2<ARCashSale.branchID>>>>, Current<AccessInfo.branchID>>))]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (ARPayment.paymentMethodID))]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>>>>, Search<PX.Objects.AR.Customer.defPaymentMethodID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<ARCashSale.customerID>>>>>))]
  [PXSelector(typeof (Search5<PX.Objects.CA.PaymentMethod.paymentMethodID, LeftJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<ARCashSale.customerID>>>>>, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>>>, Aggregate<GroupBy<PX.Objects.CA.PaymentMethod.paymentMethodID, GroupBy<PX.Objects.CA.PaymentMethod.useForAR, GroupBy<PX.Objects.CA.PaymentMethod.useForAP>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method")]
  [PXForeignReference(typeof (CompositeKey<Field<ARCashSale.customerID>.IsRelatedTo<PX.Objects.AR.CustomerPaymentMethod.bAccountID>, Field<ARCashSale.paymentMethodID>.IsRelatedTo<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>))]
  [PXForeignReference(typeof (Field<ARCashSale.paymentMethodID>.IsRelatedTo<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXDBInt(BqlField = typeof (ARPayment.pMInstanceID))]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current2<ARCashSale.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARCashSale.paymentMethodID>>>>>>, Search2<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, LeftJoin<CCProcessingCenterPmntMethodBranch, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethodBranch.paymentMethodID>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenterPmntMethodBranch.processingCenterID>, And<Current2<ARCashSale.branchID>, Equal<CCProcessingCenterPmntMethodBranch.branchID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<ARCashSale.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARCashSale.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Asc<Switch<Case<Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, IsNull>, True>, False>, Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<ARCashSale.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARCashSale.paymentMethodID>>, And<Where<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, Or<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current<ARCashSale.pMInstanceID>>>>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  [DeprecatedProcessing]
  [DisabledProcCenter]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  [PXDBString(BqlField = typeof (ARPayment.processingCenterID))]
  [PXUIField(DisplayName = "Proc. Center ID")]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<ARCashSale.pMInstanceID>>>>, Coalesce<Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>, InnerJoin<CCProcessingCenterPmntMethodBranch, On<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>, And<CCProcessingCenterPmntMethodBranch.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And2<Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.posTerminal>, And<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<ARCashSale.branchID>>, Or<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<AccessInfo.branchID>>>>>, And<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>, Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isDefault, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>>))]
  [PXSelector(typeof (Search2<CCProcessingCenter.processingCenterID, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenterPmntMethod.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>), DescriptionField = typeof (CCProcessingCenter.name), ValidateValue = false)]
  [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
  [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
  public virtual string ProcessingCenterID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARPayment.TerminalID" />
  [PXDBString(36, IsUnicode = true, InputMask = "", BqlField = typeof (ARPayment.terminalID))]
  [PXUIField(DisplayName = "Terminal")]
  [PXSelector(typeof (Search<CCProcessingCenterTerminal.terminalID, Where<CCProcessingCenterTerminal.processingCenterID, Equal<Current<ARCashSale.processingCenterID>>, And<CCProcessingCenterTerminal.isActive, Equal<True>>>>), new System.Type[] {typeof (CCProcessingCenterTerminal.displayName)}, SubstituteKey = typeof (CCProcessingCenterTerminal.displayName))]
  [PXDefault(typeof (Search2<DefaultTerminal.terminalID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>>, InnerJoin<CCProcessingCenterTerminal, On<CCProcessingCenterTerminal.processingCenterID, Equal<DefaultTerminal.processingCenterID>, And<CCProcessingCenterTerminal.terminalID, Equal<DefaultTerminal.terminalID>>>>>, Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.posTerminal>, And<DefaultTerminal.userID, Equal<Current<AccessInfo.userID>>, And<DefaultTerminal.branchID, Equal<Current<AccessInfo.branchID>>, And<DefaultTerminal.processingCenterID, Equal<Current<ARCashSale.processingCenterID>>, And<CCProcessingCenterTerminal.isActive, Equal<True>>>>>>>))]
  public virtual string TerminalID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARPayment.CardPresent" />
  [PXDBBool(BqlField = typeof (ARPayment.cardPresent))]
  [PXDefault(false)]
  public virtual bool? CardPresent { get; set; }

  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<ARCashSale.pMInstanceID>>>>>, Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.aRIsDefault, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<ARCashSale.paymentMethodID>>>>>>>, Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<ARCashSale.branchID>>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [CashAccount(typeof (ARCashSale.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<ARCashSale.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Match<Current<AccessInfo.userName>>>>))]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (ARRegister.curyID))]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public override string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (ARPayment.extRefNbr))]
  [PXUIField]
  [PXDefault]
  [PaymentRef(typeof (ARCashSale.cashAccountID), typeof (ARCashSale.paymentMethodID), typeof (PX.Objects.AR.ARPayment.updateNextNumber), typeof (PX.Objects.AR.ARRegister.isMigratedRecord))]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBDate(BqlField = typeof (ARPayment.adjDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? AdjDate
  {
    get => this._AdjDate;
    set => this._AdjDate = value;
  }

  [AROpenPeriod(typeof (ARCashSale.adjDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, new System.Type[] {typeof (PeriodKeyProviderBase.SourceSpecification<ARCashSale.branchID, True>), typeof (PeriodKeyProviderBase.SourceSpecification<ARCashSale.cashAccountID, Selector<ARCashSale.cashAccountID, PX.Objects.CA.CashAccount.branchID>, False>)}, typeof (ARCashSale.adjTranPeriodID), IsHeader = true, BqlField = typeof (ARPayment.adjFinPeriodID))]
  [PXUIField]
  public virtual string AdjFinPeriodID
  {
    get => this._AdjFinPeriodID;
    set => this._AdjFinPeriodID = value;
  }

  [PeriodID(null, null, null, true, BqlField = typeof (ARPayment.adjTranPeriodID))]
  public virtual string AdjTranPeriodID
  {
    get => this._AdjTranPeriodID;
    set => this._AdjTranPeriodID = value;
  }

  [PXDBBool(BqlField = typeof (ARPayment.cleared))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  [PXDBDate(BqlField = typeof (ARPayment.clearDate))]
  [PXUIField(DisplayName = "Clear Date")]
  public virtual DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBBool(BqlField = typeof (ARPayment.settled))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Settled")]
  public virtual bool? Settled { get; set; }

  [PXDBLong(BqlField = typeof (ARPayment.cATranID))]
  [ARCashSaleCashTranID]
  public virtual long? CATranID { get; set; }

  [PXDBBool(BqlField = typeof (ARPayment.depositAsBatch))]
  [PXUIField(DisplayName = "Batch Deposit", Enabled = false)]
  [PXDefault(false, typeof (Search<PX.Objects.CA.CashAccount.clearingAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<ARCashSale.cashAccountID>>>>))]
  public virtual bool? DepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  [PXDBInt(BqlField = typeof (ARPayment.chargeCntr))]
  [PXDefault(0)]
  public virtual int? ChargeCntr
  {
    get => this._ChargeCntr;
    set => this._ChargeCntr = value;
  }

  [PXDBDate(BqlField = typeof (ARPayment.depositAfter))]
  [PXDefault]
  [PXUIField(DisplayName = "Deposit After", Enabled = false, Visible = false)]
  public virtual DateTime? DepositAfter { get; set; }

  [PXDBBool(BqlField = typeof (ARPayment.deposited))]
  [PXUIField(DisplayName = "Deposited", Enabled = false)]
  [PXDefault(false)]
  public virtual bool? Deposited
  {
    get => this._Deposited;
    set => this._Deposited = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Batch Deposit Date", Enabled = false)]
  public virtual DateTime? DepositDate
  {
    get => this._DepositDate;
    set => this._DepositDate = value;
  }

  [PXUIField(Enabled = false)]
  [PXDBString(3, IsFixed = true, BqlField = typeof (ARPayment.depositType))]
  public virtual string DepositType
  {
    get => this._DepositType;
    set => this._DepositType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (ARPayment.depositNbr))]
  [PXUIField(DisplayName = "Batch Deposit Nbr.", Enabled = false)]
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  [ProjectDefault("AR")]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(BqlField = typeof (ARPayment.projectID))]
  public virtual int? PaymentProjectID
  {
    get => this._PaymentProjectID;
    set => this._PaymentProjectID = value;
  }

  [ActiveProjectTask(typeof (ARCashSale.paymentProjectID), "AR", DisplayName = "Project Task", BqlField = typeof (ARPayment.taskID))]
  public virtual int? PaymentTaskID
  {
    get => this._PaymentTaskID;
    set => this._PaymentTaskID = value;
  }

  [PXDBCurrency(typeof (ARCashSale.curyInfoID), typeof (ARCashSale.consolidateChargeTotal), BqlField = typeof (ARPayment.curyConsolidateChargeTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryConsolidateChargeTotal
  {
    get => this._CuryConsolidateChargeTotal;
    set => this._CuryConsolidateChargeTotal = value;
  }

  [PXDBDecimal(4, BqlField = typeof (ARPayment.consolidateChargeTotal))]
  public virtual Decimal? ConsolidateChargeTotal
  {
    get => this._ConsolidateChargeTotal;
    set => this._ConsolidateChargeTotal = value;
  }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (ARPayment.refTranExtNbr))]
  [PXDefault]
  [PXSelector(typeof (Search2<PX.Objects.AR.ExternalTransaction.tranNumber, InnerJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ExternalTransaction.docType, Equal<PX.Objects.AR.ARPayment.docType>, And<PX.Objects.AR.ExternalTransaction.refNbr, Equal<PX.Objects.AR.ARPayment.refNbr>>>>, Where<PX.Objects.AR.ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, And2<Where<Current<ARCashSale.pMInstanceID>, IsNull, And<PX.Objects.AR.ARPayment.customerID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.AR.ARPayment.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>>>>, Or<Where<Current<ARCashSale.pMInstanceID>, IsNotNull, And<PX.Objects.AR.ExternalTransaction.pMInstanceID, Equal<Current<ARCashSale.pMInstanceID>>>>>>>, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.transactionID>>>), new System.Type[] {typeof (PX.Objects.AR.ExternalTransaction.transactionID), typeof (PX.Objects.AR.ExternalTransaction.docType), typeof (PX.Objects.AR.ExternalTransaction.refNbr), typeof (PX.Objects.AR.ExternalTransaction.amount)})]
  [PXUIField]
  public virtual string RefTranExtNbr
  {
    get => this._RefTranExtNbr;
    set => this._RefTranExtNbr = value;
  }

  [PXDBBool(BqlField = typeof (ARPayment.isCCAuthorized))]
  [PXDefault(false)]
  public virtual bool? IsCCAuthorized { get; set; }

  [PXDBBool(BqlField = typeof (ARPayment.isCCCaptured))]
  [PXDefault(false)]
  public virtual bool? IsCCCaptured { get; set; }

  [PXDBBool(BqlField = typeof (ARPayment.isCCCaptureFailed))]
  [PXDefault(false)]
  public virtual bool? IsCCCaptureFailed { get; set; }

  [PXDBBool(BqlField = typeof (ARPayment.isCCRefunded))]
  [PXDefault(false)]
  public virtual bool? IsCCRefunded { get; set; }

  [PXDBBool(BqlField = typeof (ARPayment.isCCUserAttention))]
  [PXDefault(false)]
  public virtual bool? IsCCUserAttention { get; set; }

  [PXDBInt(BqlField = typeof (ARPayment.cCActualExternalTransactionID))]
  [PXDBChildIdentity(typeof (PX.Objects.AR.ExternalTransaction.transactionID))]
  public virtual int? CCActualExternalTransactionID { get; set; }

  [PXDBDate(BqlField = typeof (ARRegister.docDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public override DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PeriodID(null, null, null, true, BqlField = typeof (PX.Objects.AR.ARRegister.tranPeriodID))]
  public override string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [PX.Objects.GL.FinPeriodID(typeof (ARCashSale.docDate), typeof (ARCashSale.branchID), null, null, null, null, true, false, null, typeof (ARCashSale.tranPeriodID), null, true, true, BqlField = typeof (ARRegister.finPeriodID))]
  [PXDefault]
  [PXUIField]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? VoidAppl
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARCashSale.docType)})] get
    {
      return new bool?(this._DocType == "RCS");
    }
    set
    {
      if (!value.Value)
        return;
      this._DocType = "RCS";
    }
  }

  [PXDBBool(BqlField = typeof (ARPayment.isCCPayment))]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<PX.Objects.AR.ARRegister.isMigratedRecord, Equal<False>, And<Selector<ARCashSale.paymentMethodID, PX.Objects.CA.PaymentMethod.paymentType>, In3<PaymentMethodType.creditCard, PaymentMethodType.eft, PaymentMethodType.posTerminal>, And<Selector<ARCashSale.paymentMethodID, PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>, Equal<True>>>>, True>, False>))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool? IsCCPayment { get; set; }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Processing Status", Enabled = false, Visible = false)]
  public virtual string CCPaymentStateDescr
  {
    get => this._CCPaymentStateDescr;
    set => this._CCPaymentStateDescr = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Don't Print")]
  public override bool? DontPrint { get; set; }

  [PXDBBool]
  [PXDefault(typeof (IIf<Where<Selector<ARCashSale.paymentMethodID, PX.Objects.CA.PaymentMethod.sendPaymentReceiptsAutomatically>, Equal<True>>, False, True>))]
  [PXUIField(DisplayName = "Don't Email")]
  public override bool? DontEmail { get; set; }

  string ICCPayment.OrigDocType => (string) null;

  string ICCPayment.OrigRefNbr => (string) null;

  Decimal? ICCPayment.CuryDocBal
  {
    get => this.CuryOrigDocAmt;
    set
    {
    }
  }

  public new class PK : PrimaryKeyOf<ARCashSale>.By<ARCashSale.docType, ARCashSale.refNbr>
  {
    public static ARCashSale Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARCashSale) PrimaryKeyOf<ARCashSale>.By<ARCashSale.docType, ARCashSale.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.customerID, ARCashSale.customerLocationID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.curyID>
    {
    }

    public class ARAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.aRAccountID>
    {
    }

    public class ARSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.aRSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.scheduleID>
    {
    }

    public class OriginalDocument : 
      PrimaryKeyOf<PX.Objects.AR.ARRegister>.By<PX.Objects.AR.ARRegister.docType, PX.Objects.AR.ARRegister.refNbr>.ForeignKeyOf<ARCashSale>.By<ARCashSale.origDocType, ARCashSale.origRefNbr>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<ARCashSale>.By<ARCashSale.termsID>
    {
    }
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.refNbr>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.customerID>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCashSale.customerLocationID>
  {
  }

  public new abstract class aRAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.aRAccountID>
  {
  }

  public new abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.aRSubID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.termsID>
  {
  }

  public new abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.lineCntr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARCashSale.curyInfoID>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.origDocAmt>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.curyDocBal>
  {
  }

  public new abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.docBal>
  {
  }

  public new abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyOrigDiscAmt>
  {
  }

  public new abstract class origDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.origDiscAmt>
  {
  }

  public new abstract class curyDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyDiscTaken>
  {
  }

  public new abstract class discTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.discTaken>
  {
  }

  public new abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.curyDiscBal>
  {
  }

  public new abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.discBal>
  {
  }

  public new abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.docDesc>
  {
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARCashSale.createdByID>
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARCashSale.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARCashSale.lastModifiedByID>
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARCashSale.lastModifiedDateTime>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARCashSale.Tstamp>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.batchNbr>
  {
  }

  public new abstract class batchSeq : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARCashSale.batchSeq>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.status>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.released>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.openDoc>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.hold>
  {
  }

  public new abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.approved>
  {
  }

  public new abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.rejected>
  {
  }

  public new abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.scheduled>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.voided>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARCashSale.noteID>
  {
  }

  public new abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARCashSale.refNoteID>
  {
  }

  public new abstract class closedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARCashSale.closedDate>
  {
  }

  public new abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.closedFinPeriodID>
  {
  }

  public new abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.closedTranPeriodID>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.rGOLAmt>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.scheduleID>
  {
  }

  public new abstract class impRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.impRefNbr>
  {
  }

  public new abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARCashSale.statementDate>
  {
  }

  public new abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.salesPersonID>
  {
  }

  public new abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.origDocType>
  {
  }

  public new abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.origRefNbr>
  {
  }

  public abstract class aRInvoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.aRInvoiceDocType>
  {
  }

  public abstract class aRInvoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.aRInvoiceRefNbr>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.billAddressID>
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.billContactID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.shipContactID>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.invoiceNbr>
  {
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARCashSale.invoiceDate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.taxZoneID>
  {
  }

  public new abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.taxCalcMode>
  {
  }

  public abstract class externalTaxExemptionNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.externalTaxExemptionNumber>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.avalaraCustomerUsageType>
  {
  }

  public abstract class masterRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.masterRefNbr>
  {
  }

  public abstract class installmentNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARCashSale.installmentNbr>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.taxTotal>
  {
  }

  public abstract class curyOrigTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyOrigTaxDiscAmt>
  {
  }

  public abstract class origTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.origTaxDiscAmt>
  {
  }

  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.curyLineTotal>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.vatTaxableTotal>
  {
  }

  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.lineTotal>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.commnPct>
  {
  }

  public abstract class curyCommnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.curyCommnAmt>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.commnAmt>
  {
  }

  public abstract class curyCommnblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyCommnblAmt>
  {
  }

  public abstract class commnblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.commnblAmt>
  {
  }

  public abstract class creditHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.creditHold>
  {
  }

  public abstract class approvedCredit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.approvedCredit>
  {
  }

  public abstract class approvedCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.approvedCreditAmt>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.ownerID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.projectID>
  {
  }

  public abstract class curyLineDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyLineDiscTotal>
  {
  }

  public abstract class lineDiscTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARCashSale.lineDiscTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARCashSale.CuryGoodsExtPriceTotal" />
  public abstract class curyGoodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyGoodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARCashSale.CuryGoodsExtPriceTotal" />
  public abstract class goodsExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.goodsExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARCashSale.CuryMiscExtPriceTotal" />
  public abstract class curyMiscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyMiscExtPriceTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.Standalone.ARCashSale.MiscExtPriceTotal" />
  public abstract class miscExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.miscExtPriceTotal>
  {
  }

  public abstract class detailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.detailExtPriceTotal>
  {
  }

  public abstract class curyDetailExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyDetailExtPriceTotal>
  {
  }

  public abstract class applyOverdueCharge : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARCashSale.applyOverdueCharge>
  {
  }

  public abstract class isPaymentsTransferred : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARCashSale.isPaymentsTransferred>
  {
  }

  public abstract class aRPaymentDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.aRPaymentDocType>
  {
  }

  public abstract class aRPaymentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.aRPaymentRefNbr>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.branchID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.pMInstanceID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.processingCenterID>
  {
  }

  public abstract class terminalID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.terminalID>
  {
  }

  public abstract class cardPresent : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.cardPresent>
  {
  }

  public abstract class pMInstanceID_CustomerPaymentMethod_descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.pMInstanceID_CustomerPaymentMethod_descr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.cashAccountID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.curyID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.extRefNbr>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARCashSale.adjDate>
  {
  }

  public abstract class adjFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.adjTranPeriodID>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARCashSale.clearDate>
  {
  }

  public abstract class settled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.settled>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARCashSale.cATranID>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.depositAsBatch>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.chargeCntr>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARCashSale.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.deposited>
  {
  }

  public abstract class depositDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARCashSale.depositDate>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.depositNbr>
  {
  }

  public abstract class paymentProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.paymentProjectID>
  {
  }

  public abstract class paymentTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARCashSale.paymentTaskID>
  {
  }

  public abstract class curyConsolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.curyConsolidateChargeTotal>
  {
  }

  public abstract class consolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARCashSale.consolidateChargeTotal>
  {
  }

  public abstract class refTranExtNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.refTranExtNbr>
  {
  }

  public abstract class isCCAuthorized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.isCCAuthorized>
  {
  }

  public abstract class isCCCaptured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.isCCCaptured>
  {
  }

  public abstract class isCCCaptureFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARCashSale.isCCCaptureFailed>
  {
  }

  public abstract class isCCRefunded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.isCCRefunded>
  {
  }

  public abstract class isCCUserAttention : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARCashSale.isCCUserAttention>
  {
  }

  public abstract class cCActualExternalTransactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARCashSale.cCActualExternalTransactionID>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARCashSale.docDate>
  {
  }

  public new abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.tranPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARCashSale.finPeriodID>
  {
  }

  public new abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.customerID_Customer_acctName>
  {
  }

  public abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.voidAppl>
  {
  }

  public abstract class isCCPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.isCCPayment>
  {
  }

  public abstract class cCPaymentStateDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARCashSale.cCPaymentStateDescr>
  {
  }

  public new abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.dontPrint>
  {
  }

  public new abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARCashSale.dontEmail>
  {
  }
}
