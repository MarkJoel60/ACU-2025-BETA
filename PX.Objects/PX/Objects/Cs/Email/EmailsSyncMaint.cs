// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.EmailsSyncMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Update.WebServices;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.Email;

public class EmailsSyncMaint : PXGraph<EmailsSyncMaint>
{
  protected static readonly Guid SUID = Guid.NewGuid();
  public PXCancel<EMailAccountSyncFilter> Cancel;
  public PXAction<EMailAccountSyncFilter> ResetContacts;
  public PXAction<EMailAccountSyncFilter> ResetTasks;
  public PXAction<EMailAccountSyncFilter> ResetEvents;
  public PXAction<EMailAccountSyncFilter> ResetEmails;
  public PXAction<EMailAccountSyncFilter> ViewEmployee;
  public PXAction<EMailAccountSyncFilter> ClearLog;
  public PXAction<EMailAccountSyncFilter> ResetWarning;
  public PXAction<EMailAccountSyncFilter> Status;
  public PXFilter<EMailAccountSyncFilter> Filter;
  public PXFilteredProcessingJoin<EMailSyncAccount, EMailAccountSyncFilter, InnerJoin<EMailSyncServer, On<EMailSyncServer.accountID, Equal<EMailSyncAccount.serverID>>, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EMailSyncAccount.employeeID>>, InnerJoin<Contact, On<EPEmployee.defContactID, Equal<Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>>>>>>, Where<EMailSyncServer.isActive, Equal<True>>, OrderBy<Asc<EMailSyncAccount.serverID, Asc<EMailSyncAccount.employeeID>>>> SelectedItems;
  public PXSelect<EMailSyncAccount, Where<EMailSyncAccount.serverID, Equal<Current<EMailSyncAccount.serverID>>, And<EMailSyncAccount.employeeID, Equal<Current<EMailSyncAccount.employeeID>>>>> CurrentItem;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<EMailSyncLog, LeftJoin<Contact, On<EMailSyncLog.address, Equal<Contact.eMail>>, LeftJoin<EPEmployee, On<EPEmployee.defContactID, Equal<Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>, And<EPEmployee.bAccountID, Equal<Current<EMailSyncAccount.employeeID>>, Or<EMailSyncLog.address, IsNull>>>>>>, Where<EMailSyncLog.serverID, Equal<Current<EMailSyncAccount.serverID>>>, OrderBy<Desc<EMailSyncLog.eventID>>> OperationLog;
  public PXSelectJoin<EMailSyncReference, InnerJoin<Contact, On<Contact.noteID, Equal<EMailSyncReference.noteID>, And<Contact.createdDateTime, GreaterEqual<Required<Contact.createdDateTime>>>>>, Where<EMailSyncReference.address, Equal<Current<EMailSyncAccount.address>>, And<EMailSyncReference.serverID, Equal<Current<EMailSyncAccount.serverID>>>>> ContactsReferences;
  public PXSelectJoin<EMailSyncReference, InnerJoin<CRActivity, On<CRActivity.noteID, Equal<EMailSyncReference.noteID>, And<CRActivity.classID, Equal<CRActivityClass.task>, And<CRActivity.createdDateTime, GreaterEqual<Required<CRActivity.createdDateTime>>>>>>, Where<EMailSyncReference.address, Equal<Current<EMailSyncAccount.address>>, And<EMailSyncReference.serverID, Equal<Current<EMailSyncAccount.serverID>>>>> TasksReferences;
  public PXSelectJoin<EMailSyncReference, InnerJoin<CRActivity, On<CRActivity.noteID, Equal<EMailSyncReference.noteID>, And<CRActivity.classID, Equal<CRActivityClass.events>, And<CRActivity.createdDateTime, GreaterEqual<Required<CRActivity.createdDateTime>>>>>>, Where<EMailSyncReference.address, Equal<Current<EMailSyncAccount.address>>, And<EMailSyncReference.serverID, Equal<Current<EMailSyncAccount.serverID>>>>> EventsReferences;
  public PXSelectJoin<EMailSyncReference, InnerJoin<CRActivity, On<CRActivity.noteID, Equal<EMailSyncReference.noteID>, And<CRActivity.classID, Equal<CRActivityClass.email>, And<CRActivity.createdDateTime, GreaterEqual<Required<CRActivity.createdDateTime>>>>>>, Where<EMailSyncReference.address, Equal<Current<EMailSyncAccount.address>>, And<EMailSyncReference.serverID, Equal<Current<EMailSyncAccount.serverID>>>>> EmailsReferences;

  [InjectDependency]
  private ILegacyCompanyService _legacyCompanyService { get; set; }

