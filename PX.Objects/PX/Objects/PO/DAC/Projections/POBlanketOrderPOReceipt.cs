// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DAC.Projections.POBlanketOrderPOReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO.DAC.Projections;

/// <exclude />
[PXProjection(typeof (Select5<PX.Objects.PO.POLine, InnerJoin<POReceiptLineSigned, On<POReceiptLineSigned.pOType, Equal<PX.Objects.PO.POLine.orderType>, And<POReceiptLineSigned.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<POReceiptLineSigned.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>, InnerJoin<PX.Objects.PO.POReceipt, On<POReceiptLineSigned.receiptType, Equal<PX.Objects.PO.POReceipt.receiptType>, And<POReceiptLineSigned.receiptNbr, Equal<PX.Objects.PO.POReceipt.receiptNbr>>>>>, Where<PX.Objects.PO.POLine.pOType, IsNotNull>, Aggregate<GroupBy<PX.Objects.PO.POLine.pOType, GroupBy<PX.Objects.PO.POLine.pONbr, GroupBy<PX.Objects.PO.POLine.orderType, GroupBy<PX.Objects.PO.POLine.orderNbr, GroupBy<POReceiptLineSigned.receiptType, GroupBy<POReceiptLineSigned.receiptNbr, Sum<POReceiptLineSigned.signedBaseReceiptQty>>>>>>>>>), Persistent = false)]
[PXCacheName("Purchase Blanket Order to Purchase Receipt Link")]
[Serializable]
public class POBlanketOrderPOReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, InputMask = "", IsKey = true, BqlField = typeof (POReceiptLineSigned.receiptType))]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (POReceiptLineSigned.receiptNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr>), Filterable = true)]
  public virtual string ReceiptNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.orderType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Order Type", Enabled = false, IsReadOnly = true)]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false, IsReadOnly = true)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<POBlanketOrderPOReceipt.orderType>>>>))]
  public virtual string OrderNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.pOType))]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.pONbr))]
  public virtual string PONbr { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDBDate(BqlField = typeof (PX.Objects.PO.POReceipt.receiptDate))]
  [PXUIField]
  public virtual DateTime? ReceiptDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.PO.POReceipt.status))]
  [PXUIField]
  [POReceiptStatus.List]
  public virtual string Status { get; set; }

  [PXDBQuantity(BqlField = typeof (POReceiptLineSigned.signedBaseReceiptQty))]
  [PXUIField(DisplayName = "Received Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  public class PK : 
    PrimaryKeyOf<POBlanketOrderPOReceipt>.By<POBlanketOrderPOReceipt.pOType, POBlanketOrderPOReceipt.pONbr, POBlanketOrderPOReceipt.orderType, POBlanketOrderPOReceipt.orderNbr, POBlanketOrderPOReceipt.receiptType, POBlanketOrderPOReceipt.receiptNbr>
  {
    public static POBlanketOrderPOReceipt Find(
      PXGraph graph,
      string pOType,
      string pONbr,
      string orderType,
      string orderNbr,
      string receiptType,
      string receiptNbr,
      PKFindOptions options = 0)
    {
      return (POBlanketOrderPOReceipt) PrimaryKeyOf<POBlanketOrderPOReceipt>.By<POBlanketOrderPOReceipt.pOType, POBlanketOrderPOReceipt.pONbr, POBlanketOrderPOReceipt.orderType, POBlanketOrderPOReceipt.orderNbr, POBlanketOrderPOReceipt.receiptType, POBlanketOrderPOReceipt.receiptNbr>.FindBy(graph, (object) pOType, (object) pONbr, (object) orderType, (object) orderNbr, (object) receiptType, (object) receiptNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<POBlanketOrderPOReceipt>.By<POBlanketOrderPOReceipt.orderType, POBlanketOrderPOReceipt.orderNbr>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<PX.Objects.PO.POReceipt>.By<PX.Objects.PO.POReceipt.receiptType, PX.Objects.PO.POReceipt.receiptNbr>.ForeignKeyOf<POBlanketOrderPOReceipt>.By<POBlanketOrderPOReceipt.receiptType, POBlanketOrderPOReceipt.receiptNbr>
    {
    }
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POBlanketOrderPOReceipt.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POBlanketOrderPOReceipt.receiptNbr>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POBlanketOrderPOReceipt.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POBlanketOrderPOReceipt.orderNbr>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderPOReceipt.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderPOReceipt.pONbr>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POBlanketOrderPOReceipt.receiptDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderPOReceipt.status>
  {
  }

  public abstract class totalQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POBlanketOrderPOReceipt.totalQty>
  {
  }
}
