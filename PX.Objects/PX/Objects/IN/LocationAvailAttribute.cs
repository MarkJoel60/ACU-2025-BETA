// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.LocationAvailAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<INLocation.active, Equal<True>>), "Location '{0}' is inactive", new Type[] {typeof (INLocation.locationCD)}, CacheGlobal = true)]
public class LocationAvailAttribute : LocationAttribute, IPXFieldDefaultingSubscriber
{
  protected Type _InventoryType;
  protected Type _costCenterType;
  protected Type _IsSalesType;
  protected Type _IsReceiptType;
  protected Type _IsTransferType;
  protected Type _IsStandardCostAdjType;
  protected BqlCommand _Select;
  protected readonly Type[] _hiddenLocationStatusColumns = new Type[2]
  {
    typeof (INLocationStatusByCostCenter.qtyAvail),
    typeof (INLocationStatusByCostCenter.qtyHardAvail)
  };

  public LocationAvailAttribute(
    Type InventoryType,
    Type SubItemType,
    Type CostCenterType,
    Type SiteIDType,
    bool IsSalesType,
    bool IsReceiptType,
    bool IsTransferType)
    : this(InventoryType, SubItemType, CostCenterType, SiteIDType, (Type) null, (Type) null, (Type) null)
  {
    this._IsSalesType = IsSalesType ? typeof (Where<True>) : typeof (Where<False>);
    this._IsReceiptType = IsReceiptType ? typeof (Where<True>) : typeof (Where<False>);
    this._IsTransferType = IsTransferType ? typeof (Where<True>) : typeof (Where<False>);
    this._IsStandardCostAdjType = typeof (Where<False>);
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PrimaryItemRestrictorAttribute(InventoryType, this._IsReceiptType, this._IsSalesType, this._IsTransferType));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new LocationRestrictorAttribute(this._IsReceiptType, this._IsSalesType, this._IsTransferType));
  }

  public LocationAvailAttribute(
    Type InventoryType,
    Type SubItemType,
    Type CostCenterType,
    Type SiteIDType,
    Type TranType,
    Type InvtMultType)
    : this(InventoryType, SubItemType, CostCenterType, SiteIDType, TranType, InvtMultType, true)
  {
  }

  public LocationAvailAttribute(
    Type InventoryType,
    Type SubItemType,
    Type CostCenterType,
    Type SiteIDType,
    Type TranType,
    Type InvtMultType,
    bool VerifyAllowedOperations)
    : this(InventoryType, SubItemType, CostCenterType, SiteIDType, (Type) null, (Type) null, (Type) null)
  {
    this._IsSalesType = BqlCommand.Compose(new Type[3]
    {
      typeof (Where<,>),
      TranType,
      typeof (In3<INTranType.invoice, INTranType.debitMemo>)
    });
    this._IsReceiptType = BqlCommand.Compose(new Type[3]
    {
      typeof (Where<,>),
      TranType,
      typeof (In3<INTranType.receipt, INTranType.issue, INTranType.return_, INTranType.creditMemo>)
    });
    this._IsTransferType = BqlCommand.Compose(new Type[6]
    {
      typeof (Where<,,>),
      TranType,
      typeof (Equal<INTranType.transfer>),
      typeof (And<,>),
      InvtMultType,
      typeof (In3<short1, shortMinus1>)
    });
    this._IsStandardCostAdjType = BqlCommand.Compose(new Type[3]
    {
      typeof (Where<,>),
      TranType,
      typeof (In3<INTranType.standardCostAdjustment, INTranType.negativeCostAdjustment>)
    });
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PrimaryItemRestrictorAttribute(InventoryType, this._IsReceiptType, this._IsSalesType, this._IsTransferType));
    if (!VerifyAllowedOperations)
      return;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new LocationRestrictorAttribute(this._IsReceiptType, this._IsSalesType, this._IsTransferType));
  }

  public LocationAvailAttribute(
    Type InventoryType,
    Type SubItemType,
    Type CostCenterType,
    Type SiteIDType,
    Type IsSalesType,
    Type IsReceiptType,
    Type IsTransferType)
    : base(SiteIDType)
  {
    this._InventoryType = InventoryType;
    this._costCenterType = CostCenterType;
    Type type1 = typeof (IConstant).IsAssignableFrom(this._costCenterType) ? this._costCenterType : BqlTemplate.FromType(typeof (Optional<LocationAvailAttribute.CostCenterPh>)).Replace<LocationAvailAttribute.CostCenterPh>(CostCenterType).ToType();
    this._IsSalesType = IsSalesType;
    this._IsReceiptType = IsReceiptType;
    this._IsTransferType = IsTransferType;
    this._IsStandardCostAdjType = typeof (Where<False>);
    Type type2 = ((IBqlTemplate) BqlTemplate.OfCommand<Search<INLocation.locationID, Where<INLocation.siteID, Equal<Optional<LocationAvailAttribute.SiteIDPh>>>>>.Replace<LocationAvailAttribute.SiteIDPh>(SiteIDType)).ToType();
    Type type3 = BqlTemplate.OfJoin<LeftJoin<INLocationStatusByCostCenter, On<INLocationStatusByCostCenter.locationID, Equal<INLocation.locationID>, And<INLocationStatusByCostCenter.inventoryID, Equal<Optional<LocationAvailAttribute.InventoryPh>>, And<INLocationStatusByCostCenter.subItemID, Equal<Optional<LocationAvailAttribute.SubItemPh>>, And<INLocationStatusByCostCenter.costCenterID, Equal<LocationAvailAttribute.CostCenterPh>>>>>>>.Replace<LocationAvailAttribute.InventoryPh>(InventoryType).Replace<LocationAvailAttribute.SubItemPh>(SubItemType).Replace<LocationAvailAttribute.CostCenterPh>(type1).ToType();
    Type[] array = ((IEnumerable<Type>) new Type[10]
    {
      typeof (INLocation.locationCD),
      typeof (INLocationStatusByCostCenter.qtyOnHand),
      typeof (INLocationStatusByCostCenter.active),
      typeof (INLocation.primaryItemID),
      typeof (INLocation.primaryItemClassID),
      typeof (INLocation.receiptsValid),
      typeof (INLocation.salesValid),
      typeof (INLocation.transfersValid),
      typeof (INLocation.projectID),
      typeof (INLocation.taskID)
    }).Union<Type>((IEnumerable<Type>) this._hiddenLocationStatusColumns).ToArray<Type>();
    ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute) new LocationAttribute.LocationDimensionSelectorAttribute(type2, this.GetSiteIDKeyRelation(SiteIDType), type3, array);
    this._Select = BqlTemplate.OfCommand<Select<INItemSite, Where<INItemSite.inventoryID, Equal<Current<LocationAvailAttribute.InventoryPh>>, And<INItemSite.siteID, Equal<Current2<LocationAvailAttribute.SiteIDPh>>>>>>.Replace<LocationAvailAttribute.InventoryPh>(this._InventoryType).Replace<LocationAvailAttribute.SiteIDPh>(this._SiteIDType).ToCommand();
    if (!(IsReceiptType != (Type) null) || !(IsSalesType != (Type) null) || !(IsTransferType != (Type) null))
      return;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PrimaryItemRestrictorAttribute(InventoryType, IsReceiptType, IsSalesType, IsTransferType));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new LocationRestrictorAttribute(IsReceiptType, IsSalesType, IsTransferType));
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.HideLocationStatusColumns(sender.Graph);
  }

  public override void SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
    {
      base.SiteID_FieldUpdated(sender, e);
      object obj;
      if ((obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal)) != null)
      {
        int? nullable = (int?) obj;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          return;
      }
      PXUIFieldAttribute.SetError(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (string) null);
    }
    try
    {
      if (e.ExternalCall)
      {
        sender.SetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, PXCache.NotSetValue);
        sender.SetDefaultExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
      }
      else
      {
        object obj;
        sender.RaiseFieldDefaulting(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
        if (obj == null)
          return;
        sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, obj);
      }
    }
    catch (PXSetPropertyException ex)
    {
      PXUIFieldAttribute.SetError(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (string) null);
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
    }
  }

  protected virtual void HideLocationStatusColumns(PXGraph graph)
  {
    PXCache cach = graph.Caches[typeof (INLocationStatusByCostCenter)];
    foreach (Type locationStatusColumn in this._hiddenLocationStatusColumns)
      PXUIFieldAttribute.SetVisible(cach, locationStatusColumn.Name, false);
  }

  protected bool? VerifyExpr(PXCache cache, object data, Type whereType)
  {
    if (whereType == (Type) null)
      return new bool?(false);
    object obj = (object) null;
    bool? nullable = new bool?();
    ((IBqlUnary) Activator.CreateInstance(whereType)).Verify(cache, data, new List<object>(), ref nullable, ref obj);
    return nullable;
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.VerifyExpr(sender, e.Row, this._IsStandardCostAdjType).Value)
    {
      e.NewValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      INItemSite source1 = (INItemSite) sender.Graph.TypedViews.GetView(this._Select, false).SelectSingleBound(new object[1]
      {
        e.Row
      }, Array.Empty<object>());
      if (this.UpdateDefault<INItemSite.dfltReceiptLocationID, INItemSite.dfltShipLocationID>(sender, e, (object) source1))
        return;
      INSite source2 = (INSite) PXSelectorAttribute.Select(sender, e.Row, this._SiteIDType.Name);
      this.UpdateDefault<INSite.receiptLocationID, INSite.shipLocationID>(sender, e, (object) source2);
    }
  }

  private bool UpdateDefault<ReceiptLocationID, ShipLocationID>(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    object source)
    where ReceiptLocationID : IBqlField
    where ShipLocationID : IBqlField
  {
    if (source == null)
      return false;
    PXCache cache = sender.Graph.Caches[source.GetType()];
    if (cache.Keys.Exists((Predicate<string>) (key => cache.GetValue(source, key) == null)))
      return false;
    int num = this.VerifyExpr(sender, e.Row, this._IsReceiptType).Value ? 1 : 0;
    object obj1 = num != 0 ? cache.GetValue<ReceiptLocationID>(source) : cache.GetValue<ShipLocationID>(source);
    object obj2 = num != 0 ? cache.GetValueExt<ReceiptLocationID>(source) : cache.GetValueExt<ShipLocationID>(source);
    e.NewValue = !(obj2 is PXFieldState) ? obj2 : ((PXFieldState) obj2).Value;
    try
    {
      sender.RaiseFieldVerifying(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj1);
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) null;
    }
    return true;
  }

  [PXHidden]
  private class InventoryPh : BqlPlaceholderBase
  {
  }

  [PXHidden]
  private class SubItemPh : BqlPlaceholderBase
  {
  }

  [PXHidden]
  private class SiteIDPh : BqlPlaceholderBase
  {
  }

  [PXHidden]
  private class CostCenterPh : BqlPlaceholderBase
  {
  }
}
