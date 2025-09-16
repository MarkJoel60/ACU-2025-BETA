// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.OrganizationMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common.EntityInUse;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CR.GraphExtensions;
using PX.Objects.CS.DAC;
using PX.Objects.EP;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.GL.DAC;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GraphExtensions.ExtendBAccount;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

#nullable enable
namespace PX.Objects.CS;

public class OrganizationMaint : 
  OrganizationUnitMaintBase<
  #nullable disable
  OrganizationBAccount, Where<Match<Current<AccessInfo.userName>>>>
{
  public const string canadianCountryCode = "CA";
  public PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.bAccountID, Equal<Current<OrganizationBAccount.bAccountID>>>> OrganizationView;
  public PXSelect<OrganizationBAccount, Where<OrganizationBAccount.bAccountID, Equal<Current<OrganizationBAccount.bAccountID>>>> CurrentBAccount;
  public PXSelectJoin<PX.Objects.GL.Branch, LeftJoin<Roles, On<PX.Objects.GL.Branch.roleName, Equal<Roles.rolename>>>, Where<PX.Objects.GL.Branch.organizationID, Equal<Current<PX.Objects.GL.DAC.Organization.organizationID>>>> BranchesView;
  public PXSelectJoin<PX.Objects.EP.EPEmployee, InnerJoin<BAccount2, On<PX.Objects.EP.EPEmployee.parentBAccountID, Equal<BAccount2.bAccountID>>, InnerJoin<BranchAlias, On<BAccount2.bAccountID, Equal<BranchAlias.bAccountID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.EP.EPEmployee.defContactID>, And<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.EP.EPEmployee.defAddressID>, And<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>>>>>, Where<BranchAlias.organizationID, Equal<Current<PX.Objects.GL.DAC.Organization.organizationID>>>> Employees;
  public PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Current<PX.Objects.CR.BAccount.noteID>>>> Notedocs;
  public PXSelect<CommonSetup> Commonsetup;
  public PXSelect<CurrencyList, Where<CurrencyList.curyID, Equal<Current<PX.Objects.GL.DAC.Organization.baseCuryID>>>> CompanyCurrency;
  public PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Current<CurrencyList.curyID>>>> FinancinalCurrency;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Optional<PX.Objects.CM.Currency.curyInfoID>>>> currencyinfo;
  public PXFilter<OrganizationMaint.State> StateView;
  /// <summary>
  /// The obviously declared view which provides cache for SetVisible function in <see cref="M:PX.Objects.CS.OrganizationMaint.OrganizationBAccount_RowSelected(PX.Data.PXCache,PX.Data.PXRowSelectedEventArgs)" />.
  /// </summary>
  public PXSelect<BranchAlias> BranchAliasView;
  public PXSelect<OrganizationFinYear> OrganizationYear;
  public PXSelect<OrganizationFinPeriod> OrganizationPeriods;
  public FbqlSelect<SelectFromBase<GroupOrganizationLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<
  #nullable enable
  GroupOrganizationLink.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<BqlOperand<
  #nullable enable
  GroupOrganizationLink.organizationID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  GroupOrganizationLink>.View Groups;
  public PXSelect<FABookYear> FaBookYear;
  public PXSelect<TaxPeriod> TaxPeriodView;
  public PXSelect<FABookPeriod> FaBookPeriod;
  public PXAction<OrganizationBAccount> AddLedger;
  public PXAction<OrganizationBAccount> AddBranch;
  public PXAction<OrganizationBAccount> ViewBranch;
  public PXAction<GroupOrganizationLink> ViewGroup;
  public PXAction<GroupOrganizationLink> SetAsPrimary;
  public PXAction<OrganizationBAccount> Activate;
  public PXAction<OrganizationBAccount> Deactivate;
  protected BranchMaint BranchMaint;
  public PXFilter<OrganizationMaint.LedgerCreateParameters> CreateLedgerView;
  public PXAction<OrganizationBAccount> createLedger;
  public OrganizationMaint.OrganizationChangeID ChangeID;

  protected virtual void OrganizationFilter()
  {
    ((PXSelectBase<OrganizationBAccount>) this.BAccount).WhereAnd<Where<BqlOperand<OrganizationBAccount.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.group>>>();
  }

  public static PX.Objects.GL.DAC.Organization FinOrganizationByBAccountID(
    PXGraph graph,
    int? bAccountID)
  {
    return PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectReadonly<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bAccountID
    }));
  }

  public static PX.Objects.GL.DAC.Organization FindOrganizationByID(
    PXGraph graph,
    int? organizationID,
    bool isReadonly = true)
  {
    return OrganizationMaint.FindOrganizationByIDs(graph, organizationID.HasValue ? organizationID.SingleToArray<int?>() : (int?[]) null, isReadonly).SingleOrDefault<PX.Objects.GL.DAC.Organization>();
  }

  public static IEnumerable<PX.Objects.GL.DAC.Organization> FindOrganizationByIDs(
    PXGraph graph,
    int?[] organizationIDs,
    bool isReadonly = true)
  {
    if (organizationIDs == null || !((IEnumerable<int?>) organizationIDs).Any<int?>())
      return (IEnumerable<PX.Objects.GL.DAC.Organization>) new PX.Objects.GL.DAC.Organization[0];
    return isReadonly ? GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectReadonly<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, In<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) organizationIDs
    })) : GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, In<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) organizationIDs
    }));
  }

  public static PX.Objects.GL.DAC.Organization FindOrganizationByCD(
    PXGraph graph,
    string organizationCD,
    bool isReadonly = true)
  {
    return isReadonly ? PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectReadonly<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationCD, Equal<Required<PX.Objects.GL.DAC.Organization.organizationCD>>>>.Config>.Select(graph, new object[1]
    {
      (object) organizationCD
    })) : PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationCD, Equal<Required<PX.Objects.GL.DAC.Organization.organizationCD>>>>.Config>.Select(graph, new object[1]
    {
      (object) organizationCD
    }));
  }

  public static PX.Objects.CR.Contact GetDefaultContact(PXGraph graph, int? organizationID)
  {
    using (IEnumerator<PXResult<PX.Objects.GL.DAC.Organization>> enumerator = PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectJoin<PX.Objects.GL.DAC.Organization, LeftJoin<BAccountR, On<PX.Objects.GL.DAC.Organization.bAccountID, Equal<BAccountR.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<BAccountR.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) organizationID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return PXResult<PX.Objects.GL.DAC.Organization, BAccountR, PX.Objects.CR.Contact>.op_Implicit((PXResult<PX.Objects.GL.DAC.Organization, BAccountR, PX.Objects.CR.Contact>) enumerator.Current);
    }
    return (PX.Objects.CR.Contact) null;
  }

  public static IEnumerable<PX.Objects.CR.Contact> GetDefaultContactForCurrentOrganization(
    PXGraph graph)
  {
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(graph.Accessinfo.BranchID);
    PX.Objects.CR.Contact defaultContact = OrganizationMaint.GetDefaultContact(graph, parentOrganizationId);
    if (defaultContact == null)
      return (IEnumerable<PX.Objects.CR.Contact>) new PX.Objects.CR.Contact[0];
    return (IEnumerable<PX.Objects.CR.Contact>) new PX.Objects.CR.Contact[1]
    {
      defaultContact
    };
  }

  public static string GetCashDiscountBase(PXGraph graph, int? branchID)
  {
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(branchID);
    return OrganizationMaint.FindOrganizationByID(graph, parentOrganizationId)?.CashDiscountBase;
  }

  public static void RedirectTo(int? organizationID)
  {
    OrganizationMaint instance = PXGraph.CreateInstance<OrganizationMaint>();
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) instance, organizationID);
    if (organizationById != null)
    {
      ((PXSelectBase<OrganizationBAccount>) instance.BAccount).Current = PXResultset<OrganizationBAccount>.op_Implicit(((PXSelectBase<OrganizationBAccount>) instance.BAccount).Search<OrganizationBAccount.bAccountID>((object) organizationById.BAccountID, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  public override PXSelectBase<PX.Objects.EP.EPEmployee> EmployeesAccessor
  {
    get => (PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employees;
  }

  public OrganizationMaint()
  {
    this.OrganizationFilter();
    if (!PXAccess.FeatureInstalled<FeaturesSet.multiCompany>())
      ((PXSelectBase) this.BAccount).Cache.AllowInsert = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectReadonly<PX.Objects.GL.DAC.Organization>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) == null;
    ((PXSelectBase) this.BranchesView).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.DAC.Organization.organizationType>(((PXSelectBase) this.OrganizationView).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.branch>());
    PXUIFieldAttribute.SetReadOnly(((PXSelectBase) this.BranchesView).Cache, (object) null, true);
    PXDimensionAttribute.SuppressAutoNumbering<PX.Objects.GL.Branch.branchCD>(((PXSelectBase) this.BranchesView).Cache, true);
    PXDimensionAttribute.SuppressAutoNumbering<PX.Objects.GL.DAC.Organization.organizationCD>(((PXSelectBase) this.OrganizationView).Cache, true);
    ((PXAction) this.ActionsMenu).AddMenuAction((PXAction) this.ChangeID);
    if (EntityInUseHelper.IsEntityInUse<CurrencyInUse>())
      PXUIFieldAttribute.SetEnabled<CurrencyList.decimalPlaces>(((PXSelectBase) this.CompanyCurrency).Cache, (object) null, false);
    this.Init();
  }

  protected virtual void Init()
  {
  }

  protected virtual BranchValidator GetBranchValidator() => new BranchValidator((PXGraph) this);

  public virtual BranchMaint GetBranchMaint()
  {
    if (this.BranchMaint != null)
      return this.BranchMaint;
    this.BranchMaint = PXGraph.CreateInstance<BranchMaint>();
    return this.BranchMaint;
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Group ID")]
  [PXSelector(typeof (Search<PX.Objects.GL.DAC.Organization.organizationID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.GL.DAC.Organization, Current<AccessInfo.userName>>>, And<BqlOperand<PX.Objects.GL.DAC.Organization.organizationType, IBqlString>.IsEqual<OrganizationTypes.group>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.baseCuryID, IBqlString>.IsEqual<BqlField<PX.Objects.GL.DAC.Organization.baseCuryID, IBqlString>.FromCurrent>>>>), new System.Type[] {typeof (PX.Objects.GL.DAC.Organization.organizationCD), typeof (PX.Objects.GL.DAC.Organization.organizationName)}, SubstituteKey = typeof (PX.Objects.GL.DAC.Organization.organizationCD))]
  protected virtual void GroupOrganizationLink_GroupID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  protected virtual void GroupOrganizationLink_OrganizationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void Organization_OrganizationName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Base Currency ID")]
  [PXSelector(typeof (Search<CurrencyList.curyID>), DescriptionField = typeof (CurrencyList.description))]
  protected virtual void Company_BaseCuryID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CM.Currency.curyID>), CacheGlobal = true)]
  protected virtual void Currency_CuryID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RealGainAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RealGainSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RealLossAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RealLossSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RevalGainAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RevalGainSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RevalLossAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RevalLossSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_TranslationGainAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_TranslationGainSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_TranslationLossAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_TranslationLossSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_UnrealizedGainAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_UnrealizedGainSubID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(Required = false)]
  [PXDBInt]
  protected virtual void Currency_UnrealizedLossAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_UnrealizedLossSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RoundingGainAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RoundingGainSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RoundingLossAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(Required = false)]
  protected virtual void Currency_RoundingLossSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "CM")]
  protected virtual void Currency_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "CM")]
  protected virtual void Currency_CuryInfoBaseID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void OrganizationBAccount_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ((PXSelectBase) this.Company).Cache.SetStatus((object) PXResultset<PX.Objects.GL.Company>.op_Implicit(((PXSelectBase<PX.Objects.GL.Company>) this.Company).Select(Array.Empty<object>())), (PXEntryStatus) 1);
    if (!(e.Row is OrganizationBAccount row))
      return;
    ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Insert(new PX.Objects.GL.DAC.Organization()
    {
      OrganizationCD = row.AcctCD,
      OrganizationName = row.AcctName,
      NoteID = row.NoteID
    });
    ((PXSelectBase) this.OrganizationView).Cache.IsDirty = false;
    if (((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current == null)
      return;
    OrganizationBAccount organizationBaccount1 = row;
    OrganizationBAccount organizationBaccount2 = row;
    (string, bool) baccountComplexType = this.GetBAccountComplexType(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current);
    bool? nullable = new bool?(baccountComplexType.Item2);
    string str = baccountComplexType.Item1;
    organizationBaccount1.Type = str;
    organizationBaccount2.IsBranch = nullable;
  }

  protected virtual void OrganizationBAccount_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is OrganizationBAccount row))
      return;
    this.CanBeOrganizationDeleted(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current);
    bool cancelled;
    this.ResetVisibilityRestrictions(row.BAccountID, row.AcctCD, out cancelled);
    if (cancelled)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      this.CheckIfCompanyInGroupsRelatedToCustomerVendor(((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(row.BAccountID)).OrganizationID, out cancelled);
      if (!cancelled)
        return;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void OrganizationBAccount_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    EnumerableExtensions.ForEach<PXResult<GroupOrganizationLink>>((IEnumerable<PXResult<GroupOrganizationLink>>) ((PXSelectBase<GroupOrganizationLink>) this.Groups).Select(Array.Empty<object>()), (Action<PXResult<GroupOrganizationLink>>) (link => ((PXSelectBase<GroupOrganizationLink>) this.Groups).Delete(PXResult<GroupOrganizationLink>.op_Implicit(link))));
    this.DeleteOrganizationFixedAssetCalendar(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current);
    ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Delete(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current);
  }

  protected virtual void OrganizationBAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is OrganizationBAccount row))
    {
      ((PXAction) this.createLedger).SetEnabled(false);
      ((PXAction) this.AddBranch).SetEnabled(false);
      ((PXAction) this.Activate).SetEnabled(false);
      ((PXAction) this.Deactivate).SetEnabled(false);
    }
    else
    {
      if (row.AcctCD?.Trim() != ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current?.OrganizationCD?.Trim())
        ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Select(Array.Empty<object>()));
      PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelectJoin<PX.Objects.GL.Ledger, InnerJoin<OrganizationLedgerLink, On<PX.Objects.GL.Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<PX.Objects.GL.Ledger.balanceType, Equal<LedgerBalanceType.actual>, And<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<PX.Objects.GL.DAC.Organization.organizationID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      bool flag = !GraphHelper.IsPrimaryObjectInserted((PXGraph) this);
      ((PXAction) this.createLedger).SetEnabled(ledger == null & flag);
      ((PXAction) this.AddBranch).SetEnabled(flag);
      OrganizationMaint.State current1 = ((PXSelectBase<OrganizationMaint.State>) this.StateView).Current;
      PX.Objects.GL.DAC.Organization current2 = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current;
      PXUIFieldAttribute.SetVisible<PX.Objects.GL.DAC.Organization.fileTaxesByBranches>(((PXSelectBase) this.OrganizationView).Cache, (object) null, current2 != null && current2.OrganizationType == "Balancing");
      PXUIFieldAttribute.SetVisible<BranchAlias.branchCD>(((PXSelectBase) this.BranchAliasView).Cache, (object) null, current2 != null && current2.OrganizationType != "WithoutBranches");
      if (current2 != null)
      {
        current1.IsBranchTabVisible = new bool?(current2.OrganizationType != "WithoutBranches" && ((PXSelectBase) this.OrganizationView).Cache.GetValueOriginal<PX.Objects.GL.DAC.Organization.organizationType>((object) current2) as string != "WithoutBranches");
        current1.IsDeliverySettingsTabVisible = new bool?(current2.OrganizationType == "WithoutBranches");
        current1.IsGLAccountsTabVisible = new bool?(PXAccess.FeatureInstalled<FeaturesSet.subAccount>() && current2.OrganizationType == "WithoutBranches");
        current1.IsCompanyGroupsVisible = new bool?(PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>() && current2.OrganizationType != "Group");
        ((PXAction) this.Deactivate).SetVisible(current2.OrganizationType != "Group");
        ((PXAction) this.Activate).SetVisible(current2.OrganizationType != "Group");
        ((PXAction) this.Deactivate).SetEnabled(current2.Status == "A" & flag);
        ((PXAction) this.Activate).SetEnabled(current2.Status == "I" & flag);
      }
      else
      {
        current1.IsBranchTabVisible = new bool?(false);
        current1.IsDeliverySettingsTabVisible = new bool?(false);
        current1.IsGLAccountsTabVisible = new bool?(false);
        current1.IsCompanyGroupsVisible = new bool?(false);
      }
    }
  }

  protected virtual void Organization_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    bool flag1 = ((IQueryable<PXResult<PX.Objects.GL.DAC.Organization>>) PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectReadonly<PX.Objects.GL.DAC.Organization>.Config>.SelectWindowed((PXGraph) this, 0, 2, Array.Empty<object>())).Any<PXResult<PX.Objects.GL.DAC.Organization>>();
    bool flag2 = cache.GetStatus(e.Row) == 2 && (!flag1 || PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>());
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.DAC.Organization.baseCuryID>(cache, e.Row, flag2);
  }

  protected virtual void Organization_OrganizationType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.GL.DAC.Organization row = e.Row as PX.Objects.GL.DAC.Organization;
    OrganizationBAccount copy = PXCache<OrganizationBAccount>.CreateCopy(((PXSelectBase<OrganizationBAccount>) this.BAccount).Current);
    if (row == null || copy == null)
      return;
    string valueOriginal = (string) sender.GetValueOriginal<PX.Objects.GL.DAC.Organization.organizationType>((object) row);
    if (row.OrganizationType != valueOriginal)
    {
      OrganizationBAccount organizationBaccount1 = copy;
      OrganizationBAccount organizationBaccount2 = copy;
      (string, bool) baccountComplexType = this.GetBAccountComplexType(row);
      bool? nullable = new bool?(baccountComplexType.Item2);
      string str = baccountComplexType.Item1;
      organizationBaccount1.Type = str;
      organizationBaccount2.IsBranch = nullable;
      ((PXSelectBase<OrganizationBAccount>) this.BAccount).Update(copy);
    }
    if (!(row.OrganizationType != "Balancing"))
      return;
    row.FileTaxesByBranches = new bool?(false);
    row.Reporting1099ByBranches = new bool?(false);
  }

  protected virtual void Organization_OrganizationType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PX.Objects.GL.DAC.Organization row))
      return;
    this.VerifyOrganizationType((string) e.NewValue, row.OrganizationType, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.GL.DAC.Organization, PX.Objects.GL.DAC.Organization.reporting1099ByBranches> e)
  {
    if (!e.Row.Reporting1099ByBranches.GetValueOrDefault())
      return;
    e.Row.Reporting1099 = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.GL.DAC.Organization, PX.Objects.GL.DAC.Organization.organizationLocalizationCode> e)
  {
    if (e.Row == null || !LocalizationServiceExtensions.LocalizationEnabled<FeaturesSet.canadianLocalization>(e.Row.OrganizationLocalizationCode))
      return;
    e.Row.CashDiscountBase = "TA";
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.GL.DAC.Organization, PX.Objects.GL.DAC.Organization.cashDiscountBase> e)
  {
    if (e.Row == null || !LocalizationServiceExtensions.LocalizationEnabled<FeaturesSet.canadianLocalization>(e.Row.OrganizationLocalizationCode) || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.GL.DAC.Organization, PX.Objects.GL.DAC.Organization.cashDiscountBase>, PX.Objects.GL.DAC.Organization, object>) e).NewValue.ToString() == "DA"))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.GL.DAC.Organization, PX.Objects.GL.DAC.Organization.cashDiscountBase>>) e).Cache.RaiseExceptionHandling<PX.Objects.GL.DAC.Organization.cashDiscountBase>((object) e.Row, (object) e.Row.CashDiscountBase, (Exception) new PXSetPropertyException("If Canada is selected in the Localization box on the current form, it is recommended to select Document Amount Less Taxes in the Cash Discount Base box.", (PXErrorLevel) 2));
  }

  protected virtual void _(PX.Data.Events.RowDeleting<GroupOrganizationLink> e)
  {
    if (e.Row == null)
      return;
    bool cancelled;
    this.CheckIfTheLastCompanyInGroup(e.Row.GroupID, out cancelled);
    e.Cancel = cancelled;
  }

  protected virtual void Organization_Active_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PX.Objects.GL.DAC.Organization row))
      return;
    PX.Objects.GL.Branch[] array = BranchMaint.GetChildBranches((PXGraph) this, ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current.OrganizationID).ToArray<PX.Objects.GL.Branch>();
    if (!((IEnumerable<PX.Objects.GL.Branch>) array).Any<PX.Objects.GL.Branch>())
      return;
    this.GetBranchValidator().ValidateActiveField(((IEnumerable<PX.Objects.GL.Branch>) array).Select<PX.Objects.GL.Branch, int?>((Func<PX.Objects.GL.Branch, int?>) (b => b.BranchID)).ToArray<int?>(), (bool?) e.NewValue, row, true);
  }

  protected virtual void Address_CountryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.GL.DAC.Organization current = ((PXCache) GraphHelper.Caches<PX.Objects.GL.DAC.Organization>((PXGraph) this)).Current as PX.Objects.GL.DAC.Organization;
    if (!(e.NewValue.ToString() == "CA") || !PXAccess.FeatureInstalled<FeaturesSet.canadianLocalization>())
      return;
    ((PXCache) GraphHelper.Caches<PX.Objects.GL.DAC.Organization>(sender.Graph)).RaiseExceptionHandling<PX.Objects.GL.DAC.Organization.organizationLocalizationCode>((object) current, (object) current.OrganizationLocalizationCode, (Exception) new PXSetPropertyException("Select Canada in the Localization box to use Canada-specific functionality for this company.", (PXErrorLevel) 2));
  }

  protected virtual void Organization_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Operation != 2 || e.TranStatus != null)
      return;
    PXResult<PX.Objects.GL.DAC.Organization, PX.Objects.CR.BAccount> pxResult = (PXResult<PX.Objects.GL.DAC.Organization, PX.Objects.CR.BAccount>) PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectJoin<PX.Objects.GL.DAC.Organization, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PX.Objects.GL.DAC.Organization.bAccountID>>>, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    if (pxResult != null && PXResult<PX.Objects.GL.DAC.Organization, PX.Objects.CR.BAccount>.op_Implicit(pxResult).OrganizationCD != PXResult<PX.Objects.GL.DAC.Organization, PX.Objects.CR.BAccount>.op_Implicit(pxResult).AcctCD)
      throw new PXException("Cannot generate the next number for the sequence.");
  }

  protected virtual void OrganizationBAccount_AcctName_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is OrganizationBAccount row) || ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current == null || !(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current.OrganizationCD == row.AcctCD))
      return;
    ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current.OrganizationName = row.AcctName;
    ((PXSelectBase) this.OrganizationView).Cache.Update((object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current);
  }

  public virtual void CommonSetup_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (PXDBOperationExt.Command(e.Operation) == 3)
      return;
    PXDefaultAttribute.SetPersistingCheck<CommonSetup.weightUOM>(sender, e.Row, PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CommonSetup.volumeUOM>(sender, e.Row, PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
  }

  public virtual void CommonSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CommonSetup row))
      return;
    bool flag1 = true;
    bool flag2 = true;
    if (!string.IsNullOrEmpty(row.WeightUOM))
      flag1 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.weightUOM, IsNotNull, And<PX.Objects.IN.InventoryItem.baseItemWeight, Greater<decimal0>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) == null;
    if (!string.IsNullOrEmpty(row.VolumeUOM))
      flag2 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.volumeUOM, IsNotNull, And<PX.Objects.IN.InventoryItem.baseItemVolume, Greater<decimal0>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) == null;
    PXUIFieldAttribute.SetEnabled<CommonSetup.weightUOM>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CommonSetup.volumeUOM>(sender, (object) row, flag2);
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
    {
      short? decPlQty = row.DecPlQty;
      Decimal? nullable = decPlQty.HasValue ? new Decimal?((Decimal) decPlQty.GetValueOrDefault()) : new Decimal?();
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      {
        sender.RaiseExceptionHandling<CommonSetup.decPlQty>((object) row, (object) row.DecPlQty, (Exception) new PXSetPropertyException("Setting the quantity decimal precision to 0 with the Multiple Units of Measure feature enabled could result in incorrect UOM conversions.", (PXErrorLevel) 2));
        return;
      }
    }
    string warning = PXUIFieldAttribute.GetWarning<CommonSetup.decPlQty>(sender, (object) row);
    if (string.IsNullOrEmpty(warning) || !(warning == PXLocalizer.Localize("Setting the quantity decimal precision to 0 with the Multiple Units of Measure feature enabled could result in incorrect UOM conversions.")))
      return;
    sender.RaiseExceptionHandling<CommonSetup.decPlQty>((object) row, (object) row.DecPlQty, (Exception) null);
  }

  [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  [PXDBInt(IsKey = true, BqlTable = typeof (PX.Objects.GL.FinPeriods.TableDefinition.FinYear))]
  [PXParent(typeof (Select<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<OrganizationFinYear.organizationID>>>>))]
  protected virtual void OrganizationFinYear_OrganizationID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  [PXDBInt(IsKey = true, BqlTable = typeof (FinPeriod))]
  protected virtual void OrganizationFinPeriod_OrganizationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  protected virtual void _(PX.Data.Events.CacheAttached<FABookYear.organizationID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookPeriod.organizationID> e)
  {
  }

  protected void CreateOrganizationCalendar(PX.Objects.GL.DAC.Organization organization, PXEntryStatus orgStatus)
  {
    if (orgStatus != 2 || PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() || organization.OrganizationType == "Group")
      return;
    PXCache<OrganizationFinYear> pxCache1 = GraphHelper.Caches<OrganizationFinYear>((PXGraph) this);
    PXCache<OrganizationFinPeriod> pxCache2 = GraphHelper.Caches<OrganizationFinPeriod>((PXGraph) this);
    ((PXCache) pxCache1).Clear();
    ((PXCache) pxCache2).Clear();
    IEnumerable<MasterFinYear> masterFinYears = GraphHelper.RowCast<MasterFinYear>((IEnumerable) PXSelectBase<MasterFinYear, PXSelect<MasterFinYear>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    IEnumerable<MasterFinPeriod> masterFinPeriods = GraphHelper.RowCast<MasterFinPeriod>((IEnumerable) PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    foreach (MasterFinYear masterFinYear in masterFinYears)
    {
      if (pxCache1.Insert(new OrganizationFinYear()
      {
        OrganizationID = organization.OrganizationID,
        Year = masterFinYear.Year,
        FinPeriods = masterFinYear.FinPeriods,
        StartMasterFinPeriodID = FinPeriodUtils.GetFirstFinPeriodIDOfYear((IYear) masterFinYear),
        StartDate = masterFinYear.StartDate,
        EndDate = masterFinYear.EndDate
      }) == null)
        throw new PXException("Failed to generate the {0} financial year in the {1} company.", new object[2]
        {
          (object) masterFinYear.Year,
          (object) organization.OrganizationCD?.Trim()
        });
    }
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>();
    foreach (MasterFinPeriod masterFinPeriod in masterFinPeriods)
    {
      OrganizationFinPeriod organizationFinPeriod = pxCache2.Insert(new OrganizationFinPeriod()
      {
        OrganizationID = organization.OrganizationID,
        FinPeriodID = masterFinPeriod.FinPeriodID,
        MasterFinPeriodID = masterFinPeriod.FinPeriodID,
        FinYear = masterFinPeriod.FinYear,
        PeriodNbr = masterFinPeriod.PeriodNbr,
        Custom = masterFinPeriod.Custom,
        DateLocked = masterFinPeriod.DateLocked,
        StartDate = masterFinPeriod.StartDate,
        EndDate = masterFinPeriod.EndDate,
        Status = flag ? masterFinPeriod.Status : "Inactive",
        ARClosed = flag ? masterFinPeriod.ARClosed : new bool?(false),
        APClosed = flag ? masterFinPeriod.APClosed : new bool?(false),
        FAClosed = flag ? masterFinPeriod.FAClosed : new bool?(false),
        CAClosed = flag ? masterFinPeriod.CAClosed : new bool?(false),
        INClosed = flag ? masterFinPeriod.INClosed : new bool?(false),
        Descr = masterFinPeriod.Descr
      });
      PXDBLocalizableStringAttribute.CopyTranslations<MasterFinPeriod.descr, OrganizationFinPeriod.descr>((PXCache) GraphHelper.Caches<MasterFinPeriod>((PXGraph) this), (object) masterFinPeriod, (PXCache) pxCache2, (object) organizationFinPeriod);
      if (organizationFinPeriod == null)
        throw new PXException("Failed to generate the {0} financial period in the {1} company.", new object[2]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForDisplay(masterFinPeriod.FinPeriodID),
          (object) organization.OrganizationCD?.Trim()
        });
    }
    PXCache<FABookYear> pxCache3 = GraphHelper.Caches<FABookYear>((PXGraph) this);
    PXCache<FABookPeriod> pxCache4 = GraphHelper.Caches<FABookPeriod>((PXGraph) this);
    ((PXCache) pxCache3).Clear();
    ((PXCache) pxCache4).Clear();
    foreach (PXResult<FABook> pxResult in PXSelectBase<FABook, PXViewOf<FABook>.BasedOn<SelectFromBase<FABook, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      FABook faBook = PXResult<FABook>.op_Implicit(pxResult);
      IEnumerable<FABookYear> faBookYears = GraphHelper.RowCast<FABookYear>((IEnumerable) PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookYear.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>>.And<BqlOperand<FABookYear.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) faBook.BookID
      }));
      IEnumerable<FABookPeriod> faBookPeriods = GraphHelper.RowCast<FABookPeriod>((IEnumerable) PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>>.And<BqlOperand<FABookPeriod.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) faBook.BookID
      }));
      foreach (FABookYear faBookYear in faBookYears)
      {
        if (pxCache3.Insert(new FABookYear()
        {
          OrganizationID = organization.OrganizationID,
          BookID = faBook.BookID,
          Year = faBookYear.Year,
          FinPeriods = faBookYear.FinPeriods,
          StartMasterFinPeriodID = FinPeriodUtils.GetFirstFinPeriodIDOfYear((IYear) faBookYear),
          StartDate = faBookYear.StartDate,
          EndDate = faBookYear.EndDate
        }) == null)
          throw new PXException("Failed to generate the {0} fixed asset year for the {1} posting book in the {2} company.", new object[3]
          {
            (object) faBookYear.Year,
            (object) faBook.BookCode?.Trim(),
            (object) organization.OrganizationCD?.Trim()
          });
      }
      foreach (FABookPeriod faBookPeriod in faBookPeriods)
      {
        if (pxCache4.Insert(new FABookPeriod()
        {
          OrganizationID = organization.OrganizationID,
          BookID = faBook.BookID,
          FinPeriodID = faBookPeriod.FinPeriodID,
          MasterFinPeriodID = faBookPeriod.FinPeriodID,
          FinYear = faBookPeriod.FinYear,
          PeriodNbr = faBookPeriod.PeriodNbr,
          Custom = faBookPeriod.Custom,
          DateLocked = faBookPeriod.DateLocked,
          StartDate = faBookPeriod.StartDate,
          EndDate = faBookPeriod.EndDate,
          Descr = faBookPeriod.Descr
        }) == null)
          throw new PXException("Failed to generate the {0} fixed asset period for the {1} posting book in the {2} company.", new object[3]
          {
            (object) FinPeriodIDFormattingAttribute.FormatForDisplay(faBookPeriod.FinPeriodID),
            (object) faBook.BookCode?.Trim(),
            (object) organization.OrganizationCD?.Trim()
          });
      }
    }
  }

  protected virtual void DeleteOrganizationFixedAssetCalendar(PX.Objects.GL.DAC.Organization organization)
  {
    PXCache<FABookYear> pxCache = GraphHelper.Caches<FABookYear>((PXGraph) this);
    foreach (PXResult<FABookYear> pxResult in PXSelectBase<FABookYear, PXViewOf<FABookYear>.BasedOn<SelectFromBase<FABookYear, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookYear.organizationID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.OrganizationID
    }))
    {
      FABookYear faBookYear = PXResult<FABookYear>.op_Implicit(pxResult);
      pxCache.Delete(faBookYear);
    }
  }

  public virtual void Persist()
  {
    PX.Objects.GL.DAC.Organization organization1 = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Select(Array.Empty<object>()));
    PXEntryStatus status1 = ((PXSelectBase) this.BAccount).Cache.GetStatus((object) ((PXSelectBase<OrganizationBAccount>) this.BAccount).Current);
    PXEntryStatus status2 = ((PXSelectBase) this.OrganizationView).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current);
    PX.Objects.GL.DAC.Organization original = (PX.Objects.GL.DAC.Organization) ((PXSelectBase) this.OrganizationView).Cache.GetOriginal((object) organization1);
    bool flag1 = !((PXGraph) this).Accessinfo.BranchID.HasValue;
    if (organization1 != null)
    {
      if (!this.IsCompanyBaseCurrencySimilarToActualLedger(organization1.BaseCuryID) || !this.IsCompanyBaseCurrencySimilarToGroup(organization1.BaseCuryID))
        return;
      this.CreateOrganizationCalendar(organization1, status2);
      if (organization1.OrganizationType != "WithoutBranches" && original?.RoleName != organization1.RoleName)
      {
        if (organization1.RoleName != null)
        {
          if (!((PXGraph) this).IsImport)
            ((PXSelectBase<OrganizationBAccount>) this.BAccount).Ask(string.Empty, PXMessages.LocalizeFormatNoPrefix("The {0} access role will be assigned to all branches of the {1} company.", new object[2]
            {
              (object) organization1.RoleName,
              (object) ((PXSelectBase<OrganizationBAccount>) this.BAccount).Current.AcctCD.Trim()
            }), (MessageButtons) 0);
        }
        else if (((PXSelectBase) this.BAccount).Cache.GetStatus((object) ((PXSelectBase<OrganizationBAccount>) this.BAccount).Current) != 2)
        {
          if (!((PXGraph) this).IsImport)
            ((PXSelectBase<OrganizationMaint.State>) this.StateView).Current.ClearAccessRoleOnChildBranches = new bool?(((PXSelectBase<OrganizationBAccount>) this.BAccount).Ask(string.Empty, PXMessages.LocalizeFormatNoPrefix("Remove the access role from the settings of all branches of the {0} company?", new object[1]
            {
              (object) ((PXSelectBase<OrganizationBAccount>) this.BAccount).Current.AcctCD.Trim()
            }), (MessageButtons) 4) == 6);
          else
            ((PXSelectBase<OrganizationMaint.State>) this.StateView).Current.ClearAccessRoleOnChildBranches = new bool?(true);
        }
      }
      this.VerifyOrganizationType(organization1.OrganizationType, original?.OrganizationType, organization1);
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(((PXGraph) this).Accessinfo.BranchID);
      int? organizationId = organization1.OrganizationID;
      if (parentOrganizationId.GetValueOrDefault() == organizationId.GetValueOrDefault() & parentOrganizationId.HasValue == organizationId.HasValue)
      {
        bool? active = organization1.Active;
        bool flag2 = false;
        if (active.GetValueOrDefault() == flag2 & active.HasValue)
        {
          active = original.Active;
          if (active.GetValueOrDefault())
            flag1 = true;
        }
      }
    }
    List<PX.Objects.GL.DAC.Organization> list = ((PXSelectBase) this.OrganizationView).Cache.Deleted.Cast<PX.Objects.GL.DAC.Organization>().ToList<PX.Objects.GL.DAC.Organization>();
    foreach (PX.Objects.GL.DAC.Organization organization2 in list)
      this.CanBeOrganizationDeleted(organization2);
    bool flag3 = false;
    if (!((PXGraph) this).IsImport && !((PXGraph) this).IsExport && !((PXGraph) this).IsContractBasedAPI && !((PXGraph) this).IsMobile)
    {
      flag3 = NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.BAccount).Cache.Inserted) || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.BAccount).Cache.Deleted);
      if (!flag3)
      {
        foreach (object row in ((PXSelectBase) this.BAccount).Cache.Updated)
        {
          if (((PXSelectBase) this.BAccount).Cache.IsValueUpdated<string, OrganizationBAccount.acctName>(row, (IEqualityComparer<string>) StringComparer.CurrentCulture))
          {
            flag3 = true;
            break;
          }
        }
        foreach (object row in ((PXSelectBase) this.OrganizationView).Cache.Updated)
        {
          if (((PXSelectBase) this.OrganizationView).Cache.IsValueUpdated<bool?, PX.Objects.GL.DAC.Organization.overrideThemeVariables>(row) || ((PXSelectBase) this.OrganizationView).Cache.IsValueUpdated<string, PX.Objects.GL.DAC.Organization.backgroundColor>(row, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) || ((PXSelectBase) this.OrganizationView).Cache.IsValueUpdated<string, PX.Objects.GL.DAC.Organization.primaryColor>(row, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            flag3 = true;
          else if (((PXSelectBase) this.OrganizationView).Cache.IsValueUpdated<string, PX.Objects.GL.DAC.Organization.roleName>(row, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
          {
            flag3 = true;
            flag1 = true;
            break;
          }
        }
      }
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Persist();
      try
      {
        this.ProcessOrganizationTypeChanging(status1, original, organization1);
        int? parentOrganizationId = PXAccess.GetParentOrganizationID(((PXGraph) this).Accessinfo.BranchID);
        foreach (PX.Objects.GL.DAC.Organization organization3 in list)
        {
          this.ProcessOrganizationBAccountDeletion(organization3);
          int? nullable = parentOrganizationId;
          int? organizationId = organization3.OrganizationID;
          flag1 = nullable.GetValueOrDefault() == organizationId.GetValueOrDefault() & nullable.HasValue == organizationId.HasValue;
        }
        this.SyncLedgerBaseCuryID();
      }
      finally
      {
        ((PXSelectBase) this.BranchesView).Cache.Clear();
      }
      this.ProcessPublicableToBranchesFieldsChanging(original, organization1, status2);
      transactionScope.Complete();
    }
    if (list.Any<PX.Objects.GL.DAC.Organization>())
    {
      PX.Objects.GL.Company company = PXResultset<PX.Objects.GL.Company>.op_Implicit(((PXSelectBase<PX.Objects.GL.Company>) this.Company).Select(Array.Empty<object>()));
      PXResult<PX.Objects.GL.DAC.Organization> pxResult1 = (PXResult<PX.Objects.GL.DAC.Organization>) null;
      foreach (PXResult<PX.Objects.GL.DAC.Organization> pxResult2 in PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectGroupBy<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.active, Equal<True>>, Aggregate<GroupBy<PX.Objects.GL.DAC.Organization.baseCuryID, Count>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        if (pxResult1 != null)
        {
          int? rowCount1 = ((PXResult) pxResult1).RowCount;
          int? rowCount2 = ((PXResult) pxResult2).RowCount;
          if (!(rowCount1.GetValueOrDefault() < rowCount2.GetValueOrDefault() & rowCount1.HasValue & rowCount2.HasValue))
            continue;
        }
        pxResult1 = pxResult2;
      }
      company.BaseCuryID = pxResult1 == null ? (string) null : PXResult<PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult1).BaseCuryID;
      ((PXSelectBase) this.Company).Cache.SetStatus((object) company, (PXEntryStatus) 1);
      ((PXGraph) this).Persist();
    }
    ((PXGraph) this).SelectTimeStamp();
    ((PXSelectBase) this.OrganizationView).Cache.Clear();
    ((PXSelectBase) this.OrganizationView).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Select(Array.Empty<object>()));
    if (flag1)
    {
      this.UserBranchSlotControl.Reset();
      PXLogin.SetBranchID(PXGraph.CreateInstance<SMAccessPersonalMaint>().GetDefaultBranchId());
    }
    if (flag3)
    {
      PXPageCacheUtils.InvalidateCachedPages();
      PXDatabase.SelectTimeStamp();
      if (!((PXGraph) this).UnattendedMode)
        throw new PXRedirectRequiredException((PXGraph) this, "Organization", true);
    }
    if (flag3 & flag1 && HttpContext.Current == null)
      throw new PXRedirectRequiredException((PXGraph) this, "Organization", true);
  }

  protected void SyncLedgerBaseCuryID()
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      return;
    GeneralLedgerMaint instance = PXGraph.CreateInstance<GeneralLedgerMaint>();
    string baseCuryId = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
    foreach (PXResult<PX.Objects.GL.Ledger> pxResult in ((PXSelectBase<PX.Objects.GL.Ledger>) instance.LedgerRecords).Select(Array.Empty<object>()))
    {
      PX.Objects.GL.Ledger ledger = PXResult<PX.Objects.GL.Ledger>.op_Implicit(pxResult);
      if (ledger.BalanceType == "A" && ledger.BaseCuryID != baseCuryId)
      {
        ledger.BaseCuryID = baseCuryId;
        ((PXSelectBase<PX.Objects.GL.Ledger>) instance.LedgerRecords).Update(ledger);
      }
    }
    ((PXGraph) instance).Actions.PressSave();
  }

  protected virtual void CanBeOrganizationDeleted(PX.Objects.GL.DAC.Organization organization)
  {
    this.CheckBranchesForDeletion(organization);
  }

  protected virtual void CheckBranchesForDeletion(PX.Objects.GL.DAC.Organization organization)
  {
    PX.Objects.GL.Branch[] array = GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.OrganizationID
    })).ToArray<PX.Objects.GL.Branch>();
    if ((string) ((PXSelectBase) this.OrganizationView).Cache.GetValueOriginal<PX.Objects.GL.DAC.Organization.organizationType>((object) organization) == "WithoutBranches")
      new BranchValidator((PXGraph) this).CanBeBranchesDeleted((IReadOnlyCollection<PX.Objects.GL.Branch>) array, true);
    else if (((IEnumerable<PX.Objects.GL.Branch>) array).Any<PX.Objects.GL.Branch>())
      throw new PXException("The {0} company cannot be deleted because the following branch or branches exist for this company: {1}.", new object[2]
      {
        (object) organization.OrganizationCD.Trim(),
        (object) ((ICollection<string>) ((IEnumerable<PX.Objects.GL.Branch>) array).Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (b => b.BranchCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>()
      });
  }

  private void ProcessOrganizationBAccountDeletion(PX.Objects.GL.DAC.Organization organization)
  {
    if (!((string) ((PXSelectBase) this.OrganizationView).Cache.GetValueOriginal<PX.Objects.GL.DAC.Organization.organizationType>((object) organization) == "WithoutBranches"))
      return;
    PX.Objects.GL.Branch branch = BranchMaint.GetChildBranches((PXGraph) this, organization.OrganizationID).SingleOrDefault<PX.Objects.GL.Branch>();
    if (branch == null)
      return;
    int? baccountId1 = branch.BAccountID;
    int? baccountId2 = organization.BAccountID;
    if (!(baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue))
    {
      BranchMaint branchMaint = this.GetBranchMaint();
      ((PXGraph) branchMaint).Clear((PXClearOption) 3);
      ((PXSelectBase<BranchMaint.BranchBAccount>) branchMaint.BAccount).Current = PXResultset<BranchMaint.BranchBAccount>.op_Implicit(((PXSelectBase<BranchMaint.BranchBAccount>) branchMaint.BAccount).Search<BranchMaint.BranchBAccount.bAccountID>((object) branch.BAccountID, Array.Empty<object>()));
      ((PXSelectBase<BranchMaint.BranchBAccount>) branchMaint.BAccount).Delete(((PXSelectBase<BranchMaint.BranchBAccount>) branchMaint.BAccount).Current);
      ((PXGraph) branchMaint).Actions.PressSave();
    }
    else
    {
      ((PXSelectBase<PX.Objects.GL.Branch>) this.BranchesView).Delete(branch);
      ((PXSelectBase) this.BranchesView).Cache.Persist((PXDBOperation) 3);
    }
  }

  private void ProcessOrganizationTypeChanging(
    PXEntryStatus orgBAccountStatus,
    PX.Objects.GL.DAC.Organization origOrganization,
    PX.Objects.GL.DAC.Organization organization)
  {
    if (organization == null)
      return;
    if (orgBAccountStatus == 2 || orgBAccountStatus == 1 && BranchMaint.GetChildBranch((PXGraph) this, organization.OrganizationID) == null)
    {
      if (!(organization.OrganizationType == "WithoutBranches"))
        return;
      this.CreateSingleBranchRecord(organization);
    }
    else
    {
      if (orgBAccountStatus != 1)
        return;
      if (origOrganization?.OrganizationType == "WithoutBranches" && organization.OrganizationType != "WithoutBranches")
      {
        this.MakeBranchSeparate(((PXSelectBase<OrganizationBAccount>) this.BAccount).Current);
      }
      else
      {
        if (!(origOrganization?.OrganizationType != "WithoutBranches") || !(organization.OrganizationType == "WithoutBranches"))
          return;
        this.MergeToSingleBAccount(organization);
      }
    }
  }

  protected virtual void CreateSingleBranchRecord(PX.Objects.GL.DAC.Organization organization)
  {
    PX.Objects.GL.Branch branch = ((PXSelectBase) this.BranchesView).Cache.Update((object) new PX.Objects.GL.Branch()
    {
      OrganizationID = organization.OrganizationID,
      BAccountID = organization.BAccountID,
      BranchCD = organization.OrganizationCD,
      BaseCuryID = organization.BaseCuryID
    }) as PX.Objects.GL.Branch;
    ((PXSelectBase) this.BranchesView).Cache.Persist((PXDBOperation) 2);
  }

  protected virtual void MakeBranchSeparate(OrganizationBAccount orgBAccount)
  {
    PX.Objects.CR.BAccount baccountForBranch = this.CreateSeparateBAccountForBranch(this.GetNewBranchCD(), orgBAccount.AcctName);
    this.MapBranchToNewBAccountAndChangeBranchCD(orgBAccount.AcctCD, baccountForBranch.AcctCD, baccountForBranch.BAccountID);
    this.AssignNewBranchToEmployees(orgBAccount.BAccountID, baccountForBranch.BAccountID, baccountForBranch.AcctCD);
  }

  protected virtual void MergeToSingleBAccount(PX.Objects.GL.DAC.Organization organization)
  {
    PX.Objects.GL.Branch branch = BranchMaint.GetSeparateChildBranches((PXGraph) this, organization).Single<PX.Objects.GL.Branch>();
    this.MapBranchToNewBAccountAndChangeBranchCD(branch.BranchCD, organization.OrganizationCD, organization.BAccountID);
    this.AssignNewBranchToEmployees(branch.BAccountID, organization.BAccountID, organization.OrganizationCD);
    this.DeleteBranchBAccount(branch.BAccountID);
  }

  protected virtual void AssignNewBranchToEmployees(
    int? oldBAccountID,
    int? newBAccountID,
    string branchCD)
  {
    IEnumerable<PX.Objects.EP.EPEmployee> epEmployees = GraphHelper.RowCast<PX.Objects.EP.EPEmployee>((IEnumerable) PXSelectBase<PX.Objects.EP.EPEmployee, PXSelectReadonly<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.parentBAccountID, Equal<Required<PX.Objects.EP.EPEmployee.parentBAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) oldBAccountID
    }));
    EmployeeMaint instance = PXGraph.CreateInstance<EmployeeMaint>();
    PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchCD, Equal<Required<PX.Objects.GL.Branch.branchCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchCD
    }));
    foreach (PX.Objects.EP.EPEmployee epEmployee in epEmployees)
    {
      ((PXGraph) instance).Clear();
      ((PXSelectBase<PX.Objects.EP.EPEmployee>) instance.Employee).Current = epEmployee;
      PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) instance.Address).Select(Array.Empty<object>()));
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Select(Array.Empty<object>()));
      PX.Objects.EP.EPEmployee copy1 = PXCache<PX.Objects.EP.EPEmployee>.CreateCopy(epEmployee);
      PX.Objects.CR.Address copy2 = PXCache<PX.Objects.CR.Address>.CreateCopy(address);
      PX.Objects.CR.Contact copy3 = PXCache<PX.Objects.CR.Contact>.CreateCopy(contact);
      copy1.ParentBAccountID = newBAccountID;
      ((PXSelectBase) instance.Employee).Cache.SetStatus((object) copy1, (PXEntryStatus) 1);
      copy2.BAccountID = newBAccountID;
      copy3.BAccountID = newBAccountID;
      ((PXSelectBase<PX.Objects.CR.Address>) instance.Address).Update(copy2);
      ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Update(copy3);
      ((PXGraph) instance).Actions.PressSave();
    }
  }

  protected virtual PX.Objects.CR.BAccount CreateSeparateBAccountForBranch(
    string acctCD,
    string acctName)
  {
    SeparateBAccountMaint instance = PXGraph.CreateInstance<SeparateBAccountMaint>();
    PX.Objects.CR.BAccount baccount = new PX.Objects.CR.BAccount()
    {
      AcctCD = acctCD,
      AcctName = acctName,
      Type = "CP"
    };
    ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Insert(baccount);
    SeparateBAccountMaint.DefContactAddressExt extension1 = ((PXGraph) instance).GetExtension<SeparateBAccountMaint.DefContactAddressExt>();
    this.CopyGeneralInfoToBranch((PXGraph) instance, ((PXSelectBase<PX.Objects.CR.Contact>) extension1.DefContact).Current, ((PXSelectBase<PX.Objects.CR.Address>) extension1.DefAddress).Current);
    SeparateBAccountMaint.DefLocationExt extension2 = ((PXGraph) instance).GetExtension<SeparateBAccountMaint.DefLocationExt>();
    this.CopyLocationDataToBranch((PXGraph) instance, ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension2.DefLocation).Current, ((PXSelectBase<PX.Objects.CR.Contact>) extension2.DefLocationContact).Current);
    ((PXGraph) instance).Actions.PressSave();
    return ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Current;
  }

  protected virtual void DeleteBranchBAccount(int? baccountID)
  {
    SeparateBAccountMaint instance = PXGraph.CreateInstance<SeparateBAccountMaint>();
    ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Current = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Search<PX.Objects.CR.BAccount.bAccountID>((object) baccountID, Array.Empty<object>()));
    ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Delete(((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Current);
    ((PXGraph) instance).Actions.PressSave();
  }

  protected virtual void MapBranchToNewBAccountAndChangeBranchCD(
    string oldBranchCD,
    string newBranchCD,
    int? newBAccountID)
  {
    OrganizationMaint.SeparateBranchMaint instance = PXGraph.CreateInstance<OrganizationMaint.SeparateBranchMaint>();
    PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchCD, Equal<Required<PX.Objects.GL.Branch.branchCD>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) oldBranchCD
    }));
    branch.BAccountID = newBAccountID;
    ((PXSelectBase<PX.Objects.GL.Branch>) instance.BranchView).Update(branch);
    PXChangeID<PX.Objects.GL.Branch, PX.Objects.GL.Branch.branchCD>.ChangeCD(((PXSelectBase) instance.BranchView).Cache, branch.BranchCD, newBranchCD);
    ((PXGraph) instance).Actions.PressSave();
  }

  public virtual int Persist(System.Type cacheType, PXDBOperation operation)
  {
    int num = ((PXGraph) this).Persist(cacheType, operation);
    if (cacheType == typeof (OrganizationBAccount) && operation == 1)
    {
      foreach (PXResult<NoteDoc, UploadFile> pxResult in PXSelectBase<NoteDoc, PXSelectJoin<NoteDoc, InnerJoin<UploadFile, On<NoteDoc.fileID, Equal<UploadFile.fileID>>>, Where<NoteDoc.noteID, Equal<Current<PX.Objects.CR.BAccount.noteID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        UploadFile uploadFile = PXResult<NoteDoc, UploadFile>.op_Implicit(pxResult);
        if (!uploadFile.IsPublic.GetValueOrDefault())
        {
          ((PXGraph) this).SelectTimeStamp();
          uploadFile.IsPublic = new bool?(true);
          ((PXGraph) this).Caches[typeof (UploadFile)].PersistUpdated((object) (UploadFile) ((PXGraph) this).Caches[typeof (UploadFile)].Update((object) uploadFile));
        }
      }
    }
    return num;
  }

  protected virtual void OrganizationBAccount_OrganizationAcctCD_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if (!(e.Table != (System.Type) null) || e.Operation != 1)
      return;
    e.IsRestriction = false;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual IEnumerable commonsetup()
  {
    PXCache cache = ((PXSelectBase) this.Commonsetup).Cache;
    PXResultset<CommonSetup> pxResultset = PXSelectBase<CommonSetup, PXSelect<CommonSetup>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>());
    if (pxResultset.Count == 0)
    {
      CommonSetup commonSetup = (CommonSetup) cache.Insert((object) new CommonSetup());
      cache.IsDirty = false;
      pxResultset.Add(new PXResult<CommonSetup>(commonSetup));
    }
    else if (cache.Current == null)
      cache.SetStatus((object) PXResultset<CommonSetup>.op_Implicit(pxResultset), (PXEntryStatus) 0);
    return (IEnumerable) pxResultset;
  }

  [PXMergeAttributes]
  [PXDefault(typeof (IsNull<Current<AccessInfo.baseCuryID>, Current<PX.Objects.GL.Company.baseCuryID>>))]
  protected virtual void Organization_BaseCuryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Organization_BaseCuryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    string newValue = (string) e.NewValue;
    if (string.IsNullOrEmpty(newValue))
      return;
    if (!this.IsCompanyBaseCurrencySimilarToGroup(newValue))
      ((CancelEventArgs) e).Cancel = true;
    else if (!this.IsCompanyBaseCurrencySimilarToActualLedger(newValue))
      ((CancelEventArgs) e).Cancel = true;
    else if (!this.IsCompanyBaseCurrencySimilarToActualLedger(newValue))
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PX.Objects.CM.Currency currency1 = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<CurrencyList.curyID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        e.NewValue
      }));
      if ((currency1 != null ? (!currency1.CuryInfoID.HasValue ? 1 : 0) : 1) == 0)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      OrganizationMaint.\u003C\u003Ec__DisplayClass111_0 displayClass1110 = new OrganizationMaint.\u003C\u003Ec__DisplayClass111_0();
      // ISSUE: reference to a compiler-generated field
      displayClass1110.bc = (CurrencyList) PXSelectorAttribute.Select<PX.Objects.GL.DAC.Organization.baseCuryID>(sender, e.Row, e.NewValue);
      // ISSUE: reference to a compiler-generated field
      if (displayClass1110.bc == null)
        return;
      // ISSUE: reference to a compiler-generated field
      displayClass1110.bc.IsActive = new bool?(true);
      // ISSUE: reference to a compiler-generated field
      displayClass1110.bc.IsFinancial = new bool?(true);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      displayClass1110.bc = ((PXSelectBase<CurrencyList>) this.CompanyCurrency).Update(displayClass1110.bc);
      if (currency1 == null)
      {
        // ISSUE: method pointer
        ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.CM.CurrencyInfo.curyID>(new PXFieldDefaulting((object) displayClass1110, __methodptr(\u003COrganization_BaseCuryID_FieldVerifying\u003Eb__0)));
        // ISSUE: reference to a compiler-generated field
        PX.Objects.CM.Currency currency2 = ((PXSelectBase<PX.Objects.CM.Currency>) this.FinancinalCurrency).Insert(new PX.Objects.CM.Currency()
        {
          CuryID = displayClass1110.bc.CuryID
        });
        PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).View.SelectSingleBound(new object[1]
        {
          (object) currency2
        }, Array.Empty<object>());
        currencyInfo1.BaseCalc = new bool?(true);
        currencyInfo1.IsReadOnly = new bool?(true);
        currencyInfo1.BaseCuryID = currency2.CuryID;
        PX.Objects.CM.CurrencyInfo currencyInfo2 = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.currencyinfo).View.SelectSingle(new object[1]
        {
          (object) currency2.CuryInfoBaseID
        });
        currencyInfo2.BaseCalc = new bool?(false);
        currencyInfo2.IsReadOnly = new bool?(true);
        currencyInfo2.BaseCuryID = currency2.CuryID;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        currency1.CuryInfoID = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Insert(new PX.Objects.CM.CurrencyInfo()
        {
          CuryID = displayClass1110.bc.CuryID,
          BaseCuryID = displayClass1110.bc.CuryID,
          BaseCalc = new bool?(true),
          IsReadOnly = new bool?(true)
        }).CuryInfoID;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        currency1.CuryInfoBaseID = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Insert(new PX.Objects.CM.CurrencyInfo()
        {
          CuryID = displayClass1110.bc.CuryID,
          BaseCuryID = displayClass1110.bc.CuryID,
          BaseCalc = new bool?(false),
          IsReadOnly = new bool?(true)
        }).CuryInfoID;
        ((PXSelectBase<PX.Objects.CM.Currency>) this.FinancinalCurrency).Update(currency1);
      }
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void Organization_BaseCuryID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.CompanyCurrency).View.RequestRefresh();
    if (e.Row == null)
      return;
    PX.Objects.GL.DAC.Organization row = (PX.Objects.GL.DAC.Organization) e.Row;
    if (PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, Array.Empty<object>()).Count != 1)
      return;
    PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.GL.Company)];
    PX.Objects.GL.Company current = (PX.Objects.GL.Company) cach.Current;
    current.BaseCuryID = row.BaseCuryID;
    cach.Update((object) current);
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable addLedger(PXAdapter adapter)
  {
    GeneralLedgerMaint.RedirectTo(new int?());
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable addBranch(PXAdapter adapter)
  {
    BranchMaint instance = PXGraph.CreateInstance<BranchMaint>();
    ((PXSelectBase<BranchMaint.BranchBAccount>) instance.BAccount).Insert(new BranchMaint.BranchBAccount()
    {
      OrganizationID = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current.OrganizationID
    });
    ((PXSelectBase) instance.BAccount).Cache.IsDirty = false;
    GraphHelper.Caches<RedirectBranchParameters>((PXGraph) instance).Insert(new RedirectBranchParameters()
    {
      OrganizationID = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current.OrganizationID
    });
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable viewBranch(PXAdapter adapter)
  {
    PX.Objects.GL.Branch current = ((PXSelectBase<PX.Objects.GL.Branch>) this.BranchesView).Current;
    if (current != null)
      BranchMaint.RedirectTo(current.BAccountID);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable viewGroup(PXAdapter adapter)
  {
    GroupOrganizationLink current = ((PXSelectBase<GroupOrganizationLink>) this.Groups).Current;
    if (current != null)
      CompanyGroupsMaint.RedirectTo(current.GroupID);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Set as Primary")]
  [PXButton]
  public IEnumerable setAsPrimary(PXAdapter adapter)
  {
    GroupOrganizationLink current = ((PXSelectBase<GroupOrganizationLink>) this.Groups).Current;
    if (current != null && !current.PrimaryGroup.GetValueOrDefault())
    {
      GroupOrganizationLink organizationLink = ((IEnumerable<GroupOrganizationLink>) ((PXSelectBase<GroupOrganizationLink>) this.Groups).Select<GroupOrganizationLink>(Array.Empty<object>())).Where<GroupOrganizationLink>((Func<GroupOrganizationLink, bool>) (l => l.PrimaryGroup.GetValueOrDefault())).FirstOrDefault<GroupOrganizationLink>();
      if (organizationLink != null)
      {
        organizationLink.PrimaryGroup = new bool?(false);
        ((PXSelectBase) this.Groups).Cache.Update((object) organizationLink);
      }
      current.PrimaryGroup = new bool?(true);
      ((PXSelectBase) this.Groups).Cache.Update((object) current);
      ((PXSelectBase) this.Groups).View.RequestRefresh();
    }
    return adapter.Get();
  }

  protected virtual void VerifyOrganizationType(
    string newOrgType,
    string oldOrgType,
    PX.Objects.GL.DAC.Organization organization)
  {
    if (((PXSelectBase) this.OrganizationView).Cache.GetStatus((object) organization) == 2)
      return;
    string str = (string) null;
    if (oldOrgType != "WithoutBranches" && newOrgType == "WithoutBranches")
    {
      if (BranchMaint.MoreThenOneBranchExist((PXGraph) this, organization.OrganizationID))
        str = "The company type cannot be changed to {0} because more than one branch exists for the company.";
    }
    else if (oldOrgType == "NotBalancing" && newOrgType == "Balancing")
    {
      if (BranchMaint.MoreThenOneBranchExist((PXGraph) this, organization.OrganizationID) && GLUtility.RelatedForOrganizationGLHistoryExists((PXGraph) this, organization.OrganizationID))
        str = "The company type cannot be changed to {0} because more than one branch exists for the company and data has been posted for the company.";
    }
    else
    {
      OrganizationBAccount organizationBaccount = PXResultset<OrganizationBAccount>.op_Implicit(PXSelectBase<OrganizationBAccount, PXViewOf<OrganizationBAccount>.BasedOn<SelectFromBase<OrganizationBAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<OrganizationBAccount.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) organization.BAccountID
      }));
      if (oldOrgType == "WithoutBranches" && oldOrgType != newOrgType && organizationBaccount != null && (organizationBaccount.Type == "VE" || organizationBaccount.Type == "CU" || organizationBaccount.Type == "VC"))
        str = "The company type cannot be changed to {0} because the company has been extended to customer or vendor.";
    }
    if (str != null)
    {
      string localizedLabel = PXStringListAttribute.GetLocalizedLabel<PX.Objects.GL.DAC.Organization.organizationType>(((PXSelectBase) this.OrganizationView).Cache, (object) organization, newOrgType);
      throw new PXSetPropertyException(str, new object[1]
      {
        (object) localizedLabel
      });
    }
  }

  public virtual bool ShouldInvokeOrganizationBranchSync(PXEntryStatus status) => false;

  public virtual void OnOrganizationBranchSync(
    BranchMaint branchMaint,
    PX.Objects.GL.DAC.Organization organization,
    BranchMaint.BranchBAccount branchBaccountCopy)
  {
  }

  protected virtual void ProcessPublicableToBranchesFieldsChanging(
    PX.Objects.GL.DAC.Organization origOrganization,
    PX.Objects.GL.DAC.Organization organization,
    PXEntryStatus status)
  {
    if (organization == null)
      return;
    bool flag1 = origOrganization?.OrganizationType == "WithoutBranches";
    bool flag2 = organization?.OrganizationType == "WithoutBranches" && (origOrganization?.OrganizationType != "WithoutBranches" || status == 2);
    bool? nullable;
    int num1;
    if (origOrganization?.RoleName != organization.RoleName)
    {
      if (organization.RoleName == null)
      {
        nullable = ((PXSelectBase<OrganizationMaint.State>) this.StateView).Current.ClearAccessRoleOnChildBranches;
        if (nullable.GetValueOrDefault())
          goto label_6;
      }
      if (organization.RoleName == null)
      {
        num1 = organization.OrganizationType == "WithoutBranches" ? 1 : 0;
        goto label_8;
      }
label_6:
      num1 = 1;
    }
    else
      num1 = 0;
label_8:
    bool flag3 = num1 != 0;
    nullable = (bool?) origOrganization?.Active;
    bool? active = organization.Active;
    int num2;
    if (!(nullable.GetValueOrDefault() == active.GetValueOrDefault() & nullable.HasValue == active.HasValue))
    {
      active = organization.Active;
      num2 = !active.GetValueOrDefault() ? 1 : (organization.OrganizationType == "WithoutBranches" ? 1 : 0);
    }
    else
      num2 = 0;
    bool flag4 = num2 != 0;
    int? actualLedgerId1 = (int?) origOrganization?.ActualLedgerID;
    int? actualLedgerId2 = organization.ActualLedgerID;
    bool flag5 = !(actualLedgerId1.GetValueOrDefault() == actualLedgerId2.GetValueOrDefault() & actualLedgerId1.HasValue == actualLedgerId2.HasValue);
    bool flag6 = origOrganization?.BaseCuryID != organization.BaseCuryID;
    bool flag7 = organization.OrganizationType == "WithoutBranches" && origOrganization?.CountryID != organization.CountryID;
    bool flag8 = this.ShouldInvokeOrganizationBranchSync(status);
    bool flag9 = origOrganization?.LogoNameReport != organization.LogoNameReport;
    bool flag10 = origOrganization?.LogoName != organization.LogoName;
    if (flag2 | flag3 | flag4 | flag5 | flag7 | flag9 | flag10 | flag8 | flag6)
    {
      if (flag3 | flag2)
        PXUpdate<Set<PX.Objects.GL.Branch.roleName, Required<PX.Objects.GL.Branch.roleName>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.RoleName,
          (object) organization.OrganizationID
        });
      if (flag4 | flag2)
        PXUpdate<Set<PX.Objects.GL.Branch.active, Required<PX.Objects.GL.Branch.active>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.Active,
          (object) organization.OrganizationID
        });
      if (flag5 | flag2)
        PXUpdate<Set<PX.Objects.GL.Branch.ledgerID, Required<PX.Objects.GL.Branch.ledgerID>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.ActualLedgerID,
          (object) organization.OrganizationID
        });
      if (flag6 | flag2)
        PXUpdate<Set<PX.Objects.GL.Branch.baseCuryID, Required<PX.Objects.GL.Branch.baseCuryID>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.BaseCuryID,
          (object) organization.OrganizationID
        });
      if (flag7 | flag2)
        PXUpdate<Set<PX.Objects.GL.Branch.countryID, Required<PX.Objects.GL.Branch.countryID>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.CountryID,
          (object) organization.OrganizationID
        });
      if (flag2 | flag9)
        PXUpdate<Set<PX.Objects.GL.Branch.organizationLogoNameReport, Required<PX.Objects.GL.Branch.organizationLogoNameReport>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.LogoNameReport,
          (object) organization.OrganizationID
        });
      if (flag1 & flag9 | flag2)
        PXUpdate<Set<PX.Objects.GL.Branch.logoNameReport, Required<PX.Objects.GL.Branch.logoNameReport>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.LogoNameReport,
          (object) organization.OrganizationID
        });
      if (flag1 & flag10 | flag2)
        PXUpdate<Set<PX.Objects.GL.Branch.logoName, Required<PX.Objects.GL.Branch.logoName>>, PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Update((PXGraph) this, new object[2]
        {
          (object) organization.LogoName,
          (object) organization.OrganizationID
        });
    }
    ((PXSelectBase) this.BAccount).Cache.Clear();
    ((PXSelectBase) this.BAccount).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase<OrganizationBAccount>) this.BAccount).Current = PXResultset<OrganizationBAccount>.op_Implicit(((PXSelectBase<OrganizationBAccount>) this.BAccount).Search<OrganizationBAccount.acctCD>((object) organization.OrganizationCD, Array.Empty<object>()));
    this.ClearRoleNameInBranches();
    this.RefreshBranch();
    PXAccess.ResetOrganizationBranchSlot();
    ((PXSelectBase<OrganizationMaint.State>) this.StateView).Current.ClearAccessRoleOnChildBranches = new bool?(false);
  }

  protected virtual void CopyGeneralInfoToBranch(
    PXGraph graph,
    PX.Objects.CR.Contact destContact,
    PX.Objects.CR.Address destAddress)
  {
    OrganizationMaint.DefContactAddressExt extension = ((PXGraph) this).GetExtension<OrganizationMaint.DefContactAddressExt>();
    this.CopyContactData(graph, ((PXSelectBase<PX.Objects.CR.Contact>) extension.DefContact).SelectSingle(Array.Empty<object>()), destContact);
    int? addressId = destAddress.AddressID;
    int? baccountId = destAddress.BAccountID;
    Guid? noteId = destAddress.NoteID;
    byte[] tstamp = destAddress.tstamp;
    PXCache<PX.Objects.CR.Address>.RestoreCopy(destAddress, PXCache<PX.Objects.CR.Address>.CreateCopy(PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) extension.DefAddress).Select(Array.Empty<object>()))));
    destAddress.AddressID = addressId;
    destAddress.BAccountID = baccountId;
    destAddress.NoteID = noteId;
    destAddress.tstamp = tstamp;
    GraphHelper.Caches<PX.Objects.CR.Address>(graph).Update(destAddress);
  }

  protected virtual void CopyLocationDataToBranch(
    PXGraph graph,
    PX.Objects.CR.Standalone.Location destLocation,
    PX.Objects.CR.Contact destLocationContact)
  {
    OrganizationMaint.DefLocationExt extension = ((PXGraph) this).GetExtension<OrganizationMaint.DefLocationExt>();
    this.CopyContactData(graph, ((PXSelectBase<PX.Objects.CR.Contact>) extension.DefLocationContact).SelectSingle(Array.Empty<object>()), destLocationContact);
    int? locationId = destLocation.LocationID;
    string locationCd = destLocation.LocationCD;
    Guid? noteId = destLocation.NoteID;
    int? baccountId = destLocation.BAccountID;
    int? defAddressId = destLocation.DefAddressID;
    int? defContactId = destLocation.DefContactID;
    int? accountLocationId1 = destLocation.VAPAccountLocationID;
    int? accountLocationId2 = destLocation.CARAccountLocationID;
    int? vpaymentInfoLocationId = destLocation.VPaymentInfoLocationID;
    byte[] tstamp = destLocation.tstamp;
    PXCache<PX.Objects.CR.Standalone.Location>.RestoreCopy(destLocation, PXCache<PX.Objects.CR.Standalone.Location>.CreateCopy(((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension.DefLocation).Current ?? ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension.DefLocation).SelectSingle(Array.Empty<object>())));
    destLocation.LocationID = locationId;
    destLocation.LocationCD = locationCd;
    destLocation.NoteID = noteId;
    destLocation.BAccountID = baccountId;
    destLocation.DefAddressID = defAddressId;
    destLocation.DefContactID = defContactId;
    destLocation.VAPAccountLocationID = accountLocationId1;
    destLocation.CARAccountLocationID = accountLocationId2;
    destLocation.VPaymentInfoLocationID = vpaymentInfoLocationId;
    destLocation.tstamp = tstamp;
    GraphHelper.Caches<PX.Objects.CR.Standalone.Location>(graph).Update(destLocation);
  }

  protected virtual void CopyContactData(PXGraph graph, PX.Objects.CR.Contact contactSrc, PX.Objects.CR.Contact contactDest)
  {
    int? contactId = contactDest.ContactID;
    int? baccountId = contactDest.BAccountID;
    int? defAddressId = contactDest.DefAddressID;
    Guid? noteId = contactDest.NoteID;
    byte[] tstamp = contactDest.tstamp;
    PXCache<PX.Objects.CR.Contact>.RestoreCopy(contactDest, PXCache<PX.Objects.CR.Contact>.CreateCopy(contactSrc));
    contactDest.ContactID = contactId;
    contactDest.BAccountID = baccountId;
    contactDest.DefAddressID = defAddressId;
    contactDest.NoteID = noteId;
    contactDest.tstamp = tstamp;
    GraphHelper.Caches<PX.Objects.CR.Contact>(graph).Update(contactDest);
  }

  protected virtual string GetBAccountType(PX.Objects.GL.DAC.Organization organization)
  {
    return !(organization.OrganizationType == "WithoutBranches") ? "OR" : "CP";
  }

  protected virtual (string, bool) GetBAccountComplexType(PX.Objects.GL.DAC.Organization organization)
  {
    string baccountType = this.GetBAccountType(organization);
    return (baccountType, baccountType == "CP");
  }

  protected virtual string GetNewBranchCD()
  {
    string newBranchCd = PXMessages.LocalizeFormatNoPrefix("MAIN{0}", new object[1]
    {
      (object) string.Empty
    });
    int num = 0;
    while (true)
    {
      BAccountR baccountR;
      using (new PXReadDeletedScope(false))
        baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectReadonly<BAccountR, Where<BAccountR.acctCD, Equal<Required<BAccountR.acctCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) newBranchCd
        }));
      if (baccountR != null)
      {
        ++num;
        if (num != int.MaxValue)
          newBranchCd = PXMessages.LocalizeFormatNoPrefix("MAIN{0}", new object[1]
          {
            (object) num
          });
        else
          goto label_9;
      }
      else
        break;
    }
    return newBranchCd;
