// Decompiled with JetBrains decompiler
// Type: PX.Api.Mobile.PXDialogRequiredExceptionExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Api.Mobile;

public static class PXDialogRequiredExceptionExtension
{
  public const string SMART_PANEL_SCOPE_KEY = "PX.Api.Mobile.Scopes.SmartPanelScope.UseSmartPanels";
  public static string UrlPrefix = "__answer";

  public static string CachePrefix(string viewName, string key)
  {
    return $"{PXDialogRequiredExceptionExtension.UrlPrefix}_{viewName}_{key}";
  }

  public static string CacheKey(this PXDialogRequiredException ex) => $"{ex.ViewName}_{ex.Key}";

  public static string CachePrefix(this PXDialogRequiredException ex)
  {
    return $"{PXDialogRequiredExceptionExtension.UrlPrefix}_{ex.CacheKey()}";
  }

  public static bool IsSimple(this PXDialogRequiredException ex) => !Str.IsNullOrEmpty(ex.Message);

  public static bool UseAskDialog(this PXDialogRequiredException ex)
  {
    return PXContext.GetSlot<bool>("PX.Api.Mobile.Scopes.SmartPanelScope.UseSmartPanels") || ex.IsSimple();
  }

  public static (string ViewName, string Key) ParseCachePrefix(string cachePrefix)
  {
    string[] strArray = cachePrefix.TrimStart('_').Split('_');
    if (strArray.Length <= 1)
      return ((string) null, (string) null);
    return strArray.Length != 2 ? (strArray[1], strArray[2]) : (strArray[1], (string) null);
  }
}
