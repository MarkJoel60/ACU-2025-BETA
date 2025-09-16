// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.CostCenterDispatcher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class CostCenterDispatcher : 
  CostCenterDispatcher<POReceiptEntry, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.costCenterID>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();
  }

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLine.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (INCostCenter.costCenterID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.PO.POReceiptLine.specialOrderCostCenterID> e)
  {
  }
}
