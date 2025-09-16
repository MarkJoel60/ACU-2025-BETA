// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.BranchMaintDunningLetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.AR;

public class BranchMaintDunningLetter : PXGraphExtension<BranchMaint>
{
  public PXSetup<ARSetup> arSetup;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>();

  [PXMergeAttributes]
  [PXDBCalced(typeof (PX.Objects.GL.Branch.branchID), typeof (int))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<BranchBAccountDunningLetter.branchBranchID> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<BranchMaint.BranchBAccount.organizationID, PX.Objects.GL.DAC.Organization.dunningFeeBranchID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<BranchBAccountDunningLetter.dunningFeeBranchID> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (IIf<Where<BranchBAccountDunningLetter.dunningFeeBranchID, IsNotNull, And<BranchBAccountDunningLetter.branchBranchID, Equal<BranchBAccountDunningLetter.dunningFeeBranchID>>>, True, False>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<BranchBAccountDunningLetter.isDunningCompanyBranchID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<BranchMaint.BranchBAccount> e)
  {
    PXUIFieldAttribute.SetVisible<BranchBAccountDunningLetter.isDunningCompanyBranchID>(((PXSelectBase) this.Base.CurrentBAccount).Cache, (object) null, false);
    if (e.Row == null || ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Current == null)
      return;
    ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(((PXSelectBase<ARSetup>) this.arSetup).Select(Array.Empty<object>()));
    PX.Objects.GL.DAC.Organization current = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Current;
    int num = arSetup?.PrepareDunningLetters == "C" ? 1 : 0;
    bool flag = current.OrganizationType == "WithoutBranches";
    if (num == 0 || flag)
      return;
    PXUIFieldAttribute.SetVisible<BranchBAccountDunningLetter.isDunningCompanyBranchID>(((PXSelectBase) this.Base.CurrentBAccount).Cache, (object) e.Row, true);
  }

  [PXOverride]
  public void AfterPersist()
  {
    if (RunningFlagScope<OrganizationMaint>.IsRunning)
      return;
    PX.Objects.GL.DAC.Organization current1 = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.CurrentOrganizationView).Current;
    if (current1.OrganizationType == "WithoutBranches")
      return;
    BranchMaint.BranchBAccount current2 = ((PXSelectBase<BranchMaint.BranchBAccount>) this.Base.CurrentBAccount).Current;
    if (current2 == null)
    {
      if (!current1.DunningFeeBranchID.HasValue)
        return;
      if (PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) current1.DunningFeeBranchID
      })) != null)
        return;
      PXDatabase.Update(typeof (PX.Objects.GL.DAC.Organization), new PXDataFieldParam[2]
      {
        (PXDataFieldParam) new PXDataFieldAssign("DunningFeeBranchID", (PXDbType) 8, (object) null),
        (PXDataFieldParam) new PXDataFieldRestrict("organizationID", (PXDbType) 8, (object) current1.OrganizationID)
      });
    }
    else
    {
      BranchBAccountDunningLetter extension = ((PXSelectBase) this.Base.CurrentBAccount).Cache.GetExtension<BranchBAccountDunningLetter>((object) current2);
      PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.bAccountID, Equal<Required<PX.Objects.GL.Branch.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) current2.BAccountID
      }));
      bool? dunningCompanyBranchId = extension.IsDunningCompanyBranchID;
      int? nullable;
      if (dunningCompanyBranchId.GetValueOrDefault())
      {
        int? dunningFeeBranchId = current1.DunningFeeBranchID;
        nullable = branch.BranchID;
        if (!(dunningFeeBranchId.GetValueOrDefault() == nullable.GetValueOrDefault() & dunningFeeBranchId.HasValue == nullable.HasValue))
        {
          PXDatabase.Update(typeof (PX.Objects.GL.DAC.Organization), new PXDataFieldParam[2]
          {
            (PXDataFieldParam) new PXDataFieldAssign("DunningFeeBranchID", (PXDbType) 8, (object) branch.BranchID),
            (PXDataFieldParam) new PXDataFieldRestrict("organizationID", (PXDbType) 8, (object) current1.OrganizationID)
          });
          ((PXSelectBase) this.Base.CurrentOrganizationView).Cache.Clear();
          ((PXGraph) this.Base).SelectTimeStamp();
          return;
        }
      }
      dunningCompanyBranchId = extension.IsDunningCompanyBranchID;
      if (dunningCompanyBranchId.GetValueOrDefault())
        return;
      nullable = current1.DunningFeeBranchID;
      int? branchId = branch.BranchID;
      if (!(nullable.GetValueOrDefault() == branchId.GetValueOrDefault() & nullable.HasValue == branchId.HasValue))
        return;
      PXDatabase.Update(typeof (PX.Objects.GL.DAC.Organization), new PXDataFieldParam[2]
      {
        (PXDataFieldParam) new PXDataFieldAssign("DunningFeeBranchID", (PXDbType) 8, (object) null),
        (PXDataFieldParam) new PXDataFieldRestrict("organizationID", (PXDbType) 8, (object) current1.OrganizationID)
      });
      ((PXSelectBase) this.Base.CurrentOrganizationView).Cache.Clear();
      ((PXGraph) this.Base).SelectTimeStamp();
    }
  }
}
