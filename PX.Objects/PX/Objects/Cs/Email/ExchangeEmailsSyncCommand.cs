// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.ExchangeEmailsSyncCommand
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;

#nullable enable
namespace PX.Objects.CS.Email;

public class ExchangeEmailsSyncCommand : 
  ExchangeActivitySyncCommand<
  #nullable disable
  CREmailActivityMaint, MessageType, CRSMEmail, CRSMEmail.synchronize, CRSMEmail.noteID, CRSMEmail.ownerID>
{
  public ExchangeEmailsSyncCommand(MicrosoftExchangeSyncProvider provider)
    : base(provider, "Emails", (PXExchangeFindOptions) 0)
  {
    this.ExportInserted = true;
    this.ExportUpdated = false;
    if (!this.Policy.EmailsAttachments.GetValueOrDefault())
      return;
    this.DefFindOptions = (PXExchangeFindOptions) (this.DefFindOptions | 16 /*0x10*/);
  }

  protected override void ConfigureEnvironment(
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    this.EnsureEnvironmentConfigured<FolderType>(mailboxes, new PXSyncFolderSpecification(this.Policy.EmailsFolder, (DistinguishedFolderIdNameType) 4, (PXEmailSyncDirection.Directions) 1, false, new PXSyncMovingCondition[2]
    {
      (PXSyncMovingCondition) new PXSyncMovingMessageCondition(this, (DistinguishedFolderIdNameType) 4, true, false),
      (PXSyncMovingCondition) new PXSyncMovingMessageCondition(this, (DistinguishedFolderIdNameType) 8, false, true)
    }), new PXSyncFolderSpecification((string) null, (DistinguishedFolderIdNameType) 4, (PXEmailSyncDirection.Directions) 1, true, Array.Empty<PXSyncMovingCondition>()), new PXSyncFolderSpecification(this.Policy.EmailsFolder, (DistinguishedFolderIdNameType) 8, (PXEmailSyncDirection.Directions) 1, false, new PXSyncMovingCondition[2]
    {
      (PXSyncMovingCondition) new PXSyncMovingMessageCondition(this, (DistinguishedFolderIdNameType) 4, true, false),
      (PXSyncMovingCondition) new PXSyncMovingMessageCondition(this, (DistinguishedFolderIdNameType) 8, false, true)
    }), new PXSyncFolderSpecification((string) null, (DistinguishedFolderIdNameType) 8, (PXEmailSyncDirection.Directions) 1, true, Array.Empty<PXSyncMovingCondition>()), new PXSyncFolderSpecification((string) null, (DistinguishedFolderIdNameType) 8, (PXEmailSyncDirection.Directions) 2, true, Array.Empty<PXSyncMovingCondition>()));
  }

  protected override List<PXSyncResult> PrepareImport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported = null)
  {
    List<PXSyncResult> pxSyncResultList = base.PrepareImport(mailboxes, exported);
    foreach (PXSyncResult pxSyncResult in pxSyncResultList)
    {
      try
      {
        if (pxSyncResult.Success)
        {
          if (pxSyncResult.NoteID.HasValue)
          {
            foreach (PXResult<SMEmail> pxResult in PXSelectBase<SMEmail, PXSelect<SMEmail, Where<SMEmail.refNoteID, Equal<Required<SMEmail.refNoteID>>>>.Config>.Select((PXGraph) this.graph, new object[1]
            {
              (object) pxSyncResult.NoteID
            }))
              this.graph.MessageProcessor.Process((object) PXResult<SMEmail>.op_Implicit(pxResult));
          }
        }
      }
      catch (Exception ex)
      {
        pxSyncResult.Error = ex;
      }
    }
    pxSyncResultList.AddRange(this.ProcessSyncExportUnsynced(mailboxes, exported));
    return pxSyncResultList;
  }

  protected override void ExportImportFirst(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    this.ExportFirst(policy, direction, mailboxes);
  }

  protected override void LastUpdated(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    this.ExportFirst(policy, direction, mailboxes);
  }

  protected override void KeepBoth(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    this.ExportFirst(policy, direction, mailboxes);
  }

  protected virtual void ProcessSyncImportExported(PXExchangeResponce<MessageType> exportedItem)
  {
    PXExchangeServer gate = this.Gate;
    BasePathToElementType[] pathToElementTypeArray = new BasePathToElementType[1]
    {
      (BasePathToElementType) new PathToExtendedFieldType()
      {
        PropertyTag = "0x3e4a",
        PropertyType = (MapiPropertyTypeType) 25
      }
    };
    PXExchangeItemID[] pxExchangeItemIdArray = new PXExchangeItemID[1]
    {
      new PXExchangeItemID(((PXExchangeItemBase) exportedItem).Address, ((ItemType) ((PXExchangeItem<MessageType>) exportedItem).Item).ItemId, new DateTime?(((ItemType) ((PXExchangeItem<MessageType>) exportedItem).Item).DateTimeReceived))
    };
    using (IEnumerator<PXExchangeItem<MessageType>> enumerator = gate.GetItems<MessageType>(false, false, pathToElementTypeArray, pxExchangeItemIdArray).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      PXExchangeItem<MessageType> current = enumerator.Current;
      SMEmail smEmail = (SMEmail) null;
      if (((ItemType) current.Item).ExtendedProperty != null && ((IEnumerable<ExtendedPropertyType>) ((ItemType) current.Item).ExtendedProperty).Any<ExtendedPropertyType>((Func<ExtendedPropertyType, bool>) (_ => _.ExtendedFieldURI.PropertyTag == "0x3e4a")))
        smEmail = PXResultset<SMEmail>.op_Implicit(PXSelectBase<SMEmail, PXSelect<SMEmail, Where<SMEmail.refNoteID, Equal<Required<SMEmail.refNoteID>>>>.Config>.SelectSingleBound((PXGraph) this.graph, new object[0], new object[1]
        {
          (object) new Guid(((IEnumerable<ExtendedPropertyType>) ((ItemType) current.Item).ExtendedProperty).First<ExtendedPropertyType>((Func<ExtendedPropertyType, bool>) (_ => _.ExtendedFieldURI.PropertyTag == "0x3e4a")).Item.ToString())
        }));
      if (smEmail == null)
        return;
      PXDatabase.Update<SMEmail>(new List<PXDataFieldParam>()
      {
        (PXDataFieldParam) new PXDataFieldAssign<SMEmail.messageId>((object) current.Item.InternetMessageId),
        (PXDataFieldParam) new PXDataFieldRestrict<SMEmail.noteID>((object) smEmail.NoteID)
      }.ToArray());
    }
  }

  protected override IEnumerable<PXSyncItemBucket<CRSMEmail, T2, T3>> SelectItems<T2, T3>(
    PXGraph graph,
    BqlCommand bqlCommand,
    IEnumerable<PXSyncMailbox> mailboxes,
    PXSyncItemSet exceptionSet,
    PXSyncItemStatus status)
  {
    List<CRSMEmail> crsmEmailList = new List<CRSMEmail>();
    PXCache<CRSMEmail> cache = GraphHelper.Caches<CRSMEmail>(graph);
    foreach (PXSyncItemBucket<CRSMEmail, T2, T3> selectItem in base.SelectItems<T2, T3>(graph, bqlCommand, mailboxes, exceptionSet, status))
    {
      string errorMessage;
      if (!PXEmailSyncHelper.ValidateAllAddressesInEmail(selectItem.Item1, out errorMessage))
      {
        CRSMEmail crsmEmail = selectItem.Item1;
        crsmEmail.Exception = crsmEmail.Exception + Environment.NewLine + errorMessage;
        selectItem.Item1.MPStatus = "FL";
        selectItem.Item1.UIStatus = "CL";
        ((PXCache) cache).PersistUpdated((object) selectItem.Item1);
        cache.Remove(selectItem.Item1);
      }
      else
        yield return selectItem;
    }
  }

  [Obsolete]
  public void SendMessage(PXSyncMailbox mailbox, IEnumerable<CRSMEmail> activities)
  {
    this.EnsureEnvironmentConfigured<FolderType>((IEnumerable<PXSyncMailbox>) new PXSyncMailbox[1]
    {
      mailbox
    }, new PXSyncFolderSpecification((string) null, (DistinguishedFolderIdNameType) 8, (PXEmailSyncDirection.Directions) 2, true, Array.Empty<PXSyncMovingCondition>()));
    List<PXSyncItemBucket<CRSMEmail>> source = new List<PXSyncItemBucket<CRSMEmail>>();
    foreach (CRSMEmail activity in activities)
    {
      EMailSyncReference reference = PXResultset<EMailSyncReference>.op_Implicit(PXSelectBase<EMailSyncReference, PXSelect<EMailSyncReference, Where<EMailSyncReference.serverID, Equal<Required<EMailSyncReference.serverID>>, And<EMailSyncReference.address, Equal<Required<EMailSyncReference.address>>, And<EMailSyncReference.noteID, Equal<Required<EMailSyncReference.noteID>>>>>>.Config>.SelectSingleBound((PXGraph) this.graph, (object[]) null, new object[3]
      {
        (object) this.Account.AccountID,
        (object) mailbox.Address,
        (object) activity.NoteID
      })) ?? new EMailSyncReference();
      PXSyncItemStatus status = string.IsNullOrEmpty(reference.BinaryReference) ? PXSyncItemStatus.Inserted : PXSyncItemStatus.Updated;
      PXSyncItemBucket<CRSMEmail> pxSyncItemBucket = new PXSyncItemBucket<CRSMEmail>(mailbox, status, reference, activity);
      if (this.Attachments)
      {
        List<UploadFileWithData> uploadFileWithDataList = new List<UploadFileWithData>();
        foreach (PXResult<UploadFileWithData> pxResult in PXSelectBase<UploadFileWithData, PXSelect<UploadFileWithData, Where<UploadFileWithData.noteID, Equal<Required<UploadFileWithData.noteID>>>>.Config>.Select((PXGraph) this.graph, new object[1]
        {
          (object) activity.NoteID
        }))
        {
          UploadFileWithData uploadFileWithData = PXResult<UploadFileWithData>.op_Implicit(pxResult);
          if (uploadFileWithData.Data != null)
          {
            int num1 = 20971520 /*0x01400000*/;
            int? syncAttachmentSize = this.Account.SyncAttachmentSize;
            if (syncAttachmentSize.HasValue)
            {
              syncAttachmentSize = this.Account.SyncAttachmentSize;
              int num2 = 0;
              if (syncAttachmentSize.GetValueOrDefault() > num2 & syncAttachmentSize.HasValue)
              {
                syncAttachmentSize = this.Account.SyncAttachmentSize;
                num1 = syncAttachmentSize.Value * 1024 /*0x0400*/;
              }
            }
            if (uploadFileWithData.Data.Length <= num1)
              uploadFileWithDataList.Add(uploadFileWithData);
          }
        }
        pxSyncItemBucket.Attachments = uploadFileWithDataList.ToArray();
      }
      source.Add(pxSyncItemBucket);
    }
    PXCache cache = ((PXGraph) this.graph).Caches[typeof (CRSMEmail)];
    foreach (PXExchangeResponce<MessageType> exchangeResponce in this.ExportActivityInserted(source.Where<PXSyncItemBucket<CRSMEmail>>((Func<PXSyncItemBucket<CRSMEmail>, bool>) (b => b.Status == PXSyncItemStatus.Inserted))))
    {
      PXExchangeResponce<MessageType> item = exchangeResponce;
      PXSyncTag<CRSMEmail> tag = ((PXExchangeReBase<MessageType>) item).Tag as PXSyncTag<CRSMEmail>;
      if (!this.SafeOperation<MessageType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.Subject, (string) null, PXSyncItemStatus.Inserted, (Action) (() =>
      {
        this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((ItemType) ((PXExchangeItem<MessageType>) item).Item).ItemId, ((ItemType) ((PXExchangeItem<MessageType>) item).Item).ConversationId, ((PXExchangeItem<MessageType>) item).Hash, new bool?(true));
        this.PostProcessingSuccessfull(cache, PXSyncItemStatus.Inserted, item, tag);
      }), (Func<bool>) (() =>
      {
        this.PostProcessingFailed(cache, PXSyncItemStatus.Inserted, item, tag);
        return false;
      })).Success)
        throw new Exception(this.Provider.CreateErrorMessage(true, PXMessages.LocalizeFormatNoPrefix("An error has occured during {0} sync. {1} of item '{2}' failed.", new object[3]
        {
          (object) this.OperationCode,
          (object) "E",
          (object) tag.Row.Subject
        }), item.Message, (Exception) null, item.Details));
    }
    foreach (PXExchangeResponce<MessageType> exchangeResponce in this.ExportActivityUpdated(source.Where<PXSyncItemBucket<CRSMEmail>>((Func<PXSyncItemBucket<CRSMEmail>, bool>) (b => b.Status == PXSyncItemStatus.Updated))))
    {
      PXExchangeResponce<MessageType> item = exchangeResponce;
      PXSyncTag<CRSMEmail> tag = ((PXExchangeReBase<MessageType>) item).Tag as PXSyncTag<CRSMEmail>;
      if (!this.SafeOperation<MessageType>(item, tag.Ref.BinaryReference, tag.Ref.NoteID, tag.Row.Subject, (string) null, PXSyncItemStatus.Updated, (Action) (() =>
      {
        this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, ((ItemType) ((PXExchangeItem<MessageType>) item).Item).ItemId, ((ItemType) ((PXExchangeItem<MessageType>) item).Item).ConversationId, ((PXExchangeItem<MessageType>) item).Hash, new bool?(true));
        this.PostProcessingSuccessfull(cache, PXSyncItemStatus.Updated, item, tag);
      }), (Func<bool>) (() =>
      {
        if (item.Code != 241)
          return false;
        this.SaveReference(((PXExchangeItemBase) item).Address, tag.Row.NoteID, (ItemIdType) null, (ItemIdType) null, (string) null, new bool?(false));
        this.PostProcessingFailed(cache, PXSyncItemStatus.Updated, item, tag);
        this.MarkUnsynced(tag);
        return true;
      })).Success)
        throw new Exception(this.Provider.CreateErrorMessage(true, PXMessages.LocalizeFormatNoPrefix("An error has occured during {0} sync. {1} of item '{2}' failed.", new object[3]
        {
          (object) this.OperationCode,
          (object) "E",
          (object) tag.Row.Subject
        }), item.Message, (Exception) null, item.Details));
    }
  }

  protected override BqlCommand GetSelectCommand()
  {
    return PXSelectBase<CRSMEmail, PXSelectReadonly<CRSMEmail, Where<CRSMEmail.isIncome, NotEqual<True>, And<Where<CRSMEmail.mpstatus, Equal<MailStatusListAttribute.preProcess>, And<Where<CRSMEmail.mailAccountID, Equal<Required<EMailSyncAccount.emailAccountID>>>>>>>>.Config>.GetCommand();
  }

  protected override BqlCommand GetSelectCommandUnsynced()
  {
    return PXSelectBase<CRSMEmail, PXSelectReadonly<CRSMEmail, Where<CRSMEmail.mpstatus, Equal<MailStatusListAttribute.processed>, And<CRSMEmail.mailAccountID, Equal<Required<EMailSyncAccount.emailAccountID>>>>>.Config>.GetCommand();
  }

  protected override PXSyncTag ExportInsertedAction(
    PXSyncMailbox account,
    MessageType item,
    CRSMEmail activity)
  {
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    Expression<Func<MessageType, object>> exp1 = Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_From))), parameterExpression1);
    MessageType messageType1 = item;
    SingleRecipientType singleRecipientType1 = new SingleRecipientType();
    singleRecipientType1.Item = PXExchangeConversionHelper.ParceAddress(account.Address);
    string address = account.Address;
    this.ExportInsertedItemPropertyConditional<MessageType>(exp1, messageType1, (object) singleRecipientType1, (object) address);
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    Expression<Func<MessageType, object>> exp2 = Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_Sender))), parameterExpression2);
    MessageType messageType2 = item;
    SingleRecipientType singleRecipientType2 = new SingleRecipientType();
    singleRecipientType2.Item = PXExchangeConversionHelper.ParceAddress(activity.MailFrom);
    string mailFrom = activity.MailFrom;
    this.ExportInsertedItemPropertyConditional<MessageType>(exp2, messageType2, (object) singleRecipientType2, (object) mailFrom);
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    this.ExportInsertedItemPropertyConditional<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_ToRecipients))), parameterExpression3), item, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailTo), (object) activity.MailTo);
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    this.ExportInsertedItemPropertyConditional<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_ReplyTo))), parameterExpression4), item, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailReply), (object) activity.MailReply);
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    this.ExportInsertedItemPropertyConditional<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_CcRecipients))), parameterExpression5), item, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailCc), (object) activity.MailCc);
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    this.ExportInsertedItemPropertyConditional<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_BccRecipients))), parameterExpression6), item, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailBcc), (object) activity.MailBcc);
    ParameterExpression parameterExpression7;
    // ISSUE: method reference
    this.ExportInsertedItemProperty<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), typeof (object)), parameterExpression7), item, (object) PXExchangeConversionHelper.ParceActivityPriority(activity.Priority));
    return new PXSyncTag()
    {
      SendRequired = true,
      SendSeparateRequired = true
    };
  }

  protected override PXSyncTag ExportUpdatedAction(
    PXSyncMailbox account,
    MessageType item,
    CRSMEmail activity,
    List<ItemChangeDescriptionType> updates)
  {
    if (activity.MPStatus == "PD" || activity.MPStatus == "IP" || activity.MPStatus == "PP" || activity.IsArchived.GetValueOrDefault())
      return new PXSyncTag() { SkipReqired = true };
    List<ItemChangeDescriptionType> changeDescriptionTypeList1 = updates;
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    Expression<Func<MessageType, object>> exp1 = Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_From))), parameterExpression1);
    SingleRecipientType singleRecipientType1 = new SingleRecipientType();
    singleRecipientType1.Item = PXExchangeConversionHelper.ParceAddress(account.Address);
    string address = account.Address;
    IEnumerable<SetItemFieldType> setItemFieldTypes1 = this.ExportUpdatedItemPropertyConditional<MessageType>(exp1, (UnindexedFieldURIType) 82, (object) singleRecipientType1, (object) address);
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList1, (IEnumerable<ItemChangeDescriptionType>) setItemFieldTypes1);
    List<ItemChangeDescriptionType> changeDescriptionTypeList2 = updates;
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    Expression<Func<MessageType, object>> exp2 = Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_Sender))), parameterExpression2);
    SingleRecipientType singleRecipientType2 = new SingleRecipientType();
    singleRecipientType2.Item = PXExchangeConversionHelper.ParceAddress(activity.MailFrom);
    string mailFrom = activity.MailFrom;
    IEnumerable<SetItemFieldType> setItemFieldTypes2 = this.ExportUpdatedItemPropertyConditional<MessageType>(exp2, (UnindexedFieldURIType) 82, (object) singleRecipientType2, (object) mailFrom);
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(changeDescriptionTypeList2, (IEnumerable<ItemChangeDescriptionType>) setItemFieldTypes2);
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_ToRecipients))), parameterExpression3), (UnindexedFieldURIType) 84, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailTo)));
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_ReplyTo))), parameterExpression4), (UnindexedFieldURIType) 81, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailReply)));
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_CcRecipients))), parameterExpression5), (UnindexedFieldURIType) 85, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailCc)));
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_BccRecipients))), parameterExpression6), (UnindexedFieldURIType) 86, (object) PXExchangeConversionHelper.ParceAddresses(activity.MailBcc)));
    ParameterExpression parameterExpression7;
    // ISSUE: method reference
    Tools.AddIfNotEmpty<ItemChangeDescriptionType>(updates, (IEnumerable<ItemChangeDescriptionType>) this.ExportUpdatedItemProperty<MessageType>(Expression.Lambda<Func<MessageType, object>>((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), typeof (object)), parameterExpression7), (UnindexedFieldURIType) 25, (object) PXExchangeConversionHelper.ParceActivityPriority(activity.Priority)));
    return new PXSyncTag()
    {
      SendRequired = true,
      SendSeparateRequired = true
    };
  }

  protected override PXSyncTag ImportAction(
    PXSyncMailbox account,
    MessageType item,
    ref CRSMEmail activity)
  {
    if (string.IsNullOrWhiteSpace(item.From?.Item?.EmailAddress))
    {
      activity = (CRSMEmail) null;
      return (PXSyncTag) null;
    }
    this.PrepareActivity(account, item, false, ref activity);
    if (activity == null)
      return (PXSyncTag) null;
    activity.MessageId = item.InternetMessageId;
    activity.MessageReference = item.References;
    activity.ClassID = new int?(4);
    activity.MailAccountID = account.EmailAccountID;
    activity.IsIncome = new bool?(true);
    if (((ItemType) item).IsDraftSpecified && ((ItemType) item).IsDraft)
      activity.MPStatus = "DR";
    activity.IsIncome = new bool?(this.EvaluateIncomming(account.Address, (ItemType) item).GetValueOrDefault());
    CRSMEmail copy = (CRSMEmail) ((PXGraph) this.graph).Caches[typeof (CRSMEmail)].CreateCopy((object) activity);
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    this.ImportItemProperty<MessageType, SingleRecipientType>(Expression.Lambda<Func<MessageType, SingleRecipientType>>((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_From))), parameterExpression1), item, (Action<SingleRecipientType>) (v => copy.MailFrom = new MailAddress(v.Item.EmailAddress, v.Item.Name).ToString()));
    ParameterExpression parameterExpression2;
    // ISSUE: method reference
    this.ImportItemProperty<MessageType, EmailAddressType[]>(Expression.Lambda<Func<MessageType, EmailAddressType[]>>((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_ToRecipients))), parameterExpression2), item, (Action<EmailAddressType[]>) (v => copy.MailTo = PXExchangeConversionHelper.ParceAddresses(v)));
    ParameterExpression parameterExpression3;
    // ISSUE: method reference
    this.ImportItemProperty<MessageType, EmailAddressType[]>(Expression.Lambda<Func<MessageType, EmailAddressType[]>>((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_ReplyTo))), parameterExpression3), item, (Action<EmailAddressType[]>) (v => copy.MailReply = PXExchangeConversionHelper.ParceAddresses(v)));
    ParameterExpression parameterExpression4;
    // ISSUE: method reference
    this.ImportItemProperty<MessageType, EmailAddressType[]>(Expression.Lambda<Func<MessageType, EmailAddressType[]>>((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_CcRecipients))), parameterExpression4), item, (Action<EmailAddressType[]>) (v => copy.MailCc = PXExchangeConversionHelper.ParceAddresses(v)));
    ParameterExpression parameterExpression5;
    // ISSUE: method reference
    this.ImportItemProperty<MessageType, EmailAddressType[]>(Expression.Lambda<Func<MessageType, EmailAddressType[]>>((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (MessageType.get_BccRecipients))), parameterExpression5), item, (Action<EmailAddressType[]>) (v => copy.MailBcc = PXExchangeConversionHelper.ParceAddresses(v)));
    ParameterExpression parameterExpression6;
    // ISSUE: method reference
    this.ImportItemProperty<MessageType, ImportanceChoicesType>(Expression.Lambda<Func<MessageType, ImportanceChoicesType>>((Expression) Expression.Property((Expression) parameterExpression6, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_Importance))), parameterExpression6), item, (Action<ImportanceChoicesType>) (v => copy.Priority = new int?(PXExchangeConversionHelper.ParceActivityPriority(v))));
    ParameterExpression parameterExpression7;
    // ISSUE: method reference
    this.ImportItemProperty<MessageType, DateTime>(Expression.Lambda<Func<MessageType, DateTime>>((Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ItemType.get_DateTimeReceived))), parameterExpression7), item, (Action<DateTime>) (v => copy.StartDate = new DateTime?(v)), exchTimezone: account.ExchangeTimeZone);
    activity = (CRSMEmail) ((PXGraph) this.graph).Caches[typeof (CRSMEmail)].Update((object) copy);
    this.PostpareActivity(account, item, ref activity);
    return (PXSyncTag) null;
  }

  protected override void PostProcessingSuccessfull(
    PXCache cache,
    PXSyncItemStatus status,
    PXExchangeResponce<MessageType> item,
    PXSyncTag<CRSMEmail> tag)
  {
    tag.Row.Exception = (string) null;
    tag.Row.StartDate = new DateTime?(PXTimeZoneInfo.Now);
    tag.Row.MPStatus = "PD";
    tag.Row.UIStatus = "CD";
    cache.Update((object) tag.Row);
    cache.Persist((PXDBOperation) 1);
    this.ProcessSyncImportExported(item);
  }

  protected override void PostProcessingFailed(
    PXCache cache,
    PXSyncItemStatus status,
    PXExchangeResponce<MessageType> item,
    PXSyncTag<CRSMEmail> tag)
  {
    tag.Row.Exception = item.Message;
    tag.Row.MPStatus = "FL";
    tag.Row.UIStatus = "CL";
    cache.Update((object) tag.Row);
    cache.Persist((PXDBOperation) 1);
  }

  protected override void MarkUnsynced(PXSyncTag<CRSMEmail> tag)
  {
    PXDatabase.Update<CRActivity>(new List<PXDataFieldParam>()
    {
      (PXDataFieldParam) new PXDataFieldAssign<CRActivity.synchronize>((object) false),
      (PXDataFieldParam) new PXDataFieldRestrict<CRActivity.noteID>((object) tag.Row.NoteID.Value)
    }.ToArray());
  }

  public bool? EvaluateIncomming(string mailbox, ItemType item)
  {
    if (!(item is MessageType messageType) || ((ItemType) messageType).IsDraftSpecified && ((ItemType) messageType).IsDraft || string.IsNullOrWhiteSpace(messageType.From?.Item?.EmailAddress))
      return new bool?();
    bool flag1 = true;
    if (messageType.From == null || messageType.ReceivedBy == null)
      flag1 = false;
    else if (messageType.From != null && string.Equals(messageType.From.Item.EmailAddress, mailbox, StringComparison.InvariantCultureIgnoreCase) || messageType.Sender != null && string.Equals(messageType.Sender.Item.EmailAddress, mailbox, StringComparison.InvariantCultureIgnoreCase))
    {
      flag1 = false;
    }
    else
    {
      bool flag2 = false;
      foreach (EmailAddressType emailAddressType in ((IEnumerable<EmailAddressType>) (messageType.ToRecipients ?? new EmailAddressType[0])).Concat<EmailAddressType>((IEnumerable<EmailAddressType>) (messageType.CcRecipients ?? new EmailAddressType[0])).Concat<EmailAddressType>((IEnumerable<EmailAddressType>) (messageType.BccRecipients ?? new EmailAddressType[0])))
      {
        if (string.Equals(emailAddressType.EmailAddress, mailbox, StringComparison.InvariantCultureIgnoreCase))
        {
          flag2 = true;
          break;
        }
        if (emailAddressType.MailboxTypeSpecified && (emailAddressType.MailboxType == 4 || emailAddressType.MailboxType == 3))
        {
          string[] source = this.Gate.ExpandGroup(emailAddressType.EmailAddress);
          if (source != null && ((IEnumerable<string>) source).Contains<string>(mailbox, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase))
          {
            flag2 = true;
            break;
          }
        }
      }
      if (flag2)
        flag1 = flag2;
    }
    return new bool?(flag1);
  }

  protected override bool PreSyncRestrict(MessageType item)
  {
    return ((ItemType) item).IsDraftSpecified && ((ItemType) item).IsDraft;
  }
}
