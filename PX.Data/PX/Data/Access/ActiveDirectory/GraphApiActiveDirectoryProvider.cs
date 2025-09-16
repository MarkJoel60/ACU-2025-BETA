// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.GraphApiActiveDirectoryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal sealed class GraphApiActiveDirectoryProvider : 
  AzureActiveDirectoryProviderBase,
  IActiveDirectoryProvider
{
  private const string GroupFields = "displayName,id,description";
  private const string UserFields = "displayName,id,mail,userPrincipalName,givenName,surname";
  private const int PageSize = 100;
  private readonly ConcurrentDictionary<string, Lazy<Group>> _groupsCache = new ConcurrentDictionary<string, Lazy<Group>>();
  private readonly IGraphServiceClient _client;

  private string DefaultDomain
  {
    get => ((IEnumerable<string>) this.GetDomains()).FirstOrDefault<string>();
  }

  public GraphApiActiveDirectoryProvider(
    IGraphServiceClientFactory clientFactory,
    IOptions<PX.Data.Access.ActiveDirectory.Options> options,
    ILoggerFactory loggerFactory,
    IExpirationStorage expirationStorage,
    IGraphApiClientConfigurationProvider configurationProvider)
    : base(options.Value, loggerFactory, expirationStorage)
  {
    PX.Data.Access.ActiveDirectory.Options options1 = options.Value;
    GraphApiClientConfiguration configuration = configurationProvider.CreateConfiguration(options1);
    this._client = clientFactory.CreateClient(configuration);
  }

  public override void Reset()
  {
    this._groupsCache.Clear();
    base.Reset();
  }

  public string[] GetUserGroupIDsByLogin(string name, bool useCached = true)
  {
    if (string.IsNullOrEmpty(name))
      return Array.Empty<string>();
    User userByLogin = this.GetUserByLogin(name, true);
    return userByLogin != null ? this.GetUserGroupIDsBySID(userByLogin.SID, useCached) : Array.Empty<string>();
  }

  public string[] GetUserGroupIDsBySID(string sid, bool useCached = true)
  {
    return !string.IsNullOrEmpty(sid) ? this.GetCachedGroupsForUser(sid, (Func<string[]>) (() => this.GetUserGroups(sid).ToArray<string>()), useCached).ToArray<string>() : Array.Empty<string>();
  }

  public string[] GetUserGroupIDs(string username, bool useCached = true)
  {
    if (string.IsNullOrEmpty(username))
      return Array.Empty<string>();
    User user = this.GetUser((object) username);
    return user != null ? this.GetUserGroupIDsBySID(user.SID, useCached) : Array.Empty<string>();
  }

  public User GetUserByLogin(string name, bool useCached = true)
  {
    return !string.IsNullOrEmpty(name) ? this.GetCachedUser(name, (Func<User>) (() => this.FindUserByProviderUserKey(name)), useCached) : (User) null;
  }

  public User GetUserBySID(string sid, bool useCached = true)
  {
    return !string.IsNullOrEmpty(sid) ? this.GetCachedUser(sid, (Func<User>) (() => this.GetUserByIdOrPrincipalName(sid)), useCached) : (User) null;
  }

  public Group GetGroup(string sid)
  {
    return !string.IsNullOrEmpty(sid) ? this.GetCacheable<Group>(this._groupsCache, sid, (Func<Group>) (() => this.GetGroupById(sid)), true) : (Group) null;
  }

  public IEnumerable<User> GetUsersByGroupIDs(string[] groups, bool useCached = true)
  {
    return groups == null || groups.Length == 0 ? (IEnumerable<User>) Array.Empty<User>() : ((IEnumerable<string>) groups).SelectMany<string, User>((Func<string, IEnumerable<User>>) (groupId => this.GetCachedUsersForGroup(groupId, (Func<User[]>) (() => this.GetGroupUsers(groupId).ToArray<User>()), useCached))).GroupBy<User, string>((Func<User, string>) (user => user.SID)).Select<IGrouping<string, User>, User>((Func<IGrouping<string, User>, User>) (x => x.First<User>()));
  }

  public bool ValidateUser(string login, string password) => false;

  public IEnumerable<User> GetUsers(bool useCached = true)
  {
    return string.IsNullOrEmpty(this.RootGroup) ? this.GetCachedUsers((Func<User[]>) (() => this.GetAllUsers().ToArray<User>()), useCached) : this.GetCachedUsers((Func<User[]>) (() => this.GetGroupUsers(this.RootGroup).ToArray<User>()), useCached);
  }

  private static bool IsId(string userKey) => ActiveDirectoryProvider.IsGUID(userKey);

  protected override IEnumerable<Group> GetGroupsWithoutCaching()
  {
    return string.IsNullOrEmpty(this.RootGroup) ? this.GetAllGroups() : this.GetSubgroupsWithSelf(this.RootGroup);
  }

  protected override User GetUserWithoutCaching(string userKey)
  {
    return !GraphApiActiveDirectoryProvider.IsId(userKey) ? this.FindUserByProviderUserKey(userKey) : this.GetUserByIdOrPrincipalName(userKey);
  }

  protected override HashSet<string> GetUserGroupIdentifiersWithoutCaching(
    string userIdOrPrincipalName)
  {
    return this.GetAllPages<IUserMemberOfCollectionWithReferencesPage, DirectoryObject, string>((Func<Task<IUserMemberOfCollectionWithReferencesPage>>) (() =>
    {
      IUserMemberOfCollectionWithReferencesRequest referencesRequest = HeaderHelper.Header<IUserMemberOfCollectionWithReferencesRequest>(new UserMemberOfCollectionWithReferencesRequestBuilder(((IBaseRequestBuilder) this._client.Users[GraphApiActiveDirectoryProvider.Escape(userIdOrPrincipalName)].MemberOf).AppendSegmentToRequestUrl("microsoft.graph.group"), (IBaseClient) this._client).Request(), "ConsistencyLevel", "eventual").Top(100).Select("id");
      ((IBaseRequest) referencesRequest).QueryOptions.Add(new QueryOption("$count", "true"));
      return referencesRequest.GetAsync();
    }), (Func<IUserMemberOfCollectionWithReferencesPage, (bool, Func<Task<IUserMemberOfCollectionWithReferencesPage>>)>) (x => (x.NextPageRequest != null, (Func<Task<IUserMemberOfCollectionWithReferencesPage>>) (() => x.NextPageRequest.GetAsync()))), (Func<DirectoryObject, string>) (x => ((Entity) x).Id), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get AD groups for user with key '{Key}'", new object[1]
    {
      (object) userIdOrPrincipalName
    }))).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  private IEnumerable<string> GetUserGroups(string userIdOrPrincipalName)
  {
    return this.FilterGroupsByRootGroup((IEnumerable<string>) this.GetUserGroupIdentifiersWithoutCaching(userIdOrPrincipalName));
  }

  private User GetUserByIdOrPrincipalName(string userIdOrPrincipalName)
  {
    return GraphApiActiveDirectoryProvider.Map.User(GraphApiActiveDirectoryProvider.AsyncHelper.GetResult<User>((Func<Task<User>>) (() => this._client.Users[GraphApiActiveDirectoryProvider.Escape(userIdOrPrincipalName)].Request().Select("displayName,id,mail,userPrincipalName,givenName,surname").GetAsync()), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get AD user by key '{Key}'", new object[1]
    {
      (object) userIdOrPrincipalName
    })), !this.Options.KeepErrorsInCache), (IEnumerable<string>) this.GetDomains());
  }

  private User FindUserByProviderUserKey(string providerUserKey)
  {
    string[] domains = this.GetDomains();
    (string str1, HashSet<string> allowedDomains) = this.ExtractAccountNameAndDomains(providerUserKey, (IEnumerable<string>) domains);
    IGraphServiceUsersCollectionRequest request = this._client.Users.Request();
    IGraphServiceUsersCollectionRequest collectionRequest = request;
    string str2;
    if (!str1.Contains("@"))
      str2 = $"startswith(userPrincipalName,'{GraphApiActiveDirectoryProvider.Escape(str1)}@')";
    else
      str2 = $"userPrincipalName eq '{GraphApiActiveDirectoryProvider.Escape(str1)}' OR startsWith(userPrincipalName,'{GraphApiActiveDirectoryProvider.Escape(AzureActiveDirectoryProviderBase.ToExternalAccountName(str1))}@')";
    request = collectionRequest.Filter(str2);
    request = HeaderHelper.Header<IGraphServiceUsersCollectionRequest>(request, "ConsistencyLevel", "eventual").Select("displayName,id,mail,userPrincipalName,givenName,surname");
    ((IBaseRequest) request).QueryOptions.Add(new QueryOption("$count", "true"));
    List<User> list = this.GetAllPages<IGraphServiceUsersCollectionPage, User, User>((Func<Task<IGraphServiceUsersCollectionPage>>) (() => request.GetAsync()), (Func<IGraphServiceUsersCollectionPage, (bool, Func<Task<IGraphServiceUsersCollectionPage>>)>) (x => (x.NextPageRequest != null, (Func<Task<IGraphServiceUsersCollectionPage>>) (() => x.NextPageRequest.GetAsync()))), (Func<User, User>) (x => GraphApiActiveDirectoryProvider.Map.User(x, (IEnumerable<string>) domains)), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to find AD user by key '{Key}'", new object[1]
    {
      (object) providerUserKey
    }))).ToList<User>();
    return list.FirstOrDefault<User>((Func<User, bool>) (user => user.Name.DomainLogin.Equals(providerUserKey, StringComparison.OrdinalIgnoreCase))) ?? list.FirstOrDefault<User>((Func<User, bool>) (user => user.Name.Name.Equals(providerUserKey, StringComparison.OrdinalIgnoreCase))) ?? list.FirstOrDefault<User>((Func<User, bool>) (user => user.Name.Logins.Any<Login>((Func<Login, bool>) (l => allowedDomains.Contains(l.Domain)))));
  }

  private static string Escape(string id) => id.Replace("#", "%23").Replace("'", "''");

  private IEnumerable<Group> GetAllGroups()
  {
    string defaultDomain = ((IEnumerable<string>) this.GetDomains()).FirstOrDefault<string>();
    return this.GetAllPages<IGraphServiceGroupsCollectionPage, Group, Group>((Func<Task<IGraphServiceGroupsCollectionPage>>) (() => this._client.Groups.Request().Top(100).Select("displayName,id,description").GetAsync()), (Func<IGraphServiceGroupsCollectionPage, (bool, Func<Task<IGraphServiceGroupsCollectionPage>>)>) (x => (x.NextPageRequest != null, (Func<Task<IGraphServiceGroupsCollectionPage>>) (() => x.NextPageRequest.GetAsync()))), (Func<Group, Group>) (x => GraphApiActiveDirectoryProvider.Map.Group(x, defaultDomain)), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get all AD groups", Array.Empty<object>())));
  }

  private Group GetGroupById(string groupId)
  {
    return GraphApiActiveDirectoryProvider.Map.Group(GraphApiActiveDirectoryProvider.AsyncHelper.GetResult<Group>((Func<Task<Group>>) (() => this._client.Groups[GraphApiActiveDirectoryProvider.Escape(groupId)].Request().Select("displayName,id,description").GetAsync()), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get AD group by key '{Key}'", new object[1]
    {
      (object) groupId
    })), !this.Options.KeepErrorsInCache), this.DefaultDomain);
  }

  private IEnumerable<Group> GetSubgroupsWithSelf(string groupId)
  {
    string defaultDomain = this.DefaultDomain;
    yield return this.GetGroupById(groupId);
    foreach (Group allPage in this.GetAllPages<IGroupTransitiveMembersCollectionWithReferencesPage, DirectoryObject, Group>((Func<Task<IGroupTransitiveMembersCollectionWithReferencesPage>>) (() =>
    {
      IGroupTransitiveMembersCollectionWithReferencesRequest referencesRequest = HeaderHelper.Header<IGroupTransitiveMembersCollectionWithReferencesRequest>(new GroupTransitiveMembersCollectionWithReferencesRequestBuilder(((IBaseRequestBuilder) this._client.Groups[GraphApiActiveDirectoryProvider.Escape(groupId)].TransitiveMembers).AppendSegmentToRequestUrl("microsoft.graph.group"), (IBaseClient) this._client).Request(), "ConsistencyLevel", "eventual").Top(100).Select("displayName,id,description");
      ((IBaseRequest) referencesRequest).QueryOptions.Add(new QueryOption("$count", "true"));
      return referencesRequest.GetAsync();
    }), (Func<IGroupTransitiveMembersCollectionWithReferencesPage, (bool, Func<Task<IGroupTransitiveMembersCollectionWithReferencesPage>>)>) (x => (x.NextPageRequest != null, (Func<Task<IGroupTransitiveMembersCollectionWithReferencesPage>>) (() => x.NextPageRequest.GetAsync()))), (Func<DirectoryObject, Group>) (x => GraphApiActiveDirectoryProvider.Map.Group(x, defaultDomain)), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get AD subgroups for group with key '{Key}'", new object[1]
    {
      (object) groupId
    }))))
      yield return allPage;
  }

  private IEnumerable<User> GetAllUsers()
  {
    string[] domains = this.GetDomains();
    return this.GetAllPages<IGraphServiceUsersCollectionPage, User, User>((Func<Task<IGraphServiceUsersCollectionPage>>) (() => this._client.Users.Request().Top(100).Select("displayName,id,mail,userPrincipalName,givenName,surname").GetAsync()), (Func<IGraphServiceUsersCollectionPage, (bool, Func<Task<IGraphServiceUsersCollectionPage>>)>) (x => (x.NextPageRequest != null, (Func<Task<IGraphServiceUsersCollectionPage>>) (() => x.NextPageRequest.GetAsync()))), (Func<User, User>) (x => GraphApiActiveDirectoryProvider.Map.User(x, (IEnumerable<string>) domains)), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get all AD users", Array.Empty<object>())));
  }

  private IEnumerable<User> GetGroupUsers(string groupId)
  {
    string[] domains = this.GetDomains();
    return this.GetAllPages<IGroupTransitiveMembersCollectionWithReferencesPage, DirectoryObject, User>((Func<Task<IGroupTransitiveMembersCollectionWithReferencesPage>>) (() =>
    {
      IGroupTransitiveMembersCollectionWithReferencesRequest referencesRequest = HeaderHelper.Header<IGroupTransitiveMembersCollectionWithReferencesRequest>(new GroupTransitiveMembersCollectionWithReferencesRequestBuilder(((IBaseRequestBuilder) this._client.Groups[GraphApiActiveDirectoryProvider.Escape(groupId)].TransitiveMembers).AppendSegmentToRequestUrl("microsoft.graph.user"), (IBaseClient) this._client).Request(), "ConsistencyLevel", "eventual").Top(100).Select("displayName,id,mail,userPrincipalName,givenName,surname");
      ((IBaseRequest) referencesRequest).QueryOptions.Add(new QueryOption("$count", "true"));
      return referencesRequest.GetAsync();
    }), (Func<IGroupTransitiveMembersCollectionWithReferencesPage, (bool, Func<Task<IGroupTransitiveMembersCollectionWithReferencesPage>>)>) (x => (x.NextPageRequest != null, (Func<Task<IGroupTransitiveMembersCollectionWithReferencesPage>>) (() => x.NextPageRequest.GetAsync()))), (Func<DirectoryObject, User>) (x => GraphApiActiveDirectoryProvider.Map.User(x, (IEnumerable<string>) domains)), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get AD users for group with key '{Key}'", new object[1]
    {
      (object) groupId
    })));
  }

  private IEnumerable<TResultItem> GetAllPages<TCollectionResult, TGraphItem, TResultItem>(
    Func<Task<TCollectionResult>> firstPageTask,
    Func<TCollectionResult, (bool hasNextPage, Func<Task<TCollectionResult>> nextPageTask)> nextPage,
    Func<TGraphItem, TResultItem> map,
    System.Action<Exception> onError)
    where TCollectionResult : IEnumerable<TGraphItem>
  {
    GraphApiActiveDirectoryProvider directoryProvider = this;
    Func<Task<TCollectionResult>> func1 = firstPageTask;
    while (true)
    {
      TCollectionResult result = GraphApiActiveDirectoryProvider.AsyncHelper.GetResult<TCollectionResult>(func1, onError, !directoryProvider.Options.KeepErrorsInCache);
      if ((object) result != null)
      {
        foreach (TGraphItem graphItem in result)
        {
          TResultItem allPage = map(graphItem);
          if ((object) allPage != null)
            yield return allPage;
        }
        (bool flag, Func<Task<TCollectionResult>> func2) = nextPage(result);
        if (flag)
        {
          func1 = func2;
          result = default (TCollectionResult);
        }
        else
          goto label_3;
      }
      else
        break;
    }
    yield break;
label_3:;
  }

  private string[] GetDomains()
  {
    return this.GetCachedDomains((Func<string[]>) (() =>
    {
      IGraphServiceDomainsCollectionPage result = GraphApiActiveDirectoryProvider.AsyncHelper.GetResult<IGraphServiceDomainsCollectionPage>((Func<Task<IGraphServiceDomainsCollectionPage>>) (() => this._client.Domains.Request().GetAsync()), (System.Action<Exception>) (exception => LoggerExtensions.LogWarning(this.Logger, exception, "Failed to get list of AD domains", Array.Empty<object>())), !this.Options.KeepErrorsInCache);
      List<Domain> list = ((result != null ? ((IEnumerable<Domain>) result).Where<Domain>((Func<Domain, bool>) (x =>
      {
        bool? isVerified = x.IsVerified;
        bool flag = true;
        return isVerified.GetValueOrDefault() == flag & isVerified.HasValue;
      })) : (IEnumerable<Domain>) null) ?? Enumerable.Empty<Domain>()).ToList<Domain>();
      HashSet<string> hashSet = list.Where<Domain>((Func<Domain, bool>) (x =>
      {
        bool? isDefault = x.IsDefault;
        bool flag = true;
        return isDefault.GetValueOrDefault() == flag & isDefault.HasValue || x.AuthenticationType == "Federated";
      })).OrderBy<Domain, int>((Func<Domain, int>) (x =>
      {
        bool? isDefault = x.IsDefault;
        bool flag = true;
        return !(isDefault.GetValueOrDefault() == flag & isDefault.HasValue) ? 1 : 0;
      })).Select<Domain, string>((Func<Domain, string>) (x => ((Entity) x).Id)).ToHashSet<string>();
      return !hashSet.Any<string>() ? EnumerableExtensions.AsSingleEnumerable<string>(list.Select<Domain, string>((Func<Domain, string>) (x => ((Entity) x).Id)).LastOrDefault<string>()).ToArray<string>() : hashSet.ToArray<string>();
    })).ToArray<string>();
  }

  private static class Map
  {
    public static User User(User graphUser, IEnumerable<string> domains)
    {
      Guid result;
      return graphUser != null ? new User(((Entity) graphUser).Id, Guid.TryParse(((Entity) graphUser).Id, out result) ? result.AsNullable<Guid>() : new Guid?(), new NameInfo(graphUser.UserPrincipalName, AzureActiveDirectoryProviderBase.GetLogins(graphUser.UserPrincipalName, domains)), graphUser.DisplayName, graphUser.GivenName, graphUser.Surname, graphUser.Mail, (string) null, new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1)) : (User) null;
    }

    public static User User(DirectoryObject directoryObject, IEnumerable<string> domains)
    {
      if (directoryObject == null)
        return (User) null;
      if (directoryObject is User graphUser)
        return GraphApiActiveDirectoryProvider.Map.User(graphUser, domains);
      string str = GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "userPrincipalName");
      Guid result;
      return new User(((Entity) directoryObject).Id, Guid.TryParse(((Entity) directoryObject).Id, out result) ? result.AsNullable<Guid>() : new Guid?(), new NameInfo(str, AzureActiveDirectoryProviderBase.GetLogins(str, domains)), GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "displayName"), GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "givenName"), GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "surname"), GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "mail"), (string) null, new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1), new System.DateTime(2000, 1, 1));
    }

    public static Group Group(Group graphGroup, string defaultDomain)
    {
      return graphGroup != null ? new Group(((Entity) graphGroup).Id, (string) null, defaultDomain, graphGroup.DisplayName, graphGroup.Description ?? graphGroup.DisplayName) : (Group) null;
    }

    public static Group Group(DirectoryObject directoryObject, string defaultDomain)
    {
      if (directoryObject == null)
        return (Group) null;
      return !(directoryObject is Group graphGroup) ? new Group(((Entity) directoryObject).Id, (string) null, defaultDomain, GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "displayName"), GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "description") ?? GraphApiActiveDirectoryProvider.Map.GetString((Entity) directoryObject, "displayName")) : GraphApiActiveDirectoryProvider.Map.Group(graphGroup, defaultDomain);
    }

    private static string GetString(Entity directoryObject, string key)
    {
      object obj;
      if (!directoryObject.AdditionalData.TryGetValue(key, out obj))
        return (string) null;
      string str = obj?.ToString();
      return !string.IsNullOrEmpty(str) ? str : (string) null;
    }
  }

  private static class AsyncHelper
  {
    private static readonly TaskFactory CustomTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

    public static TResult GetResult<TResult>(
      Func<Task<TResult>> func,
      System.Action<Exception> onError,
      bool rethrow)
    {
      try
      {
        return GraphApiActiveDirectoryProvider.AsyncHelper.CustomTaskFactory.StartNew<Task<TResult>>(func).Unwrap<TResult>().GetAwaiter().GetResult();
      }
      catch (ServiceException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
      {
        if (onError != null)
          onError((Exception) ex);
      }
      catch (Exception ex) when (onError != null)
      {
        onError(ex);
        if (rethrow)
          throw;
      }
      return default (TResult);
    }
  }
}
