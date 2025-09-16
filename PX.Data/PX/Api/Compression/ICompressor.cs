// Decompiled with JetBrains decompiler
// Type: PX.Api.Compression.ICompressor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

#nullable disable
namespace PX.Api.Compression;

/// <summary>Interface for stream compressors.</summary>
public interface ICompressor
{
  /// <summary>Gets the encoding type.</summary>
  /// <value>The encoding type.</value>
  string EncodingType { get; }

  Task Compress(HttpContent content, Stream destination);

  Task<HttpContent> Decompress(HttpContent compressedContent);
}
