// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportParameterAssigmentProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Reports;

#nullable disable
namespace PX.Api.Reports;

internal class ReportParameterAssigmentProcessor : CommandProcessor<PX.Api.Models.Field>
{
  internal const string ObjectName = "Parameters";
  private readonly ReportParameterCollection _settings;

  public ReportParameterAssigmentProcessor(ReportParameterCollection settings)
  {
    this._settings = settings;
  }

  public override bool CanExecute(Command cmd)
  {
    return this._settings != null && base.CanExecute(cmd) && string.Equals(cmd.ObjectName, "Parameters");
  }

  public override void Execute(PX.Api.Models.Field fieldCmd)
  {
    string str = FieldDecoder.UnpackValue(fieldCmd);
    this._settings.Assign(fieldCmd.FieldName, str);
  }
}
