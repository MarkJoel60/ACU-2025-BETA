// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Reports;

internal static class ReportHelper
{
  public static (string reportId, bool isARmReport) GetReportInfoFromUrl(string url)
  {
    if (string.IsNullOrEmpty(url) || !url.StartsWith("~/Scripts/Screens/ReportScreen.html", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("~/Frames/ReportLauncher.aspx", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("~/Frames/RmLauncher.aspx", StringComparison.OrdinalIgnoreCase))
      return ((string) null, false);
    string str1 = ((IEnumerable<string>) url.Split('?')).LastOrDefault<string>() ?? "";
    StringValues source1;
    if (!QueryHelpers.ParseQuery(str1).TryGetValue("id", out source1) || string.IsNullOrEmpty(((IEnumerable<string>) (object) source1).FirstOrDefault<string>()))
      return ((string) null, false);
    string str2 = ((IEnumerable<string>) (object) source1).First<string>();
    if (str2.EndsWith(".rpx", StringComparison.OrdinalIgnoreCase))
    {
      string str3 = str2;
      int length = ".rpx".Length;
      str2 = str3.Substring(0, str3.Length - length);
    }
    StringValues source2;
    bool result;
    return !QueryHelpers.ParseQuery(str1).TryGetValue("IsARm", out source2) || !bool.TryParse(((IEnumerable<string>) (object) source2).FirstOrDefault<string>(), out result) ? (str2, url.StartsWith("~/Frames/RmLauncher.aspx", StringComparison.OrdinalIgnoreCase)) : (str2, result);
  }
}
