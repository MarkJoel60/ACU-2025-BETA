// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POOrderEntrySingleProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PO;

#nullable enable
namespace PX.Objects.PM;

public class POOrderEntrySingleProject : 
  SingleProjectExtension<POOrderEntry, POOrder, POOrder.projectID, POOrder.hasMultipleProjects, POOrderSingleProject, POLine, POLine.projectID>
{
  public static bool IsActive()
  {
    return SingleProjectExtension<POOrderEntry, POOrder, POOrder.projectID, POOrder.hasMultipleProjects, POOrderSingleProject, POLine, POLine.projectID>.IsExtensionEnabled();
  }

  protected override PXSelectBase<POOrder> Document => (PXSelectBase<POOrder>) this.Base.Document;

  protected override PXSelectBase<POLine> Details => (PXSelectBase<POLine>) this.Base.Transactions;

  protected override bool IsSingleProjectOnlyMode
  {
    get => ((PXSelectBase<POOrder>) this.Base.Document).Current?.OrderType == "PD";
  }
}
