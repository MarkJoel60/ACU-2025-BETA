// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POCreateProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.TM;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Specialized version of the Projection Attribute. Defines Projection as <br />
/// a select of INItemPlan Join INPlanType Join InventoryItem Join INUnit Left Join INItemSite <br />
/// filtered by InventoryItem.workgroupID and InventoryItem.productManagerID according to the values <br />
/// in the POCreateFilter: <br />
/// 1. POCreateFilter.ownerID is null or  POCreateFilter.ownerID = InventoryItem.productManagerID <br />
/// 2. POCreateFilter.workGroupID is null or  POCreateFilter.workGroupID = InventoryItem.productWorkgroupID <br />
/// 3. POCreateFilter.myWorkGroup = false or  InventoryItem.productWorkgroupID =InMember{POCreateFilter.currentOwnerID} <br />
/// 4. InventoryItem.productWorkgroupID is null or  InventoryItem.productWorkgroupID =Owened{POCreateFilter.currentOwnerID}<br />
/// </summary>
public class POCreateProjectionAttribute : OwnedFilter.ProjectionAttribute
{
  public POCreateProjectionAttribute()
    : base(typeof (POCreate.POCreateFilter), typeof (SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INPlanType>.On<INItemPlan.FK.PlanType>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INItemPlan.FK.InventoryItem>>, FbqlJoins.Inner<INSite>.On<INItemPlan.FK.Site>>, FbqlJoins.Inner<INUnit>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>, And<BqlOperand<INUnit.fromUnit, IBqlString>.IsEqual<PX.Objects.IN.InventoryItem.purchaseUnit>>>>.And<BqlOperand<INUnit.toUnit, IBqlString>.IsEqual<PX.Objects.IN.InventoryItem.baseUnit>>>>, FbqlJoins.Left<PX.Objects.IN.S.INItemSite>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.S.INItemSite.inventoryID, Equal<INItemPlan.inventoryID>>>>>.And<BqlOperand<PX.Objects.IN.S.INItemSite.siteID, IBqlInt>.IsEqual<INItemPlan.siteID>>>>, FbqlJoins.Left<INItemClass>.On<PX.Objects.IN.InventoryItem.FK.ItemClass>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.hold, Equal<False>>>>, And<BqlOperand<INItemPlan.fixedSource, IBqlString>.IsEqual<INReplenishmentSource.purchased>>>, And<BqlOperand<INPlanType.isFixed, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<INPlanType.isDemand, IBqlBool>.IsEqual<True>>>), typeof (PX.Objects.IN.INItemSite.productWorkgroupID), typeof (PX.Objects.IN.INItemSite.productManagerID))
  {
  }
}
