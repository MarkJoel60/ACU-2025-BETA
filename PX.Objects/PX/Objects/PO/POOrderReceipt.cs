// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

[Serializable]
public class POOrderReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReceiptType;
  protected string _ReceiptNbr;
  protected string _POType;
  protected string _PONbr;
  protected Guid? _ReceiptNoteID;
  protected Guid? _OrderNoteID;
  protected byte[] _tstamp;

  [PXDBString(2, IsFixed = true, InputMask = "", IsKey = true)]
  [PXDBDefault(typeof (POReceipt.receiptType))]
  [POReceiptType.List]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (POReceipt.receiptNbr))]
  [PXDefault]
  [PXParent(typeof (POOrderReceipt.FK.Receipt))]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [POOrderType.List]
  [PXUIField]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PX.Objects.PO.PO.RefNbr(typeof (Search<POOrder.orderNbr>), Filterable = true)]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBGuid(false, IsImmutable = true)]
  [PXDefault(typeof (POReceipt.noteID))]
  public virtual Guid? ReceiptNoteID
  {
    get => this._ReceiptNoteID;
    set => this._ReceiptNoteID = value;
  }

  [CopiedNoteID(typeof (POOrder))]
  public virtual Guid? OrderNoteID
  {
    get => this._OrderNoteID;
    set => this._OrderNoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<POOrderReceipt>.By<POOrderReceipt.receiptType, POOrderReceipt.receiptNbr, POOrderReceipt.pOType, POOrderReceipt.pONbr>
  {
    public static POOrderReceipt Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      string pOType,
      string pONbr,
      PKFindOptions options = 0)
    {
      return (POOrderReceipt) PrimaryKeyOf<POOrderReceipt>.By<POOrderReceipt.receiptType, POOrderReceipt.receiptNbr, POOrderReceipt.pOType, POOrderReceipt.pONbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) pOType, (object) pONbr, options);
    }
  }

  public static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POOrderReceipt>.By<POOrderReceipt.receiptType, POOrderReceipt.receiptNbr>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POOrderReceipt>.By<POOrderReceipt.pOType, POOrderReceipt.pONbr>
    {
    }
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceipt.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceipt.receiptNbr>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceipt.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderReceipt.pONbr>
  {
  }

  public abstract class receiptNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POOrderReceipt.receiptNoteID>
  {
  }

  public abstract class orderNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POOrderReceipt.orderNoteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POOrderReceipt.Tstamp>
  {
  }
}
