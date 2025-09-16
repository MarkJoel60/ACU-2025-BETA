// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CompanyGroupsMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS.DAC;
using PX.Objects.GL.DAC;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CS;

public class CompanyGroupsMaint : OrganizationMaint
{
  public 
  #nullable disable
  PXAction<OrganizationBAccount> ViewCompany;
  public FbqlSelect<SelectFromBase<GroupOrganizationLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlOperand<
  #nullable enable
  GroupOrganizationLink.organizationID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.GL.DAC.Organization.organizationID>>>, FbqlJoins.Left<PX.Objects.GL.Ledger>.On<BqlOperand<
  #nullable enable
  PX.Objects.GL.DAC.Organization.actualLedgerID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.GL.Ledger.ledgerID>>>, FbqlJoins.Left<PrimaryGroup>.On<BqlOperand<
  #nullable enable
  GroupOrganizationLink.organizationID, IBqlInt>.IsEqual<
  #nullable disable
  PrimaryGroup.organizationID>>>>.Where<BqlOperand<
  #nullable enable
  GroupOrganizationLink.groupID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  GroupOrganizationLink>.View Organizations;

  protected override void OrganizationFilter()
  {
    ((PXSelectBase<OrganizationBAccount>) this.BAccount).WhereAnd<Where<BqlOperand<OrganizationBAccount.organizationType, IBqlString>.IsEqual<OrganizationTypes.group>>>();
  }

  public CompanyGroupsMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID))
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Company), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Companies")
      });
    ((PXAction) this.Delete).SetConfirmationMessage("The current group will be deleted.");
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Group")]
  [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  [PXParent(typeof (Select<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<GroupOrganizationLink.groupID>>>>))]
  protected override void GroupOrganizationLink_GroupID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Company ID")]
  [PXSelector(typeof (Search<PX.Objects.GL.DAC.Organization.organizationID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<PX.Objects.GL.DAC.Organization, Current<AccessInfo.userName>>>, And<BqlOperand<PX.Objects.GL.DAC.Organization.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.group>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.baseCuryID, IBqlString>.IsEqual<BqlField<PX.Objects.GL.DAC.Organization.baseCuryID, IBqlString>.FromCurrent>>>>), new System.Type[] {typeof (PX.Objects.GL.DAC.Organization.organizationCD), typeof (PX.Objects.GL.DAC.Organization.organizationName)}, SubstituteKey = typeof (PX.Objects.GL.DAC.Organization.organizationCD))]
  protected override void GroupOrganizationLink_OrganizationID_CacheAttached(PXCache sender)
  {
  }

  [PXDimensionSelector("COMPANY", typeof (Search2<PX.Objects.CR.BAccount.acctCD, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>, Where<BqlChainableConditionLite<Match<PX.Objects.GL.DAC.Organization, Current<AccessInfo.userName>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.organizationType, IBqlString>.IsEqual<OrganizationTypes.group>>>>), typeof (PX.Objects.CR.BAccount.acctCD), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName)})]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  protected void OrganizationBAccount_AcctCD_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Group Name")]
  protected void OrganizationBAccount_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Company Name")]
  protected override void Organization_OrganizationName_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (IIf<Where<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>, Null, Current<PX.Objects.GL.Company.baseCuryID>>))]
  [PXUIField(DisplayName = "Currency ID")]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
  protected new void Organization_BaseCuryID_CacheAttached(PXCache sender)
  {
  }

  protected void _(PX.Data.Events.FieldUpdating<PX.Objects.GL.DAC.Organization.baseCuryID> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PX.Objects.GL.DAC.Organization) e.Row).BAccountID
    }));
    if (baccount == null)
      return;
    bool cancelled;
    this.ResetVisibilityRestrictions(baccount.BAccountID, baccount.AcctCD, out cancelled);
    if (!cancelled)
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<PX.Objects.GL.DAC.Organization.baseCuryID>>) e).Cancel = true;
  }

  [PXMergeAttributes]
  [PXDefault("Group")]
  protected void Organization_OrganizationType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault("Group")]
  protected void OrganizationBAccount_OrganizationType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(100)]
  [Country]
  protected void Address_CountryID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected void Ledger_LedgerCD_CacheAttached(PXCache sender)
  {
  }

  public override void Persist()
  {
    try
    {
      base.Persist();
    }
    finally
    {
      PXAccess.ResetOrganizationBranchSlot();
    }
  }

  public new static void RedirectTo(int? GroupID)
  {
    CompanyGroupsMaint instance = PXGraph.CreateInstance<CompanyGroupsMaint>();
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) instance, GroupID);
    if (organizationById != null)
    {
      ((PXSelectBase<OrganizationBAccount>) instance.BAccount).Current = PXResultset<OrganizationBAccount>.op_Implicit(((PXSelectBase<OrganizationBAccount>) instance.BAccount).Search<OrganizationBAccount.bAccountID>((object) organizationById.BAccountID, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  protected override void OrganizationBAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    base.OrganizationBAccount_RowSelected(cache, e);
    ((PXAction) this.createLedger).SetVisible(false);
    ((PXAction) this.ActionsMenu).SetVisible(false);
  }

  protected override void Organization_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    bool flag2 = ((PXSelectBase<GroupOrganizationLink>) this.Organizations).SelectSingle(Array.Empty<object>()) != null;
    PXUIFieldAttribute.SetEnabled<PX.Objects.GL.DAC.Organization.baseCuryID>(cache, e.Row, flag1 && !flag2);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<OrganizationBAccount> e)
  {
    if (e.Row == null)
      return;
    bool cancelled;
    this.ResetVisibilityRestrictions(e.Row.BAccountID, e.Row.AcctCD, out cancelled);
    if (!cancelled)
      return;
    e.Cancel = true;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewCompany(PXAdapter adapter)
  {
    GroupOrganizationLink current = ((PXSelectBase<GroupOrganizationLink>) this.Organizations).Current;
    if (current != null)
      OrganizationMaint.RedirectTo(current.OrganizationID);
    return adapter.Get();
  }
}
