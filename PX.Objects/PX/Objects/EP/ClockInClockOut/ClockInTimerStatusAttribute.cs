// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.ClockInTimerStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class ClockInTimerStatusAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber,
  IPXRowSelectedSubscriber,
  IPXRowInsertedSubscriber
{
  private System.Type _timerDataIDField;
  private System.Type _startDateField;

  public ClockInTimerStatusAttribute(System.Type timerDataIDField, System.Type startDateField)
  {
    this._timerDataIDField = timerDataIDField;
    this._startDateField = startDateField;
  }

  void IPXRowSelectingSubscriber.RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    using (new PXConnectionScope())
      this.SetClockInTimerStatus(sender, e.Row);
  }

  void IPXRowSelectedSubscriber.RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.SetClockInTimerStatus(sender, e.Row);
  }

  void IPXRowInsertedSubscriber.RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.SetClockInTimerStatus(sender, e.Row);
  }

  private void SetClockInTimerStatus(PXCache sender, object row)
  {
    if (row == null)
      return;
    string str = "U";
    int? nullable1 = sender.GetValue(row, this._timerDataIDField.Name) as int?;
    System.DateTime? nullable2 = sender.GetValue(row, this._startDateField.Name) as System.DateTime?;
    PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null);
    if (epEmployee != null)
    {
      EPEmployeeClockIn extension = epEmployee.GetExtension<EPEmployeeClockIn>();
      if (extension != null)
      {
        if (!nullable2.HasValue)
        {
          str = "S";
        }
        else
        {
          int? nullable3 = nullable1;
          int? activeClockInTimerId = extension.ActiveClockInTimerID;
          str = !(nullable3.GetValueOrDefault() == activeClockInTimerId.GetValueOrDefault() & nullable3.HasValue == activeClockInTimerId.HasValue) ? "P" : "R";
        }
      }
    }
    sender.SetValue(row, this._FieldName, (object) str);
  }
}
