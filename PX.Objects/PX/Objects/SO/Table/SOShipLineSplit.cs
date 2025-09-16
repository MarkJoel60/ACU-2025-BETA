// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Table.SOShipLineSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.Table;

/// <summary>It's a table, not a projection.</summary>
[Serializable]
public class SOShipLineSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSDetail,
  ILSMaster,
  IItemPlanMaster,
  ILSGeneratedDetail,
  IItemPlanSOShipSource,
  IItemPlanSource
{
  protected 
  #nullable disable
  string _ShipmentNbr;
  protected int? _LineNbr;
  protected string _OrigOrderType;
  protected string _OrigOrderNbr;
  protected int? _OrigLineNbr;
  protected int? _OrigSplitLineNbr;
  protected string _Operation;
  protected int? _SplitLineNbr;
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
  protected DateTime? _ShipDate;
  protected bool? _Confirmed;
  protected bool? _Released;
  protected bool? _IsUnassigned;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PX.Objects.SO.SOShipment.shipmentNbr))]
  [PXParent(typeof (Select<PX.Objects.SO.SOShipment, Where<PX.Objects.SO.SOShipment.shipmentNbr, Equal<Current<SOShipLineSplit.shipmentNbr>>>>))]
  [PXParent(typeof (Select<SOShipLine, Where<SOShipLine.shipmentNbr, Equal<Current<SOShipLineSplit.shipmentNbr>>, And<SOShipLine.lineNbr, Equal<Current<SOShipLineSplit.lineNbr>>>>>))]
  public virtual string ShipmentNbr
  {
    get => this._ShipmentNbr;
    set => this._ShipmentNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (SOShipLine.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (SOShipLine.origOrderType))]
  public virtual string OrigOrderType
  {
    get => this._OrigOrderType;
    set => this._OrigOrderType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault(typeof (SOShipLine.origOrderNbr))]
  public virtual string OrigOrderNbr
  {
    get => this._OrigOrderNbr;
    set => this._OrigOrderNbr = value;
  }

  [PXDBInt]
  [PXDefault(typeof (SOShipLine.origLineNbr))]
  public virtual int? OrigLineNbr
  {
    get => this._OrigLineNbr;
    set => this._OrigLineNbr = value;
  }

  [PXDBInt]
  [PXDefault(typeof (SOShipLine.origSplitLineNbr))]
  public virtual int? OrigSplitLineNbr
  {
    get => this._OrigSplitLineNbr;
    set => this._OrigSplitLineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (SOShipLine.origPlanType))]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXDefault(typeof (SOShipLine.operation))]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBShort]
  [PXDefault(typeof (INTran.invtMult))]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [Inventory(Enabled = false, Visible = true)]
  [PXDefault(typeof (SOShipLine.inventoryID))]
  [PXForeignReference(typeof (Field<SOShipLineSplit.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (SOShipLine.lineType))]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<SOShipLineSplit.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  public bool? IsStockItem { get; set; }

  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<SOShipLineSplit.inventoryID, Equal<Current<SOShipLine.inventoryID>>>, False>, True>))]
  public bool? IsComponentItem { get; set; }

  [PXDBBool]
  [PXDefault(typeof (SOShipLine.isIntercompany))]
  public virtual bool? IsIntercompany { get; set; }

  [PXFormula(typeof (Selector<SOShipLine.operation, SOOrderTypeOperation.iNDocType>))]
  [PXString(3, IsFixed = true)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  public virtual DateTime? TranDate => this._ShipDate;

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (SOShipLine.planType))]
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

  [Site(Enabled = false, Visible = false)]
  [PXDefault(typeof (SOShipLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [SOLocationAvail(typeof (SOShipLineSplit.inventoryID), typeof (SOShipLineSplit.subItemID), typeof (SOShipLine.costCenterID), typeof (SOShipLineSplit.siteID), typeof (SOShipLineSplit.tranType), typeof (SOShipLineSplit.invtMult))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [SubItem(typeof (SOShipLineSplit.inventoryID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [SOShipLotSerialNbr(typeof (SOShipLineSplit.siteID), typeof (SOShipLineSplit.inventoryID), typeof (SOShipLineSplit.subItemID), typeof (SOShipLineSplit.locationID), typeof (SOShipLine.lotSerialNbr), typeof (SOShipLine.costCenterID))]
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

  [PXDBBool]
  [PXDefault(false)]
  [HasFieldBeenModified(typeof (SOShipLineSplit.lotSerialNbr), OriginalValueField = typeof (SOShipLineSplit.OriginalValues.originalLotSerialNbr), InvertResult = true)]
  public bool? HasGeneratedLotSerialNbr { get; set; }

  [SOShipExpireDate(typeof (SOShipLineSplit.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [INUnit(typeof (SOShipLineSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (SOShipLineSplit.uOM), typeof (SOShipLineSplit.baseQty), InventoryUnitType.BaseUnit)]
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

  [PXDBQuantity(typeof (SOShipLineSplit.uOM), typeof (SOShipLineSplit.basePickedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Picked Quantity")]
  public virtual Decimal? PickedQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BasePickedQty { get; set; }

  [PXDBQuantity(typeof (SOShipLineSplit.uOM), typeof (SOShipLineSplit.basePackedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Packed Quantity")]
  public virtual Decimal? PackedQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BasePackedQty { get; set; }

  [PXDBDate]
  [PXDBDefault(typeof (PX.Objects.SO.SOShipment.shipDate))]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
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
  [PXDefault(typeof (SOShipLine.isUnassigned))]
  public virtual bool? IsUnassigned
  {
    get => this._IsUnassigned;
    set => this._IsUnassigned = value;
  }

  [PXInt]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXInt]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>
  {
    public static SOShipLineSplit Find(
      PXGraph graph,
      string shipmentNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (SOShipLineSplit) PrimaryKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>.FindBy(graph, (object) shipmentNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Shipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr>
    {
    }

    public class ShipmentLine : 
      PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentNbr, SOShipLine.lineNbr>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr>
    {
    }

    public class ShipmentLineSplit : 
      PrimaryKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.locationID>
    {
    }

    public class PlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.planType>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.planID>
    {
    }

    public class OriginalOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.origOrderType>
    {
    }

    public class OriginalOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.origOrderType, SOShipLineSplit.origOrderNbr>
    {
    }

    public class OriginalOrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.origOrderType, SOShipLineSplit.origOrderNbr, SOShipLineSplit.origLineNbr>
    {
    }

    public class OriginalOrderLineSplit : 
      PrimaryKeyOf<PX.Objects.SO.SOLineSplit>.By<PX.Objects.SO.SOLineSplit.orderType, PX.Objects.SO.SOLineSplit.orderNbr, PX.Objects.SO.SOLineSplit.lineNbr, PX.Objects.SO.SOLineSplit.splitLineNbr>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.origOrderType, SOShipLineSplit.origOrderNbr, SOShipLineSplit.origLineNbr, SOShipLineSplit.origSplitLineNbr>
    {
    }

    public class OriginalPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.origPlanType>
    {
    }

    public class OriginalLineSplit : 
      PrimaryKeyOf<PX.Objects.SO.SOLineSplit>.By<PX.Objects.SO.SOLineSplit.orderType, PX.Objects.SO.SOLineSplit.orderNbr, PX.Objects.SO.SOLineSplit.lineNbr, PX.Objects.SO.SOLineSplit.splitLineNbr>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.origOrderType, SOShipLineSplit.origOrderNbr, SOShipLineSplit.origLineNbr, SOShipLineSplit.origSplitLineNbr>
    {
    }

    public class Operation : 
      PrimaryKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType, SOOrderTypeOperation.operation>.ForeignKeyOf<SOShipLineSplit>.By<SOShipLineSplit.origOrderType, SOShipLineSplit.operation>
    {
    }
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplit.shipmentNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.lineNbr>
  {
  }

  public abstract class origOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.origOrderType>
  {
  }

  public abstract class origOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.origOrderNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.origLineNbr>
  {
  }

  public abstract class origSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOShipLineSplit.origSplitLineNbr>
  {
  }

  public abstract class origPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.origPlanType>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplit.operation>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.splitLineNbr>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOShipLineSplit.invtMult>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.inventoryID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplit.lineType>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLineSplit.isStockItem>
  {
  }

  public abstract class isComponentItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLineSplit.isComponentItem>
  {
  }

  public abstract class isIntercompany : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLineSplit.isIntercompany>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplit.tranType>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplit.planType>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOShipLineSplit.planID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.locationID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.subItemID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.lotSerialNbr>
  {
  }

  public abstract class lastLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.lastLotSerialNbr>
  {
  }

  public abstract class lotSerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplit.assignedNbr>
  {
  }

  public abstract class hasGeneratedLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOShipLineSplit.hasGeneratedLotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLineSplit.expireDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOShipLineSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLineSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLineSplit.baseQty>
  {
  }

  public abstract class pickedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLineSplit.pickedQty>
  {
  }

  public abstract class basePickedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplit.basePickedQty>
  {
  }

  public abstract class packedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOShipLineSplit.packedQty>
  {
  }

  public abstract class basePackedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOShipLineSplit.basePackedQty>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOShipLineSplit.shipDate>
  {
  }

  public abstract class confirmed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLineSplit.confirmed>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLineSplit.released>
  {
  }

  public abstract class isUnassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOShipLineSplit.isUnassigned>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOShipLineSplit.taskID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOShipLineSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLineSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOShipLineSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOShipLineSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOShipLineSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOShipLineSplit.Tstamp>
  {
  }

  public sealed class OriginalValues : PXCacheExtension<SOShipLineSplit>
  {
    [PXString]
    [PXDBCalced(typeof (SOShipLineSplit.lotSerialNbr), typeof (string))]
    public string OriginalLotSerialNbr { get; set; }

    public abstract class originalLotSerialNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOShipLineSplit.OriginalValues.originalLotSerialNbr>
    {
    }
  }
}
