// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSyncServerMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update;
using PX.Data.Update.ExchangeService;
using PX.Data.Update.WebServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class EMailSyncServerMaint : PXGraph<EMailSyncServerMaint, EMailSyncServer>
{
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (EMailSyncServer.oAuthApplicationID)})]
  public PXSelect<EMailSyncServer> Servers;
  [PXCopyPasteHiddenView]
  public PXSelect<EMailAccount, Where<EMailAccount.emailAccountType, Equal<EmailAccountTypesAttribute.exchange>>> EmailAccounts;
  public PXSelect<UserPreferences> UserSettings;
  [PXCopyPasteHiddenView]
  public PXSelect<EMailSyncAccountPreferences> SyncAccountPreferences;
  [PXCopyPasteHiddenView]
  public PXSelect<EMailSyncAccount, Where<EMailSyncAccount.serverID, Equal<Current<EMailSyncServer.accountID>>>, OrderBy<Desc<EMailSyncAccount.syncAccount>>> SyncAccounts;
  public PXAction<EMailSyncServer> TestCommand;
  public PXAction<EMailSyncServer> InitializeCommand;
  protected Dictionary<int, EMailAccount> rowsMapping = new Dictionary<int, EMailAccount>();

  public IEnumerable syncAccounts()
  {
    EMailSyncServerMaint graph = this;
    if (graph.Servers.Current != null && graph.Servers.Current.AccountCD != null)
    {
      foreach (EMailSyncAccount row in graph.QuickSelect(graph.SyncAccounts.View.BqlSelect))
      {
        if (graph.SyncAccounts.Cache.Locate((object) row) == null)
        {
          row.IsVitrual = new bool?(false);
          row.SyncAccount = new bool?(true);
          graph.SyncAccounts.Cache.Hold((object) row);
        }
      }
      foreach (EMailSyncAccount emailSyncAccount in graph.SyncAccounts.Cache.Cached)
        yield return (object) emailSyncAccount;
    }
  }

  public EMailSyncServerMaint()
  {
    PXCache cach = this.Caches[typeof (EMailSyncServer)];
    this.EmailAccounts.Cache.AutoSave = true;
    this.UserSettings.Cache.AutoSave = true;
    this.SyncAccounts.Cache.AllowSelect = false;
    this.SyncAccounts.Cache.AutoSave = true;
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton]
  protected virtual IEnumerable save(PXAdapter adapter)
  {
    this.Persist();
    this.Cancel.Press();
    return adapter.Get();
  }

  [PXProcessButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Test Server", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable testCommand(PXAdapter adapter)
  {
    return this.Do(adapter, new Action<EMailSyncServerMaint, PXExchangeServer, EMailSyncServer, EMailSyncAccount, EMailSyncPolicy>(this.CheckAccount));
  }

  [PXProcessButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Initialize Server", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable initializeCommand(PXAdapter adapter)
  {
    return this.Do(adapter, new Action<EMailSyncServerMaint, PXExchangeServer, EMailSyncServer, EMailSyncAccount, EMailSyncPolicy>(this.InitAccount));
  }

  private IEnumerable Do(
    PXAdapter adapter,
    Action<EMailSyncServerMaint, PXExchangeServer, EMailSyncServer, EMailSyncAccount, EMailSyncPolicy> action)
  {
    if (adapter.Get<EMailSyncServer>().FirstOrDefault<EMailSyncServer>() == null)
      throw new PXException("A snapshot is not selected.");
    List<EMailSyncServer> servers = new List<EMailSyncServer>();
    foreach (object obj in adapter.Get())
      servers.Add((EMailSyncServer) obj);
    PXLongOperation.ClearStatus(this.UID);
    PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => EMailSyncServerMaint.ProcessAccount((IEnumerable<EMailSyncServer>) servers, action)));
    return (IEnumerable) servers;
  }

  protected static void ProcessAccount(
    IEnumerable<EMailSyncServer> servers,
    Action<EMailSyncServerMaint, PXExchangeServer, EMailSyncServer, EMailSyncAccount, EMailSyncPolicy> action)
  {
    EMailSyncServerMaint instance = PXGraph.CreateInstance<EMailSyncServerMaint>();
    foreach (EMailSyncServer server in servers)
    {
      Dictionary<EMailSyncAccount, string> info = new Dictionary<EMailSyncAccount, string>();
      if (server.ServerType == "E")
      {
        PXExchangeServer gate = PXExchangeServer.GetGate(server);
        EMailSyncPolicy emailSyncPolicy1 = (EMailSyncPolicy) PXSelectBase<EMailSyncPolicy, PXSelect<EMailSyncPolicy, Where<EMailSyncPolicy.policyName, Equal<Required<EMailSyncPolicy.policyName>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, (object) server.DefaultPolicyName);
        foreach (PXResult<EMailSyncAccount, EMailSyncPolicy> pxResult in PXSelectBase<EMailSyncAccount, PXSelectJoin<EMailSyncAccount, LeftJoin<EMailSyncPolicy, On<EMailSyncAccount.policyName, Equal<EMailSyncPolicy.policyName>>>, Where<EMailSyncAccount.serverID, Equal<Required<EMailSyncServer.accountID>>>>.Config>.Select((PXGraph) instance, (object) server.AccountID))
        {
          EMailSyncAccount key = (EMailSyncAccount) pxResult;
          EMailSyncPolicy emailSyncPolicy2 = (EMailSyncPolicy) pxResult;
          try
          {
            if (emailSyncPolicy2 == null || string.IsNullOrEmpty(emailSyncPolicy2.PolicyName))
              emailSyncPolicy2 = emailSyncPolicy1;
            if (emailSyncPolicy2 == null || string.IsNullOrEmpty(emailSyncPolicy2.PolicyName))
              throw new PXException("The policy is not specified for the email account {0}.", new object[1]
              {
                (object) key.Address
              });
            action(instance, gate, server, key, emailSyncPolicy2);
          }
          catch (Exception ex)
          {
            info[key] = ex.Message;
          }
        }
      }
      if (info.Count > 0)
      {
        PXLongOperation.SetCustomInfo((object) info);
        throw new PXException("At least one item has not been processed.");
      }
    }
  }

  protected virtual void CheckAccount(
    EMailSyncServerMaint graph,
    PXExchangeServer gate,
    EMailSyncServer server,
    EMailSyncAccount account,
    EMailSyncPolicy policy)
  {
    gate.TestAccount(account.Address);
  }

  protected virtual void InitAccount(
    EMailSyncServerMaint graph,
    PXExchangeServer gate,
    EMailSyncServer server,
    EMailSyncAccount account,
    EMailSyncPolicy policy)
  {
    CategoryColor result = CategoryColor.Black;
    if (!Enum.TryParse<CategoryColor>(policy.Color, out result))
      result = CategoryColor.Black;
    gate.EnsureCategory(account.Address, policy.Category, result);
    bool? nullable = policy.ContactsSync;
    if (nullable.GetValueOrDefault())
    {
      nullable = policy.ContactsSeparated;
      if (nullable.GetValueOrDefault() && !string.IsNullOrEmpty(policy.ContactsFolder))
      {
        foreach (PXOperationResult<PXExchangeFolderID> ensureFolder in gate.EnsureFolders<ContactsFolderType>(new PXExchangeFolderDefinition(account.Address, DistinguishedFolderIdNameType.contacts, policy.ContactsFolder, (string) null)))
        {
          if (!ensureFolder.Success)
            throw ensureFolder.Error;
        }
      }
    }
    nullable = policy.TasksSync;
    if (nullable.GetValueOrDefault())
    {
      nullable = policy.TasksSeparated;
      if (nullable.GetValueOrDefault() && !string.IsNullOrEmpty(policy.TasksFolder))
      {
        foreach (PXOperationResult<PXExchangeFolderID> ensureFolder in gate.EnsureFolders<TasksFolderType>(new PXExchangeFolderDefinition(account.Address, DistinguishedFolderIdNameType.tasks, policy.TasksFolder, (string) null)))
        {
          if (!ensureFolder.Success)
            throw ensureFolder.Error;
        }
      }
    }
    nullable = policy.EventsSync;
    if (nullable.GetValueOrDefault())
    {
      nullable = policy.EventsSeparated;
      if (nullable.GetValueOrDefault() && !string.IsNullOrEmpty(policy.EventsFolder))
      {
        foreach (PXOperationResult<PXExchangeFolderID> ensureFolder in gate.EnsureFolders<CalendarFolderType>(new PXExchangeFolderDefinition(account.Address, DistinguishedFolderIdNameType.calendar, policy.EventsFolder, (string) null)))
        {
          if (!ensureFolder.Success)
            throw ensureFolder.Error;
        }
      }
    }
    nullable = policy.EmailsSync;
    if (nullable.GetValueOrDefault() && !string.IsNullOrEmpty(policy.EmailsFolder))
    {
      foreach (PXOperationResult<PXExchangeFolderID> ensureFolder in gate.EnsureFolders<FolderType>(new PXExchangeFolderDefinition(account.Address, DistinguishedFolderIdNameType.inbox, policy.EmailsFolder, (string) null)))
      {
        if (!ensureFolder.Success)
          throw ensureFolder.Error;
      }
      foreach (PXOperationResult<PXExchangeFolderID> ensureFolder in gate.EnsureFolders<FolderType>(new PXExchangeFolderDefinition(account.Address, DistinguishedFolderIdNameType.sentitems, policy.EmailsFolder, (string) null)))
      {
        if (!ensureFolder.Success)
          throw ensureFolder.Error;
      }
    }
    PXCache cach = graph.Caches[typeof (EMailSyncAccount)];
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.serverID, Equal<Required<EMailSyncAccount.serverID>>, And<EMailSyncAccount.address, Equal<Required<EMailSyncAccount.address>>>>>.Config>.Select((PXGraph) graph, (object) server.AccountID, (object) account.Address))
    {
      EMailSyncAccount emailSyncAccount = (EMailSyncAccount) pxResult;
      account.ContactsExportFolder = (string) null;
      account.ContactsImportFolder = (string) null;
      account.EmailsExportFolder = (string) null;
      account.EmailsImportFolder = (string) null;
      account.EventsExportFolder = (string) null;
      account.EventsImportFolder = (string) null;
      account.TasksExportFolder = (string) null;
      account.TasksImportFolder = (string) null;
      cach.Update((object) emailSyncAccount);
    }
    cach.Persist(PXDBOperation.Update);
  }

  protected virtual void EMailSyncServer_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool isEnabled = e.Row is EMailSyncServer row && cache.GetStatus((object) row) != PXEntryStatus.Inserted;
    this.InitializeCommand.SetEnabled(isEnabled);
    this.TestCommand.SetEnabled(isEnabled);
    if (PXLongOperation.GetCustomInfo(this.UID) is Dictionary<EMailSyncAccount, string> customInfo && customInfo.Count <= 0)
      PXLongOperation.ClearStatus(this.UID);
    PXUIFieldAttribute.SetEnabled<EMailSyncAccount.syncAccount>(this.Caches[typeof (EMailSyncAccount)], (object) null, isEnabled);
  }

  protected virtual void EMailSyncServer_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EMailSyncServer row))
      return;
    foreach (PXResult pxResult in PXSelectBase<EMailSyncAccount, PXSelectJoin<EMailSyncAccount, InnerJoin<EMailAccount, On<EMailSyncAccount.emailAccountID, Equal<EMailAccount.emailAccountID>>>, Where<EMailSyncAccount.serverID, Equal<Required<EMailSyncAccount.serverID>>>>.Config>.Select((PXGraph) this, (object) row.AccountID))
    {
      EMailAccount emailAccount = pxResult.GetItem<EMailAccount>();
      if (emailAccount != null && !(emailAccount.EmailAccountType != "E"))
        this.EmailAccounts.Delete(emailAccount);
    }
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.serverID, Equal<Required<EMailSyncAccount.serverID>>>>.Config>.Select((PXGraph) this, (object) row.AccountID))
      this.SyncAccounts.Delete((EMailSyncAccount) pxResult);
  }

  protected virtual void EMailSyncServer_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EMailSyncServer row) || e.Operation != PXDBOperation.Update)
      return;
    foreach (PXResult<EMailSyncAccount> pxResult in PXSelectBase<EMailSyncAccount, PXSelect<EMailSyncAccount, Where<EMailSyncAccount.serverID, Equal<Required<EMailSyncAccount.serverID>>>>.Config>.Select((PXGraph) this, (object) row.AccountID))
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
      this.SyncAccounts.Cache.Update((object) emailSyncAccount);
    }
  }

  protected virtual void EMailSyncServer_SyncAttachmentSize_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is EMailSyncAccount && e.NewValue is int? && (int) e.NewValue > SitePolicy.MaxRequestSize)
      throw new PXSetPropertyException<EMailSyncServer.syncAttachmentSize>("The attachment size for exchange sync has to be less than the maximum allowed attachment size.");
  }

  protected virtual void EMailSyncAccount_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EMailSyncAccount row))
      return;
    EMailAccount emailAccount = (EMailAccount) this.EmailAccounts.Search<EMailAccount.emailAccountID>((object) row.EmailAccountID);
    if (emailAccount == null || emailAccount.EmailAccountType != "E")
      return;
    this.EmailAccounts.Delete(emailAccount);
  }

  protected virtual void EMailSyncAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EMailSyncAccount row = e.Row as EMailSyncAccount;
    if (!(PXLongOperation.GetCustomInfo(this.UID) is Dictionary<EMailSyncAccount, string> customInfo))
      return;
    foreach (EMailSyncAccount emailSyncAccount in customInfo.Keys.ToArray<EMailSyncAccount>())
    {
      if (cache.ObjectsEqual((object) emailSyncAccount, (object) row))
      {
        cache.RaiseExceptionHandling<EMailSyncAccount.address>((object) row, (object) row.Address, (Exception) new PXSetPropertyException<EMailSyncAccount.address>(customInfo[emailSyncAccount], PXErrorLevel.RowError));
        customInfo.Remove(emailSyncAccount);
      }
    }
  }

  protected virtual void EMailSyncAccount_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    EMailSyncAccount row = e.Row as EMailSyncAccount;
    if (!row.IsVitrual.GetValueOrDefault() || cache.GetStatus((object) row) == PXEntryStatus.InsertedDeleted)
      return;
    cache.SetStatus((object) row, PXEntryStatus.Inserted);
  }

  protected virtual void EMailSyncAccount_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EMailSyncAccount row))
      return;
    int? emailAccountId = row.EmailAccountID;
    if (!emailAccountId.HasValue)
      return;
    Dictionary<int, EMailAccount> rowsMapping = this.rowsMapping;
    emailAccountId = row.EmailAccountID;
    int key = emailAccountId.Value;
    EMailAccount emailAccount;
    ref EMailAccount local = ref emailAccount;
    if (!rowsMapping.TryGetValue(key, out local))
      return;
    row.EmailAccountID = emailAccount.EmailAccountID;
  }

  protected virtual void EMailSyncAccount_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is EMailSyncAccount row) || !row.EmailAccountID.HasValue || e.Operation == PXDBOperation.Delete)
      return;
    row.IsVitrual = new bool?(false);
    this.Clear();
  }

  protected virtual void EMailSyncAccount_SyncAccount_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EMailSyncAccount row = e.Row as EMailSyncAccount;
    if (cache.GetStatus((object) row) == PXEntryStatus.Notchanged)
      return;
    EMailAccount emailAccount = (EMailAccount) null;
    if (row.EmailAccountID.HasValue)
      emailAccount = (EMailAccount) this.EmailAccounts.Search<EMailAccount.emailAccountID>((object) row.EmailAccountID);
    if (emailAccount != null && emailAccount.EmailAccountType != "E")
      return;
    UserPreferences userPreferences1 = (UserPreferences) null;
    if (row.OwnerID.HasValue)
    {
      PXAccess.Contact contact = (PXAccess.Contact) PXSelectBase<PXAccess.Contact, PXSelect<PXAccess.Contact, Where<PXAccess.Contact.contactID, Equal<Required<PXAccess.Contact.contactID>>>>.Config>.Select((PXGraph) this, (object) row.OwnerID);
      userPreferences1 = (UserPreferences) this.UserSettings.Search<UserPreferences.userID>((object) (Guid?) contact?.UserID);
      if (userPreferences1 == null)
        userPreferences1 = this.UserSettings.Insert(new UserPreferences()
        {
          UserID = (Guid?) contact?.UserID
        });
    }
    bool? syncAccount = row.SyncAccount;
    if (syncAccount.GetValueOrDefault())
    {
      if (emailAccount != null)
        return;
      emailAccount = this.EmailAccounts.Insert(new EMailAccount()
      {
        Description = row.EmployeeCD,
        Address = row.Address,
        EmailAccountType = "E",
        IncomingHostProtocol = new int?(2),
        IncomingProcessing = new bool?(true),
        ForbidRouting = new bool?(true),
        CreateActivity = new bool?(true),
        DefaultOwnerID = row.OwnerID
      });
      row.EmailAccountID = emailAccount.EmailAccountID;
    }
    else
    {
      this.SyncAccounts.Delete(row);
      if (emailAccount == null)
        return;
      this.EmailAccounts.Delete(emailAccount);
    }
    if (userPreferences1 == null)
      return;
    syncAccount = row.SyncAccount;
    UserPreferences userPreferences2;
    if (syncAccount.GetValueOrDefault())
    {
      userPreferences1.DefaultEMailAccountID = emailAccount.EmailAccountID;
      userPreferences2 = this.UserSettings.Update(userPreferences1);
    }
    else
    {
      int? defaultEmailAccountId = userPreferences1.DefaultEMailAccountID;
      int? nullable1 = emailAccount.EmailAccountID;
      if (!(defaultEmailAccountId.GetValueOrDefault() == nullable1.GetValueOrDefault() & defaultEmailAccountId.HasValue == nullable1.HasValue))
        return;
      UserPreferences userPreferences3 = userPreferences1;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      userPreferences3.DefaultEMailAccountID = nullable2;
      userPreferences2 = this.UserSettings.Update(userPreferences1);
    }
  }

  protected virtual void EMailSyncAccount_PolicyName_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EMailSyncAccount row))
      return;
    if (e.OldValue != null)
    {
      EMailSyncAccountPreferences accountPreferences = (EMailSyncAccountPreferences) this.SyncAccountPreferences.Search<EMailSyncAccountPreferences.policyName, EMailSyncAccountPreferences.employeeID>(e.OldValue, (object) row.EmployeeID);
      if (accountPreferences != null)
        this.SyncAccountPreferences.Cache.Delete((object) accountPreferences);
    }
    if (string.IsNullOrEmpty(row.PolicyName) || (EMailSyncAccountPreferences) this.SyncAccountPreferences.Search<EMailSyncAccountPreferences.policyName, EMailSyncAccountPreferences.employeeID>((object) row.PolicyName, (object) row.EmployeeID) != null)
      return;
    this.SyncAccountPreferences.Insert(new EMailSyncAccountPreferences()
    {
      EmployeeID = row.EmployeeID,
      PolicyName = row.PolicyName,
      Address = row.Address
    });
    this.SyncAccountPreferences.Cache.IsDirty = false;
  }

  protected virtual void EMailAccount_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EMailAccount row))
      return;
    int? emailAccountId = row.EmailAccountID;
    if (!emailAccountId.HasValue)
      return;
    emailAccountId = row.EmailAccountID;
    if (emailAccountId.Value >= 0)
      return;
    Dictionary<int, EMailAccount> rowsMapping = this.rowsMapping;
    emailAccountId = row.EmailAccountID;
    int key = emailAccountId.Value;
    EMailAccount emailAccount = row;
    rowsMapping[key] = emailAccount;
  }

  protected virtual void UserPreferences_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is UserPreferences row))
      return;
    int? defaultEmailAccountId = row.DefaultEMailAccountID;
    if (!defaultEmailAccountId.HasValue)
      return;
    Dictionary<int, EMailAccount> rowsMapping = this.rowsMapping;
    defaultEmailAccountId = row.DefaultEMailAccountID;
    int key = defaultEmailAccountId.Value;
    EMailAccount emailAccount;
    ref EMailAccount local = ref emailAccount;
    if (!rowsMapping.TryGetValue(key, out local))
      return;
    row.DefaultEMailAccountID = emailAccount.EmailAccountID;
  }
}
