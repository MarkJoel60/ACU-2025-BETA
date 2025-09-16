// Decompiled with JetBrains decompiler
// Type: PX.Common.PXSessionContextFactory
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Context;
using PX.Common.Session;
using PX.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;

#nullable enable
namespace PX.Common;

[PXInternalUseOnly]
public sealed class PXSessionContextFactory : ISessionContextFactory
{
  private readonly 
  #nullable disable
  IRoleManagementService \u0002;
  private readonly ISessionContextFactoryAdapter \u000E;

  internal PXSessionContextFactory(
    IRoleManagementService _param1,
    ISessionContextFactoryAdapter _param2)
  {
    this.\u0002 = _param1;
    this.\u000E = _param2;
  }

  [Obsolete("Use PXContext.PXIdentity or PXSessionContextFactory.GetSessionContext")]
  public static PXSessionContext SessionContext
  {
    get => PXSessionContextFactory.GetSessionContext(SlotStore.Instance);
  }

  internal static PXSessionContext GetSessionContext(ISlotStore _param0)
  {
    PXSessionContext sessionContext1 = PXSessionContextFactory.\u0006.\u0002(_param0);
    if (sessionContext1 != null)
      return sessionContext1;
    PXSessionContextFactory.\u0002(_param0, nameof (GetSessionContext), "C:\\build\\code_repo\\NetTools\\PX.Common\\PXSessionContextFactory.cs", 59);
    HttpContext current = HttpContext.Current;
    if (current == null)
    {
      PXSessionContext sessionContext2 = PXSessionContextFactory.\u0002((HttpCookieCollection) null);
      PXSessionContextFactory.\u0006.\u0002(_param0, sessionContext2);
      return sessionContext2;
    }
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(current);
    if (pxSessionState != null)
    {
      PXSessionContext sessionContext3 = PXSessionContextFactory.\u0008.\u0002(pxSessionState);
      if (sessionContext3 != null && !sessionContext3.Invalidated)
      {
        PXSessionContextFactory.\u0006.\u0002(_param0, sessionContext3);
        return sessionContext3;
      }
      PXSessionContext sessionContext4 = PXSessionContextFactory.\u0003.\u0002(pxSessionState);
      if (sessionContext4 != null)
      {
        PXSessionContextFactory.\u0003.\u0002(pxSessionState);
        PXSessionContextFactory.\u0008.\u0002(pxSessionState, sessionContext4);
        PXSessionContextFactory.\u0006.\u0002(_param0, sessionContext4);
        return sessionContext4;
      }
    }
    HttpCookieCollection cookies;
    try
    {
      cookies = current.Request.Cookies;
    }
    catch
    {
      return PXSessionContextFactory.\u0002((HttpCookieCollection) null);
    }
    PXSessionContext sessionContext5 = PXSessionContextFactory.\u0002(cookies);
    PXSessionContextFactory.\u0006.\u0002(_param0, sessionContext5);
    return sessionContext5;
  }

  internal static void InitializeContext(ISlotStore _param0, PXSessionContext _param1)
  {
    if (_param1 == null)
      throw new ArgumentNullException("value");
    if (PXSessionContextFactory.\u0006.\u0002(_param0) != null)
      throw new InvalidOperationException("PXSessionContext already initialized");
    PXSessionContextFactory.\u0006.\u0002(_param0, _param1);
  }

  private static PXSessionContext \u0002(HttpCookieCollection _param0)
  {
    int num = _param0 == null ? 1 : 0;
    int? nullable = num != 0 ? new int?() : PXSessionContextFactory.\u0002(_param0);
    PXTimeZoneInfo pxTimeZoneInfo = num != 0 ? (PXTimeZoneInfo) null : PXSessionContextFactory.\u0002(_param0);
    CultureInfo cultureInfo = (num != 0 ? (CultureInfo) null : PXSessionContextFactory.\u0002(_param0)) ?? new CultureInfo("en-US");
    return new PXSessionContext()
    {
      Culture = cultureInfo,
      UICulture = cultureInfo,
      BusinessDate = new DateTime?(),
      TimeZone = pxTimeZoneInfo,
      BranchID = nullable
    };
  }

  private static CultureInfo \u0002(HttpCookieCollection _param0)
  {
    HttpCookie httpCookie = _param0["Locale"];
    if (httpCookie == null)
      return (CultureInfo) null;
    string name = httpCookie["Culture"];
    return string.IsNullOrEmpty(name) ? (CultureInfo) null : new CultureInfo(name);
  }

