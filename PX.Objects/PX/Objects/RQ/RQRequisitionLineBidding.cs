// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionLineBidding
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXProjection(typeof (Select<RQRequisitionLine>), Persistent = false)]
[Serializable]
public class RQRequisitionLineBidding : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReqNbr;
  protected int? _LineNbr;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected string _Description;
  protected string _AlternateID;
  protected long? _CuryInfoID;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected string _QuoteNumber;
  protected Decimal? _QuoteQty;
  protected Decimal? _CuryQuoteUnitCost;
  protected Decimal? _QuoteUnitCost;
  protected Decimal? _CuryQuoteExtCost;
  protected Decimal? _QuoteExtCost;
  protected Decimal? _MinQty;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (RQRequisitionLine.reqNbr))]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (RQRequisitionLine.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [RQRequisitionInventoryItem(Filterable = true, BqlField = typeof (RQRequisitionLine.inventoryID), Enabled = false)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(BqlField = typeof (RQRequisitionLine.subItemID), Enabled = false)]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (RQRequisitionLine.description))]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(BqlField = typeof (RQRequisitionLine.lineNbr), InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Alternate ID", Enabled = false)]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBLong(BqlField = typeof (RQRequisitionLine.curyInfoID))]
  [PXDBDefault(typeof (RQRequisition.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [INUnit(typeof (RQRequisitionLineBidding.inventoryID), DisplayName = "UOM", BqlField = typeof (RQRequisitionLine.uOM), Enabled = false)]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (RQRequisitionLineBidding.uOM), typeof (RQRequisitionLineBidding.baseOrderQty), HandleEmptyKey = true, BqlField = typeof (RQRequisitionLine.orderQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (RQRequisitionLine.baseOrderQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Bid Number")]
  public virtual string QuoteNumber
  {
    get => this._QuoteNumber;
    set => this._QuoteNumber = value;
  }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QuoteQty
  {
    get => this._QuoteQty;
    set => this._QuoteQty = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (RQRequisitionLineBidding.curyInfoID), typeof (RQRequisitionLineBidding.quoteUnitCost))]
  [PXUIField]
  public virtual Decimal? CuryQuoteUnitCost
  {
    get => this._CuryQuoteUnitCost;
    set => this._CuryQuoteUnitCost = value;
  }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QuoteUnitCost
  {
    get => this._QuoteUnitCost;
    set => this._QuoteUnitCost = value;
  }

  [PXCurrency(typeof (RQRequisitionLineBidding.curyInfoID), typeof (RQRequisitionLineBidding.quoteExtCost))]
  [PXUIField]
  [PXFormula(typeof (Mult<RQRequisitionLineBidding.quoteQty, RQRequisitionLineBidding.curyQuoteUnitCost>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryQuoteExtCost
  {
    get => this._CuryQuoteExtCost;
    set => this._CuryQuoteExtCost = value;
  }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QuoteExtCost
  {
    get => this._QuoteExtCost;
    set => this._QuoteExtCost = value;
  }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLineBidding.reqNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLineBidding.lineNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequisitionLineBidding.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequisitionLineBidding.subItemID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLineBidding.description>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLineBidding.alternateID>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    RQRequisitionLineBidding.curyInfoID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequisitionLineBidding.uOM>
  {
  }

  public abstract class orderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineBidding.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineBidding.baseOrderQty>
  {
  }

  public abstract class quoteNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequisitionLineBidding.quoteNumber>
  {
  }

  public abstract class quoteQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineBidding.quoteQty>
  {
  }

  public abstract class curyQuoteUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineBidding.curyQuoteUnitCost>
  {
  }

  public abstract class quoteUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineBidding.quoteUnitCost>
  {
  }

  public abstract class curyQuoteExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineBidding.curyQuoteExtCost>
  {
  }

  public abstract class quoteExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequisitionLineBidding.quoteExtCost>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequisitionLineBidding.minQty>
  {
  }
}
