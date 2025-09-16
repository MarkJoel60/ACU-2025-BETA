// Decompiled with JetBrains decompiler
// Type: PX.Data.ILegacyCompanyService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// This service follows the same single/multi-tenant handling logic that <see cref="T:PX.Data.PXAccess" /> used to have since ~2013:
/// when we are in <b>single-tenant mode</b>, tenant list is empty, so parsing any login in <c>login@tenant</c> form
/// will always treat any tenant as non-existing, returning the whole thing as username (and <see langword="null" /> in tenant)
/// </summary>
public interface ILegacyCompanyService
{
  /// <remarks>
  /// <paramref name="company" /> will always be <see langword="null" /> in single-tenant deployments
  /// </remarks>
  void ParseLogin(string login, out string username, out string company, out string branch);
}
