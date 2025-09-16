// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.ServiceScheduleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FA;

public class ServiceScheduleMaint : PXGraph<ServiceScheduleMaint>
{
  public PXCancel<FAServiceSchedule> Cancel;
  public PXSavePerRow<FAServiceSchedule, FAServiceSchedule.scheduleID> Save;
  public PXSelect<FAServiceSchedule> ServiceSchedule;
  public PXSetup<PX.Objects.FA.FASetup> FASetup;

  public ServiceScheduleMaint()
  {
    PX.Objects.FA.FASetup current = ((PXSelectBase<PX.Objects.FA.FASetup>) this.FASetup).Current;
  }

  protected virtual void FAServiceSchedule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FAServiceSchedule row = (FAServiceSchedule) e.Row;
    if (e.Operation != 3)
      return;
    if (PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.serviceScheduleID, Equal<Current<FAServiceSchedule.scheduleID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXRowPersistingException("ScheduleCD", (object) row.ScheduleCD, "You cannot delete Schedule because transactions for this Schedule exist.");
  }

  protected virtual void FAServiceSchedule_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FAServiceSchedule row = (FAServiceSchedule) e.Row;
    if (PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.serviceScheduleID, Equal<Current<FAServiceSchedule.scheduleID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      e.Row
    }, Array.Empty<object>()).Count > 0)
      throw new PXSetPropertyException("You cannot delete Schedule because transactions for this Schedule exist.");
  }
}
