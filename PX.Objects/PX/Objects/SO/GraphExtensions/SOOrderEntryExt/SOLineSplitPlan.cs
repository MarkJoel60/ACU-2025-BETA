// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOLineSplitPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOLineSplitPlan : ItemPlan<SOOrderEntry, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>
{
  protected Dictionary<object[], HashSet<Guid?>> _processingSets;
  private bool _initPlan;
  private bool _initVendor;
  private bool _resetSupplyPlanID;

  public override void Initialize()
  {
    base.Initialize();
    this._processingSets = new Dictionary<object[], HashSet<Guid?>>();
    PXGraph.FieldDefaultingEvents fieldDefaulting = ((PXGraph) this.Base).FieldDefaulting;
    SOLineSplitPlan soLineSplitPlan1 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) soLineSplitPlan1, __vmethodptr(soLineSplitPlan1, NegAvailQtyFieldDefaulting));
    fieldDefaulting.AddHandler<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.negAvailQty>(pxFieldDefaulting);
    PXGraph.RowUpdatedEvents rowUpdated1 = ((PXGraph) this.Base).RowUpdated;
    SOLineSplitPlan soLineSplitPlan2 = this;
    // ISSUE: virtual method pointer
    PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>.EventDelegate eventDelegate1 = new PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>.EventDelegate((object) soLineSplitPlan2, __vmethodptr(soLineSplitPlan2, _));
    rowUpdated1.RemoveHandler<PX.Objects.SO.SOOrder>(eventDelegate1);
    PXGraph.RowUpdatedEvents rowUpdated2 = ((PXGraph) this.Base).RowUpdated;
    SOLineSplitPlan soLineSplitPlan3 = this;
    // ISSUE: virtual method pointer
    PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>.EventDelegate eventDelegate2 = new PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>.EventDelegate((object) soLineSplitPlan3, __vmethodptr(soLineSplitPlan3, _));
    rowUpdated2.AddHandler<PX.Objects.SO.SOOrder>(eventDelegate2);
  }

  public override void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e)
  {
    base._(e);
    WebDialogResult answer = ((PXSelectBase) this.Base.Document).View.Answer;
    bool flag1 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.shipDate>((object) e.Row, (object) e.OldRow) && (answer == 6 || e.Row.ShipComplete != "B");
    bool flag2 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.requestDate>((object) e.Row, (object) e.OldRow) && (answer == 6 || e.Row.ShipComplete != "B");
    bool flag3 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.creditHold, PX.Objects.SO.SOOrder.approved>((object) e.Row, (object) e.OldRow);
    bool flag4 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.customerID>((object) e.Row, (object) e.OldRow);
    if (flag4 | flag1 | flag2 | flag3 || !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.hold, PX.Objects.SO.SOOrder.cancelled, PX.Objects.SO.SOOrder.completed, PX.Objects.SO.SOOrder.backOrdered, PX.Objects.SO.SOOrder.shipComplete, PX.Objects.SO.SOOrder.prepaymentReqSatisfied, PX.Objects.SO.SOOrder.isExpired>((object) e.Row, (object) e.OldRow))
    {
      bool flag5 = ((flag1 ? 1 : 0) | (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.shipComplete>((object) e.Row, (object) e.OldRow) ? 0 : (e.Row.ShipComplete != "B" ? 1 : 0))) != 0;
      bool flag6 = ((flag2 ? 1 : 0) | (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.shipComplete>((object) e.Row, (object) e.OldRow) ? 0 : (e.Row.ShipComplete != "B" ? 1 : 0))) != 0;
      bool valueOrDefault1 = e.Row.Cancelled.GetValueOrDefault();
      bool valueOrDefault2 = e.Row.Completed.GetValueOrDefault();
      bool? backOrdered = e.Row.BackOrdered;
      PXCache pxCache = (PXCache) GraphHelper.Caches<PX.Objects.SO.SOLineSplit>((PXGraph) this.Base);
      PXCache sender = (PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base);
      PX.Objects.SO.SOOrderType ordertype = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      Dictionary<int?, bool> dictionary1 = new Dictionary<int?, bool>();
      Dictionary<long?, PX.Objects.SO.SOLineSplit> dictionary2 = new Dictionary<long?, PX.Objects.SO.SOLineSplit>();
      HashSet<long?> nullableSet = new HashSet<long?>();
      SOOrderEntry soOrderEntry = this.Base;
      object[] objArray1 = (object[]) new PX.Objects.SO.SOOrder[1]
      {
        e.Row
      };
      object[] objArray2 = Array.Empty<object>();
      foreach (IEnumerable<PX.Objects.SO.SOLineSplit> source in GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelect<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.SelectMultiBound((PXGraph) soOrderEntry, objArray1, objArray2)).GroupBy<PX.Objects.SO.SOLineSplit, int?>((Func<PX.Objects.SO.SOLineSplit, int?>) (x => x.LineNbr)).ToDictionary<IGrouping<int?, PX.Objects.SO.SOLineSplit>, int?, List<PX.Objects.SO.SOLineSplit>>((Func<IGrouping<int?, PX.Objects.SO.SOLineSplit>, int?>) (x => x.Key), (Func<IGrouping<int?, PX.Objects.SO.SOLineSplit>, List<PX.Objects.SO.SOLineSplit>>) (x => x.ToList<PX.Objects.SO.SOLineSplit>())).Values)
      {
        bool flag7 = false;
        if (!this.IsDropShipNotLegacy(source.FirstOrDefault<PX.Objects.SO.SOLineSplit>()) & valueOrDefault1 && source.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x => !x.Completed.GetValueOrDefault() && !string.IsNullOrEmpty(x.PONbr))))
        {
          flag7 = true;
          dictionary1.Add(source.First<PX.Objects.SO.SOLineSplit>().LineNbr, !source.Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (x => x.Completed.GetValueOrDefault() && !string.IsNullOrEmpty(x.PONbr))));
        }
        foreach (PX.Objects.SO.SOLineSplit soLineSplit1 in source)
        {
          PX.Objects.SO.SOLineSplit split = soLineSplit1;
          if (valueOrDefault1 | valueOrDefault2)
          {
            if (!split.Completed.GetValueOrDefault())
            {
              EnumerableExtensions.ForEach<INItemPlan>(GraphHelper.RowCast<INItemPlan>(((PXCache) this.PlanCache).Inserted).Where<INItemPlan>((Func<INItemPlan, bool>) (_ =>
              {
                long? planId1 = _.PlanID;
                long? planId2 = split.PlanID;
                return planId1.GetValueOrDefault() == planId2.GetValueOrDefault() & planId1.HasValue == planId2.HasValue;
              })), (Action<INItemPlan>) (_ => this.PlanCache.Delete(_)));
              if (flag7)
              {
                split.POCreate = new bool?(false);
                if (!string.IsNullOrEmpty(split.PONbr))
                {
                  split.ClearPOReferences();
                  split.RefNoteID = new Guid?();
                }
              }
              split.PlanID = new long?();
              split.Completed = new bool?(true);
              GraphHelper.MarkUpdated(pxCache, (object) split, true);
            }
          }
          else
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, split.InventoryID);
            int num;
            if (inventoryItem != null)
            {
              bool? nullable = inventoryItem.IsConverted;
              if (nullable.GetValueOrDefault())
              {
                nullable = split.IsStockItem;
                if (nullable.HasValue)
                {
                  nullable = split.IsStockItem;
                  bool? stkItem = inventoryItem.StkItem;
                  num = !(nullable.GetValueOrDefault() == stkItem.GetValueOrDefault() & nullable.HasValue == stkItem.HasValue) ? 1 : 0;
                  goto label_18;
                }
              }
            }
            num = 0;
