// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.Common.Interfaces;
using PX.Objects.Common.Scopes;
using PX.Objects.Common.Utility;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.PaymentProcessor.BillCom;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.AP;

public class APPaymentEntry : APDataEntryGraph<APPaymentEntry, APPayment>
{
  public PXWorkflowEventHandler<APPayment> OnPrintCheck;
  public PXWorkflowEventHandler<APPayment> OnCancelPrintCheck;
  public PXWorkflowEventHandler<APPayment> OnReleaseDocument;
  public PXWorkflowEventHandler<APPayment> OnVoidDocument;
  public PXWorkflowEventHandler<APPayment> OnCloseDocument;
  public PXWorkflowEventHandler<APPayment> OnOpenDocument;
  public PXWorkflowEventHandler<APPayment> OnProcessDocument;
  /// <summary>
  /// Necessary for proper cache resolution inside selector
  /// on <see cref="P:PX.Objects.AP.APAdjust.DisplayRefNbr" />.
  /// </summary>
  public PXSelect<PX.Objects.AP.Standalone.APRegister> dummy_register;
  [PXViewName("Payment")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APPayment.extRefNbr), typeof (APPayment.clearDate), typeof (APPayment.cleared), typeof (APPayment.externalPaymentID), typeof (APPayment.externalPaymentStatus), typeof (APPayment.externalOrganizationID), typeof (APPayment.externalPaymentCanceled), typeof (APPayment.externalPaymentVoided), typeof (APPayment.externalPaymentUpdateTime), typeof (APPayment.externalPaymentDisbursementType), typeof (APPayment.externalPaymentSentDate), typeof (APPayment.externalPaymentCheckNbr), typeof (APPayment.externalPaymentCardNbr), typeof (APPayment.externalPaymentTraceNbr), typeof (APPayment.externalPaymentBatchNbr)})]
  public PXSelectJoin<APPayment, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>, Where<APPayment.docType, Equal<Optional<APPayment.docType>>, PX.Data.And<Where<Vendor.bAccountID, PX.Data.IsNull, PX.Data.Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> Document;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APPayment.clearDate), typeof (APPayment.cleared), typeof (APPayment.externalPaymentID), typeof (APPayment.externalPaymentStatus), typeof (APPayment.externalOrganizationID), typeof (APPayment.externalPaymentCanceled), typeof (APPayment.externalPaymentVoided), typeof (APPayment.externalPaymentUpdateTime), typeof (APPayment.externalPaymentDisbursementType), typeof (APPayment.externalPaymentSentDate), typeof (APPayment.externalPaymentCheckNbr), typeof (APPayment.externalPaymentCardNbr), typeof (APPayment.externalPaymentTraceNbr), typeof (APPayment.externalPaymentBatchNbr)})]
  public PXSelect<APPayment, Where<APPayment.docType, Equal<Current<APPayment.docType>>, And<APPayment.refNbr, Equal<Current<APPayment.refNbr>>>>> CurrentDocument;
  [PXViewName("Adjust")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<APAdjust, LeftJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<PX.Data.True>, And<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<APAdjust.released, NotEqual<PX.Data.True>>>>> Adjustments;
  public PXSelect<APAdjust, Where<APAdjust.adjgDocType, Equal<Optional<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Optional<APPayment.refNbr>>, And<APAdjust.released, NotEqual<PX.Data.True>>>>> Adjustments_Raw;
  public PXSelectJoin<PX.Objects.AP.Standalone.APAdjust, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<PX.Objects.AP.Standalone.APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<PX.Objects.AP.Standalone.APAdjust.adjdRefNbr>>>, InnerJoin<APRegisterAlias, On<APRegisterAlias.docType, Equal<PX.Objects.AP.Standalone.APAdjust.adjdDocType>, And<APRegisterAlias.refNbr, Equal<PX.Objects.AP.Standalone.APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APTran.tranType, Equal<PX.Objects.AP.Standalone.APAdjust.adjdDocType>, And<APTran.refNbr, Equal<PX.Objects.AP.Standalone.APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<PX.Objects.AP.Standalone.APAdjust.adjdLineNbr>>>>, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APRegisterAlias.curyInfoID>>, LeftJoin<CurrencyInfo2, On<CurrencyInfo2.curyInfoID, Equal<PX.Objects.AP.Standalone.APAdjust.adjdCuryInfoID>>>>>>>, Where<PX.Objects.AP.Standalone.APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<PX.Objects.AP.Standalone.APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<PX.Objects.AP.Standalone.APAdjust.released, Equal<Required<PX.Objects.AP.Standalone.APAdjust.released>>>>>> Adjustments_Balance;
  public PXSelectJoin<APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<PX.Data.True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>>>, Where<APInvoice.vendorID, Equal<Current<APPayment.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> APInvoice_DocType_RefNbr;
  public PXSelect<APInvoice, Where<APInvoice.vendorID, Equal<Current<APPayment.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> APInvoice_DocType_RefNbr_Single;
  public PXSelectReadonly2<APPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APPayment.curyInfoID>>>, Where<APPayment.vendorID, Equal<Current<APPayment.vendorID>>, And<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>> ARPayment_DocType_RefNbr;
  public PXSelectJoin<APAdjust, InnerJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<APRegisterAlias, On<APRegisterAlias.docType, Equal<APAdjust.adjdDocType>, And<APRegisterAlias.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<APAdjust.released, NotEqual<PX.Data.True>, And<APAdjust.isInitialApplication, NotEqual<PX.Data.True>>>>>> Adjustments_Invoices;
  public PXSelectJoin<APAdjust, InnerJoinSingleTable<APPayment, On<APPayment.docType, Equal<APAdjust.adjdDocType>, And<APPayment.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, InnerJoin<APRegisterAlias, On<APRegisterAlias.docType, Equal<APAdjust.adjdDocType>, And<APRegisterAlias.refNbr, Equal<APAdjust.adjdRefNbr>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<APAdjust.released, NotEqual<PX.Data.True>>>>> Adjustments_Payments;
  [PXViewName("Print Check Detail")]
  public PXSelect<APPrintCheckDetail, Where<APPrintCheckDetail.adjgDocType, Equal<Current<APPayment.docType>>, And<APPrintCheckDetail.adjgRefNbr, Equal<Current<APPayment.refNbr>>>>> CheckDetails;
  [PXViewName("Application History")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<APTranPostBal, LeftJoin<PX.Objects.AP.Standalone.APRegister, On<PX.Objects.AP.Standalone.APRegister.docType, Equal<APTranPostBal.sourceDocType>, And<PX.Objects.AP.Standalone.APRegister.refNbr, Equal<APTranPostBal.sourceRefNbr>>>, LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APTranPostBal.sourceDocType>, And<APInvoice.refNbr, Equal<APTranPostBal.sourceRefNbr>>>, LeftJoin<APTran, On<APTran.tranType, Equal<APTranPostBal.sourceDocType>, And<APTran.refNbr, Equal<APTranPostBal.sourceRefNbr>, And<APTran.lineNbr, Equal<APTranPostBal.lineNbr>>>>, LeftJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AP.Standalone.APRegister.curyInfoID>>, LeftJoin<CurrencyInfo2, On<CurrencyInfo2.curyInfoID, Equal<Current<APPayment.curyInfoID>>>, LeftJoin<APAdjust2, On<APAdjust2.noteID, Equal<APTranPostBal.refNoteID>>>>>>>>, Where<APTranPostBal.docType, Equal<Current<APPayment.docType>>, And<APTranPostBal.refNbr, Equal<Current<APPayment.refNbr>>, And<APTranPostBal.type, NotEqual<APTranPost.type.origin>, And<APTranPostBal.type, NotEqual<APTranPost.type.rgol>, And<APTranPostBal.type, NotEqual<APTranPost.type.retainage>, And<APTranPostBal.type, NotEqual<ARTranPost.type.retainageReverse>>>>>>>, PX.Data.OrderBy<Asc<APTranPostBal.iD>>> APPost;
  [PXViewName("Adjust History")]
  public PXSelect<APAdjust> Adjustments_print;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APPayment.curyInfoID>>>> currencyinfo;
  [PXReadOnlyView]
  public PXSelect<APInvoice> dummy_APInvoice;
  [PXReadOnlyView]
  public PXSelect<CATran, Where<CATran.tranID, Equal<Current<APPayment.cATranID>>>> dummy_CATran;
  [PXViewName("AP Address")]
  public PXSelect<APAddress, Where<APAddress.addressID, Equal<Current<APPayment.remitAddressID>>>> Remittance_Address;
  [PXViewName("AP Contact")]
  public PXSelect<APContact, Where<APContact.contactID, Equal<Current<APPayment.remitContactID>>>> Remittance_Contact;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelectReadonly2<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<PX.Data.True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>>, Where<APInvoice.vendorID, Equal<Required<APInvoice.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> APInvoice_VendorID_DocType_RefNbr;
  public PXSelect<APPayment, Where<APPayment.vendorID, Equal<Required<APPayment.vendorID>>, And<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>> APPayment_VendorID_DocType_RefNbr;
  public APPaymentChargeSelect<APPayment, APPayment.paymentMethodID, APPayment.cashAccountID, APPayment.docDate, APPayment.tranPeriodID, Where<APPaymentChargeTran.docType, Equal<Current<APPayment.docType>>, And<APPaymentChargeTran.refNbr, Equal<Current<APPayment.refNbr>>>>> PaymentCharges;
  [PXViewName("Vendor")]
  public PXSetup<Vendor, Where<Vendor.bAccountID, Equal<Optional<APPayment.vendorID>>>> vendor;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APPayment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<APPayment.vendorLocationID>>>>> location;
  public PXSetup<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>> vendorclass;
  public PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<APPayment.cashAccountID>>>, PX.Data.OrderBy<Asc<PaymentMethodAccount.aPIsDefault>>> CashAcctDetail_AccountID;
  public PXSetup<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Optional<APPayment.paymentMethodID>>>> paymenttype;
  public PXSetup<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Optional<APPayment.cashAccountID>>>> cashaccount;
  public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Optional<APPayment.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<APPayment.paymentMethodID>>>>> cashaccountdetail;
  public PXSelectReadonly<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Optional<APPayment.adjFinPeriodID>>, PX.Data.And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<APPayment.branchID>>>>> finperiod;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<CashAccountCheck> CACheck;
  [PXViewName("Employee")]
  public PXSetup<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<APRegister.employeeID>>>> employee;
  public PXSelect<APSetupApproval, Where<APSetupApproval.docType, Equal<Current<APPayment.docType>>, PX.Data.And<Where<Current<APPayment.docType>, Equal<APDocType.check>, Or<Current<APPayment.docType>, Equal<APDocType.prepayment>>>>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomationWithReservedDoc<APPayment, APRegister.approved, APRegister.rejected, APPayment.hold, APSetupApproval> Approval;
  public static string[] AdjgDocTypesToValidateFinPeriod = new string[4]
  {
    "CHK",
    "ADR",
    "PPM",
    "REF"
  };
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXAction<APPayment> cancel;
  public PXAction<APPayment> printAPEdit;
  public PXAction<APPayment> printAPRegister;
  public PXAction<APPayment> printAPPayment;
  public PXAction<APPayment> printCheck;
  public PXAction<APPayment> newVendor;
  public PXAction<APPayment> editVendor;
  public PXAction<APPayment> vendorDocuments;
  public PXAction<APPayment> viewPPDVATAdj;
  public PXAction<APPayment> loadInvoices;
  public PXAction<APPayment> reverseApplication;
  public PXAction<APPayment> viewDocumentToApply;
  public PXAction<APPayment> viewApplicationDocument;
  public PXAction<APPayment> validateAddresses;
  public PXAction<APPayment> ViewOriginalDocument;
  public bool TakeDiscAlways;
  protected Dictionary<APAdjust, PXResultset<APInvoice>> balanceCache;
  private bool _IsVoidCheckInProgress;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Original Document")]
  protected virtual void APPayment_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXSelector(typeof (Search<CABatch.batchNbr>))]
  [PXDBScalar(typeof (Search<CABatchDetail.batchNbr, Where<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<APPayment.refNbr>>>>>))]
  [PXUIField(DisplayName = "Batch Payment Nbr.", Enabled = false, Visibility = PXUIVisibility.SelectorVisible, Visible = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APPayment.batchPaymentRefNbr> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [BatchNbrExt(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APAdjust.isMigratedRecord))]
  protected virtual void APAdjust_AdjBatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Switch<Case<Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>>>, ARAdjust.adjType.adjusted>, ARAdjust.adjType.adjusting>))]
  protected virtual void APAdjust_AdjType_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  protected virtual void APAdjust_AdjdWhTaxAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  protected virtual void APAdjust_AdjdWhTaxSubID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void APAdjust_DisplayRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (APPayment.docDate), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (APPayment.vendorID), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (APPayment.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APPayment.curyInfoID))]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (APPayment.curyOrigDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (APPayment.origDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_SourceItemType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) new APDocType.ListAttribute().ValueLabelDic[this.Document.Current.DocType];
    e.Cancel = true;
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) EPApprovalHelper.BuildEPApprovalDetailsString(sender, (IApprovalDescription) this.Document.Current);
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Doc. Type")]
  protected virtual void APTranPostBal_SourceDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Reference Nbr.")]
  protected virtual void APTranPostBal_SourceRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Amount Paid")]
  protected virtual void APTranPostBal_CuryAmt_CacheAttached(PXCache sender)
  {
  }

  private bool AppendAdjustmentsInvoicesTail(
    APAdjust adj,
    PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran> invoicesResult)
  {
    this.Adjustments_Invoices.StoreTailResult((PXResult) new PXResult<APInvoice, APRegisterAlias, APTran>((APInvoice) invoicesResult, PX.Objects.Common.Utilities.Clone<APInvoice, APRegisterAlias>((PXGraph) this, (APInvoice) invoicesResult), (APTran) invoicesResult), (object[]) new APAdjust[1]
    {
      adj
    }, (object) this.Document.Current.DocType, (object) this.Document.Current.RefNbr);
    return true;
  }

  private void Adjustments_Invoices_BeforeSelect()
  {
    if (NonGenericIEnumerableExtensions.Empty_(this.Adjustments_Raw.Cache.Cached))
      return;
    this.Adjustments_Raw.Cache.Cached.OfType<APAdjust>().Join<APAdjust, PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran>, string, bool>(this.GetVendDocs(this.Document.Current, this.APSetup.Current).AsEnumerable<PXResult<APInvoice>>().Cast<PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran>>(), (Func<APAdjust, string>) (_ => _.AdjdDocType + _.AdjdRefNbr + _.AdjdLineNbr.ToString()), (Func<PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran>, string>) (_ => _.GetItem<APInvoice>().DocType + _.GetItem<APInvoice>().RefNbr + _.GetItem<APTran>().LineNbr.GetValueOrDefault().ToString()), new Func<APAdjust, PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran>, bool>(this.AppendAdjustmentsInvoicesTail)).ToArray<bool>();
  }

  public bool AutoPaymentApp { get; set; }

  public bool IsReverseProc { get; set; }

  public OrganizationFinPeriod FINPERIOD => (OrganizationFinPeriod) this.finperiod.Select();

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  [PXCancelButton]
  [PXUIField(MapEnableRights = PXCacheRights.Select)]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    APPaymentEntry graph = this;
    string str1 = (string) null;
    if (graph.Document.Current != null)
    {
      string docType = graph.Document.Current.DocType;
      str1 = graph.Document.Current.RefNbr;
    }
    PXResult<APPayment, Vendor> row = (PXResult<APPayment, Vendor>) null;
    foreach (PXResult<APPayment, Vendor> pxResult in new PXCancel<APPayment>((PXGraph) graph, nameof (Cancel)).Press(a))
      row = pxResult;
    if (graph.Document.Cache.GetStatus((object) (APPayment) row) == PXEntryStatus.Inserted && str1 != ((APPayment) row).RefNbr && (((APPayment) row).DocType == "CHK" || ((APPayment) row).DocType == "PPM"))
    {
      string docType = ((APPayment) row).DocType;
      string refNbr = ((APPayment) row).RefNbr;
      string str2 = docType == "CHK" ? "PPM" : "CHK";
      APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) graph, (object) str2, (object) refNbr);
      APInvoice apInvoice = (APInvoice) null;
      if (str2 == "PPM")
        apInvoice = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<APDocType.prepayment>, And<APInvoice.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) graph, (object) refNbr);
      if (apPayment != null || apInvoice != null)
        graph.Document.Cache.RaiseExceptionHandling<APPayment.refNbr>((object) (APPayment) row, (object) refNbr, (Exception) new PXSetPropertyException<APPayment.refNbr>("{0} with reference number {1} already exists. Enter another reference number.", new object[2]
        {
          str2 == "CHK" ? (object) "Payment" : (object) "Prepayment",
          (object) refNbr
        }));
    }
    yield return (object) row;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "AP Edit Detailed", MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable PrintAPEdit(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AP610500");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "AP Register Detailed", MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable PrintAPRegister(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AP622000");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "AP Payment Register", MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable PrintAPPayment(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AP622500");
  }

  [PXUIField(DisplayName = "Print/Process", MapEnableRights = PXCacheRights.Select)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable PrintCheck(PXAdapter adapter)
  {
    if (this.IsDirty)
      this.Save.Press();
    APPayment current = this.Document.Current;
    APPrintChecks instance = PXGraph.CreateInstance<APPrintChecks>();
    PrintChecksFilter copy = PXCache<PrintChecksFilter>.CreateCopy(instance.Filter.Current);
    copy.BranchID = current.BranchID;
    copy.PayAccountID = current.CashAccountID;
    copy.PayTypeID = current.PaymentMethodID;
    instance.Filter.Cache.Update((object) copy);
    current.Selected = new bool?(true);
    current.Passed = new bool?(true);
    instance.APPaymentList.Cache.Update((object) current);
    instance.APPaymentList.Cache.SetStatus((object) current, PXEntryStatus.Updated);
    instance.APPaymentList.Cache.IsDirty = false;
    throw new PXRedirectRequiredException((PXGraph) instance, "Preview");
  }

  [PXUIField(DisplayName = "New Vendor", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  public virtual IEnumerable NewVendor(PXAdapter adapter)
  {
    throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<VendorMaint>(), "New Vendor");
  }

  [PXUIField(DisplayName = "Edit Vendor", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  public virtual IEnumerable EditVendor(PXAdapter adapter)
  {
    if (this.vendor.Current != null)
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      instance.BAccount.Current = (VendorR) this.vendor.Current;
      throw new PXRedirectRequiredException((PXGraph) instance, "Edit Vendor");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable VendorDocuments(PXAdapter adapter)
  {
    if (this.vendor.Current != null)
    {
      APDocumentEnq instance = PXGraph.CreateInstance<APDocumentEnq>();
      instance.Filter.Current.VendorID = this.vendor.Current.BAccountID;
      instance.Filter.Select();
      throw new PXRedirectRequiredException((PXGraph) instance, "Vendor Details");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable Release(PXAdapter adapter)
  {
    PXCache cache = this.Document.Cache;
    List<APRegister> list = new List<APRegister>();
    foreach (APPayment apPayment in adapter.Get<APPayment>())
    {
      APPayment apdoc = apPayment;
      if (apdoc.Status != "B" && apdoc.Status != "P" && apdoc.Status != "N" && apdoc.Status != "U")
        throw new PXException("Document Status is invalid for processing.");
      APReleaseProcess.VerifyExtRefNbr(cache, apdoc, (System.Action<PXCache>) (_ =>
      {
        _.RaiseExceptionHandling<APPayment.extRefNbr>((object) apdoc, (object) apdoc.ExtRefNbr, (Exception) new PXRowPersistingException(typeof (APPayment.extRefNbr).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPayment.extRefNbr>(_)
        }));
        this.Document.Cache.MarkUpdated((object) apdoc);
      }));
      list.Add((APRegister) apdoc);
    }
    this.Save.Press();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => APDocumentRelease.ReleaseDoc(list, false)));
    return (IEnumerable) list;
  }

  protected virtual bool AskUserApprovalToVoidPayment(APPayment payment)
  {
    return !payment.Deposited.GetValueOrDefault() || this.Document.View.Ask(PXMessages.LocalizeNoPrefix("The payment has already been deposited. To proceed, click OK."), MessageButtons.OKCancel) == WebDialogResult.OK;
  }

  [PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable VoidCheck(PXAdapter adapter)
  {
    if (this.Document.Current != null)
    {
      bool? nullable = this.Document.Current.Released;
      if (nullable.GetValueOrDefault())
      {
        nullable = this.Document.Current.Voided;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue && APPaymentType.VoidEnabled(this.Document.Current.DocType))
        {
          APAdjust adj1 = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjgDocType, In3<APDocType.check, APDocType.prepayment>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.Document.Current.DocType, (object) this.Document.Current.RefNbr);
          if (adj1 != null && !adj1.IsSelfAdjustment())
            throw new PXException("The prepayment cannot be voided. Void the {0} payment instead.", new object[1]
            {
              (object) adj1.AdjgRefNbr
            });
          APAdjust adj2 = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.adjgDocType, Equal<APDocType.refund>, And<APAdjust.voided, NotEqual<PX.Data.True>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.Document.Current.DocType, (object) this.Document.Current.RefNbr);
          if (adj2 != null && !adj2.IsSelfAdjustment())
            throw new PXException("{0} {1} has been refunded with {2} {3}.", new object[4]
            {
              (object) GetLabel.For<APDocType>(this.Document.Current.DocType),
              (object) this.Document.Current.RefNbr,
              (object) GetLabel.For<APDocType>(adj2.AdjgDocType),
              (object) adj2.AdjgRefNbr
            });
          nullable = this.APSetup.Current.MigrationMode;
          if (!nullable.GetValueOrDefault())
          {
            nullable = this.Document.Current.IsMigratedRecord;
            if (nullable.GetValueOrDefault())
            {
              Decimal? curyInitDocBal = this.Document.Current.CuryInitDocBal;
              Decimal? curyOrigDocAmt = this.Document.Current.CuryOrigDocAmt;
              if (!(curyInitDocBal.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & curyInitDocBal.HasValue == curyOrigDocAmt.HasValue))
                throw new PXException("The document cannot be processed because it was created when migration mode was activated. To process the document, activate migration mode on the Accounts Payable Preferences (AP101000) form.");
            }
          }
          nullable = this.APSetup.Current.MigrationMode;
          if (nullable.GetValueOrDefault())
          {
            nullable = this.Document.Current.IsMigratedRecord;
            if (nullable.GetValueOrDefault() && PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<APAdjust.released, Equal<PX.Data.True>, And<APAdjust.voided, NotEqual<PX.Data.True>, And<APAdjust.isMigratedRecord, NotEqual<PX.Data.True>, And<APAdjust.isInitialApplication, NotEqual<PX.Data.True>>>>>>>>.Config>.Select((PXGraph) this).RowCast<APAdjust>().Any<APAdjust>((Func<APAdjust, bool>) (_ => !_.VoidAppl.GetValueOrDefault())))
              throw new PXException("The payment cannot be voided because it has unreversed regular applications.");
          }
          nullable = this.Document.Current.DepositAsBatch;
          if (nullable.GetValueOrDefault() && !string.IsNullOrEmpty(this.Document.Current.DepositNbr))
          {
            nullable = this.Document.Current.Deposited;
            if (!nullable.GetValueOrDefault())
              throw new PXException("This refund is included in the {0} bank deposit. It cannot be voided until the deposit is released or the refund is excluded from it.", new object[1]
              {
                (object) this.Document.Current.DepositNbr
              });
          }
          APPayment apPayment = (APPayment) this.Document.Search<APPayment.refNbr>((object) this.Document.Current.RefNbr, (object) APPaymentType.GetVoidingAPDocType(this.Document.Current.DocType));
          bool flag2 = this.Document.Current.ExternalPaymentID != null;
          if (apPayment != null)
          {
            if (this.IsContractBasedAPI | flag2)
              throw new PXException("The document has already been voided.");
            this.Document.Current = apPayment;
            throw new PXRedirectRequiredException((PXGraph) this, "Void");
          }
          if (!this.AskUserApprovalToVoidPayment(this.Document.Current))
            return adapter.Get();
          this.DeleteUnreleasedApplications();
          this.Save.Press();
          APPayment copy = PXCache<APPayment>.CreateCopy(this.Document.Current);
          this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<APPayment.finPeriodID, APPayment.branchID>(this.Document.Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aPClosed));
          this.TryToVoidCheck(copy);
          this.Document.Cache.RaiseExceptionHandling<APPayment.finPeriodID>((object) this.Document.Current, (object) this.Document.Current.FinPeriodID, (Exception) null);
          if (((this.IsContractBasedAPI ? 1 : (this.IsImport ? 1 : 0)) | (flag2 ? 1 : 0)) == 0)
            throw new PXRedirectRequiredException((PXGraph) this, "Voided");
          return (IEnumerable) new APPayment[1]
          {
            this.Document.Current
          };
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField(MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  public virtual IEnumerable ViewPPDVATAdj(PXAdapter adapter)
  {
    APAdjust apAdjust = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.noteID, Equal<Required<APTranPostBal.refNoteID>>>>.Config>.Select((PXGraph) this, (object) this.APPost.Current.RefNoteID);
    if (apAdjust != null)
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      instance.Document.Current = (APInvoice) instance.Document.Search<APInvoice.refNbr>((object) apAdjust.PPDVATAdjRefNbr, (object) apAdjust.PPDVATAdjDocType);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
      requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
      throw requiredException;
    }
    return adapter.Get();
  }

  public override void Clear()
  {
    this.balanceCache = (Dictionary<APAdjust, PXResultset<APInvoice>>) null;
    base.Clear();
  }

  private APAdjust AddAdjustment(APAdjust adj)
  {
    Decimal? curyUnappliedBal = this.Document.Current.CuryUnappliedBal;
    Decimal num1 = 0M;
    if (curyUnappliedBal.GetValueOrDefault() == num1 & curyUnappliedBal.HasValue)
    {
      Decimal? curyOrigDocAmt = this.Document.Current.CuryOrigDocAmt;
      Decimal num2 = 0M;
      if (curyOrigDocAmt.GetValueOrDefault() > num2 & curyOrigDocAmt.HasValue)
        throw new APPaymentEntry.PXLoadInvoiceException();
    }
    return this.Adjustments.Insert(adj);
  }

  private void LoadInvoicesProc(bool LoadExistingOnly)
  {
    Dictionary<string, APAdjust> dictionary = new Dictionary<string, APAdjust>();
    APPayment currentDoc = this.Document.Current;
    try
    {
      if (currentDoc != null && currentDoc.VendorID.HasValue)
      {
        bool? openDoc = currentDoc.OpenDoc;
        bool flag = false;
        if (!(openDoc.GetValueOrDefault() == flag & openDoc.HasValue) && (!(currentDoc.DocType != "CHK") || !(currentDoc.DocType != "PPM") || !(currentDoc.DocType != "REF")))
        {
          APPaymentEntry.APPaymentEntryVendorDocsExtension vendorDocsExtension = this.GetExtension<APPaymentEntry.APPaymentEntryVendorDocsExtension>();
          List<PXResult<APAdjust>> list = this.Adjustments.View.SelectExternal().Cast<PXResult<APAdjust>>().Where<PXResult<APAdjust>>((Func<PXResult<APAdjust>, bool>) (adj => !((APAdjust) adj).Released.GetValueOrDefault() && !((APAdjust) adj).Voided.GetValueOrDefault())).ToList<PXResult<APAdjust>>();
          list.Sort((Comparison<PXResult<APAdjust>>) ((a, b) => vendorDocsExtension.CompareVendorDocs(currentDoc, PXResult.Unwrap<APInvoice>((object) a), PXResult.Unwrap<APInvoice>((object) b), PXResult.Unwrap<APTran>((object) a), PXResult.Unwrap<APTran>((object) b))));
          foreach (PXResult<APAdjust> pxResult in list)
          {
            APAdjust apAdjust = (APAdjust) pxResult;
            if (!LoadExistingOnly)
            {
              apAdjust = PXCache<APAdjust>.CreateCopy(apAdjust);
              apAdjust.CuryAdjgAmt = new Decimal?();
              apAdjust.CuryAdjgDiscAmt = new Decimal?();
              apAdjust.CuryAdjgWhTaxAmt = new Decimal?();
              apAdjust.CuryAdjgPPDAmt = new Decimal?();
            }
            string key = $"{apAdjust.AdjdDocType}_{apAdjust.AdjdRefNbr}_{apAdjust.AdjdLineNbr}";
            dictionary.Add(key, apAdjust);
            this.Adjustments.Cache.Delete((object) (APAdjust) pxResult);
          }
          APPayment apPayment = currentDoc;
          int? adjCntr = apPayment.AdjCntr;
          apPayment.AdjCntr = adjCntr.HasValue ? new int?(adjCntr.GetValueOrDefault() + 1) : new int?();
          this.Document.Cache.MarkUpdated((object) currentDoc);
          this.Document.Cache.IsDirty = true;
          foreach (APAdjust apAdjust in dictionary.Values)
          {
            APAdjust adj = new APAdjust()
            {
              AdjdDocType = apAdjust.AdjdDocType,
              AdjdRefNbr = apAdjust.AdjdRefNbr,
              AdjdLineNbr = apAdjust.AdjdLineNbr
            };
            try
            {
              APAdjust copy = PXCache<APAdjust>.CreateCopy(this.AddAdjustment(adj));
              Decimal? nullable1 = apAdjust.CuryAdjgWhTaxAmt;
              if (nullable1.HasValue)
              {
                nullable1 = apAdjust.CuryAdjgWhTaxAmt;
                Decimal? curyAdjgWhTaxAmt = copy.CuryAdjgWhTaxAmt;
                if (nullable1.GetValueOrDefault() < curyAdjgWhTaxAmt.GetValueOrDefault() & nullable1.HasValue & curyAdjgWhTaxAmt.HasValue)
                {
                  copy.CuryAdjgWhTaxAmt = apAdjust.CuryAdjgWhTaxAmt;
                  copy = PXCache<APAdjust>.CreateCopy((APAdjust) this.Adjustments.Cache.Update((object) copy));
                }
              }
              Decimal? nullable2 = apAdjust.CuryAdjgDiscAmt;
              int num1;
              if (nullable2.HasValue)
              {
                nullable2 = apAdjust.CuryAdjgDiscAmt;
                Decimal num2 = 0M;
                if (nullable2.GetValueOrDefault() < num2 & nullable2.HasValue)
                {
                  nullable1 = apAdjust.CuryAdjgDiscAmt;
                  nullable2 = copy.CuryAdjgDiscAmt;
                  num1 = nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue ? 1 : 0;
                  goto label_21;
                }
              }
              nullable2 = apAdjust.CuryAdjgDiscAmt;
              nullable1 = copy.CuryAdjgDiscAmt;
              num1 = nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue ? 1 : 0;
label_21:
              if (num1 != 0)
              {
                copy.CuryAdjgDiscAmt = apAdjust.CuryAdjgDiscAmt;
                copy.CuryAdjgPPDAmt = apAdjust.CuryAdjgDiscAmt;
                copy = PXCache<APAdjust>.CreateCopy((APAdjust) this.Adjustments.Cache.Update((object) copy));
              }
              nullable2 = apAdjust.CuryAdjgAmt;
              int num3;
              if (nullable2.HasValue)
              {
                nullable2 = apAdjust.CuryAdjgAmt;
                Decimal num4 = 0M;
                if (nullable2.GetValueOrDefault() < num4 & nullable2.HasValue)
                {
                  nullable1 = apAdjust.CuryAdjgAmt;
                  nullable2 = copy.CuryAdjgAmt;
                  num3 = nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue ? 1 : 0;
                  goto label_27;
                }
              }
              nullable2 = apAdjust.CuryAdjgAmt;
              nullable1 = copy.CuryAdjgAmt;
              num3 = nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue ? 1 : 0;
label_27:
              if (num3 != 0)
              {
                copy.CuryAdjgAmt = apAdjust.CuryAdjgAmt;
                this.Adjustments.Cache.Update((object) copy);
              }
            }
            catch (PXSetPropertyException ex)
            {
            }
          }
          if (LoadExistingOnly)
            return;
          this.FieldVerifying.AddHandler<APAdjust.adjdAPSub>(new PXFieldVerifying(this.suppresSubVerify));
          this.AutoPaymentApp = true;
          foreach (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran> vendDoc in this.GetVendDocs(currentDoc, this.APSetup.Current))
          {
            APInvoice apInvoice = (APInvoice) vendDoc;
            APTran apTran = (APTran) vendDoc;
            string docType = apInvoice.DocType;
            string refNbr = apInvoice.RefNbr;
            int? nullable3;
            int? nullable4;
            if (apTran == null)
            {
              nullable3 = new int?();
              nullable4 = nullable3;
            }
            else
              nullable4 = apTran.LineNbr;
            nullable3 = nullable4;
            // ISSUE: variable of a boxed type
            __Boxed<int> valueOrDefault = (ValueType) nullable3.GetValueOrDefault();
            string key = $"{docType}_{refNbr}_{valueOrDefault}";
            if (!dictionary.ContainsKey(key))
            {
              APAdjust apAdjust = new APAdjust();
              apAdjust.AdjdDocType = apInvoice.DocType;
              apAdjust.AdjdRefNbr = apInvoice.RefNbr;
              int? nullable5;
              if (apTran == null)
              {
                nullable3 = new int?();
                nullable5 = nullable3;
              }
              else
                nullable5 = apTran.LineNbr;
              nullable3 = nullable5;
              apAdjust.AdjdLineNbr = new int?(nullable3.GetValueOrDefault());
              apAdjust.AdjgDocType = currentDoc.DocType;
              apAdjust.AdjgRefNbr = currentDoc.RefNbr;
              apAdjust.AdjNbr = currentDoc.AdjCntr;
              APAdjust adj = apAdjust;
              this.AddBalanceCache(adj, (PXResult) vendDoc);
              PXCache cache = this.Adjustments.Cache;
              APAdjust data = adj;
              APAdjust.APInvoice selectResult = new APAdjust.APInvoice();
              selectResult.DocType = adj.AdjdDocType;
              selectResult.RefNbr = adj.AdjdRefNbr;
              selectResult.PaymentsByLinesAllowed = apInvoice.PaymentsByLinesAllowed;
              PXSelectorAttribute.StoreResult<APAdjust.adjdRefNbr>(cache, (object) data, (IBqlTable) selectResult);
              adj.VendorID = apInvoice.VendorID;
              this.StoreApplicationDataResult(adj, vendDoc);
              this.AddAdjustment(adj);
            }
          }
          this.FieldVerifying.RemoveHandler<APAdjust.adjdAPSub>(new PXFieldVerifying(this.suppresSubVerify));
          this.AutoPaymentApp = false;
          Decimal? nullable6 = currentDoc.CuryApplAmt;
          Decimal num5 = 0M;
          if (!(nullable6.GetValueOrDefault() < num5 & nullable6.HasValue))
          {
            nullable6 = currentDoc.CuryUnappliedBal;
            Decimal num6 = 0M;
            if (!(nullable6.GetValueOrDefault() > num6 & nullable6.HasValue))
              return;
          }
          List<APAdjust> apAdjustList = new List<APAdjust>();
          foreach (PXResult<APAdjust> pxResult in this.Adjustments_Raw.Select())
          {
            APAdjust apAdjust = (APAdjust) pxResult;
            if (apAdjust.AdjdDocType == "ADR")
            {
              nullable6 = apAdjust.AdjAmt;
              Decimal num7 = 0M;
              if (nullable6.GetValueOrDefault() >= num7 & nullable6.HasValue)
                apAdjustList.Add(apAdjust);
            }
          }
          apAdjustList.Sort((Comparison<APAdjust>) ((a, b) => ((IComparable) a.CuryAdjgAmt).CompareTo((object) b.CuryAdjgAmt)));
          using (List<APAdjust>.Enumerator enumerator = apAdjustList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              APAdjust current = enumerator.Current;
              Decimal? nullable7 = current.CuryAdjgAmt;
              Decimal? nullable8 = currentDoc.CuryUnappliedBal;
              if (nullable7.GetValueOrDefault() <= nullable8.GetValueOrDefault() & nullable7.HasValue & nullable8.HasValue)
              {
                this.Adjustments.Delete(current);
              }
              else
              {
                APAdjust copy = PXCache<APAdjust>.CreateCopy(current);
                APAdjust apAdjust = copy;
                nullable8 = apAdjust.CuryAdjgAmt;
                nullable7 = currentDoc.CuryUnappliedBal;
                apAdjust.CuryAdjgAmt = nullable8.HasValue & nullable7.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
                this.Adjustments.Update(copy);
              }
            }
            return;
          }
        }
      }
      throw new APPaymentEntry.PXLoadInvoiceException();
    }
    catch (APPaymentEntry.PXLoadInvoiceException ex)
    {
    }
  }

  private void suppresSubVerify(PXCache sender, PXFieldVerifyingEventArgs e) => e.Cancel = true;

  protected virtual PXResultset<APInvoice> GetVendDocs(
    APPayment currentAPPayment,
    PX.Objects.AP.APSetup currentAPSetup)
  {
    int?[] array = this.CurrentUserInformationProvider.GetAllBranches().Select<BranchInfo, int?>((Func<BranchInfo, int?>) (b => new int?(b.Id))).Distinct<int?>().ToArray<int?>();
    using (new PXReadBranchRestrictedScope(PXAccess.GetAvailableOrganizationIDs(), array, requireAccessForAllSpecified: true))
      return this.GetExtension<APPaymentEntry.APPaymentEntryVendorDocsExtension>().GetVendDocs(currentAPPayment, currentAPSetup);
  }

  [PXUIField(DisplayName = "Load Documents", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton(ImageKey = "Refresh")]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LoadInvoices(PXAdapter adapter)
  {
    if (this.IsContractBasedAPI || this.IsImport)
    {
      this.LoadInvoicesProc(false);
    }
    else
    {
      APPaymentEntry clone = this.Clone<APPaymentEntry>();
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
      {
        clone.LoadInvoicesProc(false);
        PXLongOperation.SetCustomInfo((object) clone);
      }));
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Reverse Application", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(ImageKey = "Refresh")]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ReverseApplication(PXAdapter adapter)
  {
    APPayment eventTarget = this.Document.Current;
    APAdjust application = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.noteID, Equal<Current<APTranPostBal.refNoteID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0]);
    if (application == null)
      return adapter.Get();
    if (application.AdjType == "G")
      throw new PXException("This application cannot be reversed from {0}. Open {1} {2} to reverse this application.", new object[3]
      {
        (object) GetLabel.For<APDocType>(eventTarget.DocType),
        (object) GetLabel.For<APDocType>(application.AdjgDocType),
        (object) application.AdjgRefNbr
      });
    this.CheckDocumentBeforeReversing((PXGraph) this, application);
    bool? nullable1 = !application.IsInitialApplication.GetValueOrDefault() ? application.IsMigratedRecord : throw new PXException("This application cannot be reversed because it is a special application created in Migration Mode, which reflects the difference between the original document amount and the migrated balance.");
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = this.APSetup.Current.MigrationMode;
      if (nullable1.GetValueOrDefault())
        throw new PXException("The application cannot be reversed because it was created when migration mode was deactivated. To process the application, clear the Activate Migration Mode check box on the Accounts Payable Preferences (AP101000) form.");
    }
    nullable1 = application.Voided;
    if (!nullable1.GetValueOrDefault() && (APPaymentType.CanHaveBalance(application.AdjgDocType) || application.AdjgDocType == "REF" || application.AdjgDocType == "CHK"))
    {
      if (eventTarget != null)
      {
        if (!(eventTarget.DocType != "ADR"))
        {
          nullable1 = eventTarget.PendingPPD;
          if (nullable1.GetValueOrDefault())
            goto label_17;
        }
        nullable1 = application.AdjdHasPPDTaxes;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = application.PendingPPD;
          if (!nullable1.GetValueOrDefault())
          {
            APAdjust ppdApplication = APPaymentEntry.GetPPDApplication((PXGraph) this, application.AdjdDocType, application.AdjdRefNbr);
            if (ppdApplication != null)
              throw new PXSetPropertyException("To proceed, you have to reverse application of the final payment {0} with cash discount given.", new object[1]
              {
                (object) ppdApplication.AdjgRefNbr
              });
          }
        }
      }
label_17:
      nullable1 = eventTarget.OpenDoc;
      if (!nullable1.GetValueOrDefault())
      {
        eventTarget.OpenDoc = new bool?(true);
        PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (ev => ev.OpenDocument)).FireOn((PXGraph) this, eventTarget);
        eventTarget = this.Document.Update(eventTarget);
      }
      APAdjust copy = PXCache<APAdjust>.CreateCopy(application);
      copy.Voided = new bool?(true);
      copy.VoidAdjNbr = copy.AdjNbr;
      copy.Released = new bool?(false);
      this.Adjustments.Cache.SetDefaultExt<APAdjust.isMigratedRecord>((object) copy);
      copy.AdjNbr = eventTarget.AdjCntr;
      copy.AdjBatchNbr = (string) null;
      copy.AdjgDocDate = eventTarget.AdjDate;
      APAdjust apAdjust1 = new APAdjust()
      {
        AdjgDocType = copy.AdjgDocType,
        AdjgRefNbr = copy.AdjgRefNbr,
        AdjgBranchID = copy.AdjgBranchID,
        AdjdDocType = copy.AdjdDocType,
        AdjdRefNbr = copy.AdjdRefNbr,
        AdjdLineNbr = copy.AdjdLineNbr,
        AdjdBranchID = copy.AdjdBranchID,
        VendorID = copy.VendorID,
        AdjNbr = copy.AdjNbr,
        AdjdCuryInfoID = copy.AdjdCuryInfoID,
        AdjdHasPPDTaxes = copy.AdjdHasPPDTaxes,
        JointPayeeID = copy.JointPayeeID,
        InvoiceID = copy.InvoiceID,
        PaymentID = copy.PaymentID,
        MemoID = copy.MemoID
      };
      try
      {
        this.AutoPaymentApp = true;
        this.IsReverseProc = true;
        APAdjust row = this.Adjustments.Insert(apAdjust1);
        if (row != null)
        {
          APAdjust apAdjust2 = copy;
          Decimal num1 = (Decimal) -1;
          Decimal? curyAdjgAmt = copy.CuryAdjgAmt;
          Decimal? nullable2 = curyAdjgAmt.HasValue ? new Decimal?(num1 * curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust2.CuryAdjgAmt = nullable2;
          APAdjust apAdjust3 = copy;
          Decimal num2 = (Decimal) -1;
          Decimal? curyAdjgDiscAmt = copy.CuryAdjgDiscAmt;
          Decimal? nullable3 = curyAdjgDiscAmt.HasValue ? new Decimal?(num2 * curyAdjgDiscAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust3.CuryAdjgDiscAmt = nullable3;
          APAdjust apAdjust4 = copy;
          Decimal num3 = (Decimal) -1;
          Decimal? curyAdjgPpdAmt = copy.CuryAdjgPPDAmt;
          Decimal? nullable4 = curyAdjgPpdAmt.HasValue ? new Decimal?(num3 * curyAdjgPpdAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust4.CuryAdjgPPDAmt = nullable4;
          APAdjust apAdjust5 = copy;
          Decimal num4 = (Decimal) -1;
          Decimal? curyAdjgWhTaxAmt = copy.CuryAdjgWhTaxAmt;
          Decimal? nullable5 = curyAdjgWhTaxAmt.HasValue ? new Decimal?(num4 * curyAdjgWhTaxAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust5.CuryAdjgWhTaxAmt = nullable5;
          APAdjust apAdjust6 = copy;
          Decimal num5 = (Decimal) -1;
          Decimal? adjAmt = copy.AdjAmt;
          Decimal? nullable6 = adjAmt.HasValue ? new Decimal?(num5 * adjAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust6.AdjAmt = nullable6;
          APAdjust apAdjust7 = copy;
          Decimal num6 = (Decimal) -1;
          Decimal? adjDiscAmt = copy.AdjDiscAmt;
          Decimal? nullable7 = adjDiscAmt.HasValue ? new Decimal?(num6 * adjDiscAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust7.AdjDiscAmt = nullable7;
          APAdjust apAdjust8 = copy;
          Decimal num7 = (Decimal) -1;
          Decimal? adjPpdAmt = copy.AdjPPDAmt;
          Decimal? nullable8 = adjPpdAmt.HasValue ? new Decimal?(num7 * adjPpdAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust8.AdjPPDAmt = nullable8;
          APAdjust apAdjust9 = copy;
          Decimal num8 = (Decimal) -1;
          Decimal? adjWhTaxAmt = copy.AdjWhTaxAmt;
          Decimal? nullable9 = adjWhTaxAmt.HasValue ? new Decimal?(num8 * adjWhTaxAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust9.AdjWhTaxAmt = nullable9;
          APAdjust apAdjust10 = copy;
          Decimal num9 = (Decimal) -1;
          Decimal? curyAdjdAmt = copy.CuryAdjdAmt;
          Decimal? nullable10 = curyAdjdAmt.HasValue ? new Decimal?(num9 * curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust10.CuryAdjdAmt = nullable10;
          APAdjust apAdjust11 = copy;
          Decimal num10 = (Decimal) -1;
          Decimal? curyAdjdDiscAmt = copy.CuryAdjdDiscAmt;
          Decimal? nullable11 = curyAdjdDiscAmt.HasValue ? new Decimal?(num10 * curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust11.CuryAdjdDiscAmt = nullable11;
          APAdjust apAdjust12 = copy;
          Decimal num11 = (Decimal) -1;
          Decimal? curyAdjdPpdAmt = copy.CuryAdjdPPDAmt;
          Decimal? nullable12 = curyAdjdPpdAmt.HasValue ? new Decimal?(num11 * curyAdjdPpdAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust12.CuryAdjdPPDAmt = nullable12;
          APAdjust apAdjust13 = copy;
          Decimal num12 = (Decimal) -1;
          Decimal? curyAdjdWhTaxAmt = copy.CuryAdjdWhTaxAmt;
          Decimal? nullable13 = curyAdjdWhTaxAmt.HasValue ? new Decimal?(num12 * curyAdjdWhTaxAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust13.CuryAdjdWhTaxAmt = nullable13;
          APAdjust apAdjust14 = copy;
          Decimal num13 = (Decimal) -1;
          Decimal? rgolAmt = copy.RGOLAmt;
          Decimal? nullable14 = rgolAmt.HasValue ? new Decimal?(num13 * rgolAmt.GetValueOrDefault()) : new Decimal?();
          apAdjust14.RGOLAmt = nullable14;
          copy.AdjgCuryInfoID = eventTarget.CuryInfoID;
          this.Adjustments.Cache.SetDefaultExt<APAdjust.noteID>((object) copy);
          this.Adjustments.Update(copy);
          FinPeriodIDAttribute.SetPeriodsByMaster<APAdjust.adjgFinPeriodID>(this.Adjustments.Cache, (object) row, this.Document.Current.AdjTranPeriodID);
        }
      }
      finally
      {
        this.AutoPaymentApp = false;
        this.IsReverseProc = false;
      }
    }
    return adapter.Get();
  }

  public static APAdjust GetPPDApplication(PXGraph graph, string DocType, string RefNbr)
  {
    return (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.released, Equal<PX.Data.True>, And<APAdjust.voided, NotEqual<PX.Data.True>, And<APAdjust.pendingPPD, Equal<PX.Data.True>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) DocType, (object) RefNbr);
  }

  [PXUIField(DisplayName = "View Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXLookupButton]
  public virtual IEnumerable ViewDocumentToApply(PXAdapter adapter)
  {
    APAdjust current = this.Adjustments.Current;
    if (string.IsNullOrEmpty(current?.AdjdDocType) || string.IsNullOrEmpty(current?.AdjdRefNbr))
      return adapter.Get();
    if (current.AdjdDocType == "CHK" || current.AdjgDocType == "REF" && current.AdjdDocType == "PPM")
    {
      APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
      instance.Document.Current = (APPayment) instance.Document.Search<APPayment.refNbr>((object) current.AdjdRefNbr, (object) current.AdjdDocType);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
      requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
      throw requiredException;
    }
    APInvoiceEntry instance1 = PXGraph.CreateInstance<APInvoiceEntry>();
    instance1.Document.Current = (APInvoice) instance1.Document.Search<APInvoice.refNbr>((object) current.AdjdRefNbr, (object) current.AdjdDocType);
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "View Document");
    requiredException1.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException1;
  }

  [PXUIField(DisplayName = "View Application Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXLookupButton(Tooltip = "View Application Document")]
  public virtual IEnumerable ViewApplicationDocument(PXAdapter adapter)
  {
    APTranPostBal current = this.APPost.Current;
    if (current == null || string.IsNullOrEmpty(current.SourceDocType) || string.IsNullOrEmpty(current.SourceRefNbr))
      return adapter.Get();
    string sourceDocType = current.SourceDocType;
    PXGraph graph;
    if (sourceDocType == "CHK" || sourceDocType == "PPM" || sourceDocType == "REF" || sourceDocType == "VRF" || sourceDocType == "VCK")
    {
      APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
      instance.Document.Current = (APPayment) instance.Document.Search<APPayment.refNbr>((object) current.SourceRefNbr, (object) current.SourceDocType);
      graph = (PXGraph) instance;
    }
    else
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      instance.Document.Current = (APInvoice) instance.Document.Search<APInvoice.refNbr>((object) current.SourceRefNbr, (object) current.SourceDocType);
      graph = (PXGraph) instance;
    }
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException(graph, true, "View Application Document");
    requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException;
  }

  [PXUIField(DisplayName = "Validate Addresses", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, FieldClass = "Validate Address")]
  [PXButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    APPaymentEntry apPaymentEntry = this;
    foreach (APPayment apPayment in adapter.Get<APPayment>())
    {
      if (apPayment != null)
        apPaymentEntry.FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) apPayment;
    }
  }

  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  protected virtual IEnumerable viewOriginalDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(this.Document.Current?.OrigDocType, this.Document.Current?.OrigRefNbr, this.Document.Current?.OrigModule);
    return adapter.Get();
  }

  protected virtual void CATran_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void CATran_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void CATran_TranPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void CATran_ReferenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void CATran_CuryID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void APPayment_CuryID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
  }

  protected virtual void APPayment_DocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "CHK";
  }

  protected virtual Dictionary<System.Type, System.Type> CreateApplicationMap(bool invoiceSide)
  {
    if (invoiceSide)
      return new Dictionary<System.Type, System.Type>()
      {
        {
          typeof (APAdjust.displayDocType),
          typeof (APAdjust.adjdDocType)
        },
        {
          typeof (APAdjust.displayRefNbr),
          typeof (APAdjust.adjdRefNbr)
        },
        {
          typeof (APAdjust.displayDocDate),
          typeof (APInvoice.docDate)
        },
        {
          typeof (APAdjust.displayDocDesc),
          typeof (APRegisterAlias.docDesc)
        },
        {
          typeof (APAdjust.displayCuryID),
          typeof (APInvoice.curyID)
        },
        {
          typeof (APAdjust.displayFinPeriodID),
          typeof (APInvoice.finPeriodID)
        },
        {
          typeof (APAdjust.displayStatus),
          typeof (APInvoice.status)
        },
        {
          typeof (APAdjust.displayCuryInfoID),
          typeof (APInvoice.curyInfoID)
        },
        {
          typeof (APAdjust.displayCuryAmt),
          typeof (APAdjust.curyAdjgAmt)
        },
        {
          typeof (APAdjust.displayCuryDiscAmt),
          typeof (APAdjust.curyAdjgDiscAmt)
        },
        {
          typeof (APAdjust.displayCuryWhTaxAmt),
          typeof (APAdjust.curyAdjgWhTaxAmt)
        },
        {
          typeof (APAdjust.displayCuryPPDAmt),
          typeof (APAdjust.curyAdjgPPDAmt)
        },
        {
          typeof (APInvoice.docDesc),
          typeof (APRegisterAlias.docDesc)
        }
      };
    return new Dictionary<System.Type, System.Type>()
    {
      {
        typeof (APAdjust.displayDocType),
        typeof (APAdjust.adjgDocType)
      },
      {
        typeof (APAdjust.displayRefNbr),
        typeof (APAdjust.adjgRefNbr)
      },
      {
        typeof (APAdjust.displayDocDate),
        typeof (APPayment.docDate)
      },
      {
        typeof (APAdjust.displayDocDesc),
        typeof (APRegisterAlias.docDesc)
      },
      {
        typeof (APAdjust.displayCuryID),
        typeof (APPayment.curyID)
      },
      {
        typeof (APAdjust.displayFinPeriodID),
        typeof (APPayment.finPeriodID)
      },
      {
        typeof (APAdjust.displayStatus),
        typeof (APPayment.status)
      },
      {
        typeof (APAdjust.displayCuryInfoID),
        typeof (APPayment.curyInfoID)
      },
      {
        typeof (APAdjust.displayCuryAmt),
        typeof (APAdjust.curyAdjdAmt)
      },
      {
        typeof (APAdjust.displayCuryDiscAmt),
        typeof (APAdjust.curyAdjdDiscAmt)
      },
      {
        typeof (APAdjust.displayCuryWhTaxAmt),
        typeof (APAdjust.curyAdjdWhTaxAmt)
      },
      {
        typeof (APAdjust.displayCuryPPDAmt),
        typeof (APAdjust.curyAdjdPPDAmt)
      },
      {
        typeof (APInvoice.docDesc),
        typeof (APRegisterAlias.docDesc)
      }
    };
  }

  protected virtual IEnumerable adjustments()
  {
    if (this.balanceCache == null)
      this.FillBalanceCache(this.Document.Current);
    int startRow = PX.Data.PXView.StartRow;
    int totalRows = 0;
    if (this.Document.Current == null || this.Document.Current.DocType != "REF" && this.Document.Current.DocType != "VRF")
    {
      PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, this.CreateApplicationMap(true), new System.Type[3]
      {
        typeof (APAdjust),
        typeof (APInvoice),
        typeof (APTran)
      });
      PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult();
      this.Adjustments_Invoices_BeforeSelect();
      foreach (PXResult<APAdjust, APInvoice, APRegisterAlias, APTran> pxResult in this.Adjustments_Invoices.View.Select(PX.Data.PXView.Currents, (object[]) null, pxResultMapper.Searches, pxResultMapper.SortColumns, pxResultMapper.Descendings, this.SelectAdjustmentsFilters((PXFilterRow[]) pxResultMapper.Filters), ref startRow, PX.Data.PXView.MaximumRows, ref totalRows))
      {
        APInvoice apInvoice = (APInvoice) pxResult;
        APTran apTran = (APTran) pxResult;
        PXCache<APRegister>.RestoreCopy((APRegister) apInvoice, (APRegister) (APRegisterAlias) pxResult);
        if (this.Adjustments.Cache.GetStatus((object) (APAdjust) pxResult) == PXEntryStatus.Notchanged)
          this.GetExtension<APPaymentEntry.APPaymentEntryDocumentExtension>().CalcBalances<APInvoice>((APAdjust) pxResult, apInvoice, true, !this.TakeDiscAlways, apTran);
        delegateResult.Add(pxResultMapper.CreateResult((PXResult) new PXResult<APAdjust, APInvoice, APRegisterAlias, APTran>((APAdjust) pxResult, apInvoice, (APRegisterAlias) pxResult, apTran)));
      }
      PX.Data.PXView.StartRow = 0;
      return (IEnumerable) delegateResult;
    }
    PXResultMapper pxResultMapper1 = new PXResultMapper((PXGraph) this, this.CreateApplicationMap(false), new System.Type[2]
    {
      typeof (APAdjust),
      typeof (APInvoice)
    });
    PXDelegateResult delegateResult1 = pxResultMapper1.CreateDelegateResult();
    foreach (PXResult<APAdjust, APPayment, APInvoice, APRegisterAlias> pxResult in this.Adjustments_Payments.View.Select(PX.Data.PXView.Currents, (object[]) null, pxResultMapper1.Searches, pxResultMapper1.SortColumns, pxResultMapper1.Descendings, this.SelectAdjustmentsFilters((PXFilterRow[]) pxResultMapper1.Filters), ref startRow, PX.Data.PXView.MaximumRows, ref totalRows))
    {
      APPayment apPayment = (APPayment) pxResult;
      APInvoice i3 = (APInvoice) pxResult;
      PXCache<APRegister>.RestoreCopy((APRegister) apPayment, (APRegister) (APRegisterAlias) pxResult);
      if (this.Adjustments.Cache.GetStatus((object) (APAdjust) pxResult) == PXEntryStatus.Notchanged)
        this.GetExtension<APPaymentEntry.APPaymentEntryDocumentExtension>().CalcBalances<APPayment>((APAdjust) pxResult, apPayment, true, !this.TakeDiscAlways, (APTran) null);
      delegateResult1.Add(pxResultMapper1.CreateResult((PXResult) new PXResult<APAdjust, APPayment, APRegisterAlias, APInvoice>((APAdjust) pxResult, apPayment, (APRegisterAlias) pxResult, i3)));
    }
    PX.Data.PXView.StartRow = 0;
    return (IEnumerable) delegateResult1;
  }

  protected virtual PXFilterRow[] SelectAdjustmentsFilters(PXFilterRow[] filters) => filters;

  public virtual IEnumerable appost()
  {
    using (new PXReadBranchRestrictedScope())
      return GraphHelper.QuickSelect(this, this.APPost.View.BqlSelect, this.SelectAPPostFilters((PXFilterRow[]) PX.Data.PXView.Filters));
  }

  protected virtual PXFilterRow[] SelectAPPostFilters(PXFilterRow[] filters) => filters;

  protected virtual IEnumerable adjustments_print()
  {
    if (this.Document.Current.DocType == "QCK")
    {
      foreach (PXResult<APAdjust> pxResult in this.Adjustments_Raw.Select())
        this.Adjustments.Cache.Delete((object) (APAdjust) pxResult);
      APPayment current = this.Document.Current;
      APPayment apPayment = current;
      int? adjCntr = apPayment.AdjCntr;
      apPayment.AdjCntr = adjCntr.HasValue ? new int?(adjCntr.GetValueOrDefault() + 1) : new int?();
      APAdjust apAdjust1 = this.Adjustments.Insert(new APAdjust()
      {
        AdjdDocType = current.DocType,
        AdjdRefNbr = current.RefNbr,
        AdjdLineNbr = new int?(0),
        AdjdBranchID = current.BranchID,
        AdjdOrigCuryInfoID = current.CuryInfoID,
        AdjgCuryInfoID = current.CuryInfoID,
        AdjdCuryInfoID = current.CuryInfoID
      });
      APAdjust apAdjust2 = apAdjust1;
      Decimal? nullable1 = current.CuryOrigDocAmt;
      Decimal? nullable2 = current.CuryOrigDiscAmt;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable4 = current.CuryOrigWhTaxAmt;
      Decimal? nullable5;
      if (!(nullable3.HasValue & nullable4.HasValue))
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault());
      apAdjust2.CuryDocBal = nullable5;
      apAdjust1.CuryDiscBal = current.CuryOrigDiscAmt;
      apAdjust1.CuryWhTaxBal = current.CuryOrigWhTaxAmt;
      APAdjust copy = PXCache<APAdjust>.CreateCopy(apAdjust1);
      copy.AdjgDocType = current.DocType;
      copy.AdjgRefNbr = current.RefNbr;
      copy.AdjdAPAcct = current.APAccountID;
      copy.AdjdAPSub = current.APSubID;
      copy.AdjdCuryInfoID = current.CuryInfoID;
      copy.AdjdDocDate = current.DocDate;
      FinPeriodIDAttribute.SetPeriodsByMaster<APAdjust.adjdFinPeriodID>(this.Adjustments.Cache, (object) copy, current.TranPeriodID);
      copy.AdjdOrigCuryInfoID = current.CuryInfoID;
      copy.AdjgCuryInfoID = current.CuryInfoID;
      copy.AdjgDocDate = current.DocDate;
      FinPeriodIDAttribute.SetPeriodsByMaster<APAdjust.adjgFinPeriodID>(this.Adjustments.Cache, (object) copy, current.TranPeriodID);
      copy.AdjNbr = current.AdjCntr;
      copy.AdjAmt = current.OrigDocAmt;
      copy.AdjDiscAmt = current.OrigDiscAmt;
      copy.AdjWhTaxAmt = current.OrigWhTaxAmt;
      copy.RGOLAmt = new Decimal?(0M);
      copy.CuryAdjdAmt = current.CuryOrigDocAmt;
      copy.CuryAdjdDiscAmt = current.CuryOrigDiscAmt;
      copy.CuryAdjdWhTaxAmt = current.CuryOrigWhTaxAmt;
      copy.CuryAdjgAmt = current.CuryOrigDocAmt;
      copy.CuryAdjgDiscAmt = current.CuryOrigDiscAmt;
      copy.CuryAdjgWhTaxAmt = current.CuryOrigWhTaxAmt;
      APAdjust apAdjust3 = copy;
      nullable2 = current.CuryOrigDocAmt;
      nullable1 = current.CuryOrigDiscAmt;
      nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      nullable3 = current.CuryOrigWhTaxAmt;
      Decimal? nullable6;
      if (!(nullable4.HasValue & nullable3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable6 = nullable1;
      }
      else
        nullable6 = new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault());
      apAdjust3.CuryDocBal = nullable6;
      copy.CuryDiscBal = current.CuryOrigDiscAmt;
      copy.CuryWhTaxBal = current.CuryOrigWhTaxAmt;
      copy.Released = new bool?(false);
      copy.VendorID = current.VendorID;
      this.Adjustments.Cache.Update((object) copy);
    }
    return (IEnumerable) new FbqlSelect<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust.adjgDocType, Equal<BqlField<APPayment.docType, IBqlString>.AsOptional>>>>>.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsEqual<BqlField<APPayment.refNbr, IBqlString>.AsOptional>>>, APAdjust>.View((PXGraph) this).SelectMain();
  }

  public override int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName.Equals("Document", StringComparison.OrdinalIgnoreCase) && values != null)
    {
      PXCache cache = this.Views[viewName].Cache;
      foreach (string key in values.Keys.Cast<string>().Where<string>((Func<string, bool>) (_ => cache.GetStateExt(cache.Current, _) is PXFieldState stateExt && !stateExt.Enabled)).ToArray<string>())
        values[(object) key] = PXCache.NotSetValue;
      values[(object) "CuryApplAmt"] = PXCache.NotSetValue;
      values[(object) "CuryUnappliedBal"] = PXCache.NotSetValue;
    }
    return base.ExecuteUpdate(viewName, keys, values, parameters);
  }

  protected bool InternalCall => this.UnattendedMode;

  public APPaymentEntry()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    OpenPeriodAttribute.SetValidatePeriod<APPayment.adjFinPeriodID>(this.Document.Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    this.created = new DocumentList<APPayment>((PXGraph) this);
    this.OnBeforeCommit += new System.Action<PXGraph>(this.OnBeforeCommitPayment);
  }

  public DocumentList<APPayment> created { get; }

  public Dictionary<long, PX.Objects.CM.Extensions.CurrencyInfo> createdInfo { get; } = new Dictionary<long, PX.Objects.CM.Extensions.CurrencyInfo>();

  public virtual void Segregate(APAdjust adj, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    if (this.IsDirty)
      this.Save.Press();
    APInvoice apdoc = (APInvoice) this.APInvoice_VendorID_DocType_RefNbr.Select((object) adj.AdjdLineNbr, (object) adj.VendorID, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr);
    APPayment apPayment1 = apdoc != null ? this.FindOrCreatePayment(apdoc, adj) : throw new AdjustedNotFoundException();
    bool? separateCheck = adj.SeparateCheck;
    if ((!separateCheck.GetValueOrDefault() || adj.AdjdDocType == "ADR") && apPayment1.RefNbr != null)
    {
      this.Document.Current = (APPayment) this.Document.Search<APPayment.refNbr>((object) apPayment1.RefNbr, (object) apPayment1.DocType);
      this.Document.Current.HiddenKey = apPayment1.HiddenKey;
      PX.Objects.CM.Extensions.CurrencyInfo info1;
      if (!this.createdInfo.TryGetValue(this.Document.Current.CuryInfoID.Value, out info1))
        return;
      this.FindImplementation<APPaymentEntry.MultiCurrency>()?.StoreResult(info1);
    }
    else
    {
      if (!(adj.AdjdDocType == "ADR"))
      {
        Decimal? adjAmt = adj.AdjAmt;
        Decimal num = 0M;
        if (!(adjAmt.GetValueOrDefault() < num & adjAmt.HasValue))
        {
          this.Document.View.Answer = WebDialogResult.No;
          info.CuryInfoID = new long?();
          info = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(info));
          APPayment apPayment2 = new APPayment();
          apPayment2.CuryInfoID = info.CuryInfoID;
          apPayment2.DocType = "CHK";
          separateCheck = adj.SeparateCheck;
          apPayment2.HiddenKey = separateCheck.GetValueOrDefault() ? $"{apdoc.DocType}_{apdoc.RefNbr}" : (string) null;
          APPayment copy1 = PXCache<APPayment>.CreateCopy(this.Document.Insert(apPayment2));
          this.FillOutAPPAyment(copy1, apdoc, adj);
          APPayment copy2 = PXCache<APPayment>.CreateCopy(this.Document.Update(copy1));
          if (copy2.ExtRefNbr == null)
            this.Document.Update(copy2);
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APPayment.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null);
          currencyInfo.CuryID = info.CuryID;
          currencyInfo.CuryEffDate = info.CuryEffDate;
          currencyInfo.CuryRateTypeID = info.CuryRateTypeID;
          currencyInfo.CuryRate = info.CuryRate;
          currencyInfo.RecipRate = info.RecipRate;
          currencyInfo.CuryMultDiv = info.CuryMultDiv;
          this.currencyinfo.Update(currencyInfo);
          return;
        }
      }
      throw new PXSetPropertyException("The debit adjustment cannot be applied as there are neither open bills nor credit adjustments from the vendor.", PXErrorLevel.Warning);
    }
  }

  protected virtual APPayment FindOrCreatePayment(APInvoice apdoc, APAdjust adj)
  {
    APPayment orCreatePayment = this.created.Find<APPayment.vendorID, APPayment.vendorLocationID, APRegister.hiddenKey>((object) apdoc.VendorID, (object) apdoc.PayLocationID, (object) $"{apdoc.DocType}_{apdoc.RefNbr}");
    if (orCreatePayment != null)
      return orCreatePayment;
    return this.created.Find<APPayment.vendorID, APPayment.vendorLocationID, APRegister.hiddenKey>((object) apdoc.VendorID, (object) apdoc.PayLocationID, null) ?? new APPayment();
  }

  protected virtual void FillOutAPPAyment(APPayment payment, APInvoice apdoc, APAdjust adj)
  {
    payment.BranchID = adj.AdjgBranchID;
    payment.VendorID = apdoc.VendorID;
    payment.VendorLocationID = apdoc.PayLocationID;
    payment.AdjDate = adj.AdjgDocDate;
    payment.AdjFinPeriodID = adj.AdjgFinPeriodID;
    payment.AdjTranPeriodID = adj.AdjgTranPeriodID;
    payment.CashAccountID = apdoc.PayAccountID;
    payment.PaymentMethodID = apdoc.PayTypeID;
    payment.DocDesc = apdoc.DocDesc;
  }

  public void OnBeforeCommitPayment(PXGraph graph)
  {
    PXSelectJoin<APPayment, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>, Where<APPayment.docType, Equal<Optional<APPayment.docType>>, PX.Data.And<Where<Vendor.bAccountID, PX.Data.IsNull, PX.Data.Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> document = this.Document;
    if ((document != null ? (((bool?) document.Current?.VoidAppl).GetValueOrDefault() ? 1 : 0) : 0) != 0 || this.Document?.Current?.DocType == "QCK")
      return;
    APAdjust apAdjust = new FbqlSelect<SelectFromBase<APAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APRegisterAlias2>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegisterAlias2.refNbr, Equal<APAdjust.adjdRefNbr>>>>>.And<BqlOperand<APRegisterAlias2.docType, IBqlString>.IsEqual<APAdjust.adjdDocType>>>>, FbqlJoins.Left<APInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>>.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APAdjust.adjdDocType>>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Exists<PX.Data.Select<APAdjust2, PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjust2.adjdRefNbr, Equal<APRegisterAlias2.refNbr>>>>, PX.Data.And<BqlOperand<APAdjust2.adjdDocType, IBqlString>.IsEqual<APRegisterAlias2.docType>>>, PX.Data.And<BqlOperand<APAdjust2.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APAdjust2.adjgDocType, IBqlString>.IsEqual<P.AsString>>>>>>, PX.Data.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Required<Parameter.ofString>, Equal<APDocType.check>>>>, PX.Data.And<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.prepayment>>>, PX.Data.And<BqlOperand<APAdjust.adjgDocType, IBqlString>.IsEqual<APDocType.check>>>, PX.Data.And<BqlOperand<APAdjust.adjgRefNbr, IBqlString>.IsNotEqual<P.AsString>>>>.And<BqlOperand<APAdjust.released, IBqlBool>.IsEqual<PX.Data.True>>>>>.Or<BqlOperand<APAdjust.released, IBqlBool>.IsEqual<False>>>>>.And<BqlOperand<APAdjust.voided, IBqlBool>.IsEqual<False>>>.Aggregate<To<GroupBy<APRegisterAlias2.refNbr>, GroupBy<APRegisterAlias2.docType>, GroupBy<APRegisterAlias2.curyDocBal>, Sum<APAdjust.curyAdjdAmt>>>.Having<BqlAggregatedOperand<Sum<APAdjust.curyAdjdAmt>, IBqlDecimal>.IsGreater<BqlField<APRegisterAlias2.curyDocBal, IBqlDecimal>.Maximized>>, APAdjust>.View((PXGraph) this).SelectSingle((object) this.Document?.Current?.RefNbr, (object) this.Document?.Current?.DocType, (object) this.Document?.Current?.DocType, (object) this.Document?.Current?.RefNbr);
    if (apAdjust != null)
      throw new PXException("The payment cannot be saved, because the total amount of unreleased applications for the {0} document with the {1} type exceeds the document's open balance.", new object[2]
      {
        (object) apAdjust.AdjdRefNbr,
        (object) GetLabel.For<APDocType>(apAdjust.AdjdDocType)
      });
  }

  public override void Persist()
  {
    foreach (APPayment data in this.Document.Cache.Updated)
    {
      bool? nullable1 = data.OpenDoc;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = (bool?) this.Document.Cache.GetValueOriginal<APPayment.openDoc>((object) data);
        bool flag1 = false;
        if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
        {
          Decimal? nullable2 = data.DocBal;
          Decimal num1 = 0M;
          if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
          {
            nullable2 = data.UnappliedBal;
            Decimal num2 = 0M;
            if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
            {
              nullable1 = data.Released;
              if (nullable1.GetValueOrDefault())
              {
                nullable1 = data.Hold;
                bool flag2 = false;
                if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
                  continue;
              }
              else
                continue;
            }
            else
              continue;
          }
          else
            continue;
        }
        if (this.Adjustments_Raw.SelectSingle((object) data.DocType, (object) data.RefNbr) == null)
        {
          data.OpenDoc = new bool?(false);
          this.Document.Cache.RaiseRowSelected((object) data);
        }
      }
    }
    base.Persist();
    if (this.Document.Current == null)
      return;
    APPayment apPayment = this.created.Find((object) this.Document.Current);
    if (apPayment == null)
      this.created.Add(this.Document.Current);
    else
      this.Document.Cache.RestoreCopy((object) apPayment, (object) this.Document.Current);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.FindImplementation<APPaymentEntry.MultiCurrency>()?.GetCurrencyInfo(this.Document.Current.CuryInfoID);
    if (currencyInfo == null || !currencyInfo.CuryInfoID.HasValue)
      return;
    this.createdInfo[currencyInfo.CuryInfoID.Value] = currencyInfo;
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>() || string.IsNullOrEmpty(this.cashaccount.Current?.CuryID))
      return;
    e.NewValue = (object) this.cashaccount.Current.CuryID;
    e.Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>() || string.IsNullOrEmpty(this.cashaccount.Current?.CuryRateTypeID))
      return;
    e.NewValue = (object) this.cashaccount.Current.CuryRateTypeID;
    e.Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Cache.Current == null)
      return;
    e.NewValue = (object) ((APRegister) this.Document.Cache.Current).DocDate;
    e.Cancel = true;
  }

  protected virtual void APPayment_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.vendor.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<APInvoice.vendorLocationID>(e.Row);
  }

  protected virtual void APPayment_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.location.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<APPayment.paymentMethodID>(e.Row);
    sender.SetDefaultExt<APPayment.aPAccountID>(e.Row);
    sender.SetDefaultExt<APPayment.aPSubID>(e.Row);
    try
    {
      SharedRecordAttribute.DefaultRecord<APPayment.remitAddressID>(sender, e.Row);
      SharedRecordAttribute.DefaultRecord<APPayment.remitContactID>(sender, e.Row);
    }
    catch (PXFieldValueProcessingException ex)
    {
      string locationCd = this.location.Current.LocationCD;
      ex.ErrorValue = (object) locationCd;
      throw;
    }
  }

  protected virtual void APPayment_ExtRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || !(((APRegister) e.Row).DocType == "VCK") && !(((APRegister) e.Row).DocType == "VRF"))
      return;
    e.Cancel = true;
  }

  private static object GetAcctSub<Field>(PXCache cache, object data) where Field : IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return !(valueExt is PXFieldState pxFieldState) ? valueExt : pxFieldState.Value;
  }

  protected virtual void APPayment_APAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.location.Current == null || e.Row == null)
      return;
    e.NewValue = (object) null;
    if (((APRegister) e.Row).DocType == "PPM")
      e.NewValue = APPaymentEntry.GetAcctSub<Vendor.prepaymentAcctID>(this.vendor.Cache, (object) this.vendor.Current);
    if (!string.IsNullOrEmpty((string) e.NewValue))
      return;
    e.NewValue = APPaymentEntry.GetAcctSub<PX.Objects.CR.Location.aPAccountID>(this.location.Cache, (object) this.location.Current);
  }

  protected virtual void APPayment_APSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.location.Current == null || e.Row == null)
      return;
    e.NewValue = (object) null;
    if (((APRegister) e.Row).DocType == "PPM")
      e.NewValue = APPaymentEntry.GetAcctSub<Vendor.prepaymentSubID>(this.vendor.Cache, (object) this.vendor.Current);
    if (!string.IsNullOrEmpty((string) e.NewValue))
      return;
    e.NewValue = APPaymentEntry.GetAcctSub<PX.Objects.CR.Location.aPSubID>(this.location.Cache, (object) this.location.Current);
  }

  protected virtual void APPayment_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((APPayment) e.Row == null || e.NewValue == null)
      return;
    if ((PX.Objects.CA.CashAccount) PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, e.NewValue) == null)
      return;
    foreach (PXResult<APAdjust, APInvoice, APRegisterAlias> copy in this.Adjustments_Invoices.Select())
    {
      APAdjust apAdjust = (APAdjust) copy;
      PXCache<APRegister>.RestoreCopy((APRegister) (APInvoice) copy, (APRegister) (APRegisterAlias) copy);
    }
  }

  protected virtual void APPayment_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APPayment row = (APPayment) e.Row;
    this.cashaccount.RaiseFieldUpdated(sender, e.Row);
    row.Cleared = new bool?(false);
    row.ClearDate = new System.DateTime?();
    if (this.cashaccount.Current != null)
    {
      bool? reconcile = this.cashaccount.Current.Reconcile;
      bool flag = false;
      if (reconcile.GetValueOrDefault() == flag & reconcile.HasValue)
      {
        row.Cleared = new bool?(true);
        row.ClearDate = row.DocDate;
      }
    }
    sender.SetDefaultExt<APPayment.depositAsBatch>(e.Row);
    sender.SetDefaultExt<APPayment.depositAfter>(e.Row);
  }

  protected virtual void APPayment_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.paymenttype.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<APPayment.cashAccountID>(e.Row);
    sender.SetDefaultExt<APPayment.printCheck>(e.Row);
  }

  protected virtual void APPayment_PrintCheck_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (APPaymentEntry.IsPrintableDocType(((APRegister) e.Row).DocType))
      return;
    e.NewValue = (object) false;
    e.Cancel = true;
  }

  /// <summary>
  /// </summary>
  /// <param name="payment"></param>
  /// <returns></returns>
  protected virtual bool MustPrintCheck(APPayment payment)
  {
    return APPaymentEntry.MustPrintCheck((IPrintCheckControlable) payment, this.paymenttype.Current);
  }

  public static bool MustPrintCheck(IPrintCheckControlable payment, PX.Objects.CA.PaymentMethod paymentMethod)
  {
    if (payment != null && paymentMethod != null && APPaymentEntry.IsPrintableDocType(payment.DocType) && !"VQC".Equals(payment.DocType) && !"RQC".Equals(payment.DocType))
    {
      bool? nullable = payment.Printed;
      if (!nullable.GetValueOrDefault())
      {
        nullable = payment.PrintCheck;
        if (nullable.GetValueOrDefault())
        {
          nullable = paymentMethod.PrintOrExport;
          return nullable.GetValueOrDefault();
        }
      }
    }
    return false;
  }

  public static bool IsCheckReallyPrinted(IPrintCheckControlable payment)
  {
    return payment.Printed.GetValueOrDefault() && payment.PrintCheck.GetValueOrDefault();
  }

  protected virtual void APPayment_PrintCheck_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!this.MustPrintCheck(e.Row as APPayment))
      return;
    sender.SetValueExt<APPayment.extRefNbr>(e.Row, (object) null);
  }

  protected virtual void APPayment_AdjDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (((APRegister) e.Row).Released.GetValueOrDefault() || ((APPayment) e.Row).VoidAppl.GetValueOrDefault())
      return;
    sender.SetDefaultExt<APPayment.depositAfter>(e.Row);
  }

  protected override List<KeyValuePair<string, List<PX.Data.Description.FieldInfo>>> AdjustApiScript(
    List<KeyValuePair<string, List<PX.Data.Description.FieldInfo>>> fieldsByView)
  {
    List<KeyValuePair<string, List<PX.Data.Description.FieldInfo>>> source = base.AdjustApiScript(fieldsByView);
    List<PX.Data.Description.FieldInfo> fieldInfoList = source.FirstOrDefault<KeyValuePair<string, List<PX.Data.Description.FieldInfo>>>((Func<KeyValuePair<string, List<PX.Data.Description.FieldInfo>>, bool>) (x => x.Key == "Document")).Value;
    if (fieldInfoList == null)
      return source;
    int index1 = fieldInfoList.FindIndex((Predicate<PX.Data.Description.FieldInfo>) (x => x.FieldName == "PaymentMethodID"));
    int index2 = fieldInfoList.FindIndex((Predicate<PX.Data.Description.FieldInfo>) (x => x.FieldName == "ExtRefNbr"));
    if (index1 == -1 || index2 == -1 || index1 <= index2)
      return source;
    fieldInfoList.Insert(index1 + 1, fieldInfoList[index2]);
    fieldInfoList.RemoveAt(index2);
    return source;
  }

  protected virtual void APPayment_AdjDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APPayment row = (APPayment) e.Row;
    if (!row.VoidAppl.GetValueOrDefault() && this.vendor.Current != null && this.vendor.Current.Vendor1099.GetValueOrDefault())
    {
      AP1099Year ap1099Year = (AP1099Year) PXSelectBase<AP1099Year, PXSelect<AP1099Year, Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>, And<AP1099Year.organizationID, Equal<Required<AP1099Year.organizationID>>>>>.Config>.Select((PXGraph) this, (object) ((System.DateTime) e.NewValue).Year.ToString(), (object) PXAccess.GetParentOrganizationID(row.BranchID));
      if (ap1099Year != null && ap1099Year.Status != "N")
        throw new PXSetPropertyException("The payment application date must be within an open 1099 year.");
    }
    if (!(row.DocType == "VRF"))
      return;
    APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.docType, Equal<APDocType.refund>, And<APPayment.refNbr, Equal<Current<APPayment.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    });
    if (apPayment == null)
      return;
    System.DateTime? docDate = apPayment.DocDate;
    System.DateTime newValue = (System.DateTime) e.NewValue;
    if ((docDate.HasValue ? (docDate.GetValueOrDefault() > newValue ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) apPayment.DocDate.Value.ToString("d")
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APPayment, APPayment.branchID> e)
  {
    APPayment row = e.Row;
    if (row.VoidAppl.GetValueOrDefault() || !row.AdjDate.HasValue || this.vendor.Current == null || !this.vendor.Current.Vendor1099.GetValueOrDefault())
      return;
    AP1099Year ap1099Year = (AP1099Year) PXSelectBase<AP1099Year, PXSelect<AP1099Year, Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>, And<AP1099Year.organizationID, Equal<Required<AP1099Year.organizationID>>>>>.Config>.Select((PXGraph) this, (object) row.AdjDate.Value.Year.ToString(), (object) PXAccess.GetParentOrganizationID((int?) e.NewValue));
    if (ap1099Year != null && ap1099Year.Status != "N")
    {
      PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, (int?) e.NewValue);
      e.NewValue = (object) branch.BranchCD;
      throw new PXSetPropertyException("The payment application date must be within an open 1099 year.");
    }
  }

  protected virtual void APPayment_CuryOrigDocAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (((APRegister) e.Row).Released.Value)
      return;
    sender.SetValueExt<APPayment.curyDocBal>(e.Row, (object) ((APRegister) e.Row).CuryOrigDocAmt);
  }

  protected virtual void APAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    APAdjust row = (APAdjust) e.Row;
    string error1 = PXUIFieldAttribute.GetError<APAdjust.adjdRefNbr>(sender, e.Row);
    string error2 = PXUIFieldAttribute.GetError<APAdjust.adjdLineNbr>(sender, e.Row);
    PXRowInsertingEventArgs insertingEventArgs = e;
    int num;
    if (row.AdjdRefNbr != null && row.AdjdLineNbr.HasValue && string.IsNullOrEmpty(error1) && string.IsNullOrEmpty(error2))
    {
      APPayment current = this.Document.Current;
      num = current != null ? (current.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num = 1;
    insertingEventArgs.Cancel = num != 0;
  }

  protected virtual void APAdjust_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    long? adjdCuryInfoId1 = ((APAdjust) e.Row).AdjdCuryInfoID;
    long? adjgCuryInfoId = ((APAdjust) e.Row).AdjgCuryInfoID;
    if (adjdCuryInfoId1.GetValueOrDefault() == adjgCuryInfoId.GetValueOrDefault() & adjdCuryInfoId1.HasValue == adjgCuryInfoId.HasValue)
      return;
    long? adjdCuryInfoId2 = ((APAdjust) e.Row).AdjdCuryInfoID;
    long? adjdOrigCuryInfoId = ((APAdjust) e.Row).AdjdOrigCuryInfoID;
    if (adjdCuryInfoId2.GetValueOrDefault() == adjdOrigCuryInfoId.GetValueOrDefault() & adjdCuryInfoId2.HasValue == adjdOrigCuryInfoId.HasValue || ((APAdjust) e.Row).VoidAdjNbr.HasValue)
      return;
    foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> pxResult in this.CurrencyInfo_CuryInfoID.Select((object) ((APAdjust) e.Row).AdjdCuryInfoID))
      this.currencyinfo.Delete((PX.Objects.CM.Extensions.CurrencyInfo) pxResult);
  }

  public virtual void APAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APAdjust row = (APAdjust) e.Row;
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete)
      return;
    bool? nullable1 = row.Released;
    int? nullable2;
    if (!nullable1.GetValueOrDefault())
    {
      int? adjNbr = row.AdjNbr;
      nullable2 = (int?) this.Document.Current?.AdjCntr;
      if (adjNbr.GetValueOrDefault() < nullable2.GetValueOrDefault() & adjNbr.HasValue & nullable2.HasValue)
        sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("The application cannot be processed and your changes are not saved. Cancel the changes and start over. If the error persists, please contact support."));
    }
    System.DateTime? nullable3 = row.AdjdDocDate;
    System.DateTime dateTime1 = nullable3.Value;
    ref System.DateTime local = ref dateTime1;
    nullable3 = this.Document.Current.AdjDate;
    System.DateTime dateTime2 = nullable3.Value;
    if (local.CompareTo(dateTime2) > 0)
    {
      if (!(row.AdjgDocType != "CHK") || !(row.AdjgDocType != "VCK") || !(row.AdjgDocType != "PPM"))
      {
        nullable1 = this.APSetup.Current.EarlyChecks;
        bool flag = false;
        if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
          goto label_9;
      }
      if (sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(e.Row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("{0} cannot be less than Document Date.", PXErrorLevel.RowError, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<APPayment.adjDate>(this.Document.Cache)
      })))
        throw new PXRowPersistingException(PXDataUtils.FieldName<APAdjust.adjdDocDate>(), (object) row.AdjdDocDate, "{0} cannot be less than Document Date.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPayment.adjDate>(this.Document.Cache)
        });
    }
