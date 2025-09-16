// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INExpireDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXDBDate(InputMask = "d", DisplayMask = "d")]
[PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial")]
[PXDefault]
public class INExpireDateAttribute : 
  PXEntityAttribute,
  IPXFieldSelectingSubscriber,
  IPXRowSelectedSubscriber,
  IPXFieldDefaultingSubscriber,
  IPXRowPersistingSubscriber
{
  protected Type _InventoryType;

  public virtual bool ForceDisable { get; set; }

  public INExpireDateAttribute(Type InventoryType) => this._InventoryType = InventoryType;

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (!typeof (ILSMaster).IsAssignableFrom(sender.GetItemType()))
      throw new PXArgumentException("itemType", "The specified type {0} must implement the {1} interface.", new object[2]
      {
        (object) MainTools.GetLongName(sender.GetItemType()),
        (object) MainTools.GetLongName(typeof (ILSMaster))
      });
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (typeof (ISubscriber) == typeof (IPXFieldDefaultingSubscriber) || typeof (ISubscriber) == typeof (IPXRowPersistingSubscriber))
      subscribers.Add(this as ISubscriber);
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXCache.TryDispose((object) sender.GetAttributes(e.Row, ((PXEventSubscriberAttribute) this)._FieldName));
    if (!PXGraph.ProxyIsActive)
      return;
    PXContext.GetSlot<HashSet<string>>("_MeasuringUpdatability")?.Add(((PXEventSubscriberAttribute) this)._FieldName);
  }

  protected virtual PXResult<InventoryItem, INLotSerClass> ReadInventoryItem(
    PXCache sender,
    int? InventoryID)
  {
    InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, InventoryID);
    if (inventoryItem == null)
      return (PXResult<InventoryItem, INLotSerClass>) null;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find(sender.Graph, inventoryItem.LotSerClassID);
    return new PXResult<InventoryItem, INLotSerClass>(inventoryItem, inLotSerClass ?? new INLotSerClass());
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    ((PXUIFieldAttribute) ((PXAggregateAttribute) this)._Attributes[this._UIAttrIndex]).Enabled = this.IsFieldEnabled(sender, (ILSMaster) e.Row);
  }

  protected virtual bool IsFieldEnabled(PXCache sender, ILSMaster row)
  {
    return sender.AllowUpdate && this.IsTrackExpiration(sender, row) && !this.ForceDisable;
  }

  protected virtual bool IsTrackExpiration(PXCache sender, ILSMaster row)
  {
    PXResult<InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, row.InventoryID);
    if (pxResult == null)
      return false;
    INLotSerClass lotSerClass = PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = row.TranType;
    short? invtMult = row.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    return INLotSerialNbrAttribute.IsTrackExpiration(lotSerClass, tranType, invMult);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!((ILSMaster) e.Row).SubItemID.HasValue || !((ILSMaster) e.Row).LocationID.HasValue || !this.IsTrackExpiration(sender, (ILSMaster) e.Row))
      return;
    Decimal? baseQty = ((ILSMaster) e.Row).BaseQty;
    Decimal num = 0M;
    if (baseQty.GetValueOrDefault() == num & baseQty.HasValue)
      return;
    ((IPXRowPersistingSubscriber) ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex]).RowPersisting(sender, e);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    ((IPXFieldDefaultingSubscriber) ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex]).FieldDefaulting(sender, e);
  }
}
