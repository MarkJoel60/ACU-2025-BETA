// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_AccessUsers
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.FS;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class SM_AccessUsers : PXGraphExtension<AccessUsers>
{
  [PXCopyPasteHiddenView]
  public PXSelect<FSGPSTrackingLocation, Where<FSGPSTrackingLocation.userName, Equal<Current<Users.username>>>, OrderBy<Asc<FSGPSTrackingLocation.weekDay>>> LocationTracking;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CSCalendar, InnerJoin<EPEmployee, On<EPEmployee.calendarID, Equal<CSCalendar.calendarID>>, InnerJoin<BAccount, On<BAccount.bAccountID, Equal<EPEmployee.bAccountID>>>>, Where<BAccount.defContactID, Equal<Current<Users.contactID>>>> UserCalendar;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.LocationTracking).Cache.AllowInsert = false;
    ((PXSelectBase) this.LocationTracking).Cache.AllowDelete = false;
    ((PXSelectBase) this.LocationTracking).Cache.AllowUpdate = false;
  }

  public virtual void EnableDisableLocationTracking(
    PXCache cache,
    UserPreferences userPreferencesRow,
    FSxUserPreferences fsxUserPreferencesRow)
  {
    PXUIFieldAttribute.SetEnabled<FSxUserPreferences.interval>(cache, (object) userPreferencesRow, fsxUserPreferencesRow.TrackLocation.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<FSxUserPreferences.distance>(cache, (object) userPreferencesRow, fsxUserPreferencesRow.TrackLocation.GetValueOrDefault());
    ((PXSelectBase) this.LocationTracking).Cache.AllowInsert = fsxUserPreferencesRow.TrackLocation.GetValueOrDefault();
    ((PXSelectBase) this.LocationTracking).Cache.AllowDelete = fsxUserPreferencesRow.TrackLocation.GetValueOrDefault();
    ((PXSelectBase) this.LocationTracking).Cache.AllowUpdate = fsxUserPreferencesRow.TrackLocation.GetValueOrDefault();
  }

  public virtual void SetWeeklyOnDayFlag(FSGPSTrackingLocation fsGPSTrackingLocationRow)
  {
    int? weekDay = fsGPSTrackingLocationRow.WeekDay;
    if (weekDay.HasValue)
    {
      switch (weekDay.GetValueOrDefault())
      {
        case 1:
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay1 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay2 = new bool?(true);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay3 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay4 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay5 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay6 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay7 = new bool?(false);
          return;
        case 2:
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay1 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay2 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay3 = new bool?(true);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay4 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay5 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay6 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay7 = new bool?(false);
          return;
        case 3:
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay1 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay2 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay3 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay4 = new bool?(true);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay5 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay6 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay7 = new bool?(false);
          return;
        case 4:
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay1 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay2 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay3 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay4 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay5 = new bool?(true);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay6 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay7 = new bool?(false);
          return;
        case 5:
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay1 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay2 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay3 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay4 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay5 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay6 = new bool?(true);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay7 = new bool?(false);
          return;
        case 6:
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay1 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay2 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay3 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay4 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay5 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay6 = new bool?(false);
          ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay7 = new bool?(true);
          return;
      }
    }
    ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay1 = new bool?(true);
    ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay2 = new bool?(false);
    ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay3 = new bool?(false);
    ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay4 = new bool?(false);
    ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay5 = new bool?(false);
    ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay6 = new bool?(false);
    ((FSGPSTrackingRequest) fsGPSTrackingLocationRow).WeeklyOnDay7 = new bool?(false);
  }

  public virtual void UpdateIntervalAndDistance(FSGPSTrackingLocation fsGPSTrackingLocationRow)
  {
    UserPreferences current = ((PXSelectBase<UserPreferences>) this.Base.UserPrefs).Current;
    if (current == null)
      return;
    FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(current);
    fsGPSTrackingLocationRow.Interval = new short?(extension.Interval ?? (short) 5);
    fsGPSTrackingLocationRow.Distance = new short?(extension.Distance ?? (short) 250);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<Users> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<Users> e)
  {
    if (e.Row == null)
      return;
    Users row = e.Row;
    UserPreferences current = ((PXSelectBase<UserPreferences>) this.Base.UserPrefs).Current;
    if (current == null)
      return;
    FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(current);
    if (extension == null || !extension.TrackLocation.GetValueOrDefault() || e.Operation != 2 && e.Operation != 1)
      return;
    if (current.TimeZone == null)
    {
      ((PXSelectBase) this.Base.UserPrefs).Cache.RaiseExceptionHandling<UserPreferences.timeZone>((object) current, (object) current.TimeZone, (Exception) new PXSetPropertyException("The Time Zone box cannot be empty if the Track Location check box is selected on the Location Tracking tab.", (PXErrorLevel) 4));
    }
    else
    {
      if (!((string) ((PXSelectBase) this.Base.UserPrefs).Cache.GetValueOriginal<UserPreferences.timeZone>((object) current) != current.TimeZone))
        return;
      foreach (PXResult<FSGPSTrackingLocation> pxResult in ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Select(Array.Empty<object>()))
      {
        FSGPSTrackingLocation trackingLocation = PXResult<FSGPSTrackingLocation>.op_Implicit(pxResult);
        ((FSGPSTrackingRequest) trackingLocation).TimeZoneID = current.TimeZone;
        ((PXSelectBase) this.LocationTracking).Cache.Update((object) trackingLocation);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisted<Users> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.trackLocation> e)
  {
    if (e.Row == null)
      return;
    UserPreferences row = e.Row;
    FSxUserPreferences extension = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.trackLocation>>) e).Cache.GetExtension<FSxUserPreferences>((object) row);
    if (extension == null)
      return;
    bool? nullable = extension.TrackLocation;
    bool oldValue = (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.trackLocation>, UserPreferences, object>) e).OldValue;
    if (nullable.GetValueOrDefault() == oldValue & nullable.HasValue)
      return;
    nullable = extension.TrackLocation;
    if (nullable.GetValueOrDefault() && ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Select(Array.Empty<object>()).Count == 0)
    {
      List<FSGPSTrackingLocation> trackingLocationList1 = new List<FSGPSTrackingLocation>();
      CSCalendar csCalendar = ((PXSelectBase<CSCalendar>) this.UserCalendar).SelectSingle(Array.Empty<object>());
      if (csCalendar != null)
      {
        nullable = csCalendar.SunWorkDay;
        if (nullable.GetValueOrDefault())
        {
          List<FSGPSTrackingLocation> trackingLocationList2 = trackingLocationList1;
          FSGPSTrackingLocation trackingLocation = new FSGPSTrackingLocation();
          ((FSGPSTrackingRequest) trackingLocation).WeeklyOnDay1 = new bool?(true);
          trackingLocation.WeekDay = new int?(0);
          ((FSGPSTrackingRequest) trackingLocation).StartTime = csCalendar.SunStartTime;
          ((FSGPSTrackingRequest) trackingLocation).EndTime = csCalendar.SunEndTime;
          trackingLocationList2.Add(trackingLocation);
        }
      }
      if (csCalendar != null)
      {
        nullable = csCalendar.MonWorkDay;
        if (nullable.GetValueOrDefault())
        {
          List<FSGPSTrackingLocation> trackingLocationList3 = trackingLocationList1;
          FSGPSTrackingLocation trackingLocation = new FSGPSTrackingLocation();
          ((FSGPSTrackingRequest) trackingLocation).WeeklyOnDay2 = new bool?(true);
          trackingLocation.WeekDay = new int?(1);
          ((FSGPSTrackingRequest) trackingLocation).StartTime = csCalendar.MonStartTime;
          ((FSGPSTrackingRequest) trackingLocation).EndTime = csCalendar.MonEndTime;
          trackingLocationList3.Add(trackingLocation);
        }
      }
      if (csCalendar != null)
      {
        nullable = csCalendar.TueWorkDay;
        if (nullable.GetValueOrDefault())
        {
          List<FSGPSTrackingLocation> trackingLocationList4 = trackingLocationList1;
          FSGPSTrackingLocation trackingLocation = new FSGPSTrackingLocation();
          ((FSGPSTrackingRequest) trackingLocation).WeeklyOnDay3 = new bool?(true);
          trackingLocation.WeekDay = new int?(2);
          ((FSGPSTrackingRequest) trackingLocation).StartTime = csCalendar.TueStartTime;
          ((FSGPSTrackingRequest) trackingLocation).EndTime = csCalendar.TueEndTime;
          trackingLocationList4.Add(trackingLocation);
        }
      }
      if (csCalendar != null)
      {
        nullable = csCalendar.WedWorkDay;
        if (nullable.GetValueOrDefault())
        {
          List<FSGPSTrackingLocation> trackingLocationList5 = trackingLocationList1;
          FSGPSTrackingLocation trackingLocation = new FSGPSTrackingLocation();
          ((FSGPSTrackingRequest) trackingLocation).WeeklyOnDay4 = new bool?(true);
          trackingLocation.WeekDay = new int?(3);
          ((FSGPSTrackingRequest) trackingLocation).StartTime = csCalendar.WedStartTime;
          ((FSGPSTrackingRequest) trackingLocation).EndTime = csCalendar.WedEndTime;
          trackingLocationList5.Add(trackingLocation);
        }
      }
      if (csCalendar != null)
      {
        nullable = csCalendar.ThuWorkDay;
        if (nullable.GetValueOrDefault())
        {
          List<FSGPSTrackingLocation> trackingLocationList6 = trackingLocationList1;
          FSGPSTrackingLocation trackingLocation = new FSGPSTrackingLocation();
          ((FSGPSTrackingRequest) trackingLocation).WeeklyOnDay5 = new bool?(true);
          trackingLocation.WeekDay = new int?(4);
          ((FSGPSTrackingRequest) trackingLocation).StartTime = csCalendar.ThuStartTime;
          ((FSGPSTrackingRequest) trackingLocation).EndTime = csCalendar.ThuEndTime;
          trackingLocationList6.Add(trackingLocation);
        }
      }
      if (csCalendar != null)
      {
        nullable = csCalendar.FriWorkDay;
        if (nullable.GetValueOrDefault())
        {
          List<FSGPSTrackingLocation> trackingLocationList7 = trackingLocationList1;
          FSGPSTrackingLocation trackingLocation = new FSGPSTrackingLocation();
          ((FSGPSTrackingRequest) trackingLocation).WeeklyOnDay6 = new bool?(true);
          trackingLocation.WeekDay = new int?(5);
          ((FSGPSTrackingRequest) trackingLocation).StartTime = csCalendar.FriStartTime;
          ((FSGPSTrackingRequest) trackingLocation).EndTime = csCalendar.FriEndTime;
          trackingLocationList7.Add(trackingLocation);
        }
      }
      if (csCalendar != null)
      {
        nullable = csCalendar.SatWorkDay;
        if (nullable.GetValueOrDefault())
        {
          List<FSGPSTrackingLocation> trackingLocationList8 = trackingLocationList1;
          FSGPSTrackingLocation trackingLocation = new FSGPSTrackingLocation();
          ((FSGPSTrackingRequest) trackingLocation).WeeklyOnDay7 = new bool?(true);
          trackingLocation.WeekDay = new int?(6);
          ((FSGPSTrackingRequest) trackingLocation).StartTime = csCalendar.SatStartTime;
          ((FSGPSTrackingRequest) trackingLocation).EndTime = csCalendar.SatEndTime;
          trackingLocationList8.Add(trackingLocation);
        }
      }
      foreach (FSGPSTrackingLocation trackingLocation in trackingLocationList1)
      {
        trackingLocation.StartDate = ((PXGraph) this.Base).Accessinfo.BusinessDate;
        trackingLocation.EndDate = new DateTime?(trackingLocation.StartDate.Value.AddYears(1000));
        trackingLocation.Interval = extension.Interval;
        trackingLocation.Distance = extension.Distance;
        ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Insert(trackingLocation);
      }
    }
    else
    {
      foreach (PXResult<FSGPSTrackingLocation> pxResult in ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Select(Array.Empty<object>()))
      {
        FSGPSTrackingLocation trackingLocation = PXResult<FSGPSTrackingLocation>.op_Implicit(pxResult);
        trackingLocation.IsActive = extension.TrackLocation;
        ((PXSelectBase) this.LocationTracking).Cache.Update((object) trackingLocation);
      }
    }
    PXPersistingCheck pxPersistingCheck = extension.TrackLocation.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXDefaultAttribute.SetPersistingCheck<UserPreferences.timeZone>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.trackLocation>>) e).Cache, (object) row, pxPersistingCheck);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<UserPreferences, UserPreferences.timeZone> e)
  {
    if (e.Row == null)
      return;
    UserPreferences row = e.Row;
    if (!(row.TimeZone != (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<UserPreferences, UserPreferences.timeZone>, UserPreferences, object>) e).OldValue))
      return;
    foreach (PXResult<FSGPSTrackingLocation> pxResult in ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Select(Array.Empty<object>()))
    {
      FSGPSTrackingLocation trackingLocation = PXResult<FSGPSTrackingLocation>.op_Implicit(pxResult);
      ((FSGPSTrackingRequest) trackingLocation).TimeZoneID = row.TimeZone;
      ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Update(trackingLocation);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.interval> e)
  {
    if (e.Row == null)
      return;
    FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(e.Row);
    short? interval = extension.Interval;
    int? nullable = interval.HasValue ? new int?((int) interval.GetValueOrDefault()) : new int?();
    int oldValue = (int) (short) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.interval>, UserPreferences, object>) e).OldValue;
    if (nullable.GetValueOrDefault() == oldValue & nullable.HasValue)
      return;
    foreach (PXResult<FSGPSTrackingLocation> pxResult in ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Select(Array.Empty<object>()))
    {
      FSGPSTrackingLocation trackingLocation = PXResult<FSGPSTrackingLocation>.op_Implicit(pxResult);
      trackingLocation.Interval = extension.Interval;
      ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Update(trackingLocation);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.distance> e)
  {
    if (e.Row == null)
      return;
    FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(e.Row);
    short? distance = extension.Distance;
    int? nullable = distance.HasValue ? new int?((int) distance.GetValueOrDefault()) : new int?();
    int oldValue = (int) (short) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<UserPreferences, FSxUserPreferences.distance>, UserPreferences, object>) e).OldValue;
    if (nullable.GetValueOrDefault() == oldValue & nullable.HasValue)
      return;
    foreach (PXResult<FSGPSTrackingLocation> pxResult in ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Select(Array.Empty<object>()))
    {
      FSGPSTrackingLocation trackingLocation = PXResult<FSGPSTrackingLocation>.op_Implicit(pxResult);
      trackingLocation.Interval = extension.Distance;
      ((PXSelectBase<FSGPSTrackingLocation>) this.LocationTracking).Update(trackingLocation);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelecting<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<UserPreferences> e)
  {
    if (e.Row == null)
      return;
    UserPreferences row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<UserPreferences>>) e).Cache;
    FSxUserPreferences extension = cache.GetExtension<FSxUserPreferences>((object) row);
    this.EnableDisableLocationTracking(cache, row, extension);
    if (!PXAccess.FeatureInstalled<FeaturesSet.gDPRCompliance>())
      return;
    PXSetPropertyException propertyException = extension.TrackLocation.GetValueOrDefault() ? new PXSetPropertyException("The legal aspects of the data privacy of your company must be taken into careful consideration before you activate tracking.", (PXErrorLevel) 2) : (PXSetPropertyException) null;
    cache.RaiseExceptionHandling<FSxUserPreferences.trackLocation>((object) e.Row, (object) extension.TrackLocation, (Exception) propertyException);
  }

  protected virtual void _(PX.Data.Events.RowInserting<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<UserPreferences> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<UserPreferences> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSGPSTrackingLocation, FSGPSTrackingLocation.weekDay> e)
  {
    if (e.Row == null)
      return;
    this.SetWeeklyOnDayFlag(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSGPSTrackingLocation> e)
  {
    if (e.Row == null)
      return;
    FSGPSTrackingLocation row = e.Row;
    bool? nullable = ((FSGPSTrackingRequest) row).WeeklyOnDay1;
    if (nullable.GetValueOrDefault())
    {
      row.WeekDay = new int?(0);
    }
    else
    {
      nullable = ((FSGPSTrackingRequest) row).WeeklyOnDay2;
      if (nullable.GetValueOrDefault())
      {
        row.WeekDay = new int?(1);
      }
      else
      {
        nullable = ((FSGPSTrackingRequest) row).WeeklyOnDay3;
        if (nullable.GetValueOrDefault())
        {
          row.WeekDay = new int?(2);
        }
        else
        {
          nullable = ((FSGPSTrackingRequest) row).WeeklyOnDay4;
          if (nullable.GetValueOrDefault())
          {
            row.WeekDay = new int?(3);
          }
          else
          {
            nullable = ((FSGPSTrackingRequest) row).WeeklyOnDay5;
            if (nullable.GetValueOrDefault())
            {
              row.WeekDay = new int?(4);
            }
            else
            {
              nullable = ((FSGPSTrackingRequest) row).WeeklyOnDay6;
              if (nullable.GetValueOrDefault())
              {
                row.WeekDay = new int?(5);
              }
              else
              {
                nullable = ((FSGPSTrackingRequest) row).WeeklyOnDay7;
                if (!nullable.GetValueOrDefault())
                  return;
                row.WeekDay = new int?(6);
              }
            }
          }
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSGPSTrackingLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSGPSTrackingLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSGPSTrackingLocation> e)
  {
    if (e.Row == null)
      return;
    FSGPSTrackingLocation row = e.Row;
    UserPreferences current = ((PXSelectBase<UserPreferences>) this.Base.UserPrefs).Current;
    if (current == null)
      return;
    FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(current);
    row.Interval = extension.Interval;
    row.Distance = extension.Distance;
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSGPSTrackingLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSGPSTrackingLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSGPSTrackingLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSGPSTrackingLocation> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSGPSTrackingLocation> e)
  {
    if (e.Row == null)
      return;
    FSGPSTrackingLocation row = e.Row;
    if (e.Operation != 2 && e.Operation != 1)
      return;
    this.SetWeeklyOnDayFlag(row);
    this.UpdateIntervalAndDistance(row);
    if (e.Operation == 2)
    {
      row.StartDate = ((PXGraph) this.Base).Accessinfo.BusinessDate;
      row.EndDate = new DateTime?(row.StartDate.Value.AddYears(1000));
    }
    if (((FSGPSTrackingRequest) row).TimeZoneID != null)
      return;
    ((PXSelectBase) this.Base.UserPrefs).Cache.RaiseExceptionHandling<UserPreferences.timeZone>((object) ((PXSelectBase<UserPreferences>) this.Base.UserPrefs).Current, (object) ((PXSelectBase<UserPreferences>) this.Base.UserPrefs).Current.TimeZone, (Exception) new PXSetPropertyException("The Time Zone box cannot be empty if the Track Location check box is selected on the Location Tracking tab.", (PXErrorLevel) 4));
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSGPSTrackingLocation>>) e).Cache.RaiseExceptionHandling<FSGPSTrackingLocation.weekDay>((object) row, (object) row.WeekDay, (Exception) new PXSetPropertyException("The Time Zone box cannot be empty if the Track Location check box is selected on the Location Tracking tab.", (PXErrorLevel) 5));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSGPSTrackingLocation> e)
  {
  }
}
