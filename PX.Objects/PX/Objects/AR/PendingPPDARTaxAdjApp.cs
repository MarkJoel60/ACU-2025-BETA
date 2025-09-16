// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PendingPPDARTaxAdjApp
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A projection over <see cref="T:PX.Objects.AR.ARAdjust" />, which selects
/// applications of payments to invoices that have been paid
/// in full and await processing of the cash discount on the
/// Generate AR Tax Adjustments (AR504500) process.
/// </summary>
[PXProjection(typeof (Select2<ARAdjust, InnerJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>>, Where<PX.Objects.AR.ARInvoice.released, Equal<True>, And<PX.Objects.AR.ARInvoice.pendingPPD, Equal<True>, And<PX.Objects.AR.ARInvoice.openDoc, Equal<True>, And<ARAdjust.released, Equal<True>, And<ARAdjust.voided, NotEqual<True>, And<ARAdjust.pendingPPD, Equal<True>, And<ARAdjust.pPDVATAdjRefNbr, IsNull, And<Where<ARAdjust.adjgDocType, Equal<ARDocType.payment>, Or<ARAdjust.adjgDocType, Equal<ARDocType.prepayment>, Or<ARAdjust.adjgDocType, Equal<ARDocType.prepaymentInvoice>, Or<ARAdjust.adjgDocType, Equal<ARDocType.refund>>>>>>>>>>>>>>))]
[PXCacheName("Pending PPD AR Tax Adj App")]
[Serializable]
public class PendingPPDARTaxAdjApp : ARAdjust
{
  /// <summary>Application index.</summary>
  [PXInt]
  public virtual int? Index { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARAdjust.AdjgDocType" />
  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (ARAdjust.adjgDocType))]
  public virtual 
  #nullable disable
  string PayDocType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARAdjust.AdjgRefNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (ARAdjust.adjgRefNbr))]
  public virtual string PayRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARAdjust.AdjdDocType" />
  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (ARAdjust.adjdDocType))]
  [ARInvoiceType.TaxAdjdList]
  public virtual string InvDocType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARAdjust.AdjdRefNbr" />
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (ARAdjust.adjdRefNbr))]
  public virtual string InvRefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryID" />
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.AR.ARInvoice.curyID))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string InvCuryID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryInfoID" />
  [PXDBLong(BqlField = typeof (PX.Objects.AR.ARInvoice.curyInfoID))]
  [CurrencyInfo]
  public virtual long? InvCuryInfoID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CustomerLocationID" />
  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARInvoice.customerLocationID))]
  public virtual int? InvCustomerLocationID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.TaxZoneID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARInvoice.taxZoneID))]
  public virtual string InvTaxZoneID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.TaxCalcMode" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARInvoice.taxCalcMode))]
  public virtual string InvTaxCalcMode { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.TermsID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AR.ARInvoice.termsID))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  [Terms(typeof (PX.Objects.AR.ARInvoice.docDate), typeof (PX.Objects.AR.ARInvoice.dueDate), typeof (PX.Objects.AR.ARInvoice.discDate), typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt), typeof (PX.Objects.AR.ARInvoice.curyOrigDiscAmt), typeof (PX.Objects.AR.ARInvoice.curyTaxTotal), typeof (PX.Objects.AR.ARInvoice.branchID))]
  public virtual string InvTermsID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryOrigDocAmt" />
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.origDocAmt), BqlField = typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt))]
  [PXUIField]
  public virtual Decimal? InvCuryOrigDocAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryOrigDiscAmt" />
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.origDiscAmt), BqlField = typeof (PX.Objects.AR.ARInvoice.curyOrigDiscAmt))]
  [PXUIField]
  public virtual Decimal? InvCuryOrigDiscAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryVatTaxableTotal" />
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.vatTaxableTotal), BqlField = typeof (PX.Objects.AR.ARInvoice.curyVatTaxableTotal))]
  [PXUIField]
  public virtual Decimal? InvCuryVatTaxableTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.CuryTaxTotal" />
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (PX.Objects.AR.ARInvoice.taxTotal), BqlField = typeof (PX.Objects.AR.ARInvoice.curyTaxTotal))]
  [PXUIField]
  public virtual Decimal? InvCuryTaxTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARRegister.CuryDocBal" />
  [PXDBCurrency(typeof (PX.Objects.AR.ARInvoice.curyInfoID), typeof (PX.Objects.AR.ARInvoice.docBal), BaseCalc = false, BqlField = typeof (PX.Objects.AR.ARInvoice.curyDocBal))]
  public virtual Decimal? InvCuryDocBal { get; set; }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PendingPPDARTaxAdjApp.selected>
  {
  }

  public abstract class index : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PendingPPDARTaxAdjApp.index>
  {
  }

  public abstract class payDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.payDocType>
  {
  }

  public abstract class payRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.payRefNbr>
  {
  }

  public abstract class invDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invDocType>
  {
  }

  public abstract class invRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invRefNbr>
  {
  }

  public abstract class invCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCuryID>
  {
  }

  public abstract class invCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCuryInfoID>
  {
  }

  public abstract class invCustomerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCustomerLocationID>
  {
  }

  public abstract class invTaxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invTaxZoneID>
  {
  }

  public abstract class invTaxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invTaxCalcMode>
  {
  }

  public abstract class invTermsID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invTermsID>
  {
  }

  public abstract class invCuryOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCuryOrigDocAmt>
  {
  }

  public abstract class invCuryOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCuryOrigDiscAmt>
  {
  }

  public abstract class invCuryVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCuryVatTaxableTotal>
  {
  }

  public abstract class invCuryTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCuryTaxTotal>
  {
  }

  public abstract class invCuryDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDARTaxAdjApp.invCuryDocBal>
  {
  }
}
