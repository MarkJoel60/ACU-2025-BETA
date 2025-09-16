// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEventMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Export.Imc;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EndpointAdapters;
using PX.Objects.EP.Graphs.EPEventMaint.Extensions;
using PX.Objects.EP.Imc;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.EP;

public class EPEventMaint : 
  CRBaseActivityMaint<PX.Objects.EP.EPEventMaint, CRActivity>,
  IRelatedActivitiesView,
  ICaptionable
{
  [PXViewName("Events")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRActivity.noteID), typeof (CRActivity.showAsID), typeof (CRActivity.uistatus), typeof (CRActivity.startDate), typeof (CRActivity.endDate), typeof (CRActivity.ownerID)})]
  public PXSelect<CRActivity, Where<CRActivity.classID, Equal<CRActivityClass.events>>> Events;
  [PXHidden]
  public PXSelect<CRActivity, Where<CRActivity.noteID, Equal<Current<CRActivity.noteID>>>> CurrentEvent;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> BaseContract;
  [PXHidden]
  public PXSelect<PMTimeActivity> TimeActivitiesOld;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMTimeActivity.timeSpent), typeof (PMTimeActivity.timeBillable), typeof (PMTimeActivity.overtimeSpent), typeof (PMTimeActivity.overtimeBillable)})]
  public PMTimeActivityList<CRActivity> TimeActivity;
  [PXHidden]
  public PXSelect<CRChildActivity> ChildAct;
  public PXSetup<EPSetup> Setup;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<CRActivity.ownerID>>>> CurrentOwner;
  public PXSelect<PX.Objects.CR.Contact> ContactSearch;
  public PXFilter<SendCardFilter> SendCardSettings;
  public PXSelectJoin<PX.Objects.CS.CSCalendar, InnerJoin<EPEmployee, On<EPEmployee.calendarID, Equal<PX.Objects.CS.CSCalendar.calendarID>>>, Where<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>> WorkCalendar;
  public CRReminderList<CRActivity> Reminder;
  public PXCopyPasteAction<CRActivity> CopyPaste;
  public PXDelete<CRActivity> Delete;
  public PXAction<CRActivity> Complete;
  public PXAction<CRActivity> CompleteAndFollowUp;
  public PXAction<CRActivity> CancelActivity;
  public PXAction<CRActivity> ExportCard;
  public PXAction<CRActivity> sendCard;
  public PXMenuAction<CRActivity> Action;

  PXSelectBase IRelatedActivitiesView.Activities
  {
    get => (PXSelectBase) this.GetExtension<EPEventMaint_ActivityDetailsExt>().Activities;
  }

  [InjectDependency]
  public IVCalendarFactory VCalendarFactory { get; private set; }

  public EPEventMaint()
  {
    PXUIFieldAttribute.SetVisible(this.Caches[typeof (Users)], typeof (Users.username).Name, true);
    PXUIFieldAttribute.SetDisplayName<CRActivity.startDate>(this.Caches[typeof (CRActivity)], "Start Time");
    ActivityStatusListAttribute.SetRestictedMode<CRActivity.uistatus>(this.Events.Cache, true);
  }

  public string Caption()
  {
    CRActivity current = this.Events.Current;
    return current == null || current.Subject == null ? "" : current.Subject ?? "";
  }

  [PXUIField(DisplayName = "Complete", MapEnableRights = PXCacheRights.Select)]
  [PXButton(Tooltip = "Complete Event (Ctrl + K)", DisplayOnMainToolbar = true, ClosePopup = true, Category = "Management", ShortcutCtrl = true, ShortcutChar = 'K', OnClosingPopup = PXSpecialButtonType.Refresh)]
  protected virtual void complete() => this.CompleteRow(this.Events.Current);

  [PXUIField(DisplayName = "Complete & Follow-Up", MapEnableRights = PXCacheRights.Select, Visible = false)]
  [PXButton(Tooltip = "Complete & Follow-Up (Ctrl + Shift + K)", ClosePopup = true, ShortcutCtrl = true, ShortcutShift = true, ShortcutChar = 'K', OnClosingPopup = PXSpecialButtonType.Refresh)]
  protected virtual void completeAndFollowUp()
  {
    CRActivity current = this.Events.Current;
    if (current == null)
      return;
    this.CompleteRow(current);
    PX.Objects.EP.EPEventMaint instance = PXGraph.CreateInstance<PX.Objects.EP.EPEventMaint>();
    CRActivity copy = (CRActivity) instance.Events.Cache.CreateCopy((object) current);
    copy.NoteID = new Guid?();
    copy.ParentNoteID = current.ParentNoteID;
    copy.UIStatus = (string) null;
    copy.NoteID = new Guid?();
    copy.PercentCompletion = new int?();
    if (copy.StartDate.HasValue)
      copy.StartDate = new DateTime?(copy.StartDate.Value.AddDays(1.0));
    DateTime? endDate = copy.EndDate;
    if (endDate.HasValue)
    {
      CRActivity crActivity = copy;
      endDate = copy.EndDate;
      DateTime? nullable = new DateTime?(endDate.Value.AddDays(1.0));
      crActivity.EndDate = nullable;
    }
    instance.Events.Cache.Insert((object) copy);
    if (!this.IsContractBasedAPI)
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.NewWindow);
    instance.Save.Press();
  }

  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select)]
  [PXButton(Tooltip = "Cancel", DisplayOnMainToolbar = true, Category = "Management", ClosePopup = true)]
  protected virtual IEnumerable cancelActivity(PXAdapter adapter)
  {
    this.CancelRow(this.Events.Current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Export", MapEnableRights = PXCacheRights.Select)]
  [PXButton(Tooltip = "Export information about the event in iCalendar format.", Category = "Other")]
  public virtual void exportCard()
  {
    CRActivity current = this.Events.Current;
    if (current != null)
      throw new EPIcsExportRedirectException((object) this.VCalendarFactory.CreateVEvent((object) current));
  }

  [PXButton(Tooltip = "Send i-Calendar format information about event by e-mail", Category = "Other")]
  [PXUIField(DisplayName = "Send Card", MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable SendCard(PXAdapter adapter)
  {
    if (this.Events.Current == null || !this.SendCardSettings.AskExtRequired())
      return adapter.Get();
    CRActivity current = this.Events.Current;
    string newLineString = Environment.NewLine + Environment.NewLine;
    string email = this.SendCardSettings.Current.Email;
    string str1 = $"{PXLocalizer.Localize("Event Number", typeof (Messages).FullName)}: {current.NoteID.Value.ToString()}{newLineString}{PXLocalizer.Localize("Subject", typeof (Messages).FullName)}: {current.Subject}{newLineString}{this.GetEventStringInfo(current, newLineString, string.Empty)}";
    string str2 = $"{PXLocalizer.Localize("Event", typeof (Messages).FullName)}: {current.Subject}";
    NotificationGenerator notificationGenerator = new NotificationGenerator()
    {
      To = email,
      Subject = str2,
      Body = str1,
      Owner = this.Accessinfo.ContactID
    };
    using (MemoryStream memoryStream = new MemoryStream())
    {
      this.CreateVEvent().Write((Stream) memoryStream);
      notificationGenerator.AddAttachment("event.ics", memoryStream.ToArray());
    }
    notificationGenerator.Send();
    return adapter.Get();
  }

  [PXRemoveBaseAttribute(typeof (PXNavigateSelectorAttribute))]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.subject> e)
  {
  }

  [EPStartDate(TimeZone = typeof (CRActivity.timeZone), OwnerID = typeof (CRActivity.ownerID), AllDayField = typeof (CRActivity.allDay), DisplayName = "Start Date", DisplayNameDate = "Start Date", DisplayNameTime = "Start Time", IgnoreRequireTimeOnActivity = true)]
  [PXFormula(typeof (Round30Minutes<TimeZoneNow>))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.startDate> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXCustomizeBaseAttribute(typeof (EPEndDateAttribute), "TimeZone", typeof (CRActivity.timeZone))]
  [PXCustomizeBaseAttribute(typeof (EPEndDateAttribute), "OwnerID", typeof (CRActivity.ownerID))]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.endDate> e)
  {
  }

  [PXDefault(typeof (CRActivityClass.events))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.classID> e)
  {
  }

  [PXFormula(typeof (False))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMTimeActivity.needToBeDeleted> e)
  {
  }

  [PXFormula(typeof (Default<CRActivity.allDay>))]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.timeZone> e)
  {
  }

  [PXDefault(typeof (Switch<Case<Where<Current<CRActivity.allDay>, Equal<True>>, RemindAtListAttribute.before1day>, RemindAtListAttribute.before15minutes>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXCustomizeBaseAttribute(typeof (RemindAtListAttribute), "IsAllDay", typeof (CRActivity.allDay))]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRReminder.remindAt> e)
  {
  }

  protected override void _(PX.Data.Events.RowSelected<CRActivity> e)
  {
    base._(e);
    CRActivity row = e.Row;
    if (row == null)
      return;
    PXCache cache1 = e.Cache;
    PMTimeActivity data = this.TimeActivity.SelectSingle(Array.Empty<object>());
    PXCache cache2 = this.TimeActivity.Cache;
    string str = (string) cache1.GetValueOriginal<CRActivity.uistatus>((object) row) ?? "OP";
    bool flag1 = this.IsCurrentUserCanEditEvent(row);
    bool flag2 = flag1 && str == "OP";
    PXUIFieldAttribute.SetEnabled(cache2, (object) data, flag2);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeSpent>(cache2, (object) data, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeSpent>(cache2, (object) data, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeBillable>(cache2, (object) data, false);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeBillable>(cache2, (object) data, false);
    cache1.AllowUpdate = flag1;
    cache1.AllowDelete = flag1;
    PXUIFieldAttribute.SetEnabled(cache1, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<CRActivity.noteID>(cache1, (object) row);
    PXUIFieldAttribute.SetEnabled<CRActivity.uistatus>(cache1, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CRActivity.createdByID>(cache1, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CRActivity.completedDate>(cache1, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CRActivity.workgroupID>(cache1, (object) row, false);
    this.AdjustProvidesCaseSolutionState(cache1, row);
    this.Save.SetEnabled(flag1);
    PXUIFieldAttribute.SetVisible<CRActivity.timeZone>(cache1, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<CRActivity.ownerID>(cache1, (object) row, this.IsCurrentUserCanEditOwnerField(row));
    this.Complete.SetVisible(flag1);
    this.CancelActivity.SetVisible(flag1);
    this.Complete.SetEnabled(flag2 && row.UIStatus != "CD" && row.UIStatus != "CL");
    this.CancelActivity.SetEnabled(flag2 && row.UIStatus != "CD" && row.UIStatus != "CL");
    this.MarkAs(cache1, row, this.Accessinfo.ContactID, 1);
    PXUIFieldAttribute.SetEnabled<CRActivity.refNoteID>(cache1, (object) row, cache1.GetValue<CRActivity.refNoteIDType>((object) row) != null || this.IsContractBasedAPI);
    PXUIFieldAttribute.SetEnabled<CRActivity.responseActivityNoteID>(cache1, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CRActivity.responseDueDateTime>(cache1, (object) row, false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRReminder> e)
  {
    CRReminder row = e.Row;
    if (row == null)
      return;
    bool isEnabled1 = ((string) this.Events.Cache.GetValueOriginal<CRActivity.uistatus>((object) this.Events.Current) ?? "OP") == "OP";
    PXUIFieldAttribute.SetEnabled<CRReminder.remindAt>(this.Reminder.Cache, (object) row, isEnabled1);
    bool isEnabled2 = this.IsCurrentUserCanEditEvent(this.Events.Current) & isEnabled1;
    PXUIFieldAttribute.SetEnabled<CRReminder.isReminderOn>(this.Reminder.Cache, (object) row, isEnabled2);
  }

  public virtual void _(PX.Data.Events.FieldSelecting<CRReminder.remindAt> e)
  {
    if (e.Row == null)
      return;
    CRReminder row = (CRReminder) e.Row;
    if (!string.IsNullOrEmpty(row.RemindAt) && !(row.RemindAt == "EXCH"))
      return;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    DateTime dateTime = (DateTime) e.Cache.GetValue<CRReminder.reminderDate>(e.Row);
    string timeZone = this.Events.Current?.TimeZone;
    if (!string.IsNullOrEmpty(timeZone))
      dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(PXTimeZoneInfo.ConvertTimeToUtc(dateTime, LocaleInfo.GetTimeZone()), PXTimeZoneInfo.FindSystemTimeZoneById(timeZone));
    stringList1.Add("EXCH");
    stringList2.Add(dateTime.ToString());
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), "remindAt", new bool?(false), new int?(), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(), "EXCH");
    e.ReturnValue = (object) "EXCH";
    ((PXFieldState) e.ReturnState).Enabled = false;
    e.Args.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRActivity, CRActivity.allDay> e)
  {
    this.Reminder.Cache.SetDefaultExt<CRReminder.remindAt>((object) this.Reminder.Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRActivity, CRActivity.startDate> e)
  {
    if (e.Row == null)
      return;
    this.Reminder.Cache.RaiseFieldUpdated<CRReminder.remindAt>((object) this.Reminder.Current, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CRActivity, CRActivity.timeZone> e)
  {
    if (e.Row == null)
      return;
    if (string.IsNullOrEmpty((string) e.NewValue))
      throw new PXSetPropertyException("Time Zone cannot be empty.", PXErrorLevel.Warning);
    e.Cache.RaiseFieldUpdated<CRActivity.startDate>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRActivity, CRActivity.timeZone> e)
  {
    if (e.Row == null)
      return;
    e.Cache.RaiseFieldUpdated<CRActivity.startDate>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CRReminder, CRReminder.remindAt> e)
  {
    if (e.Row == null)
      return;
    CRActivity current = this.Events.Current;
    if (current == null)
      return;
    DateTime? startDate = current.StartDate;
    ref DateTime? local = ref startDate;
    DateTime? nullable = local.HasValue ? new DateTime?(local.GetValueOrDefault().Add(RemindAtListAttribute.GetRemindAtTimeSpan((string) e.NewValue))) : e.Row.ReminderDate;
    if (current.TimeZone != null && current.TimeZone != this.GetDefaultTimeZone())
    {
      nullable = new DateTime?(PXTimeZoneInfo.ConvertTimeToUtc(nullable.Value, PXTimeZoneInfo.FindSystemTimeZoneById(current.TimeZone)));
      nullable = new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(nullable.Value, PXTimeZoneInfo.FindSystemTimeZoneById(this.GetDefaultTimeZone())));
    }
    e.Row.ReminderDate = nullable;
    e.Cache.SetStatus((object) e.Row, PXEntryStatus.Modified);
    if (!this.IsMobile)
      return;
    e.Cache.RaiseRowSelected((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<CRActivity.timeZone> e)
  {
    e.NewValue = (object) this.GetDefaultTimeZone();
    e.Cancel = true;
  }

  public override void CompleteRow(CRActivity row)
  {
    if (row == null)
      return;
    switch ((string) this.Events.Cache.GetValueOriginal<CRActivity.uistatus>((object) row) ?? "OP")
    {
      case "CD":
        break;
      case "CL":
        break;
      default:
        CRActivity copy = (CRActivity) this.Events.Cache.CreateCopy((object) row);
        copy.UIStatus = "CD";
        this.Events.Cache.Update((object) copy);
        this.Actions.PressSave();
        break;
    }
  }

  public override void CancelRow(CRActivity row)
  {
    if (row == null)
      return;
    switch ((string) this.Events.Cache.GetValueOriginal<CRActivity.uistatus>((object) row) ?? "OP")
    {
      case "CD":
        break;
      case "CL":
        break;
      default:
        CRActivity copy = (CRActivity) this.Events.Cache.CreateCopy((object) row);
        copy.UIStatus = "CL";
        this.Events.Cache.Update((object) copy);
        this.Actions.PressSave();
        break;
    }
  }

  public override void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    foreach (int index in EnumerableExtensions.SelectIndexesWhere<Command>((IEnumerable<Command>) script, (Func<Command, bool>) (f => Regex.IsMatch(f.FieldName, "(?:StartDate|EndDate)_(?:Date|Time)", RegexOptions.IgnoreCase))).Reverse<int>())
    {
      script.RemoveAt(index);
      containers.RemoveAt(index);
    }
  }

  public virtual bool IsCurrentUserAdministrator()
  {
    return PXSiteMapProvider.IsUserInRole(PXAccess.GetAdministratorRole());
  }

  public virtual bool IsCurrentUserCanEditOwnerField(CRActivity row)
  {
    int? valueOriginal = (int?) this.Events.Cache.GetValueOriginal<CRActivity.ownerID>((object) row);
    bool flag = valueOriginal.HasValue && !valueOriginal.Equals((object) row.OwnerID);
    return ((this.Events.Cache.GetStatus((object) row) == PXEntryStatus.Inserted ? 1 : (!valueOriginal.HasValue ? 1 : 0)) | (flag ? 1 : 0)) != 0;
  }

  public virtual bool IsCurrentUserCanEditEvent(CRActivity row)
  {
    return this.IsCurrentUserOwnerOfEvent(row) || this.Events.Cache.GetStatus((object) row) == PXEntryStatus.Inserted || this.Events.Cache.GetValueOriginal<CRActivity.ownerID>((object) row) == null || this.EnableForceEditEvent;
  }

  public virtual bool IsCurrentUserOwnerOfEvent(CRActivity row)
  {
    int? contactId = PXAccess.GetContactID();
    int? ownerId = row.OwnerID;
    return contactId.GetValueOrDefault() == ownerId.GetValueOrDefault() & contactId.HasValue == ownerId.HasValue;
  }

  public virtual bool IsEventEditable(CRActivity row)
  {
    return row == null || EnumerableExtensions.IsIn<string>(row.UIStatus, (string) null, "OP");
  }

  public virtual bool WasEventOriginallyEditable(CRActivity row)
  {
    return this.IsEventEditable(this.Events.Cache.GetOriginal((object) row) as CRActivity);
  }

  public virtual bool IsEventInThePast(CRActivity row)
  {
    DateTime? endDate;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      endDate = row.EndDate;
      num = !endDate.HasValue ? 1 : 0;
    }
    if (num != 0)
      return false;
    endDate = row.EndDate;
    return PXTimeZoneInfo.ConvertTimeToUtc(endDate.Value, string.IsNullOrEmpty(row?.TimeZone) ? LocaleInfo.GetTimeZone() : PXTimeZoneInfo.FindSystemTimeZoneById(row.TimeZone)) < PXTimeZoneInfo.UtcNow;
  }

  public virtual bool IsEventPersisted(CRActivity row)
  {
    return this.Events.Cache.GetOriginal((object) row) != null;
  }

  public bool IsCurrentEventEditable() => this.IsEventEditable(this.Events.Current);

  public bool IsCurrentEventInThePast() => this.IsEventInThePast(this.Events.Current);

  public bool IsCurrentEventPersisted() => this.IsEventPersisted(this.Events.Current);

  public bool WasCurrentEventOriginallyEditable()
  {
    return this.WasEventOriginallyEditable(this.Events.Current);
  }

  public bool EnableForceEditEvent { get; set; }

  protected virtual string GetDefaultTimeZone()
  {
    PXResultset<UserPreferences> pxResultset = PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.pKID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this);
    return pxResultset != null && pxResultset.Count > 0 && !string.IsNullOrEmpty(((UserPreferences) pxResultset[0][typeof (UserPreferences)]).TimeZone) ? ((UserPreferences) pxResultset[0][typeof (UserPreferences)]).TimeZone : LocaleInfo.GetTimeZone().Id;
  }

  private string GetEventStringInfo(CRActivity _event, string newLineString, string prefix)
  {
    DateTime dateTime1 = _event.StartDate.Value;
    DateTime dateTime2 = _event.EndDate.Value;
    string displayName = LocaleInfo.GetTimeZone().DisplayName;
    string eventStringInfo = $"{prefix}{PXLocalizer.Localize("Start Date", typeof (Messages).FullName)}: {dateTime1.ToLongDateString()} {dateTime1.ToShortTimeString()} {displayName}{newLineString}{prefix}{PXLocalizer.Localize("End Date", typeof (Messages).FullName)}: {dateTime2.ToLongDateString()} {dateTime2.ToShortTimeString()} {displayName}";
    CRActivity data = _event;
    if (data != null && this.Events.Cache.GetValueExt((object) data, PXLocalizer.Localize("Duration", typeof (Messages).FullName)) is PXStringState valueExt)
    {
      string str1 = $"{eventStringInfo}{newLineString}{prefix}{PXLocalizer.Localize("Duration", typeof (Messages).FullName)}: ";
      string str2 = valueExt.Value.ToString();
      eventStringInfo = str1 + (string.IsNullOrEmpty(valueExt.InputMask) ? str2 : Mask.Format(valueExt.InputMask, str2));
    }
    if (!string.IsNullOrEmpty(_event.Body))
    {
      string str = Tools.ConvertHtmlToSimpleText(_event.Body).Replace(Environment.NewLine, newLineString);
      eventStringInfo = eventStringInfo + newLineString + str;
    }
    return eventStringInfo;
  }

  private vEvent CreateVEvent()
  {
    vEvent vevent = this.VCalendarFactory.CreateVEvent((object) this.Events.Current);
    vevent.Method = "REQUEST";
    return vevent;
  }

  public class EmbeddedImagesExtractor : 
    EmbeddedImagesExtractorExtension<PX.Objects.EP.EPEventMaint, CRActivity, CRActivity.body>
  {
  }
}