label_9:
    throw new PXException("The default branch name cannot be assigned.");
  }

  protected virtual bool IsCompanyBaseCurrencySimilarToActualLedger(string baseCuryToCompare)
  {
    PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Select(Array.Empty<object>()));
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && organization.BaseCuryID != null)
    {
      foreach (PXResult<OrganizationLedgerLink> pxResult in PXSelectBase<OrganizationLedgerLink, PXViewOf<OrganizationLedgerLink>.BasedOn<SelectFromBase<OrganizationLedgerLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<OrganizationLedgerLink.organizationID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) organization.OrganizationID
      }))
      {
        PX.Objects.GL.Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this, PXResult<OrganizationLedgerLink>.op_Implicit(pxResult).LedgerID);
        if (ledgerById.BalanceType == "A" && ledgerById.BaseCuryID != baseCuryToCompare)
        {
          ((PXSelectBase) this.OrganizationView).Cache.RaiseExceptionHandling<PX.Objects.GL.DAC.Organization.baseCuryID>((object) organization, (object) baseCuryToCompare, (Exception) new PXSetPropertyException("The base currency of the {0} company does not match the {1} ledger currency.", (PXErrorLevel) 4, new object[2]
          {
            (object) organization.OrganizationCD,
            (object) ledgerById.LedgerCD
          }));
          return false;
        }
      }
    }
    return true;
  }

  protected virtual void ResetVisibilityRestrictions(
    int? bAccountID,
    string bAccountCD,
    out bool cancelled)
  {
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AR.Customer.cOrgBAccountID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) bAccountID
    }));
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AP.Vendor.vOrgBAccountID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) bAccountID
    }));
    if (customer != null || vendor != null)
    {
      if (((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Ask(PXMessages.LocalizeFormatNoPrefixNLA("The visibility of some customers or vendors is limited to the {0} group. This restriction will be deleted. To proceed, click OK.", new object[1]
      {
        (object) bAccountCD
      }), (MessageButtons) 1) == 1)
      {
        if (customer != null)
          PXUpdate<Set<PX.Objects.AR.Customer.cOrgBAccountID, Zero>, PX.Objects.AR.Customer, Where<BqlOperand<PX.Objects.AR.Customer.cOrgBAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Update((PXGraph) this, new object[1]
          {
            (object) bAccountID
          });
        if (vendor != null)
          PXUpdate<Set<PX.Objects.AP.Vendor.vOrgBAccountID, Zero>, PX.Objects.AP.Vendor, Where<BqlOperand<PX.Objects.AP.Vendor.vOrgBAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Update((PXGraph) this, new object[1]
          {
            (object) bAccountID
          });
      }
      else
        cancelled = true;
    }
    cancelled = false;
  }

  protected virtual void CheckIfTheLastCompanyInGroup(int? groupID, out bool cancelled)
  {
    cancelled = false;
    if (!groupID.HasValue)
      return;
    PXAccess.MasterCollection.Organization organizationById = PXAccess.GetOrganizationByID(groupID);
    PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(((PXSelectBase<OrganizationBAccount>) this.BAccount).Current.BAccountID);
    if (organizationById == null || organizationByBaccountId == null)
      return;
    if (PXResultset<GroupOrganizationLink>.op_Implicit(PXSelectBase<GroupOrganizationLink, PXViewOf<GroupOrganizationLink>.BasedOn<SelectFromBase<GroupOrganizationLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<GroupOrganizationLink.groupID, Equal<P.AsInt>>>>>.And<BqlOperand<GroupOrganizationLink.organizationID, IBqlInt>.IsNotEqual<P.AsInt>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) ((PXAccess.Organization) organizationById).OrganizationID,
      (object) ((PXAccess.Organization) organizationByBaccountId).OrganizationID
    })) != null)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AR.Customer.cOrgBAccountID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXAccess.Organization) organizationById).BAccountID
    }));
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AP.Vendor.vOrgBAccountID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXAccess.Organization) organizationById).BAccountID
    }));
    if (customer == null && vendor == null)
      return;
    if (((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Ask(PXMessages.LocalizeFormatNoPrefixNLA("The usage of some customers or vendors is limited to the {0} group. The deletion of all companies from the group will make these customers or vendors inaccessible for users. To proceed, click OK.", new object[1]
    {
      (object) ((PXAccess.Organization) organizationById).OrganizationCD
    }), (MessageButtons) 1) != 2)
      return;
    cancelled = true;
  }

  protected virtual void CheckIfCompanyInGroupsRelatedToCustomerVendor(
    int? organizationID,
    out bool cancelled)
  {
    cancelled = false;
    if (!organizationID.HasValue)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.cOrgBAccountID>>>, FbqlJoins.Inner<GroupOrganizationLink>.On<BqlOperand<GroupOrganizationLink.groupID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<BqlOperand<GroupOrganizationLink.organizationID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) organizationID
    }));
    PXResultset<PX.Objects.AP.Vendor> pxResultset;
    if (customer == null)
      pxResultset = PXSelectBase<PX.Objects.AP.Vendor, PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.vOrgBAccountID>>>, FbqlJoins.Inner<GroupOrganizationLink>.On<BqlOperand<GroupOrganizationLink.groupID, IBqlInt>.IsEqual<PX.Objects.GL.DAC.Organization.organizationID>>>>.Where<BqlOperand<GroupOrganizationLink.organizationID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) organizationID
      });
    else
      pxResultset = (PXResultset<PX.Objects.AP.Vendor>) null;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(pxResultset);
    if (customer == null && vendor == null)
      return;
    if (((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Ask(PXMessages.LocalizeFormatNoPrefixNLA("The usage of some customers or vendors is limited to a group or groups where the {0} company is the only member. The deletion of the company will make these customers or vendors inaccessible for users. To proceed, click OK.", new object[1]
    {
      (object) ((PXAccess.Organization) PXAccess.GetOrganizationByID(organizationID)).OrganizationCD
    }), (MessageButtons) 1) != 2)
      return;
    cancelled = true;
  }

  protected override int? BaccountIDForNewEmployee()
  {
    if (((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current == null)
      return new int?();
    string valueOriginal = (string) ((PXSelectBase) this.OrganizationView).Cache.GetValueOriginal<PX.Objects.GL.DAC.Organization.organizationType>((object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current);
    return !(valueOriginal == "WithoutBranches") || !(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current.OrganizationType == valueOriginal) ? new int?() : ((PXSelectBase<OrganizationBAccount>) this.BAccount).Current.BAccountID;
  }

  protected virtual bool IsCompanyBaseCurrencySimilarToGroup(string baseCuryToCompare)
  {
    PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Select(Array.Empty<object>()));
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && organization.BaseCuryID != null)
    {
      foreach (PXResult<GroupOrganizationLink> pxResult in ((PXSelectBase<GroupOrganizationLink>) this.Groups).Select(Array.Empty<object>()))
      {
        PXAccess.MasterCollection.Organization organizationById = PXAccess.GetOrganizationByID(PXResult<GroupOrganizationLink>.op_Implicit(pxResult).GroupID);
        if (((PXAccess.Organization) organizationById).BaseCuryID != baseCuryToCompare)
        {
          ((PXSelectBase) this.OrganizationView).Cache.RaiseExceptionHandling<PX.Objects.GL.DAC.Organization.baseCuryID>((object) organization, (object) baseCuryToCompare, (Exception) new PXSetPropertyException("The base currency of the {0} company does not match the {1} group currency.", (PXErrorLevel) 4, new object[2]
          {
            (object) organization.OrganizationCD,
            (object) ((PXAccess.Organization) organizationById).OrganizationCD
          }));
          return false;
        }
      }
    }
    return true;
  }

  [PXUIField]
  [PXButton(Category = "Other", DisplayOnMainToolbar = true)]
  public virtual IEnumerable CreateLedger(PXAdapter adapter)
  {
    // ISSUE: method pointer
    if (((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current == null || !this.CreateLedgerView.AskExtFullyValid(new PXView.InitializePanel((object) this, __methodptr(\u003CCreateLedger\u003Eb__137_0)), (DialogAnswerType) 1, true))
      return adapter.Get();
    ((PXAction) this.Save).Press();
    OrganizationMaint.CreateLeadgerProc(((PXSelectBase<OrganizationMaint.LedgerCreateParameters>) this.CreateLedgerView).Current);
    throw new PXRefreshException();
  }

  protected virtual void LedgerCreateParameters_LedgerCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelectReadonly<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerCD, Equal<Required<PX.Objects.GL.Ledger.ledgerCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })) != null)
      throw new PXSetPropertyException("The {0} ledger already exists.", new object[1]
      {
        e.NewValue
      });
  }

  public static void CreateLeadgerProc(
    OrganizationMaint.LedgerCreateParameters ledgerParamters)
  {
    GeneralLedgerMaint instance = PXGraph.CreateInstance<GeneralLedgerMaint>();
    PX.Objects.GL.Ledger ledger = ((PXSelectBase<PX.Objects.GL.Ledger>) instance.LedgerRecords).Insert(new PX.Objects.GL.Ledger()
    {
      LedgerCD = ledgerParamters.LedgerCD,
      BalanceType = "A",
      Descr = ledgerParamters.Descr,
      BaseCuryID = ledgerParamters.BaseCuryID
    });
    PXDBLocalizableStringAttribute.CopyTranslations<OrganizationMaint.LedgerCreateParameters.descr, PX.Objects.GL.Ledger.descr>((PXGraph) instance, (object) ledgerParamters, (object) ledger);
    GraphHelper.Caches<OrganizationLedgerLink>((PXGraph) instance).Insert(new OrganizationLedgerLink()
    {
      OrganizationID = ledgerParamters.OrganizationID
    });
    ((PXAction) instance.Save).Press();
  }

  [PXUIField]
  [PXButton(CommitChanges = true, Category = "Company Management")]
  public virtual IEnumerable activate(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    OrganizationMaint.\u003C\u003Ec__DisplayClass152_0 displayClass1520 = new OrganizationMaint.\u003C\u003Ec__DisplayClass152_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1520.org = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current;
    // ISSUE: reference to a compiler-generated field
    if (displayClass1520.org == null)
      adapter.Get();
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1520, __methodptr(\u003Cactivate\u003Eb__0)));
    return adapter.Get();
  }

  public static void ActivateCompanyProcess(PX.Objects.GL.DAC.Organization organization)
  {
    OrganizationMaint instance = PXGraph.CreateInstance<OrganizationMaint>();
    ((PXSelectBase<PX.Objects.GL.DAC.Organization>) instance.OrganizationView).Current = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) instance.OrganizationView).Search<PX.Objects.GL.DAC.Organization.organizationID>((object) organization.OrganizationID, Array.Empty<object>()));
    ((PXSelectBase<OrganizationBAccount>) instance.BAccount).Current = PXResultset<OrganizationBAccount>.op_Implicit(((PXSelectBase<OrganizationBAccount>) instance.BAccount).Search<OrganizationBAccount.bAccountID>((object) organization.BAccountID, Array.Empty<object>()));
    if (((PXSelectBase<PX.Objects.GL.DAC.Organization>) instance.OrganizationView).Current == null || ((PXSelectBase<OrganizationBAccount>) instance.BAccount).Current == null)
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      organization.Status = "A";
      organization.Active = new bool?(true);
      organization = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) instance.OrganizationView).Update(organization);
      if (organization != null && !PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
      {
        instance.SynchronizeMasterAndOrganizationPeriods(organization);
        instance.SynchronizeMasterAndOrganizationFAPeriods(organization);
      }
      ((PXAction) instance.Save).Press();
      transactionScope.Complete();
    }
  }

  protected virtual void SynchronizeMasterAndOrganizationPeriods(PX.Objects.GL.DAC.Organization organization)
  {
    PXCache<MasterFinPeriod> pxCache = GraphHelper.Caches<MasterFinPeriod>((PXGraph) this);
    foreach (PXResult<MasterFinYear> pxResult in PXSelectBase<MasterFinYear, PXSelectJoin<MasterFinYear, LeftJoin<OrganizationFinYear, On<MasterFinYear.year, Equal<OrganizationFinYear.year>, And<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>>>>, Where<OrganizationFinYear.year, IsNull>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.OrganizationID
    }))
    {
      MasterFinYear masterFinYear = PXResult<MasterFinYear>.op_Implicit(pxResult);
      if (((PXSelectBase<OrganizationFinYear>) this.OrganizationYear).Insert(new OrganizationFinYear()
      {
        OrganizationID = organization.OrganizationID,
        Year = masterFinYear.Year,
        FinPeriods = masterFinYear.FinPeriods,
        StartMasterFinPeriodID = FinPeriodUtils.GetFirstFinPeriodIDOfYear((IYear) masterFinYear),
        StartDate = masterFinYear.StartDate,
        EndDate = masterFinYear.EndDate
      }) == null)
        throw new PXException("The {0} financial year cannot be created for the {1} company.", new object[2]
        {
          (object) masterFinYear.Year,
          (object) organization.OrganizationCD
        });
    }
    IFinPeriodRepository service = ((PXGraph) this).GetService<IFinPeriodRepository>();
    FinPeriod firstPeriod = service.FindFirstPeriod(organization.OrganizationID);
    FinPeriod lastPeriod = service.FindLastPeriod(organization.OrganizationID);
    FinPeriod finPeriod = (FinPeriod) null;
    foreach (PXResult<MasterFinPeriod> pxResult in PXSelectBase<MasterFinPeriod, PXSelectJoin<MasterFinPeriod, LeftJoin<OrganizationFinPeriod, On<MasterFinPeriod.finPeriodID, Equal<OrganizationFinPeriod.masterFinPeriodID>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>, Where<OrganizationFinPeriod.finPeriodID, IsNull>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.OrganizationID
    }))
    {
      MasterFinPeriod masterFinPeriod = PXResult<MasterFinPeriod>.op_Implicit(pxResult);
      OrganizationFinPeriod organizationFinPeriod1 = ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationPeriods).Insert(new OrganizationFinPeriod()
      {
        OrganizationID = organization.OrganizationID,
        FinPeriodID = masterFinPeriod.FinPeriodID,
        MasterFinPeriodID = masterFinPeriod.FinPeriodID,
        FinYear = masterFinPeriod.FinYear,
        PeriodNbr = masterFinPeriod.PeriodNbr,
        Custom = masterFinPeriod.Custom,
        DateLocked = masterFinPeriod.DateLocked,
        StartDate = masterFinPeriod.StartDate,
        EndDate = masterFinPeriod.EndDate,
        Descr = masterFinPeriod.Descr
      });
      if (organizationFinPeriod1 == null)
        throw new PXException("The {0} financial period cannot be created for the {1} company.", new object[2]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(organizationFinPeriod1.FinPeriodID),
          (object) organization.OrganizationCD
        });
      PXDBLocalizableStringAttribute.CopyTranslations<MasterFinPeriod.descr, OrganizationFinPeriod.descr>((PXCache) pxCache, (object) masterFinPeriod, ((PXSelectBase) this.OrganizationPeriods).Cache, (object) organizationFinPeriod1);
      if (PXAccess.FeatureInstalled<FeaturesSet.centralizedPeriodsManagement>())
      {
        organizationFinPeriod1.Status = masterFinPeriod.Status;
        organizationFinPeriod1.ARClosed = masterFinPeriod.ARClosed;
        organizationFinPeriod1.APClosed = masterFinPeriod.APClosed;
        organizationFinPeriod1.FAClosed = masterFinPeriod.FAClosed;
        organizationFinPeriod1.CAClosed = masterFinPeriod.CAClosed;
        organizationFinPeriod1.INClosed = masterFinPeriod.INClosed;
        organizationFinPeriod1.PRClosed = masterFinPeriod.PRClosed;
      }
      else if (lastPeriod == null || string.CompareOrdinal(organizationFinPeriod1.FinPeriodID, lastPeriod?.FinPeriodID) > 0)
      {
        organizationFinPeriod1.Status = "Inactive";
        organizationFinPeriod1.ARClosed = new bool?(false);
        organizationFinPeriod1.APClosed = new bool?(false);
        organizationFinPeriod1.FAClosed = new bool?(false);
        organizationFinPeriod1.CAClosed = new bool?(false);
        organizationFinPeriod1.INClosed = new bool?(false);
        organizationFinPeriod1.PRClosed = new bool?(false);
      }
      else if (string.CompareOrdinal(organizationFinPeriod1.FinPeriodID, firstPeriod.FinPeriodID) < 0)
      {
        organizationFinPeriod1.Status = firstPeriod.Status;
        organizationFinPeriod1.ARClosed = firstPeriod.ARClosed;
        organizationFinPeriod1.APClosed = firstPeriod.APClosed;
        organizationFinPeriod1.FAClosed = firstPeriod.FAClosed;
        organizationFinPeriod1.CAClosed = firstPeriod.CAClosed;
        organizationFinPeriod1.INClosed = firstPeriod.INClosed;
        organizationFinPeriod1.PRClosed = firstPeriod.PRClosed;
      }
      else
      {
        finPeriod = finPeriod ?? service.FindPrevPeriod(organization.OrganizationID, organizationFinPeriod1.FinPeriodID);
        organizationFinPeriod1.Status = finPeriod.Status;
        organizationFinPeriod1.ARClosed = finPeriod.ARClosed;
        organizationFinPeriod1.APClosed = finPeriod.APClosed;
        organizationFinPeriod1.FAClosed = finPeriod.FAClosed;
        organizationFinPeriod1.CAClosed = finPeriod.CAClosed;
        organizationFinPeriod1.INClosed = finPeriod.INClosed;
        organizationFinPeriod1.PRClosed = finPeriod.PRClosed;
      }
      OrganizationFinPeriod organizationFinPeriod2 = ((PXSelectBase<OrganizationFinPeriod>) this.OrganizationPeriods).Update(organizationFinPeriod1);
      if (organizationFinPeriod2 == null)
        throw new PXException("The {0} financial period cannot be created for the {1} company.", new object[2]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(organizationFinPeriod2.FinPeriodID),
          (object) organization.OrganizationCD
        });
    }
  }

  protected virtual void SynchronizeMasterAndOrganizationFAPeriods(PX.Objects.GL.DAC.Organization organization)
  {
    foreach (PXResult<FABookYear> pxResult in PXSelectBase<FABookYear, PXSelectJoin<FABookYear, LeftJoin<FABook, On<FABook.bookID, Equal<FABookYear.bookID>>, LeftJoin<FABookYearAlias, On<FABookYear.year, Equal<FABookYearAlias.year>, And<FABookYear.bookID, Equal<FABookYearAlias.bookID>, And<FABookYearAlias.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>>>, Where<FABookYear.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FABook.updateGL, Equal<True>, And<FABookYearAlias.year, IsNull>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.OrganizationID
    }))
    {
      FABookYear faBookYear = PXResult<FABookYear>.op_Implicit(pxResult);
      if (((PXSelectBase<FABookYear>) this.FaBookYear).Insert(new FABookYear()
      {
        OrganizationID = organization.OrganizationID,
        Year = faBookYear.Year,
        BookID = faBookYear.BookID,
        FinPeriods = faBookYear.FinPeriods,
        StartDate = faBookYear.StartDate,
        EndDate = faBookYear.EndDate,
        StartMasterFinPeriodID = faBookYear.StartMasterFinPeriodID ?? ((PXGraph) this).GetService<IFinPeriodRepository>().FindNearestOrganizationFinYear(organization.OrganizationID, faBookYear.Year).StartMasterFinPeriodID
      }) == null)
        throw new PXException("The {0} year in the posting book cannot be created for the {1} company.", new object[2]
        {
          (object) faBookYear.Year,
          (object) organization.OrganizationCD
        });
    }
    foreach (PXResult<FABookPeriod> pxResult in PXSelectBase<FABookPeriod, PXSelectJoin<FABookPeriod, LeftJoin<FABook, On<FABook.bookID, Equal<FABookPeriod.bookID>>, LeftJoin<FABookPeriodAlias, On<FABookPeriod.finPeriodID, Equal<FABookPeriodAlias.finPeriodID>, And<FABookPeriod.bookID, Equal<FABookPeriodAlias.bookID>, And<FABookPeriodAlias.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>>>, Where<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>, And<FABook.updateGL, Equal<True>, And<FABookPeriodAlias.finPeriodID, IsNull>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.OrganizationID
    }))
    {
      FABookPeriod faBookPeriod1 = PXResult<FABookPeriod>.op_Implicit(pxResult);
      FABookPeriod faBookPeriod2 = ((PXSelectBase<FABookPeriod>) this.FaBookPeriod).Insert(new FABookPeriod()
      {
        OrganizationID = organization.OrganizationID,
        BookID = faBookPeriod1.BookID,
        FinPeriodID = faBookPeriod1.FinPeriodID,
        MasterFinPeriodID = faBookPeriod1.FinPeriodID,
        FinYear = faBookPeriod1.FinYear,
        PeriodNbr = faBookPeriod1.PeriodNbr,
        StartDate = faBookPeriod1.StartDate,
        EndDate = faBookPeriod1.EndDate,
        Active = faBookPeriod1.Active,
        Descr = faBookPeriod1.Descr
      });
      PXDBLocalizableStringAttribute.CopyTranslations<FABookPeriod.descr, FABookPeriod.descr>(((PXSelectBase) this.FaBookPeriod).Cache, (object) faBookPeriod1, ((PXSelectBase) this.FaBookPeriod).Cache, (object) faBookPeriod2);
      if (faBookPeriod2 == null)
        throw new PXException("The {0} period in the posting book cannot be created for the {1} company.", new object[2]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(faBookPeriod2.FinPeriodID),
          (object) organization.OrganizationCD
        });
    }
  }

  [PXUIField]
  [PXButton(CommitChanges = true, Category = "Company Management")]
  public virtual IEnumerable deactivate(PXAdapter adapter)
  {
    PX.Objects.GL.DAC.Organization current = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Current;
    if (current == null)
      adapter.Get();
    ((PXAction) this.Save).Press();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      current.Status = "I";
      current.Active = new bool?(false);
      ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.OrganizationView).Update(current);
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
    return adapter.Get();
  }

  protected class SeparateBranchMaint : PXGraph<BranchMaint>
  {
    public PXSelect<PX.Objects.GL.Branch> BranchView;
  }

  public class OrganizationChangeID : 
    PXChangeBAccountID<OrganizationBAccount, OrganizationBAccount.acctCD>
  {
    public OrganizationChangeID(PXGraph graph, string name)
      : base(graph, name)
    {
    }

    public OrganizationChangeID(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }

    protected virtual void Initialize()
    {
      this.DuplicatedKeyMessage = "The {0} identifier is already used for the following records: {1}.";
      // ISSUE: method pointer
      ((PXAction) this)._Graph.FieldUpdated.AddHandler<OrganizationBAccount.acctCD>(new PXFieldUpdated((object) this, __methodptr(\u003CInitialize\u003Eb__2_0)));
      base.Initialize();
    }

    [PXButton(CommitChanges = true, Category = "Other")]
    [PXUIField]
    protected virtual IEnumerable Handler(PXAdapter adapter) => base.Handler(adapter);
  }

  [Serializable]
  public class State : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    public bool? ClearAccessRoleOnChildBranches { get; set; }

    [PXBool]
    public bool? IsBranchTabVisible { get; set; }

    [PXBool]
    public bool? IsDeliverySettingsTabVisible { get; set; }

    [PXBool]
    public bool? IsGLAccountsTabVisible { get; set; }

    [PXBool]
    public bool? IsCompanyGroupsVisible { get; set; }

    public abstract class clearAccessRoleOnChildBranches : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OrganizationMaint.State.clearAccessRoleOnChildBranches>
    {
    }

    public abstract class isBranchTabVisible : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OrganizationMaint.State.isBranchTabVisible>
    {
    }

    public abstract class isDeliverySettingsTabVisible : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OrganizationMaint.State.isDeliverySettingsTabVisible>
    {
    }

    public abstract class isGLAccountsTabVisible : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OrganizationMaint.State.isGLAccountsTabVisible>
    {
    }

    public abstract class isCompanyGroupsVisible : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OrganizationMaint.State.isCompanyGroupsVisible>
    {
    }
  }

  [Serializable]
  public class LedgerCreateParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    public virtual int? OrganizationID { get; set; }

    [PXString(10, IsUnicode = true, InputMask = ">CCCCCCCCCC")]
    [PXDefault]
    [PXUIField(DisplayName = "Ledger ID")]
    public virtual string LedgerCD { get; set; }

    /// <summary>
    /// Base <see cref="T:PX.Objects.CM.Currency" /> of the Ledger.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </value>
    [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXDefault]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string BaseCuryID { get; set; }

    [PXDBLocalizableString(60, IsUnicode = true, NonDB = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Description")]
    public virtual string Descr { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      OrganizationMaint.LedgerCreateParameters.organizationID>
    {
    }

    public abstract class ledgerCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OrganizationMaint.LedgerCreateParameters.ledgerCD>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OrganizationMaint.LedgerCreateParameters.baseCuryID>
    {
    }

    public abstract class descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OrganizationMaint.LedgerCreateParameters.descr>
    {
    }
  }

  /// <exclude />
  public class DefContactAddressExt : 
    PX.Objects.CR.Extensions.DefContactAddressExt<OrganizationMaint, OrganizationBAccount, OrganizationBAccount.acctName>.WithPersistentAddressValidation
  {
    public override void Initialize()
    {
      base.Initialize();
      ((PXAction) this.Base.ActionsMenu).AddMenuAction((PXAction) this.ValidateAddresses);
    }
  }

  /// <exclude />
  public class DefLocationExt : 
    PX.Objects.CR.Extensions.DefLocationExt<OrganizationMaint, OrganizationMaint.DefContactAddressExt, OrganizationMaint.LocationDetailsExt, OrganizationBAccount, OrganizationBAccount.bAccountID, OrganizationBAccount.defLocationID>.WithUIExtension
  {
    [PXUIField]
    [PXButton(DisplayOnMainToolbar = false)]
    public void SetDefaultLocation()
    {
      OrganizationBAccount current = ((PXSelectBase<OrganizationBAccount>) this.Base.BAccount).Current;
      if (((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Current == null || current == null)
        return;
      int? locationId = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Current.LocationID;
      int? defLocationId = current.DefLocationID;
      if (locationId.GetValueOrDefault() == defLocationId.GetValueOrDefault() & locationId.HasValue == defLocationId.HasValue)
        return;
      current.DefLocationID = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Current.LocationID;
      ((PXSelectBase<OrganizationBAccount>) this.Base.BAccount).Update(current);
    }

    [PXDBInt]
    [PXDBChildIdentity(typeof (PX.Objects.CR.Standalone.Location.locationID))]
    [PXUIField]
    [PXSelector(typeof (Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<OrganizationBAccount.bAccountID>>>>), DescriptionField = typeof (PX.Objects.CR.Standalone.Location.locationCD), DirtyRead = true)]
    protected override void _(
      PX.Data.Events.CacheAttached<OrganizationBAccount.defLocationID> e)
    {
    }
  }

  /// <exclude />
  public class LocationDetailsExt : 
    PX.Objects.CR.Extensions.LocationDetailsExt<OrganizationMaint, OrganizationBAccount, OrganizationBAccount.bAccountID>
  {
    protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.GL.Branch> e)
    {
      if (e.TranStatus != null || (e.Operation & 3) != 3)
        return;
      PXUpdate<Set<PX.Objects.CR.Standalone.Location.cBranchID, Null>, PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.cBranchID, Equal<Required<PX.Objects.CR.Standalone.Location.cBranchID>>>>.Update((PXGraph) this.Base, new object[1]
      {
        (object) e.Row.BranchID
      });
      PXUpdate<Set<PX.Objects.CR.Standalone.Location.vBranchID, Null>, PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.vBranchID, Equal<Required<PX.Objects.CR.Standalone.Location.vBranchID>>>>.Update((PXGraph) this.Base, new object[1]
      {
        (object) e.Row.BranchID
      });
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location> e)
    {
      base._(e);
      if (e.Row == null)
        return;
      PX.Objects.GL.DAC.Organization current = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Current;
      PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location>>) e).Cache, (object) null, current?.OrganizationType == "WithoutBranches");
    }
  }

  /// <exclude />
  public class OrganizationMaintAddressLookupExtension : 
    AddressLookupExtension<OrganizationMaint, OrganizationBAccount, PX.Objects.CR.Address>
  {
    protected override string AddressView => "DefAddress";

    protected override string ViewOnMap => "ViewMainOnMap";
  }

  /// <exclude />
  public class OrganizationMaintDefLocationAddressLookupExtension : 
    AddressLookupExtension<OrganizationMaint, OrganizationBAccount, PX.Objects.CR.Address>
  {
    protected override string AddressView => "DefLocationAddress";

    protected override string ViewOnMap => "ViewDefLocationAddressOnMap";
  }

  /// <exclude />
  public class OrganizationLedgerLinkMaint : 
    OrganizationLedgerLinkMaintBase<OrganizationMaint, OrganizationBAccount>
  {
    public PXAction<OrganizationBAccount> ViewLedger;
    public PXSelectJoin<OrganizationLedgerLink, LeftJoin<PX.Objects.GL.Ledger, On<OrganizationLedgerLink.ledgerID, Equal<PX.Objects.GL.Ledger.ledgerID>>>, Where<OrganizationLedgerLink.organizationID, Equal<Current<PX.Objects.GL.DAC.Organization.organizationID>>>> OrganizationLedgerLinkWithLedgerSelect;
    public PXSelect<PX.Objects.GL.Ledger> LedgerView;

    public override PXSelectBase<OrganizationLedgerLink> OrganizationLedgerLinkSelect
    {
      get => (PXSelectBase<OrganizationLedgerLink>) this.OrganizationLedgerLinkWithLedgerSelect;
    }

    public override PXSelectBase<PX.Objects.GL.DAC.Organization> OrganizationViewBase
    {
      get => (PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView;
    }

    public override PXSelectBase<PX.Objects.GL.Ledger> LedgerViewBase
    {
      get => (PXSelectBase<PX.Objects.GL.Ledger>) this.LedgerView;
    }

    protected override PX.Objects.GL.DAC.Organization GetUpdatingOrganization(int? organizationID)
    {
      return ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Current;
    }

    protected override System.Type VisibleField => typeof (OrganizationLedgerLink.ledgerID);

    [PXDBInt(IsKey = true)]
    [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.organizationID))]
    [PXParent(typeof (Select<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<OrganizationLedgerLink.organizationID>>>>))]
    protected virtual void OrganizationLedgerLink_OrganizationID_CacheAttached(PXCache sender)
    {
    }

    [PXMergeAttributes]
    [PXSelector(typeof (Search<PX.Objects.GL.Ledger.ledgerID, Where<PX.Objects.GL.Ledger.balanceType, Equal<LedgerBalanceType.actual>, And<PX.Objects.GL.Ledger.baseCuryID, Equal<Current<PX.Objects.GL.DAC.Organization.baseCuryID>>, Or<PX.Objects.GL.Ledger.balanceType, NotEqual<LedgerBalanceType.actual>, Or<PX.Objects.GL.Ledger.baseCuryID, IsNull>>>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr))]
    protected virtual void OrganizationLedgerLink_LedgerID_CacheAttached(PXCache sender)
    {
    }

    [PXUIField]
    [PXButton]
    public virtual IEnumerable viewLedger(PXAdapter adapter)
    {
      OrganizationLedgerLink current = this.OrganizationLedgerLinkSelect.Current;
      if (current != null)
        GeneralLedgerMaint.RedirectTo(current.LedgerID);
      return adapter.Get();
    }
  }

  /// <exclude />
  public class ExtendToCustomer : 
    OrganizationUnitExtendToCustomer<OrganizationMaint, OrganizationBAccount>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.interBranch>();

    protected override ExtendToCustomerGraph<OrganizationMaint, OrganizationBAccount>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToCustomerGraph<OrganizationMaint, OrganizationBAccount>.SourceAccountMapping(typeof (OrganizationBAccount));
    }

    public override void Initialize()
    {
      base.Initialize();
      ((PXAction) this.viewCustomer).SetCategory("Other");
      ((PXAction) this.extendToCustomer).SetCategory("Company Management");
    }
  }

  /// <exclude />
  public class ExtendToVendor : 
    OrganizationUnitExtendToVendor<OrganizationMaint, OrganizationBAccount>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.interBranch>();

    protected override ExtendToVendorGraph<OrganizationMaint, OrganizationBAccount>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToVendorGraph<OrganizationMaint, OrganizationBAccount>.SourceAccountMapping(typeof (OrganizationBAccount));
    }

    public override void Initialize()
    {
      base.Initialize();
      ((PXAction) this.viewVendor).SetCategory("Other");
      ((PXAction) this.extendToVendor).SetCategory("Company Management");
    }
  }

  /// <exclude />
  public class OrganizationSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<OrganizationMaint, OrganizationMaint.OrganizationSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<OrganizationMaint, OrganizationMaint.OrganizationSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<OrganizationMaint, OrganizationMaint.OrganizationSharedContactOverrideGraphExt>.RelatedMapping(typeof (OrganizationBAccount))
      {
        RelatedID = typeof (OrganizationBAccount.bAccountID),
        ChildID = typeof (OrganizationBAccount.defContactID)
      };
    }

    protected override CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class OrganizationSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<OrganizationMaint, OrganizationMaint.OrganizationSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<OrganizationMaint, OrganizationMaint.OrganizationSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<OrganizationMaint, OrganizationMaint.OrganizationSharedAddressOverrideGraphExt>.RelatedMapping(typeof (OrganizationBAccount))
      {
        RelatedID = typeof (OrganizationBAccount.bAccountID),
        ChildID = typeof (OrganizationBAccount.defAddressID)
      };
    }

    protected override CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<OrganizationMaint, OrganizationMaint.OrganizationSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  public class OrganizationMaint_CRDuplicateBAccountIdentifier : 
    CRDuplicateBAccountIdentifier<OrganizationMaint, OrganizationBAccount>
  {
  }
}