  [PXButton]
  [PXUIField]
  protected IEnumerable resetContacts(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EmailsSyncMaint.\u003C\u003Ec__DisplayClass7_0 cDisplayClass70 = new EmailsSyncMaint.\u003C\u003Ec__DisplayClass7_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.\u003C\u003E4__this = this;
    EMailAccountSyncFilter current = ((PXSelectBase<EMailAccountSyncFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass70.account = ((PXSelectBase<EMailSyncAccount>) this.CurrentItem).Current;
    // ISSUE: reference to a compiler-generated field
    if (current == null || cDisplayClass70.account == null)
      return adapter.Get();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass70, __methodptr(\u003CresetContacts\u003Eb__0)));
    this.ClearViews();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  protected IEnumerable resetTasks(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EmailsSyncMaint.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new EmailsSyncMaint.\u003C\u003Ec__DisplayClass9_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.\u003C\u003E4__this = this;
    EMailAccountSyncFilter current = ((PXSelectBase<EMailAccountSyncFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.account = ((PXSelectBase<EMailSyncAccount>) this.CurrentItem).Current;
    // ISSUE: reference to a compiler-generated field
    if (current == null || cDisplayClass90.account == null)
      return adapter.Get();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass90, __methodptr(\u003CresetTasks\u003Eb__0)));
    this.ClearViews();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  protected IEnumerable resetEvents(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EmailsSyncMaint.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new EmailsSyncMaint.\u003C\u003Ec__DisplayClass11_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.\u003C\u003E4__this = this;
    EMailAccountSyncFilter current = ((PXSelectBase<EMailAccountSyncFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.account = ((PXSelectBase<EMailSyncAccount>) this.CurrentItem).Current;
    // ISSUE: reference to a compiler-generated field
    if (current == null || cDisplayClass110.account == null)
      return adapter.Get();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass110, __methodptr(\u003CresetEvents\u003Eb__0)));
    this.ClearViews();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  protected IEnumerable resetEmails(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EmailsSyncMaint.\u003C\u003Ec__DisplayClass13_0 cDisplayClass130 = new EmailsSyncMaint.\u003C\u003Ec__DisplayClass13_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.\u003C\u003E4__this = this;
    EMailAccountSyncFilter current = ((PXSelectBase<EMailAccountSyncFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass130.account = ((PXSelectBase<EMailSyncAccount>) this.CurrentItem).Current;
    // ISSUE: reference to a compiler-generated field
    if (current == null || cDisplayClass130.account == null)
      return adapter.Get();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass130, __methodptr(\u003CresetEmails\u003Eb__0)));
    this.ClearViews();
    return adapter.Get();
  }

  private void ToDefault(EMailSyncAccount row)
  {
    System.Type[] typeArray = new System.Type[8]
    {
      typeof (EMailSyncAccount.contactsExportDate),
      typeof (EMailSyncAccount.contactsImportDate),
      typeof (EMailSyncAccount.tasksExportDate),
      typeof (EMailSyncAccount.tasksImportDate),
      typeof (EMailSyncAccount.eventsExportDate),
      typeof (EMailSyncAccount.eventsImportDate),
      typeof (EMailSyncAccount.emailsExportDate),
      typeof (EMailSyncAccount.emailsImportDate)
    };
    foreach (System.Type type in typeArray)
      ((PXGraph) this).Caches[typeof (EMailSyncAccount)].SetValue((object) row, type.Name, ((PXGraph) this).Caches[typeof (EMailSyncAccount)].GetValueOriginal((object) row, type.Name));
  }

  [PXButton]
  [PXUIField]
  protected IEnumerable viewEmployee(PXAdapter adapter)
  {
    if (((PXSelectBase<EMailSyncAccount>) this.SelectedItems).Current == null)
      return adapter.Get();
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) ((PXSelectBase<EMailSyncAccount>) this.SelectedItems).Current.EmployeeID
    }));
    if (epEmployee != null)
      PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (EPEmployee)], (object) epEmployee, string.Empty, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  protected void ClearViews()
  {
    ((PXSelectBase) this.SelectedItems).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.SelectedItems).View.Clear();
    ((PXSelectBase) this.SelectedItems).Cache.Clear();
    ((PXSelectBase) this.CurrentItem).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.CurrentItem).View.Clear();
    ((PXSelectBase) this.CurrentItem).Cache.Clear();
    ((PXSelectBase) this.CurrentItem).View.RequestRefresh();
    ((PXSelectBase) this.SelectedItems).View.RequestRefresh();
  }

  [PXButton]
  [PXUIField]
  protected IEnumerable clearLog(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EmailsSyncMaint.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new EmailsSyncMaint.\u003C\u003Ec__DisplayClass19_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.\u003C\u003E4__this = this;
    EMailAccountSyncFilter current = ((PXSelectBase<EMailAccountSyncFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.account = ((PXSelectBase<EMailSyncAccount>) this.SelectedItems).Current;
    // ISSUE: reference to a compiler-generated field
    if (current == null || cDisplayClass190.account == null)
      return adapter.Get();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass190, __methodptr(\u003CclearLog\u003Eb__0)));
    ((PXSelectBase) this.OperationLog).View.RequestRefresh();
    ((PXSelectBase) this.OperationLog).View.Clear();
    return adapter.Get();
  }

  protected virtual void ClearLogInChunks(EMailSyncAccount account)
  {
    EmailsSyncMaint instance = PXGraph.CreateInstance<EmailsSyncMaint>();
    EMailSyncLog emailSyncLog1 = ((PXSelectBase<EMailSyncLog>) new PXViewOf<EMailSyncLog>.BasedOn<SelectFromBase<EMailSyncLog, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EMailSyncLog.address, Equal<P.AsString>>>>>.And<BqlOperand<EMailSyncLog.serverID, IBqlInt>.IsEqual<P.AsInt>>>.Aggregate<To<Min<EMailSyncLog.eventID>>>>.ReadOnly((PXGraph) instance)).SelectSingle(new object[2]
    {
      (object) account.Address,
      (object) account.ServerID
    });
    EMailSyncLog emailSyncLog2 = ((PXSelectBase<EMailSyncLog>) new PXViewOf<EMailSyncLog>.BasedOn<SelectFromBase<EMailSyncLog, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EMailSyncLog.address, Equal<P.AsString>>>>>.And<BqlOperand<EMailSyncLog.serverID, IBqlInt>.IsEqual<P.AsInt>>>.Aggregate<To<Max<EMailSyncLog.eventID>>>>.ReadOnly((PXGraph) instance)).SelectSingle(new object[2]
    {
      (object) account.Address,
      (object) account.ServerID
    });
    int? nullable;
    int num1;
    if (emailSyncLog1 == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = emailSyncLog1.EventID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    int num2;
    if (emailSyncLog2 == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = emailSyncLog2.EventID;
      num2 = !nullable.HasValue ? 1 : 0;
    }
    if (num2 != 0)
      return;
    nullable = emailSyncLog1.EventID;
    int num3 = nullable.Value;
    nullable = emailSyncLog2.EventID;
    int val2 = nullable.Value;
    for (int index = num3; index <= val2; index += 5000)
    {
      int num4 = Math.Min(index + 5000, val2);
      PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[4]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<EMailSyncLog.address>((object) account.Address),
        (PXDataFieldRestrict) new PXDataFieldRestrict<EMailSyncLog.serverID>((object) account.ServerID),
        null,
        null
      };
      nullable = new int?();
      dataFieldRestrictArray[2] = (PXDataFieldRestrict) new PXDataFieldRestrict<EMailSyncLog.eventID>((PXDbType) 8, nullable, (object) num3, (PXComp) 3);
      nullable = new int?();
      dataFieldRestrictArray[3] = (PXDataFieldRestrict) new PXDataFieldRestrict<EMailSyncLog.eventID>((PXDbType) 8, nullable, (object) num4, (PXComp) 5);
      PXDatabase.Delete<EMailSyncLog>(dataFieldRestrictArray);
    }
  }

  [PXButton]
  [PXUIField]
  protected IEnumerable resetWarning(PXAdapter adapter)
  {
    EMailAccountSyncFilter current1 = ((PXSelectBase<EMailAccountSyncFilter>) this.Filter).Current;
    EMailSyncAccount current2 = ((PXSelectBase<EMailSyncAccount>) this.SelectedItems).Current;
    if (current1 == null || current2 == null)
      return adapter.Get();
    PXDatabase.Update<EMailSyncAccount>(new PXDataFieldParam[3]
    {
      (PXDataFieldParam) new PXDataFieldAssign<EMailSyncAccount.hasErrors>((object) false),
      (PXDataFieldParam) new PXDataFieldRestrict<EMailSyncAccount.serverID>((object) current2.ServerID),
      (PXDataFieldParam) new PXDataFieldRestrict<EMailSyncAccount.employeeID>((object) current2.EmployeeID)
    });
    ((PXSelectBase) this.SelectedItems).View.RequestRefresh();
    ((PXSelectBase) this.SelectedItems).View.Clear();
    return adapter.Get();
  }

  [PXButton(VisibleOnProcessingResults = true)]
  [PXUIField]
  protected IEnumerable status(PXAdapter adapter)
  {
    if (((PXSelectBase<EMailSyncAccount>) this.SelectedItems).Current == null || ((PXSelectBase<EMailSyncAccount>) this.CurrentItem).AskExt() == 1)
      return adapter.Get();
    ((PXSelectBase) this.CurrentItem).Cache.Clear();
    return adapter.Get();
  }

  protected virtual IEnumerable selectedItems()
  {
    EmailsSyncMaint emailsSyncMaint = this;
    EMailAccountSyncFilter current = ((PXSelectBase<EMailAccountSyncFilter>) emailsSyncMaint.Filter).Current;
    BqlCommand bqlCommand = ((PXSelectBase) emailsSyncMaint.SelectedItems).View.BqlSelect;
    if (current != null && current.ServerID.HasValue)
      bqlCommand = bqlCommand.WhereAnd<Where<EMailSyncAccount.serverID, Equal<Current<EMailAccountSyncFilter.serverID>>>>();
    if (current != null && !string.IsNullOrEmpty(current.PolicyName))
      bqlCommand = bqlCommand.WhereAnd<Where<EMailSyncAccount.policyName, Equal<Current<EMailAccountSyncFilter.policyName>>>>();
    int num = 0;
    int startRow = PXView.StartRow;
    PXView pxView = new PXView((PXGraph) emailsSyncMaint, false, bqlCommand);
    pxView.Clear();
    foreach (PXResult<EMailSyncAccount, EMailSyncServer, EPEmployee, Contact> pxResult in pxView.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
    {
      Contact contact = PXResult<EMailSyncAccount, EMailSyncServer, EPEmployee, Contact>.op_Implicit(pxResult);
      EMailSyncAccount emailSyncAccount = PXResult<EMailSyncAccount, EMailSyncServer, EPEmployee, Contact>.op_Implicit(pxResult);
      emailSyncAccount.Address = contact?.EMail ?? emailSyncAccount.Address;
      yield return (object) pxResult;
    }
  }

  public EmailsSyncMaint()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<EMailSyncAccount>) this.SelectedItems).SetProcessDelegate(new PXProcessingBase<EMailSyncAccount>.ProcessListDelegate((object) new EmailsSyncMaint.\u003C\u003Ec__DisplayClass34_0()
    {
      uid = ((PXGraph) this).UID,
      screenid = PXSiteMap.CurrentScreenID,
      currentFilter = ((PXSelectBase<EMailAccountSyncFilter>) this.Filter).Current
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
    ((PXProcessing<EMailSyncAccount>) this.SelectedItems).SetProcessCaption("Process");
    ((PXProcessing<EMailSyncAccount>) this.SelectedItems).SetProcessAllCaption("Process All");
    ((PXProcessingBase<EMailSyncAccount>) this.SelectedItems).SetSelected<EMailSyncAccount.selected>();
    ((PXSelectBase) this.OperationLog).AllowInsert = false;
    ((PXSelectBase) this.OperationLog).AllowUpdate = false;
    ((PXSelectBase) this.OperationLog).AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.CurrentItem).Cache, (string) null, true);
    ((PXAction) this.ResetContacts).SetEnabled(false);
    ((PXAction) this.ResetTasks).SetEnabled(false);
    ((PXAction) this.ResetEvents).SetEnabled(false);
    ((PXAction) this.ResetEmails).SetEnabled(false);
    // ISSUE: method pointer
    ((PXGraph) this).FieldVerifying.AddHandler(typeof (EMailSyncAccount), typeof (EMailSyncAccount.contactsExportDate).Name + "_Date", new PXFieldVerifying((object) this, __methodptr(EMailSyncAccount_ContactsExportDate_FieldVerifying)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldVerifying.AddHandler(typeof (EMailSyncAccount), typeof (EMailSyncAccount.tasksExportDate).Name + "_Date", new PXFieldVerifying((object) this, __methodptr(EMailSyncAccount_TasksExportDate_FieldVerifying)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldVerifying.AddHandler(typeof (EMailSyncAccount), typeof (EMailSyncAccount.eventsExportDate).Name + "_Date", new PXFieldVerifying((object) this, __methodptr(EMailSyncAccount_EventsExportDate_FieldVerifying)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldVerifying.AddHandler(typeof (EMailSyncAccount), typeof (EMailSyncAccount.emailsExportDate).Name + "_Date", new PXFieldVerifying((object) this, __methodptr(EMailSyncAccount_EmailsExportDate_FieldVerifying)));
  }

  public static void Process(
    EMailAccountSyncFilter filter,
    List<EMailSyncAccount> accounts,
    object uid,
    string screenid)
  {
    EmailsSyncMaint instance = PXGraph.CreateInstance<EmailsSyncMaint>();
    foreach (RowTaskInfo task in PXLongOperation.GetTaskList())
    {
      if (!object.Equals(uid, task.NativeKey) && PXLongOperation.GetStatus(task.NativeKey) == 1)
      {
        string company = LegacyCompanyServiceExtensions.ExtractCompany(instance._legacyCompanyService, task.User);
        if ((task.Screen ?? string.Empty).Replace(".", "") == screenid && company == PXAccess.GetCompanyName())
          throw new PXException("The previous operation has not been completed yet.");
      }
    }
    using (new PXUTCTimeZoneScope())
      instance.ProcessInternal(new EmailsSyncMaint.ProcessingContext(filter, accounts, instance.GetPolicies()));
  }

  protected void ProcessInternal(EmailsSyncMaint.ProcessingContext context)
  {
    Dictionary<int, List<int>> dictionary1 = new Dictionary<int, List<int>>();
    foreach (EMailSyncAccount account in context.Accounts)
    {
      List<int> intList;
      if (!dictionary1.TryGetValue(account.ServerID.Value, out intList))
        dictionary1[account.ServerID.Value] = intList = new List<int>();
      intList.Add(account.EmployeeID.Value);
    }
    foreach (int key1 in dictionary1.Keys)
    {
      Dictionary<string, EmailsSyncMaint.ProcessingBox> dictionary2 = new Dictionary<string, EmailsSyncMaint.ProcessingBox>();
      EMailSyncServer server = (EMailSyncServer) null;
      foreach (PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee> pxResult in PXSelectBase<EMailSyncAccount, PXSelectJoin<EMailSyncAccount, InnerJoin<EMailSyncServer, On<EMailSyncServer.accountID, Equal<EMailSyncAccount.serverID>>, InnerJoin<EMailAccount, On<EMailAccount.emailAccountID, Equal<EMailSyncAccount.emailAccountID>>, LeftJoin<EPEmployee, On<EMailSyncAccount.employeeID, Equal<EPEmployee.bAccountID>>>>>, Where<EMailSyncServer.accountID, Equal<Required<EMailSyncServer.accountID>>, And<EMailSyncAccount.address, IsNotNull>>, OrderBy<Asc<EMailSyncAccount.serverID, Asc<EMailSyncAccount.employeeID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) key1
      }))
      {
        server = PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee>.op_Implicit(pxResult);
        EMailSyncAccount emailSyncAccount = PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee>.op_Implicit(pxResult);
        EMailAccount emailAccount = PXResult<EMailSyncAccount, EMailSyncServer, EMailAccount, EPEmployee>.op_Implicit(pxResult);
        if (dictionary1.ContainsKey(key1))
        {
          List<int> intList = dictionary1[key1];
          int? employeeId = emailSyncAccount.EmployeeID;
          int num1 = employeeId.Value;
          if (intList.Contains(num1))
          {
            string address = emailSyncAccount.Address;
            EMailSyncPolicy policy = (EMailSyncPolicy) null;
            if (!string.IsNullOrEmpty(emailSyncAccount.PolicyName))
              policy = context.Policies[emailSyncAccount.PolicyName];
            if (policy == null && !string.IsNullOrEmpty(server.DefaultPolicyName))
              policy = context.Policies[server.DefaultPolicyName];
            if (policy == null)
              throw new PXException("Synchronization policy could not be found for account '{0}'.", new object[1]
              {
                (object) emailSyncAccount.Address
              });
            EmailsSyncMaint.ProcessingBox processingBox;
            if (!dictionary2.TryGetValue(policy.PolicyName, out processingBox))
              dictionary2[policy.PolicyName] = processingBox = new EmailsSyncMaint.ProcessingBox(policy);
            if (policy.ContactsSync.GetValueOrDefault())
            {
              List<PXSyncMailbox> contacts = processingBox.Contacts;
              string mailbox = address;
              employeeId = emailSyncAccount.EmployeeID;
              int employee = employeeId.Value;
              int? emailAccountId = emailSyncAccount.EmailAccountID;
              PXSyncMailboxPreset exportPreset = new PXSyncMailboxPreset(emailSyncAccount.ContactsExportDate, emailSyncAccount.ContactsExportFolder);
              PXSyncMailboxPreset importPreset = new PXSyncMailboxPreset(emailSyncAccount.ContactsImportDate, emailSyncAccount.ContactsImportFolder);
              int num2 = emailAccount.IncomingProcessing.GetValueOrDefault() ? 1 : 0;
              contacts.Add(new PXSyncMailbox(mailbox, employee, emailAccountId, exportPreset, importPreset, num2 != 0)
              {
                Reinitialize = emailSyncAccount.ToReinitialize.GetValueOrDefault(),
                IsReset = emailSyncAccount.IsReset.GetValueOrDefault()
              });
            }
            if (policy.EmailsSync.GetValueOrDefault())
            {
              List<PXSyncMailbox> emails = processingBox.Emails;
              string mailbox = address;
              employeeId = emailSyncAccount.EmployeeID;
              int employee = employeeId.Value;
              int? emailAccountId = emailSyncAccount.EmailAccountID;
              PXSyncMailboxPreset exportPreset = new PXSyncMailboxPreset(emailSyncAccount.EmailsExportDate, emailSyncAccount.EmailsExportFolder);
              PXSyncMailboxPreset importPreset = new PXSyncMailboxPreset(emailSyncAccount.EmailsImportDate, emailSyncAccount.EmailsImportFolder);
              int num3 = emailAccount.IncomingProcessing.GetValueOrDefault() ? 1 : 0;
              emails.Add(new PXSyncMailbox(mailbox, employee, emailAccountId, exportPreset, importPreset, num3 != 0)
              {
                Reinitialize = emailSyncAccount.ToReinitialize.GetValueOrDefault(),
                IsReset = emailSyncAccount.IsReset.GetValueOrDefault()
              });
            }
            if (policy.TasksSync.GetValueOrDefault())
            {
              List<PXSyncMailbox> tasks = processingBox.Tasks;
              string mailbox = address;
              employeeId = emailSyncAccount.EmployeeID;
              int employee = employeeId.Value;
              int? emailAccountId = emailSyncAccount.EmailAccountID;
              PXSyncMailboxPreset exportPreset = new PXSyncMailboxPreset(emailSyncAccount.TasksExportDate, emailSyncAccount.TasksExportFolder);
              PXSyncMailboxPreset importPreset = new PXSyncMailboxPreset(emailSyncAccount.TasksImportDate, emailSyncAccount.TasksImportFolder);
              int num4 = emailAccount.IncomingProcessing.GetValueOrDefault() ? 1 : 0;
              tasks.Add(new PXSyncMailbox(mailbox, employee, emailAccountId, exportPreset, importPreset, num4 != 0)
              {
                Reinitialize = emailSyncAccount.ToReinitialize.GetValueOrDefault(),
                IsReset = emailSyncAccount.IsReset.GetValueOrDefault()
              });
            }
            if (policy.EventsSync.GetValueOrDefault())
            {
              List<PXSyncMailbox> events = processingBox.Events;
              string mailbox = address;
              employeeId = emailSyncAccount.EmployeeID;
              int employee = employeeId.Value;
              int? emailAccountId = emailSyncAccount.EmailAccountID;
              PXSyncMailboxPreset exportPreset = new PXSyncMailboxPreset(emailSyncAccount.EventsExportDate, emailSyncAccount.EventsExportFolder);
              PXSyncMailboxPreset importPreset = new PXSyncMailboxPreset(emailSyncAccount.EventsImportDate, emailSyncAccount.EventsImportFolder);
              int num5 = emailAccount.IncomingProcessing.GetValueOrDefault() ? 1 : 0;
              events.Add(new PXSyncMailbox(mailbox, employee, emailAccountId, exportPreset, importPreset, num5 != 0)
              {
                Reinitialize = emailSyncAccount.ToReinitialize.GetValueOrDefault(),
                IsReset = emailSyncAccount.IsReset.GetValueOrDefault()
              });
            }
          }
        }
      }
      if (server != null)
      {
        List<Exception> source = new List<Exception>();
        foreach (string key2 in dictionary2.Keys)
        {
          EmailsSyncMaint.ProcessingBox processingBox = dictionary2[key2];
          using (IEmailSyncProvider exchanger = PXEmailSyncHelper.GetExchanger(server, processingBox.Policy))
          {
            foreach (PXEmailSyncOperation.Operations operations in Enum.GetValues(typeof (PXEmailSyncOperation.Operations)))
            {
              try
              {
                switch (operations - 1)
                {
                  case 0:
                    if (processingBox.EmailsPending)
                    {
                      exchanger.EmailsSync(processingBox.Policy, PXEmailSyncDirection.Parse(processingBox.Policy.EmailsDirection), (IEnumerable<PXSyncMailbox>) processingBox.Emails);
                      continue;
                    }
                    continue;
                  case 1:
                    if (processingBox.ContactsPending)
                    {
                      exchanger.ContactsSync(processingBox.Policy, PXEmailSyncDirection.Parse(processingBox.Policy.ContactsDirection), (IEnumerable<PXSyncMailbox>) processingBox.Contacts);
                      continue;
                    }
                    continue;
                  case 2:
                    if (processingBox.EventsPending)
                    {
                      exchanger.EventsSync(processingBox.Policy, PXEmailSyncDirection.Parse(processingBox.Policy.EventsDirection), (IEnumerable<PXSyncMailbox>) processingBox.Events);
                      continue;
                    }
                    continue;
                  case 3:
                    if (processingBox.TasksPending)
                    {
                      exchanger.TasksSync(processingBox.Policy, PXEmailSyncDirection.Parse(processingBox.Policy.TasksDirection), (IEnumerable<PXSyncMailbox>) processingBox.Tasks);
                      continue;
                    }
                    continue;
                  default:
                    continue;
                }
              }
              catch (PXExchangeSyncItemsException ex)
              {
                if (!processingBox.Policy.SkipError.GetValueOrDefault())
                {
                  if (ex.Errors.Count > 0)
                  {
                    foreach (string key3 in ex.Errors.Keys)
                    {
                      string message = string.Join(Environment.NewLine, ex.Errors[key3].ToArray());
                      context.StoreError(key1, key3, message);
                    }
                  }
                }
              }
              catch (PXExchangeSyncFatalException ex)
              {
                if (!processingBox.Policy.SkipError.GetValueOrDefault())
                {
                  source.Add((Exception) new PXException("An error occurred during the synchronization of {0}.", new object[1]
                  {
                    (object) operations.ToString().ToLowerInvariant()
                  }));
                  if (!string.IsNullOrEmpty(ex.Mailbox))
                    context.StoreError(key1, ex.Mailbox, ex.InnerMessage);
                  else
                    source.Add((Exception) ex);
                }
              }
              catch (Exception ex)
              {
                if (!processingBox.Policy.SkipError.GetValueOrDefault())
                {
                  source.Add((Exception) new PXException("An error occurred during the synchronization of {0}.", new object[1]
                  {
                    (object) operations.ToString().ToLowerInvariant()
                  }));
                  source.Add(ex);
                }
              }
            }
          }
        }
        if (source.Count > 0 && context.Exceptions.Count == 0)
          throw new PXException(string.Join(Environment.NewLine, source.Select<Exception, string>((Func<Exception, string>) (e => e.Message)).ToArray<string>()));
      }
    }
    for (int index = 0; index < context.Accounts.Count; ++index)
      PXProcessing.SetInfo(index, "The record has been processed successfully.");
    if (context.Exceptions.Count > 0)
    {
      foreach (int key in context.Exceptions.Keys)
      {
        List<string> exception = context.Exceptions[key];
        if (exception != null && exception.Count >= 0)
          PXProcessing.SetError(key, string.Join(Environment.NewLine, exception.ToArray()));
      }
      throw new PXException("Synchronization with Exchange has been completed with errors. For details on each error, see the error messages in the grid below.");
    }
  }

  protected Dictionary<string, EMailSyncPolicy> GetPolicies()
  {
    Dictionary<string, EMailSyncPolicy> policies = new Dictionary<string, EMailSyncPolicy>();
    foreach (PXResult<EMailSyncPolicy> pxResult in PXSelectBase<EMailSyncPolicy, PXSelect<EMailSyncPolicy>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      EMailSyncPolicy emailSyncPolicy = PXResult<EMailSyncPolicy>.op_Implicit(pxResult);
      policies[emailSyncPolicy.PolicyName] = emailSyncPolicy;
    }
    return policies;
  }

  protected void EMailSyncAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EMailSyncAccount row = e.Row as EMailSyncAccount;
    ((PXAction) this.Status).SetEnabled(row != null);
    if (row == null)
      return;
    ((PXAction) this.ResetContacts).SetEnabled(row.IsContactsReset.GetValueOrDefault());
    PXAction<EMailAccountSyncFilter> resetTasks = this.ResetTasks;
    bool? nullable = row.IsTasksReset;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    ((PXAction) resetTasks).SetEnabled(num1 != 0);
    PXAction<EMailAccountSyncFilter> resetEvents = this.ResetEvents;
    nullable = row.IsEventsReset;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    ((PXAction) resetEvents).SetEnabled(num2 != 0);
    PXAction<EMailAccountSyncFilter> resetEmails = this.ResetEmails;
    nullable = row.IsEmailsReset;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    ((PXAction) resetEmails).SetEnabled(num3 != 0);
    nullable = row.HasErrors;
    if (!nullable.GetValueOrDefault())
      return;
    cache.RaiseExceptionHandling<EMailSyncAccount.address>((object) row, (object) row.Address, (Exception) new PXSetPropertyException("Some items were not synchronized. Check synchronization status and log for more details.", (PXErrorLevel) 2));
  }

  protected void EMailSyncAccount_ContactsExportDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!EmailsSyncMaint.DatesVerifier<EMailSyncAccount.contactsExportDate>(cache, e))
      return;
    cache.SetValue<EMailSyncAccount.isContactsReset>(e.Row, (object) true);
  }

  protected void EMailSyncAccount_TasksExportDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!EmailsSyncMaint.DatesVerifier<EMailSyncAccount.tasksExportDate>(cache, e))
      return;
    cache.SetValue<EMailSyncAccount.isTasksReset>(e.Row, (object) true);
  }

  protected void EMailSyncAccount_EventsExportDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!EmailsSyncMaint.DatesVerifier<EMailSyncAccount.eventsExportDate>(cache, e))
      return;
    cache.SetValue<EMailSyncAccount.isEventsReset>(e.Row, (object) true);
  }

  protected void EMailSyncAccount_EmailsExportDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!EmailsSyncMaint.DatesVerifier<EMailSyncAccount.emailsExportDate>(cache, e))
      return;
    cache.SetValue<EMailSyncAccount.isEmailsReset>(e.Row, (object) true);
  }

  protected void EMailSyncAccount_ContactsExportDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EmailsSyncMaint.DatesSwapper<EMailSyncAccount.contactsExportDate, EMailSyncAccount.contactsImportDate>(cache, e);
  }

  protected void EMailSyncAccount_TasksExportDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EmailsSyncMaint.DatesSwapper<EMailSyncAccount.tasksExportDate, EMailSyncAccount.tasksImportDate>(cache, e);
  }

  protected void EMailSyncAccount_EventsExportDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EmailsSyncMaint.DatesSwapper<EMailSyncAccount.eventsExportDate, EMailSyncAccount.eventsImportDate>(cache, e);
  }

  protected void EMailSyncAccount_EmailsExportDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EmailsSyncMaint.DatesSwapper<EMailSyncAccount.emailsExportDate, EMailSyncAccount.emailsImportDate>(cache, e);
  }

  private static void DatesSwapper<TExport, TImport>(PXCache cache, PXFieldUpdatedEventArgs e)
    where TExport : IBqlField
    where TImport : IBqlField
  {
    if (!(e.Row is EMailSyncAccount row))
      return;
    PXFieldState stateExt = cache.GetStateExt((object) row, typeof (TExport).Name) as PXFieldState;
    cache.SetValue<TImport>((object) row, stateExt?.Value);
  }

  private static bool DatesVerifier<TExport>(PXCache cache, PXFieldVerifyingEventArgs e) where TExport : IBqlField
  {
    if (!(e.Row is EMailSyncAccount row))
      return false;
    DateTime? valueOriginal = (DateTime?) cache.GetValueOriginal<TExport>((object) row);
    DateTime? newValue = (DateTime?) e.NewValue;
    if (!valueOriginal.HasValue && !newValue.HasValue)
      return false;
    DateTime? nullable1 = newValue;
    DateTime now = PXTimeZoneInfo.Now;
    if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() >= now ? 1 : 0) : 0) != 0)
    {
      e.NewValue = (object) valueOriginal;
      cache.RaiseExceptionHandling<TExport>((object) row, (object) valueOriginal, (Exception) new PXSetPropertyException("The specified date and time must not exceed the current date and time.", (PXErrorLevel) 4));
      return false;
    }
    DateTime? nullable2 = newValue;
    DateTime? nullable3 = valueOriginal;
    if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      cache.RaiseExceptionHandling<TExport>((object) row, (object) newValue, (Exception) new PXSetPropertyException("The specified date and time exceeds the date and time of the last synchronization attempt.", (PXErrorLevel) 2));
    return true;
  }

  protected class ProcessingContext
  {
    public readonly EMailAccountSyncFilter Filter;
    public readonly List<EMailSyncAccount> Accounts;
    public readonly Dictionary<int, List<string>> Exceptions;
    public readonly Dictionary<string, EMailSyncPolicy> Policies;

    public ProcessingContext(
      EMailAccountSyncFilter filter,
      List<EMailSyncAccount> accounts,
      Dictionary<string, EMailSyncPolicy> policies)
    {
      this.Filter = filter;
      this.Accounts = accounts;
      this.Policies = policies;
      this.Exceptions = new Dictionary<int, List<string>>();
    }

    public void StoreError(int server, string address, string message)
    {
      int index = this.Accounts.FindIndex((Predicate<EMailSyncAccount>) (a =>
      {
        int? serverId = a.ServerID;
        int num = server;
        return serverId.GetValueOrDefault() == num & serverId.HasValue && a.Address == address;
      }));
      List<string> stringList = (List<string>) null;
      if (!this.Exceptions.TryGetValue(index, out stringList))
        this.Exceptions[index] = stringList = new List<string>();
      stringList.Add(message);
    }
  }

  protected class ProcessingBox
  {
    public EMailSyncPolicy Policy;
    public List<PXSyncMailbox> Emails = new List<PXSyncMailbox>();
    public List<PXSyncMailbox> Contacts = new List<PXSyncMailbox>();
    public List<PXSyncMailbox> Tasks = new List<PXSyncMailbox>();
    public List<PXSyncMailbox> Events = new List<PXSyncMailbox>();

    public bool EmailsPending => this.Emails.Count > 0;

    public bool ContactsPending => this.Contacts.Count > 0;

    public bool TasksPending => this.Tasks.Count > 0;

    public bool EventsPending => this.Events.Count > 0;

    public ProcessingBox(EMailSyncPolicy policy) => this.Policy = policy;
  }
}
