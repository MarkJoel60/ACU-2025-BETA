// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.WebServiceHandlerFactoryExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection;
using System.Web;
using System.Web.Services.Protocols;

#nullable disable
namespace PX.Api.Soap;

internal static class WebServiceHandlerFactoryExtensions
{
  private static readonly WebServiceHandlerFactory Factory = new WebServiceHandlerFactory();
  private static readonly MethodInfo Method = typeof (WebServiceHandlerFactory).GetMethod("CoreGetHandler", BindingFlags.Instance | BindingFlags.NonPublic);

  internal static IHttpHandler GetAsmxHandler(this HttpContext context, Type webServiceType)
  {
    try
    {
      return (IHttpHandler) WebServiceHandlerFactoryExtensions.Method.Invoke((object) WebServiceHandlerFactoryExtensions.Factory, new object[4]
      {
        (object) webServiceType,
        (object) context,
        (object) context.Request,
        (object) context.Response
      });
    }
    catch
    {
      return (IHttpHandler) null;
    }
  }
}
