// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesPrice.Detail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Extensions.SalesPrice;

/// <summary>A mapped cache extension that represents a detail line of the document.</summary>
public class Detail : PXMappedCacheExtension
{
  /// <exclude />
  protected int? _BranchID;
  /// <exclude />
  protected int? _InventoryID;
  /// <exclude />
  protected int? _SiteID;
  /// <exclude />
  protected 
  #nullable disable
  string _UOM;
  /// <exclude />
  protected Decimal? _Quantity;
  /// <exclude />
  protected Decimal? _CuryUnitPrice;
  /// <exclude />
  protected Decimal? _CuryLineAmount;
  /// <exclude />
  protected bool? _IsFree;
  /// <exclude />
  protected bool? _ManualPrice;

  /// <summary>The identifier of the branch associated with the detail line.</summary>
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The identifier of the inventory item.</summary>
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The description of the inventory item.</summary>
  public virtual string Descr { get; set; }

  /// <summary>The identifier of the Warehouse of the item.</summary>
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  /// <summary>The unit of measure for the inventory item.</summary>
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>The quantity of the inventory item.</summary>
  public virtual Decimal? Quantity
  {
    get => this._Quantity;
    set => this._Quantity = value;
  }

  /// <summary>The price per unit of the line item, in the currency of the document.</summary>
  public virtual Decimal? CuryUnitPrice
  {
    get => this._CuryUnitPrice;
    set => this._CuryUnitPrice = value;
  }

  /// <summary>The total amount for the line item, in the currency of the document.</summary>
  public virtual Decimal? CuryLineAmount
  {
    get => this._CuryLineAmount;
    set => this._CuryLineAmount = value;
  }

  /// <summary>A property that indicates (if set to <tt>true</tt>) that the inventory item specified in the row is a free item.</summary>
  public virtual bool? IsFree
  {
    get => this._IsFree;
    set => this._IsFree = value;
  }

  /// <summary>A property that indicates (if set to <tt>true</tt>) that the price of this inventory item was changed manually.</summary>
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  /// <summary>
  /// Indicates (if selected) that the automatic line discounts are not applied to this line.
  /// </summary>
  public virtual bool? SkipLineDiscounts { get; set; }

  /// <exclude />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.branchID>
  {
  }

  /// <exclude />
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.inventoryID>
  {
  }

  /// <exclude />
  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.descr>
  {
  }

  /// <exclude />
  public abstract class siteID : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.uOM>
  {
  }

  /// <exclude />
  public abstract class quantity : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.quantity>
  {
  }

  /// <exclude />
  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.curyUnitPrice>
  {
  }

  /// <exclude />
  public abstract class curyLineAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.curyLineAmount>
  {
  }

  /// <exclude />
  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Detail.isFree>
  {
  }

  /// <exclude />
  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Detail.manualPrice>
  {
  }

  public abstract class skipLineDiscounts : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Detail.skipLineDiscounts>
  {
  }
}
