// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashSaleEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.Repositories;
using PX.Objects.AR.Standalone;
using PX.Objects.BQLConstants;
using PX.Objects.CA;
using PX.Objects.CC.Common;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.Common.Interfaces;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.DR.Descriptor;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AR;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class ARCashSaleEntry : 
  ARDataEntryGraph<
  #nullable disable
  ARCashSaleEntry, ARCashSale>,
  IGraphWithInitialization
{
  public PXWorkflowEventHandler<ARCashSale> OnUpdateStatus;
  public PXSelect<PX.Objects.IN.InventoryItem> dummy_nonstockitem_for_redirect_newitem;
  public PXSelect<PX.Objects.AP.Vendor> dummy_vendor_taxAgency_for_avalara;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (ARCashSale.extRefNbr), typeof (ARCashSale.clearDate), typeof (ARCashSale.cleared)})]
  [PXViewName("Cash Sale")]
  public PXSelectJoin<ARCashSale, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARCashSale.customerID>>>, Where<ARCashSale.docType, Equal<Optional<ARCashSale.docType>>, And2<Where<ARRegister.origModule, NotEqual<BatchModule.moduleSO>, Or<ARCashSale.released, Equal<boolTrue>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> Document;
  public PXSelect<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARCashSale.docType>>, And<ARCashSale.refNbr, Equal<Current<ARCashSale.refNbr>>>>> CurrentDocument;
  public PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<ARCashSale.processingCenterID>>>> ProcessingCenter;
  [PXViewName("AR Transactions")]
  public PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>> Transactions;
  public PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARCashSale.docType>>, And<ARTax.refNbr, Equal<Current<ARCashSale.refNbr>>>>, OrderBy<Asc<ARTax.tranType, Asc<ARTax.refNbr, Asc<ARTax.taxID>>>>> Tax_Rows;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARCashSale.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>>> Taxes;
  [PXViewName("AR Bill-To Address")]
  public PXSelect<ARAddress, Where<ARAddress.addressID, Equal<Current<ARCashSale.billAddressID>>>> Billing_Address;
  [PXViewName("AR Bill-To Contact")]
  public PXSelect<ARContact, Where<ARContact.contactID, Equal<Current<ARCashSale.billContactID>>>> Billing_Contact;
  [PXViewName("AR Ship-To Address")]
  public PXSelect<ARShippingAddress, Where<ARShippingAddress.addressID, Equal<Current<ARCashSale.shipAddressID>>>> Shipping_Address;
  [PXViewName("AR Ship-To Contact")]
  public PXSelect<ARShippingContact, Where<ARShippingContact.contactID, Equal<Current<ARCashSale.shipContactID>>>> Shipping_Contact;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARCashSale.curyInfoID>>>> currencyinfo;
  [PXReadOnlyView]
  public PXSelect<CATran, Where<CATran.tranID, Equal<Current<ARCashSale.cATranID>>>> dummy_CATran;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  [PXViewName("Customer")]
  public PXSetup<Customer, Where<Customer.bAccountID, Equal<Optional<ARCashSale.customerID>>>> customer;
  public PXSetup<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>> customerclass;
  public PXSetup<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<ARCashSale.cashAccountID>>>> cashaccount;
  public PXSetup<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<ARCashSale.adjFinPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<ARCashSale.branchID>>>>> finperiod;
  public PXSetup<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>>> paymentmethod;
  public PXSetup<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<ARCashSale.taxZoneID>>>> taxzone;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARCashSale.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<ARCashSale.customerLocationID>>>>> location;
  public PXSetup<GLSetup> glsetup;
  public PXSetup<ARSetup> arsetup;
  public PXSelect<ARBalances> arbalances;
  public PXSelect<CustSalesPeople, Where<CustSalesPeople.bAccountID, Equal<Current<ARCashSale.customerID>>, And<CustSalesPeople.locationID, Equal<Current<ARCashSale.customerLocationID>>>>> salesPerSettings;
  public PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Current<ARCashSale.docType>>, And<ARSalesPerTran.refNbr, Equal<Current<ARCashSale.refNbr>>, And<ARSalesPerTran.adjdDocType, Equal<ARDocType.undefined>, And2<Where<Current<ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byInvoice>, Or<Current<ARCashSale.released>, Equal<boolFalse>>>, Or<ARSalesPerTran.adjdDocType, Equal<Current<ARCashSale.docType>>, And<ARSalesPerTran.adjdRefNbr, Equal<Current<ARCashSale.refNbr>>, And<Current<ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byPayment>>>>>>>>> salesPerTrans;
  public ARPaymentChargeSelect<ARCashSale, ARCashSale.paymentMethodID, ARCashSale.cashAccountID, ARCashSale.docDate, ARCashSale.tranPeriodID, ARCashSale.pMInstanceID, Where<ARPaymentChargeTran.docType, Equal<Current<ARCashSale.docType>>, And<ARPaymentChargeTran.refNbr, Equal<Current<ARCashSale.refNbr>>>>> PaymentCharges;
  public PXSelect<ARSetupApproval, Where<Current<ARCashSale.docType>, Equal<ARDocType.cashReturn>, And<ARSetupApproval.docType, Equal<ARDocType.cashReturn>>>> SetupApproval;
  public PXSelect<ExternalTransaction, Where<ExternalTransaction.refNbr, Equal<Current<ARCashSale.refNbr>>, And<ExternalTransaction.docType, Equal<Current<ARCashSale.docType>>, Or<Where<ExternalTransaction.refNbr, Equal<Current<ARCashSale.origRefNbr>>, And<ExternalTransaction.docType, Equal<Current<ARCashSale.origDocType>>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>> ExternalTran;
  public PXSelectOrderBy<CCProcTran, OrderBy<Desc<CCProcTran.tranNbr>>> ccProcTran;
  [PXCopyPasteHiddenView]
  [PXViewName("Customer Payment Method Details")]
  public FbqlSelect<SelectFromBase<CustomerPaymentMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CustomerPaymentMethod.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ARCashSale.customerID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  CustomerPaymentMethod.paymentMethodID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARCashSale.paymentMethodID, IBqlString>.FromCurrent>>>, 
  #nullable disable
  CustomerPaymentMethod>.View CustomerPaymentMethodDetails;
  [PXViewName("Approval")]
  public EPApprovalAutomationWithoutHoldDefaulting<ARCashSale, ARCashSale.approved, ARCashSale.rejected, ARCashSale.hold, ARSetupApproval> Approval;
  public PXAction<ARCashSale> ViewOriginalDocument;
  public PXAction<ARCashSale> reclassifyBatch;
  public PXAction<ARCashSale> customerDocuments;
  public PXAction<ARCashSale> sendARInvoiceMemo;
  public PXAction<ARCashSale> validateAddresses;
  public PXAction<ARCashSale> viewSchedule;
  public PXAction<ARCashSale> emailInvoice;
  public PXAction<ARCashSale> printInvoice;
  public PXAction<ARCashSale> sendEmail;
  protected bool InternalCall;
  private bool _IsVoidCheckInProgress;

  public IEnumerable CcProcTran()
  {
    ARCashSaleEntry graph = this;
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

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  internal ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  public ARCashSaleEntry()
  {
    PXResultset<ARSetup>.op_Implicit(((PXSelectBase<ARSetup>) this.arsetup).Select(Array.Empty<object>()));
    PXResultset<GLSetup>.op_Implicit(((PXSelectBase<GLSetup>) this.glsetup).Select(Array.Empty<object>()));
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    ARCashSaleEntry arCashSaleEntry = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) arCashSaleEntry, __vmethodptr(arCashSaleEntry, ParentRowUpdated));
    rowUpdated.AddHandler<ARCashSale>(pxRowUpdated);
    OpenPeriodAttribute.SetValidatePeriod<ARCashSale.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9__49_0 ?? (ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9__49_0 = new PXFieldDefaulting((object) ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__49_0))));
    PXCache cach1 = ((PXGraph) this).Caches[typeof (ARAddress)];
    PXCache cach2 = ((PXGraph) this).Caches[typeof (ARContact)];
    PXCache cach3 = ((PXGraph) this).Caches[typeof (ARShippingAddress)];
    PXCache cach4 = ((PXGraph) this).Caches[typeof (ARShippingContact)];
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<ARCashSale>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (ARTran), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<ARTran.tranType>((PXDbType) 3, new int?(3), (object) ((PXSelectBase<ARCashSale>) ((ARCashSaleEntry) graph).Document).Current?.DocType),
        (PXDataFieldValue) new PXDataFieldValue<ARTran.refNbr>((object) ((PXSelectBase<ARCashSale>) ((ARCashSaleEntry) graph).Document).Current?.RefNbr)
      }))
    });
  }

  public override Dictionary<string, string> PrepareReportParams(string reportID, ARCashSale doc)
  {
    if (!(reportID == "AR641000"))
      return base.PrepareReportParams(reportID, doc);
    return new Dictionary<string, string>()
    {
      ["ARInvoice.DocType"] = doc.DocType,
      ["ARInvoice.RefNbr"] = doc.RefNbr
    };
  }

  [PXDefault(typeof (Search<INPostClass.cOGSSubID, Where<INPostClass.postClassID, Equal<Current<PX.Objects.IN.InventoryItem.postClassID>>>>))]
  [SubAccount(typeof (PX.Objects.IN.InventoryItem.cOGSAcctID), DisplayName = "Expense Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual void InventoryItem_COGSSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (ARRegister.docType))]
  protected virtual void ARSalesPerTran_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARCashSale.refNbr))]
  [PXParent(typeof (Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARSalesPerTran.docType>>, And<ARCashSale.refNbr, Equal<Current<ARSalesPerTran.refNbr>>>>>))]
  protected virtual void ARSalesPerTran_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (ARCashSale.branchID))]
  protected virtual void ARSalesPerTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [SalesPerson(DirtyRead = true, Enabled = false, IsKey = true, DescriptionField = typeof (PX.Objects.CR.Contact.displayName))]
  protected virtual void ARSalesPerTran_SalespersonID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search<CustSalesPeople.commisionPct, Where<CustSalesPeople.bAccountID, Equal<Current<ARCashSale.customerID>>, And<CustSalesPeople.locationID, Equal<Current<ARCashSale.customerLocationID>>, And<CustSalesPeople.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>>, Search<SalesPerson.commnPct, Where<SalesPerson.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>))]
  [PXUIField(DisplayName = "Commission %")]
  protected virtual void ARSalesPerTran_CommnPct_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (ARSalesPerTran.curyInfoID), typeof (ARSalesPerTran.commnblAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commissionable Amount", Enabled = false)]
  [PXFormula(null, typeof (SumCalc<ARCashSale.curyCommnblAmt>))]
  protected virtual void ARSalesPerTran_CuryCommnblAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (ARSalesPerTran.curyInfoID), typeof (ARSalesPerTran.commnAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<ARSalesPerTran.curyCommnblAmt, Div<ARSalesPerTran.commnPct, decimal100>>), typeof (SumCalc<ARCashSale.curyCommnAmt>))]
  [PXUIField(DisplayName = "Commission Amt.", Enabled = false)]
  protected virtual void ARSalesPerTran_CuryCommnAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXLineNbr(typeof (ARCashSale.chargeCntr), DecrementOnDelete = false)]
  public virtual void ARPaymentChargeTran_LineNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (ARCashSale.cashAccountID))]
  public virtual void ARPaymentChargeTran_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  /// <summary>
  /// <see cref="P:PX.Objects.AR.ARPaymentChargeTran.EntryTypeID" /> cache attached event.
  /// </summary>
  [PXMergeAttributes]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<ARCashSale.cashAccountID>>, And<CAEntryType.drCr, Equal<CADrCr.cACredit>>>>))]
  public virtual void ARPaymentChargeTran_EntryTypeID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (ARCashSale.adjDate))]
  public virtual void ARPaymentChargeTran_TranDate_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<PMProject.contractID, Where<PMProject.customerID, Equal<Current<ARPayment.customerID>>, Or<PMProject.contractID, Equal<Zero>>>>), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  [ProjectDefault]
  public virtual void _(
    PX.Data.Events.CacheAttached<ARPaymentChargeTran.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault]
  [FinPeriodID(null, typeof (ARPaymentChargeTran.cashAccountID), typeof (Selector<ARPaymentChargeTran.cashAccountID, PX.Objects.CA.CashAccount.branchID>), null, null, null, true, false, null, typeof (ARPaymentChargeTran.tranPeriodID), typeof (ARCashSale.adjTranPeriodID), true, true)]
  public virtual void ARPaymentChargeTran_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARPaymentChargeTran.consolidate, Equal<True>>, ARPaymentChargeTran.curyTranAmt>, decimal0>), typeof (SumCalc<ARCashSale.curyConsolidateChargeTotal>))]
  public virtual void ARPaymentChargeTran_CuryTranAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<ARShippingAddress.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARShippingAddress.longitude> e)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (ARCashSale.projectID))]
  protected virtual void ARTran_ProjectID_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, typeof (ARTran.branchID), null, null, null, null, true, false, null, typeof (ARTran.tranPeriodID), typeof (ARCashSale.adjTranPeriodID), true, true)]
  protected virtual void ARTran_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Coalesce<Search<PMAccountTask.taskID, Where<PMAccountTask.projectID, Equal<Current<ARTran.projectID>>, And<PMAccountTask.accountID, Equal<Current<ARTran.accountID>>>>>, Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<ARTran.projectID>>, And<PMTask.isDefault, Equal<True>>>>>))]
  [ActiveProjectTask(typeof (ARTran.projectID), "CA", DisplayName = "Project Task")]
  protected virtual void ARTran_TaskID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (ARRegister.branchID))]
  [Branch(null, null, true, true, true, Enabled = false)]
  protected virtual void ARTaxTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, typeof (ARTaxTran.branchID), null, null, null, null, true, false, null, null, typeof (ARCashSale.adjTranPeriodID), true, true)]
  [PXDefault]
  protected virtual void ARTaxTran_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (ARCashSale.docDate))]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (ARCashSale.customerID))]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (ARCashSale.docDesc))]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARCashSale.curyInfoID))]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (ARCashSale.curyOrigDocAmt))]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (ARCashSale.origDocAmt))]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_SourceItemType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARCashSale>) this.Document).Current == null)
      return;
    e.NewValue = (object) new ARDocType.ListAttribute().ValueLabelDic[((PXSelectBase<ARCashSale>) this.Document).Current.DocType];
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARCashSale>) this.Document).Current == null)
      return;
    e.NewValue = (object) EPApprovalHelper.BuildEPApprovalDetailsString(sender, (IApprovalDescription) ((PXSelectBase<ARCashSale>) this.Document).Current);
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctCD), DirtyRead = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.bAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctCD), DirtyRead = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRSMEmail.bAccountID> e)
  {
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARCashSaleEntry.\u003C\u003Ec__DisplayClass84_0 cDisplayClass840 = new ARCashSaleEntry.\u003C\u003Ec__DisplayClass84_0();
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass840.list = new List<ARRegister>();
    foreach (ARCashSale arCashSale in adapter.Get<ARCashSale>())
    {
      bool? hold = arCashSale.Hold;
      bool flag = false;
      if (hold.GetValueOrDefault() == flag & hold.HasValue)
      {
        GraphHelper.MarkUpdated(cache, (object) arCashSale);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass840.list.Add((ARRegister) arCashSale);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass840.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass840, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass840.list;
  }

  /// <summary>
  /// Ask user for approval for creation of another reversal if reversing document already exists.
  /// </summary>
  /// <param name="origDoc">The original document.</param>
  /// <returns>True if user approves, false if not.</returns>
  protected virtual bool AskUserApprovalIfReversingDocumentAlreadyExists(ARCashSale origDoc)
  {
    ARRegister arRegister = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<ARDocType.cashReturn>, And<ARRegister.origDocType, Equal<Required<ARRegister.origDocType>>, And<ARRegister.origRefNbr, Equal<Required<ARRegister.origRefNbr>>>>>, OrderBy<Desc<ARRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) origDoc.DocType,
      (object) origDoc.RefNbr
    }));
    if (arRegister == null)
      return true;
    return ((PXSelectBase) this.Document).View.Ask(PXMessages.LocalizeFormatNoPrefix("A reversing document ({0} {1}) already exists for the original document. Reverse the document?", new object[2]
    {
      (object) ARDocType.GetDisplayName("RCS"),
      (object) arRegister.RefNbr
    }), (MessageButtons) 4) == 6;
  }

  [PXUIField]
  [PXProcessButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable VoidCheck(PXAdapter adapter)
  {
    bool? nullable;
    if (((PXSelectBase<ARCashSale>) this.Document).Current != null)
    {
      nullable = ((PXSelectBase<ARCashSale>) this.Document).Current.Released;
      if (nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<ARCashSale>) this.Document).Current.Voided;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue && ((PXSelectBase<ARCashSale>) this.Document).Current.DocType == "CSL")
        {
          ARCashSale copy = PXCache<ARCashSale>.CreateCopy(((PXSelectBase<ARCashSale>) this.Document).Current);
          this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<ARCashSale.finPeriodID, ARCashSale.branchID>(((PXSelectBase) this.Document).Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aRClosed));
          if (!FlaggedModeScopeBase<SettlementProcessScope, string>.IsActive && !this.AskUserApprovalIfReversingDocumentAlreadyExists(copy))
            return adapter.Get();
          try
          {
            this._IsVoidCheckInProgress = true;
            this.VoidCheckProc(copy);
          }
          finally
          {
            this._IsVoidCheckInProgress = false;
          }
          ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<ARCashSale.finPeriodID>((object) ((PXSelectBase<ARCashSale>) this.Document).Current, (object) ((PXSelectBase<ARCashSale>) this.Document).Current.FinPeriodID, (Exception) null);
          List<ARCashSale> arCashSaleList = new List<ARCashSale>();
          if (!((PXGraph) this).IsContractBasedAPI && !((PXGraph) this).IsImport && !FlaggedModeScopeBase<SettlementProcessScope, string>.IsActive)
            throw new PXRedirectRequiredException((PXGraph) this, "Voided");
          return (IEnumerable) new ARCashSale[1]
          {
            ((PXSelectBase<ARCashSale>) this.Document).Current
          };
        }
      }
    }
    if (((PXSelectBase<ARCashSale>) this.Document).Current != null)
    {
      nullable = ((PXSelectBase<ARCashSale>) this.Document).Current.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = ((PXSelectBase<ARCashSale>) this.Document).Current.Voided;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && ((PXSelectBase<ARCashSale>) this.Document).Current.DocType == "CSL" && ExternalTranHelper.HasTransactions((PXSelectBase<ExternalTransaction>) this.ExternalTran))
        {
          ARCashSale current = ((PXSelectBase<ARCashSale>) this.Document).Current;
          current.Voided = new bool?(true);
          current.OpenDoc = new bool?(false);
          current.PendingProcessing = new bool?(false);
          ((PXSelectBase<ARCashSale>) this.Document).Update(current);
          ((PXAction) this.Save).Press();
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewOriginalDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(((PXSelectBase<ARCashSale>) this.Document).Current.OrigDocType, ((PXSelectBase<ARCashSale>) this.Document).Current.OrigRefNbr, ((PXSelectBase<ARCashSale>) this.Document).Current.OrigModule);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ReclassifyBatch(PXAdapter adapter)
  {
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Document).Current;
    if (current != null)
      ReclassifyTransactionsProcess.TryOpenForReclassificationOfDocument(((PXSelectBase) this.Document).View, "AR", current.BatchNbr, current.DocType, current.RefNbr);
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
  public virtual IEnumerable SendARInvoiceMemo(PXAdapter adapter)
  {
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Document).Current;
    if (((PXSelectBase<ARCashSale>) this.Document).Current != null)
    {
      using (new LocalizationFeatureScope((PXGraph) this))
      {
        Dictionary<string, string> parameters = new Dictionary<string, string>()
        {
          ["DocType"] = current.DocType,
          ["RefNbr"] = current.RefNbr
        };
        ((PXGraph) this).GetExtension<ARCashSaleEntry_ActivityDetailsExt>().SendNotification("Customer", "INVOICE", current.BranchID, (IDictionary<string, string>) parameters, false, (IList<Guid?>) null);
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    ARCashSaleEntry arCashSaleEntry = this;
    foreach (ARCashSale arCashSale in adapter.Get<ARCashSale>())
    {
      if (arCashSale != null)
        ((PXGraph) arCashSaleEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) arCashSale;
    }
  }

  [PXUIField]
  [PXLookupButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    ARTran current = ((PXSelectBase<ARTran>) this.Transactions).Current;
    if (current != null && ((PXSelectBase) this.Transactions).Cache.GetStatus((object) current) == null)
    {
      ((PXAction) this.Save).Press();
      ARInvoiceEntry.ViewScheduleForLine((PXGraph) this, (ARRegister) ((PXSelectBase<ARCashSale>) this.Document).Current, ((PXSelectBase<ARTran>) this.Transactions).Current);
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable EmailInvoice(PXAdapter adapter) => this.SendARInvoiceMemo(adapter);

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintInvoice(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AR641000");
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable SendEmail(PXAdapter adapter)
  {
    return ((PXAction) ((PXGraph) this).GetExtension<ARCashSaleEntry_ActivityDetailsExt>().NewMailActivity).Press(adapter);
  }

  [PXMergeAttributes]
  [PXUIField]
  public virtual void ARCashSale_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARCashSale_DocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "CSL";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARCashSale_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARCashSale row = e.Row as ARCashSale;
    if (!this.IsApprovalRequired(row))
      return;
    sender.SetValue<ARCashSale.hold>((object) row, (object) true);
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void ARCashSale_CustomerID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARCashSale_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.customer.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<ARCashSale.customerLocationID>(e.Row);
    sender.SetDefaultExt<ARRegister.printInvoice>(e.Row);
    sender.SetDefaultExt<ARPayment.paymentMethodID>(e.Row);
    if (((ARRegister) e.Row).DocType != "CRM")
      sender.SetDefaultExt<ARCashSale.termsID>(e.Row);
    else
      sender.SetValueExt<ARCashSale.termsID>(e.Row, (object) null);
    SharedRecordAttribute.DefaultRecord<ARCashSale.billAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<ARCashSale.billContactID>(sender, e.Row);
  }

  protected virtual void ARCashSale_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ARCashSale row))
      return;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  public virtual string GetDefaultTaxZone(ARCashSale row)
  {
    string defaultTaxZone = (string) null;
    if (row != null)
    {
      PX.Objects.CR.Location location1 = ((PXSelectBase<PX.Objects.CR.Location>) this.location).SelectSingle(new object[1]
      {
        (object) row.CustomerLocationID
      });
      if (location1 != null && !string.IsNullOrEmpty(location1.CTaxZoneID))
        defaultTaxZone = location1.CTaxZoneID;
      if (defaultTaxZone == null)
      {
        ARShippingAddress adrress = PXResultset<ARShippingAddress>.op_Implicit(((PXSelectBase<ARShippingAddress>) this.Shipping_Address).Select(Array.Empty<object>()));
        if (adrress != null)
          defaultTaxZone = TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, (IAddressBase) adrress);
      }
      if (defaultTaxZone == null)
      {
        PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.BranchID
        }));
        if (baccount != null)
        {
          PX.Objects.CR.Location location2 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) baccount.BAccountID,
            (object) baccount.DefLocationID
          }));
          if (location2 != null)
            defaultTaxZone = location2.VTaxZoneID;
        }
      }
    }
    return defaultTaxZone;
  }

  protected virtual void ARCashSale_BranchID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<ARTaxTran> pxResult in ((PXSelectBase<ARTaxTran>) this.Taxes).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Taxes).Cache, (object) PXResult<ARTaxTran>.op_Implicit(pxResult));
  }

  protected virtual void ARShippingAddress_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARShippingAddress row = e.Row as ARShippingAddress;
    ARShippingAddress oldRow = e.OldRow as ARShippingAddress;
    if (row == null || this.IsTaxZoneDerivedFromCustomer() || ((PXSelectBase<ARCashSale>) this.Document).Current.Released.GetValueOrDefault() || (string.IsNullOrEmpty(row.PostalCode) || !(oldRow.PostalCode != row.PostalCode)) && (string.IsNullOrEmpty(row.CountryID) || !(oldRow.CountryID != row.CountryID)) && (string.IsNullOrEmpty(row.State) || !(oldRow.State != row.State)))
      return;
    string taxZoneByAddress = TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, (IAddressBase) row);
    if (taxZoneByAddress == null || ((PXSelectBase<ARCashSale>) this.Document).Current == null || !(((PXSelectBase<ARCashSale>) this.Document).Current.TaxZoneID != taxZoneByAddress))
      return;
    ARCashSale copy = PXCache<ARCashSale>.CreateCopy(((PXSelectBase<ARCashSale>) this.Document).Current);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARCashSale.taxZoneID>((object) ((PXSelectBase<ARCashSale>) this.Document).Current, (object) taxZoneByAddress);
    ((PXSelectBase) this.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<ARCashSale>) this.Document).Current, (object) copy);
  }

  private bool IsTaxZoneDerivedFromCustomer()
  {
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
    return location != null && !string.IsNullOrEmpty(location.CTaxZoneID);
  }

  protected virtual void ARCashSale_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.location.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<ARCashSale.aRAccountID>(e.Row);
    sender.SetDefaultExt<ARCashSale.aRSubID>(e.Row);
    sender.SetDefaultExt<ARCashSale.taxCalcMode>(e.Row);
    sender.SetDefaultExt<ARCashSale.salesPersonID>(e.Row);
    sender.SetDefaultExt<ARCashSale.workgroupID>(e.Row);
    sender.SetDefaultExt<ARCashSale.ownerID>(e.Row);
    sender.SetDefaultExt<ARCashSale.externalTaxExemptionNumber>(e.Row);
    sender.SetDefaultExt<ARCashSale.avalaraCustomerUsageType>(e.Row);
    if (ProjectAttribute.IsPMVisible("AR"))
      sender.SetDefaultExt<ARCashSale.projectID>(e.Row);
    SharedRecordAttribute.DefaultRecord<ARCashSale.shipAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<ARCashSale.shipContactID>(sender, e.Row);
  }

  protected virtual void ARCashSale_ExtRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    if (e.Row != null && ((ARRegister) e.Row).DocType == "RPM")
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (row == null || string.IsNullOrEmpty((string) e.NewValue) || !string.IsNullOrEmpty(row.PaymentMethodID))
        return;
      PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current;
      ARCashSale arCashSale;
      if (current != null && current.IsAccountNumberRequired.GetValueOrDefault())
        arCashSale = PXResultset<ARCashSale>.op_Implicit(PXSelectBase<ARCashSale, PXSelectReadonly<ARCashSale, Where<ARCashSale.customerID, Equal<Current<ARCashSale.customerID>>, And<ARCashSale.pMInstanceID, Equal<Current<ARCashSale.pMInstanceID>>, And<ARCashSale.extRefNbr, Equal<Required<ARCashSale.extRefNbr>>, And<ARCashSale.voided, Equal<False>, And<Where<ARCashSale.docType, NotEqual<Current<ARCashSale.docType>>, Or<ARCashSale.refNbr, NotEqual<Current<ARCashSale.refNbr>>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          e.NewValue
        }));
      else
        arCashSale = PXResultset<ARCashSale>.op_Implicit(PXSelectBase<ARCashSale, PXSelectReadonly<ARCashSale, Where<ARCashSale.customerID, Equal<Current<ARCashSale.customerID>>, And<ARCashSale.paymentMethodID, Equal<Current<ARCashSale.paymentMethodID>>, And<ARCashSale.extRefNbr, Equal<Required<ARCashSale.extRefNbr>>, And<ARCashSale.voided, Equal<False>, And<Where<ARCashSale.docType, NotEqual<Current<ARCashSale.docType>>, Or<ARCashSale.refNbr, NotEqual<Current<ARCashSale.refNbr>>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          e.NewValue
        }));
      if (arCashSale == null)
        return;
      sender.RaiseExceptionHandling<ARCashSale.extRefNbr>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("Payment with Payment Ref. '{0}' dated '{1}' already exists for this Customer and have the same Payment Method. It's Reference Number - {2} {3}.", (PXErrorLevel) 2, new object[4]
      {
        (object) arCashSale.ExtRefNbr,
        (object) arCashSale.DocDate,
        (object) arCashSale.DocType,
        (object) arCashSale.RefNbr
      }));
    }
  }

  private object GetAcctSub<Field>(PXCache cache, object data) where Field : IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }

  protected virtual void ARCashSale_ARAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null || e.Row == null)
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aRAccountID>(((PXSelectBase) this.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current);
  }

  protected virtual void ARCashSale_ARSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null || e.Row == null)
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aRSubID>(((PXSelectBase) this.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current);
  }

  protected virtual void ARCashSale_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARCashSale.pMInstanceID>(e.Row);
    sender.SetDefaultExt<ARCashSale.cashAccountID>(e.Row);
    sender.SetDefaultExt<ARCashSale.dontEmail>(e.Row);
  }

  protected virtual void ARCashSale_PMInstanceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetValueExt<ARPayment.refTranExtNbr>(e.Row, (object) null);
    sender.SetDefaultExt<ARCashSale.cashAccountID>(e.Row);
    sender.SetDefaultExt<ARCashSale.processingCenterID>(e.Row);
  }

  protected virtual void ARCashSale_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    if (((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current != null)
    {
      int? cashAccountId1 = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current.CashAccountID;
      int? cashAccountId2 = row.CashAccountID;
      if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
        goto label_3;
    }
    ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<ARCashSale.cashAccountID>(sender, e.Row);
label_3:
    sender.SetDefaultExt<ARCashSale.depositAsBatch>(e.Row);
    sender.SetDefaultExt<ARCashSale.depositAfter>(e.Row);
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

  protected virtual void ARCashSale_Cleared_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    if (row.Cleared.GetValueOrDefault())
    {
      if (row.ClearDate.HasValue)
        return;
      row.ClearDate = row.DocDate;
    }
    else
      row.ClearDate = new DateTime?();
  }

  protected void _(
    PX.Data.Events.FieldUpdated<ARCashSale, ARCashSale.adjDate> e)
  {
    bool? released = e.Row.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue) || !(e.Row.DocType != "RCS"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale, ARCashSale.adjDate>>) e).Cache.SetDefaultExt<ARCashSale.depositAfter>((object) e.Row);
  }

  protected virtual void ARCashSale_DepositAfter_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    if (!(row.DocType == "PMT") && !(row.DocType == "CSL") && !(row.DocType == "REF") && !(row.DocType == "RCS") || !row.DepositAsBatch.GetValueOrDefault())
      return;
    e.NewValue = (object) row.AdjDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARCashSale_DepositAsBatch_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    if (!(row.DocType == "PMT") && !(row.DocType == "CSL") && !(row.DocType == "REF") && !(row.DocType == "RCS"))
      return;
    sender.SetDefaultExt<ARPayment.depositAfter>(e.Row);
  }

  protected virtual void ARCashSale_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    if (!row.CashAccountID.HasValue)
    {
      if (sender.RaiseExceptionHandling<ARCashSale.cashAccountID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[cashAccountID]"
      })))
        throw new PXRowPersistingException(typeof (ARCashSale.cashAccountID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "cashAccountID"
        });
    }
    if (string.IsNullOrEmpty(row.PaymentMethodID))
    {
      if (sender.RaiseExceptionHandling<ARCashSale.paymentMethodID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[paymentMethodID]"
      })))
        throw new PXRowPersistingException(typeof (ARCashSale.paymentMethodID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "paymentMethodID"
        });
    }
    this.ValidateTaxConfiguration(sender, row);
    PX.Objects.CA.PaymentMethod current = ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.paymentmethod).Current;
    PXCache pxCache1 = sender;
    ARCashSale arCashSale1 = row;
    bool? nullable;
    int num1;
    if (current != null)
    {
      nullable = current.IsAccountNumberRequired;
      if (nullable.GetValueOrDefault())
      {
        num1 = 1;
        goto label_10;
      }
    }
    num1 = 2;