label_18:
            if (num == 0)
            {
              bool? nullable = e.OldRow.Cancelled;
              if (nullable.GetValueOrDefault())
              {
                if (string.IsNullOrEmpty(split.ShipmentNbr))
                {
                  nullable = split.POCompleted;
                  bool flag8 = false;
                  if (nullable.GetValueOrDefault() == flag8 & nullable.HasValue)
                    split.Completed = new bool?(false);
                }
                INItemPlan inItemPlan1 = this.DefaultValuesInt(new INItemPlan(), split);
                if (inItemPlan1 != null)
                {
                  INItemPlan inItemPlan2 = this.PlanCache.Insert(inItemPlan1);
                  split.PlanID = inItemPlan2.PlanID;
                }
                GraphHelper.MarkUpdated(pxCache, (object) split, true);
              }
            }
            if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.isExpired>((object) e.OldRow, (object) e.Row))
            {
              bool? nullable1 = split.POCreate;
              if (nullable1.GetValueOrDefault() && string.IsNullOrEmpty(split.PONbr))
              {
                nullable1 = e.Row.IsExpired;
                bool flag9 = false;
                if (nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue && !split.PlanID.HasValue)
                {
                  INItemPlan inItemPlan3 = this.DefaultValuesInt(new INItemPlan(), split);
                  if (inItemPlan3 != null)
                  {
                    INItemPlan inItemPlan4 = this.PlanCache.Insert(inItemPlan3);
                    split.PlanID = inItemPlan4.PlanID;
                    GraphHelper.MarkUpdated(pxCache, (object) split, true);
                  }
                }
                else
                {
                  nullable1 = e.Row.IsExpired;
                  if (nullable1.GetValueOrDefault())
                  {
                    long? nullable2 = split.PlanID;
                    if (nullable2.HasValue)
                    {
                      nullableSet.Add(split.PlanID);
                      PX.Objects.SO.SOLineSplit soLineSplit2 = split;
                      nullable2 = new long?();
                      long? nullable3 = nullable2;
                      soLineSplit2.PlanID = nullable3;
                      GraphHelper.MarkUpdated(pxCache, (object) split, true);
                    }
                  }
                }
              }
            }
            if (flag5 && !split.Completed.GetValueOrDefault())
            {
              split.ShipDate = e.Row.ShipDate;
              GraphHelper.MarkUpdated(pxCache, (object) split, true);
            }
            if (split.PlanID.HasValue)
              dictionary2[split.PlanID] = split;
          }
        }
      }
      bool flag10 = false;
      foreach (PXResult<PX.Objects.SO.SOLine> pxResult in PXSelectBase<PX.Objects.SO.SOLine, PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.SO.SOLine.lineType, NotEqual<SOLineType.miscCharge>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.SO.SOOrder[1]
      {
        e.Row
      }, Array.Empty<object>()))
      {
        PX.Objects.SO.SOLine line = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
        bool? nullable4;
        Decimal? nullable5;
        Decimal? nullable6;
        if (valueOrDefault1 | valueOrDefault2)
        {
          nullable4 = line.Completed;
          if (!nullable4.GetValueOrDefault())
          {
            PX.Objects.SO.SOLine copy = PXCache<PX.Objects.SO.SOLine>.CreateCopy(line);
            PX.Objects.SO.SOLine soLine = line;
            nullable5 = soLine.UnbilledQty;
            nullable6 = line.OpenQty;
            soLine.UnbilledQty = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
            line.OpenQty = new Decimal?(0M);
            sender.RaiseFieldUpdated<PX.Objects.SO.SOLine.unbilledQty>((object) line, (object) 0M);
            sender.RaiseFieldUpdated<PX.Objects.SO.SOLine.openQty>((object) line, (object) 0M);
            bool flag11;
            if (dictionary1.TryGetValue(line.LineNbr, out flag11))
            {
              if (flag11)
                line.POCreated = new bool?(false);
              nullable4 = line.POCreate;
              if (nullable4.GetValueOrDefault())
              {
                line.POCreate = new bool?(false);
                sender.RaiseFieldUpdated<PX.Objects.SO.SOLine.pOCreate>((object) line, (object) true);
              }
            }
            line.Completed = new bool?(true);
            SOOrderLineSplittingAllocatedExtension.ResetAvailabilityCounters(line);
            TaxBaseAttribute.Calculate<PX.Objects.SO.SOLine.taxCategoryID>(sender, new PXRowUpdatedEventArgs((object) line, (object) copy, false));
            flag10 = true;
            GraphHelper.MarkUpdated(sender, (object) line, true);
          }
        }
        else
        {
          nullable4 = e.OldRow.Cancelled;
          bool? nullable7;
          if (nullable4.GetValueOrDefault())
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, line.InventoryID);
            int num;
            if (inventoryItem != null)
            {
              nullable4 = inventoryItem.IsConverted;
              if (nullable4.GetValueOrDefault())
              {
                nullable4 = line.IsStockItem;
                if (nullable4.HasValue)
                {
                  nullable4 = line.IsStockItem;
                  nullable7 = inventoryItem.StkItem;
                  num = !(nullable4.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable4.HasValue == nullable7.HasValue) ? 1 : 0;
                  goto label_60;
                }
              }
            }
            num = 0;
label_60:
            if (num == 0)
            {
              PX.Objects.SO.SOLine copy = PXCache<PX.Objects.SO.SOLine>.CreateCopy(line);
              PX.Objects.SO.SOLine soLine1 = line;
              nullable6 = line.OrderQty;
              nullable5 = line.ShippedQty;
              Decimal? nullable8 = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
              soLine1.OpenQty = nullable8;
              PX.Objects.SO.SOLine soLine2 = line;
              nullable5 = soLine2.UnbilledQty;
              nullable6 = line.OpenQty;
              soLine2.UnbilledQty = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
              object obj = (object) line.UnbilledQty;
              sender.RaiseFieldVerifying<PX.Objects.SO.SOLine.unbilledQty>((object) line, ref obj);
              sender.RaiseFieldUpdated<PX.Objects.SO.SOLine.unbilledQty>((object) line, obj);
              obj = (object) line.OpenQty;
              sender.RaiseFieldVerifying<PX.Objects.SO.SOLine.openQty>((object) line, ref obj);
              sender.RaiseFieldUpdated<PX.Objects.SO.SOLine.openQty>((object) line, obj);
              sender.SetValueExt<PX.Objects.SO.SOLine.completed>((object) line, (object) false);
              TaxBaseAttribute.Calculate<PX.Objects.SO.SOLine.taxCategoryID>(sender, new PXRowUpdatedEventArgs((object) line, (object) copy, false));
            }
            else
              sender.SetValueExt<PX.Objects.SO.SOLine.openLine>((object) line, (object) false);
            flag10 = true;
            GraphHelper.MarkUpdated(sender, (object) line, true);
          }
          nullable7 = line.Completed;
          if (!nullable7.GetValueOrDefault())
          {
            if (flag5)
            {
              line.ShipDate = e.Row.ShipDate;
              GraphHelper.MarkUpdated(sender, (object) line, true);
            }
            if (flag6)
            {
              line.RequestDate = e.Row.RequestDate;
              GraphHelper.MarkUpdated(sender, (object) line, true);
            }
          }
          if (flag3 || !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOOrder.hold>((object) e.Row, (object) e.OldRow))
            SOOrderLineSplittingAllocatedExtension.ResetAvailabilityCounters(line);
        }
      }
      if (flag10)
      {
        PXFormulaAttribute.CalcAggregate<PX.Objects.SO.SOLine.unbilledQty>(sender, (object) e.Row);
        PXFormulaAttribute.CalcAggregate<PX.Objects.SO.SOLine.openQty>(sender, (object) e.Row);
      }
      PXSelectBase<INItemPlan> pxSelectBase = (PXSelectBase<INItemPlan>) new PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<PX.Objects.SO.SOOrder.noteID>>>>((PXGraph) this.Base);
      int? nullable9;
      DateTime? nullable10;
      if (backOrdered.GetValueOrDefault())
      {
        nullable9 = e.Row.LastSiteID;
        if (nullable9.HasValue)
        {
          nullable10 = e.Row.LastShipDate;
          if (nullable10.HasValue)
            pxSelectBase.WhereAnd<Where<INItemPlan.siteID, Equal<Current<PX.Objects.SO.SOOrder.lastSiteID>>, And<INItemPlan.planDate, LessEqual<Current<PX.Objects.SO.SOOrder.lastShipDate>>>>>();
        }
      }
      bool? nullable11 = backOrdered;
      bool flag12 = false;
      if (nullable11.GetValueOrDefault() == flag12 & nullable11.HasValue)
      {
        PX.Objects.SO.SOOrder row1 = e.Row;
        nullable9 = new int?();
        int? nullable12 = nullable9;
        row1.LastSiteID = nullable12;
        PX.Objects.SO.SOOrder row2 = e.Row;
        nullable10 = new DateTime?();
        DateTime? nullable13 = nullable10;
        row2.LastShipDate = nullable13;
      }
      foreach (INItemPlan plan in ((PXSelectBase) pxSelectBase).View.SelectMultiBound((object[]) new PX.Objects.SO.SOOrder[1]
      {
        e.Row
      }, Array.Empty<object>()).Cast<INItemPlan>().Where<INItemPlan>((Func<INItemPlan, bool>) (plan => !plan.IsSkippedWhenBackOrdered.GetValueOrDefault())))
      {
        if (valueOrDefault1 | valueOrDefault2 || nullableSet.Contains(plan.PlanID))
        {
          this.PlanCache.Delete(plan);
        }
        else
        {
          INItemPlan copy = PXCache<INItemPlan>.CreateCopy(plan);
          if (flag5)
            plan.PlanDate = e.Row.ShipDate;
          if (flag4)
            plan.BAccountID = e.Row.CustomerID;
          plan.Hold = new bool?(this.IsOrderOnHold(e.Row, (PX.Objects.SO.SOLineSplit) null));
          PX.Objects.SO.SOLineSplit split;
          if (this.IsPlanRegular(ordertype, plan) && dictionary2.TryGetValue(plan.PlanID, out split))
          {
            plan.PlanType = this.CalcPlanType(plan, e.Row, split, backOrdered);
            plan.Hold = new bool?(this.IsOrderOnHold(e.Row, split));
            if (!string.Equals(copy.PlanType, plan.PlanType))
              this.PlanCache.RaiseRowUpdated(plan, copy);
          }
          GraphHelper.MarkUpdated((PXCache) this.PlanCache, (object) plan, true);
        }
      }
      e.Row.BackOrdered = new bool?();
    }
    this.RecalcOpenLineCounters(e.Row, e.OldRow);
  }

  public virtual bool IsPlanRegular(PX.Objects.SO.SOOrderType ordertype, INItemPlan plan)
  {
    return !ordertype.RequireAllocation.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(plan.PlanType, "60", "62", "68", "69");
  }

  public override void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLineSplit> e)
  {
    bool flag = this.IsLineLinked(e.Row);
    this._initPlan = this.InitPlanRequired(e.Row, e.OldRow) && !flag;
    this._initVendor = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOLineSplit>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLineSplit.siteID, PX.Objects.SO.SOLineSplit.subItemID, PX.Objects.SO.SOLineSplit.vendorID, PX.Objects.SO.SOLineSplit.pOCreate>((object) e.Row, (object) e.OldRow) && !flag;
    this._resetSupplyPlanID = !flag;
    try
    {
      base._(e);
    }
    finally
    {
      this._initPlan = false;
      this._initVendor = false;
      this._resetSupplyPlanID = false;
    }
  }

  public override void _(PX.Data.Events.RowPersisting<INItemPlan> e)
  {
    if (e.Operation == 1)
    {
      PXCache pxCache = (PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base);
      INItemPlan row = e.Row;
      if (row != null && pxCache.Current is PX.Objects.SO.SOOrder current)
      {
        Guid? refNoteId = row.RefNoteID;
        Guid? noteId = current.NoteID;
        object[] objArray;
        if ((refNoteId.HasValue == noteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != noteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && PXLongOperation.GetCustomInfo(((PXGraph) this.Base).UID, "PXProcessingState", ref objArray) != null && objArray != null)
        {
          HashSet<Guid?> hashSet;
          if (!this._processingSets.TryGetValue(objArray, out hashSet))
          {
            hashSet = ((IEnumerable<object>) objArray).Select<object, Guid?>((Func<object, Guid?>) (x => ((PX.Objects.SO.SOOrder) x).NoteID)).ToHashSet<Guid?>();
            this._processingSets[objArray] = hashSet;
          }
          if (hashSet.Contains(row.RefNoteID))
            e.Cancel = true;
        }
      }
    }
    base._(e);
  }

  protected virtual void NegAvailQtyFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select(sender.Graph, Array.Empty<object>()));
    if (((CancelEventArgs) e).Cancel || soOrderType == null || !soOrderType.RequireAllocation.GetValueOrDefault())
      return;
    e.NewValue = (object) false;
    ((CancelEventArgs) e).Cancel = true;
  }

  public override INItemPlan DefaultValues(INItemPlan planRow, PX.Objects.SO.SOLineSplit splitRow)
  {
    if (!splitRow.Completed.GetValueOrDefault() && !splitRow.POCompleted.GetValueOrDefault() && !(splitRow.LineType == "MI"))
    {
      bool? nullable1;
      if (splitRow.LineType == "GN")
      {
        nullable1 = splitRow.RequireShipping;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && splitRow.Behavior != "BL")
          goto label_3;
      }
      PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.ItemPlanSourceCache, (object) splitRow);
      PX.Objects.SO.SOOrder order1 = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) this.ItemPlanSourceCache, (object) splitRow) ?? ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
      if (!string.IsNullOrEmpty(planRow.PlanType) && !this._initPlan)
      {
        if (this._resetSupplyPlanID)
        {
          nullable1 = order1.IsExpired;
          if (!nullable1.GetValueOrDefault())
            goto label_10;
        }
        else
          goto label_10;
      }
      INItemPlan inItemPlan1 = planRow;
      INItemPlan plan = planRow;
      PX.Objects.SO.SOOrder order2 = order1;
      PX.Objects.SO.SOLineSplit split = splitRow;
      nullable1 = new bool?();
      bool? backOrdered = nullable1;
      string str1 = this.CalcPlanType(plan, order2, split, backOrdered);
      inItemPlan1.PlanType = str1;
      nullable1 = splitRow.POCreate;
      int? nullable2;
      if (nullable1.GetValueOrDefault())
      {
        planRow.FixedSource = "P";
        planRow.SourceSiteID = !(splitRow.POType != "BL") || !(splitRow.POType != "DP") || !(splitRow.POSource == "O") ? splitRow.SiteID : order1.DestinationSiteID ?? splitRow.SiteID;
      }
      else
      {
        planRow.Reverse = new bool?(splitRow.Operation == "R");
        INItemPlan inItemPlan2 = planRow;
        int? siteId = splitRow.SiteID;
        nullable2 = splitRow.ToSiteID;
        string str2 = !(siteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & siteId.HasValue == nullable2.HasValue) ? "T" : "N";
        inItemPlan2.FixedSource = str2;
        planRow.SourceSiteID = splitRow.SiteID;
      }
