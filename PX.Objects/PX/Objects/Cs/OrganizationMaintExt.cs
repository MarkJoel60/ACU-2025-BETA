// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.OrganizationMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.DAC;
using System;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class OrganizationMaintExt : PXGraphExtension<
#nullable disable
OrganizationMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void OrganizationBAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelectJoin<PX.Objects.GL.Ledger, InnerJoin<OrganizationLedgerLink, On<PX.Objects.GL.Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<PX.Objects.GL.Ledger.balanceType, Equal<LedgerBalanceType.actual>, And<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current<PX.Objects.GL.DAC.Organization.organizationID>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    PX.Objects.GL.DAC.Organization current1 = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Current;
    bool flag = !GraphHelper.IsPrimaryObjectInserted((PXGraph) this.Base);
    ((PXAction) this.Base.createLedger).SetEnabled(ledger == null & flag && current1.OrganizationType != "Group");
    ((PXAction) this.Base.AddBranch).SetEnabled(flag && current1.OrganizationType != "Group");
    ((PXAction) this.Base.newContact).SetEnabled(flag && current1.OrganizationType != "Group");
    PXUIFieldAttribute.SetVisible<PX.Objects.GL.DAC.Organization.roleName>(((PXSelectBase) this.Base.OrganizationView).Cache, (object) null, current1 != null && current1.OrganizationType != "Group");
    PXUIFieldAttribute.SetVisible<PX.Objects.GL.DAC.Organization.reporting1099>(((PXSelectBase) this.Base.OrganizationView).Cache, (object) null, current1 != null && current1.OrganizationType != "Group");
    PXUIFieldAttribute.SetVisible<PX.Objects.GL.DAC.Organization.countryID>(((PXSelectBase) this.Base.OrganizationView).Cache, (object) null, current1 != null && current1.OrganizationType != "Group");
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.BAccount.legalName>(((PXSelectBase) this.Base.BAccount).Cache, (object) null, current1 != null && current1.OrganizationType != "Group");
    PXUIFieldAttribute.SetVisible<PX.Objects.CR.BAccount.taxRegistrationID>(((PXSelectBase) this.Base.BAccount).Cache, (object) null, current1 != null && current1.OrganizationType != "Group");
    ((PXCache) GraphHelper.Caches<PX.Objects.CR.Location>((PXGraph) this.Base)).AllowSelect = current1 != null && current1.OrganizationType != "Group";
    ((PXSelectBase) this.Base.Commonsetup).Cache.AllowSelect = current1 != null && current1.OrganizationType != "Group";
    ((PXSelectBase) this.Base.Company).Cache.AllowSelect = current1 != null && current1.OrganizationType != "Group";
    OrganizationMaint.State current2 = ((PXSelectBase<OrganizationMaint.State>) this.Base.StateView).Current;
    OrganizationMaintExt.StateExt extension = PXCacheEx.GetExtension<OrganizationMaintExt.StateExt>((IBqlTable) ((PXSelectBase<OrganizationMaint.State>) this.Base.StateView).Current);
    if (current1 != null)
    {
      current2.IsBranchTabVisible = new bool?(current1.OrganizationType != "WithoutBranches" && current1.OrganizationType != "Group" && ((PXSelectBase) this.Base.OrganizationView).Cache.GetValueOriginal<PX.Objects.GL.DAC.Organization.organizationType>((object) current1) as string != "WithoutBranches");
      extension.IsGroup = new bool?(current1.OrganizationType == "Group");
    }
    else
      extension.IsGroup = new bool?(false);
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void Organization_BaseCuryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Organization_BaseCuryID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.GL.DAC.Organization row = (PX.Objects.GL.DAC.Organization) e.Row;
    ((PXGraph) this.Base).Views["OrganizationLedgerLinkWithLedgerSelect"].RequestRefresh();
  }

  [PXMergeAttributes]
  [OrganizationTypes.List(new string[] {"Group"})]
  protected virtual void Organization_OrganizationType_CacheAttached(PXCache sender)
  {
  }

  [PXHidden]
  [Serializable]
  public sealed class StateExt : PXCacheExtension<OrganizationMaint.State>
  {
    [PXBool]
    public bool? IsGroup { get; set; }

    public abstract class isGroup : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OrganizationMaintExt.StateExt.isGroup>
    {
    }
  }
}
