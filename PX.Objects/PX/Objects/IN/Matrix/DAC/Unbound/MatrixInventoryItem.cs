// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.IN.Matrix.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Unbound;

[PXVirtual]
[PXCacheName("Inventory Item with Attribute Values")]
[PXBreakInheritance]
public class MatrixInventoryItem : InventoryItem
{
  /// <summary>
  /// The user-friendly unique identifier of the Inventory Item.
  /// The structure of the identifier is determined by the <i>INVENTORY</i> <see cref="T:PX.Objects.CS.Dimension">Segmented Key</see>.
  /// </summary>
  [PXDefault]
  [InventoryRaw(DisplayName = "Inventory ID")]
  [MatrixInventoryItem]
  public override 
  #nullable disable
  string InventoryCD
  {
    get => base.InventoryCD;
    set => base.InventoryCD = value;
  }

  [PXInt(IsKey = true)]
  public override int? InventoryID
  {
    get => base.InventoryID;
    set => base.InventoryID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(PopupTextEnabled = true)]
  public override Guid? NoteID { get; set; }

  /// <summary>
  /// Indicates that an item with such identifier already exists.
  /// </summary>
  [PXBool]
  public virtual bool? Exists { get; set; }

  /// <summary>Indicates that the item does not currently exist.</summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? New { get; set; }

  /// <summary>
  /// Indicates that an item with such identifier is already going to be created with another attribute values.
  /// </summary>
  [PXBool]
  public virtual bool? Duplicate { get; set; }

  /// <summary>The UOM of the item to add to a document.</summary>
  [TemplateUnit(typeof (MatrixInventoryItem.inventoryID), typeof (MatrixInventoryItem.templateItemID), IsDBField = false)]
  public virtual string UOM { get; set; }

  /// <summary>Indicates that the UOM field is disabled.</summary>
  [PXBool]
  public virtual bool? UOMDisabled { get; set; }

  /// <summary>The quantity of the item to add to a document.</summary>
  [PXQuantity]
  [PXUIField]
  public virtual Decimal? Qty { get; set; }

  /// <summary>
  /// Array to store attributes (CSAttributeDetail.AttributeID)
  /// </summary>
  public virtual string[] AttributeIDs { get; set; }

  /// <summary>
  /// Array to store attribute values (CSAttributeDetail.ValueID)
  /// </summary>
  public virtual string[] AttributeValues { get; set; }

  /// <summary>
  /// Array to store attribute values (CSAttributeDetail.Descr)
  /// </summary>
  public virtual string[] AttributeValueDescrs { get; set; }

  /// <summary>
  /// The price used as the default price, if there are no other prices defined for this item in any price list in the Accounts Receivable module.
  /// </summary>
  [PXDBPriceCost]
  [PXUIField]
  public override Decimal? BasePrice
  {
    get => base.BasePrice;
    set => base.BasePrice = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that this item is a Stock Item.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Search<InventoryItem.stkItem, Where<InventoryItem.inventoryID, Equal<Current<EntryHeader.templateItemID>>>>))]
  [PXUIField(DisplayName = "Stock Item")]
  public override bool? StkItem
  {
    get => base.StkItem;
    set => base.StkItem = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INItemClass">Item Class</see>, to which the Inventory Item belongs.
  /// Item Classes provide default settings for items, which belong to them, and are used to group items.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INItemClass.ItemClassID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  [PXDefault(typeof (Search<InventoryItem.itemClassID, Where<InventoryItem.inventoryID, Equal<Current<EntryHeader.templateItemID>>>>))]
  [PXUIRequired(typeof (INItemClass.stkItem))]
  public override int? ItemClassID
  {
    get => base.ItemClassID;
    set => base.ItemClassID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory" /> associated with the item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.TaxCategoryID">Tax Category</see> associated with the <see cref="P:PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem.ItemClassID">Item Class</see>.
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXDefault(typeof (Search<InventoryItem.taxCategoryID, Where<InventoryItem.inventoryID, Equal<Current<EntryHeader.templateItemID>>>>))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public override string TaxCategoryID
  {
    get => base.TaxCategoryID;
    set => base.TaxCategoryID = value;
  }

  /// <summary>The description of the Inventory Item.</summary>
  [DBMatrixLocalizableDescription(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public override string Descr
  {
    get => base.Descr;
    set => base.Descr = value;
  }

  /// <summary>
  /// The default <see cref="T:PX.Objects.IN.INSite">Warehouse</see> used to store the items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) and when the <see cref="!:FeaturesSet.Warehouse">Warehouses</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INSite.SiteID" /> field.
  /// </value>
  [Site(DisplayName = "Default Warehouse", DescriptionField = typeof (INSite.descr), IsDBField = false)]
  public override int? DfltSiteID
  {
    get => base.DfltSiteID;
    set => base.DfltSiteID = value;
  }

  public new abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MatrixInventoryItem.inventoryCD>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatrixInventoryItem.inventoryID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  MatrixInventoryItem.noteID>
  {
  }

  public new abstract class templateItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatrixInventoryItem.templateItemID>
  {
  }

  public abstract class exists : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MatrixInventoryItem.exists>
  {
  }

  public abstract class @new : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MatrixInventoryItem.@new>
  {
  }

  public abstract class duplicate : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    MatrixInventoryItem.duplicate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MatrixInventoryItem.uOM>
  {
  }

  public abstract class uOMDisabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MatrixInventoryItem.uOMDisabled>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  MatrixInventoryItem.qty>
  {
  }

  public abstract class attributeIDs : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    MatrixInventoryItem.attributeIDs>
  {
  }

  public abstract class attributeValues : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    MatrixInventoryItem.attributeValues>
  {
  }

  public abstract class attributeValueDescrs : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    MatrixInventoryItem.attributeValueDescrs>
  {
  }

  public new abstract class basePrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatrixInventoryItem.basePrice>
  {
  }

  public new abstract class stkItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MatrixInventoryItem.stkItem>
  {
  }

  public new abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatrixInventoryItem.itemClassID>
  {
  }

  public new abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MatrixInventoryItem.taxCategoryID>
  {
  }

  public new abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MatrixInventoryItem.descr>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    MatrixInventoryItem.createdDateTime>
  {
  }

  public new abstract class dfltSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  MatrixInventoryItem.dfltSiteID>
  {
  }
}
