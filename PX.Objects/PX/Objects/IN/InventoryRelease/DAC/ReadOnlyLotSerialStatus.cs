// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.DAC.ReadOnlyLotSerialStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.DAC;

[Obsolete("This class is obsolete. It will be removed in future versions.")]
[PXHidden]
public class ReadOnlyLotSerialStatus : INLotSerialStatus
{
  protected new DateTime? _ExpireDate;

  [PXDBInt(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.inventoryID))]
  [PXDefault]
  public override int? InventoryID
  {
    get => base.InventoryID;
    set => base.InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.subItemID))]
  public override int? SubItemID
  {
    get => base.SubItemID;
    set => base.SubItemID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.siteID))]
  public override int? SiteID
  {
    get => base.SiteID;
    set => base.SiteID = value;
  }

  [Location(typeof (ReadOnlyLotSerialStatus.siteID), IsKey = true, ValidComboRequired = false, BqlField = typeof (INLotSerialStatusByCostCenter.locationID))]
  [PXDefault]
  public override int? LocationID
  {
    get => base.LocationID;
    set => base.LocationID = value;
  }

  [PXDBString(100, IsUnicode = true, IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.lotSerialNbr))]
  [PXDefault]
  public override 
  #nullable disable
  string LotSerialNbr
  {
    get => base.LotSerialNbr;
    set => base.LotSerialNbr = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyOnHand))]
  public override Decimal? QtyOnHand
  {
    get => base.QtyOnHand;
    set => base.QtyOnHand = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INLotSerialStatusByCostCenter.lotSerTrack))]
  public override string LotSerTrack
  {
    get => base.LotSerTrack;
    set => base.LotSerTrack = value;
  }

  [PXDBDate(BqlField = typeof (INLotSerialStatusByCostCenter.expireDate))]
  [PXUIField(DisplayName = "Expiry Date")]
  public override DateTime? ExpireDate
  {
    get => base.ExpireDate;
    set => base.ExpireDate = value;
  }

  [PXDBDate(BqlField = typeof (INLotSerialStatusByCostCenter.receiptDate))]
  public override DateTime? ReceiptDate
  {
    get => base.ReceiptDate;
    set => base.ReceiptDate = value;
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReadOnlyLotSerialStatus.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.lotSerialNbr>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.qtyOnHand>
  {
  }

  public new abstract class lotSerTrack : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.lotSerTrack>
  {
  }

  public new abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.expireDate>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ReadOnlyLotSerialStatus.receiptDate>
  {
  }
}
