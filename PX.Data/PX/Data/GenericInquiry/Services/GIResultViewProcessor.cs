// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.Services.GIResultViewProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.GenericInquiry.Services;

internal class GIResultViewProcessor : IGIResultViewProcessor
{
  private static readonly IReadOnlyDictionary<string, string> _emptyDict = (IReadOnlyDictionary<string, string>) new Dictionary<string, string>(0);

  public string GetGIRowStyle(
    PXGenericInqGrph graph,
    GenericResult row,
    IReadOnlyDictionary<string, string> warnings)
  {
    string giRowStyle = (string) null;
    if (!string.IsNullOrEmpty(graph.Design?.RowStyleFormula))
      giRowStyle = this.EvaluateStyleFormula(graph, row, graph.Design.RowStyleFormula);
    return giRowStyle;
  }

  public string GetGICellStyle(
    PXGenericInqGrph graph,
    GenericResult row,
    string rowStyle,
    string fieldFullName,
    IReadOnlyDictionary<string, string> warnings)
  {
    if (fieldFullName == null)
      throw new ArgumentNullException(nameof (fieldFullName));
    GIResult giResult = graph.ResultColumns.FirstOrDefault<GIResult>((Func<GIResult, bool>) (c => string.Equals(c.FieldName, fieldFullName, StringComparison.OrdinalIgnoreCase)));
    if (!string.IsNullOrEmpty(giResult?.StyleFormula))
    {
      string styleFormula = this.EvaluateStyleFormula(graph, row, giResult.StyleFormula);
      if (!string.IsNullOrEmpty(styleFormula))
        return styleFormula;
    }
    return rowStyle;
  }

  internal virtual string EvaluateStyleFormula(
    PXGenericInqGrph graph,
    GenericResult row,
    string formula)
  {
    return GIFormulaParser.Evaluate(graph, row, formula, true, false) as string;
  }

  public IReadOnlyDictionary<string, string> GetGIRowWarnings(
    PXGenericInqGrph graph,
    GenericResult row)
  {
    return GIResultViewProcessor._emptyDict;
  }

  public bool CanSort(PXGenericInqGrph graph, string fieldName, GIResult resultRow)
  {
    return resultRow != null && !graph.IsVirtualField(resultRow.FieldName) || graph.Results.Cache.PlainDacFields.Contains(fieldName);
  }
}
