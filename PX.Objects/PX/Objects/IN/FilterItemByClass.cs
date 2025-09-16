// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.FilterItemByClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.IN;

public class FilterItemByClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? ItemClassID { get; set; }

  [AnyInventory(typeof (SearchFor<InventoryItem.inventoryID>.Where<BqlChainableConditionLite<MatchUser>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.itemClassID, Equal<BqlField<FilterItemByClass.itemClassID, IBqlInt>.AsOptional>>>>>.Or<BqlOperand<Optional<FilterItemByClass.itemClassID>, IBqlInt>.IsNull>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  public abstract class itemClassID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  FilterItemByClass.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterItemByClass.inventoryID>
  {
  }
}
