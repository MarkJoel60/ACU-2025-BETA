// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Transaction Split")]
[Serializable]
public class INTranSplit : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSDetail,
  ILSMaster,
  IItemPlanMaster,
  IItemPlanINSource,
  IItemPlanSource,
  ILotSerialTrackableLineSplit
{
  protected 
  #nullable disable
  string _DocType;
  protected string _OrigModule;
  protected string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected string _POLineType;
  protected string _SOLineType;
  protected string _TransferType;
  protected int? _ToSiteID;
  protected int? _ToLocationID;
  protected int? _SplitLineNbr;
  protected DateTime? _TranDate;
  protected short? _InvtMult;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected int? _FromSiteID;
  protected int? _SiteID;
  protected int? _FromLocationID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected string _LotSerClassID;
  protected string _AssignedNbr;
  protected DateTime? _ExpireDate;
  protected bool? _Released;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _BaseQty;
  protected Decimal? _MaxTransferBaseQty;
  protected long? _PlanID;
  protected Decimal? _AdditionalCost;
  protected Decimal? _UnitCost;
  protected bool? _SkipCostUpdate;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (INTran.docType))]
  [PXParent(typeof (INTranSplit.FK.Register))]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  [INDocType.List]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDBDefault(typeof (INRegister.origModule))]
  [PXUIField(DisplayName = "Source", Enabled = false)]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault(typeof (INTran.tranType))]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXUIField(DisplayName = "Ref. Number", Enabled = false)]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INRegister.refNbr))]
  [PXParent(typeof (INTranSplit.FK.Tran))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (INTran.lineNbr))]
  [PXUIField(DisplayName = "Line Number", Enabled = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(2)]
  [PXDefault(typeof (INTran.pOLineType))]
  public virtual string POLineType
  {
    get => this._POLineType;
    set => this._POLineType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault(typeof (INTran.sOLineType))]
  public virtual string SOLineType
  {
    get => this._SOLineType;
    set => this._SOLineType = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (INRegister.transferType))]
  public virtual string TransferType
  {
    get => this._TransferType;
    set => this._TransferType = value;
  }

  [PXDBInt]
  [PXDefault(typeof (INTran.toSiteID))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  [PXDBInt]
  public virtual int? ToLocationID
  {
    get => this._ToLocationID;
    set => this._ToLocationID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (INRegister.lineCntr))]
  [PXUIField(DisplayName = "Split Line Number", Enabled = false)]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (INRegister.tranDate))]
  [PXUIField(DisplayName = "Transaction Date", Enabled = false)]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsIntercompany { get; set; }

  [PXDBShort]
  [PXDefault(typeof (INTran.invtMult))]
  [PXUIField(DisplayName = "Inventory Multiplier", Enabled = false)]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [StockItem(Visible = false)]
  [PXDefault(typeof (INTran.inventoryID))]
  [PXForeignReference(typeof (INTranSplit.FK.InventoryItem))]
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

  [PXString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<INTranSplit.inventoryID, InventoryItem.valMethod>))]
  public virtual string ValMethod { get; set; }

  [SubItem(typeof (INTranSplit.inventoryID), typeof (LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.subItemID, Equal<INSubItem.subItemID>, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<INTranSplit.inventoryID>>, And<INSiteStatusByCostCenter.siteID, Equal<Optional<INTranSplit.siteID>>, And<INSiteStatusByCostCenter.costCenterID, Equal<Optional<INTran.costCenterID>>>>>>>))]
  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current<INTranSplit.inventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [PXFormula(typeof (Default<INTranSplit.inventoryID>))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBInt]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBInt]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXInt]
  public virtual int? FromSiteID
  {
    get => this._FromSiteID;
    set => this._FromSiteID = value;
  }

  [Site(Enabled = false)]
  [PXDefault(typeof (INTran.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXInt]
  public virtual int? FromLocationID
  {
    get => this._FromLocationID;
    set => this._FromLocationID = value;
  }

  [LocationAvail(typeof (INTranSplit.inventoryID), typeof (INTranSplit.subItemID), typeof (INTran.costCenterID), typeof (INTranSplit.siteID), typeof (INTranSplit.tranType), typeof (INTranSplit.invtMult))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [INTranLotSerialNbr(typeof (INTranSplit.inventoryID), typeof (INTranSplit.subItemID), typeof (INTranSplit.locationID), typeof (INTran.lotSerialNbr), typeof (INTran.costCenterID))]
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

  /// <exclude />
  [PXString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Manufacturer Lot/Serial Nbr.", FieldClass = "LotSerialAttributes")]
  public string MfgLotSerialNbr { get; set; }

  [INExpireDate(typeof (INTranSplit.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [DBConditionalModifiedDateTime(typeof (INTranSplit.released), true)]
  public virtual DateTime? ReleasedDateTime { get; set; }

  [INUnit(typeof (INTranSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (INTranSplit.uOM), typeof (INTranSplit.baseQty), InventoryUnitType.BaseUnit)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
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

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<INTranSplit.invtMult, LessEqual<short0>>, decimal0>, INTranSplit.baseQty>), typeof (Decimal))]
  public virtual Decimal? QtyIn { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<INTranSplit.invtMult, GreaterEqual<short0>>, decimal0>, INTranSplit.baseQty>), typeof (Decimal))]
  public virtual Decimal? QtyOut { get; set; }

  [PXDBQuantity]
  public virtual Decimal? MaxTransferBaseQty
  {
    get => this._MaxTransferBaseQty;
    set => this._MaxTransferBaseQty = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  [PXDefault(typeof (INTran.origPlanType))]
  public virtual string OrigPlanType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsFixedInTransit { get; set; }

  [PXDBLong(IsImmutable = true)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalCost { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdditionalCost
  {
    get => this._AdditionalCost;
    set => this._AdditionalCost = value;
  }

  [PXDBPriceCostCalced(typeof (Switch<Case<Where<INTranSplit.totalQty, Equal<decimal0>>, decimal0>, Div<INTranSplit.totalCost, INTranSplit.totalQty>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXPriceCost]
  [PXUIField(DisplayName = "Unit Cost", Enabled = false)]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBPriceCostCalced(typeof (Switch<Case<Where<INTranSplit.totalQty, Equal<decimal0>>, INTranSplit.totalCost>, Div<Mult<INTranSplit.baseQty, INTranSplit.totalCost>, INTranSplit.totalQty>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXPriceCost]
  [PXUIField(DisplayName = "Estimated Cost", Enabled = false)]
  public virtual Decimal? EstCost { get; set; }

  [PXBool]
  public virtual bool? SkipCostUpdate
  {
    get => this._SkipCostUpdate;
    set => this._SkipCostUpdate = value;
  }

  [PXBool]
  public virtual bool? SkipQtyValidation { get; set; }

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

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  public virtual string ShipmentNbr { get; set; }

  [PXDBInt]
  public virtual int? ShipmentLineNbr { get; set; }

  [PXDBInt]
  public virtual int? ShipmentLineSplitNbr { get; set; }

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

  public static INTranSplit FromINTran(INTran item)
  {
    INTranSplit inTranSplit1 = new INTranSplit();
    inTranSplit1.DocType = item.DocType;
    inTranSplit1.TranType = item.TranType;
    inTranSplit1.RefNbr = item.RefNbr;
    inTranSplit1.LineNbr = item.LineNbr;
    inTranSplit1.SplitLineNbr = new int?(1);
    inTranSplit1.InventoryID = item.InventoryID;
    inTranSplit1.SiteID = item.SiteID;
    inTranSplit1.SubItemID = item.SubItemID;
    inTranSplit1.LocationID = item.LocationID;
    inTranSplit1.LotSerialNbr = item.LotSerialNbr;
    inTranSplit1.ExpireDate = item.ExpireDate;
    Decimal? qty1 = item.Qty;
    Decimal num = 0M;
    if (qty1.GetValueOrDefault() < num & qty1.HasValue)
    {
      INTranSplit inTranSplit2 = inTranSplit1;
      Decimal? qty2 = item.Qty;
      Decimal? nullable1 = qty2.HasValue ? new Decimal?(-qty2.GetValueOrDefault()) : new Decimal?();
      inTranSplit2.Qty = nullable1;
      INTranSplit inTranSplit3 = inTranSplit1;
      Decimal? baseQty = item.BaseQty;
      Decimal? nullable2 = baseQty.HasValue ? new Decimal?(-baseQty.GetValueOrDefault()) : new Decimal?();
      inTranSplit3.BaseQty = nullable2;
      INTranSplit inTranSplit4 = inTranSplit1;
      short? invtMult = item.InvtMult;
      short? nullable3 = invtMult.HasValue ? new short?(-invtMult.GetValueOrDefault()) : new short?();
      inTranSplit4.InvtMult = nullable3;
    }
    else
    {
      inTranSplit1.Qty = item.Qty;
      inTranSplit1.BaseQty = item.BaseQty;
      inTranSplit1.InvtMult = item.InvtMult;
    }
    inTranSplit1.UOM = item.UOM;
    inTranSplit1.TranDate = item.TranDate;
    inTranSplit1.Released = item.Released;
    inTranSplit1.POLineType = item.POLineType;
    inTranSplit1.SOLineType = item.SOLineType;
    inTranSplit1.ToSiteID = item.ToSiteID;
    inTranSplit1.ToLocationID = item.ToLocationID;
    inTranSplit1.OrigModule = item.OrigModule;
    inTranSplit1.IsIntercompany = item.IsIntercompany;
    inTranSplit1.ProjectID = item.ProjectID;
    inTranSplit1.TaskID = item.TaskID;
    return inTranSplit1;
  }

  public class PK : 
    PrimaryKeyOf<INTranSplit>.By<INTranSplit.docType, INTranSplit.refNbr, INTranSplit.lineNbr, INTranSplit.splitLineNbr>
  {
    public static INTranSplit Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (INTranSplit) PrimaryKeyOf<INTranSplit>.By<INTranSplit.docType, INTranSplit.refNbr, INTranSplit.lineNbr, INTranSplit.splitLineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public static class FK
  {
    public class Register : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INTranSplit>.By<INTranSplit.docType, INTranSplit.refNbr>
    {
    }

    public class Tran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INTranSplit>.By<INTranSplit.docType, INTranSplit.refNbr, INTranSplit.lineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.siteID>
    {
    }

    public class ToSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.toSiteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.locationID>
    {
    }

    public class ToLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.toLocationID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.costSubItemID>
    {
    }

    public class CostSite : 
      PrimaryKeyOf<INCostSite>.By<INCostSite.costSiteID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.costSiteID>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<INTranSplit>.By<INTranSplit.planID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<INTranSplit>.By<INTranSplit.inventoryID, INTranSplit.subItemID, INTranSplit.siteID, INTranSplit.locationID, INTranSplit.lotSerialNbr>
    {
    }

    public class SOShipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<INTranSplit>.By<INTranSplit.shipmentNbr>
    {
    }

    public class SOShipmentLine : 
      PrimaryKeyOf<SOShipLine>.By<SOShipLine.shipmentNbr, SOShipLine.lineNbr>.ForeignKeyOf<INTranSplit>.By<INTranSplit.shipmentNbr, INTranSplit.shipmentLineNbr>
    {
    }

    public class ShipLineSplit : 
      PrimaryKeyOf<SOShipLineSplit>.By<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>.ForeignKeyOf<INTranSplit>.By<INTranSplit.shipmentNbr, INTranSplit.shipmentLineNbr, INTranSplit.shipmentLineSplitNbr>
    {
    }

    public class OriginalPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<INTranSplit>.By<INTranSplit.origPlanType>
    {
    }

    public class RegisterItemLotSerialAttributesHeader : 
      PrimaryKeyOf<INRegisterItemLotSerialAttributesHeader>.By<INRegisterItemLotSerialAttributesHeader.docType, INRegisterItemLotSerialAttributesHeader.refNbr, INRegisterItemLotSerialAttributesHeader.inventoryID, INRegisterItemLotSerialAttributesHeader.lotSerialNbr>.ForeignKeyOf<INTranSplit>.By<INTranSplit.docType, INTranSplit.refNbr, INTranSplit.inventoryID, INTranSplit.lotSerialNbr>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.docType>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.origModule>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.lineNbr>
  {
  }

  public abstract class pOLineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.pOLineType>
  {
  }

  public abstract class sOLineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.sOLineType>
  {
  }

  public abstract class transferType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.transferType>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.toSiteID>
  {
  }

  public abstract class toLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.toLocationID>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.splitLineNbr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INTranSplit.tranDate>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTranSplit.isIntercompany>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INTranSplit.invtMult>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.inventoryID>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.valMethod>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.subItemID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.costSiteID>
  {
  }

  public abstract class fromSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.fromSiteID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.siteID>
  {
  }

  public abstract class fromLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.fromLocationID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.lotSerialNbr>
  {
  }

  public abstract class lotSerClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.lotSerClassID>
  {
  }

  public abstract class assignedNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.assignedNbr>
  {
  }

  public abstract class mfgLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranSplit.mfgLotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INTranSplit.expireDate>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTranSplit.released>
  {
  }

  public abstract class releasedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranSplit.releasedDateTime>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit.baseQty>
  {
  }

  public abstract class qtyIn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit.qtyIn>
  {
  }

  public abstract class qtyOut : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit.qtyOut>
  {
  }

  public abstract class maxTransferBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTranSplit.maxTransferBaseQty>
  {
  }

  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.origPlanType>
  {
  }

  public abstract class isFixedInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTranSplit.isFixedInTransit>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INTranSplit.planID>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit.totalQty>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit.totalCost>
  {
  }

  public abstract class additionalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTranSplit.additionalCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplit.unitCost>
  {
  }

  public abstract class estCost : IBqlField, IBqlOperand
  {
  }

  public abstract class skipCostUpdate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTranSplit.skipCostUpdate>
  {
  }

  public abstract class skipQtyValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTranSplit.skipQtyValidation>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.taskID>
  {
  }

  public abstract class shipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplit.shipmentNbr>
  {
  }

  public abstract class shipmentLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplit.shipmentLineNbr>
  {
  }

  public abstract class shipmentLineSplitNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INTranSplit.shipmentLineSplitNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTranSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INTranSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTranSplit.Tstamp>
  {
  }
}