label_9:
    if (row.AdjdTranPeriodID.CompareTo(this.Document.Current.AdjTranPeriodID) > 0)
    {
      if (!(row.AdjgDocType != "CHK") || !(row.AdjgDocType != "VCK") || !(row.AdjgDocType != "PPM"))
      {
        nullable1 = this.APSetup.Current.EarlyChecks;
        bool flag = false;
        if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
          goto label_14;
      }
      if (sender.RaiseExceptionHandling<APAdjust.adjdRefNbr>(e.Row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("{0} cannot be less than Document Financial Period.", PXErrorLevel.RowError, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<APPayment.adjFinPeriodID>(this.Document.Cache)
      })))
        throw new PXRowPersistingException(PXDataUtils.FieldName<APAdjust.adjdFinPeriodID>(), (object) row.AdjdFinPeriodID, "{0} cannot be less than Document Financial Period.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APPayment.adjFinPeriodID>(this.Document.Cache)
        });
    }
label_14:
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert && !string.IsNullOrEmpty(row.AdjgFinPeriodID) && ((IEnumerable<string>) APPaymentEntry.AdjgDocTypesToValidateFinPeriod).Contains<string>(row.AdjgDocType))
    {
      PX.Objects.Common.ProcessingResult period = this.FinPeriodUtils.CanPostToPeriod((IFinPeriod) this.FinPeriodRepository.GetByID(row.AdjgFinPeriodID, PXAccess.GetParentOrganizationID(row.AdjgBranchID)), typeof (FinPeriod.aPClosed));
      if (!period.IsSuccess)
        throw new PXRowPersistingException(PXDataUtils.FieldName<APAdjust.adjgFinPeriodID>(), (object) row.AdjgFinPeriodID, period.GetGeneralMessage(), new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APAdjust.adjgFinPeriodID>(sender)
        });
    }
    Decimal? nullable4 = row.CuryOrigDocAmt;
    Decimal num1 = 0M;
    Sign sign1 = nullable4.GetValueOrDefault() < num1 & nullable4.HasValue ? Sign.Minus : Sign.Plus;
    nullable4 = row.CuryWhTaxBal;
    Decimal num2 = 0M;
    Sign sign2 = nullable4.GetValueOrDefault() < num2 & nullable4.HasValue ? Sign.Minus : Sign.Plus;
    Decimal? curyDocBal = row.CuryDocBal;
    Sign sign3 = sign1;
    nullable4 = curyDocBal.HasValue ? new Decimal?(Sign.op_Multiply(curyDocBal.GetValueOrDefault(), sign3)) : new Decimal?();
    Decimal num3 = 0M;
    if (nullable4.GetValueOrDefault() < num3 & nullable4.HasValue)
      sender.RaiseExceptionHandling<APAdjust.curyAdjgAmt>(e.Row, (object) row.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    Decimal? nullable5;
    if (row.AdjgDocType != "QCK")
    {
      nullable5 = row.CuryDiscBal;
      Sign sign4 = sign1;
      nullable4 = nullable5.HasValue ? new Decimal?(Sign.op_Multiply(nullable5.GetValueOrDefault(), sign4)) : new Decimal?();
      Decimal num4 = 0M;
      if (nullable4.GetValueOrDefault() < num4 & nullable4.HasValue)
        sender.RaiseExceptionHandling<APAdjust.curyAdjgPPDAmt>(e.Row, (object) row.CuryAdjgPPDAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    }
    if (row.AdjgDocType != "QCK")
    {
      nullable5 = row.CuryWhTaxBal;
      Sign sign5 = sign2;
      nullable4 = nullable5.HasValue ? new Decimal?(Sign.op_Multiply(nullable5.GetValueOrDefault(), sign5)) : new Decimal?();
      Decimal num5 = 0M;
      if (nullable4.GetValueOrDefault() < num5 & nullable4.HasValue)
        sender.RaiseExceptionHandling<APAdjust.curyAdjgWhTaxAmt>(e.Row, (object) row.CuryAdjgWhTaxAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    }
    APAdjust apAdjust = row;
    nullable4 = row.CuryAdjgPPDAmt;
    Decimal num6 = 0M;
    int num7;
    if (!(nullable4.GetValueOrDefault() == num6 & nullable4.HasValue))
    {
      nullable1 = row.AdjdHasPPDTaxes;
      num7 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num7 = 0;
    bool? nullable6 = new bool?(num7 != 0);
    apAdjust.PendingPPD = nullable6;
    nullable1 = row.PendingPPD;
    if (nullable1.GetValueOrDefault())
    {
      nullable4 = row.CuryDocBal;
      Decimal num8 = 0M;
      if (!(nullable4.GetValueOrDefault() == num8 & nullable4.HasValue))
      {
        nullable1 = row.Voided;
        if (!nullable1.GetValueOrDefault())
          sender.RaiseExceptionHandling<APAdjust.curyAdjgPPDAmt>(e.Row, (object) row.CuryAdjgPPDAmt, (Exception) new PXSetPropertyException("Cash discount can be applied only on final payment."));
      }
    }
    nullable2 = row.VoidAdjNbr;
    if (!nullable2.HasValue)
    {
      nullable5 = row.CuryAdjgAmt;
      Sign sign6 = sign1;
      nullable4 = nullable5.HasValue ? new Decimal?(Sign.op_Multiply(nullable5.GetValueOrDefault(), sign6)) : new Decimal?();
      Decimal num9 = 0M;
      if (nullable4.GetValueOrDefault() < num9 & nullable4.HasValue)
        throw new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
    }
    nullable2 = row.VoidAdjNbr;
    if (nullable2.HasValue)
    {
      nullable5 = row.CuryAdjgAmt;
      Sign sign7 = sign1;
      nullable4 = nullable5.HasValue ? new Decimal?(Sign.op_Multiply(nullable5.GetValueOrDefault(), sign7)) : new Decimal?();
      Decimal num10 = 0M;
      if (nullable4.GetValueOrDefault() > num10 & nullable4.HasValue)
        throw new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "Incorrect value. The value to be entered must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
    }
    APPayment current = this.Document.Current;
    if (!this.InternalCall && current != null && current.DocType == "CHK")
    {
      nullable1 = current.Hold;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = current.Released;
        if (!nullable1.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(current.Status, "G", "E", "B") && row.AdjdDocType == "PPM")
        {
          nullable4 = row.CuryDocBal;
          Decimal num11 = 0M;
          if (!(nullable4.GetValueOrDefault() == num11 & nullable4.HasValue))
          {
            this.Adjustments.Cache.RaiseExceptionHandling<APAdjust.curyDocBal>((object) row, (object) row.CuryDocBal, (Exception) new PXSetPropertyException("Prepayment '{0}' is not paid in full. Document will not be released.", new object[1]
            {
              (object) row.AdjdRefNbr
            }));
            throw new PXRowPersistingException(typeof (APPayment.hold).Name, (object) current.Hold, "Prepayment '{0}' is not paid in full. Document will not be released.", new object[1]
            {
              (object) row.AdjdRefNbr
            });
          }
        }
      }
    }
    this.ValidatePrepaymentAmount(sender, row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<APAdjust> e)
  {
  }

  protected virtual void ValidatePrepaymentAmount(PXCache sender, APAdjust doc)
  {
    if (doc == null || !EnumerableExtensions.IsIn<string>(doc.AdjgDocType, "CHK", "PPM") || !(doc?.AdjdDocType == "PPM"))
      return;
    APInvoice apInvoice = new PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APAdjust.adjdDocType>>, And<APInvoice.docType, Equal<APDocType.prepayment>, And<APInvoice.refNbr, Equal<Required<APAdjust.adjdRefNbr>>>>>>((PXGraph) this).SelectSingle((object) doc.AdjdDocType, (object) doc.AdjdRefNbr);
    if (apInvoice == null)
      return;
    Decimal? nullable = apInvoice.CuryOrigDocAmt;
    Decimal? curyAdjdAmt1 = doc.CuryAdjdAmt;
    if (nullable.GetValueOrDefault() == curyAdjdAmt1.GetValueOrDefault() & nullable.HasValue == curyAdjdAmt1.HasValue)
      return;
    Decimal? curyOrigDocAmt = apInvoice.CuryOrigDocAmt;
    Decimal? curyAdjdAmt2 = doc.CuryAdjdAmt;
    nullable = curyAdjdAmt2.HasValue ? new Decimal?(-curyAdjdAmt2.GetValueOrDefault()) : new Decimal?();
    if (curyOrigDocAmt.GetValueOrDefault() == nullable.GetValueOrDefault() & curyOrigDocAmt.HasValue == nullable.HasValue)
      return;
    sender.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((object) doc, (object) doc.CuryAdjdAmt, (Exception) new PXSetPropertyException<APAdjust.curyAdjgAmt>("A prepayment request can be paid only in full."));
  }

  /// <summary>Calc CuryAdjgPPDAmt and CuryAdjgAmt</summary>
  /// <param name="adj"></param>
  /// <param name="curyUnappliedBal"></param>
  /// <param name="checkBalance">Check that amount of UnappliedBal is greater than 0</param>
  /// <param name="trySelect">Try to alllay this APAdjust</param>
  /// <returns>Changing amount of payment.CuryUnappliedBal</returns>
  public virtual Decimal applyAPAdjust(
    APAdjust adj,
    Decimal curyUnappliedBal,
    bool checkBalance,
    bool trySelect)
  {
    Decimal num1 = curyUnappliedBal;
    Decimal amountForAdjustment = this.GetDiscAmountForAdjustment(adj);
    Decimal? nullable = adj.CuryAdjgPPDAmt;
    Decimal num2;
    Decimal num3 = num2 = nullable.Value;
    nullable = adj.CuryDocBal;
    Decimal num4 = nullable.Value;
    nullable = adj.CuryAdjgAmt;
    Decimal num5 = nullable.Value;
    Decimal num6 = num5;
    nullable = adj.AdjgBalSign;
    Decimal num7 = nullable.Value;
    bool flag = false;
    if (trySelect)
    {
      if (num4 != 0M)
      {
        Decimal num8 = System.Math.Abs(amountForAdjustment) <= System.Math.Abs(num4) ? amountForAdjustment : num4;
        if (!checkBalance)
        {
          num3 += num8;
          num6 += num4 - num8;
        }
        else if (num4 * num7 - num8 <= curyUnappliedBal)
        {
          num3 += num8;
          num6 += num4 - num8;
        }
        else
          num6 += curyUnappliedBal > 0M ? curyUnappliedBal : 0M;
      }
    }
    else
    {
      num6 = 0M;
      num3 = 0M;
    }
    curyUnappliedBal -= (num6 - num5) * num7;
    Decimal num9 = num3;
    if (num2 != num9)
    {
      adj.CuryAdjgPPDAmt = new Decimal?(num3);
      adj.FillDiscAmts();
      flag = true;
    }
    if (num5 != num6)
    {
      adj.CuryAdjgAmt = new Decimal?(num6);
      this.Caches[typeof (APAdjust)].Update((object) adj);
      flag = true;
    }
    if (flag)
      this.GetExtension<APPaymentEntry.APPaymentEntryDocumentExtension>().CalcBalancesFromAdjustedDocument(adj, true, true);
    adj.Selected = new bool?(num6 != 0M || num3 != 0M);
    return curyUnappliedBal - num1;
  }

  protected virtual Decimal GetDiscAmountForAdjustment(APAdjust adj)
  {
    return !(adj.AdjgDocType == "ADR") ? adj.CuryDiscBal.Value : 0M;
  }

  private bool IsAdjdRefNbrFieldVerifyingDisabled(IAdjustment adj)
  {
    PXSelectJoin<APPayment, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>, Where<APPayment.docType, Equal<Optional<APPayment.docType>>, PX.Data.And<Where<Vendor.bAccountID, PX.Data.IsNull, PX.Data.Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> document = this.Document;
    bool? nullable;
    int num;
    if (document == null)
    {
      num = 0;
    }
    else
    {
      nullable = (bool?) document.Current?.VoidAppl;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0 && !(this.Document?.Current?.DocType == "QCK"))
    {
      if (adj != null)
      {
        nullable = adj.Voided;
        if (nullable.GetValueOrDefault())
          goto label_9;
      }
      if (adj != null)
      {
        nullable = adj.Released;
        if (nullable.GetValueOrDefault())
          goto label_9;
      }
      return this.AutoPaymentApp;
    }
label_9:
    return true;
  }

  protected virtual void APAdjust_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null)
      return;
    APAdjust row = e.Row as APAdjust;
    e.Cancel = this.IsAdjdRefNbrFieldVerifyingDisabled((IAdjustment) row);
    if (this.APSetup.Current.EarlyChecks.GetValueOrDefault() || !EnumerableExtensions.IsIn<string>(this.Document.Current.DocType, "CHK", "VCK", "PPM"))
      return;
    APInvoice apInvoice = this.APInvoice_DocType_RefNbr_Single.SelectSingle((object) row.AdjdDocType, e.NewValue);
    if (apInvoice == null)
      return;
    System.DateTime? docDate = (System.DateTime?) apInvoice?.DocDate;
    System.DateTime? adjDate = this.Document.Current.AdjDate;
    if ((docDate.HasValue & adjDate.HasValue ? (docDate.GetValueOrDefault() <= adjDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 || this.Document.Current.AdjTranPeriodID != null && string.Compare(apInvoice?.TranPeriodID, this.Document.Current.AdjTranPeriodID) > 0)
      throw new PXSetPropertyException("The {0} bill cannot be paid on {1}, as it is posted in a future period ({2}) and the Enable Early Checks check box is cleared on the Account Payable Preferences (AP101000) form.", new object[3]
      {
        (object) apInvoice.RefNbr,
        (object) apInvoice.DocDate,
        (object) FinPeriodIDFormattingAttribute.FormatForError(apInvoice.FinPeriodID)
      });
  }

  protected virtual void APAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.InitApplicationData(e.Row as APAdjust);
  }

  protected virtual void APAdjust_AdjdLineNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APAdjust row = e.Row as APAdjust;
    if (!(e.Cancel = this.IsAdjdRefNbrFieldVerifyingDisabled((IAdjustment) row)) && e.NewValue == null && !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentsByLines>() && (PXSelectorAttribute.Select<APAdjust.adjdRefNbr>(sender, (object) row) is APRegister apRegister ? (apRegister.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("The document was created when the Payment Application by Line feature was enabled. Paying this document can cause inconsistency in balances. To pay the document, do either of the following:\r\n- On the Bills and Adjustments (AP301000) form, select Actions > Pay Bill/Apply Adjustment.\r\n- Use the Prepare Payments (AP503000) form.");
  }

  protected virtual void APAdjust_AdjdLineNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APAdjust row = e.Row as APAdjust;
    int? adjdLineNbr = row.AdjdLineNbr;
    int num = 0;
    if (adjdLineNbr.GetValueOrDefault() == num & adjdLineNbr.HasValue || !row.AdjdLineNbr.HasValue)
      return;
    this.InitApplicationData(row);
  }

  protected virtual void InitApplicationData(APAdjust adj)
  {
    try
    {
      using (IEnumerator<PXResult<APInvoice>> enumerator = this.APInvoice_DocType_RefNbr.Select((object) adj.AdjdLineNbr.GetValueOrDefault(), (object) adj.AdjdDocType, (object) adj.AdjdRefNbr).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran> current = (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran>) enumerator.Current;
          APInvoice invoice = (APInvoice) current;
          PX.Objects.CM.Extensions.CurrencyInfo info = (PX.Objects.CM.Extensions.CurrencyInfo) current;
          APTran tran = (APTran) current;
          this.APAdjust_KeyUpdated<APInvoice>(adj, invoice, info, tran);
          return;
        }
      }
      foreach (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in this.ARPayment_DocType_RefNbr.Select((object) adj.AdjdDocType, (object) adj.AdjdRefNbr))
      {
        APPayment invoice = (APPayment) pxResult;
        PX.Objects.CM.Extensions.CurrencyInfo info = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult;
        this.APAdjust_KeyUpdated<APPayment>(adj, invoice, info);
      }
    }
    catch (PXException ex)
    {
      throw new PXSetPropertyException(ex.Message);
    }
  }

  private void StoreApplicationDataResult(
    APAdjust adj,
    PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, APTran> res)
  {
    foreach (string key in (IEnumerable<string>) this.Adjustments.Cache.Keys)
    {
      if (this.Adjustments.Cache.GetValue((object) adj, key) == null)
        this.Adjustments.Cache.SetDefaultExt((object) adj, key);
    }
    PXSelectJoin<APAdjust, LeftJoin<APInvoice, On<APInvoice.docType, Equal<APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<PX.Data.True>, And<APTran.tranType, Equal<APAdjust.adjdDocType>, And<APTran.refNbr, Equal<APAdjust.adjdRefNbr>, And<APTran.lineNbr, Equal<APAdjust.adjdLineNbr>>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<APAdjust.released, NotEqual<PX.Data.True>>>>> adjustments = this.Adjustments;
    List<object> result = new List<object>();
    result.Add((object) new PXResult<APInvoice, APTran>((APInvoice) res, (APTran) res));
    object[] currents = new object[3]
    {
      (object) (APInvoice) res,
      (object) (APTran) res,
      (object) adj
    };
    object[] objArray = new object[2]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    };
    adjustments.StoreTailResult(result, currents, objArray);
    PXSelectReadonly2<APInvoice, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<PX.Data.True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>>, Where<APInvoice.vendorID, Equal<Required<APInvoice.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> vendorIdDocTypeRefNbr = this.APInvoice_VendorID_DocType_RefNbr;
    List<object> selectResult1 = new List<object>();
    selectResult1.Add((object) new PXResult<APInvoice, APTran>((APInvoice) res, (APTran) res));
    PXQueryParameters queryParameters1 = PXQueryParameters.ExplicitParameters((object) adj.AdjdLineNbr, (object) adj.VendorID, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr);
    vendorIdDocTypeRefNbr.StoreResult(selectResult1, queryParameters1);
    PXSelectJoin<APInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<APTran, On<APInvoice.paymentsByLinesAllowed, Equal<PX.Data.True>, And<APTran.tranType, Equal<APInvoice.docType>, And<APTran.refNbr, Equal<APInvoice.refNbr>, And<APTran.lineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>>>, Where<APInvoice.vendorID, Equal<Current<APPayment.vendorID>>, And<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>> invoiceDocTypeRefNbr = this.APInvoice_DocType_RefNbr;
    List<object> selectResult2 = new List<object>();
    selectResult2.Add((object) res);
    PXQueryParameters queryParameters2 = PXQueryParameters.ExplicitParameters((object) adj.AdjdLineNbr, (object) adj.VendorID, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr);
    invoiceDocTypeRefNbr.StoreResult(selectResult2, queryParameters2);
  }

  private void APAdjust_KeyUpdated<T>(APAdjust adj, T invoice, PX.Objects.CM.Extensions.CurrencyInfo info, APTran tran = null) where T : APRegister, IInvoice, new()
  {
    this.GetExtension<APPaymentEntry.MultiCurrency>().StoreResult(info);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetExtension<APPaymentEntry.MultiCurrency>().CloneCurrencyInfo(info, this.Document.Current.DocDate);
    adj.VendorID = invoice.VendorID;
    adj.AdjgDocDate = this.Document.Current.AdjDate;
    adj.AdjgCuryInfoID = this.Document.Current.CuryInfoID;
    adj.AdjdCuryInfoID = currencyInfo.CuryInfoID;
    adj.AdjdOrigCuryInfoID = info.CuryInfoID;
    adj.AdjdBranchID = invoice.BranchID;
    adj.AdjdAPAcct = invoice.APAccountID;
    adj.AdjdAPSub = invoice.APSubID;
    adj.AdjdDocDate = invoice.DocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<APAdjust.adjdFinPeriodID>(this.Adjustments.Cache, (object) adj, invoice.TranPeriodID);
    adj.AdjdHasPPDTaxes = invoice.HasPPDTaxes;
    adj.Released = new bool?(false);
    adj.PendingPPD = new bool?(false);
    adj.CuryAdjgAmt = new Decimal?(0M);
    adj.CuryAdjgDiscAmt = new Decimal?(0M);
    adj.CuryAdjgPPDAmt = new Decimal?(0M);
    adj.CuryAdjgWhTaxAmt = new Decimal?(0M);
    adj.AdjdCuryID = info.CuryID;
    if (this.Document.Current.DocType == "ADR")
    {
      adj.InvoiceID = invoice.NoteID;
      adj.PaymentID = new Guid?();
      adj.MemoID = this.Document.Current.NoteID;
    }
    else if (invoice.DocType == "ADR")
    {
      adj.InvoiceID = new Guid?();
      adj.PaymentID = this.Document.Current.NoteID;
      adj.MemoID = invoice.NoteID;
    }
    else
    {
      adj.InvoiceID = invoice.NoteID;
      adj.PaymentID = this.Document.Current.NoteID;
      adj.MemoID = new Guid?();
    }
    this.InitAdjustmentData(adj, (APRegister) invoice, tran);
    this.GetExtension<APPaymentEntry.APPaymentEntryDocumentExtension>().CalcBalances<T>(adj, invoice, false, true, tran);
    Decimal? nullable1 = adj.CuryOrigDocAmt;
    Decimal num1 = 0M;
    Sign sign1 = nullable1.GetValueOrDefault() < num1 & nullable1.HasValue ? Sign.Minus : Sign.Plus;
    nullable1 = adj.CuryWhTaxBal;
    Decimal num2 = 0M;
    Decimal? nullable2;
    if (nullable1.GetValueOrDefault() >= num2 & nullable1.HasValue)
    {
      nullable2 = adj.CuryDiscBal;
      Sign sign2 = sign1;
      nullable1 = nullable2.HasValue ? new Decimal?(Sign.op_Multiply(nullable2.GetValueOrDefault(), sign2)) : new Decimal?();
      Decimal num3 = 0M;
      if (nullable1.GetValueOrDefault() >= num3 & nullable1.HasValue)
      {
        Decimal? curyDocBal = adj.CuryDocBal;
        Decimal? curyWhTaxBal = adj.CuryWhTaxBal;
        Decimal? nullable3 = curyDocBal.HasValue & curyWhTaxBal.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - curyWhTaxBal.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable4 = adj.CuryDiscBal;
        nullable2 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
        Sign sign3 = sign1;
        Decimal? nullable5;
        if (!nullable2.HasValue)
        {
          nullable4 = new Decimal?();
          nullable5 = nullable4;
        }
        else
          nullable5 = new Decimal?(Sign.op_Multiply(nullable2.GetValueOrDefault(), sign3));
        nullable1 = nullable5;
        Decimal num4 = 0M;
        if (nullable1.GetValueOrDefault() <= num4 & nullable1.HasValue)
          return;
      }
    }
    Decimal? nullable6 = adj.AdjgDocType == "ADR" ? new Decimal?(0M) : adj.CuryDiscBal;
    Decimal? nullable7 = adj.AdjgDocType == "ADR" ? new Decimal?(0M) : adj.CuryWhTaxBal;
    Decimal? curyUnappliedBal = this.Document.Current.CuryUnappliedBal;
    if (this.Document.Current != null)
    {
      nullable1 = curyUnappliedBal;
      Decimal num5 = 0M;
      if (nullable1.GetValueOrDefault() > num5 & nullable1.HasValue)
      {
        nullable1 = adj.AdjgBalSign;
        Decimal num6 = 0M;
        if (nullable1.GetValueOrDefault() > num6 & nullable1.HasValue)
        {
          nullable1 = curyUnappliedBal;
          nullable2 = nullable6;
          if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
            nullable6 = new Decimal?(0M);
        }
      }
    }
    adj.CuryAdjgAmt = new Decimal?(this.CalculateApplicationAmount(adj));
    adj.CuryAdjgDiscAmt = nullable6;
    adj.CuryAdjgPPDAmt = nullable6;
    adj.CuryAdjgWhTaxAmt = nullable7;
    this.GetExtension<APPaymentEntry.APPaymentEntryDocumentExtension>().CalcBalances<T>(adj, invoice, true, true, tran);
    PXCache<APAdjust>.SyncModel(adj);
  }

  protected virtual void InitAdjustmentData(APAdjust adj, APRegister invoice, APTran tran)
  {
  }

  protected virtual Decimal CalculateApplicationAmount(APAdjust adj)
  {
    Decimal? nullable1 = adj.AdjgDocType == "ADR" ? new Decimal?(0M) : adj.CuryDiscBal;
    Decimal? nullable2 = adj.AdjgDocType == "ADR" ? new Decimal?(0M) : adj.CuryWhTaxBal;
    Decimal? curyDocBal = adj.CuryDocBal;
    Decimal? nullable3 = nullable2;
    Decimal? nullable4 = curyDocBal.HasValue & nullable3.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = nullable1;
    Decimal? nullable6;
    if (!(nullable4.HasValue & nullable5.HasValue))
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
    Decimal? nullable7 = nullable6;
    Decimal? curyUnappliedBal = this.Document.Current.CuryUnappliedBal;
    nullable5 = adj.CuryOrigDocAmt;
    Decimal num1 = 0M;
    Sign sign = nullable5.GetValueOrDefault() < num1 & nullable5.HasValue ? Sign.Minus : Sign.Plus;
    if (this.Document.Current != null)
    {
      nullable5 = adj.AdjgBalSign;
      Decimal num2 = 0M;
      if (nullable5.GetValueOrDefault() < num2 & nullable5.HasValue || Sign.op_Equality(sign, Sign.Minus))
      {
        nullable5 = curyUnappliedBal;
        Decimal num3 = 0M;
        if (nullable5.GetValueOrDefault() < num3 & nullable5.HasValue)
        {
          nullable7 = new Decimal?(System.Math.Min(nullable7.Value, System.Math.Abs(curyUnappliedBal.Value)));
          goto label_20;
        }
        goto label_20;
      }
    }
    Decimal? nullable8;
    if (this.Document.Current != null)
    {
      nullable5 = curyUnappliedBal;
      Decimal num4 = 0M;
      if (nullable5.GetValueOrDefault() > num4 & nullable5.HasValue)
      {
        nullable5 = adj.AdjgBalSign;
        Decimal num5 = 0M;
        if (nullable5.GetValueOrDefault() > num5 & nullable5.HasValue)
        {
          nullable5 = curyUnappliedBal;
          nullable8 = nullable1;
          if (nullable5.GetValueOrDefault() < nullable8.GetValueOrDefault() & nullable5.HasValue & nullable8.HasValue)
          {
            nullable7 = curyUnappliedBal;
            nullable1 = new Decimal?(0M);
            goto label_20;
          }
        }
      }
    }
    if (this.Document.Current != null)
    {
      nullable8 = curyUnappliedBal;
      Decimal num6 = 0M;
      if (nullable8.GetValueOrDefault() > num6 & nullable8.HasValue)
      {
        nullable8 = adj.AdjgBalSign;
        Decimal num7 = 0M;
        if (nullable8.GetValueOrDefault() > num7 & nullable8.HasValue)
        {
          nullable7 = new Decimal?(System.Math.Min(nullable7.Value, curyUnappliedBal.Value));
          goto label_20;
        }
      }
    }
    if (this.Document.Current != null)
    {
      nullable8 = curyUnappliedBal;
      Decimal num8 = 0M;
      if (nullable8.GetValueOrDefault() <= num8 & nullable8.HasValue)
      {
        nullable8 = this.Document.Current.CuryOrigDocAmt;
        Decimal num9 = 0M;
        if (nullable8.GetValueOrDefault() > num9 & nullable8.HasValue)
          nullable7 = new Decimal?(0M);
      }
    }
label_20:
    return nullable7.GetValueOrDefault();
  }

  protected virtual void APAdjust_AdjdCuryRate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((Decimal) e.NewValue <= 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected void FillPPDAmts(APAdjust adj)
  {
    adj.CuryAdjgPPDAmt = adj.CuryAdjgDiscAmt;
    adj.CuryAdjdPPDAmt = adj.CuryAdjdDiscAmt;
    adj.AdjPPDAmt = adj.AdjDiscAmt;
  }

  protected virtual void APAdjust_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    APAdjust row = (APAdjust) e.Row;
    if (row == null || this.InternalCall)
      return;
    bool? nullable = row.Released;
    bool flag1 = false;
    bool flag2 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue;
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdDocType>(cache, (object) row, row.AdjdRefNbr == null);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdRefNbr>(cache, (object) row, row.AdjdRefNbr == null);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdLineNbr>(cache, (object) row, !row.AdjdLineNbr.HasValue);
    PXCache cache1 = cache;
    APAdjust data1 = row;
    int num1;
    if (flag2)
    {
      nullable = row.Voided;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgAmt>(cache1, (object) data1, num1 != 0);
    PXCache cache2 = cache;
    APAdjust data2 = row;
    int num2;
    if (flag2)
    {
      nullable = row.Voided;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgPPDAmt>(cache2, (object) data2, num2 != 0);
    PXCache cache3 = cache;
    APAdjust data3 = row;
    int num3;
    if (flag2)
    {
      nullable = row.Voided;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<APAdjust.curyAdjgWhTaxAmt>(cache3, (object) data3, num3 != 0);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjBatchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<APAdjust.adjBatchNbr>(cache, (object) row, !flag2);
    PXUIFieldAttribute.SetEnabled<APAdjust.adjdCuryRate>(cache, (object) row, this.GetExtension<APPaymentEntry.MultiCurrency>().EnableCrossRate(row));
    int? adjdLineNbr = row.AdjdLineNbr;
    int num4 = 0;
    if (!(adjdLineNbr.GetValueOrDefault() == num4 & adjdLineNbr.HasValue))
      return;
    APRegister paymentsByLineAllowed = this.GetAdjdInvoiceToVerifyArePaymentsByLineAllowed(row);
    if (paymentsByLineAllowed == null)
      return;
    nullable = paymentsByLineAllowed.Released;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = paymentsByLineAllowed.PaymentsByLinesAllowed;
    if (!nullable.GetValueOrDefault())
      return;
    this.Adjustments.Cache.RaiseExceptionHandling<APAdjust.adjgRefNbr>((object) row, (object) row.AdjgRefNbr, (Exception) new PXSetPropertyException("The application cannot be released because it is not distributed between document lines. Delete the application, and apply the prepayment to the document lines.", PXErrorLevel.RowWarning));
  }

  private APRegister GetAdjdInvoiceToVerifyArePaymentsByLineAllowed(APAdjust adj)
  {
    PXResultset<APInvoice> pxResultset;
    return this.balanceCache != null && this.balanceCache.TryGetValue(adj, out pxResultset) ? (APRegister) PXResult.Unwrap<APInvoice>((object) pxResultset[0]) : (APRegister) this.Caches[typeof (APInvoice)].Cached.Cast<APInvoice>().FirstOrDefault<APInvoice>((Func<APInvoice, bool>) (_ => _.DocType == adj.AdjdDocType && _.RefNbr == adj.AdjdRefNbr)) ?? (APRegister) PXSelectorAttribute.Select<APAdjust.adjdRefNbr>(this.Adjustments.Cache, (object) adj);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<APAdjust2> e)
  {
    if (e.Row == null)
      return;
    APAdjust row = (APAdjust) e.Row;
    if (row.PPDVATAdjRefNbr == null)
      return;
    row.PPDVATAdjDescription = $"{APDocType.GetDisplayName(row.PPDVATAdjDocType)}, {row.PPDVATAdjRefNbr}";
  }

  public virtual void APAdjust_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (((APAdjust) e.Row).Voided.GetValueOrDefault() && !this._IsVoidCheckInProgress && !this.IsReverseProc)
      throw new PXSetPropertyException("The record cannot be updated.");
  }

  protected virtual void APAdjust_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    APAdjust row = (APAdjust) e.Row;
    foreach (PXResult<APInvoice> pxResult in this.APInvoice_VendorID_DocType_RefNbr.Select((object) row.AdjdLineNbr, (object) row.VendorID, (object) row.AdjdDocType, (object) row.AdjdRefNbr))
    {
      APInvoice invoice = (APInvoice) pxResult;
      PaymentEntry.WarnPPDiscount<APInvoice, APAdjust>((PXGraph) this, row.AdjgDocDate, invoice, row, row.CuryAdjgPPDAmt);
    }
  }

  protected virtual void CurrencyInfo_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<PX.Objects.CM.Extensions.CurrencyInfo.curyID, PX.Objects.CM.Extensions.CurrencyInfo.curyRate, PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv>(e.Row, e.OldRow) || (e.Row is PX.Objects.CM.Extensions.CurrencyInfo row ? (!row.CuryRate.HasValue ? 1 : 0) : 1) != 0)
      return;
    Decimal? nullable = row.CuryRate;
    Decimal num1 = 0.0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    foreach (PXResult<APAdjust> pxResult in PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjgCuryInfoID, Equal<Required<APAdjust.adjgCuryInfoID>>>>.Config>.Select(sender.Graph, (object) ((PX.Objects.CM.Extensions.CurrencyInfo) e.Row).CuryInfoID))
    {
      APAdjust apAdjust = (APAdjust) pxResult;
      this.Adjustments.Cache.MarkUpdated((object) apAdjust);
      this.GetExtension<APPaymentEntry.APPaymentEntryDocumentExtension>().CalcBalancesFromAdjustedDocument(apAdjust, true, !this.TakeDiscAlways);
      nullable = apAdjust.CuryDocBal;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
        this.Adjustments.Cache.RaiseExceptionHandling<APAdjust.curyAdjgAmt>((object) apAdjust, (object) apAdjust.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      nullable = apAdjust.CuryDiscBal;
      Decimal num3 = 0M;
      if (nullable.GetValueOrDefault() < num3 & nullable.HasValue)
        this.Adjustments.Cache.RaiseExceptionHandling<APAdjust.curyAdjgPPDAmt>((object) apAdjust, (object) apAdjust.CuryAdjgPPDAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      nullable = apAdjust.CuryWhTaxBal;
      Decimal num4 = 0M;
      if (nullable.GetValueOrDefault() < num4 & nullable.HasValue)
        this.Adjustments.Cache.RaiseExceptionHandling<APAdjust.curyAdjgWhTaxAmt>((object) apAdjust, (object) apAdjust.CuryAdjgWhTaxAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    }
  }

  protected virtual void APPayment_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    foreach (APAdjust apAdjust in this.Adjustments.Cache.Inserted)
      this.Adjustments.Delete(apAdjust);
  }

  protected virtual void APPayment_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APPayment row = (APPayment) e.Row;
    if (!row.Released.GetValueOrDefault() && !row.CashAccountID.HasValue)
    {
      if (sender.RaiseExceptionHandling<APPayment.cashAccountID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[cashAccountID]"
      })))
        throw new PXRowPersistingException(typeof (APPayment.cashAccountID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "cashAccountID"
        });
    }
    bool? nullable1 = row.Released;
    if (!nullable1.GetValueOrDefault() && string.IsNullOrEmpty(row.PaymentMethodID))
    {
      if (sender.RaiseExceptionHandling<APPayment.paymentMethodID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[paymentMethodID]"
      })))
        throw new PXRowPersistingException(typeof (APPayment.paymentMethodID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "paymentMethodID"
        });
    }
    Decimal? nullable2;
    if (row.DocType == "CHK" || row.DocType == "REF" || row.DocType == "PPM")
    {
      nullable2 = row.CuryOrigDocAmt;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() < num & nullable2.HasValue)
        throw new PXRowPersistingException(typeof (APPayment.curyOrigDocAmt).Name, (object) row.CuryOrigDocAmt, "Documents of the {0} type with negative amounts cannot be released.", new object[1]
        {
          (object) GetLabel.For<APDocType>(row.DocType)
        });
    }
    nullable1 = row.OpenDoc;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = row.Hold;
      if (!nullable1.GetValueOrDefault() && this.IsPaymentUnbalanced(row))
      {
        nullable1 = this.APSetup.Current.SuggestPaymentAmount;
        if (nullable1 ?? false)
        {
          nullable2 = row.CuryOrigDocAmt;
          Decimal num = 0M;
          if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
            goto label_14;
        }
        throw new PXRowPersistingException(typeof (APPayment.curyOrigDocAmt).Name, (object) row.CuryOrigDocAmt, "The document is out of the balance.");
      }
    }
