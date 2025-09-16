// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionStateStore
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.AspNet.SessionState;
using PX.Common;
using PX.Common.Context;
using PX.Common.Session;
using PX.Data.Utility;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI;

#nullable enable
namespace PX.Data;

public sealed class PXSessionStateStore : SessionStateStoreProviderBase
{
  private 
  #nullable disable
  PXSessionStateStoreImpl _provider;
  [Obsolete("This object is obsolete and will be removed. Rewrite your code without this object or contact your partner for assistance.")]
  public static readonly bool SerializeSessionItems = WebConfig.SerializeSessionItems;
  private static readonly PXSessionStateStore.SetAppPathModifierDelegate _setAppPathModifier = PXSessionStateStore.InitDelegateSetAppPathModifier();
  private string _ignoreUrl;
  private static readonly ConcurrentDictionary<HttpApplication, PXSessionStateStore> ProviderToAppinstanceMap = new ConcurrentDictionary<HttpApplication, PXSessionStateStore>();
  private static readonly PXSessionStateStore.PXSessionStateStorePool Pool = new PXSessionStateStore.PXSessionStateStorePool();
  internal static readonly TimeSpan DefaultSessionTimeout = new TimeSpan(0, 20, 0);
  private bool _IsProviderInitStarted;

  private static 
  #nullable enable
  PXSessionStateStore GetStore(HttpContext context)
  {
    return PXSessionStateStore.GetStore(context.ApplicationInstance);
  }

  private static PXSessionStateStore GetStore(HttpApplication application)
  {
    return PXSessionStateStore.ProviderToAppinstanceMap[application];
  }

  /// <remarks>When session is not found, a new one will be created</remarks>
  internal static void SetValueInPXSession<T>(string sessionId, SessionKey<T> key, T value)
  {
    PXSessionStateStore.ProcessWithSessionContext(sessionId, (System.Action<PXSessionStateStoreData>) (data => data.PXSessionState.Set<T>(key, value)));
  }

  /// <remarks>When session is not found, a new one will be created</remarks>
  internal static void SetValueInPXSession(string sessionId, string key, object value)
  {
    PXSessionStateStore.ProcessWithSessionContext(sessionId, (System.Action<PXSessionStateStoreData>) (data => data.PXSessionState.Set(key, value)));
  }

  internal static void AddHttpSessionStateToContext(
    HttpContext httpContext,
    string sessionId,
    int sessionTimeout)
  {
    SessionStateUtility.AddHttpSessionStateToContext(httpContext, (IHttpSessionState) new HttpSessionStateContainer(sessionId, (ISessionStateItemCollection) new SessionStateItemCollection(), new HttpStaticObjectsCollection(), sessionTimeout, false, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false));
    PXSessionStateStore.InitContext(httpContext, (IPXSessionState) new DictionarySessionState());
  }

  private static void InitContext(HttpContext context, IPXSessionState session)
  {
    if (HttpContextPXSessionState.Get(context.Items) != null)
      throw new InvalidOperationException("IPXSessionState was already initialized for HttpContext");
    HttpContextPXSessionState.Set(context, session);
  }

  private static void RemoveFromContext(HttpContext context, IPXSessionState session)
  {
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(context.Items);
    if (pxSessionState == null)
      throw new InvalidOperationException("IPXSessionState was not initialized for HttpContext");
    if (pxSessionState != session)
      throw new InvalidOperationException("HttpContext references different IPXSessionState");
    HttpContextPXSessionState.Remove(context);
  }

  internal static void ClearSession(HttpContextBase httpContext)
  {
    PXSessionStateStore.GetStore(httpContext.ApplicationInstance).GetProvider().Clear(httpContext.Session);
  }

  private 
  #nullable disable
  PXSessionStateStoreImpl GetProvider()
  {
    if (!this._IsProviderInitStarted)
      throw new Exception("An attempt to access the session before the session provider is initialized.");
    return this._provider;
  }

  private int GetSessionTimeout(string sessionId)
  {
    int totalMinutes = (int) this.SessionTimeout.TotalMinutes;
    string.IsNullOrEmpty(sessionId);
    return totalMinutes;
  }

  internal static void DisposeCurrentStore()
  {
    PXSessionStateStore.GetStore(HttpContext.Current ?? throw new Exception("PXSessionStateStore DisposeCurrentStore: Cannot get SessionProvider bound to an application instance.")).Dispose();
  }

  private static PXSessionStateStore.SetAppPathModifierDelegate InitDelegateSetAppPathModifier()
  {
    DynamicMethod dynamicMethod = new DynamicMethod("SetAppPathModifier", typeof (void), new System.Type[2]
    {
      typeof (HttpResponse),
      typeof (string)
    }, typeof (PXSessionStateStore), true);
    ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
    ilGenerator.Emit(OpCodes.Ldarg_0);
    ilGenerator.Emit(OpCodes.Ldarg_1);
    ilGenerator.Emit(OpCodes.Callvirt, typeof (HttpResponse).GetMethod("SetAppPathModifier", BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[1]
    {
      typeof (string)
    }, (ParameterModifier[]) null));
    ilGenerator.Emit(OpCodes.Ret);
    return (PXSessionStateStore.SetAppPathModifierDelegate) dynamicMethod.CreateDelegate(typeof (PXSessionStateStore.SetAppPathModifierDelegate));
  }

  public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
  {
    PXSessionStateStoreData newStoreData = this._provider.CreateNewStoreData(context, timeout);
    PXSessionStateStore.InitContext(context, newStoreData.PXSessionState);
    return (SessionStateStoreData) newStoreData;
  }

  public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
  {
    timeout = this.GetSessionTimeout(id);
    this._provider.CreateUninitializedItem(context, PXSessionStateStore.alterId(context, id), timeout);
    object lockId;
    PXSessionStateStoreData itemExclusive = this.ProviderGetItemExclusive(context, PXSessionStateStore.alterId(context, id), out bool _, out TimeSpan _, out lockId, out SessionStateActions _);
    if (itemExclusive == null)
      return;
    IPXSessionState pxSessionState = itemExclusive.PXSessionState;
    if (PXSessionStateStore.IsCurrentSessionItem(context, id))
      PXSessionStateStore.RegenerateSessionStateItem(context, pxSessionState);
    string justCreatedSuffix = this.GetJustCreatedSuffix(context, id);
    if (justCreatedSuffix != null)
    {
      pxSessionState.Set<string>(PXSessionStateStore.Keys.JustCreated, justCreatedSuffix);
      PXSessionStateStore.ResetSuffixCookie(context);
    }
    this.ProviderSetAndReleaseItemExclusive(context, PXSessionStateStore.alterId(context, id), itemExclusive, lockId, false);
  }

