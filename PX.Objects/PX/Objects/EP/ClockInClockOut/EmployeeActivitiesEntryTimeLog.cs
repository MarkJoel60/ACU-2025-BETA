// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EmployeeActivitiesEntryTimeLog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class EmployeeActivitiesEntryTimeLog : PXGraphExtension<EmployeeActivitiesEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>();

  protected virtual void _(PX.Data.Events.RowPersisted<EPActivityApprove> e)
  {
    if (e.Operation != PXDBOperation.Delete || e.TranStatus != PXTranStatus.Open)
      return;
    PMTimeActivityClockIn extension = this.Base.Activity.Cache.GetExtension<PMTimeActivityClockIn>((object) e.Row);
    if (!extension.TimeLogID.HasValue)
      return;
    if ((EPActivityApprove) PXSelectBase<EPActivityApprove, PXSelect<EPActivityApprove, Where<PMTimeActivityClockIn.timeLogID, Equal<Required<EPTimeLog.timeLogID>>, And<EPActivityApprove.noteID, NotEqual<Required<EPActivityApprove.noteID>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) extension.TimeLogID, (object) e.Row.NoteID) != null)
      return;
    PXUpdate<Set<EPTimeLog.isLocked, False>, EPTimeLog, Where<EPTimeLog.timeLogID, Equal<Required<EPTimeLog.timeLogID>>>>.Update((PXGraph) this.Base, (object) extension.TimeLogID);
  }
}
