// Decompiled with JetBrains decompiler
// Type: PX.SM.InstallationSetup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update;
using PX.Data.Update.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.SM;

public class InstallationSetup : PXGraph<InstallationSetup>
{
  public PXSelect<UPSetup> Setup;
  public PXSelect<UPStorageParameters, Where<UPStorageParameters.storageProvider, Equal<Current<UPSetup.storageProvider>>>> StorageSettings;
  public PXSave<UPSetup> Save;
  public PXCancel<UPSetup> Cancel;
  public PXAction<UPSetup> ReloadParameters;

  public InstallationSetup()
  {
    this.StorageSettings.Cache.AllowDelete = false;
    this.StorageSettings.Cache.AllowInsert = false;
  }

  [PXButton(Tooltip = "Load parameters for the selected provider.")]
  [PXUIField(DisplayName = "Reload Parameters", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable reloadParameters(PXAdapter adapter)
  {
    UPSetup current = this.Setup.Current;
    if (current == null || current.StorageProvider == null)
      return adapter.Get();
    foreach (PXResult<UPStorageParameters> pxResult in this.StorageSettings.Select())
      this.StorageSettings.Delete((UPStorageParameters) pxResult);
    List<PX.Data.Update.Storage.StorageSettings> source = new List<PX.Data.Update.Storage.StorageSettings>(PXStorageHelper.GetProvider(current.StorageProvider).Settings);
    foreach (PXResult<UPStorageParameters> pxResult in this.StorageSettings.Select())
    {
      UPStorageParameters par = (UPStorageParameters) pxResult;
      PX.Data.Update.Storage.StorageSettings storageSettings = source.FirstOrDefault<PX.Data.Update.Storage.StorageSettings>((Func<PX.Data.Update.Storage.StorageSettings, bool>) (o => o.Key == par.Name));
      if (storageSettings == null)
        this.StorageSettings.Delete(par);
      else
        source.Remove(storageSettings);
    }
    foreach (PX.Data.Update.Storage.StorageSettings storageSettings in source)
      this.StorageSettings.Insert(new UPStorageParameters()
      {
        StorageProvider = current.StorageProvider,
        Name = storageSettings.Key,
        Value = storageSettings.Value
      });
    return adapter.Get();
  }

  protected virtual void UPSetup_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    UPSetup row = (UPSetup) e.Row;
    if (row == null)
      return;
    PXCache cache1 = cache;
    UPSetup data1 = row;
    bool? updateEnabled = row.UpdateEnabled;
    int num1 = updateEnabled.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<UPSetup.updateServer>(cache1, (object) data1, num1 != 0);
    PXCache cache2 = cache;
    UPSetup data2 = row;
    updateEnabled = row.UpdateEnabled;
    int num2 = updateEnabled.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<UPSetup.updateNotification>(cache2, (object) data2, num2 != 0);
    this.ReloadParameters.SetEnabled(!string.IsNullOrEmpty(row.StorageProvider));
  }

  protected virtual void UPSetup_StorageProvider_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is UPSetup row) || row.StorageProvider == null)
    {
      foreach (PXResult<UPStorageParameters> pxResult in this.StorageSettings.Select())
        this.StorageSettings.Delete((UPStorageParameters) pxResult);
    }
    else
      this.reloadParameters(new PXAdapter(this.Setup.View));
  }

  protected virtual void UPSetup_UpdateServer_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is UPSetup row) || !row.UpdateEnabled.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(e.NewValue as string))
      return;
    try
    {
      PXUpdateServer.CheckConnection(e.NewValue.ToString());
    }
    catch (Exception ex)
    {
      throw new PXSetPropertyException<UPSetup.updateServer>($"The connection to Acumatica Update Server failed.{Environment.NewLine}{ex.Message}");
    }
  }

  protected virtual void UPSetup_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is UPSetup row))
      return;
    if (string.IsNullOrEmpty(row.StorageProvider))
      return;
    try
    {
      IEnumerable<PX.Data.Update.Storage.StorageSettings> settings = this.StorageSettings.Select().AsEnumerable<PXResult<UPStorageParameters>>().Select<PXResult<UPStorageParameters>, UPStorageParameters>((Func<PXResult<UPStorageParameters>, UPStorageParameters>) (p => (UPStorageParameters) p)).Select<UPStorageParameters, PX.Data.Update.Storage.StorageSettings>((Func<UPStorageParameters, PX.Data.Update.Storage.StorageSettings>) (p => new PX.Data.Update.Storage.StorageSettings()
      {
        Key = p.Name,
        Value = p.Value
      }));
      PXStorageHelper.GetProvider(row.StorageProvider, settings).Test();
    }
    catch (Exception ex)
    {
      this.Setup.Cache.RaiseExceptionHandling<UPSetup.storageProvider>((object) row, (object) row.StorageProvider, ex);
      throw;
    }
  }

  protected virtual void UPStorageParameters_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    UPSetup current = this.Setup.Current;
    if (current == null || current.StorageProvider == null)
      return;
    UPStorageParameters row = (UPStorageParameters) e.Row;
    if (row == null || row.Name == null)
      return;
    PX.Data.Update.Storage.StorageSettings storageSettings = new List<PX.Data.Update.Storage.StorageSettings>(PXStorageHelper.GetProvider(current.StorageProvider).Settings).FirstOrDefault<PX.Data.Update.Storage.StorageSettings>((Func<PX.Data.Update.Storage.StorageSettings, bool>) (s => s.Key == row.Name));
    if (storageSettings == null || !storageSettings.Password)
      return;
    PXStringState instance = PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(true), "Value", new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null) as PXStringState;
    instance.IsPassword = true;
    instance.Value = e.ReturnValue;
    e.ReturnState = (object) instance;
    e.Cancel = true;
  }

  internal static void CheckCredentials(PXCredentials credentials)
  {
    if (string.IsNullOrWhiteSpace(credentials.UserName) && string.IsNullOrWhiteSpace(credentials.Password))
      throw new PXException("The elevated access cannot be obtained under the specified login.");
    try
    {
      using (new PXImpersonationScope(credentials))
      {
        using (FileStream fileStream = new FileStream(Path.Combine(PXInstanceHelper.RootFolder, "web.config"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
          fileStream.ReadByte();
      }
    }
    catch (Exception ex)
    {
      throw new PXException($"The elevated access cannot be obtained under the specified login.{Environment.NewLine}{ex.Message}", ex);
    }
  }
}
