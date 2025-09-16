// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.IWorkTimeCalculator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

[PXInternalUseOnly]
public interface IWorkTimeCalculator
{
  /// <summary>
  /// Indicates whether calendar is valid and can be used for calculations.
  /// This might be due to missing workdays or 0 workday hours or other reasons.
  /// Calls to methods of invalid calendar will throw <see cref="T:PX.Data.PXInvalidOperationException" />.
  /// </summary>
  bool IsValid { get; }

  /// <summary>
  /// Validates the calendar and throws <see cref="T:PX.Data.PXInvalidOperationException" /> with the details if it is invalid.
  /// Relies on <see cref="P:PX.Objects.CS.Services.WorkTimeCalculation.IWorkTimeCalculator.IsValid" /> property.
  /// </summary>
  void Validate();

  /// <exception cref="T:PX.Data.PXInvalidOperationException">
  /// Thrown when <see cref="P:PX.Objects.CS.Services.WorkTimeCalculation.IWorkTimeCalculator.IsValid" /> is false.
  /// </exception>
  WorkTimeSpan ToWorkTimeSpan(TimeSpan timeSpan);

  /// <exception cref="T:PX.Data.PXInvalidOperationException">
  /// Thrown when <see cref="P:PX.Objects.CS.Services.WorkTimeCalculation.IWorkTimeCalculator.IsValid" /> is false.
  /// </exception>
  WorkTimeSpan ToWorkTimeSpan(WorkTimeInfo workTimeInfo);

  /// <summary>
  /// Add work time to <paramref name="startDateTime" /> using underlying calendar and return new <see cref="T:PX.Objects.CS.Services.WorkTimeCalculation.DateTimeInfo" /> with result.
  /// </summary>
  /// <param name="startDateTime"></param>
  /// <param name="workTimeDiff"></param>
  /// <returns>Date time info.</returns>
  /// <remarks>Currently time subtraction is not supported.</remarks>
  /// <exception cref="T:PX.Data.PXNotSupportedException">
  /// Thrown when <paramref name="workTimeDiff" /> has negative value.
  /// </exception>
  /// <exception cref="T:PX.Data.PXInvalidOperationException">
  /// Thrown when <see cref="P:PX.Objects.CS.Services.WorkTimeCalculation.IWorkTimeCalculator.IsValid" /> is false.
  /// </exception>
  DateTimeInfo AddWorkTime(DateTimeInfo startDateTime, WorkTimeSpan workTimeDiff);
}
