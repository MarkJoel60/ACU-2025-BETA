// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportFileManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

#nullable disable
namespace PX.Reports;

public static class ReportFileManager
{
  public static string ReportsDir = "~/ReportsDefault";
  public static string CustomReportsDir = "~/ReportsCustomized";

  public static string[] ReportEnumNames()
  {
    return ((IEnumerable<string>) Directory.GetFiles(ReportFileManager.GetReportsRoot(), "*.rpx", SearchOption.AllDirectories)).Select<string, string>((Func<string, string>) (f => Path.GetFileName(f).ToLowerInvariant())).ToArray<string>();
  }

  public static string ReportGetXml(string name)
  {
    string reportPathByName = ReportFileManager.GetReportPathByName(name);
    return !string.IsNullOrEmpty(reportPathByName) || string.IsNullOrEmpty(name) ? File.ReadAllText(reportPathByName) : throw new FileNotFoundException($"The report with name {name} is not found.");
  }

  public static void SaveCustomizedReport(string name, string xml)
  {
    if (!Directory.Exists(ReportFileManager.GetCustomizedDir()))
      Directory.CreateDirectory(ReportFileManager.GetCustomizedDir());
    string customizedPath = ReportFileManager.GetCustomizedPath(name);
    if (Path.GetExtension(customizedPath) != ".rpx" && !PXSiteMap.IsARmReport(customizedPath))
      throw new ApplicationException(".rpx file extension required");
    if (File.Exists(customizedPath) && File.ReadAllText(customizedPath) == xml)
      return;
    File.WriteAllText(customizedPath, xml);
  }

  public static string GetReportPathByName(string name)
  {
    string str = !(Path.GetExtension(name) != ".rpx") || PXSiteMap.IsARmReport(name) ? Path.GetFileName(name) : throw new ApplicationException(".rpx file extension required");
    string customizedPath = ReportFileManager.GetCustomizedPath(str);
    if (File.Exists(customizedPath))
      return customizedPath;
    foreach (string reportsDir in ReportFileManager.GetReportsDirs())
    {
      string path = Path.Combine(reportsDir, str);
      if (File.Exists(path))
        return path;
    }
    return string.Empty;
  }

  public static bool IsCustomized(string name)
  {
    return File.Exists(ReportFileManager.GetCustomizedPath(name));
  }

  private static string GetCustomizedPath(string name)
  {
    string fileName = Path.GetFileName(name);
    if (Path.GetExtension(name) != ".rpx" && !PXSiteMap.IsARmReport(name))
      throw new ApplicationException(".rpx file extension required");
    return Path.Combine(ReportFileManager.GetCustomizedDir(), fileName);
  }

  private static string GetCustomizedDir()
  {
    return HostingEnvironment.MapPath(ReportFileManager.CustomReportsDir);
  }

  private static string GetReportsRoot()
  {
    return HostingEnvironment.MapPath(ReportFileManager.ReportsDir);
  }

  private static string[] GetReportsDirs()
  {
    string path = HostingEnvironment.MapPath(ReportFileManager.ReportsDir);
    return ((IEnumerable<string>) new string[1]{ path }).Union<string>((IEnumerable<string>) Directory.GetDirectories(path, "*", SearchOption.AllDirectories)).ToArray<string>();
  }
}
