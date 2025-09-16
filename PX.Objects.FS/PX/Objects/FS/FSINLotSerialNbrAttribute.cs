// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSINLotSerialNbrAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.FS;

public class FSINLotSerialNbrAttribute : PXCustomSelectorAttribute
{
  private readonly 
  #nullable disable
  Type SiteID;
  private readonly Type InventoryType;
  private readonly Type SubItemType;
  private readonly Type LocationType;
  private readonly Type CostCenterType;
  private readonly Type SrvOrdLineID;
  private PXView ApptINLotSerialStatusView;
  private List<string> LotSerialStatusFields;

  public FSINLotSerialNbrAttribute(
    Type SiteID,
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type CostCenterType,
    Type SrvOrdLineID)
    : base(typeof (Search<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lotSerialNbr>), new Type[10]
    {
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lotSerialNbr),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.siteID),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.locationID),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.costCenterID),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyOnHand),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyAvail),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.expireDate),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.srvOrdAllocation),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyFSSrvOrdAllocated),
      typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyFSSrvOrdBooked)
    })
  {
    this.SiteID = SiteID;
    this.InventoryType = InventoryType;
    this.SubItemType = SubItemType;
    this.LocationType = LocationType;
    this.CostCenterType = CostCenterType;
    this.SrvOrdLineID = SrvOrdLineID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    // ISSUE: method pointer
    this.ApptINLotSerialStatusView = new PXView(sender.Graph, false, (BqlCommand) new Select<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>(), (Delegate) new PXSelectDelegate((object) this, __methodptr(GetApptINLotSerialStatus)));
    sender.Graph.Views.Add(this.Prefixed("ApptINLotSerialStatus".ToLower()), this.ApptINLotSerialStatusView);
    sender.Graph.Caches[typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus)].DisableReadItem = true;
    // ISSUE: method pointer
    sender.Graph.RowPersisting.AddHandler<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>(new PXRowPersisting((object) this, __methodptr(ApptINLotSerialStatus_RowPersisting)));
  }

  private IEnumerable GetApptINLotSerialStatus()
  {
    return (IEnumerable) new List<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>();
  }

  private void ApptINLotSerialStatus_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.Graph is AppointmentEntry)
    {
      AppointmentEntry graph = (AppointmentEntry) sender.Graph;
      if ((graph != null ? (graph.SkipLotSerialFieldVerifying ? 1 : 0) : 0) != 0)
        return;
    }
    base.FieldVerifying(sender, e);
  }

  protected virtual IEnumerable GetRecords()
  {
    apptLine = (FSAppointmentDet) null;
    foreach (object current in PXView.Currents)
    {
      if (current != null && current.GetType() == typeof (FSAppointmentDet))
      {
        apptLine = current as FSAppointmentDet;
        break;
      }
    }
    if (apptLine == null && !(this._Graph.Caches[typeof (FSAppointmentDet)].Current is FSAppointmentDet apptLine))
      return (IEnumerable) null;
    List<FSINLotSerialNbrAttribute.ApptINLotSerialStatus> lotSerialList = new List<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>();
    int num1 = 0;
    int? soDetId = apptLine.SODetID;
    if (soDetId.HasValue)
    {
      soDetId = apptLine.SODetID;
      int num2 = 0;
      if (soDetId.GetValueOrDefault() > num2 & soDetId.HasValue)
      {
        this.AddLotSerialsFromSrvOrdSplit(apptLine, lotSerialList);
        foreach (FSINLotSerialNbrAttribute.ApptINLotSerialStatus inLotSerialStatus in lotSerialList)
        {
          Decimal? fsSrvOrdAllocated = inLotSerialStatus.QtyFSSrvOrdAllocated;
          Decimal? qtyFsSrvOrdBooked = inLotSerialStatus.QtyFSSrvOrdBooked;
          Decimal? nullable = fsSrvOrdAllocated.HasValue & qtyFsSrvOrdBooked.HasValue ? new Decimal?(fsSrvOrdAllocated.GetValueOrDefault() - qtyFsSrvOrdBooked.GetValueOrDefault()) : new Decimal?();
          Decimal num3 = 0M;
          if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
            ++num1;
        }
      }
    }
    if (num1 == 0)
      this.AddLotSerialsFromIN(apptLine, lotSerialList);
    return (IEnumerable) lotSerialList;
  }

  public virtual void AddLotSerialsFromIN(
    FSAppointmentDet apptLine,
    List<FSINLotSerialNbrAttribute.ApptINLotSerialStatus> lotSerialList)
  {
    if (apptLine == null)
      return;
    List<string> stringList = new List<string>();
    foreach (FSINLotSerialNbrAttribute.ApptINLotSerialStatus lotSerial in lotSerialList)
      stringList.Add(lotSerial.LotSerialNbr);
    foreach (PXResult<INLotSerialStatusByCostCenter, INSiteLotSerial> pxResult in PXSelectBase<INLotSerialStatusByCostCenter, PXSelectJoin<INLotSerialStatusByCostCenter, InnerJoin<INSiteLotSerial, On<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<INSiteLotSerial.siteID, Equal<INLotSerialStatusByCostCenter.siteID>, And<INSiteLotSerial.lotSerialNbr, Equal<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Required<INLotSerialStatusByCostCenter.inventoryID>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Required<INLotSerialStatusByCostCenter.subItemID>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<Required<INLotSerialStatusByCostCenter.costCenterID>>, And<INLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>, And<Where<INLotSerialStatusByCostCenter.locationID, Equal<Required<INLotSerialStatusByCostCenter.locationID>>, Or<Required<INLotSerialStatusByCostCenter.locationID>, IsNull>>>>>>>>.Config>.Select(this._Graph, new object[5]
    {
      (object) apptLine.InventoryID,
      (object) apptLine.SubItemID,
      (object) apptLine.CostCenterID,
      (object) apptLine.SiteLocationID,
      (object) apptLine.SiteLocationID
    }))
    {
      INLotSerialStatusByCostCenter inLotSerialStatus1 = PXResult<INLotSerialStatusByCostCenter, INSiteLotSerial>.op_Implicit(pxResult);
      INSiteLotSerial inSiteLotSerial = PXResult<INLotSerialStatusByCostCenter, INSiteLotSerial>.op_Implicit(pxResult);
      if (!stringList.Contains(inLotSerialStatus1.LotSerialNbr))
      {
        FSINLotSerialNbrAttribute.ApptINLotSerialStatus inLotSerialStatus2 = FSINLotSerialNbrAttribute.ApptINLotSerialStatus.GetApptINLotSerialStatus(this._Graph.Caches[typeof (INLotSerialStatusByCostCenter)], inLotSerialStatus1, new int?(-1), this.ApptINLotSerialStatusView.Cache, ref this.LotSerialStatusFields);
        inLotSerialStatus2.SrvOrdAllocation = new bool?(false);
        inLotSerialStatus2.QtyFSSrvOrdAllocated = new Decimal?();
        inLotSerialStatus2.QtyFSSrvOrdBooked = new Decimal?();
        inLotSerialStatus2.QtyAvail = inSiteLotSerial.QtyAvail;
        FSINLotSerialNbrAttribute.ApptINLotSerialStatus inLotSerialStatus3 = (FSINLotSerialNbrAttribute.ApptINLotSerialStatus) this.ApptINLotSerialStatusView.Cache.Update((object) inLotSerialStatus2);
        this.ApptINLotSerialStatusView.Cache.SetStatus((object) inLotSerialStatus3, (PXEntryStatus) 5);
        lotSerialList.Add(inLotSerialStatus3);
      }
    }
    this.ApptINLotSerialStatusView.Cache.IsDirty = false;
  }

  public virtual void AddLotSerialsFromSrvOrdSplit(
    FSAppointmentDet apptLine,
    List<FSINLotSerialNbrAttribute.ApptINLotSerialStatus> lotSerialList)
  {
    if (apptLine == null || !apptLine.SODetID.HasValue)
      return;
    int? soDetId = apptLine.SODetID;
    int num = 0;
    if (soDetId.GetValueOrDefault() < num & soDetId.HasValue)
      return;
    FSSODet fssoDet = FSSODet.UK.Find(this._Graph, apptLine.SODetID);
    if (fssoDet == null)
      return;
    foreach (PXResult<INLotSerialStatusByCostCenter, INSiteLotSerial, FSSODetSplit> pxResult in PXSelectBase<INLotSerialStatusByCostCenter, PXSelectJoin<INLotSerialStatusByCostCenter, InnerJoin<INSiteLotSerial, On<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<INSiteLotSerial.siteID, Equal<INLotSerialStatusByCostCenter.siteID>, And<INSiteLotSerial.lotSerialNbr, Equal<INLotSerialStatusByCostCenter.lotSerialNbr>>>>, InnerJoin<FSSODetSplit, On<FSSODetSplit.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<FSSODetSplit.siteID, Equal<INLotSerialStatusByCostCenter.siteID>, And<FSSODetSplit.lotSerialNbr, Equal<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Required<INLotSerialStatusByCostCenter.inventoryID>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Required<INLotSerialStatusByCostCenter.subItemID>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<Required<INLotSerialStatusByCostCenter.costCenterID>>, And2<Where<INLotSerialStatusByCostCenter.locationID, Equal<Required<INLotSerialStatusByCostCenter.locationID>>, Or<Required<INLotSerialStatusByCostCenter.locationID>, IsNull>>, And<Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>>>>>>>>>, OrderBy<Asc<FSSODetSplit.splitLineNbr>>>.Config>.Select(this._Graph, new object[8]
    {
      (object) apptLine.InventoryID,
      (object) apptLine.SubItemID,
      (object) apptLine.CostCenterID,
      (object) apptLine.SiteLocationID,
      (object) apptLine.SiteLocationID,
      (object) fssoDet.SrvOrdType,
      (object) fssoDet.RefNbr,
      (object) fssoDet.LineNbr
    }))
    {
      INLotSerialStatusByCostCenter inLotSerialStatus1 = PXResult<INLotSerialStatusByCostCenter, INSiteLotSerial, FSSODetSplit>.op_Implicit(pxResult);
      PXResult<INLotSerialStatusByCostCenter, INSiteLotSerial, FSSODetSplit>.op_Implicit(pxResult);
      FSSODetSplit fssoDetSplit = PXResult<INLotSerialStatusByCostCenter, INSiteLotSerial, FSSODetSplit>.op_Implicit(pxResult);
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus inLotSerialStatus2 = FSINLotSerialNbrAttribute.ApptINLotSerialStatus.GetApptINLotSerialStatus(this._Graph.Caches[typeof (INLotSerialStatusByCostCenter)], inLotSerialStatus1, fssoDet.SODetID, this.ApptINLotSerialStatusView.Cache, ref this.LotSerialStatusFields);
      Decimal lotSerialAvailQty = 0M;
      Decimal lotSerialUsedQty = 0M;
      this.GetLotSerialAvailability(this._Graph, apptLine, inLotSerialStatus2.LotSerialNbr, false, out lotSerialAvailQty, out lotSerialUsedQty, out bool _);
      inLotSerialStatus2.SrvOrdAllocation = new bool?(true);
      inLotSerialStatus2.QtyFSSrvOrdAllocated = fssoDetSplit.Qty;
      inLotSerialStatus2.QtyFSSrvOrdBooked = new Decimal?(lotSerialUsedQty);
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus inLotSerialStatus3 = (FSINLotSerialNbrAttribute.ApptINLotSerialStatus) this.ApptINLotSerialStatusView.Cache.Update((object) inLotSerialStatus2);
      this.ApptINLotSerialStatusView.Cache.SetStatus((object) inLotSerialStatus3, (PXEntryStatus) 5);
      lotSerialList.Add(inLotSerialStatus3);
    }
    this.ApptINLotSerialStatusView.Cache.IsDirty = false;
  }

  protected string Prefixed(string name) => $"{((object) this).GetType().Name}_{name}";

  public virtual void GetLotSerialAvailability(
    PXGraph graphToQuery,
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    bool ignoreUseByApptLine,
    out Decimal lotSerialAvailQty,
    out Decimal lotSerialUsedQty,
    out bool foundServiceOrderAllocation)
  {
    FSApptLotSerialNbrAttribute.GetLotSerialAvailabilityInt(graphToQuery, apptLine, lotSerialNbr, ignoreUseByApptLine, out lotSerialAvailQty, out lotSerialUsedQty, out foundServiceOrderAllocation);
  }

  [PXCacheName("IN Lot/Serial Status")]
  [Serializable]
  public class ApptINLotSerialStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IStatus
  {
    protected int? _InventoryID;
    protected int? _SubItemID;
    protected int? _SiteID;
    protected int? _LocationID;
    protected string _LotSerialNbr;
    protected long? _CostID;
    protected Decimal? _QtyFSSrvOrdBooked;
    protected Decimal? _QtyFSSrvOrdAllocated;
    protected Decimal? _QtyFSSrvOrdPrepared;
    protected Decimal? _QtyOnHand;
    protected Decimal? _QtyAvail;
    protected Decimal? _QtyNotAvail;
    protected Decimal? _QtyExpired;
    protected Decimal? _QtyHardAvail;
    protected Decimal? _QtyActual;
    protected Decimal? _QtyInTransit;
    protected Decimal? _QtyInTransitToSO;
    protected Decimal? _QtyPOPrepared;
    protected Decimal? _QtyPOOrders;
    protected Decimal? _QtyPOReceipts;
    protected Decimal? _QtySOBackOrdered;
    protected Decimal? _QtySOPrepared;
    protected Decimal? _QtySOBooked;
    protected Decimal? _QtySOShipped;
    protected Decimal? _QtySOShipping;
    protected Decimal? _QtyINIssues;
    protected Decimal? _QtyINReceipts;
    protected Decimal? _QtyINAssemblyDemand;
    protected Decimal? _QtyINAssemblySupply;
    protected Decimal? _QtyInTransitToProduction;
    protected Decimal? _QtyProductionSupplyPrepared;
    protected Decimal? _QtyProductionSupply;
    protected Decimal? _QtyPOFixedProductionPrepared;
    protected Decimal? _QtyPOFixedProductionOrders;
    protected Decimal? _QtyProductionDemandPrepared;
    protected Decimal? _QtyProductionDemand;
    protected Decimal? _QtyProductionAllocated;
    protected Decimal? _QtySOFixedProduction;
    protected Decimal? _QtyFixedFSSrvOrd;
    protected Decimal? _QtyPOFixedFSSrvOrd;
    protected Decimal? _QtyPOFixedFSSrvOrdPrepared;
    protected Decimal? _QtyPOFixedFSSrvOrdReceipts;
    protected Decimal? _QtyProdFixedPurchase;
    protected Decimal? _QtyProdFixedProduction;
    protected Decimal? _QtyProdFixedProdOrdersPrepared;
    protected Decimal? _QtyProdFixedProdOrders;
    protected Decimal? _QtyProdFixedSalesOrdersPrepared;
    protected Decimal? _QtyProdFixedSalesOrders;
    protected Decimal? _QtySOFixed;
    protected Decimal? _QtyPOFixedOrders;
    protected Decimal? _QtyPOFixedPrepared;
    protected Decimal? _QtyPOFixedReceipts;
    protected Decimal? _QtySODropShip;
    protected Decimal? _QtyPODropShipOrders;
    protected Decimal? _QtyPODropShipPrepared;
    protected Decimal? _QtyPODropShipReceipts;
    protected DateTime? _ExpireDate;
    protected DateTime? _ReceiptDate;
    protected string _LotSerTrack;
    protected byte[] _tstamp;
    protected DateTime? _LastModifiedDateTime;

    [StockItem(IsKey = true)]
    [PXDefault]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [SubItem(IsKey = true)]
    [PXDefault]
    public virtual int? SubItemID
    {
      get => this._SubItemID;
      set => this._SubItemID = value;
    }

    [Site(IsKey = true)]
    [PXDefault]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [Location(typeof (FSINLotSerialNbrAttribute.ApptINLotSerialStatus.siteID), IsKey = true)]
    [PXDefault]
    public virtual int? LocationID
    {
      get => this._LocationID;
      set => this._LocationID = value;
    }

    /// <exclude />
    [PXDBInt(IsKey = true)]
    [PXDefault]
    public virtual int? CostCenterID { get; set; }

    [PXDefault]
    [LotSerialNbr(IsKey = true)]
    public virtual string LotSerialNbr
    {
      get => this._LotSerialNbr;
      set => this._LotSerialNbr = value;
    }

    [PXDBLong]
    [PXDefault]
    public virtual long? CostID
    {
      get => this._CostID;
      set => this._CostID = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty. Allocated in Appointments", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
    public virtual Decimal? QtyFSSrvOrdBooked
    {
      get => this._QtyFSSrvOrdBooked;
      set => this._QtyFSSrvOrdBooked = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty. Allocated in Service Order", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
    public virtual Decimal? QtyFSSrvOrdAllocated
    {
      get => this._QtyFSSrvOrdAllocated;
      set => this._QtyFSSrvOrdAllocated = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(FieldClass = "SERVICEMANAGEMENT")]
    public virtual Decimal? QtyFSSrvOrdPrepared
    {
      get => this._QtyFSSrvOrdPrepared;
      set => this._QtyFSSrvOrdPrepared = value;
    }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Allocated in Service Order", Enabled = false)]
    public virtual bool? SrvOrdAllocation { get; set; }

    [PXInt]
    [PXDefault(-1)]
    public virtual int? SODetID { get; set; }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty. On Hand")]
    public virtual Decimal? QtyOnHand
    {
      get => this._QtyOnHand;
      set => this._QtyOnHand = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty. Available")]
    public virtual Decimal? QtyAvail
    {
      get => this._QtyAvail;
      set => this._QtyAvail = value;
    }

    [PXDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyNotAvail
    {
      get => this._QtyNotAvail;
      set => this._QtyNotAvail = value;
    }

    [PXDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyExpired
    {
      get => this._QtyExpired;
      set => this._QtyExpired = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty. Hard Available")]
    public virtual Decimal? QtyHardAvail
    {
      get => this._QtyHardAvail;
      set => this._QtyHardAvail = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty. Available for Issue")]
    public virtual Decimal? QtyActual
    {
      get => this._QtyActual;
      set => this._QtyActual = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyInTransit
    {
      get => this._QtyInTransit;
      set => this._QtyInTransit = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyInTransitToSO
    {
      get => this._QtyInTransitToSO;
      set => this._QtyInTransitToSO = value;
    }

    public Decimal? QtyINReplaned
    {
      get => new Decimal?(0M);
      set
      {
      }
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOPrepared
    {
      get => this._QtyPOPrepared;
      set => this._QtyPOPrepared = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOOrders
    {
      get => this._QtyPOOrders;
      set => this._QtyPOOrders = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOReceipts
    {
      get => this._QtyPOReceipts;
      set => this._QtyPOReceipts = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtySOBackOrdered
    {
      get => this._QtySOBackOrdered;
      set => this._QtySOBackOrdered = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtySOPrepared
    {
      get => this._QtySOPrepared;
      set => this._QtySOPrepared = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtySOBooked
    {
      get => this._QtySOBooked;
      set => this._QtySOBooked = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtySOShipped
    {
      get => this._QtySOShipped;
      set => this._QtySOShipped = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtySOShipping
    {
      get => this._QtySOShipping;
      set => this._QtySOShipping = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Inventory Issues")]
    public virtual Decimal? QtyINIssues
    {
      get => this._QtyINIssues;
      set => this._QtyINIssues = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Inventory Receipts")]
    public virtual Decimal? QtyINReceipts
    {
      get => this._QtyINReceipts;
      set => this._QtyINReceipts = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty Demanded by Kit Assembly")]
    public virtual Decimal? QtyINAssemblyDemand
    {
      get => this._QtyINAssemblyDemand;
      set => this._QtyINAssemblyDemand = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Kit Assembly")]
    public virtual Decimal? QtyINAssemblySupply
    {
      get => this._QtyINAssemblySupply;
      set => this._QtyINAssemblySupply = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity In Transit to Production.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty In Transit to Production")]
    public virtual Decimal? QtyInTransitToProduction
    {
      get => this._QtyInTransitToProduction;
      set => this._QtyInTransitToProduction = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity Production Supply Prepared.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty Production Supply Prepared")]
    public virtual Decimal? QtyProductionSupplyPrepared
    {
      get => this._QtyProductionSupplyPrepared;
      set => this._QtyProductionSupplyPrepared = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production Supply.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production Supply")]
    public virtual Decimal? QtyProductionSupply
    {
      get => this._QtyProductionSupply;
      set => this._QtyProductionSupply = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Purchase for Prod. Prepared.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Purchase for Prod. Prepared")]
    public virtual Decimal? QtyPOFixedProductionPrepared
    {
      get => this._QtyPOFixedProductionPrepared;
      set => this._QtyPOFixedProductionPrepared = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Purchase for Production.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Purchase for Production")]
    public virtual Decimal? QtyPOFixedProductionOrders
    {
      get => this._QtyPOFixedProductionOrders;
      set => this._QtyPOFixedProductionOrders = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production Demand Prepared.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production Demand Prepared")]
    public virtual Decimal? QtyProductionDemandPrepared
    {
      get => this._QtyProductionDemandPrepared;
      set => this._QtyProductionDemandPrepared = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production Demand.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production Demand")]
    public virtual Decimal? QtyProductionDemand
    {
      get => this._QtyProductionDemand;
      set => this._QtyProductionDemand = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production Allocated.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production Allocated")]
    public virtual Decimal? QtyProductionAllocated
    {
      get => this._QtyProductionAllocated;
      set => this._QtyProductionAllocated = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On SO to Production.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On SO to Production")]
    public virtual Decimal? QtySOFixedProduction
    {
      get => this._QtySOFixedProduction;
      set => this._QtySOFixedProduction = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyFixedFSSrvOrd
    {
      get => this._QtyFixedFSSrvOrd;
      set => this._QtyFixedFSSrvOrd = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOFixedFSSrvOrd
    {
      get => this._QtyPOFixedFSSrvOrd;
      set => this._QtyPOFixedFSSrvOrd = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOFixedFSSrvOrdPrepared
    {
      get => this._QtyPOFixedFSSrvOrdPrepared;
      set => this._QtyPOFixedFSSrvOrdPrepared = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOFixedFSSrvOrdReceipts
    {
      get => this._QtyPOFixedFSSrvOrdReceipts;
      set => this._QtyPOFixedFSSrvOrdReceipts = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production to Purchase.
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production to Purchase", Enabled = false)]
    public virtual Decimal? QtyProdFixedPurchase
    {
      get => this._QtyProdFixedPurchase;
      set => this._QtyProdFixedPurchase = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production to Production
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production to Production", Enabled = false)]
    public virtual Decimal? QtyProdFixedProduction
    {
      get => this._QtyProdFixedProduction;
      set => this._QtyProdFixedProduction = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production for Prod. Prepared
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production for Prod. Prepared", Enabled = false)]
    public virtual Decimal? QtyProdFixedProdOrdersPrepared
    {
      get => this._QtyProdFixedProdOrdersPrepared;
      set => this._QtyProdFixedProdOrdersPrepared = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production for Production
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production for Production", Enabled = false)]
    public virtual Decimal? QtyProdFixedProdOrders
    {
      get => this._QtyProdFixedProdOrders;
      set => this._QtyProdFixedProdOrders = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production for SO Prepared
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production for SO Prepared", Enabled = false)]
    public virtual Decimal? QtyProdFixedSalesOrdersPrepared
    {
      get => this._QtyProdFixedSalesOrdersPrepared;
      set => this._QtyProdFixedSalesOrdersPrepared = value;
    }

    /// <summary>
    /// Production / Manufacturing
    /// Specifies the quantity On Production for SO
    /// </summary>
    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Qty On Production for SO", Enabled = false)]
    public virtual Decimal? QtyProdFixedSalesOrders
    {
      get => this._QtyProdFixedSalesOrders;
      set => this._QtyProdFixedSalesOrders = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtySOFixed
    {
      get => this._QtySOFixed;
      set => this._QtySOFixed = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOFixedOrders
    {
      get => this._QtyPOFixedOrders;
      set => this._QtyPOFixedOrders = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOFixedPrepared
    {
      get => this._QtyPOFixedPrepared;
      set => this._QtyPOFixedPrepared = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPOFixedReceipts
    {
      get => this._QtyPOFixedReceipts;
      set => this._QtyPOFixedReceipts = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtySODropShip
    {
      get => this._QtySODropShip;
      set => this._QtySODropShip = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPODropShipOrders
    {
      get => this._QtyPODropShipOrders;
      set => this._QtyPODropShipOrders = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPODropShipPrepared
    {
      get => this._QtyPODropShipPrepared;
      set => this._QtyPODropShipPrepared = value;
    }

    [PXDBQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? QtyPODropShipReceipts
    {
      get => this._QtyPODropShipReceipts;
      set => this._QtyPODropShipReceipts = value;
    }

    [PXDBDate(BqlField = typeof (INItemLotSerial.expireDate))]
    [PXUIField(DisplayName = "Expiry Date")]
    public virtual DateTime? ExpireDate
    {
      get => this._ExpireDate;
      set => this._ExpireDate = value;
    }

    [PXDBDate]
    [PXDefault]
    public virtual DateTime? ReceiptDate
    {
      get => this._ReceiptDate;
      set => this._ReceiptDate = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXDefault]
    public virtual string LotSerTrack
    {
      get => this._LotSerTrack;
      set => this._LotSerTrack = value;
    }

    [PXDBTimestamp]
    public virtual byte[] tstamp
    {
      get => this._tstamp;
      set => this._tstamp = value;
    }

    [PXDBLastModifiedDateTime]
    public virtual DateTime? LastModifiedDateTime
    {
      get => this._LastModifiedDateTime;
      set => this._LastModifiedDateTime = value;
    }

    public static FSINLotSerialNbrAttribute.ApptINLotSerialStatus GetApptINLotSerialStatus(
      PXCache inCache,
      INLotSerialStatusByCostCenter inLotSerialStatus,
      int? soDetID,
      PXCache apptCache,
      ref List<string> lotSerialStatusFields)
    {
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus inLotSerialStatus1 = new FSINLotSerialNbrAttribute.ApptINLotSerialStatus();
      if (lotSerialStatusFields == null)
      {
        lotSerialStatusFields = new List<string>();
        foreach (Type bqlField in inCache.BqlFields)
        {
          Type field = bqlField;
          string str = ((IEnumerable<string>) apptCache.Fields).Where<string>((Func<string, bool>) (f => f.ToLower() == field.Name.ToLower())).FirstOrDefault<string>();
          if (str != null)
            lotSerialStatusFields.Add(str);
        }
      }
      foreach (string str in lotSerialStatusFields)
        apptCache.SetValue((object) inLotSerialStatus1, str, inCache.GetValue((object) inLotSerialStatus, str));
      inLotSerialStatus1.SODetID = soDetID;
      return inLotSerialStatus1;
    }

    public class PK : 
      PrimaryKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.inventoryID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.subItemID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.siteID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.locationID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.costCenterID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lotSerialNbr, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.sODetID>
    {
      public static FSINLotSerialNbrAttribute.ApptINLotSerialStatus Find(
        PXGraph graph,
        int? inventoryID,
        int? subItemID,
        int? siteID,
        int? locationID,
        int? costCenterID,
        string lotSerialNbr,
        int? sODetID,
        PKFindOptions options = 0)
      {
        return (FSINLotSerialNbrAttribute.ApptINLotSerialStatus) PrimaryKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.inventoryID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.subItemID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.siteID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.locationID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.costCenterID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lotSerialNbr, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.sODetID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) costCenterID, (object) lotSerialNbr, (object) sODetID, options);
      }
    }

    public static class FK
    {
      public class Location : 
        PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.locationID>
      {
      }

      public class LocationStatus : 
        PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.inventoryID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.subItemID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.siteID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.locationID>
      {
      }

      public class SubItem : 
        PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.subItemID>
      {
      }

      public class InventoryItem : 
        PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.inventoryID>
      {
      }

      public class ItemLotSerial : 
        PrimaryKeyOf<INItemLotSerial>.By<INItemLotSerial.inventoryID, INItemLotSerial.lotSerialNbr>.ForeignKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.inventoryID, FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lotSerialNbr>
      {
      }

      public class Site : 
        PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.siteID>
      {
      }

      public class CostCnter : 
        PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.ForeignKeyOf<FSINLotSerialNbrAttribute.ApptINLotSerialStatus>.By<FSINLotSerialNbrAttribute.ApptINLotSerialStatus.costCenterID>
      {
      }
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.inventoryID>
    {
    }

    public abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.subItemID>
    {
    }

    public abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.siteID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.locationID>
    {
    }

    /// <exclude />
    public abstract class costCenterID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.costCenterID>
    {
    }

    public abstract class lotSerialNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lotSerialNbr>
    {
      public const int LENGTH = 100;
    }

    public abstract class costID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.costID>
    {
    }

    public abstract class qtyFSSrvOrdBooked : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyFSSrvOrdBooked>
    {
    }

    public abstract class qtyFSSrvOrdAllocated : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyFSSrvOrdAllocated>
    {
    }

    public abstract class qtyFSSrvOrdPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyFSSrvOrdPrepared>
    {
    }

    public abstract class srvOrdAllocation : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.srvOrdAllocation>
    {
    }

    public abstract class sODetID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.sODetID>
    {
    }

    public abstract class qtyOnHand : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyOnHand>
    {
    }

    public abstract class qtyAvail : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyAvail>
    {
    }

    public abstract class qtyNotAvail : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyNotAvail>
    {
    }

    public abstract class qtyExpired : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyExpired>
    {
    }

    public abstract class qtyHardAvail : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyHardAvail>
    {
    }

    public abstract class qtyActual : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyActual>
    {
    }

    public abstract class qtyInTransit : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyInTransit>
    {
    }

    public abstract class qtyInTransitToSO : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyInTransitToSO>
    {
    }

    public abstract class qtyPOPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOPrepared>
    {
    }

    public abstract class qtyPOOrders : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOOrders>
    {
    }

    public abstract class qtyPOReceipts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOReceipts>
    {
    }

    public abstract class qtySOBackOrdered : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySOBackOrdered>
    {
    }

    public abstract class qtySOPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySOPrepared>
    {
    }

    public abstract class qtySOBooked : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySOBooked>
    {
    }

    public abstract class qtySOShipped : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySOShipped>
    {
    }

    public abstract class qtySOShipping : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySOShipping>
    {
    }

    public abstract class qtyINIssues : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyINIssues>
    {
    }

    public abstract class qtyINReceipts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyINReceipts>
    {
    }

    public abstract class qtyINAssemblyDemand : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyINAssemblyDemand>
    {
    }

    public abstract class qtyINAssemblySupply : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyINAssemblySupply>
    {
    }

    public abstract class qtyInTransitToProduction : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyInTransitToProduction>
    {
    }

    public abstract class qtyProductionSupplyPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProductionSupplyPrepared>
    {
    }

    public abstract class qtyProductionSupply : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProductionSupply>
    {
    }

    public abstract class qtyPOFixedProductionPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedProductionPrepared>
    {
    }

    public abstract class qtyPOFixedProductionOrders : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedProductionOrders>
    {
    }

    public abstract class qtyProductionDemandPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProductionDemandPrepared>
    {
    }

    public abstract class qtyProductionDemand : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProductionDemand>
    {
    }

    public abstract class qtyProductionAllocated : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProductionAllocated>
    {
    }

    public abstract class qtySOFixedProduction : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySOFixedProduction>
    {
    }

    public abstract class qtyFixedFSSrvOrd : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyFixedFSSrvOrd>
    {
    }

    public abstract class qtyPOFixedFSSrvOrd : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedFSSrvOrd>
    {
    }

    public abstract class qtyPOFixedFSSrvOrdPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedFSSrvOrdPrepared>
    {
    }

    public abstract class qtyPOFixedFSSrvOrdReceipts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedFSSrvOrdReceipts>
    {
    }

    public abstract class qtyProdFixedPurchase : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProdFixedPurchase>
    {
    }

    public abstract class qtyProdFixedProduction : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProdFixedProduction>
    {
    }

    public abstract class qtyProdFixedProdOrdersPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProdFixedProdOrdersPrepared>
    {
    }

    public abstract class qtyProdFixedProdOrders : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProdFixedProdOrders>
    {
    }

    public abstract class qtyProdFixedSalesOrdersPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProdFixedSalesOrdersPrepared>
    {
    }

    public abstract class qtyProdFixedSalesOrders : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyProdFixedSalesOrders>
    {
    }

    public abstract class qtySOFixed : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySOFixed>
    {
    }

    public abstract class qtyPOFixedOrders : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedOrders>
    {
    }

    public abstract class qtyPOFixedPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedPrepared>
    {
    }

    public abstract class qtyPOFixedReceipts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPOFixedReceipts>
    {
    }

    public abstract class qtySODropShip : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtySODropShip>
    {
    }

    public abstract class qtyPODropShipOrders : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPODropShipOrders>
    {
    }

    public abstract class qtyPODropShipPrepared : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPODropShipPrepared>
    {
    }

    public abstract class qtyPODropShipReceipts : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.qtyPODropShipReceipts>
    {
    }

    public abstract class expireDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.expireDate>
    {
    }

    public abstract class receiptDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.receiptDate>
    {
    }

    public abstract class lotSerTrack : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lotSerTrack>
    {
    }

    public abstract class Tstamp : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.Tstamp>
    {
    }

    public abstract class lastModifiedDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      FSINLotSerialNbrAttribute.ApptINLotSerialStatus.lastModifiedDateTime>
    {
    }
  }
}
