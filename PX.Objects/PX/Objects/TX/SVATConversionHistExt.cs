// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.SVATConversionHistExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXPrimaryGraph(new System.Type[] {typeof (CATranEntry), typeof (APQuickCheckEntry), typeof (APInvoiceEntry), typeof (ARCashSaleEntry), typeof (ARInvoiceEntry)}, new System.Type[] {typeof (Select<CAAdj, Where<Current<SVATConversionHistExt.module>, Equal<BatchModule.moduleCA>, And<CAAdj.adjTranType, Equal<Current<SVATConversionHistExt.adjdDocType>>, And<CAAdj.adjRefNbr, Equal<Current<SVATConversionHistExt.adjdRefNbr>>>>>>), typeof (Select<APQuickCheck, Where<Current<SVATConversionHistExt.module>, Equal<BatchModule.moduleAP>, And<APQuickCheck.docType, Equal<Current<SVATConversionHistExt.adjdDocType>>, And<APQuickCheck.refNbr, Equal<Current<SVATConversionHistExt.adjdRefNbr>>>>>>), typeof (Select<PX.Objects.AP.APInvoice, Where<Current<SVATConversionHistExt.module>, Equal<BatchModule.moduleAP>, And<PX.Objects.AP.APInvoice.docType, Equal<Current<SVATConversionHistExt.adjdDocType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Current<SVATConversionHistExt.adjdRefNbr>>>>>>), typeof (Select<ARCashSale, Where<Current<SVATConversionHistExt.module>, Equal<BatchModule.moduleAR>, And<ARCashSale.docType, Equal<Current<SVATConversionHistExt.adjdDocType>>, And<ARCashSale.refNbr, Equal<Current<SVATConversionHistExt.adjdRefNbr>>>>>>), typeof (Select<PX.Objects.AR.ARInvoice, Where<Current<SVATConversionHistExt.module>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.ARInvoice.docType, Equal<Current<SVATConversionHistExt.adjdDocType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<SVATConversionHistExt.adjdRefNbr>>>>>>)})]
[PXProjection(typeof (Select2<SVATConversionHist, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<SVATConversionHist.vendorID>>, LeftJoin<PX.Objects.AP.APInvoice, On<SVATConversionHist.module, Equal<BatchModule.moduleAP>, And<SVATConversionHist.adjdDocType, Equal<PX.Objects.AP.APInvoice.docType>, And<SVATConversionHist.adjdRefNbr, Equal<PX.Objects.AP.APInvoice.refNbr>>>>, LeftJoin<PX.Objects.AR.ARInvoice, On<SVATConversionHist.module, Equal<BatchModule.moduleAR>, And<SVATConversionHist.adjdDocType, Equal<PX.Objects.AR.ARInvoice.docType>, And<SVATConversionHist.adjdRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>>, LeftJoin<CAAdj, On<SVATConversionHist.module, Equal<BatchModule.moduleCA>, And<SVATConversionHist.adjdDocType, Equal<CAAdj.adjTranType>, And<SVATConversionHist.adjdRefNbr, Equal<CAAdj.adjRefNbr>>>>>>>>>), Persistent = true)]
[Serializable]
public class SVATConversionHistExt : SVATConversionHist
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SVATConversionHist.module))]
  [PXDefault]
  [PXUIField(DisplayName = "Module")]
  [BatchModule.List]
  [PXFieldDescription]
  public override 
  #nullable disable
  string Module { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (SVATConversionHist.adjdDocType))]
  [PXDefault]
  [PXUIField(DisplayName = "Type")]
  public override string AdjdDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (SVATConversionHist.adjdRefNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.")]
  public override string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SVATConversionHist.adjdLineNbr))]
  [PXUIField(DisplayName = "Line Nbr.")]
  [PXDefault(0)]
  public override int? AdjdLineNbr { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (SVATConversionHist.adjgDocType))]
  [PXDefault(typeof (SVATConversionHist.adjdDocType))]
  [PXUIField(DisplayName = "AdjgDocType")]
  public override string AdjgDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (SVATConversionHist.adjgRefNbr))]
  [PXDefault(typeof (SVATConversionHist.adjdRefNbr))]
  [PXUIField(DisplayName = "AdjgRefNbr")]
  public override string AdjgRefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (SVATConversionHist.adjNbr))]
  [PXDefault(-1)]
  [PXUIField(DisplayName = "Adjustment Nbr.")]
  public override int? AdjNbr { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true, BqlField = typeof (SVATConversionHist.taxID))]
  [PXUIField(DisplayName = "Tax ID")]
  public override string TaxID { get; set; }

  [PXString(1, IsFixed = true)]
  [PXDBCalced(typeof (Switch<Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleCA>>, CAAdj.status, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAP>>, PX.Objects.AP.APInvoice.status, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAR>>, PX.Objects.AR.ARInvoice.status>>>>), typeof (string))]
  [PXUIField(DisplayName = "Status")]
  [SVATHistStatus.List]
  public virtual string Status { get; set; }

  [PXString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Type")]
  [SVATHistDocType.List]
  public virtual string DisplayDocType
  {
    [PXDependsOnFields(new System.Type[] {typeof (SVATConversionHistExt.module), typeof (SVATConversionHistExt.adjdDocType)})] get
    {
      return this.Module + this.AdjdDocType;
    }
    set
    {
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "Customer/Vendor", Visible = false)]
  [PXDBCalced(typeof (Switch<Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAP>>, PX.Objects.AP.APInvoice.vendorID, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAR>>, PX.Objects.AR.ARInvoice.customerID>>>), typeof (int))]
  [PXSelector(typeof (Search<BAccountR.bAccountID, Where<BAccountR.bAccountID, Equal<Current<SVATConversionHistExt.displayCounterPartyID>>>>), DescriptionField = typeof (BAccountR.acctName), SubstituteKey = typeof (BAccountR.acctCD))]
  public virtual int? DisplayCounterPartyID { get; set; }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visible = false)]
  [PXDBCalced(typeof (Switch<Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleCA>>, CAAdj.tranDesc, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAP>>, PX.Objects.AP.APInvoice.docDesc, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAR>>, PX.Objects.AR.ARInvoice.docDesc>>>>), typeof (string))]
  public virtual string DisplayDescription { get; set; }

  [PXString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Document Ref. / Customer Order Nbr.", Visible = false)]
  [PXDBCalced(typeof (Switch<Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleCA>>, CAAdj.extRefNbr, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAP>>, PX.Objects.AP.APInvoice.invoiceNbr, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAR>>, PX.Objects.AR.ARInvoice.invoiceNbr>>>>), typeof (string))]
  public virtual string DisplayDocRef { get; set; }

  [PXString(1, IsFixed = true)]
  [PXDBCalced(typeof (IsNull<Switch<Case<Where<SVATConversionHist.taxType, Equal<PX.Objects.TX.TaxType.pendingPurchase>>, PX.Objects.AP.Vendor.sVATInputTaxEntryRefNbr, Case<Where<SVATConversionHist.taxType, Equal<PX.Objects.TX.TaxType.pendingSales>>, PX.Objects.AP.Vendor.sVATOutputTaxEntryRefNbr>>>, VendorSVATTaxEntryRefNbr.manuallyEntered>), typeof (string))]
  public virtual string DisplayTaxEntryRefNbr { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Amount")]
  [PXDBCalced(typeof (Switch<Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleCA>>, CAAdj.tranAmt, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAP>>, PX.Objects.AP.APInvoice.origDocAmt, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAR>>, PX.Objects.AR.ARInvoice.origDocAmt>>>>), typeof (Decimal))]
  public virtual Decimal? DisplayOrigDocAmt { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Balance")]
  [PXDBCalced(typeof (Switch<Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleCA>>, decimal0, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAP>>, PX.Objects.AP.APInvoice.docBal, Case<Where<SVATConversionHist.module, Equal<BatchModule.moduleAR>>, PX.Objects.AR.ARInvoice.docBal>>>>), typeof (Decimal))]
  public virtual Decimal? DisplayDocBal { get; set; }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHistExt.module>
  {
  }

  public new abstract class adjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.adjdDocType>
  {
  }

  public new abstract class adjdRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.adjdRefNbr>
  {
  }

  public new abstract class adjdLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SVATConversionHistExt.adjdLineNbr>
  {
  }

  public new abstract class adjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.adjgDocType>
  {
  }

  public new abstract class adjgRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.adjgRefNbr>
  {
  }

  public new abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SVATConversionHistExt.adjNbr>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHistExt.taxID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SVATConversionHistExt.status>
  {
  }

  public abstract class displayDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.displayDocType>
  {
  }

  public abstract class displayCounterPartyID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SVATConversionHistExt.displayCounterPartyID>
  {
  }

  public abstract class displayDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.displayDescription>
  {
  }

  public abstract class displayDocRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.displayDocRef>
  {
  }

  public abstract class displayTaxEntryRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SVATConversionHistExt.displayTaxEntryRefNbr>
  {
  }

  public abstract class displayOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHistExt.displayOrigDocAmt>
  {
  }

  public abstract class displayDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SVATConversionHistExt.displayDocBal>
  {
  }
}
