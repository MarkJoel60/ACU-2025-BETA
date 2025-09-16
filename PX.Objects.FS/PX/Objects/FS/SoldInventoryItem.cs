// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SoldInventoryItem
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<PX.Objects.IN.InventoryItem, InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<PX.Objects.AR.ARTran.tranType, Equal<ARDocType.invoice>>>, InnerJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.refNbr, Equal<PX.Objects.AR.ARTran.refNbr>, And<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.invoice>, And<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.orderType, Equal<PX.Objects.AR.ARTran.sOOrderType>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<PX.Objects.AR.ARTran.sOOrderNbr>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<PX.Objects.AR.ARTran.sOOrderLineNbr>>>>, InnerJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.SO.SOLineSplit.lineNbr>>>>, LeftJoin<FSEquipment, On<FSEquipment.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<FSEquipment.sourceType, Equal<ListField_SourceType_Equipment.AR_INVOICE>, And<FSEquipment.sourceRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<FSEquipment.arTranLineNbr, Equal<PX.Objects.AR.ARTran.lineNbr>>>>>>>>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, And<PX.Objects.IN.InventoryItem.itemStatus, Equal<InventoryItemStatus.active>, And<PX.Objects.SO.SOLineSplit.pOCreate, Equal<False>, And<FSxEquipmentModel.eQEnabled, Equal<True>, And<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.ModelEquipment>, And<FSEquipment.refNbr, IsNull>>>>>>>))]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<SoldInventoryItem.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class SoldInventoryItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr>))]
  [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARInvoice.refNbr))]
  [PXUIField]
  public virtual 
  #nullable disable
  string InvoiceRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.lineNbr))]
  [PXUIField]
  public virtual int? InvoiceLineNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.splitLineNbr))]
  [PXUIField]
  public virtual int? SOLineSplitNumber { get; set; }

  [CustomerActive]
  public virtual int? CustomerID { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<SoldInventoryItem.customerID>>, And<PX.Objects.CR.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>>))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  [Site(DisplayName = "Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr), BqlField = typeof (PX.Objects.AR.ARTran.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARInvoice.docDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [ARInvoiceType.List]
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARInvoice.docType))]
  public virtual string DocType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Equipment Type")]
  [FSSelectorEquipmentType]
  public virtual int? EquipmentTypeID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.IN.InventoryItem.itemClassID))]
  [PXSelector(typeof (Search<INItemClass.itemClassID>), SubstituteKey = typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  [PXUIField]
  public virtual int? ItemClassID { get; set; }

  [PXDBIdentity(BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [InventoryRaw(DisplayName = "Inventory ID", BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXDefault]
  [PXFieldDescription]
  public virtual string InventoryCD { get; set; }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARTran.lotSerialNbr))]
  [PXUIField]
  public virtual string LotSerialNumber { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOLineSplit.shippedQty))]
  [PXUIField]
  public virtual Decimal? ShippedQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOLine.orderQty))]
  [PXUIField]
  public virtual Decimal? Qty { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.orderDate))]
  [PXUIField]
  public virtual DateTime? SOOrderDate { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.orderType))]
  public virtual string SOOrderType { get; set; }

  [PXDBString(15, BqlField = typeof (PX.Objects.SO.SOLine.orderNbr))]
  [PXUIField]
  public virtual string SOOrderNbr { get; set; }

  [PXBool]
  [PXFormula(typeof (False))]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  public abstract class invoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SoldInventoryItem.invoiceRefNbr>
  {
  }

  public abstract class invoiceLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SoldInventoryItem.invoiceLineNbr>
  {
  }

  public abstract class sOLineSplitNumber : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SoldInventoryItem.sOLineSplitNumber>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SoldInventoryItem.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SoldInventoryItem.customerLocationID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SoldInventoryItem.descr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SoldInventoryItem.siteID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SoldInventoryItem.docDate>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SoldInventoryItem.docType>
  {
  }

  public abstract class equipmentTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SoldInventoryItem.equipmentTypeID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SoldInventoryItem.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SoldInventoryItem.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SoldInventoryItem.inventoryCD>
  {
  }

  public abstract class lotSerialNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SoldInventoryItem.lotSerialNumber>
  {
  }

  public abstract class shippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SoldInventoryItem.shippedQty>
  {
  }

  public abstract class qty : IBqlField, IBqlOperand
  {
  }

  public abstract class sOOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SoldInventoryItem.sOOrderDate>
  {
  }

  public abstract class sOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SoldInventoryItem.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SoldInventoryItem.sOOrderNbr>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SoldInventoryItem.selected>
  {
  }
}
