// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.EMailSyncServerMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS.Email;

public class EMailSyncServerMaintExt : PXGraphExtension<EMailSyncServerMaint>
{
  public PXSelect<EMailSyncAccount, Where<EMailSyncAccount.serverID, Equal<Current<EMailSyncServer.accountID>>>, OrderBy<Desc<EMailSyncAccount.syncAccount>>> SyncAccounts;

  public IEnumerable syncAccounts()
  {
    List<EMailSyncAccount> list = GraphHelper.RowCast<EMailSyncAccount>((IEnumerable) ((PXSelectBase<EMailSyncAccount>) this.Base.SyncAccounts).Select(Array.Empty<object>())).ToList<EMailSyncAccount>();
    if (((PXSelectBase<EMailSyncServer>) this.Base.Servers).Current == null || ((PXSelectBase<EMailSyncServer>) this.Base.Servers).Current.AccountCD == null)
      return (IEnumerable) new EMailSyncAccount[0];
    bool isDirty = ((PXSelectBase) this.Base.SyncAccounts).Cache.IsDirty;
    foreach (PXResult<EPEmployee, Contact> pxResult in PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, InnerJoin<Contact, On<EPEmployee.defContactID, Equal<Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>>>>, Where<Contact.eMail, IsNotNull, And<Contact.userID, IsNotNull>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      EMailSyncAccount emailSyncAccount1 = new EMailSyncAccount();
      emailSyncAccount1.ServerID = ((PXSelectBase<EMailSyncServer>) this.Base.Servers).Current.AccountID;
      emailSyncAccount1.EmployeeID = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).BAccountID;
      emailSyncAccount1.Address = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).EMail;
      emailSyncAccount1.EmployeeCD = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).AcctName;
      emailSyncAccount1.OwnerID = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).ContactID;
      emailSyncAccount1.EmployeeStatus = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).VStatus;
      if (((PXSelectBase) this.Base.SyncAccounts).Cache.Locate((object) emailSyncAccount1) == null)
      {
        emailSyncAccount1.IsVitrual = new bool?(true);
        emailSyncAccount1.SyncAccount = new bool?(false);
        EMailSyncAccount emailSyncAccount2 = (EMailSyncAccount) ((PXSelectBase) this.Base.SyncAccounts).Cache.Insert((object) emailSyncAccount1);
        if (emailSyncAccount2 != null)
        {
          ((PXSelectBase) this.Base.SyncAccounts).Cache.SetStatus((object) emailSyncAccount2, (PXEntryStatus) 5);
          list.Add(emailSyncAccount2);
        }
      }
    }
    ((PXSelectBase) this.Base.SyncAccounts).Cache.IsDirty = isDirty;
    return (IEnumerable) list;
  }

  public virtual void Initialize()
  {
    ((PXSelectBase) this.Base.SyncAccounts).Cache.AllowSelect = true;
  }

  protected virtual void EMailSyncAccount_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EMailSyncAccount row))
      return;
    int? nullable1 = row.EmployeeID;
    bool? nullable2;
    if (nullable1.HasValue && row.Address == null)
    {
      nullable2 = row.IsVitrual;
      bool flag = false;
      if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
      {
        foreach (PXResult<EPEmployee, Contact> pxResult in PXSelectBase<EPEmployee, PXSelectJoin<EPEmployee, InnerJoin<Contact, On<EPEmployee.defContactID, Equal<Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>>>>, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) row.EmployeeID
        }))
        {
          row.EmployeeCD = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).AcctName;
          row.OwnerID = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).ContactID;
          row.Address = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).EMail;
          row.EmployeeStatus = PXResult<EPEmployee, Contact>.op_Implicit(pxResult).VStatus;
        }
      }
    }
    nullable2 = row.SyncAccount;
    if (!nullable2.GetValueOrDefault())
      return;
    nullable1 = row.EmailAccountID;
    if (nullable1.HasValue)
      return;
    EMailAccount emailAccount = (EMailAccount) null;
    UserPreferences userPreferences = (UserPreferences) null;
    nullable1 = row.OwnerID;
    if (nullable1.HasValue)
    {
      userPreferences = PXResultset<UserPreferences>.op_Implicit(((PXSelectBase<UserPreferences>) this.Base.UserSettings).Search<UserPreferences.userID>((object) PXAccess.GetUserID(row.OwnerID), Array.Empty<object>()));
      if (userPreferences == null)
        userPreferences = ((PXSelectBase<UserPreferences>) this.Base.UserSettings).Insert(new UserPreferences()
        {
          UserID = PXAccess.GetUserID(row.OwnerID)
        });
    }
    using (new PXReadDeletedScope(false))
    {
      emailAccount = PXResultset<EMailAccount>.op_Implicit(PXSelectBase<EMailAccount, PXSelectReadonly<EMailAccount, Where<EMailAccount.address, Equal<Required<EMailAccount.address>>, And<EMailAccount.emailAccountType, Equal<EmailAccountTypesAttribute.exchange>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) row.Address
      }));
      if (emailAccount == null)
      {
        emailAccount = new EMailAccount();
        emailAccount.Description = row.EmployeeCD;
        emailAccount.Address = row.Address;
        emailAccount.EmailAccountType = "E";
        emailAccount.IncomingHostProtocol = new int?(2);
        emailAccount.IncomingProcessing = new bool?(true);
        emailAccount.ForbidRouting = new bool?(true);
        emailAccount.CreateActivity = new bool?(true);
        emailAccount.DefaultOwnerID = row.OwnerID;
        emailAccount = ((PXSelectBase<EMailAccount>) this.Base.EmailAccounts).Insert(emailAccount);
      }
      else
      {
        emailAccount.DeletedDatabaseRecord = new bool?(false);
        ((PXSelectBase<EMailAccount>) this.Base.EmailAccounts).Update(emailAccount);
      }
    }
    row.EmailAccountID = emailAccount.EmailAccountID;
    if (userPreferences == null || ((PXSelectBase) this.Base.EmailAccounts).Cache.GetStatus((object) emailAccount) != 2)
      return;
    userPreferences.DefaultEMailAccountID = emailAccount.EmailAccountID;
    ((PXSelectBase<UserPreferences>) this.Base.UserSettings).Update(userPreferences);
  }
}