  private static bool IsCurrentSessionItem(HttpContext context, string id)
  {
    string suffix = PXSessionStateStore.GetSuffix(context);
    string suffixFromReferrer = PXSessionStateStore.GetSuffixFromReferrer(context);
    if (suffix == suffixFromReferrer)
      return true;
    return string.IsNullOrEmpty(suffix) && string.IsNullOrEmpty(suffixFromReferrer);
  }

  internal static bool CopyStateItemsRequired(HttpContext context)
  {
    return WebConfig.EnableConcurrentSessionAccess && context.Items.Contains((object) nameof (CopyStateItemsRequired)) && (bool) context.Items[(object) nameof (CopyStateItemsRequired)];
  }

  private static void RegenerateSessionStateItem(HttpContext context, IPXSessionState session)
  {
    string company = (string) null;
    string username = (string) null;
    string name = context.User?.Identity?.Name;
    string str;
    if (!string.IsNullOrEmpty(name))
      LegacyCompanyService.ParseLogin(name, out username, out company, out str);
    if (string.IsNullOrEmpty(company))
      return;
    string companyIdFromUri = PXSessionStateStore.GetCompanyIDFromUri(context);
    if (!(companyIdFromUri != company))
      return;
    if (!string.IsNullOrEmpty(companyIdFromUri) && ServiceLocator.Current.GetInstance<IPXLogin>().SwitchCompany(companyIdFromUri, out str))
    {
      PXSessionContext sessionContext = PXSessionContextFactory.SessionContext;
      sessionContext.UserName = $"{username}@{companyIdFromUri}";
      sessionContext.MarkSaved();
      PXSessionContextFactory.SetSessionBucket(session, sessionContext);
    }
    else
      session.Set<bool>(PXSessionStateStore.Keys.FailedToRegenerate, true);
  }

  public override void Dispose()
  {
    foreach (KeyValuePair<HttpApplication, PXSessionStateStore> providerToAppinstance in PXSessionStateStore.ProviderToAppinstanceMap)
    {
      if (providerToAppinstance.Value == this)
        PXSessionStateStore.ProviderToAppinstanceMap.TryRemove(providerToAppinstance.Key, out PXSessionStateStore _);
    }
    this._provider.Dispose();
  }

  public override void EndRequest(HttpContext context)
  {
    PXSessionStateStore.CommitSharedSession(context);
    this._provider.EndRequest(context);
  }

  private PXSessionStateStoreData ProviderGetItem(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actionFlags)
  {
    PXPerformanceInfo slot = PXContext.GetSlot<PXPerformanceInfo>();
    slot?.SessionLoadTimer.Start();
    string id1 = PXSessionStateStore.alterId(context, id);
    PXSessionStateStoreData sessionStateStoreData = this.GetProvider().GetItem(context, id1, out locked, out lockAge, out lockId, out actionFlags);
    if (slot == null)
      return sessionStateStoreData;
    slot.SessionLoadTimer.Stop();
    if (!locked)
      return sessionStateStoreData;
    ++slot.SessionLockCount;
    return sessionStateStoreData;
  }

  public override SessionStateStoreData GetItem(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actionFlags)
  {
    PXSessionStateStoreData sessionStateStoreData = this.ProviderGetItem(context, PXSessionStateStore.alterId(context, id), out locked, out lockAge, out lockId, out actionFlags);
    if (sessionStateStoreData != null)
      PXSessionStateStore.InitContext(context, sessionStateStoreData.PXSessionState);
    return (SessionStateStoreData) sessionStateStoreData;
  }

  private PXSessionStateStoreData ProviderGetItemExclusive(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actionFlags)
  {
    return this.GetProvider().GetItemExclusive(context, id, out locked, out lockAge, out lockId, out actionFlags);
  }

  public override SessionStateStoreData GetItemExclusive(
    HttpContext context,
    string id,
    out bool locked,
    out TimeSpan lockAge,
    out object lockId,
    out SessionStateActions actionFlags)
  {
    string suffix = PXSessionStateStore.GetSuffix(context);
    string id1 = suffix == "" ? id : $"{id}${suffix}";
    PXSessionStateStoreData itemExclusive = this.ProviderGetItemExclusive(context, id1, out locked, out lockAge, out lockId, out actionFlags);
    try
    {
      if (itemExclusive != null)
      {
        IPXSessionState pxSessionState = itemExclusive.PXSessionState;
        context.Items[(object) "SessionObject"] = (object) itemExclusive;
        context.Items[(object) "_rqSID"] = (object) id1;
        context.Items[(object) "_rqLockID"] = lockId;
        DataSourceSession.EnsureInitialized(pxSessionState, id);
        if (!pxSessionState.Contains<int>(PXSessionStateStore.Keys.CodeVersion))
          pxSessionState.Set<int>(PXSessionStateStore.Keys.CodeVersion, PXCodeDirectoryCompiler.Version);
        this.CheckPrevItem(context, pxSessionState, suffix, id);
        TypeKeyedOperationExtensions.Set<PXCacheExtensionCollection>(context.Slots(), new PXCacheExtensionCollection());
        PXSessionStateStore.CheckDynamicExtensions(pxSessionState);
        PXSessionStateStore.InitContext(context, itemExclusive.PXSessionState);
      }
      return (SessionStateStoreData) itemExclusive;
    }
    catch
    {
      this._provider.ReleaseItemExclusive(context, id1, lockId);
      throw;
    }
  }

  private static void CheckDynamicExtensions(IPXSessionState session)
  {
    PXSessionStateStore.CheckDynamicExtensions(session, session);
  }

