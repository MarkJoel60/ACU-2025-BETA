// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ScheduleRun
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.GL;

[TableAndChartDashboardType]
public class ScheduleRun : ScheduleRunBase<
#nullable disable
ScheduleRun, ScheduleMaint, ScheduleProcess>
{
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXAction<ScheduleRun.Parameters> EditDetail;

  protected override bool checkAnyScheduleDetails => false;

  public ScheduleRun()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    ((PXSelectBase<Schedule>) this.Schedule_List).WhereAnd<Where<Schedule.module, Equal<BatchModule.moduleGL>>>();
    ((PXSelectBase<Schedule>) this.Schedule_List).WhereAnd<Where<Exists<Select<Batch, Where<Batch.scheduleID, Equal<Schedule.scheduleID>, And<Batch.scheduled, Equal<True>>>>>>>();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter) => this.ViewScheduleAction(adapter);

  [Obsolete("This method has been moved to ScheduleRunBase")]
  public static void SetProcessDelegate<ProcessGraph>(
    PXGraph graph,
    ScheduleRun.Parameters filter,
    PXProcessing<Schedule> view)
    where ProcessGraph : PXGraph<ProcessGraph>, IScheduleProcessing, new()
  {
    ScheduleRunBase.SetProcessDelegate<ProcessGraph>(graph, filter, view);
  }

  [Serializable]
  public class Parameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(1, IsFixed = true)]
    [PXUIField]
    [PXDefault("M")]
    [LabelList(typeof (ScheduleRunLimitType))]
    public virtual string LimitTypeSel { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? ExecutionDate { get; set; }

    [PXDBShort(MinValue = 1)]
    [PXUIField]
    [PXDefault(1)]
    public virtual short? RunLimit { get; set; }

    public abstract class limitTypeSel : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ScheduleRun.Parameters.limitTypeSel>
    {
    }

    public abstract class executionDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ScheduleRun.Parameters.executionDate>
    {
    }

    public abstract class runLimit : BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ScheduleRun.Parameters.runLimit>
    {
    }
  }
}
