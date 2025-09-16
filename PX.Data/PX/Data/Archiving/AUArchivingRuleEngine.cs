// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.AUArchivingRuleEngine
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Archiving;

public class AUArchivingRuleEngine : IAUArchivingRuleEngine
{
  public const string ARCHIVING_RULES = "ArchivingRules";

  public IReadOnlyDictionary<System.Type, Func<IEnumerable<IBqlTable>>> GetRecordsExtractors(
    PXGraph graph,
    IBqlTable primaryRow,
    bool filterOutPreserved)
  {
    Dictionary<System.Type, Func<IEnumerable<IBqlTable>>> recordsExtractors = new Dictionary<System.Type, Func<IEnumerable<IBqlTable>>>();
    if (graph == null || primaryRow == null || !graph.Caches[primaryRow.GetType()].IsKeysFilled((object) primaryRow))
      return (IReadOnlyDictionary<System.Type, Func<IEnumerable<IBqlTable>>>) recordsExtractors;
    System.Type primaryType = primaryRow.GetType();
    foreach (Readonly.ArchivingRule rule1 in AUArchivingRuleEngine.Slot.GetRules(primaryRow.GetType()))
    {
      Readonly.ArchivingRule rule = rule1;
      Readonly.ArchivingRule[] preservations = AUArchivingRuleEngine.Slot.GetPreservations(rule.Table).ToArray<Readonly.ArchivingRule>();
      Func<IEnumerable<IBqlTable>> func;
      switch (rule.ReferStrategy)
      {
        case ArchivingReferStrategy.Parent:
          func = !rule.IsParentToPrimary ? (Func<IEnumerable<IBqlTable>>) (() => TryFilter(PXParentAttribute.SelectChildren(graph.Caches[rule.Table], (object) primaryRow, primaryType).Cast<IBqlTable>())) : (Func<IEnumerable<IBqlTable>>) (() => TryFilter(EnumerableExtensions.AsSingleEnumerable<object>(PXParentAttribute.SelectParent(graph.Caches[primaryType], (object) primaryRow, rule.Table)).Cast<IBqlTable>()));
          break;
        case ArchivingReferStrategy.FK:
          IForeignKey fk = (IForeignKey) Activator.CreateInstance(rule.FK);
          func = !rule.IsParentToPrimary ? (Func<IEnumerable<IBqlTable>>) (() => TryFilter(fk.SelectChildren(graph, primaryRow))) : (Func<IEnumerable<IBqlTable>>) (() => TryFilter(EnumerableExtensions.AsSingleEnumerable<IBqlTable>(fk.FindParent(graph, primaryRow))));
          break;
        case ArchivingReferStrategy.Select:
          PXView select = new PXView(graph, true, BqlCommand.CreateInstance(rule.Select));
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          func = !rule.IsParentToPrimary ? (Func<IEnumerable<IBqlTable>>) (() => TryFilter(select.SelectMultiBound((object[]) new IBqlTable[1]
          {
            primaryRow
          }).Select<object, IBqlTable>(AUArchivingRuleEngine.\u003C\u003EO.\u003C0\u003E__UnwrapMain ?? (AUArchivingRuleEngine.\u003C\u003EO.\u003C0\u003E__UnwrapMain = new Func<object, IBqlTable>(PXResult.UnwrapMain))))) : (Func<IEnumerable<IBqlTable>>) (() => TryFilter(EnumerableExtensions.AsSingleEnumerable<IBqlTable>(PXResult.UnwrapMain(select.SelectSingleBound((object[]) new IBqlTable[1]
          {
            primaryRow
          })))));
          break;
        default:
          func = (Func<IEnumerable<IBqlTable>>) (() => (IEnumerable<IBqlTable>) Array.Empty<IBqlTable>());
          break;
      }
      recordsExtractors[rule.Table] = func;
    }
    return (IReadOnlyDictionary<System.Type, Func<IEnumerable<IBqlTable>>>) recordsExtractors;
    Readonly.ArchivingRule[] preservations1;
    // ISSUE: variable of a compiler-generated type
    AUArchivingRuleEngine.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10;

    IEnumerable<IBqlTable> TryFilter(IEnumerable<IBqlTable> rows)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return !cDisplayClass10.filterOutPreserved || !((IEnumerable<Readonly.ArchivingRule>) preservations1).Any<Readonly.ArchivingRule>() ? rows : cDisplayClass10.\u003C\u003E4__this.FilterOutPreserved(rows, (IEnumerable<Readonly.ArchivingRule>) preservations1, cDisplayClass10.graph, cDisplayClass10.primaryType, cDisplayClass10.primaryRow);
    }
  }

  private IEnumerable<IBqlTable> FilterOutPreserved(
    IEnumerable<IBqlTable> rows,
    IEnumerable<Readonly.ArchivingRule> preservations,
    PXGraph graph,
    System.Type primaryType,
    IBqlTable primaryRow)
  {
    foreach (IBqlTable row1 in rows)
    {
      IBqlTable row = row1;
      if (!preservations.Any<Readonly.ArchivingRule>((Func<Readonly.ArchivingRule, bool>) (p => this.IsPreserved(p, graph, row, primaryRow, primaryType))))
        yield return row;
    }
  }

  private bool IsPreserved(
    Readonly.ArchivingRule preservation,
    PXGraph graph,
    IBqlTable row,
    IBqlTable primaryRow,
    System.Type primaryType)
  {
    PXCache primaryCache = graph.Caches[preservation.Primary];
    if (preservation.IsParentToPrimary)
    {
      IEnumerable<object> source;
      switch (preservation.ReferStrategy)
      {
        case ArchivingReferStrategy.Parent:
          source = (IEnumerable<object>) PXParentAttribute.SelectChildren(primaryCache, (object) row, preservation.Table);
          break;
        case ArchivingReferStrategy.FK:
          source = (IEnumerable<object>) ((IForeignKey) Activator.CreateInstance(preservation.FK)).SelectChildren(graph, row);
          break;
        case ArchivingReferStrategy.Select:
          System.Type type = PXParentAttribute.Inverse(preservation.Select, preservation.Table, preservation.Primary);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          source = (IEnumerable<object>) new PXView(graph, true, BqlCommand.CreateInstance(type)).SelectMultiBound((object[]) new IBqlTable[1]
          {
            row
          }).Select<object, IBqlTable>(AUArchivingRuleEngine.\u003C\u003EO.\u003C0\u003E__UnwrapMain ?? (AUArchivingRuleEngine.\u003C\u003EO.\u003C0\u003E__UnwrapMain = new Func<object, IBqlTable>(PXResult.UnwrapMain)));
          break;
        default:
          source = (IEnumerable<object>) Array.Empty<object>();
          break;
      }
      if (source.Any<object>((Func<object, bool>) (c => primaryType != preservation.Primary || HasDifferentKeys(c))))
        return true;
    }
    else
    {
      object r;
      switch (preservation.ReferStrategy)
      {
        case ArchivingReferStrategy.Parent:
          r = PXParentAttribute.SelectParent(graph.Caches[preservation.Table], (object) row, preservation.Primary);
          break;
        case ArchivingReferStrategy.FK:
          r = (object) EnumerableExtensions.AsSingleEnumerable<IBqlTable>(((IForeignKey) Activator.CreateInstance(preservation.FK)).FindParent(graph, row));
          break;
        case ArchivingReferStrategy.Select:
          System.Type type = PXParentAttribute.Inverse(preservation.Select, preservation.Primary, preservation.Table);
          r = (object) PXResult.UnwrapMain(new PXView(graph, true, BqlCommand.CreateInstance(type)).SelectSingleBound((object[]) new IBqlTable[1]
          {
            row
          }));
          break;
        default:
          r = (object) null;
          break;
      }
      if (r != null && (primaryType != preservation.Primary || HasDifferentKeys(r)))
        return true;
    }
    return false;

    bool HasDifferentKeys(object r)
    {
      return !primaryCache.Keys.All<string>((Func<string, bool>) (key => object.Equals(primaryCache.GetValue(r, key), primaryCache.GetValue((object) primaryRow, key))));
    }
  }

  public class Slot : IPrefetchable, IPXCompanyDependent
  {
    private Dictionary<System.Type, Readonly.ArchivingRule[]> Rules;
    private Dictionary<System.Type, Readonly.ArchivingRule[]> Preserved;

    public void Prefetch()
    {
      Readonly.ArchivingRule[] array = PXSystemWorkflows.SelectTable<AUArchivingRule>().Select<AUArchivingRule, Readonly.ArchivingRule>((Func<AUArchivingRule, Readonly.ArchivingRule>) (r => Readonly.ArchivingRule.From(r))).ToArray<Readonly.ArchivingRule>();
      this.Rules = ((IEnumerable<Readonly.ArchivingRule>) array).GroupBy<Readonly.ArchivingRule, System.Type>((Func<Readonly.ArchivingRule, System.Type>) (r => r.Primary)).ToDictionary<IGrouping<System.Type, Readonly.ArchivingRule>, System.Type, Readonly.ArchivingRule[]>((Func<IGrouping<System.Type, Readonly.ArchivingRule>, System.Type>) (r => r.Key), (Func<IGrouping<System.Type, Readonly.ArchivingRule>, Readonly.ArchivingRule[]>) (r => r.ToArray<Readonly.ArchivingRule>()));
      this.Preserved = ((IEnumerable<Readonly.ArchivingRule>) array).GroupBy<Readonly.ArchivingRule, System.Type>((Func<Readonly.ArchivingRule, System.Type>) (r => r.Table)).Where<IGrouping<System.Type, Readonly.ArchivingRule>>((Func<IGrouping<System.Type, Readonly.ArchivingRule>, bool>) (g => g.Count<Readonly.ArchivingRule>() > 1)).ToDictionary<IGrouping<System.Type, Readonly.ArchivingRule>, System.Type, Readonly.ArchivingRule[]>((Func<IGrouping<System.Type, Readonly.ArchivingRule>, System.Type>) (r => r.Key), (Func<IGrouping<System.Type, Readonly.ArchivingRule>, Readonly.ArchivingRule[]>) (r => r.ToArray<Readonly.ArchivingRule>()));
    }

    public static AUArchivingRuleEngine.Slot GetSlot()
    {
      return PXDatabase.GetSlot<AUArchivingRuleEngine.Slot>("ArchivingRules", ((IEnumerable<System.Type>) PXSystemWorkflows.GetWorkflowDependedTypes()).Union<System.Type>((IEnumerable<System.Type>) new System.Type[2]
      {
        typeof (AUArchivingRule),
        typeof (PXGraph.FeaturesSet)
      }).ToArray<System.Type>());
    }

    public static AUArchivingRuleEngine.Slot LocallyCachedSlot
    {
      get
      {
        return PXContext.GetSlot<AUArchivingRuleEngine.Slot>("ArchivingRules") ?? PXContext.SetSlot<AUArchivingRuleEngine.Slot>("ArchivingRules", AUArchivingRuleEngine.Slot.GetSlot());
      }
    }

    public static IEnumerable<Readonly.ArchivingRule> GetRules(System.Type primaryType)
    {
      Readonly.ArchivingRule[] archivingRuleArray;
      return AUArchivingRuleEngine.Slot.LocallyCachedSlot.Rules.TryGetValue(primaryType, out archivingRuleArray) ? (IEnumerable<Readonly.ArchivingRule>) archivingRuleArray : (IEnumerable<Readonly.ArchivingRule>) Array.Empty<Readonly.ArchivingRule>();
    }

    public static IEnumerable<Readonly.ArchivingRule> GetPreservations(System.Type tableType)
    {
      Readonly.ArchivingRule[] archivingRuleArray;
      return AUArchivingRuleEngine.Slot.LocallyCachedSlot.Preserved.TryGetValue(tableType, out archivingRuleArray) ? (IEnumerable<Readonly.ArchivingRule>) archivingRuleArray : (IEnumerable<Readonly.ArchivingRule>) Array.Empty<Readonly.ArchivingRule>();
    }
  }
}
