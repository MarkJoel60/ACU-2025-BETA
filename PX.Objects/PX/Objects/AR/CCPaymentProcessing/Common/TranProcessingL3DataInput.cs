// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.TranProcessingL3DataInput
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>The class that contains the processing Level 3 data.</summary>
public class TranProcessingL3DataInput
{
  /// <summary>
  /// A previously returned transaction_id that is used for Level 3 transactions.
  /// </summary>
  public string TransactionId { get; set; }

  /// <summary>
  /// Code of the country where the goods are being shipped.
  /// </summary>
  public string DestinationCountryCode { get; set; }

  /// <summary>
  /// Fee amount associated with the import of the purchased goods.
  /// </summary>
  public Decimal? DutyAmount { get; set; }

  /// <summary>
  /// Freight or shipping portion of the total transaction amount.
  /// </summary>
  public Decimal? FreightAmount { get; set; }

  /// <summary>National tax for the transaction.</summary>
  public Decimal? NationalTax { get; set; }

  /// <summary>Sales tax for the transaction.</summary>
  public Decimal? SalesTax { get; set; }

  /// <summary>
  /// Postal/ZIP code of the address from where the purchased goods are being shipped.
  /// </summary>
  public string ShipfromZipCode { get; set; }

  /// <summary>
  /// Postal/ZIP code of the address where purchased goods will be delivered.
  /// </summary>
  public string ShiptoZipCode { get; set; }

  /// <summary>Amount of any value added taxes.</summary>
  public Decimal? TaxAmount { get; set; }

  /// <summary>Sales Tax Exempt. Allowed values: "1", "0".</summary>
  public bool TaxExempt { get; set; }

  /// <summary>List of line items.</summary>
  public List<TranProcessingL3DataLineItemInput> LineItems { get; set; }

  /// <summary>
  /// Tax registration number supplied by the Commercial Card cardholder.
  /// </summary>
  public string CustomerVatRegistration { get; set; }

  /// <summary>
  /// Government assigned tax identification number of the Merchant.
  /// </summary>
  public string MerchantVatRegistration { get; set; }

  /// <summary>The purchase order date. Format: "YYMMDD".</summary>
  public string OrderDate { get; set; }

  /// <summary>
  /// International description code of the overall goods or services being supplied.
  /// </summary>
  public string SummaryCommodityCode { get; set; }

  /// <summary>Tax rate used to calculate the sales tax amount.</summary>
  public Decimal? TaxRate { get; set; }

  /// <summary>
  /// Invoice number that is associated with the VAT invoice.
  /// </summary>
  public string UniqueVatRefNumber { get; set; }

  /// <summary>
  /// Type of a card associated with the customer payment method.
  /// </summary>
  public virtual CCCardType CardType { get; set; }
}
