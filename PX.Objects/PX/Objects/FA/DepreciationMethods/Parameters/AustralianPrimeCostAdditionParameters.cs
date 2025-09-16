// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.Parameters.AustralianPrimeCostAdditionParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.FA.DepreciationMethods.Parameters;

public class AustralianPrimeCostAdditionParameters : AdditionParameters
{
  public DateTime PlacedInServiceDate;
  /// <summary>The period in which the depreciation is starting</summary>
  public FABookPeriod DepreciateFromPeriod;
  /// <summary>The period in which the depreciation is ending</summary>
  public FABookPeriod DepreciateToPeriod;
  /// <summary>
  /// Number of days in the first period (<see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AustralianPrimeCostAdditionParameters.DepreciateFromPeriod" />) in which the fixed asset is depreciated.
  /// This is the difference between the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AdditionParameters.DepreciateFromDate" /> and the end of the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AustralianPrimeCostAdditionParameters.DepreciateFromPeriod" />
  /// </summary>
  public double DaysHeldInFromPeriod;
  /// <summary>
  /// Number of days in the last period (<see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AustralianPrimeCostAdditionParameters.DepreciateToPeriod" />) in which the fixed asset is depreciated.
  /// This is the difference between the start of the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AustralianPrimeCostAdditionParameters.DepreciateToPeriod" /> and the <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AdditionParameters.DepreciateToDate" />
  /// </summary>
  public double DaysHeldInToPeriod;
  /// <summary>
  /// Number of total days in <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AustralianPrimeCostAdditionParameters.DepreciateFromPeriod" />
  /// </summary>
  public double TotalDaysInFromPeriod;
  /// <summary>
  /// Number of total days in <see cref="F:PX.Objects.FA.DepreciationMethods.Parameters.AustralianPrimeCostAdditionParameters.DepreciateToPeriod" />
  /// </summary>
  public double TotalDaysInToPeriod;
  public Decimal PercentPerYear;
}
