// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.IProjectGroupMaskHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.PM;

public interface IProjectGroupMaskHelper
{
  void UpdateMaskForProjectGroupProjects(string projectGroupId, byte[] newGroupMask);

  void UpdateProjectMaskFromProjectGroup(PMProject project, string projectGroupID, PXCache cache);

  void UpdateNeighbourMasks(
    RelationGroup[] relationGroups,
    PXSelectBase<Neighbour> neighbours,
    Type leftEntity,
    Type rightEntity);

  void AddProjectsToNeghbourMasks(
    RelationGroup[] relationGroups,
    PXSelectBase<Neighbour> neighbours);
}
