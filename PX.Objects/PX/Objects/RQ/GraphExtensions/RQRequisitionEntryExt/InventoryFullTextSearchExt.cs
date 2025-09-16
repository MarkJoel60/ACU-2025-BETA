// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.GraphExtensions.RQRequisitionEntryExt.InventoryFullTextSearchExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.RQ.GraphExtensions.RQRequisitionEntryExt;

public class InventoryFullTextSearchExt : 
  InventoryFullTextSearchExtBase<RQRequisitionEntry, RQRequisitionLine, RQRequisitionLine.inventoryID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>();
}
