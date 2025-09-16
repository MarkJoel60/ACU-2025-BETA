// Decompiled with JetBrains decompiler
// Type: PX.SM.AmazonS3ClientFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Amazon;
using Amazon.Runtime;
using Amazon.S3;

#nullable disable
namespace PX.SM;

/// <summary>
/// Creates an instance of <see cref="T:Amazon.S3.AmazonS3Client" />
/// </summary>
public class AmazonS3ClientFactory
{
  private readonly string _awsAccessKey;
  private readonly string _awsSecretKey;
  private readonly RegionEndpoint _awsRegionEndpoint;

  /// <summary>
  /// </summary>
  protected AmazonS3ClientFactory()
  {
  }

  /// <summary>
  /// Initializes a new instance of <see cref="T:PX.SM.AmazonS3ClientFactory" />
  /// </summary>
  /// <param name="awsAccessKey"></param>
  /// <param name="awsSecretKey"></param>
  public AmazonS3ClientFactory(
    string awsAccessKey,
    string awsSecretKey,
    RegionEndpoint awsRegionEndpoint)
  {
    this._awsAccessKey = awsAccessKey;
    this._awsSecretKey = awsSecretKey;
    this._awsRegionEndpoint = awsRegionEndpoint;
  }

  /// <summary>
  /// Creates an instance of <see cref="T:Amazon.S3.AmazonS3Client" />
  /// </summary>
  /// <returns></returns>
  public virtual AmazonS3Client CreateProxy()
  {
    AmazonS3Config amazonS3Config = new AmazonS3Config();
    ((ClientConfig) amazonS3Config).RegionEndpoint = this._awsRegionEndpoint;
    ((ClientConfig) amazonS3Config).SignatureVersion = "4";
    return new AmazonS3Client(this._awsAccessKey, this._awsSecretKey, amazonS3Config);
  }
}
