// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DAC.Projections.POBlanketOrderPOOrder
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
namespace PX.Objects.PO.DAC.Projections;

/// <exclude />
[PXProjection(typeof (Select5<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>>, Where<PX.Objects.PO.POLine.pOType, IsNotNull>, Aggregate<GroupBy<PX.Objects.PO.POLine.pOType, GroupBy<PX.Objects.PO.POLine.pONbr, GroupBy<PX.Objects.PO.POLine.orderType, GroupBy<PX.Objects.PO.POLine.orderNbr, Sum<PX.Objects.PO.POLine.baseOrderQty>>>>>>>), Persistent = false)]
[PXCacheName("Purchase Blanket Order to Purchase Order Link")]
[Serializable]
public class POBlanketOrderPOOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Date of the document.</summary>
  [PXDBDate(BqlField = typeof (PX.Objects.PO.POOrder.orderDate))]
  [PXUIField]
  public virtual DateTime? OrderDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.PO.POOrder.status))]
  [PXUIField]
  [POOrderStatus.List]
  public virtual 
  #nullable disable
  string Status { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.baseOrderQty))]
  [PXUIField(DisplayName = "Ordered Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.orderType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Order Type", Enabled = false, IsReadOnly = true, Visible = false)]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false, IsReadOnly = true)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<POBlanketOrderPOOrder.orderType>>>>))]
  public virtual string OrderNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.pOType))]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.pONbr))]
  public virtual string PONbr { get; set; }

  [ProjectionNote(typeof (PX.Objects.PO.POOrder), BqlField = typeof (PX.Objects.PO.POOrder.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXString]
  public virtual string StatusText { get; set; }

  public class PK : 
    PrimaryKeyOf<POBlanketOrderPOOrder>.By<POBlanketOrderPOOrder.pOType, POBlanketOrderPOOrder.pONbr, POBlanketOrderPOOrder.orderType, POBlanketOrderPOOrder.orderNbr>
  {
    public static POBlanketOrderPOOrder Find(
      PXGraph graph,
      string pOType,
      string pONbr,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (POBlanketOrderPOOrder) PrimaryKeyOf<POBlanketOrderPOOrder>.By<POBlanketOrderPOOrder.pOType, POBlanketOrderPOOrder.pONbr, POBlanketOrderPOOrder.orderType, POBlanketOrderPOOrder.orderNbr>.FindBy(graph, (object) pOType, (object) pONbr, (object) orderType, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<POBlanketOrderPOOrder>.By<POBlanketOrderPOOrder.orderType, POBlanketOrderPOOrder.orderNbr>
    {
    }
  }

  public abstract class orderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POBlanketOrderPOOrder.orderDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderPOOrder.status>
  {
  }

  public abstract class totalQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POBlanketOrderPOOrder.totalQty>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POBlanketOrderPOOrder.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderPOOrder.orderNbr>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderPOOrder.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderPOOrder.pONbr>
  {
  }

  public abstract class noteID : IBqlField, IBqlOperand
  {
  }

  public abstract class statusText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POBlanketOrderPOOrder.statusText>
  {
  }
}
