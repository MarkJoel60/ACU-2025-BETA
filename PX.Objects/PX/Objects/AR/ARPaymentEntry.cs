// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.BQL;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.Repositories;
using PX.Objects.AR.Standalone;
using PX.Objects.BQLConstants;
using PX.Objects.CA;
using PX.Objects.CC.Common;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.Common.Interfaces;
using PX.Objects.Common.Scopes;
using PX.Objects.Common.Utility;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AR;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARPaymentEntry : ARDataEntryGraph<
#nullable disable
ARPaymentEntry, ARPayment>
{
  public PXWorkflowEventHandler<ARPayment> OnReleaseDocument;
  public PXWorkflowEventHandler<ARPayment> OnOpenDocument;
  public PXWorkflowEventHandler<ARPayment> OnCloseDocument;
  public PXWorkflowEventHandler<ARPayment> OnVoidDocument;
  public PXWorkflowEventHandler<ARPayment> OnUpdateStatus;
  /// <summary>
  /// Necessary for proper cache resolution inside selector
  /// on <see cref="P:PX.Objects.AR.ARAdjust.DisplayRefNbr" />.
  /// </summary>
  public PXSelect<PX.Objects.AR.Standalone.ARRegister> dummy_register;
  /// <summary>
  /// Necessary for proper cache resolution inside selector (Compliance Tab)
  /// </summary>
  public PXSelect<PX.Objects.AP.Vendor> dummyVendor;
  [PXViewName("AR Payment")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (ARPayment.extRefNbr), typeof (ARPayment.clearDate), typeof (ARPayment.cleared), typeof (ARPayment.cCTransactionRefund), typeof (ARPayment.refTranExtNbr), typeof (ARPayment.pMInstanceID)})]
  public PXSelectJoin<ARPayment, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>>, Where<ARPayment.docType, Equal<Optional<ARPayment.docType>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>> Document;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (ARPayment.clearDate), typeof (ARPayment.cleared)})]
  public PXSelect<ARPayment, Where<ARPayment.docType, Equal<Current<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Current<ARPayment.refNbr>>>>> CurrentDocument;
  [PXViewName("Documents to Apply")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARAdjust, LeftJoin<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>, And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARAdjust.adjdDocType>, And<ARTran.refNbr, Equal<ARAdjust.adjdRefNbr>, And<ARTran.lineNbr, Equal<ARAdjust.adjdLineNbr>>>>>>>>, Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>> Adjustments;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.AR.Standalone.ARAdjust, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdDocType>, And<ARRegisterAlias.refNbr, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdRefNbr>>>, LeftJoin<ARTran, On<ARTran.tranType, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdDocType>, And<ARTran.refNbr, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdRefNbr>, And<ARTran.lineNbr, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdLineNbr>>>>, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.Standalone.ARAdjust.adjdCuryInfoID>>, LeftJoin<CurrencyInfo2, On<CurrencyInfo2.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>>>>>>, Where<PX.Objects.AR.Standalone.ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<PX.Objects.AR.Standalone.ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<PX.Objects.AR.Standalone.ARAdjust.released, Equal<Required<PX.Objects.AR.Standalone.ARAdjust.released>>>>>> Adjustments_Balance;
  public PXSelect<ARAdjust, Where<ARAdjust.adjgDocType, Equal<Optional<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Optional<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>> Adjustments_Raw;
  public PXSelectJoin<ARAdjust, InnerJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>, And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoin<ARTran, On<ARTran.tranType, Equal<ARAdjust.adjdDocType>, And<ARTran.refNbr, Equal<ARAdjust.adjdRefNbr>, And<ARTran.lineNbr, Equal<ARAdjust.adjdLineNbr>>>>>>>, Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>> Adjustments_Invoices;
  public PXSelectJoin<ARAdjust, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARAdjust.adjdDocType>, And<ARPayment.refNbr, Equal<ARAdjust.adjdRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>, And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>>>, Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>> Adjustments_Payments;
  [PXViewName("Application History")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARTranPostBal, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARTranPostBal.sourceDocType>, And<ARInvoice.refNbr, Equal<ARTranPostBal.sourceRefNbr>>>, LeftJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARTranPostBal.sourceDocType>, And<ARRegisterAlias.refNbr, Equal<ARTranPostBal.sourceRefNbr>>>, LeftJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, LeftJoin<CurrencyInfo2, On<CurrencyInfo2.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>, LeftJoin<ARTran, On<ARTran.tranType, Equal<ARTranPostBal.sourceDocType>, And<ARTran.refNbr, Equal<ARTranPostBal.sourceRefNbr>, And<ARTran.lineNbr, Equal<ARTranPostBal.lineNbr>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.noteID, Equal<ARTranPostBal.refNoteID>>>>>>>>, Where<ARTranPostBal.docType, Equal<Current<ARPayment.docType>>, And<ARTranPostBal.refNbr, Equal<Current<ARPayment.refNbr>>, And<ARTranPostBal.type, NotEqual<ARTranPost.type.origin>, And<ARTranPostBal.type, NotEqual<ARTranPost.type.rgol>, And<ARTranPostBal.type, NotEqual<ARTranPost.type.retainageReverse>, And<ARTranPostBal.type, NotEqual<ARTranPost.type.retainage>>>>>>>, OrderBy<Asc<ARTranPostBal.iD>>> ARPost;
  public PXSetup<ARSetup> arsetup;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>> currencyinfo;
  [PXReadOnlyView]
  public PXSelect<CATran, Where<CATran.tranID, Equal<Current<ARPayment.cATranID>>>> dummy_CATran;
  public PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>> CurrentBranch;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Optional<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.lineNbr, Equal<Required<ARAdjust.adjdLineNbr>>>>>>>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>> ARInvoice_DocType_RefNbr;
  public PXSelectReadonly2<ARPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARPayment.curyInfoID>>>, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>> ARPayment_DocType_RefNbr;
  public PXSelect<PX.Objects.AR.Standalone.ARRegister, Where<PX.Objects.AR.Standalone.ARRegister.released, NotEqual<True>, And<Exists<Select<ARAdjust, Where<PX.Objects.AR.Standalone.ARRegister.docType, Equal<ARAdjust.adjdDocType>, And<PX.Objects.AR.Standalone.ARRegister.refNbr, Equal<ARAdjust.adjdRefNbr>, And<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>>>>>>>> Adjustments_Invoices_Unreleased;
  [PXViewName("Customer")]
  public PXSetup<Customer, Where<Customer.bAccountID, Equal<Optional<ARPayment.customerID>>>> customer;
  [PXViewName("Customer Location")]
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARPayment.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<ARPayment.customerLocationID>>>>> location;
  public PXSetup<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>> customerclass;
  public PXSelect<ARBalances> arbalances;
  [PXViewName("Cash Account")]
  public PXSetup<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<ARPayment.cashAccountID>>>> cashaccount;
  public PXSetup<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Optional<ARPayment.adjFinPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<ARPayment.branchID>>>>> finperiod;
  public PXSetup<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>>> paymentmethod;
  public PXSetup<CCProcessingCenter>.Where<BqlOperand<
  #nullable enable
  CCProcessingCenter.processingCenterID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARPayment.processingCenterID, IBqlString>.FromCurrent>> processingCenter;
  public 
  #nullable disable
  PXSetup<GLSetup> glsetup;
  public ARPaymentChargeSelect<ARPayment, ARPayment.paymentMethodID, ARPayment.cashAccountID, ARPayment.docDate, ARPayment.tranPeriodID, ARPayment.pMInstanceID, Where<ARPaymentChargeTran.docType, Equal<Current<ARPayment.docType>>, And<ARPaymentChargeTran.refNbr, Equal<Current<ARPayment.refNbr>>>>> PaymentCharges;
  public PXFilter<ARPaymentEntry.LoadOptions> loadOpts;
  public PXSelect<ARSetupApproval, Where<Current<ARPayment.docType>, Equal<ARDocType.refund>, And<ARSetupApproval.docType, Equal<ARDocType.refund>>>> SetupApproval;
  public PXSelect<ExternalTransaction, Where2<Where<ExternalTransaction.refNbr, Equal<Current<ARPayment.refNbr>>, And<Where<ExternalTransaction.docType, Equal<Current<ARPayment.docType>>, Or2<Where<Current<ARPayment.docType>, In3<ARDocType.payment, ARDocType.voidPayment>, And<ExternalTransaction.docType, In3<ARDocType.payment, ARDocType.voidPayment>>>, Or2<Where<Current<ARPayment.docType>, In3<ARDocType.prepayment, ARDocType.voidPayment>>, And<ExternalTransaction.docType, In3<ARDocType.prepayment, ARDocType.voidPayment>>>>>>>, Or<Where<Current<ARPayment.docType>, Equal<ARDocType.refund>, And<ExternalTransaction.voidDocType, Equal<Current<ARPayment.docType>>, And<ExternalTransaction.voidRefNbr, Equal<Current<ARPayment.refNbr>>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>> ExternalTran;
  public PXSelect<CCBatchTransaction, Where<CCBatchTransaction.refNbr, Equal<Current<ARPayment.refNbr>>, And<CCBatchTransaction.docType, Equal<Current<ARPayment.docType>>>>> BatchTran;
  public PXSelect<CCProcessingCenterPmntMethod, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<CCProcessingCenterPmntMethod.paymentMethodID>>, And<CCProcessingCenterPmntMethod.processingCenterID, Equal<Current<CCProcessingCenterPmntMethod.processingCenterID>>>>> ProcessingCenterPmntMethod;
  [PXViewName("Credit Card Processing Info")]
  public PXSelectOrderBy<CCProcTran, OrderBy<Desc<CCProcTran.tranNbr>>> ccProcTran;
  [PXCopyPasteHiddenView]
  public PXSelect<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<ARPayment.docType>>, And<ARPaymentTotals.refNbr, Equal<Current<ARPayment.refNbr>>>>> PaymentTotals;
  public static string[] AdjgDocTypesToValidateFinPeriod = new string[4]
  {
    "PMT",
    "CRM",
    "PPM",
    "REF"
  };
  [PXViewName("Approval")]
  public EPApprovalAutomationWithReservedDoc<ARPayment, ARRegister.approved, ARRegister.rejected, ARPayment.hold, ARSetupApproval> Approval;
  public PXAction<ARPayment> newCustomer;
  public PXAction<ARPayment> editCustomer;
  public PXAction<ARPayment> customerDocuments;
  public PXAction<ARPayment> viewPPDVATAdj;
  public PXAction<ARPayment> refund;
  public PXAction<ARPayment> loadInvoices;
  public PXAction<ARPayment> autoApply;
  public PXAction<ARPayment> adjustDocAmt;
  public PXAction<ARPayment> reverseApplication;
  public PXAction<ARPayment> viewDocumentToApply;
  public PXAction<ARPayment> viewApplicationDocument;
  public PXAction<ARPayment> viewCurrentBatch;
  public PXAction<ARPayment> ViewOriginalDocument;
  public bool internalCall;
  protected Dictionary<ARAdjust, PXResultset<ARInvoice>> balanceCache;
  private bool _IsVoidCheckInProgress;

  private bool AppendAdjustmentsInvoicesTail(
    ARAdjust adj,
    PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran> invoicesResult)
  {
    ((PXSelectBase<ARAdjust>) this.Adjustments_Invoices).StoreTailResult((PXResult) new PXResult<ARInvoice, ARRegisterAlias, ARTran>(PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(invoicesResult), PX.Objects.Common.Utilities.Clone<ARInvoice, ARRegisterAlias>((PXGraph) this, PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(invoicesResult)), PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(invoicesResult)), (object[]) new ARAdjust[1]
    {
      adj
    }, new object[2]
    {
      (object) ((PXSelectBase<ARPayment>) this.Document).Current.DocType,
      (object) ((PXSelectBase<ARPayment>) this.Document).Current.RefNbr
    });
    ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(invoicesResult);
    PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.lineNbr, Equal<Required<ARAdjust.adjdLineNbr>>>>>>>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>> invoiceDocTypeRefNbr = this.ARInvoice_DocType_RefNbr;
    List<object> objectList = new List<object>();
    objectList.Add((object) invoicesResult);
    PXQueryParameters pxQueryParameters = PXQueryParameters.ExplicitParameters(new object[3]
    {
      (object) adj.AdjdLineNbr.GetValueOrDefault(),
      (object) arInvoice.DocType,
      (object) arInvoice.RefNbr
    });
    ((PXSelectBase<ARInvoice>) invoiceDocTypeRefNbr).StoreResult(objectList, pxQueryParameters);
    ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).StoreTailResult((PXResult) new PXResult<PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>(PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(invoicesResult), PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(invoicesResult), PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(invoicesResult)), new object[1]
    {
      (object) arInvoice
    }, new object[3]
    {
      (object) adj.AdjdLineNbr.GetValueOrDefault(),
      (object) arInvoice.DocType,
      (object) arInvoice.RefNbr
    });
    return true;
  }

  private void Adjustments_Invoices_BeforeSelect()
  {
    if (NonGenericIEnumerableExtensions.Empty_(((PXSelectBase) this.Adjustments_Raw).Cache.Inserted))
      return;
    IEnumerable<ARAdjust> source = ((PXSelectBase) this.Adjustments_Raw).Cache.Inserted.OfType<ARAdjust>();
    ARPaymentEntry.LoadOptions opts = new ARPaymentEntry.LoadOptions()
    {
      StartRefNbr = source.Select<ARAdjust, string>((Func<ARAdjust, string>) (_ => _.AdjdRefNbr)).Min<string>(),
      EndRefNbr = source.Select<ARAdjust, string>((Func<ARAdjust, string>) (_ => _.AdjdRefNbr)).Max<string>(),
      FromDate = source.Select<ARAdjust, DateTime?>((Func<ARAdjust, DateTime?>) (_ => _.AdjdDocDate)).Min<DateTime?>(),
      TillDate = source.Select<ARAdjust, DateTime?>((Func<ARAdjust, DateTime?>) (_ => _.AdjdDocDate)).Max<DateTime?>()
    };
    ARPaymentEntry.ARPaymentEntryCustomerDocsExtension extension = ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryCustomerDocsExtension>();
    ((PXSelectBase) this.Adjustments_Raw).Cache.Cached.OfType<ARAdjust>().Join<ARAdjust, PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>, string, bool>(((IEnumerable<PXResult<ARInvoice>>) extension.GetCustDocs(opts, ((PXSelectBase<ARPayment>) this.Document).Current, ((PXSelectBase<ARSetup>) this.arsetup).Current)).AsEnumerable<PXResult<ARInvoice>>().Cast<PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>>(), (Func<ARAdjust, string>) (_ => _.AdjdDocType + _.AdjdRefNbr + _.AdjdLineNbr.ToString()), (Func<PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>, string>) (_ => ((PXResult) _).GetItem<ARInvoice>().DocType + ((PXResult) _).GetItem<ARInvoice>().RefNbr + ((PXResult) _).GetItem<ARTran>().LineNbr.GetValueOrDefault().ToString()), new Func<ARAdjust, PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>, bool>(this.AppendAdjustmentsInvoicesTail)).ToArray<bool>();
  }

  public IEnumerable CcProcTran()
  {
    ARPaymentEntry graph = this;
    PXResultset<ExternalTransaction> pxResultset = ((PXSelectBase<ExternalTransaction>) graph.ExternalTran).Select(Array.Empty<object>());
    PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>> query = new PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>>((PXGraph) graph);
    foreach (PXResult<ExternalTransaction> pxResult1 in pxResultset)
    {
      ExternalTransaction extTran = PXResult<ExternalTransaction>.op_Implicit(pxResult1);
      PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>> pxSelect = query;
      object[] objArray = new object[1]
      {
        (object) extTran.TransactionID
      };
      foreach (PXResult<CCProcTran> pxResult2 in ((PXSelectBase<CCProcTran>) pxSelect).Select(objArray))
      {
        CCProcTran ccProcTran = PXResult<CCProcTran>.op_Implicit(pxResult2);
        ccProcTran.MaskedCardNumber = !string.IsNullOrEmpty(extTran.CardType) ? ((PXGraph) graph).GetService<ICCDisplayMaskService>().UseDefaultMaskForCardNumber(extTran.LastDigits ?? string.Empty, CardType.GetDisplayName(extTran.CardType), new MeansOfPayment?(CardType.GetMeansOfPayment(extTran.CardType))) : string.Empty;
        yield return (object) ccProcTran;
      }
      extTran = (ExternalTransaction) null;
    }
  }

  public OrdersToApplyTab GetOrdersToApplyTabExtension(bool throwException = false)
  {
    OrdersToApplyTab implementation = ((PXGraph) this).FindImplementation<OrdersToApplyTab>();
    return !(implementation == null & throwException) ? implementation : throw new PXException("'{0}' cannot be found in the system.", new object[1]
    {
      (object) "OrdersToApplyTab"
    });
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (ARPaymentType.ListExAttribute))]
  [ARPaymentType.List]
  protected virtual void ARPayment_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Original Document")]
  protected virtual void ARPayment_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  protected virtual void _(PX.Data.Events.CacheAttached<ARPayment.curySOApplAmt> e)
  {
  }

  [PXDBTimestamp]
  protected virtual void CATran_tstamp_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [BatchNbrExt(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (ARAdjust.isMigratedRecord))]
  protected virtual void ARAdjust_AdjBatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (Switch<Case<Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>, ARAdjust.adjType.adjusted>, ARAdjust.adjType.adjusting>))]
  protected virtual void ARAdjust_AdjType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void ARAdjust_DisplayRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARAdjust.adjdDocType>>, And<ARInvoice.refNbr, Equal<Current<ARAdjust.adjdRefNbr>>>>>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.curyAdjdAmt, NotEqual<decimal0>, And<ARAdjust.paymentPendingProcessing, Equal<True>, And<ARAdjust.paymentCaptureFailed, NotEqual<True>>>>, int1>, int0>), typeof (SumCalc<ARInvoice.pendingProcessingCntr>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.curyAdjdAmt, NotEqual<decimal0>, And<ARAdjust.paymentCaptureFailed, Equal<True>>>, int1>, int0>), typeof (SumCalc<ARInvoice.captureFailedCntr>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.curyAdjdAmt, Greater<decimal0>, And<ARAdjust.isCCAuthorized, Equal<True>, And<ARAdjust.isCCCaptured, Equal<False>>>>, int1>, int0>), typeof (SumCalc<ARInvoice.authorizedPaymentCntr>), ForceAggregateRecalculation = true)]
  [PXRestrictor(typeof (Where<PX.Objects.SO.SOInvoice.refNbr, IsNull, Or<PX.Objects.SO.SOInvoice.released, Equal<True>>>), "The {0} document with the {1} number cannot be selected for payment because it is not released yet.", new System.Type[] {typeof (PX.Objects.SO.SOInvoice.docType), typeof (PX.Objects.SO.SOInvoice.refNbr)}, SuppressVerify = true)]
  protected virtual void ARAdjust_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [ARDocType.List]
  [PXRemoveBaseAttribute(typeof (PXStringListExtAttribute))]
  protected virtual void ARAdjust_DisplayDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.voided, Equal<False>>, ARAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<ARInvoice.curyPaymentTotal>))]
  protected virtual void ARAdjust_CuryAdjdAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Mult<ARAdjust.adjgBalSign, ARAdjust.curyAdjgAmt>), typeof (SumCalc<ARPayment.curyApplAmt>))]
  protected virtual void ARAdjust_CuryAdjgAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.voided, Equal<False>>, ARAdjust.curyAdjdDiscAmt>, decimal0>), typeof (SumCalc<ARInvoice.curyDiscAppliedAmt>))]
  protected virtual void ARAdjust_CuryAdjdDiscAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.voided, Equal<False>>, ARAdjust.curyAdjdWOAmt>, decimal0>), typeof (SumCalc<ARInvoice.curyBalanceWOTotal>))]
  protected virtual void ARAdjust_CuryAdjdWOAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.adjAmt), BaseCalc = false)]
  protected void _(PX.Data.Events.CacheAttached<ARAdjust.displayCuryAmt> e)
  {
  }

  [PXRemoveBaseAttribute(typeof (ARInvoiceNbrAttribute))]
  protected virtual void ARInvoice_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSearchableAttribute))]
  [PXNote]
  protected virtual void ARInvoice_NoteID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [Account]
  protected virtual void ARInvoice_ARAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [Account]
  protected virtual void ARInvoice_RetainageAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCurrency(typeof (ARInvoice.curyInfoID), typeof (ARInvoice.applicationBalance), BaseCalc = false)]
  protected virtual void ARInvoice_CuryApplicationBalance_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (ARPayment.docDate))]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (ARPayment.customerID))]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (ARRegister.docDesc))]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARPayment.curyInfoID))]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (ARPayment.curyOrigDocAmt))]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (ARPayment.origDocAmt))]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<ARAdjust.ARInvoice.refNbr, Where<ARAdjust.ARInvoice.docType, Equal<Current<ARPaymentTotals.adjdDocType>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARPaymentTotals.adjdRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Doc. Type")]
  protected virtual void ARTranPostBal_SourceDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Reference Nbr.")]
  protected virtual void ARTranPostBal_SourceRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Amount Paid")]
  protected virtual void ARTranPostBal_CuryAmt_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_SourceItemType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARPayment>) this.Document).Current == null)
      return;
    e.NewValue = (object) new ARDocType.ListAttribute().ValueLabelDic[((PXSelectBase<ARPayment>) this.Document).Current.DocType];
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARPayment>) this.Document).Current == null)
      return;
    e.NewValue = (object) EPApprovalHelper.BuildEPApprovalDetailsString(sender, (IApprovalDescription) ((PXSelectBase<ARPayment>) this.Document).Current);
  }

  public bool AutoPaymentApp { get; set; }

  public bool IsReverseProc { get; set; }

  public bool HasUnreleasedSOInvoice { get; set; }

  public bool ForcePaymentApp { get; set; }

  public bool IgnoreNegativeOrderBal { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable NewCustomer(PXAdapter adapter)
  {
    throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<CustomerMaint>(), "New Customer");
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable EditCustomer(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.customer).Current != null)
    {
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<Customer>) instance.BAccount).Current = ((PXSelectBase<Customer>) this.customer).Current;
      throw new PXRedirectRequiredException((PXGraph) instance, "Edit Customer");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CustomerDocuments(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.customer).Current != null)
    {
      ARDocumentEnq instance = PXGraph.CreateInstance<ARDocumentEnq>();
      ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) instance.Filter).Current.CustomerID = ((PXSelectBase<Customer>) this.customer).Current.BAccountID;
      ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) instance.Filter).Select(Array.Empty<object>());
      throw new PXRedirectRequiredException((PXGraph) instance, "Customer Details");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewPPDVATAdj(PXAdapter adapter)
  {
    ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.noteID, Equal<Required<ARTranPostBal.refNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<ARTranPostBal>) this.ARPost).Current.RefNoteID
    }));
    if (arAdjust != null)
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) arAdjust.PPDVATAdjRefNbr, new object[1]
      {
        (object) arAdjust.PPDVATAdjDocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARPaymentEntry.\u003C\u003Ec__DisplayClass121_0 displayClass1210 = new ARPaymentEntry.\u003C\u003Ec__DisplayClass121_0();
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    // ISSUE: reference to a compiler-generated field
    displayClass1210.list = new List<ARRegister>();
    foreach (ARPayment arPayment in adapter.Get<ARPayment>())
    {
      if (!arPayment.Hold.Value)
      {
        // ISSUE: reference to a compiler-generated field
        displayClass1210.list.Add((ARRegister) cache.Update((object) arPayment) ?? (ARRegister) arPayment);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (displayClass1210.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1210, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1210.list;
  }

  protected virtual bool AskUserApprovalToVoidPayment(ARPayment payment)
  {
    return !payment.Deposited.GetValueOrDefault() || ((PXSelectBase) this.Document).View.Ask(PXMessages.LocalizeNoPrefix("The payment has already been deposited. To proceed, click OK."), (MessageButtons) 1) == 1;
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable VoidCheck(PXAdapter adapter)
  {
    ARPayment current1 = ((PXSelectBase<ARPayment>) this.Document).Current;
    if (current1 == null)
      return adapter.Get();
    bool? nullable1;
    if (current1 != null)
    {
      nullable1 = current1.Released;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = current1.Voided;
        bool flag1 = false;
        if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue && ARPaymentType.VoidEnabled(current1))
        {
          ARAdjust adj = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjgDocType, Equal<ARDocType.refund>, And<ARAdjust.voided, NotEqual<True>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
          {
            (object) current1.DocType,
            (object) current1.RefNbr
          }));
          if (adj != null && !adj.IsSelfAdjustment())
            throw new PXException("{0} {1} has been refunded with {2} {3}.", new object[4]
            {
              (object) GetLabel.For<ARDocType>(current1.DocType),
              (object) ((PXSelectBase<ARPayment>) this.Document).Current.RefNbr,
              (object) GetLabel.For<ARDocType>(adj.AdjgDocType),
              (object) adj.AdjgRefNbr
            });
          nullable1 = ((PXSelectBase<ARSetup>) this.arsetup).Current.MigrationMode;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = current1.IsMigratedRecord;
            if (nullable1.GetValueOrDefault())
            {
              Decimal? curyInitDocBal = current1.CuryInitDocBal;
              Decimal? curyOrigDocAmt = current1.CuryOrigDocAmt;
              if (!(curyInitDocBal.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & curyInitDocBal.HasValue == curyOrigDocAmt.HasValue))
                throw new PXException("The document cannot be processed because it was created when migration mode was activated. To process the document, activate migration mode on the Accounts Receivable Preferences (AR101000) form.");
            }
          }
          nullable1 = ((PXSelectBase<ARSetup>) this.arsetup).Current.MigrationMode;
          if (nullable1.GetValueOrDefault())
          {
            nullable1 = current1.IsMigratedRecord;
            if (nullable1.GetValueOrDefault() && GraphHelper.RowCast<ARAdjust>((IEnumerable) PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.isMigratedRecord, NotEqual<True>, And<ARAdjust.isInitialApplication, NotEqual<True>>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<ARAdjust>((Func<ARAdjust, bool>) (_ => !_.VoidAppl.GetValueOrDefault())))
              throw new PXException("The payment cannot be voided because it has unreversed regular applications.");
          }
          if (!ARDocType.IsSelfVoiding(current1.DocType))
          {
            ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) this.Document).Search<ARPayment.refNbr>((object) current1.RefNbr, new object[1]
            {
              (object) ARPaymentType.GetVoidingARDocType(current1.DocType)
            }));
            if (arPayment != null)
            {
              ((PXSelectBase<ARPayment>) this.Document).Current = arPayment;
              if (!((PXGraph) this).IsContractBasedAPI && !((PXGraph) this).IsImport && !FlaggedModeScopeBase<SettlementProcessScope, string>.IsActive)
                throw new PXRedirectRequiredException((PXGraph) this, "Voided");
              return (IEnumerable) new ARPayment[1]
              {
                ((PXSelectBase<ARPayment>) this.Document).Current
              };
            }
          }
          if (!FlaggedModeScopeBase<SettlementProcessScope, string>.IsActive && !this.AskUserApprovalToVoidPayment(current1))
            return adapter.Get();
          this.CheckDocumentBeforeVoiding((PXGraph) this, current1);
          bool flag2 = false;
          foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) this.Adjustments_Raw).Select(Array.Empty<object>()))
          {
            ((PXSelectBase) this.Adjustments).Cache.Delete((object) PXResult<ARAdjust>.op_Implicit(pxResult));
            flag2 = true;
          }
          bool? nullable2;
          if (!flag2)
          {
            nullable2 = current1.OpenDoc;
            if (nullable2.GetValueOrDefault() && PXUIFieldAttribute.GetError<ARPayment.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) current1) != null)
              ((PXSelectBase) this.Document).Cache.SetStatus((object) current1, (PXEntryStatus) 0);
          }
          ((PXAction) this.Save).Press();
          ARPayment copy = PXCache<ARPayment>.CreateCopy(current1);
          copy.NoteID = new Guid?();
          nullable2 = copy.SelfVoidingDoc;
          if (!nullable2.GetValueOrDefault())
          {
            if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current != null)
            {
              nullable2 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current.ARDefaultVoidDateToDocumentDate;
              if (nullable2.GetValueOrDefault())
                goto label_35;
            }
            ARPayment arPayment = copy;
            DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
            DateTime? docDate = current1.DocDate;
            DateTime? nullable3 = (businessDate.HasValue & docDate.HasValue ? (businessDate.GetValueOrDefault() > docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? ((PXGraph) this).Accessinfo.BusinessDate : current1.DocDate;
            arPayment.DocDate = nullable3;
            string periodIdFromDate = this.FinPeriodRepository.GetPeriodIDFromDate(copy.DocDate, PXAccess.GetParentOrganizationID(copy.BranchID));
            string str = current1.FinPeriodID.CompareTo(periodIdFromDate) > 0 ? current1.FinPeriodID : periodIdFromDate;
            ((PXSelectBase) this.Document).Cache.SetValue<ARPayment.adjFinPeriodID>((object) copy, (object) str);
            FinPeriodIDAttribute.SetMasterPeriodID<ARPayment.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy);
            ((PXSelectBase) this.finperiod).Cache.Current = ((PXSelectBase) this.finperiod).View.SelectSingleBound(new object[1]
            {
              (object) copy
            }, Array.Empty<object>());
          }
