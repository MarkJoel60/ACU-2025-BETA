// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXCriptoHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace PX.Data.Update;

public static class PXCriptoHelper
{
  private static string PublicKeyResourceName = "PX.Data.public.snk";

  internal static bool ValidateHash(byte[] calculatedHash, byte[] signatureHash)
  {
    RSAParameters parameters = new RSAParameters();
    byte[] array;
    using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(PXCriptoHelper.PublicKeyResourceName))
    {
      using (MemoryStream destination = new MemoryStream())
      {
        manifestResourceStream.CopyTo((Stream) destination);
        array = destination.ToArray();
      }
    }
    byte[] dst1 = new byte[4];
    Buffer.BlockCopy((Array) array, 28, (Array) dst1, 0, 4);
    Array.Reverse((Array) dst1);
    parameters.Exponent = dst1;
    byte[] dst2 = new byte[128 /*0x80*/];
    Buffer.BlockCopy((Array) array, 32 /*0x20*/, (Array) dst2, 0, 128 /*0x80*/);
    Array.Reverse((Array) dst2);
    parameters.Modulus = dst2;
    RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
    cryptoServiceProvider.ImportParameters(parameters);
    return cryptoServiceProvider.VerifyHash(calculatedHash, CryptoConfig.MapNameToOID("SHA1"), signatureHash);
  }

  internal static byte[] CalculateMD5(string str)
  {
    return str == null ? (byte[]) null : new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(str));
  }

  internal static string CalculateMD5String(string str)
  {
    return PXCriptoHelper.ConvertBytes(PXCriptoHelper.CalculateMD5(str));
  }

  internal static string CalculateMD5LocalizationString(string str)
  {
    return PXCriptoHelper.CalculateMD5String(str.ToLower());
  }

  public static byte[] CalculateSHA(string str)
  {
    return str == null ? (byte[]) null : new SHA1CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(str));
  }

  internal static string CalculateSHAString(string str)
  {
    return PXCriptoHelper.ConvertBytes(PXCriptoHelper.CalculateSHA(str));
  }

  internal static string ConvertBytes(byte[] bytes) => PXCriptoHelper.ConvertBytes(bytes, 0);

  /// <remarks>Will insert a separator (<c>-</c>) every <paramref name="chars" /> characters</remarks>
  internal static string ConvertBytes(byte[] bytes, int chars)
  {
    if (bytes == null)
      return (string) null;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < bytes.Length; ++index)
    {
      if (index > 0 && chars > 0 && index % chars == 0)
        stringBuilder.Append("-");
      stringBuilder.Append(bytes[index].ToString("X2"));
    }
    return stringBuilder.ToString();
  }
}
