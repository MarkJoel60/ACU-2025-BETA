// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDetailS
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select2<POLandedCostDetail, InnerJoin<POLandedCostDoc, On<POLandedCostDetail.docType, Equal<POLandedCostDoc.docType>, And<POLandedCostDetail.refNbr, Equal<POLandedCostDoc.refNbr>>>>>), Persistent = false)]
[Serializable]
public class POLandedCostDetailS : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  /// <summary>The type of the landed cost receipt line.</summary>
  /// <value>
  /// The field is determined by the type of the parent <see cref="T:PX.Objects.PO.POLandedCostDoc">document</see>.
  /// For the list of possible values see <see cref="P:PX.Objects.PO.POLandedCostDoc.DocType" />.
  /// </value>
  [POLandedCostDocType.List]
  [PXDBString(1, IsKey = true, IsFixed = true, BqlField = typeof (POLandedCostDetail.docType))]
  [PXUIField(DisplayName = "Doc. Type")]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>
  /// Reference number of the parent <see cref="T:PX.Objects.PO.POLandedCostDoc">document</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (POLandedCostDetail.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXParent(typeof (Select<POLandedCostDoc, Where<POLandedCostDoc.docType, Equal<Current<POLandedCostDetailS.docType>>, And<POLandedCostDoc.refNbr, Equal<Current<POLandedCostDetailS.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  /// <summary>The number of the transaction line in the document.</summary>
  /// <value>
  /// Note that the sequence of line numbers of the transactions belonging to a single document may include gaps.
  /// </value>
  [PXDBInt(IsKey = true, BqlField = typeof (POLandedCostDetail.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.")]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(BqlField = typeof (POLandedCostDetail.sortOrder))]
  [PXUIField(DisplayName = "Line Order")]
  public virtual int? SortOrder { get; set; }

  [PXUIField(DisplayName = "Currency")]
  [PXDBString(5, IsUnicode = true, BqlField = typeof (POLandedCostDoc.curyID))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBLong(BqlField = typeof (POLandedCostDetail.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (POLandedCostDoc.vendorRefNbr))]
  [PXUIField]
  public virtual string VendorRefNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.PO.LandedCostCode">landed cost code</see> used to describe the specific landed costs incurred for the line.
  /// This code is one of the codes associated with the vendor.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.LandedCostCode.LandedCostCodeID">LandedCostCode.LandedCostCodeID</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsFixed = false, BqlField = typeof (POLandedCostDetail.landedCostCodeID))]
  [PXUIField(DisplayName = "Landed Cost Code")]
  [PXSelector(typeof (Search<LandedCostCode.landedCostCodeID>))]
  public virtual string LandedCostCodeID { get; set; }

  [PXDBString(150, IsUnicode = true, BqlField = typeof (POLandedCostDetail.descr))]
  [PXUIField(DisplayName = "Description")]
  [PXLocalizableDefault(typeof (Search<LandedCostCode.descr, Where<LandedCostCode.landedCostCodeID, Equal<Current<POLandedCostDetailS.landedCostCodeID>>>>), typeof (Customer.localeName))]
  public virtual string Descr { get; set; }

  [PXDBCurrency(typeof (POLandedCostDetailS.curyInfoID), typeof (POLandedCostDetailS.lineAmt), BqlField = typeof (POLandedCostDetail.curyLineAmt))]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryLineAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POLandedCostDetail.lineAmt))]
  public virtual Decimal? LineAmt { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory">tax category</see> associated with the line.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// Defaults to the <see cref="P:PX.Objects.IN.InventoryItem.TaxCategoryID">tax category associated with the line item</see>.
  /// </value>
  [PXDBString(15, IsUnicode = true, BqlField = typeof (POLandedCostDetail.taxCategoryID))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  public virtual string TaxCategoryID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.IN.InventoryItem">inventory item</see> associated with the transaction.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.InventoryItem.InventoryID">InventoryItem.InventoryID</see> field.
  /// </value>
  [APTranInventoryItem(Filterable = true, BqlField = typeof (POLandedCostDetail.inventoryID))]
  [PXForeignReference(typeof (Field<POLandedCostDetailS.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (POLandedCostDetailS.inventoryID), BqlField = typeof (POLandedCostDetail.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (POLandedCostDetail.aPDocType))]
  [PXUIField(DisplayName = "AP Doc. Type")]
  [PX.Objects.AP.APDocType.List]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POLandedCostDetail.aPRefNbr))]
  [PXUIField(DisplayName = "AP Ref. Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<POLandedCostDetailS.aPDocType>>>>))]
  public virtual string APRefNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POLandedCostDetail.iNDocType))]
  [PXUIField(DisplayName = "IN Doc. Type")]
  [PX.Objects.IN.INDocType.List]
  public virtual string INDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POLandedCostDetail.iNRefNbr))]
  [PXUIField(DisplayName = "IN Ref. Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POLandedCostDetailS.iNDocType>>>>))]
  public virtual string INRefNbr { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLandedCostDetailS.selected>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetailS.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetailS.sortOrder>
  {
    public const string DispalyName = "Line Order";
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLandedCostDetailS.curyInfoID>
  {
  }

  public abstract class vendorRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailS.vendorRefNbr>
  {
  }

  public abstract class landedCostCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailS.landedCostCodeID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.descr>
  {
  }

  public abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostDetailS.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostDetailS.lineAmt>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostDetailS.taxCategoryID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetailS.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostDetailS.subItemID>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.aPRefNbr>
  {
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.iNDocType>
  {
  }

  public abstract class iNRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostDetailS.iNRefNbr>
  {
  }
}
