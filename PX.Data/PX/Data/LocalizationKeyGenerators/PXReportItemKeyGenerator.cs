// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.PXReportItemKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
public static class PXReportItemKeyGenerator
{
  private const string PROMPT_POSTFIX = ".Prompt";
  private const string VALIDVALUE_POSTFIX = ".ValidValue";
  private const string TEXTBOX_POSTFIX = ".Value";
  private const string CHARTSERIES_POSTFIX = ".Name";
  private const string CHARTSERIES_PREFIX = "MSChartSeries";
  private const string FORMULACONSTANT_POSTFIX = ".ConstantInFormula";
  private const string MAILSETTINGS_POSTFIX = ".MailSettings";
  private const string EXPORTFILENAME_POSTFIX = ".ExportFileName";

  public static string GetSeriesItemLocalizationKey(string reportName, int itemIndex)
  {
    return $"{reportName} {"MSChartSeries"}{itemIndex}{".Name"}";
  }

  public static string GetTextBoxLocalizationKey(string reportName, string textBoxName)
  {
    return $"{reportName} {textBoxName}{".Value"}";
  }

  public static string GetValidValueLocalizationKey(
    string reportName,
    string parameterName,
    string value)
  {
    return $"{reportName} {parameterName}{".ValidValue"} {value}";
  }

  public static string GetPromptLocalizationKey(string reportName, string parameterName)
  {
    return $"{reportName} {parameterName}{".Prompt"}";
  }

  public static string GetMailSettingsLocalizationKey(string reportName, string fieldName)
  {
    return $"{reportName} {fieldName}{".MailSettings"}";
  }

  public static string GetExportFileNameLocalizationKey(string reportName, string fieldName)
  {
    return $"{reportName} {fieldName}{".ExportFileName"}";
  }

  public static string GetFormulaConstantLocalizationKey(string reportName, string textBoxName)
  {
    return $"{reportName} {textBoxName}{".ConstantInFormula"}";
  }
}
