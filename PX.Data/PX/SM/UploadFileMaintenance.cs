// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden(ServiceVisible = true)]
public class UploadFileMaintenance : PXGraph<UploadFileMaintenance>
{
  public PXFilter<UploadFileFilter> Filter;
  public UploadFileMaintenance.PXSelectFile Files;
  public PXSelect<UploadFileRevision, Where<UploadFileRevision.fileID, Equal<Current<UploadFile.fileID>>>> Revisions;
  public PXSelect<NoteDoc, Where<NoteDoc.fileID, Equal<Required<NoteDoc.fileID>>>> FileNoteDocs;
  public PXSelect<NoteDoc, Where<NoteDoc.fileID, Equal<Required<NoteDoc.fileID>>, And<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>> FileNoteDoc;
  public PXSelect<NoteDoc> NoteDocs;
  public PXSelect<WikiFileInPage, Where<WikiFileInPage.fileID, Equal<Required<WikiFileInPage.fileID>>>> WikiFileLinks;
  private static readonly string AccessOverrideSessionPrefix = typeof (Note).FullName;
  [ThreadStatic]
  private static MACTripleDES hashMaker;
  public bool IgnoreFileRestrictions;

  private static string GetAccessOverrideSessionKey(Guid? noteId)
  {
    return $"{UploadFileMaintenance.AccessOverrideSessionPrefix}+{noteId}";
  }

  internal static bool IsAccessOverride(string key)
  {
    return key.StartsWith(UploadFileMaintenance.AccessOverrideSessionPrefix, StringComparison.OrdinalIgnoreCase);
  }

  [PXInternalUseOnly]
  public static void OverrideAccessRights(PXSessionState session, Guid? noteId)
  {
    session.SetString(UploadFileMaintenance.GetAccessOverrideSessionKey(noteId), noteId?.ToString());
  }

  [PXInternalUseOnly]
  public static bool OverrideAccessRights(UploadFile file)
  {
    bool? rightsFromEntities = file.IsAccessRightsFromEntities;
    bool flag = false;
    if (rightsFromEntities.GetValueOrDefault() == flag & rightsFromEntities.HasValue)
      return false;
    foreach (PXResult<NoteDoc> pxResult in PXSelectBase<NoteDoc, PXSelectGroupBy<NoteDoc, Where<NoteDoc.fileID, Equal<Required<NoteDoc.fileID>>>, Aggregate<GroupBy<NoteDoc.noteID>>>.Config>.Select((PXGraph) (PXContext.GetSlot<UploadFileMaintenance>() ?? PXContext.SetSlot<UploadFileMaintenance>(PXGraph.CreateInstance<UploadFileMaintenance>())), (object) file.FileID))
    {
      NoteDoc noteDoc = (NoteDoc) pxResult;
      Guid? noteId = noteDoc.NoteID;
      if (noteId.HasValue)
      {
        string str1 = (string) PXContext.Session[UploadFileMaintenance.GetAccessOverrideSessionKey(noteDoc.NoteID)];
        noteId = noteDoc.NoteID;
        string str2 = noteId.ToString();
        if (str1 == str2)
          goto label_7;
      }
      string slot = PXContext.GetSlot<string>($"{typeof (Note).FullName}+{noteDoc.NoteID}");
      noteId = noteDoc.NoteID;
      string str = noteId.ToString();
      if (!(slot == str))
        continue;
label_7:
      return true;
    }
    return false;
  }

  public PXCacheRights AccessRights(Guid fileID)
  {
    return UploadFileMaintenance.AccessRights((UploadFile) PXSelectBase<UploadFile, PXSelect<UploadFile, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>>>.Config>.Select((PXGraph) this, (object) fileID));
  }

  public static PXCacheRights AccessRights(UploadFile file)
  {
    if (file != null)
    {
      bool? isSystem = file.IsSystem;
      bool flag1 = true;
      if (!(isSystem.GetValueOrDefault() == flag1 & isSystem.HasValue))
      {
        bool? isPublic = file.IsPublic;
        bool flag2 = true;
        if (isPublic.GetValueOrDefault() == flag2 & isPublic.HasValue)
          return PXCacheRights.Delete;
        if (file.PrimaryPageID.HasValue)
        {
          PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
          Guid? primaryPageId = file.PrimaryPageID;
          Guid valueOrDefault = primaryPageId.GetValueOrDefault();
          if ((PXWikiMapNode) wikiProvider1.FindSiteMapNodeFromKey(valueOrDefault) == null)
            return PXCacheRights.Delete;
          PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
          primaryPageId = file.PrimaryPageID;
          Guid pageID = primaryPageId.Value;
          return Wiki.Convert(wikiProvider2.GetAccessRights(pageID));
        }
        if (file.PrimaryScreenID != null && PXSiteMap.Provider is PXDatabaseSiteMapProvider)
          return PXSiteMap.Provider.AccessRights(file.PrimaryScreenID);
        return !PXContext.PXIdentity.User.IsInRole(((IEnumerable<string>) PXAccess.GetAdministratorRoles()).First<string>()) ? PXCacheRights.Denied : PXCacheRights.Delete;
      }
    }
    return PXCacheRights.Denied;
  }

