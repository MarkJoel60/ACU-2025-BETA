// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ItemPlanBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class ItemPlanBase<TGraph, TItemPlanSource> : ItemPlanHelper<TGraph>
  where TGraph : PXGraph
  where TItemPlanSource : class, IItemPlanSource, IBqlTable, new()
{
  private PXCache<TItemPlanSource> _itemPlanSourceCache;
  private PXCache<INItemPlan> _planCache;
  protected long? _selfKeyToAbort;
  protected Dictionary<long?, long?> _persistedToAbort;
  protected Dictionary<long?, TItemPlanSource> _inserted;
  protected Dictionary<long?, TItemPlanSource> _updated;
  protected Dictionary<long?, List<INItemPlan>> _selfInserted;
  protected Dictionary<long?, List<INItemPlan>> _selfUpdated;

  public PXCache<TItemPlanSource> ItemPlanSourceCache
  {
    get
    {
      return this._itemPlanSourceCache ?? (this._itemPlanSourceCache = GraphHelper.Caches<TItemPlanSource>((PXGraph) this.Base));
    }
  }

  public PXCache<INItemPlan> PlanCache
  {
    get
    {
      return this._planCache ?? (this._planCache = GraphHelper.Caches<INItemPlan>((PXGraph) this.Base));
    }
  }

  protected bool PersistedToAbortLocked
  {
    get => PXContext.GetSlot<bool>("ItemPlanBase.PersistedToAbortLocked");
    set => PXContext.SetSlot<bool>("ItemPlanBase.PersistedToAbortLocked", value);
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this._persistedToAbort = new Dictionary<long?, long?>();
    if (!this.Base.Views.RestorableCaches.Contains(typeof (INItemPlan)))
      this.Base.Views.RestorableCaches.Add(typeof (INItemPlan));
    this.Base.OnBeforeCommit += new Action<PXGraph>(this.VerifyUnsavedPlans);
  }

  public virtual void _(Events.RowPersisting<TItemPlanSource> e)
  {
    if (!e.Row.PlanID.HasValue)
      return;
    long? planId1 = e.Row.PlanID;
    long num = 0;
    if (planId1.GetValueOrDefault() < num & planId1.HasValue)
    {
      bool flag = false;
      foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Inserted)
      {
        planId1 = e.Row.PlanID;
        long? planId2 = inItemPlan.PlanID;
        if (planId1.GetValueOrDefault() == planId2.GetValueOrDefault() & planId1.HasValue == planId2.HasValue)
        {
          flag = true;
          try
          {
            ((PXCache) this.PlanCache).PersistInserted((object) inItemPlan);
          }
          catch (PXOuterException ex)
          {
            for (int index = 0; index < ex.InnerFields.Length; ++index)
            {
              if (((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TItemPlanSource>>) e).Cache.RaiseExceptionHandling(ex.InnerFields[index], (object) e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(ex.InnerMessages[index])))
                throw new PXRowPersistingException(ex.InnerFields[index], (object) null, ex.InnerMessages[index]);
            }
            return;
          }
          long int64 = Convert.ToInt64((object) PXDatabase.SelectIdentity());
          e.Row.PlanID = new long?(int64);
          inItemPlan.PlanID = new long?(int64);
          ((PXCache) this.PlanCache).Normalize();
          break;
        }
      }
      if (!flag && EnumerableExtensions.IsNotIn<PXEntryStatus>(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TItemPlanSource>>) e).Cache.GetStatus((object) e.Row), (PXEntryStatus) 3, (PXEntryStatus) 4))
        throw new PXException("A transaction is missing allocation details. Please, delete current document and create a new one.");
    }
    else
    {
      foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Updated)
      {
        long? planId3 = e.Row.PlanID;
        planId1 = inItemPlan.PlanID;
        if (planId3.GetValueOrDefault() == planId1.GetValueOrDefault() & planId3.HasValue == planId1.HasValue)
        {
          ((PXCache) this.PlanCache).PersistUpdated((object) inItemPlan);
          break;
        }
      }
    }
  }

  public virtual void _(Events.RowPersisted<TItemPlanSource> e)
  {
    if (e.TranStatus == 2)
    {
      long? nullable1 = e.Row.PlanID;
      long? nullable2;
      if (nullable1.HasValue && this._persistedToAbort.TryGetValue(e.Row.PlanID, out nullable2))
      {
        long? planId1 = e.Row.PlanID;
        e.Row.PlanID = nullable2;
        foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Inserted)
        {
          nullable1 = planId1;
          long? planId2 = inItemPlan.PlanID;
          if (nullable1.GetValueOrDefault() == planId2.GetValueOrDefault() & nullable1.HasValue == planId2.HasValue)
          {
            try
            {
              this.PersistedToAbortLocked = true;
              this.PlanCache.RaiseRowPersisted(inItemPlan, (PXDBOperation) 2, e.TranStatus, ((Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TItemPlanSource>>) e).Args.Exception);
            }
            finally
            {
              this.PersistedToAbortLocked = false;
            }
            inItemPlan.PlanID = nullable2;
            break;
          }
        }
      }
      else
      {
        foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Updated)
        {
          long? planId = e.Row.PlanID;
          nullable1 = inItemPlan.PlanID;
          if (planId.GetValueOrDefault() == nullable1.GetValueOrDefault() & planId.HasValue == nullable1.HasValue)
          {
            try
            {
              this.PersistedToAbortLocked = true;
              this.PlanCache.RaiseRowPersisted(inItemPlan, (PXDBOperation) 1, e.TranStatus, ((Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TItemPlanSource>>) e).Args.Exception);
            }
            finally
            {
              this.PersistedToAbortLocked = false;
            }
            ((PXCache) this.PlanCache).ResetPersisted((object) inItemPlan);
          }
        }
      }
      ((PXCache) this.PlanCache).Normalize();
    }
    else
    {
      if (e.TranStatus != 1)
        return;
      foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Inserted)
      {
        long? planId3 = e.Row.PlanID;
        long? planId4 = inItemPlan.PlanID;
        if (planId3.GetValueOrDefault() == planId4.GetValueOrDefault() & planId3.HasValue == planId4.HasValue)
        {
          this.PlanCache.RaiseRowPersisted(inItemPlan, (PXDBOperation) 2, e.TranStatus, ((Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TItemPlanSource>>) e).Args.Exception);
          ((PXCache) this.PlanCache).SetStatus((object) inItemPlan, (PXEntryStatus) 0);
          PXTimeStampScope.PutPersisted((PXCache) this.PlanCache, (object) inItemPlan, new object[1]
          {
            (object) this.Base.TimeStamp
          });
          ((PXCache) this.PlanCache).ResetPersisted((object) inItemPlan);
        }
      }
      foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Updated)
      {
        long? planId5 = e.Row.PlanID;
        long? planId6 = inItemPlan.PlanID;
        if (planId5.GetValueOrDefault() == planId6.GetValueOrDefault() & planId5.HasValue == planId6.HasValue)
        {
          this.PlanCache.RaiseRowPersisted(inItemPlan, (PXDBOperation) 1, e.TranStatus, ((Events.Event<PXRowPersistedEventArgs, Events.RowPersisted<TItemPlanSource>>) e).Args.Exception);
          ((PXCache) this.PlanCache).SetStatus((object) inItemPlan, (PXEntryStatus) 0);
          PXTimeStampScope.PutPersisted((PXCache) this.PlanCache, (object) inItemPlan, new object[1]
          {
            (object) this.Base.TimeStamp
          });
          ((PXCache) this.PlanCache).ResetPersisted((object) inItemPlan);
        }
      }
      ((PXCache) this.PlanCache).Normalize();
    }
  }

  public virtual void _(Events.RowPersisting<INItemPlan> e)
  {
    if (e.Operation != 2)
      return;
    if (e.Row.IsTempLotSerial.GetValueOrDefault())
      e.Cancel = true;
    else
      this._selfKeyToAbort = e.Row.PlanID;
  }

  protected virtual void _(Events.RowPersisted<INItemPlan> e)
  {
    this.PlanForSupplyRowPersisted(e.Row, e.Operation, e.TranStatus);
    this.PlanForItemRowPersisted(e.Row, e.Operation, e.TranStatus);
    if (e.TranStatus != null)
      return;
    this._selfKeyToAbort = new long?();
  }

  protected virtual void VerifyUnsavedPlans(PXGraph graph)
  {
    if (((PXCache) GraphHelper.Caches<INItemPlan>(graph)).Inserted.Cast<INItemPlan>().Any<INItemPlan>((Func<INItemPlan, bool>) (p => p.IsTempLotSerial.GetValueOrDefault())))
      throw new PXException("A transaction is missing allocation details. Please, delete current document and create a new one.");
  }

  protected virtual void PlanForItemRowPersisted(
    INItemPlan plan,
    PXDBOperation operation,
    PXTranStatus tranStatus)
  {
    if (operation != 2)
      return;
    if (tranStatus == null && this._selfKeyToAbort.HasValue)
    {
      if (!this._persistedToAbort.ContainsKey(plan.PlanID))
        this._persistedToAbort.Add(plan.PlanID, this._selfKeyToAbort);
      if (this._inserted == null)
        this._inserted = ((PXCache) this.ItemPlanSourceCache).Inserted.Cast<TItemPlanSource>().Where<TItemPlanSource>((Func<TItemPlanSource, bool>) (item => item.PlanID.HasValue)).ToDictionary<TItemPlanSource, long?>((Func<TItemPlanSource, long?>) (item => item.PlanID));
      TItemPlanSource itemPlanSource;
      if (this._inserted.TryGetValue(this._selfKeyToAbort, out itemPlanSource))
        itemPlanSource.PlanID = plan.PlanID;
      if (this._updated == null)
        this._updated = ((PXCache) this.ItemPlanSourceCache).Updated.Cast<TItemPlanSource>().Where<TItemPlanSource>((Func<TItemPlanSource, bool>) (item => item.PlanID.HasValue)).ToDictionary<TItemPlanSource, long?>((Func<TItemPlanSource, long?>) (item => item.PlanID));
      if (this._updated.TryGetValue(this._selfKeyToAbort, out itemPlanSource))
        itemPlanSource.PlanID = plan.PlanID;
    }
    if (tranStatus == 2 && !this.PersistedToAbortLocked)
    {
      foreach (TItemPlanSource itemPlanSource in NonGenericIEnumerableExtensions.Concat_(((PXCache) this.ItemPlanSourceCache).Inserted, ((PXCache) this.ItemPlanSourceCache).Updated))
      {
        long? nullable;
        if (itemPlanSource.PlanID.HasValue && this._persistedToAbort.TryGetValue(itemPlanSource.PlanID, out nullable))
          itemPlanSource.PlanID = nullable;
      }
    }
    if (!EnumerableExtensions.IsIn<PXTranStatus>(tranStatus, (PXTranStatus) 1, (PXTranStatus) 2))
      return;
    this._inserted = (Dictionary<long?, TItemPlanSource>) null;
    this._updated = (Dictionary<long?, TItemPlanSource>) null;
  }

  protected virtual void PlanForSupplyRowPersisted(
    INItemPlan plan,
    PXDBOperation operation,
    PXTranStatus tranStatus)
  {
    if ((operation & 3) == 2)
    {
      if (tranStatus == null && this._selfKeyToAbort.HasValue)
      {
        List<INItemPlan> inItemPlanList;
        if (this._selfInserted == null)
        {
          this._selfInserted = new Dictionary<long?, List<INItemPlan>>();
          foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Inserted)
          {
            if (inItemPlan.SupplyPlanID.HasValue)
            {
              if (!this._selfInserted.TryGetValue(inItemPlan.SupplyPlanID, out inItemPlanList))
                this._selfInserted[inItemPlan.SupplyPlanID] = inItemPlanList = new List<INItemPlan>();
              inItemPlanList.Add(inItemPlan);
            }
          }
        }
        if (this._selfInserted.TryGetValue(this._selfKeyToAbort, out inItemPlanList))
        {
          foreach (INItemPlan inItemPlan in inItemPlanList)
            inItemPlan.SupplyPlanID = plan.PlanID;
        }
        if (this._selfUpdated == null)
        {
          this._selfUpdated = new Dictionary<long?, List<INItemPlan>>();
          foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Updated)
          {
            if (inItemPlan.SupplyPlanID.HasValue)
            {
              if (!this._selfUpdated.TryGetValue(inItemPlan.SupplyPlanID, out inItemPlanList))
                this._selfUpdated[inItemPlan.SupplyPlanID] = inItemPlanList = new List<INItemPlan>();
              inItemPlanList.Add(inItemPlan);
            }
          }
        }
        if (this._selfUpdated.TryGetValue(this._selfKeyToAbort, out inItemPlanList))
        {
          foreach (INItemPlan inItemPlan in inItemPlanList)
            inItemPlan.SupplyPlanID = plan.PlanID;
        }
      }
      else if (tranStatus == 2)
      {
        foreach (INItemPlan inItemPlan in NonGenericIEnumerableExtensions.Concat_(((PXCache) this.PlanCache).Inserted, ((PXCache) this.PlanCache).Updated))
        {
          long? nullable;
          if (inItemPlan.SupplyPlanID.HasValue && this._persistedToAbort.TryGetValue(inItemPlan.SupplyPlanID, out nullable))
            inItemPlan.SupplyPlanID = nullable;
        }
      }
    }
    if (!EnumerableExtensions.IsIn<PXTranStatus>(tranStatus, (PXTranStatus) 1, (PXTranStatus) 2))
      return;
    this._selfInserted = (Dictionary<long?, List<INItemPlan>>) null;
    this._selfUpdated = (Dictionary<long?, List<INItemPlan>>) null;
  }
}