  private static PXTimeZoneInfo \u0002(HttpCookieCollection _param0)
  {
    HttpCookie httpCookie = _param0["Locale"];
    if (httpCookie == null)
      return (PXTimeZoneInfo) null;
    string id = httpCookie["TimeZone"];
    return string.IsNullOrEmpty(id) ? (PXTimeZoneInfo) null : PXTimeZoneInfo.FindSystemTimeZoneById(id);
  }

  private static int? \u0002(HttpCookieCollection _param0)
  {
    HttpCookie httpCookie = _param0["UserBranch"];
    if (httpCookie == null)
      return new int?();
    string s = httpCookie.Value;
    if (string.IsNullOrEmpty(s))
      return new int?();
    int result;
    return !int.TryParse(s, out result) ? new int?() : new int?(result);
  }

  void ISessionContextFactory.h33fcuklkkzsqxs9su3w8qqx4agmk333\u2009\u2009\u2009\u0002(
    HttpContextBase _param1)
  {
    ISlotStore islotStore = _param1.Slots();
    PXSessionContextFactory.\u0002(islotStore, "BeginRequest", "C:\\build\\code_repo\\NetTools\\PX.Common\\PXSessionContextFactory.cs", 172);
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(_param1);
    PXSessionContext pxSessionContext1 = PXSessionContextFactory.\u0008.\u0002(pxSessionState);
    if (pxSessionContext1 != null)
    {
      int num = PXSessionContextFactory.\u0002(_param1) ? 1 : 0;
      pxSessionContext1.SetUser((IPrincipal) null);
      PXSessionContextFactory.\u0006.\u0002(islotStore, pxSessionContext1);
      if (num == 0 || string.IsNullOrEmpty(pxSessionContext1.UserName))
        return;
      string name = PXSessionContextFactory.\u0002(pxSessionContext1.UserName, Thread.CurrentPrincipal ?? _param1.User);
      islotStore.ClearSingleCompanyId();
      pxSessionContext1.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(name), Array.Empty<string>()));
      PXSessionContextFactory.\u0006.\u0002(islotStore, pxSessionContext1);
      string[] rolesForUser = this.\u0002.GetRolesForUser(pxSessionContext1.UserName);
      pxSessionContext1.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(name), rolesForUser));
      PXSessionContextFactory.\u0006.\u0002(islotStore, pxSessionContext1);
      PXSessionContextFactory.\u0006.\u0002(islotStore, pxSessionContext1);
    }
    else
    {
      PXSessionContext pxSessionContext2 = PXSessionContextFactory.\u0006.\u0002(islotStore);
      if (pxSessionContext2 == null || !PXSessionContextFactory.\u0002(_param1))
        return;
      pxSessionContext2.MarkSaved();
      pxSessionContext2.SetUser((IPrincipal) null);
      PXSessionContextFactory.\u0006.\u0002(islotStore, pxSessionContext2);
      PXSessionContextFactory.\u0008.\u0002(pxSessionState, pxSessionContext2);
    }
  }

  private static bool \u0002(HttpContextBase _param0)
  {
    return !Anonymous.IsAnonymous(Thread.CurrentPrincipal) || !Anonymous.IsAnonymous(_param0.User);
  }

  void ISessionContextFactory.h33fcuklkkzsqxs9su3w8qqx4agmk333\u2009\u2009\u2009\u000E(
    HttpContextBase _param1)
  {
    ISlotStore islotStore = _param1.Slots();
    PXSessionContext pxSessionContext1 = PXSessionContextFactory.\u0006.\u0002(islotStore);
    if (pxSessionContext1 == null)
      return;
    PXSessionContextFactory.\u0002(islotStore, "SaveRequestToSession", "C:\\build\\code_repo\\NetTools\\PX.Common\\PXSessionContextFactory.cs", 241);
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(_param1);
    if ((!PXSessionContextFactory.\u0002(_param1) || !pxSessionContext1.WasSaved && PXSessionContextFactory.\u0008.\u0002(pxSessionState) != null) && !pxSessionContext1.Requested)
      return;
    if (pxSessionContext1.Requested && pxSessionContext1.WasSaved && !pxSessionContext1.Invalidated)
    {
      pxSessionContext1.MarkSaved();
      pxSessionContext1.Requested = false;
      pxSessionContext1.Invalidated = false;
      PXSessionContext pxSessionContext2 = pxSessionContext1.Clone();
      pxSessionContext2.UserName = pxSessionContext2.User.Identity.Name;
      PXSessionContextFactory.\u0003.\u0002(pxSessionState, pxSessionContext2);
    }
    else
    {
      pxSessionContext1.MarkSaved();
      if (pxSessionContext1.Requested)
        pxSessionContext1.Invalidated = false;
      pxSessionContext1.Requested = false;
      pxSessionContext1.UserName = pxSessionContext1.User.Identity.Name;
      PXSessionContextFactory.\u0008.\u0002(pxSessionState, pxSessionContext1);
    }
  }

  void ISessionContextFactory.h33fcuklkkzsqxs9su3w8qqx4agmk333\u2009\u2009\u2009\u0006(
    HttpContextBase _param1)
  {
    PXSessionContextFactory.\u0006.\u0002(_param1.Slots()).Requested = true;
  }

  void ISessionContextFactory.h33fcuklkkzsqxs9su3w8qqx4agmk333\u2009\u2009\u2009\u0008(
    HttpContextBase _param1)
  {
    ISlotStore islotStore = _param1.Slots();
    PXSessionContext pxSessionContext = PXSessionContextFactory.\u0006.\u0002(islotStore);
    if (pxSessionContext != null)
      pxSessionContext.Invalidated = true;
    PXSessionContextFactory.\u0002(islotStore, "Invalidate", "C:\\build\\code_repo\\NetTools\\PX.Common\\PXSessionContextFactory.cs", 282);
    PXSessionContextFactory.\u0003.\u0002(HttpContextPXSessionState.Get(_param1));
  }

  void ISessionContextFactory.h33fcuklkkzsqxs9su3w8qqx4agmk333\u2009\u2009\u2009\u0003(
    HttpContextBase _param1)
  {
    PXSessionContextFactory.\u000E obj1 = new PXSessionContextFactory.\u000E();
    obj1.\u0002 = HttpContextPXSessionState.Get(_param1);
    if (obj1.\u0002 == null)
      return;
    PXSessionContextFactory.\u0002(_param1.Slots(), "Abandon", "C:\\build\\code_repo\\NetTools\\PX.Common\\PXSessionContextFactory.cs", 292);
    PXSessionContext pxSessionContext = PXSessionContextFactory.\u0008.\u0002(obj1.\u0002);
    Dictionary<string, object> dictionary = ((IEnumerable<string>) new string[2]
    {
      "LastUrl",
      "Lists$LastEntryScreen_ScreenID"
    }).ToDictionary<string, string, object>(PXSessionContextFactory.\u0002.\u000E ?? (PXSessionContextFactory.\u0002.\u000E = new Func<string, string>(PXSessionContextFactory.\u0002.\u0002.\u0002)), new Func<string, object>(obj1.\u0002));
    obj1.\u0002.RemoveAll(PXSessionContextFactory.\u0002.\u0006 ?? (PXSessionContextFactory.\u0002.\u0006 = new Func<string, bool>(PXSessionContextFactory.\u0002.\u0002.\u0002)));
    this.\u000E.ClearSession(_param1);
    foreach (KeyValuePair<string, object> keyValuePair in dictionary)
    {
      string str1;
      object obj2;
      EnumerableExtensions.Deconstruct<string, object>(keyValuePair, ref str1, ref obj2);
      string str2 = str1;
      object obj3 = obj2;
      obj1.\u0002.Set(str2, obj3);
    }
    PXSessionContextFactory.\u0008.\u0002(obj1.\u0002, pxSessionContext);
  }

  private static string \u0002(string _param0, IPrincipal _param1)
  {
    string str1 = _param0;
    if (_param1.Identity.AuthenticationType == "Federated")
    {
      Claim claim = ((ClaimsPrincipal) _param1).Claims.FirstOrDefault<Claim>(PXSessionContextFactory.\u0002.\u0008 ?? (PXSessionContextFactory.\u0002.\u0008 = new Func<Claim, bool>(PXSessionContextFactory.\u0002.\u0002.\u0002)));
      if (claim != null)
      {
        string str2 = "@" + claim.Value;
        if (!str1.EndsWith(str2))
          str1 += str2;
      }
    }
    return str1;
  }

  internal static void TempSessionBucketToSessionBucket(HttpContextBase _param0)
  {
    PXSessionContextFactory.\u0002(_param0.Slots(), nameof (TempSessionBucketToSessionBucket), "C:\\build\\code_repo\\NetTools\\PX.Common\\PXSessionContextFactory.cs", 330);
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(_param0);
    PXSessionContextFactory.\u0008.\u0002(pxSessionState, PXSessionContextFactory.\u0003.\u0002(pxSessionState));
  }

  internal static PXSessionContext GetSessionBucket(IPXSessionState _param0)
  {
    return PXSessionContextFactory.\u0008.\u0002(_param0);
  }

  internal static void SetSessionBucket(IPXSessionState _param0, PXSessionContext _param1)
  {
    PXSessionContextFactory.\u0008.\u0002(_param0, _param1);
  }

  internal static PXSessionContext GetTempSessionBucket(HttpContextBase _param0)
  {
    PXSessionContextFactory.\u0002(_param0.Slots(), nameof (GetTempSessionBucket), "C:\\build\\code_repo\\NetTools\\PX.Common\\PXSessionContextFactory.cs", 381);
    return PXSessionContextFactory.\u0003.\u0002(HttpContextPXSessionState.Get(_param0));
  }

  internal static PXSessionContext GetTempSessionBucket(IPXSessionState _param0)
  {
    return PXSessionContextFactory.\u0003.\u0002(_param0);
  }

  internal static void RemoveTempSessionBucket(IPXSessionState _param0)
  {
    PXSessionContextFactory.\u0003.\u0002(_param0);
  }

  private static void \u0002(ISlotStore _param0, [CallerMemberName] string _param1, [CallerFilePath] string _param2, [CallerLineNumber] int _param3)
  {
    if (_param0.GetSessionStandIn() != null)
      throw new InvalidOperationException($"{_param1} ({_param2}:{_param3}) cannot be invoked while session stand-in is enabled");
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly PXSessionContextFactory.\u0002 \u0002 = new PXSessionContextFactory.\u0002();
    public static Func<string, string> \u000E;
    public static Func<
    #nullable enable
    string, bool> \u0006;
    public static 
    #nullable disable
    Func<Claim, bool> \u0008;

    internal string \u0002(string _param1) => _param1;

    internal bool \u0002(
    #nullable enable
    string _param1) => true;

    internal bool \u0002(
    #nullable disable
    Claim _param1) => _param1.Type == "company";
  }

  private static class \u0003
  {
    private static readonly SessionKey<PXSessionContext> \u0002 = new SessionKey<PXSessionContext>("SESSION_CONTEXT_KEY_TEMP");

    internal static void \u0002(IPXSessionState _param0, PXSessionContext _param1)
    {
      if (_param0 == null)
        return;
      _param0.Set<PXSessionContext>(PXSessionContextFactory.\u0003.\u0002, _param1);
    }

    internal static PXSessionContext \u0002(IPXSessionState _param0)
    {
      return _param0 == null ? (PXSessionContext) null : _param0.Get<PXSessionContext>(PXSessionContextFactory.\u0003.\u0002);
    }

    internal static void \u0002(IPXSessionState _param0)
    {
      if (_param0 == null)
        return;
      _param0.Remove<PXSessionContext>(PXSessionContextFactory.\u0003.\u0002);
    }
  }

  private static class \u0006
  {
    internal static PXSessionContext \u0002(ISlotStore _param0)
    {
      return _param0.Get<PXSessionContext>("SESSION_CONTEXT_KEY");
    }

    internal static void \u0002(ISlotStore _param0, PXSessionContext _param1)
    {
      _param0.Set("SESSION_CONTEXT_KEY", (object) _param1);
    }
  }

  private static class \u0008
  {
    private static readonly SessionKey<PXSessionContext> \u0002 = new SessionKey<PXSessionContext>("SESSION_CONTEXT_KEY");

    internal static PXSessionContext \u0002(IPXSessionState _param0)
    {
      if (_param0 == null)
        return (PXSessionContext) null;
      return _param0.Get<PXSessionContext>(PXSessionContextFactory.\u0008.\u0002)?.Clone();
    }

    internal static void \u0002(IPXSessionState _param0, PXSessionContext _param1)
    {
      if (_param0 == null)
        return;
      _param0.Set<PXSessionContext>(PXSessionContextFactory.\u0008.\u0002, _param1);
    }
  }

  private sealed class \u000E
  {
    public IPXSessionState \u0002;

    internal object \u0002(string _param1) => this.\u0002.Get(_param1);
  }
}
