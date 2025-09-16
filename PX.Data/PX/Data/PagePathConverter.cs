// Decompiled with JetBrains decompiler
// Type: PX.Data.PagePathConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PagePathConverter
{
  private const char SEGMENT_SEPARATOR = '/';
  private const string CUSTOMIZATION_PREFIX = "~/CstPublished/";
  private const string ORIGINAL_PREFIX = "~/Pages/";
  private const string PAGE_FOLDER_PREFIX = "pages_";

  public static string ToOriginal(string pagePath)
  {
    return !PagePathConverter.IsPathOriginal(pagePath) && PagePathConverter.IsPathCustomized(pagePath) ? PagePathConverter.FromCustomizedToOriginal(pagePath) : pagePath;
  }

  public static string ToCustomized(string pagePath)
  {
    return !PagePathConverter.IsPathCustomized(pagePath) && PagePathConverter.IsPathOriginal(pagePath) ? PagePathConverter.FromOriginalToCustomized(pagePath) : pagePath;
  }

  public static bool IsPathCustomized(string pagePath)
  {
    return !string.IsNullOrEmpty(pagePath) && pagePath.StartsWith("~/CstPublished/", StringComparison.OrdinalIgnoreCase);
  }

  public static bool IsPathOriginal(string pagePath)
  {
    return !string.IsNullOrEmpty(pagePath) && pagePath.StartsWith("~/Pages/", StringComparison.OrdinalIgnoreCase);
  }

  private static string FromOriginalToCustomized(string originalPath)
  {
    string customized = originalPath;
    string[] strArray = originalPath.Split(new char[1]
    {
      '/'
    }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray.Length == 4)
    {
      string str1 = strArray[2];
      string str2 = strArray[3];
      if (str1.Length == 2)
        customized = $"~/CstPublished/pages_{str1.ToLower()}/{str2}";
    }
    return customized;
  }

  public static string FromCustomizedToOriginal(string customizedPath)
  {
    string original = customizedPath;
    string[] strArray = customizedPath.Split(new char[1]
    {
      '/'
    }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray.Length == 4)
    {
      string str1 = strArray[2];
      string str2 = Path.GetFileNameWithoutExtension(strArray[3]).ToUpper() + Path.GetExtension(strArray[3]);
      if (str1.StartsWith("pages_", StringComparison.OrdinalIgnoreCase))
        original = $"~/Pages/{str1.Replace("pages_", string.Empty).ToUpper()}/{str2}";
    }
    return original;
  }
}
