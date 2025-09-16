// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.ExchangeActivitySyncCommand`6
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
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CS.Email;

public abstract class ExchangeActivitySyncCommand<TGraph, TExchangeType, TActivity, TActivitySyncronize, TActivityNoteID, TActivityOwner>(
  MicrosoftExchangeSyncProvider provider,
  string operationCode,
  PXExchangeFindOptions findOption) : ExchangeBaseLogicSyncCommand<TGraph, TActivity, TExchangeType>(provider, operationCode, (PXExchangeFindOptions) (findOption | 64 /*0x40*/))
  where TGraph : PXGraph, new()
  where TExchangeType : ItemType, new()
  where TActivity : CRActivity, new()
  where TActivitySyncronize : class, IBqlField
  where TActivityNoteID : class, IBqlField
  where TActivityOwner : class, IBqlField
{
  protected bool ExportInserted = true;
  protected bool ExportUpdated = true;
  protected bool ExportDeleted = true;
  protected bool ExportReowned;

  protected override void ExportImportFirst(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    List<PXExchangeItem<TExchangeType>> serverItemsList = this.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, (PXExchangeFindOptions) 256 /*0x0100*/).ToList<PXExchangeItem<TExchangeType>>();
    BqlCommand selectCommand = this.GetSelectCommand();
    bool flag;
    using (new PXReadDeletedScope(false))
      flag = this.SelectItems<TActivityNoteID>((PXGraph) this.graph, selectCommand, (IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, PXSyncItemStatus.Updated, this.Attachments).Where<PXSyncItemBucket<TActivity>>((Func<PXSyncItemBucket<TActivity>, bool>) (bucket =>
      {
        PXExchangeItem<TExchangeType> pxExchangeItem = serverItemsList.FirstOrDefault<PXExchangeItem<TExchangeType>>((Func<PXExchangeItem<TExchangeType>, bool>) (_ => _.Item.ItemId.Id == bucket.Reference.BinaryReference));
        if (pxExchangeItem == null)
          return false;
        return pxExchangeItem.Item.ItemId.ChangeKey != bucket.Reference.BinaryChangeKey || bucket.Item1.DeletedDatabaseRecord.GetValueOrDefault();
      })).Any<PXSyncItemBucket<TActivity>>();
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
      List<PXExchangeItem<TExchangeType>> serverItemsList = this.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, (PXExchangeFindOptions) 256 /*0x0100*/).ToList<PXExchangeItem<TExchangeType>>();
      BqlCommand selectCommand = this.GetSelectCommand();
      using (new PXReadDeletedScope(false))
        excludes = new PXSyncItemSet(this.SelectItems<TActivityNoteID>((PXGraph) this.graph, selectCommand, (IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, PXSyncItemStatus.Updated, this.Attachments).Select<PXSyncItemBucket<TActivity>, PXSyncItemID>((Func<PXSyncItemBucket<TActivity>, PXSyncItemID>) (bucket =>
        {
          PXExchangeItem<TExchangeType> pxExchangeItem = serverItemsList.FirstOrDefault<PXExchangeItem<TExchangeType>>((Func<PXExchangeItem<TExchangeType>, bool>) (_ => _.Item.ItemId.Id == bucket.Reference.BinaryReference && _.Item.ItemId.ChangeKey != bucket.Reference.BinaryChangeKey));
          return pxExchangeItem != null && pxExchangeItem.Item.LastModifiedTime < PXTimeZoneInfo.ConvertTimeToUtc(bucket.Item1.LastModifiedDateTime.Value, LocaleInfo.GetTimeZone()) ? new PXSyncItemID((string) null, bucket.Reference.BinaryReference, new Guid?()) : new PXSyncItemID((string) null, (string) null, new Guid?());
        })).Where<PXSyncItemID>((Func<PXSyncItemID, bool>) (_ => _.ItemID != null)));
    }
    this.ImportFirst(policy, direction, mailboxes, excludes);
  }

  protected override void KeepBoth(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    if ((direction & 2) == 2)
    {
      List<PXExchangeItem<TExchangeType>> serverItemsList = this.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, (PXExchangeFindOptions) 256 /*0x0100*/).ToList<PXExchangeItem<TExchangeType>>();
      IEnumerable<PXSyncItemBucket<TActivity>> source = this.SelectItems<TActivityNoteID>((PXGraph) this.graph, this.GetSelectCommand(), (IEnumerable<PXSyncMailbox>) mailboxes, (PXSyncItemSet) null, PXSyncItemStatus.Updated, this.Attachments);
      this.graph.IsExport = true;
      PXCache cache = this.graph.Caches[typeof (TActivity)];
      PXCache remCache = this.graph.Caches[typeof (CRReminder)];
      Action<PXSyncItemBucket<TActivity>> action = (Action<PXSyncItemBucket<TActivity>>) (bucket =>
      {
        TActivity activity1 = bucket.Item1;
        TActivity copy1 = (TActivity) cache.CreateCopy((object) activity1);
        copy1.NoteID = new Guid?();
        TActivity activity2 = (TActivity) cache.Insert((object) copy1);
        cache.Update((object) activity2);
        cache.Persist((PXDBOperation) 2);
        cache.Clear();
        remCache.Locate((IDictionary) new Dictionary<string, object>()
        {
          ["RefNoteID"] = (object) activity1.NoteID
        });
        CRReminder current = (CRReminder) remCache.Current;
        CRReminder copy2 = (CRReminder) remCache.CreateCopy((object) current);
        copy2.RefNoteID = activity2.NoteID;
        copy2.ReminderDate = current.ReminderDate;
        remCache.Insert((object) copy2);
        remCache.Persist((PXDBOperation) 2);
        remCache.Clear();
      });
      Func<PXSyncItemBucket<TActivity>, bool> predicate = (Func<PXSyncItemBucket<TActivity>, bool>) (bucket => serverItemsList.FirstOrDefault<PXExchangeItem<TExchangeType>>((Func<PXExchangeItem<TExchangeType>, bool>) (_ => _.Item.ItemId.Id == bucket.Reference.BinaryReference && _.Item.ItemId.ChangeKey != bucket.Reference.BinaryChangeKey)) != null);
      EnumerableExtensions.ForEach<PXSyncItemBucket<TActivity>>(source.Where<PXSyncItemBucket<TActivity>>(predicate), action);
    }
    this.ImportFirst(policy, direction, mailboxes);
  }

  protected override IEnumerable<PXSyncResult> ProcessSyncExport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet imported)
  {
    ExchangeActivitySyncCommand<TGraph, TExchangeType, TActivity, TActivitySyncronize, TActivityNoteID, TActivityOwner> activitySyncCommand = this;
    if (((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
    {
      PXCache cache = activitySyncCommand.graph.Caches[typeof (TActivity)];
      BqlCommand cmd = activitySyncCommand.GetSelectCommand();
      cmd = cmd.WhereAnd<Where<TActivitySyncronize, Equal<True>>>();
      if (activitySyncCommand.ExportInserted)
      {
        foreach (PXExchangeResponce<TExchangeType> exchangeResponce in activitySyncCommand.ExportActivityInserted(activitySyncCommand.SelectItems<TActivityNoteID>((PXGraph) activitySyncCommand.graph, cmd, (IEnumerable<PXSyncMailbox>) mailboxes, imported, PXSyncItemStatus.Inserted, activitySyncCommand.Attachments)))
        {
          PXExchangeResponce<TExchangeType> item = exchangeResponce;
          PXSyncTag<TActivity> tag = ((PXExchangeReBase<TExchangeType>) item).Tag as PXSyncTag<TActivity>;
          yield return activitySyncCommand.SafeOperation<TExchangeType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.Subject, (string) null, PXSyncItemStatus.Inserted, (Action) (() =>
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((PXExchangeItem<TExchangeType>) item).Item.ItemId, ((PXExchangeItem<TExchangeType>) item).Item.ConversationId, ((PXExchangeItem<TExchangeType>) item).Hash, new bool?(true));
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.PostProcessingSuccessfull(cache, PXSyncItemStatus.Inserted, item, tag);
          }), (Func<bool>) (() =>
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.PostProcessingFailed(cache, PXSyncItemStatus.Inserted, item, tag);
            return false;
          }));
        }
      }
      if (activitySyncCommand.ExportUpdated)
      {
        foreach (PXExchangeResponce<TExchangeType> exchangeResponce in activitySyncCommand.ExportActivityUpdated(activitySyncCommand.SelectItems<TActivityNoteID>((PXGraph) activitySyncCommand.graph, cmd, (IEnumerable<PXSyncMailbox>) mailboxes, imported, PXSyncItemStatus.Updated, activitySyncCommand.Attachments)))
        {
          PXExchangeResponce<TExchangeType> item = exchangeResponce;
          PXSyncTag<TActivity> tag = ((PXExchangeReBase<TExchangeType>) item).Tag as PXSyncTag<TActivity>;
          yield return activitySyncCommand.SafeOperation<TExchangeType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.Subject, (string) null, PXSyncItemStatus.Updated, (Action) (() =>
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals2.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((PXExchangeItem<TExchangeType>) item).Item.ItemId, ((PXExchangeItem<TExchangeType>) item).Item.ConversationId, ((PXExchangeItem<TExchangeType>) item).Hash, new bool?(true));
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals2.\u003C\u003E4__this.PostProcessingSuccessfull(cache, PXSyncItemStatus.Updated, item, tag);
          }), (Func<bool>) (() =>
          {
            if (item.Code == 241)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CS\u0024\u003C\u003E8__locals2.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(false));
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CS\u0024\u003C\u003E8__locals2.\u003C\u003E4__this.MarkUnsynced(tag);
              return true;
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals2.\u003C\u003E4__this.PostProcessingFailed(cache, PXSyncItemStatus.Updated, item, tag);
            return false;
          }));
        }
      }
      if (activitySyncCommand.ExportDeleted)
      {
        foreach (PXExchangeResponce<TExchangeType> exchangeResponce in activitySyncCommand.ExportActivityDeleted(activitySyncCommand.SelectItems<TActivityNoteID>((PXGraph) activitySyncCommand.graph, cmd, (IEnumerable<PXSyncMailbox>) mailboxes, imported, PXSyncItemStatus.Deleted, false)))
        {
          PXExchangeResponce<TExchangeType> item = exchangeResponce;
          PXSyncTag<TActivity> tag = ((PXExchangeReBase<TExchangeType>) item).Tag as PXSyncTag<TActivity>;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          yield return activitySyncCommand.SafeOperation<TExchangeType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.Subject, (string) null, PXSyncItemStatus.Deleted, (Action) (() => this.CS\u0024\u003C\u003E8__locals3.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(true))));
        }
        if (activitySyncCommand.ExportReowned)
        {
          BqlCommand command = PXSelectBase<TActivity, PXSelectJoin<TActivity, InnerJoin<EMailSyncReference, On<EMailSyncReference.noteID, Equal<TActivityNoteID>>, InnerJoin<Contact, On<Contact.eMail, Equal<EMailSyncReference.address>>, InnerJoin<EPEmployee, On<EPEmployee.defContactID, Equal<Contact.contactID>, And<EPEmployee.parentBAccountID, Equal<Contact.bAccountID>>>>>>, Where<CRActivity.classID, Equal<CRActivityClass.task>, And<EPEmployee.defContactID, NotEqual<TActivityOwner>, And<EMailSyncReference.binaryReference, NotEqual<Empty>, And<EMailSyncReference.address, Equal<Required<EMailSyncReference.address>>>>>>>.Config>.GetCommand();
          foreach (PXExchangeResponce<TExchangeType> exchangeResponce in activitySyncCommand.ExportActivityDeleted((IEnumerable<PXSyncItemBucket<TActivity>>) activitySyncCommand.SelectItems<IBqlTable, IBqlTable>((PXGraph) activitySyncCommand.graph, command, (IEnumerable<PXSyncMailbox>) mailboxes, imported, PXSyncItemStatus.None)))
          {
            PXExchangeResponce<TExchangeType> item = exchangeResponce;
            PXSyncTag<TActivity> tag = ((PXExchangeReBase<TExchangeType>) item).Tag as PXSyncTag<TActivity>;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            yield return activitySyncCommand.SafeOperation<TExchangeType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.Subject, (string) null, PXSyncItemStatus.Deleted, (Action) (() => this.CS\u0024\u003C\u003E8__locals4.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(true))));
          }
        }
      }
    }
  }

  protected override IEnumerable<PXSyncResult> ProcessSyncExportUnsynced(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported)
  {
    ExchangeActivitySyncCommand<TGraph, TExchangeType, TActivity, TActivitySyncronize, TActivityNoteID, TActivityOwner> activitySyncCommand = this;
    if (((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
    {
      PXCache cache = activitySyncCommand.graph.Caches[typeof (TActivity)];
      BqlCommand bqlCommand = activitySyncCommand.GetSelectCommandUnsynced().WhereAnd<Where<TActivitySyncronize, Equal<True>>>();
      foreach (PXExchangeResponce<TExchangeType> exchangeResponce in activitySyncCommand.ExportActivityUnsynced(activitySyncCommand.SelectItems<TActivityNoteID>((PXGraph) activitySyncCommand.graph, bqlCommand, (IEnumerable<PXSyncMailbox>) mailboxes, exported, PXSyncItemStatus.Unsynced, false)))
      {
        PXExchangeResponce<TExchangeType> item = exchangeResponce;
        PXSyncTag<TActivity> tag = ((PXExchangeReBase<TExchangeType>) item).Tag as PXSyncTag<TActivity>;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        yield return activitySyncCommand.SafeOperation<TExchangeType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.Subject, (string) null, PXSyncItemStatus.Updated, (Action) (() => this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((PXExchangeItem<TExchangeType>) item).Item.ItemId, ((PXExchangeItem<TExchangeType>) item).Item.ConversationId, ((PXExchangeItem<TExchangeType>) item).Hash, new bool?(true))), (Func<bool>) (() =>
        {
          if (item.Code == 241)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(false));
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.MarkUnsynced(tag);
            return true;
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.PostProcessingFailed(cache, PXSyncItemStatus.Updated, item, tag);
          return false;
        }));
      }
    }
  }

  protected override IEnumerable<PXSyncResult> ProcessSyncImport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported)
  {
    ExchangeActivitySyncCommand<TGraph, TExchangeType, TActivity, TActivitySyncronize, TActivityNoteID, TActivityOwner> activitySyncCommand = this;
    if (((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
    {
      IEnumerable<PXExchangeItem<TExchangeType>> items = activitySyncCommand.GetItems((IEnumerable<PXSyncMailbox>) mailboxes, exported, (PXExchangeFindOptions) 0, Tuple.Create<string, MapiPropertyTypeType>("0x3e4a", (MapiPropertyTypeType) 25));
      List<PXExchangeRequest<TExchangeType, ItemIdType>> toMove = new List<PXExchangeRequest<TExchangeType, ItemIdType>>();
      int i = 0;
      foreach (PXExchangeItem<TExchangeType> pxExchangeItem in items)
      {
        PXExchangeItem<TExchangeType> fitem = pxExchangeItem;
        if (i++ % 100 == 0)
          activitySyncCommand.graph = activitySyncCommand.CreateGraphWithPresetPrimaryCache();
        PXExchangeItem<TExchangeType> item = fitem;
        PXSyncMailbox mailbox = ((IEnumerable<PXSyncMailbox>) mailboxes).FirstOrDefault<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => string.Equals(m.Address, ((PXExchangeItemBase) item).Address, StringComparison.InvariantCultureIgnoreCase)));
        BqlCommand bqlCommand1 = PXSelectBase<TActivity, PXSelectJoin<TActivity, LeftJoin<EMailSyncReference, On<EMailSyncReference.noteID, Equal<TActivityNoteID>, And<EMailSyncReference.serverID, Equal<Required<EMailSyncReference.serverID>>, And<EMailSyncReference.address, Equal<Required<EMailSyncReference.address>>>>>>>.Config>.GetCommand();
        if (!activitySyncCommand.PreSyncRestrict(item.Item))
        {
          TActivity activity = default (TActivity);
          EMailSyncReference emailSyncReference = (EMailSyncReference) null;
          bool isSynchronized = true;
          if (item.Item.ExtendedProperty != null && ((IEnumerable<ExtendedPropertyType>) item.Item.ExtendedProperty).Any<ExtendedPropertyType>((Func<ExtendedPropertyType, bool>) (_ => _.ExtendedFieldURI.PropertyTag == "0x3e4a")))
          {
            bqlCommand1 = bqlCommand1.WhereNew<Where<TActivityNoteID, Equal<Required<TActivityNoteID>>, And<TActivitySyncronize, Equal<True>>>>();
            PXResult<TActivity, EMailSyncReference> pxResult = (PXResult<TActivity, EMailSyncReference>) new PXView((PXGraph) activitySyncCommand.graph, true, bqlCommand1).SelectSingle(new object[3]
            {
              (object) activitySyncCommand.Account.AccountID,
              (object) ((PXExchangeItemBase) item).Address,
              (object) new Guid(((IEnumerable<ExtendedPropertyType>) item.Item.ExtendedProperty).First<ExtendedPropertyType>((Func<ExtendedPropertyType, bool>) (_ => _.ExtendedFieldURI.PropertyTag == "0x3e4a")).Item.ToString())
            });
            activity = PXResult<TActivity, EMailSyncReference>.op_Implicit(pxResult);
            emailSyncReference = PXResult<TActivity, EMailSyncReference>.op_Implicit(pxResult);
          }
          if ((object) activity == null)
          {
            isSynchronized = false;
            BqlCommand bqlCommand2 = bqlCommand1.WhereNew<Where<EMailSyncReference.binaryReference, Equal<Required<EMailSyncReference.binaryReference>>>>();
            PXResult<TActivity, EMailSyncReference> pxResult = (PXResult<TActivity, EMailSyncReference>) new PXView((PXGraph) activitySyncCommand.graph, true, bqlCommand2).SelectSingle(new object[3]
            {
              (object) activitySyncCommand.Account.AccountID,
              (object) ((PXExchangeItemBase) item).Address,
              (object) item.Item.ItemId.Id
            });
            activity = PXResult<TActivity, EMailSyncReference>.op_Implicit(pxResult);
            emailSyncReference = PXResult<TActivity, EMailSyncReference>.op_Implicit(pxResult);
          }
          if ((object) activity == null || emailSyncReference == null || !(emailSyncReference.BinaryChangeKey == item.Item.ItemId.ChangeKey))
          {
            Guid? noteid = (Guid?) activity?.NoteID;
            PXSyncItemStatus status = (object) activity == null ? PXSyncItemStatus.Inserted : PXSyncItemStatus.Updated;
            yield return Tools.DoWith<PXSyncResult, PXSyncResult>(activitySyncCommand.SafeOperation(((PXExchangeItemBase) item).Address, item.Item.ItemId.Id, noteid, item.Item.Subject, (string) null, status, (Action) (() =>
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.ImportAction(mailbox, item.Item, ref activity);
              if ((object) activity == null)
                return;
              noteid = activity.NoteID;
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              if (this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.SaveAttachmentsSync((PXGraph) this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.graph, activity, item.Attachments))
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.graph.Actions.PressSave();
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) item).Address, activity.NoteID, item.Item.ItemId, item.Item.ConversationId, item.Hash, new bool?(isSynchronized));
              PXSyncDirectFolder syncDirectFolder = ((IEnumerable<PXSyncMailbox>) mailboxes).SelectMany<PXSyncMailbox, PXSyncDirectFolder>((Func<PXSyncMailbox, IEnumerable<PXSyncDirectFolder>>) (m => (IEnumerable<PXSyncDirectFolder>) m.Folders)).FirstOrDefault<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.FolderID is FolderIdType && ((FolderIdType) f.FolderID).Id == item.Item.ParentFolderId.Id));
              if (syncDirectFolder == null || syncDirectFolder.MoveToFolder == null || syncDirectFolder.MoveToFolder.Length == 0)
                return;
              foreach (PXSyncMovingCondition syncMovingCondition in syncDirectFolder.MoveToFolder)
              {
                BaseFolderIdType baseFolderIdType = syncMovingCondition.Evaluate(mailbox, (ItemType) item.Item);
                if (baseFolderIdType != null)
                {
                  toMove.Add(new PXExchangeRequest<TExchangeType, ItemIdType>(new PXExchangeFolderID(((PXExchangeItemBase) fitem).Address, baseFolderIdType, false), fitem.Item.ItemId, Guid.NewGuid().ToString(), (object) activity, item.Attachments));
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  this.CS\u0024\u003C\u003E8__locals1.\u003C\u003E4__this.Provider.LogInfo(((PXExchangeItemBase) item).Address, "Item '{0}' is processed and will be moved to the parend folder.", (object) item.Item.Subject);
                  break;
                }
              }
            }), true), (Func<PXSyncResult, PXSyncResult>) (r => new PXSyncResult(r, note: noteid, status: new PXSyncItemStatus?(status))), (Func<PXSyncResult>) null);
            activitySyncCommand.graph.Clear();
          }
        }
      }
      if (toMove.Count > 0)
      {
        activitySyncCommand.Provider.LogVerbose((string) null, "Moving prepared items to the parent folder.");
        foreach (PXExchangeResponce<TExchangeType> moveItem in activitySyncCommand.Gate.MoveItems<TExchangeType>((IEnumerable<PXExchangeRequest<TExchangeType, ItemIdType>>) toMove))
        {
          PXExchangeResponce<TExchangeType> moved = moveItem;
          TActivity activity = ((PXExchangeReBase<TExchangeType>) moved).Tag as TActivity;
          activitySyncCommand.SafeOperation<TExchangeType>(moved, ((PXExchangeItem<TExchangeType>) moved).Item.ItemId.Id, activity.NoteID, activity.Subject, "Moving", PXSyncItemStatus.None, (Action) (() =>
          {
            if ((object) activity == null || (object) ((PXExchangeItem<TExchangeType>) moved).Item == null || ((PXExchangeItem<TExchangeType>) moved).Item.ItemId == null)
              return;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.CS\u0024\u003C\u003E8__locals2.\u003C\u003E4__this.SaveReference(((PXExchangeItemBase) moved).Address, activity.NoteID, ((PXExchangeItem<TExchangeType>) moved).Item.ItemId, ((PXExchangeItem<TExchangeType>) moved).Item.ConversationId, (string) null, new bool?());
          }));
        }
      }
    }
  }

  protected virtual IEnumerable<PXExchangeResponce<TExchangeType>> ExportActivityInserted(
    IEnumerable<PXSyncItemBucket<TActivity>> inserted)
  {
    ExchangeActivitySyncCommand<TGraph, TExchangeType, TActivity, TActivitySyncronize, TActivityNoteID, TActivityOwner> activitySyncCommand = this;
    List<string> canceled = new List<string>();
    Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemType>> selector = (Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemType>>) (bucket =>
    {
      PXSyncMailbox mailbox = bucket.Mailbox;
      TActivity activity = bucket.Item1;
      TExchangeType exchangeType = new TExchangeType();
      ParameterExpression parameterExpression1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      this.\u003C\u003E4__this.ExportInsertedItemProperty<TExchangeType>(Expression.Lambda<Func<TExchangeType, object>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression1), exchangeType, (object) PXExchangeConversionHelper.GetExtendedProperties(Tuple.Create<string, MapiPropertyTypeType, object>("0x3e4a", (MapiPropertyTypeType) 25, (object) activity.NoteID.ToString())));
      ParameterExpression parameterExpression2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      this.\u003C\u003E4__this.ExportInsertedItemProperty<TExchangeType>(Expression.Lambda<Func<TExchangeType, object>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Subject))), parameterExpression2), exchangeType, (object) activity.Subject);
      if (!string.IsNullOrEmpty(activity.Body))
      {
        ParameterExpression parameterExpression3;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E4__this.ExportInsertedItemProperty<TExchangeType>(Expression.Lambda<Func<TExchangeType, object>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Body))), parameterExpression3), exchangeType, (object) new BodyType()
        {
          BodyType1 = (BodyTypeType) 0,
          Value = this.\u003C\u003E4__this.ClearHtml((PXGraph) this.\u003C\u003E4__this.graph, activity, activity.Body, true, (IEnumerable<UploadFileWithData>) bucket.Attachments)
        });
      }
      // ISSUE: reference to a compiler-generated field
      PXSyncTag tag = this.\u003C\u003E4__this.ExportInsertedAction(mailbox, exchangeType, activity);
      ParameterExpression parameterExpression4;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E4__this.ExportInsertedItemProperty<TExchangeType>(Expression.Lambda<Func<TExchangeType, object>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Categories))), parameterExpression4), exchangeType, (object) new string[1]
      {
        this.\u003C\u003E4__this.Policy.Category
      });
      if (tag != null && tag.CancelRequired)
        canceled.Add(bucket.ID);
      // ISSUE: reference to a compiler-generated field
      PXExchangeRequest<TExchangeType, ItemType> pxExchangeRequest = new PXExchangeRequest<TExchangeType, ItemType>((PXExchangeFolderID) mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport)), (ItemType) exchangeType, bucket.ID, (object) new PXSyncTag<TActivity>(activity, mailbox, bucket.Reference, tag), this.\u003C\u003E4__this.ConvertAttachment(bucket.Attachments));
      if (tag != null && tag.SendRequired)
        pxExchangeRequest.SendRequired = tag.SendRequired;
      if (tag != null && tag.SendSeparateRequired)
        pxExchangeRequest.SendSeparateRequired = tag.SendSeparateRequired;
      return pxExchangeRequest;
    });
    foreach (PXExchangeResponce<TExchangeType> exchangeResponce in activitySyncCommand.Gate.CreateItems<TExchangeType>(inserted.Select<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemType>>(selector)))
    {
      if (exchangeResponce.Success && canceled.Contains(((PXExchangeReBase<TExchangeType>) exchangeResponce).UID))
      {
        PXSyncDirectFolder syncDirectFolder = (((PXExchangeReBase<TExchangeType>) exchangeResponce).Tag as PXSyncTag<TActivity>).Mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport));
        TExchangeType exchangeType = new TExchangeType();
        exchangeType.ItemId = ((PXExchangeItem<TExchangeType>) exchangeResponce).Item.ItemId;
        // ISSUE: variable of a boxed type
        __Boxed<TExchangeType> local = (object) exchangeType;
        string uid = ((PXExchangeReBase<TExchangeType>) exchangeResponce).UID;
        object tag = ((PXExchangeReBase<TExchangeType>) exchangeResponce).Tag;
        PXExchangeRequest<TExchangeType, ItemType> pxExchangeRequest = new PXExchangeRequest<TExchangeType, ItemType>((PXExchangeFolderID) syncDirectFolder, (ItemType) local, uid, tag, (AttachmentType[]) null);
        PXExchangeServer gate = activitySyncCommand.Gate;
        PXExchangeRequest<TExchangeType, ItemType>[] pxExchangeRequestArray = new PXExchangeRequest<TExchangeType, ItemType>[1]
        {
          pxExchangeRequest
        };
        foreach (PXExchangeResponce<TExchangeType> deleteItem in gate.DeleteItems<TExchangeType>(pxExchangeRequestArray))
          yield return deleteItem;
      }
      else
        yield return exchangeResponce;
    }
  }

  protected virtual IEnumerable<PXExchangeResponce<TExchangeType>> ExportActivityUpdated(
    IEnumerable<PXSyncItemBucket<TActivity>> updated)
  {
    ExchangeActivitySyncCommand<TGraph, TExchangeType, TActivity, TActivitySyncronize, TActivityNoteID, TActivityOwner> activitySyncCommand = this;
    List<string> canceled = new List<string>();
    Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemChangeType>> selector = (Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemChangeType>>) (bucket =>
    {
      PXSyncMailbox mailbox = bucket.Mailbox;
      TActivity activity = bucket.Item1;
      List<ItemChangeDescriptionType> updates = new List<ItemChangeDescriptionType>();
      ParameterExpression parameterExpression1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.\u003C\u003E4__this.ExportUpdatedItemProperty<TExchangeType>(Expression.Lambda<Func<TExchangeType, object>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression1), "0x3e4a", (MapiPropertyTypeType) 25, (object) activity.NoteID.ToString()));
      ParameterExpression parameterExpression2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.\u003C\u003E4__this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Subject))), parameterExpression2), (UnindexedFieldURIType) 20, (object) activity.Subject));
      if (!string.IsNullOrEmpty(activity.Body))
      {
        ParameterExpression parameterExpression3;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.\u003C\u003E4__this.ExportUpdatedItemProperty<TaskType>(Expression.Lambda<Func<TaskType, object>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Body))), parameterExpression3), (UnindexedFieldURIType) 36, (object) new BodyType()
        {
          BodyType1 = (BodyTypeType) 0,
          Value = this.\u003C\u003E4__this.ClearHtml((PXGraph) this.\u003C\u003E4__this.graph, activity, activity.Body, false, (IEnumerable<UploadFileWithData>) bucket.Attachments)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      PXSyncTag tag = this.\u003C\u003E4__this.ExportUpdatedAction(mailbox, default (TExchangeType), activity, updates);
      if (tag != null && tag.SkipReqired)
        canceled.Add(bucket.ID);
      ItemChangeType itemChangeType = new ItemChangeType()
      {
        Item = (BaseItemIdType) new ItemIdType()
        {
          Id = bucket.Reference.BinaryReference
        },
        Updates = updates.ToArray()
      };
      if (tag != null && tag.CancelRequired)
        canceled.Add(bucket.ID);
      // ISSUE: reference to a compiler-generated field
      PXExchangeRequest<TExchangeType, ItemChangeType> pxExchangeRequest = new PXExchangeRequest<TExchangeType, ItemChangeType>((PXExchangeFolderID) mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport)), itemChangeType, bucket.ID, (object) new PXSyncTag<TActivity>(activity, mailbox, bucket.Reference, tag), this.\u003C\u003E4__this.ConvertAttachment(bucket.Attachments));
      if (tag != null && tag.SendRequired)
        pxExchangeRequest.SendRequired = tag.SendRequired;
      if (tag != null && tag.SendSeparateRequired)
        pxExchangeRequest.SendSeparateRequired = tag.SendSeparateRequired;
      return pxExchangeRequest;
    });
    foreach (PXExchangeResponce<TExchangeType> updateItem in activitySyncCommand.Gate.UpdateItems<TExchangeType>(updated.Select<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemChangeType>>(selector)))
    {
      if (updateItem.Success && canceled.Contains(((PXExchangeReBase<TExchangeType>) updateItem).UID))
      {
        PXSyncDirectFolder syncDirectFolder = (((PXExchangeReBase<TExchangeType>) updateItem).Tag as PXSyncTag<TActivity>).Mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport));
        TExchangeType exchangeType = new TExchangeType();
        exchangeType.ItemId = ((PXExchangeItem<TExchangeType>) updateItem).Item.ItemId;
        // ISSUE: variable of a boxed type
        __Boxed<TExchangeType> local = (object) exchangeType;
        string uid = ((PXExchangeReBase<TExchangeType>) updateItem).UID;
        object tag = ((PXExchangeReBase<TExchangeType>) updateItem).Tag;
        PXExchangeRequest<TExchangeType, ItemType> pxExchangeRequest = new PXExchangeRequest<TExchangeType, ItemType>((PXExchangeFolderID) syncDirectFolder, (ItemType) local, uid, tag, (AttachmentType[]) null);
        PXExchangeServer gate = activitySyncCommand.Gate;
        PXExchangeRequest<TExchangeType, ItemType>[] pxExchangeRequestArray = new PXExchangeRequest<TExchangeType, ItemType>[1]
        {
          pxExchangeRequest
        };
        foreach (PXExchangeResponce<TExchangeType> deleteItem in gate.DeleteItems<TExchangeType>(pxExchangeRequestArray))
          yield return deleteItem;
      }
      else
        yield return updateItem;
    }
  }

  protected virtual IEnumerable<PXExchangeResponce<TExchangeType>> ExportActivityUnsynced(
    IEnumerable<PXSyncItemBucket<TActivity>> updated)
  {
    Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemChangeType>> selector = (Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemChangeType>>) (bucket =>
    {
      PXSyncMailbox mailbox = bucket.Mailbox;
      TActivity row = bucket.Item1;
      List<ItemChangeDescriptionType> changeDescriptionTypeList = new List<ItemChangeDescriptionType>();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<TExchangeType>(Expression.Lambda<Func<TExchangeType, object>>((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_ExtendedProperty))), parameterExpression), "0x3e4a", (MapiPropertyTypeType) 25, (object) row.NoteID.ToString()));
      ItemChangeType itemChangeType = new ItemChangeType()
      {
        Item = (BaseItemIdType) new ItemIdType()
        {
          Id = bucket.Reference.BinaryReference
        },
        Updates = changeDescriptionTypeList.ToArray()
      };
      return new PXExchangeRequest<TExchangeType, ItemChangeType>((PXExchangeFolderID) mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport)), itemChangeType, bucket.ID, (object) new PXSyncTag<TActivity>(row, mailbox, bucket.Reference), this.ConvertAttachment(bucket.Attachments));
    });
    return this.Gate.UpdateItems<TExchangeType>(updated.Select<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemChangeType>>(selector));
  }

  protected virtual IEnumerable<PXExchangeResponce<TExchangeType>> ExportActivityDeleted(
    IEnumerable<PXSyncItemBucket<TActivity>> deleted)
  {
    Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemType>> selector = (Func<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemType>>) (bucket =>
    {
      string str = Guid.NewGuid().ToString();
      PXSyncMailbox mailbox = bucket.Mailbox;
      TActivity row = bucket.Item1;
      EMailSyncReference reference = bucket.Reference;
      return new PXExchangeRequest<TExchangeType, ItemType>((PXExchangeFolderID) mailbox.Folders.First<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.IsExport)), (ItemType) new TExchangeType()
      {
        ItemId = new ItemIdType()
        {
          Id = reference.BinaryReference
        }
      }, str, (object) new PXSyncTag<TActivity>(row, mailbox, bucket.Reference), (AttachmentType[]) null);
    });
    return this.Gate.DeleteItems<TExchangeType>(deleted.Select<PXSyncItemBucket<TActivity>, PXExchangeRequest<TExchangeType, ItemType>>(selector));
  }

  protected virtual void PostProcessingSuccessfull(
    PXCache cache,
    PXSyncItemStatus status,
    PXExchangeResponce<TExchangeType> item,
    PXSyncTag<TActivity> tag)
  {
  }

  protected virtual void PostProcessingFailed(
    PXCache cache,
    PXSyncItemStatus status,
    PXExchangeResponce<TExchangeType> item,
    PXSyncTag<TActivity> tag)
  {
  }

  protected virtual void MarkUnsynced(PXSyncTag<TActivity> tag)
  {
    PXDatabase.Update<TActivity>(new List<PXDataFieldParam>()
    {
      (PXDataFieldParam) new PXDataFieldAssign<TActivitySyncronize>((object) false),
      (PXDataFieldParam) new PXDataFieldRestrict<TActivityNoteID>((object) tag.Row.NoteID.Value)
    }.ToArray());
  }

  protected void PrepareActivity(
    PXSyncMailbox mailbox,
    TExchangeType item,
    bool evaluateOwner,
    ref TActivity activity)
  {
    PXCache cach = this.graph.Caches[typeof (TActivity)];
    if ((object) activity == null)
    {
      activity = new TActivity();
      activity = (TActivity) cach.Insert((object) activity);
    }
    else if (!activity.Synchronize.GetValueOrDefault())
    {
      activity = default (TActivity);
      return;
    }
    activity.Synchronize = new bool?(true);
    activity.Subject = item.Subject ?? activity.Subject ?? "Empty subject";
    if (item.Body != null)
      activity.Body = RichStyle.RemoveViewStyle(item.Body.Value);
    if (evaluateOwner)
    {
      int num;
      if (this.Cache.EmployeeCache.TryGetValue(mailbox.EmployeeID, out num))
        activity.OwnerID = new int?(num);
    }
    else
      activity.OwnerID = new int?();
    activity = (TActivity) cach.Update((object) activity);
  }

  protected void PostpareActivity(
    PXSyncMailbox mailbox,
    TExchangeType item,
    ref TActivity activity)
  {
    PXCache cach = this.graph.Caches[typeof (TActivity)];
    activity = (TActivity) cach.Update((object) activity);
    PXSave<TActivity> pxSave = ((OrderedDictionary) this.graph.Actions).Values.OfType<PXSave<TActivity>>().First<PXSave<TActivity>>();
    ((PXAction<TActivity>) pxSave).PressImpl(true, false);
    activity.Body = this.ClearHtml((PXGraph) this.graph, activity, activity.Body, (IEnumerable<AttachmentType>) item.Attachments);
    activity = (TActivity) cach.Update((object) activity);
    ((PXAction<TActivity>) pxSave).PressImpl(true, false);
  }

  protected abstract PXSyncTag ExportInsertedAction(
    PXSyncMailbox account,
    TExchangeType item,
    TActivity activity);

  protected abstract PXSyncTag ExportUpdatedAction(
    PXSyncMailbox account,
    TExchangeType item,
    TActivity activity,
    List<ItemChangeDescriptionType> updates);

  protected abstract PXSyncTag ImportAction(
    PXSyncMailbox account,
    TExchangeType item,
    ref TActivity activity);

  protected virtual bool PreSyncRestrict(TExchangeType item) => false;

  protected virtual BqlCommand GetSelectCommandUnsynced() => this.GetSelectCommand();
}
