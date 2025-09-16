// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.OrganizationMaintDunningLetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.AR;

public class OrganizationMaintDunningLetter : PXGraphExtension<OrganizationMaint>
{
  public PXSetup<ARSetup> arSetup;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>();

  [PXOverride]
  public void Init(System.Action baseMethod)
  {
    baseMethod();
    bool flag = PXResultset<ARSetup>.op_Implicit(((PXSelectBase<ARSetup>) this.arSetup).Select(Array.Empty<object>()))?.PrepareDunningLetters == "C";
    PXUIFieldAttribute.SetReadOnly<BranchDunningLetter.isDunningCompanyBranchID>(((PXSelectBase) this.Base.BranchesView).Cache, (object) null, !flag);
    PXUIFieldAttribute.SetVisible<BranchDunningLetter.isDunningCompanyBranchID>(((PXSelectBase) this.Base.BranchesView).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisibility<BranchDunningLetter.isDunningCompanyBranchID>(((PXSelectBase) this.Base.BranchesView).Cache, (object) null, flag ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<PX.Objects.GL.Branch.organizationID, PX.Objects.GL.DAC.Organization.dunningFeeBranchID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<BranchDunningLetter.dunningCompanyBranchID> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BranchDunningLetter.dunningCompanyBranchID, IsNotNull>>>>.And<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BranchDunningLetter.dunningCompanyBranchID>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<BranchDunningLetter.isDunningCompanyBranchID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<BranchDunningLetter.isDunningCompanyBranchID> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Current.DunningFeeBranchID = ((bool?) e.NewValue).GetValueOrDefault() ? ((PX.Objects.GL.Branch) e.Row).BranchID : new int?();
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.OrganizationView).Cache, (object) ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Current);
    foreach (PXResult<PX.Objects.GL.Branch> pxResult in ((PXSelectBase<PX.Objects.GL.Branch>) this.Base.BranchesView).Select(Array.Empty<object>()))
    {
      PX.Objects.GL.Branch branch = PXResult<PX.Objects.GL.Branch>.op_Implicit(pxResult);
      int? branchId = branch.BranchID;
      int? dunningFeeBranchId = ((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Current.DunningFeeBranchID;
      if (!(branchId.GetValueOrDefault() == dunningFeeBranchId.GetValueOrDefault() & branchId.HasValue == dunningFeeBranchId.HasValue))
        ((PXSelectBase) this.Base.BranchesView).Cache.SetValue<BranchDunningLetter.isDunningCompanyBranchID>((object) branch, (object) false);
    }
    ((PXSelectBase) this.Base.BranchesView).View.RequestRefresh();
  }

  [PXOverride]
  public void Persist(System.Action baseMethod)
  {
    PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(((PXSelectBase<PX.Objects.GL.DAC.Organization>) this.Base.OrganizationView).Select(Array.Empty<object>()));
    PX.Objects.GL.DAC.Organization original = (PX.Objects.GL.DAC.Organization) ((PXSelectBase) this.Base.OrganizationView).Cache.GetOriginal((object) organization);
    using (new RunningFlagScope<OrganizationMaint>())
      baseMethod();
    if (organization == null)
      return;
    int? dunningFeeBranchId1 = organization.DunningFeeBranchID;
    int? dunningFeeBranchId2 = original.DunningFeeBranchID;
    if (dunningFeeBranchId1.GetValueOrDefault() == dunningFeeBranchId2.GetValueOrDefault() & dunningFeeBranchId1.HasValue == dunningFeeBranchId2.HasValue)
      return;
    ((PXSelectBase) this.Base.BranchesView).Cache.Clear();
  }
}
