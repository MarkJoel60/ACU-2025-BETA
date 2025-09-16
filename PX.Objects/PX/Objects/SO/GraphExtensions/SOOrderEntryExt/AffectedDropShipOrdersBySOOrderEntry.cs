// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.AffectedDropShipOrdersBySOOrderEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.Extensions;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class AffectedDropShipOrdersBySOOrderEntry : 
  ProcessAffectedEntitiesInPrimaryGraphBase<AffectedDropShipOrdersBySOOrderEntry, SOOrderEntry, SupplyPOOrder, POOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  protected override bool PersistInSameTransaction => true;

  protected override bool EntityIsAffected(SupplyPOOrder entity)
  {
    if (entity.OrderType != "DP" || entity.IsLegacyDropShip.GetValueOrDefault())
      return false;
    int? valueOriginal = (int?) ((PXCache) GraphHelper.Caches<SupplyPOOrder>((PXGraph) this.Base)).GetValueOriginal<SupplyPOOrder.dropShipActiveLinksCount>((object) entity);
    int? activeLinksCount = entity.DropShipActiveLinksCount;
    int? nullable = valueOriginal;
    return !(activeLinksCount.GetValueOrDefault() == nullable.GetValueOrDefault() & activeLinksCount.HasValue == nullable.HasValue);
  }

  protected override void ProcessAffectedEntity(POOrderEntry primaryGraph, SupplyPOOrder entity)
  {
    PX.Objects.PO.POOrder order = PX.Objects.PO.POOrder.PK.Find((PXGraph) primaryGraph, entity.OrderType, entity.OrderNbr);
    ((PXGraph) primaryGraph).GetExtension<PX.Objects.PO.GraphExtensions.POOrderEntryExt.DropShipLinksExt>().UpdateDocumentState(order);
  }

  protected override void ClearCaches(PXGraph graph)
  {
    ((PXCache) GraphHelper.Caches<SupplyPOOrder>(graph)).Clear();
    ((PXCache) GraphHelper.Caches<SupplyPOOrder>(graph)).ClearQueryCache();
  }
}
