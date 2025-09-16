// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.ExchangeTasksSyncCommand
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Update;
using PX.Data.Update.ExchangeService;
using PX.Data.Update.WebServices;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CS.Email;

public class ExchangeTasksSyncCommand : 
  ExchangeActivitySyncCommand<CRTaskMaint, TaskType, CRActivity, CRActivity.synchronize, CRActivity.noteID, CRActivity.ownerID>
{
  public ExchangeTasksSyncCommand(MicrosoftExchangeSyncProvider provider)
    : base(provider, "Tasks", (PXExchangeFindOptions) 16 /*0x10*/)
  {
    this.ExportReowned = true;
    if (!this.Policy.TasksSkipCategory.GetValueOrDefault())
      return;
    this.DefFindOptions = (PXExchangeFindOptions) (this.DefFindOptions | 4);
  }

  protected override void ConfigureEnvironment(
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    this.EnsureEnvironmentConfigured<TasksFolderType>(mailboxes, new PXSyncFolderSpecification(this.Policy.TasksSeparated.GetValueOrDefault() ? this.Policy.TasksFolder : (string) null, (DistinguishedFolderIdNameType) 9));
  }

  protected override BqlCommand GetSelectCommand()
  {
    return PXSelectBase<CRActivity, PXSelectReadonly2<CRActivity, InnerJoin<Contact, On<CRActivity.ownerID, Equal<Contact.contactID>>, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>>>>>, Where<CRActivity.classID, Equal<CRActivityClass.task>, And<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>>.Config>.GetCommand();
  }

  protected override PXSyncTag ExportInsertedAction(
    PXSyncMailbox account,
    TaskType item,
    CRActivity activity)
  {
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_Status))), typeof (object)), parameterExpression1), item, (object) PXExchangeConversionHelper.ParceActivityStatus(activity.UIStatus));
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), typeof (object)), parameterExpression2), item, (object) activity.Priority);
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_PercentComplete))), typeof (object)), parameterExpression3), item, (object) activity.PercentCompletion);
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_StartDate))), typeof (object)), parameterExpression4), item, (object) activity.StartDate, account.ExchangeTimeZone);
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_DueDate))), typeof (object)), parameterExpression5), item, (object) activity.EndDate, account.ExchangeTimeZone);
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_CompleteDate))), typeof (object)), parameterExpression6), item, (object) activity.CompletedDate, account.ExchangeTimeZone);
    CRReminder current = this.graph.Reminder.Current;
    ParameterExpression parameterExpression7;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderIsSet))), typeof (object)), parameterExpression7), item, (object) (current != null));
    if (current != null && current.ReminderDate.HasValue)
    {
      ParameterExpression parameterExpression8;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderNextTime))), typeof (object)), parameterExpression8), item, (object) current.ReminderDate);
      ParameterExpression parameterExpression9;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression9, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderDueBy))), typeof (object)), parameterExpression9), item, (object) current.ReminderDate);
    }
    return (PXSyncTag) null;
  }

  protected override PXSyncTag ExportUpdatedAction(
    PXSyncMailbox account,
    TaskType item,
    CRActivity activity,
    List<ItemChangeDescriptionType> updates)
  {
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_Status))), typeof (object)), parameterExpression1), (UnindexedFieldURIType) 170, (object) PXExchangeConversionHelper.ParceActivityStatus(activity.UIStatus)));
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), typeof (object)), parameterExpression2), (UnindexedFieldURIType) 25, (object) PXExchangeConversionHelper.ParceActivityPriority(activity.Priority)));
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_StartDate))), typeof (object)), parameterExpression3), (UnindexedFieldURIType) 169, (object) activity.StartDate, account.ExchangeTimeZone));
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_DueDate))), typeof (object)), parameterExpression4), (UnindexedFieldURIType) 160 /*0xA0*/, (object) activity.EndDate, account.ExchangeTimeZone));
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_CompleteDate))), typeof (object)), parameterExpression5), (UnindexedFieldURIType) 156, (object) activity.CompletedDate, account.ExchangeTimeZone));
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_PercentComplete))), typeof (object)), parameterExpression6), (UnindexedFieldURIType) 167, (object) (PXExchangeConversionHelper.ParceActivityStatus(activity.UIStatus) == 2 ? new int?(100) : activity.PercentCompletion)));
    CRReminder current = this.graph.Reminder.Current;
    ParameterExpression parameterExpression7;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderIsSet))), typeof (object)), parameterExpression7), (UnindexedFieldURIType) 40, (object) (current != null)));
    if (current != null && current.ReminderDate.HasValue)
    {
      ParameterExpression parameterExpression8;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderNextTime))), typeof (object)), parameterExpression8), (UnindexedFieldURIType) 41, (object) current.ReminderDate));
      ParameterExpression parameterExpression9;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<CalendarItemType>(Expression.Lambda<Func<CalendarItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression9, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderDueBy))), typeof (object)), parameterExpression9), (UnindexedFieldURIType) 39, (object) current.ReminderDate));
    }
    return (PXSyncTag) null;
  }

  protected override PXSyncTag ImportAction(
    PXSyncMailbox account,
    TaskType item,
    ref CRActivity activity)
  {
    this.PrepareActivity(account, item, true, ref activity);
    if (activity == null)
      return (PXSyncTag) null;
    activity.ClassID = new int?(0);
    CRActivity copy = activity;
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.ImportItemProperty<TaskType, TaskStatusType>(Expression.Lambda<Func<TaskType, TaskStatusType>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_Status))), parameterExpression1), item, (Action<TaskStatusType>) (v => copy.UIStatus = PXExchangeConversionHelper.ParceActivityStatus(v)));
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    this.ImportItemProperty<TaskType, ImportanceChoicesType>(Expression.Lambda<Func<TaskType, ImportanceChoicesType>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), parameterExpression2), item, (Action<ImportanceChoicesType>) (v => copy.Priority = new int?(PXExchangeConversionHelper.ParceActivityPriority(v))));
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    this.ImportItemProperty<TaskType, DateTime>(Expression.Lambda<Func<TaskType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_StartDate))), parameterExpression3), item, (Action<DateTime>) (v => copy.StartDate = new DateTime?(v)), exchTimezone: account.ExchangeTimeZone);
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    this.ImportItemProperty<TaskType, DateTime>(Expression.Lambda<Func<TaskType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_DueDate))), parameterExpression4), item, (Action<DateTime>) (v => copy.EndDate = new DateTime?(v)), exchTimezone: account.ExchangeTimeZone);
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    this.ImportItemProperty<TaskType, DateTime>(Expression.Lambda<Func<TaskType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_CompleteDate))), parameterExpression5), item, (Action<DateTime>) (v => copy.CompletedDate = new DateTime?(v)), exchTimezone: account.ExchangeTimeZone);
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    this.ImportItemProperty<TaskType, double>(Expression.Lambda<Func<TaskType, double>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaskType.get_PercentComplete))), parameterExpression6), item, (Action<double>) (v => copy.PercentCompletion = new int?((int) v)));
    if (((ItemType) item).ReminderIsSet && ((ItemType) item).ReminderNextTimeSpecified)
    {
      CRReminder reminder = (CRReminder) this.graph.Reminder.SelectSingle() ?? (CRReminder) this.graph.Reminder.Cache.Insert();
      reminder.RefNoteID = activity.NoteID;
      ParameterExpression parameterExpression7;
      // ISSUE: method reference
      this.ImportItemProperty<TaskType, DateTime>(Expression.Lambda<Func<TaskType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ReminderNextTime))), parameterExpression7), item, (Action<DateTime>) (v => reminder.ReminderDate = new DateTime?(v)));
      ((PXGraph) this.graph).Caches[typeof (CRReminder)].RaiseFieldUpdated<CRReminder.reminderDate>((object) reminder, (object) null);
      ((PXGraph) this.graph).Caches[typeof (CRReminder)].Update((object) reminder);
    }
    this.PostpareActivity(account, item, ref activity);
    return (PXSyncTag) null;
  }
}
