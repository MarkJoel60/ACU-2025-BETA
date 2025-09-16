// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBaseMembershipProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web;
using System.Web.Security;

#nullable enable
namespace PX.Data;

/// <exclude />
[PXInternalUseOnly]
public abstract class PXBaseMembershipProvider : MembershipProvider
{
  protected const 
  #nullable disable
  string _UserChace_Slot_ = "MembershipUser";
  protected internal const 
  #nullable enable
  string mockUserNamePrefix = "AlwaysFailUser_";
  protected internal static readonly string _mockUserName = "AlwaysFailUser_" + Guid.NewGuid().ToString();
  protected internal static readonly string _mockedSavedPassword = Guid.NewGuid().ToString();
  protected internal static readonly string _mockedPassword = PXBaseMembershipProvider._mockedSavedPassword + "Fail";

  private protected 
  #nullable disable
  ILegacyCompanyService LegacyCompanyService { get; private set; }

  public override void Initialize(string name, NameValueCollection config)
  {
    base.Initialize(name, config);
    this.LegacyCompanyService = ProviderBaseDependencyHelper.Resolve<ILegacyCompanyService>();
  }

  public abstract bool ConcurrentUserMode { get; }

  private protected static void AssertNotNull(HttpRequestBase request)
  {
    if (request == null)
      throw new Exception("The system cannot validate the user IP address because the HTTP request is not present.");
  }

  /// <summary>
  /// Verifies that the specified user name and password exist in the data source, and returns login for this username in current provider.
  /// </summary>
  protected internal virtual bool ValidateUser(
    HttpRequestBase request,
    string username,
    string password,
    out string providerLogin)
  {
    PXBaseMembershipProvider.AssertNotNull(request);
    providerLogin = username;
    return this.ValidateUser(username, password);
  }

  /// <param name="onlyAllowed">True to check if user is active/approved/locked out. False, if just password checking is needed.</param>
  protected internal abstract bool ValidateUserPassword(
    string username,
    string password,
    bool onlyAllowed,
    bool skipForbidWithPasswordCheck = false);

  protected internal bool ValidateUserPassword(
    string username,
    string password,
    bool onlyAllowed,
    out string providerUsername)
  {
    return this.ValidateUserPassword(username, password, onlyAllowed, false, out providerUsername);
  }

  protected internal virtual bool ValidateUserPassword(
    string username,
    string password,
    bool onlyAllowed,
    bool skipForbidWithPasswordCheck,
    out string providerUsername)
  {
    providerUsername = username;
    return this.ValidateUserPassword(username, password, onlyAllowed, skipForbidWithPasswordCheck);
  }

  /// <summary>
  /// Updates user password directly without any checks and validation.
  /// </summary>
  protected internal abstract void UpdateUserPassword(string username, string password);

  /// <exclude />
  protected class UsersCache : Dictionary<string, MembershipUserExt>
  {
    public UsersCache()
      : base((IEqualityComparer<string>) PXLocalesProvider.CollationComparer)
    {
    }
  }

  protected readonly struct UserWithPasswordCacheKey(
  #nullable enable
  string? userName, string? dbPassword) : 
    IEquatable<PXBaseMembershipProvider.UserWithPasswordCacheKey>
  {
    public string? UserName { get; } = userName;

    public string? DbPassword { get; } = dbPassword;

    public override bool Equals(object obj)
    {
      return obj is PXBaseMembershipProvider.UserWithPasswordCacheKey other && this.Equals(other);
    }

    public bool Equals(
      PXBaseMembershipProvider.UserWithPasswordCacheKey other)
    {
      return ConstantTimeStringComparer.AreEqual(this.UserName, other.UserName) && ConstantTimeStringComparer.AreEqual(this.DbPassword, other.DbPassword);
    }

    public override int GetHashCode()
    {
      return 23 * (23 * 17 + ConstantTimeStringComparer.GetConstantTimeHashCode(this.UserName, 256 /*0x0100*/)) + ConstantTimeStringComparer.GetConstantTimeHashCode(this.DbPassword, 512 /*0x0200*/);
    }
  }
}
