// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectRevenueBudgetDefaultingExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM.GraphExtensions.AccountGroupDefaulting;

#nullable disable
namespace PX.Objects.PM;

public class ProjectRevenueBudgetDefaultingExt : 
  AccountGroupDefaultingExt<ProjectEntry, PMRevenueBudget>
{
  protected override string InventoryFieldName => "inventoryID";

  protected override string ProjectFieldName => "projectID";

  protected override string AccountGroupFieldName => "accountGroupID";

  protected override string GetDefaultAccountType() => "I";

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
}
