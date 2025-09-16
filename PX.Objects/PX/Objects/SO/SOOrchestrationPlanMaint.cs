// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrchestrationPlanMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

public class SOOrchestrationPlanMaint : PXGraph<
#nullable disable
SOOrchestrationPlanMaint, SOOrchestrationPlan>
{
  [PXCopyPasteHiddenFields(new Type[] {typeof (SOOrchestrationPlan.sourceSiteID), typeof (SOOrchestrationPlan.shippingZoneID), typeof (SOOrchestrationPlan.includeSourceWarehouse)})]
  public FbqlSelect<SelectFromBase<SOOrchestrationPlan, TypeArrayOf<IFbqlJoin>.Empty>, SOOrchestrationPlan>.View OrchestrationPlan;
  public FbqlSelect<SelectFromBase<SOOrchestrationPlanLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SOOrchestrationPlanLine.planID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOOrchestrationPlan.planID, IBqlString>.FromCurrent>>, 
  #nullable disable
  SOOrchestrationPlanLine>.View OrchestrationPlanLines;
  public FbqlSelect<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SOLine.orchestrationPlanID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOOrchestrationPlan.planID, IBqlString>.FromCurrent>>, 
  #nullable disable
  SOLine>.View OrchestratedOrderLines;

  public void _(
    PX.Data.Events.FieldUpdated<SOOrchestrationPlan, SOOrchestrationPlan.sourceSiteID> e)
  {
    if (e.Row == null)
      return;
    this.IncludeSourceWarehouseForFieldEvents();
  }

  public void _(
    PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.sourceSiteID> e)
  {
    SOOrchestrationPlan row = e.Row;
    if (row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.sourceSiteID>, SOOrchestrationPlan, object>) e).NewValue == null)
      return;
    bool? isActive = row.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.sourceSiteID>, SOOrchestrationPlan, object>) e).NewValue;
    SOOrchestrationPlan orchestrationPlan = PXResult<SOOrchestrationPlan>.op_Implicit(((IQueryable<PXResult<SOOrchestrationPlan>>) PXSelectBase<SOOrchestrationPlan, PXViewOf<SOOrchestrationPlan>.BasedOn<SelectFromBase<SOOrchestrationPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.sourceSiteID, Equal<P.AsInt>>>>, And<BqlOperand<SOOrchestrationPlan.strategy, IBqlString>.IsEqual<OrchestrationStrategies.warehousePriority>>>, And<BqlOperand<SOOrchestrationPlan.isActive, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<SOOrchestrationPlan.planID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) newValue,
      (object) row.PlanID
    })).FirstOrDefault<PXResult<SOOrchestrationPlan>>());
    if (orchestrationPlan != null)
    {
      string siteCd;
      if (e.OldValue != null)
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.sourceSiteID>, SOOrchestrationPlan, object>) e).NewValue = (object) PX.Objects.IN.INSite.PK.Find((PXGraph) this, (int?) e.OldValue).SiteCD;
        siteCd = PX.Objects.IN.INSite.PK.Find((PXGraph) this, newValue).SiteCD;
      }
      else
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.sourceSiteID>, SOOrchestrationPlan, object>) e).NewValue = (object) (siteCd = PX.Objects.IN.INSite.PK.Find((PXGraph) this, newValue).SiteCD);
      throw new PXSetPropertyException<SOOrchestrationPlan.sourceSiteID>("The {0} source warehouse has already been specified in the {1} orchestration plan.", (PXErrorLevel) 4, new object[2]
      {
        (object) siteCd,
        (object) orchestrationPlan.PlanID
      });
    }
  }

  public void _(
    PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.isActive> e)
  {
    if (e.Row == null || e.Row != null && !(bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.isActive>, SOOrchestrationPlan, object>) e)?.NewValue)
      return;
    bool? nullable1 = new bool?(false);
    if (e.Row.Strategy == "PO")
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, e.Row.SourceSiteID);
      bool? nullable2;
      if (inSite == null)
      {
        nullable2 = new bool?();
      }
      else
      {
        bool? active = inSite.Active;
        nullable2 = active.HasValue ? new bool?(!active.GetValueOrDefault()) : new bool?();
      }
      nullable1 = nullable2;
    }
    if (!nullable1.GetValueOrDefault())
      nullable1 = new bool?(((IQueryable<PXResult<SOOrchestrationPlanLine>>) PXSelectBase<SOOrchestrationPlanLine, PXViewOf<SOOrchestrationPlanLine>.BasedOn<SelectFromBase<SOOrchestrationPlanLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<BqlOperand<SOOrchestrationPlanLine.targetSiteID, IBqlInt>.IsEqual<PX.Objects.IN.INSite.siteID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INSite.active, Equal<False>>>>>.And<BqlOperand<SOOrchestrationPlanLine.planID, IBqlString>.IsEqual<BqlField<SOOrchestrationPlan.planID, IBqlString>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).FirstOrDefault<PXResult<SOOrchestrationPlanLine>>() != null);
    if (nullable1.GetValueOrDefault())
      throw new PXSetPropertyException<SOOrchestrationPlan.isActive>("You cannot activate the orchestration plan because at least one warehouse in this plan is inactive.", (PXErrorLevel) 4);
  }

  public void _(
    PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.shippingZoneID> e)
  {
    SOOrchestrationPlan row = e.Row;
    if (row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.shippingZoneID>, SOOrchestrationPlan, object>) e).NewValue == null)
      return;
    bool? isActive = row.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      return;
    string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOOrchestrationPlan, SOOrchestrationPlan.shippingZoneID>, SOOrchestrationPlan, object>) e).NewValue as string;
    SOOrchestrationPlan orchestrationPlan = PXResult<SOOrchestrationPlan>.op_Implicit(((IQueryable<PXResult<SOOrchestrationPlan>>) PXSelectBase<SOOrchestrationPlan, PXViewOf<SOOrchestrationPlan>.BasedOn<SelectFromBase<SOOrchestrationPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.shippingZoneID, Equal<P.AsString>>>>, And<BqlOperand<SOOrchestrationPlan.strategy, IBqlString>.IsEqual<OrchestrationStrategies.destinationPriority>>>, And<BqlOperand<SOOrchestrationPlan.isActive, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<SOOrchestrationPlan.planID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) newValue,
      (object) row.PlanID
    })).FirstOrDefault<PXResult<SOOrchestrationPlan>>());
    if (orchestrationPlan != null)
      throw new PXSetPropertyException<SOOrchestrationPlan.shippingZoneID>("The {0} shipping zone has already been specified in the {1} orchestration plan.", (PXErrorLevel) 4, new object[2]
      {
        (object) newValue,
        (object) orchestrationPlan.PlanID
      });
  }

  public void _(
    PX.Data.Events.FieldDefaulting<SOOrchestrationPlanLine, SOOrchestrationPlanLine.priority> e)
  {
    if (e.Row == null || !e.Row.TargetSiteID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOOrchestrationPlanLine, SOOrchestrationPlanLine.priority>, SOOrchestrationPlanLine, object>) e).NewValue = (object) this.GetPriority(e.Row);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SOOrchestrationPlanLine, SOOrchestrationPlanLine.priority>>) e).Cancel = true;
  }

  public void _(PX.Data.Events.RowInserted<SOOrchestrationPlanLine> e)
  {
    if (e.Row == null)
      return;
    this.IncludeSourceWarehouseForRowEvents();
  }

  public void _(PX.Data.Events.RowDeleted<SOOrchestrationPlanLine> e)
  {
    if (e.Row == null)
      return;
    this.IncludeSourceWarehouseForRowEvents();
  }

  public void _(PX.Data.Events.RowUpdated<SOOrchestrationPlanLine> e)
  {
    if (e.Row == null || !e.Row.TargetSiteID.HasValue)
      return;
    this.IncludeSourceWarehouseForRowEvents();
  }

  protected virtual void BeforePersist(PXGraph graph)
  {
    SOOrchestrationPlan current = ((PXSelectBase<SOOrchestrationPlan>) this.OrchestrationPlan).Current;
    if (current == null)
      return;
    bool? isActive;
    if (current.Strategy == "DP")
    {
      isActive = current.IsActive;
      if (isActive.GetValueOrDefault())
      {
        if (PXResult<SOOrchestrationPlan>.op_Implicit(((IQueryable<PXResult<SOOrchestrationPlan>>) PXSelectBase<SOOrchestrationPlan, PXViewOf<SOOrchestrationPlan>.BasedOn<SelectFromBase<SOOrchestrationPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.shippingZoneID, Equal<P.AsString>>>>, And<BqlOperand<SOOrchestrationPlan.strategy, IBqlString>.IsEqual<OrchestrationStrategies.destinationPriority>>>, And<BqlOperand<SOOrchestrationPlan.isActive, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<SOOrchestrationPlan.planID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) current.ShippingZoneID,
          (object) current.PlanID
        })).FirstOrDefault<PXResult<SOOrchestrationPlan>>()) != null)
        {
          ((PXSelectBase) this.OrchestrationPlan).Cache.RaiseExceptionHandling<SOOrchestrationPlan.shippingZoneID>((object) current, (object) current.ShippingZoneID, (Exception) new PXSetPropertyException<SOOrchestrationPlan.shippingZoneID>("The {0} shipping zone has already been specified in the {1} orchestration plan.", (PXErrorLevel) 4, new object[2]
          {
            (object) current.ShippingZoneID,
            (object) current.PlanID
          }));
          return;
        }
      }
    }
    if (current.Strategy == "PO")
    {
      isActive = current.IsActive;
      if (isActive.GetValueOrDefault())
      {
        if (PXResult<SOOrchestrationPlan>.op_Implicit(((IQueryable<PXResult<SOOrchestrationPlan>>) PXSelectBase<SOOrchestrationPlan, PXViewOf<SOOrchestrationPlan>.BasedOn<SelectFromBase<SOOrchestrationPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.sourceSiteID, Equal<P.AsInt>>>>, And<BqlOperand<SOOrchestrationPlan.strategy, IBqlString>.IsEqual<OrchestrationStrategies.warehousePriority>>>, And<BqlOperand<SOOrchestrationPlan.isActive, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<SOOrchestrationPlan.planID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) current.SourceSiteID,
          (object) current.PlanID
        })).FirstOrDefault<PXResult<SOOrchestrationPlan>>()) != null)
        {
          string siteCd = PX.Objects.IN.INSite.PK.Find((PXGraph) this, current.SourceSiteID).SiteCD;
          ((PXSelectBase) this.OrchestrationPlan).Cache.RaiseExceptionHandling<SOOrchestrationPlan.sourceSiteID>((object) current, (object) siteCd, (Exception) new PXSetPropertyException<SOOrchestrationPlan.sourceSiteID>("The {0} source warehouse has already been specified in the {1} orchestration plan.", (PXErrorLevel) 4, new object[2]
          {
            (object) siteCd,
            (object) current.PlanID
          }));
          return;
        }
      }
    }
    IEnumerable<SOOrchestrationPlanLine> source = GraphHelper.RowCast<SOOrchestrationPlanLine>((IEnumerable) ((PXSelectBase<SOOrchestrationPlanLine>) this.OrchestrationPlanLines).Select(Array.Empty<object>())).AsEnumerable<SOOrchestrationPlanLine>().GroupBy<SOOrchestrationPlanLine, int?>((Func<SOOrchestrationPlanLine, int?>) (l => l.Priority)).Where<IGrouping<int?, SOOrchestrationPlanLine>>((Func<IGrouping<int?, SOOrchestrationPlanLine>, bool>) (l => l.Count<SOOrchestrationPlanLine>() > 1)).SelectMany<IGrouping<int?, SOOrchestrationPlanLine>, SOOrchestrationPlanLine>((Func<IGrouping<int?, SOOrchestrationPlanLine>, IEnumerable<SOOrchestrationPlanLine>>) (g => (IEnumerable<SOOrchestrationPlanLine>) g));
    if (!source.Any<SOOrchestrationPlanLine>())
      return;
    foreach (SOOrchestrationPlanLine orchestrationPlanLine in NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.OrchestrationPlanLines).Cache.Updated, ((PXSelectBase) this.OrchestrationPlanLines).Cache.Inserted))
    {
      SOOrchestrationPlanLine line = orchestrationPlanLine;
      if (source.Any<SOOrchestrationPlanLine>((Func<SOOrchestrationPlanLine, bool>) (l =>
      {
        int? priority1 = l.Priority;
        int? priority2 = line.Priority;
        return priority1.GetValueOrDefault() == priority2.GetValueOrDefault() & priority1.HasValue == priority2.HasValue;
      })))
      {
        ((PXSelectBase) this.OrchestrationPlanLines).Cache.RaiseExceptionHandling<SOOrchestrationPlanLine.priority>((object) line, (object) line.Priority, (Exception) new PXSetPropertyException((IBqlTable) line, "This priority already exists.", (PXErrorLevel) 4));
        break;
      }
    }
  }

  public void _(PX.Data.Events.RowSelected<SOOrchestrationPlan> e)
  {
    if (e.Row == null)
      return;
    bool flag = ((PXSelectBase<SOLine>) this.OrchestratedOrderLines).SelectSingle(Array.Empty<object>()) == null;
    PXUIFieldAttribute.SetEnabled<SOOrchestrationPlan.strategy>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrchestrationPlan>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<SOOrchestrationPlan.sourceSiteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrchestrationPlan>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<SOOrchestrationPlan.shippingZoneID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrchestrationPlan>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetEnabled<SOOrchestrationPlan.includeSourceWarehouse>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrchestrationPlan>>) e).Cache, (object) e.Row, flag);
    ((PXSelectBase) this.OrchestrationPlanLines).AllowInsert = flag;
    ((PXSelectBase) this.OrchestrationPlanLines).AllowUpdate = flag;
    ((PXSelectBase) this.OrchestrationPlanLines).AllowDelete = flag;
  }

  public void _(PX.Data.Events.RowDeleting<SOOrchestrationPlan> e)
  {
    if (e.Row != null && ((PXSelectBase<SOLine>) this.OrchestratedOrderLines).SelectSingle(Array.Empty<object>()) != null)
      throw new PXSetPropertyException((IBqlTable) e.Row, "The orchestration plan cannot be deleted because at least one order orchestration has been performed for this plan.");
  }

  public void _(PX.Data.Events.RowInserted<SOOrchestrationPlan> e)
  {
    SOOrchestrationPlan row = e.Row;
    if (row == null)
      return;
    this.AddOrReviewSourceWarehouse(row.SourceSiteID, row.IncludeSourceWarehouse);
    if (row.Strategy == "PO")
    {
      PXUIFieldAttribute.SetError<SOOrchestrationPlan.shippingZoneID>(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<SOOrchestrationPlan>>) e).Cache, (object) row, (string) null);
    }
    else
    {
      if (!(row.Strategy == "DP"))
        return;
      PXUIFieldAttribute.SetError<SOOrchestrationPlan.sourceSiteID>(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<SOOrchestrationPlan>>) e).Cache, (object) row, (string) null);
    }
  }

  public void _(PX.Data.Events.RowUpdated<SOOrchestrationPlan> e)
  {
    SOOrchestrationPlan row = e.Row;
    if (row == null)
      return;
    this.AddOrReviewSourceWarehouse(row.SourceSiteID, row.IncludeSourceWarehouse);
    if (row.Strategy == "PO")
    {
      PXUIFieldAttribute.SetError<SOOrchestrationPlan.shippingZoneID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOOrchestrationPlan>>) e).Cache, (object) row, (string) null);
    }
    else
    {
      if (!(row.Strategy == "DP"))
        return;
      PXUIFieldAttribute.SetError<SOOrchestrationPlan.sourceSiteID>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOOrchestrationPlan>>) e).Cache, (object) row, (string) null);
    }
  }

  public SOOrchestrationPlanMaint()
  {
    ((PXGraph) this).OnBeforePersist += new Action<PXGraph>(this.BeforePersist);
  }

  protected virtual void IncludeSourceWarehouseForRowEvents()
  {
    SOOrchestrationPlan current = ((PXSelectBase<SOOrchestrationPlan>) this.OrchestrationPlan).Current;
    int? sourceSiteID = current.SourceSiteID;
    SOOrchestrationPlan copy = PXCache<SOOrchestrationPlan>.CreateCopy(current);
    if (!sourceSiteID.HasValue)
    {
      copy.IncludeSourceWarehouse = new bool?(false);
      ((PXSelectBase) this.OrchestrationPlan).Cache.Update((object) copy);
    }
    else
    {
      copy.IncludeSourceWarehouse = ((IEnumerable<SOOrchestrationPlanLine>) ((PXSelectBase<SOOrchestrationPlanLine>) this.OrchestrationPlanLines).Select<SOOrchestrationPlanLine>(Array.Empty<object>())).AsEnumerable<SOOrchestrationPlanLine>().Where<SOOrchestrationPlanLine>((Func<SOOrchestrationPlanLine, bool>) (i =>
      {
        int? targetSiteId = i.TargetSiteID;
        int? nullable = sourceSiteID;
        return targetSiteId.GetValueOrDefault() == nullable.GetValueOrDefault() & targetSiteId.HasValue == nullable.HasValue;
      })).FirstOrDefault<SOOrchestrationPlanLine>() == null ? new bool?(false) : new bool?(true);
      ((PXSelectBase) this.OrchestrationPlan).Cache.Update((object) copy);
    }
  }

  protected virtual void IncludeSourceWarehouseForFieldEvents()
  {
    SOOrchestrationPlan current = ((PXSelectBase<SOOrchestrationPlan>) this.OrchestrationPlan).Current;
    int? sourceSiteID = current.SourceSiteID;
    if (!sourceSiteID.HasValue)
      ((PXSelectBase) this.OrchestrationPlan).Cache.SetValueExt<SOOrchestrationPlan.includeSourceWarehouse>((object) current, (object) false);
    else if (((IEnumerable<SOOrchestrationPlanLine>) ((PXSelectBase<SOOrchestrationPlanLine>) this.OrchestrationPlanLines).Select<SOOrchestrationPlanLine>(Array.Empty<object>())).Where<SOOrchestrationPlanLine>((Func<SOOrchestrationPlanLine, bool>) (i =>
    {
      int? targetSiteId = i.TargetSiteID;
      int? nullable = sourceSiteID;
      return targetSiteId.GetValueOrDefault() == nullable.GetValueOrDefault() & targetSiteId.HasValue == nullable.HasValue;
    })).FirstOrDefault<SOOrchestrationPlanLine>() != null)
      ((PXSelectBase) this.OrchestrationPlan).Cache.SetValueExt<SOOrchestrationPlan.includeSourceWarehouse>((object) current, (object) true);
    else
      ((PXSelectBase) this.OrchestrationPlan).Cache.SetValueExt<SOOrchestrationPlan.includeSourceWarehouse>((object) current, (object) false);
  }

  protected virtual void AddOrReviewSourceWarehouse(
    int? sourceWarehouse,
    bool? includeSourceWarehouse)
  {
    List<SOOrchestrationPlanLine> list = GraphHelper.RowCast<SOOrchestrationPlanLine>((IEnumerable) ((IEnumerable<SOOrchestrationPlanLine>) ((PXSelectBase<SOOrchestrationPlanLine>) this.OrchestrationPlanLines).Select<SOOrchestrationPlanLine>(Array.Empty<object>())).AsEnumerable<SOOrchestrationPlanLine>().Where<SOOrchestrationPlanLine>((Func<SOOrchestrationPlanLine, bool>) (i =>
    {
      int? targetSiteId = i.TargetSiteID;
      int? nullable = sourceWarehouse;
      if (targetSiteId.GetValueOrDefault() == nullable.GetValueOrDefault() & targetSiteId.HasValue == nullable.HasValue)
        return true;
      int? priority = i.Priority;
      int num = 0;
      return priority.GetValueOrDefault() == num & priority.HasValue;
    }))).ToList<SOOrchestrationPlanLine>();
    if (!list.Any<SOOrchestrationPlanLine>() && sourceWarehouse.HasValue && includeSourceWarehouse.GetValueOrDefault())
    {
      ((PXSelectBase) this.OrchestrationPlanLines).Cache.Insert((object) new SOOrchestrationPlanLine()
      {
        TargetSiteID = sourceWarehouse,
        Priority = new int?(0)
      });
    }
    else
    {
      if (!list.Any<SOOrchestrationPlanLine>())
        return;
      SOOrchestrationPlanLine orchestrationPlanLine1 = list.Where<SOOrchestrationPlanLine>((Func<SOOrchestrationPlanLine, bool>) (w =>
      {
        int? priority = w.Priority;
        int num = 0;
        return priority.GetValueOrDefault() == num & priority.HasValue;
      })).FirstOrDefault<SOOrchestrationPlanLine>();
      SOOrchestrationPlanLine orchestrationPlanLine2 = list.Where<SOOrchestrationPlanLine>((Func<SOOrchestrationPlanLine, bool>) (w =>
      {
        int? targetSiteId = w.TargetSiteID;
        int? nullable = sourceWarehouse;
        return targetSiteId.GetValueOrDefault() == nullable.GetValueOrDefault() & targetSiteId.HasValue == nullable.HasValue;
      })).FirstOrDefault<SOOrchestrationPlanLine>();
      int? priority1;
      if (orchestrationPlanLine1 != null)
      {
        priority1 = orchestrationPlanLine1.Priority;
        int num = 0;
        if (priority1.GetValueOrDefault() == num & priority1.HasValue)
        {
          bool? nullable = includeSourceWarehouse;
          bool flag = false;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue || !includeSourceWarehouse.HasValue || list.Count<SOOrchestrationPlanLine>() == 2)
          {
            ((PXSelectBase) this.OrchestrationPlanLines).Cache.Delete((object) orchestrationPlanLine1);
            goto label_10;
          }
        }
      }
      if (orchestrationPlanLine2 != null)
      {
        priority1 = orchestrationPlanLine2.Priority;
        int num = 0;
        if (!(priority1.GetValueOrDefault() == num & priority1.HasValue) && includeSourceWarehouse.GetValueOrDefault())
        {
          orchestrationPlanLine2.Priority = new int?(0);
          ((PXSelectBase) this.OrchestrationPlanLines).Cache.Update((object) orchestrationPlanLine2);
        }
      }
    }
