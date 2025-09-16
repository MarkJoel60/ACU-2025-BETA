// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.InventoryItemWithLotSerialAttributesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.DAC.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Attributes;

[PXInternalUseOnly]
public class InventoryItemWithLotSerialAttributesAttribute : PXEventSubscriberAttribute
{
  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    InventoryItemWithLotSerialAttributesAttribute attributesAttribute = this;
    // ISSUE: virtual method pointer
    Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<InventoryItemLotSerialAttributes>>.EventDelegate eventDelegate = new Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<InventoryItemLotSerialAttributes>>.EventDelegate((object) attributesAttribute, __vmethodptr(attributesAttribute, RowSelecting));
    rowSelecting.AddHandler<InventoryItemLotSerialAttributes>(eventDelegate);
  }

  public virtual void RowSelecting(
    Events.RowSelecting<InventoryItemLotSerialAttributes> e)
  {
    List<INItemLotSerialAttribute> list = GraphHelper.RowCast<INItemLotSerialAttribute>((IEnumerable) PXSelectBase<INItemLotSerialAttribute, PXViewOf<INItemLotSerialAttribute>.BasedOn<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemLotSerialAttribute.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select(((Events.Event<PXRowSelectingEventArgs, Events.RowSelecting<InventoryItemLotSerialAttributes>>) e).Cache.Graph, new object[1]
    {
      (object) e.Row.InventoryID
    })).ToList<INItemLotSerialAttribute>();
    e.Row.AttributeIdentifiers = list.Select<INItemLotSerialAttribute, string>((Func<INItemLotSerialAttribute, string>) (r => r.AttributeID)).ToArray<string>();
    e.Row.AttributeRequired = list.Select<INItemLotSerialAttribute, bool>((Func<INItemLotSerialAttribute, bool>) (r => r.Required.GetValueOrDefault())).ToArray<bool>();
    e.Row.AttributeIsActive = list.Select<INItemLotSerialAttribute, bool>((Func<INItemLotSerialAttribute, bool>) (r => r.IsActive.GetValueOrDefault())).ToArray<bool>();
    e.Row.AttributeSortOrder = list.Select<INItemLotSerialAttribute, short>((Func<INItemLotSerialAttribute, short>) (r => r.SortOrder.GetValueOrDefault())).ToArray<short>();
  }
}
