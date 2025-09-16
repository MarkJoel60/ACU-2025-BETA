// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Helpers.FeaturesSetHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CN.Common.Helpers;

public static class FeaturesSetHelper
{
  private const string FeatureRequiredError = "Feature '{0}' should be enabled";
  private const string ProcoreIntegrationFeatureName = "Procore Integration";
  private const string ConstructionFeatureName = "Construction";
  private const string ProjectManagementFeatureName = "Construction Project Management";

  public static void CheckProcoreIntegrationFeature()
  {
    FeaturesSetHelper.CheckFeature<FeaturesSet.procoreIntegration>("Procore Integration");
  }

  public static void CheckConstructionFeature()
  {
    FeaturesSetHelper.CheckFeature<FeaturesSet.construction>("Construction");
  }

  private static void CheckFeature<TFeature>(string featureName) where TFeature : IBqlField
  {
    if (!PXAccess.FeatureInstalled<TFeature>())
      throw new Exception($"Feature '{featureName}' should be enabled");
  }
}
