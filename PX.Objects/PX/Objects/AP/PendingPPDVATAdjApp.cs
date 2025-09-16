// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PendingPPDVATAdjApp
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Pending Prompt Payment Discount (PPD) VAT Adjustment Applications
/// </summary>
[PXProjection(typeof (Select2<APAdjust, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APAdjust.adjdDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APAdjust.adjdRefNbr>>>>, Where<PX.Objects.AP.APInvoice.released, Equal<True>, And<PX.Objects.AP.APInvoice.pendingPPD, Equal<True>, And<PX.Objects.AP.APInvoice.openDoc, Equal<True>, And<APAdjust.released, Equal<True>, And<APAdjust.voided, NotEqual<True>, And<APAdjust.pendingPPD, Equal<True>, And<APAdjust.pPDVATAdjRefNbr, PX.Data.IsNull>>>>>>>>))]
[PXCacheName("Applications Pending VAT Adjustment for Prompt Payment Discount")]
[Serializable]
public class PendingPPDVATAdjApp : APAdjust
{
  protected 
  #nullable disable
  string _InvCuryID;
  protected int? _InvVendorLocationID;
  protected string _InvTaxZoneID;
  protected string _InvTermsID;
  protected Decimal? _InvCuryOrigDocAmt;
  protected Decimal? _InvCuryOrigDiscAmt;
  protected Decimal? _InvCuryVatTaxableTotal;
  protected Decimal? _InvCuryTaxTotal;
  protected Decimal? _InvCuryDocBal;

  /// <summary>Index of the record</summary>
  [PXInt]
  public virtual int Index { get; set; }

