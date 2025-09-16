// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRScheduleOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.DR;

public class DRScheduleOption : ILabelProvider
{
  private static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "S",
      "On Start of Financial Period"
    },
    {
      "E",
      "On End of Financial Period"
    },
    {
      "D",
      "On Fixed Day of Financial Period"
    }
  };
  public const string ScheduleOptionStart = "S";
  public const string ScheduleOptionEnd = "E";
  public const string ScheduleOptionFixedDate = "D";

  public IEnumerable<ValueLabelPair> ValueLabelPairs => DRScheduleOption._valueLabelPairs;
}
