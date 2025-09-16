// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INIntegrityCheckExt.InventoryLinkFilterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INIntegrityCheckExt;

public class InventoryLinkFilterExt : 
  InventoryLinkFilterExtensionBase<INIntegrityCheck, INRecalculateInventoryFilter, INRecalculateInventoryFilter.inventoryID>
{
  [PXMergeAttributes]
  [AnyInventory(typeof (SearchFor<InventoryItem.inventoryID>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INRecalculateInventoryFilter.itemClassID>, IsNull>>>>.Or<BqlOperand<InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<INRecalculateInventoryFilter.itemClassID, IBqlInt>.FromCurrent>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), IsKey = true)]
  [PXRestrictor(typeof (Where<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.inactive>>), "The inventory item is {0}.", new Type[] {typeof (InventoryItem.itemStatus)}, ShowWarning = true)]
  protected override void _(
    Events.CacheAttached<InventoryLinkFilter.inventoryID> e)
  {
  }

  protected virtual void _(Events.RowUpdated<INRecalculateInventoryFilter> e)
  {
    if (!e.Row.InventoryID.HasValue || ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INRecalculateInventoryFilter>>) e).Cache.ObjectsEqual<INRecalculateInventoryFilter.inventoryID>((object) e.OldRow, (object) e.Row))
      return;
    ((PXSelectBase) this.SelectedItems).Cache.Clear();
    ((PXSelectBase) this.SelectedItems).Cache.ClearQueryCache();
  }

  protected virtual void _(
    Events.FieldUpdated<INRecalculateInventoryFilter, INRecalculateInventoryFilter.itemClassID> e)
  {
    if (this.IsValidValue<INRecalculateInventoryFilter.inventoryID>((object) e.Row))
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INRecalculateInventoryFilter, INRecalculateInventoryFilter.itemClassID>>) e).Cache.SetValueExt<INRecalculateInventoryFilter.inventoryID>((object) e.Row, (object) null);
  }

  protected virtual bool IsValidValue<TField>(object row) where TField : IBqlField
  {
    PXCache cach = ((PXGraph) this.Base).Caches[BqlCommand.GetItemType<TField>()];
    object obj = ((PXSelectBase) this.Base.Filter).Cache.GetValue<TField>(row);
    if (obj == null)
      return true;
    try
    {
      cach.RaiseFieldVerifying<TField>(row, ref obj);
      return true;
    }
    catch
    {
      return false;
    }
  }

  /// Overrides <see cref="M:PX.Objects.IN.INIntegrityCheck.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.IN.INRecalculateInventoryFilter)" />
  [PXOverride]
  public BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    INRecalculateInventoryFilter filter,
    Func<BqlCommand, IList<object>, INRecalculateInventoryFilter, BqlCommand> base_AppendFilter)
  {
    cmd = base_AppendFilter(cmd, parameters, filter);
    int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
    if (array.Length != 0)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<InventoryItemCommon.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
      parameters.Add((object) array);
    }
    return cmd;
  }

  public class descr : 
    InventoryLinkFilterExtensionBase<INIntegrityCheck, INRecalculateInventoryFilter, INRecalculateInventoryFilter.inventoryID>.AttachedInventoryDescription<InventoryLinkFilterExt.descr>
  {
  }
}