label_35:
          PX.Objects.CA.PaymentMethod current2 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current;
          int num;
          if (current2 == null)
          {
            num = 0;
          }
          else
          {
            nullable2 = current2.ARDefaultVoidDateToDocumentDate;
            num = nullable2.GetValueOrDefault() ? 1 : 0;
          }
          if (num != 0)
          {
            copy.AdjFinPeriodID = copy.FinPeriodID;
            copy.AdjTranPeriodID = copy.TranPeriodID;
          }
          nullable2 = copy.DepositAsBatch;
          if (nullable2.GetValueOrDefault() && !string.IsNullOrEmpty(copy.DepositNbr))
          {
            nullable2 = copy.Deposited;
            if (!nullable2.GetValueOrDefault())
            {
              if (copy.DocType == "REF")
                throw new PXException("This refund is included in the {0} bank deposit. It cannot be voided until the deposit is released or the refund is excluded from it.", new object[1]
                {
                  (object) copy.DepositNbr
                });
              if (FlaggedModeScopeBase<SettlementProcessScope, string>.IsActive && FlaggedModeScopeBase<SettlementProcessScope, string>.Parameters == "REJ")
                throw new PXException("This payment is included in the {0} bank deposit. Rejection cannot be recorded until the deposit is released or the payment is excluded from it.", new object[1]
                {
                  (object) copy.DepositNbr
                });
              throw new PXException("This payment is included in the {0} bank deposit. It cannot be voided until the deposit is released or the payment is excluded from it.", new object[1]
              {
                (object) copy.DepositNbr
              });
            }
          }
          this.CheckCreditCardTranStateBeforeVoiding();
          try
          {
            this._IsVoidCheckInProgress = true;
            nullable2 = current1.SelfVoidingDoc;
            if (nullable2.GetValueOrDefault())
              this.SelfVoidingProc(copy);
            else
              this.VoidCheckProc(copy);
          }
          finally
          {
            this._IsVoidCheckInProgress = false;
          }
          ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<ARPayment.finPeriodID>((object) ((PXSelectBase<ARPayment>) this.Document).Current, (object) ((PXSelectBase<ARPayment>) this.Document).Current.FinPeriodID, (Exception) null);
          if (!((PXGraph) this).IsContractBasedAPI && !((PXGraph) this).IsImport && !FlaggedModeScopeBase<SettlementProcessScope, string>.IsActive)
            throw new PXRedirectRequiredException((PXGraph) this, "Voided");
          return (IEnumerable) new ARPayment[1]
          {
            ((PXSelectBase<ARPayment>) this.Document).Current
          };
        }
      }
    }
    nullable1 = current1.Released;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = current1.Voided;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && (current1.DocType == "PMT" || current1.DocType == "PPM") && ExternalTranHelper.HasTransactions((PXSelectBase<ExternalTransaction>) this.ExternalTran))
      {
        ((PXAction) this.Save).Press();
        nullable1 = ((PXSelectBase<ARSetup>) this.arsetup).Current.RequireExtRef;
        if (nullable1.GetValueOrDefault() && string.IsNullOrWhiteSpace(current1.ExtRefNbr))
          throw new PXException("'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName(((PXSelectBase) this.Document).Cache, "ExtRefNbr")
          });
        ARPayment arPayment1 = current1;
        arPayment1.Voided = new bool?(true);
        arPayment1.OpenDoc = new bool?(false);
        arPayment1.PendingProcessing = new bool?(false);
        ARPayment arPayment2 = ((PXSelectBase<ARPayment>) this.Document).Update(arPayment1);
        ((PXGraph) this).Caches[typeof (ARAdjust)].ClearQueryCache();
        foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) this.Adjustments_Raw).Select(Array.Empty<object>()))
        {
          ARAdjust arAdjust = PXResult<ARAdjust>.op_Implicit(pxResult);
          nullable1 = arAdjust.Voided;
          if (!nullable1.GetValueOrDefault())
          {
            ARAdjust copy = (ARAdjust) ((PXGraph) this).Caches[typeof (ARAdjust)].CreateCopy((object) arAdjust);
            copy.Voided = new bool?(true);
            ((PXGraph) this).Caches[typeof (ARAdjust)].Update((object) copy);
          }
        }
        long? nullable4 = arPayment2.CATranID;
        if (nullable4.HasValue && arPayment2.CashAccountID.HasValue)
        {
          CATran caTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) arPayment2.CATranID
          }));
          if (caTran != null)
            ((PXGraph) this).Caches[typeof (CATran)].Delete((object) caTran);
          ARPayment arPayment3 = arPayment2;
          nullable4 = new long?();
          long? nullable5 = nullable4;
          arPayment3.CATranID = nullable5;
        }
        arPayment2.CCReauthTriesLeft = new int?(0);
        arPayment2.CCReauthDate = new DateTime?();
        arPayment2.IsCCUserAttention = new bool?(false);
        ARPayment arPayment4 = ((PXSelectBase<ARPayment>) this.Document).Update(arPayment2);
        ((SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.VoidDocument))).FireOn((PXGraph) this, arPayment4);
        ((PXAction) this.Save).Press();
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable Refund(PXAdapter adapter)
  {
    ARPayment current = ((PXSelectBase<ARPayment>) this.Document).Current;
    if (current == null || current == null)
      return adapter.Get();
    bool? nullable = current.Released;
    if (nullable.GetValueOrDefault())
    {
      nullable = current.Voided;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        this.ValidatePaymentForRefund(current);
        this.ValidatePaymentForVoid(current);
        ((PXAction) this.Save).Press();
        if (!(current.DocType == "CRM") && !(current.DocType == "PPI"))
          return this.CreateRefundFromPayment(current);
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) current.RefNbr, new object[1]
        {
          (object) current.DocType
        }));
        ((PXAction) instance.customerRefund).Press();
      }
    }
    return adapter.Get();
  }

  private IEnumerable CreateRefundFromPayment(ARPayment payment)
  {
    ARPayment copy = PXCache<ARPayment>.CreateCopy(payment);
    copy.NoteID = new Guid?();
    copy.DocDate = ((PXGraph) this).Accessinfo.BusinessDate;
    string periodIdFromDate = this.FinPeriodRepository.GetPeriodIDFromDate(copy.DocDate, new int?(0));
    FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy, periodIdFromDate);
    ((PXSelectBase) this.finperiod).Cache.Current = ((PXSelectBase) this.finperiod).View.SelectSingleBound(new object[1]
    {
      (object) copy
    }, Array.Empty<object>());
    this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<ARPayment.adjFinPeriodID, ARPayment.branchID>(((PXSelectBase) this.Document).Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aRClosed));
    this.RefundCheckProc(copy);
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<ARPayment.finPeriodID>((object) ((PXSelectBase<ARPayment>) this.Document).Current, (object) ((PXSelectBase<ARPayment>) this.Document).Current.FinPeriodID, (Exception) null);
    if (!((PXGraph) this).IsContractBasedAPI)
      throw new PXRedirectRequiredException((PXGraph) this, "Refund");
    return (IEnumerable) new ARPayment[1]
    {
      ((PXSelectBase<ARPayment>) this.Document).Current
    };
  }

  private ARAdjust AddAdjustment(ARAdjust adj)
  {
    Decimal? curyUnappliedBal = ((PXSelectBase<ARPayment>) this.Document).Current.CuryUnappliedBal;
    Decimal num1 = 0M;
    if (curyUnappliedBal.GetValueOrDefault() == num1 & curyUnappliedBal.HasValue)
    {
      Decimal? curyOrigDocAmt = ((PXSelectBase<ARPayment>) this.Document).Current.CuryOrigDocAmt;
      Decimal num2 = 0M;
      if (curyOrigDocAmt.GetValueOrDefault() > num2 & curyOrigDocAmt.HasValue)
        throw new ARPaymentEntry.PXLoadInvoiceException();
    }
    return ((PXSelectBase<ARAdjust>) this.Adjustments).Insert(adj);
  }

  private ARAdjust AddAdjustmentExt(ARAdjust adj)
  {
    return ((PXSelectBase<ARAdjust>) this.Adjustments).Insert(adj);
  }

  /// <summary>
  /// This particular overload of LoadInvoicesProc is being used in ARReleaseProcess.ReleaseDocProc to create applications for available documents when AutoApplyPayments is True
  /// Can be overloaded in extension to pass LoadOptions to use in that particular case
  /// </summary>
  public virtual void LoadInvoicesProc(bool LoadExistingOnly)
  {
    this.LoadInvoicesProc((LoadExistingOnly ? 1 : 0) != 0, new ARPaymentEntry.LoadOptions()
    {
      Apply = new bool?(true)
    });
  }

  private static string ToKeyString(ARAdjust adj)
  {
    return string.Join("_", (object) adj.AdjdDocType, (object) adj.AdjdRefNbr, (object) adj.AdjdLineNbr);
  }

  private static string ToKeyString(ARInvoice invoice, ARTran tran)
  {
    return string.Join("_", (object) invoice.DocType, (object) invoice.RefNbr, (object) ((int?) tran?.LineNbr).GetValueOrDefault());
  }

  public virtual void LoadInvoicesProc(bool LoadExistingOnly, ARPaymentEntry.LoadOptions opts)
  {
    ARPayment current = ((PXSelectBase<ARPayment>) this.Document).Current;
    this.InternalCall = true;
    Decimal num1 = 0M;
    ((PXSelectBase) this.ARInvoice_DocType_RefNbr).Cache.DisableReadItem = true;
    ((PXSelectBase) this.Adjustments).Cache.DisableReadItem = true;
    bool flag1 = opts?.LoadingMode == "R";
    Dictionary<string, ARAdjust> dictionary1 = new Dictionary<string, ARAdjust>();
    try
    {
      if (current != null && current.CustomerID.HasValue)
      {
        bool? openDoc = current.OpenDoc;
        bool flag2 = false;
        if (!(openDoc.GetValueOrDefault() == flag2 & openDoc.HasValue) && (!(current.DocType != "PMT") || !(current.DocType != "PPM") || !(current.DocType != "CRM") || !(current.DocType != "REF")))
        {
          this.CalcApplAmounts(((PXSelectBase) this.Document).Cache, current);
          this.Adjustments_Invoices_BeforeSelect();
          Dictionary<string, ARInvoice> dictionary2 = ((IEnumerable<PXResult<ARAdjust>>) ((PXSelectBase<ARAdjust>) this.Adjustments_Invoices).Select(Array.Empty<object>())).ToDictionary<PXResult<ARAdjust>, string, ARInvoice>((Func<PXResult<ARAdjust>, string>) (_ => ARPaymentEntry.ToKeyString(PXResult<ARAdjust>.op_Implicit(_))), (Func<PXResult<ARAdjust>, ARInvoice>) (_ => ((PXResult) _).GetItem<ARInvoice>()));
          foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) this.Adjustments_Raw).Select(Array.Empty<object>()))
          {
            ARAdjust adj = PXResult<ARAdjust>.op_Implicit(pxResult);
            if (!flag1)
            {
              if (!LoadExistingOnly)
                adj = PXCache<ARAdjust>.CreateCopy(adj);
              dictionary1.Add(ARPaymentEntry.ToKeyString(adj), adj);
            }
            ((PXSelectBase) this.Adjustments).Cache.Delete((object) PXResult<ARAdjust>.op_Implicit(pxResult));
          }
          ARPayment arPayment = current;
          int? adjCntr = arPayment.AdjCntr;
          arPayment.AdjCntr = adjCntr.HasValue ? new int?(adjCntr.GetValueOrDefault() + 1) : new int?();
          GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) current);
          ((PXSelectBase) this.Document).Cache.IsDirty = true;
          this.Adjustments_Invoices_BeforeSelect();
          if (!flag1 | LoadExistingOnly)
          {
            foreach (KeyValuePair<string, ARAdjust> keyValuePair in dictionary1)
            {
              ARAdjust arAdjust1 = keyValuePair.Value;
              ARAdjust arAdjust2 = new ARAdjust();
              arAdjust2.AdjdDocType = arAdjust1.AdjdDocType;
              arAdjust2.AdjdRefNbr = arAdjust1.AdjdRefNbr;
              arAdjust2.AdjdLineNbr = arAdjust1.AdjdLineNbr;
              arAdjust2.CuryAdjgAmt = arAdjust1.CuryAdjgAmt;
              Decimal? nullable1 = arAdjust1.CuryAdjgPPDAmt;
              arAdjust2.CuryAdjgDiscAmt = nullable1.GetValueOrDefault() == 0M ? arAdjust1.CuryAdjgDiscAmt : arAdjust1.CuryAdjgPPDAmt;
              ARAdjust adj = arAdjust2;
              ARInvoice arInvoice;
              if (dictionary2.TryGetValue(keyValuePair.Key, out arInvoice))
                PXSelectorAttribute.StoreResult<ARAdjust.adjdRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) adj, (IBqlTable) arInvoice);
              try
              {
                ARAdjust copy = PXCache<ARAdjust>.CreateCopy(this.AddAdjustmentExt(adj));
                nullable1 = arAdjust1.CuryAdjgDiscAmt;
                Decimal? nullable2;
                int num2;
                if (nullable1.HasValue)
                {
                  nullable1 = arAdjust1.CuryAdjgDiscAmt;
                  Decimal num3 = 0M;
                  if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
                  {
                    nullable2 = arAdjust1.CuryAdjgDiscAmt;
                    nullable1 = copy.CuryAdjgDiscAmt;
                    num2 = nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue ? 1 : 0;
                    goto label_25;
                  }
                }
                nullable1 = arAdjust1.CuryAdjgDiscAmt;
                nullable2 = copy.CuryAdjgDiscAmt;
                num2 = nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue ? 1 : 0;
label_25:
                if (num2 != 0)
                {
                  copy.CuryAdjgDiscAmt = arAdjust1.CuryAdjgDiscAmt;
                  copy.CuryAdjgPPDAmt = arAdjust1.CuryAdjgDiscAmt;
                  copy = PXCache<ARAdjust>.CreateCopy((ARAdjust) ((PXSelectBase) this.Adjustments).Cache.Update((object) copy));
                }
                nullable1 = arAdjust1.CuryAdjgAmt;
                int num4;
                if (nullable1.HasValue)
                {
                  nullable1 = arAdjust1.CuryAdjgAmt;
                  Decimal num5 = 0M;
                  if (nullable1.GetValueOrDefault() < num5 & nullable1.HasValue)
                  {
                    nullable2 = arAdjust1.CuryAdjgAmt;
                    nullable1 = copy.CuryAdjgAmt;
                    num4 = nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue ? 1 : 0;
                    goto label_31;
                  }
                }
                nullable1 = arAdjust1.CuryAdjgAmt;
                nullable2 = copy.CuryAdjgAmt;
                num4 = nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue ? 1 : 0;
label_31:
                if (num4 != 0)
                {
                  copy.CuryAdjgAmt = arAdjust1.CuryAdjgAmt;
                  copy = PXCache<ARAdjust>.CreateCopy((ARAdjust) ((PXSelectBase) this.Adjustments).Cache.Update((object) copy));
                }
                if (arAdjust1.WriteOffReasonCode != null)
                {
                  copy.WriteOffReasonCode = arAdjust1.WriteOffReasonCode;
                  copy.CuryAdjgWOAmt = arAdjust1.CuryAdjgWOAmt;
                  ((PXSelectBase) this.Adjustments).Cache.Update((object) copy);
                }
              }
              catch (PXSetPropertyException ex)
              {
              }
            }
          }
          if (LoadExistingOnly)
            return;
          PXResultset<ARInvoice> custDocs = ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryCustomerDocsExtension>().GetCustDocs(opts, current, ((PXSelectBase<ARSetup>) this.arsetup).Current);
          this.AutoPaymentApp = true;
          foreach (PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran> res in custDocs)
          {
            ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(res);
            ARRegisterAlias arRegisterAlias = PX.Objects.Common.Utilities.Clone<ARInvoice, ARRegisterAlias>((PXGraph) this, arInvoice);
            PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(res);
            ARTran tran = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(res);
            if (!dictionary1.ContainsKey(ARPaymentEntry.ToKeyString(arInvoice, tran)))
            {
              ARAdjust arAdjust = new ARAdjust();
              arAdjust.AdjdDocType = arInvoice.DocType;
              arAdjust.AdjdRefNbr = arInvoice.RefNbr;
              int? nullable3;
              int? nullable4;
              if (tran == null)
              {
                nullable3 = new int?();
                nullable4 = nullable3;
              }
              else
                nullable4 = tran.LineNbr;
              nullable3 = nullable4;
              arAdjust.AdjdLineNbr = new int?(nullable3.GetValueOrDefault());
              arAdjust.AdjgDocType = current.DocType;
              arAdjust.AdjgRefNbr = current.RefNbr;
              arAdjust.AdjNbr = current.AdjCntr;
              arAdjust.CuryAdjgAmt = new Decimal?(0M);
              arAdjust.CuryAdjgDiscAmt = new Decimal?(0M);
              ARAdjust adj = arAdjust;
              PXSelectJoin<ARAdjust, InnerJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>, And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoin<ARTran, On<ARTran.tranType, Equal<ARAdjust.adjdDocType>, And<ARTran.refNbr, Equal<ARAdjust.adjdRefNbr>, And<ARTran.lineNbr, Equal<ARAdjust.adjdLineNbr>>>>>>>, Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>> adjustmentsInvoices = this.Adjustments_Invoices;
              List<object> objectList1 = new List<object>();
              objectList1.Add((object) new PXResult<ARInvoice, ARRegisterAlias, ARTran>(arInvoice, arRegisterAlias, tran));
              object[] objArray1 = new object[4]
              {
                (object) arInvoice,
                (object) arRegisterAlias,
                (object) tran,
                (object) adj
              };
              object[] objArray2 = new object[2]
              {
                (object) adj.AdjgDocType,
                (object) adj.AdjgRefNbr
              };
              ((PXSelectBase<ARAdjust>) adjustmentsInvoices).StoreTailResult(objectList1, objArray1, objArray2);
              PXSelectJoin<ARAdjust, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARAdjust.adjdDocType>, And<ARPayment.refNbr, Equal<ARAdjust.adjdRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>, And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>>>, Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>> adjustmentsPayments = this.Adjustments_Payments;
              List<object> objectList2 = new List<object>();
              objectList2.Add((object) new PXResult<ARPayment, ARRegisterAlias, ARTran>(((PXSelectBase<ARPayment>) this.Document).Current, arRegisterAlias, tran));
              object[] objArray3 = new object[4]
              {
                (object) arInvoice,
                (object) arRegisterAlias,
                (object) tran,
                (object) adj
              };
              object[] objArray4 = new object[2]
              {
                (object) adj.AdjgDocType,
                (object) adj.AdjgRefNbr
              };
              ((PXSelectBase<ARAdjust>) adjustmentsPayments).StoreTailResult(objectList2, objArray3, objArray4);
              this.AddBalanceCache(adj, (PXResult) res);
              this.StoreInvoiceForAdjdRefNbrSelector(arInvoice, adj);
              ARTran arTran = tran;
              nullable3 = arTran.LineNbr;
              if (!nullable3.HasValue)
                arTran = new ARTran()
                {
                  TranType = arInvoice.DocType,
                  RefNbr = arInvoice.RefNbr,
                  LineNbr = new int?(0),
                  SortOrder = new int?(0)
                };
              PXSelectorAttribute.StoreResult<ARAdjust.adjdLineNbr>(((PXSelectBase) this.Adjustments).Cache, (object) adj, new List<object>()
              {
                (object) new PXResult<ARTran, ARInvoice>(arTran, arInvoice)
              });
              PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.lineNbr, Equal<Required<ARAdjust.adjdLineNbr>>>>>>>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>> invoiceDocTypeRefNbr = this.ARInvoice_DocType_RefNbr;
              List<object> objectList3 = new List<object>();
              objectList3.Add((object) res);
              PXQueryParameters pxQueryParameters = PXQueryParameters.ExplicitParameters(new object[3]
              {
                (object) adj.AdjdLineNbr,
                (object) arInvoice.DocType,
                (object) arInvoice.RefNbr
              });
              ((PXSelectBase<ARInvoice>) invoiceDocTypeRefNbr).StoreResult(objectList3, pxQueryParameters);
              PXParentAttribute.SetParent(((PXSelectBase) this.Adjustments).Cache, (object) adj, typeof (ARInvoice), (object) arInvoice);
              PXParentAttribute.SetParent(((PXSelectBase) this.Adjustments).Cache, (object) adj, typeof (ARPayment), (object) current);
              Decimal num6 = num1;
              Decimal? curyDocBal = arInvoice.CuryDocBal;
              Decimal? signBalance = arInvoice.SignBalance;
              Decimal valueOrDefault = (curyDocBal.HasValue & signBalance.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() * signBalance.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
              num1 = num6 + valueOrDefault;
              this.AddAdjustmentExt(adj);
            }
          }
          this.AutoPaymentApp = false;
          goto label_57;
        }
      }
      throw new ARPaymentEntry.PXLoadInvoiceException();
    }
    catch (ARPaymentEntry.PXLoadInvoiceException ex)
    {
    }
    finally
    {
      this.InternalCall = false;
    }
