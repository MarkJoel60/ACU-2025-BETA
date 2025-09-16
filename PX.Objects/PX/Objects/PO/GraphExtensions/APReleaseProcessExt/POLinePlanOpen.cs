// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APReleaseProcessExt.POLinePlanOpen
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APReleaseProcessExt;

public class POLinePlanOpen : POLinePlanOpen<APReleaseProcess>
{
  [PXOverride]
  public void PerformPersist(PXGraph.IPersistPerformer persister)
  {
    persister.Insert((PXCache) this.PlanCache);
    persister.Update((PXCache) this.PlanCache);
    persister.Delete((PXCache) this.PlanCache);
  }
}
