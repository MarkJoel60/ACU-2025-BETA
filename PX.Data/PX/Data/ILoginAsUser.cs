// Decompiled with JetBrains decompiler
// Type: PX.Data.ILoginAsUser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal interface ILoginAsUser
{
  void LoginAsUser(string userName);

  void LoginAsUser(string userName, string userNameOriginal, ILoginAsUserSession session);

  /// <returns>Non-empty string or <c>null</c> (never returns <see cref="F:System.String.Empty">String.Empty</see>)</returns>
  string GetLoggedAsUserName(ILoginAsUserSession session);

  void RemoveLoggedAsUser(ILoginAsUserSession session);
}
