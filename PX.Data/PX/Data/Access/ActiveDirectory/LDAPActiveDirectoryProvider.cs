// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.LDAPActiveDirectoryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Principal;
using System.Text;
using System.Web.Hosting;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal sealed class LDAPActiveDirectoryProvider : ActiveDirectoryProvider, IActiveDirectoryProvider
{
  private readonly PX.Data.Access.ActiveDirectory.Options _options;

  public LDAPActiveDirectoryProvider(
    IOptions<PX.Data.Access.ActiveDirectory.Options> options,
    ILoggerFactory loggerFactory,
    IExpirationStorage expirationStorage)
    : base("LDAP://" + options.Value.Path, options.Value.GetCredential(), options.Value, loggerFactory, expirationStorage)
  {
    this._options = options.Value;
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
    return this.GetUserGroupIDsByKey("samaccountname", name, useCached);
  }

  public string[] GetUserGroupIDsBySID(string sid, bool useCached = true)
  {
    return this.GetUserGroupIDsByKey("objectsid", sid, useCached);
  }

  private string[] GetUserGroupIDsByKey(string key, string value, bool useCached = true)
  {
    Login user = ActiveDirectoryProvider.ExtractLogin(value, this.DefaultDomainComponent);
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal($"(&(objectCategory=user)({key}={user.Name}))"));
    return this.GetCachedGroupsForUser(value, del, useCached).ToArray<string>();
  }

  private string[] GetUserGroupIDsInternal(
    string query,
    Predicate<IDictionary<string, IList>> predicate = null)
  {
    List<string> stringList = new List<string>();
    SearchResult searchResult = this.SearchOnceInDirectory(query, "adspath");
    if (searchResult == null)
      return stringList.ToArray();
    DirectoryEntry directoryEntry = searchResult.GetDirectoryEntry();
    directoryEntry.RefreshCache(new string[1]
    {
      "tokenGroups"
    });
    foreach (byte[] binaryForm in (CollectionBase) directoryEntry.Properties["tokenGroups"])
    {
      SecurityIdentifier securityIdentifier = new SecurityIdentifier(binaryForm, 0);
      if (securityIdentifier.AccountDomainSid != (SecurityIdentifier) null)
        stringList.Add(securityIdentifier.ToString());
    }
    return stringList.ToArray();
  }

  private IEnumerable<User> GetUsersByGroupIDInternal(string grSid)
  {
    LDAPActiveDirectoryProvider directoryProvider1 = this;
    SearchResult searchResult = directoryProvider1.SearchOnceInDirectory($"(&(objectCategory=group)({"objectsid"}={grSid}))", "distinguishedname");
    if (searchResult != null)
    {
      LDAPActiveDirectoryProvider directoryProvider2 = directoryProvider1;
      string query = $"(&(objectCategory=user)(|(memberOf:1.2.840.113556.1.4.1941:={searchResult.Properties["distinguishedname"][0]})(primaryGroupID={Group.GetRID(grSid)})))";
      string[] strArray = new string[13]
      {
        "objectsid",
        "primarygroupid",
        "memberof",
        "samaccountname",
        "distinguishedname",
        "displayname",
        "givenname",
        "sn",
        "mail",
        "description",
        "lastlogon",
        "pwdlastset",
        "objectguid"
      };
      foreach (IDictionary<string, IList> dictionary in directoryProvider2.SearchInDirectory(query, strArray))
      {
        string objectSid = LDAPActiveDirectoryProvider.ExtractObjectSID(dictionary, "objectsid");
        if (!string.IsNullOrEmpty(objectSid))
        {
          string str = LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "samaccountname"));
          List<Login> logins = LDAPActiveDirectoryProvider.GetLogins(str, LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "distinguishedname"));
          yield return new User(objectSid, LDAPActiveDirectoryProvider.ExtractObjectGUID(dictionary, "objectguid"), new NameInfo(str, (IEnumerable<Login>) logins), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "displayname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "givenname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "sn")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "mail")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "description")), new System.DateTime(2000, 1, 1), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "lastlogon")), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "pwdlastset")));
        }
      }
    }
  }

  public IEnumerable<User> GetUsersByGroupIDs(string[] groups, bool useCached = true)
  {
    return ((IEnumerable<string>) groups ?? Enumerable.Empty<string>()).SelectMany<string, User>((Func<string, IEnumerable<User>>) (grSid => this.GetCachedUsersForGroup(grSid, (Func<User[]>) (() => this.GetUsersByGroupIDInternal(grSid).ToArray<User>()), useCached))).GroupBy<User, string>((Func<User, string>) (u => u.SID)).Select<IGrouping<string, User>, User>((Func<IGrouping<string, User>, User>) (g => g.First<User>()));
  }

  public Group GetGroup(string sid)
  {
    SearchResult searchResult = this.SearchOnceInDirectory($"(&(objectCategory=group)({"objectsid"}={sid}))", "objectsid", "dc", "cn", "displayName", "description");
    if (searchResult == null)
      return (Group) null;
    Dictionary<string, IList> groupDescription = new Dictionary<string, IList>();
    foreach (string propertyName in (IEnumerable) searchResult.Properties.PropertyNames)
      groupDescription.Add(propertyName, (IList) new ArrayList((ICollection) searchResult.Properties[propertyName]));
    return this.createGroup((IDictionary<string, IList>) groupDescription);
  }

  public User GetUser(object providerUserKey)
  {
    if (providerUserKey == null)
      return (User) null;
    string userKey = providerUserKey.ToString();
    return this.GetCachedUser(userKey, (Func<User>) (() => !ActiveDirectoryProvider.IsSID(userKey) ? this.GetUserByLoginInternal(userKey) : this.GetUserBySIDInternal(userKey)), true);
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
    using (IEnumerator<IDictionary<string, IList>> enumerator = this.SearchInDirectory($"(&(objectCategory=user)(objectsid={sid}))", "distinguishedname", "samaccountname", "displayname", "givenname", "sn", "mail", "description", "lastlogon", "pwdlastset", "objectguid").GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        IDictionary<string, IList> current = enumerator.Current;
        string str = LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "samaccountname"));
        return new User(sid, LDAPActiveDirectoryProvider.ExtractObjectGUID(current, "objectguid"), new NameInfo(str, (IEnumerable<Login>) LDAPActiveDirectoryProvider.GetLogins(str, LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "distinguishedname"))), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "displayname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "givenname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "sn")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "mail")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "description")), new System.DateTime(2000, 1, 1), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "lastlogon")), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(current, "pwdlastset")));
      }
    }
    return (User) null;
  }

  private string[] GetGroupsBySID(string sid, bool useCached)
  {
    return this.GetGroupsByKey("objectsid", sid, useCached);
  }

  private string[] GetGroupsByKey(string key, string value, bool useCached)
  {
    Login user = ActiveDirectoryProvider.ExtractLogin(value, this.DefaultDomainComponent);
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal($"(&(objectCategory=user)({key}={user.Name}))", (Predicate<IDictionary<string, IList>>) (props => LDAPActiveDirectoryProvider.GetLogins(user.Name, LDAPActiveDirectoryProvider.GetValueOfAttribute(props, "distinguishedname")).Find((Predicate<Login>) (item => string.Compare(item.Domain, user.Domain, StringComparison.OrdinalIgnoreCase) == 0)) != null)));
    return this.GetCachedGroupsForUser(value, del, useCached).ToArray<string>();
  }

  private IEnumerable<Group> GetGroupsInternal()
  {
    LDAPActiveDirectoryProvider directoryProvider1 = this;
    LDAPActiveDirectoryProvider directoryProvider2 = directoryProvider1;
    string[] strArray = new string[5]
    {
      "objectsid",
      "dc",
      "cn",
      "displayName",
      "description"
    };
    foreach (IDictionary<string, IList> groupDescription in directoryProvider2.SearchInDirectory("(objectCategory=group)", strArray))
    {
      Group group = directoryProvider1.createGroup(groupDescription);
      if (group != null)
        yield return group;
    }
  }

  private Group createGroup(IDictionary<string, IList> groupDescription)
  {
    string objectSid = LDAPActiveDirectoryProvider.ExtractObjectSID(groupDescription, "objectsid");
    if (string.IsNullOrEmpty(objectSid))
      return (Group) null;
    string cn = LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(groupDescription, "cn"));
    string defaultDomainComponent = LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(groupDescription, "dc"));
    if (string.IsNullOrEmpty(defaultDomainComponent))
      defaultDomainComponent = this.DefaultDomainComponent;
    string name = LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(groupDescription, "displayName"));
    if (string.IsNullOrEmpty(name))
      name = cn;
    string description = LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(groupDescription, "description"));
    return new Group(objectSid, cn, defaultDomainComponent, name, description);
  }

  public IEnumerable<Group> GetGroups(bool useCached = true)
  {
    return this.GetCachedGroups((Func<Group[]>) (() => this.GetGroupsInternal().ToArray<Group>()), useCached);
  }

  public IEnumerable<User> GetUsers(bool useCached = true)
  {
    return this.GetCachedUsers((Func<User[]>) (() =>
    {
      List<User> userList = new List<User>();
      foreach (IDictionary<string, IList> dictionary in this.SearchInDirectory("(objectCategory=user)", "objectsid", "distinguishedname", "samaccountname", "displayname", "givenname", "sn", "mail", "description", "lastlogon", "pwdlastset", "objectguid"))
      {
        string objectSid = LDAPActiveDirectoryProvider.ExtractObjectSID(dictionary, "objectsid");
        if (!string.IsNullOrEmpty(objectSid))
        {
          string str = LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "samaccountname"));
          userList.Add(new User(objectSid, LDAPActiveDirectoryProvider.ExtractObjectGUID(dictionary, "objectguid"), new NameInfo(str, (IEnumerable<Login>) LDAPActiveDirectoryProvider.GetLogins(str, LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "distinguishedname"))), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "displayname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "givenname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "sn")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "mail")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "description")), new System.DateTime(2000, 1, 1), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "lastlogon")), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "pwdlastset"))));
        }
      }
      return userList.ToArray();
    }), useCached);
  }

  private bool ValidateUserCached(string login, string password, Func<string, string, bool> del)
  {
    string orSetCachedHash = PasswordHashing.GetOrSetCachedHash(login, password, false, (Func<string>) (() => PasswordHashing.HashPasswordWithSalt(password, login, false)));
    string key = $"{typeof (LDAPActiveDirectoryProvider).FullName}_{login}_{orSetCachedHash}";
    Lazy<bool> lazy1 = new Lazy<bool>((Func<bool>) (() => del(login, password)));
    if (!(MemoryCache.Default.AddOrGetExisting(key, (object) lazy1, DateTimeOffset.UtcNow.AddMinutes(2.0), (string) null) is Lazy<bool> lazy2))
      lazy2 = lazy1;
    return lazy2.Value;
  }

  public bool ValidateUser(string login, string password)
  {
    return password != null && this.ValidateUserCached(login.Trim(), password, (Func<string, string, bool>) ((l, p) =>
    {
      (string, string)? nullable = this.SplitPath();
      string fullLogin = this.GetFullLogin(l);
      return nullable.HasValue ? this.TryToAuthWithPrincipalContext(fullLogin, p) : this.TryToAuthWithCreds(fullLogin, p);
    }));
  }

  private string[] GetDomainFromPrincipal()
  {
    Login login = ActiveDirectoryProvider.ExtractLogin(this._user.Login, this.DefaultDomainComponent);
    HashSet<string> allowedDomains = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      this.DefaultDomainComponent,
      login.Domain
    };
    foreach (IDictionary<string, IList> dictionary in this.SearchInDirectory($"(&(objectCategory=user)(samaccountname={login.Name}))", "objectsid", "distinguishedname", "userprincipalname"))
    {
      if (!string.IsNullOrEmpty(LDAPActiveDirectoryProvider.ExtractObjectSID(dictionary, "objectsid")) && LDAPActiveDirectoryProvider.GetLogins(login.Name, LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "distinguishedname")).Find((Predicate<Login>) (user => allowedDomains.Contains(user.Domain))) != null)
      {
        string domain = ActiveDirectoryProvider.ExtractLogin(LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "userprincipalname")), "").Domain;
        string[] domainFromPrincipal;
        if (!string.IsNullOrEmpty(domain))
          domainFromPrincipal = new string[1]{ domain };
        else
          domainFromPrincipal = Array.Empty<string>();
        return domainFromPrincipal;
      }
    }
    return Array.Empty<string>();
  }

  private string GetFullLogin(string login)
  {
    IEnumerable<string> cachedDomains = this.GetCachedDomains(new Func<string[]>(this.GetDomainFromPrincipal));
    if (cachedDomains == null || cachedDomains.Count<string>() == 0)
      return login;
    string str = cachedDomains.Single<string>();
    Login login1 = ActiveDirectoryProvider.ExtractLogin(login, "");
    return !string.IsNullOrEmpty(login1.Domain) ? login : $"{login1.Name}@{str}";
  }

  private (string name, string container)? SplitPath()
  {
    string[] source = this._options.Path.Split('/');
    return ((IEnumerable<string>) source).Count<string>() == 2 ? new (string, string)?((source[0], source[1])) : new (string, string)?();
  }

  private bool TryToAuthWithCreds(string login, string password)
  {
    using (DirectoryEntry directoryEntry = this.createDirectoryEntry(login, password))
    {
      using (DirectorySearcher directorySearcher = this.createDirectorySearcher(directoryEntry))
      {
        directorySearcher.PropertiesToLoad.Add("cn");
        try
        {
          return directorySearcher.FindOne() != null;
        }
        catch (OutOfMemoryException ex)
        {
        }
        catch (StackOverflowException ex)
        {
        }
        catch (Exception ex)
        {
          LoggerExtensions.LogError(this.Logger, ex, "Failed to auth with exception", Array.Empty<object>());
        }
        return false;
      }
    }
  }

  private bool TryToAuthWithPrincipalContext(string login, string password)
  {
    try
    {
      if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        throw new ArgumentException("The user name or password is incorrect.");
      ContextOptions options = ContextOptions.SimpleBind;
      if (this._options.SSL)
        options |= ContextOptions.SecureSocketLayer;
      (string name, string container) tuple = this.SplitPath().Value;
      using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, tuple.name, tuple.container, options, this._user?.Login, this._user?.Password))
        return principalContext.ValidateCredentials(login, password, options);
    }
    catch (Exception ex)
    {
      LoggerExtensions.LogError(this.Logger, ex, "Failed to auth with exception", Array.Empty<object>());
    }
    return false;
  }

  private static List<Login> GetLogins(string samaccountname, IList distinguishednameProps)
  {
    List<Login> logins = new List<Login>();
    if (distinguishednameProps != null)
    {
      foreach (object distinguishednameProp in (IEnumerable) distinguishednameProps)
      {
        if (distinguishednameProp != null)
        {
          string str1 = distinguishednameProp.ToString();
          string domain = (string) null;
          char[] separator = new char[1]{ ',' };
          foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
          {
            int length = str2.IndexOf('=');
            if (length > -1 && length < str2.Length - 1)
            {
              string strA = str2.Substring(0, length);
              string str3 = str2.Substring(length + 1);
              if (string.Compare(strA, "dc", true) == 0)
                domain = str3;
            }
            if (domain != null)
            {
              logins.Add(new Login(samaccountname, domain));
              break;
            }
          }
        }
      }
    }
    return logins;
  }

  private User GetUserByLoginInternal(string providerUserKey)
  {
    Login login = ActiveDirectoryProvider.ExtractLogin(providerUserKey, this.DefaultDomainComponent);
    HashSet<string> allowedDomains = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
      this.DefaultDomainComponent,
      login.Domain
    };
    foreach (IDictionary<string, IList> dictionary in this.SearchInDirectory($"(&(objectCategory=user)(samaccountname={login.Name}))", "objectsid", "distinguishedname", "displayname", "givenname", "sn", "mail", "description", "lastlogon", "pwdlastset", "objectguid"))
    {
      string objectSid = LDAPActiveDirectoryProvider.ExtractObjectSID(dictionary, "objectsid");
      if (!string.IsNullOrEmpty(objectSid))
      {
        List<Login> logins = LDAPActiveDirectoryProvider.GetLogins(login.Name, LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "distinguishedname"));
        if (logins.Find((Predicate<Login>) (user => allowedDomains.Contains(user.Domain))) != null)
          return new User(objectSid, LDAPActiveDirectoryProvider.ExtractObjectGUID(dictionary, "objectguid"), new NameInfo(login.Name, (IEnumerable<Login>) logins), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "displayname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "givenname")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "sn")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "mail")), LDAPActiveDirectoryProvider.ConvertToString(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "description")), new System.DateTime(2000, 1, 1), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "lastlogon")), LDAPActiveDirectoryProvider.ConvertToDateTime(LDAPActiveDirectoryProvider.GetValueOfAttribute(dictionary, "pwdlastset")));
      }
    }
    return (User) null;
  }

  private IEnumerable<IDictionary<string, IList>> SearchInDirectory(
    string query,
    params string[] paramsToLoad)
  {
    using (HostingEnvironment.Impersonate())
    {
      DirectorySearcher directorySearcher = this.createDirectorySearcher(this.createDirectoryEntry(this._user?.Login, this._user?.Password));
      directorySearcher.PageSize = 50;
      directorySearcher.SizeLimit = 0;
      directorySearcher.Filter = query;
      directorySearcher.PropertiesToLoad.AddRange(paramsToLoad);
      List<IDictionary<string, IList>> dictionaryList = new List<IDictionary<string, IList>>();
      SearchResultCollection resultCollection = (SearchResultCollection) null;
      try
      {
        resultCollection = directorySearcher.FindAll();
        foreach (SearchResult searchResult in resultCollection)
        {
          Dictionary<string, IList> dictionary = new Dictionary<string, IList>();
          foreach (string propertyName in (IEnumerable) searchResult.Properties.PropertyNames)
            dictionary.Add(propertyName, (IList) new ArrayList((ICollection) searchResult.Properties[propertyName]));
          foreach (string key in paramsToLoad)
          {
            if (!dictionary.ContainsKey(key))
              dictionary.Add(key, (IList) new string[0]);
          }
          dictionaryList.Add((IDictionary<string, IList>) dictionary);
        }
      }
      catch (OutOfMemoryException ex)
      {
        throw;
      }
      catch (StackOverflowException ex)
      {
        throw;
      }
      catch
      {
      }
      finally
      {
        resultCollection?.Dispose();
      }
      return (IEnumerable<IDictionary<string, IList>>) dictionaryList;
    }
  }

  private DirectorySearcher createDirectorySearcher(DirectoryEntry de)
  {
    return new DirectorySearcher(de)
    {
      ClientTimeout = TimeSpan.FromSeconds((double) this._options.Timeout)
    };
  }

  private DirectoryEntry createDirectoryEntry(string user, string password)
  {
    string path = string.IsNullOrEmpty(this._path) ? (string) null : this._path;
    if (this._options.SSL)
    {
      if (user == null || password == null)
        throw new ArgumentException("LDAPS requires username and password");
      return new DirectoryEntry(path, user, password, AuthenticationTypes.Encryption);
    }
    return user != null && password != null ? new DirectoryEntry(path, user, password) : new DirectoryEntry(path);
  }

  private SearchResult SearchOnceInDirectory(string query, params string[] paramsToLoad)
  {
    using (HostingEnvironment.Impersonate())
    {
      DirectorySearcher directorySearcher = this.createDirectorySearcher(this.createDirectoryEntry(this._user?.Login, this._user?.Password));
      directorySearcher.Filter = query;
      directorySearcher.PropertiesToLoad.AddRange(paramsToLoad);
      return directorySearcher.FindOne();
    }
  }

  private static IDictionary<string, IList> DecodeKeyValuePairs(string data)
  {
    if (string.IsNullOrEmpty(data))
      return (IDictionary<string, IList>) new Dictionary<string, IList>(0);
    string[] strArray = data.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    Dictionary<string, IList> dictionary = new Dictionary<string, IList>(strArray.Length);
    foreach (string str1 in strArray)
    {
      int length = str1.IndexOf('=');
      if (length > -1)
      {
        string lower = str1.Substring(0, length).ToLower();
        string str2 = length < str1.Length - 1 ? str1.Substring(length + 1) : string.Empty;
        if (!dictionary.ContainsKey(lower))
          dictionary.Add(lower, (IList) new List<string>());
        dictionary[lower].Add((object) str2);
      }
    }
    return (IDictionary<string, IList>) dictionary;
  }

  private static string ExtractObjectSID(IDictionary<string, IList> attributes, string attName)
  {
    string objectSid = (string) null;
    byte[] valueOfAttribute1 = LDAPActiveDirectoryProvider.GetFirstValueOfAttribute<byte[]>(attributes, attName);
    if (valueOfAttribute1 != null)
      objectSid = new SecurityIdentifier(valueOfAttribute1, 0).ToString();
    if (objectSid == null)
    {
      string[] valueOfAttribute2 = LDAPActiveDirectoryProvider.GetFirstValueOfAttribute<string[]>(attributes, attName);
      if (valueOfAttribute2 != null && valueOfAttribute2.Length != 0)
        objectSid = valueOfAttribute2[0];
    }
    if (objectSid == null)
    {
      string valueOfAttribute3 = LDAPActiveDirectoryProvider.GetFirstValueOfAttribute<string>(attributes, attName);
      if (valueOfAttribute3 != null)
        objectSid = valueOfAttribute3;
    }
    return objectSid;
  }

  private static Guid? ExtractObjectGUID(IDictionary<string, IList> attributes, string attName)
  {
    IList valueOfAttribute = LDAPActiveDirectoryProvider.GetValueOfAttribute(attributes, attName);
    return valueOfAttribute != null && valueOfAttribute.Count > 0 ? new Guid?(new Guid((byte[]) valueOfAttribute[0])) : new Guid?();
  }

  private static IEnumerable<string> ExtractParameter(IList data, string parameter)
  {
    if (data != null && data.Count != 0 && !string.IsNullOrEmpty(parameter))
    {
      foreach (object obj in (IEnumerable) data)
      {
        string[] strArray = obj.ToString().Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries);
        for (int index = 0; index < strArray.Length; ++index)
        {
          string str1 = strArray[index];
          int length = str1.IndexOf('=');
          if (length > -1)
          {
            string strA = str1.Substring(0, length);
            string str2 = length == str1.Length - 1 ? string.Empty : str1.Substring(length + 1);
            string strB = parameter;
            if (string.Compare(strA, strB, true) == 0)
              yield return str2;
          }
        }
        strArray = (string[]) null;
      }
    }
  }

  private static IList GetValueOfAttribute(IDictionary<string, IList> dic, string attributeName)
  {
    return !dic.ContainsKey(attributeName) ? (IList) null : dic[attributeName];
  }

  private static T GetFirstValueOfAttribute<T>(IDictionary<string, IList> dic, string attributeName) where T : class
  {
    return LDAPActiveDirectoryProvider.GetFirstValueOfAttribute<T>(dic.ContainsKey(attributeName) ? dic[attributeName] : (IList) null);
  }

  private static T GetFirstValueOfAttribute<T>(IList attribute) where T : class
  {
    return attribute != null && attribute.Count > 0 ? attribute[0] as T : attribute as T;
  }

  private static System.DateTime ConvertToDateTime(IList data)
  {
    if (data != null)
    {
      foreach (object obj in (IEnumerable) data)
      {
        long result;
        if (obj != null && long.TryParse(obj.ToString(), out result))
          return result == 0L ? System.DateTime.MinValue : new System.DateTime(1601, 1, 1).AddTicks(result);
      }
    }
    return System.DateTime.MinValue;
  }

  private static string ConvertToString(IList data)
  {
    if (data == null || data.Count == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    bool flag = false;
    foreach (object obj in (IEnumerable) data)
    {
      if (obj != null)
      {
        if (flag)
          stringBuilder.Append("; ");
        stringBuilder.Append(obj);
        flag = true;
      }
    }
    return stringBuilder.ToString();
  }

  private static string ConvertBytes(byte[] source)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (source != null)
    {
      foreach (byte num in source)
        stringBuilder.AppendFormat("{0:00#}", (object) num);
    }
    return stringBuilder.ToString();
  }
}
