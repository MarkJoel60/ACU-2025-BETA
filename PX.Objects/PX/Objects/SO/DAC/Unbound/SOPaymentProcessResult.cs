// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.SOPaymentProcessResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

[PXBreakInheritance]
[PXCacheName("Credit Card Processing for Sales Result")]
[PXVirtual]
public class SOPaymentProcessResult : ARPayment
{
  /// <summary>
  /// Cury increased authorized amount (in a currency of payment)
  /// </summary>
  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (SOPaymentProcessResult.increasedAuthorizedAmount))]
  [PXUIField(DisplayName = "Increased Authorized Amount")]
  public Decimal? CuryIncreasedAuthorizedAmount { get; set; }

  /// <summary>Increased authorized amount (in a base currency)</summary>
  [PXBaseCury]
  public Decimal? IncreasedAuthorizedAmount { get; set; }

  /// <summary>Increased applied amount (in a currency of payment)</summary>
  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (SOPaymentProcessResult.increasedAppliedAmount))]
  [PXUIField(DisplayName = "Increased Applied Amount")]
  public Decimal? CuryIncreasedAppliedAmount { get; set; }

  /// <summary>Increased applied amount (in a base currency)</summary>
  [PXBaseCury]
  public Decimal? IncreasedAppliedAmount { get; set; }

  [PXDateAndTime]
  [PXUIField(DisplayName = "Funds Hold Expiration Date", Enabled = false)]
  public virtual DateTime? FundHoldExpDate { get; set; }

  [PXString]
  [ExtTransactionProcStatusCode.List]
  [PXUIField(DisplayName = "Proc. Status", Enabled = false)]
  public virtual 
  #nullable disable
  string RelatedTranProcessingStatus { get; set; }

  [PXString]
  [SOPaymentProcessResult.relatedDocument.List]
  [PXUIField(DisplayName = "Document Type")]
  public string RelatedDocument { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Document Type")]
  public string RelatedDocumentType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Document Number")]
  public string RelatedDocumentNumber { get; set; }

  [PXString]
  [MultiDocumentStatusList(new Type[] {typeof (PX.Objects.SO.SOOrder.status), typeof (PX.Objects.SO.SOInvoice.status)})]
  [PXUIField(DisplayName = "Document Status")]
  public string RelatedDocumentStatus { get; set; }

  [PXLong]
  [CurrencyInfo]
  public virtual long? RelatedDocumentCuryInfoID { get; set; }

  /// <summary>Related document currency id</summary>
  [PXString(5, IsUnicode = true)]
  public virtual string RelatedDocumentCuryID { get; set; }

  /// <summary>Related document applied amount</summary>
  [PXBaseCury]
  public Decimal? RelatedDocumentAppliedAmount { get; set; }

  /// <summary>Related document applied amount (in a currency)</summary>
  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (SOPaymentProcessResult.relatedDocumentAppliedAmount))]
  [PXUIField(DisplayName = "Applied Amount")]
  public Decimal? CuryRelatedDocumentAppliedAmount { get; set; }

  /// <summary>Related document unpaid amount</summary>
  [PXBaseCury]
  public Decimal? RelatedDocumentUnpaidAmount { get; set; }

  /// <summary>
  /// Related document unpaid amount (in a currency of payment)
  /// </summary>
  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (SOPaymentProcessResult.relatedDocumentUnpaidAmount))]
  public Decimal? CuryRelatedDocumentUnpaidAmount { get; set; }

  [PXString]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  [PXUIField(DisplayName = "Credit Terms")]
  public string RelatedDocumentCreditTerms { get; set; }

  [PXString]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  [PXUIField(DisplayName = "Error Description")]
  public string ErrorDescription { get; set; }

  /// <summary>Related document payment counter</summary>
  [PXInt]
  public int? RelatedDocumentPaymentCntr { get; set; }

  public abstract class curyIncreasedAuthorizedAmount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.curyIncreasedAuthorizedAmount>
  {
  }

  public abstract class increasedAuthorizedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPaymentProcessResult.increasedAuthorizedAmount>
  {
  }

  public abstract class curyIncreasedAppliedAmount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.curyIncreasedAppliedAmount>
  {
  }

  public abstract class increasedAppliedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPaymentProcessResult.increasedAppliedAmount>
  {
  }

  public abstract class fundHoldExpDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPaymentProcessResult.fundHoldExpDate>
  {
  }

  public abstract class relatedTranProcessingStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedTranProcessingStatus>
  {
  }

  public abstract class relatedDocument : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocument>
  {
    public class ListAttribute : PXStringListAttribute
    {
      public const string SalesOrder = "SalesOrder";
      public const string Invoice = "Invoice";

      public ListAttribute()
        : base(new string[2]
        {
          nameof (SalesOrder),
          nameof (Invoice)
        }, new string[2]{ "Sales Order", nameof (Invoice) })
      {
      }
    }
  }

  public abstract class relatedDocumentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentType>
  {
  }

  public abstract class relatedDocumentNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentNumber>
  {
  }

  public abstract class relatedDocumentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentNumber>
  {
  }

  public abstract class relatedDocumentCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentCuryInfoID>
  {
  }

  public abstract class relatedDocumentCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentCuryID>
  {
  }

  public abstract class relatedDocumentAppliedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentAppliedAmount>
  {
  }

  public abstract class curyRelatedDocumentAppliedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPaymentProcessResult.curyRelatedDocumentAppliedAmount>
  {
  }

  public abstract class relatedDocumentUnpaidAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentUnpaidAmount>
  {
  }

  public abstract class curyRelatedDocumentUnpaidAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOPaymentProcessResult.curyRelatedDocumentUnpaidAmount>
  {
  }

  public abstract class relatedDocumentCreditTerms : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentCreditTerms>
  {
  }

  public abstract class errorDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.errorDescription>
  {
  }

  public abstract class relatedDocumentPaymentCntr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPaymentProcessResult.relatedDocumentPaymentCntr>
  {
  }
}