label_14:
    PaymentRefAttribute.SetUpdateCashManager<APPayment.extRefNbr>(sender, e.Row, row.DocType != "VCK" && row.DocType != "REF" && row.DocType != "VRF");
    nullable1 = row.OpenDoc;
    string errMsg;
    if (nullable1.GetValueOrDefault() && !this.VerifyAdjFinPeriodID(row, row.AdjFinPeriodID, out errMsg) && sender.RaiseExceptionHandling<APPayment.adjFinPeriodID>(e.Row, (object) PeriodIDAttribute.FormatForDisplay(row.AdjFinPeriodID), (Exception) new PXSetPropertyException(errMsg)))
      throw new PXRowPersistingException(typeof (APPayment.adjFinPeriodID).Name, (object) PeriodIDAttribute.FormatForError(row.AdjFinPeriodID), errMsg);
    nullable1 = this.APSetup.Current.SuggestPaymentAmount;
    if (!(nullable1 ?? false))
      return;
    nullable1 = row.Released;
    if (nullable1.GetValueOrDefault())
      return;
    nullable2 = row.CuryUnappliedBal;
    if (!nullable2.HasValue)
      return;
    nullable2 = row.CuryUnappliedBal;
    if (!(nullable2.Value < 0M))
      return;
    nullable2 = row.CuryOrigDocAmt;
    Decimal num1 = 0M;
    if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
      return;
    row.CuryOrigDocAmt = row.CuryApplAmt;
    row.CuryUnappliedBal = new Decimal?(0M);
    this.Document.Cache.RaiseFieldUpdated<APPayment.curyOrigDocAmt>((object) row, (object) null);
    object curyOrigDocAmt = (object) row.CuryOrigDocAmt;
    this.Document.Cache.RaiseFieldVerifying<APPayment.curyOrigDocAmt>((object) row, ref curyOrigDocAmt);
  }

  protected virtual void APPayment_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    APPayment row = (APPayment) e.Row;
    if (e.Operation != PXDBOperation.Insert || e.TranStatus != PXTranStatus.Open || !(row.DocType == "CHK") && !(row.DocType == "PPM"))
      return;
    string str = row.DocType == "CHK" ? "PPM" : "CHK";
    APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, (object) str, (object) row.RefNbr);
    APInvoice apInvoice = (APInvoice) null;
    if (str == "PPM")
      apInvoice = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<APDocType.prepayment>, And<APInvoice.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, (object) row.RefNbr);
    if (apPayment == null && apInvoice == null)
      return;
    Numbering numbering = (Numbering) PXSelectorAttribute.Select<PX.Objects.AP.APSetup.checkNumberingID>(this.Caches[typeof (PX.Objects.AP.APSetup)], this.Caches[typeof (PX.Objects.AP.APSetup)].Current);
    if ((numbering != null ? (numbering.UserNumbering.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXRowPersistedException(typeof (APPayment.refNbr).Name, (object) row.RefNbr, "{0} with reference number {1} already exists. Enter another reference number.", new object[2]
      {
        str == "CHK" ? (object) "Payment" : (object) "Prepayment",
        (object) row.RefNbr
      });
    throw new PXLockViolationException(typeof (APPayment), PXDBOperation.Insert, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    });
  }

  protected virtual bool PaymentRefMustBeUnique
  {
    get => PaymentRefAttribute.PaymentRefMustBeUnique(this.paymenttype.Current);
  }

  /// <summary>
  /// Determines whether the approval is required for the document.
  /// </summary>
  /// <param name="doc">The document for which the check should be performed.</param>
  /// <returns>Returns <c>true</c> if approval is required; otherwise, returns <c>false</c>.</returns>
  public bool IsApprovalRequired(APPayment doc)
  {
    return EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>.ApprovableDocTypes.Contains(doc.DocType);
  }

  protected virtual void APPayment_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APPayment row1))
      return;
    this.release.SetEnabled(true);
    this.Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)].AllowUpdate = true;
    this.Caches[typeof (APTranPostBal)].AllowInsert = this.Caches[typeof (APTranPostBal)].AllowUpdate = this.Caches[typeof (APTranPostBal)].AllowDelete = false;
    this.Caches[typeof (APAdjust2)].AllowInsert = this.Caches[typeof (APAdjust2)].AllowUpdate = this.Caches[typeof (APAdjust2)].AllowDelete = false;
    bool flag1 = !this.IsApprovalRequired(row1);
    bool? nullable1 = row1.DontApprove;
    bool flag2 = flag1;
    if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
      cache.SetValueExt<APRegister.dontApprove>((object) row1, (object) flag1);
    if (this.InternalCall)
      return;
    this.Adjustments.Cache.AllowSelect = this.APPost.Cache.AllowSelect = this.PaymentCharges.AllowSelect = true;
    int? nullable2;
    if (this.vendor.Current != null)
    {
      int? vendorId = row1.VendorID;
      nullable2 = this.vendor.Current.BAccountID;
      if (!(vendorId.GetValueOrDefault() == nullable2.GetValueOrDefault() & vendorId.HasValue == nullable2.HasValue))
        this.vendor.Current = (Vendor) null;
    }
    if (this.finperiod.Current != null && !object.Equals((object) this.finperiod.Current.MasterFinPeriodID, (object) row1.AdjTranPeriodID))
      this.finperiod.Current = (OrganizationFinPeriod) null;
    bool isVisible1 = row1.DocType != "ADR";
    PXUIFieldAttribute.SetVisible<APPayment.curyID>(cache, (object) row1, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetVisible<APPayment.cashAccountID>(cache, (object) row1, isVisible1);
    PXUIFieldAttribute.SetVisible<APPayment.cleared>(cache, (object) row1, isVisible1);
    PXUIFieldAttribute.SetVisible<APPayment.clearDate>(cache, (object) row1, isVisible1);
    PXUIFieldAttribute.SetVisible<APPayment.paymentMethodID>(cache, (object) row1, isVisible1);
    PXUIFieldAttribute.SetVisible<APPayment.extRefNbr>(cache, (object) row1, isVisible1);
    nullable1 = row1.Released;
    bool valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = row1.Hold;
    bool valueOrDefault2 = nullable1.GetValueOrDefault();
    nullable1 = row1.OpenDoc;
    bool valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = row1.VoidAppl;
    bool valueOrDefault4 = nullable1.GetValueOrDefault();
    bool flag3 = this.IsDocReallyPrinted(row1);
    int num1;
    if (valueOrDefault2 && this.cashaccount.Current != null)
    {
      nullable1 = this.cashaccount.Current.Reconcile;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool isEnabled1 = num1 != 0;
    bool flag4 = false;
    PXUIFieldAttribute.SetRequired<APPayment.cashAccountID>(cache, !valueOrDefault1);
    PXUIFieldAttribute.SetRequired<APPayment.paymentMethodID>(cache, !valueOrDefault1);
    PXUIFieldAttribute.SetRequired<APPayment.extRefNbr>(cache, !valueOrDefault1 && this.PaymentRefMustBeUnique);
    PaymentRefAttribute.SetUpdateCashManager<APPayment.extRefNbr>(cache, e.Row, row1.DocType != "VCK" && row1.DocType != "REF" && row1.DocType != "VRF");
    bool flag5 = row1.DocType == "REF";
    PXCache cache1 = cache;
    APPayment data1 = row1;
    int num2;
    if (flag5)
    {
      nullable1 = row1.DepositAsBatch;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetVisible<APPayment.depositAfter>(cache1, (object) data1, num2 != 0);
    PXUIFieldAttribute.SetEnabled<APPayment.depositAfter>(cache, (object) row1, false);
    PXCache cache2 = cache;
    int num3;
    if (flag5)
    {
      nullable1 = row1.DepositAsBatch;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetRequired<APPayment.depositAfter>(cache2, num3 != 0);
    PXCache cache3 = cache;
    APPayment data2 = row1;
    int check;
    if (flag5)
    {
      nullable1 = row1.DepositAsBatch;
      if (nullable1.GetValueOrDefault())
      {
        check = 1;
        goto label_23;
      }
    }
    check = 2;
label_23:
    PXDefaultAttribute.SetPersistingCheck<APPayment.depositAfter>(cache3, (object) data2, (PXPersistingCheck) check);
    this.validateAddresses.SetEnabled(false);
    System.DateTime? nullable3;
    if (cache.GetStatus((object) row1) == PXEntryStatus.Notchanged && (row1.Status == "N" || row1.Status == "U") && !valueOrDefault4)
    {
      nullable3 = row1.AdjDate;
      if (nullable3.HasValue)
      {
        nullable3 = row1.AdjDate;
        System.DateTime dateTime1 = nullable3.Value;
        ref System.DateTime local = ref dateTime1;
        nullable3 = this.Accessinfo.BusinessDate;
        System.DateTime dateTime2 = nullable3.Value;
        if (local.CompareTo(dateTime2) < 0)
        {
          if (!this.Adjustments_Raw.View.SelectMultiBound(new object[1]
          {
            e.Row
          }).Any<object>())
          {
            row1.AdjDate = this.Accessinfo.BusinessDate;
            FinPeriod finPeriodByDate = this.FinPeriodRepository.FindFinPeriodByDate(row1.AdjDate, PXAccess.GetParentOrganizationID(row1.BranchID));
            row1.AdjFinPeriodID = finPeriodByDate?.FinPeriodID;
            row1.AdjTranPeriodID = finPeriodByDate?.MasterFinPeriodID;
            cache.SetStatus((object) row1, PXEntryStatus.Held);
          }
        }
      }
    }
    bool flag6 = false;
    int num4 = AutoNumberAttribute.IsViewOnlyRecord<APPayment.refNbr>(cache, (object) row1) ? 1 : 0;
    bool flag7 = !string.IsNullOrEmpty(row1.DepositNbr) && !string.IsNullOrEmpty(row1.DepositType);
    PX.Objects.CA.CashAccount current1 = this.cashaccount.Current;
    int num5;
    if (current1 != null)
    {
      nullable2 = current1.CashAccountID;
      int? cashAccountId = row1.CashAccountID;
      if (nullable2.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable2.HasValue == cashAccountId.HasValue)
      {
        nullable1 = current1.ClearingAccount;
        num5 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_32;
      }
    }
    num5 = 0;
label_32:
    bool flag8 = num5 != 0;
    int num6;
    if (!flag7 && current1 != null)
    {
      if (!flag8)
      {
        nullable1 = row1.DepositAsBatch;
        bool flag9 = flag8;
        num6 = !(nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue) ? 1 : 0;
      }
      else
        num6 = 1;
    }
    else
      num6 = 0;
    bool isEnabled2 = num6 != 0;
    bool flag10 = row1.DocType == "VCK";
    bool flag11 = this.paymenttype?.Current?.PaymentType == "EPP";
    bool flag12 = flag11 && !string.IsNullOrEmpty(row1.ExternalPaymentStatus);
    if (num4 != 0)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      cache.AllowUpdate = false;
      cache.AllowDelete = false;
      this.Adjustments.Cache.SetAllEditPermissions(false);
    }
    else if (valueOrDefault4 && !valueOrDefault1)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APPayment.adjDate>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APPayment.adjFinPeriodID>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APPayment.docDesc>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APPayment.hold>(cache, (object) row1, true);
      cache.AllowUpdate = true;
      cache.AllowDelete = true;
      this.Adjustments.Cache.SetAllEditPermissions(false);
    }
    else if (valueOrDefault1 & valueOrDefault3)
    {
      PXResultset<APAdjust> resultSet = this.Adjustments_Raw.Select();
      int count = resultSet.Count;
      flag4 = resultSet.RowCast<APAdjust>().TakeWhile<APAdjust>((Func<APAdjust, bool>) (adj => !adj.Voided.GetValueOrDefault())).Any<APAdjust>((Func<APAdjust, bool>) (adj => adj.Hold.GetValueOrDefault()));
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APPayment.adjDate>(cache, (object) row1, !flag4);
      PXUIFieldAttribute.SetEnabled<APPayment.adjFinPeriodID>(cache, (object) row1, !flag4);
      PXUIFieldAttribute.SetEnabled<APPayment.hold>(cache, (object) row1, !flag4);
      cache.AllowDelete = false;
      cache.AllowUpdate = !flag4 | isEnabled2;
      PXCache cache4 = this.Adjustments.Cache;
      int num7;
      if (!flag4)
      {
        nullable1 = row1.PaymentsByLinesAllowed;
        num7 = !nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num7 = 0;
      cache4.SetAllEditPermissions(num7 != 0);
      if (resultSet.RowCast<APAdjust>().TakeWhile<APAdjust>((Func<APAdjust, bool>) (adj => !adj.Voided.GetValueOrDefault())).Any<APAdjust>((Func<APAdjust, bool>) (adj =>
      {
        if (!adj.Hold.GetValueOrDefault())
          return false;
        Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
        Decimal num8 = 0M;
        return curyAdjgAmt.GetValueOrDefault() <= num8 & curyAdjgAmt.HasValue;
      })))
        this.Adjustments.Cache.AllowDelete = true;
      PXAction<APPayment> release = this.release;
      nullable1 = row1.Hold;
      bool flag13 = false;
      int num9 = !(nullable1.GetValueOrDefault() == flag13 & nullable1.HasValue) || flag4 ? 0 : (count != 0 ? 1 : 0);
      release.SetEnabled(num9 != 0);
    }
    else if (((!(valueOrDefault1 | flag12) ? 0 : (!valueOrDefault3 ? 1 : 0)) | (flag3 ? 1 : 0)) != 0)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APPayment.docDesc>(cache, (object) row1, !valueOrDefault1);
      PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, (object) row1, !valueOrDefault1 && row1.DocType != "VRF");
      cache.AllowUpdate = !(valueOrDefault1 | flag12) | isEnabled2;
      cache.AllowDelete = !(valueOrDefault1 | flag12) && !flag3;
      this.Adjustments.Cache.SetAllEditPermissions(false);
      this.Remittance_Address.Cache.SetAllEditPermissions(false);
    }
    else if (valueOrDefault4)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      cache.AllowDelete = false;
      cache.AllowUpdate = false;
      this.Adjustments.Cache.SetAllEditPermissions(false);
    }
    else
    {
      CATran caTran = (CATran) PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Config>.Select((PXGraph) this, (object) row1.CATranID);
      flag6 = caTran != null && caTran.RefTranID.HasValue;
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APPayment.status>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APPayment.curyID>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APPayment.printCheck>(cache, (object) row1, !flag3 && APPaymentEntry.IsPrintableDocType(row1.DocType));
      bool flag14 = this.MustPrintCheck(row1);
      PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, (object) row1, !flag14 && !flag6 && row1.DocType != "VRF");
      PXUIFieldAttribute.SetEnabled<APPayment.docDate>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APPayment.finPeriodID>(cache, (object) row1, false);
      cache.AllowDelete = true;
      cache.AllowUpdate = true;
      this.Adjustments.Cache.SetAllEditPermissions(true);
      PXUIFieldAttribute.SetEnabled<APPayment.curyOrigDocAmt>(cache, (object) row1, !flag6);
      PXUIFieldAttribute.SetEnabled<APPayment.vendorID>(cache, (object) row1, !flag6);
    }
    if (valueOrDefault1 || row1.Status == "P")
    {
      this.Remittance_Address.Cache.AllowUpdate = false;
      this.Remittance_Contact.Cache.AllowUpdate = false;
    }
    this.validateAddresses.SetEnabled(!valueOrDefault1 && this.FindAllImplementations<IAddressValidationHelper>().RequiresValidation());
    PXUIFieldAttribute.SetEnabled<APPayment.cashAccountID>(cache, (object) row1, !valueOrDefault1 && !flag3 && !valueOrDefault4 && !flag6);
    PXUIFieldAttribute.SetEnabled<APPayment.paymentMethodID>(cache, (object) row1, !valueOrDefault1 && !flag3 && !valueOrDefault4 && !flag6);
    PXUIFieldAttribute.SetEnabled<APPayment.cleared>(cache, (object) row1, isEnabled1);
    PXCache cache5 = cache;
    APPayment data3 = row1;
    int num10;
    if (isEnabled1)
    {
      nullable1 = row1.Cleared;
      num10 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num10 = 0;
    PXUIFieldAttribute.SetEnabled<APPayment.clearDate>(cache5, (object) data3, num10 != 0);
    PXUIFieldAttribute.SetEnabled<APPayment.docType>(cache, (object) row1);
    PXUIFieldAttribute.SetEnabled<APPayment.refNbr>(cache, (object) row1);
    PXUIFieldAttribute.SetEnabled<APPayment.curyUnappliedBal>(cache, (object) row1, false);
    PXUIFieldAttribute.SetEnabled<APPayment.curyApplAmt>(cache, (object) row1, false);
    PXUIFieldAttribute.SetEnabled<APPayment.batchNbr>(cache, (object) row1, false);
    PXAction<APPayment> voidCheck = this.voidCheck;
    int num11;
    if (valueOrDefault1)
    {
      nullable1 = row1.Voided;
      if (!nullable1.GetValueOrDefault() && APPaymentType.VoidEnabled(row1.DocType))
      {
        num11 = !flag4 ? 1 : 0;
        goto label_62;
      }
    }
    num11 = 0;
label_62:
    voidCheck.SetEnabled(num11 != 0);
    PXAction<APPayment> loadInvoices = this.loadInvoices;
    int? vendorId1 = row1.VendorID;
    int num12 = !(vendorId1.HasValue & valueOrDefault3) || flag4 || !(row1.DocType == "CHK") && !(row1.DocType == "PPM") && !(row1.DocType == "REF") ? 0 : (!flag3 ? 1 : 0);
    loadInvoices.SetEnabled(num12 != 0);
    this.SetDocTypeList(e.Row);
    this.editVendor.SetEnabled(this.vendor?.Current != null);
    vendorId1 = row1.VendorID;
    if (vendorId1.HasValue && this.Adjustments_Raw.Select().Any<PXResult<APAdjust>>())
      PXUIFieldAttribute.SetEnabled<APPayment.vendorID>(cache, (object) row1, false);
    if (e.Row != null && !((APPayment) e.Row).CuryApplAmt.HasValue)
    {
      bool IsReadOnly = cache.GetStatus(e.Row) == PXEntryStatus.Notchanged;
      PXFormulaAttribute.CalcAggregate<APAdjust.curyAdjgAmt>(this.Adjustments.Cache, e.Row, IsReadOnly);
      cache.RaiseFieldUpdated<APPayment.curyApplAmt>(e.Row, (object) null);
    }
    PXUIFieldAttribute.SetEnabled<APPayment.depositDate>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APPayment.deposited>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APPayment.depositNbr>(cache, (object) null, false);
    if (isEnabled2)
    {
      PXCache pxCache = cache;
      APPayment row2 = row1;
      // ISSUE: variable of a boxed type
      __Boxed<bool?> depositAsBatch = (ValueType) row1.DepositAsBatch;
      nullable1 = row1.DepositAsBatch;
      bool flag15 = flag8;
      PXSetPropertyException propertyException = !(nullable1.GetValueOrDefault() == flag15 & nullable1.HasValue) ? new PXSetPropertyException("'Batch deposit' setting does not match 'Clearing Account' flag of the Cash Account", PXErrorLevel.Warning) : (PXSetPropertyException) null;
      pxCache.RaiseExceptionHandling<APPayment.depositAsBatch>((object) row2, (object) depositAsBatch, (Exception) propertyException);
    }
    this.PaymentCharges.Cache.SetAllEditPermissions(!valueOrDefault1 && (row1.DocType == "CHK" || row1.DocType == "VCK"));
    PXUIFieldAttribute.SetEnabled<APPayment.depositAsBatch>(cache, (object) row1, isEnabled2);
    PXCache cache6 = cache;
    APPayment data4 = row1;
    int num13;
    if (!flag7 & flag8)
    {
      nullable1 = row1.DepositAsBatch;
      num13 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num13 = 0;
    PXUIFieldAttribute.SetEnabled<APPayment.depositAfter>(cache6, (object) data4, num13 != 0);
    bool flag16 = APPaymentEntry.IsPrintableDocType(row1.DocType) || this.paymenttype.Current == null;
    PXCache cache7 = cache;
    APPayment data5 = row1;
    PX.Objects.CA.PaymentMethod current2 = this.paymenttype.Current;
    int num14;
    if (current2 == null)
    {
      num14 = 0;
    }
    else
    {
      nullable1 = current2.PrintOrExport;
      num14 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num15 = flag16 ? 1 : 0;
    int num16 = num14 & num15;
    PXUIFieldAttribute.SetVisible<APPayment.printCheck>(cache7, (object) data5, num16 != 0);
    PXAction<APPayment> reverseApplication = this.reverseApplication;
    int num17;
    if (row1.DocType != "VCK" && row1.DocType != "VRF")
    {
      nullable1 = row1.Voided;
      num17 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num17 = 0;
    reverseApplication.SetEnabled(num17 != 0);
    nullable1 = row1.IsMigratedRecord;
    bool valueOrDefault5 = nullable1.GetValueOrDefault();
    bool isVisible2 = valueOrDefault5 && !valueOrDefault1;
    int num18;
    if (valueOrDefault5)
    {
      nullable1 = row1.Released;
      num18 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num18 = 0;
    bool isVisible3 = num18 != 0;
    bool isEnabled3 = isVisible2 && row1.DocType == "PPM";
    PXUIFieldAttribute.SetVisible<APPayment.curyUnappliedBal>(cache, (object) row1, !isVisible2);
    PXUIFieldAttribute.SetVisible<APPayment.curyInitDocBal>(cache, (object) row1, isVisible2);
    PXUIFieldAttribute.SetVisible<APRegister.displayCuryInitDocBal>(cache, (object) row1, isVisible3);
    PXUIFieldAttribute.SetEnabled<APPayment.curyInitDocBal>(cache, (object) row1, isEnabled3);
    if (valueOrDefault5)
      PXUIFieldAttribute.SetEnabled<APPayment.printCheck>(cache, (object) row1, false);
    if (isVisible2)
      this.Adjustments.Cache.AllowSelect = this.APPost.Cache.AllowSelect = this.PaymentCharges.AllowSelect = false;
    PX.Objects.AP.APSetup current3 = this.APSetup.Current;
    int num19;
    if (current3 == null)
    {
      num19 = 0;
    }
    else
    {
      nullable1 = current3.MigrationMode;
      num19 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if ((num19 != 0 ? (!valueOrDefault5 ? 1 : 0) : (isVisible2 ? 1 : 0)) != 0)
    {
      bool allowInsert = this.Document.Cache.AllowInsert;
      bool allowDelete = this.Document.Cache.AllowDelete;
      this.DisableCaches();
      this.Document.Cache.AllowInsert = allowInsert;
      this.Document.Cache.AllowDelete = allowDelete;
    }
    if (isEnabled3)
    {
      if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<APPayment.curyInitDocBal>(cache, (object) row1)))
        cache.RaiseExceptionHandling<APPayment.curyInitDocBal>((object) row1, (object) row1.CuryInitDocBal, (Exception) new PXSetPropertyException("Enter the document open balance to this box.", PXErrorLevel.Warning));
    }
    else
      cache.RaiseExceptionHandling<APPayment.curyInitDocBal>((object) row1, (object) row1.CuryInitDocBal, (Exception) null);
    if (valueOrDefault5)
    {
      cache.SetValue<APPayment.printCheck>((object) row1, (object) false);
      PXUIFieldAttribute.SetEnabled<APPayment.printCheck>(cache, (object) row1, false);
    }
    this.CheckForUnreleasedIncomingApplications(cache, row1);
    if (this.IsApprovalRequired(row1))
    {
      if (row1.DocType == "CHK" || row1.DocType == "PPM")
      {
        if (!(row1.Status == "E") && !(row1.Status == "R") && !(row1.Status == "C") && !(row1.Status == "P") && !(row1.Status == "V"))
        {
          if (row1.Status == "G")
          {
            nullable1 = row1.DontApprove;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = row1.Approved;
              if (nullable1.GetValueOrDefault())
                goto label_104;
            }
          }
          if (row1.Status == "B")
          {
            nullable1 = row1.DontApprove;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = row1.Approved;
              if (!nullable1.GetValueOrDefault())
                goto label_105;
            }
            else
              goto label_105;
          }
          else
            goto label_105;
        }
label_104:
        PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
        this.Adjustments.Cache.AllowInsert = false;
        this.Approval.Cache.AllowInsert = false;
        this.PaymentCharges.Cache.AllowInsert = false;
        this.Adjustments.Cache.AllowUpdate = false;
        this.Approval.Cache.AllowUpdate = false;
        this.PaymentCharges.Cache.AllowUpdate = false;
label_105:
        if (row1.Status == "E" || row1.Status == "R" || row1.Status == "B" || row1.Status == "G" || row1.Status == "H")
          PXUIFieldAttribute.SetEnabled<APPayment.hold>(cache, (object) row1, true);
        nullable1 = row1.Approved;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = row1.Released;
          if (!nullable1.GetValueOrDefault())
            PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, (object) row1, true);
        }
      }
      PXUIFieldAttribute.SetEnabled<APPayment.docType>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APPayment.refNbr>(cache, (object) row1, true);
    }
    if (row1.DocType == "ADR")
    {
      nullable1 = row1.PaymentsByLinesAllowed;
      if (nullable1.GetValueOrDefault())
        cache.RaiseExceptionHandling<APPayment.refNbr>((object) row1, (object) row1.RefNbr, (Exception) new PXSetPropertyException("The {0} debit adjustment is paid by line and cannot be applied to documents directly. To apply the debit adjustment, on the Checks and Payments (AP302000) form, create a check and apply the debit adjustment lines and the needed bill to it.", PXErrorLevel.Warning, new object[1]
        {
          (object) row1.RefNbr
        }));
    }
    if (row1.Status == "P" && row1.DocType == "CHK" && row1.DocType != "VRF")
    {
      PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APPayment.docDesc>(cache, (object) row1, true);
    }
    if (row1.DocType == "VCK")
      PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(cache, (object) row1, false);
    if (valueOrDefault1)
    {
      if (!valueOrDefault2)
      {
        nullable1 = (bool?) cache.GetValueOriginal<APPayment.hold>((object) row1);
        if (!nullable1.GetValueOrDefault())
          goto label_122;
      }
      PXUIFieldAttribute.SetEnabled<APPayment.hold>(cache, (object) row1, true);
      cache.AllowUpdate = true;
    }
