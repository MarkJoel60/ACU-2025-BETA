// Decompiled with JetBrains decompiler
// Type: PX.Api.Compression.GZipCompressor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.IO;
using System.IO.Compression;

#nullable disable
namespace PX.Api.Compression;

/// <summary>
/// Compressor for handling <c>gzip</c> encodings.
/// </summary>
public class GZipCompressor : BaseCompressor
{
  /// <summary>Gets the encoding type.</summary>
  /// <value>The encoding type.</value>
  public override string EncodingType => "gzip";

  /// <summary>Creates the compression stream.</summary>
  /// <param name="output">The output stream.</param>
  /// <returns>The compressed stream.</returns>
  public override Stream CreateCompressionStream(Stream output)
  {
    return (Stream) new GZipStream(output, CompressionMode.Compress, true);
  }

  /// <summary>Creates the decompression stream.</summary>
  /// <param name="input">The input stream.</param>
  /// <returns>The decompressed stream.</returns>
  public override Stream CreateDecompressionStream(Stream input)
  {
    return (Stream) new GZipStream(input, CompressionMode.Decompress);
  }
}
