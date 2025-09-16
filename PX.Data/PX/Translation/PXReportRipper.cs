// Decompiled with JetBrains decompiler
// Type: PX.Translation.PXReportRipper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Parser;
using PX.Data;
using PX.Data.LocalizationKeyGenerators;
using PX.Data.Reports;
using PX.Reports;
using PX.Reports.Charting;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.Reports.Parser;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace PX.Translation;

/// <exclude />
internal class PXReportRipper : PXRipper
{
  public const string COLLECTION_RESOURCES_KEY = "ReportRipperCollectingResources";

  protected override void OnRipStart(List<string> processed, ResourceCollection result)
  {
    foreach (UserReport userReport in PXSelectBase<UserReport, PXSelect<UserReport>.Config>.Select(new PXGraph()).FirstTableItems.OrderByDescending<UserReport, bool?>((Func<UserReport, bool?>) (r => r.IsActive)).ToArray<UserReport>())
    {
      if (!processed.Contains(userReport.ReportFileName))
      {
        Report report = new Report();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load((TextReader) new StringReader(userReport.Xml));
        try
        {
          report.LoadFromXml(xmlDocument);
          if (report.Localizable)
          {
            this.RipItems(((ReportItem) report).Items, result, userReport.ReportFileName);
            this.RipParameters(report.Parameters, result, userReport.ReportFileName);
            Dictionary<string, ReportVariable> variables = report.GetVariables();
            if (variables != null)
              this.RipVariables((IEnumerable<ReportVariable>) variables.Values, report, result, userReport.ReportFileName);
            try
            {
              this.RipNonUIFields(report, result);
            }
            catch
            {
            }
          }
          processed.Add(userReport.ReportFileName);
        }
        catch
        {
        }
      }
    }
  }

  protected void RipNonUIFields(Report rep, ResourceCollection result)
  {
    PXContext.SetSlot<ResourceCollection>("ReportRipperCollectingResources", result);
    ICollection<object> viewerFields = rep.GetViewerFields();
    if (viewerFields.Count <= 0)
      return;
    SoapNavigator.FilterDBFields(viewerFields, new Dictionary<string, string>(), new ReportSelectArguments()
    {
      Tables = rep.Tables,
      Relations = rep.Relations
    });
  }

  protected override void Rip(
    System.IO.FileInfo file,
    List<string> processed,
    ResourceCollection result,
    LocalizationTranslationSetItem item = null,
    TranslationSetMaint graph = null,
    bool standalone = false)
  {
    if (file == null)
      return;
    if (processed.Contains(file.Name))
      return;
    try
    {
      Report report = new Report();
      report.LoadFromFile(file.FullName);
      if (report.Localizable)
      {
        this.RipItems(((ReportItem) report).Items, result, file.Name);
        this.RipParameters(report.Parameters, result, file.Name);
        this.RipMailSettings(report.MailSettings, report, result, file.Name);
        this.RipExportFileName(report, result, file.Name);
        Dictionary<string, ReportVariable> variables = report.GetVariables();
        if (variables != null)
          this.RipVariables((IEnumerable<ReportVariable>) variables.Values, report, result, file.Name);
        try
        {
          this.RipNonUIFields(report, result);
        }
        catch
        {
        }
      }
      processed.Add(file.Name);
    }
    catch
    {
    }
  }

  private void RipItems(ReportItemCollection items, ResourceCollection result, string fileName)
  {
    if (items == null)
      return;
    foreach (ReportItem reportItem in (CollectionBase) items)
    {
      this.RipItems(reportItem.Items, result, fileName);
      if (reportItem is TextBox)
        this.RipTextBox(reportItem as TextBox, fileName, result);
      else if (reportItem is Chart)
        this.RipChart(reportItem as Chart, fileName, result);
    }
  }

  private void RipChart(Chart chart, string fileName, ResourceCollection result)
  {
    if (chart == null || string.IsNullOrEmpty(fileName) || result == null)
      return;
    for (int index = 0; index < ((List<MSChartSeries>) chart.Series).Count; ++index)
    {
      string name = ((List<MSChartSeries>) chart.Series)[index].Name;
      if (!string.IsNullOrEmpty(name))
      {
        string itemLocalizationKey = PXReportItemKeyGenerator.GetSeriesItemLocalizationKey(fileName, index);
        result.AddResource(new LocalizationResourceLite(itemLocalizationKey, LocalizationResourceType.ChartName, name));
      }
    }
  }

  private void RipTextBox(TextBox textBox, string fileName, ResourceCollection result)
  {
    if (textBox == null || !textBox.Localizable || string.IsNullOrEmpty(textBox.Value) || string.IsNullOrEmpty(fileName) || result == null)
      return;
    if (!textBox.Value.StartsWith("="))
    {
      string boxLocalizationKey = PXReportItemKeyGenerator.GetTextBoxLocalizationKey(fileName, ((ReportItem) textBox).Name);
      result.AddResource(new LocalizationResourceLite(boxLocalizationKey, LocalizationResourceType.TextBoxValue, textBox.Value));
    }
    else
      this.RipFormulaConstants(textBox.Value, ((ReportItem) textBox).Name, ((ReportItem) textBox).Report, fileName, result);
  }

