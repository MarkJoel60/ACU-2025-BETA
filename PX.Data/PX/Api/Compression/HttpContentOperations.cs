// Decompiled with JetBrains decompiler
// Type: PX.Api.Compression.HttpContentOperations
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

#nullable disable
namespace PX.Api.Compression;

/// <summary>
/// Helper methods for operating on <see cref="T:System.Net.Http.HttpContent" /> instances.
/// </summary>
public class HttpContentOperations
{
  /// <summary>Copies the HTTP headers onto the new response.</summary>
  /// <param name="input">The input.</param>
  /// <param name="output">The output.</param>
  public static void CopyHeaders(HttpContent input, HttpContent output)
  {
    foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) input.Headers)
      output.Headers.TryAddWithoutValidation(header.Key, header.Value);
  }
}
