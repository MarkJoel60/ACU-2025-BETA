// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.TemplateAssigmentProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Reports;
using PX.Reports.Controls;
using System.Collections.Generic;

#nullable disable
namespace PX.Api.Reports;

internal class TemplateAssigmentProcessor : CommandProcessor<PX.Api.Models.Action>
{
  private static readonly string _templateSave = "TemplateSave";
  private static readonly string _templateRemove = "TemplateRemove";
  private static readonly string _templateSelect = "TemplateSelect";
  private string _screenId;
  private Report _report;
  private ScreenUtils.Template _template;
  private readonly SettingsProvider _settingsProvider;

  public TemplateAssigmentProcessor(
    ScreenUtils.Template template,
    Report report,
    string screenId,
    SettingsProvider settingsProvider)
  {
    this._screenId = screenId;
    this._report = report;
    this._template = template;
    this._settingsProvider = ExceptionExtensions.CheckIfNull<SettingsProvider>(settingsProvider, nameof (settingsProvider), (string) null);
  }

  public override bool CanExecute(Command cmd)
  {
    if (!base.CanExecute(cmd))
      return false;
    return string.Equals(cmd.FieldName, TemplateAssigmentProcessor._templateSave) || string.Equals(cmd.FieldName, TemplateAssigmentProcessor._templateRemove) || string.Equals(cmd.FieldName, TemplateAssigmentProcessor._templateSelect);
  }

  public override void Execute(PX.Api.Models.Action fieldCmd)
  {
    if (string.Equals(fieldCmd.FieldName, TemplateAssigmentProcessor._templateRemove) && !string.IsNullOrEmpty(this._template.Name) && !string.IsNullOrEmpty(this._screenId))
      this._settingsProvider.Delete(this._template.Name, this._screenId);
    if (string.Equals(fieldCmd.FieldName, TemplateAssigmentProcessor._templateSave))
      this._settingsProvider.Save(new PXReportSettings(this._template.Name, this._report.CommonSettings, ScreenUtils.ConvertMailSettings(this._report.MailSettings), this._report.Parameters, this._report.DynamicSorting, this._report.DynamicFilters, (FilterExpCollection) null)
      {
        IsDefault = this._template.IsDefault,
        IsShared = this._template.IsShared
      }, this._screenId);
    if (!string.Equals(fieldCmd.FieldName, TemplateAssigmentProcessor._templateSelect) || this._template.Name == null)
      return;
    PXReportSettings pxReportSettings = this._settingsProvider.Read(this._template.Name, PXAccess.GetUserName(), this._screenId);
    if (pxReportSettings == null)
      return;
    pxReportSettings.ParameterValues.UpdateParameters(this._report.Parameters);
    ((Settings) pxReportSettings.CommonSettings).UpdateParameters((Settings) this._report.CommonSettings);
    ((Settings) pxReportSettings.Mail).UpdateParameters((Settings) ScreenUtils.ConvertMailSettings(this._report.MailSettings));
    ((List<SortExp>) this._report.DynamicSorting).Clear();
    ((List<FilterExp>) this._report.DynamicFilters).Clear();
    ((List<SortExp>) this._report.DynamicSorting).AddRange((IEnumerable<SortExp>) pxReportSettings.Sorting);
    ((List<FilterExp>) this._report.DynamicFilters).AddRange((IEnumerable<FilterExp>) pxReportSettings.Filters);
  }
}
