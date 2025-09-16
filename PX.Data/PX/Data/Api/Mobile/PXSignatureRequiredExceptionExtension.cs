// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.PXSignatureRequiredExceptionExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Api.Mobile.SignManager;
using System;

#nullable disable
namespace PX.Data.Api.Mobile;

[PXInternalUseOnly]
public static class PXSignatureRequiredExceptionExtension
{
  public static string UrlPrefix = "__result";

  public static string CacheKey(this PXSignatureRequiredException ex)
  {
    return PXSignatureRequiredExceptionExtension.GetCacheKey(ex.ViewName, ex.FileId);
  }

  public static string CachePrefix(this PXSignatureRequiredException ex)
  {
    return PXSignatureRequiredExceptionExtension.GetCachePrefix(ex.ViewName, ex.FileId);
  }

  public static string GetPreviewFileId(Guid fileId) => $"{fileId}_ReportPreview";

  public static string GetCacheKey(string viewName, Guid fileId) => $"{viewName}_{fileId}";

  public static string GetCachePrefix(string viewName, Guid fileId)
  {
    return $"{PXSignatureRequiredExceptionExtension.UrlPrefix}_{PXSignatureRequiredExceptionExtension.GetCacheKey(viewName, fileId)}";
  }

  public static (string ViewName, Guid? FileId) ParseCachePrefix(string cachePrefix)
  {
    if (!cachePrefix.StartsWith(PXSignatureRequiredExceptionExtension.UrlPrefix))
      return ((string) null, new Guid?());
    string[] strArray = cachePrefix.TrimStart('_').Split('_');
    if (strArray.Length <= 1)
      return ((string) null, new Guid?());
    return strArray.Length != 2 ? (strArray[1], new Guid?(Guid.Parse(strArray[2]))) : (strArray[1], new Guid?());
  }
}
