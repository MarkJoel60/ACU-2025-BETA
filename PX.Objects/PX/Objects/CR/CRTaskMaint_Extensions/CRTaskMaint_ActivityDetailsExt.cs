// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTaskMaint_Extensions.CRTaskMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.CRTaskMaint_Extensions;

public class CRTaskMaint_ActivityDetailsExt : 
  ActivityDetailsExt_Child<CRTaskMaint, CRActivity, CRActivity.noteID>
{
  public override System.Type GetBAccountIDCommand() => typeof (CRActivity.bAccountID);

  public override System.Type GetContactIDCommand() => typeof (CRActivity.contactID);

  protected virtual void _(Events.RowSelected<PMTimeActivity> e)
  {
    PMTimeActivity row = e.Row;
    if (row == null)
      return;
    PMTimeActivity pmTimeActivity1 = row;
    PMTimeActivity pmTimeActivity2 = row;
    PMTimeActivity pmTimeActivity3 = row;
    PMTimeActivity pmTimeActivity4 = row;
    (int timeSpent, int overtimeSpent, int timeBillable, int overtimeBillable) childrenTimeTotals = this.GetChildrenTimeTotals((object) row.RefNoteID);
    int? nullable1 = new int?(childrenTimeTotals.timeSpent);
    int? nullable2 = new int?(childrenTimeTotals.overtimeSpent);
    int? nullable3 = new int?(childrenTimeTotals.timeBillable);
    int? nullable4 = new int?(childrenTimeTotals.overtimeBillable);
    int? nullable5 = nullable1;
    pmTimeActivity1.TimeSpent = nullable5;
    pmTimeActivity2.OvertimeSpent = nullable2;
    pmTimeActivity3.TimeBillable = nullable3;
    pmTimeActivity4.OvertimeBillable = nullable4;
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMTimeActivity>>) e).Cache;
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeSpent>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeSpent>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeBillable>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeBillable>(cache, (object) row, false);
  }

  protected virtual void _(Events.RowSelecting<PMTimeActivity> e)
  {
    using (new PXConnectionScope())
    {
      PMTimeActivity row = e.Row;
      if (row == null)
        return;
      PMTimeActivity pmTimeActivity1 = row;
      PMTimeActivity pmTimeActivity2 = row;
      PMTimeActivity pmTimeActivity3 = row;
      PMTimeActivity pmTimeActivity4 = row;
      (int timeSpent, int overtimeSpent, int timeBillable, int overtimeBillable) childrenTimeTotals = this.GetChildrenTimeTotals((object) row.RefNoteID);
      int? nullable1 = new int?(childrenTimeTotals.timeSpent);
      int? nullable2 = new int?(childrenTimeTotals.overtimeSpent);
      int? nullable3 = new int?(childrenTimeTotals.timeBillable);
      int? nullable4 = new int?(childrenTimeTotals.overtimeBillable);
      int? nullable5 = nullable1;
      pmTimeActivity1.TimeSpent = nullable5;
      pmTimeActivity2.OvertimeSpent = nullable2;
      pmTimeActivity3.TimeBillable = nullable3;
      pmTimeActivity4.OvertimeBillable = nullable4;
    }
  }
}
