// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLotSerialNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOLotSerialNbrAttribute : INLotSerialNbrAttribute
{
  private SOLotSerialNbrAttribute()
  {
  }

  public SOLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type CostCenterType)
  {
    Type itemType = BqlCommand.GetItemType(InventoryType);
    if (!typeof (ILSMaster).IsAssignableFrom(itemType))
      throw new PXArgumentException("The specified type {0} must implement the {1} interface.", MainTools.GetLongName(itemType), new object[1]
      {
        (object) MainTools.GetLongName(typeof (ILSMaster))
      });
    this._InventoryType = InventoryType;
    this._SubItemType = SubItemType;
    this._LocationType = LocationType;
    this._CostCenterType = CostCenterType;
    Type type = typeof (IConstant).IsAssignableFrom(CostCenterType) ? CostCenterType : BqlTemplate.FromType(typeof (Optional<BqlPlaceholder.D>)).Replace<BqlPlaceholder.D>(CostCenterType).ToType();
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(((IBqlTemplate) BqlTemplate.OfCommand<Search2<INLotSerialStatusByCostCenter.lotSerialNbr, InnerJoin<INSiteLotSerial, On<INLotSerialStatusByCostCenter.inventoryID, Equal<INSiteLotSerial.inventoryID>, And<INLotSerialStatusByCostCenter.siteID, Equal<INSiteLotSerial.siteID>, And<INLotSerialStatusByCostCenter.lotSerialNbr, Equal<INSiteLotSerial.lotSerialNbr>>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Optional<BqlPlaceholder.A>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Optional<BqlPlaceholder.B>>, And2<Where<INLotSerialStatusByCostCenter.locationID, Equal<Optional<BqlPlaceholder.C>>, Or<Optional<BqlPlaceholder.C>, IsNull>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<BqlPlaceholder.D>, And<INLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>>>>>>>>.Replace<BqlPlaceholder.A>(InventoryType).Replace<BqlPlaceholder.B>(SubItemType).Replace<BqlPlaceholder.C>(LocationType).Replace<BqlPlaceholder.D>(type)).ToType(), new Type[5]
    {
      typeof (INLotSerialStatusByCostCenter.lotSerialNbr),
      typeof (INLotSerialStatusByCostCenter.siteID),
      typeof (INLotSerialStatusByCostCenter.qtyOnHand),
      typeof (INSiteLotSerial.qtyAvail),
      typeof (INLotSerialStatusByCostCenter.expireDate)
    }));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public SOLotSerialNbrAttribute(
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type ParentLotSerialNbrType,
    Type CostCenterType)
    : this(InventoryType, SubItemType, LocationType, CostCenterType)
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

  public class SOAllocationLotSerialNbrAttribute : SOLotSerialNbrAttribute
  {
    public SOAllocationLotSerialNbrAttribute(
      Type InventoryType,
      Type SubItemType,
      Type SiteType,
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
      Type type = typeof (IConstant).IsAssignableFrom(CostCenterType) ? CostCenterType : BqlTemplate.FromType(typeof (Optional<BqlPlaceholder.E>)).Replace<BqlPlaceholder.E>(CostCenterType).ToType();
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(((IBqlTemplate) BqlTemplate.OfCommand<Search5<INLotSerialStatusByCostCenter.lotSerialNbr, InnerJoin<INSiteLotSerial, On<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<INSiteLotSerial.siteID, Equal<INLotSerialStatusByCostCenter.siteID>, And<INSiteLotSerial.lotSerialNbr, Equal<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Optional<BqlPlaceholder.A>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Optional<BqlPlaceholder.B>>, And2<Where<INLotSerialStatusByCostCenter.siteID, Equal<Optional<BqlPlaceholder.C>>, Or<Optional<BqlPlaceholder.C>, IsNull>>, And2<Where<INLotSerialStatusByCostCenter.locationID, Equal<Optional<BqlPlaceholder.D>>, Or<Optional<BqlPlaceholder.D>, IsNull>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<BqlPlaceholder.E>, And<INLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>>>>>>>, Aggregate<GroupBy<INLotSerialStatusByCostCenter.lotSerialNbr, GroupBy<INLotSerialStatusByCostCenter.siteID, Sum<INLotSerialStatusByCostCenter.qtyOnHand, Sum<INLotSerialStatusByCostCenter.qtyAvail>>>>>>>.Replace<BqlPlaceholder.A>(InventoryType).Replace<BqlPlaceholder.B>(SubItemType).Replace<BqlPlaceholder.C>(SiteType).Replace<BqlPlaceholder.D>(LocationType).Replace<BqlPlaceholder.E>(type)).ToType(), new Type[7]
      {
        typeof (INLotSerialStatusByCostCenter.lotSerialNbr),
        typeof (INLotSerialStatusByCostCenter.siteID),
        typeof (INLotSerialStatusByCostCenter.qtyOnHand),
        typeof (INLotSerialStatusByCostCenter.qtyAvail),
        typeof (INSiteLotSerial.qtyOnHand),
        typeof (INSiteLotSerial.qtyAvail),
        typeof (INLotSerialStatusByCostCenter.expireDate)
      }));
      this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    }

    public override void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      base.RowSelected(sender, e);
      Type[] typeArray;
      if (sender.GetValue(e.Row, this._LocationType.Name) == null)
        typeArray = new Type[5]
        {
          typeof (INLotSerialStatusByCostCenter.lotSerialNbr),
          typeof (INLotSerialStatusByCostCenter.siteID),
          typeof (INSiteLotSerial.qtyOnHand),
          typeof (INSiteLotSerial.qtyAvail),
          typeof (INLotSerialStatusByCostCenter.expireDate)
        };
      else
        typeArray = new Type[5]
        {
          typeof (INLotSerialStatusByCostCenter.lotSerialNbr),
          typeof (INLotSerialStatusByCostCenter.siteID),
          typeof (INLotSerialStatusByCostCenter.qtyOnHand),
          typeof (INLotSerialStatusByCostCenter.qtyAvail),
          typeof (INLotSerialStatusByCostCenter.expireDate)
        };
      Type[] source = typeArray;
      PXSelectorAttribute.SetColumns(sender, ((PXEventSubscriberAttribute) this)._FieldName, ((IEnumerable<Type>) source).Select<Type, string>((Func<Type, string>) (f => SOLotSerialNbrAttribute.SOAllocationLotSerialNbrAttribute.GetFieldName(sender.Graph, f))).ToArray<string>(), ((IEnumerable<Type>) source).Select<Type, string>((Func<Type, string>) (f => SOLotSerialNbrAttribute.SOAllocationLotSerialNbrAttribute.GetFieldDisplayName(sender.Graph, f))).ToArray<string>());
    }

    private static string GetFieldName(PXGraph graph, Type field)
    {
      Type itemType = BqlCommand.GetItemType(field);
      return !(itemType == typeof (INLotSerialStatusByCostCenter)) ? $"{itemType.Name}__{field.Name}" : field.Name;
    }

    private static string GetFieldDisplayName(PXGraph graph, Type field)
    {
      Type itemType = BqlCommand.GetItemType(field);
      object obj = (object) null;
      graph.Caches[itemType].RaiseFieldSelecting(field.Name, (object) null, ref obj, true);
      return !(obj is PXFieldState pxFieldState) ? field.Name : pxFieldState.DisplayName;
    }

    public SOAllocationLotSerialNbrAttribute(
      Type InventoryType,
      Type SubItemType,
      Type SiteType,
      Type LocationType,
      Type ParentLotSerialNbrType,
      Type CostCenterType)
      : this(InventoryType, SubItemType, SiteType, LocationType, CostCenterType)
    {
      ((PXAggregateAttribute) this)._Attributes[this._DefAttrIndex] = (PXEventSubscriberAttribute) new PXDefaultAttribute(ParentLotSerialNbrType)
      {
        PersistingCheck = (PXPersistingCheck) 1
      };
    }
  }
}
