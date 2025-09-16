// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSApptLineSplit
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Appointment Lot/Serial Detail")]
[Serializable]
public class FSApptLineSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSDetail,
  ILSMaster,
  IItemPlanMaster
{
  protected int? _LineNbr;
  protected int? _OrigSplitLineNbr;
  protected 
  #nullable disable
  string _Operation;
  protected int? _SplitLineNbr;
  protected int? _ParentSplitLineNbr;
  protected short? _InvtMult;
  protected int? _InventoryID;
  protected string _LineType;
  protected string _TranType;
  protected string _PlanType;
  protected long? _PlanID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected int? _SubItemID;
  protected string _LotSerialNbr;
  protected string _LastLotSerialNbr;
  protected string _LotSerClassID;
  protected string _AssignedNbr;
  protected DateTime? _ExpireDate;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected DateTime? _ApptDate;
  protected bool? _Confirmed;
  protected bool? _Released;
  protected bool? _IsUnassigned;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected bool? _POCreate;
  protected bool? _POCompleted;
  protected bool? _POCancelled;
  protected string _POSource;
  protected int? _VendorID;
  protected int? _POSiteID;
  protected string _POType;
  protected string _PONbr;
  protected int? _POLineNbr;
  protected string _POReceiptType;
  protected string _POReceiptNbr;
  protected Guid? _RefNoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (FSApptLineSplit.FK.Appointment))]
  public virtual string ApptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (FSAppointmentDet.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false, Visible = false)]
  [PXParent(typeof (FSApptLineSplit.FK.ApptLine))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(4, IsFixed = true)]
  [PXDefault(typeof (FSAppointmentDet.srvOrdType))]
  public virtual string OrigSrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXDBDefault(typeof (FSAppointment.soRefNbr), DefaultForUpdate = false)]
  public virtual string OrigSrvOrdNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBInt]
  public virtual int? OrigSplitLineNbr
  {
    get => this._OrigSplitLineNbr;
    set => this._OrigSplitLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string OrigPlanType { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXDefault(typeof (FSAppointmentDet.operation))]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSAppointment.splitLineCntr))]
  [PXUIField(DisplayName = "Allocation ID", Visible = false, IsReadOnly = true)]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Parent Allocation ID", Visible = false, IsReadOnly = true)]
  public virtual int? ParentSplitLineNbr
  {
    get => this._ParentSplitLineNbr;
    set => this._ParentSplitLineNbr = value;
  }

  [PXDBShort]
  [PXDefault(typeof (shortMinus1))]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [Inventory(Enabled = false, Visible = true)]
  [PXDefault(typeof (FSAppointmentDet.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (Selector<FSApptLineSplit.inventoryID, Switch<Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, Or<PX.Objects.IN.InventoryItem.kitItem, Equal<True>>>, SOLineType.inventory, Case<Where<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>, SOLineType.nonInventory>>, SOLineType.miscCharge>>))]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<FSApptLineSplit.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public bool? IsStockItem { get; set; }

  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<FSApptLineSplit.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>>, False>, True>))]
  public bool? IsComponentItem { get; set; }

  [PXString(3, IsFixed = true)]
  [PXUnboundDefault(typeof (FSAppointmentDet.tranType))]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  public virtual DateTime? TranDate => this.ApptDate;

  [PXDBString(2, IsFixed = true)]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXDBLong(IsImmutable = true)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [Site]
  [PXDefault(typeof (FSAppointmentDet.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [LocationAvail(typeof (FSApptLineSplit.inventoryID), typeof (FSApptLineSplit.subItemID), typeof (FSApptLineSplit.costCenterID), typeof (FSApptLineSplit.siteID), typeof (FSApptLineSplit.tranType), typeof (FSApptLineSplit.invtMult))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [SubItem(typeof (FSApptLineSplit.inventoryID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [FSApptLotSerialNbr(typeof (FSApptLineSplit.siteID), typeof (FSApptLineSplit.inventoryID), typeof (FSApptLineSplit.subItemID), typeof (FSApptLineSplit.locationID), typeof (FSAppointmentDet.lotSerialNbr), typeof (FSApptLineSplit.costCenterID), FieldClass = "LotSerial")]
  [PXDefault]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXString(100, IsUnicode = true)]
  public virtual string LastLotSerialNbr
  {
    get => this._LastLotSerialNbr;
    set => this._LastLotSerialNbr = value;
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

  [INExpireDate(typeof (FSApptLineSplit.inventoryID), Visible = false, FieldClass = "LotSerial")]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [INUnit(typeof (FSApptLineSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (FSApptLineSplit.uOM), typeof (FSApptLineSplit.baseQty), InventoryUnitType.BaseUnit)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (FSAppointment.effDocDate))]
  public virtual DateTime? ApptDate
  {
    get => this._ApptDate;
    set => this._ApptDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Confirmed")]
  public virtual bool? Confirmed
  {
    get => this._Confirmed;
    set => this._Confirmed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnassigned
  {
    get => this._IsUnassigned;
    set => this._IsUnassigned = value;
  }

  [PXFormula(typeof (Selector<FSApptLineSplit.locationID, INLocation.projectID>))]
  [PXInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXFormula(typeof (Selector<FSApptLineSplit.locationID, INLocation.taskID>))]
  [PXInt]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <exclude />
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.IN.CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Mark for PO", Visible = true, Enabled = false)]
  public virtual bool? POCreate
  {
    get => this._POCreate;
    set => this._POCreate = new bool?(value.GetValueOrDefault());
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? POCompleted
  {
    get => this._POCompleted;
    set => this._POCompleted = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? POCancelled
  {
    get => this._POCancelled;
    set => this._POCancelled = value;
  }

  [PXDBString]
  [PXFormula(typeof (Current<FSAppointmentDet.pOSource>))]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value;
  }

  [PXDBInt]
  [PXFormula(typeof (Current<FSAppointmentDet.poVendorID>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  public virtual int? POSiteID
  {
    get => this._POSiteID;
    set => this._POSiteID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PO Type", Enabled = false)]
  [POOrderType.RBDList]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<FSApptLineSplit.pOType>>>>), DescriptionField = typeof (PX.Objects.PO.POOrder.orderDesc))]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PO Receipt Type", Enabled = false)]
  public virtual string POReceiptType
  {
    get => this._POReceiptType;
    set => this._POReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<PX.Objects.PO.POReceipt.receiptType, Equal<Current<FSApptLineSplit.pOReceiptType>>>>), DescriptionField = typeof (PX.Objects.PO.POReceipt.invoiceNbr))]
  public virtual string POReceiptNbr
  {
    get => this._POReceiptNbr;
    set => this._POReceiptNbr = value;
  }

  [PXUIField(DisplayName = "Related Document", Enabled = false)]
  [FSApptLineSplit.PXRefNote]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (FSAppointmentDet.curyInfoID))]
  [PXDefault(typeof (FSAppointmentDet.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSApptLineSplit.curyInfoID), typeof (FSApptLineSplit.unitCost))]
  [PXUIField]
  [PXDefault(typeof (FSAppointmentDet.curyUnitCost))]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(typeof (FSAppointmentDet.unitCost))]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBCurrency(typeof (FSApptLineSplit.curyInfoID), typeof (FSApptLineSplit.extCost))]
  [PXUIField(DisplayName = "Ext. Cost", Enabled = false)]
  [PXFormula(typeof (Mult<FSApptLineSplit.curyUnitCost, FSApptLineSplit.qty>))]
  [PXDefault(typeof (Mult<FSApptLineSplit.curyUnitCost, FSApptLineSplit.qty>))]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBPriceCost]
  public virtual Decimal? ExtCost { get; set; }

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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public virtual FSSODetSplit FSSODetSplitRow { get; set; }

  bool? ILSMaster.IsIntercompany => new bool?(false);

  public class PK : 
    PrimaryKeyOf<FSApptLineSplit>.By<FSApptLineSplit.srvOrdType, FSApptLineSplit.apptNbr, FSApptLineSplit.lineNbr, FSApptLineSplit.splitLineNbr>
  {
    public static FSApptLineSplit Find(
      PXGraph graph,
      string srvOrdType,
      string apptNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (FSApptLineSplit) PrimaryKeyOf<FSApptLineSplit>.By<FSApptLineSplit.srvOrdType, FSApptLineSplit.apptNbr, FSApptLineSplit.lineNbr, FSApptLineSplit.splitLineNbr>.FindBy(graph, (object) srvOrdType, (object) apptNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.srvOrdType, FSApptLineSplit.apptNbr>
    {
    }

    public class ApptLine : 
      PrimaryKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.refNbr, FSAppointmentDet.lineNbr>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.srvOrdType, FSApptLineSplit.apptNbr, FSApptLineSplit.lineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.siteID>
    {
    }

    public class OrigLineSplit : 
      PrimaryKeyOf<FSSODetSplit>.By<FSSODetSplit.srvOrdType, FSSODetSplit.refNbr, FSSODetSplit.lineNbr, FSSODetSplit.splitLineNbr>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.origSrvOrdType, FSApptLineSplit.origSrvOrdNbr, FSApptLineSplit.origLineNbr, FSApptLineSplit.origSplitLineNbr>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.inventoryID, FSApptLineSplit.subItemID, FSApptLineSplit.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.locationID>
    {
    }

    public class CostCenter : 
      PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.costCenterID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.inventoryID, FSApptLineSplit.subItemID, FSApptLineSplit.siteID, FSApptLineSplit.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.inventoryID, FSApptLineSplit.subItemID, FSApptLineSplit.siteID, FSApptLineSplit.locationID, FSApptLineSplit.lotSerialNbr>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.vendorID>
    {
    }

    public class POSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.pOSiteID>
    {
    }

    public class POOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.pOType, FSApptLineSplit.pONbr>
    {
    }

    public class POLine : 
      PrimaryKeyOf<PX.Objects.PO.POLine>.By<PX.Objects.PO.POLine.orderType, PX.Objects.PO.POLine.orderNbr, PX.Objects.PO.POLine.lineNbr>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.pOType, FSApptLineSplit.pONbr, FSApptLineSplit.pOLineNbr>
    {
    }

    public class POReceipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<FSApptLineSplit>.By<FSApptLineSplit.pOReceiptType, FSApptLineSplit.pOReceiptNbr>
    {
    }
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.srvOrdType>
  {
  }

  public abstract class apptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.apptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.lineNbr>
  {
  }

  public abstract class origSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.origSrvOrdType>
  {
  }

  public abstract class origSrvOrdNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.origSrvOrdNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.origLineNbr>
  {
  }

  public abstract class origSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSApptLineSplit.origSplitLineNbr>
  {
  }

  public abstract class origPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.origPlanType>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.operation>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.splitLineNbr>
  {
  }

  public abstract class parentSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSApptLineSplit.parentSplitLineNbr>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSApptLineSplit.invtMult>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.inventoryID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.lineType>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSApptLineSplit.isStockItem>
  {
  }

  public abstract class isComponentItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSApptLineSplit.isComponentItem>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.tranType>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.planType>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSApptLineSplit.planID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.locationID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.lotSerialNbr>
  {
  }

  public abstract class lastLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.lastLotSerialNbr>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.assignedNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSApptLineSplit.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSApptLineSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSApptLineSplit.baseQty>
  {
  }

  public abstract class apptDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSApptLineSplit.apptDate>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSApptLineSplit.confirmed>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSApptLineSplit.released>
  {
  }

  public abstract class isUnassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSApptLineSplit.isUnassigned>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.taskID>
  {
  }

  /// <exclude />
  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.costCenterID>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSApptLineSplit.pOCreate>
  {
  }

  public abstract class pOCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSApptLineSplit.pOCompleted>
  {
  }

  public abstract class pOCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSApptLineSplit.pOCancelled>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.pOSource>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.vendorID>
  {
  }

  public abstract class pOSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.pOSiteID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSApptLineSplit.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSApptLineSplit.pOLineNbr>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.pOReceiptNbr>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSApptLineSplit.refNoteID>
  {
  }

  public class PXRefNoteAttribute : PXRefNoteBaseAttribute
  {
    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      // ISSUE: method pointer
      PXButtonDelegate pxButtonDelegate = new PXButtonDelegate((object) this, __methodptr(\u003CCacheAttached\u003Eb__1_0));
      string str = $"{sender.GetItemType().Name}${((PXEventSubscriberAttribute) this)._FieldName}$Link";
      sender.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(typeof (FSAppointment)), (object) sender.Graph, (object) str, (object) pxButtonDelegate, (object) new PXEventSubscriberAttribute[1]
      {
        (PXEventSubscriberAttribute) new PXUIFieldAttribute()
        {
          MapEnableRights = (PXCacheRights) 1
        }
      });
    }

    public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (e.Row is FSApptLineSplit row && !string.IsNullOrEmpty(row.PONbr))
      {
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (PX.Objects.PO.POOrder)], new object[2]
        {
          (object) row.POType,
          (object) row.PONbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (PX.Objects.PO.POOrder), new object[2]
        {
          (object) row.POType,
          (object) row.PONbr
        });
      }
      else if (row != null && !string.IsNullOrEmpty(row.POReceiptNbr))
      {
        e.ReturnValue = this.GetEntityRowID(sender.Graph.Caches[typeof (PX.Objects.PO.POReceipt)], new object[2]
        {
          (object) row.POReceiptType,
          (object) row.POReceiptNbr
        });
        e.ReturnState = (object) PXRefNoteBaseAttribute.PXLinkState.CreateInstance(e.ReturnState, typeof (PX.Objects.PO.POReceipt), new object[2]
        {
          (object) row.POReceiptType,
          (object) row.POReceiptNbr
        });
      }
      else
        base.FieldSelecting(sender, e);
    }
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSApptLineSplit.curyInfoID>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSApptLineSplit.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSApptLineSplit.unitCost>
  {
  }

  public abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSApptLineSplit.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSApptLineSplit.extCost>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSApptLineSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSApptLineSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSApptLineSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSApptLineSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSApptLineSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSApptLineSplit.Tstamp>
  {
  }
}
