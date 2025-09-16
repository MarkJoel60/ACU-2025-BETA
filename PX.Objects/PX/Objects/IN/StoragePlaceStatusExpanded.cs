// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.StoragePlaceStatusExpanded
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
[PXHidden]
public class StoragePlaceStatusExpanded : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXImage]
  [PXFormula(typeof (SplitIcon.split))]
  public virtual 
  #nullable disable
  string SplittedIcon { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (INLocation))]
  public int? SiteID { get; set; }

  [PXDBString(BqlTable = typeof (INSite))]
  public string SiteCD { get; set; }

  [Location(typeof (StoragePlaceStatusExpanded.siteID), BqlTable = typeof (INLocation), Enabled = false)]
  public virtual int? LocationID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INLocation.locationID))]
  public virtual int? StorageID { get; set; }

  [PXDBString(BqlField = typeof (INLocation.locationCD))]
  public string StorageCD { get; set; }

  [PXDBString(BqlTable = typeof (INLocation))]
  public virtual string Descr { get; set; }

  [PXDBBool(BqlTable = typeof (INLocation))]
  public virtual bool? Active { get; set; }

  [PXDBInt(BqlTable = typeof (InventoryItem))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(BqlTable = typeof (InventoryItem))]
  public virtual string InventoryCD { get; set; }

  [PXDBInt(BqlTable = typeof (INLotSerialStatus))]
  public virtual int? SubItemID { get; set; }

  [PXDBString(BqlTable = typeof (INLotSerialStatus))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBDate(BqlTable = typeof (INLotSerialStatus))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBDecimal(BqlField = typeof (INLotSerialStatus.qtyOnHand))]
  public virtual Decimal? Qty { get; set; }

  [INUnit(DisplayName = "Base Unit", BqlTable = typeof (InventoryItem), Enabled = false)]
  public virtual string BaseUnit { get; set; }

  public abstract class splittedIcon : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.splittedIcon>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatusExpanded.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlaceStatusExpanded.siteCD>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.locationID>
  {
  }

  public abstract class storageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatusExpanded.storageID>
  {
  }

  public abstract class storageCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.storageCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StoragePlaceStatusExpanded.descr>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  StoragePlaceStatusExpanded.active>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.inventoryCD>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StoragePlaceStatusExpanded.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.expireDate>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  StoragePlaceStatusExpanded.qty>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StoragePlaceStatusExpanded.baseUnit>
  {
  }
}
