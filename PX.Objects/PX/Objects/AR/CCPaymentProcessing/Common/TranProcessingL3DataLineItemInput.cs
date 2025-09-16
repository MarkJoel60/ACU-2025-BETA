// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.TranProcessingL3DataLineItemInput
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>
/// The class that contains the processing Level 3 data of line item.
/// </summary>
public class TranProcessingL3DataLineItemInput
{
  /// <summary>Description of the item.</summary>
  public string Description { get; set; }

  /// <summary>
  /// Total discount amount applied against the line item total.
  /// </summary>
  public Decimal? DiscountAmount { get; set; }

  /// <summary>Merchant-defined description code of the item.</summary>
  public string ProductCode { get; set; }

  /// <summary>Quantity of the item.</summary>
  public Decimal? Quantity { get; set; }

  /// <summary>Amount of any value added taxes.</summary>
  public Decimal? TaxAmount { get; set; }

  /// <summary>Tax rate used to calculate the sales tax amount.</summary>
  public Decimal? TaxRate { get; set; }

  /// <summary>Units of measurement as used in international trade.</summary>
  public string UnitCode { get; set; }

  /// <summary>Unit cost of the item.</summary>
  public Decimal UnitCost { get; set; }

  /// <summary>
  /// An international description code of the individual good or service being supplied.
  /// </summary>
  public string CommodityCode { get; set; }

  /// <summary>
  /// Used if city or multiple county taxes need to be broken out separately.
  /// </summary>
  public Decimal? OtherTaxAmount { get; set; }

  /// <summary>
  /// Tax identification number of the merchant that reported the alternate tax amount.
  /// </summary>
  public string AlternateTaxId { get; set; }

  /// <summary>
  /// Indicator used to reflect debit (D) or credit (C) transaction. Allowed values: "D", "C".
  /// </summary>
  public string DebitCredit { get; set; }

  /// <summary>Discount rate for the line item</summary>
  public Decimal? DiscountRate { get; set; }

  /// <summary>
  /// Type of value-added taxes that are being used (Conditional If tax amount is supplied)
  /// This field is only required when Merchant is directed to include by Mastercard.
  /// </summary>
  public string TaxTypeApplied { get; set; }

  /// <summary>
  /// Indicates the type of tax collected in relationship to a specific tax amount (Conditional If tax amount is supplied).
  /// </summary>
  public string TaxTypeId { get; set; }
}
