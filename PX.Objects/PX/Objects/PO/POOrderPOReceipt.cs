// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderPOReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select5<POReceipt, InnerJoin<POOrderReceipt, On<POOrderReceipt.FK.Receipt>, LeftJoin<POReceiptLineSigned, On<POReceiptLineSigned.receiptType, Equal<POReceipt.receiptType>, And<POReceiptLineSigned.receiptNbr, Equal<POReceipt.receiptNbr>, And<POReceiptLineSigned.pOType, Equal<POOrderReceipt.pOType>, And<POReceiptLineSigned.pONbr, Equal<POOrderReceipt.pONbr>>>>>>>, Aggregate<GroupBy<POOrderReceipt.receiptType, GroupBy<POOrderReceipt.receiptNbr, GroupBy<POOrderReceipt.pOType, GroupBy<POOrderReceipt.pONbr, Sum<POReceiptLineSigned.signedBaseReceiptQty>>>>>>>), Persistent = false)]
[PXCacheName("Purchase Order to Purchase Receipt Link")]
[Serializable]
public class POOrderPOReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReceiptNbr;

  [PXDBString(2, IsFixed = true, InputMask = "", IsKey = true, BqlField = typeof (POOrderReceipt.receiptType))]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string ReceiptType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (POOrderReceipt.receiptNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Optional<POOrderPOReceipt.receiptType>>>>), Filterable = true)]
  public virtual string ReceiptNbr { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDBDate(BqlField = typeof (POReceipt.receiptDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POReceipt.status))]
  [PXUIField]
  [POReceiptStatus.List]
  public virtual string Status { get; set; }

  [PXDBQuantity(BqlField = typeof (POReceiptLineSigned.signedBaseReceiptQty))]
  [PXUIField(DisplayName = "Received Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POOrderReceipt.pOType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "PO Type", Enabled = false, IsReadOnly = true)]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (POOrderReceipt.pONbr))]
  [PXUIField(DisplayName = "PO Number", Enabled = false, IsReadOnly = true)]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Optional<POOrderPOReceipt.pOType>>>>))]
  public virtual string PONbr { get; set; }

  [PXString]
  public virtual string StatusText { get; set; }

  [ProjectionNote(typeof (POReceipt), BqlField = typeof (POReceipt.noteID))]
  public virtual Guid? NoteID { get; set; }

  public class PK : 
    PrimaryKeyOf<POOrderPOReceipt>.By<POOrderPOReceipt.receiptType, POOrderPOReceipt.receiptNbr, POOrderPOReceipt.pOType, POOrderPOReceipt.pONbr>
  {
    public static POOrderPOReceipt Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      string pOType,
      string pONbr,
      PKFindOptions options = 0)
    {
      return (POOrderPOReceipt) PrimaryKeyOf<POOrderPOReceipt>.By<POOrderPOReceipt.receiptType, POOrderPOReceipt.receiptNbr, POOrderPOReceipt.pOType, POOrderPOReceipt.pONbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) pOType, (object) pONbr, options);
    }
  }

  public static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POOrderPOReceipt>.By<POOrderPOReceipt.receiptType, POOrderPOReceipt.receiptNbr>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POOrderPOReceipt>.By<POOrderPOReceipt.pOType, POOrderPOReceipt.pONbr>
    {
    }
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPOReceipt.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPOReceipt.receiptNbr>
  {
    public const string DisplayName = "Receipt Nbr.";
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POOrderPOReceipt.docDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPOReceipt.status>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrderPOReceipt.totalQty>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPOReceipt.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPOReceipt.pONbr>
  {
  }

  public abstract class statusText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderPOReceipt.statusText>
  {
  }

  public abstract class noteID : IBqlField, IBqlOperand
  {
  }
}
