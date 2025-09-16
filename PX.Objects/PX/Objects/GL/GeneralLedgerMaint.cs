// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GeneralLedgerMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.CS.DAC;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

[PXPrimaryGraph(new Type[] {typeof (GeneralLedgerMaint)}, new Type[] {typeof (Select<Ledger, Where<Ledger.ledgerID, Equal<Current<Ledger.ledgerID>>>>)})]
public class GeneralLedgerMaint : PXGraph<GeneralLedgerMaint, Ledger>
{
  [PXImport(typeof (Ledger))]
  public PXSelect<Ledger> LedgerRecords;
  public PXSetup<Company> company;
  public PXSelectReadonly2<Branch, InnerJoin<PX.Objects.GL.DAC.Organization, On<Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, InnerJoin<OrganizationLedgerLink, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<OrganizationLedgerLink.organizationID>>>>, Where<OrganizationLedgerLink.ledgerID, Equal<Current<Ledger.ledgerID>>>> BranchesView;
  public PXSelect<PX.Objects.GL.DAC.Organization> OrganizationView;
  public PXAction<Ledger> action;
  [PXButton(IsLockedOnToolbar = true, DisplayOnMainToolbar = true)]
  public PXChangeID<Ledger, Ledger.ledgerCD> ChangeID;

  public static void RedirectTo(int? ledgerID)
  {
    GeneralLedgerMaint instance = PXGraph.CreateInstance<GeneralLedgerMaint>();
    if (ledgerID.HasValue)
      ((PXSelectBase<Ledger>) instance.LedgerRecords).Current = PXResultset<Ledger>.op_Implicit(((PXSelectBase<Ledger>) instance.LedgerRecords).Search<Ledger.ledgerID>((object) ledgerID, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  public static Ledger FindLedgerByID(PXGraph graph, int? ledgerID, bool isReadonly = true)
  {
    if (isReadonly)
      return Ledger.PK.Find(graph, ledgerID);
    return PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelect<Ledger, Where<Ledger.ledgerID, Equal<Required<Ledger.ledgerID>>>>.Config>.Select(graph, new object[1]
    {
      (object) ledgerID
    }));
  }

  public GeneralLedgerMaint()
  {
    PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSetup<PX.Objects.GL.DAC.Organization>.Select((PXGraph) this, Array.Empty<object>()));
    Ledger ledger = PXResultset<Ledger>.op_Implicit(PXSelectBase<Ledger, PXSelectReadonly<Ledger, Where<BqlOperand<Ledger.balanceType, IBqlString>.IsEqual<LedgerBalanceType.actual>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (organization == null && ((PXSelectBase<Company>) this.company).Current.BaseCuryID == null && ledger == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (Company), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Companies")
      });
    PXUIFieldAttribute.SetVisible<Ledger.baseCuryID>(((PXSelectBase) this.LedgerRecords).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    ((PXSelectBase) this.BranchesView).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.branch>();
    ((PXAction) this.action).MenuAutoOpen = true;
  }

  [PXMergeAttributes]
  [PXDefault(typeof (IIf<Where<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>, Null, IsNull<Current<AccessInfo.baseCuryID>, Current<Company.baseCuryID>>>))]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>>>))]
  protected void Ledger_BaseCuryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Ledger_BaseCuryID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is Ledger row))
      return;
    int? ledgerId = row.LedgerID;
    if (!ledgerId.HasValue || row.BaseCuryID == null)
      return;
    ledgerId = row.LedgerID;
    if (GLUtility.IsLedgerHistoryExist((PXGraph) this, new int?(ledgerId.Value)))
      throw new PXSetPropertyException("{0} cannot be changed.", new object[1]
      {
        (object) "[baseCuryID]"
      });
    if (row.BalanceType == "A" && ((PXSelectBase<Company>) this.company).Current.BaseCuryID != (string) e.NewValue && !PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      throw new PXSetPropertyException("Actual ledger '{0}' must be defined in base currency {1} only.", new object[2]
      {
        (object) row.LedgerCD,
        (object) ((PXSelectBase<Company>) this.company).Current.BaseCuryID
      });
  }

  protected virtual void Ledger_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is Ledger row))
      return;
    this.CanBeLedgerDeleted(row);
  }

  protected virtual void Ledger_BalanceType_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    Ledger row = e.Row as Ledger;
    string newValue = (string) e.NewValue;
    if (row == null)
      return;
    int? ledgerId = row.LedgerID;
    if (!ledgerId.HasValue || !row.CreatedByID.HasValue)
      return;
    ledgerId = row.LedgerID;
    if (GLUtility.IsLedgerHistoryExist((PXGraph) this, new int?(ledgerId.Value)))
      throw new PXSetPropertyException("{0} cannot be changed.", new object[1]
      {
        (object) "[balanceType]"
      });
    if (!(newValue == "A"))
      return;
    if (((PXSelectBase<Company>) this.company).Current.BaseCuryID != row.BaseCuryID && !PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      throw new PXSetPropertyException("Actual ledger '{0}' must be defined in base currency {1} only.", new object[2]
      {
        (object) row.LedgerCD,
        (object) ((PXSelectBase<Company>) this.company).Current.BaseCuryID
      });
    this.CanBeLedgerSetAsActual(row, ((PXGraph) this).GetExtension<GeneralLedgerMaint.OrganizationLedgerLinkMaint>());
  }

  protected virtual void _(Events.FieldUpdated<Ledger.balanceType> e)
  {
    Ledger row = e.Row as Ledger;
    if (!(row.BalanceType == "A"))
      return;
    foreach (object obj in GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<OrganizationLedgerLink, PXSelectJoin<OrganizationLedgerLink, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<OrganizationLedgerLink.organizationID>>>, Where<OrganizationLedgerLink.ledgerID, Equal<Required<OrganizationLedgerLink.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.LedgerID
    })))
    {
      if (((PXSelectBase) this.OrganizationView).Cache.Locate(obj) is PX.Objects.GL.DAC.Organization organization)
      {
        organization.ActualLedgerID = row.LedgerID;
        GraphHelper.MarkUpdated(((PXSelectBase) this.OrganizationView).Cache, (object) organization);
      }
    }
  }

  public virtual void Persist()
  {
    GeneralLedgerMaint.OrganizationLedgerLinkMaint extension = ((PXGraph) this).GetExtension<GeneralLedgerMaint.OrganizationLedgerLinkMaint>();
    foreach (Ledger ledger in ((PXSelectBase) this.LedgerRecords).Cache.Deleted)
      this.CanBeLedgerDeleted(ledger);
    foreach (Ledger ledger in ((PXSelectBase) this.LedgerRecords).Cache.Updated)
    {
      string valueOriginal = ((PXSelectBase) this.LedgerRecords).Cache.GetValueOriginal<Ledger.balanceType>((object) ledger) as string;
      if (valueOriginal != ledger.BalanceType)
      {
        if (PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelectReadonly<GLTran, Where<GLTran.ledgerID, Equal<Required<GLTran.ledgerID>>, And<GLTran.released, Equal<True>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) ledger.LedgerID
        })) != null)
          throw new PXException("The type of the {0} ledger cannot be changed because at least one released GL transaction exists for the ledger.", new object[1]
          {
            (object) ledger.LedgerCD
          });
        if (valueOriginal == "A")
          this.SetActualLedgerIDNullInRelatedCompanies(ledger, extension);
      }
      if (ledger.BalanceType == "A")
        this.CanBeLedgerSetAsActual(ledger, extension);
    }
    PX.Objects.GL.DAC.Organization[] array = ((PXSelectBase) this.OrganizationView).Cache.Updated.Cast<PX.Objects.GL.DAC.Organization>().Select<PX.Objects.GL.DAC.Organization, PX.Objects.GL.DAC.Organization>(new Func<PX.Objects.GL.DAC.Organization, PX.Objects.GL.DAC.Organization>(PXCache<PX.Objects.GL.DAC.Organization>.CreateCopy)).ToArray<PX.Objects.GL.DAC.Organization>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXGraph) this).Persist();
      ((PXSelectBase) this.OrganizationView).Cache.Clear();
      extension.OnPersist((IEnumerable<PX.Objects.GL.DAC.Organization>) array);
      transactionScope.Complete();
    }
  }

  protected virtual void CanBeLedgerSetAsActual(
    Ledger ledger,
    GeneralLedgerMaint.OrganizationLedgerLinkMaint linkMaint)
  {
    linkMaint.CheckActualLedgerCanBeAssigned(ledger, this.GetLinkedOrganizationIDs(ledger).ToArray<int?>());
  }

  private void SetActualLedgerIDNullInRelatedCompanies(
    Ledger ledger,
    GeneralLedgerMaint.OrganizationLedgerLinkMaint linkMaint)
  {
    linkMaint.SetActualLedgerIDNullInRelatedCompanies(ledger, this.GetOrganizationIDsWithActualLedger(ledger).ToArray<int?>());
  }

  private IEnumerable<int?> GetOrganizationIDsWithActualLedger(Ledger ledger)
  {
    return GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.actualLedgerID, Equal<Required<PX.Objects.GL.DAC.Organization.actualLedgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ledger.LedgerID
    })).Select<PX.Objects.GL.DAC.Organization, int?>((Func<PX.Objects.GL.DAC.Organization, int?>) (l => l.OrganizationID));
  }

  private IEnumerable<int?> GetLinkedOrganizationIDs(Ledger ledger)
  {
    return GraphHelper.RowCast<OrganizationLedgerLink>((IEnumerable) PXSelectBase<OrganizationLedgerLink, PXSelect<OrganizationLedgerLink, Where<OrganizationLedgerLink.ledgerID, Equal<Required<OrganizationLedgerLink.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ledger.LedgerID
    })).Select<OrganizationLedgerLink, int?>((Func<OrganizationLedgerLink, int?>) (l => l.OrganizationID));
  }

  protected virtual void CanBeLedgerDeleted(Ledger ledger)
  {
    this.CheckLinksToOrganizationsOnDelete(ledger);
    if (PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelectReadonly<Batch, Where<Batch.ledgerID, Equal<Required<Batch.ledgerID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ledger.LedgerID
    })) != null)
      throw new PXException("The {0} ledger cannot be deleted because at least one General Ledger batch exists for the ledger.", new object[1]
      {
        (object) ledger.LedgerCD
      });
    if (PXResultset<GLTran>.op_Implicit(PXSelectBase<GLTran, PXSelectReadonly<GLTran, Where<GLTran.ledgerID, Equal<Required<GLTran.ledgerID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ledger.LedgerID
    })) != null)
      throw new PXException("The {0} ledger cannot be deleted because at least one General Ledger transaction has been released for this ledger.", new object[1]
      {
        (object) ledger.LedgerCD
      });
  }

  protected virtual void CheckLinksToOrganizationsOnDelete(Ledger ledger)
  {
    PX.Objects.GL.DAC.Organization[] array = ((IEnumerable<PXResult<OrganizationLedgerLink>>) PXSelectBase<OrganizationLedgerLink, PXSelectJoin<OrganizationLedgerLink, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<OrganizationLedgerLink.organizationID>>>, Where<OrganizationLedgerLink.ledgerID, Equal<Required<OrganizationLedgerLink.ledgerID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ledger.LedgerID
    })).AsEnumerable<PXResult<OrganizationLedgerLink>>().Cast<PXResult<OrganizationLedgerLink, PX.Objects.GL.DAC.Organization>>().Select<PXResult<OrganizationLedgerLink, PX.Objects.GL.DAC.Organization>, PX.Objects.GL.DAC.Organization>((Func<PXResult<OrganizationLedgerLink, PX.Objects.GL.DAC.Organization>, PX.Objects.GL.DAC.Organization>) (row => PXResult<OrganizationLedgerLink, PX.Objects.GL.DAC.Organization>.op_Implicit(row))).ToArray<PX.Objects.GL.DAC.Organization>();
    if (((IEnumerable<PX.Objects.GL.DAC.Organization>) array).Any<PX.Objects.GL.DAC.Organization>())
      throw new PXException("The {0} ledger cannot be deleted because the following company or companies are associated with the ledger: {1}.", new object[2]
      {
        (object) ledger.LedgerCD.Trim(),
        (object) ((ICollection<string>) ((IEnumerable<PX.Objects.GL.DAC.Organization>) array).Select<PX.Objects.GL.DAC.Organization, string>((Func<PX.Objects.GL.DAC.Organization, string>) (l => l.OrganizationCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>()
      });
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(PXAdapter adapter) => adapter.Get();

  public class OrganizationLedgerLinkMaint : 
    OrganizationLedgerLinkMaintBase<GeneralLedgerMaint, Ledger>
  {
    protected Dictionary<int?, Ledger> LedgerIDMap = new Dictionary<int?, Ledger>();
    public PXAction<Ledger> ViewOrganization;
    public PXSelectJoin<OrganizationLedgerLink, LeftJoin<PX.Objects.GL.DAC.Organization, On<OrganizationLedgerLink.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where<OrganizationLedgerLink.ledgerID, Equal<Current<Ledger.ledgerID>>>> OrganizationLedgerLinkWithOrganizationSelect;

    public override PXSelectBase<OrganizationLedgerLink> OrganizationLedgerLinkSelect
    {
      get
      {
        return (PXSelectBase<OrganizationLedgerLink>) this.OrganizationLedgerLinkWithOrganizationSelect;
      }
    }

    public override PXSelectBase<PX.Objects.GL.DAC.Organization> OrganizationViewBase
    {
      get => (PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView;
    }

    public override PXSelectBase<Ledger> LedgerViewBase
    {
      get => (PXSelectBase<Ledger>) this.Base.LedgerRecords;
    }

    protected override PX.Objects.GL.DAC.Organization GetUpdatingOrganization(int? organizationID)
    {
      return PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Search<PX.Objects.GL.DAC.Organization.organizationID>((object) organizationID, Array.Empty<object>()));
    }

    protected override Type VisibleField => typeof (OrganizationLedgerLink.organizationID);

    [PXUIField]
    [PXButton]
    public virtual IEnumerable viewOrganization(PXAdapter adapter)
    {
      OrganizationLedgerLink current = this.OrganizationLedgerLinkSelect.Current;
      if (current != null)
        OrganizationMaint.RedirectTo(current.OrganizationID);
      return adapter.Get();
    }

    [PXMergeAttributes]
    [Organization(true, typeof (Search<PX.Objects.GL.DAC.Organization.organizationID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.baseCuryID, Equal<BqlField<Ledger.baseCuryID, IBqlString>.FromCurrent>>>>, Or<BqlOperand<Current<Ledger.baseCuryID>, IBqlString>.IsNull>>, Or<BqlOperand<PX.Objects.GL.DAC.Organization.baseCuryID, IBqlString>.IsNull>>>.Or<BqlOperand<Current<Ledger.balanceType>, IBqlString>.IsNotEqual<LedgerBalanceType.actual>>>>>>), null, IsKey = true, FieldClass = null)]
    protected virtual void OrganizationLedgerLink_OrganizationID_CacheAttached(PXCache sender)
    {
    }

    protected override void OrganizationLedgerLink_RowInserting(
      PXCache cache,
      PXRowInsertingEventArgs e)
    {
      if (e.Row is OrganizationLedgerLink row && !PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && ((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current != null && ((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current.BalanceType == "A" && ((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current.BaseCuryID == null)
        ((PXSelectBase) this.Base.LedgerRecords).Cache.SetValueExt<Ledger.baseCuryID>((object) ((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current, (object) this.GetUpdatingOrganization(row.OrganizationID).BaseCuryID);
      base.OrganizationLedgerLink_RowInserting(cache, e);
    }

    protected override void OrganizationLedgerLink_RowInserted(
      PXCache cache,
      PXRowInsertedEventArgs e)
    {
      base.OrganizationLedgerLink_RowInserted(cache, e);
      if (!(e.Row is OrganizationLedgerLink row) || ((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current == null || ((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current.BaseCuryID != null)
        return;
      ((PXSelectBase) this.Base.LedgerRecords).Cache.SetValueExt<Ledger.baseCuryID>((object) ((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current, (object) this.GetUpdatingOrganization(row.OrganizationID).BaseCuryID);
    }

    protected virtual void OrganizationLedgerLink_RowUpdating(
      PXCache cache,
      PXRowUpdatingEventArgs e)
    {
      if ((e.NewRow is OrganizationLedgerLink newRow ? (!newRow.OrganizationID.HasValue ? 1 : 0) : 1) != 0)
        return;
      this.CheckActualLedgerCanBeAssigned(((PXSelectBase<Ledger>) this.Base.LedgerRecords).Current, newRow.OrganizationID.SingleToArray<int?>());
    }

    public virtual void Organization_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
    {
      ((CancelEventArgs) e).Cancel = true;
    }

    protected virtual void Ledger_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
    {
      Ledger row = e.Row as Ledger;
      ((PXAction) this.Base.ChangeID).SetEnabled(cache.GetStatus((object) row) != 2 || row.LedgerCD != null);
      if (row == null || !row.LedgerID.HasValue)
        return;
      bool flag1 = GLUtility.IsLedgerHistoryExist((PXGraph) this.Base, row.LedgerID);
      PXUIFieldAttribute.SetEnabled<Ledger.balanceType>(((PXSelectBase) this.Base.LedgerRecords).Cache, (object) row, !flag1);
      bool flag2 = row.BalanceType != "A" && !flag1 && PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
      if (!flag1 && row.BalanceType == "A" && PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
        flag2 = ((PXSelectBase<OrganizationLedgerLink>) this.OrganizationLedgerLinkWithOrganizationSelect).SelectSingle(Array.Empty<object>()) == null;
      PXUIFieldAttribute.SetEnabled<Ledger.baseCuryID>(((PXSelectBase) this.Base.LedgerRecords).Cache, (object) row, flag2);
    }

    public virtual void Ledger_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
    {
      if (!(e.Row is Ledger row))
        return;
      int? ledgerId = row.LedgerID;
      int num = 0;
      if (!(ledgerId.GetValueOrDefault() < num & ledgerId.HasValue))
        return;
      this.LedgerIDMap[row.LedgerID] = row;
    }

    public virtual void OnPersist(IEnumerable<PX.Objects.GL.DAC.Organization> organizations)
    {
      OrganizationMaint instance = PXGraph.CreateInstance<OrganizationMaint>();
      PXCache cache = ((PXSelectBase) instance.OrganizationView).Cache;
      cache.GetAttributesOfType<PXDBTimestampAttribute>((object) null, "tstamp").First<PXDBTimestampAttribute>().VerifyTimestamp = (VerifyTimestampOptions) 1;
      foreach (PX.Objects.GL.DAC.Organization organization in organizations)
      {
        ((PXGraph) instance).Clear();
        ((PXSelectBase<OrganizationBAccount>) instance.BAccount).Current = PXResultset<OrganizationBAccount>.op_Implicit(((PXSelectBase<OrganizationBAccount>) instance.BAccount).Search<OrganizationBAccount.bAccountID>((object) organization.BAccountID, Array.Empty<object>()));
        cache.Clear();
        cache.ClearQueryCacheObsolete();
        int? actualLedgerId = organization.ActualLedgerID;
        int num = 0;
        if (actualLedgerId.GetValueOrDefault() < num & actualLedgerId.HasValue)
          organization.ActualLedgerID = this.LedgerIDMap[organization.ActualLedgerID].LedgerID;
        cache.Current = (object) organization;
        cache.SetStatus((object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) instance.OrganizationView).Current, (PXEntryStatus) 1);
        ((PXGraph) instance).Actions.PressSave();
      }
    }
  }
}
