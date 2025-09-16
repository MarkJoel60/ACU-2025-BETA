// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PayeeRecordB
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.IO;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Payee Record (B)
/// File format is based on IRS publication 1220 (http://www.irs.gov/pub/irs-pdf/p1220.pdf)
/// </summary>
public class PayeeRecordB : I1099Record
{
  public string RecordType { get; set; }

  public string PaymentYear { get; set; }

  public string CorrectedReturnIndicator { get; set; }

  public string NameControl { get; set; }

  public string TypeOfTIN { get; set; }

  public string PayerTaxpayerIdentificationNumberTIN { get; set; }

  public string PayerAccountNumberForPayee { get; set; }

  public string PayerOfficeCode { get; set; }

  public Decimal PaymentAmount1 { get; set; }

  public Decimal PaymentAmount2 { get; set; }

  public Decimal PaymentAmount3 { get; set; }

  public Decimal PaymentAmount4 { get; set; }

  public Decimal PaymentAmount5 { get; set; }

  public Decimal PaymentAmount6 { get; set; }

  public Decimal PaymentAmount7 { get; set; }

  public Decimal PaymentAmount8 { get; set; }

  public Decimal PaymentAmount9 { get; set; }

  public Decimal PaymentAmountA { get; set; }

  public Decimal PaymentAmountB { get; set; }

  public Decimal PaymentAmountC { get; set; }

  public Decimal Payment { get; set; }

  public Decimal PaymentAmountE { get; set; }

  public Decimal PaymentAmountF { get; set; }

  public Decimal PaymentAmountG { get; set; }

  public Decimal PaymentAmountH { get; set; }

  public Decimal PaymentAmountJ { get; set; }

  public string ForeignCountryIndicator { get; set; }

  public string PayeeNameLine { get; set; }

  public string PayeeMailingAddress { get; set; }

  public string PayeeCity { get; set; }

  public string PayeeState { get; set; }

  public string PayeeZipCode { get; set; }

  public string RecordSequenceNumber { get; set; }

  public string SecondTINNotice { get; set; }

  public string DirectSalesIndicator { get; set; }

  public string FATCA { get; set; }

  public string SpecialDataEntries { get; set; }

  public string StateIncomeTaxWithheld { get; set; }

  public string LocalIncomeTaxWithheld { get; set; }

  public string CombineFederalOrStateCode { get; set; }

  void I1099Record.WriteToFile(StreamWriter writer, YearFormat yearFormat)
  {
    StringBuilder str = new StringBuilder(800);
    switch (yearFormat)
    {
      case YearFormat.F2020:
        StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(str, this.RecordType, 1, 1), this.PaymentYear, 2, 4), this.CorrectedReturnIndicator, 6, 1).Append(this.NameControl, 7, 4, regexReplacePattern: "[^0-9a-zA-Z]"), this.TypeOfTIN, 11, 1).Append(this.PayerTaxpayerIdentificationNumberTIN, 12, 9, regexReplacePattern: "[^0-9]"), this.PayerAccountNumberForPayee, 21, 20), this.PayerOfficeCode, 41, 4), string.Empty, 45, 10).Append(this.PaymentAmount1, 55, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount2, 67, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount3, 79, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount4, 91, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount5, 103, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount6, 115, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount7, (int) sbyte.MaxValue, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount8, 139, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount9, 151, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountA, 163, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountB, 175, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountC, 187, 12, PaddingEnum.Left, '0').Append(this.Payment, 199, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountE, 211, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountF, 223, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountG, 235, 12, PaddingEnum.Left, '0'), this.ForeignCountryIndicator, 247, 1).Append(this.PayeeNameLine, 248, 80 /*0x50*/, regexReplacePattern: "[^0-9a-zA-Z-& ]"), string.Empty, 328, 40), this.PayeeMailingAddress, 368, 40), string.Empty, 408, 40), this.PayeeCity, 448, 40), this.PayeeState, 488, 2).Append(this.PayeeZipCode, 490, 9, regexReplacePattern: "[^0-9a-zA-Z]"), string.Empty, 499, 1).Append(this.RecordSequenceNumber, 500, 8, PaddingEnum.Left, '0'), string.Empty, 508, 36), this.SecondTINNotice, 544, 1), string.Empty, 545, 2), this.DirectSalesIndicator, 547, 1), this.FATCA, 548, 1), string.Empty, 549, 114), this.SpecialDataEntries, 663, 60).Append(this.StateIncomeTaxWithheld, 723, 12, PaddingEnum.Left, '0').Append(this.LocalIncomeTaxWithheld, 735, 12, PaddingEnum.Left, '0'), this.CombineFederalOrStateCode, 747, 2), string.Empty, 749, 2);
        break;
      case YearFormat.F2021:
        StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(str, this.RecordType, 1, 1), this.PaymentYear, 2, 4), this.CorrectedReturnIndicator, 6, 1).Append(this.NameControl, 7, 4, regexReplacePattern: "[^0-9a-zA-Z]"), this.TypeOfTIN, 11, 1).Append(this.PayerTaxpayerIdentificationNumberTIN, 12, 9, regexReplacePattern: "[^0-9]"), this.PayerAccountNumberForPayee, 21, 20), this.PayerOfficeCode, 41, 4), string.Empty, 45, 10).Append(this.PaymentAmount1, 55, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount2, 67, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount3, 79, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount4, 91, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount5, 103, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount6, 115, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount7, (int) sbyte.MaxValue, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount8, 139, 12, PaddingEnum.Left, '0').Append(this.PaymentAmount9, 151, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountA, 163, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountB, 175, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountC, 187, 12, PaddingEnum.Left, '0').Append(this.Payment, 199, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountE, 211, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountF, 223, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountG, 235, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountH, 247, 12, PaddingEnum.Left, '0').Append(this.PaymentAmountJ, 259, 12, PaddingEnum.Left, '0'), string.Empty, 271, 16 /*0x10*/), this.ForeignCountryIndicator, 287, 1).Append(this.PayeeNameLine, 288, 80 /*0x50*/, regexReplacePattern: "[^0-9a-zA-Z-& ]"), this.PayeeMailingAddress, 368, 40), string.Empty, 408, 40), this.PayeeCity, 448, 40), this.PayeeState, 488, 2).Append(this.PayeeZipCode, 490, 9, regexReplacePattern: "[^0-9a-zA-Z]"), string.Empty, 499, 1).Append(this.RecordSequenceNumber, 500, 8, PaddingEnum.Left, '0'), string.Empty, 508, 36), this.SecondTINNotice, 544, 1), string.Empty, 545, 2), this.DirectSalesIndicator, 547, 1), this.FATCA, 548, 1), string.Empty, 549, 114), this.SpecialDataEntries, 663, 60).Append(this.StateIncomeTaxWithheld, 723, 12, PaddingEnum.Left, '0').Append(this.LocalIncomeTaxWithheld, 735, 12, PaddingEnum.Left, '0'), this.CombineFederalOrStateCode, 747, 2), string.Empty, 749, 2);
        break;
    }
    writer.WriteLine((object) str);
  }
}
