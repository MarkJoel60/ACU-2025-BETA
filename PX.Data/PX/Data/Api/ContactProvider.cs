// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.ContactProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Security;
using System;
using System.Web.Security;

#nullable disable
namespace PX.Data.Api;

internal class ContactProvider : IContactProvider
{
  private readonly IUserManagementService _userManagementService;

  public ContactProvider(IUserManagementService userManagementService)
  {
    this._userManagementService = userManagementService;
  }

  public string GetEmail(string userName) => this._userManagementService.GetUser(userName)?.Email;

  public Guid? GetUserId(string userName)
  {
    MembershipUser user = this._userManagementService.GetUser(userName);
    return user == null ? new Guid?() : user.GetID();
  }
}
