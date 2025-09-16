// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Mobile.EP.Descriptor.Attributes.MobileProjectCostCodeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Mobile.EP.Descriptor.Attributes;

public class MobileProjectCostCodeAttribute : CostCodeAttribute
{
  public MobileProjectCostCodeAttribute(
    Type task,
    string budgetType,
    Type projectField = null,
    Type inventoryField = null,
    bool useNewDefaulting = false)
    : base((Type) null, task, budgetType, (Type) null, false, useNewDefaulting)
  {
    this.ProjectField = projectField;
    this.InventoryField = inventoryField;
    ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1] = (PXEventSubscriberAttribute) new MobileProjectCostCodeDimensionSelectorAttribute(task, budgetType);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public override void CacheAttached(PXCache sender) => base.CacheAttached(sender);
}
