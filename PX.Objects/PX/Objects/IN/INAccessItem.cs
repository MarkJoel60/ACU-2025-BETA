// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAccessItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class INAccessItem : BaseAccess
{
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  public PXSetup<CommonSetup> CommoSetup;
  public PXSelect<InventoryItem> Item;
  public PXSelect<INItemClass> Class;

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [INAccessItem.INRelationGroupSelector(typeof (RelationGroup.groupName), Filterable = true)]
  protected virtual void RelationGroup_GroupName_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("INVENTORY", typeof (Search<InventoryItem.inventoryCD, Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>>>>), typeof (InventoryItem.inventoryCD))]
  protected virtual void InventoryItem_InventoryCD_CacheAttached(PXCache sender)
  {
  }

  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXRemoveBaseAttribute(typeof (PXUIRequiredAttribute))]
  protected virtual void InventoryItem_ItemClassID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Lot/Serial Class")]
  [PXDBString(10, IsUnicode = true)]
  protected virtual void InventoryItem_LotSerClassID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Posting Class")]
  [PXDBString(10, IsUnicode = true)]
  protected virtual void InventoryItem_PostClassID_CacheAttached(PXCache sender)
  {
  }

  public INAccessItem()
  {
    PX.Objects.IN.INSetup current = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
    ((PXSelectBase) this.Class).Cache.AllowDelete = false;
    ((PXSelectBase) this.Class).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Class).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INItemClass.included>(((PXSelectBase) this.Class).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Item).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<InventoryItem.included>(((PXSelectBase) this.Item).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<InventoryItem.inventoryCD>(((PXSelectBase) this.Item).Cache, (object) null);
    PXDefaultAttribute.SetPersistingCheck<InventoryItem.valMethod>(((PXSelectBase) this.Item).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<InventoryItem.cOGSAcctID>(((PXSelectBase) this.Item).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<InventoryItem.cOGSSubID>(((PXSelectBase) this.Item).Cache, (object) null, (PXPersistingCheck) 2);
  }

  protected virtual IEnumerable item()
  {
    INAccessItem inAccessItem = this;
    if (((PXSelectBase<RelationGroup>) inAccessItem.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) inAccessItem.Group).Current.GroupName))
    {
      PXSelect<InventoryItem, Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<InventoryItem.groupMask>>>>>>>> pxSelect = new PXSelect<InventoryItem, Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<InventoryItem.groupMask>>>>>>>>((PXGraph) inAccessItem);
      int startRow = PXView.StartRow;
      int num = 0;
      object[] array = ((IEnumerable<object>) PXView.Parameters).Concat<object>((IEnumerable<object>) new object[1]
      {
        (object) new byte[0]
      }).ToArray<object>();
      foreach (InventoryItem inventoryItem in ((PXSelectBase) pxSelect).View.Select(PXView.Currents, array, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
        yield return (object) inventoryItem;
      PXView.StartRow = 0;
    }
  }

  protected virtual IEnumerable cLass()
  {
    INAccessItem inAccessItem = this;
    if (((PXSelectBase<RelationGroup>) inAccessItem.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) inAccessItem.Group).Current.GroupName))
    {
      foreach (PXResult<INItemClass> pxResult in PXSelectBase<INItemClass, PXSelect<INItemClass>.Config>.Select((PXGraph) inAccessItem, Array.Empty<object>()))
      {
        INItemClass inItemClass = PXResult<INItemClass>.op_Implicit(pxResult);
        ((PXSelectBase<INItemClass>) inAccessItem.Class).Current = inItemClass;
        yield return (object) inItemClass;
      }
    }
  }

  public static IEnumerable GroupDelegate(PXGraph graph, bool inclInserted)
  {
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<itemType>, Or<Neighbour.leftEntityType, Equal<itemClassType>>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select(graph, Array.Empty<object>());
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select(graph, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(relationGroup.GroupName) | inclInserted && (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (InventoryItem).Namespace) && (relationGroup.SpecificType == null || relationGroup.SpecificType == typeof (SegmentValue).FullName || relationGroup.SpecificType == typeof (InventoryItem).FullName) || UserAccess.InNeighbours(set, relationGroup))
        yield return (object) relationGroup;
    }
  }

  protected virtual IEnumerable group() => INAccessItem.GroupDelegate((PXGraph) this, true);

  protected virtual void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    base.RelationGroup_RowInserted(cache, e);
    RelationGroup row = (RelationGroup) e.Row;
    row.SpecificModule = typeof (inventoryModule).Namespace;
    row.SpecificType = typeof (InventoryItem).FullName;
  }

  protected virtual void RelationGroup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RelationGroup row))
      return;
    if (string.IsNullOrEmpty(row.GroupName))
      ((PXAction) this.Save).SetEnabled(false);
    else
      ((PXAction) this.Save).SetEnabled(true);
  }

  protected virtual void INItemClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INItemClass row = e.Row as INItemClass;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null || sender.GetStatus((object) row) != null)
      return;
    row.Included = new bool?(false);
    for (int index = 0; index < row.GroupMask.Length && index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0 && ((int) row.GroupMask[index] & (int) current.GroupMask[index]) == (int) current.GroupMask[index])
        row.Included = new bool?(true);
    }
  }

  protected virtual void INItemClass_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    INItemClass row = e.Row as INItemClass;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  protected virtual void InventoryItem_InventoryCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXSelectorAttribute.Select<InventoryItem.inventoryCD>(sender, e.Row, e.NewValue) == null)
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "[InventoryCD]"
      });
  }

  protected virtual void InventoryItem_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<InventoryItem.inventoryCD>(sender, e.Row) is InventoryItem inventoryItem)
    {
      inventoryItem.Included = new bool?(true);
      PXCache<InventoryItem>.RestoreCopy((InventoryItem) e.Row, inventoryItem);
      GraphHelper.MarkUpdated(sender, e.Row, true);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void InventoryItem_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is InventoryItem row) || sender.GetStatus((object) row) != null)
      return;
    row.Included = new bool?(false);
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void InventoryItem_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    InventoryItem row = e.Row as InventoryItem;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  public virtual void Persist()
  {
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    this.populateNeighbours<InventoryItem>((PXSelectBase<InventoryItem>) this.Item);
    this.populateNeighbours<INItemClass>((PXSelectBase<INItemClass>) this.Class);
    this.populateNeighbours<InventoryItem>((PXSelectBase<InventoryItem>) this.Item);
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<Users>();
    PXSelectorAttribute.ClearGlobalCache<INItemClass>();
    PXSelectorAttribute.ClearGlobalCache<InventoryItem>();
    PXDimensionAttribute.Clear();
  }

  public class INRelationGroupSelectorAttribute(Type type) : PXCustomSelectorAttribute(type)
  {
    public virtual IEnumerable GetRecords() => INAccessItem.GroupDelegate(this._Graph, false);
  }
}
