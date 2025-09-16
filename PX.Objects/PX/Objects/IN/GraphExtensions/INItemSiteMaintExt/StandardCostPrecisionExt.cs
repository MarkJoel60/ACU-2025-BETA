// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INItemSiteMaintExt.StandardCostPrecisionExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INItemSiteMaintExt;

public class StandardCostPrecisionExt : 
  StandardCostPrecisionExtBase<INItemSiteMaint, INItemSite, INItemSite.inventoryID, INItemSite.stdCost, INItemSite.pendingStdCost>
{
  protected override PX.Objects.CM.Currency GetBaseCurrency(INItemSite settings)
  {
    return CurrencyCollection.GetCurrency(INSite.PK.Find((PXGraph) this.Base, settings.SiteID)?.BaseCuryID);
  }
}
