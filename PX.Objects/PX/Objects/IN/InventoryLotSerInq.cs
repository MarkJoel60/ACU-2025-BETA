// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryLotSerInq
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
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.RQ;
using System;
using System.Collections;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
[Serializable]
public class InventoryLotSerInq : PXGraph<
#nullable disable
InventoryLotSerInq>
{
  public PXFilter<INLotSerFilter> Filter;
  public PXCancel<INLotSerFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<InventoryLotSerInq.INTranSplit, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<InventoryLotSerInq.INTranSplit.tranInventoryID>, And<Match<InventoryItem, Current<AccessInfo.userName>>>>, LeftJoin<InventoryLotSerInq.POReceiptLine, On<InventoryLotSerInq.POReceiptLine.receiptType, Equal<InventoryLotSerInq.INTranSplit.pOReceiptType>, And<InventoryLotSerInq.POReceiptLine.receiptNbr, Equal<InventoryLotSerInq.INTranSplit.pOReceiptNbr>, And<InventoryLotSerInq.POReceiptLine.lineNbr, Equal<InventoryLotSerInq.INTranSplit.pOReceiptLineNbr>>>>, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<InventoryLotSerInq.POReceiptLine.vendorID>>, LeftJoin<InventoryLotSerInq.SOLine, On<InventoryLotSerInq.SOLine.orderType, Equal<InventoryLotSerInq.INTranSplit.sOOrderType>, And<InventoryLotSerInq.SOLine.orderNbr, Equal<InventoryLotSerInq.INTranSplit.sOOrderNbr>, And<InventoryLotSerInq.SOLine.lineNbr, Equal<InventoryLotSerInq.INTranSplit.sOOrderLineNbr>>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<InventoryLotSerInq.SOLine.customerID>>, InnerJoin<INSite, On2<InventoryLotSerInq.INTranSplit.FK.Site, And<Match<INSite, Current<AccessInfo.userName>>>>>>>>>>, Where<Current<INLotSerFilter.lotSerialNbr>, IsNotNull, And2<Where2<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>, Or<INSite.branchID, InsideBranchesOf<Current<INLotSerFilter.orgBAccountID>>>>, And2<Where<Current<INLotSerFilter.startDate>, IsNull, Or<InventoryLotSerInq.INTranSplit.tranTranDate, GreaterEqual<Current<INLotSerFilter.startDate>>>>, And<Where<Current<INLotSerFilter.endDate>, IsNull, Or<InventoryLotSerInq.INTranSplit.tranTranDate, LessEqual<Current<INLotSerFilter.endDate>>>>>>>>, OrderBy<Desc<InventoryLotSerInq.INTranSplit.releasedDateTime>>> Records;
  public PXAction<INLotSerFilter> viewSummary;
  public PXAction<INLotSerFilter> viewAllocDet;

  public InventoryLotSerInq()
  {
    ((PXSelectBase) this.Records).Cache.AllowInsert = false;
    ((PXSelectBase) this.Records).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Records).Cache.AllowDelete = false;
    ((PXSelectBase) this.Records).WhereAndCurrent<INLotSerFilter>();
    PXUIFieldAttribute.SetVisible<InventoryLotSerInq.INTranSplit.sOOrderType>(((PXSelectBase) this.Records).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<InventoryLotSerInq.INTranSplit.sOOrderNbr>(((PXSelectBase) this.Records).Cache, (object) null, true);
    PXCache cach = ((PXGraph) this).Caches[typeof (InventoryLotSerInq.POReceiptLine)];
    PXUIFieldAttribute.SetVisible<InventoryLotSerInq.POReceiptLine.receiptType>(cach, (object) null, true);
    PXUIFieldAttribute.SetVisible<InventoryLotSerInq.POReceiptLine.receiptNbr>(cach, (object) null, true);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.AR.Customer.acctCD>(((PXGraph) this).Caches[typeof (PX.Objects.AR.Customer)], "Customer ID");
    PXUIFieldAttribute.SetDisplayName<InventoryLotSerInq.POReceiptLine.receiptType>(cach, "Receipt Type");
    PXUIFieldAttribute.SetDisplayName<InventoryLotSerInq.POReceiptLine.receiptNbr>(cach, "Receipt Nbr.");
  }

  protected virtual void INLotSerFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INLotSerFilter row = (INLotSerFilter) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetVisible<InventoryLotSerInq.INTranSplit.tranInventoryID>(((PXSelectBase) this.Records).Cache, (object) null, !row.InventoryID.HasValue);
    PXUIFieldAttribute.SetVisible<InventoryLotSerInq.INTranSplit.siteID>(((PXSelectBase) this.Records).Cache, (object) null, !row.SiteID.HasValue);
    PXUIFieldAttribute.SetVisible<InventoryLotSerInq.INTranSplit.locationID>(((PXSelectBase) this.Records).Cache, (object) null, !row.LocationID.HasValue);
  }

  protected virtual void INTranSplit_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    InventoryLotSerInq.INTranSplit row = (InventoryLotSerInq.INTranSplit) e.Row;
    INLotSerFilter current = ((PXSelectBase<INLotSerFilter>) this.Filter).Current;
    if (row == null || current == null)
      return;
    if (current.ShowAdjUnitCost.GetValueOrDefault())
    {
      InventoryLotSerInq.INTranSplit inTranSplit = row;
      Decimal? nullable1 = row.TotalQty;
      Decimal? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = row.TotalQty;
        Decimal num = 0M;
        if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        {
          Decimal? totalCost = row.TotalCost;
          Decimal? additionalCost = row.AdditionalCost;
          nullable1 = totalCost.HasValue & additionalCost.HasValue ? new Decimal?(totalCost.GetValueOrDefault() + additionalCost.GetValueOrDefault()) : new Decimal?();
          Decimal? totalQty = row.TotalQty;
          nullable2 = nullable1.HasValue & totalQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / totalQty.GetValueOrDefault()) : new Decimal?();
          goto label_6;
        }
      }
      nullable2 = new Decimal?(0M);
