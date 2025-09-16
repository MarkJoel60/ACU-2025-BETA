// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select2<SOOrder, InnerJoin<SOOrderType, On<SOOrder.FK.OrderType>, InnerJoin<INItemPlan, On<INItemPlan.refNoteID, Equal<SOOrder.noteID>>, InnerJoin<INPlanType, On<INItemPlan.FK.PlanType>>>>, Where<INItemPlan.hold, Equal<boolFalse>, And<INItemPlan.planQty, Greater<decimal0>, And<INPlanType.isDemand, Equal<boolTrue>, And<INPlanType.isFixed, Equal<boolFalse>, And<INPlanType.isForDate, Equal<boolTrue>, And<Where<INItemPlan.fixedSource, IsNull, Or<INItemPlan.fixedSource, NotEqual<INReplenishmentSource.transfer>>>>>>>>>>))]
[Serializable]
public class SOShipmentPlan : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _DestinationSiteID;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected DateTime? _PlanDate;
  protected long? _PlanID;
  protected Decimal? _PlanQty;
  protected bool? _Reverse;
  protected short? _InclQtySOBackOrdered;
  protected short? _InclQtySOShipping;
  protected short? _InclQtySOShipped;
  protected bool? _RequireAllocation;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">LL", BqlField = typeof (SOOrder.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOOrder.orderNbr))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDefault]
  [ToSite(DisplayName = "Destination Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr), BqlField = typeof (SOOrder.destinationSiteID))]
  public virtual int? DestinationSiteID
  {
    get => this._DestinationSiteID;
    set => this._DestinationSiteID = value;
  }

  [AnyInventory(BqlField = typeof (INItemPlan.inventoryID), Enabled = false)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (INItemPlan.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBInt(BqlField = typeof (INItemPlan.siteID))]
  [PXSelector(typeof (Search<PX.Objects.IN.INSite.siteID>), CacheGlobal = true, SubstituteKey = typeof (PX.Objects.IN.INSite.siteCD))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (INItemPlan.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INItemPlan.planType))]
  public virtual string PlanType { get; set; }

  [PXDBDate(BqlField = typeof (INItemPlan.planDate))]
  [PXUIField(DisplayName = "Sched. Ship. Date")]
  public virtual DateTime? PlanDate
  {
    get => this._PlanDate;
    set => this._PlanDate = value;
  }

  [PXDBLong(IsKey = true, BqlField = typeof (INItemPlan.planID))]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXDBLong(BqlField = typeof (INItemPlan.demandPlanID))]
  public virtual long? DemandPlanID { get; set; }

  [PXDBDecimal(6, BqlField = typeof (INItemPlan.planQty))]
  public virtual Decimal? PlanQty
  {
    get => this._PlanQty;
    set => this._PlanQty = value;
  }

  [PXDBBool(BqlField = typeof (INItemPlan.reverse))]
  public virtual bool? Reverse
  {
    get => this._Reverse;
    set => this._Reverse = value;
  }

  [PXDBShort(BqlField = typeof (INPlanType.inclQtySOBackOrdered))]
  public virtual short? InclQtySOBackOrdered
  {
    get => this._InclQtySOBackOrdered;
    set => this._InclQtySOBackOrdered = value;
  }

  [PXDBShort(BqlField = typeof (INPlanType.inclQtySOShipping))]
  public virtual short? InclQtySOShipping
  {
    get => this._InclQtySOShipping;
    set => this._InclQtySOShipping = value;
  }

  [PXDBShort(BqlField = typeof (INPlanType.inclQtySOShipped))]
  public virtual short? InclQtySOShipped
  {
    get => this._InclQtySOShipped;
    set => this._InclQtySOShipped = value;
  }

  [PXDBBool(BqlField = typeof (SOOrderType.requireAllocation))]
  public virtual bool? RequireAllocation
  {
    get => this._RequireAllocation;
    set => this._RequireAllocation = value;
  }

  [PXDBBool(BqlField = typeof (SOOrder.isManualPackage))]
  public virtual bool? IsManualPackage { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipmentPlan.selected>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentPlan.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentPlan.orderNbr>
  {
  }

  public abstract class destinationSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipmentPlan.destinationSiteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentPlan.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentPlan.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipmentPlan.siteID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentPlan.lotSerialNbr>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipmentPlan.planType>
  {
  }

  public abstract class planDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOShipmentPlan.planDate>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOShipmentPlan.planID>
  {
  }

  public abstract class demandPlanID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOShipmentPlan.demandPlanID>
  {
  }

  public abstract class planQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipmentPlan.planQty>
  {
  }

  public abstract class reverse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipmentPlan.reverse>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SOShipmentPlan.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SOShipmentPlan.inclQtySOShipping>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SOShipmentPlan.inclQtySOShipped>
  {
  }

  public abstract class requireAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentPlan.requireAllocation>
  {
  }

  public abstract class isManualPackage : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipmentPlan.isManualPackage>
  {
  }
}
