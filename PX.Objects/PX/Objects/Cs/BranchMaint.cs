// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.BranchMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CR.GraphExtensions;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;
using PX.Objects.GraphExtensions.ExtendBAccount;
using PX.SM;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class BranchMaint : 
  OrganizationUnitMaintBase<
  #nullable disable
  BranchMaint.BranchBAccount, Where<True, Equal<True>>>
{
  public PXSelect<BranchMaint.BranchBAccount, Where<BranchMaint.BranchBAccount.bAccountID, Equal<Current<BranchMaint.BranchBAccount.bAccountID>>>> CurrentBAccount;
  public PXSelectJoin<PX.Objects.EP.EPEmployee, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.EP.EPEmployee.defContactID>, And<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.EP.EPEmployee.defAddressID>, And<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>>>, Where<PX.Objects.EP.EPEmployee.parentBAccountID, Equal<Current<BranchMaint.BranchBAccount.bAccountID>>>> Employees;
  public PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Current<PX.Objects.CR.BAccount.noteID>>>> Notedocs;
  public PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Current2<BranchMaint.BranchBAccount.organizationID>>>> CurrentOrganizationView;
  public PXSelectReadonly2<PX.Objects.GL.Ledger, InnerJoin<OrganizationLedgerLink, On<PX.Objects.GL.Ledger.ledgerID, Equal<OrganizationLedgerLink.ledgerID>>>, Where<OrganizationLedgerLink.organizationID, Equal<Current2<BranchMaint.BranchBAccount.organizationID>>>> LedgersView;
  public PXAction<BranchMaint.BranchBAccount> ViewLedger;
  public PXChangeBAccountID<BranchMaint.BranchBAccount, BranchMaint.BranchBAccount.acctCD> ChangeID;

  public static PX.Objects.GL.Branch FindBranchByCD(
    PXGraph graph,
    string branchCD,
    bool isReadonly = true)
  {
    if (!isReadonly)
      throw new NotImplementedException();
    return PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchCD, Equal<Required<PX.Objects.GL.Branch.branchCD>>>>.Config>.Select(graph, new object[1]
    {
      (object) branchCD
    }));
  }

  public static PX.Objects.GL.Branch FindBranchByID(PXGraph graph, int? branchID)
  {
    return BranchMaint.FindBranchesByID(graph, branchID.SingleToArray<int?>()).SingleOrDefault<PX.Objects.GL.Branch>();
  }

  public static IEnumerable<PX.Objects.GL.Branch> FindBranchesByID(PXGraph graph, int?[] branchIDs)
  {
    if (branchIDs == null || !((IEnumerable<int?>) branchIDs).Any<int?>())
      return (IEnumerable<PX.Objects.GL.Branch>) new PX.Objects.GL.Branch[0];
    return GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, In<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select(graph, new object[1]
    {
      (object) branchIDs
    }));
  }

  public static IEnumerable<PX.Objects.GL.Branch> GetSeparateChildBranches(
    PXGraph graph,
    PX.Objects.GL.DAC.Organization organization)
  {
    if (!organization.OrganizationID.HasValue || !organization.BAccountID.HasValue)
      return (IEnumerable<PX.Objects.GL.Branch>) new PX.Objects.GL.Branch[0];
    return GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>, And<PX.Objects.GL.Branch.bAccountID, NotEqual<Required<PX.Objects.GL.Branch.bAccountID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) organization.OrganizationID,
      (object) organization.BAccountID
    }));
  }

  public static IEnumerable<PX.Objects.GL.Branch> GetChildBranches(
    PXGraph graph,
    int? organizationID)
  {
    return GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Config>.Select(graph, new object[1]
    {
      (object) organizationID
    }));
  }

  public static PX.Objects.GL.Branch GetChildBranch(PXGraph graph, int? organizationID)
  {
    return PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) organizationID
    }));
  }

  public static bool MoreThenOneBranchExist(PXGraph graph, int? organizationID)
  {
    return GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>.Config>.SelectWindowed(graph, 0, 2, new object[1]
    {
      (object) organizationID
    })).ToArray<PX.Objects.GL.Branch>().Length > 1;
  }

  public static void RedirectTo(int? baccountID)
  {
    BranchMaint instance = PXGraph.CreateInstance<BranchMaint>();
    if (baccountID.HasValue)
      ((PXSelectBase<BranchMaint.BranchBAccount>) instance.BAccount).Current = PXResultset<BranchMaint.BranchBAccount>.op_Implicit(((PXSelectBase<BranchMaint.BranchBAccount>) instance.BAccount).Search<BranchMaint.BranchBAccount.bAccountID>((object) baccountID, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  public override PXSelectBase<PX.Objects.EP.EPEmployee> EmployeesAccessor
  {
    get => (PXSelectBase<PX.Objects.EP.EPEmployee>) this.Employees;
  }

  public BranchMaint()
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.branch>();
    ((PXSelectBase) this.BAccount).Cache.AllowInsert = flag;
    ((PXAction) this.Next).SetVisible(flag);
    ((PXAction) this.Previous).SetVisible(flag);
    ((PXAction) this.Last).SetVisible(flag);
    ((PXAction) this.First).SetVisible(flag);
    ((PXAction) this.Insert).SetVisible(flag);
    ((PXAction) this.ChangeID).SetCategory("Other");
    GraphHelper.Caches<RedirectBranchParameters>((PXGraph) this);
  }

  protected virtual BranchValidator GetBranchValidator() => new BranchValidator((PXGraph) this);

  protected virtual void BranchBAccount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row))
      return;
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = true;
    ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Current = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Select(Array.Empty<object>()));
    PX.Objects.GL.DAC.Organization current = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Current;
    bool flag4 = true;
    if (current?.OrganizationType == "WithoutBranches")
    {
      flag2 = false;
      flag3 = false;
      flag4 = false;
    }
    else
    {
      PX.Objects.GL.Branch branchByCd = BranchMaint.FindBranchByCD((PXGraph) this, row.BranchBranchCD);
      if (branchByCd != null)
        flag1 = GLUtility.GetRelatedToBranchGLHistory((PXGraph) this, branchByCd.BranchID.SingleToArray<int?>()) == null;
    }
    GraphHelper.IsPrimaryObjectInserted((PXGraph) this);
    ((PXSelectBase) this.BAccount).AllowUpdate = flag4;
    ((PXSelectBase) this.CurrentBAccount).AllowUpdate = flag4;
    ((PXSelectBase) this.CurrentOrganizationView).AllowUpdate = flag4;
    ((PXSelectBase) this.Employees).AllowUpdate = flag4;
    PXUIFieldAttribute.SetEnabled<BranchMaint.BranchBAccount.organizationID>(sender, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<BranchMaint.BranchBAccount.branchRoleName>(sender, (object) null, current != null && current.RoleName == null);
    ((PXSelectBase) this.BAccount).AllowDelete = flag3;
    ((PXAction) this.Delete).SetEnabled(flag3);
    ((PXAction) this.ChangeID).SetEnabled((sender.GetStatus((object) row) != 2 || row.AcctCD != null) && current?.OrganizationType != "WithoutBranches");
    PXUIFieldAttribute.SetVisible<BranchMaint.BranchBAccount.organizationID>(sender, (object) null, flag2);
    PXUIFieldAttribute.SetEnabled<BranchMaint.BranchBAccount.branchLogoName>(sender, (object) null, flag4);
    PXUIFieldAttribute.SetEnabled<BranchMaint.BranchBAccount.branchLogoNameReport>(sender, (object) null, flag4);
  }

  protected virtual void BranchBAccount_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row))
      return;
    int? valueOriginal = (int?) sender.GetValueOriginal<BranchMaint.BranchBAccount.organizationID>((object) row);
    int? nullable1;
    if (e.Operation == 1)
    {
      nullable1 = valueOriginal;
      int? organizationId = row.OrganizationID;
      if (!(nullable1.GetValueOrDefault() == organizationId.GetValueOrDefault() & nullable1.HasValue == organizationId.HasValue))
      {
        ((PXSelectBase) this.CurrentOrganizationView).Cache.SetStatus((object) OrganizationMaint.FindOrganizationByID((PXGraph) this, valueOriginal, false), (PXEntryStatus) 1);
        if (GLUtility.RelatedForBranchReleasedTransactionExists((PXGraph) this, BranchMaint.FindBranchByCD((PXGraph) this, row.BranchBranchCD).BranchID))
          throw new PXException("The {0} of the {1} branch cannot be changed because at least one released General Ledger transaction exists for the branch.", new object[2]
          {
            (object) PXUIFieldAttribute.GetDisplayName<BranchMaint.BranchBAccount.organizationID>(sender),
            (object) row.BranchBranchCD
          });
      }
    }
    if (e.Operation != 2)
    {
      if (e.Operation != 1)
        return;
      int? nullable2 = valueOriginal;
      nullable1 = row.OrganizationID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        return;
    }
    ((PXSelectBase) this.CurrentOrganizationView).Cache.SetStatus((object) OrganizationMaint.FindOrganizationByID((PXGraph) this, row.OrganizationID, false), (PXEntryStatus) 1);
  }

  protected virtual void BranchBAccount_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row))
      return;
    if (!((PXGraph) this).UnattendedMode)
    {
      PX.Objects.GL.Branch branchByCd = BranchMaint.FindBranchByCD((PXGraph) this, row.BranchBranchCD);
      if (branchByCd != null)
        this.GetBranchValidator().CanBeBranchesDeletedSeparately((IReadOnlyCollection<PX.Objects.GL.Branch>) branchByCd.SingleToArray<PX.Objects.GL.Branch>());
    }
    ((PXSelectBase) this.CurrentOrganizationView).Cache.SetStatus((object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Current, (PXEntryStatus) 1);
    BAccount2 baccount2_1 = PXResultset<BAccount2>.op_Implicit(PXSelectBase<BAccount2, PXSelectReadonly2<BAccount2, InnerJoin<PX.Objects.CR.Standalone.Location, On<BAccount2.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.CR.Standalone.Location.cBranchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<PX.Objects.AR.Customer, On<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<BAccount2.bAccountID>>>>>, Where<PX.Objects.GL.Branch.branchCD, Equal<Required<PX.Objects.GL.Branch.branchCD>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.BranchBranchCD
    }));
    if (baccount2_1 != null)
      throw new PXSetPropertyException("This branch is associated with at least one customer account. To delete the branch, clear the Default Branch box on the Customers (AR303000) form for the following customer accounts: {0}.", new object[1]
      {
        (object) baccount2_1.AcctCD.Trim()
      });
    BAccount2 baccount2_2 = PXResultset<BAccount2>.op_Implicit(PXSelectBase<BAccount2, PXSelectReadonly2<BAccount2, InnerJoin<PX.Objects.CR.Standalone.Location, On<BAccount2.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.CR.Standalone.Location.vBranchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<PX.Objects.AP.Vendor, On<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<BAccount2.bAccountID>>>>>, Where<PX.Objects.GL.Branch.branchCD, Equal<Required<PX.Objects.GL.Branch.branchCD>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.BranchBranchCD
    }));
    if (baccount2_2 != null)
      throw new PXSetPropertyException("This branch is associated with at least one vendor account. To delete the branch, clear the Receiving Branch box on the Purchase Settings tab of the Vendors (AP303000) form for the following vendor accounts: {0}.", new object[1]
      {
        (object) baccount2_2.AcctCD.Trim()
      });
  }

  protected virtual void BranchBAccount_Active_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row) || ((PXGraph) this).UnattendedMode)
      return;
    PX.Objects.GL.Branch branchByCd = BranchMaint.FindBranchByCD((PXGraph) this, row.BranchBranchCD);
    if (branchByCd == null)
      return;
    this.GetBranchValidator().ValidateActiveField(branchByCd.BranchID.SingleToArray<int?>(), (bool?) e.NewValue, ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Current);
  }

  protected virtual void BranchBAccount_OrganizationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row))
      return;
    ((PXSelectBase) this.BAccount).Cache.SetDefaultExt<BranchMaint.BranchBAccount.branchRoleName>((object) row);
    ((PXSelectBase) this.BAccount).Cache.SetDefaultExt<BranchMaint.BranchBAccount.branchCountryID>((object) row);
    ((PXSelectBase) this.BAccount).Cache.SetDefaultExt<BranchMaint.BranchBAccount.organizationLogoNameReport>((object) row);
    BranchMaint.DefLocationExt extension = ((PXGraph) this).GetExtension<BranchMaint.DefLocationExt>();
    ((PXSelectBase) extension?.DefLocation).Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType>((object) ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension.DefLocation).Current);
  }

  protected virtual void Location_CAvalaraCustomerUsageType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Select(Array.Empty<object>()));
    if (organization == null)
      return;
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccount2, On<BAccount2.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<BAccount2.defLocationID, Equal<PX.Objects.CR.Standalone.Location.locationID>>>>, Where<BAccount2.bAccountID, Equal<Required<BAccount2.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.BAccountID
    }));
    e.NewValue = (object) location?.CAvalaraCustomerUsageType;
  }

  protected virtual void BranchBAccount_BranchRoleName_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row))
      return;
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, row.OrganizationID);
    if (organizationById == null)
      e.NewValue = (object) null;
    else if (organizationById.RoleName != null)
      e.NewValue = (object) organizationById.RoleName;
    else
      e.NewValue = (object) row.BranchRoleName;
  }

  protected virtual void BranchBAccount_BranchCountryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row))
      return;
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, row.OrganizationID);
    if (organizationById != null && organizationById.CountryID != null && row.BranchCountryID == null)
      e.NewValue = (object) organizationById.CountryID;
    else
      e.NewValue = (object) row.BranchCountryID;
  }

  protected virtual void BranchBAccount_AcctMapNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is BranchMaint.BranchBAccount row) || row.BranchBranchCD == null)
      return;
    using (new PXReadDeletedScope(false))
    {
      PX.Objects.GL.Branch branchByCd = BranchMaint.FindBranchByCD((PXGraph) this, row.BranchBranchCD);
      if (branchByCd == null)
        return;
      e.NewValue = (object) (int?) GraphHelper.RowCast<BranchAcctMap>((IEnumerable) PXSelectBase<BranchAcctMap, PXSelectGroupBy<BranchAcctMap, Where<BranchAcctMap.branchID, Equal<Required<BranchAcctMap.branchID>>>, Aggregate<Max<BranchAcctMap.lineNbr>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) branchByCd.BranchID
      })).FirstOrDefault<BranchAcctMap>()?.LineNbr;
    }
  }

  protected virtual void Address_CountryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Select(Array.Empty<object>()));
    if (organization == null)
      return;
    PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Required<PX.Objects.CR.Address.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) organization.BAccountID
    }));
    if (address == null)
      return;
    e.NewValue = (object) address.CountryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewLedger(PXAdapter adapter)
  {
    PX.Objects.GL.Ledger current = ((PXSelectBase<PX.Objects.GL.Ledger>) this.LedgersView).Current;
    if (current != null)
      GeneralLedgerMaint.RedirectTo(current.LedgerID);
    return adapter.Get();
  }

  public virtual void Clear()
  {
    RedirectBranchParameters current = ((PXCache) GraphHelper.Caches<RedirectBranchParameters>((PXGraph) this)).Current as RedirectBranchParameters;
    ((PXGraph) this).Clear();
    if (current == null)
      return;
    GraphHelper.Caches<RedirectBranchParameters>((PXGraph) this).Insert(current);
  }

  public virtual void Persist()
  {
    bool flag1 = !((PXGraph) this).Accessinfo.BranchID.HasValue;
    bool flag2 = false;
    if (!((PXGraph) this).IsImport && !((PXGraph) this).IsExport && !((PXGraph) this).IsContractBasedAPI && !((PXGraph) this).IsMobile)
    {
      flag2 = NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.BAccount).Cache.Inserted) || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.BAccount).Cache.Deleted);
      if (!flag2)
      {
        foreach (object row in ((PXSelectBase) this.BAccount).Cache.Updated)
        {
          if (((PXSelectBase) this.BAccount).Cache.IsValueUpdated<string, BranchMaint.BranchBAccount.acctName>(row, (IEqualityComparer<string>) StringComparer.CurrentCulture))
            flag2 = true;
          if (((PXSelectBase) this.BAccount).Cache.IsValueUpdated<bool?, BranchMaint.BranchBAccount.overrideThemeVariables>(row) || ((PXSelectBase) this.BAccount).Cache.IsValueUpdated<string, BranchMaint.BranchBAccount.backgroundColor>(row, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) || ((PXSelectBase) this.BAccount).Cache.IsValueUpdated<string, BranchMaint.BranchBAccount.primaryColor>(row, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            flag2 = true;
          else if (((PXSelectBase) this.BAccount).Cache.IsValueUpdated<string, BranchMaint.BranchBAccount.branchRoleName>(row, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
          {
            flag2 = true;
            flag1 = true;
          }
        }
      }
    }
    BranchMaint.BranchBAccount current = ((PXSelectBase<BranchMaint.BranchBAccount>) this.BAccount).Current;
    PXEntryStatus status = ((PXSelectBase) this.BAccount).Cache.GetStatus((object) current);
    int? nullable;
    if (current != null)
    {
      nullable = current.OrganizationID;
      if (nullable.HasValue && (status == 1 || status == 2))
      {
        PX.Objects.GL.Branch branchByCd = BranchMaint.FindBranchByCD((PXGraph) this, current.BranchBranchCD);
        BranchMaint.BranchBAccount branchBAccount = current;
        int? branchID;
        if (branchByCd == null)
        {
          nullable = new int?();
          branchID = nullable;
        }
        else
          branchID = branchByCd.BranchID;
        this.ProcessActiveChange(branchBAccount, branchID);
        if ((string) ((PXSelectBase) this.BAccount).Cache.GetValueOriginal<BranchMaint.BranchBAccount.branchRoleName>((object) current) != current.BranchRoleName)
          ((PXSelectBase) this.CurrentOrganizationView).Cache.SetStatus((object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Current, (PXEntryStatus) 1);
      }
    }
    foreach (BranchMaint.BranchBAccount branchBaccount in ((PXSelectBase) this.BAccount).Cache.Deleted)
    {
      PX.Objects.GL.Branch branchByCd = BranchMaint.FindBranchByCD((PXGraph) this, branchBaccount.BranchBranchCD);
      this.GetBranchValidator().CanBeBranchesDeletedSeparately((IReadOnlyCollection<PX.Objects.GL.Branch>) branchByCd.SingleToArray<PX.Objects.GL.Branch>());
      nullable = ((PXGraph) this).Accessinfo.BranchID;
      int? branchId = branchByCd.BranchID;
      flag1 = nullable.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable.HasValue == branchId.HasValue;
    }
    ((PXGraph) this).Persist();
    this.AfterPersist();
    if (!((PXGraph) this).UnattendedMode)
    {
      this.ClearRoleNameInBranches();
      this.RefreshBranch();
    }
    if (flag2)
    {
      PXPageCacheUtils.InvalidateCachedPages();
      PXDatabase.SelectTimeStamp();
      if (!((PXGraph) this).UnattendedMode && !ScreenGraphUtils.IsScreenOpenedInPopupMode())
        throw new PXRedirectRequiredException((PXGraph) this, "Branch", true);
    }
    if (flag2 & flag1 && HttpContext.Current == null && !((PXGraph) this).UnattendedMode)
      throw new PXRedirectRequiredException((PXGraph) this, "Branch", true);
  }

  public virtual int Persist(System.Type cacheType, PXDBOperation operation)
  {
    int num = ((PXGraph) this).Persist(cacheType, operation);
    if (cacheType == typeof (BranchMaint.BranchBAccount) && operation == 1)
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

  protected virtual void AfterPersist()
  {
  }

  protected virtual void BranchBAccount_Type_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "CP";
  }

  protected virtual void BranchBAccount_BranchBranchCD_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if (!(e.Table != (System.Type) null) || e.Operation != 1)
      return;
    e.IsRestriction = false;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ProcessActiveChange(
    BranchMaint.BranchBAccount branchBAccount,
    int? branchID)
  {
    bool? valueOriginal = (bool?) ((PXSelectBase) this.BAccount).Cache.GetValueOriginal<BranchMaint.BranchBAccount.active>((object) branchBAccount);
    bool? active = branchBAccount.Active;
    if (valueOriginal.GetValueOrDefault() == active.GetValueOrDefault() & valueOriginal.HasValue == active.HasValue)
      return;
    this.GetBranchValidator().ValidateActiveField(branchID.SingleToArray<int?>(), branchBAccount.Active, ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Current);
    if (!branchBAccount.Active.GetValueOrDefault())
      return;
    ((PXSelectBase) this.CurrentOrganizationView).Cache.SetStatus((object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.CurrentOrganizationView).Current, (PXEntryStatus) 1);
  }

  protected override int? BaccountIDForNewEmployee()
  {
    return ((PXSelectBase<BranchMaint.BranchBAccount>) this.BAccount).Current?.BAccountID;
  }

  [PXProjection(typeof (Select2<PX.Objects.CR.BAccount, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>, Where<True, Equal<True>>>), Persistent = true)]
  [PXCacheName("Branch")]
  [PXPrimaryGraph(typeof (BranchMaint))]
  [Serializable]
  public class BranchBAccount : PX.Objects.CR.BAccount
  {
    private string _BranchRoleName;
    protected int? _LedgerID;
    protected string _BranchPhoneMask;
    protected string _BranchCountryID;
    protected new byte[] _GroupMask;
    protected int? _AcctMapNbr;
    protected Guid? _DefaultPrinterID;

    [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.GL.Branch.branchCD))]
    [PXExtraKey]
    public virtual string BranchBranchCD
    {
      get => this._AcctCD;
      set => this._AcctCD = value;
    }

    /// <summary>
    /// Reference to <see cref="T:PX.Objects.GL.DAC.Organization" /> record to which the Branch belongs.
    /// </summary>
    [PXRestrictor(typeof (Where<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.withoutBranches>, And<BqlOperand<PX.Objects.GL.DAC.Organization.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.group>>>), "Only a company with branches can be specified.", new System.Type[] {})]
    [Organization(true, typeof (Search<PX.Objects.GL.DAC.Organization.organizationID>), typeof (IsNull<Current<RedirectBranchParameters.organizationID>, DefaultOrganizationID>), BqlField = typeof (PX.Objects.GL.Branch.organizationID))]
    public virtual int? OrganizationID { get; set; }

    [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.GL.Branch.roleName))]
    [PXSelector(typeof (Search<Roles.rolename, Where<Roles.guest, Equal<False>>>), DescriptionField = typeof (Roles.descr))]
    [PXUIField(DisplayName = "Access Role")]
    public string BranchRoleName
    {
      get => this._BranchRoleName;
      set => this._BranchRoleName = value;
    }

    [PXDBString(IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.GL.Branch.logoName))]
    [PXUIField(DisplayName = "Logo File")]
    public string BranchLogoName { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Logo File")]
    public string BranchLogoNameGetter
    {
      get => this.BranchLogoName;
      set
      {
      }
    }

    [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
    [PXDBString(IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.GL.Branch.mainLogoName))]
    public string BranchMainLogoName { get; set; }

    [PXDBString(IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.GL.Branch.logoNameReport))]
    [PXUIField(DisplayName = "Report Logo File")]
    public string BranchLogoNameReport { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Logo File")]
    public string BranchLogoNameReportGetter
    {
      get => this.BranchLogoNameReport;
      set
      {
      }
    }

    /// <summary>The name of the organization report logo image file.</summary>
    [PXDefault(typeof (Search<PX.Objects.GL.DAC.Organization.logoNameReport, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Optional2<BranchMaint.BranchBAccount.organizationID>>>>))]
    [PXDBString(IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.GL.Branch.organizationLogoNameReport))]
    [PXUIField(DisplayName = "Organization Logo File")]
    public string OrganizationLogoNameReport { get; set; }

    [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
    [PXDBInt(BqlField = typeof (PX.Objects.GL.Branch.ledgerID))]
    [PXUIField(DisplayName = "Posting Ledger")]
    [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.actualLedgerID))]
    [PXSelector(typeof (Search<PX.Objects.GL.Ledger.ledgerID, Where<PX.Objects.GL.Ledger.balanceType, Equal<PX.Objects.CM.ActualLedger>>>), DescriptionField = typeof (PX.Objects.GL.Ledger.descr), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), CacheGlobal = true)]
    public virtual int? LedgerID
    {
      get => this._LedgerID;
      set => this._LedgerID = value;
    }

    [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.GL.Branch.baseCuryID))]
    [PXUIField(DisplayName = "Base Currency ID")]
    [PXDBDefault(typeof (PX.Objects.GL.DAC.Organization.baseCuryID))]
    [PXSelector(typeof (Search<CurrencyList.curyID>))]
    public override string BaseCuryID { get; set; }

    [PXDBInt(BqlField = typeof (PX.Objects.GL.Branch.bAccountID))]
    [PXUIField(Visible = true, Enabled = false)]
    [PXExtraKey]
    public virtual int? BranchBAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.GL.Branch.active))]
    [PXUIField(DisplayName = "Active", FieldClass = "BRANCH")]
    [PXDefault(true)]
    public bool? Active { get; set; }

    [PXDimensionSelector("BRANCH", typeof (Search2<PX.Objects.CR.BAccount.acctCD, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>>), typeof (PX.Objects.CR.BAccount.acctCD), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName)})]
    [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXDefault]
    [PXUIField]
    public override string AcctCD
    {
      get => this._AcctCD;
      set => this._AcctCD = value;
    }

    [PXDBString(60, IsUnicode = true)]
    [PXDefault]
    [PXUIField]
    public override string AcctName
    {
      get => this._AcctName;
      set => this._AcctName = value;
    }

    [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.CR.BAccount.type))]
    public override string Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.CR.BAccount.acctReferenceNbr))]
    public override string AcctReferenceNbr
    {
      get => this._AcctReferenceNbr;
      set => this._AcctReferenceNbr = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.CR.BAccount.parentBAccountID))]
    public override int? ParentBAccountID
    {
      get => this._ParentBAccountID;
      set => this._ParentBAccountID = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.CR.BAccount.ownerID))]
    public override int? OwnerID
    {
      get => this._OwnerID;
      set => this._OwnerID = value;
    }

    [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
    [PXDBString(50, BqlField = typeof (PX.Objects.GL.Branch.phoneMask))]
    [PXDefault]
    [PXUIField(DisplayName = "Phone Mask")]
    public virtual string BranchPhoneMask
    {
      get => this._BranchPhoneMask;
      set => this._BranchPhoneMask = value;
    }

    [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.GL.Branch.countryID))]
    [PXUIField(DisplayName = "Default Country")]
    [PXSelector(typeof (Country.countryID), DescriptionField = typeof (Country.description))]
    public virtual string BranchCountryID
    {
      get => this._BranchCountryID;
      set => this._BranchCountryID = value;
    }

    /// <summary>Transmitter Control Code (TCC) for the 1099 form.</summary>
    [PXDBString(5, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXUIField(DisplayName = "Transmitter Control Code (TCC)")]
    public virtual string TCC { get; set; }

    /// <summary>
    /// Indicates whether the Branch is considered a Foreign Entity in the context of 1099 form.
    /// </summary>
    [PXDBBool(BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Foreign Entity")]
    public virtual bool? ForeignEntity { get; set; }

    /// <summary>Combined Federal/State Filer for the 1099 form.</summary>
    [PXDBBool(BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Combined Federal/State Filer")]
    public virtual bool? CFSFiler { get; set; }

    /// <summary>Contact Name for the 1099 form.</summary>
    [PXDBString(40, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXUIField(DisplayName = "Contact Name")]
    public virtual string ContactName { get; set; }

    /// <summary>Contact Phone Number for the 1099 form.</summary>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXUIField(DisplayName = "Contact Telephone Number")]
    public virtual string CTelNumber { get; set; }

    /// <summary>Contact E-mail for the 1099 form.</summary>
    [PXDBString(50, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXUIField(DisplayName = "Contact E-mail")]
    public virtual string CEmail { get; set; }

    /// <summary>Name Control for the 1099 form.</summary>
    [PXDBString(4, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXUIField(DisplayName = "Name Control")]
    public virtual string NameControl { get; set; }

    [PXDBBool(BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "1099-MISC Reporting Entity")]
    [PXUIVisible(typeof (Where<Selector<BranchMaint.BranchBAccount.organizationID, PX.Objects.GL.DAC.Organization.reporting1099ByBranches>, Equal<True>>))]
    public virtual bool? Reporting1099 { get; set; }

    [SingleGroup(BqlTable = typeof (PX.Objects.GL.Branch))]
    public new virtual byte[] GroupMask
    {
      get => this._GroupMask;
      set => this._GroupMask = value;
    }

    [PXDBInt(BqlTable = typeof (PX.Objects.GL.Branch))]
    [PXDefault(0)]
    public virtual int? AcctMapNbr
    {
      get => this._AcctMapNbr;
      set => this._AcctMapNbr = value;
    }

    [PXPrinterSelector]
    public virtual Guid? DefaultPrinterID
    {
      get => this._DefaultPrinterID;
      set => this._DefaultPrinterID = value;
    }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (PX.Objects.GL.Branch.carrierFacility))]
    [PXUIField(DisplayName = "Carrier Facility")]
    public virtual string CarrierFacility { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.GL.Branch.overrideThemeVariables))]
    [PXDefault(false)]
    [PXUIVisible(typeof (PXThemeVariableAttribute.ThemeHasVariables))]
    [PXUIField(DisplayName = "Override Colors for the Selected Branch")]
    public bool? OverrideThemeVariables { get; set; }

    [PXString(30, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Primary Color")]
    [PXThemeVariable("--primary-color", PersistDefaultValue = typeof (BranchMaint.BranchBAccount.overrideThemeVariables))]
    [PXUIEnabled(typeof (BranchMaint.BranchBAccount.overrideThemeVariables))]
    public string PrimaryColor { get; set; }

    [PXString(30, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Background Color")]
    [PXThemeVariable("--background-color", PersistDefaultValue = typeof (BranchMaint.BranchBAccount.overrideThemeVariables))]
    [PXUIEnabled(typeof (BranchMaint.BranchBAccount.overrideThemeVariables))]
    public string BackgroundColor { get; set; }

    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.bAccountID>
    {
    }

    public new abstract class defContactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.defContactID>
    {
    }

    public new abstract class defAddressID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.defAddressID>
    {
    }

    public new abstract class defLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.defLocationID>
    {
    }

    public abstract class branchBranchCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.branchBranchCD>
    {
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.organizationID>
    {
    }

    public abstract class branchRoleName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.branchRoleName>
    {
    }

    public abstract class branchLogoName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.branchLogoName>
    {
    }

    public abstract class mainLogoName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.mainLogoName>
    {
    }

    public abstract class branchLogoNameReport : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.branchLogoNameReport>
    {
    }

    public abstract class organizationLogoNameReport : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.organizationLogoNameReport>
    {
    }

    public abstract class ledgerID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BranchMaint.BranchBAccount.ledgerID>
    {
    }

    public new abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.baseCuryID>
    {
    }

    public abstract class branchBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.branchBAccountID>
    {
    }

    public abstract class active : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BranchMaint.BranchBAccount.active>
    {
    }

    public new abstract class acctCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.acctCD>
    {
    }

    public new abstract class acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.acctName>
    {
    }

    public new abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.type>
    {
    }

    public new abstract class acctReferenceNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.acctReferenceNbr>
    {
    }

    public new abstract class parentBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.parentBAccountID>
    {
    }

    public new abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.ownerID>
    {
    }

    public abstract class branchPhoneMask : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.branchPhoneMask>
    {
    }

    public abstract class branchCountryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.branchCountryID>
    {
    }

    public abstract class tCC : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BranchMaint.BranchBAccount.tCC>
    {
    }

    public abstract class foreignEntity : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.foreignEntity>
    {
    }

    public abstract class cFSFiler : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.cFSFiler>
    {
    }

    public abstract class contactName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.contactName>
    {
    }

    public abstract class cTelNumber : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.cTelNumber>
    {
    }

    public abstract class cEmail : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.cEmail>
    {
    }

    public abstract class nameControl : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.nameControl>
    {
    }

    public abstract class reporting1099 : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.reporting1099>
    {
    }

    public new abstract class groupMask : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.groupMask>
    {
    }

    public abstract class acctMapNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.acctMapNbr>
    {
    }

    public abstract class defaultPrinterID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.defaultPrinterID>
    {
    }

    public abstract class carrierFacility : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.carrierFacility>
    {
    }

    public abstract class overrideThemeVariables : IBqlField, IBqlOperand
    {
    }

    public abstract class primaryColor : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.primaryColor>
    {
    }

    public abstract class backgroundColor : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.backgroundColor>
    {
    }

    /// <summary>
    /// The registered entity for government payroll reporting.
    /// </summary>
    public new abstract class registeredEntityForReporting : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      BranchMaint.BranchBAccount.registeredEntityForReporting>
    {
    }
  }

  /// <exclude />
  public class DefContactAddressExt : 
    PX.Objects.CR.Extensions.DefContactAddressExt<BranchMaint, BranchMaint.BranchBAccount, BranchMaint.BranchBAccount.acctName>.WithPersistentAddressValidation
  {
    public override void Initialize()
    {
      base.Initialize();
      ((PXAction) this.Base.ActionsMenu).AddMenuAction((PXAction) this.ValidateAddresses);
    }

    protected virtual void BranchBAccount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (!(e.Row is BranchMaint.BranchBAccount))
        return;
      ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Current = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Select(Array.Empty<object>()));
      PX.Objects.GL.DAC.Organization current = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Current;
      bool flag1 = true;
      if (current?.OrganizationType == "WithoutBranches")
        flag1 = false;
      bool flag2 = !GraphHelper.IsPrimaryObjectInserted((PXGraph) this.Base);
      ((PXSelectBase) this.DefAddress).AllowUpdate = flag1;
      ((PXSelectBase) this.DefContact).AllowUpdate = flag1;
      ((PXAction) this.ValidateAddresses).SetEnabled(flag1 & flag2);
    }
  }

  /// <exclude />
  public class DefLocationExt : 
    PX.Objects.CR.Extensions.DefLocationExt<BranchMaint, BranchMaint.DefContactAddressExt, BranchMaint.LocationDetailsExt, BranchMaint.BranchBAccount, BranchMaint.BranchBAccount.bAccountID, BranchMaint.BranchBAccount.defLocationID>.WithUIExtension
  {
    [PXUIField]
    [PXButton(DisplayOnMainToolbar = false)]
    public void SetDefaultLocation()
    {
      BranchMaint.BranchBAccount current = ((PXSelectBase<BranchMaint.BranchBAccount>) this.Base.BAccount).Current;
      if (((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Current == null || current == null)
        return;
      int? locationId = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Current.LocationID;
      int? defLocationId = current.DefLocationID;
      if (locationId.GetValueOrDefault() == defLocationId.GetValueOrDefault() & locationId.HasValue == defLocationId.HasValue)
        return;
      current.DefLocationID = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.LocationDetailsExtension.Locations).Current.LocationID;
      ((PXSelectBase<BranchMaint.BranchBAccount>) this.Base.BAccount).Update(current);
    }

    [PXDBInt]
    [PXDBChildIdentity(typeof (PX.Objects.CR.Standalone.Location.locationID))]
    [PXUIField]
    [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<BranchMaint.BranchBAccount.bAccountID>>>>), DescriptionField = typeof (PX.Objects.CR.Location.locationCD), DirtyRead = true)]
    [PXMergeAttributes]
    protected override void _(
      PX.Data.Events.CacheAttached<BranchMaint.BranchBAccount.defLocationID> e)
    {
    }

    protected override void _(PX.Data.Events.RowSelected<BranchMaint.BranchBAccount> e)
    {
      PX.Objects.GL.DAC.Organization current = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Current;
      bool flag = true;
      if (current?.OrganizationType == "WithoutBranches")
        flag = false;
      ((PXSelectBase) this.DefLocation).AllowUpdate = flag;
      ((PXSelectBase) this.DefLocationAddress).AllowUpdate = flag;
      ((PXSelectBase) this.DefLocationContact).AllowUpdate = flag;
    }
  }

  /// <exclude />
  public class LocationDetailsExt : 
    PX.Objects.CR.Extensions.LocationDetailsExt<BranchMaint, BranchMaint.BranchBAccount, BranchMaint.BranchBAccount.bAccountID>
  {
    protected virtual void _(PX.Data.Events.RowPersisted<BranchMaint.BranchBAccount> e)
    {
      if (e.TranStatus != null || (e.Operation & 3) != 3)
        return;
      int? branchId = PXAccess.GetBranchID(e.Row.BranchBranchCD?.Trim());
      PXUpdate<Set<PX.Objects.CR.Standalone.Location.cBranchID, Null>, PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.cBranchID, Equal<Required<PX.Objects.CR.Standalone.Location.cBranchID>>>>.Update((PXGraph) this.Base, new object[1]
      {
        (object) branchId
      });
      PXUpdate<Set<PX.Objects.CR.Standalone.Location.vBranchID, Null>, PX.Objects.CR.Standalone.Location, Where<PX.Objects.CR.Standalone.Location.vBranchID, Equal<Required<PX.Objects.CR.Standalone.Location.vBranchID>>>>.Update((PXGraph) this.Base, new object[1]
      {
        (object) branchId
      });
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location> e)
    {
      base._(e);
      if (e.Row == null)
        return;
      PX.Objects.GL.DAC.Organization current = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Current;
      PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location>>) e).Cache, (object) null, !(current?.OrganizationType == "WithoutBranches"));
    }
  }

  /// <exclude />
  public class BranchMaintAddressLookupExtension : 
    AddressLookupExtension<BranchMaint, BranchMaint.BranchBAccount, PX.Objects.CR.Address>
  {
    protected override bool GetIsEnabled()
    {
      return base.GetIsEnabled() && ((PXSelectBase) ((PXGraph) this.Base).GetExtension<BranchMaint.DefContactAddressExt>().DefAddress).AllowUpdate;
    }

    protected override string AddressView => "DefAddress";

    protected override string ViewOnMap => "ViewMainOnMap";
  }

  /// <exclude />
  public class BranchMaintDefLocationAddressLookupExtension : 
    AddressLookupExtension<BranchMaint, BranchMaint.BranchBAccount, PX.Objects.CR.Address>
  {
    protected override bool GetIsEnabled()
    {
      return base.GetIsEnabled() && ((PXSelectBase) ((PXGraph) this.Base).GetExtension<BranchMaint.DefLocationExt>().DefLocation).AllowUpdate;
    }

    protected override string AddressView => "DefLocationAddress";

    protected override string ViewOnMap => "ViewDefLocationAddressOnMap";
  }

  /// <exclude />
  public class ExtendToCustomer : 
    OrganizationUnitExtendToCustomer<BranchMaint, BranchMaint.BranchBAccount>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.interBranch>();

    protected override ExtendToCustomerGraph<BranchMaint, BranchMaint.BranchBAccount>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToCustomerGraph<BranchMaint, BranchMaint.BranchBAccount>.SourceAccountMapping(typeof (BranchMaint.BranchBAccount));
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
    OrganizationUnitExtendToVendor<BranchMaint, BranchMaint.BranchBAccount>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.interBranch>();

    protected override ExtendToVendorGraph<BranchMaint, BranchMaint.BranchBAccount>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToVendorGraph<BranchMaint, BranchMaint.BranchBAccount>.SourceAccountMapping(typeof (BranchMaint.BranchBAccount));
    }

    public override void Initialize()
    {
      base.Initialize();
      ((PXAction) this.viewVendor).SetCategory("Other");
      ((PXAction) this.extendToVendor).SetCategory("Company Management");
    }
  }

  /// <exclude />
  public class BranchSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<BranchMaint, BranchMaint.BranchSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<BranchMaint, BranchMaint.BranchSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<BranchMaint, BranchMaint.BranchSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<BranchMaint, BranchMaint.BranchSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<BranchMaint, BranchMaint.BranchSharedContactOverrideGraphExt>.RelatedMapping(typeof (BranchMaint.BranchBAccount))
      {
        RelatedID = typeof (BranchMaint.BranchBAccount.bAccountID),
        ChildID = typeof (BranchMaint.BranchBAccount.defContactID)
      };
    }

    protected override CRParentChild<BranchMaint, BranchMaint.BranchSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<BranchMaint, BranchMaint.BranchSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class BranchSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<BranchMaint, BranchMaint.BranchSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<BranchMaint, BranchMaint.BranchSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<BranchMaint, BranchMaint.BranchSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<BranchMaint, BranchMaint.BranchSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<BranchMaint, BranchMaint.BranchSharedAddressOverrideGraphExt>.RelatedMapping(typeof (BranchMaint.BranchBAccount))
      {
        RelatedID = typeof (BranchMaint.BranchBAccount.bAccountID),
        ChildID = typeof (BranchMaint.BranchBAccount.defAddressID)
      };
    }

    protected override CRParentChild<BranchMaint, BranchMaint.BranchSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<BranchMaint, BranchMaint.BranchSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  public class BranchMaint_CRDuplicateBAccountIdentifier : 
    CRDuplicateBAccountIdentifier<BranchMaint, BranchMaint.BranchBAccount>
  {
  }
}
