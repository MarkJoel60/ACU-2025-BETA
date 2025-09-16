// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDB3DesCryphStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDB3DesCryphStringAttribute : PXDBCryptStringAttribute
{
  public PXDB3DesCryphStringAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given maximum
  /// length.</summary>
  public PXDB3DesCryphStringAttribute(int length)
    : base(length)
  {
  }

  [Obsolete]
  internal static string Encrypt(string source)
  {
    return Convert.ToBase64String(SitePolicy.TripleDESCryptoProvider.Encrypt(Encoding.Unicode.GetBytes(source)));
  }

  protected override byte[] Encrypt(byte[] source)
  {
    return SitePolicy.TripleDESCryptoProvider.Encrypt(source);
  }

  protected override byte[] Decrypt(byte[] source)
  {
    return SitePolicy.TripleDESCryptoProvider.Decrypt(source);
  }
}
