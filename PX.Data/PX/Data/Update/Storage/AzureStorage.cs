// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.AzureStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

#nullable disable
namespace PX.Data.Update.Storage;

public class AzureStorage : BaseStorage, IStorageProvider
{
  protected internal const string ACCOUNT = "Account";
  protected internal const string KEY = "Key";
  protected internal const string CONTAINER = "Container";
  private CloudBlobClient _BlobClient;
  private CloudBlobContainer _BlobContainer;

  protected CloudBlobClient BlobClient
  {
    get
    {
      if (this._BlobClient == null || this._BlobContainer == null)
        this.InitBlob();
      return this._BlobClient;
    }
  }

  protected CloudBlobContainer BlobContainer
  {
    get
    {
      if (this._BlobContainer == null || this._BlobContainer == null)
        this.InitBlob();
      return this._BlobContainer;
    }
  }

  private void InitBlob()
  {
    this._BlobClient = new CloudStorageAccount(new StorageCredentials(this.GetParameter("Account"), this.GetParameter("Key")), true).CreateCloudBlobClient();
    this._BlobContainer = this._BlobClient.GetContainerReference(this.GetParameter("Container").ToLower());
    this._BlobContainer.CreateIfNotExists((BlobRequestOptions) null, (OperationContext) null);
  }

  public AzureStorage()
  {
    this.settings.Add(new StorageSettings()
    {
      Key = "Account"
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "Key",
      Password = true
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "Container"
    });
  }

  protected override void ValidateParameters()
  {
    this.ValidateParameters("Account", "Key", "Container");
  }

  public override void Test()
  {
    new CloudStorageAccount(new StorageCredentials(this.GetParameter("Account"), this.GetParameter("Key")), true).CreateCloudBlobClient().GetContainerReference(this.GetParameter("Container").ToLower());
  }

  public override bool Exists(string id)
  {
    try
    {
      ICloudBlob referenceFromServer = this.BlobContainer.GetBlobReferenceFromServer(id, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
      referenceFromServer.FetchAttributes((AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
      return referenceFromServer.Exists((BlobRequestOptions) null, (OperationContext) null);
    }
    catch (StorageException ex)
    {
      if (!((Exception) ex).Message.Contains("404"))
      {
        if (!((Exception) ex).Message.Contains("Not Found"))
          goto label_4;
      }
      return false;
    }
label_4:
    return true;
  }

  public override Stream OpenWrite(string id)
  {
    CloudBlockBlob blob = this.BlobContainer.GetBlockBlobReference(id.ToString());
    MemoryStream stream = new MemoryStream();
    return (Stream) new WrappedStream((Stream) stream, true, true, true, (System.Action) (() =>
    {
      stream.Position = 0L;
      blob.UploadFromStream((Stream) stream, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
    }));
  }

  public override Stream OpenRead(string id)
  {
    return this.BlobContainer.GetBlobReferenceFromServer(id, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null).OpenRead((AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
  }

  public void Delete(Guid id)
  {
    this.BlobContainer.GetBlobReferenceFromServer(id.ToString(), (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null).DeleteIfExists((DeleteSnapshotsOption) 0, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
  }

  public override long GetSize(string id)
  {
    ICloudBlob referenceFromServer = this.BlobContainer.GetBlobReferenceFromServer(id, (AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
    referenceFromServer.FetchAttributes((AccessCondition) null, (BlobRequestOptions) null, (OperationContext) null);
    return referenceFromServer.Properties.Length;
  }
}
