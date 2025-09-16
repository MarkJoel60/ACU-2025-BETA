// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportRange
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public abstract class RMReportRange
{
  public static void ParseRangeStartEndPair(string range, out string start, out string end)
  {
    if (string.IsNullOrEmpty(range))
    {
      start = string.Empty;
      end = string.Empty;
    }
    else
    {
      string[] strArray = range.Split(':');
      if (strArray.Length == 2)
      {
        start = strArray[0];
        end = strArray[1];
      }
      else
      {
        start = strArray.Length == 1 ? strArray[0] : throw new ArgumentException(PXMessages.LocalizeFormatNoPrefixNLA("The range is invalid: {0}.", new object[1]
        {
          (object) range
        }));
        end = start;
      }
    }
  }
}