  private static void CheckDynamicExtensions(
    IPXSessionState session,
    IPXSessionState codeVersionSource)
  {
    int num;
    if (codeVersionSource.TryGet<int>(PXSessionStateStore.Keys.CodeVersion, out num) && num == PXCodeDirectoryCompiler.Version || !session.CheckDynamicExtensions())
      return;
    session.Set<int>(PXSessionStateStore.Keys.CodeVersion, PXCodeDirectoryCompiler.Version);
  }

  internal static void InitExtensions(HttpContext httpContext)
  {
    try
    {
      if (HttpContextPXSessionState.Get(httpContext) == null)
        return;
      ISlotStore islotStore = httpContext.Slots();
      if (TypeKeyedOperationExtensions.Get<PXCacheExtensionCollection>(islotStore) != null)
        return;
      TypeKeyedOperationExtensions.Set<PXCacheExtensionCollection>(islotStore, new PXCacheExtensionCollection());
    }
    catch
    {
    }
  }

  /// <summary>
  /// If current session is result of redirect
  /// load parent session and copy redirect specific data
  /// </summary>
  private void CheckPrevItem(
    HttpContext context,
    IPXSessionState session,
    string suffix,
    string id)
  {
    string fromSuffix = session.Get<string>(PXSessionStateStore.Keys.JustCreated);
    bool forcePrefix = false;
    int result;
    if (string.IsNullOrEmpty(fromSuffix) && suffix != null && suffix.Length > 7 && string.Equals(suffix.Substring(0, 2), "W(", StringComparison.OrdinalIgnoreCase) && suffix[suffix.Length - 1] == ')' && int.TryParse(suffix.Substring(2, suffix.Length - 3), out result))
    {
      if (result > 10000)
      {
        fromSuffix = $"W({(result - 10000).ToString()})";
        forcePrefix = result < 20000;
      }
      else if (result == 10000)
      {
        fromSuffix = "";
        forcePrefix = true;
      }
    }
    if (this.IgnoreUrl(context))
      return;
    bool flag = !PXUrl.IsMainPage(context.Request.AppRelativeCurrentExecutionFilePath);
    if (fromSuffix == null)
    {
      if (!flag || "~/frames/login.aspx".Contains(context.Request.AppRelativeCurrentExecutionFilePath.ToLower()))
        return;
      PXSessionContext tempSessionBucket = PXSessionContextFactory.GetTempSessionBucket(session);
      if (tempSessionBucket == null)
        return;
      PXSessionContextFactory.SetSessionBucket(session, tempSessionBucket);
      PXSessionContextFactory.RemoveTempSessionBucket(session);
    }
    else
    {
      if (!(context.CurrentHandler is MvcHandler) && (context.CurrentHandler is Control currentHandler ? currentHandler.Page : (Page) null) == null && (!(context.Request.HttpMethod != HttpMethod.Get.Method) || !context.IsModernUIRequest()))
        return;
      if (flag)
      {
        session.Remove<string>(PXSessionStateStore.Keys.JustCreated);
        session.Remove<bool>(PXSessionStateStore.Keys.AlreadyCopied);
      }
      if (!(fromSuffix != suffix))
        return;
      object lockId = (object) null;
      PXSessionStateStoreData sessionStateStoreData = (PXSessionStateStoreData) null;
      try
      {
        for (int index = 0; index < 50; ++index)
        {
          bool locked;
          sessionStateStoreData = this.ProviderGetItemExclusive(context, fromSuffix == "" ? id : $"{id}${fromSuffix}", out locked, out TimeSpan _, out lockId, out SessionStateActions _);
          if (sessionStateStoreData == null && locked)
            Thread.Sleep(200);
          else
            break;
        }
        if (sessionStateStoreData == null)
          return;
        System.Type type = (System.Type) null;
        string url = context.GetScreenRelativeUrl();
        IPXSessionState pxSessionState = sessionStateStoreData.PXSessionState;
        if (flag)
        {
          type = pxSessionState.GetRedirectGraphType(url);
          if (type == (System.Type) null)
          {
            string screenRelativeUrl = context.GetScreenRelativeUrl(true);
            type = pxSessionState.GetRedirectGraphType(screenRelativeUrl);
            if (type != (System.Type) null)
              url = screenRelativeUrl;
          }
          if (type != (System.Type) null)
          {
            ISlotStore slots = context.Slots();
            slots.ClearSingleCompanyId();
            string userName = PXSessionContextFactory.GetSessionBucket(pxSessionState)?.UserName;
            if (userName != null)
              PXDatabase.Provider.InitializeCurrentCompany(slots, (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(userName), Array.Empty<string>()));
            type = PXGraph._GetWrapperType(type);
            PXSessionStateStore.HandleRedirect(pxSessionState, session, url, type, forcePrefix);
            PXReusableGraphFactory.Clear(session);
          }
          else
          {
            this.MovePageParams(url, pxSessionState, session);
            this.MovePageParams(context.Request.RawUrl.ToRelativeUrl(context.Request.ApplicationPath), pxSessionState, session, false);
          }
        }
        if (session.Contains<bool>(PXSessionStateStore.Keys.AlreadyCopied))
          return;
        PXSessionContext tempSessionBucket = PXSessionContextFactory.GetTempSessionBucket(pxSessionState);
        if (tempSessionBucket != null)
        {
          PXSessionContextFactory.SetSessionBucket(session, tempSessionBucket.Clone());
          if (flag)
            PXSessionContextFactory.RemoveTempSessionBucket(pxSessionState);
        }
        else
        {
          PXSessionContext sessionBucket = PXSessionContextFactory.GetSessionBucket(pxSessionState);
          if (sessionBucket != null)
            PXSessionContextFactory.SetSessionBucket(session, sessionBucket.Clone());
        }
        RefreshCheck.CopyUser(pxSessionState, fromSuffix, session, suffix);
        if (!flag || type != (System.Type) null)
          PXSessionStateStore.CheckDynamicExtensions(session, pxSessionState);
        session.Set<bool>(PXSessionStateStore.Keys.AlreadyCopied, true);
        foreach (string key in pxSessionState.Keys)
        {
          if (UploadFileMaintenance.IsAccessOverride(key))
            session.Set(key, pxSessionState.Get(key));
        }
      }
      finally
      {
        if (sessionStateStoreData != null)
        {
          string id1 = fromSuffix == "" ? id : $"{id}${fromSuffix}";
          this.ProviderSetAndReleaseItemExclusive(context, id1, sessionStateStoreData, lockId, false);
        }
      }
    }
  }

