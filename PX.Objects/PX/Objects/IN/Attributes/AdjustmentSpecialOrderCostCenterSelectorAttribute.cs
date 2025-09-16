// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.AdjustmentSpecialOrderCostCenterSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.IN.Attributes;

public class AdjustmentSpecialOrderCostCenterSelectorAttribute : 
  SpecialOrderCostCenterSelectorAttribute
{
  public AdjustmentSpecialOrderCostCenterSelectorAttribute()
    : base(typeof (Search2<INCostCenter.costCenterID, InnerJoin<PX.Objects.SO.SOLine, On<INCostCenter.FK.OrderLine>, InnerJoin<INSiteStatusByCostCenter, On<PX.Objects.SO.SOLine.inventoryID, Equal<INSiteStatusByCostCenter.inventoryID>, And<PX.Objects.SO.SOLine.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, And<INCostCenter.siteID, Equal<INSiteStatusByCostCenter.siteID>, And<INCostCenter.costCenterID, Equal<INSiteStatusByCostCenter.costCenterID>>>>>>>, Where<PX.Objects.SO.SOLine.inventoryID, Equal<Current2<INTran.inventoryID>>, And<INCostCenter.siteID, Equal<Current2<INTran.siteID>>, And2<Where<Current2<INTran.tranType>, Equal<INTranType.receiptCostAdjustment>, Or<INSiteStatusByCostCenter.qtyOnHand, Greater<decimal0>>>, And<Where<Current2<PX.Objects.IN.INRegister.pIID>, IsNull, Or<Current2<INTran.lotSerialNbr>, IsNull, Or<Current2<INTran.lotSerialNbr>, Equal<StringEmpty>, Or<Exists<Select<INLotSerialStatusByCostCenter, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Current2<INTran.inventoryID>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Current2<INTran.subItemID>>, And<INLotSerialStatusByCostCenter.siteID, Equal<Current2<INTran.siteID>>, And<INLotSerialStatusByCostCenter.locationID, Equal<Current2<INTran.locationID>>, And<INLotSerialStatusByCostCenter.lotSerialNbr, Equal<Current2<INTran.lotSerialNbr>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<INCostCenter.costCenterID>, And<INLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>>>>>>>>>>>>>>>>>>>))
  {
    this.InventoryIDField = typeof (INTran.inventoryID);
    this.SiteIDField = typeof (INTran.siteID);
    this.SOOrderTypeField = typeof (INTran.sOOrderType);
    this.SOOrderNbrField = typeof (INTran.sOOrderNbr);
    this.SOOrderLineNbrField = typeof (INTran.sOOrderLineNbr);
    this.IsSpecialOrderField = typeof (INTran.isSpecialOrder);
    this.CostCenterIDField = typeof (INTran.costCenterID);
    this.CostLayerTypeField = typeof (INTran.costLayerType);
    this.OrigModuleField = typeof (INTran.origModule);
    this.ReleasedField = typeof (INTran.released);
    this.ProjectIDField = typeof (INTran.projectID);
    this.TaskIDField = typeof (INTran.taskID);
    this.CostCodeIDField = typeof (INTran.costCodeID);
    this.InventorySourceField = typeof (INTran.inventorySource);
  }
}
