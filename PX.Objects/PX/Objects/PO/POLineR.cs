// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineR
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select<POLine>), Persistent = true)]
[Serializable]
public class POLineR : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _LineType;
  protected Decimal? _ReceivedQty;
  protected Decimal? _BaseReceivedQty;
  protected long? _CuryInfoID;
  protected string _UOM;
  protected bool? _Completed;
  protected bool? _Cancelled;
  protected Decimal? _OrderQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected int? _InventoryID;
  protected byte[] _tstamp;
  protected bool? _AllowComplete;
  protected DateTime? _DRTermStartDate;
  protected DateTime? _DRTermEndDate;

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POLine.orderType))]
  [PXDefault]
  [PXUIField]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POLine.orderNbr))]
  [PXDefault]
  [PXParent(typeof (POLineR.FK.OrderR))]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (POLine.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(BqlField = typeof (POLine.sortOrder))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POLine.lineType))]
  [PXUIField(DisplayName = "Line Type")]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBQuantity(BqlField = typeof (POLine.orderedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderedQty { get; set; }

  [PXDBBaseQtyWithOrigQty(typeof (POLineR.uOM), typeof (POLineR.orderedQty), typeof (POLineR.uOM), typeof (POLineR.baseOrderQty), typeof (POLineR.orderQty), BqlField = typeof (POLine.baseOrderedQty), HandleEmptyKey = true, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrderedQty { get; set; }

  [PXDBQuantity(BqlField = typeof (POLine.receivedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReceivedQty
  {
    get => this._ReceivedQty;
    set => this._ReceivedQty = value;
  }

  [PXDBBaseQtyWithOrigQty(typeof (POLineR.uOM), typeof (POLineR.receivedQty), typeof (POLineR.uOM), typeof (POLineR.baseOrderQty), typeof (POLineR.orderQty), BqlField = typeof (POLine.baseReceivedQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BaseReceivedQty
  {
    get => this._BaseReceivedQty;
    set => this._BaseReceivedQty = value;
  }

  [PXDBQuantity(BqlField = typeof (POLine.billedQty))]
  public virtual Decimal? BilledQty { get; set; }

  [PXDBLong(BqlField = typeof (POLine.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (POLineR.curyInfoID), typeof (POLineR.bLOrderedCost), BqlField = typeof (POLine.curyBLOrderedCost))]
  [PXDefault]
  public virtual Decimal? CuryBLOrderedCost { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POLine.bLOrderedCost))]
  [PXDefault]
  public virtual Decimal? BLOrderedCost { get; set; }

  [INUnit(typeof (POLineR.inventoryID), DisplayName = "UOM", BqlField = typeof (POLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBBool(BqlField = typeof (POLine.completed))]
  [PXDefault(false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<POLineR.lineType, NotEqual<POLineType.description>, And<POLineR.completed, Equal<False>>>, int1>, int0>), typeof (SumCalc<POOrderEntry.POOrderR.linesToCompleteCntr>))]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  [PXDBBool(BqlField = typeof (POLine.cancelled))]
  [PXDefault(false)]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXDBBool(BqlField = typeof (POLine.closed))]
  [PXUnboundFormula(typeof (Switch<Case<Where<POLineR.lineType, NotEqual<POLineType.description>, And<POLineR.closed, Equal<False>>>, int1>, int0>), typeof (SumCalc<POOrderEntry.POOrderR.linesToCloseCntr>))]
  public virtual bool? Closed { get; set; }

  [PXDBQuantity(typeof (POLineR.uOM), typeof (POLineR.baseOrderQty), HandleEmptyKey = true, MinValue = 0.0, BqlField = typeof (POLine.orderQty))]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POLine.baseOrderQty))]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXDBQuantity(typeof (POLineR.uOM), typeof (POLineR.baseOpenQty), HandleEmptyKey = true, BqlField = typeof (POLine.openQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POLine.baseOpenQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (POLine.extCost))]
  public virtual Decimal? ExtCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POLine.billedAmt))]
  public virtual Decimal? BilledAmt { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (POLine.retainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBInt(BqlField = typeof (POLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POLine.pOType))]
  [POOrderType.List]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POLine.pONbr))]
  public virtual string PONbr { get; set; }

  [PXDBInt(BqlField = typeof (POLine.pOLineNbr))]
  public virtual int? POLineNbr { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (POLine.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (POLine.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (POLine.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBBool(BqlField = typeof (POLine.allowComplete))]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? AllowComplete
  {
    get => this._AllowComplete;
    set => this._AllowComplete = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POLine.completePOLine))]
  [PXDefault]
  [CompletePOLineTypes.List]
  public virtual string CompletePOLine { get; set; }

  [PXDBDecimal(2, BqlField = typeof (POLine.rcptQtyThreshold))]
  public virtual Decimal? RcptQtyThreshold { get; set; }

  [PXDBDate(BqlField = typeof (POLine.dRTermStartDate))]
  [PXDefault]
  [PXUIField(DisplayName = "Term Start Date")]
  public DateTime? DRTermStartDate
  {
    get => this._DRTermStartDate;
    set => this._DRTermStartDate = value;
  }

  [PXDBDate(BqlField = typeof (POLine.dRTermEndDate))]
  [PXDefault]
  [PXUIField(DisplayName = "Term End Date")]
  public DateTime? DRTermEndDate
  {
    get => this._DRTermEndDate;
    set => this._DRTermEndDate = value;
  }

  public class PK : PrimaryKeyOf<POLineR>.By<POLineR.orderType, POLineR.orderNbr, POLineR.lineNbr>
  {
    public static POLineR Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POLineR) PrimaryKeyOf<POLineR>.By<POLineR.orderType, POLineR.orderNbr, POLineR.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class OrderR : 
      PrimaryKeyOf<POOrderEntry.POOrderR>.By<POOrderEntry.POOrderR.orderType, POOrderEntry.POOrderR.orderNbr>.ForeignKeyOf<POLineR>.By<POLineR.orderType, POLineR.orderNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POLineR>.By<POLineR.inventoryID>
    {
    }

    public class BlanketOrder : 
      PrimaryKeyOf<POOrderEntry.POOrderR>.By<POOrderEntry.POOrderR.orderType, POOrderEntry.POOrderR.orderNbr>.ForeignKeyOf<POLineR>.By<POLineR.pOType, POLineR.pONbr>
    {
    }

    public class BlanketOrderLine : 
      PrimaryKeyOf<POLineR>.By<POLineR.orderType, POLineR.orderNbr, POLineR.lineNbr>.ForeignKeyOf<POLineR>.By<POLineR.pOType, POLineR.pONbr, POLineR.pOLineNbr>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineR.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineR.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineR.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineR.sortOrder>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineR.lineType>
  {
  }

  public abstract class orderedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.orderedQty>
  {
  }

  public abstract class baseOrderedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.baseOrderedQty>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineR.baseReceivedQty>
  {
  }

  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.billedQty>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLineR.curyInfoID>
  {
  }

  public abstract class curyBLOrderedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineR.curyBLOrderedCost>
  {
  }

  public abstract class bLOrderedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.bLOrderedCost>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineR.uOM>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineR.completed>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineR.cancelled>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineR.closed>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.orderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.baseOrderQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.baseOpenQty>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.extCost>
  {
  }

  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.billedAmt>
  {
  }

  public abstract class retainageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineR.retainageAmt>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineR.inventoryID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineR.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineR.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineR.pOLineNbr>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLineR.Tstamp>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLineR.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLineR.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLineR.lastModifiedDateTime>
  {
  }

  public abstract class allowComplete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineR.allowComplete>
  {
  }

  public abstract class completePOLine : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineR.completePOLine>
  {
  }

  public abstract class rcptQtyThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineR.rcptQtyThreshold>
  {
  }

  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLineR.dRTermStartDate>
  {
  }

  public abstract class dRTermEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLineR.dRTermEndDate>
  {
  }
}
