// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.ExchangeBaseLogicSyncCommand`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.RichTextEdit;
using PX.Data.Update;
using PX.Data.Update.ExchangeService;
using PX.Data.Update.WebServices;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Objects.CS.Email;

public abstract class ExchangeBaseLogicSyncCommand<GraphType, TPrimary, ExchangeType> : 
  ExchangeBaseSyncCommand
  where GraphType : PXGraph, new()
  where TPrimary : IBqlTable, new()
  where ExchangeType : ItemType, new()
{
  protected const string BrokenImageBase64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAOCAYAAAAmL5yKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAADNSURBVDhPYwCC/7HVB/+Hl+3/7527+79z2vb/9slbcWLL2I3/DcNW/tcJWPwfpJchueHI/wcvP//XD9uAF8PUINNavnP/M6Q2HUFRSArW9J71nyG66uD/y/ffk4xBLtDwmvafIbh4H1jgy/ffJGGQAeruk/4zBBTuJdsANbcJw8IA/4I9FBjQ/58BlPrINUDVpec/g3vWTrINUHZo/s/glLodzCEHK9s3gfIDw3+ruA3/jcJXATPIov9aPnPASRQfVnXp/q/i2AbUzPAfAPWz01dc928mAAAAAElFTkSuQmCC";
  protected const string ContentIdPrefix = "ac_file_id_";
  protected const string ContentIdPrefixOld = "ac_file_id:";
  private GraphType _graph;
  protected PXExchangeFindOptions DefFindOptions;
  private UploadFileMaintenance uploader;

  protected GraphType graph
  {
    get
    {
      if ((object) this._graph == null)
        this._graph = this.CreateGraphWithPresetPrimaryCache();
      return this._graph;
    }
    set => this._graph = value;
  }

  protected bool Attachments
  {
    get
    {
      return ((Enum) (object) this.DefFindOptions).HasFlag((Enum) (object) (PXExchangeFindOptions) 16 /*0x10*/);
    }
  }

  protected UploadFileMaintenance Uploader
  {
    get
    {
      if (this.uploader == null)
      {
        this.uploader = new UploadFileMaintenance();
        this.uploader.IgnoreFileRestrictions = true;
      }
      return this.uploader;
    }
  }

  protected ExchangeBaseLogicSyncCommand(
    MicrosoftExchangeSyncProvider provider,
    string operationCode,
    PXExchangeFindOptions findOption)
    : base(provider, operationCode)
  {
    this.DefFindOptions = findOption;
    this.CreateGraphWithPresetPrimaryCache();
  }

  protected virtual GraphType CreateGraphWithPresetPrimaryCache()
  {
    GraphType instance = PXGraph.CreateInstance<GraphType>();
    instance.IsImport = true;
    PXCache cach = instance.Caches[typeof (TPrimary)];
    cach.AllowDelete = true;
    cach.AllowInsert = true;
    cach.AllowUpdate = true;
    return instance;
  }

  protected void EnsureEnvironmentConfigured<T>(
    IEnumerable<PXSyncMailbox> mailboxes,
    params PXSyncFolderSpecification[] folders)
    where T : BaseFolderType, new()
  {
    Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
    List<PXSyncMailbox> list = mailboxes.ToList<PXSyncMailbox>();
    foreach (List<PXSyncMailbox> pxSyncMailboxList in list.Batch<PXSyncMailbox, List<PXSyncMailbox>>(this.Gate.ProcessPackageSize, (Func<IEnumerable<PXSyncMailbox>, List<PXSyncMailbox>>) (item => item.ToList<PXSyncMailbox>())))
    {
      foreach ((PXOperationResult<int> pxOperationResult, PXSyncMailbox pxSyncMailbox) in this.Gate.GetUsersTimeZonesBiases(pxSyncMailboxList.Select<PXSyncMailbox, string>((Func<PXSyncMailbox, string>) (mbox => mbox.Address))).Zip<PXOperationResult<int>, PXSyncMailbox, (PXOperationResult<int>, PXSyncMailbox)>((IEnumerable<PXSyncMailbox>) pxSyncMailboxList, (Func<PXOperationResult<int>, PXSyncMailbox, (PXOperationResult<int>, PXSyncMailbox)>) ((b, m) => (b, m))))
      {
        try
        {
          if (pxOperationResult.Success)
          {
            if (pxSyncMailbox.Reinitialize || !pxSyncMailbox.ExportPreset.Date.HasValue && !pxSyncMailbox.ImportPreset.Date.HasValue)
            {
              CategoryColor result;
              if (!Enum.TryParse<CategoryColor>(this.Policy.Color, out result))
                result = (CategoryColor) 15;
              this.Gate.EnsureCategory(pxSyncMailbox.Address, this.Policy.Category, result, (CategoryKeyboardShortcut) 0);
            }
            pxSyncMailbox.ExchangeTimeZone = PXTimeZoneInfo.FindSystemTimeZoneByOffset((double) (-pxOperationResult.Result * 60)).Id;
          }
          else
            ExceptionDispatchInfo.Capture(pxOperationResult.Error).Throw();
        }
        catch (Exception ex)
        {
          List<string> stringList;
          if (!errors.TryGetValue(pxSyncMailbox.Address, out stringList))
            errors[pxSyncMailbox.Address] = stringList = new List<string>();
          string errorMessage = this.Provider.CreateErrorMessage(true, PXMessages.LocalizeFormatNoPrefix("An error has occured during processing '{0}' mailbox.", new object[1]
          {
            (object) pxSyncMailbox.Address
          }), (string) null, ex, (string[]) null);
          this.Provider.LogError(pxSyncMailbox.Address, ex, errorMessage);
          stringList.Add(errorMessage);
        }
      }
    }
    foreach (PXOperationResult<PXExchangeFolderID> ensureFolder in this.Gate.EnsureFolders<T>(list.SelectMany<PXSyncMailbox, PXExchangeFolderDefinition>((Func<PXSyncMailbox, IEnumerable<PXExchangeFolderDefinition>>) (mailbox =>
    {
      bool flag1 = ((IEnumerable<PXSyncFolderSpecification>) folders).Count<PXSyncFolderSpecification>((Func<PXSyncFolderSpecification, bool>) (f => !string.IsNullOrEmpty(f.Name))) == 1;
      bool flag2 = ((IEnumerable<PXSyncFolderSpecification>) folders).Count<PXSyncFolderSpecification>((Func<PXSyncFolderSpecification, bool>) (f => !string.IsNullOrEmpty(f.Name) && ((Enum) (object) f.Direction).HasFlag((Enum) (object) (PXEmailSyncDirection.Directions) 1))) == 1;
      bool flag3 = ((IEnumerable<PXSyncFolderSpecification>) folders).Count<PXSyncFolderSpecification>((Func<PXSyncFolderSpecification, bool>) (f => !string.IsNullOrEmpty(f.Name) && ((Enum) (object) f.Direction).HasFlag((Enum) (object) (PXEmailSyncDirection.Directions) 2))) == 1;
      List<PXExchangeFolderDefinition> folderDefinitionList = new List<PXExchangeFolderDefinition>();
      if (!errors.ContainsKey(mailbox.Address))
      {
        foreach (PXSyncFolderSpecification folder in folders)
        {
          if (string.IsNullOrEmpty(folder.Name))
            folderDefinitionList.Add(new PXExchangeFolderDefinition(mailbox.Address, folder.Type, folder.Name, (string) null, (object) Tuple.Create<PXSyncMailbox, PXSyncFolderSpecification>(mailbox, folder)));
          else if (flag1)
            folderDefinitionList.Add(new PXExchangeFolderDefinition(mailbox.Address, folder.Type, folder.Name, mailbox.ImportPreset.FolderID, (object) Tuple.Create<PXSyncMailbox, PXSyncFolderSpecification>(mailbox, folder)));
          else if (flag2 && ((Enum) (object) folder.Direction).HasFlag((Enum) (object) (PXEmailSyncDirection.Directions) 1))
            folderDefinitionList.Add(new PXExchangeFolderDefinition(mailbox.Address, folder.Type, folder.Name, mailbox.ImportPreset.FolderID, (object) Tuple.Create<PXSyncMailbox, PXSyncFolderSpecification>(mailbox, folder)));
          else if (flag3 && ((Enum) (object) folder.Direction).HasFlag((Enum) (object) (PXEmailSyncDirection.Directions) 2))
            folderDefinitionList.Add(new PXExchangeFolderDefinition(mailbox.Address, folder.Type, folder.Name, mailbox.ExportPreset.FolderID, (object) Tuple.Create<PXSyncMailbox, PXSyncFolderSpecification>(mailbox, folder)));
          else
            folderDefinitionList.Add(new PXExchangeFolderDefinition(mailbox.Address, folder.Type, folder.Name, (string) null, (object) Tuple.Create<PXSyncMailbox, PXSyncFolderSpecification>(mailbox, folder)));
        }
      }
      return (IEnumerable<PXExchangeFolderDefinition>) folderDefinitionList;
    }))))
    {
      PXSyncMailbox pxSyncMailbox = ensureFolder.Tag is Tuple<PXSyncMailbox, PXSyncFolderSpecification> tag1 ? tag1.Item1 : (PXSyncMailbox) null;
      PXSyncFolderSpecification folderSpecification = ensureFolder.Tag is Tuple<PXSyncMailbox, PXSyncFolderSpecification> tag2 ? tag2.Item2 : (PXSyncFolderSpecification) null;
      if (folderSpecification != null && pxSyncMailbox != null)
      {
        string str = folderSpecification.Type.ToString();
        if (!string.IsNullOrEmpty(folderSpecification.Name))
          str = $"{str}\\{folderSpecification.Name}";
        if (ensureFolder.Success)
        {
          PXExchangeFolderID result = ensureFolder.Result;
          List<PXSyncMovingCondition> syncMovingConditionList = new List<PXSyncMovingCondition>();
          foreach (PXSyncMovingCondition syncMovingCondition in folderSpecification.MoveTo)
          {
            if (!string.IsNullOrEmpty(folderSpecification.Name))
            {
              PXOperationResult<PXExchangeFolderID> pxOperationResult = this.Gate.EnsureFolders<T>(new PXExchangeFolderDefinition[1]
              {
                new PXExchangeFolderDefinition(pxSyncMailbox.Address, syncMovingCondition.ParentFolder, (string) null, (string) null, (object) null)
              }).FirstOrDefault<PXOperationResult<PXExchangeFolderID>>();
              if (pxOperationResult?.Result != null && !(pxOperationResult.Result.FolderID is FolderIdType))
                Tools.AddIfNotEmpty<PXSyncMovingCondition>(syncMovingConditionList, new PXSyncMovingCondition[1]
                {
                  syncMovingCondition.InitialiseFolder(pxOperationResult.Result.FolderID)
                });
            }
          }
          pxSyncMailbox.Folders.Add(new PXSyncDirectFolder(((PXExchangeItemBase) result).Address, result.FolderID, folderSpecification.Direction, folderSpecification.Categorized, syncMovingConditionList.Count > 0 ? syncMovingConditionList.ToArray() : (PXSyncMovingCondition[]) null));
          if (result.NeedUpdate)
          {
            FolderIdType folderId = result.FolderID as FolderIdType;
            this.SetSyncAccount((PXGraph) this.graph, pxSyncMailbox.EmailAccountID, this.OperationCode, folderSpecification.Direction, new DateTime?(), folderId?.Id, new bool?());
          }
        }
        else
        {
          List<string> stringList;
          if (!errors.TryGetValue(pxSyncMailbox.Address, out stringList))
            errors[pxSyncMailbox.Address] = stringList = new List<string>();
          string errorMessage = this.Provider.CreateErrorMessage(true, PXMessages.LocalizeFormatNoPrefix("An error has occurred during folder '{0}' initialization for '{1}' mailbox.", new object[2]
          {
            (object) str,
            (object) pxSyncMailbox.Address
          }), (string) null, ensureFolder.Error, (string[]) null);
          this.Provider.LogError(pxSyncMailbox.Address, ensureFolder.Error, errorMessage);
          stringList.Add(errorMessage);
        }
      }
    }
    if (errors.Count > 0)
      throw new PXExchangeSyncItemsException(errors);
  }

  protected void DeleteReference(string address, Guid? noteID)
  {
    if (!noteID.HasValue)
      throw new PXException("Note id cannot be null.");
    bool flag = false;
    using (IEnumerator<PXDataRecord> enumerator = PXDatabase.SelectMulti<EMailSyncReference>(new PXDataField[4]
    {
      (PXDataField) new PXDataField<EMailSyncReference.serverID>(),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.serverID>((object) this.Account.AccountID),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.address>((object) address),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.noteID>((object) noteID)
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXDataRecord current = enumerator.Current;
        flag = true;
      }
    }
    if (!flag)
      return;
    PXDatabase.Delete<EMailSyncReference>(new List<PXDataFieldRestrict>()
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<EMailSyncReference.serverID>((object) this.Account.AccountID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<EMailSyncReference.address>((object) address),
      (PXDataFieldRestrict) new PXDataFieldRestrict<EMailSyncReference.noteID>((object) noteID)
    }.ToArray());
  }

  protected void SaveReference(
    string address,
    Guid? noteID,
    ItemIdType itemID,
    ItemIdType conversationID,
    string hash,
    bool? isSynchronized)
  {
    if (!noteID.HasValue)
      throw new PXException("Note id cannot be null.");
    bool flag = false;
    using (IEnumerator<PXDataRecord> enumerator = PXDatabase.SelectMulti<EMailSyncReference>(new PXDataField[4]
    {
      (PXDataField) new PXDataField<EMailSyncReference.binaryReference>(),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.serverID>((object) this.Account.AccountID),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.address>((object) address),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.noteID>((object) noteID)
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXDataRecord current = enumerator.Current;
        flag = true;
      }
    }
    if (flag)
    {
      List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<EMailSyncReference.binaryReference>(itemID != null ? (object) Encoding.UTF8.GetBytes(itemID.Id) : (object) new byte[0]));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<EMailSyncReference.binaryChangeKey>(itemID != null ? (object) Encoding.UTF8.GetBytes(itemID.ChangeKey) : (object) new byte[0]));
      if (hash != null)
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<EMailSyncReference.hash>((object) hash));
      if (itemID == null || conversationID != null)
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<EMailSyncReference.conversation>((object) conversationID?.Id));
      if (isSynchronized.HasValue)
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<EMailSyncReference.isSynchronized>((object) isSynchronized));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<EMailSyncReference.serverID>((object) this.Account.AccountID));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<EMailSyncReference.address>((object) address));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<EMailSyncReference.noteID>((object) noteID));
      PXDatabase.Update<EMailSyncReference>(pxDataFieldParamList.ToArray());
    }
    else
      PXDatabase.Insert<EMailSyncReference>(new List<PXDataFieldAssign>()
      {
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.binaryReference>(itemID != null ? (object) Encoding.UTF8.GetBytes(itemID.Id) : (object) new byte[0]),
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.binaryChangeKey>(itemID != null ? (object) Encoding.UTF8.GetBytes(itemID.ChangeKey) : (object) new byte[0]),
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.conversation>((object) conversationID?.Id),
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.hash>((object) hash),
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.serverID>((object) this.Account.AccountID),
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.address>((object) address),
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.noteID>((object) noteID),
        (PXDataFieldAssign) new PXDataFieldAssign<EMailSyncReference.isSynchronized>((object) isSynchronized)
      }.ToArray());
  }

  protected bool ValidateItemHash(PXExchangeItem<ExchangeType> item)
  {
    if (item == null)
      throw new PXException("Note id cannot be null.");
    if (DateTime.Now.Ticks > 0L)
      return false;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EMailSyncReference>(new PXDataField[5]
    {
      (PXDataField) new PXDataField<EMailSyncReference.noteID>(),
      (PXDataField) new PXDataField<EMailSyncReference.hash>(),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.serverID>((object) this.Account.AccountID),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.address>((object) ((PXExchangeItemBase) item).Address),
      (PXDataField) new PXDataFieldValue<EMailSyncReference.binaryReference>((object) item.Item.ItemId.Id)
    }))
    {
      if (pxDataRecord == null)
        return false;
      EMailSyncReference emailSyncReference = new EMailSyncReference();
      emailSyncReference.ServerID = this.Account.AccountID;
      emailSyncReference.Address = ((PXExchangeItemBase) item).Address;
      emailSyncReference.BinaryReference = item.Item.ItemId.Id;
      emailSyncReference.NoteID = pxDataRecord.GetGuid(0);
      emailSyncReference.Hash = pxDataRecord.GetString(1);
      bool flag = emailSyncReference.NoteID.HasValue && emailSyncReference.Hash != null && item.Hash == emailSyncReference.Hash;
      if (flag)
        this.Provider.LogInfo(((PXExchangeItemBase) item).Address, "Skipping import of item '{0}' due to item hash is not changed since last import.", (object) item.Item.Subject);
      return flag;
    }
  }

  protected virtual IEnumerable<PXExchangeItem<ExchangeType>> GetItems(
    IEnumerable<PXSyncMailbox> mailboxes,
    PXSyncItemSet exported,
    PXExchangeFindOptions options,
    params Tuple<string, MapiPropertyTypeType>[] extFields)
  {
    ExchangeBaseLogicSyncCommand<GraphType, TPrimary, ExchangeType> logicSyncCommand = this;
    Func<PXExchangeItem<ExchangeType>, PXExchangeRequest<ExchangeType, ExchangeType>> converter = (Func<PXExchangeItem<ExchangeType>, PXExchangeRequest<ExchangeType, ExchangeType>>) (i =>
    {
      PXExchangeFolderID exchangeFolderId = new PXExchangeFolderID(((PXExchangeItemBase) i).Address, (BaseFolderIdType) i.Item.ParentFolderId, false);
      ExchangeType exchangeType = new ExchangeType();
      exchangeType.ItemId = i.Item.ItemId;
      exchangeType.Categories = i.Item.Categories;
      string str = Guid.NewGuid().ToString();
      AttachmentType[] attachments = i.Attachments;
      return new PXExchangeRequest<ExchangeType, ExchangeType>(exchangeFolderId, exchangeType, str, (object) null, attachments);
    });
    HashSet<string> ids = exported != null ? exported.ToHashSet() : new HashSet<string>();
    options = logicSyncCommand.DefFindOptions | options;
    bool[] flagArray = new bool[2]{ true, false };
    for (int index = 0; index < flagArray.Length; ++index)
    {
      bool categorized = flagArray[index];
      DateTime? date = logicSyncCommand.GetDateTime((PXEmailSyncDirection.Directions) 1, mailboxes);
      PXSyncDirectFolder[] folders = mailboxes.SelectMany<PXSyncMailbox, PXSyncDirectFolder>((Func<PXSyncMailbox, IEnumerable<PXSyncDirectFolder>>) (m => m.Folders.Where<PXSyncDirectFolder>((Func<PXSyncDirectFolder, bool>) (f => f.Categorized == categorized && f.IsImport)))).ToArray<PXSyncDirectFolder>();
      if (folders != null && folders.Length != 0)
      {
        List<PXExchangeRequest<ExchangeType, ExchangeType>> toCategorizing = new List<PXExchangeRequest<ExchangeType, ExchangeType>>();
        BasePathToElementType[] extPaths = extFields == null || extFields.Length == 0 ? (BasePathToElementType[]) null : (BasePathToElementType[]) ((IEnumerable<Tuple<string, MapiPropertyTypeType>>) extFields).Select<Tuple<string, MapiPropertyTypeType>, PathToExtendedFieldType>((Func<Tuple<string, MapiPropertyTypeType>, PathToExtendedFieldType>) (e => new PathToExtendedFieldType()
        {
          PropertyTag = e.Item1,
          PropertyType = e.Item2
        })).ToArray<PathToExtendedFieldType>();
        foreach (PXExchangeItem<ExchangeType> pxExchangeItem in logicSyncCommand.Gate.FindItems<ExchangeType>((PXExchangeFolderID[]) folders, (PXExchangeFindOptions) (3 | options), categorized ? logicSyncCommand.Policy.Category : (string) null, date, extPaths, ids))
        {
          if ((ids == null || !ids.Contains(pxExchangeItem.Item.ItemId.Id)) && !logicSyncCommand.ValidateItemHash(pxExchangeItem))
          {
            if (!categorized && (options & 256 /*0x0100*/) != 256 /*0x0100*/ && (pxExchangeItem.Item.Categories == null || !((IEnumerable<string>) pxExchangeItem.Item.Categories).Contains<string>(logicSyncCommand.Policy.Category)))
            {
              toCategorizing.Add(converter(pxExchangeItem));
              logicSyncCommand.Provider.LogInfo(((PXExchangeItemBase) pxExchangeItem).Address, "Item '{0}' has been processed and will be marked with category {1}.", (object) pxExchangeItem.Item.Subject, (object) logicSyncCommand.Policy.Category);
            }
            ids.Add(pxExchangeItem.Item.ItemId.Id);
            yield return pxExchangeItem;
          }
        }
        if (((Enum) (object) options).HasFlag((Enum) (object) (PXExchangeFindOptions) 4) && date.HasValue)
        {
          foreach (PXExchangeItem<ExchangeType> pxExchangeItem in logicSyncCommand.Gate.FindItems<ExchangeType>((PXExchangeFolderID[]) folders, (PXExchangeFindOptions) (1 | options), (string) null, date, extPaths, ids))
          {
            if ((ids == null || !ids.Contains(pxExchangeItem.Item.ItemId.Id)) && !ids.Contains(pxExchangeItem.Item.ItemId.Id))
            {
              if ((options & 256 /*0x0100*/) != 256 /*0x0100*/ && (pxExchangeItem.Item.Categories == null || !((IEnumerable<string>) pxExchangeItem.Item.Categories).Contains<string>(logicSyncCommand.Policy.Category)))
              {
                toCategorizing.Add(converter(pxExchangeItem));
                logicSyncCommand.Provider.LogInfo(((PXExchangeItemBase) pxExchangeItem).Address, "Item '{0}' has been processed and will be marked with category {1}.", (object) pxExchangeItem.Item.Subject, (object) logicSyncCommand.Policy.Category);
              }
              ids.Add(pxExchangeItem.Item.ItemId.Id);
              yield return pxExchangeItem;
            }
          }
        }
        if (toCategorizing.Count > 0)
        {
          logicSyncCommand.Provider.LogVerbose((string) null, "Marking prepared items with category.");
          logicSyncCommand.Gate.CategorizeItems<ExchangeType>(logicSyncCommand.Policy.Category, toCategorizing.ToArray()).ToArray<PXExchangeResponce<ExchangeType>>();
        }
        date = new DateTime?();
        folders = (PXSyncDirectFolder[]) null;
        toCategorizing = (List<PXExchangeRequest<ExchangeType, ExchangeType>>) null;
        extPaths = (BasePathToElementType[]) null;
      }
    }
    flagArray = (bool[]) null;
  }

  protected virtual BqlCommand PrepareBqlCommand<TNote>(
    BqlCommand bqlCommand,
    PXSyncItemStatus status)
    where TNote : IBqlField
  {
    bqlCommand = BqlCommand.AppendJoin<LeftJoin<EMailSyncReference, On<EMailSyncReference.noteID, Equal<TNote>, And<EMailSyncReference.serverID, Equal<Required<EMailSyncReference.serverID>>, And<EMailSyncReference.address, Equal<Required<EMailSyncReference.address>>>>>>>(bqlCommand);
    bqlCommand = BqlCommand.AppendJoin<LeftJoin<SyncTimeTag, On<SyncTimeTag.noteID, Equal<TNote>>>>(bqlCommand);
    switch (status)
    {
      case PXSyncItemStatus.Inserted:
        bqlCommand = bqlCommand.WhereAnd<Where<EMailSyncReference.binaryReference, IsNull>>();
        break;
      case PXSyncItemStatus.Updated:
        bqlCommand = bqlCommand.WhereAnd<Where2<Where<SyncTimeTag.timeTag, GreaterEqual<Required<SyncTimeTag.timeTag>>, Or<EMailSyncReference.isSynchronized, Equal<False>>>, And<EMailSyncReference.binaryReference, IsNotNull>>>();
        break;
      case PXSyncItemStatus.Unsynced:
        bqlCommand = bqlCommand.WhereAnd<Where<EMailSyncReference.isSynchronized, Equal<False>, And<EMailSyncReference.binaryReference, IsNotNull>>>();
        break;
      case PXSyncItemStatus.Deleted:
        bqlCommand = bqlCommand.WhereAnd<Where<EMailSyncReference.binaryReference, NotEqual<Empty>>>();
        if (typeof (TPrimary) != typeof (Contact))
        {
          System.Type nestedType = typeof (TPrimary).GetNestedType(typeof (CRActivity.deletedDatabaseRecord).Name);
          bqlCommand = bqlCommand.WhereAnd(BqlCommand.Compose(new System.Type[4]
          {
            typeof (Where<,>),
            nestedType,
            typeof (Equal<>),
            typeof (True)
          }));
          break;
        }
        break;
    }
    return bqlCommand;
  }

  protected virtual List<Func<PXSyncMailbox, object>> PrepareParameters(BqlCommand bqlCommand)
  {
    List<Func<PXSyncMailbox, object>> funcList = new List<Func<PXSyncMailbox, object>>();
    foreach (IBqlParameter parameter in bqlCommand.GetParameters())
    {
      System.Type referencedType = parameter.GetReferencedType();
      if (string.Equals(referencedType.Name, "ServerID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Account.AccountID));
      else if (string.Equals(referencedType.Name, "Address", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) m.Address));
      else if (string.Equals(referencedType.Name, "TimeTag", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.GetDateTime((PXEmailSyncDirection.Directions) 2, m)));
      else if (string.Equals(referencedType.Name, "Owner", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Cache.EmployeeCache[m.EmployeeID]));
      else if (string.Equals(referencedType.Name, "OwnerID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Cache.EmployeeCache[m.EmployeeID]));
      else if (string.Equals(referencedType.Name, "User", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Cache.EmployeeCache[m.EmployeeID]));
      else if (string.Equals(referencedType.Name, "UserID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Cache.EmployeeCache[m.EmployeeID]));
      else if (string.Equals(referencedType.Name, "ContactID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Cache.EmployeeCache[m.EmployeeID]));
      else if (string.Equals(referencedType.Name, "DefContactID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Cache.EmployeeCache[m.EmployeeID]));
      else if (string.Equals(referencedType.Name, "WorkgroupID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) this.Cache.EmployeeCache[m.EmployeeID]));
      else if (string.Equals(referencedType.Name, "BAccountID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) m.EmployeeID));
      else if (string.Equals(referencedType.Name, "EmailAccountID", StringComparison.InvariantCultureIgnoreCase))
        funcList.Add((Func<PXSyncMailbox, object>) (m => (object) m.EmailAccountID));
    }
    return funcList;
  }

  protected virtual IEnumerable<PXSyncItemBucket<TPrimary>> SelectItems<TNote>(
    PXGraph graph,
    BqlCommand bqlCommand,
    IEnumerable<PXSyncMailbox> mailboxes,
    PXSyncItemSet exceptionSet,
    PXSyncItemStatus status,
    bool withAttachments)
    where TNote : IBqlField
  {
    return (IEnumerable<PXSyncItemBucket<TPrimary>>) this.SelectItems<TNote, IBqlTable>(graph, bqlCommand, mailboxes, exceptionSet, status, withAttachments);
  }

  protected virtual IEnumerable<PXSyncItemBucket<TPrimary, T2>> SelectItems<TNote, T2>(
    PXGraph graph,
    BqlCommand bqlCommand,
    IEnumerable<PXSyncMailbox> mailboxes,
    PXSyncItemSet exceptionSet,
    PXSyncItemStatus status,
    bool withAttachments)
    where TNote : IBqlField
    where T2 : IBqlTable
  {
    return (IEnumerable<PXSyncItemBucket<TPrimary, T2>>) this.SelectItems<TNote, T2, IBqlTable>(graph, bqlCommand, mailboxes, exceptionSet, status, withAttachments);
  }

  protected virtual IEnumerable<PXSyncItemBucket<TPrimary, T2, T3>> SelectItems<TNote, T2, T3>(
    PXGraph graph,
    BqlCommand bqlCommand,
    IEnumerable<PXSyncMailbox> mailboxes,
    PXSyncItemSet exceptionSet,
    PXSyncItemStatus status,
    bool withAttachments)
    where TNote : IBqlField
    where T2 : IBqlTable
    where T3 : IBqlTable
  {
    ExchangeBaseLogicSyncCommand<GraphType, TPrimary, ExchangeType> logicSyncCommand = this;
    PXCache cache = graph.Caches[typeof (TPrimary)];
    Func<TPrimary, Guid?> getNote = (Func<TPrimary, Guid?>) (row => PXNoteAttribute.GetNoteID(cache, (object) row, (string) null));
    bqlCommand = logicSyncCommand.PrepareBqlCommand<TNote>(bqlCommand, status);
    if (bqlCommand != null)
    {
      foreach (PXSyncItemBucket<TPrimary, T2, T3> selectItem in logicSyncCommand.SelectItems<T2, T3>(graph, bqlCommand, mailboxes, exceptionSet, status))
      {
        TPrimary primary = selectItem.Item1;
        EMailSyncReference reference = selectItem.Reference;
        List<UploadFileWithData> uploadFileWithDataList = new List<UploadFileWithData>();
        if (withAttachments && selectItem.Status != PXSyncItemStatus.Deleted)
        {
          Guid? nullable = getNote(primary);
          if (nullable.HasValue)
          {
            foreach (PXResult<UploadFileWithData> pxResult in PXSelectBase<UploadFileWithData, PXSelect<UploadFileWithData, Where<UploadFileWithData.noteID, Equal<Required<UploadFileWithData.noteID>>>>.Config>.Select(graph, new object[1]
            {
              (object) nullable
            }))
            {
              UploadFileWithData uploadFileWithData = PXResult<UploadFileWithData>.op_Implicit(pxResult);
              if (uploadFileWithData.Data != null)
              {
                int num1 = 20971520 /*0x01400000*/;
                int? syncAttachmentSize = logicSyncCommand.Account.SyncAttachmentSize;
                if (syncAttachmentSize.HasValue)
                {
                  syncAttachmentSize = logicSyncCommand.Account.SyncAttachmentSize;
                  int num2 = 0;
                  if (syncAttachmentSize.GetValueOrDefault() > num2 & syncAttachmentSize.HasValue)
                  {
                    syncAttachmentSize = logicSyncCommand.Account.SyncAttachmentSize;
                    num1 = syncAttachmentSize.Value * 1024 /*0x0400*/;
                  }
                }
                if (uploadFileWithData.Data.Length > num1)
                  logicSyncCommand.Provider.LogWarning(selectItem.Mailbox.Address, "The size of the attachment '{0}' is bigger than '{1}' bytes; the attachment will be skipped.", (object) uploadFileWithData.Name, (object) logicSyncCommand.Account.SyncAttachmentSize);
                else
                  uploadFileWithDataList.Add(uploadFileWithData);
              }
            }
          }
        }
        if (withAttachments && uploadFileWithDataList.Count > 0)
          selectItem.Attachments = uploadFileWithDataList.ToArray();
        yield return selectItem;
      }
    }
  }

  protected virtual IEnumerable<PXSyncItemBucket<TPrimary, T2, T3>> SelectItems<T2, T3>(
    PXGraph graph,
    BqlCommand bqlCommand,
    IEnumerable<PXSyncMailbox> mailboxes,
    PXSyncItemSet exceptionSet,
    PXSyncItemStatus status)
    where T2 : IBqlTable
    where T3 : IBqlTable
  {
    PXCache cache = graph.Caches[typeof (TPrimary)];
    Func<TPrimary, bool> getSynced = (Func<TPrimary, bool>) (row =>
    {
      object obj = cache.GetValue((object) row, "Synchronize");
      return obj != null && obj is bool flag2 && flag2;
    });
    HashSet<string> ids = exceptionSet != null ? exceptionSet.ToHashSet() : new HashSet<string>();
    List<Func<PXSyncMailbox, object>> args = this.PrepareParameters(bqlCommand);
    foreach (PXSyncMailbox mailbox1 in mailboxes)
    {
      PXSyncMailbox mailbox = mailbox1;
      object[] arguments = args.Select<Func<PXSyncMailbox, object>, object>((Func<Func<PXSyncMailbox, object>, object>) (a => a(mailbox))).ToArray<object>();
      if ((status & PXSyncItemStatus.Inserted) == PXSyncItemStatus.Inserted || (status & PXSyncItemStatus.Updated) == PXSyncItemStatus.Updated)
      {
        foreach (PXResult pxResult in this.ExecuteSelect(mailbox, new PXView(graph, true, bqlCommand), arguments))
        {
          TPrimary primary = pxResult.GetItem<TPrimary>();
          EMailSyncReference reference = pxResult.GetItem<EMailSyncReference>();
          if ((ids == null || !ids.Contains(reference.BinaryReference)) && getSynced(primary))
          {
            PXSyncItemStatus status1 = reference == null || string.IsNullOrEmpty(reference.BinaryReference) ? PXSyncItemStatus.Inserted : PXSyncItemStatus.Updated;
            if (status == PXSyncItemStatus.None || status == PXSyncItemStatus.Unsynced || (status1 & status) == status)
              yield return new PXSyncItemBucket<TPrimary, T2, T3>(mailbox, status1, reference, pxResult.GetItem<TPrimary>(), pxResult.GetItem<T2>(), pxResult.GetItem<T3>());
          }
        }
      }
      if ((status & PXSyncItemStatus.Deleted) == PXSyncItemStatus.Deleted)
      {
        using (new PXReadDeletedScope(typeof (TPrimary) != typeof (Contact)))
        {
          foreach (PXResult pxResult in this.ExecuteSelect(mailbox, new PXView(graph, true, bqlCommand), arguments))
          {
            TPrimary primary = pxResult.GetItem<TPrimary>();
            EMailSyncReference reference = pxResult.GetItem<EMailSyncReference>();
            if ((ids == null || !ids.Contains(reference.BinaryReference)) && getSynced(primary))
              yield return new PXSyncItemBucket<TPrimary, T2, T3>(mailbox, PXSyncItemStatus.Deleted, reference, pxResult.GetItem<TPrimary>(), pxResult.GetItem<T2>(), pxResult.GetItem<T3>());
          }
        }
      }
      arguments = (object[]) null;
    }
  }

  protected IEnumerable<PXResult> ExecuteSelect(
    PXSyncMailbox mailbox,
    PXView view,
    object[] arguments)
  {
    ExchangeBaseLogicSyncCommand<GraphType, TPrimary, ExchangeType> logicSyncCommand = this;
    view.Clear();
    try
    {
      view.SelectMulti(arguments);
    }
    catch (Exception ex)
    {
      logicSyncCommand.Provider.LogError(mailbox.Address, ex);
      logicSyncCommand.StoreError(mailbox.Address, ex);
      yield break;
    }
    foreach (PXResult pxResult in view.SelectMulti(arguments))
      yield return pxResult;
  }

  protected abstract BqlCommand GetSelectCommand();

  public override void ProcessSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes)
  {
    try
    {
      this.ConfigureEnvironment(direction, mailboxes);
    }
    catch (PXExchangeSyncItemsException ex)
    {
      if (ex.Errors == null || ex.Errors.Count <= 0)
        throw;
      this.MergeErrors(ex.Errors);
    }
    try
    {
      List<PXSyncMailbox> pxSyncMailboxList = new List<PXSyncMailbox>();
      foreach (PXSyncMailbox mailbox in mailboxes)
      {
        if (!this.Errors.ContainsKey(mailbox.Address))
          pxSyncMailboxList.Add(mailbox);
      }
      this.PrepareSync(policy, direction, pxSyncMailboxList.ToArray());
    }
    catch (PXExchangeSyncItemsException ex)
    {
      if (ex.Errors == null || ex.Errors.Count <= 0)
        throw;
      this.MergeErrors(ex.Errors);
    }
    if (this.Errors.Count > 0)
      throw new PXExchangeSyncItemsException(this.Errors);
  }

  protected abstract void ExportImportFirst(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes);

  protected virtual void ExportFirst(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet excludes = null)
  {
    if (!((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
      return;
    List<PXSyncResult> pxSyncResultList = (List<PXSyncResult>) null;
    if ((direction & 2) == 2)
    {
      this.Provider.LogVerbose((string) null, "{0} operation for {1} has been started.", (object) (PXEmailSyncDirection.Directions) 2, (object) this.OperationCode);
      DateTime now = PXTimeZoneInfo.Now;
      pxSyncResultList = this.PrepareExport(mailboxes, excludes);
      this.PostSyncHandling((PXGraph) this.graph, this.OperationCode, (PXEmailSyncDirection.Directions) 2, now, (IEnumerable<PXSyncMailbox>) mailboxes, (IEnumerable<PXSyncResult>) pxSyncResultList);
      this.Provider.LogVerbose((string) null, "{0} operation for {1} has been finished.", (object) (PXEmailSyncDirection.Directions) 2, (object) this.OperationCode);
    }
    Thread.Sleep(500);
    if ((direction & 1) != 1)
      return;
    this.Provider.LogVerbose((string) null, "{0} operation for {1} has been started.", (object) (PXEmailSyncDirection.Directions) 1, (object) this.OperationCode);
    DateTime now1 = PXTimeZoneInfo.Now;
    List<PXSyncResult> processed = this.PrepareImport(mailboxes, new PXSyncItemSet((IEnumerable<PXSyncItemID>) pxSyncResultList));
    this.PostSyncHandling((PXGraph) this.graph, this.OperationCode, (PXEmailSyncDirection.Directions) 1, now1, (IEnumerable<PXSyncMailbox>) mailboxes, (IEnumerable<PXSyncResult>) processed);
    this.Provider.LogVerbose((string) null, "{0} operation for {1} has been finished.", (object) (PXEmailSyncDirection.Directions) 1, (object) this.OperationCode);
  }

  protected virtual void ImportFirst(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet excludes = null)
  {
    if (!((IEnumerable<PXSyncMailbox>) mailboxes).Any<PXSyncMailbox>())
      return;
    List<PXSyncResult> pxSyncResultList = (List<PXSyncResult>) null;
    if ((direction & 1) == 1)
    {
      this.Provider.LogVerbose((string) null, "{0} operation for {1} has been started.", (object) (PXEmailSyncDirection.Directions) 1, (object) this.OperationCode);
      DateTime now = PXTimeZoneInfo.Now;
      pxSyncResultList = this.PrepareImport(mailboxes, excludes);
      this.PostSyncHandling((PXGraph) this.graph, this.OperationCode, (PXEmailSyncDirection.Directions) 1, now, (IEnumerable<PXSyncMailbox>) mailboxes, (IEnumerable<PXSyncResult>) pxSyncResultList);
      this.Provider.LogVerbose((string) null, "{0} operation for {1} has been finished.", (object) (PXEmailSyncDirection.Directions) 1, (object) this.OperationCode);
    }
    Thread.Sleep(500);
    if ((direction & 2) != 2)
      return;
    this.Provider.LogVerbose((string) null, "{0} operation for {1} has been started.", (object) (PXEmailSyncDirection.Directions) 2, (object) this.OperationCode);
    DateTime now1 = PXTimeZoneInfo.Now;
    List<PXSyncResult> processed = this.PrepareExport(mailboxes, new PXSyncItemSet((IEnumerable<PXSyncItemID>) pxSyncResultList));
    this.PostSyncHandling((PXGraph) this.graph, this.OperationCode, (PXEmailSyncDirection.Directions) 2, now1, (IEnumerable<PXSyncMailbox>) mailboxes, (IEnumerable<PXSyncResult>) processed);
    this.Provider.LogVerbose((string) null, "{0} operation for {1} has been finished.", (object) (PXEmailSyncDirection.Directions) 2, (object) this.OperationCode);
  }

  protected abstract void LastUpdated(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes);

  protected abstract void KeepBoth(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes);

  protected virtual void PrepareSync(
    EMailSyncPolicy policy,
    PXEmailSyncDirection.Directions direction,
    PXSyncMailbox[] mailboxes)
  {
    switch (policy.Priority)
    {
      case "E":
        this.ImportFirst(policy, direction, mailboxes);
        break;
      case "L":
        this.ImportFirst(policy, direction, ((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (_ => _.IsReset)).ToArray<PXSyncMailbox>());
        this.LastUpdated(policy, direction, ((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (_ => !_.IsReset)).ToArray<PXSyncMailbox>());
        break;
      case "B":
        this.ImportFirst(policy, direction, ((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (_ => _.IsReset)).ToArray<PXSyncMailbox>());
        this.KeepBoth(policy, direction, ((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (_ => !_.IsReset)).ToArray<PXSyncMailbox>());
        break;
      default:
        this.ImportFirst(policy, direction, ((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (_ => _.IsReset)).ToArray<PXSyncMailbox>());
        this.ExportImportFirst(policy, direction, ((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (_ => !_.IsReset)).ToArray<PXSyncMailbox>());
        break;
    }
  }

  protected abstract void ConfigureEnvironment(
    PXEmailSyncDirection.Directions direction,
    IEnumerable<PXSyncMailbox> mailboxes);

  protected virtual List<PXSyncResult> PrepareExport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet imported = null)
  {
    List<PXSyncResult> pxSyncResultList = new List<PXSyncResult>();
    pxSyncResultList.AddRange(this.ProcessSyncExport(((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => !m.ExportPreset.Date.HasValue)).ToArray<PXSyncMailbox>(), imported));
    pxSyncResultList.AddRange(this.ProcessSyncExport(((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => m.ExportPreset.Date.HasValue)).ToArray<PXSyncMailbox>(), imported));
    return pxSyncResultList;
  }

  protected virtual List<PXSyncResult> PrepareImport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported = null)
  {
    List<PXSyncResult> pxSyncResultList = new List<PXSyncResult>();
    pxSyncResultList.AddRange(this.ProcessSyncImport(((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => !m.ImportPreset.Date.HasValue)).ToArray<PXSyncMailbox>(), exported));
    pxSyncResultList.AddRange(this.ProcessSyncImport(((IEnumerable<PXSyncMailbox>) mailboxes).Where<PXSyncMailbox>((Func<PXSyncMailbox, bool>) (m => m.ImportPreset.Date.HasValue)).ToArray<PXSyncMailbox>(), exported));
    if (typeof (ExchangeType) != typeof (MessageType))
      pxSyncResultList.AddRange(this.ProcessSyncExportUnsynced(mailboxes, exported));
    return pxSyncResultList;
  }

  protected abstract IEnumerable<PXSyncResult> ProcessSyncExport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet imported);

  protected abstract IEnumerable<PXSyncResult> ProcessSyncExportUnsynced(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported);

  protected abstract IEnumerable<PXSyncResult> ProcessSyncImport(
    PXSyncMailbox[] mailboxes,
    PXSyncItemSet exported);

  protected virtual bool IsFullyFilled(ExchangeType item) => true;

  protected virtual bool SaveAttachmentsSync(
    PXGraph graph,
    TPrimary row,
    AttachmentType[] attachments)
  {
    return this.SaveAttachmentsSync(graph, row, attachments, out string _);
  }

  protected virtual bool SaveAttachmentsSync(
    PXGraph graph,
    TPrimary row,
    AttachmentType[] attachments,
    out string imgName)
  {
    imgName = (string) null;
    bool flag = false;
    if (attachments == null || attachments.Length == 0)
      return flag;
    List<Guid> guidList1 = new List<Guid>();
    foreach (AttachmentType attachment in attachments)
    {
      byte[] right = (byte[]) null;
      if (attachment is FileAttachmentType)
        right = ((FileAttachmentType) attachment).Content;
      Guid? uid;
      if (right != null)
      {
        Guid fileId;
        FileInfo file;
        if (this.TryParseAcumaticaContentId(attachment.ContentId, out fileId) && this.CheckFileExistance(fileId, out file) && this.CompareBinaryData(file.BinData, right))
        {
          List<Guid> guidList2 = guidList1;
          uid = file.UID;
          Guid guid = uid.Value;
          guidList2.Add(guid);
        }
        else
        {
          string attachmentName = this.GetAttachmentName(graph, row, attachment);
          if (!this.CheckFileExistance(attachmentName, out file) || !this.CompareBinaryData(file.BinData, right))
          {
            FileInfo fileInfo = new FileInfo(attachmentName, (string) null, right);
            flag = true;
            this.Uploader.SaveFile(fileInfo, (FileExistsAction) 1);
            uid = fileInfo.UID;
            if (uid.HasValue)
            {
              List<Guid> guidList3 = guidList1;
              uid = fileInfo.UID;
              Guid guid = uid.Value;
              guidList3.Add(guid);
            }
            if (attachment.ContentType != null && attachment.ContentType.Contains("image"))
              imgName = fileInfo.Name;
          }
        }
      }
    }
    if (guidList1.Count > 0)
      PXNoteAttribute.SetFileNotes(graph.Caches[row.GetType()], (object) row, guidList1.ToArray());
    return flag;
  }

  protected virtual AttachmentType[] ConvertAttachment(UploadFileWithData[] files, string photoName = null)
  {
    if (files == null || files.Length == 0)
      return (AttachmentType[]) null;
    AttachmentType[] attachmentTypeArray1 = new AttachmentType[files.Length];
    for (int index1 = 0; index1 < files.Length; ++index1)
    {
      UploadFileWithData file = files[index1];
      AttachmentType[] attachmentTypeArray2 = attachmentTypeArray1;
      int index2 = index1;
      FileAttachmentType fileAttachmentType = new FileAttachmentType();
      ((AttachmentType) fileAttachmentType).Name = this.GetAttachmentName(file.Name);
      ((AttachmentType) fileAttachmentType).Size = file.Size.GetValueOrDefault();
      ((AttachmentType) fileAttachmentType).SizeSpecified = true;
      fileAttachmentType.Content = file.Data;
      ((AttachmentType) fileAttachmentType).ContentId = this.ConvertToAcumaticaContentId(file.FileID);
      fileAttachmentType.IsContactPhoto = file.Name == photoName;
      fileAttachmentType.IsContactPhotoSpecified = true;
      attachmentTypeArray2[index2] = (AttachmentType) fileAttachmentType;
    }
    return attachmentTypeArray1;
  }

  protected string ConvertToAcumaticaContentId(Guid? fileId)
  {
    return "ac_file_id_" + fileId?.ToString("D");
  }

  protected bool TryParseAcumaticaContentId(string contentId, out Guid fileId)
  {
    contentId = contentId?.Trim();
    if (string.IsNullOrEmpty(contentId) || !contentId.StartsWith("ac_file_id_", StringComparison.InvariantCulture))
      return this.TryParseAcumaticaContentIdOld(contentId, out fileId);
    contentId = contentId.Substring("ac_file_id_".Length, contentId.Length - "ac_file_id_".Length);
    return Guid.TryParse(contentId, out fileId);
  }

  protected bool TryParseAcumaticaContentIdOld(string contentId, out Guid fileId)
  {
    if (string.IsNullOrEmpty(contentId) || !contentId.StartsWith("ac_file_id:", StringComparison.InvariantCulture))
    {
      fileId = new Guid();
      return false;
    }
    contentId = contentId.Substring("ac_file_id:".Length, contentId.Length - "ac_file_id:".Length);
    return Guid.TryParse(contentId, out fileId);
  }

  protected string GetAttachmentFileID(string name)
  {
    if (!name.StartsWith("~/Frames/GetFile.ashx?fileID="))
      return (string) null;
    name = HttpUtility.UrlDecode(name.Substring("~/Frames/GetFile.ashx?fileID=".Length));
    return name;
  }

  protected string GetAttachmentName(string name)
  {
    int num1 = name.LastIndexOf("\\", StringComparison.InvariantCultureIgnoreCase);
    if (num1 >= 0 && num1 < name.Length - 1)
      name = name.Substring(num1 + 1);
    int num2 = name.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase);
    if (num2 >= 0 && num2 < name.Length - 1)
      name = name.Substring(num2 + 1);
    return name;
  }

  protected string GetAttachmentName(PXGraph graph, TPrimary row, AttachmentType attachment)
  {
    string str1 = this.OperationCode;
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, graph.GetType());
    if (siteMapNode != null)
      str1 = siteMapNode.Title;
    string str2 = string.Join<object>(" ", ((IEnumerable<string>) graph.Caches[typeof (TPrimary)].Keys).Select<string, object>((Func<string, object>) (key => graph.Caches[typeof (TPrimary)].GetValue((object) row, key))).Where<object>((Func<object, bool>) (value => value != null)));
    string caption = $"{str1} ({str2})\\";
    List<string> source = new List<string>();
    if (graph.Caches[typeof (TPrimary)].GetValueExt((object) row, "NoteFiles") is PXNoteState valueExt && ((PXFieldState) valueExt).Value != null && ((PXFieldState) valueExt).Value is string[])
    {
      foreach (string str3 in ((PXFieldState) valueExt).Value as string[])
      {
        if (!string.IsNullOrEmpty(str3))
        {
          string[] strArray = str3.Split(new char[1]{ '$' }, StringSplitOptions.None);
          if (strArray.Length > 1)
            source.Add(strArray[1]);
        }
      }
    }
    if (!string.IsNullOrEmpty(attachment.ContentId) && attachment.ContentId.StartsWith(caption))
      return attachment.ContentId;
    if (!string.IsNullOrEmpty(attachment.Name) && attachment.Name.StartsWith(caption))
      return attachment.Name;
    string attachmentName = source.FirstOrDefault<string>((Func<string, bool>) (e => e == caption + attachment.ContentId || e == caption + attachment.Name));
    if (!string.IsNullOrEmpty(attachmentName))
      return attachmentName;
    string str4 = attachment.Name;
    int num = str4.LastIndexOf("\\");
    if (num >= 0 && num < str4.Length - 1)
      str4 = str4.Substring(num + 1);
    return caption + str4;
  }

  protected string GetAttachmentNameByContentId(string ContentId)
  {
    Guid fileId;
    FileInfo file;
    return this.TryParseAcumaticaContentId(ContentId, out fileId) && this.CheckFileExistance(fileId, out file) ? file.FullName : (string) null;
  }

  protected string ClearHtml(
    PXGraph graph,
    TPrimary row,
    string html,
    IEnumerable<AttachmentType> attachments)
  {
    if (string.IsNullOrEmpty(html))
      return html;
    List<AttachmentType> source = new List<AttachmentType>();
    if (attachments != null)
    {
      foreach (AttachmentType attachment in attachments)
        source.Add(attachment);
    }
    Regex regex1 = new Regex("(?<root><img.+?(?<src>src=[\"].+?[\"])[^>]*?>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    Regex regex2 = new Regex("(?<root><a.+?(?<href>href=[\"].+?[\"])[^>]*?>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    string input = html;
    foreach (Match m in ((IEnumerable<Match>) regex1.Matches(input).ToArray<Match>()).Concat<Match>((IEnumerable<Match>) regex2.Matches(html).ToArray<Match>()))
    {
      if (m.Success)
      {
        bool image;
        Group group1 = this.ExtractGroup(m, out image);
        Group group2 = m.Groups["root"];
        if (group1 != null && group1.Success)
        {
          string str1 = group1.Value;
          string str2 = str1.Substring(str1.IndexOf("\"", StringComparison.InvariantCultureIgnoreCase) + 1);
          string str3 = str2.Substring(0, str2.LastIndexOf("\"", StringComparison.InvariantCultureIgnoreCase));
          if (str3.StartsWith("cid:"))
          {
            AttachmentType attachment = (AttachmentType) null;
            string filename = str3.Replace("cid:", "");
            if (source.Count == 1)
              attachment = source[0];
            if (attachment == null && source.Any<AttachmentType>((Func<AttachmentType, bool>) (a => a.ContentId == filename)))
              attachment = source.First<AttachmentType>((Func<AttachmentType, bool>) (a => a.ContentId == filename));
            if (attachment != null)
            {
              filename = this.GetAttachmentNameByContentId(attachment.ContentId);
              if (string.IsNullOrEmpty(filename))
                filename = this.GetAttachmentName(graph, row, attachment);
            }
            string str4 = this.ParceHtmlTag(m.Value, filename, attachment != null, image);
            html = html.Replace(group2.Value, str4 ?? group2.Value);
          }
        }
      }
    }
    return html;
  }

  protected string ClearHtml(
    PXGraph graph,
    TPrimary row,
    string html,
    bool creation,
    IEnumerable<UploadFileWithData> attachments)
  {
    if (string.IsNullOrEmpty(html))
      return html;
    Regex regex1 = new Regex("(?<root><img.+?(?<src>src=[\"].+?[\"]).+?(?<exch>exchange=[\"].+?[\"])*?>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    Regex regex2 = new Regex("(?<root><a.+?(?<href>href=[\"].+?[\"]).+?(?<exch>exchange=[\"].+?[\"])*?>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
    string input = html;
    foreach (Match m in ((IEnumerable<Match>) regex1.Matches(input).ToArray<Match>()).Concat<Match>((IEnumerable<Match>) regex2.Matches(html).ToArray<Match>()))
    {
      if (m.Success)
      {
        bool image = false;
        Group group1 = this.ExtractGroup(m, out image);
        string src = (string) null;
        string oldValue = (string) null;
        if (image && group1 != null && group1.Success)
        {
          src = oldValue = group1.Value;
          src = src.Substring(src.IndexOf("\"", StringComparison.InvariantCultureIgnoreCase) + 1);
          src = src.Substring(0, src.LastIndexOf("\"", StringComparison.InvariantCultureIgnoreCase));
        }
        string newValue1 = (string) null;
        Group group2 = m.Groups["exch"];
        if (group2 != null && group2.Success)
        {
          string str1 = group2.Value;
          string str2 = str1.Substring(str1.IndexOf("\"", StringComparison.InvariantCultureIgnoreCase) + 1);
          newValue1 = str2.Substring(0, str2.LastIndexOf("\"", StringComparison.InvariantCultureIgnoreCase));
        }
        if (!string.IsNullOrEmpty(newValue1))
          html = html.Replace(group2.Value, string.Empty);
        if (!string.IsNullOrEmpty(src))
        {
          string filename = this.GetAttachmentFileID(src);
          UploadFileWithData uploadFileWithData;
          if (!string.IsNullOrEmpty(filename))
          {
            uploadFileWithData = attachments != null ? attachments.FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (f =>
            {
              Guid? fileId = f.FileID;
              ref Guid? local = ref fileId;
              return string.Equals(local.HasValue ? local.GetValueOrDefault().ToString() : (string) null, filename, StringComparison.InvariantCultureIgnoreCase);
            })) : (UploadFileWithData) null;
          }
          else
          {
            filename = this.GetAttachmentName(src);
            uploadFileWithData = attachments != null ? attachments.FirstOrDefault<UploadFileWithData>((Func<UploadFileWithData, bool>) (f => string.Equals(f.Name, src, StringComparison.InvariantCultureIgnoreCase))) : (UploadFileWithData) null;
          }
          if (creation)
          {
            if (uploadFileWithData != null)
            {
              string acumaticaContentId = this.ConvertToAcumaticaContentId(uploadFileWithData.FileID);
              string newValue2 = oldValue.Replace(src, "cid:" + acumaticaContentId);
              html = html.Replace(oldValue, newValue2);
              html = html.Replace(src, filename);
              uploadFileWithData.ContentID = acumaticaContentId;
            }
          }
          else if (!string.IsNullOrEmpty(newValue1))
          {
            html = html.Replace(src, newValue1);
            if (uploadFileWithData != null)
              uploadFileWithData.ContentID = newValue1.Replace("cid:", "");
          }
        }
      }
    }
    return html;
  }

  protected Group ExtractGroup(Match m, out bool image)
  {
    Group group1 = m.Groups["src"];
    Group group2 = m.Groups["href"];
    Group group3 = (Group) null;
    if (group1 != null && group1.Success)
      group3 = group1;
    if (group2 != null && group2.Success)
      group3 = group2;
    image = group3 == group1;
    return group3;
  }

  protected string ParceHtmlTag(string tag, string fileName, bool isFound, bool isImg)
  {
    int num = 0;
    ParsedOpeningTag parsedOpeningTag = ParsedOpeningTag.Parse(tag, ref num, "<", ">");
    if (parsedOpeningTag == null)
      return (string) null;
    parsedOpeningTag.SetAttribute("exchange", parsedOpeningTag.GetAttribute(isImg ? "src" : "href"));
    if (isFound)
    {
      parsedOpeningTag.SetAttribute("objtype", "file");
      parsedOpeningTag.SetAttribute("data-convert", "view");
      parsedOpeningTag.SetAttribute("embedded", "true");
      if (isImg)
        parsedOpeningTag.SetAttribute("src", fileName);
      else
        parsedOpeningTag.SetAttribute("href", fileName);
    }
    else if (isImg)
    {
      parsedOpeningTag.SetAttribute("src", "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAOCAYAAAAmL5yKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAADNSURBVDhPYwCC/7HVB/+Hl+3/7527+79z2vb/9slbcWLL2I3/DcNW/tcJWPwfpJchueHI/wcvP//XD9uAF8PUINNavnP/M6Q2HUFRSArW9J71nyG66uD/y/ffk4xBLtDwmvafIbh4H1jgy/ffJGGQAeruk/4zBBTuJdsANbcJw8IA/4I9FBjQ/58BlPrINUDVpec/g3vWTrINUHZo/s/glLodzCEHK9s3gfIDw3+ruA3/jcJXATPIov9aPnPASRQfVnXp/q/i2AbUzPAfAPWz01dc928mAAAAAElFTkSuQmCC");
    }
    else
    {
      parsedOpeningTag.SetAttribute("data-convert-error", "This link is broken. Please check attached files.");
      parsedOpeningTag.SetAttribute("href", "~/Frames/Error.aspx");
    }
    return ((ParsedTag) parsedOpeningTag).Value;
  }

  private bool CheckFileExistance(Guid fileId, out FileInfo file)
  {
    file = this.Uploader.GetFile(fileId);
    return file != null;
  }

  private bool CheckFileExistance(string fileName, out FileInfo file)
  {
    file = this.Uploader.GetFile(fileName);
    return file != null;
  }

  protected bool CompareBinaryData(byte[] left, byte[] right)
  {
    int? length1 = left?.Length;
    int? length2 = right?.Length;
    return length1.GetValueOrDefault() == length2.GetValueOrDefault() & length1.HasValue == length2.HasValue;
  }
}
