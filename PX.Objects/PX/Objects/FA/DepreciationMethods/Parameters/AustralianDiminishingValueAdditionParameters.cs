// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationMethods.Parameters.AustralianDiminishingValueAdditionParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.FA.DepreciationMethods.Parameters;

public class AustralianDiminishingValueAdditionParameters : AdditionParameters
{
  /// <summary>The period in which the depreciation is starting</summary>
  public FABookPeriod DepreciateFromPeriod;
  /// <summary>The period in which the depreciation is ending</summary>
  public FABookPeriod DepreciateToPeriod;
  /// <summary>True when the adition is an original acquisition</summary>
  public bool IsFirstAddition;
}
