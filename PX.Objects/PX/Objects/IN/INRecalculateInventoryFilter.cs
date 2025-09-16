// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INRecalculateInventoryFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Recalculate Inventory Filter")]
public class INRecalculateInventoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site]
  public virtual int? SiteID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<BqlOperand<INItemClass.stkItem, IBqlBool>.IsEqual<True>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [AnyInventory(typeof (FbqlSelect<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INRecalculateInventoryFilter.itemClassID>, IsNull>>>>.Or<BqlOperand<InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<INRecalculateInventoryFilter.itemClassID, IBqlInt>.FromCurrent>>>, InventoryItem>.SearchFor<InventoryItem.inventoryID>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.inactive>>), "The inventory item is {0}.", new Type[] {typeof (InventoryItem.itemStatus)}, ShowWarning = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Rebuild Item History Since")]
  public virtual bool? RebuildHistory { get; set; }

  [FinPeriodNonLockedSelector(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Replan Back Orders")]
  public virtual bool? ReplanBackorders { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude Items Without Allotted Plan Types")]
  public virtual bool? ShowOnlyAllocatedItems { get; set; }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INRecalculateInventoryFilter.siteID>
  {
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INRecalculateInventoryFilter.itemClassID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INRecalculateInventoryFilter.inventoryID>
  {
  }

  public abstract class rebuildHistory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRecalculateInventoryFilter.rebuildHistory>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRecalculateInventoryFilter.finPeriodID>
  {
  }

  public abstract class replanBackorders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRecalculateInventoryFilter.replanBackorders>
  {
  }

  public abstract class showOnlyAllocatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INRecalculateInventoryFilter.showOnlyAllocatedItems>
  {
  }
}
