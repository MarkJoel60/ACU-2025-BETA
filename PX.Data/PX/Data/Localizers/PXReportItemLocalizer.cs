// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.PXReportItemLocalizer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Parser;
using PX.Data.LocalizationKeyGenerators;
using PX.Reports;
using PX.Reports.Charting;
using PX.Reports.Controls;
using PX.Reports.Parser;
using PX.Translation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Localizers;

/// <exclude />
public class PXReportItemLocalizer : IPXObjectLocalizer<Report>, IPXObjectLocalizer
{
  public const string FORMULA_CONSTANT_BEGINNING = "=";
  public const string FORBIDDEN_CONSTANT_BEGINNING_1 = "$";
  public const string FORBIDDEN_CONSTANT_BEGINNING_2 = "@";

  public void Localize(Report localizableObject)
  {
    if (localizableObject == null)
      return;
    if (localizableObject.Localizable)
      this.LocalizeItems(((ReportItem) localizableObject).Items, localizableObject.ReportName);
    this.LocalizeParameters(localizableObject);
    this.LocalizeMailSettings(localizableObject);
  }

  private void LocalizeItems(ReportItemCollection reportItems, string reportName)
  {
    if (reportItems == null || string.IsNullOrEmpty(reportName))
      return;
    foreach (ReportItem reportItem in (CollectionBase) reportItems)
    {
      this.LocalizeItems(reportItem.Items, reportName);
      switch (reportItem)
      {
        case SubReport _:
          this.LocalizeSubReport(reportItem as SubReport);
          continue;
        case TextBox _:
          this.LocalizeTextBox(reportItem as TextBox, reportName);
          continue;
        case Chart _:
          this.LocalizeChart(reportItem as Chart, reportName);
          continue;
        default:
          continue;
      }
    }
  }

  private void LocalizeChart(Chart chart, string reportName)
  {
    if (chart == null || chart.Series == null || ((List<MSChartSeries>) chart.Series).Count <= 0 || string.IsNullOrEmpty(reportName))
      return;
    for (int index = 0; index < ((List<MSChartSeries>) chart.Series).Count; ++index)
    {
      if (!string.IsNullOrEmpty(((List<MSChartSeries>) chart.Series)[index].Name))
      {
        string itemLocalizationKey = PXReportItemKeyGenerator.GetSeriesItemLocalizationKey(reportName, index);
        ((List<MSChartSeries>) chart.Series)[index].Name = PXReportItemLocalizer.LocalizeWithoutBR(((List<MSChartSeries>) chart.Series)[index].Name, itemLocalizationKey);
      }
    }
  }

  private void LocalizeTextBox(TextBox textBox, string reportName)
  {
    if (textBox == null || !textBox.Localizable || string.IsNullOrEmpty(textBox.Value))
      return;
    if (!textBox.Value.StartsWith("="))
    {
      string boxLocalizationKey = PXReportItemKeyGenerator.GetTextBoxLocalizationKey(reportName, ((ReportItem) textBox).Name);
      textBox.Value = PXReportItemLocalizer.LocalizeWithoutBR(textBox.Value, boxLocalizationKey);
    }
    else
      this.LocalizeFormulaConstants(textBox, reportName);
  }

  public string LocalizeExportFileName(Report localizableObject)
  {
    if (!string.IsNullOrEmpty(localizableObject.ExportFileName))
    {
      if (!localizableObject.ExportFileName.StartsWith("="))
      {
        string nameLocalizationKey = PXReportItemKeyGenerator.GetExportFileNameLocalizationKey(localizableObject.ReportName, "ExportFileName");
        localizableObject.ExportFileName = PXLocalizer.Localize(localizableObject.ExportFileName, nameLocalizationKey);
      }
      else
      {
        ExpressionNode node = ReportExprParser.Parse(localizableObject.ExportFileName.Substring(1), localizableObject);
        localizableObject.ExportFileName = this.GetLocalizeProperty(node, localizableObject, "ExportFileName");
      }
    }
    return localizableObject.ExportFileName;
  }

  private void LocalizeMailSettings(Report localizableObject)
  {
    if (localizableObject.MailSettings == null)
      return;
    if (!string.IsNullOrEmpty(localizableObject.MailSettings.Subject) && !localizableObject.MailSettings.Subject.StartsWith("="))
    {
      string settingsLocalizationKey = PXReportItemKeyGenerator.GetMailSettingsLocalizationKey(localizableObject.ReportName, "MailSettingsSubject");
      localizableObject.MailSettings.Subject = PXReportItemLocalizer.LocalizeWithoutBR(localizableObject.MailSettings.Subject, settingsLocalizationKey);
    }
    if (string.IsNullOrEmpty(localizableObject.MailSettings.Body) || localizableObject.MailSettings.Body.StartsWith("="))
      return;
    string settingsLocalizationKey1 = PXReportItemKeyGenerator.GetMailSettingsLocalizationKey(localizableObject.ReportName, "MailSettingsBody");
    localizableObject.MailSettings.Body = PXLocalizer.Localize(localizableObject.MailSettings.Body, settingsLocalizationKey1);
  }

  private void LocalizeFormulaConstants(TextBox textBox, string reportName)
  {
  }

  private void LocalizeSubReport(SubReport subReport)
  {
    if (subReport == null || subReport.ChildReport == null)
      return;
    this.LocalizeItems(((ReportItem) subReport.ChildReport).Items, subReport.ChildReport.ReportName);
    this.LocalizeParameters(subReport.ChildReport);
  }

