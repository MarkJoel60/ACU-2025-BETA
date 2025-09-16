// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRTaskMaint_Extensions.CRTaskMaint_ActivityDetailsExt_Actions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.CRTaskMaint_Extensions;

public class CRTaskMaint_ActivityDetailsExt_Actions : 
  ActivityDetailsExt_Child_Actions<CRTaskMaint_ActivityDetailsExt, CRTaskMaint, CRActivity, CRActivity.noteID>
{
  protected override void _(Events.RowSelected<CRActivity> e)
  {
    base._(e);
    CRActivity row = e.Row;
    if (row == null)
      return;
    string str = (string) ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRActivity>>) e).Cache.GetValueOriginal<CRActivity.uistatus>((object) row) ?? "OP";
    bool flag = str == "OP" || str == "DR" || str == "IP";
    ((PXAction) this.RemoveActivity).SetVisible(true);
    ((PXAction) this.RemoveActivity).SetEnabled(flag);
    ((PXAction) this.NewEvent).SetVisible(false);
  }
}
