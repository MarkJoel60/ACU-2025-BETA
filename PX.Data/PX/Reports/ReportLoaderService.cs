// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportLoaderService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data;
using PX.Data.Reports;
using PX.Data.Wiki.Parser;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Reports;

internal class ReportLoaderService : IFullTrustReportLoaderService, IReportLoaderService
{
  private readonly LocalizationProvider _localizationProvider;
  private readonly SettingsProvider _settingsProvider;

  public ISettings WikiSettings { get; }

  public ReportLoaderService(
    ISettings wikiSettings,
    LocalizationProvider localizationProvider,
    SettingsProvider settingsProvider)
  {
    this.WikiSettings = wikiSettings;
    this._localizationProvider = localizationProvider;
    this._settingsProvider = settingsProvider;
  }

  /// <exception cref="T:PX.Data.PXException">When user has no rights to the screen</exception>
  public Report LoadReport(string reportID, IPXResultset incoming)
  {
    return this.LoadReport(this.FindReport(reportID, false), incoming, false);
  }

  Report IFullTrustReportLoaderService.LoadReportUnsecure(string reportID, IPXResultset incoming)
  {
    return this.LoadReport(this.FindReport(reportID, true), incoming, true);
  }

  private Report LoadReport(PXSiteMapNode siteMap, IPXResultset incoming, bool securityTrimming)
  {
    if (siteMap == null)
      return (Report) null;
    Report report = new Report();
    string schemaFromUrl = ReportSchemaExtractor.ExtractSchemaFromUrl(siteMap.Url);
    if (schemaFromUrl == null)
      return (Report) null;
    ((ReportItem) report).Name = siteMap.Title;
    bool flag = PXSiteMap.IsARmReport(siteMap.Url);
    if (!flag)
    {
      if (!schemaFromUrl.EndsWith(".rpx"))
        schemaFromUrl += ".rpx";
      report.LoadByName(schemaFromUrl, this._localizationProvider);
      SoapNavigator soapNavigator = new SoapNavigator(new PXGraph(), incoming, securityTrimming);
      report.DataSource = (object) soapNavigator;
      report.ApplyRules((ReportItem) report);
    }
    report.SchemaUrl = flag ? siteMap.Url : schemaFromUrl;
    return report;
  }

