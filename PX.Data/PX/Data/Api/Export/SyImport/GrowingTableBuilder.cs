// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.GrowingTableBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Export.SyImport;

internal class GrowingTableBuilder
{
  public static IGrowingTable Create(PXGraph graph, SyExportContext context)
  {
    IEnumerable<string> viewNamesOrder = GrowingTableBuilder.GetViewNamesOrder(context);
    Tuple<string, string>[] array1 = EnumerableExtensions.Distinct<SyCommand, Tuple<string, string>>(((IEnumerable<SyCommand>) context.Commands).Where<SyCommand>((Func<SyCommand, bool>) (c => c.CommandType == SyCommandType.EnumFieldValues)), (Func<SyCommand, Tuple<string, string>>) (c => Tuple.Create<string, string>(c.ViewAlias, c.Field))).Select<SyCommand, Tuple<string, string>>((Func<SyCommand, Tuple<string, string>>) (c => Tuple.Create<string, string>(SyExportContext.GetViewId(c), c.View))).ToArray<Tuple<string, string>>();
    string[] array2 = graph.Views.Keys.ToArray<string>();
    string[] array3 = viewNamesOrder.ToArray<string>();
    HashSet<string> viewNamesSet = new HashSet<string>((IEnumerable<string>) array3);
    Dictionary<System.Type, string[]> graphViewsByCacheType = ((IEnumerable<string>) array2).Select(c => new
    {
      cacheType = graph.Views[c].GetItemType(),
      viewName = c
    }).GroupBy(c => c.cacheType).ToDictionary<IGrouping<System.Type, \u003C\u003Ef__AnonymousType11<System.Type, string>>, System.Type, string[]>(c => c.Key, c => c.Select(v => v.viewName).ToArray<string>());
    Dictionary<string, string[]> viewParents = new Dictionary<string, string[]>();
    bool flag = true;
    foreach (string str in array3)
    {
      if (!viewParents.ContainsKey(str))
      {
        PXView pxView;
        if (!graph.Views.TryGetOrCreateValue(SyMappingUtils.CleanViewName(str), out pxView))
        {
          PXTrace.WithStack().Information<string>("Export optimization failed because view {View} does not exist", str);
          flag = false;
        }
        else
        {
          IEnumerable<System.Type> dependToCacheTypes = pxView.GetDependToCacheTypes();
          if (dependToCacheTypes == null)
          {
            PXTrace.WithStack().Information<string>("Export optimization failed due to Bql delegate on view {View}", str);
            flag = false;
          }
          else
          {
            string[] strArray;
            string[] array4 = dependToCacheTypes.SelectMany<System.Type, string>((Func<System.Type, IEnumerable<string>>) (c => !graphViewsByCacheType.TryGetValue(c, out strArray) ? Enumerable.Empty<string>() : (IEnumerable<string>) strArray)).Where<string>((Func<string, bool>) (c => viewNamesSet.Contains(c))).ToArray<string>();
            if (array4.Length != 0)
              viewParents[str] = array4;
          }
        }
      }
    }
    return flag ? (IGrowingTable) new OptimizedGrowingTable((IEnumerable<string>) array3, array1, viewParents, graph.PrimaryView, new HashSet<string>(GrowingTableBuilder.GetViewAliasesImpl(graph, graph.PrimaryView, (IEnumerable<string>) array3))) : (IGrowingTable) new GrowingTable();
  }

  private static IEnumerable<string> GetViewNamesOrder(SyExportContext context)
  {
    return ((IEnumerable<string>) new string[1]
    {
      context.PrimaryView
    }).Concat<string>(((IEnumerable<SyCommand>) context.Commands).Select<SyCommand, string>((Func<SyCommand, string>) (c => c.ViewAlias))).Distinct<string>();
  }

  private static IEnumerable<string> GetViewAliasesImpl(
    PXGraph screenGraph,
    string primaryView,
    IEnumerable<string> views)
  {
    System.Type primaryViewCacheType = screenGraph.Views[primaryView].GetItemType();
    return screenGraph.Views.Where<KeyValuePair<string, PXView>>((Func<KeyValuePair<string, PXView>, bool>) (c => views.Contains<string>(c.Key))).Where<KeyValuePair<string, PXView>>((Func<KeyValuePair<string, PXView>, bool>) (c => c.Key != primaryView && c.Value.GetItemType() == primaryViewCacheType)).Select<KeyValuePair<string, PXView>, string>((Func<KeyValuePair<string, PXView>, string>) (c => c.Key));
  }
}