label_122:
    if (row1.DocType == "PPI")
    {
      nullable1 = row1.PendingPayment;
      if (nullable1.GetValueOrDefault())
        this.Adjustments.Cache.AllowInsert = false;
    }
    nullable1 = row1.PrintCheck;
    bool valueOrDefault6 = nullable1.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<APAdjust.stubNbr>(this.Adjustments.Cache, (object) null, valueOrDefault6 && row1.Status == "P");
    PXUIFieldAttribute.SetVisible<APAdjust2.stubNbr>(this.Caches[typeof (APAdjust2)], (object) null, valueOrDefault6 & valueOrDefault1);
    bool flag17 = row1.ExternalPaymentDisbursementType == ((DisbursementType) 0).ToString();
    string disbursementType1 = row1.ExternalPaymentDisbursementType;
    DisbursementType disbursementType2 = (DisbursementType) 9;
    string str1 = disbursementType2.ToString();
    int num20;
    if (!(disbursementType1 == str1))
    {
      string disbursementType3 = row1.ExternalPaymentDisbursementType;
      disbursementType2 = (DisbursementType) 7;
      string str2 = disbursementType2.ToString();
      num20 = disbursementType3 == str2 ? 1 : 0;
    }
    else
      num20 = 1;
    bool flag18 = num20 != 0;
    string disbursementType4 = row1.ExternalPaymentDisbursementType;
    disbursementType2 = (DisbursementType) 2;
    string str3 = disbursementType2.ToString();
    bool flag19 = disbursementType4 == str3;
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentStatus>(cache, (object) row1, flag11 && !flag10);
    PXCache cache8 = cache;
    APPayment data6 = row1;
    int num21;
    if (flag12)
    {
      nullable3 = row1.ExternalPaymentUpdateTime;
      num21 = nullable3.HasValue ? 1 : 0;
    }
    else
      num21 = 0;
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentUpdateTime>(cache8, (object) data6, num21 != 0);
    PXCache cache9 = cache;
    APPayment data7 = row1;
    int num22;
    if (flag12)
    {
      nullable3 = row1.ExternalPaymentSentDate;
      num22 = nullable3.HasValue ? 1 : 0;
    }
    else
      num22 = 0;
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentSentDate>(cache9, (object) data7, num22 != 0);
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentDisbursementType>(cache, (object) row1, !string.IsNullOrEmpty(row1.ExternalPaymentDisbursementType) && !flag10);
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentCheckNbr>(cache, (object) row1, flag12 & flag17 && !string.IsNullOrEmpty(row1.ExternalPaymentCheckNbr));
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentTraceNbr>(cache, (object) row1, flag12 & flag19 && !string.IsNullOrEmpty(row1.ExternalPaymentTraceNbr));
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentBatchNbr>(cache, (object) row1, flag12 & flag19 && !string.IsNullOrEmpty(row1.ExternalPaymentBatchNbr));
    PXUIFieldAttribute.SetVisible<APPayment.externalPaymentCardNbr>(cache, (object) row1, flag12 & flag18 && !string.IsNullOrEmpty(row1.ExternalPaymentCardNbr));
  }

  private static bool IsPrintableDocType(string docType)
  {
    return docType != "VCK" && docType != "REF" && docType != "VRF" && docType != "ADR";
  }

  protected virtual void CheckForUnreleasedIncomingApplications(PXCache sender, APPayment document)
  {
    if (!document.Released.GetValueOrDefault() || !document.OpenDoc.GetValueOrDefault())
      return;
    APAdjust apAdjust = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.released, NotEqual<PX.Data.True>>>>>.Config>.Select((PXGraph) this, (object) document.DocType, (object) document.RefNbr);
    sender.ClearFieldErrors<APPayment.refNbr>((object) document);
    if (apAdjust == null)
      return;
    this.Adjustments.Cache.SetAllEditPermissions(false);
    if (apAdjust.AdjgDocType == "VCK")
    {
      sender.DisplayFieldWarning<APPayment.refNbr>((object) document, (object) null, PXLocalizer.LocalizeFormat("An unreleased voided payment ({0}) is applied to the prepayment. To create new applications for the prepayment, remove the voided payment.", (object) apAdjust.AdjgRefNbr));
      this.Adjustments.Cache.AllowDelete = true;
      this.loadInvoices.SetEnabled(false);
    }
    else
      sender.DisplayFieldWarning<APPayment.refNbr>((object) document, (object) null, PXLocalizer.LocalizeFormat("The document has an unreleased application from {0} {1}. To create new applications for the document, remove or release the unreleased application from {0} {1}.", (object) GetLabel.For<APDocType>(apAdjust.AdjgDocType), (object) apAdjust.AdjgRefNbr));
  }

  public virtual bool IsDocReallyPrinted(APPayment doc)
  {
    return APPaymentEntry.IsCheckReallyPrinted((IPrintCheckControlable) doc);
  }

  protected virtual void FillBalanceCache(APPayment row, bool released = false)
  {
    if (row == null || row.DocType == null || row.RefNbr == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetExtension<APPaymentEntry.MultiCurrency>().GetCurrencyInfo(row.CuryInfoID);
    this.GetExtension<APPaymentEntry.MultiCurrency>().StoreResult(currencyInfo);
    if (this.balanceCache == null)
      this.balanceCache = new Dictionary<APAdjust, PXResultset<APInvoice>>((IEqualityComparer<APAdjust>) new RecordKeyComparer<APAdjust>(this.Adjustments.Cache));
    if (this.balanceCache.Keys.Any<APAdjust>((Func<APAdjust, bool>) (_ =>
    {
      bool? released1 = _.Released;
      bool flag = released;
      return released1.GetValueOrDefault() == flag & released1.HasValue;
    })))
      return;
    foreach (PXResult<PX.Objects.AP.Standalone.APAdjust, APInvoice, APRegisterAlias, APTran, PX.Objects.CM.Extensions.CurrencyInfo, CurrencyInfo2> res in this.Adjustments_Balance.View.SelectMultiBound(new object[1]
    {
      (object) row
    }, (object) released))
    {
      PX.Objects.AP.Standalone.APAdjust apAdjust = (PX.Objects.AP.Standalone.APAdjust) res;
      this.AddBalanceCache(new APAdjust()
      {
        AdjdDocType = apAdjust.AdjdDocType,
        AdjdRefNbr = apAdjust.AdjdRefNbr,
        AdjdLineNbr = apAdjust.AdjdLineNbr,
        AdjgDocType = apAdjust.AdjgDocType,
        AdjgRefNbr = apAdjust.AdjgRefNbr,
        AdjNbr = apAdjust.AdjNbr
      }, (PXResult) res);
    }
  }

  protected virtual void AddBalanceCache(APAdjust adj, PXResult res)
  {
    if (this.balanceCache == null)
      this.balanceCache = new Dictionary<APAdjust, PXResultset<APInvoice>>((IEqualityComparer<APAdjust>) new RecordKeyComparer<APAdjust>(this.Adjustments.Cache));
    APInvoice apInvoice = PXResult.Unwrap<APInvoice>((object) res);
    APTran i1 = PXResult.Unwrap<APTran>((object) res);
    PX.Objects.CM.Extensions.CurrencyInfo info1 = PXResult.Unwrap<PX.Objects.CM.Extensions.CurrencyInfo>((object) res);
    PX.Objects.CM.Extensions.CurrencyInfo info2 = PX.Objects.Common.Utilities.Clone<CurrencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) this, PXResult.Unwrap<CurrencyInfo2>((object) res));
    PX.Objects.CM.Extensions.CurrencyInfo info3 = PX.Objects.Common.Utilities.Clone<CurrencyInfo3, PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) this, PXResult.Unwrap<CurrencyInfo3>((object) res));
    APRegisterAlias copy = PXResult.Unwrap<APRegisterAlias>((object) res);
    this.GetExtension<APPaymentEntry.MultiCurrency>().StoreResult(info1);
    this.GetExtension<APPaymentEntry.MultiCurrency>().StoreResult(info3);
    this.GetExtension<APPaymentEntry.MultiCurrency>().StoreResult(info2);
    if (copy != null)
      PXCache<APRegister>.RestoreCopy((APRegister) apInvoice, (APRegister) copy);
    PXSelectorAttribute.StoreResult<APAdjust.displayRefNbr>(this.Adjustments.Cache, (object) adj, (IBqlTable) apInvoice);
    Dictionary<APAdjust, PXResultset<APInvoice>> balanceCache = this.balanceCache;
    APAdjust key = adj;
    PXResultset<APInvoice, APTran> pxResultset = new PXResultset<APInvoice, APTran>();
    pxResultset.Add((PXResult<APInvoice>) new PXResult<APInvoice, APTran>(apInvoice, i1));
    balanceCache[key] = (PXResultset<APInvoice>) pxResultset;
  }

  public virtual void APPayment_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    APPayment row = (APPayment) e.Row;
    if (row == null || row.CuryApplAmt.HasValue)
      return;
    using (new PXConnectionScope())
    {
      this.FillBalanceCache(row);
      this.RecalcApplAmounts(sender, row, true);
    }
  }

  public virtual void RecalcApplAmounts(PXCache sender, APPayment row, bool aReadOnly)
  {
    PXFormulaAttribute.CalcAggregate<APAdjust.curyAdjgAmt>(this.Adjustments.Cache, (object) row, aReadOnly);
    sender.RaiseFieldUpdated<APPayment.curyApplAmt>((object) row, (object) null);
  }

  public static void SetDocTypeList(PXCache cache, string docType)
  {
    switch (docType)
    {
      case "REF":
      case "VRF":
        PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, (object) "ADR");
        PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, (object) null, new string[2]
        {
          "ADR",
          "PPM"
        }, new string[2]{ "Debit Adj.", "Prepayment" });
        break;
      case "PPM":
        PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, (object) null, new string[3]
        {
          "INV",
          "ACR",
          "PPI"
        }, new string[3]
        {
          "Bill",
          "Credit Adj.",
          "Prepmt. Invoice"
        });
        break;
      case "ADR":
        PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, (object) null, new string[2]
        {
          "INV",
          "ACR"
        }, new string[2]{ "Bill", "Credit Adj." });
        break;
      case "PPI":
        PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, (object) null, new string[2]
        {
          "INV",
          "ACR"
        }, new string[2]{ "Bill", "Credit Adj." });
        break;
      case "CHK":
      case "VCK":
        PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, (object) null, new string[5]
        {
          "INV",
          "ADR",
          "ACR",
          "PPM",
          "PPI"
        }, new string[5]
        {
          "Bill",
          "Debit Adj.",
          "Credit Adj.",
          "Prepayment",
          "Prepmt. Invoice"
        });
        break;
      default:
        PXDefaultAttribute.SetDefault<APAdjust.adjdDocType>(cache, (object) "INV");
        PXStringListAttribute.SetList<APAdjust.adjdDocType>(cache, (object) null, new string[4]
        {
          "INV",
          "ACR",
          "PPM",
          "PPI"
        }, new string[4]
        {
          "Bill",
          "Credit Adj.",
          "Prepayment",
          "Prepmt. Invoice"
        });
        break;
    }
  }

  protected virtual void SetDocTypeList(object Row)
  {
    if (!(Row is APPayment apPayment))
      return;
    APPaymentEntry.SetDocTypeList(this.Adjustments.Cache, apPayment.DocType);
  }

  protected virtual void APPayment_Cleared_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    APPayment row = (APPayment) e.Row;
    if (row.Cleared.GetValueOrDefault())
    {
      if (row.ClearDate.HasValue)
        return;
      row.ClearDate = row.DocDate;
    }
    else
      row.ClearDate = new System.DateTime?();
  }

  protected virtual void APPayment_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    bool? released = ((APRegister) e.Row).Released;
    bool flag1 = false;
    if (!(released.GetValueOrDefault() == flag1 & released.HasValue))
      return;
    bool? voidAppl = ((APPayment) e.Row).VoidAppl;
    bool flag2 = false;
    if (!(voidAppl.GetValueOrDefault() == flag2 & voidAppl.HasValue))
      return;
    e.NewValue = (object) ((APPayment) e.Row).AdjDate;
    e.Cancel = true;
  }

  protected virtual void APPayment_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((APRegister) e.Row).Released.Value)
      return;
    e.NewValue = (object) ((APPayment) e.Row).AdjFinPeriodID;
    e.Cancel = true;
  }

  protected virtual void APPayment_TranPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((APRegister) e.Row).Released.Value)
      return;
    e.NewValue = (object) ((APPayment) e.Row).AdjTranPeriodID;
    e.Cancel = true;
  }

  protected virtual void APPayment_DepositAfter_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APPayment row = (APPayment) e.Row;
    if (!(row.DocType == "REF") || !row.DepositAsBatch.GetValueOrDefault())
      return;
    e.NewValue = (object) row.AdjDate;
    e.Cancel = true;
  }

  protected virtual void APPayment_DepositAsBatch_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(((APRegister) e.Row).DocType == "REF"))
      return;
    sender.SetDefaultExt<APPayment.depositAfter>(e.Row);
  }

  protected virtual void APPayment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is APPayment row))
      return;
    bool? released = row.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    row.DocDate = row.AdjDate;
    row.FinPeriodID = row.AdjFinPeriodID;
    row.TranPeriodID = row.AdjTranPeriodID;
    sender.RaiseExceptionHandling<APPayment.finPeriodID>(e.Row, (object) row.FinPeriodID, (Exception) null);
  }

  protected virtual void APPayment_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APPayment row = e.Row as APPayment;
    if (!this.IsApprovalRequired(row))
      return;
    sender.SetValue<APPayment.hold>((object) row, (object) true);
  }

  protected virtual void APPayment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    APPayment row1 = (APPayment) e.Row;
    if (!row1.Released.GetValueOrDefault())
    {
      row1.DocDate = row1.AdjDate;
      row1.FinPeriodID = row1.AdjFinPeriodID;
      row1.TranPeriodID = row1.AdjTranPeriodID;
      sender.RaiseExceptionHandling<APPayment.finPeriodID>((object) row1, (object) row1.FinPeriodID, (Exception) null);
      this.PaymentCharges.UpdateChangesFromPayment(sender, e);
    }
    bool? nullable = row1.OpenDoc;
    if (nullable.GetValueOrDefault())
    {
      nullable = row1.Hold;
      if (!nullable.GetValueOrDefault())
      {
        Decimal? curyOrigDocAmt1;
        if (row1.DocType == "CHK" || row1.DocType == "REF" || row1.DocType == "PPM")
        {
          curyOrigDocAmt1 = row1.CuryOrigDocAmt;
          Decimal num = 0M;
          if (curyOrigDocAmt1.GetValueOrDefault() < num & curyOrigDocAmt1.HasValue)
          {
            sender.RaiseExceptionHandling<APPayment.curyOrigDocAmt>((object) row1, (object) row1.CuryOrigDocAmt, (Exception) new PXSetPropertyException("Documents of the {0} type with negative amounts cannot be released.", PXErrorLevel.Error, new object[1]
            {
              (object) GetLabel.For<APDocType>(row1.DocType)
            }));
            goto label_14;
          }
        }
        if (this.IsPaymentUnbalanced(row1))
        {
          PXCache pxCache = sender;
          APPayment row2 = row1;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> curyOrigDocAmt2 = (ValueType) row1.CuryOrigDocAmt;
          nullable = this.APSetup.Current.SuggestPaymentAmount;
          int errorLevel;
          if (nullable ?? false)
          {
            curyOrigDocAmt1 = row1.CuryOrigDocAmt;
            Decimal num = 0M;
            if (curyOrigDocAmt1.GetValueOrDefault() == num & curyOrigDocAmt1.HasValue)
            {
              errorLevel = 2;
              goto label_12;
            }
          }
          errorLevel = 4;
label_12:
          PXSetPropertyException propertyException = new PXSetPropertyException("The document is out of the balance.", (PXErrorLevel) errorLevel);
          pxCache.RaiseExceptionHandling<APPayment.curyOrigDocAmt>((object) row2, (object) curyOrigDocAmt2, (Exception) propertyException);
        }
        else
          sender.RaiseExceptionHandling<APPayment.curyOrigDocAmt>((object) row1, (object) row1.CuryOrigDocAmt, (Exception) null);
      }
    }
