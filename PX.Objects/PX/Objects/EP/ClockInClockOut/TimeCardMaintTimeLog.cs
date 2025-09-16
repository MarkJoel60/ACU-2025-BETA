// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.TimeCardMaintTimeLog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class TimeCardMaintTimeLog : PXGraphExtension<TimeCardMaint>
{
  [PXViewName("Time Logs")]
  public PXSelect<EPTimeLog, Where<EPTimeLog.employeeID, Equal<Current<EPTimeCard.employeeID>>>, PX.Data.OrderBy<Asc<EPTimeLog.startDate>>> TimeLogs;
  public PXAction<EPTimeCard> open;
  public PXAction<EPTimeCard> PreloadFromTimeLog;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>();

  public override void Initialize()
  {
    base.Initialize();
    string field1 = typeof (EPTimeLog.startDate).Name + "_Date";
    string field2 = typeof (EPTimeLog.startDate).Name + "_Time";
    this.Base.FieldUpdating.AddHandler(typeof (EPTimeLog), field1, new PXFieldUpdating(this.EPTimeLog_StartDate_Date_FieldUpdating));
    this.Base.FieldUpdating.AddHandler(typeof (EPTimeLog), field2, new PXFieldUpdating(this.EPTimeLog_StartDate_Time_FieldUpdating));
    string field3 = typeof (EPTimeLog.endDate).Name + "_Date";
    string field4 = typeof (EPTimeLog.endDate).Name + "_Time";
    this.Base.FieldUpdating.AddHandler(typeof (EPTimeLog), field3, new PXFieldUpdating(this.EPTimeLog_EndDate_Date_FieldUpdating));
    this.Base.FieldUpdating.AddHandler(typeof (EPTimeLog), field4, new PXFieldUpdating(this.EPTimeLog_EndDate_Time_FieldUpdating));
  }

  public virtual IEnumerable timeLogs()
  {
    object[] objArray = new object[0];
    BqlCommand select = this.TimeLogs.View.BqlSelect;
    EPTimeCard current = this.Base.Document.Current;
    int? weekId1;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      weekId1 = current.WeekID;
      num = weekId1.HasValue ? 1 : 0;
    }
    if (num != 0)
    {
      weekId1 = this.Base.Document.Current.WeekID;
      int weekId2 = weekId1.Value;
      select = select.WhereAnd<Where<EPTimeLog.startDate, GreaterEqual<P.AsDateTime>, And<EPTimeLog.startDate, LessEqual<P.AsDateTime>, Or<EPTimeLog.endDate, GreaterEqual<P.AsDateTime>, And<EPTimeLog.endDate, LessEqual<P.AsDateTime>>>>>>();
      System.DateTime weekStartDateUtc = PXWeekSelector2Attribute.GetWeekStartDateUtc((PXGraph) this.Base, weekId2);
      System.DateTime weekEndDateUtc = PXWeekSelector2Attribute.GetWeekEndDateUtc((PXGraph) this.Base, weekId2);
      objArray = new object[4]
      {
        (object) weekStartDateUtc,
        (object) weekEndDateUtc,
        (object) weekStartDateUtc,
        (object) weekEndDateUtc
      };
    }
    return (IEnumerable) new PXView((PXGraph) this.Base, false, select).SelectMulti(objArray);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void Open()
  {
    EPTimeLog current = this.TimeLogs.Current;
    if (current == null)
      return;
    new EntityHelper((PXGraph) this.Base).NavigateToRow(current.RelatedEntityType, current.RelatedEntityID, PXRedirectHelper.WindowMode.NewWindow);
  }

  [PXUIField(DisplayName = "Load from Time Log")]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable preloadFromTimeLog(PXAdapter adapter)
  {
    EPTimeCard current = this.Base.Document.Current;
    int? weekId1;
    int num1;
    if (current == null)
    {
      num1 = 1;
    }
    else
    {
      weekId1 = current.WeekID;
      num1 = !weekId1.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return adapter.Get();
    weekId1 = this.Base.Document.Current.WeekID;
    int weekId2 = weekId1.Value;
    System.DateTime weekStartDate = PXWeekSelector2Attribute.GetWeekStartDate((PXGraph) this.Base, weekId2);
    System.DateTime weekEndDateTime = PXWeekSelector2Attribute.GetWeekEndDateTime((PXGraph) this.Base, weekId2);
    Dictionary<string, IClockInClockOut> dictionary = new Dictionary<string, IClockInClockOut>();
    foreach (PXResult<EPTimeLog> pxResult in this.TimeLogs.Select())
    {
      EPTimeLog epTimeLog = (EPTimeLog) pxResult;
      System.DateTime dateTime1 = epTimeLog.StartDate.Value;
      System.DateTime dateTime2 = epTimeLog.EndDate.Value;
      TimeSpan timeSpan = dateTime2 - dateTime1;
      int num2 = (int) timeSpan.TotalDays + 1;
      for (int index = 0; index < num2; ++index)
      {
        bool flag1 = index == 0;
        bool flag2 = index == num2 - 1;
        System.DateTime dateTime3 = flag1 ? dateTime1 : dateTime1.AddDays((double) index).Date;
        int num3;
        if (flag2)
        {
          timeSpan = dateTime2 - dateTime3;
          num3 = (int) System.Math.Ceiling(timeSpan.TotalMinutes);
        }
        else if (flag1)
        {
          timeSpan = TimeSpan.FromMinutes(1440.0) - dateTime1.TimeOfDay;
          num3 = (int) System.Math.Ceiling(timeSpan.TotalMinutes);
        }
        else
          num3 = 1440;
        if (dateTime3 >= weekStartDate && dateTime3 <= weekEndDateTime)
        {
          TimeCardMaint.EPTimecardDetail row = new TimeCardMaint.EPTimecardDetail();
          EPEarningType epEarningType = (EPEarningType) PXSelectBase<EPEarningType, PXViewOf<EPEarningType>.BasedOn<SelectFromBase<EPEarningType, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EPTimeLogType>.On<BqlOperand<EPTimeLogType.earningTypeID, IBqlString>.IsEqual<EPEarningType.typeCD>>>>.Where<BqlOperand<EPTimeLogType.timeLogTypeID, IBqlString>.IsEqual<P.AsString>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) epTimeLog.TimeLogTypeID);
          if (epEarningType != null)
          {
            object typeCd = (object) epEarningType.TypeCD;
            try
            {
              this.Base.Activities.Cache.RaiseFieldVerifying<PMTimeActivity.earningTypeID>((object) row, ref typeCd);
              row.EarningTypeID = (string) typeCd;
            }
            catch
            {
            }
            this.Base.Activities.Cache.GetExtension<PMTimeActivityClockIn>((object) row).TimeLogID = epTimeLog.TimeLogID;
            row.TimeSpent = new int?(num3);
            row.Summary = epTimeLog.Summary;
            row.Date = new System.DateTime?(dateTime3);
            row.ReportedInTimeZoneID = epTimeLog.ReportedInTimeZoneID;
            IClockInClockOut implementation;
            if (!dictionary.TryGetValue(epTimeLog.RelatedEntityType, out implementation))
            {
              implementation = this.GetGraphBasedOnEntityType(epTimeLog.RelatedEntityType)?.FindImplementation<IClockInClockOut>();
              dictionary.Add(epTimeLog.RelatedEntityType, implementation);
            }
            if (implementation != null)
            {
              TimeLogData timeLogData = implementation.GetTimeLogData(epTimeLog.RelatedEntityID);
              object projectId = (object) timeLogData.ProjectID;
              object taskId = (object) timeLogData.TaskID;
              try
              {
                this.Base.Activities.Cache.RaiseFieldVerifying<TimeCardMaint.EPTimecardDetail.projectID>((object) row, ref projectId);
                row.ProjectID = (int?) projectId;
                this.Base.Activities.Cache.RaiseFieldVerifying<TimeCardMaint.EPTimecardDetail.projectTaskID>((object) row, ref taskId);
                row.ProjectTaskID = (int?) taskId;
              }
              catch
              {
              }
            }
            this.Base.Activities.Insert(row);
            epTimeLog.IsLocked = new bool?(true);
            this.TimeLogs.Update(epTimeLog);
          }
        }
      }
    }
    dictionary.Clear();
    this.Base.Save.Press();
    return adapter.Get();
  }

  private PXGraph GetGraphBasedOnEntityType(string entityType)
  {
    return PXGraph.CreateInstance(PXRedirectHelper.GetGraphType(this.Base.Caches[PXBuildManager.GetType(entityType, false)]));
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPTimeCard> e)
  {
    if (e.Row == null)
      return;
    this.PreloadFromTimeLog.SetEnabled(this.Base.preloadFromTasks.GetEnabled() && this.TimeLogs.SelectSingle() != null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPTimeLog> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.IsLocked.GetValueOrDefault())
      PXUIFieldAttribute.SetEnabled(e.Cache, (object) e.Row, false);
    if (string.IsNullOrEmpty(e.Row.RelatedEntityType) || e.Row.RelatedEntityID.HasValue)
      return;
    PXUIFieldAttribute.SetWarning<EPTimeLog.documentNbr>(e.Cache, (object) e.Row, "The document related to the time log has been deleted.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPTimeLog.startDate> e)
  {
    if (!(e.Row is EPTimeLog row) || !(e.NewValue is System.DateTime newValue))
      return;
    System.DateTime? endDate = row.EndDate;
    if (!endDate.HasValue)
      return;
    System.DateTime valueOrDefault = endDate.GetValueOrDefault();
    row.TimeSpent = new int?((int) System.Math.Ceiling((valueOrDefault - newValue).TotalMinutes));
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPTimeLog.timeSpent> e)
  {
    if (!(e.Row is EPTimeLog row) || !(e.NewValue is int newValue))
      return;
    System.DateTime? startDate = row.StartDate;
    if (!startDate.HasValue)
      return;
    System.DateTime valueOrDefault = startDate.GetValueOrDefault();
    row.EndDate = new System.DateTime?(valueOrDefault.AddMinutes((double) newValue));
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPTimeLog.endDate> e)
  {
    if (!(e.Row is EPTimeLog row) || !(e.NewValue is System.DateTime newValue))
      return;
    System.DateTime? startDate = row.StartDate;
    if (!startDate.HasValue)
      return;
    System.DateTime valueOrDefault = startDate.GetValueOrDefault();
    row.TimeSpent = new int?((int) System.Math.Ceiling((newValue - valueOrDefault).TotalMinutes));
  }

  protected virtual void EPTimeLog_StartDate_Date_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is EPTimeLog row) || !(e.NewValue is System.DateTime dateTime))
      return;
    System.DateTime? nullable = row.StartDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.EndDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault2 = nullable.GetValueOrDefault();
    dateTime = new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, valueOrDefault1.Hour, valueOrDefault1.Minute, valueOrDefault1.Second, valueOrDefault1.Millisecond, valueOrDefault1.Kind);
    if (dateTime > valueOrDefault2)
    {
      e.NewValue = (object) dateTime;
      throw new PXSetPropertyException<EPTimeLog.endDate>("The start date cannot be later than the end date.");
    }
  }

  protected virtual void EPTimeLog_StartDate_Time_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is EPTimeLog row) || !(e.NewValue is System.DateTime dateTime))
      return;
    System.DateTime? nullable = row.StartDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.EndDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault2 = nullable.GetValueOrDefault();
    dateTime = new System.DateTime(valueOrDefault1.Year, valueOrDefault1.Month, valueOrDefault1.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, valueOrDefault1.Kind);
    if (dateTime > valueOrDefault2)
    {
      e.NewValue = (object) dateTime;
      throw new PXSetPropertyException<EPTimeLog.startDate>("The start time cannot be later than the end time.");
    }
  }

  protected virtual void EPTimeLog_EndDate_Date_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is EPTimeLog row) || !(e.NewValue is System.DateTime dateTime))
      return;
    System.DateTime? nullable = row.StartDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.EndDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault2 = nullable.GetValueOrDefault();
    dateTime = new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, valueOrDefault2.Hour, valueOrDefault2.Minute, valueOrDefault2.Second, valueOrDefault2.Millisecond, valueOrDefault2.Kind);
    if (dateTime < valueOrDefault1)
    {
      e.NewValue = (object) dateTime;
      throw new PXSetPropertyException<EPTimeLog.endDate>("The end date cannot be earlier than the start date.");
    }
  }

  protected virtual void EPTimeLog_EndDate_Time_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is EPTimeLog row) || !(e.NewValue is System.DateTime dateTime))
      return;
    System.DateTime? nullable = row.StartDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.EndDate;
    if (!nullable.HasValue)
      return;
    System.DateTime valueOrDefault2 = nullable.GetValueOrDefault();
    dateTime = new System.DateTime(valueOrDefault2.Year, valueOrDefault2.Month, valueOrDefault2.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, valueOrDefault2.Kind);
    if (dateTime < valueOrDefault1)
    {
      e.NewValue = (object) dateTime;
      throw new PXSetPropertyException<EPTimeLog.endDate>("The end time cannot be earlier than the start time.");
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPTimeLog> e)
  {
    if (e.Row == null)
      return;
    if ((TimeCardMaint.EPTimecardDetail) PXSelectBase<TimeCardMaint.EPTimecardDetail, PXSelect<TimeCardMaint.EPTimecardDetail, Where<PMTimeActivityClockIn.timeLogID, Equal<Required<EPTimeLog.timeLogID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) e.Row.TimeLogID) != null)
      throw new PXException("The time log cannot be deleted because it is associated with at least one time activity.");
  }

  protected virtual void _(
    PX.Data.Events.RowDeleted<TimeCardMaint.EPTimecardDetail> e)
  {
    if (e.Row == null)
      return;
    PMTimeActivityClockIn extension = this.Base.Activities.Cache.GetExtension<PMTimeActivityClockIn>((object) e.Row);
    if (!extension.TimeLogID.HasValue)
      return;
    if ((TimeCardMaint.EPTimecardDetail) PXSelectBase<TimeCardMaint.EPTimecardDetail, PXSelect<TimeCardMaint.EPTimecardDetail, Where<PMTimeActivityClockIn.timeLogID, Equal<Required<EPTimeLog.timeLogID>>, And<TimeCardMaint.EPTimecardDetail.noteID, NotEqual<Required<TimeCardMaint.EPTimecardDetail.noteID>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) extension.TimeLogID, (object) e.Row.NoteID) != null)
      return;
    EPTimeLog epTimeLog = (EPTimeLog) PXSelectBase<EPTimeLog, PXSelect<EPTimeLog, Where<EPTimeLog.timeLogID, Equal<Required<EPTimeLog.timeLogID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) extension.TimeLogID);
    if (epTimeLog == null)
      return;
    epTimeLog.IsLocked = new bool?(false);
    this.TimeLogs.Update(epTimeLog);
  }
}
