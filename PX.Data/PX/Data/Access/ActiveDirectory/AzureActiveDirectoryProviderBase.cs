// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.AzureActiveDirectoryProviderBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal abstract class AzureActiveDirectoryProviderBase : ActiveDirectoryProvider
{
  protected const string ExternalLoginMark = "#EXT#";

  protected string RootGroup { get; }

  protected bool RootGroupRestrictionEnabled { get; }

  protected static IEnumerable<Login> GetLogins(string samaccountname, IEnumerable<string> domains)
  {
    if (!(domains is string[] strArray))
      strArray = domains.ToArray<string>();
    domains = (IEnumerable<string>) strArray;
    (string str1, string str2) = ActiveDirectoryProvider.ExtractNameDomain(samaccountname, (string) null, domains);
    if (str2 == null)
      return (IEnumerable<Login>) domains.Select<string, Login>((Func<string, Login>) (k => new Login(samaccountname, k))).ToList<Login>();
    return (IEnumerable<Login>) new Login[1]
    {
      new Login(str1, str2)
    };
  }

  protected (string accountName, HashSet<string> allowedDomains) ExtractAccountNameAndDomains(
    string providerUserKey,
    IEnumerable<string> domains)
  {
    if (!(domains is string[] strArray))
      strArray = domains.ToArray<string>();
    domains = (IEnumerable<string>) strArray;
    Login login = ActiveDirectoryProvider.ExtractLogin(providerUserKey, this.DefaultDomainComponent, domains);
    HashSet<string> stringSet = new HashSet<string>(domains, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      login.Domain
    };
    return (login.Name, stringSet);
  }

  protected static string ToExternalAccountName(string externalLogin)
  {
    return !externalLogin.Contains("#EXT#") ? externalLogin.Replace('@', '_') + "#EXT#" : externalLogin;
  }

  protected AzureActiveDirectoryProviderBase(
    Options options,
    ILoggerFactory loggerFactory,
    IExpirationStorage expirationStorage)
    : base(options.Path, options.GetCredential(), options, loggerFactory, expirationStorage)
  {
    this.RootGroup = options.RootGroup;
    this.RootGroupRestrictionEnabled = options.RootGroupRestrictionEnabled;
  }

  protected IEnumerable<string> FilterGroupsByRootGroup(IEnumerable<string> availableGroups)
  {
    if (availableGroups == null || string.IsNullOrEmpty(this.RootGroup) || !this.RootGroupRestrictionEnabled)
      return availableGroups;
    if (!(availableGroups is IReadOnlyCollection<string> strings))
      strings = (IReadOnlyCollection<string>) availableGroups.ToList<string>();
    IReadOnlyCollection<string> source = strings;
    if (!source.Any<string>((Func<string, bool>) (x => x.Equals(this.RootGroup, StringComparison.OrdinalIgnoreCase))))
      return Enumerable.Empty<string>();
    HashSet<string> groupsUnderRootGroup = this.GetGroupsWithoutCaching().Select<Group, string>((Func<Group, string>) (x => x.SID)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    return source.Where<string>((Func<string, bool>) (x => groupsUnderRootGroup.Contains(x)));
  }

  /// <summary>
  /// Returns list of all available groups without caching and with RootGroup restriction
  /// </summary>
  protected abstract IEnumerable<Group> GetGroupsWithoutCaching();

  /// <summary>
  /// Returns user without caching and with RootGroup restriction
  /// </summary>
  protected abstract User GetUserWithoutCaching(string userKey);

  /// <summary>Returns user group identifiers without caching</summary>
  protected abstract HashSet<string> GetUserGroupIdentifiersWithoutCaching(string userId);

  public IEnumerable<Group> GetGroups(bool useCached = true)
  {
    return this.GetCachedGroups((Func<Group[]>) (() => this.GetGroupsWithoutCaching().ToArray<Group>()), useCached);
  }

  public User GetUser(object providerUserKey)
  {
    string userKey = providerUserKey?.ToString();
    return string.IsNullOrEmpty(userKey) ? (User) null : this.GetCachedUser(userKey, (Func<User>) (() => this.GetUserWithoutCaching(userKey)), true);
  }

  public override bool IsUserDisabled(string login)
  {
    if (base.IsUserDisabled(login))
      return true;
    User user = this.GetUser((object) login);
    if (user == null || string.IsNullOrEmpty(this.RootGroup) || !this.RootGroupRestrictionEnabled)
      return false;
    HashSet<string> identifiersWithoutCaching = this.GetUserGroupIdentifiersWithoutCaching(user.SID);
    // ISSUE: explicit non-virtual call
    return identifiersWithoutCaching == null || !__nonvirtual (identifiersWithoutCaching.Contains(this.RootGroup));
  }
}
