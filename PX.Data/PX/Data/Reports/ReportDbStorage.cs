// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportDbStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.BulkInsert;
using PX.BulkInsert.SpecialUpgrade.UserReports;
using PX.BulkInsert.Tools;
using PX.Data.Localization;
using PX.Metadata;
using PX.Reports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#nullable disable
namespace PX.Data.Reports;

public static class ReportDbStorage
{
  private static UserReport GetActive(string fileName)
  {
    return (UserReport) PXSelectBase<UserReport, PXSelect<UserReport, Where<Optional<UserReport.reportFileName>, Equal<UserReport.reportFileName>, And<UserReport.isActive, Equal<True>>>, OrderBy<Desc<UserReport.isActive, Desc<UserReport.version>>>>.Config>.SelectSingleBound(new PXGraph(), (object[]) null, (object) fileName);
  }

  public static string GetVersion(string fileName, int id)
  {
    UserReport userReport = ReportDbStorage.SelectVersion(fileName, id);
    return string.IsNullOrWhiteSpace(userReport?.Xml) ? (string) null : ReportDbStorage.AddVersionToXml(userReport.Xml);
  }

  internal static ReportDbStorage.VersionUpdateResult TryUpdateExistingReportVersionXml(
    string fileName,
    int reportVersionNumber,
    string xml)
  {
    UserReport userReport = ReportDbStorage.SelectVersion(fileName, reportVersionNumber);
    if (string.IsNullOrWhiteSpace(userReport?.Xml))
      return ReportDbStorage.VersionUpdateResult.NotFound;
    userReport.Xml = ReportDbStorage.AddVersionToXml(xml);
    ReportDbStorage.Update(userReport);
    return ReportDbStorage.VersionUpdateResult.SuccessfullyUpdated;
  }

  private static UserReport SelectVersion(string fileName, int id)
  {
    return (UserReport) PXSelectBase<UserReport, PXSelect<UserReport, Where<Optional<UserReport.reportFileName>, Equal<UserReport.reportFileName>, And<Optional<UserReport.version>, Equal<UserReport.version>>>>.Config>.SelectSingleBound(new PXGraph(), (object[]) null, (object) fileName, (object) id);
  }

  internal static void Update(params UserReport[] r)
  {
    PXGraph pxGraph = new PXGraph();
    pxGraph.Views.Caches.Add(typeof (UserReport));
    foreach (UserReport userReport in r)
      pxGraph.Caches[typeof (UserReport)].Update((object) userReport);
    pxGraph.Actions.PressSave();
    IScreenInfoCacheControl instance = ServiceLocator.IsLocationProviderSet ? ServiceLocator.Current.GetInstance<IScreenInfoCacheControl>() : (IScreenInfoCacheControl) null;
    if (instance == null)
      return;
    foreach (UserReport userReport in r)
    {
      string screenId = ReportStorageHelper.GetScreenId(userReport.ReportFileName);
      if (!string.IsNullOrEmpty(screenId))
        instance.InvalidateCache(screenId);
    }
  }

  public static string[] ReportEnumNames()
  {
    return ((IEnumerable<string>) ((IEnumerable<UserReportHeader>) PXSelectBase<UserReportHeader, PXSelect<UserReportHeader, Where<Optional<UserReport.isActive>, Equal<UserReport.isActive>>>.Config>.Select(new PXGraph(), (object) true).FirstTableItems.ToArray<UserReportHeader>()).Select<UserReportHeader, string>((Func<UserReportHeader, string>) (r => r.ReportFileName)).Distinct<string>().ToArray<string>()).Union<string>((IEnumerable<string>) ReportFileManager.ReportEnumNames()).ToArray<string>();
  }

  public static string ReportGetXmlFromDb(string reportName)
  {
    UserReport active = ReportDbStorage.GetActive(reportName);
    return active == null ? (string) null : ReportDbStorage.AddVersionToXml(active.Xml);
  }

  public static string ReportGetXml(string reportName)
  {
    if (string.IsNullOrEmpty(reportName) || !reportName.EndsWith(".rpx"))
      return string.Empty;
    string enabledLocalization = ServiceLocator.Current.GetInstance<ILocalizationFeaturesService>().GetEnabledLocalization();
    if (enabledLocalization != null)
    {
      string str = reportName.Insert(reportName.IndexOf(".rpx"), "." + enabledLocalization);
      try
      {
        return ReportDbStorage.ReportGetXmlFromDb(str) ?? ReportDbStorage.AddVersionToXml(ReportFileManager.ReportGetXml(str));
      }
      catch (FileNotFoundException ex)
      {
      }
    }
    return ReportDbStorage.ReportGetXmlFromDb(reportName) ?? ReportDbStorage.AddVersionToXml(ReportFileManager.ReportGetXml(reportName));
  }