label_10:
    PXDefaultAttribute.SetPersistingCheck<ARPayment.pMInstanceID>(pxCache1, (object) arCashSale1, (PXPersistingCheck) num1);
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<ARCashSale.termsID>(((PXSelectBase) this.Document).Cache, (object) row);
    if (terms == null)
    {
      sender.SetValue<ARCashSale.termsID>((object) row, (object) null);
    }
    else
    {
      if (terms.InstallmentType == "M")
        sender.RaiseExceptionHandling<ARCashSale.termsID>((object) row, (object) row.TermsID, (Exception) new PXSetPropertyException("Multiple Installments are not allowed for Cash Sale.", new object[1]
        {
          (object) "[termsID]"
        }));
      PXCache pxCache2 = sender;
      ARCashSale arCashSale2 = row;
      int num2;
      if (!(row.DocType == "CSL") && !(row.DocType == "RCS"))
      {
        nullable = ((PXSelectBase<ARSetup>) this.arsetup).Current.RequireExtRef;
        bool flag = false;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        {
          num2 = 0;
          goto label_18;
        }
      }
      num2 = 2;
label_18:
      PXDefaultAttribute.SetPersistingCheck<ARCashSale.extRefNbr>(pxCache2, (object) arCashSale2, (PXPersistingCheck) num2);
      PaymentRefAttribute.SetUpdateCashManager<ARCashSale.extRefNbr>(sender, e.Row, ((ARRegister) e.Row).DocType != "RPM");
    }
  }

  private void ValidateTaxConfiguration(PXCache cache, ARCashSale cashSale)
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult<ARTax, PX.Objects.TX.Tax> pxResult in PXSelectBase<ARTax, PXSelectJoin<ARTax, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTax.taxID>>>, Where<ARTax.tranType, Equal<Current<ARCashSale.docType>>, And<ARTax.refNbr, Equal<Current<ARCashSale.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PX.Objects.TX.Tax tax = PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      if (tax.TaxApplyTermsDisc == "P")
        flag1 = true;
      if (tax.TaxApplyTermsDisc == "X")
        flag2 = true;
      if (flag1 & flag2)
        cache.RaiseExceptionHandling<ARCashSale.taxZoneID>((object) cashSale, (object) cashSale.TaxZoneID, (Exception) new PXSetPropertyException("Tax configuration is invalid. A document cannot contain both Reduce Taxable Amount and Reduce Taxable Amount On Early Payment taxes."));
    }
  }

  /// <summary>
  /// Determines whether the approval is required for the document.
  /// </summary>
  /// <param name="doc">The document for which the check should be performed.</param>
  /// <returns>Returns <c>true</c> if approval is required; otherwise, returns <c>false</c>.</returns>
  private bool IsApprovalRequired(ARCashSale doc)
  {
    return EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.ApprovableDocTypes.Contains(doc.DocType);
  }

  protected virtual void ARCashSale_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ARCashSale row = e.Row as ARCashSale;
    ((PXAction) this.release).SetEnabled(true);
    if (row == null || this.InternalCall)
      return;
    ((PXAction) this.release).SetEnabled(true);
    ((PXAction) this.reclassifyBatch).SetEnabled(true);
    bool flag1 = !this.IsApprovalRequired(row);
    bool? dontApprove = row.DontApprove;
    bool flag2 = flag1;
    if (!(dontApprove.GetValueOrDefault() == flag2 & dontApprove.HasValue))
      cache.SetValueExt<ARRegister.dontApprove>((object) row, (object) flag1);
    ((PXSelectBase) this.PaymentCharges).Cache.AllowSelect = true;
    bool flag3 = row.DocType == "PMT" || row.DocType == "CSL" || row.DocType == "RCS";
    PXCache pxCache1 = cache;
    ARCashSale arCashSale1 = row;
    bool? nullable1;
    int num1;
    if (flag3)
    {
      nullable1 = row.DepositAsBatch;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetVisible<ARCashSale.depositAfter>(pxCache1, (object) arCashSale1, num1 != 0);
    PXUIFieldAttribute.SetVisible<ARTran.defScheduleID>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisibility<ARTran.defScheduleID>(((PXSelectBase) this.Transactions).Cache, (object) null, (PXUIVisibility) 1);
    nullable1 = row.Hold;
    int num2;
    if (nullable1.GetValueOrDefault())
    {
      PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current;
      if (current == null)
      {
        num2 = 0;
      }
      else
      {
        nullable1 = current.Reconcile;
        num2 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num2 = 0;
    bool flag4 = num2 != 0;
    nullable1 = row.Released;
    int num3;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Voided;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 1;
    bool flag5 = num3 != 0;
    ((PXSelectBase) this.Shipping_Address).Cache.AllowUpdate = ((PXSelectBase) this.Shipping_Contact).Cache.AllowUpdate = !flag5;
    int? nullable2;
    if (flag5)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      cache.AllowDelete = false;
      ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
      ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
      ((PXAction) this.release).SetEnabled(false);
      PXAction<ARCashSale> voidCheck = this.voidCheck;
      nullable1 = row.Voided;
      bool flag6 = false;
      int num4 = nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue ? 1 : 0;
      ((PXAction) voidCheck).SetEnabled(num4 != 0);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<ARCashSale.status>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<ARCashSale.curyID>(cache, (object) row, false);
      cache.AllowUpdate = true;
      ((PXSelectBase) this.Transactions).Cache.AllowDelete = true;
      ((PXSelectBase) this.Transactions).Cache.AllowUpdate = true;
      PXCache cache1 = ((PXSelectBase) this.Transactions).Cache;
      int num5;
      if (row.CustomerID.HasValue)
      {
        nullable2 = row.CustomerLocationID;
        num5 = nullable2.HasValue ? 1 : 0;
      }
      else
        num5 = 0;
      cache1.AllowInsert = num5 != 0;
      PXAction<ARCashSale> release = this.release;
      nullable1 = row.Hold;
      bool flag7 = false;
      int num6 = nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue ? 1 : 0;
      ((PXAction) release).SetEnabled(num6 != 0);
      ((PXAction) this.voidCheck).SetEnabled(false);
    }
    PXUIFieldAttribute.SetEnabled<ARCashSale.docType>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<ARCashSale.refNbr>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<ARCashSale.batchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyLineTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyTaxTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyDocBal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyCommnblAmt>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyCommnAmt>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyVatExemptTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.curyVatTaxableTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.cleared>(cache, (object) row, flag4);
    PXCache pxCache2 = cache;
    ARCashSale arCashSale2 = row;
    int num7;
    if (flag4)
    {
      nullable1 = row.Cleared;
      num7 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num7 = 0;
    PXUIFieldAttribute.SetEnabled<ARCashSale.clearDate>(pxCache2, (object) arCashSale2, num7 != 0);
    PXUIFieldAttribute.SetEnabled<ARCashSale.depositAsBatch>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.deposited>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.depositType>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.depositNbr>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.depositDate>(cache, (object) null, false);
    PXCache pxCache3 = cache;
    ARCashSale arCashSale3 = row;
    int num8;
    if (flag3)
    {
      nullable1 = row.DepositAsBatch;
      num8 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num8 = 0;
    PXUIFieldAttribute.SetEnabled<ARCashSale.depositAfter>(pxCache3, (object) arCashSale3, num8 != 0);
    PXCache pxCache4 = cache;
    int num9;
    if (flag3)
    {
      nullable1 = row.DepositAsBatch;
      num9 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num9 = 0;
    PXUIFieldAttribute.SetRequired<ARCashSale.depositAfter>(pxCache4, num9 != 0);
    int num10;
    if (flag3)
    {
      nullable1 = row.DepositAsBatch;
      if (nullable1.GetValueOrDefault())
      {
        num10 = 1;
        goto label_33;
      }
    }
    num10 = 2;
label_33:
    PXPersistingCheck pxPersistingCheck = (PXPersistingCheck) num10;
    PXDefaultAttribute.SetPersistingCheck<ARCashSale.depositAfter>(cache, (object) row, pxPersistingCheck);
    nullable2 = row.CustomerID;
    if (nullable2.HasValue && ((PXSelectBase<ARTran>) this.Transactions).Any<ARTran>())
      PXUIFieldAttribute.SetEnabled<ARCashSale.customerID>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARCashSale.cCPaymentStateDescr>(cache, (object) null, false);
    bool flag8 = !string.IsNullOrEmpty(row.DepositNbr) && !string.IsNullOrEmpty(row.DepositType);
    PX.Objects.CA.CashAccount current1 = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.cashaccount).Current;
    int num11;
    if (current1 != null)
    {
      nullable2 = current1.CashAccountID;
      int? cashAccountId = row.CashAccountID;
      if (nullable2.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable2.HasValue == cashAccountId.HasValue)
      {
        nullable1 = current1.ClearingAccount;
        num11 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_39;
      }
    }
    num11 = 0;
label_39:
    bool flag9 = num11 != 0;
    int num12;
    if (!flag8 && current1 != null)
    {
      if (!flag9)
      {
        nullable1 = row.DepositAsBatch;
        bool flag10 = flag9;
        num12 = !(nullable1.GetValueOrDefault() == flag10 & nullable1.HasValue) ? 1 : 0;
      }
      else
        num12 = 1;
    }
    else
      num12 = 0;
    bool flag11 = num12 != 0;
    if (flag11)
    {
      cache.AllowUpdate = true;
      nullable1 = row.DepositAsBatch;
      bool flag12 = flag9;
      PXSetPropertyException propertyException = !(nullable1.GetValueOrDefault() == flag12 & nullable1.HasValue) ? new PXSetPropertyException("'Batch deposit' setting does not match 'Clearing Account' flag of the Cash Account", (PXErrorLevel) 2) : (PXSetPropertyException) null;
      cache.RaiseExceptionHandling<ARCashSale.depositAsBatch>((object) row, (object) row.DepositAsBatch, (Exception) propertyException);
    }
    PXUIFieldAttribute.SetEnabled<ARCashSale.depositAsBatch>(cache, (object) row, flag11);
    PXCache pxCache5 = cache;
    ARCashSale arCashSale4 = row;
    int num13;
    if (!flag8 & flag9)
    {
      nullable1 = row.DepositAsBatch;
      num13 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num13 = 0;
    PXUIFieldAttribute.SetEnabled<ARCashSale.depositAfter>(pxCache5, (object) arCashSale4, num13 != 0);
    this.CheckCashAccount(cache, row);
    PXAction<ARCashSale> validateAddresses = this.validateAddresses;
    nullable1 = row.Released;
    bool flag13 = false;
    int num14 = !(nullable1.GetValueOrDefault() == flag13 & nullable1.HasValue) ? 0 : (((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation() ? 1 : 0);
    ((PXAction) validateAddresses).SetEnabled(num14 != 0);
    nullable1 = row.Released;
    bool flag14 = !nullable1.GetValueOrDefault() && (row.DocType == "CSL" || row.DocType == "RCS");
    ((PXSelectBase) this.PaymentCharges).Cache.AllowInsert = flag14;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowUpdate = flag14;
    ((PXSelectBase) this.PaymentCharges).Cache.AllowDelete = flag14;
    ((PXSelectBase) this.Taxes).Cache.AllowInsert = ((PXSelectBase) this.Transactions).Cache.AllowInsert;
    ((PXSelectBase) this.Taxes).Cache.AllowUpdate = ((PXSelectBase) this.Transactions).Cache.AllowUpdate;
    ((PXSelectBase) this.Taxes).Cache.AllowDelete = ((PXSelectBase) this.Transactions).Cache.AllowDelete;
    nullable1 = row.IsMigratedRecord;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    int num15;
    if (valueOrDefault)
    {
      nullable1 = row.Released;
      num15 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num15 = 0;
    bool flag15 = num15 != 0;
    if (flag15)
      ((PXSelectBase) this.PaymentCharges).Cache.AllowSelect = false;
    ARSetup current2 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    int num16;
    if (current2 == null)
    {
      num16 = 0;
    }
    else
    {
      nullable1 = current2.MigrationMode;
      num16 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if ((num16 != 0 ? (!valueOrDefault ? 1 : 0) : (flag15 ? 1 : 0)) != 0)
    {
      bool allowInsert = ((PXSelectBase) this.Document).Cache.AllowInsert;
      bool allowDelete = ((PXSelectBase) this.Document).Cache.AllowDelete;
      ((PXGraph) this).DisableCaches();
      ((PXSelectBase) this.Document).Cache.AllowInsert = allowInsert;
      ((PXSelectBase) this.Document).Cache.AllowDelete = allowDelete;
    }
    if (this.IsApprovalRequired(row))
    {
      if (row.Status == "D" || row.Status == "J")
        ((PXAction) this.release).SetEnabled(false);
      if (row.DocType == "RCS")
      {
        if (row.Status == "D" || row.Status == "J" || row.Status == "C" || row.Status == "B")
        {
          nullable1 = row.DontApprove;
          bool flag16 = false;
          if (nullable1.GetValueOrDefault() == flag16 & nullable1.HasValue)
            PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
        }
        if (row.Status == "D" || row.Status == "J" || row.Status == "B")
          PXUIFieldAttribute.SetEnabled<ARPayment.hold>(cache, (object) row, true);
      }
      if (!(row.Status == "D") && !(row.Status == "J"))
      {
        if (row.Status == "B")
        {
          nullable1 = row.DontApprove;
          bool flag17 = false;
          if (nullable1.GetValueOrDefault() == flag17 & nullable1.HasValue)
            goto label_72;
        }
        if (!(row.Status == "C"))
          goto label_73;
      }
label_72:
      ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
      ((PXSelectBase) this.Taxes).Cache.AllowInsert = false;
      ((PXSelectBase) this.Approval).Cache.AllowInsert = false;
      ((PXSelectBase) this.salesPerTrans).Cache.AllowInsert = false;
      ((PXSelectBase) this.PaymentCharges).Cache.AllowInsert = false;
      ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Taxes).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Approval).Cache.AllowUpdate = false;
      ((PXSelectBase) this.salesPerTrans).Cache.AllowUpdate = false;
      ((PXSelectBase) this.PaymentCharges).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
      ((PXSelectBase) this.Taxes).Cache.AllowDelete = false;
      ((PXSelectBase) this.Approval).Cache.AllowDelete = false;
      ((PXSelectBase) this.salesPerTrans).Cache.AllowDelete = false;
      ((PXSelectBase) this.PaymentCharges).Cache.AllowDelete = false;
    }
label_73:
    PXUIFieldAttribute.SetEnabled<ARCashSale.docType>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<ARCashSale.refNbr>(cache, (object) row, true);
    UIState.RaiseOrHideErrorByErrorLevelPriority<ARPayment.status>(cache, (object) row, row.Status == "W", "The document can be processed as a document with the Balanced status. Card processing actions are no longer available because the Integrated Card Processing feature has been disabled.", (PXErrorLevel) 2);
    ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
    nullable1 = row.IsCCPayment;
    ((PXSelectBase) this.ccProcTran).Cache.AllowSelect = nullable1.GetValueOrDefault() && (PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() || !PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() && transactionState != null && transactionState.IsActive);
    ((PXSelectBase) this.ccProcTran).AllowUpdate = false;
    ((PXSelectBase) this.ccProcTran).AllowDelete = false;
    ((PXSelectBase) this.ccProcTran).AllowInsert = false;
    this.EnableVoidIfPossible(row, cache, false);
  }

  public virtual void EnableVoidIfPossible(ARCashSale doc, PXCache cache, bool docStatusIsCCHold)
  {
    bool? nullable;
    int num;
    if (doc.Released.GetValueOrDefault() && doc.DocType == "CSL")
    {
      nullable = doc.Voided;
      bool flag = false;
      num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag1 = num != 0;
    nullable = doc.Released;
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && !flag1 && doc.DocType == "CSL")
    {
      nullable = doc.Voided;
      bool flag3 = false;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
      {
        bool flag4 = ExternalTranHelper.HasTransactions((PXSelectBase<ExternalTransaction>) this.ExternalTran);
        if (flag4)
        {
          ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
          if (((transactionState.IsCaptured ? 0 : (!transactionState.IsPreAuthorized ? 1 : 0)) | (docStatusIsCCHold ? 1 : 0)) != 0)
            flag1 = true;
        }
        cache.AllowDelete = !flag4;
      }
    }
    ((PXAction) this.voidCheck).SetEnabled(flag1);
  }

  private void CheckCashAccount(PXCache cache, ARCashSale doc)
  {
    CCProcessingCenter processingCenter = ((PXSelectBase<CCProcessingCenter>) this.ProcessingCenter).SelectSingle(Array.Empty<object>());
    if ((processingCenter != null ? (!processingCenter.ImportSettlementBatches.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    if (!((IQueryable<PXResult<CashAccountDeposit>>) ((PXSelectBase<CashAccountDeposit>) new PXSelect<CashAccountDeposit, Where<CashAccountDeposit.cashAccountID, Equal<Required<CCProcessingCenter.depositAccountID>>, And<CashAccountDeposit.depositAcctID, Equal<Required<ARCashSale.cashAccountID>>, And<Where<CashAccountDeposit.paymentMethodID, Equal<Required<ARCashSale.paymentMethodID>>, Or<CashAccountDeposit.paymentMethodID, Equal<EmptyString>>>>>>>((PXGraph) this)).Select(new object[3]
    {
      (object) processingCenter.DepositAccountID,
      (object) doc.CashAccountID,
      (object) doc.PaymentMethodID
    })).Any<PXResult<CashAccountDeposit>>())
    {
      PX.Objects.CA.CashAccount cashAccount = PX.Objects.CA.CashAccount.PK.Find((PXGraph) this, processingCenter.DepositAccountID);
      cache.RaiseExceptionHandling<ARCashSale.cashAccountID>((object) doc, (object) doc.CashAccountID, (Exception) new PXSetPropertyException("This cash account is not a clearing account for the {0} cash account, this payment will not be included in a bank deposit on import of the settlement batch.", (PXErrorLevel) 2, new object[1]
      {
        (object) cashAccount.CashAccountCD
      }));
    }
    else
      cache.RaiseExceptionHandling<ARCashSale.cashAccountID>((object) doc, (object) doc.CashAccountID, (Exception) null);
  }

  protected virtual void ARCashSale_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is ARCashSale))
      return;
    using (new PXConnectionScope())
      PXFormulaAttribute.CalcAggregate<ARPaymentChargeTran.curyTranAmt>(((PXSelectBase) this.PaymentCharges).Cache, e.Row, true);
  }

  protected virtual void ARCashSale_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    bool? released = ((ARRegister) e.Row).Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    e.NewValue = (object) ((ARCashSale) e.Row).AdjDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARCashSale_DocDesc_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      bool? released = row.Released;
      bool flag = false;
      num = !(released.GetValueOrDefault() == flag & released.HasValue) ? 1 : 0;
    }
    if (num != 0)
      return;
    foreach (PXResult<ARTaxTran> pxResult in ((PXSelectBase<ARTaxTran>) this.Taxes).Select(Array.Empty<object>()))
    {
      ARTaxTran arTaxTran = PXResult<ARTaxTran>.op_Implicit(pxResult);
      arTaxTran.Description = row.DocDesc;
      ((PXSelectBase) this.Taxes).Cache.Update((object) arTaxTran);
    }
  }

  protected virtual void ARCashSale_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    bool? released = ((ARRegister) e.Row).Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    e.NewValue = (object) ((ARCashSale) e.Row).AdjFinPeriodID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARCashSale_TranPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    bool? released = ((ARRegister) e.Row).Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    e.NewValue = (object) ((ARCashSale) e.Row).AdjTranPeriodID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARCashSale_ProjectID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARCashSale))
      return;
    foreach (PXResult<ARTran> pxResult in ((PXSelectBase<ARTran>) this.Transactions).Select(Array.Empty<object>()))
    {
      ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
      ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<ARTran.projectID>((object) arTran);
      ((PXSelectBase<ARTran>) this.Transactions).Update(arTran);
    }
  }

  protected virtual void ARCashSale_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    ARRegister row = (ARRegister) e.Row;
    Decimal? origDocAmt = ((ARRegister) e.Row).OrigDocAmt;
    Decimal? BalanceAmt = origDocAmt.HasValue ? new Decimal?(-origDocAmt.GetValueOrDefault()) : new Decimal?();
    ARReleaseProcess.UpdateARBalances((PXGraph) this, row, BalanceAmt);
  }

  protected virtual void ARCashSale_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ARCashSale row = (ARCashSale) e.Row;
    bool? released = row.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue)
    {
      row.DocDate = row.AdjDate;
      row.FinPeriodID = row.AdjFinPeriodID;
      row.TranPeriodID = row.AdjTranPeriodID;
      sender.RaiseExceptionHandling<ARCashSale.finPeriodID>(e.Row, (object) row.FinPeriodID, (Exception) null);
    }
    ARReleaseProcess.UpdateARBalances((PXGraph) this, (ARRegister) e.Row, ((ARRegister) e.Row).OrigDocAmt);
  }

  protected virtual void ARCashSale_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARCashSale row = e.Row as ARCashSale;
    Decimal? nullable1;
    Decimal? nullable2;
    if (!row.Released.GetValueOrDefault())
    {
      row.DocDate = row.AdjDate;
      row.FinPeriodID = row.AdjFinPeriodID;
      row.TranPeriodID = row.AdjTranPeriodID;
      if (!((PXGraph) this).IsCopyPasteContext)
      {
        sender.RaiseExceptionHandling<ARCashSale.finPeriodID>((object) row, (object) row.FinPeriodID, (Exception) null);
        if (!sender.ObjectsEqual<ARCashSale.curyDocBal, ARCashSale.curyOrigDiscAmt>(e.Row, e.OldRow))
        {
          Decimal? curyDocBal = row.CuryDocBal;
          Decimal? curyOrigDiscAmt = row.CuryOrigDiscAmt;
          nullable1 = curyDocBal.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
          nullable2 = row.CuryOrigDocAmt;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            nullable2 = row.CuryDocBal;
            if (nullable2.HasValue)
            {
              nullable2 = row.CuryOrigDiscAmt;
              if (nullable2.HasValue)
              {
                nullable2 = row.CuryDocBal;
                Decimal num = 0M;
                if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
                {
                  PXCache pxCache = sender;
                  ARCashSale arCashSale = row;
                  nullable2 = row.CuryDocBal;
                  nullable1 = row.CuryOrigDiscAmt;
                  // ISSUE: variable of a boxed type
                  __Boxed<Decimal?> local = (ValueType) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?());
                  pxCache.SetValueExt<ARCashSale.curyOrigDocAmt>((object) arCashSale, (object) local);
                  goto label_15;
                }
              }
            }
            sender.SetValueExt<ARCashSale.curyOrigDocAmt>((object) row, (object) 0M);
            goto label_15;
          }
        }
        if (!sender.ObjectsEqual<ARCashSale.curyOrigDocAmt>(e.Row, e.OldRow))
        {
          nullable1 = row.CuryDocBal;
          if (nullable1.HasValue)
          {
            nullable1 = row.CuryOrigDocAmt;
            if (nullable1.HasValue)
            {
              nullable1 = row.CuryDocBal;
              Decimal num = 0M;
              if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
              {
                PXCache pxCache = sender;
                ARCashSale arCashSale = row;
                nullable1 = row.CuryDocBal;
                nullable2 = row.CuryOrigDocAmt;
                // ISSUE: variable of a boxed type
                __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?());
                pxCache.SetValueExt<ARCashSale.curyOrigDiscAmt>((object) arCashSale, (object) local);
                goto label_15;
              }
            }
          }
          sender.SetValueExt<ARCashSale.curyOrigDiscAmt>((object) row, (object) 0M);
        }
