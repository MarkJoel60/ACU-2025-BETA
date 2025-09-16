// Decompiled with JetBrains decompiler
// Type: PX.Licensing.ApiRequestHelpers
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;

#nullable disable
namespace PX.Licensing;

internal static class ApiRequestHelpers
{
  internal static bool IsApiRequest(this HttpContext c)
  {
    return c != null && ApiRequestHelpers.IsApiRequest(c.Request.HttpMethod, c.Request.AppRelativeCurrentExecutionFilePath);
  }

  internal static bool IsApi(this HttpRequestBase request)
  {
    return ApiRequestHelpers.IsApiRequest(request.HttpMethod, request.AppRelativeCurrentExecutionFilePath);
  }

  private static bool IsApiRequest(string httpMethod, string relativePath)
  {
    if (relativePath == null)
      return false;
    if (relativePath.StartsWith("~/entity/", StringComparison.OrdinalIgnoreCase))
    {
      string[] strArray = relativePath.Split('/');
      return (!httpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) || strArray.Length != 4) && !relativePath.EndsWith("swagger.json", StringComparison.OrdinalIgnoreCase) && (!httpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) || strArray.Length <= 6 || !strArray[6].Equals("status", StringComparison.OrdinalIgnoreCase)) && (strArray.Length <= 2 || !strArray[2].Equals("DeviceHub", StringComparison.OrdinalIgnoreCase));
    }
    return relativePath.StartsWith("~/soap/", StringComparison.OrdinalIgnoreCase) ? !(httpMethod == "GET") : relativePath.StartsWith("~/Api/", StringComparison.OrdinalIgnoreCase) && !relativePath.StartsWith("~/Api/commercehook", StringComparison.OrdinalIgnoreCase) || relativePath.StartsWith("~/Webhooks/", StringComparison.OrdinalIgnoreCase) || relativePath.StartsWith("~/CustomizationApi/", StringComparison.OrdinalIgnoreCase);
  }
}
