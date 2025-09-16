// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranDetEnqResult
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
public class InventoryTranDetEnqResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Decimal? _BegQty;
  protected Decimal? _QtyIn;
  protected Decimal? _QtyOut;
  protected Decimal? _EndQty;
  protected Decimal? _BegBalance;
  protected Decimal? _ExtCostIn;
  protected Decimal? _ExtCostOut;
  protected Decimal? _EndBalance;
  protected Decimal? _UnitCost;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? GridLineNbr { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? TranDate { get; set; }

  [PXString(3)]
  [INTranType.List]
  [PXUIField]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  public virtual string DocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<Current<InventoryTranDetEnqResult.docType>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PX.Objects.PO.POReceiptType.List]
  [PXUIField]
  public virtual string POReceiptType { get; set; }

  [SubItem]
  public virtual int? SubItemID { get; set; }

  [Site]
  public virtual int? SiteID { get; set; }

  [Location(typeof (InventoryTranDetEnqResult.siteID))]
  public virtual int? LocationID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string LotSerialNbr { get; set; }

  [PXBool]
  [PXUIField]
  public virtual bool? Released { get; set; }

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
  public virtual Decimal? ExtCostIn
  {
    get => this._ExtCostIn;
    set => this._ExtCostIn = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExtCostOut
  {
    get => this._ExtCostOut;
    set => this._ExtCostOut = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? EndBalance
  {
    get => this._EndBalance;
    set => this._EndBalance = value;
  }

  [PXDBPriceCost]
  [PXUIField]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  public abstract class gridLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranDetEnqResult.gridLineNbr>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranDetEnqResult.tranDate>
  {
  }

  public abstract class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqResult.tranType>
  {
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqResult.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryTranDetEnqResult.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranDetEnqResult.lineNbr>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqResult.pOReceiptType>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranDetEnqResult.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranDetEnqResult.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranDetEnqResult.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranDetEnqResult.lotSerialNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryTranDetEnqResult.released>
  {
  }

  public abstract class begQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.begQty>
  {
  }

  public abstract class qtyIn : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryTranDetEnqResult.qtyIn>
  {
  }

  public abstract class qtyOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.qtyOut>
  {
  }

  public abstract class endQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.endQty>
  {
  }

  public abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.begBalance>
  {
  }

  public abstract class extCostIn : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.extCostIn>
  {
  }

  public abstract class extCostOut : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.extCostOut>
  {
  }

  public abstract class endBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.endBalance>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranDetEnqResult.unitCost>
  {
  }
}
