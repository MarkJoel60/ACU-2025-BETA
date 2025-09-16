// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INUpdateMCAssignmentExt.InventoryLinkFilterExt
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
namespace PX.Objects.IN.GraphExtensions.INUpdateMCAssignmentExt;

public class InventoryLinkFilterExt : 
  InventoryLinkFilterExtensionBase<INUpdateMCAssignment, UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.inventoryID>
{
  [PXMergeAttributes]
  [AnyInventory(typeof (FbqlSelect<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlOperand<InventoryItem.stkItem, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<UpdateMCAssignmentSettings.itemClassID>, IsNull>>>>.Or<BqlOperand<InventoryItem.itemClassID, IBqlInt>.IsEqual<BqlField<UpdateMCAssignmentSettings.itemClassID, IBqlInt>.FromCurrent>>>>, InventoryItem>.SearchFor<InventoryItem.inventoryID>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), IsKey = true)]
  protected override void _(
    Events.CacheAttached<InventoryLinkFilter.inventoryID> e)
  {
  }

  protected virtual void _(Events.RowUpdated<UpdateMCAssignmentSettings> e)
  {
    if (e.Row.InventoryID.HasValue && !((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<UpdateMCAssignmentSettings>>) e).Cache.ObjectsEqual<UpdateMCAssignmentSettings.inventoryID>((object) e.OldRow, (object) e.Row))
    {
      ((PXSelectBase) this.SelectedItems).Cache.Clear();
      ((PXSelectBase) this.SelectedItems).Cache.ClearQueryCache();
    }
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<UpdateMCAssignmentSettings>>) e).Cache.ObjectsEqual<UpdateMCAssignmentSettings.itemClassID>((object) e.OldRow, (object) e.Row) || this.IsInventoryIDValid(e.Row))
      return;
    ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<UpdateMCAssignmentSettings>>) e).Cache.SetValueExt<UpdateMCAssignmentSettings.inventoryID>((object) e.Row, (object) null);
    ((PXSelectBase) this.SelectedItems).Cache.Clear();
    ((PXSelectBase) this.SelectedItems).Cache.ClearQueryCache();
  }

  protected virtual bool IsInventoryIDValid(UpdateMCAssignmentSettings row)
  {
    if (row.InventoryID.HasValue)
      return this.IsValidValue<UpdateMCAssignmentSettings.inventoryID>((object) row, row.InventoryID);
    foreach (int? nullable in this.GetSelectedEntities(row).ToArray<int?>())
    {
      if (!this.IsValidValue<UpdateMCAssignmentSettings.inventoryID>((object) row, nullable))
        return false;
    }
    return true;
  }

  protected virtual bool IsValidValue<TField>(object row, int? value) where TField : IBqlField
  {
    PXCache cach = ((PXGraph) this.Base).Caches[BqlCommand.GetItemType<TField>()];
    try
    {
      object obj = (object) value;
      cach.RaiseFieldVerifying<TField>(row, ref obj);
      return true;
    }
    catch
    {
      return false;
    }
  }

  /// Overrides <see cref="M:PX.Objects.IN.INUpdateMCAssignment.AppendFilter(PX.Data.BqlCommand,System.Collections.Generic.IList{System.Object},PX.Objects.IN.UpdateMCAssignmentSettings)" />
  [PXOverride]
  public BqlCommand AppendFilter(
    BqlCommand cmd,
    IList<object> parameters,
    UpdateMCAssignmentSettings filter,
    Func<BqlCommand, IList<object>, UpdateMCAssignmentSettings, BqlCommand> baseAppendFilter)
  {
    cmd = baseAppendFilter(cmd, parameters, filter);
    int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
    if (array.Length != 0)
    {
      cmd = cmd.WhereAnd<Where<BqlOperand<UpdateMCAssignmentResult.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
      parameters.Add((object) array);
    }
    return cmd;
  }

  public class descr : 
    InventoryLinkFilterExtensionBase<INUpdateMCAssignment, UpdateMCAssignmentSettings, UpdateMCAssignmentSettings.inventoryID>.AttachedInventoryDescription<InventoryLinkFilterExt.descr>
  {
  }
}