  private static void HandleRedirect(
    IPXSessionState prevSession,
    IPXSessionState session,
    string url,
    System.Type redirect,
    bool forcePrefix)
  {
    prevSession.RemoveRedirectGraphType(url);
    session.SetRedirectGraphType(url, redirect);
    foreach (string str in prevSession.Keys.Where<string>((Func<string, bool>) (n => n != url && n.EndsWith(url, StringComparison.OrdinalIgnoreCase))))
      session.Set(str, prevSession.Get(str));
    string graphStatePrefix = prevSession.GetRedirectGraphStatePrefix();
    prevSession.RemoveRedirectGraphStatePrefix();
    session.RemoveRedirectGraphStatePrefix();
    GraphSessionStatePrefix redirectData = PXSessionStateStore.FindRedirectData(prevSession, redirect, graphStatePrefix, forcePrefix);
    if (redirectData != null)
    {
      GraphSessionStatePrefix targetPrefix = string.IsNullOrEmpty(graphStatePrefix) ? GraphSessionStatePrefix.WithoutStatePrefix(redirect) : GraphSessionStatePrefix.Create(graphStatePrefix, redirect);
      RedirectToGraph.MoveCompleteGraphInfo(prevSession, redirectData, session, targetPrefix);
    }
    else
    {
      if (!forcePrefix)
        return;
      session.RemoveSubKeys(GraphSessionStatePrefix.WithoutStatePrefix(redirect));
    }
  }

  private static GraphSessionStatePrefix FindRedirectData(
    IPXSessionState session,
    System.Type redirect,
    string statePrefix,
    bool forcePrefix)
  {
    foreach (GraphSessionStatePrefix candidate in Candidates())
    {
      if (session.GetGraphInfo(candidate) != null)
        return candidate;
    }
    return (GraphSessionStatePrefix) null;

    IEnumerable<GraphSessionStatePrefix> Candidates()
    {
      yield return RedirectToGraph.CreateGraphSessionStatePrefixWithRedirect(redirect);
      if (!forcePrefix)
      {
        yield return GraphSessionStatePrefix.WithoutStatePrefix(redirect);
        if (!string.IsNullOrEmpty(statePrefix))
          yield return GraphSessionStatePrefix.Create(statePrefix, redirect);
      }
    }
  }

  private void MovePageParams(
    string url,
    IPXSessionState from,
    IPXSessionState to,
    bool clearPrev = true)
  {
    if (from.Get(url) is IPXResultset pxResultset)
    {
      to.Set(url, (object) pxResultset);
      if (!clearPrev)
        return;
      from.Remove(url);
    }
    else
    {
      object obj = (object) (from.Get(url) as Dictionary<string, string>) ?? (object) (from.Get(url) as PXReportsRedirectList);
      if (obj == null)
        return;
      to.Set(url, obj);
      if (!clearPrev)
        return;
      from.Remove(url);
    }
  }

  internal TimeSpan SessionTimeout { get; private set; } = PXSessionStateStore.DefaultSessionTimeout;

  internal static TimeSpan GetStoreSessionTimeout(HttpContext context)
  {
    return PXSessionStateStore.GetStore(context).SessionTimeout;
  }

  public override void Initialize(string name, NameValueCollection config)
  {
    PXSessionStateStore.PXSessionStateStorePool pool = PXSessionStateStore.Pool;
    if (pool.Config == null)
      pool.Config = config;
    this._IsProviderInitStarted = true;
    TimeSpan? configSessionTimeout = PXSessionStateStore.GetWebConfigSessionTimeout();
    if (configSessionTimeout.HasValue)
      this.SessionTimeout = configSessionTimeout.GetValueOrDefault();
    if (string.IsNullOrEmpty(name))
      name = "InProc Session State Provider";
    base.Initialize(name, config);
    this._provider = PXSessionStateStore.CreateProvider(config, "internalProvider", "System.Web.SessionState.InProcSessionStateStore");
    if (WebConfig.IsClusterEnabled && !WebConfig.IsMultiSiteMode && this._provider.GetType().FullName == "System.Web.SessionState.InProcSessionStateStore")
      throw new ConfigurationErrorsException("InProcSessionStateStore is used in the cluster mode.");
    this._ignoreUrl = config["ignoreUrl"];
    if (!(name != "PoolProvider"))
      return;
    HttpApplication applicationInstance = (HttpContext.Current ?? throw new Exception("PXSessionStateStore Initialize: Cannot bind SessionProvider to an application instance.")).ApplicationInstance;
    PXSessionStateStore.ProviderToAppinstanceMap[applicationInstance] = this;
  }

  internal static TimeSpan? GetWebConfigSessionTimeout()
  {
    return ((SessionStateSection) WebConfigurationManager.GetSection("system.web/sessionState"))?.Timeout;
  }

  private static PXSessionStateStoreImpl CreateProvider(
    NameValueCollection config,
    string name,
    string defaultType)
  {
    string typeName = config[name + "Type"] ?? defaultType;
    if (Str.IsNullOrEmpty(typeName))
      return (PXSessionStateStoreImpl) null;
    string str1 = config[name + "Config"] ?? "";
    string name1 = config[name + "Name"] ?? name;
    ProviderBase providerBase = (ProviderBase) ((IEnumerable<ConstructorInfo>) PXBuildManager.GetType(typeName, true).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).FirstOrDefault<ConstructorInfo>((Func<ConstructorInfo, bool>) (c => c.GetParameters().Length == 0)).Invoke(new object[0]);
    SessionStateStoreProviderBase inner;
    switch (providerBase)
    {
      case SessionStateStoreProviderAsyncBase provider:
        inner = (SessionStateStoreProviderBase) new AsyncToSyncSessionStateStore(provider);
        break;
      case SessionStateStoreProviderBase storeProviderBase1:
        inner = storeProviderBase1;
        break;
      default:
        throw new InvalidOperationException("Unexpected provider type: " + providerBase.GetType().FullName);
    }
    SessionStateStoreProviderBase storeProviderBase2 = (SessionStateStoreProviderBase) new PXSessionStateProviderWrapper(inner);
    if (WebConfig.SerializeSessionItems)
      storeProviderBase2 = (SessionStateStoreProviderBase) new PersistingSessionStateStoreProviderWrapper(storeProviderBase2);
    NameValueCollection config1 = new NameValueCollection();
    char[] chArray = new char[1]{ ';' };
    foreach (string str2 in str1.Split(chArray))
    {
      if (!string.IsNullOrEmpty(str2))
      {
        string[] source = str2.Split(new char[1]{ '=' }, 2);
        config1.Add(((IEnumerable<string>) source).First<string>(), ((IEnumerable<string>) source).Last<string>());
      }
    }
    storeProviderBase2.Initialize(name1, config1);
    return new PXSessionStateStoreImpl(storeProviderBase2);
  }

