// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectRedirectSchedule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXSelectRedirectSchedule(PXGraph graph) : 
  PXSelectRedirect<AUScheduleInq.AUScheduleExt, AUScheduleInq.AUScheduleExt>(graph)
{
  [PXButton]
  [PXUIField(DisplayName = "View", Visible = false)]
  public override IEnumerable ViewItem(PXAdapter adapter)
  {
    if (this.Current != null)
    {
      AUScheduleCurrentScreen scheduleCurrentScreen = new AUScheduleCurrentScreen();
      scheduleCurrentScreen.ScreenID = this.Current.ScreenID;
      AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
      instance.Schedule.Current = (AUSchedule) this.Current;
      instance.ScheduleCurrentScreen.Current = scheduleCurrentScreen;
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }
}
