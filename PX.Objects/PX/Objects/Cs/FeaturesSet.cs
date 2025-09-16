// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FeaturesSet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.EntityInUse;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.GL.DAC;
using PX.Objects.GL.DAC.Standalone;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.PR.Standalone;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Features Set")]
[PXPrimaryGraph(typeof (FeaturesMaint))]
[Serializable]
public class FeaturesSet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _LicenseID;
  protected DateTime? _ValidUntill;
  protected string _ValidationCode;
  protected bool? _FinancialModule;
  protected bool? _FinancialStandard;
  protected bool? _Branch;
  protected bool? _AccountLocations;
  protected bool? _Multicurrency;
  protected bool? _SupportBreakQty;
  protected bool? _Prebooking;
  protected bool? _TaxEntryFromGL;
  protected bool? _VATReporting;
  protected bool? _Reporting1099;
  protected bool? _NetGrossEntryMode;
  protected bool? _InvoiceRounding;
  protected bool? _FinancialAdvanced;
  protected bool? _SubAccount;
  protected bool? _AllocationTemplates;
  protected bool? _GLConsolidation;
  protected bool? _FinStatementCurTranslation;
  protected bool? _CustomerDiscounts;
  protected bool? _VendorDiscounts;
  protected bool? _Commissions;
  protected bool? _OverdueFinCharges;
  protected bool? _DunningLetter;
  protected bool? _DefferedRevenue;
  protected bool? _ConsolidatedPosting;
  protected bool? _ParentChildAccount;
  protected bool? _ContractManagement;
  protected bool? _FixedAsset;
  protected bool? _DistributionModule;
  protected bool? _Inventory;
  protected bool? _MultipleUnitMeasure;
  protected bool? _LotSerialTracking;
  protected bool? _BlanketPO;
  protected bool? _POReceiptsWithoutInventory;
  protected bool? _DropShipments;
  protected bool? _Warehouse;
  protected bool? _WarehouseLocation;
  protected bool? _Replenishment;
  protected bool? _SubItem;
  protected bool? _AutoPackaging;
  protected bool? _KitAssemblies;
  protected bool? _AdvancedPhysicalCounts;
  protected bool? _SOToPOLink;
  protected bool? _UserDefinedOrderTypes;
  protected bool? _PurchaseRequisitions;
  protected bool? _CrossReferenceUniqueness;
  protected bool? _OrganizationModule;
  protected bool? _CustomerModule;
  protected bool? _CaseManagement;
  protected bool? _ContactDuplicate;
  protected bool? _SalesQuotes;
  protected bool? _PlatformModule;
  protected bool? _MiscModule;
  protected bool? _TimeReportingModule;
  protected bool? _ApprovalWorkflow;
  protected bool? _FieldLevelLogging;
  protected bool? _RowLevelSecurity;
  protected bool? _ScheduleModule;
  protected bool? _NotificationModule;
  protected bool? _DeviceHub;
  protected bool? _IntegrationModule;
  protected bool? _CarrierIntegration;
  protected bool? _ExchangeIntegration;
  protected bool? _AvalaraTax;
  protected bool? _AddressValidation;
  protected bool? _SalesforceIntegration;
  protected bool? _HubSpotIntegration;
  protected bool? _ImageRecognition;
  protected bool? _RouteOptimizer;

  [PXString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "License ID", Visible = false)]
  public virtual string LicenseID
  {
    get => this._LicenseID;
    set => this._LicenseID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(3)]
  [PXIntList(new int[] {0, 1, 2, 3}, new string[] {"Validated", "Failed Validation", "Pending Validation", "Pending Activation"})]
  [PXUIField(DisplayName = "Activation Status", Enabled = false)]
  public int? Status { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Next Validation Date", Enabled = false, Visible = false)]
  public virtual DateTime? ValidUntill
  {
    get => this._ValidUntill;
    set => this._ValidUntill = value;
  }

  [PXString(500, IsUnicode = true, InputMask = "")]
  public virtual string ValidationCode
  {
    get => this._ValidationCode;
    set => this._ValidationCode = value;
  }

  [Feature(true, null, typeof (Select<GLSetup>), DisplayName = "Finance", Enabled = false)]
  public virtual bool? FinancialModule
  {
    get => this._FinancialModule;
    set => this._FinancialModule = value;
  }

  [Feature(true, typeof (FeaturesSet.financialModule), null, DisplayName = "Standard Financials", SyncToParent = true)]
  public virtual bool? FinancialStandard
  {
    get => this._FinancialStandard;
    set => this._FinancialStandard = value;
  }

  [FeatureRestrictor(typeof (Select<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationType, Equal<OrganizationTypes.withBranchesBalancing>, Or<PX.Objects.GL.DAC.Organization.organizationType, Equal<OrganizationTypes.withBranchesNotBalancing>>>>))]
  [Feature(typeof (FeaturesSet.financialStandard), DisplayName = "Multibranch Support")]
  public virtual bool? Branch
  {
    get => this._Branch;
    set => this._Branch = value;
  }

  [FeatureRestrictor(typeof (SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Empty>.Where<NotExists<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.And<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<AccessInfo.branchID, IBqlInt>.FromCurrent>>>>>))]
  [Feature(false, typeof (FeaturesSet.financialStandard), Visible = true, DisplayName = "Multicompany Support")]
  public virtual bool? MultiCompany { get; set; }

  [Feature(false, typeof (FeaturesSet.financialStandard), DisplayName = "Business Account Locations")]
  public virtual bool? AccountLocations
  {
    get => this._AccountLocations;
    set => this._AccountLocations = value;
  }

  [Feature(typeof (FeaturesSet.financialStandard), typeof (Select<CMSetup>), DisplayName = "Multicurrency Accounting")]
  public virtual bool? Multicurrency
  {
    get => this._Multicurrency;
    set => this._Multicurrency = value;
  }

  [Feature(true, typeof (FeaturesSet.financialStandard), DisplayName = "Centralized Period Management")]
  [PXFormula(typeof (IIf<Where<FeaturesSet.multiCompany, NotEqual<True>>, True, FeaturesSet.centralizedPeriodsManagement>))]
  public virtual bool? CentralizedPeriodsManagement { get; set; }

  [Feature(false, typeof (FeaturesSet.financialStandard), DisplayName = "Volume Pricing")]
  public virtual bool? SupportBreakQty
  {
    get => this._SupportBreakQty;
    set => this._SupportBreakQty = value;
  }

  [Feature(typeof (FeaturesSet.financialStandard), typeof (Select<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.prebookBatchNbr, IsNotNull>>), DisplayName = "Expense Reclassification")]
  public virtual bool? Prebooking { get; set; }

  [Feature(false, typeof (FeaturesSet.financialStandard), typeof (Select<FeatureInUse, Where<FeatureInUse.featureName, Equal<FeaturesSet.taxEntryFromGL.entityInUseKey>>>), DisplayName = "Tax Entry from GL Module")]
  public virtual bool? TaxEntryFromGL
  {
    get => this._TaxEntryFromGL;
    set => this._TaxEntryFromGL = value;
  }

  [Feature(typeof (FeaturesSet.financialStandard), typeof (Select<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.vat>>>), DisplayName = "VAT Reporting")]
  public virtual bool? VATReporting
  {
    get => this._VATReporting;
    set => this._VATReporting = value;
  }

  /// <summary>
  /// Represents the feature switch that enables support for report VAT taxes as soon as the prepayment from client is received.
  /// </summary>
  [Feature(typeof (FeaturesSet.vATReporting), typeof (Select<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<APDocType.prepaymentInvoice>>>), DisplayName = "VAT Recognition on AP Prepayments", Visible = false)]
  public virtual bool? VATRecognitionOnPrepaymentsAP { get; set; }

  /// <summary>
  /// Represents the feature switch that enables support for report VAT taxes as soon as the prepayment from client is received.
  /// </summary>
  [Feature(typeof (FeaturesSet.vATReporting), typeof (Select<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<ARDocType.prepaymentInvoice>>>), DisplayName = "VAT Recognition on AR Prepayments", Visible = true)]
  public virtual bool? VATRecognitionOnPrepaymentsAR { get; set; }

  [Feature(typeof (FeaturesSet.financialStandard), DisplayName = "1099 Reporting")]
  public virtual bool? Reporting1099
  {
    get => this._Reporting1099;
    set => this._Reporting1099 = value;
  }

  [Feature(typeof (FeaturesSet.financialStandard), DisplayName = "Net/Gross Entry Mode")]
  public virtual bool? NetGrossEntryMode
  {
    get => this._NetGrossEntryMode;
    set => this._NetGrossEntryMode = value;
  }

  [Feature(typeof (FeaturesSet.financialStandard), DisplayName = "Invoice Rounding")]
  public virtual bool? InvoiceRounding { get; set; }

  [Feature(false, typeof (FeaturesSet.financialStandard), DisplayName = "Expense Management")]
  [FeatureRestrictor(typeof (Select<EPExpenseClaimDetails>))]
  [FeatureRestrictor(typeof (Select<EPExpenseClaim>))]
  public virtual bool? ExpenseManagement { get; set; }

  [Feature(typeof (FeaturesSet.financialModule), null, DisplayName = "Advanced Financials")]
  public virtual bool? FinancialAdvanced
  {
    get => this._FinancialAdvanced;
    set => this._FinancialAdvanced = value;
  }

  [Feature(typeof (FeaturesSet.financialAdvanced), typeof (Select<PX.Objects.GL.Sub, Where<PX.Objects.GL.Sub.subCD, NotEqual<INSubItem.Zero>>>), DisplayName = "Subaccounts")]
  public virtual bool? SubAccount
  {
    get => this._SubAccount;
    set => this._SubAccount = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "General Ledger Allocation Templates")]
  public virtual bool? AllocationTemplates
  {
    get => this._AllocationTemplates;
    set => this._AllocationTemplates = value;
  }

  [FeatureRestrictor(typeof (Select2<BranchAcctMap, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<BranchAcctMap.branchID>, And<PX.Objects.GL.Branch.active, Equal<True>>>>>))]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.branch), typeof (FeaturesSet.multiCompany)})]
  [Feature(typeof (FeaturesSet.financialAdvanced), null, DisplayName = "Inter-Branch Transactions")]
  public virtual bool? InterBranch { get; set; }

  [Feature(false, null, DisplayName = "Projects")]
  public virtual bool? ProjectAccounting { get; set; }

  [Feature(typeof (FeaturesSet.projectAccounting), typeof (Select<PMForecast>), DisplayName = "Budget Forecast")]
  public virtual bool? BudgetForecast { get; set; }

  [Feature(typeof (FeaturesSet.projectAccounting), typeof (Select<PMChangeOrder>), DisplayName = "Change Orders")]
  public virtual bool? ChangeOrder { get; set; }

  [Feature(typeof (FeaturesSet.changeOrder), typeof (Select<PMChangeRequest>), DisplayName = "Change Requests")]
  public virtual bool? ChangeRequest { get; set; }

  [FeatureRestrictor(typeof (Select<PMProformaLine, Where<PMProformaLine.merged, Equal<True>>>))]
  [Feature(typeof (FeaturesSet.projectAccounting), DisplayName = "Construction")]
  public virtual bool? Construction { get; set; }

  [Feature(typeof (FeaturesSet.construction), DisplayName = "Construction Project Management")]
  public virtual bool? ConstructionProjectManagement { get; set; }

  /// <summary>
  /// Represents a feature switch that enables/disables project 360 dashboard.
  /// </summary>
  [Feature(typeof (FeaturesSet.construction), DisplayName = "360 Dashboards")]
  public virtual bool? ProjectOverview { get; set; }

  /// <summary>
  /// Represents a feature switch that enables/disables weather service integration for daily field reports.
  /// </summary>
  [Feature(typeof (FeaturesSet.constructionProjectManagement), DisplayName = "Weather Services")]
  public virtual bool? WeatherServices { get; set; }

  [Feature(typeof (FeaturesSet.projectAccounting), typeof (Select2<PMBudget, InnerJoin<PMCostCode, On<PMBudget.costCodeID, Equal<PMCostCode.costCodeID>>>, Where<PMCostCode.isDefault, Equal<False>>>), DisplayName = "Cost Codes")]
  public virtual bool? CostCodes { get; set; }

  [FeatureRestrictor(typeof (Select2<PMProject, InnerJoin<Company, On<Not<Company.baseCuryID, Equal<PMProject.curyID>>>>>))]
  [FeatureRestrictor(typeof (Select2<PMTran, InnerJoin<Company, On<Not<Company.baseCuryID, Equal<PMTran.tranCuryID>>>>>))]
  [Feature(typeof (FeaturesSet.projectAccounting), DisplayName = "Multicurrency Projects")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.multicurrency)})]
  public virtual bool? ProjectMultiCurrency { get; set; }

  /// <summary>
  /// Represents a feature switch that enables/disables proffessional services edition.
  /// </summary>
  [Feature(typeof (FeaturesSet.projectAccounting), DisplayName = "Professional Services")]
  public virtual bool? ProfessionalServices { get; set; }

  [Feature(typeof (FeaturesSet.projectAccounting), DisplayName = "Project Quotes")]
  public virtual bool? ProjectQuotes { get; set; }

  [Feature(typeof (FeaturesSet.projectAccounting), DisplayName = "Project-Specific Inventory")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? MaterialManagement { get; set; }

  /// <summary>
  /// Represents a feature switch that enables/disables document management for projects.
  /// </summary>
  [Feature(typeof (FeaturesSet.projectAccounting), DisplayName = "Document Management")]
  public virtual bool? FileManagement { get; set; }

  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.branch), typeof (FeaturesSet.multiCompany)})]
  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Customer and Vendor Visibility Restriction")]
  public virtual bool? VisibilityRestriction { get; set; }

  [FeatureRestrictor(typeof (Select2<PX.Objects.GL.DAC.Organization, InnerJoin<OrganizationAlias, On<PX.Objects.GL.DAC.Organization.baseCuryID, NotEqual<OrganizationAlias.baseCuryID>>>>))]
  [FeatureDependency(true, new System.Type[] {typeof (FeaturesSet.multiCompany), typeof (FeaturesSet.multicurrency), typeof (FeaturesSet.visibilityRestriction)})]
  [FeatureMutuallyExclusiveDependency(true, new System.Type[] {typeof (FeaturesSet.modernPortalModule), typeof (FeaturesSet.procoreIntegration), typeof (FeaturesSet.serviceManagementModule), typeof (FeaturesSet.payrollModule), typeof (FeaturesSet.purchaseRequisitions), typeof (FeaturesSet.lotSerialAttributes)})]
  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Multiple Base Currencies")]
  public virtual bool? MultipleBaseCurrencies { get; set; }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Multiple Calendar Support")]
  public virtual bool? MultipleCalendarsSupport { get; set; }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "General Ledger Consolidation")]
  public virtual bool? GLConsolidation
  {
    get => this._GLConsolidation;
    set => this._GLConsolidation = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Translation of Financial Statements")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.multicurrency)})]
  public virtual bool? FinStatementCurTranslation
  {
    get => this._FinStatementCurTranslation;
    set => this._FinStatementCurTranslation = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Customer Discounts")]
  public virtual bool? CustomerDiscounts
  {
    get => this._CustomerDiscounts;
    set => this._CustomerDiscounts = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Vendor Discounts")]
  public virtual bool? VendorDiscounts
  {
    get => this._VendorDiscounts;
    set => this._VendorDiscounts = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Commissions")]
  public virtual bool? Commissions
  {
    get => this._Commissions;
    set => this._Commissions = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Overdue Charges")]
  public virtual bool? OverdueFinCharges
  {
    get => this._OverdueFinCharges;
    set => this._OverdueFinCharges = value;
  }

  [Feature(typeof (FeaturesSet.financialAdvanced), typeof (Select<ARDunningSetup>), DisplayName = "Dunning Letter Management")]
  public virtual bool? DunningLetter
  {
    get => this._DunningLetter;
    set => this._DunningLetter = value;
  }

  [Feature(typeof (FeaturesSet.financialAdvanced), typeof (Select<DRSchedule>), DisplayName = "Deferred Revenue Management")]
  public virtual bool? DefferedRevenue
  {
    get => this._DefferedRevenue;
    set => this._DefferedRevenue = value;
  }

  [Feature(false, typeof (FeaturesSet.defferedRevenue), DisplayName = "Revenue Recognition by IFRS 15/ASC 606")]
  public virtual bool? ASC606 { get; set; }

  [Obsolete("ConsolidatedPosting setting was moved to GLSetup. FeatureSet.ConsolidatedPosting will be fully eliminated in the future version.")]
  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Consolidated Posting to GL", Visible = false)]
  public virtual bool? ConsolidatedPosting
  {
    get => this._ConsolidatedPosting;
    set => this._ConsolidatedPosting = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.consolidatingBAccountID, NotEqual<PX.Objects.AR.Customer.bAccountID>, Or<PX.Objects.AR.Customer.statementCustomerID, NotEqual<PX.Objects.AR.Customer.bAccountID>, Or<PX.Objects.AR.Customer.sharedCreditCustomerID, NotEqual<PX.Objects.AR.Customer.bAccountID>>>>>), DisplayName = "Parent-Child Customer Relationship")]
  public virtual bool? ParentChildAccount
  {
    get => this._ParentChildAccount;
    set => this._ParentChildAccount = value;
  }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Retainage Support")]
  [FeatureRestrictor(typeof (Select<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.isRetainageDocument, Equal<True>, Or<PX.Objects.AP.APRegister.retainageApply, Equal<True>>>>))]
  [FeatureRestrictor(typeof (Select<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.isRetainageDocument, Equal<True>, Or<PX.Objects.AR.ARRegister.retainageApply, Equal<True>>>>))]
  [FeatureRestrictor(typeof (Select<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.retainageApply, Equal<True>>>))]
  [FeatureRestrictor(typeof (Select<PMProject, Where<PMProject.retainagePct, Greater<decimal0>>>))]
  [FeatureRestrictor(typeof (Select<PMBudget, Where<PMBudget.retainagePct, Greater<decimal0>>>))]
  [FeatureRestrictor(typeof (Select<PMProforma, Where<PMProforma.curyRetainageDetailTotal, Greater<decimal0>, Or<PMProforma.curyRetainageTaxTotal, Greater<decimal0>>>>))]
  public virtual bool? Retainage { get; set; }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), typeof (Select<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.perUnit>>>), DisplayName = "Per Unit/Specific Tax Support", Visible = false)]
  public virtual bool? PerUnitTaxSupport { get; set; }

  [FeatureRestrictor(typeof (Select<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.paymentsByLinesAllowed, Equal<True>>>))]
  [FeatureRestrictor(typeof (Select<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.paymentsByLinesAllowed, Equal<True>>>))]
  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Payment Application by Line", Visible = true)]
  public virtual bool? PaymentsByLines { get; set; }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Exempted Tax Reporting", Visible = false)]
  public virtual bool? ExemptedTaxReporting { get; set; }

  [Feature(false, typeof (FeaturesSet.financialAdvanced), DisplayName = "Bank Transaction Splits", Visible = false)]
  public virtual bool? BankTransactionSplits { get; set; }

  [Feature(typeof (FeaturesSet.financialModule), typeof (Select<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.nonProject, Equal<False>, And<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>>>>), DisplayName = "Contract Management")]
  public virtual bool? ContractManagement { get; set; }

  [Feature(typeof (FeaturesSet.financialModule), typeof (Select<FASetup>), DisplayName = "Fixed Asset Management")]
  public virtual bool? FixedAsset
  {
    get => this._FixedAsset;
    set => this._FixedAsset = value;
  }

  [Feature(typeof (FeaturesSet.financialModule), typeof (Select<INSetup>), Top = true, DisplayName = "Inventory and Order Management")]
  public virtual bool? DistributionModule
  {
    get => this._DistributionModule;
    set => this._DistributionModule = value;
  }

  [Feature(typeof (FeaturesSet.distributionModule), null, DisplayName = "Inventory", SyncToParent = false)]
  [FeatureMutuallyExclusiveDependency(new System.Type[] {typeof (FeaturesSet.pOReceiptsWithoutInventory)})]
  public virtual bool? Inventory
  {
    get => this._Inventory;
    set => this._Inventory = value;
  }

  [FeatureRestrictor(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.baseUnit, NotEqual<PX.Objects.IN.InventoryItem.salesUnit>, Or<PX.Objects.IN.InventoryItem.baseUnit, NotEqual<PX.Objects.IN.InventoryItem.purchaseUnit>, Or<PX.Objects.IN.InventoryItem.decimalSalesUnit, NotEqual<PX.Objects.IN.InventoryItem.decimalBaseUnit>, Or<PX.Objects.IN.InventoryItem.decimalPurchaseUnit, NotEqual<PX.Objects.IN.InventoryItem.decimalBaseUnit>>>>>>))]
  [FeatureRestrictor(typeof (Select<INItemClass, Where<INItemClass.baseUnit, NotEqual<INItemClass.salesUnit>, Or<INItemClass.baseUnit, NotEqual<INItemClass.purchaseUnit>, Or<INItemClass.decimalSalesUnit, NotEqual<INItemClass.decimalBaseUnit>, Or<INItemClass.decimalPurchaseUnit, NotEqual<INItemClass.decimalBaseUnit>>>>>>))]
  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Multiple Units of Measure")]
  public virtual bool? MultipleUnitMeasure
  {
    get => this._MultipleUnitMeasure;
    set => this._MultipleUnitMeasure = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Lot and Serial Tracking")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? LotSerialTracking
  {
    get => this._LotSerialTracking;
    set => this._LotSerialTracking = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), typeof (Select<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.blanket>, Or<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.standardBlanket>>>>), DisplayName = "Blanket and Standard Purchase Orders")]
  public virtual bool? BlanketPO
  {
    get => this._BlanketPO;
    set => this._BlanketPO = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), null, DisplayName = "Purchase Receipts Without Inventory")]
  [FeatureMutuallyExclusiveDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? POReceiptsWithoutInventory
  {
    get => this._POReceiptsWithoutInventory;
    set => this._POReceiptsWithoutInventory = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), typeof (Select<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.dropShip>>>), DisplayName = "Drop Shipments")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? DropShipments
  {
    get => this._DropShipments;
    set => this._DropShipments = value;
  }

  [FeatureRestrictor(typeof (Select<INItemSite, Where<INItemSite.replenishmentSourceSiteID, IsNotNull>>))]
  [FeatureRestrictor(typeof (Select<INItemRep, Where<INItemRep.replenishmentSourceSiteID, IsNotNull>>))]
  [FeatureRestrictor(typeof (Select<INItemClassRep, Where<INItemClassRep.replenishmentSourceSiteID, IsNotNull>>))]
  [Feature(false, typeof (FeaturesSet.distributionModule), typeof (Select<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.siteCD, NotEqual<PX.Objects.IN.INSite.main>, And<Where<SiteAnyAttribute.transitSiteID, IsNull, Or<PX.Objects.IN.INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>>), DisplayName = "Multiple Warehouses")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? Warehouse
  {
    get => this._Warehouse;
    set => this._Warehouse = value;
  }

  /// <summary>
  /// Represents a switch that indicates whether the Order Orchestration features are enabled.
  /// </summary>
  [Feature(false, typeof (FeaturesSet.warehouse), DisplayName = "Order Orchestration")]
  [FeatureDependency(true, new System.Type[] {typeof (FeaturesSet.interBranch)})]
  public virtual bool? OrderOrchestration { get; set; }

  /// <summary>
  /// Distribution requirement planning (Non manufacturing version of the material requirement planning)
  /// </summary>
  [PXDefault(false)]
  [PXUIEnabled(typeof (BqlOperand<FeaturesSet.manufacturing, IBqlBool>.IsEqual<False>))]
  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Distribution Requirements Planning")]
  public virtual bool? DistributionReqPlan { get; set; }

  [Feature(false, typeof (FeaturesSet.distributionModule), typeof (Select<INLocation, Where<INLocation.locationCD, NotEqual<INLocation.main>>>), DisplayName = "Multiple Warehouse Locations")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? WarehouseLocation
  {
    get => this._WarehouseLocation;
    set => this._WarehouseLocation = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Inventory Replenishment")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.warehouse), typeof (FeaturesSet.warehouseLocation)})]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? Replenishment
  {
    get => this._Replenishment;
    set => this._Replenishment = value;
  }

  [Feature(typeof (FeaturesSet.distributionModule), typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.isTemplate, Equal<True>>>), DisplayName = "Matrix Items")]
  public virtual bool? MatrixItem { get; set; }

  [Feature(typeof (FeaturesSet.distributionModule), typeof (Select<INSubItem, Where<INSubItem.subItemCD, NotEqual<INSubItem.Zero>>>), DisplayName = "Inventory Subitems", Visible = false)]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? SubItem
  {
    get => this._SubItem;
    set => this._SubItem = value;
  }

  [Feature(typeof (FeaturesSet.distributionModule), DisplayName = "Automatic Packaging")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? AutoPackaging
  {
    get => this._AutoPackaging;
    set => this._AutoPackaging = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.kitItem, Equal<True>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>>), DisplayName = "Kit Assembly")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? KitAssemblies
  {
    get => this._KitAssemblies;
    set => this._KitAssemblies = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Related Items")]
  public virtual bool? RelatedItems { get; set; }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Advanced Physical Count")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? AdvancedPhysicalCounts
  {
    get => this._AdvancedPhysicalCounts;
    set => this._AdvancedPhysicalCounts = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Sales Order to Purchase Order Link")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? SOToPOLink
  {
    get => this._SOToPOLink;
    set => this._SOToPOLink = value;
  }

  [Feature(false, typeof (FeaturesSet.sOToPOLink), DisplayName = "Special Orders", Visible = false)]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? SpecialOrders { get; set; }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Custom Order Types")]
  public virtual bool? UserDefinedOrderTypes
  {
    get => this._UserDefinedOrderTypes;
    set => this._UserDefinedOrderTypes = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Purchase Requisitions")]
  [FeatureMutuallyExclusiveDependency(true, new System.Type[] {typeof (FeaturesSet.multipleBaseCurrencies)})]
  public virtual bool? PurchaseRequisitions
  {
    get => this._PurchaseRequisitions;
    set => this._PurchaseRequisitions = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Advanced SO Invoices")]
  public virtual bool? AdvancedSOInvoices { get; set; }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Cross-Reference Uniqueness", Visible = false)]
  public virtual bool? CrossReferenceUniqueness
  {
    get => this._CrossReferenceUniqueness;
    set => this._CrossReferenceUniqueness = value;
  }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Vendor Relations")]
  public virtual bool? VendorRelations { get; set; }

  [Feature(false, typeof (FeaturesSet.distributionModule), DisplayName = "Warehouse Management")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.inventory)})]
  public virtual bool? AdvancedFulfillment { get; set; }

  [Feature(false, typeof (FeaturesSet.advancedFulfillment), DisplayName = "Fulfillment")]
  public virtual bool? WMSFulfillment { get; set; }

  [Feature(false, typeof (FeaturesSet.wMSFulfillment), typeof (Select<SOPickingJob, Where<BqlOperand<SOPickingJob.status, IBqlString>.IsNotIn<SOPickingJob.status.completed, SOPickingJob.status.cancelled>>>), DisplayName = "Paperless Picking")]
  public virtual bool? WMSPaperlessPicking { get; set; }

  [Feature(false, typeof (FeaturesSet.wMSFulfillment), typeof (Select<SOPickingWorksheet, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.status, NotEqual<SOPickingWorksheet.status.completed>>>>>.And<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsIn<SOPickingWorksheet.worksheetType.wave, SOPickingWorksheet.worksheetType.batch>>>>), DisplayName = "Advanced Picking")]
  public virtual bool? WMSAdvancedPicking { get; set; }

  [Feature(false, typeof (FeaturesSet.advancedFulfillment), DisplayName = "Receiving")]
  public virtual bool? WMSReceiving { get; set; }

  [Feature(false, typeof (FeaturesSet.advancedFulfillment), DisplayName = "Inventory Operations")]
  public virtual bool? WMSInventory { get; set; }

  [Feature(false, typeof (FeaturesSet.advancedFulfillment), typeof (Select<INCartSplit>), DisplayName = "Cart Tracking")]
  public virtual bool? WMSCartTracking { get; set; }

  [Feature(true, DisplayName = "Organization", Enabled = false, Visible = false)]
  public virtual bool? OrganizationModule
  {
    get => this._OrganizationModule;
    set => this._OrganizationModule = value;
  }

  [Feature(false, null, typeof (Select<CRSetup>), DisplayName = "Customer Management")]
  public virtual bool? CustomerModule
  {
    get => this._CustomerModule;
    set => this._CustomerModule = value;
  }

  [Feature(typeof (FeaturesSet.customerModule), DisplayName = "Case Management")]
  public virtual bool? CaseManagement
  {
    get => this._CaseManagement;
    set => this._CaseManagement = value;
  }

  [Feature(typeof (FeaturesSet.customerModule), DisplayName = "Duplicate Validation")]
  public virtual bool? ContactDuplicate
  {
    get => this._ContactDuplicate;
    set => this._ContactDuplicate = value;
  }

  [Feature(typeof (FeaturesSet.integrationModule), DisplayName = "SendGrid Integration")]
  public virtual bool? SendGridIntegration { get; set; }

  [Feature(typeof (FeaturesSet.customerModule), DisplayName = "Sales Quotes")]
  public virtual bool? SalesQuotes
  {
    get => this._SalesQuotes;
    set => this._SalesQuotes = value;
  }

  [Feature(typeof (FeaturesSet.customerModule), DisplayName = "Address Lookup Integration")]
  public virtual bool? AddressLookup { get; set; }

  [Obsolete("ProjectModule setting was moved to ProjectAccounting")]
  [Feature(false, typeof (FeaturesSet.projectAccounting), typeof (Select<PMProject, Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>>), DisplayName = "Project Accounting", SyncToParent = true, Visible = false)]
  public virtual bool? ProjectModule { get; set; }

  /// <summary>Customer Portal (Old portal)</summary>
  [Feature(false, DisplayName = "Customer Portal")]
  public virtual bool? PortalModule { get; set; }

  /// <summary>Portal B2B Ordering (Old portal)</summary>
  [Feature(false, typeof (FeaturesSet.portalModule), DisplayName = "B2B Ordering")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.distributionModule)})]
  public virtual bool? B2BOrdering { get; set; }

  /// <summary>Portal Case Management (Old portal)</summary>
  [Feature(false, typeof (FeaturesSet.portalModule), DisplayName = "Case Management on Portal")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.caseManagement)})]
  public virtual bool? PortalCaseManagement { get; set; }

  /// <summary>Portal Financials (Old portal)</summary>
  [Feature(false, typeof (FeaturesSet.portalModule), DisplayName = "Financials on Portal")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.financialAdvanced)})]
  public virtual bool? PortalFinancials { get; set; }

  [Feature(false, null, null, DisplayName = "Service Management")]
  [FeatureMutuallyExclusiveDependency(true, new System.Type[] {typeof (FeaturesSet.multipleBaseCurrencies)})]
  public virtual bool? ServiceManagementModule { get; set; }

  [Feature(false, typeof (FeaturesSet.serviceManagementModule), null, DisplayName = "Equipment Management")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.serviceManagementModule)})]
  public virtual bool? EquipmentManagementModule { get; set; }

  [Feature(false, typeof (FeaturesSet.serviceManagementModule), null, DisplayName = "Route Management")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.serviceManagementModule)})]
  public virtual bool? RouteManagementModule { get; set; }

  /// <summary>
  /// Represents the switch that enables/disables the group of advanced integration features.
  /// </summary>
  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "Advanced Integration Engine")]
  public virtual bool? AdvancedIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "Retail Commerce")]
  public virtual bool? CommerceIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.commerceIntegration), DisplayName = "Amazon Connector", Visible = true)]
  public virtual bool? AmazonIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.commerceIntegration), DisplayName = "BigCommerce Connector")]
  public virtual bool? BigCommerceIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.commerceIntegration), DisplayName = "Shopify Connector")]
  public virtual bool? ShopifyIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.shopifyIntegration), DisplayName = "Shopify POS Connector")]
  public virtual bool? ShopifyPOS { get; set; }

  /// <summary>
  /// Represents the feature switch that enables support for custom-developed connectors
  /// </summary>
  [Feature(false, typeof (FeaturesSet.advancedIntegration), DisplayName = "Custom Connectors")]
  public virtual bool? CustomCommerceConnectors { get; set; }

  /// <summary>
  /// Represents the feature switch that enables/disables B2B features in Commerce connectors
  /// </summary>
  [FeatureDependency(false, new System.Type[] {typeof (FeaturesSet.shopifyIntegration), typeof (FeaturesSet.shopifyPOS), typeof (FeaturesSet.bigCommerceIntegration)})]
  [Feature(false, typeof (FeaturesSet.commerceIntegration), DisplayName = "Business-to-Business Entities")]
  public virtual bool? CommerceB2B { get; set; }

  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "Bank Feed Integration")]
  public virtual bool? BankFeedIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "Integrated Card Processing")]
  [FeatureRestrictor(typeof (SelectFromBase<PX.Objects.CA.PaymentMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired, Equal<True>>>>, And<BqlOperand<PX.Objects.CA.PaymentMethod.paymentType, IBqlString>.IsEqual<PaymentMethodType.creditCard>>>, And<BqlOperand<PX.Objects.CA.PaymentMethod.useForAR, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.CA.PaymentMethod.aRIsOnePerCustomer, IBqlBool>.IsEqual<False>>>))]
  public virtual bool? IntegratedCardProcessing { get; set; }

  [Feature(false, typeof (FeaturesSet.integratedCardProcessing), DisplayName = "Acumatica Payments", Visible = true)]
  public virtual bool? AcumaticaPayments { get; set; }

  [Feature(false, typeof (FeaturesSet.integratedCardProcessing), DisplayName = "Authorize.Net Payment Plug-In", Visible = false)]
  public virtual bool? AuthorizeNetIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.integratedCardProcessing), DisplayName = "Stripe Payment Plug-In", Visible = true)]
  public virtual bool? StripeIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.integratedCardProcessing), DisplayName = "Custom Payment Plug-In", Visible = true)]
  public virtual bool? CustomCCIntegration { get; set; }

  [FeatureRestrictor(typeof (Select<PREarningType>))]
  [Feature(false, null, null, DisplayName = "Payroll")]
  [FeatureMutuallyExclusiveDependency(true, new System.Type[] {typeof (FeaturesSet.multipleBaseCurrencies)})]
  public virtual bool? PayrollModule { get; set; }

  [FeatureRestrictor(typeof (Select<EPShiftCode>))]
  [Feature(false, typeof (FeaturesSet.timeReportingModule), DisplayName = "Shift Differential")]
  public virtual bool? ShiftDifferential { get; set; }

  [Feature(false, typeof (FeaturesSet.payrollModule), DisplayName = "US Payroll", Visible = true)]
  [FeatureRestrictor(typeof (SelectFromBase<PRTaxCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PRTaxCode.countryID, Equal<CountryCodes.us>>>>>.And<BqlOperand<PRTaxCode.isDeleted, IBqlBool>.IsEqual<False>>>))]
  [FeatureRestrictor(typeof (SelectFromBase<PRDeductCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PRDeductCode.countryID, IBqlString>.IsEqual<CountryCodes.us>>))]
  public virtual bool? PayrollUS { get; set; }

  [Feature(false, typeof (FeaturesSet.payrollModule), DisplayName = "Canadian Payroll", Visible = true)]
  [FeatureRestrictor(typeof (SelectFromBase<PRTaxCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PRTaxCode.countryID, Equal<CountryCodes.canada>>>>>.And<BqlOperand<PRTaxCode.isDeleted, IBqlBool>.IsEqual<False>>>))]
  [FeatureRestrictor(typeof (SelectFromBase<PRDeductCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PRDeductCode.countryID, IBqlString>.IsEqual<CountryCodes.canada>>))]
  public virtual bool? PayrollCAN { get; set; }

  [Feature(false, Visible = false)]
  public virtual bool? PayrollConstruction { get; set; }

  [Feature(true, null, null, DisplayName = "Platform", Enabled = false)]
  public virtual bool? PlatformModule
  {
    get => this._PlatformModule;
    set => this._PlatformModule = value;
  }

  [Feature(true, typeof (FeaturesSet.platformModule), DisplayName = "Monitoring & Automation", Enabled = false)]
  public virtual bool? MiscModule
  {
    get => this._MiscModule;
    set => this._MiscModule = value;
  }

  [FeatureRestrictor(typeof (Select<PMTimeActivity, Where<PMTimeActivity.trackTime, Equal<boolTrue>>>))]
  [Feature(false, null, DisplayName = "Time Management")]
  public virtual bool? TimeReportingModule
  {
    get => this._TimeReportingModule;
    set => this._TimeReportingModule = value;
  }

  [Feature(typeof (FeaturesSet.miscModule), DisplayName = "Approval Workflow")]
  public virtual bool? ApprovalWorkflow
  {
    get => this._ApprovalWorkflow;
    set => this._ApprovalWorkflow = value;
  }

  [Feature(typeof (FeaturesSet.miscModule), DisplayName = "Field-Level Audit")]
  public virtual bool? FieldLevelLogging
  {
    get => this._FieldLevelLogging;
    set => this._FieldLevelLogging = value;
  }

  [Feature(typeof (FeaturesSet.miscModule), typeof (Select<RelationGroup>), DisplayName = "Row-Level Security")]
  public virtual bool? RowLevelSecurity
  {
    get => this._RowLevelSecurity;
    set => this._RowLevelSecurity = value;
  }

  [Feature(true, typeof (FeaturesSet.miscModule), typeof (Select<AUSchedule, Where<AUSchedule.isActive, Equal<True>>>), DisplayName = "Scheduled Processing")]
  public virtual bool? ScheduleModule
  {
    get => this._ScheduleModule;
    set => this._ScheduleModule = value;
  }

  [Feature(false, typeof (FeaturesSet.miscModule), typeof (Select<AUNotification, Where<AUNotification.isActive, Equal<True>>>), DisplayName = "Change Notifications", Visible = false)]
  public virtual bool? NotificationModule
  {
    get => this._NotificationModule;
    set => this._NotificationModule = value;
  }

  [Feature(typeof (FeaturesSet.miscModule), DisplayName = "DeviceHub")]
  public virtual bool? DeviceHub
  {
    get => this._DeviceHub;
    set => this._DeviceHub = value;
  }

  [Feature(false, typeof (FeaturesSet.miscModule), DisplayName = "GDPR Compliance Tools")]
  public virtual bool? GDPRCompliance { get; set; }

  [Feature(false, typeof (FeaturesSet.miscModule), DisplayName = "Secure Business Date")]
  public virtual bool? SecureBusinessDate { get; set; }

  [Feature(true, DisplayName = "Third-Party Integrations")]
  public virtual bool? IntegrationModule
  {
    get => this._IntegrationModule;
    set => this._IntegrationModule = value;
  }

  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "Shipping Carrier Integration")]
  [FeatureDependency(true, new System.Type[] {typeof (FeaturesSet.distributionModule), typeof (FeaturesSet.inventory)})]
  public virtual bool? CarrierIntegration
  {
    get => this._CarrierIntegration;
    set => this._CarrierIntegration = value;
  }

  [Feature(false, typeof (FeaturesSet.carrierIntegration), DisplayName = "FedEx")]
  public virtual bool? FedExCarrierIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.carrierIntegration), DisplayName = "UPS")]
  public virtual bool? UPSCarrierIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.carrierIntegration), DisplayName = "Stamps.com")]
  public virtual bool? StampsCarrierIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.carrierIntegration), DisplayName = "ShipEngine")]
  public virtual bool? ShipEngineCarrierIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.carrierIntegration), DisplayName = "EasyPost")]
  public virtual bool? EasyPostCarrierIntegration { get; set; }

  /// <summary>
  /// Represents the feature switch that enables support for Pacejet Carrier Integration
  /// </summary>
  [Feature(false, typeof (FeaturesSet.carrierIntegration), DisplayName = "Pacejet")]
  public virtual bool? PacejetCarrierIntegration { get; set; }

  [Feature(false, typeof (FeaturesSet.carrierIntegration), DisplayName = "Custom")]
  public virtual bool? CustomCarrierIntegration { get; set; }

  [Feature(typeof (FeaturesSet.integrationModule), DisplayName = "Exchange Integration")]
  public virtual bool? ExchangeIntegration
  {
    get => this._ExchangeIntegration;
    set => this._ExchangeIntegration = value;
  }

  [Feature(typeof (FeaturesSet.integrationModule), DisplayName = "External Tax Calculation Integration")]
  public virtual bool? AvalaraTax
  {
    get => this._AvalaraTax;
    set => this._AvalaraTax = value;
  }

  /// <summary>
  /// Represents the feature switch that enables/disables external exemption certificate management
  /// </summary>
  [Feature(false, typeof (FeaturesSet.avalaraTax), DisplayName = "Exemption Certificate Management")]
  public virtual bool? ECM { get; set; }

  [Feature(typeof (FeaturesSet.integrationModule), DisplayName = "Address Validation Integration")]
  public virtual bool? AddressValidation
  {
    get => this._AddressValidation;
    set => this._AddressValidation = value;
  }

  /// <summary>Bill.com - External payment processor</summary>
  [Feature(typeof (FeaturesSet.integrationModule), DisplayName = "Bill.com Integration")]
  public virtual bool? PaymentProcessor { get; set; }

  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "Salesforce Integration")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.customerModule)})]
  public virtual bool? SalesforceIntegration
  {
    get => this._SalesforceIntegration;
    set => this._SalesforceIntegration = value;
  }

  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "HubSpot Integration")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.customerModule)})]
  public virtual bool? HubSpotIntegration
  {
    get => this._HubSpotIntegration;
    set => this._HubSpotIntegration = value;
  }

  [Feature(false, typeof (FeaturesSet.integrationModule), DisplayName = "Procore Integration")]
  [FeatureMutuallyExclusiveDependency(true, new System.Type[] {typeof (FeaturesSet.multipleBaseCurrencies)})]
  public virtual bool? ProcoreIntegration { get; set; }

  [Feature(typeof (FeaturesSet.integrationModule), DisplayName = "Outlook Integration")]
  public virtual bool? OutlookIntegration { get; set; }

  protected virtual void SetValueToPackGroup(
    ref bool? packGroup,
    bool? value,
    ref bool? option1,
    ref bool? option2,
    ref bool? option3)
  {
    packGroup = value;
    bool? nullable1 = value;
    bool flag1 = false;
    if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
    {
      option1 = new bool?(false);
      option2 = new bool?(false);
      option3 = new bool?(false);
    }
    else
    {
      bool? nullable2 = option1;
      bool flag2 = false;
      if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
        return;
      bool? nullable3 = option2;
      bool flag3 = false;
      if (!(nullable3.GetValueOrDefault() == flag3 & nullable3.HasValue))
        return;
      bool? nullable4 = option3;
      bool flag4 = false;
      if (!(nullable4.GetValueOrDefault() == flag4 & nullable4.HasValue))
        return;
      option1 = new bool?(true);
    }
  }

  protected virtual void SetValueToPackOption(
    ref bool? packOption,
    bool? value,
    bool? packGroup,
    ref bool? otherOption1,
    ref bool? otherOption2)
  {
    if (value.GetValueOrDefault())
    {
      otherOption1 = new bool?(false);
      otherOption2 = new bool?(false);
      packOption = new bool?(true);
    }
    else
    {
      bool? nullable = packGroup;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) && !otherOption1.GetValueOrDefault() && !otherOption2.GetValueOrDefault())
        return;
      packOption = new bool?(false);
    }
  }

  [Feature(false, DisplayName = "Manufacturing")]
  public virtual bool? Manufacturing { get; set; }

  [Feature(typeof (FeaturesSet.manufacturing), DisplayName = "Material Requirements Planning")]
  public virtual bool? ManufacturingMRP { get; set; }

  [Feature(typeof (FeaturesSet.manufacturing), DisplayName = "Product Configurator")]
  public virtual bool? ManufacturingProductConfigurator { get; set; }

  [Feature(typeof (FeaturesSet.manufacturing), DisplayName = "Estimating")]
  public virtual bool? ManufacturingEstimating { get; set; }

  [Feature(typeof (FeaturesSet.manufacturing), DisplayName = "Advanced Planning and Scheduling")]
  public virtual bool? ManufacturingAdvancedPlanning { get; set; }

  [Feature(typeof (FeaturesSet.manufacturing), DisplayName = "Engineering Change Control")]
  public virtual bool? ManufacturingECC { get; set; }

  [Feature(typeof (FeaturesSet.manufacturing), DisplayName = "Manufacturing Data Collection")]
  public virtual bool? ManufacturingDataCollection { get; set; }

  [Feature(false, DisplayName = "Core Manufacturing", Visible = false)]
  public virtual bool? ManufacturingBase { get; set; }

  [Feature(typeof (FeaturesSet.platformModule), DisplayName = "Image Recognition for Expense Receipts")]
  public virtual bool? ImageRecognition
  {
    get => this._ImageRecognition;
    set => this._ImageRecognition = value;
  }

  [Feature(typeof (FeaturesSet.platformModule), DisplayName = "AP Document Recognition Service")]
  public virtual bool? APDocumentRecognition { get; set; }

  [Feature(typeof (FeaturesSet.integrationModule), DisplayName = "Workwave Route Optimization")]
  public virtual bool? RouteOptimizer
  {
    get => this._RouteOptimizer;
    set => this._RouteOptimizer = value;
  }

  [Feature(true, typeof (FeaturesSet.platformModule), DisplayName = "Authentication")]
  public virtual bool? AdvancedAuthentication { get; set; }

  [Feature(true, typeof (FeaturesSet.advancedAuthentication), DisplayName = "Two-Factor Authentication")]
  public virtual bool? TwoFactorAuthentication { get; set; }

  [Feature(true, typeof (FeaturesSet.advancedAuthentication), DisplayName = "Active Directory and Other External SSO")]
  public virtual bool? ActiveDirectoryAndOtherExternalSSO { get; set; }

  [Feature(true, typeof (FeaturesSet.advancedAuthentication), DisplayName = "OpenID Connect")]
  public virtual bool? OpenIDConnect { get; set; }

  [PXDefault]
  [Feature(false, null, null, DisplayName = "Canadian Localization", Visible = true)]
  public bool? CanadianLocalization { get; set; }

  [PXDefault]
  [Feature(false, null, null, DisplayName = "UK Localization", Visible = true)]
  public bool? UKLocalization { get; set; }

  [Feature(true, DisplayName = "Experimental Features", Enabled = false)]
  public virtual bool? ExperimentalFeatures { get; set; }

  [Feature(typeof (FeaturesSet.experimentalFeatures), DisplayName = "Import of SendGrid Designs")]
  public virtual bool? ImportSendGridDesigns { get; set; }

  [Feature(typeof (FeaturesSet.experimentalFeatures), DisplayName = "GL Anomaly Detection")]
  public virtual bool? GLAnomalyDetection { get; set; }

  /// <exclude />
  [Feature(typeof (FeaturesSet.experimentalFeatures), DisplayName = "Related Item Assistant")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.relatedItems), typeof (FeaturesSet.commerceIntegration)})]
  public virtual bool? RelatedItemAssistant { get; set; }

  [PXDefault]
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Teams Integration")]
  public bool? TeamsIntegration { get; set; }

  /// <summary>Intelligent Text Completion.</summary>
  [Feature(true, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Intelligent Text Completion")]
  public virtual bool? IntelligentTextCompletion { get; set; }

  /// <summary>Detection of Numeric Anomalies in Generic Inquiries.</summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Detection of Numeric Anomalies in Generic Inquiries")]
  public bool? GIAnomalyDetection { get; set; }

  /// <summary>
  /// AI Studio. Use user-written AI prompts to autofill business objects with data.
  /// </summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "AI Studio")]
  public bool? AIStudio { get; set; }

  /// <summary>
  /// A Boolean field that indicates whether the mapping of multiple bank feed accounts to one cash account is enabled.
  /// </summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Mapping of Multiple Accounts for Bank Feeds")]
  public bool? BankFeedAccountsMultipleMapping { get; set; }

  /// <summary>Sales Territory Management</summary>
  [Feature(typeof (FeaturesSet.experimentalFeatures), typeof (Select<SalesTerritory>), DisplayName = "Sales Territory Management")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.customerModule)})]
  public virtual bool? SalesTerritoryManagement { get; set; }

  /// <summary>Case Commitments</summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Case Commitments")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.caseManagement)})]
  public virtual bool? CaseCommitmentsTracking { get; set; }

  /// <summary>Customer Portal (New UI)</summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Modern Customer Portal")]
  [FeatureMutuallyExclusiveDependency(true, new System.Type[] {typeof (FeaturesSet.multipleBaseCurrencies)})]
  public virtual bool? ModernPortalModule { get; set; }

  /// <summary>Portal B2B Ordering</summary>
  [Feature(false, typeof (FeaturesSet.modernPortalModule), DisplayName = "B2B Ordering (Modern Portal)")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.distributionModule)})]
  public virtual bool? ModernPortalB2BOrdering { get; set; }

  /// <summary>Portal case management</summary>
  [Feature(false, typeof (FeaturesSet.modernPortalModule), DisplayName = "Case Management (Modern Portal)")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.caseManagement)})]
  public virtual bool? ModernPortalCaseManagement { get; set; }

  /// <summary>Portal financials</summary>
  [Feature(false, typeof (FeaturesSet.modernPortalModule), DisplayName = "Financials (Modern Portal)")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.financialAdvanced)})]
  public virtual bool? ModernPortalFinancials { get; set; }

  /// <summary>Portal Payments</summary>
  [Feature(false, typeof (FeaturesSet.modernPortalModule), DisplayName = "Payments (Modern Portal)")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.integratedCardProcessing)})]
  public virtual bool? ModernPortalPayments { get; set; }

  /// <summary>Lot/Serial Attributes</summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Lot/Serial Attributes")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.lotSerialTracking)})]
  [FeatureMutuallyExclusiveDependency(true, new System.Type[] {typeof (FeaturesSet.multipleBaseCurrencies)})]
  public virtual bool? LotSerialAttributes { get; set; }

  /// <summary>Recognition of Project-Related Documents</summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Recognition of Project-Related Documents")]
  [FeatureDependency(true, new System.Type[] {typeof (FeaturesSet.apDocumentRecognition), typeof (FeaturesSet.projectAccounting)})]
  public virtual bool? ProjectRelatedDocumentsRecognition { get; set; }

  /// <summary>Full-Text Search for Inventory</summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Full-Text Search for Inventory")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.distributionModule)})]
  public virtual bool? InventoryFullTextSearch { get; set; }

  /// <summary>Clock In and Clock Out</summary>
  [Feature(false, typeof (FeaturesSet.experimentalFeatures), DisplayName = "Clock In and Clock Out")]
  [FeatureDependency(new System.Type[] {typeof (FeaturesSet.timeReportingModule)})]
  public virtual bool? ClockInClockOut { get; set; }

  public abstract class licenseID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FeaturesSet.licenseID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FeaturesSet.status>
  {
  }

  public abstract class validUntill : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FeaturesSet.validUntill>
  {
  }

  public abstract class validationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FeaturesSet.validationCode>
  {
  }

  public abstract class financialModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.financialModule>
  {
  }

  public abstract class financialStandard : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.financialStandard>
  {
  }

  public abstract class branch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.branch>
  {
  }

  public abstract class multiCompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.multiCompany>
  {
  }

  public abstract class accountLocations : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.accountLocations>
  {
  }

  public abstract class multicurrency : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.multicurrency>
  {
  }

  public abstract class centralizedPeriodsManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.centralizedPeriodsManagement>
  {
  }

  public abstract class supportBreakQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.supportBreakQty>
  {
  }

  public abstract class prebooking : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.prebooking>
  {
  }

  public abstract class taxEntryFromGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.taxEntryFromGL>
  {
    public static readonly string EntityInUseKey = typeof (FeaturesSet.taxEntryFromGL).FullName;

    public class entityInUseKey : Constant<string>
    {
      public entityInUseKey()
        : base(FeaturesSet.taxEntryFromGL.EntityInUseKey)
      {
      }
    }
  }

  public abstract class vATReporting : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.vATReporting>
  {
  }

  public abstract class vATRecognitionOnPrepaymentsAP : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.vATRecognitionOnPrepaymentsAP>
  {
  }

  public abstract class vATRecognitionOnPrepaymentsAR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.vATRecognitionOnPrepaymentsAR>
  {
  }

  public abstract class reporting1099 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.reporting1099>
  {
  }

  public abstract class netGrossEntryMode : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.netGrossEntryMode>
  {
  }

  public abstract class invoiceRounding : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.invoiceRounding>
  {
  }

  public abstract class expenseManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.expenseManagement>
  {
  }

  public abstract class financialAdvanced : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.financialAdvanced>
  {
  }

  public abstract class subAccount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.subAccount>
  {
  }

  public abstract class allocationTemplates : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.allocationTemplates>
  {
  }

  public abstract class interBranch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.interBranch>
  {
  }

  public abstract class projectAccounting : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.projectAccounting>
  {
    public const string InventorySource = "InventorySource";
  }

  public abstract class budgetForecast : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.budgetForecast>
  {
  }

  public abstract class changeOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.changeOrder>
  {
  }

  public abstract class changeRequest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.changeRequest>
  {
  }

  public abstract class construction : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.construction>
  {
  }

  public abstract class constructionProjectManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.constructionProjectManagement>
  {
  }

  public abstract class projectOverview : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.projectOverview>
  {
  }

  public abstract class weatherServices : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.weatherServices>
  {
  }

  public abstract class costCodes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.costCodes>
  {
  }

  public abstract class projectMultiCurrency : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.projectMultiCurrency>
  {
  }

  public abstract class professionalServices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.professionalServices>
  {
  }

  public abstract class projectQuotes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.projectQuotes>
  {
  }

  public abstract class materialManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.materialManagement>
  {
  }

  public abstract class fileManagement : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.fileManagement>
  {
  }

  public abstract class visibilityRestriction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.visibilityRestriction>
  {
  }

  public abstract class multipleBaseCurrencies : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.multipleBaseCurrencies>
  {
  }

  public abstract class multipleCalendarsSupport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.multipleCalendarsSupport>
  {
  }

  public abstract class gLConsolidation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.gLConsolidation>
  {
  }

  public abstract class finStatementCurTranslation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.finStatementCurTranslation>
  {
  }

  public abstract class customerDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.customerDiscounts>
  {
  }

  public abstract class vendorDiscounts : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.vendorDiscounts>
  {
  }

  public abstract class commissions : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.commissions>
  {
  }

  public abstract class overdueFinCharges : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.overdueFinCharges>
  {
  }

  public abstract class dunningLetter : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.dunningLetter>
  {
  }

  public abstract class defferedRevenue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.defferedRevenue>
  {
  }

  public abstract class aSC606 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.aSC606>
  {
  }

  public abstract class consolidatedPosting : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.consolidatedPosting>
  {
  }

  public abstract class parentChildAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.parentChildAccount>
  {
  }

  public abstract class retainage : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.retainage>
  {
  }

  public abstract class perUnitTaxSupport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.perUnitTaxSupport>
  {
  }

  public abstract class paymentsByLines : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.paymentsByLines>
  {
  }

  public abstract class exemptedTaxReporting : IBqlField, IBqlOperand
  {
  }

  public abstract class bankTransactionSplits : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.bankTransactionSplits>
  {
  }

  public abstract class contractManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.contractManagement>
  {
  }

  public abstract class fixedAsset : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.fixedAsset>
  {
  }

  public abstract class distributionModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.distributionModule>
  {
  }

  public abstract class inventory : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.inventory>
  {
    public const string FieldClass = "DISTINV";
    public const string CostLayerType = "CostLayerType";
  }

  public abstract class multipleUnitMeasure : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.multipleUnitMeasure>
  {
  }

  public abstract class lotSerialTracking : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.lotSerialTracking>
  {
  }

  public abstract class blanketPO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.blanketPO>
  {
  }

  public abstract class pOReceiptsWithoutInventory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.pOReceiptsWithoutInventory>
  {
  }

  public abstract class dropShipments : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.dropShipments>
  {
  }

  public abstract class warehouse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.warehouse>
  {
  }

  public abstract class orderOrchestration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.orderOrchestration>
  {
  }

  public abstract class distributionReqPlan : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.distributionReqPlan>
  {
  }

  public abstract class warehouseLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.warehouseLocation>
  {
  }

  public abstract class replenishment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.replenishment>
  {
  }

  public abstract class matrixItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.matrixItem>
  {
  }

  public abstract class subItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.subItem>
  {
  }

  public abstract class autoPackaging : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.autoPackaging>
  {
  }

  public abstract class kitAssemblies : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.kitAssemblies>
  {
  }

  public abstract class relatedItems : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.relatedItems>
  {
  }

  public abstract class advancedPhysicalCounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.advancedPhysicalCounts>
  {
  }

  public abstract class sOToPOLink : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.sOToPOLink>
  {
  }

  public abstract class specialOrders : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.specialOrders>
  {
    public const string FieldClass = "SpecialOrders";
  }

  public abstract class userDefinedOrderTypes : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.userDefinedOrderTypes>
  {
  }

  public abstract class purchaseRequisitions : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.purchaseRequisitions>
  {
  }

  public abstract class advancedSOInvoices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.advancedSOInvoices>
  {
  }

  public abstract class crossReferenceUniqueness : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.crossReferenceUniqueness>
  {
  }

  public abstract class vendorRelations : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.vendorRelations>
  {
  }

  public abstract class advancedFulfillment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.advancedFulfillment>
  {
  }

  public abstract class wMSFulfillment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.wMSFulfillment>
  {
  }

  public abstract class wMSPaperlessPicking : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.wMSPaperlessPicking>
  {
  }

  public abstract class wMSAdvancedPicking : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.wMSAdvancedPicking>
  {
  }

  public abstract class wMSReceiving : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.wMSReceiving>
  {
  }

  public abstract class wMSInventory : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.wMSInventory>
  {
  }

  public abstract class wMSCartTracking : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.wMSCartTracking>
  {
  }

  public abstract class organizationModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.organizationModule>
  {
  }

  public abstract class customerModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.customerModule>
  {
    public const string FieldClass = "CRM";
  }

  public abstract class caseManagement : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.caseManagement>
  {
    public const string FieldClass = "CASE";
  }

  public abstract class contactDuplicate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.contactDuplicate>
  {
    public const string FieldClass = "DUPLICATE";
  }

  public abstract class sendGridIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.sendGridIntegration>
  {
    public const string FieldClass = "SENDGRID";
  }

  public abstract class salesQuotes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.salesQuotes>
  {
  }

  public abstract class addressLookup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.addressLookup>
  {
    public const string FieldClass = "Address Lookup";
  }

  public abstract class projectModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.projectModule>
  {
  }

  public abstract class portalModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.portalModule>
  {
  }

  public abstract class b2BOrdering : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.b2BOrdering>
  {
  }

  public abstract class portalCaseManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.portalCaseManagement>
  {
  }

  public abstract class portalFinancials : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.portalFinancials>
  {
  }

  public abstract class serviceManagementModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.serviceManagementModule>
  {
  }

  public abstract class equipmentManagementModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.equipmentManagementModule>
  {
  }

  public abstract class routeManagementModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.routeManagementModule>
  {
  }

  public abstract class advancedIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.advancedIntegration>
  {
  }

  public abstract class commerceIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.commerceIntegration>
  {
  }

  public abstract class amazonIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.amazonIntegration>
  {
  }

  public abstract class bigCommerceIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.bigCommerceIntegration>
  {
  }

  public abstract class shopifyIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.shopifyIntegration>
  {
  }

  public abstract class shopifyPOS : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.shopifyPOS>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CS.FeaturesSet.CustomCommerceConnectors" />
  public abstract class customCommerceConnectors : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.customCommerceConnectors>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.CS.FeaturesSet.CommerceB2B" />
  public abstract class commerceB2B : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.commerceB2B>
  {
  }

  public abstract class bankFeedIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.bankFeedIntegration>
  {
  }

  public abstract class integratedCardProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.integratedCardProcessing>
  {
  }

  public abstract class acumaticaPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.acumaticaPayments>
  {
  }

  public abstract class authorizeNetIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.authorizeNetIntegration>
  {
  }

  public abstract class stripeIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.stripeIntegration>
  {
  }

  public abstract class customCCIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.customCCIntegration>
  {
  }

  public abstract class payrollModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.payrollModule>
  {
  }

  public abstract class shiftDifferential : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.shiftDifferential>
  {
  }

  public abstract class payrollUS : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.payrollUS>
  {
  }

  public abstract class payrollCAN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.payrollCAN>
  {
  }

  public abstract class payrollConstruction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.payrollConstruction>
  {
  }

  public abstract class platformModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.platformModule>
  {
  }

  public abstract class miscModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.miscModule>
  {
  }

  public abstract class timeReportingModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.timeReportingModule>
  {
  }

  public abstract class approvalWorkflow : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.approvalWorkflow>
  {
  }

  public abstract class fieldLevelLogging : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.fieldLevelLogging>
  {
  }

  public abstract class rowLevelSecurity : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.rowLevelSecurity>
  {
  }

  public abstract class scheduleModule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.scheduleModule>
  {
  }

  public abstract class notificationModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.notificationModule>
  {
  }

  public abstract class deviceHub : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.deviceHub>
  {
  }

  public abstract class gDPRCompliance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.gDPRCompliance>
  {
    public const string FieldClass = "GDPR";
  }

  public abstract class secureBusinessDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.secureBusinessDate>
  {
  }

  public abstract class integrationModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.integrationModule>
  {
  }

  public abstract class carrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.carrierIntegration>
  {
  }

  public abstract class fedExCarrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.fedExCarrierIntegration>
  {
  }

  public abstract class uPSCarrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.uPSCarrierIntegration>
  {
  }

  public abstract class stampsCarrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.stampsCarrierIntegration>
  {
  }

  public abstract class shipEngineCarrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.shipEngineCarrierIntegration>
  {
  }

  public abstract class easyPostCarrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.easyPostCarrierIntegration>
  {
  }

  public abstract class pacejetCarrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.pacejetCarrierIntegration>
  {
  }

  public abstract class customCarrierIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.customCarrierIntegration>
  {
  }

  public abstract class exchangeIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.exchangeIntegration>
  {
    public const string FieldClass = "ExchangeIntegration";
  }

  public abstract class avalaraTax : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.avalaraTax>
  {
  }

  public abstract class eCM : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.eCM>
  {
  }

  public abstract class addressValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.addressValidation>
  {
    public const string FieldClass = "Validate Address";
  }

  public abstract class paymentProcessor : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.paymentProcessor>
  {
  }

  public abstract class salesforceIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.salesforceIntegration>
  {
  }

  public abstract class hubSpotIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.hubSpotIntegration>
  {
  }

  public abstract class procoreIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.procoreIntegration>
  {
  }

  public abstract class outlookIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.outlookIntegration>
  {
  }

  public abstract class manufacturing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.manufacturing>
  {
  }

  public abstract class manufacturingMRP : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.manufacturingMRP>
  {
  }

  public abstract class manufacturingProductConfigurator : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.manufacturingProductConfigurator>
  {
  }

  public abstract class manufacturingEstimating : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.manufacturingEstimating>
  {
  }

  public abstract class manufacturingAdvancedPlanning : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.manufacturingAdvancedPlanning>
  {
  }

  public abstract class manufacturingECC : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.manufacturingECC>
  {
  }

  public abstract class manufacturingDataCollection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.manufacturingDataCollection>
  {
  }

  public abstract class manufacturingBase : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.manufacturingBase>
  {
  }

  public abstract class imageRecognition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.imageRecognition>
  {
  }

  public abstract class apDocumentRecognition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.apDocumentRecognition>
  {
  }

  public abstract class routeOptimizer : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.routeOptimizer>
  {
  }

  public abstract class advancedAuthentication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.advancedAuthentication>
  {
  }

  public abstract class twoFactorAuthentication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.twoFactorAuthentication>
  {
  }

  public abstract class activeDirectoryAndOtherExternalSSO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.activeDirectoryAndOtherExternalSSO>
  {
  }

  public abstract class openIDConnect : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.openIDConnect>
  {
  }

  public abstract class canadianLocalization : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.canadianLocalization>
  {
  }

  public abstract class uKLocalization : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.uKLocalization>
  {
  }

  public abstract class experimentalFeatures : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.experimentalFeatures>
  {
  }

  public abstract class importSendGridDesigns : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.importSendGridDesigns>
  {
  }

  public abstract class glAnomalyDetection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.glAnomalyDetection>
  {
  }

  public abstract class relatedItemAssistant : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.relatedItemAssistant>
  {
  }

  public abstract class teamsIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.teamsIntegration>
  {
  }

  public abstract class intelligentTextCompletion : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.intelligentTextCompletion>
  {
  }

  public abstract class giAnomalyDetection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.giAnomalyDetection>
  {
  }

  public abstract class aiStudio : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.aiStudio>
  {
  }

  public abstract class bankFeedAccountsMultipleMapping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.bankFeedAccountsMultipleMapping>
  {
  }

  public abstract class salesTerritoryManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.salesTerritoryManagement>
  {
    public const string FieldClass = "SalesTerritoryManagement";
  }

  public abstract class caseCommitmentsTracking : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.caseCommitmentsTracking>
  {
    public const string FieldClass = "CaseCommitmentsTracking";
  }

  public abstract class modernPortalModule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.modernPortalModule>
  {
  }

  public abstract class modernPortalB2BOrdering : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.modernPortalB2BOrdering>
  {
  }

  public abstract class modernPortalCaseManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.modernPortalCaseManagement>
  {
  }

  public abstract class modernPortalFinancials : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.modernPortalFinancials>
  {
  }

  public abstract class modernPortalPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.modernPortalPayments>
  {
  }

  public abstract class lotSerialAttributes : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.lotSerialAttributes>
  {
  }

  public abstract class projectRelatedDocumentsRecognition : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.projectRelatedDocumentsRecognition>
  {
    public const string FieldClass = "ProjectRelatedDocumentsRecognition";
  }

  public abstract class inventoryFullTextSearch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FeaturesSet.inventoryFullTextSearch>
  {
  }

  public abstract class clockInClockOut : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FeaturesSet.clockInClockOut>
  {
  }
}
