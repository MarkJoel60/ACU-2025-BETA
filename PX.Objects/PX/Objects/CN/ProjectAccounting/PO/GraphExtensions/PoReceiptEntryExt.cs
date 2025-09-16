// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PO.GraphExtensions.PoReceiptEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PO.GraphExtensions;

public class PoReceiptEntryExt : PXGraphExtension<POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new Type[] {})]
  [PXFormula(typeof (Validate<POReceiptLine.projectID, POReceiptLine.costCodeID, POReceiptLine.inventoryID, POReceiptLine.siteID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.taskID> e)
  {
  }
}
