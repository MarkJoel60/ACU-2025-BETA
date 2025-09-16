// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARScheduleRun
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.MigrationMode;
using PX.Objects.GL;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARScheduleRun : ScheduleRunBase<ARScheduleRun, ARScheduleMaint, ARScheduleProcess>
{
  public ARSetupNoMigrationMode ARSetup;
  public PXAction<ScheduleRun.Parameters> EditDetail;
  public PXAction<ScheduleRun.Parameters> NewSchedule;

  protected override bool checkAnyScheduleDetails => false;

  public ARScheduleRun()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_List).Join<LeftJoin<ARRegisterAccess, On<ARRegisterAccess.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<ARRegisterAccess.scheduled, Equal<True>, And<Not<Match<ARRegisterAccess, Current<AccessInfo.userName>>>>>>>>();
    ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_List).WhereAnd<Where<PX.Objects.GL.Schedule.module, Equal<BatchModule.moduleAR>, And<ARRegisterAccess.docType, IsNull>>>();
    ((PXSelectBase<PX.Objects.GL.Schedule>) this.Schedule_List).WhereAnd<Where<Exists<Select<ARRegister, Where<ARRegister.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<ARRegister.scheduled, Equal<True>>>>>>>();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter) => this.ViewScheduleAction(adapter);

  [PXUIField]
  [PXButton]
  public virtual IEnumerable newSchedule(PXAdapter adapter)
  {
    ARScheduleMaint instance = PXGraph.CreateInstance<ARScheduleMaint>();
    ((PXSelectBase<PX.Objects.GL.Schedule>) instance.Schedule_Header).Insert(new PX.Objects.GL.Schedule());
    ((PXSelectBase) instance.Schedule_Header).Cache.IsDirty = false;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "New Schedule");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }
}
