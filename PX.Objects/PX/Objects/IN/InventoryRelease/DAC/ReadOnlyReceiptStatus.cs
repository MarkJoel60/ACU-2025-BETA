// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.DAC.ReadOnlyReceiptStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.DAC;

[PXHidden]
public class ReadOnlyReceiptStatus : INReceiptStatus
{
  [PXDBLong(IsKey = true)]
  [PXDefault]
  public override long? ReceiptID
  {
    get => this._ReceiptID;
    set => this._ReceiptID = value;
  }

  [StockItem(IsKey = true)]
  [PXDefault]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [Account(IsKey = true)]
  [PXDefault]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (ReadOnlyReceiptStatus.accountID), IsKey = true)]
  [PXDefault]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public override string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  public new abstract class receiptID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.receiptID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.inventoryID>
  {
  }

  public new abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.costSubItemID>
  {
  }

  public new abstract class costSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.costSiteID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReadOnlyReceiptStatus.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReadOnlyReceiptStatus.subID>
  {
  }

  public new abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.docType>
  {
  }

  public new abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.receiptNbr>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.receiptDate>
  {
  }

  public new abstract class origQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.origQty>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.qtyOnHand>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReadOnlyReceiptStatus.lotSerialNbr>
  {
  }
}