label_57:
    if (opts == null || !opts.Apply.GetValueOrDefault())
      return;
    Decimal? curyOrigDocAmt = ((PXSelectBase<ARPayment>) this.Document).Current.CuryOrigDocAmt;
    Decimal num7 = 0M;
    this.AutoApplyProc(!(curyOrigDocAmt.GetValueOrDefault() == num7 & curyOrigDocAmt.HasValue));
  }

  private void StoreInvoiceForAdjdRefNbrSelector(ARInvoice invoice, ARAdjust adj)
  {
    PXCache cache = ((PXSelectBase) this.Adjustments).Cache;
    ARAdjust arAdjust = adj;
    ARAdjust.ARInvoice arInvoice = new ARAdjust.ARInvoice();
    arInvoice.DocType = adj.AdjdDocType;
    arInvoice.RefNbr = adj.AdjdRefNbr;
    arInvoice.PaymentsByLinesAllowed = invoice.PaymentsByLinesAllowed;
    arInvoice.PendingPPD = invoice.PendingPPD;
    arInvoice.HasPPDTaxes = invoice.HasPPDTaxes;
    PXSelectorAttribute.StoreResult<ARAdjust.adjdRefNbr>(cache, (object) arAdjust, (IBqlTable) arInvoice);
  }

  internal static int CompareCustDocs(
    ARPayment currentARPayment,
    ARSetup currentARSetup,
    ARPaymentEntry.LoadOptions opts,
    ARInvoice aInvoice,
    ARInvoice bInvoice,
    ARTran aTran,
    ARTran bTran)
  {
    int num1 = 0;
    int num2 = 0;
    int? lineNbr;
    int num3;
    if (!(aInvoice.DocType == "CRM"))
    {
      lineNbr = aTran.LineNbr;
      if (lineNbr.HasValue)
      {
        Decimal? curyOrigTranAmt = aTran.CuryOrigTranAmt;
        Decimal num4 = 0M;
        num3 = curyOrigTranAmt.GetValueOrDefault() < num4 & curyOrigTranAmt.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
    }
    else
    {
      lineNbr = aTran.LineNbr;
      if (lineNbr.HasValue)
      {
        Decimal? curyOrigTranAmt = aTran.CuryOrigTranAmt;
        Decimal num5 = 0M;
        num3 = curyOrigTranAmt.GetValueOrDefault() > num5 & curyOrigTranAmt.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
    }
    bool flag1 = num3 != 0;
    int num6;
    if (!(bInvoice.DocType == "CRM"))
    {
      lineNbr = bTran.LineNbr;
      if (lineNbr.HasValue)
      {
        Decimal? curyOrigTranAmt = bTran.CuryOrigTranAmt;
        Decimal num7 = 0M;
        num6 = curyOrigTranAmt.GetValueOrDefault() < num7 & curyOrigTranAmt.HasValue ? 1 : 0;
      }
      else
        num6 = 0;
    }
    else
    {
      lineNbr = bTran.LineNbr;
      if (lineNbr.HasValue)
      {
        Decimal? curyOrigTranAmt = bTran.CuryOrigTranAmt;
        Decimal num8 = 0M;
        num6 = curyOrigTranAmt.GetValueOrDefault() > num8 & curyOrigTranAmt.HasValue ? 1 : 0;
      }
      else
        num6 = 0;
    }
    bool flag2 = num6 != 0;
    int num9 = num1;
    Decimal? curyOrigDocAmt = currentARPayment.CuryOrigDocAmt;
    Decimal num10 = 0M;
    int num11 = curyOrigDocAmt.GetValueOrDefault() > num10 & curyOrigDocAmt.HasValue & flag1 ? 0 : 10000;
    int num12 = num9 + num11;
    int num13 = num2;
    curyOrigDocAmt = currentARPayment.CuryOrigDocAmt;
    Decimal num14 = 0M;
    int num15 = curyOrigDocAmt.GetValueOrDefault() > num14 & curyOrigDocAmt.HasValue & flag2 ? 0 : 10000;
    int num16 = num13 + num15;
    if (currentARSetup.FinChargeFirst.GetValueOrDefault())
    {
      num12 += aInvoice.DocType == "FCH" ? 0 : 1000;
      num16 += bInvoice.DocType == "FCH" ? 0 : 1000;
    }
    DateTime? dueDate = aInvoice.DueDate;
    DateTime dateTime1 = dueDate ?? DateTime.MinValue;
    dueDate = bInvoice.DueDate;
    DateTime dateTime2 = dueDate ?? DateTime.MinValue;
    int num17;
    int num18;
    switch (opts?.OrderBy ?? "DUE")
    {
      case "REF":
        object refNbr1 = (object) aInvoice.RefNbr;
        object refNbr2 = (object) bInvoice.RefNbr;
        num17 = num12 + (1 + ((IComparable) refNbr1).CompareTo(refNbr2)) / 2;
        num18 = num16 + (1 - ((IComparable) refNbr1).CompareTo(refNbr2)) / 2;
        break;
      case "DOC":
        object docDate1 = (object) aInvoice.DocDate;
        object docDate2 = (object) bInvoice.DocDate;
        int num19 = num12 + (1 + ((IComparable) docDate1).CompareTo(docDate2)) / 2 * 10;
        int num20 = num16 + (1 - ((IComparable) docDate1).CompareTo(docDate2)) / 2 * 10;
        object refNbr3 = (object) aInvoice.RefNbr;
        object refNbr4 = (object) bInvoice.RefNbr;
        num17 = num19 + (1 + ((IComparable) refNbr3).CompareTo(refNbr4)) / 2;
        num18 = num20 + (1 - ((IComparable) refNbr3).CompareTo(refNbr4)) / 2;
        break;
      default:
        int num21 = num12 + (aInvoice.DocType == "CRM" ? 0 : 100);
        int num22 = num16 + (bInvoice.DocType == "CRM" ? 0 : 100);
        int num23 = num21 + (1 + dateTime1.CompareTo(dateTime2)) / 2 * 100;
        int num24 = num22 + (1 - dateTime1.CompareTo(dateTime2)) / 2 * 100;
        object refNbr5 = (object) aInvoice.RefNbr;
        object refNbr6 = (object) bInvoice.RefNbr;
        int num25 = num23 + (1 + ((IComparable) refNbr5).CompareTo(refNbr6)) / 2 * 10;
        int num26 = num24 + (1 - ((IComparable) refNbr5).CompareTo(refNbr6)) / 2 * 10;
        lineNbr = aTran.LineNbr;
        object valueOrDefault1 = (object) lineNbr.GetValueOrDefault();
        lineNbr = bTran.LineNbr;
        object valueOrDefault2 = (object) lineNbr.GetValueOrDefault();
        num17 = num25 + (1 + ((IComparable) valueOrDefault1).CompareTo(valueOrDefault2)) / 2;
        num18 = num26 + (1 - ((IComparable) valueOrDefault1).CompareTo(valueOrDefault2)) / 2;
        break;
    }
    return num17.CompareTo(num18);
  }

  public virtual void LoadOptions_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ARPaymentEntry.LoadOptions row = (ARPaymentEntry.LoadOptions) e.Row;
    if (row == null)
      return;
    PXCache pxCache1 = sender;
    ARPaymentEntry.LoadOptions loadOptions1 = row;
    bool? isInvoice = row.IsInvoice;
    int num1 = isInvoice.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.startRefNbr>(pxCache1, (object) loadOptions1, num1 != 0);
    PXCache pxCache2 = sender;
    ARPaymentEntry.LoadOptions loadOptions2 = row;
    isInvoice = row.IsInvoice;
    int num2 = isInvoice.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.endRefNbr>(pxCache2, (object) loadOptions2, num2 != 0);
    PXCache pxCache3 = sender;
    ARPaymentEntry.LoadOptions loadOptions3 = row;
    isInvoice = row.IsInvoice;
    int num3 = isInvoice.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.orderBy>(pxCache3, (object) loadOptions3, num3 != 0);
    PXCache pxCache4 = sender;
    ARPaymentEntry.LoadOptions loadOptions4 = row;
    isInvoice = row.IsInvoice;
    int num4 = isInvoice.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.loadingMode>(pxCache4, (object) loadOptions4, num4 != 0);
    PXCache pxCache5 = sender;
    ARPaymentEntry.LoadOptions loadOptions5 = row;
    isInvoice = row.IsInvoice;
    int num5 = isInvoice.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.apply>(pxCache5, (object) loadOptions5, num5 != 0);
    int num6;
    if (PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>() && ((PXSelectBase<ARPayment>) this.Document).Current != null && ((PXSelectBase<ARPayment>) this.Document).Current.CustomerID.HasValue)
      num6 = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.parentBAccountID, Equal<Required<Customer.parentBAccountID>>, And<Customer.consolidateToParent, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARPayment>) this.Document).Current.CustomerID
      })) != null ? 1 : 0;
    else
      num6 = 0;
    bool flag1 = num6 != 0;
    PXCache pxCache6 = sender;
    ARPaymentEntry.LoadOptions loadOptions6 = row;
    isInvoice = row.IsInvoice;
    int num7 = isInvoice.GetValueOrDefault() & flag1 ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.loadChildDocuments>(pxCache6, (object) loadOptions6, num7 != 0);
    PXCache pxCache7 = sender;
    ARPaymentEntry.LoadOptions loadOptions7 = row;
    isInvoice = row.IsInvoice;
    bool flag2 = false;
    int num8 = isInvoice.GetValueOrDefault() == flag2 & isInvoice.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.startOrderNbr>(pxCache7, (object) loadOptions7, num8 != 0);
    PXCache pxCache8 = sender;
    ARPaymentEntry.LoadOptions loadOptions8 = row;
    isInvoice = row.IsInvoice;
    bool flag3 = false;
    int num9 = isInvoice.GetValueOrDefault() == flag3 & isInvoice.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.endOrderNbr>(pxCache8, (object) loadOptions8, num9 != 0);
    PXCache pxCache9 = sender;
    ARPaymentEntry.LoadOptions loadOptions9 = row;
    isInvoice = row.IsInvoice;
    bool flag4 = false;
    int num10 = isInvoice.GetValueOrDefault() == flag4 & isInvoice.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARPaymentEntry.LoadOptions.sOOrderBy>(pxCache9, (object) loadOptions9, num10 != 0);
  }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LoadInvoices(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARPaymentEntry.\u003C\u003Ec__DisplayClass138_0 displayClass1380 = new ARPaymentEntry.\u003C\u003Ec__DisplayClass138_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1380.\u003C\u003E4__this = this;
    if (this.loadOpts != null && ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).Current != null)
      ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).Current.IsInvoice = new bool?(true);
    string error = PXUIFieldAttribute.GetError<ARPaymentEntry.LoadOptions.branchID>(((PXSelectBase) this.loadOpts).Cache, (object) ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).Current);
    if (!string.IsNullOrEmpty(error))
      throw new PXException(error);
    WebDialogResult webDialogResult = ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).AskExt();
    if (webDialogResult == 1 || webDialogResult == 6)
    {
      if (webDialogResult != 1)
      {
        if (webDialogResult == 6)
          ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).Current.LoadingMode = "R";
      }
      else
        ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).Current.LoadingMode = "L";
      ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).ClearDialog();
      if (((PXGraph) this).IsImport)
      {
        this.LoadInvoicesProc(false, ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).Current);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        displayClass1380.clone = GraphHelper.Clone<ARPaymentEntry>(this);
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1380, __methodptr(\u003CLoadInvoices\u003Eb__0)));
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AutoApply(PXAdapter adapter)
  {
    Decimal? curyOrigDocAmt = ((PXSelectBase<ARPayment>) this.Document).Current.CuryOrigDocAmt;
    Decimal num = 0M;
    this.AutoApplyProc(!(curyOrigDocAmt.GetValueOrDefault() == num & curyOrigDocAmt.HasValue));
    return adapter.Get();
  }

  public virtual void AutoApplyProc(bool checkBalance = true)
  {
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    ARPayment payment = ((PXSelectBase<ARPayment>) this.Document).Current;
    Decimal num1 = payment.CuryUnappliedBal.Value;
    try
    {
      this.InternalCall = true;
      if (num1 < 0M)
      {
        foreach (ARAdjust adj in GraphHelper.RowCast<ARAdjust>(((PXSelectBase) this.Adjustments).View.SelectExternal()).Where<ARAdjust>((Func<ARAdjust, bool>) (adj =>
        {
          Decimal? curyAdjgAmt = adj.CuryAdjgAmt;
          Decimal num3 = 0M;
          return !(curyAdjgAmt.GetValueOrDefault() == num3 & curyAdjgAmt.HasValue) && !adj.Released.GetValueOrDefault() && !adj.Voided.GetValueOrDefault();
        })))
        {
          Decimal? curyAdjgPpdAmt = adj.CuryAdjgPPDAmt;
          Decimal num2 = 0M;
          if (!(curyAdjgPpdAmt.GetValueOrDefault() == num2 & curyAdjgPpdAmt.HasValue))
          {
            adj.CuryAdjgPPDAmt = new Decimal?(0M);
            adj.FillDiscAmts();
          }
          ((PXSelectBase) this.Adjustments).Cache.SetValueExt<ARAdjust.curyAdjgAmt>((object) adj, (object) 0M);
          ((PXSelectBase) this.Adjustments).Cache.Update((object) adj);
        }
      }
      foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase) this.Adjustments).View.SelectExternal())
        PXSelectorAttribute.StoreResult<ARAdjust.adjdRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) pxResult, (IBqlTable) ((PXResult) pxResult).GetItem<ARInvoice>());
      this.RecalcApplAmounts(cache, payment);
      Decimal curyUnappliedBal = payment.CuryUnappliedBal.Value;
      List<PXResult<ARAdjust>> list = ((PXSelectBase) this.Adjustments).View.SelectExternal().Cast<PXResult<ARAdjust>>().Where<PXResult<ARAdjust>>((Func<PXResult<ARAdjust>, bool>) (adj => !PXResult<ARAdjust>.op_Implicit(adj).Released.GetValueOrDefault() && !PXResult<ARAdjust>.op_Implicit(adj).Voided.GetValueOrDefault())).ToList<PXResult<ARAdjust>>();
      list.Sort((Comparison<PXResult<ARAdjust>>) ((a, b) => ARPaymentEntry.CompareCustDocs(payment, ((PXSelectBase<ARSetup>) this.arsetup).Current, (ARPaymentEntry.LoadOptions) null, PXResult.Unwrap<ARInvoice>((object) a), PXResult.Unwrap<ARInvoice>((object) b), PXResult.Unwrap<ARTran>((object) a), PXResult.Unwrap<ARTran>((object) b))));
      foreach (PXResult<ARAdjust> pxResult in list)
      {
        ARAdjust adj = PXResult<ARAdjust>.op_Implicit(pxResult);
        if (adj.Selected.GetValueOrDefault())
        {
          Decimal? curyDocBal = adj.CuryDocBal;
          Decimal num4 = 0M;
          if (curyDocBal.GetValueOrDefault() == num4 & curyDocBal.HasValue)
            continue;
        }
        Decimal num5 = this.applyARAdjust(adj, curyUnappliedBal, checkBalance, true);
        bool? selected = adj.Selected;
        bool flag = false;
        if (!(selected.GetValueOrDefault() == flag & selected.HasValue))
        {
          curyUnappliedBal += num5;
          using (new ARPaymentEntry.AutoApplyProcess())
            ((PXSelectBase) this.Adjustments).Cache.Update((object) adj);
        }
        else
          break;
      }
      Decimal num6 = list.Cast<PXResult<ARAdjust>>().Sum<PXResult<ARAdjust>>((Func<PXResult<ARAdjust>, Decimal?>) (adj =>
      {
        Decimal? curyAdjgAmt = PXResult<ARAdjust>.op_Implicit(adj).CuryAdjgAmt;
        Decimal? adjgBalSign = PXResult<ARAdjust>.op_Implicit(adj).AdjgBalSign;
        return !(curyAdjgAmt.HasValue & adjgBalSign.HasValue) ? new Decimal?() : new Decimal?(curyAdjgAmt.GetValueOrDefault() * adjgBalSign.GetValueOrDefault());
      })).Value;
      if (num6 < 0M)
      {
        Decimal val2 = num6 * -1M;
        foreach (PXResult<ARAdjust> pxResult in (IEnumerable<PXResult<ARAdjust>>) list.Where<PXResult<ARAdjust>>((Func<PXResult<ARAdjust>, bool>) (adj => PXResult<ARAdjust>.op_Implicit(adj).AdjdDocType == "CRM")).OrderBy<PXResult<ARAdjust>, Decimal?>((Func<PXResult<ARAdjust>, Decimal?>) (adj => PXResult<ARAdjust>.op_Implicit(adj).CuryAdjgAmt)))
        {
          ARAdjust arAdjust1 = PXResult<ARAdjust>.op_Implicit(pxResult);
          bool? selected = arAdjust1.Selected;
          bool flag = false;
          if (!(selected.GetValueOrDefault() == flag & selected.HasValue))
          {
            Decimal? curyAdjgAmt = arAdjust1.CuryAdjgAmt;
            Decimal num7 = 0M;
            if (!(curyAdjgAmt.GetValueOrDefault() == num7 & curyAdjgAmt.HasValue))
            {
              curyAdjgAmt = arAdjust1.CuryAdjgAmt;
              Decimal num8 = Math.Min(curyAdjgAmt.Value, val2);
              ARAdjust arAdjust2 = arAdjust1;
              curyAdjgAmt = arAdjust2.CuryAdjgAmt;
              Decimal num9 = num8;
              arAdjust2.CuryAdjgAmt = curyAdjgAmt.HasValue ? new Decimal?(curyAdjgAmt.GetValueOrDefault() - num9) : new Decimal?();
              val2 -= num8;
              ARAdjust arAdjust3 = arAdjust1;
              curyAdjgAmt = arAdjust1.CuryAdjgAmt;
              Decimal num10 = 0M;
              bool? nullable = new bool?(!(curyAdjgAmt.GetValueOrDefault() == num10 & curyAdjgAmt.HasValue));
              arAdjust3.Selected = nullable;
              using (new ARPaymentEntry.AutoApplyProcess())
                ((PXSelectBase) this.Adjustments).Cache.Update((object) arAdjust1);
              if (val2 == 0M)
                break;
            }
          }
        }
      }
      this.RecalcApplAmounts(cache, payment);
      this.IsPaymentUnbalancedException(cache, payment);
    }
    finally
    {
      this.InternalCall = false;
    }
  }

  [PXUIField]
  [PXButton(Tooltip = "Set Payment Amount to Applied to Documents Amount", ImageSet = "main")]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AdjustDocAmt(PXAdapter adapter)
  {
    ARPayment copy = (ARPayment) ((PXSelectBase) this.Document).Cache.CreateCopy((object) ((PXSelectBase<ARPayment>) this.Document).Current);
    Decimal? curyUnappliedBal1 = copy.CuryUnappliedBal;
    Decimal num = 0M;
    if (!(curyUnappliedBal1.GetValueOrDefault() == num & curyUnappliedBal1.HasValue))
    {
      ARPayment arPayment = copy;
      Decimal? curyOrigDocAmt = copy.CuryOrigDocAmt;
      Decimal? curyUnappliedBal2 = copy.CuryUnappliedBal;
      Decimal? nullable = curyOrigDocAmt.HasValue & curyUnappliedBal2.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() - curyUnappliedBal2.GetValueOrDefault()) : new Decimal?();
      arPayment.CuryOrigDocAmt = nullable;
      ((PXSelectBase) this.Document).Cache.Update((object) copy);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ReverseApplication(PXAdapter adapter)
  {
    ARPayment current = ((PXSelectBase<ARPayment>) this.Document).Current;
    this.ReverseApplicationProc(PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.noteID, Equal<Current<ARTranPostBal.refNoteID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>())), current);
    return adapter.Get();
  }

  protected virtual void ReverseApplicationProc(ARAdjust application, ARPayment payment)
  {
    if (application == null)
      return;
    if (application.AdjType == "G")
      throw new PXException("This application cannot be reversed from {0}. Open {1} {2} to reverse this application.", new object[3]
      {
        (object) GetLabel.For<ARDocType>(payment.DocType),
        (object) GetLabel.For<ARDocType>(application.AdjgDocType),
        (object) application.AdjgRefNbr
      });
    if (application.AdjdDocType == "PPI" && application.AdjgDocType == "CRM")
      throw new PXException("Application of a credit memo created on reverse of a prepayment invoice cannot be reversed.");
    this.CheckDocumentBeforeReversing((PXGraph) this, application);
    bool? nullable = application.IsInitialApplication;
    nullable = !nullable.GetValueOrDefault() ? application.IsMigratedRecord : throw new PXException("This application cannot be reversed because it is a special application created in Migration Mode, which reflects the difference between the original document amount and the migrated balance.");
    if (!nullable.GetValueOrDefault())
    {
      nullable = ((PXSelectBase<ARSetup>) this.arsetup).Current.MigrationMode;
      if (nullable.GetValueOrDefault())
        throw new PXException("The application cannot be reversed because it was created when migration mode was deactivated. To process the application, clear the Activate Migration Mode check box on the Accounts Receivable Preferences (AR101000) form.");
    }
    nullable = application.Voided;
    if (nullable.GetValueOrDefault() || !ARPaymentType.CanHaveBalance(application.AdjgDocType) && !(application.AdjgDocType == "REF"))
      return;
    if (payment != null)
    {
      if (!(payment.DocType != "CRM"))
      {
        nullable = payment.PendingPPD;
        if (nullable.GetValueOrDefault())
          goto label_19;
      }
      nullable = application.AdjdHasPPDTaxes;
      if (nullable.GetValueOrDefault())
      {
        nullable = application.PendingPPD;
        if (!nullable.GetValueOrDefault())
        {
          ARAdjust ppdApplication = ARPaymentEntry.GetPPDApplication((PXGraph) this, application.AdjdDocType, application.AdjdRefNbr);
          if (ppdApplication != null)
            throw new PXSetPropertyException("To proceed, you have to reverse application of the final payment {0} with cash discount given.", new object[1]
            {
              (object) ppdApplication.AdjgRefNbr
            });
        }
      }
    }
label_19:
    nullable = ((PXSelectBase<ARPayment>) this.Document).Current.OpenDoc;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      ((PXSelectBase<ARPayment>) this.Document).Current.OpenDoc = new bool?(true);
      DateTime? adjDate = payment.AdjDate;
      ((SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.OpenDocument))).FireOn((PXGraph) this, payment);
      payment = ((PXSelectBase<ARPayment>) this.Document).Update(payment);
      ((PXSelectBase) this.Document).Cache.SetValueExt<ARPayment.adjDate>((object) payment, (object) adjDate);
    }
    this.CreateReversingApp(application, payment);
  }

  public static ARAdjust GetPPDApplication(PXGraph graph, string DocType, string RefNbr)
  {
    return PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.pendingPPD, Equal<True>>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[2]
    {
      (object) DocType,
      (object) RefNbr
    }));
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocumentToApply(PXAdapter adapter)
  {
    ARAdjust current = ((PXSelectBase<ARAdjust>) this.Adjustments).Current;
    if (string.IsNullOrEmpty(current?.AdjdDocType) || string.IsNullOrEmpty(current?.AdjdRefNbr))
      return adapter.Get();
    if (current.AdjdDocType == "PMT" || current.AdjgDocType == "REF" && current.AdjdDocType == "PPM")
    {
      ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) current.AdjdRefNbr, new object[1]
      {
        (object) current.AdjdDocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    ARInvoiceEntry arInvoiceEntry = PX.Objects.SO.SOInvoice.PK.Find((PXGraph) this, current.AdjdDocType, current.AdjdRefNbr) != null ? (ARInvoiceEntry) PXGraph.CreateInstance<SOInvoiceEntry>() : PXGraph.CreateInstance<ARInvoiceEntry>();
    ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Search<ARInvoice.refNbr>((object) current.AdjdRefNbr, new object[1]
    {
      (object) current.AdjdDocType
    }));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) arInvoiceEntry, true, "View Document");
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewApplicationDocument(PXAdapter adapter)
  {
    ARTranPostBal current = ((PXSelectBase<ARTranPostBal>) this.ARPost).Current;
    if (current == null || string.IsNullOrEmpty(current.SourceDocType) || string.IsNullOrEmpty(current.SourceRefNbr))
      return adapter.Get();
    string sourceDocType = current.SourceDocType;
    PXGraph pxGraph;
    if (sourceDocType == "PMT" || sourceDocType == "PPM" || sourceDocType == "REF" || sourceDocType == "VRF" || sourceDocType == "RPM" || sourceDocType == "SMB")
    {
      ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) current.SourceRefNbr, new object[1]
      {
        (object) current.SourceDocType
      }));
      pxGraph = (PXGraph) instance;
    }
    else
    {
      ARInvoiceEntry arInvoiceEntry = PX.Objects.SO.SOInvoice.PK.Find((PXGraph) this, current.SourceDocType, current.SourceRefNbr) != null ? (ARInvoiceEntry) PXGraph.CreateInstance<SOInvoiceEntry>() : PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Search<ARInvoice.refNbr>((object) current.SourceRefNbr, new object[1]
      {
        (object) current.SourceDocType
      }));
      pxGraph = (PXGraph) arInvoiceEntry;
    }
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException(pxGraph, true, "View Application Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewCurrentBatch(PXAdapter adapter)
  {
    ARTranPostBal current = ((PXSelectBase<ARTranPostBal>) this.ARPost).Current;
    if (current != null && !string.IsNullOrEmpty(current.BatchNbr))
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<PX.Objects.GL.Batch>) instance.BatchModule).Current = PXResultset<PX.Objects.GL.Batch>.op_Implicit(PXSelectBase<PX.Objects.GL.Batch, PXSelect<PX.Objects.GL.Batch, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAR>, And<PX.Objects.GL.Batch.batchNbr, Equal<Required<PX.Objects.GL.Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.BatchNbr
      }));
      if (((PXSelectBase<PX.Objects.GL.Batch>) instance.BatchModule).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Batch");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewOriginalDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(((PXSelectBase<ARPayment>) this.Document).Current?.OrigDocType, ((PXSelectBase<ARPayment>) this.Document).Current?.OrigRefNbr, ((PXSelectBase<ARPayment>) this.Document).Current?.OrigModule);
    return adapter.Get();
  }

  public static void CheckValidPeriodForCCTran(PXGraph graph, ARPayment payment)
  {
    DateTime today = PXTimeZoneInfo.Today;
    FinPeriod finPeriodByDate = graph.GetService<IFinPeriodRepository>().FindFinPeriodByDate(new DateTime?(today), PXAccess.GetParentOrganizationID(payment.BranchID));
    if (finPeriodByDate == null)
      throw new PXException("Cannot record CC transaction on {0} : {1}", new object[2]
      {
        (object) today.ToString("d", (IFormatProvider) graph.Culture),
        (object) "The financial period does not exist for the related branch or company."
      });
    try
    {
      graph.GetService<IFinPeriodUtils>().CanPostToPeriod((IFinPeriod) finPeriodByDate, typeof (FinPeriod.aRClosed));
    }
    catch (PXException ex)
    {
      throw new PXException("Cannot record CC transaction on {0} : {1}", new object[2]
      {
        (object) today.ToString("d", (IFormatProvider) graph.Culture),
        (object) ex.MessageNoPrefix
      });
    }
  }

  protected virtual void CATran_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_TranPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_ReferenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATran_CuryID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_DocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "PMT";
  }

  protected virtual Dictionary<System.Type, System.Type> CreateApplicationMap(bool invoiceSide)
  {
    if (invoiceSide)
      return new Dictionary<System.Type, System.Type>()
      {
        {
          typeof (ARAdjust.displayDocType),
          typeof (ARAdjust.adjdDocType)
        },
        {
          typeof (ARAdjust.displayRefNbr),
          typeof (ARAdjust.adjdRefNbr)
        },
        {
          typeof (ARAdjust.displayCustomerID),
          typeof (ARInvoice.customerID)
        },
        {
          typeof (ARAdjust.displayDocDate),
          typeof (ARInvoice.docDate)
        },
        {
          typeof (ARAdjust.displayDocDesc),
          typeof (ARInvoice.docDesc)
        },
        {
          typeof (ARAdjust.displayCuryID),
          typeof (ARInvoice.curyID)
        },
        {
          typeof (ARAdjust.displayFinPeriodID),
          typeof (ARInvoice.finPeriodID)
        },
        {
          typeof (ARAdjust.displayStatus),
          typeof (ARInvoice.status)
        },
        {
          typeof (ARAdjust.displayCuryInfoID),
          typeof (ARInvoice.curyInfoID)
        },
        {
          typeof (ARAdjust.displayCuryAmt),
          typeof (ARAdjust.curyAdjgAmt)
        },
        {
          typeof (ARAdjust.displayCuryWOAmt),
          typeof (ARAdjust.curyAdjgWOAmt)
        },
        {
          typeof (ARAdjust.displayCuryPPDAmt),
          typeof (ARAdjust.curyAdjgPPDAmt)
        }
      };
    return new Dictionary<System.Type, System.Type>()
    {
      {
        typeof (ARAdjust.displayDocType),
        typeof (ARAdjust.adjgDocType)
      },
      {
        typeof (ARAdjust.displayRefNbr),
        typeof (ARAdjust.adjgRefNbr)
      },
      {
        typeof (ARAdjust.displayCustomerID),
        typeof (ARPayment.customerID)
      },
      {
        typeof (ARAdjust.displayDocDate),
        typeof (ARPayment.docDate)
      },
      {
        typeof (ARAdjust.displayDocDesc),
        typeof (ARRegister.docDesc)
      },
      {
        typeof (ARAdjust.displayCuryID),
        typeof (ARPayment.curyID)
      },
      {
        typeof (ARAdjust.displayFinPeriodID),
        typeof (ARPayment.finPeriodID)
      },
      {
        typeof (ARAdjust.displayStatus),
        typeof (ARPayment.status)
      },
      {
        typeof (ARAdjust.displayCuryInfoID),
        typeof (ARPayment.curyInfoID)
      },
      {
        typeof (ARAdjust.displayCuryAmt),
        typeof (ARAdjust.curyAdjdAmt)
      },
      {
        typeof (ARAdjust.displayCuryWOAmt),
        typeof (ARAdjust.curyAdjdWOAmt)
      },
      {
        typeof (ARAdjust.displayCuryPPDAmt),
        typeof (ARAdjust.curyAdjdPPDAmt)
      }
    };
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  protected virtual IEnumerable adjustments()
  {
    this.FillBalanceCache(((PXSelectBase<ARPayment>) this.Document).Current);
    int startRow = PXView.StartRow;
    int num = 0;
    this.Adjustments_Invoices_BeforeSelect();
    if (((PXSelectBase<ARPayment>) this.Document).Current == null || ((PXSelectBase<ARPayment>) this.Document).Current.DocType != "REF" && ((PXSelectBase<ARPayment>) this.Document).Current.DocType != "VRF")
    {
      PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, this.CreateApplicationMap(true), new System.Type[4]
      {
        typeof (ARAdjust),
        typeof (ARInvoice),
        typeof (ARRegisterAlias),
        typeof (ARTran)
      });
      PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult();
      foreach (PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran> pxResult in ((PXSelectBase) this.Adjustments_Invoices).View.Select(PXView.Currents, (object[]) null, pxResultMapper.Searches, pxResultMapper.SortColumns, pxResultMapper.Descendings, PXView.PXFilterRowCollection.op_Implicit(pxResultMapper.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        ARInvoice voucher = PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>.op_Implicit(pxResult);
        ARTran tran = PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>.op_Implicit(pxResult);
        PXCache<ARRegister>.RestoreCopy((ARRegister) voucher, (ARRegister) PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>.op_Implicit(pxResult));
        if (((PXSelectBase) this.Adjustments).Cache.GetStatus((object) PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>.op_Implicit(pxResult)) == null)
          ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryDocumentExtension>().CalcBalances<ARInvoice>(PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>.op_Implicit(pxResult), voucher, true, false, tran);
        ((List<object>) delegateResult).Add(pxResultMapper.CreateResult((PXResult) new PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>(PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>.op_Implicit(pxResult), voucher, PXResult<ARAdjust, ARInvoice, ARRegisterAlias, ARTran>.op_Implicit(pxResult), tran)));
      }
      PXView.StartRow = 0;
      return (IEnumerable) delegateResult;
    }
    PXResultMapper pxResultMapper1 = new PXResultMapper((PXGraph) this, this.CreateApplicationMap(false), new System.Type[4]
    {
      typeof (ARAdjust),
      typeof (ARPayment),
      typeof (ARRegisterAlias),
      typeof (ARInvoice)
    });
    PXDelegateResult delegateResult1 = pxResultMapper1.CreateDelegateResult();
    foreach (PXResult<ARAdjust, ARPayment, ARRegisterAlias> pxResult in ((PXSelectBase) this.Adjustments_Payments).View.Select(PXView.Currents, (object[]) null, pxResultMapper1.Searches, pxResultMapper1.SortColumns, pxResultMapper1.Descendings, PXView.PXFilterRowCollection.op_Implicit(pxResultMapper1.Filters), ref startRow, PXView.MaximumRows, ref num))
    {
      ARPayment voucher = PXResult<ARAdjust, ARPayment, ARRegisterAlias>.op_Implicit(pxResult);
      PXCache<ARRegister>.RestoreCopy((ARRegister) voucher, (ARRegister) PXResult<ARAdjust, ARPayment, ARRegisterAlias>.op_Implicit(pxResult));
      ARInvoice arInvoice1 = new ARInvoice();
      arInvoice1.DocType = voucher.DocType;
      arInvoice1.RefNbr = voucher.RefNbr;
      ARInvoice arInvoice2 = arInvoice1;
      if (voucher.DocType == "CRM")
        voucher.DiscDate = (DateTime?) ARInvoice.PK.Find((PXGraph) this, voucher.DocType, voucher.RefNbr)?.DiscDate;
      if (((PXSelectBase) this.Adjustments).Cache.GetStatus((object) PXResult<ARAdjust, ARPayment, ARRegisterAlias>.op_Implicit(pxResult)) == null)
        ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryDocumentExtension>().CalcBalances<ARPayment>(PXResult<ARAdjust, ARPayment, ARRegisterAlias>.op_Implicit(pxResult), voucher, true, false, (ARTran) null);
      ((List<object>) delegateResult1).Add(pxResultMapper1.CreateResult((PXResult) new PXResult<ARAdjust, ARPayment, ARRegisterAlias, ARInvoice>(PXResult<ARAdjust, ARPayment, ARRegisterAlias>.op_Implicit(pxResult), voucher, PXResult<ARAdjust, ARPayment, ARRegisterAlias>.op_Implicit(pxResult), arInvoice2)));
    }
    PXView.StartRow = 0;
    return (IEnumerable) delegateResult1;
  }

  public virtual IEnumerable arpost()
  {
    using (new PXReadBranchRestrictedScope())
      return GraphHelper.QuickSelect(((PXSelectBase) this.ARPost).View);
  }

  public ARPaymentEntry()
  {
    ARPaymentEntry.LoadOptions current1 = ((PXSelectBase<ARPaymentEntry.LoadOptions>) this.loadOpts).Current;
    ARSetup current2 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    OpenPeriodAttribute.SetValidatePeriod<ARPayment.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    bool? nullable = new bool?(false);
    current2.CreditCheckError = nullable;
    this.ForcePaymentApp = ForcePaymentAppScope.IsActive;
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName.Equals("Document", StringComparison.OrdinalIgnoreCase) && values != null)
    {
      PXCache cache = ((PXGraph) this).Views[viewName].Cache;
      foreach (string key in values.Keys.Cast<string>().Where<string>((Func<string, bool>) (_ => cache.GetStateExt(cache.Current, _) is PXFieldState stateExt && !stateExt.Enabled)).ToArray<string>())
        values[(object) key] = PXCache.NotSetValue;
      values[(object) "CurySOApplAmt"] = PXCache.NotSetValue;
      values[(object) "CuryApplAmt"] = PXCache.NotSetValue;
      values[(object) "CuryWOAmt"] = PXCache.NotSetValue;
      values[(object) "CuryUnappliedBal"] = PXCache.NotSetValue;
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  public virtual void Clear()
  {
    this.balanceCache = (Dictionary<ARAdjust, PXResultset<ARInvoice>>) null;
    ((PXGraph) this).Clear();
  }

  public virtual void Persist()
  {
    foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) this.Adjustments).Select(Array.Empty<object>()))
    {
      ARAdjust arAdjust = PXResult<ARAdjust>.op_Implicit(pxResult);
      PXResult.Unwrap<ARRegisterAlias>((object) pxResult);
      Decimal? nullable = arAdjust.CuryAdjgAmt;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = arAdjust.CuryAdjgDiscAmt;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
        {
          nullable = arAdjust.CuryAdjgPPDAmt;
          Decimal num3 = 0M;
          if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
            ((PXSelectBase) this.Adjustments).Cache.Delete((object) arAdjust);
        }
      }
    }
    foreach (ARPayment arPayment in ((PXSelectBase) this.Document).Cache.Updated)
    {
      bool? nullable1 = arPayment.SelfVoidingDoc;
      if (nullable1.GetValueOrDefault())
      {
        int num4 = ((IQueryable<PXResult<ARAdjust>>) ((PXSelectBase<ARAdjust>) this.Adjustments_Raw).Select(new object[2]
        {
          (object) arPayment.DocType,
          (object) arPayment.RefNbr
        })).Count<PXResult<ARAdjust>>((Expression<Func<PXResult<ARAdjust>, bool>>) (adj => ((ARAdjust) adj).Voided == (bool?) true));
        int num5 = ((IQueryable<PXResult<ARTranPostBal>>) ((PXSelectBase<ARTranPostBal>) this.ARPost).Select(Array.Empty<object>())).Count<PXResult<ARTranPostBal>>();
        if (num4 > 0 && num4 != num5)
          throw new PXException("The document should be voided in full. The reversing applications cannot be deleted partially.");
      }
      nullable1 = arPayment.OpenDoc;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = (bool?) ((PXSelectBase) this.Document).Cache.GetValueOriginal<ARPayment.openDoc>((object) arPayment);
        bool flag1 = false;
        if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
        {
          Decimal? nullable2 = arPayment.DocBal;
          Decimal num6 = 0M;
          if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
          {
            nullable2 = arPayment.UnappliedBal;
            Decimal num7 = 0M;
            if (nullable2.GetValueOrDefault() == num7 & nullable2.HasValue)
            {
              nullable1 = arPayment.Released;
              if (nullable1.GetValueOrDefault())
              {
                nullable1 = arPayment.Hold;
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
        if (((PXSelectBase<ARAdjust>) this.Adjustments_Raw).SelectSingle(new object[2]
        {
          (object) arPayment.DocType,
          (object) arPayment.RefNbr
        }) == null)
        {
          arPayment.OpenDoc = new bool?(false);
          ((PXSelectBase) this.Document).Cache.RaiseRowSelected((object) arPayment);
        }
      }
    }
    ARPaymentEntry.ARPaymentContextExtention extension = ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentContextExtention>();
    extension.GraphContext = GraphContextExtention<ARPaymentEntry>.Context.Persist;
    ((PXGraph) this).Persist();
    extension.GraphContext = GraphContextExtention<ARPaymentEntry>.Context.None;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Caches[typeof (CADailySummary)].Persist((PXDBOperation) 2);
      transactionScope.Complete((PXGraph) this);
    }
    ((PXGraph) this).Caches[typeof (CADailySummary)].Persisted(false);
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current.CuryID))
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current.CuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current.CuryRateTypeID))
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current.CuryRateTypeID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase) this.Document).Cache.Current == null)
      return;
    e.NewValue = (object) ((ARRegister) ((PXSelectBase) this.Document).Cache.Current).DocDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.customer.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<ARPayment.paymentMethodID>(e.Row);
    sender.SetDefaultExt<ARInvoice.customerLocationID>(e.Row);
    ARPayment row = (ARPayment) e.Row;
    if (!e.ExternalCall || row.IsMigratedRecord.GetValueOrDefault())
      return;
    Customer current = ((PXSelectBase<Customer>) this.customer).Current;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      bool? autoApplyPayments = current.AutoApplyPayments;
      bool flag = false;
      num1 = autoApplyPayments.GetValueOrDefault() == flag & autoApplyPayments.HasValue ? 1 : 0;
    }
    if (num1 == 0 || ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsMobile || ((PXGraph) this).IsProcessing || ((PXGraph) this).IsImport || ((PXGraph) this).UnattendedMode)
      return;
    short? autoLoadMaxDocs = ((PXSelectBase<ARSetup>) this.arsetup).Current.AutoLoadMaxDocs;
    int? nullable = autoLoadMaxDocs.HasValue ? new int?((int) autoLoadMaxDocs.GetValueOrDefault()) : new int?();
    int num2 = 0;
    if (!(nullable.GetValueOrDefault() > num2 & nullable.HasValue))
      return;
    if (((PXSelectBase) this.Document).View.GetAnswer("RowCountWarningDialog") != null)
    {
      ((PXSelectBase<ARPayment>) this.Document).ClearDialog();
    }
    else
    {
      ARPaymentEntry.LoadOptions loadOptions = new ARPaymentEntry.LoadOptions();
      loadOptions.Apply = new bool?(false);
      autoLoadMaxDocs = ((PXSelectBase<ARSetup>) this.arsetup).Current.AutoLoadMaxDocs;
      loadOptions.MaxDocs = autoLoadMaxDocs.HasValue ? new int?((int) autoLoadMaxDocs.GetValueOrDefault()) : new int?();
      ARPaymentEntry.LoadOptions opts = loadOptions;
      int custDocsCount = ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryCustomerDocsExtension>().GetCustDocsCount(opts, row, ((PXSelectBase<ARSetup>) this.arsetup).Current);
      int num3 = custDocsCount;
      int? maxDocs = opts.MaxDocs;
      int valueOrDefault = maxDocs.GetValueOrDefault();
      if (num3 > valueOrDefault & maxDocs.HasValue)
      {
        string str1;
        if (!PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>())
          str1 = PXLocalizer.LocalizeFormat("The {0} customer has {1} open documents to apply. To add documents, click Load Documents or Add Row.", new object[2]
          {
            (object) ((PXSelectBase<Customer>) this.customer).Current?.AcctCD,
            (object) custDocsCount
          });
        else
          str1 = PXLocalizer.LocalizeFormat("The {0} customer has {1} rows to apply. To add documents and document lines, click Load Documents or Add Row.", new object[2]
          {
            (object) ((PXSelectBase<Customer>) this.customer).Current?.AcctCD,
            (object) custDocsCount
          });
        string str2 = str1;
        sender.RaiseExceptionHandling<ARPayment.customerID>(e.Row, (object) row.CustomerID, (Exception) new PXSetPropertyException(str2, (PXErrorLevel) 2));
        ((PXSelectBase<ARPayment>) this.Document).Ask("RowCountWarningDialog", "Warning", str2, (MessageButtons) 0, (MessageIcon) 3);
      }
      else
      {
        sender.RaiseExceptionHandling<ARPayment.customerID>(e.Row, (object) row.CustomerID, (Exception) null);
        if (custDocsCount <= 0)
          return;
        this.LoadInvoicesProc(false, opts);
      }
    }
  }

  protected virtual void ARPayment_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.location.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<ARRegister.aRAccountID>(e.Row);
    sender.SetDefaultExt<ARRegister.aRSubID>(e.Row);
  }

  protected virtual void ARPayment_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARPayment row = e.Row as ARPayment;
    if (!this.IsApprovalRequired(row))
      return;
    sender.SetValue<ARPayment.hold>((object) row, (object) true);
  }

  protected virtual void ARPayment_ExtRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (row == null)
      return;
    if (row.DocType == "RPM" || row.DocType == "VRF")
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (string.IsNullOrEmpty((string) e.NewValue) || string.IsNullOrEmpty(row.PaymentMethodID))
        return;
      ARPayment arPayment;
      if (((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current.IsAccountNumberRequired.GetValueOrDefault())
        arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelectReadonly<ARPayment, Where<ARPayment.customerID, Equal<Current<ARPayment.customerID>>, And<ARPayment.pMInstanceID, Equal<Current<ARPayment.pMInstanceID>>, And<ARPayment.extRefNbr, Equal<Required<ARPayment.extRefNbr>>, And<ARPayment.voided, Equal<False>, And<Where<ARPayment.docType, NotEqual<Current<ARPayment.docType>>, Or<ARPayment.refNbr, NotEqual<Current<ARPayment.refNbr>>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          e.NewValue
        }));
      else
        arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelectReadonly<ARPayment, Where<ARPayment.customerID, Equal<Current<ARPayment.customerID>>, And<ARPayment.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>, And<ARPayment.extRefNbr, Equal<Required<ARPayment.extRefNbr>>, And<ARPayment.voided, Equal<False>, And<Where<ARPayment.docType, NotEqual<Current<ARPayment.docType>>, Or<ARPayment.refNbr, NotEqual<Current<ARPayment.refNbr>>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          e.NewValue
        }));
      if (arPayment == null)
        return;
      sender.RaiseExceptionHandling<ARPayment.extRefNbr>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("Payment with Payment Ref. '{0}' dated '{1}' already exists for this Customer and have the same Payment Method. It's Reference Number - {2} {3}.", (PXErrorLevel) 2, new object[4]
      {
        (object) arPayment.ExtRefNbr,
        (object) arPayment.DocDate,
        (object) arPayment.DocType,
        (object) arPayment.RefNbr
      }));
    }
  }

  private object GetAcctSub<Field>(PXCache cache, object data) where Field : IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }

  protected virtual void ARPayment_ARAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<Customer>) this.customer).Current == null || e.Row == null)
      return;
    if (((ARRegister) e.Row).DocType == "PPM")
      e.NewValue = this.GetAcctSub<Customer.prepaymentAcctID>(((PXSelectBase) this.customer).Cache, (object) ((PXSelectBase<Customer>) this.customer).Current);
    if (!string.IsNullOrEmpty((string) e.NewValue))
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aRAccountID>(((PXSelectBase) this.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current);
  }

  protected virtual void ARPayment_ARSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<Customer>) this.customer).Current == null || e.Row == null)
      return;
    if (((ARRegister) e.Row).DocType == "PPM")
      e.NewValue = this.GetAcctSub<Customer.prepaymentSubID>(((PXSelectBase) this.customer).Cache, (object) ((PXSelectBase<Customer>) this.customer).Current);
    if (!string.IsNullOrEmpty((string) e.NewValue))
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aRSubID>(((PXSelectBase) this.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current);
  }

  protected virtual void ARPayment_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARPayment row = e.Row as ARPayment;
    sender.SetDefaultExt<ARPayment.pMInstanceID>((object) row);
    sender.SetDefaultExt<ARPayment.cashAccountID>((object) row);
    sender.SetDefaultExt<ARRegister.aRAccountID>((object) row);
    sender.SetDefaultExt<ARPayment.dontEmail>((object) row);
  }

  protected virtual void ARPayment_PMInstanceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.OldValue != null)
      return;
    sender.SetDefaultExt<ARPayment.cashAccountID>(e.Row);
  }

  protected virtual void PMInstanceIDFieldDefaulting(
    PX.Data.Events.FieldDefaulting<ARPayment.pMInstanceID> e)
  {
    if (!(e.Row is ARPayment) || !this._IsVoidCheckInProgress)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARPayment.pMInstanceID>>) e).Cancel = true;
  }

  protected virtual void ARPayment_CashAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (!this._IsVoidCheckInProgress)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current != null)
    {
      int? cashAccountId1 = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current.CashAccountID;
      int? cashAccountId2 = row.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
        goto label_3;
    }
    ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<ARPayment.cashAccountID>(sender, e.Row);