label_14:
    if (!this.IsCopyPasteContext)
      return;
    sender.SetValue<APPayment.printed>((object) row1, (object) false);
    sender.SetDefaultExt<APPayment.printCheck>((object) row1);
  }

  public virtual bool IsPaymentUnbalanced(APPayment payment)
  {
    Decimal? nullable1;
    int num1;
    if (!payment.CanHaveBalance.GetValueOrDefault())
    {
      if (payment.IsMigratedRecord.GetValueOrDefault())
      {
        nullable1 = payment.CuryInitDocBal;
        Decimal num2 = 0M;
        num1 = nullable1.GetValueOrDefault() == num2 & nullable1.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
    }
    else
      num1 = 1;
    bool flag = num1 != 0;
    Decimal? nullable2;
    if (flag && !payment.VoidAppl.GetValueOrDefault())
    {
      nullable1 = payment.CuryUnappliedBal;
      Decimal num3 = 0M;
      if (!(nullable1.GetValueOrDefault() < num3 & nullable1.HasValue))
      {
        nullable1 = payment.CuryApplAmt;
        Decimal num4 = 0M;
        if (nullable1.GetValueOrDefault() < num4 & nullable1.HasValue)
        {
          nullable1 = payment.CuryUnappliedBal;
          nullable2 = payment.CuryOrigDocAmt;
          if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
            goto label_15;
        }
        nullable2 = payment.CuryOrigDocAmt;
        Decimal num5 = 0M;
        if (!(nullable2.GetValueOrDefault() < num5 & nullable2.HasValue))
          goto label_10;
      }
label_15:
      return true;
    }
label_10:
    if (flag)
      return false;
    nullable2 = payment.CuryUnappliedBal;
    Decimal num6 = 0M;
    if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
      return false;
    nullable2 = payment.CuryUnappliedBal;
    return nullable2.HasValue;
  }

  public bool IsRequestPrepayment(APPayment apdoc)
  {
    if (!apdoc.IsRequestPrepayment.HasValue)
    {
      PXSelectReadonly<APInvoice, Where<APInvoice.docType, Equal<Required<APPayment.docType>>, And<APInvoice.refNbr, Equal<Required<APPayment.refNbr>>, And<APInvoice.docType, Equal<APDocType.prepayment>>>>> pxSelectReadonly = new PXSelectReadonly<APInvoice, Where<APInvoice.docType, Equal<Required<APPayment.docType>>, And<APInvoice.refNbr, Equal<Required<APPayment.refNbr>>, And<APInvoice.docType, Equal<APDocType.prepayment>>>>>((PXGraph) this);
      using (new PXFieldScope(pxSelectReadonly.View, new System.Type[2]
      {
        typeof (APInvoice.docType),
        typeof (APInvoice.refNbr)
      }))
        apdoc.IsRequestPrepayment = new bool?(pxSelectReadonly.SelectSingle((object) apdoc.DocType, (object) apdoc.RefNbr) != null);
    }
    return apdoc.IsRequestPrepayment.GetValueOrDefault();
  }

  /// <summary>
  /// Creates Payment during CreatePayments process on Prepare Payments (AP505000)
  /// </summary>
  /// <param name="adjustments">Adjustment to add into new or already created AP Payment. Expected to contain adjustments to same document (several ones if Pay By Line is active for the document or single one - if not).</param>
  /// <param name="info">Currency Info for the new payment</param>
  public virtual void CreatePayment(IEnumerable<APAdjust> adjustments, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    if (!adjustments.Any<APAdjust>())
      return;
    adjustments.First<APAdjust>().SeparateCheck = new bool?(adjustments.Any<APAdjust>((Func<APAdjust, bool>) (_ => _.SeparateCheck.GetValueOrDefault())));
    this.Segregate(adjustments.First<APAdjust>(), info);
    APInvoice apInvoice = (APInvoice) null;
    foreach (APAdjust adjustment in adjustments)
      apInvoice = this.InsertGeneratedAdjustment(adjustment);
    Decimal? curyApplAmt = this.Document.Current.CuryApplAmt;
    APPayment copy = PXCache<APPayment>.CreateCopy(this.Document.Current);
    copy.CuryOrigDocAmt = curyApplAmt;
    copy.DocDesc = this.GetPaymentDescription(copy.DocDesc, this.vendor.Current.AcctCD, copy.DocDesc != apInvoice.DocDesc);
    this.Document.Cache.Update((object) copy);
    this.Save.Press();
    this.Adjustments.Cache.DisableReadItem = false;
    foreach (APAdjust adjustment in adjustments)
    {
      adjustment.AdjgDocType = this.Document.Current.DocType;
      adjustment.AdjgRefNbr = this.Document.Current.RefNbr;
    }
  }

  /// <summary>
  /// Inserts adjustment, generated by Prepare Payments (AP505000) within CreatePayment(...) method
  /// </summary>
  /// <param name="apdoc">APAdjust, generated by Prepare Payments (AP505000)</param>
  /// <returns>The related APInvoice instance</returns>
  public virtual APInvoice InsertGeneratedAdjustment(APAdjust apdoc)
  {
    APAdjust data = new APAdjust();
    data.AdjdDocType = apdoc.AdjdDocType;
    data.AdjdRefNbr = apdoc.AdjdRefNbr;
    data.AdjdLineNbr = apdoc.AdjdLineNbr;
    this.Document.Cache.SetValueExt<APPayment.curyOrigDocAmt>((object) this.Document.Current, (object) 0M);
    this.Adjustments.Cache.DisableReadItem = true;
    APInvoice apInvoice = (APInvoice) this.APInvoice_VendorID_DocType_RefNbr.Select((object) apdoc.AdjdLineNbr, (object) apdoc.VendorID, (object) apdoc.AdjdDocType, (object) apdoc.AdjdRefNbr);
    PXSelectorAttribute.StoreCached<APAdjust.adjdRefNbr>(this.Adjustments.Cache, (object) data, (object) apInvoice, true);
    APAdjust copy = PXCache<APAdjust>.CreateCopy(this.Adjustments.Insert(data));
    if (this.TakeDiscAlways)
    {
      copy.CuryAdjgAmt = new Decimal?(0M);
      copy.CuryAdjgDiscAmt = new Decimal?(0M);
      this.GetExtension<APPaymentEntry.APPaymentEntryDocumentExtension>().CalcBalancesFromAdjustedDocument(copy, true, !this.TakeDiscAlways);
      copy = PXCache<APAdjust>.CreateCopy((APAdjust) this.Adjustments.Cache.Update((object) copy));
    }
    if (apdoc.CuryAdjgAmt.HasValue)
    {
      copy.CuryAdjgAmt = apdoc.CuryAdjgAmt;
      copy = PXCache<APAdjust>.CreateCopy((APAdjust) this.Adjustments.Cache.Update((object) copy));
    }
    if (apdoc.CuryAdjgDiscAmt.HasValue)
    {
      copy.CuryAdjgDiscAmt = apdoc.CuryAdjgDiscAmt;
      copy = PXCache<APAdjust>.CreateCopy((APAdjust) this.Adjustments.Cache.Update((object) copy));
    }
    Decimal? curyApplAmt = this.Document.Current.CuryApplAmt;
    Decimal num1 = 0M;
    if (curyApplAmt.GetValueOrDefault() < num1 & curyApplAmt.HasValue)
    {
      Decimal? curyAdjgAmt = copy.CuryAdjgAmt;
      Decimal num2 = (Decimal) System.Math.Sign(copy.CuryAdjgAmt.GetValueOrDefault());
      Decimal? nullable1 = this.Document.Current.CuryApplAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3;
      if (!(curyAdjgAmt.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(curyAdjgAmt.GetValueOrDefault() + nullable2.GetValueOrDefault());
      Decimal? nullable4 = nullable3;
      if (nullable4.HasValue)
      {
        Decimal? nullable5 = nullable4;
        Decimal num3 = 0M;
        if (!(nullable5.GetValueOrDefault() <= num3 & nullable5.HasValue))
        {
          copy.CuryAdjgAmt = nullable4;
          this.Adjustments.Update(copy);
          goto label_14;
        }
      }
      this.Adjustments.Delete(copy);
    }
label_14:
    return apInvoice;
  }

  protected virtual string GetPaymentDescription(string descr, string vendor, bool multipleAdjust)
  {
    return !multipleAdjust ? descr : string.Format(PXMessages.LocalizeNoPrefix("Payment for {0}"), (object) vendor);
  }

  public virtual void CreatePayment(APInvoice apdoc)
  {
    string paymentType = apdoc.DocType == "ADR" ? "REF" : "CHK";
    this.CreatePayment(apdoc, paymentType);
  }

  public virtual void CreatePayment(APInvoice apdoc, string paymentType)
  {
    APPayment row = this.Document.Current;
    bool? nullable1;
    if (row != null && object.Equals((object) row.VendorID, (object) apdoc.VendorID) && object.Equals((object) row.VendorLocationID, (object) apdoc.PayLocationID))
    {
      nullable1 = apdoc.SeparateCheck;
      if (!nullable1.GetValueOrDefault())
        goto label_11;
    }
    this.Clear();
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, (object) apdoc.VendorID, (object) apdoc.PayLocationID);
    if (location == null)
      throw new PXException("Internal Error: {0}.", new object[1]
      {
        (object) 502
      });
    if (apdoc.PayTypeID == null)
      apdoc.PayTypeID = location.PaymentMethodID;
    int? nullable2 = apdoc.PayAccountID ?? location.CashAccountID;
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) nullable2);
    if (cashAccount == null)
      throw new PXException("Cash Account is not set up for Vendor.");
    if (cashAccount.CuryID == apdoc.CuryID)
      apdoc.PayAccountID = nullable2;
    APPayment apPayment1 = new APPayment();
    apPayment1.DocType = paymentType;
    APPayment copy1 = PXCache<APPayment>.CreateCopy(this.Document.Insert(apPayment1));
    copy1.VendorID = apdoc.VendorID;
    copy1.VendorLocationID = apdoc.PayLocationID;
    APPayment apPayment2 = copy1;
    System.DateTime t1 = this.Accessinfo.BusinessDate.Value;
    System.DateTime? docDate = apdoc.DocDate;
    System.DateTime t2 = docDate.Value;
    System.DateTime? nullable3 = System.DateTime.Compare(t1, t2) < 0 ? apdoc.DocDate : this.Accessinfo.BusinessDate;
    apPayment2.AdjDate = nullable3;
    copy1.CashAccountID = apdoc.PayAccountID;
    copy1.CuryID = apdoc.CuryID;
    copy1.PaymentMethodID = apdoc.PayTypeID;
    copy1.DocDesc = apdoc.DocDesc;
    this.FieldDefaulting.AddHandler<APPayment.cashAccountID>((PXFieldDefaulting) ((sender, e) =>
    {
      if (!(apdoc.DocType == "PPM"))
        return;
      e.NewValue = (object) null;
      e.Cancel = true;
    }));
    this.FieldDefaulting.AddHandler<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((PXFieldDefaulting) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) ((PX.Objects.CM.Extensions.CurrencyInfo) e.Row).CuryID;
      e.Cancel = true;
    }));
    row = this.Document.Update(copy1);
