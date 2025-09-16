// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQSiteStatusSelected
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.Common.Interfaces;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.PO;
using PX.Objects.RQ.Attributes;
using System;

#nullable enable
namespace PX.Objects.RQ;

[RQSiteStatusProjection(Persistent = false)]
public class RQSiteStatusSelected : 
  PXBqlTable,
  IQtySelectable,
  IPXSelectable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IFTSSelectable
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
  protected int? _PreferredVendorID;
  protected string _PreferredVendorDescription;
  protected string _BarCode;
  protected int? _SiteID;
  protected string _SiteCD;
  protected int? _SubItemID;
  protected string _SubItemCD;
  protected string _BaseUnit;
  protected string _PurchaseUnit;
  protected Decimal? _QtySelected;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyOnHandExt;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyAvailExt;
  protected Decimal? _QtyPOPrepared;
  protected Decimal? _QtyPOPreparedExt;
  protected Decimal? _QtyPOOrders;
  protected Decimal? _QtyPOOrdersExt;
  protected Decimal? _QtyPOReceipts;
  protected Decimal? _QtyPOReceiptsExt;
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
  [PXUIField(DisplayName = "Item Class Description", Visible = false)]
  public virtual string ItemClassDescription
  {
    get => this._ItemClassDescription;
    set => this._ItemClassDescription = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.priceClassID))]
  [PXUIField(DisplayName = "Price Class ID", Visible = false)]
  public virtual string PriceClassID
  {
    get => this._PriceClassID;
    set => this._PriceClassID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INPriceClass.description))]
  [PXUIField(DisplayName = "Price Class Description", Visible = false)]
  public virtual string PriceClassDescription
  {
    get => this._PriceClassDescription;
    set => this._PriceClassDescription = value;
  }

  [Vendor(DisplayName = "Preferred Vendor ID", Required = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), BqlField = typeof (InventoryItemCurySettings.preferredVendorID), Visible = false)]
  public virtual int? PreferredVendorID
  {
    get => this._PreferredVendorID;
    set => this._PreferredVendorID = value;
  }

  [PXDBString(250, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Vendor.acctName))]
  [PXUIField(DisplayName = "Preferred Vendor Name", Visible = false)]
  public virtual string PreferredVendorDescription
  {
    get => this._PreferredVendorDescription;
    set => this._PreferredVendorDescription = value;
  }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (INItemXRef.alternateID), IsUnicode = true)]
  public virtual string BarCode
  {
    get => this._BarCode;
    set => this._BarCode = value;
  }

  [PXUIField(DisplayName = "Warehouse")]
  [Site(BqlField = typeof (INSiteStatusByCostCenter.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INSite.siteCD>, Empty>), typeof (string))]
  public virtual string SiteCD
  {
    get => this._SiteCD;
    set => this._SiteCD = value;
  }

  [SubItem(typeof (RQSiteStatusSelected.inventoryID), BqlField = typeof (INSubItem.subItemID))]
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

  [PXDefault(typeof (Search<INItemClass.baseUnit, Where<INItemClass.itemClassID, Equal<Current<PX.Objects.IN.InventoryItem.itemClassID>>>>))]
  [INUnit]
  public virtual string BaseUnit
  {
    get => this._BaseUnit;
    set => this._BaseUnit = value;
  }

  [INUnit(typeof (POSiteStatusSelected.inventoryID), DisplayName = "Purchase Unit", BqlField = typeof (PX.Objects.IN.InventoryItem.purchaseUnit))]
  public virtual string PurchaseUnit
  {
    get => this._PurchaseUnit;
    set => this._PurchaseUnit = value;
  }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
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
          this._Selected = new bool?(true);
      }
      this._QtySelected = value;
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

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyOnHand, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyOnHand, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHandExt
  {
    get => this._QtyOnHandExt;
    set => this._QtyOnHandExt = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyAvail, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyAvail, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvailExt
  {
    get => this._QtyAvailExt;
    set => this._QtyAvailExt = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Prepared")]
  public virtual Decimal? QtyPOPrepared
  {
    get => this._QtyPOPrepared;
    set => this._QtyPOPrepared = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyPOPrepared, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyPOPrepared, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Prepared")]
  public virtual Decimal? QtyPOPreparedExt
  {
    get => this._QtyPOPreparedExt;
    set => this._QtyPOPreparedExt = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Orders")]
  public virtual Decimal? QtyPOOrders
  {
    get => this._QtyPOOrders;
    set => this._QtyPOOrders = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyPOOrders, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyPOOrders, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Orders")]
  public virtual Decimal? QtyPOOrdersExt
  {
    get => this._QtyPOOrdersExt;
    set => this._QtyPOOrdersExt = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Receipts")]
  public virtual Decimal? QtyPOReceipts
  {
    get => this._QtyPOReceipts;
    set => this._QtyPOReceipts = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INUnit.unitMultDiv, Equal<MultDiv.divide>>, Mult<INSiteStatusByCostCenter.qtyPOReceipts, INUnit.unitRate>>, Div<INSiteStatusByCostCenter.qtyPOReceipts, INUnit.unitRate>>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Receipts")]
  public virtual Decimal? QtyPOReceiptsExt
  {
    get => this._QtyPOReceiptsExt;
    set => this._QtyPOReceiptsExt = value;
  }

  [PXNote(BqlField = typeof (PX.Objects.IN.InventoryItem.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <exclude />
  [DBRankTop(BqlField = typeof (InventorySearchIndexAlternateIDTop.rank))]
  public virtual int? Rank { get; set; }

  /// <exclude />
  [PXString]
  [DBCombinedSearchString(typeof (InventorySearchIndexAlternateIDTop.contentIDDesc), BqlTable = typeof (RQSiteStatusSelected))]
  public virtual string CombinedSearchString { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQSiteStatusSelected.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSiteStatusSelected.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.inventoryCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSiteStatusSelected.descr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSiteStatusSelected.itemClassID>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.itemClassCD>
  {
  }

  public abstract class itemClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.itemClassDescription>
  {
  }

  public abstract class priceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.priceClassID>
  {
  }

  public abstract class priceClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.priceClassDescription>
  {
  }

  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQSiteStatusSelected.preferredVendorID>
  {
  }

  public abstract class preferredVendorDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.preferredVendorDescription>
  {
  }

  public abstract class barCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSiteStatusSelected.barCode>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSiteStatusSelected.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSiteStatusSelected.siteCD>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSiteStatusSelected.subItemID>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSiteStatusSelected.subItemCD>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQSiteStatusSelected.baseUnit>
  {
  }

  public abstract class purchaseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.purchaseUnit>
  {
  }

  public abstract class qtySelected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtySelected>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyOnHand>
  {
  }

  public abstract class qtyOnHandExt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyOnHandExt>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQSiteStatusSelected.qtyAvail>
  {
  }

  public abstract class qtyAvailExt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyAvailExt>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyPOPrepared>
  {
  }

  public abstract class qtyPOPreparedExt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyPOPreparedExt>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyPOOrders>
  {
  }

  public abstract class qtyPOOrdersExt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyPOOrdersExt>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyPOReceipts>
  {
  }

  public abstract class qtyPOReceiptsExt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQSiteStatusSelected.qtyPOReceiptsExt>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQSiteStatusSelected.noteID>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQSiteStatusSelected.rank>
  {
  }

  public abstract class combinedSearchString : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQSiteStatusSelected.combinedSearchString>
  {
  }
}
