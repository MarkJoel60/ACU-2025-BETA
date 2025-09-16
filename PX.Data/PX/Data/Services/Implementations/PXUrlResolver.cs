// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Implementations.PXUrlResolver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Services.Interfaces;
using System;

#nullable disable
namespace PX.Data.Services.Implementations;

internal class PXUrlResolver : IUrlResolver
{
  public string ToAbsolute(string relativeUrl)
  {
    try
    {
      return PXUrl.ToAbsoluteUrl(relativeUrl, false);
    }
    catch (ArgumentException ex) when (!(ex is ArgumentNullException) && !(ex is ArgumentOutOfRangeException))
    {
      return relativeUrl;
    }
  }

  public string ToRelative(string absoluteUrl) => PXUrl.ToRelativeUrl(absoluteUrl);

  public string SecureQueryString(string queryString)
  {
    if (string.IsNullOrEmpty(queryString))
      return queryString;
    Func<string, string> func = (Func<string, string>) (s => s);
    if (queryString.StartsWith("&"))
    {
      queryString = "?" + queryString.TrimStart('&');
      func = (Func<string, string>) (s => "&" + s.TrimStart('?'));
    }
    else if (!queryString.StartsWith("?"))
    {
      queryString = "?" + queryString;
      func = (Func<string, string>) (s => s.TrimStart('?'));
    }
    return func(PXUrl.IgnoreSystemParameters(queryString));
  }

  public string IgnoreSystemQueryParameters(string url)
  {
    return string.IsNullOrEmpty(url) ? url : PXUrl.IgnoreSystemParameters(url);
  }

  public bool IsAbsolute(string url) => PXUrl.IsAbsolute(url);
}
