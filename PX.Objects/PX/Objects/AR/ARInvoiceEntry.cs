// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Api.Models;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.Description;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AR.BQL;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.Common.Scopes;
using PX.Objects.Common.Utility;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.Extensions.CostAccrual;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AR;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.Helpers;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
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
public class ARInvoiceEntry : 
  ARDataEntryGraph<
  #nullable disable
  ARInvoiceEntry, ARInvoice>,
  PXImportAttribute.IPXPrepareItems,
  IGraphWithInitialization
{
  public PXWorkflowEventHandler<ARInvoice, ARRegister> OnConfirmSchedule;
  public PXWorkflowEventHandler<ARInvoice, ARRegister> OnVoidSchedule;
  public PXWorkflowEventHandler<ARInvoice> OnOpenDocument;
  public PXWorkflowEventHandler<ARInvoice> OnCloseDocument;
  public PXWorkflowEventHandler<ARInvoice> OnVoidDocument;
  public PXWorkflowEventHandler<ARInvoice> OnReleaseDocument;
  public PXWorkflowEventHandler<ARInvoice> OnCancelDocument;
  public PXWorkflowEventHandler<ARInvoice> OnUpdateStatus;
  public PXWorkflowEventHandler<ARInvoice> OnCompleteProcessing;
  public PXAction<ARInvoice> viewSchedule;
  public PXAction<ARPayment> viewPPDVATAdj;
  public PXAction<ARInvoice> newCustomer;
  public PXAction<ARInvoice> editCustomer;
  public PXAction<ARInvoice> customerDocuments;
  public PXAction<ARInvoice> sOInvoice;
  public PXAction<ARInvoice> sendARInvoiceMemo;
  public PXAction<ARInvoice> writeOff;
  public PXAction<ARInvoice> ViewOriginalDocument;
  public PXAction<ARInvoice> reverseInvoice;
  public PXAction<ARInvoice> reverseInvoiceAndApplyToMemo;
  public PXAction<ARInvoice> payInvoice;
  public PXAction<ARInvoice> createSchedule;
  public PXAction<ARInvoice> viewScheduleOfCurrentDocument;
  public PXAction<ARInvoice> reclassifyBatch;
  public PXAction<ARInvoice> loadDocuments;
  public PXAction<ARInvoice> autoApply;
  public PXAction<ARInvoice> viewPayment;
  public PXAction<ARInvoice> viewInvoice;
  public PXAction<ARInvoice> viewItem;
  public PXAction<ARInvoice> validateAddresses;
  public PXAction<ARInvoice> recalculateDiscountsAction;
  public PXAction<ARInvoice> RecalculatePricesAndDiscountsFromImport;
  public PXAction<ARInvoice> recalcOk;
  public static readonly Dictionary<string, string> ARDocTypeDict = new ARDocType.ListAttribute().ValueLabelDic;
  public PXAction<ARInvoice> customerRefund;
  public PXAction<ARInvoice> putOnCreditHold;
  public PXAction<ARInvoice> releaseFromCreditHold;
  public PXAction<ARInvoice> emailInvoice;
  public PXAction<ARInvoice> sendEmail;
  public PXAction<ARInvoice> printInvoice;
  public PXAction<ARInvoice> approve;
  public PXAction<ARInvoice> reject;
  public PXSelect<PX.Objects.AR.Standalone.ARRegister> dummy_register;
  public PXSelect<PX.Objects.IN.InventoryItem> dummy_nonstockitem_for_redirect_newitem;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> bAccountBasic;
  [PXHidden]
  public PXSelect<BAccountR> bAccountRBasic;
  public PXSelect<PX.Objects.AP.Vendor> dummy_vendor_taxAgency_for_avalara;
  [PXViewName("AR Invoice/Memo")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (ARInvoice.invoiceNbr)}, FieldsToShowInSimpleImport = new System.Type[] {typeof (ARInvoice.invoiceNbr)})]
  public PXSelectJoin<ARInvoice, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>>, Where<ARInvoice.docType, Equal<Optional<ARInvoice.docType>>, And2<Where<ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<ARInvoice.released, Equal<True>>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> Document;
  [PXCopyPasteEmptyFields(new System.Type[] {typeof (ARInvoice.taxZoneID), typeof (ARInvoice.externalTaxExemptionNumber), typeof (ARInvoice.workgroupID), typeof (ARInvoice.ownerID), typeof (ARInvoice.paymentMethodID), typeof (ARInvoice.pMInstanceID), typeof (ARInvoice.cashAccountID), typeof (ARInvoice.salesPersonID)})]
  public PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Current<ARInvoice.refNbr>>>>> CurrentDocument;
  [PXCopyPasteHiddenView]
  public PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>>>> AllTransactions;
  [PXViewName("AR Transactions")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APTran.defScheduleID)})]
  [PXImport(typeof (ARInvoice))]
  public PXOrderedSelect<ARInvoice, ARTran, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.sortOrder, Asc<ARTran.lineNbr>>>>>> Transactions;
  public PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<ARTran.lineType, Equal<SOLineType.discount>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.lineNbr>>>>> Discount_Row;
  [PXCopyPasteHiddenView]
  public PXSelect<ARTax, Where<ARTax.tranType, Equal<Current<ARInvoice.docType>>, And<ARTax.refNbr, Equal<Current<ARInvoice.refNbr>>>>, OrderBy<Asc<ARTax.tranType, Asc<ARTax.refNbr, Asc<ARTax.taxID>>>>> Tax_Rows;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARInvoice.refNbr>>>>>> Taxes;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTaxTran.refNbr, Equal<Current<ARInvoice.refNbr>>>>>> TaxesList;
  /// <summary>
  /// Applications for the current document, except
  /// when it is a credit memo.
  /// </summary>
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARAdjust2, InnerJoin<ARPayment, On<ARPayment.docType, Equal<ARAdjust2.adjgDocType>, And<ARPayment.refNbr, Equal<ARAdjust2.adjgRefNbr>>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<ARPayment.cCActualExternalTransactionID>>>>> Adjustments;
  /// <summary>
  /// Applications for the current document,
  /// when it is an unreleased credit memo.
  /// </summary>
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARAdjust, InnerJoin<ARInvoice, On<ARInvoice.docType, Equal<ARAdjust.adjgDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjgRefNbr>>>>> Adjustments_1;
  public PXSelectJoin<CCProcessingCenter, LeftJoin<CustomerPaymentMethod, On<CCProcessingCenter.processingCenterID, Equal<CustomerPaymentMethod.cCProcessingCenterID>>>, Where<CustomerPaymentMethod.pMInstanceID, Equal<Current<ARInvoice.pMInstanceID>>>> ProcessingCenter;
  public PXSelectJoin<ARAdjust2, InnerJoin<PX.Objects.AR.Standalone.ARPayment, On<PX.Objects.AR.Standalone.ARPayment.docType, Equal<ARAdjust2.adjgDocType>, And<PX.Objects.AR.Standalone.ARPayment.refNbr, Equal<ARAdjust2.adjgRefNbr>>>, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARAdjust2.adjgDocType>, And<ARRegisterAlias.refNbr, Equal<ARAdjust2.adjgRefNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<PX.Objects.AR.Standalone.ARPayment.cCActualExternalTransactionID>>>>>>, Where<ARAdjust2.invoiceID, Equal<Current<ARInvoice.noteID>>>> Adjustments_Inv;
  public PXSelectJoin<PX.Objects.AR.Standalone.ARPayment, InnerJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<PX.Objects.AR.Standalone.ARPayment.docType>, And<ARRegisterAlias.refNbr, Equal<PX.Objects.AR.Standalone.ARPayment.refNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<ARPayment.cCActualExternalTransactionID>>>>>, Where<ARRegisterAlias.customerID, Equal<Current<ARInvoice.customerID>>, And<ARRegisterAlias.released, Equal<True>, And<ARRegisterAlias.openDoc, Equal<True>, And<ARRegisterAlias.hold, NotEqual<True>, And<PX.Objects.AR.Standalone.ARPayment.refNbr, In<Required<PX.Objects.AR.Standalone.ARPayment.refNbr>>>>>>>> AvailablePayments;
  public PXSelect<ARAdjust, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  ARInvoice.docType>, 
  #nullable disable
  Equal<ARDocType.creditMemo>>>>>.And<BqlOperand<
  #nullable enable
  ARAdjust.memoID, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARInvoice.noteID, IBqlGuid>.FromCurrent>>>> Adjustments_Crm;
  public 
  #nullable disable
  PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelect<ARInvoice, Where<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>> ARInvoice_CustomerID_DocType_RefNbr;
  public FbqlSelect<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<
  #nullable enable
  PMTran.projectID, IBqlInt>.IsEqual<
  #nullable disable
  PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.aRTranType, 
  #nullable disable
  Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.aRRefNbr, 
  #nullable disable
  Equal<P.AsString>>>>>.And<BqlOperand<
  #nullable enable
  PMTran.refLineNbr, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>>>, PMTran>.View RefContractUsageTran;
  [PXViewName("AR Ship-To Address")]
  [PXCopyPasteEmptyFields(new System.Type[] {typeof (ARShippingAddress.addressLine1), typeof (ARShippingAddress.addressLine2), typeof (ARShippingAddress.city), typeof (ARShippingAddress.state), typeof (ARShippingAddress.postalCode), typeof (ARAddress.department), typeof (ARAddress.subDepartment), typeof (ARAddress.streetName), typeof (ARAddress.buildingNumber), typeof (ARAddress.buildingName), typeof (ARAddress.floor), typeof (ARAddress.unitNumber), typeof (ARAddress.postBox), typeof (ARAddress.room), typeof (ARAddress.townLocationName), typeof (ARAddress.districtName), typeof (ARShippingAddress.latitude), typeof (ARShippingAddress.longitude)})]
  public PXSelect<ARShippingAddress, Where<ARShippingAddress.addressID, Equal<Current<ARInvoice.shipAddressID>>>> Shipping_Address;
  [PXViewName("AR Ship-To Contact")]
  [PXCopyPasteEmptyFields(new System.Type[] {typeof (ARShippingContact.fullName), typeof (ARShippingContact.attention), typeof (ARShippingContact.phone1), typeof (ARShippingContact.email)})]
  public PXSelect<ARShippingContact, Where<ARShippingContact.contactID, Equal<Current<ARInvoice.shipContactID>>>> Shipping_Contact;
  [PXViewName("AR Bill-To Address")]
  [PXCopyPasteEmptyFields(new System.Type[] {typeof (ARAddress.addressLine1), typeof (ARAddress.addressLine2), typeof (ARAddress.city), typeof (ARAddress.state), typeof (ARAddress.postalCode), typeof (ARAddress.department), typeof (ARAddress.subDepartment), typeof (ARAddress.streetName), typeof (ARAddress.buildingNumber), typeof (ARAddress.buildingName), typeof (ARAddress.floor), typeof (ARAddress.unitNumber), typeof (ARAddress.postBox), typeof (ARAddress.room), typeof (ARAddress.townLocationName), typeof (ARAddress.districtName)})]
  public PXSelect<ARAddress, Where<ARAddress.addressID, Equal<Current<ARInvoice.billAddressID>>>> Billing_Address;
  [PXViewName("AR Bill-To Contact")]
  [PXCopyPasteEmptyFields(new System.Type[] {typeof (ARContact.fullName), typeof (ARContact.attention), typeof (ARContact.phone1), typeof (ARContact.email)})]
  public PXSelect<ARContact, Where<ARContact.contactID, Equal<Current<ARInvoice.billContactID>>>> Billing_Contact;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARInvoice.curyInfoID>>>> currencyinfo;
  [PXViewName("Customer")]
  public PXSetup<Customer>.Where<BqlOperand<
  #nullable enable
  Customer.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARInvoice.customerID, IBqlInt>.AsOptional>> customer;
  public 
  #nullable disable
  PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARInvoice.branchID, IBqlInt>.AsOptional>> branch;
  public 
  #nullable disable
  PXSetup<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>> customerclass;
  public PXSetup<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<ARInvoice.taxZoneID>>>> taxzone;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARInvoice.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<ARInvoice.customerLocationID>>>>> location;
  public PXSelect<ARBalances> arbalances;
  public PXSetup<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<ARInvoice.finPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<ARInvoice.branchID>>>>> finperiod;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXSetup<GLSetup> glsetup;
  public PXSetupOptional<SOSetup> soSetup;
  [PXCopyPasteHiddenView]
  public PXFilter<RecalcDiscountsParamFilter> recalcdiscountsfilter;
  [PXCopyPasteHiddenView]
  public PXSelectJoinGroupBy<ARDunningLetterDetail, InnerJoin<Customer, On<Customer.bAccountID, Equal<Current<ARInvoice.customerID>>>, LeftJoin<ARDunningLetter, On<ARDunningLetter.dunningLetterID, Equal<ARDunningLetterDetail.dunningLetterID>>>>, Where<ARDunningLetterDetail.dunningLetterBAccountID, Equal<Customer.sharedCreditCustomerID>, And<ARDunningLetterDetail.refNbr, Equal<Current<ARInvoice.refNbr>>, And<ARDunningLetterDetail.docType, Equal<Current<ARInvoice.docType>>, And<ARDunningLetter.voided, Equal<False>, And<ARDunningLetter.released, Equal<True>, And<ARDunningLetterDetail.dunningLetterLevel, Greater<int0>>>>>>>, Aggregate<GroupBy<ARDunningLetter.voided, GroupBy<ARDunningLetter.released, GroupBy<ARDunningLetterDetail.refNbr, GroupBy<ARDunningLetterDetail.docType>>>>>> dunningLetterDetail;
  public PXSelect<ARInvoiceDiscountDetail, Where<ARInvoiceDiscountDetail.docType, Equal<Current<ARInvoice.docType>>, And<ARInvoiceDiscountDetail.refNbr, Equal<Current<ARInvoice.refNbr>>>>, OrderBy<Asc<ARInvoiceDiscountDetail.lineNbr>>> ARDiscountDetails;
  public PXSelect<CustSalesPeople, Where<CustSalesPeople.bAccountID, Equal<Current<ARInvoice.customerID>>, And<CustSalesPeople.locationID, Equal<Current<ARInvoice.customerLocationID>>>>> salesPerSettings;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<ARSalesPerTran, LeftJoin<ARSPCommissionPeriod, On<ARSPCommissionPeriod.commnPeriodID, Equal<ARSalesPerTran.commnPaymntPeriod>>>, Where<ARSalesPerTran.docType, Equal<Current<ARInvoice.docType>>, And<ARSalesPerTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<ARSalesPerTran.adjdDocType, Equal<ARDocType.undefined>, And2<Where<Current<PX.Objects.AR.ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byInvoice>, Or<Current<ARInvoice.released>, Equal<boolFalse>>>, Or<ARSalesPerTran.adjdDocType, Equal<Current<ARInvoice.docType>>, And<ARSalesPerTran.adjdRefNbr, Equal<Current<ARInvoice.refNbr>>, And<Current<PX.Objects.AR.ARSetup.sPCommnCalcType>, Equal<SPCommnCalcTypes.byPayment>>>>>>>>> salesPerTrans;
  public PXSelect<ARFinChargeTran, Where<ARFinChargeTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARFinChargeTran.refNbr, Equal<Current<ARInvoice.refNbr>>>>> finChargeTrans;
  /// <summary>
  /// This dummy view is used to provide a fix for AC-259509, currently there is a plaform limitation of PXOverride is not working with .Net Inheritance of Child of Parent type,
  /// because of that reason, it is not possible override GetChildren method of PX.Objects.AR.ARInvoiceEntry.MultiCurrency for SOInvoiceEntry.
  /// This can be removed, once AC-262831 issue is fixed from Platform team.
  /// </summary>
  [PXCopyPasteHiddenView]
  public PXSelect<SOFreightDetail, Where<SOFreightDetail.docType, Equal<Optional2<ARInvoice.docType>>, And<SOFreightDetail.refNbr, Equal<Optional2<ARInvoice.refNbr>>>>> FreightDetailsDummy;
  public PXSelect<DRSchedule> dummySchedule_forPXParent;
  public PXSelect<DRScheduleDetail> dummyScheduleDetail_forPXParent;
  public PXSelect<DRScheduleTran> dummyScheduleTran_forPXParent;
  [PXCopyPasteHiddenView]
  public PXFilter<DuplicateFilter> duplicatefilter;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;
  public PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>> CurrentBranch;
  public PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>> InventoryItem;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMUnbilledDailySummaryAccum> UnbilledSummary;
  [PXCopyPasteHiddenView]
  [PXViewName("Customer Payment Method Details")]
  public PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.bAccountID, Equal<Current<ARInvoice.customerID>>, And<CustomerPaymentMethod.paymentMethodID, Equal<Current<ARInvoice.paymentMethodID>>>>> CustomerPaymentMethodDetails;
  [PXHidden]
  public PXSelect<CRRelation> RelationsLink;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<ARPaymentTotals> PaymentTotalsUpd;
  [PXReadOnlyView]
  [PXCopyPasteHiddenView]
  public PXSelect<ARRetainageInvoice, Where<True, Equal<False>>> RetainageDocuments;
  internal Dictionary<string, HashSet<string>> TaxesByTaxCategory;
  public FbqlSelect<SelectFromBase<ARSetupApproval, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  ARSetupApproval.docType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  ARInvoice.docType, IBqlString>.FromCurrent>>, 
  #nullable disable
  ARSetupApproval>.View SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomationWithoutHoldDefaulting<ARInvoice, ARRegister.approved, ARRegister.rejected, ARInvoice.hold, ARSetupApproval> Approval;
  protected string salesSubMask;
  public PXAction<ARInvoice> notification;
  private Dictionary<System.Type, CachePermission> cachePermission;
  private bool isReverse;
  private static readonly Dictionary<string, string> DocTypes = new ARInvoiceType.TaxAdjdListAttribute().ValueLabelDic;

  protected DiscountEngine<ARTran, ARInvoiceDiscountDetail> ARDiscountEngine
  {
    get => DiscountEngineProvider.GetEngineFor<ARTran, ARInvoiceDiscountDetail>();
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void ARInvoice_CustomerLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [LastFinchargeDate]
  protected virtual void ARInvoice_LastFinChargeDate_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [LastPaymentDate]
  protected virtual void ARInvoice_LastPaymentDate_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void ARInvoice_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  [PXFormula(typeof (IIf<Where<ExternalCall, Equal<True>, Or<PendingValue<ARInvoice.termsID>, IsNotPending>>, IIf<Where<Current<ARInvoice.docType>, NotEqual<ARDocType.creditMemo>, Or<Current<PX.Objects.AR.ARSetup.termsInCreditMemos>, Equal<True>>>, Selector<ARInvoice.customerID, Customer.termsID>, Null>, ARInvoice.termsID>))]
  [PXUIField]
  [ARTermsSelector]
  [Terms(typeof (ARInvoice.docDate), typeof (ARInvoice.dueDate), typeof (ARInvoice.discDate), typeof (ARInvoice.curyOrigDocAmt), typeof (ARInvoice.curyOrigDiscAmt), typeof (ARInvoice.curyTaxTotal), typeof (ARInvoice.branchID))]
  protected virtual void ARInvoice_TermsID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Where2<FeatureInstalled<FeaturesSet.paymentsByLines>, And<ARInvoice.origModule, NotEqual<BatchModule.moduleTX>, And<ARInvoice.origModule, NotEqual<BatchModule.moduleEP>, And<ARInvoice.origModule, NotEqual<BatchModule.moduleSO>, And<Substring<ARRegister.createdByScreenID, int0, int2>, NotEqual<BatchModule.moduleFS>, And<ARInvoice.isMigratedRecord, NotEqual<True>, And<ARInvoice.pendingPPD, NotEqual<True>, And<ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>, And<Current<Customer.paymentsByLinesAllowed>, Equal<True>>>>>>>>>>))]
  protected virtual void ARInvoice_PaymentsByLinesAllowed_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Retained Amount", FieldClass = "Retainage")]
  protected virtual void ARInvoice_CuryLineRetainageTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Document Discounts", Enabled = false)]
  protected virtual void ARInvoice_CuryDiscTot_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<INPostClass.cOGSSubID, Where<INPostClass.postClassID, Equal<Current<PX.Objects.IN.InventoryItem.postClassID>>>>))]
  [SubAccount(typeof (PX.Objects.IN.InventoryItem.cOGSAcctID), DisplayName = "Expense Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual void InventoryItem_COGSSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (ARInvoice.docType))]
  protected virtual void ARSalesPerTran_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARInvoice.refNbr))]
  [PXParent(typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARSalesPerTran.docType>>, And<ARInvoice.refNbr, Equal<Current<ARSalesPerTran.refNbr>>>>>))]
  protected virtual void ARSalesPerTran_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (ARInvoice.branchID), DefaultForInsert = true, DefaultForUpdate = true)]
  protected virtual void ARSalesPerTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [SalesPerson(DirtyRead = true, Enabled = false, IsKey = true, DescriptionField = typeof (PX.Objects.CR.Contact.displayName))]
  protected virtual void ARSalesPerTran_SalespersonID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  protected virtual void ARSalesPerTran_AdjNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsFixed = true, IsKey = true)]
  [PXDefault("UND")]
  protected virtual void ARSalesPerTran_AdjdDocType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault("")]
  protected virtual void ARSalesPerTran_AdjdRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<Search<CustSalesPeople.commisionPct, Where<CustSalesPeople.bAccountID, Equal<Current<ARInvoice.customerID>>, And<CustSalesPeople.locationID, Equal<Current<ARInvoice.customerLocationID>>, And<CustSalesPeople.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>>, Search<SalesPerson.commnPct, Where<SalesPerson.salesPersonID, Equal<Current<ARSalesPerTran.salespersonID>>>>>))]
  [PXUIField(DisplayName = "Commission %")]
  protected virtual void ARSalesPerTran_CommnPct_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (ARSalesPerTran.curyInfoID), typeof (ARSalesPerTran.commnblAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Commissionable Amount", Enabled = false)]
  [PXFormula(null, typeof (SumCalc<ARInvoice.curyCommnblAmt>))]
  protected virtual void ARSalesPerTran_CuryCommnblAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (ARSalesPerTran.curyInfoID), typeof (ARSalesPerTran.commnAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<ARSalesPerTran.curyCommnblAmt, Div<ARSalesPerTran.commnPct, decimal100>>), typeof (SumCalc<ARInvoice.curyCommnAmt>))]
  [PXUIField(DisplayName = "Commission Amt.", Enabled = false)]
  protected virtual void ARSalesPerTran_CuryCommnAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsFixed = true)]
  [PXDBDefault(typeof (ARInvoice.docType))]
  public void PMTran_ARTranType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AR Reference Nbr.")]
  [PXDBDefault(typeof (ARInvoice.refNbr))]
  public void PMTran_ARRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (ARTran.lineNbr))]
  public void PMTran_RefLineNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBLong]
  protected void _(PX.Data.Events.CacheAttached<PMTran.projectCuryInfoID> e)
  {
  }

  [PXDBDefault(typeof (ARRegister.branchID))]
  [Branch(null, null, true, true, true, Enabled = false)]
  protected virtual void ARTaxTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [LocationAvail(typeof (ARTran.inventoryID), typeof (ARTran.subItemID), typeof (CostCenter.freeStock), typeof (ARTran.siteID), typeof (ARTran.tranType), typeof (ARTran.invtMult), false)]
  [PXDefault]
  protected virtual void ARTran_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (ARInvoice.branchID))]
  protected virtual void ARAdjust_AdjgBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (ARInvoice.refNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARPaymentTotals.adjdRefNbr> eventArgs)
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

  [PXDefault(typeof (ARInvoice.docDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (ARInvoice.customerID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (ARInvoice.ownerID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (ARInvoice.docDesc))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (ARInvoice.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (ARInvoice.curyOrigDocAmt))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (ARInvoice.origDocAmt))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_SourceItemType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null)
      return;
    e.NewValue = (object) ARInvoiceEntry.ARDocTypeDict[((PXSelectBase<ARInvoice>) this.Document).Current.DocType];
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    ARTran current = ((PXSelectBase<ARTran>) this.Transactions).Current;
    if (current != null && ((PXSelectBase) this.Transactions).Cache.GetStatus((object) current) == null)
    {
      ((PXAction) this.Save).Press();
      ARInvoiceEntry.ViewScheduleForLine((PXGraph) this, (ARRegister) ((PXSelectBase<ARInvoice>) this.Document).Current, current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewPPDVATAdj(PXAdapter adapter)
  {
    ARAdjust2 current = ((PXSelectBase<ARAdjust2>) this.Adjustments).Current;
    if (current != null)
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) current.PPDVATAdjRefNbr, new object[1]
      {
        (object) current.PPDVATAdjDocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  public static void ViewScheduleForLine(PXGraph graph, ARRegister document, ARTran documentLine)
  {
    PXSelectBase<DRSchedule> pxSelectBase = (PXSelectBase<DRSchedule>) new PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Current<ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Current<ARTran.refNbr>>, And<DRSchedule.lineNbr, Equal<Current<ARTran.lineNbr>>>>>>>(graph);
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(pxSelectBase.Select(Array.Empty<object>()));
    if (drSchedule == null || drSchedule.IsDraft.GetValueOrDefault())
    {
      PXResult<ARTax, PX.Objects.TX.Tax> pxResult = (PXResult<ARTax, PX.Objects.TX.Tax>) PXResultset<ARTax>.op_Implicit(PXSelectBase<ARTax, PXSelectJoin<ARTax, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTax.taxID>>>, Where<ARTax.tranType, Equal<Required<ARTax.tranType>>, And<ARTax.refNbr, Equal<Required<ARTax.refNbr>>, And<ARTax.lineNbr, Equal<Required<ARTax.lineNbr>>>>>>.Config>.Select(graph, new object[3]
      {
        (object) documentLine.TranType,
        (object) documentLine.RefNbr,
        (object) documentLine.LineNbr
      }));
      ARReleaseProcess.Amount salesPostingAmount = ARReleaseProcess.GetSalesPostingAmount(graph, document, documentLine, PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult), PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult), (Func<Decimal, Decimal>) (amount => PX.Objects.CM.PXDBCurrencyAttribute.Round(graph.Caches[typeof (ARTran)], (object) documentLine, amount, CMPrecision.TRANCURY)));
      DRDeferredCode deferralCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Current2<ARTran.deferredCode>>>>.Config>.Select(graph, Array.Empty<object>()));
      if (deferralCode != null)
      {
        DRProcess instance = PXGraph.CreateInstance<DRProcess>();
        instance.CreateSchedule(documentLine, deferralCode, document, salesPostingAmount.Base.Value, true);
        ((PXGraph) instance).Actions.PressSave();
        ((PXSelectBase) pxSelectBase).Cache.Clear();
        ((PXSelectBase) pxSelectBase).Cache.ClearQueryCacheObsolete();
        ((PXSelectBase) pxSelectBase).View.Clear();
        drSchedule = PXResultset<DRSchedule>.op_Implicit(pxSelectBase.Select(Array.Empty<object>()));
      }
    }
    if (drSchedule == null)
      throw new PXException("Deferral schedule does not exist for the selected line.");
    PXRedirectHelper.TryRedirect(graph.Caches[typeof (DRSchedule)], (object) drSchedule, "View Schedule", (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable NewCustomer(PXAdapter adapter)
  {
    throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<CustomerMaint>(), "New Customer");
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
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
  [PXButton(DisplayOnMainToolbar = false)]
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
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable SOInvoice(PXAdapter adapter)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
    ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) current.RefNbr, new object[1]
    {
      (object) current.DocType
    }));
    throw new PXRedirectRequiredException((PXGraph) instance, "SO Invoice");
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable SendARInvoiceMemo(PXAdapter adapter, [PXString] string reportID)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if (reportID == null)
      reportID = "AR641000";
    if (current != null)
    {
      using (new LocalizationFeatureScope((PXGraph) this))
      {
        ReportNotificationGenerator notificationGenerator = this.ReportNotificationGeneratorFactory(reportID);
        notificationGenerator.Parameters = (IDictionary<string, string>) new Dictionary<string, string>()
        {
          ["DocType"] = current.DocType,
          ["RefNbr"] = current.RefNbr
        };
        if (!notificationGenerator.Send().Any<CRSMEmail>())
          throw new PXException("The mail send has failed.");
        ((PXGraph) this).Clear();
        ((PXSelectBase<ARInvoice>) this.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) this.Document).Search<ARInvoice.refNbr>((object) current.RefNbr, new object[1]
        {
          (object) current.DocType
        }));
      }
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
    ARInvoiceEntry.\u003C\u003Ec__DisplayClass69_0 cDisplayClass690 = new ARInvoiceEntry.\u003C\u003Ec__DisplayClass69_0();
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass690.list = new List<ARRegister>();
    foreach (ARInvoice doc in adapter.Get<ARInvoice>())
    {
      this.OnBeforeRelease((ARRegister) doc);
      bool? nullable = doc.Hold;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = doc.Released;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          GraphHelper.MarkUpdated(cache, (object) doc);
          // ISSUE: reference to a compiler-generated field
          cDisplayClass690.list.Add((ARRegister) doc);
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass690.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass690, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass690.list;
  }

  public virtual ARRegister OnBeforeRelease(ARRegister doc) => doc;

  public void ReleaseProcess(List<ARRegister> list)
  {
    PXTimeStampScope.SetRecordComesFirst(typeof (ARInvoice), true);
    ARDocumentRelease.ReleaseDoc(list, false, (List<PX.Objects.GL.Batch>) null, (ARDocumentRelease.ARMassProcessDelegate) ((a, b) => { }));
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable WriteOff(PXAdapter adapter)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null && (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "INV" || ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "DRM" || ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM" || ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "FCH" || ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "SMC"))
    {
      ((PXAction) this.Save).Press();
      bool? nullable1;
      if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM")
      {
        nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.PaymentsByLinesAllowed;
        if (nullable1.GetValueOrDefault())
          throw new PXException("The {0} credit memo is paid by line; write-offs are not supported for such credit memos. To write off the credit memo, create a debit memo and apply this credit memo to it on the Invoices and Memos (AR301000) form.", new object[1]
          {
            (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr
          });
      }
      Customer customer = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) this.customer).Select(new object[1]
      {
        (object) ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID
      }));
      if (customer != null)
      {
        nullable1 = customer.SmallBalanceAllow;
        if (!nullable1.GetValueOrDefault())
          throw new PXException("Write-Off is disabled for the given customer. Set non zero write-off limit on the Customer screen and try again.");
        Decimal? smallBalanceLimit = customer.SmallBalanceLimit;
        Decimal? nullable2 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDocBal;
        if (smallBalanceLimit.GetValueOrDefault() < nullable2.GetValueOrDefault() & smallBalanceLimit.HasValue & nullable2.HasValue)
        {
          nullable2 = customer.SmallBalanceLimit;
          Decimal valueOrDefault = nullable2.GetValueOrDefault();
          int num1;
          if (((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Current != null)
          {
            short? basePrecision = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Current.BasePrecision;
            if (basePrecision.HasValue)
            {
              basePrecision = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Current.BasePrecision;
              num1 = (int) basePrecision.Value;
              goto label_12;
            }
          }
          num1 = 2;
label_12:
          int num2 = num1;
          throw new PXException("Document balance exceeds the configured write-off limit for the given customer (Limit = {0}). Change the write-off limit on the Customer screen and try again.", new object[1]
          {
            (object) valueOrDefault.ToString("N" + num2.ToString())
          });
        }
      }
      ARCreateWriteOff instance = PXGraph.CreateInstance<ARCreateWriteOff>();
      if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM")
        ((PXSelectBase) instance.Filter).Cache.SetValueExt<ARWriteOffFilter.woType>((object) ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current, (object) "SMC");
      ((PXSelectBase) instance.Filter).Cache.SetValueExt<ARWriteOffFilter.organizationID>((object) ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current, (object) PXAccess.GetParentOrganizationID(((PXSelectBase<ARInvoice>) this.Document).Current.BranchID));
      ((PXSelectBase) instance.Filter).Cache.SetValueExt<ARWriteOffFilter.branchID>((object) ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current, (object) ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID);
      ((PXSelectBase) instance.Filter).Cache.SetValueExt<ARWriteOffFilter.orgBAccountID>((object) ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current, (object) PXAccess.GetBranch(((PXSelectBase<ARInvoice>) this.Document).Current.BranchID).BAccountID);
      ((PXSelectBase) instance.Filter).Cache.SetValueExt<ARWriteOffFilter.customerID>((object) ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current, (object) ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID);
      ((PXSelectBase) instance.Filter).Cache.RaiseFieldUpdated<ARWriteOffFilter.wODate>((object) ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current, (object) ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current.WODate);
      foreach (PXResult<ARCreateWriteOff.ARRegisterEx> pxResult in ((PXSelectBase<ARCreateWriteOff.ARRegisterEx>) instance.ARDocumentList).Select(Array.Empty<object>()))
      {
        ARCreateWriteOff.ARRegisterEx arRegisterEx = PXResult<ARCreateWriteOff.ARRegisterEx>.op_Implicit(pxResult);
        if (arRegisterEx.DocType == ((PXSelectBase<ARInvoice>) this.Document).Current.DocType && arRegisterEx.RefNbr == ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr)
        {
          arRegisterEx.Selected = new bool?(true);
          ((PXSelectBase<ARCreateWriteOff.ARRegisterEx>) instance.ARDocumentList).Update(arRegisterEx);
        }
      }
      throw new PXRedirectRequiredException((PXGraph) instance, "Create Write-Off");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewOriginalDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(((PXSelectBase<ARInvoice>) this.Document).Current.OrigDocType, ((PXSelectBase<ARInvoice>) this.Document).Current.OrigRefNbr, ((PXSelectBase<ARInvoice>) this.Document).Current.OrigModule, ((PXSelectBase<ARInvoice>) this.Document).Current.IsChildRetainageDocument());
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  [PXActionRestriction(typeof (Where<Current<ARRegister.isRetainageReversing>, Equal<True>, And<Where<Current<ARInvoice.isRetainageDocument>, Equal<True>, Or<Current<ARInvoice.retainageApply>, Equal<True>>>>>), "The Reverse action cannot be used for credit and debit memos that reverse original documents with retainage and retainage documents.")]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ReverseInvoice(PXAdapter adapter)
  {
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      bool? openDoc = current1.OpenDoc;
      bool flag = false;
      num1 = openDoc.GetValueOrDefault() == flag & openDoc.HasValue ? 1 : 0;
    }
    bool? nullable;
    if (num1 != 0)
    {
      ARInvoice current2 = ((PXSelectBase<ARInvoice>) this.Document).Current;
      int num2;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.Released;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 != 0)
      {
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PaymentsByLinesAllowed;
        if (!nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.RetainageApply;
          if (nullable.GetValueOrDefault())
            throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has been fully or partially settled or it has a released retainage document.", new object[2]
            {
              (object) ARDocType.GetDisplayName(((PXSelectBase<ARInvoice>) this.Document).Current.DocType),
              (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr
            });
        }
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.IsRetainageDocument;
        if (nullable.GetValueOrDefault())
          throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has been fully or partially settled.", new object[2]
          {
            (object) ARDocType.GetDisplayName(((PXSelectBase<ARInvoice>) this.Document).Current.DocType),
            (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr
          });
      }
    }
    ARInvoice current3 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    int num3;
    if (current3 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable = current3.OpenDoc;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num3 != 0)
    {
      ARInvoice current4 = ((PXSelectBase<ARInvoice>) this.Document).Current;
      int num4;
      if (current4 == null)
      {
        num4 = 0;
      }
      else
      {
        nullable = current4.Released;
        num4 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num4 != 0)
      {
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.IsRetainageDocument;
        bool flag1 = nullable.GetValueOrDefault() || ((PXSelectBase<ARInvoice>) this.Document).Current.IsOriginalRetainageDocument();
        ARRegister reversingDoc;
        if (flag1 && this.CheckReversingRetainageDocumentAlreadyExists(((PXSelectBase<ARInvoice>) this.Document).Current, out reversingDoc))
          throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has already been reversed with the {2} document with the {3} ref. number.", new object[4]
          {
            (object) ARDocType.GetDisplayName(((PXSelectBase<ARInvoice>) this.Document).Current.DocType),
            (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr,
            (object) ARDocType.GetDisplayName(reversingDoc.DocType),
            (object) reversingDoc.RefNbr
          });
        bool flag2 = ((IQueryable<PXResult<ARAdjust>>) PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Current<ARInvoice.docType>>, And<ARAdjust.adjdRefNbr, Equal<Current<ARInvoice.refNbr>>, And<ARAdjust.voided, Equal<False>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>())).Any<PXResult<ARAdjust>>();
        ARInvoice origDoc = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.isRetainageDocument, Equal<True>, And<ARInvoice.origDocType, Equal<Required<ARRegister.docType>>, And<ARInvoice.origRefNbr, Equal<Required<ARRegister.refNbr>>, And<ARInvoice.released, Equal<True>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
        {
          (object) ((PXSelectBase<ARInvoice>) this.Document).Current.DocType,
          (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr
        }));
        bool flag3 = origDoc != null && !this.CheckReversingRetainageDocumentAlreadyExists(origDoc, out reversingDoc);
        bool flag4 = ((IQueryable<PXResult<PMTask>>) PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMTask.taskID, IBqlInt>.IsIn<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Transactions).Select(Array.Empty<object>())).Select<ARTran, int?>((Func<ARTran, int?>) (a => a.TaskID)).ToArray<int?>()
        })).Where<PXResult<PMTask>>((Expression<Func<PXResult<PMTask>, bool>>) (a => ((PMTask) a).IsActive != (bool?) true || ((PMTask) a).IsCompleted == (bool?) true)).Any<PXResult<PMTask>>();
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PaymentsByLinesAllowed;
        if (((nullable.GetValueOrDefault() || flag2 || flag3 ? 0 : (!flag4 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
          throw new PXException("The Reverse command cannot be used for invoices with retainage and for retainage documents. To reverse this document, click Reverse and Apply to Memo on the More menu.");
      }
    }
    if (((PXSelectBase<ARInvoice>) this.Document).Current?.DocType == "PPI")
    {
      string str;
      using (new PXLocaleScope(((PXSelectBase<Customer>) this.customer).Current.LocaleName))
      {
        Decimal? curyDocBal = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDocBal;
        Decimal? curyOrigDocAmt = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
        str = PXMessages.LocalizeFormatNoPrefix(curyDocBal.GetValueOrDefault() < curyOrigDocAmt.GetValueOrDefault() & curyDocBal.HasValue & curyOrigDocAmt.HasValue ? "Write-off of prepayment invoice {0}" : "Voiding of prepayment invoice {0}", new object[1]
        {
          (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr
        });
      }
      return this.ReverseDocumentAndApplyToReversalIfNeeded(adapter, new ReverseInvoiceArgs()
      {
        ApplyToOriginalDocument = true,
        OverrideDocumentDescr = str
      });
    }
    return this.ReverseDocumentAndApplyToReversalIfNeeded(adapter, new ReverseInvoiceArgs()
    {
      ApplyToOriginalDocument = false
    });
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  [PXActionRestriction(typeof (Where<Current<ARInvoice.paymentsByLinesAllowed>, Equal<True>>), "The Reverse and Apply to Memo command cannot be used if the Pay by Line check box is selected for a document. To reverse this document, click Reverse on the More menu.")]
  [PXActionRestriction(typeof (Where<Current<ARRegister.isRetainageReversing>, Equal<True>, And<Where<Current<ARInvoice.isRetainageDocument>, Equal<True>, Or<Current<ARInvoice.retainageApply>, Equal<True>>>>>), "The Reverse and Apply to Memo action cannot be used for credit and debit memos that reverse original documents with retainage and retainage documents.")]
  public virtual IEnumerable ReverseInvoiceAndApplyToMemo(PXAdapter adapter)
  {
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.OpenDoc;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
    {
      ARInvoice current2 = ((PXSelectBase<ARInvoice>) this.Document).Current;
      int num2;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = current2.Released;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num2 != 0)
      {
        ARRegister reversingDocument = this.GetReversingDocument(((PXSelectBase<ARInvoice>) this.Document).Current);
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PaymentsByLinesAllowed;
        if (!nullable.GetValueOrDefault() && reversingDocument != null)
        {
          int num3;
          if (reversingDocument == null)
          {
            num3 = 1;
          }
          else
          {
            nullable = reversingDocument.Released;
            num3 = !nullable.GetValueOrDefault() ? 1 : 0;
          }
          if (num3 != 0)
            throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has already been reversed with the {2} document with the {3} ref. number.", new object[4]
            {
              (object) ARDocType.GetDisplayName(((PXSelectBase<ARInvoice>) this.Document).Current.DocType),
              (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr,
              (object) ARDocType.GetDisplayName(reversingDocument.DocType),
              (object) reversingDocument.RefNbr
            });
        }
        ARInvoice current3 = ((PXSelectBase<ARInvoice>) this.Document).Current;
        int num4;
        if (current3 == null)
        {
          num4 = 0;
        }
        else
        {
          nullable = current3.PendingPPD;
          num4 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num4 != 0)
          throw new PXException("This document has been paid in full. To close the document, apply the cash discount by generating a credit memo on the Generate AR Tax Adjustments (AR504500) form. To reverse the document, click Reverse on the More menu.");
      }
    }
    return this.ReverseDocumentAndApplyToReversalIfNeeded(adapter, new ReverseInvoiceArgs()
    {
      ApplyToOriginalDocument = true
    });
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable PayInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.Released;
      if (nullable.GetValueOrDefault())
      {
        if (!(((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "INV"))
        {
          if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "PPI")
          {
            nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PendingPayment;
            if (!nullable.GetValueOrDefault())
              goto label_9;
          }
          else
            goto label_9;
        }
        ARAdjust creditMemo = (ARAdjust) null;
        if (this.AskUserApprovalIfUnreleasedCreditMemoAlreadyExists(((PXSelectBase<ARInvoice>) this.Document).Current, out creditMemo))
        {
          ((PXSelectBase<ARInvoice>) this.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) this.Document).Search<ARInvoice.refNbr>((object) creditMemo.AdjgRefNbr, new object[1]
          {
            (object) "CRM"
          }));
          PXRedirectHelper.TryRedirect((PXGraph) this, (PXRedirectHelper.WindowMode) 0);
        }
        else if (creditMemo != null)
          return adapter.Get();
label_9:
        ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
        int num;
        if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM")
        {
          nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PaymentsByLinesAllowed;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 0;
        bool flag1 = num != 0;
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.Payable;
        if (nullable.GetValueOrDefault() | flag1)
        {
          nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.OpenDoc;
          if (nullable.GetValueOrDefault())
          {
            nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PendingPPD;
            if (nullable.GetValueOrDefault())
              throw new PXSetPropertyException("This document has been paid in full. To close the document, apply the cash discount by generating a credit memo on the Generate AR Tax Adjustments (AR.50.45.00) form.");
            ARAdjust2 arAdjust2 = PXResultset<ARAdjust2>.op_Implicit(PXSelectBase<ARAdjust2, PXSelect<ARAdjust2, Where<ARAdjust2.adjdDocType, Equal<Current<ARInvoice.docType>>, And<ARAdjust2.adjdRefNbr, Equal<Current<ARInvoice.refNbr>>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.voided, Equal<False>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
            if (arAdjust2 != null)
            {
              ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) arAdjust2.AdjgRefNbr, new object[1]
              {
                (object) arAdjust2.AdjgDocType
              }));
            }
            else
            {
              if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "PPI")
              {
                nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PendingPayment;
                bool flag2 = false;
                if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
                {
                  ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr, new object[1]
                  {
                    (object) ((PXSelectBase<ARInvoice>) this.Document).Current.DocType
                  }));
                  goto label_22;
                }
              }
              instance.CreatePayment(((PXSelectBase<ARInvoice>) this.Document).Current, "PMT");
            }
label_22:
            throw new PXRedirectRequiredException((PXGraph) instance, nameof (PayInvoice));
          }
        }
        if (!(((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM"))
        {
          if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "PPI")
          {
            nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PendingPayment;
            if (nullable.GetValueOrDefault())
              goto label_27;
          }
          else
            goto label_27;
        }
        ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr, new object[1]
        {
          (object) ((PXSelectBase<ARInvoice>) this.Document).Current.DocType
        }));
        throw new PXRedirectRequiredException((PXGraph) instance, nameof (PayInvoice));
      }
    }
label_27:
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton(DisplayOnMainToolbar = false)]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable CreateSchedule(PXAdapter adapter)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if (current == null)
      return adapter.Get();
    ((PXAction) this.Save).Press();
    IsSchedulable<ARRegister>.Ensure((PXGraph) this, (ARRegister) current);
    ARScheduleMaint instance1 = PXGraph.CreateInstance<ARScheduleMaint>();
    if (!current.Scheduled.GetValueOrDefault() || current.ScheduleID == null)
    {
      ((PXSelectBase) instance1.Schedule_Header).Cache.Insert();
      ARRegister instance2 = ((PXSelectBase) instance1.Document_Detail).Cache.CreateInstance() as ARRegister;
      PXCache<ARRegister>.RestoreCopy(instance2, (ARRegister) current);
      ARRegister arRegister = ((PXSelectBase) instance1.Document_Detail).Cache.Update((object) instance2) as ARRegister;
      throw new PXRedirectRequiredException((PXGraph) instance1, "Create Schedule");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(CommitChanges = false)]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ViewScheduleOfCurrentDocument(PXAdapter adapter)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if (current == null)
      return adapter.Get();
    if (((PXGraph) this).IsDirty)
      ((PXAction) this.Save).Press();
    ARScheduleMaint instance = PXGraph.CreateInstance<ARScheduleMaint>();
    if (current.Scheduled.GetValueOrDefault() && current.ScheduleID != null)
    {
      ((PXSelectBase<PX.Objects.GL.Schedule>) instance.Schedule_Header).Current = PXResultset<PX.Objects.GL.Schedule>.op_Implicit(((PXSelectBase<PX.Objects.GL.Schedule>) instance.Schedule_Header).Search<PX.Objects.GL.Schedule.scheduleID>((object) current.ScheduleID, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, "View Schedule");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ReclassifyBatch(PXAdapter adapter)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if (current != null)
      ReclassifyTransactionsProcess.TryOpenForReclassificationOfDocument(((PXSelectBase) this.Document).View, "AR", current.BatchNbr, current.DocType, current.RefNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LoadDocuments(PXAdapter adapter)
  {
    this.LoadDocumentsProc();
    return adapter.Get();
  }

  /// <summary>Load not applied payments</summary>
  /// <returns></returns>
  public virtual PXResultset<ARAdjust2, ARPayment, ExternalTransaction> LoadDocumentsProc()
  {
    PXResultset<ARAdjust2, ARPayment, ExternalTransaction> pxResultset = new PXResultset<ARAdjust2, ARPayment, ExternalTransaction>();
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null && (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "INV" || ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "DRM"))
    {
      bool? nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.Scheduled;
        if (!nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.Voided;
          if (!nullable.GetValueOrDefault())
          {
            using (new ReadOnlyScope(new PXCache[4]
            {
              ((PXSelectBase) this.Adjustments).Cache,
              ((PXSelectBase) this.Document).Cache,
              ((PXSelectBase) this.arbalances).Cache,
              ((PXSelectBase) this.PaymentTotalsUpd).Cache
            }))
            {
              PXSelectReadonly2<ARRegisterAlias, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARRegisterAlias.refNbr>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.voided, Equal<False>, And<Where<ARAdjust2.adjdDocType, NotEqual<Current<ARInvoice.docType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Current<ARInvoice.refNbr>>>>>>>>>, LeftJoin<SOAdjust, On<SOAdjust.adjgDocType, Equal<ARRegisterAlias.docType>, And<SOAdjust.adjgRefNbr, Equal<ARRegisterAlias.refNbr>, And<SOAdjust.adjAmt, Greater<decimal0>>>>, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>, LeftJoin<ARPaymentTotals, On<ARPaymentTotals.docType, Equal<ARRegisterAlias.docType>, And<ARPaymentTotals.refNbr, Equal<ARRegisterAlias.refNbr>>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<ARPayment.cCActualExternalTransactionID>>>>>>>>, Where2<Where<ARRegisterAlias.customerID, Equal<Current<ARInvoice.customerID>>, Or<ARRegisterAlias.customerID, Equal<Current<Customer.consolidatingBAccountID>>>>, And2<Where<ARRegisterAlias.docType, Equal<ARDocType.payment>, Or<ARRegisterAlias.docType, Equal<ARDocType.prepayment>, Or<ARRegisterAlias.docType, Equal<ARDocType.creditMemo>, Or<ARRegisterAlias.docType, Equal<ARDocType.prepaymentInvoice>>>>>, And<ARRegisterAlias.docDate, LessEqual<Current<ARInvoice.docDate>>, And<ARRegisterAlias.tranPeriodID, LessEqual<Current<ARInvoice.tranPeriodID>>, And<ARRegisterAlias.released, Equal<boolTrue>, And<ARRegisterAlias.openDoc, Equal<boolTrue>, And<ARRegisterAlias.pendingPayment, Equal<False>, And<ARRegisterAlias.hold, Equal<False>, And<ARRegisterAlias.paymentsByLinesAllowed, Equal<False>, And<ARAdjust2.adjdRefNbr, IsNull, And<SOAdjust.adjgRefNbr, IsNull, And<Not<HasUnreleasedVoidPayment<ARPayment.docType, ARPayment.refNbr>>>>>>>>>>>>>>> pxSelectReadonly2 = new PXSelectReadonly2<ARRegisterAlias, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARRegisterAlias.refNbr>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.voided, Equal<False>, And<Where<ARAdjust2.adjdDocType, NotEqual<Current<ARInvoice.docType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Current<ARInvoice.refNbr>>>>>>>>>, LeftJoin<SOAdjust, On<SOAdjust.adjgDocType, Equal<ARRegisterAlias.docType>, And<SOAdjust.adjgRefNbr, Equal<ARRegisterAlias.refNbr>, And<SOAdjust.adjAmt, Greater<decimal0>>>>, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>, LeftJoin<ARPaymentTotals, On<ARPaymentTotals.docType, Equal<ARRegisterAlias.docType>, And<ARPaymentTotals.refNbr, Equal<ARRegisterAlias.refNbr>>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<ARPayment.cCActualExternalTransactionID>>>>>>>>, Where2<Where<ARRegisterAlias.customerID, Equal<Current<ARInvoice.customerID>>, Or<ARRegisterAlias.customerID, Equal<Current<Customer.consolidatingBAccountID>>>>, And2<Where<ARRegisterAlias.docType, Equal<ARDocType.payment>, Or<ARRegisterAlias.docType, Equal<ARDocType.prepayment>, Or<ARRegisterAlias.docType, Equal<ARDocType.creditMemo>, Or<ARRegisterAlias.docType, Equal<ARDocType.prepaymentInvoice>>>>>, And<ARRegisterAlias.docDate, LessEqual<Current<ARInvoice.docDate>>, And<ARRegisterAlias.tranPeriodID, LessEqual<Current<ARInvoice.tranPeriodID>>, And<ARRegisterAlias.released, Equal<boolTrue>, And<ARRegisterAlias.openDoc, Equal<boolTrue>, And<ARRegisterAlias.pendingPayment, Equal<False>, And<ARRegisterAlias.hold, Equal<False>, And<ARRegisterAlias.paymentsByLinesAllowed, Equal<False>, And<ARAdjust2.adjdRefNbr, IsNull, And<SOAdjust.adjgRefNbr, IsNull, And<Not<HasUnreleasedVoidPayment<ARPayment.docType, ARPayment.refNbr>>>>>>>>>>>>>>>((PXGraph) this);
              List<object> objectList = (List<object>) null;
              if (!PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID.HasValue)
              {
                ((PXSelectBase<ARRegisterAlias>) pxSelectReadonly2).WhereAnd<Where<ARRegisterAlias.branchID, In<Required<ARInvoice.branchID>>>>();
                objectList = new List<object>();
                objectList.Add((object) BranchHelper.GetBranchesToApplyDocuments((PXGraph) this, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID));
              }
              foreach (PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction> pxResult in ((PXSelectBase<ARRegisterAlias>) pxSelectReadonly2).Select(new object[1]
              {
                (object) objectList
              }))
              {
                ARPayment payment = PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult);
                PX.Objects.CM.Extensions.CurrencyInfo info = PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult);
                ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().StoreResult(info);
                PXCache<ARRegister>.RestoreCopy((ARRegister) payment, (ARRegister) PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult));
                ARAdjust2 arAdjust2_1 = new ARAdjust2();
                arAdjust2_1.AdjdDocType = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType;
                arAdjust2_1.AdjdRefNbr = ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr;
                arAdjust2_1.AdjgDocType = payment.DocType;
                arAdjust2_1.AdjgRefNbr = payment.RefNbr;
                arAdjust2_1.InvoiceID = ((PXSelectBase<ARInvoice>) this.Document).Current.NoteID;
                arAdjust2_1.PaymentID = payment.DocType != "CRM" ? payment.NoteID : new Guid?();
                arAdjust2_1.MemoID = payment.DocType == "CRM" ? payment.NoteID : new Guid?();
                arAdjust2_1.AdjNbr = payment.AdjCntr;
                arAdjust2_1.CustomerID = payment.CustomerID;
                arAdjust2_1.AdjdCustomerID = ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID;
                arAdjust2_1.AdjdBranchID = ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID;
                arAdjust2_1.AdjgBranchID = payment.BranchID;
                arAdjust2_1.AdjgCuryInfoID = payment.CuryInfoID;
                arAdjust2_1.AdjdOrigCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
                arAdjust2_1.AdjdCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
                arAdjust2_1.AdjdFinPeriodID = ((PXSelectBase<ARInvoice>) this.Document).Current.FinPeriodID;
                arAdjust2_1.AdjgFinPeriodID = payment.FinPeriodID;
                ARAdjust2 arAdjust2_2 = arAdjust2_1;
                PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.docType, Equal<Current<ARAdjust2.adjgDocType>>, And<ARPayment.refNbr, Equal<Current<ARAdjust2.adjgRefNbr>>>>>.Config>.StoreResult((PXGraph) this, new List<object>()
                {
                  (object) new PXResult<ARPayment>(PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult))
                }, PXQueryParameters.ExplicitParameters(new object[2]
                {
                  (object) arAdjust2_2.AdjgDocType,
                  (object) arAdjust2_2.AdjgRefNbr
                }));
                ARPaymentTotals arPaymentTotals1 = PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult);
                if (!string.IsNullOrEmpty(arPaymentTotals1.DocType))
                  PXSelectBase<ARPaymentTotals, PXSelect<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<ARAdjust2.adjgDocType>>, And<ARPaymentTotals.refNbr, Equal<Current<ARAdjust2.adjgRefNbr>>>>>.Config>.StoreResult((PXGraph) this, new List<object>()
                  {
                    (object) new PXResult<ARPaymentTotals>(arPaymentTotals1)
                  }, PXQueryParameters.ExplicitParameters(new object[2]
                  {
                    (object) arAdjust2_2.AdjgDocType,
                    (object) arAdjust2_2.AdjgRefNbr
                  }));
                if (((PXSelectBase) this.Adjustments).Cache.Locate((object) arAdjust2_2) == null || ((PXSelectBase) this.Adjustments).Cache.GetStatus((object) arAdjust2_2) == 4)
                {
                  ARPaymentTotals arPaymentTotals2 = PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult);
                  GraphHelper.Hold((PXCache) GraphHelper.Caches<ARPayment>((PXGraph) this), (object) payment);
                  PXParentAttribute.SetParent(((PXSelectBase) this.Adjustments).Cache, (object) arAdjust2_2, typeof (ARPayment), (object) payment);
                  if (arPaymentTotals2.RefNbr != null)
                  {
                    GraphHelper.Hold((PXCache) GraphHelper.Caches<ARPaymentTotals>((PXGraph) this), (object) arPaymentTotals2);
                    PXParentAttribute.SetParent(((PXSelectBase) this.Adjustments).Cache, (object) arAdjust2_2, typeof (ARPaymentTotals), (object) arPaymentTotals2);
                  }
                  PXSelectorAttribute.StoreCached<ARAdjust2.adjgRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) arAdjust2_2, (object) PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult));
                  ARAdjust2 adj = ((PXSelectBase<ARAdjust2>) this.Adjustments).Insert(arAdjust2_2);
                  if (adj == null)
                  {
                    ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust2.curyDocBal>((object) adj, (object) 0M, (Exception) new NullReferenceException());
                  }
                  else
                  {
                    try
                    {
                      new ARInvoiceBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>(), (PXGraph) this).InitBalancesFromInvoiceSide((ARAdjust) adj, (IInvoice) ((PXSelectBase<ARInvoice>) this.Document).Current, payment);
                    }
                    catch (Exception ex)
                    {
                      ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust2.curyDocBal>((object) adj, (object) 0M, ex);
                    }
                  }
                  if (adj != null)
                    ((PXResultset<ARAdjust2>) pxResultset).Add((PXResult<ARAdjust2>) new PXResult<ARAdjust2, ARPayment, ExternalTransaction>(adj, payment, PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARAdjust2, SOAdjust, ARPayment, ARPaymentTotals, ExternalTransaction>.op_Implicit(pxResult)));
                }
              }
            }
          }
        }
      }
    }
    return pxResultset;
  }

  [PXUIField]
  [PXLookupButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AutoApply(PXAdapter adapter)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      bool? released = ((PXSelectBase<ARInvoice>) this.Document).Current.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue && (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "INV" || ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "DRM"))
      {
        foreach (PXResult<ARAdjust2> pxResult in this.SelectAdjustmentsRaw())
        {
          ARAdjust2 adj = PXResult<ARAdjust2>.op_Implicit(pxResult);
          if (adj != null)
          {
            Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
            Decimal num = 0M;
            if (!(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue))
              this.UpdateARAdjustCuryAdjdAmt(adj, new Decimal?(0M));
          }
        }
        Decimal? val = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDocBal;
        foreach (ARAdjust2 adj in GraphHelper.RowCast<ARAdjust2>(((PXSelectBase) this.Adjustments).View.SelectExternal()).Where<ARAdjust2>((Func<ARAdjust2, bool>) (adj =>
        {
          Decimal? curyDocBal = adj.CuryDocBal;
          Decimal num = 0M;
          return curyDocBal.GetValueOrDefault() > num & curyDocBal.HasValue;
        })))
        {
          Decimal? curyDocBal = adj.CuryDocBal;
          Decimal? nullable = val;
          if (curyDocBal.GetValueOrDefault() >= nullable.GetValueOrDefault() & curyDocBal.HasValue & nullable.HasValue)
          {
            this.UpdateARAdjustCuryAdjdAmt(adj, val);
            break;
          }
          nullable = val;
          curyDocBal = adj.CuryDocBal;
          val = nullable.HasValue & curyDocBal.HasValue ? new Decimal?(nullable.GetValueOrDefault() - curyDocBal.GetValueOrDefault()) : new Decimal?();
          this.UpdateARAdjustCuryAdjdAmt(adj, adj.CuryDocBal);
        }
        ((PXSelectBase) this.Adjustments).View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  protected virtual ARAdjust2 UpdateARAdjustCuryAdjdAmt(ARAdjust2 adj, Decimal? val)
  {
    ARAdjust2 copy = (ARAdjust2) ((PXSelectBase) this.Adjustments).Cache.CreateCopy((object) adj);
    copy.CuryAdjdAmt = val;
    return ((PXSelectBase<ARAdjust2>) this.Adjustments).Update(copy);
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewPayment(PXAdapter adapter)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null && ((PXSelectBase<ARAdjust2>) this.Adjustments).Current != null)
    {
      ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
      ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) ((PXSelectBase<ARAdjust2>) this.Adjustments).Current.AdjgRefNbr, new object[1]
      {
        (object) ((PXSelectBase<ARAdjust2>) this.Adjustments).Current.AdjgDocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Payment");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null || ((PXSelectBase<ARAdjust>) this.Adjustments_1).Current == null)
      return adapter.Get();
    if (ARDocType.Payable(((PXSelectBase<ARAdjust>) this.Adjustments_1).Current.DisplayDocType).GetValueOrDefault())
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) ((PXSelectBase<ARAdjust>) this.Adjustments_1).Current.AdjdRefNbr, new object[1]
      {
        (object) ((PXSelectBase<ARAdjust>) this.Adjustments_1).Current.AdjdDocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Invoice");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    ARPaymentEntry instance1 = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<ARPayment>) instance1.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance1.Document).Search<ARPayment.refNbr>((object) ((PXSelectBase<ARAdjust>) this.Adjustments_1).Current.AdjgRefNbr, new object[1]
    {
      (object) ((PXSelectBase<ARAdjust>) this.Adjustments_1).Current.AdjgDocType
    }));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Payment");
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewItem(PXAdapter adapter)
  {
    if (((PXSelectBase<ARTran>) this.Transactions).Current != null)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARTran.inventoryID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
      if (inventoryItem != null)
        PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, "View Item", (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    ARInvoiceEntry arInvoiceEntry = this;
    foreach (ARInvoice arInvoice in adapter.Get<ARInvoice>())
    {
      if (arInvoice != null)
        ((PXGraph) arInvoiceEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) arInvoice;
    }
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable RecalculateDiscountsAction(PXAdapter adapter)
  {
    if (adapter.MassProcess || ((PXGraph) this).IsContractBasedAPI)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRecalculateDiscountsAction\u003Eb__103_0)));
    }
    else if (!adapter.ExternalCall || ((PXGraph) this).IsExport || ((PXGraph) this).IsImport)
      this.RecalculateDiscountsProc(true);
    else if (((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).AskExt() == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ARInvoiceEntry.\u003C\u003Ec__DisplayClass103_0 displayClass1030 = new ARInvoiceEntry.\u003C\u003Ec__DisplayClass103_0();
      // ISSUE: reference to a compiler-generated field
      displayClass1030.clone = GraphHelper.Clone<ARInvoiceEntry>(this);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase) displayClass1030.clone.Document).Cache.RaiseRowSelected(((PXSelectBase) displayClass1030.clone.Document).Cache.Current);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1030, __methodptr(\u003CRecalculateDiscountsAction\u003Eb__1)));
    }
    if (((PXGraph) this).IsContractBasedAPI)
      PXLongOperation.WaitCompletion(((PXGraph) this).UID);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Recalculate Prices and Discounts on Import", Visible = true)]
  [PXButton(DisplayOnMainToolbar = false)]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public void recalculatePricesAndDiscountsFromImport()
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null)
      return;
    this.ARDiscountEngine.RecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, ((PXSelectBase<ARTran>) this.Transactions).Current, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current) | DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport);
    ((PXAction) this.Save).Press();
  }

  protected virtual void RecalculateDiscountsProc(bool redirect)
  {
    this.ARDiscountEngine.RecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, ((PXSelectBase<ARTran>) this.Transactions).Current, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current));
    if (redirect)
      PXLongOperation.SetCustomInfo((object) this);
    else
      ((PXAction) this.Save).Press();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RecalcOk(PXAdapter adapter) => adapter.Get();

  public virtual void ClearRetainageSummary(ARInvoice document)
  {
    document.CuryLineRetainageTotal = new Decimal?(0M);
    document.CuryRetainageTotal = new Decimal?(0M);
    document.CuryRetainageReleased = new Decimal?(0M);
    document.CuryRetainageUnreleasedAmt = new Decimal?(0M);
    document.CuryRetainageUnpaidTotal = new Decimal?(0M);
    document.CuryRetainedTaxTotal = new Decimal?(0M);
    document.CuryRetainedDiscTotal = new Decimal?(0M);
  }

  /// <summary>Check if reversing retainage document already exists.</summary>
  public virtual bool CheckReversingRetainageDocumentAlreadyExists(
    ARInvoice origDoc,
    out ARRegister reversingDoc)
  {
    reversingDoc = PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.origDocType, Equal<Required<ARRegister.origDocType>>, And<ARRegister.origRefNbr, Equal<Required<ARRegister.origRefNbr>>>>>, OrderBy<Desc<ARRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) this.GetReversingDocType(origDoc.DocType),
      (object) origDoc.DocType,
      (object) origDoc.RefNbr
    }));
    if (reversingDoc == null)
      return false;
    return reversingDoc.IsOriginalRetainageDocument() == origDoc.IsOriginalRetainageDocument() || reversingDoc.IsChildRetainageDocument() == origDoc.IsChildRetainageDocument();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable CustomerRefund(PXAdapter adapter)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.Released;
      if (nullable.Value)
      {
        if (!(((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM"))
        {
          if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "PPI")
          {
            nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.PendingPayment;
            bool flag = false;
            if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
              goto label_10;
          }
          else
            goto label_10;
        }
        nullable = ((PXSelectBase<ARInvoice>) this.Document).Current.OpenDoc;
        if (nullable.Value)
        {
          ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
          ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelectJoin<ARAdjust, InnerJoin<ARRegisterAlias, On<ARAdjust.adjgDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust.adjgRefNbr, Equal<ARRegisterAlias.refNbr>>>>, Where<ARAdjust.adjdDocType, Equal<Current<ARInvoice.docType>>, And<ARAdjust.adjdRefNbr, Equal<Current<ARInvoice.refNbr>>, And<ARAdjust.released, NotEqual<True>, And<Not<ARAdjust.voided, Equal<True>, And<ARRegisterAlias.voided, Equal<True>, And<Where<ARRegisterAlias.docType, Equal<ARDocType.payment>, Or<ARRegisterAlias.docType, Equal<ARDocType.prepayment>>>>>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
          if (arAdjust != null)
          {
            ((PXSelectBase<ARPayment>) instance.Document).Current = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) arAdjust.AdjgRefNbr, new object[1]
            {
              (object) arAdjust.AdjgDocType
            }));
          }
          else
          {
            this.VerifyCanCreateNewRefundForDocument((ARRegister) ((PXSelectBase<ARInvoice>) this.Document).Current);
            ((PXGraph) instance).Clear();
            instance.CreatePayment(((PXSelectBase<ARInvoice>) this.Document).Current, "REF");
          }
          throw new PXRedirectRequiredException((PXGraph) instance, nameof (CustomerRefund));
        }
      }
    }
label_10:
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Credit Hold")]
  protected virtual IEnumerable PutOnCreditHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Credit Hold")]
  protected virtual IEnumerable ReleaseFromCreditHold(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField]
  [ARMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable EmailInvoice(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "Invoice");
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable SendEmail(PXAdapter adapter)
  {
    return ((PXAction) ((PXGraph) this).GetExtension<ARInvoiceEntry_ActivityDetailsExt>().NewMailActivity).Press(adapter);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintInvoice(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AR641000");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Reject(PXAdapter adapter) => adapter.Get();

  private object disableJoined(object val)
  {
    if (val is PXFieldState pxFieldState)
      pxFieldState.Enabled = false;
    return val;
  }

  public virtual object GetStateExt(string viewName, object data, string fieldName)
  {
    if (!(viewName == "Adjustments"))
      return ((PXGraph) this).GetStateExt(viewName, data, fieldName);
    if (data != null)
      return ((PXGraph) this).GetStateExt(viewName, data, fieldName);
    int length = fieldName.IndexOf("__");
    if (length <= 0 || length >= fieldName.Length - 2)
      return ((PXGraph) this).Caches[((PXGraph) this).GetItemType(viewName)].GetStateExt((object) null, fieldName);
    string str = fieldName.Substring(0, length);
    PXCache pxCache = (PXCache) null;
    foreach (System.Type itemType in ((PXGraph) this).Views[viewName].GetItemTypes())
    {
      if (itemType.Name == str)
        pxCache = ((PXGraph) this).Caches[itemType];
    }
    if (pxCache == null)
      pxCache = ((PXGraph) this).Caches[str];
    return pxCache != null ? this.disableJoined(pxCache.GetStateExt((object) null, fieldName.Substring(length + 2))) : (object) null;
  }

  private bool AppendAdjustmentsRawTail(ARAdjust adj, PXResult<PX.Objects.AR.Standalone.ARPayment> paymentResult)
  {
    ((PXSelectBase<ARAdjust2>) this.Adjustments_Inv).StoreTailResult((PXResult) paymentResult, (object[]) new ARAdjust[1]
    {
      adj
    }, new object[1]
    {
      (object) ((PXSelectBase<ARInvoice>) this.Document).Current.NoteID
    });
    return true;
  }

  protected virtual IEnumerable<PXResult<ARAdjust2>> SelectAdjustmentsRaw(bool withinViewContext = false)
  {
    ARInvoiceEntry graph = this;
    if (((PXSelectBase) graph.Adjustments_Inv).Cache.Cached.Count() > 1L)
      ((PXSelectBase) graph.Adjustments_Inv).Cache.Cached.OfType<ARAdjust>().Join<ARAdjust, PXResult<PX.Objects.AR.Standalone.ARPayment>, string, bool>((IEnumerable<PXResult<PX.Objects.AR.Standalone.ARPayment>>) ((PXSelectBase<PX.Objects.AR.Standalone.ARPayment>) graph.AvailablePayments).Select((object[]) new string[1][]
      {
        ((PXSelectBase) graph.Adjustments_Inv).Cache.Cached.Cast<ARAdjust2>().Select<ARAdjust2, string>((Func<ARAdjust2, string>) (_ => _.AdjgRefNbr)).ToArray<string>()
      }), (Func<ARAdjust, string>) (_ => _.AdjgDocType + _.AdjgRefNbr), (Func<PXResult<PX.Objects.AR.Standalone.ARPayment>, string>) (_ => ((PXResult) _).GetItem<PX.Objects.AR.Standalone.ARPayment>().DocType + ((PXResult) _).GetItem<PX.Objects.AR.Standalone.ARPayment>().RefNbr), new Func<ARAdjust, PXResult<PX.Objects.AR.Standalone.ARPayment>, bool>(graph.AppendAdjustmentsRawTail)).ToArray<bool>();
    foreach (PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction> pxResult in withinViewContext ? (IEnumerable) ((PXSelectBase<ARAdjust2>) graph.Adjustments_Inv).SelectWithViewContext(Array.Empty<object>()) : (IEnumerable) ((PXSelectBase<ARAdjust2>) graph.Adjustments_Inv).Select(Array.Empty<object>()))
    {
      ARAdjust2 arAdjust2 = PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
      PX.Objects.AR.Standalone.ARPayment source = PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
      ARPayment arPayment = PX.Objects.Common.Utilities.Clone<PX.Objects.AR.Standalone.ARPayment, ARPayment>((PXGraph) graph, source);
      yield return (PXResult<ARAdjust2>) new PXResult<ARAdjust2, ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>(arAdjust2, arPayment, PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult), PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult), PXResult<ARAdjust2, PX.Objects.AR.Standalone.ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult));
    }
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public virtual IEnumerable transactions()
  {
    this.PrefetchWithDetails();
    return (IEnumerable) null;
  }

  public virtual void PrefetchWithDetails()
  {
  }

  public virtual IEnumerable taxes()
  {
    bool hasPPDTaxes = false;
    bool vatReportingInstalled = PXAccess.FeatureInstalled<FeaturesSet.vATReporting>();
    ARTaxTran artaxMax = (ARTaxTran) null;
    Decimal? DiscountedTaxableTotal = new Decimal?(0M);
    Decimal? DiscountedPriceTotal = new Decimal?(0M);
    foreach (PXResult<ARTaxTran, PX.Objects.TX.Tax> pxResult in ((PXSelectBase<ARTaxTran>) this.TaxesList).Select(Array.Empty<object>()))
    {
      if (vatReportingInstalled)
      {
        PX.Objects.TX.Tax tax = PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
        ARTaxTran artax = PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
        hasPPDTaxes = tax.TaxApplyTermsDisc == "P" | hasPPDTaxes;
        if (hasPPDTaxes && ((PXSelectBase<ARInvoice>) this.Document).Current != null)
        {
          Decimal? nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
          if (nullable1.HasValue)
          {
            nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
            Decimal num1 = 0M;
            if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
            {
              nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDiscAmt;
              if (nullable1.HasValue)
              {
                nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDiscAmt;
                Decimal? nullable2 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
                Decimal cashDiscPercent = (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?()).Value;
                int num2 = ARPPDCreditMemoProcess.CalculateDiscountedTaxes(((PXSelectBase) this.Taxes).Cache, artax, cashDiscPercent) ? 1 : 0;
                nullable1 = DiscountedPriceTotal;
                nullable2 = artax.CuryDiscountedPrice;
                DiscountedPriceTotal = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                if (num2 != 0)
                {
                  nullable2 = DiscountedTaxableTotal;
                  nullable1 = artax.CuryDiscountedTaxableAmt;
                  DiscountedTaxableTotal = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
                  if (artaxMax != null)
                  {
                    nullable1 = artax.CuryDiscountedTaxableAmt;
                    nullable2 = artaxMax.CuryDiscountedTaxableAmt;
                    if (!(nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
                      goto label_12;
                  }
                  artaxMax = artax;
                }
              }
            }
          }
        }
      }
label_12:
      yield return (object) pxResult;
    }
    if (vatReportingInstalled && ((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      ((PXSelectBase<ARInvoice>) this.Document).Current.HasPPDTaxes = new bool?(hasPPDTaxes);
      if (hasPPDTaxes)
      {
        Decimal? nullable3 = DiscountedTaxableTotal;
        Decimal? nullable4 = DiscountedPriceTotal;
        Decimal? nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
        nullable4 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
        Decimal? nullable6 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDiscAmt;
        Decimal? nullable7 = nullable4.HasValue & nullable6.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
        current.CuryDiscountedDocTotal = nullable7;
        if (artaxMax != null)
        {
          Decimal? nullable8 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryVatTaxableTotal;
          Decimal? nullable9 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryTaxTotal;
          nullable6 = nullable8.HasValue & nullable9.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
          nullable4 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
          if (nullable6.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable6.HasValue == nullable4.HasValue)
          {
            nullable4 = nullable5;
            nullable6 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscountedDocTotal;
            if (!(nullable4.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable4.HasValue == nullable6.HasValue))
            {
              ARTaxTran arTaxTran = artaxMax;
              nullable6 = arTaxTran.CuryDiscountedTaxableAmt;
              nullable9 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscountedDocTotal;
              nullable8 = nullable5;
              nullable4 = nullable9.HasValue & nullable8.HasValue ? new Decimal?(nullable9.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable10;
              if (!(nullable6.HasValue & nullable4.HasValue))
              {
                nullable8 = new Decimal?();
                nullable10 = nullable8;
              }
              else
                nullable10 = new Decimal?(nullable6.GetValueOrDefault() + nullable4.GetValueOrDefault());
              arTaxTran.CuryDiscountedTaxableAmt = nullable10;
              nullable4 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscountedDocTotal;
              nullable6 = DiscountedPriceTotal;
              Decimal? nullable11;
              if (!(nullable4.HasValue & nullable6.HasValue))
              {
                nullable8 = new Decimal?();
                nullable11 = nullable8;
              }
              else
                nullable11 = new Decimal?(nullable4.GetValueOrDefault() - nullable6.GetValueOrDefault());
              DiscountedTaxableTotal = nullable11;
            }
          }
        }
        ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscountedPrice = DiscountedPriceTotal;
        ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscountedTaxableTotal = DiscountedTaxableTotal;
      }
    }
  }

  public virtual string GetReversingDocType(string docType)
  {
    switch (docType)
    {
      case "INV":
      case "PPI":
      case "DRM":
        docType = "CRM";
        break;
      case "CRM":
        docType = "DRM";
        break;
    }
    return docType;
  }

  protected virtual ARInvoice CreateReversalARInvoice(ARInvoice doc, ReverseInvoiceArgs reverseArgs)
  {
    ARInvoice copy = PXCache<ARInvoice>.CreateCopy(doc);
    copy.DocType = reverseArgs.PreserveOriginalDocumentSign ? copy.DocType : this.GetReversingDocType(copy.DocType);
    PX.Objects.AR.ARSetup current1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      bool? migrationMode = current1.MigrationMode;
      bool flag = false;
      num1 = migrationMode.GetValueOrDefault() == flag & migrationMode.HasValue ? 1 : 0;
    }
    if (num1 != 0)
    {
      copy.InitDocBal = new Decimal?(0M);
      copy.CuryInitDocBal = new Decimal?(0M);
    }
    copy.ProformaExists = new bool?(false);
    copy.OrigModule = (string) null;
    copy.RefNbr = (string) null;
    copy.OpenDoc = new bool?(true);
    copy.Released = new bool?(false);
    ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARInvoice.isMigratedRecord>((object) copy);
    copy.Printed = new bool?(false);
    copy.Emailed = new bool?(false);
    copy.BatchNbr = (string) null;
    copy.ScheduleID = (string) null;
    copy.Scheduled = new bool?(false);
    copy.NoteID = new Guid?();
    copy.RefNoteID = new Guid?();
    copy.InstallmentCntr = new short?();
    copy.InstallmentNbr = new short?();
    PX.Objects.AR.ARSetup current2 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      bool? termsInCreditMemos = current2.TermsInCreditMemos;
      bool flag = false;
      num2 = termsInCreditMemos.GetValueOrDefault() == flag & termsInCreditMemos.HasValue ? 1 : 0;
    }
    if (num2 != 0)
    {
      copy.TermsID = (string) null;
      copy.DueDate = new DateTime?();
      copy.DiscDate = new DateTime?();
      copy.CuryOrigDiscAmt = new Decimal?(0M);
    }
    copy.CuryDetailExtPriceTotal = new Decimal?(0M);
    copy.DetailExtPriceTotal = new Decimal?(0M);
    copy.CuryLineDiscTotal = new Decimal?(0M);
    copy.LineDiscTotal = new Decimal?(0M);
    copy.CuryMiscExtPriceTotal = new Decimal?(0M);
    copy.MiscExtPriceTotal = new Decimal?(0M);
    copy.CuryGoodsExtPriceTotal = new Decimal?(0M);
    copy.GoodsExtPriceTotal = new Decimal?(0M);
    bool? nullable1;
    if (doc.IsPrepaymentInvoiceDocument())
    {
      nullable1 = doc.PendingPayment;
      if (nullable1.GetValueOrDefault())
      {
        copy.CuryOrigDocAmt = doc.CuryDocBal;
        copy.DisableAutomaticDiscountCalculation = new bool?(true);
        copy.PendingPayment = new bool?(false);
        goto label_14;
      }
    }
    copy.CuryDocBal = doc.CuryOrigDocAmt;
label_14:
    copy.OrigDocDate = doc.DocDate;
    ARInvoice arInvoice1 = copy;
    nullable1 = new bool?();
    bool? nullable2 = nullable1;
    arInvoice1.Approved = nullable2;
    ARInvoice arInvoice2 = copy;
    nullable1 = new bool?();
    bool? nullable3 = nullable1;
    arInvoice2.DontApprove = nullable3;
    switch (reverseArgs.DateOption)
    {
      case ReverseInvoiceArgs.CopyOption.SetOriginal:
        FinPeriodIDAttribute.SetPeriodsByMaster<ARInvoice.finPeriodID>(((PXSelectBase) this.CurrentDocument).Cache, (object) copy, doc.FinPeriodID);
        break;
      case ReverseInvoiceArgs.CopyOption.SetDefault:
        copy.DocDate = ((PXGraph) this).Accessinfo.BusinessDate;
        copy.TranPeriodID = (string) null;
        copy.FinPeriodID = (string) null;
        break;
      case ReverseInvoiceArgs.CopyOption.Override:
        copy.DocDate = reverseArgs.DocumentDate;
        copy.TranPeriodID = (string) null;
        copy.FinPeriodID = reverseArgs.DocumentFinPeriodID;
        break;
    }
    copy.OrigDocType = doc.DocType;
    copy.OrigRefNbr = doc.RefNbr;
    copy.CuryLineTotal = new Decimal?(0M);
    copy.CuryGoodsTotal = new Decimal?(0M);
    copy.CuryGoodsExtPriceTotal = new Decimal?(0M);
    copy.CuryMiscTot = new Decimal?(0M);
    copy.CuryMiscExtPriceTotal = new Decimal?(0M);
    copy.CuryLineDiscTotal = new Decimal?(0M);
    copy.CuryFreightCost = new Decimal?(0M);
    copy.CuryFreightAmt = new Decimal?(0M);
    copy.CuryPremiumFreightAmt = new Decimal?(0M);
    copy.CuryFreightTot = new Decimal?(0M);
    copy.CuryCommnblAmt = new Decimal?(0M);
    copy.CuryCommnAmt = new Decimal?(0M);
    copy.CuryTaxTotal = new Decimal?(0M);
    copy.CuryPaymentTotal = new Decimal?(0M);
    copy.IsTaxPosted = new bool?(false);
    copy.IsTaxValid = new bool?(false);
    copy.CuryVatTaxableTotal = new Decimal?(0M);
    copy.CuryVatExemptTotal = new Decimal?(0M);
    copy.StatementDate = new DateTime?();
    ARInvoice arInvoice3 = copy;
    nullable1 = reverseArgs.OverrideDocumentHold;
    bool? nullable4 = new bool?(((int) nullable1 ?? (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.HoldEntry.GetValueOrDefault() ? 1 : (this.IsApprovalRequired(copy) ? 1 : 0))) != 0);
    arInvoice3.Hold = nullable4;
    copy.PendingPPD = new bool?(false);
    copy.IsCancellation = new bool?(false);
    copy.IsCorrection = new bool?(false);
    copy.IsUnderCorrection = new bool?(false);
    copy.Canceled = new bool?(false);
    ARInvoice arInvoice4 = copy;
    int num3;
    if (!doc.IsOriginalRetainageDocument())
    {
      nullable1 = doc.IsRetainageDocument;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 1;
    bool? nullable5 = new bool?(num3 != 0);
    arInvoice4.IsRetainageReversing = nullable5;
    if (!string.IsNullOrEmpty(reverseArgs.OverrideDocumentDescr))
      copy.DocDesc = reverseArgs.OverrideDocumentDescr;
    int? nullable6;
    if (!string.IsNullOrEmpty(copy.PaymentMethodID))
    {
      nullable6 = copy.CashAccountID;
      if (nullable6.HasValue)
      {
        PXResult<PX.Objects.CA.PaymentMethod, PaymentMethodAccount> pxResult = (PXResult<PX.Objects.CA.PaymentMethod, PaymentMethodAccount>) PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelectJoin<PX.Objects.CA.PaymentMethod, LeftJoin<PaymentMethodAccount, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>>>, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>, And<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) copy.PaymentMethodID,
          (object) copy.CashAccountID
        }));
        PX.Objects.CA.PaymentMethod paymentMethod = PXResult<PX.Objects.CA.PaymentMethod, PaymentMethodAccount>.op_Implicit(pxResult);
        PaymentMethodAccount paymentMethodAccount = PXResult<PX.Objects.CA.PaymentMethod, PaymentMethodAccount>.op_Implicit(pxResult);
        if (paymentMethod != null)
        {
          nullable1 = paymentMethod.UseForAR;
          bool flag1 = false;
          if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
          {
            nullable1 = paymentMethod.IsActive;
            bool flag2 = false;
            if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
            {
              if (paymentMethodAccount != null)
              {
                nullable6 = paymentMethodAccount.CashAccountID;
                if (nullable6.HasValue)
                {
                  nullable1 = paymentMethodAccount.UseForAR;
                  if (nullable1.GetValueOrDefault())
                    goto label_37;
                }
              }
              ARInvoice arInvoice5 = copy;
              nullable6 = new int?();
              int? nullable7 = nullable6;
              arInvoice5.CashAccountID = nullable7;
              goto label_37;
            }
          }
        }
        copy.PaymentMethodID = (string) null;
        ARInvoice arInvoice6 = copy;
        nullable6 = new int?();
        int? nullable8 = nullable6;
        arInvoice6.CashAccountID = nullable8;
      }
      else
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) copy.PaymentMethodID
        }));
        if (paymentMethod != null)
        {
          nullable1 = paymentMethod.UseForAR;
          bool flag3 = false;
          if (!(nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue))
          {
            nullable1 = paymentMethod.IsActive;
            bool flag4 = false;
            if (!(nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue))
              goto label_37;
          }
        }
        copy.PaymentMethodID = (string) null;
        ARInvoice arInvoice7 = copy;
        nullable6 = new int?();
        int? nullable9 = nullable6;
        arInvoice7.CashAccountID = nullable9;
        ARInvoice arInvoice8 = copy;
        nullable6 = new int?();
        int? nullable10 = nullable6;
        arInvoice8.PMInstanceID = nullable10;
      }
label_37:
      nullable6 = copy.PMInstanceID;
      if (nullable6.HasValue)
      {
        CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) copy.PMInstanceID
        }));
        if (!string.IsNullOrEmpty(copy.PaymentMethodID) && customerPaymentMethod != null)
        {
          nullable1 = customerPaymentMethod.IsActive;
          bool flag = false;
          if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue) && !(customerPaymentMethod.PaymentMethodID != copy.PaymentMethodID))
            goto label_42;
        }
        ARInvoice arInvoice9 = copy;
        nullable6 = new int?();
        int? nullable11 = nullable6;
        arInvoice9.PMInstanceID = nullable11;
      }
    }
    else
    {
      copy.CashAccountID = new int?();
      copy.PMInstanceID = new int?();
    }
label_42:
    SalesPerson salesPerson = (SalesPerson) PXSelectorAttribute.Select<ARInvoice.salesPersonID>(((PXSelectBase) this.Document).Cache, (object) copy);
    if (salesPerson != null)
    {
      nullable1 = salesPerson.IsActive;
      bool flag = false;
      if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
        goto label_45;
    }
    ARInvoice arInvoice10 = copy;
    nullable6 = new int?();
    int? nullable12 = nullable6;
    arInvoice10.SalesPersonID = nullable12;
label_45:
    return copy;
  }

  protected virtual ARTran CreateReversalARTran(ARTran srcTran, ReverseInvoiceArgs reverseArgs)
  {
    ARTran copy = PXCache<ARTran>.CreateCopy(srcTran);
    copy.TranType = (string) null;
    copy.RefNbr = (string) null;
    copy.DrCr = (string) null;
    copy.Released = new bool?();
    copy.CuryInfoID = new long?();
    copy.OrigInvoiceDate = copy.TranDate;
    copy.IsCancellation = new bool?();
    copy.Canceled = new bool?(false);
    copy.NoteID = new Guid?();
    copy.ManualPrice = new bool?(true);
    copy.IsStockItem = new bool?();
    copy.InvtReleased = new bool?(false);
    copy.TranCost = new Decimal?(0M);
    copy.TranCostOrig = new Decimal?(0M);
    copy.IsTranCostFinal = new bool?(false);
    if (reverseArgs.ReverseINTransaction.GetValueOrDefault())
    {
      ARTran arTran = copy;
      short? invtMult = copy.InvtMult;
      short? nullable = invtMult.HasValue ? new short?((short) ((int) invtMult.GetValueOrDefault() * -1)) : new short?();
      arTran.InvtMult = nullable;
      copy.InvtDocType = (string) null;
      copy.InvtRefNbr = (string) null;
    }
    return copy;
  }

  public virtual void ReverseInvoiceProc(ARRegister doc, ReverseInvoiceArgs reverseArgs)
  {
    DuplicateFilter copy1 = PXCache<DuplicateFilter>.CreateCopy(((PXSelectBase<DuplicateFilter>) this.duplicatefilter).Current);
    WebDialogResult answer = ((PXSelectBase) this.duplicatefilter).View.Answer;
    string tranPeriodId = doc.TranPeriodID;
    this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<ARInvoice.finPeriodID, ARInvoice.branchID>(((PXSelectBase) this.Document).Cache, (object) doc, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aRClosed));
    FinPeriodIDAttribute.SetMasterPeriodID<ARInvoice.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) doc);
    ((PXGraph) this).Clear((PXClearOption) 1);
    foreach (PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer> pxResult in PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<ARInvoice.termsID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<PX.Objects.GL.Account, On<ARInvoice.aRAccountID, Equal<PX.Objects.GL.Account.accountID>>>>>>, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1;
      switch (reverseArgs.CurrencyRateOption)
      {
        case ReverseInvoiceArgs.CopyOption.SetOriginal:
          currencyInfo1 = PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Customer>.op_Implicit(pxResult);
          break;
        case ReverseInvoiceArgs.CopyOption.SetDefault:
          currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) null;
          break;
        case ReverseInvoiceArgs.CopyOption.Override:
          currencyInfo1 = reverseArgs.CurrencyRate;
          break;
        default:
          throw new PXArgumentException(nameof (reverseArgs));
      }
      if (currencyInfo1 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo1);
        copy2.CuryInfoID = new long?();
        copy2.IsReadOnly = new bool?(false);
        copy2.BaseCalc = new bool?(true);
        currencyInfo1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(copy2));
      }
      ARInvoice reversalArInvoice = this.CreateReversalARInvoice((ARInvoice) doc, reverseArgs);
      reversalArInvoice.CuryInfoID = (long?) currencyInfo1?.CuryInfoID;
      this.isReverse = true;
      this.ClearRetainageSummary(reversalArInvoice);
      ARInvoice arInvoice1 = ((PXSelectBase<ARInvoice>) this.Document).Insert(reversalArInvoice);
      ARInvoice arInvoice2 = arInvoice1;
      PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        bool? termsInCreditMemos = current.TermsInCreditMemos;
        bool flag = false;
        num = termsInCreditMemos.GetValueOrDefault() == flag & termsInCreditMemos.HasValue ? 1 : 0;
      }
      Decimal? nullable = num != 0 ? new Decimal?(0M) : doc.CuryOrigDiscAmt;
      arInvoice2.CuryOrigDiscAmt = nullable;
      if (arInvoice1.ExternalTaxExemptionNumber != null && doc is ARInvoice && ((ARInvoice) doc).ExternalTaxExemptionNumber == null)
      {
        arInvoice1.ExternalTaxExemptionNumber = (string) null;
        arInvoice1 = ((PXSelectBase<ARInvoice>) this.Document).Update(arInvoice1);
      }
      if (reverseArgs.DateOption == ReverseInvoiceArgs.CopyOption.SetOriginal)
        FinPeriodIDAttribute.SetPeriodsByMaster<ARInvoice.finPeriodID>(((PXSelectBase) this.CurrentDocument).Cache, (object) arInvoice1, tranPeriodId);
      if (reverseArgs.CurrencyRateOption == ReverseInvoiceArgs.CopyOption.SetDefault && arInvoice1.CuryID != doc.CuryID)
      {
        ARInvoice copy3 = PXCache<ARInvoice>.CreateCopy(arInvoice1);
        copy3.CuryID = doc.CuryID;
        arInvoice1 = ((PXSelectBase<ARInvoice>) this.Document).Update(copy3);
      }
      this.isReverse = false;
      if (arInvoice1.RefNbr == null)
      {
        if (PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice>.Config>.Search<ARInvoice.docType, ARInvoice.refNbr>((PXGraph) this, (object) arInvoice1.DocType, (object) arInvoice1.OrigRefNbr, Array.Empty<object>())) != null)
        {
          PXCache<DuplicateFilter>.RestoreCopy(((PXSelectBase<DuplicateFilter>) this.duplicatefilter).Current, copy1);
          ((PXSelectBase) this.duplicatefilter).View.Answer = answer;
          if (((PXSelectBase<DuplicateFilter>) this.duplicatefilter).AskExt() == 1)
          {
            ((PXSelectBase) this.duplicatefilter).Cache.Clear();
            if (((PXSelectBase<DuplicateFilter>) this.duplicatefilter).Current.RefNbr == null)
              throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (DuplicateFilter.refNbr).Name
              });
            if (PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice>.Config>.Search<ARInvoice.docType, ARInvoice.refNbr>((PXGraph) this, (object) arInvoice1.DocType, (object) ((PXSelectBase<DuplicateFilter>) this.duplicatefilter).Current.RefNbr, Array.Empty<object>())) != null)
              throw new PXException("The record already exists.");
            arInvoice1 = this.SetRefNumber(arInvoice1, ((PXSelectBase<DuplicateFilter>) this.duplicatefilter).Current.RefNbr);
          }
        }
        else
          arInvoice1 = this.SetRefNumber(arInvoice1, arInvoice1.OrigRefNbr);
      }
      this.ARInvoiceCreated(arInvoice1, doc);
      if (currencyInfo1 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<ARInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null));
        currencyInfo2.CuryID = currencyInfo1.CuryID;
        currencyInfo2.CuryEffDate = currencyInfo1.CuryEffDate;
        currencyInfo2.CuryRateTypeID = currencyInfo1.CuryRateTypeID;
        currencyInfo2.CuryRate = currencyInfo1.CuryRate;
        currencyInfo2.RecipRate = currencyInfo1.RecipRate;
        currencyInfo2.CuryMultDiv = currencyInfo1.CuryMultDiv;
        ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo2);
      }
    }
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<ARTran.salesPersonID>(ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__198_0 ?? (ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__198_0 = new PXFieldDefaulting((object) ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReverseInvoiceProc\u003Eb__198_0))));
    Dictionary<int?, int?> origLineNbrsDict = new Dictionary<int?, int?>();
    if (doc.IsPrepaymentInvoiceDocument())
    {
      this.InsertReversedTransactionDetailsBalance(doc, reverseArgs);
    }
    else
    {
      this.InsertReversedTransactionDetails(doc, reverseArgs, origLineNbrsDict);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((PXGraph) this).RowInserting.AddHandler<ARSalesPerTran>(ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__198_1 ?? (ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__198_1 = new PXRowInserting((object) ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReverseInvoiceProc\u003Eb__198_1))));
      foreach (PXResult<ARSalesPerTran> pxResult in PXSelectBase<ARSalesPerTran, PXSelect<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Required<ARSalesPerTran.docType>>, And<ARSalesPerTran.refNbr, Equal<Required<ARSalesPerTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }))
      {
        ARSalesPerTran copy4 = PXCache<ARSalesPerTran>.CreateCopy(PXResult<ARSalesPerTran>.op_Implicit(pxResult));
        copy4.DocType = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType;
        copy4.RefNbr = ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr;
        copy4.Released = new bool?(false);
        copy4.CuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
        ARSalesPerTran arSalesPerTran1 = copy4;
        Decimal? nullable = arSalesPerTran1.CuryCommnblAmt;
        Decimal num1 = reverseArgs.PreserveOriginalDocumentSign ? 1M : -1M;
        arSalesPerTran1.CuryCommnblAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num1) : new Decimal?();
        ARSalesPerTran arSalesPerTran2 = copy4;
        nullable = arSalesPerTran2.CuryCommnAmt;
        Decimal num2 = reverseArgs.PreserveOriginalDocumentSign ? 1M : -1M;
        arSalesPerTran2.CuryCommnAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num2) : new Decimal?();
        SalesPerson salesPerson = (SalesPerson) PXSelectorAttribute.Select<ARSalesPerTran.salespersonID>(((PXSelectBase) this.salesPerTrans).Cache, (object) copy4);
        if (salesPerson != null)
        {
          bool? isActive = salesPerson.IsActive;
          bool flag = false;
          if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
            ((PXSelectBase<ARSalesPerTran>) this.salesPerTrans).Update(copy4);
        }
      }
      foreach (PXResult<ARInvoiceDiscountDetail> pxResult in PXSelectBase<ARInvoiceDiscountDetail, PXSelect<ARInvoiceDiscountDetail, Where<ARInvoiceDiscountDetail.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoiceDiscountDetail.refNbr, Equal<Required<ARInvoice.refNbr>>>>, OrderBy<Asc<ARInvoiceDiscountDetail.docType, Asc<ARInvoiceDiscountDetail.refNbr>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      }))
      {
        ARInvoiceDiscountDetail copy5 = PXCache<ARInvoiceDiscountDetail>.CreateCopy(PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult));
        copy5.DocType = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType;
        copy5.RefNbr = ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr;
        copy5.IsManual = new bool?(!reverseArgs.PreserveOriginalDocumentSign);
        copy5.CuryInfoID = new long?();
        this.ARDiscountEngine.UpdateDiscountDetail(((PXSelectBase) this.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, copy5);
      }
      bool? nullable1;
      if (this.IsExternalTax(((PXSelectBase<ARInvoice>) this.Document).Current.TaxZoneID))
      {
        ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
        int num;
        if (current1 == null)
        {
          num = 0;
        }
        else
        {
          nullable1 = current1.DisableAutomaticTaxCalculation;
          num = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num == 0)
        {
          ARInvoice arInvoice = (ARInvoice) doc;
          ARInvoice current2 = ((PXSelectBase<ARInvoice>) this.Document).Current;
          current2.CuryDocBal = arInvoice.CuryOrigDocAmt;
          current2.CuryOrigDocAmt = arInvoice.CuryOrigDocAmt;
          current2.CuryTaxTotal = arInvoice.CuryTaxTotal;
          ((PXSelectBase<ARInvoice>) this.Document).Update(current2);
          using (IEnumerator<PXResult<ARTaxTran>> enumerator = PXSelectBase<ARTaxTran, PXViewOf<ARTaxTran>.BasedOn<SelectFromBase<ARTaxTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTaxTran.tranType, Equal<P.AsString>>>>>.And<BqlOperand<ARTaxTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) doc.DocType,
            (object) doc.RefNbr
          }).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ARTaxTran arTaxTran1 = PXResult<ARTaxTran>.op_Implicit(enumerator.Current);
              ARTaxTran arTaxTran2 = new ARTaxTran();
              arTaxTran2.TaxID = arTaxTran1.TaxID;
              arTaxTran2.TaxRate = arTaxTran1.TaxRate;
              arTaxTran2.TaxBucketID = arTaxTran1.TaxBucketID;
              arTaxTran2.CuryTaxableAmt = arTaxTran1.CuryTaxableAmt;
              arTaxTran2.CuryExemptedAmt = arTaxTran1.CuryExemptedAmt;
              arTaxTran2.CuryTaxAmt = arTaxTran1.CuryTaxAmt;
              arTaxTran2.CuryTaxAmtSumm = arTaxTran1.CuryTaxAmtSumm;
              arTaxTran2.NonDeductibleTaxRate = arTaxTran1.NonDeductibleTaxRate;
              arTaxTran2.CuryExpenseAmt = arTaxTran1.CuryExpenseAmt;
              arTaxTran2.CuryRetainedTaxableAmt = arTaxTran1.CuryRetainedTaxableAmt;
              arTaxTran2.CuryRetainedTaxAmt = arTaxTran1.CuryRetainedTaxAmt;
              arTaxTran2.CuryRetainedTaxAmtSumm = arTaxTran1.CuryRetainedTaxAmtSumm;
              ((PXSelectBase<ARTaxTran>) this.Taxes).Insert(arTaxTran2);
            }
            return;
          }
        }
      }
      nullable1 = doc.PendingPPD;
      int num3;
      if ((!nullable1.GetValueOrDefault() || !(doc.DocType == "CRM")) && !doc.IsOriginalRetainageDocument())
      {
        nullable1 = doc.IsRetainageDocument;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.IsCancellation;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.IsCorrection;
            num3 = nullable1.GetValueOrDefault() ? 1 : 0;
            goto label_62;
          }
        }
      }
      num3 = 1;
label_62:
      bool flag1 = num3 != 0;
      OldNewTaxTranPair<ARTaxTran>[] array = GraphHelper.RowCast<ARTaxTran>((IEnumerable) PXSelectBase<ARTaxTran, PXSelect<ARTaxTran, Where<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>, And<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) doc.DocType,
        (object) doc.RefNbr
      })).Select<ARTaxTran, OldNewTaxTranPair<ARTaxTran>>(new Func<ARTaxTran, OldNewTaxTranPair<ARTaxTran>>(OldNewTaxTranPair.Create<ARTaxTran>)).ToArray<OldNewTaxTranPair<ARTaxTran>>();
      if (flag1)
        EnumerableExtensions.ForEach<OldNewTaxTranPair<ARTaxTran>>((IEnumerable<OldNewTaxTranPair<ARTaxTran>>) array, (System.Action<OldNewTaxTranPair<ARTaxTran>>) (_ => _.InsertCurrentNewTaxTran((PXSelectBase<ARTaxTran>) this.Taxes)));
      foreach (OldNewTaxTranPair<ARTaxTran> oldNewTaxTranPair in array)
      {
        ARTaxTran oldTaxTran = oldNewTaxTranPair.OldTaxTran;
        ARTaxTran arTaxTran = flag1 ? oldNewTaxTranPair.NewTaxTran : oldNewTaxTranPair.InsertCurrentNewTaxTran((PXSelectBase<ARTaxTran>) this.Taxes);
        if (arTaxTran != null)
        {
          ARTaxTran copy6 = PXCache<ARTaxTran>.CreateCopy(arTaxTran);
          copy6.TaxRate = oldTaxTran.TaxRate;
          copy6.CuryTaxableAmt = oldTaxTran.CuryTaxableAmt;
          copy6.CuryExemptedAmt = oldTaxTran.CuryExemptedAmt;
          copy6.CuryTaxAmt = oldTaxTran.CuryTaxAmt;
          copy6.CuryTaxAmtSumm = oldTaxTran.CuryTaxAmtSumm;
          copy6.NonDeductibleTaxRate = oldTaxTran.NonDeductibleTaxRate;
          copy6.CuryExpenseAmt = oldTaxTran.CuryExpenseAmt;
          copy6.CuryRetainedTaxableAmt = oldTaxTran.CuryRetainedTaxableAmt;
          copy6.CuryRetainedTaxAmt = oldTaxTran.CuryRetainedTaxAmt;
          copy6.CuryRetainedTaxAmtSumm = oldTaxTran.CuryRetainedTaxAmtSumm;
          ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
          int num4;
          if (current == null)
          {
            num4 = 0;
          }
          else
          {
            nullable1 = current.DisableAutomaticTaxCalculation;
            num4 = nullable1.GetValueOrDefault() ? 1 : 0;
          }
          if (num4 != 0)
            copy6.TaxBucketID = oldTaxTran.TaxBucketID;
          ((PXSelectBase<ARTaxTran>) this.Taxes).Update(copy6);
        }
      }
      nullable1 = doc.IsRetainageDocument;
      if (!nullable1.GetValueOrDefault())
        return;
      nullable1 = doc.PaymentsByLinesAllowed;
      if (!nullable1.GetValueOrDefault())
        return;
      foreach (PXResult<ARTran> pxResult1 in ((PXSelectBase<ARTran>) this.Transactions).Select(Array.Empty<object>()))
      {
        ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult1);
        foreach (PXResult<ARTax> pxResult2 in PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Required<ARTax.tranType>>, And<ARTax.refNbr, Equal<Required<ARTax.refNbr>>, And<ARTax.lineNbr, Equal<Required<ARTax.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) arTran.TranType,
          (object) arTran.RefNbr,
          (object) arTran.LineNbr
        }))
        {
          ARTax arTax1 = PXResult<ARTax>.op_Implicit(pxResult2);
          int? nullable2 = origLineNbrsDict[arTran.LineNbr];
          ARTax arTax2 = PXResultset<ARTax>.op_Implicit(PXSelectBase<ARTax, PXSelect<ARTax, Where<ARTax.tranType, Equal<Required<ARTax.tranType>>, And<ARTax.refNbr, Equal<Required<ARTax.refNbr>>, And<ARTax.taxID, Equal<Required<ARTax.taxID>>, And<ARTax.lineNbr, Equal<Required<ARTax.lineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[4]
          {
            (object) doc.DocType,
            (object) doc.RefNbr,
            (object) arTax1.TaxID,
            (object) nullable2
          }));
          if (arTax2 != null)
          {
            ARTax copy7 = PXCache<ARTax>.CreateCopy(arTax1);
            copy7.CuryTaxableAmt = arTax2.CuryTaxableAmt;
            copy7.CuryTaxAmt = arTax2.CuryTaxAmt;
            copy7.NonDeductibleTaxRate = arTax2.NonDeductibleTaxRate;
            copy7.CuryExpenseAmt = arTax2.CuryExpenseAmt;
            ((PXSelectBase<ARTax>) this.Tax_Rows).Update(copy7);
          }
          else
            ((PXSelectBase<ARTax>) this.Tax_Rows).Delete(arTax1);
        }
      }
    }
  }

  public virtual ARInvoice InsertReversalARInvoice(ARInvoice arInvoice)
  {
    return ((PXSelectBase<ARInvoice>) this.Document).Insert(arInvoice);
  }

  public virtual ARInvoice SetRefNumber(ARInvoice arInvoice, string refNbr)
  {
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.refNbr>((object) arInvoice, (object) refNbr);
    ((PXSelectBase) this.Document).Cache.Normalize();
    return arInvoice;
  }

  public virtual void InsertReversedTransactionDetails(
    ARRegister doc,
    ReverseInvoiceArgs reverseArgs,
    Dictionary<int?, int?> origLineNbrsDict)
  {
    foreach (PXResult<ARTran> pxResult in PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }))
    {
      ARTran srcTran = PXResult<ARTran>.op_Implicit(pxResult);
      int? nullable1;
      if (srcTran?.LineType == "DS")
      {
        nullable1 = srcTran.SOOrderLineNbr;
        if (nullable1.HasValue)
          continue;
      }
      ARTran reversalArTran = this.CreateReversalARTran(srcTran, reverseArgs);
      if (reversalArTran != null)
      {
        ARTran arTran1 = reversalArTran;
        bool? nullable2 = doc.IsRetainageDocument;
        int? nullable3 = nullable2.GetValueOrDefault() ? srcTran.OrigLineNbr : srcTran.LineNbr;
        arTran1.OrigLineNbr = nullable3;
        this.ReverseDRSchedule(doc, reversalArTran);
        reversalArTran.ClearInvoiceDetailsBalance();
        SalesPerson salesPerson = (SalesPerson) PXSelectorAttribute.Select<ARTran.salesPersonID>(((PXSelectBase) this.Transactions).Cache, (object) reversalArTran);
        if (salesPerson != null)
        {
          nullable2 = salesPerson.IsActive;
          bool flag = false;
          if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
            goto label_8;
        }
        ARTran arTran2 = reversalArTran;
        nullable1 = new int?();
        int? nullable4 = nullable1;
        arTran2.SalesPersonID = nullable4;
label_8:
        this.isReverse = true;
        Decimal? curyTranAmt1 = reversalArTran.CuryTranAmt;
        ARTran arTran3 = ((PXSelectBase<ARTran>) this.Transactions).Insert(reversalArTran);
        PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Transactions).Cache, (object) srcTran, ((PXSelectBase) this.Transactions).Cache, (object) arTran3, (PXNoteAttribute.IPXCopySettings) null);
        this.isReverse = false;
        if (arTran3 != null)
        {
          Decimal? curyTranAmt2 = arTran3.CuryTranAmt;
          Decimal? nullable5 = curyTranAmt1;
          if (!(curyTranAmt2.GetValueOrDefault() == nullable5.GetValueOrDefault() & curyTranAmt2.HasValue == nullable5.HasValue) || arTran3.DeferredCode != reversalArTran.DeferredCode)
          {
            arTran3.CuryTranAmt = curyTranAmt1;
            arTran3.DeferredCode = reversalArTran.DeferredCode;
            arTran3 = (ARTran) ((PXSelectBase) this.Transactions).Cache.Update((object) arTran3);
          }
        }
        arTran3.ManualDisc = new bool?(true);
        if (arTran3.LineType == "DS")
        {
          arTran3.DrCr = reverseArgs.PreserveOriginalDocumentSign ? srcTran.DrCr : (srcTran.DrCr == "D" ? "C" : "D");
          arTran3.FreezeManualDisc = new bool?(!reverseArgs.PreserveOriginalDocumentSign);
          arTran3.TaxCategoryID = (string) null;
          ((PXSelectBase<ARTran>) this.Transactions).Update(arTran3);
        }
        origLineNbrsDict.Add(arTran3.LineNbr, srcTran.LineNbr);
      }
    }
  }

  public virtual void InsertReversedTransactionDetailsBalance(
    ARRegister doc,
    ReverseInvoiceArgs reverseArgs)
  {
    ARTran arTran1 = new ARTran();
    arTran1.TaxCategoryID = (string) null;
    arTran1.Commissionable = new bool?(false);
    this.isReverse = true;
    ARTran arTran2 = ((PXSelectBase<ARTran>) this.Transactions).Insert(arTran1);
    this.isReverse = false;
    arTran2.CuryExtPrice = doc.CuryDocBal;
    arTran2.TranDesc = reverseArgs.OverrideDocumentDescr;
    ARTran arTran3 = PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Required<ARInvoice.refNbr>>, And<ARTran.taskID, IsNotNull, And<Not<Where<ARTran.lineType, IsNotNull, And<ARTran.lineType, Equal<SOLineType.discount>>>>>>>>, OrderBy<Asc<ARTran.lineNbr>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    arTran2.TaskID = (int?) arTran3?.TaskID;
    arTran2.CostCodeID = (int?) arTran3?.CostCodeID;
    ((PXSelectBase<ARTran>) this.Transactions).Update(arTran2);
  }

  public virtual void ReverseDRSchedule(ARRegister doc, ARTran tran)
  {
    if (string.IsNullOrEmpty(tran.DeferredCode))
      return;
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<PX.Objects.BQLConstants.moduleAR>, And<DRSchedule.docType, Equal<Required<DRSchedule.docType>>, And<DRSchedule.refNbr, Equal<Required<DRSchedule.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) doc.DocType,
      (object) doc.RefNbr,
      (object) tran.LineNbr
    }));
    if (drSchedule == null)
      return;
    tran.DefScheduleID = drSchedule.ScheduleID;
  }

  /// <summary>
  /// Reverse current document and apply it to reversal document if needed.
  /// </summary>
  /// <param name="reverseArgs">Arguments needed for creating a reversal.</param>
  /// <returns />
  public virtual IEnumerable ReverseDocumentAndApplyToReversalIfNeeded(
    PXAdapter adapter,
    ReverseInvoiceArgs reverseArgs)
  {
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    string docType = current1?.DocType;
    bool flag1 = docType != "INV" && docType != "PPI" && docType != "DRM" && docType != "CRM";
    if (current1 == null | flag1)
      return adapter.Get();
    if (current1.InstallmentNbr.HasValue && !string.IsNullOrEmpty(current1.MasterRefNbr))
      throw new PXSetPropertyException("Multiple installments invoice cannot be reversed, Please reverse original invoice '{0}'.", new object[1]
      {
        (object) current1.MasterRefNbr
      });
    bool? retainageDocument = current1.IsRetainageDocument;
    if (retainageDocument.GetValueOrDefault() || current1.IsOriginalRetainageDocument())
    {
      ARRetainageInvoice retainageInvoice = GraphHelper.RowCast<ARRetainageInvoice>((IEnumerable) ((PXSelectBase<ARRetainageInvoice>) this.RetainageDocuments).Select(Array.Empty<object>())).FirstOrDefault<ARRetainageInvoice>((Func<ARRetainageInvoice, bool>) (row => !row.Released.GetValueOrDefault()));
      if (retainageInvoice != null)
        throw new PXException("The document cannot be reversed because there is retainage {0} {1} associated with this {2} that is not released yet.", new object[3]
        {
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[retainageInvoice.DocType]),
          (object) retainageInvoice.RefNbr,
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[current1.DocType])
        });
      bool flag2 = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Current<ARInvoice.docType>>, And<ARAdjust.adjdRefNbr, Equal<Current<ARInvoice.refNbr>>, And<ARAdjust.voided, Equal<False>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>())) != null;
      retainageDocument = current1.IsRetainageDocument;
      if (retainageDocument.GetValueOrDefault() & flag2)
        throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has been fully or partially settled.", new object[2]
        {
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[current1.DocType]),
          (object) current1.RefNbr
        });
      int num1;
      if (current1.IsOriginalRetainageDocument())
      {
        Decimal? curyRetainageTotal = current1.CuryRetainageTotal;
        Decimal? retainageUnreleasedAmt = current1.CuryRetainageUnreleasedAmt;
        num1 = !(curyRetainageTotal.GetValueOrDefault() == retainageUnreleasedAmt.GetValueOrDefault() & curyRetainageTotal.HasValue == retainageUnreleasedAmt.HasValue) ? 1 : 0;
      }
      else
        num1 = 0;
      int num2 = flag2 ? 1 : 0;
      if ((num1 | num2) != 0)
        throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has been fully or partially settled or it has a released retainage document.", new object[2]
        {
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[current1.DocType]),
          (object) current1.RefNbr
        });
      ARRegister reversingDoc;
      if (this.CheckReversingRetainageDocumentAlreadyExists(current1, out reversingDoc))
        throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has already been reversed with the {2} document with the {3} ref. number.", new object[4]
        {
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[current1.DocType]),
          (object) current1.RefNbr,
          (object) PXMessages.LocalizeNoPrefix(ARInvoiceEntry.ARDocTypeDict[reversingDoc.DocType]),
          (object) reversingDoc.RefNbr
        });
    }
    else if (docType == "PPI")
    {
      ARAdjust creditMemo = (ARAdjust) null;
      if (this.AskUserApprovalIfUnreleasedCreditMemoAlreadyExists(((PXSelectBase<ARInvoice>) this.Document).Current, out creditMemo))
      {
        ((PXSelectBase<ARInvoice>) this.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) this.Document).Search<ARInvoice.refNbr>((object) creditMemo.AdjgRefNbr, new object[1]
        {
          (object) "CRM"
        }));
        PXRedirectHelper.TryRedirect((PXGraph) this, (PXRedirectHelper.WindowMode) 0);
      }
      else if (creditMemo != null)
        return adapter.Get();
      ARAdjust2 arAdjust2 = PXResultset<ARAdjust2>.op_Implicit(PXSelectBase<ARAdjust2, PXSelect<ARAdjust2, Where<ARAdjust2.adjdDocType, Equal<Current<ARInvoice.docType>>, And<ARAdjust2.adjdRefNbr, Equal<Current<ARInvoice.refNbr>>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.voided, Equal<False>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()));
      if (arAdjust2 != null)
        throw new PXSetPropertyException("The prepayment invoice has an unreleased application to the {0} document with the {1} type and cannot be reversed.", new object[2]
        {
          (object) arAdjust2.AdjgRefNbr,
          (object) arAdjust2.AdjgDocType
        });
    }
    else if (!this.AskUserApprovalIfReversingDocumentAlreadyExists(current1))
      return adapter.Get();
    if (current1.DocType == "INV" && this.GetReversingDocument(current1) == null && !this.AskUserApprovalIfInvoiceIsLinkedToShipment(current1))
      return adapter.Get();
    ((PXAction) this.Save).Press();
    ARInvoice copy = PXCache<ARInvoice>.CreateCopy(current1);
    try
    {
      this.ReverseInvoiceProc((ARRegister) copy, reverseArgs);
      ARInvoice current2 = ((PXSelectBase<ARInvoice>) this.Document).Current;
      if (reverseArgs.ApplyToOriginalDocument)
        this.ApplyOriginalDocumentToReversal(current1, current2);
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<ARInvoice.finPeriodID>((object) current2, (object) current2.FinPeriodID, (Exception) null);
      return (IEnumerable) new List<ARInvoice>()
      {
        current2
      };
    }
    catch (PXException ex)
    {
      ((PXGraph) this).Clear((PXClearOption) 1);
      ((PXSelectBase<ARInvoice>) this.Document).Current = copy;
      throw;
    }
  }

  /// <summary>
  /// Ask user for approval for creation of another reversal if reversing document already exists.
  /// </summary>
  /// <param name="origDoc">The original document.</param>
  /// <returns>True if user approves, false if not.</returns>
  public virtual bool AskUserApprovalIfReversingDocumentAlreadyExists(ARInvoice origDoc)
  {
    ARRegister reversingDocument = this.GetReversingDocument(origDoc);
    if (reversingDocument == null)
      return true;
    string str;
    new ARDocType.ListAttribute().ValueLabelDic.TryGetValue(this.GetReversingDocType(origDoc.DocType), out str);
    return ((PXSelectBase<ARInvoice>) this.Document).Ask("Reverse Document", PXMessages.LocalizeFormatNoPrefix("A reversing document ({0} {1}) already exists for the original document. Reverse the document?", new object[2]
    {
      (object) str,
      (object) reversingDocument.RefNbr
    }), (MessageButtons) 1) == 1;
  }

  /// <summary>
  /// Ask user for approval for creation payment if unreleased credit memo already exists.
  /// </summary>
  /// <param name="doc">The current document.</param>
  /// <returns>True if user approves, false if not.</returns>
  protected virtual bool AskUserApprovalIfUnreleasedCreditMemoAlreadyExists(
    ARInvoice doc,
    out ARAdjust creditMemo)
  {
    creditMemo = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjgDocType, Equal<ARDocType.creditMemo>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>>>>>, OrderBy<Desc<ARRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    if (creditMemo == null)
      return false;
    return ((PXSelectBase) this.Document).View.Ask(PXMessages.LocalizeFormatNoPrefix(doc.DocType == "PPI" ? "The prepayment invoice has already been applied to the {0} credit memo, but the credit memo is not released. Do you want to navigate to the credit memo?" : "The invoice has already been applied to the {0} credit memo, but the credit memo is not released. Do you want to navigate to the credit memo?", new object[1]
    {
      (object) creditMemo.AdjgRefNbr
    }), (MessageButtons) 4) == 6;
  }

  /// <summary>Get reversing document for the document if exist .</summary>
  /// <param name="origDoc">The original document.</param>
  /// <returns>Reversing document if exisits or null if not.</returns>
  protected virtual ARRegister GetReversingDocument(ARInvoice origDoc)
  {
    return PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<Required<ARRegister.docType>>, And<ARRegister.origDocType, Equal<Required<ARRegister.origDocType>>, And<ARRegister.origRefNbr, Equal<Required<ARRegister.origRefNbr>>>>>, OrderBy<Desc<ARRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) this.GetReversingDocType(origDoc.DocType),
      (object) origDoc.DocType,
      (object) origDoc.RefNbr
    }));
  }

  /// <summary>
  /// Ask user for approval for reverse invoice if invoice is linked to shipment(s) exists.
  /// </summary>
  /// <param name="origDoc">The original document.</param>
  /// <returns>
  /// True if shpment is not linked or else, if user approves , false if not.
  /// </returns>
  protected virtual bool AskUserApprovalIfInvoiceIsLinkedToShipment(ARInvoice origDoc)
  {
    return PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.refNbr, Equal<Required<ARInvoice.refNbr>>, And<ARTran.tranType, Equal<Required<ARInvoice.docType>>, And<ARTran.sOShipmentNbr, IsNotNull, And<ARTran.sOShipmentNbr, NotEqual<PX.Objects.SO.Constants.noShipmentNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) origDoc.RefNbr,
      (object) origDoc.DocType
    })) == null || ((PXSelectBase) this.Document).View.Ask((object) ((PXSelectBase<ARInvoice>) this.Document).Current, "Warning", "The invoice is linked to one or multiple shipments. If you need only to reverse the invoice, click OK to proceed. If you need to correct the current invoice, or cancel it and prepare another invoice for the shipped goods, click Cancel to cancel reversing, and then use the Correct or Cancel action on the Invoices (SO303000) form.", (MessageButtons) 1, (MessageIcon) 3) == 1;
  }

  private void VerifyCanCreateNewRefundForDocument(ARRegister doc)
  {
    ARAdjust arAdjust = PXResultset<ARAdjust>.op_Implicit(PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsEqual<P.AsString>>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>>>>, And<BqlOperand<ARAdjust.released, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) doc.DocType,
      (object) doc.RefNbr,
      (object) doc.DocType,
      (object) doc.RefNbr
    }));
    if (arAdjust != null)
    {
      string docType = arAdjust.AdjgDocType;
      string str = arAdjust.AdjgRefNbr;
      if (docType == doc.DocType && str == doc.RefNbr)
      {
        docType = arAdjust.AdjdDocType;
        str = arAdjust.AdjdRefNbr;
      }
      throw new PXException("The {0} cannot be refunded because it has an unreleased application to the following document: {1} {2}.", new object[3]
      {
        (object) ARDocType.GetDisplayName(doc.DocType),
        (object) ARDocType.GetDisplayName(docType),
        (object) str
      });
    }
  }

  private void ApplyOriginalDocumentToReversal(ARInvoice origDoc, ARInvoice reversingDoc)
  {
    if (origDoc.HasZeroBalance<ARRegister.curyDocBal, ARTran.curyTranBal>((PXGraph) this) && (!origDoc.IsOriginalRetainageDocument() || origDoc.HasZeroBalance<ARRegister.curyRetainageUnreleasedAmt, ARTran.curyRetainageBal>((PXGraph) this)) || origDoc.Status == "C" || reversingDoc == null)
      return;
    switch (reversingDoc.DocType)
    {
      case "DRM":
        this.ApplyOriginalDocAdjustmentToDebitMemo(origDoc, reversingDoc);
        break;
      case "CRM":
        this.ApplyOriginalDocAdjustmentToCreditMemo(origDoc, reversingDoc);
        break;
    }
  }

  protected virtual void ApplyOriginalDocAdjustmentToCreditMemo(
    ARInvoice origDoc,
    ARInvoice reversingDoc)
  {
    ((PXSelectBase<ARAdjust>) this.Adjustments_1).Insert(new ARAdjust()
    {
      AdjgDocType = reversingDoc.DocType,
      AdjgRefNbr = reversingDoc.RefNbr,
      AdjdDocType = origDoc.DocType,
      AdjdRefNbr = origDoc.RefNbr,
      CuryAdjgAmt = origDoc.CuryDocBal,
      InvoiceID = origDoc.NoteID,
      AdjdCustomerID = origDoc.CustomerID,
      MemoID = reversingDoc.NoteID
    });
  }

  /// <summary>
  /// Applies the original document adjustment to reversing debit memo. By this moment usually there are already several applications to the debit memo,
  /// so select is used to find an application for a reversing document among them and set its balance.
  /// </summary>
  /// <param name="origDoc">The original document.</param>
  /// <param name="reversingDebitMemo">The reversing debit memo.</param>
  private void ApplyOriginalDocAdjustmentToDebitMemo(
    ARInvoice origDoc,
    ARInvoice reversingDebitMemo)
  {
    ARAdjust2 arAdjust2_1 = PXResultset<ARAdjust2>.op_Implicit(PXSelectBase<ARAdjust2, PXSelect<ARAdjust2, Where<ARAdjust2.adjdDocType, Equal<Current<ARInvoice.docType>>, And<ARAdjust2.adjgDocType, Equal<Required<ARInvoice.docType>>, And<ARAdjust2.adjgRefNbr, Equal<Required<ARInvoice.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) origDoc.DocType,
      (object) origDoc.RefNbr
    }));
    if (arAdjust2_1 == null)
    {
      ARAdjust2 arAdjust2_2 = new ARAdjust2();
      arAdjust2_2.AdjdDocType = reversingDebitMemo.DocType;
      arAdjust2_2.AdjdRefNbr = reversingDebitMemo.RefNbr;
      arAdjust2_2.AdjgDocType = origDoc.DocType;
      arAdjust2_2.AdjgRefNbr = origDoc.RefNbr;
      arAdjust2_2.AdjNbr = origDoc.AdjCntr;
      arAdjust2_2.CuryAdjdAmt = origDoc.CuryDocBal;
      arAdjust2_2.CustomerID = origDoc.CustomerID;
      arAdjust2_2.AdjdCustomerID = reversingDebitMemo.CustomerID;
      arAdjust2_2.AdjdBranchID = reversingDebitMemo.BranchID;
      arAdjust2_2.AdjgBranchID = origDoc.BranchID;
      arAdjust2_2.AdjgCuryInfoID = origDoc.CuryInfoID;
      arAdjust2_2.AdjdOrigCuryInfoID = reversingDebitMemo.CuryInfoID;
      arAdjust2_2.AdjdCuryInfoID = reversingDebitMemo.CuryInfoID;
      arAdjust2_2.InvoiceID = reversingDebitMemo.NoteID;
      arAdjust2_2.MemoID = origDoc.NoteID;
      ((PXSelectBase<ARAdjust2>) this.Adjustments).Insert(arAdjust2_2);
    }
    else
      ((PXSelectBase) this.Adjustments).Cache.SetValueExt<ARAdjust2.curyAdjdAmt>((object) arAdjust2_1, (object) origDoc.CuryDocBal);
  }

  protected virtual void ARInvoiceCreated(ARInvoice invoice, ARRegister doc)
  {
  }

  public virtual string SalesSubMask
  {
    get
    {
      if (this.salesSubMask == null)
        this.salesSubMask = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.SalesSubMask;
      return this.salesSubMask;
    }
    set => this.salesSubMask = value;
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PMProject project;
    if (ProjectDefaultAttribute.IsProject((PXGraph) this, (int?) ((PXSelectBase<ARInvoice>) this.Document).Current?.ProjectID, out project))
    {
      e.NewValue = (object) project.BillingCuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else if (((PXSelectBase<Customer>) this.customer).Current != null && !string.IsNullOrEmpty(((PXSelectBase<Customer>) this.customer).Current.CuryID))
    {
      e.NewValue = (object) ((PXSelectBase<Customer>) this.customer).Current.CuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current.BaseCuryID))
        return;
      e.NewValue = (object) ((PXSelectBase<PX.Objects.GL.Branch>) this.branch).Current.BaseCuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    PX.Objects.CM.Extensions.CurrencyInfo row = e.Row;
    if ((row != null ? (!row.CuryRate.HasValue ? 1 : 0) : 1) != 0)
      return;
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if ((current1 != null ? (!current1.CustomerID.HasValue ? 1 : 0) : 1) != 0)
      return;
    ARInvoice current2 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if ((current2 != null ? (current2.Released.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      return;
    if (((PXSelectBase<ARInvoice>) this.Document).Current?.DocType != "CRM")
    {
      foreach (ARAdjust2 adj in GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase<ARAdjust2>) this.Adjustments_Inv).Select(Array.Empty<object>())).Where<ARAdjust2>((Func<ARAdjust2, bool>) (_ => _ != null)))
      {
        this.CalcBalancesFromInvoiceSide(adj, true, false);
        ((PXSelectBase<ARAdjust2>) this.Adjustments).Update(adj);
        Decimal? curyWhTaxBal = adj.CuryWhTaxBal;
        Decimal num = 0M;
        if (curyWhTaxBal.GetValueOrDefault() < num & curyWhTaxBal.HasValue)
          ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust2.curyAdjdWOAmt>((object) adj, (object) adj.CuryAdjdWOAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
      }
    }
    else
      EnumerableExtensions.ForEach<PXResult<ARAdjust>>((IEnumerable<PXResult<ARAdjust>>) ((PXSelectBase<ARAdjust>) this.Adjustments_1).Select(Array.Empty<object>()), (System.Action<PXResult<ARAdjust>>) (adjustment => this.CalcBalances(PXResult<ARAdjust>.op_Implicit(adjustment), true, false)));
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.GetStatus(e.Row) == 2)
      return;
    if (!sender.ObjectsEqual<ARInvoice.docDate, ARInvoice.finPeriodID, ARInvoice.curyID>(e.Row, e.OldRow))
    {
      foreach (PXResult<ARSalesPerTran> pxResult in ((PXSelectBase<ARSalesPerTran>) this.salesPerTrans).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.salesPerTrans).Cache, (object) PXResult<ARSalesPerTran>.op_Implicit(pxResult));
      foreach (PXResult<ARFinChargeTran> pxResult in ((PXSelectBase<ARFinChargeTran>) this.finChargeTrans).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.finChargeTrans).Cache, (object) PXResult<ARFinChargeTran>.op_Implicit(pxResult));
    }
    if (!sender.ObjectsEqual<ARInvoice.docDate, ARInvoice.finPeriodID, ARInvoice.curyID, ARInvoice.aRAccountID, ARInvoice.aRSubID, ARInvoice.branchID>(e.Row, e.OldRow))
    {
      foreach (PXResult<ARAdjust2> pxResult in ((PXSelectBase<ARAdjust2>) this.Adjustments).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments).Cache, (object) PXResult<ARAdjust2>.op_Implicit(pxResult));
      foreach (PXResult<ARAdjust> pxResult in ((PXSelectBase<ARAdjust>) this.Adjustments_1).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments_1).Cache, (object) PXResult<ARAdjust>.op_Implicit(pxResult));
    }
    if (sender.ObjectsEqual<ARInvoice.branchID>(e.Row, e.OldRow))
      return;
    foreach (PXResult<ARSalesPerTran> pxResult in ((PXSelectBase<ARSalesPerTran>) this.salesPerTrans).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.salesPerTrans).Cache, (object) PXResult<ARSalesPerTran>.op_Implicit(pxResult));
  }

  public bool IsProcessingMode { get; set; }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  public ARInvoiceEntry()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    GraphHelper.EnsureCachePersistence<ARInvoiceAdjusted>((PXGraph) this);
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    ARInvoiceEntry arInvoiceEntry = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) arInvoiceEntry, __vmethodptr(arInvoiceEntry, ParentFieldUpdated));
    rowUpdated.AddHandler<ARInvoice>(pxRowUpdated);
    ((PXSelectBase) this.RetainageDocuments).Cache.AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.retainage>();
    ((PXSelectBase) this.RetainageDocuments).Cache.AllowDelete = false;
    ((PXSelectBase) this.RetainageDocuments).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.RetainageDocuments).Cache, (string) null, false);
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__242_0 ?? (ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9__242_0 = new PXFieldDefaulting((object) ARInvoiceEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__242_0))));
    if (current != null && current.DunningLetterProcessType.GetValueOrDefault() == 1)
      PXUIFieldAttribute.SetVisible<ARInvoice.revoked>(((PXSelectBase) this.Document).Cache, (object) null, true);
    ((PXGraph) this).Caches.SubscribeCacheCreated(((PXSelectBase<ARAdjust2>) this.Adjustments).GetItemType(), (System.Action) (() => PXUIFieldAttribute.SetVisible<ARAdjust.customerID>(((PXSelectBase) this.Adjustments).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>())));
    PXUIFieldAttribute.SetVisible<ARAdjust.adjdCustomerID>(((PXSelectBase) this.Adjustments_1).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());
    PXUIFieldAttribute.SetEnabled<ARAdjust.curyAdjdPPDAmt>(((PXSelectBase) this.Adjustments).Cache, (object) null, false);
    OpenPeriodAttribute.SetValidatePeriod<ARRegister.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    this.TaxesByTaxCategory = new Dictionary<string, HashSet<string>>();
    PXCache cach1 = ((PXGraph) this).Caches[typeof (ARAddress)];
    PXCache cach2 = ((PXGraph) this).Caches[typeof (ARContact)];
    PXCache cach3 = ((PXGraph) this).Caches[typeof (ARShippingAddress)];
    PXCache cach4 = ((PXGraph) this).Caches[typeof (ARShippingContact)];
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<ARInvoice>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (ARTran), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[4]
      {
        (PXDataFieldValue) new PXDataFieldValue<ARTran.tranType>((PXDbType) 3, new int?(3), (object) ((PXSelectBase<ARInvoice>) ((ARInvoiceEntry) graph).Document).Current?.DocType),
        (PXDataFieldValue) new PXDataFieldValue<ARTran.refNbr>((object) ((PXSelectBase<ARInvoice>) ((ARInvoiceEntry) graph).Document).Current?.RefNbr),
        (PXDataFieldValue) new PXDataFieldValue<ARTran.lineType>((PXDbType) 3, new int?(2), (object) "FR", (PXComp) 10),
        (PXDataFieldValue) new PXDataFieldValue<ARTran.lineType>((PXDbType) 3, new int?(2), (object) "DS", (PXComp) 10)
      }))
    });
  }

  protected virtual List<KeyValuePair<string, List<FieldInfo>>> AdjustApiScript(
    List<KeyValuePair<string, List<FieldInfo>>> fieldsByView)
  {
    List<KeyValuePair<string, List<FieldInfo>>> source1 = ((PXGraph) this).AdjustApiScript(fieldsByView);
    KeyValuePair<string, List<FieldInfo>> keyValuePair = source1.FirstOrDefault<KeyValuePair<string, List<FieldInfo>>>((Func<KeyValuePair<string, List<FieldInfo>>, bool>) (x => x.Key == "Document"));
    List<FieldInfo> fieldInfoList = keyValuePair.Value;
    keyValuePair = source1.FirstOrDefault<KeyValuePair<string, List<FieldInfo>>>((Func<KeyValuePair<string, List<FieldInfo>>, bool>) (x => x.Key == "CurrentDocument"));
    List<FieldInfo> source2 = keyValuePair.Value;
    if (source2 == null || fieldInfoList == null)
      return source1;
    FieldInfo fieldInfo = source2.FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (x => x.FieldName == "BranchID"));
    if (fieldInfo == null)
      return source1;
    source2.Remove(fieldInfo);
    fieldInfoList.Insert(0, fieldInfo);
    return source1;
  }

  public override void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    base.CopyPasteGetScript(isImportSimple, script, containers);
    int index1 = script.FindIndex((Predicate<Command>) (x => x.FieldName == "SubID"));
    if (index1 == -1)
      return;
    Command cmd = script[index1];
    Command command = script.LastOrDefault<Command>((Func<Command, bool>) (x => string.Equals(x.ObjectName, cmd.ObjectName)));
    int index2 = script.IndexOf(command);
    Container container = containers[index1];
    script.RemoveAt(index1);
    containers.RemoveAt(index1);
    script.Insert(index2, cmd);
    containers.Insert(index2, container);
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    map.Add(typeof (PX.Objects.CT.Contract), typeof (PX.Objects.CT.Contract));
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      bool? released = ((PXSelectBase<ARInvoice>) this.Document).Current.Released;
      bool flag1 = false;
      if (released.GetValueOrDefault() == flag1 & released.HasValue)
      {
        bool flag2 = ((PXSelectBase<ARTran>) this.Discount_Row).Any<ARTran>();
        Decimal? curyDiscTot;
        if (!flag2)
        {
          curyDiscTot = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscTot;
          Decimal num = 0M;
          if (!(curyDiscTot.GetValueOrDefault() == num & curyDiscTot.HasValue))
          {
            this.AddDiscount(((PXSelectBase) this.Document).Cache, ((PXSelectBase<ARInvoice>) this.Document).Current);
            goto label_8;
          }
        }
        if (flag2)
        {
          curyDiscTot = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscTot;
          Decimal num = 0M;
          if (curyDiscTot.GetValueOrDefault() == num & curyDiscTot.HasValue && !((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).Any<ARInvoiceDiscountDetail>())
            EnumerableExtensions.ForEach<ARTran>(GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Discount_Row).Select(Array.Empty<object>())), (System.Action<ARTran>) (discountLine => ((PXSelectBase) this.Discount_Row).Cache.Delete((object) discountLine)));
        }
      }
    }
label_8:
    ((PXSelectBase) this.Adjustments).Cache.ClearQueryCache();
    BranchBaseAttribute.VerifyFieldInPXCache<ARTran, ARTran.branchID>((PXGraph) this, ((PXSelectBase<ARTran>) this.Transactions).Select(Array.Empty<object>()));
    foreach (object obj in ((PXSelectBase) this.Adjustments).Cache.Inserted.Cast<ARAdjust2>().Where<ARAdjust2>((Func<ARAdjust2, bool>) (adj =>
    {
      Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
      Decimal num = 0M;
      return curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue && !adj.Recalculatable.GetValueOrDefault();
    })))
      ((PXSelectBase) this.Adjustments).Cache.SetStatus(obj, (PXEntryStatus) 4);
    foreach (object obj in ((PXSelectBase) this.Adjustments).Cache.Updated.Cast<ARAdjust2>().Where<ARAdjust2>((Func<ARAdjust2, bool>) (adj =>
    {
      Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
      Decimal num = 0M;
      return curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue && !adj.Recalculatable.GetValueOrDefault();
    })))
      ((PXSelectBase) this.Adjustments).Cache.SetStatus(obj, (PXEntryStatus) 3);
    foreach (ARInvoice arInvoice in ((PXSelectBase) this.Document).Cache.Cached)
    {
      PXEntryStatus status = ((PXSelectBase) this.Document).Cache.GetStatus((object) arInvoice);
      bool? nullable1;
      if (status == 3)
      {
        nullable1 = arInvoice.PendingPPD;
        if (nullable1.GetValueOrDefault() && (arInvoice.DocType == "CRM" || arInvoice.DocType == "DRM"))
          PXUpdate<Set<ARAdjust.pPDVATAdjRefNbr, Null, Set<ARAdjust.pPDVATAdjDocType, Null>>, ARAdjust, Where<ARAdjust.pendingPPD, Equal<True>, And<ARAdjust.pPDVATAdjRefNbr, Equal<Required<ARAdjust.pPDVATAdjRefNbr>>, And<ARAdjust.pPDVATAdjDocType, Equal<Required<ARAdjust.pPDVATAdjDocType>>>>>>.Update((PXGraph) this, new object[2]
          {
            (object) arInvoice.RefNbr,
            (object) arInvoice.DocType
          });
      }
      Decimal? nullable2;
      Decimal? nullable3;
      if (EnumerableExtensions.IsIn<PXEntryStatus>(status, (PXEntryStatus) 2, (PXEntryStatus) 1) && EnumerableExtensions.IsIn<string>(arInvoice.DocType, "INV", "DRM"))
      {
        nullable1 = arInvoice.Released;
        bool flag3 = false;
        if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
        {
          nullable1 = arInvoice.ApplyPaymentWhenTaxAvailable;
          if (!nullable1.GetValueOrDefault())
          {
            ARAdjust2 arAdjust2_1 = (ARAdjust2) null;
            Decimal? nullable4 = new Decimal?(0M);
            Decimal? nullable5 = new Decimal?(0M);
            foreach (ARAdjust2 arAdjust2_2 in ((PXSelectBase) this.Adjustments_Inv).Cache.Cached)
            {
              if (((PXSelectBase) this.Adjustments_Inv).Cache.GetStatus((object) arAdjust2_2) == null)
                ((PXSelectBase) this.Adjustments_Inv).Cache.Remove((object) arAdjust2_2);
            }
            foreach (ARAdjust2 application in EnumerableExtensions.WhereNotNull<ARAdjust2>(GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase) this.Adjustments_Inv).View.SelectMultiBound(new object[1]
            {
              (object) arInvoice
            }, Array.Empty<object>()))))
            {
              arAdjust2_1 = application;
              Decimal? nullable6 = nullable5;
              Decimal? adjAmt = application.AdjAmt;
              Decimal? adjDiscAmt = application.AdjDiscAmt;
              Decimal? nullable7 = adjAmt.HasValue & adjDiscAmt.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + adjDiscAmt.GetValueOrDefault()) : new Decimal?();
              Decimal? adjWoAmt = application.AdjWOAmt;
              Decimal? nullable8 = nullable7.HasValue & adjWoAmt.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + adjWoAmt.GetValueOrDefault()) : new Decimal?();
              nullable5 = nullable6.HasValue & nullable8.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
              nullable8 = nullable4;
              Decimal adjustedBalanceDelta = application.GetFullBalanceDelta().CurrencyAdjustedBalanceDelta;
              nullable4 = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + adjustedBalanceDelta) : new Decimal?();
              Decimal? curyDocBal = arInvoice.CuryDocBal;
              Decimal? nullable9 = nullable4;
              nullable8 = curyDocBal.HasValue & nullable9.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new Decimal?();
              Decimal num1 = 0M;
              if (nullable8.GetValueOrDefault() < num1 & nullable8.HasValue)
              {
                nullable8 = nullable4;
                Decimal num2 = 0M;
                if (nullable8.GetValueOrDefault() > num2 & nullable8.HasValue)
                {
                  GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments).Cache, (object) application);
                  ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust2.curyAdjdAmt>((object) application, (object) application.CuryAdjdAmt, (Exception) new PXSetPropertyException("The total application amount must not exceed the document amount."));
                  throw new PXException("The total application amount must not exceed the document amount.");
                }
              }
            }
            if (arAdjust2_1 != null)
            {
              Decimal? curyDocBal = arInvoice.CuryDocBal;
              Decimal? nullable10 = nullable4;
              Decimal? nullable11 = curyDocBal.HasValue & nullable10.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable10.GetValueOrDefault()) : new Decimal?();
              Decimal? docBal = arInvoice.DocBal;
              Decimal? nullable12 = nullable5;
              Decimal? nullable13 = docBal.HasValue & nullable12.HasValue ? new Decimal?(docBal.GetValueOrDefault() - nullable12.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable14 = nullable11;
              Decimal num3 = 0M;
              int num4;
              if (nullable14.GetValueOrDefault() > num3 & nullable14.HasValue)
              {
                nullable2 = nullable13;
                Decimal num5 = 0M;
                num4 = nullable2.GetValueOrDefault() < num5 & nullable2.HasValue ? 1 : 0;
              }
              else
                num4 = 0;
              bool flag4 = num4 != 0;
              nullable2 = nullable11;
              Decimal num6 = 0M;
              int num7;
              if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
              {
                nullable2 = nullable13;
                Decimal num8 = 0M;
                num7 = !(nullable2.GetValueOrDefault() == num8 & nullable2.HasValue) ? 1 : 0;
              }
              else
                num7 = 0;
              int num9 = flag4 ? 1 : 0;
              if ((num7 | num9) != 0)
              {
                ARAdjust2 arAdjust2_3 = arAdjust2_1;
                nullable2 = arAdjust2_3.AdjAmt;
                nullable3 = nullable13;
                arAdjust2_3.AdjAmt = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                ARAdjust2 arAdjust2_4 = arAdjust2_1;
                nullable3 = arAdjust2_4.RGOLAmt;
                nullable1 = arAdjust2_1.ReverseGainLoss;
                Decimal? nullable15;
                if (nullable1.GetValueOrDefault())
                {
                  Decimal? nullable16 = nullable13;
                  nullable15 = nullable16.HasValue ? new Decimal?(-nullable16.GetValueOrDefault()) : new Decimal?();
                }
                else
                  nullable15 = nullable13;
                nullable2 = nullable15;
                arAdjust2_4.RGOLAmt = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
                GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments_Inv).Cache, (object) arAdjust2_1);
              }
            }
          }
        }
      }
      if (EnumerableExtensions.IsIn<PXEntryStatus>(status, (PXEntryStatus) 2, (PXEntryStatus) 1) && arInvoice.DocType == "CRM")
      {
        nullable1 = arInvoice.Released;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        {
          Decimal? nullable17 = new Decimal?(0M);
          foreach (ARAdjust application in EnumerableExtensions.WhereNotNull<ARAdjust>(GraphHelper.RowCast<ARAdjust>((IEnumerable) ((PXSelectBase) this.Adjustments_Crm).View.SelectMultiBound(new object[1]
          {
            (object) arInvoice
          }, Array.Empty<object>()))))
          {
            FullBalanceDelta fullBalanceDelta = application.GetFullBalanceDelta();
            nullable2 = nullable17;
            nullable1 = ARDocType.Payable(application.DisplayDocType);
            Decimal num10 = nullable1.GetValueOrDefault() ? fullBalanceDelta.CurrencyAdjustingBalanceDelta : fullBalanceDelta.CurrencyAdjustedBalanceDelta;
            Decimal? nullable18;
            if (!nullable2.HasValue)
            {
              nullable3 = new Decimal?();
              nullable18 = nullable3;
            }
            else
              nullable18 = new Decimal?(nullable2.GetValueOrDefault() + num10);
            nullable17 = nullable18;
            nullable3 = arInvoice.CuryDocBal;
            Decimal? nullable19 = nullable17;
            nullable2 = nullable3.HasValue & nullable19.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable19.GetValueOrDefault()) : new Decimal?();
            Decimal num11 = 0M;
            if (nullable2.GetValueOrDefault() < num11 & nullable2.HasValue)
            {
              nullable2 = nullable17;
              Decimal num12 = 0M;
              if (nullable2.GetValueOrDefault() > num12 & nullable2.HasValue)
              {
                GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments_1).Cache, (object) application);
                ((PXSelectBase) this.Adjustments_1).Cache.RaiseExceptionHandling<ARAdjust.displayCuryAmt>((object) application, (object) application.DisplayCuryAmt, (Exception) new PXSetPropertyException("The total application amount must not exceed the document amount."));
                throw new PXException("The total application amount must not exceed the document amount.");
              }
            }
          }
        }
      }
    }
    this.ValidateARDiscountDetails();
    this.InsertImportedTaxes();
    ((PXGraph) this).Persist();
    ARTran current = ((PXSelectBase<ARTran>) this.Transactions).Current;
    ((PXSelectBase) this.Transactions).Cache.Clear();
    ((PXSelectBase) this.Transactions).View.Clear();
    if (current == null)
      return;
    ((PXSelectBase<ARTran>) this.Transactions).Current = ARTran.PK.Find((PXGraph) this, current.TranType, current.RefNbr, current.LineNbr);
  }

  public virtual void ValidateARDiscountDetails()
  {
    List<ARInvoiceDiscountDetail> source = new List<ARInvoiceDiscountDetail>();
    foreach (PXResult<ARInvoiceDiscountDetail> pxResult in ((IEnumerable<PXResult<ARInvoiceDiscountDetail>>) ((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).Select(Array.Empty<object>())).ToList<PXResult<ARInvoiceDiscountDetail>>())
    {
      ARInvoiceDiscountDetail invoiceDiscountDetail = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult);
      source.Add(invoiceDiscountDetail);
    }
    if (source.GroupBy(x => new
    {
      DiscountID = x.DiscountID,
      DiscountSequenceID = x.DiscountSequenceID,
      Type = x.Type,
      OrderType = x.OrderType,
      OrderNbr = x.OrderNbr
    }).Where<IGrouping<\u003C\u003Ef__AnonymousType13<string, string, string, string, string>, ARInvoiceDiscountDetail>>(gr => gr.Count<ARInvoiceDiscountDetail>() > 1 && gr.Key.Type != "B").Select(gr => gr.Key).Count() <= 0)
      return;
    this.ARDiscountEngine.ValidateDiscountDetails((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails);
  }

  [PXUIField(DisplayName = "Notifications", Visible = false)]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable Notification(PXAdapter adapter, [PXString] string notificationCD)
  {
    List<ARInvoice> list = adapter.Get<ARInvoice>().ToList<ARInvoice>();
    ARInvoiceEntry_ActivityDetailsExt activityExt = ((PXGraph) this).GetExtension<ARInvoiceEntry_ActivityDetailsExt>();
    activityExt.SendNotifications((Func<ARInvoice, string>) (doc => !ProjectDefaultAttribute.IsProject((PXGraph) this, doc.ProjectID) || !activityExt.IsProjectSourceActive(doc.ProjectID, notificationCD) ? "Customer" : "Project"), notificationCD, (IList<ARInvoice>) list, (Func<ARInvoice, int?>) (doc => doc.BranchID), (Func<ARInvoice, IDictionary<string, string>>) (doc => (IDictionary<string, string>) new Dictionary<string, string>()
    {
      ["DocType"] = doc.DocType,
      ["RefNbr"] = doc.RefNbr
    }), new MassEmailingActionParameters(adapter), (Func<ARInvoice, object>) (doc => (object) doc.CustomerID));
    ((PXAction) this.Save).Press();
    return (IEnumerable) list;
  }

  public override string GetCustomerReportID(string reportID, ARInvoice doc)
  {
    ((PXSelectBase<ARInvoice>) this.Document).Current = doc;
    ARInvoiceEntry_ActivityDetailsExt extension = ((PXGraph) this).GetExtension<ARInvoiceEntry_ActivityDetailsExt>();
    string customerReportId = (string) null;
    if (!ProjectDefaultAttribute.IsProject((PXGraph) this, doc.ProjectID) || extension.ProjectInvoiceReportActive(doc.ProjectID) == null || reportID != "AR641000")
      customerReportId = new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, doc.CustomerID, doc.BranchID);
    return customerReportId;
  }

  public virtual IEnumerable adjustments()
  {
    ARInvoiceEntry arInvoiceEntry = this;
    ARInvoice current = ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current;
    if ((current != null ? (!current.CustomerID.HasValue ? 1 : 0) : 1) == 0)
    {
      ((PXSelectBase) arInvoiceEntry.Adjustments).Cache.ClearQueryCache();
      int num;
      if (((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.DocType == "INV" || ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.DocType == "DRM")
      {
        bool? nullable = ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.Released;
        if (!nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.Scheduled;
          if (!nullable.GetValueOrDefault())
          {
            nullable = ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.Voided;
            if (!nullable.GetValueOrDefault())
            {
              nullable = ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.IsLoadApplications;
              if (nullable.GetValueOrDefault())
              {
                nullable = ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.FromSchedule;
                num = !nullable.GetValueOrDefault() ? 1 : 0;
                goto label_10;
              }
            }
          }
        }
      }
      num = 0;
label_10:
      bool allowLoadDocuments = num != 0;
      foreach (PXResult<ARAdjust2, ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction> pxResult in arInvoiceEntry.SelectAdjustmentsRaw(!allowLoadDocuments))
      {
        ARPayment payment = PXResult<ARAdjust2, ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
        ARAdjust2 adj = PXResult<ARAdjust2, ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
        PX.Objects.CM.Extensions.CurrencyInfo info = PXResult<ARAdjust2, ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
        ((PXGraph) arInvoiceEntry).GetExtension<ARInvoiceEntry.MultiCurrency>().StoreCached(info);
        PXCache<ARRegister>.RestoreCopy((ARRegister) payment, (ARRegister) PXResult<ARAdjust2, ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult));
        if (adj != null)
        {
          if (((PXSelectBase) arInvoiceEntry.Adjustments).Cache.GetStatus((object) adj) == null)
            arInvoiceEntry.CalcBalancesFromInvoiceSide(adj, payment, true, true);
          yield return (object) new PXResult<ARAdjust2, ARPayment, ExternalTransaction>(adj, payment, PXResult<ARAdjust2, ARPayment, ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult));
        }
      }
      if (allowLoadDocuments)
      {
        foreach (PXResult<ARAdjust2, ARPayment> pxResult in (PXResultset<ARAdjust2>) arInvoiceEntry.LoadDocumentsProc())
          yield return (object) pxResult;
        ((PXSelectBase<ARInvoice>) arInvoiceEntry.Document).Current.IsLoadApplications = new bool?(false);
      }
      else
        PXView.StartRow = 0;
    }
  }

  public virtual IEnumerable adjustments_1()
  {
    PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, new Dictionary<System.Type, System.Type>()
    {
      {
        typeof (ARAdjust.displayDocType),
        typeof (ARRegisterAlias.docType)
      },
      {
        typeof (ARAdjust.displayRefNbr),
        typeof (ARRegisterAlias.refNbr)
      },
      {
        typeof (ARAdjust.displayDocDate),
        typeof (ARRegisterAlias.docDate)
      },
      {
        typeof (ARAdjust.displayDocDesc),
        typeof (ARRegisterAlias.docDesc)
      },
      {
        typeof (ARAdjust.displayCustomerID),
        typeof (ARRegisterAlias.customerID)
      },
      {
        typeof (ARAdjust.displayBranchID),
        typeof (ARRegisterAlias.branchID)
      },
      {
        typeof (ARAdjust.displayCuryID),
        typeof (ARRegisterAlias.curyID)
      },
      {
        typeof (ARAdjust.displayFinPeriodID),
        typeof (ARRegisterAlias.finPeriodID)
      },
      {
        typeof (ARAdjust.displayStatus),
        typeof (ARRegisterAlias.status)
      },
      {
        typeof (ARAdjust.displayCuryInfoID),
        typeof (ARRegisterAlias.curyInfoID)
      },
      {
        typeof (ARAdjust.displayProcStatus),
        typeof (ExternalTransaction.procStatus)
      }
    }, new System.Type[2]{ typeof (ARAdjust), typeof (ARInvoice) });
    PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult();
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    Guid? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = current1.NoteID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 == 0)
    {
      ARInvoice current2 = ((PXSelectBase<ARInvoice>) this.Document).Current;
      if ((current2 != null ? (!current2.CustomerID.HasValue ? 1 : 0) : 1) == 0)
      {
        PXSelect<ARAdjust> pxSelect = new PXSelect<ARAdjust>((PXGraph) this);
        if (((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "INV")
        {
          ((PXSelectBase<ARAdjust>) pxSelect).Join<LeftJoin<ARRegisterAlias, On<ARRegisterAlias.noteID, Equal<ARAdjust.memoID>, And<BqlField<ARInvoice.docType, IBqlString>.FromCurrent, Equal<ARDocType.invoice>, Or<ARRegisterAlias.noteID, Equal<IsNull<ARAdjust.invoiceID, ARAdjust.paymentID>>>>>>>();
          ((PXSelectBase<ARAdjust>) pxSelect).WhereNew<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ARInvoice.docType>, NotEqual<ARDocType.creditMemo>>>>, And<BqlOperand<Current<ARInvoice.released>, IBqlBool>.IsEqual<True>>>, And<BqlOperand<Current<ARInvoice.isMigratedRecord>, IBqlBool>.IsEqual<True>>>, And<BqlOperand<Current<ARInvoice.curyInitDocBal>, IBqlDecimal>.IsNotEqual<BqlField<ARInvoice.curyOrigDocAmt, IBqlDecimal>.FromCurrent>>>, And<BqlOperand<ARAdjust.invoiceID, IBqlGuid>.IsEqual<BqlField<ARInvoice.noteID, IBqlGuid>.FromCurrent>>>>.And<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsEqual<ARRegisterAlias.docType>>>>();
        }
        else
        {
          ((PXSelectBase<ARAdjust>) pxSelect).Join<LeftJoin<ARRegisterAlias, On<ARAdjust.memoID, Equal<BqlField<ARInvoice.noteID, IBqlGuid>.FromCurrent>, And<Where<ARRegisterAlias.noteID, Equal<IsNull<ARAdjust.invoiceID, ARAdjust.paymentID>>>>>>>();
          ((PXSelectBase<ARAdjust>) pxSelect).WhereNew<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<ARInvoice.docType>, Equal<ARDocType.creditMemo>>>>>.And<BqlOperand<ARAdjust.memoID, IBqlGuid>.IsEqual<BqlField<ARInvoice.noteID, IBqlGuid>.FromCurrent>>>>();
        }
        ((PXSelectBase<ARAdjust>) pxSelect).Join<LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, LeftJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<ARPayment.cCActualExternalTransactionID>>>>>>>();
        foreach (PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction> pxResult in pxResultMapper.Select(((PXSelectBase) pxSelect).View))
        {
          ARAdjust adj1 = PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
          ARRegisterAlias arRegisterAlias1 = PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
          ARInvoice arInvoice = PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
          ARPayment arPayment = PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
          PX.Objects.CM.Extensions.CurrencyInfo info = PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
          ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().StoreCached(info);
          PXCache<ARRegister>.RestoreCopy((ARRegister) arInvoice, (ARRegister) PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult));
          ARRegisterAlias arRegisterAlias2 = PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult);
          PXCache<ARRegister>.RestoreCopy((ARRegister) arPayment, (ARRegister) arRegisterAlias2);
          if (adj1 != null)
          {
            ARAdjust arAdjust = adj1;
            nullable = adj1.InvoiceID;
            string str = nullable.HasValue ? "D" : "G";
            arAdjust.AdjType = str;
            ((PXCache) GraphHelper.Caches<ARAdjust>((PXGraph) this)).RaiseFieldUpdated<ARAdjust.adjType>((object) adj1, (object) null);
            nullable = adj1.InvoiceID;
            if (nullable.HasValue && ((PXSelectBase) this.Adjustments_1).Cache.GetStatus((object) PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult)) == null)
            {
              ARAdjust adj2 = adj1;
              ARInvoice invoice = arInvoice;
              bool? released1 = adj1.Released;
              bool flag1 = false;
              int num2 = released1.GetValueOrDefault() == flag1 & released1.HasValue ? 1 : 0;
              bool? released2 = adj1.Released;
              bool flag2 = false;
              int num3 = released2.GetValueOrDefault() == flag2 & released2.HasValue ? 1 : 0;
              this.CalcBalances<ARInvoice>(adj2, invoice, num2 != 0, num3 != 0);
            }
            nullable = adj1.PaymentID;
            if (nullable.HasValue)
              this.CalcBalancesFromInvoiceSide(adj1, true, true);
            ((List<object>) delegateResult).Add(pxResultMapper.CreateResult((PXResult) new PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ExternalTransaction>(adj1, arRegisterAlias1, arInvoice, PXResult<ARAdjust, ARRegisterAlias, ARInvoice, ARPayment, PX.Objects.CM.Extensions.CurrencyInfo, ExternalTransaction>.op_Implicit(pxResult))));
          }
        }
        return (IEnumerable) delegateResult;
      }
    }
    return (IEnumerable) delegateResult;
  }

  public virtual void LoadInvoicesProc()
  {
    try
    {
      ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
      if ((current1 != null ? (!current1.CustomerID.HasValue ? 1 : 0) : 1) == 0)
      {
        bool? openDoc = ((PXSelectBase<ARInvoice>) this.Document).Current.OpenDoc;
        bool flag = false;
        if (!(openDoc.GetValueOrDefault() == flag & openDoc.HasValue) && !(((PXSelectBase<ARInvoice>) this.Document).Current.DocType != "INV"))
        {
          GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<ARInvoice>) this.Document).Current);
          ((PXSelectBase) this.Document).Cache.IsDirty = true;
          Decimal? nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDocBal;
          this.LoadDocumentsProc();
          using (IEnumerator<ARAdjust2> enumerator = GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase<ARAdjust2>) this.Adjustments).Select(Array.Empty<object>())).Select<ARAdjust2, ARAdjust2>(new Func<ARAdjust2, ARAdjust2>(PXCache<ARAdjust2>.CreateCopy)).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              ARAdjust2 current2 = enumerator.Current;
              Decimal? nullable2 = nullable1;
              Decimal? nullable3 = current2.CuryDocBal;
              if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
              {
                current2.CuryAdjdAmt = current2.CuryDocBal;
                nullable3 = nullable1;
                nullable2 = current2.CuryAdjdAmt;
                nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
              }
              else
              {
                current2.CuryAdjdAmt = nullable1;
                nullable1 = new Decimal?(0M);
              }
              ((PXSelectBase) this.Adjustments).Cache.Update((object) current2);
              nullable2 = nullable1;
              Decimal num = 0M;
              if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
                throw new ARInvoiceEntry.PXLoadInvoiceException();
            }
            return;
          }
        }
      }
      throw new ARInvoiceEntry.PXLoadInvoiceException();
    }
    catch (ARInvoiceEntry.PXLoadInvoiceException ex)
    {
    }
  }

  protected virtual void ARTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    bool? nullable1 = row.IsStockItem;
    int num1;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.AccrueCost;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    if (flag)
    {
      PXDefaultAttribute.SetPersistingCheck<ARTran.costBasis>(sender, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<ARTran.expenseAccrualAccountID>(sender, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<ARTran.expenseAccrualSubID>(sender, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<ARTran.expenseAccountID>(sender, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<ARTran.expenseSubID>(sender, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
      if ((e.Operation & 3) == 2 || (e.Operation & 3) == 1)
      {
        ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
        int num2;
        if (current == null)
        {
          num2 = 1;
        }
        else
        {
          nullable1 = current.IsRetainageDocument;
          num2 = !nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num2 != 0 && row != null)
        {
          int? nullable2 = row.ProjectID;
          if (nullable2.HasValue && !ProjectDefaultAttribute.IsNonProject(row.ProjectID))
          {
            nullable2 = row.TaskID;
            if (nullable2.HasValue)
            {
              PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, row.ExpenseAccountID);
              if (account != null)
              {
                nullable2 = account.AccountGroupID;
                if (!nullable2.HasValue)
                  throw new PXRowPersistingException(typeof (ARTran.expenseAccountID).Name, (object) account.AccountCD, "Record is associated with Project whereas Account '{0}' is not associated with any Account Group", new object[1]
                  {
                    (object) account.AccountCD
                  });
              }
            }
          }
        }
      }
    }
    if (row.DeferredCode != null)
    {
      DRDeferredCode drDeferredCode = (DRDeferredCode) PXSelectorAttribute.Select<ARTran.deferredCode>(sender, (object) row);
      int num3 = drDeferredCode != null ? 1 : 0;
      nullable1 = drDeferredCode.MultiDeliverableArrangement;
      int num4 = nullable1.GetValueOrDefault() ? 1 : 0;
      if ((num3 & num4) != 0)
      {
        PX.Objects.IN.InventoryItem byId1 = this.InventoryItemGetByID(row.InventoryID);
        DRDeferredCode byId2 = byId1 == null ? (DRDeferredCode) null : this.DeferredCodeGetByID(byId1.DeferredCode);
        if (byId2 != null)
        {
          nullable1 = byId2.MultiDeliverableArrangement;
          if (nullable1.GetValueOrDefault())
            goto label_21;
        }
        if (sender.RaiseExceptionHandling<ARTran.deferredCode>((object) row, (object) drDeferredCode.DeferredCodeID, (Exception) new PXSetPropertyException<ARTran.deferredCode>("An MDA code is allowed only with Multiple-Deliverable Arrangement items")))
          throw new PXRowPersistingException(typeof (ARTran.deferredCode).Name, (object) drDeferredCode.DeferredCodeID, "An MDA code is allowed only with Multiple-Deliverable Arrangement items");
      }
    }
label_21:
    Decimal? nullable3 = row.CuryExtPrice;
    int num5 = Math.Sign(nullable3.GetValueOrDefault());
    nullable3 = row.CuryRetainageAmt;
    int num6 = Math.Sign(nullable3.GetValueOrDefault());
    if ((Decimal) (num5 * num6) < 0M)
      throw new PXRowPersistingException(typeof (ARTran.curyRetainageAmt).Name, (object) row.CuryRetainageAmt, "The line retainage amount must have the same sign as the line amount.");
    nullable3 = row.CuryExtPrice;
    Decimal num7 = 0M;
    if (nullable3.GetValueOrDefault() < num7 & nullable3.HasValue)
    {
      nullable3 = row.CuryCashDiscBal;
      if (nullable3.GetValueOrDefault() > 0M)
        throw new PXRowPersistingException(typeof (ARTran.curyCashDiscBal).Name, (object) row.CuryDiscAmt, "The amount must be less than or equal to {0}.", new object[1]
        {
          (object) 0
        });
    }
    ScheduleHelper.DeleteAssociatedScheduleIfDeferralCodeChanged(sender, row);
  }

  protected virtual void ARInvoice_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel || !(e.Row is ARInvoice row))
      return;
    bool? nullable = row.IsCancellation;
    if (nullable.GetValueOrDefault())
      return;
    nullable = row.IsCorrection;
    if (nullable.GetValueOrDefault())
      return;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  public virtual string GetDefaultTaxZone(ARInvoice row)
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

  private bool IsTaxZoneDerivedFromCustomer()
  {
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
    return location != null && !string.IsNullOrEmpty(location.CTaxZoneID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.branchID> e)
  {
    ARInvoice row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.branchID>>) e).Cache.SetDefaultExt<ARInvoice.taxZoneID>((object) row);
    ARInvoice.paymentMethodID parent = PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<ARInvoice>.By<ARInvoice.paymentMethodID>.FindParent((PXGraph) this, (ARInvoice.paymentMethodID) row, (PKFindOptions) 0);
    if ((parent != null ? (EnumerableExtensions.IsIn<string>(((PX.Objects.CA.PaymentMethod) parent).PaymentType, "CCD", "EFT") ? 1 : 0) : 0) == 0)
      return;
    ARInvoice copy = (ARInvoice) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.branchID>>) e).Cache.CreateCopy((object) row);
    copy.BranchID = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.branchID>, ARInvoice, object>) e).OldValue;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.branchID>>) e).Cache.RaiseFieldDefaulting<ARInvoice.pMInstanceID>((object) copy, ref obj);
    int? nullable = (int?) obj;
    int? pmInstanceId = row.PMInstanceID;
    if (!(nullable.GetValueOrDefault() == pmInstanceId.GetValueOrDefault() & nullable.HasValue == pmInstanceId.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.branchID>>) e).Cache.SetDefaultExt<ARInvoice.pMInstanceID>((object) row);
  }

  protected virtual void ARShippingAddress_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARShippingAddress row = e.Row as ARShippingAddress;
    ARShippingAddress oldRow = e.OldRow as ARShippingAddress;
    if (row == null)
      return;
    int? customerId1 = oldRow.CustomerID;
    int? customerId2 = row.CustomerID;
    if (!(customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue) || this.IsTaxZoneDerivedFromCustomer() || ((PXSelectBase<ARInvoice>) this.Document).Current.Released.GetValueOrDefault() || (string.IsNullOrEmpty(row.PostalCode) || !(oldRow.PostalCode != row.PostalCode)) && (string.IsNullOrEmpty(row.CountryID) || !(oldRow.CountryID != row.CountryID)) && (string.IsNullOrEmpty(row.State) || !(oldRow.State != row.State)))
      return;
    string taxZoneByAddress = TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, (IAddressBase) row);
    if (taxZoneByAddress == null)
    {
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<ARInvoice.taxZoneID>((object) ((PXSelectBase<ARInvoice>) this.Document).Current);
    }
    else
    {
      if (((PXSelectBase<ARInvoice>) this.Document).Current == null || !(((PXSelectBase<ARInvoice>) this.Document).Current.TaxZoneID != taxZoneByAddress))
        return;
      ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) this.Document).Current);
      ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.taxZoneID>((object) ((PXSelectBase<ARInvoice>) this.Document).Current, (object) taxZoneByAddress);
      ((PXSelectBase) this.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<ARInvoice>) this.Document).Current, (object) copy);
    }
  }

  protected virtual void ARInvoice_DocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "INV";
  }

  public object GetAcctSub<Field>(PXCache cache, object data) where Field : IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }

  protected virtual void ARInvoice_ARAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null || e.Row == null)
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aRAccountID>(((PXSelectBase) this.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current);
  }

  protected virtual void ARInvoice_ARSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null || e.Row == null)
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aRSubID>(((PXSelectBase) this.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current);
  }

  protected virtual void ARInvoice_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.location.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<ARInvoice.aRAccountID>(e.Row);
    sender.SetDefaultExt<ARInvoice.aRSubID>(e.Row);
    sender.SetDefaultExt<ARInvoice.taxZoneID>(e.Row);
    sender.SetDefaultExt<ARInvoice.taxCalcMode>(e.Row);
    sender.SetDefaultExt<ARInvoice.externalTaxExemptionNumber>(e.Row);
    sender.SetDefaultExt<ARInvoice.avalaraCustomerUsageType>(e.Row);
    sender.SetDefaultExt<ARInvoice.salesPersonID>(e.Row);
    sender.SetDefaultExt<ARInvoice.workgroupID>(e.Row);
    sender.SetDefaultExt<ARInvoice.ownerID>(e.Row);
    sender.SetDefaultExt<ARInvoice.paymentsByLinesAllowed>(e.Row);
    object projectId = (object) ((ARInvoice) e.Row).ProjectID;
    if (ProjectDefaultAttribute.IsProject((PXGraph) this, ((ARInvoice) e.Row).ProjectID))
    {
      try
      {
        sender.RaiseFieldVerifying<ARInvoice.projectID>(e.Row, ref projectId);
      }
      catch (PXSetPropertyException ex)
      {
        ((ARInvoice) e.Row).ProjectID = new int?();
        sender.SetValuePending<ARInvoice.projectID>(e.Row, projectId);
      }
    }
    SharedRecordAttribute.DefaultRecord<ARInvoice.shipAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<ARInvoice.shipContactID>(sender, e.Row);
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void ARInvoice_CustomerID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARInvoice_PaymentsByLinesAllowed_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARInvoice row) || !((bool?) e.OldValue).GetValueOrDefault() || row.PaymentsByLinesAllowed.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<ARInvoice.curyDiscTot>((object) row, (object) row.CuryDiscTot, (Exception) null);
    sender.RaiseExceptionHandling<ARInvoice.curyTaxTotal>((object) row, (object) row.CuryTaxTotal, (Exception) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.retainageApply> e)
  {
    ARInvoice row = e.Row;
    if (row == null)
      return;
    bool? nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.retainageApply>, ARInvoice, object>) e).OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.RetainageApply;
    if (nullable.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARInvoice, ARInvoice.retainageApply>>) e).Cache.RaiseExceptionHandling<ARInvoice.curyDiscTot>((object) row, (object) row.CuryDiscTot, (Exception) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARInvoice, ARInvoice.curyOrigDiscAmt> e)
  {
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARInvoice, ARInvoice.curyOrigDiscAmt>, ARInvoice, object>) e).NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() < num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected virtual void ARInvoice_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    ((PXSetup<Customer, Where<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<BqlField<ARInvoice.customerID, IBqlInt>.AsOptional>>>) this.customer).RaiseFieldUpdated(sender, e.Row);
    if (((PXSelectBase<Customer>) this.customer).Current != null)
    {
      row.ApplyOverdueCharge = ((PXSelectBase<Customer>) this.customer).Current.FinChargeApply;
      if (!e.ExternalCall)
        ((PXSelectBase<Customer>) this.customer).Current.CreditRule = (string) null;
    }
    ((PXSelectBase) this.Adjustments_Inv).Cache.Clear();
    ((PXSelectBase) this.Adjustments_Inv).Cache.ClearQueryCacheObsolete();
    EnumerableExtensions.ForEach<ARAdjust2>(GraphHelper.RowCast<ARAdjust2>((IEnumerable) PXSelectBase<ARAdjust2, PXSelect<ARAdjust2, Where<ARAdjust2.adjdDocType, Equal<Required<ARInvoice.docType>>, And<ARAdjust2.adjdRefNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    })), (System.Action<ARAdjust2>) (application => ((PXSelectBase) this.Adjustments).Cache.Delete((object) application)));
    object projectId = (object) ((ARInvoice) e.Row).ProjectID;
    if (!ProjectDefaultAttribute.IsProject((PXGraph) this, ((ARInvoice) e.Row).ProjectID))
      return;
    try
    {
      sender.RaiseFieldVerifying<ARInvoice.projectID>(e.Row, ref projectId);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling<ARInvoice.projectID>(e.Row, projectId, (Exception) ex);
    }
  }

  private void SetDefaultsAfterCustomerIDChanging(PXCache sender, ARInvoice row)
  {
    sender.SetDefaultExt<ARInvoice.customerLocationID>((object) row);
    sender.SetDefaultExt<ARRegister.dontPrint>((object) row);
    sender.SetDefaultExt<ARInvoice.dontEmail>((object) row);
    try
    {
      SharedRecordAttribute.DefaultRecord<ARInvoice.billAddressID>(sender, (object) row);
      SharedRecordAttribute.DefaultRecord<ARInvoice.billContactID>(sender, (object) row);
    }
    catch (PXFieldValueProcessingException ex)
    {
      string acctCd = ((PXSelectBase<Customer>) this.customer).Current.AcctCD;
      ((PXSetPropertyException) ex).ErrorValue = (object) acctCd;
      throw;
    }
    sender.SetDefaultExt<ARInvoice.taxZoneID>((object) row);
    sender.SetDefaultExt<ARInvoice.paymentMethodID>((object) row);
  }

  protected virtual void ARInvoice_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null || e.IsReadOnly || string.IsNullOrEmpty(row.DocType) || string.IsNullOrEmpty(row.RefNbr))
      return;
    Decimal? nullable = row.CuryPaymentTotal;
    if (nullable.HasValue)
    {
      nullable = row.CuryCCAuthorizedAmt;
      if (nullable.HasValue)
      {
        nullable = row.CuryPaidAmt;
        if (nullable.HasValue)
        {
          nullable = row.CuryUnreleasedPaymentAmt;
          if (nullable.HasValue)
          {
            nullable = row.CuryBalanceWOTotal;
            if (nullable.HasValue)
              return;
          }
        }
      }
    }
    bool flag = sender.GetStatus(e.Row) == 0;
    if (row.DocType == "CRM")
      PXFormulaAttribute.CalcAggregate<ARAdjust.curyAdjdAmt>(((PXSelectBase) this.Adjustments_1).Cache, e.Row, flag);
    else
      PXFormulaAttribute.CalcAggregate<ARAdjust2.curyAdjdAmt>(((PXSelectBase) this.Adjustments).Cache, e.Row, flag);
    sender.RaiseFieldUpdated<ARInvoice.curyPaymentTotal>(e.Row, (object) null);
    sender.RaiseFieldUpdated<ARInvoice.curyUnreleasedPaymentAmt>(e.Row, (object) null);
    sender.RaiseFieldUpdated<ARInvoice.curyCCAuthorizedAmt>(e.Row, (object) null);
    sender.RaiseFieldUpdated<ARInvoice.curyPaidAmt>(e.Row, (object) null);
    if (!(row.DocType != "CRM"))
      return;
    PXFormulaAttribute.CalcAggregate<ARAdjust2.curyAdjdWOAmt>(((PXSelectBase) this.Adjustments).Cache, e.Row, flag);
    sender.RaiseFieldUpdated<ARInvoice.curyBalanceWOTotal>(e.Row, (object) null);
  }

  protected virtual void ARInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    bool flag1 = (row.DocType != "CRM" || row.DocType == "CRM" && row.TermsID != null) && row.DocType != "SMC";
    bool flag2 = row.DocType == "CSL" || row.DocType == "RCS";
    if (flag1 && row.DocType != "CRM" && string.IsNullOrEmpty(row.TermsID))
    {
      if (sender.RaiseExceptionHandling<ARInvoice.termsID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[termsID]"
      })))
        throw new PXRowPersistingException(typeof (ARInvoice.termsID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "termsID"
        });
    }
    DateTime? nullable1;
    if (flag1 && !flag2)
    {
      nullable1 = row.DueDate;
      if (!nullable1.HasValue)
      {
        if (sender.RaiseExceptionHandling<ARInvoice.dueDate>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[dueDate]"
        })))
          throw new PXRowPersistingException(typeof (ARInvoice.dueDate).Name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) "dueDate"
          });
      }
    }
    if (flag1 && !flag2)
    {
      nullable1 = row.DiscDate;
      if (!nullable1.HasValue)
      {
        if (sender.RaiseExceptionHandling<ARInvoice.discDate>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[discDate]"
        })))
          throw new PXRowPersistingException(typeof (ARInvoice.discDate).Name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) "discDate"
          });
      }
    }
    if (row.DocType == "FCH")
      AutoNumberAttribute.SetNumberingId<ARInvoice.refNbr>(sender, row.DocType, ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.FinChargeNumberingID);
    if ((e.Operation & 3) == 2 && row.DocType == "FCH" && ((PXGraph) this).Accessinfo.ScreenID == "AR.30.10.00")
      throw new PXException("Financial charges cannot be entered directly. Please use Overdue Charges calculation process.");
    Decimal? curyDiscTot = row.CuryDiscTot;
    Decimal num1 = Math.Abs(row.CuryLineTotal.GetValueOrDefault());
    if (curyDiscTot.GetValueOrDefault() > num1 & curyDiscTot.HasValue && sender.RaiseExceptionHandling<ARInvoice.curyDiscTot>(e.Row, (object) row.CuryDiscTot, (Exception) new PXSetPropertyException("The total amount of line and document discounts cannot exceed the Detail Total amount.", (PXErrorLevel) 4)))
      throw new PXRowPersistingException(typeof (ARInvoice.curyDiscTot).Name, (object) null, "The total amount of line and document discounts cannot exceed the Detail Total amount.");
    if ((e.Operation & 3) == 3)
    {
      if (PXResultset<PX.Objects.GL.GLTran>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>, And<PX.Objects.GL.GLTran.tranType, Equal<Required<PX.Objects.GL.GLTran.tranType>>, And<PX.Objects.GL.GLTran.released, Equal<True>, And<PX.Objects.GL.GLTran.module, Equal<Required<PX.Objects.GL.GLTran.module>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
      {
        (object) row.RefNbr,
        (object) row.DocType,
        (object) "AR"
      })) != null)
        throw new PXException("The document cannot be deleted because GL batches have been released for the document. To resolve the issue, please contact your Acumatica support provider.");
      if (PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.released, Equal<True>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
      {
        (object) row.RefNbr,
        (object) row.DocType
      })) != null)
        throw new PXException("The document cannot be deleted as it has been partially released. To resolve the issue, please contact your Acumatica support provider.");
    }
    Decimal? curyOrigDiscAmt = row.CuryOrigDiscAmt;
    Decimal num2 = 0M;
    if (curyOrigDiscAmt.GetValueOrDefault() == num2 & curyOrigDiscAmt.HasValue)
      return;
    bool? nullable2 = row.PaymentsByLinesAllowed;
    if (!nullable2.GetValueOrDefault())
    {
      nullable2 = row.RetainageApply;
      if (!nullable2.GetValueOrDefault())
        return;
    }
    foreach (PXResult<ARTaxTran, PX.Objects.TX.Tax> pxResult in ((PXSelectBase<ARTaxTran>) this.Taxes).Select(Array.Empty<object>()))
    {
      PX.Objects.TX.Tax tax = PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      if (tax.TaxApplyTermsDisc == "P")
      {
        sender.RaiseExceptionHandling<ARInvoice.curyOrigDiscAmt>(e.Row, (object) row.CuryOrigDiscAmt, (Exception) new PXSetPropertyException("VAT recalculated on cash discounts is not supported in documents with retainage or documents paid by lines.", (PXErrorLevel) 4));
        break;
      }
    }
  }

  protected virtual void ARInvoice_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null || !row.Released.GetValueOrDefault() || (e.Operation & 3) != 1)
      return;
    OldInvoiceDateRefresher invoiceDateRefresher = new OldInvoiceDateRefresher();
    invoiceDateRefresher.RecordDocument(row.BranchID, row.CustomerID, row.CustomerLocationID);
    invoiceDateRefresher.CommitRefresh((PXGraph) this);
  }

  protected virtual void ARInvoice_DocDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARInvoice invoice = (ARInvoice) e.Row;
    if (!(invoice.DocType == "CRM") || !invoice.DocDate.HasValue)
      return;
    foreach (ARAdjust arAdjust in GraphHelper.RowCast<ARAdjust>((IEnumerable) ((PXSelectBase<ARAdjust>) this.Adjustments_Crm).Select(Array.Empty<object>())).Where<ARAdjust>((Func<ARAdjust, bool>) (adj =>
    {
      if (ARDocType.Payable(adj.DisplayDocType).GetValueOrDefault())
        return false;
      DateTime? adjdDocDate = adj.AdjdDocDate;
      DateTime? docDate = invoice.DocDate;
      if (adjdDocDate.HasValue != docDate.HasValue)
        return true;
      return adjdDocDate.HasValue && adjdDocDate.GetValueOrDefault() != docDate.GetValueOrDefault();
    })))
    {
      arAdjust.AdjdDocDate = invoice.DocDate;
      ((PXSelectBase) this.Adjustments_Crm).Cache.Update((object) arAdjust);
    }
  }

  protected virtual void ARInvoice_DocDesc_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
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

  protected virtual void ARInvoice_TermsID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARInvoice row = e.Row as ARInvoice;
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<ARInvoice.termsID>(sender, e.Row);
    if (terms != null && terms.InstallmentType != "S")
    {
      foreach (PXResult<ARAdjust2> pxResult in ((PXSelectBase<ARAdjust2>) this.Adjustments).Select(Array.Empty<object>()))
        ((PXSelectBase) this.Adjustments).Cache.Delete((object) PXResult<ARAdjust2>.op_Implicit(pxResult));
    }
    if (!((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.TermsInCreditMemos.GetValueOrDefault() || !(row?.DocType == "CRM") || terms != null)
      return;
    sender.SetValueExt<ARInvoice.curyOrigDiscAmt>((object) row, (object) 0M);
  }

  protected virtual void ARInvoice_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARInvoice.pMInstanceID>(e.Row);
    sender.SetDefaultExt<ARInvoice.cashAccountID>(e.Row);
  }

  protected virtual void ARInvoice_PMInstanceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARInvoice.cashAccountID>(e.Row);
  }

  public virtual ARInvoiceState GetDocumentState(PXCache cache, ARInvoice doc)
  {
    if (cache == null)
      throw new PXArgumentException(nameof (cache));
    if (doc == null)
      throw new PXArgumentException(nameof (doc));
    ARInvoiceState documentState = new ARInvoiceState()
    {
      PaymentsByLinesAllowed = doc.PaymentsByLinesAllowed.GetValueOrDefault(),
      RetainageApply = doc.RetainageApply.GetValueOrDefault(),
      IsRetainageDocument = doc.IsRetainageDocument.GetValueOrDefault(),
      IsDocumentReleased = doc.Released.GetValueOrDefault(),
      IsDocumentInvoice = doc.DocType == "INV",
      IsDocumentPrepaymentInvoice = doc.DocType == "PPI",
      IsDocumentCreditMemo = doc.DocType == "CRM",
      IsDocumentDebitMemo = doc.DocType == "DRM",
      IsDocumentFinCharge = doc.DocType == "FCH",
      IsDocumentSmallCreditWO = doc.DocType == "SMC",
      IsRetainageReversing = (doc.IsOriginalRetainageDocument() || doc.IsRetainageDocument.GetValueOrDefault()) && doc.IsRetainageReversing.GetValueOrDefault(),
      RetainTaxes = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetainTaxes.GetValueOrDefault(),
      IsDocumentOnHold = doc.Hold.GetValueOrDefault(),
      IsDocumentOnCreditHold = doc.CreditHold.GetValueOrDefault(),
      IsDocumentScheduled = doc.Scheduled.GetValueOrDefault(),
      IsDocumentVoided = doc.Voided.GetValueOrDefault(),
      IsDocumentRejected = doc.Rejected.GetValueOrDefault()
    };
    ARInvoiceState arInvoiceState1 = documentState;
    bool? nullable;
    int num1;
    if (documentState.IsDocumentInvoice || documentState.IsDocumentDebitMemo)
    {
      nullable = doc.Released;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        num1 = doc.CustomerID.HasValue ? 1 : 0;
        goto label_8;
      }
    }
    num1 = 0;
label_8:
    arInvoiceState1.InvoiceUnreleased = num1 != 0;
    documentState.IsPrepaymentInvoiceReversing = documentState.IsDocumentCreditMemo && doc.OrigDocType == "PPI";
    documentState.IsRetainageApplyDocument = !documentState.IsPrepaymentInvoiceReversing && ((documentState.IsDocumentInvoice || documentState.IsDocumentCreditMemo) && doc.OrigModule == "AR" && !documentState.IsDocumentReleased && !documentState.IsRetainageDocument || documentState.RetainageApply);
    ARInvoiceState arInvoiceState2 = documentState;
    int num2;
    if (!documentState.IsDocumentOnHold && !documentState.IsDocumentScheduled && !documentState.IsDocumentReleased && !documentState.IsDocumentVoided)
    {
      if (!documentState.IsDocumentRejected)
      {
        nullable = doc.Approved;
        if (!nullable.GetValueOrDefault())
        {
          nullable = doc.DontApprove;
          num2 = !nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num2 = 0;
      }
      else
        num2 = 1;
    }
    else
      num2 = 0;
    arInvoiceState2.IsDocumentRejectedOrPendingApproval = num2 != 0;
    ARInvoiceState arInvoiceState3 = documentState;
    int num3;
    if (!documentState.IsDocumentOnHold && !documentState.IsDocumentScheduled && !documentState.IsDocumentReleased && !documentState.IsDocumentVoided)
    {
      nullable = doc.Approved;
      if (nullable.GetValueOrDefault())
      {
        nullable = doc.DontApprove;
        num3 = !nullable.GetValueOrDefault() ? 1 : 0;
        goto label_19;
      }
    }
    num3 = 0;
label_19:
    arInvoiceState3.IsDocumentApprovedBalanced = num3 != 0;
    ARInvoiceState arInvoiceState4 = documentState;
    int num4;
    if (((PXSelectBase<Customer>) this.customer).Current != null)
    {
      nullable = ((PXSelectBase<Customer>) this.customer).Current.AllowOverrideCury;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 1;
    arInvoiceState4.CuryEnabled = num4 != 0;
    ARInvoiceState arInvoiceState5 = documentState;
    nullable = doc.Released;
    int num5;
    if (!nullable.GetValueOrDefault())
    {
      nullable = doc.Voided;
      if (!nullable.GetValueOrDefault() && !(doc.DocType == "SMC"))
      {
        nullable = doc.PendingPPD;
        if (!nullable.GetValueOrDefault() && !documentState.IsPrepaymentInvoiceReversing)
        {
          num5 = !(doc.DocType == "FCH") || this.IsProcessingMode ? 0 : (cache.GetStatus((object) doc) == 2 ? 1 : 0);
          goto label_27;
        }
      }
    }
    num5 = 1;
label_27:
    arInvoiceState5.ShouldDisableHeader = num5 != 0;
    documentState.IsRegularBalancedDocument = !documentState.ShouldDisableHeader && !documentState.IsDocumentRejectedOrPendingApproval && !documentState.IsDocumentApprovedBalanced && !documentState.IsRetainageReversing;
    ARInvoiceState arInvoiceState6 = documentState;
    nullable = doc.Released;
    int num6 = nullable.GetValueOrDefault() || !(doc.DocType == "SMC") ? 0 : (!AutoNumberAttribute.IsViewOnlyRecord<ARInvoice.refNbr>(cache, (object) doc) ? 1 : 0);
    arInvoiceState6.IsUnreleasedWO = num6 != 0;
    ARInvoiceState arInvoiceState7 = documentState;
    nullable = doc.Released;
    int num7;
    if (!nullable.GetValueOrDefault())
    {
      nullable = doc.PendingPPD;
      num7 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num7 = 0;
    arInvoiceState7.IsUnreleasedPPD = num7 != 0;
    documentState.AllowDeleteDocument = !documentState.ShouldDisableHeader || documentState.IsUnreleasedWO || documentState.IsUnreleasedPPD || documentState.IsPrepaymentInvoiceReversing;
    documentState.DocumentHoldEnabled = !documentState.ShouldDisableHeader;
    documentState.DocumentDateEnabled = !documentState.ShouldDisableHeader || documentState.IsPrepaymentInvoiceReversing && !documentState.IsDocumentReleased;
    documentState.DocumentDescrEnabled = !documentState.ShouldDisableHeader;
    documentState.EditCustomerEnabled = doc != null && ((PXSelectBase<Customer>) this.customer).Current != null && !documentState.IsRetainageReversing;
    ARInvoiceState arInvoiceState8 = documentState;
    nullable = doc.Released;
    bool flag1 = false;
    int num8 = !(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) || ((PXSelectBase<Customer>) this.customer).Current == null ? 0 : (((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation() ? 1 : 0);
    arInvoiceState8.AddressValidationEnabled = num8 != 0;
    ARInvoiceState arInvoiceState9 = documentState;
    nullable = doc.ProformaExists;
    int num9;
    if (!nullable.GetValueOrDefault() && !documentState.IsRetainageDocument && !documentState.IsRetainageReversing)
    {
      nullable = doc.Released;
      if (!nullable.GetValueOrDefault() && !documentState.IsDocumentRejectedOrPendingApproval)
      {
        num9 = !documentState.IsDocumentApprovedBalanced ? 1 : 0;
        goto label_34;
      }
    }
    num9 = 0;
label_34:
    arInvoiceState9.IsTaxZoneIDEnabled = num9 != 0;
    ARInvoiceState arInvoiceState10 = documentState;
    nullable = doc.ProformaExists;
    int num10;
    if (!nullable.GetValueOrDefault())
    {
      nullable = doc.Released;
      if (!nullable.GetValueOrDefault() && !documentState.IsDocumentRejectedOrPendingApproval && !documentState.IsDocumentApprovedBalanced)
      {
        num10 = !documentState.IsRetainageDocument ? 1 : 0;
        goto label_38;
      }
    }
    num10 = 0;
label_38:
    arInvoiceState10.IsAvalaraCustomerUsageTypeEnabled = num10 != 0;
    ARInvoiceState arInvoiceState11 = documentState;
    int num11;
    if (((PXSelectBase<Customer>) this.customer).Current != null)
    {
      nullable = ((PXSelectBase<Customer>) this.customer).Current.FinChargeApply;
      if (nullable.GetValueOrDefault())
      {
        if (!(doc.DocType == "INV") && !(doc.DocType == "DRM"))
        {
          if (doc.DocType == "FCH")
          {
            PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
            if (current == null)
            {
              num11 = 0;
              goto label_48;
            }
            nullable = current.FinChargeOnCharge;
            num11 = nullable.GetValueOrDefault() ? 1 : 0;
            goto label_48;
          }
          num11 = 0;
          goto label_48;
        }
        num11 = 1;
        goto label_48;
      }
    }
    num11 = 0;
label_48:
    arInvoiceState11.ApplyFinChargeVisible = num11 != 0;
    ARInvoiceState arInvoiceState12 = documentState;
    int num12;
    if (!(doc.Status != "C") && doc.LastFinChargeDate.HasValue && doc.LastPaymentDate.HasValue)
    {
      DateTime? lastFinChargeDate = doc.LastFinChargeDate;
      DateTime? lastPaymentDate = doc.LastPaymentDate;
      num12 = lastFinChargeDate.HasValue & lastPaymentDate.HasValue ? (lastFinChargeDate.GetValueOrDefault() <= lastPaymentDate.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num12 = 1;
    arInvoiceState12.ApplyFinChargeEnable = num12 != 0;
    documentState.ShowCashDiscountInfo = false;
    if (PXAccess.FeatureInstalled<FeaturesSet.vATReporting>())
    {
      Decimal? curyOrigDiscAmt = doc.CuryOrigDiscAmt;
      Decimal num13 = 0M;
      if (curyOrigDiscAmt.GetValueOrDefault() > num13 & curyOrigDiscAmt.HasValue && (doc.DocType != "CRM" || doc.DocType == "CRM" && doc.TermsID != null) && doc.DocType != "SMC")
      {
        ((PXSelectBase<ARTaxTran>) this.Taxes).Select(Array.Empty<object>());
        ARInvoiceState arInvoiceState13 = documentState;
        nullable = doc.HasPPDTaxes;
        int num14 = nullable.GetValueOrDefault() ? 1 : 0;
        arInvoiceState13.ShowCashDiscountInfo = num14 != 0;
      }
    }
    documentState.ShowCommissionsInfo = doc.DocType != "FCH" && doc.DocType != "SMC" && doc.DocType != "PPI";
    documentState.IsAssignmentEnabled = !documentState.IsDocumentReleased && !documentState.IsDocumentVoided && !documentState.IsDocumentRejectedOrPendingApproval && !documentState.IsDocumentApprovedBalanced;
    ARInvoiceState arInvoiceState14 = documentState;
    nullable = doc.IsMigratedRecord;
    int num15 = nullable.GetValueOrDefault() ? 1 : 0;
    arInvoiceState14.IsMigratedDocument = num15 != 0;
    ARInvoiceState arInvoiceState15 = documentState;
    int num16;
    if (documentState.IsMigratedDocument)
    {
      nullable = doc.Released;
      num16 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num16 = 0;
    arInvoiceState15.IsUnreleasedMigratedDocument = num16 != 0;
    ARInvoiceState arInvoiceState16 = documentState;
    int num17;
    if (documentState.IsMigratedDocument)
    {
      nullable = doc.Released;
      num17 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num17 = 0;
    arInvoiceState16.IsReleasedMigratedDocument = num17 != 0;
    ARInvoiceState arInvoiceState17 = documentState;
    PX.Objects.AR.ARSetup current1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    int num18;
    if (current1 == null)
    {
      num18 = 0;
    }
    else
    {
      nullable = current1.MigrationMode;
      num18 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    arInvoiceState17.IsMigrationMode = num18 != 0;
    documentState.BalanceBaseCalc = documentState.IsRegularBalancedDocument;
    ARInvoiceState arInvoiceState18 = documentState;
    int num19;
    if (documentState.IsRegularBalancedDocument)
    {
      nullable = doc.ProformaExists;
      if ((!nullable.GetValueOrDefault() || cache.GetStatus((object) doc) == 2) && !documentState.IsRetainageDocument)
      {
        num19 = !documentState.IsRetainageReversing ? 1 : 0;
        goto label_67;
      }
    }
    num19 = 0;
label_67:
    arInvoiceState18.AllowDeleteTransactions = num19 != 0;
    documentState.AllowUpdateTransactions = documentState.IsRegularBalancedDocument && !documentState.IsRetainageDocument && !documentState.IsRetainageReversing;
    ARInvoiceState arInvoiceState19 = documentState;
    int num20;
    if (documentState.IsRegularBalancedDocument)
    {
      nullable = doc.ProformaExists;
      if ((!nullable.GetValueOrDefault() || cache.GetStatus((object) doc) == 2) && !documentState.IsRetainageDocument && !documentState.IsRetainageReversing && doc.CustomerID.HasValue && doc.CustomerLocationID.HasValue && doc.DocType != "FCH")
      {
        num20 = doc.ProjectID.HasValue ? 1 : (!ProjectAttribute.IsPMVisible("AR") ? 1 : 0);
        goto label_71;
      }
    }
    num20 = 0;
label_71:
    arInvoiceState19.AllowInsertTransactions = num20 != 0;
    documentState.AllowDeleteTaxes = documentState.IsRegularBalancedDocument;
    documentState.AllowUpdateTaxes = documentState.IsRegularBalancedDocument;
    documentState.AllowInsertTaxes = documentState.IsRegularBalancedDocument;
    documentState.AllowDeleteDiscounts = documentState.AllowDeleteTransactions && !documentState.RetainageApply;
    ARInvoiceState arInvoiceState20 = documentState;
    int num21;
    if (documentState.AllowUpdateTransactions && !documentState.RetainageApply)
    {
      nullable = doc.ProformaExists;
      num21 = !nullable.GetValueOrDefault() ? 1 : (cache.GetStatus((object) doc) == 2 ? 1 : 0);
    }
    else
      num21 = 0;
    arInvoiceState20.AllowUpdateDiscounts = num21 != 0;
    documentState.AllowInsertDiscounts = documentState.AllowInsertTransactions && !documentState.RetainageApply;
    if (documentState.AllowUpdateTransactions)
    {
      nullable = doc.ProformaExists;
      if (nullable.GetValueOrDefault())
      {
        documentState.ExplicitlyEnabledTranFields.Add("Commissionable");
        documentState.ExplicitlyEnabledTranFields.Add("SalesPersonID");
        documentState.ExplicitlyEnabledTranFields.Add("SubID");
      }
    }
    documentState.AllowUpdateAdjustments = documentState.AllowUpdateTransactions && !documentState.IsRetainageReversing || documentState.IsDocumentRejectedOrPendingApproval || documentState.IsDocumentApprovedBalanced;
    documentState.AllowDeleteAdjustments = documentState.InvoiceUnreleased;
    documentState.LoadDocumentsEnabled = documentState.InvoiceUnreleased;
    documentState.AutoApplyEnabled = documentState.InvoiceUnreleased;
    documentState.AllowUpdateCMAdjustments = documentState.AllowUpdateTransactions && !documentState.IsDocumentScheduled && !documentState.PaymentsByLinesAllowed && !documentState.IsDocumentReleased;
    return documentState;
  }

  protected virtual void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ARInvoice doc = (ARInvoice) e.Row;
    if (doc == null)
      return;
    PXUIFieldAttribute.SetEnabled<ARTran.defScheduleID>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARTran.defScheduleID>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<ARTran.deferredCode>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARTran.deferredCode>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<ARTran.dRTermStartDate>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARTran.dRTermStartDate>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<ARTran.dRTermEndDate>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ARTran.dRTermEndDate>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    this.ApplyDocumentState(cache, doc, this.GetDocumentState(cache, doc));
    ((PXSelectBase<DuplicateFilter>) this.duplicatefilter).Current.Label = string.Format(PXMessages.LocalizeNoPrefix("A document of the {0} type with the {1} reference number already exists in the system. To proceed, enter another reference number for the reversing document."), (object) PXMessages.LocalizeNoPrefix(ARDocType.Labels[((IEnumerable<string>) ARDocType.Values).FindIndex<string>((Predicate<string>) (_ => _ == this.GetReversingDocType(doc.DocType)))]), (object) doc.RefNbr);
  }

  protected virtual void ApplyDocumentState(PXCache cache, ARInvoice doc, ARInvoiceState state)
  {
    ((PXAction) this.release).SetEnabled(true);
    ((PXAction) this.createSchedule).SetEnabled(true);
    ((PXAction) this.payInvoice).SetEnabled(true);
    ((PXAction) this.reclassifyBatch).SetEnabled(true);
    string str1;
    switch (doc.DocType)
    {
      case "CRM":
        str1 = "Apply";
        break;
      case "PPI":
        string str2;
        switch (doc.Status)
        {
          case "U":
            str2 = "Apply";
            break;
          case "Z":
            str2 = "Apply";
            break;
          case "C":
            str2 = "Apply";
            break;
          case "V":
            str2 = "Apply";
            break;
          case "J":
            str2 = "Apply";
            break;
          default:
            str2 = "Pay";
            break;
        }
        str1 = str2;
        break;
      default:
        str1 = "Pay";
        break;
    }
    ((PXAction) this.payInvoice).SetCaption(str1);
    if (doc.DocType == "PPI")
      ((PXAction) this.reverseInvoice).SetCaption("Write Off Unpaid Balance");
    ((PXAction) this.createSchedule).SetVisible(doc.Status != "S");
    ((PXAction) this.viewScheduleOfCurrentDocument).SetVisible(doc.Status == "S");
    ((PXSelectBase) this.Shipping_Address).Cache.AllowUpdate = true;
    ((PXSelectBase) this.Shipping_Contact).Cache.AllowUpdate = true;
    bool? nullable;
    if (((PXGraph) this).IsImport)
    {
      PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
      if ((current != null ? (current.MigrationMode.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable = doc.IsMigratedRecord;
        if (nullable.GetValueOrDefault() && this.cachePermission != null)
        {
          ((PXGraph) this).LoadCachesPermissions(this.cachePermission);
          this.cachePermission = (Dictionary<System.Type, CachePermission>) null;
        }
      }
    }
    ((PXSelectBase) this.salesPerTrans).Cache.AllowSelect = state.ShowCommissionsInfo;
    PXUIFieldAttribute.SetVisible<CustSalesPeople.salesPersonID>(cache, (object) doc, state.ShowCommissionsInfo);
    PXUIFieldAttribute.SetVisible<ARInvoice.curyCommnblAmt>(cache, (object) doc, state.ShowCommissionsInfo);
    PXUIFieldAttribute.SetVisible<ARInvoice.curyCommnAmt>(cache, (object) doc, state.ShowCommissionsInfo);
    ((PXSelectBase) this.Adjustments).Cache.AllowSelect = ((PXSelectBase) this.Adjustments_1).Cache.AllowSelect = true;
    PXUIFieldAttribute.SetVisible<ARInvoice.curyID>(cache, (object) doc, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetRequired<ARInvoice.termsID>(cache, !state.IsDocumentCreditMemo);
    PXCache pxCache1 = cache;
    int num1;
    if (state.IsDocumentCreditMemo)
    {
      if (state.IsDocumentCreditMemo)
      {
        nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.TermsInCreditMemos;
        num1 = nullable.GetValueOrDefault() ? 1 : (doc.TermsID != null ? 1 : 0);
      }
      else
        num1 = 0;
    }
    else
      num1 = 1;
    PXUIFieldAttribute.SetRequired<ARInvoice.dueDate>(pxCache1, num1 != 0);
    PXCache pxCache2 = cache;
    int num2;
    if (state.IsDocumentCreditMemo)
    {
      if (state.IsDocumentCreditMemo)
      {
        nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.TermsInCreditMemos;
        num2 = nullable.GetValueOrDefault() ? 1 : (doc.TermsID != null ? 1 : 0);
      }
      else
        num2 = 0;
    }
    else
      num2 = 1;
    PXUIFieldAttribute.SetRequired<ARInvoice.discDate>(pxCache2, num2 != 0);
    PXUIFieldAttribute.SetVisible<ARTran.origInvoiceDate>(((PXSelectBase) this.Transactions).Cache, (object) null, state.IsDocumentCreditMemo);
    PXUIFieldAttribute.SetVisible<ARTran.sOOrderNbr>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<ARTran.sOOrderType>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    ((PXAction) this.autoApply).SetEnabled(state.AutoApplyEnabled);
    ((PXAction) this.loadDocuments).SetEnabled(state.LoadDocumentsEnabled);
    if (state.ShouldDisableHeader)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.hold>(cache, (object) doc, state.DocumentHoldEnabled);
      PXCache pxCache3 = cache;
      ARInvoice arInvoice1 = doc;
      int num3;
      if ((!state.IsDocumentCreditMemo || state.IsDocumentCreditMemo && doc.TermsID != null) && !state.IsDocumentSmallCreditWO && !state.IsDocumentFinCharge)
      {
        nullable = doc.OpenDoc;
        if (nullable.GetValueOrDefault())
        {
          nullable = doc.PendingPPD;
          num3 = !nullable.GetValueOrDefault() ? 1 : 0;
          goto label_32;
        }
      }
      num3 = 0;
label_32:
      PXUIFieldAttribute.SetEnabled<ARInvoice.dueDate>(pxCache3, (object) arInvoice1, num3 != 0);
      PXCache pxCache4 = cache;
      ARInvoice arInvoice2 = doc;
      int num4;
      if ((!state.IsDocumentCreditMemo || state.IsDocumentCreditMemo && doc.TermsID != null) && !state.IsDocumentSmallCreditWO && !state.IsDocumentFinCharge)
      {
        nullable = doc.OpenDoc;
        if (nullable.GetValueOrDefault())
        {
          nullable = doc.PendingPPD;
          num4 = !nullable.GetValueOrDefault() ? 1 : 0;
          goto label_36;
        }
      }
      num4 = 0;
label_36:
      PXUIFieldAttribute.SetEnabled<ARInvoice.discDate>(pxCache4, (object) arInvoice2, num4 != 0);
      PXUIFieldAttribute.SetEnabled<ARInvoice.emailed>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARInvoice.docDate>(cache, (object) doc, state.DocumentDateEnabled);
      PXUIFieldAttribute.SetEnabled<ARInvoice.finPeriodID>(cache, (object) doc, state.DocumentDateEnabled);
      PXUIFieldAttribute.SetEnabled<ARInvoice.docDesc>(cache, (object) doc, state.DocumentDescrEnabled);
      cache.AllowDelete = state.AllowDeleteDocument;
      cache.AllowUpdate = true;
      ((PXAction) this.release).SetEnabled(state.IsUnreleasedWO || state.IsUnreleasedPPD || state.IsCancellationDocument || state.IsPrepaymentInvoiceReversing && !state.IsDocumentReleased);
      this.SetEnabledPaymentMethod(cache, doc);
      ((PXSelectBase) this.Shipping_Address).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Shipping_Contact).Cache.AllowUpdate = false;
      if (state.IsCancellationDocument)
      {
        ((PXSelectBase) this.Billing_Address).Cache.AllowUpdate = false;
        ((PXSelectBase) this.Billing_Contact).Cache.AllowUpdate = false;
      }
    }
    else if (state.IsDocumentRejectedOrPendingApproval || state.IsDocumentApprovedBalanced)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.hold>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARRegister.dontPrint>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARInvoice.dontEmail>(cache, (object) doc, true);
      this.SetEnabledPaymentMethod(cache, doc);
    }
    else if (state.IsRetainageReversing && !state.IsDocumentReleased)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.docDesc>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARInvoice.hold>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARInvoice.docDate>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARInvoice.finPeriodID>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARRegister.dontPrint>(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARInvoice.dontEmail>(cache, (object) doc, true);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) doc, true);
      PXUIFieldAttribute.SetEnabled<ARInvoice.isRetainageDocument>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.status>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyDocBal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyLineTotal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyTaxTotal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.batchNbr>(cache, (object) doc, false);
      PXCache pxCache5 = cache;
      ARInvoice arInvoice3 = doc;
      nullable = doc.Scheduled;
      int num5 = !nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<ARInvoice.hold>(pxCache5, (object) arInvoice3, num5 != 0);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyVatExemptTotal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyVatTaxableTotal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyDetailExtPriceTotal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyGoodsTotal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyMiscTot>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyDiscTot>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyTaxTotal>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyFreightTot>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyFreightAmt>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyPremiumFreightAmt>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.multiShipAddress>(cache, (object) doc, false);
      this.SetEnabledPaymentMethod(cache, doc);
      PXCache pxCache6 = cache;
      ARInvoice arInvoice4 = doc;
      int num6;
      if (state.IsDocumentCreditMemo)
      {
        if (state.IsDocumentCreditMemo)
        {
          nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.TermsInCreditMemos;
          num6 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num6 = 0;
      }
      else
        num6 = 1;
      PXUIFieldAttribute.SetEnabled<ARInvoice.termsID>(pxCache6, (object) arInvoice4, num6 != 0);
      PXUIFieldAttribute.SetEnabled<ARInvoice.dueDate>(cache, (object) doc, !state.IsDocumentCreditMemo || state.IsDocumentCreditMemo && doc.TermsID != null);
      PXUIFieldAttribute.SetEnabled<ARInvoice.discDate>(cache, (object) doc, !state.IsDocumentCreditMemo || state.IsDocumentCreditMemo && doc.TermsID != null);
      bool flag = ((PX.Objects.CS.Terms) PXSelectorAttribute.Select<ARInvoice.termsID>(cache, (object) doc))?.InstallmentType == "M";
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyOrigDiscAmt>(cache, (object) doc, (!state.IsDocumentCreditMemo || state.IsDocumentCreditMemo && doc.TermsID != null) && !state.IsDocumentPrepaymentInvoice && !flag);
      cache.AllowDelete = true;
      cache.AllowUpdate = true;
      PXCache pxCache7 = cache;
      ARInvoice arInvoice5 = doc;
      int num7;
      if (!PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
      {
        nullable = doc.PaymentsByLinesAllowed;
        if (!nullable.GetValueOrDefault())
        {
          nullable = doc.RetainageApply;
          num7 = !nullable.GetValueOrDefault() ? 1 : 0;
          goto label_51;
        }
      }
      num7 = 0;
label_51:
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyDiscTot>(pxCache7, (object) arInvoice5, num7 != 0);
      PXCache pxCache8 = cache;
      ARInvoice arInvoice6 = doc;
      int num8;
      if (!state.IsDocumentDebitMemo)
      {
        nullable = doc.ProformaExists;
        num8 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num8 = 0;
      PXUIFieldAttribute.SetEnabled<ARInvoice.retainageApply>(pxCache8, (object) arInvoice6, num8 != 0);
      PXUIFieldAttribute.SetEnabled<ARInvoice.projectID>(cache, (object) doc, !state.IsRetainageDocument);
      PXUIFieldAttribute.SetEnabled<ARInvoice.taxZoneID>(cache, (object) doc, !state.IsRetainageDocument);
      PXUIFieldAttribute.SetEnabled<ARInvoice.branchID>(cache, (object) doc, !state.IsRetainageDocument);
      PXUIFieldAttribute.SetEnabled<ARInvoice.curyID>(cache, (object) doc, state.CuryEnabled && !state.IsRetainageDocument);
      PXUIFieldAttribute.SetEnabled<ARTran.curyRetainageAmt>(((PXSelectBase) this.Transactions).Cache, (object) null, state.RetainageApply);
      PXUIFieldAttribute.SetEnabled<ARTran.retainagePct>(((PXSelectBase) this.Transactions).Cache, (object) null, state.RetainageApply);
      PXUIFieldAttribute.SetEnabled<ARInvoice.disableAutomaticTaxCalculation>(cache, (object) doc, false);
    }
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = state.AllowDeleteTransactions;
    ((PXSelectBase) this.Transactions).Cache.AllowUpdate = state.AllowUpdateTransactions;
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = state.AllowInsertTransactions;
    ((PXSelectBase) this.Taxes).Cache.AllowDelete = state.AllowDeleteTaxes;
    ((PXSelectBase) this.Taxes).Cache.AllowUpdate = state.AllowUpdateTaxes;
    ((PXSelectBase) this.Taxes).Cache.AllowInsert = state.AllowInsertTaxes;
    ((PXSelectBase) this.ARDiscountDetails).Cache.AllowDelete = state.AllowDeleteDiscounts;
    ((PXSelectBase) this.ARDiscountDetails).Cache.AllowUpdate = state.AllowUpdateDiscounts;
    ((PXSelectBase) this.ARDiscountDetails).Cache.AllowInsert = state.AllowInsertDiscounts;
    PXUIFieldAttribute.SetEnabled<ARInvoice.docType>(cache, (object) doc);
    PXUIFieldAttribute.SetEnabled<ARInvoice.refNbr>(cache, (object) doc);
    PXUIFieldAttribute.SetEnabled<ARInvoice.isHiddenInIntercompanySales>(cache, (object) doc);
    ((PXSelectBase) this.Adjustments).AllowSelect = !state.IsDocumentCreditMemo;
    ((PXSelectBase) this.Adjustments).Cache.AllowInsert = state.AllowUpdateAdjustments && state.LoadDocumentsEnabled;
    ((PXSelectBase) this.Adjustments).Cache.AllowDelete = state.AllowDeleteAdjustments;
    ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = state.AllowUpdateAdjustments;
    ((PXSelectBase) this.Adjustments_1).AllowSelect = state.IsDocumentCreditMemo;
    ((PXSelectBase) this.Adjustments_1).Cache.AllowInsert = state.AllowUpdateCMAdjustments;
    ((PXSelectBase) this.Adjustments_1).Cache.AllowDelete = state.AllowUpdateCMAdjustments;
    ((PXSelectBase) this.Adjustments_1).Cache.AllowUpdate = state.AllowUpdateCMAdjustments;
    PXUIFieldAttribute.SetEnabled<ARAdjust2.adjgBranchID>(((PXSelectBase) this.Adjustments).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARAdjust.adjgBranchID>(((PXSelectBase) this.Adjustments_1).Cache, (object) null, false);
    ((PXAction) this.editCustomer).SetEnabled(state.EditCustomerEnabled);
    PXAction<ARInvoice> customerRefund = this.customerRefund;
    int num9;
    if (!state.IsDocumentCreditMemo || !state.IsDocumentReleased || state.IsRetainageReversing)
    {
      if (state.IsDocumentPrepaymentInvoice)
      {
        nullable = doc.PendingPayment;
        num9 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num9 = 0;
    }
    else
      num9 = 1;
    ((PXAction) customerRefund).SetEnabled(num9 != 0);
    if (doc.CustomerID.HasValue && (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Transactions).Cache.Inserted) || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Transactions).Cache.Updated) || ((PXSelectBase<ARTran>) this.Transactions).Any<ARTran>()))
    {
      PXUIFieldAttribute.SetEnabled<ARInvoice.customerID>(cache, (object) doc, false);
      PXUIFieldAttribute.SetEnabled<ARInvoice.paymentsByLinesAllowed>(cache, (object) doc, false);
    }
    if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current != null)
    {
      PXCache pxCache9 = cache;
      ARInvoice arInvoice = doc;
      PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
      int num10;
      if (current == null)
      {
        num10 = 0;
      }
      else
      {
        nullable = current.RequireControlTotal;
        num10 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      int num11 = num10 != 0 ? 1 : (state.IsDocumentReleased ? 1 : 0);
      PXUIFieldAttribute.SetVisible<ARInvoice.curyOrigDocAmt>(pxCache9, (object) arInvoice, num11 != 0);
    }
    PXUIFieldAttribute.SetEnabled<ARInvoice.curyCommnblAmt>(cache, (object) doc, false);
    PXUIFieldAttribute.SetEnabled<ARInvoice.curyCommnAmt>(cache, (object) doc, false);
    if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current != null)
    {
      PXUIFieldAttribute.SetVisible<ARInvoice.commnPct>(cache, (object) doc, false);
      if (state.IsDocumentReleased || state.IsDocumentVoided || state.IsDocumentRejectedOrPendingApproval || state.IsDocumentApprovedBalanced || state.IsCancellationDocument)
      {
        ((PXSelectBase) this.salesPerTrans).Cache.AllowInsert = false;
        ((PXSelectBase) this.salesPerTrans).Cache.AllowDelete = false;
        bool flag = false;
        if (!state.IsCancellationDocument)
        {
          PXResult<ARSalesPerTran, ARSPCommissionPeriod> pxResult = (PXResult<ARSalesPerTran, ARSPCommissionPeriod>) PXResultset<ARSalesPerTran>.op_Implicit(((PXSelectBase<ARSalesPerTran>) this.salesPerTrans).Select(Array.Empty<object>()));
          if (pxResult != null)
          {
            ARSPCommissionPeriod commissionPeriod = PXResult<ARSalesPerTran, ARSPCommissionPeriod>.op_Implicit(pxResult);
            if (!string.IsNullOrEmpty(commissionPeriod.CommnPeriodID) && commissionPeriod.Status == "C")
              flag = true;
          }
        }
        ((PXSelectBase) this.salesPerTrans).Cache.AllowUpdate = !flag && !state.IsDocumentRejectedOrPendingApproval && !state.IsDocumentApprovedBalanced && !state.IsCancellationDocument;
      }
      PXUIFieldAttribute.SetEnabled<ARInvoice.workgroupID>(cache, (object) doc, state.IsAssignmentEnabled && !state.IsCancellationDocument);
      PXUIFieldAttribute.SetEnabled<ARInvoice.ownerID>(cache, (object) doc, state.IsAssignmentEnabled && !state.IsCancellationDocument);
    }
    PXUIFieldAttribute.SetVisible<ARTran.taskID>(((PXSelectBase) this.Transactions).Cache, (object) null, ProjectAttribute.IsPMVisible("AR"));
    ((PXAction) this.validateAddresses).SetEnabled(state.AddressValidationEnabled);
    if (PXResultset<ContractBillingTrace>.op_Implicit(PXSelectBase<ContractBillingTrace, PXSelect<ContractBillingTrace, Where<ContractBillingTrace.contractID, Equal<Required<ContractBillingTrace.contractID>>, And<ContractBillingTrace.docType, Equal<Required<ContractBillingTrace.docType>>, And<ContractBillingTrace.refNbr, Equal<Required<ContractBillingTrace.refNbr>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) doc.ProjectID,
      (object) doc.DocType,
      (object) doc.RefNbr
    })) == null)
    {
      nullable = doc.ProformaExists;
      if (!nullable.GetValueOrDefault())
      {
        if (PXResultset<PMBillingRecord>.op_Implicit(PXSelectBase<PMBillingRecord, PXSelect<PMBillingRecord, Where<PMBillingRecord.aRDocType, Equal<Current<ARInvoice.docType>>, And<PMBillingRecord.aRRefNbr, Equal<Current<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
        {
          PXUIFieldAttribute.SetEnabled<ARInvoice.projectID>(cache, (object) doc, false);
          goto label_80;
        }
        goto label_80;
      }
    }
    PXUIFieldAttribute.SetEnabled<ARInvoice.projectID>(cache, (object) doc, false);
label_80:
    PXUIFieldAttribute.SetEnabled<ARInvoice.taxZoneID>(cache, (object) doc, state.IsTaxZoneIDEnabled && !state.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetEnabled<ARInvoice.externalTaxExemptionNumber>(cache, (object) doc, state.IsAvalaraCustomerUsageTypeEnabled && !state.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetEnabled<ARInvoice.avalaraCustomerUsageType>(cache, (object) doc, state.IsAvalaraCustomerUsageTypeEnabled && !state.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetEnabled<ARInvoice.revoked>(cache, (object) doc, true);
    PXUIFieldAttribute.SetVisible<ARInvoice.applyOverdueCharge>(cache, (object) null, state.ApplyFinChargeVisible);
    PXUIFieldAttribute.SetEnabled<ARInvoice.applyOverdueCharge>(cache, (object) null, state.ApplyFinChargeEnable);
    PXUIFieldAttribute.SetVisible<ARRegister.curyDiscountedDocTotal>(cache, (object) doc, state.ShowCashDiscountInfo);
    PXUIFieldAttribute.SetVisible<ARRegister.curyDiscountedTaxableTotal>(cache, (object) doc, state.ShowCashDiscountInfo);
    PXUIFieldAttribute.SetVisible<ARRegister.curyDiscountedPrice>(cache, (object) doc, state.ShowCashDiscountInfo);
    PXUIVisibility pxuiVisibility = state.ShowCashDiscountInfo ? (PXUIVisibility) 3 : (PXUIVisibility) 1;
    PXUIFieldAttribute.SetVisibility<ARTaxTran.curyDiscountedPrice>(((PXSelectBase) this.Taxes).Cache, (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisibility<ARTaxTran.curyDiscountedTaxableAmt>(((PXSelectBase) this.Taxes).Cache, (object) null, pxuiVisibility);
    PXUIFieldAttribute.SetVisible<ARRegister.retainageAcctID>(cache, (object) doc, state.RetainageApply);
    PXUIFieldAttribute.SetVisible<ARRegister.retainageSubID>(cache, (object) doc, state.RetainageApply);
    PXUIFieldAttribute.SetVisible<ARInvoice.retainageApply>(cache, (object) doc, state.IsRetainageApplyDocument);
    PXUIFieldAttribute.SetVisible<ARInvoice.isRetainageDocument>(cache, (object) doc, state.IsRetainageDocument);
    PXUIFieldAttribute.SetVisible<ARTran.retainagePct>(((PXSelectBase) this.Transactions).Cache, (object) null, state.RetainageApply);
    PXUIFieldAttribute.SetVisible<ARTran.origRefNbr>(((PXSelectBase) this.Transactions).Cache, (object) null, state.IsRetainageDocument);
    PXUIFieldAttribute.SetVisible<ARTran.curyRetainageAmt>(((PXSelectBase) this.Transactions).Cache, (object) null, state.RetainageApply);
    PXUIFieldAttribute.SetVisible<ARTaxTran.curyRetainedTaxableAmt>(((PXSelectBase) this.Taxes).Cache, (object) null, state.RetainageApply && state.RetainTaxes);
    PXUIFieldAttribute.SetVisible<ARTaxTran.curyRetainedTaxAmt>(((PXSelectBase) this.Taxes).Cache, (object) null, state.RetainageApply && state.RetainTaxes);
    PXUIFieldAttribute.SetRequired<ARRegister.retainageAcctID>(cache, state.RetainageApply);
    PXUIFieldAttribute.SetRequired<ARRegister.retainageSubID>(cache, state.RetainageApply);
    PXUIFieldAttribute.SetEnabled<ARInvoice.taxCalcMode>(cache, (object) doc, !state.IsRetainageDocument && (!state.IsRetainageApplyDocument || !state.IsDocumentReleased) && !state.IsDocumentReleased);
    int num12;
    if (PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>())
    {
      nullable = doc.PaymentsByLinesAllowed;
      num12 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num12 = 0;
    bool flag1 = num12 != 0;
    PXUIFieldAttribute.SetVisible<ARTran.sortOrder>(((PXSelectBase) this.Transactions).Cache, (object) null, flag1);
    if (flag1)
    {
      ((PXAction) this.autoApply).SetVisible(false);
      ((PXAction) this.loadDocuments).SetEnabled(false);
      ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
      ((PXSelectBase) this.ARDiscountDetails).Cache.SetAllEditPermissions(false);
      if (!state.IsDocumentReleased && !((PXGraph) this).UnattendedMode)
      {
        string str3 = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() ? "Group discounts and document discounts are not supported if the Pay by Line check box is selected." : "Document discounts are not supported if the Pay by Line check box is selected.";
        cache.RaiseExceptionHandling<ARInvoice.curyDiscTot>((object) doc, (object) doc.CuryDiscTot, (Exception) new PXSetPropertyException(str3, (PXErrorLevel) 2));
      }
      if (state.IsDocumentReleased)
      {
        foreach (ARAdjust2 arAdjust2 in GraphHelper.RowCast<ARAdjust2>((IEnumerable) ((PXSelectBase<ARAdjust2>) this.Adjustments).Select(Array.Empty<object>())).Where<ARAdjust2>((Func<ARAdjust2, bool>) (a =>
        {
          int? adjdLineNbr = a.AdjdLineNbr;
          int num13 = 0;
          return adjdLineNbr.GetValueOrDefault() == num13 & adjdLineNbr.HasValue && !a.Released.GetValueOrDefault();
        })))
          ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust2.adjdRefNbr>((object) arAdjust2, (object) arAdjust2.AdjgRefNbr, (Exception) new PXSetPropertyException("The application cannot be released because it is not distributed between document lines. On the Payments and Applications (AR302000) form, delete the application, and apply the payment or credit memo to the document lines.", (PXErrorLevel) 3));
      }
    }
    nullable = doc.RetainageApply;
    if (nullable.GetValueOrDefault())
    {
      string str4 = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() ? "Group discounts and document discounts are not supported if the Apply Retainage check box is selected." : "Document discounts are not supported if the Apply Retainage check box is selected.";
      cache.RaiseExceptionHandling<ARInvoice.curyDiscTot>((object) doc, (object) doc.CuryDiscTot, (Exception) new PXSetPropertyException(str4, (PXErrorLevel) 2));
    }
    PXUIFieldAttribute.SetEnabled<ARTran.curyCashDiscBal>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.curyRetainageBal>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.curyTranBal>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.curyOrigTaxAmt>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXCache cache1 = ((PXSelectBase) this.Transactions).Cache;
    int num14;
    if (flag1 && state.IsDocumentReleased)
    {
      Decimal? curyOrigDiscAmt = doc.CuryOrigDiscAmt;
      Decimal num15 = 0M;
      num14 = !(curyOrigDiscAmt.GetValueOrDefault() == num15 & curyOrigDiscAmt.HasValue) ? 1 : 0;
    }
    else
      num14 = 0;
    PXUIFieldAttribute.SetVisible<ARTran.curyCashDiscBal>(cache1, (object) null, num14 != 0);
    bool flag2 = flag1 && state.IsDocumentReleased && state.RetainageApply;
    PXUIFieldAttribute.SetVisible<ARTran.curyRetainageBal>(((PXSelectBase) this.Transactions).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<ARTran.curyRetainedTaxAmt>(((PXSelectBase) this.Transactions).Cache, (object) null, flag2);
    bool flag3 = flag1 && state.IsDocumentReleased;
    PXUIFieldAttribute.SetVisible<ARTran.curyTranBal>(((PXSelectBase) this.Transactions).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<ARTran.curyOrigTaxAmt>(((PXSelectBase) this.Transactions).Cache, (object) null, flag3);
    if (state.IsDocumentPrepaymentInvoice || state.IsPrepaymentInvoiceReversing)
      PXUIFieldAttribute.SetVisible<ARInvoice.paymentsByLinesAllowed>(cache, (object) doc, false);
    cache.RaiseExceptionHandling<ARRegister.curyRoundDiff>((object) doc, (object) null, (Exception) null);
    if (!state.IsDocumentReleased && !((PXGraph) this).UnattendedMode)
    {
      Decimal? roundDiff = doc.RoundDiff;
      Decimal num16 = 0M;
      if (roundDiff.GetValueOrDefault() == num16 & roundDiff.HasValue && PXAccess.FeatureInstalled<FeaturesSet.invoiceRounding>())
      {
        if (state.RetainageApply)
          cache.RaiseExceptionHandling<ARRegister.curyRoundDiff>((object) doc, (object) doc.CuryRoundDiff, (Exception) new PXSetPropertyException("Documents with retainage do not support invoice rounding.", (PXErrorLevel) 2));
        else if (flag1)
          cache.RaiseExceptionHandling<ARRegister.curyRoundDiff>((object) doc, (object) doc.CuryRoundDiff, (Exception) new PXSetPropertyException("Documents paid by line do not support invoice rounding.", (PXErrorLevel) 2));
      }
    }
    if (doc.DocType != "CRM")
    {
      nullable = doc.Released;
      if (nullable.GetValueOrDefault() && state.IsMigratedDocument)
      {
        Decimal? curyInitDocBal = doc.CuryInitDocBal;
        Decimal? curyOrigDocAmt = doc.CuryOrigDocAmt;
        if (!(curyInitDocBal.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & curyInitDocBal.HasValue == curyOrigDocAmt.HasValue))
        {
          ((PXSelectBase) this.Adjustments).AllowSelect = false;
          ((PXSelectBase) this.Adjustments_1).AllowSelect = true;
        }
      }
    }
    PXUIFieldAttribute.SetVisible<ARInvoice.curyDocBal>(cache, (object) doc, !state.IsUnreleasedMigratedDocument);
    PXUIFieldAttribute.SetVisible<ARInvoice.curyInitDocBal>(cache, (object) doc, state.IsUnreleasedMigratedDocument);
    PXUIFieldAttribute.SetEnabled<ARInvoice.curyInitDocBal>(cache, (object) doc, state.IsUnreleasedMigratedDocument);
    PXUIFieldAttribute.SetVisible<ARRegister.displayCuryInitDocBal>(cache, (object) doc, state.IsReleasedMigratedDocument);
    bool flag4 = this.IsWarehouseVisible(doc);
    PXUIFieldAttribute.SetEnabled<ARTran.siteID>(((PXSelectBase) this.Transactions).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<ARTran.siteID>(((PXSelectBase) this.Transactions).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisibility<ARTran.siteID>(((PXSelectBase) this.Transactions).Cache, (object) null, flag4 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetEnabled<ARTran.tranCost>(((PXSelectBase) this.Transactions).Cache, (object) null, state.IsMigrationMode);
    PXUIFieldAttribute.SetVisible<ARTran.tranCost>(((PXSelectBase) this.Transactions).Cache, (object) null, state.IsMigrationMode);
    PXUIFieldAttribute.SetVisibility<ARTran.tranCost>(((PXSelectBase) this.Transactions).Cache, (object) null, state.IsMigrationMode ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    if (state.IsMigrationMode)
      PXUIFieldAttribute.SetEnabled<ARInvoice.paymentsByLinesAllowed>(cache, (object) doc, false);
    if (state.IsUnreleasedMigratedDocument)
      ((PXSelectBase) this.Adjustments).Cache.AllowSelect = ((PXSelectBase) this.Adjustments_1).Cache.AllowSelect = false;
    if ((state.IsMigrationMode ? (!state.IsMigratedDocument ? 1 : 0) : (state.IsUnreleasedMigratedDocument ? 1 : 0)) != 0)
    {
      bool allowInsert = ((PXSelectBase) this.Document).Cache.AllowInsert;
      bool allowDelete = ((PXSelectBase) this.Document).Cache.AllowDelete;
      if (((PXGraph) this).IsImport && this.cachePermission == null)
        this.cachePermission = ((PXGraph) this).SaveCachesPermissions(true);
      ((PXGraph) this).DisableCaches();
      ((PXSelectBase) this.Document).Cache.AllowInsert = allowInsert;
      ((PXSelectBase) this.Document).Cache.AllowDelete = allowDelete;
    }
    if (state.IsUnreleasedMigratedDocument && string.IsNullOrEmpty(PXUIFieldAttribute.GetError<ARInvoice.curyInitDocBal>(cache, (object) doc)))
      cache.RaiseExceptionHandling<ARInvoice.curyInitDocBal>((object) doc, (object) doc.CuryInitDocBal, (Exception) new PXSetPropertyException("Enter the document open balance to this box.", (PXErrorLevel) 2));
    if (state.ExplicitlyEnabledTranFields.Count <= 0)
      return;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Transactions).Cache, (string) null, false);
    foreach (string enabledTranField in (IEnumerable<string>) state.ExplicitlyEnabledTranFields)
      PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Transactions).Cache, (object) null, enabledTranField, true);
  }

  protected virtual void DisableCreditHoldActions(PXCache cache, ARInvoice doc)
  {
    ((PXAction) this.putOnCreditHold).SetEnabled(doc.DocType != "CRM" && !this.Approval.GetAssignedMaps(doc, cache).Any<PX.Objects.EP.ApprovalMap>());
  }

  protected virtual bool IsWarehouseVisible(ARInvoice doc)
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    return current != null && current.MigrationMode.GetValueOrDefault();
  }

  protected virtual void SetEnabledPaymentMethod(PXCache cache, ARInvoice doc)
  {
    bool flag1 = doc.DocType != "SMC" && doc.OpenDoc.GetValueOrDefault();
    bool flag2 = !string.IsNullOrEmpty(doc.PaymentMethodID);
    bool flag3 = false;
    bool flag4 = EnumerableExtensions.IsIn<string>(doc.DocType, "INV", "DRM", "CRM", "FCH");
    if (flag1 & flag4 & flag2)
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXViewOf<PX.Objects.CA.PaymentMethod>.BasedOn<SelectFromBase<PX.Objects.CA.PaymentMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CA.PaymentMethod.paymentMethodID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) doc.PaymentMethodID
      }));
      flag3 = paymentMethod != null && paymentMethod.IsAccountNumberRequired.GetValueOrDefault();
    }
    PXUIFieldAttribute.SetEnabled<ARInvoice.paymentMethodID>(cache, (object) doc, flag1);
    PXUIFieldAttribute.SetEnabled<ARInvoice.pMInstanceID>(cache, (object) doc, flag3);
    PXUIFieldAttribute.SetEnabled<ARInvoice.cashAccountID>(cache, (object) doc, flag1 & flag2);
  }

  protected virtual void ARInvoice_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    PMProject project;
    bool flag1 = ProjectDefaultAttribute.IsProject((PXGraph) this, row.ProjectID, out project);
    foreach (PXResult<ARTran> pxResult in ((PXSelectBase<ARTran>) new PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      ARTran tran = PXResult<ARTran>.op_Implicit(pxResult);
      int? projectId = tran.ProjectID;
      int? nullable1 = row.ProjectID;
      bool flag2 = !(projectId.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId.HasValue == nullable1.HasValue);
      tran.ProjectID = row.ProjectID;
      if (PXAccess.FeatureInstalled<FeaturesSet.costCodes>() & flag1 && project.BudgetLevel == "T")
      {
        int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
        int num1;
        if (!flag2)
        {
          nullable1 = tran.CostCodeID;
          int num2 = defaultCostCode;
          num1 = !(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue) ? 1 : 0;
        }
        else
          num1 = 1;
        flag2 = num1 != 0;
        tran.CostCodeID = new int?(defaultCostCode);
      }
      if (tran.LineType == "DS" || tran.LineType == "FR")
      {
        try
        {
          ARTran arTran = tran;
          int? nullable2;
          if (!flag1)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = this.GetTaskByAccount(tran, project);
          arTran.TaskID = nullable2;
        }
        catch (PXException ex)
        {
          PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
          {
            e.OldValue
          }));
          sender.RaiseExceptionHandling<ARInvoice.projectID>(e.Row, (object) pmProject.ContractCD ?? e.OldValue, (Exception) new PXSetPropertyException(ex.MessageNoNumber));
        }
      }
      if (flag2)
        ((PXSelectBase<ARTran>) this.Transactions).Update(tran);
      sender.SetDefaultExt<ARRegister.defRetainagePct>(e.Row);
    }
  }

  public virtual bool IsReverse => this.isReverse;

  protected virtual void ARInvoice_ProjectID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this.isReverse)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARInvoice_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this.isReverse)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARInvoice_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARInvoice row = e.Row as ARInvoice;
    if (!this.IsApprovalRequired(row))
      return;
    sender.SetValue<PX.Objects.AP.APInvoice.hold>((object) row, (object) true);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<ARInvoice.pendingPayment> e)
  {
    if (!((e.Row is ARInvoice row ? row.DocType : (string) null) == "PPI"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARInvoice.pendingPayment>, object, object>) e).NewValue = (object) true;
  }

  protected virtual void ARInvoice_Hold_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    this.setDontApproveValue(row, sender);
  }

  protected virtual void ARInvoice_DocType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    this.setDontApproveValue(row, sender);
  }

  protected virtual void ARInvoice_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    int? projectId = row.ProjectID;
    if (!projectId.HasValue)
      return;
    projectId = row.ProjectID;
    int? nullable = ProjectDefaultAttribute.NonProject();
    if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue || ((PXGraph) this).UnattendedMode || !row.CreatedByScreenID.StartsWith("CT") || !((PXSelectorAttribute.Select<ARInvoice.projectID>(sender, (object) row) is PX.Objects.CT.Contract contract ? contract.BaseType : (string) null) == "C"))
      return;
    if (((PXSelectBase) this.Document).View.Ask(PXMessages.LocalizeFormatNoPrefix("Deletion of the document will not affect the data in the related {0} contract, such as the billed usage or the next billing date. To cancel the last action performed on this contract, on the Customer Contracts (CT301000) form, click Actions > Undo Last Action. To proceed with deletion, click OK.", new object[1]
    {
      (object) contract.ContractCD
    }), (MessageButtons) 1) == 1)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARInvoice_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is ARInvoice row))
      return;
    ((SelectedEntityEvent<ARInvoice>) PXEntityEventBase<ARInvoice>.Container<ARInvoice.Events>.Select((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice.Events>>>) (ev => ev.ARInvoiceDeleted))).FireOn((PXGraph) this, row);
  }

  protected virtual void ARInvoice_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    ARInvoice oldRow = (ARInvoice) e.OldRow;
    if (row.Released.GetValueOrDefault())
      return;
    if (e.ExternalCall && !sender.ObjectsEqual<ARInvoice.docDate, ARInvoice.retainageApply>((object) oldRow, (object) row) && row.OrigDocType == null)
    {
      if (row.OrigRefNbr == null)
      {
        try
        {
          if (!row.DisableAutomaticDiscountCalculation.GetValueOrDefault())
            this.ARDiscountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (ARTran) null, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, row.CustomerLocationID, row.DocDate, this.GetDefaultARDiscountCalculationOptions(row));
        }
        finally
        {
          row.DisableAutomaticDiscountCalculation = new bool?(false);
        }
      }
    }
    if (sender.GetStatus((object) row) != 3 && sender.GetStatus((object) row) != 4 && !sender.ObjectsEqual<ARInvoice.curyDiscTot>((object) oldRow, (object) row))
    {
      this.AddDiscount(sender, row);
      if (!this.ARDiscountEngine.IsInternalDiscountEngineCall && e.ExternalCall)
      {
        this.ARDiscountEngine.SetTotalDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.CuryDiscTot, this.GetDefaultARDiscountCalculationOptions(row));
        this.RecalculateTotalDiscount();
      }
    }
    Decimal? nullable1;
    if (!((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal.GetValueOrDefault() && !sender.Graph.IsCopyPasteContext)
    {
      Decimal? curyDocBal = row.CuryDocBal;
      nullable1 = row.CuryOrigDocAmt;
      if (!(curyDocBal.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyDocBal.HasValue == nullable1.HasValue))
      {
        PXCache pxCache = sender;
        ARInvoice arInvoice = row;
        nullable1 = row.CuryDocBal;
        Decimal? nullable2;
        if (nullable1.HasValue)
        {
          nullable1 = row.CuryDocBal;
          Decimal num = 0M;
          if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
          {
            nullable2 = row.CuryDocBal;
            goto label_16;
          }
        }
        nullable2 = new Decimal?(0M);
label_16:
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> local = (ValueType) nullable2;
        pxCache.SetValueExt<ARInvoice.curyOrigDocAmt>((object) arInvoice, (object) local);
      }
    }
    bool? nullable3 = row.Hold;
    Decimal? nullable4;
    if (!nullable3.GetValueOrDefault())
    {
      nullable1 = row.CuryDocBal;
      nullable4 = row.CuryOrigDocAmt;
      if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
      {
        sender.RaiseExceptionHandling<ARInvoice.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
      }
      else
      {
        nullable4 = row.CuryOrigDocAmt;
        Decimal num = 0M;
        if (nullable4.GetValueOrDefault() < num & nullable4.HasValue)
        {
          nullable3 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal;
          if (nullable3.GetValueOrDefault())
            sender.RaiseExceptionHandling<ARInvoice.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
          else
            sender.RaiseExceptionHandling<ARInvoice.curyDocBal>((object) row, (object) row.CuryDocBal, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
        }
        else
        {
          sender.RaiseExceptionHandling<ARInvoice.curyOrigDocAmt>((object) row, (object) null, (Exception) null);
          sender.RaiseExceptionHandling<ARInvoice.curyDocBal>((object) row, (object) null, (Exception) null);
        }
      }
    }
    int? nullable5 = row.CustomerID;
    if (!nullable5.HasValue)
      return;
    nullable4 = row.CuryDiscTot;
    if (!nullable4.HasValue)
      return;
    nullable4 = row.CuryDiscTot;
    Decimal num1 = 0M;
    if (!(nullable4.GetValueOrDefault() > num1 & nullable4.HasValue))
      return;
    nullable4 = row.CuryLineTotal;
    if (!nullable4.HasValue)
      return;
    nullable4 = row.CuryLineTotal;
    Decimal num2 = 0M;
    if (!(nullable4.GetValueOrDefault() > num2 & nullable4.HasValue))
      return;
    DiscountEngine<ARTran, ARInvoiceDiscountDetail> arDiscountEngine = this.ARDiscountEngine;
    PXCache cache = sender;
    int? customerId = row.CustomerID;
    nullable5 = new int?();
    int? vendorID = nullable5;
    Decimal discountLimit = arDiscountEngine.GetDiscountLimit(cache, customerId, vendorID);
    Decimal? nullable6 = row.CuryLineTotal;
    Decimal num3 = (Decimal) 100;
    Decimal? nullable7 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() / num3) : new Decimal?();
    Decimal num4 = discountLimit;
    Decimal? nullable8;
    if (!nullable7.HasValue)
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = new Decimal?(nullable7.GetValueOrDefault() * num4);
    nullable4 = nullable8;
    nullable1 = row.CuryDiscTot;
    if (!(nullable4.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable4.HasValue & nullable1.HasValue))
      return;
    PXUIFieldAttribute.SetWarning<ARInvoice.curyDiscTot>(sender, (object) row, PXMessages.LocalizeFormatNoPrefix("The total amount of group and document discounts cannot exceed the limit specified for the customer class ({0:F2}%) on the Customer Classes (AR201000) form.", new object[1]
    {
      (object) discountLimit
    }));
  }

  protected virtual void ARInvoice_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.setDontApproveValue((ARInvoice) e.Row, sender);
  }

  protected virtual void ARTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null || ((PXSelectBase<ARInvoice>) this.Document).Current == null)
      return;
    Customer customer1 = ((PXSelectBase<Customer>) this.customer).Current;
    if (customer1 == null)
      customer1 = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID
      }));
    Customer customer2 = customer1;
    if (row.InventoryID.HasValue && (!customer2.IsBranch.GetValueOrDefault() || !(((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.IntercompanySalesAccountDefault == "L")) || ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null)
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.CSalesAcctID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTran_AccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARTran row = e.Row as ARTran;
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if ((current != null ? (!current.IsRetainageDocument.GetValueOrDefault() ? 1 : 0) : 1) == 0 || row == null)
      return;
    int? nullable = row.ProjectID;
    if (!nullable.HasValue || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    nullable = row.TaskID;
    if (!nullable.HasValue)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (account == null)
      return;
    nullable = account.AccountGroupID;
    if (nullable.HasValue)
      return;
    sender.RaiseExceptionHandling<ARTran.accountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 2, new object[1]
    {
      (object) account.AccountCD
    }));
  }

  protected virtual void ARTran_AccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran row) || row.TaskID.HasValue)
      return;
    sender.SetDefaultExt<ARTran.taskID>(e.Row);
  }

  protected virtual void ARTran_CuryExtPrice_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<ARTran.curyRetainageAmt> e)
  {
    if (!(e.Row is ARTran row))
      return;
    Decimal? nullable = row.CuryExtPrice;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.CuryDiscAmt;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal availableRetainageAmount = valueOrDefault1 - valueOrDefault2;
    nullable = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARTran.curyRetainageAmt>, object, object>) e).NewValue;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    RetainageAmountAttribute.AssertRetainageAmount(availableRetainageAmount, valueOrDefault3);
  }

  protected virtual void ARTran_ExpenseAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARTran row = e.Row as ARTran;
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if ((current != null ? (!current.IsRetainageDocument.GetValueOrDefault() ? 1 : 0) : 1) == 0 || row == null)
      return;
    int? nullable = row.ProjectID;
    if (!nullable.HasValue || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    nullable = row.TaskID;
    if (!nullable.HasValue)
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, e.NewValue as int?);
    if (account == null)
      return;
    nullable = account.AccountGroupID;
    if (nullable.HasValue)
      return;
    sender.RaiseExceptionHandling<ARTran.expenseAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 4, new object[1]
    {
      (object) account.AccountCD
    }));
  }

  protected virtual void ARTran_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null || !row.AccountID.HasValue || ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null)
      return;
    PX.Objects.IN.InventoryItem byId = this.InventoryItemGetByID(row.InventoryID);
    PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<ARTran.employeeID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<ARTran.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BranchID
    }));
    SalesPerson salesPerson = PXResultset<SalesPerson>.op_Implicit(PXSelectBase<SalesPerson, PXSelect<SalesPerson, Where<SalesPerson.salesPersonID, Equal<Current<ARTran.salesPersonID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    int? nullable1 = (int?) ((PXGraph) this).Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cSalesSubID>((object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current);
    int? nullable2 = (int?) ((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.salesSubID>((object) byId);
    int? nullable3 = (int?) ((PXGraph) this).Caches[typeof (PX.Objects.EP.EPEmployee)].GetValue<PX.Objects.EP.EPEmployee.salesSubID>((object) epEmployee);
    int? nullable4 = (int?) ((PXGraph) this).Caches[typeof (PX.Objects.CR.Standalone.Location)].GetValue<PX.Objects.CR.Standalone.Location.cMPSalesSubID>((object) location);
    int? nullable5 = (int?) ((PXGraph) this).Caches[typeof (SalesPerson)].GetValue<SalesPerson.salesSubID>((object) salesPerson);
    object obj;
    try
    {
      obj = (object) SubAccountMaskAttribute.MakeSub<PX.Objects.AR.ARSetup.salesSubMask>((PXGraph) this, this.SalesSubMask, new object[5]
      {
        (object) nullable1,
        (object) nullable2,
        (object) nullable3,
        (object) nullable4,
        (object) nullable5
      }, new System.Type[5]
      {
        typeof (PX.Objects.CR.Location.cSalesSubID),
        typeof (PX.Objects.IN.InventoryItem.salesSubID),
        typeof (PX.Objects.EP.EPEmployee.salesSubID),
        typeof (PX.Objects.CR.Location.cMPSalesSubID),
        typeof (SalesPerson.salesSubID)
      });
      sender.RaiseFieldUpdating<ARTran.subID>(e.Row, ref obj);
      sender.RaiseFieldVerifying<ARTran.subID>(e.Row, ref obj);
    }
    catch (PXException ex)
    {
      if (FieldErrorScope.NeedsSet(typeof (ARTran.subID)))
        throw;
      obj = (object) null;
    }
    e.NewValue = (object) (int?) obj;
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [ARTax(typeof (ARInvoice), typeof (ARTax), typeof (ARTaxTran), typeof (ARInvoice.taxCalcMode), typeof (ARInvoice.branchID), Inventory = typeof (ARTran.inventoryID), UOM = typeof (ARTran.uOM), LineQty = typeof (ARTran.qty))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.taxCategoryID>))]
  protected virtual void ARTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [DRTerms.Dates(typeof (ARTran.dRTermStartDate), typeof (ARTran.dRTermEndDate), typeof (ARTran.inventoryID), typeof (ARTran.deferredCode), typeof (ARInvoice.hold))]
  protected virtual void ARTran_RequiresTerms_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null || row.InventoryID.HasValue || this.isReverse || TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(sender, e.Row) != TaxCalc.Calc || ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.DfltTaxCategoryID))
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
    ARTran row = e.Row as ARTran;
    bool flag = false;
    bool? nullable1;
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      nullable1 = ((PXSelectBase<ARInvoice>) this.Document).Current.ProformaExists;
      if (nullable1.GetValueOrDefault())
        flag = true;
    }
    if (row != null && row.InventoryID.HasValue && row.UOM != null)
    {
      nullable1 = row.ManualPrice;
      if (!nullable1.GetValueOrDefault() && !flag)
      {
        string custPriceClass = "BASE";
        PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
        if (location != null && !string.IsNullOrEmpty(location.CPriceClassID))
          custPriceClass = location.CPriceClassID;
        DateTime? nullable2 = ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate;
        DateTime date = nullable2.Value;
        string taxCalcMode = ((PXSelectBase<ARInvoice>) this.Document).Current.TaxCalcMode;
        if (row.TranType == "CRM")
        {
          nullable2 = row.OrigInvoiceDate;
          if (nullable2.HasValue)
          {
            nullable2 = row.OrigInvoiceDate;
            date = nullable2.Value;
          }
        }
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
        (ARSalesPriceMaint.SalesPriceItem salesPriceItem, Decimal? nullable3) = ARSalesPriceMaint.SingleARSalesPriceMaint.GetSalesPriceItemAndCalculatedPrice(sender, custPriceClass, row.CustomerID, row.InventoryID, row.LotSerialNbr, row.SiteID, currencyInfo.GetCM(), row.UOM, row.Qty, date, row.CuryUnitPrice, taxCalcMode);
        ARTran arTran = row;
        bool? nullable4;
        if (salesPriceItem == null)
        {
          nullable1 = new bool?();
          nullable4 = nullable1;
        }
        else
          nullable4 = new bool?(salesPriceItem.SkipLineDiscounts);
        arTran.SkipLineDiscountsBuffer = nullable4;
        e.NewValue = (object) nullable3.GetValueOrDefault();
        ARSalesPriceMaint.CheckNewUnitPrice<ARTran, ARTran.curyUnitPrice>(sender, row, e.NewValue);
        return;
      }
    }
    e.NewValue = sender.GetValue<ARTran.curyUnitPrice>(e.Row);
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void ARTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
    this.CalculateAccruedCost(sender, (ARTran) e.Row);
  }

  protected virtual void ARTran_OrigInvoiceDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
  }

  protected virtual void ARTran_Qty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    Decimal? qty = row.Qty;
    Decimal num = 0M;
    if (qty.GetValueOrDefault() == num & qty.HasValue)
    {
      sender.SetValueExt<ARTran.curyDiscAmt>((object) row, (object) 0M);
      sender.SetValueExt<ARTran.discPct>((object) row, (object) 0M);
    }
    else
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

  protected virtual void ARTran_EmployeeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ARTran.subID>(e.Row);
  }

  [PopupMessage]
  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  protected virtual void ARTran_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [ARDocType.RetainageInvoiceList]
  protected virtual void _(PX.Data.Events.CacheAttached<ARRetainageInvoice.docType> e)
  {
  }

  protected virtual void ARTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARTran row = e.Row as ARTran;
    int? accountId = row.AccountID;
    using (new FieldErrorScope(typeof (ARTran.subID), FieldErrorScope.Action.Reset))
      sender.SetDefaultExt<ARTran.accountID>(e.Row);
    row.AccountID = row.AccountID ?? accountId;
    try
    {
      using (new FieldErrorScope(typeof (ARTran.subID), FieldErrorScope.Action.Set))
        sender.SetDefaultExt<ARTran.subID>(e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<ARTran.subID>(e.Row, (object) null);
    }
    PX.Objects.IN.InventoryItem byId = this.InventoryItemGetByID(row.InventoryID);
    if (byId != null && !byId.StkItem.GetValueOrDefault())
    {
      sender.SetDefaultExt<ARTran.accrueCost>(e.Row);
      sender.SetDefaultExt<ARTran.costBasis>(e.Row);
      if (byId.PostToExpenseAccount == "S")
      {
        sender.SetDefaultExt<ARTran.expenseAccrualAccountID>(e.Row);
        try
        {
          sender.SetDefaultExt<ARTran.expenseAccrualSubID>(e.Row);
        }
        catch (PXSetPropertyException ex)
        {
          sender.SetValue<ARTran.expenseAccrualSubID>(e.Row, (object) null);
        }
        sender.SetDefaultExt<ARTran.expenseAccountID>(e.Row);
        try
        {
          sender.SetDefaultExt<ARTran.expenseSubID>(e.Row);
        }
        catch (PXSetPropertyException ex)
        {
          sender.SetValue<ARTran.expenseSubID>(e.Row, (object) null);
        }
      }
      else
      {
        row.ExpenseAccrualAccountID = new int?();
        row.ExpenseAccrualSubID = new int?();
        row.ExpenseAccountID = new int?();
        row.ExpenseSubID = new int?();
      }
    }
    else
    {
      row.ExpenseAccrualAccountID = new int?();
      row.ExpenseAccrualSubID = new int?();
      row.ExpenseAccountID = new int?();
      row.ExpenseSubID = new int?();
      row.CostBasis = "U";
    }
    sender.SetDefaultExt<ARTran.taxCategoryID>(e.Row);
    sender.SetDefaultExt<ARTran.deferredCode>(e.Row);
    if (e.ExternalCall && row != null)
      row.CuryUnitPrice = new Decimal?(0M);
    sender.RaiseExceptionHandling<ARTran.uOM>(e.Row, (object) null, (Exception) null);
    sender.SetDefaultExt<ARTran.uOM>(e.Row);
    sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
    if (byId == null || row == null)
      return;
    row.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) byId, "Descr", ((PXSelectBase<Customer>) this.customer).Current?.LocaleName);
  }

  [NullableSite]
  protected virtual void ARTran_SiteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(typeof (ARInvoice.isMigratedRecord))]
  protected virtual void ARTran_IsTranCostFinal_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ARTran_ManualPrice_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    bool? nullable = row.ManualPrice;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.IsFree;
      if (!nullable.GetValueOrDefault() && !sender.Graph.IsCopyPasteContext)
        sender.SetDefaultExt<ARTran.curyUnitPrice>(e.Row);
    }
    nullable = row.ManualPrice;
    if (!nullable.GetValueOrDefault())
      return;
    sender.SetValue<ARTran.skipLineDiscounts>((object) row, (object) false);
  }

  protected virtual void ARTran_DefScheduleID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((ARTran) e.Row).DefScheduleID
    }));
    if (drSchedule == null)
      return;
    ARTran arTran = PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) drSchedule.DocType,
      (object) drSchedule.RefNbr,
      (object) drSchedule.LineNbr
    }));
    if (arTran == null)
      return;
    ((ARTran) e.Row).DeferredCode = arTran.DeferredCode;
  }

  protected virtual void ARTran_DiscountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARTran row = e.Row as ARTran;
    if (!e.ExternalCall)
      return;
    if (row == null)
      return;
    try
    {
      ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      this.ARDiscountEngine.UpdateManualLineDiscount(sender, (PXSelectBase<ARTran>) this.Transactions, row, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true));
    }
    finally
    {
      ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOSetup>) this.soSetup).Current.DeferPriceDiscountRecalculation;
    }
  }

  protected virtual void ARTran_DeferredCode_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!((e.Row is ARTran row ? row.TranType : (string) null) == "CRM"))
      return;
    if (!(PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }))?.Method == "C"))
      return;
    ((CancelEventArgs) e).Cancel = true;
    if (sender.RaiseExceptionHandling<ARTran.deferredCode>(e.Row, e.NewValue, (Exception) new PXSetPropertyException("On Cash Receipt Deferred Code is not valid for the given document.")))
      throw new PXSetPropertyException("On Cash Receipt Deferred Code is not valid for the given document.");
  }

  protected virtual void ARTran_DeferredCode_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    string oldValue = (string) e.OldValue;
    string deferredCode = row.DeferredCode;
    if (string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(deferredCode))
    {
      ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
      ARInvoice arInvoice = current;
      int? drSchedCntr = arInvoice.DRSchedCntr;
      arInvoice.DRSchedCntr = drSchedCntr.HasValue ? new int?(drSchedCntr.GetValueOrDefault() + 1) : new int?();
      ((PXSelectBase<ARInvoice>) this.Document).Update(current);
    }
    if (string.IsNullOrEmpty(oldValue) || !string.IsNullOrEmpty(deferredCode))
      return;
    ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Document).Current;
    ARInvoice arInvoice1 = current1;
    int? drSchedCntr1 = arInvoice1.DRSchedCntr;
    arInvoice1.DRSchedCntr = drSchedCntr1.HasValue ? new int?(drSchedCntr1.GetValueOrDefault() - 1) : new int?();
    ((PXSelectBase<ARInvoice>) this.Document).Update(current1);
  }

  protected virtual void ARTran_DiscPct_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    e.NewValue = (object) MinGrossProfitValidator<ARTran>.ValidateDiscountPct<ARTran.inventoryID, ARTran.uOM>(sender, row, row.UnitPrice, (Decimal?) e.NewValue);
  }

  protected virtual void ARTran_CuryDiscAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    e.NewValue = (object) MinGrossProfitValidator<ARTran>.ValidateDiscountAmt<ARTran.inventoryID, ARTran.uOM>(sender, row, row.UnitPrice, (Decimal?) e.NewValue);
  }

  protected virtual void ARTran_CuryUnitPrice_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    e.NewValue = (object) MinGrossProfitValidator<ARTran>.ValidateUnitPrice<ARTran.curyInfoID, ARTran.inventoryID, ARTran.uOM>(sender, row, (Decimal?) e.NewValue);
  }

  protected virtual void ARTran_AccruedCost_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran row) || !(row.CostBasis == "S"))
      return;
    row.CuryAccruedCost = new Decimal?(((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().GetDefaultCurrencyInfo().CuryConvCury(row.AccruedCost.GetValueOrDefault()));
  }

  protected virtual void ARTran_CuryTranAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARTran row))
      return;
    this.CalculateAccruedCost(sender, row);
  }

  protected virtual void ARTran_DRTermStartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is ARTran row) || !row.RequiresTerms.GetValueOrDefault())
      return;
    e.NewValue = (object) ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate;
  }

  protected virtual void ARTran_TaxCategoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    sender.SetDefaultExt<ARTran.curyTaxableAmt>(e.Row);
    sender.SetDefaultExt<ARTran.curyTaxAmt>(e.Row);
  }

  protected virtual void ARTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    ARTran oldRow = (ARTran) e.OldRow;
    if (row == null)
      return;
    bool? nullable1 = row.SkipLineDiscountsBuffer;
    if (nullable1.HasValue)
    {
      row.SkipLineDiscounts = row.SkipLineDiscountsBuffer;
      ARTran arTran = row;
      nullable1 = new bool?();
      bool? nullable2 = nullable1;
      arTran.SkipLineDiscountsBuffer = nullable2;
    }
    if ((e.ExternalCall || sender.Graph.IsImport) && sender.ObjectsEqual<ARTran.inventoryID>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.uOM>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.qty>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.branchID>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.siteID>(e.Row, e.OldRow) && sender.ObjectsEqual<ARTran.manualPrice>(e.Row, e.OldRow) && (!sender.ObjectsEqual<ARTran.curyUnitPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.curyExtPrice>(e.Row, e.OldRow)))
    {
      nullable1 = row.ManualPrice;
      bool? manualPrice = oldRow.ManualPrice;
      if (nullable1.GetValueOrDefault() == manualPrice.GetValueOrDefault() & nullable1.HasValue == manualPrice.HasValue)
      {
        row.ManualPrice = new bool?(true);
        row.SkipLineDiscounts = new bool?(false);
      }
    }
    if ((!sender.ObjectsEqual<ARTran.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.baseQty>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.curyUnitPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.curyTranAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.curyExtPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.curyDiscAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.discPct>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.manualDisc>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.skipLineDiscounts>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARTran.discountID>(e.Row, e.OldRow)) && row.LineType != "DS")
      this.RecalculateDiscounts(sender, row);
    bool? nullable3 = row.ManualDisc;
    if (!nullable3.GetValueOrDefault())
    {
      ARDiscount arDiscount = (ARDiscount) PXSelectorAttribute.Select<PX.Objects.SO.SOLine.discountID>(sender, (object) row);
      ARTran arTran = row;
      Decimal? nullable4;
      if (arDiscount != null)
      {
        nullable3 = arDiscount.IsAppliedToDR;
        if (nullable3.GetValueOrDefault())
        {
          nullable4 = row.DiscPct;
          goto label_13;
        }
      }
      nullable4 = new Decimal?(0.0M);
label_13:
      arTran.DiscPctDR = nullable4;
    }
    nullable3 = row.ManualPrice;
    if (!nullable3.GetValueOrDefault())
      row.CuryUnitPriceDR = row.CuryUnitPrice;
    TaxBaseAttribute.Calculate<ARTran.taxCategoryID>(sender, e);
    if (sender.ObjectsEqual<ARTran.accountID, ARTran.deferredCode>(e.Row, e.OldRow) || string.IsNullOrEmpty(row.DeferredCode))
      return;
    DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DeferredCode
    }));
    if (drDeferredCode == null)
      return;
    int? accountId1 = drDeferredCode.AccountID;
    int? accountId2 = row.AccountID;
    if (!(accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue))
      return;
    sender.RaiseExceptionHandling<ARTran.accountID>(e.Row, (object) row.AccountID, (Exception) new PXSetPropertyException("Transaction Account is same as Deferral Account specified in Deferred Code.", (PXErrorLevel) 2));
  }

  protected virtual void ARTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    PXParentAttribute.SetParent(sender, e.Row, typeof (ARRegister), (object) ((PXSelectBase<ARInvoice>) this.Document).Current);
  }

  protected virtual void ARTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (((ARTran) e.Row).CalculateDiscountsOnImport.GetValueOrDefault() && (sender.Graph.IsContractBasedAPI || sender.Graph.IsImportFromExcel))
      this.RecalculateDiscounts(sender, (ARTran) e.Row);
    TaxBaseAttribute.Calculate<ARTran.taxCategoryID>(sender, e);
    if (((ARTran) e.Row).SortOrder.HasValue)
      return;
    ((ARTran) e.Row).SortOrder = ((ARTran) e.Row).LineNbr;
  }

  protected virtual void ARTran_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row != null && row.Released.GetValueOrDefault())
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("The document cannot be deleted as it has been partially released. To resolve the issue, please contact your Acumatica support provider.");
    }
  }

  protected virtual void ARTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null && !((PXSelectBase<ARInvoice>) this.Document).Current.InstallmentNbr.HasValue)
    {
      ((PXSelectBase<ARInvoice>) this.Document).Current.IsTaxValid = new bool?(false);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<ARInvoice>) this.Document).Current);
    }
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null || ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<ARInvoice>) this.Document).Current) == 3 || ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<ARInvoice>) this.Document).Current) == 4)
      return;
    if (row.LineType != "DS")
    {
      try
      {
        ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
        DiscountEngine.DiscountCalculationOptions discountCalculationOptions = this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation;
        if (row.TranType == "PPI")
          discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts;
        this.ARDiscountEngine.RecalculateGroupAndDocumentDiscounts(sender, (PXSelectBase<ARTran>) this.Transactions, (ARTran) null, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, discountCalculationOptions);
      }
      finally
      {
        ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOSetup>) this.soSetup).Current.DeferPriceDiscountRecalculation;
      }
    }
    this.RecalculateTotalDiscount();
  }

  protected virtual void ARTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row is ARTran row)
    {
      PXUIFieldAttribute.SetEnabled<ARTran.defScheduleID>(sender, (object) row, row.TranType == "CRM" || row.TranType == "DRM");
      PXUIFieldAttribute.SetEnabled<ARTran.deferredCode>(sender, (object) row, !row.DefScheduleID.HasValue);
      bool valueOrDefault = row.AccrueCost.GetValueOrDefault();
      PXUIFieldAttribute.SetEnabled<ARTran.curyAccruedCost>(sender, (object) row, valueOrDefault);
      PXUIFieldAttribute.SetEnabled<ARTran.expenseAccountID>(sender, (object) row, valueOrDefault);
      PXUIFieldAttribute.SetEnabled<ARTran.expenseSubID>(sender, (object) row, valueOrDefault);
      PXUIFieldAttribute.SetEnabled<ARTran.expenseAccrualAccountID>(sender, (object) row, valueOrDefault);
      PXUIFieldAttribute.SetEnabled<ARTran.expenseAccrualSubID>(sender, (object) row, valueOrDefault);
    }
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if (current == null)
      return;
    bool? nullable = current.IsMigratedRecord;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current.Released;
    if (nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<ARTran.defScheduleID>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.deferredCode>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.dRTermStartDate>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<ARTran.dRTermEndDate>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
  }

  protected virtual void ARTran_DiscountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTaxTran_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null)
      return;
    e.NewValue = (object) ((PXSelectBase<ARInvoice>) this.Document).Current.TaxZoneID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARTaxTran))
      return;
    PXUIFieldAttribute.SetEnabled<ARTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == 2);
    PXUIFieldAttribute.SetEnabled<ARTran.skipLineDiscounts>(sender, e.Row, ((PXGraph) this).IsCopyPasteContext);
  }

  protected virtual void ARTax_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
  }

  protected virtual void ARTaxTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    PXParentAttribute.SetParent(sender, e.Row, typeof (ARRegister), (object) ((PXSelectBase<ARInvoice>) this.Document).Current);
  }

  protected virtual void ARTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null || e.Operation != 2 && e.Operation != 1)
      return;
    ((TaxTran) e.Row).TaxZoneID = ((PXSelectBase<ARInvoice>) this.Document).Current.TaxZoneID;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<ARTaxTran, ARTaxTran.taxID> e)
  {
    ARTaxTran row = e.Row;
    if (row == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARTaxTran, ARTaxTran.taxID>, ARTaxTran, object>) e).OldValue == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARTaxTran, ARTaxTran.taxID>, ARTaxTran, object>) e).OldValue == e.NewValue)
      return;
    object obj1;
    ((PXSelectBase) this.Taxes).Cache.RaiseFieldDefaulting<ARTaxTran.accountID>((object) row, ref obj1);
    row.AccountID = (int?) obj1;
    object obj2;
    ((PXSelectBase) this.Taxes).Cache.RaiseFieldDefaulting<ARTaxTran.taxType>((object) row, ref obj2);
    row.TaxType = (string) obj2;
    object obj3;
    ((PXSelectBase) this.Taxes).Cache.RaiseFieldDefaulting<ARTaxTran.taxBucketID>((object) row, ref obj3);
    row.TaxBucketID = (int?) obj3;
    object obj4;
    ((PXSelectBase) this.Taxes).Cache.RaiseFieldDefaulting<ARTaxTran.vendorID>((object) row, ref obj4);
    row.VendorID = (int?) obj4;
    object obj5;
    ((PXSelectBase) this.Taxes).Cache.RaiseFieldDefaulting<ARTaxTran.subID>((object) row, ref obj5);
    row.SubID = (int?) obj5;
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

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXStringListAttribute))]
  [PXDefault(typeof (ARDocType.payment))]
  [ARInvoiceType.AppList]
  protected virtual void ARAdjust2_AdjgDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [ARInvoiceType.AdjdRefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARRegisterAlias.refNbr>, And<ARAdjust2.released, Equal<False>, And<ARAdjust2.voided, Equal<False>, And<Where<ARAdjust2.adjdDocType, NotEqual<Current<ARInvoice.docType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Current<ARInvoice.refNbr>>>>>>>>>, LeftJoin<SOAdjust, On<SOAdjust.adjgDocType, Equal<ARRegisterAlias.docType>, And<SOAdjust.adjgRefNbr, Equal<ARRegisterAlias.refNbr>, And<SOAdjust.adjAmt, Greater<decimal0>>>>, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>, LeftJoin<ExternalTransaction, On<ExternalTransaction.transactionID, Equal<ARPayment.cCActualExternalTransactionID>>>>>>>, Where2<Where<ARRegisterAlias.customerID, Equal<Current<ARInvoice.customerID>>, Or<ARRegisterAlias.customerID, Equal<Current<Customer.consolidatingBAccountID>>>>, And2<Where<ARRegisterAlias.docType, Equal<Optional<ARAdjust2.adjgDocType>>>, And<ARRegisterAlias.docDate, LessEqual<Current<ARInvoice.docDate>>, And<ARRegisterAlias.tranPeriodID, LessEqual<Current<ARInvoice.tranPeriodID>>, And<ARRegisterAlias.released, Equal<boolTrue>, And<ARRegisterAlias.paymentsByLinesAllowed, Equal<False>, And<ARRegisterAlias.openDoc, Equal<boolTrue>, And<ARRegisterAlias.pendingPayment, Equal<False>, And<ARRegisterAlias.hold, Equal<False>, And<ARAdjust2.adjdRefNbr, IsNull, And<SOAdjust.adjgRefNbr, IsNull, And<Not<HasUnreleasedVoidPayment<ARPayment.docType, ARPayment.refNbr>>>>>>>>>>>>>>>), new System.Type[] {typeof (ARRegisterAlias.branchID), typeof (ARRegisterAlias.refNbr), typeof (ARRegisterAlias.docDate), typeof (ARRegisterAlias.finPeriodID), typeof (ARRegisterAlias.customerID), typeof (ARRegisterAlias.customerLocationID), typeof (ARRegisterAlias.curyID), typeof (ARRegisterAlias.curyOrigDocAmt), typeof (ARRegisterAlias.curyDocBal), typeof (ARRegisterAlias.status), typeof (ARRegisterAlias.docDesc)}, Filterable = true)]
  protected virtual void ARAdjust2_AdjgRefNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<ARInvoice> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARInvoice, ARInvoice.finPeriodID> e)
  {
    if (e.Row == null || !(e.Row.FinPeriodID != (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARInvoice, ARInvoice.finPeriodID>, ARInvoice, object>) e).NewValue) || !e.Row.IsRetainageDocument.GetValueOrDefault())
      return;
    ARRegisterAlias arRegisterAlias = GraphHelper.RowCast<ARRegisterAlias>((IEnumerable) PXSelectBase<ARTran, PXSelectJoin<ARTran, LeftJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<ARTran.origDocType>, And<ARRegisterAlias.refNbr, Equal<ARTran.origRefNbr>>>>, Where<ARTran.tranType, Equal<Current<ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<ARInvoice.refNbr>>, And<Current<ARInvoice.isRetainageDocument>, Equal<True>>>>, OrderBy<Desc<ARRegisterAlias.finPeriodID>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).FirstOrDefault<ARRegisterAlias>();
    if (arRegisterAlias.FinPeriodID.CompareTo((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARInvoice, ARInvoice.finPeriodID>, ARInvoice, object>) e).NewValue) > 0)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARInvoice, ARInvoice.finPeriodID>, ARInvoice, object>) e).NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARInvoice, ARInvoice.finPeriodID>, ARInvoice, object>) e).NewValue);
      throw new PXSetPropertyException((IBqlTable) e.Row, "The retainage document cannot be posted to the period earlier than the post period of the related original document with retainage. The value must be greater than or equal to {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(arRegisterAlias.FinPeriodID)
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<ARAdjust2> e)
  {
    if (e.Row == null || (PXSelectorAttribute.Select<ARAdjust2.adjgRefNbr>(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<ARAdjust2>>) e).Cache, (object) e.Row) is ARPayment arPayment ? (arPayment.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<ARAdjust2> e)
  {
    ARAdjust2 row = e.Row;
    if (row == null)
      return;
    row.Selected = new bool?(row.CuryAdjdAmt.GetValueOrDefault() != 0M);
    if (row.PPDVATAdjRefNbr == null)
      return;
    row.PPDVATAdjDescription = $"{ARDocType.GetDisplayName(row.PPDVATAdjDocType)}, {row.PPDVATAdjRefNbr}";
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARAdjust2> e)
  {
    ARAdjust2 row = e.Row;
    if (row == null)
      return;
    bool flag1 = row.AdjdRefNbr != null;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust2>>) e).Cache;
    ARAdjust2 arAdjust2 = row;
    int num;
    if (flag1)
    {
      ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        bool? released = current.Released;
        bool flag2 = false;
        num = released.GetValueOrDefault() == flag2 & released.HasValue ? 1 : 0;
      }
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<ARAdjust2.selected>(cache, (object) arAdjust2, num != 0);
    PXUIFieldAttribute.SetEnabled<ARAdjust2.adjdDocType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust2>>) e).Cache, (object) row, !flag1);
    PXUIFieldAttribute.SetEnabled<ARAdjust2.adjdRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust2>>) e).Cache, (object) row, !flag1);
    PXUIFieldAttribute.SetEnabled<ARAdjust2.adjgDocType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust2>>) e).Cache, (object) row, row.AdjgRefNbr == null);
    PXUIFieldAttribute.SetEnabled<ARAdjust2.adjgRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust2>>) e).Cache, (object) row, row.AdjgRefNbr == null);
  }

  protected virtual void ARAdjust2_AdjgRefNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARAdjust2 row = (ARAdjust2) e.Row;
    if (row == null || e.OldValue != null || row.AdjdCustomerID.HasValue)
      return;
    PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARPayment> pxResult = (PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARPayment>) PXResultset<ARRegisterAlias>.op_Implicit(PXSelectBase<ARRegisterAlias, PXSelectJoin<ARRegisterAlias, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARRegisterAlias.curyInfoID>>, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>>>, Where<ARRegisterAlias.docType, Equal<Current<ARAdjust2.adjgDocType>>, And<ARRegisterAlias.refNbr, Equal<Current<ARAdjust2.adjgRefNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    ARPayment payment = PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARPayment>.op_Implicit(pxResult);
    PX.Objects.CM.Extensions.CurrencyInfo info = PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARPayment>.op_Implicit(pxResult);
    ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().StoreCached(info);
    PXCache<ARRegister>.RestoreCopy((ARRegister) payment, (ARRegister) PXResult<ARRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, ARPayment>.op_Implicit(pxResult));
    row.InvoiceID = ((PXSelectBase<ARInvoice>) this.Document).Current.NoteID;
    row.PaymentID = payment.DocType != "CRM" ? payment.NoteID : new Guid?();
    row.MemoID = payment.DocType == "CRM" ? payment.NoteID : new Guid?();
    row.AdjNbr = payment.AdjCntr;
    row.CustomerID = payment.CustomerID;
    row.AdjdCustomerID = ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID;
    row.AdjdBranchID = ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID;
    row.AdjgBranchID = payment.BranchID;
    row.AdjgCuryInfoID = payment.CuryInfoID;
    row.AdjdOrigCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    row.AdjdCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    try
    {
      new ARInvoiceBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>(), (PXGraph) this).InitBalancesFromInvoiceSide((ARAdjust) row, (IInvoice) ((PXSelectBase<ARInvoice>) this.Document).Current, payment);
    }
    catch (Exception ex)
    {
      ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<ARAdjust2.curyDocBal>((object) row, (object) 0M, ex);
    }
  }

  protected virtual void ARAdjust2_AdjgRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARAdjust2 row = (ARAdjust2) e.Row;
    if (row == null || row.AdjgRefNbr == null)
      return;
    PXSelectJoin<ARPayment, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARPayment.docType>, And<ARAdjust.adjdRefNbr, Equal<ARPayment.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARPayment.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARPayment.refNbr>, And<ARAdjust2.released, NotEqual<True>, And<ARAdjust2.voided, NotEqual<True>, And<Where<ARAdjust2.adjdDocType, NotEqual<Required<ARAdjust2.adjdDocType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Required<ARAdjust2.adjdRefNbr>>>>>>>>>>>, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>, And<ARPayment.released, Equal<True>, And<ARPayment.openDoc, Equal<True>, And<ARPayment.hold, NotEqual<True>, And<Where<ARAdjust.adjgRefNbr, IsNotNull, Or<ARAdjust2.adjdRefNbr, IsNotNull>>>>>>>>> pxSelectJoin = new PXSelectJoin<ARPayment, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARPayment.docType>, And<ARAdjust.adjdRefNbr, Equal<ARPayment.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARPayment.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARPayment.refNbr>, And<ARAdjust2.released, NotEqual<True>, And<ARAdjust2.voided, NotEqual<True>, And<Where<ARAdjust2.adjdDocType, NotEqual<Required<ARAdjust2.adjdDocType>>, Or<ARAdjust2.adjdRefNbr, NotEqual<Required<ARAdjust2.adjdRefNbr>>>>>>>>>>>, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>, And<ARPayment.released, Equal<True>, And<ARPayment.openDoc, Equal<True>, And<ARPayment.hold, NotEqual<True>, And<Where<ARAdjust.adjgRefNbr, IsNotNull, Or<ARAdjust2.adjdRefNbr, IsNotNull>>>>>>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectJoin).View, new System.Type[6]
    {
      typeof (ARPayment.docType),
      typeof (ARPayment.refNbr),
      typeof (ARAdjust.adjdDocType),
      typeof (ARAdjust.adjdRefNbr),
      typeof (ARAdjust2.adjgDocType),
      typeof (ARAdjust2.adjgRefNbr)
    }))
    {
      if (!(((PXSelectBase) pxSelectJoin).View.SelectSingle(new object[4]
      {
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr,
        (object) row.AdjgDocType,
        (object) row.AdjgRefNbr
      }) is PXResult<ARPayment, ARAdjust, ARAdjust2> pxResult))
        return;
      ARPayment arPayment = PXResult<ARPayment, ARAdjust, ARAdjust2>.op_Implicit(pxResult);
      ARAdjust arAdjust = PXResult<ARPayment, ARAdjust, ARAdjust2>.op_Implicit(pxResult);
      ARAdjust2 arAdjust2 = PXResult<ARPayment, ARAdjust, ARAdjust2>.op_Implicit(pxResult);
      string str1 = string.IsNullOrEmpty(arAdjust.AdjgDocType) ? arAdjust2.AdjdDocType : arAdjust.AdjgDocType;
      string str2 = string.IsNullOrEmpty(arAdjust.AdjgRefNbr) ? arAdjust2.AdjdRefNbr : arAdjust.AdjgRefNbr;
      ARDocType provider = new ARDocType();
      if (!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str1))
        throw new PXSetPropertyException("{0} {1} is already applied to {2} {3}.", new object[4]
        {
          (object) provider.GetLabel(arPayment.DocType),
          (object) arPayment.RefNbr,
          (object) provider.GetLabel(str1),
          (object) str2
        });
    }
  }

  protected virtual void ARAdjust2_CuryAdjdAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARAdjust2 row = (ARAdjust2) e.Row;
    PX.Objects.CS.Terms terms = PXResultset<PX.Objects.CS.Terms>.op_Implicit(PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Current<ARInvoice.termsID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (terms != null && terms.InstallmentType != "S" && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("No applications can be created for documents with multiple installment credit terms specified.");
    Decimal? nullable = row.CuryDocBal;
    if (!nullable.HasValue)
      this.CalcBalancesFromInvoiceSide(row, false, false);
    nullable = row.CuryDocBal;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.CuryAdjdAmt;
    Decimal num1 = nullable.Value;
    if (valueOrDefault1 + num1 - (Decimal) e.NewValue < 0M)
    {
      object[] objArray = new object[1];
      nullable = row.CuryDocBal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      nullable = row.CuryAdjdAmt;
      Decimal num2 = nullable.Value;
      objArray[0] = (object) (valueOrDefault2 + num2).ToString();
      throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
    }
  }

  protected virtual void ARAdjust2_Selected_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ARAdjust2 row = e.Row as ARAdjust2;
    row.CuryAdjdAmt = row.Selected.GetValueOrDefault() ? row.CuryDocBal : new Decimal?(0M);
    this.CalcBalancesFromInvoiceSide(row, true, false);
  }

  protected virtual void ARAdjust2_CuryAdjdAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARAdjust2 row = e.Row as ARAdjust2;
    this.CalcBalancesFromInvoiceSide(row, true, false);
    ARAdjust2 arAdjust2 = row;
    Decimal? curyAdjdAmt = row.CuryAdjdAmt;
    Decimal num = 0M;
    bool? nullable = new bool?(!(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue));
    arAdjust2.Selected = nullable;
  }

  protected virtual void ARAdjust2_CuryAdjdWOAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ARAdjust2 row = e.Row as ARAdjust2;
    this.CalcBalancesFromInvoiceSide(row, true, false);
    ARAdjust2 arAdjust2 = row;
    Decimal? curyAdjdAmt = row.CuryAdjdAmt;
    Decimal num = 0M;
    bool? nullable = new bool?(!(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue));
    arAdjust2.Selected = nullable;
  }

  protected virtual void ARAdjust2_CuryAdjdWOAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARAdjust2 row = e.Row as ARAdjust2;
    Decimal? nullable;
    if (row.CuryDocBal.HasValue)
    {
      nullable = row.CuryDiscBal;
      if (nullable.HasValue)
      {
        nullable = row.CuryWhTaxBal;
        if (nullable.HasValue)
          goto label_4;
      }
    }
    this.CalcBalancesFromInvoiceSide(row, false, false);
label_4:
    nullable = row.CuryDocBal;
    if (nullable.HasValue)
    {
      nullable = row.CuryWhTaxBal;
      if (nullable.HasValue)
      {
        nullable = row.CuryWhTaxBal;
        Decimal num1 = nullable.Value;
        nullable = row.CuryAdjdWOAmt;
        Decimal num2 = Math.Abs(nullable.Value);
        if (!(num1 + num2 - Math.Abs((Decimal) e.NewValue) < 0M))
          return;
        object[] objArray = new object[1];
        nullable = row.CuryWhTaxBal;
        Decimal num3 = nullable.Value;
        nullable = row.CuryAdjdWOAmt;
        Decimal num4 = Math.Abs(nullable.Value);
        objArray[0] = (object) (num3 + num4).ToString();
        throw new PXSetPropertyException("The customer's write-off limit {0} has been exceeded.", objArray);
      }
    }
    sender.RaiseExceptionHandling<ARAdjust2.adjgRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<ARAdjust2.adjgRefNbr>(sender)
    }));
  }

  protected virtual void ARAdjust2_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) true;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARAdjust2_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ARAdjust2 row = e.Row as ARAdjust2;
    Decimal? curyDocBal = row.CuryDocBal;
    Decimal num1 = 0M;
    if (curyDocBal.GetValueOrDefault() < num1 & curyDocBal.HasValue)
      sender.RaiseExceptionHandling<ARAdjust2.curyAdjdAmt>(e.Row, (object) row.CuryAdjdAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    Decimal? nullable = row.CuryDiscBal;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
      sender.RaiseExceptionHandling<ARAdjust.curyAdjdPPDAmt>(e.Row, (object) row.CuryAdjdPPDAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    nullable = row.CuryWhTaxBal;
    Decimal num3 = 0M;
    if (nullable.GetValueOrDefault() < num3 & nullable.HasValue)
      sender.RaiseExceptionHandling<ARAdjust2.curyAdjdWOAmt>(e.Row, (object) row.CuryAdjdWOAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    nullable = row.CuryAdjdWOAmt;
    Decimal num4 = 0M;
    if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue) && string.IsNullOrEmpty(row.WriteOffReasonCode))
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
    if (!row.VoidAdjNbr.HasValue && adjustedBalanceDelta < 0M)
      sender.RaiseExceptionHandling<ARAdjust2.curyAdjdAmt>((object) row, (object) row.CuryAdjdAmt, (Exception) new PXSetPropertyException("The total application amount, including the cash discount and the write-off amounts, cannot be negative."));
    if (!row.VoidAdjNbr.HasValue || !(adjustedBalanceDelta > 0M))
      return;
    sender.RaiseExceptionHandling<ARAdjust2.curyAdjdAmt>((object) row, (object) row.CuryAdjdAmt, (Exception) new PXSetPropertyException("For reversed applications, the total application amount, including the cash discount and the write-off amounts, cannot be positive."));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ARAdjust2> e)
  {
    if (((PXGraph) this).UnattendedMode || e.TranStatus != null)
      return;
    ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<ARAdjust2>>) e).Cache.VerifyFieldAndRaiseException<ARAdjust2.adjgRefNbr>((object) e.Row);
  }

  /// <summary>
  /// The method to calculate application
  /// balances in Invoice currency.
  /// </summary>
  protected void CalcBalancesFromInvoiceSide(ARAdjust2 adj, bool isCalcRGOL, bool DiscOnDiscDate)
  {
    using (IEnumerator<PXResult<ARPayment>> enumerator = PXSelectBase<ARPayment, PXSelectReadonly<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    }).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      ARPayment payment = PXResult<ARPayment>.op_Implicit(enumerator.Current);
      this.CalcBalancesFromInvoiceSide(adj, payment, isCalcRGOL, DiscOnDiscDate);
    }
  }

  protected virtual void CalcBalancesFromInvoiceSide(
    ARAdjust adj,
    bool isCalcRGOL,
    bool DiscOnDiscDate)
  {
    ARAdjust2 adj1 = new ARAdjust2();
    PXCache<ARAdjust>.RestoreCopy((ARAdjust) adj1, adj);
    this.CalcBalancesFromInvoiceSide(adj1, isCalcRGOL, DiscOnDiscDate);
    PXCache<ARAdjust>.RestoreCopy(adj, (ARAdjust) adj1);
  }

  protected virtual void CalcBalancesFromInvoiceSide(
    ARAdjust adj,
    ARPayment payment,
    bool isCalcRGOL,
    bool DiscOnDiscDate)
  {
    ARAdjust2 adj1 = new ARAdjust2();
    PXCache<ARAdjust>.RestoreCopy((ARAdjust) adj1, adj);
    this.CalcBalancesFromInvoiceSide(adj1, payment, isCalcRGOL, DiscOnDiscDate);
    PXCache<ARAdjust>.RestoreCopy(adj, (ARAdjust) adj1);
  }

  /// <summary>
  /// The method to calculate application
  /// balances in Invoice currency. Only
  /// payment document should be set.
  /// </summary>
  protected virtual void CalcBalancesFromInvoiceSide(
    ARAdjust2 adj,
    ARPayment payment,
    bool isCalcRGOL,
    bool DiscOnDiscDate)
  {
    using (IEnumerator<PXResult<ARInvoice>> enumerator = ((PXSelectBase<ARInvoice>) this.ARInvoice_CustomerID_DocType_RefNbr).Select(new object[3]
    {
      (object) adj.AdjdCustomerID,
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      ARInvoice arInvoice = PXResult<ARInvoice>.op_Implicit(enumerator.Current);
      new ARInvoiceBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>(), (PXGraph) this).CalcBalancesFromInvoiceSide((ARAdjust) adj, (IInvoice) arInvoice, payment, isCalcRGOL, DiscOnDiscDate);
    }
  }

  protected virtual void ARInvoiceDiscountDetail_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    ARInvoiceDiscountDetail row = (ARInvoiceDiscountDetail) e.Row;
  }

  protected virtual void ARInvoiceDiscountDetail_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>())
      return;
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.Document).Current;
    if ((current != null ? (current.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARInvoiceDiscountDetail_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    ARInvoiceDiscountDetail row = (ARInvoiceDiscountDetail) e.Row;
    if (!this.ARDiscountEngine.IsInternalDiscountEngineCall)
    {
      if (row != null)
      {
        try
        {
          ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
          if (row.DiscountID != null)
          {
            this.ARDiscountEngine.InsertManualDocGroupDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, row, row.DiscountID, (string) null, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation);
            this.RecalculateTotalDiscount();
          }
          if (this.ARDiscountEngine.SetExternalManualDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, row, (ARInvoiceDiscountDetail) null, this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true)))
            this.RecalculateTotalDiscount();
        }
        finally
        {
          ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOSetup>) this.soSetup).Current.DeferPriceDiscountRecalculation;
        }
      }
    }
    if (row == null || row.DiscountID == null || row.DiscountSequenceID == null || row.Description != null)
      return;
    object obj;
    sender.RaiseFieldDefaulting<ARInvoiceDiscountDetail.description>((object) row, ref obj);
    sender.SetValue<ARInvoiceDiscountDetail.description>((object) row, obj);
  }

  protected virtual void ARInvoiceDiscountDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ARInvoiceDiscountDetail row = (ARInvoiceDiscountDetail) e.Row;
    ARInvoiceDiscountDetail oldRow = (ARInvoiceDiscountDetail) e.OldRow;
    if (this.ARDiscountEngine.IsInternalDiscountEngineCall)
      return;
    if (row == null)
      return;
    try
    {
      ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      if (!sender.ObjectsEqual<ARInvoiceDiscountDetail.discountID>(e.Row, e.OldRow) || !sender.ObjectsEqual<ARInvoiceDiscountDetail.discountSequenceID>(e.Row, e.OldRow))
      {
        this.ARDiscountEngine.UpdateManualDocGroupDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, row, row.DiscountID, sender.ObjectsEqual<ARInvoiceDiscountDetail.discountID>(e.Row, e.OldRow) ? row.DiscountSequenceID : (string) null, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, new DateTime?(((PXSelectBase<ARInvoice>) this.Document).Current.DocDate.Value), this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation);
        this.RecalculateTotalDiscount();
      }
      if (!sender.ObjectsEqual<ARInvoiceDiscountDetail.skipDiscount>(e.Row, e.OldRow))
      {
        this.ARDiscountEngine.UpdateDocumentDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, row.Type != "D", this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation);
        this.RecalculateTotalDiscount();
      }
      if (!this.ARDiscountEngine.SetExternalManualDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, row, oldRow, this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true)))
        return;
      this.RecalculateTotalDiscount();
    }
    finally
    {
      ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOSetup>) this.soSetup).Current.DeferPriceDiscountRecalculation;
    }
  }

  protected virtual void ARInvoiceDiscountDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    ARInvoiceDiscountDetail row = (ARInvoiceDiscountDetail) e.Row;
    if (!this.ARDiscountEngine.IsInternalDiscountEngineCall && row != null)
    {
      if (row.IsOrigDocDiscount.GetValueOrDefault())
      {
        this.ARDiscountEngine.UpdateGroupAndDocumentDiscountRatesOnly(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (ARTran) null, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, false, true);
      }
      else
      {
        try
        {
          ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
          this.ARDiscountEngine.UpdateDocumentDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, row.Type != null && row.Type != "D" && row.Type != "B", this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation);
        }
        finally
        {
          ((PXSelectBase<ARInvoice>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOSetup>) this.soSetup).Current.DeferPriceDiscountRecalculation;
        }
      }
    }
    this.RecalculateTotalDiscount();
  }

  protected virtual void ARInvoiceDiscountDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    ARInvoiceDiscountDetail row = (ARInvoiceDiscountDetail) e.Row;
    bool flag = row.Type == "B";
    PXCache pxCache1 = sender;
    ARInvoiceDiscountDetail invoiceDiscountDetail1 = row;
    bool? isOrigDocDiscount;
    int num1;
    if (!flag)
    {
      isOrigDocDiscount = row.IsOrigDocDiscount;
      if (!isOrigDocDiscount.GetValueOrDefault())
      {
        num1 = 1;
        goto label_4;
      }
    }
    num1 = 2;
label_4:
    PXDefaultAttribute.SetPersistingCheck<ARInvoiceDiscountDetail.discountID>(pxCache1, (object) invoiceDiscountDetail1, (PXPersistingCheck) num1);
    PXCache pxCache2 = sender;
    ARInvoiceDiscountDetail invoiceDiscountDetail2 = row;
    int num2;
    if (!flag)
    {
      isOrigDocDiscount = row.IsOrigDocDiscount;
      if (!isOrigDocDiscount.GetValueOrDefault())
      {
        num2 = 1;
        goto label_8;
      }
    }
    num2 = 2;
label_8:
    PXDefaultAttribute.SetPersistingCheck<ARInvoiceDiscountDetail.discountSequenceID>(pxCache2, (object) invoiceDiscountDetail2, (PXPersistingCheck) num2);
  }

  protected virtual void ARInvoiceDiscountDetail_DiscountSequenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARInvoiceDiscountDetail_DiscountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultARDiscountCalculationOptions(
    ARInvoice doc)
  {
    return this.GetDefaultARDiscountCalculationOptions(doc, false);
  }

  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultARDiscountCalculationOptions(
    ARInvoice doc,
    bool doNotDeferDiscountCalculation)
  {
    DiscountEngine.DiscountCalculationOptions calculationOptions = (DiscountEngine.DiscountCalculationOptions) (16 /*0x10*/ | (doc == null || !doc.DisableAutomaticDiscountCalculation.GetValueOrDefault() ? 0 : 4));
    if (doc.IsPrepaymentInvoiceDocument())
      calculationOptions |= DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts;
    if (doc == null || !doc.DeferPriceDiscountRecalculation.GetValueOrDefault() || doNotDeferDiscountCalculation)
      return calculationOptions;
    doc.IsPriceAndDiscountsValid = new bool?(false);
    return calculationOptions | DiscountEngine.DiscountCalculationOptions.DisablePriceCalculation | DiscountEngine.DiscountCalculationOptions.DisableGroupAndDocumentDiscounts | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation;
  }

  private PX.Objects.IN.InventoryItem InventoryItemGetByID(int? inventoryID)
  {
    return PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID);
  }

  private DRDeferredCode DeferredCodeGetByID(string deferredCodeID)
  {
    return PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) deferredCodeID
    }));
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, "Transactions", true) == 0)
    {
      keys[(object) "tranType"] = (object) ((PXSelectBase<ARInvoice>) this.Document).Current.DocType;
      keys[(object) "refNbr"] = (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr;
      if (ARInvoiceEntry.DontUpdateExistRecords)
      {
        IDictionary dictionary = keys;
        int? lineCntr = ((PXSelectBase<ARInvoice>) this.Document).Current.LineCntr;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) (lineCntr.HasValue ? new int?(lineCntr.GetValueOrDefault() + 1) : new int?());
        dictionary[(object) "lineNbr"] = (object) local;
      }
    }
    return true;
  }

  private static bool DontUpdateExistRecords
  {
    get
    {
      object obj;
      return PXExecutionContext.Current.Bag.TryGetValue("_DONT_UPDATE_EXIST_RECORDS", out obj) && true.Equals(obj);
    }
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual ARInvoice CalculateExternalTax(ARInvoice invoice) => invoice;

  protected virtual void InsertImportedTaxes()
  {
  }

  public virtual void RecalcUnbilledTax()
  {
  }

  public virtual ARInvoice RecalculateExternalTax(ARInvoice invoice) => invoice;

  public virtual void RecalculateDiscounts(PXCache sender, ARTran line)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() && line.Qty.HasValue && line.CuryTranAmt.HasValue && !line.IsFree.GetValueOrDefault())
    {
      DiscountEngine.DiscountCalculationOptions discountCalculationOptions = this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current) | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation;
      if (line.CalculateDiscountsOnImport.GetValueOrDefault())
        discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport;
      if (line.TranType == "PPI")
        discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts;
      this.ARDiscountEngine.SetDiscounts(sender, (PXSelectBase<ARTran>) this.Transactions, line, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID, ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerLocationID, ((PXSelectBase<ARInvoice>) this.Document).Current.CuryID, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, discountCalculationOptions);
      if (!line.CuryTranAmt.HasValue || line.IsFree.GetValueOrDefault())
        return;
      this.RecalculateTotalDiscount();
    }
    else
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() || ((PXSelectBase<ARInvoice>) this.Document).Current == null)
        return;
      this.ARDiscountEngine.CalculateDocumentDiscountRate(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<ARTran>) this.Transactions, line, (PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails, this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) this.Document).Current));
    }
  }

  public virtual void RecalculateTotalDiscount()
  {
    if (((PXSelectBase<ARInvoice>) this.Document).Current == null)
      return;
    ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) this.Document).Current);
    (Decimal groupDiscountTotal, Decimal documentDiscountTotal, Decimal discountTotal) discountTotals = this.ARDiscountEngine.GetDiscountTotals<ARInvoiceDiscountDetail>((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.curyGroupDiscTotal>((object) ((PXSelectBase<ARInvoice>) this.Document).Current, (object) discountTotals.groupDiscountTotal);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.curyDocumentDiscTotal>((object) ((PXSelectBase<ARInvoice>) this.Document).Current, (object) discountTotals.documentDiscountTotal);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.curyDiscTot>((object) ((PXSelectBase<ARInvoice>) this.Document).Current, (object) discountTotals.discountTotal);
    ((PXSelectBase) this.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<ARInvoice>) this.Document).Current, (object) copy);
  }

  private void CheckApplicationDateAndPeriod(
    PXCache sender,
    ARInvoice document,
    ARAdjust application)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    DateTime? nullable = application != null ? application.AdjdDocDate : throw new ArgumentNullException(nameof (application));
    DateTime? docDate = document.DocDate;
    if ((nullable.HasValue & docDate.HasValue ? (nullable.GetValueOrDefault() > docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 && sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) application, (object) application.AdjdRefNbr, (Exception) new PXSetPropertyException("Unable to apply the document because the application date is earlier than the document date.", (PXErrorLevel) 5)))
      throw new PXRowPersistingException(PXDataUtils.FieldName<ARAdjust.adjdDocDate>(), (object) application.AdjdDocDate, "Unable to apply the document because the application date is earlier than the document date.");
    string adjdTranPeriodId = application.AdjdTranPeriodID;
    if ((adjdTranPeriodId != null ? (adjdTranPeriodId.CompareTo(document.TranPeriodID) > 0 ? 1 : 0) : 0) != 0 && sender.RaiseExceptionHandling<ARAdjust.adjdRefNbr>((object) application, (object) application.AdjdRefNbr, (Exception) new PXSetPropertyException("Unable to apply the document because the application period precedes the financial period of the document.", (PXErrorLevel) 5)))
      throw new PXRowPersistingException(PXDataUtils.FieldName<ARAdjust.adjdFinPeriodID>(), (object) application.AdjdFinPeriodID, "Unable to apply the document because the application period precedes the financial period of the document.");
  }

  public virtual void AddDiscount(PXCache sender, ARInvoice row)
  {
    ARTran instance = (ARTran) ((PXSelectBase) this.Discount_Row).Cache.CreateInstance();
    instance.LineType = "DS";
    instance.Commissionable = new bool?(false);
    instance.DrCr = ((PXSelectBase<ARInvoice>) this.Document).Current.DrCr == "D" ? "C" : "D";
    instance.FreezeManualDisc = new bool?(true);
    ARTran tran = PXResultset<ARTran>.op_Implicit(((PXSelectBase<ARTran>) this.Discount_Row).Select(Array.Empty<object>())) ?? (ARTran) ((PXSelectBase) this.Discount_Row).Cache.Insert((object) instance);
    ARTran copy = (ARTran) ((PXSelectBase) this.Discount_Row).Cache.CreateCopy((object) tran);
    tran.CuryTranAmt = (Decimal?) sender.GetValue<ARInvoice.curyDiscTot>((object) row);
    tran.TaxCategoryID = (string) null;
    using (new PXLocaleScope(((PXSelectBase<Customer>) this.customer).Current.LocaleName))
      tran.TranDesc = PXMessages.LocalizeNoPrefix("Group and Document Discount");
    this.DefaultDiscountAccountAndSubAccount(tran);
    PMProject project;
    if (!tran.TaskID.HasValue && ProjectDefaultAttribute.IsProject((PXGraph) this, tran.ProjectID, out project) && project != null)
      tran.TaskID = this.GetTaskByAccount(tran, project);
    if (CostCodeAttribute.UseCostCode() && !tran.CostCodeID.HasValue)
      tran.CostCodeID = CostCodeAttribute.DefaultCostCode;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Discount_Row).Cache, (object) tran);
    tran.ManualDisc = new bool?(true);
    ((PXSelectBase) this.Discount_Row).Cache.RaiseRowUpdated((object) tran, (object) copy);
    Decimal documentDiscount = this.ARDiscountEngine.GetTotalGroupAndDocumentDiscount<ARInvoiceDiscountDetail>((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails);
    Decimal? curyTranAmt = tran.CuryTranAmt;
    Decimal valueOrDefault = curyTranAmt.GetValueOrDefault();
    if (!(documentDiscount == valueOrDefault & curyTranAmt.HasValue))
      return;
    tran.ManualDisc = new bool?(false);
  }

  private int? GetTaskByAccount(ARTran tran, PMProject project)
  {
    PMAccountTask pmAccountTask = PXResultset<PMAccountTask>.op_Implicit(PXSelectBase<PMAccountTask, PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Required<PMAccountTask.projectID>>, And<PMAccountTask.accountID, Equal<Required<PMAccountTask.accountID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) tran.ProjectID,
      (object) tran.AccountID
    }));
    if (pmAccountTask != null)
      return pmAccountTask.TaskID;
    using (new PXReadDeletedScope(false))
    {
      PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) tran.AccountID
      }));
      throw new PXException("Account Task Mapping is not configured for the following Project: {0}, Account: {1}", new object[2]
      {
        (object) project.ContractCD,
        (object) account.AccountCD
      });
    }
  }

  public object GetValue<Field>(object data) where Field : IBqlField
  {
    return ((PXGraph) this).Caches[BqlCommand.GetItemType(typeof (Field))].GetValue(data, typeof (Field).Name);
  }

  public virtual void DefaultDiscountAccountAndSubAccount(ARTran tran)
  {
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
    object obj1 = this.GetValue<PX.Objects.CR.Location.cDiscountAcctID>((object) current);
    if (obj1 != null)
    {
      tran.AccountID = (int?) obj1;
      ((PXSelectBase) this.Discount_Row).Cache.RaiseFieldUpdated<ARTran.accountID>((object) tran, (object) null);
    }
    if (!tran.AccountID.HasValue)
      return;
    object obj2 = this.GetValue<PX.Objects.CR.Location.cDiscountSubID>((object) current);
    if (obj2 == null)
      return;
    tran.SubID = (int?) obj2;
    ((PXSelectBase) this.Discount_Row).Cache.RaiseFieldUpdated<ARTran.subID>((object) tran, (object) null);
  }

  public virtual void CalculateAccruedCost(PXCache sender, ARTran row)
  {
    if (row == null || !(row.CostBasis == "S"))
      return;
    Decimal? nullable = (Decimal?) PXFormulaAttribute.Evaluate<ARTran.accruedCost>(sender, (object) row);
    sender.SetValueExt<ARTran.accruedCost>((object) row, (object) nullable);
  }

  [PXMergeAttributes]
  [FinPeriodID(typeof (ARAdjust.adjgDocDate), typeof (ARAdjust.adjgBranchID), null, null, null, null, true, false, null, typeof (ARAdjust.adjgTranPeriodID), null, true, true)]
  protected virtual void ARAdjust_AdjgFinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [FinPeriodID(typeof (ARAdjust.adjdDocDate), typeof (ARAdjust.adjdBranchID), null, null, null, null, true, false, null, typeof (ARAdjust.adjdTranPeriodID), null, true, true)]
  protected virtual void ARAdjust_AdjdFinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  protected virtual void ARAdjust_AdjNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDBDefault(typeof (ARInvoice.docDate))]
  protected virtual void ARAdjust_AdjgDocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.adjDiscAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void ARAdjust_CuryAdjgDiscAmt_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.adjWOAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void ARAdjust_CuryAdjgWOAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(0)]
  protected virtual void ARAdjust_AdjdLineNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (ARInvoice.noteID))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARAdjust.memoID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUnboundDefaultAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARAdjust.curyDocBal> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType> e)
  {
    if (e.Row == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType>, ARAdjust, object>) e).OldValue || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType>>) e).ExternalCall)
      return;
    using (new DisableFormulaCalculationScope(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType>>) e).Cache, new System.Type[1]
    {
      typeof (ARAdjust.displayDocType)
    }))
    {
      if (ARDocType.Payable((string) e.NewValue).GetValueOrDefault())
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType>>) e).Cache.SetValue<ARAdjust.adjgDocType>((object) e.Row, (object) ((PXSelectBase<ARInvoice>) this.Document).Current.DocType);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType>>) e).Cache.SetValueExt<ARAdjust.adjdDocType>((object) e.Row, e.NewValue);
      }
      else
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType>>) e).Cache.SetValue<ARAdjust.adjdDocType>((object) e.Row, (object) ((PXSelectBase<ARInvoice>) this.Document).Current.DocType);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayDocType>>) e).Cache.SetValueExt<ARAdjust.adjgDocType>((object) e.Row, e.NewValue);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr> e)
  {
    if (e.Row == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>, ARAdjust, object>) e).OldValue || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).ExternalCall)
      return;
    using (new DisableFormulaCalculationScope(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).Cache, new System.Type[1]
    {
      typeof (ARAdjust.displayRefNbr)
    }))
    {
      if (ARDocType.Payable(e.Row.DisplayDocType).GetValueOrDefault())
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).Cache.SetValue<ARAdjust.invoiceID>((object) e.Row, (object) (Guid?) ARInvoice.PK.Find((PXGraph) this, e.Row.DisplayDocType, (string) e.NewValue)?.NoteID);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).Cache.SetValue<ARAdjust.adjgRefNbr>((object) e.Row, (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).Cache.SetValueExt<ARAdjust.adjdRefNbr>((object) e.Row, e.NewValue);
      }
      else
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).Cache.SetValue<ARAdjust.paymentID>((object) e.Row, (object) (Guid?) ARPayment.PK.Find((PXGraph) this, e.Row.DisplayDocType, (string) e.NewValue)?.NoteID);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).Cache.SetValue<ARAdjust.adjdRefNbr>((object) e.Row, (object) ((PXSelectBase<ARInvoice>) this.Document).Current.RefNbr);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayRefNbr>>) e).Cache.SetValueExt<ARAdjust.adjgRefNbr>((object) e.Row, e.NewValue);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayCuryAmt> e)
  {
    if (e.Row == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayCuryAmt>, ARAdjust, object>) e).OldValue || !((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayCuryAmt>>) e).ExternalCall)
      return;
    using (new DisableFormulaCalculationScope(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayCuryAmt>>) e).Cache, new System.Type[1]
    {
      typeof (ARAdjust.displayCuryAmt)
    }))
    {
      if (ARDocType.Payable(e.Row.DisplayDocType).GetValueOrDefault())
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayCuryAmt>>) e).Cache.SetValueExt<ARAdjust.curyAdjgAmt>((object) e.Row, e.NewValue);
      else
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARAdjust, ARAdjust.displayCuryAmt>>) e).Cache.SetValueExt<ARAdjust.curyAdjdAmt>((object) e.Row, e.NewValue);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.displayCuryAmt> e)
  {
    ARAdjust row = e.Row;
    if (!row.CuryDocBal.HasValue || !row.CuryDiscBal.HasValue || !row.CuryWhTaxBal.HasValue)
    {
      if (ARDocType.Payable(e.Row.DisplayDocType).GetValueOrDefault())
        this.CalcBalances(row, false, false);
      else
        this.CalcBalancesFromInvoiceSide(row, false, false);
    }
    if (!row.CuryDocBal.HasValue)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.displayCuryAmt>, ARAdjust, object>) e).NewValue = (object) row.DisplayRefNbr;
      throw new PXSetPropertyException<ARAdjust.displayRefNbr>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<ARAdjust.adjdRefNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.displayCuryAmt>>) e).Cache)
      });
    }
    if ((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.displayCuryAmt>, ARAdjust, object>) e).NewValue < 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    Decimal num = row.CuryDocBal.Value;
    Decimal? nullable = row.DisplayCuryAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    if (num + valueOrDefault1 - (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARAdjust, ARAdjust.displayCuryAmt>, ARAdjust, object>) e).NewValue < 0M)
    {
      object[] objArray = new object[1];
      nullable = row.CuryDocBal;
      Decimal valueOrDefault2;
      if (!nullable.HasValue)
      {
        Decimal? displayCuryAmt = row.DisplayCuryAmt;
        valueOrDefault2 = (displayCuryAmt.HasValue ? new Decimal?(0M + displayCuryAmt.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      }
      else
        valueOrDefault2 = nullable.GetValueOrDefault();
      objArray[0] = (object) valueOrDefault2.ToString();
      throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
    }
  }

  [PXMergeAttributes]
  [PXDBCalced(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.adjdDocType, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.adjgDocType>>>), typeof (string))]
  [PXFormula(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.adjdDocType, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.adjgDocType>>>))]
  [PXDefault(typeof (ARDocType.invoice))]
  protected virtual void ARAdjust_DisplayDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXDBCalced(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.adjdRefNbr, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.adjgRefNbr>>>), typeof (string))]
  [PXFormula(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.adjdRefNbr, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.adjgRefNbr>>>))]
  [ARInvoiceType.AdjdRefNbr(typeof (Search2<ARRegisterAlias.refNbr, LeftJoinSingleTable<ARInvoice, On<ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, LeftJoin<Customer, On<ARRegisterAlias.customerID, Equal<Customer.bAccountID>>, LeftJoin<PX.Objects.SO.SOInvoice, On<ARRegisterAlias.docType, Equal<PX.Objects.SO.SOInvoice.docType>, And<ARRegisterAlias.refNbr, Equal<PX.Objects.SO.SOInvoice.refNbr>>>>>>, Where<ARRegisterAlias.docType, Equal<Optional<ARAdjust.displayDocType>>, And2<Where<ARRegisterAlias.released, Equal<True>, Or<Current<ARRegister.origModule>, Equal<BatchModule.moduleSO>, And<ARRegisterAlias.docType, Equal<ARDocType.refund>>>>, And<ARRegisterAlias.openDoc, Equal<True>, And<ARRegisterAlias.customerID, In2<Search<PX.Objects.AR.Override.BAccount.bAccountID, Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Optional<ARInvoice.customerID>>, Or<PX.Objects.AR.Override.BAccount.consolidatingBAccountID, Equal<Optional<ARInvoice.customerID>>>>>>, And2<Where<ARRegisterAlias.pendingPPD, NotEqual<True>, Or<Current<ARRegister.pendingPPD>, Equal<True>>>, And2<Where<Current<PX.Objects.AR.ARSetup.migrationMode>, NotEqual<True>, Or<ARRegisterAlias.isMigratedRecord, Equal<Current<ARRegister.isMigratedRecord>>>>, And2<NotExists<Select<ARAdjust, Where<ARAdjust.adjdDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust.adjdRefNbr, Equal<ARRegisterAlias.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.memoID, NotEqual<Current<ARInvoice.noteID>>>>>>>>>, And<NotExists<Select<ARAdjust, Where<ARAdjust.adjgDocType, Equal<ARRegisterAlias.docType>, And<ARAdjust.adjgRefNbr, Equal<ARRegisterAlias.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.memoID, NotEqual<Current<ARInvoice.noteID>>>>>>>>>>>>>>>>>>), new System.Type[] {typeof (ARRegisterAlias.branchID), typeof (ARRegisterAlias.refNbr), typeof (ARRegisterAlias.docDate), typeof (ARRegisterAlias.finPeriodID), typeof (ARRegisterAlias.customerID), typeof (ARRegisterAlias.customerLocationID), typeof (ARRegisterAlias.curyID), typeof (ARRegisterAlias.curyOrigDocAmt), typeof (ARRegisterAlias.curyDocBal), typeof (ARRegisterAlias.status), typeof (ARRegister.dueDate), typeof (ARAdjust.ARInvoice.invoiceNbr), typeof (ARRegisterAlias.docDesc)}, Filterable = true)]
  protected virtual void ARAdjust_DisplayRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (ARRegister.refNbr))]
  [PXRemoveBaseAttribute(typeof (ARInvoiceType.AdjdRefNbrAttribute))]
  [PXRemoveBaseAttribute(typeof (PXRestrictorAttribute))]
  [PXParent(typeof (Select<ARInvoiceAdjusted, Where<ARInvoiceAdjusted.noteID, Equal<Current<ARAdjust.invoiceID>>, And<ARInvoiceAdjusted.docType, Equal<Current<ARAdjust.adjdDocType>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARAdjust.adjdRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARRegister.refNbr))]
  [PXParent(typeof (Select<ARInvoice, Where<ARInvoice.noteID, Equal<Current<ARAdjust.memoID>>, And<ARInvoice.docType, Equal<ARDocType.creditMemo>>>>))]
  [PXParent(typeof (Select<ARPayment, Where<ARPayment.noteID, Equal<Current<ARAdjust.paymentID>>>>))]
  [PXParent(typeof (Select<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<ARAdjust.adjgDocType>>, And<ARPaymentTotals.refNbr, Equal<Current<ARAdjust.adjgRefNbr>>, And<Current<ARAdjust.paymentID>, IsNotNull>>>>), ParentCreate = true, LeaveChildren = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<ARAdjust.adjgRefNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDecimalAttribute))]
  [PXDBCurrency(typeof (ARAdjust.adjdCuryInfoID), typeof (ARAdjust.adjAmt), BaseCalc = false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.voided, Equal<False>>, ARAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<ARInvoice.curyPaymentTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<ARAdjust.voided, Equal<False>>, ARAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<ARInvoiceAdjusted.curyPaymentTotal>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARAdjust.curyAdjdAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (IsNull<Selector<ARAdjust.displayRefNbr, ARRegisterAlias.hasPPDTaxes>, False>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARAdjust.adjdHasPPDTaxes> e)
  {
  }

  [PXMergeAttributes]
  [PXCurrency(typeof (ARAdjust.displayCuryInfoID), typeof (ARAdjust.adjAmt), BaseCalc = false)]
  [PXDBCalced(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.curyAdjgAmt, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.curyAdjdAmt>>>), typeof (Decimal?))]
  [PXFormula(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.curyAdjgAmt, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.curyAdjdAmt>>>))]
  protected virtual void ARAdjust_DisplayCuryAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [CurrencyInfo]
  [PXDBCalced(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.adjgCuryInfoID, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.adjdCuryInfoID>>>), typeof (long?))]
  [PXFormula(typeof (Switch<Case<Where<ARAdjust.invoiceID, IsNotNull>, ARAdjust.adjgCuryInfoID, Case<Where<ARAdjust.paymentID, IsNotNull>, ARAdjust.adjdCuryInfoID>>>))]
  protected virtual void ARAdjust_DisplayCuryInfoID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARAdjust> e)
  {
    ARAdjust row = e.Row;
    if (row == null)
      return;
    bool flag = row.DisplayRefNbr != null;
    PXUIFieldAttribute.SetEnabled<ARAdjust.selected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust>>) e).Cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<ARAdjust.displayDocType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust>>) e).Cache, (object) row, !flag);
    PXUIFieldAttribute.SetEnabled<ARAdjust.displayRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARAdjust>>) e).Cache, (object) row, !flag);
  }

  protected virtual void ARAdjust_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = !((ARAdjust) e.Row).AdjdCustomerID.HasValue;
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
    if ((e.Operation & 3) == 3)
      return;
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
      this.CheckApplicationDateAndPeriod(sender, ((PXSelectBase<ARInvoice>) this.Document).Current, row);
    Decimal? curyDocBal = row.CuryDocBal;
    Decimal num1 = 0M;
    if (curyDocBal.GetValueOrDefault() < num1 & curyDocBal.HasValue)
      sender.RaiseExceptionHandling<ARAdjust.curyAdjgAmt>(e.Row, (object) row.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    int? adjdLineNbr = row.AdjdLineNbr;
    int num2 = 0;
    if (!(adjdLineNbr.GetValueOrDefault() == num2 & adjdLineNbr.HasValue) || (PXSelectorAttribute.Select<ARAdjust.displayRefNbr>(sender, (object) row) is ARRegisterAlias arRegisterAlias ? (arRegisterAlias.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    sender.RaiseExceptionHandling<ARAdjust.displayRefNbr>((object) row, (object) row.AdjdRefNbr, (Exception) new PXSetPropertyException<ARAdjust.displayRefNbr>("The document has the Pay by Line check box selected and cannot be applied on this form, because Amount Paid is not distributed between document lines. To proceed, release the credit memo, open it on the Payments and Applications (AR302000) form, and apply to the document lines."));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<ARAdjust> e)
  {
    if (((PXGraph) this).UnattendedMode || e.TranStatus != null)
      return;
    ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<ARAdjust>>) e).Cache.VerifyFieldAndRaiseException<ARAdjust.adjdRefNbr>((object) e.Row);
  }

  protected virtual void ARAdjust_DisplayRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARAdjust row) || (PXSelectorAttribute.Select<ARAdjust.displayRefNbr>(sender, (object) row, e.NewValue) is ARRegisterAlias arRegisterAlias ? (arRegisterAlias.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    sender.RaiseExceptionHandling<ARAdjust.displayRefNbr>((object) row, e.NewValue, (Exception) new PXSetPropertyException<ARAdjust.displayRefNbr>("The document has the Pay by Line check box selected and cannot be applied on this form, because Amount Paid is not distributed between document lines. To proceed, release the credit memo, open it on the Payments and Applications (AR302000) form, and apply to the document lines."));
  }

  protected virtual void ARAdjust_AdjdRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    try
    {
      ARAdjust row = (ARAdjust) e.Row;
      if (row.CuryDocBal.HasValue && row.DocBal.HasValue)
        return;
      using (IEnumerator<PXResult<ARInvoice>> enumerator = PXSelectBase<ARInvoice, PXViewOf<ARInvoice>.BasedOn<SelectFromBase<ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CM.Extensions.CurrencyInfo>.On<BqlOperand<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, IBqlLong>.IsEqual<ARInvoice.curyInfoID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.docType, Equal<P.AsString>>>>>.And<BqlOperand<ARInvoice.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AdjdDocType,
        (object) row.AdjdRefNbr
      }).GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo> current = (PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current;
        this.ARAdjust_AdjdRefNbr_FieldUpdated(sender, PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), PXResult<ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), row);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  protected virtual void ARAdjust_AdjgRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    try
    {
      ARAdjust row = (ARAdjust) e.Row;
      if (string.Compare(row.AdjgRefNbr, (string) e.OldValue) == 0)
        return;
      using (IEnumerator<PXResult<ARPayment>> enumerator = PXSelectBase<ARPayment, PXViewOf<ARPayment>.BasedOn<SelectFromBase<ARPayment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CM.Extensions.CurrencyInfo>.On<BqlOperand<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, IBqlLong>.IsEqual<ARPayment.curyInfoID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, Equal<P.AsString>>>>>.And<BqlOperand<ARPayment.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AdjgDocType,
        (object) row.AdjgRefNbr
      }).GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo> current = (PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current;
        this.ARAdjust_AdjgRefNbr_FieldUpdated(sender, PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), PXResult<ARPayment, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), row);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  private void ARAdjust_AdjdRefNbr_FieldUpdated(
    PXCache cache,
    ARInvoice invoice,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    ARAdjust adj)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(currencyInfo, ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate);
    adj.CustomerID = ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID;
    adj.AdjgDocDate = ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate;
    adj.AdjgCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    adj.AdjgBranchID = ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID;
    cache.SetValueExt<ARAdjust.adjdCuryInfoID>((object) adj, (object) currencyInfo1.CuryInfoID);
    adj.AdjdCustomerID = invoice.CustomerID;
    adj.AdjdDocDate = invoice.DocDate;
    adj.AdjdCuryInfoID = currencyInfo1.CuryInfoID;
    adj.AdjdOrigCuryInfoID = invoice.CuryInfoID;
    adj.AdjdBranchID = invoice.BranchID;
    adj.AdjdARAcct = invoice.ARAccountID;
    adj.AdjdARSub = invoice.ARSubID;
    adj.InvoiceID = invoice.DocType != "CRM" ? invoice.NoteID : new Guid?();
    adj.PaymentID = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType != "CRM" ? ((PXSelectBase<ARInvoice>) this.Document).Current.NoteID : new Guid?();
    adj.MemoID = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM" ? ((PXSelectBase<ARInvoice>) this.Document).Current.NoteID : new Guid?();
    FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjdFinPeriodID>(cache, (object) adj, invoice.TranPeriodID);
    FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjgFinPeriodID>(cache, (object) adj, ((PXSelectBase<ARInvoice>) this.Document).Current.TranPeriodID);
    adj.Released = new bool?(false);
    this.CalcBalances<ARInvoice>(adj, invoice, false, true);
    Decimal? nullable1 = adj.CuryDocBal;
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? nullable3;
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      nullable3 = nullable2;
      Decimal num = 0M;
      if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
      {
        nullable1 = new Decimal?(Math.Min(nullable1.Value, nullable2.Value));
        goto label_7;
      }
    }
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      nullable3 = nullable2;
      Decimal num1 = 0M;
      if (nullable3.GetValueOrDefault() <= num1 & nullable3.HasValue)
      {
        nullable3 = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
        Decimal num2 = 0M;
        if (nullable3.GetValueOrDefault() > num2 & nullable3.HasValue)
          nullable1 = new Decimal?(0M);
      }
    }
label_7:
    adj.CuryAdjgAmt = nullable1;
    adj.CuryAdjgDiscAmt = new Decimal?(0M);
    adj.CuryAdjgWOAmt = new Decimal?(0M);
    this.FillDisplayFields(adj, (ARRegister) invoice);
    this.CalcBalances<ARInvoice>(adj, invoice, true, true);
    PXCache<ARAdjust>.SyncModel(adj);
  }

  private void ARAdjust_AdjgRefNbr_FieldUpdated(
    PXCache cache,
    ARPayment payment,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    ARAdjust adj)
  {
    adj.CustomerID = payment.CustomerID;
    adj.AdjgDocDate = payment.DocDate;
    adj.AdjgCuryInfoID = currencyInfo.CuryInfoID;
    adj.AdjgBranchID = payment.BranchID;
    adj.AdjdCustomerID = ((PXSelectBase<ARInvoice>) this.Document).Current.CustomerID;
    adj.AdjdDocDate = ((PXSelectBase<ARInvoice>) this.Document).Current.DocDate;
    adj.AdjdCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    adj.AdjdOrigCuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
    adj.AdjdBranchID = ((PXSelectBase<ARInvoice>) this.Document).Current.BranchID;
    adj.AdjdARAcct = ((PXSelectBase<ARInvoice>) this.Document).Current.ARAccountID;
    adj.AdjdARSub = ((PXSelectBase<ARInvoice>) this.Document).Current.ARSubID;
    adj.Hold = new bool?(payment.DocType == "REF");
    adj.InvoiceID = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType != "CRM" ? ((PXSelectBase<ARInvoice>) this.Document).Current.NoteID : new Guid?();
    adj.PaymentID = payment.DocType != "CRM" ? payment.NoteID : new Guid?();
    adj.MemoID = ((PXSelectBase<ARInvoice>) this.Document).Current.DocType == "CRM" ? ((PXSelectBase<ARInvoice>) this.Document).Current.NoteID : new Guid?();
    FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjdFinPeriodID>(cache, (object) adj, ((PXSelectBase<ARInvoice>) this.Document).Current.TranPeriodID);
    FinPeriodIDAttribute.SetPeriodsByMaster<ARAdjust.adjgFinPeriodID>(cache, (object) adj, payment.TranPeriodID);
    adj.Released = new bool?(false);
    this.CalcBalancesFromInvoiceSide(adj, payment, false, true);
    Decimal? nullable1 = adj.CuryDocBal;
    Decimal? nullable2 = new Decimal?(0M);
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      Decimal? nullable3 = nullable2;
      Decimal num = 0M;
      if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
      {
        nullable1 = new Decimal?(Math.Min(nullable1.Value, nullable2.Value));
        goto label_7;
      }
    }
    if (((PXSelectBase<ARInvoice>) this.Document).Current != null)
    {
      Decimal? nullable4 = nullable2;
      Decimal num1 = 0M;
      if (nullable4.GetValueOrDefault() <= num1 & nullable4.HasValue)
      {
        Decimal? curyOrigDocAmt = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryOrigDocAmt;
        Decimal num2 = 0M;
        if (curyOrigDocAmt.GetValueOrDefault() > num2 & curyOrigDocAmt.HasValue)
          nullable1 = new Decimal?(0M);
      }
    }
label_7:
    adj.CuryAdjdAmt = nullable1;
    adj.CuryAdjdDiscAmt = new Decimal?(0M);
    adj.CuryAdjdWOAmt = new Decimal?(0M);
    this.FillDisplayFields(adj, (ARRegister) payment);
    this.CalcBalancesFromInvoiceSide(adj, payment, true, true);
    PXCache<ARAdjust>.SyncModel(adj);
  }

  protected virtual void ARAdjust_CuryAdjgAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalances((ARAdjust) e.Row, true, false);
  }

  protected virtual void ARAdjust_CuryAdjdAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.CalcBalancesFromInvoiceSide((ARAdjust) e.Row, true, false);
  }

  protected virtual void FillDisplayFields(ARAdjust adj, ARRegister doc)
  {
    adj.DisplayBranchID = doc.BranchID;
    adj.DisplayCustomerID = doc.CustomerID;
    adj.DisplayCuryID = doc.CuryID;
    adj.DisplayStatus = doc.Status;
    adj.DisplayDocDesc = doc.DocDesc;
    adj.DisplayDocDate = doc.DocDate;
    adj.DisplayFinPeriodID = doc.FinPeriodID;
  }

  protected void CalcBalances(ARAdjust adj, bool isCalcRGOL, bool DiscOnDiscDate)
  {
    using (IEnumerator<PXResult<ARInvoice>> enumerator = ((PXSelectBase<ARInvoice>) this.ARInvoice_CustomerID_DocType_RefNbr).Select(new object[3]
    {
      (object) adj.AdjdCustomerID,
      (object) adj.AdjdDocType,
      (object) adj.AdjdRefNbr
    }).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      ARInvoice invoice = PXResult<ARInvoice>.op_Implicit(enumerator.Current);
      this.CalcBalances<ARInvoice>(adj, invoice, isCalcRGOL, DiscOnDiscDate);
    }
  }

  protected void CalcBalances<T>(ARAdjust adj, T invoice, bool isCalcRGOL, bool DiscOnDiscDate) where T : class, IBqlTable, IInvoice, new()
  {
    new PaymentBalanceCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>()).CalcBalances(adj.AdjgCuryInfoID, adj.AdjdCuryInfoID, (IInvoice) invoice, (IAdjustment) adj);
    if (DiscOnDiscDate)
      PaymentEntry.CalcDiscount(adj.AdjgDocDate, (IInvoice) invoice, (IAdjustment) adj);
    adj.CuryWhTaxBal = new Decimal?(0M);
    adj.WhTaxBal = new Decimal?(0M);
    invoice.CuryWhTaxBal = new Decimal?(0M);
    invoice.WhTaxBal = new Decimal?(0M);
    new PaymentBalanceAjuster((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>()).AdjustBalance((IAdjustment) adj);
    if (!isCalcRGOL || adj.Voided.GetValueOrDefault())
      return;
    new PaymentRGOLCalculator((IPXCurrencyHelper) ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>(), (IAdjustment) adj, adj.ReverseGainLoss).Calculate((IInvoice) invoice);
  }

  public virtual ARInvoice CreatePPDCreditMemo(
    ARPPDTaxAdjustmentParameters filter,
    List<PendingPPDARTaxAdjApp> list,
    ref int index)
  {
    bool flag = true;
    ARInvoice invoice1 = (ARInvoice) ((PXSelectBase) this.Document).Cache.CreateInstance();
    foreach (PendingPPDARTaxAdjApp doc in list)
    {
      if (flag)
      {
        flag = false;
        index = doc.Index.Value;
        invoice1 = this.PopulateVATAdjustingDocument(filter, invoice1, doc, "CRM");
      }
      this.AddTaxAndVATAdjustmentDocDetails(doc);
    }
    EnumerableExtensions.ForEach<ARInvoiceDiscountDetail>(GraphHelper.RowCast<ARInvoiceDiscountDetail>((IEnumerable) ((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).Select(Array.Empty<object>())), (System.Action<ARInvoiceDiscountDetail>) (discountDetail => ((PXSelectBase) this.ARDiscountDetails).Cache.Delete((object) discountDetail)));
    if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal.GetValueOrDefault())
    {
      invoice1.CuryOrigDocAmt = invoice1.CuryDocBal;
      ((PXSelectBase) this.Document).Cache.Update((object) invoice1);
    }
    invoice1.Hold = new bool?(false);
    ARInvoice invoice2 = ((PXSelectBase<ARInvoice>) this.Document).Update(invoice1);
    this.AddPPDCreditMemoApplications(list);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.discDate>((object) invoice2, (object) invoice2.DueDate);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.curyOrigDiscAmt>((object) invoice2, (object) 0M);
    ((PXAction) this.Save).Press();
    this.UpdateARAdjustPPDVATAdjRef(list, invoice2);
    return invoice2;
  }

  public virtual ARInvoice CreatePPDDebitMemo(
    ARPPDTaxAdjustmentParameters filter,
    List<PendingPPDARTaxAdjApp> list,
    ref int index)
  {
    bool flag = true;
    ARInvoice invoice1 = (ARInvoice) ((PXSelectBase) this.Document).Cache.CreateInstance();
    foreach (PendingPPDARTaxAdjApp doc in list)
    {
      if (flag)
      {
        flag = false;
        index = doc.Index.Value;
        invoice1 = this.PopulateVATAdjustingDocument(filter, invoice1, doc, "DRM");
      }
      this.AddTaxAndVATAdjustmentDocDetails(doc);
    }
    EnumerableExtensions.ForEach<ARInvoiceDiscountDetail>(GraphHelper.RowCast<ARInvoiceDiscountDetail>((IEnumerable) ((PXSelectBase<ARInvoiceDiscountDetail>) this.ARDiscountDetails).Select(Array.Empty<object>())), (System.Action<ARInvoiceDiscountDetail>) (discountDetail => ((PXSelectBase) this.ARDiscountDetails).Cache.Delete((object) discountDetail)));
    if (((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RequireControlTotal.GetValueOrDefault())
    {
      invoice1.CuryOrigDocAmt = invoice1.CuryDocBal;
      ((PXSelectBase) this.Document).Cache.Update((object) invoice1);
    }
    invoice1.Hold = new bool?(false);
    ARInvoice invoice2 = ((PXSelectBase<ARInvoice>) this.Document).Update(invoice1);
    this.AddPPDDebitMemoApplications(list);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.discDate>((object) invoice2, (object) invoice2.DueDate);
    ((PXSelectBase) this.Document).Cache.SetValueExt<ARInvoice.curyOrigDiscAmt>((object) invoice2, (object) 0M);
    ((PXAction) this.Save).Press();
    this.UpdateARAdjustPPDVATAdjRef(list, invoice2);
    return invoice2;
  }

  protected virtual ARInvoice PopulateVATAdjustingDocument(
    ARPPDTaxAdjustmentParameters filter,
    ARInvoice invoice,
    PendingPPDARTaxAdjApp doc,
    string docType)
  {
    ARInvoiceEntry.MultiCurrency extension = ((PXGraph) this).GetExtension<ARInvoiceEntry.MultiCurrency>();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = extension.CloneCurrencyInfo(extension.GetCurrencyInfo(doc.InvCuryInfoID));
    invoice.DocType = docType;
    invoice.DocDate = filter.GenerateOnePerCustomer.GetValueOrDefault() ? filter.TaxAdjustmentDate : doc.AdjgDocDate;
    invoice.BranchID = doc.AdjdBranchID;
    invoice.CuryInfoID = currencyInfo.CuryInfoID;
    string masterFinPeriodID = filter.GenerateOnePerCustomer.GetValueOrDefault() ? this.FinPeriodRepository.GetByID(filter.FinPeriodID, PXAccess.GetParentOrganizationID(filter.BranchID)).MasterFinPeriodID : doc.AdjgTranPeriodID;
    FinPeriodIDAttribute.SetPeriodsByMaster<ARInvoice.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) invoice, masterFinPeriodID);
    invoice = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) this.Document).Insert(invoice));
    invoice.CustomerID = doc.AdjdCustomerID;
    invoice.CustomerLocationID = doc.InvCustomerLocationID;
    invoice.CuryInfoID = currencyInfo.CuryInfoID;
    invoice.CuryID = currencyInfo.CuryID;
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) doc.CustomerID
    }));
    invoice.DocDesc = PXDBLocalizableStringAttribute.GetTranslation(((PXGraph) this).Caches[typeof (PX.Objects.AR.ARSetup)], (object) ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current, "pPDCreditMemoDescr", customer?.LocaleName);
    invoice.ARAccountID = doc.AdjdARAcct;
    invoice.ARSubID = doc.AdjdARSub;
    invoice.TaxZoneID = doc.InvTaxZoneID;
    invoice.TaxCalcMode = doc.InvTaxCalcMode;
    invoice.PendingPPD = new bool?(true);
    invoice = ((PXSelectBase<ARInvoice>) this.Document).Update(invoice);
    if (invoice.TaxCalcMode != doc.InvTaxCalcMode)
    {
      invoice.TaxCalcMode = doc.InvTaxCalcMode;
      invoice = ((PXSelectBase<ARInvoice>) this.Document).Update(invoice);
    }
    invoice.DontPrint = new bool?(true);
    invoice.DontEmail = new bool?(true);
    return invoice;
  }

  protected virtual void AddPPDCreditMemoApplications(List<PendingPPDARTaxAdjApp> list)
  {
    foreach (PendingPPDARTaxAdjApp pendingPpdarTaxAdjApp in list)
    {
      ARAdjust arAdjust = ((PXSelectBase<ARAdjust>) this.Adjustments_1).Insert(new ARAdjust()
      {
        AdjdDocType = pendingPpdarTaxAdjApp.AdjdDocType,
        AdjdRefNbr = pendingPpdarTaxAdjApp.AdjdRefNbr
      });
      ((PXSelectBase) this.Adjustments_1).Cache.SetDefaultExt<ARAdjust.adjdHasPPDTaxes>((object) arAdjust);
      arAdjust.CuryAdjgAmt = pendingPpdarTaxAdjApp.InvCuryDocBal;
      ((PXSelectBase<ARAdjust>) this.Adjustments_1).Update(arAdjust);
    }
  }

  protected virtual void AddPPDDebitMemoApplications(List<PendingPPDARTaxAdjApp> list)
  {
    ((PXAction) this.loadDocuments).Press();
    foreach (ARAdjust2 arAdjust2 in GraphHelper.RowCast<ARAdjust2>(((PXSelectBase) this.Adjustments).View.SelectExternal()))
    {
      ARAdjust2 adj = arAdjust2;
      if (list.Exists((Predicate<PendingPPDARTaxAdjApp>) (a => a.AdjdDocType == adj.AdjgDocType && a.AdjdRefNbr == adj.AdjgRefNbr)))
      {
        ((PXSelectBase) this.Adjustments).Cache.SetDefaultExt<ARAdjust.adjdHasPPDTaxes>((object) adj);
        this.UpdateARAdjustCuryAdjdAmt(adj, adj.CuryDocBal);
      }
    }
  }

  protected virtual void UpdateARAdjustPPDVATAdjRef(
    List<PendingPPDARTaxAdjApp> list,
    ARInvoice invoice)
  {
    list.ForEach((System.Action<PendingPPDARTaxAdjApp>) (doc => PXUpdate<Set<ARAdjust.pPDVATAdjRefNbr, Required<ARAdjust.pPDVATAdjRefNbr>, Set<ARAdjust.pPDVATAdjDocType, Required<ARAdjust.pPDVATAdjDocType>>>, ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjgDocType, Equal<Required<ARAdjust.adjgDocType>>, And<ARAdjust.adjgRefNbr, Equal<Required<ARAdjust.adjgRefNbr>>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.pendingPPD, Equal<True>>>>>>>>>.Update((PXGraph) this, new object[6]
    {
      (object) invoice.RefNbr,
      (object) invoice.DocType,
      (object) doc.AdjdDocType,
      (object) doc.AdjdRefNbr,
      (object) doc.AdjgDocType,
      (object) doc.AdjgRefNbr
    })));
  }

  public virtual void AddTaxAndVATAdjustmentDocDetails(PendingPPDARTaxAdjApp doc)
  {
    ARTaxTran arTaxTran1 = (ARTaxTran) null;
    Decimal? TaxTotal = new Decimal?(0M);
    Decimal? InclusiveTotal = new Decimal?(0M);
    Decimal? nullable1 = new Decimal?(0M);
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? curyAdjdPpdAmt1 = doc.CuryAdjdPPDAmt;
    Decimal? invCuryOrigDocAmt1 = doc.InvCuryOrigDocAmt;
    Decimal? nullable3 = curyAdjdPpdAmt1.HasValue & invCuryOrigDocAmt1.HasValue ? new Decimal?(curyAdjdPpdAmt1.GetValueOrDefault() / invCuryOrigDocAmt1.GetValueOrDefault()) : new Decimal?();
    Decimal cashDiscPercent = nullable3.Value;
    PXResultset<ARTaxTran> taxes = PXSelectBase<ARTaxTran, PXSelectJoin<ARTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<ARTaxTran.taxID>>>, Where<ARTaxTran.module, Equal<BatchModule.moduleAR>, And<ARTaxTran.tranType, Equal<Required<ARTaxTran.tranType>>, And<ARTaxTran.refNbr, Equal<Required<ARTaxTran.refNbr>>, And<PX.Objects.TX.Tax.taxApplyTermsDisc, Equal<CSTaxTermsDiscount.toPromtPayment>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) doc.AdjdDocType,
      (object) doc.AdjdRefNbr
    });
    foreach (PXResult<ARTaxTran, PX.Objects.TX.Tax> pxResult in taxes)
    {
      PX.Objects.TX.Tax tax = PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      ARTaxTran copy = PXCache<ARTaxTran>.CreateCopy(PXResult<ARTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult));
      ARTaxTran arTaxTran2 = PXResultset<ARTaxTran>.op_Implicit(((PXSelectBase<ARTaxTran>) this.Taxes).Search<ARTaxTran.taxID>((object) copy.TaxID, Array.Empty<object>()));
      if (arTaxTran2 == null)
      {
        copy.TranType = (string) null;
        copy.RefNbr = (string) null;
        copy.TaxPeriodID = (string) null;
        copy.FinPeriodID = (string) null;
        copy.Released = new bool?(false);
        copy.Voided = new bool?(false);
        copy.CuryInfoID = ((PXSelectBase<ARInvoice>) this.Document).Current.CuryInfoID;
        TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.NoCalc);
        arTaxTran2 = ((PXSelectBase<ARTaxTran>) this.Taxes).Insert(copy);
        arTaxTran2.CuryTaxableAmt = new Decimal?(0M);
        arTaxTran2.CuryTaxAmt = new Decimal?(0M);
        arTaxTran2.CuryTaxAmtSumm = new Decimal?(0M);
        arTaxTran2.TaxRate = copy.TaxRate;
      }
      int num1 = ARPPDCreditMemoProcess.CalculateDiscountedTaxes(((PXSelectBase) this.Taxes).Cache, copy, cashDiscPercent) ? 1 : 0;
      Decimal? nullable4 = nullable2;
      Decimal? nullable5 = copy.CuryDiscountedPrice;
      Decimal? nullable6;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
      nullable2 = nullable6;
      nullable5 = copy.CuryTaxableAmt;
      nullable4 = copy.CuryDiscountedTaxableAmt;
      Decimal? nullable7;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable7 = nullable3;
      }
      else
        nullable7 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
      Decimal? nullable8 = nullable7;
      nullable4 = copy.CuryTaxAmt;
      nullable5 = copy.CuryDiscountedPrice;
      Decimal? nullable9;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable9 = nullable3;
      }
      else
        nullable9 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
      Decimal? nullable10 = nullable9;
      nullable5 = copy.CuryTaxAmtSumm;
      nullable4 = copy.CuryDiscountedPrice;
      if (nullable5.HasValue & nullable4.HasValue)
      {
        Decimal num2 = nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault();
      }
      ARTaxTran arTaxTran3 = arTaxTran2;
      nullable4 = arTaxTran3.CuryTaxableAmt;
      nullable5 = nullable8;
      Decimal? nullable11;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable11 = nullable3;
      }
      else
        nullable11 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
      arTaxTran3.CuryTaxableAmt = nullable11;
      ARTaxTran arTaxTran4 = arTaxTran2;
      nullable5 = arTaxTran4.CuryTaxAmt;
      nullable4 = nullable10;
      Decimal? nullable12;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable12 = nullable3;
      }
      else
        nullable12 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
      arTaxTran4.CuryTaxAmt = nullable12;
      ARTaxTran arTaxTran5 = arTaxTran2;
      nullable4 = arTaxTran5.CuryTaxAmtSumm;
      nullable5 = nullable10;
      Decimal? nullable13;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable13 = nullable3;
      }
      else
        nullable13 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
      arTaxTran5.CuryTaxAmtSumm = nullable13;
      TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
      ((PXSelectBase<ARTaxTran>) this.Taxes).Update(arTaxTran2);
      if (num1 != 0)
      {
        nullable5 = nullable1;
        nullable4 = copy.CuryDiscountedTaxableAmt;
        Decimal? nullable14;
        if (!(nullable5.HasValue & nullable4.HasValue))
        {
          nullable3 = new Decimal?();
          nullable14 = nullable3;
        }
        else
          nullable14 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
        nullable1 = nullable14;
        if (arTaxTran1 != null)
        {
          nullable4 = arTaxTran2.CuryTaxableAmt;
          nullable5 = arTaxTran1.CuryTaxableAmt;
          if (!(nullable4.GetValueOrDefault() > nullable5.GetValueOrDefault() & nullable4.HasValue & nullable5.HasValue))
            goto label_31;
        }
        arTaxTran1 = arTaxTran2;
      }
label_31:
      bool flag = PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>();
      if (tax.TaxCalcLevel == "0" && (!flag || ((PXSelectBase<ARInvoice>) this.Document).Current.TaxCalcMode != "N") || flag && ((PXSelectBase<ARInvoice>) this.Document).Current.TaxCalcMode == "G")
      {
        nullable5 = InclusiveTotal;
        nullable4 = nullable10;
        Decimal? nullable15;
        if (!(nullable5.HasValue & nullable4.HasValue))
        {
          nullable3 = new Decimal?();
          nullable15 = nullable3;
        }
        else
          nullable15 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
        InclusiveTotal = nullable15;
      }
      else
      {
        nullable4 = TaxTotal;
        nullable5 = nullable10;
        Decimal? nullable16;
        if (!(nullable4.HasValue & nullable5.HasValue))
        {
          nullable3 = new Decimal?();
          nullable16 = nullable3;
        }
        else
          nullable16 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
        TaxTotal = nullable16;
      }
    }
    Decimal? invCuryOrigDocAmt2 = doc.InvCuryOrigDocAmt;
    Decimal? invCuryOrigDiscAmt = doc.InvCuryOrigDiscAmt;
    Decimal? nullable17 = invCuryOrigDocAmt2.HasValue & invCuryOrigDiscAmt.HasValue ? new Decimal?(invCuryOrigDocAmt2.GetValueOrDefault() - invCuryOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable18 = nullable1;
    Decimal? nullable19 = nullable2;
    Decimal? nullable20 = nullable18.HasValue & nullable19.HasValue ? new Decimal?(nullable18.GetValueOrDefault() + nullable19.GetValueOrDefault()) : new Decimal?();
    nullable19 = doc.InvCuryOrigDiscAmt;
    Decimal? curyAdjdPpdAmt2 = doc.CuryAdjdPPDAmt;
    if (nullable19.GetValueOrDefault() == curyAdjdPpdAmt2.GetValueOrDefault() & nullable19.HasValue == curyAdjdPpdAmt2.HasValue && arTaxTran1 != null)
    {
      Decimal? curyVatTaxableTotal = doc.InvCuryVatTaxableTotal;
      Decimal? invCuryTaxTotal = doc.InvCuryTaxTotal;
      Decimal? nullable21 = curyVatTaxableTotal.HasValue & invCuryTaxTotal.HasValue ? new Decimal?(curyVatTaxableTotal.GetValueOrDefault() + invCuryTaxTotal.GetValueOrDefault()) : new Decimal?();
      nullable19 = doc.InvCuryOrigDocAmt;
      if (nullable21.GetValueOrDefault() == nullable19.GetValueOrDefault() & nullable21.HasValue == nullable19.HasValue)
      {
        nullable19 = nullable20;
        nullable21 = nullable17;
        if (!(nullable19.GetValueOrDefault() == nullable21.GetValueOrDefault() & nullable19.HasValue == nullable21.HasValue))
        {
          ARTaxTran arTaxTran6 = arTaxTran1;
          nullable21 = arTaxTran6.CuryTaxableAmt;
          Decimal? nullable22 = nullable20;
          Decimal? nullable23 = nullable17;
          nullable19 = nullable22.HasValue & nullable23.HasValue ? new Decimal?(nullable22.GetValueOrDefault() - nullable23.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable24;
          if (!(nullable21.HasValue & nullable19.HasValue))
          {
            nullable23 = new Decimal?();
            nullable24 = nullable23;
          }
          else
            nullable24 = new Decimal?(nullable21.GetValueOrDefault() + nullable19.GetValueOrDefault());
          arTaxTran6.CuryTaxableAmt = nullable24;
          TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
          ((PXSelectBase<ARTaxTran>) this.Taxes).Update(arTaxTran1);
        }
      }
    }
    this.AddVATAdjustmentDetails(doc, TaxTotal, InclusiveTotal, taxes);
  }

  public virtual void AddVATAdjustmentDetails(
    PendingPPDARTaxAdjApp doc,
    Decimal? TaxTotal,
    Decimal? InclusiveTotal,
    PXResultset<ARTaxTran> taxes)
  {
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) doc.AdjdCustomerID
    }));
    ARTran arTran1 = ((PXSelectBase<ARTran>) this.Transactions).Insert();
    arTran1.BranchID = doc.AdjdBranchID;
    using (new PXLocaleScope(customer.LocaleName))
      arTran1.TranDesc = $"{PXMessages.LocalizeNoPrefix(ARInvoiceEntry.DocTypes[doc.AdjdDocType])} {doc.AdjdRefNbr}, {PXMessages.LocalizeNoPrefix("Payment")} {doc.AdjgRefNbr}";
    ARTran arTran2 = arTran1;
    Decimal? nullable1 = doc.CuryAdjdPPDAmt;
    Decimal? nullable2 = TaxTotal;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    arTran2.CuryExtPrice = nullable3;
    ARTran arTran3 = arTran1;
    nullable2 = arTran1.CuryExtPrice;
    nullable1 = InclusiveTotal;
    Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    arTran3.CuryTaxableAmt = nullable4;
    ARTran arTran4 = arTran1;
    nullable1 = TaxTotal;
    nullable2 = InclusiveTotal;
    Decimal? nullable5 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    arTran4.CuryTaxAmt = nullable5;
    arTran1.AccountID = customer.DiscTakenAcctID;
    arTran1.SubID = customer.DiscTakenSubID;
    arTran1.TaxCategoryID = (string) null;
    arTran1.IsFree = new bool?(true);
    arTran1.ManualDisc = new bool?(true);
    arTran1.CuryDiscAmt = new Decimal?(0M);
    arTran1.DiscPct = new Decimal?(0M);
    arTran1.GroupDiscountRate = new Decimal?(1M);
    arTran1.DocumentDiscountRate = new Decimal?(1M);
    if (taxes.Count == 1)
    {
      ARTaxTran arTaxTran = PXResult<ARTaxTran>.op_Implicit(taxes[0]);
      ARTran arTran5 = PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelectJoin<ARTran, InnerJoin<ARTax, On<ARTax.tranType, Equal<ARTran.tranType>, And<ARTax.refNbr, Equal<ARTran.refNbr>, And<ARTax.lineNbr, Equal<ARTran.lineNbr>>>>>, Where<ARTax.tranType, Equal<Required<ARTax.tranType>>, And<ARTax.refNbr, Equal<Required<ARTax.refNbr>>, And<ARTax.taxID, Equal<Required<ARTax.taxID>>>>>, OrderBy<Asc<ARTran.lineNbr>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
      {
        (object) arTaxTran.TranType,
        (object) arTaxTran.RefNbr,
        (object) arTaxTran.TaxID
      }));
      if (arTran5 != null)
        arTran1.TaxCategoryID = arTran5.TaxCategoryID;
    }
    ((PXSelectBase<ARTran>) this.Transactions).Update(arTran1);
  }

  public virtual bool IsApprovalRequired(ARInvoice doc)
  {
    return EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.ApprovableDocTypes.Contains(doc.DocType);
  }

  protected virtual void setDontApproveValue(ARInvoice doc, PXCache cache)
  {
    bool flag = doc.DocType == "SMC" || doc.DocType == "FCH" || doc.InstallmentNbr.HasValue || !this.IsApprovalRequired(doc);
    cache.SetValue<ARRegister.dontApprove>((object) doc, (object) flag);
  }

  public class UnlinkContractUsagesOnDeleteScope : 
    FlaggedModeScopeBase<ARInvoiceEntry.UnlinkContractUsagesOnDeleteScope>
  {
  }

  public class SuppressRecalculateDiscountScope : 
    FlaggedModeScopeBase<ARInvoiceEntry.SuppressRecalculateDiscountScope>
  {
  }

  public class ARInvoiceEntryDocumentExtension : InvoiceGraphExtension<ARInvoiceEntry, ARAdjust2>
  {
    public override void SuppressApproval() => this.Base.Approval.SuppressApproval = true;

    public override PXSelectBase<ARAdjust2> AppliedAdjustments
    {
      get => (PXSelectBase<ARAdjust2>) this.Base.Adjustments;
    }

    public PXSelectBase<ARAdjust> ApplyingAdjustments
    {
      get => (PXSelectBase<ARAdjust>) this.Base.Adjustments_1;
    }

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice>((PXSelectBase) this.Base.Document);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.AllTransactions);
    }

    protected override InvoiceMapping GetDocumentMapping()
    {
      return new InvoiceMapping(typeof (ARInvoice))
      {
        HeaderTranPeriodID = typeof (ARInvoice.tranPeriodID),
        HeaderDocDate = typeof (ARInvoice.docDate)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (ARTran));
    }

    protected override void _(PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice> e)
    {
      base._(e);
      if (!this.ShouldUpdateAdjustmentsOnDocumentUpdated(e))
        return;
      foreach (PXResult<ARAdjust> pxResult in this.ApplyingAdjustments.Select(Array.Empty<object>()))
      {
        ARAdjust row = PXResult<ARAdjust>.op_Implicit(pxResult);
        if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.branchID>((object) e.Row, (object) e.OldRow))
          ((PXSelectBase) this.ApplyingAdjustments).Cache.SetDefaultExt<Adjust.adjgBranchID>((object) row);
        if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerDocDate>((object) e.Row, (object) e.OldRow))
          ((PXSelectBase) this.ApplyingAdjustments).Cache.SetDefaultExt<Adjust.adjgDocDate>((object) row);
        if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerTranPeriodID>((object) e.Row, (object) e.OldRow))
          FinPeriodIDAttribute.SetPeriodsByMaster<Adjust.adjgFinPeriodID>(((PXSelectBase) this.ApplyingAdjustments).Cache, (object) row, e.Row.HeaderTranPeriodID);
        if (((PXSelectBase) this.ApplyingAdjustments).Cache is PXModelExtension<Adjust> cache)
          cache.UpdateExtensionMapping((object) row);
        GraphHelper.MarkUpdated(((PXSelectBase) this.ApplyingAdjustments).Cache, (object) row);
      }
    }
  }

  public class MultiCurrency : ARMultiCurrencyGraph<ARInvoiceEntry, ARInvoice>
  {
    protected override string DocumentStatus
    {
      get => ((PXSelectBase<ARInvoice>) this.Base.Document).Current?.Status;
    }

    protected override MultiCurrencyGraph<ARInvoiceEntry, ARInvoice>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ARInvoiceEntry, ARInvoice>.DocumentMapping(typeof (ARInvoice))
      {
        DocumentDate = typeof (ARInvoice.docDate),
        BAccountID = typeof (ARInvoice.customerID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[9]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.Transactions,
        (PXSelectBase) this.Base.Tax_Rows,
        (PXSelectBase) this.Base.Taxes,
        (PXSelectBase) this.Base.ARDiscountDetails,
        (PXSelectBase) this.Base.Adjustments_1,
        (PXSelectBase) this.Base.Adjustments_Inv,
        (PXSelectBase) this.Base.salesPerTrans,
        (PXSelectBase) this.Base.FreightDetailsDummy
      };
    }

    protected override bool AllowOverrideCury()
    {
      PXSelectJoin<ARInvoice, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>>, Where<ARInvoice.docType, Equal<Optional<ARInvoice.docType>>, And2<Where<ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<ARInvoice.released, Equal<True>>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> document = this.Base.Document;
      return (document != null ? (((bool?) ((PXSelectBase<ARInvoice>) document).Current?.IsCancellation).GetValueOrDefault() ? 1 : 0) : 0) == 0 && base.AllowOverrideCury();
    }

    protected virtual void _(
      PX.Data.Events.FieldSelecting<ARPayment, ARPayment.curyID> e)
    {
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<ARPayment, ARPayment.curyID>>) e).ReturnValue = (object) this.CuryIDFieldSelecting<ARRegister.curyInfoID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<ARPayment, ARPayment.curyID>>) e).Cache, (object) e.Row);
    }

    public void StoreCached(PX.Objects.CM.Extensions.CurrencyInfo info)
    {
      if (((PXSelectBase) this.currencyinfo).Cache.Locate((object) info) != null)
        return;
      ((PXSelectBase) this.currencyinfo).Cache.SetStatus((object) info, (PXEntryStatus) 0);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
    {
      base._(e);
      this.Base.SetDefaultsAfterCustomerIDChanging(((PXSelectBase) this.Base.Document).Cache, (ARInvoice) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.MultiCurrency.Document>(e.Row));
    }
  }

  public class CostAccrual : NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>
  {
    protected virtual void ARTran_SubID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      ARTran row = (ARTran) e.Row;
      if (row == null || !row.SubID.HasValue)
        return;
      bool? nullable = row.IsStockItem;
      if (nullable.GetValueOrDefault())
        return;
      nullable = row.AccrueCost;
      if (!nullable.GetValueOrDefault())
        return;
      INPostClass inPostClass = INPostClass.PK.Find((PXGraph) this.Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryID)?.PostClassID);
      int num;
      if (inPostClass == null)
      {
        num = 0;
      }
      else
      {
        nullable = inPostClass.COGSSubFromSales;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num == 0)
        return;
      sender.SetDefaultExt<ARTran.expenseSubID>(e.Row);
    }

    protected virtual void ARTran_ExpenseAccrualAccountID_FieldDefaulting(
      PXCache sender,
      PXFieldDefaultingEventArgs e)
    {
      ARTran row = (ARTran) e.Row;
      if (row == null)
        return;
      bool? nullable = row.IsStockItem;
      if (nullable.GetValueOrDefault())
        return;
      nullable = row.AccrueCost;
      if (!nullable.GetValueOrDefault())
        return;
      this.SetExpenseAccountSub(sender, e, row.InventoryID, row.SiteID, (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubUsingPostingClassDelegate) ((item, site, postClass) => (object) INReleaseProcess.GetAcctID<INPostClass.invtAcctID>((PXGraph) this.Base, postClass.InvtAcctDefault, item, site, postClass)), (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubFromItemDelegate) (item => (object) item.InvtAcctID));
    }

    protected virtual void ARTran_ExpenseAccrualSubID_FieldDefaulting(
      PXCache sender,
      PXFieldDefaultingEventArgs e)
    {
      ARTran row = (ARTran) e.Row;
      if (row == null)
        return;
      bool? nullable = row.IsStockItem;
      if (nullable.GetValueOrDefault())
        return;
      nullable = row.AccrueCost;
      if (!nullable.GetValueOrDefault() || !row.ExpenseAccrualAccountID.HasValue)
        return;
      this.SetExpenseAccountSub(sender, e, row.InventoryID, row.SiteID, (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubUsingPostingClassDelegate) ((item, site, postClass) => (object) INReleaseProcess.GetSubID<INPostClass.invtSubID>((PXGraph) this.Base, postClass.InvtAcctDefault, postClass.InvtSubMask, item, site, postClass)), (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubFromItemDelegate) (item => (object) item.InvtSubID));
    }

    protected virtual void ARTran_ExpenseAccountID_FieldDefaulting(
      PXCache sender,
      PXFieldDefaultingEventArgs e)
    {
      ARTran tran = (ARTran) e.Row;
      if (tran == null)
        return;
      bool? nullable = tran.IsStockItem;
      if (nullable.GetValueOrDefault())
        return;
      nullable = tran.AccrueCost;
      if (!nullable.GetValueOrDefault())
        return;
      this.SetExpenseAccountSub(sender, e, tran.InventoryID, tran.SiteID, (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubUsingPostingClassDelegate) ((item, site, postClass) =>
      {
        PMProject project;
        PMTask task;
        PMProjectHelper.TryToGetProjectAndTask((PXGraph) this.Base, (int?) tran?.ProjectID, (int?) tran?.TaskID, out project, out task);
        return (object) INReleaseProcess.GetAcctID<INPostClass.cOGSAcctID>((PXGraph) this.Base, postClass.COGSAcctDefault, InventoryAccountServiceHelper.Params(item, site, postClass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
      }), (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubFromItemDelegate) (item => (object) item.COGSAcctID));
    }

    protected virtual void ARTran_ExpenseSubID_FieldDefaulting(
      PXCache sender,
      PXFieldDefaultingEventArgs e)
    {
      ARTran tran = (ARTran) e.Row;
      if (tran == null)
        return;
      bool? nullable1 = tran.IsStockItem;
      if (nullable1.GetValueOrDefault())
        return;
      nullable1 = tran.AccrueCost;
      if (!nullable1.GetValueOrDefault() || !tran.ExpenseAccountID.HasValue)
        return;
      this.SetExpenseAccountSub(sender, e, tran.InventoryID, tran.SiteID, (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubUsingPostingClassDelegate) ((item, site, postClass) =>
      {
        object valuePending = sender.GetValuePending((object) tran, typeof (ARTran.subID).Name);
        int? nullable2;
        if (postClass != null && postClass.COGSSubFromSales.GetValueOrDefault())
        {
          nullable2 = tran.SubID;
          if (nullable2.HasValue || valuePending != null)
            return (object) tran.SubID;
        }
        ARInvoiceEntry graph = this.Base;
        ARTran arTran1 = tran;
        int? projectID;
        if (arTran1 == null)
        {
          nullable2 = new int?();
          projectID = nullable2;
        }
        else
          projectID = arTran1.ProjectID;
        ARTran arTran2 = tran;
        int? taskID;
        if (arTran2 == null)
        {
          nullable2 = new int?();
          taskID = nullable2;
        }
        else
          taskID = arTran2.TaskID;
        PMProject project;
        ref PMProject local1 = ref project;
        PMTask task;
        ref PMTask local2 = ref task;
        PMProjectHelper.TryToGetProjectAndTask((PXGraph) graph, projectID, taskID, out local1, out local2);
        return (object) INReleaseProcess.GetSubID<INPostClass.cOGSSubID>((PXGraph) this.Base, postClass.COGSAcctDefault, postClass.COGSSubMask, InventoryAccountServiceHelper.Params(item, site, postClass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
      }), (NonStockAccrualGraph<ARInvoiceEntry, ARInvoice>.GetAccountSubFromItemDelegate) (item => (object) item.COGSSubID));
    }
  }

  public class RelatedIntercompanyAPDocumentExtension : PXGraphExtension<ARInvoiceEntry>
  {
    public FbqlSelect<SelectFromBase<PX.Objects.AP.APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
    #nullable enable
    PX.Objects.AP.APInvoice.intercompanyInvoiceNoteID, IBqlGuid>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    ARInvoice.noteID, IBqlGuid>.FromCurrent>>, 
    #nullable disable
    PX.Objects.AP.APInvoice>.View RelatedIntercompanyAPDocument;
    public PXAction<ARInvoice> viewRelatedAPDocument;

    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.interBranch>();

    [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Related AP Document")]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APInvoice.documentKey> e)
    {
    }

    protected void _(PX.Data.Events.RowSelected<ARInvoice> e)
    {
      PXCache<PX.Objects.AP.APInvoice> pxCache = GraphHelper.Caches<PX.Objects.AP.APInvoice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARInvoice>>) e).Cache.Graph);
      PX.Objects.AP.APInvoice current1 = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.RelatedIntercompanyAPDocument).Current;
      Customer current2 = ((PXSelectBase<Customer>) this.Base.customer).Current;
      int num = current2 != null ? (current2.IsBranch.GetValueOrDefault() ? 1 : 0) : 0;
      PXUIFieldAttribute.SetVisible<PX.Objects.AP.APInvoice.documentKey>((PXCache) pxCache, (object) current1, num != 0);
    }

    public virtual IEnumerable relatedIntercompanyAPDocument(PXAdapter adapter)
    {
      using (new PXReadBranchRestrictedScope())
        return (IEnumerable) new List<PX.Objects.AP.APInvoice>()
        {
          ((PXSelectBase<PX.Objects.AP.APInvoice>) new PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.intercompanyInvoiceNoteID, Equal<Current<ARInvoice.noteID>>>>((PXGraph) this.Base)).SelectSingle(Array.Empty<object>())
        };
    }

    [PXLookupButton(DisplayOnMainToolbar = false)]
    [PXUIField]
    public IEnumerable ViewRelatedAPDocument(PXAdapter adapter)
    {
      PX.Objects.AP.APInvoice current = ((PXSelectBase<PX.Objects.AP.APInvoice>) this.RelatedIntercompanyAPDocument).Current;
      if (current != null)
        new EntityHelper((PXGraph) this.Base).NavigateToRow(current.NoteID, (PXRedirectHelper.WindowMode) 3);
      return adapter.Get();
    }
  }

  public delegate void ARInvoiceCreatedDelegate(ARInvoice invoice, ARRegister doc);

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
  public class ARInvoiceEntryAddressLookupExtension : 
    AddressLookupExtension<ARInvoiceEntry, ARInvoice, ARAddress>
  {
    protected override string AddressView => "Billing_Address";
  }

  /// <exclude />
  public class ARInvoiceEntryShippingAddressLookupExtension : 
    AddressLookupExtension<ARInvoiceEntry, ARInvoice, ARShippingAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }

  public class ARInvoiceEntryAddressCachingHelper : 
    AddressValidationExtension<ARInvoiceEntry, ARAddress>
  {
    protected override IEnumerable<PXSelectBase<ARAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ARInvoiceEntry.ARInvoiceEntryAddressCachingHelper addressCachingHelper = this;
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

  public class ARInvoiceEntryShippingAddressCachingHelper : 
    AddressValidationExtension<ARInvoiceEntry, ARShippingAddress>
  {
    protected override IEnumerable<PXSelectBase<ARShippingAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ARInvoiceEntry.ARInvoiceEntryShippingAddressCachingHelper addressCachingHelper = this;
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
