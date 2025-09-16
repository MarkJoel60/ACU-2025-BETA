// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScheduleInq
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.DependencyInjection;
using PX.Data.Process;
using System;
using System.Collections;

#nullable enable
namespace PX.SM;

public class AUScheduleInq : PXGraph<
#nullable disable
AUScheduleInq>, IGraphWithInitialization
{
  public PXCancel<AUScheduleInq.AUScheduleExt> Cancel;
  public PXSelectRedirectSchedule Schedule_Redirect;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<AUScheduleInq.AUScheduleExt, OrderBy<Asc<AUSchedule.screenID, Asc<AUSchedule.scheduleID>>>> Schedule;
  public PXAction<AUScheduleInq.AUScheduleExt> viewScreen;
  public PXAction<AUScheduleInq.AUScheduleExt> viewHistory;
  public PXAction<AUScheduleInq.AUScheduleExt> RunSchedule;

  [InjectDependency]
  private IScheduleProcessorService ScheduleProcessorService { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    this.RunSchedule.SetEnabled(this.ScheduleProcessorService.CanStart);
  }

  protected IEnumerable schedule(PXAdapter adapter)
  {
    foreach (PXResult<AUScheduleInq.AUScheduleExt> pxResult in PXSelectBase<AUScheduleInq.AUScheduleExt, PXSelectReadonly3<AUScheduleInq.AUScheduleExt, OrderBy<Asc<AUSchedule.screenID, Asc<AUSchedule.scheduleID>>>>.Config>.Select((PXGraph) this))
    {
      AUScheduleInq.AUScheduleExt scheduleItem = (AUScheduleInq.AUScheduleExt) pxResult;
      if (string.IsNullOrEmpty(scheduleItem.ScreenID))
        yield return (object) scheduleItem;
      if (PXSiteMap.Provider.FindSiteMapNodeByScreenID(scheduleItem.ScreenID) != null)
        yield return (object) scheduleItem;
      scheduleItem = (AUScheduleInq.AUScheduleExt) null;
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "View Screen")]
  protected void ViewScreen()
  {
    AUScheduleInq.AUScheduleExt current = this.Schedule.Current;
    if (current == null || string.IsNullOrEmpty(current.ScreenID))
      return;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(current.ScreenID);
    if (mapNodeByScreenId != null && !string.IsNullOrEmpty(mapNodeByScreenId.Url))
      throw new PXRedirectToUrlException(mapNodeByScreenId.Url, nameof (ViewScreen));
  }

  [PXButton]
  [PXUIField(DisplayName = "View History")]
  protected void ViewHistory()
  {
    AUScheduleInq.AUScheduleExt current = this.Schedule.Current;
    if (current != null && current.ScheduleID.HasValue)
    {
      AUScheduleExecutionMaint instance = PXGraph.CreateInstance<AUScheduleExecutionMaint>();
      instance.Filter.Current.ScheduleID = current.ScheduleID;
      throw new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewHistory));
    }
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Initialize Scheduler")]
  protected void runSchedule() => this.ScheduleProcessorService.Start();

  public virtual void AUScheduleExt_RowSelected(PXCache cache, PXRowSelectedEventArgs args)
  {
    this.RunSchedule.SetEnabled(this.ScheduleProcessorService.CanStart);
    if (!(args.Row is AUScheduleInq.AUScheduleExt row))
      return;
    this.viewScreen.SetVisible(!string.IsNullOrEmpty(row.ScreenID));
    bool? isActive = row.IsActive;
    bool flag1 = false;
    int num;
    if (isActive.GetValueOrDefault() == flag1 & isActive.HasValue)
    {
      short? nullable1 = row.AbortCntr;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable1 = row.MaxAbortCount;
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      num = nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool flag2 = num != 0;
    cache.RaiseExceptionHandling<AUSchedule.isActive>((object) row, (object) row.IsActive, flag2 ? (Exception) new PXSetPropertyException<AUSchedule.isActive>("The automation schedule has been deactivated after reaching the maximum number of consecutive aborted executions.", PXErrorLevel.Warning) : (Exception) null);
  }

  [PXPrimaryGraph(typeof (AUScheduleMaint))]
  [Serializable]
  public class AUScheduleExt : AUSchedule
  {
    protected System.DateTime? _NextRunDateTime;

    [PXDefault]
    [PXDateAndTime(UseTimeZone = true, DisplayMask = "g", InputMask = "g")]
    [PXUIField(DisplayName = "Next Execution Date")]
    public new virtual System.DateTime? NextRunDateTime
    {
      [PXDependsOnFields(new System.Type[] {typeof (AUSchedule.nextRunDate), typeof (AUSchedule.nextRunTime)})] get
      {
        System.DateTime? nullable;
        System.DateTime dateTime;
        long num1;
        if (!this.NextRunDate.HasValue)
        {
          num1 = 0L;
        }
        else
        {
          nullable = this.NextRunDate;
          dateTime = nullable.Value;
          long ticks1 = dateTime.Ticks;
          dateTime = new System.DateTime(1900, 1, 1);
          long ticks2 = dateTime.Ticks;
          num1 = ticks1 - ticks2;
        }
        long num2 = 0L + num1;
        nullable = this.NextRunTime;
        long num3;
        if (!nullable.HasValue)
        {
          num3 = 0L;
        }
        else
        {
          nullable = this.NextRunTime;
          dateTime = nullable.Value;
          num3 = dateTime.Ticks;
        }
        long ticks = num2 + num3;
        if (ticks == 0L)
        {
          dateTime = System.DateTime.MaxValue;
          ticks = dateTime.Ticks;
        }
        return new System.DateTime?(new System.DateTime(ticks));
      }
    }

    public new abstract class nextRunDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      AUScheduleInq.AUScheduleExt.nextRunDateTime>
    {
    }
  }
}
