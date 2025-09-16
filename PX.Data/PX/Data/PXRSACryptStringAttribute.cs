// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRSACryptStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <summary>The attribute that adds the ability to encrypt a string field.
/// The encryption is performed by <see cref="P:PX.Data.SitePolicy.RSACryptoProvider" />.</summary>
public class PXRSACryptStringAttribute : PXDBCryptStringAttribute
{
  public PXRSACryptStringAttribute()
  {
  }

  /// <summary>Initializes a new instance with the specified maximum
  /// length.</summary>
  public PXRSACryptStringAttribute(int length)
    : base(length)
  {
  }

  /// <summary>
  /// Encrypts the string <paramref name="source" /> (if the encryption is enabled) or
  /// leaves it without modification and returns the result.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public static string Encrypt(string source)
  {
    return !SitePolicy.RSACryptoProvider.IsEncryptEnable ? source : Convert.ToBase64String(SitePolicy.RSACryptoProvider.Encrypt(Encoding.Unicode.GetBytes(source)));
  }

  internal static string Decrypt(string source)
  {
    if (source == null)
      return (string) null;
    byte[] bytes;
    try
    {
      bytes = Convert.FromBase64String(source);
    }
    catch (Exception ex)
    {
      return source;
    }
    return !SitePolicy.RSACryptoProvider.IsDecryptEnable ? Encoding.Unicode.GetString(bytes) : Encoding.Unicode.GetString(SitePolicy.RSACryptoProvider.Decrypt(bytes));
  }

  public virtual bool EncryptOnCertificateReplacement(PXCache cache, object row) => true;

  protected internal override void SetBqlTable(System.Type bqlTable) => base.SetBqlTable(bqlTable);

  protected override byte[] Encrypt(byte[] source)
  {
    return !SitePolicy.RSACryptoProvider.IsEncryptEnable ? source : SitePolicy.RSACryptoProvider.Encrypt(source);
  }

  protected override byte[] Decrypt(byte[] source)
  {
    return !SitePolicy.RSACryptoProvider.IsDecryptEnable ? source : SitePolicy.RSACryptoProvider.Decrypt(source);
  }
}