label_10:
    ((PXSelectBase) this.OrchestrationPlanLines).View.RequestRefresh();
  }

  protected virtual int GetPriority(SOOrchestrationPlanLine row)
  {
    SOOrchestrationPlan current = ((PXSelectBase<SOOrchestrationPlan>) this.OrchestrationPlan).Current;
    int? targetSiteId = row.TargetSiteID;
    int? sourceSiteId = current.SourceSiteID;
    if (targetSiteId.GetValueOrDefault() == sourceSiteId.GetValueOrDefault() & targetSiteId.HasValue == sourceSiteId.HasValue)
      return 0;
    int? priority = row.Priority;
    int num = 1;
    if (priority.GetValueOrDefault() >= num & priority.HasValue)
      return row.Priority.Value;
    SOOrchestrationPlanLine orchestrationPlanLine = GraphHelper.RowCast<SOOrchestrationPlanLine>((IEnumerable) ((IEnumerable<PXResult<SOOrchestrationPlanLine>>) ((PXSelectBase<SOOrchestrationPlanLine>) this.OrchestrationPlanLines).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOOrchestrationPlanLine>>()).OrderBy<SOOrchestrationPlanLine, int?>((Func<SOOrchestrationPlanLine, int?>) (i => i.Priority)).LastOrDefault<SOOrchestrationPlanLine>();
    return orchestrationPlanLine == null || !orchestrationPlanLine.Priority.HasValue ? 1 : orchestrationPlanLine.Priority.Value + 1;
  }
}
