// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranByAcctEnqResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class InventoryTranByAcctEnqResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _GridLineNbr;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _TranType;
  protected string _DocRefNbr;
  protected string _ReceiptNbr;
  protected int? _InventoryID;
  protected string _SubItemCD;
  protected int? _SiteID;
  protected int? _LocationID;
  protected bool? _CostAdj;
  protected DateTime? _TranDate;
  protected string _FinPerNbr;
  protected string _TranPerNbr;
  protected Decimal? _Qty;
  protected Decimal? _BegBalance;
  protected Decimal? _Debit;
  protected Decimal? _Credit;
  protected Decimal? _EndBalance;
  protected Decimal? _UnitCost;
  protected DateTime? _CreatedDateTime;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? GridLineNbr
  {
    get => this._GridLineNbr;
    set => this._GridLineNbr = value;
  }

  [Account]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXString(3)]
  [INTranType.List]
  [PXUIField]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXString(1, IsFixed = true)]
  public virtual string DocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<Current<InventoryTranByAcctEnqResult.docType>>>>))]
  public virtual string DocRefNbr
  {
    get => this._DocRefNbr;
    set => this._DocRefNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Receipt Nbr.", Visible = false)]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [Inventory]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (InventoryTranByAcctEnqResult.inventoryID), DisplayName = "Cost Subitem")]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  [Site]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (InventoryTranByAcctEnqResult.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cost Adjustment")]
  public virtual bool? CostAdj
  {
    get => this._CostAdj;
    set => this._CostAdj = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public virtual string FinPerNbr
  {
    get => this._FinPerNbr;
    set => this._FinPerNbr = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public virtual string TranPerNbr
  {
    get => this._TranPerNbr;
    set => this._TranPerNbr = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
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
  public virtual Decimal? Debit
  {
    get => this._Debit;
    set => this._Debit = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Credit
  {
    get => this._Credit;
    set => this._Credit = value;
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
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXUIField(DisplayName = "Release Date", Visible = false)]
  [PXDBDate]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  public abstract class gridLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.gridLineNbr>
  {
  }

  public abstract class accountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranByAcctEnqResult.subID>
  {
  }

  public abstract class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.tranType>
  {
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.docType>
  {
  }

  public abstract class docRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.docRefNbr>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.receiptNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.inventoryID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.subItemCD>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranByAcctEnqResult.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.locationID>
  {
  }

  public abstract class costAdj : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryTranByAcctEnqResult.costAdj>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.tranDate>
  {
  }

  public abstract class finPerNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.finPerNbr>
  {
  }

  public abstract class tranPerNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.tranPerNbr>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InventoryTranByAcctEnqResult.qty>
  {
  }

  public abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.begBalance>
  {
  }

  public abstract class debit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.debit>
  {
  }

  public abstract class credit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.credit>
  {
  }

  public abstract class endBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.endBalance>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.unitCost>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranByAcctEnqResult.createdDateTime>
  {
  }
}
