// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ItemPlan`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class ItemPlan<TGraph, TRefEntity, TItemPlanSource> : 
  ItemPlanBase<TGraph, TItemPlanSource>,
  IItemPlanHandler<TItemPlanSource>
  where TGraph : PXGraph
  where TRefEntity : class, IBqlTable, new()
  where TItemPlanSource : class, IItemPlanSource, IBqlTable, new()
{
  protected Dictionary<Type, List<PXView>> _viewsToClear;

  public IDisposable ReleaseModeScope()
  {
    return (IDisposable) new ItemPlan<TGraph, TRefEntity, TItemPlanSource>.ReleasingScope(this);
  }

  public bool ReleaseMode { get; private set; }

  public override void Initialize()
  {
    base.Initialize();
    if (!this.Base.Views.Caches.Contains(typeof (INItemPlan)))
    {
      ((RowInsertingEvents) this.Base.RowInserting).AddAbstractHandler<INItemPlan>(new Action<AbstractEvents.IRowInserting<INItemPlan>>(this.PlanRowInserting));
      ((RowInsertedEvents) this.Base.RowInserted).AddAbstractHandler<INItemPlan>(new Action<AbstractEvents.IRowInserted<INItemPlan>>(this.PlanRowInserted));
      ((RowUpdatedEvents) this.Base.RowUpdated).AddAbstractHandler<INItemPlan>(new Action<AbstractEvents.IRowUpdated<INItemPlan>>(this.PlanRowUpdated));
      ((RowDeletedEvents) this.Base.RowDeleted).AddAbstractHandler<INItemPlan>(new Action<AbstractEvents.IRowDeleted<INItemPlan>>(this.PlanRowDeleted));
      ((CommandPreparingEvents) this.Base.CommandPreparing).AddAbstractHandler<INItemPlan.planID>(new Action<AbstractEvents.ICommandPreparing<object, INItemPlan.planID>>(this.ParameterCommandPreparing));
      if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      {
        if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
        {
          ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<INItemPlan.siteID>(new Action<AbstractEvents.IFieldDefaulting<object, INItemPlan.siteID, object>>(this.FeatureFieldDefaulting));
          ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<LocationStatusByCostCenter.siteID>(new Action<AbstractEvents.IFieldDefaulting<object, LocationStatusByCostCenter.siteID, object>>(this.FeatureFieldDefaulting));
          ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.siteID>(new Action<AbstractEvents.IFieldDefaulting<object, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.siteID, object>>(this.FeatureFieldDefaulting));
        }
        if (!PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
        {
          ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<INItemPlan.locationID>(new Action<AbstractEvents.IFieldDefaulting<object, INItemPlan.locationID, object>>(this.FeatureFieldDefaulting));
          ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<LocationStatusByCostCenter.locationID>(new Action<AbstractEvents.IFieldDefaulting<object, LocationStatusByCostCenter.locationID, object>>(this.FeatureFieldDefaulting));
          ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.locationID>(new Action<AbstractEvents.IFieldDefaulting<object, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.locationID, object>>(this.FeatureFieldDefaulting));
        }
      }
    }
    ItemPlanHelper<TGraph>.AddStatusDACsToCacheMapping((PXGraph) this.Base);
    if (!this.Base.Views.Caches.Contains(typeof (TItemPlanSource)))
      this.Base.Views.Caches.Add(typeof (TItemPlanSource));
    if (this.Base.IsImport || this.Base.UnattendedMode)
    {
      if (!this.Base.Views.Caches.Contains(typeof (INItemPlan)))
        this.Base.Views.Caches.Insert(this.Base.Views.Caches.IndexOf(typeof (TItemPlanSource)), typeof (INItemPlan));
    }
    else if (!this.Base.Views.Caches.Contains(typeof (INItemPlan)))
      this.Base.Views.Caches.Add(typeof (INItemPlan));
    if (!this.Base.Views.Caches.Contains(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)))
      this.Base.Views.Caches.Add(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter));
    if (!this.Base.Views.Caches.Contains(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)))
      this.Base.Views.Caches.Add(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial));
    if (!this.Base.Views.Caches.Contains(typeof (SiteLotSerial)))
      this.Base.Views.Caches.Add(typeof (SiteLotSerial));
    if (!this.Base.Views.Caches.Contains(typeof (LocationStatusByCostCenter)))
      this.Base.Views.Caches.Add(typeof (LocationStatusByCostCenter));
    if (!this.Base.Views.Caches.Contains(typeof (SiteStatusByCostCenter)))
      this.Base.Views.Caches.Add(typeof (SiteStatusByCostCenter));
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<SiteStatusByCostCenter.subItemID>(new Action<AbstractEvents.IFieldVerifying<object, SiteStatusByCostCenter.subItemID, object>>(this.SurrogateIDFieldVerifying));
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<LocationStatusByCostCenter.subItemID>(new Action<AbstractEvents.IFieldVerifying<object, LocationStatusByCostCenter.subItemID, object>>(this.SurrogateIDFieldVerifying));
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.subItemID>(new Action<AbstractEvents.IFieldVerifying<object, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.subItemID, object>>(this.SurrogateIDFieldVerifying));
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<LocationStatusByCostCenter.locationID>(new Action<AbstractEvents.IFieldVerifying<object, LocationStatusByCostCenter.locationID, object>>(this.SurrogateIDFieldVerifying));
    ((FieldVerifyingEvents) this.Base.FieldVerifying).AddAbstractHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.locationID>(new Action<AbstractEvents.IFieldVerifying<object, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.locationID, object>>(this.SurrogateIDFieldVerifying));
    ((RowPersistedEvents) this.Base.RowPersisted).AddAbstractHandler<SiteStatusByCostCenter>(new Action<AbstractEvents.IRowPersisted<SiteStatusByCostCenter>>(this.AccumulatorRowPersisted<SiteStatusByCostCenter>));
    ((RowPersistedEvents) this.Base.RowPersisted).AddAbstractHandler<LocationStatusByCostCenter>(new Action<AbstractEvents.IRowPersisted<LocationStatusByCostCenter>>(this.AccumulatorRowPersisted<LocationStatusByCostCenter>));
    ((RowPersistedEvents) this.Base.RowPersisted).AddAbstractHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>(new Action<AbstractEvents.IRowPersisted<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>>(this.AccumulatorRowPersisted<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>));
    ((RowPersistedEvents) this.Base.RowPersisted).AddAbstractHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>(new Action<AbstractEvents.IRowPersisted<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>>(this.AccumulatorRowPersisted<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>));
    ((RowPersistedEvents) this.Base.RowPersisted).AddAbstractHandler<SiteLotSerial>(new Action<AbstractEvents.IRowPersisted<SiteLotSerial>>(this.AccumulatorRowPersisted<SiteLotSerial>));
    ((CommandPreparingEvents) this.Base.CommandPreparing).AddAbstractHandler<TItemPlanSource>("PlanID", new Action<AbstractEvents.ICommandPreparing<TItemPlanSource, IBqlField>>(this.ParameterCommandPreparing));
  }

  protected virtual void PlanRowInserting(AbstractEvents.IRowInserting<INItemPlan> e)
  {
    if (e.Row == null || e.Row.InventoryID.HasValue)
      return;
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void PlanRowInserted(AbstractEvents.IRowInserted<INItemPlan> e)
  {
    this.UpdateAllocatedQuantitiesWithPlan(e.Row, false);
  }

  protected virtual void PlanRowDeleted(AbstractEvents.IRowDeleted<INItemPlan> e)
  {
    this.UpdateAllocatedQuantitiesWithPlan(e.Row, true);
  }

  protected virtual void PlanRowUpdated(AbstractEvents.IRowUpdated<INItemPlan> e)
  {
    this.UpdateAllocatedQuantitiesWithPlan(e.Row, false);
    this.UpdateAllocatedQuantitiesWithPlan(e.OldRow, true);
  }

  public virtual void _(Events.RowInserted<TItemPlanSource> e)
  {
    if (this.ReleaseMode)
      return;
    INItemPlan planRow = this.FetchPlan(e.Row);
    if (planRow == null)
    {
      INItemPlan inItemPlan1 = this.DefaultValuesInt(new INItemPlan(), e.Row);
      if (inItemPlan1 == null)
        return;
      INItemPlan inItemPlan2 = this.PlanCache.Insert(inItemPlan1);
      this.SetPlanID(e.Row, inItemPlan2.PlanID);
    }
    else
    {
      INItemPlan copy = PXCache<INItemPlan>.CreateCopy(planRow);
      INItemPlan inItemPlan3 = this.DefaultValuesInt(planRow, e.Row);
      if (inItemPlan3 == null)
        return;
      if (!this.PlanCache.ObjectsEqual(inItemPlan3, copy))
      {
        inItemPlan3.PlanID = new long?();
        this.PlanCache.Delete(copy);
        INItemPlan inItemPlan4 = this.PlanCache.Insert(inItemPlan3);
        this.SetPlanID(e.Row, inItemPlan4.PlanID);
      }
      else
        this.PlanCache.Update(inItemPlan3);
    }
  }

  public virtual void _(Events.RowUpdated<TItemPlanSource> e) => this.RowUpdatedImpl(e.Row);

  public virtual void _(Events.RowDeleted<TItemPlanSource> e)
  {
    if (this.ReleaseMode)
      return;
    INItemPlan inItemPlan = this.FetchPlan(e.Row);
    if (inItemPlan == null)
      return;
    this.PlanCache.Delete(inItemPlan);
  }

  public virtual void _(Events.RowInserted<TRefEntity> e)
  {
    PXNoteAttribute.GetNoteID(((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TRefEntity>>) e).Cache, (object) e.Row, "noteID");
    ((PXCache) GraphHelper.Caches<Note>((PXGraph) this.Base)).IsDirty = false;
  }

  public virtual void _(Events.RowUpdated<TRefEntity> e)
  {
    PXNoteAttribute.GetNoteID(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TRefEntity>>) e).Cache, (object) e.Row, "noteID");
  }

  protected virtual void ParameterCommandPreparing(
    AbstractEvents.ICommandPreparing<object, IBqlField> e)
  {
    if ((e.Operation & 3) != null || (e.Operation & 124) == 16 /*0x10*/ || (e.Operation & 124) == 64 /*0x40*/ || e.Row != null || !(e.Value is long num) || num >= 0L)
      return;
    e.DataValue = (object) null;
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void AccumulatorRowPersisted<TAccumulator>(
    AbstractEvents.IRowPersisted<TAccumulator> e)
    where TAccumulator : class, IBqlTable, IQtyAllocatedBase
  {
    if (e.Operation == 3 || e.TranStatus != 1)
      return;
    this.Clear<TAccumulator>();
  }

  protected virtual void FeatureFieldDefaulting(
    AbstractEvents.IFieldDefaulting<object, IBqlField, object> e)
  {
    e.NewValue = (object) null;
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void SurrogateIDFieldVerifying(
    AbstractEvents.IFieldVerifying<object, IBqlField, object> e)
  {
    if (!(e.NewValue is int))
      return;
    ((ICancelEventArgs) e).Cancel = true;
  }

  public void RaiseRowUpdated(TItemPlanSource row) => this.RowUpdatedImpl(row);

  private void RowUpdatedImpl(TItemPlanSource row)
  {
    if (this.ReleaseMode)
      return;
    INItemPlan planRow = this.FetchPlan(row);
    if (planRow == null)
    {
      INItemPlan inItemPlan1 = this.DefaultValuesInt(new INItemPlan(), row);
      if (inItemPlan1 == null)
        return;
      INItemPlan inItemPlan2 = this.PlanCache.Insert(inItemPlan1);
      this.SetPlanID(row, inItemPlan2.PlanID);
    }
    else
    {
      INItemPlan copy = PXCache<INItemPlan>.CreateCopy(planRow);
      INItemPlan info = this.DefaultValuesInt(planRow, row);
      if (info != null)
      {
        if (this.IsPlanInfoChanged(info, copy))
        {
          info.PlanID = new long?();
          this.PlanCache.Delete(copy);
          INItemPlan inItemPlan3 = this.PlanCache.Insert(info);
          this.SetPlanID(row, inItemPlan3.PlanID);
          foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) copy.PlanID
          }))
          {
            INItemPlan inItemPlan4 = PXResult<INItemPlan>.op_Implicit(pxResult);
            inItemPlan4.SupplyPlanID = inItemPlan3.PlanID;
            GraphHelper.MarkUpdated((PXCache) this.PlanCache, (object) inItemPlan4, true);
          }
        }
        else
          this.PlanCache.Update(info);
      }
      else
      {
        this.PlanCache.Delete(copy);
        this.ClearPlanID(row);
      }
    }
  }

  public abstract INItemPlan DefaultValues(INItemPlan planRow, TItemPlanSource origRow);

  protected INItemPlan DefaultValuesInt(INItemPlan planRow, TItemPlanSource origRow)
  {
    INItemPlan inItemPlan = this.DefaultValues(planRow, origRow);
    if (inItemPlan == null || !inItemPlan.InventoryID.HasValue || !inItemPlan.SiteID.HasValue)
      return (INItemPlan) null;
    inItemPlan.RefEntityType = this.GetRefEntityType();
    return inItemPlan;
  }

  protected virtual string GetRefEntityType() => typeof (TRefEntity).FullName;

  protected virtual bool CanUpdateAllocatedQuantitiesWithPlan(INItemPlan plan)
  {
    if (plan.InventoryID.HasValue && plan.SubItemID.HasValue && plan.SiteID.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, plan.InventoryID);
      if (inventoryItem != null)
        return inventoryItem.StkItem.GetValueOrDefault();
    }
    return false;
  }

  protected virtual void UpdateAllocatedQuantitiesWithPlan(INItemPlan plan, bool revert)
  {
    INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this.Base, plan.PlanType);
    INPlanType plantype = revert ? -inPlanType : inPlanType;
    if (!this.CanUpdateAllocatedQuantitiesWithPlan(plan))
      return;
    if (plan.LocationID.HasValue)
    {
      LocationStatusByCostCenter statusByCostCenter = this.UpdateAllocatedQuantities<LocationStatusByCostCenter>(plan, plantype, true);
      this.UpdateAllocatedQuantities<SiteStatusByCostCenter>(plan, plantype, statusByCostCenter.InclQtyAvail.Value);
      if (string.IsNullOrEmpty(plan.LotSerialNbr))
        return;
      this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>(plan, plantype, true);
      this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>(plan, plantype, true);
      this.UpdateAllocatedQuantities<SiteLotSerial>(plan, plantype, statusByCostCenter.InclQtyAvail.Value);
    }
    else
    {
      this.UpdateAllocatedQuantities<SiteStatusByCostCenter>(plan, plantype, true);
      if (string.IsNullOrEmpty(plan.LotSerialNbr))
        return;
      this.UpdateAllocatedQuantities<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>(plan, plantype, true);
      this.UpdateAllocatedQuantities<SiteLotSerial>(plan, plantype, true);
    }
  }

  protected TNode UpdateAllocatedQuantities<TNode>(
    INItemPlan plan,
    INPlanType plantype,
    bool inclQtyAvail)
    where TNode : class, IQtyAllocatedBase
  {
    INPlanType targetPlanType = this.GetTargetPlanType<TNode>(plan, plantype);
    return this.UpdateAllocatedQuantitiesBase<TNode>(plan, targetPlanType, inclQtyAvail);
  }

  public virtual AvailabilitySigns GetAvailabilitySigns<TNode>(TItemPlanSource data) where TNode : class, IQtyAllocatedBase, IBqlTable, new()
  {
    INItemPlan plan = this.DefaultValuesInt(new INItemPlan()
    {
      IsTemporary = new bool?(true)
    }, data);
    if (plan == null)
      return new AvailabilitySigns();
    INPlanType plantype = INPlanType.PK.Find((PXGraph) this.Base, plan.PlanType);
    INPlanType targetPlanType = this.GetTargetPlanType<TNode>(plan, plantype);
    return this.GetAvailabilitySigns<TNode>(plan, targetPlanType);
  }

  protected AvailabilitySigns GetAvailabilitySigns<TNode>(INItemPlan plan, INPlanType plantype) where TNode : class, IQtyAllocatedBase, IBqlTable, new()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    TNode node = this.InsertWith<TNode>(this.ConvertPlan<TNode>(plan), ItemPlan<TGraph, TRefEntity, TItemPlanSource>.\u003C\u003Ec__30<TNode>.\u003C\u003E9__30_0 ?? (ItemPlan<TGraph, TRefEntity, TItemPlanSource>.\u003C\u003Ec__30<TNode>.\u003C\u003E9__30_0 = new PXRowInserted((object) ItemPlan<TGraph, TRefEntity, TItemPlanSource>.\u003C\u003Ec__30<TNode>.\u003C\u003E9, __methodptr(\u003CGetAvailabilitySigns\u003Eb__30_0))));
    Decimal signQtyAvail = 0M;
    bool? nullable1 = plan.Reverse;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = node.InclQtySOReverse;
      if (!nullable1.GetValueOrDefault() && this.IsSORelated(plantype))
        goto label_84;
    }
    Decimal num1 = signQtyAvail;
    nullable1 = node.InclQtyINIssues;
    short? nullable2;
    Decimal num2;
    if (!nullable1.GetValueOrDefault())
    {
      num2 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyINIssues;
      num2 = (Decimal) nullable2.Value;
    }
    Decimal num3 = num1 - num2;
    nullable1 = node.InclQtyINReceipts;
    Decimal num4;
    if (!nullable1.GetValueOrDefault())
    {
      num4 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyINReceipts;
      num4 = (Decimal) nullable2.Value;
    }
    Decimal num5 = num3 + num4;
    nullable1 = node.InclQtyInTransit;
    Decimal num6;
    if (!nullable1.GetValueOrDefault())
    {
      num6 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyInTransit;
      num6 = (Decimal) nullable2.Value;
    }
    Decimal num7 = num5 + num6;
    nullable1 = node.InclQtyPOPrepared;
    Decimal num8;
    if (!nullable1.GetValueOrDefault())
    {
      num8 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyPOPrepared;
      num8 = (Decimal) nullable2.Value;
    }
    Decimal num9 = num7 + num8;
    nullable1 = node.InclQtyPOOrders;
    Decimal num10;
    if (!nullable1.GetValueOrDefault())
    {
      num10 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyPOOrders;
      num10 = (Decimal) nullable2.Value;
    }
    Decimal num11 = num9 + num10;
    nullable1 = node.InclQtyPOReceipts;
    Decimal num12;
    if (!nullable1.GetValueOrDefault())
    {
      num12 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyPOReceipts;
      num12 = (Decimal) nullable2.Value;
    }
    Decimal num13 = num11 + num12;
    nullable1 = node.InclQtyINAssemblySupply;
    Decimal num14;
    if (!nullable1.GetValueOrDefault())
    {
      num14 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyINAssemblySupply;
      num14 = (Decimal) nullable2.Value;
    }
    Decimal num15 = num13 + num14;
    nullable1 = node.InclQtyProductionSupplyPrepared;
    Decimal num16;
    if (!nullable1.GetValueOrDefault())
    {
      num16 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyProductionSupplyPrepared;
      num16 = (Decimal) nullable2.Value;
    }
    Decimal num17 = num15 + num16;
    nullable1 = node.InclQtyProductionSupply;
    Decimal num18;
    if (!nullable1.GetValueOrDefault())
    {
      num18 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyProductionSupply;
      num18 = (Decimal) nullable2.Value;
    }
    Decimal num19 = num17 + num18;
    nullable1 = node.InclQtySOBackOrdered;
    Decimal num20;
    if (!nullable1.GetValueOrDefault())
    {
      num20 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtySOBackOrdered;
      num20 = (Decimal) nullable2.Value;
    }
    Decimal num21 = num19 - num20;
    nullable1 = node.InclQtySOPrepared;
    Decimal num22;
    if (!nullable1.GetValueOrDefault())
    {
      num22 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtySOPrepared;
      num22 = (Decimal) nullable2.Value;
    }
    Decimal num23 = num21 - num22;
    nullable1 = node.InclQtySOBooked;
    Decimal num24;
    if (!nullable1.GetValueOrDefault())
    {
      num24 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtySOBooked;
      num24 = (Decimal) nullable2.Value;
    }
    Decimal num25 = num23 - num24;
    nullable1 = node.InclQtySOShipped;
    Decimal num26;
    if (!nullable1.GetValueOrDefault())
    {
      num26 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtySOShipped;
      num26 = (Decimal) nullable2.Value;
    }
    Decimal num27 = num25 - num26;
    nullable1 = node.InclQtySOShipping;
    Decimal num28;
    if (!nullable1.GetValueOrDefault())
    {
      num28 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtySOShipping;
      num28 = (Decimal) nullable2.Value;
    }
    Decimal num29 = num27 - num28;
    nullable1 = node.InclQtyINAssemblyDemand;
    Decimal num30;
    if (!nullable1.GetValueOrDefault())
    {
      num30 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyINAssemblyDemand;
      num30 = (Decimal) nullable2.Value;
    }
    Decimal num31 = num29 - num30;
    nullable1 = node.InclQtyProductionDemandPrepared;
    Decimal num32;
    if (!nullable1.GetValueOrDefault())
    {
      num32 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyProductionDemandPrepared;
      num32 = (Decimal) nullable2.Value;
    }
    Decimal num33 = num31 - num32;
    nullable1 = node.InclQtyProductionDemand;
    Decimal num34;
    if (!nullable1.GetValueOrDefault())
    {
      num34 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyProductionDemand;
      num34 = (Decimal) nullable2.Value;
    }
    Decimal num35 = num33 - num34;
    nullable1 = node.InclQtyProductionAllocated;
    Decimal num36;
    if (!nullable1.GetValueOrDefault())
    {
      num36 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyProductionAllocated;
      num36 = (Decimal) nullable2.Value;
    }
    Decimal num37 = num35 - num36;
    nullable1 = node.InclQtyFSSrvOrdPrepared;
    Decimal num38;
    if (!nullable1.GetValueOrDefault())
    {
      num38 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyFSSrvOrdPrepared;
      num38 = (Decimal) nullable2.Value;
    }
    Decimal num39 = num37 - num38;
    nullable1 = node.InclQtyFSSrvOrdBooked;
    Decimal num40;
    if (!nullable1.GetValueOrDefault())
    {
      num40 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyFSSrvOrdBooked;
      num40 = (Decimal) nullable2.Value;
    }
    Decimal num41 = num39 - num40;
    nullable1 = node.InclQtyFSSrvOrdAllocated;
    Decimal num42;
    if (!nullable1.GetValueOrDefault())
    {
      num42 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyFSSrvOrdAllocated;
      num42 = (Decimal) nullable2.Value;
    }
    Decimal num43 = num41 - num42;
    nullable1 = node.InclQtyPOFixedReceipt;
    Decimal num44;
    if (!nullable1.GetValueOrDefault())
    {
      num44 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtyPOFixedReceipts;
      num44 = (Decimal) nullable2.Value;
    }
    signQtyAvail = num43 + num44;
    nullable1 = node.InclQtyFixedSOPO;
    if (nullable1.GetValueOrDefault())
    {
      Decimal num45 = signQtyAvail;
      nullable1 = node.InclQtyPOOrders;
      Decimal num46;
      if (!nullable1.GetValueOrDefault())
      {
        num46 = 0M;
      }
      else
      {
        nullable2 = plantype.InclQtyPOFixedOrders;
        num46 = (Decimal) nullable2.Value;
      }
      Decimal num47 = num45 + num46;
      nullable1 = node.InclQtyPOPrepared;
      Decimal num48;
      if (!nullable1.GetValueOrDefault())
      {
        num48 = 0M;
      }
      else
      {
        nullable2 = plantype.InclQtyPOFixedPrepared;
        num48 = (Decimal) nullable2.Value;
      }
      Decimal num49 = num47 + num48;
      nullable1 = node.InclQtyPOReceipts;
      Decimal num50;
      if (!nullable1.GetValueOrDefault())
      {
        num50 = 0M;
      }
      else
      {
        nullable2 = plantype.InclQtyPOFixedReceipts;
        num50 = (Decimal) nullable2.Value;
      }
      Decimal num51 = num49 + num50;
      nullable1 = node.InclQtySOBooked;
      Decimal num52;
      if (!nullable1.GetValueOrDefault())
      {
        num52 = 0M;
      }
      else
      {
        nullable2 = plantype.InclQtySOFixed;
        num52 = (Decimal) nullable2.Value;
      }
      signQtyAvail = num51 - num52;
    }
    nullable1 = plan.Reverse;
    if (nullable1.GetValueOrDefault())
      signQtyAvail = -signQtyAvail;
