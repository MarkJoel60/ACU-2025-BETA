// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCustomErrorPageException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;
using System.Web;

#nullable disable
namespace PX.Data;

public class PXCustomErrorPageException : PXException
{
  public PXCustomErrorPageException(string msg)
    : base(msg)
  {
  }

  internal PXCustomErrorPageException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public static bool HandleError(HttpContext ctx)
  {
    try
    {
      if (ctx == null || ctx.Request.HttpMethod != "GET")
        return false;
      Exception lastError = ctx.Server.GetLastError();
      if (!(lastError is PXCustomErrorPageException errorPageException1))
        errorPageException1 = lastError?.InnerException as PXCustomErrorPageException;
      PXCustomErrorPageException errorPageException2 = errorPageException1;
      if (errorPageException2 == null)
        return false;
      string message = errorPageException2.Message;
      ctx.Server.ClearError();
      ctx.Response.Clear();
      ctx.Response.StatusCode = 500;
      ctx.Response.ContentType = "text/html";
      ctx.Response.Expires = -1;
      ctx.Response.Write($"{message} Timestamp:{System.DateTime.UtcNow.ToString("O")}");
      ctx.Response.Write(new string(' ', 600));
      ctx.Response.End();
    }
    catch (Exception ex)
    {
    }
    return true;
  }
}
