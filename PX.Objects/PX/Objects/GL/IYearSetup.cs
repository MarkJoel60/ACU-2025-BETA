// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.IYearSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.GL;

public interface IYearSetup
{
  string FirstFinYear { get; set; }

  DateTime? BegFinYear { get; set; }

  DateTime? PeriodsStartDate { get; set; }

  short? FinPeriods { get; set; }

  short? PeriodLength { get; set; }

  bool? UserDefined { get; set; }

  string PeriodType { get; set; }

  bool? HasAdjustmentPeriod { get; set; }

  bool? BelongsToNextYear { get; set; }

  FiscalPeriodSetupCreator.FPType FPType { get; }

  bool IsFixedLengthPeriod { get; }

  string EndYearCalcMethod { get; set; }

  int? EndYearDayOfWeek { get; set; }
}
