// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Graphs.EPEventMaint.Extensions.EPEventMaint_ActivityDetailsExt_Actions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.EP.Graphs.EPEventMaint.Extensions;

public class EPEventMaint_ActivityDetailsExt_Actions : 
  ActivityDetailsExt_Child_Actions<EPEventMaint_ActivityDetailsExt, PX.Objects.EP.EPEventMaint, PX.Objects.CR.CRActivity, PX.Objects.CR.CRActivity.noteID>
{
  protected override void _(PX.Data.Events.RowSelected<PX.Objects.CR.CRActivity> e)
  {
    base._(e);
    PX.Objects.CR.CRActivity row = e.Row;
    if (row == null)
      return;
    string str = (string) e.Cache.GetValueOriginal<PX.Objects.CR.CRActivity.uistatus>((object) row) ?? "OP";
    bool isEnabled = str == "OP" || str == "DR" || str == "IP";
    this.RemoveActivity.SetVisible(true);
    this.RemoveActivity.SetEnabled(isEnabled);
    this.NewEvent.SetVisible(false);
    this.NewTask.SetVisible(false);
  }
}
