// Decompiled with JetBrains decompiler
// Type: PX.SM.LongOperationPerformanceParameters
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.SM;

internal sealed class LongOperationPerformanceParameters
{
  public LongOperationPerformanceParameters(ISlotStore slots, HttpContext httpContext)
  {
    if (httpContext != null)
    {
      try
      {
        this.ScreenUrl = PXUrl.GetScreenUrl(httpContext.Request.RawUrl).ToLowerInvariant();
        string str1 = httpContext.Request.Form["__CALLBACKPARAM"];
        if (str1 != null)
        {
          this.CommandName = ((IEnumerable<string>) str1.Split('|')).FirstOrDefault<string>();
        }
        else
        {
          string str2 = httpContext.Request.RawUrl;
          if (!string.IsNullOrEmpty(httpContext.Request.ApplicationPath) && str2.StartsWith(httpContext.Request.ApplicationPath, StringComparison.OrdinalIgnoreCase))
          {
            str2 = str2.Substring(httpContext.Request.ApplicationPath.Length);
            if (!str2.StartsWith("/"))
              str2 = "/" + str2;
          }
          if (!str2.StartsWith("/ui/", StringComparison.OrdinalIgnoreCase))
            return;
          this.CommandName = PXPerformanceMonitor.CurrentSample?.CommandName;
        }
      }
      catch (HttpException ex)
      {
      }
    }
    else
    {
      string screenId = slots.GetScreenID();
      if (string.IsNullOrEmpty(screenId))
        return;
      this.ScreenUrl = $"~/Pages/{screenId.Substring(0, 2)}/{screenId.Replace(".", "")}.aspx".ToLowerInvariant();
    }
  }

  internal string CommandName { get; }

  internal string ScreenUrl { get; }
}
