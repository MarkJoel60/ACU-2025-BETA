// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.CostCenterDispatcher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class CostCenterDispatcher : CostCenterDispatcher<POOrderEntry, POLine, POLine.costCenterID>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();
  }

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(Events.CacheAttached<POLine.costCenterID> e)
  {
  }
}
