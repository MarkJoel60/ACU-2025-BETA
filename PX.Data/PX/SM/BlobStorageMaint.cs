// Decompiled with JetBrains decompiler
// Type: PX.SM.BlobStorageMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Session;
using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.SM;

public class BlobStorageMaint : PXGraph<BlobStorageMaint>
{
  private static readonly object _uid = (object) nameof (BlobStorageMaint);
  public PXSelect<BlobStorageConfig> Filter;
  public PXSelect<BlobProviderSettings> Settings;
  public PXSelectReadonly<UploadFileRevision, Where<UploadFileRevision.blobData, IsNotNull, And<UploadFileRevision.blobHandler, IsNull>>> FilesInDatabase;
  public PXSelectReadonly<UploadFileRevision, Where<UploadFileRevision.blobHandler, IsNotNull>> FilesInBlob;
  public PXSave<BlobStorageConfig> Save;
  public PXCancel<BlobStorageConfig> Cancel;
  public PXFilter<BlobStorageMaint.BlobStorageMessage> BlobStorageMessages;
  public PXAction<BlobStorageConfig> ActionEnableProvider;
  public PXAction<BlobStorageConfig> ActionSaveFlag;
  public PXAction<BlobStorageConfig> ActionDisableProvider;
  public PXAction<BlobStorageConfig> ActionMoveFilesToDatabase;
  public PXAction<BlobStorageConfig> ActionPanelMessagesOK;
  public PXAction<BlobStorageConfig> ActionMoveFilesToStorage;

  public BlobStorageMaint()
  {
    this.UID = BlobStorageMaint._uid;
    if (!this.UnattendedMode && HttpContext.Current != null)
    {
      GraphSessionStatePrefix sessionPrefix = GraphSessionStatePrefix.WithoutStatePrefixFor((PXGraph) this);
      IPXSessionState inner = PXContext.Session.Inner;
      if (inner != null)
      {
        object[] graphInfo = inner.GetGraphInfo(sessionPrefix);
        if (graphInfo != null)
          graphInfo[2] = BlobStorageMaint._uid;
      }
    }
    if (WebConfig.DisableExternalFileStorage)
      throw new PXSetupNotEnteredException<BlobStorageConfig>("External File Storage is disabled by administrator.", Array.Empty<object>());
  }

