// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncPolicyMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Web;

#nullable disable
namespace PX.SM;

public class EMailSyncPolicyMaint : PXGraph<EMailSyncPolicyMaint, EMailSyncPolicy>
{
  public PXSelect<EMailSyncPolicy> SyncPolicy;
  public PXSelect<EMailSyncPolicy, Where<EMailSyncPolicy.policyName, Equal<Current<EMailSyncPolicy.policyName>>>> CurrentSyncPolicy;
  public PXSelect<EMailSyncAccountPreferences, Where<EMailSyncAccountPreferences.policyName, Equal<Current<EMailSyncPolicy.policyName>>>, OrderBy<Desc<EMailSyncAccountPreferences.employeeID>>> Preferences;
  public PXSelect<EMailSyncAccount> Accounts_;
  public PXSelect<EMailSyncServer> Servers_;

  public EMailSyncPolicyMaint() => this.Preferences.Cache.AllowSelect = false;

  protected virtual void EMailSyncPolicy_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row))
      return;
    PXCache cache1 = cache;
    EMailSyncPolicy data1 = row;
    bool? nullable = row.ContactsSync;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsDirection>(cache1, (object) data1, num1 != 0);
    PXCache cache2 = cache;
    EMailSyncPolicy data2 = row;
    nullable = row.ContactsSync;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsSeparated>(cache2, (object) data2, num2 != 0);
    PXCache cache3 = cache;
    EMailSyncPolicy data3 = row;
    nullable = row.ContactsSync;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsMerge>(cache3, (object) data3, num3 != 0);
    PXCache cache4 = cache;
    EMailSyncPolicy data4 = row;
    nullable = row.ContactsSync;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsFilter>(cache4, (object) data4, num4 != 0);
    PXCache cache5 = cache;
    EMailSyncPolicy data5 = row;
    nullable = row.ContactsSync;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsClass>(cache5, (object) data5, num5 != 0);
    PXCache cache6 = cache;
    EMailSyncPolicy data6 = row;
    nullable = row.ContactsSync;
    int num6;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.ContactsSeparated;
      if (!nullable.GetValueOrDefault())
      {
        num6 = row.ContactsDirection != "E" ? 1 : 0;
        goto label_5;
      }
    }
    num6 = 0;
label_5:
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsSkipCategory>(cache6, (object) data6, num6 != 0);
    PXCache cache7 = cache;
    EMailSyncPolicy data7 = row;
    nullable = row.ContactsSync;
    int num7 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsGenerateLink>(cache7, (object) data7, num7 != 0);
    PXCache cache8 = cache;
    EMailSyncPolicy data8 = row;
    nullable = row.ContactsSync;
    int num8;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.ContactsSeparated;
      num8 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num8 = 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.contactsFolder>(cache8, (object) data8, num8 != 0);
    PXCache cache9 = cache;
    EMailSyncPolicy data9 = row;
    nullable = row.EmailsSync;
    int num9 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.emailsDirection>(cache9, (object) data9, num9 != 0);
    PXCache cache10 = cache;
    EMailSyncPolicy data10 = row;
    nullable = row.EmailsSync;
    int num10 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.emailsFolder>(cache10, (object) data10, num10 != 0);
    PXCache cache11 = cache;
    EMailSyncPolicy data11 = row;
    nullable = row.EmailsSync;
    int num11 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.emailsAttachments>(cache11, (object) data11, num11 != 0);
    PXCache cache12 = cache;
    EMailSyncPolicy data12 = row;
    nullable = row.EmailsSync;
    int num12 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.emailsValidateContact>(cache12, (object) data12, num12 != 0);
    PXCache cache13 = cache;
    EMailSyncPolicy data13 = row;
    nullable = row.EmailsSync;
    int check1 = nullable.GetValueOrDefault() ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<EMailSyncPolicy.emailsFolder>(cache13, (object) data13, (PXPersistingCheck) check1);
    PXCache cache14 = cache;
    EMailSyncPolicy data14 = row;
    nullable = row.TasksSync;
    int num13 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.tasksDirection>(cache14, (object) data14, num13 != 0);
    PXCache cache15 = cache;
    EMailSyncPolicy data15 = row;
    nullable = row.TasksSync;
    int num14 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.tasksSeparated>(cache15, (object) data15, num14 != 0);
    PXCache cache16 = cache;
    EMailSyncPolicy data16 = row;
    nullable = row.TasksSync;
    int num15;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.TasksSeparated;
      num15 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num15 = 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.tasksSkipCategory>(cache16, (object) data16, num15 != 0);
    PXCache cache17 = cache;
    EMailSyncPolicy data17 = row;
    nullable = row.TasksSync;
    int num16;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.TasksSeparated;
      num16 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num16 = 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.tasksFolder>(cache17, (object) data17, num16 != 0);
    PXCache cache18 = cache;
    EMailSyncPolicy data18 = row;
    nullable = row.EventsSync;
    int num17 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.eventsDirection>(cache18, (object) data18, num17 != 0);
    PXCache cache19 = cache;
    EMailSyncPolicy data19 = row;
    nullable = row.EventsSync;
    int num18 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.eventsSeparated>(cache19, (object) data19, num18 != 0);
    PXCache cache20 = cache;
    EMailSyncPolicy data20 = row;
    nullable = row.EventsSync;
    int num19;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.EventsSeparated;
      num19 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num19 = 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.eventsSkipCategory>(cache20, (object) data20, num19 != 0);
    PXCache cache21 = cache;
    EMailSyncPolicy data21 = row;
    nullable = row.EventsSync;
    int num20;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.EventsSeparated;
      num20 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num20 = 0;
    PXUIFieldAttribute.SetEnabled<EMailSyncPolicy.eventsFolder>(cache21, (object) data21, num20 != 0);
    PXCache cache22 = cache;
    EMailSyncPolicy data22 = row;
    nullable = row.ContactsSync;
    int check2;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.ContactsGenerateLink;
      if (nullable.GetValueOrDefault())
      {
        check2 = 1;
        goto label_24;
      }
    }
    check2 = 2;
