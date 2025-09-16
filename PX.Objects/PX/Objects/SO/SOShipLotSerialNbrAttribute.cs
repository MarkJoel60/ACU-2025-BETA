// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipLotSerialNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class SOShipLotSerialNbrAttribute : INLotSerialNbrAttribute
{
  public SOShipLotSerialNbrAttribute(
    Type SiteID,
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type CostCenterType)
  {
    Type itemType = BqlCommand.GetItemType(InventoryType);
    if (!typeof (ILSMaster).IsAssignableFrom(itemType))
      throw new PXArgumentException("itemType", "The specified type {0} must implement the {1} interface.", new object[2]
      {
        (object) MainTools.GetLongName(itemType),
        (object) MainTools.GetLongName(typeof (ILSMaster))
      });
    this._InventoryType = InventoryType;
    this._SubItemType = SubItemType;
    this._LocationType = LocationType;
    this._CostCenterType = CostCenterType;
    List<Type> typeList = new List<Type>()
    {
      typeof (Search2<,,>),
      typeof (INLotSerialStatusByCostCenter.lotSerialNbr),
      typeof (InnerJoin<INSiteLotSerial, On<INLotSerialStatusByCostCenter.inventoryID, Equal<INSiteLotSerial.inventoryID>, And<INLotSerialStatusByCostCenter.siteID, Equal<INSiteLotSerial.siteID>, And<INLotSerialStatusByCostCenter.lotSerialNbr, Equal<INSiteLotSerial.lotSerialNbr>>>>>),
      typeof (Where<,,>),
      typeof (INLotSerialStatusByCostCenter.inventoryID),
      typeof (Equal<>),
      typeof (Optional<>),
      InventoryType,
      typeof (And<,,>),
      typeof (INLotSerialStatusByCostCenter.siteID),
      typeof (Equal<>),
      typeof (Optional<>),
      SiteID,
      typeof (And<,,>),
      typeof (INLotSerialStatusByCostCenter.subItemID),
      typeof (Equal<>),
      typeof (Optional<>),
      SubItemType,
      typeof (And2<,>),
      typeof (Where<,,>),
      typeof (Optional<>),
      LocationType,
      typeof (IsNotNull),
      typeof (And<,,>),
      typeof (INLotSerialStatusByCostCenter.locationID),
      typeof (Equal<>),
      typeof (Optional<>),
      LocationType,
      typeof (Or<,>),
      typeof (Optional<>),
      LocationType,
      typeof (IsNull),
      typeof (And<,,>),
      typeof (INLotSerialStatusByCostCenter.qtyOnHand),
      typeof (Greater<>),
      typeof (decimal0),
      typeof (And<,>),
      typeof (INLotSerialStatusByCostCenter.costCenterID),
      typeof (Equal<>)
    };
    if (!typeof (IConstant).IsAssignableFrom(CostCenterType))
      typeList.Add(typeof (Optional<>));
    typeList.Add(CostCenterType);
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(BqlCommand.Compose(typeList.ToArray()), new Type[6]
    {
      typeof (INLotSerialStatusByCostCenter.lotSerialNbr),
      typeof (INLotSerialStatusByCostCenter.siteID),
      typeof (INLotSerialStatusByCostCenter.locationID),
      typeof (INLotSerialStatusByCostCenter.qtyOnHand),
      typeof (INSiteLotSerial.qtyAvail),
      typeof (INLotSerialStatusByCostCenter.expireDate)
    }));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public SOShipLotSerialNbrAttribute(
    Type SiteID,
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type ParentLotSerialNbrType,
    Type CostCenterType)
    : this(SiteID, InventoryType, SubItemType, LocationType, CostCenterType)
  {
    ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex] = (PXEventSubscriberAttribute) new PXDefaultAttribute(ParentLotSerialNbrType)
    {
      PersistingCheck = (PXPersistingCheck) 1
    };
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber) || typeof (ISubscriber) == typeof (IPXFieldDefaultingSubscriber) || typeof (ISubscriber) == typeof (IPXRowPersistingSubscriber))
      subscribers.Add(this as ISubscriber);
    else if (typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber))
    {
      base.GetSubscriber<ISubscriber>(subscribers);
      subscribers.Remove(this as ISubscriber);
      subscribers.Add(this as ISubscriber);
      subscribers.Reverse();
    }
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    SOShipLotSerialNbrAttribute serialNbrAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) serialNbrAttribute, __vmethodptr(serialNbrAttribute, LotSerialNumberUpdated));
    fieldUpdated.AddHandler<SOShipLineSplit.lotSerialNbr>(pxFieldUpdated);
  }

  protected virtual void LotSerialNumberUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOShipLineSplit row) || string.IsNullOrEmpty(row.LotSerialNbr))
      return;
    SOShipLine soShipLine1 = PXParentAttribute.SelectParent<SOShipLine>(sender, e.Row);
    if (soShipLine1 == null || !soShipLine1.IsUnassigned.GetValueOrDefault() || row.LocationID.HasValue)
      return;
    SOShipLine soShipLine2 = PXParentAttribute.SelectParent<SOShipLine>(sender, e.Row);
    PXResultset<INLotSerialStatusByCostCenter> pxResultset = PXSelectBase<INLotSerialStatusByCostCenter, PXSelect<INLotSerialStatusByCostCenter, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Required<INLotSerialStatusByCostCenter.inventoryID>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Required<INLotSerialStatusByCostCenter.subItemID>>, And<INLotSerialStatusByCostCenter.siteID, Equal<Required<INLotSerialStatusByCostCenter.siteID>>, And<INLotSerialStatusByCostCenter.lotSerialNbr, Equal<Required<INLotSerialStatusByCostCenter.lotSerialNbr>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<Required<INLotSerialStatusByCostCenter.costCenterID>>, And<INLotSerialStatusByCostCenter.qtyHardAvail, Greater<Zero>>>>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[5]
    {
      (object) row.InventoryID,
      (object) row.SubItemID,
      (object) row.SiteID,
      (object) row.LotSerialNbr,
      (object) soShipLine2.CostCenterID
    });
    if (pxResultset.Count != 1)
      return;
    sender.SetValueExt<SOShipLineSplit.locationID>((object) row, (object) PXResultset<INLotSerialStatusByCostCenter>.op_Implicit(pxResultset).LocationID);
  }
}
