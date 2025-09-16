// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCostStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Cost Status")]
[Serializable]
public class INCostStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected long? _CostID;
  protected int? _InventoryID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _SiteID;
  protected 
  #nullable disable
  string _LayerType;
  protected string _ValMethod;
  protected string _ReceiptNbr;
  protected DateTime? _ReceiptDate;
  protected string _LotSerialNbr;
  protected Decimal? _OrigQty;
  protected Decimal? _QtyOnHand;
  protected Decimal? _UnitCost;
  protected Decimal? _TotalCost;
  protected Decimal? _AvgCost;
  protected byte[] _tstamp;

  [PXDBLongIdentity(IsKey = true)]
  [PXDefault]
  public virtual long? CostID
  {
    get => this._CostID;
    set => this._CostID = value;
  }

  [StockItem]
  [PXDefault]
  [PXForeignReference(typeof (INCostStatus.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem]
  [PXDefault]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Cost Site")]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [Account]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [Site(true)]
  [PXDefault(typeof (Coalesce<Search<INSite.siteID, Where<INSite.siteID, Equal<Current<INCostStatus.costSiteID>>>>, Search<INLocation.siteID, Where<INLocation.locationID, Equal<Current<INCostStatus.costSiteID>>>>>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string LayerType
  {
    get => this._LayerType;
    set => this._LayerType = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  [PXDefault]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Receipt Date")]
  [PXDefault]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Lot/Serial Number")]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigQty
  {
    get => this._OrigQty;
    set => this._OrigQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  /// <summary>
  /// The unbound field is used only during inventory documents release for storing the original on hand qty before processing of oversolds.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigQtyOnHand { get; set; }

  /// <summary>
  /// The unbound field is used only during inventory documents release for indicating that <see cref="P:PX.Objects.IN.INCostStatus.OrigQty" /> should be overriden.
  /// Used for PO Corrections scenarios when there are both Issue and Receipt transactions and we need to separate Receipts only.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public bool? OverrideOrigQty { get; set; }

  /// <summary>
  /// The unbound field is used only during inventory documents release for storing the summ quantity of positive transactions.
  /// Used for PO Corrections scenarios when there are both Issue and Receipt transactions and we need to separate Receipts only.
  /// </summary>
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? PositiveTranQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Cost")]
  public virtual Decimal? TotalCost
  {
    get => this._TotalCost;
    set => this._TotalCost = value;
  }

  public virtual Decimal? AvgCost
  {
    get => this._AvgCost;
    set => this._AvgCost = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INCostStatus>.By<INCostStatus.inventoryID, INCostStatus.costSubItemID, INCostStatus.costSiteID, INCostStatus.accountID, INCostStatus.subID, INCostStatus.layerType, INCostStatus.costID>
  {
    public static INCostStatus Find(
      PXGraph graph,
      int? inventoryID,
      int? costSubItemID,
      int? costSiteID,
      int? accountID,
      int? subID,
      string layerType,
      long? costID,
      PKFindOptions options = 0)
    {
      return (INCostStatus) PrimaryKeyOf<INCostStatus>.By<INCostStatus.inventoryID, INCostStatus.costSubItemID, INCostStatus.costSiteID, INCostStatus.accountID, INCostStatus.subID, INCostStatus.layerType, INCostStatus.costID>.FindBy(graph, (object) inventoryID, (object) costSubItemID, (object) costSiteID, (object) accountID, (object) subID, (object) layerType, (object) costID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INCostStatus>.By<INCostStatus.inventoryID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INCostStatus>.By<INCostStatus.costSubItemID>
    {
    }

    public class CostSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INCostStatus>.By<INCostStatus.costSiteID>
    {
    }

    public class CostItemSite : 
      PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.ForeignKeyOf<INCostStatus>.By<INCostStatus.inventoryID, INCostStatus.costSiteID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INCostStatus>.By<INCostStatus.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<INCostStatus>.By<INCostStatus.subID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INCostStatus>.By<INCostStatus.siteID>
    {
    }
  }

  public abstract class costID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INCostStatus.costID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatus.inventoryID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatus.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatus.costSiteID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatus.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatus.subID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostStatus.siteID>
  {
  }

  public abstract class layerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostStatus.layerType>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostStatus.valMethod>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostStatus.receiptNbr>
  {
  }

  public abstract class receiptDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INCostStatus.receiptDate>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostStatus.lotSerialNbr>
  {
  }

  public abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INCostStatus.origQty>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INCostStatus.qtyOnHand>
  {
  }

  public abstract class origQtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCostStatus.origQtyOnHand>
  {
  }

  public abstract class overrideOrigQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INCostStatus.overrideOrigQty>
  {
  }

  public abstract class positiveTranQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INCostStatus.positiveTranQty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INCostStatus.unitCost>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INCostStatus.totalCost>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INCostStatus.Tstamp>
  {
  }
}
