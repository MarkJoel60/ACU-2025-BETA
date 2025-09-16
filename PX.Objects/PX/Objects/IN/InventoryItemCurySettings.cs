// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemCurySettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Inventory Item Currency Settings", CacheGlobal = true)]
public class InventoryItemCurySettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _RecPrice;
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXUIField]
  [PXParent(typeof (InventoryItemCurySettings.FK.Inventory))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Currency", Enabled = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The standard cost assigned to the item before the current standard cost was set.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  [PXUIField(DisplayName = "Last Cost", Enabled = false)]
  public virtual Decimal? LastStdCost { get; set; }

  /// <summary>
  /// The standard cost to be assigned to the item when the costs are updated.
  /// </summary>
  [PXDBPriceCost(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  [PXUIField(DisplayName = "Pending Cost")]
  public virtual Decimal? PendingStdCost { get; set; }

  /// <summary>
  /// The date when the <see cref="P:PX.Objects.IN.InventoryItemCurySettings.PendingStdCost">Pending Cost</see> becomes effective.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Pending Cost Date")]
  [PXFormula(typeof (Switch<Case<Where<InventoryItemCurySettings.pendingStdCost, NotEqual<decimal0>>, Current<AccessInfo.businessDate>>, InventoryItemCurySettings.pendingStdCostDate>))]
  public virtual DateTime? PendingStdCostDate { get; set; }

  /// <summary>The current standard cost of the item.</summary>
  [PXDBPriceCost]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  public virtual Decimal? StdCost { get; set; }

  /// <summary>
  /// The date when the <see cref="P:PX.Objects.IN.InventoryItemCurySettings.StdCost">Current Cost</see> became effective.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  public virtual DateTime? StdCostDate { get; set; }

  /// <summary>
  /// The price used as the default price, if there are no other prices defined for this item in any price list in the Accounts Receivable module.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  [PXUIField]
  public virtual Decimal? BasePrice { get; set; }

  /// <summary>
  /// The manufacturer's suggested retail price of the item.
  /// </summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [CurySymbol(null, null, null, null, null, null, null, true, true)]
  [PXUIField(DisplayName = "MSRP")]
  public virtual Decimal? RecPrice
  {
    get => this._RecPrice;
    set => this._RecPrice = value;
  }

  /// <summary>
  /// The default <see cref="T:PX.Objects.IN.INSite">Warehouse</see> used to store the items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) and when the <see cref="P:PX.Objects.CS.FeaturesSet.Warehouse">Warehouses</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INSite.SiteID" /> field.
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClassCurySettings.DfltSiteID">Default Warehouse</see> specified for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Class of the item</see>.
  /// </value>
  [Site(DisplayName = "Default Warehouse", DescriptionField = typeof (INSite.descr))]
  [PXForeignReference(typeof (InventoryItemCurySettings.FK.DefaultSite))]
  public virtual int? DfltSiteID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INLocation">Location of warehouse</see> used by default to issue items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) when the <see cref="P:PX.Objects.CS.FeaturesSet.WarehouseLocation">Warehouse Locations</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLocation.LocationID" /> field.
  /// </value>
  [Location(typeof (InventoryItemCurySettings.dfltSiteID), DisplayName = "Default Issue From", KeepEntry = false, ResetEntry = false, DescriptionField = typeof (INLocation.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  public virtual int? DfltShipLocationID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INLocation">Location of warehouse</see> used by default to receive items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) when the <see cref="P:PX.Objects.CS.FeaturesSet.WarehouseLocation">Warehouse Locations</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLocation.LocationID" /> field.
  /// </value>
  [Location(typeof (InventoryItemCurySettings.dfltSiteID), DisplayName = "Default Receipt To", KeepEntry = false, ResetEntry = false, DescriptionField = typeof (INLocation.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  public virtual int? DfltReceiptLocationID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INLocation">Location of warehouse</see> used by default to receive items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) when the <see cref="P:PX.Objects.CS.FeaturesSet.WarehouseLocation">Warehouse Locations</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INLocation.LocationID" /> field.
  /// </value>
  [Location(typeof (InventoryItemCurySettings.dfltSiteID), DisplayName = "Default Putaway To", KeepEntry = false, ResetEntry = false, DescriptionField = typeof (INLocation.descr))]
  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  public virtual int? DfltPutawayLocationID { get; set; }

  /// <summary>
  /// Preferred (default) <see cref="T:PX.Objects.AP.Vendor">Vendor</see> for purchases of this item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [VendorNonEmployeeActiveOrHoldPayments(DisplayName = "Preferred Vendor", Required = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName))]
  public virtual int? PreferredVendorID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Location" /> of the <see cref="P:PX.Objects.IN.InventoryItemCurySettings.PreferredVendorID">Preferred (default) Vendor</see>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [LocationID(typeof (Where<BqlOperand<PX.Objects.CR.Location.bAccountID, IBqlInt>.IsEqual<BqlField<InventoryItemCurySettings.preferredVendorID, IBqlInt>.FromCurrent>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Preferred Location")]
  public virtual int? PreferredVendorLocationID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.inventoryID, InventoryItemCurySettings.curyID>
  {
    public static InventoryItemCurySettings Find(
      PXGraph graph,
      int? inventoryID,
      string curyID,
      PKFindOptions options = 0)
    {
      return (InventoryItemCurySettings) PrimaryKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.inventoryID, InventoryItemCurySettings.curyID>.FindBy(graph, (object) inventoryID, (object) curyID, options);
    }

    public static InventoryItemCurySettings FindDirty(
      PXGraph graph,
      int? inventoryID,
      string curyID)
    {
      return PXResultset<InventoryItemCurySettings>.op_Implicit(PXSelectBase<InventoryItemCurySettings, PXSelect<InventoryItemCurySettings, Where<InventoryItemCurySettings.inventoryID, Equal<Required<InventoryItemCurySettings.inventoryID>>, And<InventoryItemCurySettings.curyID, Equal<Required<InventoryItemCurySettings.curyID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) inventoryID,
        (object) curyID
      }));
    }
  }

  public static class FK
  {
    public class Inventory : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.inventoryID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.curyID>
    {
    }

    public class DefaultSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.dfltSiteID>
    {
    }

    public class DefaultShipLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.dfltShipLocationID>
    {
    }

    public class DefaultReceiptLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.dfltReceiptLocationID>
    {
    }

    public class DefaultPutawayLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<InventoryItemCurySettings>.By<InventoryItemCurySettings.dfltPutawayLocationID>
    {
    }

    public class PreferredVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<InventoryItem>.By<InventoryItemCurySettings.preferredVendorID>
    {
    }

    public class PreferredVendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<InventoryItem>.By<InventoryItemCurySettings.preferredVendorID, InventoryItemCurySettings.preferredVendorLocationID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemCurySettings.inventoryID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryItemCurySettings.curyID>
  {
  }

  public abstract class lastStdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItemCurySettings.lastStdCost>
  {
  }

  public abstract class pendingStdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItemCurySettings.pendingStdCost>
  {
  }

  public abstract class pendingStdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItemCurySettings.pendingStdCostDate>
  {
  }

  public abstract class stdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItemCurySettings.stdCost>
  {
  }

  public abstract class stdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItemCurySettings.stdCostDate>
  {
  }

  public abstract class basePrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItemCurySettings.basePrice>
  {
  }

  public abstract class recPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryItemCurySettings.recPrice>
  {
  }

  public abstract class dfltSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemCurySettings.dfltSiteID>
  {
  }

  public abstract class dfltShipLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemCurySettings.dfltShipLocationID>
  {
  }

  public abstract class dfltReceiptLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemCurySettings.dfltReceiptLocationID>
  {
  }

  public abstract class dfltPutawayLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemCurySettings.dfltPutawayLocationID>
  {
  }

  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemCurySettings.preferredVendorID>
  {
  }

  public abstract class preferredVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemCurySettings.preferredVendorLocationID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryItemCurySettings.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemCurySettings.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItemCurySettings.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    InventoryItemCurySettings.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemCurySettings.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryItemCurySettings.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    InventoryItemCurySettings.Tstamp>
  {
  }
}
