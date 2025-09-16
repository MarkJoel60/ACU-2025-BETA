// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.PXStorageHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PX.Data.Services;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.Update.Storage;

public static class PXStorageHelper
{
  private static readonly Dictionary<string, System.Type> _providers = new Dictionary<string, System.Type>();

  public static IEnumerable<string> Providers
  {
    get => (IEnumerable<string>) PXStorageHelper._providers.Keys.ToArray<string>();
  }

  static PXStorageHelper()
  {
    List<Assembly> assemblyList = new List<Assembly>();
    assemblyList.Add(Assembly.GetExecutingAssembly());
    IEnumerable<Assembly> collection = ServiceLocator.Current.GetInstance<IStorageService>().LoadCustomStorageProviderAssemblies();
    if (collection != null)
      assemblyList.AddRange(collection);
    foreach (System.Type enumTypesInAssembly in PXSubstManager.EnumTypesInAssemblies(nameof (PXStorageHelper), (List<Exception>) null, false, assemblyList.ToArray()))
    {
      if (enumTypesInAssembly != (System.Type) null && typeof (IStorageProvider).IsAssignableFrom(enumTypesInAssembly) && enumTypesInAssembly != typeof (IStorageProvider))
      {
        IStorageProvider instance = (IStorageProvider) Activator.CreateInstance(enumTypesInAssembly);
        PXStorageHelper._providers.Add(instance.Name, enumTypesInAssembly);
        PXSubstManager.AddTypeToNamedList(nameof (PXStorageHelper), enumTypesInAssembly);
      }
    }
    PXSubstManager.SaveTypeCache(nameof (PXStorageHelper));
  }

  public static bool IsStorageSetup()
  {
    InstallationSetup installationSetup = new InstallationSetup();
    installationSetup.Setup.Current = (UPSetup) installationSetup.Setup.Select();
    return installationSetup.Setup.Current != null && !string.IsNullOrEmpty(installationSetup.Setup.Current.StorageProvider) || PXStorageHelper.GetDefaultProvider() != null;
  }

  public static IStorageProvider GetProvider()
  {
    IStorageProvider defaultProvider = PXStorageHelper.GetDefaultProvider();
    InstallationSetup installationSetup = new InstallationSetup();
    installationSetup.Setup.Current = (UPSetup) installationSetup.Setup.Select();
    if (installationSetup.Setup.Current == null || string.IsNullOrEmpty(installationSetup.Setup.Current.StorageProvider))
      return defaultProvider != null ? defaultProvider : throw new PXException("The storage provider is not set up.");
    IEnumerable<StorageSettings> settings = installationSetup.StorageSettings.Select().AsEnumerable<PXResult<UPStorageParameters>>().Select<PXResult<UPStorageParameters>, UPStorageParameters>((Func<PXResult<UPStorageParameters>, UPStorageParameters>) (p => (UPStorageParameters) p)).Select<UPStorageParameters, StorageSettings>((Func<UPStorageParameters, StorageSettings>) (p => new StorageSettings()
    {
      Key = p.Name,
      Value = p.Value
    }));
    return PXStorageHelper.GetProvider(installationSetup.Setup.Current.StorageProvider, settings);
  }

  internal static IStorageProvider GetProvider(string key)
  {
    return PXStorageHelper.GetProvider(key, (IEnumerable<StorageSettings>) null);
  }

  internal static IStorageProvider GetProvider(string key, IEnumerable<StorageSettings> settings)
  {
    IStorageProvider provider = !string.IsNullOrEmpty(key) ? (IStorageProvider) Activator.CreateInstance(PXStorageHelper._providers[key]) : throw new ArgumentNullException("Provider Name");
    if (settings != null)
      provider.Settings = settings;
    return provider;
  }

  private static IStorageProvider GetDefaultProvider()
  {
    try
    {
      string appSetting = ConfigurationManager.AppSettings["SnapshotsFolder"];
      if (!string.IsNullOrEmpty(appSetting))
      {
        if (Directory.Exists(appSetting))
        {
          LocalStorage defaultProvider = new LocalStorage();
          defaultProvider.Settings = (IEnumerable<StorageSettings>) new StorageSettings[1]
          {
            new StorageSettings() { Key = "Folder", Value = appSetting }
          };
          return (IStorageProvider) defaultProvider;
        }
      }
    }
    catch
    {
    }
    return (IStorageProvider) null;
  }

  internal static AppDataStorage GetAppDataProvider() => new AppDataStorage();

  public static bool Exists(this ICloudBlob blob)
  {
    try
    {
      blob.FetchAttributes((AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
      return true;
    }
    catch (StorageException ex)
    {
      if (ex.RequestInformation.HttpStatusCode == 404)
        return false;
      throw;
    }
  }
}
