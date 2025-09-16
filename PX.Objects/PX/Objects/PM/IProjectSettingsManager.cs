// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.IProjectSettingsManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.PM;

public interface IProjectSettingsManager
{
  int NonProjectID { get; }

  int EmptyInventoryID { get; }

  string CostBudgetUpdateMode { get; }

  string RevenueBudgetUpdateMode { get; }

  bool CalculateProjectSpecificTaxes { get; }

  bool CostCommitmentTracking { get; }

  bool IsPMVisible(string module);
}
