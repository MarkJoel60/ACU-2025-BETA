// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReceiptStatus
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

[PXHidden]
[Serializable]
public class INReceiptStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected long? _ReceiptID;
  protected int? _InventoryID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _LayerType;
  protected string _ValMethod;
  protected string _ReceiptNbr;
  protected string _LotSerialNbr;
  protected DateTime? _ReceiptDate;
  protected Decimal? _OrigQty;
  protected Decimal? _QtyOnHand;
  protected byte[] _tstamp;

  [PXDBLongIdentity(IsKey = true)]
  [PXDefault]
  public virtual long? ReceiptID
  {
    get => this._ReceiptID;
    set => this._ReceiptID = value;
  }

  [StockItem]
  [PXDefault]
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

  [SubAccount(typeof (INReceiptStatus.accountID))]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
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

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXDefault("")]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
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
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<INReceiptStatus>.By<INReceiptStatus.docType, INReceiptStatus.receiptNbr, INReceiptStatus.inventoryID, INReceiptStatus.costSubItemID, INReceiptStatus.costSiteID, INReceiptStatus.lotSerialNbr, INReceiptStatus.accountID, INReceiptStatus.subID>
  {
    public static INReceiptStatus Find(
      PXGraph graph,
      string docType,
      string receiptNbr,
      int? inventoryID,
      int? costSubItemID,
      int? costSiteID,
      string lotSerialNbr,
      int? accountID,
      int? subID,
      PKFindOptions options = 0)
    {
      return (INReceiptStatus) PrimaryKeyOf<INReceiptStatus>.By<INReceiptStatus.docType, INReceiptStatus.receiptNbr, INReceiptStatus.inventoryID, INReceiptStatus.costSubItemID, INReceiptStatus.costSiteID, INReceiptStatus.lotSerialNbr, INReceiptStatus.accountID, INReceiptStatus.subID>.FindBy(graph, (object) docType, (object) receiptNbr, (object) inventoryID, (object) costSubItemID, (object) costSiteID, (object) lotSerialNbr, (object) accountID, (object) subID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INReceiptStatus>.By<INReceiptStatus.inventoryID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INReceiptStatus>.By<INReceiptStatus.costSubItemID>
    {
    }

    public class CostSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INReceiptStatus>.By<INReceiptStatus.costSiteID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<INReceiptStatus>.By<INReceiptStatus.subID>
    {
    }
  }

  public abstract class receiptID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INReceiptStatus.receiptID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReceiptStatus.inventoryID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReceiptStatus.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReceiptStatus.costSiteID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReceiptStatus.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReceiptStatus.subID>
  {
  }

  public abstract class layerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReceiptStatus.layerType>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReceiptStatus.valMethod>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReceiptStatus.docType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReceiptStatus.receiptNbr>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReceiptStatus.lotSerialNbr>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReceiptStatus.receiptDate>
  {
  }

  public abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INReceiptStatus.origQty>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INReceiptStatus.qtyOnHand>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INReceiptStatus.Tstamp>
  {
  }
}
