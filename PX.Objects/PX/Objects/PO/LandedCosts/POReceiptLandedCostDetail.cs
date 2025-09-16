// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.POReceiptLandedCostDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.PO.LandedCosts.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO.LandedCosts;

[PXProjection(typeof (Select5<POLandedCostReceipt, InnerJoin<POLandedCostDoc, On<POLandedCostDoc.docType, Equal<POLandedCostReceipt.lCDocType>, And<POLandedCostDoc.refNbr, Equal<POLandedCostReceipt.lCRefNbr>>>, InnerJoin<POLandedCostDetail, On<POLandedCostDetail.docType, Equal<POLandedCostReceipt.lCDocType>, And<POLandedCostDetail.refNbr, Equal<POLandedCostReceipt.lCRefNbr>>>, InnerJoin<POLandedCostReceiptLine, On<POLandedCostReceiptLine.docType, Equal<POLandedCostReceipt.lCDocType>, And<POLandedCostReceiptLine.refNbr, Equal<POLandedCostReceipt.lCRefNbr>, And<POLandedCostReceiptLine.pOReceiptNbr, Equal<POLandedCostReceipt.pOReceiptNbr>>>>, LeftJoin<POLandedCostSplit, On<POLandedCostSplit.receiptLineNbr, Equal<POLandedCostReceiptLine.lineNbr>, And<POLandedCostSplit.detailLineNbr, Equal<POLandedCostDetail.lineNbr>, And<POLandedCostSplit.refNbr, Equal<POLandedCostReceipt.lCRefNbr>, And<POLandedCostSplit.docType, Equal<POLandedCostReceipt.lCDocType>>>>>>>>>, Aggregate<GroupBy<POLandedCostReceipt.lCDocType, GroupBy<POLandedCostReceipt.lCRefNbr, GroupBy<POLandedCostDetail.lineNbr, GroupBy<POLandedCostReceipt.pOReceiptType, GroupBy<POLandedCostReceipt.pOReceiptNbr, Sum<POLandedCostSplit.lineAmt, Sum<POLandedCostSplit.curyLineAmt>>>>>>>>>), Persistent = false)]
[PXBreakInheritance]
[PXCacheName("Landed Costs Receipt")]
[Serializable]
public class POReceiptLandedCostDetail : POLandedCostReceipt
{
  [POLandedCostDocType.List]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Landed Cost Type", Visible = false)]
  public override 
  #nullable disable
  string LCDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Landed Cost Nbr.")]
  [PXSelector(typeof (Search<POLandedCostDoc.refNbr, Where<POLandedCostDoc.docType, Equal<Current<POReceiptLandedCostDetail.lCDocType>>>>))]
  public override string LCRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostDetail.LineNbr" />
  [PXDBInt(IsKey = true, BqlField = typeof (POLandedCostDetail.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr { get; set; }

  /// <summary>The type of the receipt document.</summary>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "PO Receipt Type", Visible = false)]
  public override string POReceiptType { get; set; }

  /// <summary>The reference number of the receipt document.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "PO Receipt Nbr.")]
  public override string POReceiptNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostDoc.Status" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (POLandedCostDoc.status))]
  [PXUIField]
  [POLandedCostDocStatus.List]
  public virtual string Status { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostDoc.DocDate" />
  [PXDBDate(BqlField = typeof (POLandedCostDoc.docDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostDoc.VendorID" />
  [Vendor]
  public virtual int? VendorID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostDoc.CuryID" />
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POLandedCostDoc.curyID))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBLong(BqlField = typeof (POLandedCostDetail.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(15, IsUnicode = true, IsFixed = false, BqlField = typeof (POLandedCostDetail.landedCostCodeID))]
  [PXUIField(DisplayName = "Landed Cost Code")]
  [PXSelector(typeof (Search<LandedCostCode.landedCostCodeID>))]
  public virtual string LandedCostCodeID { get; set; }

  [PXDBString(150, IsUnicode = true, BqlField = typeof (POLandedCostDetail.descr))]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POLandedCostDetail.allocationMethod))]
  [LandedCostAllocationMethod.List]
  [PXUIField]
  public virtual string AllocationMethod { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostSplit.CuryLineAmt" />
  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  [PXDBDecimal(BqlField = typeof (POLandedCostSplit.curyLineAmt))]
  public virtual Decimal? POLandedCostSplitCuryLineAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostDetail.CuryLineAmt" />
  [Obsolete("This DAC field has been deprecated and will be removed in a later Acumatica ERP version.")]
  [PXDBDecimal(BqlField = typeof (POLandedCostDetail.curyLineAmt))]
  public virtual Decimal? POLandedCostDetailCuryLineAmt { get; set; }

  [PXCurrency(typeof (POReceiptLandedCostDetail.curyInfoID), typeof (POReceiptLandedCostDetail.lineAmt))]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryLineAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POLandedCostDetail.LineAmt" />
  [PXDBDecimal(BqlField = typeof (POLandedCostDetail.lineAmt))]
  public virtual Decimal? POLandedCostDetailLineAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLandedCostSplit.lineAmt))]
  public virtual Decimal? LineAmt { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (POLandedCostDetail.aPDocType))]
  [PXUIField(DisplayName = "AP Doc. Type", Enabled = false)]
  [PX.Objects.AP.APDocType.List]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POLandedCostDetail.aPRefNbr))]
  [PXUIField(DisplayName = "AP Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Current<POReceiptLandedCostDetail.aPDocType>>>>))]
  public virtual string APRefNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POLandedCostDetail.iNDocType))]
  [PXUIField(DisplayName = "IN Doc. Type", Enabled = false)]
  [PX.Objects.IN.INDocType.List]
  public virtual string INDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POLandedCostDetail.iNRefNbr))]
  [PXUIField(DisplayName = "IN Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POReceiptLandedCostDetail.iNDocType>>>>))]
  public virtual string INRefNbr { get; set; }

  public new abstract class lCDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.lCDocType>
  {
  }

  public new abstract class lCRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.lCRefNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLandedCostDetail.lineNbr>
  {
  }

  public new abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.pOReceiptType>
  {
  }

  public new abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.pOReceiptNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLandedCostDetail.status>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLandedCostDetail.docDate>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLandedCostDetail.vendorID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLandedCostDetail.curyID>
  {
  }

  public abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    POReceiptLandedCostDetail.curyInfoID>
  {
  }

  public abstract class landedCostCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.landedCostCodeID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLandedCostDetail.descr>
  {
  }

  public abstract class allocationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.allocationMethod>
  {
  }

  [Obsolete("This class has been deprecated and will be removed in the later Acumatica versions.")]
  public abstract class pOLandedCostSplitCuryLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLandedCostDetail.pOLandedCostSplitCuryLineAmt>
  {
  }

  [Obsolete("This class has been deprecated and will be removed in the later Acumatica versions.")]
  public abstract class pOLandedCostDetailCuryLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLandedCostDetail.pOLandedCostDetailCuryLineAmt>
  {
  }

  public abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLandedCostDetail.curyLineAmt>
  {
  }

  public abstract class pOLandedCostDetailLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLandedCostDetail.pOLandedCostDetailLineAmt>
  {
  }

  public abstract class lineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLandedCostDetail.lineAmt>
  {
  }

  public abstract class aPDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.aPDocType>
  {
  }

  public abstract class aPRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.aPRefNbr>
  {
  }

  public abstract class iNDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.iNDocType>
  {
  }

  public abstract class iNRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLandedCostDetail.iNRefNbr>
  {
  }
}
