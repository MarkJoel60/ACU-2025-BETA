// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.RelationGroupsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.PM;

public class RelationGroupsExt : PXGraphExtension<RelationGroups>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXMergeAttributes]
  [PXDBInt]
  protected void _(PX.Data.Events.CacheAttached<Contract.templateID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  protected void _(PX.Data.Events.CacheAttached<Contract.duration> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(1)]
  protected void _(PX.Data.Events.CacheAttached<Contract.durationType> e)
  {
  }

  [PXOverride]
  public bool CanBeRestricted(Type entityType, object instance)
  {
    if (entityType == typeof (PX.Objects.IN.InventoryItem) && instance is PX.Objects.IN.InventoryItem inventoryItem)
      return inventoryItem.ItemStatus != "XX";
    if (!(entityType == typeof (Contract)) && !(entityType == typeof (PMProject)) || !(instance is Contract contract))
      return true;
    return !contract.NonProject.GetValueOrDefault() && contract.BaseType == "P";
  }
}