label_3:
    sender.SetDefaultExt<ARPayment.depositAsBatch>(e.Row);
    sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);
    row.Cleared = new bool?(false);
    row.ClearDate = new DateTime?();
    PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Select(Array.Empty<object>()));
    if (!(paymentMethod?.PaymentType != "CCD") || !(paymentMethod?.PaymentType != "EFT"))
      return;
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      bool? reconcile = current.Reconcile;
      bool flag = false;
      num = reconcile.GetValueOrDefault() == flag & reconcile.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    row.Cleared = new bool?(true);
    row.ClearDate = row.DocDate;
  }

  protected virtual void ARPayment_Cleared_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (row.Cleared.GetValueOrDefault())
    {
      if (row.ClearDate.HasValue)
        return;
      row.ClearDate = row.DocDate;
    }
    else
      row.ClearDate = new DateTime?();
  }

  protected virtual void ARPayment_AdjDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    bool? released = row.Released;
    bool flag1 = false;
    if (!(released.GetValueOrDefault() == flag1 & released.HasValue))
      return;
    bool? voidAppl = row.VoidAppl;
    bool flag2 = false;
    if (!(voidAppl.GetValueOrDefault() == flag2 & voidAppl.HasValue))
      return;
    sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);
  }

  protected virtual void ARPayment_AdjDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (row == null || !(row.DocType == "RPM") && !(row.DocType == "VRF"))
      return;
    ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where2<Where<ARPayment.docType, Equal<ARDocType.payment>, Or<ARPayment.docType, Equal<ARDocType.prepayment>, Or<ARPayment.docType, Equal<ARDocType.refund>>>>, And<ARPayment.refNbr, Equal<Current<ARPayment.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    if (arPayment == null)
      return;
    DateTime? docDate = arPayment.DocDate;
    DateTime newValue = (DateTime) e.NewValue;
    if ((docDate.HasValue ? (docDate.GetValueOrDefault() > newValue ? 1 : 0) : 0) != 0)
    {
      object[] objArray = new object[1];
      docDate = arPayment.DocDate;
      objArray[0] = (object) docDate.Value.ToString("d");
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", objArray);
    }
  }

  protected virtual void ARPayment_CuryOrigDocAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (((ARRegister) e.Row).Released.Value)
      return;
    sender.SetValueExt<ARPayment.curyDocBal>(e.Row, (object) ((ARRegister) e.Row).CuryOrigDocAmt);
  }

  protected virtual void ARPayment_RefTranExtNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  private bool IsApprovalRequired(ARPayment doc)
  {
    return doc.DocType == "REF" && EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.ApprovableDocTypes.Contains(doc.DocType);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<ARAdjust2> e)
  {
    if (e.Row == null)
      return;
    ARAdjust2 row = e.Row;
    if (row.PPDVATAdjRefNbr == null)
      return;
    row.PPDVATAdjDescription = $"{ARDocType.GetDisplayName(row.PPDVATAdjDocType)}, {row.PPDVATAdjRefNbr}";
  }

  protected virtual void ARAdjust_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is ARAdjust row))
      return;
    ARAdjust arAdjust = row;
    Decimal? nullable1 = row.CuryAdjgAmt;
    Decimal num1 = 0M;
    int num2;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      nullable1 = row.CuryAdjgPPDAmt;
      Decimal num3 = 0M;
      num2 = !(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue) ? 1 : 0;
    }
    else
      num2 = 1;
    bool? nullable2 = new bool?(num2 != 0);
    arAdjust.Selected = nullable2;
  }

  protected virtual void ARAdjust_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARAdjust row))
      return;
    bool? nullable1 = row.Released;
    bool flag1 = !nullable1.GetValueOrDefault();
    nullable1 = row.Voided;
    bool flag2 = !nullable1.GetValueOrDefault();
    bool flag3 = row.AdjdRefNbr != null;
    PXUIFieldAttribute.SetEnabled<ARAdjust.adjdDocType>(cache, (object) row, row.AdjdRefNbr == null);
    PXUIFieldAttribute.SetEnabled<ARAdjust.adjdRefNbr>(cache, (object) row, row.AdjdRefNbr == null);
    PXUIFieldAttribute.SetEnabled<ARAdjust.adjdLineNbr>(cache, (object) row, !row.AdjdLineNbr.HasValue);
    PXUIFieldAttribute.SetEnabled<ARAdjust.selected>(cache, (object) row, flag3 & flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<ARAdjust.curyAdjgAmt>(cache, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<ARAdjust.curyAdjgPPDAmt>(cache, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<ARAdjust.adjBatchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<ARAdjust.adjBatchNbr>(cache, (object) row, !flag1);
    if (((PXSelectBase<ARPayment>) this.Document).Current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.AdjdCustomerID
      }));
      if (customer != null)
      {
        nullable1 = customer.SmallBalanceAllow;
        Decimal? nullable2;
        int num1;
        if (nullable1.GetValueOrDefault())
        {
          nullable2 = customer.SmallBalanceLimit;
          Decimal num2 = 0M;
          if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
          {
            num1 = row.AdjdDocType != "CRM" ? 1 : 0;
            goto label_8;
          }
        }
        num1 = 0;
label_8:
        bool flag4 = num1 != 0;
        nullable2 = row.CuryOrigDocAmt;
        Decimal num3 = 0M;
        Sign sign = nullable2.GetValueOrDefault() < num3 & nullable2.HasValue ? Sign.Minus : Sign.Plus;
        PXUIFieldAttribute.SetEnabled<ARAdjust.curyAdjgWOAmt>(cache, (object) row, flag1 & flag2 & flag4 && Sign.op_Equality(sign, Sign.Plus));
        PXCache pxCache = cache;
        ARAdjust arAdjust = row;
        int num4;
        if (flag4)
        {
          nullable1 = ((PXSelectBase<ARPayment>) this.Document).Current.SelfVoidingDoc;
          num4 = !nullable1.GetValueOrDefault() ? 1 : 0;
        }
        else
          num4 = 0;
        PXUIFieldAttribute.SetEnabled<ARAdjust.writeOffReasonCode>(pxCache, (object) arAdjust, num4 != 0);
      }
    }
    bool flag5 = false;
    nullable1 = row.Released;
    if (!nullable1.GetValueOrDefault())
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().GetCurrencyInfo(row.AdjgCuryInfoID);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().GetCurrencyInfo(row.AdjdCuryInfoID);
      flag5 = currencyInfo2 != null && !string.Equals(currencyInfo1.CuryID, currencyInfo2.CuryID) && !string.Equals(currencyInfo2.CuryID, currencyInfo2.BaseCuryID);
    }
    PXUIFieldAttribute.SetEnabled<ARAdjust.adjdCuryRate>(cache, (object) row, flag5);
    int? adjdLineNbr = row.AdjdLineNbr;
    int num5 = 0;
    if (!(adjdLineNbr.GetValueOrDefault() == num5 & adjdLineNbr.HasValue))
      return;
    ARRegister paymentsByLineAllowed = this.GetAdjdInvoiceToVerifyArePaymentsByLineAllowed(((PXSelectBase) this.Adjustments).Cache, row);
    int num6;
    if (paymentsByLineAllowed == null)
    {
      num6 = 0;
    }
    else
    {
      nullable1 = paymentsByLineAllowed.PaymentsByLinesAllowed;
      num6 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num6 == 0)
      return;
    ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("The application cannot be released because it is not distributed between document lines. On the Payments and Applications (AR302000) form, delete the application, and apply the payment or credit memo to the document lines.", (PXErrorLevel) 3));
  }

  private ARRegister GetAdjdInvoiceToVerifyArePaymentsByLineAllowed(PXCache cache, ARAdjust adj)
  {
    PXResultset<ARInvoice> pxResultset;
    return this.balanceCache != null && this.balanceCache.TryGetValue(adj, out pxResultset) ? (ARRegister) PXResult.Unwrap<ARInvoice>((object) pxResultset[0]) : (ARRegister) ((PXGraph) this).Caches[typeof (ARInvoice)].Cached.Cast<ARInvoice>().FirstOrDefault<ARInvoice>((Func<ARInvoice, bool>) (_ => _.DocType == adj.AdjdDocType && _.RefNbr == adj.AdjdRefNbr)) ?? (ARRegister) PXSelectorAttribute.Select<ARAdjust.adjdRefNbr>(cache, (object) adj);
  }

  protected virtual void ARAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    ARAdjust row = (ARAdjust) e.Row;
    string error1 = PXUIFieldAttribute.GetError<ARAdjust.adjdRefNbr>(sender, (object) row);
    string error2 = PXUIFieldAttribute.GetError<ARAdjust.adjdLineNbr>(sender, e.Row);
    PXRowInsertingEventArgs insertingEventArgs = e;
    int num;
    if (row.AdjdRefNbr != null && row.AdjdLineNbr.HasValue && string.IsNullOrEmpty(error1) && string.IsNullOrEmpty(error2))
    {
      ARPayment current = ((PXSelectBase<ARPayment>) this.Document).Current;
      num = current != null ? (current.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num = 1;
    ((CancelEventArgs) insertingEventArgs).Cancel = num != 0;
  }

  protected virtual void ARAdjust_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (((ARAdjust) e.Row).Voided.GetValueOrDefault() && !this._IsVoidCheckInProgress && !this.IsReverseProc && !sender.ObjectsEqualExceptFields<ARAdjust.isCCPayment, ARAdjust.paymentReleased, ARAdjust.isCCAuthorized, ARAdjust.isCCCaptured>(e.Row, e.NewRow))
      throw new PXSetPropertyException("The record cannot be updated.");
  }

  protected virtual void ARAdjust_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    long? adjdCuryInfoId1 = ((ARAdjust) e.Row).AdjdCuryInfoID;
    long? adjgCuryInfoId = ((ARAdjust) e.Row).AdjgCuryInfoID;
    if (adjdCuryInfoId1.GetValueOrDefault() == adjgCuryInfoId.GetValueOrDefault() & adjdCuryInfoId1.HasValue == adjgCuryInfoId.HasValue)
      return;
    long? adjdCuryInfoId2 = ((ARAdjust) e.Row).AdjdCuryInfoID;
    long? adjdOrigCuryInfoId = ((ARAdjust) e.Row).AdjdOrigCuryInfoID;
    if (adjdCuryInfoId2.GetValueOrDefault() == adjdOrigCuryInfoId.GetValueOrDefault() & adjdCuryInfoId2.HasValue == adjdOrigCuryInfoId.HasValue || ((ARAdjust) e.Row).VoidAdjNbr.HasValue)
      return;
    foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> pxResult in ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).Select(new object[1]
    {
      (object) ((ARAdjust) e.Row).AdjdCuryInfoID
    }))
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Delete(PXResult<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult));
  }

  protected virtual void ARAdjust_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARAdjust row = (ARAdjust) e.Row;
    if ((e.Operation & 3) != 3)
    {
      if ((e.Operation & 3) == 2 && !string.IsNullOrEmpty(row.AdjgFinPeriodID) && ((IEnumerable<string>) ARPaymentEntry.AdjgDocTypesToValidateFinPeriod).Contains<string>(row.AdjgDocType))
      {
        ProcessingResult period = this.FinPeriodUtils.CanPostToPeriod((IFinPeriod) this.FinPeriodRepository.GetByID(row.AdjgFinPeriodID, PXAccess.GetParentOrganizationID(row.AdjgBranchID)), typeof (FinPeriod.aRClosed));
        if (!period.IsSuccess)
          throw new PXRowPersistingException(PXDataUtils.FieldName<ARAdjust.adjgFinPeriodID>(), (object) row.AdjgFinPeriodID, period.GetGeneralMessage(), new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<ARAdjust.adjgFinPeriodID>(sender)
          });
      }
      Decimal? nullable1 = row.CuryOrigDocAmt;
      Decimal num1 = 0M;
      Sign sign1 = nullable1.GetValueOrDefault() < num1 & nullable1.HasValue ? Sign.Minus : Sign.Plus;
      bool? nullable2 = row.Released;
      int? nullable3;
      if (!nullable2.GetValueOrDefault())
      {
        int? adjNbr = row.AdjNbr;
        nullable3 = (int?) ((PXSelectBase<ARPayment>) this.Document).Current?.AdjCntr;
        if (adjNbr.GetValueOrDefault() < nullable3.GetValueOrDefault() & adjNbr.HasValue & nullable3.HasValue)
          sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("The application cannot be processed and your changes are not saved. Cancel the changes and start over. If the error persists, please contact support."));
      }
      Decimal? nullable4 = row.CuryDocBal;
      Sign sign2 = sign1;
      nullable1 = nullable4.HasValue ? new Decimal?(Sign.op_Multiply(nullable4.GetValueOrDefault(), sign2)) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() < num2 & nullable1.HasValue)
        sender.RaiseExceptionHandling<ARAdjust.curyAdjgAmt>(e.Row, (object) row.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      nullable4 = row.CuryDiscBal;
      Sign sign3 = sign1;
      nullable1 = nullable4.HasValue ? new Decimal?(Sign.op_Multiply(nullable4.GetValueOrDefault(), sign3)) : new Decimal?();
      Decimal num3 = 0M;
      if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
        sender.RaiseExceptionHandling<ARAdjust.curyAdjgPPDAmt>(e.Row, (object) row.CuryAdjgPPDAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      nullable4 = row.CuryWOBal;
      Sign sign4 = sign1;
      nullable1 = nullable4.HasValue ? new Decimal?(Sign.op_Multiply(nullable4.GetValueOrDefault(), sign4)) : new Decimal?();
      Decimal num4 = 0M;
      if (nullable1.GetValueOrDefault() < num4 & nullable1.HasValue)
        sender.RaiseExceptionHandling<ARAdjust.curyAdjgWOAmt>(e.Row, (object) row.CuryAdjgWOAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      ARAdjust arAdjust = row;
      nullable1 = row.CuryAdjgPPDAmt;
      Decimal num5 = 0M;
      int num6;
      if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
      {
        nullable2 = row.AdjdHasPPDTaxes;
        num6 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      else
        num6 = 0;
      bool? nullable5 = new bool?(num6 != 0);
      arAdjust.PendingPPD = nullable5;
      nullable2 = row.PendingPPD;
      if (nullable2.GetValueOrDefault())
      {
        nullable1 = row.CuryDocBal;
        Decimal num7 = 0M;
        if (!(nullable1.GetValueOrDefault() == num7 & nullable1.HasValue))
        {
          nullable2 = row.Voided;
          if (!nullable2.GetValueOrDefault())
            sender.RaiseExceptionHandling<ARAdjust.curyAdjgPPDAmt>(e.Row, (object) row.CuryAdjgPPDAmt, (Exception) new PXSetPropertyException("Cash discount can be applied only on final payment."));
        }
      }
      nullable1 = row.CuryAdjgWOAmt;
      Decimal num8 = 0M;
      if (!(nullable1.GetValueOrDefault() == num8 & nullable1.HasValue) && string.IsNullOrEmpty(row.WriteOffReasonCode))
      {
        if (sender.RaiseExceptionHandling<ARAdjust.writeOffReasonCode>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARAdjust.writeOffReasonCode>(sender)
        })))
          throw new PXRowPersistingException(PXDataUtils.FieldName<ARAdjust.writeOffReasonCode>(), (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<ARAdjust.writeOffReasonCode>(sender)
          });
      }
      Decimal adjustedBalanceDelta = row.GetFullBalanceDelta().CurrencyAdjustedBalanceDelta;
      nullable3 = row.VoidAdjNbr;
      if (!nullable3.HasValue && Sign.op_Multiply(adjustedBalanceDelta, sign1) < 0M)
        sender.RaiseExceptionHandling<ARAdjust.curyAdjgAmt>((object) row, (object) row.CuryAdjdAmt, (Exception) new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "The total application amount, including the cash discount and the write-off amounts, cannot be negative." : "The total application amount, including cash discounts and write-offs, cannot be positive."));
      nullable3 = row.VoidAdjNbr;
      if (nullable3.HasValue && Sign.op_Multiply(adjustedBalanceDelta, sign1) > 0M)
        sender.RaiseExceptionHandling<ARAdjust.curyAdjgAmt>((object) row, (object) row.CuryAdjdAmt, (Exception) new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "For reversed applications, the total application amount, including the cash discount and the write-off amounts, cannot be positive." : "For reversed applications, the total application amount including cash discounts and write-offs cannot be negative."));
      nullable3 = row.VoidAdjNbr;
      if (!nullable3.HasValue)
      {
        nullable4 = row.CuryAdjgAmt;
        Sign sign5 = sign1;
        nullable1 = nullable4.HasValue ? new Decimal?(Sign.op_Multiply(nullable4.GetValueOrDefault(), sign5)) : new Decimal?();
        Decimal num9 = 0M;
        if (nullable1.GetValueOrDefault() < num9 & nullable1.HasValue)
          throw new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
      }
      nullable3 = row.VoidAdjNbr;
      if (nullable3.HasValue)
      {
        nullable4 = row.CuryAdjgAmt;
        Sign sign6 = sign1;
        nullable1 = nullable4.HasValue ? new Decimal?(Sign.op_Multiply(nullable4.GetValueOrDefault(), sign6)) : new Decimal?();
        Decimal num10 = 0M;
        if (nullable1.GetValueOrDefault() > num10 & nullable1.HasValue)
          throw new PXSetPropertyException(Sign.op_Equality(sign1, Sign.Plus) ? "Incorrect value. The value to be entered must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
      }
      nullable3 = row.VoidAdjNbr;
      if (!nullable3.HasValue)
      {
        Sign sign7 = sign1;
        nullable1 = row.CuryAdjgPPDAmt;
        int num11 = Math.Sign(nullable1.Value);
        if (Sign.op_Multiply(sign7, num11) < 0)
          goto label_38;
      }
      nullable3 = row.VoidAdjNbr;
      if (!nullable3.HasValue)
        return;
      Sign sign8 = sign1;
      nullable1 = row.CuryAdjgPPDAmt;
      int num12 = Math.Sign(nullable1.Value);
      if (Sign.op_Multiply(sign8, num12) <= 0)
        return;
label_38:
      string str1 = PXDataUtils.FieldName<ARAdjust.curyAdjgPPDAmt>();
      nullable1 = row.CuryAdjgPPDAmt;
      string str2 = nullable1.Value < 0M ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.";
      object[] objArray = new object[1]{ (object) 0 };
      throw new PXRowPersistingException(str1, (object) null, str2, objArray);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ARAdjust> e)
  {
    if (((PXGraph) this).UnattendedMode || e.TranStatus != null || this.IsAdjdRefNbrFieldVerifyingDisabled((IAdjustment) e.Row))
      return;
    ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<ARAdjust>>) e).Cache.VerifyFieldAndRaiseException<ARAdjust.adjdRefNbr>((object) e.Row);
  }

  /// <summary>Calc CuryAdjgPPDAmt and CuryAdjgAmt</summary>
  /// <param name="adj"></param>
  /// <param name="curyUnappliedBal"></param>
  /// <param name="checkBalance">Check that amount of UnappliedBal is greater than 0</param>
  /// <param name="trySelect">Try to alllay this ARAdjust</param>
  /// <returns>Changing amount of payment.CuryUnappliedBal</returns>
  public virtual Decimal applyARAdjust(
    ARAdjust adj,
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
        Decimal num8 = Math.Abs(amountForAdjustment) <= Math.Abs(num4) ? amountForAdjustment : num4;
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
      flag = true;
    }
    if (flag)
      ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryDocumentExtension>().CalcBalancesFromAdjustedDocument(adj, true, true);
    adj.Selected = new bool?(num6 != 0M || num3 != 0M);
    return curyUnappliedBal - num1;
  }

  protected virtual Decimal GetDiscAmountForAdjustment(ARAdjust adj)
  {
    return !(adj.AdjgDocType == "CRM") ? adj.CuryDiscBal.Value : 0M;
  }

  protected virtual void ARAdjust_Selected_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARAdjust row = (ARAdjust) e.Row;
    if (row.Released.GetValueOrDefault())
      return;
    bool? nullable = row.Voided;
    if (nullable.GetValueOrDefault() || this._IsVoidCheckInProgress || this.IsReverseProc || FlaggedModeScopeBase<ARPaymentEntry.AutoApplyProcess>.IsActive)
      return;
    ARPayment current = ((PXSelectBase<ARPayment>) this.Document).Current;
    bool checkBalance = !EnumerableExtensions.IsIn<string>(current.Status, "H", "B", "W");
    nullable = row.Selected;
    bool valueOrDefault = nullable.GetValueOrDefault();
    this.applyARAdjust(row, current.CuryUnappliedBal.Value, checkBalance, valueOrDefault);
  }

  public bool IsAdjdRefNbrFieldVerifyingDisabled(IAdjustment adj)
  {
    PXSelectJoin<ARPayment, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>>, Where<ARPayment.docType, Equal<Optional<ARPayment.docType>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>> document = this.Document;
    bool? nullable;
    int num;
    if (document == null)
    {
      num = 0;
    }
    else
    {
      nullable = (bool?) ((PXSelectBase<ARPayment>) document).Current?.VoidAppl;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0)
    {
      if (adj != null)
      {
        nullable = adj.Voided;
        if (nullable.GetValueOrDefault())
          goto label_10;
      }
      if (adj != null)
      {
        nullable = adj.Released;
        if (nullable.GetValueOrDefault())
          goto label_10;
      }
      if (!this.AutoPaymentApp && !this.ForcePaymentApp && !this.IsReverseProc)
        return this.HasUnreleasedSOInvoice;
    }
label_10:
    return true;
  }

  protected virtual void ARAdjust_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARAdjust row = e.Row as ARAdjust;
    ((CancelEventArgs) e).Cancel = this.IsAdjdRefNbrFieldVerifyingDisabled((IAdjustment) row);
  }

  protected virtual void ARAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    ARAdjust row = (ARAdjust) e.Row;
    bool flag = true;
    if (PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>())
    {
      ARRegister paymentsByLineAllowed = this.GetAdjdInvoiceToVerifyArePaymentsByLineAllowed(sender, row);
      flag = paymentsByLineAllowed == null || !paymentsByLineAllowed.PaymentsByLinesAllowed.GetValueOrDefault();
    }
    if (!flag)
      return;
    this.InitApplicationData(row);
  }

  protected virtual void ARAdjust_AdjdLineNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null)
      return;
    ARAdjust row = (ARAdjust) e.Row;
    if ((((CancelEventArgs) e).Cancel = this.IsAdjdRefNbrFieldVerifyingDisabled((IAdjustment) row)) || e.NewValue != null || PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>())
      return;
    ARRegister paymentsByLineAllowed = this.GetAdjdInvoiceToVerifyArePaymentsByLineAllowed(sender, row);
    if ((paymentsByLineAllowed != null ? (paymentsByLineAllowed.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("The document was created when the Payment Application by Line feature was enabled. Paying this document can cause inconsistency in balances. To pay the document, on the Invoices and Memos (AR301000) form, select Actions > Enter Payment/Apply Memo.");
  }

  protected virtual void ARAdjust_AdjdLineNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    ARAdjust row = (ARAdjust) e.Row;
    int? adjdLineNbr = row.AdjdLineNbr;
    int num = 0;
    if (adjdLineNbr.GetValueOrDefault() == num & adjdLineNbr.HasValue)
      return;
    adjdLineNbr = row.AdjdLineNbr;
    if (!adjdLineNbr.HasValue)
      return;
    this.InitApplicationData(row);
  }

  protected virtual void ARPaymentChargeTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is ARPaymentChargeTran row))
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, row.AccountID);
    if (account == null)
      return;
    int? nullable = row.ProjectID;
    int id = NonProject.ID;
    if (nullable.GetValueOrDefault() == id & nullable.HasValue)
      return;
    nullable = account.AccountGroupID;
    if (nullable.HasValue)
      return;
    ((PXSelectBase) this.PaymentCharges).Cache.RaiseExceptionHandling<ARPaymentChargeTran.accountID>((object) row, (object) account.AccountCD, (Exception) new PXSetPropertyException("The offset account specified in the project-related line is not mapped to an account group. Assign an account group to the {0} account or select the non-project code in the line.", (PXErrorLevel) 4, new object[1]
    {
      (object) account.AccountCD
    }));
  }

  protected virtual void ARPaymentChargeTran_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is ARPaymentChargeTran row))
      return;
    bool flag = ProjectAttribute.IsPMVisible("AR");
    PXDefaultAttribute.SetPersistingCheck<ARPaymentChargeTran.projectID>(sender, e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, row.AccountID);
    if (account == null)
      return;
    int? nullable = row.ProjectID;
    int id = NonProject.ID;
    if (nullable.GetValueOrDefault() == id & nullable.HasValue)
      return;
    nullable = account.AccountGroupID;
    if (!nullable.HasValue)
      throw new PXRowPersistingException(typeof (ARPaymentChargeTran.accountID).Name, (object) null, "The offset account specified in the project-related line is not mapped to an account group. Assign an account group to the {0} account or select the non-project code in the line.", new object[1]
      {
        (object) "accountID"
      });
  }

  private void InitApplicationData(ARAdjust adj)
  {
    try
    {
      using (IEnumerator<PXResult<ARInvoice>> enumerator = ((PXSelectBase<ARInvoice>) this.ARInvoice_DocType_RefNbr).Select(new object[3]
      {
        (object) adj.AdjdLineNbr.GetValueOrDefault(),
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran> current = (PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>) enumerator.Current;
          ARInvoice arInvoice = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(current);
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(current);
          ARTran tran = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, Customer, ARTran>.op_Implicit(current);
          ARRegisterAlias arRegisterAlias = PX.Objects.Common.Utilities.Clone<ARInvoice, ARRegisterAlias>((PXGraph) this, arInvoice);
          ARAdjust arAdjust = adj;
          foreach (string key in (IEnumerable<string>) ((PXSelectBase) this.Adjustments).Cache.Keys)
          {
            if (((PXSelectBase) this.Adjustments).Cache.GetValue((object) arAdjust, key) == null)
              ((PXSelectBase) this.Adjustments).Cache.SetDefaultExt((object) arAdjust, key, (object) null);
          }
          PXParentAttribute.SetParent(((PXSelectBase) this.Adjustments).Cache, (object) arAdjust, typeof (ARInvoice), (object) arInvoice);
          PXSelectorAttribute.StoreResult<ARAdjust.adjdRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) adj, (IBqlTable) arInvoice);
          PXSelectJoin<ARAdjust, LeftJoin<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARAdjust.adjdDocType>, And<ARRegisterAlias.refNbr, Equal<ARAdjust.adjdRefNbr>>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARAdjust.adjdDocType>, And<ARTran.refNbr, Equal<ARAdjust.adjdRefNbr>, And<ARTran.lineNbr, Equal<ARAdjust.adjdLineNbr>>>>>>>>, Where<ARAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<ARAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>, And<ARAdjust.released, NotEqual<True>>>>> adjustments = this.Adjustments;
          List<object> objectList = new List<object>();
          objectList.Add((object) new PXResult<ARInvoice, ARRegisterAlias, ARTran>(arInvoice, arRegisterAlias, tran));
          object[] objArray1 = new object[4]
          {
            (object) arInvoice,
            (object) arRegisterAlias,
            (object) tran,
            (object) arAdjust
          };
          object[] objArray2 = new object[2]
          {
            (object) arAdjust.AdjgDocType,
            (object) arAdjust.AdjgRefNbr
          };
          ((PXSelectBase<ARAdjust>) adjustments).StoreTailResult(objectList, objArray1, objArray2);
          this.ARAdjust_KeyUpdated<ARInvoice>(adj, arInvoice, currencyInfo, tran);
          return;
        }
      }
      foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in ((PXSelectBase<ARPayment>) this.ARPayment_DocType_RefNbr).Select(new object[2]
      {
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }))
      {
        ARPayment invoice = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        this.ARAdjust_KeyUpdated<ARPayment>(adj, invoice, currencyInfo);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  private void ARAdjust_KeyUpdated<T>(
    ARAdjust adj,
    T invoice,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    ARTran tran = null)
    where T : ARRegister, IInvoice, new()
  {
    ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().StoreResult(currencyInfo);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().CloneCurrencyInfo(currencyInfo, ((PXSelectBase<ARPayment>) this.Document).Current.DocDate);
    bool flag = ((PXSelectBase<ARPayment>) this.Document).Current.DocType == "REF" && invoice.DocType == "PPI";
    adj.CustomerID = ((PXSelectBase<ARPayment>) this.Document).Current.CustomerID;
    adj.AdjgDocDate = ((PXSelectBase<ARPayment>) this.Document).Current.AdjDate;
    adj.AdjgCuryInfoID = ((PXSelectBase<ARPayment>) this.Document).Current.CuryInfoID;
    adj.AdjdCustomerID = invoice.CustomerID;
    adj.AdjdCuryInfoID = currencyInfo1.CuryInfoID;
    adj.AdjdOrigCuryInfoID = invoice.CuryInfoID;
    if (((PXSelectBase<ARPayment>) this.Document).Current.DocType == "CRM")
    {
      adj.InvoiceID = invoice.NoteID;
      adj.PaymentID = new Guid?();
      adj.MemoID = ((PXSelectBase<ARPayment>) this.Document).Current.NoteID;
    }
    else if (invoice.DocType == "CRM")
    {
      adj.InvoiceID = new Guid?();
      adj.PaymentID = ((PXSelectBase<ARPayment>) this.Document).Current.NoteID;
      adj.MemoID = invoice.NoteID;
    }
    else
    {
      adj.InvoiceID = invoice.NoteID;
      adj.PaymentID = ((PXSelectBase<ARPayment>) this.Document).Current.NoteID;
      adj.MemoID = new Guid?();
    }
    ((PXSelectBase) this.Adjustments).Cache.SetValue<ARAdjust.adjdBranchID>((object) adj, (object) invoice.BranchID);
    adj.AdjdARAcct = flag ? invoice.PrepaymentAccountID : invoice.ARAccountID;
    adj.AdjdARSub = flag ? invoice.PrepaymentSubID : invoice.ARSubID;
    adj.AdjdDocDate = invoice.DocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjdFinPeriodID>(((PXSelectBase) this.Adjustments).Cache, (object) adj, invoice.TranPeriodID);
    adj.AdjdHasPPDTaxes = invoice.HasPPDTaxes;
    adj.Released = new bool?(false);
    adj.PendingPPD = new bool?(false);
    adj.CuryAdjgAmt = new Decimal?(0M);
    adj.CuryAdjgDiscAmt = new Decimal?(0M);
    adj.CuryAdjgPPDAmt = new Decimal?(0M);
    adj.CuryAdjgWhTaxAmt = new Decimal?(0M);
    adj.AdjdCuryID = currencyInfo.CuryID;
    ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryDocumentExtension>().CalcBalances<T>(adj, invoice, false, true, tran);
    Decimal? curyUnappliedBal = ((PXSelectBase<ARPayment>) this.Document).Current.CuryUnappliedBal;
    Decimal? valuePending1 = ((PXSelectBase) this.Adjustments).Cache.GetValuePending<ARAdjust.curyAdjgAmt>((object) adj) as Decimal?;
    Decimal? valuePending2 = ((PXSelectBase) this.Adjustments).Cache.GetValuePending<ARAdjust.curyAdjgDiscAmt>((object) adj) as Decimal?;
    if (!((PXGraph) this).IsContractBasedAPI)
    {
      ((PXSelectBase) this.Adjustments).Cache.SetValuePending<ARAdjust.curyAdjgAmt>((object) adj, (object) null);
      ((PXSelectBase) this.Adjustments).Cache.SetValuePending<ARAdjust.curyAdjgDiscAmt>((object) adj, (object) null);
    }
    Decimal? nullable1 = valuePending1;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      nullable1 = valuePending2;
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
        return;
    }
    nullable1 = adj.CuryOrigDocAmt;
    Decimal num3 = 0M;
    Sign sign1 = nullable1.GetValueOrDefault() < num3 & nullable1.HasValue ? Sign.Minus : Sign.Plus;
    Decimal? nullable2 = new Decimal?(this.GetDiscAmountForAdjustment(adj));
    nullable1 = adj.CuryDocBal;
    Decimal? nullable3 = nullable2;
    Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    nullable1 = adj.CuryDiscBal;
    Sign sign2 = sign1;
    nullable3 = nullable1.HasValue ? new Decimal?(Sign.op_Multiply(nullable1.GetValueOrDefault(), sign2)) : new Decimal?();
    Decimal num4 = 0M;
    if (nullable3.GetValueOrDefault() >= num4 & nullable3.HasValue)
    {
      Decimal? curyDocBal = adj.CuryDocBal;
      Decimal? curyDiscBal = adj.CuryDiscBal;
      nullable1 = curyDocBal.HasValue & curyDiscBal.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - curyDiscBal.GetValueOrDefault()) : new Decimal?();
      Sign sign3 = sign1;
      nullable3 = nullable1.HasValue ? new Decimal?(Sign.op_Multiply(nullable1.GetValueOrDefault(), sign3)) : new Decimal?();
      Decimal num5 = 0M;
      if (nullable3.GetValueOrDefault() <= num5 & nullable3.HasValue)
        return;
    }
    if (((PXSelectBase<ARPayment>) this.Document).Current != null)
    {
      nullable3 = adj.AdjgBalSign;
      Decimal num6 = 0M;
      if (nullable3.GetValueOrDefault() < num6 & nullable3.HasValue || Sign.op_Equality(sign1, Sign.Minus))
      {
        nullable3 = curyUnappliedBal;
        Decimal num7 = 0M;
        if (nullable3.GetValueOrDefault() < num7 & nullable3.HasValue)
        {
          nullable4 = new Decimal?(Math.Min(nullable4.Value, Math.Abs(curyUnappliedBal.Value)));
          goto label_31;
        }
        goto label_31;
      }
    }
    Decimal? nullable5;
    Decimal? nullable6;
    if (((PXSelectBase<ARPayment>) this.Document).Current != null)
    {
      nullable3 = curyUnappliedBal;
      Decimal num8 = 0M;
      if (nullable3.GetValueOrDefault() > num8 & nullable3.HasValue)
      {
        nullable3 = adj.AdjgBalSign;
        Decimal num9 = 0M;
        if (nullable3.GetValueOrDefault() > num9 & nullable3.HasValue)
        {
          nullable4 = new Decimal?(Math.Min(nullable4.Value, curyUnappliedBal.Value));
          Decimal? nullable7 = nullable4;
          nullable5 = nullable2;
          nullable6 = nullable7.HasValue & nullable5.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
          Sign sign4 = sign1;
          Decimal? nullable8;
          if (!nullable6.HasValue)
          {
            nullable5 = new Decimal?();
            nullable8 = nullable5;
          }
          else
            nullable8 = new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign4));
          nullable3 = nullable8;
          nullable6 = adj.CuryDocBal;
          Sign sign5 = sign1;
          Decimal? nullable9;
          if (!nullable6.HasValue)
          {
            nullable5 = new Decimal?();
            nullable9 = nullable5;
          }
          else
            nullable9 = new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign5));
          nullable1 = nullable9;
          if (nullable3.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable3.HasValue & nullable1.HasValue)
          {
            nullable2 = new Decimal?(0M);
            goto label_31;
          }
          goto label_31;
        }
      }
    }
    if (((PXSelectBase<ARPayment>) this.Document).Current != null)
    {
      nullable1 = curyUnappliedBal;
      Decimal num10 = 0M;
      if (nullable1.GetValueOrDefault() <= num10 & nullable1.HasValue)
      {
        nullable1 = ((PXSelectBase<ARPayment>) this.Document).Current.CuryOrigDocAmt;
        Decimal num11 = 0M;
        if (nullable1.GetValueOrDefault() > num11 & nullable1.HasValue)
        {
          nullable4 = new Decimal?(0M);
          nullable2 = new Decimal?(0M);
        }
      }
    }
