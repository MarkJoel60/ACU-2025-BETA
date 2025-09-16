// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.TransmitterTRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.IO;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// Transmitter Record (T)
/// File format is based on IRS publication 1220 (http://www.irs.gov/pub/irs-pdf/p1220.pdf)
/// </summary>
public class TransmitterTRecord : I1099Record
{
  public string RecordType { get; set; }

  public string PaymentYear { get; set; }

  public string PriorYearDataIndicator { get; set; }

  public string TransmitterTIN { get; set; }

  public string TransmitterControlCode { get; set; }

  public string TestFileIndicator { get; set; }

  public string ForeignEntityIndicator { get; set; }

  public string TransmitterName { get; set; }

  public string CompanyName { get; set; }

  public string CompanyMailingAddress { get; set; }

  public string CompanyCity { get; set; }

  public string CompanyState { get; set; }

  public string CompanyZipCode { get; set; }

  public string TotalNumberofPayees { get; set; }

  public string ContactName { get; set; }

  public string ContactTelephoneAndExt { get; set; }

  public string ContactEmailAddress { get; set; }

  public string RecordSequenceNumber { get; set; }

  public string VendorIndicator { get; set; }

  public string VendorName { get; set; }

  public string VendorMailingAddress { get; set; }

  public string VendorCity { get; set; }

  public string VendorState { get; set; }

  public string VendorZipCode { get; set; }

  public string VendorContactName { get; set; }

  public string VendorContactTelephoneAndExt { get; set; }

  public string VendorForeignEntityIndicator { get; set; }

  void I1099Record.WriteToFile(StreamWriter writer, YearFormat yearFormat)
  {
    StringBuilder str = new StringBuilder(800);
    StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(str, this.RecordType, 1, 1), this.PaymentYear, 2, 4), this.PriorYearDataIndicator, 6, 1).Append(this.TransmitterTIN, 7, 9, regexReplacePattern: "[^0-9]"), this.TransmitterControlCode, 16 /*0x10*/, 5), string.Empty, 21, 7), this.TestFileIndicator, 28, 1), this.ForeignEntityIndicator, 29, 1).Append(this.TransmitterName, 30, 80 /*0x50*/, PaddingEnum.Right).Append(this.CompanyName, 110, 80 /*0x50*/, PaddingEnum.Right).Append(this.CompanyMailingAddress, 190, 40, PaddingEnum.Right).Append(this.CompanyCity, 230, 40, PaddingEnum.Right).Append(this.CompanyState, 270, 2, PaddingEnum.Right).Append(this.CompanyZipCode, 272, 9, PaddingEnum.Right, regexReplacePattern: "[^0-9a-zA-Z]").Append(string.Empty, 281, 15, PaddingEnum.Right).Append(this.TotalNumberofPayees, 296, 8, PaddingEnum.Left, '0'), this.ContactName, 304, 40).Append(this.ContactTelephoneAndExt, 344, 15, PaddingEnum.Right, regexReplacePattern: "[^0-9a-zA-Z]").Append(this.ContactEmailAddress, 359, 50, PaddingEnum.Right, alphaCharacterCaseStyle: AlphaCharacterCaseEnum.None).Append(string.Empty, 409, 91, PaddingEnum.Right).Append(this.RecordSequenceNumber, 500, 8, PaddingEnum.Left, '0').Append(string.Empty, 508, 10, PaddingEnum.Right), this.VendorIndicator, 518, 1), this.VendorName, 519, 40), this.VendorMailingAddress, 559, 40), this.VendorCity, 599, 40), this.VendorState, 639, 2).Append(this.VendorZipCode, 641, 9, regexReplacePattern: "[^0-9a-zA-Z]"), this.VendorContactName, 650, 40).Append(this.VendorContactTelephoneAndExt, 690, 15, regexReplacePattern: "[^0-9a-zA-Z]"), string.Empty, 705, 35), this.VendorForeignEntityIndicator, 740, 1), string.Empty, 741, 8), string.Empty, 749, 2);
    writer.WriteLine((object) str);
  }
}
