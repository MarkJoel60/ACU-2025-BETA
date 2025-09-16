// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.AmazonS3Storage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using PX.Common;
using PX.Common.Context;
using PX.SM;
using System;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Data.Update.Storage;

public class AmazonS3Storage : BaseStorage, IStorageProvider
{
  private const string AwsAccessKeyProperty = "AWSAccessKey";
  private const string AwsSecretKeyProperty = "AWSSecretKey";
  private const string AwsRegionEndpoint = "AWSRegionEndpoint";
  private const string BucketUncProperty = "BucketName";
  private const string PathPrefixProperty = "PathPrefix";
  private AmazonS3ClientFactory _factory;
  private readonly string _tempFolder;

  public AmazonS3Storage()
  {
    this.settings.Add(new StorageSettings()
    {
      Key = "AWSAccessKey",
      Password = true
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "AWSSecretKey",
      Password = true
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "AWSRegionEndpoint"
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "BucketName"
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "PathPrefix"
    });
    this._tempFolder = Path.Combine(PXSnapshotBase.GetSnapshotsFolderPath(), "S3temp");
    if (Directory.Exists(this._tempFolder))
      return;
    Directory.CreateDirectory(this._tempFolder);
  }

  /// <summary>Deletes file</summary>
  /// <param name="id"></param>
  public void Delete(Guid id) => this.Delete(this.GuidToStringId(id));

  public T tryCatchAmazonException<T>(Func<T> method)
  {
    try
    {
      return method();
    }
    catch (AmazonS3Exception ex)
    {
      throw new PXException($"{((Exception) ex).Message}{Environment.NewLine}at:{Environment.NewLine}{((Exception) ex).StackTrace}");
    }
  }

  public void tryCatchAmazonException(System.Action method)
  {
    this.tryCatchAmazonException<int>((Func<int>) (() =>
    {
      method();
      return 1;
    }));
  }

  /// <summary>Test connection to the remote service</summary>
  public override void Test()
  {
    this.tryCatchAmazonException((System.Action) (() =>
    {
      using (AmazonS3Client proxy = this.Factory.CreateProxy())
      {
        ListObjectsResponse listObjectsResponse = proxy.ListObjects(new ListObjectsRequest()
        {
          BucketName = this.GetParameter("BucketName"),
          Prefix = this.GetRootPath()
        });
        if (listObjectsResponse == null)
          return;
        int httpStatusCode = (int) ((AmazonWebServiceResponse) listObjectsResponse).HttpStatusCode;
      }
    }));
  }

  /// <summary>Checks if the specified S3 object exits in AWS S3</summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override bool Exists(string id)
  {
    return this.tryCatchAmazonException<bool>((Func<bool>) (() =>
    {
      using (AmazonS3Client proxy = this.Factory.CreateProxy())
      {
        ListObjectsResponse listObjectsResponse = proxy.ListObjects(new ListObjectsRequest()
        {
          BucketName = this.GetParameter("BucketName"),
          Prefix = this.CreateKeyToObject(id)
        });
        return listObjectsResponse != null && listObjectsResponse.S3Objects.Count > 0;
      }
    }));
  }

  /// <summary>Opens stream for writing to S3 object.</summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override Stream OpenWrite(string id)
  {
    FileStream stream = new FileStream(Path.Combine(this._tempFolder, Guid.NewGuid().ToString("N")), FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192 /*0x2000*/, FileOptions.DeleteOnClose);
    return (Stream) new WrappedStream((Stream) stream, true, true, true, (System.Action) (() => this.UploadFromStream(this.CreateKeyToObject(id), (Stream) stream)));
  }

  /// <summary>Opens file for reading by the specified identifier</summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override Stream OpenRead(string id)
  {
    MemoryStream memoryStream = new MemoryStream();
    this.DownloadToStream(this.CreateKeyToObject(id), (Stream) memoryStream);
    return (Stream) memoryStream;
  }