label_84:
    Decimal signQtyHardAvail = 0M;
    nullable1 = plan.Reverse;
    if (!nullable1.GetValueOrDefault())
    {
      Decimal num53 = signQtyHardAvail;
      nullable2 = plantype.InclQtySOShipped;
      Decimal num54 = (Decimal) nullable2.Value;
      Decimal num55 = num53 - num54;
      nullable2 = plantype.InclQtySOShipping;
      Decimal num56 = (Decimal) nullable2.Value;
      Decimal num57 = num55 - num56;
      nullable2 = plantype.InclQtyINIssues;
      Decimal num58 = (Decimal) nullable2.Value;
      Decimal num59 = num57 - num58;
      nullable2 = plantype.InclQtyProductionAllocated;
      Decimal num60 = (Decimal) nullable2.Value;
      Decimal num61 = num59 - num60;
      nullable2 = plantype.InclQtyFSSrvOrdAllocated;
      Decimal num62 = (Decimal) nullable2.Value;
      Decimal num63 = num61 - num62;
      nullable2 = plantype.InclQtyINAssemblyDemand;
      Decimal num64 = (Decimal) nullable2.Value;
      signQtyHardAvail = num63 - num64;
    }
    nullable1 = plan.Reverse;
    Decimal num65;
    if (nullable1.GetValueOrDefault())
    {
      num65 = 0M;
    }
    else
    {
      nullable2 = plantype.InclQtySOShipped;
      num65 = -(Decimal) nullable2.Value;
    }
    Decimal signQtyActual = num65;
    return new AvailabilitySigns(signQtyAvail, signQtyHardAvail, signQtyActual);
  }

  protected virtual bool IsPlanInfoChanged(INItemPlan info, INItemPlan oldInfo)
  {
    if (this.PlanCache.ObjectsEqual(info, oldInfo) && (string.Equals(info.LotSerialNbr, oldInfo.LotSerialNbr, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(info.LotSerialNbr) && string.IsNullOrEmpty(oldInfo.LotSerialNbr)))
    {
      int? projectId1 = info.ProjectID;
      int? projectId2 = oldInfo.ProjectID;
      if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue)
      {
        int? taskId1 = info.TaskID;
        int? taskId2 = oldInfo.TaskID;
        return !(taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue);
      }
    }
    return true;
  }

  protected virtual void SetPlanID(TItemPlanSource row, long? planID) => row.PlanID = planID;

  protected virtual void ClearPlanID(TItemPlanSource row) => row.PlanID = new long?();

  protected virtual INItemPlan FetchCachedPlan(TItemPlanSource origRow)
  {
    return this.PlanCache.Locate(new INItemPlan()
    {
      PlanID = origRow.PlanID,
      InventoryID = origRow.InventoryID,
      SiteID = origRow.SiteID
    });
  }

  protected virtual INItemPlan FetchPlan(TItemPlanSource origRow)
  {
    INItemPlan inItemPlan1 = (INItemPlan) null;
    if (origRow.PlanID.HasValue)
    {
      long? planId1 = origRow.PlanID;
      long num1 = 0;
      if (planId1.GetValueOrDefault() < num1 & planId1.HasValue)
        inItemPlan1 = this.FetchCachedPlan(origRow);
      if (inItemPlan1 == null)
      {
        long? planId2 = origRow.PlanID;
        long num2 = 0;
        if (planId2.GetValueOrDefault() < num2 & planId2.HasValue)
        {
          foreach (INItemPlan inItemPlan2 in ((PXCache) this.PlanCache).Inserted)
          {
            long? planId3 = inItemPlan2.PlanID;
            long? planId4 = origRow.PlanID;
            if (planId3.GetValueOrDefault() == planId4.GetValueOrDefault() & planId3.HasValue == planId4.HasValue)
            {
              inItemPlan1 = inItemPlan2;
              break;
            }
          }
        }
        else
          inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) origRow.PlanID
          }));
      }
      if (inItemPlan1 != null)
        return PXCache<INItemPlan>.CreateCopy(inItemPlan1);
      origRow.PlanID = new long?();
    }
    return inItemPlan1;
  }

  protected void Clear<TNode>() where TNode : class, IBqlTable
  {
    if (this._viewsToClear == null)
      this._viewsToClear = new Dictionary<Type, List<PXView>>();
    List<PXView> pxViewList;
    if (!this._viewsToClear.TryGetValue(typeof (TNode), out pxViewList))
    {
      pxViewList = this._viewsToClear[typeof (TNode)] = new List<PXView>();
      foreach (PXView pxView in new List<PXView>((IEnumerable<PXView>) ((Dictionary<string, PXView>) this.Base.Views).Values))
      {
        if (typeof (TNode).IsAssignableFrom(pxView.GetItemType()))
          pxViewList.Add(pxView);
      }
      foreach (PXView pxView in new List<PXView>((IEnumerable<PXView>) ((Dictionary<Type, PXView>) this.Base.TypedViews).Values))
      {
        if (typeof (TNode).IsAssignableFrom(pxView.GetItemType()))
          pxViewList.Add(pxView);
      }
      foreach (PXView pxView in new List<PXView>((IEnumerable<PXView>) this.Base.TypedViews.ReadOnlyValues))
      {
        if (typeof (TNode).IsAssignableFrom(pxView.GetItemType()))
          pxViewList.Add(pxView);
      }
    }
    foreach (PXView pxView in pxViewList)
    {
      pxView.Clear();
      pxView.Cache.Clear();
    }
  }

  protected T InsertWith<T>(T row, PXRowInserted handler) where T : class, IBqlTable, new()
  {
    this.Base.RowInserted.AddHandler<T>(handler);
    try
    {
      return PXCache<T>.Insert((PXGraph) this.Base, row);
    }
    finally
    {
      this.Base.RowInserted.RemoveHandler<T>(handler);
    }
  }

  private class ReleasingScope : IDisposable
  {
    private readonly bool _initReleaseMode;
    private readonly ItemPlan<TGraph, TRefEntity, TItemPlanSource> _itemPlanExt;

    public ReleasingScope(
      ItemPlan<TGraph, TRefEntity, TItemPlanSource> itemPlanExt)
    {
      this._itemPlanExt = itemPlanExt;
      this._initReleaseMode = this._itemPlanExt.ReleaseMode;
      this._itemPlanExt.ReleaseMode = true;
    }

    void IDisposable.Dispose() => this._itemPlanExt.ReleaseMode = this._initReleaseMode;
  }
}
