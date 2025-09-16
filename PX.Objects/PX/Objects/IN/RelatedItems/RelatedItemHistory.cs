// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.RelatedItemHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.RelatedItems;

[PXCacheName]
public class RelatedItemHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? LineID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsDraft { get; set; }

  [Inventory(DisplayName = "Original Item ID", Required = false)]
  [PXDefault]
  [PXForeignReference(typeof (RelatedItemHistory.FK.OriginalInventoryItem))]
  public virtual int? OriginalInventoryID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Original Item Description")]
  [PXFormula(typeof (Selector<RelatedItemHistory.originalInventoryID, PX.Objects.IN.InventoryItem.descr>))]
  public virtual 
  #nullable disable
  string OriginalInventoryDesc { get; set; }

  [INUnit(typeof (RelatedItemHistory.originalInventoryID), DisplayName = "Original Item UOM", Required = false)]
  [PXDefault]
  public virtual string OriginalInventoryUOM { get; set; }

  [PXDBQuantity]
  [PXDefault]
  [PXUIField(DisplayName = "Original Item Qty.")]
  public virtual Decimal? OriginalInventoryQty { get; set; }

  [Inventory(DisplayName = "Related Item ID", Required = false)]
  [PXDefault]
  [PXForeignReference(typeof (RelatedItemHistory.FK.RelatedInventoryItem))]
  public virtual int? RelatedInventoryID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Related Item Description", Enabled = false)]
  [PXFormula(typeof (Selector<RelatedItemHistory.relatedInventoryID, PX.Objects.IN.InventoryItem.descr>))]
  public virtual string RelatedInventoryDesc { get; set; }

  [INUnit(typeof (RelatedItemHistory.relatedInventoryID), DisplayName = "Related Item UOM", Required = false)]
  [PXDefault]
  public virtual string RelatedInventoryUOM { get; set; }

  [PXDBQuantity]
  [PXDefault]
  [PXUIField(DisplayName = "Related Item Qty.")]
  public virtual Decimal? RelatedInventoryQty { get; set; }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Qty. Sold")]
  public virtual Decimal? SoldQty { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Relation")]
  [PXDefault]
  [InventoryRelation.ListAttribute.WithAll]
  public virtual string Relation { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Tag")]
  [PXDefault]
  [InventoryRelationTag.ListAttribute.WithAll]
  public virtual string Tag { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Document Date", Visible = false, Required = false)]
  public virtual DateTime? DocumentDate { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXParent(typeof (RelatedItemHistory.FK.SalesOrder))]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<BqlField<RelatedItemHistory.orderType, IBqlString>.FromCurrent>>>))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  [PXDBInt]
  [PXParent(typeof (RelatedItemHistory.FK.OriginalSalesOrderLine), LeaveChildren = true)]
  public virtual int? OriginalOrderLineNbr { get; set; }

  [PXDBInt]
  [PXParent(typeof (RelatedItemHistory.FK.RelatedSalesOrderLine))]
  public virtual int? RelatedOrderLineNbr { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string InvoiceDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Invoice Nbr.")]
  [PXParent(typeof (RelatedItemHistory.FK.ARInvoice))]
  [PXParent(typeof (RelatedItemHistory.FK.Invoice))]
  [PXSelector(typeof (Search<PX.Objects.SO.SOInvoice.refNbr, Where<PX.Objects.SO.SOInvoice.docType, Equal<BqlField<RelatedItemHistory.invoiceDocType, IBqlString>.FromCurrent>>>))]
  public virtual string InvoiceRefNbr { get; set; }

  [PXDBInt]
  [PXParent(typeof (RelatedItemHistory.FK.OriginalInvoiceLine), LeaveChildren = true)]
  public virtual int? OriginalInvoiceLineNbr { get; set; }

  [PXDBInt]
  [PXParent(typeof (RelatedItemHistory.FK.RelatedInvoiceLine))]
  public virtual int? RelatedInvoiceLineNbr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<RelatedItemHistory>.By<RelatedItemHistory.lineID>
  {
    public static RelatedItemHistory Find(PXGraph graph, int? lineID, PKFindOptions options = 0)
    {
      return (RelatedItemHistory) PrimaryKeyOf<RelatedItemHistory>.By<RelatedItemHistory.lineID>.FindBy(graph, (object) lineID, options);
    }

    public class Dirty : PrimaryKeyOf<RelatedItemHistory>.By<RelatedItemHistory.lineID>.Dirty
    {
      public static RelatedItemHistory Find(PXGraph graph, int? lineID, PKFindOptions options = 0)
      {
        return (RelatedItemHistory) PrimaryKeyOf<RelatedItemHistory>.By<RelatedItemHistory.lineID>.Dirty.FindBy(graph, (object) lineID, options);
      }
    }
  }

  public static class FK
  {
    public class OriginalInventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.originalInventoryID>
    {
    }

    public class RelatedInventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.relatedInventoryID>
    {
    }

    public class SalesOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.orderType, RelatedItemHistory.orderNbr>
    {
    }

    public class OriginalSalesOrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.orderType, RelatedItemHistory.orderNbr, RelatedItemHistory.originalOrderLineNbr>
    {
    }

    public class RelatedSalesOrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.orderType, RelatedItemHistory.orderNbr, RelatedItemHistory.relatedOrderLineNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.invoiceDocType, RelatedItemHistory.invoiceRefNbr>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.invoiceDocType, RelatedItemHistory.invoiceRefNbr>
    {
    }

    public class OriginalInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.invoiceDocType, RelatedItemHistory.invoiceRefNbr, RelatedItemHistory.originalInvoiceLineNbr>
    {
    }

    public class RelatedInvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<RelatedItemHistory>.By<RelatedItemHistory.invoiceDocType, RelatedItemHistory.invoiceRefNbr, RelatedItemHistory.relatedInvoiceLineNbr>
    {
    }
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RelatedItemHistory.lineID>
  {
  }

  public abstract class isDraft : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelatedItemHistory.isDraft>
  {
  }

  public abstract class originalInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.originalInventoryID>
  {
  }

  public abstract class originalInventoryDesc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.originalInventoryDesc>
  {
  }

  public abstract class originalInventoryUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemHistory.originalInventoryUOM>
  {
  }

  public abstract class originalInventoryQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemHistory.originalInventoryQty>
  {
  }

  public abstract class relatedInventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.relatedInventoryID>
  {
  }

  public abstract class relatedInventoryDesc : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.relatedInventoryDesc>
  {
  }

  public abstract class relatedInventoryUOM : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemHistory.relatedInventoryUOM>
  {
  }

  public abstract class relatedInventoryQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RelatedItemHistory.relatedInventoryQty>
  {
  }

  public abstract class soldQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RelatedItemHistory.soldQty>
  {
  }

  public abstract class relation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItemHistory.relation>
  {
  }

  public abstract class tag : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItemHistory.tag>
  {
  }

  public abstract class documentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RelatedItemHistory.documentDate>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItemHistory.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedItemHistory.orderNbr>
  {
  }

  public abstract class originalOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.originalOrderLineNbr>
  {
  }

  public abstract class relatedOrderLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.relatedOrderLineNbr>
  {
  }

  public abstract class invoiceDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemHistory.invoiceDocType>
  {
  }

  public abstract class invoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemHistory.invoiceRefNbr>
  {
  }

  public abstract class originalInvoiceLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.originalInvoiceLineNbr>
  {
  }

  public abstract class relatedInvoiceLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RelatedItemHistory.relatedInvoiceLineNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RelatedItemHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RelatedItemHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RelatedItemHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedItemHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RelatedItemHistory.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RelatedItemHistory.Tstamp>
  {
  }
}
