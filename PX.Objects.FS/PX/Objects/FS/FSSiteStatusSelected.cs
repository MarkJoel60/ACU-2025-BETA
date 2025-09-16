// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSiteStatusSelected
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Interfaces;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<PX.Objects.IN.InventoryItem, LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<boolTrue>, And<INSiteStatusByCostCenter.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<PX.Objects.IN.CostCenter.freeStock>>>>>, LeftJoin<INSubItem, On<INSiteStatusByCostCenter.FK.SubItem>, LeftJoin<PX.Objects.IN.INSite, On<INSiteStatusByCostCenter.FK.Site>, LeftJoin<INItemXRef, On<INItemXRef.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And2<Where<INItemXRef.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, Or<INSiteStatusByCostCenter.subItemID, IsNull>>, And<Where<CurrentValue<INSiteStatusFilter.barCode>, IsNotNull, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>>>, LeftJoin<INItemPartNumber, On<INItemPartNumber.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemPartNumber.alternateID, Like<CurrentValue<INSiteStatusFilter.inventory_Wildcard>>, And2<Where<INItemPartNumber.bAccountID, Equal<Zero>, Or<INItemPartNumber.bAccountID, Equal<CurrentValue<FSServiceOrder.customerID>>, Or<INItemPartNumber.bAccountID, Equal<CurrentValue<FSSchedule.customerID>>, Or<INItemPartNumber.alternateType, Equal<INAlternateType.vPN>>>>>, And<Where<INItemPartNumber.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, Or<INSiteStatusByCostCenter.subItemID, IsNull>>>>>>, LeftJoin<INItemClass, On<PX.Objects.IN.InventoryItem.FK.ItemClass>, LeftJoin<INPriceClass, On<INPriceClass.priceClassID, Equal<PX.Objects.IN.InventoryItem.priceClassID>>, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<InventoryItemCurySettings.curyID, Equal<CurrentValue<PX.Objects.IN.INSite.baseCuryID>>>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<InventoryItemCurySettings.preferredVendorID>>, LeftJoin<INItemCustSalesStats, On<CurrentValue<FSSiteStatusFilter.mode>, Equal<SOAddItemMode.byCustomer>, And<INItemCustSalesStats.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INItemCustSalesStats.subItemID, Equal<INSiteStatusByCostCenter.subItemID>, And<INItemCustSalesStats.siteID, Equal<INSiteStatusByCostCenter.siteID>, And2<Where<INItemCustSalesStats.bAccountID, Equal<CurrentValue<FSServiceOrder.customerID>>, Or<INItemCustSalesStats.bAccountID, Equal<CurrentValue<FSSchedule.customerID>>>>, And<INItemCustSalesStats.lastDate, GreaterEqual<CurrentValue<FSSiteStatusFilter.historyDate>>>>>>>>, LeftJoin<INUnit, On<INUnit.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.fromUnit, Equal<PX.Objects.IN.InventoryItem.salesUnit>, And<INUnit.toUnit, Equal<PX.Objects.IN.InventoryItem.baseUnit>>>>>>>>>>>>>>>>, Where2<Where<CurrentValue<FSServiceOrder.customerID>, IsNotNull, Or<CurrentValue<FSSchedule.customerID>, IsNotNull, Or<CurrentValue<FSSrvOrdType.behavior>, Equal<ListField.ServiceOrderTypeBehavior.internalAppointment>>>>, And2<CurrentMatch<PX.Objects.IN.InventoryItem, AccessInfo.userName>, And2<Where<INSiteStatusByCostCenter.siteID, IsNull, Or<PX.Objects.IN.INSite.branchID, IsNotNull, And2<CurrentMatch<PX.Objects.IN.INSite, AccessInfo.userName>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or2<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<FSServiceOrder.branchID>>, Or<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<FSSchedule.branchID>>>>>>>>>, And2<Where<INSiteStatusByCostCenter.subItemID, IsNull, Or<CurrentMatch<INSubItem, AccessInfo.userName>>>, And2<Where<CurrentValue<FSSiteStatusFilter.onlyAvailable>, Equal<boolFalse>, Or<INSiteStatusByCostCenter.qtyAvail, Greater<decimal0>, Or<PX.Objects.IN.InventoryItem.stkItem, Equal<boolFalse>>>>, And2<Where<CurrentValue<FSSiteStatusFilter.mode>, Equal<SOAddItemMode.bySite>, Or<INItemCustSalesStats.lastQty, Greater<decimal0>>>, And2<Where<CurrentValue<FSSiteStatusFilter.lineType>, Equal<FSLineType.All>, Or2<Where<CurrentValue<FSSiteStatusFilter.lineType>, Equal<FSLineType.Service>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>>>>, Or2<Where<CurrentValue<FSSiteStatusFilter.lineType>, Equal<FSLineType.NonStockItem>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>>>>, Or<Where<CurrentValue<FSSiteStatusFilter.lineType>, Equal<FSLineType.Inventory_Item>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>>>>>, And2<Where2<Where<CurrentValue<FSSiteStatusFilter.includeIN>, Equal<True>>, Or<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>, And<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>, And<PX.Objects.IN.InventoryItem.itemStatus, NotIn3<InventoryItemStatus.unknown, InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion, InventoryItemStatus.noSales>>>>>>>>>>>>), Persistent = false)]
[Serializable]
public class FSSiteStatusSelected : 
  PXBqlTable,
  IQtySelectable,
  IPXSelectable,
  IBqlTable,
  IBqlTableSystemDataStorage
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
  protected string _ItemType;
  protected string _PriceClassID;
  protected string _PriceClassDescription;
  protected int? _PreferredVendorID;
  protected string _PreferredVendorDescription;
  protected string _BarCode;
  protected string _AlternateID;
  protected string _AlternateType;
  protected string _AlternateDescr;
  protected int? _SiteID;
  protected string _SiteCD;
  protected int? _SubItemID;
  protected string _SubItemCD;
  protected string _BaseUnit;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected string _SalesUnit;
  protected Decimal? _QtySelected;
  protected int? _DurationSelected;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyLast;
  protected Decimal? _BaseUnitPrice;
  protected Decimal? _CuryUnitPrice;
  protected Decimal? _QtyAvailSale;
  protected Decimal? _QtyOnHandSale;
  protected Decimal? _QtyLastSale;
  protected DateTime? _LastSalesDate;
  protected Guid? _NoteID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Inventory(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID), IsKey = true)]
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
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  [PXUIField(DisplayName = "Item Class ID", Visible = false)]
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
  [PXUIField]
  public virtual string ItemClassDescription
  {
    get => this._ItemClassDescription;
    set => this._ItemClassDescription = value;
  }

  /// <summary>The type of the Inventory Item.</summary>
  /// <value>
  /// Possible values are:
  /// <c>"F"</c> - Finished Good (Stock Items only),
  /// <c>"M"</c> - Component Part (Stock Items only),
  /// <c>"A"</c> - Subassembly (Stock Items only),
  /// <c>"N"</c> - Non-Stock Item (a general type of Non-Stock Item),
  /// <c>"L"</c> - Labor (Non-Stock Items only),
  /// <c>"S"</c> - Service (Non-Stock Items only),
  /// <c>"C"</c> - Charge (Non-Stock Items only),
  /// <c>"E"</c> - Expense (Non-Stock Items only).
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClass.ItemType">Type</see> associated with the <see cref="P:PX.Objects.FS.FSSiteStatusSelected.ItemClassID">Item Class</see>
  /// of the item if it's specified, or to Finished Good (<c>"F"</c>) otherwise.
  /// </value>
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.IN.InventoryItem.itemType))]
  [PXUIField]
  [INItemTypes.List]
  public virtual string ItemType
  {
    get => this._ItemType;
    set => this._ItemType = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.priceClassID))]
  [PXUIField(DisplayName = "Price Class ID", Visible = false)]
  public virtual string PriceClassID
  {
    get => this._PriceClassID;
    set => this._PriceClassID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INPriceClass.description))]
  [PXUIField]
  public virtual string PriceClassDescription
  {
    get => this._PriceClassDescription;
    set => this._PriceClassDescription = value;
  }

  [VendorNonEmployeeActive]
  public virtual int? PreferredVendorID
  {
    get => this._PreferredVendorID;
    set => this._PreferredVendorID = value;
  }

  [PXDBString(250, IsUnicode = true, BqlField = typeof (BAccountR.acctName))]
  [PXUIField]
  public virtual string PreferredVendorDescription
  {
    get => this._PreferredVendorDescription;
    set => this._PreferredVendorDescription = value;
  }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (INItemXRef.alternateID), IsUnicode = true)]
  [PXUIField(DisplayName = "Barcode", Visible = false)]
  public virtual string BarCode
  {
    get => this._BarCode;
    set => this._BarCode = value;
  }

  [PXString(225, IsUnicode = true, InputMask = "")]
  [PXDBCalced(typeof (IsNull<INItemXRef.alternateID, INItemPartNumber.alternateID>), typeof (string))]
  [PXUIField(DisplayName = "Alternate ID", Visible = false)]
  [PXExtraKey]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXString(4)]
  [PXDBCalced(typeof (IsNull<INItemXRef.alternateType, INItemPartNumber.alternateType>), typeof (string))]
  [INAlternateType.List]
  [PXDefault("GLBL")]
  [PXUIField(DisplayName = "Alternate Type", Visible = false)]
  public virtual string AlternateType
  {
    get => this._AlternateType;
    set => this._AlternateType = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXDBCalced(typeof (IsNull<INItemXRef.descr, INItemPartNumber.descr>), typeof (string))]
  [PXUIField(DisplayName = "Alternate Description", Visible = false)]
  public virtual string AlternateDescr
  {
    get => this._AlternateDescr;
    set => this._AlternateDescr = value;
  }

  [PXUIField(DisplayName = "Warehouse")]
  [Site(BqlField = typeof (INSiteStatusByCostCenter.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<PX.Objects.IN.INSite.siteCD>, Empty>), typeof (string))]
  public virtual string SiteCD
  {
    get => this._SiteCD;
    set => this._SiteCD = value;
  }

  [SubItem(typeof (FSSiteStatusSelected.inventoryID), BqlField = typeof (INSubItem.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INSubItem.subItemCD>, Empty>), typeof (string))]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  [INUnit]
  public virtual string BaseUnit
  {
    get => this._BaseUnit;
    set => this._BaseUnit = value;
  }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
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

  [INUnit(typeof (FSSiteStatusSelected.inventoryID), DisplayName = "Sales Unit", BqlField = typeof (PX.Objects.IN.InventoryItem.salesUnit))]
  public virtual string SalesUnit
  {
    get => this._SalesUnit;
    set => this._SalesUnit = value;
  }

  [PXDBString(4, IsFixed = true, BqlField = typeof (FSxService.billingRule))]
  [ListField_BillingRule.List]
  [PXUIField(DisplayName = "Billing Rule")]
  public virtual string BillingRule { get; set; }

  [PXUIField(DisplayName = "Estimated Duration")]
  [PXDBTimeSpanLong]
  public virtual int? EstimatedDuration { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Selected")]
  [PXUIEnabled(typeof (Where<FSSiteStatusSelected.billingRule, NotEqual<ListField_BillingRule.Time>>))]
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
          this._Selected = new bool?(true);
      }
      this._QtySelected = value;
    }
  }

  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Duration Selected", Enabled = true)]
  [PXUIEnabled(typeof (Where<FSSiteStatusSelected.billingRule, Equal<ListField_BillingRule.Time>>))]
  public virtual int? DurationSelected
  {
    get => new int?(this._DurationSelected.GetValueOrDefault());
    set
    {
      if (value.HasValue)
      {
        int? nullable = value;
        int num = 0;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
          this._Selected = new bool?(true);
      }
      this._DurationSelected = value;
    }
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemCustSalesStats.lastQty))]
  public virtual Decimal? QtyLast
  {
    get => this._QtyLast;
    set => this._QtyLast = value;
  }

  [PXDBPriceCost(true, BqlField = typeof (INItemCustSalesStats.lastUnitPrice))]
  public virtual Decimal? BaseUnitPrice
  {
    get => this._BaseUnitPrice;
    set => this._BaseUnitPrice = value;
  }

  [PXUnitPriceCuryConv(typeof (FSSiteStatusSelected.curyInfoID), typeof (FSSiteStatusSelected.baseUnitPrice))]
  [PXUIField]
  public virtual Decimal? CuryUnitPrice
  {
    get => this._CuryUnitPrice;
    set => this._CuryUnitPrice = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyAvail, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyAvail, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvailSale
  {
    get => this._QtyAvailSale;
    set => this._QtyAvailSale = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyOnHand, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyOnHand, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHandSale
  {
    get => this._QtyOnHandSale;
    set => this._QtyOnHandSale = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INItemCustSalesStats.lastQty, INUnit.unitRate>>, Div<INItemCustSalesStats.lastQty, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXUIField(DisplayName = "Qty. Last Sales")]
  public virtual Decimal? QtyLastSale
  {
    get => this._QtyLastSale;
    set => this._QtyLastSale = value;
  }

  [PXDBDate(BqlField = typeof (INItemCustSalesStats.lastDate))]
  [PXUIField(DisplayName = "Last Sales Date")]
  public virtual DateTime? LastSalesDate
  {
    get => this._LastSalesDate;
    set => this._LastSalesDate = value;
  }

  [PXDBQuantity(BqlField = typeof (INItemCustSalesStats.dropShipLastQty))]
  public virtual Decimal? DropShipLastBaseQty { get; set; }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INItemCustSalesStats.dropShipLastQty, INUnit.unitRate>>, Div<INItemCustSalesStats.dropShipLastQty, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXUIField(DisplayName = "Qty. of Last Drop Ship")]
  public virtual Decimal? DropShipLastQty { get; set; }

  [PXDBPriceCost(true, BqlField = typeof (INItemCustSalesStats.dropShipLastUnitPrice))]
  public virtual Decimal? DropShipLastUnitPrice { get; set; }

  [PXUnitPriceCuryConv(typeof (FSSiteStatusSelected.curyInfoID), typeof (FSSiteStatusSelected.dropShipLastUnitPrice))]
  [PXUIField]
  public virtual Decimal? DropShipCuryUnitPrice { get; set; }

  [PXDBDate(BqlField = typeof (INItemCustSalesStats.dropShipLastDate))]
  [PXUIField(DisplayName = "Date of Last Drop Ship")]
  public virtual DateTime? DropShipLastDate { get; set; }

  [PXNote(BqlField = typeof (PX.Objects.IN.InventoryItem.noteID), DoNotUseAsRecordID = true)]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSSiteStatusSelected>.By<FSSiteStatusSelected.inventoryID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<FSSiteStatusSelected>.By<FSSiteStatusSelected.itemClassID>
    {
    }

    public class PreferredVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSSiteStatusSelected>.By<FSSiteStatusSelected.preferredVendorID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSSiteStatusSelected>.By<FSSiteStatusSelected.siteID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSSiteStatusSelected>.By<FSSiteStatusSelected.subItemID>
    {
    }

    public class PriceClass : 
      PrimaryKeyOf<INPriceClass>.By<INPriceClass.priceClassID>.ForeignKeyOf<FSSiteStatusSelected>.By<FSSiteStatusSelected.priceClassID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<FSSiteStatusSelected>.By<FSSiteStatusSelected.curyID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSSiteStatusSelected.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSiteStatusSelected.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.inventoryCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.descr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSiteStatusSelected.itemClassID>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.itemClassCD>
  {
  }

  public abstract class itemClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.itemClassDescription>
  {
  }

  public abstract class itemType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.itemType>
  {
  }

  public abstract class priceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.priceClassID>
  {
  }

  public abstract class priceClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.priceClassDescription>
  {
  }

  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSiteStatusSelected.preferredVendorID>
  {
  }

  public abstract class preferredVendorDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.preferredVendorDescription>
  {
  }

  public abstract class barCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.barCode>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.alternateID>
  {
  }

  public abstract class alternateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.alternateType>
  {
  }

  public abstract class alternateDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSiteStatusSelected.alternateDescr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSiteStatusSelected.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.siteCD>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSiteStatusSelected.subItemID>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.subItemCD>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.baseUnit>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSSiteStatusSelected.curyInfoID>
  {
  }

  public abstract class salesUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSSiteStatusSelected.salesUnit>
  {
  }

  public abstract class billingRule : ListField_BillingRule
  {
  }

  public abstract class estimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSiteStatusSelected.estimatedDuration>
  {
  }

  public abstract class qtySelected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.qtySelected>
  {
  }

  public abstract class durationSelected : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSSiteStatusSelected.durationSelected>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.qtyOnHand>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSiteStatusSelected.qtyAvail>
  {
  }

  public abstract class qtyLast : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSSiteStatusSelected.qtyLast>
  {
  }

  public abstract class baseUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.baseUnitPrice>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.curyUnitPrice>
  {
  }

  public abstract class qtyAvailSale : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.qtyAvailSale>
  {
  }

  public abstract class qtyOnHandSale : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.qtyOnHandSale>
  {
  }

  public abstract class qtyLastSale : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.qtyLastSale>
  {
  }

  public abstract class lastSalesDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSiteStatusSelected.lastSalesDate>
  {
  }

  public abstract class dropShipLastBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.dropShipLastBaseQty>
  {
  }

  public abstract class dropShipLastQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.dropShipLastQty>
  {
  }

  public abstract class dropShipLastUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.dropShipLastUnitPrice>
  {
  }

  public abstract class dropShipCuryUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSSiteStatusSelected.dropShipCuryUnitPrice>
  {
  }

  public abstract class dropShipLastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSiteStatusSelected.dropShipLastDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSiteStatusSelected.noteID>
  {
  }
}
