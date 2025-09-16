// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAccessDetailItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.IN;

public class INAccessDetailItem : UserAccess
{
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  public PXSelect<INItemClass> Class;
  public PXSelect<InventoryItem> Item;

  [PXDefault]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("INVENTORY", typeof (InventoryItem.inventoryCD), typeof (InventoryItem.inventoryCD))]
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

  public INAccessDetailItem()
  {
    PX.Objects.IN.INSetup current = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
    ((PXSelectBase) this.Class).Cache.AllowDelete = false;
    ((PXSelectBase) this.Class).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Class).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<INItemClass.itemClassCD>(((PXSelectBase) this.Class).Cache, (object) null);
    ((PXSelectBase) this.Item).Cache.AllowDelete = false;
    ((PXSelectBase) this.Item).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Item).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<InventoryItem.inventoryCD>(((PXSelectBase) this.Item).Cache, (object) null);
    ((PXGraph) this).Views.Caches.Remove(typeof (RelationGroup));
    ((PXGraph) this).Views.Caches.Add(typeof (RelationGroup));
    PXDefaultAttribute.SetPersistingCheck<InventoryItem.valMethod>(((PXSelectBase) this.Item).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<InventoryItem.cOGSAcctID>(((PXSelectBase) this.Item).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<InventoryItem.cOGSSubID>(((PXSelectBase) this.Item).Cache, (object) null, (PXPersistingCheck) 2);
  }

  protected virtual IEnumerable groups()
  {
    INAccessDetailItem accessDetailItem = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) accessDetailItem, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if ((relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (InventoryItem).Namespace) && (((PXSelectBase<INItemClass>) accessDetailItem.Class).Current != null || relationGroup.SpecificType == null || relationGroup.SpecificType == typeof (SegmentValue).FullName || relationGroup.SpecificType == typeof (InventoryItem).FullName) || UserAccess.IsIncluded(((UserAccess) accessDetailItem).getMask(), relationGroup))
      {
        ((PXSelectBase<RelationGroup>) accessDetailItem.Groups).Current = relationGroup;
        yield return (object) relationGroup;
      }
    }
  }

  protected virtual byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
      mask = ((PXSelectBase<InventoryItem>) this.Item).Current.GroupMask;
    else if (((PXSelectBase<INItemClass>) this.Class).Current != null)
      mask = ((PXSelectBase<INItemClass>) this.Class).Current.GroupMask;
    return mask;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<InventoryItem>) this.Item).Current != null)
    {
      UserAccess.PopulateNeighbours<InventoryItem>((PXSelectBase<InventoryItem>) this.Item, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<InventoryItem>();
    }
    else
    {
      if (((PXSelectBase<INItemClass>) this.Class).Current == null)
        return;
      UserAccess.PopulateNeighbours<INItemClass>((PXSelectBase<INItemClass>) this.Class, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<Type>());
      PXSelectorAttribute.ClearGlobalCache<INItemClass>();
    }
    base.Persist();
  }
}
