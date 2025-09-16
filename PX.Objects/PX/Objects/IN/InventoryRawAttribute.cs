// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(InputMask = "", IsUnicode = true, PadSpaced = true)]
[PXUIField]
public sealed class InventoryRawAttribute : PXEntityAttribute
{
  public const string DimensionName = "INVENTORY";
  private Type _whereType;

  public InventoryRawAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INVENTORY", typeof (Search<InventoryItem.inventoryCD, Where2<Match<Current<AccessInfo.userName>>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>>>>), typeof (InventoryItem.inventoryCD))
    {
      DescriptionField = typeof (InventoryItem.descr),
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public InventoryRawAttribute(Type WhereType)
    : this()
  {
    if (!(WhereType != (Type) null))
      return;
    this._whereType = WhereType;
    ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INVENTORY", BqlCommand.Compose(new Type[13]
    {
      typeof (Search<,>),
      typeof (InventoryItem.inventoryCD),
      typeof (Where2<,>),
      typeof (Match<>),
      typeof (Current<AccessInfo.userName>),
      typeof (And<,,>),
      typeof (InventoryItem.itemStatus),
      typeof (NotEqual<InventoryItemStatus.unknown>),
      typeof (And<,,>),
      typeof (InventoryItem.isTemplate),
      typeof (Equal<False>),
      typeof (And<>),
      this._whereType
    }), typeof (InventoryItem.inventoryCD))
    {
      DescriptionField = typeof (InventoryItem.descr),
      CacheGlobal = true
    };
  }
}
