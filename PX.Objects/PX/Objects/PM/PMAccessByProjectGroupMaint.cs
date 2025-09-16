// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccessByProjectGroupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class PMAccessByProjectGroupMaint : UserAccess
{
  public FbqlSelect<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectGroup>.View ProjectGroup;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>, PMProject>.View Projects;
  [PXHidden]
  public FbqlSelect<SelectFromBase<Neighbour, TypeArrayOf<IFbqlJoin>.Empty>, Neighbour>.View Neighbours;
  public PXSave<PMProjectGroup> Save;
  public PXCancel<PMProjectGroup> Cancel;
  public PXFirst<PMProjectGroup> First;
  public PXPrevious<PMProjectGroup> Prev;
  public PXNext<PMProjectGroup> Next;
  public PXLast<PMProjectGroup> Last;

  public IProjectGroupMaskHelper ProjectGroupMaskHelper
  {
    get
    {
      return (IProjectGroupMaskHelper) ((PXGraph) this).GetExtension<PMAccessByProjectGroupMaint.ProjectGroupMaskHelperExt>();
    }
  }

  /// <see cref="F:PX.SM.UserAccess.Groups">UserAccess.Groups</see>
  protected virtual IEnumerable groups()
  {
    PMAccessByProjectGroupMaint projectGroupMaint = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXViewOf<RelationGroup>.BasedOn<SelectFromBase<RelationGroup, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select((PXGraph) projectGroupMaint, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PMProjectGroup).Namespace || UserAccess.IsIncluded(((UserAccess) projectGroupMaint).getMask(), relationGroup))
      {
        ((PXSelectBase<RelationGroup>) projectGroupMaint.Groups).Current = relationGroup;
        yield return (object) relationGroup;
      }
    }
  }

  public PMAccessByProjectGroupMaint()
  {
    ((PXSelectBase) this.ProjectGroup).Cache.AllowDelete = false;
    ((PXSelectBase) this.ProjectGroup).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetRequired(((PXSelectBase) this.ProjectGroup).Cache, (string) null, false);
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
    ((PXGraph) this).Views.Caches.Add(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
  }

  protected virtual byte[] getMask()
  {
    if (((PXSelectBase<Users>) this.User).Current != null)
      return ((PXSelectBase<Users>) this.User).Current.GroupMask;
    return ((PXSelectBase<PMProjectGroup>) this.ProjectGroup).Current != null ? ((PXSelectBase<PMProjectGroup>) this.ProjectGroup).Current.GroupMask : (byte[]) null;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<Users>) this.User).Current != null)
    {
      UserAccess.PopulateNeighbours<Users>((PXSelectBase<Users>) this.User, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<Users>();
      base.Persist();
    }
    else
    {
      if (((PXSelectBase<PMProjectGroup>) this.ProjectGroup).Current == null)
        return;
      UserAccess.PopulateNeighbours<PMProjectGroup>((PXSelectBase<PMProjectGroup>) this.ProjectGroup, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      this.ProjectGroupMaskHelper.AddProjectsToNeghbourMasks(GraphHelper.RowCast<RelationGroup>(((PXSelectBase) this.Groups).Cache.Updated).ToArray<RelationGroup>(), (PXSelectBase<Neighbour>) this.Neighbours);
      PXSelectorAttribute.ClearGlobalCache<PMProjectGroup>();
      PXSelectorAttribute.ClearGlobalCache<PMProject>();
      base.Persist();
      this.ProjectGroupMaskHelper.UpdateMaskForProjectGroupProjects(((PXSelectBase<PMProjectGroup>) this.ProjectGroup).Current.ProjectGroupID, ((PXSelectBase<PMProjectGroup>) this.ProjectGroup).Current.GroupMask);
    }
  }

  protected void _(Events.RowUpdated<RelationGroup> e)
  {
    string projectGroupId = ((PXSelectBase<PMProjectGroup>) this.ProjectGroup).Current?.ProjectGroupID;
    if (string.IsNullOrEmpty(projectGroupId))
      return;
    RelationGroup row = e.Row;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      bool? included = row.Included;
      bool flag = false;
      num = !(included.GetValueOrDefault() == flag & included.HasValue) ? 1 : 0;
    }
    if (num != 0)
      return;
    if (7 != ((PXSelectBase) this.ProjectGroup).Ask("Update Projects", $"If you clear the Included check box, the projects included in the {projectGroupId} project group will be excluded from the current restriction group. Do you want to proceed?", (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) new Dictionary<WebDialogResult, string>()
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
      return;
    ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<RelationGroup>>) e).Cache.SetValue<RelationGroup.included>((object) e.Row, (object) true);
  }

  [PXMergeAttributes]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>, PMProjectGroup>.SearchFor<PMProjectGroup.projectGroupID>))]
  [PXUIField]
  protected void _(
    Events.CacheAttached<PMProjectGroup.projectGroupID> _)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected void _(Events.CacheAttached<PMProject.baseCuryID> _)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected void _(Events.CacheAttached<PMProject.billingCuryID> _)
  {
  }

  [PXRemoveBaseAttribute(typeof (CurrencyInfoAttribute))]
  protected void _(Events.CacheAttached<PMProject.curyInfoID> _)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected void _(Events.CacheAttached<PMProject.startDate> _)
  {
  }

  public sealed class ProjectGroupMaskHelperExt : PX.Objects.PM.ProjectGroupMaskHelper<PMAccessByProjectGroupMaint>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }
}
