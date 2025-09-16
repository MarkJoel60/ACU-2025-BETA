// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.StandardCostPrecisionExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.Matrix.Graphs;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public class StandardCostPrecisionExt : 
  StandardCostPrecisionExtBase<TemplateInventoryItemMaint, InventoryItemCurySettings, InventoryItemCurySettings.inventoryID, InventoryItemCurySettings.stdCost, InventoryItemCurySettings.pendingStdCost>
{
  protected override PX.Objects.CM.Currency GetBaseCurrency(InventoryItemCurySettings settings)
  {
    return CurrencyCollection.GetCurrency(((PXGraph) this.Base).Accessinfo.BaseCuryID);
  }
}