  public override void InitializeRequest(HttpContext context)
  {
    this._provider.InitializeRequest(context);
    string header = context.Request.Headers["AspFilterSessionId"];
    if (header == null || !header.StartsWith("W("))
      return;
    PXSessionStateStore._setAppPathModifier(context.Response, $"({header})");
  }

  public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
  {
    PXSessionStateStoreData sessionStateStoreData = (PXSessionStateStoreData) context.Items[(object) "SessionObject"];
    if (sessionStateStoreData != null)
    {
      if (sessionStateStoreData.Items.Dirty)
      {
        this.SetAndReleaseItemExclusive(context, id, sessionStateStoreData, lockId, false);
        return;
      }
      PXSessionStateStore.RemoveFromContext(context, sessionStateStoreData.PXSessionState);
    }
    else
      HttpContextPXSessionState.Remove(context);
    this._provider.ReleaseItemExclusive(context, PXSessionStateStore.alterId(context, id), lockId);
  }

  public override void RemoveItem(
    HttpContext context,
    string id,
    object lockId,
    SessionStateStoreData item)
  {
    this.RemoveItem(context, id, lockId, (PXSessionStateStoreData) item);
  }

  private void RemoveItem(
    HttpContext context,
    string id,
    object lockId,
    PXSessionStateStoreData item)
  {
    PXSessionStateStore.RemoveFromContext(context, item.PXSessionState);
    this._provider.RemoveItem(context, PXSessionStateStore.alterId(context, id), lockId, item);
  }

  public override void ResetItemTimeout(HttpContext context, string id)
  {
    this._provider.ResetItemTimeout(context, PXSessionStateStore.alterId(context, id));
  }

