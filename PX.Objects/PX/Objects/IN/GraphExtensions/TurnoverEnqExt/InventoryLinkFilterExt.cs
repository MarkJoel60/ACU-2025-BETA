// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.TurnoverEnqExt.InventoryLinkFilterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.Turnover;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.TurnoverEnqExt;

public class InventoryLinkFilterExt : 
  InventoryLinkFilterExtensionBase<TurnoverEnq, INTurnoverEnqFilter, INTurnoverEnqFilter.inventoryID>
{
  [PXMergeAttributes]
  [AnyInventory(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlOperand<PX.Objects.IN.InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTurnoverEnqFilter.itemClassID>, IsNull>>>>.Or<BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<INTurnoverEnqFilter.itemClassID, IBqlInt>.FromCurrent>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), DisplayName = "Inventory ID", IsKey = true)]
  protected override void _(
    Events.CacheAttached<InventoryLinkFilter.inventoryID> e)
  {
  }

  protected virtual void _(Events.RowUpdated<INTurnoverEnqFilter> e)
  {
    if (!e.Row.InventoryID.HasValue || ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<INTurnoverEnqFilter>>) e).Cache.ObjectsEqual<INTurnoverEnqFilter.inventoryID>((object) e.OldRow, (object) e.Row))
      return;
    ((PXSelectBase) this.SelectedItems).Cache.Clear();
    ((PXSelectBase) this.SelectedItems).Cache.ClearQueryCache();
  }

  [PXButton]
  [PXUIField(DisplayName = "List")]
  public override void selectItems()
  {
    base.selectItems();
    this.Base.FindTurnoverCalc(((PXSelectBase<INTurnoverEnqFilter>) this.Base.Filter).Current);
  }

  /// Overrides <see cref="M:PX.Objects.IN.Turnover.TurnoverEnq.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.IN.Turnover.INTurnoverEnqFilter)" />
  [PXOverride]
  public BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    INTurnoverEnqFilter filter,
    Func<BqlCommand, IList<object>, INTurnoverEnqFilter, BqlCommand> base_AppendFilter)
  {
    cmd = base_AppendFilter(cmd, parameters, filter);
    int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
    if (array.Length != 0)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<TurnoverCalcItem.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
      parameters.Add((object) array);
    }
    return cmd;
  }

  public class descr : 
    InventoryLinkFilterExtensionBase<TurnoverEnq, INTurnoverEnqFilter, INTurnoverEnqFilter.inventoryID>.AttachedInventoryDescription<InventoryLinkFilterExt.descr>
  {
  }
}
