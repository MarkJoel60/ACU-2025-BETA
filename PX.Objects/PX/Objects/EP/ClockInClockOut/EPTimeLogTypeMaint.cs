// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EPTimeLogTypeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class EPTimeLogTypeMaint : PXGraph<EPTimeLogTypeMaint>
{
  public PXSelect<EPTimeLogType> TimeLogTypes;
  public PXSavePerRow<EPTimeLogType> Save;
  public PXCancel<EPTimeLogType> Cancel;

  protected virtual void _(PX.Data.Events.RowDeleting<EPTimeLogType> e)
  {
    EPSetupClockIn extension = this.Caches[typeof (EPSetup)].GetExtension<EPSetupClockIn>((object) (EPSetup) PXSelectBase<EPSetup, PXSelectReadonly<EPSetup>.Config>.Select((PXGraph) this));
    if (extension != null && extension.DefTimeLogTypeID == e.Row.TimeLogTypeID)
      throw new PXException("The type cannot be deleted because it is specified as the Default Time Log Type on the Time & Expenses Preferences (EP101000) form.");
    if ((EPTimeLog) PXSelectBase<EPTimeLog, PXSelectReadonly<EPTimeLog, Where<EPTimeLog.timeLogTypeID, Equal<Required<EPTimeLogType.timeLogTypeID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) e.Row.TimeLogTypeID) != null)
      throw new PXException("The type cannot be deleted because there is at least one time entry with this type.");
  }
}