  private void ProviderSetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    PXSessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    if (context != null && context.Items[(object) "_rqCommited"] != null)
      return;
    string str = "";
    if (context != null && context.User != null)
      str = context.User.Identity.Name;
    PXSessionListItem pxSessionListItem = PXSessionList.Add(new PXSessionListItem()
    {
      ItemID = id,
      User = str
    });
    PXSessionStateStoreImpl provider = this.GetProvider();
    PXPerformanceInfo slot = PXContext.GetSlot<PXPerformanceInfo>();
    slot?.SessionSaveTimer.Start();
    HttpContext context1 = context;
    string id1 = id;
    PXSessionStateStoreData sessionStateStoreData = item;
    object lockId1 = lockId;
    int num = newItem ? 1 : 0;
    provider.SetAndReleaseItemExclusive(context1, id1, sessionStateStoreData, lockId1, num != 0);
    if (slot == null)
      return;
    pxSessionListItem.Size = slot.SessionSaveSize;
    slot.SessionSaveTimer.Stop();
  }

  private bool IgnoreUrl(HttpContext context)
  {
    return !string.IsNullOrEmpty(this._ignoreUrl) && context.Request.AppRelativeCurrentExecutionFilePath != null && this._ignoreUrl.ToLower().Contains(context.Request.AppRelativeCurrentExecutionFilePath.ToLower());
  }

  public override void SetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    SessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    this.SetAndReleaseItemExclusive(context, id, (PXSessionStateStoreData) item, lockId, newItem);
  }

  private void SetAndReleaseItemExclusive(
    HttpContext context,
    string id,
    PXSessionStateStoreData item,
    object lockId,
    bool newItem)
  {
    PXSessionStateStore.RemoveFromContext(context, item.PXSessionState);
    string id1 = PXSessionStateStore.alterId(context, id);
    this.ProviderSetAndReleaseItemExclusive(context, id1, item, lockId, newItem);
  }

  public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
  {
    return this._provider.SetItemExpireCallback(expireCallback);
  }

  public static string GetSessionID(HttpContext context)
  {
    return context == null || context.Session == null ? (string) null : PXSessionStateStore.alterId(context, context.Session.SessionID);
  }

  private static string alterId(HttpContext context, string id)
  {
    if (id.Contains("$(W(") || id.Contains("$W("))
      return id;
    string suffix = PXSessionStateStore.GetSuffix(context);
    return suffix != "" ? $"{id}${suffix}" : id;
  }

  public static string ResolvePopup(string suffix)
  {
    int result;
    if (string.IsNullOrEmpty(suffix) || !suffix.StartsWith("W(") || !suffix.EndsWith(")") || !int.TryParse(suffix.Substring(2, suffix.Length - 3), out result))
      return suffix;
    if (result > 10000)
      return $"W({(result - 10000).ToString()})";
    return result == 10000 ? "" : suffix;
  }

  private static string GetCompanyIDFromUri(HttpContext context)
  {
    string companyIdFromUri = context.Request.QueryString["CompanyID"];
    if (string.IsNullOrEmpty(companyIdFromUri) && context.Request.UrlReferrer != (Uri) null)
      companyIdFromUri = HttpUtility.ParseQueryString(context.Request.UrlReferrer.Query)["CompanyID"];
    return companyIdFromUri;
  }

  private string GetJustCreatedSuffix(HttpContext context, string id)
  {
    string suffix = PXSessionStateStore.GetSuffix(context);
    if (suffix == "")
      return (string) null;
    string justCreatedSuffix = (string) null;
    if (context != null)
    {
      string suffixFromReferrer = PXSessionStateStore.GetSuffixFromReferrer(context);
      if (suffix != suffixFromReferrer)
        justCreatedSuffix = suffixFromReferrer;
      string suffixFromCookie = PXSessionStateStore.GetSuffixFromCookie(context);
      if (suffix != suffixFromCookie && suffixFromCookie != null)
        justCreatedSuffix = suffixFromCookie;
    }
    return justCreatedSuffix;
  }

  public static string GetSuffix(HttpContext context)
  {
    return context == null ? string.Empty : PXSessionStateStore.GetSuffix(context.Request);
  }

  internal static string GetSuffix(HttpRequest request)
  {
    if (request == null)
      return string.Empty;
    string header = request.Headers["AspFilterSessionId"];
    if (header == null)
      return PXSessionStateStore.GetSuffixFromReferrer(request);
    return header.StartsWith("W(") ? header : string.Empty;
  }

  private static string GetSuffixFromReferrer(HttpContext context)
  {
    return PXSessionStateStore.GetSuffixFromReferrer(context.Request);
  }

  private static string GetSuffixFromReferrer(HttpRequest request)
  {
    return request.UrlReferrer == (Uri) null ? string.Empty : PXSessionStateStore.GetSuffixFromUrl(HttpUtility.UrlDecode(request.UrlReferrer.OriginalString));
  }

  private static string GetSuffixFromCookie(HttpContext context)
  {
    try
    {
      HttpCookie cookie = context.Request.Cookies["prevwin"];
      if (cookie != null)
      {
        if (!string.IsNullOrWhiteSpace(cookie.Value))
        {
          if (int.TryParse(cookie.Value, out int _))
            return cookie.Value == "0" ? "" : $"W({cookie.Value})";
        }
      }
    }
    catch
    {
    }
    return (string) null;
  }

  private static void ResetSuffixCookie(HttpContext context)
  {
    HttpCookie cookie = context.Request.Cookies["prevwin"];
    if (cookie == null)
      return;
    cookie.Value = (string) null;
    context.Response.Cookies.Set(cookie);
  }

  public static string GetSuffix(HttpContextBase context)
  {
    if (context == null)
      return string.Empty;
    string header = context.Request.Headers["AspFilterSessionId"];
    if (header == null)
      return PXSessionStateStore.GetSuffixFromReferrer(context);
    return header.StartsWith("W(") ? header : string.Empty;
  }

  private static string GetSuffixFromReferrer(HttpContextBase context)
  {
    return context.Request.UrlReferrer == (Uri) null ? string.Empty : PXSessionStateStore.GetSuffixFromUrl(HttpUtility.UrlDecode(context.Request.UrlReferrer.OriginalString));
  }

  [PXInternalUseOnly]
  public static string GetSuffixFromUrl(string url)
  {
    string s = ((IEnumerable<string>) url.Split('/')).FirstOrDefault<string>((Func<string, bool>) (str => str.StartsWith("(W(") && str.EndsWith("))"))).RemoveFromEnd("))").RemoveFromStart("(W(") ?? string.Empty;
    if (s == string.Empty)
      return string.Empty;
    int result;
    if (int.TryParse(s, out result))
      return $"W({result})";
    throw new PXException("Invalid session key");
  }

  public static string GetSessionUrl(HttpContext context)
  {
    string absoluteUri = context.Request.Url.AbsoluteUri;
    return PXSessionStateStore.GetSessionUrl(context, absoluteUri);
  }

  public static string GetSessionUrl(HttpContext context, string href)
  {
    return PXSessionStateStore.GetSessionUrl(context, href, PXSessionStateStore.GetSuffix(context));
  }

  public static string GetSessionUrl(HttpContext context, string href, string suffix)
  {
    if (string.IsNullOrEmpty(href) || string.IsNullOrEmpty(suffix))
      return href;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    suffix = new string(((IEnumerable<char>) suffix.ToCharArray()).Where<char>(PXSessionStateStore.\u003C\u003EO.\u003C0\u003E__IsDigit ?? (PXSessionStateStore.\u003C\u003EO.\u003C0\u003E__IsDigit = new Func<char, bool>(char.IsDigit))).ToArray<char>());
    if (string.IsNullOrEmpty(suffix))
      return href;
    int num1 = int.Parse(suffix);
    int length = href.IndexOf("/(W(", StringComparison.OrdinalIgnoreCase);
    if (length != -1)
      return $"{href.Substring(0, length)}/(W({num1.ToString()})){href.Substring(href.IndexOf("/", length + 4, StringComparison.OrdinalIgnoreCase))}";
    href = PXSessionStateStore.CorrectAspxUrl(href);
    string applicationPath = context.Request.ApplicationPath;
    int num2 = href.IndexOf("//", StringComparison.OrdinalIgnoreCase);
    int num3 = num2 == -1 ? href.IndexOf(applicationPath, StringComparison.OrdinalIgnoreCase) : href.IndexOf(applicationPath, num2 + 2, StringComparison.OrdinalIgnoreCase);
    if (num3 == -1)
      return href;
    if (applicationPath.Length > 1 || applicationPath[0] != '/')
      num3 += applicationPath.Length;
    return $"{href.Substring(0, num3)}/(W({num1.ToString()})){href.Substring(num3)}";
  }

  private static string CorrectAspxUrl(string url)
  {
    int num1 = url.IndexOf("aspx");
    int num2 = url.IndexOf("?");
    if (num1 > 0 && num2 < 0)
      url = url.Substring(0, num1 + 4);
    return url;
  }

  public static bool IsPrimitive(object v)
  {
    switch (v)
    {
      case null:
        return true;
      case string _:
        return true;
      case ValueType _:
        return true;
      default:
        return false;
    }
  }

  internal static SessionStateStoreData GetSessionItemReadonlyNotLocked(string id, out bool locked)
  {
    if (Str.IsNullOrEmpty(id))
      throw new PXException("ProcessWithSessionContext failed.");
    HttpContext context = new HttpContext(new HttpRequest("", "http://localhost/", ""), new HttpResponse((TextWriter) new StringWriter(new StringBuilder())));
    HttpContext current = HttpContext.Current;
    TimeSpan lockAge;
    object lockId;
    SessionStateActions actionFlags;
    return current != null ? (SessionStateStoreData) PXSessionStateStore.GetStore(current).ProviderGetItem(context, id, out locked, out lockAge, out lockId, out actionFlags) : (SessionStateStoreData) PXSessionStateStore.Pool.GetItem().ProviderGetItem(context, id, out locked, out lockAge, out lockId, out actionFlags);
  }

  private void ProcessWithSessionContextImpl(
    string id,
    System.Action<PXSessionStateStoreData> processMethod)
  {
    HttpContext context = new HttpContext(new HttpRequest("", "http://localhost/", ""), new HttpResponse((TextWriter) new StringWriter(new StringBuilder())));
    object lockId;
    bool isNew;
    PXSessionStateStoreData sessionStateStoreData = this.LoadSessionItem(context, id, out lockId, out isNew);
    if (sessionStateStoreData == null)
      throw new PXException("Session object wait timeout: {0}", new object[1]
      {
        (object) id
      });
    try
    {
      processMethod(sessionStateStoreData);
      this.ProviderSetAndReleaseItemExclusive(context, id, sessionStateStoreData, lockId, isNew);
    }
    catch
    {
      this.GetProvider().ReleaseItemExclusive(context, id, lockId);
      throw;
    }
  }

  private PXSessionStateStoreData LoadSessionItem(
    HttpContext context,
    string id,
    out object lockId,
    out bool isNew)
  {
    lockId = (object) null;
    isNew = false;
    TimeSpan timeSpan = new TimeSpan(0, 1, 0);
    int num = 200;
    for (int index = 0; index < num; ++index)
    {
      bool locked;
      TimeSpan lockAge;
      PXSessionStateStoreData itemExclusive = this.ProviderGetItemExclusive(context, id, out locked, out lockAge, out lockId, out SessionStateActions _);
      PXSessionStateStoreImpl provider = this.GetProvider();
      if (locked && itemExclusive == null)
      {
        if (lockAge < timeSpan)
          Thread.Sleep(500);
        else
          provider.ReleaseItemExclusive(context, id, lockId);
      }
      else
      {
        if (itemExclusive != null)
          return itemExclusive;
        int sessionTimeout = this.GetSessionTimeout(id);
        provider.CreateUninitializedItem(context, id, sessionTimeout);
      }
    }
    throw new PXException("Session object wait timeout: {0}", new object[1]
    {
      (object) id
    });
  }

  private static void ProcessWithSessionContext(
    string id,
    System.Action<PXSessionStateStoreData> processMethod)
  {
    if (Str.IsNullOrEmpty(id))
      throw new PXException("ProcessWithSessionContext failed.");
    HttpContext current = HttpContext.Current;
    if (current != null)
    {
      PXSessionStateStore.GetStore(current).ProcessWithSessionContextImpl(id, processMethod);
    }
    else
    {
      PXSessionStateStore sessionStateStore = PXSessionStateStore.Pool.GetItem();
      sessionStateStore.ProcessWithSessionContextImpl(id, processMethod);
      PXSessionStateStore.Pool.ReleaseItem(sessionStateStore);
    }
  }

  internal static Hashtable LoadSharedSession()
  {
    PXSessionStateStore.SharedSessionInfo sharedSessionInfo = (PXSessionStateStore.SharedSessionInfo) HttpContext.Current.Items[(object) "SharedSessionInfo"];
    if (sharedSessionInfo == null)
    {
      sharedSessionInfo = new PXSessionStateStore.SharedSessionInfo()
      {
        sharedSessionId = "Shared" + HttpContext.Current.Session.SessionID
      };
      object lockId;
      bool isNew;
      sharedSessionInfo.result = PXSessionStateStore.GetStore(HttpContext.Current).LoadSessionItem(HttpContext.Current, sharedSessionInfo.sharedSessionId, out lockId, out isNew);
      sharedSessionInfo.lockId = lockId;
      sharedSessionInfo.isNew = isNew;
      HttpContext.Current.Items[(object) "SharedSessionInfo"] = (object) sharedSessionInfo;
    }
    Hashtable hashtable = (Hashtable) sharedSessionInfo.result.Items["PXSharedUserSession"];
    if (hashtable == null)
    {
      hashtable = new Hashtable();
      sharedSessionInfo.result.Items["PXSharedUserSession"] = (object) hashtable;
    }
    return hashtable;
  }

  internal static void CommitSession(HttpContext context)
  {
    PXSessionStateStoreData sessionStateStoreData = (PXSessionStateStoreData) context.Items[(object) "SessionObject"];
    string id = (string) context.Items[(object) "_rqSID"];
    object lockId = context.Items[(object) "_rqLockID"];
    if (sessionStateStoreData == null || string.IsNullOrEmpty(id))
      return;
    PXSessionStateStore.GetStore(context).ProviderSetAndReleaseItemExclusive(context, id, sessionStateStoreData, lockId, false);
    context.Items[(object) "_rqCommited"] = (object) true;
  }

  internal static void CommitSharedSession(HttpContext context)
  {
    PXSessionStateStore.SharedSessionInfo sharedSessionInfo = (PXSessionStateStore.SharedSessionInfo) HttpContext.Current.Items[(object) "SharedSessionInfo"];
    if (sharedSessionInfo == null || sharedSessionInfo.sharedSessionId == null)
      return;
    HttpContext.Current.Items[(object) "SharedSessionInfo"] = (object) null;
    PXSessionStateStore.GetStore(HttpContext.Current).ProviderSetAndReleaseItemExclusive(context, sharedSessionInfo.sharedSessionId, sharedSessionInfo.result, sharedSessionInfo.lockId, sharedSessionInfo.isNew);
  }

  internal static void DisableSharedSession(HttpContext context)
  {
    context.Items[(object) "SharedSessionInfo"] = (object) new PXSessionStateStore.SharedSessionInfo()
    {
      result = new PXSessionStateStoreData(new SessionStateStoreData((ISessionStateItemCollection) new SessionStateItemCollection(), (HttpStaticObjectsCollection) null, 0))
    };
  }

  internal static bool CheckExpiration(HttpContext httpContext, string sessionId)
  {
    IPXSessionState session = HttpContextPXSessionState.Get(httpContext) ?? throw new InvalidOperationException("Cannot be called in a sessionless context");
    if (!session.Contains<bool>(PXSessionStateStore.Keys.FailedToRegenerate))
      return !DataSourceSession.IsValid(session, sessionId);
    session.Remove<bool>(PXSessionStateStore.Keys.FailedToRegenerate);
    return true;
  }

  internal static MemProfilerResult[] GetSessionsMemoryInfo()
  {
    IEnumerable<MemProfilerResult> source1 = Enumerable.Empty<MemProfilerResult>();
    try
    {
      List<PXSessionListItem> list;
      lock (PXSessionList.Items)
        list = PXSessionList.Items.Values.ToList<PXSessionListItem>();
      PXSessionStateStore provider = PXSessionStateStore.ProviderToAppinstanceMap.Values.FirstOrDefault<PXSessionStateStore>();
      source1 = list.Select<PXSessionListItem, MemProfilerResult>((Func<PXSessionListItem, MemProfilerResult>) (item =>
      {
        HttpContext context = new HttpContext((HttpWorkerRequest) new SimpleWorkerRequest("dummy", (string) null, (TextWriter) null));
        bool locked;
        object lockId;
        SessionStateStoreData sessionItem = provider.GetItemExclusive(context, item.ItemID, out locked, out TimeSpan _, out lockId, out SessionStateActions _);
        try
        {
          SessionStateStoreData sessionStateStoreData = sessionItem;
          object[] objArray;
          if (sessionStateStoreData == null)
          {
            objArray = (object[]) null;
          }
          else
          {
            ISessionStateItemCollection items = sessionStateStoreData.Items;
            if (items == null)
            {
              objArray = (object[]) null;
            }
            else
            {
              NameObjectCollectionBase.KeysCollection keys = items.Keys;
              if (keys == null)
              {
                objArray = (object[]) null;
              }
              else
              {
                string[] array = keys.ToArray<string>();
                if (array == null)
                {
                  objArray = (object[]) null;
                }
                else
                {
                  IEnumerable<object> source2 = ((IEnumerable<string>) array).Select<string, object>((Func<string, object>) (_ => sessionItem.Items[_]));
                  objArray = source2 != null ? source2.ToArray<object>() : (object[]) null;
                }
              }
            }
          }
          MemProfilerResult size = PXReflectionSerializer.GetSize((object) objArray);
          size.ID = item.ItemID;
          size.User = item.User;
          size.DateCreated = new System.DateTime?(item.DateCreated);
          return size;
        }
        finally
        {
          if (locked && sessionItem != null)
            provider.ReleaseItemExclusive(context, item.ItemID, lockId);
        }
      }));
    }
    catch
    {
    }
    return source1.ToArray<MemProfilerResult>();
  }

  internal static bool TryClearSession(string sessionId)
  {
    if (string.IsNullOrEmpty(sessionId))
      return false;
    try
    {
      PXSessionStateStore sessionStateStore = PXSessionStateStore.ProviderToAppinstanceMap.Values.FirstOrDefault<PXSessionStateStore>();
      HttpContext context = new HttpContext((HttpWorkerRequest) new SimpleWorkerRequest("dummy", (string) null, (TextWriter) null));
      object lockId = (object) null;
      PXSessionStateStoreData itemExclusive = (PXSessionStateStoreData) sessionStateStore?.GetItemExclusive(context, sessionId, out bool _, out TimeSpan _, out lockId, out SessionStateActions _);
      if (itemExclusive == null)
        return false;
      itemExclusive.Items.Clear();
      itemExclusive.PXSessionState.Set<bool>(PXSessionStateStore.Keys.SessionWasCleared, true);
      sessionStateStore.SetAndReleaseItemExclusive(context, sessionId, itemExclusive, lockId, false);
      return true;
    }
    catch
    {
    }
    return false;
  }

  internal static bool WasSessionCleared(HttpContext httpContext)
  {
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(httpContext);
    return pxSessionState != null && pxSessionState.Contains<bool>(PXSessionStateStore.Keys.SessionWasCleared);
  }

  internal static void UnmarkSessionCleared(HttpContext httpContext)
  {
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(httpContext);
    if (pxSessionState == null)
      return;
    pxSessionState.Remove<bool>(PXSessionStateStore.Keys.SessionWasCleared);
  }

  internal static class Headers
  {
    internal const string ASP_FILTER_SESSION_ID = "AspFilterSessionId";
  }

  private static class ContextItems
  {
    internal const string SESSION_OBJECT = "SessionObject";
    internal const string INTERNAL_SESSION_ID = "_rqSID";
    internal const string INTERNAL_LOCK_ID = "_rqLockID";
    internal const string INTERNAL_COMMITED = "_rqCommited";
  }

  private static class Cookies
  {
    internal const string PREVWIN = "prevwin";
  }

  private static class Keys
  {
    internal static readonly SessionKey<bool> SessionWasCleared = new SessionKey<bool>("WasCleared");
    internal static readonly SessionKey<bool> FailedToRegenerate = new SessionKey<bool>(nameof (FailedToRegenerate));
    internal static readonly SessionKey<string> JustCreated = new SessionKey<string>("justCreated");
    internal static readonly SessionKey<bool> AlreadyCopied = new SessionKey<bool>("alreadyCopied");
    internal static readonly SessionKey<int> CodeVersion = new SessionKey<int>(nameof (CodeVersion));
  }

  private delegate void SetAppPathModifierDelegate(HttpResponse response, string appPathModifier);

  private class PXSessionStateStorePool
  {
    public const string ProviderName = "PoolProvider";
    private readonly ConcurrentBag<PXSessionStateStore> Items = new ConcurrentBag<PXSessionStateStore>();
    public NameValueCollection Config;

    public PXSessionStateStore GetItem()
    {
      PXSessionStateStore result;
      if (this.Items.TryTake(out result))
        return result;
      PXSessionStateStore sessionStateStore = new PXSessionStateStore();
      sessionStateStore.Initialize("PoolProvider", this.Config);
      return sessionStateStore;
    }

    public void ReleaseItem(PXSessionStateStore item)
    {
      if (this.Items.Count >= 20)
        return;
      this.Items.Add(item);
    }
  }

  private class SharedSessionInfo
  {
    public string sharedSessionId;
    public PXSessionStateStoreData result;
    public object lockId;
    public bool isNew;
  }
}
