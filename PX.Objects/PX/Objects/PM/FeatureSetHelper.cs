// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.FeatureSetHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public static class FeatureSetHelper
{
  private const string CurrentProjectFeatureSetSlotName = "CurrentProjectFeatureSetSlotName";
  private static Dictionary<ProjectFeatureSet, Type> ProjectFeatureMapping = new Dictionary<ProjectFeatureSet, Type>()
  {
    [ProjectFeatureSet.ProjectAccounting] = typeof (FeaturesSet.projectAccounting),
    [ProjectFeatureSet.ChangeOrders] = typeof (FeaturesSet.changeOrder),
    [ProjectFeatureSet.ChangeRequests] = typeof (FeaturesSet.changeRequest),
    [ProjectFeatureSet.Construction] = typeof (FeaturesSet.construction),
    [ProjectFeatureSet.ProjectManagement] = typeof (FeaturesSet.constructionProjectManagement),
    [ProjectFeatureSet.Inventory] = typeof (FeaturesSet.distributionModule),
    [ProjectFeatureSet.ProjectSpecificInventory] = typeof (FeaturesSet.materialManagement),
    [ProjectFeatureSet.Manufacturing] = typeof (FeaturesSet.manufacturing)
  };
  private static Dictionary<string, PXAccess.FeatureInfo> AllFeaturesInfo = PXAccess.GetAllFeaturesInfo().ToDictionary<PXAccess.FeatureInfo, string, PXAccess.FeatureInfo>((Func<PXAccess.FeatureInfo, string>) (x => x.ID.ToLower()), (Func<PXAccess.FeatureInfo, PXAccess.FeatureInfo>) (x => x));

  public static ProjectFeatureSet GetCurrentProjectFeatureSet()
  {
    ProjectFeatureSet? slot = PXContext.GetSlot<ProjectFeatureSet?>("CurrentProjectFeatureSetSlotName");
    if (slot.HasValue && slot.Value != ProjectFeatureSet.None)
      return slot.Value;
    ProjectFeatureSet projectFeatureSet = ProjectFeatureSet.None;
    foreach (KeyValuePair<ProjectFeatureSet, Type> keyValuePair in FeatureSetHelper.ProjectFeatureMapping)
    {
      if (PXAccess.FeatureInstalled(keyValuePair.Value.FullName))
        projectFeatureSet |= keyValuePair.Key;
    }
    PXContext.SetSlot<ProjectFeatureSet?>("CurrentProjectFeatureSetSlotName", new ProjectFeatureSet?(projectFeatureSet));
    return projectFeatureSet;
  }

  public static bool IsProjectFeatureDisabled(ProjectFeatureSet feature)
  {
    return !FeatureSetHelper.IsProjectFeatureEnabled(feature);
  }

  public static bool IsProjectFeatureEnabled(ProjectFeatureSet feature)
  {
    return FeatureSetHelper.IsProjectFeatureIn(FeatureSetHelper.GetCurrentProjectFeatureSet(), feature);
  }

  public static bool IsProjectFeatureIn(ProjectFeatureSet featureSet, ProjectFeatureSet feature)
  {
    return (featureSet & feature) == feature;
  }

  public static IEnumerable<PXAccess.FeatureInfo> GetDisabledFeaturesInfo(
    ProjectFeatureSet featureSet)
  {
    foreach (KeyValuePair<ProjectFeatureSet, Type> keyValuePair in FeatureSetHelper.ProjectFeatureMapping)
    {
      PXAccess.FeatureInfo featureInfo;
      if (FeatureSetHelper.IsProjectFeatureIn(featureSet, keyValuePair.Key) && !PXAccess.FeatureInstalled(keyValuePair.Value.FullName) && FeatureSetHelper.AllFeaturesInfo.TryGetValue(keyValuePair.Value.FullName.ToLower(), out featureInfo))
        yield return featureInfo;
    }
  }
}
