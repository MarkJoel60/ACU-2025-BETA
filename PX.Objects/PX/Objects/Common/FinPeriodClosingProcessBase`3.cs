// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.FinPeriodClosingProcessBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

public abstract class FinPeriodClosingProcessBase<TGraph, TSubledgerClosedFlagField, TFeatureField> : 
  FinPeriodClosingProcessBase<TGraph, TSubledgerClosedFlagField>
  where TGraph : PXGraph
  where TSubledgerClosedFlagField : IBqlField
  where TFeatureField : IBqlField
{
  protected override string FeatureName => typeof (TFeatureField).FullName;
}