label_31:
    if (valuePending1.HasValue)
    {
      nullable6 = valuePending1;
      Sign sign6 = sign1;
      Decimal? nullable10;
      if (!nullable6.HasValue)
      {
        nullable5 = new Decimal?();
        nullable10 = nullable5;
      }
      else
        nullable10 = new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign6));
      nullable1 = nullable10;
      nullable6 = nullable4;
      Sign sign7 = sign1;
      Decimal? nullable11;
      if (!nullable6.HasValue)
      {
        nullable5 = new Decimal?();
        nullable11 = nullable5;
      }
      else
        nullable11 = new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign7));
      nullable3 = nullable11;
      if (nullable1.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue)
        nullable4 = valuePending1;
    }
    if (valuePending2.HasValue)
    {
      nullable6 = valuePending2;
      Sign sign8 = sign1;
      Decimal? nullable12;
      if (!nullable6.HasValue)
      {
        nullable5 = new Decimal?();
        nullable12 = nullable5;
      }
      else
        nullable12 = new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign8));
      nullable3 = nullable12;
      nullable6 = nullable2;
      Sign sign9 = sign1;
      Decimal? nullable13;
      if (!nullable6.HasValue)
      {
        nullable5 = new Decimal?();
        nullable13 = nullable5;
      }
      else
        nullable13 = new Decimal?(Sign.op_Multiply(nullable6.GetValueOrDefault(), sign9));
      nullable1 = nullable13;
      if (nullable3.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable3.HasValue & nullable1.HasValue)
        nullable2 = valuePending2;
    }
    adj.CuryAdjgAmt = nullable4;
    adj.CuryAdjgDiscAmt = nullable2;
    adj.CuryAdjgPPDAmt = nullable2;
    adj.CuryAdjgWOAmt = new Decimal?(0M);
    ARAdjust arAdjust = adj;
    nullable1 = adj.CuryAdjgAmt;
    Decimal num12 = 0M;
    int num13;
    if (nullable1.GetValueOrDefault() == num12 & nullable1.HasValue)
    {
      nullable1 = adj.CuryAdjgPPDAmt;
      Decimal num14 = 0M;
      num13 = !(nullable1.GetValueOrDefault() == num14 & nullable1.HasValue) ? 1 : 0;
    }
    else
      num13 = 1;
    bool? nullable14 = new bool?(num13 != 0);
    arAdjust.Selected = nullable14;
    ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryDocumentExtension>().CalcBalances<T>(adj, invoice, true, true, tran);
    PXCache<ARAdjust>.SyncModel(adj);
  }

  protected virtual void ARAdjust_AdjdCuryRate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() <= num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected virtual void CurrencyInfo_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<PX.Objects.CM.Extensions.CurrencyInfo.curyID, PX.Objects.CM.Extensions.CurrencyInfo.curyRate, PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv>(e.Row, e.OldRow))
      return;
    if ((e.Row is PX.Objects.CM.Extensions.CurrencyInfo row ? (!row.CuryRate.HasValue ? 1 : 0) : 1) != 0)
    {
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<ARPayment.adjDate>((object) ((PXSelectBase<ARPayment>) this.Document).Current, (object) ((PXSelectBase<ARPayment>) this.Document).Current.AdjDate, (Exception) new PXSetPropertyException("Currency Rate is not defined.", (PXErrorLevel) 3));
    }
    else
    {
      foreach (PXResult<ARAdjust> pxResult in PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjgCuryInfoID, Equal<Required<ARAdjust.adjgCuryInfoID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) ((PX.Objects.CM.Extensions.CurrencyInfo) e.Row).CuryInfoID
      }))
      {
        ARAdjust adj = PXResult<ARAdjust>.op_Implicit(pxResult);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments).Cache, (object) adj);
        ((PXGraph) this).GetExtension<ARPaymentEntry.ARPaymentEntryDocumentExtension>().CalcBalancesFromAdjustedDocument(adj, true, true);
        Decimal? nullable = adj.CuryDocBal;
        Decimal num1 = 0M;
        if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
          ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust.curyAdjgAmt>((object) adj, (object) adj.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
        nullable = adj.CuryDiscBal;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
          ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust.curyAdjgPPDAmt>((object) adj, (object) adj.CuryAdjgPPDAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
        nullable = adj.CuryWOBal;
        Decimal num3 = 0M;
        if (nullable.GetValueOrDefault() < num3 & nullable.HasValue)
          ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust.curyAdjgWOAmt>((object) adj, (object) adj.CuryAdjgWOAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      }
    }
  }

  protected virtual void ARPayment_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    bool flag = row.DocType == "CRM" || row.DocType == "SMB" || row.IsPrepaymentInvoiceDocument();
    if (!flag && !row.CashAccountID.HasValue)
    {
      if (sender.RaiseExceptionHandling<ARPayment.cashAccountID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[cashAccountID]"
      })))
        throw new PXRowPersistingException(typeof (ARPayment.cashAccountID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "cashAccountID"
        });
    }
    if (!flag && string.IsNullOrEmpty(row.PaymentMethodID))
    {
      if (sender.RaiseExceptionHandling<ARPayment.paymentMethodID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[paymentMethodID]"
      })))
        throw new PXRowPersistingException(typeof (ARPayment.paymentMethodID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "paymentMethodID"
        });
    }
    bool? nullable1 = row.OpenDoc;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = row.Hold;
      if (!nullable1.GetValueOrDefault() && this.IsPaymentUnbalanced(row))
        throw new PXRowPersistingException(typeof (ARPayment.curyOrigDocAmt).Name, (object) row.CuryOrigDocAmt, "The document is out of the balance.");
    }
    PaymentRefAttribute.SetUpdateCashManager<ARPayment.extRefNbr>(sender, e.Row, ((ARRegister) e.Row).DocType != "RPM");
    nullable1 = row.OpenDoc;
    string errMsg;
    if (nullable1.GetValueOrDefault() && !this.VerifyAdjFinPeriodID(row, row.AdjFinPeriodID, out errMsg) && sender.RaiseExceptionHandling<ARPayment.adjFinPeriodID>(e.Row, (object) PeriodIDAttribute.FormatForDisplay(row.AdjFinPeriodID), (Exception) new PXSetPropertyException(errMsg)))
      throw new PXRowPersistingException(typeof (ARPayment.adjFinPeriodID).Name, (object) PeriodIDAttribute.FormatForError(row.AdjFinPeriodID), errMsg);
    if (ARDocType.IsSelfVoiding(row.DocType))
    {
      nullable1 = row.OpenDoc;
      if (nullable1.GetValueOrDefault())
      {
        Decimal? nullable2 = row.CuryApplAmt;
        if (nullable2.HasValue)
        {
          nullable2 = row.CuryApplAmt;
          Decimal num1 = Math.Abs(nullable2.GetValueOrDefault());
          nullable2 = row.CuryOrigDocAmt;
          Decimal num2 = Math.Abs(nullable2.GetValueOrDefault());
          if (num1 != num2)
            throw new PXRowPersistingException(typeof (ARPayment.curyOrigDocAmt).Name, (object) row.CuryOrigDocAmt, "The document is out of the balance.");
        }
      }
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>())
      return;
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current;
    bool? nullable3;
    if (current == null)
    {
      nullable1 = new bool?();
      nullable3 = nullable1;
    }
    else
      nullable3 = current.IsAccountNumberRequired;
    nullable1 = nullable3;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<ARPayment.pMInstanceID>(sender, (object) row, !flag & valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected internal bool InternalCall { get; set; }

  protected virtual void ARPayment_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ARPayment doc = e.Row as ARPayment;
    if (doc == null || this.InternalCall)
      return;
    ((PXAction) this.release).SetEnabled(true);
    ((PXGraph) this).Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)].AllowUpdate = true;
    ((PXGraph) this).Caches[typeof (ARTranPostBal)].AllowInsert = ((PXGraph) this).Caches[typeof (ARTranPostBal)].AllowUpdate = ((PXGraph) this).Caches[typeof (ARTranPostBal)].AllowDelete = false;
    ((PXGraph) this).Caches[typeof (ARAdjust2)].AllowInsert = ((PXGraph) this).Caches[typeof (ARAdjust2)].AllowUpdate = ((PXGraph) this).Caches[typeof (ARAdjust2)].AllowDelete = false;
    bool flag1 = !this.IsApprovalRequired(doc);
    bool? nullable1 = doc.DontApprove;
    bool flag2 = flag1;
    if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
      cache.SetValueExt<ARRegister.dontApprove>((object) doc, (object) flag1);
    bool flag3 = doc.DocType == "CRM" || doc.DocType == "SMB" || doc.DocType == "PPI";
    bool flag4 = false;
    if (!string.IsNullOrEmpty(doc.PaymentMethodID))
    {
      PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current;
      bool? nullable2;
      if (current == null)
      {
        nullable1 = new bool?();
        nullable2 = nullable1;
      }
      else
        nullable2 = current.IsAccountNumberRequired;
      nullable1 = nullable2;
      flag4 = nullable1.GetValueOrDefault();
    }
    PXUIFieldAttribute.SetVisible<ARPayment.curyID>(cache, (object) doc, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetVisible<ARPayment.cashAccountID>(cache, (object) doc, !flag3);
    PXUIFieldAttribute.SetVisible<ARPayment.cleared>(cache, (object) doc, !flag3);
    PXUIFieldAttribute.SetVisible<ARPayment.clearDate>(cache, (object) doc, !flag3);
    PXUIFieldAttribute.SetVisible<ARPayment.paymentMethodID>(cache, (object) doc, !flag3);
    PXUIFieldAttribute.SetVisible<ARPayment.pMInstanceID>(cache, (object) doc, !flag3);
    PXUIFieldAttribute.SetVisible<ARPayment.extRefNbr>(cache, (object) doc, !flag3);
    PXUIFieldAttribute.SetRequired<ARPayment.cashAccountID>(cache, !flag3);
    PXUIFieldAttribute.SetRequired<ARPayment.pMInstanceID>(cache, !flag3 & flag4);
    PXUIFieldAttribute.SetEnabled<ARPayment.pMInstanceID>(cache, e.Row, !flag3 & flag4);
    bool flag5 = doc.DocType == "PMT" || doc.DocType == "PPM" || doc.DocType == "REF" || doc.DocType == "CSL";
    PXCache pxCache1 = cache;
    ARPayment arPayment1 = doc;
    int num1;
    if (flag5)
    {
      nullable1 = doc.DepositAsBatch;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetVisible<ARPayment.depositAfter>(pxCache1, (object) arPayment1, num1 != 0);
    PXUIFieldAttribute.SetEnabled<ARPayment.depositAfter>(cache, (object) doc, false);
    PXCache pxCache2 = cache;
    int num2;
    if (flag5)
    {
      nullable1 = doc.DepositAsBatch;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetRequired<ARPayment.depositAfter>(pxCache2, num2 != 0);
    int num3;
    if (flag5)
    {
      nullable1 = doc.DepositAsBatch;
      if (nullable1.GetValueOrDefault())
      {
        num3 = 1;
        goto label_18;
      }
    }
    num3 = 2;
label_18:
    PXPersistingCheck pxPersistingCheck = (PXPersistingCheck) num3;
    PXDefaultAttribute.SetPersistingCheck<ARPayment.depositAfter>(cache, (object) doc, pxPersistingCheck);
    PXUIFieldAttribute.SetVisible<ARAdjust.adjdCustomerID>(((PXSelectBase) this.Adjustments).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());
    PXCache cache1 = cache;
    ARPayment data = doc;
    nullable1 = doc.OpenDoc;
    int isValidatePeriod = nullable1.GetValueOrDefault() ? 2 : 1;
    OpenPeriodAttribute.SetValidatePeriod<ARPayment.adjFinPeriodID>(cache1, (object) data, (PeriodValidation) isValidatePeriod);
    PX.Objects.CA.CashAccount current1 = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current;
    int? nullable3;
    int num4;
    if (current1 != null)
    {
      int? cashAccountId = current1.CashAccountID;
      nullable3 = doc.CashAccountID;
      if (cashAccountId.GetValueOrDefault() == nullable3.GetValueOrDefault() & cashAccountId.HasValue == nullable3.HasValue)
      {
        nullable1 = current1.ClearingAccount;
        num4 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_22;
      }
    }
    num4 = 0;
label_22:
    bool flag6 = num4 != 0;
    bool flag7 = false;
    nullable1 = doc.Hold;
    bool flag8 = false;
    bool flag9 = nullable1.GetValueOrDefault() == flag8 & nullable1.HasValue;
    nullable1 = doc.Hold;
    bool valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = doc.Released;
    bool flag10 = false;
    bool flag11 = nullable1.GetValueOrDefault() == flag10 & nullable1.HasValue;
    nullable1 = doc.Released;
    bool valueOrDefault2 = nullable1.GetValueOrDefault();
    nullable1 = doc.OpenDoc;
    bool valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = doc.Voided;
    bool flag12 = false;
    bool flag13 = nullable1.GetValueOrDefault() == flag12 & nullable1.HasValue;
    int num5;
    if (valueOrDefault1)
    {
      PX.Objects.CA.CashAccount current2 = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current;
      if (current2 == null)
      {
        num5 = 0;
      }
      else
      {
        nullable1 = current2.Reconcile;
        num5 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num5 = 0;
    bool flag14 = num5 != 0;
    bool flag15 = false;
    int num6 = doc.Status == "C" ? 1 : 0;
    bool flag16 = doc.DocType == "PMT" || doc.DocType == "PPM";
    nullable1 = doc.IsCCPayment;
    bool flag17 = nullable1.GetValueOrDefault() && (flag16 || EnumerableExtensions.IsIn<string>(doc.DocType, "REF", "RPM"));
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
    ExternalTranHelper.SharedTranStatus sharedTranStatus = ExternalTranHelper.GetSharedTranStatus((PXGraph) this, (IExternalTransaction) GraphHelper.RowCast<ExternalTransaction>((IEnumerable) ((PXSelectBase<ExternalTransaction>) this.ExternalTran).Select(Array.Empty<object>())).FirstOrDefault<ExternalTransaction>());
    bool flag18 = doc.DocType == "REF" && sharedTranStatus == ExternalTranHelper.SharedTranStatus.ClearState;
    bool flag19 = doc.DocType == "REF" && sharedTranStatus == ExternalTranHelper.SharedTranStatus.Synchronized;
    bool flag20 = false;
    nullable3 = doc.CCActualExternalTransactionID;
    if (nullable3.HasValue)
      flag20 = GraphHelper.RowCast<CCProcTran>((IEnumerable) ((PXSelectBase<CCProcTran>) this.ccProcTran).Select(Array.Empty<object>())).FirstOrDefault<CCProcTran>((Func<CCProcTran, bool>) (i =>
      {
        int? transactionId = i.TransactionID;
        int? externalTransactionId = doc.CCActualExternalTransactionID;
        return transactionId.GetValueOrDefault() == externalTransactionId.GetValueOrDefault() & transactionId.HasValue == externalTransactionId.HasValue;
      }))?.ProcStatus == "OPN";
    bool flag21 = string.Equals(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current?.PaymentType, "EFT");
    bool flag22 = (((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current?.PaymentType == "CCD" || ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current?.PaymentType == "EFT") && doc.DocType != "REF" && !flag20;
    int num7;
    if (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() & flag22 && this.ShowCardChck(doc) && (!transactionState.IsActive || transactionState.ProcessingStatus == ProcessingStatus.AuthorizeExpired || transactionState.ProcessingStatus == ProcessingStatus.CaptureExpired))
    {
      PX.Objects.CA.PaymentMethod current3 = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current;
      if (current3 == null)
      {
        num7 = 0;
      }
      else
      {
        nullable1 = current3.ARIsProcessingRequired;
        num7 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num7 = 0;
    bool flag23 = num7 != 0;
    PXUIFieldAttribute.SetEnabled<ARPayment.newCard>(cache, (object) doc, flag22 && !flag21);
    PXUIFieldAttribute.SetVisible<ARPayment.newCard>(cache, (object) doc, flag23 && !flag21);
    PXUIFieldAttribute.SetEnabled<ARPayment.newAccount>(cache, (object) doc, flag22 & flag21);
    PXUIFieldAttribute.SetVisible<ARPayment.newAccount>(cache, (object) doc, flag23 & flag21);
    string savePaymentProfiles = ((PXSelectBase<CustomerClass>) this.customerclass).Current?.SavePaymentProfiles;
    bool flag24 = false;
    bool flag25 = false;
    if (savePaymentProfiles == "A")
    {
      CCProcessingCenter current4 = ((PXSelectBase<CCProcessingCenter>) this.processingCenter).Current;
      nullable1 = doc.NewCard;
      int num8;
      if (nullable1.GetValueOrDefault() & flag23)
      {
        if (current4 == null)
        {
          num8 = 0;
        }
        else
        {
          nullable1 = current4.AllowSaveProfile;
          num8 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num8 = 0;
      flag24 = num8 != 0;
      int num9;
      if (flag22)
      {
        if (current4 == null)
        {
          num9 = 0;
        }
        else
        {
          nullable1 = current4.AllowSaveProfile;
          num9 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num9 = 0;
      flag25 = num9 != 0;
    }
    PXUIFieldAttribute.SetEnabled<ARPayment.saveCard>(cache, (object) doc, flag25 && !flag21);
    PXUIFieldAttribute.SetVisible<ARPayment.saveCard>(cache, (object) doc, flag24 && !flag21);
    PXUIFieldAttribute.SetEnabled<ARPayment.saveAccount>(cache, (object) doc, flag25 & flag21);
    PXUIFieldAttribute.SetVisible<ARPayment.saveAccount>(cache, (object) doc, flag24 & flag21);
    int num10;
    if (flag17)
    {
      nullable1 = doc.Voided;
      num10 = !nullable1.Value ? 1 : 0;
    }
    else
      num10 = 0;
    bool flag26 = num10 != 0;
    this.CheckCashAccount(cache, doc);
    bool flag27 = AutoNumberAttribute.IsViewOnlyRecord<ARPayment.refNbr>(cache, (object) doc);
    OrdersToApplyTab applyTabExtension = this.GetOrdersToApplyTabExtension();
    object obj;
    if (applyTabExtension == null)
      obj = (object) null;
    else
      obj = ((PXSelectBase) applyTabExtension.SOAdjustments_Raw).View.SelectSingleBound(new object[1]
      {
        e.Row
      }, Array.Empty<object>());
    bool flag28 = obj > null;
    nullable3 = doc.CustomerID;
    bool flag29 = nullable3.HasValue && ((IQueryable<PXResult<ARAdjust>>) ((PXSelectBase<ARAdjust>) this.Adjustments_Raw).Select(Array.Empty<object>())).Any<PXResult<ARAdjust>>();
    this.HasUnreleasedSOInvoice = flag11 & flag28 && ((IQueryable<PXResult<PX.Objects.AR.Standalone.ARRegister>>) ((PXSelectBase<PX.Objects.AR.Standalone.ARRegister>) this.Adjustments_Invoices_Unreleased).Select(Array.Empty<object>())).Any<PXResult<PX.Objects.AR.Standalone.ARRegister>>();
    if (flag27)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
      cache.AllowUpdate = false;
      cache.AllowDelete = false;
      ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
      ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
      ((PXAction) this.release).SetEnabled(false);
    }
    else
    {
      nullable1 = doc.Voided;
      if (nullable1.Value)
      {
        PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
        cache.AllowDelete = false;
        cache.AllowUpdate = false;
        ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
        ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
        ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
        ((PXAction) this.release).SetEnabled(false);
      }
      else
      {
        nullable1 = doc.VoidAppl;
        if (nullable1.Value & flag11)
        {
          PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<ARPayment.adjDate>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARPayment.adjFinPeriodID>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARRegister.docDesc>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, (object) doc, true);
          PXCache pxCache3 = cache;
          ARPayment arPayment2 = doc;
          int num11;
          if (flag5)
          {
            nullable1 = doc.DepositAsBatch;
            num11 = nullable1.GetValueOrDefault() ? 1 : 0;
          }
          else
            num11 = 0;
          PXUIFieldAttribute.SetEnabled<ARPayment.depositAfter>(pxCache3, (object) arPayment2, num11 != 0);
          cache.AllowUpdate = true;
          cache.AllowDelete = true;
          ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
          ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
          ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
          ((PXAction) this.release).SetEnabled(flag9);
        }
        else if (valueOrDefault2 & valueOrDefault3)
        {
          flag15 = GraphHelper.RowCast<ARAdjust>((IEnumerable) ((PXSelectBase<ARAdjust>) this.Adjustments_Raw).Select(Array.Empty<object>())).TakeWhile<ARAdjust>((Func<ARAdjust, bool>) (adj => !adj.Voided.GetValueOrDefault())).Any<ARAdjust>((Func<ARAdjust, bool>) (adj => adj.Hold.GetValueOrDefault()));
          PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<ARPayment.adjDate>(cache, (object) doc, !flag15);
          PXUIFieldAttribute.SetEnabled<ARPayment.adjFinPeriodID>(cache, (object) doc, !flag15);
          PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, (object) doc, !flag15);
          cache.AllowDelete = false;
          cache.AllowUpdate = !flag15;
          PXCache cache2 = ((PXSelectBase) this.Adjustments).Cache;
          int num12;
          if (!flag15)
          {
            nullable1 = doc.PaymentsByLinesAllowed;
            num12 = !nullable1.GetValueOrDefault() ? 1 : 0;
          }
          else
            num12 = 0;
          cache2.AllowDelete = num12 != 0;
          PXCache cache3 = ((PXSelectBase) this.Adjustments).Cache;
          int num13;
          if (!flag15)
          {
            nullable1 = doc.SelfVoidingDoc;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = doc.PaymentsByLinesAllowed;
              num13 = !nullable1.GetValueOrDefault() ? 1 : 0;
              goto label_69;
            }
          }
          num13 = 0;
label_69:
          cache3.AllowInsert = num13 != 0;
          PXCache cache4 = ((PXSelectBase) this.Adjustments).Cache;
          int num14;
          if (!flag15)
          {
            nullable1 = doc.PaymentsByLinesAllowed;
            num14 = !nullable1.GetValueOrDefault() ? 1 : 0;
          }
          else
            num14 = 0;
          cache4.AllowUpdate = num14 != 0;
          ((PXAction) this.release).SetEnabled(((!flag9 ? 0 : (!flag15 ? 1 : 0)) & (flag29 ? 1 : 0)) != 0);
        }
        else if (this.HasUnreleasedSOInvoice)
        {
          PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<ARPayment.newCard>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<ARPayment.newAccount>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARRegister.docDesc>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARRegister.aRAccountID>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARRegister.aRSubID>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARPayment.extRefNbr>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARPayment.adjDate>(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<ARPayment.adjFinPeriodID>(cache, (object) doc, true);
          cache.AllowDelete = !flag26 || !transactionState.IsPreAuthorized && !transactionState.IsSettlementDue;
          cache.AllowUpdate = true;
          ((PXSelectBase) this.Adjustments).Cache.AllowDelete = true;
          ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
          ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
          ((PXAction) this.release).SetEnabled(flag9);
        }
        else if (valueOrDefault2 && !valueOrDefault3)
        {
          PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
          cache.AllowDelete = false;
          cache.AllowUpdate = flag6;
          ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
          ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
          ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
          ((PXAction) this.release).SetEnabled(false);
        }
        else
        {
          nullable1 = doc.VoidAppl;
          if (nullable1.Value)
          {
            PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
            cache.AllowDelete = false;
            cache.AllowUpdate = false;
            ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
            ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
            ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
            ((PXAction) this.release).SetEnabled(flag9);
          }
          else if (flag26 && !flag18 && ((transactionState.IsPreAuthorized || transactionState.IsSettlementDue || transactionState.NeedSync ? 1 : (transactionState.IsImportedUnknown ? 1 : 0)) | (flag19 ? 1 : 0)) != 0)
          {
            PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
            PXUIFieldAttribute.SetEnabled<ARPayment.adjDate>(cache, (object) doc, true);
            PXUIFieldAttribute.SetEnabled<ARPayment.adjFinPeriodID>(cache, (object) doc, true);
            PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, (object) doc, true);
            cache.AllowDelete = false;
            cache.AllowUpdate = true;
            ((PXSelectBase) this.Adjustments).Cache.AllowDelete = true;
            ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = true;
            ((PXSelectBase) this.Adjustments).Cache.AllowInsert = true;
            ((PXAction) this.release).SetEnabled(flag9);
          }
          else
          {
            CATran caTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATran.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) doc.CATranID
            }));
            bool flag30 = caTran != null && caTran.RefTranID.HasValue;
            PXUIFieldAttribute.SetEnabled(cache, (object) doc, true);
            PXUIFieldAttribute.SetEnabled<ARPayment.status>(cache, (object) doc, false);
            PXUIFieldAttribute.SetEnabled<ARPayment.curyID>(cache, (object) doc, flag7);
            cache.AllowDelete = !ExternalTranHelper.HasSuccessfulTrans((PXSelectBase<ExternalTransaction>) this.ExternalTran) | flag18;
            cache.AllowUpdate = true;
            ((PXSelectBase) this.Adjustments).Cache.AllowDelete = true;
            ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = true;
            ((PXSelectBase) this.Adjustments).Cache.AllowInsert = true;
            ((PXAction) this.release).SetEnabled(flag9);
            PXUIFieldAttribute.SetEnabled<ARPayment.curyOrigDocAmt>(cache, (object) doc, !flag30);
            PXUIFieldAttribute.SetEnabled<ARPayment.cashAccountID>(cache, (object) doc, !flag30);
            PXUIFieldAttribute.SetEnabled<ARPayment.pMInstanceID>(cache, (object) doc, !flag30 & flag4);
            PXUIFieldAttribute.SetEnabled<ARPayment.paymentMethodID>(cache, (object) doc, !flag30);
            PXUIFieldAttribute.SetEnabled<ARPayment.extRefNbr>(cache, (object) doc, !flag30);
            PXUIFieldAttribute.SetEnabled<ARPayment.customerID>(cache, (object) doc, !flag30);
            PXUIFieldAttribute.SetEnabled<ARPayment.docDate>(cache, (object) doc, false);
            PXUIFieldAttribute.SetEnabled<ARPayment.finPeriodID>(cache, (object) doc, false);
          }
        }
      }
    }
    if (doc.DocType == "VRF")
      PXUIFieldAttribute.SetEnabled<ARPayment.extRefNbr>(cache, (object) doc, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.docType>(cache, (object) doc);
    PXUIFieldAttribute.SetEnabled<ARPayment.refNbr>(cache, (object) doc);
    PXUIFieldAttribute.SetEnabled<ARPayment.curyUnappliedBal>(cache, (object) doc, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.curyApplAmt>(cache, (object) doc, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.curyWOAmt>(cache, (object) doc, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.batchNbr>(cache, (object) doc, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.cleared>(cache, (object) doc, flag14);
    PXCache pxCache4 = cache;
    ARPayment arPayment3 = doc;
    int num15;
    if (flag14)
    {
      nullable1 = doc.Cleared;
      num15 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num15 = 0;
    PXUIFieldAttribute.SetEnabled<ARPayment.clearDate>(pxCache4, (object) arPayment3, num15 != 0);
    bool flag31 = false;
    if (valueOrDefault2 & flag13 && ARPaymentType.VoidEnabled(doc))
      flag31 = true;
    if (!valueOrDefault2 & flag16 & flag13 && ExternalTranHelper.HasTransactions((PXSelectBase<ExternalTransaction>) this.ExternalTran) && ((transactionState.IsCaptured || transactionState.IsPreAuthorized ? 0 : (!transactionState.IsImportedUnknown ? 1 : 0)) | (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() ? (false ? 1 : 0) : (doc.Status == "W" ? 1 : 0))) != 0)
      flag31 = true;
    bool flag32 = !flag19 && !(ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran) & flag16);
    ((PXAction) this.voidCheck).SetEnabled(((!flag31 ? 0 : (!flag15 ? 1 : 0)) & (flag32 ? 1 : 0)) != 0);
    nullable3 = doc.CustomerID;
    int num16;
    if (nullable3.HasValue)
    {
      nullable1 = doc.OpenDoc;
      if (nullable1.Value && !flag15 && (doc.DocType == "PMT" || doc.DocType == "PPM" || doc.DocType == "CRM"))
      {
        nullable1 = doc.PaymentsByLinesAllowed;
        num16 = !nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_95;
      }
    }
    num16 = 0;
label_95:
    bool flag33 = num16 != 0;
    ((PXAction) this.loadInvoices).SetEnabled(flag33);
    PXAction<ARPayment> adjustDocAmt = this.adjustDocAmt;
    Decimal? nullable4;
    int num17;
    if (((PXFieldState) cache.GetStateExt<ARPayment.curyOrigDocAmt>((object) doc)).Enabled)
    {
      nullable4 = doc.CuryApplAmt;
      Decimal num18 = 0M;
      if (nullable4.GetValueOrDefault() == num18 & nullable4.HasValue)
      {
        nullable4 = doc.CurySOApplAmt;
        Decimal num19 = 0M;
        num17 = !(nullable4.GetValueOrDefault() == num19 & nullable4.HasValue) ? 1 : 0;
      }
      else
        num17 = 1;
    }
    else
      num17 = 0;
    ((PXAction) adjustDocAmt).SetEnabled(num17 != 0);
    this.SetDocTypeList(e.Row);
    ((PXAction) this.editCustomer).SetEnabled(((PXSelectBase<Customer>) this.customer)?.Current != null);
    nullable3 = doc.CustomerID;
    if (nullable3.HasValue && flag29 | flag28)
      PXUIFieldAttribute.SetEnabled<ARPayment.customerID>(cache, (object) doc, false);
    PXAction<ARPayment> autoApply = this.autoApply;
    int num20;
    if (flag33)
    {
      nullable4 = doc.CuryOrigDocAmt;
      Decimal num21 = 0M;
      num20 = nullable4.GetValueOrDefault() > num21 & nullable4.HasValue ? 1 : 0;
    }
    else
      num20 = 0;
    int num22 = flag29 ? 1 : 0;
    int num23 = num20 & num22;
    ((PXAction) autoApply).SetEnabled(num23 != 0);
    PXUIFieldAttribute.SetEnabled<ARPayment.cCPaymentStateDescr>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.depositDate>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.depositAsBatch>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.deposited>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARPayment.depositNbr>(cache, (object) null, false);
    this.CalcApplAmounts(cache, doc);
    bool flag34 = !string.IsNullOrEmpty(doc.DepositNbr) && !string.IsNullOrEmpty(doc.DepositType);
    int num24;
    if (!flag34 && current1 != null)
    {
      if (!flag6)
      {
        nullable1 = doc.DepositAsBatch;
        bool flag35 = flag6;
        num24 = !(nullable1.GetValueOrDefault() == flag35 & nullable1.HasValue) ? 1 : 0;
      }
      else
        num24 = 1;
    }
    else
      num24 = 0;
    bool flag36 = num24 != 0;
    if (flag36)
    {
      nullable1 = doc.DepositAsBatch;
      bool flag37 = flag6;
      PXSetPropertyException propertyException = !(nullable1.GetValueOrDefault() == flag37 & nullable1.HasValue) ? new PXSetPropertyException("'Batch deposit' setting does not match 'Clearing Account' flag of the Cash Account", (PXErrorLevel) 2) : (PXSetPropertyException) null;
      cache.RaiseExceptionHandling<ARPayment.depositAsBatch>((object) doc, (object) doc.DepositAsBatch, (Exception) propertyException);
    }
    PXUIFieldAttribute.SetEnabled<ARPayment.depositAsBatch>(cache, (object) doc, flag36);
    PXCache pxCache5 = cache;
    ARPayment arPayment4 = doc;
    int num25;
    if (!flag34 & flag6)
    {
      nullable1 = doc.DepositAsBatch;
      num25 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num25 = 0;
    PXUIFieldAttribute.SetEnabled<ARPayment.depositAfter>(pxCache5, (object) arPayment4, num25 != 0);
    nullable1 = doc.Released;
    bool flag38 = !nullable1.GetValueOrDefault() && (doc.DocType == "PMT" || doc.DocType == "RPM" || doc.DocType == "PPM" || doc.DocType == "REF" || doc.DocType == "VRF");
    ((PXSelectBase) this.PaymentCharges).Cache.AllowInsert = flag38;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowUpdate = flag38;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowDelete = flag38;
    int num26;
    if (doc.DocType != "SMB" && doc.DocType != "RPM" && doc.DocType != "VRF")
    {
      nullable1 = doc.Voided;
      num26 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num26 = 0;
    ((PXAction) this.reverseApplication).SetEnabled(num26 != 0);
    nullable1 = doc.IsMigratedRecord;
    bool valueOrDefault4 = nullable1.GetValueOrDefault();
    bool flag39 = valueOrDefault4 && !valueOrDefault2;
    bool flag40 = valueOrDefault4 & valueOrDefault2;
    bool flag41 = flag39 && (doc.DocType == "PMT" || doc.DocType == "PPM");
    PXUIFieldAttribute.SetVisible<ARPayment.curyUnappliedBal>(cache, (object) doc, !flag39);
    PXUIFieldAttribute.SetVisible<ARPayment.curyInitDocBal>(cache, (object) doc, flag39);
    PXUIFieldAttribute.SetVisible<ARRegister.displayCuryInitDocBal>(cache, (object) doc, flag40);
    PXUIFieldAttribute.SetEnabled<ARPayment.curyInitDocBal>(cache, (object) doc, flag41);
    ((PXSelectBase) this.Adjustments).Cache.AllowSelect = !flag39;
    ((PXSelectBase) this.ARPost).Cache.AllowSelect = !flag39;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowSelect = !flag39;
    ARSetup current5 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    int num27;
    if (current5 == null)
    {
      num27 = 0;
    }
    else
    {
      nullable1 = current5.MigrationMode;
      num27 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if ((num27 != 0 ? (!valueOrDefault4 ? 1 : 0) : (flag39 ? 1 : 0)) != 0)
    {
      bool allowInsert = ((PXSelectBase) this.Document).Cache.AllowInsert;
      bool allowDelete = ((PXSelectBase) this.Document).Cache.AllowDelete;
      ((PXGraph) this).DisableCaches();
      ((PXSelectBase) this.Document).Cache.AllowInsert = allowInsert;
      ((PXSelectBase) this.Document).Cache.AllowDelete = allowDelete;
    }
    if (flag41)
    {
      if (string.IsNullOrEmpty(PXUIFieldAttribute.GetError<ARPayment.curyInitDocBal>(cache, (object) doc)))
        cache.RaiseExceptionHandling<ARPayment.curyInitDocBal>((object) doc, (object) doc.CuryInitDocBal, (Exception) new PXSetPropertyException("Enter the document open balance to this box.", (PXErrorLevel) 2));
    }
    else
      cache.RaiseExceptionHandling<ARPayment.curyInitDocBal>((object) doc, (object) doc.CuryInitDocBal, (Exception) null);
    this.CheckForUnreleasedIncomingApplications(cache, doc);
    if (this.IsApprovalRequired(doc))
    {
      if (doc.Status == "D" || doc.Status == "J")
        ((PXAction) this.release).SetEnabled(false);
      if (doc.DocType == "REF" && (doc.Status == "D" || doc.Status == "J" || doc.Status == "B"))
      {
        nullable1 = doc.DontApprove;
        bool flag42 = false;
        if (nullable1.GetValueOrDefault() == flag42 & nullable1.HasValue)
          this.DisableViewsOnUnapprovedRefund();
      }
      if (doc.DocType == "REF")
      {
        if (doc.Status == "D" || doc.Status == "J" || doc.Status == "C" || doc.Status == "B")
        {
          nullable1 = doc.DontApprove;
          bool flag43 = false;
          if (nullable1.GetValueOrDefault() == flag43 & nullable1.HasValue)
            PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
        }
        if (doc.Status == "D" || doc.Status == "J" || doc.Status == "B")
        {
          PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, (object) doc, true);
          cache.AllowDelete = true;
        }
      }
    }
    PXUIFieldAttribute.SetEnabled<ARPayment.docType>(cache, (object) doc, true);
    PXUIFieldAttribute.SetEnabled<ARPayment.refNbr>(cache, (object) doc, true);
    if (valueOrDefault2)
    {
      if (!valueOrDefault1)
      {
        nullable1 = (bool?) cache.GetValueOriginal<ARPayment.hold>((object) doc);
        if (!nullable1.GetValueOrDefault())
          goto label_143;
      }
      PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, (object) doc, true);
      cache.AllowUpdate = true;
    }
label_143:
    if (doc.DocType == "CRM")
    {
      nullable1 = doc.PaymentsByLinesAllowed;
      if (nullable1.GetValueOrDefault())
        cache.RaiseExceptionHandling<ARPayment.refNbr>((object) doc, (object) doc.RefNbr, (Exception) new PXSetPropertyException("The {0} credit memo is paid by line and cannot be applied to documents directly. To apply the credit memo, on the Payments and Applications (AR302000) form, create a payment and apply the credit memo lines and the needed invoice to it.", (PXErrorLevel) 2, new object[1]
        {
          (object) doc.RefNbr
        }));
    }
    PXUIFieldAttribute.SetVisible<ARPaymentChargeTran.projectID>(((PXSelectBase) this.PaymentCharges).Cache, (object) null, ProjectAttribute.IsPMVisible("AR"));
    if (doc.DocType == "PPI" && doc.Status == "Y")
      ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
    nullable1 = doc.IsCCPayment;
    ((PXSelectBase) this.ccProcTran).Cache.AllowSelect = nullable1.GetValueOrDefault() && (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() || !PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && transactionState.IsActive);
    ((PXSelectBase) this.ccProcTran).Cache.AllowUpdate = false;
    ((PXSelectBase) this.ccProcTran).Cache.AllowDelete = false;
    ((PXSelectBase) this.ccProcTran).Cache.AllowInsert = false;
    bool isIncorrect = doc.Status == "W" && !PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>();
    UIState.RaiseOrHideErrorByErrorLevelPriority<ARPayment.status>(cache, e.Row, isIncorrect, "The document can be processed as a document with the Balanced status. Card processing actions are no longer available because the Integrated Card Processing feature has been disabled.", (PXErrorLevel) 2);
  }

  protected virtual void CheckForUnreleasedIncomingApplications(PXCache sender, ARPayment document)
  {
    if (!document.Released.GetValueOrDefault() || !document.OpenDoc.GetValueOrDefault())
      return;
    ARAdjust arAdjust1 = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<ARRegisterAlias, On<ARAdjust.adjgDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust.adjgRefNbr, Equal<ARRegisterAlias.refNbr>>>>, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.released, NotEqual<True>, And<Not<ARAdjust.voided, Equal<True>, And<ARRegisterAlias.voided, Equal<True>, And<Where<ARRegisterAlias.docType, Equal<ARDocType.payment>, Or<ARRegisterAlias.docType, Equal<ARDocType.prepayment>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    sender.ClearFieldErrors<ARPayment.refNbr>((object) document);
    string warningMessage = (string) null;
    if (arAdjust1 != null)
    {
      warningMessage = PXLocalizer.LocalizeFormat("The document has an unreleased application from {0} {1}. To create new applications for the document, remove or release the unreleased application from {0} {1}.", new object[2]
      {
        (object) GetLabel.For<ARDocType>(arAdjust1.AdjgDocType),
        (object) arAdjust1.AdjgRefNbr
      });
    }
    else
    {
      ARAdjust arAdjust2 = HasApplicationToUnreleasedCM<Required<ARAdjust.adjgDocType>, Required<ARAdjust.adjgRefNbr>>.Select.SelectSingle<ARAdjust>((PXGraph) this, true, (object) document.DocType, (object) document.RefNbr);
      if (arAdjust2 != null)
        warningMessage = PXLocalizer.LocalizeFormat("The document has an unreleased application to the {0} credit memo. To create a new application for the document, remove or release the unreleased application to {0} on the Applications tab of the Invoices and Memos (AR301000) form.", new object[1]
        {
          (object) arAdjust2.AdjdRefNbr
        });
    }
    if (warningMessage == null)
      return;
    this.SetUnreleasedIncomingApplicationWarning(sender, document, warningMessage);
  }

  protected virtual void SetUnreleasedIncomingApplicationWarning(
    PXCache sender,
    ARPayment document,
    string warningMessage)
  {
    sender.DisplayFieldWarning<ARPayment.refNbr>((object) document, (object) null, warningMessage);
    ((PXSelectBase) this.Adjustments).Cache.AllowInsert = ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
    ((PXAction) this.loadInvoices).SetEnabled(false);
    ((PXAction) this.autoApply).SetEnabled(false);
    ((PXAction) this.adjustDocAmt).SetEnabled(false);
  }

  protected virtual void DisableViewsOnUnapprovedRefund()
  {
    ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowInsert = false;
    ((PXSelectBase) this.Approval).Cache.AllowInsert = false;
    ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Approval).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
    ((PXSelectBase) this.CurrentDocument).Cache.AllowDelete = false;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowDelete = false;
    ((PXSelectBase) this.Approval).Cache.AllowDelete = false;
  }

  private void CheckCreditCardTranStateBeforeVoiding()
  {
    ARPayment current = ((PXSelectBase<ARPayment>) this.Document).Current;
    if (current == null || !(current.DocType == "PMT") && !(current.DocType == "PPM"))
      return;
    string documentName1 = TranValidationHelper.GetDocumentName(current.DocType);
    ExternalTransaction extTran = !ExternalTranHelper.HasImportedNeedSyncTran((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran) ? GraphHelper.RowCast<ExternalTransaction>((IEnumerable) ((PXSelectBase<ExternalTransaction>) this.ExternalTran).Select(Array.Empty<object>())).FirstOrDefault<ExternalTransaction>() : throw new PXException("The {0} {1} cannot be voided. Use the Validate Card Payment action to validate the credit card transaction and void the document.", new object[2]
    {
      (object) current.RefNbr,
      (object) documentName1
    });
    if (extTran != null && ExternalTranHelper.GetSharedTranStatus((PXGraph) this, (IExternalTransaction) extTran) == ExternalTranHelper.SharedTranStatus.Synchronized)
    {
      string documentName2 = TranValidationHelper.GetDocumentName(extTran.VoidDocType);
      throw new PXException("The {0} {1} cannot be voided. The void transaction ({2}) has been recorded for the {3} {4}.", new object[5]
      {
        (object) current.RefNbr,
        (object) documentName1,
        (object) extTran.TranNumber,
        (object) extTran.VoidRefNbr,
        (object) documentName2
      });
    }
  }

  protected virtual void CheckCashAccount(PXCache cache, ARPayment doc)
  {
    CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) this.processingCenter).SelectSingle(Array.Empty<object>());
    if ((processingCenter != null ? (!processingCenter.ImportSettlementBatches.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    bool flag = ((IQueryable<PXResult<CashAccountDeposit>>) ((PXSelectBase<CashAccountDeposit>) new PXSelect<CashAccountDeposit, Where<CashAccountDeposit.cashAccountID, Equal<Required<CCProcessingCenter.depositAccountID>>, And<CashAccountDeposit.depositAcctID, Equal<Required<ARPayment.cashAccountID>>, And<Where<CashAccountDeposit.paymentMethodID, Equal<Required<ARPayment.paymentMethodID>>, Or<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>>>>((PXGraph) this)).Select(new object[3]
    {
      (object) processingCenter.DepositAccountID,
      (object) doc.CashAccountID,
      (object) doc.PaymentMethodID
    })).Any<PXResult<CashAccountDeposit>>();
    PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, processingCenter.DepositAccountID);
    UIState.RaiseOrHideErrorByErrorLevelPriority<ARPayment.cashAccountID>(cache, (object) doc, (flag ? 0 : (doc.CashAccountID.HasValue ? 1 : 0)) != 0, "This cash account is not a clearing account for the {0} cash account, this payment will not be included in a bank deposit on import of the settlement batch.", (PXErrorLevel) 2, (object) cashAccount.CashAccountCD);
  }

  public virtual void CheckDocumentBeforeVoiding(PXGraph graph, ARPayment payment)
  {
  }

  public virtual void CheckDocumentBeforeReversing(PXGraph graph, ARAdjust application)
  {
  }

  protected virtual void FillBalanceCache(ARPayment row, bool released = false)
  {
    if (row == null || row.DocType == null || row.RefNbr == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().GetCurrencyInfo(row.CuryInfoID);
    ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().StoreResult(currencyInfo);
    if (this.balanceCache == null)
      this.balanceCache = new Dictionary<ARAdjust, PXResultset<ARInvoice>>((IEqualityComparer<ARAdjust>) new RecordKeyComparer<ARAdjust>(((PXSelectBase) this.Adjustments).Cache));
    if (this.balanceCache.Keys.Any<ARAdjust>((Func<ARAdjust, bool>) (_ =>
    {
      bool? released1 = _.Released;
      bool flag = released;
      return released1.GetValueOrDefault() == flag & released1.HasValue;
    })) || !row.OpenDoc.GetValueOrDefault())
      return;
    foreach (PXResult<PX.Objects.AR.Standalone.ARAdjust, ARInvoice, ARRegisterAlias, ARTran, PX.Objects.CM.Extensions.CurrencyInfo> res in ((PXSelectBase) this.Adjustments_Balance).View.SelectMultiBound(new object[1]
    {
      (object) row
    }, new object[1]{ (object) released }))
    {
      PX.Objects.AR.Standalone.ARAdjust arAdjust = PXResult<PX.Objects.AR.Standalone.ARAdjust, ARInvoice, ARRegisterAlias, ARTran, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(res);
      this.AddBalanceCache(new ARAdjust()
      {
        AdjdDocType = arAdjust.AdjdDocType,
        AdjdRefNbr = arAdjust.AdjdRefNbr,
        AdjgDocType = arAdjust.AdjgDocType,
        AdjgRefNbr = arAdjust.AdjgRefNbr,
        AdjdLineNbr = arAdjust.AdjdLineNbr,
        AdjNbr = arAdjust.AdjNbr
      }, (PXResult) res);
    }
  }

  protected virtual void AddBalanceCache(ARAdjust adj, PXResult res)
  {
    if (this.balanceCache == null)
      this.balanceCache = new Dictionary<ARAdjust, PXResultset<ARInvoice>>((IEqualityComparer<ARAdjust>) new RecordKeyComparer<ARAdjust>(((PXSelectBase) this.Adjustments).Cache));
    ARInvoice arInvoice = PXResult.Unwrap<ARInvoice>((object) res);
    ARTran arTran = PXResult.Unwrap<ARTran>((object) res);
    ARRegisterAlias arRegisterAlias = PXResult.Unwrap<ARRegisterAlias>((object) res);
    PX.Objects.CM.Extensions.CurrencyInfo info1 = PXResult.Unwrap<PX.Objects.CM.Extensions.CurrencyInfo>((object) res);
    PX.Objects.CM.Extensions.CurrencyInfo info2 = PX.Objects.Common.Utilities.Clone<CurrencyInfo2, PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) this, PXResult.Unwrap<CurrencyInfo2>((object) res));
    if (arRegisterAlias != null && arRegisterAlias.RefNbr != null)
      PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, (ARRegister) arRegisterAlias);
    PXSelectorAttribute.StoreResult<ARAdjust.displayRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) adj, (IBqlTable) arInvoice);
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).StoreResult((IBqlTable) info1);
    ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().StoreResult(info1);
    ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().StoreResult(info2);
    Dictionary<ARAdjust, PXResultset<ARInvoice>> balanceCache = this.balanceCache;
    ARAdjust key = adj;
    PXResultset<ARInvoice, ARTran> pxResultset = new PXResultset<ARInvoice, ARTran>();
    ((PXResultset<ARInvoice>) pxResultset).Add((PXResult<ARInvoice>) new PXResult<ARInvoice, ARTran>(arInvoice, arTran));
    balanceCache[key] = (PXResultset<ARInvoice>) pxResultset;
  }

  public virtual void ARPayment_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is ARPayment row) || e.IsReadOnly)
      return;
    using (new PXConnectionScope())
    {
      this.FillBalanceCache(row);
      if (sender.GetStatus(e.Row) == null && (row.Status == "N" || row.Status == "U"))
      {
        bool? voidAppl = row.VoidAppl;
        bool flag = false;
        if (voidAppl.GetValueOrDefault() == flag & voidAppl.HasValue)
        {
          DateTime? nullable = row.AdjDate;
          if (nullable.HasValue)
          {
            nullable = row.AdjDate;
            DateTime dateTime1 = nullable.Value;
            ref DateTime local = ref dateTime1;
            nullable = ((PXGraph) this).Accessinfo.BusinessDate;
            DateTime dateTime2 = nullable.Value;
            if (local.CompareTo(dateTime2) < 0 && !((PXGraph) this).IsImport)
            {
              if (!((PXSelectBase) this.Adjustments_Raw).View.SelectMultiBound(new object[1]
              {
                e.Row
              }, Array.Empty<object>()).Any<object>())
              {
                IFinPeriodRepository periodRepository = this.FinPeriodRepository;
                nullable = ((PXGraph) this).Accessinfo.BusinessDate;
                DateTime? date = new DateTime?(nullable.Value);
                int? parentOrganizationId = PXAccess.GetParentOrganizationID(row.BranchID);
                FinPeriod finPeriodByDate = periodRepository.FindFinPeriodByDate(date, parentOrganizationId);
                if (finPeriodByDate != null)
                {
                  row.AdjDate = ((PXGraph) this).Accessinfo.BusinessDate;
                  row.AdjTranPeriodID = finPeriodByDate.MasterFinPeriodID;
                  row.AdjFinPeriodID = finPeriodByDate.FinPeriodID;
                  sender.SetStatus(e.Row, (PXEntryStatus) 5);
                }
              }
            }
          }
        }
      }
      this.CalcApplAmounts(sender, row);
      this.RecalcWriteOffAmounts(sender, row);
    }
  }

  public virtual void CalcApplAmounts(PXCache sender, ARPayment row)
  {
    if (row.CuryApplAmt.HasValue)
      return;
    this.RecalcApplAmounts(sender, row);
  }

  public virtual void RecalcApplAmounts(PXCache sender, ARPayment row)
  {
    bool flag = false;
    PXFormulaAttribute.CalcAggregate<ARAdjust.curyAdjgAmt>(((PXSelectBase) this.Adjustments).Cache, (object) row, flag);
    if (!row.CuryApplAmt.HasValue)
      row.CuryApplAmt = new Decimal?(0M);
    sender.RaiseFieldUpdated<ARPayment.curyApplAmt>((object) row, (object) null);
  }

  public virtual void RecalcWriteOffAmounts(PXCache sender, ARPayment row)
  {
    bool flag = false;
    PXFormulaAttribute.CalcAggregate<ARAdjust.curyAdjgWOAmt>(((PXSelectBase) this.Adjustments).Cache, (object) row, flag);
    if (!row.CuryWOAmt.HasValue)
      row.CuryWOAmt = new Decimal?(0M);
    sender.RaiseFieldUpdated<ARPayment.curyWOAmt>((object) row, (object) null);
  }

  public static void SetDocTypeList(PXCache cache, string docType)
  {
    string str = "INV";
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    switch (docType)
    {
      case "REF":
      case "VRF":
        str = "CRM";
        stringList1.AddRange((IEnumerable<string>) new string[4]
        {
          "CRM",
          "PMT",
          "PPM",
          "PPI"
        });
        stringList2.AddRange((IEnumerable<string>) new string[4]
        {
          "Credit Memo",
          "Payment",
          "Prepayment",
          "Prepmt. Invoice"
        });
        break;
      case "PMT":
      case "RPM":
      case "PPM":
        stringList1.AddRange((IEnumerable<string>) new string[4]
        {
          "INV",
          "DRM",
          "CRM",
          "FCH"
        });
        stringList2.AddRange((IEnumerable<string>) new string[4]
        {
          "Invoice",
          "Debit Memo",
          "Credit Memo",
          "Overdue Charge"
        });
        if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
        {
          stringList1.Add("PPI");
          stringList2.Add("Prepmt. Invoice");
          break;
        }
        break;
      case "CRM":
        stringList1.AddRange((IEnumerable<string>) new string[3]
        {
          "INV",
          "DRM",
          "FCH"
        });
        stringList2.AddRange((IEnumerable<string>) new string[3]
        {
          "Invoice",
          "Debit Memo",
          "Overdue Charge"
        });
        break;
      default:
        stringList1.AddRange((IEnumerable<string>) new string[3]
        {
          "INV",
          "DRM",
          "FCH"
        });
        stringList2.AddRange((IEnumerable<string>) new string[3]
        {
          "Invoice",
          "Debit Memo",
          "Overdue Charge"
        });
        break;
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.overdueFinCharges>() && stringList1.Contains("FCH") && stringList2.Contains("Overdue Charge"))
    {
      stringList1.Remove("FCH");
      stringList2.Remove("Overdue Charge");
    }
    PXDefaultAttribute.SetDefault<ARAdjust.adjdDocType>(cache, (object) str);
    PXStringListAttribute.SetList<ARAdjust.adjdDocType>(cache, (object) null, stringList1.ToArray(), stringList2.ToArray());
  }

  public static PXAdapter CreateAdapterWithDummyView(ARPaymentEntry graph, ARPayment doc)
  {
    return new PXAdapter((PXView) new PXView.Dummy((PXGraph) graph, ((PXSelectBase) graph.Document).View.BqlSelect, new List<object>()
    {
      (object) doc
    }));
  }

  private void SetDocTypeList(object Row)
  {
    if (!(Row is ARPayment arPayment))
      return;
    ARPaymentEntry.SetDocTypeList(((PXSelectBase) this.Adjustments).Cache, arPayment.DocType);
  }

  protected virtual void ARPayment_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    bool? released = ((ARRegister) e.Row).Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    e.NewValue = (object) ((ARPayment) e.Row).AdjDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_DocDate_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is ARPayment row))
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
    DateTime? lastActivityDate = (DateTime?) transactionState.ExternalTransaction?.LastActivityDate;
    if (!lastActivityDate.HasValue)
      return;
    bool? voidAppl = row.VoidAppl;
    bool flag = false;
    if (voidAppl.GetValueOrDefault() == flag & voidAppl.HasValue)
    {
      if (!transactionState.IsSettlementDue || DateTime.Compare(((DateTime?) e.NewValue).Value, lastActivityDate.Value.Date) == 0)
        return;
      sender.RaiseExceptionHandling<ARPayment.docDate>((object) row, (object) null, (Exception) new PXSetPropertyException("Payment date is different than date of capture transaction", (PXErrorLevel) 2));
    }
    else
    {
      if (!transactionState.IsRefunded || DateTime.Compare(((DateTime?) e.NewValue).Value, lastActivityDate.Value.Date) == 0)
        return;
      sender.RaiseExceptionHandling<ARPayment.docDate>((object) row, (object) null, (Exception) new PXSetPropertyException("Payment date is different than date of capture transaction", (PXErrorLevel) 2));
    }
  }

  protected virtual void ARPayment_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((ARRegister) e.Row).Released.Value)
      return;
    e.NewValue = (object) ((ARPayment) e.Row).AdjFinPeriodID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_TranPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((ARRegister) e.Row).Released.Value)
      return;
    e.NewValue = (object) ((ARPayment) e.Row).AdjTranPeriodID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_DepositAfter_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (!(row.DocType == "PMT") && !(row.DocType == "PPM") && !(row.DocType == "CSL") && !(row.DocType == "REF") || !row.DepositAsBatch.GetValueOrDefault())
      return;
    e.NewValue = (object) row.AdjDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_DepositAsBatch_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    if (!(row.DocType == "PMT") && !(row.DocType == "PPM") && !(row.DocType == "CSL") && !(row.DocType == "REF"))
      return;
    sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);
  }

  protected virtual void ARPayment_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    foreach (ARAdjust arAdjust in ((PXSelectBase) this.Adjustments).Cache.Inserted)
      ((PXSelectBase<ARAdjust>) this.Adjustments).Delete(arAdjust);
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current;
    bool flag = ((ARRegister) e.Row).DocType == "RPM" && !transactionState.IsRefunded && !transactionState.IsVoided;
    if (current?.PaymentType == "CCD" && current != null && current.ARIsProcessingRequired.GetValueOrDefault() && transactionState != null && transactionState.IsActive && !flag)
      throw new PXException("The document cannot be deleted because there are card transactions associated with it. Use the Void action to cancel the document.");
  }

  protected virtual void ARPayment_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
  }

  protected virtual void ARPayment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    bool? released = row.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    row.DocDate = row.AdjDate;
    row.FinPeriodID = row.AdjFinPeriodID;
    row.TranPeriodID = row.AdjTranPeriodID;
    sender.RaiseExceptionHandling<ARPayment.finPeriodID>(e.Row, (object) row.FinPeriodID, (Exception) null);
  }

  protected virtual void ARPayment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARPayment row = (ARPayment) e.Row;
    ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
    if (!row.Released.GetValueOrDefault())
    {
      row.DocDate = row.AdjDate;
      row.FinPeriodID = row.AdjFinPeriodID;
      row.TranPeriodID = row.AdjTranPeriodID;
      sender.RaiseExceptionHandling<ARPayment.finPeriodID>(e.Row, (object) row.FinPeriodID, (Exception) null);
      this.PaymentCharges.UpdateChangesFromPayment(sender, e);
    }
    this.IsPaymentUnbalancedException(sender, row);
  }

  public virtual void IsPaymentUnbalancedException(PXCache sender, ARPayment payment)
  {
    if (!payment.OpenDoc.GetValueOrDefault() || payment.Hold.GetValueOrDefault())
      return;
    if (this.IsPaymentUnbalanced(payment))
      sender.RaiseExceptionHandling<ARPayment.curyOrigDocAmt>((object) payment, (object) payment.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The document is out of the balance.", (PXErrorLevel) 2));
    else
      sender.RaiseExceptionHandling<ARPayment.curyOrigDocAmt>((object) payment, (object) null, (Exception) null);
  }

  public virtual bool IsPaymentUnbalanced(ARPayment payment)
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
            goto label_14;
        }
        nullable2 = payment.CuryOrigDocAmt;
        Decimal num5 = 0M;
        if (!(nullable2.GetValueOrDefault() < num5 & nullable2.HasValue))
          goto label_10;
      }
