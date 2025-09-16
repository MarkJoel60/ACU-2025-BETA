// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.MassConvertStockNonStockFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable enable
namespace PX.Objects.IN.DAC.Unbound;

[PXCacheName("Change Stock Status of Items Filter")]
public class MassConvertStockNonStockFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1)]
  [PXUIField(DisplayName = "Action")]
  [MassConvertStockNonStockFilter.action.List]
  [PXDefault("_")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<MassConvertStockNonStockFilter.action, Equal<MassConvertStockNonStockFilter.action.convertStockToNonStock>>, True, Case<Where<MassConvertStockNonStockFilter.action, Equal<MassConvertStockNonStockFilter.action.convertNonStockToStock>>, False>>, Null>))]
  public virtual bool? StkItem { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<INItemClass.stkItem, Equal<Current<MassConvertStockNonStockFilter.stkItem>>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  [PXFormula(typeof (Default<MassConvertStockNonStockFilter.stkItem>))]
  public virtual int? ItemClassID { get; set; }

  [AnyInventory(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<Current<MassConvertStockNonStockFilter.stkItem>>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<PX.Objects.IN.InventoryItem.templateItemID, IsNull, And<PX.Objects.IN.InventoryItem.kitItem, Equal<False>, And2<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, Or<PX.Objects.IN.InventoryItem.nonStockReceipt, Equal<True>, And<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>, And<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.nonStockItem>>>>>, And<MatchUser>>>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), DisplayName = "Inventory ID")]
  [PXFormula(typeof (Default<MassConvertStockNonStockFilter.stkItem>))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<INItemClass.stkItem, Equal<False>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? NonStockItemClassID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Posting Class")]
  [PXSelector(typeof (INPostClass.postClassID), DescriptionField = typeof (INPostClass.descr))]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<MassConvertStockNonStockFilter.nonStockItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.postClassID), CacheGlobal = true)]
  [PXFormula(typeof (Default<MassConvertStockNonStockFilter.nonStockItemClassID>))]
  public virtual string NonStockPostClassID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class")]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<INItemClass.stkItem, Equal<True>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? StockItemClassID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Posting Class")]
  [PXSelector(typeof (INPostClass.postClassID), DescriptionField = typeof (INPostClass.descr))]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<MassConvertStockNonStockFilter.stockItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.postClassID), CacheGlobal = true)]
  [PXFormula(typeof (Default<MassConvertStockNonStockFilter.stockItemClassID>))]
  public virtual string StockPostClassID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Valuation Method")]
  [INValMethod.List]
  [PXDefault("T", typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<MassConvertStockNonStockFilter.stockItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.valMethod), CacheGlobal = true)]
  [PXFormula(typeof (Default<MassConvertStockNonStockFilter.stockItemClassID>))]
  public virtual string StockValMethod { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (INLotSerClass.lotSerClassID), DescriptionField = typeof (INLotSerClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Lot/Serial Class", FieldClass = "LotSerial")]
  [PXDefault(typeof (SelectFromBase<INItemClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemClass.itemClassID, IBqlInt>.IsEqual<BqlField<MassConvertStockNonStockFilter.stockItemClassID, IBqlInt>.FromCurrent>>), SourceField = typeof (INItemClass.lotSerClassID), CacheGlobal = true)]
  [PXFormula(typeof (Default<MassConvertStockNonStockFilter.stockItemClassID>))]
  public virtual string StockLotSerClassID { get; set; }

  public abstract class action : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.action>
  {
    public const string None = "_";
    public const string ConvertStockToNonStock = "S";
    public const string ConvertNonStockToStock = "N";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]{ "_", "S", "N" }, new string[3]
        {
          "<SELECT>",
          "Convert Stock to Non-Stock",
          "Convert Non-Stock to Stock"
        })
      {
      }
    }

    [PXLocalizable]
    public abstract class DisplayNames
    {
      public const string None = "<SELECT>";
      public const string ConvertStockToNonStock = "Convert Stock to Non-Stock";
      public const string ConvertNonStockToStock = "Convert Non-Stock to Stock";
    }

    public class convertStockToNonStock : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      MassConvertStockNonStockFilter.action.convertStockToNonStock>
    {
      public convertStockToNonStock()
        : base("S")
      {
      }
    }

    public class convertNonStockToStock : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      MassConvertStockNonStockFilter.action.convertNonStockToStock>
    {
      public convertNonStockToStock()
        : base("N")
      {
      }
    }
  }

  public abstract class stkItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.stkItem>
  {
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.itemClassID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.inventoryID>
  {
  }

  public abstract class nonStockItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.nonStockItemClassID>
  {
  }

  public abstract class nonStockPostClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.nonStockPostClassID>
  {
  }

  public abstract class stockItemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.stockItemClassID>
  {
  }

  public abstract class stockPostClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.stockPostClassID>
  {
  }

  public abstract class stockValMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.stockValMethod>
  {
  }

  public abstract class stockLotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MassConvertStockNonStockFilter.stockLotSerClassID>
  {
  }
}
