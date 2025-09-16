// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.StateTotalsRecordK
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
/// State Totals Record (K)
/// File format is based on IRS publication 1220 (http://www.irs.gov/pub/irs-pdf/p1220.pdf)
/// </summary>
public class StateTotalsRecordK : I1099Record
{
  public string RecordType { get; set; }

  public string NumberOfPayees { get; set; }

  public Decimal ControlTotal1 { get; set; }

  public Decimal ControlTotal2 { get; set; }

  public Decimal ControlTotal3 { get; set; }

  public Decimal ControlTotal4 { get; set; }

  public Decimal ControlTotal5 { get; set; }

  public Decimal ControlTotal6 { get; set; }

  public Decimal ControlTotal7 { get; set; }

  public Decimal ControlTotal8 { get; set; }

  public Decimal ControlTotal9 { get; set; }

  public Decimal ControlTotalA { get; set; }

  public Decimal ControlTotalB { get; set; }

  public Decimal ControlTotalC { get; set; }

  public Decimal ControlTotalD { get; set; }

  public Decimal ControlTotalE { get; set; }

  public Decimal ControlTotalF { get; set; }

  public Decimal ControlTotalG { get; set; }

  public Decimal ControlTotalH { get; set; }

  public Decimal ControlTotalJ { get; set; }

  public string RecordSequenceNumber { get; set; }

  public Decimal StateIncomeTaxWithheldTotal { get; set; }

  public Decimal LocalIncomeTaxWithheldTotal { get; set; }

  public string CombinedFederalOrStateCode { get; set; }

  void I1099Record.WriteToFile(StreamWriter writer, YearFormat yearFormat)
  {
    StringBuilder str = new StringBuilder(800);
    if (yearFormat == YearFormat.F2020)
      StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(str, this.RecordType, 1, 1).Append(this.NumberOfPayees, 2, 8, PaddingEnum.Left, '0'), string.Empty, 10, 6).Append(this.ControlTotal1, 16 /*0x10*/, 18, PaddingEnum.Left, '0').Append(this.ControlTotal2, 34, 18, PaddingEnum.Left, '0').Append(this.ControlTotal3, 52, 18, PaddingEnum.Left, '0').Append(this.ControlTotal4, 70, 18, PaddingEnum.Left, '0').Append(this.ControlTotal5, 88, 18, PaddingEnum.Left, '0').Append(this.ControlTotal6, 106, 18, PaddingEnum.Left, '0').Append(this.ControlTotal7, 124, 18, PaddingEnum.Left, '0').Append(this.ControlTotal8, 142, 18, PaddingEnum.Left, '0').Append(this.ControlTotal9, 160 /*0xA0*/, 18, PaddingEnum.Left, '0').Append(this.ControlTotalA, 178, 18, PaddingEnum.Left, '0').Append(this.ControlTotalB, 196, 18, PaddingEnum.Left, '0').Append(this.ControlTotalC, 214, 18, PaddingEnum.Left, '0').Append(this.ControlTotalD, 232, 18, PaddingEnum.Left, '0').Append(this.ControlTotalE, 250, 18, PaddingEnum.Left, '0').Append(this.ControlTotalF, 268, 18, PaddingEnum.Left, '0').Append(this.ControlTotalG, 286, 18, PaddingEnum.Left, '0'), string.Empty, 304, 196).Append(this.RecordSequenceNumber, 500, 8, PaddingEnum.Left, '0'), string.Empty, 508, 199).Append(this.StateIncomeTaxWithheldTotal, 707, 18, PaddingEnum.Left, '0').Append(this.LocalIncomeTaxWithheldTotal, 725, 18, PaddingEnum.Left, '0'), string.Empty, 743, 4), this.CombinedFederalOrStateCode, 747, 2), string.Empty, 749, 2);
    else
      StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(str, this.RecordType, 1, 1).Append(this.NumberOfPayees, 2, 8, PaddingEnum.Left, '0'), string.Empty, 10, 6).Append(this.ControlTotal1, 16 /*0x10*/, 18, PaddingEnum.Left, '0').Append(this.ControlTotal2, 34, 18, PaddingEnum.Left, '0').Append(this.ControlTotal3, 52, 18, PaddingEnum.Left, '0').Append(this.ControlTotal4, 70, 18, PaddingEnum.Left, '0').Append(this.ControlTotal5, 88, 18, PaddingEnum.Left, '0').Append(this.ControlTotal6, 106, 18, PaddingEnum.Left, '0').Append(this.ControlTotal7, 124, 18, PaddingEnum.Left, '0').Append(this.ControlTotal8, 142, 18, PaddingEnum.Left, '0').Append(this.ControlTotal9, 160 /*0xA0*/, 18, PaddingEnum.Left, '0').Append(this.ControlTotalA, 178, 18, PaddingEnum.Left, '0').Append(this.ControlTotalB, 196, 18, PaddingEnum.Left, '0').Append(this.ControlTotalC, 214, 18, PaddingEnum.Left, '0').Append(this.ControlTotalD, 232, 18, PaddingEnum.Left, '0').Append(this.ControlTotalE, 250, 18, PaddingEnum.Left, '0').Append(this.ControlTotalF, 268, 18, PaddingEnum.Left, '0').Append(this.ControlTotalG, 286, 18, PaddingEnum.Left, '0').Append(this.ControlTotalH, 304, 18, PaddingEnum.Left, '0').Append(this.ControlTotalJ, 322, 18, PaddingEnum.Left, '0'), string.Empty, 340, 160 /*0xA0*/).Append(this.RecordSequenceNumber, 500, 8, PaddingEnum.Left, '0'), string.Empty, 508, 199).Append(this.StateIncomeTaxWithheldTotal, 707, 18, PaddingEnum.Left, '0').Append(this.LocalIncomeTaxWithheldTotal, 725, 18, PaddingEnum.Left, '0'), string.Empty, 743, 4), this.CombinedFederalOrStateCode, 747, 2), string.Empty, 749, 2);
    writer.WriteLine((object) str);
  }
}