label_11:
    nullable1 = this.APSetup.Current.EarlyChecks;
    if (!nullable1.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(row.DocType, "CHK", "VCK", "PPM"))
    {
      docDate = apdoc.DocDate;
      System.DateTime? adjDate = row.AdjDate;
      if ((docDate.HasValue & adjDate.HasValue ? (docDate.GetValueOrDefault() <= adjDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 || row.AdjTranPeriodID != null && string.Compare(apdoc.TranPeriodID, row.AdjTranPeriodID) > 0)
        throw new PXException("The {0} bill cannot be paid on {1}, as it is posted in a future period ({2}) and the Enable Early Checks check box is cleared on the Account Payable Preferences (AP101000) form.", new object[3]
        {
          (object) apdoc.RefNbr,
          (object) apdoc.DocDate,
          (object) FinPeriodIDFormattingAttribute.FormatForError(apdoc.FinPeriodID)
        });
    }
    APAdjust apAdjust = new APAdjust();
    apAdjust.AdjdDocType = apdoc.DocType;
    apAdjust.AdjdRefNbr = apdoc.RefNbr;
    this.Document.SetValueExt<APPayment.curyOrigDocAmt>(row, (object) 0M);
    try
    {
      nullable1 = apdoc.PaymentsByLinesAllowed;
      if (nullable1.GetValueOrDefault())
      {
        foreach (PXResult<APTran> pxResult in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<APTran.curyTranBal, NotEqual<PX.Data.Zero>>>>, PX.Data.OrderBy<Desc<APTran.curyTranBal>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) apdoc
        }))
        {
          APTran apTran = (APTran) pxResult;
          APAdjust copy2 = PXCache<APAdjust>.CreateCopy(apAdjust);
          copy2.AdjdLineNbr = apTran.LineNbr;
          this.Adjustments.Insert(copy2);
        }
      }
      else
      {
        apAdjust.AdjdLineNbr = new int?(0);
        this.Adjustments.Insert(apAdjust);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new AdjustedNotFoundException();
    }
    Decimal? curyApplAmt = row.CuryApplAmt;
    Decimal? nullable4 = curyApplAmt;
    Decimal num = 0M;
    if (!(nullable4.GetValueOrDefault() > num & nullable4.HasValue))
      return;
    this.Document.SetValueExt<APPayment.curyOrigDocAmt>(row, (object) curyApplAmt);
    this.Document.Current = this.Document.Update(row);
  }

  protected virtual void APPayment_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    e.Cancel = true;
  }

  protected virtual void APPayment_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    e.Cancel = true;
  }

  protected virtual void APPayment_AdjFinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this._IsVoidCheckInProgress)
      e.Cancel = true;
    string errMsg;
    if (!this.VerifyAdjFinPeriodID((APPayment) e.Row, (string) e.NewValue, out errMsg))
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw new PXSetPropertyException(errMsg);
    }
  }

  protected virtual bool VerifyAdjFinPeriodID(APPayment doc, string newValue, out string errMsg)
  {
    errMsg = (string) null;
    if (doc.Released.GetValueOrDefault() && doc.FinPeriodID.CompareTo(newValue) > 0)
    {
      errMsg = $"Incorrect value. The value to be entered must be greater than or equal to {PeriodIDAttribute.FormatForError(doc.FinPeriodID)}.";
      return false;
    }
    if (doc.DocType == "VCK")
    {
      APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where2<Where<APPayment.docType, Equal<APDocType.check>, Or<APPayment.docType, Equal<APDocType.prepayment>>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) doc.RefNbr);
      if (apPayment != null && apPayment.FinPeriodID.CompareTo(newValue) > 0)
      {
        errMsg = $"Incorrect value. The value to be entered must be greater than or equal to {PeriodIDAttribute.FormatForError(apPayment.FinPeriodID)}.";
        return false;
      }
    }
    else
    {
      APAdjust apAdjust = (APAdjust) PXSelectBase<APAdjust, PXSelectJoin<APAdjust, LeftJoin<APAdjust2, On<APAdjust2.adjdDocType, Equal<APAdjust.adjdDocType>, And<APAdjust2.adjdRefNbr, Equal<APAdjust.adjdRefNbr>, And<APAdjust2.adjdLineNbr, Equal<APAdjust.adjdLineNbr>, And<APAdjust2.adjgDocType, Equal<APAdjust.adjgDocType>, And<APAdjust2.adjgRefNbr, Equal<APAdjust.adjgRefNbr>, And<APAdjust2.adjNbr, NotEqual<APAdjust.adjNbr>, And<Switch<Case<Where<APAdjust.voidAdjNbr, PX.Data.IsNotNull>, APAdjust.voidAdjNbr>, APAdjust.adjNbr>, Equal<Switch<Case<Where<APAdjust.voidAdjNbr, PX.Data.IsNotNull>, APAdjust2.adjNbr>, APAdjust2.voidAdjNbr>>, And<APAdjust2.adjgTranPeriodID, Equal<APAdjust.adjgTranPeriodID>, And<APAdjust2.released, Equal<PX.Data.True>, And<APAdjust2.voided, Equal<PX.Data.True>, And<APAdjust.voided, Equal<PX.Data.True>>>>>>>>>>>>>, Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.released, Equal<PX.Data.True>, And<APAdjust2.adjdRefNbr, PX.Data.IsNull>>>>, PX.Data.OrderBy<Desc<APAdjust.adjgTranPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) doc.DocType, (object) doc.RefNbr);
      if (apAdjust != null && apAdjust.AdjgFinPeriodID.CompareTo(newValue) > 0)
      {
        errMsg = $"Incorrect value. The value to be entered must be greater than or equal to {PeriodIDAttribute.FormatForError(apAdjust.AdjgFinPeriodID)}.";
        return false;
      }
    }
    return true;
  }

  protected virtual void APPayment_EmployeeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    e.Cancel = true;
  }

  public virtual void TryToVoidCheck(APPayment doc)
  {
    try
    {
      this._IsVoidCheckInProgress = true;
      this.VoidCheckProc(doc);
    }
    catch (PXSetPropertyException ex)
    {
      this.Clear();
      this.Document.Current = doc;
      throw;
    }
    finally
    {
      this._IsVoidCheckInProgress = false;
    }
  }

  public virtual void VoidCheckProc(APPayment doc)
  {
    this.Clear(PXClearOption.PreserveTimeStamp);
    this.Document.View.Answer = WebDialogResult.No;
    foreach (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Vendor> pxResult in PXSelectBase<APPayment, PXSelectJoin<APPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APPayment.curyInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<APPayment.cashAccountID>>>>>>, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr, (object) doc.VendorID))
    {
      doc = (APPayment) pxResult;
      PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy((PX.Objects.CM.Extensions.CurrencyInfo) pxResult);
      copy1.CuryInfoID = new long?();
      copy1.IsReadOnly = new bool?(false);
      PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(copy1));
      APPayment apPayment1 = new APPayment();
      apPayment1.DocType = APPaymentType.GetVoidingAPDocType(doc.DocType);
      apPayment1.RefNbr = doc.RefNbr;
      apPayment1.CuryInfoID = copy2.CuryInfoID;
      apPayment1.VoidAppl = new bool?(true);
      APPayment apPayment2 = this.Document.Insert(apPayment1);
      string createdByScreenId = apPayment2.CreatedByScreenID;
      Guid? createdById = apPayment2.CreatedByID;
      APPayment copy3 = PXCache<APPayment>.CreateCopy((APPayment) pxResult);
      copy3.CreatedByScreenID = createdByScreenId;
      copy3.CreatedByID = createdById;
      copy3.CuryInfoID = copy2.CuryInfoID;
      copy3.VoidAppl = new bool?(true);
      copy3.CATranID = new long?();
      copy3.Printed = new bool?(true);
      copy3.OpenDoc = new bool?(true);
      copy3.Released = new bool?(false);
      copy3.OrigDocType = doc.DocType;
      copy3.OrigRefNbr = doc.RefNbr;
      copy3.OrigModule = "AP";
      if (doc.DocType == "PPM" || doc.DocType == "CHK")
        copy3.ExtRefNbr = doc.ExtRefNbr;
      this.Document.Cache.SetDefaultExt<APPayment.hold>((object) copy3);
      this.Document.Cache.SetDefaultExt<APPayment.isMigratedRecord>((object) copy3);
      this.Document.Cache.SetDefaultExt<APPayment.status>((object) copy3);
      copy3.LineCntr = new int?(0);
      copy3.AdjCntr = new int?(0);
      copy3.BatchNbr = (string) null;
      APPayment apPayment3 = copy3;
      Decimal num1 = (Decimal) -1;
      Decimal? nullable1 = copy3.CuryOrigDocAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num1 * nullable1.GetValueOrDefault()) : new Decimal?();
      apPayment3.CuryOrigDocAmt = nullable2;
      APPayment apPayment4 = copy3;
      Decimal num2 = (Decimal) -1;
      nullable1 = copy3.OrigDocAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
      apPayment4.OrigDocAmt = nullable3;
      APPayment apPayment5 = copy3;
      Decimal num3 = (Decimal) -1;
      nullable1 = copy3.CuryInitDocBal;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(num3 * nullable1.GetValueOrDefault()) : new Decimal?();
      apPayment5.CuryInitDocBal = nullable4;
      APPayment apPayment6 = copy3;
      Decimal num4 = (Decimal) -1;
      nullable1 = copy3.InitDocBal;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(num4 * nullable1.GetValueOrDefault()) : new Decimal?();
      apPayment6.InitDocBal = nullable5;
      copy3.CuryChargeAmt = new Decimal?(0M);
      APPayment apPayment7 = copy3;
      nullable1 = new Decimal?();
      Decimal? nullable6 = nullable1;
      apPayment7.CuryApplAmt = nullable6;
      APPayment apPayment8 = copy3;
      nullable1 = new Decimal?();
      Decimal? nullable7 = nullable1;
      apPayment8.CuryUnappliedBal = nullable7;
      copy3.AdjDate = doc.DocDate;
      FinPeriodIDAttribute.SetPeriodsByMaster<APPayment.adjFinPeriodID>(this.Document.Cache, (object) copy3, doc.TranPeriodID);
      copy3.ExternalPaymentID = (string) null;
      copy3.ExternalPaymentStatus = (string) null;
      copy3.ExternalOrganizationID = (string) null;
      copy3.ExternalPaymentUpdateTime = new System.DateTime?();
      copy3.ExternalPaymentDisbursementType = (string) null;
      copy3.ExternalPaymentSentDate = new System.DateTime?();
      copy3.ExternalPaymentBatchNbr = (string) null;
      copy3.ExternalPaymentCardNbr = (string) null;
      copy3.ExternalPaymentCheckNbr = (string) null;
      copy3.ExternalPaymentTraceNbr = (string) null;
      this.Document.Cache.SetDefaultExt<APRegister.employeeID>((object) copy3);
      this.Document.Cache.SetDefaultExt<APRegister.employeeWorkgroupID>((object) copy3);
      bool? nullable8;
      if (this.cashaccount.Current != null)
      {
        nullable8 = this.cashaccount.Current.Reconcile;
        bool flag = false;
        if (nullable8.GetValueOrDefault() == flag & nullable8.HasValue)
        {
          copy3.Cleared = new bool?(true);
          copy3.ClearDate = copy3.DocDate;
          goto label_8;
        }
      }
      copy3.Cleared = new bool?(false);
      copy3.ClearDate = new System.DateTime?();
