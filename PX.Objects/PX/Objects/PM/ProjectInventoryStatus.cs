// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectInventoryStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.IN;
using PX.Objects.PM.DAC;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PXProjection(typeof (SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCostCenter>.On<BqlOperand<INCostCenter.costCenterID, IBqlInt>.IsEqual<INLocationStatusByCostCenter.costCenterID>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<INCostCenter.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>, FbqlJoins.Left<PMItemCostStatusByCostCenter>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMItemCostStatusByCostCenter.costCenterID, Equal<INCostCenter.costCenterID>>>>, And<BqlOperand<PMItemCostStatusByCostCenter.inventoryID, IBqlInt>.IsEqual<INLocationStatusByCostCenter.inventoryID>>>>.And<BqlOperand<PMProject.accountingMode, IBqlString>.IsEqual<ProjectAccountingModes.projectSpecific>>>>>.Where<BqlOperand<INLocationStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>))]
[Serializable]
public class ProjectInventoryStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INCostCenter.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  [PXDBString(BqlField = typeof (INCostCenter.costLayerType))]
  [PX.Objects.IN.CostLayerType.List]
  [PXUIField(DisplayName = "Cost Layer Type")]
  public virtual 
  #nullable disable
  string CostLayerType { get; set; }

  [Site(IsKey = true, BqlField = typeof (INLocationStatusByCostCenter.siteID))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (ProjectInventoryStatus.siteID), IsKey = true, BqlField = typeof (INLocationStatusByCostCenter.locationID))]
  public virtual int? LocationID { get; set; }

  [Project(typeof (Where<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>), BqlField = typeof (PMProject.contractID))]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (ProjectInventoryStatus.projectID), BqlField = typeof (INCostCenter.taskID))]
  public virtual int? TaskID { get; set; }

  [PXDBString(1, BqlField = typeof (PMProject.accountingMode))]
  [ProjectAccountingModes.List]
  public string AccountingMode { get; set; }

  [StockItem(IsKey = true, BqlField = typeof (INLocationStatusByCostCenter.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyOnHand))]
  [PXUIField(DisplayName = "Total Qty. On Hand")]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXPriceCost]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<PMItemCostStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>, Div<PMItemCostStatusByCostCenter.totalCost, PMItemCostStatusByCostCenter.qtyOnHand>>, decimal0>), typeof (Decimal?))]
  public virtual Decimal? UnitCost { get; set; }

  [PXPriceCost]
  [PXUIField(DisplayName = "Total Cost")]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<ProjectInventoryStatus.accountingMode, IBqlString>.IsEqual<ProjectAccountingModes.projectSpecific>>, Mult<ProjectInventoryStatus.unitCost, ProjectInventoryStatus.qtyOnHand>>, Null>))]
  public virtual Decimal? TotalCost { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ProjectInventoryStatus.selected>
  {
  }

  public abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ProjectInventoryStatus.costCenterID>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ProjectInventoryStatus.costLayerType>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectInventoryStatus.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectInventoryStatus.locationID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectInventoryStatus.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectInventoryStatus.taskID>
  {
  }

  public abstract class accountingMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ProjectInventoryStatus.accountingMode>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProjectInventoryStatus.inventoryID>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ProjectInventoryStatus.qtyOnHand>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ProjectInventoryStatus.unitCost>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ProjectInventoryStatus.totalCost>
  {
  }
}
