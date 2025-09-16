// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportParametersContainerExportProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.Reports;

internal abstract class ReportParametersContainerExportProcessor : CommandProcessor<PX.Api.Models.Field>
{
  protected abstract Container GetContainer(PX.Api.Models.Field cmd);

  public virtual string GetValue(PX.Api.Models.Field cmd) => cmd.Value;

  public override void Execute(PX.Api.Models.Field fieldCmd)
  {
    ((IEnumerable<PX.Api.Models.Field>) this.GetContainer(fieldCmd).Fields).First<PX.Api.Models.Field>((Func<PX.Api.Models.Field, bool>) (f => f.FieldName == fieldCmd.FieldName)).Value = this.GetValue(fieldCmd);
  }
}
