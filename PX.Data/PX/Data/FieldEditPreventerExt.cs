// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldEditPreventerExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public static class FieldEditPreventerExt
{
  public static bool CanEdit<TField>(this PXGraph graph, object row, object newValue) where TField : IBqlField
  {
    return FieldEditPreventerExt.GetCacheOf<TField>(graph).CanEdit<TField>(row, newValue);
  }

  public static bool CanEdit<TField>(this PXCache cache, object row, object newValue) where TField : IBqlField
  {
    return !FieldEditPreventerExt.GetEditPreventingReasonsImpl<TField>(cache, row, newValue).Any<string>();
  }

  public static IEnumerable<string> GetEditPreventingReasonsOf<TField>(
    this PXGraph graph,
    object row,
    object newValue)
    where TField : IBqlField
  {
    return FieldEditPreventerExt.GetCacheOf<TField>(graph).GetEditPreventingReasonsOf<TField>(row, newValue);
  }

  public static IEnumerable<string> GetEditPreventingReasonsOf<TField>(
    this PXCache cache,
    object row,
    object newValue)
    where TField : IBqlField
  {
    return (IEnumerable<string>) FieldEditPreventerExt.GetEditPreventingReasonsImpl<TField>(cache, row, newValue).ToArray<string>();
  }

  /// <summary>
  /// Creates an instance of scope, within which all edit preventing rules (<see cref="T:PX.Data.EditPreventor`1" />) for a specific field are weakened.
  /// </summary>
  public static RuleWeakeningScope MakeRuleWeakeningScopeFor<TField>(
    this PXGraph graph,
    RuleWeakenLevel ruleWeakenLevel)
    where TField : IBqlField
  {
    return new RuleWeakeningScope(graph, typeof (TField), ruleWeakenLevel);
  }

  private static IEnumerable<string> GetEditPreventingReasonsImpl<TField>(
    PXCache cache,
    object row,
    object newValue)
    where TField : IBqlField
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    GetEditPreventingReasonArgs args = new GetEditPreventingReasonArgs(cache, typeof (TField), row, newValue);
    return cache.Graph.Extensions.OfType<IEditPreventer>().Where<IEditPreventer>((Func<IEditPreventer, bool>) (e => e.Fields.Contains<System.Type>(typeof (TField)))).Select<IEditPreventer, string>((Func<IEditPreventer, string>) (ext => ext.GetEditPreventingReason(args))).Where<string>((Func<string, bool>) (res => !string.IsNullOrEmpty(res)));
  }

  private static PXCache GetCacheOf<TField>(PXGraph graph) where TField : IBqlField
  {
    return graph.Caches[BqlCommand.GetItemType(typeof (TField))];
  }

  [PXLocalizable]
  public static class Messages
  {
    public const string FieldAdjustingIsRestrictedByRecordsExistence = "The {0} value cannot be adjusted because the current {1} is referenced in the {2} table.";
    public const string RowDeletingIsRestrictedByRecordsExistence = "The {0} record cannot be deleted because it is referenced in the {1} table.";
  }
}
