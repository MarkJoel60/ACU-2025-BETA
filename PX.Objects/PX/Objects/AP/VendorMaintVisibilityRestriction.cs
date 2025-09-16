// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class VendorMaintVisibilityRestriction : PXGraphExtension<VendorMaint>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.Standalone.Location> DummyLocations;
  private bool? ResetLocationBranch;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.visibilityRestriction>();

  public override void Initialize()
  {
    base.Initialize();
    this.Base.BAccount.WhereAnd<Where<Vendor.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }

  public void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Location> e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    this.DummyLocations.Cache.MarkUpdated((object) Utilities.Clone<PX.Objects.CR.Location, PX.Objects.CR.Standalone.Location>((PXGraph) this.Base, e.Row));
    e.Cancel = true;
  }

  public void _(PX.Data.Events.CommandPreparing<PX.Objects.CR.Standalone.Location.noteID> e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.Args.ExcludeFromInsertUpdate();
  }

  public void _(PX.Data.Events.RowUpdating<Vendor> e)
  {
    int? vorgBaccountId1 = e.Row.VOrgBAccountID;
    int? vorgBaccountId2 = e.NewRow.VOrgBAccountID;
    this.ResetLocationBranch = new bool?(!(vorgBaccountId1.GetValueOrDefault() == vorgBaccountId2.GetValueOrDefault() & vorgBaccountId1.HasValue == vorgBaccountId2.HasValue));
    int? vorgBaccountId3 = e.Row.VOrgBAccountID;
    int num1 = 0;
    if (vorgBaccountId3.GetValueOrDefault() == num1 & vorgBaccountId3.HasValue)
      return;
    int? vorgBaccountId4 = e.NewRow.VOrgBAccountID;
    int num2 = 0;
    if (!(vorgBaccountId4.GetValueOrDefault() == num2 & vorgBaccountId4.HasValue) || !((IEnumerable<PX.Objects.CR.Standalone.Location>) this.Base.GetExtension<VendorMaint.LocationDetailsExt>().Locations.Select<PX.Objects.CR.Standalone.Location>()).ToList<PX.Objects.CR.Standalone.Location>().Any<PX.Objects.CR.Standalone.Location>((Func<PX.Objects.CR.Standalone.Location, bool>) (l => l.VBranchID.HasValue)))
      return;
    this.ResetLocationBranch = new bool?(this.Base.GetExtension<VendorMaint.LocationDetailsExt>().Locations.Ask("Warning", "Do you want to keep the value in the Receiving Branch box?", MessageButtons.YesNo) == WebDialogResult.No);
  }

  public void _(PX.Data.Events.RowUpdated<Vendor> e)
  {
    if (!this.ResetLocationBranch.GetValueOrDefault())
      return;
    List<PXAccess.MasterCollection.Branch> branchList = PXAccess.GetOrganizationByBAccountID(e.Row.VOrgBAccountID)?.ChildBranches;
    if (branchList == null)
    {
      PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(e.Row.VOrgBAccountID);
      branchList = branchByBaccountId != null ? branchByBaccountId.SingleToList<PXAccess.MasterCollection.Branch>() : (List<PXAccess.MasterCollection.Branch>) null;
    }
    List<PXAccess.MasterCollection.Branch> source = branchList;
    HashSet<int> branches = source != null ? new HashSet<int>(source.Select<PXAccess.MasterCollection.Branch, int>((Func<PXAccess.MasterCollection.Branch, int>) (_ => _.BranchID))) : new HashSet<int>();
    int? defaultBranch = branches.Count == 1 ? new int?(branches.First<int>()) : new int?();
    foreach (PX.Objects.CR.Standalone.Location row in this.Base.GetExtension<VendorMaint.LocationDetailsExt>().Locations.Select().ToList<PXResult<PX.Objects.CR.Standalone.Location>>().RowCast<PX.Objects.CR.Standalone.Location>().Where<PX.Objects.CR.Standalone.Location>((Func<PX.Objects.CR.Standalone.Location, bool>) (l =>
    {
      if (defaultBranch.HasValue)
        return true;
      int? vbranchId = l.VBranchID;
      if (!vbranchId.HasValue)
        return false;
      HashSet<int> intSet = branches;
      vbranchId = l.VBranchID;
      int num = vbranchId.Value;
      return !intSet.Contains(num);
    })))
    {
      row.VBranchID = defaultBranch;
      if (this.Base.Caches<PX.Objects.CR.Standalone.Location>().GetStatus(row) == PXEntryStatus.Notchanged)
        this.Base.Caches<PX.Objects.CR.Standalone.Location>().MarkUpdated((object) row);
    }
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [Branch(null, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<Current<Vendor.bAccountID>>>>>, Where<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>), true, true, false, IsDetail = false, DisplayName = "Default Branch", BqlField = typeof (PX.Objects.CR.Standalone.Location.vBranchID), PersistingCheck = PXPersistingCheck.Nothing, IsEnabledWhenOneBranchIsAccessible = true)]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.branchID, Inside<Current<Vendor.vOrgBAccountID>>>), "The usage of the {0} vendor is restricted in the {1} branch.", new System.Type[] {typeof (Vendor.acctCD), typeof (PX.Objects.GL.Branch.branchCD)})]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.bAccountID, Equal<Current<Vendor.vOrgBAccountID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vBranchID> e)
  {
  }
}