label_14:
      return true;
    }
label_10:
    if (!flag)
    {
      nullable2 = payment.CuryUnappliedBal;
      Decimal num6 = 0M;
      if (!(nullable2.GetValueOrDefault() == num6 & nullable2.HasValue))
        return !payment.SelfVoidingDoc.GetValueOrDefault();
    }
    return false;
  }

  public virtual void CreatePayment(ARInvoice ardoc)
  {
    this.CreatePayment(ardoc, (PX.Objects.CM.Extensions.CurrencyInfo) null, new DateTime?(), (string) null, true);
  }

  public virtual void CreatePayment(ARInvoice ardoc, string paymentType)
  {
    this.CreatePayment(ardoc, (PX.Objects.CM.Extensions.CurrencyInfo) null, new DateTime?(), (string) null, true, paymentType);
  }

  public virtual void CreatePayment(
    ARInvoice ardoc,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    DateTime? paymentDate,
    string aFinPeriod,
    bool overrideDesc)
  {
    string paymentType = ardoc.DocType == "CRM" ? "REF" : "PMT";
    this.CreatePayment(ardoc, (PX.Objects.CM.Extensions.CurrencyInfo) null, new DateTime?(), (string) null, true, paymentType);
  }

  public virtual void CreatePayment(
    ARInvoice ardoc,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    DateTime? paymentDate,
    string aFinPeriod,
    bool overrideDesc,
    string paymentType)
  {
    ARPayment current = ((PXSelectBase<ARPayment>) this.Document).Current;
    int? nullable1;
    if (current != null && object.Equals((object) current.CustomerID, (object) ardoc.CustomerID))
    {
      int? pmInstanceId = ardoc.PMInstanceID;
      if (pmInstanceId.HasValue)
      {
        pmInstanceId = current.PMInstanceID;
        nullable1 = ardoc.PMInstanceID;
        if (pmInstanceId.GetValueOrDefault() == nullable1.GetValueOrDefault() & pmInstanceId.HasValue == nullable1.HasValue)
          goto label_22;
      }
      else
        goto label_22;
    }
    ((PXGraph) this).Clear();
    if (info != null)
      info = ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().CloneCurrencyInfo(info);
    PXSelectJoin<ARPayment, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>>, Where<ARPayment.docType, Equal<Optional<ARPayment.docType>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>> document = this.Document;
    ARPayment arPayment1 = new ARPayment();
    arPayment1.DocType = paymentType;
    ARPayment copy1 = PXCache<ARPayment>.CreateCopy(((PXSelectBase<ARPayment>) document).Insert(arPayment1));
    if (info != null)
      copy1.CuryInfoID = info.CuryInfoID;
    ARPayment arPayment2 = copy1;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    arPayment2.BranchID = nullable2;
    copy1.CustomerID = ardoc.CustomerID;
    copy1.CustomerLocationID = ardoc.CustomerLocationID;
    if (overrideDesc)
      copy1.DocDesc = ardoc.DocDesc;
    if (paymentDate.HasValue)
    {
      copy1.AdjDate = paymentDate;
    }
    else
    {
      ARPayment arPayment3 = copy1;
      DateTime? nullable3 = ((PXGraph) this).Accessinfo.BusinessDate;
      DateTime t1 = nullable3.Value;
      nullable3 = ardoc.DocDate;
      DateTime t2 = nullable3.Value;
      DateTime? nullable4 = DateTime.Compare(t1, t2) < 0 ? ardoc.DocDate : ((PXGraph) this).Accessinfo.BusinessDate;
      arPayment3.AdjDate = nullable4;
    }
    if (!string.IsNullOrEmpty(aFinPeriod))
      copy1.AdjFinPeriodID = aFinPeriod;
    if (!string.IsNullOrEmpty(ardoc.PaymentMethodID))
      copy1.PaymentMethodID = ardoc.PaymentMethodID;
    nullable1 = ardoc.PMInstanceID;
    if (nullable1.HasValue)
    {
      this.VerifyHasActiveCustomerPaymentMethod(ardoc);
      copy1.PMInstanceID = ardoc.PMInstanceID;
    }
    nullable1 = ardoc.CashAccountID;
    if (nullable1.HasValue)
      copy1.CashAccountID = ardoc.CashAccountID;
    ((PXSelectBase<ARPayment>) this.Document).Update(copy1);
    if (info != null)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null));
      currencyInfo.CuryID = info.CuryID;
      currencyInfo.CuryEffDate = info.CuryEffDate;
      currencyInfo.CuryRateTypeID = info.CuryRateTypeID;
      currencyInfo.CuryRate = info.CuryRate;
      currencyInfo.RecipRate = info.RecipRate;
      currencyInfo.CuryMultDiv = info.CuryMultDiv;
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo);
    }
