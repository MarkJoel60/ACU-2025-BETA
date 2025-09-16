// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.ReportParameters.OrganizationBranchReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CA;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;
using PX.Objects.GL.Descriptor;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;

#nullable enable
namespace PX.Objects.Common.DAC.ReportParameters;

public class OrganizationBranchReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AccountID;
  protected int? _SubID;

  [Organization(false, typeof (Search2<PX.Objects.GL.DAC.Organization.organizationID, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>, And2<FeatureInstalled<FeaturesSet.branch>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>>))]
  public int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (OrganizationBranchReportParameters.organizationID), false, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, CrossJoin<FeaturesSet>>, Where<FeaturesSet.branch, Equal<True>, And<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.withoutBranches>, And<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>>), null)]
  public int? BranchID { get; set; }

  [OrganizationTree(typeof (OrganizationBranchReportParameters.organizationID), typeof (OrganizationBranchReportParameters.branchID), null, false)]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
  public int? OrgBAccountID { get; set; }

  [LedgerOfOrganization(typeof (OrganizationBranchReportParameters.organizationID), typeof (OrganizationBranchReportParameters.branchID), null)]
  public virtual int? LedgerID { get; set; }

  [LedgerOfOrganization(typeof (OrganizationBranchReportParameters.organizationID), typeof (OrganizationBranchReportParameters.branchID), typeof (Where<PX.Objects.GL.Ledger.balanceType, NotEqual<LedgerBalanceType.budget>>))]
  public virtual int? NotBudgetLedgerID { get; set; }

  [LedgerOfOrganization(typeof (OrganizationBranchReportParameters.organizationID), typeof (OrganizationBranchReportParameters.branchID), null, typeof (Search<PX.Objects.GL.Ledger.ledgerID>), typeof (Where<PX.Objects.GL.Ledger.balanceType, Equal<LedgerBalanceType.budget>>))]
  public virtual int? BudgetLedgerID { get; set; }

  [FinPeriodSelector(null, typeof (AccessInfo.businessDate), typeof (OrganizationBranchReportParameters.branchID), null, typeof (OrganizationBranchReportParameters.organizationID), null, null, true, true, true, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public 
  #nullable disable
  string FinPeriodID { get; set; }

  [FinPeriodSelector(null, typeof (AccessInfo.businessDate), typeof (OrganizationBranchReportParameters.branchID), null, null, null, null, true, true, false, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string BranchFinPeriodID { get; set; }

  [FinPeriodSelector(null, typeof (AccessInfo.businessDate), null, null, typeof (OrganizationBranchReportParameters.organizationID), null, null, true, false, true, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string FinPeriodIDByOrganization { get; set; }

  [FinPeriodSelector(null, typeof (AccessInfo.businessDate), null, null, null, null, null, true, false, false, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string MasterFinPeriodID { get; set; }

  [GenericFinYearSelector(null, typeof (AccessInfo.businessDate), typeof (OrganizationBranchReportParameters.branchID), typeof (OrganizationBranchReportParameters.organizationID), null, true, true, null, true, null)]
  public string FinYear { get; set; }

  [FinPeriodSelector(null, typeof (AccessInfo.businessDate), typeof (OrganizationBranchReportParameters.branchID), null, typeof (OrganizationBranchReportParameters.organizationID), null, typeof (Search2<FinPeriod.finPeriodID, InnerJoin<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, On<FinPeriod.finYear, Equal<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>, And<FinPeriod.organizationID, Equal<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID>>>>, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.startDate, LessEqual<Current<AccessInfo.businessDate>>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.endDate, GreaterEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Asc<FinPeriod.finPeriodID>>>), true, true, true, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string StartYearPeriodID { get; set; }

  [FinPeriodSelector(null, typeof (AccessInfo.businessDate), typeof (OrganizationBranchReportParameters.branchID), null, typeof (OrganizationBranchReportParameters.organizationID), null, typeof (Search2<FinPeriod.finPeriodID, InnerJoin<PX.Objects.GL.FinPeriods.TableDefinition.FinYear, On<FinPeriod.finYear, Equal<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year>, And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>, And<FinPeriod.organizationID, Equal<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID>>>>>, Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.startDate, LessEqual<Current<AccessInfo.businessDate>>, And<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.endDate, GreaterEqual<Current<AccessInfo.businessDate>>>>, OrderBy<Desc<FinPeriod.finPeriodID>>>), true, true, true, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string EndYearPeriodID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Use Master Calendar")]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleCalendarsSupport>))]
  public bool? UseMasterCalendar { get; set; }

  [FinPeriodSelector(typeof (Search2<OrganizationFinPeriod.finPeriodID, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<FinPeriod.organizationID>>, InnerJoin<CashAccount, On<CashAccount.branchID, Equal<PX.Objects.GL.Branch.branchID>>>>, Where<CashAccount.cashAccountCD, Equal<Optional<CashAccount.cashAccountCD>>>>), null, null, null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string CashAccountPeriodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (SearchFor<PX.Objects.GL.Ledger.ledgerID>.In<SelectFromBase<PX.Objects.GL.Ledger, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Ledger.ledgerID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.ledgerID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.branchID, IsNull>>>>.Or<Where<PX.Objects.GL.Branch.branchID, Inside<Optional<OrganizationBranchReportParameters.orgBAccountID>>>>>.AggregateTo<GroupBy<PX.Objects.GL.Ledger.ledgerID>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD))]
  public virtual int? LedgerIDByBAccount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (SearchFor<PX.Objects.GL.Ledger.ledgerID>.In<SelectFromBase<PX.Objects.GL.Ledger, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Ledger.ledgerID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.ledgerID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Ledger.balanceType, Equal<LedgerBalanceType.budget>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.branchID, IsNull>>>>.Or<Where<PX.Objects.GL.Branch.branchID, Inside<Optional<OrganizationBranchReportParameters.orgBAccountID>>>>>>.AggregateTo<GroupBy<PX.Objects.GL.Ledger.ledgerID>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD))]
  public virtual int? BudgetLedgerIDByBAccount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (SearchFor<PX.Objects.GL.Ledger.ledgerID>.In<SelectFromBase<PX.Objects.GL.Ledger, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Ledger.ledgerID, IBqlInt>.IsEqual<PX.Objects.GL.Branch.ledgerID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Ledger.balanceType, NotEqual<LedgerBalanceType.budget>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.Branch.branchID, IsNull>>>, Or<Where<PX.Objects.GL.Branch.branchID, Inside<Optional<OrganizationBranchReportParameters.orgBAccountID>>>>>>.Or<BqlOperand<Optional<OrganizationBranchReportParameters.orgBAccountID>, IBqlInt>.IsNull>>>.AggregateTo<GroupBy<PX.Objects.GL.Ledger.ledgerID>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD))]
  public virtual int? NotBudgetLedgerIDByBAccount { get; set; }

  [FinPeriodSelector(typeof (Search<OrganizationFinPeriod.finPeriodID, Where<Where2<Where<FinPeriod.organizationID, Suit<Optional2<OrganizationBranchReportParameters.orgBAccountID>>, And<IsNull<Optional2<OrganizationBranchReportParameters.useMasterCalendar>, False>, NotEqual<True>>>, Or<Where<FinPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<Optional2<OrganizationBranchReportParameters.useMasterCalendar>, Equal<True>>>>>>>), null, null, null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string FinPeriodIDByBAccount { get; set; }

  /// <summary>The last financial period of the date range.</summary>
  [FinPeriodSelector(typeof (Search<OrganizationFinPeriod.finPeriodID, Where<Where2<Where2<Where<FinPeriod.organizationID, Suit<Optional2<OrganizationBranchReportParameters.orgBAccountID>>, And<IsNull<Optional2<OrganizationBranchReportParameters.useMasterCalendar>, False>, NotEqual<True>>>, Or<Where<FinPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<Optional2<OrganizationBranchReportParameters.useMasterCalendar>, Equal<True>>>>>, And<FinPeriod.finPeriodID, StartsWith<Substring<Optional2<OrganizationBranchReportParameters.finPeriodIDByBAccount>, int1, int4>>, And<FinPeriod.finPeriodID, GreaterEqual<Optional2<OrganizationBranchReportParameters.finPeriodIDByBAccount>>>>>>>), null, null, null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public string EndFinPeriodIDByBAccountInSameYear { get; set; }

  [MatchOrganization]
  public int? OrgMatchBAccountID { get; set; }

  [AccountAny(null, typeof (Search2<PX.Objects.GL.Account.accountID, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<Optional2<OrganizationBranchReportParameters.orgBAccountID>>>>, Where2<Where<PX.Objects.GL.Branch.branchID, IsNull, Or<Match<Optional<OrganizationBranchReportParameters.orgMatchBAccountID>>>>, And<Match<Current<AccessInfo.userName>>>>>), DescriptionField = typeof (PX.Objects.GL.Account.description))]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (Search2<PX.Objects.GL.Sub.subID, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<Optional2<OrganizationBranchReportParameters.orgBAccountID>>>>, Where2<Where<PX.Objects.GL.Branch.branchID, IsNull, Or<Match<Optional<OrganizationBranchReportParameters.orgMatchBAccountID>>>>, And<Match<Current<AccessInfo.userName>>>>>), null, null, false, DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [CashAccount(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.branchID, Inside<Optional2<OrganizationBranchReportParameters.orgBAccountID>>, And<CashAccount.restrictVisibilityWithBranch, Equal<True>, Or<CashAccount.restrictVisibilityWithBranch, NotEqual<True>>>>>))]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// A full name for the document of the prepayment invoice type
  /// that is used only on the Invoice/Memo(AR641000) report.
  /// The documents of the prepayment invoice type are a part of the <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> feature.
  /// </summary>
  /// <value>
  /// Long equivalent of the<see cref="T:PX.Objects.AR.ARDocType.PrintListAttribute" /> for the prepayment invoice document type.
  /// </value>
  [PXDefault("Prepayment Invoice")]
  public string PrepaymentInvoiceFullName { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.organizationID>
  {
  }

  public abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.branchID>
  {
  }

  public abstract class orgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.orgBAccountID>
  {
  }

  public abstract class ledgerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.ledgerID>
  {
  }

  public abstract class notBudgetLedgerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.notBudgetLedgerID>
  {
  }

  public abstract class budgetLedgerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.budgetLedgerID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.finPeriodID>
  {
  }

  public abstract class branchFinPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class finPeriodIDByOrganization : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.finPeriodIDByOrganization>
  {
  }

  public abstract class masterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.masterFinPeriodID>
  {
  }

  public abstract class finYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.finYear>
  {
  }

  public abstract class startYearPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.startYearPeriodID>
  {
  }

  public abstract class endYearPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.endYearPeriodID>
  {
  }

  public abstract class useMasterCalendar : IBqlField, IBqlOperand
  {
  }

  public abstract class cashAccountPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class ledgerIDByBAccount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.ledgerIDByBAccount>
  {
  }

  public abstract class budgetLedgerIDByBAccount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.budgetLedgerIDByBAccount>
  {
  }

  public abstract class notBudgetLedgerIDByBAccount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.notBudgetLedgerIDByBAccount>
  {
  }

  public abstract class finPeriodIDByBAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.finPeriodIDByBAccount>
  {
  }

  public abstract class endFinPeriodIDByBAccountInSameYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationBranchReportParameters.endFinPeriodIDByBAccountInSameYear>
  {
  }

  public abstract class orgMatchBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.orgMatchBAccountID>
  {
  }

  public abstract class accountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OrganizationBranchReportParameters.subID>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.cashAccountID>
  {
  }

  public abstract class prepaymentInvoiceFullName : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationBranchReportParameters.prepaymentInvoiceFullName>
  {
  }
}