  private void DisableItems()
  {
    if (this.Filter.Current == null)
      this.Filter.Current = (BlobStorageConfig) this.Filter.Select();
    int num;
    if (this.Filter.Current != null)
    {
      bool? isActive = this.Filter.Current.IsActive;
      bool flag = true;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    else
      num = 0;
    bool isEnabled = num != 0;
    if (isEnabled)
    {
      try
      {
        PXBlobStorage.CheckInitError();
      }
      catch (PXProviderConfigException ex)
      {
        if (ex.Row != null && !string.IsNullOrWhiteSpace(ex.Row.Name) && this.Settings.Cache.Cached != null && this.Settings.Cache.Cached.Cast<BlobProviderSettings>().Any<BlobProviderSettings>((Func<BlobProviderSettings, bool>) (_ => _.Name == ex.Row.Name)))
          PXUIFieldAttribute.SetError<BlobProviderSettings.value>(this.Settings.Cache, (object) this.Settings.Cache.Cached.Cast<BlobProviderSettings>().First<BlobProviderSettings>((Func<BlobProviderSettings, bool>) (_ => _.Name == ex.Row.Name)), ex.Message);
        else
          this.Settings.Cache.RaiseExceptionHandling<BlobProviderSettings.value>((object) ex.Row, (object) null, (Exception) ex);
      }
    }
    bool flag1 = false;
    if (PXLongOperation.GetStatus(BlobStorageMaint._uid) == PXLongRunStatus.InProcess)
      flag1 = true;
    this.Filter.Cache.AllowDelete = !isEnabled;
    this.Filter.Cache.AllowInsert = !isEnabled;
    this.Filter.Cache.AllowUpdate = !isEnabled;
    PXUIFieldAttribute.SetEnabled(this.Filter.Cache, (object) null, (string) null, !isEnabled);
    PXUIFieldAttribute.SetEnabled<BlobStorageConfig.allowWrite>(this.Filter.Cache, (object) null, isEnabled);
    this.Settings.Cache.AllowDelete = !isEnabled;
    this.Settings.Cache.AllowInsert = !isEnabled;
    this.Settings.Cache.AllowUpdate = !isEnabled;
    this.ActionSaveFlag.SetEnabled(isEnabled);
    bool flag2 = isEnabled && this.GetHasFilesInStorage();
    this.ActionMoveFilesToDatabase.SetEnabled(isEnabled & flag2 && !PXBlobStorage.AllowSave && !flag1);
    this.ActionMoveFilesToStorage.SetEnabled(((!isEnabled ? 0 : (PXBlobStorage.AllowSave ? 1 : 0)) & (isEnabled ? 1 : 0)) != 0 && !flag1 && this.GetHasFilesInDb());
    this.ActionEnableProvider.SetEnabled(!isEnabled);
    this.ActionDisableProvider.SetEnabled(isEnabled && !flag2);
    this.ActionPanelMessagesOK.SetEnabled(true);
  }

  private bool GetHasFilesInDb()
  {
    return PXBlobStorage.AllowSave && this.FilesInDatabase.SelectWindowed(0, 1).FirstTableItems.Any<UploadFileRevision>((Func<UploadFileRevision, bool>) (_ => PXBlobStorage.IsRemoteStorageEnabled(_.BlobData)));
  }

  private bool GetHasFilesInStorage()
  {
    return this.FilesInBlob.SelectWindowed(0, 1).FirstTableItems.Any<UploadFileRevision>();
  }

  protected void _(Events.FieldUpdated<BlobStorageConfig.provider> e)
  {
    if (!e.ExternalCall)
      return;
    this.ProviderChanged((BlobStorageConfig) e.Row);
  }

  protected void _(Events.RowInserted<BlobStorageConfig> e)
  {
    if (!e.ExternalCall)
      return;
    this.ProviderChanged((BlobStorageConfig) null);
  }

  private void ProviderChanged(BlobStorageConfig config)
  {
    foreach (BlobProviderSettings firstTableItem in this.Settings.Select().FirstTableItems)
      this.Settings.Delete(firstTableItem);
    if (config == null)
      return;
    foreach (BlobProviderSettings setting in PXBlobStorage.CreateProvider(config.Provider).GetSettings())
      this.Settings.Insert(setting);
  }

  protected void BlobProviderSettingsRowSelectedEvent(Events.RowSelected<BlobProviderSettings> e)
  {
    if (e == null)
      return;
    this.DisableItems();
  }

  protected void BlobStorageConfigRowSelectedEvent(Events.RowSelected<BlobStorageConfig> e)
  {
    if (e == null)
      return;
    this.DisableItems();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Enable Provider")]
  protected void actionEnableProvider()
  {
    BlobStorageConfig current = this.Filter.Current;
    IBlobStorageProvider provider = PXBlobStorage.CreateProvider(current.Provider);
    if (provider == null)
      return;
    try
    {
      provider.Init(this.Settings.Select().FirstTableItems);
      current.IsActive = new bool?(true);
      this.Filter.Update(current);
      this.Save.Press();
      PXBlobStorage.Reset();
      this.DisableItems();
      this.Settings.Cache.Clear();
      this.Settings.Cache.ClearQueryCache();
    }
    catch (PXProviderConfigException ex)
    {
      this.Settings.Cache.RaiseExceptionHandling<BlobProviderSettings.value>((object) ex.Row, (object) null, (Exception) ex);
    }
    catch (Exception ex)
    {
      if (ex == null)
        return;
      throw;
    }
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Switch Direction")]
  protected void actionSaveFlag()
  {
    BlobStorageConfig current = this.Filter.Current;
    current.AllowWrite = new bool?(!current.AllowWrite.GetValueOrDefault());
    this.Filter.Update(current);
    this.Save.Press();
    PXBlobStorage.Reset();
    this.DisableItems();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Disable Provider")]
  protected void actionDisableProvider()
  {
    BlobStorageConfig current = this.Filter.Current;
    current.IsActive = new bool?(false);
    current.AllowWrite = new bool?(false);
    this.DisableItems();
    this.Filter.Update(current);
    this.Save.Press();
    PXBlobStorage.Reset();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Move Files to Database")]
  protected void actionMoveFilesToDatabase()
  {
    string messages = "";
    PXLongOperation.StartOperation(BlobStorageMaint._uid, (PXToggleAsyncDelegate) (() =>
    {
      messages = BlobStorageMaint.MigrateFromStorage();
      if (string.IsNullOrEmpty(messages))
        return;
      BlobStorageMaint.BlobStorageMessage err = this.BlobStorageMessages.Current;
      int num = (int) this.BlobStorageMessages.AskExt((PXView.InitializePanel) ((_param1, _param2) => err.Messages = messages));
    }));
  }

  [PXButton]
  [PXUIField(DisplayName = "OK", Visible = false)]
  protected void actionPanelMessagesOK()
  {
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Move Files to Storage")]
  protected void actionMoveFilesToStorage()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXLongOperation.StartOperation(BlobStorageMaint._uid, BlobStorageMaint.\u003C\u003EO.\u003C0\u003E__MigrateToStorage ?? (BlobStorageMaint.\u003C\u003EO.\u003C0\u003E__MigrateToStorage = new PXToggleAsyncDelegate(BlobStorageMaint.MigrateToStorage)));
  }

  private static void MigrateToStorage()
  {
    UploadFileRevision lastRow = (UploadFileRevision) null;
    while (true)
    {
      BlobStorageMaint instance = PXGraph.CreateInstance<BlobStorageMaint>();
      PXSelectBase<UploadFileRevision> filesInDatabase = (PXSelectBase<UploadFileRevision>) instance.FilesInDatabase;
      IEnumerable<UploadFileRevision> nextItems = BlobStorageMaint.GetNextItems(filesInDatabase, lastRow, 1);
      if (nextItems.Any<UploadFileRevision>())
      {
        foreach (UploadFileRevision file in nextItems)
        {
          lastRow = file;
          if (!PXBlobStorage.AllowSave)
            throw new PXException("The provider is not configured to store files.");
          if (!PXBlobStorage.IsRemoteStorageEnabled(file.Data))
            throw new PXException("Cannot move the file to a remote storage.");
          BlobStorageMaint.CopyData(file);
          filesInDatabase.Cache.SetStatus((object) file, PXEntryStatus.Updated);
        }
        instance.Save.Press();
      }
      else
        break;
    }
  }

  private static string MigrateFromStorage()
  {
    UploadFileRevision lastRow = (UploadFileRevision) null;
    string str = "";
    List<string> values = new List<string>();
    HashSet<string> skippedFiles = new HashSet<string>();
label_1:
    BlobStorageMaint instance = PXGraph.CreateInstance<BlobStorageMaint>();
    PXSelectBase<UploadFileRevision> filesInBlob = (PXSelectBase<UploadFileRevision>) instance.FilesInBlob;
    IEnumerable<UploadFileRevision> source = BlobStorageMaint.GetNextItems(filesInBlob, lastRow, 0).Where<UploadFileRevision>((Func<UploadFileRevision, bool>) (_ => !skippedFiles.Contains(_.FileID.ToString() + _.FileRevisionID.ToString())));
    if (source.Any<UploadFileRevision>())
    {
      using (IEnumerator<UploadFileRevision> enumerator = source.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          UploadFileRevision current = enumerator.Current;
          lastRow = current;
          Guid id = current.BlobHandler.Value;
          if (PXBlobStorage.AllowSave)
            throw new PXException("The provider is configured to store files.");
          try
          {
            BlobStorageMaint.CopyData(current);
            filesInBlob.Cache.SetStatus((object) current, PXEntryStatus.Updated);
          }
          catch (FileNotFoundException ex)
          {
            skippedFiles.Add(current.FileID.ToString() + current.FileRevisionID.ToString());
            values.Add(ex.Message);
            continue;
          }
          instance.Save.Press();
          PXBlobStorage.Remove(id);
        }
        goto label_1;
      }
    }
    if (values.Count > 0)
      str = string.Join("\n", (IEnumerable<string>) values);
    return str;
  }

  private static IEnumerable<UploadFileRevision> GetNextItems(
    PXSelectBase<UploadFileRevision> psSelect,
    UploadFileRevision lastRow,
    int maximumRows)
  {
    return psSelect.SelectWindowed(0, maximumRows).FirstTableItems;
  }

  private static void CopyData(UploadFileRevision file)
  {
    byte[] data1 = file.Data;
    file.Data = data1;
    byte[] data2 = file.Data;
    if ((data2 == data1 ? 1 : (data2.Length == data1.Length ? 1 : 0)) == 0)
      throw new PXException("Data distinct");
  }

  [Serializable]
  public class BlobStorageMessage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXUIField(DisplayName = "Messages")]
    [PXString]
    public string Messages { get; set; }

    public class messages : IBqlField, IBqlOperand
    {
    }
  }
}