label_10:
      if (this._resetSupplyPlanID)
        planRow.SupplyPlanID = new long?();
      planRow.VendorID = splitRow.VendorID;
      if (!this._initVendor)
      {
        nullable1 = splitRow.POCreate;
        if (nullable1.GetValueOrDefault())
        {
          nullable2 = planRow.VendorID;
          if (nullable2.HasValue)
          {
            nullable2 = planRow.VendorLocationID;
            if (nullable2.HasValue)
              goto label_17;
          }
          else
            goto label_17;
        }
        else
          goto label_17;
      }
      planRow.VendorLocationID = POItemCostManager.FetchLocation((PXGraph) this.Base, splitRow.VendorID, splitRow.InventoryID, splitRow.SubItemID, splitRow.SiteID);
label_17:
      INItemPlan inItemPlan3 = planRow;
      int? nullable3;
      if (soLine == null)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = soLine.CustomerID;
      inItemPlan3.BAccountID = nullable3;
      planRow.InventoryID = splitRow.InventoryID;
      INItemPlan inItemPlan4 = planRow;
      nullable2 = splitRow.InventoryID;
      int? nullable4 = (int?) soLine?.InventoryID;
      int? nullable5;
      if (nullable2.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue)
      {
        nullable4 = new int?();
        nullable5 = nullable4;
      }
      else if (soLine == null)
      {
        nullable4 = new int?();
        nullable5 = nullable4;
      }
      else
        nullable5 = soLine.InventoryID;
      inItemPlan4.KitInventoryID = nullable5;
      planRow.SubItemID = splitRow.SubItemID;
      planRow.SiteID = splitRow.SiteID;
      planRow.LocationID = splitRow.LocationID;
      planRow.LotSerialNbr = splitRow.LotSerialNbr;
      planRow.IsTempLotSerial = new bool?(!string.IsNullOrEmpty(splitRow.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(splitRow.AssignedNbr, splitRow.LotSerialNbr));
      INItemPlan inItemPlan5 = planRow;
      int? nullable6;
      if (soLine == null)
      {
        nullable4 = new int?();
        nullable6 = nullable4;
      }
      else
        nullable6 = soLine.ProjectID;
      inItemPlan5.ProjectID = nullable6;
      INItemPlan inItemPlan6 = planRow;
      int? nullable7;
      if (soLine == null)
      {
        nullable4 = new int?();
        nullable7 = nullable4;
      }
      else
        nullable7 = soLine.TaskID;
      inItemPlan6.TaskID = nullable7;
      planRow.CostCenterID = splitRow.CostCenterID;
      nullable1 = planRow.IsTempLotSerial;
      if (nullable1.GetValueOrDefault())
        planRow.LotSerialNbr = (string) null;
      planRow.PlanDate = splitRow.ShipDate;
      planRow.UOM = soLine?.UOM;
      INItemPlan inItemPlan7 = planRow;
      nullable1 = splitRow.POCreate;
      Decimal? nullable8;
      Decimal? nullable9;
      if (!nullable1.GetValueOrDefault())
      {
        nullable9 = splitRow.BaseQty;
      }
      else
      {
        Decimal? baseUnreceivedQty = splitRow.BaseUnreceivedQty;
        nullable8 = splitRow.Behavior == "BL" ? new Decimal?(0M) : splitRow.BaseShippedQty;
        nullable9 = baseUnreceivedQty.HasValue & nullable8.HasValue ? new Decimal?(baseUnreceivedQty.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? nullable10 = nullable9;
      Decimal? baseQtyOnOrders = splitRow.BaseQtyOnOrders;
      Decimal? nullable11;
      if (!(nullable10.HasValue & baseQtyOnOrders.HasValue))
      {
        nullable8 = new Decimal?();
        nullable11 = nullable8;
      }
      else
        nullable11 = new Decimal?(nullable10.GetValueOrDefault() - baseQtyOnOrders.GetValueOrDefault());
      inItemPlan7.PlanQty = nullable11;
      planRow.RefNoteID = order1.NoteID;
      planRow.Hold = new bool?(this.IsOrderOnHold(order1, splitRow));
      return string.IsNullOrEmpty(planRow.PlanType) ? (INItemPlan) null : planRow;
    }
label_3:
    return (INItemPlan) null;
  }

  protected virtual void RecalcOpenLineCounters(PX.Objects.SO.SOOrder newRow, PX.Objects.SO.SOOrder oldRow)
  {
    if (((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOOrder)].ObjectsEqual<PX.Objects.SO.SOOrder.cancelled, PX.Objects.SO.SOOrder.completed>((object) newRow, (object) oldRow))
      return;
    PXCache cach1 = ((PXGraph) this.Base).Caches[typeof (SOOrderSite)];
    PXCache cach2 = ((PXGraph) this.Base).Caches[typeof (PX.Objects.SO.SOLine)];
    foreach (SOOrderSite selectChild in PXParentAttribute.SelectChildren(cach1, (object) newRow, typeof (PX.Objects.SO.SOOrder)))
    {
      PXFormulaAttribute.CalcAggregate<PX.Objects.SO.SOLine.siteID>(cach2, (object) selectChild);
      GraphHelper.MarkUpdated(cach1, (object) selectChild, true);
    }
    PXFormulaAttribute.CalcAggregate<SOOrderSite.openShipmentCntr>(cach1, (object) newRow);
    PXFormulaAttribute.CalcAggregate<PX.Objects.SO.SOLine.openLine>(cach2, (object) newRow);
  }

  public virtual bool IsDropShipNotLegacy(PX.Objects.SO.SOLineSplit split)
  {
    PX.Objects.SO.SOLine soLine = split != null ? PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.ItemPlanSourceCache, (object) split) : (PX.Objects.SO.SOLine) null;
    if (soLine != null)
    {
      bool? nullable = soLine.POCreate;
      if (nullable.GetValueOrDefault() && soLine.POSource == "D")
      {
        nullable = soLine.IsLegacyDropShip;
        return !nullable.GetValueOrDefault();
      }
    }
    return false;
  }

  protected virtual bool InitPlanRequired(PX.Objects.SO.SOLineSplit row, PX.Objects.SO.SOLineSplit oldRow)
  {
    return !((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLineSplit>((PXGraph) this.Base)).ObjectsEqual<PX.Objects.SO.SOLineSplit.isAllocated, PX.Objects.SO.SOLineSplit.siteID, PX.Objects.SO.SOLineSplit.pOCreate, PX.Objects.SO.SOLineSplit.pOSource, PX.Objects.SO.SOLineSplit.operation>((object) row, (object) oldRow);
  }

  protected virtual bool IsLineLinked(PX.Objects.SO.SOLineSplit soLineSplit)
  {
    if (soLineSplit == null)
      return false;
    if (soLineSplit.PONbr != null)
      return true;
    return soLineSplit.SOOrderNbr != null && soLineSplit.IsAllocated.GetValueOrDefault();
  }

  protected virtual bool IsOrderOnHold(PX.Objects.SO.SOOrder order, PX.Objects.SO.SOLineSplit split)
  {
    bool flag = order?.Behavior == "RM" && order.DefaultOperation == "I" && EnumerableExtensions.IsIn<string>(order.ARDocType, "INV", "DRM") && split?.Operation == "R";
    if (order == null)
      return false;
    bool? nullable1 = order.Hold;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = order.CreditHold;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = order.Approved;
        bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
        if (!nullable2.GetValueOrDefault())
        {
          if (flag)
            return false;
          nullable2 = order.PrepaymentReqSatisfied;
          return !nullable2.GetValueOrDefault();
        }
      }
    }
    return true;
  }

  protected virtual string CalcPlanType(
    INItemPlan plan,
    PX.Objects.SO.SOOrder order,
    PX.Objects.SO.SOLineSplit split,
    bool? backOrdered = null)
  {
    if (split.POCreate.GetValueOrDefault() && split.Operation == "R")
      return (string) null;
    PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.ItemPlanSourceCache, (object) split);
    if (split.POCreate.GetValueOrDefault() && soLine != null && soLine.IsLegacyDropShip.GetValueOrDefault())
      return split.POType == "BL" ? (!(split.POSource == "D") ? "6B" : "6E") : (!(split.POSource == "D") ? "66" : "6D");
    if (split.POSource == "D" && (split.POCreate.GetValueOrDefault() || split.PONbr != null))
      return "6D";
    if (split.POCreate.GetValueOrDefault() && split.POSource == "L")
      return "6E";
    if (split.POCreate.GetValueOrDefault() && split.POSource == "B")
      return !order.IsExpired.GetValueOrDefault() || !string.IsNullOrEmpty(split.PONbr) ? "6B" : (string) null;
    if (split.POCreate.GetValueOrDefault() && split.POSource == "O")
      return !order.IsExpired.GetValueOrDefault() || !string.IsNullOrEmpty(split.PONbr) ? "66" : (string) null;
    PX.Objects.SO.SOOrderType ordertype = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
    bool flag1 = split.IsAllocated.GetValueOrDefault() || INPlanConstants.IsAllocated(plan.PlanType) || INPlanConstants.IsFixed(plan.PlanType);
    bool isOrderOnHold = this.IsOrderOnHold(order, split) && !ordertype.RequireAllocation.GetValueOrDefault();
    string str = this.CalcPlanType(plan, split, ordertype, isOrderOnHold);
    bool flag2 = str == "69";
    return !this._initPlan && !flag2 && !flag1 && (backOrdered.GetValueOrDefault() || !backOrdered.HasValue && plan.PlanType == "68") ? "68" : str;
  }

  protected virtual string CalcPlanType(
    INItemPlan plan,
    PX.Objects.SO.SOLineSplit split,
    PX.Objects.SO.SOOrderType ordertype,
    bool isOrderOnHold)
  {
    if (split.Behavior == "BL")
      return !split.IsAllocated.GetValueOrDefault() ? (string) null : split.AllocatedPlanType;
    if (ordertype == null || ordertype.RequireShipping.GetValueOrDefault())
    {
      if (split.IsAllocated.GetValueOrDefault())
        return split.AllocatedPlanType;
      if (isOrderOnHold)
        return "69";
      return split.RequireAllocation.GetValueOrDefault() && split.IsStockItem.GetValueOrDefault() ? split.BackOrderPlanType : split.PlanType;
    }
    return isOrderOnHold && split.IsStockItem.GetValueOrDefault() ? "69" : split.PlanType;
  }
}
