// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.GraphExtensions.TemplateGlobalTaskMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.GraphExtensions;

public class TemplateGlobalTaskMaintExt : PXGraphExtension<TemplateGlobalTaskMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected virtual void _(Events.RowInserting<PMTask> args)
  {
    PMTask row = args.Row;
    if (row == null)
      return;
    row.Type = "CostRev";
  }
}
