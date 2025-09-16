// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderSiteStatusSelected
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Interfaces;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.DAC.Projections;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXCacheName("Sales Order Inventory Lookup Row")]
[SOSiteStatusProjection(typeof (SOOrder.branchID), typeof (SOOrder.customerID), typeof (SOOrder.customerLocationID), typeof (CurrentValue<SOOrder.behavior>))]
public class SOOrderSiteStatusSelected : 
  PXBqlTable,
  IQtySelectable,
  IPXSelectable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAlternateSelectable,
  IFTSSelectable
{
  protected Decimal? _QtySelected;

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [Inventory(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID), IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [PXDefault]
  [InventoryRaw(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  public virtual 
  #nullable disable
  string InventoryCD { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  [PXUIField(DisplayName = "Item Class ID", Visible = false)]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), ValidComboRequired = true)]
  public virtual int? ItemClassID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (INItemClass.itemClassCD))]
  public virtual string ItemClassCD { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INItemClass.descr), IsProjection = true)]
  [PXUIField]
  public virtual string ItemClassDescription { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.priceClassID))]
  [PXUIField(DisplayName = "Price Class ID", Visible = false)]
  public virtual string PriceClassID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INPriceClass.description))]
  [PXUIField]
  public virtual string PriceClassDescription { get; set; }

  [VendorNonEmployeeActive]
  public virtual int? PreferredVendorID { get; set; }

  [PXDBString(250, IsUnicode = true, BqlField = typeof (BAccountR.acctName))]
  [PXUIField]
  public virtual string PreferredVendorDescription { get; set; }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (INItemXRef.alternateID), IsUnicode = true)]
  [PXUIField(DisplayName = "Barcode", Visible = false)]
  public virtual string BarCode { get; set; }

  [PXDBString(4, BqlField = typeof (INItemXRef.alternateType))]
  public virtual string BarCodeType { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (INItemXRef.descr))]
  public virtual string BarCodeDescr { get; set; }

  [PXDBString(225, IsUnicode = true, InputMask = "", BqlField = typeof (INItemPartNumber.alternateID))]
  [PXUIField(DisplayName = "Alternate ID")]
  [PXExtraKey]
  public virtual string AlternateID { get; set; }

  [PXDBString(4, BqlField = typeof (INItemPartNumber.alternateType))]
  [INAlternateType.List]
  [PXDefault("GLBL")]
  [PXUIField(DisplayName = "Alternate Type")]
  public virtual string AlternateType { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (INItemPartNumber.descr))]
  [PXUIField(DisplayName = "Alternate Description", Visible = false)]
  public virtual string AlternateDescr { get; set; }

  [PXDBString(225, IsUnicode = true, InputMask = "", BqlField = typeof (INItemPartNumber.alternateID))]
  public virtual string InventoryAlternateID { get; set; }

  [PXDBString(4, BqlField = typeof (INItemPartNumber.alternateType))]
  public virtual string InventoryAlternateType { get; set; }

  [PXDBString(4, BqlField = typeof (INItemPartNumber.descr))]
  public virtual string InventoryAlternateDescr { get; set; }

  [PXUIField(DisplayName = "Warehouse")]
  [Site(BqlField = typeof (INSiteStatusByCostCenter.siteID))]
  public virtual int? SiteID { get; set; }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<PX.Objects.IN.INSite.siteCD>, Empty>), typeof (string))]
  public virtual string SiteCD { get; set; }

  [SubItem(typeof (SOOrderSiteStatusSelected.inventoryID), BqlField = typeof (INSubItem.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INSubItem.subItemCD>, Empty>), typeof (string))]
  public virtual string SubItemCD { get; set; }

  [INUnit]
  public virtual string BaseUnit { get; set; }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [INUnit(typeof (SOOrderSiteStatusSelected.inventoryID), DisplayName = "Sales Unit", BqlField = typeof (PX.Objects.IN.InventoryItem.salesUnit))]
  public virtual string SalesUnit { get; set; }

  [PXQuantity]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Selected")]
  public virtual Decimal? QtySelected
  {
    get => new Decimal?(this._QtySelected.GetValueOrDefault());
    set
    {
      if (value.HasValue)
      {
        Decimal? nullable = value;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
          this.Selected = new bool?(true);
      }
      this._QtySelected = value;
    }
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCustSalesStats.lastQty))]
  public virtual Decimal? QtyLast { get; set; }

  [PXDBPriceCost(true, BqlField = typeof (INItemCustSalesStats.lastUnitPrice))]
  public virtual Decimal? BaseUnitPrice { get; set; }

  [PXUnitPriceCuryConv(typeof (SOOrderSiteStatusSelected.curyInfoID), typeof (SOOrderSiteStatusSelected.baseUnitPrice))]
  [PXUIField]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyAvail, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyAvail, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvailSale { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyOnHand, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyOnHand, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHandSale { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INItemCustSalesStats.lastQty, INUnit.unitRate>>, Div<INItemCustSalesStats.lastQty, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXUIField(DisplayName = "Qty. Last Sales")]
  public virtual Decimal? QtyLastSale { get; set; }

  [PXDBDate(BqlField = typeof (INItemCustSalesStats.lastDate))]
  [PXUIField(DisplayName = "Last Sales Date")]
  public virtual DateTime? LastSalesDate { get; set; }

  [PXDBQuantity(BqlField = typeof (INItemCustSalesStats.dropShipLastQty))]
  public virtual Decimal? DropShipLastBaseQty { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INItemCustSalesStats.dropShipLastQty, INUnit.unitRate>>, Div<INItemCustSalesStats.dropShipLastQty, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXUIField(DisplayName = "Qty. of Last Drop Ship")]
  public virtual Decimal? DropShipLastQty { get; set; }

  [PXDBPriceCost(true, BqlField = typeof (INItemCustSalesStats.dropShipLastUnitPrice))]
  public virtual Decimal? DropShipLastUnitPrice { get; set; }

  [PXUnitPriceCuryConv(typeof (SOOrderSiteStatusSelected.curyInfoID), typeof (SOOrderSiteStatusSelected.dropShipLastUnitPrice))]
  [PXUIField]
  public virtual Decimal? DropShipCuryUnitPrice { get; set; }

  [PXDBDate(BqlField = typeof (INItemCustSalesStats.dropShipLastDate))]
  [PXUIField(DisplayName = "Date of Last Drop Ship")]
  public virtual DateTime? DropShipLastDate { get; set; }

  [PXNote(BqlField = typeof (PX.Objects.IN.InventoryItem.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.IN.InventoryItem.itemStatus))]
  [InventoryItemStatus.List]
  public virtual string ItemStatus { get; set; }

  [DBRankTop(BqlField = typeof (InventorySearchIndexAlternateIDTop.rank))]
  public virtual int? Rank { get; set; }

  [PXString]
  [DBCombinedSearchString(typeof (Concat<TypeArrayOf<IBqlOperand>.FilledWith<InventorySearchIndexAlternateIDTop.contentIDDesc, Space, IsNull<INItemPartNumber.alternateID, StringEmpty>>>), BqlTable = typeof (SOOrderSiteStatusSelected))]
  public virtual string CombinedSearchString { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.selected>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.inventoryCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.descr>
  {
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.itemClassID>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.itemClassCD>
  {
  }

  public abstract class itemClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.itemClassDescription>
  {
  }

  public abstract class priceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.priceClassID>
  {
  }

  public abstract class priceClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.priceClassDescription>
  {
  }

  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.preferredVendorID>
  {
  }

  public abstract class preferredVendorDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.preferredVendorDescription>
  {
  }

  public abstract class barCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.barCode>
  {
  }

  public abstract class barCodeType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.barCodeType>
  {
  }

  public abstract class barCodeDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.barCodeDescr>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.alternateID>
  {
  }

  public abstract class alternateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.alternateType>
  {
  }

  public abstract class alternateDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.alternateDescr>
  {
  }

  public abstract class inventoryAlternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.inventoryAlternateID>
  {
  }

  public abstract class inventoryAlternateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.inventoryAlternateType>
  {
  }

  public abstract class inventoryAlternateDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.inventoryAlternateDescr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.siteCD>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.subItemID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.subItemCD>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.baseUnit>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.curyID>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.curyInfoID>
  {
  }

  public abstract class salesUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.salesUnit>
  {
  }

  public abstract class qtySelected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.qtySelected>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.qtyOnHand>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.qtyAvail>
  {
  }

  public abstract class qtyLast : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.qtyLast>
  {
  }

  public abstract class baseUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.baseUnitPrice>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.curyUnitPrice>
  {
  }

  public abstract class qtyAvailSale : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.qtyAvailSale>
  {
  }

  public abstract class qtyOnHandSale : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.qtyOnHandSale>
  {
  }

  public abstract class qtyLastSale : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.qtyLastSale>
  {
  }

  public abstract class lastSalesDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.lastSalesDate>
  {
  }

  public abstract class dropShipLastBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.dropShipLastBaseQty>
  {
  }

  public abstract class dropShipLastQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.dropShipLastQty>
  {
  }

  public abstract class dropShipLastUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.dropShipLastUnitPrice>
  {
  }

  public abstract class dropShipCuryUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.dropShipCuryUnitPrice>
  {
  }

  public abstract class dropShipLastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.dropShipLastDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.noteID>
  {
  }

  public abstract class itemStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.itemStatus>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderSiteStatusSelected.rank>
  {
  }

  public abstract class combinedSearchString : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderSiteStatusSelected.combinedSearchString>
  {
  }
}