label_6:
      inTranSplit.TranUnitCost = nullable2;
    }
    else
    {
      InventoryLotSerInq.INTranSplit inTranSplit = row;
      Decimal? nullable3 = row.TotalQty;
      Decimal? nullable4;
      if (nullable3.HasValue)
      {
        nullable3 = row.TotalQty;
        Decimal num = 0M;
        if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
        {
          nullable3 = row.TotalCost;
          Decimal? totalQty = row.TotalQty;
          nullable4 = nullable3.HasValue & totalQty.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / totalQty.GetValueOrDefault()) : new Decimal?();
          goto label_11;
        }
      }
      nullable4 = new Decimal?(0M);
label_11:
      inTranSplit.TranUnitCost = nullable4;
    }
  }

  protected virtual void INLotSerFilter_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    e.NewValue = (object) new DateTime(dateTime.Year, 1, 1);
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXButton]
  [PXUIField(DisplayName = "Inventory Summary")]
  protected virtual IEnumerable ViewSummary(PXAdapter a)
  {
    if (((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current != null)
    {
      PXSegmentedState valueExt = ((PXSelectBase) this.Records).Cache.GetValueExt<InventoryLotSerInq.INTranSplit.subItemID>((object) ((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current) as PXSegmentedState;
      InventorySummaryEnq.Redirect(((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current.InventoryID, valueExt != null ? (string) ((PXFieldState) valueExt).Value : (string) null, ((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current.SiteID, ((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current.LocationID, false);
    }
    return a.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Allocation Details")]
  protected virtual IEnumerable ViewAllocDet(PXAdapter a)
  {
    if (((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current != null)
    {
      PXSegmentedState valueExt = ((PXSelectBase) this.Records).Cache.GetValueExt<InventoryLotSerInq.INTranSplit.subItemID>((object) ((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current) as PXSegmentedState;
      InventoryAllocDetEnq.Redirect(((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current.InventoryID, valueExt != null ? (string) ((PXFieldState) valueExt).Value : (string) null, ((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current.LotSerialNbr, ((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current.SiteID, ((PXSelectBase<InventoryLotSerInq.INTranSplit>) this.Records).Current.LocationID);
    }
    return a.Get();
  }

  [Serializable]
  public class POReceiptLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _ReceiptNbr;
    protected string _ReceiptType;
    protected int? _LineNbr;
    protected int? _VendorID;

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Receipt Nbr.", Visible = true)]
    [POReceiptType.RefNbr(typeof (Search2<PX.Objects.PO.POReceipt.receiptNbr, InnerJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.PO.POReceipt.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Optional<InventoryLotSerInq.INTranSplit.pOReceiptType>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, OrderBy<Desc<PX.Objects.PO.POReceipt.receiptNbr>>>), Filterable = true)]
    public virtual string ReceiptNbr
    {
      get => this._ReceiptNbr;
      set => this._ReceiptNbr = value;
    }

    [PXDBString(2, IsFixed = true, IsKey = true)]
    [POReceiptType.List]
    [PXDBDefault(typeof (PX.Objects.PO.POReceipt.receiptType))]
    [PXUIField(DisplayName = "Receipt Type", Visible = true)]
    public virtual string ReceiptType
    {
      get => this._ReceiptType;
      set => this._ReceiptType = value;
    }

    [PXDBInt(IsKey = true)]
    [PXUIField]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [Vendor]
    [PXDBDefault(typeof (PX.Objects.PO.POReceipt.vendorID))]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    public abstract class receiptNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.POReceiptLine.receiptNbr>
    {
    }

    public abstract class receiptType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.POReceiptLine.receiptType>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.POReceiptLine.lineNbr>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.POReceiptLine.vendorID>
    {
    }
  }

  [Serializable]
  public class SOLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _OrderType;
    protected string _OrderNbr;
    protected int? _LineNbr;
    protected int? _CustomerID;

    [PXDBString(2, IsKey = true, IsFixed = true)]
    [PXDefault(typeof (PX.Objects.SO.SOOrder.orderType))]
    [PXUIField(DisplayName = "Order Type", Visible = true)]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Order Nbr.", Visible = true)]
    [PX.Objects.SO.SO.RefNbr(typeof (Search2<PX.Objects.SO.SOOrder.orderNbr, InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.SO.SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<InventoryLotSerInq.INTranSplit.sOOrderType>>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, OrderBy<Desc<PX.Objects.SO.SOOrder.orderNbr>>>), Filterable = true)]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBInt(IsKey = true)]
    [PXLineNbr(typeof (PX.Objects.SO.SOOrder.lineCntr))]
    [PXUIField(DisplayName = "Line Nbr.", Visible = true)]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [PXDBInt]
    [PXDefault(typeof (PX.Objects.SO.SOOrder.customerID))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.SOLine.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.SOLine.orderNbr>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryLotSerInq.SOLine.lineNbr>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.SOLine.customerID>
    {
    }
  }

  [PXProjection(typeof (Select2<InventoryLotSerInq.INTranSplit, InnerJoin<INTran, On<InventoryLotSerInq.INTranSplit.FK.Tran>>>), Persistent = false)]
  [Serializable]
  public class INTranSplit : 
    PXBqlTable,
    IBqlTable,
    IBqlTableSystemDataStorage,
    ILSDetail,
    ILSMaster,
    IItemPlanMaster
  {
    protected bool? _Selected = new bool?(false);
    protected string _DocType;
    protected string _TranType;
    protected string _RefNbr;
    protected int? _LineNbr;
    protected int? _SplitLineNbr;
    protected DateTime? _TranDate;
    protected short? _InvtMult;
    protected int? _InventoryID;
    protected int? _SubItemID;
    protected int? _CostSubItemID;
    protected int? _CostSiteID;
    protected int? _SiteID;
    protected int? _LocationID;
    protected string _LotSerialNbr;
    protected string _LotSerClassID;
    protected string _AssignedNbr;
    protected DateTime? _ExpireDate;
    protected bool? _Released;
    protected string _UOM;
    protected Decimal? _Qty;
    protected Decimal? _BaseQty;
    protected Decimal? _InvQty;
    protected Decimal? _InvBaseQty;
    protected long? _PlanID;
    protected int? _ProjectID;
    protected int? _TaskID;
    protected Decimal? _AdditionalCost;
    protected Decimal? _TranUnitCost;
    protected int? _TranInventoryID;
    protected Decimal? _TranBaseQty;
    protected string _SOOrderType;
    protected string _SOOrderNbr;
    protected int? _SOOrderLineNbr;
    protected string _POReceiptType;
    protected string _POReceiptNbr;
    protected int? _POReceiptLineNbr;
    protected DateTime? _TranTranDate;

    [PXBool]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBString(1, IsFixed = true, IsKey = true)]
    [PXUIField(DisplayName = "Doc. Type", Visible = false)]
    public virtual string DocType
    {
      get => this._DocType;
      set => this._DocType = value;
    }

    [PXDBString(3, IsFixed = true)]
    [INTranType.List]
    [PXUIField(DisplayName = "Tran. Type")]
    public virtual string TranType
    {
      get => this._TranType;
      set => this._TranType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDBDefault(typeof (INRegister.refNbr))]
    [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<Current<InventoryLotSerInq.INTranSplit.docType>>>>))]
    [PXUIField(DisplayName = "Reference Nbr.")]
    public virtual string RefNbr
    {
      get => this._RefNbr;
      set => this._RefNbr = value;
    }

    [PXDBInt(IsKey = true)]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [PXDBInt(IsKey = true)]
    [PXLineNbr(typeof (INRegister.lineCntr))]
    public virtual int? SplitLineNbr
    {
      get => this._SplitLineNbr;
      set => this._SplitLineNbr = value;
    }

    [PXDBDate]
    [PXDBDefault(typeof (INRegister.tranDate))]
    [PXUIField(DisplayName = "Tran. Date")]
    public virtual DateTime? TranDate
    {
      get => this._TranDate;
      set => this._TranDate = value;
    }

    [PXDBShort]
    public virtual short? InvtMult
    {
      get => this._InvtMult;
      set => this._InvtMult = value;
    }

    [StockItem(Visible = true, DisplayName = "Inventory ID", BqlField = typeof (InventoryLotSerInq.INTranSplit.inventoryID))]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    public bool? IsStockItem
    {
      get => new bool?(true);
      set
      {
      }
    }

    [SubItem(typeof (InventoryLotSerInq.INTranSplit.inventoryID), typeof (LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.subItemID, Equal<INSubItem.subItemID>, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<InventoryLotSerInq.INTranSplit.inventoryID>>, And<INSiteStatusByCostCenter.siteID, Equal<Optional<InventoryLotSerInq.INTranSplit.siteID>>, And<INSiteStatusByCostCenter.costCenterID, Equal<Optional<InventoryLotSerInq.INTranSplit.costCenterID>>>>>>>), BqlField = typeof (InventoryLotSerInq.INTranSplit.subItemID))]
    public virtual int? SubItemID
    {
      get => this._SubItemID;
      set => this._SubItemID = value;
    }

    [PXInt]
    public virtual int? CostSubItemID
    {
      get => this._CostSubItemID;
      set => this._CostSubItemID = value;
    }

    [PXInt]
    public virtual int? CostSiteID
    {
      get => this._CostSiteID;
      set => this._CostSiteID = value;
    }

    [Site]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [LocationAvail(typeof (InventoryLotSerInq.INTranSplit.inventoryID), typeof (InventoryLotSerInq.INTranSplit.subItemID), typeof (INTran.costCenterID), typeof (InventoryLotSerInq.INTranSplit.siteID), typeof (InventoryLotSerInq.INTranSplit.tranType), typeof (InventoryLotSerInq.INTranSplit.invtMult))]
    public virtual int? LocationID
    {
      get => this._LocationID;
      set => this._LocationID = value;
    }

    [PXUIField(DisplayName = "Lot/Serial Nbr.")]
    [PXDBString(100, IsUnicode = true)]
    public virtual string LotSerialNbr
    {
      get => this._LotSerialNbr;
      set => this._LotSerialNbr = value;
    }

    [PXString(10, IsUnicode = true)]
    public virtual string LotSerClassID
    {
      get => this._LotSerClassID;
      set => this._LotSerClassID = value;
    }

    [PXString(30, IsUnicode = true)]
    public virtual string AssignedNbr
    {
      get => this._AssignedNbr;
      set => this._AssignedNbr = value;
    }

    [INExpireDate(typeof (InventoryLotSerInq.INTranSplit.inventoryID))]
    public virtual DateTime? ExpireDate
    {
      get => this._ExpireDate;
      set => this._ExpireDate = value;
    }

    [PXDBBool]
    [PXUIField(DisplayName = "Released")]
    public virtual bool? Released
    {
      get => this._Released;
      set => this._Released = value;
    }

    [INUnit(typeof (InventoryLotSerInq.INTranSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
    public virtual string UOM
    {
      get => this._UOM;
      set => this._UOM = value;
    }

    [PXDBQuantity(typeof (InventoryLotSerInq.INTranSplit.uOM), typeof (InventoryLotSerInq.INTranSplit.baseQty))]
    public virtual Decimal? Qty
    {
      get => this._Qty;
      set => this._Qty = value;
    }

    [PXDBQuantity]
    public virtual Decimal? BaseQty
    {
      get => this._BaseQty;
      set => this._BaseQty = value;
    }

    [PXDBCalced(typeof (Mult<InventoryLotSerInq.INTranSplit.qty, InventoryLotSerInq.INTranSplit.invtMult>), typeof (Decimal))]
    [PXQuantity]
    [PXUIField(DisplayName = "Quantity")]
    public virtual Decimal? InvQty
    {
      get => this._InvQty;
      set => this._InvQty = value;
    }

    [PXDBCalced(typeof (Mult<InventoryLotSerInq.INTranSplit.baseQty, InventoryLotSerInq.INTranSplit.invtMult>), typeof (Decimal))]
    [PXQuantity]
    [PXUIField(DisplayName = "Quantity")]
    public virtual Decimal? InvBaseQty
    {
      get => this._InvBaseQty;
      set => this._InvBaseQty = value;
    }

    [PXDBLong]
    public virtual long? PlanID
    {
      get => this._PlanID;
      set => this._PlanID = value;
    }

    [PXFormula(typeof (Selector<InventoryLotSerInq.INTranSplit.locationID, INLocation.projectID>))]
    [PXInt]
    public virtual int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    [PXFormula(typeof (Selector<InventoryLotSerInq.INTranSplit.locationID, INLocation.taskID>))]
    [PXInt]
    public virtual int? TaskID
    {
      get => this._TaskID;
      set => this._TaskID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? IsIntercompany { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(Visible = false)]
    public virtual Decimal? TotalQty { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(Visible = false)]
    public virtual Decimal? TotalCost { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(Visible = false)]
    public virtual Decimal? AdditionalCost
    {
      get => this._AdditionalCost;
      set => this._AdditionalCost = value;
    }

    [PXBaseCury(MinValue = 0.0)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unit Cost")]
    public virtual Decimal? TranUnitCost
    {
      get => this._TranUnitCost;
      set => this._TranUnitCost = value;
    }

    [PXDefault]
    [StockItem(DisplayName = "Inventory ID", BqlField = typeof (INTran.inventoryID))]
    public virtual int? TranInventoryID
    {
      get => this._TranInventoryID;
      set => this._TranInventoryID = value;
    }

    [PXDBQuantity(BqlField = typeof (INTran.baseQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? TranBaseQty
    {
      get => this._TranBaseQty;
      set => this._TranBaseQty = value;
    }

    /// <exclude />
    [PXDBInt(BqlField = typeof (INTran.costCenterID))]
    [PXDefault]
    public virtual int? CostCenterID { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (INTran.sOOrderType))]
    [PXUIField(DisplayName = "SO Order Type", Visible = false)]
    public virtual string SOOrderType
    {
      get => this._SOOrderType;
      set => this._SOOrderType = value;
    }

    [PXDBString(15, IsUnicode = true, BqlField = typeof (INTran.sOOrderNbr))]
    [PXUIField(DisplayName = "Order Nbr.", Visible = false, Enabled = false)]
    [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<InventoryLotSerInq.INTranSplit.sOOrderType>>>>))]
    public virtual string SOOrderNbr
    {
      get => this._SOOrderNbr;
      set => this._SOOrderNbr = value;
    }

    [PXDBInt(BqlField = typeof (INTran.sOOrderLineNbr))]
    public virtual int? SOOrderLineNbr
    {
      get => this._SOOrderLineNbr;
      set => this._SOOrderLineNbr = value;
    }

    [PXDBString(2, IsFixed = true, BqlField = typeof (INTran.pOReceiptType))]
    [PXUIField(DisplayName = "PO Receipt Type", Visible = false, Enabled = false)]
    public virtual string POReceiptType
    {
      get => this._POReceiptType;
      set => this._POReceiptType = value;
    }

    [PXDBString(15, IsUnicode = true, BqlField = typeof (INTran.pOReceiptNbr))]
    [PXUIField(DisplayName = "PO Receipt Nbr.", Visible = false, Enabled = false)]
    public virtual string POReceiptNbr
    {
      get => this._POReceiptNbr;
      set => this._POReceiptNbr = value;
    }

    [PXDBInt(BqlField = typeof (INTran.pOReceiptLineNbr))]
    public virtual int? POReceiptLineNbr
    {
      get => this._POReceiptLineNbr;
      set => this._POReceiptLineNbr = value;
    }

    [PXDBDate(BqlField = typeof (INTran.tranDate))]
    [PXDBDefault(typeof (INRegister.tranDate))]
    public virtual DateTime? TranTranDate
    {
      get => this._TranTranDate;
      set => this._TranTranDate = value;
    }

    /// <exclude />
    [PXDBDateAndTime]
    public virtual DateTime? ReleasedDateTime { get; set; }

    public class PK : 
      PrimaryKeyOf<InventoryLotSerInq.INTranSplit>.By<InventoryLotSerInq.INTranSplit.docType, InventoryLotSerInq.INTranSplit.refNbr, InventoryLotSerInq.INTranSplit.lineNbr, InventoryLotSerInq.INTranSplit.splitLineNbr>
    {
      public InventoryLotSerInq.INTranSplit Find(
        PXGraph graph,
        string docType,
        string refNbr,
        int? lineNbr,
        int? splitLineNbr)
      {
        return (InventoryLotSerInq.INTranSplit) PrimaryKeyOf<InventoryLotSerInq.INTranSplit>.By<InventoryLotSerInq.INTranSplit.docType, InventoryLotSerInq.INTranSplit.refNbr, InventoryLotSerInq.INTranSplit.lineNbr, InventoryLotSerInq.INTranSplit.splitLineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) splitLineNbr, (PKFindOptions) 0);
      }
    }

    public static class FK
    {
      public class Register : 
        PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<InventoryLotSerInq.INTranSplit>.By<InventoryLotSerInq.INTranSplit.docType, InventoryLotSerInq.INTranSplit.refNbr>
      {
      }

      public class Tran : 
        PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<InventoryLotSerInq.INTranSplit>.By<InventoryLotSerInq.INTranSplit.docType, InventoryLotSerInq.INTranSplit.refNbr, InventoryLotSerInq.INTranSplit.lineNbr>
      {
      }

      public class Inventory : 
        PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<InventoryLotSerInq.INTranSplit>.By<InventoryLotSerInq.INTranSplit.inventoryID>
      {
      }

      public class Site : 
        PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<InventoryLotSerInq.INTranSplit>.By<InventoryLotSerInq.INTranSplit.siteID>
      {
      }
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.selected>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.docType>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.tranType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.refNbr>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.lineNbr>
    {
    }

    public abstract class splitLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.splitLineNbr>
    {
    }

    public abstract class tranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.tranDate>
    {
    }

    public abstract class invtMult : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.invtMult>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.inventoryID>
    {
    }

    public abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.subItemID>
    {
    }

    public abstract class costSubItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.costSubItemID>
    {
    }

    public abstract class costSiteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.costSiteID>
    {
    }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryLotSerInq.INTranSplit.siteID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.locationID>
    {
    }

    public abstract class lotSerialNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.lotSerialNbr>
    {
    }

    public abstract class lotSerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.lotSerClassID>
    {
    }

    public abstract class assignedNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.assignedNbr>
    {
    }

    public abstract class expireDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.expireDate>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.released>
    {
    }

    public abstract class uOM : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryLotSerInq.INTranSplit.uOM>
    {
    }

    public abstract class qty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.qty>
    {
    }

    public abstract class baseQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.baseQty>
    {
    }

    public abstract class invQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.invQty>
    {
    }

    public abstract class invBaseQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.invBaseQty>
    {
    }

    public abstract class planID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.planID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.projectID>
    {
    }

    public abstract class taskID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryLotSerInq.INTranSplit.taskID>
    {
    }

    public abstract class isIntercompany : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.isIntercompany>
    {
    }

    public abstract class totalQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.totalQty>
    {
    }

    public abstract class totalCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.totalCost>
    {
    }

    public abstract class additionalCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.additionalCost>
    {
    }

    public abstract class tranUnitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.tranUnitCost>
    {
    }

    public abstract class tranInventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.tranInventoryID>
    {
    }

    public abstract class tranBaseQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.tranBaseQty>
    {
    }

    public abstract class costCenterID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.costCenterID>
    {
    }

    public abstract class sOOrderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.sOOrderType>
    {
    }

    public abstract class sOOrderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.sOOrderNbr>
    {
    }

    public abstract class sOOrderLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.sOOrderLineNbr>
    {
    }

    public abstract class pOReceiptType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.pOReceiptType>
    {
    }

    public abstract class pOReceiptNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.pOReceiptNbr>
    {
    }

    public abstract class pOReceiptLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.pOReceiptLineNbr>
    {
    }

    public abstract class tranTranDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.tranTranDate>
    {
    }

    public abstract class releasedDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      InventoryLotSerInq.INTranSplit.releasedDateTime>
    {
    }
  }
}
