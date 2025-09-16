// Decompiled with JetBrains decompiler
// Type: PX.Api.ContractBased.AdHocReportServiceLegacyExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.AspNetCore;
using PX.Common;
using PX.Reports.Controls;
using System;
using System.Web;

#nullable enable
namespace PX.Api.ContractBased;

[PXInternalUseOnly]
public static class AdHocReportServiceLegacyExtensions
{
  public static string GetReportStatusPath(
    this IAdHocReportService service,
    HttpContext httpContext,
    Guid operationId,
    string format,
    string locale)
  {
    return service.GetReportStatusPath(HttpContextExtensions.GetOwinContext(httpContext).GetHttpContext(), operationId, format, locale);
  }

  public static string StartReport(
    this IAdHocReportService service,
    HttpContext httpContext,
    string screenId,
    string locale,
    string format,
    Action<Report> configurator)
  {
    Guid operationId = service.StartReport(screenId, locale, configurator);
    return service.GetReportStatusPath(httpContext, operationId, format, locale);
  }
}
