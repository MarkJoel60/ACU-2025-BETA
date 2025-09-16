// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ScheduleMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.DR;

public class ScheduleMaintMultipleBaseCurrencies : PXGraphExtension<ScheduleMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public void PerformReleaseCustomScheduleValidations(
    DRSchedule schedule,
    IEnumerable<DRScheduleDetail> details,
    ScheduleMaintMultipleBaseCurrencies.PerformReleaseCustomScheduleValidationsDelegate baseMethod)
  {
    if (!schedule.BAccountID.HasValue)
      return;
    BAccount baccount = (BAccount) PXSelectorAttribute.Select<DRSchedule.bAccountID>(((PXSelectBase) this.Base.Schedule).Cache, (object) schedule);
    if ((VisibilityRestriction.IsNotEmpty(baccount.COrgBAccountID) || VisibilityRestriction.IsNotEmpty(baccount.VOrgBAccountID)) && schedule.BaseCuryID != baccount.BaseCuryID)
      throw new PXException("The document cannot be released because the deferral schedule currency differs from the base currency of the {0} entity associated with the {1} account. ", new object[2]
      {
        (object) (VisibilityRestriction.IsNotEmpty(baccount.COrgBAccountID) ? PXAccess.GetBranchByBAccountID(baccount.COrgBAccountID) : PXAccess.GetBranchByBAccountID(baccount.VOrgBAccountID)).BranchCD,
        (object) baccount.AcctCD
      });
    foreach (DRScheduleDetail detail in details)
    {
      PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(detail.BranchID);
      if (schedule.BaseCuryID != branch.BaseCuryID)
        throw new PXException("The document cannot be released because the deferral schedule currency differs from the base currency of the {0} component branch.", new object[1]
        {
          (object) branch.BranchCD
        });
    }
  }

  public delegate void PerformReleaseCustomScheduleValidationsDelegate(
    DRSchedule schedule,
    IEnumerable<DRScheduleDetail> details);
}
