// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt.RelatedItemsTabExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.RelatedItems;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt;

public class RelatedItemsTabExt : RelatedItemsTab<InventoryItemMaintBase>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.relatedItems>() || PXAccess.FeatureInstalled<FeaturesSet.commerceIntegration>();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.InventoryItemMaintBase.Persist" />
  /// </summary>
  /// <param name="baseImpl"></param>
  [PXOverride]
  public virtual void Persist(Action baseImpl)
  {
    this.CheckForDuplicates();
    baseImpl();
  }
}
