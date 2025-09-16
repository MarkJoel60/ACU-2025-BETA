// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectGroupMaskHelper`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CT;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.PM;

public abstract class ProjectGroupMaskHelper<TGraph> : 
  PXGraphExtension<TGraph>,
  IProjectGroupMaskHelper
  where TGraph : PXGraph
{
  public void UpdateMaskForProjectGroupProjects(string projectGroupId, byte[] newGroupMask)
  {
    if (string.IsNullOrEmpty(projectGroupId))
      return;
    PXDatabase.Update<Contract>(new PXDataFieldParam[2]
    {
      (PXDataFieldParam) new PXDataFieldRestrict("ProjectGroupID", (object) projectGroupId),
      (PXDataFieldParam) new PXDataFieldAssign("GroupMask", (object) newGroupMask)
    });
  }

  public void UpdateProjectMaskFromProjectGroup(
    PMProject project,
    string projectGroupID,
    PXCache cache)
  {
    if (project == null)
      return;
    if (string.IsNullOrEmpty(projectGroupID))
    {
      cache.SetValue<PMProject.groupMask>((object) project, (object) Array.Empty<byte>());
    }
    else
    {
      PMProjectGroup pmProjectGroup = PXResultset<PMProjectGroup>.op_Implicit(PXSelectBase<PMProjectGroup, PXViewOf<PMProjectGroup>.BasedOn<SelectFromBase<PMProjectGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProjectGroup.projectGroupID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) projectGroupID
      }));
      cache.SetValue<PMProject.groupMask>((object) project, (object) pmProjectGroup.GroupMask);
    }
  }

  public void AddProjectsToNeghbourMasks(
    RelationGroup[] relationGroups,
    PXSelectBase<Neighbour> neighbours)
  {
    this.UpdateNeighbourMasks(relationGroups, neighbours, typeof (Contract), typeof (Contract));
    this.UpdateNeighbourMasks(relationGroups, neighbours, typeof (Contract), typeof (Users));
    this.UpdateNeighbourMasks(relationGroups, neighbours, typeof (Contract), typeof (PMProjectGroup));
    this.UpdateNeighbourMasks(relationGroups, neighbours, typeof (Users), typeof (Contract));
    this.UpdateNeighbourMasks(relationGroups, neighbours, typeof (PMProjectGroup), typeof (Contract));
  }

  public void UpdateNeighbourMasks(
    RelationGroup[] relationGroups,
    PXSelectBase<Neighbour> neighbours,
    Type leftEntity,
    Type rightEntity)
  {
    if (relationGroups == null)
      return;
    foreach (PXResult<Neighbour> pxResult in neighbours.Select(Array.Empty<object>()))
    {
      Neighbour neighbour = PXResult<Neighbour>.op_Implicit(pxResult);
      if (!(neighbour.LeftEntityType != leftEntity.FullName) || !(neighbour.RightEntityType != rightEntity.FullName))
      {
        foreach (RelationGroup relationGroup in relationGroups)
          this.UpdateNeighbourMasks(relationGroup, neighbour);
        neighbours.Update(neighbour);
      }
    }
    Neighbour neighbour1 = new Neighbour()
    {
      LeftEntityType = leftEntity.FullName,
      RightEntityType = rightEntity.FullName
    };
    if (neighbours.Locate(neighbour1) != null)
      return;
    foreach (RelationGroup relationGroup in relationGroups)
      this.UpdateNeighbourMasks(relationGroup, neighbour1);
    neighbours.Insert(neighbour1);
  }

  protected virtual void UpdateNeighbourMasks(RelationGroup relationGroup, Neighbour neighbour)
  {
    byte[] groupMask1 = relationGroup.GroupMask;
    if (groupMask1 == null)
      return;
    byte[] groupMask2 = new byte[groupMask1.Length];
    neighbour.CoverageMask = GroupMaskHelper.UpdateMask(true, neighbour.CoverageMask, groupMask2);
    neighbour.WinCoverageMask = GroupMaskHelper.UpdateMask(true, neighbour.WinCoverageMask, groupMask2);
    neighbour.InverseMask = GroupMaskHelper.UpdateMask(true, neighbour.InverseMask, groupMask2);
    neighbour.WinInverseMask = GroupMaskHelper.UpdateMask(true, neighbour.WinInverseMask, groupMask2);
    switch (relationGroup.GroupType)
    {
      case "IE":
        neighbour.CoverageMask = GroupMaskHelper.UpdateMask(true, neighbour.CoverageMask, groupMask1);
        break;
      case "IO":
        neighbour.WinCoverageMask = GroupMaskHelper.UpdateMask(true, neighbour.WinCoverageMask, groupMask1);
        break;
      case "EE":
        neighbour.InverseMask = GroupMaskHelper.UpdateMask(true, neighbour.InverseMask, groupMask1);
        break;
      case "EO":
        neighbour.WinInverseMask = GroupMaskHelper.UpdateMask(true, neighbour.WinInverseMask, groupMask1);
        break;
    }
  }
}
