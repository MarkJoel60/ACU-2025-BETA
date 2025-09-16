// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.DiscoveryHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;

#nullable disable
namespace PX.Api.Soap;

internal class DiscoveryHandler : IHttpHandler
{
  public IHttpHandler Base;

  public bool IsReusable => false;

  public void ProcessRequest(HttpContext context)
  {
    string s = string.Format("<discovery xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schemas.xmlsoap.org/disco/\">\r\n  <contractRef ref=\"{0}?wsdl\" docRef=\"{0}\" xmlns=\"http://schemas.xmlsoap.org/disco/scl/\" />\r\n  <soap address=\"{0}\" xmlns:q1=\"http://www.acumatica.com/generic/\" binding=\"q1:ScreenSoap\" xmlns=\"http://schemas.xmlsoap.org/disco/soap/\" />\r\n  <soap address=\"{0}\" xmlns:q2=\"http://www.acumatica.com/generic/\" binding=\"q2:ScreenSoap12\" xmlns=\"http://schemas.xmlsoap.org/disco/soap/\" />\r\n</discovery>", (object) context.Request.GetExternalUrl().GetLeftPart(UriPartial.Path));
    context.Response.ContentType = "text/xml";
    context.Response.Write(s);
  }
}