  /// <summary>Gets the size of the S3 object</summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override long GetSize(string id)
  {
    return this.tryCatchAmazonException<long>((Func<long>) (() =>
    {
      using (AmazonS3Client proxy = this.Factory.CreateProxy())
      {
        ListObjectsResponse listObjectsResponse = proxy.ListObjects(new ListObjectsRequest()
        {
          BucketName = this.GetParameter("BucketName"),
          Prefix = this.CreateKeyToObject(id)
        });
        if (listObjectsResponse != null)
        {
          if (listObjectsResponse.S3Objects.Count > 0)
            return listObjectsResponse.S3Objects.First<S3Object>().Size;
        }
      }
      return 0;
    }));
  }

  /// <summary>
  /// Returns name for root folder, all files of this company will placed at this root
  /// </summary>
  private string GetRootPath()
  {
    int? singleCompanyId = SlotStore.Instance.GetSingleCompanyId();
    return $"{this.GetParameter("PathPrefix")}/{singleCompanyId.ToString().PadLeft(10, '0')}/{"Snapshots"}";
  }

  /// <summary>
  /// Generates an AWS access key for a given file object. The key may be used to access the object in AWS
  /// </summary>
  /// <param name="id">identifier of the file being processed</param>
  /// <returns></returns>
  private string CreateKeyToObject(string id) => $"{this.GetRootPath()}/{id}";

  /// <summary>Deletes S3 object by the specified key</summary>
  /// <param name="key"></param>
  public void Delete(string id) => this.DeleteIfExits(this.CreateKeyToObject(id));

  /// <summary>
  /// Downloads the S3 object by the specified key into the specified stream
  /// </summary>
  /// <param name="key"></param>
  /// <param name="stream"></param>
  private void DownloadToStream(string key, Stream stream)
  {
    this.tryCatchAmazonException((System.Action) (() =>
    {
      using (AmazonS3Client proxy = this.Factory.CreateProxy())
      {
        GetObjectRequest getObjectRequest = new GetObjectRequest()
        {
          BucketName = this.GetParameter("BucketName"),
          Key = key
        };
        BufferedStream bufferedStream = new BufferedStream(((StreamResponse) proxy.GetObject(getObjectRequest)).ResponseStream);
        byte[] buffer = new byte[8192 /*0x2000*/];
        int count;
        while ((count = bufferedStream.Read(buffer, 0, buffer.Length)) > 0)
          stream.Write(buffer, 0, count);
      }
    }));
  }

  /// <summary>Uploads the stream to S3 storage.</summary>
  /// <param name="key"></param>
  /// <param name="source"></param>
  private void UploadFromStream(string key, Stream source)
  {
    this.tryCatchAmazonException((System.Action) (() =>
    {
      using (AmazonS3Client proxy = this.Factory.CreateProxy())
      {
        source.Position = 0L;
        new TransferUtility((IAmazonS3) proxy).Upload(source, this.GetParameter("BucketName"), key);
      }
    }));
  }

  /// <summary>Deletes S3 object by the specified key</summary>
  /// <param name="key"></param>
  private void DeleteIfExits(string key)
  {
    this.tryCatchAmazonException((System.Action) (() =>
    {
      using (AmazonS3Client proxy = this._factory.CreateProxy())
      {
        DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest()
        {
          BucketName = this.GetParameter("BucketName"),
          Key = key
        };
        proxy.DeleteObject(deleteObjectRequest);
      }
    }));
  }

  /// <summary>
  /// Gets instance of <see cref="T:PX.SM.AmazonS3ClientFactory" />
  /// </summary>
  private AmazonS3ClientFactory Factory
  {
    get
    {
      if (this._factory == null)
      {
        this._factory = this.CreateFactory();
        if (this._factory == null)
          throw new Exception("CreateFactory() must not return null.");
      }
      return this._factory;
    }
  }

  /// <summary>
  /// Creates an instance of <see cref="T:PX.SM.AmazonS3ClientFactory" />
  /// </summary>
  /// <returns></returns>
  protected virtual AmazonS3ClientFactory CreateFactory()
  {
    return new AmazonS3ClientFactory(this.GetParameter("AWSAccessKey"), this.GetParameter("AWSSecretKey"), RegionEndpoint.GetBySystemName(this.GetParameter("AWSRegionEndpoint")));
  }
}
