// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Mobile.EP.Descriptor.Attributes.MobileProjectCostCodeDimensionSelectorAttribute
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

public class MobileProjectCostCodeDimensionSelectorAttribute : CostCodeDimensionSelectorAttribute
{
  public MobileProjectCostCodeDimensionSelectorAttribute(Type task, string budgetType)
    : base((Type) null, task, budgetType, (Type) null, false)
  {
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    int num = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    MobileProjectCostCodeSelectorAttribute selectorAttribute = new MobileProjectCostCodeSelectorAttribute();
    selectorAttribute.TaskID = task;
    selectorAttribute.BudgetType = budgetType;
    ((PXSelectorAttribute) selectorAttribute).DescriptionField = typeof (PMCostCode.description);
    attributes[num] = (PXEventSubscriberAttribute) selectorAttribute;
  }
}
