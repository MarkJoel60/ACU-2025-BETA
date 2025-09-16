// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZScheduleProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.WZ;

public class WZScheduleProcess : PXGraph<WZScheduleProcess>, IScheduleProcessing
{
  public PXSelect<Schedule> Running_Schedule;

  public virtual void GenerateProc(Schedule s)
  {
    this.GenerateProc(s, (short) 1, ((PXGraph) this).Accessinfo.BusinessDate.Value);
  }

  public virtual void GenerateProc(Schedule s, short Times, DateTime runDate)
  {
    IEnumerable<ScheduleDet> scheduleDets = new Scheduler((PXGraph) this).MakeSchedule(s, Times, new DateTime?(runDate));
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (ScheduleDet scheduleDet in scheduleDets)
      {
        foreach (PXResult<WZScenario> pxResult in PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.scheduleID, Equal<Required<Schedule.scheduleID>>, And<WZScenario.scheduled, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) s.ScheduleID
        }))
        {
          WZScenario wzScenario = PXResult<WZScenario>.op_Implicit(pxResult);
          wzScenario.ExecutionDate = scheduleDet.ScheduledDate;
          ((PXSelectBase<WZScenario>) instance.Scenario).Current = wzScenario;
          if (wzScenario.Status != "SU")
            ((PXAction) instance.activateScenarioWithoutRefresh).Press();
        }
        s.LastRunDate = scheduleDet.ScheduledDate;
        ((PXSelectBase) this.Running_Schedule).Cache.Update((object) s);
      }
      ((PXSelectBase) this.Running_Schedule).Cache.Persist((PXDBOperation) 1);
      transactionScope.Complete((PXGraph) this);
    }
  }
}
