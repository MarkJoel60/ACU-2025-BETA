// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AP.CacheExtensions.VendorExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.AP.CacheExtensions;

public sealed class VendorExt : PXCacheExtension<PX.Objects.AP.Vendor>
{
  [PXDBInt]
  [CostCodeDimensionSelector(null, null, null, null, true)]
  [PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
  public int? VendorDefaultCostCodeId { get; set; }

  [PXDBInt]
  [PXDimensionSelector("INVENTORY", typeof (Search2<PX.Objects.IN.InventoryItem.inventoryID, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.IN.InventoryItem.cOGSAcctID>>, InnerJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PX.Objects.GL.Account.accountGroupID>>>>, Where<PMAccountGroup.type, Equal<AccountType.expense>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Inventory ID", FieldClass = "Construction")]
  public int? VendorDefaultInventoryId { get; set; }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() || PXAccess.FeatureInstalled<FeaturesSet.costCodes>();
  }

  public abstract class vendorDefaultInventoryId : IBqlField, IBqlOperand
  {
  }

  public abstract class vendorDefaultCostCodeId : IBqlField, IBqlOperand
  {
  }
}
