// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Graphs.EPEventMaint.Extensions.EPEventMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.EP.Graphs.EPEventMaint.Extensions;

public class EPEventMaint_ActivityDetailsExt : 
  ActivityDetailsExt_Child<PX.Objects.EP.EPEventMaint, PX.Objects.CR.CRActivity, PX.Objects.CR.CRActivity.noteID>
{
  public override System.Type GetBAccountIDCommand() => typeof (PX.Objects.CR.CRActivity.bAccountID);

  public override System.Type GetContactIDCommand() => typeof (PX.Objects.CR.CRActivity.contactID);

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.CR.CRActivity> e)
  {
    PX.Objects.CR.CRActivity row = e.Row;
    if (row == null)
      return;
    string str = (string) e.Cache.GetValueOriginal<PX.Objects.CR.CRActivity.uistatus>((object) row) ?? "OP";
    bool flag = this.Base.IsCurrentUserCanEditEvent(row) && str == "OP";
    this.Activities.View.Cache.AllowUpdate = false;
    this.Activities.View.Cache.AllowInsert = flag;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTimeActivity> e)
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
    PXCache cache = e.Cache;
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeSpent>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeSpent>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeBillable>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeBillable>(cache, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PMTimeActivity> e)
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
