// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Handlers.FileOutlookGetterBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace PX.Objects.CR.Handlers;

[PXInternalUseOnly]
public abstract class FileOutlookGetterBase : IHttpHandler, IRequiresSessionState
{
  protected abstract string ResourceAddinManifest { get; }

  protected virtual string OutputFileName => "OutlookAddinManifest.xml";

  public bool IsReusable => true;

  protected virtual bool IsSecureConnection()
  {
    return HttpContext.Current.Request.IsSecureConnection || string.Equals(HttpContext.Current.Request.Headers["X-Forwarded-Proto"], "https", StringComparison.InvariantCultureIgnoreCase);
  }

  public void ProcessRequest(HttpContext context)
  {
    if (!this.IsSecureConnection())
      throw new PXSetPropertyException("The Outlook add-in works only if your Acumatica ERP instance is hosted over HTTPS.");
    if (!PXAccess.FeatureInstalled<FeaturesSet.outlookIntegration>())
      throw new PXSetPropertyException("The Outlook Integration feature is disabled or not licensed. Please review the feature check box on the Enable/Disable Features (CS100000) form.");
    context.Response.Clear();
    context.Response.Cache.SetCacheability(HttpCacheability.Private);
    context.Response.Cache.SetValidUntilExpires(true);
    context.Response.AddHeader("Connection", "Keep-Alive");
    context.Response.BufferOutput = false;
    foreach (string allKey in context.Request.Cookies.AllKeys)
      context.Response.Cookies.Add(context.Request.Cookies[allKey]);
    context.Response.AddHeader("content-type", "application/octet-stream");
    context.Response.AddHeader("Accept-Ranges", "bytes");
    byte[] bytes = Encoding.UTF8.GetBytes(this.AdjustManifest(this.ReadManifest(), context.Request));
    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + this.OutputFileName);
    context.Response.ContentType = "application/xml";
    context.Response.BinaryWrite(bytes);
  }

  protected virtual string GetSiteBaseUrl(HttpRequest request)
  {
    return $"{this.GetScheme(request)}://{this.GetHost(request)}/{this.GetPath(request)}";
  }

  protected virtual string GetHost(HttpRequest request)
  {
    return request.Headers["X-Host"] ?? request.Headers["X-Forwarded-Host"] ?? request.Headers["Host"];
  }

  protected virtual string GetScheme(HttpRequest request) => "https";

  protected virtual string GetPath(HttpRequest request)
  {
    string str = string.Join("", ((IEnumerable<string>) request.Url.Segments).Take<string>(request.Url.Segments.Length - 1)).Trim('/');
    return string.IsNullOrEmpty(str) ? string.Empty : str + "/";
  }

  protected virtual string ReadManifest()
  {
    using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.ResourceAddinManifest))
    {
      using (StreamReader streamReader = new StreamReader(manifestResourceStream))
        return streamReader.ReadToEnd();
    }
  }

  protected virtual string AdjustManifest(string manifest, HttpRequest request)
  {
    string siteBaseUrl = this.GetSiteBaseUrl(request);
    return manifest.Replace("{domain}", siteBaseUrl);
  }
}
