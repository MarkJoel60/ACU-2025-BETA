// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemLotSerial
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Lot/Serial by Item")]
[Serializable]
public class INItemLotSerial : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _LotSerialNbr;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyHardAvail;
  protected Decimal? _QtyActual;
  protected Decimal? _QtyInTransit;
  protected bool? _Preassigned;
  protected DateTime? _ExpireDate;
  protected bool? _UpdateExpireDate;
  protected string _LotSerTrack;
  protected string _LotSerAssign;
  protected byte[] _tstamp;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [StockItem(IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault]
  [PX.Objects.IN.LotSerialNbr(IsKey = true)]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Hard Available")]
  public virtual Decimal? QtyHardAvail
  {
    get => this._QtyHardAvail;
    set => this._QtyHardAvail = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available for Issue")]
  public virtual Decimal? QtyActual
  {
    get => this._QtyActual;
    set => this._QtyActual = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  /// <summary>
  /// The field is used only for When Used Serial Numbers. It stores the quantity of the first IN Document
  /// released the serial number (<c>-1</c>, <c>1</c>) or <c>null</c> if it was not released yet.
  /// </summary>
  [PXDBQuantity]
  public virtual Decimal? QtyOrig { get; set; }

  /// <summary>
  /// The field is used to store receipts Qty separately from issues.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnReceipt { get; set; }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2022R2.")]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Pre-Assigned", Enabled = false)]
  public virtual bool? Preassigned
  {
    get => this._Preassigned;
    set => this._Preassigned = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Expiry Date")]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXBool]
  public virtual bool? UpdateExpireDate
  {
    get => this._UpdateExpireDate;
    set => this._UpdateExpireDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string LotSerTrack
  {
    get => this._LotSerTrack;
    set => this._LotSerTrack = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public virtual string LotSerAssign
  {
    get => this._LotSerAssign;
    set => this._LotSerAssign = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public class PK : 
    PrimaryKeyOf<INItemLotSerial>.By<INItemLotSerial.inventoryID, INItemLotSerial.lotSerialNbr>
  {
    public static INItemLotSerial Find(
      PXGraph graph,
      int? inventoryID,
      string lotSerialNbr,
      PKFindOptions options = 0)
    {
      return (INItemLotSerial) PrimaryKeyOf<INItemLotSerial>.By<INItemLotSerial.inventoryID, INItemLotSerial.lotSerialNbr>.FindBy(graph, (object) inventoryID, (object) lotSerialNbr, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemLotSerial>.By<INItemLotSerial.inventoryID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemLotSerial.inventoryID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerial.lotSerialNbr>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemLotSerial.qtyOnHand>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemLotSerial.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerial.qtyHardAvail>
  {
  }

  public abstract class qtyActual : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemLotSerial.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerial.qtyInTransit>
  {
  }

  public abstract class qtyOrig : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemLotSerial.qtyOrig>
  {
  }

  public abstract class qtyOnReceipt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemLotSerial.qtyOnReceipt>
  {
  }

  [Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2022R2.")]
  public abstract class preassigned : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemLotSerial.preassigned>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemLotSerial.expireDate>
  {
  }

  public abstract class lotSerTrack : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemLotSerial.lotSerTrack>
  {
  }

  public abstract class lotSerAssign : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerial.lotSerAssign>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemLotSerial.Tstamp>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemLotSerial.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemLotSerial.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemLotSerial.noteID>
  {
  }
}