  private PXSiteMapNode FindReport(string screenID, bool securityTrimming)
  {
    return (securityTrimming ? PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID) : PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID)) ?? throw new PXException(string.Format(ErrorMessages.GetLocal("You have insufficient rights to access the object ({0})."), (object) screenID));
  }

  public void InitDefaultReportParameters(Report report, IDictionary<string, string> rParams)
  {
    PXReportSettings settings = this._settingsProvider.Default;
    this.InitReportParameters(report, rParams, settings);
  }

  public void InitReportParameters(
    Report report,
    IDictionary<string, string> rParams,
    PXReportSettings settings)
  {
    this.InitReportParameters(report, rParams, settings, false);
  }

  public void InitReportParameters(
    Report report,
    IDictionary<string, string> rParams,
    PXReportSettings settings,
    bool resetReport)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (KeyValuePair<string, string> rParam in (IEnumerable<KeyValuePair<string, string>>) rParams)
    {
      dictionary[rParam.Key] = rParam.Value;
      string key = "Row" + rParam.Key;
      if (key.Contains(".") && !dictionary.ContainsKey(key))
        dictionary.Add(key, rParam.Value);
    }
    SoapNavigator soapNavigator = (SoapNavigator) report.DataSource;
    if (resetReport)
    {
      soapNavigator = new SoapNavigator(new PXGraph(), soapNavigator.Incoming);
      report.DataSource = (object) soapNavigator;
    }
    if (settings != null)
    {
      settings.ParameterValues.UpdateParameters(report.Parameters);
      ((Settings) settings.CommonSettings).UpdateParameters((Settings) report.CommonSettings);
      ((List<SortExp>) report.DynamicSorting).Clear();
      ((List<SortExp>) report.DynamicSorting).AddRange((IEnumerable<SortExp>) settings.Sorting);
      ((List<FilterExp>) report.DynamicFilters).Clear();
      ((List<FilterExp>) report.DynamicFilters).AddRange((IEnumerable<FilterExp>) settings.Filters);
      report.RequestParams = false;
      if (settings.BaseFilters != null && ((List<FilterExp>) settings.BaseFilters).Count > 0)
      {
        ((List<FilterExp>) report.Filters).Clear();
        ((List<FilterExp>) report.Filters).AddRange((IEnumerable<FilterExp>) settings.BaseFilters);
      }
    }
    else
    {
      foreach (SortExp sortExp in (List<SortExp>) report.Sorting)
        ((List<SortExp>) report.DynamicSorting).Add(sortExp);
    }
    foreach (ReportParameter parameter in (List<ReportParameter>) report.Parameters)
    {
      string str;
      if (dictionary.TryGetValue(parameter.Name, out str))
      {
        parameter.Value = (object) str;
        dictionary.Remove(parameter.Name);
      }
      else
        parameter.Process((IDataNavigator) soapNavigator, report);
    }
    foreach (ViewerField viewerField in (List<ViewerField>) report.ViewerFields)
    {
      if (string.IsNullOrEmpty(viewerField.Description) && !string.IsNullOrEmpty(viewerField.Name))
      {
        string field = (string) null;
        string a = (string) null;
        int length = viewerField.Name.IndexOf('.');
        if (length <= 1)
        {
          if (((List<ReportTable>) report.Tables).Count <= 1)
            field = $"{((List<ReportTable>) report.Tables)[0].Name}.{viewerField.Name}";
          else
            continue;
        }
        else
        {
          a = viewerField.Name.Substring(0, length);
          foreach (ReportRelation relation in (CollectionBase) report.Relations)
          {
            if (a.OrdinalEquals(relation.ParentAlias))
            {
              field = $"{relation.ParentTable.Name}.{viewerField.Name.Substring(length + 1)}";
              break;
            }
            if (a.OrdinalEquals(relation.ChildAlias))
            {
              field = $"{relation.ChildTable.Name}.{viewerField.Name.Substring(length + 1)}";
              break;
            }
          }
        }
        if (field != null)
        {
          if (soapNavigator.GetDisplayName((object) field) is string str && a != null)
          {
            int startIndex = str.IndexOf('.');
            if (startIndex > 1)
              str = a + str.Substring(startIndex);
          }
        }
        else
          str = soapNavigator.GetDisplayName((object) viewerField.Name) as string;
        if (str != null)
          viewerField.Description = str;
      }
    }
    if (dictionary.Count <= 0)
      return;
    int count = ((List<FilterExp>) report.Filters).Count;
    if (count > 0)
    {
      ++((List<FilterExp>) report.Filters)[0].OpenBraces;
      ++((List<FilterExp>) report.Filters)[count - 1].CloseBraces;
      ((List<FilterExp>) report.Filters)[count - 1].Operator = (FilterOperator) 0;
    }
    foreach (ViewerField viewerField in (List<ViewerField>) report.ViewerFields)
    {
      string fieldName = viewerField.Name.StartsWith("Row") ? viewerField.Name.Substring(3) : viewerField.Name;
      string str;
      if (!string.IsNullOrEmpty(viewerField.Description) && dictionary.TryGetValue(viewerField.Description, out str) || dictionary.TryGetValue(viewerField.Name, out str))
      {
        this.DeactivateFiltersOnField(report, fieldName);
        ((List<FilterExp>) report.Filters).Add(new FilterExp(viewerField.Name, (FilterCondition) 0)
        {
          Value = str
        });
      }
    }
    int num1 = ((List<FilterExp>) report.Filters).Count - 1;
    int num2 = 0;
    while (num1 < ((List<FilterExp>) report.Filters).Count)
    {
      bool flag = false;
      num1 = ((List<FilterExp>) report.Filters).Count;
      foreach (ViewerField viewerField in (List<ViewerField>) report.ViewerFields)
      {
        string fieldName = viewerField.Name.StartsWith("Row") ? viewerField.Name.Substring(3) : viewerField.Name;
        StringBuilder stringBuilder1 = new StringBuilder(viewerField.Name);
        stringBuilder1.Append(Convert.ToString(num2));
        StringBuilder stringBuilder2 = new StringBuilder(viewerField.Description ?? viewerField.Name);
        stringBuilder2.Append(Convert.ToString(num2));
        string str;
        if (dictionary.TryGetValue(stringBuilder2.ToString(), out str) || dictionary.TryGetValue(stringBuilder1.ToString(), out str))
        {
          if (!flag)
            this.DeactivateFiltersOnField(report, fieldName);
          ((List<FilterExp>) report.Filters).Add(new FilterExp(viewerField.Name, (FilterCondition) 0)
          {
            Value = str,
            Operator = (FilterOperator) 0
          });
        }
      }
      if (num1 < ((List<FilterExp>) report.Filters).Count)
        ((List<FilterExp>) report.Filters)[((List<FilterExp>) report.Filters).Count - 1].Operator = (FilterOperator) 1;
      ++num2;
    }
    if (((List<FilterExp>) report.Filters).Count <= count)
      return;
    ++((List<FilterExp>) report.Filters)[count].OpenBraces;
    ++((List<FilterExp>) report.Filters)[((List<FilterExp>) report.Filters).Count - 1].CloseBraces;
  }

  private void DeactivateFiltersOnField(Report report, string fieldName)
  {
    foreach (FilterExp filter in (List<FilterExp>) report.Filters)
    {
      if (filter.DataField.OrdinalEquals(fieldName) && filter.Condition == null)
      {
        filter.Condition = (FilterCondition) 12;
        filter.Value = filter.Value2 = "";
      }
    }
  }
}
