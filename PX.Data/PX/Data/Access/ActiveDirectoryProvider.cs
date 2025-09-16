// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectoryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data.Access.ActiveDirectory;
using PX.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace PX.Data.Access;

[PXInternalUseOnly]
public abstract class ActiveDirectoryProvider
{
  private string _defaultDC;
  private bool _defaultDCCalced;
  protected readonly string _path;
  protected readonly Credential _user;
  private readonly IExpirationStorage _expirationStorage;
  private readonly ConcurrentDictionary<string, Lazy<User>> _usersCache = new ConcurrentDictionary<string, Lazy<User>>();
  private readonly ConcurrentDictionary<string, Lazy<User[]>> _usersByGroupCache = new ConcurrentDictionary<string, Lazy<User[]>>();
  private readonly ConcurrentDictionary<string, Lazy<string[]>> _groupsByUserCache = new ConcurrentDictionary<string, Lazy<string[]>>();
  private readonly ActiveDirectoryProvider.LazyInitializable<User[]> _allUsersCache = new ActiveDirectoryProvider.LazyInitializable<User[]>();
  private readonly ActiveDirectoryProvider.LazyInitializable<PX.Data.Access.ActiveDirectory.Group[]> _allGroupsCache = new ActiveDirectoryProvider.LazyInitializable<PX.Data.Access.ActiveDirectory.Group[]>();
  private readonly ActiveDirectoryProvider.LazyInitializable<string[]> _allDomains = new ActiveDirectoryProvider.LazyInitializable<string[]>();
  private static readonly Regex _sidRegex = new Regex("^S-\\d-(\\d+-){1,14}\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

  internal static IActiveDirectoryProvider CreateInstance(IServiceProvider serviceProvider)
  {
    IConfiguration requiredService = ServiceProviderServiceExtensions.GetRequiredService<IConfiguration>(serviceProvider);
    PX.Data.Access.ActiveDirectory.Options options = ServiceProviderServiceExtensions.GetRequiredService<IOptions<PX.Data.Access.ActiveDirectory.Options>>(serviceProvider).Value;
    if (!options.Enabled)
      return (IActiveDirectoryProvider) null;
    ConfigurationSectionWithProviders configurationSection1 = ConfigurationBuilderExtensions.GetMembershipConfigurationSection(requiredService);
    bool flag = false;
    if (string.IsNullOrEmpty(configurationSection1.DefaultProvider))
    {
      ProviderConfigurationSection configurationSection2 = configurationSection1.Providers.FirstOrDefault<ProviderConfigurationSection>();
      flag = configurationSection2 != null && string.Compare(configurationSection2.Name, configurationSection1.DefaultProvider, true) == 0;
    }
    else
    {
      foreach (ProviderConfigurationSection provider in configurationSection1.Providers)
      {
        if (string.Compare(provider.Name, configurationSection1.DefaultProvider, true) == 0)
        {
          System.Type type;
          flag = !string.IsNullOrEmpty(provider.Type) && (type = System.Type.GetType(provider.Type, false)) != (System.Type) null && typeof (PXActiveDirectorySyncMembershipProvider).IsAssignableFrom(type);
          break;
        }
      }
    }
    if (!flag)
      return (IActiveDirectoryProvider) null;
    switch (options.Protocol)
    {
      case ActiveDirectoryProtocol.LDAP:
        return (IActiveDirectoryProvider) ActivatorUtilities.CreateInstance<LDAPActiveDirectoryProvider>(serviceProvider, Array.Empty<object>());
      case ActiveDirectoryProtocol.ADAL:
        return (IActiveDirectoryProvider) ActivatorUtilities.CreateInstance<ADALActiveDirectoryProvider>(serviceProvider, Array.Empty<object>());
      case ActiveDirectoryProtocol.Claim:
        return (IActiveDirectoryProvider) ActivatorUtilities.CreateInstance<ClaimActiveDirectoryProvider>(serviceProvider, Array.Empty<object>());
      case ActiveDirectoryProtocol.MicrosoftGraph:
        return (IActiveDirectoryProvider) ActivatorUtilities.CreateInstance<GraphApiActiveDirectoryProvider>(serviceProvider, Array.Empty<object>());
      default:
        return (IActiveDirectoryProvider) null;
    }
  }

  public string DefaultDomainComponent
  {
    get
    {
      if (this._defaultDC == null)
      {
        if (!this._defaultDCCalced)
        {
          try
          {
            Domain domain = Domain.GetComputerDomain() ?? Domain.GetCurrentDomain();
            if (domain != null)
              this._defaultDC = domain.Name;
          }
          catch (AuthenticationException ex)
          {
          }
          catch (ActiveDirectoryObjectNotFoundException ex)
          {
          }
          this._defaultDCCalced = true;
        }
      }
      return this._defaultDC;
    }
  }

  protected ILogger Logger { get; }

  protected PX.Data.Access.ActiveDirectory.Options Options { get; }

  internal ActiveDirectoryProvider(
    string path,
    Credential user,
    PX.Data.Access.ActiveDirectory.Options options,
    ILoggerFactory loggerFactory,
    IExpirationStorage expirationStorage)
  {
    if (path != null && (path = path.Trim()).Length > 0)
      this._path = path;
    this._user = user;
    this.Options = options;
    this._expirationStorage = expirationStorage;
    this._defaultDC = options.DC;
    this.Logger = LoggerFactoryExtensions.CreateLogger(loggerFactory, this.GetType());
  }

  public virtual void Reset()
  {
    this._usersCache.Clear();
    this._usersByGroupCache.Clear();
    this._groupsByUserCache.Clear();
    this._allGroupsCache.Reset();
    this._allUsersCache.Reset();
    this._allDomains.Reset();
  }

  private T GetCachedValue<T>(object key, Func<T> getValueFromCache, System.Action removeValueFromCache)
  {
    try
    {
      T cachedValue = getValueFromCache();
      this._expirationStorage.ClearExpiration(key);
      return cachedValue;
    }
    catch
    {
      if (!this.Options.KeepErrorsInCache && this._expirationStorage.IsExpired(key))
      {
        this._expirationStorage.UpdateExpiration(key);
        removeValueFromCache();
      }
      throw;
    }
  }

  protected T GetCacheable<T>(
    ActiveDirectoryProvider.LazyInitializable<T> cache,
    Func<T> del,
    bool keepCache)
  {
    if (keepCache)
      return this.GetCachedValue<T>((object) cache, (Func<T>) (() => cache.Get(del)), (System.Action) (() => cache.Reset()));
    T res = del();
    if (cache.Initialized)
    {
      cache.Reset();
      cache.Get((Func<T>) (() => res));
    }
    return res;
  }

  protected T GetCacheable<T>(
    ConcurrentDictionary<string, Lazy<T>> cache,
    string key,
    Func<T> del,
    bool keepCache)
  {
    if (keepCache)
      return this.GetCachedValue<T>((object) (cache, key), (Func<T>) (() => cache.GetOrAdd(key, (Func<string, Lazy<T>>) (_ => new Lazy<T>(del))).Value), (System.Action) (() => cache.TryRemove(key, out Lazy<T> _)));
    T res = del();
    if (cache.ContainsKey(key))
    {
      Lazy<T> lazy = new Lazy<T>((Func<T>) (() => res));
      cache.AddOrUpdate(key, lazy, (Func<string, Lazy<T>, Lazy<T>>) ((k, o) => lazy));
    }
    return res;
  }

  protected User GetCachedUser(string key, Func<User> del, bool keepCache)
  {
    return this.GetCacheable<User>(this._usersCache, key, del, keepCache);
  }

  protected IEnumerable<string> GetCachedGroupsForUser(
    string key,
    Func<string[]> del,
    bool keepCache)
  {
    return (IEnumerable<string>) this.GetCacheable<string[]>(this._groupsByUserCache, key, del, keepCache);
  }

  protected IEnumerable<User> GetCachedUsersForGroup(string key, Func<User[]> del, bool keepCache)
  {
    return (IEnumerable<User>) this.GetCacheable<User[]>(this._usersByGroupCache, key, del, keepCache);
  }

  protected IEnumerable<User> GetCachedUsers(Func<User[]> del, bool keepCache)
  {
    return (IEnumerable<User>) this.GetCacheable<User[]>(this._allUsersCache, del, keepCache);
  }

  protected IEnumerable<PX.Data.Access.ActiveDirectory.Group> GetCachedGroups(
    Func<PX.Data.Access.ActiveDirectory.Group[]> del,
    bool keepCache)
  {
    return (IEnumerable<PX.Data.Access.ActiveDirectory.Group>) this.GetCacheable<PX.Data.Access.ActiveDirectory.Group[]>(this._allGroupsCache, del, keepCache);
  }

  protected IEnumerable<string> GetCachedDomains(Func<string[]> del)
  {
    return (IEnumerable<string>) this.GetCacheable<string[]>(this._allDomains, del, true);
  }

  public virtual bool IsUserDisabled(string login) => false;

  internal static string GetDomainLogin(IEnumerable<Login> logins)
  {
    return logins == null || !logins.Any<Login>() ? (string) null : $"{string.Join(".", logins.Select<Login, string>((Func<Login, string>) (l => l.Domain)))}\\{logins.First<Login>().Name}";
  }

  [PXInternalUseOnly]
  public static Login ExtractLogin(
    string str,
    string defaultDomain,
    IEnumerable<string> allowedDomains = null)
  {
    (string Name, string Domain) nameDomain = ActiveDirectoryProvider.ExtractNameDomain(str, defaultDomain, allowedDomains);
    return new Login(nameDomain.Name, nameDomain.Domain);
  }

  internal static (string Name, string Domain) ExtractNameDomain(
    string str,
    string defaultDomain,
    IEnumerable<string> allowedDomains = null)
  {
    int length1 = str == null ? -1 : str.IndexOf('\\');
    int length2 = str == null ? -1 : str.IndexOf('@');
    if (length1 > -1 && length1 < str.Length - 1)
      return (str.Substring(length1 + 1), str.Substring(0, length1));
    if (length2 <= -1 || length2 >= str.Length - 1)
      return (str, defaultDomain);
    string str1 = str.Substring(0, length2);
    string str2 = str.Substring(length2 + 1);
    if (allowedDomains != null && !allowedDomains.Contains<string>(str2))
    {
      str1 = $"{str1}@{str2}";
      str2 = defaultDomain;
    }
    return (str1, str2);
  }

  [DebuggerStepThrough]
  protected static bool IsSID(string str)
  {
    if (!string.IsNullOrEmpty(str))
    {
      if (ActiveDirectoryProvider._sidRegex.IsMatch(str.Trim()))
      {
        try
        {
          SecurityIdentifier securityIdentifier = new SecurityIdentifier(str);
        }
        catch (ArgumentException ex)
        {
          return false;
        }
        return true;
      }
    }
    return false;
  }

  [DebuggerStepThrough]
  protected static bool IsGUID(string str) => Guid.TryParse(str, out Guid _);

  protected class LazyInitializable<T>
  {
    private bool _initialized;
    private object _lock = new object();
    private T _val;

    public void Reset() => this._initialized = false;

    public T Get(Func<T> factory)
    {
      return LazyInitializer.EnsureInitialized<T>(ref this._val, ref this._initialized, ref this._lock, factory);
    }

    public bool Initialized => this._initialized;
  }
}
