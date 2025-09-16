// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderApprovalAutomation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderApprovalAutomation : 
  EPApprovalAutomation<SOOrder, SOOrder.approved, SOOrder.rejected, SOOrder.hold, SOSetupApproval>
{
  public SOOrderApprovalAutomation(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public SOOrderApprovalAutomation(PXGraph graph)
    : base(graph)
  {
  }

  protected override bool AllowAssign(PXCache cache, SOOrder oldDoc, SOOrder doc)
  {
    bool? nullable = oldDoc.Hold;
    int num = nullable.GetValueOrDefault() ? 1 : 0;
    nullable = oldDoc.Cancelled;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = doc.Cancelled;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    return num != 0 ? !valueOrDefault2 : valueOrDefault1 && !valueOrDefault2;
  }
}
