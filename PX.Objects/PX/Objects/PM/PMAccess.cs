// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class PMAccess : BaseAccess
{
  public FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>, PMProject>.View Project;
  public FbqlSelect<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectGroup>.View ProjectGroup;

  protected IEnumerable project()
  {
    PMAccess pmAccess = this;
    if (!string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) pmAccess.Group).Current?.GroupName))
    {
      Dictionary<string, bool?> groupMap = GraphHelper.RowCast<PMProjectGroup>(((PXSelectBase) pmAccess.ProjectGroup).Cache.Updated).ToDictionary<PMProjectGroup, string, bool?>((Func<PMProjectGroup, string>) (g => g.ProjectGroupID), (Func<PMProjectGroup, bool?>) (g => g.Included));
      foreach (PXResult<PMProject> pxResult in PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.Or<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.projectTemplate>>>>>.And<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) pmAccess, Array.Empty<object>()))
      {
        PMProject pmProject = PXResult<PMProject>.op_Implicit(pxResult);
        if (((PXSelectBase) pmAccess.Project).Cache.GetStatus((object) pmProject) == null)
        {
          Dictionary<string, bool?> dictionary = groupMap;
          string key = pmProject.ProjectGroupID ?? string.Empty;
          bool? nullable;
          ref bool? local = ref nullable;
          pmProject.Included = !dictionary.TryGetValue(key, out local) ? new bool?(GroupMaskHelper.IsIncluded(pmProject.GroupMask, ((PXSelectBase<RelationGroup>) pmAccess.Group).Current.GroupMask)) : nullable;
        }
        yield return (object) pmProject;
      }
    }
  }

  protected IEnumerable projectGroup()
  {
    PMAccess pmAccess = this;
    if (!string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) pmAccess.Group).Current?.GroupName))
    {
      foreach (PXResult<PMProjectGroup> pxResult in PXSelectBase<PMProjectGroup, PXViewOf<PMProjectGroup>.BasedOn<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select((PXGraph) pmAccess, Array.Empty<object>()))
      {
        PMProjectGroup pmProjectGroup = PXResult<PMProjectGroup>.op_Implicit(pxResult);
        if (((PXSelectBase) pmAccess.ProjectGroup).Cache.GetStatus((object) pmProjectGroup) == null)
          pmProjectGroup.Included = new bool?(GroupMaskHelper.IsIncluded(pmProjectGroup.GroupMask, ((PXSelectBase<RelationGroup>) pmAccess.Group).Current.GroupMask));
        yield return (object) pmProjectGroup;
      }
    }
  }

  /// <see cref="F:PX.SM.BaseAccess.Group">BaseAccess.Group</see>
  protected virtual IEnumerable group()
  {
    PMAccess pmAccess = this;
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<projectType>, Or<Neighbour.leftEntityType, Equal<projectGroupType>>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select((PXGraph) pmAccess, Array.Empty<object>());
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) pmAccess, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if ((relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PMProject).Namespace) && (relationGroup.SpecificType == null || relationGroup.SpecificType == typeof (PMProject).FullName || relationGroup.SpecificType == typeof (PMProjectGroup).FullName) || UserAccess.InNeighbours(set, relationGroup))
        yield return (object) relationGroup;
    }
  }

  protected void _(PX.Data.Events.RowPersisting<PMProject> e)
  {
    PMProject row = e.Row;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || current == null || e.Operation == 3)
      return;
    row.GroupMask = GroupMaskHelper.UpdateMask(row.Included.GetValueOrDefault(), row.GroupMask, current.GroupMask);
  }

  protected void _(PX.Data.Events.RowPersisting<PMProjectGroup> e)
  {
    PMProjectGroup row = e.Row;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || current == null || e.Operation == 3)
      return;
    row.GroupMask = GroupMaskHelper.UpdateMask(row.Included.GetValueOrDefault(), row.GroupMask, current.GroupMask);
  }

  protected void _(PX.Data.Events.RowSelected<RelationGroup> e)
  {
    RelationGroup row = e.Row;
    if (row == null)
      return;
    ((PXAction) this.Save).SetEnabled(!string.IsNullOrEmpty(row.GroupName));
  }

  protected virtual void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    base.RelationGroup_RowInserted(cache, e);
    RelationGroup row = (RelationGroup) e.Row;
    row.SpecificModule = typeof (PMProject).Namespace;
    row.SpecificType = typeof (PMProject).FullName;
  }

  protected void _(PX.Data.Events.RowUpdated<PMProjectGroup> e)
  {
    string projectGroupId = e.Row?.ProjectGroupID;
    PMProjectGroup row = e.Row;
    if ((row != null ? (!row.Included.HasValue ? 1 : 0) : 1) != 0 || string.IsNullOrEmpty(projectGroupId))
      return;
    bool flag = e.Row.Included.Value;
    string message = $"If you clear the Included check box, the projects included in the {projectGroupId} project group will be excluded from the current restriction group. Do you want to proceed?";
    if (!flag)
    {
      if (7 == ((PXSelectBase) this.ProjectGroup).Ask("Update Projects", message, (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
      {
        {
          (WebDialogResult) 6,
          "OK"
        },
        {
          (WebDialogResult) 7,
          "Cancel"
        }
      }))
      {
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMProjectGroup>>) e).Cache.SetValue<PMProjectGroup.included>((object) e.Row, (object) true);
        return;
      }
    }
    foreach (PMProject pmProject in ((PXSelectBase) this.Project).Cache.Updated)
    {
      if (string.Equals(projectGroupId, pmProject.ProjectGroupID, StringComparison.OrdinalIgnoreCase))
      {
        pmProject.Included = new bool?(flag);
        ((PXSelectBase<PMProject>) this.Project).Update(pmProject);
      }
    }
  }

  public PMAccess()
  {
    ((PXSelectBase) this.Project).Cache.AllowInsert = false;
    ((PXSelectBase) this.Project).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Project).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PMProject.included>(((PXSelectBase) this.Project).Cache, (object) null);
    ((PXSelectBase) this.ProjectGroup).Cache.AllowInsert = false;
    ((PXSelectBase) this.ProjectGroup).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.ProjectGroup).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PMProjectGroup.included>(((PXSelectBase) this.ProjectGroup).Cache, (object) null);
  }

  public virtual void Persist()
  {
    this.AdjustProjectGroupMasks();
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    this.populateNeighbours<PMProjectGroup>((PXSelectBase<PMProjectGroup>) this.ProjectGroup);
    this.populateNeighbours<PMProject>((PXSelectBase<PMProject>) this.Project);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<Users>();
    PXSelectorAttribute.ClearGlobalCache<PMProjectGroup>();
    PXSelectorAttribute.ClearGlobalCache<PMProject>();
  }

  private void AdjustProjectGroupMasks()
  {
    Dictionary<string, bool?> dictionary = GraphHelper.RowCast<PMProjectGroup>(((PXSelectBase) this.ProjectGroup).Cache.Updated).ToDictionary<PMProjectGroup, string, bool?>((Func<PMProjectGroup, string>) (g => g.ProjectGroupID), (Func<PMProjectGroup, bool?>) (g => g.Included));
    foreach (PXResult<PMProject> pxResult in ((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()))
    {
      PMProject pmProject = PXResult<PMProject>.op_Implicit(pxResult);
      bool? nullable;
      if (dictionary.TryGetValue(pmProject.ProjectGroupID ?? string.Empty, out nullable) && ((PXSelectBase) this.Project).Cache.GetStatus((object) pmProject) == null)
      {
        ((PXSelectBase) this.Project).Cache.SetValue<PMProject.included>((object) pmProject, (object) nullable);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Project).Cache, (object) pmProject);
      }
    }
  }

  [SubAccount]
  protected void _(
    PX.Data.Events.CacheAttached<PMProject.defaultSalesSubID> _)
  {
  }

  [SubAccount]
  protected void _(
    PX.Data.Events.CacheAttached<PMProject.defaultExpenseSubID> _)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected void _(PX.Data.Events.CacheAttached<PMProject.baseCuryID> _)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected void _(PX.Data.Events.CacheAttached<PMProject.billingCuryID> _)
  {
  }

  [PXRemoveBaseAttribute(typeof (CurrencyInfoAttribute))]
  protected void _(PX.Data.Events.CacheAttached<PMProject.curyInfoID> _)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected void _(PX.Data.Events.CacheAttached<PMProject.startDate> _)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<MatchUser>, PMProjectGroup>.SearchFor<PMProjectGroup.projectGroupID>))]
  protected void _(
    PX.Data.Events.CacheAttached<PMProjectGroup.projectGroupID> _)
  {
  }
}