label_22:
    ARAdjust arAdjust = new ARAdjust();
    arAdjust.AdjdDocType = ardoc.DocType;
    arAdjust.AdjdRefNbr = ardoc.RefNbr;
    ((PXSelectBase<ARPayment>) this.Document).Current.CuryOrigDocAmt = new Decimal?(0M);
    ((PXSelectBase<ARPayment>) this.Document).Update(((PXSelectBase<ARPayment>) this.Document).Current);
    try
    {
      if (ardoc.PaymentsByLinesAllowed.GetValueOrDefault())
      {
        foreach (PXResult<ARTran> pxResult in PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<ARTran.curyTranBal, NotEqual<Zero>>>>, OrderBy<Desc<ARTran.curyTranBal>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
        {
          (object) ardoc
        }, Array.Empty<object>()))
        {
          ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
          ARAdjust copy2 = PXCache<ARAdjust>.CreateCopy(arAdjust);
          copy2.AdjdLineNbr = arTran.LineNbr;
          ((PXSelectBase<ARAdjust>) this.Adjustments).Insert(copy2);
        }
      }
      else
      {
        arAdjust.AdjdLineNbr = new int?(0);
        ((PXSelectBase<ARAdjust>) this.Adjustments).Insert(arAdjust);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "AR Invoice/Memo"
      });
    }
    Decimal? curyApplAmt = ((PXSelectBase<ARPayment>) this.Document).Current.CuryApplAmt;
    Decimal? nullable5 = curyApplAmt;
    Decimal num = 0M;
    if (!(nullable5.GetValueOrDefault() > num & nullable5.HasValue))
      return;
    ((PXSelectBase<ARPayment>) this.Document).Current.CuryOrigDocAmt = curyApplAmt;
    ((PXSelectBase<ARPayment>) this.Document).Update(((PXSelectBase<ARPayment>) this.Document).Current);
  }

  private void VerifyHasActiveCustomerPaymentMethod(ARInvoice doc)
  {
    if (!doc.PMInstanceID.HasValue)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<ARInvoice>((PXGraph) this), (object) doc, typeof (ARInvoice.pMInstanceID), true);
    CustomerPaymentMethod customerPaymentMethod = CustomerPaymentMethod.PK.Find((PXGraph) this, doc.PMInstanceID);
    if (customerPaymentMethod == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<CustomerPaymentMethod>((PXGraph) this), new object[1]
      {
        (object) doc.PMInstanceID
      });
    if (!customerPaymentMethod.IsActive.GetValueOrDefault())
      throw new PXException("The {0} card/account number is inactive on the Customer Payment Methods (AR303010) form and cannot be processed.", new object[1]
      {
        (object) customerPaymentMethod.Descr
      });
  }

  protected virtual void ARPayment_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARPayment_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this._IsVoidCheckInProgress)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (!(e.Row is ARPayment row))
        return;
      ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
      if ((row.Released.GetValueOrDefault() || transactionState.IsCaptured) && row.AdjFinPeriodID.CompareTo((string) e.NewValue) < 0)
      {
        e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
        {
          (object) PeriodIDAttribute.FormatForError(row.AdjFinPeriodID)
        });
      }
    }
  }

  protected virtual void ARPayment_AdjFinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this._IsVoidCheckInProgress)
      ((CancelEventArgs) e).Cancel = true;
    string errMsg;
    if (!this.VerifyAdjFinPeriodID((ARPayment) e.Row, (string) e.NewValue, out errMsg))
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw new PXSetPropertyException(errMsg);
    }
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2026 R1.")]
  public virtual bool VerifyAdjFinPeriodID(ARPayment doc, string newValue)
  {
    return this.VerifyAdjFinPeriodID(doc, newValue, out string _);
  }

  public virtual bool VerifyAdjTranPeriodID(ARPayment doc, string newValue)
  {
    return this.VerifyAdjTranPeriodID(doc, newValue, out string _);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2026 R1.")]
  protected virtual bool VerifyAdjFinPeriodID(ARPayment doc, string newValue, out string errMsg)
  {
    return this.VerifyPeriodID<ARPayment.finPeriodID, ARAdjust.adjgFinPeriodID>(doc, newValue, out errMsg);
  }

  protected virtual bool VerifyAdjTranPeriodID(ARPayment doc, string newValue, out string errMsg)
  {
    return this.VerifyPeriodID<ARPayment.tranPeriodID, ARAdjust.adjgTranPeriodID>(doc, newValue, out errMsg);
  }

  private bool VerifyPeriodID<TPeriodIDField, TAdjgPeriodIDField>(
    ARPayment doc,
    string newValue,
    out string errMsg)
    where TPeriodIDField : IBqlField
    where TAdjgPeriodIDField : IBqlField
  {
    string period1 = ((PXCache) GraphHelper.Caches<ARPayment>((PXGraph) this)).GetValue<TPeriodIDField>((object) doc) as string;
    errMsg = (string) null;
    ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
    if (doc.Released.GetValueOrDefault() && period1.CompareTo(newValue) > 0)
    {
      errMsg = $"Incorrect value. The value to be entered must be greater than or equal to {PeriodIDAttribute.FormatForError(period1)}.";
      return false;
    }
    if (doc.DocType == "RPM")
    {
      ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where2<Where<ARPayment.docType, Equal<ARDocType.payment>, Or<ARPayment.docType, Equal<ARDocType.prepayment>>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) doc.RefNbr
      }));
      if (arPayment != null && arPayment.FinPeriodID.CompareTo(newValue) > 0)
      {
        errMsg = $"Incorrect value. The value to be entered must be greater than or equal to {PeriodIDAttribute.FormatForError(arPayment.FinPeriodID)}.";
        return false;
      }
    }
    else
    {
      try
      {
        this.internalCall = true;
        ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, LeftJoin<ARAdjust2, On<ARAdjust2.adjdDocType, Equal<ARAdjust.adjdDocType>, And<ARAdjust2.adjdRefNbr, Equal<ARAdjust.adjdRefNbr>, And<ARAdjust2.adjdLineNbr, Equal<ARAdjust.adjdLineNbr>, And<ARAdjust2.adjgDocType, Equal<ARAdjust.adjgDocType>, And<ARAdjust2.adjgRefNbr, Equal<ARAdjust.adjgRefNbr>, And<ARAdjust2.adjNbr, NotEqual<ARAdjust.adjNbr>, And<Switch<Case<Where<ARAdjust.voidAdjNbr, IsNotNull>, ARAdjust.voidAdjNbr>, ARAdjust.adjNbr>, Equal<Switch<Case<Where<ARAdjust.voidAdjNbr, IsNotNull>, ARAdjust2.adjNbr>, ARAdjust2.voidAdjNbr>>, And<ARAdjust2.adjgTranPeriodID, Equal<ARAdjust.adjgTranPeriodID>, And<ARAdjust2.released, Equal<True>, And<ARAdjust2.voided, Equal<True>, And<ARAdjust.voided, Equal<True>>>>>>>>>>>>>, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.released, Equal<True>, And<ARAdjust2.adjdRefNbr, IsNull>>>>, OrderBy<Desc<ARAdjust.adjgTranPeriodID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
        {
          (object) doc.DocType,
          (object) doc.RefNbr
        }));
        if (((PXCache) GraphHelper.Caches<ARAdjust>((PXGraph) this)).GetValue<TAdjgPeriodIDField>((object) arAdjust) is string period2)
        {
          if (period2.CompareTo(newValue) > 0)
          {
            errMsg = $"Incorrect value. The value to be entered must be greater than or equal to {PeriodIDAttribute.FormatForError(period2)}.";
            return false;
          }
        }
      }
      finally
      {
        this.internalCall = false;
      }
    }
    return true;
  }

  public virtual bool ShowCardChck(ARPayment doc)
  {
    if (doc.DocType == "PMT" || doc.DocType == "PPM" || doc.DocType == "REF")
    {
      bool? nullable = doc.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = doc.Voided;
        bool flag2 = false;
        return nullable.GetValueOrDefault() == flag2 & nullable.HasValue;
      }
    }
    return false;
  }

  public virtual void VoidCheckProcExt(ARPayment doc)
  {
    try
    {
      this._IsVoidCheckInProgress = true;
      this.VoidCheckProc(doc);
    }
    finally
    {
      this._IsVoidCheckInProgress = false;
    }
  }

  public virtual void SelfVoidingProc(ARPayment doc)
  {
    ARPayment copy = PXCache<ARPayment>.CreateCopy(doc);
    bool? openDoc = copy.OpenDoc;
    bool flag = false;
    if (openDoc.GetValueOrDefault() == flag & openDoc.HasValue)
    {
      copy.OpenDoc = new bool?(true);
      ((PXSelectBase) this.Document).Cache.RaiseRowSelected((object) copy);
    }
    foreach (PXResult<ARAdjust> pxResult in PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARAdjust.adjdCuryInfoID>>>, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.voided, NotEqual<True>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) copy.DocType,
      (object) copy.RefNbr
    }))
      this.CreateReversingApp(PXResult<ARAdjust>.op_Implicit(pxResult), copy);
    copy.CuryApplAmt = new Decimal?();
    copy.CuryUnappliedBal = new Decimal?();
    copy.CuryWOAmt = new Decimal?();
    copy.CCReauthDate = new DateTime?();
    copy.CCReauthTriesLeft = new int?(0);
    copy.IsCCUserAttention = new bool?(false);
    ARPayment arPayment = ((PXSelectBase<ARPayment>) this.Document).Update(copy);
    ((SelectedEntityEvent<ARPayment>) PXEntityEventBase<ARPayment>.Container<ARPayment.Events>.Select((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment.Events>>>) (ev => ev.OpenDocument))).FireOn((PXGraph) this, arPayment);
  }

  public virtual ARAdjust CreateReversingApp(ARAdjust adj, ARPayment payment)
  {
    ARAdjust copy = PXCache<ARAdjust>.CreateCopy(adj);
    copy.Voided = new bool?(true);
    copy.VoidAdjNbr = copy.AdjNbr;
    copy.Released = new bool?(false);
    ((PXSelectBase) this.Adjustments).Cache.SetDefaultExt<ARAdjust.isMigratedRecord>((object) copy);
    copy.AdjNbr = payment.AdjCntr;
    copy.AdjBatchNbr = (string) null;
    copy.StatementDate = new DateTime?();
    copy.AdjgDocDate = payment.AdjDate;
    ARAdjust row = new ARAdjust();
    row.AdjgDocType = copy.AdjgDocType;
    row.AdjgRefNbr = copy.AdjgRefNbr;
    row.AdjgBranchID = copy.AdjgBranchID;
    row.AdjdDocType = copy.AdjdDocType;
    row.AdjdRefNbr = copy.AdjdRefNbr;
    row.AdjdLineNbr = copy.AdjdLineNbr;
    row.AdjdBranchID = copy.AdjdBranchID;
    row.CustomerID = copy.CustomerID;
    row.AdjdCustomerID = copy.AdjdCustomerID;
    row.AdjNbr = copy.AdjNbr;
    row.AdjdCuryInfoID = copy.AdjdCuryInfoID;
    row.AdjdHasPPDTaxes = copy.AdjdHasPPDTaxes;
    row.InvoiceID = copy.InvoiceID;
    row.PaymentID = copy.PaymentID;
    row.MemoID = copy.MemoID;
    try
    {
      this.AutoPaymentApp = true;
      this.IsReverseProc = true;
      row = ((PXSelectBase<ARAdjust>) this.Adjustments).Insert(row);
      if (row != null)
      {
        copy.AdjdOrderType = (string) null;
        copy.AdjdOrderNbr = (string) null;
        ARAdjust arAdjust1 = copy;
        Decimal num1 = (Decimal) -1;
        Decimal? curyAdjgAmt = copy.CuryAdjgAmt;
        Decimal? nullable1 = curyAdjgAmt.HasValue ? new Decimal?(num1 * curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust1.CuryAdjgAmt = nullable1;
        ARAdjust arAdjust2 = copy;
        Decimal num2 = (Decimal) -1;
        Decimal? curyAdjgDiscAmt = copy.CuryAdjgDiscAmt;
        Decimal? nullable2 = curyAdjgDiscAmt.HasValue ? new Decimal?(num2 * curyAdjgDiscAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust2.CuryAdjgDiscAmt = nullable2;
        ARAdjust arAdjust3 = copy;
        Decimal num3 = (Decimal) -1;
        Decimal? curyAdjgPpdAmt = copy.CuryAdjgPPDAmt;
        Decimal? nullable3 = curyAdjgPpdAmt.HasValue ? new Decimal?(num3 * curyAdjgPpdAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust3.CuryAdjgPPDAmt = nullable3;
        ARAdjust arAdjust4 = copy;
        Decimal num4 = (Decimal) -1;
        Decimal? curyAdjgWoAmt = copy.CuryAdjgWOAmt;
        Decimal? nullable4 = curyAdjgWoAmt.HasValue ? new Decimal?(num4 * curyAdjgWoAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust4.CuryAdjgWOAmt = nullable4;
        ARAdjust arAdjust5 = copy;
        Decimal num5 = (Decimal) -1;
        Decimal? adjAmt = copy.AdjAmt;
        Decimal? nullable5 = adjAmt.HasValue ? new Decimal?(num5 * adjAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust5.AdjAmt = nullable5;
        ARAdjust arAdjust6 = copy;
        Decimal num6 = (Decimal) -1;
        Decimal? adjDiscAmt = copy.AdjDiscAmt;
        Decimal? nullable6 = adjDiscAmt.HasValue ? new Decimal?(num6 * adjDiscAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust6.AdjDiscAmt = nullable6;
        ARAdjust arAdjust7 = copy;
        Decimal num7 = (Decimal) -1;
        Decimal? adjPpdAmt = copy.AdjPPDAmt;
        Decimal? nullable7 = adjPpdAmt.HasValue ? new Decimal?(num7 * adjPpdAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust7.AdjPPDAmt = nullable7;
        ARAdjust arAdjust8 = copy;
        Decimal num8 = (Decimal) -1;
        Decimal? adjWoAmt = copy.AdjWOAmt;
        Decimal? nullable8 = adjWoAmt.HasValue ? new Decimal?(num8 * adjWoAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust8.AdjWOAmt = nullable8;
        ARAdjust arAdjust9 = copy;
        Decimal num9 = (Decimal) -1;
        Decimal? curyAdjdAmt = copy.CuryAdjdAmt;
        Decimal? nullable9 = curyAdjdAmt.HasValue ? new Decimal?(num9 * curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust9.CuryAdjdAmt = nullable9;
        ARAdjust arAdjust10 = copy;
        Decimal num10 = (Decimal) -1;
        Decimal? curyAdjdDiscAmt = copy.CuryAdjdDiscAmt;
        Decimal? nullable10 = curyAdjdDiscAmt.HasValue ? new Decimal?(num10 * curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust10.CuryAdjdDiscAmt = nullable10;
        ARAdjust arAdjust11 = copy;
        Decimal num11 = (Decimal) -1;
        Decimal? curyAdjdPpdAmt = copy.CuryAdjdPPDAmt;
        Decimal? nullable11 = curyAdjdPpdAmt.HasValue ? new Decimal?(num11 * curyAdjdPpdAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust11.CuryAdjdPPDAmt = nullable11;
        ARAdjust arAdjust12 = copy;
        Decimal num12 = (Decimal) -1;
        Decimal? curyAdjdWoAmt = copy.CuryAdjdWOAmt;
        Decimal? nullable12 = curyAdjdWoAmt.HasValue ? new Decimal?(num12 * curyAdjdWoAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust12.CuryAdjdWOAmt = nullable12;
        ARAdjust arAdjust13 = copy;
        Decimal num13 = (Decimal) -1;
        Decimal? rgolAmt = copy.RGOLAmt;
        Decimal? nullable13 = rgolAmt.HasValue ? new Decimal?(num13 * rgolAmt.GetValueOrDefault()) : new Decimal?();
        arAdjust13.RGOLAmt = nullable13;
        copy.AdjgCuryInfoID = payment.CuryInfoID;
        ((PXSelectBase) this.Adjustments).Cache.SetDefaultExt<ARAdjust.noteID>((object) copy);
        ((PXSelectBase<ARAdjust>) this.Adjustments).Update(copy);
        FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjgFinPeriodID>(((PXSelectBase) this.Adjustments).Cache, (object) row, ((PXSelectBase<ARPayment>) this.Document).Current.AdjTranPeriodID);
      }
    }
    finally
    {
      this.AutoPaymentApp = false;
      this.IsReverseProc = false;
    }
    return row;
  }

  public virtual void VoidCheckProc(ARPayment doc)
  {
    ((PXGraph) this).Clear((PXClearOption) 1);
    foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer> pxResult in PXSelectBase<ARPayment, PXSelectJoin<ARPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARPayment.curyInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<ARPayment.cashAccountID>>>>>>, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      ARPayment arPayment1 = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer>.op_Implicit(pxResult);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this).GetExtension<ARPaymentEntry.MultiCurrency>().CloneCurrencyInfo(PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer>.op_Implicit(pxResult));
      PXSelectJoin<ARPayment, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>>, Where<ARPayment.docType, Equal<Optional<ARPayment.docType>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>> document = this.Document;
      ARPayment arPayment2 = new ARPayment();
      arPayment2.DocType = arPayment1.DocType;
      arPayment2.RefNbr = arPayment1.RefNbr;
      arPayment2.CuryInfoID = currencyInfo1.CuryInfoID;
      arPayment2.VoidAppl = new bool?(true);
      ARPayment arPayment3 = ((PXSelectBase<ARPayment>) document).Insert(arPayment2);
      string createdByScreenId = arPayment3.CreatedByScreenID;
      Guid? createdById = arPayment3.CreatedByID;
      ARPayment copy = PXCache<ARPayment>.CreateCopy(arPayment1);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.noteID>((object) copy);
      copy.CreatedByScreenID = createdByScreenId;
      copy.CreatedByID = createdById;
      copy.CuryInfoID = currencyInfo1.CuryInfoID;
      copy.VoidAppl = new bool?(true);
      copy.CATranID = new long?();
      copy.OrigDocType = arPayment1.DocType;
      copy.OrigRefNbr = arPayment1.RefNbr;
      copy.OrigModule = "AR";
      copy.OpenDoc = new bool?(true);
      copy.Released = new bool?(false);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.hold>((object) copy);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.isMigratedRecord>((object) copy);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.status>((object) copy);
      copy.LineCntr = new int?(0);
      copy.AdjCntr = new int?(0);
      copy.BatchNbr = (string) null;
      ARPayment arPayment4 = copy;
      Decimal num1 = (Decimal) -1;
      Decimal? curyOrigDocAmt = copy.CuryOrigDocAmt;
      Decimal? nullable1 = curyOrigDocAmt.HasValue ? new Decimal?(num1 * curyOrigDocAmt.GetValueOrDefault()) : new Decimal?();
      arPayment4.CuryOrigDocAmt = nullable1;
      ARPayment arPayment5 = copy;
      Decimal num2 = (Decimal) -1;
      Decimal? origDocAmt = copy.OrigDocAmt;
      Decimal? nullable2 = origDocAmt.HasValue ? new Decimal?(num2 * origDocAmt.GetValueOrDefault()) : new Decimal?();
      arPayment5.OrigDocAmt = nullable2;
      ARPayment arPayment6 = copy;
      Decimal num3 = (Decimal) -1;
      Decimal? curyInitDocBal = copy.CuryInitDocBal;
      Decimal? nullable3 = curyInitDocBal.HasValue ? new Decimal?(num3 * curyInitDocBal.GetValueOrDefault()) : new Decimal?();
      arPayment6.CuryInitDocBal = nullable3;
      ARPayment arPayment7 = copy;
      Decimal num4 = (Decimal) -1;
      Decimal? initDocBal = copy.InitDocBal;
      Decimal? nullable4 = initDocBal.HasValue ? new Decimal?(num4 * initDocBal.GetValueOrDefault()) : new Decimal?();
      arPayment7.InitDocBal = nullable4;
      copy.CuryChargeAmt = new Decimal?(0M);
      copy.CuryConsolidateChargeTotal = new Decimal?(0M);
      copy.CuryApplAmt = new Decimal?();
      ARPayment arPayment8 = copy;
      Decimal num5 = (Decimal) -1;
      Decimal? soApplAmt = copy.SOApplAmt;
      Decimal? nullable5 = soApplAmt.HasValue ? new Decimal?(num5 * soApplAmt.GetValueOrDefault()) : new Decimal?();
      arPayment8.SOApplAmt = nullable5;
      ARPayment arPayment9 = copy;
      Decimal num6 = (Decimal) -1;
      Decimal? curySoApplAmt = copy.CurySOApplAmt;
      Decimal? nullable6 = curySoApplAmt.HasValue ? new Decimal?(num6 * curySoApplAmt.GetValueOrDefault()) : new Decimal?();
      arPayment9.CurySOApplAmt = nullable6;
      copy.CuryUnappliedBal = new Decimal?();
      copy.CuryWOAmt = new Decimal?();
      copy.DocDate = doc.DocDate;
      copy.AdjDate = doc.DocDate;
      FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy, doc.AdjTranPeriodID);
      copy.StatementDate = new DateTime?();
      copy.Emailed = new bool?(false);
      copy.Printed = new bool?(false);
      copy.CCReauthDate = new DateTime?();
      copy.CCReauthTriesLeft = new int?(0);
      copy.IsCCUserAttention = new bool?(false);
      string paymentType = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current?.PaymentType;
      bool? nullable7;
      if (paymentType != "CCD" && paymentType != "EFT")
      {
        PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current;
        int num7;
        if (current == null)
        {
          num7 = 0;
        }
        else
        {
          nullable7 = current.Reconcile;
          bool flag = false;
          num7 = nullable7.GetValueOrDefault() == flag & nullable7.HasValue ? 1 : 0;
        }
        if (num7 != 0)
        {
          copy.Cleared = new bool?(true);
          copy.ClearDate = copy.DocDate;
          goto label_9;
        }
      }
      copy.Cleared = new bool?(false);
      copy.ClearDate = new DateTime?();
label_9:
      string paymentMethodId = copy.PaymentMethodID;
      nullable7 = copy.DepositAsBatch;
      if (nullable7.GetValueOrDefault() && !string.IsNullOrEmpty(copy.DepositNbr))
      {
        nullable7 = PX.Objects.CA.PaymentMethod.PK.Find((PXGraph) this, copy.PaymentMethodID).ARVoidOnDepositAccount;
        bool valueOrDefault = nullable7.GetValueOrDefault();
        if (!valueOrDefault)
        {
          nullable7 = copy.Deposited;
          bool flag = false;
          if (nullable7.GetValueOrDefault() == flag & nullable7.HasValue)
          {
            if (doc.DocType == "REF")
              throw new PXException("This refund is included in the {0} bank deposit. It cannot be voided until the deposit is released or the refund is excluded from it.", new object[1]
              {
                (object) copy.DepositNbr
              });
            throw new PXException("This payment is included in the {0} bank deposit. It cannot be voided until the deposit is released or the payment is excluded from it.", new object[1]
            {
              (object) copy.DepositNbr
            });
          }
          CADeposit caDeposit = PXResult<CADeposit, PX.Objects.CA.CashAccount>.op_Implicit((PXResult<CADeposit, PX.Objects.CA.CashAccount>) PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelectJoin<CADeposit, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CADeposit.cashAccountID>>>, Where<CADeposit.tranType, Equal<Required<CADeposit.tranType>>, And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) copy.DepositType,
            (object) copy.DepositNbr
          })) ?? throw new PXException("This payment refers to the invalid document {0} {1}.", new object[2]
          {
            (object) GetLabel.For<DepositType>(copy.DepositType),
            (object) copy.DepositNbr
          }));
          copy.CashAccountID = caDeposit.CashAccountID;
        }
        copy.DepositAsBatch = new bool?(valueOrDefault);
        copy.DepositType = (string) null;
        copy.DepositNbr = (string) null;
        copy.Deposited = new bool?(false);
        copy.DepositDate = new DateTime?();
      }
      ((PXSelectBase<ARPayment>) this.Document).Update(copy);
      using (new SuppressWorkflowAutoPersistScope((PXGraph) this))
        ((PXAction) this.initializeState).Press();
      ARPayment arPayment10 = ((PXSelectBase<ARPayment>) this.Document).Current;
      if (arPayment10.PaymentMethodID != paymentMethodId)
      {
        arPayment10.PaymentMethodID = paymentMethodId;
        arPayment10 = ((PXSelectBase<ARPayment>) this.Document).Update(arPayment10);
      }
      if (arPayment1.DocType == "PMT")
        ((PXSelectBase) this.Document).Cache.SetValueExt<ARPayment.extRefNbr>((object) arPayment10, (object) arPayment1.ExtRefNbr);
      ((PXSelectBase) this.Document).Cache.SetValueExt<ARPayment.adjFinPeriodID>((object) arPayment10, (object) PeriodIDAttribute.FormatForDisplay(doc.AdjFinPeriodID));
      if (currencyInfo1 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null));
        currencyInfo2.CuryID = currencyInfo1.CuryID;
        currencyInfo2.CuryEffDate = currencyInfo1.CuryEffDate;
        currencyInfo2.CuryRateTypeID = currencyInfo1.CuryRateTypeID;
        currencyInfo2.CuryRate = currencyInfo1.CuryRate;
        currencyInfo2.RecipRate = currencyInfo1.RecipRate;
        currencyInfo2.CuryMultDiv = currencyInfo1.CuryMultDiv;
        ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo2);
      }
    }
    foreach (PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARAdjust.adjdCuryInfoID>>>, Where<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.isInitialApplication, NotEqual<True>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      ARAdjust copy = PXCache<ARAdjust>.CreateCopy(PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult));
      ((PXSelectBase) this.Adjustments).Cache.SetDefaultExt<ARAdjust.noteID>((object) copy);
      bool? nullable8;
      if (!(doc.DocType != "CRM"))
      {
        nullable8 = doc.PendingPPD;
        if (nullable8.GetValueOrDefault())
          goto label_42;
      }
      nullable8 = copy.AdjdHasPPDTaxes;
      if (nullable8.GetValueOrDefault())
      {
        nullable8 = copy.PendingPPD;
        if (!nullable8.GetValueOrDefault())
        {
          ARAdjust ppdApplication = ARPaymentEntry.GetPPDApplication((PXGraph) this, copy.AdjdDocType, copy.AdjdRefNbr);
          if (ppdApplication != null && (ppdApplication.AdjgDocType != copy.AdjgDocType || ppdApplication.AdjgRefNbr != copy.AdjgRefNbr))
          {
            ARAdjust arAdjust1 = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
            ((PXGraph) this).Clear();
            ARAdjust arAdjust2 = (ARAdjust) ((PXSelectBase) this.Adjustments).Cache.Update((object) arAdjust1);
            ((PXSelectBase<ARPayment>) this.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) this.Document).Search<ARPayment.refNbr>((object) arAdjust2.AdjgRefNbr, new object[1]
            {
              (object) arAdjust2.AdjgDocType
            }));
            ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) arAdjust2, (object) arAdjust2.AdjdRefNbr, (Exception) new PXSetPropertyException("To proceed, you have to reverse application of the final payment {0} with cash discount given.", (PXErrorLevel) 5, new object[1]
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
label_42:
      copy.VoidAppl = new bool?(true);
      copy.Released = new bool?(false);
      ((PXSelectBase) this.Adjustments).Cache.SetDefaultExt<ARAdjust.isMigratedRecord>((object) copy);
      copy.VoidAdjNbr = copy.AdjNbr;
      copy.AdjNbr = new int?(0);
      copy.AdjBatchNbr = (string) null;
      copy.StatementDate = new DateTime?();
      if (((PXSelectBase<ARAdjust>) this.Adjustments).Insert(new ARAdjust()
      {
        AdjgDocType = copy.AdjgDocType,
        AdjgRefNbr = copy.AdjgRefNbr,
        AdjgBranchID = copy.AdjgBranchID,
        AdjdDocType = copy.AdjdDocType,
        AdjdRefNbr = copy.AdjdRefNbr,
        AdjdLineNbr = copy.AdjdLineNbr,
        AdjdBranchID = copy.AdjdBranchID,
        CustomerID = copy.CustomerID,
        AdjdCustomerID = copy.AdjdCustomerID,
        AdjdCuryInfoID = copy.AdjdCuryInfoID,
        AdjdHasPPDTaxes = copy.AdjdHasPPDTaxes
      }) == null)
      {
        ARAdjust arAdjust3 = PXResult<ARAdjust, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        ((PXGraph) this).Clear();
        ARAdjust arAdjust4 = (ARAdjust) ((PXSelectBase) this.Adjustments).Cache.Update((object) arAdjust3);
        ((PXSelectBase<ARPayment>) this.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) this.Document).Search<ARPayment.refNbr>((object) arAdjust4.AdjgRefNbr, new object[1]
        {
          (object) arAdjust4.AdjgDocType
        }));
        ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) arAdjust4, (object) arAdjust4.AdjdRefNbr, (Exception) new PXSetPropertyException("Multiple applications exists for this document. Please reverse these applications individually and then void the document.", (PXErrorLevel) 5));
        throw new PXException("Multiple applications exists for this document. Please reverse these applications individually and then void the document.");
      }
      copy.PaymentID = ((PXSelectBase<ARPayment>) this.Document).Current.NoteID;
      ARAdjust arAdjust5 = copy;
      Decimal num8 = (Decimal) -1;
      Decimal? curyAdjgAmt = copy.CuryAdjgAmt;
      Decimal? nullable9 = curyAdjgAmt.HasValue ? new Decimal?(num8 * curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust5.CuryAdjgAmt = nullable9;
      ARAdjust arAdjust6 = copy;
      Decimal num9 = (Decimal) -1;
      Decimal? curyAdjgDiscAmt = copy.CuryAdjgDiscAmt;
      Decimal? nullable10 = curyAdjgDiscAmt.HasValue ? new Decimal?(num9 * curyAdjgDiscAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust6.CuryAdjgDiscAmt = nullable10;
      ARAdjust arAdjust7 = copy;
      Decimal num10 = (Decimal) -1;
      Decimal? curyAdjgPpdAmt = copy.CuryAdjgPPDAmt;
      Decimal? nullable11 = curyAdjgPpdAmt.HasValue ? new Decimal?(num10 * curyAdjgPpdAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust7.CuryAdjgPPDAmt = nullable11;
      ARAdjust arAdjust8 = copy;
      Decimal num11 = (Decimal) -1;
      Decimal? curyAdjgWoAmt = copy.CuryAdjgWOAmt;
      Decimal? nullable12 = curyAdjgWoAmt.HasValue ? new Decimal?(num11 * curyAdjgWoAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust8.CuryAdjgWOAmt = nullable12;
      ARAdjust arAdjust9 = copy;
      Decimal num12 = (Decimal) -1;
      Decimal? adjAmt = copy.AdjAmt;
      Decimal? nullable13 = adjAmt.HasValue ? new Decimal?(num12 * adjAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust9.AdjAmt = nullable13;
      ARAdjust arAdjust10 = copy;
      Decimal num13 = (Decimal) -1;
      Decimal? adjDiscAmt = copy.AdjDiscAmt;
      Decimal? nullable14 = adjDiscAmt.HasValue ? new Decimal?(num13 * adjDiscAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust10.AdjDiscAmt = nullable14;
      ARAdjust arAdjust11 = copy;
      Decimal num14 = (Decimal) -1;
      Decimal? adjPpdAmt = copy.AdjPPDAmt;
      Decimal? nullable15 = adjPpdAmt.HasValue ? new Decimal?(num14 * adjPpdAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust11.AdjPPDAmt = nullable15;
      ARAdjust arAdjust12 = copy;
      Decimal num15 = (Decimal) -1;
      Decimal? adjWoAmt = copy.AdjWOAmt;
      Decimal? nullable16 = adjWoAmt.HasValue ? new Decimal?(num15 * adjWoAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust12.AdjWOAmt = nullable16;
      ARAdjust arAdjust13 = copy;
      Decimal num16 = (Decimal) -1;
      Decimal? curyAdjdAmt = copy.CuryAdjdAmt;
      Decimal? nullable17 = curyAdjdAmt.HasValue ? new Decimal?(num16 * curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust13.CuryAdjdAmt = nullable17;
      ARAdjust arAdjust14 = copy;
      Decimal num17 = (Decimal) -1;
      Decimal? curyAdjdDiscAmt = copy.CuryAdjdDiscAmt;
      Decimal? nullable18 = curyAdjdDiscAmt.HasValue ? new Decimal?(num17 * curyAdjdDiscAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust14.CuryAdjdDiscAmt = nullable18;
      ARAdjust arAdjust15 = copy;
      Decimal num18 = (Decimal) -1;
      Decimal? curyAdjdPpdAmt = copy.CuryAdjdPPDAmt;
      Decimal? nullable19 = curyAdjdPpdAmt.HasValue ? new Decimal?(num18 * curyAdjdPpdAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust15.CuryAdjdPPDAmt = nullable19;
      ARAdjust arAdjust16 = copy;
      Decimal num19 = (Decimal) -1;
      Decimal? curyAdjdWoAmt = copy.CuryAdjdWOAmt;
      Decimal? nullable20 = curyAdjdWoAmt.HasValue ? new Decimal?(num19 * curyAdjdWoAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust16.CuryAdjdWOAmt = nullable20;
      ARAdjust arAdjust17 = copy;
      Decimal num20 = (Decimal) -1;
      Decimal? rgolAmt = copy.RGOLAmt;
      Decimal? nullable21 = rgolAmt.HasValue ? new Decimal?(num20 * rgolAmt.GetValueOrDefault()) : new Decimal?();
      arAdjust17.RGOLAmt = nullable21;
      copy.AdjgCuryInfoID = ((PXSelectBase<ARPayment>) this.Document).Current.CuryInfoID;
      FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjgFinPeriodID>(((PXSelectBase) this.Adjustments).Cache, (object) copy, ((PXSelectBase<ARPayment>) this.Document).Current.AdjTranPeriodID);
      ((PXSelectBase<ARAdjust>) this.Adjustments).Update(copy);
    }
    this.PaymentCharges.ReverseCharges((PX.Objects.CM.IRegister) doc, (PX.Objects.CM.IRegister) ((PXSelectBase<ARPayment>) this.Document).Current);
  }

  public virtual void RefundCheckProc(ARPayment doc)
  {
    ((PXGraph) this).Clear((PXClearOption) 1);
    foreach (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer> pxResult in PXSelectBase<ARPayment, PXSelectJoin<ARPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARPayment.curyInfoID>>, InnerJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<ARPayment.cashAccountID>>>>>>, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      ARPayment arPayment = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer>.op_Implicit(pxResult);
      PX.Objects.CM.Extensions.CurrencyInfo origCurrencyInfo = PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.Currency, Customer>.op_Implicit(pxResult);
      this.CreateApplicationOnRefundToOriginalPayment(this.CopyRefundFromPayment(doc, origCurrencyInfo, arPayment), arPayment);
    }
  }

  private ARPayment CopyRefundFromPayment(
    ARPayment doc,
    PX.Objects.CM.Extensions.CurrencyInfo origCurrencyInfo,
    ARPayment origPayment)
  {
    PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(origCurrencyInfo);
    copy1.CuryInfoID = new long?();
    copy1.IsReadOnly = new bool?(false);
    PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(copy1));
    ARPayment arPayment1 = new ARPayment();
    arPayment1.DocType = "REF";
    arPayment1.RefNbr = (string) null;
    arPayment1.CuryInfoID = copy2.CuryInfoID;
    string refNbr = ((PXSelectBase<ARPayment>) this.Document).Insert(arPayment1).RefNbr;
    ARPayment copy3 = PXCache<ARPayment>.CreateCopy(origPayment);
    copy3.DocType = "REF";
    copy3.RefNbr = refNbr;
    copy3.ExtRefNbr = "";
    ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.noteID>((object) copy3);
    ExternalTransaction externalTransaction = ExternalTransaction.PK.Find((PXGraph) this, origPayment.CCActualExternalTransactionID);
    copy3.RefTranExtNbr = externalTransaction?.TranNumber;
    copy3.CuryInfoID = copy2.CuryInfoID;
    copy3.CATranID = new long?();
    copy3.OpenDoc = new bool?(true);
    copy3.Released = new bool?(false);
    ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.hold>((object) copy3);
    ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.isMigratedRecord>((object) copy3);
    ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.status>((object) copy3);
    copy3.LineCntr = new int?(0);
    copy3.AdjCntr = new int?(0);
    copy3.BatchNbr = (string) null;
    copy3.CuryOrigDocAmt = origPayment.CuryUnappliedBal;
    copy3.CuryChargeAmt = new Decimal?(0M);
    copy3.CurySOApplAmt = new Decimal?(0M);
    copy3.CuryConsolidateChargeTotal = new Decimal?(0M);
    copy3.CuryApplAmt = new Decimal?();
    copy3.CuryUnappliedBal = new Decimal?();
    copy3.CuryWOAmt = new Decimal?();
    copy3.DocDate = doc.DocDate;
    copy3.AdjDate = doc.DocDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy3, doc.AdjTranPeriodID);
    copy3.StatementDate = new DateTime?();
    copy3.Printed = new bool?(false);
    copy3.Emailed = new bool?(false);
    copy3.CCReauthDate = new DateTime?();
    copy3.CCReauthTriesLeft = new int?(0);
    copy3.IsCCAuthorized = new bool?(false);
    copy3.IsCCCaptured = new bool?(false);
    copy3.IsCCCaptureFailed = new bool?(false);
    copy3.IsCCUserAttention = new bool?(false);
    copy3.CCActualExternalTransactionID = new int?();
    copy3.Deposited = new bool?(false);
    copy3.DepositDate = new DateTime?();
    copy3.DepositNbr = (string) null;
    copy3.ClearDate = !copy3.Cleared.GetValueOrDefault() ? new DateTime?() : copy3.DocDate;
    string paymentMethodId = copy3.PaymentMethodID;
    object cashAccountId = (object) copy3.CashAccountID;
    try
    {
      ((PXSelectBase) this.Document).Cache.RaiseFieldVerifying<ARPayment.cashAccountID>((object) copy3, ref cashAccountId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.cashAccountID>((object) copy3);
    }
    ((PXSelectBase<ARPayment>) this.Document).Update(copy3);
    ((PXAction) this.initializeState).Press();
    ARPayment arPayment2 = ((PXSelectBase<ARPayment>) this.Document).Current;
    if (arPayment2.PaymentMethodID != paymentMethodId)
    {
      arPayment2.PaymentMethodID = paymentMethodId;
      arPayment2 = ((PXSelectBase<ARPayment>) this.Document).Update(arPayment2);
    }
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARPayment.adjFinPeriodID>((object) arPayment2, (object) PeriodIDAttribute.FormatForDisplay(doc.AdjFinPeriodID));
    if (copy2 != null)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARPayment.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null));
      currencyInfo.CuryID = copy2.CuryID;
      currencyInfo.CuryRateTypeID = copy2.CuryRateTypeID;
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo);
    }
    return arPayment2;
  }

  private ARAdjust CreateApplicationOnRefundToOriginalPayment(
    ARPayment refund,
    ARPayment origDocument)
  {
    ARAdjust instance = (ARAdjust) ((PXSelectBase) this.Adjustments).Cache.CreateInstance();
    instance.AdjgDocType = refund.DocType;
    instance.AdjgRefNbr = refund.RefNbr;
    instance.AdjdDocType = origDocument.DocType;
    instance.AdjdRefNbr = origDocument.RefNbr;
    return ((PXSelectBase<ARAdjust>) this.Adjustments).Insert(instance);
  }

  protected virtual void ValidatePaymentForRefund(ARPayment document)
  {
    if (!document.Released.GetValueOrDefault() || !document.OpenDoc.GetValueOrDefault())
      return;
    ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<ARRegisterAlias, On<ARAdjust.adjgDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust.adjgRefNbr, Equal<ARRegisterAlias.refNbr>>>>, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.released, NotEqual<True>, And<Not<ARAdjust.voided, Equal<True>, And<ARRegisterAlias.voided, Equal<True>, And<Where<ARRegisterAlias.docType, Equal<ARDocType.payment>, Or<ARRegisterAlias.docType, Equal<ARDocType.prepayment>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) document.DocType,
      (object) document.RefNbr
    }));
    if (arAdjust != null)
      throw new PXException(PXLocalizer.LocalizeFormat("The document has an unreleased application from {0} {1}. To create new applications for the document, remove or release the unreleased application from {0} {1}.", new object[2]
      {
        (object) GetLabel.For<ARDocType>(arAdjust.AdjgDocType),
        (object) arAdjust.AdjgRefNbr
      }));
  }

  protected virtual void ValidatePaymentForVoid(ARPayment payment)
  {
    if (!payment.Released.GetValueOrDefault() || !payment.OpenDoc.GetValueOrDefault() || ARDocType.IsSelfVoiding(payment.DocType))
      return;
    ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXViewOf<ARPayment>.BasedOn<SelectFromBase<ARPayment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, Equal<P.AsString>>>>, And<BqlOperand<ARRegister.origRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<ARPayment.released, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) "RPM",
      (object) payment.RefNbr
    }));
    if (arPayment != null)
      throw new PXException(PXLocalizer.LocalizeFormat("The {0} {1} cannot be {2} because an unreleased {3} document exists for this {4}. To proceed, delete or release the {5} {6} document.", new object[7]
      {
        (object) GetLabel.For<ARDocType>(payment.DocType),
        (object) payment.RefNbr,
        (object) "refunded",
        (object) GetLabel.For<ARDocType>(arPayment.DocType),
        (object) GetLabel.For<ARDocType>(payment.DocType),
        (object) GetLabel.For<ARDocType>(arPayment.DocType),
        (object) arPayment.RefNbr
      }));
  }

  public class ARPaymentSOBalanceCalculator : 
    PXGraphExtension<OrdersToApplyTab, ARPaymentEntry.MultiCurrency, ARPaymentEntry>
  {
    public void CalcBalances(SOAdjust adj, PX.Objects.SO.SOOrder invoice, bool isCalcRGOL, bool DiscOnDiscDate)
    {
      new PaymentBalanceCalculator((IPXCurrencyHelper) ((PXGraphExtension<ARPaymentEntry.MultiCurrency, ARPaymentEntry>) this).Base1).CalcBalances(adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, (IInvoice) invoice, (IAdjustment) adj);
      if (DiscOnDiscDate)
        PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) invoice, (IAdjustment) adj);
      PaymentEntry.WarnDiscount<PX.Objects.SO.SOOrder, SOAdjust>((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, adj.AdjgDocDate, invoice, adj);
      new PaymentBalanceAjuster((IPXCurrencyHelper) ((PXGraphExtension<ARPaymentEntry.MultiCurrency, ARPaymentEntry>) this).Base1).AdjustBalance((IAdjustment) adj);
      if (!isCalcRGOL || adj.Voided.GetValueOrDefault())
        return;
      new PaymentRGOLCalculator((IPXCurrencyHelper) ((PXGraphExtension<ARPaymentEntry.MultiCurrency, ARPaymentEntry>) this).Base1, (IAdjustment) adj, adj.ReverseGainLoss).Calculate((IInvoice) invoice);
    }

    [PXMergeAttributes]
    [PXRemoveBaseAttribute(typeof (PX.Objects.CM.PXCurrencyAttribute))]
    [PXCurrency(typeof (PX.Objects.SO.SOOrder.curyInfoID), typeof (PX.Objects.SO.SOOrder.docBal))]
    protected virtual void SOOrder_CuryDocBal_CacheAttached(PXCache sender)
    {
    }

    public void CalcBalances(SOAdjust adj, bool isCalcRGOL, bool DiscOnDiscDate)
    {
      if (PXTransactionScope.IsConnectionOutOfScope)
        return;
      foreach (PXResult<PX.Objects.SO.SOOrder> pxResult in ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base2.SOOrder_CustomerID_OrderType_RefNbr).Select(new object[3]
      {
        (object) adj.CustomerID,
        (object) adj.AdjdOrderType,
        (object) adj.AdjdOrderNbr
      }))
      {
        PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(PXResult<PX.Objects.SO.SOOrder>.op_Implicit(pxResult));
        ((PXGraphExtension<ARPaymentEntry>) this).Base.internalCall = true;
        SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectGroupBy<SOAdjust, Where<SOAdjust.voided, Equal<False>, And<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<Where<SOAdjust.adjgDocType, NotEqual<Required<SOAdjust.adjgDocType>>, Or<SOAdjust.adjgRefNbr, NotEqual<Required<SOAdjust.adjgRefNbr>>>>>>>>, Aggregate<GroupBy<SOAdjust.adjdOrderType, GroupBy<SOAdjust.adjdOrderNbr, Sum<SOAdjust.curyAdjdAmt, Sum<SOAdjust.adjAmt>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[4]
        {
          (object) adj.AdjdOrderType,
          (object) adj.AdjdOrderNbr,
          (object) adj.AdjgDocType,
          (object) adj.AdjgRefNbr
        }));
        if (soAdjust != null && soAdjust.AdjdOrderNbr != null)
        {
          PX.Objects.SO.SOOrder soOrder1 = copy;
          Decimal? nullable1 = soOrder1.CuryDocBal;
          Decimal? nullable2 = soAdjust.CuryAdjdAmt;
          soOrder1.CuryDocBal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          PX.Objects.SO.SOOrder soOrder2 = copy;
          nullable2 = soOrder2.DocBal;
          nullable1 = soAdjust.AdjAmt;
          soOrder2.DocBal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
        }
        ((PXGraphExtension<ARPaymentEntry>) this).Base.internalCall = false;
        this.CalcBalances(adj, copy, isCalcRGOL, DiscOnDiscDate);
        if (this.Base2.IsApplicationToBlanketOrderWithChild(adj))
        {
          adj.CuryDocBal = new Decimal?(0M);
          adj.DocBal = new Decimal?(0M);
        }
      }
    }

    public virtual void _(
      PX.Data.Events.FieldUpdating<SOAdjust, SOAdjust.curyDocBal> e)
    {
      if (!((PXGraphExtension<ARPaymentEntry>) this).Base.internalCall && e.Row != null)
      {
        if (e.Row.AdjdCuryInfoID.HasValue && !e.Row.CuryDocBal.HasValue && ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<SOAdjust, SOAdjust.curyDocBal>>) e).Cache.GetStatus((object) e.Row) != 3)
        {
          this.CalcBalances(e.Row, false, false);
          this.RemoveNotchangedSalesOrdersFromCache();
        }
        ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<SOAdjust, SOAdjust.curyDocBal>>) e).NewValue = (object) e.Row.CuryDocBal;
      }
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<SOAdjust, SOAdjust.curyDocBal>>) e).Cancel = true;
    }

    protected virtual void RemoveNotchangedSalesOrdersFromCache()
    {
      if (!PXTransactionScope.IsScoped)
        return;
      PXCache cache = ((PXSelectBase) this.Base2.SOOrder_CustomerID_OrderType_RefNbr).Cache;
      foreach (object obj in cache.Cached)
      {
        if (cache.GetStatus(obj) == null)
          cache.Remove(obj);
      }
      cache.ClearQueryCacheObsolete();
    }

    public virtual void _(
      PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.curyAdjgAmt> e)
    {
      this.CalcBalances(e.Row, true, false);
    }

    public virtual void _(
      PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgAmt> e)
    {
      if (!e.Row.CuryDocBal.HasValue || !e.Row.CuryDiscBal.HasValue)
        this.CalcBalances(e.Row, false, false);
      if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgAmt>>) e).Cancel)
        return;
      if (!e.Row.CuryDocBal.HasValue)
        throw new PXSetPropertyException<SOAdjust.adjdOrderNbr>("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<SOAdjust.adjdOrderNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgAmt>>) e).Cache)
        });
      if ((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgAmt>, SOAdjust, object>) e).NewValue < 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
      Decimal num1 = e.Row.CuryDocBal.Value;
      Decimal? nullable = e.Row.CuryAdjgAmt;
      Decimal num2 = nullable.Value;
      if (num1 + num2 - (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgAmt>, SOAdjust, object>) e).NewValue < 0M && !this.Base2.IsApplicationToBlanketOrderWithChild(e.Row))
      {
        object[] objArray = new object[1];
        nullable = e.Row.CuryDocBal;
        Decimal num3 = nullable.Value;
        nullable = e.Row.CuryAdjgAmt;
        Decimal num4 = nullable.Value;
        objArray[0] = (object) (num3 + num4).ToString();
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
      }
    }

    public virtual void _(
      PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.curyAdjgDiscAmt> e)
    {
      this.CalcBalances(e.Row, true, false);
    }

    public virtual void _(
      PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgDiscAmt> e)
    {
      SOAdjust row = e.Row;
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue)
        this.CalcBalances(e.Row, false, false);
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue)
        throw new PXSetPropertyException<SOAdjust.adjdOrderNbr>("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<SOAdjust.adjdOrderNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgDiscAmt>>) e).Cache)
        });
      if ((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgDiscAmt>, SOAdjust, object>) e).NewValue < 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
      Decimal num1 = row.CuryDiscBal.Value;
      Decimal? nullable1 = row.CuryAdjgDiscAmt;
      Decimal num2 = nullable1.Value;
      if (num1 + num2 - (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgDiscAmt>, SOAdjust, object>) e).NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable1 = row.CuryDiscBal;
        Decimal num3 = nullable1.Value;
        nullable1 = row.CuryAdjgDiscAmt;
        Decimal num4 = nullable1.Value;
        objArray[0] = (object) (num3 + num4).ToString();
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
      }
      nullable1 = row.CuryAdjgAmt;
      if (!nullable1.HasValue)
        return;
      if (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgDiscAmt>>) e).Cache.GetValuePending<SOAdjust.curyAdjgAmt>((object) e.Row) != PXCache.NotSetValue)
      {
        nullable1 = (Decimal?) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgDiscAmt>>) e).Cache.GetValuePending<SOAdjust.curyAdjgAmt>((object) e.Row);
        Decimal? curyAdjgAmt = row.CuryAdjgAmt;
        if (!(nullable1.GetValueOrDefault() == curyAdjgAmt.GetValueOrDefault() & nullable1.HasValue == curyAdjgAmt.HasValue))
          return;
      }
      Decimal num5 = row.CuryDocBal.Value;
      Decimal? nullable2 = row.CuryAdjgDiscAmt;
      Decimal num6 = nullable2.Value;
      if (num5 + num6 - (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.curyAdjgDiscAmt>, SOAdjust, object>) e).NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable2 = row.CuryDocBal;
        Decimal num7 = nullable2.Value;
        nullable2 = row.CuryAdjgDiscAmt;
        Decimal num8 = nullable2.Value;
        objArray[0] = (object) (num7 + num8).ToString();
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
      }
    }
  }

  [PXHidden]
  [Serializable]
  public class LoadOptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _FromDate;
    protected DateTime? _TillDate;
    protected string _StartRefNbr;
    protected string _EndRefNbr;
    protected string _StartOrderNbr;
    protected string _EndOrderNbr;
    protected int? _MaxDocs;
    protected string _OrderBy;
    protected string _SOOrderBy;
    protected bool? _IsInvoice;

    [Organization(true, typeof (Null))]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (ARPaymentEntry.LoadOptions.organizationID), true, true, typeof (Null), null)]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (ARPaymentEntry.LoadOptions.organizationID), typeof (ARPaymentEntry.LoadOptions.branchID), null, true)]
    public int? OrgBAccountID { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "From Date")]
    public virtual DateTime? FromDate
    {
      get => this._FromDate;
      set => this._FromDate = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "To Date")]
    [PXDefault(typeof (ARPayment.adjDate))]
    public virtual DateTime? TillDate
    {
      get => this._TillDate;
      set => this._TillDate = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "From Ref. Nbr.")]
    [PXSelector(typeof (Search2<ARInvoice.refNbr, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>, And<Where<ARAdjust.adjgDocType, NotEqual<Current<ARRegister.docType>>, Or<ARAdjust.adjgRefNbr, NotEqual<Current<ARRegister.refNbr>>>>>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARInvoice.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust2.released, NotEqual<True>, And<ARAdjust2.voided, NotEqual<True>>>>>>>>, Where<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<True>, And<ARInvoice.hold, Equal<False>, And<ARAdjust.adjgRefNbr, IsNull, And<ARAdjust2.adjdRefNbr, IsNull, And<ARInvoice.docDate, LessEqual<Current<ARPayment.adjDate>>, And<ARInvoice.tranPeriodID, LessEqual<Current<ARPayment.adjTranPeriodID>>, And<ARInvoice.pendingPPD, NotEqual<True>, And<Where<ARInvoice.customerID, Equal<Optional<ARPayment.customerID>>, Or<Customer.consolidatingBAccountID, Equal<Optional<ARRegister.customerID>>>>>>>>>>>>>>))]
    public virtual string StartRefNbr
    {
      get => this._StartRefNbr;
      set => this._StartRefNbr = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "To Ref. Nbr.")]
    [PXSelector(typeof (Search2<ARInvoice.refNbr, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>, And<Where<ARAdjust.adjgDocType, NotEqual<Current<ARRegister.docType>>, Or<ARAdjust.adjgRefNbr, NotEqual<Current<ARRegister.refNbr>>>>>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARInvoice.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust2.released, NotEqual<True>, And<ARAdjust2.voided, NotEqual<True>>>>>>>>, Where<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<True>, And<ARInvoice.hold, Equal<False>, And<ARAdjust.adjgRefNbr, IsNull, And<ARAdjust2.adjdRefNbr, IsNull, And<ARInvoice.docDate, LessEqual<Current<ARPayment.adjDate>>, And<ARInvoice.tranPeriodID, LessEqual<Current<ARPayment.adjTranPeriodID>>, And<ARInvoice.pendingPPD, NotEqual<True>, And<Where<ARInvoice.customerID, Equal<Optional<ARPayment.customerID>>, Or<Customer.consolidatingBAccountID, Equal<Optional<ARRegister.customerID>>>>>>>>>>>>>>))]
    public virtual string EndRefNbr
    {
      get => this._EndRefNbr;
      set => this._EndRefNbr = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Start Order Nbr.")]
    [PXSelector(typeof (Search2<PX.Objects.SO.SOOrder.orderNbr, LeftJoin<PX.Objects.SO.SOOrderType, On<PX.Objects.SO.SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>>, Where<PX.Objects.SO.SOOrder.customerID, Equal<Optional<ARPayment.customerID>>, And<PX.Objects.SO.SOOrder.openDoc, Equal<True>, And<Where<PX.Objects.SO.SOOrderType.aRDocType, Equal<ARDocType.invoice>, Or<PX.Objects.SO.SOOrderType.aRDocType, Equal<ARDocType.debitMemo>>>>>>>))]
    public virtual string StartOrderNbr
    {
      get => this._StartOrderNbr;
      set => this._StartOrderNbr = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "End Order Nbr.")]
    [PXSelector(typeof (Search2<PX.Objects.SO.SOOrder.orderNbr, LeftJoin<PX.Objects.SO.SOOrderType, On<PX.Objects.SO.SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>>, Where<PX.Objects.SO.SOOrder.customerID, Equal<Optional<ARPayment.customerID>>, And<PX.Objects.SO.SOOrder.openDoc, Equal<True>, And<Where<PX.Objects.SO.SOOrderType.aRDocType, Equal<ARDocType.invoice>, Or<PX.Objects.SO.SOOrderType.aRDocType, Equal<ARDocType.debitMemo>>>>>>>))]
    public virtual string EndOrderNbr
    {
      get => this._EndOrderNbr;
      set => this._EndOrderNbr = value;
    }

    [PXDBInt(MinValue = 0)]
    [PXUIField(DisplayName = "Max. Number of Rows")]
    [PXDefault(100)]
    public virtual int? MaxDocs
    {
      get => this._MaxDocs;
      set => this._MaxDocs = value;
    }

    [PXDBString(5, IsFixed = true)]
    [PXUIField(DisplayName = "Include Child Documents")]
    [ARPaymentEntry.LoadOptions.loadChildDocuments.List]
    [PXDefault("NOONE")]
    public virtual string LoadChildDocuments { get; set; }

    [PXDBString(3, IsFixed = true)]
    [PXUIField(DisplayName = "Sort Order")]
    [ARPaymentEntry.LoadOptions.orderBy.List]
    [PXDefault("DUE")]
    public virtual string OrderBy
    {
      get => this._OrderBy;
      set => this._OrderBy = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Loading mode")]
    [ARPaymentEntry.LoadOptions.loadingMode.List]
    [PXDefault("L")]
    public virtual string LoadingMode { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Automatically Apply Amount Paid")]
    public virtual bool? Apply { get; set; }

    [PXDBString(3, IsFixed = true)]
    [PXUIField(DisplayName = "Sort Order")]
    [ARPaymentEntry.LoadOptions.sOOrderBy.List]
    [PXDefault("DAT")]
    public virtual string SOOrderBy
    {
      get => this._SOOrderBy;
      set => this._SOOrderBy = value;
    }

    [PXDBBool]
    public virtual bool? IsInvoice
    {
      get => this._IsInvoice;
      set => this._IsInvoice = value;
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.organizationID>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPaymentEntry.LoadOptions.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class fromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.fromDate>
    {
    }

    public abstract class tillDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.tillDate>
    {
    }

    public abstract class startRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.startRefNbr>
    {
    }

    public abstract class endRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.endRefNbr>
    {
    }

    public abstract class startOrderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.startOrderNbr>
    {
    }

    public abstract class endOrderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.endOrderNbr>
    {
    }

    public abstract class maxDocs : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPaymentEntry.LoadOptions.maxDocs>
    {
    }

    public abstract class loadChildDocuments : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.loadChildDocuments>
    {
      public const string None = "NOONE";
      public const string ExcludeCRM = "EXCRM";
      public const string IncludeCRM = "INCRM";

      public class none : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        ARPaymentEntry.LoadOptions.loadChildDocuments.none>
      {
        public none()
          : base("NOONE")
        {
        }
      }

      public class excludeCRM : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        ARPaymentEntry.LoadOptions.loadChildDocuments.excludeCRM>
      {
        public excludeCRM()
          : base("EXCRM")
        {
        }
      }

      public class includeCRM : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        ARPaymentEntry.LoadOptions.loadChildDocuments.includeCRM>
      {
        public includeCRM()
          : base("INCRM")
        {
        }
      }

      public class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new string[3]{ "NOONE", "EXCRM", "INCRM" }, new string[3]
          {
            "None",
            "Except Credit Memos",
            "All Types"
          })
        {
        }
      }

      public class NoCRMListAttribute : PXStringListAttribute
      {
        public NoCRMListAttribute()
          : base(new string[2]{ "NOONE", "EXCRM" }, new string[2]
          {
            "None",
            "Except Credit Memos"
          })
        {
        }
      }
    }

    public abstract class orderBy : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.orderBy>
    {
      public const string DueDateRefNbr = "DUE";
      public const string DocDateRefNbr = "DOC";
      public const string RefNbr = "REF";

      public class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new string[3]{ "DUE", "DOC", "REF" }, new string[3]
          {
            "Due Date, Reference Nbr.",
            "Doc. Date, Reference Nbr.",
            "Reference Nbr."
          })
        {
        }
      }
    }

    public abstract class loadingMode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.loadingMode>
    {
      public const string Load = "L";
      public const string Reload = "R";

      public class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new string[2]{ "L", "R" }, new string[2]
          {
            "Load",
            "Reload"
          })
        {
        }
      }
    }

    public abstract class apply : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPaymentEntry.LoadOptions.apply>
    {
    }

    public abstract class sOOrderBy : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.sOOrderBy>
    {
      public const string OrderDateOrderNbr = "DAT";
      public const string OrderNbr = "ORD";

      public class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new string[2]{ "DAT", "ORD" }, new string[2]
          {
            "Order Date, Order Nbr.",
            "Order Nbr."
          })
        {
        }
      }
    }

    public abstract class isInvoice : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARPaymentEntry.LoadOptions.isInvoice>
    {
    }
  }

  public class MultiCurrency : ARMultiCurrencyGraph<ARPaymentEntry, ARPayment>
  {
    protected override string DocumentStatus
    {
      get => ((PXSelectBase<ARPayment>) this.Base.Document).Current?.Status;
    }

    protected override MultiCurrencyGraph<ARPaymentEntry, ARPayment>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<ARPaymentEntry, ARPayment>.CurySourceMapping(typeof (PX.Objects.CA.CashAccount))
      {
        CuryID = typeof (PX.Objects.CA.CashAccount.curyID),
        CuryRateTypeID = typeof (PX.Objects.CA.CashAccount.curyRateTypeID)
      };
    }

    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = base.CurrentSourceSelect();
      if (curySource != null)
        curySource.AllowOverrideRate = (bool?) ((PXSelectBase<Customer>) this.Base.customer)?.Current?.AllowOverrideRate;
      return curySource;
    }

    protected override MultiCurrencyGraph<ARPaymentEntry, ARPayment>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ARPaymentEntry, ARPayment>.DocumentMapping(typeof (ARPayment))
      {
        DocumentDate = typeof (ARPayment.adjDate),
        BAccountID = typeof (ARPayment.customerID)
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
        (PXSelectBase) this.Base.dummy_CATran,
        (PXSelectBase) this.Base.PaymentCharges,
        (PXSelectBase) this.Base.ARPost
      };
    }

    protected override bool ShouldBeDisabledDueToDocStatus()
    {
      string docType = ((PXSelectBase<ARPayment>) this.Base.Document).Current?.DocType;
      return docType == "RPM" || docType == "VRF" || base.ShouldBeDisabledDueToDocStatus();
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<ARPayment, ARPayment.cashAccountID> e)
    {
      if (this.Base._IsVoidCheckInProgress || !PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        return;
      this.RestoreDifferentCurrencyInfoIDs<ARAdjust>((PXSelectBase<ARAdjust>) this.Base.Adjustments);
      this.SourceFieldUpdated<ARPayment.curyInfoID, ARPayment.curyID, ARPayment.adjDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARPayment, ARPayment.cashAccountID>>) e).Cache, (IBqlTable) e.Row);
      this.SetAdjgCuryInfoID<ARAdjust>((PXSelectBase<ARAdjust>) this.Base.Adjustments, e.Row.CuryInfoID);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.documentDate> e)
    {
      base._(e);
      if (this.ShouldBeDisabledDueToDocStatus() || this.Base.HasUnreleasedSOInvoice)
        return;
      this.Base.LoadInvoicesProc(true, (ARPaymentEntry.LoadOptions) null);
      ARPayment arPayment = (ARPayment) e.Row.Base;
      foreach (ARAdjust arAdjust in GraphHelper.RowCast<ARAdjust>(((PXSelectBase) this.Base.Adjustments).View.SelectExternal()).Where<ARAdjust>((Func<ARAdjust, bool>) (adj => adj.Selected.GetValueOrDefault())))
      {
        ARInvoice arInvoice = ARInvoice.PK.Find((PXGraph) this.Base, arAdjust.AdjdDocType, arAdjust.AdjdRefNbr);
        DateTime? adjDate = arPayment.AdjDate;
        DateTime? discDate = (DateTime?) arInvoice?.DiscDate;
        if ((adjDate.HasValue & discDate.HasValue ? (adjDate.GetValueOrDefault() <= discDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 && arInvoice.OrigModule == "AR")
        {
          ARPaymentEntry arPaymentEntry = this.Base;
          Decimal? curyOrigDocAmt = arPayment.CuryOrigDocAmt;
          Decimal num1 = 0M;
          int num2 = !(curyOrigDocAmt.GetValueOrDefault() == num1 & curyOrigDocAmt.HasValue) ? 1 : 0;
          arPaymentEntry.AutoApplyProc(num2 != 0);
        }
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldSelecting<ARAdjust, ARAdjust.adjdCuryID> e)
    {
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<ARAdjust, ARAdjust.adjdCuryID>>) e).ReturnValue = (object) this.CuryIDFieldSelecting<ARAdjust.adjdCuryInfoID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<ARAdjust, ARAdjust.adjdCuryID>>) e).Cache, (object) e.Row);
    }
  }

  public class AutoApplyProcess : FlaggedModeScopeBase<ARPaymentEntry.AutoApplyProcess>
  {
  }

  public class PXLoadInvoiceException : Exception
  {
    public PXLoadInvoiceException()
    {
    }

    public PXLoadInvoiceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

  public class ARPaymentContextExtention : GraphContextExtention<ARPaymentEntry>
  {
  }

  public class ARPaymentEntryCustomerDocsExtension : CustomerDocsExtensionBase<ARPaymentEntry>
  {
  }

  public class CurrencyInfoSelect : 
    PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>
  {
    public CurrencyInfoSelect(PXGraph graph)
      : base(graph)
    {
      ((PXSelectBase) this).View = (PXView) new ARPaymentEntry.CurrencyInfoSelect.PXView(graph, false, (BqlCommand) new Select<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>());
      PXGraph.RowDeletedEvents rowDeleted = graph.RowDeleted;
      ARPaymentEntry.CurrencyInfoSelect currencyInfoSelect = this;
      // ISSUE: virtual method pointer
      PXRowDeleted pxRowDeleted = new PXRowDeleted((object) currencyInfoSelect, __vmethodptr(currencyInfoSelect, CurrencyInfo_RowDeleted));
      rowDeleted.AddHandler<PX.Objects.CM.Extensions.CurrencyInfo>(pxRowDeleted);
    }

    public virtual void CurrencyInfo_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
    {
      ((PXSelectBase) this).View.Clear();
    }

    public class PXView : PXView
    {
      protected Dictionary<long?, object> _cached = new Dictionary<long?, object>();

      public PXView(PXGraph graph, bool isReadOnly, BqlCommand select)
        : base(graph, isReadOnly, select)
      {
      }

      public PXView(PXGraph graph, bool isReadOnly, BqlCommand select, Delegate handler)
        : base(graph, isReadOnly, select, handler)
      {
      }

      public virtual object SelectSingle(params object[] parameters)
      {
        object obj = (object) null;
        if (!this._cached.TryGetValue((long?) parameters[0], out obj))
        {
          obj = base.SelectSingle(parameters);
          if (obj != null)
            this._cached.Add((long?) parameters[0], obj);
        }
        return obj;
      }

      public virtual List<object> SelectMulti(params object[] parameters)
      {
        List<object> objectList = new List<object>();
        object obj;
        if ((obj = base.SelectSingle(parameters)) != null)
          objectList.Add(obj);
        return objectList;
      }

      public virtual void Clear()
      {
        this._cached.Clear();
        base.Clear();
      }
    }
  }

  public class ARPaymentEntryDocumentExtension : 
    PaymentGraphExtension<ARPaymentEntry, ARPayment, ARAdjust, ARInvoice, ARTran>
  {
    private ARInvoiceBalanceCalculator _aRInvoiceBalanceCalculator;

    private ARInvoiceBalanceCalculator ARInvoiceBalanceCalculator
    {
      get
      {
        return this._aRInvoiceBalanceCalculator ?? (this._aRInvoiceBalanceCalculator = new ARInvoiceBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this.Base).GetExtension<ARPaymentEntry.MultiCurrency>(), (PXGraph) this.Base));
      }
    }

    protected override AbstractPaymentBalanceCalculator<ARAdjust, ARTran> GetAbstractBalanceCalculator()
    {
      return (AbstractPaymentBalanceCalculator<ARAdjust, ARTran>) new ARPaymentBalanceCalculator(this.Base);
    }

    protected override bool InternalCall => this.Base.internalCall;

    public override PXSelectBase<ARAdjust> Adjustments
    {
      get => (PXSelectBase<ARAdjust>) this.Base.Adjustments_Raw;
    }

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Payment>((PXSelectBase) this.Base.Document);
    }

    protected override PaymentMapping GetPaymentMapping() => new PaymentMapping(typeof (ARPayment));

    public override void CalcBalancesFromAdjustedDocument(
      ARAdjust adj,
      bool isCalcRGOL,
      bool DiscOnDiscDate)
    {
      PXResultset<ARInvoice> pxResultset;
      if (this.Base.balanceCache == null || !this.Base.balanceCache.TryGetValue(adj, out pxResultset))
        pxResultset = ((PXSelectBase<ARInvoice>) this.Base.ARInvoice_DocType_RefNbr).Select(new object[3]
        {
          (object) adj.AdjdLineNbr,
          (object) adj.AdjdDocType,
          (object) adj.AdjdRefNbr
        });
      using (IEnumerator<PXResult<ARInvoice>> enumerator = pxResultset.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult<ARInvoice> current = enumerator.Current;
          ARInvoice voucher = PXResult<ARInvoice>.op_Implicit(current);
          ARTran tran = PXResult.Unwrap<ARTran>((object) current);
          this.CalcBalances<ARInvoice>(adj, voucher, isCalcRGOL, DiscOnDiscDate, tran);
          return;
        }
      }
      foreach (PXResult<ARPayment> pxResult in ((PXSelectBase<ARPayment>) this.Base.ARPayment_DocType_RefNbr).Select(new object[2]
      {
        (object) adj.AdjdDocType,
        (object) adj.AdjdRefNbr
      }))
      {
        ARPayment voucher = PXResult<ARPayment>.op_Implicit(pxResult);
        this.CalcBalances<ARPayment>(adj, voucher, isCalcRGOL, DiscOnDiscDate, (ARTran) null);
      }
    }

    public override void CalcBalances<T>(
      ARAdjust adj,
      T voucher,
      bool isCalcRGOL,
      bool DiscOnDiscDate,
      ARTran tran)
    {
      if (!voucher.Released.GetValueOrDefault())
      {
        ARPayment current1 = ((PXSelectBase<ARPayment>) this.Base.Document).Current;
        if ((current1 != null ? (current1.IsCCPayment.GetValueOrDefault() ? 1 : 0) : 0) == 0 && !(adj.AdjAmt.GetValueOrDefault() == 0M))
        {
          ARPayment current2 = ((PXSelectBase<ARPayment>) this.Base.Document).Current;
          if ((current2 != null ? (current2.CustomerID.HasValue ? 1 : 0) : 0) == 0)
            return;
          this.ARInvoiceBalanceCalculator.CalcBalancesFromInvoiceSide(adj, (IInvoice) voucher, ((PXSelectBase<ARPayment>) this.Base.Document).Current, isCalcRGOL, DiscOnDiscDate);
          return;
        }
      }
      base.CalcBalances<T>(adj, voucher, isCalcRGOL, DiscOnDiscDate, tran);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.curyAdjgPPDAmt> e)
    {
      if (e.Row == null)
        return;
      e.Row.FillDiscAmts();
      this.CalcBalancesFromAdjustedDocument(e.Row, true, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt> e)
    {
      ARAdjust adj = e.Row;
      using (IEnumerator<string> enumerator = ((IEnumerable<string>) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>>) e).Cache.Keys).Where<string>((Func<string, bool>) (key => ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>>) e).Cache.GetValue((object) adj, key) == null)).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>>) e).Cache, current)
          });
        }
      }
      Decimal? nullable = adj.CuryDocBal;
      if (nullable.HasValue)
      {
        nullable = adj.CuryDiscBal;
        if (nullable.HasValue)
        {
          nullable = adj.CuryWOBal;
          if (nullable.HasValue)
            goto label_11;
        }
      }
      this.CalcBalancesFromAdjustedDocument(e.Row, false, false);