  /// <summary>
  /// Checks, whether file is attached to record which is restricted for viewing by current user.
  /// </summary>
  public static bool IsRestrictedFile(UploadFile file)
  {
    if (file != null && file.PrimaryScreenID != null)
    {
      bool? isPublic = file.IsPublic;
      bool flag1 = true;
      if (!(isPublic.GetValueOrDefault() == flag1 & isPublic.HasValue))
      {
        PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(file.PrimaryScreenID);
        if (mapNodeByScreenId == null || string.IsNullOrEmpty(mapNodeByScreenId.GraphType))
          return false;
        System.Type type1 = PXBuildManager.GetType(mapNodeByScreenId.GraphType, false);
        if ((object) type1 == null)
          type1 = System.Type.GetType(mapNodeByScreenId.GraphType);
        System.Type graphType = type1;
        if (graphType == (System.Type) null)
          return false;
        PXGraph instance1;
        using (new PXPreserveScope())
          instance1 = PXGraph.CreateInstance(graphType);
        bool flag2 = false;
        foreach (PXResult<Note, NoteDoc> pxResult in PXSelectBase<Note, PXSelectJoin<Note, InnerJoin<NoteDoc, On<Note.noteID, Equal<NoteDoc.noteID>>>, Where<NoteDoc.fileID, Equal<Required<NoteDoc.fileID>>>>.Config>.Select(instance1, (object) file.FileID))
        {
          Note note = (Note) pxResult;
          if (!(note.GraphType != mapNodeByScreenId.GraphType))
          {
            System.Type type2 = PXBuildManager.GetType(note.EntityType, false);
            if (!(type2 == (System.Type) null))
            {
              System.Type itemType = instance1.Caches[type2].GetItemType();
              System.Type type3 = (System.Type) null;
              bool flag3 = false;
              foreach (System.Type bqlField in instance1.Caches[itemType].BqlFields)
              {
                foreach (PXEventSubscriberAttribute attribute in instance1.Caches[itemType].GetAttributes(bqlField.Name))
                {
                  if (attribute is PXNoteAttribute)
                  {
                    type3 = bqlField;
                    flag3 = true;
                    break;
                  }
                }
                if (flag3)
                  break;
              }
              if (!(type3 == (System.Type) null))
              {
                System.Type type4 = BqlCommand.Compose(typeof (Optional<AccessInfo.userName>));
                System.Type type5 = BqlCommand.Compose(typeof (And<>), BqlCommand.Compose(typeof (Match<,>), itemType, type4));
                System.Type type6 = BqlCommand.Compose(typeof (Where<,,>), type3, typeof (Equal<Required<Note.noteID>>), type5);
                BqlCommand instance2 = BqlCommand.CreateInstance(typeof (PX.Data.Select<,>), itemType, type6);
                PXView pxView = new PXView(instance1, true, instance2);
                if (pxView.SelectSingle((object) note.NoteID, (object) instance1.Accessinfo.UserName) != null)
                  return false;
                foreach (object data in pxView.Cache.Inserted)
                {
                  Guid result;
                  if (Guid.TryParse(pxView.Cache.GetValueExt(data, "NoteID").ToString(), out result))
                  {
                    Guid guid = result;
                    Guid? noteId = note.NoteID;
                    if ((noteId.HasValue ? (guid == noteId.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                      return false;
                  }
                }
                flag2 = true;
              }
            }
          }
        }
        return flag2;
      }
    }
    return false;
  }

  /// <summary>
  /// Check if file is system (restricted to view for all users)
  /// </summary>
  /// <param name="file"></param>
  /// <returns></returns>
  public static string ComputeHash(UploadFile file)
  {
    string base64String = Convert.ToBase64String(UploadFileMaintenance.ComputeHash(Encoding.Unicode.GetBytes(file.FileID.ToString() + PXAccess.GetUserID().ToString())));
    return base64String.Substring(0, base64String.Length - 1);
  }

  public static void DeleteFile(Guid? fileId)
  {
    if (PXBlobStorage.AllowSave)
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UploadFileRevision>((PXDataField) new PXDataField<UploadFileRevision.blobHandler>(), (PXDataField) new PXDataFieldValue<UploadFileRevision.fileID>(PXDbType.UniqueIdentifier, (object) fileId)))
      {
        Guid? guid = pxDataRecord.GetGuid(0);
        if (guid.HasValue)
          PXBlobStorage.Remove(guid.Value);
      }
    }
    List<Guid> guidList = new List<Guid>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<NoteDoc>((PXDataField) new PXDataField<NoteDoc.noteID>(), (PXDataField) new PXDataFieldValue<UploadFileRevision.fileID>(PXDbType.UniqueIdentifier, (object) fileId)))
    {
      Guid? guid = pxDataRecord.GetGuid(0);
      if (guid.HasValue)
        guidList.Add(guid.Value);
    }
    foreach (Guid guid in guidList)
      PXDatabase.Update<SyncTimeTag>((PXDataFieldParam) new PXDataFieldAssign<SyncTimeTag.timeTag>(PXDbType.DateTime, (object) System.DateTime.UtcNow), (PXDataFieldParam) new PXDataFieldRestrict<SyncTimeTag.noteID>(PXDbType.UniqueIdentifier, (object) guid));
    PXDataFieldRestrict dataFieldRestrict = (PXDataFieldRestrict) new PXDataFieldRestrict<UploadFileRevision.fileID>(PXDbType.UniqueIdentifier, (object) fileId);
    PXDatabase.Delete<UploadFileRevision>(dataFieldRestrict);
    PXDatabase.Delete<UploadFile>(dataFieldRestrict);
    PXDatabase.Delete<WikiFileInPage>(dataFieldRestrict);
    PXDatabase.Delete<NoteDoc>(dataFieldRestrict);
  }

