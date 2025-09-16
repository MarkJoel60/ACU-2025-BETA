// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranHistEnqResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class InventoryTranHistEnqResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected Decimal? _BegQty;
  protected Decimal? _QtyIn;
  protected Decimal? _QtyOut;
  protected Decimal? _EndQty;
  protected Decimal? _UnitCost;
  protected Decimal? _ExtCost;
  protected Decimal? _BegBalance;
  protected Decimal? _EndBalance;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? GridLineNbr { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? TranDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<Current<InventoryTranHistEnqResult.docType>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  public virtual int? LineNbr { get; set; }

  [PXDBInt]
  public virtual int? SplitLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PX.Objects.PO.POReceiptType.List]
  [PXUIField]
  public virtual string POReceiptType { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BegQty
  {
    get => this._BegQty;
    set => this._BegQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyIn
  {
    get => this._QtyIn;
    set => this._QtyIn = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyOut
  {
    get => this._QtyOut;
    set => this._QtyOut = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? EndQty
  {
    get => this._EndQty;
    set => this._EndQty = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExtCost
  {
    get => this._ExtCost;
    set => this._ExtCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BegBalance
  {
    get => this._BegBalance;
    set => this._BegBalance = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? EndBalance
  {
    get => this._EndBalance;
    set => this._EndBalance = value;
  }

  public abstract class gridLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranHistEnqResult.gridLineNbr>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranHistEnqResult.tranDate>
  {
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranHistEnqResult.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryTranHistEnqResult.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranHistEnqResult.lineNbr>
  {
  }

  public abstract class splitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranHistEnqResult.splitLineNbr>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranHistEnqResult.pOReceiptType>
  {
  }

  public abstract class begQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranHistEnqResult.begQty>
  {
  }

  public abstract class qtyIn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryTranHistEnqResult.qtyIn>
  {
  }

  public abstract class qtyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranHistEnqResult.qtyOut>
  {
  }

  public abstract class endQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranHistEnqResult.endQty>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranHistEnqResult.unitCost>
  {
  }

  public abstract class extCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranHistEnqResult.extCost>
  {
  }

  public abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranHistEnqResult.begBalance>
  {
  }

  public abstract class endBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranHistEnqResult.endBalance>
  {
  }
}
