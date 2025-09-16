// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportMailAndCommonSettingsExportProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Reports.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Api.Reports;

internal class ReportMailAndCommonSettingsExportProcessor : CommandProcessor<PX.Api.Models.Field>
{
  private readonly Report _report;
  private readonly Content _exportSchema;
  private readonly string _commonSettings = "CommonSettings";
  private readonly string _mailSettings = "MailSettings";

  public ReportMailAndCommonSettingsExportProcessor(Content exportSchema, Report report)
  {
    this._exportSchema = exportSchema;
    this._report = report;
  }

  public override bool CanExecute(Command cmd)
  {
    if (!base.CanExecute(cmd))
      return false;
    return string.Equals(cmd.ObjectName, this._commonSettings) || string.Equals(cmd.ObjectName, this._mailSettings);
  }

  public override void Execute(PX.Api.Models.Field cmd)
  {
    ((IEnumerable<PX.Api.Models.Field>) ((IEnumerable<Container>) this._exportSchema.Containers).First<Container>((Func<Container, bool>) (schemaContainer => schemaContainer.Name == cmd.ObjectName)).Fields).First<PX.Api.Models.Field>((Func<PX.Api.Models.Field, bool>) (field => field.FieldName == cmd.FieldName)).Value = ReportMailAndCommonSettingsExportProcessor.GetProperty(ReportMailAndCommonSettingsExportProcessor.GetProperty((object) this._report, cmd.ObjectName), cmd.FieldName).ToString();
  }

  private static object GetProperty(object obj, string property)
  {
    PropertyInfo property1 = obj.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public);
    return property1 != (PropertyInfo) null ? property1.GetValue(obj, (object[]) null) : (object) null;
  }
}
