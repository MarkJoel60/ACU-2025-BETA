// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Services.AutomationConditionEvaluator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Automation.Services;

internal class AutomationConditionEvaluator : IConditionEvaluator
{
  public bool Evaluate(PXGraph graph, PXView view, object row, IEnumerable<PXFilterRow> filters)
  {
    PXFilterRow[] array = filters.ToArray<PXFilterRow>();
    if (row == null)
      return false;
    if (array.Length == 0)
      return true;
    return this.MatchFilters(new List<object>() { row }, array, graph, view.BqlSelect);
  }

  public bool MatchFilters(
    List<object> list,
    PXFilterRow[] filters,
    PXGraph graph,
    BqlCommand command)
  {
    PXView view = new PXView(graph, true, command);
    view.prepareFilters(ref filters);
    return this.MatchFilters(list, filters, view);
  }

  private bool MatchFilters(List<object> list, PXFilterRow[] filters, PXView view)
  {
    List<object> list1 = list.ToList<object>();
    view.FilterResult(list1, filters);
    return list1.Any<object>();
  }
}
