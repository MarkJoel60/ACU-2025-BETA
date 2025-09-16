// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBlobStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

public static class PXBlobStorage
{
  [ThreadStatic]
  public static PXBlobStorageContext SaveContext;

  public static string ProviderName()
  {
    return PXBlobStorage.Provider == null ? "" : PXBlobStorage.Provider.GetType().FullName;
  }

  public static void CheckInitError() => PXBlobStorage.CurrentCompanyDefinition.CheckInitError();

  public static void Reset() => PXBlobStorage.CurrentCompanyDefinition.Reset();

  public static IBlobStorageProvider CreateProvider(string name)
  {
    return Str.IsNullOrEmpty(name) ? (IBlobStorageProvider) null : (IBlobStorageProvider) Activator.CreateInstance(PXBuildManager.GetType(name, true));
  }

  public static IBlobStorageProvider Provider => PXBlobStorage.CurrentCompanyDefinition.Provider;

  public static Guid Save(byte[] data)
  {
    PXBlobStorage.CheckInitError();
    if (!PXBlobStorage.IsRemoteStorageEnabled(data))
      throw new PXException("Cannot store data in the provider.");
    return PXBlobStorage.Provider.Save(data, PXBlobStorage.SaveContext);
  }

  public static byte[] Load(Guid id)
  {
    PXBlobStorage.CheckInitError();
    return PXBlobStorage.Provider.Load(id);
  }

  public static bool IsRemoteStorageEnabled(byte[] data) => PXBlobStorage.AllowSave && data != null;

  public static bool IsEnabled => PXBlobStorage.Provider != null;

  public static bool AllowSave => PXBlobStorage.CurrentCompanyDefinition.AllowSave;

  public static IPXFileAttachmentProvider FileAttachmentProvider
  {
    get
    {
      return PXBlobStorage.CurrentCompanyDefinition.HasInitError ? (IPXFileAttachmentProvider) null : PXBlobStorage.Provider as IPXFileAttachmentProvider;
    }
  }

  public static void Remove(Guid id)
  {
    PXBlobStorage.CheckInitError();
    if (WebConfig.DisableDeleteOnExternalFileStorage)
      return;
    PXBlobStorage.Provider.Remove(id);
  }

  private static BlobStorageProviderDefinition CurrentCompanyDefinition
  {
    get
    {
      return PXDatabase.GetSlot<BlobStorageProviderDefinition>("BlobStorageProviderDefinition", typeof (BlobStorageConfig), typeof (BlobProviderSettings));
    }
  }
}