  private void LocalizeParameters(Report localizableObject)
  {
    if (localizableObject == null || localizableObject.Parameters == null)
      return;
    foreach (ReportParameter parameter in (List<ReportParameter>) localizableObject.Parameters)
    {
      this.LocalizePrompt(parameter, localizableObject.ReportName);
      this.LocalizeValidValues(parameter, localizableObject.ReportName);
    }
  }

  private void LocalizePrompt(ReportParameter reportParameter, string reportName)
  {
    if (reportParameter == null || string.IsNullOrEmpty(reportParameter.Prompt) || string.IsNullOrEmpty(reportName))
      return;
    string promptLocalizationKey = PXReportItemKeyGenerator.GetPromptLocalizationKey(reportName, reportParameter.Name);
    reportParameter.Prompt = PXReportItemLocalizer.LocalizeWithoutBR(reportParameter.Prompt, promptLocalizationKey);
  }

  private void LocalizeValidValues(ReportParameter reportParameter, string reportName)
  {
    if (reportParameter == null || reportParameter.ValidValues == null || ((List<ParameterValue>) reportParameter.ValidValues).Count <= 0 || string.IsNullOrEmpty(reportName))
      return;
    foreach (ParameterValue validValue in (List<ParameterValue>) reportParameter.ValidValues)
    {
      string valueLocalizationKey = PXReportItemKeyGenerator.GetValidValueLocalizationKey(reportName, reportParameter.Name, validValue.Value);
      validValue.Label = PXReportItemLocalizer.LocalizeWithoutBR(validValue.Label, valueLocalizationKey);
    }
  }

  public string GetLocalizeProperty(ExpressionNode node, Report report, string propertyName)
  {
    string localizeProperty = report.ExportFileName;
    if (node != null && report != null && !string.IsNullOrEmpty(propertyName))
    {
      foreach (ConstantNode constantNode in ConstantNode.FindConstantNodes(node))
      {
        if (constantNode.Type == 3 && !string.IsNullOrEmpty(constantNode.Value as string))
        {
          string str = constantNode.NonLocalizedText ?? (constantNode.NonLocalizedText = (string) constantNode.Value);
          if (!str.StartsWith("$") && !str.StartsWith("@"))
          {
            string constantLocalizationKey = PXReportItemKeyGenerator.GetFormulaConstantLocalizationKey(report.ReportName, propertyName);
            string newValue = PXReportItemLocalizer.LocalizeWithoutBR(str, constantLocalizationKey);
            localizeProperty = localizeProperty.Replace(str, newValue);
          }
        }
      }
    }
    return localizeProperty;
  }

  public void LocalizeFormulaConstants(ExpressionNode node, string reportName, string textBoxName)
  {
    if (node == null || string.IsNullOrEmpty(reportName) || string.IsNullOrEmpty(textBoxName))
      return;
    foreach (ConstantNode constantNode in ConstantNode.FindConstantNodes(node))
    {
      if (constantNode.Type == 3 && !string.IsNullOrEmpty(constantNode.Value as string))
      {
        string message = constantNode.NonLocalizedText ?? (constantNode.NonLocalizedText = (string) constantNode.Value);
        if (!message.StartsWith("$") && !message.StartsWith("@"))
        {
          string constantLocalizationKey = PXReportItemKeyGenerator.GetFormulaConstantLocalizationKey(reportName, textBoxName);
          constantNode.Value = (object) PXReportItemLocalizer.LocalizeWithoutBR(message, constantLocalizationKey);
        }
      }
    }
  }

  public static void LocalizeReportField(
    string fieldName,
    ref string displayName,
    PXCache cache,
    bool checkTranslation = true)
  {
    PXUIFieldAttribute uiField = cache.GetAttributes(fieldName).OfType<PXUIFieldAttribute>().FirstOrDefault<PXUIFieldAttribute>();
    if (uiField != null)
    {
      if (!checkTranslation || PXContext.GetSlot<ResourceCollection>("ReportRipperCollectingResources") != null || !TranslationValidationManager.ValidateCurrentLocale())
        return;
      PXLocalizerRepository.UIFieldLocalizer.ValidateTranslation(uiField, cache.BqlTable.FullName, cache, uiField.GetFieldSourceType(cache));
    }
    else
      PXReportItemLocalizer.LocalizeNonUIField(ref displayName, cache, checkTranslation);
  }

  public static void LocalizeNonUIField(ref string fieldName, PXCache cache, bool checkTranslation)
  {
    string empty = string.Empty;
    if (string.IsNullOrEmpty(fieldName) || cache == null)
      return;
    ResourceCollection slot = PXContext.GetSlot<ResourceCollection>("ReportRipperCollectingResources");
    if (slot != null)
    {
      string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(cache.GetItemType().FullName);
      slot.AddResource(new LocalizationResourceLite(nameLocalizationKey, LocalizationResourceType.NonUIField, fieldName));
    }
    else
    {
      string nameLocalizationKey = PXUIFieldKeyGenerator.GetDacFieldNameLocalizationKey(cache.GetItemType().FullName);
      bool isTranslated;
      string str = PXLocalizer.Localize(fieldName, nameLocalizationKey, out isTranslated);
      if (!isTranslated & checkTranslation)
        TranslationValidationManager.AddWarning($"DataField '{cache.Graph.GetType().Name}::{cache.BqlTable.Name}::{fieldName}'");
      else
        fieldName = str;
    }
  }

  private static string LocalizeWithoutBR(string message, string key)
  {
    if (string.IsNullOrEmpty(message))
      return message;
    message = message.Replace("~", "#126;");
    return PXLocalizer.Localize(message, key).Replace("#126;", "~");
  }
}
