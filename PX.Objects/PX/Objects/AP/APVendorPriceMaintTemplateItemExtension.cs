// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorPriceMaintTemplateItemExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public class APVendorPriceMaintTemplateItemExtension : PXGraphExtension<APVendorPriceMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.matrixItem>();

  /// Overrides <see cref="M:PX.Objects.AP.APVendorPriceMaint.CreateUnitCostSelectCommand" />
  [PXOverride]
  public virtual BqlCommand CreateUnitCostSelectCommand(
    APVendorPriceMaintTemplateItemExtension.CreateUnitCostSelectCommandParameterLessOrig baseMethod)
  {
    return baseMethod().OrderByNew(typeof (OrderBy<Desc<APVendorPrice.isPromotionalPrice, Desc<APVendorPrice.siteID, Desc<APVendorPrice.vendorID, Asc<PX.Objects.IN.InventoryItem.isTemplate, Desc<APVendorPrice.breakQty>>>>>>));
  }

  /// Overrides <see cref="M:PX.Objects.AP.APVendorPriceMaint.GetInventoryIDs(PX.Data.PXCache,System.Nullable{System.Int32})" />
  [PXOverride]
  public virtual int?[] GetInventoryIDs(
    PXCache sender,
    int? inventoryID,
    APVendorPriceMaintTemplateItemExtension.GetInventoryIDsOrig baseMethod)
  {
    int? templateItemId = (int?) PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID)?.TemplateItemID;
    if (!templateItemId.HasValue)
      return baseMethod(sender, inventoryID);
    return new int?[2]{ inventoryID, templateItemId };
  }

  public delegate BqlCommand CreateUnitCostSelectCommandParameterLessOrig();

  public delegate int?[] GetInventoryIDsOrig(PXCache sender, int? inventoryID);
}
