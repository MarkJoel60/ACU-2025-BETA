// Decompiled with JetBrains decompiler
// Type: PX.Api.ContractBased.IAdHocReportService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using PX.Common;
using PX.Reports.Controls;
using System;

#nullable enable
namespace PX.Api.ContractBased;

[PXInternalUseOnly]
public interface IAdHocReportService
{
  Guid StartReport(string screenId, string locale, Action<Report> configurator);

  string GetReportStatusPath(
    HttpContext httpContext,
    Guid operationId,
    string format,
    string locale);

  string GetReportStatusVirtualPath(
    string endpointName,
    string endpointVersion,
    Guid operationId,
    string format,
    string locale);
}