  [PXInternalUseOnly]
  public static void RemoveFromSystemFiles(Guid? fileId)
  {
    if (!fileId.HasValue)
      return;
    PXDatabase.Update<UploadFile>((PXDataFieldParam) new PXDataFieldAssign<UploadFile.isSystem>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldParam) new PXDataFieldRestrict<UploadFile.fileID>(PXDbType.UniqueIdentifier, (object) fileId.Value));
  }

  [PXInternalUseOnly]
  public static void UpdatePrimaryScreenId(Guid? fileId, string screenId)
  {
    if (!fileId.HasValue)
      return;
    PXDataFieldRestrict dataFieldRestrict = (PXDataFieldRestrict) new PXDataFieldRestrict<UploadFile.fileID>(PXDbType.UniqueIdentifier, (object) fileId.Value);
    PXDatabase.Update<UploadFile>((PXDataFieldParam) new PXDataFieldAssign<UploadFile.primaryScreenID>(PXDbType.Text, new int?(8), (object) screenId), (PXDataFieldParam) dataFieldRestrict);
  }

  private static byte[] ComputeHash(byte[] source)
  {
    if (UploadFileMaintenance.hashMaker == null)
    {
      UploadFileMaintenance.hashMaker = new MACTripleDES();
      UploadFileMaintenance.hashMaker.Initialize();
      byte[] data = new byte[24];
      RandomNumberGenerator.Create().GetBytes(data);
      UploadFileMaintenance.hashMaker.Key = data;
    }
    return UploadFileMaintenance.hashMaker.ComputeHash(source);
  }

  public FileInfo GetFile(string fileName) => this.GetFileByName(fileName, new int?());

  public FileInfo GetFile(string fileName, int revision)
  {
    return this.GetFileByName(fileName, new int?(revision));
  }

  public FileInfo GetFile(Guid fileID) => this.DoGetFile((object) fileID, new int?());

  public FileInfo GetFile(Guid fileID, int revision)
  {
    return this.DoGetFile((object) fileID, new int?(revision));
  }

  public FileInfo GetFileWithNoData(Guid fileID) => this.DoGetFile((object) fileID, new int?(-1));

  public FileInfo GetFileWithNoData(string fileName) => this.GetFileByName(fileName, new int?(-1));

  public IEnumerable GetFileRevisions(Guid fileID) => this.DoGetFileRevisions((object) fileID);

  public IEnumerable GetFileRevisions(string fileName)
  {
    return this.DoGetFileRevisions((object) fileName);
  }

  /// <summary>
  /// Saves a file to database or creates link to existing file.
  /// </summary>
  /// <param name="finfo">A structure describing file to save.</param>
  /// <param name="actionOnExisting">A <see cref="T:PX.SM.FileExistsAction" /> value.</param>
  /// <returns>True if file is successfully saved to database. Otherwise false.</returns>
  public bool SaveFile(FileInfo finfo, FileExistsAction actionOnExisting)
  {
    string lower = Path.GetExtension(finfo.Name).ToLower();
    if (!this.IgnoreFileRestrictions && !((IEnumerable<string>) this.AllowedFileTypes).Select<string, string>((Func<string, string>) (_ => _.ToLower())).Contains<string>(lower))
      throw new PXNotSupportedFileTypeException("Unsupported file type. Supported types are: " + string.Join(", ", this.AllowedFileTypes));
    if (finfo.IsLinkValid)
    {
      FileInfo file = this.GetFile(finfo.ToString());
      if (file != null)
      {
        finfo.RevisionId = file.RevisionId;
        finfo.UID = file.UID;
        finfo.UID = file.UID;
        finfo.FullName = file.FullName;
        this.dataUpdated = false;
        return true;
      }
    }
    return this.DoSaveFile(finfo, actionOnExisting);
  }

  /// <summary>
  /// Saves a file to database or creates link to existing file.
  /// </summary>
  /// <param name="finfo">A structure describing file to save.</param>
  /// <returns>True if file is successfully saved to database. Otherwise false.</returns>
  public virtual bool SaveFile(FileInfo finfo)
  {
    return this.SaveFile(finfo, FileExistsAction.ThrowException);
  }

  public void AttachToPage(Guid fileID, Guid pageID)
  {
    UploadFileMaintenance.SetAccessSource(fileID, new Guid?(pageID), (string) null, true);
  }

  public static void SetAccessSource(
    Guid fileID,
    Guid? pageID,
    string screentID,
    bool overridePrimaryScreenID = false)
  {
    Guid? nullable1 = new Guid?();
    string str1 = (string) null;
    Guid? nullable2 = new Guid?();
    System.DateTime? nullable3 = new System.DateTime?();
    int? nullable4 = new int?();
    string str2 = (string) null;
    bool? nullable5 = new bool?();
    Guid? nullable6 = new Guid?();
    using (PXDataRecord pxDataRecord1 = PXDatabase.SelectSingle<UploadFile>(new PXDataField(typeof (UploadFile.checkedOutBy).Name), new PXDataField(typeof (UploadFile.checkedOutComment).Name), new PXDataField(typeof (UploadFile.createdByID).Name), new PXDataField(typeof (UploadFile.createdDateTime).Name), new PXDataField(typeof (UploadFile.lastRevisionID).Name), new PXDataField(typeof (UploadFile.name).Name), new PXDataField(typeof (UploadFile.versioned).Name), new PXDataField(typeof (UploadFile.noteID).Name), (PXDataField) new PXDataFieldValue(typeof (UploadFile.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID), (PXDataField) new PXDataFieldValue(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, new int?(), (object) null, PXComp.ISNULL), (PXDataField) new PXDataFieldValue(typeof (UploadFile.primaryScreenID).Name, PXDbType.VarChar, new int?(), (object) null, PXComp.ISNULL)))
    {
      if (pxDataRecord1 != null)
      {
        nullable1 = pxDataRecord1.GetGuid(0);
        str1 = pxDataRecord1.GetString(1);
        nullable2 = pxDataRecord1.GetGuid(2);
        nullable3 = pxDataRecord1.GetDateTime(3);
        nullable4 = pxDataRecord1.GetInt32(4);
        str2 = pxDataRecord1.GetString(5);
        nullable5 = pxDataRecord1.GetBoolean(6);
        nullable6 = pxDataRecord1.GetGuid(7);
      }
      else
      {
        using (PXDataRecord pxDataRecord2 = PXDatabase.SelectSingle<UploadFile>(new PXDataField(typeof (UploadFile.checkedOutBy).Name), new PXDataField(typeof (UploadFile.checkedOutComment).Name), new PXDataField(typeof (UploadFile.createdByID).Name), new PXDataField(typeof (UploadFile.createdDateTime).Name), new PXDataField(typeof (UploadFile.lastRevisionID).Name), new PXDataField(typeof (UploadFile.name).Name), new PXDataField(typeof (UploadFile.versioned).Name), new PXDataField(typeof (UploadFile.noteID).Name), (PXDataField) new PXDataFieldValue(typeof (UploadFile.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID), (PXDataField) new PXDataFieldValue(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, new int?(), (object) null, PXComp.ISNULL)))
        {
          if (pxDataRecord2 != null)
          {
            nullable1 = pxDataRecord2.GetGuid(0);
            str1 = pxDataRecord2.GetString(1);
            nullable2 = pxDataRecord2.GetGuid(2);
            nullable3 = pxDataRecord2.GetDateTime(3);
            nullable4 = pxDataRecord2.GetInt32(4);
            str2 = pxDataRecord2.GetString(5);
            nullable5 = pxDataRecord2.GetBoolean(6);
            nullable6 = pxDataRecord2.GetGuid(7);
          }
        }
      }
    }
    try
    {
      if (!(!PXDatabase.Update<UploadFile>((PXDataFieldParam) new PXDataFieldAssign(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, (object) pageID), (PXDataFieldParam) new PXDataFieldAssign(typeof (UploadFile.primaryScreenID).Name, PXDbType.VarChar, (object) screentID), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UploadFile.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, new int?(), (object) null, PXComp.ISNULL), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UploadFile.primaryScreenID).Name, PXDbType.VarChar, new int?(), (object) null, PXComp.ISNULL), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed) & overridePrimaryScreenID))
        return;
      PXDatabase.Update<UploadFile>((PXDataFieldParam) new PXDataFieldAssign(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, (object) pageID), (PXDataFieldParam) new PXDataFieldAssign(typeof (UploadFile.primaryScreenID).Name, PXDbType.VarChar, (object) screentID), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UploadFile.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, new int?(), (object) null, PXComp.ISNULL), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
    }
    catch (PXDbOperationSwitchRequiredException ex)
    {
      if (str2 == null)
        return;
      PXDatabase.Insert<UploadFile>(new PXDataFieldAssign(typeof (UploadFile.primaryPageID).Name, PXDbType.UniqueIdentifier, (object) pageID), new PXDataFieldAssign(typeof (UploadFile.primaryScreenID).Name, PXDbType.VarChar, (object) screentID), new PXDataFieldAssign(typeof (UploadFile.fileID).Name, PXDbType.UniqueIdentifier, (object) fileID), new PXDataFieldAssign(typeof (UploadFile.checkedOutBy).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) nullable1), new PXDataFieldAssign(typeof (UploadFile.checkedOutComment).Name, PXDbType.VarChar, new int?(500), (object) str1), new PXDataFieldAssign(typeof (UploadFile.createdByID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) nullable2), new PXDataFieldAssign(typeof (UploadFile.createdDateTime).Name, PXDbType.DateTime, new int?(8), (object) nullable3), new PXDataFieldAssign(typeof (UploadFile.lastRevisionID).Name, PXDbType.Int, new int?(4), (object) nullable4), new PXDataFieldAssign(typeof (UploadFile.name).Name, PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) str2), new PXDataFieldAssign(typeof (UploadFile.versioned).Name, PXDbType.Bit, new int?(1), (object) nullable5), new PXDataFieldAssign(typeof (UploadFile.noteID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) nullable6));
    }
  }

  public string GetFileComment(string filename)
  {
    using (IEnumerator<PXResult<UploadFileWithNoData>> enumerator = PXSelectBase<UploadFileWithNoData, PXSelect<UploadFileWithNoData, Where<UploadFile.name, Equal<Required<UploadFile.name>>>>.Config>.Select((PXGraph) this, (object) filename).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return ((UploadFileWithNoData) enumerator.Current).Comment;
    }
    return (string) null;
  }

  public bool SetFileComment(Guid fileId, string comment)
  {
    UploadFileRevisionNoData fileRevisionNoData = (UploadFileRevisionNoData) PXSelectBase<UploadFileRevisionNoData, PXSelect<UploadFileRevisionNoData, Where<UploadFileRevisionNoData.fileID, Equal<Required<UploadFileRevisionNoData.fileID>>>, OrderBy<Desc<UploadFileRevisionNoData.fileRevisionID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) fileId);
    if ((fileRevisionNoData != null ? (!fileRevisionNoData.FileRevisionID.HasValue ? 1 : 0) : 1) != 0)
      return false;
    PXDataFieldRestrict dataFieldRestrict1 = (PXDataFieldRestrict) new PXDataFieldRestrict<UploadFileRevision.fileID>(PXDbType.UniqueIdentifier, (object) fileId);
    PXDataFieldRestrict dataFieldRestrict2 = (PXDataFieldRestrict) new PXDataFieldRestrict<UploadFileRevision.fileRevisionID>((object) fileRevisionNoData.FileRevisionID);
    return PXDatabase.Update<UploadFileRevision>((PXDataFieldParam) new PXDataFieldAssign<UploadFileRevision.comment>((object) comment), (PXDataFieldParam) dataFieldRestrict1, (PXDataFieldParam) dataFieldRestrict2);
  }

  /// <summary>
  /// Gets an array of file types which are allowed for upload.
  /// </summary>
  public string[] AllowedFileTypes => SitePolicy.AllowedFileTypesExt;

  public static void DeleteFileLink(Guid fileId, Guid noteId)
  {
    PXDatabase.Delete<NoteDoc>((PXDataFieldRestrict) new PXDataFieldRestrict<NoteDoc.fileID>(PXDbType.UniqueIdentifier, (object) fileId), (PXDataFieldRestrict) new PXDataFieldRestrict<NoteDoc.noteID>(PXDbType.UniqueIdentifier, (object) noteId));
  }

  public bool HasFileLink(Guid fileId, Guid noteId)
  {
    PXResultset<NoteDoc> pxResultset = this.FileNoteDoc.Select((object) fileId, (object) noteId);
    return pxResultset != null && pxResultset.Count > 0;
  }

  public void AddFileLink(Guid fileId, Guid noteId)
  {
    if (this.HasFileLink(fileId, noteId))
      return;
    PXDatabase.Insert<NoteDoc>((PXDataFieldAssign) new PXDataFieldAssign<NoteDoc.noteID>(PXDbType.UniqueIdentifier, (object) noteId), (PXDataFieldAssign) new PXDataFieldAssign<NoteDoc.fileID>(PXDbType.UniqueIdentifier, (object) fileId));
  }

  public int GetNodeFileCount(Guid nodeId)
  {
    return PXDatabase.SelectMulti<NoteDoc>((PXDataField) new PXDataField<NoteDoc.noteID>(), (PXDataField) new PXDataFieldValue<NoteDoc.noteID>(PXDbType.UniqueIdentifier, (object) nodeId)).Count<PXDataRecord>();
  }

  private UploadFile GetUploadFile(object file, int? revision)
  {
    PXResultset<UploadFile> pxResultset;
    if (!revision.HasValue)
      pxResultset = this.Files.Select(file);
    else
      pxResultset = this.Files.Select(file, (object) revision);
    UploadFile file1 = (UploadFile) pxResultset;
    if (file1 != null)
    {
      if (!UploadFileMaintenance.OverrideAccessRights(file1))
      {
        if (UploadFileMaintenance.AccessRights(file1) < PXCacheRights.Select)
          throw new PXException("You don't have enough rights to download this file.");
        if (UploadFileMaintenance.IsRestrictedFile(file1))
          throw new PXException("You are not in a group that has access to this file.");
      }
      this.Files.Current = file1;
    }
    return file1;
  }

  private FileInfo GetFileByName(string fileName, int? revision)
  {
    Match match = new Regex("\\[(image:\\s*|{up}\\s*)(?<Name>.+)(\\]|\\|)", RegexOptions.IgnoreCase).Match(fileName);
    if (match.Success)
      fileName = match.Groups["Name"].Value;
    else if (Uri.IsWellFormedUriString(fileName, UriKind.Absolute))
    {
      string[] strArray = new Uri(fileName, UriKind.Absolute).Query.Split('=');
      if (strArray.Length == 2 && strArray[0].OrdinalEquals("?fileid"))
      {
        Guid? guid = GUID.CreateGuid(strArray[1]);
        return guid.HasValue ? this.DoGetFile((object) guid.Value, revision) : (FileInfo) null;
      }
    }
    return this.DoGetFile((object) fileName, revision);
  }

  private FileInfo DoGetFile(object file, int? revision)
  {
    UploadFile uploadFile = this.GetUploadFile(file, revision);
    FileInfo file1 = uploadFile != null ? new FileInfo(new Guid?(uploadFile.FileID.Value), uploadFile.FileRevisionID, uploadFile.Name, uploadFile.OriginalName, (string) null, uploadFile.Data, uploadFile.Comment, uploadFile.Size, uploadFile.RevisionDate) : (FileInfo) null;
    if (file1 != null)
      file1.IsPublic = uploadFile.IsPublic.GetValueOrDefault();
    return file1;
  }

  private IEnumerable DoGetFileRevisions(object file)
  {
    UploadFile rec = this.GetUploadFile(file, new int?());
    foreach (PXResult<UploadFileRevision> pxResult in this.Revisions.Select())
    {
      UploadFileRevision uploadFileRevision = (UploadFileRevision) pxResult;
      yield return (object) new FileInfo(new Guid?(rec.FileID.Value), uploadFileRevision.FileRevisionID, rec.Name, uploadFileRevision.OriginalName, (string) null, uploadFileRevision.Data, uploadFileRevision.Comment, uploadFileRevision.Size, uploadFileRevision.CreatedDateTime);
    }
  }

  internal bool dataUpdated { get; set; } = true;

  private bool DoSaveFile(FileInfo finfo, FileExistsAction actionOnExisting)
  {
    this.SelectTimeStamp();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      UploadFile uploadFile1 = (UploadFile) this.Files.Select((object) finfo.FullName);
      Guid? nullable;
      if (uploadFile1 == null)
      {
        nullable = finfo.UID;
        if (nullable.HasValue)
        {
          UploadFileMaintenance.PXSelectFile files = this.Files;
          object[] objArray = new object[1];
          nullable = finfo.UID;
          objArray[0] = (object) nullable.Value;
          uploadFile1 = (UploadFile) files.Select(objArray);
        }
      }
      if (uploadFile1 != null)
      {
        this.Files.Current = uploadFile1;
        switch (actionOnExisting)
        {
          case FileExistsAction.ThrowException:
            throw new PXSetPropertyException("This file already exists.");
          case FileExistsAction.CreateVersion:
            nullable = uploadFile1.CheckedOutBy;
            if (nullable.HasValue)
            {
              nullable = uploadFile1.CheckedOutBy;
              Guid userId = this.Accessinfo.UserID;
              if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != userId ? 1 : 0) : 0) : 1) != 0)
                throw new PXException("A new revision cannot be uploaded because the file is checked out by another user.");
            }
            if (!PXPath.GetExtension(uploadFile1.Name).OrdinalEquals(PXPath.GetExtension(finfo.FullName)))
              throw new PXException("The file you are trying to upload has an extension that differs from the extension that is stored in the database.");
            uploadFile1.Data = finfo.BinData;
            uploadFile1.Comment = finfo.Comment;
            uploadFile1.OriginalName = UploadFileHelper.GetOriginalName(uploadFile1.Name, finfo.OriginalName);
            uploadFile1.IsPublic = new bool?(finfo.IsPublic);
            uploadFile1.IsSystem = new bool?(finfo.IsSystem);
            this.dataUpdated = true;
            this.Files.Update(uploadFile1);
            this.Actions.PressSave();
            finfo.UID = uploadFile1.FileID;
            break;
          case FileExistsAction.ReturnExisting:
            finfo.UID = uploadFile1.FileID;
            break;
        }
      }
      else
      {
        if (!this.IgnoreFileRestrictions && (long) finfo.BinData.Length > (long) SitePolicy.MaxRequestSize * 1024L /*0x0400*/)
          throw new PXTooBigFileException("The file exceeds the maximum allowed size ({0} KB).", new object[1]
          {
            (object) SitePolicy.MaxRequestSize
          });
        UploadFile uploadFile2 = new UploadFile()
        {
          Name = finfo.FullName,
          Data = finfo.BinData,
          Comment = finfo.Comment
        };
        uploadFile2.OriginalName = UploadFileHelper.GetOriginalName(uploadFile2.Name, finfo.OriginalName);
        uploadFile2.IsPublic = new bool?(finfo.IsPublic);
        uploadFile2.IsSystem = new bool?(finfo.IsSystem);
        nullable = finfo.UID;
        if (nullable.HasValue)
          uploadFile2.FileID = finfo.UID;
        uploadFile1 = this.Files.Insert(uploadFile2);
        if (uploadFile1 == null)
          return false;
        this.dataUpdated = true;
        this.Actions.PressSave();
      }
      transactionScope.Complete();
      finfo.UID = uploadFile1.FileID;
      finfo.RevisionId = uploadFile1.LastRevisionID;
      PXTrace.WithSourceLocation(nameof (DoSaveFile), "C:\\build\\code_repo\\NetTools\\PX.Data\\Wiki\\UploadFile.cs", 799).ForTelemetry("ScreenInfo", "FileSaved").Verbose("File {FullName} {BinData} with comment {Comment} and original name {OriginalName} is public {IsPublic} is system {IsSystem} saved", new object[6]
      {
        (object) uploadFile1.Name,
        (object) uploadFile1.Data,
        (object) uploadFile1.Comment,
        (object) uploadFile1.OriginalName,
        (object) uploadFile1.IsPublic,
        (object) uploadFile1.IsSystem
      });
      return true;
    }
  }

  protected void UploadFile_FileID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected void UploadFile_PrimaryPageID_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    UploadFile row = (UploadFile) e.Row;
    if (!row.PrimaryPageID.HasValue)
      return;
    e.NewValue = (object) row.PrimaryPageID;
  }

  protected void UploadFile_PrimaryScreenID_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    UploadFile row = (UploadFile) e.Row;
    if (row.PrimaryScreenID != null)
    {
      e.NewValue = (object) row.PrimaryScreenID;
    }
    else
    {
      string screenId = PXContext.GetScreenID();
      string str = string.IsNullOrEmpty(screenId) ? screenId : screenId.Replace(".", "");
      e.NewValue = (object) str;
    }
  }

  protected void UploadFile_Name_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected void UploadFile_RowSelected(PXCache cahce, PXRowSelectedEventArgs e)
  {
    UploadFile row = (UploadFile) e.Row;
    if (row == null)
      return;
    int? nullable1 = row.FileRevisionID;
    if (nullable1.HasValue)
      return;
    nullable1 = this.Filter.Current.FileRevisionID;
    int? nullable2 = nullable1 ?? row.LastRevisionID;
    if (nullable2.HasValue)
    {
      nullable1 = nullable2;
      int num = 0;
      if (nullable1.GetValueOrDefault() < num & nullable1.HasValue)
      {
        UploadFileRevisionNoData fileRevisionNoData = (UploadFileRevisionNoData) PXSelectBase<UploadFileRevisionNoData, PXSelect<UploadFileRevisionNoData, Where<UploadFileRevisionNoData.fileID, Equal<Current<UploadFile.fileID>>, And<UploadFileRevisionNoData.fileRevisionID, Equal<Required<UploadFileRevisionNoData.fileRevisionID>>>>>.Config>.Select((PXGraph) this, (object) row.LastRevisionID);
        if (fileRevisionNoData == null)
          return;
        row.RevisionDate = fileRevisionNoData.CreatedDateTime;
        row.Size = fileRevisionNoData.Size;
        return;
      }
    }
    UploadFileRevision uploadFileRevision = (UploadFileRevision) PXSelectBase<UploadFileRevision, PXSelect<UploadFileRevision, Where<UploadFileRevision.fileID, Equal<Current<UploadFile.fileID>>, And<UploadFileRevision.fileRevisionID, Equal<Required<UploadFileRevision.fileRevisionID>>>>>.Config>.Select((PXGraph) this, (object) nullable2);
    if (uploadFileRevision != null)
    {
      row.Data = uploadFileRevision.Data;
      row.Comment = uploadFileRevision.Comment;
      row.FileRevisionID = uploadFileRevision.FileRevisionID;
      row.OriginalName = uploadFileRevision.OriginalName;
      row.RevisionDate = uploadFileRevision.CreatedDateTime;
      row.Size = uploadFileRevision.Size;
    }
    else
      row.FileRevisionID = new int?(0);
  }

  protected void UploadFile_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    UploadFile row = (UploadFile) e.Row;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete)
      return;
    foreach (UploadFileRevision uploadFileRevision in this.Revisions.Cache.Cached)
    {
      Guid? fileId1 = uploadFileRevision.FileID;
      Guid? fileId2 = row.FileID;
      if ((fileId1.HasValue == fileId2.HasValue ? (fileId1.HasValue ? (fileId1.GetValueOrDefault() == fileId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        this.Revisions.Cache.Remove((object) uploadFileRevision);
    }
    PXDatabase.Delete<WikiFileInPage>(new PXDataFieldRestrict(typeof (WikiFileInPage.fileID).Name, PXDbType.UniqueIdentifier, (object) row.FileID));
    PXDatabase.Delete<UploadFileRevision>(new PXDataFieldRestrict(typeof (UploadFileRevision.fileID).Name, PXDbType.UniqueIdentifier, (object) row.FileID));
  }

  private void CreateRevision(UploadFile row)
  {
    UploadFileRevision uploadFileRevision = new UploadFileRevision();
    uploadFileRevision.FileID = row.FileID;
    uploadFileRevision.Data = row.Data;
    uploadFileRevision.Comment = row.Comment;
    uploadFileRevision.Size = new int?(row.Data != null ? UploadFileHelper.BytesToKilobytes(row.Data.Length) : 0);
    uploadFileRevision.OriginalName = row.OriginalName;
    bool? versioned = row.Versioned;
    bool flag = true;
    if (versioned.GetValueOrDefault() == flag & versioned.HasValue)
    {
      UploadFile uploadFile = row;
      int? lastRevisionId = uploadFile.LastRevisionID;
      uploadFile.LastRevisionID = lastRevisionId.HasValue ? new int?(lastRevisionId.GetValueOrDefault() + 1) : new int?();
    }
    uploadFileRevision.FileRevisionID = row.LastRevisionID;
    this.Revisions.Insert(uploadFileRevision);
  }

  public override void Persist()
  {
    foreach (UploadFile row in this.Files.Cache.Cached)
    {
      switch (this.Files.Cache.GetStatus((object) row))
      {
        case PXEntryStatus.Updated:
        case PXEntryStatus.Inserted:
          if (row.Data != null && this.dataUpdated)
          {
            this.CreateRevision(row);
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    base.Persist();
  }

  /// <exclude />
  public class PXSelectFile : PXSelectBase<UploadFile>
  {
    public PXSelectFile(PXGraph graph)
    {
      this.View = (PXView) new UploadFileMaintenance.PXSelectFile.ViewPage(graph);
    }

    public PXSelectFile(PXGraph graph, Delegate handler)
    {
      this.View = (PXView) new UploadFileMaintenance.PXSelectFile.ViewPage(graph, handler);
    }

    /// <exclude />
    private class ViewPage : PXView
    {
      public ViewPage(PXGraph graph, Delegate handler)
        : base(graph, false, (BqlCommand) new PX.Data.Select<UploadFile>(), handler)
      {
      }

      public ViewPage(PXGraph graph)
        : base(graph, false, (BqlCommand) new PX.Data.Select<UploadFile>())
      {
      }

      public override List<object> Select(
        object[] currents,
        object[] parameters,
        object[] searches,
        string[] sortcolumns,
        bool[] descendings,
        PXFilterRow[] filters,
        ref int startRow,
        int maximumRows,
        ref int totalRows)
      {
        List<object> objectList = new List<object>();
        if (!(this.Graph.Caches[typeof (UploadFileFilter)].Current is UploadFileFilter uploadFileFilter))
        {
          uploadFileFilter = new UploadFileFilter();
          this.Graph.Caches[typeof (UploadFileFilter)].Current = (object) uploadFileFilter;
        }
        bool flag1 = false;
        if (searches != null)
        {
          for (int index = 0; index < searches.Length; ++index)
          {
            if (string.CompareOrdinal(sortcolumns[index], typeof (UploadFile.fileID).Name) == 0)
            {
              flag1 = searches[index] != null;
              break;
            }
          }
        }
        if (!flag1)
        {
          if (parameters == null || parameters.Length == 0 || parameters.Length > 2)
            return objectList;
          if (parameters.Length > 1 && parameters[1] is int)
            uploadFileFilter.FileRevisionID = new int?((int) parameters[1]);
          if (parameters[0] is Guid)
          {
            sortcolumns = new string[1]
            {
              typeof (UploadFile.fileID).Name
            };
            searches = new object[1]{ parameters[0] };
            uploadFileFilter.FileID = new Guid?((Guid) parameters[0]);
            parameters = (object[]) null;
          }
          else if (parameters[0] is string)
          {
            sortcolumns = new string[1]
            {
              typeof (UploadFile.name).Name
            };
            searches = new object[1]{ parameters[0] };
            uploadFileFilter.Name = (string) parameters[0];
            parameters = (object[]) null;
          }
        }
        foreach (UploadFile file in base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, 1, ref totalRows))
        {
          bool flag2 = false;
          if (!string.IsNullOrEmpty(uploadFileFilter.HashAccess))
            flag2 = UploadFileMaintenance.ComputeHash(file) == uploadFileFilter.HashAccess;
          if (flag2 || UploadFileMaintenance.AccessRights(file) >= PXCacheRights.Select || UploadFileMaintenance.OverrideAccessRights(file))
            objectList.Add((object) file);
        }
        return objectList;
      }
    }
  }
}
