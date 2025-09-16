// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.POLinePlan`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public abstract class POLinePlan<TGraph> : POLinePlanBase<TGraph, PX.Objects.PO.POLine> where TGraph : PXGraph
{
  public override void _(PX.Data.Events.RowUpdated<PX.Objects.PO.POOrder> e)
  {
    base._(e);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.PO.POOrder>>) e).Cache.ObjectsEqual<PX.Objects.PO.POOrder.status, PX.Objects.PO.POOrder.cancelled>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.refNoteID, Equal<Current<PX.Objects.PO.POOrder.noteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      INItemPlan plan = PXResult<INItemPlan>.op_Implicit(pxResult);
      if (e.Row.Cancelled.GetValueOrDefault())
      {
        this.PlanCache.Delete(plan);
      }
      else
      {
        INItemPlan copy = PXCache<INItemPlan>.CreateCopy(plan);
        bool isOnHold = this.IsOrderOnHold(e.Row);
        string newPlanType;
        if (this.TryCalcPlanType(plan, isOnHold, out newPlanType))
          plan.PlanType = newPlanType;
        plan.Hold = new bool?(isOnHold);
        if (!string.Equals(copy.PlanType, plan.PlanType))
          this.PlanCache.RaiseRowUpdated(plan, copy);
        GraphHelper.MarkUpdated((PXCache) this.PlanCache, (object) plan, true);
      }
    }
  }

  protected virtual bool TryCalcPlanType(INItemPlan plan, bool isOnHold, out string newPlanType)
  {
    newPlanType = (string) null;
    string planType = plan.PlanType;
    if (planType != null && planType.Length == 2)
    {
      switch (planType[1])
      {
        case '0':
          if (planType == "70")
            goto label_10;
          goto label_14;
        case '3':
          switch (planType)
          {
            case "M3":
              break;
            case "73":
              goto label_10;
            default:
              goto label_14;
          }
          break;
        case '4':
          switch (planType)
          {
            case "M4":
              break;
            case "74":
              goto label_12;
            default:
              goto label_14;
          }
          break;
        case '6':
          if (planType == "76")
            goto label_11;
          goto label_14;
        case '7':
          if (planType == "F7")
            goto label_13;
          goto label_14;
        case '8':
          switch (planType)
          {
            case "78":
              goto label_11;
            case "F8":
              goto label_13;
            default:
              goto label_14;
          }
        case '9':
          if (planType == "79")
            goto label_12;
          goto label_14;
        default:
          goto label_14;
      }
      newPlanType = isOnHold ? "M3" : "M4";
      goto label_14;
label_10:
      newPlanType = isOnHold ? "73" : "70";
      goto label_14;
label_11:
      newPlanType = isOnHold ? "78" : "76";
      goto label_14;
label_12:
      newPlanType = isOnHold ? "79" : "74";
      goto label_14;
label_13:
      newPlanType = isOnHold ? "F8" : "F7";
    }
label_14:
    return newPlanType != null;
  }
}
