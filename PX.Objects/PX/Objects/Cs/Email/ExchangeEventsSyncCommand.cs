// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.ExchangeEventsSyncCommand
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Update;
using PX.Data.Update.ExchangeService;
using PX.Data.Update.WebServices;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.EP.Graphs.EPEventMaint.Extensions;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CS.Email;

public class ExchangeEventsSyncCommand : 
  ExchangeActivitySyncCommand<PX.Objects.EP.EPEventMaint, CalendarItemType, CRActivity, CRActivity.synchronize, CRActivity.noteID, CRActivity.ownerID>
{
  public ExchangeEventsSyncCommand(MicrosoftExchangeSyncProvider provider)
    : base(provider, "Events", (PXExchangeFindOptions) 16 /*0x10*/)
  {
    if (!this.Policy.EventsSkipCategory.GetValueOrDefault())
      return;
    this.DefFindOptions = (PXExchangeFindOptions) (this.DefFindOptions | 4);
  }

  protected override PX.Objects.EP.EPEventMaint CreateGraphWithPresetPrimaryCache()
  {
    PX.Objects.EP.EPEventMaint presetPrimaryCache = base.CreateGraphWithPresetPrimaryCache();
    presetPrimaryCache.EnableForceEditEvent = true;
    return presetPrimaryCache;
  }

  protected override void ConfigureEnvironment(
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    this.EnsureEnvironmentConfigured<CalendarFolderType>(mailboxes, new PXSyncFolderSpecification(this.Policy.EventsSeparated.GetValueOrDefault() ? this.Policy.EventsFolder : (string) null, (DistinguishedFolderIdNameType) 0));
  }

  protected override BqlCommand GetSelectCommand()
  {
    return PXSelectBase<CRActivity, PXSelectReadonly2<CRActivity, InnerJoin<PX.Objects.CR.Contact, On<CRActivity.ownerID, Equal<PX.Objects.CR.Contact.contactID>>, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>>>, Where<CRActivity.classID, Equal<CRActivityClass.events>, And<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>>.Config>.GetCommand();
  }

  protected override PXSyncTag ExportInsertedAction(
    PXSyncMailbox account,
    CalendarItemType item,
    CRActivity activity)
  {
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), typeof (object)), parameterExpression1), item, (object) PXExchangeConversionHelper.ParceActivityPriority(activity.Priority));
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_IsAllDayEvent))), typeof (object)), parameterExpression2), item, (object) activity.AllDay);
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_Start))), typeof (object)), parameterExpression3), item, (object) activity.StartDate, activity.TimeZone, activity.AllDay.GetValueOrDefault());
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_End))), typeof (object)), parameterExpression4), item, (object) activity.EndDate, activity.TimeZone, activity.AllDay.GetValueOrDefault());
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_LegacyFreeBusyStatus))), typeof (object)), parameterExpression5), item, (object) PXExchangeConversionHelper.ParceShowAs(activity.ShowAsID));
    CRReminder current = this.graph.Reminder.Current;
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderIsSet))), typeof (object)), parameterExpression6), item, (object) (current != null));
    if (current != null && current.ReminderDate.HasValue)
    {
      ParameterExpression parameterExpression7;
      // ISSUE: method reference
      Expression<Func<CalendarItemType, object>> exp = Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderMinutesBeforeStart))), parameterExpression7);
      CalendarItemType calendarItemType = item;
      DateTime? startDate = activity.StartDate;
      DateTime? reminderDate = current.ReminderDate;
      string str = ((int) (startDate.HasValue & reminderDate.HasValue ? new TimeSpan?(startDate.GetValueOrDefault() - reminderDate.GetValueOrDefault()) : new TimeSpan?()).Value.TotalMinutes).ToString();
      this.ExportInsertedItemProperty<CalendarItemType>(exp, calendarItemType, (object) str);
    }
    if (!activity.IsPrivate.GetValueOrDefault())
    {
      ParameterExpression parameterExpression8;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_Location))), parameterExpression8), item, (object) activity.Location);
      List<AttendeeType> attendeeTypeList = new List<AttendeeType>();
      ((PXSelectBase<CRActivity>) this.graph.Events).Current = activity;
      foreach (PXResult<EPAttendee> pxResult in ((PXSelectBase<EPAttendee>) ((PXGraph) this.graph).GetExtension<EPEventMaint_AttendeeExt>().AttendeesExceptOwner).Select(Array.Empty<object>()))
      {
        EPAttendee epAttendee = PXResult<EPAttendee>.op_Implicit(pxResult);
        attendeeTypeList.Add(new AttendeeType()
        {
          Mailbox = new EmailAddressType()
          {
            EmailAddress = epAttendee.Email
          },
          ResponseType = PXExchangeConversionHelper.ParceAttendeeStatus(epAttendee.Invitation),
          ResponseTypeSpecified = true
        });
      }
      if (attendeeTypeList.Count > 0)
        item.RequiredAttendees = attendeeTypeList.ToArray();
    }
    return new PXSyncTag()
    {
      SendRequired = item.RequiredAttendees != null && item.RequiredAttendees.Length != 0,
      CancelRequired = activity.UIStatus == "CL"
    };
  }

  protected override PXSyncTag ExportUpdatedAction(
    PXSyncMailbox account,
    CalendarItemType item,
    CRActivity activity,
    List<ItemChangeDescriptionType> updates)
  {
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), typeof (object)), parameterExpression1), (UnindexedFieldURIType) 25, (object) PXExchangeConversionHelper.ParceActivityPriority(activity.Priority)));
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_IsAllDayEvent))), typeof (object)), parameterExpression2), (UnindexedFieldURIType) 107, (object) activity.AllDay));
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_Start))), typeof (object)), parameterExpression3), (UnindexedFieldURIType) 100, (object) activity.StartDate, activity.TimeZone, activity.AllDay.GetValueOrDefault()));
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_End))), typeof (object)), parameterExpression4), (UnindexedFieldURIType) 101, (object) activity.EndDate, activity.TimeZone, activity.AllDay.GetValueOrDefault()));
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_LegacyFreeBusyStatus))), typeof (object)), parameterExpression5), (UnindexedFieldURIType) 108, (object) PXExchangeConversionHelper.ParceShowAs(activity.ShowAsID)));
    CRReminder current = this.graph.Reminder.Current;
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderIsSet))), typeof (object)), parameterExpression6), (UnindexedFieldURIType) 40, (object) (current != null)));
    if (current != null)
    {
      DateTime? nullable = current.ReminderDate;
      if (nullable.HasValue)
      {
        List<ItemChangeDescriptionType> changeDescriptionTypeList = updates;
        ParameterExpression parameterExpression7;
        // ISSUE: method reference
        Expression<Func<CalendarItemType, object>> exp = Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderMinutesBeforeStart))), parameterExpression7);
        nullable = activity.StartDate;
        DateTime? reminderDate = current.ReminderDate;
        string str = ((int) (nullable.HasValue & reminderDate.HasValue ? new TimeSpan?(nullable.GetValueOrDefault() - reminderDate.GetValueOrDefault()) : new TimeSpan?()).Value.TotalMinutes).ToString();
        IEnumerable<SetItemFieldType> setItemFieldTypes = this.ExportUpdatedItemProperty<CalendarItemType>(exp, (UnindexedFieldURIType) 42, (object) str);
        Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) setItemFieldTypes);
      }
    }
    List<AttendeeType> attendeeTypeList = new List<AttendeeType>();
    if (!activity.IsPrivate.GetValueOrDefault())
    {
      ParameterExpression parameterExpression8;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_Location))), parameterExpression8), (UnindexedFieldURIType) 109, (object) activity.Location));
      ((PXSelectBase<CRActivity>) this.graph.Events).Current = activity;
      foreach (PXResult<EPAttendee> pxResult in ((PXSelectBase<EPAttendee>) ((PXGraph) this.graph).GetExtension<EPEventMaint_AttendeeExt>().AttendeesExceptOwner).Select(Array.Empty<object>()))
      {
        EPAttendee epAttendee = PXResult<EPAttendee>.op_Implicit(pxResult);
        attendeeTypeList.Add(new AttendeeType()
        {
          Mailbox = new EmailAddressType()
          {
            EmailAddress = epAttendee.Email
          },
          ResponseType = PXExchangeConversionHelper.ParceAttendeeStatus(epAttendee.Invitation),
          ResponseTypeSpecified = true
        });
      }
      if (attendeeTypeList.Count > 0)
      {
        List<ItemChangeDescriptionType> changeDescriptionTypeList = updates;
        SetItemFieldType setItemFieldType = new SetItemFieldType();
        ((ChangeDescriptionType) setItemFieldType).Item = (BasePathToElementType) new PathToUnindexedFieldType()
        {
          FieldURI = (UnindexedFieldURIType) 120
        };
        setItemFieldType.Item1 = (ItemType) new CalendarItemType()
        {
          RequiredAttendees = attendeeTypeList.ToArray()
        };
        changeDescriptionTypeList.Add((ItemChangeDescriptionType) setItemFieldType);
      }
    }
    return new PXSyncTag()
    {
      SendRequired = attendeeTypeList.Count > 0,
      CancelRequired = activity.UIStatus == "CL"
    };
  }

  protected override PXSyncTag ImportAction(
    PXSyncMailbox account,
    CalendarItemType item,
    ref CRActivity activity)
  {
    if (item.Organizer != null && item.Organizer.Item != null && string.Compare(item.Organizer.Item.EmailAddress, account.Address, StringComparison.CurrentCultureIgnoreCase) != 0)
      return (PXSyncTag) null;
    this.PrepareActivity(account, item, true, ref activity);
    if (activity == null)
      return (PXSyncTag) null;
    activity.ClassID = new int?(1);
    CRActivity copy = activity;
    if (((ItemType) item).Sensitivity == 2)
    {
      activity.IsPrivate = new bool?(true);
      activity.Subject = "Private event";
      activity.Body = "Private event";
    }
    else
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      this.ImportItemProperty<CalendarItemType, string>(Expression.Lambda<Func<CalendarItemType, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_Location))), parameterExpression), item, (Action<string>) (v => copy.Location = v));
    }
    string itemTimeZone = GetItemTimeZone();
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.ImportItemProperty<CalendarItemType, bool>(Expression.Lambda<Func<CalendarItemType, bool>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_IsAllDayEvent))), parameterExpression1), item, (Action<bool>) (v => copy.AllDay = new bool?(v)));
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    Expression<Func<CalendarItemType, DateTime>> exp1 = Expression.Lambda<Func<CalendarItemType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_Start))), parameterExpression2);
    CalendarItemType calendarItemType1 = item;
    Action<DateTime> setter1 = (Action<DateTime>) (v => copy.StartDate = new DateTime?(v));
    string exchTimezone1 = itemTimeZone;
    bool? allDay = copy.AllDay;
    int num1 = allDay.GetValueOrDefault() ? 1 : 0;
    this.ImportItemProperty<CalendarItemType, DateTime>(exp1, calendarItemType1, setter1, exchTimezone: exchTimezone1, isAllDay: num1 != 0);
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    Expression<Func<CalendarItemType, DateTime>> exp2 = Expression.Lambda<Func<CalendarItemType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_End))), parameterExpression3);
    CalendarItemType calendarItemType2 = item;
    Action<DateTime> setter2 = (Action<DateTime>) (v => copy.EndDate = new DateTime?(v));
    string exchTimezone2 = itemTimeZone;
    allDay = copy.AllDay;
    int num2 = allDay.GetValueOrDefault() ? 1 : 0;
    this.ImportItemProperty<CalendarItemType, DateTime>(exp2, calendarItemType2, setter2, exchTimezone: exchTimezone2, isAllDay: num2 != 0);
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    this.ImportItemProperty<CalendarItemType, LegacyFreeBusyType>(Expression.Lambda<Func<CalendarItemType, LegacyFreeBusyType>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CalendarItemType.get_LegacyFreeBusyStatus))), parameterExpression4), item, (Action<LegacyFreeBusyType>) (v => copy.ShowAsID = new int?(PXExchangeConversionHelper.ParceShowAs(v))));
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    this.ImportItemProperty<CalendarItemType, ImportanceChoicesType>(Expression.Lambda<Func<CalendarItemType, ImportanceChoicesType>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), parameterExpression5), item, (Action<ImportanceChoicesType>) (v => copy.Priority = new int?(PXExchangeConversionHelper.ParceActivityPriority(v))));
    copy.TimeZone = itemTimeZone;
    if (((ItemType) item).ReminderIsSet && ((ItemType) item).ReminderNextTimeSpecified)
    {
      CRReminder reminder = (CRReminder) this.graph.Reminder.SelectSingle() ?? (CRReminder) this.graph.Reminder.Cache.Insert();
      reminder.RefNoteID = activity.NoteID;
      ParameterExpression parameterExpression6;
      // ISSUE: method reference
      this.ImportItemProperty<CalendarItemType, DateTime>(Expression.Lambda<Func<CalendarItemType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderNextTime))), parameterExpression6), item, (Action<DateTime>) (v => reminder.ReminderDate = new DateTime?(v)));
      ParameterExpression parameterExpression7;
      // ISSUE: method reference
      this.ImportItemProperty<CalendarItemType, string>(Expression.Lambda<Func<CalendarItemType, string>>((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderMinutesBeforeStart))), parameterExpression7), item, (Action<string>) (v => reminder.RemindAt = PXExchangeConversionHelper.ParseReminderMinutesBeforeStart(v)));
      ((PXGraph) this.graph).Caches[typeof (CRReminder)].RaiseFieldUpdated<CRReminder.reminderDate>((object) reminder, (object) null);
      ((PXGraph) this.graph).Caches[typeof (CRReminder)].Update((object) reminder);
    }
    activity = (CRActivity) ((PXGraph) this.graph).Caches[typeof (CRActivity)].Update((object) copy);
    if (((ItemType) item).Sensitivity != 2 && (item.OptionalAttendees != null || item.RequiredAttendees != null))
    {
      PXCache cache1 = ((PXSelectBase) this.graph.Events).Cache;
      EPEventMaint_AttendeeExt extension = ((PXGraph) this.graph).GetExtension<EPEventMaint_AttendeeExt>();
      PXCache cache2 = ((PXSelectBase) extension.Attendees).Cache;
      int num3;
      bool flag1 = (num3 = (cache2.AllowDelete = cache2.AllowInsert = cache2.AllowUpdate = true) ? 1 : 0) != 0;
      cache1.AllowUpdate = num3 != 0;
      int num4;
      bool flag2 = (num4 = flag1 ? 1 : 0) != 0;
      cache1.AllowInsert = num4 != 0;
      cache1.AllowDelete = flag2;
      List<string> stringList = new List<string>();
      List<EPAttendee> list = GraphHelper.RowCast<EPAttendee>((IEnumerable) ((PXSelectBase<EPAttendee>) extension.AttendeesExceptOwner).Select(Array.Empty<object>())).ToList<EPAttendee>();
      foreach (AttendeeType attendeeType in ((IEnumerable<AttendeeType>) (item.OptionalAttendees ?? new AttendeeType[0])).Concat<AttendeeType>((IEnumerable<AttendeeType>) (item.RequiredAttendees ?? new AttendeeType[0])))
      {
        AttendeeType at = attendeeType;
        if (!stringList.Contains(at.Mailbox.EmailAddress))
        {
          stringList.Add(at.Mailbox.EmailAddress);
          EPAttendee epAttendee = list.FirstOrDefault<EPAttendee>((Func<EPAttendee, bool>) (a => string.Equals(a.Email, at.Mailbox.EmailAddress, StringComparison.InvariantCultureIgnoreCase)));
          if (!string.IsNullOrEmpty(at.Mailbox.EmailAddress))
          {
            int num5 = epAttendee == null ? 1 : 0;
            if (num5 != 0)
              epAttendee = new EPAttendee();
            int num6;
            int? nullable = this.Cache.UsersCache.TryGetValue(at.Mailbox.EmailAddress, out num6) ? new int?(num6) : new int?();
            epAttendee.EventNoteID = activity.NoteID;
            epAttendee.ContactID = nullable;
            epAttendee.Email = at.Mailbox.EmailAddress;
            epAttendee.Invitation = new int?(PXExchangeConversionHelper.ParceAttendeeStatus(at.ResponseType));
            if (num5 != 0)
              cache2.Update((object) epAttendee);
            else
              cache2.Insert((object) epAttendee);
          }
        }
      }
      foreach (EPAttendee epAttendee in list)
      {
        if (!stringList.Contains(epAttendee.Email))
        {
          int num7;
          if (this.Cache.UsersCache.TryGetValue(epAttendee.Email, out num7))
          {
            int num8 = num7;
            int? ownerId = activity.OwnerID;
            int valueOrDefault = ownerId.GetValueOrDefault();
            if (num8 == valueOrDefault & ownerId.HasValue)
              continue;
          }
          cache2.Delete((object) epAttendee);
        }
      }
    }
    this.PostpareActivity(account, item, ref activity);
    return (PXSyncTag) null;

    string GetItemTimeZone()
    {
      if (item.StartTimeZoneId == null)
        return account.ExchangeTimeZone;
      PXTimeZoneInfo bySystemRegionId = PXTimeZoneInfo.FindSystemTimeZoneBySystemRegionId(item.StartTimeZoneId);
      if (bySystemRegionId != null)
      {
        string id = bySystemRegionId.Id;
        if (id != null)
          return id;
      }
      this.Provider.LogWarning(account.Address, $"Cannot parse item's {((ItemType) item).ItemId.Id} timezone {item.StartTimeZoneId}");
      return account.ExchangeTimeZone;
    }
  }
}
