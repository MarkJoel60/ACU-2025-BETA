// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO.LandedCosts.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Landed Costs Detail")]
[Serializable]
public class POLandedCostDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected int? _LCAccrualAcct;
  protected int? _LCAccrualSub;

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the transaction belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
  /// </value>
  [Branch(typeof (POLandedCostDoc.branchID), null, true, true, true)]
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
  [PXParent(typeof (POLandedCostDetail.FK.LandedCostDocument))]
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

  [PXDBLong]
  [CurrencyInfo(typeof (POLandedCostDoc.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PO.LandedCostCode">landed cost code</see> used to describe the specific landed costs incurred for the line.
  /// This code is one of the codes associated with the vendor.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.LandedCostCode.LandedCostCodeID">LandedCostCode.LandedCostCodeID</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsFixed = false)]
  [PXUIField(DisplayName = "Landed Cost Code")]
  [PXSelector(typeof (Search<LandedCostCode.landedCostCodeID>))]
  [PXDefault]
  public virtual string LandedCostCodeID { get; set; }

  [PXDBString(150, IsUnicode = true)]
  [PXUIField]
  [PXLocalizableDefault(typeof (Search<LandedCostCode.descr, Where<LandedCostCode.landedCostCodeID, Equal<Current<POLandedCostDetail.landedCostCodeID>>>>), typeof (Customer.localeName))]
  public virtual string Descr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<POLandedCostDetail.landedCostCodeID, LandedCostCode.allocationMethod>))]
  [LandedCostAllocationMethod.List]
  [PXUIField]
  public virtual string AllocationMethod { get; set; }

  [PXDBCurrency(typeof (POLandedCostDetail.curyInfoID), typeof (POLandedCostDetail.lineAmt))]
  [PXUIField(DisplayName = "Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory">tax category</see> associated with the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// Defaults to the <see cref="P:PX.Objects.IN.InventoryItem.TaxCategoryID">tax category associated with the line item</see>.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [POLandedCostTax(typeof (POLandedCostDoc), typeof (POLandedCostTax), typeof (POLandedCostTaxTran))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<LandedCostCode.taxCategoryID, Where<LandedCostCode.landedCostCodeID, Equal<Current<POLandedCostDetail.landedCostCodeID>>>>))]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID">InventoryItem.InventoryID</see> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [LandedCostDetailInventory]
  [PXForeignReference(typeof (POLandedCostDetail.FK.InventoryItem))]
  public virtual int? InventoryID { get; set; }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POLandedCostDetail.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (POLandedCostDetail.inventoryID))]
  public virtual int? SubItemID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<LandedCostCode.lCAccrualAcct, Where<LandedCostCode.landedCostCodeID, Equal<Current<POLandedCostDetail.landedCostCodeID>>>>))]
  public virtual int? LCAccrualAcct
  {
    get => this._LCAccrualAcct;
    set => this._LCAccrualAcct = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<LandedCostCode.lCAccrualSub, Where<LandedCostCode.landedCostCodeID, Equal<Current<POLandedCostDetail.landedCostCodeID>>>>))]
  public virtual int? LCAccrualSub
  {
    get => this._LCAccrualSub;
    set => this._LCAccrualSub = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "AP Doc. Type", Enabled = false)]
  [PX.Objects.AP.APDocType.List]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "AP Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<POLandedCostDetail.aPDocType>>>>))]
  public virtual string APRefNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "IN Doc. Type", Enabled = false)]
  [PX.Objects.IN.INDocType.List]
  public virtual string INDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "IN Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POLandedCostDetail.iNDocType>>>>))]
  public virtual string INRefNbr { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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
    PrimaryKeyOf<POLandedCostDetail>.By<POLandedCostDetail.docType, POLandedCostDetail.refNbr, POLandedCostDetail.lineNbr>
  {
    public static POLandedCostDetail Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POLandedCostDetail) PrimaryKeyOf<POLandedCostDetail>.By<POLandedCostDetail.docType, POLandedCostDetail.refNbr, POLandedCostDetail.lineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class LandedCostDocument : 
      PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.docType, POLandedCostDetail.refNbr>
    {
    }

    public class LandedCostCode : 
      PrimaryKeyOf<LandedCostCode>.By<LandedCostCode.landedCostCodeID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.landedCostCodeID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.taxCategoryID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.subItemID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.curyInfoID>
    {
    }

    public class LandedCostAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.lCAccrualAcct>
    {
    }

    public class LandedCostAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.lCAccrualSub>
    {
    }

    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.aPDocType, POLandedCostDetail.aPRefNbr>
    {
    }

    public class INRegister : 
      PrimaryKeyOf<PX.Objects.IN.INRegister>.By<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>.ForeignKeyOf<POLandedCostDetail>.By<POLandedCostDetail.iNDocType, POLandedCostDetail.iNRefNbr>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetail.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetail.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetail.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetail.sortOrder>
  {
    public const string DispalyName = "Line Order";
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLandedCostDetail.curyInfoID>
  {
  }

  public abstract class landedCostCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetail.landedCostCodeID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetail.descr>
  {
  }

  public abstract class allocationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetail.allocationMethod>
  {
  }

  public abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDetail.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostDetail.lineAmt>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetail.taxCategoryID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetail.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetail.subItemID>
  {
  }

  public abstract class lCAccrualAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetail.lCAccrualAcct>
  {
  }

  public abstract class lCAccrualSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetail.lCAccrualSub>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetail.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetail.aPRefNbr>
  {
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetail.iNDocType>
  {
  }

  public abstract class iNRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetail.iNRefNbr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLandedCostDetail.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLandedCostDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POLandedCostDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostDetail.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLandedCostDetail.Tstamp>
  {
  }
}
