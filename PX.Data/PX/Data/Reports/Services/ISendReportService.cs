// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.Services.ISendReportService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Reports.Mail;
using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Reports.Services;

[PXInternalUseOnly]
public interface ISendReportService
{
  void SendEmail(GroupMessage message, IList<FileInfo> files);

  void SendEmail(SendEmailParams sendEmailParams);

  void SendEmail(SendEmailParams sendEmailParams, object handler);
}
