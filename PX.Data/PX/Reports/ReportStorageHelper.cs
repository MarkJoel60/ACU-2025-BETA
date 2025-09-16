// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportStorageHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Reports;

internal static class ReportStorageHelper
{
  private const string ReportLauncherBaseUrl = "~/Frames/ReportLauncher.aspx?ID=";
  internal static readonly Dictionary<string, ReportInfo> ReportDescriptors = new Dictionary<string, ReportInfo>();

  public static void Clear() => ReportStorageHelper.ReportDescriptors.Clear();

  public static string GetScreenId(string reportName)
  {
    if (string.IsNullOrWhiteSpace(reportName))
      return (string) null;
    return PXSiteMap.Provider.FindSiteMapNodeUnsecure("~/Frames/ReportLauncher.aspx?ID=" + reportName)?.ScreenID;
  }
}
