// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBlobStorageS3Provider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using PX.Common;
using PX.Common.Context;
using PX.Data.Wiki.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.SM;

public class PXBlobStorageS3Provider : IBlobStorageProvider
{
  private const string AwsAccessKeyProperty = "AWSAccessKey";
  private const string AwsSecretKeyProperty = "AWSSecretKey";
  private const string AwsRegionEndpoint = "AWSRegionEndpoint";
  private const string BucketUncProperty = "BucketName";
  private const string PathPrefixProperty = "PathPrefix";
  private string _awsAccessKey;
  private string _awsSecretKey;
  private string _awsRegionEndpoint;
  private string _bucketUnc;
  private string _pathPrefix;
  private AmazonS3ClientFactory _factory;

  /// <summary>Saves data to the S3 storage bucket</summary>
  /// <param name="data">Actual data to be saved to AWS</param>
  /// <param name="context"></param>
  /// <returns></returns>
  public Guid Save(byte[] data, PXBlobStorageContext saveContext)
  {
    using (AmazonS3Client proxy = this.Factory.CreateProxy())
    {
      Guid id = Guid.NewGuid();
      PutObjectRequest putObjectRequest = new PutObjectRequest()
      {
        BucketName = this._bucketUnc,
        Key = this.CreateKeyToObject(id),
        InputStream = (Stream) new MemoryStream(data)
      };
      if (saveContext.FileInfo != null && !string.IsNullOrEmpty(saveContext.FileInfo.Extansion))
        putObjectRequest.ContentType = MimeTypes.GetMimeType("." + saveContext.FileInfo.Extansion);
      this.AddMetaData(putObjectRequest.Metadata, saveContext);
      proxy.PutObject(putObjectRequest);
      return id;
    }
  }

  /// <summary>
  /// Adds metdata data to the specified medata data collection.
  /// </summary>
  /// <param name="metadata"></param>
  /// <param name="context"></param>
  private void AddMetaData(MetadataCollection metadata, PXBlobStorageContext context)
  {
    if (context.FileInfo != null)
    {
      if (!context.FileInfo.Name.Any<char>((Func<char, bool>) (_ => _ > 'ÿ')))
        metadata.Add("Name", new string(context.FileInfo.Name.Where<char>((Func<char, bool>) (_ => _ < '\u0080')).ToArray<char>()));
      metadata.Add("ScreenID", context.FileInfo.PrimaryScreenID);
      metadata.Add("FileRevisionID", context.FileInfo.FileRevisionID.GetValueOrDefault().ToString());
      if (context.FileInfo.tstamp != null)
        metadata.Add("Timestamp", context.FileInfo.tstamp.ToString());
      if (context.NoteID.HasValue)
        metadata.Add("NoteID", context.NoteID.GetValueOrDefault().ToString());
    }
    metadata.Add("FileID", context.Revision.FileID.ToString());
    metadata.Add("OriginalName", context.Revision.OriginalName);
  }

  /// <summary>
  /// Loads the object from the remote store by the specified object identifier
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public byte[] Load(Guid id)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (AmazonS3Client proxy = this.Factory.CreateProxy())
      {
        GetObjectRequest getObjectRequest = new GetObjectRequest()
        {
          BucketName = this._bucketUnc,
          Key = this.CreateKeyToObject(id)
        };
        BufferedStream bufferedStream = new BufferedStream(((StreamResponse) proxy.GetObject(getObjectRequest)).ResponseStream);
        byte[] buffer = new byte[8192 /*0x2000*/];
        int count;
        while ((count = bufferedStream.Read(buffer, 0, buffer.Length)) > 0)
          memoryStream.Write(buffer, 0, count);
      }
      return memoryStream.ToArray();
    }
  }

  /// <summary>Removes the object from the remote storage</summary>
  /// <param name="id"></param>
  public void Remove(Guid id)
  {
    using (AmazonS3Client proxy = this.Factory.CreateProxy())
    {
      DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest()
      {
        BucketName = this._bucketUnc,
        Key = this.CreateKeyToObject(id)
      };
      proxy.DeleteObject(deleteObjectRequest);
    }
  }

  public void CleanUp(int companyId)
  {
  }

  /// <summary>
  /// Generates an AWS access key for a given file object. The key may be used to access the object in AWS
  /// </summary>
  /// <param name="id">GUID identifier of the file being processed</param>
  /// <returns></returns>
  private string CreateKeyToObject(Guid id)
  {
    return $"{this._pathPrefix}/{SlotStore.Instance.GetSingleCompanyId().ToString().PadLeft(10, '0')}/{id}";
  }

  public IEnumerable<BlobProviderSettings> GetSettings()
  {
    yield return new BlobProviderSettings()
    {
      Name = "AWSAccessKey",
      Value = this._awsAccessKey
    };
    yield return new BlobProviderSettings()
    {
      Name = "AWSSecretKey",
      Value = this._awsSecretKey
    };
    yield return new BlobProviderSettings()
    {
      Name = "AWSRegionEndpoint",
      Value = this._awsRegionEndpoint
    };
    yield return new BlobProviderSettings()
    {
      Name = "BucketName",
      Value = this._bucketUnc
    };
    yield return new BlobProviderSettings()
    {
      Name = "PathPrefix",
      Value = this._pathPrefix
    };
  }

  /// <summary>Initializes this instance of the provider</summary>
  /// <param name="settings"></param>
  public void Init(IEnumerable<BlobProviderSettings> settings)
  {
    foreach (BlobProviderSettings setting in settings)
    {
      if (string.IsNullOrEmpty(setting.Value))
        throw new PXProviderConfigException($"Configuration property '{setting.Name}' cannot be empty.")
        {
          Row = setting
        };
      switch (setting.Name)
      {
        case "AWSAccessKey":
          this._awsAccessKey = setting.Value;
          continue;
        case "AWSSecretKey":
          this._awsSecretKey = setting.Value;
          continue;
        case "AWSRegionEndpoint":
          this._awsRegionEndpoint = setting.Value;
          continue;
        case "BucketName":
          this._bucketUnc = setting.Value;
          continue;
        case "PathPrefix":
          this._pathPrefix = setting.Value;
          continue;
        default:
          throw new ArgumentException("Unknown parameter");
      }
    }
  }

  public string GetIdentity() => "S3:" + this._bucketUnc;

  /// <summary>
  /// Creates an instance of <see cref="T:PX.SM.AmazonS3ClientFactory" />
  /// </summary>
  /// <returns></returns>
  private AmazonS3ClientFactory CreateFactory()
  {
    return new AmazonS3ClientFactory(this._awsAccessKey, this._awsSecretKey, RegionEndpoint.GetBySystemName(this._awsRegionEndpoint));
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
}
