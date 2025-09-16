// Decompiled with JetBrains decompiler
// Type: PX.Data.Handlers.AppPath
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Handlers;

internal static class AppPath
{
  /// <returns>Application path with session segment (usually <c>W(..)</c>) appended to it.</returns>
  /// <seealso cref="P:System.Web.HttpRequest.ApplicationPath">HttpRequest.ApplicationPath</seealso>
  /// <seealso cref="M:System.Web.HttpResponse.ApplyAppPathModifier(System.String)">HttpResponse.ApplyAppPathModifier</seealso>
  internal static string WithSessionIdentifier
  {
    get
    {
      string str = HttpContext.Current.Response.ApplyAppPathModifier(HttpContext.Current.Request.ApplicationPath);
      StringBuilder stringBuilder = new StringBuilder(str);
      if (!str.EndsWith("/"))
        stringBuilder.Append("/");
      return stringBuilder.ToString();
    }
  }
}
