// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZScenarioActivateProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.WZ;

[Serializable]
public class WZScenarioActivateProcess : PXGraph<WZScenarioActivateProcess>
{
  public PXCancel<PendingWZScenario> Cancel;
  [PXFilterable(new Type[] {})]
  public PXProcessing<Schedule, Where<Schedule.active, Equal<boolTrue>, And<Schedule.module, Equal<BatchModule.moduleWZ>>>> ScheduleList;

  public WZScenarioActivateProcess()
  {
    // ISSUE: method pointer
    ((PXProcessingBase<Schedule>) this.ScheduleList).SetProcessDelegate(new PXProcessingBase<Schedule>.ProcessListDelegate((object) null, __methodptr(Activate)));
  }

  public static void Activate(List<Schedule> list)
  {
    WZScheduleProcess instance = PXGraph.CreateInstance<WZScheduleProcess>();
    foreach (Schedule s in list)
      instance.GenerateProc(s);
    PXSiteMap.Provider.Clear();
  }
}