  private void RipVariables(
    IEnumerable<ReportVariable> variables,
    Report report,
    ResourceCollection result,
    string fileName)
  {
    if (variables == null || result == null || string.IsNullOrEmpty(fileName))
      return;
    foreach (ReportVariable reportVariable in variables.Where<ReportVariable>((Func<ReportVariable, bool>) (v => v.ValueExpr != null && v.ValueExpr.StartsWith("="))))
      this.RipFormulaConstants(reportVariable.ValueExpr, reportVariable.Name, report, fileName, result);
  }

  private void RipParameters(
    ReportParameterCollection parameters,
    ResourceCollection result,
    string fileName)
  {
    if (parameters == null || result == null || string.IsNullOrEmpty(fileName))
      return;
    foreach (ReportParameter parameter in (List<ReportParameter>) parameters)
    {
      this.RipPrompt(parameter, fileName, result);
      this.RipValidValues(parameter, fileName, result);
    }
  }

  private void RipMailSettings(
    MailSettings mailSettings,
    Report report,
    ResourceCollection result,
    string fileName)
  {
    if (mailSettings == null || result == null || string.IsNullOrEmpty(fileName))
      return;
    if (!string.IsNullOrEmpty(mailSettings.Subject))
    {
      if (!mailSettings.Subject.StartsWith("="))
      {
        string settingsLocalizationKey = PXReportItemKeyGenerator.GetMailSettingsLocalizationKey(fileName, "MailSettingsSubject");
        result.AddResource(new LocalizationResourceLite(settingsLocalizationKey, LocalizationResourceType.MailSettings, mailSettings.Subject));
      }
      else
        this.RipFormulaConstants(mailSettings.Subject, "MailSettingsSubject", report, fileName, result);
    }
    if (string.IsNullOrEmpty(mailSettings.Body))
      return;
    if (!mailSettings.Body.StartsWith("="))
    {
      string settingsLocalizationKey = PXReportItemKeyGenerator.GetMailSettingsLocalizationKey(fileName, "MailSettingsBody");
      result.AddResource(new LocalizationResourceLite(settingsLocalizationKey, LocalizationResourceType.MailSettings, mailSettings.Body));
    }
    else
      this.RipFormulaConstants(mailSettings.Body, "MailSettingsBody", report, fileName, result);
  }

  private void RipExportFileName(Report report, ResourceCollection result, string fileName)
  {
    if (string.IsNullOrEmpty(report.ExportFileName) || result == null || string.IsNullOrEmpty(fileName))
      return;
    if (!report.ExportFileName.StartsWith("="))
    {
      string nameLocalizationKey = PXReportItemKeyGenerator.GetExportFileNameLocalizationKey(fileName, "ExportFileName");
      result.AddResource(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.ExportFileName, report.ExportFileName));
    }
    else
      this.RipFormulaConstants(report.ExportFileName, "ExportFileName", report, fileName, result);
  }

  private void RipPrompt(ReportParameter parameter, string fileName, ResourceCollection result)
  {
    if (parameter == null || string.IsNullOrEmpty(parameter.Prompt) || string.IsNullOrEmpty(fileName) || result == null)
      return;
    string promptLocalizationKey = PXReportItemKeyGenerator.GetPromptLocalizationKey(fileName, parameter.Name);
    result.AddResource(new LocalizationResourceLite(promptLocalizationKey, LocalizationResourceType.Prompt, parameter.Prompt));
  }

  private void RipValidValues(
    ReportParameter parameter,
    string fileName,
    ResourceCollection result)
  {
    if (parameter == null || parameter.ValidValues == null || ((List<ParameterValue>) parameter.ValidValues).Count <= 0 || string.IsNullOrEmpty(fileName) || result == null)
      return;
    foreach (ParameterValue validValue in (List<ParameterValue>) parameter.ValidValues)
    {
      string valueLocalizationKey = PXReportItemKeyGenerator.GetValidValueLocalizationKey(fileName, parameter.Name, validValue.Value);
      result.AddResource(new LocalizationResourceLite(valueLocalizationKey, LocalizationResourceType.ValidValue, validValue.Label));
    }
  }

  private void RipFormulaConstants(
    string expr,
    string name,
    Report report,
    string fileName,
    ResourceCollection result)
  {
    if (result == null)
      return;
    try
    {
      foreach (ConstantNode constantNode in ConstantNode.FindConstantNodes(ReportExprParser.Parse(expr.Substring(1), report)))
      {
        if (constantNode.Type == 3 && !string.IsNullOrEmpty(constantNode.Value as string))
        {
          string str = (string) constantNode.Value;
          if (!str.StartsWith("$") && !str.StartsWith("@"))
          {
            string constantLocalizationKey = PXReportItemKeyGenerator.GetFormulaConstantLocalizationKey(fileName, name);
            result.AddResource(new LocalizationResourceLite(constantLocalizationKey, LocalizationResourceType.ConstantInFormula, constantNode.Value as string));
          }
        }
      }
    }
    catch (ParserException ex)
    {
    }
  }
}
