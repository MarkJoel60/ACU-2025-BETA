// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportLauncherHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Reports;
using PX.Reports.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.Reports;

public static class ReportLauncherHelper
{
  public const string _SENDEMAILPARAMS_TYPE = "SendEmailParams";
  public const string _SENDEMAILMAINT_TYPE = "PX.Objects.CR.CREmailActivityMaint";
  public const string _SENDEMAIL_METHOD = "SendEmail";
  public const string _REPORTFUNCTIONS_TYPE = "PX.Objects.CA.Descriptor.ReportFunctions";
  public const string _COMMONREPORTFUNCTIONS_TYPE = "PX.Objects.Common.ReportFunctions";
  public const string _WEATHERINTEGRATIONUNITOFMEASURESERVICE_TYPE = "PX.Objects.PJ.DailyFieldReports.PJ.Services.WeatherIntegrationUnitOfMeasureService";
  public const string _REPORTID_PARAM_KEY = "ReportIDParamKey";
  public const string _AUTO_EXPORT_PDF = "AutoExportPDF";
  public const string _ENABLED_LOCALIZATION = "EnabledLocalization";

  public static void LoadParameters(Report report, object passed, SoapNavigator nav)
  {
    Dictionary<string, string> dictionary = PXReportRedirectParameters.UnwrapParameters(passed);
    if (dictionary != null)
    {
      foreach (ReportParameter parameter in (List<ReportParameter>) report.Parameters)
      {
        string str;
        if (dictionary.TryGetValue(parameter.Name, out str))
        {
          parameter.Value = (object) str;
          dictionary.Remove(parameter.Name);
        }
      }
    }
    nav.SelectArguments.Tables = report.Tables;
    foreach (ViewerField viewerField in (List<ViewerField>) report.ViewerFields)
    {
      if (string.IsNullOrEmpty(viewerField.Description) && !string.IsNullOrEmpty(viewerField.Name))
      {
        string field = (string) null;
        string strB = (string) null;
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
          strB = viewerField.Name.Substring(0, length);
          foreach (ReportRelation relation in (CollectionBase) report.Relations)
          {
            if (relation.ParentTable != null && string.Compare(relation.ParentAlias, strB, StringComparison.OrdinalIgnoreCase) == 0)
            {
              field = $"{relation.ParentTable.Name}.{viewerField.Name.Substring(length + 1)}";
              break;
            }
            if (relation.ChildTable != null && string.Compare(relation.ChildAlias, strB, StringComparison.OrdinalIgnoreCase) == 0)
            {
              field = $"{relation.ChildTable.Name}.{viewerField.Name.Substring(length + 1)}";
              break;
            }
          }
        }
        if (field != null)
        {
          if (nav.GetDisplayName((object) field) is string str && strB != null)
          {
            int num = str.IndexOf('.');
            if (num > 1)
              str = $"{str.Substring(0, num)} ({strB}){str.Substring(num)}";
          }
        }
        else
          str = nav.GetDisplayName((object) viewerField.Name) as string;
        if (str != null)
          viewerField.Description = str;
      }
    }
    if (dictionary != null && dictionary.Count > 0)
    {
      int count1 = ((List<FilterExp>) report.Filters).Count;
      if (count1 > 0)
      {
        ++((List<FilterExp>) report.Filters)[0].OpenBraces;
        ++((List<FilterExp>) report.Filters)[count1 - 1].CloseBraces;
        ((List<FilterExp>) report.Filters)[count1 - 1].Operator = (FilterOperator) 0;
      }
      int count2 = ((List<FilterExp>) report.DynamicFilters).Count;
      if (count2 > 0)
      {
        ++((List<FilterExp>) report.DynamicFilters)[0].OpenBraces;
        ++((List<FilterExp>) report.DynamicFilters)[count2 - 1].CloseBraces;
        ((List<FilterExp>) report.DynamicFilters)[count2 - 1].Operator = (FilterOperator) 0;
      }
      foreach (ViewerField viewerField in (List<ViewerField>) report.ViewerFields)
      {
        string str1 = viewerField.Name.StartsWith("Row") ? viewerField.Name.Substring(3) : viewerField.Name;
        string str2;
        foreach (FilterExp filter in (List<FilterExp>) report.Filters)
        {
          if (filter.DataField == str1 && (dictionary.TryGetValue(viewerField.Description, out str2) || dictionary.TryGetValue(viewerField.Name, out str2)) && filter.Condition == null)
          {
            filter.Condition = (FilterCondition) 12;
            filter.Value = filter.Value2 = "";
          }
        }
        if (!string.IsNullOrEmpty(viewerField.Description) && dictionary.TryGetValue(viewerField.Description, out str2) || dictionary.TryGetValue(viewerField.Name, out str2))
        {
          ((List<FilterExp>) report.DynamicFilters).Add(new FilterExp(viewerField.Name, (FilterCondition) 0)
          {
            Value = str2
          });
          dictionary.Remove(viewerField.Description);
          dictionary.Remove(viewerField.Name);
        }
      }
      int num1 = ((List<FilterExp>) report.DynamicFilters).Count - 1;
      int num2 = 0;
      while (num1 < ((List<FilterExp>) report.DynamicFilters).Count)
      {
        bool flag = false;
        num1 = ((List<FilterExp>) report.DynamicFilters).Count;
        foreach (ViewerField viewerField in (List<ViewerField>) report.ViewerFields)
        {
          string str3 = viewerField.Name.StartsWith("Row") ? viewerField.Name.Substring(3) : viewerField.Name;
          StringBuilder stringBuilder1 = new StringBuilder(str3);
          stringBuilder1.Append(Convert.ToString(num2));
          StringBuilder stringBuilder2 = new StringBuilder(viewerField.Description == null ? str3 : viewerField.Description);
          stringBuilder2.Append(Convert.ToString(num2));
          string str4;
          if (!flag)
          {
            foreach (FilterExp filter in (List<FilterExp>) report.Filters)
            {
              if (filter.DataField == str3 && (dictionary.TryGetValue(stringBuilder2.ToString(), out str4) || dictionary.TryGetValue(stringBuilder1.ToString(), out str4)) && filter.Condition == null)
              {
                filter.Condition = (FilterCondition) 12;
                filter.Value = filter.Value2 = "";
              }
            }
          }
          if (dictionary.TryGetValue(stringBuilder2.ToString(), out str4) || dictionary.TryGetValue(stringBuilder1.ToString(), out str4))
          {
            ((List<FilterExp>) report.DynamicFilters).Add(new FilterExp(viewerField.Name, (FilterCondition) 0)
            {
              Value = str4,
              Operator = (FilterOperator) 0
            });
            dictionary.Remove(stringBuilder2.ToString());
            dictionary.Remove(stringBuilder1.ToString());
          }
        }
        if (num1 < ((List<FilterExp>) report.DynamicFilters).Count)
          ((List<FilterExp>) report.DynamicFilters)[((List<FilterExp>) report.DynamicFilters).Count - 1].Operator = (FilterOperator) 1;
        ++num2;
      }
      if (count1 > 0 && ((List<FilterExp>) report.DynamicFilters).Count > count2)
      {
        ++((List<FilterExp>) report.DynamicFilters)[0].OpenBraces;
        ++((List<FilterExp>) report.DynamicFilters)[((List<FilterExp>) report.DynamicFilters).Count - 1].CloseBraces;
      }
    }
    if (dictionary == null || dictionary.Count <= 0)
      return;
    string str5 = string.Empty;
    foreach (KeyValuePair<string, string> keyValuePair in dictionary)
    {
      if (!string.Equals(keyValuePair.Key, "PopupPanel", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "HideScript", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "timeStamp", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "action", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "max", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "unum", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "isRedirect", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "IsARm", StringComparison.OrdinalIgnoreCase) && !string.Equals(keyValuePair.Key, "ReportIDParamKey", StringComparison.OrdinalIgnoreCase))
        str5 = $"{str5}{keyValuePair.Key}, ";
    }
    if (!string.IsNullOrEmpty(str5))
    {
      str5 = str5.Trim();
      if (str5.EndsWith(","))
        str5 = str5.Substring(0, str5.Length - 1);
    }
    if (!string.IsNullOrEmpty(str5))
      throw new PXException("The report does not contain parameters: {0}.", new object[1]
      {
        (object) str5
      });
  }
}
