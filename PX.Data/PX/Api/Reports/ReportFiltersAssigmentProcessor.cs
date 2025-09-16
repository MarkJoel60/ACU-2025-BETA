// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportFiltersAssigmentProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Reports;
using System;
using System.Linq;

#nullable disable
namespace PX.Api.Reports;

internal class ReportFiltersAssigmentProcessor(Func<FilterExp> targetProvider) : 
  ReportCollectionItemAssignProcessor<FilterExp>(targetProvider)
{
  public override void Execute(PX.Api.Models.Field cmd)
  {
    this.TryChangeLabelToValue(cmd);
    base.Execute(cmd);
  }

  private void TryChangeLabelToValue(PX.Api.Models.Field cmd)
  {
    if (cmd.FieldName != "Value" && cmd.FieldName != "Value2" || string.IsNullOrEmpty(this.CollectionItem.DataField) || !this.CollectionItem.DataField.Contains<char>('.'))
      return;
    string[] strArray = this.CollectionItem.DataField.Split('.');
    if (strArray.Length != 2)
      return;
    string dacName = strArray[0];
    string dacProperty = strArray[1];
    if (dacName == null)
      return;
    cmd.Value = FieldDecoder.PackValue(DacPropertyValueToLabelTransmitter.GetValue(FieldDecoder.UnpackValue(cmd), dacName, dacProperty));
  }
}
