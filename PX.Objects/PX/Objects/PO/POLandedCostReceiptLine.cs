// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostReceiptLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Landed Costs Receipt Line")]
[Serializable]
public class POLandedCostReceiptLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
  /// </value>
  [Branch(typeof (POLandedCostDoc.branchID), null, true, true, true, Enabled = false)]
  public virtual int? BranchID { get; set; }

  /// <summary>The type of the landed cost receipt line.</summary>
  /// <value>
  /// The field is determined by the type of the parent <see cref="T:PX.Objects.PO.POLandedCostDoc">document</see>.
  /// For the list of possible values see <see cref="P:PX.Objects.PO.POLandedCostDoc.DocType" />.
  /// </value>
  [POLandedCostDocType.List]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (POLandedCostDoc.docType))]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>
  /// Reference number of the parent <see cref="T:PX.Objects.PO.POLandedCostDoc">document</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (POLandedCostDoc.refNbr))]
  [PXUIField]
  [PXParent(typeof (POLandedCostReceiptLine.FK.LandedCostDocument))]
  [PXParent(typeof (POLandedCostReceiptLine.FK.LandedCostReceipt), LeaveChildren = true, ParentCreate = true)]
  public virtual string RefNbr { get; set; }

  /// <summary>The number of the transaction line in the document.</summary>
  /// <value>
  /// Note that the sequence of line numbers of the transactions belonging to a single document may include gaps.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (POLandedCostDoc.lineCntr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  public virtual int? SortOrder { get; set; }

  [PXDBBool]
  public virtual bool? IsStockItem { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID">InventoryItem.InventoryID</see> field.
  /// </value>
  [Inventory(Filterable = true, Enabled = false)]
  [PXForeignReference(typeof (POLandedCostReceiptLine.FK.InventoryItem))]
  [ConvertedInventoryItem(typeof (POLandedCostReceiptLine.isStockItem))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (POLandedCostReceiptLine.inventoryID), Enabled = false)]
  public virtual int? SubItemID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (POLandedCostDoc.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "PO Receipt Type", Visible = false, Enabled = false)]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Current<POLandedCostReceiptLine.pOReceiptType>>>>))]
  [PXFormula(null, typeof (CountCalc<POLandedCostReceipt.lineCntr>))]
  public virtual string POReceiptNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Receipt Line Nbr.", Enabled = false)]
  public virtual int? POReceiptLineNbr { get; set; }

  /// <summary>
  /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the line.
  /// </summary>
  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string POReceiptBaseCuryID { get; set; }

  [Site(Enabled = false)]
  [PXForeignReference(typeof (POLandedCostReceiptLine.FK.Site))]
  public virtual int? SiteID { get; set; }

  [INUnit(typeof (POLandedCostReceiptLine.inventoryID), DisplayName = "UOM", Enabled = false)]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (POLandedCostReceiptLine.uOM), typeof (POLandedCostReceiptLine.baseReceiptQty), HandleEmptyKey = true, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReceiptQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReceiptQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault]
  public virtual Decimal? UnitWeight { get; set; }

  [PXDBDecimal(6)]
  [PXDefault]
  public virtual Decimal? UnitVolume { get; set; }

  [PXDBDecimal(6)]
  [PXUIField]
  [PXFormula(typeof (Mult<Row<POLandedCostReceiptLine.baseReceiptQty>.WithDependency<POLandedCostReceiptLine.receiptQty>, POLandedCostReceiptLine.unitWeight>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtWeight { get; set; }

  [PXDBDecimal(6)]
  [PXUIField]
  [PXFormula(typeof (Mult<Row<POLandedCostReceiptLine.baseReceiptQty>.WithDependency<POLandedCostReceiptLine.receiptQty>, POLandedCostReceiptLine.unitVolume>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtVolume { get; set; }

  [PXDBBaseCury]
  [PXUIField(DisplayName = "Ext. Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt { get; set; }

  [PXDBCurrency(typeof (POLandedCostReceiptLine.curyInfoID), typeof (POLandedCostReceiptLine.allocatedLCAmt))]
  [PXUIField(DisplayName = "Allocated Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAllocatedLCAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedLCAmt { get; set; }

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

  public class PK : 
    PrimaryKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.docType, POLandedCostReceiptLine.refNbr, POLandedCostReceiptLine.lineNbr>
  {
    public static POLandedCostReceiptLine Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POLandedCostReceiptLine) PrimaryKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.docType, POLandedCostReceiptLine.refNbr, POLandedCostReceiptLine.lineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.branchID>
    {
    }

    public class LandedCostDocument : 
      PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.docType, POLandedCostReceiptLine.refNbr>
    {
    }

    public class LandedCostReceipt : 
      PrimaryKeyOf<POLandedCostReceipt>.By<POLandedCostReceipt.lCDocType, POLandedCostReceipt.lCRefNbr, POLandedCostReceipt.pOReceiptType, POLandedCostReceipt.pOReceiptNbr>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.docType, POLandedCostReceiptLine.refNbr, POLandedCostReceiptLine.pOReceiptType, POLandedCostReceiptLine.pOReceiptNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.subItemID>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.pOReceiptType, POLandedCostReceiptLine.pOReceiptNbr>
    {
    }

    public class ReceiptLine : 
      PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.pOReceiptType, POLandedCostReceiptLine.pOReceiptNbr, POLandedCostReceiptLine.pOReceiptLineNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.siteID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.curyInfoID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostReceiptLine.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostReceiptLine.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostReceiptLine.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostReceiptLine.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostReceiptLine.sortOrder>
  {
    public const string DispalyName = "Line Order";
  }

  public abstract class isStockItem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POLandedCostReceiptLine.isStockItem>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POLandedCostReceiptLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostReceiptLine.subItemID>
  {
  }

  public abstract class tranDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceiptLine.tranDesc>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    POLandedCostReceiptLine.curyInfoID>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceiptLine.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceiptLine.pOReceiptNbr>
  {
  }

  public abstract class pOReceiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POLandedCostReceiptLine.pOReceiptLineNbr>
  {
  }

  public abstract class pOReceiptBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceiptLine.pOReceiptBaseCuryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostReceiptLine.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostReceiptLine.uOM>
  {
  }

  public abstract class receiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.receiptQty>
  {
  }

  public abstract class baseReceiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.baseReceiptQty>
  {
  }

  public abstract class unitWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.unitWeight>
  {
  }

  public abstract class unitVolume : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.unitVolume>
  {
  }

  public abstract class extWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.extWeight>
  {
  }

  public abstract class extVolume : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.extVolume>
  {
  }

  public abstract class lineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.lineAmt>
  {
  }

  public abstract class curyAllocatedLCAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.curyAllocatedLCAmt>
  {
  }

  public abstract class allocatedLCAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostReceiptLine.allocatedLCAmt>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POLandedCostReceiptLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceiptLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostReceiptLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POLandedCostReceiptLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostReceiptLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostReceiptLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLandedCostReceiptLine.Tstamp>
  {
  }
}
