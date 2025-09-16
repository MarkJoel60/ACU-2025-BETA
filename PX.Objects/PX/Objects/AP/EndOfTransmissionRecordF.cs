// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.EndOfTransmissionRecordF
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.IO;
using System.Text;

#nullable disable
namespace PX.Objects.AP;

/// <summary>
/// End of Transmission Record (F)
/// File format is based on IRS publication 1220 (http://www.irs.gov/pub/irs-pdf/p1220.pdf)
/// </summary>
public class EndOfTransmissionRecordF : I1099Record
{
  public string RecordType { get; set; }

  public string NumberOfARecords { get; set; }

  public string TotalNumberOfPayees { get; set; }

  public string RecordSequenceNumber { get; set; }

  void I1099Record.WriteToFile(StreamWriter writer, YearFormat yearFormat)
  {
    StringBuilder str = new StringBuilder(800);
    StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(StringBuilderExtensions.Append(str, this.RecordType, 1, 1).Append(this.NumberOfARecords, 2, 8, PaddingEnum.Left, '0').Append(string.Empty, 10, 21, paddingChar: '0'), string.Empty, 31 /*0x1F*/, 19).Append(this.TotalNumberOfPayees, 50, 8, PaddingEnum.Left, '0'), string.Empty, 58, 442).Append(this.RecordSequenceNumber, 500, 8, PaddingEnum.Left, '0'), string.Empty, 508, 241), string.Empty, 749, 2);
    writer.WriteLine((object) str);
  }
}
