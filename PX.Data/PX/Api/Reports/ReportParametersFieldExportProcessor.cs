// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportParametersFieldExportProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Reports;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.Reports;

internal class ReportParametersFieldExportProcessor : ReportParametersContainerExportProcessor
{
  private readonly Content _exportSchema;
  private readonly ReportParameterCollection _reportParameters;

  public ReportParametersFieldExportProcessor(
    Content exportSchema,
    ReportParameterCollection reportParameters)
  {
    this._exportSchema = exportSchema;
    this._reportParameters = reportParameters;
  }

  protected override Container GetContainer(PX.Api.Models.Field cmd)
  {
    return ((IEnumerable<Container>) this._exportSchema.Containers).First<Container>((Func<Container, bool>) (c => c.Name == cmd.ObjectName));
  }

  public override string GetValue(PX.Api.Models.Field cmd)
  {
    return this._reportParameters[cmd.FieldName].Value as string;
  }

  public override bool CanExecute(Command cmd)
  {
    return base.CanExecute(cmd) && string.Equals(cmd.ObjectName, "Parameters");
  }
}
