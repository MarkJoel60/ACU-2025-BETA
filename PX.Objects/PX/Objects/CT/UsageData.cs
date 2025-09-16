// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.UsageData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CT;

/// <exclude />
[PXCacheName("Contract Usage")]
[PXHidden]
[Serializable]
public class UsageData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _InventoryCD;
  protected string _Description;
  protected string _UOM;
  protected int? _EmployeeID;
  protected string _Prefix;
  protected Decimal? _Qty;
  protected Decimal? _PreciseQty;
  protected Decimal? _Proportion;
  protected Decimal? _ExtPrice;
  protected bool? _IsFree;
  protected bool? _IsTranData;
  protected Decimal? _PriceOverride;
  protected int? _RefLineNbr;
  protected List<long?> _TranIDs;
  protected DateTime? _TranDate;

  [PXDBInt]
  [PXDefault]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString]
  [PXDefault]
  [PXUIField(DisplayName = "Item Code")]
  public virtual string InventoryCD
  {
    get => this._InventoryCD;
    set => this._InventoryCD = value;
  }

  [PXDBString]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString]
  [PXDefault]
  [PXUIField(DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBInt]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString]
  [PXDefault]
  [PXUIField(DisplayName = "Billing Type")]
  public virtual string Prefix
  {
    get => this._Prefix;
    set => this._Prefix = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Included Qty")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBQuantity]
  public virtual Decimal? PreciseQty
  {
    get
    {
      this._PreciseQty = this._PreciseQty ?? this._Qty;
      return this._PreciseQty;
    }
    set => this._PreciseQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Proportion
  {
    get => this._Proportion;
    set => this._Proportion = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPrice
  {
    get => this._ExtPrice;
    set => this._ExtPrice = value;
  }

  [PXDBBool]
  [PXDefault]
  public virtual bool? IsFree
  {
    get => this._IsFree;
    set => this._IsFree = value;
  }

  [PXDBBool]
  [PXDefault]
  public virtual bool? IsTranData
  {
    get => this._IsTranData;
    set => this._IsTranData = value;
  }

  [PXDBBool]
  [PXDefault]
  public virtual bool? IsDollarUsage { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PriceOverride
  {
    get => this._PriceOverride;
    set => this._PriceOverride = value;
  }

  /// <summary>
  /// Is used to sort the <see cref="T:PX.Objects.CT.UsageData" /> objects in the same way as the <see cref="T:PX.Objects.CT.ContractDetail" /> objects are sorted in the contract.
  /// The sorting is required to create an <see cref="T:PX.Objects.AR.ARInvoice">AR invoice</see> with the same line order as the contract has.
  /// If there are lines that are not related to <see cref="T:PX.Objects.CT.ContractDetail" /> to be added to <see cref="T:PX.Objects.AR.ARInvoice" />,
  /// these lines will have this field filled in with <see cref="F:System.Int32.MaxValue" />. (These lines should be put after the lines related to the contract.)
  /// </summary>
  [PXInt]
  public virtual int? ContractDetailsLineNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? RefLineNbr
  {
    get => this._RefLineNbr;
    set => this._RefLineNbr = value;
  }

  public virtual List<long?> TranIDs
  {
    get => this._TranIDs;
    set => this._TranIDs = value;
  }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string DiscountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string DiscountSeq { get; set; }

  [PXDBString(15)]
  public virtual string CaseCD { get; set; }

  public virtual string ActionItem { get; set; }

  [PXInt]
  public virtual int? ContractItemID { get; set; }

  [PXInt]
  public virtual int? ContractDetailID { get; set; }

  public UsageData() => this._TranIDs = new List<long?>();

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UsageData.branchID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UsageData.inventoryID>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.inventoryCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.uOM>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UsageData.employeeID>
  {
  }

  public abstract class prefix : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.prefix>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UsageData.qty>
  {
  }

  public abstract class preciseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UsageData.preciseQty>
  {
  }

  public abstract class proportion : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UsageData.proportion>
  {
  }

  public abstract class extPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UsageData.extPrice>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UsageData.isFree>
  {
  }

  public abstract class isTranData : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UsageData.isTranData>
  {
  }

  public abstract class isDollarUsage : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UsageData.isDollarUsage>
  {
  }

  public abstract class priceOverride : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UsageData.priceOverride>
  {
  }

  public abstract class contractDetailsLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UsageData.contractDetailsLineNbr>
  {
  }

  public abstract class refLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UsageData.refLineNbr>
  {
  }

  public abstract class tranIDs : IBqlField, IBqlOperand
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  UsageData.tranDate>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.discountID>
  {
  }

  public abstract class discountSeq : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.discountSeq>
  {
  }

  public abstract class caseCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.caseCD>
  {
  }

  public abstract class actionItem : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UsageData.actionItem>
  {
  }

  public abstract class contractItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UsageData.contractItemID>
  {
  }

  public abstract class contractDetailID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UsageData.contractDetailID>
  {
  }
}
