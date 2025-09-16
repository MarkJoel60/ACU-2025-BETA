// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalAutomationWithReservedDoc`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// The helper for approval automation that is used for processing the cases when it is necessary to switch between the Open and Reserved statuses without clearing the approvals.
/// </summary>
/// <typeparam name="SourceAssign"></typeparam>
/// <typeparam name="Approved"></typeparam>
/// <typeparam name="Rejected"></typeparam>
/// <typeparam name="Hold"></typeparam>
/// <typeparam name="SetupApproval"></typeparam>
public class EPApprovalAutomationWithReservedDoc<SourceAssign, Approved, Rejected, Hold, SetupApproval> : 
  EPApprovalAutomationWithoutHoldDefaulting<SourceAssign, Approved, Rejected, Hold, SetupApproval>
  where SourceAssign : class, IReserved, IApprovable, IAssign, IBqlTable, new()
  where Approved : class, IBqlField
  where Rejected : class, IBqlField
  where Hold : class, IBqlField
  where SetupApproval : class, IAssignedMap, IBqlTable, new()
{
  public EPApprovalAutomationWithReservedDoc(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
  }

  public EPApprovalAutomationWithReservedDoc(PXGraph graph)
    : base(graph)
  {
  }

  protected override void RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    SourceAssign row = e.Row as SourceAssign;
    if (!(e.OldRow is SourceAssign oldRow) || (object) row == null)
      return;
    bool? approved = row.Approved;
    bool? hold1 = row.Hold;
    bool? hold2 = oldRow.Hold;
    if (!hold2.HasValue)
      return;
    if (hold1.GetValueOrDefault())
    {
      bool? nullable = hold2;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      nullable = row.Released;
      if (nullable.GetValueOrDefault())
        return;
      this.ClearRelatedApprovals(row);
      cache.SetDefaultExt<Approved>((object) row);
      cache.SetDefaultExt<Rejected>((object) row);
    }
    else
    {
      if (!hold2.GetValueOrDefault() || approved.GetValueOrDefault())
        return;
      List<ApprovalMap> assignedMaps = this.GetAssignedMaps(row, cache);
      if (assignedMaps.Any<ApprovalMap>())
      {
        this.Assign(row, (IEnumerable<ApprovalMap>) assignedMaps);
      }
      else
      {
        cache.SetValue<Approved>((object) row, (object) true);
        cache.SetValue<Rejected>((object) row, (object) false);
      }
    }
  }
}
