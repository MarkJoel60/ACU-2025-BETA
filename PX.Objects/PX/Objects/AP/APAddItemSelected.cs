// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAddItemSelected
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<PX.Objects.IN.InventoryItem, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>, LeftJoin<INPriceClass, On<INPriceClass.priceClassID, Equal<PX.Objects.IN.InventoryItem.priceClassID>>, LeftJoin<INUnit, On<INUnit.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INUnit.fromUnit, Equal<PX.Objects.IN.InventoryItem.salesUnit>, And<INUnit.toUnit, Equal<PX.Objects.IN.InventoryItem.baseUnit>>>>>>>, Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, PX.Data.And<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>>>>>>), Persistent = false)]
[Serializable]
public class APAddItemSelected : PXBqlTable, IPXSelectable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _InventoryCD;
  protected string _Descr;
  protected int? _ItemClassID;
  protected string _ItemClassCD;
  protected string _ItemClassDescription;
  protected string _PriceClassID;
  protected string _PriceClassDescription;
  protected string _BaseUnit;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _CuryUnitPrice;
  protected Guid? _NoteID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [InventoryIncludingTemplates(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID), IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault]
  [InventoryRaw(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual string InventoryCD
  {
    get => this._InventoryCD;
    set => this._InventoryCD = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  [PXUIField(DisplayName = "Item Class ID", Visible = true)]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), ValidComboRequired = true)]
  public virtual int? ItemClassID
  {
    get => this._ItemClassID;
    set => this._ItemClassID = value;
  }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (INItemClass.itemClassCD))]
  public virtual string ItemClassCD
  {
    get => this._ItemClassCD;
    set => this._ItemClassCD = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INItemClass.descr), IsProjection = true)]
  [PXUIField(DisplayName = "Item Class Description", Visible = false, ErrorHandling = PXErrorHandling.Always)]
  public virtual string ItemClassDescription
  {
    get => this._ItemClassDescription;
    set => this._ItemClassDescription = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.priceClassID))]
  [PXUIField(DisplayName = "Price Class ID", Visible = true)]
  public virtual string PriceClassID
  {
    get => this._PriceClassID;
    set => this._PriceClassID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INPriceClass.description))]
  [PXUIField(DisplayName = "Price Class Description", Visible = false, ErrorHandling = PXErrorHandling.Always)]
  public virtual string PriceClassDescription
  {
    get => this._PriceClassDescription;
    set => this._PriceClassDescription = value;
  }

  [INUnit(DisplayName = "Base Unit", Visibility = PXUIVisibility.Visible, BqlField = typeof (PX.Objects.IN.InventoryItem.baseUnit))]
  public virtual string BaseUnit
  {
    get => this._BaseUnit;
    set => this._BaseUnit = value;
  }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXUIField(DisplayName = "Last Unit Price", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitPrice
  {
    get => this._CuryUnitPrice;
    set => this._CuryUnitPrice = value;
  }

  /// <summary>The workgroup that is responsible for the item.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID" /> field.
  /// </value>
  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.productWorkgroupID))]
  [PXWorkgroupSelector]
  [PXUIField(DisplayName = "Product Workgroup")]
  public virtual int? ProductWorkgroupID { get; set; }

  /// <summary>
  /// The <see cref="!:EPEmployee">product manager</see> responsible for this item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.CA.CACorpCard.PKID" /> field.
  /// </value>
  [Owner(typeof (PX.Objects.IN.InventoryItem.productWorkgroupID), BqlField = typeof (PX.Objects.IN.InventoryItem.productManagerID), DisplayName = "Product Manager")]
  public virtual int? ProductManagerID { get; set; }

  [PXNote(BqlField = typeof (PX.Objects.IN.InventoryItem.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAddItemSelected.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAddItemSelected.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddItemSelected.inventoryCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddItemSelected.descr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAddItemSelected.itemClassID>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddItemSelected.itemClassCD>
  {
  }

  public abstract class itemClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddItemSelected.itemClassDescription>
  {
  }

  public abstract class priceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddItemSelected.priceClassID>
  {
  }

  public abstract class priceClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAddItemSelected.priceClassDescription>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddItemSelected.baseUnit>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAddItemSelected.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APAddItemSelected.curyInfoID>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAddItemSelected.curyUnitPrice>
  {
  }

  public abstract class productWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APAddItemSelected.productWorkgroupID>
  {
  }

  public abstract class productManagerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APAddItemSelected.productManagerID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAddItemSelected.noteID>
  {
  }
}
