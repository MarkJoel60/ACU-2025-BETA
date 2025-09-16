// Decompiled with JetBrains decompiler
// Type: System.Web.Security.MembershipUserExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace System.Web.Security;

internal static class MembershipUserExtensions
{
  internal static Guid? GetID(this MembershipUser user) => user.ProviderUserKey as Guid?;

  internal static Guid GetIDOrDefault(this MembershipUser user)
  {
    return CurrentUserInformationProvider.UserIDOrDefault(user != null ? user.GetID() : new Guid?());
  }

  internal static bool IsGuest(this MembershipUser user)
  {
    return user != null && user is MembershipUserExt user1 && MembershipUserExtensions.IsGuest(user1);
  }

  internal static bool IsGuest(this MembershipUserExt user) => user.Guest;
}