label_11:
      nullable = adj.CuryDocBal;
      if (!nullable.HasValue)
      {
        ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>>) e).Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) adj, (object) adj.AdjdRefNbr, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARAdjust.adjdRefNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>>) e).Cache)
        }));
      }
      else
      {
        nullable = adj.CuryOrigDocAmt;
        Decimal num1 = 0M;
        Sign sign = nullable.GetValueOrDefault() < num1 & nullable.HasValue ? Sign.Minus : Sign.Plus;
        int? voidAdjNbr = adj.VoidAdjNbr;
        if (!voidAdjNbr.HasValue && Sign.op_Multiply((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>, ARAdjust, object>) e).NewValue, sign) < 0M)
          throw new PXSetPropertyException(Sign.op_Equality(sign, Sign.Plus) ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        voidAdjNbr = adj.VoidAdjNbr;
        if (voidAdjNbr.HasValue && Sign.op_Multiply((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>, ARAdjust, object>) e).NewValue, sign) > 0M)
          throw new PXSetPropertyException(Sign.op_Equality(sign, Sign.Plus) ? "Incorrect value. The value to be entered must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
        nullable = adj.CuryDocBal;
        Decimal num2 = nullable.Value;
        nullable = adj.CuryAdjgAmt;
        Decimal num3 = nullable.Value;
        if (Sign.op_Multiply(num2 + num3 - (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgAmt>, ARAdjust, object>) e).NewValue, sign) < 0M)
        {
          string str = Sign.op_Equality(sign, Sign.Plus) ? "Incorrect value. The value to be entered must be less than or equal to {0}." : "Incorrect value. The value to be entered must be greater than or equal to {0}.";
          object[] objArray = new object[1];
          nullable = adj.CuryDocBal;
          Decimal num4 = nullable.Value;
          nullable = adj.CuryAdjgAmt;
          Decimal num5 = nullable.Value;
          objArray[0] = (object) (num4 + num5).ToString();
          throw new PXSetPropertyException(str, objArray);
        }
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.curyAdjgAmt> e)
    {
      if (e.Row == null || e.NewValue == null)
        return;
      this.CalcBalancesFromAdjustedDocument(e.Row, true, this.Base.InternalCall);
      ARAdjust row = e.Row;
      Decimal? nullable1 = e.Row.CuryAdjgAmt;
      Decimal num1 = 0M;
      int num2;
      if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      {
        nullable1 = e.Row.CuryAdjgPPDAmt;
        Decimal num3 = 0M;
        num2 = !(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue) ? 1 : 0;
      }
      else
        num2 = 1;
      bool? nullable2 = new bool?(num2 != 0);
      row.Selected = nullable2;
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt> e)
    {
      ARAdjust row = e.Row;
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue || !row.CuryWOBal.HasValue)
        this.CalcBalancesFromAdjustedDocument(e.Row, false, false);
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue)
      {
        ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>>) e).Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARAdjust.adjdRefNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>>) e).Cache)
        }));
      }
      else
      {
        Decimal? nullable1 = row.CuryOrigDocAmt;
        Decimal num1 = 0M;
        Sign sign = nullable1.GetValueOrDefault() < num1 & nullable1.HasValue ? Sign.Minus : Sign.Plus;
        if (!row.VoidAdjNbr.HasValue && Sign.op_Multiply(sign, Math.Sign((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>, ARAdjust, object>) e).NewValue)) < 0 || row.VoidAdjNbr.HasValue && Sign.op_Multiply(sign, Math.Sign((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>, ARAdjust, object>) e).NewValue)) > 0)
          throw new PXSetPropertyException((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>, ARAdjust, object>) e).NewValue < 0M ? "Incorrect value. The value to be entered must be greater than or equal to {0}." : "Incorrect value. The value to be entered must be less than or equal to {0}.", (PXErrorLevel) 0);
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
            if (Math.Sign(nullable1.Value) * Math.Sign((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>, ARAdjust, object>) e).NewValue) <= 0)
              goto label_11;
          }
          if (Math.Abs(num2) < Math.Abs((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>, ARAdjust, object>) e).NewValue))
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
          if (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>>) e).Cache.GetValuePending<ARAdjust.curyAdjgAmt>((object) e.Row) != PXCache.NotSetValue)
          {
            nullable1 = (Decimal?) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>>) e).Cache.GetValuePending<ARAdjust.curyAdjgAmt>((object) e.Row);
            nullable2 = row.CuryAdjgAmt;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
              goto label_16;
          }
          nullable2 = row.CuryDocBal;
          Decimal num4 = Math.Abs(nullable2.Value);
          nullable2 = row.CuryAdjgPPDAmt;
          Decimal num5 = Math.Abs(nullable2.Value);
          if (num4 + num5 < Math.Abs((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgPPDAmt>, ARAdjust, object>) e).NewValue))
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
        if (row.AdjdHasPPDTaxes.GetValueOrDefault() && row.AdjgDocType == "CRM")
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be equal to {0}.", new object[1]
          {
            (object) 0.ToString()
          });
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgWOAmt> e)
    {
      ARAdjust row = e.Row;
      if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue || !row.CuryWOBal.HasValue)
        this.CalcBalancesFromAdjustedDocument(e.Row, false, false);
      if (!row.CuryDocBal.HasValue || !row.CuryWOBal.HasValue)
      {
        ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgWOAmt>>) e).Cache.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<ARAdjust.adjdRefNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgWOAmt>>) e).Cache)
        }));
      }
      else
      {
        Decimal num1 = row.CuryWOBal.Value;
        Decimal? nullable1 = row.CuryAdjgWOAmt;
        Decimal num2 = Math.Abs(nullable1.Value);
        if (num1 + num2 - Math.Abs((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgWOAmt>, ARAdjust, object>) e).NewValue) < 0M)
        {
          object[] objArray = new object[1];
          nullable1 = row.CuryWOBal;
          Decimal num3 = nullable1.Value;
          nullable1 = row.CuryAdjgWOAmt;
          Decimal num4 = Math.Abs(nullable1.Value);
          objArray[0] = (object) (num3 + num4).ToString();
          throw new PXSetPropertyException("The customer's write-off limit {0} has been exceeded.", objArray);
        }
        nullable1 = row.CuryAdjgAmt;
        if (!nullable1.HasValue)
          return;
        Decimal? nullable2;
        if (((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgWOAmt>>) e).Cache.GetValuePending<ARAdjust.curyAdjgAmt>((object) e.Row) != PXCache.NotSetValue)
        {
          nullable1 = (Decimal?) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgWOAmt>>) e).Cache.GetValuePending<ARAdjust.curyAdjgAmt>((object) e.Row);
          nullable2 = row.CuryAdjgAmt;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            return;
        }
        nullable2 = row.CuryDocBal;
        Decimal num5 = nullable2.Value;
        nullable2 = row.CuryAdjgWOAmt;
        Decimal num6 = nullable2.Value;
        if (num5 + num6 - (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.curyAdjgWOAmt>, ARAdjust, object>) e).NewValue < 0M)
        {
          object[] objArray = new object[1];
          nullable2 = row.CuryDocBal;
          Decimal num7 = nullable2.Value;
          nullable2 = row.CuryAdjgWOAmt;
          Decimal num8 = nullable2.Value;
          objArray[0] = (object) (num7 + num8).ToString();
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
        }
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.curyAdjgWOAmt> e)
    {
      this.CalcBalancesFromAdjustedDocument(e.Row, true, false);
    }
  }
}
