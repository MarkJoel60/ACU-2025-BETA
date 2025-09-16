// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.RelationGroupsExt.HideTemplates
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.RelationGroupsExt;

public class HideTemplates : PXGraphExtension<RelationGroups>
{
  [PXOverride]
  public virtual bool CanBeRestricted(
    Type entityType,
    object instance,
    Func<Type, object, bool> baseMethod)
  {
    if (!baseMethod(entityType, instance))
      return false;
    if (!(entityType == typeof (InventoryItem)) || !(instance is InventoryItem inventoryItem))
      return true;
    bool? isTemplate = inventoryItem.IsTemplate;
    bool flag = false;
    return isTemplate.GetValueOrDefault() == flag & isTemplate.HasValue;
  }
}
