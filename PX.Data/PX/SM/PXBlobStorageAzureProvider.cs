// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBlobStorageAzureProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

public class PXBlobStorageAzureProvider : IBlobStorageProvider
{
  private string Key;
  private string AccountName;
  private string ParamContainer = "files";
  private const string PARAM_ACCOUNT = "Account";
  private const string PARAM_KEY = "Key";
  private const string PARAM_CONTAINER = "Container";
  private CloudBlobClient _BlobClient;
  private CloudBlobContainer _BlobContainer;

  public Guid Save(byte[] data, PXBlobStorageContext saveContext)
  {
    Guid guid = Guid.NewGuid();
    this._BlobContainer.GetBlockBlobReference(guid.ToString()).UploadFromByteArray(data, 0, data.Length, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
    return guid;
  }

  public byte[] Load(Guid id)
  {
    ICloudBlob referenceFromServer = this._BlobContainer.GetBlobReferenceFromServer(id.ToString(), (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
    referenceFromServer.FetchAttributes((AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
    byte[] numArray = new byte[referenceFromServer.Properties.Length];
    referenceFromServer.DownloadToByteArray(numArray, 0, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
    return numArray;
  }

  public void Remove(Guid id)
  {
    this._BlobContainer.GetBlobReferenceFromServer(id.ToString(), (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null).DeleteIfExists((DeleteSnapshotsOption) 0, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
  }

  public void CleanUp(int companyId)
  {
  }

  public IEnumerable<BlobProviderSettings> GetSettings()
  {
    yield return new BlobProviderSettings()
    {
      Name = "Account",
      Value = this.AccountName
    };
    yield return new BlobProviderSettings()
    {
      Name = "Key",
      Value = this.Key
    };
    yield return new BlobProviderSettings()
    {
      Name = "Container",
      Value = this.ParamContainer
    };
  }

  public void Init(IEnumerable<BlobProviderSettings> settings)
  {
    foreach (BlobProviderSettings setting in settings)
    {
      if (string.IsNullOrEmpty(setting.Value))
        throw new PXProviderConfigException($"Configuration property '{setting.Name}' cannot be empty.")
        {
          Row = setting
        };
      if (setting.Name == "Account")
        this.AccountName = setting.Value;
      if (setting.Name == "Key")
        this.Key = setting.Value;
      if (setting.Name == "Container")
        this.ParamContainer = setting.Value.ToLowerInvariant();
    }
    this._BlobClient = new CloudStorageAccount(new StorageCredentials(this.AccountName, this.Key), true).CreateCloudBlobClient();
    this._BlobContainer = this._BlobClient.GetContainerReference(this.ParamContainer);
    this._BlobContainer.CreateIfNotExists((BlobRequestOptions) null, (OperationContext) null);
  }

  public string GetIdentity() => "Azure:" + this.AccountName;
}
