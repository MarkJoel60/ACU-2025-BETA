// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.S.INItemSite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.IN.S;

[Serializable]
public class INItemSite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _InventoryID;
  protected int? _SiteID;
  protected 
  #nullable disable
  string _SiteStatus;
  protected int? _InvtAcctID;
  protected int? _InvtSubID;
  protected string _ValMethod;
  protected int? _DfltShipLocationID;
  protected int? _DfltReceiptLocationID;
  protected string _DfltSalesUnit;
  protected string _DfltPurchaseUnit;
  protected Decimal? _LastStdCost;
  protected Decimal? _PendingStdCost;
  protected DateTime? _PendingStdCostDate;
  protected Decimal? _StdCost;
  protected DateTime? _StdCostDate;
  protected Decimal? _LastBasePrice;
  protected Decimal? _PendingBasePrice;
  protected DateTime? _PendingBasePriceDate;
  protected Decimal? _BasePrice;
  protected DateTime? _BasePriceDate;
  protected bool? _PreferredVendorOverride;
  protected int? _PreferredVendorID;
  protected int? _PreferredVendorLocationID;
  protected int? _ProductWorkgroupID;
  protected int? _ProductManagerID;
  protected int? _PriceWorkgroupID;
  protected int? _PriceManagerID;
  protected bool? _IsDefault;
  protected bool? _StdCostOverride;
  protected bool? _BasePriceOverride;
  protected bool? _Commissionable;
  protected string _ABCCodeID;
  protected bool? _ABCCodeIsFixed;
  protected string _MovementClassID;
  protected bool? _MovementClassIsFixed;
  protected Guid? _NoteID;
  protected bool? _POCreate;
  protected Decimal? _MarkupPct;
  protected bool? _MarkupPctOverride;
  protected Decimal? _RecPrice;
  protected bool? _RecPriceOverride;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _ReplenishmentPolicyOverride;
  protected string _ReplenishmentPolicyID;
  protected string _ReplenishmentSource;
  protected int? _ReplenishmentSourceSiteID;
  protected string _ReplenishmentMethod;
  protected bool? _MaxShelfLifeOverride;
  protected int? _MaxShelfLife;
  protected bool? _LaunchDateOverride;
  protected DateTime? _LaunchDate;
  protected bool? _TerminationDateOverride;
  protected DateTime? _TerminationDate;
  protected bool? _SafetyStockOverride;
  protected Decimal? _SafetyStock;
  protected bool? _MinQtyOverride;
  protected Decimal? _MinQty;
  protected bool? _MaxQtyOverride;
  protected Decimal? _MaxQty;
  protected bool? _TransferERQOverride;
  protected Decimal? _TransferERQ;
  protected byte[] _tstamp;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [StockItem(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID")]
  [PXParent(typeof (INItemSite.FK.InventoryItem))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [Site(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (INItemSite.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("AC")]
  [PXUIField]
  [PXStringList(new string[] {"AC", "IN"}, new string[] {"Active", "Inactive"})]
  public virtual string SiteStatus
  {
    get => this._SiteStatus;
    set => this._SiteStatus = value;
  }

  [Account]
  [PXDefault]
  public virtual int? InvtAcctID
  {
    get => this._InvtAcctID;
    set => this._InvtAcctID = value;
  }

  [SubAccount(typeof (INItemSite.invtAcctID))]
  [PXDefault]
  public virtual int? InvtSubID
  {
    get => this._InvtSubID;
    set => this._InvtSubID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.valMethod, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  public virtual string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  [Location(typeof (INItemSite.siteID), DisplayName = "Default Issue From", DescriptionField = typeof (INLocation.descr))]
  public virtual int? DfltShipLocationID
  {
    get => this._DfltShipLocationID;
    set => this._DfltShipLocationID = value;
  }

  [Location(typeof (INItemSite.siteID), DisplayName = "Default Receipt To", DescriptionField = typeof (INLocation.descr))]
  public virtual int? DfltReceiptLocationID
  {
    get => this._DfltReceiptLocationID;
    set => this._DfltReceiptLocationID = value;
  }

  [INUnit(null, typeof (PX.Objects.IN.InventoryItem.baseUnit), DisplayName = "Sales Unit")]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  public virtual string DfltSalesUnit
  {
    get => this._DfltSalesUnit;
    set => this._DfltSalesUnit = value;
  }

  [INUnit(null, typeof (PX.Objects.IN.InventoryItem.baseUnit), DisplayName = "Purchase Unit")]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  public virtual string DfltPurchaseUnit
  {
    get => this._DfltPurchaseUnit;
    set => this._DfltPurchaseUnit = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost", Enabled = false)]
  public virtual Decimal? LastStdCost
  {
    get => this._LastStdCost;
    set => this._LastStdCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Cost")]
  public virtual Decimal? PendingStdCost
  {
    get => this._PendingStdCost;
    set => this._PendingStdCost = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Pending Cost Date")]
  public virtual DateTime? PendingStdCostDate
  {
    get => this._PendingStdCostDate;
    set => this._PendingStdCostDate = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  public virtual Decimal? StdCost
  {
    get => this._StdCost;
    set => this._StdCost = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  public virtual DateTime? StdCostDate
  {
    get => this._StdCostDate;
    set => this._StdCostDate = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Price", Enabled = false)]
  public virtual Decimal? LastBasePrice
  {
    get => this._LastBasePrice;
    set => this._LastBasePrice = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Price")]
  public virtual Decimal? PendingBasePrice
  {
    get => this._PendingBasePrice;
    set => this._PendingBasePrice = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Pending Price Date", Enabled = false)]
  public virtual DateTime? PendingBasePriceDate
  {
    get => this._PendingBasePriceDate;
    set => this._PendingBasePriceDate = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Price", Enabled = false)]
  public virtual Decimal? BasePrice
  {
    get => this._BasePrice;
    set => this._BasePrice = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  public virtual DateTime? BasePriceDate
  {
    get => this._BasePriceDate;
    set => this._BasePriceDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Preferred Vendor Override")]
  public virtual bool? PreferredVendorOverride
  {
    get => this._PreferredVendorOverride;
    set => this._PreferredVendorOverride = value;
  }

  [VendorActive(DisplayName = "Preferred Vendor", Required = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName))]
  public virtual int? PreferredVendorID
  {
    get => this._PreferredVendorID;
    set => this._PreferredVendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<INItemSite.preferredVendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<INItemSite.preferredVendorID>>>>))]
  [PXFormula(typeof (Default<INItemSite.preferredVendorID>))]
  public virtual int? PreferredVendorLocationID
  {
    get => this._PreferredVendorLocationID;
    set => this._PreferredVendorLocationID = value;
  }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Product Workgroup")]
  public virtual int? ProductWorkgroupID
  {
    get => this._ProductWorkgroupID;
    set => this._ProductWorkgroupID = value;
  }

  [Owner(typeof (INItemSite.productWorkgroupID), DisplayName = "Product Manager")]
  public virtual int? ProductManagerID
  {
    get => this._ProductManagerID;
    set => this._ProductManagerID = value;
  }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Price Workgroup")]
  public virtual int? PriceWorkgroupID
  {
    get => this._PriceWorkgroupID;
    set => this._PriceWorkgroupID = value;
  }

  [Owner(typeof (INItemSite.priceWorkgroupID), DisplayName = "Price Manager")]
  public virtual int? PriceManagerID
  {
    get => this._PriceManagerID;
    set => this._PriceManagerID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Default")]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Std. Cost Override")]
  public virtual bool? StdCostOverride
  {
    get => this._StdCostOverride;
    set => this._StdCostOverride = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Price Override")]
  public virtual bool? BasePriceOverride
  {
    get => this._BasePriceOverride;
    set => this._BasePriceOverride = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Commissionable
  {
    get => this._Commissionable;
    set => this._Commissionable = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXSelector(typeof (INABCCode.aBCCodeID), DescriptionField = typeof (INABCCode.descr))]
  public virtual string ABCCodeID
  {
    get => this._ABCCodeID;
    set => this._ABCCodeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Fixed ABC Code")]
  public virtual bool? ABCCodeIsFixed
  {
    get => this._ABCCodeIsFixed;
    set => this._ABCCodeIsFixed = value;
  }

  [PXDBString(1)]
  [PXUIField]
  [PXSelector(typeof (INMovementClass.movementClassID), DescriptionField = typeof (INMovementClass.descr))]
  public virtual string MovementClassID
  {
    get => this._MovementClassID;
    set => this._MovementClassID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Fixed Movement Class")]
  public virtual bool? MovementClassIsFixed
  {
    get => this._MovementClassIsFixed;
    set => this._MovementClassIsFixed = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INItemSite.replenishmentSource, Equal<INReplenishmentSource.purchaseToOrder>, Or<INItemSite.replenishmentSource, Equal<INReplenishmentSource.dropShipToOrder>>>, boolTrue>, boolFalse>), typeof (bool))]
  public virtual bool? POCreate
  {
    get => this._POCreate;
    set => this._POCreate = new bool?(value.GetValueOrDefault());
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Markup %", Enabled = false)]
  public virtual Decimal? MarkupPct
  {
    get => this._MarkupPct;
    set => this._MarkupPct = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Markup % Override")]
  public virtual bool? MarkupPctOverride
  {
    get => this._MarkupPctOverride;
    set => this._MarkupPctOverride = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "MSRP", Enabled = false)]
  public virtual Decimal? RecPrice
  {
    get => this._RecPrice;
    set => this._RecPrice = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Price Override")]
  public virtual bool? RecPriceOverride
  {
    get => this._RecPriceOverride;
    set => this._RecPriceOverride = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Replenishment Policy Override")]
  public virtual bool? ReplenishmentPolicyOverride
  {
    get => this._ReplenishmentPolicyOverride;
    set => this._ReplenishmentPolicyOverride = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Seasonality")]
  [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof (INReplenishmentPolicy.descr))]
  public virtual string ReplenishmentPolicyID
  {
    get => this._ReplenishmentPolicyID;
    set => this._ReplenishmentPolicyID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Replenishment Source")]
  [INReplenishmentSource.List]
  public virtual string ReplenishmentSource
  {
    get => this._ReplenishmentSource;
    set => this._ReplenishmentSource = value;
  }

  [Site(DisplayName = "Replenishment Warehouse", DescriptionField = typeof (INSite.descr))]
  public virtual int? ReplenishmentSourceSiteID
  {
    get => this._ReplenishmentSourceSiteID;
    set => this._ReplenishmentSourceSiteID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Replenishment Method", Enabled = false)]
  [INReplenishmentMethod.List]
  public virtual string ReplenishmentMethod
  {
    get => this._ReplenishmentMethod;
    set => this._ReplenishmentMethod = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INItemSite.PlanningMethod" />
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Planning Method", FieldClass = "InvPlanning")]
  [INPlanningMethod.List]
  public string PlanningMethod { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? MaxShelfLifeOverride
  {
    get => this._MaxShelfLifeOverride;
    set => this._MaxShelfLifeOverride = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Max. Shelf Life (Days)")]
  public virtual int? MaxShelfLife
  {
    get => this._MaxShelfLife;
    set => this._MaxShelfLife = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? LaunchDateOverride
  {
    get => this._LaunchDateOverride;
    set => this._LaunchDateOverride = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Launch Date")]
  public virtual DateTime? LaunchDate
  {
    get => this._LaunchDate;
    set => this._LaunchDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? TerminationDateOverride
  {
    get => this._TerminationDateOverride;
    set => this._TerminationDateOverride = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Termination Date")]
  public virtual DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? SafetyStockOverride
  {
    get => this._SafetyStockOverride;
    set => this._SafetyStockOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Safety Stock")]
  public virtual Decimal? SafetyStock
  {
    get => this._SafetyStock;
    set => this._SafetyStock = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? MinQtyOverride
  {
    get => this._MinQtyOverride;
    set => this._MinQtyOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Reorder Point")]
  public virtual Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? MaxQtyOverride
  {
    get => this._MaxQtyOverride;
    set => this._MaxQtyOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Qty.")]
  public virtual Decimal? MaxQty
  {
    get => this._MaxQty;
    set => this._MaxQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? TransferERQOverride
  {
    get => this._TransferERQOverride;
    set => this._TransferERQOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Transfer ERQ")]
  public virtual Decimal? TransferERQ
  {
    get => this._TransferERQ;
    set => this._TransferERQ = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>
  {
    public static INItemSite Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      PKFindOptions options = 0)
    {
      return (INItemSite) PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.FindBy(graph, (object) inventoryID, (object) siteID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<INItemSite>.By<INItemSite.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSite>.By<INItemSite.siteID>
    {
    }

    public class InventoryAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INItemSite>.By<INItemSite.invtAcctID>
    {
    }

    public class InventorySubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INItemSite>.By<INItemSite.invtSubID>
    {
    }

    public class DefaultShipLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemSite>.By<INItemSite.dfltShipLocationID>
    {
    }

    public class DefaultReceiptLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemSite>.By<INItemSite.dfltReceiptLocationID>
    {
    }

    public class PreferredVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<INItemSite>.By<INItemSite.preferredVendorID>
    {
    }

    public class PreferredVendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<INItemSite>.By<INItemSite.preferredVendorID, INItemSite.preferredVendorLocationID>
    {
    }

    public class ProductWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<INItemSite>.By<INItemSite.productWorkgroupID>
    {
    }

    public class ProductManager : 
      PrimaryKeyOf<PX.Objects.CR.Standalone.EPEmployee>.By<PX.Objects.CR.Standalone.EPEmployee.bAccountID>.ForeignKeyOf<INItemSite>.By<INItemSite.productManagerID>
    {
    }

    public class ABCCode : 
      PrimaryKeyOf<INABCCode>.By<INABCCode.aBCCodeID>.ForeignKeyOf<INItemSite>.By<INItemSite.aBCCodeID>
    {
    }

    public class MovementClass : 
      PrimaryKeyOf<INMovementClass>.By<INMovementClass.movementClassID>.ForeignKeyOf<INItemSite>.By<INItemSite.movementClassID>
    {
    }

    public class ReplenishmentPolicy : 
      PrimaryKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.replenishmentPolicyID>.ForeignKeyOf<INItemSite>.By<INItemSite.replenishmentPolicyID>
    {
    }

    public class ReplenishmentSourceSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSite>.By<INItemSite.replenishmentSourceSiteID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.siteID>
  {
  }

  public abstract class siteStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.siteStatus>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.invtAcctID>
  {
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.invtSubID>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.valMethod>
  {
  }

  public abstract class dfltShipLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.dfltShipLocationID>
  {
  }

  public abstract class dfltReceiptLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.dfltReceiptLocationID>
  {
  }

  public abstract class dfltSalesUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.dfltSalesUnit>
  {
  }

  public abstract class dfltPurchaseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.dfltPurchaseUnit>
  {
  }

  public abstract class lastStdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.lastStdCost>
  {
  }

  public abstract class pendingStdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.pendingStdCost>
  {
  }

  public abstract class pendingStdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.pendingStdCostDate>
  {
  }

  public abstract class stdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.stdCost>
  {
  }

  public abstract class stdCostDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSite.stdCostDate>
  {
  }

  public abstract class lastBasePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.lastBasePrice>
  {
  }

  public abstract class pendingBasePrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.pendingBasePrice>
  {
  }

  public abstract class pendingBasePriceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.pendingBasePriceDate>
  {
  }

  public abstract class basePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.basePrice>
  {
  }

  public abstract class basePriceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.basePriceDate>
  {
  }

  public abstract class preferredVendorOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.preferredVendorOverride>
  {
  }

  public abstract class preferredVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.preferredVendorID>
  {
  }

  public abstract class preferredVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.preferredVendorLocationID>
  {
  }

  public abstract class productWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.productWorkgroupID>
  {
  }

  public abstract class productManagerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.productManagerID>
  {
  }

  public abstract class priceWorkgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.priceWorkgroupID>
  {
  }

  public abstract class priceManagerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.priceManagerID>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.isDefault>
  {
  }

  public abstract class stdCostOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.stdCostOverride>
  {
  }

  public abstract class basePriceOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.basePriceOverride>
  {
  }

  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.commissionable>
  {
  }

  public abstract class aBCCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.aBCCodeID>
  {
  }

  public abstract class aBCCodeIsFixed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.aBCCodeIsFixed>
  {
  }

  public abstract class movementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.movementClassID>
  {
  }

  public abstract class movementClassIsFixed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.movementClassIsFixed>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemSite.noteID>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.pOCreate>
  {
  }

  public abstract class markupPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.markupPct>
  {
  }

  public abstract class markupPctOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.markupPctOverride>
  {
  }

  public abstract class recPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.recPrice>
  {
  }

  public abstract class recPriceOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.recPriceOverride>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemSite.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemSite.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.lastModifiedDateTime>
  {
  }

  public abstract class replenishmentPolicyOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.replenishmentPolicyOverride>
  {
  }

  public abstract class replenishmentPolicyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.replenishmentPolicyID>
  {
  }

  public abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.replenishmentSource>
  {
  }

  public abstract class replenishmentSourceSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.replenishmentSourceSiteID>
  {
  }

  public abstract class replenishmentMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.replenishmentMethod>
  {
  }

  public abstract class planningMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.planningMethod>
  {
  }

  public abstract class maxShelfLifeOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.maxShelfLifeOverride>
  {
  }

  public abstract class maxShelfLife : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.maxShelfLife>
  {
  }

  public abstract class launchDateOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.launchDateOverride>
  {
  }

  public abstract class launchDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSite.launchDate>
  {
  }

  public abstract class terminationDateOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.terminationDateOverride>
  {
  }

  public abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.terminationDate>
  {
  }

  public abstract class safetyStockOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.safetyStockOverride>
  {
  }

  public abstract class safetyStock : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.safetyStock>
  {
  }

  public abstract class minQtyOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.minQtyOverride>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.minQty>
  {
  }

  public abstract class maxQtyOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.maxQtyOverride>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.maxQty>
  {
  }

  public abstract class transferERQOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.transferERQOverride>
  {
  }

  public abstract class transferERQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.transferERQ>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemSite.Tstamp>
  {
  }
}
