// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PayerRecordA
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.IO;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Payer Record (A)
/// File format is based on IRS publication 1220 (http://www.irs.gov/pub/irs-pdf/p1220.pdf)
/// </summary>
public class PayerRecordA : I1099Record
{
  public string RecordType { get; set; }

  public string PaymentYear { get; set; }

  public string CombinedFederalORStateFiler { get; set; }

  public string PayerTaxpayerIdentificationNumberTIN { get; set; }

  public string PayerNameControl { get; set; }

  public string LastFilingIndicator { get; set; }

  public string TypeofReturn { get; set; }

  public string AmountCodes { get; set; }

  public string ForeignEntityIndicator { get; set; }

  public string FirstPayerNameLine { get; set; }

  public string SecondPayerNameLine { get; set; }

  public string TransferAgentIndicator { get; set; }

  public string PayerShippingAddress { get; set; }

  public string PayerCity { get; set; }

  public string PayerState { get; set; }

  public string PayerZipCode { get; set; }

  public string PayerTelephoneAndExt { get; set; }

  public string RecordSequenceNumber { get; set; }

  void I1099Record.WriteToFile(StreamWriter writer, YearFormat yearFormat)
  {
    StringBuilder str = new StringBuilder(800);
    StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(str, this.RecordType, 1, 1), this.PaymentYear, 2, 4), this.CombinedFederalORStateFiler, 6, 1), string.Empty, 7, 5).Append(this.PayerTaxpayerIdentificationNumberTIN, 12, 9, regexReplacePattern: "[^0-9]").Append(this.PayerNameControl, 21, 4, regexReplacePattern: "[^0-9a-zA-Z]"), this.LastFilingIndicator, 25, 1), this.TypeofReturn, 26, 2), this.AmountCodes, 28, 16 /*0x10*/), string.Empty, 44, 8), this.ForeignEntityIndicator, 52, 1), this.FirstPayerNameLine, 53, 40), this.SecondPayerNameLine, 93, 40), this.TransferAgentIndicator, 133, 1), this.PayerShippingAddress, 134, 40), this.PayerCity, 174, 40), this.PayerState, 214, 2).Append(this.PayerZipCode, 216, 9, regexReplacePattern: "[^0-9a-zA-Z]").Append(this.PayerTelephoneAndExt, 225, 15, regexReplacePattern: "[^0-9a-zA-Z]"), string.Empty, 240 /*0xF0*/, 260).Append(this.RecordSequenceNumber, 500, 8, PaddingEnum.Left, '0'), string.Empty, 508, 241), string.Empty, 749, 2);
    writer.WriteLine((object) str);
  }
}
