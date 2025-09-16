// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.ExchangeContactsSyncCommand
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
using PX.SM;
using PX.TM;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Objects.CS.Email;

public class ExchangeContactsSyncCommand : 
  ExchangeBaseLogicSyncCommand<ContactMaint, Contact, ContactItemType>
{
  public ExchangeContactsSyncCommand(MicrosoftExchangeSyncProvider provider)
    : base(provider, "Contacts", (PXExchangeFindOptions) 16 /*0x10*/)
  {
    if (!this.Policy.ContactsSkipCategory.GetValueOrDefault())
      return;
    this.DefFindOptions = (PXExchangeFindOptions) (this.DefFindOptions | 4);
  }

  protected override void ConfigureEnvironment(
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    this.EnsureEnvironmentConfigured<ContactsFolderType>(mailboxes, new PXSyncFolderSpecification(this.Policy.ContactsSeparated.GetValueOrDefault() ? this.Policy.ContactsFolder : (string) null, (DistinguishedFolderIdNameType) 1));
  }

  protected override List<PXSyncResult> PrepareImport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported = null)
  {
    List<PXSyncResult> source = new List<PXSyncResult>();
    source.AddRange(this.ProcessSyncImport(((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => !m.ImportPreset.Date.HasValue)).ToArray<PXSyncMailbox>(), exported));
    source.AddRange(this.ProcessSyncImport(((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => m.ImportPreset.Date.HasValue)).ToArray<PXSyncMailbox>(), exported));
    if (source.Count > 0 && (PXEmailSyncDirection.Parse(this.Policy.ContactsDirection) & 2) == 2)
    {
      PXSyncItemSet imported = new PXSyncItemSet(source.Where<PXSyncResult>((Func<PXSyncResult, bool>) (p => p.Success && p.ItemStatus != PXSyncItemStatus.Deleted)).Select<PXSyncResult, PXSyncItemID>((Func<PXSyncResult, PXSyncItemID>) (p => (PXSyncItemID) p)));
      source.AddRange(this.ProcessSyncExport(mailboxes, imported));
    }
    source.AddRange(this.ProcessSyncExportUnsynced(mailboxes, (PXSyncItemSet) null));
    return source;
  }

  protected override void ExportImportFirst(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    List<PXExchangeItem<ContactItemType>> serverItemsList = this.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, (PXExchangeFindOptions) 256 /*0x0100*/).ToList<PXExchangeItem<ContactItemType>>();
    BqlCommand selectCommand = this.GetSelectCommand();
    bool flag;
    using (new PXReadDeletedScope(false))
      flag = this.SelectItems<Contact.noteID>((PXGraph) this.graph, selectCommand, (IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, PXSyncItemStatus.Updated, this.Attachments).Where<PXSyncItemBucket<Contact>>((Func<PXSyncItemBucket<Contact>, bool>) (bucket => serverItemsList.FirstOrDefault<PXExchangeItem<ContactItemType>>((Func<PXExchangeItem<ContactItemType>, bool>) (_ => ((ItemType) _.Item).ItemId.Id == bucket.Reference.BinaryReference && ((ItemType) _.Item).ItemId.ChangeKey != bucket.Reference.BinaryChangeKey)) != null)).Any<PXSyncItemBucket<Contact>>();
    if (flag)
      this.ExportFirst(policy, direction, mailboxes);
    else
      this.ImportFirst(policy, direction, mailboxes);
  }

  protected override void LastUpdated(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    PXSyncItemSet excludes = (PXSyncItemSet) null;
    if ((direction & 3) == 3)
    {
      List<PXExchangeItem<ContactItemType>> serverItemsList = this.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, (PXExchangeFindOptions) 256 /*0x0100*/).ToList<PXExchangeItem<ContactItemType>>();
      BqlCommand selectCommand = this.GetSelectCommand();
      using (new PXReadDeletedScope(false))
        excludes = new PXSyncItemSet(this.SelectItems<Contact.noteID, Address, BAccount>((PXGraph) this.graph, selectCommand, (IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, PXSyncItemStatus.Updated, this.Attachments).Select<PXSyncItemBucket<Contact, Address, BAccount>, PXSyncItemID>((Func<PXSyncItemBucket<Contact, Address, BAccount>, PXSyncItemID>) (bucket =>
        {
          PXExchangeItem<ContactItemType> pxExchangeItem = serverItemsList.FirstOrDefault<PXExchangeItem<ContactItemType>>((Func<PXExchangeItem<ContactItemType>, bool>) (_ => ((ItemType) _.Item).ItemId.Id == bucket.Reference.BinaryReference && ((ItemType) _.Item).ItemId.ChangeKey != bucket.Reference.BinaryChangeKey));
          return pxExchangeItem != null && ((ItemType) pxExchangeItem.Item).LastModifiedTime < PXTimeZoneInfo.ConvertTimeToUtc(bucket.Item1.LastModifiedDateTime.Value, LocaleInfo.GetTimeZone()) ? new PXSyncItemID((string) null, bucket.Reference.BinaryReference, new Guid?()) : new PXSyncItemID((string) null, (string) null, new Guid?());
        })).Where<PXSyncItemID>((Func<PXSyncItemID, bool>) (_ => _.ItemID != null)));
    }
    this.ImportFirst(policy, direction, mailboxes, excludes);
  }

  protected override void KeepBoth(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    PXSyncItemSet employeeOnServer = (PXSyncItemSet) null;
    if ((direction & 3) == 3)
    {
      List<PXExchangeItem<ContactItemType>> serverItemsList = this.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, (PXExchangeFindOptions) 256 /*0x0100*/).ToList<PXExchangeItem<ContactItemType>>();
      IEnumerable<PXSyncItemBucket<Contact, Address, BAccount>> source = this.SelectItems<Contact.noteID, Address, BAccount>((PXGraph) this.graph, this.GetSelectCommand(), (IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, PXSyncItemStatus.Updated, this.Attachments);
      PXCache addressCache = ((PXGraph) this.graph).Caches[typeof (Address)];
      PXCache contactCache = ((PXGraph) this.graph).Caches[typeof (Contact)];
      Action<PXSyncItemBucket<Contact, Address, BAccount>> action = (Action<PXSyncItemBucket<Contact, Address, BAccount>>) (bucket =>
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          Address copy = (Address) addressCache.CreateCopy((object) bucket.Item2);
          copy.AddressID = new int?();
          copy.NoteID = new Guid?();
          Address address = addressCache.Insert((object) copy) as Address;
          Contact contact = (Contact) contactCache.CreateCopy((object) bucket.Item1);
          contact.ContactID = new int?();
          contact.NoteID = new Guid?();
          contact.DefAddressID = address.AddressID;
          try
          {
            contact = contactCache.Insert((object) contact) as Contact;
            addressCache.Persist((PXDBOperation) 2);
            contactCache.Persist((PXDBOperation) 2);
            transactionScope.Complete();
          }
          catch (PXFieldProcessingException ex)
          {
            if (ex.FieldName.Equals(contactCache.GetField(typeof (Contact.bAccountID)), StringComparison.InvariantCultureIgnoreCase) && contact.ContactType == "EP" && contact.BAccountID.HasValue)
            {
              PXSyncItemID pxSyncItemId = new PXSyncItemID((string) null, bucket.Reference.BinaryReference, new Guid?());
              if (employeeOnServer == null)
                employeeOnServer = new PXSyncItemSet((IEnumerable<PXSyncItemID>) null);
              employeeOnServer.Add(pxSyncItemId);
            }
            else
              throw;
          }
        }
        addressCache.Clear();
        contactCache.Clear();
      });
      Func<PXSyncItemBucket<Contact, Address, BAccount>, bool> predicate = (Func<PXSyncItemBucket<Contact, Address, BAccount>, bool>) (bucket => serverItemsList.FirstOrDefault<PXExchangeItem<ContactItemType>>((Func<PXExchangeItem<ContactItemType>, bool>) (_ => ((ItemType) _.Item).ItemId.Id == bucket.Reference.BinaryReference && ((ItemType) _.Item).ItemId.ChangeKey != bucket.Reference.BinaryChangeKey)) != null);
      EnumerableExtensions.ForEach<PXSyncItemBucket<Contact, Address, BAccount>>(source.Where<PXSyncItemBucket<Contact, Address, BAccount>>(predicate), action);
    }
    this.ImportFirst(policy, direction, mailboxes, employeeOnServer);
  }

  protected override IEnumerable<PXSyncResult> ProcessSyncExport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet imported)
  {
    ExchangeContactsSyncCommand contactsSyncCommand1 = this;
    if (((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
    {
      PXCache cach = ((PXGraph) contactsSyncCommand1.graph).Caches[typeof (Contact)];
      BqlCommand cmd = contactsSyncCommand1.GetSelectCommand();
      foreach (PXExchangeResponce<ContactItemType> exchangeResponce in contactsSyncCommand1.ExportContactsInserted(contactsSyncCommand1.graph, contactsSyncCommand1.SelectItems<Contact.noteID, Address, BAccount>((PXGraph) contactsSyncCommand1.graph, cmd, (IEnumerable<PXSyncMailbox>) mailboxes, imported, PXSyncItemStatus.Inserted, contactsSyncCommand1.Attachments)))
      {
        ExchangeContactsSyncCommand contactsSyncCommand = contactsSyncCommand1;
        PXExchangeResponce<ContactItemType> item = exchangeResponce;
        PXSyncTag<Contact> tag = ((PXExchangeReBase<ContactItemType>) item).Tag as PXSyncTag<Contact>;
        yield return contactsSyncCommand1.SafeOperation<ContactItemType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.DisplayName, (string) null, PXSyncItemStatus.Inserted, (System.Action) (() => contactsSyncCommand.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((ItemType) ((PXExchangeItem<ContactItemType>) item).Item).ItemId, ((ItemType) ((PXExchangeItem<ContactItemType>) item).Item).ConversationId, (string) null, new bool?(true))));
      }
      foreach (PXExchangeResponce<ContactItemType> exchangeResponce in contactsSyncCommand1.ExportContactsUpdated(contactsSyncCommand1.graph, contactsSyncCommand1.SelectItems<Contact.noteID, Address, BAccount>((PXGraph) contactsSyncCommand1.graph, cmd, (IEnumerable<PXSyncMailbox>) mailboxes, imported, PXSyncItemStatus.Updated, contactsSyncCommand1.Attachments)))
      {
        ExchangeContactsSyncCommand contactsSyncCommand = contactsSyncCommand1;
        PXExchangeResponce<ContactItemType> item = exchangeResponce;
        PXSyncTag<Contact> tag = ((PXExchangeReBase<ContactItemType>) item).Tag as PXSyncTag<Contact>;
        yield return contactsSyncCommand1.SafeOperation<ContactItemType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.DisplayName, (string) null, PXSyncItemStatus.Updated, (System.Action) (() => contactsSyncCommand.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((ItemType) ((PXExchangeItem<ContactItemType>) item).Item).ItemId, ((ItemType) ((PXExchangeItem<ContactItemType>) item).Item).ConversationId, (string) null, new bool?(true))), (Func<bool>) (() =>
        {
          if (item.Success || item.Code != 241)
            return false;
          contactsSyncCommand.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(false));
          PXDatabase.Update<Contact>(new List<PXDataFieldParam>()
          {
            (PXDataFieldParam) new PXDataFieldAssign<Contact.synchronize>((object) false),
            (PXDataFieldParam) new PXDataFieldRestrict<Contact.contactID>((object) tag.Row.ContactID.Value)
          }.ToArray());
          return true;
        }));
      }
      cmd = contactsSyncCommand1.GetDeleteCommand();
      foreach (PXExchangeResponce<ContactItemType> exchangeResponce in contactsSyncCommand1.ExportContactsDeleted(contactsSyncCommand1.graph, contactsSyncCommand1.SelectItems<Contact.noteID, BAccount>((PXGraph) contactsSyncCommand1.graph, cmd, (IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, PXSyncItemStatus.Deleted, contactsSyncCommand1.Attachments)))
      {
        ExchangeContactsSyncCommand contactsSyncCommand = contactsSyncCommand1;
        PXExchangeResponce<ContactItemType> item = exchangeResponce;
        PXSyncTag<Contact> tag = ((PXExchangeReBase<ContactItemType>) item).Tag as PXSyncTag<Contact>;
        yield return contactsSyncCommand1.SafeOperation<ContactItemType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.DisplayName, (string) null, PXSyncItemStatus.Deleted, (System.Action) (() =>
        {
          if (tag.Row.DeletedDatabaseRecord.GetValueOrDefault())
          {
            contactsSyncCommand.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(true));
          }
          else
          {
            PXDatabase.Update<Contact>(new List<PXDataFieldParam>()
            {
              (PXDataFieldParam) new PXDataFieldAssign<Contact.synchronize>((object) false),
              (PXDataFieldParam) new PXDataFieldRestrict<Contact.contactID>((object) tag.Row.ContactID.Value)
            }.ToArray());
            contactsSyncCommand.DeleteReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID);
          }
        }));
      }
    }
  }

  protected override IEnumerable<PXSyncResult> ProcessSyncExportUnsynced(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported)
  {
    ExchangeContactsSyncCommand contactsSyncCommand1 = this;
    if (((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
    {
      PXCache cach = ((PXGraph) contactsSyncCommand1.graph).Caches[typeof (Contact)];
      BqlCommand selectCommand = contactsSyncCommand1.GetSelectCommand();
      foreach (PXExchangeResponce<ContactItemType> exchangeResponce in contactsSyncCommand1.ExportContactsUnsynced(contactsSyncCommand1.graph, contactsSyncCommand1.SelectItems<Contact.noteID, Address, BAccount>((PXGraph) contactsSyncCommand1.graph, selectCommand, (IEnumerable<PXSyncMailbox>) mailboxes, exported, PXSyncItemStatus.Unsynced, contactsSyncCommand1.Attachments)))
      {
        ExchangeContactsSyncCommand contactsSyncCommand = contactsSyncCommand1;
        PXExchangeResponce<ContactItemType> item = exchangeResponce;
        PXSyncTag<Contact> tag = ((PXExchangeReBase<ContactItemType>) item).Tag as PXSyncTag<Contact>;
        yield return contactsSyncCommand1.SafeOperation<ContactItemType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.DisplayName, (string) null, PXSyncItemStatus.Unsynced, (System.Action) (() => contactsSyncCommand.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((ItemType) ((PXExchangeItem<ContactItemType>) item).Item).ItemId, ((ItemType) ((PXExchangeItem<ContactItemType>) item).Item).ConversationId, (string) null, new bool?(true))), (Func<bool>) (() =>
        {
          if (item.Success || item.Code != 241)
            return false;
          contactsSyncCommand.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(false));
          PXDatabase.Update<Contact>(new List<PXDataFieldParam>()
          {
            (PXDataFieldParam) new PXDataFieldAssign<Contact.synchronize>((object) false),
            (PXDataFieldParam) new PXDataFieldRestrict<Contact.contactID>((object) tag.Row.ContactID.Value)
          }.ToArray());
          return true;
        }));
      }
    }
  }

  protected override IEnumerable<PXSyncResult> ProcessSyncImport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported)
  {
    ExchangeContactsSyncCommand contactsSyncCommand = this;
    if (((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
    {
      PXCache cache = ((PXGraph) contactsSyncCommand.graph).Caches[typeof (Contact)];
      IEnumerable<PXExchangeItem<ContactItemType>> items = contactsSyncCommand.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, exported, (PXExchangeFindOptions) 128 /*0x80*/, Tuple.Create<string, MapiPropertyTypeType>("0x3a45", (MapiPropertyTypeType) 25), Tuple.Create<string, MapiPropertyTypeType>("0x3a4d", (MapiPropertyTypeType) 21), Tuple.Create<string, MapiPropertyTypeType>("0x3e4a", (MapiPropertyTypeType) 25));
      int i = 0;
      foreach (PXExchangeItem<ContactItemType> pxExchangeItem in items)
      {
        if (i++ % 100 == 0)
        {
          contactsSyncCommand.graph = PXGraph.CreateInstance<ContactMaint>();
          cache = ((PXGraph) contactsSyncCommand.graph).Caches[typeof (Contact)];
          cache.AllowDelete = true;
          cache.AllowInsert = true;
          cache.AllowUpdate = true;
        }
        PXExchangeItem<ContactItemType> item = pxExchangeItem;
        PXSyncMailbox mailbox = ((IEnumerable<PXSyncMailbox>) mailboxes).FirstOrDefault<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => string.Equals(m.Address, ((PXExchangeItemBase) item).Address, StringComparison.InvariantCultureIgnoreCase)));
        bool merge = false;
        BqlCommand bqlCommand = PXSelectBase<Contact, PXSelectJoin<Contact, LeftJoin<EMailSyncReference, On<EMailSyncReference.noteID, Equal<Contact.noteID>, And<EMailSyncReference.serverID, Equal<Required<EMailSyncReference.serverID>>, And<EMailSyncReference.address, Equal<Required<EMailSyncReference.address>>>>>>>.Config>.GetCommand();
        Contact contact = (Contact) null;
        EMailSyncReference emailSyncReference = (EMailSyncReference) null;
        bool foundByNoteID = false;
        if (((ItemType) item.Item).ExtendedProperty != null && ((IEnumerable<ExtendedPropertyType>) ((ItemType) item.Item).ExtendedProperty).Any<ExtendedPropertyType>((Func<ExtendedPropertyType, bool>) (_ => _.ExtendedFieldURI.PropertyTag == "0x3e4a")))
        {
          using (new PXReadDeletedScope(false))
          {
            bqlCommand = bqlCommand.WhereNew<Where<Contact.noteID, Equal<Required<Contact.noteID>>, And<Contact.synchronize, Equal<True>>>>();
            PXResult<Contact, EMailSyncReference> pxResult = (PXResult<Contact, EMailSyncReference>) new PXView((PXGraph) contactsSyncCommand.graph, true, bqlCommand).SelectSingle(new object[3]
            {
              (object) contactsSyncCommand.Account.AccountID,
              (object) mailbox.Address,
              (object) new Guid(((IEnumerable<ExtendedPropertyType>) ((ItemType) item.Item).ExtendedProperty).First<ExtendedPropertyType>((Func<ExtendedPropertyType, bool>) (_ => _.ExtendedFieldURI.PropertyTag == "0x3e4a")).Item.ToString())
            });
            contact = PXResult<Contact, EMailSyncReference>.op_Implicit(pxResult);
            emailSyncReference = PXResult<Contact, EMailSyncReference>.op_Implicit(pxResult);
            if (contact != null)
              foundByNoteID = true;
          }
        }
        if (!foundByNoteID)
        {
          using (new PXReadDeletedScope(false))
          {
            bqlCommand = bqlCommand.WhereNew<Where<EMailSyncReference.binaryReference, Equal<Required<EMailSyncReference.binaryReference>>>>();
            PXResult<Contact, EMailSyncReference> pxResult = (PXResult<Contact, EMailSyncReference>) new PXView((PXGraph) contactsSyncCommand.graph, true, bqlCommand).SelectSingle(new object[3]
            {
              (object) contactsSyncCommand.Account.AccountID,
              (object) mailbox.Address,
              (object) ((ItemType) item.Item).ItemId.Id
            });
            contact = PXResult<Contact, EMailSyncReference>.op_Implicit(pxResult);
            emailSyncReference = PXResult<Contact, EMailSyncReference>.op_Implicit(pxResult);
          }
        }
        if (contact != null && contactsSyncCommand.Policy.ContactsFilter != null)
        {
          if ((emailSyncReference == null || !(emailSyncReference.BinaryChangeKey == ((ItemType) item.Item).ItemId.ChangeKey)) && !contact.DeletedDatabaseRecord.GetValueOrDefault())
          {
            if (contactsSyncCommand.Policy.ContactsFilter == "Owner")
              bqlCommand = bqlCommand.WhereAnd<Where<Contact.ownerID, Equal<Required<Contact.ownerID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>();
            else if (contactsSyncCommand.Policy.ContactsFilter == "Workgroup")
              bqlCommand = bqlCommand.WhereAnd<Where<Contact.workgroupID, IsWorkgroupOfContact<Required<Contact.contactID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>();
            Contact contact1 = (Contact) null;
            if (foundByNoteID)
            {
              using (new PXReadDeletedScope(false))
                contact1 = PXResult<Contact, EMailSyncReference>.op_Implicit((PXResult<Contact, EMailSyncReference>) new PXView((PXGraph) contactsSyncCommand.graph, true, bqlCommand).SelectSingle(new object[4]
                {
                  (object) contactsSyncCommand.Account.AccountID,
                  (object) mailbox.Address,
                  (object) new Guid(((IEnumerable<ExtendedPropertyType>) ((ItemType) item.Item).ExtendedProperty).First<ExtendedPropertyType>((Func<ExtendedPropertyType, bool>) (_ => _.ExtendedFieldURI.PropertyTag == "0x3e4a")).Item.ToString()),
                  (object) contactsSyncCommand.Cache.EmployeeCache[mailbox.EmployeeID]
                }));
            }
            else
            {
              using (new PXReadDeletedScope(false))
                contact1 = PXResult<Contact, EMailSyncReference>.op_Implicit((PXResult<Contact, EMailSyncReference>) new PXView((PXGraph) contactsSyncCommand.graph, true, bqlCommand).SelectSingle(new object[4]
                {
                  (object) contactsSyncCommand.Account.AccountID,
                  (object) mailbox.Address,
                  (object) ((ItemType) item.Item).ItemId.Id,
                  (object) contactsSyncCommand.Cache.EmployeeCache[mailbox.EmployeeID]
                }));
            }
            if (contact1 == null)
              contact = (Contact) null;
          }
          else
            continue;
        }
        else if (contact == null && contactsSyncCommand.Policy.ContactsMerge.GetValueOrDefault() && contactsSyncCommand.GetEmailByType(item.Item.EmailAddresses, (EmailAddressKeyType) 0) != null)
        {
          contact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.eMail, Equal<Required<Contact.eMail>>, And<Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>>.Config>.SelectSingleBound((PXGraph) contactsSyncCommand.graph, new object[0], new object[1]
          {
            (object) contactsSyncCommand.GetEmailByType(item.Item.EmailAddresses, (EmailAddressKeyType) 0)
          }));
          if (contact != null)
            merge = true;
        }
        Guid? noteid = contact == null ? new Guid?(Guid.Empty) : contact.NoteID;
        string key = !string.IsNullOrWhiteSpace(item.Item.DisplayName) ? item.Item.DisplayName : (!string.IsNullOrWhiteSpace(contactsSyncCommand.GetEmailByType(item.Item.EmailAddresses, (EmailAddressKeyType) 0)) ? contactsSyncCommand.GetEmailByType(item.Item.EmailAddresses, (EmailAddressKeyType) 0) : ((ItemType) item.Item).ItemId.Id);
        PXSyncItemStatus status = contact == null ? PXSyncItemStatus.Inserted : PXSyncItemStatus.Updated;
        yield return Tools.DoWith<PXSyncResult, PXSyncResult>(contactsSyncCommand.SafeOperation(mailbox.Address, ((ItemType) item.Item).ItemId.Id, noteid, key, (string) null, status, (System.Action) (() =>
        {
          if (!this.IsFullyFilled(item.Item))
            return;
          bool flag1 = false;
          if (contact == null)
          {
            contact = new Contact();
            contact = (Contact) cache.Insert((object) contact);
            if (!string.IsNullOrEmpty(this.Policy.ContactsClass))
              contact.ClassID = this.Policy.ContactsClass;
            flag1 = true;
          }
          else if (!contact.Synchronize.GetValueOrDefault() || contact.ContactType == "EP")
            return;
          bool? isActive = contact.IsActive;
          if ((isActive.HasValue ? new bool?(!isActive.GetValueOrDefault()) : new bool?()) ?? true)
          {
            contact.Synchronize = new bool?(false);
            contact = (Contact) cache.Update((object) contact);
            ((PXGraph) this.graph).Actions.PressSave();
          }
          else
          {
            contact.Synchronize = new bool?(true);
            noteid = contact.NoteID;
            int num1;
            if (cache.GetStatus((object) contact) == 2 && this.Cache.EmployeeCache.TryGetValue(mailbox.EmployeeID, out num1))
            {
              int? ownerId = contact.OwnerID;
              int num2 = num1;
              if (!(ownerId.GetValueOrDefault() == num2 & ownerId.HasValue))
              {
                contact.WorkgroupID = OwnerAttribute.DefaultWorkgroup((PXGraph) this.graph, new int?(num1));
                contact.OwnerID = new int?(num1);
              }
            }
            this.ImportContactSync(mailbox, contact, item.Item, merge);
            contact = (Contact) cache.Update((object) contact);
            bool flag2 = PXExchangeReflectionHelper.IsObjectModified<Contact>(cache, (object) contact, false, Array.Empty<string>());
            Address address1 = (Address) ((PXSelectBase) this.graph.AddressCurrent).Cache.Current ?? (Address) ((PXSelectBase) this.graph.AddressCurrent).View.SelectSingle(Array.Empty<object>());
            PhysicalAddressDictionaryEntryType dictionaryEntryType = PXExchangeConversionHelper.GetValueByType(item.Item.PhysicalAddresses, (PhysicalAddressKeyType) 0) ?? PXExchangeConversionHelper.GetValueByType(item.Item.PhysicalAddresses, (PhysicalAddressKeyType) 1);
            this.ImportAddressSync((PXSyncMailbox) null, address1, dictionaryEntryType, merge);
            Address address2 = (Address) ((PXSelectBase) this.graph.AddressCurrent).Cache.Update((object) address1);
            if (!flag2)
              flag2 = PXExchangeReflectionHelper.IsObjectModified<Contact>(((PXSelectBase) this.graph.AddressCurrent).Cache, (object) address2, false, Array.Empty<string>());
            if (((ItemType) item.Item).Body != null && PXNoteAttribute.GetNote(((PXGraph) this.graph).Caches[typeof (Contact)], (object) contact) != ((ItemType) item.Item).Body.Value)
            {
              flag2 = true;
              PXNoteAttribute.SetNote(((PXGraph) this.graph).Caches[typeof (Contact)], (object) contact, RichStyle.RemoveViewStyle(((ItemType) item.Item).Body.Value));
            }
            contact = (Contact) cache.Update((object) contact);
            ((PXGraph) this.graph).Actions.PressSave();
            string imgName;
            if (this.SaveAttachmentsSync((PXGraph) this.graph, contact, item.Attachments, out imgName))
            {
              flag2 = true;
              if (!string.IsNullOrEmpty(imgName))
              {
                contact.Img = imgName;
                contact = (Contact) cache.Update((object) contact);
              }
            }
            if (flag2 && ((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => m.ExportPreset.Date.HasValue)))
              PXTimeTagAttribute.UpdateTag<Contact.noteID>(cache, (object) contact, PXTimeZoneInfo.ConvertTimeToUtc(((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => m.ExportPreset.Date.HasValue)).Max<PXSyncMailbox, DateTime>((Func<PXSyncMailbox, DateTime>) (m => m.ExportPreset.Date.Value)), LocaleInfo.GetTimeZone()));
            ((PXGraph) this.graph).Actions.PressSave();
            this.SaveReference(mailbox.Address, contact.NoteID, ((ItemType) item.Item).ItemId, ((ItemType) item.Item).ConversationId, item.Hash, new bool?(!flag1 && foundByNoteID));
          }
        }), true), (Func<PXSyncResult, PXSyncResult>) (r => new PXSyncResult(r, note: noteid, status: new PXSyncItemStatus?(status))
        {
          Reprocess = merge
        }), (Func<PXSyncResult>) null);
        ((PXGraph) contactsSyncCommand.graph).Clear();
      }
    }
  }

  protected override BqlCommand GetSelectCommand()
  {
    BqlCommand selectCommand = PXSelectBase<Contact, PXSelectReadonly2<Contact, InnerJoin<Address, On<Contact.defAddressID, Equal<Address.addressID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>>, Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>, And<Where<Contact.synchronize, Equal<True>>>>>.Config>.GetCommand();
    if (this.Policy.ContactsFilter == "Owner")
      selectCommand = selectCommand.WhereAnd<Where<Contact.ownerID, Equal<Required<Contact.ownerID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>();
    if (this.Policy.ContactsFilter == "Workgroup")
      selectCommand = selectCommand.WhereAnd<Where<Contact.workgroupID, IsWorkgroupOfContact<Required<Contact.contactID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>();
    return selectCommand;
  }

  protected BqlCommand GetDeleteCommand()
  {
    BqlCommand deleteCommand = PXSelectBase<Contact, PXSelectReadonly2<Contact, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>, Where2<Where2<Where<Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>, And<Where<Contact.synchronize, Equal<True>, And<Contact.deletedDatabaseRecord, Equal<True>>>>>, Or<Where<Contact.isActive, Equal<False>, And<Contact.synchronize, Equal<True>, And<Contact.deletedDatabaseRecord, Equal<False>>>>>>>.Config>.GetCommand();
    if (this.Policy.ContactsFilter == "Owner")
      deleteCommand = deleteCommand.WhereAnd<Where<Contact.ownerID, Equal<Required<Contact.ownerID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>();
    if (this.Policy.ContactsFilter == "Workgroup")
      deleteCommand = deleteCommand.WhereAnd<Where<Contact.workgroupID, IsWorkgroupOfContact<Required<Contact.contactID>>, Or<Contact.contactType, Equal<ContactTypesAttribute.employee>>>>();
    return deleteCommand;
  }

  protected virtual IEnumerable<PXExchangeResponce<ContactItemType>> ExportContactsInserted(
    ContactMaint graph,
    IEnumerable<PXSyncItemBucket<Contact, Address, BAccount>> inserted)
  {
    Func<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemType>> selector = (Func<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemType>>) (bucket =>
    {
      PXSyncMailbox mailbox = bucket.Mailbox;
      EMailSyncReference reference = bucket.Reference;
      Contact contact = bucket.Item1;
      Address address = bucket.Item2;
      BAccount baccount = bucket.Item3;
      ContactItemType contactItemType = new ContactItemType();
      ParameterExpression parameterExpression1;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_GivenName))), parameterExpression1), contactItemType, (object) contact.FirstName);
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_Surname))), parameterExpression2), contactItemType, (object) contact.LastName);
      ParameterExpression parameterExpression3;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_MiddleName))), parameterExpression3), contactItemType, (object) contact.MidName);
      ParameterExpression parameterExpression4;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_DisplayName))), parameterExpression4), contactItemType, (object) contact.DisplayName);
      ParameterExpression parameterExpression5;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Subject))), parameterExpression5), contactItemType, (object) contact.DisplayName);
      ParameterExpression parameterExpression6;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_BusinessHomePage))), parameterExpression6), contactItemType, (object) this.GenerateLink(contact));
      ParameterExpression parameterExpression7;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_Birthday))), typeof (object)), parameterExpression7), contactItemType, (object) contact.DateOfBirth, mailbox.ExchangeTimeZone);
      ParameterExpression parameterExpression8;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_SpouseName))), parameterExpression8), contactItemType, (object) contact.Spouse);
      ParameterExpression parameterExpression9;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression9, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_JobTitle))), parameterExpression9), contactItemType, (object) contact.Salutation);
      ParameterExpression parameterExpression10;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression10, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_CompanyName))), parameterExpression10), contactItemType, baccount == null || baccount.AcctName == null ? (object) contact.FullName : (object) baccount.AcctName);
      ParameterExpression parameterExpression11;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression11, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression11), contactItemType, (object) PXExchangeConversionHelper.GetExtendedProperties(Tuple.Create<string, MapiPropertyTypeType, object>("0x3a45", (MapiPropertyTypeType) 25, (object) contact.Title), Tuple.Create<string, MapiPropertyTypeType, object>("0x3a4d", (MapiPropertyTypeType) 21, PXExchangeConversionHelper.ParceAcGender(contact.Gender)), Tuple.Create<string, MapiPropertyTypeType, object>("0x3e4a", (MapiPropertyTypeType) 25, (object) contact.NoteID.ToString())));
      ParameterExpression parameterExpression12;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression12, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Categories))), parameterExpression12), contactItemType, (object) new string[1]
      {
        this.Policy.Category
      });
      ParameterExpression parameterExpression13;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression13, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_FileAsMapping))), typeof (object)), parameterExpression13), contactItemType, (object) (FileAsMappingType) (string.IsNullOrEmpty(contactItemType.CompanyName) ? 1 : 4));
      ParameterExpression parameterExpression14;
      // ISSUE: method reference
      this.ExportInsertedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression14, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Body))), parameterExpression14), contactItemType, (object) new BodyType()
      {
        BodyType1 = (BodyTypeType) 1,
        Value = (PXNoteAttribute.GetNote(((PXGraph) graph).Caches[typeof (Contact)], (object) contact) ?? string.Empty)
      });
      ParameterExpression parameterExpression15;
      // ISSUE: method reference
      this.ExportInsertedItemPropertyConditional<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression15, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_EmailAddresses))), parameterExpression15), contactItemType, (object) new EmailAddressDictionaryEntryType[1]
      {
        new EmailAddressDictionaryEntryType()
        {
          Key = (EmailAddressKeyType) 0,
          Value = contact.EMail
        }
      }, (object) contact.EMail);
      contactItemType.PhoneNumbers = new PhoneNumberDictionaryEntryType[4]
      {
        PXExchangeConversionHelper.ParcePhone(contact.FaxType, contact.Fax),
        PXExchangeConversionHelper.ParcePhone(contact.Phone1Type, contact.Phone1),
        PXExchangeConversionHelper.ParcePhone(contact.Phone2Type, contact.Phone2),
        PXExchangeConversionHelper.ParcePhone(contact.Phone3Type, contact.Phone3)
      };
      string empty = string.Empty;
      string[] strArray = new string[3]
      {
        address.AddressLine1,
        address.AddressLine2,
        address.AddressLine3
      };
      foreach (string str1 in strArray)
      {
        string str2 = (str1 ?? string.Empty).Trim(' ', '\n', '\r');
        if (!string.IsNullOrEmpty(str2.Trim()))
          empty += empty.Length > 0 ? Environment.NewLine + str2 : str2;
      }
      contactItemType.PhysicalAddresses = new PhysicalAddressDictionaryEntryType[1]
      {
        new PhysicalAddressDictionaryEntryType()
        {
          Key = (PhysicalAddressKeyType) 0,
          City = address.City,
          CountryOrRegion = address.CountryID,
          PostalCode = address.PostalCode,
          State = address.State,
          Street = empty
        }
      };
      string photoName = bucket.Attachments == null ? (string) null : ((IEnumerable<UploadFileWithData>) bucket.Attachments).FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (a => a.Name == contact.Img)).With<UploadFileWithData, string>((Func<UploadFileWithData, string>) (a => a.Name)) ?? ((IEnumerable<UploadFileWithData>) bucket.Attachments).FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (a => ((IEnumerable<string>) SitePolicy.AllowedImageTypesExt).Any<string>((Func<string, bool>) (i => a.Name.EndsWith(i))))).With<UploadFileWithData, string>((Func<UploadFileWithData, string>) (a => a.Name));
      return new PXExchangeRequest<ContactItemType, ItemType>((PXExchangeFolderID) mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport)), (ItemType) contactItemType, bucket.ID, (object) new PXSyncTag<Contact>(contact, mailbox, reference), this.ConvertAttachment(bucket.Attachments, photoName));
    });
    return this.Gate.CreateItems<ContactItemType>(inserted.Select<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemType>>(selector));
  }

  protected virtual IEnumerable<PXExchangeResponce<ContactItemType>> ExportContactsUpdated(
    ContactMaint graph,
    IEnumerable<PXSyncItemBucket<Contact, Address, BAccount>> updated)
  {
    Func<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemChangeType>> selector = (Func<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemChangeType>>) (bucket =>
    {
      PXSyncMailbox mailbox = bucket.Mailbox;
      EMailSyncReference reference = bucket.Reference;
      Contact contact = bucket.Item1;
      Address address = bucket.Item2;
      BAccount baccount = bucket.Item3;
      List<ItemChangeDescriptionType> changeDescriptionTypeList = new List<ItemChangeDescriptionType>();
      ParameterExpression parameterExpression1;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_GivenName))), parameterExpression1), (UnindexedFieldURIType) 191, (object) contact.FirstName));
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_Surname))), parameterExpression2), (UnindexedFieldURIType) 212, (object) contact.LastName));
      ParameterExpression parameterExpression3;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_MiddleName))), parameterExpression3), (UnindexedFieldURIType) 197, (object) contact.MidName));
      ParameterExpression parameterExpression4;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_JobTitle))), parameterExpression4), (UnindexedFieldURIType) 194, (object) contact.Salutation));
      ParameterExpression parameterExpression5;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_DisplayName))), parameterExpression5), (UnindexedFieldURIType) 184, (object) contact.DisplayName));
      ParameterExpression parameterExpression6;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Subject))), parameterExpression6), (UnindexedFieldURIType) 20, (object) contact.DisplayName));
      ParameterExpression parameterExpression7;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_BusinessHomePage))), parameterExpression7), (UnindexedFieldURIType) 176 /*0xB0*/, (object) this.GenerateLink(contact)));
      ParameterExpression parameterExpression8;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression8, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_Birthday))), typeof (object)), parameterExpression8), (UnindexedFieldURIType) 175, (object) contact.DateOfBirth, mailbox.ExchangeTimeZone));
      ParameterExpression parameterExpression9;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression9, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_SpouseName))), parameterExpression9), (UnindexedFieldURIType) 211, (object) contact.Spouse));
      ParameterExpression parameterExpression10;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression10, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_CompanyName))), parameterExpression10), (UnindexedFieldURIType) 179, baccount == null || baccount.AcctName == null ? (object) contact.FullName : (object) baccount.AcctName));
      ParameterExpression parameterExpression11;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression11, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Body))), parameterExpression11), (UnindexedFieldURIType) 36, (object) new BodyType()
      {
        BodyType1 = (BodyTypeType) 1,
        Value = (PXNoteAttribute.GetNote(((PXGraph) graph).Caches[typeof (Contact)], (object) contact) ?? string.Empty)
      }));
      ParameterExpression parameterExpression12;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression12, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression12), "0x3a45", (MapiPropertyTypeType) 25, (object) contact.Title));
      ParameterExpression parameterExpression13;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression13, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression13), "0x3a4d", (MapiPropertyTypeType) 21, PXExchangeConversionHelper.ParceAcGender(contact.Gender)));
      ParameterExpression parameterExpression14;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression14, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression14), "0x3e4a", (MapiPropertyTypeType) 25, (object) contact.NoteID.ToString()));
      ParameterExpression parameterExpression15;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression15, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_EmailAddresses))), parameterExpression15), (DictionaryURIType) 8, "EmailAddress1", (object) new EmailAddressDictionaryEntryType[1]
      {
        new EmailAddressDictionaryEntryType()
        {
          Key = (EmailAddressKeyType) 0,
          Value = contact.EMail
        }
      }, (object) contact.EMail));
      PhoneNumberDictionaryEntryType dictionaryEntryType1 = PXExchangeConversionHelper.ParcePhone(contact.FaxType, contact.Fax);
      ParameterExpression parameterExpression16;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression16, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhoneNumbers))), parameterExpression16), (DictionaryURIType) 7, dictionaryEntryType1.Key.ToString(), (object) new PhoneNumberDictionaryEntryType[1]
      {
        dictionaryEntryType1
      }, (object) contact.Fax));
      PhoneNumberDictionaryEntryType dictionaryEntryType2 = PXExchangeConversionHelper.ParcePhone(contact.Phone1Type, contact.Phone1);
      ParameterExpression parameterExpression17;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression17, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhoneNumbers))), parameterExpression17), (DictionaryURIType) 7, dictionaryEntryType2.Key.ToString(), (object) new PhoneNumberDictionaryEntryType[1]
      {
        dictionaryEntryType2
      }, (object) contact.Phone1));
      PhoneNumberDictionaryEntryType dictionaryEntryType3 = PXExchangeConversionHelper.ParcePhone(contact.Phone2Type, contact.Phone2);
      ParameterExpression parameterExpression18;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression18, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhoneNumbers))), parameterExpression18), (DictionaryURIType) 7, dictionaryEntryType3.Key.ToString(), (object) new PhoneNumberDictionaryEntryType[1]
      {
        dictionaryEntryType3
      }, (object) contact.Phone2));
      PhoneNumberDictionaryEntryType dictionaryEntryType4 = PXExchangeConversionHelper.ParcePhone(contact.Phone3Type, contact.Phone3);
      ParameterExpression parameterExpression19;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression19, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhoneNumbers))), parameterExpression19), (DictionaryURIType) 7, dictionaryEntryType4.Key.ToString(), (object) new PhoneNumberDictionaryEntryType[1]
      {
        dictionaryEntryType4
      }, (object) contact.Phone3));
      foreach (PhoneNumberDictionaryEntryType purgePhone in PXExchangeConversionHelper.PurgePhones(dictionaryEntryType1, dictionaryEntryType2, dictionaryEntryType3, dictionaryEntryType4))
      {
        ParameterExpression parameterExpression20;
        // ISSUE: method reference
        Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.DeleteItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression20, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhoneNumbers))), parameterExpression20), (DictionaryURIType) 7, purgePhone.Key.ToString()));
      }
      ParameterExpression parameterExpression21;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression21, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhysicalAddresses))), parameterExpression21), (DictionaryURIType) 3, "Business", (object) new PhysicalAddressDictionaryEntryType[1]
      {
        new PhysicalAddressDictionaryEntryType()
        {
          Key = (PhysicalAddressKeyType) 0,
          City = address.City
        }
      }, (object) address.City));
      ParameterExpression parameterExpression22;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression22, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhysicalAddresses))), parameterExpression22), (DictionaryURIType) 5, "Business", (object) new PhysicalAddressDictionaryEntryType[1]
      {
        new PhysicalAddressDictionaryEntryType()
        {
          Key = (PhysicalAddressKeyType) 0,
          CountryOrRegion = address.CountryID
        }
      }, (object) address.CountryID));
      ParameterExpression parameterExpression23;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression23, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhysicalAddresses))), parameterExpression23), (DictionaryURIType) 6, "Business", (object) new PhysicalAddressDictionaryEntryType[1]
      {
        new PhysicalAddressDictionaryEntryType()
        {
          Key = (PhysicalAddressKeyType) 0,
          PostalCode = address.PostalCode
        }
      }, (object) address.PostalCode));
      ParameterExpression parameterExpression24;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression24, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhysicalAddresses))), parameterExpression24), (DictionaryURIType) 4, "Business", (object) new PhysicalAddressDictionaryEntryType[1]
      {
        new PhysicalAddressDictionaryEntryType()
        {
          Key = (PhysicalAddressKeyType) 0,
          State = address.State
        }
      }, (object) address.State));
      string empty = string.Empty;
      string[] strArray = new string[3]
      {
        address.AddressLine1,
        address.AddressLine2,
        address.AddressLine3
      };
      foreach (string str1 in strArray)
      {
        string str2 = (str1 ?? string.Empty).Trim(' ', '\n', '\r');
        if (!string.IsNullOrEmpty(str2.Trim()))
          empty += empty.Length > 0 ? Environment.NewLine + str2 : str2;
      }
      ParameterExpression parameterExpression25;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression25, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_PhysicalAddresses))), parameterExpression25), (DictionaryURIType) 2, "Business", (object) new PhysicalAddressDictionaryEntryType[1]
      {
        new PhysicalAddressDictionaryEntryType()
        {
          Key = (PhysicalAddressKeyType) 0,
          Street = empty
        }
      }, (object) empty));
      string photoName = bucket.Attachments == null ? (string) null : ((IEnumerable<UploadFileWithData>) bucket.Attachments).FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (a => a.Name == contact.Img)).With<UploadFileWithData, string>((Func<UploadFileWithData, string>) (a => a.Name)) ?? ((IEnumerable<UploadFileWithData>) bucket.Attachments).FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (a => ((IEnumerable<string>) SitePolicy.AllowedImageTypesExt).Any<string>((Func<string, bool>) (i => a.Name.EndsWith(i))))).With<UploadFileWithData, string>((Func<UploadFileWithData, string>) (a => a.Name));
      ItemIdType itemIdType = new ItemIdType()
      {
        Id = reference.BinaryReference
      };
      PXSyncDirectFolder syncDirectFolder = mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport));
      ItemChangeType itemChangeType = new ItemChangeType();
      itemChangeType.Item = (BaseItemIdType) itemIdType;
      itemChangeType.Updates = changeDescriptionTypeList.ToArray();
      string id = bucket.ID;
      PXSyncTag<Contact> pxSyncTag = new PXSyncTag<Contact>(contact, mailbox, reference);
      AttachmentType[] attachmentTypeArray = this.ConvertAttachment(bucket.Attachments, photoName);
      return new PXExchangeRequest<ContactItemType, ItemChangeType>((PXExchangeFolderID) syncDirectFolder, itemChangeType, id, (object) pxSyncTag, attachmentTypeArray);
    });
    return this.Gate.UpdateItems<ContactItemType>(updated.Select<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemChangeType>>(selector));
  }

  protected virtual IEnumerable<PXExchangeResponce<ContactItemType>> ExportContactsUnsynced(
    ContactMaint graph,
    IEnumerable<PXSyncItemBucket<Contact, Address, BAccount>> updated)
  {
    Func<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemChangeType>> selector = (Func<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemChangeType>>) (bucket =>
    {
      PXSyncMailbox mailbox = bucket.Mailbox;
      EMailSyncReference reference = bucket.Reference;
      Contact contact = bucket.Item1;
      List<ItemChangeDescriptionType> changeDescriptionTypeList = new List<ItemChangeDescriptionType>();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<ContactItemType>(Expression.Lambda<Func<ContactItemType, object>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression), "0x3e4a", (MapiPropertyTypeType) 25, (object) contact.NoteID.ToString()));
      string photoName = bucket.Attachments == null ? (string) null : ((IEnumerable<UploadFileWithData>) bucket.Attachments).FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (a => a.Name == contact.Img)).With<UploadFileWithData, string>((Func<UploadFileWithData, string>) (a => a.Name)) ?? ((IEnumerable<UploadFileWithData>) bucket.Attachments).FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (a => ((IEnumerable<string>) SitePolicy.AllowedImageTypesExt).Any<string>((Func<string, bool>) (i => a.Name.EndsWith(i))))).With<UploadFileWithData, string>((Func<UploadFileWithData, string>) (a => a.Name));
      ItemIdType itemIdType = new ItemIdType()
      {
        Id = reference.BinaryReference
      };
      PXSyncDirectFolder syncDirectFolder = mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport));
      ItemChangeType itemChangeType = new ItemChangeType();
      itemChangeType.Item = (BaseItemIdType) itemIdType;
      itemChangeType.Updates = changeDescriptionTypeList.ToArray();
      string id = bucket.ID;
      PXSyncTag<Contact> pxSyncTag = new PXSyncTag<Contact>(contact, mailbox, reference);
      AttachmentType[] attachmentTypeArray = this.ConvertAttachment(bucket.Attachments, photoName);
      return new PXExchangeRequest<ContactItemType, ItemChangeType>((PXExchangeFolderID) syncDirectFolder, itemChangeType, id, (object) pxSyncTag, attachmentTypeArray);
    });
    return this.Gate.UpdateItems<ContactItemType>(updated.Select<PXSyncItemBucket<Contact, Address, BAccount>, PXExchangeRequest<ContactItemType, ItemChangeType>>(selector));
  }

  protected virtual IEnumerable<PXExchangeResponce<ContactItemType>> ExportContactsDeleted(
    ContactMaint graph,
    IEnumerable<PXSyncItemBucket<Contact, BAccount>> deleted)
  {
    Func<PXSyncItemBucket<Contact, BAccount>, PXExchangeRequest<ContactItemType, ItemType>> selector = (Func<PXSyncItemBucket<Contact, BAccount>, PXExchangeRequest<ContactItemType, ItemType>>) (bucket =>
    {
      string str = Guid.NewGuid().ToString();
      PXSyncMailbox mailbox = bucket.Mailbox;
      Contact row = bucket.Item1;
      EMailSyncReference reference = bucket.Reference;
      ContactItemType contactItemType = new ContactItemType();
      ((ItemType) contactItemType).ItemId = new ItemIdType()
      {
        Id = reference.BinaryReference
      };
      return new PXExchangeRequest<ContactItemType, ItemType>((PXExchangeFolderID) mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport)), (ItemType) contactItemType, str, (object) new PXSyncTag<Contact>(row, mailbox, bucket.Reference), (AttachmentType[]) null);
    });
    return this.Gate.DeleteItems<ContactItemType>(deleted.Select<PXSyncItemBucket<Contact, BAccount>, PXExchangeRequest<ContactItemType, ItemType>>(selector));
  }

  protected virtual string GenerateLink(Contact row)
  {
    if (!this.Policy.ContactsGenerateLink.GetValueOrDefault())
      return row.WebSite;
    PXCache cach = ((PXGraph) this.graph).Caches[typeof (Contact)];
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (ContactMaint));
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append((this.Policy.LinkTemplate ?? string.Empty).TrimEnd('/'));
    stringBuilder.Append(PXUrl.ToAbsoluteUrl(siteMapNode.Url));
    stringBuilder.Append(stringBuilder.ToString().Contains("?") ? "&" : "?");
    string companyName = PXAccess.GetCompanyName();
    if (!string.IsNullOrEmpty(companyName))
    {
      stringBuilder.Append("CompanyID=" + HttpUtility.UrlEncode(companyName));
      stringBuilder.Append("&");
    }
    stringBuilder.Append("ScreenId=" + siteMapNode.ScreenID);
    foreach (string key in (IEnumerable<string>) cach.Keys)
    {
      string str1 = key;
      object obj = cach.GetValue((object) row, key);
      if (obj != null)
      {
        string str2 = obj.ToString();
        if (str2.Contains<char>('\\'))
          str2 = str2.Replace("\\", "%5C");
        stringBuilder.Append("&");
        stringBuilder.Append($"{str1}={HttpUtility.UrlEncode(str2)}");
      }
    }
    return stringBuilder.ToString();
  }

  protected virtual void ImportContactSync(
    PXSyncMailbox account,
    Contact contact,
    ContactItemType item,
    bool merge)
  {
    string str1 = !string.IsNullOrWhiteSpace(item.CompleteName.LastName) ? item.CompleteName.LastName : item.CompleteName.FullName;
    if (!merge)
    {
      if (!string.IsNullOrEmpty(str1))
        contact.LastName = str1;
      if (!string.IsNullOrEmpty(item.CompleteName.FirstName))
        contact.FirstName = item.CompleteName.FirstName;
      if (!string.IsNullOrEmpty(item.CompleteName.MiddleName))
        contact.MidName = item.CompleteName.MiddleName;
    }
    if (!string.IsNullOrEmpty(item.CompleteName.Title) || !merge)
      contact.Title = item.CompleteName.Title;
    if (!string.IsNullOrEmpty(item.JobTitle) || !merge)
      contact.Salutation = item.JobTitle;
    if (!contact.BAccountID.HasValue && !string.IsNullOrEmpty(item.CompanyName))
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      this.ImportItemProperty<ContactItemType, string>(Expression.Lambda<Func<ContactItemType, string>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_CompanyName))), parameterExpression), item, (Action<string>) (v => contact.FullName = v), merge);
    }
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.ImportItemProperty<ContactItemType, DateTime>(Expression.Lambda<Func<ContactItemType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_Birthday))), parameterExpression1), item, (Action<DateTime>) (v => contact.DateOfBirth = new DateTime?(v)), merge, account.ExchangeTimeZone);
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    this.ImportItemProperty<ContactItemType, string>(Expression.Lambda<Func<ContactItemType, string>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_SpouseName))), parameterExpression2), item, (Action<string>) (v => contact.Spouse = v), merge);
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    this.ImportItemProperty<ContactItemType, EmailAddressDictionaryEntryType[]>(Expression.Lambda<Func<ContactItemType, EmailAddressDictionaryEntryType[]>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_EmailAddresses))), parameterExpression3), item, (Action<EmailAddressDictionaryEntryType[]>) (v => contact.EMail = this.GetEmailByType(v, (EmailAddressKeyType) 0)), merge);
    if (!this.Policy.ContactsGenerateLink.GetValueOrDefault() || ((PXGraph) this.graph).Caches[typeof (Contact)].GetStatus((object) contact) == 2)
    {
      ParameterExpression parameterExpression4;
      // ISSUE: method reference
      this.ImportItemProperty<ContactItemType, string>(Expression.Lambda<Func<ContactItemType, string>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContactItemType.get_BusinessHomePage))), parameterExpression4), item, (Action<string>) (v => contact.WebSite = v), merge);
    }
    if (((ItemType) item).ExtendedProperty != null)
    {
      foreach (ExtendedPropertyType extendedPropertyType in ((ItemType) item).ExtendedProperty)
      {
        switch (extendedPropertyType.ExtendedFieldURI.PropertyTag)
        {
          case "0x3a4d":
            contact.Gender = PXExchangeConversionHelper.ParceExGender(extendedPropertyType.Item);
            break;
          case "0x3a45":
            contact.Title = extendedPropertyType.Item as string;
            break;
        }
      }
    }
    if (item.PhoneNumbers == null)
      return;
    short num = 1;
    foreach (PhoneNumberDictionaryEntryType phoneNumber in item.PhoneNumbers)
    {
      string type;
      string str2;
      if (PXExchangeConversionHelper.ParcePhone(phoneNumber, out type, out str2) && !(str2 == null & merge))
      {
        if (phoneNumber.Key == 1 || phoneNumber.Key == 12 || phoneNumber.Key == 7)
        {
          contact.FaxType = type;
          contact.Fax = str2;
        }
        else
        {
          switch (num)
          {
            case 1:
              contact.Phone1Type = type;
              contact.Phone1 = str2;
              break;
            case 2:
              contact.Phone2Type = type;
              contact.Phone2 = str2;
              break;
            case 3:
              contact.Phone3Type = type;
              contact.Phone3 = str2;
              break;
          }
          ++num;
        }
      }
    }
  }

  protected virtual void ImportAddressSync(
    PXSyncMailbox account,
    Address address,
    PhysicalAddressDictionaryEntryType item,
    bool merge)
  {
    if (item != null)
    {
      if (!string.IsNullOrEmpty(item.City) || !merge)
        address.City = item.City;
      if (!string.IsNullOrEmpty(item.Street) || !merge)
      {
        address.AddressLine1 = (string) null;
        address.AddressLine2 = (string) null;
        address.AddressLine3 = (string) null;
        if (!string.IsNullOrEmpty(item.Street))
        {
          string[] source = item.Street.Split(new string[3]
          {
            Environment.NewLine,
            "\r",
            "\n"
          }, StringSplitOptions.RemoveEmptyEntries);
          if (source.Length != 0)
          {
            address.AddressLine1 = source[0];
            address.AddressLine2 = source.Length != 2 ? string.Join(" ", ((IEnumerable<string>) source).Skip<string>(1).ToArray<string>()) : source[1];
          }
        }
      }
      if (!string.IsNullOrEmpty(item.PostalCode))
        address.PostalCode = item.PostalCode;
      if (!string.IsNullOrEmpty(item.CountryOrRegion))
        address.CountryID = item.CountryOrRegion;
      if (!string.IsNullOrEmpty(item.State))
        address.State = item.State;
    }
    if (address.CountryID != null)
      return;
    Address address1 = PXResultset<Address>.op_Implicit(PXSelectBase<Address, PXSelectJoin<Address, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.defAddressID, Equal<Address.addressID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccount.bAccountID>, And<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>>>.Config>.SelectSingleBound((PXGraph) this.graph, (object[]) null, (object[]) null));
    address.CountryID = address1.CountryID;
  }

  protected virtual string GetEmailByType(
    EmailAddressDictionaryEntryType[] types,
    EmailAddressKeyType key,
    bool checkEmpty = false)
  {
    if (types != null)
    {
      foreach (EmailAddressDictionaryEntryType type in types)
      {
        if (type.Key == key)
          return type.Value == null || type.Value.Contains("@") || !type.Value.Contains("/") && !type.Value.Contains("=") ? type.Value : this.Gate.ResolveName(type.Value);
      }
    }
    if (checkEmpty)
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("Email with type {0} has not found.", new object[1]
      {
        (object) key.ToString()
      }));
    return (string) null;
  }

  protected override bool IsFullyFilled(ContactItemType item)
  {
    return item.CompleteName != null && item.CompleteName != null;
  }
}
