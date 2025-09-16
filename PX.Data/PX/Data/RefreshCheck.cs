// Decompiled with JetBrains decompiler
// Type: PX.Data.RefreshCheck
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Session;
using System.Web;

#nullable enable
namespace PX.Data;

internal static class RefreshCheck
{
  private static string GetKey(string suffix) => "PxActiveUserID" + suffix;

  internal static void StoreUser(string suffix, string? userName)
  {
    PXContext.Session.SetString(RefreshCheck.GetKey(suffix), userName);
  }

  internal static string? GetStoredUser(string suffix)
  {
    return (string) PXContext.Session[RefreshCheck.GetKey(suffix)];
  }

  /// <summary>Stores the current user ID in session.</summary>
  internal static void StoreCurrentUser(HttpContext context)
  {
    RefreshCheck.StoreCurrentUser(PXSessionStateStore.GetSuffix(context));
  }

  /// <summary>Stores the current user ID in session.</summary>
  internal static void StoreCurrentUser(HttpContextBase context)
  {
    RefreshCheck.StoreCurrentUser(PXSessionStateStore.GetSuffix(context));
  }

  private static void StoreCurrentUser(string suffix)
  {
    RefreshCheck.StoreUser(suffix, RefreshCheck.GetCurrentUser());
  }

  internal static void CopyUser(
    IPXSessionState from,
    string fromSuffix,
    IPXSessionState to,
    string toSuffix)
  {
    to.Set(RefreshCheck.GetKey(toSuffix), from.Get(RefreshCheck.GetKey(fromSuffix)));
  }

  internal static string? GetCurrentUser() => PXAccess.GetFullUserName();
}
