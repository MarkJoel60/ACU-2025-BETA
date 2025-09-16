// Decompiled with JetBrains decompiler
// Type: PX.Licensing.ICodeSigningManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Reflection;

#nullable disable
namespace PX.Licensing;

/// <summary>Check assembly digital signature for Anti-tampering</summary>
[PXInternalUseOnly]
public interface ICodeSigningManager
{
  /// <summary>
  /// Searching for .sig file with signed assembly hash and verifying assembly hash
  /// </summary>
  /// <param name="assembly">Assembly for checking</param>
  /// <returns></returns>
  bool VerifyAssemblyCodeSign(Assembly assembly);
}
