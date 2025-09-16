// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APScheduleRun
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.MigrationMode;
using PX.Objects.CS;
using PX.Objects.GL;
using System.Collections;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APScheduleRun : ScheduleRunBase<APScheduleRun, APScheduleMaint, APScheduleProcess>
{
  public APSetupNoMigrationMode APSetup;
  public PXAction<ScheduleRun.Parameters> ViewSchedule;
  public PXAction<ScheduleRun.Parameters> NewSchedule;

  [PXUIField(DisplayName = "", Visible = false, MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXEditDetailButton]
  public virtual IEnumerable viewSchedule(PXAdapter adapter) => this.ViewScheduleAction(adapter);

  [PXUIField(DisplayName = "New Schedule", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual IEnumerable newSchedule(PXAdapter adapter)
  {
    APScheduleMaint instance = PXGraph.CreateInstance<APScheduleMaint>();
    instance.Schedule_Header.Insert(new PX.Objects.GL.Schedule());
    instance.Schedule_Header.Cache.IsDirty = false;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "New Schedule");
    requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw requiredException;
  }

  protected override bool checkAnyScheduleDetails => false;

  public APScheduleRun()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.Schedule_List.Join<LeftJoin<APRegisterAccess, On<APRegisterAccess.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<APRegisterAccess.scheduled, Equal<boolTrue>, And<Not<Match<APRegisterAccess, Current<AccessInfo.userName>>>>>>>>();
    this.Schedule_List.WhereAnd<Where<PX.Objects.GL.Schedule.module, Equal<BatchModule.moduleAP>, And<APRegisterAccess.docType, IsNull>>>();
    this.Schedule_List.WhereAnd<Where<Exists<PX.Data.Select<APRegister, Where<APRegister.scheduleID, Equal<PX.Objects.GL.Schedule.scheduleID>, And<APRegister.scheduled, Equal<True>>>>>>>();
  }
}
