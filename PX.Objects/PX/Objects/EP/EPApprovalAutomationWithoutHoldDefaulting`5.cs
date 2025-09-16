// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalAutomationWithoutHoldDefaulting`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// The helper for approval automation that does not set the Hold value to true (while the base class of the helper sets the Hold value to true).
/// </summary>
/// <typeparam name="SourceAssign"></typeparam>
/// <typeparam name="Approved"></typeparam>
/// <typeparam name="Rejected"></typeparam>
/// <typeparam name="Hold"></typeparam>
/// <typeparam name="SetupApproval"></typeparam>
public class EPApprovalAutomationWithoutHoldDefaulting<SourceAssign, Approved, Rejected, Hold, SetupApproval> : 
  EPApprovalAutomation<SourceAssign, Approved, Rejected, Hold, SetupApproval>
  where SourceAssign : class, IAssign, IBqlTable, new()
  where Approved : class, IBqlField
  where Rejected : class, IBqlField
  where Hold : class, IBqlField
  where SetupApproval : class, IAssignedMap, IBqlTable, new()
{
  public EPApprovalAutomationWithoutHoldDefaulting(PXGraph graph, Delegate @delegate)
    : base(graph, @delegate)
  {
  }

  public EPApprovalAutomationWithoutHoldDefaulting(PXGraph graph)
    : base(graph)
  {
  }

  protected override void Hold_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
  }
}
