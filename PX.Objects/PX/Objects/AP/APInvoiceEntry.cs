// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.CloudServices.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP.BQL;
using PX.Objects.AP.Overrides.APDocumentRelease;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.CM;
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
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.DR.Descriptor;
using PX.Objects.EP;
using PX.Objects.Extensions.CostAccrual;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.PO.LandedCosts;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class APInvoiceEntry : 
  APDataEntryGraph<
  #nullable disable
  APInvoiceEntry, APInvoice>,
  PXImportAttribute.IPXPrepareItems,
  IGraphWithInitialization,
  IPXReusableGraph
{
  public bool IsReverseContext;
  internal bool IsPPDCreateContext;
  internal bool IsRecognitionProcess;
  public PXAction<APInvoice> printAPEdit;
  public PXAction<APInvoice> printAPRegister;
  public PXAction<APInvoice> viewSchedule;
  public PXAction<APInvoice> newVendor;
  public PXAction<APInvoice> editVendor;
  public PXAction<APInvoice> vendorDocuments;
  public PXAction<APInvoice> prebook;
  public PXAction<APInvoice> voidInvoice;
  public static readonly Dictionary<string, string> APDocTypeDict = new APDocType.ListAttribute().ValueLabelDic;
  public PXAction<APInvoice> vendorRefund;
  public PXAction<APInvoice> reverseInvoice;
  public PXAction<APInvoice> reclassifyBatch;
  public PXAction<APInvoice> payInvoice;
  public PXAction<APInvoice> createSchedule;
  public PXAction<APInvoice> viewScheduleOfCurrentDocument;
  public PXAction<APInvoice> viewPODocument;
  public PXAction<APInvoice> ViewOriginalDocument;
  public PXAction<APInvoice> autoApply;
  public PXAction<APInvoice> voidDocument;
  public PXAction<APInvoice> viewPayment;
  public PXAction<APInvoice> viewInvoice;
  public PXAction<APInvoice> viewItem;
  public PXAction<APInvoice> recalculateDiscountsAction;
  public PXAction<APInvoice> recalcOk;
  public PXWorkflowEventHandler<APInvoice, APRegister> OnConfirmSchedule;
  public PXWorkflowEventHandler<APInvoice, APRegister> OnVoidSchedule;
  public PXWorkflowEventHandler<APInvoice> OnOpenDocument;
  public PXWorkflowEventHandler<APInvoice> OnCloseDocument;
  public PXWorkflowEventHandler<APInvoice> OnReleaseDocument;
  public PXWorkflowEventHandler<APInvoice> OnVoidDocument;
  public PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>> nonStockItem;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APInvoice.invoiceNbr)}, FieldsToShowInSimpleImport = new System.Type[] {typeof (APInvoice.invoiceNbr)})]
  [PXViewName("AP document")]
  public PXSelectJoin<APInvoice, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>>, Where<APInvoice.docType, Equal<Optional<APInvoice.docType>>, And2<Where<APRegister.origModule, NotEqual<BatchModule.moduleTX>, Or<APInvoice.released, Equal<True>>>, PX.Data.And<Where<Vendor.bAccountID, PX.Data.IsNull, PX.Data.Or<Match<Vendor, Current<AccessInfo.userName>>>>>>>> Document;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APInvoice.paySel), typeof (APInvoice.payDate), typeof (APInvoice.intercompanyInvoiceNoteID)})]
  public PXSelect<APInvoice, Where<APInvoice.docType, Equal<Current<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Current<APInvoice.refNbr>>>>> CurrentDocument;
  [PXCopyPasteHiddenView]
  public PXSelect<APTran, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>>>> AllTransactions;
  [PXImport(typeof (APInvoice))]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APTran.pOOrderType), typeof (APTran.pONbr), typeof (APTran.pOLineNbr), typeof (APTran.receiptNbr), typeof (APTran.receiptLineNbr), typeof (APTran.pPVDocType), typeof (APTran.pPVRefNbr), typeof (APTran.defScheduleID)})]
  [PXViewName("AP Transactions")]
  public PXSelect<APTran, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<APTran.lineType, NotEqual<SOLineType.discount>>>>, PX.Data.OrderBy<Asc<APTran.tranType, Asc<APTran.refNbr, Asc<APTran.lineNbr>>>>> Transactions;
  public PXSelectJoin<APTran, LeftJoin<PX.Objects.PO.POLine, On<PX.Objects.PO.POLine.orderType, Equal<APTran.pOOrderType>, And<PX.Objects.PO.POLine.orderNbr, Equal<APTran.pONbr>, And<PX.Objects.PO.POLine.lineNbr, Equal<APTran.pOLineNbr>>>>>, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>>>, PX.Data.OrderBy<Asc<APTran.tranType, Asc<APTran.refNbr, Asc<APTran.lineNbr>>>>> TransactionsPOLine;
  public PXSelect<APTran, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<APTran.lineType, Equal<SOLineType.discount>>>>, PX.Data.OrderBy<Asc<APTran.tranType, Asc<APTran.refNbr, Asc<APTran.lineNbr>>>>> Discount_Row;
  [PXCopyPasteHiddenView]
  public PXSelect<APTax, Where<APTax.tranType, Equal<Current<APInvoice.docType>>, And<APTax.refNbr, Equal<Current<APInvoice.refNbr>>>>, PX.Data.OrderBy<Asc<APTax.tranType, Asc<APTax.refNbr, Asc<APTax.taxID>>>>> Tax_Rows;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APInvoice.docType>>, And<APTaxTran.refNbr, Equal<Current<APInvoice.refNbr>>>>>> Taxes;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APInvoice.docType>>, And<APTaxTran.refNbr, Equal<Current<APInvoice.refNbr>>>>>> TaxesList;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly2<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APInvoice.docType>>, And<APTaxTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>>>> UseTaxes;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<APInvoiceEntry.APAdjust, InnerJoin<APPayment, On<APPayment.docType, Equal<APInvoiceEntry.APAdjust.adjgDocType>, And<APPayment.refNbr, Equal<APInvoiceEntry.APAdjust.adjgRefNbr>>>>> Adjustments;
  public PXSelectJoin<APInvoiceEntry.APAdjust, InnerJoinSingleTable<APPayment, On<APPayment.docType, Equal<APInvoiceEntry.APAdjust.adjgDocType>, And<APPayment.refNbr, Equal<APInvoiceEntry.APAdjust.adjgRefNbr>>>, InnerJoin<APRegisterAlias, On<APRegisterAlias.docType, Equal<APInvoiceEntry.APAdjust.adjgDocType>, And<APRegisterAlias.refNbr, Equal<APInvoiceEntry.APAdjust.adjgRefNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APRegisterAlias.curyInfoID>>>>>, Where<APInvoiceEntry.APAdjust.invoiceID, Equal<Current<APInvoice.noteID>>>> Adjustments_Raw;
  public PXSelect<APInvoiceEntry.APAdjust2, PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<
  #nullable enable
  APInvoice.docType>, 
  #nullable disable
  Equal<APDocType.debitAdj>>>>>.And<BqlOperand<
  #nullable enable
  APInvoiceEntry.APAdjust2.memoID, IBqlGuid>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APInvoice.noteID, IBqlGuid>.FromCurrent>>>> Adjustments_1;
  public 
  #nullable disable
  PXSelectJoin<APPayment, InnerJoin<APRegisterAlias, On<APRegisterAlias.docType, Equal<APPayment.docType>, And<APRegisterAlias.refNbr, Equal<APPayment.refNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APRegisterAlias.curyInfoID>>>>, Where<APRegisterAlias.vendorID, Equal<Current<APInvoice.vendorID>>, And<APRegisterAlias.docDate, LessEqual<Current<APInvoice.docDate>>, And<APRegisterAlias.tranPeriodID, LessEqual<Current<APRegister.tranPeriodID>>, And<APRegisterAlias.released, Equal<True>, And<APRegisterAlias.openDoc, Equal<True>, And<APRegisterAlias.hold, NotEqual<True>, And<APPayment.refNbr, PX.Data.In<Required<APPayment.refNbr>>>>>>>>>> AvailablePayments;
  public PXSelect<APInvoiceDiscountDetail, Where<APInvoiceDiscountDetail.docType, Equal<Current<APInvoice.docType>>, And<APInvoiceDiscountDetail.refNbr, Equal<Current<APInvoice.refNbr>>>>, PX.Data.OrderBy<Asc<APInvoiceDiscountDetail.orderType, Asc<APInvoiceDiscountDetail.orderNbr, Asc<APInvoiceDiscountDetail.receiptType, Asc<APInvoiceDiscountDetail.receiptNbr, Asc<APInvoiceDiscountDetail.lineNbr>>>>>>> DiscountDetails;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APInvoice.curyInfoID>>>> currencyinfo;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  [PXViewName("Vendor")]
  public PXSetup<Vendor>.Where<BqlOperand<
  #nullable enable
  Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APInvoice.vendorID, IBqlInt>.AsOptional>> vendor;
  public 
  #nullable disable
  PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  APInvoice.branchID, IBqlInt>.AsOptional>> branch;
  public 
  #nullable disable
  PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<APInvoice.vendorID>>>> EmployeeByVendor;
  [PXViewName("Employee")]
  public PXSetup<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<APRegister.employeeID>>>> employee;
  public PXSetup<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>> vendorclass;
  public PXSetup<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<APInvoice.taxZoneID>>>> taxzone;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APInvoice.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<APInvoice.vendorLocationID>>>>> location;
  public PXSetup<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<APInvoice.finPeriodID>>, PX.Data.And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<APInvoice.branchID>>>>> finperiod;
  [PXCopyPasteHiddenView]
  public PXFilter<RecalcDiscountsParamFilter> recalcdiscountsfilter;
  public PXSelect<AP1099Hist> ap1099hist;
  public PXSelect<AP1099Yr> ap1099year;
  public PXSetup<GLSetup> glsetup;
  public PXSetupOptional<INSetup> insetup;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSetup<POSetup> posetup;
  public PXSelect<DRSchedule> dummySchedule_forPXParent;
  public PXSelect<DRScheduleDetail> dummyScheduleDetail_forPXParent;
  public PXSelect<DRScheduleTran> dummyScheduleTran_forPXParent;
  public PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Current<APInvoice.origRefNbr>>, And<Current<APRegister.origModule>, Equal<BatchModule.moduleEP>>>> expenseclaim;
  [PXCopyPasteHiddenView]
  public PXFilter<DuplicateFilter> duplicatefilter;
  public PXSelect<APTran, Where<APTran.refNbr, Equal<Optional<APTran.refNbr>>, And<APTran.tranType, Equal<Optional<APTran.tranType>>>>> siblingTrans;
  public PXSelect<APSetupApproval, Where<APSetupApproval.docType, Equal<Current<APInvoice.docType>>, PX.Data.Or<Where<Current<APInvoice.docType>, Equal<APDocType.prepayment>, And<APSetupApproval.docType, Equal<APDocType.prepaymentRequest>>>>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomationWithoutHoldDefaulting<APInvoice, APRegister.approved, APRegister.rejected, APInvoice.hold, APSetupApproval> Approval;
  [PXViewName("Purchase Order")]
  public FbqlSelect<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APTran>.On<APTran.FK.POOrder>>>.Where<KeysRelation<CompositeKey<PX.Data.ReferentialIntegrity.Attributes.Field<APTran.tranType>.IsRelatedTo<APInvoice.docType>, PX.Data.ReferentialIntegrity.Attributes.Field<APTran.refNbr>.IsRelatedTo<APInvoice.refNbr>>.WithTablesOf<APInvoice, APTran>, APInvoice, APTran>.SameAsCurrent>, PX.Objects.PO.POOrder>.View POOrderForApproval;
  [PXReadOnlyView]
  [PXCopyPasteHiddenView]
  public PXSelect<APRetainageInvoice, Where<True, Equal<False>>> RetainageDocuments;
  protected bool IsVendorIDUpdated;
  private Dictionary<System.Type, CachePermission> cachePermission;
  protected bool changedSuppliedByVendorLocation;
  private bool _allowToVoidReleased;
  private static readonly Dictionary<string, string> DocTypes = new APInvoiceType.TaxInvoiceListAttribute().ValueLabelDic;

  private DiscountEngine<APTran, APInvoiceDiscountDetail> _discountEngine
  {
    get => DiscountEngineProvider.GetEngineFor<APTran, APInvoiceDiscountDetail>();
  }

  [PXDefault(typeof (Search<INPostClass.cOGSSubID, Where<INPostClass.postClassID, Equal<Current<PX.Objects.IN.InventoryItem.postClassID>>>>))]
  [SubAccount(typeof (PX.Objects.IN.InventoryItem.cOGSAcctID), DisplayName = "Expense Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual void InventoryItem_COGSSubID_CacheAttached(PXCache sender)
  {
  }

  [Branch(typeof (APRegister.branchID), null, true, true, true, Enabled = false)]
  protected virtual void APTaxTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PopupMessage]
  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [VendorActiveOrHoldPayments(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true)]
  [PXDefault]
  protected virtual void APInvoice_VendorID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Original Document")]
  protected virtual void APInvoice_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXFormula(typeof (IIf<Where<Current<APInvoice.docType>, NotEqual<APDocType.prepayment>, And2<Where<Current<APInvoice.docType>, NotEqual<APDocType.debitAdj>>, PX.Data.Or<Where<Current<PX.Objects.AP.APSetup.termsInDebitAdjustments>, Equal<True>>>>>, Selector<APInvoice.vendorID, Vendor.termsID>, Null>))]
  protected virtual void APInvoice_TermsID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDefault(typeof (Where2<FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentsByLines>, And<APInvoice.docType, NotEqual<APDocType.prepayment>, And<APRegister.origModule, NotEqual<BatchModule.moduleTX>, And<APRegister.origModule, NotEqual<BatchModule.moduleEP>, And<APInvoice.isTaxDocument, NotEqual<True>, And<APInvoice.isMigratedRecord, NotEqual<True>, And<APInvoice.pendingPPD, NotEqual<True>, And<APInvoice.docType, NotEqual<APDocType.prepaymentInvoice>, And<Current<Vendor.paymentsByLinesAllowed>, Equal<True>>>>>>>>>>))]
  protected virtual void APInvoice_PaymentsByLinesAllowed_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDefault(typeof (Coalesce<Search<CREmployee.defContactID, Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, And<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>>>, Search2<PX.Objects.CR.BAccount.ownerID, InnerJoin<CREmployee, On<CREmployee.defContactID, Equal<PX.Objects.CR.BAccount.ownerID>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<APRegister.vendorID>>, And<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void APInvoice_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Retained Amount", FieldClass = "Retainage")]
  protected virtual void APInvoice_CuryLineRetainageTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Document Discounts", Enabled = true)]
  protected virtual void APInvoice_CuryDiscTot_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (APInvoice.docDate), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (APInvoice.vendorID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (APRegister.employeeID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (APInvoice.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APInvoice.curyInfoID))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (APInvoice.curyOrigDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (APInvoice.origDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_SourceItemType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) APInvoiceEntry.APDocTypeDict[this.Document.Current.DocType];
    e.Cancel = true;
  }

  protected static DRSchedule GetDeferralSchedule(PXGraph graph, APTran transaction)
  {
    return (DRSchedule) PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAP>, And<DRSchedule.docType, Equal<Required<APTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<APTran.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<APTran.lineNbr>>>>>>>.Config>.Select(graph, (object) transaction.TranType, (object) transaction.RefNbr, (object) transaction.LineNbr);
  }

  public static void ViewScheduleForLine(PXGraph graph, APInvoice document, APTran tran)
  {
    DRSchedule deferralSchedule = APInvoiceEntry.GetDeferralSchedule(graph, tran);
    if (deferralSchedule == null || deferralSchedule.IsDraft.GetValueOrDefault())
    {
      ARReleaseProcess.Amount expensePostingAmount = APReleaseProcess.GetExpensePostingAmount(graph, tran);
      DRDeferredCode defCode = (DRDeferredCode) PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Current2<APTran.deferredCode>>>>.Config>.Select(graph);
      if (defCode != null)
      {
        DRProcess instance = PXGraph.CreateInstance<DRProcess>();
        instance.CreateSchedule(tran, defCode, document, expensePostingAmount.Base.Value, true);
        instance.Actions.PressSave();
        graph.Caches<DRSchedule>().Clear();
        graph.Caches<DRSchedule>().ClearQueryCache();
        deferralSchedule = APInvoiceEntry.GetDeferralSchedule(graph, tran);
      }
    }
    if (deferralSchedule != null)
    {
      DraftScheduleMaint instance = PXGraph.CreateInstance<DraftScheduleMaint>();
      instance.Clear();
      instance.Schedule.Current = deferralSchedule;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewSchedule");
      requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
      throw requiredException;
    }
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

  [PXUIField(DisplayName = "View Deferrals", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(ImageKey = "Settings")]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    APTran current = this.Transactions.Current;
    if (current != null && this.Transactions.Cache.GetStatus((object) current) == PXEntryStatus.Notchanged)
    {
      this.Save.Press();
      APInvoiceEntry.ViewScheduleForLine((PXGraph) this, this.Document.Current, this.Transactions.Current);
    }
    return adapter.Get();
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
    foreach (APInvoice doc in adapter.Get<APInvoice>())
    {
      this.OnBeforeRelease((APRegister) doc);
      if (!doc.Hold.Value && !doc.Released.Value)
      {
        cache.Update((object) doc);
        list.Add((APRegister) doc);
      }
    }
    if (list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    this.Save.Press();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => PX.Objects.AP.APDocumentRelease.ReleaseDoc(list, false)));
    return (IEnumerable) list;
  }

  public virtual APRegister OnBeforeRelease(APRegister doc) => doc;

  [PXUIField(DisplayName = "Pre-release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable Prebook(PXAdapter adapter)
  {
    PXCache cache = this.Document.Cache;
    List<APRegister> list = new List<APRegister>();
    foreach (APInvoice row in adapter.Get<APInvoice>())
    {
      if (!row.Hold.Value && !row.Released.Value && !row.Prebooked.GetValueOrDefault())
      {
        if (!row.PrebookAcctID.HasValue)
          cache.RaiseExceptionHandling<APInvoice.prebookAcctID>((object) row, (object) row.PrebookAcctID, (Exception) new PXSetPropertyException("To release the document, specify the reclassification account."));
        else if (!row.PrebookSubID.HasValue)
        {
          cache.RaiseExceptionHandling<APInvoice.prebookSubID>((object) row, (object) row.PrebookSubID, (Exception) new PXSetPropertyException("To release the document, specify the reclassification account."));
        }
        else
        {
          cache.Update((object) row);
          list.Add((APRegister) row);
        }
      }
    }
    if (list.Count == 0)
      throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
      {
        (object) "Updating",
        (object) this.Document.View.CacheGetItemType().Name
      });
    this.Save.Press();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => PX.Objects.AP.APDocumentRelease.ReleaseDoc(list, false, true)));
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable VoidInvoice(PXAdapter adapter)
  {
    PXCache cache = this.Document.Cache;
    List<APRegister> list = new List<APRegister>();
    foreach (APInvoice apInvoice in adapter.Get<APInvoice>())
    {
      if (apInvoice.Released.GetValueOrDefault() || apInvoice.Prebooked.GetValueOrDefault())
      {
        cache.Update((object) apInvoice);
        list.Add((APRegister) apInvoice);
      }
    }
    if (list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    this.Persist();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => PX.Objects.AP.APDocumentRelease.VoidDoc(list)));
    return (IEnumerable) list;
  }

  public virtual void ClearRetainageSummary(APInvoice document)
  {
    document.CuryLineRetainageTotal = new Decimal?(0M);
    document.CuryRetainageTotal = new Decimal?(0M);
    document.CuryRetainageUnreleasedAmt = new Decimal?(0M);
    document.CuryRetainedTaxTotal = new Decimal?(0M);
    document.CuryRetainedDiscTotal = new Decimal?(0M);
  }

  [PXUIField(DisplayName = "Refund", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable VendorRefund(PXAdapter adapter)
  {
    if (this.Document.Current != null)
    {
      bool? nullable = this.Document.Current.Released;
      if (nullable.Value && this.Document.Current.DocType == "ADR")
      {
        nullable = this.Document.Current.OpenDoc;
        if (nullable.Value)
        {
          APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
          APInvoiceEntry.APAdjust apAdjust = (APInvoiceEntry.APAdjust) PXSelectBase<APInvoiceEntry.APAdjust, PXSelect<APInvoiceEntry.APAdjust, Where<APInvoiceEntry.APAdjust.adjdDocType, Equal<Current<APInvoice.docType>>, And<APInvoiceEntry.APAdjust.adjdRefNbr, Equal<Current<APInvoice.refNbr>>, And<APInvoiceEntry.APAdjust.released, Equal<False>>>>>.Config>.Select((PXGraph) this);
          if (apAdjust != null)
          {
            instance.Document.Current = (APPayment) instance.Document.Search<APPayment.refNbr>((object) apAdjust.AdjgRefNbr, (object) apAdjust.AdjgDocType);
          }
          else
          {
            instance.Clear();
            instance.CreatePayment(this.Document.Current, "REF");
          }
          throw new PXRedirectRequiredException((PXGraph) instance, "PayInvoice");
        }
      }
    }
    return adapter.Get();
  }

  /// <summary>Check if reversing retainage document already exists.</summary>
  /// <param name="origDoc">The original document.</param>
  /// <param name="errorMessage">Displayed message if reversing document already exists.</param>
  public virtual bool CheckReversingRetainageDocumentAlreadyExists(
    APInvoice origDoc,
    out APRegister reversingDoc)
  {
    reversingDoc = (APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.docType, Equal<Required<APRegister.docType>>, And<APRegister.origDocType, Equal<Required<APRegister.origDocType>>, And<APRegister.origRefNbr, Equal<Required<APRegister.origRefNbr>>>>>, PX.Data.OrderBy<Desc<APRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) this.GetReversingDocType(origDoc.DocType), (object) origDoc.DocType, (object) origDoc.RefNbr);
    if (reversingDoc == null)
      return false;
    return reversingDoc.IsOriginalRetainageDocument() == origDoc.IsOriginalRetainageDocument() || reversingDoc.IsChildRetainageDocument() == origDoc.IsChildRetainageDocument();
  }

  /// <summary>
  /// Ask user for approval for creation of another reversal if reversing document already exists.
  /// </summary>
  /// <param name="origDoc">The original document.</param>
  /// <returns>True if user approves, false if not.</returns>
  protected virtual bool AskUserApprovalIfReversingDocumentAlreadyExists(APInvoice origDoc)
  {
    APRegister apRegister = (APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.docType, Equal<Required<APRegister.docType>>, And<APRegister.origDocType, Equal<Required<APRegister.origDocType>>, And<APRegister.origRefNbr, Equal<Required<APRegister.origRefNbr>>>>>, PX.Data.OrderBy<Desc<APRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) this.GetReversingDocType(origDoc.DocType), (object) origDoc.DocType, (object) origDoc.RefNbr);
    if (apRegister == null)
      return true;
    return this.Document.View.Ask(PXMessages.LocalizeFormatNoPrefix("A reversing {0} document with the {1} ref. number already exists. Do you want to continue?", (object) APDocType.GetDisplayName(apRegister.DocType), (object) apRegister.RefNbr), MessageButtons.YesNo) == WebDialogResult.Yes;
  }

  /// <summary>
  /// Ask user for approval to proceede if there is unreleased Debit Adjustment application to the document.
  /// </summary>
  /// <param name="doc">The current document.</param>
  /// <param name="message">Text of the confirmation request. Must contain a placeholder for the reference number of the found document.</param>
  /// <returns>
  /// True if the application exists and user approves, false otherwise.
  /// </returns>
  protected virtual bool AskUserApprovalIfUnreleasedDebitAdjApplicationAlreadyExists(
    APInvoice doc,
    string message,
    out APInvoiceEntry.APAdjust application)
  {
    application = (APInvoiceEntry.APAdjust) PXSelectBase<APInvoiceEntry.APAdjust, PXViewOf<APInvoiceEntry.APAdjust>.BasedOn<SelectFromBase<APInvoiceEntry.APAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoiceEntry.APAdjust.adjgDocType, Equal<APDocType.debitAdj>>>>, PX.Data.And<BqlOperand<APInvoiceEntry.APAdjust.adjdDocType, IBqlString>.IsEqual<P.AsString.ASCII>>>, PX.Data.And<BqlOperand<APInvoiceEntry.APAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APInvoiceEntry.APAdjust.released, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr);
    if (application == null)
      return false;
    return this.Document.View.Ask(PXMessages.LocalizeFormatNoPrefix(message, (object) application.AdjgRefNbr), MessageButtons.YesNo) == WebDialogResult.Yes;
  }

  [PXUIField(DisplayName = "Reverse", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  [PXActionRestriction(typeof (Where<Current<APRegister.isRetainageReversing>, Equal<True>, PX.Data.And<Where<Current<APInvoice.isRetainageDocument>, Equal<True>, Or<Current<APInvoice.retainageApply>, Equal<True>>>>>), "The Reverse action cannot be used for credit and debit adjustments that reverse original documents with retainage and retainage documents.")]
  public virtual IEnumerable ReverseInvoice(PXAdapter adapter)
  {
    APInvoice current1 = this.Document.Current;
    string docType = current1?.DocType;
    bool flag1 = docType != "INV" && docType != "PPI" && docType != "ACR" && docType != "ADR";
    if (current1 == null | flag1)
      return adapter.Get();
    if (current1.InstallmentNbr.HasValue && !string.IsNullOrEmpty(current1.MasterRefNbr))
      throw new PXSetPropertyException("Multiple installments bill cannot be reversed, Please reverse original bill '{0}'.", new object[1]
      {
        (object) current1.MasterRefNbr
      });
    if (current1.IsOriginalRetainageDocument() || current1.IsChildRetainageDocument())
    {
      APRetainageInvoice retainageInvoice = this.RetainageDocuments.Select().RowCast<APRetainageInvoice>().FirstOrDefault<APRetainageInvoice>((Func<APRetainageInvoice, bool>) (row => !row.Released.GetValueOrDefault()));
      if (retainageInvoice != null)
        throw new PXException("The document cannot be reversed because there is retainage {0} {1} associated with this {2} that is not released yet.", new object[3]
        {
          (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[retainageInvoice.DocType]),
          (object) retainageInvoice.RefNbr,
          (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[current1.DocType])
        });
      bool flag2 = (APInvoiceEntry.APAdjust) PXSelectBase<APInvoiceEntry.APAdjust, PXSelect<APInvoiceEntry.APAdjust, Where<APInvoiceEntry.APAdjust.adjdDocType, Equal<Current<APInvoice.docType>>, And<APInvoiceEntry.APAdjust.adjdRefNbr, Equal<Current<APInvoice.refNbr>>, And<APInvoiceEntry.APAdjust.voided, Equal<False>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null) != null;
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
          (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[current1.DocType]),
          (object) current1.RefNbr
        });
      APRegister reversingDoc;
      if (this.CheckReversingRetainageDocumentAlreadyExists(current1, out reversingDoc))
        throw new PXException("The {0} document with the {1} ref. number cannot be reversed because it has already been reversed with the {2} document with the {3} ref. number.", new object[4]
        {
          (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[current1.DocType]),
          (object) current1.RefNbr,
          (object) PXMessages.LocalizeNoPrefix(APInvoiceEntry.APDocTypeDict[reversingDoc.DocType]),
          (object) reversingDoc.RefNbr
        });
    }
    else if (this.Document.Current?.DocType == "PPI")
    {
      APInvoiceEntry.APAdjust application = (APInvoiceEntry.APAdjust) null;
      if (this.AskUserApprovalIfUnreleasedDebitAdjApplicationAlreadyExists(this.Document.Current, "The prepayment invoice has already been applied to the {0} debit adjustment, but the debit adjustment has not been released. Do you want to open the debit adjustment?", out application))
      {
        this.Document.Current = (APInvoice) this.Document.Search<APInvoice.refNbr>((object) application.AdjgRefNbr, (object) "ADR");
        PXRedirectHelper.TryRedirect((PXGraph) this, PXRedirectHelper.WindowMode.Same);
      }
      else if (application != null)
        return adapter.Get();
      APInvoiceEntry.APAdjust2 apAdjust2 = (APInvoiceEntry.APAdjust2) PXSelectBase<APInvoiceEntry.APAdjust2, PXViewOf<APInvoiceEntry.APAdjust2>.BasedOn<SelectFromBase<APInvoiceEntry.APAdjust2, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoiceEntry.APAdjust2.adjdDocType, Equal<APDocType.prepaymentInvoice>>>>, PX.Data.And<BqlOperand<APInvoiceEntry.APAdjust2.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Data.And<BqlOperand<APInvoiceEntry.APAdjust2.released, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<APInvoiceEntry.APAdjust2.voided, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this, (object) this.Document.Current.RefNbr);
      if (apAdjust2 != null)
        throw new PXSetPropertyException("The prepayment invoice has an unreleased application to the {0} document with the {1} type and cannot be reversed.", new object[2]
        {
          (object) apAdjust2.AdjgRefNbr,
          (object) apAdjust2.AdjgDocType
        });
      APInvoice copy = PXCache<APInvoice>.CreateCopy(this.Document.Current);
      this._finPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<APInvoice.finPeriodID, APInvoice.branchID>(this.Document.Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aPClosed));
      try
      {
        this.IsReverseContext = true;
        this.ReverseInvoiceProc((APRegister) copy);
        APInvoice current2 = this.Document.Current;
        string str;
        using (new PXLocaleScope(this.vendor.Current.LocaleName))
        {
          Decimal? curyDocBal = copy.CuryDocBal;
          Decimal? curyOrigDocAmt = copy.CuryOrigDocAmt;
          str = PXMessages.LocalizeFormatNoPrefix(curyDocBal.GetValueOrDefault() < curyOrigDocAmt.GetValueOrDefault() & curyDocBal.HasValue & curyOrigDocAmt.HasValue ? "Write-off of prepayment invoice {0}" : "Voiding of prepayment invoice {0}", (object) copy.RefNbr);
        }
        current2.DocDesc = str;
        foreach (PXResult<APTran> pxResult in this.Transactions.Select())
          ((APTran) pxResult).TranDesc = str;
        this.ApplyOriginalDocAdjustmentToDebitMemo(copy, current2);
        this.Document.Cache.RaiseExceptionHandling<APInvoice.finPeriodID>((object) this.Document.Current, (object) this.Document.Current.FinPeriodID, (Exception) null);
        return (IEnumerable) new List<APInvoice>()
        {
          this.Document.Current
        };
      }
      catch (PXException ex)
      {
        this.Clear(PXClearOption.PreserveTimeStamp);
        this.Document.Current = copy;
        throw;
      }
      finally
      {
        this.IsReverseContext = false;
      }
    }
    else if (!this.AskUserApprovalIfReversingDocumentAlreadyExists(current1))
      return adapter.Get();
    this.Save.Press();
    bool flag3 = false;
    foreach (PXResult<APTran> pxResult in this.Transactions.Select())
    {
      APTran apTran = (APTran) pxResult;
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, apTran.InventoryID);
      if (inventoryItem != null)
      {
        bool? nullable = inventoryItem.IsConverted;
        if (nullable.GetValueOrDefault())
        {
          nullable = apTran.IsStockItem;
          if (nullable.HasValue)
          {
            nullable = apTran.IsStockItem;
            bool? stkItem = inventoryItem.StkItem;
            if (!(nullable.GetValueOrDefault() == stkItem.GetValueOrDefault() & nullable.HasValue == stkItem.HasValue))
              continue;
          }
        }
      }
      if ((!string.IsNullOrEmpty(apTran.ReceiptNbr) || !string.IsNullOrEmpty(apTran.PONbr)) && (!(apTran.POOrderType == "PD") || !POLineType.IsService(apTran.LineType) && apTran.DropshipExpenseRecording == "R"))
      {
        flag3 = true;
        break;
      }
    }
    if (current1.DocType == "ADR")
    {
      if (flag3 || this.Transactions.Select().RowCast<APTran>().Any<APTran>((Func<APTran, bool>) (_ => !string.IsNullOrEmpty(_.LCRefNbr))))
        throw new PXException("The debit adjustment is linked to a purchase order, purchase receipt, purchase return, landed cost, or subcontract and cannot be reversed automatically. To reverse the debit adjustment, create a credit adjustment or a bill.");
    }
    else if (flag3)
    {
      int num = (int) this.Document.Ask("Warning", "By reversing an AP bill that was matched to a PO receipt, your PO Receipt lines will be marked as unbilled and associated PO accrual account will be affected upon release of this document.", MessageButtons.OK, MessageIcon.Warning);
    }
    APInvoice copy1 = PXCache<APInvoice>.CreateCopy(this.Document.Current);
    this._finPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<APInvoice.finPeriodID, APInvoice.branchID>(this.Document.Cache, (object) copy1, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aPClosed));
    try
    {
      this.IsReverseContext = true;
      this.ReverseInvoiceProc((APRegister) copy1);
      this.Document.Cache.RaiseExceptionHandling<APInvoice.finPeriodID>((object) this.Document.Current, (object) this.Document.Current.FinPeriodID, (Exception) null);
      return (IEnumerable) new List<APInvoice>()
      {
        this.Document.Current
      };
    }
    catch (PXException ex)
    {
      this.Clear(PXClearOption.PreserveTimeStamp);
      this.Document.Current = current1;
      throw;
    }
    finally
    {
      this.IsReverseContext = false;
    }
  }

  public virtual void ApplyOriginalDocAdjustmentToDebitMemo(
    APInvoice origDoc,
    APInvoice reversingDebitMemo)
  {
    APInvoiceEntry.APAdjust2 apAdjust2 = new APInvoiceEntry.APAdjust2();
    apAdjust2.AdjgDocType = reversingDebitMemo.DocType;
    apAdjust2.AdjgRefNbr = reversingDebitMemo.RefNbr;
    apAdjust2.AdjdDocType = origDoc.DocType;
    apAdjust2.AdjdRefNbr = origDoc.RefNbr;
    apAdjust2.AdjNbr = origDoc.AdjCntr;
    apAdjust2.CuryAdjgAmt = origDoc.CuryDocBal;
    apAdjust2.CuryAdjdAmt = origDoc.CuryDocBal;
    apAdjust2.AdjAmt = origDoc.DocBal;
    apAdjust2.VendorID = origDoc.VendorID;
    apAdjust2.AdjgBranchID = origDoc.BranchID;
    apAdjust2.AdjdBranchID = origDoc.BranchID;
    apAdjust2.AdjgCuryInfoID = origDoc.CuryInfoID;
    apAdjust2.AdjdCuryInfoID = origDoc.CuryInfoID;
    apAdjust2.AdjdOrigCuryInfoID = origDoc.CuryInfoID;
    apAdjust2.InvoiceID = origDoc.NoteID;
    apAdjust2.MemoID = reversingDebitMemo.NoteID;
    this.Adjustments_1.Insert(apAdjust2);
  }

  [PXUIField(DisplayName = "Reclassify GL Batch", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ReclassifyBatch(PXAdapter adapter)
  {
    APInvoice current = this.Document.Current;
    if (current != null)
      ReclassifyTransactionsProcess.TryOpenForReclassificationOfDocument(this.Document.View, "AP", current.BatchNbr, current.DocType, current.RefNbr);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Pay/Apply Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable PayInvoice(PXAdapter adapter)
  {
    if (this.Document.Current != null)
    {
      bool? nullable = this.Document.Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = this.Document.Current.Prebooked;
        if (!nullable.GetValueOrDefault())
          goto label_30;
      }
      APInvoiceEntry.APAdjust application = (APInvoiceEntry.APAdjust) null;
      if (this.Document.Current.DocType == "PPI")
      {
        if (this.AskUserApprovalIfUnreleasedDebitAdjApplicationAlreadyExists(this.Document.Current, "The prepayment invoice has already been applied to the {0} debit adjustment, but the debit adjustment has not been released. Do you want to open the debit adjustment?", out application))
        {
          this.Document.Current = (APInvoice) this.Document.Search<APInvoice.refNbr>((object) application.AdjgRefNbr, (object) "ADR");
          PXRedirectHelper.TryRedirect((PXGraph) this, PXRedirectHelper.WindowMode.Same);
        }
        if (application != null)
          return adapter.Get();
      }
      APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
      int num;
      if (this.Document.Current.DocType == "ADR")
      {
        nullable = this.Document.Current.PaymentsByLinesAllowed;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num = 0;
      bool flag1 = num != 0;
      nullable = this.Document.Current.OpenDoc;
      if (nullable.GetValueOrDefault())
      {
        nullable = this.Document.Current.Payable;
        if (((nullable.GetValueOrDefault() ? 1 : (this.Document.Current.DocType == "PPM" ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          nullable = this.Document.Current.PendingPPD;
          if (nullable.GetValueOrDefault())
            throw new PXSetPropertyException("This document has been paid in full. To close the document, apply the cash discount by generating a debit adjustment on the Generate AP Tax Adjustments (AP504500) form.");
          string field0 = (string) null;
          string str = (string) null;
          APInvoiceEntry.APAdjust apAdjust = (APInvoiceEntry.APAdjust) PXSelectBase<APInvoiceEntry.APAdjust, PXSelect<APInvoiceEntry.APAdjust, Where<APInvoiceEntry.APAdjust.adjdDocType, Equal<Current<APInvoice.docType>>, And<APInvoiceEntry.APAdjust.adjdRefNbr, Equal<Current<APInvoice.refNbr>>, And<APInvoiceEntry.APAdjust.voided, Equal<False>>>>, PX.Data.OrderBy<Asc<APInvoiceEntry.APAdjust.released>>>.Config>.Select((PXGraph) this);
          if (apAdjust != null)
          {
            nullable = apAdjust.Released;
            if (nullable.GetValueOrDefault())
            {
              if (apAdjust.AdjdDocType == "PPM")
              {
                if (PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.refNbr, Equal<Required<APPayment.refNbr>>, And<APPayment.docType, Equal<Required<APPayment.docType>>>>>.Config>.Select((PXGraph) this, (object) this.Document.Current.RefNbr, (object) this.Document.Current.DocType).Count > 0)
                {
                  field0 = this.Document.Current.RefNbr;
                  str = this.Document.Current.DocType;
                }
              }
            }
            else
            {
              field0 = apAdjust.AdjgRefNbr;
              str = apAdjust.AdjgDocType;
            }
          }
          if (field0 != null && str != null)
          {
            instance.Document.Current = (APPayment) instance.Document.Search<APPayment.refNbr>((object) field0, (object) str);
          }
          else
          {
            if (this.Document.Current.DocType == "PPI")
            {
              nullable = this.Document.Current.PendingPayment;
              bool flag2 = false;
              if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
              {
                instance.Document.Current = (APPayment) instance.Document.Search<APPayment.refNbr>((object) this.Document.Current.RefNbr, (object) this.Document.Current.DocType);
                goto label_27;
              }
            }
            instance.Clear();
            instance.CreatePayment(this.Document.Current, "CHK");
          }
label_27:
          throw new PXRedirectRequiredException((PXGraph) instance, nameof (PayInvoice));
        }
      }
      if (this.Document.Current.DocType == "ADR")
      {
        instance.Document.Current = (APPayment) instance.Document.Search<APPayment.refNbr>((object) this.Document.Current.RefNbr, (object) this.Document.Current.DocType);
        throw new PXRedirectRequiredException((PXGraph) instance, nameof (PayInvoice));
      }
    }
label_30:
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add to Schedule", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable CreateSchedule(PXAdapter adapter)
  {
    APInvoice current = this.Document.Current;
    if (current == null)
      return adapter.Get();
    this.Save.Press();
    IsSchedulable<APRegister>.Ensure((PXGraph) this, (APRegister) current);
    APScheduleMaint instance1 = PXGraph.CreateInstance<APScheduleMaint>();
    if (!current.Scheduled.GetValueOrDefault() || current.ScheduleID == null)
    {
      instance1.Schedule_Header.Cache.Insert();
      APRegister instance2 = instance1.Document_Detail.Cache.CreateInstance() as APRegister;
      PXCache<APRegister>.RestoreCopy(instance2, (APRegister) current);
      APRegister apRegister = instance1.Document_Detail.Cache.Update((object) instance2) as APRegister;
      throw new PXRedirectRequiredException((PXGraph) instance1, "Create Schedule");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Schedule", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(CommitChanges = false)]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ViewScheduleOfCurrentDocument(PXAdapter adapter)
  {
    APInvoice current = this.Document.Current;
    if (current == null)
      return adapter.Get();
    if (this.IsDirty)
      this.Save.Press();
    APScheduleMaint instance = PXGraph.CreateInstance<APScheduleMaint>();
    if (current.Scheduled.GetValueOrDefault() && current.ScheduleID != null)
    {
      instance.Schedule_Header.Current = (PX.Objects.GL.Schedule) instance.Schedule_Header.Search<PX.Objects.GL.Schedule.scheduleID>((object) current.ScheduleID);
      throw new PXRedirectRequiredException((PXGraph) instance, "View Schedule");
    }
    return adapter.Get();
  }

  public virtual void AddLandedCosts(IEnumerable<POLandedCostDetailS> details)
  {
    foreach (IGrouping<\u003C\u003Ef__AnonymousType0<string, string>, POLandedCostDetailS> source in details.GroupBy(t => new
    {
      DocType = t.DocType,
      RefNbr = t.RefNbr
    }))
    {
      PXResultset<POLandedCostDoc> doc = PXSelectBase<POLandedCostDoc, PXSelect<POLandedCostDoc, Where<POLandedCostDoc.docType, Equal<Required<POLandedCostDoc.docType>>, And<POLandedCostDoc.refNbr, Equal<Required<POLandedCostDoc.refNbr>>>>>.Config>.Select((PXGraph) this, (object) source.Key.DocType, (object) source.Key.RefNbr);
      List<POLandedCostDetail> list = PXSelectBase<POLandedCostDetail, PXSelect<POLandedCostDetail, Where<POLandedCostDetail.docType, Equal<Required<POLandedCostDetail.docType>>, And<POLandedCostDetail.refNbr, Equal<Required<POLandedCostDetail.refNbr>>, And<POLandedCostDetail.lineNbr, PX.Data.In<Required<POLandedCostDetail.lineNbr>>>>>>.Config>.Select((PXGraph) this, (object) source.Key.DocType, (object) source.Key.RefNbr, (object) source.Select<POLandedCostDetailS, int?>((Func<POLandedCostDetailS, int?>) (t => t.LineNbr)).ToArray<int?>()).RowCast<POLandedCostDetail>().ToList<POLandedCostDetail>();
      Decimal mult = this.Document.Current.DrCr == "D" ? 1M : -1M;
      EnumerableExtensions.ForEach<APTran>((IEnumerable<APTran>) this.GetLandedCostApBillFactory().CreateTransactions((POLandedCostDoc) doc, (IEnumerable<POLandedCostDetail>) list, mult), (System.Action<APTran>) (tran => this.LandedCostDetailSetLink(this.Transactions.Insert(tran))));
    }
  }

  protected virtual POLandedCostDetail GetLandedCostDetail(
    string docType,
    string refNbr,
    int lineNbr)
  {
    return (POLandedCostDetail) PXSelectBase<POLandedCostDetail, PXSelect<POLandedCostDetail, Where<POLandedCostDetail.docType, Equal<Required<POLandedCostDetail.docType>>, And<POLandedCostDetail.refNbr, Equal<Required<POLandedCostDetail.refNbr>>, And<POLandedCostDetail.lineNbr, Equal<Required<POLandedCostDetail.lineNbr>>>>>>.Config>.Select((PXGraph) this, (object) docType, (object) refNbr, (object) lineNbr);
  }

  public virtual void LandedCostDetailSetLink(APTran tran)
  {
    if (tran == null || tran.LCDocType == null || tran.LCRefNbr == null || !tran.LCLineNbr.HasValue)
      return;
    POLandedCostDetail landedCostDetail = this.GetLandedCostDetail(tran.LCDocType, tran.LCRefNbr, tran.LCLineNbr.Value);
    landedCostDetail.APDocType = tran.TranType;
    landedCostDetail.APRefNbr = tran.RefNbr;
    PXParentAttribute.SetParent((PXCache) this.Caches<POLandedCostDetail>(), (object) landedCostDetail, typeof (APInvoice), (object) this.Document.Current);
    this.Caches<POLandedCostDetail>().SetStatus((object) landedCostDetail, PXEntryStatus.Updated);
  }

  public virtual void LandedCostDetailClearLink(APTran tran)
  {
    if (this.Document.Current == null || tran == null || tran.LCDocType == null || tran.LCRefNbr == null)
      return;
    int? lcLineNbr = tran.LCLineNbr;
    if (!lcLineNbr.HasValue)
      return;
    string lcDocType = tran.LCDocType;
    string lcRefNbr = tran.LCRefNbr;
    lcLineNbr = tran.LCLineNbr;
    int lineNbr = lcLineNbr.Value;
    POLandedCostDetail landedCostDetail = this.GetLandedCostDetail(lcDocType, lcRefNbr, lineNbr);
    if (landedCostDetail.APDocType != this.Document.Current.DocType || landedCostDetail.APRefNbr != this.Document.Current.RefNbr)
      return;
    landedCostDetail.APDocType = (string) null;
    landedCostDetail.APRefNbr = (string) null;
    PXParentAttribute.SetParent((PXCache) this.Caches<POLandedCostDetail>(), (object) landedCostDetail, typeof (APInvoice), (object) null);
    this.Caches<POLandedCostDetail>().SetStatus((object) landedCostDetail, PXEntryStatus.Updated);
  }

  public virtual void LinkLandedCostDetailLine(
    APInvoice doc,
    APTran apTran,
    POLandedCostDetailS detail)
  {
    if (doc == null || apTran == null || detail == null)
      return;
    apTran.ReceiptType = (string) null;
    apTran.ReceiptNbr = (string) null;
    apTran.ReceiptLineNbr = new int?();
    apTran.POOrderType = (string) null;
    apTran.PONbr = (string) null;
    apTran.POLineNbr = new int?();
    apTran.AccountID = new int?();
    apTran.SubID = new int?();
    apTran.UOM = (string) null;
    apTran.Qty = new Decimal?(1M);
    apTran.CuryUnitCost = apTran.CuryLineAmt;
    apTran.LCDocType = (string) null;
    apTran.LCRefNbr = (string) null;
    apTran.LCLineNbr = new int?();
    apTran.LandedCostCodeID = (string) null;
    LandedCostCode landedCostCode = LandedCostCode.PK.Find((PXGraph) this, detail.LandedCostCodeID);
    apTran.AccountID = landedCostCode.LCAccrualAcct;
    apTran.SubID = landedCostCode.LCAccrualSub;
    apTran.TranDesc = detail.Descr;
    apTran.TaxCategoryID = detail.TaxCategoryID;
    apTran.LCDocType = detail.DocType;
    apTran.LCRefNbr = detail.RefNbr;
    apTran.LCLineNbr = detail.LineNbr;
    apTran.LandedCostCodeID = detail.LandedCostCodeID;
    this.Transactions.Cache.Update((object) apTran);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  public void checkTaxCalcMode()
  {
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  public void updateTaxCalcMode()
  {
  }

  [PXUIField(DisplayName = "View PO Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true)]
  [PXLookupButton]
  public virtual IEnumerable ViewPODocument(PXAdapter adapter)
  {
    if (this.Transactions.Current != null)
    {
      APTran current = this.Transactions.Current;
      if (!string.IsNullOrEmpty(current.ReceiptNbr))
      {
        POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
        instance.Document.Current = (PX.Objects.PO.POReceipt) instance.Document.Search<PX.Objects.PO.POReceipt.receiptNbr>((object) current.ReceiptNbr, (object) current.ReceiptType);
        if (instance.Document.Current != null)
          throw new PXRedirectRequiredException((PXGraph) instance, "View PO Receipt");
      }
      else if (!string.IsNullOrEmpty(current.POOrderType) && !string.IsNullOrEmpty(current.PONbr))
      {
        POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
        instance.Document.Current = (PX.Objects.PO.POOrder) instance.Document.Search<PX.Objects.PO.POOrder.orderNbr>((object) current.PONbr, (object) current.POOrderType);
        if (instance.Document.Current != null)
          throw new PXRedirectRequiredException((PXGraph) instance, "View PO Order");
      }
    }
    return adapter.Get();
  }

  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  protected virtual IEnumerable viewOriginalDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(this.Document.Current.OrigDocType, this.Document.Current.OrigRefNbr, this.Document.Current.OrigModule, this.Document.Current.IsChildRetainageDocument());
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Auto-Apply", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable AutoApply(PXAdapter adapter)
  {
    if (this.Document.Current != null && this.Document.Current.DocType == "INV")
    {
      bool? nullable1 = this.Document.Current.Released;
      bool flag1 = false;
      if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
      {
        nullable1 = this.Document.Current.Prebooked;
        bool flag2 = false;
        if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
        {
          foreach (PXResult<APInvoiceEntry.APAdjust, APPayment, APRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in this.SelectAdjustmentsRaw())
          {
            APInvoiceEntry.APAdjust row = (APInvoiceEntry.APAdjust) pxResult;
            row.CuryAdjdAmt = new Decimal?(0M);
            this.Adjustments_Raw.Cache.MarkUpdated((object) row);
          }
          Decimal? val = this.Document.Current.CuryDocBal;
          foreach (PXResult<APInvoiceEntry.APAdjust, APPayment> pxResult in this.Adjustments.Select())
          {
            APInvoiceEntry.APAdjust adj = (APInvoiceEntry.APAdjust) pxResult;
            Decimal? curyDocBal = adj.CuryDocBal;
            Decimal num = 0M;
            if (curyDocBal.GetValueOrDefault() > num & curyDocBal.HasValue)
            {
              curyDocBal = adj.CuryDocBal;
              Decimal? nullable2 = val;
              if (curyDocBal.GetValueOrDefault() > nullable2.GetValueOrDefault() & curyDocBal.HasValue & nullable2.HasValue)
              {
                UpdateAdjdAmt(val);
                Decimal? nullable3 = new Decimal?(0M);
                break;
              }
              Decimal? nullable4 = val;
              curyDocBal = adj.CuryDocBal;
              val = nullable4.HasValue & curyDocBal.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - curyDocBal.GetValueOrDefault()) : new Decimal?();
              UpdateAdjdAmt(adj.CuryDocBal);
            }

            void UpdateAdjdAmt(Decimal? val)
            {
              adj.CuryAdjdAmt = val;
              this.Adjustments.Cache.RaiseFieldUpdated<APInvoiceEntry.APAdjust.curyAdjdAmt>((object) adj, (object) 0M);
              this.Adjustments.Cache.MarkUpdated((object) adj);
              this.Adjustments.Cache.IsDirty = true;
            }
          }
          this.Adjustments.View.RequestRefresh();
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Void Prepayment", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable VoidDocument(PXAdapter adapter)
  {
    APRegister current = (APRegister) this.Document.Current;
    if (current == null || !(current.DocType == "PPM"))
      return adapter.Get();
    APInvoiceEntry.APAdjust apAdjust = (APInvoiceEntry.APAdjust) PXSelectBase<APInvoiceEntry.APAdjust, PXSelect<APInvoiceEntry.APAdjust, Where<APInvoiceEntry.APAdjust.adjdDocType, Equal<Required<APInvoiceEntry.APAdjust.adjdDocType>>, And<APInvoiceEntry.APAdjust.adjdRefNbr, Equal<Required<APInvoiceEntry.APAdjust.adjdRefNbr>>>>>.Config>.Select((PXGraph) this, (object) current.DocType, (object) current.RefNbr);
    if (apAdjust != null)
      throw new PXException(apAdjust.Released.GetValueOrDefault() ? "The prepayment request cannot be voided. It has been paid with check {0}." : "The prepayment request cannot be voided. It has been selected for payment with check {0}. To void the prepayment request, first remove the check application.", new object[1]
      {
        (object) apAdjust.AdjgRefNbr
      });
    this.VoidPrepayment(current);
    return (IEnumerable) new List<APInvoice>()
    {
      this.Document.Current
    };
  }

  [PXUIField(DisplayName = "View Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXLookupButton]
  public virtual IEnumerable ViewPayment(PXAdapter adapter)
  {
    if (this.Document.Current != null && this.Adjustments.Current != null)
    {
      switch (this.Adjustments.Current.AdjType)
      {
        case "G":
          APPaymentEntry instance1 = PXGraph.CreateInstance<APPaymentEntry>();
          instance1.Document.Current = (APPayment) instance1.Document.Search<APPayment.refNbr>((object) this.Adjustments.Current.AdjgRefNbr, (object) this.Adjustments.Current.AdjgDocType);
          PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Payment");
          requiredException1.Mode = PXBaseRedirectException.WindowMode.NewWindow;
          throw requiredException1;
        case "D":
          APInvoiceEntry instance2 = PXGraph.CreateInstance<APInvoiceEntry>();
          instance2.Document.Current = (APInvoice) instance2.Document.Search<APInvoice.refNbr>((object) this.Adjustments.Current.AdjdRefNbr, (object) this.Adjustments.Current.AdjdDocType);
          PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException((PXGraph) instance2, true, "Invoice");
          requiredException2.Mode = PXBaseRedirectException.WindowMode.NewWindow;
          throw requiredException2;
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Invoice", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXLookupButton]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (this.Document.Current == null || this.Adjustments_1.Current == null)
      return adapter.Get();
    if (APDocType.Payable(this.Adjustments_1.Current.DisplayDocType).GetValueOrDefault())
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      instance.Document.Current = (APInvoice) instance.Document.Search<APInvoice.refNbr>((object) this.Adjustments_1.Current.AdjdRefNbr, (object) this.Adjustments_1.Current.AdjdDocType);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Invoice");
      requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
      throw requiredException;
    }
    APPaymentEntry instance1 = PXGraph.CreateInstance<APPaymentEntry>();
    instance1.Document.Current = (APPayment) instance1.Document.Search<APPayment.refNbr>((object) this.Adjustments_1.Current.AdjgRefNbr, (object) this.Adjustments_1.Current.AdjgDocType);
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, true, "Payment");
    requiredException1.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException1;
  }

  [PXUIField(DisplayName = "View Item", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXLookupButton]
  public virtual IEnumerable ViewItem(PXAdapter adapter)
  {
    if (this.Transactions.Current != null)
    {
      PX.Objects.IN.InventoryItem row = (PX.Objects.IN.InventoryItem) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null);
      if (row != null)
        PXRedirectHelper.TryRedirect(this.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) row, "View Item", PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Recalculate Prices", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable RecalculateDiscountsAction(PXAdapter adapter)
  {
    if (adapter.MassProcess)
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
      {
        this._discountEngine.RecalculatePricesAndDiscounts(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, this.Transactions.Current, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.recalcdiscountsfilter.Current, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
        this.Save.Press();
      }));
    else if (this.recalcdiscountsfilter.AskExt() == WebDialogResult.OK)
      this._discountEngine.RecalculatePricesAndDiscounts(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, this.Transactions.Current, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.recalcdiscountsfilter.Current, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "OK", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  public virtual IEnumerable RecalcOk(PXAdapter adapter) => adapter.Get();

  private bool AppendAdjustmentsRawTail(
    APInvoiceEntry.APAdjust adj,
    PXResult<APPayment> paymentResult)
  {
    this.Adjustments_Raw.StoreTailResult((PXResult) paymentResult, (object[]) new APInvoiceEntry.APAdjust[1]
    {
      adj
    }, (object) this.Document.Current.NoteID);
    return true;
  }

  protected virtual IEnumerable<PXResult<APInvoiceEntry.APAdjust>> SelectAdjustmentsRaw()
  {
    if (this.Adjustments_Raw.Cache.Cached.Count() > 1L)
      this.Adjustments_Raw.Cache.Cached.OfType<APInvoiceEntry.APAdjust>().Join<APInvoiceEntry.APAdjust, PXResult<APPayment>, string, bool>((IEnumerable<PXResult<APPayment>>) this.AvailablePayments.Select((object[]) new string[1][]
      {
        this.Adjustments_Raw.Cache.Cached.Cast<APInvoiceEntry.APAdjust>().Select<APInvoiceEntry.APAdjust, string>((Func<APInvoiceEntry.APAdjust, string>) (_ => _.AdjgRefNbr)).ToArray<string>()
      }), (Func<APInvoiceEntry.APAdjust, string>) (_ => _.AdjgDocType + _.AdjgRefNbr), (Func<PXResult<APPayment>, string>) (_ => _.GetItem<APPayment>().DocType + _.GetItem<APPayment>().RefNbr), new Func<APInvoiceEntry.APAdjust, PXResult<APPayment>, bool>(this.AppendAdjustmentsRawTail)).ToArray<bool>();
    return (IEnumerable<PXResult<APInvoiceEntry.APAdjust>>) this.Adjustments_Raw.Select();
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public virtual IEnumerable transactions() => (IEnumerable) null;

  public virtual IEnumerable taxes()
  {
    bool hasPPDTaxes = false;
    bool vatReportingInstalled = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATReporting>();
    APTaxTran aptaxMax = (APTaxTran) null;
    Decimal? discountedTaxableTotal = new Decimal?(0M);
    Decimal? discountedPriceTotal = new Decimal?(0M);
    foreach (PXResult<APTaxTran, PX.Objects.TX.Tax> pxResult in this.TaxesList.Select())
    {
      if (vatReportingInstalled)
      {
        PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
        APTaxTran aptaxTran = (APTaxTran) pxResult;
        hasPPDTaxes = tax.TaxApplyTermsDisc == "P" | hasPPDTaxes;
        if (hasPPDTaxes && this.Document.Current != null)
        {
          Decimal? nullable1 = this.Document.Current.CuryOrigDocAmt;
          if (nullable1.HasValue)
          {
            nullable1 = this.Document.Current.CuryOrigDocAmt;
            Decimal num1 = 0M;
            if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
            {
              nullable1 = this.Document.Current.CuryOrigDiscAmt;
              if (nullable1.HasValue)
              {
                nullable1 = this.Document.Current.CuryOrigDiscAmt;
                Decimal? nullable2 = this.Document.Current.CuryOrigDocAmt;
                Decimal cashDiscPercent = (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?()).Value;
                int num2 = APPPDDebitAdjProcess.CalculateDiscountedTaxes(this.Taxes.Cache, aptaxTran, cashDiscPercent) ? 1 : 0;
                bool? reverseTax = tax.ReverseTax;
                Decimal num3 = reverseTax.GetValueOrDefault() ? -1M : 1M;
                nullable1 = discountedPriceTotal;
                Decimal? nullable3 = aptaxTran.CuryDiscountedPrice;
                Decimal num4 = num3;
                nullable2 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num4) : new Decimal?();
                Decimal? nullable4;
                if (!(nullable1.HasValue & nullable2.HasValue))
                {
                  nullable3 = new Decimal?();
                  nullable4 = nullable3;
                }
                else
                  nullable4 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
                discountedPriceTotal = nullable4;
                if (num2 != 0)
                {
                  reverseTax = tax.ReverseTax;
                  bool flag = false;
                  if (reverseTax.GetValueOrDefault() == flag & reverseTax.HasValue)
                  {
                    nullable2 = discountedTaxableTotal;
                    nullable1 = aptaxTran.CuryDiscountedTaxableAmt;
                    Decimal? nullable5;
                    if (!(nullable2.HasValue & nullable1.HasValue))
                    {
                      nullable3 = new Decimal?();
                      nullable5 = nullable3;
                    }
                    else
                      nullable5 = new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault());
                    discountedTaxableTotal = nullable5;
                  }
                  if (aptaxMax != null)
                  {
                    nullable1 = aptaxTran.CuryDiscountedTaxableAmt;
                    nullable2 = aptaxMax.CuryDiscountedTaxableAmt;
                    if (!(nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
                      goto label_20;
                  }
                  aptaxMax = aptaxTran;
                }
              }
            }
          }
        }
      }
label_20:
      yield return (object) pxResult;
    }
    if (vatReportingInstalled && this.Document.Current != null)
    {
      this.Document.Current.HasPPDTaxes = new bool?(hasPPDTaxes);
      if (hasPPDTaxes)
      {
        Decimal? nullable6 = discountedTaxableTotal;
        Decimal? nullable7 = discountedPriceTotal;
        Decimal? nullable8 = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
        APInvoice current = this.Document.Current;
        nullable7 = this.Document.Current.CuryOrigDocAmt;
        Decimal? nullable9 = this.Document.Current.CuryOrigDiscAmt;
        Decimal? nullable10 = nullable7.HasValue & nullable9.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new Decimal?();
        current.CuryDiscountedDocTotal = nullable10;
        if (aptaxMax != null)
        {
          Decimal? nullable11 = this.Document.Current.CuryVatTaxableTotal;
          Decimal? nullable12 = this.Document.Current.CuryTaxTotal;
          nullable9 = nullable11.HasValue & nullable12.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new Decimal?();
          nullable7 = this.Document.Current.CuryOrigDocAmt;
          if (nullable9.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable9.HasValue == nullable7.HasValue)
          {
            nullable7 = nullable8;
            nullable9 = this.Document.Current.CuryDiscountedDocTotal;
            if (!(nullable7.GetValueOrDefault() == nullable9.GetValueOrDefault() & nullable7.HasValue == nullable9.HasValue))
            {
              APTaxTran apTaxTran = aptaxMax;
              nullable9 = apTaxTran.CuryDiscountedTaxableAmt;
              nullable12 = this.Document.Current.CuryDiscountedDocTotal;
              nullable11 = nullable8;
              nullable7 = nullable12.HasValue & nullable11.HasValue ? new Decimal?(nullable12.GetValueOrDefault() - nullable11.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable13;
              if (!(nullable9.HasValue & nullable7.HasValue))
              {
                nullable11 = new Decimal?();
                nullable13 = nullable11;
              }
              else
                nullable13 = new Decimal?(nullable9.GetValueOrDefault() + nullable7.GetValueOrDefault());
              apTaxTran.CuryDiscountedTaxableAmt = nullable13;
              nullable7 = this.Document.Current.CuryDiscountedDocTotal;
              nullable9 = discountedPriceTotal;
              Decimal? nullable14;
              if (!(nullable7.HasValue & nullable9.HasValue))
              {
                nullable11 = new Decimal?();
                nullable14 = nullable11;
              }
              else
                nullable14 = new Decimal?(nullable7.GetValueOrDefault() - nullable9.GetValueOrDefault());
              discountedTaxableTotal = nullable14;
            }
          }
        }
        this.Document.Current.CuryDiscountedPrice = discountedPriceTotal;
        this.Document.Current.CuryDiscountedTaxableTotal = discountedTaxableTotal;
      }
    }
  }

  [PXCustomizeBaseAttribute(typeof (OwnerAttribute), "Visibility", PXUIVisibility.Visible)]
  [PXCustomizeBaseAttribute(typeof (OwnerAttribute), "Visible", false)]
  public virtual void _(PX.Data.Events.CacheAttached<EPApproval.origOwnerID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visibility", PXUIVisibility.Visible)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  public virtual void _(PX.Data.Events.CacheAttached<EPApproval.assignmentMapID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visibility", PXUIVisibility.Visible)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  public virtual void _(PX.Data.Events.CacheAttached<EPApproval.ruleID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visibility", PXUIVisibility.Visible)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  public virtual void _(PX.Data.Events.CacheAttached<EPApproval.stepID> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visibility", PXUIVisibility.Visible)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  public virtual void _(PX.Data.Events.CacheAttached<EPApproval.createdDateTime> e)
  {
  }

  public virtual void VoidPrepayment(APRegister doc)
  {
    APInvoice copy = PXCache<APInvoice>.CreateCopy((APInvoice) doc);
    copy.OpenDoc = new bool?(false);
    copy.Voided = new bool?(true);
    copy.CuryDocBal = new Decimal?(0M);
    this.Document.Update(copy);
    this.Save.Press();
  }

  public virtual void ReverseInvoiceProc(APRegister doc)
  {
    DuplicateFilter copy1 = PXCache<DuplicateFilter>.CreateCopy(this.duplicatefilter.Current);
    WebDialogResult answer = this.duplicatefilter.View.Answer;
    this.Clear(PXClearOption.PreserveTimeStamp);
    this.CurrentDocument.Cache.AllowUpdate = true;
    Company company = (Company) PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this);
    foreach (PXResult<APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, Vendor> pxResult in PXSelectBase<APInvoice, PXSelectJoin<APInvoice, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<APInvoice.termsID>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>>>>, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APInvoice apInvoice1 = (APInvoice) pxResult;
      bool? nullable1 = apInvoice1.IsTaxDocument;
      bool flag1 = nullable1.GetValueOrDefault() && apInvoice1.CuryID != company.BaseCuryID;
      PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy((PX.Objects.CM.Extensions.CurrencyInfo) pxResult);
      copy2.CuryInfoID = new long?();
      copy2.IsReadOnly = new bool?(false);
      copy2.BaseCalc = new bool?(!flag1);
      PX.Objects.CM.Extensions.CurrencyInfo copy3 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(copy2));
      APInvoice apInvoice2 = PXCache<APInvoice>.CreateCopy(apInvoice1);
      apInvoice2.CuryInfoID = copy3.CuryInfoID;
      apInvoice2.DocType = this.GetReversingDocType(apInvoice2.DocType);
      apInvoice2.RefNbr = (string) null;
      PX.Objects.AP.APSetup current1 = this.APSetup.Current;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable1 = current1.MigrationMode;
        bool flag2 = false;
        num1 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
      }
      if (num1 != 0)
      {
        apInvoice2.InitDocBal = new Decimal?(0M);
        apInvoice2.CuryInitDocBal = new Decimal?(0M);
      }
      apInvoice2.OpenDoc = new bool?(true);
      apInvoice2.Released = new bool?(false);
      this.Document.Cache.SetDefaultExt<APInvoice.isMigratedRecord>((object) apInvoice2);
      apInvoice2.BatchNbr = (string) null;
      apInvoice2.PrebookBatchNbr = (string) null;
      apInvoice2.Prebooked = new bool?(false);
      apInvoice2.ScheduleID = (string) null;
      apInvoice2.Scheduled = new bool?(false);
      apInvoice2.NoteID = new Guid?();
      apInvoice2.InstallmentCntr = new short?();
      apInvoice2.InstallmentNbr = new short?();
      PX.Objects.AP.APSetup current2 = this.APSetup.Current;
      int num2;
      if (current2 == null)
      {
        num2 = 0;
      }
      else
      {
        nullable1 = current2.TermsInDebitAdjustments;
        bool flag3 = false;
        num2 = nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue ? 1 : 0;
      }
      if (num2 != 0)
      {
        apInvoice2.TermsID = (string) null;
        apInvoice2.DueDate = new System.DateTime?();
        apInvoice2.DiscDate = new System.DateTime?();
        apInvoice2.CuryOrigDiscAmt = new Decimal?(0M);
      }
      FinPeriodIDAttribute.SetPeriodsByMaster<APInvoice.finPeriodID>(this.CurrentDocument.Cache, (object) apInvoice2, doc.FinPeriodID);
      apInvoice2.OrigDocDate = apInvoice2.DocDate;
      if (doc.IsChildRetainageDocument())
      {
        apInvoice2.OrigDocType = doc.OrigDocType;
        apInvoice2.OrigRefNbr = doc.OrigRefNbr;
      }
      else
      {
        apInvoice2.OrigDocType = doc.DocType;
        apInvoice2.OrigRefNbr = doc.RefNbr;
      }
      apInvoice2.CuryDetailExtPriceTotal = new Decimal?(0M);
      apInvoice2.DetailExtPriceTotal = new Decimal?(0M);
      apInvoice2.CuryLineDiscTotal = new Decimal?(0M);
      apInvoice2.LineDiscTotal = new Decimal?(0M);
      apInvoice2.PaySel = new bool?(false);
      if (doc.IsPrepaymentInvoiceDocument())
      {
        nullable1 = doc.PendingPayment;
        if (nullable1.GetValueOrDefault())
        {
          apInvoice2.CuryOrigDocAmt = doc.CuryDocBal;
          apInvoice2.CuryTaxTotal = new Decimal?(0M);
          apInvoice2.CuryTaxAmt = new Decimal?(0M);
          apInvoice2.CuryOrigDiscAmt = new Decimal?(0M);
          apInvoice2.CuryDiscTot = new Decimal?(0M);
          apInvoice2.DisableAutomaticDiscountCalculation = new bool?(true);
          apInvoice2.PendingPayment = new bool?(false);
          goto label_19;
        }
      }
      apInvoice2.CuryDocBal = doc.CuryOrigDocAmt;
label_19:
      apInvoice2.CuryLineTotal = new Decimal?(0M);
      apInvoice2.IsTaxPosted = new bool?(false);
      apInvoice2.IsTaxValid = new bool?(false);
      apInvoice2.CuryVatTaxableTotal = new Decimal?(0M);
      apInvoice2.CuryVatExemptTotal = new Decimal?(0M);
      apInvoice2.PrebookAcctID = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.prebooking>() ? apInvoice1.PrebookAcctID : new int?();
      apInvoice2.PrebookSubID = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.prebooking>() ? apInvoice1.PrebookSubID : new int?();
      APInvoice apInvoice3 = apInvoice2;
      nullable1 = this.apsetup.Current.HoldEntry;
      bool? nullable2 = new bool?(nullable1.GetValueOrDefault() || this.IsApprovalRequired(apInvoice2));
      apInvoice3.Hold = nullable2;
      apInvoice2.PendingPPD = new bool?(false);
      apInvoice2.TaxCostINAdjRefNbr = (string) null;
      apInvoice2.IsRetainageReversing = new bool?(doc.IsOriginalRetainageDocument() || doc.IsChildRetainageDocument());
      apInvoice2.IntercompanyInvoiceNoteID = new Guid?();
      this.ClearRetainageSummary(apInvoice2);
      using (new DisableSelectorValidationScope(this.Document.Cache, new System.Type[1]
      {
        typeof (APRegister.employeeID)
      }))
      {
        this.Document.Cache.SetDefaultExt<APRegister.employeeID>((object) apInvoice2);
        this.Document.Cache.SetDefaultExt<APRegister.employeeWorkgroupID>((object) apInvoice2);
        apInvoice2 = this.Document.Insert(apInvoice2);
        APInvoice apInvoice4 = apInvoice2;
        PX.Objects.AP.APSetup current3 = this.APSetup.Current;
        int num3;
        if (current3 == null)
        {
          num3 = 0;
        }
        else
        {
          nullable1 = current3.TermsInDebitAdjustments;
          bool flag4 = false;
          num3 = nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue ? 1 : 0;
        }
        Decimal? nullable3 = num3 != 0 ? new Decimal?(0M) : doc.CuryOrigDiscAmt;
        apInvoice4.CuryOrigDiscAmt = nullable3;
        FinPeriodIDAttribute.SetPeriodsByMaster<APInvoice.finPeriodID>(this.CurrentDocument.Cache, (object) apInvoice2, doc.TranPeriodID);
      }
      apInvoice2.ExternalTaxExemptionNumber = apInvoice1.ExternalTaxExemptionNumber;
      apInvoice2.EntityUsageType = apInvoice1.EntityUsageType;
      if (apInvoice2.RefNbr == null)
      {
        if ((APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice>.Config>.Search<APInvoice.docType, APInvoice.refNbr>((PXGraph) this, (object) apInvoice2.DocType, (object) apInvoice2.OrigRefNbr) != null)
        {
          PXCache<DuplicateFilter>.RestoreCopy(this.duplicatefilter.Current, copy1);
          this.duplicatefilter.View.Answer = answer;
          if (this.duplicatefilter.AskExt() == WebDialogResult.OK)
          {
            this.duplicatefilter.Cache.Clear();
            if (this.duplicatefilter.Current.RefNbr == null)
              throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (DuplicateFilter.refNbr).Name
              });
            if ((APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice>.Config>.Search<APInvoice.docType, APInvoice.refNbr>((PXGraph) this, (object) apInvoice2.DocType, (object) this.duplicatefilter.Current.RefNbr) != null)
              throw new PXException("The record already exists.");
            this.Document.Cache.SetValueExt<APInvoice.refNbr>((object) apInvoice2, (object) this.duplicatefilter.Current.RefNbr);
          }
        }
        else
          this.Document.Cache.SetValueExt<APInvoice.refNbr>((object) apInvoice2, (object) apInvoice2.OrigRefNbr);
        this.Document.Cache.Normalize();
      }
      if (copy3 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null);
        currencyInfo.BaseCuryID = copy3.BaseCuryID;
        currencyInfo.CuryID = copy3.CuryID;
        currencyInfo.CuryEffDate = copy3.CuryEffDate;
        currencyInfo.CuryRateTypeID = copy3.CuryRateTypeID;
        currencyInfo.CuryRate = copy3.CuryRate;
        currencyInfo.RecipRate = copy3.RecipRate;
        currencyInfo.CuryMultDiv = copy3.CuryMultDiv;
        this.currencyinfo.Update(currencyInfo);
      }
    }
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
    Dictionary<int?, int?> origLineNbrsDict = new Dictionary<int?, int?>();
    if (doc.IsPrepaymentInvoiceDocument())
    {
      this.InsertReversedTransactionDetailsBalance(doc);
    }
    else
    {
      this.InsertReversedTransactionDetails(doc, origLineNbrsDict);
      foreach (PXResult<APInvoiceDiscountDetail> pxResult in PXSelectBase<APInvoiceDiscountDetail, PXSelect<APInvoiceDiscountDetail, Where<APInvoiceDiscountDetail.docType, Equal<Required<APInvoice.docType>>, And<APInvoiceDiscountDetail.refNbr, Equal<Required<APInvoice.refNbr>>>>, PX.Data.OrderBy<Asc<APInvoiceDiscountDetail.docType, Asc<APInvoiceDiscountDetail.refNbr>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
      {
        APInvoiceDiscountDetail copy4 = PXCache<APInvoiceDiscountDetail>.CreateCopy((APInvoiceDiscountDetail) pxResult);
        copy4.DocType = this.Document.Current.DocType;
        copy4.RefNbr = this.Document.Current.RefNbr;
        copy4.IsManual = new bool?(true);
        this._discountEngine.UpdateDiscountDetail(this.DiscountDetails.Cache, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, copy4);
      }
      if (this.IsExternalTax(this.Document.Current.TaxZoneID))
        return;
      bool flag = doc.PendingPPD.GetValueOrDefault() && doc.DocType == "ADR" || doc.IsOriginalRetainageDocument() || doc.IsChildRetainageDocument();
      OldNewTaxTranPair<APTaxTran>[] array = PXSelectBase<APTaxTran, PXSelect<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr).RowCast<APTaxTran>().Select<APTaxTran, OldNewTaxTranPair<APTaxTran>>(new Func<APTaxTran, OldNewTaxTranPair<APTaxTran>>(OldNewTaxTranPair.Create<APTaxTran>)).ToArray<OldNewTaxTranPair<APTaxTran>>();
      if (flag)
        EnumerableExtensions.ForEach<OldNewTaxTranPair<APTaxTran>>((IEnumerable<OldNewTaxTranPair<APTaxTran>>) array, (System.Action<OldNewTaxTranPair<APTaxTran>>) (_ => _.InsertCurrentNewTaxTran((PXSelectBase<APTaxTran>) this.Taxes)));
      foreach (OldNewTaxTranPair<APTaxTran> oldNewTaxTranPair in array)
      {
        APTaxTran oldTaxTran = oldNewTaxTranPair.OldTaxTran;
        APTaxTran apTaxTran = flag ? oldNewTaxTranPair.NewTaxTran : oldNewTaxTranPair.InsertCurrentNewTaxTran((PXSelectBase<APTaxTran>) this.Taxes);
        if (apTaxTran != null)
        {
          APTaxTran copy5 = PXCache<APTaxTran>.CreateCopy(apTaxTran);
          copy5.TaxRate = oldTaxTran.TaxRate;
          copy5.CuryTaxableAmt = oldTaxTran.CuryTaxableAmt;
          copy5.CuryTaxAmt = oldTaxTran.CuryTaxAmt;
          copy5.CuryTaxAmtSumm = oldTaxTran.CuryTaxAmtSumm;
          copy5.NonDeductibleTaxRate = oldTaxTran.NonDeductibleTaxRate;
          copy5.CuryExpenseAmt = oldTaxTran.CuryExpenseAmt;
          copy5.CuryRetainedTaxableAmt = oldTaxTran.CuryRetainedTaxableAmt;
          copy5.CuryRetainedTaxAmt = oldTaxTran.CuryRetainedTaxAmt;
          copy5.CuryRetainedTaxAmtSumm = oldTaxTran.CuryRetainedTaxAmtSumm;
          this.Taxes.Update(copy5);
        }
      }
      if (!doc.IsChildRetainageDocument() || !doc.PaymentsByLinesAllowed.GetValueOrDefault())
        return;
      foreach (PXResult<APTran> pxResult1 in this.Transactions.Select())
      {
        APTran apTran = (APTran) pxResult1;
        foreach (PXResult<APTax> pxResult2 in PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>, And<APTax.lineNbr, Equal<Required<APTax.lineNbr>>>>>>.Config>.Select((PXGraph) this, (object) apTran.TranType, (object) apTran.RefNbr, (object) apTran.LineNbr))
        {
          APTax apTax1 = (APTax) pxResult2;
          int? nullable = origLineNbrsDict[apTran.LineNbr];
          APTax apTax2 = (APTax) PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>, And<APTax.taxID, Equal<Required<APTax.taxID>>, And<APTax.lineNbr, Equal<Required<APTax.lineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) doc.DocType, (object) doc.RefNbr, (object) apTax1.TaxID, (object) nullable);
          if (apTax2 != null)
          {
            APTax copy6 = PXCache<APTax>.CreateCopy(apTax1);
            copy6.CuryTaxableAmt = apTax2.CuryTaxableAmt;
            copy6.CuryTaxAmt = apTax2.CuryTaxAmt;
            copy6.NonDeductibleTaxRate = apTax2.NonDeductibleTaxRate;
            copy6.CuryExpenseAmt = apTax2.CuryExpenseAmt;
            this.Tax_Rows.Update(copy6);
          }
        }
      }
    }
  }

  public virtual void InsertReversedTransactionDetails(
    APRegister doc,
    Dictionary<int?, int?> origLineNbrsDict)
  {
    foreach (PXResult<APTran> pxResult in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APTran src_row = (APTran) pxResult;
      APTran copy = PXCache<APTran>.CreateCopy(src_row);
      copy.TranType = (string) null;
      copy.RefNbr = (string) null;
      copy.OrigLineNbr = doc.IsChildRetainageDocument() ? src_row.OrigLineNbr : src_row.LineNbr;
      string drCr = copy.DrCr;
      copy.DrCr = (string) null;
      APTran apTran1 = copy;
      bool? nullable1 = new bool?();
      bool? nullable2 = nullable1;
      apTran1.Released = nullable2;
      copy.CuryInfoID = new long?();
      copy.ManualDisc = new bool?(true);
      copy.PPVDocType = (string) null;
      copy.PPVRefNbr = (string) null;
      copy.POPPVAmt = new Decimal?(0M);
      copy.NoteID = new Guid?();
      APTran apTran2 = copy;
      nullable1 = new bool?();
      bool? nullable3 = nullable1;
      apTran2.IsStockItem = nullable3;
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, copy.InventoryID);
      int num;
      if (inventoryItem != null)
      {
        nullable1 = inventoryItem.IsConverted;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = src_row.IsStockItem;
          if (nullable1.HasValue)
          {
            nullable1 = src_row.IsStockItem;
            bool? stkItem = inventoryItem.StkItem;
            num = !(nullable1.GetValueOrDefault() == stkItem.GetValueOrDefault() & nullable1.HasValue == stkItem.HasValue) ? 1 : 0;
            goto label_7;
          }
        }
      }
      num = 0;
label_7:
      bool flag = num != 0;
      if (flag)
      {
        copy.LineType = (string) null;
        copy.POAccrualLineNbr = new int?();
        copy.POAccrualRefNoteID = new Guid?();
        copy.POAccrualType = (string) null;
        copy.POOrderType = (string) null;
        copy.PONbr = (string) null;
        copy.POLineNbr = new int?();
        copy.ReceiptType = (string) null;
        copy.ReceiptNbr = (string) null;
        copy.ReceiptLineNbr = new int?();
        if (inventoryItem.StkItem.GetValueOrDefault())
          copy.InventoryID = new int?();
      }
      copy.ClearInvoiceDetailsBalance();
      if (!string.IsNullOrEmpty(copy.DeferredCode))
      {
        DRSchedule drSchedule = (DRSchedule) PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.module, Equal<PX.Objects.BQLConstants.moduleAP>, And<DRSchedule.docType, Equal<Required<DRSchedule.docType>>, And<DRSchedule.refNbr, Equal<Required<DRSchedule.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<DRSchedule.lineNbr>>>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr, (object) copy.LineNbr);
        if (drSchedule != null)
          copy.DefScheduleID = drSchedule.ScheduleID;
      }
      Decimal? curyTranAmt1 = copy.CuryTranAmt;
      APTran dst_row = this.Transactions.Insert(copy);
      PXNoteAttribute.CopyNoteAndFiles(this.Transactions.Cache, (object) src_row, this.Transactions.Cache, (object) dst_row);
      if (flag)
      {
        dst_row.AccountID = new int?();
        dst_row.SubID = new int?();
        dst_row = (APTran) this.Transactions.Cache.Update((object) dst_row);
      }
      if (dst_row != null)
      {
        Decimal? curyTranAmt2 = dst_row.CuryTranAmt;
        Decimal? nullable4 = curyTranAmt1;
        if (!(curyTranAmt2.GetValueOrDefault() == nullable4.GetValueOrDefault() & curyTranAmt2.HasValue == nullable4.HasValue))
        {
          dst_row.CuryTranAmt = curyTranAmt1;
          dst_row = (APTran) this.Transactions.Cache.Update((object) dst_row);
        }
      }
      if (dst_row.LineType == "DS")
      {
        dst_row.DrCr = drCr == "D" ? "C" : "D";
        dst_row.FreezeManualDisc = new bool?(true);
        dst_row.TaxCategoryID = (string) null;
        this.Transactions.Update(dst_row);
      }
      short? nullable5 = src_row.Box1099;
      if (!nullable5.HasValue)
      {
        APTran apTran3 = dst_row;
        nullable5 = new short?();
        short? nullable6 = nullable5;
        apTran3.Box1099 = nullable6;
      }
      if (src_row.DeferredCode == null)
        dst_row.DeferredCode = (string) null;
      origLineNbrsDict.Add(dst_row.LineNbr, src_row.LineNbr);
    }
  }

  public virtual void InsertReversedTransactionDetailsBalance(APRegister doc)
  {
    APTran row = this.Transactions.Insert(new APTran()
    {
      BranchID = doc.BranchID,
      TaxCategoryID = (string) null,
      CuryLineAmt = doc.CuryDocBal
    });
    row.AccountID = (int?) this.location.Current?.VExpenseAcctID;
    row.SubID = (int?) this.location.Current?.VExpenseSubID;
    APTran apTran = (APTran) PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APInvoice.docType>>, And<APTran.refNbr, Equal<Required<APInvoice.refNbr>>, PX.Data.And<Not<Where<APTran.lineType, PX.Data.IsNotNull, And<APTran.lineType, Equal<SOLineType.discount>>>>>>>, PX.Data.OrderBy<Asc<APTran.lineNbr>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr);
    row.TaskID = (int?) apTran?.TaskID;
    this.Transactions.Cache.MarkUpdated((object) row);
  }

  public virtual string GetReversingDocType(string docType) => docType == "ADR" ? "ACR" : "ADR";

  public object GetAcctSub<Field>(PXCache cache, object data) where Field : IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }

  public virtual LandedCostAPBillFactory GetLandedCostApBillFactory()
  {
    return new LandedCostAPBillFactory((PXGraph) this);
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  public APInvoiceEntry()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.RetainageDocuments.Cache.AllowSelect = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>();
    this.RetainageDocuments.Cache.AllowDelete = false;
    this.RetainageDocuments.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(this.RetainageDocuments.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.curyAdjdPPDAmt>(this.Adjustments.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<APTran.projectID>(this.Transactions.Cache, (object) null, ProjectAttribute.IsPMVisible("AP"));
    PXUIFieldAttribute.SetVisible<APTran.taskID>(this.Transactions.Cache, (object) null, ProjectAttribute.IsPMVisible("AP"));
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.ManualLineCalc);
    this.FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>((PXFieldDefaulting) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) false;
    }));
    OpenPeriodAttribute.SetValidatePeriod<APInvoice.finPeriodID>(this.Document.Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    TaxBaseAttribute.IncludeDirectTaxLine<APTran.taxCategoryID>(this.AllTransactions.Cache, (object) null, true);
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    this.OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<APInvoice>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (APTran), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[3]
      {
        (PXDataFieldValue) new PXDataFieldValue<APTran.tranType>(PXDbType.Char, new int?(3), (object) ((APInvoiceEntry) graph).Document.Current?.DocType),
        (PXDataFieldValue) new PXDataFieldValue<APTran.refNbr>((object) ((APInvoiceEntry) graph).Document.Current?.RefNbr),
        (PXDataFieldValue) new PXDataFieldValue<APTran.lineType>(PXDbType.Char, new int?(2), (object) "DS", PXComp.NEorISNULL)
      }))
    });
  }

  public override void Persist()
  {
    Decimal? nullable1;
    if (this.Document.Current != null)
    {
      bool flag = this.Discount_Row.Any<APTran>();
      if (!flag)
      {
        nullable1 = this.Document.Current.CuryDiscTot;
        Decimal num = 0M;
        if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        {
          this.AddDiscount(this.Document.Cache, this.Document.Current);
          goto label_7;
        }
      }
      if (flag)
      {
        nullable1 = this.Document.Current.CuryDiscTot;
        Decimal num = 0M;
        if (nullable1.GetValueOrDefault() == num & nullable1.HasValue && !this.DiscountDetails.Any<APInvoiceDiscountDetail>())
          EnumerableExtensions.ForEach<APTran>(this.Discount_Row.Select().RowCast<APTran>(), (System.Action<APTran>) (discountLine => this.Discount_Row.Cache.Delete((object) discountLine)));
      }
    }
label_7:
    foreach (APInvoiceEntry.APAdjust apAdjust in this.Adjustments.Cache.Inserted)
    {
      nullable1 = apAdjust.CuryAdjdAmt;
      Decimal num = 0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        APInvoice current = this.Document.Current;
        // ISSUE: explicit non-virtual call
        if ((current != null ? (__nonvirtual (current.Rejected).GetValueOrDefault() ? 1 : 0) : 0) == 0)
          continue;
      }
      this.Adjustments.Cache.SetStatus((object) apAdjust, PXEntryStatus.InsertedDeleted);
    }
    foreach (APInvoiceEntry.APAdjust apAdjust in this.Adjustments.Cache.Updated)
    {
      Decimal? curyAdjdAmt = apAdjust.CuryAdjdAmt;
      Decimal num = 0M;
      if (!(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue))
      {
        APInvoice current = this.Document.Current;
        // ISSUE: explicit non-virtual call
        if ((current != null ? (__nonvirtual (current.Rejected).GetValueOrDefault() ? 1 : 0) : 0) == 0)
          continue;
      }
      this.Adjustments.Cache.SetStatus((object) apAdjust, PXEntryStatus.Deleted);
    }
    foreach (APInvoiceEntry.APAdjust apAdjust in this.Adjustments_Raw.Cache.Deleted)
    {
      PXCache cache = this.Adjustments_1.Cache;
      APInvoiceEntry.APAdjust2 apAdjust2 = new APInvoiceEntry.APAdjust2();
      apAdjust2.AdjgDocType = apAdjust.AdjgDocType;
      apAdjust2.AdjgRefNbr = apAdjust.AdjgRefNbr;
      apAdjust2.AdjdDocType = apAdjust.AdjdDocType;
      apAdjust2.AdjdRefNbr = apAdjust.AdjdRefNbr;
      apAdjust2.AdjdLineNbr = apAdjust.AdjdLineNbr;
      apAdjust2.AdjNbr = apAdjust.AdjNbr;
      this.Adjustments_1.Cache.Remove(cache.Locate((object) apAdjust2));
    }
    this.Adjustments.Cache.ClearQueryCache();
    foreach (APInvoice apInvoice in this.Document.Cache.Cached)
    {
      PXEntryStatus status = this.Document.Cache.GetStatus((object) apInvoice);
      bool? nullable2;
      if (status == PXEntryStatus.Deleted)
      {
        nullable2 = apInvoice.PendingPPD;
        if (nullable2.GetValueOrDefault() && apInvoice.DocType == "ADR")
          PXUpdate<Set<PX.Objects.AP.APAdjust.pPDVATAdjRefNbr, Null, Set<PX.Objects.AP.APAdjust.pPDVATAdjDocType, Null>>, APInvoiceEntry.APAdjust, Where<PX.Objects.AP.APAdjust.pendingPPD, Equal<True>, And<PX.Objects.AP.APAdjust.pPDVATAdjRefNbr, Equal<Required<PX.Objects.AP.APAdjust.pPDVATAdjRefNbr>>>>>.Update((PXGraph) this, (object) apInvoice.RefNbr);
      }
      if ((status == PXEntryStatus.Inserted || status == PXEntryStatus.Updated) && apInvoice.DocType == "INV")
      {
        nullable2 = apInvoice.Released;
        if (!nullable2.GetValueOrDefault())
        {
          nullable2 = apInvoice.Prebooked;
          if (!nullable2.GetValueOrDefault())
          {
            Decimal? nullable3 = new Decimal?(0M);
            foreach (APInvoiceEntry.APAdjust row in this.Adjustments_Raw.View.SelectMultiBound(new object[1]
            {
              (object) apInvoice
            }).RowCast<APInvoiceEntry.APAdjust>().Where<APInvoiceEntry.APAdjust>((Func<APInvoiceEntry.APAdjust, bool>) (adj => adj != null)))
            {
              Decimal? nullable4 = nullable3;
              Decimal? nullable5 = row.CuryAdjdAmt;
              nullable3 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
              nullable4 = apInvoice.CuryDocBal;
              Decimal? nullable6 = nullable3;
              nullable5 = nullable4.HasValue & nullable6.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
              Decimal num = 0M;
              if (nullable5.GetValueOrDefault() < num & nullable5.HasValue)
              {
                this.Adjustments.Cache.MarkUpdated((object) row);
                this.Adjustments.Cache.RaiseExceptionHandling<APInvoiceEntry.APAdjust.curyAdjdAmt>((object) row, (object) row.CuryAdjdAmt, (Exception) new PXSetPropertyException("Total application amount cannot exceed document amount."));
                throw new PXException("Total application amount cannot exceed document amount.");
              }
            }
          }
        }
      }
      if (status == PXEntryStatus.InsertedDeleted && apInvoice.DocType == "INV")
      {
        PXCache cach = this.Caches[typeof (RecognizedRecord)];
        foreach (RecognizedRecord recognizedRecord in cach.Cached)
        {
          Guid? documentLink = recognizedRecord.DocumentLink;
          Guid? noteId = apInvoice.NoteID;
          if ((documentLink.HasValue == noteId.HasValue ? (documentLink.HasValue ? (documentLink.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            cach.SetStatus((object) recognizedRecord, PXEntryStatus.InsertedDeleted);
        }
      }
    }
    this._discountEngine.ValidateDiscountDetails((PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails);
    this.InsertImportedTaxes();
    using (new APInvoicePersistBaseScope())
      base.Persist();
    APTran current1 = this.Transactions.Current;
    this.Transactions.Cache.Clear();
    this.Transactions.View.Clear();
    if (current1 == null)
      return;
    this.Transactions.Current = APTran.PK.Find((PXGraph) this, current1.TranType, current1.RefNbr, current1.LineNbr);
  }

  public virtual IEnumerable adjustments()
  {
    APInvoiceEntry graph = this;
    PX.Objects.CM.Extensions.CurrencyInfo inv_info = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APInvoice.curyInfoID>>>>.Config>.Select((PXGraph) graph);
    bool? nullable1;
    foreach (PXResult<APInvoiceEntry.APAdjust, APPayment, APRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in graph.SelectAdjustmentsRaw())
    {
      APPayment i1 = (APPayment) pxResult;
      APInvoiceEntry.APAdjust apAdjust1 = (APInvoiceEntry.APAdjust) pxResult;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult;
      Exception exception = (Exception) null;
      PXCache<APRegister>.RestoreCopy((APRegister) i1, (APRegister) (APRegisterAlias) pxResult);
      Decimal num1 = 0M;
      try
      {
        num1 = BalanceCalculation.CalculateApplicationDocumentBalance(currencyInfo, inv_info, i1.DocBal, i1.CuryDocBal);
      }
      catch (Exception ex)
      {
        exception = ex;
      }
      if (apAdjust1 != null)
      {
        nullable1 = apAdjust1.Released;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        {
          Decimal? curyAdjdAmt = apAdjust1.CuryAdjdAmt;
          Decimal num2 = num1;
          if (curyAdjdAmt.GetValueOrDefault() > num2 & curyAdjdAmt.HasValue)
          {
            apAdjust1.CuryDocBal = new Decimal?(num1);
            apAdjust1.CuryAdjdAmt = new Decimal?(0M);
          }
          else
          {
            APInvoiceEntry.APAdjust apAdjust2 = apAdjust1;
            Decimal num3 = num1;
            curyAdjdAmt = apAdjust1.CuryAdjdAmt;
            Decimal? nullable2 = curyAdjdAmt.HasValue ? new Decimal?(num3 - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
            apAdjust2.CuryDocBal = nullable2;
          }
        }
        else
          apAdjust1.CuryDocBal = new Decimal?(num1);
        apAdjust1.AdjType = "G";
        PXSelectorAttribute.StoreResult<PX.Objects.AP.APAdjust.displayRefNbr>(graph.Adjustments.Cache, (object) apAdjust1, (IBqlTable) (APRegisterAlias) pxResult);
        APInvoiceEntry.PopulateAdjDisplayFields((APPayment) pxResult, (APInvoiceEntry.APAdjust) pxResult);
        if (exception != null)
          graph.Caches<APInvoiceEntry.APAdjust>().RaiseExceptionHandling<APInvoiceEntry.APAdjust.curyDocBal>((object) apAdjust1, (object) 0M, exception);
      }
      if (apAdjust1 != null)
        yield return (object) new PXResult<APInvoiceEntry.APAdjust, APPayment, PX.Objects.CM.Extensions.CurrencyInfo>(apAdjust1, i1, currencyInfo);
    }
    APInvoice current = graph.Document.Current;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable1 = current.Released;
      num = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
    {
      foreach (PXResult<APInvoiceEntry.APAdjust, APRegisterAlias, APInvoice, PX.Objects.CM.Extensions.CurrencyInfo> copy in PXSelectBase<APInvoiceEntry.APAdjust, PXSelectJoin<APInvoiceEntry.APAdjust, InnerJoin<APRegisterAlias, On<APRegisterAlias.docType, Equal<APInvoiceEntry.APAdjust.adjdDocType>, And<APRegisterAlias.refNbr, Equal<APInvoiceEntry.APAdjust.adjdRefNbr>>>, InnerJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APInvoiceEntry.APAdjust.adjdDocType>, And<APInvoice.refNbr, Equal<APInvoiceEntry.APAdjust.adjdRefNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APRegisterAlias.curyInfoID>>>>>, Where<APInvoiceEntry.APAdjust.adjgDocType, Equal<Current<APInvoice.docType>>, And<APInvoiceEntry.APAdjust.adjgRefNbr, Equal<Current<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) graph))
      {
        APInvoiceEntry.APAdjust apAdjust = (APInvoiceEntry.APAdjust) copy;
        APInvoice i1 = (APInvoice) copy;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) copy;
        PXCache<APRegister>.RestoreCopy((APRegister) i1, (APRegister) (APRegisterAlias) copy);
        apAdjust.AdjType = "D";
        graph.Caches<APInvoiceEntry.APAdjust>().RaiseFieldUpdated<PX.Objects.AP.APAdjust.adjType>((object) apAdjust, (object) null);
        try
        {
          apAdjust.CuryDocBal = new Decimal?(BalanceCalculation.CalculateApplicationDocumentBalance(currencyInfo, inv_info, i1.DocBal, i1.CuryDocBal));
        }
        catch (Exception ex)
        {
          graph.Caches<APInvoiceEntry.APAdjust>().RaiseExceptionHandling<APInvoiceEntry.APAdjust.curyDocBal>((object) apAdjust, (object) 0M, ex);
        }
        yield return (object) new PXResult<APInvoiceEntry.APAdjust, APInvoice, PX.Objects.CM.Extensions.CurrencyInfo>(apAdjust, i1, currencyInfo);
      }
    }
    if (graph.Document.Current != null && (graph.Document.Current.DocType == "INV" || graph.Document.Current.DocType == "ACR"))
    {
      nullable1 = graph.Document.Current.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = graph.Document.Current.Prebooked;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = graph.Document.Current.Rejected;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = graph.Document.Current.Scheduled;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = graph.Document.Current.Voided;
              if (!nullable1.GetValueOrDefault())
              {
                using (new ReadOnlyScope(new PXCache[2]
                {
                  graph.Adjustments.Cache,
                  graph.Document.Cache
                }))
                {
                  using (new DisableSelectorValidationScope(graph.Adjustments.Cache, Array.Empty<System.Type>()))
                  {
                    using (new DisableFormulaCalculationScope(graph.Adjustments.Cache, new System.Type[7]
                    {
                      typeof (PX.Objects.AP.APAdjust.displayDocType),
                      typeof (PX.Objects.AP.APAdjust.displayRefNbr),
                      typeof (PX.Objects.AP.APAdjust.displayDocDate),
                      typeof (PX.Objects.AP.APAdjust.displayDocDesc),
                      typeof (PX.Objects.AP.APAdjust.displayCuryID),
                      typeof (PX.Objects.AP.APAdjust.displayFinPeriodID),
                      typeof (PX.Objects.AP.APAdjust.displayStatus)
                    }))
                    {
                      foreach (PXResult<APRegisterAlias, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.GL.Branch, APInvoiceEntry.APAdjust, APPayment> copy in PXSelectBase<APRegisterAlias, PXSelectJoin<APRegisterAlias, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APRegisterAlias.curyInfoID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<APRegisterAlias.branchID>, And<PX.Objects.GL.Branch.active, Equal<True>>>, LeftJoin<APInvoiceEntry.APAdjust, On<APInvoiceEntry.APAdjust.adjgDocType, Equal<APRegisterAlias.docType>, And<APInvoiceEntry.APAdjust.adjgRefNbr, Equal<APRegisterAlias.refNbr>, And<APInvoiceEntry.APAdjust.released, NotEqual<True>, PX.Data.And<Where<APInvoiceEntry.APAdjust.adjdDocType, NotEqual<Current<APInvoice.docType>>, Or<APInvoiceEntry.APAdjust.adjdRefNbr, NotEqual<Current<APInvoice.refNbr>>>>>>>>, InnerJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegisterAlias.docType>, And<APPayment.refNbr, Equal<APRegisterAlias.refNbr>>>>>>>, Where<APRegisterAlias.vendorID, Equal<Current<APInvoice.vendorID>>, And2<Where<APRegisterAlias.docType, Equal<APDocType.prepayment>, Or<APRegisterAlias.docType, Equal<APDocType.debitAdj>>>, And2<Where<APRegisterAlias.docDate, LessEqual<Current<APInvoice.docDate>>, And<APRegisterAlias.tranPeriodID, LessEqual<Current<APRegister.tranPeriodID>>, And<APRegisterAlias.released, Equal<True>, And<APRegisterAlias.openDoc, Equal<True>, And<APRegisterAlias.paymentsByLinesAllowed, Equal<False>, And<APInvoiceEntry.APAdjust.adjdRefNbr, PX.Data.IsNull>>>>>>, And<APRegisterAlias.hold, NotEqual<True>, PX.Data.And<Not<HasUnreleasedVoidPayment<APPayment.docType, APPayment.refNbr>>>>>>>>.Config>.Select((PXGraph) graph))
                      {
                        APPayment apPayment = (APPayment) copy;
                        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) copy;
                        PXCache<APRegister>.RestoreCopy((APRegister) apPayment, (APRegister) (APRegisterAlias) copy);
                        APInvoiceEntry.APAdjust apAdjust3 = new APInvoiceEntry.APAdjust();
                        apAdjust3.VendorID = graph.Document.Current.VendorID;
                        apAdjust3.AdjdDocType = graph.Document.Current.DocType;
                        apAdjust3.AdjdRefNbr = graph.Document.Current.RefNbr;
                        apAdjust3.AdjdBranchID = graph.Document.Current.BranchID;
                        apAdjust3.AdjgDocType = apPayment.DocType;
                        apAdjust3.AdjgRefNbr = apPayment.RefNbr;
                        apAdjust3.AdjgBranchID = apPayment.BranchID;
                        apAdjust3.AdjNbr = apPayment.AdjCntr;
                        apAdjust3.InvoiceID = graph.Document.Current.NoteID;
                        apAdjust3.PaymentID = apPayment.DocType != "ADR" ? apPayment.NoteID : new Guid?();
                        apAdjust3.MemoID = apPayment.DocType == "ADR" ? apPayment.NoteID : new Guid?();
                        APInvoiceEntry.APAdjust adj = apAdjust3;
                        if (graph.Adjustments.Cache.Locate((object) adj) == null)
                        {
                          adj.AdjgCuryInfoID = apPayment.CuryInfoID;
                          adj.AdjdOrigCuryInfoID = graph.Document.Current.CuryInfoID;
                          adj.AdjdCuryInfoID = graph.Document.Current.CuryInfoID;
                          adj.AdjType = "G";
                          Exception exception = (Exception) null;
                          try
                          {
                            adj.CuryDocBal = new Decimal?(BalanceCalculation.CalculateApplicationDocumentBalance(currencyInfo, inv_info, apPayment.DocBal, apPayment.CuryDocBal));
                          }
                          catch (Exception ex)
                          {
                            exception = ex;
                          }
                          APInvoiceEntry.PopulateAdjDisplayFields(apPayment, adj);
                          APInvoiceEntry.APAdjust apAdjust4 = graph.Adjustments.Insert(adj);
                          if (exception != null)
                            graph.Caches<APInvoiceEntry.APAdjust>().RaiseExceptionHandling<APInvoiceEntry.APAdjust.curyDocBal>((object) apAdjust4, (object) 0M, exception);
                          if (apAdjust4 != null)
                            yield return (object) new PXResult<APInvoiceEntry.APAdjust, APPayment, PX.Objects.CM.Extensions.CurrencyInfo>(apAdjust4, apPayment, currencyInfo);
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  public virtual IEnumerable adjustments_1()
  {
    APInvoiceEntry graph1 = this;
    PX.Objects.CM.Extensions.CurrencyInfo inv_info = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APInvoice.curyInfoID>>>>.Config>.Select((PXGraph) graph1);
    APInvoiceEntry graph2 = graph1;
    Dictionary<System.Type, System.Type> fieldMap = new Dictionary<System.Type, System.Type>();
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayDocType), typeof (APRegisterAlias.docType));
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayRefNbr), typeof (APRegisterAlias.refNbr));
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayDocDate), typeof (APRegisterAlias.docDate));
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayDocDesc), typeof (APRegisterAlias.docDesc));
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayCuryID), typeof (APRegisterAlias.curyID));
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayFinPeriodID), typeof (APRegisterAlias.finPeriodID));
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayStatus), typeof (APRegisterAlias.status));
    fieldMap.Add(typeof (PX.Objects.AP.APAdjust.displayCuryInfoID), typeof (APRegisterAlias.curyInfoID));
    System.Type[] typeArray = new System.Type[2]
    {
      typeof (APInvoiceEntry.APAdjust2),
      typeof (APInvoice)
    };
    PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) graph2, fieldMap, typeArray);
    pxResultMapper.CreateDelegateResult();
    APInvoice current1 = graph1.Document.Current;
    if ((current1 != null ? (!current1.NoteID.HasValue ? 1 : 0) : 1) == 0)
    {
      APInvoice current2 = graph1.Document.Current;
      if ((current2 != null ? (!current2.VendorID.HasValue ? 1 : 0) : 1) == 0)
      {
        PXSelect<APInvoiceEntry.APAdjust2> pxSelect = new PXSelect<APInvoiceEntry.APAdjust2>((PXGraph) graph1);
        if (graph1.Document.Current.DocType == "ADR")
        {
          pxSelect.Join<LeftJoin<APRegisterAlias, On<APInvoiceEntry.APAdjust2.memoID, Equal<BqlField<APInvoice.noteID, IBqlGuid>.FromCurrent>, PX.Data.And<Where<APRegisterAlias.noteID, Equal<IsNull<APInvoiceEntry.APAdjust2.invoiceID, APInvoiceEntry.APAdjust2.paymentID>>>>>>>();
          pxSelect.WhereNew<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APInvoice.docType>, Equal<APDocType.debitAdj>>>>>.And<BqlOperand<APInvoiceEntry.APAdjust2.memoID, IBqlGuid>.IsEqual<BqlField<APInvoice.noteID, IBqlGuid>.FromCurrent>>>>();
        }
        else
        {
          pxSelect.Join<LeftJoin<APRegisterAlias, On<APRegisterAlias.noteID, Equal<APInvoiceEntry.APAdjust2.memoID>, And<BqlField<APInvoice.docType, IBqlString>.FromCurrent, Equal<APDocType.invoice>, Or<APRegisterAlias.noteID, Equal<IsNull<APInvoiceEntry.APAdjust2.invoiceID, APInvoiceEntry.APAdjust2.paymentID>>>>>>>();
          pxSelect.WhereNew<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APInvoice.docType>, NotEqual<APDocType.debitAdj>>>>, PX.Data.And<PX.Data.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<Current<APInvoice.released>, Equal<True>>>>, PX.Data.Or<BqlOperand<Current<APInvoice.prebooked>, IBqlBool>.IsEqual<True>>>>.Or<BqlOperand<APInvoiceEntry.APAdjust2.released, IBqlBool>.IsEqual<BqlField<APInvoice.released, IBqlBool>.FromCurrent>>>>>, PX.Data.And<BqlOperand<Current<PX.Objects.AP.APAdjust.isInitialApplication>, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<APInvoiceEntry.APAdjust2.invoiceID, IBqlGuid>.IsEqual<BqlField<APInvoice.noteID, IBqlGuid>.FromCurrent>>>>();
        }
        pxSelect.Join<LeftJoinSingleTable<APInvoice, On<APInvoice.docType, Equal<APRegisterAlias.docType>, And<APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>, LeftJoinSingleTable<APPayment, On<APPayment.docType, Equal<APRegisterAlias.docType>, And<APPayment.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APRegisterAlias.curyInfoID>>>>>>();
        foreach (PXResult<APInvoiceEntry.APAdjust2, APRegisterAlias, APInvoice, APPayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in pxResultMapper.Select(pxSelect.View))
        {
          APPayment i1 = (APPayment) pxResult;
          APInvoiceEntry.APAdjust2 apAdjust2_1 = (APInvoiceEntry.APAdjust2) pxResult;
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult;
          Exception exception = (Exception) null;
          PXCache<APRegister>.RestoreCopy((APRegister) i1, (APRegister) (APRegisterAlias) pxResult);
          Decimal num1 = 0M;
          try
          {
            num1 = BalanceCalculation.CalculateApplicationDocumentBalance(currencyInfo, inv_info, i1.DocBal, i1.CuryDocBal);
          }
          catch (Exception ex)
          {
            exception = ex;
          }
          if (apAdjust2_1 != null)
          {
            bool? released = apAdjust2_1.Released;
            bool flag = false;
            if (released.GetValueOrDefault() == flag & released.HasValue)
            {
              Decimal? curyAdjdAmt = apAdjust2_1.CuryAdjdAmt;
              Decimal num2 = num1;
              if (curyAdjdAmt.GetValueOrDefault() > num2 & curyAdjdAmt.HasValue)
              {
                apAdjust2_1.CuryDocBal = new Decimal?(num1);
                apAdjust2_1.CuryAdjdAmt = new Decimal?(0M);
              }
              else
              {
                APInvoiceEntry.APAdjust2 apAdjust2_2 = apAdjust2_1;
                Decimal num3 = num1;
                curyAdjdAmt = apAdjust2_1.CuryAdjdAmt;
                Decimal? nullable = curyAdjdAmt.HasValue ? new Decimal?(num3 - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
                apAdjust2_2.CuryDocBal = nullable;
              }
            }
            else
              apAdjust2_1.CuryDocBal = new Decimal?(num1);
            apAdjust2_1.AdjType = "G";
            PXSelectorAttribute.StoreResult<PX.Objects.AP.APAdjust.displayRefNbr>(graph1.Adjustments.Cache, (object) apAdjust2_1, (IBqlTable) (APRegisterAlias) pxResult);
            apAdjust2_1.DisplayDocType = i1.DocType;
            apAdjust2_1.DisplayRefNbr = i1.RefNbr;
            apAdjust2_1.DisplayDocDate = i1.DocDate;
            apAdjust2_1.DisplayDocDesc = i1.DocDesc;
            apAdjust2_1.DisplayCuryID = i1.CuryID;
            apAdjust2_1.DisplayFinPeriodID = i1.FinPeriodID;
            apAdjust2_1.DisplayStatus = i1.Status;
            if (exception != null)
              graph1.Caches<APInvoiceEntry.APAdjust2>().RaiseExceptionHandling<APInvoiceEntry.APAdjust2.curyDocBal>((object) apAdjust2_1, (object) 0M, exception);
          }
          if (apAdjust2_1 != null)
            yield return (object) new PXResult<APInvoiceEntry.APAdjust2, APPayment, PX.Objects.CM.Extensions.CurrencyInfo>(apAdjust2_1, i1, currencyInfo);
        }
      }
    }
  }

  private static void PopulateAdjDisplayFields(APPayment payment, APInvoiceEntry.APAdjust adj)
  {
    adj.DisplayDocType = payment.DocType;
    adj.DisplayRefNbr = payment.RefNbr;
    adj.DisplayDocDate = payment.DocDate;
    adj.DisplayDocDesc = payment.DocDesc;
    adj.DisplayCuryID = payment.CuryID;
    adj.DisplayFinPeriodID = payment.FinPeriodID;
    adj.DisplayStatus = payment.Status;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [APDocStatus.List]
  protected virtual void APInvoice_Status_CacheAttached(PXCache sender)
  {
  }

  protected virtual void APInvoice_DocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "INV";
  }

  protected virtual void APInvoice_APAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(this.location.View.SelectSingleBound(new object[1]
    {
      e.Row
    }) is PX.Objects.CR.Location data) || e.Row == null)
      return;
    e.NewValue = (object) null;
    if (((APRegister) e.Row).DocType == "PPM")
      e.NewValue = this.GetAcctSub<Vendor.prepaymentAcctID>(this.vendor.Cache, (object) this.vendor.Current);
    if (!string.IsNullOrEmpty((string) e.NewValue))
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aPAccountID>(this.location.Cache, (object) data);
  }

  protected virtual void APInvoice_APSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(this.location.View.SelectSingleBound(new object[1]
    {
      e.Row
    }) is PX.Objects.CR.Location data) || e.Row == null)
      return;
    e.NewValue = (object) null;
    if (((APRegister) e.Row).DocType == "PPM")
      e.NewValue = this.GetAcctSub<Vendor.prepaymentSubID>(this.vendor.Cache, (object) this.vendor.Current);
    if (!string.IsNullOrEmpty((string) e.NewValue))
      return;
    e.NewValue = this.GetAcctSub<PX.Objects.CR.Location.aPSubID>(this.location.Cache, (object) data);
  }

  protected virtual void APInvoice_PayLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<APInvoice.separateCheck>(e.Row);
    sender.SetDefaultExt<APInvoice.payTypeID>(e.Row);
  }

  protected virtual void APInvoice_PaymentsByLinesAllowed_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APInvoice row) || !((bool?) e.OldValue).GetValueOrDefault() || row.PaymentsByLinesAllowed.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<APInvoice.curyDiscTot>((object) row, (object) row.CuryDiscTot, (Exception) null);
    sender.RaiseExceptionHandling<APInvoice.curyOrderDiscTotal>((object) row, (object) row.CuryOrderDiscTotal, (Exception) null);
    sender.RaiseExceptionHandling<APInvoice.curyTaxTotal>((object) row, (object) row.CuryTaxTotal, (Exception) null);
    sender.RaiseExceptionHandling<APInvoice.curyOrigWhTaxAmt>((object) row, (object) row.CuryOrigWhTaxAmt, (Exception) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APInvoice, APInvoice.paymentsByLinesAllowed> e)
  {
    if (!(e.NewValue as bool?).GetValueOrDefault() || e.Row == null || !this.HasWithholdingTax(e.Cache, e.Row))
      return;
    e.Cancel = true;
    e.NewValue = (object) false;
    this.Document.Cache.RaiseExceptionHandling<APInvoice.paymentsByLinesAllowed>((object) e.Row, e.NewValue, (Exception) new PXSetPropertyException<APInvoice.paymentsByLinesAllowed>("The Pay by Line check box cannot be selected because the document has withholding taxes."));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<APInvoice, APInvoice.retainageApply> e)
  {
    APInvoice row = e.Row;
    if (row == null)
      return;
    bool? nullable = (bool?) e.OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.RetainageApply;
    if (nullable.GetValueOrDefault())
      return;
    e.Cache.RaiseExceptionHandling<APInvoice.curyDiscTot>((object) row, (object) row.CuryDiscTot, (Exception) null);
    e.Cache.RaiseExceptionHandling<APInvoice.curyOrderDiscTotal>((object) row, (object) row.CuryOrderDiscTotal, (Exception) null);
  }

  protected virtual void APInvoice_CuryDocBal_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APInvoice row1) || !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentsByLines>() || FlaggedModeScopeBase<APInvoicePersistBaseScope>.IsActive)
      return;
    PXEntryStatus status = this.Document.Cache.GetStatus((object) row1);
    if (FlaggedKeyModeScopeBase<APInvoiceEntry.SkipUpdAdjustments>.IsActive(this.Document.Current?.DocType + this.Document.Current?.RefNbr) || status != PXEntryStatus.Inserted && status != PXEntryStatus.Updated)
      return;
    bool? nullable1 = row1.PaymentsByLinesAllowed;
    if (!nullable1.GetValueOrDefault() || !(row1.DocType == "INV"))
      return;
    nullable1 = row1.Released;
    if (nullable1.GetValueOrDefault())
      return;
    nullable1 = row1.Prebooked;
    if (nullable1.GetValueOrDefault())
      return;
    bool flag = false;
    foreach (APInvoiceEntry.APAdjust row2 in this.Adjustments.View.SelectMultiBound(new object[1]
    {
      (object) row1
    }).RowCast<APInvoiceEntry.APAdjust>().Where<APInvoiceEntry.APAdjust>((Func<APInvoiceEntry.APAdjust, bool>) (adj =>
    {
      if (adj == null)
        return false;
      Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
      Decimal num = 0M;
      return !(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue);
    })))
    {
      APInvoiceEntry.APAdjust apAdjust = row2;
      Decimal valueOrDefault1 = row2.CuryAdjdAmt.GetValueOrDefault();
      Decimal? curyDocBal = row2.CuryDocBal;
      Decimal valueOrDefault2 = curyDocBal.GetValueOrDefault();
      Decimal val1 = valueOrDefault1 + valueOrDefault2;
      curyDocBal = row1.CuryDocBal;
      Decimal valueOrDefault3 = curyDocBal.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(System.Math.Min(val1, valueOrDefault3));
      apAdjust.CuryAdjdAmt = nullable2;
      this.Adjustments.Cache.MarkUpdated((object) row2);
      flag = true;
    }
    if (!flag)
      return;
    this.RecalculateApplicationsAmounts(this.GetExtension<APInvoiceEntry.MultiCurrency>());
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APInvoice, APInvoice.curyOrigDiscAmt> e)
  {
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() < num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected virtual void _(PX.Data.Events.RowUpdating<APInvoice> e)
  {
    if (e.Row == null || !(e.Row.FinPeriodID != e.NewRow.FinPeriodID) || !e.Row.IsRetainageDocument.GetValueOrDefault())
      return;
    APInvoice apInvoice = (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, (object) e.Row.OrigDocType, (object) e.Row.OrigRefNbr);
    if (apInvoice.FinPeriodID.CompareTo(e.NewRow.FinPeriodID) <= 0)
      return;
    e.Cache.RaiseExceptionHandling<APInvoice.finPeriodID>((object) e.NewRow, (object) PeriodIDAttribute.FormatForDisplay(e.NewRow.FinPeriodID), (Exception) new PXSetPropertyException("The retainage document cannot be posted to the period earlier than the post period of the related original document with retainage. The value must be greater than or equal to {0}.", PXErrorLevel.Error, new object[1]
    {
      (object) PeriodIDAttribute.FormatForError(apInvoice.FinPeriodID)
    }));
    e.Cancel = true;
  }

  protected virtual void InsertImportedTaxes()
  {
  }

  protected virtual void APInvoice_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.IsVendorIDUpdated = true;
    APInvoice row = (APInvoice) e.Row;
    this.vendor.RaiseFieldUpdated(sender, e.Row);
    this.Adjustments_Raw.Cache.Clear();
    this.Adjustments_Raw.Cache.ClearQueryCache();
    this.Adjustments_1.Cache.Clear();
    this.Adjustments_1.Cache.ClearQueryCache();
    EnumerableExtensions.ForEach<APInvoiceEntry.APAdjust>(PXSelectBase<APInvoiceEntry.APAdjust, PXSelect<APInvoiceEntry.APAdjust, Where<APInvoiceEntry.APAdjust.adjdDocType, Equal<Required<APInvoice.docType>>, And<APInvoiceEntry.APAdjust.adjdRefNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, (object) row.DocType, (object) row.RefNbr).RowCast<APInvoiceEntry.APAdjust>(), (System.Action<APInvoiceEntry.APAdjust>) (application => this.Adjustments.Cache.Delete((object) application)));
    EnumerableExtensions.ForEach<APInvoiceEntry.APAdjust2>(PXSelectBase<APInvoiceEntry.APAdjust2, PXSelect<APInvoiceEntry.APAdjust2, Where<APInvoiceEntry.APAdjust2.adjdDocType, Equal<Required<APInvoice.docType>>, And<APInvoiceEntry.APAdjust2.adjdRefNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, (object) row.DocType, (object) row.RefNbr).RowCast<APInvoiceEntry.APAdjust2>(), (System.Action<APInvoiceEntry.APAdjust2>) (application => this.Adjustments_1.Cache.Delete((object) application)));
  }

  protected virtual void APInvoice_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.location.RaiseFieldUpdated(sender, e.Row);
    try
    {
      if (!this.IsVendorIDUpdated && !e.ExternalCall)
        return;
      this.SetDefaultsAfterVendorIDChanging(sender, (APInvoice) e.Row);
    }
    finally
    {
      this.IsVendorIDUpdated = false;
    }
  }

  private void SetDefaultsAfterVendorIDChanging(PXCache sender, APInvoice row)
  {
    sender.SetDefaultExt<APInvoice.aPAccountID>((object) row);
    sender.SetDefaultExt<APInvoice.aPSubID>((object) row);
    sender.SetValue<APInvoice.payLocationID>((object) row, sender.GetValue<APInvoice.vendorLocationID>((object) row));
    sender.SetDefaultExt<APInvoice.payTypeID>((object) row);
    sender.SetDefaultExt<APInvoice.separateCheck>((object) row);
    sender.SetDefaultExt<APInvoice.taxCalcMode>((object) row);
    sender.SetDefaultExt<APInvoice.taxZoneID>((object) row);
    sender.SetDefaultExt<APInvoice.prebookAcctID>((object) row);
    sender.SetDefaultExt<APInvoice.prebookSubID>((object) row);
    sender.SetDefaultExt<APInvoice.paymentsByLinesAllowed>((object) row);
  }

  protected virtual bool IsAllowWithholdingTax(PXCache sender, APInvoice doc)
  {
    return !(doc.DocType == "PPM") || !this.HasWithholdingTax(sender, doc);
  }

  protected virtual bool HasWithholdingTax(PXCache sender, APInvoice doc)
  {
    return (APTaxTran) PXSelectBase<APTaxTran, PXSelectJoin<APTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APInvoice.docType>>, And<APTaxTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) doc
    }) != null;
  }

  protected virtual bool HasTermsWithMultipleInstallments(PXCache sender, APInvoice doc)
  {
    return (PX.Objects.CS.Terms) PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Current<APInvoice.termsID>>, And<PX.Objects.CS.Terms.installmentType, Equal<TermsInstallmentType.multiple>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) doc
    }) != null;
  }

  protected virtual void APInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
    if (!this.IsAllowWithholdingTax(sender, row))
      throw new PXRowPersistingException("APInvoice", (object) row, "The document cannot be saved because withholding taxes in prepayment requests are not supported.");
    if (this.HasTermsWithMultipleInstallments(sender, row) && this.HasWithholdingTax(sender, row))
      throw new PXRowPersistingException("APInvoice", (object) row, "Withholding taxes are not supported in documents that have credit terms with the Multiple installment type.");
    Decimal? nullable1;
    if (this.ShouldPerformBalanceVerification(row))
    {
      Decimal? curyDocBal = row.CuryDocBal;
      nullable1 = row.CuryOrigDocAmt;
      if (!(curyDocBal.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyDocBal.HasValue == nullable1.HasValue))
        sender.RaiseExceptionHandling<APInvoice.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
    }
    if (row.DocType != "ADR" && row.DocType != "PPM" && string.IsNullOrEmpty(row.TermsID))
      sender.RaiseExceptionHandling<APInvoice.termsID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<APInvoice.termsID>(this.Document.Cache, (object) row);
    if (this.vendor.Current != null && this.vendor.Current.Vendor1099.Value && terms != null && terms.InstallmentType == "M")
      sender.RaiseExceptionHandling<APInvoice.termsID>((object) row, (object) row.TermsID, (Exception) new PXSetPropertyException("Multiple Installments are not allowed for 1099 Vendors."));
    if ((PX.Objects.EP.EPEmployee) this.EmployeeByVendor.Select() != null && terms != null)
    {
      if (!PX.Objects.CM.PXCurrencyAttribute.IsNullOrEmpty(terms.DiscPercent))
        sender.RaiseExceptionHandling<APInvoice.termsID>((object) row, (object) row.TermsID, (Exception) new PXSetPropertyException("Terms discounts are not allowed for Employees."));
      if (terms.InstallmentType == "M")
        sender.RaiseExceptionHandling<APInvoice.termsID>(e.Row, (object) row.TermsID, (Exception) new PXSetPropertyException("Multiple Installments are not allowed for Employees."));
    }
    System.DateTime? nullable2;
    if (row.DocType != "ADR" || row.DocType == "ADR" && row.TermsID != null)
    {
      nullable2 = row.DueDate;
      if (!nullable2.HasValue)
        sender.RaiseExceptionHandling<APInvoice.dueDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
    }
    bool? nullable3;
    if (row.DocType != "ADR")
    {
      nullable3 = row.PaySel;
      if (nullable3.GetValueOrDefault())
      {
        nullable2 = row.PayDate;
        if (!nullable2.HasValue)
          sender.RaiseExceptionHandling<APInvoice.payDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
      }
    }
    if (row.DocType != "ADR")
    {
      nullable3 = row.PaySel;
      if (nullable3.GetValueOrDefault())
      {
        nullable2 = row.PayDate;
        if (nullable2.HasValue)
        {
          nullable2 = row.DocDate;
          System.DateTime dateTime1 = nullable2.Value;
          ref System.DateTime local = ref dateTime1;
          nullable2 = row.PayDate;
          System.DateTime dateTime2 = nullable2.Value;
          if (local.CompareTo(dateTime2) > 0)
            sender.RaiseExceptionHandling<APInvoice.payDate>(e.Row, (object) row.PayDate, (Exception) new PXSetPropertyException("{0} cannot be less than Document Date.", PXErrorLevel.RowError));
        }
      }
    }
    int? nullable4;
    if (row.DocType != "ADR")
    {
      nullable3 = row.PaySel;
      if (nullable3.GetValueOrDefault())
      {
        nullable4 = row.PayLocationID;
        if (!nullable4.HasValue)
          sender.RaiseExceptionHandling<APInvoice.payLocationID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
      }
    }
    if (row.DocType != "ADR")
    {
      nullable3 = row.PaySel;
      if (nullable3.GetValueOrDefault())
      {
        nullable4 = row.PayAccountID;
        if (!nullable4.HasValue)
          sender.RaiseExceptionHandling<APInvoice.payAccountID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
      }
    }
    if (row.DocType != "ADR")
    {
      nullable3 = row.PaySel;
      if (nullable3.GetValueOrDefault() && row.PayTypeID == null)
        sender.RaiseExceptionHandling<APInvoice.payTypeID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
    }
    if (row.DocType == "PPM")
    {
      nullable3 = row.PaySel;
      if (nullable3.GetValueOrDefault())
      {
        nullable4 = row.PayAccountID;
        if (nullable4.HasValue)
        {
          object payAccountId = (object) row.PayAccountID;
          try
          {
            sender.RaiseFieldVerifying<APInvoice.payAccountID>((object) row, ref payAccountId);
          }
          catch (PXSetPropertyException ex)
          {
            sender.RaiseExceptionHandling<APInvoice.payAccountID>((object) row, payAccountId, (Exception) ex);
          }
        }
      }
    }
    if ((row.DocType != "ADR" || row.DocType == "ADR" && row.TermsID != null) && row.DocType != "PPM")
    {
      nullable2 = row.DiscDate;
      if (!nullable2.HasValue)
        sender.RaiseExceptionHandling<APInvoice.discDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
    }
    if (string.IsNullOrEmpty(row.InvoiceNbr) && this.IsInvoiceNbrRequired(row))
      sender.RaiseExceptionHandling<APInvoice.invoiceNbr>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
    if (row.DocType == "PPM")
    {
      nullable3 = row.OpenDoc;
      if (nullable3.GetValueOrDefault())
      {
        nullable3 = row.Voided;
        if (nullable3.GetValueOrDefault())
        {
          row.OpenDoc = new bool?(false);
          row.ClosedDate = row.DocDate;
          row.ClosedFinPeriodID = row.FinPeriodID;
          row.ClosedTranPeriodID = row.TranPeriodID;
        }
      }
    }
    nullable1 = row.CuryDiscTot;
    Decimal num1 = System.Math.Abs(row.CuryLineTotal.GetValueOrDefault());
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      sender.RaiseExceptionHandling<APInvoice.curyDiscTot>(e.Row, (object) row.CuryDiscTot, (Exception) new PXSetPropertyException("The total amount of line and document discounts cannot exceed the Detail Total amount.", PXErrorLevel.Error));
    this.ValidateAPAndReclassificationAccountsAndSubs(sender, row);
    this.IsVendorIDUpdated = false;
    nullable1 = row.CuryOrigDiscAmt;
    Decimal num2 = 0M;
    if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
    {
      nullable3 = row.PaymentsByLinesAllowed;
      if (!nullable3.GetValueOrDefault())
      {
        nullable3 = row.RetainageApply;
        if (!nullable3.GetValueOrDefault())
          goto label_68;
      }
      foreach (PXResult<APTaxTran, PX.Objects.TX.Tax> pxResult in this.Taxes.Select())
      {
        PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
        APTaxTran apTaxTran = (APTaxTran) pxResult;
        if (tax.TaxApplyTermsDisc == "P")
        {
          sender.RaiseExceptionHandling<APInvoice.curyOrigDiscAmt>(e.Row, (object) row.CuryOrigDiscAmt, (Exception) new PXSetPropertyException("VAT recalculated on cash discounts is not supported in documents with retainage or documents paid by lines.", PXErrorLevel.Error));
          break;
        }
      }
    }
label_68:
    nullable1 = row.CuryDiscTot;
    num2 = 0.0M;
    if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
      return;
    if (this.Transactions.Select().Where<PXResult<APTran>>((Expression<Func<PXResult<APTran>, bool>>) (a => ((APTran) a).IsDirectTaxLine != (bool?) true)).Count<PXResult<APTran>>() != 0)
      return;
    sender.RaiseExceptionHandling<APInvoice.curyDiscTot>(e.Row, (object) row.CuryDiscTot, (Exception) new PXSetPropertyException("Discounts cannot be applied to a document line with a direct-entry tax.", PXErrorLevel.Error));
  }

  protected virtual void APInvoice_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
    if (row == null || !(row.DocType == "PPM") || e.Operation != PXDBOperation.Insert || e.TranStatus != PXTranStatus.Open)
      return;
    if ((APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.docType, Equal<APDocType.check>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, (object) row.RefNbr) == null)
      return;
    Numbering numbering = (Numbering) PXSelectorAttribute.Select<PX.Objects.AP.APSetup.invoiceNumberingID>(this.Caches[typeof (PX.Objects.AP.APSetup)], this.Caches[typeof (PX.Objects.AP.APSetup)].Current);
    if ((numbering != null ? (numbering.UserNumbering.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXRowPersistedException(typeof (APPayment.refNbr).Name, (object) row.RefNbr, "{0} with reference number {1} already exists. Enter another reference number.", new object[2]
      {
        (object) "Payment",
        (object) row.RefNbr
      });
    throw new PXLockViolationException(typeof (APInvoice), PXDBOperation.Insert, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    });
  }

  protected virtual bool IsInvoiceNbrRequired(APInvoice doc)
  {
    if (this.APSetup.Current.RequireVendorRef.GetValueOrDefault() && doc.DocType != "ADR" && doc.DocType != "ACR" && doc.DocType != "PPM")
    {
      if (this.vendor.Current != null)
      {
        bool? taxAgency = this.vendor.Current.TaxAgency;
        bool flag = false;
        if (!(taxAgency.GetValueOrDefault() == flag & taxAgency.HasValue))
          goto label_6;
      }
      return doc.OrigDocType == null && doc.OrigRefNbr == null || doc.IsChildRetainageDocument();
    }
label_6:
    return false;
  }

  protected virtual void APInvoice_DocDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
    if (!(row.DocType == "PPM"))
      return;
    sender.SetDefaultExt<APInvoice.dueDate>((object) row);
  }

  protected virtual void APInvoice_DocDesc_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
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
    foreach (PXResult<APTaxTran> pxResult in this.Taxes.Select())
    {
      APTaxTran apTaxTran = (APTaxTran) pxResult;
      apTaxTran.Description = row.DocDesc;
      this.Taxes.Cache.Update((object) apTaxTran);
    }
  }

  protected virtual void APInvoice_DueDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
    if (!(row.DocType == "PPM"))
      return;
    e.NewValue = (object) row.DocDate;
  }

  protected virtual void APInvoice_TermsID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<APInvoice.termsID>(sender, e.Row);
    APInvoice row = e.Row as APInvoice;
    if (terms != null && terms.InstallmentType != "S")
    {
      foreach (PXResult<APInvoiceEntry.APAdjust> pxResult in this.Adjustments.Select())
        this.Adjustments.Cache.Delete((object) (APInvoiceEntry.APAdjust) pxResult);
    }
    APInvoiceState documentState = this.GetDocumentState(sender, row);
    if (!this.apsetup.Current.TermsInDebitAdjustments.GetValueOrDefault() || !documentState.IsDocumentDebitAdjustment || terms != null)
      return;
    row.CuryOrigDiscAmt = new Decimal?(0M);
  }

  public virtual APInvoiceState GetDocumentState(PXCache cache, APInvoice document)
  {
    if (cache == null)
      throw new PXArgumentException(nameof (cache));
    APInvoiceState documentState = document != null ? new APInvoiceState()
    {
      IsFromExpenseClaims = document.OrigModule == "EP"
    } : throw new PXArgumentException(nameof (document));
    documentState.DontApprove = documentState.IsFromExpenseClaims || document.InstallmentNbr.HasValue || document.OrigRefNbr == null && document.IsTaxDocument.GetValueOrDefault() || !this.IsApprovalRequired(document);
    documentState.HasPOLink = IsPOLinkedAPBill.Ensure(cache, document);
    APInvoiceState apInvoiceState1 = documentState;
    bool? nullable = document.PaymentsByLinesAllowed;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    apInvoiceState1.PaymentsByLinesAllowed = num1 != 0;
    documentState.IsDocumentPrepayment = document.DocType == "PPM";
    documentState.IsDocumentInvoice = document.DocType == "INV";
    documentState.IsDocumentPrepaymentInvoice = document.DocType == "PPI";
    documentState.IsDocumentDebitAdjustment = document.DocType == "ADR";
    documentState.IsDocumentCreditAdjustment = document.DocType == "ACR";
    APInvoiceState apInvoiceState2 = documentState;
    nullable = document.Hold;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    apInvoiceState2.IsDocumentOnHold = num2 != 0;
    APInvoiceState apInvoiceState3 = documentState;
    nullable = document.Scheduled;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    apInvoiceState3.IsDocumentScheduled = num3 != 0;
    APInvoiceState apInvoiceState4 = documentState;
    nullable = document.Prebooked;
    int num4;
    if (nullable.GetValueOrDefault())
    {
      nullable = document.Released;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = document.Voided;
        bool flag2 = false;
        num4 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
        goto label_8;
      }
    }
    num4 = 0;
label_8:
    apInvoiceState4.IsDocumentPrebookedNotCompleted = num4 != 0;
    APInvoiceState apInvoiceState5 = documentState;
    nullable = document.Released;
    int num5;
    if (!nullable.GetValueOrDefault())
    {
      nullable = document.Prebooked;
      num5 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 1;
    apInvoiceState5.IsDocumentReleasedOrPrebooked = num5 != 0;
    APInvoiceState apInvoiceState6 = documentState;
    nullable = document.Voided;
    int num6 = nullable.GetValueOrDefault() ? 1 : 0;
    apInvoiceState6.IsDocumentVoided = num6 != 0;
    APInvoiceState apInvoiceState7 = documentState;
    nullable = document.Rejected;
    int num7 = nullable.GetValueOrDefault() ? 1 : 0;
    apInvoiceState7.IsDocumentRejected = num7 != 0;
    APInvoiceState apInvoiceState8 = documentState;
    nullable = document.RetainageApply;
    int num8 = nullable.GetValueOrDefault() ? 1 : 0;
    apInvoiceState8.RetainageApply = num8 != 0;
    APInvoiceState apInvoiceState9 = documentState;
    nullable = document.IsRetainageDocument;
    int num9 = nullable.GetValueOrDefault() ? 1 : 0;
    apInvoiceState9.IsRetainageDocument = num9 != 0;
    APInvoiceState apInvoiceState10 = documentState;
    int num10;
    if (document.IsOriginalRetainageDocument() || document.IsChildRetainageDocument())
    {
      nullable = document.IsRetainageReversing;
      num10 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num10 = 0;
    apInvoiceState10.IsRetainageReversing = num10 != 0;
    documentState.IsPrepaymentInvoiceReversing = documentState.IsDocumentDebitAdjustment && document.OrigDocType == "PPI";
    documentState.IsRetainageApplyDocument = (documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment) && document.OrigModule == "AP" && !documentState.IsPrepaymentInvoiceReversing && !documentState.IsDocumentReleasedOrPrebooked && !documentState.IsRetainageDocument || documentState.RetainageApply;
    APInvoiceState apInvoiceState11 = documentState;
    int num11;
    if (!documentState.IsDocumentOnHold && !documentState.IsDocumentScheduled && !documentState.IsDocumentReleasedOrPrebooked && !documentState.IsDocumentVoided)
    {
      if (!documentState.IsDocumentRejected)
      {
        nullable = document.Approved;
        if (!nullable.GetValueOrDefault())
        {
          nullable = document.DontApprove;
          num11 = !nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num11 = 0;
      }
      else
        num11 = 1;
    }
    else
      num11 = 0;
    apInvoiceState11.IsDocumentRejectedOrPendingApproval = num11 != 0;
    APInvoiceState apInvoiceState12 = documentState;
    int num12;
    if (!documentState.IsDocumentOnHold && !documentState.IsDocumentScheduled && !documentState.IsDocumentReleasedOrPrebooked && !documentState.IsDocumentVoided)
    {
      nullable = document.Approved;
      if (nullable.GetValueOrDefault())
      {
        nullable = document.DontApprove;
        num12 = !nullable.GetValueOrDefault() ? 1 : 0;
        goto label_25;
      }
    }
    num12 = 0;
label_25:
    apInvoiceState12.IsDocumentApprovedBalanced = num12 != 0;
    documentState.LandedCostEnabled = false;
    if (document.VendorID.HasValue && document.VendorLocationID.HasValue && (documentState.IsDocumentInvoice || documentState.IsDocumentDebitAdjustment) && this.vendor.Current != null)
    {
      APInvoiceState apInvoiceState13 = documentState;
      nullable = this.vendor.Current.LandedCostVendor;
      int num13 = nullable.Value ? 1 : 0;
      apInvoiceState13.LandedCostEnabled = num13 != 0;
      if (!documentState.LandedCostEnabled && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
      {
        bool flag = PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.landedCostVendor, Equal<True>, And<Vendor.payToVendorID, Equal<Required<Vendor.payToVendorID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.vendor.Current.BAccountID).Any<PXResult<Vendor>>();
        documentState.LandedCostEnabled = flag;
      }
    }
    documentState.IsAssignmentEnabled = !documentState.IsDocumentReleasedOrPrebooked && !documentState.IsDocumentVoided && !documentState.IsDocumentRejectedOrPendingApproval && !documentState.IsDocumentApprovedBalanced;
    APInvoiceState apInvoiceState14 = documentState;
    nullable = document.IsTaxDocument;
    int num14;
    if (!nullable.GetValueOrDefault())
    {
      int num15;
      if (!documentState.IsDocumentPrepayment)
      {
        Vendor current = this.vendor.Current;
        if (current == null)
        {
          num15 = 0;
        }
        else
        {
          nullable = current.AllowOverrideCury;
          num15 = nullable.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num15 = !documentState.HasPOLink ? 1 : 0;
      if (num15 != 0)
      {
        num14 = !documentState.IsPrepaymentInvoiceReversing ? 1 : 0;
        goto label_37;
      }
    }
    num14 = 0;
label_37:
    apInvoiceState14.IsCuryEnabled = num14 != 0;
    documentState.IsFromPO = document.OrigModule == "PO";
    return documentState;
  }

  protected virtual void APInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APInvoice row1))
      return;
    APInvoiceState invoiceState = this.GetDocumentState(cache, row1);
    PXUIFieldAttribute.SetVisible<APInvoice.prebookAcctID>(cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APInvoice.prebookSubID>(cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APInvoice.prepaymentAccountID>(cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.prepaymentAccountID>(cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APInvoice.prepaymentSubID>(cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.prepaymentSubID>(cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APInvoice.projectID>(cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.projectID>(cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APTran.defScheduleID>(this.Transactions.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APTran.defScheduleID>(this.Transactions.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(this.Transactions.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APTran.deferredCode>(this.Transactions.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APTran.dRTermStartDate>(this.Transactions.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APTran.dRTermStartDate>(this.Transactions.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APTran.dRTermEndDate>(this.Transactions.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APTran.dRTermEndDate>(this.Transactions.Cache, (object) null, true);
    string caption;
    switch (row1.DocType)
    {
      case "INV":
        caption = "Pay";
        break;
      case "ACR":
        caption = "Pay";
        break;
      case "PPM":
        caption = "Pay/Apply";
        break;
      case "PPI":
        caption = row1.PendingPayment.GetValueOrDefault() ? "Pay" : "Apply";
        break;
      default:
        caption = "Apply";
        break;
    }
    this.payInvoice.SetCaption(caption);
    this.voidDocument.SetCaption("Void");
    this.createSchedule.SetVisible(row1.Status != "S");
    this.viewScheduleOfCurrentDocument.SetVisible(row1.Status == "S");
    this.release.SetEnabled(true);
    this.prebook.SetEnabled(true);
    this.createSchedule.SetEnabled(true);
    this.payInvoice.SetEnabled(true);
    this.reclassifyBatch.SetEnabled(true);
    if (invoiceState.IsDocumentPrepaymentInvoice)
      this.reverseInvoice.SetCaption("Write Off Unpaid Balance");
    bool? nullable1;
    if (this.IsImport)
    {
      PX.Objects.AP.APSetup current = this.APSetup.Current;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        nullable1 = current.MigrationMode;
        num = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      if (num != 0)
      {
        nullable1 = row1.IsMigratedRecord;
        if (nullable1.GetValueOrDefault() && this.cachePermission != null)
        {
          this.LoadCachesPermissions(this.cachePermission);
          this.cachePermission = (Dictionary<System.Type, CachePermission>) null;
        }
      }
    }
    nullable1 = row1.DontApprove;
    bool dontApprove = invoiceState.DontApprove;
    if (!(nullable1.GetValueOrDefault() == dontApprove & nullable1.HasValue))
      cache.SetValueExt<APInvoice.dontApprove>((object) row1, (object) invoiceState.DontApprove);
    this.Adjustments.Cache.AllowSelect = true;
    this.Adjustments_1.Cache.AllowSelect = true;
    this.recalculateDiscountsAction.SetEnabled(true);
    PXUIFieldAttribute.SetRequired<APInvoice.invoiceNbr>(cache, this.IsInvoiceNbrRequired(row1));
    PXUIFieldAttribute.SetVisible<APInvoice.curyID>(cache, (object) row1, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetRequired<APInvoice.termsID>(cache, !invoiceState.IsDocumentDebitAdjustment && !invoiceState.IsDocumentPrepayment);
    PXCache cache1 = cache;
    int num1;
    if (invoiceState.IsDocumentDebitAdjustment)
    {
      if (invoiceState.IsDocumentDebitAdjustment)
      {
        nullable1 = this.apsetup.Current.TermsInDebitAdjustments;
        if (nullable1.GetValueOrDefault())
        {
          num1 = row1.TermsID != null ? 1 : 0;
          goto label_24;
        }
      }
      num1 = 0;
    }
    else
      num1 = 1;
label_24:
    PXUIFieldAttribute.SetRequired<APInvoice.dueDate>(cache1, num1 != 0);
    PXCache cache2 = cache;
    int num2;
    if (invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentPrepayment)
    {
      if (invoiceState.IsDocumentDebitAdjustment)
      {
        nullable1 = this.apsetup.Current.TermsInDebitAdjustments;
        if (nullable1.GetValueOrDefault())
        {
          num2 = row1.TermsID != null ? 1 : 0;
          goto label_30;
        }
      }
      num2 = 0;
    }
    else
      num2 = 1;
label_30:
    PXUIFieldAttribute.SetRequired<APInvoice.discDate>(cache2, num2 != 0);
    this.viewPODocument.SetVisible(invoiceState.IsDocumentInvoice || invoiceState.IsDocumentDebitAdjustment);
    Vendor current1 = this.vendor.Current;
    int num3;
    if (current1 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable1 = current1.Vendor1099;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    bool flag1 = num3 != 0;
    Vendor current2 = this.vendor.Current;
    int num4;
    if (current2 == null)
    {
      num4 = 0;
    }
    else
    {
      nullable1 = current2.TaxAgency;
      num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num4 != 0)
    {
      PXUIFieldAttribute.SetEnabled<APInvoice.taxZoneID>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, false);
    }
    row1.LCEnabled = new bool?(invoiceState.LandedCostEnabled);
    cache.AllowDelete = true;
    cache.AllowUpdate = true;
    this.Transactions.Cache.AllowDelete = true;
    this.Transactions.Cache.AllowUpdate = true;
    this.Transactions.Cache.AllowInsert = row1.VendorID.HasValue && row1.VendorLocationID.HasValue;
    if (invoiceState.IsDocumentReleasedOrPrebooked || invoiceState.IsDocumentVoided)
    {
      int num5;
      if (this.vendor.Current != null)
      {
        nullable1 = this.vendor.Current.Vendor1099;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = row1.Voided;
          bool flag2 = false;
          num5 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
          goto label_43;
        }
      }
      num5 = 0;
label_43:
      bool isEnabled = num5 != 0;
      bool flag3 = false;
      foreach (PXResult<APInvoiceEntry.APAdjust> pxResult in this.Adjustments.Select())
      {
        APInvoiceEntry.APAdjust apAdjust = (APInvoiceEntry.APAdjust) pxResult;
        AP1099Year ap1099Year = (AP1099Year) PXSelectBase<AP1099Year, PXSelect<AP1099Year, Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>, And<AP1099Year.organizationID, Equal<Required<AP1099Year.organizationID>>>>>.Config>.Select((PXGraph) this, (object) apAdjust.AdjgDocDate.Value.Year.ToString(), (object) PXAccess.GetParentOrganizationID(apAdjust.AdjgBranchID));
        if (ap1099Year != null && ap1099Year.Status != "N")
          isEnabled = false;
        flag3 = true;
        if (flag3 & !isEnabled)
          break;
      }
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.dueDate>(cache, (object) row1, (!invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentDebitAdjustment && row1.TermsID != null) && row1.OpenDoc.GetValueOrDefault() && !row1.PendingPPD.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<APInvoice.paySel>(cache, (object) row1, !invoiceState.IsDocumentDebitAdjustment && row1.OpenDoc.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<APInvoice.payLocationID>(cache, (object) row1, row1.OpenDoc.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<APInvoice.payAccountID>(cache, (object) row1, row1.OpenDoc.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<APInvoice.payTypeID>(cache, (object) row1, row1.OpenDoc.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<APInvoice.isJointPayees>(cache, (object) row1, row1.OpenDoc.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<APInvoice.payDate>(cache, (object) row1, (!invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentDebitAdjustment && row1.TermsID != null) && row1.OpenDoc.GetValueOrDefault());
      PXUIFieldAttribute.SetEnabled<APInvoice.discDate>(cache, (object) row1, (!invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentDebitAdjustment && row1.TermsID != null) && !invoiceState.IsDocumentPrepayment && row1.OpenDoc.GetValueOrDefault() && !row1.PendingPPD.GetValueOrDefault());
      cache.AllowDelete = false;
      this.Transactions.Cache.AllowDelete = false;
      this.Transactions.Cache.AllowUpdate = isEnabled || invoiceState.IsDocumentPrebookedNotCompleted;
      this.Transactions.Cache.AllowInsert = false;
      this.DiscountDetails.Cache.SetAllEditPermissions(false);
      this.Taxes.Cache.AllowUpdate = false;
      bool flag4 = !flag3 && invoiceState.HasPOLink;
      if (this._allowToVoidReleased)
      {
        PXAction<APInvoice> voidInvoice = this.voidInvoice;
        nullable1 = row1.Released;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = row1.Prebooked;
          if (!nullable1.GetValueOrDefault())
            goto label_58;
        }
        int num6;
        if (!flag4)
        {
          nullable1 = row1.Voided;
          bool flag5 = false;
          if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
          {
            num6 = !flag3 ? 1 : 0;
            goto label_59;
          }
        }
label_58:
        num6 = 0;
label_59:
        voidInvoice.SetEnabled(num6 != 0);
      }
      else
        this.voidInvoice.SetEnabled(invoiceState.IsDocumentPrebookedNotCompleted && !flag4);
      if (isEnabled || invoiceState.IsDocumentPrebookedNotCompleted)
      {
        PXUIFieldAttribute.SetEnabled(this.Transactions.Cache, (string) null, false);
        PXUIFieldAttribute.SetEnabled<APTran.box1099>(this.Transactions.Cache, (object) null, isEnabled);
        PXUIFieldAttribute.SetEnabled<APTran.accountID>(this.Transactions.Cache, (object) null, invoiceState.IsDocumentPrebookedNotCompleted);
        PXUIFieldAttribute.SetEnabled<APTran.subID>(this.Transactions.Cache, (object) null, invoiceState.IsDocumentPrebookedNotCompleted);
        PXUIFieldAttribute.SetEnabled<APTran.branchID>(this.Transactions.Cache, (object) null, invoiceState.IsDocumentPrebookedNotCompleted);
        if (!invoiceState.IsDocumentPrebookedNotCompleted)
          PXUIFieldAttribute.SetEnabled<APTran.projectID>(this.Transactions.Cache, (object) null, false);
        PXUIFieldAttribute.SetEnabled<APTran.taskID>(this.Transactions.Cache, (object) null, invoiceState.IsDocumentPrebookedNotCompleted);
      }
    }
    else if (invoiceState.IsDocumentRejectedOrPendingApproval || invoiceState.IsDocumentApprovedBalanced)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APRegister.hold>(cache, (object) row1);
      PXUIFieldAttribute.SetEnabled<APInvoice.separateCheck>(cache, (object) row1, !invoiceState.IsDocumentRejected);
      PXUIFieldAttribute.SetEnabled<APInvoice.paySel>(cache, (object) row1, !invoiceState.IsDocumentRejected);
      PXUIFieldAttribute.SetEnabled<APInvoice.payDate>(cache, (object) row1, !invoiceState.IsDocumentRejected);
      PXUIFieldAttribute.SetEnabled<APInvoice.payLocationID>(cache, (object) row1, !invoiceState.IsDocumentRejected);
      PXUIFieldAttribute.SetEnabled<APInvoice.payTypeID>(cache, (object) row1, !invoiceState.IsDocumentRejected);
      PXUIFieldAttribute.SetEnabled<APInvoice.payAccountID>(cache, (object) row1, !invoiceState.IsDocumentRejected);
      this.Transactions.Cache.SetAllEditPermissions(false);
      this.DiscountDetails.Cache.SetAllEditPermissions(false);
      this.Taxes.Cache.SetAllEditPermissions(false);
      this.recalculateDiscountsAction.SetEnabled(false);
    }
    else if (invoiceState.IsRetainageReversing)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.docDesc>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APInvoice.hold>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APInvoice.docDate>(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APInvoice.finPeriodID>(cache, (object) row1, true);
      if (invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentCreditAdjustment)
        PXUIFieldAttribute.SetEnabled<APInvoice.invoiceNbr>(cache, (object) row1, true);
      this.DiscountDetails.Cache.SetAllEditPermissions(false);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<APInvoice.isRetainageDocument>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.status>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.curyDocBal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.curyLineTotal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.curyTaxTotal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.curyOrigWhTaxAmt>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.curyVatExemptTotal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.curyVatTaxableTotal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.batchNbr>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.curyID>(cache, (object) row1, invoiceState.IsCuryEnabled);
      PXCache cache3 = cache;
      APInvoice data1 = row1;
      nullable1 = row1.Scheduled;
      bool flag6 = false;
      int num7 = nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<APInvoice.hold>(cache3, (object) data1, num7 != 0);
      if (invoiceState.IsPrepaymentInvoiceReversing)
      {
        PXUIFieldAttribute.SetEnabled<APInvoice.docDesc>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.vendorLocationID>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.aPAccountID>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.aPSubID>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.prebookAcctID>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.prebookSubID>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.payLocationID>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.entityUsageType>(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<APInvoice.externalTaxExemptionNumber>(cache, (object) row1, false);
      }
      PXCache cache4 = cache;
      APInvoice data2 = row1;
      if (invoiceState.IsDocumentDebitAdjustment)
      {
        nullable1 = this.apsetup.Current.TermsInDebitAdjustments;
        if (!nullable1.GetValueOrDefault())
          goto label_77;
      }
      int num8;
      if (!invoiceState.IsDocumentPrepayment)
      {
        num8 = !invoiceState.IsPrepaymentInvoiceReversing ? 1 : 0;
        goto label_78;
      }
label_77:
      num8 = 0;
label_78:
      PXUIFieldAttribute.SetEnabled<APInvoice.termsID>(cache4, (object) data2, num8 != 0);
      PXUIFieldAttribute.SetEnabled<APInvoice.dueDate>(cache, (object) row1, !invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentDebitAdjustment && row1.TermsID != null);
      PXUIFieldAttribute.SetEnabled<APInvoice.discDate>(cache, (object) row1, (!invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentDebitAdjustment && row1.TermsID != null) && !invoiceState.IsDocumentPrepayment);
      bool flag7 = ((PX.Objects.CS.Terms) PXSelectorAttribute.Select<APInvoice.termsID>(cache, (object) row1))?.InstallmentType == "M";
      PXUIFieldAttribute.SetEnabled<APInvoice.curyOrigDiscAmt>(cache, (object) row1, !flag7 && (!invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentDebitAdjustment && row1.TermsID != null) && !invoiceState.IsDocumentPrepayment && !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
      PXUIFieldAttribute.SetEnabled(this.Transactions.Cache, (string) null, true);
      PXUIFieldAttribute.SetEnabled<APTran.defScheduleID>(this.Transactions.Cache, (object) null, row1.DocType == "ADR");
      PXUIFieldAttribute.SetEnabled<APTran.curyTranAmt>(this.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<APTran.discountSequenceID>(this.Transactions.Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<APTran.baseQty>(this.Transactions.Cache, (object) null, false);
      PXCache cache5 = cache;
      APInvoice data3 = row1;
      nullable1 = row1.OpenDoc;
      int num9 = nullable1.Value ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<APInvoice.payAccountID>(cache5, (object) data3, num9 != 0);
      PXCache cache6 = cache;
      APInvoice data4 = row1;
      nullable1 = row1.OpenDoc;
      int num10 = nullable1.Value ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<APInvoice.payTypeID>(cache6, (object) data4, num10 != 0);
      PXUIFieldAttribute.SetEnabled<APInvoice.payDate>(cache, (object) row1, !invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentDebitAdjustment && row1.TermsID != null);
      this.DiscountDetails.Cache.SetAllEditPermissions(!invoiceState.RetainageApply && !invoiceState.IsPrepaymentInvoiceReversing);
      PXCache cache7 = cache;
      APInvoice data5 = row1;
      int num11;
      if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorDiscounts>())
      {
        nullable1 = row1.PaymentsByLinesAllowed;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = row1.RetainageApply;
          num11 = !nullable1.GetValueOrDefault() ? 1 : 0;
          goto label_82;
        }
      }
      num11 = 0;
label_82:
      PXUIFieldAttribute.SetEnabled<APInvoice.curyDiscTot>(cache7, (object) data5, num11 != 0);
      Vendor vendor = (Vendor) this.vendor.Select();
      if (vendor != null && vendor.VStatus == "I")
        cache.RaiseExceptionHandling<APInvoice.vendorID>((object) row1, (object) vendor.AcctCD, (Exception) new PXSetPropertyException("The vendor status is '{0}'.", PXErrorLevel.Warning, new object[1]
        {
          (object) "Inactive"
        }));
      this.Taxes.Cache.AllowUpdate = true;
      PXUIFieldAttribute.SetEnabled<APInvoice.retainageApply>(cache, (object) row1, !invoiceState.IsDocumentCreditAdjustment);
      if (invoiceState.HasPOLink || invoiceState.IsFromExpenseClaims || invoiceState.IsRetainageDocument)
        PXUIFieldAttribute.SetEnabled<APInvoice.projectID>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<APInvoice.taxZoneID>(cache, (object) row1, !invoiceState.IsRetainageDocument && !invoiceState.IsPrepaymentInvoiceReversing);
      PXUIFieldAttribute.SetEnabled<APInvoice.taxCalcMode>(cache, (object) row1, !invoiceState.IsRetainageDocument);
      PXUIFieldAttribute.SetEnabled<APInvoice.branchID>(cache, (object) row1, !invoiceState.IsRetainageDocument && !invoiceState.IsPrepaymentInvoiceReversing);
      PXUIFieldAttribute.SetEnabled<APTran.curyRetainageAmt>(this.Transactions.Cache, (object) null, invoiceState.RetainageApply);
      PXUIFieldAttribute.SetEnabled<APTran.retainagePct>(this.Transactions.Cache, (object) null, invoiceState.RetainageApply);
    }
    if (this.APSetup.Current != null)
    {
      PXUIFieldAttribute.SetEnabled<APRegister.employeeWorkgroupID>(cache, (object) row1, invoiceState.IsAssignmentEnabled);
      PXUIFieldAttribute.SetEnabled<APRegister.employeeID>(cache, (object) row1, invoiceState.IsAssignmentEnabled);
    }
    PXUIFieldAttribute.SetEnabled<APTran.pOAccrualType>(this.Transactions.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APInvoice.docType>(cache, (object) row1);
    PXUIFieldAttribute.SetEnabled<APInvoice.refNbr>(cache, (object) row1);
    PXCache cache8 = this.Taxes.Cache;
    cache8.AllowDelete = ((cache8.AllowDelete ? 1 : 0) & (invoiceState.IsRetainageReversing || invoiceState.IsRetainageDocument || invoiceState.IsDocumentReleasedOrPrebooked ? 0 : (!invoiceState.IsPrepaymentInvoiceReversing ? 1 : 0))) != 0;
    PXCache cache9 = this.Taxes.Cache;
    cache9.AllowInsert = ((cache9.AllowInsert ? 1 : 0) & (invoiceState.IsRetainageReversing || invoiceState.IsRetainageDocument || invoiceState.IsDocumentReleasedOrPrebooked ? 0 : (!invoiceState.IsPrepaymentInvoiceReversing ? 1 : 0))) != 0;
    PXCache cache10 = this.Taxes.Cache;
    cache10.AllowUpdate = ((cache10.AllowUpdate ? 1 : 0) & (invoiceState.IsRetainageReversing || invoiceState.IsRetainageDocument || invoiceState.IsDocumentReleasedOrPrebooked ? 0 : (!invoiceState.IsPrepaymentInvoiceReversing ? 1 : 0))) != 0;
    this.Adjustments.AllowSelect = !invoiceState.IsDocumentDebitAdjustment;
    this.Adjustments.Cache.AllowInsert = false;
    this.Adjustments.Cache.AllowDelete = false;
    this.Adjustments.Cache.AllowUpdate = !invoiceState.IsRetainageReversing && invoiceState.IsDocumentRejectedOrPendingApproval || invoiceState.IsDocumentApprovedBalanced ? !invoiceState.IsDocumentRejected : this.Transactions.Cache.AllowUpdate && !invoiceState.IsDocumentPrebookedNotCompleted;
    this.Adjustments_1.AllowSelect = invoiceState.IsDocumentDebitAdjustment;
    this.Adjustments_1.Cache.AllowInsert = false;
    this.Adjustments_1.Cache.AllowDelete = false;
    this.Adjustments_1.Cache.AllowUpdate = false;
    this.editVendor.SetEnabled(this.vendor?.Current != null && !invoiceState.IsRetainageReversing);
    PXUIFieldAttribute.SetEnabled<APInvoiceEntry.APAdjust.adjgBranchID>(this.Adjustments.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.displayDocType>(this.Adjustments.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APAdjust.displayRefNbr>(this.Adjustments.Cache, (object) null, false);
    PXAction<APInvoice> reclassifyBatch = this.reclassifyBatch;
    nullable1 = row1.Released;
    int num12 = !nullable1.GetValueOrDefault() || invoiceState.IsDocumentPrepayment ? 0 : (!invoiceState.IsRetainageReversing ? 1 : 0);
    reclassifyBatch.SetEnabled(num12 != 0);
    this.autoApply.SetEnabled(!invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    if (this.Transactions.Any<APTran>())
      PXUIFieldAttribute.SetEnabled<APInvoice.vendorID>(cache, (object) row1, !row1.VendorID.HasValue);
    if (row1.VendorLocationID.HasValue && !invoiceState.IsRetainageReversing && !invoiceState.IsPrepaymentInvoiceReversing)
    {
      PXCache cache11 = cache;
      APInvoice data = row1;
      int num13;
      if (invoiceState.IsDocumentEditable)
      {
        nullable1 = row1.Voided;
        num13 = !nullable1.Value ? 1 : 0;
      }
      else
        num13 = 0;
      PXUIFieldAttribute.SetEnabled<APInvoice.vendorLocationID>(cache11, (object) data, num13 != 0);
    }
    PXCache cache12 = cache;
    APInvoice data6 = row1;
    nullable1 = this.APSetup.Current.RequireControlTotal;
    int num14 = nullable1.GetValueOrDefault() ? 1 : (invoiceState.IsDocumentReleasedOrPrebooked ? 1 : 0);
    PXUIFieldAttribute.SetVisible<APInvoice.curyOrigDocAmt>(cache12, (object) data6, num14 != 0);
    PXCache cache13 = cache;
    APInvoice data7 = row1;
    int num15;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
    {
      nullable1 = this.APSetup.Current.RequireControlTaxTotal;
      num15 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num15 = 0;
    PXUIFieldAttribute.SetVisible<APInvoice.curyTaxAmt>(cache13, (object) data7, num15 != 0);
    PXUIFieldAttribute.SetVisible<APTran.box1099>(this.Transactions.Cache, (object) null, flag1 && !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetEnabled<APTran.box1099>(this.Transactions.Cache, (object) null, flag1 && !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    PXCache cache14 = cache;
    APInvoice data8 = row1;
    nullable1 = row1.Prebooked;
    int num16 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<APInvoice.prebookBatchNbr>(cache14, (object) data8, num16 != 0);
    PXCache cache15 = cache;
    APInvoice data9 = row1;
    nullable1 = row1.Voided;
    int num17 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<APInvoice.voidBatchNbr>(cache15, (object) data9, num17 != 0);
    PXCache cache16 = cache;
    APInvoice data10 = row1;
    nullable1 = row1.Voided;
    int num18 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<APInvoice.batchNbr>(cache16, (object) data10, num18 != 0);
    Decimal? curyRoundDiff = row1.CuryRoundDiff;
    Decimal num19 = 0M;
    bool isVisible1 = !(curyRoundDiff.GetValueOrDefault() == num19 & curyRoundDiff.HasValue) || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.invoiceRounding>();
    PXUIFieldAttribute.SetVisible<APInvoice.curyRoundDiff>(cache, (object) row1, isVisible1);
    PXResultset<APTaxTran> resultSet = this.UseTaxes.Select();
    if (resultSet.Count != 0 && !this.UnattendedMode)
      cache.RaiseExceptionHandling<APInvoice.curyTaxTotal>((object) row1, (object) row1.CuryTaxTotal, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", PXErrorLevel.Warning));
    if (!this.UnattendedMode)
    {
      nullable1 = row1.Hold;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row1.Released;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = row1.Prebooked;
          if (!nullable1.GetValueOrDefault())
          {
            PX.Objects.PO.POOrder differentTaxCalcMode = this.FindPOOrderWithDifferentTaxCalcMode(row1);
            if (differentTaxCalcMode != null && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
              cache.RaiseExceptionHandling<APInvoice.taxCalcMode>((object) row1, (object) row1.TaxCalcMode, (Exception) new PXSetPropertyException("The {1} purchase order has a tax calculation mode other than {0}.", PXErrorLevel.Warning, new object[2]
              {
                (object) PXStringListAttribute.GetLocalizedLabel<APInvoice.taxCalcMode>(cache, (object) row1),
                (object) differentTaxCalcMode.OrderNbr
              }));
          }
        }
      }
    }
    Company company = (Company) PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this);
    nullable1 = row1.IsTaxDocument;
    bool flag8 = nullable1.GetValueOrDefault() && this.vendor.Current?.CuryID != null && this.vendor.Current?.CuryID != company.BaseCuryID;
    PXCache cache17 = this.Transactions.Cache;
    cache17.AllowDelete = ((cache17.AllowDelete ? 1 : 0) & (invoiceState.IsRetainageDocument || invoiceState.IsRetainageReversing || flag8 ? 0 : (!invoiceState.IsPrepaymentInvoiceReversing ? 1 : 0))) != 0;
    PXCache cache18 = this.Transactions.Cache;
    cache18.AllowInsert = ((cache18.AllowInsert ? 1 : 0) & (invoiceState.IsRetainageDocument || invoiceState.IsRetainageReversing || flag8 || invoiceState.IsPrepaymentRequestFromPO ? 0 : (!invoiceState.IsPrepaymentInvoiceReversing ? 1 : 0))) != 0;
    PXCache cache19 = this.Transactions.Cache;
    cache19.AllowUpdate = ((cache19.AllowUpdate ? 1 : 0) & (!(!invoiceState.IsRetainageDocument | flag1) || invoiceState.IsRetainageReversing || flag8 ? 0 : (!invoiceState.IsPrepaymentInvoiceReversing ? 1 : 0))) != 0;
    bool flag9 = PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.payToVendorID, Equal<Current<APInvoice.vendorID>>>, PX.Data.OrderBy<Asc<Vendor.bAccountID>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row1
    }).AsEnumerable<PXResult<Vendor>>().Any<PXResult<Vendor>>();
    int num20;
    if (!invoiceState.HasPOLink)
    {
      nullable1 = row1.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row1.Prebooked;
        num20 = !nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_111;
      }
    }
    num20 = 0;
label_111:
    int num21 = flag9 ? 1 : 0;
    bool isEnabled1 = (num20 & num21) != 0 && !invoiceState.IsRetainageReversing;
    PXUIFieldAttribute.SetEnabled<APInvoice.suppliedByVendorID>(cache, (object) row1, isEnabled1);
    PXUIFieldAttribute.SetVisible<APInvoice.suppliedByVendorID>(cache, (object) row1, row1.DocType == "INV" || row1.DocType == "ADR");
    PXCache cache20 = cache;
    APInvoice data11 = row1;
    int num22;
    if (isEnabled1)
    {
      int? vendorId = row1.VendorID;
      int? suppliedByVendorId = row1.SuppliedByVendorID;
      num22 = !(vendorId.GetValueOrDefault() == suppliedByVendorId.GetValueOrDefault() & vendorId.HasValue == suppliedByVendorId.HasValue) ? 1 : 0;
    }
    else
      num22 = 0;
    PXUIFieldAttribute.SetEnabled<APInvoice.suppliedByVendorLocationID>(cache20, (object) data11, num22 != 0);
    PXUIFieldAttribute.SetVisible<APInvoice.suppliedByVendorLocationID>(cache, (object) row1, row1.DocType == "INV" || row1.DocType == "ADR");
    int num23;
    if (invoiceState.IsDocumentInvoice && !invoiceState.IsFromExpenseClaims)
    {
      if (!invoiceState.IsDocumentReleasedOrPrebooked)
      {
        nullable1 = row1.IsRetainageDocument;
        if (!nullable1.GetValueOrDefault())
        {
          num23 = 1;
          goto label_120;
        }
      }
      num23 = invoiceState.RetainageApply ? 1 : 0;
    }
    else
      num23 = 0;
label_120:
    int num24 = !invoiceState.IsDocumentDebitAdjustment || invoiceState.IsFromExpenseClaims ? 0 : (invoiceState.RetainageApply ? 1 : 0);
    bool flag10 = invoiceState.IsDocumentInvoice || invoiceState.IsDocumentPrepayment || invoiceState.IsDocumentPrepaymentInvoice || invoiceState.IsDocumentDebitAdjustment || invoiceState.IsDocumentCreditAdjustment;
    PXUIFieldAttribute.SetVisible<APRegister.retainageAcctID>(cache, (object) row1, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetVisible<APRegister.retainageSubID>(cache, (object) row1, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetVisible<APInvoice.retainageApply>(cache, (object) row1, invoiceState.IsRetainageApplyDocument);
    PXUIFieldAttribute.SetVisible<APInvoice.isRetainageDocument>(cache, (object) row1, invoiceState.IsRetainageDocument);
    if (!flag10)
      PXUIFieldAttribute.SetVisible<APInvoice.projectID>(cache, (object) row1, false);
    PXUIFieldAttribute.SetVisible<APTran.retainagePct>(this.Transactions.Cache, (object) null, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetVisible<APTran.curyRetainageAmt>(this.Transactions.Cache, (object) null, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetVisible<APTaxTran.curyRetainedTaxableAmt>(this.Taxes.Cache, (object) null, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetVisible<APTaxTran.curyRetainedTaxAmt>(this.Taxes.Cache, (object) null, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetVisible<APInvoice.isJointPayees>(cache, (object) row1, row1.DocType == "INV");
    PXUIFieldAttribute.SetRequired<APRegister.retainageAcctID>(cache, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetRequired<APRegister.retainageSubID>(cache, invoiceState.RetainageApply);
    PXUIFieldAttribute.SetRequired<APInvoice.projectID>(cache, flag10 && !invoiceState.HasPOLink && !invoiceState.IsFromExpenseClaims && invoiceState.RetainageApply);
    PXDefaultAttribute.SetPersistingCheck<APInvoice.projectID>(cache, (object) row1, !flag10 || invoiceState.HasPOLink || invoiceState.IsFromExpenseClaims || !invoiceState.RetainageApply ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
    bool isVisible2 = false;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATReporting>())
    {
      Decimal? curyOrigDiscAmt = row1.CuryOrigDiscAmt;
      Decimal num25 = 0M;
      if (curyOrigDiscAmt.GetValueOrDefault() > num25 & curyOrigDiscAmt.HasValue && (row1.DocType != "ADR" || row1.DocType == "ADR" && row1.TermsID != null))
      {
        this.Taxes.Select();
        nullable1 = row1.HasPPDTaxes;
        isVisible2 = nullable1.GetValueOrDefault();
      }
    }
    PXUIFieldAttribute.SetVisible<APRegister.curyDiscountedDocTotal>(cache, e.Row, isVisible2);
    PXUIFieldAttribute.SetVisible<APRegister.curyDiscountedTaxableTotal>(cache, e.Row, isVisible2);
    PXUIFieldAttribute.SetVisible<APRegister.curyDiscountedPrice>(cache, e.Row, isVisible2);
    PXUIVisibility visibility = isVisible2 ? PXUIVisibility.Visible : PXUIVisibility.Invisible;
    PXUIFieldAttribute.SetVisibility<APTaxTran.curyDiscountedPrice>(this.Taxes.Cache, (object) null, visibility);
    PXUIFieldAttribute.SetVisibility<APTaxTran.curyDiscountedTaxableAmt>(this.Taxes.Cache, (object) null, visibility);
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.retainage>())
    {
      nullable1 = row1.RetainageApply;
      if (nullable1.GetValueOrDefault() && resultSet.RowCast<APTaxTran>().Where<APTaxTran>((Func<APTaxTran, bool>) (taxtran =>
      {
        Decimal? curyRetainedTaxAmt = taxtran.CuryRetainedTaxAmt;
        Decimal num26 = 0M;
        return !(curyRetainedTaxAmt.GetValueOrDefault() == num26 & curyRetainedTaxAmt.HasValue);
      })).Any<APTaxTran>() && !this.UnattendedMode)
        cache.RaiseExceptionHandling<APRegister.curyRetainedTaxTotal>((object) row1, (object) row1.CuryRetainedTaxTotal, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", PXErrorLevel.Warning));
    }
    PXUIFieldAttribute.SetVisible<APInvoice.paymentsByLinesAllowed>(cache, (object) row1, !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    int num27;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentsByLines>())
    {
      nullable1 = row1.PaymentsByLinesAllowed;
      num27 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num27 = 0;
    bool isVisible3 = num27 != 0;
    PXUIFieldAttribute.SetVisible<APTran.lineNbr>(this.Transactions.Cache, (object) null, isVisible3);
    if (isVisible3)
    {
      this.autoApply.SetVisible(false);
      this.Adjustments.Cache.SetAllEditPermissions(false);
      this.DiscountDetails.Cache.SetAllEditPermissions(false);
      this.DiscountDetails.Cache.AllowDelete = true;
      if (!invoiceState.IsDocumentReleasedOrPrebooked && !this.UnattendedMode)
      {
        string message = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorDiscounts>() ? "Group discounts and document discounts are not supported if the Pay by Line check box is selected." : "Document discounts are not supported if the Pay by Line check box is selected.";
        cache.RaiseExceptionHandling<APInvoice.curyDiscTot>((object) row1, (object) row1.CuryDiscTot, (Exception) new PXSetPropertyException(message, PXErrorLevel.Warning));
        cache.RaiseExceptionHandling<APInvoice.curyOrigWhTaxAmt>((object) row1, (object) row1.CuryOrigWhTaxAmt, (Exception) new PXSetPropertyException("Withholding taxes are not supported when the Pay by Line check box is selected.", PXErrorLevel.Warning));
      }
      nullable1 = row1.Released;
      if (nullable1.GetValueOrDefault())
      {
        foreach (APInvoiceEntry.APAdjust row2 in this.Adjustments.Select().RowCast<APInvoiceEntry.APAdjust>().Where<APInvoiceEntry.APAdjust>((Func<APInvoiceEntry.APAdjust, bool>) (a =>
        {
          int? adjdLineNbr = a.AdjdLineNbr;
          int num28 = 0;
          return adjdLineNbr.GetValueOrDefault() == num28 & adjdLineNbr.HasValue && !a.Released.GetValueOrDefault();
        })))
          this.Adjustments.Cache.RaiseExceptionHandling<APInvoiceEntry.APAdjust.adjgRefNbr>((object) row2, (object) row2.AdjgRefNbr, (Exception) new PXSetPropertyException("The application cannot be released because it is not distributed between document lines. On the Checks and Payments (AP302000) form, delete the application, and apply the prepayment to the document lines.", PXErrorLevel.RowWarning));
      }
    }
    nullable1 = row1.RetainageApply;
    if (nullable1.GetValueOrDefault())
    {
      string message = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorDiscounts>() ? "Group discounts and document discounts are not supported if the Apply Retainage check box is selected." : "Document discounts are not supported if the Apply Retainage check box is selected.";
      cache.RaiseExceptionHandling<APInvoice.curyDiscTot>((object) row1, (object) row1.CuryDiscTot, (Exception) new PXSetPropertyException(message, PXErrorLevel.Warning));
    }
    if (invoiceState.IsDocumentPrepayment || invoiceState.IsRetainageDocument || row1.Status == "X")
      PXUIFieldAttribute.SetEnabled<APInvoice.paymentsByLinesAllowed>(cache, (object) row1, false);
    PXUIFieldAttribute.SetEnabled<APTran.curyCashDiscBal>(this.Transactions.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APTran.curyRetainageBal>(this.Transactions.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APTran.curyTranBal>(this.Transactions.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APTran.curyOrigTaxAmt>(this.Transactions.Cache, (object) null, false);
    PXCache cache21 = this.Transactions.Cache;
    int num29;
    if (isVisible3 && invoiceState.IsDocumentReleasedOrPrebooked)
    {
      Decimal? curyOrigDiscAmt = row1.CuryOrigDiscAmt;
      Decimal num30 = 0M;
      num29 = !(curyOrigDiscAmt.GetValueOrDefault() == num30 & curyOrigDiscAmt.HasValue) ? 1 : 0;
    }
    else
      num29 = 0;
    PXUIFieldAttribute.SetVisible<APTran.curyCashDiscBal>(cache21, (object) null, num29 != 0);
    bool isVisible4 = isVisible3 && invoiceState.IsDocumentReleasedOrPrebooked && invoiceState.RetainageApply;
    PXUIFieldAttribute.SetVisible<APTran.curyRetainageBal>(this.Transactions.Cache, (object) null, isVisible4);
    PXUIFieldAttribute.SetVisible<APTran.curyRetainedTaxAmt>(this.Transactions.Cache, (object) null, isVisible4);
    bool isVisible5 = isVisible3 && invoiceState.IsDocumentReleasedOrPrebooked;
    PXUIFieldAttribute.SetVisible<APTran.curyTranBal>(this.Transactions.Cache, (object) null, isVisible5);
    PXUIFieldAttribute.SetVisible<APTran.curyOrigTaxAmt>(this.Transactions.Cache, (object) null, isVisible5);
    nullable1 = row1.IsMigratedRecord;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    int num31;
    if (valueOrDefault)
    {
      nullable1 = row1.Released;
      num31 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num31 = 0;
    bool flag11 = num31 != 0;
    int num32;
    if (valueOrDefault)
    {
      nullable1 = row1.Released;
      num32 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num32 = 0;
    bool isVisible6 = num32 != 0;
    PX.Objects.AP.APSetup current3 = this.APSetup.Current;
    int num33;
    if (current3 == null)
    {
      num33 = 0;
    }
    else
    {
      nullable1 = current3.MigrationMode;
      num33 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<APInvoice.curyDocBal>(cache, (object) row1, !flag11);
    PXUIFieldAttribute.SetVisible<APInvoice.curyInitDocBal>(cache, (object) row1, flag11);
    PXUIFieldAttribute.SetEnabled<APInvoice.curyInitDocBal>(cache, (object) row1, flag11);
    PXUIFieldAttribute.SetVisible<APRegister.displayCuryInitDocBal>(cache, (object) row1, isVisible6);
    if (num33 != 0)
      PXUIFieldAttribute.SetEnabled<APInvoice.paymentsByLinesAllowed>(cache, (object) row1, false);
    if (flag11)
      this.Adjustments.Cache.AllowSelect = false;
    if ((num33 != 0 ? (!valueOrDefault ? 1 : 0) : (flag11 ? 1 : 0)) != 0)
    {
      bool allowInsert = this.Document.Cache.AllowInsert;
      bool allowDelete = this.Document.Cache.AllowDelete;
      if (this.IsImport && this.cachePermission == null)
        this.cachePermission = this.SaveCachesPermissions(true);
      this.DisableCaches();
      this.Document.Cache.AllowInsert = allowInsert;
      this.Document.Cache.AllowDelete = allowDelete;
    }
    if (flag11 && string.IsNullOrEmpty(PXUIFieldAttribute.GetError<APInvoice.curyInitDocBal>(cache, (object) row1)))
      cache.RaiseExceptionHandling<APInvoice.curyInitDocBal>((object) row1, (object) row1.CuryInitDocBal, (Exception) new PXSetPropertyException("Enter the document open balance to this box.", PXErrorLevel.Warning));
    PXUIFieldAttribute.SetVisible<APTran.nonBillable>(this.Transactions.Cache, (object) null, ProjectAttribute.IsPMVisible("AP") && !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetEnabled<APTran.nonBillable>(this.Transactions.Cache, (object) null, !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetVisible<APTran.date>(this.Transactions.Cache, (object) null, !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    PXUIFieldAttribute.SetEnabled<APTran.date>(this.Transactions.Cache, (object) null, !invoiceState.IsDocumentPrepaymentInvoice && !invoiceState.IsPrepaymentInvoiceReversing);
    this.Transactions.Cache.Adjust<PXUIFieldAttribute>().For<APTran.prepaymentPct>((System.Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = invoiceState.IsDocumentPrepayment || invoiceState.IsDocumentPrepaymentInvoice;
      a.Visible = invoiceState.IsDocumentPrepayment || invoiceState.IsDocumentPrepaymentInvoice;
    })).For<APTran.curyPrepaymentAmt>((System.Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = invoiceState.IsDocumentPrepaymentInvoice;
      a.Visible = invoiceState.IsDocumentPrepayment || invoiceState.IsDocumentPrepaymentInvoice;
    }));
    cache.RaiseExceptionHandling<APInvoice.curyRoundDiff>((object) row1, (object) null, (Exception) null);
    nullable1 = this.APSetup.Current.RequireControlTaxTotal;
    bool flag12 = nullable1.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>();
    nullable1 = row1.Hold;
    Decimal? nullable2;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row1.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row1.Prebooked;
        if (!nullable1.GetValueOrDefault())
        {
          Decimal? roundDiff = row1.RoundDiff;
          Decimal num34 = 0M;
          if (!(roundDiff.GetValueOrDefault() == num34 & roundDiff.HasValue))
          {
            if (!flag12)
            {
              if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.invoiceRounding>())
              {
                Decimal? taxRoundDiff = row1.TaxRoundDiff;
                Decimal num35 = 0M;
                if (taxRoundDiff.GetValueOrDefault() == num35 & taxRoundDiff.HasValue)
                  goto label_176;
              }
              cache.RaiseExceptionHandling<APInvoice.curyRoundDiff>((object) row1, (object) row1.CuryRoundDiff, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>() ? (Exception) new PXSetPropertyException("Tax Amount cannot be edited because \"Validate Tax Totals on Entry\" is not selected on the AP Preferences form.", row1.OrigModule == "EP" ? PXErrorLevel.Warning : PXErrorLevel.Error) : (Exception) new PXSetPropertyException("Tax Amount cannot be edited because the Net/Gross Entry Mode feature is not enabled."));
              goto label_181;
            }
label_176:
            if (this.currencyinfo.Current == null)
              this.currencyinfo.Current = this.currencyinfo.SelectSingle();
            string curyID = this.currencyinfo.Current?.BaseCuryID ?? this.Accessinfo.BaseCuryID;
            Decimal num36 = CurrencyCollection.GetCurrency(curyID).RoundingLimit.Value;
            nullable2 = row1.RoundDiff;
            if (System.Math.Abs(nullable2.Value) > System.Math.Abs(num36))
              cache.RaiseExceptionHandling<APInvoice.curyRoundDiff>((object) row1, (object) row1.CuryRoundDiff, (Exception) new PXSetPropertyException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
              {
                (object) curyID,
                (object) PXDBQuantityAttribute.Round(row1.RoundDiff),
                (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(this.currencyinfo.Current.BaseCuryID).RoundingLimit)
              }));
          }
        }
      }
    }
label_181:
    if (!invoiceState.IsDocumentReleasedOrPrebooked && !this.UnattendedMode)
    {
      nullable2 = row1.RoundDiff;
      Decimal num37 = 0M;
      if (nullable2.GetValueOrDefault() == num37 & nullable2.HasValue && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.invoiceRounding>())
      {
        if (invoiceState.RetainageApply)
          cache.RaiseExceptionHandling<APInvoice.curyRoundDiff>((object) row1, (object) row1.CuryRoundDiff, (Exception) new PXSetPropertyException("Documents with retainage do not support invoice rounding.", PXErrorLevel.Warning));
        else if (isVisible3)
          cache.RaiseExceptionHandling<APInvoice.curyRoundDiff>((object) row1, (object) row1.CuryRoundDiff, (Exception) new PXSetPropertyException("Documents paid by line do not support invoice rounding.", PXErrorLevel.Warning));
      }
    }
    if (this.Document.Current != null)
    {
      nullable1 = this.Document.Current.SetWarningOnDiscount;
      if (nullable1.GetValueOrDefault())
        this.Document.Cache.RaiseExceptionHandling<APInvoice.curyDiscTot>((object) this.Document.Current, (object) this.Document.Current.CuryDiscTot, (Exception) new PXSetPropertyException("One or more purchase orders added to the AP bill contain group or document discounts. Please check the purchase orders and add discounts manually, if needed. See Trace Log for details.", PXErrorLevel.Warning));
    }
    PXCache cache22 = cache;
    object row3 = e.Row;
    string docType = row1.DocType;
    int num38 = docType != null ? (EnumerableExtensions.IsIn<string>(docType, "ADR", "INV") ? 1 : 0) : 0;
    PXUIFieldAttribute.SetVisible<APRegister.taxCostINAdjRefNbr>(cache22, row3, num38 != 0);
    PXCache cache23 = cache;
    object row4 = e.Row;
    Vendor current4 = this.vendor.Current;
    int num39;
    if (current4 == null)
    {
      num39 = 0;
    }
    else
    {
      nullable1 = current4.IsBranch;
      num39 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num40 = num39 == 0 ? 0 : (row1.DocType == "INV" || row1.DocType == "ADR" ? 1 : (row1.DocType == "ACR" ? 1 : 0));
    PXUIFieldAttribute.SetVisible<APInvoice.intercompanyInvoiceNoteID>(cache23, row4, num40 != 0);
    PXUIFieldAttribute.SetEnabled<APInvoice.intercompanyInvoiceNoteID>(cache, (object) row1, row1.Status != "R");
    PX.Objects.AR.ARTran arTran = (PX.Objects.AR.ARTran) null;
    PX.Objects.AR.ARInvoice arInvoice = (PX.Objects.AR.ARInvoice) null;
    if (cache.GetStatus((object) row1) == PXEntryStatus.Inserted)
    {
      arTran = (PX.Objects.AR.ARTran) PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AR.ARInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARInvoice.docType>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<PX.Objects.AR.ARInvoice.noteID, Equal<P.AsGuid>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.deferredCode, IBqlString>.IsNotNull>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row1.IntercompanyInvoiceNoteID);
      arInvoice = (PX.Objects.AR.ARInvoice) PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AR.ARInvoice.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row1.IntercompanyInvoiceNoteID);
    }
    if (arTran != null)
      cache.RaiseExceptionHandling<APInvoice.refNbr>((object) row1, (object) row1.RefNbr, (Exception) new PXSetPropertyException("The deferral codes from the AR document cannot be copied.", PXErrorLevel.Warning));
    if (arInvoice != null)
    {
      nullable2 = arInvoice.CuryOrigDocAmt;
      Decimal? nullable3 = row1.CuryOrigDocAmt;
      if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
      {
        PXCache pxCache = cache;
        nullable1 = this.APSetup.Current.RequireControlTotal;
        string name = nullable1.GetValueOrDefault() ? "CuryOrigDocAmt" : "CuryDocBal";
        APInvoice row5 = row1;
        nullable1 = this.APSetup.Current.RequireControlTotal;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal?> newValue = (ValueType) (nullable1.GetValueOrDefault() ? row1.CuryOrigDocAmt : row1.CuryDocBal);
        PXSetPropertyException propertyException = new PXSetPropertyException("The document amount differs from the document amount in the related AR document.", PXErrorLevel.Warning);
        pxCache.RaiseExceptionHandling(name, (object) row5, (object) newValue, (Exception) propertyException);
      }
      nullable3 = arInvoice.CuryTaxTotal;
      nullable2 = row1.CuryTaxTotal;
      if (!(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue))
        cache.RaiseExceptionHandling<APInvoice.curyTaxTotal>((object) row1, (object) row1.CuryTaxTotal, (Exception) new PXSetPropertyException("The document's tax total differs from the tax total in the related AR document.", PXErrorLevel.Warning));
    }
    PXAction<APInvoice> reverseInvoice = this.reverseInvoice;
    int num41;
    if (this.Document.Current.OrigModule != "TX")
    {
      if (invoiceState.IsDocumentPrepaymentInvoice)
      {
        nullable1 = row1.PendingPayment;
        num41 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num41 = 1;
    }
    else
      num41 = 0;
    reverseInvoice.SetEnabled(num41 != 0);
    this.duplicatefilter.Current.Label = string.Format(PXMessages.LocalizeNoPrefix("A document of the {0} type with the {1} reference number already exists in the system. To proceed, enter another reference number for the reversing document."), (object) PXMessages.LocalizeNoPrefix(APDocType.Labels[((IEnumerable<string>) APDocType.Values).FindIndex<string>((Predicate<string>) (_ => _ == this.GetReversingDocType(this.Document.Current.DocType)))]), (object) this.Document.Current.RefNbr);
  }

  private PX.Objects.PO.POOrder FindPOOrderWithDifferentTaxCalcMode(APInvoice doc)
  {
    IEnumerable<PXResult<APTran>> source1 = PXSelectBase<APTran, PXSelectJoin<APTran, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<APTran.pOOrderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<APTran.pONbr>>>>, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<PX.Objects.PO.POOrder.taxCalcMode, NotEqual<Current<APInvoice.taxCalcMode>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new APInvoice[1]
    {
      doc
    }).AsEnumerable<PXResult<APTran>>();
    if (source1.Any<PXResult<APTran>>())
      return PXResult.Unwrap<PX.Objects.PO.POOrder>((object) source1.First<PXResult<APTran>>());
    IEnumerable<PXResult<APTran>> source2 = PXSelectBase<APTran, PXSelectJoin<APTran, InnerJoin<PX.Objects.PO.POReceiptLine, On<PX.Objects.PO.POReceiptLine.receiptType, Equal<APTran.receiptType>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<APTran.receiptNbr>, And<PX.Objects.PO.POReceiptLine.lineNbr, Equal<APTran.receiptLineNbr>>>>, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>>>>>, Where<APTran.tranType, Equal<Current<APInvoice.docType>>, And<APTran.refNbr, Equal<Current<APInvoice.refNbr>>, And<PX.Objects.PO.POOrder.taxCalcMode, NotEqual<Current<APInvoice.taxCalcMode>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new APInvoice[1]
    {
      doc
    }).AsEnumerable<PXResult<APTran>>();
    return source2.Any<PXResult<APTran>>() ? PXResult.Unwrap<PX.Objects.PO.POOrder>((object) source2.First<PXResult<APTran>>()) : (PX.Objects.PO.POOrder) null;
  }

  protected virtual void APInvoice_PayTypeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<APInvoice.payAccountID>(e.Row);
  }

  protected virtual void APInvoice_PayAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is APInvoice row))
      return;
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectBase<PX.Objects.CA.CashAccount, PXSelectReadonly<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<APInvoice.payAccountID>>>>.Config>.Select((PXGraph) this, e.NewValue);
    if (cashAccount == null || !cashAccount.RestrictVisibilityWithBranch.GetValueOrDefault())
      return;
    int? branchId1 = cashAccount.BranchID;
    int? branchId2 = row.BranchID;
    if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue)
      return;
    e.NewValue = (object) null;
  }

  protected virtual void APInvoice_BranchID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<APInvoice.payAccountID>(e.Row);
    sender.SetDefaultExt<APInvoice.externalTaxExemptionNumber>(e.Row);
    sender.SetDefaultExt<APInvoice.entityUsageType>(e.Row);
  }

  protected virtual void APInvoice_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is APInvoice row))
      return;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  public virtual string GetDefaultTaxZone(APInvoice row)
  {
    string defaultTaxZone = (string) null;
    if (row == null)
      return defaultTaxZone;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
    {
      PX.Objects.CR.Location location = (PX.Objects.CR.Location) PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, (object) row.SuppliedByVendorID, (object) row.SuppliedByVendorLocationID);
      if (location != null && !string.IsNullOrEmpty(location.VTaxZoneID))
        defaultTaxZone = location.VTaxZoneID;
    }
    else
    {
      PX.Objects.CR.Location location = (PX.Objects.CR.Location) PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, (object) row.VendorID, (object) row.VendorLocationID);
      if (location != null && !string.IsNullOrEmpty(location.VTaxZoneID))
        defaultTaxZone = location.VTaxZoneID;
    }
    return defaultTaxZone;
  }

  private void ValidateAPAndReclassificationAccountsAndSubs(PXCache sender, APInvoice invoice)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.prebooking>())
      return;
    string message = (string) null;
    bool flag1 = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.subAccount>();
    int? prebookAcctId = invoice.PrebookAcctID;
    int? nullable = invoice.APAccountID;
    bool flag2 = prebookAcctId.GetValueOrDefault() == nullable.GetValueOrDefault() & prebookAcctId.HasValue == nullable.HasValue;
    if (flag2 && !flag1)
      message = "The AP Account and the Reclassification Account boxes should not have the same accounts specified.";
    else if (flag2 & flag1)
    {
      nullable = invoice.PrebookSubID;
      int? apSubId = invoice.APSubID;
      if (nullable.GetValueOrDefault() == apSubId.GetValueOrDefault() & nullable.HasValue == apSubId.HasValue)
        message = "The AP Account (subaccount) and the Reclassification Account (subaccount) boxes should not have the same account-subaccount pairs specified.";
    }
    if (message == null)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException(message, PXErrorLevel.Error);
    PXFieldState stateExt1 = (PXFieldState) sender.GetStateExt<APInvoice.prebookAcctID>((object) invoice);
    sender.RaiseExceptionHandling<APInvoice.prebookAcctID>((object) invoice, stateExt1.Value, (Exception) propertyException);
    PXFieldState stateExt2 = (PXFieldState) sender.GetStateExt<APInvoice.prebookSubID>((object) invoice);
    sender.RaiseExceptionHandling<APInvoice.prebookSubID>((object) invoice, stateExt2.Value, (Exception) propertyException);
  }

  protected virtual void APInvoice_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is APInvoice row))
      return;
    bool? nullable1 = row.Released;
    Decimal? nullable2;
    Decimal? nullable3;
    if (!nullable1.GetValueOrDefault())
    {
      if (e.ExternalCall && (!sender.ObjectsEqual<APInvoice.docDate, APInvoice.retainageApply>(e.OldRow, e.Row) && row.OrigDocType == null && row.OrigRefNbr == null || !sender.ObjectsEqual<APInvoice.vendorLocationID>(e.OldRow, e.Row) || (this.changedSuppliedByVendorLocation = !sender.ObjectsEqual<APInvoice.suppliedByVendorLocationID>(e.OldRow, e.Row))))
        this._discountEngine.AutoRecalculatePricesAndDiscounts(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (APTran) null, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, row.VendorLocationID, row.DocDate, this.GetDefaultAPDiscountCalculationOptions(row));
      if (sender.GetStatus((object) row) != PXEntryStatus.Deleted && !sender.ObjectsEqual<APInvoice.curyDiscTot>(e.OldRow, e.Row))
      {
        if (!sender.Graph.IsImport)
        {
          try
          {
            this.AddDiscount(sender, row);
          }
          catch (PXException ex)
          {
            sender.RaiseExceptionHandling<APInvoice.curyDiscTot>((object) row, (object) row.CuryDiscTot, (Exception) ex);
          }
        }
        if (!this._discountEngine.IsInternalDiscountEngineCall && e.ExternalCall)
        {
          this._discountEngine.SetTotalDocDiscount(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.CuryDiscTot, this.GetDefaultAPDiscountCalculationOptions(row));
          this.RecalculateTotalDiscount();
        }
      }
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Prebooked;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = this.APSetup.Current.RequireControlTotal;
          if (!nullable1.GetValueOrDefault())
          {
            nullable2 = row.CuryDocBal;
            Decimal? curyOrigDocAmt = row.CuryOrigDocAmt;
            if (!(nullable2.GetValueOrDefault() == curyOrigDocAmt.GetValueOrDefault() & nullable2.HasValue == curyOrigDocAmt.HasValue))
            {
              nullable3 = row.CuryDocBal;
              if (nullable3.HasValue)
              {
                nullable3 = row.CuryDocBal;
                Decimal num = 0M;
                if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
                {
                  sender.SetValueExt<APInvoice.curyOrigDocAmt>((object) row, (object) row.CuryDocBal);
                  goto label_18;
                }
              }
              sender.SetValueExt<APInvoice.curyOrigDocAmt>((object) row, (object) 0M);
            }
          }
label_18:
          if (row.DocType == "PPM" && !row.DueDate.HasValue)
            sender.SetValue<APInvoice.dueDate>(e.Row, (object) this.Accessinfo.BusinessDate);
        }
      }
      if (this.ShouldPerformBalanceVerification(row))
      {
        nullable3 = row.CuryDocBal;
        nullable2 = row.CuryOrigDocAmt;
        if (!(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue) && !this.IsImport)
        {
          sender.RaiseExceptionHandling<APInvoice.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
        }
        else
        {
          nullable2 = row.CuryOrigDocAmt;
          Decimal num = 0M;
          if (nullable2.GetValueOrDefault() < num & nullable2.HasValue)
          {
            nullable1 = this.APSetup.Current.RequireControlTotal;
            if (nullable1.GetValueOrDefault())
              sender.RaiseExceptionHandling<APInvoice.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
            else
              sender.RaiseExceptionHandling<APInvoice.curyDocBal>((object) row, (object) row.CuryDocBal, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
          }
          else
          {
            nullable1 = this.APSetup.Current.RequireControlTotal;
            if (nullable1.GetValueOrDefault())
              sender.RaiseExceptionHandling<APInvoice.curyOrigDocAmt>((object) row, (object) null, (Exception) null);
            else
              sender.RaiseExceptionHandling<APInvoice.curyDocBal>((object) row, (object) null, (Exception) null);
          }
        }
      }
    }
    nullable1 = this.APSetup.Current.RequireControlTaxTotal;
    bool flag = nullable1.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>();
    int num1;
    if (this.ShouldPerformBalanceVerification(row))
    {
      nullable2 = row.CuryTaxTotal;
      nullable3 = row.CuryTaxAmt;
      num1 = !(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    int num2 = flag ? 1 : 0;
    if ((num1 & num2) != 0)
      sender.RaiseExceptionHandling<APInvoice.curyTaxAmt>((object) row, (object) row.CuryTaxAmt, (Exception) new PXSetPropertyException("Tax Amount must be equal to Tax Total."));
    else if (flag)
    {
      sender.RaiseExceptionHandling<APInvoice.curyTaxAmt>((object) row, (object) null, (Exception) null);
    }
    else
    {
      PXCache pxCache = sender;
      APInvoice data = row;
      nullable3 = row.CuryTaxTotal;
      Decimal? nullable4;
      if (nullable3.HasValue)
      {
        nullable3 = row.CuryTaxTotal;
        Decimal num3 = 0M;
        if (!(nullable3.GetValueOrDefault() == num3 & nullable3.HasValue))
        {
          nullable4 = row.CuryTaxTotal;
          goto label_41;
        }
      }
      nullable4 = new Decimal?(0M);
label_41:
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) nullable4;
      pxCache.SetValueExt<APInvoice.curyTaxAmt>((object) data, (object) local);
    }
  }

  protected virtual bool ShouldPerformBalanceVerification(APInvoice doc)
  {
    return !doc.Hold.GetValueOrDefault() && !doc.Released.GetValueOrDefault() && !doc.Prebooked.GetValueOrDefault();
  }

  protected virtual void APInvoice_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if ((EPExpenseClaim) this.expenseclaim.Select() != null)
      throw new PXException("AP document created as a result of expense claim release cannot be deleted.");
  }

  /// <summary>
  /// Sets Expense Account for items with Accrue Cost = true. See implementation in CostAccrual extension.
  /// </summary>
  public virtual void SetExpenseAccount(PXCache sender, PXFieldDefaultingEventArgs e)
  {
  }

  protected virtual void APTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (row == null)
      return;
    bool? nullable1 = row.AccrueCost;
    if (nullable1.GetValueOrDefault())
      this.SetExpenseAccount(sender, e);
    PX.Objects.AP.APSetup current1 = this.APSetup.Current;
    int num1;
    if (current1 == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = current1.MigrationMode;
      num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int? nullable2;
    if (num1 != 0)
    {
      nullable2 = row.InventoryID;
      if (nullable2.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) this.nonStockItem.Select((object) row.InventoryID);
        int num2;
        if (inventoryItem == null)
        {
          num2 = 0;
        }
        else
        {
          nullable1 = inventoryItem.StkItem;
          num2 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num2 != 0)
        {
          e.NewValue = (object) null;
          e.Cancel = true;
          return;
        }
      }
    }
    if (this.vendor.Current == null || this.IsCopyPasteContext || this.IsReverseContext)
      return;
    nullable2 = row.InventoryID;
    if (!nullable2.HasValue && (this.vendor.Current.Type == "VE" || this.vendor.Current.Type == "VC"))
    {
      PX.Objects.CR.Location current2 = this.location.Current;
      int num3;
      if (current2 == null)
      {
        num3 = 0;
      }
      else
      {
        nullable2 = current2.VExpenseAcctID;
        num3 = nullable2.HasValue ? 1 : 0;
      }
      if (num3 != 0)
        goto label_24;
    }
    nullable2 = row.InventoryID;
    if (nullable2.HasValue)
    {
      nullable1 = this.vendor.Current.IsBranch;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.AccrueCost;
        if (!nullable1.GetValueOrDefault() && this.APSetup.Current?.IntercompanyExpenseAccountDefault == "L")
          goto label_24;
      }
    }
    nullable2 = row.InventoryID;
    if (nullable2.HasValue || !(this.vendor.Current.Type == "EP"))
      return;
    PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) this.EmployeeByVendor.Select();
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    nullable2 = epEmployee.ExpenseAcctID;
    object obj = (object) nullable2 ?? e.NewValue;
    defaultingEventArgs.NewValue = obj;
    e.Cancel = true;
    return;
label_24:
    e.NewValue = (object) this.location.Current.VExpenseAcctID;
    e.Cancel = true;
  }

  protected virtual void APTran_AccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    APTran row = e.Row as APTran;
    if (this.vendor.Current != null && this.vendor.Current.Vendor1099.Value)
      sender.SetDefaultExt<APTran.box1099>(e.Row);
    int? projectId = row.ProjectID;
    if (projectId.HasValue)
    {
      projectId = row.ProjectID;
      int? nullable = ProjectDefaultAttribute.NonProject();
      if (!(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue) || this.IsRecognitionProcess)
        return;
    }
    sender.SetDefaultExt<APTran.projectID>(e.Row);
  }

  /// <summary>
  /// Sets Expense Subaccount for items with Accrue Cost = true. See implementation in CostAccrual extension.
  /// </summary>
  public virtual object GetExpenseSub(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    return (object) null;
  }

  private bool RelatedWithPOOrLC(APTran tran)
  {
    if (!string.IsNullOrEmpty(tran.PONbr) || !string.IsNullOrEmpty(tran.ReceiptNbr))
      return true;
    return !string.IsNullOrEmpty(tran.LCDocType) && !string.IsNullOrEmpty(tran.LCRefNbr);
  }

  protected virtual void APTran_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is APTran row) || e.Cancel)
      return;
    PX.Objects.IN.InventoryItem data1 = (PX.Objects.IN.InventoryItem) this.nonStockItem.Select((object) row.InventoryID);
    if (!this.IsCopyPasteContext)
    {
      bool? nullable1;
      if (data1 != null)
      {
        nullable1 = data1.StkItem;
        if (nullable1.GetValueOrDefault() && FlaggedModeScopeBase<APInvoiceFillFromRecognizedScope>.IsActive && !string.IsNullOrEmpty(row.PONbr))
          goto label_5;
      }
      PX.Objects.AP.APSetup current = this.APSetup.Current;
      int num;
      if (current == null)
      {
        num = 1;
      }
      else
      {
        nullable1 = current.MigrationMode;
        num = !nullable1.GetValueOrDefault() ? 1 : 0;
      }
      if (num != 0 && data1 != null)
      {
        nullable1 = data1.StkItem;
        if (nullable1.GetValueOrDefault())
        {
          e.NewValue = (object) null;
          e.Cancel = true;
          return;
        }
      }
      if (this.vendor.Current?.Type == null || this.RelatedWithPOOrLC(row) || this.IsReverseContext)
        return;
      PX.Objects.EP.EPEmployee data2 = (PX.Objects.EP.EPEmployee) PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this, (object) ((int?) this.Document.Current?.EmployeeID ?? PXAccess.GetContactID()));
      PX.Objects.CR.Standalone.Location data3 = (PX.Objects.CR.Standalone.Location) PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<APTran.branchID>>>>.Config>.Select((PXGraph) this, (object) row.BranchID);
      PX.Objects.CT.Contract contract = (PX.Objects.CT.Contract) PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select((PXGraph) this, (object) row.ProjectID);
      string mask = this.APSetup.Current.ExpenseSubMask;
      int? nullable2 = new int?();
      if (contract == null || contract.BaseType == "C")
      {
        contract = (PX.Objects.CT.Contract) PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.nonProject, Equal<True>>>.Config>.Select((PXGraph) this);
        mask = mask.Replace("T", "J");
      }
      else
      {
        PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
        if (dirty != null)
          nullable2 = dirty.DefaultExpenseSubID;
      }
      int? nullable3 = new int?();
      switch (this.vendor.Current.Type)
      {
        case "VE":
        case "VC":
          nullable3 = (int?) this.Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.vExpenseSubID>((object) this.location.Current);
          break;
        case "EP":
          nullable3 = ((PX.Objects.EP.EPEmployee) this.EmployeeByVendor.Select()).ExpenseSubID ?? nullable3;
          break;
      }
      int? nullable4 = (int?) this.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.cOGSSubID>((object) data1);
      int? nullable5 = (int?) this.Caches[typeof (PX.Objects.EP.EPEmployee)].GetValue<PX.Objects.EP.EPEmployee.expenseSubID>((object) data2);
      int? nullable6 = (int?) this.Caches[typeof (PX.Objects.CR.Standalone.Location)].GetValue<PX.Objects.CR.Standalone.Location.cMPExpenseSubID>((object) data3);
      int? defaultExpenseSubId = contract.DefaultExpenseSubID;
      object newValue;
      if (row != null)
      {
        nullable1 = row.AccrueCost;
        if (nullable1.GetValueOrDefault())
        {
          newValue = this.GetExpenseSub(sender, e);
          goto label_25;
        }
      }
      newValue = (object) SubAccountMaskAttribute.MakeSub<PX.Objects.AP.APSetup.expenseSubMask>((PXGraph) this, mask, new object[6]
      {
        (object) nullable3,
        (object) nullable4,
        (object) nullable5,
        (object) nullable6,
        (object) defaultExpenseSubId,
        (object) nullable2
      }, new System.Type[6]
      {
        typeof (PX.Objects.CR.Location.vExpenseSubID),
        typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
        typeof (PX.Objects.EP.EPEmployee.expenseSubID),
        typeof (PX.Objects.CR.Location.cMPExpenseSubID),
        typeof (PMProject.defaultExpenseSubID),
        typeof (PMTask.defaultExpenseSubID)
      });
label_25:
      try
      {
        sender.RaiseFieldUpdating<APTran.subID>((object) row, ref newValue);
        sender.RaiseFieldVerifying<APTran.subID>((object) row, ref newValue);
        e.NewValue = (object) (int?) newValue;
      }
      catch (PXException ex)
      {
        if (FieldErrorScope.NeedsSet(typeof (APTran.subID)))
          throw;
        newValue = (object) null;
      }
      e.NewValue = (object) (int?) newValue;
      e.Cancel = true;
      return;
    }
label_5:
    e.NewValue = (object) row.SubID;
    e.Cancel = true;
  }

  protected virtual void APTran_LCTranID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.Cancel = true;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
  [APTax(typeof (APRegister), typeof (APTax), typeof (APTaxTran), typeof (APInvoice.taxCalcMode), typeof (APRegister.branchID), Inventory = typeof (APTran.inventoryID), UOM = typeof (APTran.uOM), LineQty = typeof (APTran.qty))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void APTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [DRTerms.Dates(typeof (APTran.dRTermStartDate), typeof (APTran.dRTermEndDate), typeof (APTran.inventoryID), typeof (APTran.deferredCode), typeof (APInvoice.hold))]
  protected virtual void APTran_RequiresTerms_CacheAttached(PXCache sender)
  {
  }

  protected virtual void APTran_TaxCategoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (row == null || (string) e.OldValue == row.TaxCategoryID)
      return;
    sender.SetDefaultExt<APTran.curyTaxableAmt>(e.Row);
    sender.SetDefaultExt<APTran.curyTaxAmt>(e.Row);
    APInvoice current = this.Document.Current;
    bool flag = current.IsOriginalRetainageDocument() || current.IsChildRetainageDocument();
    if (this.IsCopyPasteContext || this.IsReverseContext || flag || this.IsPPDCreateContext || this.UnattendedMode || this.IsImport || this.IsContractBasedAPI || row.PONbr != null || !this.APSetup.Current.RequireControlTotal.GetValueOrDefault() || !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>() || !(current.TaxCalcMode == "N"))
      return;
    Decimal? nullable1 = row.Qty;
    Decimal num1 = 0M;
    if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
      return;
    nullable1 = row.CuryLineAmt;
    if (!(nullable1.GetValueOrDefault() != 0M))
      return;
    PXResultset<APTax> pxResultset = PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>, And<APTax.lineNbr, Equal<Required<APTax.lineNbr>>>>>>.Config>.Select((PXGraph) this, (object) row.TranType, (object) row.RefNbr, (object) row.LineNbr);
    Decimal num2 = 0M;
    foreach (PXResult<APTax> pxResult in pxResultset)
    {
      APTax apTax = (APTax) pxResult;
      Decimal num3 = num2;
      nullable1 = apTax.CuryTaxAmt;
      Decimal num4 = nullable1.Value;
      num2 = num3 + num4;
    }
    Decimal? nullable2 = new Decimal?(TaxAttribute.CalcTaxableFromTotalAmount(sender, (object) row, current.TaxZoneID, row.TaxCategoryID, current.DocDate.Value, row.CuryLineAmt.Value + num2, false, TaxAttribute.TaxCalcLevelEnforcing.EnforceCalcOnItemAmount, current.TaxCalcMode));
    sender.SetValueExt<APTran.curyLineAmt>((object) row, (object) nullable2);
  }

  protected virtual void APTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (row == null || row.InventoryID.HasValue || this.vendor == null || this.vendor.Current == null || this.vendor.Current.TaxAgency.GetValueOrDefault() || TaxBaseAttribute.GetTaxCalc<APTran.taxCategoryID>(sender, (object) row) != TaxCalc.Calc || this.taxzone.Current == null || string.IsNullOrEmpty(this.taxzone.Current.DfltTaxCategoryID))
      return;
    e.NewValue = (object) this.taxzone.Current.DfltTaxCategoryID;
    e.Cancel = true;
  }

  protected virtual void APTran_UnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (row == null || row.InventoryID.HasValue)
      return;
    e.NewValue = (object) 0M;
    e.Cancel = true;
  }

  protected virtual void APTran_CuryUnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row1 = (APTran) e.Row;
    if (this.APSetup.Current.RequireControlTotal.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
    {
      Decimal? qty = row1.Qty;
      Decimal num = 0M;
      if (qty.GetValueOrDefault() == num & qty.HasValue)
      {
        e.NewValue = (object) 0.0M;
        e.Cancel = true;
        return;
      }
    }
    if (row1 != null)
    {
      int? nullable1 = row1.InventoryID;
      if (nullable1.HasValue && string.IsNullOrEmpty(row1.PONbr))
      {
        APInvoice current = this.Document.Current;
        if (current == null)
          return;
        nullable1 = current.VendorID;
        if (!nullable1.HasValue || row1 == null)
          return;
        if (row1.ManualPrice.GetValueOrDefault())
        {
          Decimal? curyUnitCost = row1.CuryUnitCost;
          if (curyUnitCost.HasValue)
          {
            PXFieldDefaultingEventArgs defaultingEventArgs = e;
            curyUnitCost = row1.CuryUnitCost;
            // ISSUE: variable of a boxed type
            __Boxed<Decimal> valueOrDefault = (ValueType) curyUnitCost.GetValueOrDefault();
            defaultingEventArgs.NewValue = (object) valueOrDefault;
            goto label_16;
          }
        }
        Decimal? nullable2 = new Decimal?();
        nullable1 = row1.InventoryID;
        if (nullable1.HasValue && row1.UOM != null)
        {
          System.DateTime date = this.Document.Current.DocDate.Value;
          nullable2 = APVendorPriceMaint.CalculateUnitCost(sender, row1.VendorID, current.VendorLocationID, row1.InventoryID, row1.SiteID, ((PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfo.Select()).GetCM(), row1.UOM, row1.Qty, date, row1.CuryUnitCost);
          e.NewValue = (object) nullable2;
        }
        if (!nullable2.HasValue)
        {
          PXFieldDefaultingEventArgs defaultingEventArgs = e;
          PXGraph graph = sender.Graph;
          APTran row2 = row1;
          int? vendorId = current.VendorID;
          int? vendorLocationId = current.VendorLocationID;
          System.DateTime? docDate = current.DocDate;
          string curyId = current.CuryID;
          int? inventoryId = row1.InventoryID;
          nullable1 = new int?();
          int? subItemID = nullable1;
          nullable1 = new int?();
          int? siteID = nullable1;
          string uom = row1.UOM;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) POItemCostManager.Fetch<APTran.inventoryID, APTran.curyInfoID>(graph, (object) row2, vendorId, vendorLocationId, docDate, curyId, inventoryId, subItemID, siteID, uom);
          defaultingEventArgs.NewValue = (object) local;
        }
        APVendorPriceMaint.CheckNewUnitCost<APTran, APTran.curyUnitCost>(sender, row1, e.NewValue);
label_16:
        e.Cancel = true;
        return;
      }
    }
    e.NewValue = sender.GetValue<APTran.curyUnitCost>(e.Row);
    e.Cancel = e.NewValue != null;
  }

  protected virtual void APTran_ManualPrice_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran) || sender.Graph.IsCopyPasteContext)
      return;
    sender.SetDefaultExt<APTran.curyUnitCost>(e.Row);
  }

  protected virtual void APTran_CuryLineAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APInvoice current = this.Document.Current;
    APTran row = e.Row as APTran;
    if (current == null || row == null || this.IsImport || !this.APSetup.Current.RequireControlTotal.GetValueOrDefault() || !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>() || current.RetainageApply.GetValueOrDefault())
      return;
    Decimal? nullable1 = new Decimal?(0M);
    if (string.IsNullOrEmpty(row.TaxCategoryID))
      sender.SetDefaultExt<APTran.taxCategoryID>((object) row);
    nullable1 = new Decimal?(TaxAttribute.CalcResidualAmt(sender, (object) row, current.TaxZoneID, row.TaxCategoryID, current.DocDate.Value, current.TaxCalcMode, current.CuryOrigDocAmt.Value, current.CuryLineTotal.Value, current.CuryTaxTotal.Value));
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    Decimal? nullable2 = nullable1;
    Decimal num = 0M;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (nullable2.GetValueOrDefault() >= num & nullable2.HasValue ? nullable1 : new Decimal?(0M));
    defaultingEventArgs.NewValue = (object) local;
    e.Cancel = true;
  }

  protected virtual void APTran_LCLineNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (this.APSetup.Current.RequireControlTotal.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
    {
      Decimal? qty = row.Qty;
      Decimal num = 0M;
      if (qty.GetValueOrDefault() == num & qty.HasValue)
        return;
    }
    sender.SetDefaultExt<APTran.unitCost>((object) row);
    sender.SetDefaultExt<APTran.curyUnitCost>((object) row);
    sender.SetValue<APTran.unitCost>((object) row, (object) null);
  }

  protected virtual void APTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (!string.IsNullOrEmpty(row.PONbr) && row.POAccrualType == "O")
    {
      string oldValue = (string) e.OldValue;
      Decimal? nullable = row.Qty;
      Decimal num1 = nullable.GetValueOrDefault();
      nullable = row.CuryUnitCost;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      if (string.IsNullOrEmpty(oldValue) || string.IsNullOrEmpty(row.UOM) || !(oldValue != row.UOM))
        return;
      if (num1 != 0M)
      {
        Decimal num2 = INUnitAttribute.ConvertToBase<APTran.inventoryID>(sender, e.Row, oldValue, num1, INPrecision.NOROUND);
        num1 = INUnitAttribute.ConvertFromBase<APTran.inventoryID>(sender, e.Row, row.UOM, num2, INPrecision.QUANTITY);
      }
      if (valueOrDefault != 0M)
      {
        Decimal num3 = INUnitAttribute.ConvertFromBase<APTran.inventoryID>(sender, e.Row, oldValue, valueOrDefault, INPrecision.NOROUND);
        valueOrDefault = INUnitAttribute.ConvertToBase<APTran.inventoryID>(sender, e.Row, row.UOM, num3, INPrecision.UNITCOST);
      }
      sender.SetValueExt<APTran.qty>(e.Row, (object) num1);
      sender.SetValueExt<APTran.curyUnitCost>(e.Row, (object) valueOrDefault);
    }
    else
    {
      if (this.APSetup.Current.RequireControlTotal.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
      {
        Decimal? qty = row.Qty;
        Decimal num = 0M;
        if (qty.GetValueOrDefault() == num & qty.HasValue)
          return;
      }
      sender.SetDefaultExt<APTran.unitCost>((object) row);
      sender.SetDefaultExt<APTran.curyUnitCost>((object) row);
      sender.SetValue<APTran.unitCost>((object) row, (object) null);
    }
  }

  protected virtual void APTran_Qty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    Decimal? qty = row.Qty;
    Decimal num = 0M;
    if (qty.GetValueOrDefault() == num & qty.HasValue)
    {
      sender.SetValueExt<APTran.curyDiscAmt>((object) row, (object) 0M);
      sender.SetValueExt<APTran.discPct>((object) row, (object) 0M);
    }
    else
      sender.SetDefaultExt<APTran.curyUnitCost>(e.Row);
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PopupMessage]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  protected virtual void APTran_InventoryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void APTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row) || !string.IsNullOrEmpty(row.ReceiptNbr) || !string.IsNullOrEmpty(row.PONbr))
      return;
    sender.SetDefaultExt<APTran.accrueCost>(e.Row);
    using (new FieldErrorScope(typeof (APTran.subID), FieldErrorScope.Action.Reset))
      sender.SetDefaultExt<APTran.accountID>((object) row);
    using (new FieldErrorScope(typeof (APTran.subID), FieldErrorScope.Action.Set))
      sender.SetDefaultExt<APTran.subID>((object) row);
    sender.SetDefaultExt<APTran.taxCategoryID>((object) row);
    sender.SetDefaultExt<APTran.deferredCode>((object) row);
    sender.SetDefaultExt<APTran.uOM>((object) row);
    if (this.APSetup.Current.RequireControlTotal.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
    {
      Decimal? qty = row.Qty;
      Decimal num = 0M;
      if (qty.GetValueOrDefault() == num & qty.HasValue)
        goto label_16;
    }
    if (e.ExternalCall && row != null)
      row.CuryUnitCost = new Decimal?(0M);
    sender.SetDefaultExt<APTran.unitCost>((object) row);
    sender.SetDefaultExt<APTran.curyUnitCost>((object) row);
    sender.SetValue<APTran.unitCost>((object) row, (object) null);
label_16:
    sender.SetDefaultExt<APTran.costCodeID>((object) row);
    PX.Objects.IN.InventoryItem data = (PX.Objects.IN.InventoryItem) this.nonStockItem.Select((object) row.InventoryID);
    if (data != null)
      row.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(this.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) data, "Descr", this.vendor.Current?.LocaleName);
    else
      row.TranDesc = (string) null;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<APTran.curyRetainageAmt> e)
  {
    if (!(e.Row is APTran row))
      return;
    Decimal? nullable;
    Decimal availableRetainageAmount;
    if (!(row.TranType == "PPM"))
    {
      nullable = row.CuryLineAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = row.CuryDiscAmt;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      availableRetainageAmount = valueOrDefault1 - valueOrDefault2;
    }
    else
    {
      nullable = row.CuryPrepaymentAmt;
      availableRetainageAmount = nullable.GetValueOrDefault();
    }
    nullable = (Decimal?) e.NewValue;
    Decimal valueOrDefault = nullable.GetValueOrDefault();
    RetainageAmountAttribute.AssertRetainageAmount(availableRetainageAmount, valueOrDefault);
  }

  protected virtual void APTran_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    if (this.APSetup.Current.ExpenseSubMask != null && this.APSetup.Current.ExpenseSubMask.Contains("J") && !this.RelatedWithPOOrLC(row))
      sender.SetDefaultExt<APTran.subID>((object) row);
    foreach (PXResult<APTran> pxResult in this.Discount_Row.Select())
    {
      APTran apTran = (APTran) pxResult;
      this.SetProjectIDForDiscountTran(apTran);
      if (!ProjectDefaultAttribute.IsNonProject(apTran.ProjectID))
      {
        try
        {
          this.SetTaskIDForDiscountTran(apTran);
        }
        catch (PXException ex)
        {
          PMProject pmProject = (PMProject) PXSelectorAttribute.Select<APTran.projectID>(sender, (object) row);
          sender.RaiseExceptionHandling<APTran.projectID>((object) row, (object) pmProject?.ContractCD, (Exception) ex);
        }
      }
      else
        apTran.TaskID = new int?();
    }
  }

  protected virtual void APTran_TaskID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    if (this.APSetup.Current.ExpenseSubMask != null && this.APSetup.Current.ExpenseSubMask.Contains("T"))
      sender.SetDefaultExt<APTran.subID>((object) row);
    sender.SetDefaultExt<APTran.costCodeID>((object) row);
  }

  protected virtual void APTran_TaskID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is APTran row) || !(e.NewValue is int))
      return;
    this.CheckOrderTaskRule(sender, row, (int?) e.NewValue);
  }

  protected virtual void APTran_DefScheduleID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    DRSchedule drSchedule = (DRSchedule) PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, (object) ((APTran) e.Row).DefScheduleID);
    if (drSchedule == null)
      return;
    APTran apTran = (APTran) PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.lineNbr, Equal<Required<APTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, (object) drSchedule.DocType, (object) drSchedule.RefNbr, (object) drSchedule.LineNbr);
    if (apTran == null)
      return;
    ((APTran) e.Row).DeferredCode = apTran.DeferredCode;
  }

  protected virtual void APTran_DiscountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row) || !e.ExternalCall)
      return;
    this._discountEngine.UpdateManualLineDiscount(sender, (PXSelectBase<APTran>) this.Transactions, row, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.BranchID, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
  }

  protected virtual void APTran_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    APInvoice current1 = this.Document.Current;
    bool? nullable1;
    int num1;
    if (current1 != null)
    {
      nullable1 = current1.Prebooked;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = current1.Released;
        bool flag1 = false;
        if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
        {
          nullable1 = current1.Voided;
          bool flag2 = false;
          num1 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
          goto label_8;
        }
      }
      num1 = 0;
    }
    else
      num1 = 0;
label_8:
    bool flag3 = num1 != 0;
    bool flag4 = !string.IsNullOrEmpty(row.ReceiptNbr);
    bool flag5 = !string.IsNullOrEmpty(row.PONbr);
    bool flag6 = flag5 && row.POAccrualType == "O";
    bool hasValue = row.OrigLineNbr.HasValue;
    bool flag7 = false;
    int num2;
    if (current1 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable1 = current1.Released;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    bool flag8 = num2 != 0;
    bool flag9 = current1?.DocType == "ADR";
    int num3;
    if (current1 != null)
    {
      nullable1 = current1.Released;
      if (nullable1.GetValueOrDefault())
      {
        num3 = 1;
        goto label_17;
      }
    }
    if (current1 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable1 = current1.Prebooked;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
label_17:
    bool flag10 = num3 != 0;
    if (row.LCDocType != null && row.LCRefNbr != null)
      flag7 = true;
    bool flag11 = false;
    PXCache cache1 = cache;
    APTran data1 = row;
    APInvoice current2 = this.Document.Current;
    int num4;
    if (current2 == null)
    {
      num4 = 0;
    }
    else
    {
      nullable1 = current2.RetainageApply;
      num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num5 = num4 == 0 ? 0 : (!flag10 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<APTran.retainagePct>(cache1, (object) data1, num5 != 0);
    PXCache cache2 = cache;
    APTran data2 = row;
    APInvoice current3 = this.Document.Current;
    int num6;
    if (current3 == null)
    {
      num6 = 0;
    }
    else
    {
      nullable1 = current3.RetainageApply;
      num6 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num7 = num6 == 0 ? 0 : (!flag10 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<APTran.curyRetainageAmt>(cache2, (object) data2, num7 != 0);
    PXUIFieldAttribute.SetEnabled<APTran.manualDisc>(cache, (object) row, !flag10);
    PXUIFieldAttribute.SetEnabled<APTran.curyDiscAmt>(cache, (object) row, !flag10);
    PXUIFieldAttribute.SetEnabled<APTran.discPct>(cache, (object) row, !flag10);
    PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(cache, (object) row, !flag10);
    PXUIFieldAttribute.SetEnabled<APTran.discountID>(cache, (object) row, !flag10);
    PX.Objects.AP.APSetup current4 = this.APSetup.Current;
    int num8;
    if (current4 == null)
    {
      num8 = 1;
    }
    else
    {
      nullable1 = current4.MigrationMode;
      num8 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int? nullable2;
    if (num8 != 0)
    {
      nullable2 = row.InventoryID;
      if (nullable2.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, (object) row.InventoryID);
        int num9;
        if (inventoryItem == null)
        {
          num9 = 0;
        }
        else
        {
          nullable1 = inventoryItem.StkItem;
          num9 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        flag11 = num9 != 0;
      }
    }
    if (flag11 && row.TranType != "PPM" && row.POAccrualType != "O" && !flag4)
    {
      nullable1 = current1.IsRetainageDocument;
      if (!nullable1.GetValueOrDefault())
        cache.RaiseExceptionHandling<APTran.inventoryID>((object) row, (object) row.InventoryID, (Exception) new PXSetPropertyException("A line of the \"Goods for IN\" type must be linked to a purchase receipt line.", PXErrorLevel.Warning));
    }
    if (!flag7)
    {
      if (flag5 | flag4)
      {
        PXUIFieldAttribute.SetEnabled<APTran.inventoryID>(cache, (object) row, false);
        PXUIFieldAttribute.SetEnabled<APTran.uOM>(cache, (object) row, flag6 && !hasValue && !flag10);
        if (flag6 & hasValue && POLineType.UsePOAccrual(row.LineType))
        {
          PXUIFieldAttribute.SetEnabled<APTran.qty>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.curyUnitCost>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.curyLineAmt>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.discPct>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.curyDiscAmt>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.manualDisc>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.discountID>(cache, (object) row, false);
        }
      }
      int num10;
      if (current1 != null)
      {
        nullable1 = current1.Prebooked;
        bool flag12 = false;
        if (nullable1.GetValueOrDefault() == flag12 & nullable1.HasValue)
        {
          nullable1 = current1.Released;
          bool flag13 = false;
          if (nullable1.GetValueOrDefault() == flag13 & nullable1.HasValue)
          {
            nullable1 = current1.Voided;
            bool flag14 = false;
            num10 = nullable1.GetValueOrDefault() == flag14 & nullable1.HasValue ? 1 : 0;
            goto label_47;
          }
        }
        num10 = 0;
      }
      else
        num10 = 0;
label_47:
      bool flag15 = num10 != 0;
      PXUIFieldAttribute.SetEnabled<APTran.defScheduleID>(cache, (object) row, flag15 && row.TranType == "ADR");
      PXCache cache3 = cache;
      APTran data3 = row;
      int num11;
      if (flag15)
      {
        nullable2 = row.DefScheduleID;
        num11 = !nullable2.HasValue ? 1 : 0;
      }
      else
        num11 = 0;
      PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(cache3, (object) data3, num11 != 0);
      if (current1 != null)
      {
        nullable1 = current1.Released;
        bool flag16 = false;
        if (nullable1.GetValueOrDefault() == flag16 & nullable1.HasValue)
        {
          nullable1 = current1.Prebooked;
          bool flag17 = false;
          if (nullable1.GetValueOrDefault() == flag17 & nullable1.HasValue)
          {
            bool flag18 = false;
            if (!string.IsNullOrEmpty(row.PONbr))
            {
              PX.Objects.PO.POOrder poOrder = (PX.Objects.PO.POOrder) PXSelectBase<PX.Objects.PO.POOrder, PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<Required<PX.Objects.PO.POOrder.orderType>>, And<PX.Objects.PO.POOrder.orderNbr, Equal<Required<PX.Objects.PO.POOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, (object) row.POOrderType, (object) row.PONbr);
              if (!string.IsNullOrEmpty(poOrder?.OrderNbr))
                flag18 = current1.CuryID != poOrder.CuryID;
            }
            if (flag18)
            {
              cache.RaiseExceptionHandling<APTran.curyLineAmt>((object) row, (object) row.CuryLineAmt, (Exception) new PXSetPropertyException("The currency of the source document is different from the one of this document. The value may be recalculated or require correction.", PXErrorLevel.Warning));
              cache.RaiseExceptionHandling<APTran.curyUnitCost>((object) row, (object) row.CuryUnitCost, (Exception) new PXSetPropertyException("The currency of the source document is different from the one of this document. The value may be recalculated or require correction.", PXErrorLevel.Warning));
            }
          }
        }
      }
      if (flag5 | flag4)
      {
        bool flag19 = row.TranType == "PPM";
        bool isEnabled = !flag8 && !flag3 && !POLineType.UsePOAccrual(row.LineType) | flag19;
        PXUIFieldAttribute.SetEnabled<APTran.accountID>(cache, (object) row, isEnabled);
        PXUIFieldAttribute.SetEnabled<APTran.subID>(cache, (object) row, isEnabled);
        PXUIFieldAttribute.SetEnabled<APTran.branchID>(cache, (object) row, isEnabled);
      }
      else
      {
        PXUIFieldAttribute.SetEnabled<APTran.accountID>(cache, (object) row, !flag11 && !flag8);
        PXUIFieldAttribute.SetEnabled<APTran.subID>(cache, (object) row, !flag11 && !flag8);
      }
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<APTran.qty>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.accountID>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.subID>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.uOM>(cache, (object) row, false);
      if (flag9)
      {
        nullable2 = row.OrigLineNbr;
        if (nullable2.HasValue)
        {
          PXUIFieldAttribute.SetEnabled<APTran.curyUnitCost>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.curyLineAmt>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.discPct>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.curyDiscAmt>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.manualDisc>(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.taxCategoryID>(cache, (object) row, false);
        }
      }
    }
    bool flag20 = !flag5;
    PX.Objects.IN.InventoryItem inventoryItem1 = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select(cache, e.Row, cache.GetField(typeof (APTran.inventoryID)));
    if (inventoryItem1 != null)
    {
      nullable1 = inventoryItem1.StkItem;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = inventoryItem1.NonStockReceipt;
        if (!nullable1.GetValueOrDefault())
          flag20 = true;
      }
    }
    PXCache cache4 = cache;
    APTran data4 = row;
    int num12;
    if (flag20)
    {
      if (flag8)
      {
        Vendor current5 = this.vendor.Current;
        int num13;
        if (current5 == null)
        {
          num13 = 0;
        }
        else
        {
          nullable1 = current5.Vendor1099;
          num13 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num13 != 0)
        {
          num12 = !(current1.Status != "X") ? 1 : 0;
          goto label_77;
        }
      }
      num12 = 1;
    }
    else
      num12 = 0;
label_77:
    PXUIFieldAttribute.SetEnabled<APTran.projectID>(cache4, (object) data4, num12 != 0);
    PXCache cache5 = cache;
    APTran data5 = row;
    int num14;
    if (flag20)
    {
      if (flag8)
      {
        Vendor current6 = this.vendor.Current;
        int num15;
        if (current6 == null)
        {
          num15 = 0;
        }
        else
        {
          nullable1 = current6.Vendor1099;
          num15 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num15 != 0)
        {
          num14 = !(current1.Status != "X") ? 1 : 0;
          goto label_86;
        }
      }
      num14 = 1;
    }
    else
      num14 = 0;
label_86:
    PXUIFieldAttribute.SetEnabled<APTran.taskID>(cache5, (object) data5, num14 != 0);
    PXUIFieldAttribute.SetEnabled<APTran.lCDocType>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APTran.lCRefNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APTran.lCLineNbr>(cache, (object) row, false);
    if (current1 != null)
    {
      nullable1 = current1.IsRetainageDocument;
      if (nullable1.GetValueOrDefault())
      {
        Vendor current7 = this.vendor.Current;
        int num16;
        if (current7 == null)
        {
          num16 = 0;
        }
        else
        {
          nullable1 = current7.Vendor1099;
          num16 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num16 != 0)
        {
          PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
          PXUIFieldAttribute.SetEnabled<APTran.box1099>(cache, (object) row, true);
        }
      }
    }
    if (current1 != null)
    {
      nullable1 = current1.IsMigratedRecord;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = current1.Released;
        if (!nullable1.GetValueOrDefault())
        {
          PXUIFieldAttribute.SetEnabled<APTran.defScheduleID>(this.Transactions.Cache, (object) null, false);
          PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(this.Transactions.Cache, (object) null, false);
          PXUIFieldAttribute.SetEnabled<APTran.dRTermStartDate>(this.Transactions.Cache, (object) null, false);
          PXUIFieldAttribute.SetEnabled<APTran.dRTermEndDate>(this.Transactions.Cache, (object) null, false);
        }
      }
    }
    nullable1 = row.IsDirectTaxLine;
    if (nullable1.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled<APTran.retainagePct>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.curyRetainageAmt>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.manualDisc>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.curyDiscAmt>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.discPct>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.discountID>(cache, (object) row, false);
    }
    if (!(current1?.DocType == "PPI"))
      return;
    PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APTran.dRTermStartDate>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APTran.dRTermEndDate>(cache, (object) row, false);
  }

  protected virtual void APTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    int? nullable;
    if (row.LineType == "DS")
    {
      nullable = row.AccountID;
      nullable = nullable.HasValue ? row.SubID : throw new PXException("The document cannot be saved because no discount account is specified for the vendor. To proceed, specify a discount account on the Vendors (AP303000) form.");
      if (!nullable.HasValue)
        throw new PXException("The document cannot be saved because no discount subaccount is specified for the vendor. To proceed, specify a discount subaccount on the Vendors (AP303000) form.");
    }
    APInvoice current1 = this.Document.Current;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, (object) row.InventoryID);
    int num1;
    if ((inventoryItem != null ? (inventoryItem.StkItem.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      PX.Objects.AP.APSetup current2 = this.APSetup.Current;
      num1 = current2 != null ? (!current2.MigrationMode.GetValueOrDefault() ? 1 : 0) : 1;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXDefaultAttribute.SetPersistingCheck<APTran.accountID>(sender, (object) row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
    PXDefaultAttribute.SetPersistingCheck<APTran.subID>(sender, (object) row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
    {
      this.CheckOrderTaskRule(sender, row, row.TaskID);
      this.CheckProjectAccountRule(sender, row);
      Decimal? qty = row.Qty;
      Decimal num2 = 0M;
      if (!(qty.GetValueOrDefault() == num2 & qty.HasValue) && string.IsNullOrEmpty(row.UOM))
      {
        nullable = row.TaskID;
        if (nullable.HasValue)
        {
          nullable = row.InventoryID;
          if (nullable.HasValue && sender.RaiseExceptionHandling((string) null, (object) row, (object) null, (Exception) new PXSetPropertyException<APTran.uOM>("UOM is required if Quantity is not equal to zero.", PXErrorLevel.Error)))
            throw new PXSetPropertyException<APTran.uOM>("UOM is required if Quantity is not equal to zero.");
        }
      }
    }
    ScheduleHelper.DeleteAssociatedScheduleIfDeferralCodeChanged((PXGraph) this, (IDocumentLine) row);
    object projectId = (object) row.ProjectID;
    try
    {
      sender.RaiseFieldVerifying<APTran.projectID>((object) row, ref projectId);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling<APTran.projectID>((object) row, projectId, (Exception) ex);
    }
    if (!(row.LineType == "DS") || row.TaskID.HasValue || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
      return;
    this.SetTaskIDForDiscountTran(row);
  }

  protected virtual void APTran_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    TaxBaseAttribute.Calculate<APTran.taxCategoryID>(sender, e);
  }

  protected virtual void APTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row is APTran row && !sender.ObjectsEqual<APTran.accountID, APTran.deferredCode>(e.Row, e.OldRow) && !string.IsNullOrEmpty(row.DeferredCode))
    {
      DRDeferredCode drDeferredCode = (DRDeferredCode) PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, (object) row.DeferredCode);
      if (drDeferredCode != null)
      {
        int? accountId1 = drDeferredCode.AccountID;
        int? accountId2 = row.AccountID;
        if (accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue)
          sender.RaiseExceptionHandling<APTran.accountID>(e.Row, (object) row.AccountID, (Exception) new PXSetPropertyException("Transaction Account is same as Deferral Account specified in Deferred Code.", PXErrorLevel.Warning));
      }
    }
    if (!sender.ObjectsEqual<APTran.lCDocType, APTran.lCRefNbr, APTran.lCLineNbr>(e.Row, e.OldRow))
    {
      this.LandedCostDetailClearLink(e.OldRow as APTran);
      this.LandedCostDetailSetLink(e.Row as APTran);
    }
    bool flag = false;
    if (!sender.ObjectsEqual<APTran.isDirectTaxLine>(e.Row, e.OldRow) && row.LineType != "DS" && row.LineType != "LA")
    {
      this._discountEngine.RecalculateGroupAndDocumentDiscounts(sender, (PXSelectBase<APTran>) this.Transactions, (APTran) null, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.BranchID, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
      flag = true;
    }
    if (((!sender.ObjectsEqual<APTran.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.baseQty>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.curyUnitCost>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.curyTranAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.curyLineAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.curyDiscAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.discPct>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.manualDisc>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.discountID>(e.Row, e.OldRow) ? 1 : (this.changedSuppliedByVendorLocation ? 1 : 0)) | (flag ? 1 : 0)) != 0 && row.LineType != "DS" && row.LineType != "LA")
      this.RecalculateDiscounts(sender, row);
    if ((e.ExternalCall || sender.Graph.IsImport) && sender.ObjectsEqual<APTran.inventoryID>(e.Row, e.OldRow) && sender.ObjectsEqual<APTran.uOM>(e.Row, e.OldRow) && sender.ObjectsEqual<APTran.qty>(e.Row, e.OldRow) && sender.ObjectsEqual<APTran.branchID>(e.Row, e.OldRow) && sender.ObjectsEqual<APTran.siteID>(e.Row, e.OldRow) && sender.ObjectsEqual<APTran.manualPrice>(e.Row, e.OldRow) && (!sender.ObjectsEqual<APTran.curyUnitCost>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.curyLineAmt>(e.Row, e.OldRow)))
      row.ManualPrice = new bool?(true);
    bool? nullable = this.Document.Current.Released;
    if (!nullable.GetValueOrDefault())
    {
      nullable = this.Document.Current.Prebooked;
      if (!nullable.GetValueOrDefault())
        TaxBaseAttribute.Calculate<APTran.taxCategoryID>(sender, e);
    }
    if (sender.ObjectsEqual<APTran.box1099>(e.Row, e.OldRow))
      return;
    foreach (PXResult<APInvoiceEntry.APAdjust, APPayment> pxResult in new FbqlSelect<SelectFromBase<APInvoiceEntry.APAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APPayment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.docType, Equal<APInvoiceEntry.APAdjust.adjgDocType>>>>>.And<BqlOperand<APPayment.refNbr, IBqlString>.IsEqual<APInvoiceEntry.APAdjust.adjgRefNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoiceEntry.APAdjust.adjdDocType, Equal<P.AsString>>>>, PX.Data.And<BqlOperand<APInvoiceEntry.APAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APInvoiceEntry.APAdjust.released, IBqlBool>.IsEqual<True>>>, APInvoiceEntry.APAdjust>.View((PXGraph) this).Select((object) this.Document.Current.DocType, (object) this.Document.Current.RefNbr))
    {
      APInvoiceEntry.APAdjust adj = (APInvoiceEntry.APAdjust) pxResult;
      APPayment payment = (APPayment) pxResult;
      APReleaseProcess.Update1099Hist((PXGraph) this, -1M, (PX.Objects.AP.APAdjust) adj, (APTran) e.OldRow, (APRegister) this.Document.Current, (APRegister) payment);
      APReleaseProcess.Update1099Hist((PXGraph) this, 1M, (PX.Objects.AP.APAdjust) adj, (APTran) e.Row, (APRegister) this.Document.Current, (APRegister) payment);
    }
  }

  protected virtual void APTran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (!string.IsNullOrEmpty(row.LCRefNbr))
      this.LandedCostDetailClearLink(row);
    if (this.Document.Current == null || this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Deleted || this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.InsertedDeleted)
      return;
    if (row.LineType != "DS" && row.LineType != "LA")
      this._discountEngine.RecalculateGroupAndDocumentDiscounts(sender, (PXSelectBase<APTran>) this.Transactions, (APTran) null, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.BranchID, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
    this.RecalculateTotalDiscount();
  }

  protected virtual void APTran_Box1099_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.vendor.Current != null)
    {
      bool? vendor1099 = this.vendor.Current.Vendor1099;
      bool flag = false;
      if (!(vendor1099.GetValueOrDefault() == flag & vendor1099.HasValue))
        return;
    }
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void AP1099Hist_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (((AP1099History) e.Row).BoxNbr.HasValue)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<APTran, APTran.box1099> e)
  {
    APTran row = e.Row;
    if (row == null || !(row.LineType == "DS"))
      return;
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    if (this.vendor.Current != null && !string.IsNullOrEmpty(this.vendor.Current.CuryID))
    {
      e.NewValue = (object) this.vendor.Current.CuryID;
      e.Cancel = true;
    }
    else
    {
      if (this.branch.Current == null || string.IsNullOrEmpty(this.branch.Current.BaseCuryID))
        return;
      e.NewValue = (object) this.branch.Current.BaseCuryID;
      e.Cancel = true;
    }
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>() || this.vendor.Current == null || string.IsNullOrEmpty(this.vendor.Current.CuryRateTypeID))
      return;
    e.NewValue = (object) this.vendor.Current.CuryRateTypeID;
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

  protected virtual void APTaxTran_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) this.Document.Current.TaxZoneID;
    e.Cancel = true;
  }

  protected virtual void APTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APTaxTran row))
      return;
    PXUIFieldAttribute.SetEnabled<APTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == PXEntryStatus.Inserted);
    if ((PXSelectorAttribute.Select<PX.Objects.TX.Tax.taxID>(sender, (object) row) is PX.Objects.TX.Tax tax ? (tax.DirectTax.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXUIFieldAttribute.SetEnabled<APTaxTran.taxID>(sender, e.Row, false);
  }

  protected virtual void APTax_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is APTax row))
      return;
    APInvoice current = this.Document.Current;
    if ((current != null ? (current.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0 || !(((PX.Objects.TX.Tax) PXSelectorAttribute.Select<APTax.taxID>(sender, (object) row))?.TaxType == "W"))
      return;
    e.Cancel = true;
  }

  protected virtual void APTaxTran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is APTaxTran row))
      return;
    PXParentAttribute.SetParent(sender, e.Row, typeof (APRegister), (object) this.Document.Current);
    APInvoice current = this.Document.Current;
    if ((current != null ? (current.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0 || !(((PX.Objects.TX.Tax) PXSelectorAttribute.Select<APTaxTran.taxID>(sender, (object) row))?.TaxType == "W"))
      return;
    e.Cancel = true;
  }

  protected virtual void APTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.Document.Current == null || e.Operation != PXDBOperation.Insert && e.Operation != PXDBOperation.Update)
      return;
    ((TaxTran) e.Row).TaxZoneID = this.Document.Current.TaxZoneID;
  }

  protected virtual void APTaxTran_TaxZoneID_ExceptionHandling(
    PXCache sender,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Exception is PXSetPropertyException))
      return;
    this.Document.Cache.RaiseExceptionHandling<APInvoice.taxZoneID>((object) this.Document.Current, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
    e.Cancel = false;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<APTaxTran, APTaxTran.taxID> e)
  {
    APTaxTran row = e.Row;
    if (row == null || e.OldValue == null || e.OldValue == e.NewValue)
      return;
    object newValue1;
    this.Taxes.Cache.RaiseFieldDefaulting<APTaxTran.accountID>((object) row, out newValue1);
    row.AccountID = (int?) newValue1;
    object newValue2;
    this.Taxes.Cache.RaiseFieldDefaulting<APTaxTran.taxType>((object) row, out newValue2);
    row.TaxType = (string) newValue2;
    object newValue3;
    this.Taxes.Cache.RaiseFieldDefaulting<APTaxTran.taxBucketID>((object) row, out newValue3);
    row.TaxBucketID = (int?) newValue3;
    object newValue4;
    this.Taxes.Cache.RaiseFieldDefaulting<APTaxTran.vendorID>((object) row, out newValue4);
    row.VendorID = (int?) newValue4;
    object newValue5;
    this.Taxes.Cache.RaiseFieldDefaulting<APTaxTran.subID>((object) row, out newValue5);
    row.SubID = (int?) newValue5;
  }

  protected virtual void APAdjust_CuryAdjdAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    APInvoiceEntry.APAdjust row = (APInvoiceEntry.APAdjust) e.Row;
    if (row.AdjgDocType == "ADR" && row.AdjdDocType == "PPI")
    {
      e.Cancel = true;
    }
    else
    {
      PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Current<APInvoice.termsID>>>>.Config>.Select((PXGraph) this);
      if (terms != null && terms.InstallmentType != "S" && (Decimal) e.NewValue > 0M)
        throw new PXSetPropertyException("No applications can be created for documents with multiple installment credit terms specified.");
      if ((Decimal) e.NewValue != 0M)
      {
        APInvoiceEntry.APAdjust incomingApplications = (APInvoiceEntry.APAdjust) this.TryGetUnreleasedIncomingApplications(row.AdjgDocType, row.AdjgRefNbr);
        if (incomingApplications != null)
          throw new PXSetPropertyException((IBqlTable) row, "An unreleased voided payment ({0}) is applied to the prepayment. To create new applications for the prepayment, remove the voided payment.", PXErrorLevel.RowError, new object[1]
          {
            (object) incomingApplications.AdjgRefNbr
          });
      }
      Decimal? nullable1 = row.CuryDocBal;
      if (!nullable1.HasValue)
      {
        PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo> paymentCurrencyInfo = (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo>) (PXResult<APPayment>) PXSelectBase<APPayment, PXSelectJoin<APPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APPayment.curyInfoID>>>, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, (object) row.AdjgDocType, (object) row.AdjgRefNbr);
        APPayment apPayment = (APPayment) paymentCurrencyInfo;
        Decimal applicationDocumentBalance = BalanceCalculation.CalculateApplicationDocumentBalance((PX.Objects.CM.Extensions.CurrencyInfo) paymentCurrencyInfo, (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this), apPayment.DocBal, apPayment.CuryDocBal);
        APInvoiceEntry.APAdjust apAdjust = row;
        Decimal num = applicationDocumentBalance;
        nullable1 = row.CuryAdjdAmt;
        Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num - nullable1.GetValueOrDefault()) : new Decimal?();
        apAdjust.CuryDocBal = nullable2;
      }
      nullable1 = row.CuryDocBal;
      Decimal num1 = nullable1.Value;
      nullable1 = row.CuryAdjdAmt;
      Decimal num2 = nullable1.Value;
      if (num1 + num2 - (Decimal) e.NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable1 = row.CuryDocBal;
        Decimal num3 = nullable1.Value;
        nullable1 = row.CuryAdjdAmt;
        Decimal num4 = nullable1.Value;
        objArray[0] = (object) (num3 + num4).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
      }
    }
  }

  protected virtual PXResultset<APInvoiceEntry.APAdjust> TryGetUnreleasedIncomingApplications(
    string docType,
    string refNbr)
  {
    return PXSelectBase<APInvoiceEntry.APAdjust, PXViewOf<APInvoiceEntry.APAdjust>.BasedOn<SelectFromBase<APInvoiceEntry.APAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoiceEntry.APAdjust.adjdDocType, Equal<P.AsString.ASCII>>>>, PX.Data.And<BqlOperand<APInvoiceEntry.APAdjust.adjdRefNbr, IBqlString>.IsEqual<P.AsString>>>, PX.Data.And<BqlOperand<APInvoiceEntry.APAdjust.adjgDocType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.And<BqlOperand<APInvoiceEntry.APAdjust.released, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this, (object) docType, (object) refNbr);
  }

  protected virtual void APAdjust_CuryAdjdAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APInvoiceEntry.APAdjust))
      return;
    this.RecalculateApplicationsAmounts(this.GetExtension<APInvoiceEntry.MultiCurrency>());
  }

  protected virtual void APAdjust_Hold_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) true;
    e.Cancel = true;
  }

  protected virtual void APInvoice_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APInvoice row = e.Row as APInvoice;
    if (!this.IsApprovalRequired(row))
      return;
    sender.SetValue<APInvoice.hold>((object) row, (object) true);
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Switch<Case<Where<PX.Objects.AP.APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>, APInvoiceEntry.APAdjust.adjdDocType, Case<Where<PX.Objects.AP.APAdjust.adjType, Equal<ARAdjust.adjType.adjusting>>, APInvoiceEntry.APAdjust.adjgDocType>>>))]
  protected virtual void APAdjust_DisplayDocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Switch<Case<Where<PX.Objects.AP.APAdjust.adjType, Equal<ARAdjust.adjType.adjusted>>, APInvoiceEntry.APAdjust.adjdRefNbr, Case<Where<PX.Objects.AP.APAdjust.adjType, Equal<ARAdjust.adjType.adjusting>>, APInvoiceEntry.APAdjust.adjgRefNbr>>>))]
  protected virtual void APAdjust_DisplayRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Selector<PX.Objects.AP.APAdjust.displayRefNbr, PX.Objects.AP.Standalone.APRegister.docDate>))]
  protected virtual void APAdjust_DisplayDocDate_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Selector<PX.Objects.AP.APAdjust.displayRefNbr, PX.Objects.AP.Standalone.APRegister.docDesc>))]
  protected virtual void APAdjust_DisplayDocDesc_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Selector<PX.Objects.AP.APAdjust.displayRefNbr, PX.Objects.AP.Standalone.APRegister.curyID>))]
  protected virtual void APAdjust_DisplayCuryID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (FormatPeriodID<FormatDirection.display, Selector<PX.Objects.AP.APAdjust.displayRefNbr, PX.Objects.AP.Standalone.APRegister.finPeriodID>>))]
  protected virtual void APAdjust_DisplayFinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXFormula(typeof (Selector<PX.Objects.AP.APAdjust.displayRefNbr, PX.Objects.AP.Standalone.APRegister.status>))]
  protected virtual void APAdjust_DisplayStatus_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APInvoiceEntry.APAdjust, APInvoiceEntry.APAdjust.adjgDocDate> e)
  {
    System.DateTime? displayDocDate = e.Row.DisplayDocDate;
    System.DateTime? docDate = this.Document.Current.DocDate;
    if ((displayDocDate.HasValue & docDate.HasValue ? (displayDocDate.GetValueOrDefault() < docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      e.NewValue = (object) this.GetAdjgDate();
    else
      e.NewValue = (object) this.Document.Current.DocDate;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APInvoiceEntry.APAdjust> e)
  {
    if (this.Document.Current == null || EnumerableExtensions.IsIn<PXEntryStatus>(e.Cache.GetStatus((object) e.Row), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
      return;
    System.DateTime? displayDocDate = e.Row.DisplayDocDate;
    System.DateTime? docDate = this.Document.Current.DocDate;
    if ((displayDocDate.HasValue & docDate.HasValue ? (displayDocDate.GetValueOrDefault() < docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      e.Row.AdjgDocDate = this.GetAdjgDate();
    FinPeriod byId1 = this.FinPeriodRepository.GetByID(this.Document.Current.FinPeriodID, PXAccess.GetParentOrganizationID(this.Document.Current.BranchID));
    FinPeriod byId2 = this.FinPeriodRepository.GetByID(e.Row.DisplayFinPeriodID, PXAccess.GetParentOrganizationID(e.Row.AdjgBranchID));
    if (byId1.MasterFinPeriodID != byId2.MasterFinPeriodID)
    {
      string masterFinPeriodID = ((IEnumerable<string>) new string[2]
      {
        byId1.MasterFinPeriodID,
        byId2.MasterFinPeriodID
      }).OrderByDescending<string, string>((Func<string, string>) (_ => _)).First<string>();
      FinPeriod result = this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(e.Row.AdjgBranchID), masterFinPeriodID).Result;
      e.Row.AdjgFinPeriodID = result.FinPeriodID;
      e.Row.AdjgTranPeriodID = result.MasterFinPeriodID;
    }
    if (!(e.Row.CuryAdjdAmt.Value != 0M))
      return;
    APInvoiceEntry.APAdjust incomingApplications = (APInvoiceEntry.APAdjust) this.TryGetUnreleasedIncomingApplications(e.Row.AdjgDocType, e.Row.AdjgRefNbr);
    if (incomingApplications == null)
      return;
    e.Cache.RaiseExceptionHandling<APInvoiceEntry.APAdjust.curyAdjdAmt>((object) e.Row, (object) e.Row.CuryAdjdAmt, (Exception) new PXSetPropertyException((IBqlTable) e.Row, "An unreleased voided payment ({0}) is applied to the prepayment. To create new applications for the prepayment, remove the voided payment.", PXErrorLevel.RowError, new object[1]
    {
      (object) incomingApplications.AdjgRefNbr
    }));
  }

  protected virtual System.DateTime? GetAdjgDate()
  {
    ParameterExpression parameterExpression1;
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    System.DateTime? adjgDate = this.Adjustments_Raw.Select().Where<PXResult<APInvoiceEntry.APAdjust>>(Expression.Lambda<Func<PXResult<APInvoiceEntry.APAdjust>, bool>>((Expression) Expression.NotEqual((Expression) Expression.Coalesce((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.AP.APAdjust.get_AdjAmt))), (Expression) Expression.Constant((object) 0M, typeof (Decimal))), (Expression) Expression.Constant((object) 0M, typeof (Decimal))), parameterExpression1)).Select<PXResult<APInvoiceEntry.APAdjust>, System.DateTime?>(Expression.Lambda<Func<PXResult<APInvoiceEntry.APAdjust>, System.DateTime?>>((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (APPayment.get_AdjDate))), parameterExpression2)).Where<System.DateTime?>((Expression<Func<System.DateTime?, bool>>) (_ => _.HasValue)).OrderBy<System.DateTime?, System.DateTime>((Expression<Func<System.DateTime?, System.DateTime>>) (_ => _.Value)).FirstOrDefault<System.DateTime?>();
    if (adjgDate.HasValue)
    {
      System.DateTime dateTime = adjgDate.Value;
      System.DateTime? docDate = this.Document.Current.DocDate;
      if ((docDate.HasValue ? (dateTime > docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return adjgDate;
    }
    return this.Document.Current.DocDate;
  }

  protected virtual void APAdjust2_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) true;
    e.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APInvoiceEntry.APAdjust2, APInvoiceEntry.APAdjust2.adjgDocDate> e)
  {
    System.DateTime? displayDocDate = e.Row.DisplayDocDate;
    System.DateTime? docDate = this.Document.Current.DocDate;
    if ((displayDocDate.HasValue & docDate.HasValue ? (displayDocDate.GetValueOrDefault() < docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      e.NewValue = (object) this.GetAdjgDate();
    else
      e.NewValue = (object) this.Document.Current.DocDate;
  }

  protected virtual void APInvoiceDiscountDetail_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (this.Document == null)
      return;
    APInvoice current = this.Document.Current;
  }

  protected virtual void APInvoiceDiscountDetail_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentsByLines>())
      return;
    APInvoice current = this.Document.Current;
    if ((current != null ? (current.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    e.Cancel = true;
  }

  protected virtual void APInvoiceDiscountDetail_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    APInvoiceDiscountDetail row = (APInvoiceDiscountDetail) e.Row;
    if (this._discountEngine.IsInternalDiscountEngineCall || row == null)
      return;
    if (row.DiscountID != null)
    {
      this._discountEngine.InsertManualDocGroupDiscount(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, row, row.DiscountID, (string) null, this.Document.Current.BranchID, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
      this.RecalculateTotalDiscount();
    }
    if (!this._discountEngine.SetExternalManualDocDiscount(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, row, (APInvoiceDiscountDetail) null, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current)))
      return;
    this.RecalculateTotalDiscount();
  }

  protected virtual void APInvoiceDiscountDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    APInvoiceDiscountDetail row = (APInvoiceDiscountDetail) e.Row;
    APInvoiceDiscountDetail oldRow = (APInvoiceDiscountDetail) e.OldRow;
    if (this._discountEngine.IsInternalDiscountEngineCall || row == null)
      return;
    if (!sender.ObjectsEqual<APInvoiceDiscountDetail.skipDiscount>(e.Row, e.OldRow))
    {
      this._discountEngine.UpdateDocumentDiscount(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.BranchID, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, row.Type != "D", this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
      this.RecalculateTotalDiscount();
    }
    if (!sender.ObjectsEqual<APInvoiceDiscountDetail.discountID>(e.Row, e.OldRow) || !sender.ObjectsEqual<APInvoiceDiscountDetail.discountSequenceID>(e.Row, e.OldRow))
    {
      this._discountEngine.UpdateManualDocGroupDiscount(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, row, row.DiscountID, sender.ObjectsEqual<APInvoiceDiscountDetail.discountID>(e.Row, e.OldRow) ? row.DiscountSequenceID : (string) null, this.Document.Current.BranchID, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
      this.RecalculateTotalDiscount();
    }
    if (!this._discountEngine.SetExternalManualDocDiscount(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, row, oldRow, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current)))
      return;
    this.RecalculateTotalDiscount();
  }

  protected virtual void APInvoiceDiscountDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    APInvoiceDiscountDetail row = (APInvoiceDiscountDetail) e.Row;
    if (!this._discountEngine.IsInternalDiscountEngineCall && row != null)
      this._discountEngine.UpdateDocumentDiscount(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.BranchID, this.Document.Current.VendorLocationID, this.Document.Current.DocDate, row.Type != null && row.Type != "D" && row.Type != "B", this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
    this.RecalculateTotalDiscount();
  }

  protected virtual void APInvoiceDiscountDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    APInvoiceDiscountDetail row = (APInvoiceDiscountDetail) e.Row;
    bool flag = row.Type == "B";
    PXDefaultAttribute.SetPersistingCheck<APInvoiceDiscountDetail.discountID>(sender, (object) row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
    PXDefaultAttribute.SetPersistingCheck<APInvoiceDiscountDetail.discountSequenceID>(sender, (object) row, flag ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
  }

  protected virtual void APInvoiceDiscountDetail_DiscountSequenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall)
      return;
    e.Cancel = true;
  }

  protected virtual void APInvoiceDiscountDetail_DiscountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall)
      return;
    e.Cancel = true;
  }

  public virtual List<POReceiptLineS> GetInvoicePOReceiptLines(PX.Objects.PO.POReceipt receipt, APInvoice doc)
  {
    return PXSelectBase<POReceiptLineS, PXSelectReadonly2<POReceiptLineS, LeftJoin<APTran, On<APTran.released, Equal<False>, And<APTran.pOAccrualType, Equal<POReceiptLineS.pOAccrualType>, And<APTran.pOAccrualRefNoteID, Equal<POReceiptLineS.pOAccrualRefNoteID>, And<APTran.pOAccrualLineNbr, Equal<POReceiptLineS.pOAccrualLineNbr>>>>>>, Where<POReceiptLineS.receiptType, Equal<Required<POReceiptLineS.receiptType>>, And<POReceiptLineS.receiptNbr, Equal<Required<POReceiptLineS.receiptNbr>>, And2<Where<APTran.refNbr, PX.Data.IsNull, Or<APTran.refNbr, Equal<Required<APTran.refNbr>>, And<APTran.tranType, Equal<Required<APTran.tranType>>>>>, And<POReceiptLineS.unbilledQty, Greater<decimal0>>>>>, PX.Data.OrderBy<Asc<POReceiptLineS.sortOrder>>>.Config>.Select((PXGraph) this, (object) receipt.ReceiptType, (object) receipt.ReceiptNbr, (object) doc.RefNbr, (object) doc.DocType).RowCast<POReceiptLineS>().ToList<POReceiptLineS>();
  }

  public virtual void InvoicePOReceipt(
    PX.Objects.PO.POReceipt receipt,
    DocumentList<APInvoice> list,
    bool saveAndAdd = false,
    bool usePOParameters = true,
    bool keepOrderTaxes = false,
    bool errorIfUnreleasedAPExists = true)
  {
    if (errorIfUnreleasedAPExists)
    {
      List<APTran> list1 = PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectJoinGroupBy<PX.Objects.PO.POReceiptLine, InnerJoin<APTran, On<APTran.pOAccrualType, Equal<PX.Objects.PO.POReceiptLine.pOAccrualType>, And<APTran.pOAccrualRefNoteID, Equal<PX.Objects.PO.POReceiptLine.pOAccrualRefNoteID>, And<APTran.pOAccrualLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOAccrualLineNbr>>>>>, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<APTran.released, Equal<False>>>>, PX.Data.Aggregate<GroupBy<APTran.tranType, GroupBy<APTran.refNbr, GroupBy<APTran.pOAccrualType>>>>>.Config>.Select((PXGraph) this, (object) receipt.ReceiptType, (object) receipt.ReceiptNbr).RowCast<APTran>().ToList<APTran>();
      if (list1.Any<APTran>())
      {
        PXException pxException;
        if (!list1.Any<APTran>((Func<APTran, bool>) (t => t.POAccrualType == "R")))
          pxException = new PXException("There is at least one unreleased AP document {0} prepared for this purchase receipt. To create new AP document, remove or release the unreleased AP document.", new object[1]
          {
            (object) list1.First<APTran>().RefNbr
          });
        else
          pxException = new PXException("There is one or more unreleased AP documents created for this document. They must be released before another AP Document may be created");
        throw pxException;
      }
    }
    bool flag1 = false;
    string str1 = (string) null;
    string str2 = (string) null;
    string str3 = (string) null;
    string str4 = (string) null;
    int? nullable1 = new int?();
    string a = (string) null;
    string str5 = (string) null;
    string str6 = (string) null;
    List<POOrderReceiptLink> list2 = PXSelectBase<POOrderReceiptLink, PXSelectGroupBy<POOrderReceiptLink, Where<POOrderReceiptLink.receiptType, Equal<Required<POOrderReceiptLink.receiptType>>, And<POOrderReceiptLink.receiptNbr, Equal<Required<POOrderReceiptLink.receiptNbr>>>>, PX.Data.Aggregate<GroupBy<POOrderReceiptLink.taxZoneID, GroupBy<POOrderReceiptLink.taxCalcMode, GroupBy<POOrderReceiptLink.payToVendorID, GroupBy<POOrderReceiptLink.curyID, GroupBy<POOrderReceiptLink.termsID>>>>>>>.Config>.Select((PXGraph) this, (object) receipt.ReceiptType, (object) receipt.ReceiptNbr).RowCast<POOrderReceiptLink>().ToList<POOrderReceiptLink>();
    int? nullable2;
    if (list2.Any<POOrderReceiptLink>())
    {
      POOrderReceiptLink firstOrderReceipt = list2.First<POOrderReceiptLink>();
      if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
      {
        if (list2.Any<POOrderReceiptLink>((Func<POOrderReceiptLink, bool>) (x =>
        {
          int? payToVendorId1 = x.PayToVendorID;
          int? payToVendorId2 = firstOrderReceipt.PayToVendorID;
          return !(payToVendorId1.GetValueOrDefault() == payToVendorId2.GetValueOrDefault() & payToVendorId1.HasValue == payToVendorId2.HasValue);
        })))
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Purchase Receipt {0} contains Purchase Orders with different Pay-To Vendors.", (object) receipt.ReceiptNbr));
        nullable2 = firstOrderReceipt.PayToVendorID;
      }
      else
        nullable2 = receipt.VendorID;
      if (usePOParameters)
      {
        if (list2.Any<POOrderReceiptLink>((Func<POOrderReceiptLink, bool>) (x => x.TaxZoneID != firstOrderReceipt.TaxZoneID)) && list != null)
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Purchase Receipt {0} contains Purchase Orders with different Tax Zones.", (object) receipt.ReceiptNbr));
        str1 = firstOrderReceipt.TaxZoneID;
        str2 = firstOrderReceipt.ExternalTaxExemptionNumber;
        str3 = firstOrderReceipt.EntityUsageType;
        if (list == null || !list2.Any<POOrderReceiptLink>((Func<POOrderReceiptLink, bool>) (x => x.TaxCalcMode != firstOrderReceipt.TaxCalcMode)))
          str4 = firstOrderReceipt.TaxCalcMode;
        if (list2.Any<POOrderReceiptLink>((Func<POOrderReceiptLink, bool>) (x => x.CuryID != firstOrderReceipt.CuryID)))
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Purchase Receipt {0} contains Purchase Orders with different Currencies.", (object) receipt.ReceiptNbr));
        a = firstOrderReceipt.CuryID;
        if (list2.Count == 1 && receipt.ReceiptType != "RN")
          str5 = firstOrderReceipt.TermsID;
        if (list2.Count == 1)
          str6 = PX.Objects.PO.POOrder.PK.Find((PXGraph) this, firstOrderReceipt.POType, firstOrderReceipt.PONbr)?.OrderDesc;
      }
    }
    else
    {
      flag1 = true;
      nullable2 = receipt.VendorID;
    }
    bool flag2 = false;
    APInvoice apInvoice1;
    if (list != null)
    {
      APInvoice apInvoice2 = list.Find<APInvoice.docType, APInvoice.branchID, APInvoice.vendorID, APInvoice.vendorLocationID, APInvoice.curyID, APInvoice.invoiceDate>((object) receipt.GetAPDocType(), (object) receipt.BranchID, (object) receipt.VendorID, (object) receipt.VendorLocationID, (object) receipt.CuryID, (object) receipt.ReceiptDate) ?? new APInvoice();
      if (apInvoice2.RefNbr != null)
      {
        this.Document.Current = (APInvoice) this.Document.Search<APInvoice.refNbr>((object) apInvoice2.RefNbr, (object) apInvoice2.DocType);
      }
      else
      {
        apInvoice2.DocType = receipt.GetAPDocType();
        apInvoice2.DocDate = receipt.InvoiceDate;
        apInvoice2.BranchID = receipt.BranchID;
        if (System.DateTime.Compare(receipt.ReceiptDate.Value, receipt.InvoiceDate.Value) == 0)
          apInvoice2.FinPeriodID = receipt.FinPeriodID;
        APInvoice copy = PXCache<APInvoice>.CreateCopy(this.Document.Insert(apInvoice2));
        if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
        {
          copy.VendorID = nullable2;
          APInvoice apInvoice3 = copy;
          int? vendorId = receipt.VendorID;
          int? nullable3 = nullable2;
          int? nullable4 = vendorId.GetValueOrDefault() == nullable3.GetValueOrDefault() & vendorId.HasValue == nullable3.HasValue ? receipt.VendorLocationID : new int?();
          apInvoice3.VendorLocationID = nullable4;
          copy.SuppliedByVendorID = receipt.VendorID;
          copy.SuppliedByVendorLocationID = receipt.VendorLocationID;
        }
        else
        {
          copy.VendorID = copy.SuppliedByVendorID = receipt.VendorID;
          copy.VendorLocationID = copy.SuppliedByVendorLocationID = receipt.VendorLocationID;
        }
        this.Document.Cache.SetValueExt<APInvoice.payLocationID>((object) copy, (object) copy.VendorLocationID);
        PXFieldState stateExt = (PXFieldState) this.Document.Cache.GetStateExt<APInvoice.payLocationID>((object) copy);
        if (stateExt != null && !string.IsNullOrEmpty(stateExt.Error))
          throw new PXException(stateExt.Error);
        this.Adjustments.Cache.Clear();
        PXFieldDefaulting handler = (PXFieldDefaulting) ((cache, e) =>
        {
          e.NewValue = cache.GetValue<APInvoice.branchID>(e.Row);
          e.Cancel = true;
        });
        this.FieldDefaulting.AddHandler<APInvoice.branchID>(handler);
        try
        {
          copy = PXCache<APInvoice>.CreateCopy(this.Document.Update(copy));
        }
        finally
        {
          this.FieldDefaulting.RemoveHandler<APInvoice.branchID>(handler);
        }
        if (receipt.AutoCreateInvoice.GetValueOrDefault())
          copy.RefNoteID = receipt.NoteID;
        copy.ProjectID = receipt.ProjectID;
        copy.InvoiceNbr = receipt.InvoiceNbr;
        if (str5 != null)
          copy.TermsID = str5;
        if (str1 != null)
          copy.TaxZoneID = str1;
        copy.ExternalTaxExemptionNumber = str2;
        if (str3 != null)
          copy.EntityUsageType = str3;
        string curyId = copy.CuryID;
        string str7 = this.currencyinfo.Current?.BaseCuryID ?? ((Company) PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this)).BaseCuryID;
        this.Document.Cache.SetValueExt<APInvoice.curyID>((object) copy, (object) str7);
        APInvoice apInvoice4 = this.Document.Update(copy);
        if (str4 != null)
          apInvoice4.TaxCalcMode = str4;
        bool flag3 = usePOParameters && this.posetup.Current != null && (a == null || string.Equals(a, receipt.CuryID));
        apInvoice4.CuryID = flag3 ? receipt.CuryID : a ?? curyId;
        apInvoice1 = this.Document.Update(apInvoice4);
        if (flag3)
        {
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfo.Select();
          PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<APInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this, (object) receipt.CuryInfoID);
          currencyInfo1.CuryID = currencyInfo2.CuryID;
          currencyInfo1.CuryEffDate = currencyInfo2.CuryEffDate;
          currencyInfo1.CuryRateTypeID = currencyInfo2.CuryRateTypeID;
          currencyInfo1.CuryRate = currencyInfo2.CuryRate;
          currencyInfo1.RecipRate = currencyInfo2.RecipRate;
          currencyInfo1.CuryMultDiv = currencyInfo2.CuryMultDiv;
          this.currencyinfo.Update(currencyInfo1);
        }
        flag2 = true;
      }
    }
    else
    {
      APInvoice copy = PXCache<APInvoice>.CreateCopy(this.Document.Current);
      int? nullable5;
      if (!copy.VendorID.HasValue)
      {
        nullable5 = copy.VendorLocationID;
        if (!nullable5.HasValue)
        {
          if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
          {
            copy.VendorID = nullable2;
            APInvoice apInvoice5 = copy;
            nullable5 = receipt.VendorID;
            int? nullable6 = nullable2;
            int? nullable7 = nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue ? receipt.VendorLocationID : new int?();
            apInvoice5.VendorLocationID = nullable7;
            copy.SuppliedByVendorID = receipt.VendorID;
            copy.SuppliedByVendorLocationID = receipt.VendorLocationID;
          }
          else
          {
            copy.VendorID = copy.SuppliedByVendorID = receipt.VendorID;
            copy.VendorLocationID = copy.SuppliedByVendorLocationID = receipt.VendorLocationID;
          }
          copy.DocDate = receipt.InvoiceDate;
          if (string.IsNullOrEmpty(copy.InvoiceNbr))
            copy.InvoiceNbr = receipt.InvoiceNbr;
          if (string.IsNullOrEmpty(copy.TermsID))
            copy.TermsID = str5;
          if (str1 != null)
            copy.TaxZoneID = str1;
          if (a != null)
            copy.CuryID = a;
          APInvoice apInvoice6 = this.Document.Update(copy);
          if (str4 != null)
            apInvoice6.TaxCalcMode = str4;
          apInvoice1 = this.Document.Update(apInvoice6);
          goto label_71;
        }
      }
      int? vendorId = copy.VendorID;
      nullable5 = nullable2;
      if (!(vendorId.GetValueOrDefault() == nullable5.GetValueOrDefault() & vendorId.HasValue == nullable5.HasValue))
      {
        bool flag4 = true;
        if (flag1 && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
        {
          Vendor vendor = (Vendor) this.vendor.Select((object) nullable2);
          nullable5 = copy.VendorID;
          int? payToVendorId = (int?) vendor?.PayToVendorID;
          flag4 = !(nullable5.GetValueOrDefault() == payToVendorId.GetValueOrDefault() & nullable5.HasValue == payToVendorId.HasValue);
        }
        if (flag4)
          throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("PO receipt {0} belongs to another vendor.", (object) receipt.ReceiptNbr));
      }
    }
label_71:
    APInvoice current = this.Document.Current;
    if (string.IsNullOrEmpty(current.InvoiceNbr))
      current.InvoiceNbr = receipt.InvoiceNbr;
    if (string.IsNullOrEmpty(current.DocDesc) && !string.IsNullOrEmpty(str6))
      current.DocDesc = str6;
    POAccrualSet usedPoAccrualSet = this.GetUsedPOAccrualSet();
    List<POReceiptLineS> invoicePoReceiptLines = this.GetInvoicePOReceiptLines(receipt, current);
    HashSet<string> ordersWithDiscounts = new HashSet<string>();
    foreach (POReceiptLineS aLine in invoicePoReceiptLines)
    {
      Decimal? nullable8 = aLine.RetainagePct;
      if (nullable8.GetValueOrDefault() != 0M)
        this.EnableRetainage();
      if (keepOrderTaxes)
      {
        bool flag5 = string.IsNullOrEmpty(aLine.POType) || string.IsNullOrEmpty(aLine.PONbr);
        TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID, TaxAttribute>(this.Transactions.Cache, (object) null, keepOrderTaxes && !flag5 ? TaxCalc.ManualCalc : TaxCalc.ManualLineCalc);
      }
      PXRowInserting handler = (PXRowInserting) ((sender, e) => PXParentAttribute.SetParent(sender, e.Row, typeof (APRegister), (object) this.Document.Current));
      this.RowInserting.AddHandler<APTran>(handler);
      this.AddPOReceiptLine((IAPTranSource) aLine, (HashSet<APTran>) usedPoAccrualSet);
      if (this.Document.Current != null)
      {
        nullable8 = aLine.DocumentDiscountRate;
        if (nullable8.HasValue)
        {
          nullable8 = aLine.GroupDiscountRate;
          Decimal num1 = (Decimal) 1;
          if (nullable8.GetValueOrDefault() == num1 & nullable8.HasValue)
          {
            nullable8 = aLine.DocumentDiscountRate;
            Decimal num2 = (Decimal) 1;
            if (nullable8.GetValueOrDefault() == num2 & nullable8.HasValue)
              goto label_86;
          }
          this.Document.Current.SetWarningOnDiscount = new bool?(true);
          ordersWithDiscounts.Add(aLine.PONbr);
        }
      }
label_86:
      this.RowInserting.RemoveHandler<APTran>(handler);
    }
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID, TaxAttribute>(this.Transactions.Cache, (object) null, TaxCalc.ManualLineCalc);
    this.AutoRecalculateDiscounts();
    if (keepOrderTaxes)
    {
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID, TaxAttribute>(this.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
      this.AddOrderTaxes(receipt);
    }
    if (flag2)
    {
      current.CuryOrigDocAmt = current.CuryDocBal;
      this.Document.Update(current);
    }
    if (list != null & saveAndAdd)
    {
      this.Save.Press();
      if (list.Find((object) this.Document.Current) == null)
        list.Add(this.Document.Current);
    }
    this.WritePODiscountWarningToTrace(this.Document.Current, ordersWithDiscounts);
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.ManualLineCalc);
  }

  public virtual void AddOrderTaxes(PX.Objects.PO.POOrder order)
  {
    if (this.Document.Current == null || this.IsExternalTax(this.Document.Current.TaxZoneID))
      return;
    this.AddOrderTaxLines(order.OrderType, order.OrderNbr);
  }

  public virtual void AddOrderTaxes(PX.Objects.PO.POReceipt receipt)
  {
    if (this.Document.Current == null || this.IsExternalTax(this.Document.Current.TaxZoneID))
      return;
    foreach (PXResult<POOrderReceiptLink> pxResult in PXSelectBase<POOrderReceiptLink, PXSelectGroupBy<POOrderReceiptLink, Where<POOrderReceiptLink.receiptType, Equal<Required<POOrderReceiptLink.receiptType>>, And<POOrderReceiptLink.receiptNbr, Equal<Required<POOrderReceiptLink.receiptNbr>>>>, PX.Data.Aggregate<GroupBy<POOrderReceiptLink.pOType, GroupBy<POOrderReceiptLink.pONbr, GroupBy<POOrderReceiptLink.taxZoneID>>>>>.Config>.Select((PXGraph) this, (object) receipt.ReceiptType, (object) receipt.ReceiptNbr))
    {
      POOrderReceiptLink orderReceiptLink = (POOrderReceiptLink) pxResult;
      this.Caches[typeof (POOrderReceiptLink)].Current = (object) orderReceiptLink;
      this.AddOrderTaxLines(orderReceiptLink.POType, orderReceiptLink.PONbr);
    }
  }

  public virtual void AddOrderTaxLines(string orderType, string orderNbr)
  {
    foreach (var data in PXSelectBase<POTaxTran, PXSelectJoin<POTaxTran, InnerJoin<PX.Objects.TX.Tax, On<POTaxTran.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<POTaxTran.orderType, Equal<Required<POTaxTran.orderType>>, And<POTaxTran.orderNbr, Equal<Required<POTaxTran.orderNbr>>>>>.Config>.Select((PXGraph) this, (object) orderType, (object) orderNbr).AsEnumerable<PXResult<POTaxTran>>().Select(r => new
    {
      POTax = PXResult.Unwrap<POTaxTran>((object) r),
      Tax = PXResult.Unwrap<PX.Objects.TX.Tax>((object) r)
    }).OrderBy(r => r.Tax, (IComparer<PX.Objects.TX.Tax>) TaxByCalculationLevelComparer.Instance))
    {
      APTaxTran newtax = new APTaxTran();
      newtax.Module = "AP";
      this.Taxes.Cache.SetDefaultExt<APTaxTran.origTranType>((object) newtax);
      this.Taxes.Cache.SetDefaultExt<APTaxTran.origRefNbr>((object) newtax);
      this.Taxes.Cache.SetDefaultExt<TaxTran.lineRefNbr>((object) newtax);
      newtax.TranType = this.Document.Current.DocType;
      newtax.RefNbr = this.Document.Current.RefNbr;
      newtax.TaxID = data.POTax.TaxID;
      newtax.TaxRate = new Decimal?(0M);
      foreach (APTaxTran apTaxTran in this.Taxes.Cache.Cached.RowCast<APTaxTran>().Where<APTaxTran>((Func<APTaxTran, bool>) (a => !EnumerableExtensions.IsIn<PXEntryStatus>(this.Taxes.Cache.GetStatus((object) a), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted) && this.Taxes.Cache.ObjectsEqual<APTaxTran.module, APTaxTran.refNbr, APTaxTran.tranType, APTaxTran.taxID>((object) newtax, (object) a))))
        this.Taxes.Delete(apTaxTran);
      newtax = this.Taxes.Insert(newtax);
    }
  }

  public virtual bool EnableRetainage()
  {
    if (this.Document.Current.RetainageApply.GetValueOrDefault())
      return false;
    this.Document.Current.RetainageApply = new bool?(true);
    this.Document.Cache.SetDefaultExt<APRegister.defRetainagePct>((object) this.Document.Current);
    this.Document.Cache.RaiseExceptionHandling<APInvoice.retainageApply>((object) this.Document.Current, (object) true, (Exception) new PXSetPropertyException("The Apply Retainage check box is selected automatically because you have added one or more lines with a retainage from the purchase order.", PXErrorLevel.Warning));
    return true;
  }

  public virtual void AttachPrepayment() => this.AttachPrepayment((List<PX.Objects.PO.POOrder>) null);

  public virtual void AttachPrepayment(List<PX.Objects.PO.POOrder> orders)
  {
    PX.Objects.CM.Extensions.CurrencyInfo invoiceCurrencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APInvoice.curyInfoID>>>>.Config>.Select((PXGraph) this);
    if (this.Document.Current == null || !(this.Document.Current.DocType == "INV") && !(this.Document.Current.DocType == "ACR"))
      return;
    bool? nullable1 = this.Document.Current.Released;
    bool flag1 = false;
    if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      return;
    nullable1 = this.Document.Current.Prebooked;
    bool flag2 = false;
    if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
      return;
    Lazy<APTran[]> lazy = new Lazy<APTran[]>((Func<APTran[]>) (() => this.Transactions.SelectMain()));
    using (new ReadOnlyScope(new PXCache[2]
    {
      this.Adjustments.Cache,
      this.Document.Cache
    }))
    {
      Decimal? curyDocBal = this.Document.Current.CuryDocBal;
      Decimal? nullable2 = this.Adjustments.Select().Sum<PXResult<APInvoiceEntry.APAdjust>>((Expression<Func<PXResult<APInvoiceEntry.APAdjust>, Decimal?>>) (a => ((APInvoiceEntry.APAdjust) a).CuryAdjdAmt));
      Decimal? nullable3 = curyDocBal.HasValue & nullable2.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      nullable2 = nullable3;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
        return;
      foreach (PXResult<APPayment, PX.Objects.CM.Extensions.CurrencyInfo, APInvoiceEntry.APAdjust> pxResult1 in PXSelectBase<APPayment, PXSelectJoin<APPayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APPayment.curyInfoID>>, LeftJoin<APInvoiceEntry.APAdjust, On<APInvoiceEntry.APAdjust.adjgDocType, Equal<APPayment.docType>, And<APInvoiceEntry.APAdjust.adjgRefNbr, Equal<APPayment.refNbr>, And<APInvoiceEntry.APAdjust.released, NotEqual<True>, PX.Data.And<Where<APInvoiceEntry.APAdjust.adjdDocType, NotEqual<Current<APInvoice.docType>>, Or<APInvoiceEntry.APAdjust.adjdRefNbr, NotEqual<Current<APInvoice.refNbr>>>>>>>>>>, Where<APPayment.vendorID, Equal<Current<APInvoice.vendorID>>, And<APPayment.docType, Equal<APDocType.prepayment>, And<APPayment.docDate, LessEqual<Current<APInvoice.docDate>>, And<APPayment.tranPeriodID, LessEqual<Current<APInvoice.tranPeriodID>>, And<APPayment.released, Equal<True>, And<APPayment.openDoc, Equal<True>, And<APPayment.curyDocBal, Greater<decimal0>, And<APInvoiceEntry.APAdjust.adjdRefNbr, PX.Data.IsNull>>>>>>>>>.Config>.Select((PXGraph) this))
      {
        APPayment apPayment = (APPayment) pxResult1;
        PX.Objects.CM.Extensions.CurrencyInfo paymentCurrencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult1;
        if ((APInvoiceEntry.APAdjust) this.TryGetUnreleasedIncomingApplications(apPayment.DocType, apPayment.RefNbr) == null)
        {
          foreach (PXResult<POOrderPrepayment, PX.Objects.CM.Extensions.CurrencyInfo> pxResult2 in PXSelectBase<POOrderPrepayment, PXSelectJoin<POOrderPrepayment, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<POOrderPrepayment.curyInfoID>>>, Where<POOrderPrepayment.aPDocType, Equal<Required<APPayment.docType>>, And<POOrderPrepayment.aPRefNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, (object) apPayment.DocType, (object) apPayment.RefNbr))
          {
            POOrderPrepayment orderPrepay = (POOrderPrepayment) pxResult2;
            PX.Objects.CM.Extensions.CurrencyInfo paymentCurrencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) pxResult2;
            if ((orders == null || orders.Any<PX.Objects.PO.POOrder>((Func<PX.Objects.PO.POOrder, bool>) (o => o.OrderType == orderPrepay.OrderType && o.OrderNbr == orderPrepay.OrderNbr))) && ((IEnumerable<APTran>) lazy.Value).Any<APTran>((Func<APTran, bool>) (t => t.POOrderType == orderPrepay.OrderType && t.PONbr == orderPrepay.OrderNbr)))
            {
              APInvoiceEntry.APAdjust apAdjust1 = new APInvoiceEntry.APAdjust();
              apAdjust1.AdjdDocType = this.Document.Current.DocType;
              apAdjust1.AdjdRefNbr = this.Document.Current.RefNbr;
              apAdjust1.AdjdLineNbr = new int?(0);
              apAdjust1.AdjgDocType = apPayment.DocType;
              apAdjust1.AdjgRefNbr = apPayment.RefNbr;
              apAdjust1.AdjNbr = apPayment.AdjCntr;
              APInvoiceEntry.APAdjust apAdjust2 = apAdjust1;
              APInvoiceEntry.APAdjust apAdjust3;
              APInvoiceEntry.APAdjust apAdjust4;
              if ((apAdjust3 = this.Adjustments.Locate(apAdjust2)) == null || EnumerableExtensions.IsIn<PXEntryStatus>(this.Adjustments.Cache.GetStatus((object) apAdjust2), PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted))
              {
                apAdjust2.VendorID = this.Document.Current.VendorID;
                apAdjust2.AdjdBranchID = this.Document.Current.BranchID;
                apAdjust2.AdjgBranchID = apPayment.BranchID;
                apAdjust2.AdjgCuryInfoID = apPayment.CuryInfoID;
                apAdjust2.AdjdOrigCuryInfoID = this.Document.Current.CuryInfoID;
                apAdjust2.AdjdCuryInfoID = this.Document.Current.CuryInfoID;
                apAdjust2.CuryDocBal = new Decimal?(BalanceCalculation.CalculateApplicationDocumentBalance(paymentCurrencyInfo1, invoiceCurrencyInfo, apPayment.DocBal, apPayment.CuryDocBal));
                apAdjust4 = this.Adjustments.Insert(apAdjust2);
              }
              else
                apAdjust4 = (APInvoiceEntry.APAdjust) this.Adjustments.Cache.CreateCopy((object) apAdjust3);
              nullable2 = apAdjust4.CuryAdjdAmt;
              if (!nullable2.HasValue)
                throw new ArgumentNullException("CuryAdjdAmt");
              Decimal? nullable4 = new Decimal?(BalanceCalculation.CalculateApplicationDocumentBalance(paymentCurrencyInfo2, invoiceCurrencyInfo, orderPrepay.AppliedAmt, orderPrepay.CuryAppliedAmt));
              Decimal? nullable5 = ((IEnumerable<Decimal?>) new Decimal?[3]
              {
                apAdjust4.CuryDocBal,
                nullable4,
                nullable3
              }).Min();
              nullable2 = nullable5;
              Decimal num2 = 0M;
              if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
              {
                if (!nullable5.HasValue)
                  throw new ArgumentNullException("appAmt");
                APInvoiceEntry.APAdjust apAdjust5 = apAdjust4;
                nullable2 = apAdjust4.CuryAdjdAmt;
                Decimal? nullable6 = nullable5;
                Decimal? nullable7 = new Decimal?(System.Math.Max((nullable2.HasValue & nullable6.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?()).Value, 0M));
                apAdjust5.CuryAdjdAmt = nullable7;
                this.Adjustments.Update(apAdjust4);
                nullable2 = nullable3;
                nullable6 = nullable5;
                nullable3 = nullable2.HasValue & nullable6.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
              }
            }
          }
        }
      }
    }
  }

  public virtual void InvoicePOOrder(PX.Objects.PO.POOrder order, bool createNew, bool keepOrderTaxes = false)
  {
    APInvoice row;
    if (createNew)
    {
      APInvoice apInvoice1 = new APInvoice();
      apInvoice1.DocType = "INV";
      apInvoice1.BranchID = order.BranchID;
      apInvoice1.OrigModule = "PO";
      row = PXCache<APInvoice>.CreateCopy(this.Document.Insert(apInvoice1));
      if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
      {
        row.VendorID = order.PayToVendorID;
        APInvoice apInvoice2 = row;
        int? vendorId = order.VendorID;
        int? nullable1 = order.PayToVendorID;
        int? nullable2;
        if (!(vendorId.GetValueOrDefault() == nullable1.GetValueOrDefault() & vendorId.HasValue == nullable1.HasValue))
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = order.VendorLocationID;
        apInvoice2.VendorLocationID = nullable2;
        row.SuppliedByVendorID = order.VendorID;
        row.SuppliedByVendorLocationID = order.VendorLocationID;
      }
      else
      {
        row.VendorID = row.SuppliedByVendorID = order.VendorID;
        row.VendorLocationID = row.SuppliedByVendorLocationID = order.VendorLocationID;
      }
      row.TermsID = order.TermsID;
      row.InvoiceNbr = order.VendorRefNbr;
      row.CuryID = order.CuryID;
      row.DocDesc = order.OrderDesc;
      row.ProjectID = order.ProjectID;
      row.TaxZoneID = order.TaxZoneID;
      row.EntityUsageType = order.EntityUsageType;
      row.ExternalTaxExemptionNumber = order.ExternalTaxExemptionNumber;
      row.TaxCalcMode = order.TaxCalcMode;
      row.RetainageApply = order.RetainageApply;
      row.DefRetainagePct = order.DefRetainagePct;
      PXFieldDefaulting handler = (PXFieldDefaulting) ((cache, e) =>
      {
        e.NewValue = cache.GetValue<APInvoice.branchID>(e.Row);
        e.Cancel = true;
      });
      this.FieldDefaulting.AddHandler<APInvoice.branchID>(handler);
      try
      {
        row = this.Document.Update(row);
      }
      finally
      {
        this.FieldDefaulting.RemoveHandler<APInvoice.branchID>(handler);
      }
    }
    else
    {
      APInvoice copy = PXCache<APInvoice>.CreateCopy(this.Document.Current);
      if (string.IsNullOrEmpty(copy.DocDesc))
        copy.DocDesc = order.OrderDesc;
      if (string.IsNullOrEmpty(copy.InvoiceNbr))
        copy.InvoiceNbr = order.VendorRefNbr;
      row = this.Document.Update(copy);
    }
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>())
    {
      int? nullable;
      if (row.VendorID.HasValue)
      {
        int? vendorId = row.VendorID;
        nullable = order.VendorID;
        if (!(vendorId.GetValueOrDefault() == nullable.GetValueOrDefault() & vendorId.HasValue == nullable.HasValue))
          goto label_20;
      }
      nullable = row.VendorLocationID;
      if (nullable.HasValue)
      {
        nullable = row.VendorLocationID;
        int? vendorLocationId = order.VendorLocationID;
        if (nullable.GetValueOrDefault() == vendorLocationId.GetValueOrDefault() & nullable.HasValue == vendorLocationId.HasValue)
          goto label_21;
      }
      else
        goto label_21;
label_20:
      throw new PXException("The bill '{0}' has different vendor or vendor location than the purchase order '{1}'.", new object[2]
      {
        (object) row.RefNbr,
        (object) order.OrderNbr
      });
    }
label_21:
    if (row.CuryID != order.CuryID)
      throw new PXException("The currency '{1}' of the bill '{0}' differs from currency '{3}' of the purchase order '{2}'.", new object[4]
      {
        (object) row.RefNbr,
        (object) row.CuryID,
        (object) order.OrderNbr,
        (object) order.CuryID
      });
    POAccrualSet usedPoAccrualSet = this.GetUsedPOAccrualSet();
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID, TaxAttribute>(this.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
    this.ProcessPOOrderLines(order, (HashSet<APTran>) usedPoAccrualSet, !createNew, keepOrderTaxes);
    if (keepOrderTaxes)
      this.AddOrderTaxes(order);
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID, TaxAttribute>(this.Transactions.Cache, (object) null, TaxCalc.ManualLineCalc);
    if (!keepOrderTaxes)
    {
      object copy = (object) PXCache<APInvoice>.CreateCopy(this.Document.Current);
      this.Document.Current.TaxZoneID = (string) null;
      this.Document.Cache.Update(copy);
    }
    this.Document.Cache.RaiseFieldUpdated<APInvoice.curyOrigDocAmt>((object) row, (object) 0M);
  }

  public virtual void ProcessPOOrderLines(
    PX.Objects.PO.POOrder order,
    HashSet<APTran> duplicates,
    bool addBilled,
    bool keepOrderTaxes = false)
  {
    this.ProcessPOOrderLines((IEnumerable<IAPTranSource>) this.GetPOOrderLines(order, this.Document.Current, addBilled), duplicates, keepOrderTaxes);
  }

  public List<POLineS> GetPOOrderLines(PX.Objects.PO.POOrder order, APInvoice doc, bool addBilled)
  {
    PXSelectBase<POLineS> pxSelectBase = (PXSelectBase<POLineS>) new PXSelectReadonly<POLineS, Where<POLineS.orderType, Equal<Required<POLineS.orderType>>, And<POLineS.orderNbr, Equal<Required<POLineS.orderNbr>>, And<POLineS.cancelled, Equal<False>, And<POLineS.pOAccrualType, Equal<POAccrualType.order>>>>>, PX.Data.OrderBy<Asc<POLineS.sortOrder>>>((PXGraph) this);
    if ((!(order.OrderType == "RS") ? 0 : (doc.DocType == "ADR" ? 1 : 0)) == 0)
      pxSelectBase.WhereAnd<Where<POLineS.closed, Equal<False>>>();
    if (!addBilled)
    {
      pxSelectBase.Join<LeftJoin<APTran, On<APTran.pOAccrualRefNoteID, Equal<POLineS.orderNoteID>, And<APTran.pOAccrualLineNbr, Equal<POLineS.lineNbr>, And<APTran.released, Equal<False>>>>>>();
      pxSelectBase.WhereAnd<Where<APTran.refNbr, PX.Data.IsNull>>();
      pxSelectBase.WhereAnd<Where<POLineS.billed, Equal<False>>>();
    }
    return pxSelectBase.Select((object) order.OrderType, (object) order.OrderNbr).RowCast<POLineS>().ToList<POLineS>();
  }

  public virtual void ProcessPOOrderLines(
    IEnumerable<IAPTranSource> orderlines,
    HashSet<APTran> duplicates = null,
    bool keepOrderTaxes = false)
  {
    duplicates = duplicates ?? (HashSet<APTran>) new POAccrualSet();
    bool flag1 = false;
    bool flag2 = false;
    HashSet<string> ordersWithDiscounts = new HashSet<string>();
    foreach (IAPTranSource orderline in orderlines)
    {
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID, TaxAttribute>(this.Transactions.Cache, (object) null, keepOrderTaxes ? TaxCalc.ManualCalc : TaxCalc.ManualLineCalc);
      PXRowInserting handler = (PXRowInserting) ((sender, e) => PXParentAttribute.SetParent(sender, e.Row, typeof (APRegister), (object) this.Document.Current));
      this.RowInserting.AddHandler<APTran>(handler);
      try
      {
        bool flag3 = orderline.OrderType == "RS" && this.Document.Current.DocType == "ADR";
        if (orderline.RetainagePct.GetValueOrDefault() != 0M && !flag3)
          this.EnableRetainage();
        APTran apTran = this.AddPOReceiptLine(orderline, duplicates);
        if (apTran != null)
        {
          if (apTran.PONbr != null)
          {
            if (this.Document.Current != null)
            {
              Decimal? nullable = orderline.DocumentDiscountRate;
              if (nullable.HasValue)
              {
                nullable = orderline.GroupDiscountRate;
                Decimal num1 = (Decimal) 1;
                if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
                {
                  nullable = orderline.DocumentDiscountRate;
                  Decimal num2 = (Decimal) 1;
                  if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
                    continue;
                }
                this.Document.Current.SetWarningOnDiscount = new bool?(true);
                ordersWithDiscounts.Add(apTran.PONbr);
              }
            }
          }
        }
      }
      catch (PXException ex)
      {
        PXTrace.WriteError((Exception) ex);
        flag1 = true;
        flag2 = orderline.OrderType == "RS";
      }
      finally
      {
        this.RowInserting.RemoveHandler<APTran>(handler);
      }
    }
    if (flag1)
      throw new PXException(flag2 ? "One subcontract line or multiple subcontract lines cannot be added to the bill. See Trace Log for details." : "One purchase order line or multiple purchase order lines cannot be added to the bill. See Trace Log for details.");
    this.AutoRecalculateDiscounts();
    this.WritePODiscountWarningToTrace(this.Document.Current, ordersWithDiscounts);
  }

  public virtual void WritePODiscountWarningToTrace(
    APInvoice document,
    HashSet<string> ordersWithDiscounts)
  {
    if (!document.SetWarningOnDiscount.GetValueOrDefault() || ordersWithDiscounts.Count <= 0)
      return;
    PXTrace.WriteWarning("One or more purchase orders added to the AP bill contain group or document discounts. Please check the purchase orders: {0}", (object) string.Join(", ", ordersWithDiscounts.ToArray<string>()));
  }

  public bool IsApprovalRequired(APInvoice doc)
  {
    if (EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>.ApprovableDocTypes.Contains(doc.DocType))
      return true;
    return doc.DocType == "PPM" && EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>.ApprovableDocTypes.Contains("PPR");
  }

  public virtual APTran AddPOReceiptLine(IAPTranSource aLine, HashSet<APTran> checkForDuplicates)
  {
    APTran newtran = new APTran();
    APTran row1 = (APTran) null;
    aLine.SetReferenceKeyTo(newtran);
    HashSet<APTran> apTranSet = checkForDuplicates;
    // ISSUE: explicit non-virtual call
    if ((apTranSet != null ? (__nonvirtual (apTranSet.Contains(newtran)) ? 1 : 0) : 0) != 0)
    {
      if (!aLine.AggregateWithExistingTran)
        return (APTran) null;
      row1 = checkForDuplicates.First<APTran>((Func<APTran, bool>) (t => checkForDuplicates.Comparer.Equals(t, newtran)));
    }
    newtran.TranType = this.Document.Current.DocType;
    newtran.RefNbr = this.Document.Current.RefNbr;
    newtran.BranchID = aLine.BranchID;
    newtran.AccrueCost = aLine.OrderType == "PD" ? new bool?(false) : aLine.AccrueCost;
    newtran.DropshipExpenseRecording = aLine.DropshipExpenseRecording;
    PX.Objects.IN.InventoryItem byId = this.InventoryItemGetByID(aLine.InventoryID);
    bool? nullable1 = newtran.AccrueCost;
    if (nullable1.GetValueOrDefault())
    {
      if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.inventory>() && this.insetup != null && this.insetup.Current != null)
      {
        nullable1 = this.insetup.Current.UpdateGL;
        if (nullable1.GetValueOrDefault() && byId != null)
        {
          nullable1 = byId.NonStockReceipt;
          if (nullable1.GetValueOrDefault())
            goto label_9;
        }
      }
      newtran.AccountID = aLine.ExpenseAcctID;
      newtran.SubID = aLine.ExpenseSubID;
      goto label_10;
    }
label_9:
    newtran.AccountID = aLine.POAccrualAcctID ?? aLine.ExpenseAcctID;
    newtran.SubID = aLine.POAccrualSubID ?? aLine.ExpenseSubID;
label_10:
    newtran.LineType = aLine.LineType;
    newtran.SiteID = aLine.SiteID;
    newtran.IsStockItem = aLine.IsStockItem;
    newtran.InventoryID = aLine.InventoryID;
    newtran.UOM = string.IsNullOrEmpty(aLine.UOM) ? (string) null : aLine.UOM;
    Decimal? nullable2;
    int num1;
    if (aLine.IsPartiallyBilled)
    {
      nullable2 = aLine.BaseOrigQty;
      Decimal num2 = 0M;
      num1 = nullable2.GetValueOrDefault() == num2 & nullable2.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag1 = num1 != 0;
    Decimal? nullable3;
    if (!flag1)
    {
      if (!string.IsNullOrEmpty(aLine.UOM) && aLine.UOM == aLine.OrigUOM)
      {
        nullable2 = aLine.BillQty;
        if (nullable2.HasValue)
        {
          Decimal? billQty = aLine.BillQty;
          nullable2 = aLine.OrigQty;
          nullable3 = billQty.HasValue & nullable2.HasValue ? new Decimal?(billQty.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
          goto label_19;
        }
      }
      nullable2 = aLine.BaseBillQty;
      Decimal? baseOrigQty = aLine.BaseOrigQty;
      nullable3 = nullable2.HasValue & baseOrigQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / baseOrigQty.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable3 = new Decimal?(1M);
label_19:
    nullable2 = aLine.BillQty;
    if (nullable2.HasValue)
    {
      newtran.Qty = aLine.BillQty;
    }
    else
    {
      newtran.BaseQty = aLine.BaseBillQty;
      PXDBQuantityAttribute.CalcTranQty<APTran.qty>(this.Transactions.Cache, (object) newtran);
    }
    nullable2 = nullable3;
    Decimal? nullable4 = aLine.LineAmt;
    Decimal? nullable5 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
    nullable4 = nullable3;
    nullable2 = aLine.CuryLineAmt;
    Decimal? nullable6 = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = nullable3;
    nullable4 = aLine.DiscAmt;
    Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
    Decimal? nullable7;
    if (!nullable2.HasValue)
    {
      nullable4 = new Decimal?();
      nullable7 = nullable4;
    }
    else
      nullable7 = new Decimal?(nullable2.GetValueOrDefault() * valueOrDefault1);
    Decimal? nullable8 = nullable7;
    nullable2 = nullable3;
    nullable4 = aLine.CuryDiscAmt;
    Decimal valueOrDefault2 = nullable4.GetValueOrDefault();
    Decimal? nullable9;
    if (!nullable2.HasValue)
    {
      nullable4 = new Decimal?();
      nullable9 = nullable4;
    }
    else
      nullable9 = new Decimal?(nullable2.GetValueOrDefault() * valueOrDefault2);
    Decimal? nullable10 = nullable9;
    Decimal? discPct = aLine.DiscPct;
    nullable2 = nullable3;
    nullable4 = aLine.RetainageAmt;
    Decimal valueOrDefault3 = nullable4.GetValueOrDefault();
    Decimal? nullable11;
    if (!nullable2.HasValue)
    {
      nullable4 = new Decimal?();
      nullable11 = nullable4;
    }
    else
      nullable11 = new Decimal?(nullable2.GetValueOrDefault() * valueOrDefault3);
    Decimal? nullable12 = nullable11;
    nullable2 = nullable3;
    nullable4 = aLine.CuryRetainageAmt;
    Decimal valueOrDefault4 = nullable4.GetValueOrDefault();
    Decimal? nullable13;
    if (!nullable2.HasValue)
    {
      nullable4 = new Decimal?();
      nullable13 = nullable4;
    }
    else
      nullable13 = new Decimal?(nullable2.GetValueOrDefault() * valueOrDefault4);
    Decimal? nullable14 = nullable13;
    Decimal? nullable15;
    ref Decimal? local = ref nullable15;
    nullable2 = aLine.RetainagePct;
    Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
    local = new Decimal?(valueOrDefault5);
    nullable2 = aLine.UnitCost;
    Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
    nullable2 = aLine.CuryUnitCost;
    Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
    newtran.ManualPrice = new bool?(true);
    newtran.ManualDisc = new bool?(newtran.PONbr != null);
    newtran.FreezeManualDisc = new bool?(true);
    newtran.DiscountID = aLine.DiscountID;
    newtran.DiscountSequenceID = aLine.DiscountSequenceID;
    DRDeferredCode deferralCode = (DRDeferredCode) PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, (object) byId?.DeferredCode);
    if (deferralCode != null)
      newtran.RequiresTerms = new bool?(this.DoesRequireTerms(deferralCode));
    newtran.DRTermStartDate = aLine.DRTermStartDate;
    newtran.DRTermEndDate = aLine.DRTermEndDate;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, (object) aLine.CuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = (PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfo.Select();
    bool areCurrenciesSame = currencyInfo2.CuryID == currencyInfo1?.CuryID;
    if (areCurrenciesSame)
    {
      newtran.CuryUnitCost = new Decimal?(valueOrDefault7);
      newtran.CuryLineAmt = flag1 ? nullable6 : new Decimal?(currencyInfo2.RoundCury(nullable6.GetValueOrDefault()));
      newtran.CuryDiscAmt = flag1 ? nullable10 : new Decimal?(currencyInfo2.RoundCury(nullable10.GetValueOrDefault()));
      newtran.CuryRetainageAmt = flag1 ? nullable14 : new Decimal?(0M);
    }
    else
    {
      newtran.CuryUnitCost = new Decimal?(currencyInfo2.CuryConvCury(valueOrDefault6, new int?((int) this.commonsetup.Current.DecPlPrcCst.Value)));
      newtran.CuryLineAmt = new Decimal?(currencyInfo2.CuryConvCury(nullable5.Value));
      newtran.CuryDiscAmt = new Decimal?(currencyInfo2.CuryConvCury(nullable8.Value));
      newtran.CuryRetainageAmt = new Decimal?(flag1 ? currencyInfo2.CuryConvCury(nullable12.Value) : 0M);
    }
    if (!string.IsNullOrEmpty(newtran.UOM) && newtran.UOM != aLine.OrigUOM)
    {
      nullable2 = newtran.CuryUnitCost;
      Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
      Decimal num3 = INUnitAttribute.ConvertFromBase<APTran.inventoryID>(this.Transactions.Cache, (object) newtran, aLine.OrigUOM, valueOrDefault8, INPrecision.UNITCOST);
      Decimal num4 = INUnitAttribute.ConvertToBase<APTran.inventoryID>(this.Transactions.Cache, (object) newtran, newtran.UOM, num3, INPrecision.UNITCOST);
      newtran.CuryUnitCost = new Decimal?(num4);
    }
    this.CopyCustomizationFieldsToAPTran(newtran, aLine, areCurrenciesSame);
    newtran.DiscPct = discPct;
    newtran.RetainagePct = flag1 ? nullable15 : new Decimal?(0M);
    newtran.TranDesc = aLine.TranDesc;
    newtran.TaxCategoryID = aLine.TaxCategoryID;
    newtran.TaxID = aLine.TaxID;
    if (row1 == null)
    {
      newtran.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<APTran.lineNbr>(this.Transactions.Cache, (object) this.Document.Current);
      newtran = this.InsertTranOnAddPOReceiptLine(aLine, newtran);
      bool flag2 = aLine.OrderType == "RS" && this.Document.Current.DocType == "ADR";
      if (!flag1)
      {
        nullable2 = nullable15;
        Decimal num5 = 0M;
        if (!(nullable2.GetValueOrDefault() == num5 & nullable2.HasValue) && !flag2)
        {
          this.Transactions.Cache.SetValueExt<APTran.retainagePct>((object) newtran, (object) nullable15);
          newtran = this.Transactions.Update(newtran);
        }
      }
      checkForDuplicates.Add(newtran);
      return newtran;
    }
    if (row1.UOM != newtran.UOM)
    {
      this.Transactions.Cache.RaiseExceptionHandling<APTran.qty>((object) row1, (object) row1.Qty, (Exception) new PXSetPropertyException("A line from the PO receipt related to the same PO line as this line has not been added to the bill because the quantity of items is in different UOM.", PXErrorLevel.Warning));
      return (APTran) null;
    }
    APTran copy = (APTran) this.Transactions.Cache.CreateCopy((object) row1);
    APTran apTran1 = copy;
    nullable2 = apTran1.Qty;
    nullable4 = newtran.Qty;
    apTran1.Qty = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    APTran apTran2 = copy;
    nullable4 = apTran2.CuryLineAmt;
    nullable2 = newtran.CuryLineAmt;
    apTran2.CuryLineAmt = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    APTran apTran3 = copy;
    nullable2 = apTran3.CuryDiscAmt;
    nullable4 = newtran.CuryDiscAmt;
    apTran3.CuryDiscAmt = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
    APTran row2 = this.UpdateTranOnAddPOReceiptLine(aLine, copy);
    this.Transactions.Cache.RaiseExceptionHandling<APTran.qty>((object) row2, (object) row2.Qty, (Exception) new PXSetPropertyException("The quantity of items in this line has been summed with the quantity of items related to the same PO line from the added PO receipt or receipts.", PXErrorLevel.Warning));
    checkForDuplicates.Remove(newtran);
    checkForDuplicates.Add(row2);
    return row2;
  }

  public virtual bool DoesRequireTerms(DRDeferredCode deferralCode)
  {
    bool? deliverableArrangement = deferralCode.MultiDeliverableArrangement;
    bool flag = false;
    return deliverableArrangement.GetValueOrDefault() == flag & deliverableArrangement.HasValue && DeferredMethodType.RequiresTerms(deferralCode.Method);
  }

  protected virtual APTran InsertTranOnAddPOReceiptLine(IAPTranSource line, APTran tran)
  {
    return this.Transactions.Insert(tran);
  }

  protected virtual APTran UpdateTranOnAddPOReceiptLine(IAPTranSource line, APTran tran)
  {
    return this.Transactions.Update(tran);
  }

  [MethodImpl(MethodImplOptions.NoInlining)]
  protected virtual void CopyCustomizationFieldsToAPTran(
    APTran apTranToFill,
    IAPTranSource poSourceLine,
    bool areCurrenciesSame)
  {
  }

  private PX.Objects.IN.InventoryItem InventoryItemGetByID(int? inventoryID)
  {
    return PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID);
  }

  private DRDeferredCode DeferredCodeGetByID(string deferredCodeID)
  {
    return (DRDeferredCode) PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select((PXGraph) this, (object) deferredCodeID);
  }

  public virtual void CheckOrderTaskRule(PXCache sender, APTran row, int? newTaskID)
  {
    if (row.POOrderType == null || row.PONbr == null || !row.POLineNbr.HasValue || POLineType.IsStock(row.LineType))
      return;
    PX.Objects.PO.POLine poLine = (PX.Objects.PO.POLine) PXSelectBase<PX.Objects.PO.POLine, PXSelectReadonly<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>.Config>.Select((PXGraph) this, (object) row.POOrderType, (object) row.PONbr, (object) row.POLineNbr);
    if (poLine == null || !poLine.TaskID.HasValue)
      return;
    int? taskId = poLine.TaskID;
    int? nullable = newTaskID;
    if (taskId.GetValueOrDefault() == nullable.GetValueOrDefault() & taskId.HasValue == nullable.HasValue)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
    string str1;
    if (dirty == null)
    {
      str1 = (string) null;
    }
    else
    {
      string str2 = str1 = dirty.TaskCD;
    }
    string newValue = str1;
    if (this.posetup.Current.OrderRequestApproval.GetValueOrDefault())
    {
      string message = poLine.OrderType == "RS" ? "The project and project task cannot be changed because the line is linked to the subcontract." : "Given Task differs from the Project Task selected on Purchase Order line. Since Purchase Order document explicitly requires approval and was approved Project Task cannot be changed on the Receipt.";
      sender.RaiseExceptionHandling<APTran.taskID>((object) row, (object) newValue, (Exception) new PXSetPropertyException(message, PXErrorLevel.Error));
    }
    else
    {
      string message = poLine.OrderType == "RS" ? "The selected project and project task differ from those of the specified subcontract." : "Given Task differs from the Project Task selected on Purchase Order line.";
      sender.RaiseExceptionHandling<APTran.taskID>((object) row, (object) newValue, (Exception) new PXSetPropertyException(message, PXErrorLevel.Warning));
    }
  }

  public virtual void CheckProjectAccountRule(PXCache sender, APTran row)
  {
    int? projectId = row.ProjectID;
    int? nullable = ProjectDefaultAttribute.NonProject();
    if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue)
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, row.AccountID);
    if (account == null || account.AccountGroupID.HasValue)
      return;
    sender.RaiseExceptionHandling<APTran.accountID>((object) row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Account {0} is not mapped to any project account group. Either map the account or select a non-project code.", PXErrorLevel.Error, new object[1]
    {
      (object) account.AccountCD
    }));
  }

  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultAPDiscountCalculationOptions(
    APInvoice doc)
  {
    return (DiscountEngine.DiscountCalculationOptions) (9 | (doc == null || !doc.DisableAutomaticDiscountCalculation.GetValueOrDefault() ? 0 : 4));
  }

  protected virtual void RecalculateDiscounts(PXCache sender, APTran line)
  {
    DiscountEngine.DiscountCalculationOptions calculationOptions = this.GetDefaultAPDiscountCalculationOptions(this.Document.Current);
    if (line.CalculateDiscountsOnImport.GetValueOrDefault())
      calculationOptions |= DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorDiscounts>() && line.CuryLineAmt.HasValue)
    {
      if (line.Qty.HasValue)
      {
        bool flag = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>();
        if (flag)
          line.SuppliedByVendorID = this.Document.Current.SuppliedByVendorID;
        this._discountEngine.SetDiscounts(sender, (PXSelectBase<APTran>) this.Transactions, line, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, this.Document.Current.BranchID, flag ? this.Document.Current.SuppliedByVendorLocationID : this.Document.Current.VendorLocationID, this.Document.Current.CuryID, this.Document.Current.DocDate, this.recalcdiscountsfilter.Current, calculationOptions);
      }
      this.RecalculateTotalDiscount();
    }
    else
    {
      if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorDiscounts>() || this.Document.Current == null)
        return;
      this._discountEngine.CalculateDocumentDiscountRate(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, line, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, calculationOptions);
    }
  }

  public virtual void AutoRecalculateDiscounts()
  {
    this._discountEngine.AutoRecalculatePricesAndDiscounts(this.Transactions.Cache, (PXSelectBase<APTran>) this.Transactions, (APTran) null, (PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorRelations>() ? this.Document.Current.SuppliedByVendorLocationID : this.Document.Current.VendorLocationID, this.Document.Current.DocDate, this.GetDefaultAPDiscountCalculationOptions(this.Document.Current));
    this.RecalculateTotalDiscount();
  }

  private void RecalculateApplicationsAmounts(APInvoiceEntry.MultiCurrency MultiCurrencyExt)
  {
    APInvoice current = this.Document.Current;
    Decimal valueOrDefault1 = current.CuryDocBal.GetValueOrDefault();
    Decimal valueOrDefault2 = current.DocBal.GetValueOrDefault();
    foreach (PXResult<APInvoiceEntry.APAdjust> pxResult in this.Adjustments.Select())
    {
      APInvoiceEntry.APAdjust apAdjust1 = (APInvoiceEntry.APAdjust) pxResult;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = MultiCurrencyExt.GetCurrencyInfo(apAdjust1.AdjdCuryInfoID);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
      Decimal? nullable1 = apAdjust1.CuryAdjdAmt;
      Decimal curyval1 = nullable1.Value;
      Decimal baseval = currencyInfo2.CuryConvBase(curyval1);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = MultiCurrencyExt.GetCurrencyInfo(apAdjust1.AdjgCuryInfoID);
      Decimal num1;
      if (!string.Equals(currencyInfo3.CuryID, currencyInfo1.CuryID))
      {
        num1 = currencyInfo3.CuryConvCury(baseval);
      }
      else
      {
        nullable1 = apAdjust1.CuryAdjdAmt;
        num1 = nullable1.Value;
      }
      Decimal curyval2 = num1;
      Decimal num2 = !object.Equals((object) currencyInfo3.CuryID, (object) currencyInfo1.CuryID) || !object.Equals((object) currencyInfo3.CuryRate, (object) currencyInfo1.CuryRate) || !object.Equals((object) currencyInfo3.CuryMultDiv, (object) currencyInfo1.CuryMultDiv) ? currencyInfo3.CuryConvBase(curyval2) : baseval;
      apAdjust1.CuryAdjgAmt = new Decimal?(curyval2);
      APInvoiceEntry.APAdjust apAdjust2 = apAdjust1;
      Decimal num3 = valueOrDefault1;
      nullable1 = apAdjust1.CuryAdjdAmt;
      Decimal num4 = nullable1.Value;
      Decimal? nullable2 = new Decimal?(num3 == num4 ? valueOrDefault2 : baseval);
      apAdjust2.AdjAmt = nullable2;
      APInvoiceEntry.APAdjust apAdjust3 = apAdjust1;
      Decimal num5 = num2;
      nullable1 = apAdjust1.AdjAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num5 - nullable1.GetValueOrDefault()) : new Decimal?();
      apAdjust3.RGOLAmt = nullable3;
      valueOrDefault1 -= curyval2;
      valueOrDefault2 -= baseval;
    }
  }

  private void RecalculateTotalDiscount()
  {
    if (this.Document.Current == null || this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Deleted || this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.InsertedDeleted)
      return;
    APInvoice copy = PXCache<APInvoice>.CreateCopy(this.Document.Current);
    this.Document.Cache.SetValueExt<APInvoice.curyDiscTot>((object) this.Document.Current, (object) this._discountEngine.GetTotalGroupAndDocumentDiscount<APInvoiceDiscountDetail>((PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails));
    this.Document.Cache.RaiseRowUpdated((object) this.Document.Current, (object) copy);
  }

  public virtual void SetProjectIDForDiscountTran(APTran apTran)
  {
    int?[] array = this.Transactions.Select().RowCast<APTran>().Where<APTran>((Func<APTran, bool>) (tran => tran.ProjectID.HasValue)).Select<APTran, int?>((Func<APTran, int?>) (tran => tran.ProjectID)).Distinct<int?>().ToArray<int?>();
    int? nullable = array.Length == 1 ? ((IEnumerable<int?>) array).Single<int?>() : ProjectDefaultAttribute.NonProject();
    apTran.ProjectID = nullable;
  }

  public virtual void SetTaskIDForDiscountTran(APTran apTran)
  {
    PMProject pmProject = (PMProject) PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.Select((PXGraph) this, (object) apTran.ProjectID);
    if (pmProject == null || !(pmProject.BaseType != "C"))
      return;
    PMAccountTask pmAccountTask = (PMAccountTask) PXSelectBase<PMAccountTask, PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Required<PMAccountTask.projectID>>, And<PMAccountTask.accountID, Equal<Required<PMAccountTask.accountID>>>>>.Config>.Select((PXGraph) this, (object) apTran.ProjectID, (object) apTran.AccountID);
    if (pmAccountTask != null)
    {
      apTran.TaskID = pmAccountTask.TaskID;
    }
    else
    {
      PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, (object) apTran.AccountID);
      throw new PXException("Account Task Mapping is not configured for a document discount line. Project: {0}, Account: {1}", new object[2]
      {
        (object) pmProject.ContractCD.Trim(),
        (object) account.AccountCD.Trim()
      });
    }
  }

  public virtual void SetCostCodeForDiscountTran(APTran apTran)
  {
    if (!CostCodeAttribute.UseCostCode())
      return;
    apTran.CostCodeID = CostCodeAttribute.DefaultCostCode;
  }

  protected virtual void AddDiscount(PXCache sender, APInvoice row)
  {
    APTran instance = (APTran) this.Discount_Row.Cache.CreateInstance();
    instance.LineType = "DS";
    instance.DrCr = this.Document.Current.DrCr == "D" ? "C" : "D";
    APTran apTran = (APTran) this.Discount_Row.Select() ?? (APTran) this.Discount_Row.Cache.Insert((object) instance);
    APTran copy = (APTran) this.Discount_Row.Cache.CreateCopy((object) apTran);
    apTran.CuryTranAmt = (Decimal?) sender.GetValue<APInvoice.curyDiscTot>((object) row);
    apTran.TaxCategoryID = (string) null;
    using (new PXLocaleScope(this.vendor.Current?.LocaleName))
      apTran.TranDesc = PXMessages.LocalizeNoPrefix("Group and Document Discount");
    this.DefaultDiscountAccountAndSubAccount(apTran);
    if (!apTran.ProjectID.HasValue)
      this.SetProjectIDForDiscountTran(apTran);
    this.SetCostCodeForDiscountTran(apTran);
    if (!apTran.ProjectID.HasValue && copy.ProjectID.HasValue)
      apTran.ProjectID = copy.ProjectID;
    if (!apTran.TaskID.HasValue && !ProjectDefaultAttribute.IsNonProject(apTran.ProjectID))
      this.SetTaskIDForDiscountTran(apTran);
    this.Discount_Row.Cache.MarkUpdated((object) apTran);
    apTran.ManualDisc = new bool?(true);
    this.Discount_Row.Cache.RaiseRowUpdated((object) apTran, (object) copy);
    Decimal documentDiscount = this._discountEngine.GetTotalGroupAndDocumentDiscount<APInvoiceDiscountDetail>((PXSelectBase<APInvoiceDiscountDetail>) this.DiscountDetails);
    Decimal? curyTranAmt = apTran.CuryTranAmt;
    Decimal valueOrDefault = curyTranAmt.GetValueOrDefault();
    if (!(documentDiscount == valueOrDefault & curyTranAmt.HasValue))
      return;
    apTran.ManualDisc = new bool?(false);
  }

  protected object GetValue<Field>(object data) where Field : IBqlField
  {
    return this.Caches[BqlCommand.GetItemType(typeof (Field))].GetValue(data, typeof (Field).Name);
  }

  private void DefaultDiscountAccountAndSubAccount(APTran tran)
  {
    PX.Objects.CR.Location current = this.location.Current;
    object obj1 = this.GetValue<PX.Objects.CR.Location.vDiscountAcctID>((object) current);
    if (obj1 != null)
    {
      tran.AccountID = (int?) obj1;
      this.Discount_Row.Cache.RaiseFieldUpdated<APTran.accountID>((object) tran, (object) null);
    }
    if (!tran.AccountID.HasValue)
      return;
    object obj2 = this.GetValue<PX.Objects.CR.Location.vDiscountSubID>((object) current);
    if (obj2 == null)
      return;
    tran.SubID = (int?) obj2;
    this.Discount_Row.Cache.RaiseFieldUpdated<APTran.subID>((object) tran, (object) null);
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, "Transactions", true) == 0)
    {
      if (values.Contains((object) "tranType"))
        values[(object) "tranType"] = (object) this.Document.Current.DocType;
      else
        values.Add((object) "tranType", (object) this.Document.Current.DocType);
      if (values.Contains((object) "refNbr"))
        values[(object) "refNbr"] = (object) this.Document.Current.RefNbr;
      else
        values.Add((object) "refNbr", (object) this.Document.Current.RefNbr);
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual APInvoice CalculateExternalTax(APInvoice invoice) => invoice;

  public virtual APInvoice CreatePPDDebitAdj(
    APPPDVATAdjParameters filter,
    List<PendingPPDVATAdjApp> list)
  {
    bool flag = true;
    APInvoice row = (APInvoice) this.Document.Cache.CreateInstance();
    foreach (PendingPPDVATAdjApp doc in list)
    {
      if (flag)
      {
        flag = false;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, (object) doc.InvCuryInfoID);
        currencyInfo1.CuryInfoID = new long?();
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.currencyinfo.Insert(currencyInfo1);
        row.DocType = "ADR";
        row = PXCache<APInvoice>.CreateCopy(this.Document.Insert(row));
        row.VendorID = doc.VendorID;
        row.VendorLocationID = doc.InvVendorLocationID;
        row.CuryInfoID = currencyInfo2.CuryInfoID;
        row.CuryID = currencyInfo2.CuryID;
        Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Config>.Select((PXGraph) this, (object) doc.VendorID);
        row.DocDesc = PXDBLocalizableStringAttribute.GetTranslation(this.Caches[typeof (PX.Objects.AP.APSetup)], (object) this.APSetup.Current, "pPDDebitAdjustmentDescr", vendor?.LocaleName);
        row.BranchID = doc.AdjdBranchID;
        APInvoice apInvoice = row;
        bool? generateOnePerVendor = filter.GenerateOnePerVendor;
        System.DateTime? nullable = generateOnePerVendor.GetValueOrDefault() ? filter.DebitAdjDate : doc.AdjgDocDate;
        apInvoice.DocDate = nullable;
        row.APAccountID = doc.AdjdAPAcct;
        row.APSubID = doc.AdjdAPSub;
        row.TaxZoneID = doc.InvTaxZoneID;
        row.PendingPPD = new bool?(true);
        row.SuppliedByVendorLocationID = doc.InvVendorLocationID;
        row.Hold = new bool?(false);
        row.TaxCalcMode = doc.InvTaxCalcMode;
        row = this.Document.Update(row);
        generateOnePerVendor = filter.GenerateOnePerVendor;
        string masterFinPeriodID = generateOnePerVendor.GetValueOrDefault() ? this.FinPeriodRepository.GetByID(filter.FinPeriodID, PXAccess.GetParentOrganizationID(filter.BranchID)).MasterFinPeriodID : doc.AdjgTranPeriodID;
        FinPeriodIDAttribute.SetPeriodsByMaster<APInvoice.finPeriodID>(this.Document.Cache, (object) row, masterFinPeriodID);
        if (row.TaxCalcMode != doc.InvTaxCalcMode)
        {
          row.TaxCalcMode = doc.InvTaxCalcMode;
          row = this.Document.Update(row);
        }
      }
      this.AddTaxes(doc);
    }
    EnumerableExtensions.ForEach<APInvoiceDiscountDetail>(this.DiscountDetails.Select().RowCast<APInvoiceDiscountDetail>(), (System.Action<APInvoiceDiscountDetail>) (discountDetail => this.DiscountDetails.Cache.Delete((object) discountDetail)));
    bool? nullable1 = this.APSetup.Current.RequireControlTotal;
    if (nullable1.GetValueOrDefault())
    {
      row.CuryOrigDocAmt = row.CuryDocBal;
      this.Document.Cache.Update((object) row);
    }
    nullable1 = this.APSetup.Current.RequireControlTaxTotal;
    if (nullable1.GetValueOrDefault())
    {
      row.CuryTaxAmt = row.CuryTaxTotal;
      this.Document.Cache.Update((object) row);
    }
    row.CuryOrigDiscAmt = new Decimal?(0M);
    row.DiscDate = row.DueDate;
    APInvoice ppdDebitAdj = this.Document.Update(row);
    this.Save.Press();
    return ppdDebitAdj;
  }

  public virtual APInvoice CreatePPDCreditAdj(
    APPPDVATAdjParameters filter,
    List<PendingPPDVATAdjApp> list)
  {
    bool flag = true;
    APInvoice row = (APInvoice) this.Document.Cache.CreateInstance();
    foreach (PendingPPDVATAdjApp doc in list)
    {
      if (flag)
      {
        flag = false;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, (object) doc.InvCuryInfoID);
        currencyInfo1.CuryInfoID = new long?();
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.currencyinfo.Insert(currencyInfo1);
        row.DocType = "ACR";
        row = PXCache<APInvoice>.CreateCopy(this.Document.Insert(row));
        row.VendorID = doc.VendorID;
        row.VendorLocationID = doc.InvVendorLocationID;
        row.CuryInfoID = currencyInfo2.CuryInfoID;
        row.CuryID = currencyInfo2.CuryID;
        Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Config>.Select((PXGraph) this, (object) doc.VendorID);
        row.DocDesc = PXDBLocalizableStringAttribute.GetTranslation(this.Caches[typeof (PX.Objects.AP.APSetup)], (object) this.APSetup.Current, "pPDDebitAdjustmentDescr", vendor?.LocaleName);
        row.BranchID = doc.AdjdBranchID;
        APInvoice apInvoice = row;
        bool? generateOnePerVendor = filter.GenerateOnePerVendor;
        System.DateTime? nullable = generateOnePerVendor.GetValueOrDefault() ? filter.DebitAdjDate : doc.AdjgDocDate;
        apInvoice.DocDate = nullable;
        row.APAccountID = doc.AdjdAPAcct;
        row.APSubID = doc.AdjdAPSub;
        row.TaxZoneID = doc.InvTaxZoneID;
        row.PendingPPD = new bool?(true);
        row.SuppliedByVendorLocationID = doc.InvVendorLocationID;
        row.Hold = new bool?(false);
        row.TaxCalcMode = doc.InvTaxCalcMode;
        row = this.Document.Update(row);
        generateOnePerVendor = filter.GenerateOnePerVendor;
        string masterFinPeriodID = generateOnePerVendor.GetValueOrDefault() ? this.FinPeriodRepository.GetByID(filter.FinPeriodID, PXAccess.GetParentOrganizationID(filter.BranchID)).MasterFinPeriodID : doc.AdjgTranPeriodID;
        FinPeriodIDAttribute.SetPeriodsByMaster<APInvoice.finPeriodID>(this.Document.Cache, (object) row, masterFinPeriodID);
        if (row.TaxCalcMode != doc.InvTaxCalcMode)
        {
          row.TaxCalcMode = doc.InvTaxCalcMode;
          row = this.Document.Update(row);
        }
      }
      this.AddTaxes(doc);
    }
    EnumerableExtensions.ForEach<APInvoiceDiscountDetail>(this.DiscountDetails.Select().RowCast<APInvoiceDiscountDetail>(), (System.Action<APInvoiceDiscountDetail>) (discountDetail => this.DiscountDetails.Cache.Delete((object) discountDetail)));
    bool? nullable1 = this.APSetup.Current.RequireControlTotal;
    if (nullable1.GetValueOrDefault())
    {
      row.CuryOrigDocAmt = row.CuryDocBal;
      this.Document.Cache.Update((object) row);
    }
    nullable1 = this.APSetup.Current.RequireControlTaxTotal;
    if (nullable1.GetValueOrDefault())
    {
      row.CuryTaxAmt = row.CuryTaxTotal;
      this.Document.Cache.Update((object) row);
    }
    this.autoApply.Press();
    foreach (PXResult<APInvoiceEntry.APAdjust> pxResult in this.Adjustments.Select())
    {
      APInvoiceEntry.APAdjust adj = (APInvoiceEntry.APAdjust) pxResult;
      PendingPPDVATAdjApp pendingPpdvatAdjApp = list.Where<PendingPPDVATAdjApp>((Func<PendingPPDVATAdjApp, bool>) (doc => doc.AdjdDocType == adj.AdjgDocType && doc.AdjdRefNbr == adj.AdjgRefNbr)).FirstOrDefault<PendingPPDVATAdjApp>();
      if (pendingPpdvatAdjApp != null)
      {
        adj.CuryAdjdAmt = pendingPpdvatAdjApp.InvCuryDocBal;
        adj = this.Adjustments.Update(adj);
      }
    }
    row.CuryOrigDiscAmt = new Decimal?(0M);
    row.DiscDate = row.DueDate;
    APInvoice ppdCreditAdj = this.Document.Update(row);
    this.Save.Press();
    return ppdCreditAdj;
  }

  public virtual void AddTaxes(PendingPPDVATAdjApp doc)
  {
    APTaxTran apTaxTran1 = (APTaxTran) null;
    Decimal? TaxTotal = new Decimal?(0M);
    Decimal? InclusiveTotal = new Decimal?(0M);
    Decimal? nullable1 = new Decimal?(0M);
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? curyAdjdPpdAmt1 = doc.CuryAdjdPPDAmt;
    Decimal? invCuryOrigDocAmt1 = doc.InvCuryOrigDocAmt;
    Decimal? nullable3 = curyAdjdPpdAmt1.HasValue & invCuryOrigDocAmt1.HasValue ? new Decimal?(curyAdjdPpdAmt1.GetValueOrDefault() / invCuryOrigDocAmt1.GetValueOrDefault()) : new Decimal?();
    Decimal cashDiscPercent = nullable3.Value;
    PXResultset<APTaxTran> taxes = PXSelectBase<APTaxTran, PXSelectJoin<APTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>, And<PX.Objects.TX.Tax.taxApplyTermsDisc, Equal<CSTaxTermsDiscount.toPromtPayment>>>>>>.Config>.Select((PXGraph) this, (object) doc.AdjdDocType, (object) doc.AdjdRefNbr);
    foreach (PXResult<APTaxTran, PX.Objects.TX.Tax> pxResult in taxes)
    {
      PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
      APTaxTran copy = PXCache<APTaxTran>.CreateCopy((APTaxTran) pxResult);
      APTaxTran apTaxTran2 = (APTaxTran) this.Taxes.Search<APTaxTran.taxID>((object) copy.TaxID);
      if (apTaxTran2 == null)
      {
        copy.TranType = (string) null;
        copy.RefNbr = (string) null;
        copy.TaxPeriodID = (string) null;
        copy.Released = new bool?(false);
        copy.Voided = new bool?(false);
        copy.FinPeriodID = (string) null;
        copy.CuryInfoID = this.Document.Current.CuryInfoID;
        TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.NoCalc);
        apTaxTran2 = this.Taxes.Insert(copy);
        apTaxTran2.CuryTaxableAmt = new Decimal?(0M);
        apTaxTran2.CuryTaxAmt = new Decimal?(0M);
        apTaxTran2.CuryTaxAmtSumm = new Decimal?(0M);
        apTaxTran2.TaxRate = copy.TaxRate;
      }
      int num1 = APPPDDebitAdjProcess.CalculateDiscountedTaxes(this.Taxes.Cache, copy, cashDiscPercent) ? 1 : 0;
      Decimal? nullable4 = copy.CuryTaxableAmt;
      Decimal? nullable5 = copy.CuryDiscountedTaxableAmt;
      Decimal? nullable6;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new Decimal?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault());
      Decimal? nullable7 = nullable6;
      nullable5 = copy.CuryTaxAmt;
      nullable4 = copy.CuryDiscountedPrice;
      Decimal? nullable8;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable8 = nullable3;
      }
      else
        nullable8 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
      Decimal? nullable9 = nullable8;
      bool? reverseTax = tax.ReverseTax;
      Decimal num2 = reverseTax.GetValueOrDefault() ? -1M : 1M;
      nullable4 = nullable2;
      nullable3 = copy.CuryDiscountedPrice;
      Decimal num3 = num2;
      nullable5 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num3) : new Decimal?();
      Decimal? nullable10;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable10 = nullable3;
      }
      else
        nullable10 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
      nullable2 = nullable10;
      APTaxTran apTaxTran3 = apTaxTran2;
      nullable5 = apTaxTran3.CuryTaxableAmt;
      nullable4 = nullable7;
      Decimal? nullable11;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable11 = nullable3;
      }
      else
        nullable11 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
      apTaxTran3.CuryTaxableAmt = nullable11;
      APTaxTran apTaxTran4 = apTaxTran2;
      nullable4 = apTaxTran4.CuryTaxAmt;
      nullable5 = nullable9;
      Decimal? nullable12;
      if (!(nullable4.HasValue & nullable5.HasValue))
      {
        nullable3 = new Decimal?();
        nullable12 = nullable3;
      }
      else
        nullable12 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
      apTaxTran4.CuryTaxAmt = nullable12;
      APTaxTran apTaxTran5 = apTaxTran2;
      nullable5 = apTaxTran5.CuryTaxAmtSumm;
      nullable4 = nullable9;
      Decimal? nullable13;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable13 = nullable3;
      }
      else
        nullable13 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
      apTaxTran5.CuryTaxAmtSumm = nullable13;
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
      this.Taxes.Update(apTaxTran2);
      if (num1 != 0)
      {
        reverseTax = tax.ReverseTax;
        if (!reverseTax.GetValueOrDefault())
        {
          nullable4 = nullable1;
          nullable5 = copy.CuryDiscountedTaxableAmt;
          Decimal? nullable14;
          if (!(nullable4.HasValue & nullable5.HasValue))
          {
            nullable3 = new Decimal?();
            nullable14 = nullable3;
          }
          else
            nullable14 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
          nullable1 = nullable14;
        }
        if (apTaxTran1 != null)
        {
          nullable5 = apTaxTran2.CuryTaxableAmt;
          nullable4 = apTaxTran1.CuryTaxableAmt;
          if (!(nullable5.GetValueOrDefault() > nullable4.GetValueOrDefault() & nullable5.HasValue & nullable4.HasValue))
            goto label_31;
        }
        apTaxTran1 = apTaxTran2;
      }
label_31:
      bool flag = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>();
      if (tax.TaxCalcLevel == "0" && (!flag || this.Document.Current.TaxCalcMode != "N") || flag && this.Document.Current.TaxCalcMode == "G")
      {
        nullable4 = InclusiveTotal;
        nullable5 = nullable9;
        Decimal? nullable15;
        if (!(nullable4.HasValue & nullable5.HasValue))
        {
          nullable3 = new Decimal?();
          nullable15 = nullable3;
        }
        else
          nullable15 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
        InclusiveTotal = nullable15;
      }
      else
      {
        nullable5 = TaxTotal;
        nullable3 = nullable9;
        Decimal num4 = num2;
        nullable4 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num4) : new Decimal?();
        Decimal? nullable16;
        if (!(nullable5.HasValue & nullable4.HasValue))
        {
          nullable3 = new Decimal?();
          nullable16 = nullable3;
        }
        else
          nullable16 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
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
    if (nullable19.GetValueOrDefault() == curyAdjdPpdAmt2.GetValueOrDefault() & nullable19.HasValue == curyAdjdPpdAmt2.HasValue && apTaxTran1 != null)
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
          APTaxTran apTaxTran6 = apTaxTran1;
          nullable21 = apTaxTran6.CuryTaxableAmt;
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
          apTaxTran6.CuryTaxableAmt = nullable24;
          TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
          this.Taxes.Update(apTaxTran1);
        }
      }
    }
    this.AddPPDDebitAdjDetails(doc, TaxTotal, InclusiveTotal, taxes);
  }

  [InjectDependency]
  protected IFinPeriodUtils _finPeriodUtils { get; set; }

  public virtual void AddPPDDebitAdjDetails(
    PendingPPDVATAdjApp doc,
    Decimal? TaxTotal,
    Decimal? InclusiveTotal,
    PXResultset<APTaxTran> taxes)
  {
    Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>.Config>.Select((PXGraph) this, (object) doc.VendorID);
    APTran apTran1 = this.Transactions.Insert();
    apTran1.BranchID = doc.AdjdBranchID;
    using (new PXLocaleScope(vendor.LocaleName))
      apTran1.TranDesc = $"{PXMessages.LocalizeNoPrefix(APInvoiceEntry.DocTypes[doc.AdjdDocType])} {doc.AdjdRefNbr}, {PXMessages.LocalizeNoPrefix("Payment")} {doc.AdjgRefNbr}";
    APTran apTran2 = apTran1;
    Decimal? nullable1 = doc.InvCuryDocBal;
    Decimal? nullable2 = TaxTotal;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    apTran2.CuryLineAmt = nullable3;
    APTran apTran3 = apTran1;
    Decimal? invCuryDocBal = doc.InvCuryDocBal;
    Decimal? nullable4 = TaxTotal;
    nullable2 = invCuryDocBal.HasValue & nullable4.HasValue ? new Decimal?(invCuryDocBal.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    nullable1 = InclusiveTotal;
    Decimal? nullable5;
    if (!(nullable2.HasValue & nullable1.HasValue))
    {
      nullable4 = new Decimal?();
      nullable5 = nullable4;
    }
    else
      nullable5 = new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault());
    apTran3.CuryTaxableAmt = nullable5;
    APTran apTran4 = apTran1;
    nullable1 = TaxTotal;
    nullable2 = InclusiveTotal;
    Decimal? nullable6;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable4 = new Decimal?();
      nullable6 = nullable4;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    apTran4.CuryTaxAmt = nullable6;
    apTran1.AccountID = vendor.DiscTakenAcctID;
    apTran1.SubID = vendor.DiscTakenSubID;
    apTran1.TaxCategoryID = (string) null;
    apTran1.ManualDisc = new bool?(true);
    apTran1.CuryDiscAmt = new Decimal?(0M);
    apTran1.DiscPct = new Decimal?(0M);
    apTran1.GroupDiscountRate = new Decimal?(1M);
    apTran1.DocumentDiscountRate = new Decimal?(1M);
    if (taxes.Count == 1)
    {
      APTaxTran tax = (APTaxTran) taxes[0];
      APTran apTran5 = (APTran) PXSelectBase<APTran, PXSelectJoin<APTran, InnerJoin<APTax, On<APTax.tranType, Equal<APTran.tranType>, And<APTax.refNbr, Equal<APTran.refNbr>, And<APTax.lineNbr, Equal<APTran.lineNbr>>>>>, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>, And<APTax.taxID, Equal<Required<APTax.taxID>>>>>, PX.Data.OrderBy<Asc<APTran.lineNbr>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) tax.TranType, (object) tax.RefNbr, (object) tax.TaxID);
      if (apTran5 != null)
        apTran1.TaxCategoryID = apTran5.TaxCategoryID;
    }
    this.Transactions.Update(apTran1);
  }

  public virtual POAccrualSet GetUsedPOAccrualSet()
  {
    return new POAccrualSet(this.Transactions.Select().RowCast<APTran>(), this.Document.Current?.DocType == "PPM" ? (IEqualityComparer<APTran>) new POLineComparer() : (IEqualityComparer<APTran>) new POAccrualComparer());
  }

  public class APInvoiceEntryDocumentExtension : 
    InvoiceGraphExtension<APInvoiceEntry, APInvoiceEntry.APAdjust>
  {
    public override PXSelectBase<PX.Objects.CR.Location> Location
    {
      get => (PXSelectBase<PX.Objects.CR.Location>) this.Base.location;
    }

    public override void SuppressApproval() => this.Base.Approval.SuppressApproval = true;

    public override PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo> CurrencyInfo
    {
      get => (PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.currencyinfo;
    }

    public override PXSelectBase<APInvoiceEntry.APAdjust> AppliedAdjustments
    {
      get => (PXSelectBase<APInvoiceEntry.APAdjust>) this.Base.Adjustments;
    }

    public PXSelectBase<APInvoiceEntry.APAdjust2> ApplyingAdjustments
    {
      get => (PXSelectBase<APInvoiceEntry.APAdjust2>) this.Base.Adjustments_1;
    }

    public override void Initialize()
    {
      base.Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice>((PXSelectBase) this.Base.Document);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.AllTransactions);
      this.InvoiceTrans = new PXSelectExtension<InvoiceTran>((PXSelectBase) this.Base.Transactions);
      this.TaxTrans = new PXSelectExtension<GenericTaxTran>((PXSelectBase) this.Base.Taxes);
      this.LineTaxes = new PXSelectExtension<LineTax>((PXSelectBase) this.Base.Tax_Rows);
    }

    protected override InvoiceMapping GetDocumentMapping()
    {
      return new InvoiceMapping(typeof (APInvoice))
      {
        HeaderTranPeriodID = typeof (APInvoice.tranPeriodID),
        HeaderDocDate = typeof (APInvoice.docDate),
        ContragentID = typeof (APInvoice.vendorID),
        ContragentLocationID = typeof (APInvoice.vendorLocationID),
        ModuleAccountID = typeof (APInvoice.aPAccountID),
        ModuleSubID = typeof (APInvoice.aPSubID)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (APTran));
    }

    protected override ContragentMapping GetContragentMapping()
    {
      return new ContragentMapping(typeof (Vendor));
    }

    protected override InvoiceTranMapping GetInvoiceTranMapping()
    {
      return new InvoiceTranMapping(typeof (APTran));
    }

    protected override GenericTaxTranMapping GetGenericTaxTranMapping()
    {
      return new GenericTaxTranMapping(typeof (APTaxTran));
    }

    protected override LineTaxMapping GetLineTaxMapping() => new LineTaxMapping(typeof (APTax));

    protected override bool ShouldUpdateAdjustmentsOnDocumentUpdated(PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice> e)
    {
      return base.ShouldUpdateAdjustmentsOnDocumentUpdated(e) || !e.Cache.ObjectsEqual<InvoiceBase.moduleAccountID, InvoiceBase.moduleSubID, PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.branchID>((object) e.Row, (object) e.OldRow);
    }

    protected override void _(PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Invoice> e)
    {
      base._(e);
      if (!this.ShouldUpdateAdjustmentsOnDocumentUpdated(e))
        return;
      foreach (PXResult<APInvoiceEntry.APAdjust2> pxResult in this.ApplyingAdjustments.Select())
      {
        APInvoiceEntry.APAdjust2 apAdjust2 = (APInvoiceEntry.APAdjust2) pxResult;
        if (!e.Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.branchID>((object) e.Row, (object) e.OldRow))
          this.ApplyingAdjustments.Cache.SetDefaultExt<Adjust.adjgBranchID>((object) apAdjust2);
        if (!e.Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerDocDate>((object) e.Row, (object) e.OldRow))
          this.ApplyingAdjustments.Cache.SetDefaultExt<Adjust.adjgDocDate>((object) apAdjust2);
        if (!e.Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerTranPeriodID>((object) e.Row, (object) e.OldRow))
          FinPeriodIDAttribute.SetPeriodsByMaster<Adjust.adjgFinPeriodID>(this.ApplyingAdjustments.Cache, (object) apAdjust2, e.Row.HeaderTranPeriodID);
        if (this.ApplyingAdjustments.Cache is PXModelExtension<Adjust> cache)
          cache.UpdateExtensionMapping((object) apAdjust2);
        this.ApplyingAdjustments.Cache.MarkUpdated((object) apAdjust2);
      }
    }
  }

  public class CostAccrual : NonStockAccrualGraph<APInvoiceEntry, APInvoice>
  {
    [PXOverride]
    public virtual void SetExpenseAccount(
      PXCache sender,
      PXFieldDefaultingEventArgs e,
      Action<PXCache, PXFieldDefaultingEventArgs> baseMethod)
    {
      APTran row = (APTran) e.Row;
      if (row == null || !row.AccrueCost.GetValueOrDefault())
        return;
      this.SetExpenseAccountSub(sender, e, row.InventoryID, row.SiteID, (NonStockAccrualGraph<APInvoiceEntry, APInvoice>.GetAccountSubUsingPostingClassDelegate) ((inItem, inSite, inPostClass) => (object) INReleaseProcess.GetAcctID<INPostClass.invtAcctID>((PXGraph) this.Base, inPostClass.InvtAcctDefault, inItem, inSite, inPostClass)), (NonStockAccrualGraph<APInvoiceEntry, APInvoice>.GetAccountSubFromItemDelegate) (inItem => (object) inItem.InvtAcctID));
    }

    [PXOverride]
    public virtual object GetExpenseSub(
      PXCache sender,
      PXFieldDefaultingEventArgs e,
      Func<PXCache, PXFieldDefaultingEventArgs, object> baseMethod)
    {
      APTran row = (APTran) e.Row;
      object expenseSub = (object) null;
      if (row != null && row.AccrueCost.GetValueOrDefault())
        expenseSub = this.GetExpenseAccountSub(sender, e, row.InventoryID, row.SiteID, (NonStockAccrualGraph<APInvoiceEntry, APInvoice>.GetAccountSubUsingPostingClassDelegate) ((inItem, inSite, inPostClass) => (object) INReleaseProcess.GetSubID<INPostClass.invtSubID>((PXGraph) this.Base, inPostClass.InvtAcctDefault, inPostClass.InvtSubMask, inItem, inSite, inPostClass)), (NonStockAccrualGraph<APInvoiceEntry, APInvoice>.GetAccountSubFromItemDelegate) (inItem => (object) inItem.InvtSubID));
      return expenseSub;
    }
  }

  public class MultiCurrency : APMultiCurrencyGraph<APInvoiceEntry, APInvoice>
  {
    protected override string DocumentStatus => this.Base.Document.Current?.Status;

    protected override MultiCurrencyGraph<APInvoiceEntry, APInvoice>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<APInvoiceEntry, APInvoice>.DocumentMapping(typeof (APInvoice))
      {
        DocumentDate = typeof (APInvoice.docDate),
        BAccountID = typeof (APInvoice.vendorID)
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
        (PXSelectBase) this.Base.DiscountDetails,
        (PXSelectBase) this.Base.Adjustments,
        (PXSelectBase) this.Base.Adjustments_Raw
      };
    }

    protected override bool AllowOverrideCury()
    {
      if (this.ShouldBeDisabledDueToDocStatus())
        return false;
      if (this.Base.Document.Current.DocType == "PPM")
        return !IsPOLinkedAPBill.Ensure(this.Base.Document.Cache, this.Base.Document.Current);
      return (!(this.Base.Document.Current.DocType == "ADR") || !(this.Base.Document.Current.OrigDocType == "PPI")) && base.AllowOverrideCury();
    }

    protected override void _(PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo> e)
    {
      base._(e);
      PX.Objects.CM.Extensions.CurrencyInfo row = e.Row;
      if ((row != null ? (!row.CuryRate.HasValue ? 1 : 0) : 1) != 0 || this.Base.Document.Current == null)
        return;
      APInvoice current = this.Base.Document.Current;
      if ((current != null ? (!current.ReleasedOrPrebooked.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      this.Base.RecalculateApplicationsAmounts(this);
    }

    protected virtual void _(
      PX.Data.Events.FieldSelecting<APPayment, APPayment.curyID> e)
    {
      e.ReturnValue = (object) this.CuryIDFieldSelecting<APRegister.curyInfoID>(e.Cache, (object) e.Row);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
    {
      base._(e);
      this.Base.SetDefaultsAfterVendorIDChanging(this.Base.Document.Cache, (APInvoice) this.Documents.Cache.GetMain<PX.Objects.Extensions.MultiCurrency.Document>(e.Row));
    }
  }

  public class SkipUpdAdjustments(string key) : 
    FlaggedKeyModeScopeBase<APInvoiceEntry.SkipUpdAdjustments>(key)
  {
  }

  [Serializable]
  public class APAdjust : PX.Objects.AP.APAdjust
  {
    [PXDBInt]
    [PXDBDefault(typeof (APInvoice.vendorID))]
    [PXUIField(DisplayName = "Vendor ID", Visibility = PXUIVisibility.Visible, Visible = false)]
    public override int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
    [APPaymentType.List]
    [PXDefault]
    [PXUIField(DisplayName = "Doc. Type", Enabled = false)]
    public override string AdjgDocType
    {
      get => this._AdjgDocType;
      set => this._AdjgDocType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDBDefault(typeof (APRegister.refNbr))]
    [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
    public override string AdjgRefNbr
    {
      get => this._AdjgRefNbr;
      set => this._AdjgRefNbr = value;
    }

    [Branch(null, null, true, true, false)]
    public override int? AdjgBranchID
    {
      get => this._AdjgBranchID;
      set => this._AdjgBranchID = value;
    }

    [PXDBLong]
    [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APInvoice.curyInfoID))]
    public override long? AdjdCuryInfoID
    {
      get => this._AdjdCuryInfoID;
      set => this._AdjdCuryInfoID = value;
    }

    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
    [PXDBDefault(typeof (APInvoice.docType))]
    [PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.Invisible, Visible = false)]
    public override string AdjdDocType
    {
      get => this._AdjdDocType;
      set => this._AdjdDocType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDBDefault(typeof (APInvoice.refNbr))]
    [PXParent(typeof (PX.Data.Select<APInvoice, Where<APInvoice.docType, Equal<Current<APInvoiceEntry.APAdjust.adjdDocType>>, And<APInvoice.refNbr, Equal<Current<APInvoiceEntry.APAdjust.adjdRefNbr>>>>>))]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Invisible, Visible = false)]
    public override string AdjdRefNbr { get; set; }

    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
    [PXDefault(typeof (Switch<Case<Where<Selector<APInvoiceEntry.APAdjust.adjdRefNbr, PX.Objects.AP.APAdjust.APInvoice.paymentsByLinesAllowed>, NotEqual<True>>, PX.Objects.CS.int0>, Null>))]
    [APInvoiceType.AdjdLineNbr]
    public override int? AdjdLineNbr { get; set; }

    [Branch(typeof (APInvoice.branchID), null, true, true, true)]
    public override int? AdjdBranchID
    {
      get => this._AdjdBranchID;
      set => this._AdjdBranchID = value;
    }

    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Adjustment Nbr.", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
    [PXDefault]
    public override int? AdjNbr
    {
      get => this._AdjNbr;
      set => this._AdjNbr = value;
    }

    [PXDBString(40, IsUnicode = true)]
    public override string StubNbr
    {
      get => this._StubNbr;
      set => this._StubNbr = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, Visible = true, Enabled = false)]
    public override string AdjBatchNbr
    {
      get => this._AdjBatchNbr;
      set => this._AdjBatchNbr = value;
    }

    [PXDBLong]
    [PXDBDefault(typeof (APInvoice.curyInfoID))]
    public override long? AdjdOrigCuryInfoID
    {
      get => this._AdjdOrigCuryInfoID;
      set => this._AdjdOrigCuryInfoID = value;
    }

    [PXDBLong]
    public override long? AdjgCuryInfoID
    {
      get => this._AdjgCuryInfoID;
      set => this._AdjgCuryInfoID = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Transaction Date")]
    public override System.DateTime? AdjgDocDate
    {
      get => this._AdjgDocDate;
      set => this._AdjgDocDate = value;
    }

    [FinPeriodID(typeof (APInvoiceEntry.APAdjust.adjgDocDate), typeof (APInvoiceEntry.APAdjust.adjgBranchID), null, null, null, null, true, false, null, typeof (APInvoiceEntry.APAdjust.adjgTranPeriodID), null, true, true)]
    [PXUIField(DisplayName = "Application Period")]
    public override string AdjgFinPeriodID
    {
      get => this._AdjgFinPeriodID;
      set => this._AdjgFinPeriodID = value;
    }

    [PeriodID(null, null, null, true)]
    public override string AdjgTranPeriodID
    {
      get => this._AdjgTranPeriodID;
      set => this._AdjgTranPeriodID = value;
    }

    [PXDBDate]
    [PXDBDefault(typeof (APInvoice.docDate))]
    [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override System.DateTime? AdjdDocDate
    {
      get => this._AdjdDocDate;
      set => this._AdjdDocDate = value;
    }

    [FinPeriodID(null, typeof (APInvoiceEntry.APAdjust.adjdBranchID), null, null, null, null, true, false, null, typeof (APInvoiceEntry.APAdjust.adjdTranPeriodID), typeof (APInvoice.tranPeriodID), true, true)]
    [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
    public override string AdjdFinPeriodID
    {
      get => this._AdjdFinPeriodID;
      set => this._AdjdFinPeriodID = value;
    }

    [PeriodID(null, null, null, true)]
    public override string AdjdTranPeriodID
    {
      get => this._AdjdTranPeriodID;
      set => this._AdjdTranPeriodID = value;
    }

    [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceEntry.APAdjust.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust.adjDiscAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjdDiscAmt
    {
      get => this._CuryAdjdDiscAmt;
      set => this._CuryAdjdDiscAmt = value;
    }

    [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceEntry.APAdjust.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust.adjAmt), BaseCalc = false, MinValue = 0.0)]
    [PXUIField(DisplayName = "Amount Paid", Visibility = PXUIVisibility.Visible)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjdAmt { get; set; }

    [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceEntry.APAdjust.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust.adjWhTaxAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjdWhTaxAmt
    {
      get => this._CuryAdjdWhTaxAmt;
      set => this._CuryAdjdWhTaxAmt = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Amount")]
    public override Decimal? AdjAmt
    {
      get => this._AdjAmt;
      set => this._AdjAmt = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Cash Discount Amount")]
    public override Decimal? AdjDiscAmt
    {
      get => this._AdjDiscAmt;
      set => this._AdjDiscAmt = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Withholding Tax Amount")]
    public override Decimal? AdjWhTaxAmt
    {
      get => this._AdjWhTaxAmt;
      set => this._AdjWhTaxAmt = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjgDiscAmt
    {
      get => this._CuryAdjgDiscAmt;
      set => this._CuryAdjgDiscAmt = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjgAmt
    {
      get => this._CuryAdjgAmt;
      set => this._CuryAdjgAmt = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjgWhTaxAmt
    {
      get => this._CuryAdjgWhTaxAmt;
      set => this._CuryAdjgWhTaxAmt = value;
    }

    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Realized Gain/Loss Amount")]
    public override Decimal? RGOLAmt
    {
      get => this._RGOLAmt;
      set => this._RGOLAmt = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Released")]
    public override bool? Released
    {
      get => this._Released;
      set => this._Released = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    public override bool? Voided
    {
      get => this._Voided;
      set => this._Voided = value;
    }

    [PXDBInt]
    public override int? VoidAdjNbr
    {
      get => this._VoidAdjNbr;
      set => this._VoidAdjNbr = value;
    }

    [Account]
    [PXDBDefault(typeof (APInvoice.aPAccountID))]
    public override int? AdjdAPAcct
    {
      get => this._AdjdAPAcct;
      set => this._AdjdAPAcct = value;
    }

    [SubAccount]
    [PXDBDefault(typeof (APInvoice.aPSubID))]
    public override int? AdjdAPSub
    {
      get => this._AdjdAPSub;
      set => this._AdjdAPSub = value;
    }

    [Account]
    [PXDefault(typeof (Search2<APTaxTran.accountID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<APInvoiceEntry.APAdjust.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<APInvoiceEntry.APAdjust.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, PX.Data.OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public override int? AdjdWhTaxAcctID
    {
      get => this._AdjdWhTaxAcctID;
      set => this._AdjdWhTaxAcctID = value;
    }

    [SubAccount]
    [PXDefault(typeof (Search2<APTaxTran.subID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<APInvoiceEntry.APAdjust.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<APInvoiceEntry.APAdjust.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, PX.Data.OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public override int? AdjdWhTaxSubID
    {
      get => this._AdjdWhTaxSubID;
      set => this._AdjdWhTaxSubID = value;
    }

    [PXNote]
    public override Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.InvoiceID" />
    [PXDBGuid(false)]
    public override Guid? InvoiceID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.PaymentID" />
    [PXDBGuid(false)]
    public override Guid? PaymentID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.MemoID" />
    [PXDBGuid(false)]
    public override Guid? MemoID { get; set; }

    [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoiceEntry.APAdjust.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override Decimal? CuryDocBal
    {
      get => this._CuryDocBal;
      set => this._CuryDocBal = value;
    }

    [PXDecimal(4)]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? DocBal
    {
      get => this._DocBal;
      set => this._DocBal = value;
    }

    [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoiceEntry.APAdjust.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust.discBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override Decimal? CuryDiscBal
    {
      get => this._CuryDiscBal;
      set => this._CuryDiscBal = value;
    }

    [PXDecimal(4)]
    [PXUnboundDefault]
    public override Decimal? DiscBal
    {
      get => this._DiscBal;
      set => this._DiscBal = value;
    }

    [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoiceEntry.APAdjust.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust.whTaxBal), BaseCalc = false)]
    [PXUIField(DisplayName = "With. Tax Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override Decimal? CuryWhTaxBal
    {
      get => this._CuryWhTaxBal;
      set => this._CuryWhTaxBal = value;
    }

    [PXDecimal(4)]
    public override Decimal? WhTaxBal
    {
      get => this._WhTaxBal;
      set => this._WhTaxBal = value;
    }

    [PXBool]
    [PXUIField(DisplayName = "Void Application", Visibility = PXUIVisibility.Visible)]
    [PXDefault(false)]
    public override bool? VoidAppl
    {
      [PXDependsOnFields(new System.Type[] {typeof (APInvoiceEntry.APAdjust.adjgDocType)})] get
      {
        return new bool?(APPaymentType.VoidAppl(this.AdjgDocType));
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
    public override bool? ReverseGainLoss
    {
      [PXDependsOnFields(new System.Type[] {typeof (APInvoiceEntry.APAdjust.adjgDocType)})] get
      {
        return new bool?(APPaymentType.DrCr(this._AdjgDocType) == "D");
      }
      set
      {
      }
    }

    public new abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.vendorID>
    {
    }

    public new abstract class adjgDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjgDocType>
    {
    }

    public new abstract class adjgRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjgRefNbr>
    {
    }

    public new abstract class adjgBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjgBranchID>
    {
    }

    public new abstract class adjdCuryInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdCuryInfoID>
    {
    }

    public new abstract class adjdDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdDocType>
    {
    }

    public new abstract class adjdRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdRefNbr>
    {
    }

    public new abstract class adjdLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdLineNbr>
    {
    }

    public new abstract class adjdBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdBranchID>
    {
    }

    public new abstract class adjNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust.adjNbr>
    {
    }

    public new abstract class stubNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.stubNbr>
    {
    }

    public new abstract class adjBatchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjBatchNbr>
    {
    }

    public new abstract class adjdOrigCuryInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdOrigCuryInfoID>
    {
    }

    public new abstract class adjgCuryInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjgCuryInfoID>
    {
    }

    public new abstract class adjgDocDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjgDocDate>
    {
    }

    public new abstract class adjgFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjgFinPeriodID>
    {
    }

    public new abstract class adjgTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjgTranPeriodID>
    {
    }

    public new abstract class adjdDocDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdDocDate>
    {
    }

    public new abstract class adjdFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdFinPeriodID>
    {
    }

    public new abstract class adjdTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdTranPeriodID>
    {
    }

    public new abstract class curyAdjdDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyAdjdDiscAmt>
    {
    }

    public new abstract class curyAdjdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyAdjdAmt>
    {
    }

    public new abstract class curyAdjdWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyAdjdWhTaxAmt>
    {
    }

    public new abstract class adjAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjAmt>
    {
    }

    public new abstract class adjDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjDiscAmt>
    {
    }

    public new abstract class adjWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjWhTaxAmt>
    {
    }

    public new abstract class curyAdjgDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyAdjgDiscAmt>
    {
    }

    public new abstract class curyAdjgAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyAdjgAmt>
    {
    }

    public new abstract class curyAdjgWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyAdjgWhTaxAmt>
    {
    }

    public new abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.rGOLAmt>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.released>
    {
    }

    public new abstract class voided : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust.voided>
    {
    }

    public new abstract class voidAdjNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.voidAdjNbr>
    {
    }

    public new abstract class adjdAPAcct : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdAPAcct>
    {
    }

    public new abstract class adjdAPSub : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdAPSub>
    {
    }

    public new abstract class adjdWhTaxAcctID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdWhTaxAcctID>
    {
    }

    public new abstract class adjdWhTaxSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.adjdWhTaxSubID>
    {
    }

    public new abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust.noteID>
    {
    }

    public new abstract class invoiceID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.invoiceID>
    {
    }

    public new abstract class paymentID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.paymentID>
    {
    }

    public new abstract class memoID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust.memoID>
    {
    }

    public new abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyDocBal>
    {
    }

    public new abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.docBal>
    {
    }

    public new abstract class curyDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyDiscBal>
    {
    }

    public new abstract class discBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.discBal>
    {
    }

    public new abstract class curyWhTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.curyWhTaxBal>
    {
    }

    public new abstract class whTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.whTaxBal>
    {
    }

    public new abstract class voidAppl : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.voidAppl>
    {
    }

    public new abstract class reverseGainLoss : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust.reverseGainLoss>
    {
    }
  }

  /// <inheritdoc cref="T:PX.Objects.AP.APAdjust" />
  [PXCacheName("Adjust")]
  [Serializable]
  public class APAdjust2 : PX.Objects.AP.APAdjust
  {
    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.VendorID" />
    [PXDBInt]
    [PXDBDefault(typeof (APInvoice.vendorID))]
    [PXUIField(DisplayName = "Vendor ID", Visibility = PXUIVisibility.Visible, Visible = false)]
    public override int? VendorID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjgDocType" />
    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
    [APPaymentType.List]
    [PXDefault]
    [PXUIField(DisplayName = "Doc. Type", Enabled = false)]
    public override string AdjgDocType { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjgRefNbr" />
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDBDefault(typeof (APRegister.refNbr))]
    [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
    public override string AdjgRefNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjgBranchID" />
    [Branch(null, null, true, true, false)]
    public override int? AdjgBranchID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdCuryInfoID" />
    [PXDBLong]
    [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APInvoice.curyInfoID))]
    public override long? AdjdCuryInfoID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdDocType" />
    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
    [PXDBDefault(typeof (APInvoice.docType))]
    [PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.Invisible, Visible = false)]
    public override string AdjdDocType { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdRefNbr" />
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDBDefault(typeof (APInvoice.refNbr))]
    [PXParent(typeof (PX.Data.Select<APInvoice, Where<APInvoice.docType, Equal<Current<APInvoiceEntry.APAdjust2.adjdDocType>>, And<APInvoice.refNbr, Equal<Current<APInvoiceEntry.APAdjust2.adjdRefNbr>>>>>))]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Invisible, Visible = false)]
    public override string AdjdRefNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdLineNbr" />
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
    [PXDefault(typeof (Switch<Case<Where<Selector<APInvoiceEntry.APAdjust2.adjdRefNbr, PX.Objects.AP.APAdjust.APInvoice.paymentsByLinesAllowed>, NotEqual<True>>, PX.Objects.CS.int0>, Null>))]
    [APInvoiceType.AdjdLineNbr]
    public override int? AdjdLineNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdBranchID" />
    [Branch(typeof (APInvoice.branchID), null, true, true, true)]
    public override int? AdjdBranchID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjNbr" />
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Adjustment Nbr.", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
    [PXDefault]
    public override int? AdjNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.StubNbr" />
    [PXDBString(40, IsUnicode = true)]
    public override string StubNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjBatchNbr" />
    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, Visible = true, Enabled = false)]
    public override string AdjBatchNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdOrigCuryInfoID" />
    [PXDBLong]
    [PXDBDefault(typeof (APInvoice.curyInfoID))]
    public override long? AdjdOrigCuryInfoID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjgCuryInfoID" />
    [PXDBLong]
    public override long? AdjgCuryInfoID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjgDocDate" />
    [PXDBDate]
    [PXUIField(DisplayName = "Transaction Date")]
    public override System.DateTime? AdjgDocDate { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjgFinPeriodID" />
    [FinPeriodID(typeof (APInvoiceEntry.APAdjust2.adjgDocDate), typeof (APInvoiceEntry.APAdjust2.adjgBranchID), null, null, null, null, true, false, null, typeof (APInvoiceEntry.APAdjust2.adjgTranPeriodID), null, true, true)]
    [PXUIField(DisplayName = "Application Period")]
    public override string AdjgFinPeriodID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjgTranPeriodID" />
    [PeriodID(null, null, null, true)]
    public override string AdjgTranPeriodID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdDocDate" />
    [PXDBDate]
    [PXDBDefault(typeof (APInvoice.docDate))]
    [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override System.DateTime? AdjdDocDate { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdFinPeriodID" />
    [FinPeriodID(null, typeof (APInvoiceEntry.APAdjust2.adjdBranchID), null, null, null, null, true, false, null, typeof (APInvoiceEntry.APAdjust2.adjdTranPeriodID), typeof (APInvoice.tranPeriodID), true, true)]
    [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
    public override string AdjdFinPeriodID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdTranPeriodID" />
    [PeriodID(null, null, null, true)]
    public override string AdjdTranPeriodID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryAdjdDiscAmt" />
    [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceEntry.APAdjust2.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust2.adjDiscAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjdDiscAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryAdjdAmt" />
    [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceEntry.APAdjust2.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust2.adjAmt), BaseCalc = false, MinValue = 0.0)]
    [PXUIField(DisplayName = "Amount Paid", Visibility = PXUIVisibility.Visible)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjdAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryAdjdWhTaxAmt" />
    [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APInvoiceEntry.APAdjust2.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust2.adjWhTaxAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjdWhTaxAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjAmt" />
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Amount")]
    public override Decimal? AdjAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjDiscAmt" />
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Cash Discount Amount")]
    public override Decimal? AdjDiscAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjWhTaxAmt" />
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Withholding Tax Amount")]
    public override Decimal? AdjWhTaxAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryAdjgDiscAmt" />
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjgDiscAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryAdjgAmt" />
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjgAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryAdjgWhTaxAmt" />
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? CuryAdjgWhTaxAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.RGOLAmt" />
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Realized Gain/Loss Amount")]
    public override Decimal? RGOLAmt { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.Released" />
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Released")]
    public override bool? Released { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.Voided" />
    [PXDBBool]
    [PXDefault(false)]
    public override bool? Voided { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.VoidAdjNbr" />
    [PXDBInt]
    public override int? VoidAdjNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdAPAcct" />
    [Account]
    [PXDBDefault(typeof (APInvoice.aPAccountID))]
    public override int? AdjdAPAcct { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdAPSub" />
    [SubAccount]
    [PXDBDefault(typeof (APInvoice.aPSubID))]
    public override int? AdjdAPSub { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdWhTaxAcctID" />
    [Account]
    [PXDefault(typeof (Search2<APTaxTran.accountID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<APInvoiceEntry.APAdjust2.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<APInvoiceEntry.APAdjust2.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, PX.Data.OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public override int? AdjdWhTaxAcctID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdWhTaxSubID" />
    [SubAccount]
    [PXDefault(typeof (Search2<APTaxTran.subID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<APInvoiceEntry.APAdjust2.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<APInvoiceEntry.APAdjust2.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, PX.Data.OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public override int? AdjdWhTaxSubID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.NoteID" />
    [PXNote]
    public override Guid? NoteID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.InvoiceID" />
    [PXDBGuid(false)]
    public override Guid? InvoiceID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.PaymentID" />
    [PXDBGuid(false)]
    public override Guid? PaymentID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.MemoID" />
    [PXDBGuid(false)]
    public override Guid? MemoID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryDocBal" />
    [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoiceEntry.APAdjust2.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust2.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override Decimal? CuryDocBal { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.DocBal" />
    [PXDecimal(4)]
    [PXUnboundDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? DocBal { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryDiscBal" />
    [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoiceEntry.APAdjust2.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust2.discBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override Decimal? CuryDiscBal { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.DiscBal" />
    [PXDecimal(4)]
    [PXUnboundDefault]
    public override Decimal? DiscBal { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.CuryWhTaxBal" />
    [PX.Objects.CM.Extensions.PXCurrency(typeof (APInvoiceEntry.APAdjust2.adjdCuryInfoID), typeof (APInvoiceEntry.APAdjust2.whTaxBal), BaseCalc = false)]
    [PXUIField(DisplayName = "With. Tax Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override Decimal? CuryWhTaxBal { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.WhTaxBal" />
    [PXDecimal(4)]
    public override Decimal? WhTaxBal { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.VoidAppl" />
    [PXBool]
    [PXUIField(DisplayName = "Void Application", Visibility = PXUIVisibility.Visible)]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public override bool? VoidAppl
    {
      [PXDependsOnFields(new System.Type[] {typeof (APInvoiceEntry.APAdjust2.adjgDocType)})] get
      {
        return new bool?(APPaymentType.VoidAppl(this.AdjgDocType));
      }
      set
      {
        if (!value.Value)
          return;
        this._AdjgDocType = APPaymentType.GetVoidingAPDocType(this.AdjgDocType) ?? "VCK";
        this.Voided = new bool?(true);
      }
    }

    /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.ReverseGainLoss" />
    [PXBool]
    public override bool? ReverseGainLoss
    {
      [PXDependsOnFields(new System.Type[] {typeof (APInvoiceEntry.APAdjust2.adjgDocType)})] get
      {
        return new bool?(APPaymentType.DrCr(this._AdjgDocType) == "D");
      }
      set
      {
      }
    }

    public new abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.vendorID>
    {
    }

    public new abstract class adjgDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjgDocType>
    {
    }

    public new abstract class adjgRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjgRefNbr>
    {
    }

    public new abstract class adjgBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjgBranchID>
    {
    }

    public new abstract class adjdCuryInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdCuryInfoID>
    {
    }

    public new abstract class adjdDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdDocType>
    {
    }

    public new abstract class adjdRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdRefNbr>
    {
    }

    public new abstract class adjdLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdLineNbr>
    {
    }

    public new abstract class adjdBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdBranchID>
    {
    }

    public new abstract class adjNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust2.adjNbr>
    {
    }

    public new abstract class stubNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.stubNbr>
    {
    }

    public new abstract class adjBatchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjBatchNbr>
    {
    }

    public new abstract class adjdOrigCuryInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdOrigCuryInfoID>
    {
    }

    public new abstract class adjgCuryInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjgCuryInfoID>
    {
    }

    public new abstract class adjgDocDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjgDocDate>
    {
    }

    public new abstract class adjgFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjgFinPeriodID>
    {
    }

    public new abstract class adjgTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjgTranPeriodID>
    {
    }

    public new abstract class adjdDocDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdDocDate>
    {
    }

    public new abstract class adjdFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdFinPeriodID>
    {
    }

    public new abstract class adjdTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdTranPeriodID>
    {
    }

    public new abstract class curyAdjdDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyAdjdDiscAmt>
    {
    }

    public new abstract class curyAdjdAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyAdjdAmt>
    {
    }

    public new abstract class curyAdjdWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyAdjdWhTaxAmt>
    {
    }

    public new abstract class adjAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjAmt>
    {
    }

    public new abstract class adjDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjDiscAmt>
    {
    }

    public new abstract class adjWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjWhTaxAmt>
    {
    }

    public new abstract class curyAdjgDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyAdjgDiscAmt>
    {
    }

    public new abstract class curyAdjgAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyAdjgAmt>
    {
    }

    public new abstract class curyAdjgWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyAdjgWhTaxAmt>
    {
    }

    public new abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.rGOLAmt>
    {
    }

    public new abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.released>
    {
    }

    public new abstract class voided : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust2.voided>
    {
    }

    public new abstract class voidAdjNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.voidAdjNbr>
    {
    }

    public new abstract class adjdAPAcct : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdAPAcct>
    {
    }

    public new abstract class adjdAPSub : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdAPSub>
    {
    }

    public new abstract class adjdWhTaxAcctID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdWhTaxAcctID>
    {
    }

    public new abstract class adjdWhTaxSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.adjdWhTaxSubID>
    {
    }

    public new abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust2.noteID>
    {
    }

    public new abstract class invoiceID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.invoiceID>
    {
    }

    public new abstract class paymentID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.paymentID>
    {
    }

    public new abstract class memoID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APInvoiceEntry.APAdjust2.memoID>
    {
    }

    public new abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyDocBal>
    {
    }

    public new abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.docBal>
    {
    }

    public new abstract class curyDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyDiscBal>
    {
    }

    public new abstract class discBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.discBal>
    {
    }

    public new abstract class curyWhTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.curyWhTaxBal>
    {
    }

    public new abstract class whTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.whTaxBal>
    {
    }

    public new abstract class voidAppl : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.voidAppl>
    {
    }

    public new abstract class reverseGainLoss : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APInvoiceEntry.APAdjust2.reverseGainLoss>
    {
    }
  }
}
