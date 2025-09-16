// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAccessBudget
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.GL;

public class GLAccessBudget : GLAccess
{
  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [GLAccessBudget.GLRelationGroupBudgetSelector(typeof (RelationGroup.groupName), Filterable = true)]
  protected override void RelationGroup_GroupName_CacheAttached(PXCache sender)
  {
  }

  [PX.Objects.GL.Account]
  protected virtual void GLBudgetTree_AccountID_CacheAttached(PXCache sender)
  {
  }

  public new static IEnumerable GroupDelegate(PXGraph graph, bool inclInserted)
  {
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<budgetType>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select(graph, Array.Empty<object>());
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select(graph, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(relationGroup.GroupName) | inclInserted && (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PX.Objects.GL.Account).Namespace) || UserAccess.InNeighbours(set, relationGroup))
        yield return (object) relationGroup;
    }
  }

  protected override IEnumerable group() => GLAccessBudget.GroupDelegate((PXGraph) this, true);

  public class GLRelationGroupBudgetSelectorAttribute(Type type) : PXCustomSelectorAttribute(type)
  {
    public virtual IEnumerable GetRecords() => GLAccessBudget.GroupDelegate(this._Graph, false);
  }
}
