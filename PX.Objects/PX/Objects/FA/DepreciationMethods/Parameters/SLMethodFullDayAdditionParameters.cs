// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.FA.DepreciationMethods.Parameters;

public class SLMethodFullDayAdditionParameters : SLMethodAdditionParameters
{
  /// <summary>The period in which the depreciation is starting</summary>
  public FABookPeriod DepreciateFromPeriod;
  /// <summary>The period in which the depreciation is ending</summary>
  public FABookPeriod DepreciateToPeriod;
  /// <summary>
  /// Number of days in the first period (<see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateFromPeriod" />) in which the fixed asset is depreciated.
  /// This is the difference between the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AdditionParameters.DepreciateFromDate" /> and the end of the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateFromPeriod" />
  /// </summary>
  public double DaysHeldInFromPeriod;
  /// <summary>
  /// Number of days in the last period (<see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateToPeriod" />) in which the fixed asset is depreciated.
  /// This is the difference between the start of the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateToPeriod" /> and the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AdditionParameters.DepreciateToDate" />
  /// </summary>
  public double DaysHeldInToPeriod;
  /// <summary>
  /// Number of total days in <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateFromPeriod" />
  /// </summary>
  public double TotalDaysInFromPeriod;
  /// <summary>
  /// Number of total days in <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateToPeriod" />
  /// </summary>
  public double TotalDaysInToPeriod;
  /// <summary>
  /// True when depreciation starts from the start of the first period
  /// (<see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AdditionParameters.DepreciateFromDate" /> is a first date of the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateFromPeriod" />)
  /// </summary>
  public bool IsFirstPeriodFull;
  /// <summary>
  /// True when the ddition is an original acquisition (the addition Period is the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.SLMethodFullDayAdditionParameters.DepreciateFromPeriod" />)
  /// </summary>
  public bool IsFirstAddition;
}
