// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class CustomerMaintVisibilityRestriction : PXGraphExtension<CustomerMaint>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.Standalone.Location> DummyLocations;
  private bool? ResetLocationBranch;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<Customer>) this.Base.BAccount).WhereAnd<Where<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Restrict Visibility To", FieldClass = "VisibilityRestriction", Required = false)]
  public void Customer_COrgBAccountID_CacheAttached(PXCache sender)
  {
  }

  public void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Location> e)
  {
    if ((e.Operation & 3) != 1)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.DummyLocations).Cache, (object) PX.Objects.Common.Utilities.Clone<PX.Objects.CR.Location, PX.Objects.CR.Standalone.Location>((PXGraph) this.Base, e.Row));
    e.Cancel = true;
  }

  public void _(PX.Data.Events.CommandPreparing<PX.Objects.CR.Standalone.Location.noteID> e)
  {
    if ((e.Operation & 3) != 1)
      return;
    ((PX.Data.Events.Event<PXCommandPreparingEventArgs, PX.Data.Events.CommandPreparing<PX.Objects.CR.Standalone.Location.noteID>>) e).Args.ExcludeFromInsertUpdate();
  }

  public void _(PX.Data.Events.RowUpdating<Customer> e)
  {
    int? corgBaccountId1 = e.Row.COrgBAccountID;
    int? corgBaccountId2 = e.NewRow.COrgBAccountID;
    this.ResetLocationBranch = new bool?(!(corgBaccountId1.GetValueOrDefault() == corgBaccountId2.GetValueOrDefault() & corgBaccountId1.HasValue == corgBaccountId2.HasValue));
    int? corgBaccountId3 = e.Row.COrgBAccountID;
    int num1 = 0;
    if (corgBaccountId3.GetValueOrDefault() == num1 & corgBaccountId3.HasValue)
      return;
    int? corgBaccountId4 = e.NewRow.COrgBAccountID;
    int num2 = 0;
    if (!(corgBaccountId4.GetValueOrDefault() == num2 & corgBaccountId4.HasValue) || !((IEnumerable<PX.Objects.CR.Standalone.Location>) ((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this.Base).GetExtension<CustomerMaint.LocationDetailsExt>().Locations).Select<PX.Objects.CR.Standalone.Location>(Array.Empty<object>())).ToList<PX.Objects.CR.Standalone.Location>().Any<PX.Objects.CR.Standalone.Location>((Func<PX.Objects.CR.Standalone.Location, bool>) (l => l.CBranchID.HasValue)))
      return;
    this.ResetLocationBranch = new bool?(((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this.Base).GetExtension<CustomerMaint.LocationDetailsExt>().Locations).Ask("Warning", "Do you want to keep the value in the Shipping Branch box?", (MessageButtons) 4) == 7);
  }

  public void _(PX.Data.Events.RowUpdated<Customer> e)
  {
    if (!this.ResetLocationBranch.GetValueOrDefault())
      return;
    List<PXAccess.MasterCollection.Branch> branchList = PXAccess.GetOrganizationByBAccountID(e.Row.COrgBAccountID)?.ChildBranches;
    if (branchList == null)
    {
      PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(e.Row.COrgBAccountID);
      branchList = branchByBaccountId != null ? branchByBaccountId.SingleToList<PXAccess.MasterCollection.Branch>() : (List<PXAccess.MasterCollection.Branch>) null;
    }
    List<PXAccess.MasterCollection.Branch> source = branchList;
    HashSet<int> branches = source != null ? new HashSet<int>(source.Select<PXAccess.MasterCollection.Branch, int>((Func<PXAccess.MasterCollection.Branch, int>) (_ => _.BranchID))) : new HashSet<int>();
    int? defaultBranch = branches.Count == 1 ? new int?(branches.First<int>()) : new int?();
    foreach (PX.Objects.CR.Standalone.Location location in GraphHelper.RowCast<PX.Objects.CR.Standalone.Location>((IEnumerable) ((IEnumerable<PXResult<PX.Objects.CR.Standalone.Location>>) ((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this.Base).GetExtension<CustomerMaint.LocationDetailsExt>().Locations).Select(Array.Empty<object>())).ToList<PXResult<PX.Objects.CR.Standalone.Location>>()).Where<PX.Objects.CR.Standalone.Location>((Func<PX.Objects.CR.Standalone.Location, bool>) (l =>
    {
      if (defaultBranch.HasValue)
        return true;
      int? cbranchId = l.CBranchID;
      if (!cbranchId.HasValue)
        return false;
      HashSet<int> intSet = branches;
      cbranchId = l.CBranchID;
      int num = cbranchId.Value;
      return !intSet.Contains(num);
    })))
    {
      location.CBranchID = defaultBranch;
      if (GraphHelper.Caches<PX.Objects.CR.Standalone.Location>((PXGraph) this.Base).GetStatus(location) == null)
        GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<PX.Objects.CR.Standalone.Location>((PXGraph) this.Base), (object) location);
    }
  }

  [PXMergeAttributes]
  [Branch(null, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<Current<Customer.bAccountID>>>>>, Where<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>), true, true, false)]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.branchID, Inside<Current<Customer.cOrgBAccountID>>>), "The usage of the {0} customer is restricted in the {1} branch.", new System.Type[] {typeof (Customer.acctCD), typeof (PX.Objects.GL.Branch.branchCD)})]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.bAccountID, Equal<Current<Customer.cOrgBAccountID>>>>))]
  public void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cBranchID> e)
  {
  }
}
