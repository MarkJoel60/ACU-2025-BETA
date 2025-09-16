// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.PXScreenProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using PX.Data.DependencyInjection;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

#nullable disable
namespace PX.Data.Process;

public class PXScreenProcessor
{
  private static object _etwLock = new object();

  public static void Process(string uri, IPrincipal user, AsyncCallback cb)
  {
    PXScreenProcessor.Process(uri, user, cb, (System.Action<HttpContext>) null, (System.Action<HttpContext>) null);
  }

  [PXInternalUseOnly]
  public static void Process(
    string uri,
    IPrincipal user,
    AsyncCallback cb,
    System.Action<HttpContext> initContext,
    System.Action<HttpContext> preExecute)
  {
    HttpContext current = HttpContext.Current;
    PXPerformanceInfo slot = PXContext.GetSlot<PXPerformanceInfo>();
    IDictionary slots = PXAccess.IsMultiDbMode ? PXContextCopyingRequiredAttribute.Capture((Func<string, object, object>) ((key, value) => !PXLongOperation.IsLongRunOperation ? (object) key : (object) value.GetType())) : (IDictionary) new Dictionary<object, object>();
    DummyRequest dummyRequest = new DummyRequest(uri);
    HttpContext httpContext = (HttpContext) typeof (HttpContext).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[2]
    {
      typeof (HttpWorkerRequest),
      typeof (bool)
    }, (ParameterModifier[]) null).Invoke(new object[2]
    {
      (object) dummyRequest,
      (object) false
    });
    httpContext.User = user;
    HttpContext.Current = httpContext;
    ISlotStorageProvider slotStorage = HttpContextSlotStorageProvider.Get(httpContext);
    PXContext.PXIdentity.SetUser(user);
    PXContextCopyingRequiredAttribute.SetToStorage(slots, slotStorage);
    TypeKeyedOperationExtensions.Set<PXPerformanceInfo>(httpContext.Slots(), slot);
    if (initContext != null)
      initContext(httpContext);
    IHttpAsyncHandler httpAsyncHandler = (IHttpAsyncHandler) PXBuildManager.GetType("System.Web.HttpApplicationFactory", true).GetMethod("GetApplicationInstance", BindingFlags.Static | BindingFlags.NonPublic).Invoke((object) null, new object[1]
    {
      (object) httpContext
    });
    typeof (HttpContext).GetProperty("AsyncAppHandler", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) httpContext, (object) httpAsyncHandler, new object[0]);
    httpContext.ApplicationInstance = (HttpApplication) httpAsyncHandler;
    typeof (HttpResponse).GetMethod("InitResponseWriter", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((object) httpContext.Response, new object[0]);
    PropertyInfo property = typeof (HttpRuntime).GetProperty("UseIntegratedPipeline", BindingFlags.Static | BindingFlags.NonPublic);
    if (property != (PropertyInfo) null)
    {
      if ((bool) property.GetValue((object) null, new object[0]))
      {
        try
        {
          PXSessionStateStore.DisableSharedSession(httpContext);
        }
        catch
        {
        }
        PXSessionStateStore.AddHttpSessionStateToContext(httpContext, httpContext.CreateSessionId(), 10);
        uri = uri[0] == '/' ? "~" + uri : "~/" + uri;
        int length1;
        if ((length1 = uri.IndexOf('#')) != -1 && length1 + 1 < uri.Length)
          uri = uri.Substring(0, length1);
        int length2;
        if ((length2 = uri.IndexOf('?')) != -1 && length2 + 1 < uri.Length)
          uri = uri.Substring(0, length2);
        if (uri != null)
        {
          int num1;
          if ((num1 = uri.IndexOf("/Pages/", StringComparison.InvariantCultureIgnoreCase)) != -1)
          {
            try
            {
              string str = uri.Substring(num1 + 7);
              int num2 = str.LastIndexOf('/');
              if (num2 > 0)
                str = str.Substring(0, num2).Replace('/', '_') + str.Substring(num2);
              string virtualPath = $"{uri.Substring(0, num1 + 1)}CstPublished/pages_{str}";
              httpContext.Handler = (IHttpHandler) BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof (Page));
            }
            catch
            {
            }
          }
        }
        try
        {
          using (httpContext.Slots().BeginLifetimeScope())
          {
            if (httpContext.Handler == null)
            {
              PXScreenProcessor.HackEtwTrace(false);
              httpContext.Handler = (IHttpHandler) BuildManager.CreateInstanceFromVirtualPath(uri, typeof (Page));
            }
            if (preExecute != null)
              preExecute(httpContext);
            PXScreenProcessor.HackEtwTrace(false);
            PXSiteMap.Provider.SetCurrentNode(PXSiteMap.Provider.FindSiteMapNode(uri));
            httpContext.Server.Execute(httpContext.Handler, (TextWriter) new StringWriter(), true);
            ConstructorInfo constructor = PXBuildManager.GetType("System.Web.HttpAsyncResult", true).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new System.Type[2]
            {
              typeof (AsyncCallback),
              typeof (object)
            }, (ParameterModifier[]) null);
            cb((IAsyncResult) constructor.Invoke(new object[2]
            {
              (object) cb,
              (object) httpContext
            }));
            goto label_33;
          }
        }
        catch (HttpException ex)
        {
          throw PXException.ExtractInner((Exception) ex);
        }
        finally
        {
          PXSessionStateStore.DisposeCurrentStore();
          HttpContext.Current = current;
        }
      }
    }
    using (httpContext.Slots().BeginLifetimeScope())
      httpAsyncHandler.EndProcessRequest(httpAsyncHandler.BeginProcessRequest(httpContext, cb, (object) httpContext));
label_33:
    HttpContext.Current = current;
  }

  public static void HackEtwTrace(bool s)
  {
    System.Reflection.FieldInfo field = typeof (HttpWorkerRequest).Assembly.GetType("System.Web.EtwTrace", true).GetField("s_WrType", BindingFlags.Static | BindingFlags.NonPublic);
    if (field == (System.Reflection.FieldInfo) null)
      return;
    lock (PXScreenProcessor._etwLock)
    {
      object obj = Enum.Parse(typeof (HttpWorkerRequest).Assembly.GetType("System.Web.EtwWorkerRequestType", true), "Unknown");
      field.SetValue((object) null, obj);
    }
  }
}
