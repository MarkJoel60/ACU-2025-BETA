// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPlanType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
public class INPlanType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PlanType;
  protected string _Descr;
  protected string _LocalizedDescr;
  protected bool? _IsFixed;
  protected bool? _IsSupply;
  protected bool? _IsDemand;
  protected bool? _IsForDate;
  protected short? _InclQtyFSSrvOrdBooked;
  protected short? _InclQtyFSSrvOrdAllocated;
  protected short? _InclQtyFSSrvOrdPrepared;
  protected short? _InclQtySOBackOrdered;
  protected short? _InclQtySOPrepared;
  protected short? _InclQtySOBooked;
  protected short? _InclQtySOShipped;
  protected short? _InclQtySOShipping;
  protected short? _InclQtyInTransit;
  protected short? _InclQtyInTransitToSO;
  protected short? _InclQtyPOReceipts;
  protected short? _InclQtyPOPrepared;
  protected short? _InclQtyPOOrders;
  protected short? _InclQtyINIssues;
  protected short? _InclQtyINReceipts;
  protected short? _InclQtyINAssemblyDemand;
  protected short? _InclQtyINAssemblySupply;
  protected short? _InclQtyInTransitToProduction;
  protected short? _InclQtyProductionSupplyPrepared;
  protected short? _InclQtyProductionSupply;
  protected short? _InclQtyPOFixedProductionPrepared;
  protected short? _InclQtyPOFixedProductionOrders;
  protected short? _InclQtyProductionDemandPrepared;
  protected short? _InclQtyProductionDemand;
  protected short? _InclQtyProductionAllocated;
  protected short? _InclQtySOFixedProduction;
  protected short? _InclQtyProdFixedPurchase;
  protected short? _InclQtyProdFixedProduction;
  protected short? _InclQtyProdFixedProdOrdersPrepared;
  protected short? _InclQtyProdFixedProdOrders;
  protected short? _InclQtyProdFixedSalesOrdersPrepared;
  protected short? _InclQtyProdFixedSalesOrders;
  protected short? _InclQtyINReplaned;
  protected short? _InclQtyFixedFSSrvOrd;
  protected short? _InclQtyPOFixedFSSrvOrd;
  protected short? _InclQtyPOFixedFSSrvOrdPrepared;
  protected short? _InclQtyPOFixedFSSrvOrdReceipts;
  protected short? _InclQtySOFixed;
  protected short? _InclQtyPOFixedOrders;
  protected short? _InclQtyPOFixedPrepared;
  protected short? _InclQtyPOFixedReceipts;
  protected short? _InclQtySODropShip;
  protected short? _InclQtyPODropShipOrders;
  protected short? _InclQtyPODropShipPrepared;
  protected short? _InclQtyPODropShipReceipts;
  protected bool? _DeleteOnEvent;
  protected string _ReplanOnEvent;
  protected byte[] _tstamp;

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa")]
  [PXDefault]
  [PXUIField]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXUIField]
  [INPlanType.LocalizedField(typeof (INPlanType.descr))]
  public virtual string LocalizedDescr
  {
    get => this._LocalizedDescr;
    set => this._LocalizedDescr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Fixed", Enabled = false)]
  public virtual bool? IsFixed
  {
    get => this._IsFixed;
    set => this._IsFixed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Supply", Enabled = false)]
  public virtual bool? IsSupply
  {
    get => this._IsSupply;
    set => this._IsSupply = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Demand", Enabled = false)]
  public virtual bool? IsDemand
  {
    get => this._IsDemand;
    set => this._IsDemand = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Planned for Date", Enabled = false)]
  public virtual bool? IsForDate
  {
    get => this._IsForDate;
    set => this._IsForDate = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "FS Booked", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual short? InclQtyFSSrvOrdBooked
  {
    get => this._InclQtyFSSrvOrdBooked;
    set => this._InclQtyFSSrvOrdBooked = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "FS Allocated", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual short? InclQtyFSSrvOrdAllocated
  {
    get => this._InclQtyFSSrvOrdAllocated;
    set => this._InclQtyFSSrvOrdAllocated = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "FS Prepared", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual short? InclQtyFSSrvOrdPrepared
  {
    get => this._InclQtyFSSrvOrdPrepared;
    set => this._InclQtyFSSrvOrdPrepared = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO Back Ordered", Enabled = false)]
  public virtual short? InclQtySOBackOrdered
  {
    get => this._InclQtySOBackOrdered;
    set => this._InclQtySOBackOrdered = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO Prepared", Enabled = false)]
  public virtual short? InclQtySOPrepared
  {
    get => this._InclQtySOPrepared;
    set => this._InclQtySOPrepared = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO Booked", Enabled = false)]
  public virtual short? InclQtySOBooked
  {
    get => this._InclQtySOBooked;
    set => this._InclQtySOBooked = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO Shipped", Enabled = false)]
  public virtual short? InclQtySOShipped
  {
    get => this._InclQtySOShipped;
    set => this._InclQtySOShipped = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO Allocated", Enabled = false)]
  public virtual short? InclQtySOShipping
  {
    get => this._InclQtySOShipping;
    set => this._InclQtySOShipping = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "In-Transit", Enabled = false)]
  public virtual short? InclQtyInTransit
  {
    get => this._InclQtyInTransit;
    set => this._InclQtyInTransit = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "In-Transit to SO", Enabled = false)]
  public virtual short? InclQtyInTransitToSO
  {
    get => this._InclQtyInTransitToSO;
    set => this._InclQtyInTransitToSO = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "PO Receipt", Enabled = false)]
  public virtual short? InclQtyPOReceipts
  {
    get => this._InclQtyPOReceipts;
    set => this._InclQtyPOReceipts = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "PO Prepared", Enabled = false)]
  public virtual short? InclQtyPOPrepared
  {
    get => this._InclQtyPOPrepared;
    set => this._InclQtyPOPrepared = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "PO Order", Enabled = false)]
  public virtual short? InclQtyPOOrders
  {
    get => this._InclQtyPOOrders;
    set => this._InclQtyPOOrders = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "IN Issues", Enabled = false)]
  public virtual short? InclQtyINIssues
  {
    get => this._InclQtyINIssues;
    set => this._InclQtyINIssues = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "IN Receipts", Enabled = false)]
  public virtual short? InclQtyINReceipts
  {
    get => this._InclQtyINReceipts;
    set => this._InclQtyINReceipts = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "IN Assembly Demand", Enabled = false)]
  public virtual short? InclQtyINAssemblyDemand
  {
    get => this._InclQtyINAssemblyDemand;
    set => this._InclQtyINAssemblyDemand = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "IN Assembly Supply", Enabled = false)]
  public virtual short? InclQtyINAssemblySupply
  {
    get => this._InclQtyINAssemblySupply;
    set => this._InclQtyINAssemblySupply = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity In Transit to Production.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "In Transit to Production", Enabled = false)]
  public virtual short? InclQtyInTransitToProduction
  {
    get => this._InclQtyInTransitToProduction;
    set => this._InclQtyInTransitToProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production Supply Prepared.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production Supply Prepared", Enabled = false)]
  public virtual short? InclQtyProductionSupplyPrepared
  {
    get => this._InclQtyProductionSupplyPrepared;
    set => this._InclQtyProductionSupplyPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production Supply.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production Supply", Enabled = false)]
  public virtual short? InclQtyProductionSupply
  {
    get => this._InclQtyProductionSupply;
    set => this._InclQtyProductionSupply = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Purchase for Prod. Prepared.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Purchase for Prod. Prepared", Enabled = false)]
  public virtual short? InclQtyPOFixedProductionPrepared
  {
    get => this._InclQtyPOFixedProductionPrepared;
    set => this._InclQtyPOFixedProductionPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Purchase for Production.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Purchase for Production", Enabled = false)]
  public virtual short? InclQtyPOFixedProductionOrders
  {
    get => this._InclQtyPOFixedProductionOrders;
    set => this._InclQtyPOFixedProductionOrders = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production Demand Prepared.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production Demand Prepared", Enabled = false)]
  public virtual short? InclQtyProductionDemandPrepared
  {
    get => this._InclQtyProductionDemandPrepared;
    set => this._InclQtyProductionDemandPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production Demand.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production Demand", Enabled = false)]
  public virtual short? InclQtyProductionDemand
  {
    get => this._InclQtyProductionDemand;
    set => this._InclQtyProductionDemand = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production Allocated.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production Allocated", Enabled = false)]
  public virtual short? InclQtyProductionAllocated
  {
    get => this._InclQtyProductionAllocated;
    set => this._InclQtyProductionAllocated = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity SO to Production.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO to Production", Enabled = false)]
  public virtual short? InclQtySOFixedProduction
  {
    get => this._InclQtySOFixedProduction;
    set => this._InclQtySOFixedProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production to Purchase.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production to Purchase", Enabled = false)]
  public virtual short? InclQtyProdFixedPurchase
  {
    get => this._InclQtyProdFixedPurchase;
    set => this._InclQtyProdFixedPurchase = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production to Production.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production to Production", Enabled = false)]
  public virtual short? InclQtyProdFixedProduction
  {
    get => this._InclQtyProdFixedProduction;
    set => this._InclQtyProdFixedProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production for Prod. Prepared.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production for Prod. Prepared", Enabled = false)]
  public virtual short? InclQtyProdFixedProdOrdersPrepared
  {
    get => this._InclQtyProdFixedProdOrdersPrepared;
    set => this._InclQtyProdFixedProdOrdersPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production for Production.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production for Production", Enabled = false)]
  public virtual short? InclQtyProdFixedProdOrders
  {
    get => this._InclQtyProdFixedProdOrders;
    set => this._InclQtyProdFixedProdOrders = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production for SO Prepared.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production for SO Prepared", Enabled = false)]
  public virtual short? InclQtyProdFixedSalesOrdersPrepared
  {
    get => this._InclQtyProdFixedSalesOrdersPrepared;
    set => this._InclQtyProdFixedSalesOrdersPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies (if set to <c>1</c>) that the plan type impacts the quantity Production for SO.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Production for SO", Enabled = false)]
  public virtual short? InclQtyProdFixedSalesOrders
  {
    get => this._InclQtyProdFixedSalesOrders;
    set => this._InclQtyProdFixedSalesOrders = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "IN Replanned", Enabled = false)]
  public virtual short? InclQtyINReplaned
  {
    get => this._InclQtyINReplaned;
    set => this._InclQtyINReplaned = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "FS to Purchase", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual short? InclQtyFixedFSSrvOrd
  {
    get => this._InclQtyFixedFSSrvOrd;
    set => this._InclQtyFixedFSSrvOrd = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Purchase for FS", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual short? InclQtyPOFixedFSSrvOrd
  {
    get => this._InclQtyPOFixedFSSrvOrd;
    set => this._InclQtyPOFixedFSSrvOrd = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Purchase for FS Prepared", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual short? InclQtyPOFixedFSSrvOrdPrepared
  {
    get => this._InclQtyPOFixedFSSrvOrdPrepared;
    set => this._InclQtyPOFixedFSSrvOrdPrepared = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Receipts for FS", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual short? InclQtyPOFixedFSSrvOrdReceipts
  {
    get => this._InclQtyPOFixedFSSrvOrdReceipts;
    set => this._InclQtyPOFixedFSSrvOrdReceipts = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO to Purchase", Enabled = false)]
  public virtual short? InclQtySOFixed
  {
    get => this._InclQtySOFixed;
    set => this._InclQtySOFixed = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Purchase for SO", Enabled = false)]
  public virtual short? InclQtyPOFixedOrders
  {
    get => this._InclQtyPOFixedOrders;
    set => this._InclQtyPOFixedOrders = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Purchase for SO Prepared", Enabled = false)]
  public virtual short? InclQtyPOFixedPrepared
  {
    get => this._InclQtyPOFixedPrepared;
    set => this._InclQtyPOFixedPrepared = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Receipts for SO", Enabled = false)]
  public virtual short? InclQtyPOFixedReceipts
  {
    get => this._InclQtyPOFixedReceipts;
    set => this._InclQtyPOFixedReceipts = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "SO to Drop-Ship", Enabled = false)]
  public virtual short? InclQtySODropShip
  {
    get => this._InclQtySODropShip;
    set => this._InclQtySODropShip = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Drop-Ship for SO", Enabled = false)]
  public virtual short? InclQtyPODropShipOrders
  {
    get => this._InclQtyPODropShipOrders;
    set => this._InclQtyPODropShipOrders = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Drop-Ship for SO Prepared", Enabled = false)]
  public virtual short? InclQtyPODropShipPrepared
  {
    get => this._InclQtyPODropShipPrepared;
    set => this._InclQtyPODropShipPrepared = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Drop-Ship for SO Receipts", Enabled = false)]
  public virtual short? InclQtyPODropShipReceipts
  {
    get => this._InclQtyPODropShipReceipts;
    set => this._InclQtyPODropShipReceipts = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Delete On Event", Enabled = false)]
  public virtual bool? DeleteOnEvent
  {
    get => this._DeleteOnEvent;
    set => this._DeleteOnEvent = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Replan On Event")]
  public virtual string ReplanOnEvent
  {
    get => this._ReplanOnEvent;
    set => this._ReplanOnEvent = value;
  }

  /// <exclude />
  [PXBool]
  [PXInternalUseOnly]
  public virtual bool? DeleteOperation { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public static INPlanType operator -(INPlanType t1)
  {
    INPlanType copy = PXCache<INPlanType>.CreateCopy(t1);
    short? nullable1 = copy.InclQtyINIssues;
    int? nullable2 = nullable1.HasValue ? new int?((int) -nullable1.GetValueOrDefault()) : new int?();
    copy.InclQtyINIssues = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyINReceipts;
    int? nullable3;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable3 = nullable2;
    }
    else
      nullable3 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable3;
    copy.InclQtyINReceipts = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyInTransit;
    int? nullable4;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable4;
    copy.InclQtyInTransit = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyInTransitToSO;
    int? nullable5;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable5;
    copy.InclQtyInTransitToSO = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOReceipts;
    int? nullable6;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable6;
    copy.InclQtyPOReceipts = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOPrepared;
    int? nullable7;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable7 = nullable2;
    }
    else
      nullable7 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable7;
    copy.InclQtyPOPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOOrders;
    int? nullable8;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable8 = nullable2;
    }
    else
      nullable8 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable8;
    copy.InclQtyPOOrders = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyFSSrvOrdBooked;
    int? nullable9;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable9 = nullable2;
    }
    else
      nullable9 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable9;
    copy.InclQtyFSSrvOrdBooked = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyFSSrvOrdAllocated;
    int? nullable10;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable10 = nullable2;
    }
    else
      nullable10 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable10;
    copy.InclQtyFSSrvOrdAllocated = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyFSSrvOrdPrepared;
    int? nullable11;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable11 = nullable2;
    }
    else
      nullable11 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable11;
    copy.InclQtyFSSrvOrdPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySOBackOrdered;
    int? nullable12;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable12 = nullable2;
    }
    else
      nullable12 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable12;
    copy.InclQtySOBackOrdered = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySOPrepared;
    int? nullable13;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable13 = nullable2;
    }
    else
      nullable13 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable13;
    copy.InclQtySOPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySOBooked;
    int? nullable14;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable14 = nullable2;
    }
    else
      nullable14 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable14;
    copy.InclQtySOBooked = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySOShipped;
    int? nullable15;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable15 = nullable2;
    }
    else
      nullable15 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable15;
    copy.InclQtySOShipped = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySOShipping;
    int? nullable16;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable16 = nullable2;
    }
    else
      nullable16 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable16;
    copy.InclQtySOShipping = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyINAssemblySupply;
    int? nullable17;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable17 = nullable2;
    }
    else
      nullable17 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable17;
    copy.InclQtyINAssemblySupply = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyINAssemblyDemand;
    int? nullable18;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable18 = nullable2;
    }
    else
      nullable18 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable18;
    copy.InclQtyINAssemblyDemand = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyInTransitToProduction;
    int? nullable19;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable19 = nullable2;
    }
    else
      nullable19 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable19;
    copy.InclQtyInTransitToProduction = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProductionSupplyPrepared;
    int? nullable20;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable20 = nullable2;
    }
    else
      nullable20 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable20;
    copy.InclQtyProductionSupplyPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProductionSupply;
    int? nullable21;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable21 = nullable2;
    }
    else
      nullable21 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable21;
    copy.InclQtyProductionSupply = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedProductionPrepared;
    int? nullable22;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable22 = nullable2;
    }
    else
      nullable22 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable22;
    copy.InclQtyPOFixedProductionPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedProductionOrders;
    int? nullable23;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable23 = nullable2;
    }
    else
      nullable23 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable23;
    copy.InclQtyPOFixedProductionOrders = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProductionDemandPrepared;
    int? nullable24;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable24 = nullable2;
    }
    else
      nullable24 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable24;
    copy.InclQtyProductionDemandPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProductionDemand;
    int? nullable25;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable25 = nullable2;
    }
    else
      nullable25 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable25;
    copy.InclQtyProductionDemand = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProductionAllocated;
    int? nullable26;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable26 = nullable2;
    }
    else
      nullable26 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable26;
    copy.InclQtyProductionAllocated = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySOFixedProduction;
    int? nullable27;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable27 = nullable2;
    }
    else
      nullable27 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable27;
    copy.InclQtySOFixedProduction = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProdFixedPurchase;
    int? nullable28;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable28 = nullable2;
    }
    else
      nullable28 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable28;
    copy.InclQtyProdFixedPurchase = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProdFixedProduction;
    int? nullable29;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable29 = nullable2;
    }
    else
      nullable29 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable29;
    copy.InclQtyProdFixedProduction = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProdFixedProdOrdersPrepared;
    int? nullable30;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable30 = nullable2;
    }
    else
      nullable30 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable30;
    copy.InclQtyProdFixedProdOrdersPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProdFixedProdOrders;
    int? nullable31;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable31 = nullable2;
    }
    else
      nullable31 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable31;
    copy.InclQtyProdFixedProdOrders = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProdFixedSalesOrdersPrepared;
    int? nullable32;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable32 = nullable2;
    }
    else
      nullable32 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable32;
    copy.InclQtyProdFixedSalesOrdersPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyProdFixedSalesOrders;
    int? nullable33;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable33 = nullable2;
    }
    else
      nullable33 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable33;
    copy.InclQtyProdFixedSalesOrders = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyINReplaned;
    int? nullable34;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable34 = nullable2;
    }
    else
      nullable34 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable34;
    copy.InclQtyINReplaned = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyFixedFSSrvOrd;
    int? nullable35;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable35 = nullable2;
    }
    else
      nullable35 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable35;
    copy.InclQtyFixedFSSrvOrd = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedFSSrvOrd;
    int? nullable36;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable36 = nullable2;
    }
    else
      nullable36 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable36;
    copy.InclQtyPOFixedFSSrvOrd = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedFSSrvOrdPrepared;
    int? nullable37;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable37 = nullable2;
    }
    else
      nullable37 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable37;
    copy.InclQtyPOFixedFSSrvOrdPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedFSSrvOrdReceipts;
    int? nullable38;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable38 = nullable2;
    }
    else
      nullable38 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable38;
    copy.InclQtyPOFixedFSSrvOrdReceipts = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySOFixed;
    int? nullable39;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable39 = nullable2;
    }
    else
      nullable39 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable39;
    copy.InclQtySOFixed = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedOrders;
    int? nullable40;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable40 = nullable2;
    }
    else
      nullable40 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable40;
    copy.InclQtyPOFixedOrders = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedPrepared;
    int? nullable41;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable41 = nullable2;
    }
    else
      nullable41 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable41;
    copy.InclQtyPOFixedPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPOFixedReceipts;
    int? nullable42;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable42 = nullable2;
    }
    else
      nullable42 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable42;
    copy.InclQtyPOFixedReceipts = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtySODropShip;
    int? nullable43;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable43 = nullable2;
    }
    else
      nullable43 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable43;
    copy.InclQtySODropShip = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPODropShipOrders;
    int? nullable44;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable44 = nullable2;
    }
    else
      nullable44 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable44;
    copy.InclQtyPODropShipOrders = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPODropShipPrepared;
    int? nullable45;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable45 = nullable2;
    }
    else
      nullable45 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable45;
    copy.InclQtyPODropShipPrepared = new short?((short) nullable2.Value);
    nullable1 = copy.InclQtyPODropShipReceipts;
    int? nullable46;
    if (!nullable1.HasValue)
    {
      nullable2 = new int?();
      nullable46 = nullable2;
    }
    else
      nullable46 = new int?((int) -nullable1.GetValueOrDefault());
    nullable2 = nullable46;
    copy.InclQtyPODropShipReceipts = new short?((short) nullable2.Value);
    copy.DeleteOperation = new bool?(true);
    return copy;
  }

  public static INPlanType operator -(INPlanType t1, INPlanType t2)
  {
    INPlanType copy = PXCache<INPlanType>.CreateCopy(t1);
    short? nullable1 = t1.InclQtyINIssues;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = t2.InclQtyINIssues;
    int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int? nullable4 = nullable2.HasValue & nullable3.HasValue ? new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new int?();
    copy.InclQtyINIssues = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyINReceipts;
    int? nullable5;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable5 = nullable4;
    }
    else
      nullable5 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable5;
    nullable1 = t2.InclQtyINReceipts;
    int? nullable6;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable6 = nullable4;
    }
    else
      nullable6 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable6;
    int? nullable7;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable7 = nullable4;
    }
    else
      nullable7 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable7;
    copy.InclQtyINReceipts = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyInTransit;
    int? nullable8;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable8 = nullable4;
    }
    else
      nullable8 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable8;
    nullable1 = t2.InclQtyInTransit;
    int? nullable9;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable9 = nullable4;
    }
    else
      nullable9 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable9;
    int? nullable10;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable10 = nullable4;
    }
    else
      nullable10 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable10;
    copy.InclQtyInTransit = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyInTransitToSO;
    int? nullable11;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable11 = nullable4;
    }
    else
      nullable11 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable11;
    nullable1 = t2.InclQtyInTransitToSO;
    int? nullable12;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable12 = nullable4;
    }
    else
      nullable12 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable12;
    int? nullable13;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable13 = nullable4;
    }
    else
      nullable13 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable13;
    copy.InclQtyInTransitToSO = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOReceipts;
    int? nullable14;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable14 = nullable4;
    }
    else
      nullable14 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable14;
    nullable1 = t2.InclQtyPOReceipts;
    int? nullable15;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable15 = nullable4;
    }
    else
      nullable15 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable15;
    int? nullable16;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable16 = nullable4;
    }
    else
      nullable16 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable16;
    copy.InclQtyPOReceipts = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOPrepared;
    int? nullable17;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable17 = nullable4;
    }
    else
      nullable17 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable17;
    nullable1 = t2.InclQtyPOPrepared;
    int? nullable18;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable18 = nullable4;
    }
    else
      nullable18 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable18;
    int? nullable19;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable19 = nullable4;
    }
    else
      nullable19 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable19;
    copy.InclQtyPOPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOOrders;
    int? nullable20;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable20 = nullable4;
    }
    else
      nullable20 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable20;
    nullable1 = t2.InclQtyPOOrders;
    int? nullable21;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable21 = nullable4;
    }
    else
      nullable21 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable21;
    int? nullable22;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable22 = nullable4;
    }
    else
      nullable22 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable22;
    copy.InclQtyPOOrders = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyFSSrvOrdBooked;
    int? nullable23;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable23 = nullable4;
    }
    else
      nullable23 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable23;
    nullable1 = t2.InclQtyFSSrvOrdBooked;
    int? nullable24;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable24 = nullable4;
    }
    else
      nullable24 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable24;
    int? nullable25;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable25 = nullable4;
    }
    else
      nullable25 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable25;
    copy.InclQtyFSSrvOrdBooked = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyFSSrvOrdAllocated;
    int? nullable26;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable26 = nullable4;
    }
    else
      nullable26 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable26;
    nullable1 = t2.InclQtyFSSrvOrdAllocated;
    int? nullable27;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable27 = nullable4;
    }
    else
      nullable27 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable27;
    int? nullable28;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable28 = nullable4;
    }
    else
      nullable28 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable28;
    copy.InclQtyFSSrvOrdAllocated = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyFSSrvOrdPrepared;
    int? nullable29;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable29 = nullable4;
    }
    else
      nullable29 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable29;
    nullable1 = t2.InclQtyFSSrvOrdPrepared;
    int? nullable30;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable30 = nullable4;
    }
    else
      nullable30 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable30;
    int? nullable31;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable31 = nullable4;
    }
    else
      nullable31 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable31;
    copy.InclQtyFSSrvOrdPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySOBackOrdered;
    int? nullable32;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable32 = nullable4;
    }
    else
      nullable32 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable32;
    nullable1 = t2.InclQtySOBackOrdered;
    int? nullable33;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable33 = nullable4;
    }
    else
      nullable33 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable33;
    int? nullable34;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable34 = nullable4;
    }
    else
      nullable34 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable34;
    copy.InclQtySOBackOrdered = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySOPrepared;
    int? nullable35;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable35 = nullable4;
    }
    else
      nullable35 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable35;
    nullable1 = t2.InclQtySOPrepared;
    int? nullable36;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable36 = nullable4;
    }
    else
      nullable36 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable36;
    int? nullable37;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable37 = nullable4;
    }
    else
      nullable37 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable37;
    copy.InclQtySOPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySOBooked;
    int? nullable38;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable38 = nullable4;
    }
    else
      nullable38 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable38;
    nullable1 = t2.InclQtySOBooked;
    int? nullable39;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable39 = nullable4;
    }
    else
      nullable39 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable39;
    int? nullable40;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable40 = nullable4;
    }
    else
      nullable40 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable40;
    copy.InclQtySOBooked = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySOShipped;
    int? nullable41;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable41 = nullable4;
    }
    else
      nullable41 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable41;
    nullable1 = t2.InclQtySOShipped;
    int? nullable42;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable42 = nullable4;
    }
    else
      nullable42 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable42;
    int? nullable43;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable43 = nullable4;
    }
    else
      nullable43 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable43;
    copy.InclQtySOShipped = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySOShipping;
    int? nullable44;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable44 = nullable4;
    }
    else
      nullable44 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable44;
    nullable1 = t2.InclQtySOShipping;
    int? nullable45;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable45 = nullable4;
    }
    else
      nullable45 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable45;
    int? nullable46;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable46 = nullable4;
    }
    else
      nullable46 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable46;
    copy.InclQtySOShipping = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyINAssemblySupply;
    int? nullable47;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable47 = nullable4;
    }
    else
      nullable47 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable47;
    nullable1 = t2.InclQtyINAssemblySupply;
    int? nullable48;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable48 = nullable4;
    }
    else
      nullable48 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable48;
    int? nullable49;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable49 = nullable4;
    }
    else
      nullable49 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable49;
    copy.InclQtyINAssemblySupply = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyINAssemblyDemand;
    int? nullable50;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable50 = nullable4;
    }
    else
      nullable50 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable50;
    nullable1 = t2.InclQtyINAssemblyDemand;
    int? nullable51;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable51 = nullable4;
    }
    else
      nullable51 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable51;
    int? nullable52;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable52 = nullable4;
    }
    else
      nullable52 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable52;
    copy.InclQtyINAssemblyDemand = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyInTransitToProduction;
    int? nullable53;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable53 = nullable4;
    }
    else
      nullable53 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable53;
    nullable1 = t2.InclQtyInTransitToProduction;
    int? nullable54;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable54 = nullable4;
    }
    else
      nullable54 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable54;
    int? nullable55;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable55 = nullable4;
    }
    else
      nullable55 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable55;
    copy.InclQtyInTransitToProduction = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProductionSupplyPrepared;
    int? nullable56;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable56 = nullable4;
    }
    else
      nullable56 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable56;
    nullable1 = t2.InclQtyProductionSupplyPrepared;
    int? nullable57;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable57 = nullable4;
    }
    else
      nullable57 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable57;
    int? nullable58;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable58 = nullable4;
    }
    else
      nullable58 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable58;
    copy.InclQtyProductionSupplyPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProductionSupply;
    int? nullable59;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable59 = nullable4;
    }
    else
      nullable59 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable59;
    nullable1 = t2.InclQtyProductionSupply;
    int? nullable60;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable60 = nullable4;
    }
    else
      nullable60 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable60;
    int? nullable61;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable61 = nullable4;
    }
    else
      nullable61 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable61;
    copy.InclQtyProductionSupply = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedProductionPrepared;
    int? nullable62;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable62 = nullable4;
    }
    else
      nullable62 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable62;
    nullable1 = t2.InclQtyPOFixedProductionPrepared;
    int? nullable63;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable63 = nullable4;
    }
    else
      nullable63 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable63;
    int? nullable64;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable64 = nullable4;
    }
    else
      nullable64 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable64;
    copy.InclQtyPOFixedProductionPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedProductionOrders;
    int? nullable65;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable65 = nullable4;
    }
    else
      nullable65 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable65;
    nullable1 = t2.InclQtyPOFixedProductionOrders;
    int? nullable66;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable66 = nullable4;
    }
    else
      nullable66 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable66;
    int? nullable67;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable67 = nullable4;
    }
    else
      nullable67 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable67;
    copy.InclQtyPOFixedProductionOrders = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProductionDemandPrepared;
    int? nullable68;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable68 = nullable4;
    }
    else
      nullable68 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable68;
    nullable1 = t2.InclQtyProductionDemandPrepared;
    int? nullable69;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable69 = nullable4;
    }
    else
      nullable69 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable69;
    int? nullable70;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable70 = nullable4;
    }
    else
      nullable70 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable70;
    copy.InclQtyProductionDemandPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProductionDemand;
    int? nullable71;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable71 = nullable4;
    }
    else
      nullable71 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable71;
    nullable1 = t2.InclQtyProductionDemand;
    int? nullable72;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable72 = nullable4;
    }
    else
      nullable72 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable72;
    int? nullable73;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable73 = nullable4;
    }
    else
      nullable73 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable73;
    copy.InclQtyProductionDemand = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProductionAllocated;
    int? nullable74;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable74 = nullable4;
    }
    else
      nullable74 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable74;
    nullable1 = t2.InclQtyProductionAllocated;
    int? nullable75;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable75 = nullable4;
    }
    else
      nullable75 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable75;
    int? nullable76;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable76 = nullable4;
    }
    else
      nullable76 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable76;
    copy.InclQtyProductionAllocated = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySOFixedProduction;
    int? nullable77;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable77 = nullable4;
    }
    else
      nullable77 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable77;
    nullable1 = t2.InclQtySOFixedProduction;
    int? nullable78;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable78 = nullable4;
    }
    else
      nullable78 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable78;
    int? nullable79;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable79 = nullable4;
    }
    else
      nullable79 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable79;
    copy.InclQtySOFixedProduction = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProdFixedPurchase;
    int? nullable80;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable80 = nullable4;
    }
    else
      nullable80 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable80;
    nullable1 = t2.InclQtyProdFixedPurchase;
    int? nullable81;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable81 = nullable4;
    }
    else
      nullable81 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable81;
    int? nullable82;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable82 = nullable4;
    }
    else
      nullable82 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable82;
    copy.InclQtyProdFixedPurchase = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProdFixedProduction;
    int? nullable83;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable83 = nullable4;
    }
    else
      nullable83 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable83;
    nullable1 = t2.InclQtyProdFixedProduction;
    int? nullable84;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable84 = nullable4;
    }
    else
      nullable84 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable84;
    int? nullable85;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable85 = nullable4;
    }
    else
      nullable85 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable85;
    copy.InclQtyProdFixedProduction = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProdFixedProdOrdersPrepared;
    int? nullable86;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable86 = nullable4;
    }
    else
      nullable86 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable86;
    nullable1 = t2.InclQtyProdFixedProdOrdersPrepared;
    int? nullable87;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable87 = nullable4;
    }
    else
      nullable87 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable87;
    int? nullable88;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable88 = nullable4;
    }
    else
      nullable88 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable88;
    copy.InclQtyProdFixedProdOrdersPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProdFixedProdOrders;
    int? nullable89;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable89 = nullable4;
    }
    else
      nullable89 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable89;
    nullable1 = t2.InclQtyProdFixedProdOrders;
    int? nullable90;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable90 = nullable4;
    }
    else
      nullable90 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable90;
    int? nullable91;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable91 = nullable4;
    }
    else
      nullable91 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable91;
    copy.InclQtyProdFixedProdOrders = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProdFixedSalesOrdersPrepared;
    int? nullable92;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable92 = nullable4;
    }
    else
      nullable92 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable92;
    nullable1 = t2.InclQtyProdFixedSalesOrdersPrepared;
    int? nullable93;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable93 = nullable4;
    }
    else
      nullable93 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable93;
    int? nullable94;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable94 = nullable4;
    }
    else
      nullable94 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable94;
    copy.InclQtyProdFixedSalesOrdersPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyProdFixedSalesOrders;
    int? nullable95;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable95 = nullable4;
    }
    else
      nullable95 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable95;
    nullable1 = t2.InclQtyProdFixedSalesOrders;
    int? nullable96;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable96 = nullable4;
    }
    else
      nullable96 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable96;
    int? nullable97;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable97 = nullable4;
    }
    else
      nullable97 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable97;
    copy.InclQtyProdFixedSalesOrders = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyINReplaned;
    int? nullable98;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable98 = nullable4;
    }
    else
      nullable98 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable98;
    nullable1 = t2.InclQtyINReplaned;
    int? nullable99;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable99 = nullable4;
    }
    else
      nullable99 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable99;
    int? nullable100;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable100 = nullable4;
    }
    else
      nullable100 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable100;
    copy.InclQtyINReplaned = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyFixedFSSrvOrd;
    int? nullable101;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable101 = nullable4;
    }
    else
      nullable101 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable101;
    nullable1 = t2.InclQtyFixedFSSrvOrd;
    int? nullable102;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable102 = nullable4;
    }
    else
      nullable102 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable102;
    int? nullable103;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable103 = nullable4;
    }
    else
      nullable103 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable103;
    copy.InclQtyFixedFSSrvOrd = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedFSSrvOrd;
    int? nullable104;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable104 = nullable4;
    }
    else
      nullable104 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable104;
    nullable1 = t2.InclQtyPOFixedFSSrvOrd;
    int? nullable105;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable105 = nullable4;
    }
    else
      nullable105 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable105;
    int? nullable106;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable106 = nullable4;
    }
    else
      nullable106 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable106;
    copy.InclQtyPOFixedFSSrvOrd = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedFSSrvOrdPrepared;
    int? nullable107;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable107 = nullable4;
    }
    else
      nullable107 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable107;
    nullable1 = t2.InclQtyPOFixedFSSrvOrdPrepared;
    int? nullable108;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable108 = nullable4;
    }
    else
      nullable108 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable108;
    int? nullable109;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable109 = nullable4;
    }
    else
      nullable109 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable109;
    copy.InclQtyPOFixedFSSrvOrdPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedFSSrvOrdReceipts;
    int? nullable110;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable110 = nullable4;
    }
    else
      nullable110 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable110;
    nullable1 = t2.InclQtyPOFixedFSSrvOrdReceipts;
    int? nullable111;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable111 = nullable4;
    }
    else
      nullable111 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable111;
    int? nullable112;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable112 = nullable4;
    }
    else
      nullable112 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable112;
    copy.InclQtyPOFixedFSSrvOrdReceipts = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySOFixed;
    int? nullable113;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable113 = nullable4;
    }
    else
      nullable113 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable113;
    nullable1 = t2.InclQtySOFixed;
    int? nullable114;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable114 = nullable4;
    }
    else
      nullable114 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable114;
    int? nullable115;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable115 = nullable4;
    }
    else
      nullable115 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable115;
    copy.InclQtySOFixed = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedOrders;
    int? nullable116;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable116 = nullable4;
    }
    else
      nullable116 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable116;
    nullable1 = t2.InclQtyPOFixedOrders;
    int? nullable117;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable117 = nullable4;
    }
    else
      nullable117 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable117;
    int? nullable118;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable118 = nullable4;
    }
    else
      nullable118 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable118;
    copy.InclQtyPOFixedOrders = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedPrepared;
    int? nullable119;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable119 = nullable4;
    }
    else
      nullable119 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable119;
    nullable1 = t2.InclQtyPOFixedPrepared;
    int? nullable120;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable120 = nullable4;
    }
    else
      nullable120 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable120;
    int? nullable121;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable121 = nullable4;
    }
    else
      nullable121 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable121;
    copy.InclQtyPOFixedPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPOFixedReceipts;
    int? nullable122;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable122 = nullable4;
    }
    else
      nullable122 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable122;
    nullable1 = t2.InclQtyPOFixedReceipts;
    int? nullable123;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable123 = nullable4;
    }
    else
      nullable123 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable123;
    int? nullable124;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable124 = nullable4;
    }
    else
      nullable124 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable124;
    copy.InclQtyPOFixedReceipts = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtySODropShip;
    int? nullable125;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable125 = nullable4;
    }
    else
      nullable125 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable125;
    nullable1 = t2.InclQtySODropShip;
    int? nullable126;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable126 = nullable4;
    }
    else
      nullable126 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable126;
    int? nullable127;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable127 = nullable4;
    }
    else
      nullable127 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable127;
    copy.InclQtySODropShip = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPODropShipOrders;
    int? nullable128;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable128 = nullable4;
    }
    else
      nullable128 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable128;
    nullable1 = t2.InclQtyPODropShipOrders;
    int? nullable129;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable129 = nullable4;
    }
    else
      nullable129 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable129;
    int? nullable130;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable130 = nullable4;
    }
    else
      nullable130 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable130;
    copy.InclQtyPODropShipOrders = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPODropShipPrepared;
    int? nullable131;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable131 = nullable4;
    }
    else
      nullable131 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable131;
    nullable1 = t2.InclQtyPODropShipPrepared;
    int? nullable132;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable132 = nullable4;
    }
    else
      nullable132 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable132;
    int? nullable133;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable133 = nullable4;
    }
    else
      nullable133 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable133;
    copy.InclQtyPODropShipPrepared = new short?((short) nullable4.Value);
    nullable1 = t1.InclQtyPODropShipReceipts;
    int? nullable134;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable134 = nullable4;
    }
    else
      nullable134 = new int?((int) nullable1.GetValueOrDefault());
    nullable2 = nullable134;
    nullable1 = t2.InclQtyPODropShipReceipts;
    int? nullable135;
    if (!nullable1.HasValue)
    {
      nullable4 = new int?();
      nullable135 = nullable4;
    }
    else
      nullable135 = new int?((int) nullable1.GetValueOrDefault());
    nullable3 = nullable135;
    int? nullable136;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable4 = new int?();
      nullable136 = nullable4;
    }
    else
      nullable136 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
    nullable4 = nullable136;
    copy.InclQtyPODropShipReceipts = new short?((short) nullable4.Value);
    return copy;
  }

  public static INPlanType operator +(INPlanType t1, INPlanType t2) => t1 - -t2;

  public static INPlanType FromInt(int n)
  {
    return new INPlanType()
    {
      InclQtyINIssues = new short?((short) n),
      InclQtyINReceipts = new short?((short) n),
      InclQtyInTransit = new short?((short) n),
      InclQtyInTransitToSO = new short?((short) n),
      InclQtyPOReceipts = new short?((short) n),
      InclQtyPOPrepared = new short?((short) n),
      InclQtyPOOrders = new short?((short) n),
      InclQtyFSSrvOrdBooked = new short?((short) n),
      InclQtyFSSrvOrdAllocated = new short?((short) n),
      InclQtyFSSrvOrdPrepared = new short?((short) n),
      InclQtySOBackOrdered = new short?((short) n),
      InclQtySOPrepared = new short?((short) n),
      InclQtySOBooked = new short?((short) n),
      InclQtySOShipped = new short?((short) n),
      InclQtySOShipping = new short?((short) n),
      InclQtyINAssemblySupply = new short?((short) n),
      InclQtyINAssemblyDemand = new short?((short) n),
      InclQtyInTransitToProduction = new short?((short) n),
      InclQtyProductionSupplyPrepared = new short?((short) n),
      InclQtyProductionSupply = new short?((short) n),
      InclQtyPOFixedProductionPrepared = new short?((short) n),
      InclQtyPOFixedProductionOrders = new short?((short) n),
      InclQtyProductionDemandPrepared = new short?((short) n),
      InclQtyProductionDemand = new short?((short) n),
      InclQtyProductionAllocated = new short?((short) n),
      InclQtySOFixedProduction = new short?((short) n),
      InclQtyProdFixedPurchase = new short?((short) n),
      InclQtyProdFixedProduction = new short?((short) n),
      InclQtyProdFixedProdOrdersPrepared = new short?((short) n),
      InclQtyProdFixedProdOrders = new short?((short) n),
      InclQtyProdFixedSalesOrdersPrepared = new short?((short) n),
      InclQtyProdFixedSalesOrders = new short?((short) n),
      InclQtyINReplaned = new short?((short) n),
      InclQtyFixedFSSrvOrd = new short?((short) n),
      InclQtyPOFixedFSSrvOrd = new short?((short) n),
      InclQtyPOFixedFSSrvOrdPrepared = new short?((short) n),
      InclQtyPOFixedFSSrvOrdReceipts = new short?((short) n),
      InclQtySOFixed = new short?((short) n),
      InclQtyPOFixedOrders = new short?((short) n),
      InclQtyPOFixedPrepared = new short?((short) n),
      InclQtyPOFixedReceipts = new short?((short) n),
      InclQtySODropShip = new short?((short) n),
      InclQtyPODropShipOrders = new short?((short) n),
      InclQtyPODropShipPrepared = new short?((short) n),
      InclQtyPODropShipReceipts = new short?((short) n)
    };
  }

  public static int ToInt(INPlanType t)
  {
    short? inclQtyInIssues1 = t.InclQtyINIssues;
    int? nullable = inclQtyInIssues1.HasValue ? new int?((int) inclQtyInIssues1.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (!(nullable.GetValueOrDefault() > num1 & nullable.HasValue))
    {
      short? inclQtyInReceipts1 = t.InclQtyINReceipts;
      nullable = inclQtyInReceipts1.HasValue ? new int?((int) inclQtyInReceipts1.GetValueOrDefault()) : new int?();
      int num2 = 0;
      if (!(nullable.GetValueOrDefault() > num2 & nullable.HasValue))
      {
        short? inclQtyInTransit1 = t.InclQtyInTransit;
        nullable = inclQtyInTransit1.HasValue ? new int?((int) inclQtyInTransit1.GetValueOrDefault()) : new int?();
        int num3 = 0;
        if (!(nullable.GetValueOrDefault() > num3 & nullable.HasValue))
        {
          short? qtyInTransitToSo1 = t.InclQtyInTransitToSO;
          nullable = qtyInTransitToSo1.HasValue ? new int?((int) qtyInTransitToSo1.GetValueOrDefault()) : new int?();
          int num4 = 0;
          if (!(nullable.GetValueOrDefault() > num4 & nullable.HasValue))
          {
            short? inclQtyPoReceipts1 = t.InclQtyPOReceipts;
            nullable = inclQtyPoReceipts1.HasValue ? new int?((int) inclQtyPoReceipts1.GetValueOrDefault()) : new int?();
            int num5 = 0;
            if (!(nullable.GetValueOrDefault() > num5 & nullable.HasValue))
            {
              short? inclQtyPoPrepared1 = t.InclQtyPOPrepared;
              nullable = inclQtyPoPrepared1.HasValue ? new int?((int) inclQtyPoPrepared1.GetValueOrDefault()) : new int?();
              int num6 = 0;
              if (!(nullable.GetValueOrDefault() > num6 & nullable.HasValue))
              {
                short? inclQtyPoOrders1 = t.InclQtyPOOrders;
                nullable = inclQtyPoOrders1.HasValue ? new int?((int) inclQtyPoOrders1.GetValueOrDefault()) : new int?();
                int num7 = 0;
                if (!(nullable.GetValueOrDefault() > num7 & nullable.HasValue))
                {
                  short? qtySoBackOrdered1 = t.InclQtySOBackOrdered;
                  nullable = qtySoBackOrdered1.HasValue ? new int?((int) qtySoBackOrdered1.GetValueOrDefault()) : new int?();
                  int num8 = 0;
                  if (!(nullable.GetValueOrDefault() > num8 & nullable.HasValue))
                  {
                    short? inclQtySoPrepared1 = t.InclQtySOPrepared;
                    nullable = inclQtySoPrepared1.HasValue ? new int?((int) inclQtySoPrepared1.GetValueOrDefault()) : new int?();
                    int num9 = 0;
                    if (!(nullable.GetValueOrDefault() > num9 & nullable.HasValue))
                    {
                      short? inclQtySoBooked1 = t.InclQtySOBooked;
                      nullable = inclQtySoBooked1.HasValue ? new int?((int) inclQtySoBooked1.GetValueOrDefault()) : new int?();
                      int num10 = 0;
                      if (!(nullable.GetValueOrDefault() > num10 & nullable.HasValue))
                      {
                        short? inclQtySoShipped1 = t.InclQtySOShipped;
                        nullable = inclQtySoShipped1.HasValue ? new int?((int) inclQtySoShipped1.GetValueOrDefault()) : new int?();
                        int num11 = 0;
                        if (!(nullable.GetValueOrDefault() > num11 & nullable.HasValue))
                        {
                          short? inclQtySoShipping1 = t.InclQtySOShipping;
                          nullable = inclQtySoShipping1.HasValue ? new int?((int) inclQtySoShipping1.GetValueOrDefault()) : new int?();
                          int num12 = 0;
                          if (!(nullable.GetValueOrDefault() > num12 & nullable.HasValue))
                          {
                            short? inAssemblySupply1 = t.InclQtyINAssemblySupply;
                            nullable = inAssemblySupply1.HasValue ? new int?((int) inAssemblySupply1.GetValueOrDefault()) : new int?();
                            int num13 = 0;
                            if (!(nullable.GetValueOrDefault() > num13 & nullable.HasValue))
                            {
                              short? inAssemblyDemand1 = t.InclQtyINAssemblyDemand;
                              nullable = inAssemblyDemand1.HasValue ? new int?((int) inAssemblyDemand1.GetValueOrDefault()) : new int?();
                              int num14 = 0;
                              if (!(nullable.GetValueOrDefault() > num14 & nullable.HasValue))
                              {
                                short? inclQtyInReplaned1 = t.InclQtyINReplaned;
                                nullable = inclQtyInReplaned1.HasValue ? new int?((int) inclQtyInReplaned1.GetValueOrDefault()) : new int?();
                                int num15 = 0;
                                if (!(nullable.GetValueOrDefault() > num15 & nullable.HasValue))
                                {
                                  short? qtyFixedFsSrvOrd1 = t.InclQtyFixedFSSrvOrd;
                                  nullable = qtyFixedFsSrvOrd1.HasValue ? new int?((int) qtyFixedFsSrvOrd1.GetValueOrDefault()) : new int?();
                                  int num16 = 0;
                                  if (!(nullable.GetValueOrDefault() > num16 & nullable.HasValue))
                                  {
                                    short? qtyPoFixedFsSrvOrd1 = t.InclQtyPOFixedFSSrvOrd;
                                    nullable = qtyPoFixedFsSrvOrd1.HasValue ? new int?((int) qtyPoFixedFsSrvOrd1.GetValueOrDefault()) : new int?();
                                    int num17 = 0;
                                    if (!(nullable.GetValueOrDefault() > num17 & nullable.HasValue))
                                    {
                                      short? fsSrvOrdPrepared1 = t.InclQtyPOFixedFSSrvOrdPrepared;
                                      nullable = fsSrvOrdPrepared1.HasValue ? new int?((int) fsSrvOrdPrepared1.GetValueOrDefault()) : new int?();
                                      int num18 = 0;
                                      if (!(nullable.GetValueOrDefault() > num18 & nullable.HasValue))
                                      {
                                        short? fsSrvOrdReceipts1 = t.InclQtyPOFixedFSSrvOrdReceipts;
                                        nullable = fsSrvOrdReceipts1.HasValue ? new int?((int) fsSrvOrdReceipts1.GetValueOrDefault()) : new int?();
                                        int num19 = 0;
                                        if (!(nullable.GetValueOrDefault() > num19 & nullable.HasValue))
                                        {
                                          short? inclQtySoFixed1 = t.InclQtySOFixed;
                                          nullable = inclQtySoFixed1.HasValue ? new int?((int) inclQtySoFixed1.GetValueOrDefault()) : new int?();
                                          int num20 = 0;
                                          if (!(nullable.GetValueOrDefault() > num20 & nullable.HasValue))
                                          {
                                            short? qtyPoFixedOrders1 = t.InclQtyPOFixedOrders;
                                            nullable = qtyPoFixedOrders1.HasValue ? new int?((int) qtyPoFixedOrders1.GetValueOrDefault()) : new int?();
                                            int num21 = 0;
                                            if (!(nullable.GetValueOrDefault() > num21 & nullable.HasValue))
                                            {
                                              short? qtyPoFixedPrepared1 = t.InclQtyPOFixedPrepared;
                                              nullable = qtyPoFixedPrepared1.HasValue ? new int?((int) qtyPoFixedPrepared1.GetValueOrDefault()) : new int?();
                                              int num22 = 0;
                                              if (!(nullable.GetValueOrDefault() > num22 & nullable.HasValue))
                                              {
                                                short? qtyPoFixedReceipts1 = t.InclQtyPOFixedReceipts;
                                                nullable = qtyPoFixedReceipts1.HasValue ? new int?((int) qtyPoFixedReceipts1.GetValueOrDefault()) : new int?();
                                                int num23 = 0;
                                                if (!(nullable.GetValueOrDefault() > num23 & nullable.HasValue))
                                                {
                                                  short? inclQtySoDropShip1 = t.InclQtySODropShip;
                                                  nullable = inclQtySoDropShip1.HasValue ? new int?((int) inclQtySoDropShip1.GetValueOrDefault()) : new int?();
                                                  int num24 = 0;
                                                  if (!(nullable.GetValueOrDefault() > num24 & nullable.HasValue))
                                                  {
                                                    short? poDropShipOrders1 = t.InclQtyPODropShipOrders;
                                                    nullable = poDropShipOrders1.HasValue ? new int?((int) poDropShipOrders1.GetValueOrDefault()) : new int?();
                                                    int num25 = 0;
                                                    if (!(nullable.GetValueOrDefault() > num25 & nullable.HasValue))
                                                    {
                                                      short? dropShipPrepared1 = t.InclQtyPODropShipPrepared;
                                                      nullable = dropShipPrepared1.HasValue ? new int?((int) dropShipPrepared1.GetValueOrDefault()) : new int?();
                                                      int num26 = 0;
                                                      if (!(nullable.GetValueOrDefault() > num26 & nullable.HasValue))
                                                      {
                                                        short? dropShipReceipts1 = t.InclQtyPODropShipReceipts;
                                                        nullable = dropShipReceipts1.HasValue ? new int?((int) dropShipReceipts1.GetValueOrDefault()) : new int?();
                                                        int num27 = 0;
                                                        if (!(nullable.GetValueOrDefault() > num27 & nullable.HasValue))
                                                        {
                                                          short? inclQtyInIssues2 = t.InclQtyINIssues;
                                                          nullable = inclQtyInIssues2.HasValue ? new int?((int) inclQtyInIssues2.GetValueOrDefault()) : new int?();
                                                          int num28 = 0;
                                                          if (!(nullable.GetValueOrDefault() < num28 & nullable.HasValue))
                                                          {
                                                            short? inclQtyInReceipts2 = t.InclQtyINReceipts;
                                                            nullable = inclQtyInReceipts2.HasValue ? new int?((int) inclQtyInReceipts2.GetValueOrDefault()) : new int?();
                                                            int num29 = 0;
                                                            if (!(nullable.GetValueOrDefault() < num29 & nullable.HasValue))
                                                            {
                                                              short? inclQtyInTransit2 = t.InclQtyInTransit;
                                                              nullable = inclQtyInTransit2.HasValue ? new int?((int) inclQtyInTransit2.GetValueOrDefault()) : new int?();
                                                              int num30 = 0;
                                                              if (!(nullable.GetValueOrDefault() < num30 & nullable.HasValue))
                                                              {
                                                                short? qtyInTransitToSo2 = t.InclQtyInTransitToSO;
                                                                nullable = qtyInTransitToSo2.HasValue ? new int?((int) qtyInTransitToSo2.GetValueOrDefault()) : new int?();
                                                                int num31 = 0;
                                                                if (!(nullable.GetValueOrDefault() < num31 & nullable.HasValue))
                                                                {
                                                                  short? inclQtyPoReceipts2 = t.InclQtyPOReceipts;
                                                                  nullable = inclQtyPoReceipts2.HasValue ? new int?((int) inclQtyPoReceipts2.GetValueOrDefault()) : new int?();
                                                                  int num32 = 0;
                                                                  if (!(nullable.GetValueOrDefault() < num32 & nullable.HasValue))
                                                                  {
                                                                    short? inclQtyPoPrepared2 = t.InclQtyPOPrepared;
                                                                    nullable = inclQtyPoPrepared2.HasValue ? new int?((int) inclQtyPoPrepared2.GetValueOrDefault()) : new int?();
                                                                    int num33 = 0;
                                                                    if (!(nullable.GetValueOrDefault() < num33 & nullable.HasValue))
                                                                    {
                                                                      short? inclQtyPoOrders2 = t.InclQtyPOOrders;
                                                                      nullable = inclQtyPoOrders2.HasValue ? new int?((int) inclQtyPoOrders2.GetValueOrDefault()) : new int?();
                                                                      int num34 = 0;
                                                                      if (!(nullable.GetValueOrDefault() < num34 & nullable.HasValue))
                                                                      {
                                                                        short? qtyFsSrvOrdBooked = t.InclQtyFSSrvOrdBooked;
                                                                        nullable = qtyFsSrvOrdBooked.HasValue ? new int?((int) qtyFsSrvOrdBooked.GetValueOrDefault()) : new int?();
                                                                        int num35 = 0;
                                                                        if (!(nullable.GetValueOrDefault() < num35 & nullable.HasValue))
                                                                        {
                                                                          short? fsSrvOrdAllocated = t.InclQtyFSSrvOrdAllocated;
                                                                          nullable = fsSrvOrdAllocated.HasValue ? new int?((int) fsSrvOrdAllocated.GetValueOrDefault()) : new int?();
                                                                          int num36 = 0;
                                                                          if (!(nullable.GetValueOrDefault() < num36 & nullable.HasValue))
                                                                          {
                                                                            short? fsSrvOrdPrepared2 = t.InclQtyFSSrvOrdPrepared;
                                                                            nullable = fsSrvOrdPrepared2.HasValue ? new int?((int) fsSrvOrdPrepared2.GetValueOrDefault()) : new int?();
                                                                            int num37 = 0;
                                                                            if (!(nullable.GetValueOrDefault() < num37 & nullable.HasValue))
                                                                            {
                                                                              short? qtySoBackOrdered2 = t.InclQtySOBackOrdered;
                                                                              nullable = qtySoBackOrdered2.HasValue ? new int?((int) qtySoBackOrdered2.GetValueOrDefault()) : new int?();
                                                                              int num38 = 0;
                                                                              if (!(nullable.GetValueOrDefault() < num38 & nullable.HasValue))
                                                                              {
                                                                                short? inclQtySoPrepared2 = t.InclQtySOPrepared;
                                                                                nullable = inclQtySoPrepared2.HasValue ? new int?((int) inclQtySoPrepared2.GetValueOrDefault()) : new int?();
                                                                                int num39 = 0;
                                                                                if (!(nullable.GetValueOrDefault() < num39 & nullable.HasValue))
                                                                                {
                                                                                  short? inclQtySoBooked2 = t.InclQtySOBooked;
                                                                                  nullable = inclQtySoBooked2.HasValue ? new int?((int) inclQtySoBooked2.GetValueOrDefault()) : new int?();
                                                                                  int num40 = 0;
                                                                                  if (!(nullable.GetValueOrDefault() < num40 & nullable.HasValue))
                                                                                  {
                                                                                    short? inclQtySoShipped2 = t.InclQtySOShipped;
                                                                                    nullable = inclQtySoShipped2.HasValue ? new int?((int) inclQtySoShipped2.GetValueOrDefault()) : new int?();
                                                                                    int num41 = 0;
                                                                                    if (!(nullable.GetValueOrDefault() < num41 & nullable.HasValue))
                                                                                    {
                                                                                      short? inclQtySoShipping2 = t.InclQtySOShipping;
                                                                                      nullable = inclQtySoShipping2.HasValue ? new int?((int) inclQtySoShipping2.GetValueOrDefault()) : new int?();
                                                                                      int num42 = 0;
                                                                                      if (!(nullable.GetValueOrDefault() < num42 & nullable.HasValue))
                                                                                      {
                                                                                        short? inAssemblySupply2 = t.InclQtyINAssemblySupply;
                                                                                        nullable = inAssemblySupply2.HasValue ? new int?((int) inAssemblySupply2.GetValueOrDefault()) : new int?();
                                                                                        int num43 = 0;
                                                                                        if (!(nullable.GetValueOrDefault() < num43 & nullable.HasValue))
                                                                                        {
                                                                                          short? inAssemblyDemand2 = t.InclQtyINAssemblyDemand;
                                                                                          nullable = inAssemblyDemand2.HasValue ? new int?((int) inAssemblyDemand2.GetValueOrDefault()) : new int?();
                                                                                          int num44 = 0;
                                                                                          if (!(nullable.GetValueOrDefault() < num44 & nullable.HasValue))
                                                                                          {
                                                                                            short? transitToProduction = t.InclQtyInTransitToProduction;
                                                                                            nullable = transitToProduction.HasValue ? new int?((int) transitToProduction.GetValueOrDefault()) : new int?();
                                                                                            int num45 = 0;
                                                                                            if (!(nullable.GetValueOrDefault() < num45 & nullable.HasValue))
                                                                                            {
                                                                                              short? productionSupplyPrepared = t.InclQtyProductionSupplyPrepared;
                                                                                              nullable = productionSupplyPrepared.HasValue ? new int?((int) productionSupplyPrepared.GetValueOrDefault()) : new int?();
                                                                                              int num46 = 0;
                                                                                              if (!(nullable.GetValueOrDefault() < num46 & nullable.HasValue))
                                                                                              {
                                                                                                short? productionSupply = t.InclQtyProductionSupply;
                                                                                                nullable = productionSupply.HasValue ? new int?((int) productionSupply.GetValueOrDefault()) : new int?();
                                                                                                int num47 = 0;
                                                                                                if (!(nullable.GetValueOrDefault() < num47 & nullable.HasValue))
                                                                                                {
                                                                                                  short? productionPrepared = t.InclQtyPOFixedProductionPrepared;
                                                                                                  nullable = productionPrepared.HasValue ? new int?((int) productionPrepared.GetValueOrDefault()) : new int?();
                                                                                                  int num48 = 0;
                                                                                                  if (!(nullable.GetValueOrDefault() < num48 & nullable.HasValue))
                                                                                                  {
                                                                                                    short? productionOrders = t.InclQtyPOFixedProductionOrders;
                                                                                                    nullable = productionOrders.HasValue ? new int?((int) productionOrders.GetValueOrDefault()) : new int?();
                                                                                                    int num49 = 0;
                                                                                                    if (!(nullable.GetValueOrDefault() < num49 & nullable.HasValue))
                                                                                                    {
                                                                                                      short? productionDemandPrepared = t.InclQtyProductionDemandPrepared;
                                                                                                      nullable = productionDemandPrepared.HasValue ? new int?((int) productionDemandPrepared.GetValueOrDefault()) : new int?();
                                                                                                      int num50 = 0;
                                                                                                      if (!(nullable.GetValueOrDefault() < num50 & nullable.HasValue))
                                                                                                      {
                                                                                                        short? productionDemand = t.InclQtyProductionDemand;
                                                                                                        nullable = productionDemand.HasValue ? new int?((int) productionDemand.GetValueOrDefault()) : new int?();
                                                                                                        int num51 = 0;
                                                                                                        if (!(nullable.GetValueOrDefault() < num51 & nullable.HasValue))
                                                                                                        {
                                                                                                          short? productionAllocated = t.InclQtyProductionAllocated;
                                                                                                          nullable = productionAllocated.HasValue ? new int?((int) productionAllocated.GetValueOrDefault()) : new int?();
                                                                                                          int num52 = 0;
                                                                                                          if (!(nullable.GetValueOrDefault() < num52 & nullable.HasValue))
                                                                                                          {
                                                                                                            short? soFixedProduction = t.InclQtySOFixedProduction;
                                                                                                            nullable = soFixedProduction.HasValue ? new int?((int) soFixedProduction.GetValueOrDefault()) : new int?();
                                                                                                            int num53 = 0;
                                                                                                            if (!(nullable.GetValueOrDefault() < num53 & nullable.HasValue))
                                                                                                            {
                                                                                                              short? prodFixedPurchase = t.InclQtyProdFixedPurchase;
                                                                                                              nullable = prodFixedPurchase.HasValue ? new int?((int) prodFixedPurchase.GetValueOrDefault()) : new int?();
                                                                                                              int num54 = 0;
                                                                                                              if (!(nullable.GetValueOrDefault() < num54 & nullable.HasValue))
                                                                                                              {
                                                                                                                short? prodFixedProduction = t.InclQtyProdFixedProduction;
                                                                                                                nullable = prodFixedProduction.HasValue ? new int?((int) prodFixedProduction.GetValueOrDefault()) : new int?();
                                                                                                                int num55 = 0;
                                                                                                                if (!(nullable.GetValueOrDefault() < num55 & nullable.HasValue))
                                                                                                                {
                                                                                                                  short? prodOrdersPrepared = t.InclQtyProdFixedProdOrdersPrepared;
                                                                                                                  nullable = prodOrdersPrepared.HasValue ? new int?((int) prodOrdersPrepared.GetValueOrDefault()) : new int?();
                                                                                                                  int num56 = 0;
                                                                                                                  if (!(nullable.GetValueOrDefault() < num56 & nullable.HasValue))
                                                                                                                  {
                                                                                                                    short? prodFixedProdOrders = t.InclQtyProdFixedProdOrders;
                                                                                                                    nullable = prodFixedProdOrders.HasValue ? new int?((int) prodFixedProdOrders.GetValueOrDefault()) : new int?();
                                                                                                                    int num57 = 0;
                                                                                                                    if (!(nullable.GetValueOrDefault() < num57 & nullable.HasValue))
                                                                                                                    {
                                                                                                                      short? salesOrdersPrepared = t.InclQtyProdFixedSalesOrdersPrepared;
                                                                                                                      nullable = salesOrdersPrepared.HasValue ? new int?((int) salesOrdersPrepared.GetValueOrDefault()) : new int?();
                                                                                                                      int num58 = 0;
                                                                                                                      if (!(nullable.GetValueOrDefault() < num58 & nullable.HasValue))
                                                                                                                      {
                                                                                                                        short? fixedSalesOrders = t.InclQtyProdFixedSalesOrders;
                                                                                                                        nullable = fixedSalesOrders.HasValue ? new int?((int) fixedSalesOrders.GetValueOrDefault()) : new int?();
                                                                                                                        int num59 = 0;
                                                                                                                        if (!(nullable.GetValueOrDefault() < num59 & nullable.HasValue))
                                                                                                                        {
                                                                                                                          short? inclQtyInReplaned2 = t.InclQtyINReplaned;
                                                                                                                          nullable = inclQtyInReplaned2.HasValue ? new int?((int) inclQtyInReplaned2.GetValueOrDefault()) : new int?();
                                                                                                                          int num60 = 0;
                                                                                                                          if (!(nullable.GetValueOrDefault() < num60 & nullable.HasValue))
                                                                                                                          {
                                                                                                                            short? qtyFixedFsSrvOrd2 = t.InclQtyFixedFSSrvOrd;
                                                                                                                            nullable = qtyFixedFsSrvOrd2.HasValue ? new int?((int) qtyFixedFsSrvOrd2.GetValueOrDefault()) : new int?();
                                                                                                                            int num61 = 0;
                                                                                                                            if (!(nullable.GetValueOrDefault() < num61 & nullable.HasValue))
                                                                                                                            {
                                                                                                                              short? qtyPoFixedFsSrvOrd2 = t.InclQtyPOFixedFSSrvOrd;
                                                                                                                              nullable = qtyPoFixedFsSrvOrd2.HasValue ? new int?((int) qtyPoFixedFsSrvOrd2.GetValueOrDefault()) : new int?();
                                                                                                                              int num62 = 0;
                                                                                                                              if (!(nullable.GetValueOrDefault() < num62 & nullable.HasValue))
                                                                                                                              {
                                                                                                                                short? fsSrvOrdPrepared3 = t.InclQtyPOFixedFSSrvOrdPrepared;
                                                                                                                                nullable = fsSrvOrdPrepared3.HasValue ? new int?((int) fsSrvOrdPrepared3.GetValueOrDefault()) : new int?();
                                                                                                                                int num63 = 0;
                                                                                                                                if (!(nullable.GetValueOrDefault() < num63 & nullable.HasValue))
                                                                                                                                {
                                                                                                                                  short? fsSrvOrdReceipts2 = t.InclQtyPOFixedFSSrvOrdReceipts;
                                                                                                                                  nullable = fsSrvOrdReceipts2.HasValue ? new int?((int) fsSrvOrdReceipts2.GetValueOrDefault()) : new int?();
                                                                                                                                  int num64 = 0;
                                                                                                                                  if (!(nullable.GetValueOrDefault() < num64 & nullable.HasValue))
                                                                                                                                  {
                                                                                                                                    short? inclQtySoFixed2 = t.InclQtySOFixed;
                                                                                                                                    nullable = inclQtySoFixed2.HasValue ? new int?((int) inclQtySoFixed2.GetValueOrDefault()) : new int?();
                                                                                                                                    int num65 = 0;
                                                                                                                                    if (!(nullable.GetValueOrDefault() < num65 & nullable.HasValue))
                                                                                                                                    {
                                                                                                                                      short? qtyPoFixedOrders2 = t.InclQtyPOFixedOrders;
                                                                                                                                      nullable = qtyPoFixedOrders2.HasValue ? new int?((int) qtyPoFixedOrders2.GetValueOrDefault()) : new int?();
                                                                                                                                      int num66 = 0;
                                                                                                                                      if (!(nullable.GetValueOrDefault() < num66 & nullable.HasValue))
                                                                                                                                      {
                                                                                                                                        short? qtyPoFixedPrepared2 = t.InclQtyPOFixedPrepared;
                                                                                                                                        nullable = qtyPoFixedPrepared2.HasValue ? new int?((int) qtyPoFixedPrepared2.GetValueOrDefault()) : new int?();
                                                                                                                                        int num67 = 0;
                                                                                                                                        if (!(nullable.GetValueOrDefault() < num67 & nullable.HasValue))
                                                                                                                                        {
                                                                                                                                          short? qtyPoFixedReceipts2 = t.InclQtyPOFixedReceipts;
                                                                                                                                          nullable = qtyPoFixedReceipts2.HasValue ? new int?((int) qtyPoFixedReceipts2.GetValueOrDefault()) : new int?();
                                                                                                                                          int num68 = 0;
                                                                                                                                          if (!(nullable.GetValueOrDefault() < num68 & nullable.HasValue))
                                                                                                                                          {
                                                                                                                                            short? inclQtySoDropShip2 = t.InclQtySODropShip;
                                                                                                                                            nullable = inclQtySoDropShip2.HasValue ? new int?((int) inclQtySoDropShip2.GetValueOrDefault()) : new int?();
                                                                                                                                            int num69 = 0;
                                                                                                                                            if (!(nullable.GetValueOrDefault() < num69 & nullable.HasValue))
                                                                                                                                            {
                                                                                                                                              short? poDropShipOrders2 = t.InclQtyPODropShipOrders;
                                                                                                                                              nullable = poDropShipOrders2.HasValue ? new int?((int) poDropShipOrders2.GetValueOrDefault()) : new int?();
                                                                                                                                              int num70 = 0;
                                                                                                                                              if (!(nullable.GetValueOrDefault() < num70 & nullable.HasValue))
                                                                                                                                              {
                                                                                                                                                short? dropShipPrepared2 = t.InclQtyPODropShipPrepared;
                                                                                                                                                nullable = dropShipPrepared2.HasValue ? new int?((int) dropShipPrepared2.GetValueOrDefault()) : new int?();
                                                                                                                                                int num71 = 0;
                                                                                                                                                if (!(nullable.GetValueOrDefault() < num71 & nullable.HasValue))
                                                                                                                                                {
                                                                                                                                                  short? dropShipReceipts2 = t.InclQtyPODropShipReceipts;
                                                                                                                                                  nullable = dropShipReceipts2.HasValue ? new int?((int) dropShipReceipts2.GetValueOrDefault()) : new int?();
                                                                                                                                                  int num72 = 0;
                                                                                                                                                  if (!(nullable.GetValueOrDefault() < num72 & nullable.HasValue))
                                                                                                                                                    return 0;
                                                                                                                                                }
                                                                                                                                              }
                                                                                                                                            }
                                                                                                                                          }
                                                                                                                                        }
                                                                                                                                      }
                                                                                                                                    }
                                                                                                                                  }
                                                                                                                                }
                                                                                                                              }
                                                                                                                            }
                                                                                                                          }
                                                                                                                        }
                                                                                                                      }
                                                                                                                    }
                                                                                                                  }
                                                                                                                }
                                                                                                              }
                                                                                                            }
                                                                                                          }
                                                                                                        }
                                                                                                      }
                                                                                                    }
                                                                                                  }
                                                                                                }
                                                                                              }
                                                                                            }
                                                                                          }
                                                                                        }
                                                                                      }
                                                                                    }
                                                                                  }
                                                                                }
                                                                              }
                                                                            }
                                                                          }
                                                                        }
                                                                      }
                                                                    }
                                                                  }
                                                                }
                                                              }
                                                            }
                                                          }
                                                          return -1;
                                                        }
                                                      }
                                                    }
                                                  }
                                                }
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return 1;
  }

  public static bool operator ==(INPlanType t1, INPlanType t2)
  {
    if (object.Equals((object) t1, (object) null) || object.Equals((object) t2, (object) null))
      return object.Equals((object) t1, (object) null) && object.Equals((object) t2, (object) null);
    short? nullable1 = t1.InclQtyINIssues;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = t2.InclQtyINIssues;
    int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
    {
      nullable1 = t1.InclQtyINReceipts;
      nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable1 = t2.InclQtyINReceipts;
      int? nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
      {
        nullable1 = t1.InclQtyInTransit;
        nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        nullable1 = t2.InclQtyInTransit;
        nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
        {
          nullable1 = t1.InclQtyInTransitToSO;
          nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          nullable1 = t2.InclQtyInTransitToSO;
          nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
          {
            nullable1 = t1.InclQtyPOReceipts;
            nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            nullable1 = t2.InclQtyPOReceipts;
            nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
            {
              nullable1 = t1.InclQtyPOPrepared;
              nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
              nullable1 = t2.InclQtyPOPrepared;
              nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
              if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
              {
                nullable1 = t1.InclQtyPOOrders;
                nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                nullable1 = t2.InclQtyPOOrders;
                nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                {
                  nullable1 = t1.InclQtyFSSrvOrdBooked;
                  nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                  nullable1 = t2.InclQtyFSSrvOrdBooked;
                  nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                  if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                  {
                    nullable1 = t1.InclQtyFSSrvOrdAllocated;
                    nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                    nullable1 = t2.InclQtyFSSrvOrdAllocated;
                    nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                    if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                    {
                      nullable1 = t1.InclQtyFSSrvOrdPrepared;
                      nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                      nullable1 = t2.InclQtyFSSrvOrdPrepared;
                      nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                      {
                        nullable1 = t1.InclQtySOBackOrdered;
                        nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                        nullable1 = t2.InclQtySOBackOrdered;
                        nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                        {
                          nullable1 = t1.InclQtySOPrepared;
                          nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                          nullable1 = t2.InclQtySOPrepared;
                          nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                          {
                            nullable1 = t1.InclQtySOBooked;
                            nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                            nullable1 = t2.InclQtySOBooked;
                            nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                            if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                            {
                              nullable1 = t1.InclQtySOShipped;
                              nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                              nullable1 = t2.InclQtySOShipped;
                              nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                              if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                              {
                                nullable1 = t1.InclQtySOShipping;
                                nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                nullable1 = t2.InclQtySOShipping;
                                nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                {
                                  nullable1 = t1.InclQtyINAssemblySupply;
                                  nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                  nullable1 = t2.InclQtyINAssemblySupply;
                                  nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                  if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                  {
                                    nullable1 = t1.InclQtyINAssemblyDemand;
                                    nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                    nullable1 = t2.InclQtyINAssemblyDemand;
                                    nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                    if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                    {
                                      nullable1 = t1.InclQtyInTransitToProduction;
                                      nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                      nullable1 = t2.InclQtyInTransitToProduction;
                                      nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                      {
                                        nullable1 = t1.InclQtyProductionSupplyPrepared;
                                        nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                        nullable1 = t2.InclQtyProductionSupplyPrepared;
                                        nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                        {
                                          nullable1 = t1.InclQtyProductionSupply;
                                          nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                          nullable1 = t2.InclQtyProductionSupply;
                                          nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                          {
                                            nullable1 = t1.InclQtyPOFixedProductionPrepared;
                                            nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                            nullable1 = t2.InclQtyPOFixedProductionPrepared;
                                            nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                            if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                            {
                                              nullable1 = t1.InclQtyPOFixedProductionOrders;
                                              nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                              nullable1 = t2.InclQtyPOFixedProductionOrders;
                                              nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                              if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                              {
                                                nullable1 = t1.InclQtyProductionDemandPrepared;
                                                nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                nullable1 = t2.InclQtyProductionDemandPrepared;
                                                nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                {
                                                  nullable1 = t1.InclQtyProductionDemand;
                                                  nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                  nullable1 = t2.InclQtyProductionDemand;
                                                  nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                  if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                  {
                                                    nullable1 = t1.InclQtyProductionAllocated;
                                                    nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                    nullable1 = t2.InclQtyProductionAllocated;
                                                    nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                    if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                    {
                                                      nullable1 = t1.InclQtySOFixedProduction;
                                                      nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                      nullable1 = t2.InclQtySOFixedProduction;
                                                      nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                      {
                                                        nullable1 = t1.InclQtyFixedFSSrvOrd;
                                                        nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                        nullable1 = t2.InclQtyFixedFSSrvOrd;
                                                        nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                        {
                                                          nullable1 = t1.InclQtyPOFixedFSSrvOrd;
                                                          nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                          nullable1 = t2.InclQtyPOFixedFSSrvOrd;
                                                          nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                          {
                                                            nullable1 = t1.InclQtyPOFixedFSSrvOrdPrepared;
                                                            nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                            nullable1 = t2.InclQtyPOFixedFSSrvOrdPrepared;
                                                            nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                            if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                            {
                                                              nullable1 = t1.InclQtyPOFixedFSSrvOrdReceipts;
                                                              nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                              nullable1 = t2.InclQtyPOFixedFSSrvOrdReceipts;
                                                              nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                              if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                              {
                                                                nullable1 = t1.InclQtyProdFixedPurchase;
                                                                nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                nullable1 = t2.InclQtyProdFixedPurchase;
                                                                nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                                {
                                                                  nullable1 = t1.InclQtyProdFixedProduction;
                                                                  nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                  nullable1 = t2.InclQtyProdFixedProduction;
                                                                  nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                  if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                                  {
                                                                    nullable1 = t1.InclQtyProdFixedProdOrdersPrepared;
                                                                    nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                    nullable1 = t2.InclQtyProdFixedProdOrdersPrepared;
                                                                    nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                    if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                                    {
                                                                      nullable1 = t1.InclQtyProdFixedProdOrders;
                                                                      nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                      nullable1 = t2.InclQtyProdFixedProdOrders;
                                                                      nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                                      {
                                                                        nullable1 = t1.InclQtyProdFixedSalesOrdersPrepared;
                                                                        nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                        nullable1 = t2.InclQtyProdFixedSalesOrdersPrepared;
                                                                        nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                                        {
                                                                          nullable1 = t1.InclQtyProdFixedSalesOrders;
                                                                          nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                          nullable1 = t2.InclQtyProdFixedSalesOrders;
                                                                          nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                                          {
                                                                            nullable1 = t1.InclQtySOFixed;
                                                                            nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                            nullable1 = t2.InclQtySOFixed;
                                                                            nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                            if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                                            {
                                                                              nullable1 = t1.InclQtyPOFixedOrders;
                                                                              nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                              nullable1 = t2.InclQtyPOFixedOrders;
                                                                              nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                              if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                                              {
                                                                                nullable1 = t1.InclQtyPOFixedPrepared;
                                                                                nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                nullable1 = t2.InclQtyPOFixedPrepared;
                                                                                nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                                                {
                                                                                  nullable1 = t1.InclQtyPOFixedReceipts;
                                                                                  nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                  nullable1 = t2.InclQtyPOFixedReceipts;
                                                                                  nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                  if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                                                  {
                                                                                    nullable1 = t1.InclQtySODropShip;
                                                                                    nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                    nullable1 = t2.InclQtySODropShip;
                                                                                    nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                    if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                                                    {
                                                                                      nullable1 = t1.InclQtyPODropShipOrders;
                                                                                      nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                      nullable1 = t2.InclQtyPODropShipOrders;
                                                                                      nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                      if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                                                                                      {
                                                                                        nullable1 = t1.InclQtyPODropShipPrepared;
                                                                                        nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                        nullable1 = t2.InclQtyPODropShipPrepared;
                                                                                        nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                        if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                                                                                        {
                                                                                          nullable1 = t1.InclQtyPODropShipReceipts;
                                                                                          nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                          nullable1 = t2.InclQtyPODropShipReceipts;
                                                                                          nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
                                                                                          return nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue;
                                                                                        }
                                                                                      }
                                                                                    }
                                                                                  }
                                                                                }
                                                                              }
                                                                            }
                                                                          }
                                                                        }
                                                                      }
                                                                    }
                                                                  }
                                                                }
                                                              }
                                                            }
                                                          }
                                                        }
                                                      }
                                                    }
                                                  }
                                                }
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    return false;
  }

  public static bool operator !=(INPlanType t1, INPlanType t2) => !(t1 == t2);

  public virtual bool Equals(object obj) => this == (INPlanType) obj;

  public virtual int GetHashCode() => __nonvirtual (((object) this).GetHashCode());

  public class PK : PrimaryKeyOf<INPlanType>.By<INPlanType.planType>
  {
    public static INPlanType Find(PXGraph graph, string planType, PKFindOptions options = 0)
    {
      return (INPlanType) PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.FindBy(graph, (object) planType, options);
    }
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPlanType.planType>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPlanType.descr>
  {
  }

  public abstract class localizedDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPlanType.localizedDescr>
  {
  }

  public abstract class isFixed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPlanType.isFixed>
  {
  }

  public abstract class isSupply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPlanType.isSupply>
  {
  }

  public abstract class isDemand : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPlanType.isDemand>
  {
  }

  public abstract class isForDate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPlanType.isForDate>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INPlanType.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtySOShipping>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyInTransit>
  {
  }

  public abstract class inclQtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyInTransitToSO>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOReceipts>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOPrepared>
  {
  }

  public abstract class inclQtyPOOrders : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INPlanType.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyINIssues : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INPlanType.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyINReceipts>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyINAssemblySupply>
  {
  }

  public abstract class inclQtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyInTransitToProduction>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedProductionPrepared>
  {
  }

  public abstract class inclQtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedProductionOrders>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProductionAllocated>
  {
  }

  public abstract class inclQtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtySOFixedProduction>
  {
  }

  public abstract class inclQtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProdFixedPurchase>
  {
  }

  public abstract class inclQtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProdFixedProduction>
  {
  }

  public abstract class inclQtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class inclQtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProdFixedProdOrders>
  {
  }

  public abstract class inclQtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class inclQtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyProdFixedSalesOrders>
  {
  }

  public abstract class inclQtyINReplaned : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyINReplaned>
  {
  }

  public abstract class inclQtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyFixedFSSrvOrd>
  {
  }

  public abstract class inclQtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedFSSrvOrd>
  {
  }

  public abstract class inclQtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class inclQtySOFixed : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INPlanType.inclQtySOFixed>
  {
  }

  public abstract class inclQtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedOrders>
  {
  }

  public abstract class inclQtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedPrepared>
  {
  }

  public abstract class inclQtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPOFixedReceipts>
  {
  }

  public abstract class inclQtySODropShip : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtySODropShip>
  {
  }

  public abstract class inclQtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPODropShipOrders>
  {
  }

  public abstract class inclQtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPODropShipPrepared>
  {
  }

  public abstract class inclQtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INPlanType.inclQtyPODropShipReceipts>
  {
  }

  public abstract class deleteOnEvent : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPlanType.deleteOnEvent>
  {
  }

  public abstract class replanOnEvent : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPlanType.replanOnEvent>
  {
  }

  /// <exclude />
  [PXInternalUseOnly]
  public abstract class deleteOperation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPlanType.deleteOperation>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPlanType.Tstamp>
  {
  }

  public class LocalizedFieldAttribute : PXEventSubscriberAttribute, IPXRowSelectingSubscriber
  {
    protected Type _origField;

    public LocalizedFieldAttribute(Type origField) => this._origField = origField;

    public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      string str = (string) sender.GetValue(e.Row, this._origField.Name);
      sender.SetValue(e.Row, this._FieldName, (object) PXLocalizer.Localize(str));
    }
  }
}
