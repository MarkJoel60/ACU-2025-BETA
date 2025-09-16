// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccountGroupAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class PMAccountGroupAccess : BaseAccess
{
  public PXSelect<PMAccountGroup> AccountGroup;

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PMAccountGroupAccess.RelationGroupAccountGroupSelector(typeof (RelationGroup.groupName), Filterable = true)]
  protected virtual void _(Events.CacheAttached<RelationGroup.groupName> e)
  {
  }

  public PMAccountGroupAccess()
  {
    ((PXSelectBase) this.AccountGroup).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.AccountGroup).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PMAccountGroup.included>(((PXSelectBase) this.AccountGroup).Cache, (object) null);
  }

  protected virtual IEnumerable accountGroup()
  {
    PMAccountGroupAccess accountGroupAccess = this;
    if (((PXSelectBase<RelationGroup>) accountGroupAccess.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) accountGroupAccess.Group).Current.GroupName))
    {
      foreach (PXResult<PMAccountGroup> pxResult in PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup>.Config>.Select((PXGraph) accountGroupAccess, Array.Empty<object>()))
      {
        PMAccountGroup pmAccountGroup = PXResult<PMAccountGroup>.op_Implicit(pxResult);
        ((PXSelectBase<PMAccountGroup>) accountGroupAccess.AccountGroup).Current = pmAccountGroup;
        yield return (object) pmAccountGroup;
      }
    }
  }

  public virtual void Persist()
  {
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    this.populateNeighbours<PMAccountGroup>((PXSelectBase<PMAccountGroup>) this.AccountGroup);
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<Users>();
    PXSelectorAttribute.ClearGlobalCache<PMAccountGroup>();
  }

  public static IEnumerable GroupDelegate(PXGraph graph, bool inclInserted)
  {
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<accountGroupType>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select(graph, Array.Empty<object>());
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select(graph, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(relationGroup.GroupName) | inclInserted && (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PMAccountGroup).Namespace) && (relationGroup.SpecificType == null || relationGroup.SpecificType == typeof (PMAccountGroup).FullName) || UserAccess.InNeighbours(set, relationGroup))
        yield return (object) relationGroup;
    }
  }

  protected virtual IEnumerable group() => PMAccountGroupAccess.GroupDelegate((PXGraph) this, true);

  protected virtual void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    base.RelationGroup_RowInserted(cache, e);
    RelationGroup row = (RelationGroup) e.Row;
    row.SpecificModule = typeof (PMAccountGroup).Namespace;
    row.SpecificType = typeof (PMAccountGroup).FullName;
  }

  protected virtual void _(Events.RowSelected<RelationGroup> e)
  {
    RelationGroup row = e.Row;
    if (row == null)
      return;
    if (string.IsNullOrEmpty(row.GroupName))
    {
      ((PXAction) this.Save).SetEnabled(false);
      ((PXSelectBase) this.AccountGroup).Cache.AllowInsert = false;
    }
    else
    {
      ((PXAction) this.Save).SetEnabled(true);
      ((PXSelectBase) this.AccountGroup).Cache.AllowInsert = true;
    }
  }

  protected virtual void _(Events.RowSelected<PMAccountGroup> e)
  {
    PMAccountGroup row = e.Row;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null || ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<PMAccountGroup>>) e).Cache.GetStatus((object) row) != null)
      return;
    row.Included = new bool?(false);
    for (int index = 0; index < row.GroupMask.Length && index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0 && ((int) row.GroupMask[index] & (int) current.GroupMask[index]) == (int) current.GroupMask[index])
        row.Included = new bool?(true);
    }
  }

  protected virtual void _(Events.RowPersisting<PMAccountGroup> e)
  {
    PMAccountGroup row = e.Row;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  public class RelationGroupAccountGroupSelectorAttribute(Type type) : PXCustomSelectorAttribute(type)
  {
    public virtual IEnumerable GetRecords()
    {
      return PMAccountGroupAccess.GroupDelegate(this._Graph, false);
    }
  }
}
