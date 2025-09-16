// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Services.INavigationExpressionEvaluator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation.Services;

internal interface INavigationExpressionEvaluator
{
  /// <summary>Evaluate expression.</summary>
  /// <param name="graph">Data graph.</param>
  /// <param name="row">Record.</param>
  /// <param name="expression">Expression for evaluating.</param>
  /// <param name="isFromSchema">If expression is used from schema.</param>
  /// <param name="useExt">If this parameter is true, cache.GetValueExt will be called instead of GetValue</param>
  /// <param name="applyMask">If this parameter is true result value will be formatted by PXFieldState input mask.</param>
  /// <returns></returns>
  object Evaluate(
    PXGraph graph,
    object row,
    string expression,
    bool? isFromSchema = null,
    bool useExt = true,
    bool applyMask = false);
}
