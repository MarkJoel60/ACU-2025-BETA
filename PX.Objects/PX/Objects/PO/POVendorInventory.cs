// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POVendorInventory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Inventory Item Vendor Details")]
[Serializable]
public class POVendorInventory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected bool? _AllLocations;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _PurchaseUnit;
  protected string _VendorInventoryID;
  protected short? _VLeadTime;
  protected bool? _OverrideSettings;
  protected short? _AddLeadTimeDays;
  protected bool? _Active;
  protected int? _MinOrdFreq;
  protected Decimal? _MinOrdQty;
  protected Decimal? _MaxOrdQty;
  protected Decimal? _LotSize;
  protected Decimal? _ERQ;
  protected Decimal? _LastPrice;
  protected string _CuryID;
  protected bool? _IsDefault;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [VendorNonEmployeeActiveOrHoldPayments]
  [PXDefault(typeof (PX.Objects.AP.Vendor.bAccountID))]
  [PXForeignReference(typeof (POVendorInventory.FK.Vendor))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POVendorInventory.vendorID>>>))]
  [PXFormula(typeof (Default<POVendorInventory.vendorID>))]
  [PXParent(typeof (POVendorInventory.FK.VendorLocation))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "All Locations")]
  [PXDBCalced(typeof (Switch<Case<Where<POVendorInventory.vendorLocationID, IsNull>, True>, False>), typeof (bool))]
  public virtual bool? AllLocations
  {
    get => this._AllLocations;
    set => this._AllLocations = value;
  }

  [Inventory(Filterable = true, DirtyRead = true, Enabled = false)]
  [PXParent(typeof (POVendorInventory.FK.InventoryItem))]
  [PXDBDefault(typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (POVendorInventory.inventoryID), DisplayName = "Subitem")]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POVendorInventory.inventoryID>>>>))]
  [PXFormula(typeof (Default<POVendorInventory.inventoryID>))]
  [INUnit(typeof (POVendorInventory.inventoryID))]
  [PXCheckUnique(new System.Type[] {typeof (POVendorInventory.vendorID), typeof (POVendorInventory.vendorLocationID), typeof (POVendorInventory.inventoryID), typeof (POVendorInventory.subItemID), typeof (POVendorInventory.purchaseUnit)}, IgnoreNulls = false, ClearOnDuplicate = true)]
  public virtual string PurchaseUnit
  {
    get => this._PurchaseUnit;
    set => this._PurchaseUnit = value;
  }

  [PXDBString(50, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual string VendorInventoryID
  {
    get => this._VendorInventoryID;
    set => this._VendorInventoryID = value;
  }

  [PXShort(MinValue = 0, MaxValue = 100000)]
  [PXUIField(DisplayName = "Vendor Lead Time (Days)", Enabled = false)]
  [PXDBScalar(typeof (Search<PX.Objects.CR.Standalone.Location.vLeadTime, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<POVendorInventory.vendorID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<POVendorInventory.vendorLocationID>>>>))]
  public virtual short? VLeadTime
  {
    get => this._VLeadTime;
    set => this._VLeadTime = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideSettings
  {
    get => this._OverrideSettings;
    set => this._OverrideSettings = value;
  }

  [PXDefault(0)]
  [PXDBShort]
  [PXUIField(DisplayName = "Add. Lead Time (Days)")]
  public virtual short? AddLeadTimeDays
  {
    get => this._AddLeadTimeDays;
    set => this._AddLeadTimeDays = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  [PXUIVerify]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Min. Order Freq.(Days)")]
  [PXDefault(0)]
  public virtual int? MinOrdFreq
  {
    get => this._MinOrdFreq;
    set => this._MinOrdFreq = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Min. Order Qty.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? MinOrdQty
  {
    get => this._MinOrdQty;
    set => this._MinOrdQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Order Qty.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? MaxOrdQty
  {
    get => this._MaxOrdQty;
    set => this._MaxOrdQty = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Lot Size")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LotSize
  {
    get => this._LotSize;
    set => this._LotSize = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "EOQ")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ERQ
  {
    get => this._ERQ;
    set => this._ERQ = value;
  }

  [PXDBPriceCost]
  [PXUIField(DisplayName = "Last Vendor Price", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LastPrice
  {
    get => this._LastPrice;
    set => this._LastPrice = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID), CacheGlobal = true)]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AP.Vendor.curyID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POVendorInventory.vendorID>>>>, Search<Company.baseCuryID>>))]
  [PXFormula(typeof (Default<POVendorInventory.vendorID>))]
  [PXUIField(DisplayName = "Currency ID", Enabled = false)]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Default", Enabled = false)]
  [PXDependsOnFields(new System.Type[] {typeof (POVendorInventory.inventoryID), typeof (POVendorInventory.vendorID), typeof (POVendorInventory.vendorLocationID)})]
  [PODefaultVendor(typeof (POVendorInventory.inventoryID), typeof (POVendorInventory.vendorID), typeof (POVendorInventory.vendorLocationID))]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Prepayment Percent")]
  [PXDefault]
  public virtual Decimal? PrepaymentPct { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public class PK : PrimaryKeyOf<POVendorInventory>.By<POVendorInventory.recordID>
  {
    public static POVendorInventory Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (POVendorInventory) PrimaryKeyOf<POVendorInventory>.By<POVendorInventory.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POVendorInventory>.By<POVendorInventory.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POVendorInventory>.By<POVendorInventory.subItemID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POVendorInventory>.By<POVendorInventory.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<POVendorInventory>.By<POVendorInventory.vendorID, POVendorInventory.vendorLocationID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POVendorInventory>.By<POVendorInventory.curyID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventory.recordID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventory.vendorID>
  {
    public class PreventEditBAccountVOrgBAccountID<TGraph> : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<PX.Objects.CR.BAccount.vOrgBAccountID>>.On<TGraph>.IfExists<SelectFromBase<POVendorInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
      #nullable enable
      POVendorInventory.vendorID, IBqlInt>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.CR.BAccount.bAccountID, IBqlInt>.FromCurrent>>>
      where TGraph : 
      #nullable disable
      PXGraph
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        PX.Objects.CR.BAccount row = arg.Row as PX.Objects.CR.BAccount;
        int? newValue = arg.NewValue as int?;
        string str = PXAccess.GetBranchByBAccountID(newValue)?.BaseCuryID ?? ((PXAccess.Organization) PXAccess.GetOrganizationByBAccountID(newValue))?.BaseCuryID;
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, firstPreventingEntity is POVendorInventory poVendorInventory ? poVendorInventory.InventoryID : new int?());
        string baseCuryId = row?.BaseCuryID;
        if (str == baseCuryId)
          return (string) null;
        return row != null && row.BaseCuryID != null ? PXMessages.LocalizeFormatNoPrefix("A branch with the base currency other than {0} cannot be associated with the {1} vendor because {1} is added to the vendor's list of the {2} item.", new object[3]
        {
          (object) row.BaseCuryID,
          (object) row.AcctCD,
          (object) inventoryItem?.InventoryCD
        }) : PXMessages.LocalizeFormatNoPrefix("This box must remain blank because {0} is added to the list of vendors in the settings of the {1} item.", new object[2]
        {
          (object) row.AcctCD,
          (object) inventoryItem?.InventoryCD
        });
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnVendorMaint : 
      POVendorInventory.vendorID.PreventEditBAccountVOrgBAccountID<VendorMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnCustomerMaint : 
      POVendorInventory.vendorID.PreventEditBAccountVOrgBAccountID<CustomerMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POVendorInventory.vendorLocationID>
  {
  }

  public abstract class allLocations : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POVendorInventory.allLocations>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventory.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventory.subItemID>
  {
  }

  public abstract class purchaseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POVendorInventory.purchaseUnit>
  {
  }

  public abstract class vendorInventoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POVendorInventory.vendorInventoryID>
  {
  }

  public abstract class vLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  POVendorInventory.vLeadTime>
  {
  }

  public abstract class overrideSettings : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POVendorInventory.overrideSettings>
  {
  }

  public abstract class addLeadTimeDays : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    POVendorInventory.addLeadTimeDays>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POVendorInventory.active>
  {
  }

  public abstract class minOrdFreq : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POVendorInventory.minOrdFreq>
  {
  }

  public abstract class minOrdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventory.minOrdQty>
  {
  }

  public abstract class maxOrdQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventory.maxOrdQty>
  {
  }

  public abstract class lotSize : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventory.lotSize>
  {
  }

  public abstract class eRQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventory.eRQ>
  {
  }

  public abstract class lastPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POVendorInventory.lastPrice>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POVendorInventory.curyID>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POVendorInventory.isDefault>
  {
  }

  public abstract class prepaymentPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POVendorInventory.prepaymentPct>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POVendorInventory.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POVendorInventory.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POVendorInventory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POVendorInventory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POVendorInventory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POVendorInventory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POVendorInventory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POVendorInventory.lastModifiedDateTime>
  {
  }
}