label_8:
      nullable8 = copy3.DepositAsBatch;
      if (nullable8.GetValueOrDefault() && !string.IsNullOrEmpty(copy3.DepositNbr))
      {
        nullable8 = copy3.Deposited;
        bool flag = false;
        if (nullable8.GetValueOrDefault() == flag & nullable8.HasValue)
          throw new PXException("This refund is included in the {0} bank deposit. It cannot be voided until the deposit is released or the refund is excluded from it.", new object[1]
          {
            (object) copy3.DepositNbr
          });
        copy3.DepositAsBatch = new bool?(false);
        copy3.DepositType = (string) null;
        copy3.DepositNbr = (string) null;
        copy3.Deposited = new bool?(false);
        copy3.DepositDate = new System.DateTime?();
      }
      this.Document.Cache.SetDefaultExt<APPayment.noteID>((object) copy3);
      APPayment data = this.Document.Update(copy3);
      this.Document.Cache.SetDefaultExt<APPayment.printCheck>((object) data);
      this.Document.Cache.SetValueExt<APPayment.adjFinPeriodID>((object) data, (object) PeriodIDAttribute.FormatForDisplay(doc.FinPeriodID));
      using (new SuppressWorkflowAutoPersistScope((PXGraph) this))
        this.initializeState.Press();
      APPayment current = this.Document.Current;
      if (copy2 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APPayment.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null);
        currencyInfo.CuryID = copy2.CuryID;
        currencyInfo.CuryEffDate = copy2.CuryEffDate;
        currencyInfo.CuryRateTypeID = copy2.CuryRateTypeID;
        currencyInfo.CuryRate = copy2.CuryRate;
        currencyInfo.RecipRate = copy2.RecipRate;
        currencyInfo.CuryMultDiv = copy2.CuryMultDiv;
        this.currencyinfo.Update(currencyInfo);
      }
    }
    foreach (PXResult<APAdjust, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<APAdjust, PXSelectJoin<APAdjust, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APAdjust.adjdCuryInfoID>>>, Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.voided, NotEqual<PX.Data.True>, And<APAdjust.isInitialApplication, NotEqual<PX.Data.True>>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APAdjust copy = PXCache<APAdjust>.CreateCopy((APAdjust) pxResult);
      bool? nullable9;
      if (!(doc.DocType != "ADR"))
      {
        nullable9 = doc.PendingPPD;
        if (nullable9.GetValueOrDefault())
          goto label_31;
      }
      nullable9 = copy.AdjdHasPPDTaxes;
      if (nullable9.GetValueOrDefault())
      {
        nullable9 = copy.PendingPPD;
        if (!nullable9.GetValueOrDefault())
        {
          APAdjust ppdApplication = APPaymentEntry.GetPPDApplication((PXGraph) this, copy.AdjdDocType, copy.AdjdRefNbr);
          if (ppdApplication != null && (ppdApplication.AdjgDocType != copy.AdjgDocType || ppdApplication.AdjgRefNbr != copy.AdjgRefNbr))
          {
            APAdjust apAdjust = (APAdjust) pxResult;
            this.Clear();
            APAdjust row = (APAdjust) this.Adjustments.Cache.Update((object) apAdjust);
            this.Document.Current = (APPayment) this.Document.Search<APPayment.refNbr>((object) row.AdjgRefNbr, (object) row.AdjgDocType);
            this.Adjustments.Cache.RaiseExceptionHandling<APAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("To proceed, you have to reverse application of the final payment {0} with cash discount given.", PXErrorLevel.RowError, new object[1]
            {
              (object) ppdApplication.AdjgRefNbr
            }));
            throw new PXSetPropertyException("To proceed, you have to reverse application of the final payment {0} with cash discount given.", new object[1]
            {
              (object) ppdApplication.AdjgRefNbr
            });
          }
        }
      }
