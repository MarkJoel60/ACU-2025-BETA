// Decompiled with JetBrains decompiler
// Type: PX.Api.RequestExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Monads;
using System.Net.Http;
using System.Web.Http.Dependencies;

#nullable disable
namespace PX.Api;

public static class RequestExtensions
{
  [Obsolete("Try using DI instead")]
  public static T GetRequestScopedService<T>(this HttpRequestMessage request) where T : class
  {
    return request != null ? ArgumentCheck.CheckNull<T>(MaybeObjects.OfType<T>(ArgumentCheck.CheckNull<object>(ArgumentCheck.CheckNull<IDependencyScope>(HttpRequestMessageExtensions.GetDependencyScope(request), (Func<Exception>) (() => (Exception) new InvalidOperationException("No dependency scope"))).GetService(typeof (T)), (Func<Exception>) (() => (Exception) new InvalidOperationException("No service of the type " + typeof (T).FullName)))), (Func<Exception>) (() => (Exception) new InvalidOperationException("The service can't be cast to the type " + typeof (T).FullName))) : throw new ArgumentNullException(nameof (request));
  }
}
