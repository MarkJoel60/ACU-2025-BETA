// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INDefaultWarehouse
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class INDefaultWarehouse : PXEventSubscriberAttribute, IPXDependsOnFields
{
  private Type inventoryID;
  private Type siteID;

  public INDefaultWarehouse(Type siteID, Type inventoryID)
  {
    this.siteID = siteID;
    this.inventoryID = inventoryID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    Type itemType = sender.GetItemType();
    INDefaultWarehouse defaultWarehouse = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) defaultWarehouse, __vmethodptr(defaultWarehouse, RowSelecting));
    rowSelecting.AddHandler(itemType, pxRowSelecting);
  }

  protected virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    object obj1 = sender.GetValue(e.Row, this.inventoryID.Name);
    object obj2 = sender.GetValue(e.Row, this.siteID.Name);
    if (obj1 == null || obj2 == null)
      return;
    INSite inSite = INSite.PK.Find(sender.Graph, (int?) obj2);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(sender.Graph, new int?((int) obj1), inSite?.BaseCuryID);
    sender.SetValue(e.Row, this._FieldName, (object) (bool) (itemCurySettings == null ? 0 : (object.Equals((object) itemCurySettings.DfltSiteID, obj2) ? 1 : 0)));
  }

  public ISet<Type> GetDependencies(PXCache cache)
  {
    HashSet<Type> dependencies = new HashSet<Type>();
    if (this.siteID != (Type) null)
      dependencies.Add(this.siteID);
    if (this.inventoryID != (Type) null)
      dependencies.Add(this.inventoryID);
    return (ISet<Type>) dependencies;
  }
}
