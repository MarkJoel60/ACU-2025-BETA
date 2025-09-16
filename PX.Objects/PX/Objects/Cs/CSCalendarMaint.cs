// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSCalendarMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS.Services.WorkTimeCalculation;
using PX.Objects.CT;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CS;

public class CSCalendarMaint : PXGraph<
#nullable disable
CSCalendarMaint, CSCalendar>
{
  public PXSelect<CSCalendar> Calendar;
  public PXSelect<CSCalendar, Where<CSCalendar.calendarID, Equal<Current<CSCalendar.calendarID>>>> CalendarDetails;
  public PXFilter<CSCalendarMaint.CSCalendarExceptionsParamsParameters> Filter;
  public PXSelect<PX.Objects.CS.CSCalendarExceptions> CSCalendarExceptions;
  public PXSelect<CSCalendarBreakTime, Where<CSCalendarBreakTime.calendarID, Equal<Current<CSCalendar.calendarID>>>> CalendarBreakTimes;

  protected virtual IEnumerable cSCalendarExceptions()
  {
    CSCalendarMaint csCalendarMaint = this;
    CSCalendarMaint.CSCalendarExceptionsParamsParameters header = ((PXSelectBase<CSCalendarMaint.CSCalendarExceptionsParamsParameters>) csCalendarMaint.Filter).Current;
    if (header != null)
    {
      foreach (PXResult<PX.Objects.CS.CSCalendarExceptions> pxResult in PXSelectBase<PX.Objects.CS.CSCalendarExceptions, PXSelect<PX.Objects.CS.CSCalendarExceptions, Where<PX.Objects.CS.CSCalendarExceptions.calendarID, Equal<Current<CSCalendar.calendarID>>>>.Config>.Select((PXGraph) csCalendarMaint, Array.Empty<object>()))
      {
        PX.Objects.CS.CSCalendarExceptions calendarExceptions = PXResult<PX.Objects.CS.CSCalendarExceptions>.op_Implicit(pxResult);
        int? yearId = header.YearID;
        if (yearId.HasValue)
        {
          yearId = calendarExceptions.YearID;
          int num = header.YearID.Value;
          if (!(yearId.GetValueOrDefault() == num & yearId.HasValue))
            continue;
        }
        yield return (object) calendarExceptions;
      }
    }
  }

  protected virtual void CSCalendar_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = (CSCalendar) e.Row;
    PXUIFieldAttribute.SetEnabled<CSCalendar.sunStartTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.SunWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.sunEndTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.SunWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.monStartTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.MonWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.monEndTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.MonWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.tueStartTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.TueWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.tueEndTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.TueWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.wedStartTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.WedWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.wedEndTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.WedWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.thuStartTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.ThuWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.thuEndTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.ThuWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.friStartTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.FriWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.friEndTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.FriWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.satStartTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.SatWorkDay.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CSCalendar.satEndTime>(((PXSelectBase) this.Calendar).Cache, (object) row, row.SatWorkDay.GetValueOrDefault());
    this.CalculateWorkTime(row);
  }

  protected virtual void CSCalendar_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CSCalendar newRow = (CSCalendar) e.NewRow;
    if (newRow == null)
      return;
    this.CalculateWorkTime(newRow);
  }

  protected virtual void CSCalendar_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CSCalendar row = (CSCalendar) e.Row;
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.sunStartTime>(sender, e.Row, row.SunWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.sunEndTime>(sender, e.Row, row.SunWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.monStartTime>(sender, e.Row, row.MonWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.monEndTime>(sender, e.Row, row.MonWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.tueStartTime>(sender, e.Row, row.TueWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.tueEndTime>(sender, e.Row, row.TueWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.wedStartTime>(sender, e.Row, row.WedWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.wedEndTime>(sender, e.Row, row.WedWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.thuStartTime>(sender, e.Row, row.ThuWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.thuEndTime>(sender, e.Row, row.ThuWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.friStartTime>(sender, e.Row, row.FriWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.friEndTime>(sender, e.Row, row.FriWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.satStartTime>(sender, e.Row, row.SatWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CSCalendar.satEndTime>(sender, e.Row, row.SatWorkDay.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool? nullable = row.SunWorkDay;
    if (nullable.HasValue && !nullable.GetValueOrDefault())
    {
      nullable = row.MonWorkDay;
      if (nullable.HasValue && !nullable.GetValueOrDefault())
      {
        nullable = row.TueWorkDay;
        if (nullable.HasValue && !nullable.GetValueOrDefault())
        {
          nullable = row.WedWorkDay;
          if (nullable.HasValue && !nullable.GetValueOrDefault())
          {
            nullable = row.ThuWorkDay;
            if (nullable.HasValue && !nullable.GetValueOrDefault())
            {
              nullable = row.FriWorkDay;
              if (nullable.HasValue && !nullable.GetValueOrDefault())
              {
                nullable = row.SatWorkDay;
                if (nullable.HasValue && !nullable.GetValueOrDefault())
                  throw new PXException("The calendar {0} should have the Day of Week check box selected for at least one weekday.", new object[1]
                  {
                    (object) row.CalendarID
                  });
              }
            }
          }
        }
      }
    }
    int? workdayTime = row.WorkdayTime;
    bool flag;
    if (workdayTime.HasValue)
    {
      int valueOrDefault = workdayTime.GetValueOrDefault();
      if (valueOrDefault <= 0 || valueOrDefault > 1440)
      {
        flag = true;
        goto label_12;
      }
    }
    flag = false;
label_12:
    if (flag)
      throw new PXException("The Workday Hours should be greater than 00:00 and less than or equal to 24:00.");
  }

  protected virtual void CSCalendar_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is CSCalendar))
      return;
    EPEmployeeClass epEmployeeClass = PXResultset<EPEmployeeClass>.op_Implicit(PXSelectBase<EPEmployeeClass, PXSelect<EPEmployeeClass, Where<EPEmployeeClass.calendarID, Equal<Current<CSCalendar.calendarID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (epEmployeeClass != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("This record cannot be deleted because it is referenced by Employee Class - {0}", new object[1]
      {
        (object) epEmployeeClass.VendorClassID
      });
    }
    Carrier carrier = PXResultset<Carrier>.op_Implicit(PXSelectBase<Carrier, PXSelect<Carrier, Where<Carrier.calendarID, Equal<Current<CSCalendar.calendarID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (carrier != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("This record cannot be deleted because it is referenced by Carrier - {0}", new object[1]
      {
        (object) carrier.CarrierID
      });
    }
    Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.calendarID, Equal<Current<CSCalendar.calendarID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (contract != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("This record cannot be deleted because it is referenced by Contract - {0}", new object[1]
      {
        (object) contract.ContractID
      });
    }
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.calendarID, Equal<Current<CSCalendar.calendarID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (epEmployee != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("This record cannot be deleted because it is referenced by Employee - {0}", new object[1]
      {
        (object) epEmployee.ClassID
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.sunWorkDay> e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = e.Row;
    bool? sunWorkDay = row.SunWorkDay;
    if (!sunWorkDay.HasValue || !sunWorkDay.GetValueOrDefault())
      return;
    DateTime? nullable = row.SunStartTime;
    if (!nullable.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.sunWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.sunStartTime>((object) row);
    nullable = row.SunEndTime;
    if (nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.sunWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.sunEndTime>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.monWorkDay> e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = e.Row;
    bool? monWorkDay = row.MonWorkDay;
    if (!monWorkDay.HasValue || !monWorkDay.GetValueOrDefault())
      return;
    DateTime? nullable = row.MonStartTime;
    if (!nullable.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.monWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.monStartTime>((object) row);
    nullable = row.MonEndTime;
    if (nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.monWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.monEndTime>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.tueWorkDay> e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = e.Row;
    bool? tueWorkDay = row.TueWorkDay;
    if (!tueWorkDay.HasValue || !tueWorkDay.GetValueOrDefault())
      return;
    DateTime? nullable = row.TueStartTime;
    if (!nullable.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.tueWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.tueStartTime>((object) row);
    nullable = row.TueEndTime;
    if (nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.tueWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.tueEndTime>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.wedWorkDay> e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = e.Row;
    bool? wedWorkDay = row.WedWorkDay;
    if (!wedWorkDay.HasValue || !wedWorkDay.GetValueOrDefault())
      return;
    DateTime? nullable = row.WedStartTime;
    if (!nullable.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.wedWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.wedStartTime>((object) row);
    nullable = row.WedEndTime;
    if (nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.wedWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.wedEndTime>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.thuWorkDay> e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = e.Row;
    bool? thuWorkDay = row.ThuWorkDay;
    if (!thuWorkDay.HasValue || !thuWorkDay.GetValueOrDefault())
      return;
    DateTime? nullable = row.ThuStartTime;
    if (!nullable.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.thuWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.thuStartTime>((object) row);
    nullable = row.ThuEndTime;
    if (nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.thuWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.thuEndTime>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.friWorkDay> e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = e.Row;
    bool? friWorkDay = row.FriWorkDay;
    if (!friWorkDay.HasValue || !friWorkDay.GetValueOrDefault())
      return;
    DateTime? nullable = row.FriStartTime;
    if (!nullable.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.friWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.friStartTime>((object) row);
    nullable = row.FriEndTime;
    if (nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.friWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.friEndTime>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.satWorkDay> e)
  {
    if (e.Row == null)
      return;
    CSCalendar row = e.Row;
    bool? satWorkDay = row.SatWorkDay;
    if (!satWorkDay.HasValue || !satWorkDay.GetValueOrDefault())
      return;
    DateTime? nullable = row.SatStartTime;
    if (!nullable.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.satWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.satStartTime>((object) row);
    nullable = row.SatEndTime;
    if (nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.satWorkDay>>) e).Cache.SetDefaultExt<CSCalendar.satEndTime>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.sunStartTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.sunStartTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.sunEndTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.sunEndTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(0);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.monStartTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.monStartTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(1);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.monEndTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.monEndTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(1);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.tueStartTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.tueStartTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(2);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.tueEndTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.tueEndTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(2);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.wedStartTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.wedStartTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(3);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.wedEndTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.wedEndTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(3);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.thuStartTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.thuStartTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(4);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.thuEndTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.thuEndTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(4);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.friStartTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.friStartTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(5);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.friEndTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.friEndTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(5);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.satStartTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.satStartTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(6);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.satEndTime> e)
  {
    if (e.NewValue == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CSCalendar, CSCalendar.satEndTime>, CSCalendar, object>) e).OldValue))
      return;
    this.RecalculateBreakTimes(6);
  }

  protected virtual void _(PX.Data.Events.RowInserted<CSCalendarBreakTime> e)
  {
    if (e.Row == null)
      return;
    this.RecalculateBreakTimes(e.Row.DayOfWeek.Value);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CSCalendarBreakTime> e)
  {
    if (e.Row == null)
      return;
    this.RecalculateBreakTimes(e.Row.DayOfWeek.Value);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CSCalendarBreakTime> e)
  {
    if (e.Row == null)
      return;
    this.RecalculateBreakTimes(e.Row.DayOfWeek.Value);
  }

  protected virtual void CSCalendarExceptions_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    PX.Objects.CS.CSCalendarExceptions row = (PX.Objects.CS.CSCalendarExceptions) e.Row;
    if (row.CalendarID == null && ((PXSelectBase<CSCalendar>) this.Calendar).Current != null)
      row.CalendarID = ((PXSelectBase<CSCalendar>) this.Calendar).Current.CalendarID;
    DateTime? nullable1 = row.Date;
    if (nullable1.HasValue)
    {
      PX.Objects.CS.CSCalendarExceptions calendarExceptions1 = row;
      nullable1 = row.Date;
      int? nullable2 = new int?(nullable1.Value.Year);
      calendarExceptions1.YearID = nullable2;
      PX.Objects.CS.CSCalendarExceptions calendarExceptions2 = row;
      nullable1 = row.Date;
      int? nullable3 = new int?((int) (nullable1.Value.DayOfWeek + 1));
      calendarExceptions2.DayOfWeek = nullable3;
    }
    else
    {
      PX.Objects.CS.CSCalendarExceptions calendarExceptions3 = row;
      nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
      int? nullable4 = new int?(nullable1.Value.Year);
      calendarExceptions3.YearID = nullable4;
      PX.Objects.CS.CSCalendarExceptions calendarExceptions4 = row;
      nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
      int? nullable5 = new int?((int) (nullable1.Value.DayOfWeek + 1));
      calendarExceptions4.DayOfWeek = nullable5;
    }
  }

  protected virtual void CSCalendarExceptions_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    PX.Objects.CS.CSCalendarExceptions newRow = (PX.Objects.CS.CSCalendarExceptions) e.NewRow;
    if (newRow.Date.HasValue)
    {
      newRow.YearID = new int?(newRow.Date.Value.Year);
      newRow.DayOfWeek = new int?((int) (newRow.Date.Value.DayOfWeek + 1));
    }
    else
    {
      newRow.YearID = new int?(((PXGraph) this).Accessinfo.BusinessDate.Value.Year);
      newRow.DayOfWeek = new int?((int) (((PXGraph) this).Accessinfo.BusinessDate.Value.DayOfWeek + 1));
    }
  }

  public virtual void RecalculateBreakTimes(int dayOfWeek)
  {
    CSCalendar current = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current;
    CalendarInfo calendarInfo = WorkTimeCalculatorProvider.ConvertToCalendarInfo(current, (IReadOnlyCollection<PX.Objects.CS.CSCalendarExceptions>) ((IEnumerable<PX.Objects.CS.CSCalendarExceptions>) ((PXSelectBase<PX.Objects.CS.CSCalendarExceptions>) this.CSCalendarExceptions).Select<PX.Objects.CS.CSCalendarExceptions>(Array.Empty<object>())).ToList<PX.Objects.CS.CSCalendarExceptions>(), (IReadOnlyCollection<CSCalendarBreakTime>) ((IEnumerable<CSCalendarBreakTime>) ((PXSelectBase<CSCalendarBreakTime>) this.CalendarBreakTimes).Select<CSCalendarBreakTime>(Array.Empty<object>())).ToList<CSCalendarBreakTime>());
    switch (dayOfWeek)
    {
      case 0:
        current.SunUnpaidTime = this.CalculateDayBreakTime(calendarInfo, dayOfWeek);
        break;
      case 1:
        current.MonUnpaidTime = this.CalculateDayBreakTime(calendarInfo, dayOfWeek);
        break;
      case 2:
        current.TueUnpaidTime = this.CalculateDayBreakTime(calendarInfo, dayOfWeek);
        break;
      case 3:
        current.WedUnpaidTime = this.CalculateDayBreakTime(calendarInfo, dayOfWeek);
        break;
      case 4:
        current.ThuUnpaidTime = this.CalculateDayBreakTime(calendarInfo, dayOfWeek);
        break;
      case 5:
        current.FriUnpaidTime = this.CalculateDayBreakTime(calendarInfo, dayOfWeek);
        break;
      case 6:
        current.SatUnpaidTime = this.CalculateDayBreakTime(calendarInfo, dayOfWeek);
        break;
      case 10:
        current.SunUnpaidTime = this.CalculateDayBreakTime(calendarInfo, 0);
        current.MonUnpaidTime = this.CalculateDayBreakTime(calendarInfo, 1);
        current.TueUnpaidTime = this.CalculateDayBreakTime(calendarInfo, 2);
        current.WedUnpaidTime = this.CalculateDayBreakTime(calendarInfo, 3);
        current.ThuUnpaidTime = this.CalculateDayBreakTime(calendarInfo, 4);
        current.FriUnpaidTime = this.CalculateDayBreakTime(calendarInfo, 5);
        current.SatUnpaidTime = this.CalculateDayBreakTime(calendarInfo, 6);
        break;
    }
    this.CalculateWorkTime(current);
  }

  [Obsolete]
  public virtual int? CalculateDayBreakTime(
    int dayOfWeek,
    DateTime? startDateTime,
    DateTime? endDateTime)
  {
    return new int?();
  }

  public virtual int? CalculateDayBreakTime(CalendarInfo calendarInfo, int dayOfWeek)
  {
    DayOfWeek key = (DayOfWeek) dayOfWeek;
    return new int?((int) calendarInfo.DaysOfWeek[key].GetBreakTimeDuration().TotalMinutes);
  }

  public virtual void CalculateWorkTime(CSCalendar calendar)
  {
    calendar.SunWorkTime = new int?(this.CalculateWorkTimeByDay(calendar.SunStartTime, calendar.SunEndTime, calendar.SunUnpaidTime.GetValueOrDefault()));
    calendar.MonWorkTime = new int?(this.CalculateWorkTimeByDay(calendar.MonStartTime, calendar.MonEndTime, calendar.MonUnpaidTime.GetValueOrDefault()));
    calendar.TueWorkTime = new int?(this.CalculateWorkTimeByDay(calendar.TueStartTime, calendar.TueEndTime, calendar.TueUnpaidTime.GetValueOrDefault()));
    calendar.WedWorkTime = new int?(this.CalculateWorkTimeByDay(calendar.WedStartTime, calendar.WedEndTime, calendar.WedUnpaidTime.GetValueOrDefault()));
    calendar.ThuWorkTime = new int?(this.CalculateWorkTimeByDay(calendar.ThuStartTime, calendar.ThuEndTime, calendar.ThuUnpaidTime.GetValueOrDefault()));
    calendar.FriWorkTime = new int?(this.CalculateWorkTimeByDay(calendar.FriStartTime, calendar.FriEndTime, calendar.FriUnpaidTime.GetValueOrDefault()));
    calendar.SatWorkTime = new int?(this.CalculateWorkTimeByDay(calendar.SatStartTime, calendar.SatEndTime, calendar.SatUnpaidTime.GetValueOrDefault()));
    bool? workdayTimeOverride = calendar.WorkdayTimeOverride;
    if (!workdayTimeOverride.HasValue || workdayTimeOverride.GetValueOrDefault())
      return;
    calendar.WorkdayTime = new int?(this.CalculateWorkdayTime(calendar));
  }

  public virtual int CalculateWorkTimeByDay(
    DateTime? startTime,
    DateTime? endTime,
    int breakTimeMinutes)
  {
    return !startTime.HasValue || !endTime.HasValue ? 0 : (int) ((DateTime.Compare(startTime.Value, endTime.Value) >= 0 ? endTime.Value.AddDays(1.0) : endTime.Value) - startTime.Value).TotalMinutes - breakTimeMinutes;
  }

  public virtual int CalculateWorkdayTime(CSCalendar calendar)
  {
    if (calendar == null)
      return 0;
    bool? nullable1 = calendar.SunWorkDay;
    int? nullable2;
    int num1;
    if (!nullable1.HasValue || !nullable1.GetValueOrDefault())
    {
      num1 = 0;
    }
    else
    {
      nullable2 = calendar.SunWorkTime;
      num1 = nullable2.GetValueOrDefault();
    }
    nullable1 = calendar.MonWorkDay;
    int num2;
    if (!nullable1.HasValue || !nullable1.GetValueOrDefault())
    {
      num2 = 0;
    }
    else
    {
      nullable2 = calendar.MonWorkTime;
      num2 = nullable2.GetValueOrDefault();
    }
    int num3 = num1 + num2;
    nullable1 = calendar.TueWorkDay;
    int num4;
    if (!nullable1.HasValue || !nullable1.GetValueOrDefault())
    {
      num4 = 0;
    }
    else
    {
      nullable2 = calendar.TueWorkTime;
      num4 = nullable2.GetValueOrDefault();
    }
    int num5 = num3 + num4;
    nullable1 = calendar.WedWorkDay;
    int num6;
    if (!nullable1.HasValue || !nullable1.GetValueOrDefault())
    {
      num6 = 0;
    }
    else
    {
      nullable2 = calendar.WedWorkTime;
      num6 = nullable2.GetValueOrDefault();
    }
    int num7 = num5 + num6;
    nullable1 = calendar.ThuWorkDay;
    int num8;
    if (!nullable1.HasValue || !nullable1.GetValueOrDefault())
    {
      num8 = 0;
    }
    else
    {
      nullable2 = calendar.ThuWorkTime;
      num8 = nullable2.GetValueOrDefault();
    }
    int num9 = num7 + num8;
    nullable1 = calendar.FriWorkDay;
    int num10;
    if (!nullable1.HasValue || !nullable1.GetValueOrDefault())
    {
      num10 = 0;
    }
    else
    {
      nullable2 = calendar.FriWorkTime;
      num10 = nullable2.GetValueOrDefault();
    }
    int num11 = num9 + num10;
    nullable1 = calendar.SatWorkDay;
    int num12;
    if (!nullable1.HasValue || !nullable1.GetValueOrDefault())
    {
      num12 = 0;
    }
    else
    {
      nullable2 = calendar.SatWorkTime;
      num12 = nullable2.GetValueOrDefault();
    }
    int num13 = num11 + num12;
    nullable1 = calendar.SunWorkDay;
    int num14 = nullable1.HasValue && nullable1.GetValueOrDefault() ? 1 : 0;
    nullable1 = calendar.MonWorkDay;
    int num15 = nullable1.HasValue && nullable1.GetValueOrDefault() ? 1 : 0;
    int num16 = num14 + num15;
    nullable1 = calendar.TueWorkDay;
    int num17 = nullable1.HasValue && nullable1.GetValueOrDefault() ? 1 : 0;
    int num18 = num16 + num17;
    nullable1 = calendar.WedWorkDay;
    int num19 = nullable1.HasValue && nullable1.GetValueOrDefault() ? 1 : 0;
    int num20 = num18 + num19;
    nullable1 = calendar.ThuWorkDay;
    int num21 = nullable1.HasValue && nullable1.GetValueOrDefault() ? 1 : 0;
    int num22 = num20 + num21;
    nullable1 = calendar.FriWorkDay;
    int num23 = nullable1.HasValue && nullable1.GetValueOrDefault() ? 1 : 0;
    int num24 = num22 + num23;
    nullable1 = calendar.SatWorkDay;
    int num25 = nullable1.HasValue && nullable1.GetValueOrDefault() ? 1 : 0;
    int num26 = num24 + num25;
    return num26 != 0 ? (int) Decimal.Round((Decimal) num13 / (Decimal) num26, 2, MidpointRounding.AwayFromZero) : 0;
  }

  public virtual DateTime? GetStartDateTimeOfDay(int dayOfWeek)
  {
    DateTime? startDateTimeOfDay = new DateTime?();
    switch (dayOfWeek)
    {
      case 0:
        startDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.SunStartTime;
        break;
      case 1:
        startDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.MonStartTime;
        break;
      case 2:
        startDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.TueStartTime;
        break;
      case 3:
        startDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.WedStartTime;
        break;
      case 4:
        startDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.ThuStartTime;
        break;
      case 5:
        startDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.FriStartTime;
        break;
      case 6:
        startDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.SatStartTime;
        break;
    }
    return startDateTimeOfDay;
  }

  public virtual DateTime? GetEndDateTimeOfDay(int dayOfWeek)
  {
    DateTime? endDateTimeOfDay = new DateTime?();
    switch (dayOfWeek)
    {
      case 0:
        endDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.SunEndTime;
        break;
      case 1:
        endDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.MonEndTime;
        break;
      case 2:
        endDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.TueEndTime;
        break;
      case 3:
        endDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.WedEndTime;
        break;
      case 4:
        endDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.ThuEndTime;
        break;
      case 5:
        endDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.FriEndTime;
        break;
      case 6:
        endDateTimeOfDay = ((PXSelectBase<CSCalendar>) this.CalendarDetails).Current.SatEndTime;
        break;
    }
    return endDateTimeOfDay;
  }

  [PXHidden]
  [Serializable]
  public class CSCalendarExceptionsParamsParameters : 
    PXBqlTable,
    IBqlTable,
    IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXUIField]
    [PXSelector(typeof (Search4<PX.Objects.CS.CSCalendarExceptions.yearID, Where<PX.Objects.CS.CSCalendarExceptions.calendarID, Equal<Current<CSCalendar.calendarID>>>, Aggregate<GroupBy<PX.Objects.CS.CSCalendarExceptions.yearID>>>), SubstituteKey = typeof (PX.Objects.CS.CSCalendarExceptions.yearIDAsString))]
    public virtual int? YearID { get; set; }

    public abstract class yearID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CSCalendarMaint.CSCalendarExceptionsParamsParameters.yearID>
    {
    }
  }
}
