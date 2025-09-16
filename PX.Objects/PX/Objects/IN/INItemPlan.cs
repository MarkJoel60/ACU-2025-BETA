// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Item Plan")]
[Serializable]
public class INItemPlan : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IQtyPlanned
{
  protected bool? _Selected = new bool?(false);
  protected int? _InventoryID;
  protected int? _SiteID;
  protected DateTime? _PlanDate;
  protected long? _PlanID;
  protected 
  #nullable disable
  string _FixedSource;
  protected bool? _Active = new bool?(false);
  protected string _PlanType;
  protected long? _OrigPlanID;
  protected string _OrigPlanType;
  protected Guid? _OrigNoteID;
  protected int? _SubItemID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected int? _SourceSiteID;
  protected long? _SupplyPlanID;
  protected long? _DemandPlanID;
  protected Decimal? _PlanQty;
  protected Guid? _RefNoteID;
  protected bool? _Hold;
  protected bool? _Reverse;
  protected byte[] _tstamp;
  protected int? _BAccountID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [AnyInventory]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SiteAny]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Planned On")]
  public virtual DateTime? PlanDate
  {
    get => this._PlanDate;
    set => this._PlanDate = value;
  }

  [PXDBLongIdentity(IsKey = true)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Fixed Source")]
  [PXDefault("P")]
  [INReplenishmentSource.INPlanList]
  public virtual string FixedSource
  {
    get => this._FixedSource;
    set => this._FixedSource = value;
  }

  [PXExistance]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Plan Type")]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true, DescriptionField = typeof (INPlanType.localizedDescr))]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXDBInt]
  public int? ExcludePlanLevel { get; set; }

  [PXDBLong]
  public virtual long? OrigPlanID
  {
    get => this._OrigPlanID;
    set => this._OrigPlanID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Plan Type")]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType
  {
    get => this._OrigPlanType;
    set => this._OrigPlanType = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? OrigNoteID
  {
    get => this._OrigNoteID;
    set => this._OrigNoteID = value;
  }

  [PXDBInt]
  public int? OrigPlanLevel { get; set; }

  /// <summary>
  /// The field is used for breaking inheritance between plans.
  /// It may be needed, e.g., when Lot/Serial Number of the base plan differs from the derivative one.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public bool? IgnoreOrigPlan { get; set; }

  [SubItem]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Location(typeof (INItemPlan.siteID), ValidComboRequired = false)]
  [PXForeignReference(typeof (INItemPlan.FK.Location))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBInt]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  public virtual int? TaskID { get; set; }

  [PX.Objects.IN.LotSerialNbr]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXBool]
  public virtual bool? IsTempLotSerial { get; set; }

  [PXDBInt]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [SiteAny]
  public virtual int? SourceSiteID
  {
    get => this._SourceSiteID;
    set => this._SourceSiteID = value;
  }

  [PXDBLong]
  [PXSelector(typeof (Search<INItemPlan.planID>), DirtyRead = true)]
  public virtual long? SupplyPlanID
  {
    get => this._SupplyPlanID;
    set => this._SupplyPlanID = value;
  }

  [PXDBLong]
  [PXSelector(typeof (Search<INItemPlan.planID>))]
  public virtual long? DemandPlanID
  {
    get => this._DemandPlanID;
    set => this._DemandPlanID = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  public virtual string OrigUOM { get; set; }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Planned Qty.")]
  public virtual Decimal? PlanQty
  {
    get => this._PlanQty;
    set => this._PlanQty = value;
  }

  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXUIField(DisplayName = "On Hold")]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Reverse")]
  public virtual bool? Reverse
  {
    get => this._Reverse;
    set => this._Reverse = value;
  }

  /// <summary>
  /// The flag indicates if the record has to be skipped when backordered.
  /// </summary>
  [PXBool]
  public virtual bool? IsSkippedWhenBackOrdered { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public static PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial ToItemLotSerial(
    INItemPlan item)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial();
    itemLotSerial.InventoryID = item.InventoryID;
    itemLotSerial.LotSerialNbr = item.LotSerialNbr;
    return itemLotSerial;
  }

  public static SiteLotSerial ToSiteLotSerial(INItemPlan item)
  {
    SiteLotSerial siteLotSerial = new SiteLotSerial();
    siteLotSerial.InventoryID = item.InventoryID;
    siteLotSerial.SiteID = item.SiteID;
    siteLotSerial.LotSerialNbr = item.LotSerialNbr;
    return siteLotSerial;
  }

  public static SiteStatusByCostCenter ToSiteStatusByCostCenter(INItemPlan plan)
  {
    SiteStatusByCostCenter statusByCostCenter = new SiteStatusByCostCenter();
    statusByCostCenter.InventoryID = plan.InventoryID;
    statusByCostCenter.SubItemID = plan.SubItemID;
    statusByCostCenter.SiteID = plan.SiteID;
    statusByCostCenter.CostCenterID = plan.CostCenterID;
    return statusByCostCenter;
  }

  public static LocationStatusByCostCenter ToLocationStatusByCostCenter(INItemPlan plan)
  {
    LocationStatusByCostCenter statusByCostCenter = new LocationStatusByCostCenter();
    statusByCostCenter.InventoryID = plan.InventoryID;
    statusByCostCenter.SubItemID = plan.SubItemID;
    statusByCostCenter.SiteID = plan.SiteID;
    statusByCostCenter.LocationID = plan.LocationID;
    statusByCostCenter.CostCenterID = plan.CostCenterID;
    return statusByCostCenter;
  }

  public static PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter ToLotSerialStatusByCostCenter(
    INItemPlan plan)
  {
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter();
    statusByCostCenter.InventoryID = plan.InventoryID;
    statusByCostCenter.SubItemID = plan.SubItemID;
    statusByCostCenter.SiteID = plan.SiteID;
    statusByCostCenter.LocationID = plan.LocationID;
    statusByCostCenter.LotSerialNbr = plan.LotSerialNbr;
    statusByCostCenter.CostCenterID = plan.CostCenterID;
    return statusByCostCenter;
  }

  [PXDBInt]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>
  /// The flag indicates if the record is not for persistence
  /// </summary>
  [PXBool]
  public virtual bool? IsTemporary { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false)]
  [PXDefault]
  public string RefEntityType { get; set; }

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  /// <summary>
  /// An identifier of a non-stock kit item that is associated with this component plan.
  /// </summary>
  [NonStockItem]
  [PXRestrictor(typeof (Where<InventoryItem.kitItem, Equal<boolTrue>>), "The inventory item is not a kit.", new System.Type[] {})]
  public virtual int? KitInventoryID { get; set; }

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

  public class PK : PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>
  {
    public static INItemPlan Find(PXGraph graph, long? planID, PKFindOptions options = 0)
    {
      return (INItemPlan) PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.FindBy(graph, (object) planID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.siteID>
    {
    }

    public class PlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<INItemPlan>.By<INItemPlan.planType>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.inventoryID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.locationID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.subItemID>
    {
    }

    public class LotSerialStatusByCostCenter : 
      PrimaryKeyOf<INLotSerialStatusByCostCenter>.By<INLotSerialStatusByCostCenter.inventoryID, INLotSerialStatusByCostCenter.subItemID, INLotSerialStatusByCostCenter.siteID, INLotSerialStatusByCostCenter.locationID, INLotSerialStatusByCostCenter.lotSerialNbr, INLotSerialStatusByCostCenter.costCenterID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.inventoryID, INItemPlan.subItemID, INItemPlan.siteID, INItemPlan.locationID, INItemPlan.lotSerialNbr, INItemPlan.costCenterID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.vendorID, INItemPlan.vendorLocationID>
    {
    }

    public class SourceSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.sourceSiteID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.bAccountID>
    {
    }

    public class OriginalItemPlan : 
      PrimaryKeyOf<INItemPlanOrig>.By<INItemPlanOrig.planID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.origPlanID>
    {
    }

    public class SupplyItemPlan : 
      PrimaryKeyOf<INItemPlanSupply>.By<INItemPlanSupply.planID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.supplyPlanID>
    {
    }

    public class DemandItemPlan : 
      PrimaryKeyOf<INItemPlanDemand>.By<INItemPlanDemand.planID>.ForeignKeyOf<INItemPlan>.By<INItemPlan.demandPlanID>
    {
    }

    public class OriginalPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<INItemPlan>.By<INItemPlan.origPlanType>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlan.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      InventoryItem.baseUnit.PreventEditIfExists<Select<INItemPlan, Where<INItemPlan.inventoryID, Equal<Current<InventoryItem.inventoryID>>, And<INItemPlan.planQty, NotEqual<decimal0>>>>>
    {
    }

    public class InventoryLotSerClassIDRule : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<InventoryItem.lotSerClassID>>.On<InventoryItemMaint>.IfExists<Select<INItemPlan, Where<INItemPlan.inventoryID, Equal<Current<InventoryItem.inventoryID>>, And<INItemPlan.planType, NotIn3<INPlanConstants.plan60, INPlanConstants.plan68, INPlanConstants.plan69, INPlanConstants.plan70, INPlanConstants.plan73, INPlanConstants.plan66, INPlanConstants.plan6B, INPlanConstants.plan6D, INPlanConstants.plan6E, INPlanConstants.plan90>, And<INItemPlan.planType, NotIn3<INPlanConstants.plan74, INPlanConstants.plan76, INPlanConstants.plan78, INPlanConstants.plan79>, And<INItemPlan.planType, NotIn3<INPlanConstants.planM1, INPlanConstants.planM2, INPlanConstants.planM5, INPlanConstants.planM6, INPlanConstants.planM8, INPlanConstants.planMB, INPlanConstants.planMC, INPlanConstants.planMD, INPlanConstants.planME>, And<INItemPlan.planQty, NotEqual<decimal0>>>>>>>>
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        INItemPlan inItemPlan = (INItemPlan) firstPreventingEntity;
        if (!EnumerableExtensions.IsIn<string>(inItemPlan.PlanType, "61", "62", "63"))
          return PXMessages.Localize("Lot/serial class cannot be changed when its tracking method is not compatible with the previous class and the item is in use.");
        EntityHelper entityHelper = new EntityHelper((PXGraph) ((PXGraphExtension<InventoryItemMaint>) this).Base);
        return PXMessages.LocalizeFormat("The lot/serial class of the {0} item cannot be changed because this item is allocated in the {1} {2} document.", new object[3]
        {
          (object) InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<InventoryItemMaint>) this).Base, inItemPlan.InventoryID).InventoryCD,
          (object) EntityHelper.GetFriendlyEntityName(System.Type.GetType(inItemPlan.RefEntityType)),
          (object) entityHelper.GetEntityRowID(inItemPlan.RefNoteID, ", ")
        });
      }
    }

    public class InventoryDecimalBaseUnitRule : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<InventoryItem.decimalBaseUnit>>.On<InventoryItemMaintBase>.IfExists<Select<INItemPlan, Where<INItemPlan.inventoryID, Equal<Current<InventoryItem.inventoryID>>, And<INItemPlan.planQty, NotEqual<decimal0>, And<INItemPlan.planQty, NotEqual<Round<INItemPlan.planQty, int0>>>>>>>
    {
      protected virtual void OnPreventEdit(GetEditPreventingReasonArgs args)
      {
        if (!((bool?) args.NewValue).GetValueOrDefault())
          return;
        ((CancelEventArgs) args).Cancel = true;
      }

      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs args,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        INPlanType inPlanType = INPlanType.PK.Find(args.Graph, ((INItemPlan) firstPreventingEntity).PlanType);
        return PXMessages.LocalizeFormat("The {0} UOM cannot be changed to not divisible because the quantity of the item allocated for {1} is fractional.", new object[2]
        {
          (object) ((InventoryItem) args.Row).BaseUnit,
          (object) inPlanType.Descr
        });
      }
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.siteID>
  {
  }

  public abstract class planDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemPlan.planDate>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INItemPlan.planID>
  {
  }

  public abstract class fixedSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlan.fixedSource>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlan.active>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlan.planType>
  {
  }

  public abstract class excludePlanLevel : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.excludePlanLevel>
  {
  }

  public abstract class origPlanID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INItemPlan.origPlanID>
  {
  }

  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlan.origPlanType>
  {
  }

  public abstract class origNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemPlan.origNoteID>
  {
  }

  public abstract class origPlanLevel : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.origPlanLevel>
  {
  }

  public abstract class ignoreOrigPlan : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlan.ignoreOrigPlan>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.subItemID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.locationID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.taskID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlan.lotSerialNbr>
  {
  }

  public abstract class isTempLotSerial : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlan.isTempLotSerial>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.vendorID>
  {
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.vendorLocationID>
  {
  }

  public abstract class sourceSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.sourceSiteID>
  {
  }

  public abstract class supplyPlanID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INItemPlan.supplyPlanID>
  {
  }

  public abstract class demandPlanID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INItemPlan.demandPlanID>
  {
  }

  public abstract class origUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlan.origUOM>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlan.uOM>
  {
    public class PreventEditINUnitIfExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<INUnit.unitMultDiv, INUnit.unitRate>>.On<InventoryItemMaint>.IfExists<Select<INItemPlan, Where<INItemPlan.inventoryID, Equal<Current<INUnit.inventoryID>>, And<Current<INUnit.fromUnit>, In3<INItemPlan.uOM, INItemPlan.origUOM>, And<INItemPlan.planQty, NotEqual<decimal0>>>>>>
    {
      protected virtual bool PreventOnRowDeleting => true;

      protected virtual bool PreventOnRowPersisting => true;

      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        INItemPlan inItemPlan = (INItemPlan) firstPreventingEntity;
        EntityHelper entityHelper = new EntityHelper((PXGraph) ((PXGraphExtension<InventoryItemMaint>) this).Base);
        return PXMessages.LocalizeFormat("The unit conversion cannot be modified because it is currently in use in the following document: {0}, {1}.", new object[2]
        {
          (object) entityHelper.GetFriendlyEntityName(inItemPlan.RefNoteID),
          (object) entityHelper.GetEntityRowID(inItemPlan.RefNoteID, ", ")
        });
      }
    }
  }

  public abstract class planQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemPlan.planQty>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemPlan.refNoteID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlan.hold>
  {
  }

  public abstract class reverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlan.reverse>
  {
  }

  public abstract class isSkippedWhenBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemPlan.isSkippedWhenBackOrdered>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemPlan.Tstamp>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.bAccountID>
  {
  }

  public abstract class isTemporary : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemPlan.isTemporary>
  {
  }

  public abstract class refEntityType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemPlan.refEntityType>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.costCenterID>
  {
  }

  public abstract class kitInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemPlan.kitInventoryID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemPlan.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemPlan.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemPlan.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemPlan.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemPlan.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemPlan.lastModifiedDateTime>
  {
  }
}
