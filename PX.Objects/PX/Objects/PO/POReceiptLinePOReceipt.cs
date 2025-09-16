// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLinePOReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>Read-only class for selector</summary>
[PXProjection(typeof (SelectFrom<POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<POReceipt>.On<POReceiptLine.FK.Receipt>), Persistent = false)]
[PXCacheName("Purchase Receipt Line")]
[Serializable]
public class POReceiptLinePOReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.ReceiptType" />
  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POReceipt.receiptType))]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.ReceiptNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (POReceipt.receiptNbr))]
  [POReceiptType.RefNbr(typeof (Search<POReceipt.receiptNbr, Where<BqlOperand<POReceipt.receiptType, IBqlString>.IsEqual<BqlField<POReceiptLinePOReceipt.receiptType, IBqlString>.FromCurrent>>>), Filterable = true)]
  [PXUIField(DisplayName = "Receipt Nbr.", Enabled = false)]
  public virtual string ReceiptNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.LineNbr" />
  [PXDBInt(IsKey = true, BqlField = typeof (POReceiptLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
  public virtual int? LineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.BranchID" />
  [Branch(null, null, true, true, true, BqlField = typeof (POReceiptLine.branchID))]
  public virtual int? BranchID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.POType" />
  [PXDBString(2, IsFixed = true, BqlField = typeof (POReceiptLine.pOType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string POType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.PONbr" />
  [PXDBString(15, IsUnicode = true, BqlField = typeof (POReceiptLine.pONbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PX.Objects.PO.PO.RefNbr(typeof (Search<POOrder.orderNbr>), Filterable = true)]
  public virtual string PONbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.POLineNbr" />
  [PXDBInt(BqlField = typeof (POReceiptLine.pOLineNbr))]
  [PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
  public virtual int? POLineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.ReceiptDate" />
  [PXDBDate(BqlField = typeof (POReceipt.receiptDate))]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? ReceiptDate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.Status" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (POReceipt.status))]
  [PXDefault("H")]
  [PXUIField]
  [POReceiptStatus.List]
  public virtual string Status { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.InventoryID" />
  [Inventory(Enabled = false, BqlField = typeof (POReceiptLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.LotSerialNbr" />
  [PXDBString(100, IsUnicode = true, BqlField = typeof (POReceiptLine.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.UOM" />
  [INUnit(typeof (POReceiptLine.inventoryID), Enabled = false, BqlField = typeof (POReceiptLine.uOM))]
  public virtual string UOM { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.ReceiptQty" />
  [PXDBQuantity(BqlField = typeof (POReceiptLine.receiptQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Receipt Qty.", Enabled = false)]
  public virtual Decimal? ReceiptQty { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLine.InvtMult" />
  [PXDBShort(BqlField = typeof (POReceiptLine.invtMult))]
  [PXDefault]
  [PXUIField(DisplayName = "Inventory Multiplier", Enabled = false)]
  public virtual short? InvtMult { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.IsUnderCorrection" />
  [PXDBBool(BqlField = typeof (POReceipt.isUnderCorrection))]
  [PXDefault(false)]
  [PXUIField(Enabled = false)]
  public virtual bool? IsUnderCorrection { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.Canceled" />
  [PXDBBool(BqlField = typeof (POReceipt.canceled))]
  [PXDefault(false)]
  [PXUIField(Enabled = false)]
  public virtual bool? Canceled { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.ReceiptType" />
  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLinePOReceipt.receiptType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.ReceiptNbr" />
  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLinePOReceipt.receiptNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.LineNbr" />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLinePOReceipt.lineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.BranchID" />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLinePOReceipt.branchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.POType" />
  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLinePOReceipt.pOType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.PONbr" />
  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLinePOReceipt.pONbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.POLineNbr" />
  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLinePOReceipt.pOLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.ReceiptDate" />
  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLinePOReceipt.receiptDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.Status" />
  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLinePOReceipt.status>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.InventoryID" />
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLinePOReceipt.inventoryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.LotSerialNbr" />
  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLinePOReceipt.lotSerialNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.UOM" />
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLinePOReceipt.uOM>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.ReceiptQty" />
  public abstract class receiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLinePOReceipt.receiptQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.InvtMult" />
  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  POReceiptLinePOReceipt.invtMult>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.IsUnderCorrection" />
  public abstract class isUnderCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLinePOReceipt.isUnderCorrection>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceiptLinePOReceipt.Canceled" />
  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLinePOReceipt.canceled>
  {
  }
}