  /// <summary>Payment (adjusting) Document type</summary>
  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (APAdjust.adjgDocType))]
  public virtual string PayDocType { get; set; }

  /// <summary>Payment (adjusting) Ref. number</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APAdjust.adjgRefNbr))]
  public virtual string PayRefNbr { get; set; }

  /// <summary>
  /// Adjusted Doc Type, it can be a docType listed in the <see cref="T:PX.Objects.AP.APInvoiceType.TaxInvoiceListAttribute" />
  /// </summary>
  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (APAdjust.adjdDocType))]
  [APInvoiceType.TaxInvoiceList]
  public virtual string InvDocType { get; set; }

  /// <summary>Adjusted Ref. number</summary>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (APAdjust.adjdRefNbr))]
  public virtual string InvRefNbr { get; set; }

  /// <summary>
  /// Prompt Payment Discount Adjust Number.
  /// Should be filled from the <see cref="!:APPayment.AdjCntr">number of lines</see> in the related payment document.
  /// </summary>
  [PXDBInt(IsKey = true, BqlField = typeof (APAdjust.adjNbr))]
  public virtual int? PPDAdjNbr { get; set; }

  /// <summary>Adjusted document CuryID</summary>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.AP.APInvoice.curyID))]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string InvCuryID
  {
    get => this._InvCuryID;
    set => this._InvCuryID = value;
  }

  /// <summary>Adjusted document CuryID</summary>
  [PXDBLong(BqlField = typeof (PX.Objects.AP.APInvoice.curyInfoID))]
  public virtual long? InvCuryInfoID { get; set; }

  /// <summary>Adjusted document VendorLocationID</summary>
  [PXDBInt(BqlField = typeof (PX.Objects.AP.APInvoice.vendorLocationID))]
  public virtual int? InvVendorLocationID
  {
    get => this._InvVendorLocationID;
    set => this._InvVendorLocationID = value;
  }

  /// <summary>Adjusted document TaxZoneID</summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APInvoice.taxZoneID))]
  public virtual string InvTaxZoneID
  {
    get => this._InvTaxZoneID;
    set => this._InvTaxZoneID = value;
  }

  /// <summary>Adjusted document TaxCalcMode</summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APInvoice.taxCalcMode))]
  public virtual string InvTaxCalcMode { get; set; }

  /// <summary>Adjusted document Credit TermsID</summary>
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APInvoice.termsID))]
  [PXUIField(DisplayName = "Credit Terms", Visibility = PXUIVisibility.Visible)]
  public virtual string InvTermsID
  {
    get => this._InvTermsID;
    set => this._InvTermsID = value;
  }

  /// <summary>Adjusted document original cury amount</summary>
  [PXDBCurrency(typeof (PX.Objects.AP.APInvoice.curyInfoID), typeof (PX.Objects.AP.APInvoice.origDocAmt), BqlField = typeof (PX.Objects.AP.APInvoice.curyOrigDocAmt))]
  [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? InvCuryOrigDocAmt
  {
    get => this._InvCuryOrigDocAmt;
    set => this._InvCuryOrigDocAmt = value;
  }

  /// <summary>Adjusted document Cury original discount amount</summary>
  [PXDBCurrency(typeof (PX.Objects.AP.APInvoice.curyInfoID), typeof (PX.Objects.AP.APInvoice.origDiscAmt), BqlField = typeof (PX.Objects.AP.APInvoice.curyOrigDiscAmt))]
  [PXUIField(DisplayName = "Cash Discount", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Decimal? InvCuryOrigDiscAmt
  {
    get => this._InvCuryOrigDiscAmt;
    set => this._InvCuryOrigDiscAmt = value;
  }

  /// <summary>Adjusted document VAT Taxable total amount</summary>
  [PXDBCurrency(typeof (PX.Objects.AP.APInvoice.curyInfoID), typeof (PX.Objects.AP.APInvoice.vatTaxableTotal), BqlField = typeof (PX.Objects.AP.APInvoice.curyVatTaxableTotal))]
  [PXUIField(DisplayName = "VAT Taxable Total", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? InvCuryVatTaxableTotal
  {
    get => this._InvCuryVatTaxableTotal;
    set => this._InvCuryVatTaxableTotal = value;
  }

  /// <summary>Adjusted document cury Tax Total amount</summary>
  [PXDBCurrency(typeof (APRegister.curyInfoID), typeof (PX.Objects.AP.APInvoice.taxTotal), BqlField = typeof (PX.Objects.AP.APInvoice.curyTaxTotal))]
  [PXUIField(DisplayName = "Tax Total", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? InvCuryTaxTotal
  {
    get => this._InvCuryTaxTotal;
    set => this._InvCuryTaxTotal = value;
  }

  /// <summary>Adjusted document cury Document Balance</summary>
  [PXDBCurrency(typeof (PX.Objects.AP.APInvoice.curyInfoID), typeof (PX.Objects.AP.APInvoice.docBal), BaseCalc = false, BqlField = typeof (PX.Objects.AP.APInvoice.curyDocBal))]
  public virtual Decimal? InvCuryDocBal
  {
    get => this._InvCuryDocBal;
    set => this._InvCuryDocBal = value;
  }

  /// <summary>Vendor Ref. of the Invoice.</summary>
  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Standalone.APInvoice.invoiceNbr))]
  [PXUIField(DisplayName = "Vendor Ref.", Visible = false)]
  public virtual string InvoiceNbr { get; set; }

  /// <summary>Description of the document.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true, BqlField = typeof (APRegister.docDesc))]
  [PXUIField(DisplayName = "Description", Visible = false)]
  public virtual string DocDesc { get; set; }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PendingPPDVATAdjApp.selected>
  {
  }

  public abstract class index : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PendingPPDVATAdjApp.index>
  {
  }

  public abstract class payDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDVATAdjApp.payDocType>
  {
  }

  public abstract class payRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PendingPPDVATAdjApp.payRefNbr>
  {
  }

  public abstract class invDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invDocType>
  {
  }

  public abstract class invRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PendingPPDVATAdjApp.invRefNbr>
  {
  }

  public abstract class ppdAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PendingPPDVATAdjApp.ppdAdjNbr>
  {
  }

  public abstract class invCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PendingPPDVATAdjApp.invCuryID>
  {
  }

  public abstract class invCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invCuryInfoID>
  {
  }

  public abstract class invVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invVendorLocationID>
  {
  }

  public abstract class invTaxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invTaxZoneID>
  {
  }

  public abstract class invTaxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invTaxCalcMode>
  {
  }

  public abstract class invTermsID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invTermsID>
  {
  }

  public abstract class invCuryOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invCuryOrigDocAmt>
  {
  }

  public abstract class invCuryOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invCuryOrigDiscAmt>
  {
  }

  public abstract class invCuryVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invCuryVatTaxableTotal>
  {
  }

  public abstract class invCuryTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invCuryTaxTotal>
  {
  }

  public abstract class invCuryDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invCuryDocBal>
  {
  }

  public abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PendingPPDVATAdjApp.invoiceNbr>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PendingPPDVATAdjApp.docDesc>
  {
  }
}
