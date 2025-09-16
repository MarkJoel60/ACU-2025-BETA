// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.UsageScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class UsageScheduleMaint : PXGraph<UsageScheduleMaint>
{
  public PXCancel<FAUsageSchedule> Cancel;
  public PXSavePerRow<FAUsageSchedule, FAUsageSchedule.scheduleID> Save;
  public PXSelect<FAUsageSchedule> UsageSchedule;
  public PXSetup<PX.Objects.FA.FASetup> FASetup;

  public UsageScheduleMaint()
  {
    PX.Objects.FA.FASetup current = ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current;
  }

  protected virtual void FAUsageSchedule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FAUsageSchedule row = (FAUsageSchedule) e.Row;
    if (e.Operation != 3)
      return;
    if (PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.usageScheduleID, Equal<Current<FAUsageSchedule.scheduleID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXRowPersistingException("ScheduleCD", (object) row.ScheduleCD, "You cannot delete Schedule because transactions for this Schedule exist.");
  }

  protected virtual void FAUsageSchedule_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FAUsageSchedule row = (FAUsageSchedule) e.Row;
    if (PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.usageScheduleID, Equal<Current<FAUsageSchedule.scheduleID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException("You cannot delete Schedule because transactions for this Schedule exist.");
  }
}