  public static bool HasReport(string reportId)
  {
    bool flag = true;
    try
    {
      if (!reportId.EndsWith(".rpx"))
        reportId += ".rpx";
      ReportDbStorage.ReportGetXml(reportId);
    }
    catch (FileNotFoundException ex)
    {
      flag = false;
    }
    return flag;
  }

  /// <summary>
  /// Updates the latest version of the existing report or creates a new report version in the DB in case the report does not exist in the DB.
  /// </summary>
  /// <param name="reportName">Name of the report.</param>
  /// <param name="xml">The report version new XML.</param>
  /// <param name="versionDescription">The report version description.</param>
  /// <returns>
  /// A <see cref="T:PX.Data.Reports.ReportDbStorage.VersionCreateOrUpdateResult" /> value indicating if the report version is found and updated, or new version is created.
  /// </returns>
  internal static ReportDbStorage.VersionCreateOrUpdateResult CreateOrUpdateLatestReportVersion(
    string reportName,
    string xml,
    string versionDescription)
  {
    UserReport active = ReportDbStorage.GetActive(reportName);
    ReportDbStorage.SaveCustomizedReport(active, reportName, xml, false, versionDescription);
    return active == null ? ReportDbStorage.VersionCreateOrUpdateResult.Created : ReportDbStorage.VersionCreateOrUpdateResult.Updated;
  }

  public static void SaveCustomizedReport(
    string reportName,
    string xml,
    bool createVersion,
    string versionDescription)
  {
    ReportDbStorage.SaveCustomizedReport(ReportDbStorage.GetActive(reportName), reportName, xml, createVersion, versionDescription);
  }

  private static void SaveCustomizedReport(
    UserReport active,
    string reportName,
    string xml,
    bool createVersion,
    string versionDescription)
  {
    xml = ReportDbStorage.AddVersionToXml(xml);
    if ((createVersion ? 1 : (active == null ? 1 : 0)) != 0)
    {
      UserReport userReport1 = (UserReport) PXSelectBase<UserReport, PXSelect<UserReport, Where<Optional<UserReport.reportFileName>, Equal<UserReport.reportFileName>>, OrderBy<Desc<UserReport.version>>>.Config>.SelectSingleBound(new PXGraph(), (object[]) null, (object) reportName);
      int num1 = 0;
      if (userReport1 != null)
        num1 = userReport1.Version.GetValueOrDefault();
      int num2 = num1 + 1;
      UserReport userReport2 = new UserReport()
      {
        DateCreated = new System.DateTime?(System.DateTime.Now),
        IsActive = new bool?(true),
        ReportFileName = reportName,
        Version = new int?(num2),
        Xml = xml
      };
      if (versionDescription != null)
        userReport2.Description = versionDescription;
      if (active == null)
      {
        ReportDbStorage.Update(userReport2);
      }
      else
      {
        active.IsActive = new bool?(false);
        ReportDbStorage.Update(active, userReport2);
      }
    }
    else
    {
      UserReport userReport = active;
      userReport.Xml = xml;
      if (versionDescription != null)
        userReport.Description = versionDescription;
      ReportDbStorage.Update(userReport);
    }
  }

  private static string AddVersionToXml(string xml)
  {
    XDocument xdocument = XDocument.Parse(xml);
    int reportUpgraderVersion = ReportDbStorage.GetReportUpgraderVersion();
    string attributeValue = XMLHelper.GetAttributeValue(xdocument.Root, "version");
    int result;
    if (!string.IsNullOrEmpty(attributeValue) && int.TryParse(attributeValue, out result) && result == reportUpgraderVersion)
      return xml;
    xdocument.Root.SetAttributeValue((XName) "version", (object) reportUpgraderVersion.ToString());
    using (TextWriter textWriter = (TextWriter) new Utf8StringWriter(new StringBuilder(), (IFormatProvider) CultureInfo.InvariantCulture))
    {
      xdocument.Save(textWriter);
      return textWriter.ToString();
    }
  }

  private static int GetReportUpgraderVersion()
  {
    int reportUpgraderVersion = 20160101;
    if (reportUpgraderVersion < UpgradeHelper.ClassesThatUpdateReports.Value.Item1)
      reportUpgraderVersion = UpgradeHelper.ClassesThatUpdateReports.Value.Item1;
    return reportUpgraderVersion;
  }

  /// <summary>
  /// Values that represent the result of the update operation of the existing report version in the DB.
  /// </summary>
  internal enum VersionUpdateResult
  {
    NotFound,
    FailedToUpdate,
    SuccessfullyUpdated,
  }

  internal enum VersionCreateOrUpdateResult
  {
    FailedToUpdate,
    Updated,
    Created,
  }
}
