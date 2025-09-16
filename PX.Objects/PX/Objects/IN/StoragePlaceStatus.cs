// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.StoragePlaceStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
public class StoragePlaceStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  [PXImage]
  public virtual 
  #nullable disable
  string SplittedIcon { get; set; }

  [Site(IsKey = true, Enabled = false)]
  public int? SiteID { get; set; }

  [Obsolete]
  [PXDBString]
  [PXUIField(DisplayName = "Warehouse", FieldClass = "INSITE", IsReadOnly = false)]
  public string SiteCD { get; set; }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  [Location(typeof (StoragePlaceStatus.siteID), Enabled = false)]
  public virtual int? LocationID { get; set; }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  [PXDBInt]
  [PXSelector(typeof (SearchFor<INCart.cartID>.Where<BqlOperand<INCart.active, IBqlBool>.IsEqual<True>>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  public int? CartID { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? StorageID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Storage ID", Enabled = false)]
  public string StorageCD { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Storage Description", Enabled = false)]
  public virtual string Descr { get; set; }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  [PXDBBool]
  public virtual bool? IsCart { get; set; }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  [PXDBBool]
  [PXUIField(DisplayName = "Active", Enabled = false)]
  public virtual bool? Active { get; set; }

  [Inventory(IsKey = true, Enabled = false)]
  public virtual int? InventoryID { get; set; }

  [Obsolete]
  [PXDBString]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  public virtual string InventoryCD { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string InventoryDescr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Subitem", FieldClass = "INSUBITEM", Enabled = false)]
  public virtual int? SubItemID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial", Enabled = false)]
  public virtual string LotSerialNbr { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial", Enabled = false)]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Qty. On Hand", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Qty. Picked to Carts", Enabled = false, FieldClass = "Carts")]
  public virtual Decimal? QtyPickedToCart { get; set; }

  [INUnit(DisplayName = "Base Unit", Enabled = false)]
  public virtual string BaseUnit { get; set; }

  [BorrowedNote(typeof (InventoryItem), typeof (InventoryItemMaint))]
  public virtual Guid? NoteID { get; set; }

  public abstract class splittedIcon : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatus.splittedIcon>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatus.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlaceStatus.siteCD>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatus.locationID>
  {
  }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatus.cartID>
  {
  }

  public abstract class storageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatus.storageID>
  {
  }

  public abstract class storageCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlaceStatus.storageCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlaceStatus.descr>
  {
  }

  public abstract class isCart : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  StoragePlaceStatus.isCart>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  StoragePlaceStatus.active>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatus.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatus.inventoryCD>
  {
  }

  public abstract class inventoryDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatus.inventoryDescr>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatus.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatus.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    StoragePlaceStatus.expireDate>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  StoragePlaceStatus.qty>
  {
  }

  public abstract class qtyPickedToCart : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    StoragePlaceStatus.qtyPickedToCart>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlaceStatus.baseUnit>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  StoragePlaceStatus.noteID>
  {
  }
}
