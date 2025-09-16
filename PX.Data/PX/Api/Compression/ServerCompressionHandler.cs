// Decompiled with JetBrains decompiler
// Type: PX.Api.Compression.ServerCompressionHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Api.Compression;

/// <summary>
/// Message handler for handling gzip/deflate requests/responses on a <see cref="T:System.Web.Http.HttpServer" />.
/// </summary>
public class ServerCompressionHandler : DelegatingHandler
{
  /// <summary>The content size threshold before compressing.</summary>
  private readonly int _contentSizeThreshold;
  /// <summary>The HTTP content operations</summary>
  private readonly HttpContentOperations _httpContentOperations;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Api.Compression.ServerCompressionHandler" /> class.
  /// </summary>
  /// <param name="compressors">The compressors.</param>
  public ServerCompressionHandler(params ICompressor[] compressors)
    : this((HttpMessageHandler) null, 860, compressors)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Api.Compression.ServerCompressionHandler" /> class.
  /// </summary>
  /// <param name="contentSizeThreshold">The content size threshold before compressing.</param>
  /// <param name="compressors">The compressors.</param>
  public ServerCompressionHandler(int contentSizeThreshold, params ICompressor[] compressors)
    : this((HttpMessageHandler) null, contentSizeThreshold, compressors)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Api.Compression.ServerCompressionHandler" /> class.
  /// </summary>
  /// <param name="innerHandler">The inner handler.</param>
  /// <param name="compressors">The compressors.</param>
  public ServerCompressionHandler(HttpMessageHandler innerHandler, params ICompressor[] compressors)
    : this(innerHandler, 860, compressors)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Api.Compression.ServerCompressionHandler" /> class.
  /// </summary>
  /// <param name="innerHandler">The inner handler.</param>
  /// <param name="contentSizeThreshold">The content size threshold before compressing.</param>
  /// <param name="compressors">The compressors.</param>
  public ServerCompressionHandler(
    HttpMessageHandler innerHandler,
    int contentSizeThreshold,
    params ICompressor[] compressors)
  {
    if (innerHandler != null)
      this.InnerHandler = innerHandler;
    this.Compressors = (ICollection<ICompressor>) compressors;
    this._contentSizeThreshold = contentSizeThreshold;
    this._httpContentOperations = new HttpContentOperations();
  }

  /// <summary>Gets the compressors.</summary>
  /// <value>The compressors.</value>
  public ICollection<ICompressor> Compressors { get; private set; }

  /// <summary>send as an asynchronous operation.</summary>
  /// <param name="request">The HTTP request message to send to the server.</param>
  /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
  /// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.</returns>
  protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
  {
    if (request.Content != null && request.Content.Headers.ContentEncoding.Any<string>())
      await this.DecompressRequest(request);
    HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    if (response.Content != null && request.Headers.AcceptEncoding.Any<StringWithQualityHeaderValue>())
      this.CompressResponse(request, response);
    return response;
  }

  /// <summary>Compresses the content.</summary>
  /// <param name="request">The request.</param>
  /// <param name="response">The response.</param>
  /// <returns>An async void.</returns>
  private void CompressResponse(HttpRequestMessage request, HttpResponseMessage response)
  {
    ICompressor compressor = request.Headers.AcceptEncoding.Select(encoding => new
    {
      encoding = encoding,
      quality = encoding.Quality ?? 1.0
    }).Where(_param1 => _param1.quality > 0.0).Join((IEnumerable<ICompressor>) this.Compressors, _param1 => _param1.encoding.Value.ToLowerInvariant(), (Func<ICompressor, string>) (c => c.EncodingType.ToLowerInvariant()), (_param1, c) => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      c = c
    }).OrderByDescending(_param1 => _param1.\u003C\u003Eh__TransparentIdentifier0.quality).Select(_param1 => _param1.c).FirstOrDefault<ICompressor>();
    if (compressor == null)
      return;
    try
    {
      if (this._contentSizeThreshold != 0 && response.Content.Headers.ContentLength.HasValue)
      {
        if (this._contentSizeThreshold <= 0)
          return;
        long? contentLength = response.Content.Headers.ContentLength;
        long contentSizeThreshold = (long) this._contentSizeThreshold;
        if (!(contentLength.GetValueOrDefault() >= contentSizeThreshold & contentLength.HasValue))
          return;
      }
      response.Content = (HttpContent) new CompressedContent(response.Content, compressor);
    }
    catch (Exception ex)
    {
      throw new Exception($"Unable to compress the response by using the compressor '{compressor.GetType()}'", ex);
    }
  }

  /// <summary>Decompresses the request.</summary>
  /// <param name="request">The request.</param>
  /// <returns>An async void.</returns>
  private async Task DecompressRequest(HttpRequestMessage request)
  {
    string encoding = request.Content.Headers.ContentEncoding.First<string>();
    ICompressor compressor = this.Compressors.FirstOrDefault<ICompressor>((Func<ICompressor, bool>) (c => c.EncodingType.Equals(encoding, StringComparison.OrdinalIgnoreCase)));
    if (compressor == null)
    {
      compressor = (ICompressor) null;
    }
    else
    {
      try
      {
        HttpRequestMessage httpRequestMessage = request;
        httpRequestMessage.Content = await compressor.Decompress(request.Content).ConfigureAwait(false);
        httpRequestMessage = (HttpRequestMessage) null;
        compressor = (ICompressor) null;
      }
      catch (Exception ex)
      {
        throw new Exception($"Unable to decompress the request by using the compressor '{compressor.GetType()}'", ex);
      }
    }
  }

  internal static bool TryConfigure(IConfiguration configuration, out int compressionThreshold)
  {
    if (ConfigurationBinder.GetValue<bool>(configuration, "EnableCompression", true))
    {
      compressionThreshold = ConfigurationBinder.GetValue<int>(configuration, "CompressionThreshold", 0);
      return true;
    }
    compressionThreshold = 0;
    return false;
  }
}
