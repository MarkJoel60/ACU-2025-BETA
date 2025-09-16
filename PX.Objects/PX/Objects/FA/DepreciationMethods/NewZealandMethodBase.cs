// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.NewZealandMethodBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods;

public abstract class NewZealandMethodBase : DepreciationMethodBase
{
  protected virtual Decimal GetDaysHeldInPeriod(FABookPeriod fABookPeriod, IYearSetup yearSetup)
  {
    return fABookPeriod.StartDate.Value.DayOfYear == 32 /*0x20*/ && yearSetup.PeriodType == "MO" ? 28M : (Decimal) (fABookPeriod.EndDate.Value - fABookPeriod.StartDate.Value).TotalDays;
  }
}