label_15:
        if (!row.Hold.GetValueOrDefault())
        {
          nullable2 = row.CuryDocBal;
          nullable1 = row.CuryOrigDocAmt;
          if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
          {
            sender.RaiseExceptionHandling<ARCashSale.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
          }
          else
          {
            nullable1 = row.CuryOrigDocAmt;
            Decimal num = 0M;
            if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
              sender.RaiseExceptionHandling<ARCashSale.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
            else
              sender.RaiseExceptionHandling<ARCashSale.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) null);
          }
        }
        this.PaymentCharges.UpdateChangesFromPayment(sender, e);
      }
    }
    if (e.OldRow != null)
    {
      ARRegister oldRow = (ARRegister) e.OldRow;
      nullable1 = ((ARRegister) e.OldRow).OrigDocAmt;
      Decimal? BalanceAmt;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        BalanceAmt = nullable2;
      }
      else
        BalanceAmt = new Decimal?(-nullable1.GetValueOrDefault());
      ARReleaseProcess.UpdateARBalances((PXGraph) this, oldRow, BalanceAmt);
    }
    ARReleaseProcess.UpdateARBalances((PXGraph) this, (ARRegister) e.Row, ((ARRegister) e.Row).OrigDocAmt);
  }

  protected virtual void ParentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<ARCashSale.branchID>(e.Row, e.OldRow))
      return;
    foreach (PXResult<ARSalesPerTran> pxResult in ((PXSelectBase<ARSalesPerTran>) this.salesPerTrans).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.salesPerTrans).Cache, (object) PXResult<ARSalesPerTran>.op_Implicit(pxResult));
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

  protected virtual void ARTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null || ((PXSelectBase<ARCashSale>) this.Document).Current == null)
      return;
    Customer customer1 = ((PXSelectBase<Customer>) this.customer).Current;
    if (customer1 == null)
      customer1 = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARCashSale>) this.Document).Current.CustomerID
      }));
    Customer customer2 = customer1;
    if (row.InventoryID.HasValue && (!customer2.IsBranch.GetValueOrDefault() || !(((PXSelectBase<ARSetup>) this.arsetup).Current.IntercompanySalesAccountDefault == "L")) || ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null)
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.CSalesAcctID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null || !row.AccountID.HasValue || ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.InventoryID
    }));
    PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Required<PX.Objects.EP.EPEmployee.userID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) PXAccess.GetUserID()
    }));
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<ARTran.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BranchID
    }));
    SalesPerson salesPerson = PXResultset<SalesPerson>.op_Implicit(PXSelectBase<SalesPerson, PXSelect<SalesPerson, Where<SalesPerson.salesPersonID, Equal<Current<ARTran.salesPersonID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    object obj = (object) SubAccountMaskAttribute.MakeSub<ARSetup.salesSubMask>((PXGraph) this, ((PXSelectBase<ARSetup>) this.arsetup).Current.SalesSubMask, new object[5]
    {
      (object) (int?) ((PXGraph) this).Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cSalesSubID>((object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current),
      (object) (int?) ((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.salesSubID>((object) inventoryItem),
      (object) (int?) ((PXGraph) this).Caches[typeof (PX.Objects.EP.EPEmployee)].GetValue<PX.Objects.EP.EPEmployee.salesSubID>((object) epEmployee),
      (object) (int?) ((PXGraph) this).Caches[typeof (PX.Objects.CR.Standalone.Location)].GetValue<PX.Objects.CR.Standalone.Location.cMPSalesSubID>((object) location),
      (object) (int?) ((PXGraph) this).Caches[typeof (SalesPerson)].GetValue<SalesPerson.salesSubID>((object) salesPerson)
    }, new System.Type[5]
    {
      typeof (PX.Objects.CR.Location.cSalesSubID),
      typeof (PX.Objects.IN.InventoryItem.salesSubID),
      typeof (PX.Objects.EP.EPEmployee.salesSubID),
      typeof (PX.Objects.CR.Location.cMPSalesSubID),
      typeof (SalesPerson.salesSubID)
    });
    sender.RaiseFieldUpdating<ARTran.subID>(e.Row, ref obj);
    e.NewValue = (object) (int?) obj;
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [ARCashSaleTax(typeof (ARCashSale), typeof (ARTax), typeof (ARTaxTran), typeof (ARCashSale.branchID), Inventory = typeof (ARTran.inventoryID), UOM = typeof (ARTran.uOM), LineQty = typeof (ARTran.qty))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARTran.inventoryID>>>>))]
  protected virtual void ARTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [DRTerms.Dates(typeof (ARTran.dRTermStartDate), typeof (ARTran.dRTermEndDate), typeof (ARTran.inventoryID), typeof (ARTran.deferredCode), typeof (ARCashSale.hold))]
  protected virtual void ARTran_RequiresTerms_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(sender, e.Row) != TaxCalc.Calc || ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.DfltTaxCategoryID) || ((ARTran) e.Row).InventoryID.HasValue)
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.DfltTaxCategoryID;
  }

  protected virtual void ARTran_UnitPrice_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((ARTran) e.Row).InventoryID.HasValue)
      return;
    e.NewValue = (object) 0M;
  }

  protected virtual void ARTran_CuryUnitPrice_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Document).Current;
    if (current != null)
    {
      int? nullable1 = row.InventoryID;
      if (nullable1.HasValue && row.UOM != null)
      {
        nullable1 = current.CustomerID;
        if (nullable1.HasValue)
        {
          Decimal? nullable2 = row.Qty;
          if (nullable2.HasValue && !row.ManualPrice.GetValueOrDefault())
          {
            string custPriceClass = "BASE";
            PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
            if (location != null && !string.IsNullOrEmpty(location.CPriceClassID))
              custPriceClass = location.CPriceClassID;
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
            PXFieldDefaultingEventArgs defaultingEventArgs = e;
            nullable2 = ARSalesPriceMaint.CalculateSalesPrice(sender, custPriceClass, current.CustomerID, row.InventoryID, row.SiteID, currencyInfo.GetCM(), row.UOM, row.Qty, current.DocDate.Value, row.CuryUnitPrice, current.TaxCalcMode);
            // ISSUE: variable of a boxed type
            __Boxed<Decimal> valueOrDefault = (ValueType) nullable2.GetValueOrDefault();
            defaultingEventArgs.NewValue = (object) valueOrDefault;
            ARSalesPriceMaint.CheckNewUnitPrice<ARTran, ARTran.curyUnitPrice>(sender, row, e.NewValue);
            return;
          }
        }
      }
    }
    e.NewValue = sender.GetValue<ARTran.curyUnitPrice>(e.Row);
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void ARTran_ManualPrice_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    bool? nullable = row.ManualPrice;
    if (nullable.GetValueOrDefault())
      return;
    nullable = row.IsFree;
    if (nullable.GetValueOrDefault() || sender.Graph.IsCopyPasteContext)
      return;
    sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
  }

  protected virtual void ARTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARTran.unitPrice>(e.Row);
    sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
    sender.SetValue<ARTran.unitPrice>(e.Row, (object) null);
  }

  protected virtual void ARTran_Qty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran))
      return;
    sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
  }

  protected virtual void ARTran_InventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_SOShipmentNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_SalesPersonID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARTran.subID>(e.Row);
  }

  [PopupMessage]
  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  protected virtual void ARTran_InventoryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARTran.accountID>(e.Row);
    sender.SetDefaultExt<ARTran.subID>(e.Row);
    sender.SetDefaultExt<ARTran.taxCategoryID>(e.Row);
    sender.SetDefaultExt<ARTran.deferredCode>(e.Row);
    sender.SetDefaultExt<ARTran.uOM>(e.Row);
    sender.SetDefaultExt<ARTran.unitPrice>(e.Row);
    sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
    ARTran row = e.Row as ARTran;
    if (!(PXSelectorAttribute.Select<PX.Objects.IN.InventoryItem.inventoryID>(sender, (object) row) is PX.Objects.IN.InventoryItem inventoryItem) || row == null)
      return;
    ARTran arTran = row;
    PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)];
    string localeName = ((PXSelectBase<Customer>) this.customer).Current?.LocaleName;
    string translation = PXDBLocalizableStringAttribute.GetTranslation(cach, (object) inventoryItem, "Descr", localeName);
    arTran.TranDesc = translation;
  }

  protected virtual void ARTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    ARTran oldRow = (ARTran) e.OldRow;
    if (row != null)
      TaxBaseAttribute.Calculate<ARTran.taxCategoryID, ARCashSaleTaxAttribute>(sender, e);
    bool? nullable1 = row.ManualDisc;
    if (!nullable1.GetValueOrDefault())
    {
      ARDiscount arDiscount = (ARDiscount) PXSelectorAttribute.Select<PX.Objects.SO.SOLine.discountID>(sender, (object) row);
      ARTran arTran = row;
      Decimal? nullable2;
      if (arDiscount != null)
      {
        nullable1 = arDiscount.IsAppliedToDR;
        if (nullable1.GetValueOrDefault())
        {
          nullable2 = row.DiscPct;
          goto label_7;
        }
      }
      nullable2 = new Decimal?(0.0M);
label_7:
      arTran.DiscPctDR = nullable2;
    }
    if ((e.ExternalCall || sender.Graph.IsImport) && sender.ObjectsEqual<ARTran.inventoryID>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.uOM>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.qty>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.branchID>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.siteID>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.manualPrice>(e.Row, e.OldRow) && (!sender.ObjectsEqual<ARTran.curyUnitPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.curyExtPrice>(e.Row, e.OldRow)))
    {
      nullable1 = row.ManualPrice;
      bool? manualPrice = oldRow.ManualPrice;
      if (nullable1.GetValueOrDefault() == manualPrice.GetValueOrDefault() & nullable1.HasValue == manualPrice.HasValue)
        row.ManualPrice = new bool?(true);
    }
    if (row.ManualPrice.GetValueOrDefault())
      return;
    row.CuryUnitPriceDR = row.CuryUnitPrice;
  }

  protected virtual void ARTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    TaxBaseAttribute.Calculate<ARTran.taxCategoryID, ARCashSaleTaxAttribute>(sender, e);
  }

  protected virtual void ARTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
  }

  protected virtual void ARTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARTran))
      return;
    ((PXAction) this.viewSchedule).SetEnabled(sender.GetStatus(e.Row) != 2);
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Document).Current;
    if (current == null)
      return;
    bool? nullable = current.IsMigratedRecord;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current.Released;
    if (nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<ARTran.defScheduleID>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.deferredCode>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.dRTermStartDate>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.dRTermEndDate>(sender, (object) null, false);
  }

  protected virtual void ARTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null)
      return;
    ScheduleHelper.DeleteAssociatedScheduleIfDeferralCodeChanged((PXGraph) this, (IDocumentLine) (e.Row as ARTran));
  }

  protected virtual void ARTran_DrCr_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARCashSale>) this.Document).Current == null)
      return;
    e.NewValue = (object) ARInvoiceType.DrCr(((PXSelectBase<ARCashSale>) this.Document).Current.DocType);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_DRTermStartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ARTran row) || !row.RequiresTerms.GetValueOrDefault())
      return;
    e.NewValue = (object) ((PXSelectBase<ARCashSale>) this.Document).Current.DocDate;
  }

  protected virtual void ARTaxTran_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARCashSale>) this.Document).Current == null)
      return;
    e.NewValue = (object) ((PXSelectBase<ARCashSale>) this.Document).Current.TaxZoneID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARTaxTran))
      return;
    PXUIFieldAttribute.SetEnabled<ARTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == 2);
  }

  protected virtual void ARTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (((PXSelectBase<ARCashSale>) this.Document).Current == null || e.Operation != 2 && e.Operation != 1)
      return;
    ((TaxTran) e.Row).TaxZoneID = ((PXSelectBase<ARCashSale>) this.Document).Current.TaxZoneID;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<ARTaxTran, ARTaxTran.taxID> e)
  {
    ARTaxTran row = e.Row;
    if (row == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARTaxTran, ARTaxTran.taxID>, ARTaxTran, object>) e).OldValue == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARTaxTran, ARTaxTran.taxID>, ARTaxTran, object>) e).OldValue == e.NewValue)
      return;
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<ARTaxTran.accountID>((object) row);
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<ARTaxTran.taxType>((object) row);
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<ARTaxTran.taxBucketID>((object) row);
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<ARTaxTran.vendorID>((object) row);
    ((PXSelectBase) this.Taxes).Cache.SetDefaultExt<ARTaxTran.subID>((object) row);
  }

  protected virtual void ARSalesPerTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    ARSalesPerTran row = (ARSalesPerTran) e.Row;
    foreach (PXResult<ARSalesPerTran> pxResult in ((PXSelectBase<ARSalesPerTran>) this.salesPerTrans).Select(Array.Empty<object>()))
    {
      ARSalesPerTran arSalesPerTran = PXResult<ARSalesPerTran>.op_Implicit(pxResult);
      int? salespersonId1 = arSalesPerTran.SalespersonID;
      int? salespersonId2 = row.SalespersonID;
      if (salespersonId1.GetValueOrDefault() == salespersonId2.GetValueOrDefault() & salespersonId1.HasValue == salespersonId2.HasValue)
      {
        PXEntryStatus status = ((PXSelectBase) this.salesPerTrans).Cache.GetStatus((object) arSalesPerTran);
        if (status != 4 && status != 3)
        {
          sender.RaiseExceptionHandling<ARSalesPerTran.salespersonID>(e.Row, (object) null, (Exception) new PXException("This Sales Person is already added"));
          ((CancelEventArgs) e).Cancel = true;
          break;
        }
      }
    }
  }

  protected virtual void ARCashSale_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARCashSale_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARCashSale_AdjFinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void VoidCheckProc(ARCashSale doc)
  {
    ((PXGraph) this).Clear((PXClearOption) 1);
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.NoCalc);
    foreach (PXResult<ARCashSale, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<ARCashSale, PXSelectJoin<ARCashSale, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARCashSale.curyInfoID>>>, Where<ARCashSale.docType, Equal<Required<ARCashSale.docType>>, And<ARCashSale.refNbr, Equal<Required<ARCashSale.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(PXResult<ARCashSale, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult));
      copy1.CuryInfoID = new long?();
      copy1.IsReadOnly = new bool?(false);
      PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(copy1));
      PXSelectJoin<ARCashSale, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARCashSale.customerID>>>, Where<ARCashSale.docType, Equal<Optional<ARCashSale.docType>>, And2<Where<ARRegister.origModule, NotEqual<BatchModule.moduleSO>, Or<ARCashSale.released, Equal<boolTrue>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> document = this.Document;
      ARCashSale arCashSale1 = new ARCashSale();
      arCashSale1.DocType = "RCS";
      arCashSale1.RefNbr = (string) null;
      arCashSale1.CuryInfoID = copy2.CuryInfoID;
      arCashSale1.OrigDocType = PXResult<ARCashSale, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult).DocType;
      arCashSale1.OrigRefNbr = PXResult<ARCashSale, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult).RefNbr;
      arCashSale1.OrigDocDate = PXResult<ARCashSale, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult).DocDate;
      arCashSale1.OrigModule = "AR";
      ARCashSale arCashSale2 = ((PXSelectBase<ARCashSale>) document).Insert(arCashSale1);
      if (arCashSale2.RefNbr == null)
      {
        if (PXResultset<ARCashSale>.op_Implicit(PXSelectBase<ARCashSale, PXSelect<ARCashSale>.Config>.Search<ARCashSale.docType, ARCashSale.refNbr>((PXGraph) this, (object) arCashSale2.DocType, (object) arCashSale2.OrigRefNbr, Array.Empty<object>())) != null)
          throw new PXException("The record already exists.");
        arCashSale2.RefNbr = arCashSale2.OrigRefNbr;
        ((PXSelectBase) this.Document).Cache.Normalize();
        ((PXSelectBase<ARCashSale>) this.Document).Update(arCashSale2);
      }
      ARCashSale copy3 = PXCache<ARCashSale>.CreateCopy(PXResult<ARCashSale, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult));
      copy3.OrigModule = "AR";
      ExternalTransactionState transactionState = ExternalTranHelper.GetActiveTransactionState((PXGraph) this, (PXSelectBase<ExternalTransaction>) this.ExternalTran);
      if (transactionState.IsCaptured)
        copy3.RefTranExtNbr = transactionState.ExternalTransaction.TranNumber;
      copy3.CuryInfoID = copy2.CuryInfoID;
      copy3.DocType = ((PXSelectBase<ARCashSale>) this.Document).Current.DocType;
      copy3.RefNbr = ((PXSelectBase<ARCashSale>) this.Document).Current.RefNbr;
      copy3.OrigDocType = ((PXSelectBase<ARCashSale>) this.Document).Current.OrigDocType;
      copy3.OrigRefNbr = ((PXSelectBase<ARCashSale>) this.Document).Current.OrigRefNbr;
      copy3.OrigDocDate = ((PXSelectBase<ARCashSale>) this.Document).Current.OrigDocDate;
      copy3.CATranID = new long?();
      copy3.NoteID = new Guid?();
      copy3.RefNoteID = new Guid?();
      copy3.IsTaxPosted = new bool?(false);
      copy3.IsTaxValid = new bool?(false);
      copy3.OpenDoc = new bool?(true);
      copy3.Released = new bool?(false);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.hold>((object) copy3);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.isMigratedRecord>((object) copy3);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARPayment.status>((object) copy3);
      copy3.Printed = new bool?(false);
      copy3.Emailed = new bool?(false);
      copy3.LineCntr = new int?(0);
      copy3.AdjCntr = new int?(0);
      copy3.BatchNbr = (string) null;
      copy3.AdjDate = doc.DocDate;
      FinPeriodIDAttribute.SetPeriodsByMaster<ARCashSale.adjFinPeriodID>(((PXSelectBase) this.Document).Cache, (object) copy3, doc.AdjTranPeriodID);
      ARCashSale arCashSale3 = copy3;
      Decimal? curyOrigDocAmt = copy3.CuryOrigDocAmt;
      Decimal? curyOrigDiscAmt = copy3.CuryOrigDiscAmt;
      Decimal? nullable = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() + curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
      arCashSale3.CuryDocBal = nullable;
      copy3.CuryChargeAmt = new Decimal?(0M);
      copy3.CuryConsolidateChargeTotal = new Decimal?(0M);
      copy3.ClosedDate = new DateTime?();
      copy3.ClosedFinPeriodID = (string) null;
      copy3.ClosedTranPeriodID = (string) null;
      copy3.Cleared = new bool?(false);
      copy3.ClearDate = new DateTime?();
      copy3.Deposited = new bool?(false);
      copy3.DepositDate = new DateTime?();
      copy3.DepositType = (string) null;
      copy3.DepositNbr = (string) null;
      copy3.CuryVatTaxableTotal = new Decimal?(0M);
      copy3.CuryVatExemptTotal = new Decimal?(0M);
      ((PXSelectBase<ARCashSale>) this.Document).Update(copy3);
      using (new SuppressWorkflowAutoPersistScope((PXGraph) this))
        ((PXAction) this.initializeState).Press();
      if (copy2 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARCashSale.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null));
        currencyInfo.CuryID = copy2.CuryID;
        currencyInfo.CuryEffDate = copy2.CuryEffDate;
        currencyInfo.CuryRateTypeID = copy2.CuryRateTypeID;
        currencyInfo.CuryRate = copy2.CuryRate;
        currencyInfo.RecipRate = copy2.RecipRate;
        currencyInfo.CuryMultDiv = copy2.CuryMultDiv;
        ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo);
      }
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<ARTran.salesPersonID>(ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9__183_0 ?? (ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9__183_0 = new PXFieldDefaulting((object) ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CVoidCheckProc\u003Eb__183_0))));
    foreach (PXResult<ARTran> pxResult in PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      ARTran arTran1 = PXResult<ARTran>.op_Implicit(pxResult);
      ARTran copy = PXCache<ARTran>.CreateCopy(arTran1);
      copy.TranType = (string) null;
      copy.RefNbr = (string) null;
      copy.DrCr = (string) null;
      ARTran arTran2 = copy;
      bool? nullable1 = new bool?();
      bool? nullable2 = nullable1;
      arTran2.Released = nullable2;
      copy.CuryInfoID = new long?();
      copy.NoteID = new Guid?();
      ARTran arTran3 = copy;
      nullable1 = new bool?();
      bool? nullable3 = nullable1;
      arTran3.IsStockItem = nullable3;
      SalesPerson salesPerson = (SalesPerson) PXSelectorAttribute.Select<ARTran.salesPersonID>(((PXSelectBase) this.Transactions).Cache, (object) copy);
      if (salesPerson != null)
      {
        nullable1 = salesPerson.IsActive;
        bool flag = false;
        if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
          goto label_24;
      }
      copy.SalesPersonID = new int?();
label_24:
      ARTran arTran4 = ((PXSelectBase<ARTran>) this.Transactions).Insert(copy);
      PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Transactions).Cache, (object) arTran1, ((PXSelectBase) this.Transactions).Cache, (object) arTran4, (PXNoteAttribute.IPXCopySettings) null);
      if (arTran1.DeferredCode == null)
        arTran4.DeferredCode = (string) null;
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).RowInserting.AddHandler<ARSalesPerTran>(ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9__183_1 ?? (ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9__183_1 = new PXRowInserting((object) ARCashSaleEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CVoidCheckProc\u003Eb__183_1))));
    foreach (PXResult<ARSalesPerTran> pxResult in PXSelectBase<ARSalesPerTran, PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      ARSalesPerTran copy = PXCache<ARSalesPerTran>.CreateCopy(PXResult<ARSalesPerTran>.op_Implicit(pxResult));
      copy.DocType = ((PXSelectBase<ARCashSale>) this.Document).Current.DocType;
      copy.RefNbr = ((PXSelectBase<ARCashSale>) this.Document).Current.RefNbr;
      copy.Released = new bool?(false);
      copy.CuryInfoID = ((PXSelectBase<ARCashSale>) this.Document).Current.CuryInfoID;
      ARSalesPerTran arSalesPerTran1 = copy;
      Decimal? nullable = arSalesPerTran1.CuryCommnblAmt;
      Decimal num1 = -1M;
      arSalesPerTran1.CuryCommnblAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num1) : new Decimal?();
      ARSalesPerTran arSalesPerTran2 = copy;
      nullable = arSalesPerTran2.CuryCommnAmt;
      Decimal num2 = -1M;
      arSalesPerTran2.CuryCommnAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num2) : new Decimal?();
      SalesPerson salesPerson = (SalesPerson) PXSelectorAttribute.Select<ARSalesPerTran.salespersonID>(((PXSelectBase) this.salesPerTrans).Cache, (object) copy);
      if (salesPerson != null)
      {
        bool? isActive = salesPerson.IsActive;
        bool flag = false;
        if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
          ((PXSelectBase<ARSalesPerTran>) this.salesPerTrans).Update(copy);
      }
    }
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    if (!this.IsExternalTax(doc.TaxZoneID))
    {
      List<ARTaxTran> arTaxTranList = new List<ARTaxTran>();
      foreach (PXResult<ARTaxTran> pxResult in PXSelectBase<ARTaxTran, PXSelect<ARTaxTran, Where<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>, And<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }))
      {
        ARTaxTran arTaxTran1 = PXResult<ARTaxTran>.op_Implicit(pxResult);
        ARTaxTran arTaxTran2 = new ARTaxTran();
        arTaxTran2.TaxID = arTaxTran1.TaxID;
        ARTaxTran arTaxTran3 = ((PXSelectBase<ARTaxTran>) this.Taxes).Insert(arTaxTran2);
        if (arTaxTran3 != null)
        {
          ARTaxTran copy = PXCache<ARTaxTran>.CreateCopy(arTaxTran3);
          copy.TaxRate = arTaxTran1.TaxRate;
          copy.CuryTaxableAmt = arTaxTran1.CuryTaxableAmt;
          copy.CuryTaxAmt = arTaxTran1.CuryTaxAmt;
          copy.CuryTaxAmtSumm = arTaxTran1.CuryTaxAmtSumm;
          copy.CuryTaxDiscountAmt = arTaxTran1.CuryTaxDiscountAmt;
          copy.CuryTaxableDiscountAmt = arTaxTran1.CuryTaxableDiscountAmt;
          arTaxTranList.Add(copy);
        }
      }
      foreach (ARTaxTran arTaxTran in arTaxTranList)
        ((PXSelectBase<ARTaxTran>) this.Taxes).Update(arTaxTran);
    }
    ARCashSale current = ((PXSelectBase<ARCashSale>) this.Document).Current;
    current.CuryOrigDiscAmt = doc.CuryOrigDiscAmt;
    ((PXSelectBase<ARCashSale>) this.Document).Update(current);
    this.PaymentCharges.ReverseCharges((PX.Objects.CM.IRegister) doc, (PX.Objects.CM.IRegister) ((PXSelectBase<ARCashSale>) this.Document).Current);
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual ARCashSale CalculateExternalTax(ARCashSale invoice) => invoice;

  public class ARCashSaleEntryDocumentExtension : PaidInvoiceGraphExtension<ARCashSaleEntry>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Documents = new PXSelectExtension<PaidInvoice>((PXSelectBase) this.Base.Document);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.Transactions);
    }

    public override void SuppressApproval() => this.Base.Approval.SuppressApproval = true;

    protected override PaidInvoiceMapping GetDocumentMapping()
    {
      return new PaidInvoiceMapping(typeof (ARCashSale))
      {
        HeaderTranPeriodID = typeof (ARCashSale.adjTranPeriodID),
        HeaderDocDate = typeof (ARCashSale.adjDate)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (ARTran));
    }
  }

  public class MultiCurrency : ARMultiCurrencyGraph<ARCashSaleEntry, ARCashSale>
  {
    protected override string DocumentStatus
    {
      get => ((PXSelectBase<ARCashSale>) this.Base.Document).Current?.Status;
    }

    protected override MultiCurrencyGraph<ARCashSaleEntry, ARCashSale>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<ARCashSaleEntry, ARCashSale>.CurySourceMapping(typeof (PX.Objects.CA.CashAccount))
      {
        CuryID = typeof (PX.Objects.CA.CashAccount.curyID),
        CuryRateTypeID = typeof (PX.Objects.CA.CashAccount.curyRateTypeID)
      };
    }

    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = base.CurrentSourceSelect();
      if (curySource == null)
        return (CurySource) null;
      if (((PXSelectBase<ARCashSale>) this.Base.Document)?.Current?.DocType == "RCS")
      {
        curySource.AllowOverrideRate = new bool?(false);
        curySource.AllowOverrideCury = new bool?(false);
      }
      else
        curySource.AllowOverrideRate = (bool?) ((PXSelectBase<Customer>) this.Base.customer)?.Current?.AllowOverrideRate;
      return curySource;
    }

    protected override MultiCurrencyGraph<ARCashSaleEntry, ARCashSale>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ARCashSaleEntry, ARCashSale>.DocumentMapping(typeof (ARCashSale))
      {
        DocumentDate = typeof (ARCashSale.adjDate),
        BAccountID = typeof (ARCashSale.customerID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[7]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.Transactions,
        (PXSelectBase) this.Base.Tax_Rows,
        (PXSelectBase) this.Base.Taxes,
        (PXSelectBase) this.Base.salesPerTrans,
        (PXSelectBase) this.Base.PaymentCharges,
        (PXSelectBase) this.Base.dummy_CATran
      };
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.documentDate> e)
    {
      if (((PXSelectBase<ARCashSale>) this.Base.Document)?.Current?.DocType == "RCS")
        return;
      base._(e);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<ARCashSale, ARCashSale.cashAccountID> e)
    {
      if (this.Base._IsVoidCheckInProgress || !PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        return;
      this.SourceFieldUpdated<ARCashSale.curyInfoID, ARCashSale.curyID, ARCashSale.adjDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARCashSale, ARCashSale.cashAccountID>>) e).Cache, (IBqlTable) e.Row);
      this.SetDetailCuryInfoID<ARTran>((PXSelectBase<ARTran>) this.Base.Transactions, e.Row.CuryInfoID);
    }
  }

  /// <exclude />
  public class ARCashSaleEntryAddressLookupExtension : 
    AddressLookupExtension<ARCashSaleEntry, ARCashSale, ARAddress>
  {
    protected override string AddressView => "Billing_Address";
  }

  /// <exclude />
  public class ARCashSaleEntryShippingAddressLookupExtension : 
    AddressLookupExtension<ARCashSaleEntry, ARCashSale, ARShippingAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }

  public class ARCashSaleEntryAddressCachingHelper : 
    AddressValidationExtension<ARCashSaleEntry, ARAddress>
  {
    protected override IEnumerable<PXSelectBase<ARAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ARCashSaleEntry.ARCashSaleEntryAddressCachingHelper addressCachingHelper = this;
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
      this.\u003C\u003E2__current = (PXSelectBase<ARAddress>) addressCachingHelper.Base.Billing_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  public class ARCashSaleEntryShippingAddressCachingHelper : 
    AddressValidationExtension<ARCashSaleEntry, ARShippingAddress>
  {
    protected override IEnumerable<PXSelectBase<ARShippingAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ARCashSaleEntry.ARCashSaleEntryShippingAddressCachingHelper addressCachingHelper = this;
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
      this.\u003C\u003E2__current = (PXSelectBase<ARShippingAddress>) addressCachingHelper.Base.Shipping_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
