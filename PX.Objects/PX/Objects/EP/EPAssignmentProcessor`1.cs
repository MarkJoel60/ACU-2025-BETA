// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentProcessor`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EPAssignmentProcessor<Table> : PXGraph<EPAssignmentProcessor<Table>> where Table : class, IAssign, IBqlTable, new()
{
  private readonly PXGraph _Graph;

  public EPAssignmentProcessor(PXGraph graph)
    : this()
  {
    this._Graph = graph;
  }

  public EPAssignmentProcessor() => this._Graph = (PXGraph) this;

  public virtual bool Assign(Table item, int? assignmentMapID)
  {
    if ((object) item == null)
      throw new ArgumentNullException(nameof (item));
    int? nullable = assignmentMapID;
    int num = 0;
    if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      throw new ArgumentOutOfRangeException(nameof (assignmentMapID));
    EPAssignmentMap map = PXResultset<EPAssignmentMap>.op_Implicit(PXSelectBase<EPAssignmentMap, PXSelect<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) assignmentMapID
    }));
    if (map == null)
      return false;
    int? mapType = map.MapType;
    if (mapType.HasValue)
    {
      switch (mapType.GetValueOrDefault())
      {
        case 0:
          try
          {
            ApproveInfo approveInfo = new EPAssignmentProcessHelper<Table>(this._Graph).Assign(item, map, false).FirstOrDefault<ApproveInfo>();
            if (approveInfo != null)
            {
              item.OwnerID = approveInfo.OwnerID;
              item.WorkgroupID = approveInfo.WorkgroupID;
              return true;
            }
          }
          catch
          {
            return false;
          }
          return false;
        case 1:
          try
          {
            ApproveInfo approveInfo = new EPAssignmentHelper<Table>(this._Graph).Assign(item, map, false, new int?(0)).FirstOrDefault<ApproveInfo>();
            if (approveInfo != null)
            {
              item.OwnerID = approveInfo.OwnerID;
              item.WorkgroupID = approveInfo.WorkgroupID;
              return true;
            }
          }
          catch (Exception ex)
          {
            PXTrace.WriteInformation(ex);
            return false;
          }
          return false;
        case 2:
          throw new ArgumentException(nameof (assignmentMapID));
      }
    }
    return false;
  }

  public virtual IEnumerable<ApproveInfo> Approve(
    Table item,
    EPAssignmentMap map,
    int? currentStepSequence)
  {
    if ((object) item == null)
      throw new ArgumentNullException(nameof (item));
    int? nullable = map != null ? map.MapType : throw new ArgumentOutOfRangeException(nameof (map));
    if (nullable.HasValue)
    {
      switch (nullable.GetValueOrDefault())
      {
        case 0:
          return new EPAssignmentProcessHelper<Table>(this._Graph).Assign(item, map, false);
        case 1:
          throw new ArgumentException("The type of the selected map is incorrect. Only an approval map can be used for assigning documents for approval.");
        case 2:
          return new EPAssignmentHelper<Table>(this._Graph).Assign(item, map, true, currentStepSequence);
      }
    }
    return (IEnumerable<ApproveInfo>) null;
  }
}
