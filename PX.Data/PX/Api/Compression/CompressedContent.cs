// Decompiled with JetBrains decompiler
// Type: PX.Api.Compression.CompressedContent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

#nullable disable
namespace PX.Api.Compression;

/// <summary>Represents compressed HTTP content.</summary>
public class CompressedContent : HttpContent
{
  /// <summary>The original content</summary>
  private readonly HttpContent _originalContent;
  /// <summary>The compressor</summary>
  private readonly ICompressor _compressor;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Api.Compression.CompressedContent" /> class.
  /// </summary>
  /// <param name="content">The content.</param>
  /// <param name="compressor">The compressor.</param>
  public CompressedContent(HttpContent content, ICompressor compressor)
  {
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    if (compressor == null)
      throw new ArgumentNullException(nameof (compressor));
    this._originalContent = content;
    this._compressor = compressor;
    HttpContentOperations.CopyHeaders(this._originalContent, (HttpContent) this);
    this.Headers.ContentLength = new long?();
    this.Headers.ContentEncoding.Clear();
    this.Headers.ContentEncoding.Add(compressor.EncodingType);
  }

  /// <summary>
  /// Determines whether the HTTP content has a valid length in bytes.
  /// </summary>
  /// <param name="length">The length in bytes of the HTTP content.</param>
  /// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="length" /> is a valid length; otherwise, false.</returns>
  protected override bool TryComputeLength(out long length)
  {
    length = -1L;
    return false;
  }

  /// <summary>serialize to stream as an asynchronous operation.</summary>
  /// <param name="stream">The target stream.</param>
  /// <param name="context">Information about the transport (channel binding token, for example). This parameter may be null.</param>
  /// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
  protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
  {
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    await this._compressor.Compress(this._originalContent, stream).ConfigureAwait(false);
  }

  /// <summary>
  /// Releases the unmanaged resources used by the <see cref="T:System.Net.Http.HttpContent" /> and optionally disposes of the managed resources.
  /// </summary>
  /// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources.</param>
  protected override void Dispose(bool disposing)
  {
    this._originalContent.Dispose();
    base.Dispose(disposing);
  }
}
