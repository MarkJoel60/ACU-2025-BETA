// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ADALActiveDirectoryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal sealed class ADALActiveDirectoryProvider(
  IOptions<PX.Data.Access.ActiveDirectory.Options> options,
  ILoggerFactory loggerFactory,
  IExpirationStorage expirationStorage) : AzureActiveDirectoryProviderBase(options.Value, loggerFactory, expirationStorage), IActiveDirectoryProvider
{
  private const string TokenUrl = "https://login.windows.net/{0}/oauth2/token?api-version=1.0";
  private const string ServiceUrl = "https://graph.windows.net";
  private const string ServiceVersion = "2013-11-08";
  private ADALDirectoryService _service;

  private ADALDirectoryService Service
  {
    get
    {
      if (this._service == null)
      {
        string path1 = this._path;
        string login = this._user.Login;
        string password = this._user.Password;
        string path2 = path1;
        if (!string.IsNullOrEmpty(path2) && !path2.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
          path2 = new Uri(new Uri("https://graph.windows.net"), path1).ToString();
        if (string.IsNullOrEmpty(path2))
          path2 = "https://graph.windows.net";
        AuthenticationContext context = new AuthenticationContext($"https://login.windows.net/{path1}/oauth2/token?api-version=1.0");
        string str = password;
        ClientCredential creds = new ClientCredential(login, str);
        this._service = new ADALDirectoryService("https://graph.windows.net", "2013-11-08", path2, context, creds);
      }
      return this._service;
    }
  }

  public string[] GetUserGroupIDs(string username, bool useCached = true)
  {
    if (!ActiveDirectoryProvider.IsSID(username))
    {
      User userByLogin = this.GetUserByLogin(username, true);
      if (userByLogin != null)
        username = $"{ActiveDirectoryProvider.ExtractLogin(username, this.DefaultDomainComponent).Domain}\\{userByLogin.SID}";
    }
    return this.GetGroupsBySID(username, useCached);
  }

  public string[] GetUserGroupIDsByLogin(string name, bool useCached = true)
  {
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal_((string) null, name));
    return this.GetCachedGroupsForUser(name, del, useCached).ToArray<string>();
  }

  public string[] GetUserGroupIDsBySID(string sid, bool useCached = true)
  {
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal_(sid, (string) null));
    return this.GetCachedGroupsForUser(sid, del, useCached).ToArray<string>();
  }

  private string[] GetUserGroupIDsInternal_(string sid, string name)
  {
    return this.FilterGroupsByRootGroup(this.GetUserGroupIdentifiers(sid, name)).ToArray<string>();
  }

  private IEnumerable<string> GetUserGroupIdentifiers(string sid, string name)
  {
    if (!ActiveDirectoryProvider.IsGUID(sid))
      return Enumerable.Empty<string>();
    List<string> groupIdentifiers = new List<string>();
    DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User> source = Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.User>((IQueryable) this.Service.DirectoryObjects) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>;
    if (sid != null)
      source = ((IQueryable<Microsoft.WindowsAzure.ActiveDirectory.User>) source).Where<Microsoft.WindowsAzure.ActiveDirectory.User>((Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.User, bool>>) (it => it.objectId == sid)) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>;
    if (name != null)
      source = ((IQueryable<Microsoft.WindowsAzure.ActiveDirectory.User>) source).Where<Microsoft.WindowsAzure.ActiveDirectory.User>((Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.User, bool>>) (it => it.userPrincipalName.StartsWith(name + "@"))) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>;
    try
    {
      foreach (DirectoryObject directoryObject in (IEnumerable<DirectoryObject>) this.Service.TakeAll<DirectoryObject>(this.Service.Execute<DirectoryObject>(new Uri(((DataServiceRequest) source).RequestUri?.ToString() + "/memberOf")) as QueryOperationResponse<DirectoryObject>))
        groupIdentifiers.Add(directoryObject.objectId);
    }
    catch (DataServiceQueryException ex) when (((Exception) ex).InnerException is DataServiceClientException innerException && innerException.StatusCode == 404)
    {
      LoggerExtensions.LogWarning(this.Logger, (Exception) ex, "User with SID {SID} is not found in the AD", new object[1]
      {
        (object) sid
      });
    }
    catch (Exception ex)
    {
      LoggerExtensions.LogWarning(this.Logger, ex, "Failed to retrieve ad groups for sid {SID}", new object[1]
      {
        (object) sid
      });
      if (!this.Options.KeepErrorsInCache)
        throw;
    }
    return (IEnumerable<string>) groupIdentifiers;
  }

  protected override HashSet<string> GetUserGroupIdentifiersWithoutCaching(string userId)
  {
    return new HashSet<string>(this.GetUserGroupIdentifiers(userId, (string) null), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  private IEnumerable<User> GetUsersByGroupIDInternal(string grSid)
  {
    ADALActiveDirectoryProvider directoryProvider = this;
    IEnumerable<string> domains = directoryProvider.GetCachedDomains(new Func<string[]>(directoryProvider.GetDomains));
    Uri uri = new Uri(((DataServiceRequest) (Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.Group>((IQueryable) directoryProvider.Service.DirectoryObjects).Where<Microsoft.WindowsAzure.ActiveDirectory.Group>((Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.Group, bool>>) (it => it.objectId == grSid)) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>)).RequestUri?.ToString() + "/members");
    IQueryable<Microsoft.WindowsAzure.ActiveDirectory.User> queryable;
    try
    {
      queryable = Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.User>(directoryProvider.Service.TakeAll<DirectoryObject>(directoryProvider.Service.Execute<DirectoryObject>(uri) as QueryOperationResponse<DirectoryObject>));
    }
    catch (DataServiceQueryException ex) when (((Exception) ex).InnerException is DataServiceClientException innerException && innerException.StatusCode == 404)
    {
      LoggerExtensions.LogWarning(directoryProvider.Logger, (Exception) ex, "Group with SID {SID} is not found in the AD", new object[1]
      {
        (object) grSid
      });
      yield break;
    }
    catch (Exception ex)
    {
      LoggerExtensions.LogWarning(directoryProvider.Logger, ex, "Failed to retrieve ad users for group with sid {SID}", new object[1]
      {
        (object) grSid
      });
      if (directoryProvider.Options.KeepErrorsInCache)
        yield break;
      throw;
    }
    foreach (Microsoft.WindowsAzure.ActiveDirectory.User user in (IEnumerable<Microsoft.WindowsAzure.ActiveDirectory.User>) queryable)
      yield return new User(user.objectId, new Guid?(Guid.Parse(user.objectId)), new NameInfo(user.userPrincipalName, AzureActiveDirectoryProviderBase.GetLogins(user.userPrincipalName, domains)), user.displayName, user.givenName, user.surname, user.mail, (string) null, new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1));
  }

  protected IEnumerable<Group> GetGroupsByGroupIDInternal(string grSid, bool includeParentGroup = true)
  {
    string defDomain = this.GetDefaultDomain();
    DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group> request = Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.Group>((IQueryable) this.Service.DirectoryObjects).Where<Microsoft.WindowsAzure.ActiveDirectory.Group>((Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.Group, bool>>) (it => it.objectId == grSid)) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>;
    if (includeParentGroup)
    {
      foreach (Microsoft.WindowsAzure.ActiveDirectory.Group group in (IEnumerable<Microsoft.WindowsAzure.ActiveDirectory.Group>) this.Service.TakeAll<Microsoft.WindowsAzure.ActiveDirectory.Group>(request.Execute() as QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.Group>))
        yield return new Group(group.objectId, (string) null, defDomain, group.displayName, group.description ?? group.displayName);
    }
    foreach (Microsoft.WindowsAzure.ActiveDirectory.Group group in (IEnumerable<Microsoft.WindowsAzure.ActiveDirectory.Group>) Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.Group>(this.Service.TakeAll<DirectoryObject>(this.Service.Execute<DirectoryObject>(new Uri(((DataServiceRequest) request).RequestUri?.ToString() + "/members")) as QueryOperationResponse<DirectoryObject>)))
      yield return new Group(group.objectId, (string) null, defDomain, group.displayName, group.description ?? group.displayName);
  }

  public IEnumerable<User> GetUsersByGroupIDs(string[] groups, bool useCached = true)
  {
    return ((IEnumerable<string>) groups ?? Enumerable.Empty<string>()).SelectMany<string, User>((Func<string, IEnumerable<User>>) (grSid => this.GetCachedUsersForGroup(grSid, (Func<User[]>) (() => this.GetUsersByGroupIDInternal(grSid).ToArray<User>()), useCached))).GroupBy<User, string>((Func<User, string>) (u => u.SID)).Select<IGrouping<string, User>, User>((Func<IGrouping<string, User>, User>) (g => g.First<User>()));
  }

  public Group GetGroup(string sid)
  {
    foreach (Group group in this.GetGroups(true))
    {
      if (group.SID == sid)
        return group;
    }
    return (Group) null;
  }

  public User GetUserByLogin(string name, bool useCached = true)
  {
    return string.IsNullOrEmpty(name) ? (User) null : this.GetCachedUser(name, (Func<User>) (() => this.GetUserByLoginInternal(name)), useCached);
  }

  public User GetUserBySID(string sid, bool useCached = true)
  {
    return string.IsNullOrEmpty(sid) ? (User) null : this.GetCachedUser(sid, (Func<User>) (() => this.GetUserBySIDInternal(sid)), useCached);
  }

  public User GetUserBySIDInternal(string sid)
  {
    IEnumerable<string> cachedDomains = this.GetCachedDomains(new Func<string[]>(this.GetDomains));
    IQueryable<Microsoft.WindowsAzure.ActiveDirectory.User> source = Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.User>((IQueryable) this.Service.DirectoryObjects);
    Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.User, bool>> predicate = (Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.User, bool>>) (it => it.objectId == sid);
    using (IEnumerator<Microsoft.WindowsAzure.ActiveDirectory.User> enumerator = this.Service.TakeAll<Microsoft.WindowsAzure.ActiveDirectory.User>((source.Where<Microsoft.WindowsAzure.ActiveDirectory.User>(predicate) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>).Execute() as QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.User>).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        Microsoft.WindowsAzure.ActiveDirectory.User current = enumerator.Current;
        return new User(current.objectId, new Guid?(Guid.Parse(current.objectId)), new NameInfo(current.userPrincipalName, AzureActiveDirectoryProviderBase.GetLogins(current.userPrincipalName, cachedDomains)), current.displayName, current.givenName, current.surname, current.mail, (string) null, new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1));
      }
    }
    return (User) null;
  }

  protected override User GetUserWithoutCaching(string userKey)
  {
    return !ActiveDirectoryProvider.IsGUID(userKey) ? this.GetUserByLoginInternal(userKey) : this.GetUserBySIDInternal(userKey);
  }

  private string[] GetGroupsBySID(string sid, bool useCached)
  {
    Login login = ActiveDirectoryProvider.ExtractLogin(sid, this.DefaultDomainComponent);
    if (this.GetUser((object) login.Name) == null)
      return new string[0];
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal_(login.Name, (string) null));
    return this.GetCachedGroupsForUser(sid, del, useCached).ToArray<string>();
  }

  private IEnumerable<Group> GetGroupsInternal()
  {
    string defDomain = this.GetDefaultDomain();
    foreach (Microsoft.WindowsAzure.ActiveDirectory.Group group in (IEnumerable<Microsoft.WindowsAzure.ActiveDirectory.Group>) this.Service.TakeAll<Microsoft.WindowsAzure.ActiveDirectory.Group>((Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.Group>((IQueryable) this.Service.DirectoryObjects) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>).Execute() as QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.Group>))
      yield return new Group(group.objectId, (string) null, defDomain, group.displayName, group.description ?? group.displayName);
  }

  protected override IEnumerable<Group> GetGroupsWithoutCaching()
  {
    return this.RootGroup != null ? this.GetGroupsByGroupIDInternal(this.RootGroup) : this.GetGroupsInternal();
  }

  private IEnumerable<User> GetUsersInternal()
  {
    ADALActiveDirectoryProvider directoryProvider = this;
    IEnumerable<string> domains = directoryProvider.GetCachedDomains(new Func<string[]>(directoryProvider.GetDomains));
    DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User> dataServiceQuery = Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.User>((IQueryable) directoryProvider.Service.DirectoryObjects) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>;
    foreach (Microsoft.WindowsAzure.ActiveDirectory.User user in (IEnumerable<Microsoft.WindowsAzure.ActiveDirectory.User>) directoryProvider.Service.TakeAll<Microsoft.WindowsAzure.ActiveDirectory.User>(dataServiceQuery.Execute() as QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.User>))
      yield return new User(user.objectId, new Guid?(Guid.Parse(user.objectId)), new NameInfo(user.userPrincipalName, AzureActiveDirectoryProviderBase.GetLogins(user.userPrincipalName, domains)), user.displayName, user.givenName, user.surname, user.mail, (string) null, new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1));
  }

  public IEnumerable<User> GetUsers(bool useCached = true)
  {
    return this.GetCachedUsers(this.RootGroup != null ? (Func<User[]>) (() => this.GetUsersByGroupIDInternal(this.RootGroup).ToArray<User>()) : (Func<User[]>) (() => this.GetUsersInternal().ToArray<User>()), useCached);
  }

  public bool ValidateUser(string login, string password) => false;

  private User GetUserByLoginInternal(string providerUserKey)
  {
    IEnumerable<string> domains = this.GetCachedDomains(new Func<string[]>(this.GetDomains));
    (string accountName, HashSet<string> allowedDomains) = this.ExtractAccountNameAndDomains(providerUserKey, domains);
    DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User> source = Queryable.OfType<Microsoft.WindowsAzure.ActiveDirectory.User>((IQueryable) this.Service.DirectoryObjects) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>;
    DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User> dataServiceQuery;
    if (accountName.Contains("@"))
    {
      string externalAccountName = AzureActiveDirectoryProviderBase.ToExternalAccountName(accountName);
      dataServiceQuery = ((IQueryable<Microsoft.WindowsAzure.ActiveDirectory.User>) source).Where<Microsoft.WindowsAzure.ActiveDirectory.User>((Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.User, bool>>) (it => it.userPrincipalName.StartsWith(externalAccountName + "@") || it.userPrincipalName == accountName)) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>;
    }
    else
      dataServiceQuery = ((IQueryable<Microsoft.WindowsAzure.ActiveDirectory.User>) source).Where<Microsoft.WindowsAzure.ActiveDirectory.User>((Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.User, bool>>) (it => it.userPrincipalName.StartsWith(accountName + "@"))) as DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>;
    List<User> list = this.Service.TakeAll<Microsoft.WindowsAzure.ActiveDirectory.User>(dataServiceQuery.Execute() as QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.User>).Select<Microsoft.WindowsAzure.ActiveDirectory.User, User>((Expression<Func<Microsoft.WindowsAzure.ActiveDirectory.User, User>>) (adUser => new User(adUser.objectId, (Guid?) Guid.Parse(adUser.objectId), new NameInfo(adUser.userPrincipalName, AzureActiveDirectoryProviderBase.GetLogins(adUser.userPrincipalName, domains)), adUser.displayName, adUser.givenName, adUser.surname, adUser.mail, default (string), new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1)))).ToList<User>();
    return list.FirstOrDefault<User>((Func<User, bool>) (user => user.Name.DomainLogin.Equals(providerUserKey, StringComparison.OrdinalIgnoreCase))) ?? list.FirstOrDefault<User>((Func<User, bool>) (user => user.Name.Logins.Any<Login>((Func<Login, bool>) (l => allowedDomains.Contains(l.Domain)))));
  }

  private string[] GetDomains()
  {
    string str1 = (string) null;
    string str2 = (string) null;
    List<string> stringList1 = new List<string>();
    foreach (TenantDetail tenantDetail in (IEnumerable<TenantDetail>) this.Service.TakeAll<TenantDetail>((Queryable.OfType<TenantDetail>((IQueryable) this.Service.DirectoryObjects) as DataServiceQuery<TenantDetail>).Execute() as QueryOperationResponse<TenantDetail>))
    {
      foreach (VerifiedDomain verifiedDomain in tenantDetail.verifiedDomains)
      {
        bool? nullable = verifiedDomain.@default;
        bool flag = true;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          str1 = verifiedDomain.name;
        else if (verifiedDomain.type == "Federated")
          stringList1.Add(verifiedDomain.name);
        str2 = verifiedDomain.name;
      }
    }
    List<string> stringList2 = new List<string>();
    if (str1 != null)
      stringList2.Add(str1);
    if (stringList1.Any<string>())
      stringList2.AddRange((IEnumerable<string>) stringList1);
    if (stringList2.Count == 0)
      stringList2.Add(str2);
    return stringList2.ToArray();
  }

  private string GetDefaultDomain()
  {
    IEnumerable<string> cachedDomains = this.GetCachedDomains(new Func<string[]>(this.GetDomains));
    return cachedDomains.Count<string>() <= 0 ? (string) null : cachedDomains.First<string>();
  }
}