label_24:
    PXDefaultAttribute.SetPersistingCheck<EMailSyncPolicy.linkTemplate>(cache22, (object) data22, (PXPersistingCheck) check2);
  }

  protected virtual void EMailSyncPolicy_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row))
      return;
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.policyName, Equal<Required<EMailSyncAccount.policyName>>>>.Config>.Select((PXGraph) this, (object) row.PolicyName))
    {
      EMailSyncAccount emailSyncAccount = (EMailSyncAccount) pxResult;
      emailSyncAccount.ContactsExportFolder = (string) null;
      emailSyncAccount.ContactsImportFolder = (string) null;
      emailSyncAccount.EmailsExportFolder = (string) null;
      emailSyncAccount.EmailsImportFolder = (string) null;
      emailSyncAccount.EventsExportFolder = (string) null;
      emailSyncAccount.EventsImportFolder = (string) null;
      emailSyncAccount.TasksExportFolder = (string) null;
      emailSyncAccount.TasksImportFolder = (string) null;
      this.Accounts_.Cache.Update((object) emailSyncAccount);
    }
  }

  protected virtual void EMailSyncPolicy_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row))
      return;
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.policyName, Equal<Required<EMailSyncAccount.policyName>>>>.Config>.Select((PXGraph) this, (object) row.PolicyName))
    {
      EMailSyncAccount emailSyncAccount = (EMailSyncAccount) pxResult;
      emailSyncAccount.PolicyName = (string) null;
      this.Accounts_.Cache.Update((object) emailSyncAccount);
    }
    foreach (PXResult<EMailSyncServer> pxResult in PXSelectBase<EMailSyncServer, PXSelect<EMailSyncServer, Where<EMailSyncServer.defaultPolicyName, Equal<Required<EMailSyncAccount.policyName>>>>.Config>.Select((PXGraph) this, (object) row.PolicyName))
    {
      EMailSyncServer emailSyncServer = (EMailSyncServer) pxResult;
      emailSyncServer.DefaultPolicyName = (string) null;
      this.Servers_.Cache.Update((object) emailSyncServer);
    }
  }

  protected virtual void EMailSyncPolicy_ContactsSeparated_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row) || !row.ContactsSeparated.GetValueOrDefault())
      return;
    row.ContactsSkipCategory = new bool?(true);
  }

  protected virtual void EMailSyncPolicy_ContactsDirection_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row) || !(row.ContactsDirection == "E"))
      return;
    row.ContactsSkipCategory = new bool?(true);
  }

  protected virtual void EMailSyncPolicy_TasksSeparated_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row) || !row.TasksSeparated.GetValueOrDefault())
      return;
    row.TasksSkipCategory = new bool?(true);
  }

  protected virtual void EMailSyncPolicy_EventsSeparated_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EMailSyncPolicy row) || !row.EventsSeparated.GetValueOrDefault())
      return;
    row.EventsSkipCategory = new bool?(true);
  }

  protected virtual void EMailSyncPolicy_LinkTemplate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    object row = e.Row;
    if (HttpContext.Current == null)
      return;
    e.NewValue = (object) HttpContext.Current.Request.GetWebsiteUrl();
  }

  protected virtual void EMailSyncAccountPreferences_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EMailSyncAccountPreferences row) || !string.IsNullOrEmpty(row.PolicyName) || this.SyncPolicy.Current == null)
      return;
    row.PolicyName = this.SyncPolicy.Current.PolicyName;
  }

  protected virtual void EMailSyncAccountPreferences_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    if (!(e.Row is EMailSyncAccountPreferences row))
      return;
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.employeeID, Equal<Required<EMailSyncAccount.employeeID>>>>.Config>.Select((PXGraph) this, (object) row.EmployeeID))
    {
      EMailSyncAccount emailSyncAccount = (EMailSyncAccount) pxResult;
      emailSyncAccount.PolicyName = row.PolicyName;
      this.Accounts_.Cache.Update((object) emailSyncAccount);
    }
  }

  protected virtual void EMailSyncAccountPreferences_RowDeleted(
    PXCache sender,
    PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EMailSyncAccountPreferences row))
      return;
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.employeeID, Equal<Required<EMailSyncAccount.employeeID>>>>.Config>.Select((PXGraph) this, (object) row.EmployeeID))
    {
      EMailSyncAccount emailSyncAccount = (EMailSyncAccount) pxResult;
      emailSyncAccount.PolicyName = (string) null;
      this.Accounts_.Cache.Update((object) emailSyncAccount);
    }
  }
}
