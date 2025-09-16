// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.HandlerFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.Common;
using PX.Data;
using System;
using System.IO;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Api.Soap;

public class HandlerFactory : IHttpHandlerFactory
{
  public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string path)
  {
    if (string.IsNullOrEmpty(ApiConfiguration.SoapUrlMatcher.Match(url).Groups[1].Value))
      return (IHttpHandler) null;
    return !string.Equals(context.Request.QueryString.ToString(), "disco", StringComparison.OrdinalIgnoreCase) ? HandlerFactory.GetServiceHandler(context, requestType, url, path) : (IHttpHandler) new DiscoveryHandler();
  }

  public void ReleaseHandler(IHttpHandler handler)
  {
  }

  private static IHttpHandler GetServiceHandler(
    HttpContext context,
    string requestType,
    string url,
    string path)
  {
    string screenID1 = path.Substring(0, path.Length - 5);
    int num1 = screenID1.LastIndexOf('\\');
    if (num1 != -1)
      screenID1 = num1 + 1 >= screenID1.Length ? "" : screenID1.Substring(num1 + 1);
    if (!string.IsNullOrEmpty(screenID1))
      screenID1 = screenID1.ToUpper();
    if (context.Request.QueryString != null && context.Request.QueryString.Count > 0)
    {
      for (int index = 0; index < context.Request.QueryString.Count; ++index)
      {
        if (context.Request.QueryString.GetKey(index) != null && context.Request.QueryString.GetKey(index).StartsWith("wsdl", StringComparison.OrdinalIgnoreCase) || context.Request.QueryString.Get(index) != null && context.Request.QueryString.Get(index).StartsWith("wsdl", StringComparison.OrdinalIgnoreCase))
          return HandlerFactory.GetWsdlBuilder(screenID1);
      }
    }
    if (string.IsNullOrEmpty(screenID1))
      return context.GetAsmxHandler(HandlerFactory.GetScreenUntypedType());
    PXLoginScope pxLoginScope1;
    PXSiteMapNode screenIdUnsecure;
    using (pxLoginScope1 = ScreenUtils.EnsureLogin())
    {
      using (new PXImpersonationContext(pxLoginScope1 == null ? Thread.CurrentPrincipal.Identity.Name : pxLoginScope1.UserName + (string.IsNullOrEmpty(pxLoginScope1.CompanyName) ? string.Empty : "@" + pxLoginScope1.CompanyName), PXAccess.GetAdministratorRoles()))
      {
        screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID1);
        PXLocalizer.Localize("Explicit", typeof (InfoMessages).FullName);
      }
    }
    if (pxLoginScope1 != null)
      PXDatabase.ResetCredentials();
    if (screenIdUnsecure != null)
    {
      PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID1));
      return context.GetAsmxHandler(HandlerFactory.GetScreenTypedType());
    }
    string str1 = context.Request.Headers["SOAPAction"] ?? context.Request.QueryString["op"] ?? string.Empty;
    string str2 = "http://www.acumatica.com/generic/";
    int num2;
    if (!string.IsNullOrEmpty(str1) && (num2 = str1.IndexOf(str2, StringComparison.OrdinalIgnoreCase)) >= 0)
    {
      string str3 = str1.Substring(num2 + str2.Length, str1.Length - num2 - str2.Length);
      if (str3.EndsWith("\""))
        str3 = str3.Substring(0, str3.Length - 1);
      if (str3.EndsWith("/"))
        str3 = str3.Substring(0, str3.Length - 1);
      int length;
      if ((length = str3.IndexOf("/")) > 0)
        PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", str3.Substring(0, length)));
      return context.GetAsmxHandler(HandlerFactory.GetScreenGenericType());
    }
    if (context.Request.ContentEncoding != null)
    {
      byte[] numArray = new byte[context.Request.InputStream.Length];
      int count;
      try
      {
        count = context.Request.InputStream.Read(numArray, 0, numArray.Length);
      }
      finally
      {
        context.Request.InputStream.Position = 0L;
      }
      using (StringWriter stringWriter = new StringWriter())
      {
        stringWriter.Write(context.Request.ContentEncoding.GetChars(numArray, 0, count));
        string str4 = stringWriter.ToString();
        int num3 = str4.IndexOf("<soap:Body>", StringComparison.OrdinalIgnoreCase);
        if (num3 == -1)
          num3 = str4.IndexOf("<soap12:Body>", StringComparison.OrdinalIgnoreCase);
        if (num3 != -1)
        {
          int num4 = str4.IndexOf('<', num3 + 1);
          if (num4 != -1)
          {
            for (int index = 1; num4 + index < str4.Length; ++index)
            {
              if (!char.IsLetterOrDigit(str4[num4 + index]))
              {
                if (str4[num4 + index] == ':')
                {
                  num4 += index;
                  break;
                }
                break;
              }
            }
          }
          if (num4 + 9 <= str4.Length)
          {
            string screenID2 = str4.Substring(num4 + 1, 8);
            PXLoginScope pxLoginScope2;
            using (pxLoginScope2 = ScreenUtils.EnsureLogin())
            {
              using (new PXImpersonationContext(pxLoginScope2 == null ? Thread.CurrentPrincipal.Identity.Name : pxLoginScope2.UserName + (string.IsNullOrEmpty(pxLoginScope2.CompanyName) ? string.Empty : "@" + pxLoginScope2.CompanyName), PXAccess.GetAdministratorRoles()))
                screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID2);
            }
            if (pxLoginScope2 != null)
              PXDatabase.ResetCredentials();
            if (screenIdUnsecure != null)
            {
              PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID2));
              return context.GetAsmxHandler(HandlerFactory.GetScreenGenericType());
            }
          }
        }
      }
    }
    return context.GetAsmxHandler(HandlerFactory.GetScreenGenericType());
  }

  private static IHttpHandler GetWsdlBuilder(string screenID)
  {
    return (IHttpHandler) new WsdlBuilder(screenID);
  }

  private static System.Type GetScreenGenericType() => typeof (ScreenGeneric);

  private static System.Type GetScreenUntypedType() => typeof (ScreenUntyped);

  private static System.Type GetScreenTypedType() => typeof (ScreenTyped);
}
