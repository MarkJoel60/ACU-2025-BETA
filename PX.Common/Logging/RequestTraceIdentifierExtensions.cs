// Decompiled with JetBrains decompiler
// Type: PX.Logging.RequestTraceIdentifierExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.Extensions.DependencyInjection;
using PX.Common;
using System;
using System.Collections;
using System.Web;

#nullable disable
namespace PX.Logging;

[PXInternalUseOnly]
public static class RequestTraceIdentifierExtensions
{
  private static readonly Guid \u0002 = Guid.Empty;
  [PXInternalUseOnly]
  public static readonly string EmptyRequestTraceIdentifierString = RequestTraceIdentifierExtensions.\u0002.ToString();

  [PXInternalUseOnly]
  public static string GetRequestTraceIdentifier(IDictionary items)
  {
    object obj = items != null ? items[(object) "PX.Logging.RequestTraceIdentifier"] : throw new ArgumentNullException(nameof (items));
    if (obj is string requestTraceIdentifier)
      return requestTraceIdentifier;
    if (obj == null)
      return (string) null;
    throw new InvalidOperationException("Http context property PX.Logging.RequestTraceIdentifier is of unexpected type " + obj.GetType().FullName);
  }

  [PXInternalUseOnly]
  public static void SetRequestTraceIdentifier(IDictionary items, string requestTraceIdentifier)
  {
    IDictionary dictionary = items != null ? items : throw new ArgumentNullException(nameof (items));
    dictionary[(object) "PX.Logging.RequestTraceIdentifier"] = (object) (requestTraceIdentifier ?? throw new ArgumentNullException(nameof (requestTraceIdentifier)));
  }

  [PXInternalUseOnly]
  public static string EnsureRequestTraceIdentifier(this HttpContext httpContext)
  {
    if (httpContext == null)
      return (string) null;
    string requestTraceIdentifier = RequestTraceIdentifierExtensions.GetRequestTraceIdentifier(httpContext.Items);
    if (requestTraceIdentifier != null)
      return requestTraceIdentifier;
    string str = httpContext.\u0002().ToString();
    httpContext.Items[(object) "PX.Logging.RequestTraceIdentifier"] = (object) str;
    return str;
  }

  private static Guid \u0002(this HttpContext _param0)
  {
    HttpWorkerRequest service = ServiceProviderServiceExtensions.GetService<HttpWorkerRequest>((IServiceProvider) _param0);
    if (service == null)
      return Guid.NewGuid();
    Guid guid = service.RequestTraceIdentifier;
    if (guid == RequestTraceIdentifierExtensions.\u0002)
      guid = Guid.NewGuid();
    return guid;
  }
}
