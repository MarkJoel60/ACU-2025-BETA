// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSplit2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select2<SOLineSplit, InnerJoin<PX.Objects.SO.SOOrderType, On<SOLineSplit.FK.OrderType>, InnerJoin<SOOrderTypeOperation, On<SOLineSplit.FK.OrderTypeOperation>>>>), new Type[] {typeof (SOLineSplit)})]
[Serializable]
public class SOLineSplit2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SplitLineNbr;
  protected string _Operation;
  protected bool? _Completed;
  protected int? _InventoryID;
  protected int? _SiteID;
  protected int? _ToSiteID;
  protected string _LotSerialNbr;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _ShippedQty;
  protected Decimal? _BaseShippedQty;
  protected DateTime? _ShipDate;
  protected string _PlanType;
  protected bool? _POCreate;
  protected bool? _IsAllocated;
  protected Guid? _RefNoteID;
  protected long? _PlanID;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOLineNbr;
  protected int? _SOSplitLineNbr;

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLineSplit.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLineSplit.orderNbr))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLineSplit.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLineSplit.splitLineNbr))]
  public virtual int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a", BqlField = typeof (SOLineSplit.operation))]
  [PXUIField(DisplayName = "Operation")]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBBool(BqlField = typeof (SOLineSplit.completed))]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  [PXDBInt(BqlField = typeof (SOLineSplit.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (SOLineSplit.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt(BqlField = typeof (SOLineSplit.toSiteID))]
  public virtual int? ToSiteID
  {
    get => this._ToSiteID;
    set => this._ToSiteID = value;
  }

  /// <exclude />
  [PXDBInt(BqlField = typeof (SOLineSplit.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (SOLineSplit.lotSerialNbr))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [INUnit(typeof (SOLineSplit2.inventoryID), BqlField = typeof (SOLineSplit.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLineSplit.qty))]
  [PXDefault]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLineSplit.baseQty))]
  [PXDefault]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (SOLineSplit.shippedQty))]
  [PXDefault]
  public virtual Decimal? ShippedQty
  {
    get => this._ShippedQty;
    set => this._ShippedQty = value;
  }

  [PXDBBaseQtyWithOrigQty(typeof (SOLineSplit2.uOM), typeof (SOLineSplit2.shippedQty), typeof (SOLineSplit2.uOM), typeof (SOLineSplit2.baseQty), typeof (SOLineSplit2.qty), BqlField = typeof (SOLineSplit.baseShippedQty))]
  [PXDefault]
  public virtual Decimal? BaseShippedQty
  {
    get => this._BaseShippedQty;
    set => this._BaseShippedQty = value;
  }

  [PXDBDate(BqlField = typeof (SOLineSplit.shipDate))]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (SOOrderTypeOperation.orderPlanType))]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXDBBool(BqlField = typeof (SOLineSplit.pOCreate))]
  public virtual bool? POCreate { get; set; }

  [PXDBBool(BqlField = typeof (SOLineSplit.isAllocated))]
  public virtual bool? IsAllocated { get; set; }

  [PXRefNote(BqlField = typeof (SOLineSplit.refNoteID))]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  [PXDBLong(BqlField = typeof (SOLineSplit.planID), IsImmutable = true)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (SOLineSplit.sOOrderType))]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLineSplit.sOOrderNbr))]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBInt(BqlField = typeof (SOLineSplit.sOLineNbr))]
  public virtual int? SOLineNbr
  {
    get => this._SOLineNbr;
    set => this._SOLineNbr = value;
  }

  [PXDBInt(BqlField = typeof (SOLineSplit.sOSplitLineNbr))]
  public virtual int? SOSplitLineNbr
  {
    get => this._SOSplitLineNbr;
    set => this._SOSplitLineNbr = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (SOLineSplit.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (SOLineSplit.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (SOLineSplit.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.lineNbr>
  {
  }

  public abstract class splitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.splitLineNbr>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.operation>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit2.completed>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.siteID>
  {
  }

  public abstract class toSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.toSiteID>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.costCenterID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.lotSerialNbr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit2.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit2.baseQty>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLineSplit2.shippedQty>
  {
  }

  public abstract class baseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLineSplit2.baseShippedQty>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLineSplit2.shipDate>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.planType>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit2.pOCreate>
  {
  }

  public abstract class isAllocated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLineSplit2.isAllocated>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOLineSplit2.refNoteID>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOLineSplit2.planID>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineSplit2.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.sOLineNbr>
  {
  }

  public abstract class sOSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLineSplit2.sOSplitLineNbr>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOLineSplit2.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineSplit2.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLineSplit2.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOLineSplit2.Tstamp>
  {
  }
}
