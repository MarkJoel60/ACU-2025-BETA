// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.TemplateInventoryRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(InputMask = "", IsUnicode = true, PadSpaced = true)]
[PXUIField]
public sealed class TemplateInventoryRawAttribute : PXEntityAttribute
{
  public TemplateInventoryRawAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INVENTORY", typeof (Search<InventoryItem.inventoryCD, Where2<Match<Current<AccessInfo.userName>>, And<InventoryItem.isTemplate, Equal<True>>>>), typeof (InventoryItem.inventoryCD))
    {
      DescriptionField = typeof (InventoryItem.descr),
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