label_31:
      copy.VoidAppl = new bool?(true);
      copy.Released = new bool?(false);
      this.Adjustments.Cache.SetDefaultExt<APAdjust.isMigratedRecord>((object) copy);
      copy.VoidAdjNbr = copy.AdjNbr;
      copy.AdjNbr = new int?(0);
      copy.AdjBatchNbr = (string) null;
      APAdjust apAdjust1 = this.Adjustments.Insert(new APAdjust()
      {
        AdjgDocType = copy.AdjgDocType,
        AdjgRefNbr = copy.AdjgRefNbr,
        AdjgBranchID = copy.AdjgBranchID,
        AdjdDocType = copy.AdjdDocType,
        AdjdRefNbr = copy.AdjdRefNbr,
        AdjdLineNbr = copy.AdjdLineNbr,
        AdjdBranchID = copy.AdjdBranchID,
        VendorID = copy.VendorID,
        AdjdCuryInfoID = copy.AdjdCuryInfoID,
        AdjdHasPPDTaxes = copy.AdjdHasPPDTaxes
      });
      if (apAdjust1 == null)
      {
        APAdjust apAdjust2 = (APAdjust) pxResult;
        this.Clear();
        APAdjust row = (APAdjust) this.Adjustments.Cache.Update((object) apAdjust2);
        this.Document.Current = (APPayment) this.Document.Search<APPayment.refNbr>((object) row.AdjgRefNbr, (object) row.AdjgDocType);
        this.Adjustments.Cache.RaiseExceptionHandling<APAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("Multiple applications exists for this document. Please reverse these applications individually and then void the document.", PXErrorLevel.RowError));
        throw new PXException("Multiple applications exists for this document. Please reverse these applications individually and then void the document.");
      }
      APAdjust apAdjust3 = copy;
      Decimal num5 = (Decimal) -1;
      Decimal? nullable10 = copy.CuryAdjgAmt;
      Decimal? nullable11 = nullable10.HasValue ? new Decimal?(num5 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust3.CuryAdjgAmt = nullable11;
      APAdjust apAdjust4 = copy;
      Decimal num6 = (Decimal) -1;
      nullable10 = copy.CuryAdjgDiscAmt;
      Decimal? nullable12 = nullable10.HasValue ? new Decimal?(num6 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust4.CuryAdjgDiscAmt = nullable12;
      APAdjust apAdjust5 = copy;
      Decimal num7 = (Decimal) -1;
      nullable10 = copy.CuryAdjgWhTaxAmt;
      Decimal? nullable13 = nullable10.HasValue ? new Decimal?(num7 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust5.CuryAdjgWhTaxAmt = nullable13;
      APAdjust apAdjust6 = copy;
      Decimal num8 = (Decimal) -1;
      nullable10 = copy.AdjAmt;
      Decimal? nullable14 = nullable10.HasValue ? new Decimal?(num8 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust6.AdjAmt = nullable14;
      APAdjust apAdjust7 = copy;
      Decimal num9 = (Decimal) -1;
      nullable10 = copy.AdjDiscAmt;
      Decimal? nullable15 = nullable10.HasValue ? new Decimal?(num9 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust7.AdjDiscAmt = nullable15;
      APAdjust apAdjust8 = copy;
      Decimal num10 = (Decimal) -1;
      nullable10 = copy.AdjWhTaxAmt;
      Decimal? nullable16 = nullable10.HasValue ? new Decimal?(num10 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust8.AdjWhTaxAmt = nullable16;
      APAdjust apAdjust9 = copy;
      Decimal num11 = (Decimal) -1;
      nullable10 = copy.CuryAdjdAmt;
      Decimal? nullable17 = nullable10.HasValue ? new Decimal?(num11 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust9.CuryAdjdAmt = nullable17;
      APAdjust apAdjust10 = copy;
      Decimal num12 = (Decimal) -1;
      nullable10 = copy.CuryAdjdDiscAmt;
      Decimal? nullable18 = nullable10.HasValue ? new Decimal?(num12 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust10.CuryAdjdDiscAmt = nullable18;
      APAdjust apAdjust11 = copy;
      Decimal num13 = (Decimal) -1;
      nullable10 = copy.CuryAdjdWhTaxAmt;
      Decimal? nullable19 = nullable10.HasValue ? new Decimal?(num13 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust11.CuryAdjdWhTaxAmt = nullable19;
      APAdjust apAdjust12 = copy;
      Decimal num14 = (Decimal) -1;
      nullable10 = copy.RGOLAmt;
      Decimal? nullable20 = nullable10.HasValue ? new Decimal?(num14 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust12.RGOLAmt = nullable20;
      copy.AdjgCuryInfoID = this.Document.Current.CuryInfoID;
      APAdjust apAdjust13 = copy;
      Decimal num15 = (Decimal) -1;
      nullable10 = copy.CuryAdjgPPDAmt;
      Decimal? nullable21 = nullable10.HasValue ? new Decimal?(num15 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust13.CuryAdjgPPDAmt = nullable21;
      APAdjust apAdjust14 = copy;
      Decimal num16 = (Decimal) -1;
      nullable10 = copy.AdjPPDAmt;
      Decimal? nullable22 = nullable10.HasValue ? new Decimal?(num16 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust14.AdjPPDAmt = nullable22;
      APAdjust apAdjust15 = copy;
      Decimal num17 = (Decimal) -1;
      nullable10 = copy.CuryAdjdPPDAmt;
      Decimal? nullable23 = nullable10.HasValue ? new Decimal?(num17 * nullable10.GetValueOrDefault()) : new Decimal?();
      apAdjust15.CuryAdjdPPDAmt = nullable23;
      copy.AdjdCuryRate = apAdjust1.AdjdCuryRate;
      this.Adjustments.Cache.SetDefaultExt<APAdjust.noteID>((object) copy);
      this.Adjustments.Update(copy);
    }
    this.PaymentCharges.ReverseCharges((PX.Objects.CM.IRegister) doc, (PX.Objects.CM.IRegister) this.Document.Current);
  }

  public virtual IEnumerable<AdjustmentStubGroup> GetAdjustmentsPrintList()
  {
    List<AdjustmentStubGroup> list = ((IEnumerable<APAdjust>) this.Adjustments_print.SelectMain()).GroupBy<APAdjust, AdjustmentGroupKey, IAdjustmentStub>((Func<APAdjust, AdjustmentGroupKey>) (adj => new AdjustmentGroupKey()
    {
      Source = "A",
      AdjdDocType = adj.AdjdDocType,
      AdjdRefNbr = adj.AdjdRefNbr,
      AdjdCuryInfoID = adj.AdjdCuryInfoID
    }), (Func<APAdjust, IAdjustmentStub>) (adj => (IAdjustmentStub) adj)).Where<IGrouping<AdjustmentGroupKey, IAdjustmentStub>>((Func<IGrouping<AdjustmentGroupKey, IAdjustmentStub>, bool>) (g =>
    {
      Decimal? nullable = g.Sum<IAdjustmentStub>((Func<IAdjustmentStub, Decimal?>) (a => a.CuryAdjgAmt));
      Decimal num = 0M;
      return !(nullable.GetValueOrDefault() == num & nullable.HasValue);
    })).Select<IGrouping<AdjustmentGroupKey, IAdjustmentStub>, AdjustmentStubGroup>((Func<IGrouping<AdjustmentGroupKey, IAdjustmentStub>, AdjustmentStubGroup>) (g => new AdjustmentStubGroup()
    {
      GroupedStubs = g
    })).ToList<AdjustmentStubGroup>();
    if (this.Document.Current.DocType == "PPM" && list.Count<AdjustmentStubGroup>() == 0)
    {
      AdjustmentStubGroup outstandingBalanceRow = this.GetOutstandingBalanceRow();
      if (outstandingBalanceRow != null)
        list.Add(outstandingBalanceRow);
    }
    return (IEnumerable<AdjustmentStubGroup>) list;
  }

  protected virtual AdjustmentStubGroup GetOutstandingBalanceRow()
  {
    Decimal? curyUnappliedBal = this.Document.Current.CuryUnappliedBal;
    if (curyUnappliedBal.HasValue)
    {
      Decimal? nullable = curyUnappliedBal;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        IEnumerable<IGrouping<AdjustmentGroupKey, IAdjustmentStub>> source = ((IEnumerable<IAdjustmentStub>) new IAdjustmentStub[1]
        {
          (IAdjustmentStub) new OutstandingBalanceRow()
          {
            CuryOutstandingBalance = curyUnappliedBal,
            OutstandingBalanceDate = this.Accessinfo.BusinessDate
          }
        }).GroupBy<IAdjustmentStub, AdjustmentGroupKey, IAdjustmentStub>((Func<IAdjustmentStub, AdjustmentGroupKey>) (adj => new AdjustmentGroupKey()
        {
          Source = "T",
          AdjdDocType = "---",
          AdjdRefNbr = string.Empty,
          AdjdCuryInfoID = this.Document.Current.CuryInfoID
        }), (Func<IAdjustmentStub, IAdjustmentStub>) (adj => adj));
        return new AdjustmentStubGroup()
        {
          GroupedStubs = source.Single<IGrouping<AdjustmentGroupKey, IAdjustmentStub>>()
        };
      }
    }
    return (AdjustmentStubGroup) null;
  }

  public virtual void AddCheckDetail(AdjustmentStubGroup adj, string stubNbr)
  {
    this.CheckDetails.Insert(this.GetCheckDetail(adj, stubNbr));
  }

  protected virtual APPrintCheckDetail GetCheckDetail(AdjustmentStubGroup adj, string stubNbr)
  {
    APPrintCheckDetail checkDetail = new APPrintCheckDetail();
    checkDetail.AdjgDocType = this.Document.Current.DocType;
    checkDetail.AdjgRefNbr = this.Document.Current.RefNbr;
    checkDetail.Source = adj.GroupedStubs.Key.Source;
    checkDetail.AdjdDocType = adj.GroupedStubs.Key.AdjdDocType;
    checkDetail.AdjdRefNbr = adj.GroupedStubs.Key.AdjdRefNbr;
    checkDetail.AdjdCuryInfoID = adj.GroupedStubs.Key.AdjdCuryInfoID;
    checkDetail.AdjgCuryInfoID = this.Document.Current.CuryInfoID;
    checkDetail.StubNbr = stubNbr;
    checkDetail.CashAccountID = this.Document.Current.CashAccountID;
    checkDetail.PaymentMethodID = this.Document.Current.PaymentMethodID;
    var data = adj.GroupedStubs.GroupBy<IAdjustmentStub, int>((Func<IAdjustmentStub, int>) (a => 1)).Select(a => new
    {
      TotalCuryAdjgAmt = a.Sum<IAdjustmentStub>((Func<IAdjustmentStub, Decimal?>) (r => r.CuryAdjgAmt)),
      TotalCuryAdjgDiscAmt = a.Sum<IAdjustmentStub>((Func<IAdjustmentStub, Decimal?>) (r => r.CuryAdjgDiscAmt)),
      CuryOutstandingBalance = a.Sum<IAdjustmentStub>((Func<IAdjustmentStub, Decimal?>) (r => r.CuryOutstandingBalance)),
      OutstandingBalanceDate = a.Max<IAdjustmentStub, System.DateTime?>((Func<IAdjustmentStub, System.DateTime?>) (r => r.OutstandingBalanceDate)),
      CuryExtraDocBal = a.Sum<IAdjustmentStub>((Func<IAdjustmentStub, Decimal?>) (r => !r.IsRequest.GetValueOrDefault() ? new Decimal?(0M) : r.CuryAdjgAmt))
    }).Single();
    checkDetail.CuryAdjgAmt = data.TotalCuryAdjgAmt;
    checkDetail.CuryAdjgDiscAmt = data.TotalCuryAdjgDiscAmt;
    checkDetail.CuryOutstandingBalance = data.CuryOutstandingBalance;
    checkDetail.CuryExtraDocBal = data.CuryExtraDocBal;
    checkDetail.OutstandingBalanceDate = data.OutstandingBalanceDate;
    return checkDetail;
  }

  public virtual void DeletePrintList()
  {
    EnumerableExtensions.ForEach<PXResult<APPrintCheckDetail>>((IEnumerable<PXResult<APPrintCheckDetail>>) new PXSelect<APPrintCheckDetail, Where<APPrintCheckDetail.adjgDocType, Equal<Required<APPrintCheckDetail.adjgDocType>>, And<APPrintCheckDetail.adjgRefNbr, Equal<Required<APPrintCheckDetail.adjgRefNbr>>>>>((PXGraph) this).Select((object) this.Document.Current.DocType, (object) this.Document.Current.RefNbr), (System.Action<PXResult<APPrintCheckDetail>>) (c => this.CheckDetails.Delete((APPrintCheckDetail) c)));
  }

  public virtual void RefillAPPrintCheckDetail(APPayment payment)
  {
    this.Document.Current = payment;
    this.DeletePrintList();
    IEnumerable<AdjustmentStubGroup> adjustmentsPrintList = this.GetAdjustmentsPrintList();
    PX.Objects.CA.PaymentMethod current = this.paymenttype.Current;
    if (!current.APCreateBatchPayment.GetValueOrDefault())
    {
      string str = payment.ExtRefNbr;
      short num1 = 0;
      foreach (AdjustmentStubGroup adj in adjustmentsPrintList)
      {
        int num2 = (int) num1;
        short? apStubLines = current.APStubLines;
        int? nullable = apStubLines.HasValue ? new int?((int) apStubLines.GetValueOrDefault() - 1) : new int?();
        int valueOrDefault = nullable.GetValueOrDefault();
        if (num2 > valueOrDefault & nullable.HasValue)
        {
          if (current.APPrintRemittance.GetValueOrDefault())
          {
            this.AddCheckDetail(adj, (string) null);
            continue;
          }
          num1 = (short) 0;
          str = AutoNumberAttribute.NextNumber(str);
        }
        this.AddCheckDetail(adj, str);
        ++num1;
      }
    }
    else
    {
      foreach (AdjustmentStubGroup adj in adjustmentsPrintList)
        this.AddCheckDetail(adj, (string) null);
    }
  }

  protected virtual void DeleteUnreleasedApplications()
  {
    foreach (PXResult<APAdjust> pxResult in this.Adjustments_Raw.Select())
      this.Adjustments.Cache.Delete((object) (APAdjust) pxResult);
  }

  public virtual void CheckDocumentBeforeReversing(PXGraph graph, APAdjust application)
  {
  }

  /// <exclude />
  public class SuppressCuryAdjustingScope : 
    FlaggedModeScopeBase<APPaymentEntry.SuppressCuryAdjustingScope>
  {
  }

  public class MultiCurrency : APMultiCurrencyGraph<APPaymentEntry, APPayment>
  {
    protected override string DocumentStatus => this.Base.Document.Current?.Status;

    protected override MultiCurrencyGraph<APPaymentEntry, APPayment>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<APPaymentEntry, APPayment>.CurySourceMapping(typeof (PX.Objects.CA.CashAccount))
      {
        CuryID = typeof (PX.Objects.CA.CashAccount.curyID),
        CuryRateTypeID = typeof (PX.Objects.CA.CashAccount.curyRateTypeID)
      };
    }

    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = base.CurrentSourceSelect();
      if (curySource != null)
        curySource.AllowOverrideRate = (bool?) this.Base.vendor?.Current?.AllowOverrideRate;
      return curySource;
    }

    protected override MultiCurrencyGraph<APPaymentEntry, APPayment>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<APPaymentEntry, APPayment>.DocumentMapping(typeof (APPayment))
      {
        DocumentDate = typeof (APPayment.adjDate),
        BAccountID = typeof (APPayment.vendorID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[8]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.Adjustments,
        (PXSelectBase) this.Base.Adjustments_Balance,
        (PXSelectBase) this.Base.Adjustments_Invoices,
        (PXSelectBase) this.Base.Adjustments_Payments,
        (PXSelectBase) this.Base.PaymentCharges,
        (PXSelectBase) this.Base.dummy_CATran,
        (PXSelectBase) this.Base.APPost
      };
    }

    public virtual bool EnableCrossRate(APAdjust adj)
    {
      bool? released = adj.Released;
      bool flag = false;
      if (!(released.GetValueOrDefault() == flag & released.HasValue))
        return false;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.GetCurrencyInfo(adj.AdjgCuryInfoID);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.GetCurrencyInfo(adj.AdjdCuryInfoID);
      return currencyInfo2 != null && !string.Equals(currencyInfo1.CuryID, currencyInfo2.CuryID) && !string.Equals(currencyInfo2.CuryID, currencyInfo2.BaseCuryID);
    }

    protected override bool ShouldBeDisabledDueToDocStatus()
    {
      string docType = this.Base.Document.Current?.DocType;
      return docType == "VCK" || docType == "VRF" || base.ShouldBeDisabledDueToDocStatus();
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<APPayment, APPayment.cashAccountID> e)
    {
      if (this.Base._IsVoidCheckInProgress || !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>() || FlaggedModeScopeBase<APPaymentEntry.SuppressCuryAdjustingScope>.IsActive)
        return;
      this.RestoreDifferentCurrencyInfoIDs<APAdjust>((PXSelectBase<APAdjust>) this.Base.Adjustments);
      this.SourceFieldUpdated<APPayment.curyInfoID, APPayment.curyID, APPayment.adjDate>(e.Cache, (IBqlTable) e.Row);
      this.SetAdjgCuryInfoID<APAdjust>((PXSelectBase<APAdjust>) this.Base.Adjustments, e.Row.CuryInfoID);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.documentDate> e)
    {
      base._(e);
      if (this.ShouldBeDisabledDueToDocStatus() || FlaggedModeScopeBase<APPaymentEntry.SuppressCuryAdjustingScope>.IsActive)
        return;
      this.Base.LoadInvoicesProc(true);
      APPayment apPayment = (APPayment) e.Row.Base;
      foreach (APAdjust adj in this.Base.Adjustments.View.SelectExternal().RowCast<APAdjust>())
      {
        APInvoice apInvoice = APInvoice.PK.Find((PXGraph) this.Base, adj.AdjdDocType, adj.AdjdRefNbr);
        System.DateTime? adjDate = apPayment.AdjDate;
        System.DateTime? discDate = (System.DateTime?) apInvoice?.DiscDate;
        if ((adjDate.HasValue & discDate.HasValue ? (adjDate.GetValueOrDefault() <= discDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 && apInvoice.OrigModule == "AP")
        {
          bool checkBalance = !(apPayment.Status == "H") && !(apPayment.Status == "B");
          this.Base.applyAPAdjust(adj, apPayment.CuryUnappliedBal.Value, checkBalance, true);
        }
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldSelecting<APAdjust, APAdjust.adjdCuryID> e)
    {
      e.ReturnValue = (object) this.CuryIDFieldSelecting<APAdjust.adjdCuryInfoID>(e.Cache, (object) e.Row);
    }
  }

  private class PXLoadInvoiceException : Exception
  {
    public PXLoadInvoiceException()
    {
    }

    public PXLoadInvoiceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

  /// <exclude />
  public class APPaymentEntryAddressLookupExtension : 
    AddressLookupExtension<APPaymentEntry, APPayment, APAddress>
  {
    protected override string AddressView => "Remittance_Address";
  }

  public class APPaymentEntryAddressCachingHelper : 
    AddressValidationExtension<APPaymentEntry, APAddress>
  {
    protected override IEnumerable<PXSelectBase<APAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      APPaymentEntry.APPaymentEntryAddressCachingHelper addressCachingHelper = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (PXSelectBase<APAddress>) addressCachingHelper.Base.Remittance_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  public class APPaymentEntryVendorDocsExtension : VendorDocsExtensionBase<APPaymentEntry>
  {
  }

  public class APPaymentEntryDocumentExtension : 
    PaymentGraphExtension<APPaymentEntry, APPayment, APAdjust, APInvoice, APTran>
  {
    protected override bool DiscOnDiscDate => !this.Base.TakeDiscAlways;

    protected override bool InternalCall => this.Base.UnattendedMode;

    public override PXSelectBase<APAdjust> Adjustments
    {
      get => (PXSelectBase<APAdjust>) this.Base.Adjustments_Raw;
    }

    protected override AbstractPaymentBalanceCalculator<APAdjust, APTran> GetAbstractBalanceCalculator()
    {
      return (AbstractPaymentBalanceCalculator<APAdjust, APTran>) new APPaymentBalanceCalculator((IPXCurrencyHelper) this.Base.GetExtension<APPaymentEntry.MultiCurrency>());
    }

    public override void Initialize()
    {
      base.Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Payment>((PXSelectBase) this.Base.Document);
    }

    protected override PaymentMapping GetPaymentMapping() => new PaymentMapping(typeof (APPayment));

    public override void CalcBalancesFromAdjustedDocument(
      APAdjust adj,
      bool isCalcRGOL,
      bool DiscOnDiscDate)
    {
      PXResultset<APInvoice> pxResultset;
      if (this.Base.balanceCache == null || !this.Base.balanceCache.TryGetValue(adj, out pxResultset))
        pxResultset = this.Base.APInvoice_VendorID_DocType_RefNbr.Select((object) adj.AdjdLineNbr, (object) adj.VendorID, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr);
      using (IEnumerator<PXResult<APInvoice>> enumerator = pxResultset.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<APInvoice, APTran> current = (PXResult<APInvoice, APTran>) enumerator.Current;
          APInvoice voucher = (APInvoice) current;
          APTran tran = (APTran) current;
          this.CalcBalances<APInvoice>(adj, voucher, isCalcRGOL, DiscOnDiscDate, tran);
          return;
        }
      }
      foreach (PXResult<APPayment> pxResult in this.Base.APPayment_VendorID_DocType_RefNbr.Select((object) adj.VendorID, (object) adj.AdjdDocType, (object) adj.AdjdRefNbr))
      {
        APPayment voucher = (APPayment) pxResult;
        this.CalcBalances<APPayment>(adj, voucher, isCalcRGOL, DiscOnDiscDate, (APTran) null);
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<APAdjust, APAdjust.curyAdjgPPDAmt> e)
    {
      if (e.Row == null)
        return;
      if (e.OldValue != null)
      {
        Decimal? nullable = e.Row.CuryDocBal;
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        {
          nullable = e.Row.CuryAdjgAmt;
          Decimal oldValue = (Decimal) e.OldValue;
          if (nullable.GetValueOrDefault() < oldValue & nullable.HasValue)
            e.Row.CuryAdjgDiscAmt = new Decimal?(0M);
        }
      }
      e.Row.FillDiscAmts();
      this.CalcBalancesFromAdjustedDocument(e.Row, true, !this.Base.TakeDiscAlways);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdating<APAdjust, APAdjust.curyWhTaxBal> e)
    {
      e.Cancel = true;
      if (this.InternalCall || e.Row == null)
        return;
      if (e.Row.AdjdCuryInfoID.HasValue && !e.Row.CuryWhTaxBal.HasValue && e.Cache.GetStatus((object) e.Row) != PXEntryStatus.Deleted)
        this.CalcBalancesFromAdjustedDocument(e.Row, false, this.DiscOnDiscDate);
      e.NewValue = (object) e.Row.CuryWhTaxBal;
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgAmt> e)
    {
      APAdjust adj = e.Row;
      using (IEnumerator<string> enumerator = e.Cache.Keys.Where<string>((Func<string, bool>) (key => e.Cache.GetValue((object) adj, key) == null)).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName(e.Cache, current)
          });
        }
      }
      Decimal? nullable = adj.CuryDocBal;
      if (nullable.HasValue)
      {
        nullable = adj.CuryDiscBal;
        if (nullable.HasValue)
        {
          nullable = adj.CuryWhTaxBal;
          if (nullable.HasValue)
            goto label_11;
        }
      }
      this.CalcBalancesFromAdjustedDocument(e.Row, false, this.DiscOnDiscDate);
label_11:
      nullable = adj.CuryDocBal;
      if (!nullable.HasValue)
      {
        e.Cache.RaiseExceptionHandling<APAdjust.adjdRefNbr>((object) adj, (object) adj.AdjdRefNbr, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APAdjust.adjdRefNbr>(e.Cache)
        }));
      }
      else
      {
        nullable = adj.CuryOrigDocAmt;
        Decimal num1 = 0M;
        Sign sign = nullable.GetValueOrDefault() < num1 & nullable.HasValue ? Sign.Minus : Sign.Plus;
        int? voidAdjNbr = adj.VoidAdjNbr;
        if (!voidAdjNbr.HasValue && Sign.op_Multiply((Decimal) e.NewValue, sign) < 0M)
          throw new PXSetPropertyException(Sign.op_Equality(sign, Sign.Plus) ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        voidAdjNbr = adj.VoidAdjNbr;
        if (voidAdjNbr.HasValue && Sign.op_Multiply((Decimal) e.NewValue, sign) > 0M)
          throw new PXSetPropertyException(Sign.op_Equality(sign, Sign.Plus) ? "Incorrect value. The value to be entered must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        nullable = adj.CuryDocBal;
        Decimal num2 = nullable.Value;
        nullable = adj.CuryAdjgAmt;
        Decimal num3 = nullable.Value;
        if (Sign.op_Multiply(num2 + num3 - (Decimal) e.NewValue, sign) < 0M)
        {
          string format = Sign.op_Equality(sign, Sign.Plus) ? "The amount must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.";
          object[] objArray = new object[1];
          nullable = adj.CuryDocBal;
          Decimal num4 = nullable.Value;
          nullable = adj.CuryAdjgAmt;
          Decimal num5 = nullable.Value;
          objArray[0] = (object) (num4 + num5).ToString();
          throw new PXSetPropertyException(format, objArray);
        }
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<APAdjust, APAdjust.curyAdjgAmt> e)
    {
      this.CalcBalancesFromAdjustedDocument(e.Row, true, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgPPDAmt> e)
    {
      APAdjust row = e.Row;
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue || !row.CuryWhTaxBal.HasValue)
        this.CalcBalancesFromAdjustedDocument(e.Row, false, this.DiscOnDiscDate);
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue)
      {
        e.Cache.RaiseExceptionHandling<APAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APAdjust.adjdRefNbr>(e.Cache)
        }));
      }
      else
      {
        Decimal? nullable1 = row.CuryOrigDocAmt;
        Decimal num1 = 0M;
        Sign sign = nullable1.GetValueOrDefault() < num1 & nullable1.HasValue ? Sign.Minus : Sign.Plus;
        if (!row.VoidAdjNbr.HasValue && Sign.op_Multiply(sign, System.Math.Sign((Decimal) e.NewValue)) < 0 || row.VoidAdjNbr.HasValue && Sign.op_Multiply(sign, System.Math.Sign((Decimal) e.NewValue)) > 0)
          throw new PXSetPropertyException((Decimal) e.NewValue < 0M ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.", PXErrorLevel.Undefined);
        nullable1 = row.CuryDiscBal;
        Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable1 = row.CuryAdjgPPDAmt;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        Decimal num2 = valueOrDefault1 + valueOrDefault2;
        if (!row.VoidAdjNbr.HasValue)
        {
          nullable1 = row.CuryDiscBal;
          Decimal num3 = 0M;
          if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
          {
            nullable1 = row.CuryDiscBal;
            if (System.Math.Sign(nullable1.Value) * System.Math.Sign((Decimal) e.NewValue) <= 0)
              goto label_11;
          }
          if (System.Math.Abs(num2) < System.Math.Abs((Decimal) e.NewValue))
            throw new PXSetPropertyException("The amount entered exceeds the remaining cash discount balance ({0}).", new object[1]
            {
              (object) num2.ToString()
            });
        }
label_11:
        nullable1 = row.CuryAdjgAmt;
        if (nullable1.HasValue)
        {
          Decimal? nullable2;
          if (e.Cache.GetValuePending<APAdjust.curyAdjgAmt>((object) e.Row) != PXCache.NotSetValue)
          {
            nullable1 = (Decimal?) e.Cache.GetValuePending<APAdjust.curyAdjgAmt>((object) e.Row);
            nullable2 = row.CuryAdjgAmt;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
              goto label_16;
          }
          nullable2 = row.CuryDocBal;
          Decimal num4 = System.Math.Abs(nullable2.Value);
          nullable2 = row.CuryAdjgPPDAmt;
          Decimal num5 = System.Math.Abs(nullable2.Value);
          if (num4 + num5 < System.Math.Abs((Decimal) e.NewValue))
          {
            object[] objArray = new object[1];
            nullable2 = row.CuryDocBal;
            Decimal num6 = nullable2.Value;
            nullable2 = row.CuryAdjgPPDAmt;
            Decimal num7 = nullable2.Value;
            objArray[0] = (object) (num6 + num7).ToString();
            throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
          }
        }
label_16:
        if (row.AdjdHasPPDTaxes.GetValueOrDefault() && row.AdjgDocType == "ADR")
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<APAdjust, APAdjust.curyAdjgWhTaxAmt> e)
    {
      APAdjust row = e.Row;
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue || !row.CuryWhTaxBal.HasValue)
        this.CalcBalancesFromAdjustedDocument(e.Row, false, this.DiscOnDiscDate);
      if (!row.CuryDocBal.HasValue || !row.CuryWhTaxBal.HasValue)
      {
        e.Cache.RaiseExceptionHandling<APAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APAdjust.adjdRefNbr>(e.Cache)
        }));
      }
      else
      {
        if (!row.VoidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        if (row.VoidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        Decimal num1 = row.CuryWhTaxBal.Value;
        Decimal? nullable1 = row.CuryAdjgWhTaxAmt;
        Decimal num2 = nullable1.Value;
        if (num1 + num2 - (Decimal) e.NewValue < 0M)
        {
          object[] objArray = new object[1];
          nullable1 = row.CuryWhTaxBal;
          Decimal num3 = nullable1.Value;
          nullable1 = row.CuryAdjgWhTaxAmt;
          Decimal num4 = nullable1.Value;
          objArray[0] = (object) (num3 + num4).ToString();
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
        nullable1 = row.CuryAdjgAmt;
        if (!nullable1.HasValue)
          return;
        Decimal? nullable2;
        if (e.Cache.GetValuePending<APAdjust.curyAdjgAmt>((object) e.Row) != PXCache.NotSetValue)
        {
          nullable1 = (Decimal?) e.Cache.GetValuePending<APAdjust.curyAdjgAmt>((object) e.Row);
          nullable2 = row.CuryAdjgAmt;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            return;
        }
        nullable2 = row.CuryDocBal;
        Decimal num5 = nullable2.Value;
        nullable2 = row.CuryAdjgWhTaxAmt;
        Decimal num6 = nullable2.Value;
        if (num5 + num6 - (Decimal) e.NewValue < 0M)
        {
          object[] objArray = new object[1];
          nullable2 = row.CuryDocBal;
          Decimal num7 = nullable2.Value;
          nullable2 = row.CuryAdjgWhTaxAmt;
          Decimal num8 = nullable2.Value;
          objArray[0] = (object) (num7 + num8).ToString();
          throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
        }
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<APAdjust, APAdjust.curyAdjgWhTaxAmt> e)
    {
      this.CalcBalancesFromAdjustedDocument(e.Row, true, this.DiscOnDiscDate);
    }
  }
}
