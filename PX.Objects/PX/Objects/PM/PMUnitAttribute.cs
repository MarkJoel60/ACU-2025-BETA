// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMUnitAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Same as INUnit with support for Inventory=<![CDATA[ <N/A> ]]>
/// </summary>
[PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
[PXUIField]
public class PMUnitAttribute : INUnitAttribute
{
  public PMUnitAttribute(Type InventoryType)
    : base(InventoryType)
  {
    Type type = ((IBqlTemplate) BqlTemplate.OfCommand<Search<INUnit.fromUnit, Where<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.inventoryID, Equal<Optional<BqlPlaceholder.A>>, Or<INUnit.unitType, Equal<INUnitType.global>, And<Optional<BqlPlaceholder.A>, IsNull, Or<Optional<BqlPlaceholder.A>, Equal<Zero>>>>>>>>.Replace<BqlPlaceholder.A>(InventoryType)).ToType();
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PMUnitSelectorAttrubute(InventoryType, type));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.SupportNAInventory = true;
  }
}
