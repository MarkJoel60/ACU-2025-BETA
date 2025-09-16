// Decompiled with JetBrains decompiler
// Type: PX.Licensing.SigningExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Licensing;

public static class SigningExtensions
{
  public static bool VerifyAssemblyCodeSign(
    this ICodeSigningManager signingManager,
    PXAccessProvider accessProvider)
  {
    return signingManager.VerifyAssemblyCodeSign(accessProvider.GetType().Assembly);
  }
}
