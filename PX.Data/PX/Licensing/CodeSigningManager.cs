// Decompiled with JetBrains decompiler
// Type: PX.Licensing.CodeSigningManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Hosting;
using PX.Common;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

#nullable disable
namespace PX.Licensing;

[PXInternalUseOnly]
public sealed class CodeSigningManager : ICodeSigningManager
{
  private const string PublicKeyResourceKey = "PX.Data.Resources.public.pem";
  private const string SignatureFileExtension = "sig";
  private const string BinFolder = "Bin";
  private readonly RSACryptoServiceProvider _cryptoServiceProvider;
  private readonly IHostEnvironment _hostEnvironment;

  public CodeSigningManager(IHostEnvironment hostEnvironment)
  {
    this._hostEnvironment = hostEnvironment;
    byte[] publicKeyFromResource = this.GetPublicKeyFromResource("PX.Data.Resources.public.pem");
    this._cryptoServiceProvider = new RSACryptoServiceProvider();
    this._cryptoServiceProvider.ImportCspBlob(publicKeyFromResource);
  }

  public bool VerifyAssemblyCodeSign(Assembly assembly)
  {
    string path = Path.ChangeExtension(this._hostEnvironment.ContentRootFileProvider.GetFileInfo(Path.Combine("Bin", assembly.GetName().Name + ".dll")).PhysicalPath, "sig");
    if (!File.Exists(path))
      return false;
    byte[] buffer = File.ReadAllBytes(assembly.Location);
    byte[] signature = File.ReadAllBytes(path);
    return this._cryptoServiceProvider.VerifyData(buffer, (object) SHA256.Create(), signature);
  }

  private byte[] GetPublicKeyFromResource(string resourceKey)
  {
    Stream manifestResourceStream = typeof (CodeSigningManager).Assembly.GetManifestResourceStream(resourceKey);
    if (manifestResourceStream == null)
      throw new NullReferenceException("Cannot get resource stream from PX.Data assembly");
    using (Stream stream = manifestResourceStream)
    {
      using (MemoryStream destination = new MemoryStream())
      {
        stream.CopyTo((Stream) destination);
        return destination.ToArray();
      }
    }
  }
}
