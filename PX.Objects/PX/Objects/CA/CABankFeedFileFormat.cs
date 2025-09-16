// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedFileFormat
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public static class CABankFeedFileFormat
{
  public const string Csv = "C";
  public const string Bai = "B";
  private static (string, string)[] codes = new (string, string)[2]
  {
    ("C", "CSV (Comma-Separated Values)"),
    ("B", "BAI2 (Bank Administration Institute)")
  };

  internal static string GetCABankFeedFileFormatName(string caBankFeedFileFormatCode)
  {
    if (!((IEnumerable<(string, string)>) CABankFeedFileFormat.codes).Where<(string, string)>((Func<(string, string), bool>) (i => i.Item1.Equals(caBankFeedFileFormatCode))).Any<(string, string)>())
      throw new InvalidOperationException();
    return ((IEnumerable<(string, string)>) CABankFeedFileFormat.codes).Where<(string, string)>((Func<(string, string), bool>) (i => i.Item1.Equals(caBankFeedFileFormatCode))).First<(string, string)>().Item2;
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CABankFeedFileFormat.ListAttribute.GetTypes)
    {
    }

    public static (string, string)[] GetTypes => CABankFeedFileFormat.codes;
  }
}
