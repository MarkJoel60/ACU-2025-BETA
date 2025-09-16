// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.MassConvertStockNonStockExt.InventoryLinkFilterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN.DAC.Unbound;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.MassConvertStockNonStockExt;

public class InventoryLinkFilterExt : 
  InventoryLinkFilterExtensionBase<MassConvertStockNonStock, MassConvertStockNonStockFilter, MassConvertStockNonStockFilter.inventoryID>
{
  [PXMergeAttributes]
  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.stkItem, Equal<Current<MassConvertStockNonStockFilter.stkItem>>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.templateItemID, IsNull, And<InventoryItem.kitItem, Equal<False>, And2<Where<InventoryItem.stkItem, Equal<True>, Or<InventoryItem.nonStockReceipt, Equal<True>, And<InventoryItem.nonStockShip, Equal<True>, And<InventoryItem.itemType, Equal<INItemTypes.nonStockItem>>>>>, And<MatchUser>>>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), DisplayName = "Inventory ID", IsKey = true)]
  protected override void _(
    Events.CacheAttached<InventoryLinkFilter.inventoryID> e)
  {
  }

  protected virtual void _(
    Events.RowUpdated<MassConvertStockNonStockFilter> e)
  {
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<MassConvertStockNonStockFilter>>) e).Cache.ObjectsEqual<MassConvertStockNonStockFilter.stkItem, MassConvertStockNonStockFilter.inventoryID>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase) this.SelectedItems).Cache.Clear();
    ((PXSelectBase) this.SelectedItems).Cache.ClearQueryCache();
  }

  /// Overrides <see cref="M:PX.Objects.IN.MassConvertStockNonStock.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.IN.DAC.Unbound.MassConvertStockNonStockFilter)" />
  [PXOverride]
  public BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    MassConvertStockNonStockFilter filter,
    Func<BqlCommand, IList<object>, MassConvertStockNonStockFilter, BqlCommand> base_AppendFilter)
  {
    cmd = base_AppendFilter(cmd, parameters, filter);
    int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
    if (array.Length != 0)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<InventoryItem.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
      parameters.Add((object) array);
    }
    return cmd;
  }

  public class descr : 
    InventoryLinkFilterExtensionBase<MassConvertStockNonStock, MassConvertStockNonStockFilter, MassConvertStockNonStockFilter.inventoryID>.AttachedInventoryDescription<InventoryLinkFilterExt.descr>
  {
  }
}
