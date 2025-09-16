// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.AffectedPOOrdersByPOReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class AffectedPOOrdersByPOReceipt : 
  AffectedPOOrdersByPOLine<AffectedPOOrdersByPOReceipt, POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  protected override bool EntityIsAffected(POOrder entity)
  {
    return !(entity.OrderType == "RS") && base.EntityIsAffected(entity);
  }

  [PXMergeAttributes]
  [PXFormula(null, typeof (SumCalc<POLine.baseReceivedQty>), ValidateAggregateCalculation = true)]
  protected virtual void _(Events.CacheAttached<POLine.baseReceivedQty> e)
  {
  }
}
