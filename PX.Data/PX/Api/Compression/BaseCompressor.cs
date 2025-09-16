// Decompiled with JetBrains decompiler
// Type: PX.Api.Compression.BaseCompressor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

#nullable disable
namespace PX.Api.Compression;

/// <summary>Base compressor for compressing streams.</summary>
/// <remarks>
/// Based on the work by:
///     Ben Foster (http://benfoster.io/blog/aspnet-web-api-compression)
///     Kiran Challa (http://blogs.msdn.com/b/kiranchalla/archive/2012/09/04/handling-compression-accept-encoding-sample.aspx)
/// </remarks>
public abstract class BaseCompressor : ICompressor
{
  /// <summary>Gets the encoding type.</summary>
  /// <value>The encoding type.</value>
  public abstract string EncodingType { get; }

  /// <summary>Creates the compression stream.</summary>
  /// <param name="output">The output stream.</param>
  /// <returns>The compressed stream.</returns>
  public abstract Stream CreateCompressionStream(Stream output);

  /// <summary>Creates the decompression stream.</summary>
  /// <param name="input">The input stream.</param>
  /// <returns>The decompressed stream.</returns>
  public abstract Stream CreateDecompressionStream(Stream input);

  public virtual async Task Compress(HttpContent content, Stream destination)
  {
    using (Stream compressed = this.CreateCompressionStream(destination))
      await content.CopyToAsync(compressed);
  }

  public virtual async Task<HttpContent> Decompress(HttpContent compressedContent)
  {
    HttpContent httpContent;
    using (MemoryStream compressedContentStream = new MemoryStream())
    {
      await compressedContent.CopyToAsync((Stream) compressedContentStream);
      using (Stream decompressionStream = this.CreateDecompressionStream((Stream) compressedContentStream))
      {
        StreamContent output = new StreamContent(decompressionStream);
        HttpContentOperations.CopyHeaders(compressedContent, (HttpContent) output);
        httpContent = (HttpContent) output;
      }
    }
    return httpContent;
  }
}
